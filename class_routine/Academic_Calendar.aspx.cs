using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;
using System.Globalization;
using DataHelper.Framework;
using Entity.Framework;

public partial class class_routine_Academic_Calendar : System.Web.UI.Page
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

    DistributedTransaction DT = new DistributedTransaction();

    EntityList theList = new EntityList();

    HelperFunction hf = new HelperFunction();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadFaculty();
            loadLevel();

            loadProgram();

            LoadMonth();
            txtYear.Text = hf.NepaliYear();
            ddlMonth.SelectedValue = hf.NepaliMonth();

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

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBatch();
        LoadSemester();
    }

    protected void LoadSemester()
    {
        theList = new EntityList();
        EntityList semList = new EntityList();
        BEnt = new BatchYear();
        BEnt.ACTIVE = "1";
        BEnt.PROGRAM = ddlProgram.SelectedValue;

        theList = BSer.GetAll(BEnt);
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

    protected void LoadMonth()
    {
        MEnt = new Months();
        ddlMonth.DataSource = MSer.GetAll(MEnt);
        ddlMonth.DataTextField = "MONTHNAME";
        ddlMonth.DataValueField = "MONTHID";
        ddlMonth.DataBind();

    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        if (ddlBatch.SelectedValue != "Select" && ddlProgram.SelectedValue != "Select")
        {
            DT = new DistributedTransaction();
            for (int i = 1; i <= 32; i++)
            {
                ACDEnt = new ACADEMIC_CALENDAR();
                ACDEnt.BATCH = ddlBatch.SelectedValue;
                ACDEnt.SEMESTER = ddlSemester.SelectedValue;
                ACDEnt.PROGRAMID = ddlProgram.SelectedValue;
                ACDEnt.CAL_MONTH = ddlMonth.SelectedValue;
                ACDEnt.CAL_YEAR = txtYear.Text;
                ACDEnt.CAL_DAY = i.ToString();
                ACDEnt = (ACADEMIC_CALENDAR)ACDSer.GetSingle(ACDEnt, DT);
                if (ACDEnt == null)
                {

                    try
                    {

                        DateTime english = DateTime.ParseExact(hf.ConvertNepaliTOEnglish(i.ToString(), ddlMonth.SelectedValue, txtYear.Text), "dd.MMM.yyyy", CultureInfo.InvariantCulture);

                        ACDEnt = new ACADEMIC_CALENDAR();
                        ACDEnt.PROGRAMID = ddlProgram.SelectedValue;
                        ACDEnt.BATCH = ddlBatch.SelectedValue;
                        ACDEnt.SEMESTER = ddlSemester.SelectedValue;
                        ACDEnt.CAL_MONTH = ddlMonth.SelectedValue;
                        ACDEnt.CAL_YEAR = txtYear.Text;
                        ACDEnt.CAL_DAY = i.ToString();
                        ACDEnt.CAL_DATE = hf.ConvertNepaliTOEnglish(i.ToString(), ddlMonth.SelectedValue, txtYear.Text);
                        // string[] engdate = hf.ConvertNepaliTOEnglish(i.ToString(), ddlMonth.SelectedValue, txtYear.Text).Split('.');

                        //  DateTime dayValue = new DateTime(Convert.ToInt32(engdate[2]), Convert.ToInt32(engdate[1]), Convert.ToInt32(engdate[0]));
                        ACDEnt.CAL_DAY_OF_WEEK = english.DayOfWeek.ToString();

                        if (english.DayOfWeek.ToString() == "Saturday")
                        {
                            ACDEnt.WORKING_DAY = "0";
                        }
                        else
                        {
                            ACDEnt.WORKING_DAY = "1";
                        }

                        ACDSer.Insert(ACDEnt, DT);

                    }
                    catch { }
                }
                else
                {
                    LoadGrid();

                }

            }

            if (DT.HAPPY == true)
            {
                DT.Commit();

            }
            else
            {
                DT.Abort();

            }
            DT.Dispose();

            LoadGrid();
        }
        else
        {
            HelperFunction.MsgBox(this, this.GetType(), "Please Select Batch and Semester");
        }
    }

    protected void LoadGrid()
    {
        ACDEnt = new ACADEMIC_CALENDAR();
        ACDEnt.BATCH = ddlBatch.SelectedValue;
        ACDEnt.SEMESTER = ddlSemester.SelectedValue;
        ACDEnt.CAL_YEAR = txtYear.Text;
        ACDEnt.CAL_MONTH = ddlMonth.SelectedValue;
        gridAcademicCalendar.DataSource = ACDSer.GetAll(ACDEnt);
        gridAcademicCalendar.DataBind();
    }


    protected void gridAcademicCalendar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblWorkingDay = e.Row.FindControl("lblWorkingDay") as Label;
            CheckBox chkWorkingDay = e.Row.FindControl("chkWorkingDay") as CheckBox;
            Label lblDay = e.Row.FindControl("lblDay") as Label;


            if (lblWorkingDay.Text == "1")
            {
                chkWorkingDay.Checked = true;
                e.Row.BackColor = System.Drawing.Color.White;
                e.Row.ForeColor = System.Drawing.Color.Black;
            }
            else
            {
                chkWorkingDay.Checked = false;
                e.Row.BackColor = System.Drawing.Color.Coral;
                e.Row.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DT = new DistributedTransaction();
        foreach (GridViewRow gr in gridAcademicCalendar.Rows)
        {
            Label lblDate = gr.FindControl("lblDate") as Label;
            CheckBox chkWorkingDay = gr.FindControl("chkWorkingDay") as CheckBox;
            TextBox txtRemarks = gr.FindControl("txtRemarks") as TextBox;

            ACDEnt = new ACADEMIC_CALENDAR();
            ACDEnt.PROGRAMID = ddlProgram.SelectedValue;
            ACDEnt.BATCH = ddlBatch.SelectedValue;
            ACDEnt.SEMESTER = ddlSemester.SelectedValue;
            ACDEnt.CAL_YEAR = txtYear.Text;
            ACDEnt.CAL_MONTH = ddlMonth.SelectedValue;
            ACDEnt.CAL_DAY = lblDate.Text;
            ACDEnt = (ACADEMIC_CALENDAR)ACDSer.GetSingle(ACDEnt, DT);
            if (ACDEnt != null)
            {
                if (chkWorkingDay.Checked == true)
                {
                    ACDEnt.WORKING_DAY = "1";
                }
                else
                {
                    ACDEnt.WORKING_DAY = "0";
                }
                ACDEnt.REMARKS = txtRemarks.Text;
                ACDEnt.SEMESTER = ddlSemester.SelectedValue;
                ACDEnt.BATCH = ddlBatch.SelectedValue;
                ACDEnt.PROGRAMID = ddlProgram.SelectedValue;
                ACDSer.Update(ACDEnt, DT);
            }


        }

        if (DT.HAPPY == true)
        {
            DT.Commit();
            HelperFunction.MsgBox(this, this.GetType(), "Successfully Updated");
        }
        else
        {
            DT.Abort();
            HelperFunction.MsgBox(this, this.GetType(), "Something goes wrong. Please Try Again");
        }
        DT.Dispose();
    }
    protected void chkWorkingDay_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow gr = ((CheckBox)sender).Parent.Parent as GridViewRow;
        CheckBox chkWorkingDay = gr.FindControl("chkWorkingDay") as CheckBox;
        TextBox txtRemarks = gr.FindControl("txtRemarks") as TextBox;



        if (chkWorkingDay.Checked == true)
        {
            gr.BackColor = System.Drawing.Color.White;
            gr.ForeColor = System.Drawing.Color.Black;
        }
        else
        {
            gr.BackColor = System.Drawing.Color.Coral;
            gr.ForeColor = System.Drawing.Color.Red;
        }

        txtRemarks.Focus();
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