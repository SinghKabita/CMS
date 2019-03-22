using Entity.Components;
using Entity.Framework;
using Service.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class attendance_reports_SemesterAttendance : System.Web.UI.Page
{
    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    BatchYear BTCEnt = new BatchYear();
    BatchYearService BTCSer = new BatchYearService();

    semester SMSEnt = new semester();
    semesterService SMSSer = new semesterService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    HelperFunction hf = new HelperFunction();

    EntityList theList = new EntityList();
    EntityList newList = new EntityList();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFaculty();
            LoadLevel();
            LoadProgram();
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
    }

    protected void LoadLevel()
    {
        LEnt = new HSS_LEVEL();
        ddlLevel.DataSource = LSrv.GetAll(LEnt);
        ddlLevel.DataTextField = "LEVEL_NAME";
        ddlLevel.DataValueField = "LEVEL_NAME";
        ddlLevel.DataBind();
    }


    protected void LoadProgram()
    {
        PEnt = new program();
        PEnt.FACULTY_ID = ddlFaculty.SelectedValue;
        PEnt.PROGRAM_LEVEL = ddlLevel.SelectedValue;
        ddlProgram.DataSource = PSer.GetAll(PEnt);
        ddlProgram.DataTextField = "PROGRAM_CODE";
        ddlProgram.DataValueField = "PK_ID";
        ddlProgram.DataBind();
        ddlProgram.Items.Insert(0, "Select");
        //ddlBatch.Items.Insert(0, "Select");
        //ddlSemester.Items.Insert(0, "Select");

    }

    protected void LoadBatch()
    {
        BTCEnt = new BatchYear();
        BTCEnt.ACTIVE = "1";
        BTCEnt.SEMESTER = ddlSemester.SelectedValue;
        ddlBatch.DataSource = BTCSer.GetAll(BTCEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
        //ddlBatch.Items.Insert(0, "Select");
    }

    protected void LoadSemester()
    {
        theList = new EntityList();
        EntityList semList = new EntityList();
        BTCEnt = new BatchYear();
        BTCEnt.ACTIVE = "1";
        BTCEnt.PROGRAM = ddlProgram.SelectedValue;
        theList = BTCSer.GetAll(BTCEnt);
        #region to get the active Semester
        foreach (BatchYear by in theList)
        {
            SMSEnt = new semester();
            SMSEnt.PK_ID = by.SEMESTER;
            SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
            if (SMSEnt != null)
            {
                semList.Add(SMSEnt);
            }
        }
        #endregion

        ddlSemester.DataSource = semList;
        ddlSemester.DataTextField = "SEMESTER_CODE";
        ddlSemester.DataValueField = "PK_ID";
        ddlSemester.DataBind();
        ddlSemester.Items.Insert(0, "Select");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        gridSemesterAttendance.DataSource = hf.getSemesterAttendance(ddlBatch.SelectedValue, ddlSemester.SelectedValue,"");
        gridSemesterAttendance.DataBind();
    }
    protected void gridSemesterAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTotalPresent = e.Row.FindControl("lblTotalPresent") as Label;
            Label lblTotalDays = e.Row.FindControl("lblTotalDays") as Label;
            Label lblPresentPercent = e.Row.FindControl("lblPresentPercent") as Label;
            Label lblMarks = e.Row.FindControl("lblMarks") as Label;

            lblPresentPercent.Text = (Convert.ToDouble(lblTotalPresent.Text) / Convert.ToDouble(lblTotalDays.Text) * 100).ToString("#0.00");
            lblMarks.Text = (Convert.ToDouble(lblTotalPresent.Text) / Convert.ToDouble(lblTotalDays.Text) * 5).ToString("#0.00");
        }
    }



    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProgram.SelectedValue != "Select")
        {
            LoadSemester();

        }
        //else
        //{
        //    ddlBatch.Items.Clear();
        //    ddlBatch.Items.Insert(0, "Select");

        //    ddlSemester.Items.Clear();
        //    ddlSemester.Items.Insert(0, "Select");
        //}
        //if (ddlBatch.SelectedValue == "Select")
        //{
        //    ddlSemester.Items.Clear();
        //    ddlSemester.Items.Insert(0, "Select");
        //}
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

            ddlBatch.Items.Clear();
            ddlBatch.Items.Insert(0, "Select");

            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Select");
        }

    }


    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBatch();
    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }
}