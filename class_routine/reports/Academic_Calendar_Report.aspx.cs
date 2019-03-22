using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;
using Entity.Framework;

public partial class class_routine_reports_Academic_Calendar_Report : System.Web.UI.Page
{
    hss_faculty FEnt = new hss_faculty();
    hss_facultyService FSer = new hss_facultyService();

    HSS_LEVEL LVLEnt = new HSS_LEVEL();
    HSS_LEVELService LVLSer = new HSS_LEVELService();

    program PEnt = new program();
    programService PSer = new programService();

    Months MEnt = new Months();
    MonthsService MSer = new MonthsService();

    BatchYear BEnt = new BatchYear();
    BatchYearService BSer = new BatchYearService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    ACADEMIC_CALENDAR ACDEnt = new ACADEMIC_CALENDAR();
    ACADEMIC_CALENDARService ACDSer = new ACADEMIC_CALENDARService();

    HelperFunction hf = new HelperFunction();

    double workingdays = 0;

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
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadProgram();
    }

    protected void loadLevel()
    {
        LVLEnt = new HSS_LEVEL();
        ddlLevel.DataSource = LVLSer.GetAll(LVLEnt);
        ddlLevel.DataTextField = "LEVEL_NAME";
        ddlLevel.DataValueField = "LEVEL_NAME";
        ddlLevel.DataBind();
        ddlFaculty.Items.Insert(0, "Select");
    }

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadProgram();
    }

    protected void loadProgram()
    {
        PEnt = new program();
        PEnt.FACULTY_ID = ddlFaculty.SelectedValue;
        PEnt.PROGRAM_LEVEL = ddlLevel.SelectedValue;
        ddlProgram.DataSource = PSer.GetAll(PEnt);
        ddlProgram.DataTextField = "PROGRAM_CODE";
        ddlProgram.DataValueField = "PK_ID";
        ddlProgram.DataBind();
        ddlProgram.Items.Insert(0, "Select");
    }

    protected void LoadBatch()
    {
        BEnt = new BatchYear();
        BEnt.ACTIVE = "1";
        BEnt.PROGRAM = ddlProgram.SelectedValue;
        ddlBatch.DataSource = BSer.GetAll(BEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSemester();
        LoadBatch();
    }

    protected void LoadSemester()
    {
        EntityList theList = new EntityList();
        EntityList semList = new EntityList();
        BEnt = new BatchYear();
        BEnt.ACTIVE = "1";
        BEnt.PROGRAM = ddlProgram.SelectedValue;

        theList = BSer.GetAll(BEnt);
        if (theList.Count > 0)
        {
            #region to get the active Semester
            foreach (BatchYear by in theList)
            {
                SMEnt = new semester();
                SMEnt.PK_ID = by.SEMESTER;
                SMEnt = (semester)SMSer.GetSingle(SMEnt);
                if (SMEnt != null)
                {
                    semList.Add(SMEnt);
                }
            }
            #endregion

            ddlSemester.DataSource = semList;
            ddlSemester.DataTextField = "SEMESTER_CODE";
            ddlSemester.DataValueField = "PK_ID";
            ddlSemester.DataBind();
        }

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        ACDEnt = new ACADEMIC_CALENDAR();
        ACDEnt.BATCH = ddlBatch.SelectedValue;
        ACDEnt.SEMESTER = ddlSemester.SelectedValue;
        ACDEnt.PROGRAMID = ddlProgram.SelectedValue;
        gridAcademicCalendar.DataSource = ACDSer.GetAll(ACDEnt);
        gridAcademicCalendar.DataBind();
    }

    protected void gridAcademicCalendar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblMonth = e.Row.FindControl("lblMonth") as Label;
            Label lblMonthName = e.Row.FindControl("lblMonthName") as Label;
            Label lblDays = e.Row.FindControl("lblDays") as Label;
            Label lblWorkingDays = e.Row.FindControl("lblWorkingDays") as Label;

            if (lblWorkingDays.Text == "0")
            {
                e.Row.BackColor = System.Drawing.Color.Coral;
            }

            MEnt = new Months();
            MEnt.MONTHID = lblMonth.Text;
            MEnt = (Months)MSer.GetSingle(MEnt);
            if (MEnt != null)
            {
                lblMonthName.Text = MEnt.MONTHNAME.ToUpper();

                string month = "";
                foreach (char c in lblMonthName.Text)
                {

                    month += c + "<br>";
                }
                lblMonthName.Text = month;
            }


            workingdays = workingdays + Convert.ToDouble(lblWorkingDays.Text);


            lblDays.Text = workingdays.ToString();

        }


        for (int rowIndex = gridAcademicCalendar.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow gvRow = gridAcademicCalendar.Rows[rowIndex];
            GridViewRow gvPreviousRow = gridAcademicCalendar.Rows[rowIndex + 1];
            if (((Label)gvRow.Cells[1].FindControl("lblMonth")).Text == ((Label)gvPreviousRow.Cells[0].FindControl("lblMonth")).Text)
            {
                if (gvPreviousRow.Cells[0].RowSpan < 2)
                {
                    gvRow.Cells[0].RowSpan = 2;

                }
                else
                {
                    gvRow.Cells[0].RowSpan = gvPreviousRow.Cells[0].RowSpan + 1;

                }

                gvRow.Cells[0].BackColor = System.Drawing.Color.AliceBlue;

                gvPreviousRow.Cells[0].Visible = false;
            }

        }
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        BEnt = new BatchYear();
        BEnt.SEMESTER = ddlSemester.SelectedValue;
        BEnt = (BatchYear)BSer.GetSingle(BEnt);
        if (BEnt != null)
        {
            ddlBatch.SelectedValue = BEnt.BATCH;
        }
    }
}