using Entity.Components;
using Entity.Framework;
using Service.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class administration_batch : System.Web.UI.Page
{
    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    BatchYear BTEnt = new BatchYear();
    BatchYearService BTSer = new BatchYearService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    ACTIVE_SEMESTER_BATCH ASBEnt = new ACTIVE_SEMESTER_BATCH();
    ACTIVE_SEMESTER_BATCHService ASBSrv = new ACTIVE_SEMESTER_BATCHService();

    semester SEnt = new semester();
    semesterService SSrv = new semesterService();

    SYLLABUS_YEAR SYEnt = new SYLLABUS_YEAR();
    SYLLABUS_YEARService SYSrv = new SYLLABUS_YEARService();

    HelperFunction hf = new HelperFunction();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFaculty();
            //LoadProgram();


        }
    }
    protected void LoadFaculty()
    {
        FCEnt = new hss_faculty();
        ddlFaculty.DataSource = FCSer.GetAll(FCEnt);
        ddlFaculty.DataTextField = "FACULTY";
        ddlFaculty.DataValueField = "PK_ID";
        ddlFaculty.DataBind();
        ddlFaculty.Items.Insert(0, "Select");
        ddlProgram.Items.Insert(0, "Select");
        //txtSyllabusYear.Text = "";

    }

    protected void LoadProgram()
    {
        PEnt = new program();
        PEnt.FACULTY_ID = ddlFaculty.SelectedValue;
        PEnt.PROGRAM_LEVEL = ddlLevel.SelectedItem.ToString();
        ddlProgram.DataSource = PSer.GetAll(PEnt);
        ddlProgram.DataTextField = "PROGRAM_CODE";
        ddlProgram.DataValueField = "PK_ID";
        ddlProgram.DataBind();
        ddlProgram.Items.Insert(0, "Select");
        //txtSyllabusYear.Text = "";

    }

    protected void LoadSyllabusYr()
    {
        SYEnt = new SYLLABUS_YEAR();
        SYEnt.PROGRAM = ddlProgram.SelectedValue;

        ddlSyllabusYr.DataSource = SYSrv.GetAll(SYEnt);
        ddlSyllabusYr.DataTextField = "YEAR";
        ddlSyllabusYr.DataValueField = "YEAR";
        ddlSyllabusYr.DataBind();
        ddlSyllabusYr.Items.Insert(0, "Select");

    }

    protected void loadLevel()
    {

        LEnt = new HSS_LEVEL();

        ddlLevel.DataSource = LSrv.GetAll(LEnt);
        ddlLevel.DataTextField = "LEVEL_NAME";
        ddlLevel.DataValueField = "PK_ID";
        ddlLevel.DataBind();
        ddlLevel.Items.Insert(0, "Select");

    }

    protected void LoadGrid()
    {

        BTEnt = new BatchYear();
        BTEnt.PROGRAM = ddlProgram.SelectedValue;
        BTEnt.SYLLABUS_YEAR = ddlSyllabusYr.SelectedValue;
        gridBatch.DataSource = BTSer.GetAll(BTEnt);
        gridBatch.DataBind();

        if (gridBatch.Rows.Count == 0)
        {
            BTEnt = new BatchYear();
            ArrayList a1 = new ArrayList();
            a1.Add(BTEnt);
            gridBatch.DataSource = a1;
            gridBatch.DataBind();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        GridViewRow row = gridBatch.HeaderRow;

        TextBox txtBatchH = (TextBox)row.FindControl("txtBatchH");

        TextBox txtSyllabusYearH = (TextBox)row.FindControl("txtSyllabusYearH");

        DropDownList ddlStatusH = (DropDownList)row.FindControl("ddlStatusH");
        DropDownList ddlSemesterH = (DropDownList)row.FindControl("ddlSemesterH");

        BTEnt = new BatchYear();
        BTEnt.BATCH = txtBatchH.Text;
        BTEnt.ACTIVE = ddlStatusH.SelectedValue;
        BTEnt.PROGRAM = ddlProgram.SelectedValue;
        BTEnt.SYLLABUS_YEAR = txtSyllabusYearH.Text;
        BTEnt.SEMESTER = ddlSemesterH.SelectedValue;

        BTSer.Insert(BTEnt);

        LoadGrid();
    }

    protected void gridBatch_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            Label lblStat = e.Row.FindControl("lblStat") as Label;
            Label lblStatus = e.Row.FindControl("lblStatus") as Label;


            if (lblStat.Text == "1")
            {
                lblStatus.Text = "Active";
            }
            else if (lblStat.Text == "E")
            {
                lblStatus.Text = "Inactive";
            }

        }

        if (e.Row.RowType == DataControlRowType.Header)
        {
            DropDownList ddlSemesterH = (DropDownList)e.Row.FindControl("ddlSemesterH");
            Label lblSemE = e.Row.FindControl("lblSemE") as Label;

            semester SEnt = new semester();
            SEnt.PROGRAM_ID = ddlProgram.SelectedValue;
            SEnt.SYLLABUS_YEAR = ddlSyllabusYr.SelectedValue;
            ddlSemesterH.DataSource = SSrv.GetAll(SEnt);
            ddlSemesterH.DataTextField = "SEMESTER_CODE";
            ddlSemesterH.DataValueField = "PK_ID";
            ddlSemesterH.DataBind();
        }


        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            Label lblSem = e.Row.FindControl("lblSem") as Label;
            Label lblSemester = e.Row.FindControl("lblSemester") as Label;
            DropDownList ddlSemesterE = e.Row.FindControl("ddlSemesterE") as DropDownList;


            semester SEnt = new semester();
            SEnt.PROGRAM_ID = ddlProgram.SelectedValue;
            SEnt.PK_ID = lblSem.Text;
            SEnt = (semester)SSrv.GetSingle(SEnt);
            if (SEnt != null)
            {
                lblSemester.Text = SEnt.SEMESTER_CODE;
            }
        }


        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) != 0)
        {
            Label lblStatE = e.Row.FindControl("lblStatE") as Label;
            DropDownList ddlStatusE = e.Row.FindControl("ddlStatusE") as DropDownList;

            Label lblSemE = e.Row.FindControl("lblSemE") as Label;

            DropDownList ddlSemesterE = e.Row.FindControl("ddlSemesterE") as DropDownList;

            ddlSemesterE.SelectedValue = lblSemE.Text;

            semester SEnt = new semester();
            SEnt.PROGRAM_ID = ddlProgram.SelectedValue;
            SEnt.SYLLABUS_YEAR = ddlSyllabusYr.SelectedValue;
            ddlSemesterE.DataSource = SSrv.GetAll(SEnt);
            ddlSemesterE.DataTextField = "SEMESTER_CODE";
            ddlSemesterE.DataValueField = "PK_ID";
            ddlSemesterE.DataBind();

            ddlStatusE.SelectedValue = lblStatE.Text;

        }
    }

    protected void gridBatch_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridBatch.EditIndex = e.NewEditIndex;
        LoadGrid();
    }
    protected void gridBatch_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gridBatch.Rows[e.RowIndex];
        Label lblPKIDE = (Label)row.FindControl("lblPKIDE");
        TextBox txtBatchE = (TextBox)row.FindControl("txtBatchE");

        DropDownList ddlStatusE = (DropDownList)row.FindControl("ddlStatusE");
        DropDownList ddlSemesterE = (DropDownList)row.FindControl("ddlSemesterE");


        BTEnt = new BatchYear();
        BTEnt.BATCHID = lblPKIDE.Text;
        BTEnt.BATCH = txtBatchE.Text;
        BTEnt.SYLLABUS_YEAR = ddlSyllabusYr.SelectedValue;
        BTEnt.PROGRAM = ddlProgram.SelectedValue;
        BTEnt.ACTIVE = ddlStatusE.SelectedValue;
        BTEnt.SEMESTER = ddlSemesterE.SelectedValue;

        BTSer.Update(BTEnt);
        gridBatch.EditIndex = -1;
        LoadGrid();
    }
    protected void gridBatch_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridBatch.EditIndex = -1;
        LoadGrid();
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadLevel();
        LoadProgram();

    }

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFaculty.SelectedValue != "Select")
        {
            LoadProgram();
        }
        else
        {
            ddlProgram.Items.Clear();
            ddlProgram.Items.Insert(0, "Select");


        }
        if (ddlProgram.SelectedValue == "Select")
        {

            gridBatch.DataSource = null;
            gridBatch.DataBind();
        }
    }



    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSyllabusYr();

    }

    protected void ddlSyllabusYr_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();
    }
}