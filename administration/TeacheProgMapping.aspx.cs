using Entity.Components;
using Service.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Entity.Framework;

public partial class administration_TeacheProgMapping : System.Web.UI.Page
{

    program PEnt = new program();
    programService PSer = new programService();

    hss_faculty FEnt = new hss_faculty();
    hss_facultyService FSer = new hss_facultyService();

    HSS_LEVEL LVLEnt = new HSS_LEVEL();
    HSS_LEVELService LVLSer = new HSS_LEVELService();

    Employees EMPEnt = new Employees();
    EmployeesService EMPSer = new EmployeesService();

    TEACHERPROGRAMMAPPING TPMEnt = new TEACHERPROGRAMMAPPING();
    TEACHERPROGRAMMAPPINGService TPMSer = new TEACHERPROGRAMMAPPINGService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadFaculty();
            loadLevel();
        }
    }

    protected void loadFaculty()
    {
        FEnt = new hss_faculty();
        ddlFaculty.DataSource = FSer.GetAll(FEnt);
        ddlFaculty.DataTextField = "FACULTY";
        ddlFaculty.DataValueField = "PK_ID";
        ddlFaculty.DataBind();
        ddlFaculty.Items.Insert(0, "Select");
    }

    protected void loadLevel()
    {
        LVLEnt = new HSS_LEVEL();
        ddlLevel.DataSource = LVLSer.GetAll(LVLEnt);
        ddlLevel.DataTextField = "LEVEL_NAME";
        ddlLevel.DataValueField = "LEVEL_NAME";
        ddlLevel.DataBind();
        ddlLevel.Items.Insert(0, "Select");
    }

    protected void LoadProgram()
    {
        PEnt = new program();
        PEnt.PROGRAM_LEVEL = ddlLevel.SelectedValue;
        PEnt.FACULTY_ID = ddlFaculty.SelectedValue;
        ddlProgram.DataSource = PSer.GetAll(PEnt);
        ddlProgram.DataTextField = "PROGRAM_CODE";
        ddlProgram.DataValueField = "PK_ID";
        ddlProgram.DataBind();
        ddlProgram.Items.Insert(0, "Select");

    }

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadGrid();

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        GridViewRow row = gridTeacherProgMap.HeaderRow;

        DropDownList ddlTeacherH = (DropDownList)row.FindControl("ddlTeacherH");
        DropDownList ddlStatusH = (DropDownList)row.FindControl("ddlStatusH");

        TPMEnt = new TEACHERPROGRAMMAPPING();
        TPMEnt.TEACHERID = ddlTeacherH.SelectedValue;
        TPMEnt.PROGRAMID = ddlProgram.SelectedValue;
        //TPMEnt.STATUS = "1";
        TPMEnt = (TEACHERPROGRAMMAPPING)TPMSer.GetSingle(TPMEnt);
        if (TPMEnt != null)
        {
            TPMEnt.TEACHERID = ddlTeacherH.SelectedValue;
            TPMEnt.PROGRAMID = ddlProgram.SelectedValue;
            TPMEnt.STATUS = ddlStatusH.SelectedValue;
            if (TPMSer.Update(TPMEnt) > 0)
            {
                HelperFunction.MsgBox(this, this.GetType(), "Updated Successfully");

            }
            else
            {
                HelperFunction.MsgBox(this, this.GetType(), "Unable to update");
            }
        }
        else
        {
            TPMEnt = new TEACHERPROGRAMMAPPING();
            TPMEnt.TEACHERID = ddlTeacherH.SelectedValue;
            TPMEnt.PROGRAMID = ddlProgram.SelectedValue;
            TPMEnt.STATUS = ddlStatusH.SelectedValue;
            TPMSer.Insert(TPMEnt);
        }
        loadGrid();

    }

    protected void loadGrid()
    {
        TPMEnt = new TEACHERPROGRAMMAPPING();
        TPMEnt.PROGRAMID = ddlProgram.SelectedValue;
        gridTeacherProgMap.DataSource = TPMSer.GetAll(TPMEnt);
        gridTeacherProgMap.DataBind();
        if (gridTeacherProgMap.Rows.Count == 0)
        {
            TPMEnt = new TEACHERPROGRAMMAPPING();
            ArrayList a1 = new ArrayList();
            a1.Add(TPMEnt);
            gridTeacherProgMap.DataSource = a1;
            gridTeacherProgMap.DataBind();
        }
    }

    protected void gridTeacherProgMap_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            Label lblStat = e.Row.FindControl("lblStat") as Label;
            Label lblStatus = e.Row.FindControl("lblStatus") as Label;

            if (lblStat.Text == "1")
            {
                lblStatus.Text = "Available";

            }
            else if (lblStat.Text == "0")
            {
                lblStatus.Text = "Not Available";
                lblStatus.Visible = false;

            }
        }


        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            Label lblTeacher = e.Row.FindControl("lblTeacher") as Label;
            Label lblTeacherName = e.Row.FindControl("lblTeacherName") as Label;

            Employees EMPEnt = new Employees();
            EMPEnt.EMPLOYEEID = lblTeacher.Text;
            EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
            if (EMPEnt != null && lblTeacher.Text!="")
            {
                lblTeacherName.Text = EMPEnt.FULLNAME;
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) != 0)
        {
            Label lblStatE = e.Row.FindControl("lblStatE") as Label;
            DropDownList ddlStatusE = e.Row.FindControl("ddlStatusE") as DropDownList;

            Label lblTeachE = e.Row.FindControl("lblTeachE") as Label;
            DropDownList ddlTeacherE = e.Row.FindControl("ddlTeacherE") as DropDownList;

            EntityList theList = new EntityList();

            EMPEnt = new Employees();
            EMPEnt.STATUS = "0";
            ddlTeacherE.DataSource = EMPSer.GetAll(EMPEnt);
            ddlTeacherE.DataTextField = "FULLNAME";
            ddlTeacherE.DataValueField = "EMPLOYEEID";
            ddlTeacherE.DataBind();
            ddlTeacherE.SelectedValue = lblTeachE.Text;

            ddlStatusE.SelectedValue = lblStatE.Text;

        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            DropDownList ddlTeacherH = e.Row.FindControl("ddlTeacherH") as DropDownList;

            EMPEnt = new Employees();
            EMPEnt.STATUS = "0";
            ddlTeacherH.DataSource = EMPSer.GetAll(EMPEnt);
            ddlTeacherH.DataTextField = "FULLNAME";
            ddlTeacherH.DataValueField = "EMPLOYEEID";
            ddlTeacherH.DataBind();
            ddlTeacherH.Items.Insert(0, "Select");
        }
    }

    protected void gridTeacherProgMap_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridTeacherProgMap.EditIndex = -1;
        loadGrid();
    }
    protected void gridTeacherProgMap_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridTeacherProgMap.EditIndex = e.NewEditIndex;
        loadGrid();
    }

    protected void gridTeacherProgMap_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gridTeacherProgMap.Rows[e.RowIndex];
        Label lblPKIDE = (Label)row.FindControl("lblPKIDE");

        DropDownList ddlTeacherE = (DropDownList)row.FindControl("ddlTeacherE");
        DropDownList ddlStatusE = (DropDownList)row.FindControl("ddlStatusE");

        TPMEnt = new TEACHERPROGRAMMAPPING();
        TPMEnt.PK_ID = lblPKIDE.Text;
        TPMEnt = (TEACHERPROGRAMMAPPING)TPMSer.GetSingle(TPMEnt);
        if (TPMEnt != null)
        {
            TPMEnt.PROGRAMID = ddlProgram.SelectedValue;
            TPMEnt.TEACHERID = ddlTeacherE.SelectedValue;
            TPMEnt.STATUS = ddlStatusE.SelectedValue;
            TPMSer.Update(TPMEnt);

            gridTeacherProgMap.EditIndex = -1;
            loadGrid();
        }
    }
}