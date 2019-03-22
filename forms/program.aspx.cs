using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;
using System.Collections;

public partial class forms_program : System.Web.UI.Page
{
    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
    }

    protected void LoadData()
    {
        PEnt = new program();
        gridProgarm.DataSource = PSer.GetAll(PEnt);
        gridProgarm.DataBind();

        if (gridProgarm.Rows.Count == 0)
        {
            ArrayList a1 = new ArrayList();
            PEnt = new program();
            a1.Add(PEnt);
            gridProgarm.DataSource = a1;
            gridProgarm.DataBind();
        }
    }

   
    protected void gridProgarm_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {
            DropDownList ddlFacultyH = e.Row.FindControl("ddlFacultyH") as DropDownList;
            DropDownList ddlLevelH = e.Row.FindControl("ddlLevelH") as DropDownList;

            FCEnt = new hss_faculty();
            ddlFacultyH.DataSource = FCSer.GetAll(FCEnt);
            ddlFacultyH.DataTextField = "FACULTY";
            ddlFacultyH.DataValueField = "PK_ID";
            ddlFacultyH.DataBind();
            ddlFacultyH.Items.Insert(0, "Select");

            LEnt = new HSS_LEVEL();
            ddlLevelH.DataSource = LSrv.GetAll(LEnt);
            ddlLevelH.DataTextField = "LEVEL_NAME";
            ddlLevelH.DataValueField = "LEVEL_NAME";
            ddlLevelH.DataBind();
            ddlLevelH.Items.Insert(0, "Select");

            

        }

        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            Label lblFac = e.Row.FindControl("lblFac") as Label;
            Label lblFaculty = e.Row.FindControl("lblFaculty") as Label;
           
            FCEnt = new hss_faculty();
            FCEnt.PK_ID = lblFac.Text;
            FCEnt = (hss_faculty)FCSer.GetSingle(FCEnt);
            if (FCEnt != null)
            {
                lblFaculty.Text = FCEnt.FACULTY;
            }

        }

        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) != 0)
        {
            Label lblFacultyE = e.Row.FindControl("lblFacultyE") as Label;
            DropDownList ddlFacultyE = e.Row.FindControl("ddlFacultyE") as DropDownList;
            Label lblLevelE = e.Row.FindControl("lblLevelE") as Label;
            DropDownList ddlLevelE = e.Row.FindControl("ddlLevelE") as DropDownList;
            Label lblResultTypeE = e.Row.FindControl("lblResultTypeE") as Label;
            DropDownList ddlResultTypeE = e.Row.FindControl("ddlResultTypeE") as DropDownList;
            TextBox txtProgramCodeE = e.Row.FindControl("txtProgramCodeE") as TextBox;

            FCEnt = new hss_faculty();
            ddlFacultyE.DataSource = FCSer.GetAll(FCEnt);
            ddlFacultyE.DataTextField = "FACULTY";
            ddlFacultyE.DataValueField = "PK_ID";
            ddlFacultyE.DataBind();
            ddlFacultyE.Items.Insert(0, "Select");
            ddlFacultyE.SelectedValue = lblFacultyE.Text;

            LEnt = new HSS_LEVEL();
            ddlLevelE.DataSource = LSrv.GetAll(LEnt);
            ddlLevelE.DataTextField = "LEVEL_NAME";
            ddlLevelE.DataValueField = "LEVEL_NAME";
            ddlLevelE.DataBind();
            ddlLevelE.Items.Insert(0, "Select");
            ddlLevelE.SelectedValue = lblLevelE.Text;

            ddlResultTypeE.SelectedValue = lblResultTypeE.Text;


        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        GridViewRow row = gridProgarm.HeaderRow;

        DropDownList ddlFacultyH = (DropDownList)row.FindControl("ddlFacultyH");
        DropDownList ddlLevelH = (DropDownList)row.FindControl("ddlLevelH");
        DropDownList ddlResultTypeH = (DropDownList)row.FindControl("ddlResultTypeH");
        TextBox txtProgramCodeH = (TextBox)row.FindControl("txtProgramCodeH");
        TextBox txtProgramNameH = (TextBox)row.FindControl("txtProgramNameH");

        if (ddlFacultyH.SelectedIndex != 0 && ddlLevelH.SelectedIndex != 0 && txtProgramCodeH.Text != "" && txtProgramNameH.Text != null)
        {
            PEnt = new program();
            PEnt.FACULTY_ID = ddlFacultyH.SelectedValue;
            PEnt.PROGRAM_CODE = txtProgramCodeH.Text;
            PEnt.PROGRAM_NAME = txtProgramNameH.Text;
            PEnt.PROGRAM_LEVEL = ddlLevelH.SelectedValue;
            PEnt.RESULT_TYPE = ddlResultTypeH.SelectedValue;

            PSer.Insert(PEnt);
            LoadData();
        }
        else
        {
            HelperFunction.MsgBox(this, this.GetType(), "Field must not be empty!");
        }

    }
    protected void gridProgarm_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridProgarm.EditIndex = e.NewEditIndex;
        LoadData();
    }
    protected void gridProgarm_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridProgarm.EditIndex = -1;
        LoadData();
    }

    protected void gridProgarm_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gridProgarm.Rows[e.RowIndex];
        DropDownList ddlFacultyE = row.FindControl("ddlFacultyE") as DropDownList;
        DropDownList ddlLevelE = row.FindControl("ddlLevelE") as DropDownList;
        DropDownList ddlResultTypeE = row.FindControl("ddlResultTypeE") as DropDownList;
        TextBox txtProgramCodeE = row.FindControl("txtProgramCodeE") as TextBox;
        TextBox txtProgramNameE = row.FindControl("txtProgramNameE") as TextBox;
        Label lblPKIDU = row.FindControl("lblPKIDU") as Label;

        if (ddlFacultyE.SelectedIndex != 0 && ddlLevelE.SelectedIndex != 0 && txtProgramCodeE.Text != "" && txtProgramNameE.Text != "")
        {
            PEnt = new program();
            PEnt.PK_ID = lblPKIDU.Text;
            PEnt.FACULTY_ID = ddlFacultyE.SelectedValue;
            PEnt.PROGRAM_CODE = txtProgramCodeE.Text;
            PEnt.PROGRAM_NAME = txtProgramNameE.Text;
            PEnt.PROGRAM_LEVEL = ddlLevelE.SelectedValue;
            PEnt.RESULT_TYPE = ddlResultTypeE.SelectedValue;
            PSer.Update(PEnt);
            gridProgarm.EditIndex = -1;
            LoadData();
        }
        else
        {
            HelperFunction.MsgBox(this, this.GetType(), "Field must not be empty!");
        }

    }
}