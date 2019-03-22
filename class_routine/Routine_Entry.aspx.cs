using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;
using DataHelper.Framework;
using System.Globalization;
using Entity.Framework;
using System.Collections;

public partial class class_routine_Routine_Entry : System.Web.UI.Page
{
    HSS_LEVEL LVLEnt = new HSS_LEVEL();
    HSS_LEVELService LVLSer = new HSS_LEVELService();

    BatchYear BTCEnt = new BatchYear();
    BatchYearService BTCSer = new BatchYearService();

    semester SMSEnt = new semester();
    semesterService SMSSer = new semesterService();

    Section SCEnt = new Section();
    SectionService SCSer = new SectionService();

    HSS_SUBJECT SUBEnt = new HSS_SUBJECT();
    HSS_SUBJECTService SUBSer = new HSS_SUBJECTService();

    TEACHER_SUBJECT_MAPPING TSUBEnt = new TEACHER_SUBJECT_MAPPING();
    TEACHER_SUBJECT_MAPPINGService TSUBSer = new TEACHER_SUBJECT_MAPPINGService();

    Employees EMPEnt = new Employees();
    EmployeesService EMPSer = new EmployeesService();

    PERIOD PRDEnt = new PERIOD();
    PERIODService PRDSer = new PERIODService();

    ROUTINE RTEnt = new ROUTINE();
    ROUTINEService RTSer = new ROUTINEService();

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    HelperFunction hf = new HelperFunction();

    EntityList theList = new EntityList();

    CLASS_PROGRESS_SCHEDULE CPSEnt = new CLASS_PROGRESS_SCHEDULE();
    CLASS_PROGRESS_SCHEDULEService CPSSer = new CLASS_PROGRESS_SCHEDULEService();

    ACADEMIC_CALENDAR ACEnt = new ACADEMIC_CALENDAR();
    ACADEMIC_CALENDARService ACSer = new ACADEMIC_CALENDARService();

    string routineID = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadLevel();
            LoadFaculty();
            LoadBatch();
            LoadSemester();
            LoadSection();
        }
    }

    protected string GetTeacherName(string empid)
    {
        string teachername = "";
        EMPEnt = new Employees();
        EMPEnt.EMPLOYEEID = empid;
        EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
        if (EMPEnt != null)
        {
            teachername = EMPEnt.FIRSTNAME + " " + EMPEnt.LASTNAME + " (" + EMPEnt.Abbrevation + ")";
        }
        return teachername;
    }

    protected void loadLevel()
    {
        LVLEnt = new HSS_LEVEL();
        ddlLevel.DataSource = LVLSer.GetAll(LVLEnt);
        ddlLevel.DataTextField = "LEVEL_NAME";
        ddlLevel.DataValueField = "LEVEL_NAME";
        ddlLevel.DataBind();
    }

    protected void LoadPeriod()
    {
        EntityList thelist = new EntityList();
        EntityList newlist = new EntityList();
        PRDEnt = new PERIOD();

        PRDEnt.PROGRAM_ID = ddlProgram.SelectedValue;
        PRDEnt.SEMESTER_ID = ddlSemester.SelectedValue;
        PRDEnt.SECTION_ID = ddlSection.SelectedValue;
        thelist = PRDSer.GetAll(PRDEnt);
        foreach (PERIOD prd in thelist)
        {
            if (prd.PERIODS != "Break")
            {
                newlist.Add(prd);
            }
        }
        ddlPeriod.DataSource = newlist;
        ddlPeriod.DataTextField = "PERIODS";
        ddlPeriod.DataValueField = "PK_ID";
        ddlPeriod.DataBind();

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
        ddlBatch.Items.Insert(0, "Select");
        ddlSemester.Items.Insert(0, "Select");
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
        ddlBatch.Items.Insert(0, "Select");
        ddlSemester.Items.Insert(0, "Select");
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

    protected void LoadSection()
    {
        SCEnt = new Section();
        ddlSection.DataSource = SCSer.GetAll(SCEnt);
        ddlSection.DataTextField = "SECTION";
        ddlSection.DataValueField = "SECTION";
        ddlSection.DataBind();
        ddlSection.Items.Insert(0, "Select");
    }

    protected void LoadSubject()
    {
        BTCEnt = new BatchYear();
        BTCEnt.BATCH = ddlBatch.SelectedValue;
        BTCEnt.PROGRAM = ddlProgram.SelectedValue;
        BTCEnt = (BatchYear)BTCSer.GetSingle(BTCEnt);
        if (BTCEnt != null)
        {
            SUBEnt = new HSS_SUBJECT();
            SUBEnt.YEAR = BTCEnt.SYLLABUS_YEAR;
            SUBEnt.SEMESTER = ddlSemester.SelectedValue;
            ddlSubject.DataSource = SUBSer.GetAll(SUBEnt);
            ddlSubject.DataTextField = "SUBJECT_NAME";
            ddlSubject.DataValueField = "PK_ID";
            ddlSubject.DataBind();
            ddlSubject.Items.Insert(0, "Select");

        }
    }

    protected void loadTeacher()
    {
        EntityList theList = new EntityList();

        TSUBEnt = new TEACHER_SUBJECT_MAPPING();
        TSUBEnt.BATCH = ddlBatch.SelectedValue;
        TSUBEnt.SEMESTER = ddlSemester.SelectedValue;
        TSUBEnt.SUBJECT_ID = ddlSubject.SelectedValue;
        theList = TSUBSer.GetAll(TSUBEnt);

        ddlTeacher.Items.Clear();
        foreach (TEACHER_SUBJECT_MAPPING tsm in theList)
        {

            ddlTeacher.Items.Add(new ListItem(GetTeacherName(tsm.TEACHER_ID), tsm.TEACHER_ID));
        }
    }

    protected void ddlSubject_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadTeacher();
        loadTeacherLab();

    }

    protected void loadTeacherLab()
    {
        EntityList theList = new EntityList();
        TSUBEnt = new TEACHER_SUBJECT_MAPPING();
        TSUBEnt.BATCH = ddlBatch.SelectedValue;
        TSUBEnt.SEMESTER = ddlSemester.SelectedValue;
        TSUBEnt.SUBJECT_ID = ddlSubject.SelectedValue;
        theList = TSUBSer.GetAll(TSUBEnt);

        ddlAssistTeacher.Items.Clear();
        foreach (TEACHER_SUBJECT_MAPPING tsm in theList)
        {

            ddlAssistTeacher.Items.Add(new ListItem(GetTeacherName(tsm.TEACHER_ID), tsm.TEACHER_ID));
        }
        ddlAssistTeacher.Items.Insert(0, "Select");
    }



    protected void btnReset_Click(object sender, EventArgs e)
    {
        //clearFields();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "popp();", true);
        divupdate.Visible = true;
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        DistributedTransaction DT = new DistributedTransaction();
        try
        {
            RTEnt = new ROUTINE();
            RTEnt.ROUTINE_DATE = txtDate.Text;
            string[] nepdate = hf.ConvertEnglishToNepali(txtDate.Text);
            RTEnt.ROUTINE_DAY = nepdate[0];
            RTEnt.ROUTINE_MONTH = nepdate[1];
            RTEnt.ROUTINE_YEAR = nepdate[2];

            RTEnt.BATCH = ddlBatch.SelectedValue;
            RTEnt.SEMESTER = ddlSemester.SelectedValue;
            RTEnt.SECTION = ddlSection.SelectedValue;
            RTEnt.PERIOD_ID = ddlPeriod.SelectedValue;
            RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
            if (RTEnt != null)
            {
                RTEnt.PERIOD_ID = ddlPeriod.SelectedValue;
                RTEnt.SUBJECT_ID = ddlSubject.SelectedValue;
                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                RTEnt.LAB_THEORY = ddlLabTheory.SelectedValue;
                if (ddlLabTheory.SelectedValue == "Lab")
                {
                    RTEnt.LAB_NO = txtLabNo.Text;
                    if (ddlAssistTeacher.SelectedValue != "Select")
                        RTEnt.ASSIST_TEACH_ID = ddlAssistTeacher.SelectedValue;
                    else
                        RTEnt.ASSIST_TEACH_ID = "";
                }

                else
                {
                    RTEnt.LAB_NO = txtClassNo.Text;
                    RTEnt.ASSIST_TEACH_ID = "";
                }

                RTEnt.STATUS = "1";
                RTSer.Update(RTEnt, DT);
            }

            CPSEnt = new CLASS_PROGRESS_SCHEDULE();
            CPSEnt.CLASS_DATE = txtDate.Text;
            string[] nepdate2 = hf.ConvertEnglishToNepali(txtDate.Text);
            CPSEnt.CLASS_DAY = nepdate2[0];
            CPSEnt.CLASS_MONTH = nepdate2[1];
            CPSEnt.CLASS_YEAR = nepdate2[2];
            DateTime date = DateTime.ParseExact(txtDate.Text, "dd.MMM.yyyy", CultureInfo.InvariantCulture);

            CPSEnt.CLASS_DAY_OF_WEEK = date.DayOfWeek.ToString().Substring(0, 3).ToUpper();
            CPSEnt.BATCH = ddlBatch.SelectedValue;
            CPSEnt.SEMESTER = ddlSemester.SelectedValue;
            CPSEnt.SECTION = ddlSection.SelectedValue;
            CPSEnt.LAB_ASSIST = "0";
            string[] prevsubject = lblPreviousSubject.Text.Split('-');
            SUBEnt = new HSS_SUBJECT();
            SUBEnt.SUBJECT_CODE = prevsubject[0];
            SUBEnt.SEMESTER = ddlSemester.SelectedValue;
            SUBEnt.STATUS = "1";
            SUBEnt.YEAR = hf.getSyllabusYear(ddlProgram.SelectedValue, hf.NepaliYear());
            SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt, DT);
            if (SUBEnt != null)
            {
                CPSEnt.SUBJECT_ID = SUBEnt.PK_ID;
            }

            CPSEnt = (CLASS_PROGRESS_SCHEDULE)CPSSer.GetSingle(CPSEnt, DT);
            if (CPSEnt != null)
            {
                #region to update main lab teacher
                CPSEnt.SUBJECT_ID = ddlSubject.SelectedValue;
                CPSEnt.CLASS_HOUR = "1";
                #region to get class hour from period
                PRDEnt = new PERIOD();
                PRDEnt.PROGRAM_ID = ddlProgram.SelectedValue;
                PRDEnt.SEMESTER_ID = ddlSemester.SelectedValue;
                PRDEnt.SECTION_ID = ddlSection.SelectedValue;
                PRDEnt.PK_ID = ddlPeriod.SelectedValue;
                PRDEnt = (PERIOD)PRDSer.GetSingle(PRDEnt);
                if (PRDEnt != null)
                {
                    CPSEnt.CLASS_HOUR = PRDEnt.CLASS_HOUR;
                }

                #endregion

                SUBEnt = new HSS_SUBJECT();
                SUBEnt.PK_ID = ddlSubject.SelectedValue;
                SUBEnt.STATUS = "1";
                SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt, DT);
                if (SUBEnt != null)
                {
                    CPSEnt.TOTAL_CLASS_HOUR = SUBEnt.CLASS_HOUR;
                }

                CPSEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                CPSEnt.REMARKS = txtRemarks.Text;
                CPSEnt.LAB_THEORY = ddlLabTheory.SelectedValue;
                CPSEnt.LAB_ASSIST = "0";
                CPSSer.Update(CPSEnt, DT);
                #endregion

            }

            if (ddlLabTheory.SelectedValue == "Theory")
            {
                CPSEnt = new CLASS_PROGRESS_SCHEDULE();
                CPSEnt.CLASS_DATE = txtDate.Text;
                nepdate2 = hf.ConvertEnglishToNepali(txtDate.Text);
                CPSEnt.CLASS_DAY = nepdate2[0];
                CPSEnt.CLASS_MONTH = nepdate2[1];
                CPSEnt.CLASS_YEAR = nepdate2[2];
                date = DateTime.ParseExact(txtDate.Text, "dd.MMM.yyyy", CultureInfo.InvariantCulture);

                CPSEnt.CLASS_DAY_OF_WEEK = date.DayOfWeek.ToString().Substring(0, 3).ToUpper();
                CPSEnt.BATCH = ddlBatch.SelectedValue;
                CPSEnt.SEMESTER = ddlSemester.SelectedValue;
                CPSEnt.SECTION = ddlSection.SelectedValue;
                CPSEnt.LAB_ASSIST = "1";
                prevsubject = lblPreviousSubject.Text.Split('-');
                SUBEnt = new HSS_SUBJECT();
                SUBEnt.SUBJECT_CODE = prevsubject[0];
                SUBEnt.STATUS = "1";
                SUBEnt.SEMESTER = ddlSemester.SelectedValue;
                SUBEnt.YEAR = hf.getSyllabusYear(ddlProgram.SelectedValue, hf.NepaliYear());
                SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt, DT);
                if (SUBEnt != null)
                {
                    CPSEnt.SUBJECT_ID = SUBEnt.PK_ID;
                }

                CPSEnt = (CLASS_PROGRESS_SCHEDULE)CPSSer.GetSingle(CPSEnt, DT);
                if (CPSEnt != null)
                {
                    CPSEnt.CLASS_HOUR = "0";
                    CPSSer.Update(CPSEnt, DT);
                }
            }

            #region to update Lab Assistant in CLASS_PROGRESS_SCHEDULE / insert if not inserted
            else if (ddlLabTheory.SelectedValue == "Lab" && ddlAssistTeacher.SelectedValue != "Select")
            {
                CLASS_PROGRESS_SCHEDULE CPSEnt2 = new CLASS_PROGRESS_SCHEDULE();
                CPSEnt2.CLASS_DATE = txtDate.Text;
                string[] nepdate3 = hf.ConvertEnglishToNepali(txtDate.Text);
                CPSEnt2.CLASS_DAY = nepdate3[0];
                CPSEnt2.CLASS_MONTH = nepdate3[1];
                CPSEnt2.CLASS_YEAR = nepdate3[2];
                DateTime date2 = DateTime.ParseExact(txtDate.Text, "dd.MMM.yyyy", CultureInfo.InvariantCulture);

                CPSEnt2.CLASS_DAY_OF_WEEK = date2.DayOfWeek.ToString().Substring(0, 3).ToUpper();
                CPSEnt2.BATCH = ddlBatch.SelectedValue;
                CPSEnt2.SEMESTER = ddlSemester.SelectedValue;
                CPSEnt2.SECTION = ddlSection.SelectedValue;

                string[] prevsubject1 = lblPreviousSubject.Text.Split('-');
                SUBEnt = new HSS_SUBJECT();
                SUBEnt.SUBJECT_CODE = prevsubject1[0];
                SUBEnt.STATUS = "1";
                SUBEnt.SEMESTER = ddlSemester.SelectedValue;
                SUBEnt.YEAR = hf.getSyllabusYear(ddlProgram.SelectedValue, hf.NepaliYear());
                SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt, DT);
                if (SUBEnt != null)
                {
                    CPSEnt2.SUBJECT_ID = SUBEnt.PK_ID;
                }
                CPSEnt2.LAB_ASSIST = "1";
                CPSEnt2 = (CLASS_PROGRESS_SCHEDULE)CPSSer.GetSingle(CPSEnt2, DT);
                if (CPSEnt2 != null)
                {
                    CPSEnt2.SUBJECT_ID = ddlSubject.SelectedValue;
                    if (ddlAssistTeacher.SelectedValue != "Select")
                        CPSEnt2.CLASS_HOUR = "1";
                    else
                        CPSEnt2.CLASS_HOUR = "0";
                    #region to get class hour from period
                    PRDEnt = new PERIOD();
                    PRDEnt.PROGRAM_ID = ddlProgram.SelectedValue;
                    PRDEnt.SEMESTER_ID = ddlSemester.SelectedValue;
                    PRDEnt.SECTION_ID = ddlSection.SelectedValue;
                    PRDEnt.PK_ID = ddlPeriod.SelectedValue;
                    PRDEnt = (PERIOD)PRDSer.GetSingle(PRDEnt);
                    if (PRDEnt != null)
                    {
                        CPSEnt2.CLASS_HOUR = PRDEnt.CLASS_HOUR;
                    }

                    #endregion

                    SUBEnt = new HSS_SUBJECT();
                    SUBEnt.PK_ID = ddlSubject.SelectedValue;
                    SUBEnt.STATUS = "1";
                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt, DT);
                    if (SUBEnt != null)
                    {
                        CPSEnt2.TOTAL_CLASS_HOUR = SUBEnt.CLASS_HOUR;
                    }

                    CPSEnt2.TEACHER_ID = ddlAssistTeacher.SelectedValue;
                    CPSEnt2.REMARKS = txtRemarks.Text;
                    CPSEnt2.LAB_THEORY = ddlLabTheory.SelectedValue;
                    CPSEnt2.LAB_ASSIST = "1";
                    CPSSer.Update(CPSEnt2, DT);

                }
                else
                {
                    CPSEnt2 = new CLASS_PROGRESS_SCHEDULE();
                    CPSEnt2.CLASS_DATE = txtDate.Text;
                    nepdate3 = hf.ConvertEnglishToNepali(txtDate.Text);
                    CPSEnt2.CLASS_DAY = nepdate3[0];
                    CPSEnt2.CLASS_MONTH = nepdate3[1];
                    CPSEnt2.CLASS_YEAR = nepdate3[2];
                    date2 = DateTime.ParseExact(txtDate.Text, "dd.MMM.yyyy", CultureInfo.InvariantCulture);

                    CPSEnt2.CLASS_DAY_OF_WEEK = date2.DayOfWeek.ToString().Substring(0, 3).ToUpper();
                    CPSEnt2.BATCH = ddlBatch.SelectedValue;
                    CPSEnt2.SEMESTER = ddlSemester.SelectedValue;
                    CPSEnt2.SECTION = ddlSection.SelectedValue;

                    CPSEnt2.SUBJECT_ID = ddlSubject.SelectedValue;
                    if (ddlAssistTeacher.SelectedValue != "Select")
                        CPSEnt2.CLASS_HOUR = "1";
                    else
                        CPSEnt2.CLASS_HOUR = "0";
                    #region to get class hour from period
                    PRDEnt = new PERIOD();
                    PRDEnt.PROGRAM_ID = ddlProgram.SelectedValue;
                    PRDEnt.SEMESTER_ID = ddlSemester.SelectedValue;
                    PRDEnt.SECTION_ID = ddlSection.SelectedValue;
                    PRDEnt.PK_ID = ddlPeriod.SelectedValue;
                    PRDEnt = (PERIOD)PRDSer.GetSingle(PRDEnt);
                    if (PRDEnt != null)
                    {
                        CPSEnt2.CLASS_HOUR = PRDEnt.CLASS_HOUR;
                    }

                    #endregion

                    SUBEnt = new HSS_SUBJECT();
                    SUBEnt.PK_ID = ddlSubject.SelectedValue;
                    SUBEnt.STATUS = "1";
                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt, DT);
                    if (SUBEnt != null)
                    {
                        CPSEnt2.TOTAL_CLASS_HOUR = SUBEnt.CLASS_HOUR;
                    }

                    CPSEnt2.TEACHER_ID = ddlAssistTeacher.SelectedValue;
                    CPSEnt2.REMARKS = txtRemarks.Text;
                    CPSEnt2.LAB_THEORY = ddlLabTheory.SelectedValue;
                    CPSEnt2.LAB_ASSIST = "1";
                    CPSSer.Insert(CPSEnt2, DT);
                }
            }

            #endregion

            if (DT.HAPPY == true)
            {
                DT.Commit();
                HelperFunction.MsgBox(this, this.GetType(), "Successfull");
                lblroutineIDe.Text = "";
                TRprd.Visible = false;
                trLabThr.Visible = false;
                trBtnSave.Visible = false;

            }
            else
            {
                DT.Abort();
                HelperFunction.MsgBox(this, this.GetType(), "Something Goes Wrong. Try Again Please");
                lblroutineIDe.Text = "";
                TRprd.Visible = false;
                trLabThr.Visible = false;
                trBtnSave.Visible = false;
            }
            DT.Dispose();

            LoadGrid();

        }
        catch
        {

        }
        divupdate.Visible = false;
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        if (ddlFaculty.SelectedValue == "Select" || ddlProgram.SelectedValue == "Select" || ddlSection.SelectedValue == "Select" || ddlSemester.SelectedValue == "Select")
        {
            txtDate.Text = "";
            lblDayD.Text = "";
            HelperFunction.MsgBox(this, this.GetType(), "Please enter all the required fields");
        }
        else
        {
            #region to check valid date
            DateTime dateTime;
            string toValidate = txtDate.Text;

            bool isStringValid = DateTime.TryParseExact(
                toValidate,
                "dd.MMM.yyyy",
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out dateTime);
            #endregion
            if (!isStringValid)
            {
                lblDayD.Text = "";
                txtDate.Text = "";
                HelperFunction.MsgBox(this, this.GetType(), "Invalid Date Format");
            }
            else
            {
                #region to hide the grid and edit portion
                gridRoutine.Visible = false;
                #endregion

                divupdate.Visible = false;
                ACEnt = new ACADEMIC_CALENDAR();
                ACEnt.CAL_DATE = txtDate.Text;
                ACEnt.SEMESTER = ddlSemester.Text;
                ACEnt.PROGRAMID = ddlProgram.Text;
                ACEnt = (ACADEMIC_CALENDAR)ACSer.GetSingle(ACEnt);
                if (ACEnt != null)
                {
                    lblDayD.Text = ACEnt.CAL_DAY_OF_WEEK;
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        HelperFunction.MsgBox(this, this.GetType(), txtDate.Text + " is Holiday");
                        btnSave.Enabled = false;
                    }
                }
                else
                {
                    lblDayD.Text = "";
                    HelperFunction.MsgBox(this, this.GetType(), txtDate.Text + " is Invalid");
                }
            }
        }
    }

    protected void txtPrevDate_TextChanged(object sender, EventArgs e)
    {
        #region to check valid date
        DateTime dateTime;
        string toValidate = txtPrevDate.Text;

        bool isStringValid = DateTime.TryParseExact(
            toValidate,
            "dd.MMM.yyyy",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out dateTime);
        #endregion
        if (!isStringValid)
        {
            lblPrevDay.Text = "";
            txtPrevDate.Text = "";
            HelperFunction.MsgBox(this, this.GetType(), "Invalid Date Format");
        }
        else
        {
            if (ddlFaculty.SelectedValue == "Select" || ddlProgram.SelectedValue == "Select" || ddlSection.SelectedValue == "Select" || ddlSemester.SelectedValue == "Select")
            {
                txtPrevDate.Text = "";
                lblPrevDay.Text = "";
                HelperFunction.MsgBox(this, this.GetType(), "Please enter all the required fields");
            }
            else
            {
                ACEnt = new ACADEMIC_CALENDAR();
                ACEnt.CAL_DATE = txtPrevDate.Text;
                ACEnt.SEMESTER = ddlSemester.Text;
                ACEnt.PROGRAMID = ddlProgram.Text;
                ACEnt = (ACADEMIC_CALENDAR)ACSer.GetSingle(ACEnt);
                if (ACEnt != null)
                {
                    lblPrevDay.Text = ACEnt.CAL_DAY_OF_WEEK;
                }
            }

        }
    }



    protected void LoadGrid()
    {
        PRDEnt = new PERIOD();
        PRDEnt.PROGRAM_ID = ddlProgram.SelectedValue;
        PRDEnt.SEMESTER_ID = ddlSemester.SelectedValue;
        PRDEnt.SECTION_ID = ddlSection.SelectedValue;

        gridRoutine.DataSource = PRDSer.GetAll(PRDEnt);
        gridRoutine.DataBind();

    }

    protected void gridRoutine_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string routineDATE = "";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblRoutine1 = e.Row.FindControl("lblRoutine1") as Label;


            Label lblDay = e.Row.FindControl("lblDay") as Label;
            Label lblPeriodId = e.Row.FindControl("lblPeriodId") as Label;
            Label lblPeriod = e.Row.FindControl("lblPeriod") as Label;

            Label lblRoutineID = e.Row.FindControl("lblRoutineID") as Label;



            if (lblPrevDayID.Text == "3")
            {
                routineDATE = txtPrevDate.Text;
                DateTime date = DateTime.ParseExact(txtPrevDate.Text, "dd.MMM.yyyy", CultureInfo.InvariantCulture);
                lblDay.Text = date.DayOfWeek.ToString().Substring(0, 3).ToUpper();
            }
            else
            {
                routineDATE = txtDate.Text;
                DateTime date = DateTime.ParseExact(txtDate.Text, "dd.MMM.yyyy", CultureInfo.InvariantCulture);
                lblDay.Text = date.DayOfWeek.ToString().Substring(0, 3).ToUpper();

            }

            EntityList theList = new EntityList();

            BTCEnt = new BatchYear();
            BTCEnt.BATCH = ddlBatch.SelectedValue;
            BTCEnt = (BatchYear)BTCSer.GetSingle(BTCEnt);
            if (BTCEnt != null)
            {
                RTEnt = new ROUTINE();
                RTEnt.ROUTINE_DATE = routineDATE;
                RTEnt.BATCH = ddlBatch.SelectedValue;
                RTEnt.SEMESTER = BTCEnt.SEMESTER;
                RTEnt.PERIOD_ID = lblPeriodId.Text;
                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                if (RTEnt != null)
                {

                    lblRoutineID.Text = RTEnt.PK_ID;

                    if (RTEnt.STATUS == "0")
                    {
                        lblRoutine1.Text = "Off";
                    }
                    else
                    {
                        SUBEnt = new HSS_SUBJECT();
                        SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                        SUBEnt.STATUS = "1";
                        SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                        if (SUBEnt != null)
                        {
                            lblRoutine1.Text = SUBEnt.SUBJECT_CODE;
                        }

                        EMPEnt = new Employees();
                        EMPEnt.EMPLOYEEID = RTEnt.TEACHER_ID;
                        EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
                        if (EMPEnt != null)
                        {
                            lblRoutine1.Text += "-" + EMPEnt.Abbrevation;
                        }
                        if (RTEnt.ASSIST_TEACH_ID != "")
                        {
                            EMPEnt = new Employees();
                            EMPEnt.EMPLOYEEID = RTEnt.ASSIST_TEACH_ID;
                            EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
                            if (EMPEnt != null)
                            {
                                lblRoutine1.Text += "/" + EMPEnt.Abbrevation;

                            }
                        }

                        if (RTEnt.LAB_THEORY == "Theory")
                        {
                            lblRoutine1.Text += "-TH";
                            if (RTEnt.LAB_NO != "")
                            {
                                lblRoutine1.Text += "-" + RTEnt.LAB_NO;
                            }
                        }
                        else
                        {
                            lblRoutine1.Text += "-LB";

                            if (RTEnt.LAB_NO != "")
                            {
                                lblRoutine1.Text += "-" + RTEnt.LAB_NO;
                            }
                        }
                    }

                }

            }

        }

    }

    bool ReturnValue()
    {
        return false;
    }

    protected void gridRoutine_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName.Equals("off"))
        {
            #region off button click
            DistributedTransaction DT = new DistributedTransaction();
            TRprd.Visible = false;
            trLabThr.Visible = false;
            trBtnSave.Visible = false;

            GridViewRow gr = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            Label lblPeriodId = gr.FindControl("lblPeriodId") as Label;
            Label lblRoutineID = gr.FindControl("lblRoutineID") as Label;
            Label lblRoutine1 = gr.FindControl("lblRoutine1") as Label;
            Button btnOff = gr.FindControl("btnOff") as Button;


            RTEnt = new ROUTINE();
            RTEnt.PK_ID = lblRoutineID.Text;
            RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
            if (RTEnt != null)
            {
                CPSEnt = new CLASS_PROGRESS_SCHEDULE();
                CPSEnt.TEACHER_ID = RTEnt.TEACHER_ID;
                CPSEnt.CLASS_DATE = txtDate.Text;
                string[] nepdate2 = hf.ConvertEnglishToNepali(txtDate.Text);
                CPSEnt.CLASS_DAY = nepdate2[0];
                CPSEnt.CLASS_MONTH = nepdate2[1];
                CPSEnt.CLASS_YEAR = nepdate2[2];
                DateTime date = DateTime.ParseExact(txtDate.Text, "dd.MMM.yyyy", CultureInfo.InvariantCulture);

                CPSEnt.CLASS_DAY_OF_WEEK = date.DayOfWeek.ToString().Substring(0, 3).ToUpper();
                CPSEnt.BATCH = ddlBatch.SelectedValue;
                CPSEnt.SEMESTER = ddlSemester.SelectedValue;
                CPSEnt.SECTION = ddlSection.SelectedValue;
                CPSEnt = (CLASS_PROGRESS_SCHEDULE)CPSSer.GetSingle(CPSEnt);
                if (CPSEnt != null)
                {
                    CPSEnt.CLASS_HOUR = "0";
                    CPSSer.Update(CPSEnt, DT);

                }

                CLASS_PROGRESS_SCHEDULE CPSEnt2 = new CLASS_PROGRESS_SCHEDULE();
                CPSEnt2.TEACHER_ID = RTEnt.ASSIST_TEACH_ID;
                CPSEnt2.CLASS_DATE = txtDate.Text;
                nepdate2 = hf.ConvertEnglishToNepali(txtDate.Text);
                CPSEnt2.CLASS_DAY = nepdate2[0];
                CPSEnt2.CLASS_MONTH = nepdate2[1];
                CPSEnt2.CLASS_YEAR = nepdate2[2];
                date = DateTime.ParseExact(txtDate.Text, "dd.MMM.yyyy", CultureInfo.InvariantCulture);

                CPSEnt2.CLASS_DAY_OF_WEEK = date.DayOfWeek.ToString().Substring(0, 3).ToUpper();
                CPSEnt2.BATCH = ddlBatch.SelectedValue;
                CPSEnt2.SEMESTER = ddlSemester.SelectedValue;
                CPSEnt2.SECTION = ddlSection.SelectedValue;
                CPSEnt2 = (CLASS_PROGRESS_SCHEDULE)CPSSer.GetSingle(CPSEnt2);
                if (CPSEnt2 != null)
                {
                    CPSEnt2.CLASS_HOUR = "0";
                    CPSSer.Update(CPSEnt2, DT);

                }


                RTEnt.TEACHER_ID = "";
                RTEnt.ASSIST_TEACH_ID = "";
                RTEnt.STATUS = "0";
                RTSer.Update(RTEnt, DT);


                if (DT.HAPPY == true)
                {
                    DT.Commit();
                    lblRoutine1.Text = "Off";
                }
                else
                {
                    DT.Abort();
                }
                DT.Dispose();

            }
            #endregion
        }
        if (e.CommandName.Equals("change"))
        {

            GridViewRow gr = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            Label lblPeriodId = gr.FindControl("lblPeriodId") as Label;
            Label lblRoutineID = gr.FindControl("lblRoutineID") as Label;

            if (lblRoutineID.Text == "")
            {
                TRprd.Visible = false;
                trLabThr.Visible = false;
                trBtnSave.Visible = false;
                HelperFunction.MsgBox(this, this.GetType(), "Please enter Routine of the following Period");
            }
            else
            {
                TRprd.Visible = true;
                trLabThr.Visible = true;
                trBtnSave.Visible = true;

                lblroutineIDe.Text = lblRoutineID.Text;

                RTEnt = new ROUTINE();
                RTEnt.PK_ID = lblRoutineID.Text;
                RTEnt.STATUS = "1";
                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                if (RTEnt != null)
                {
                    LoadPeriod();
                    ddlPeriod.SelectedValue = RTEnt.PERIOD_ID;
                    ddlSubject.SelectedValue = RTEnt.SUBJECT_ID;
                    loadTeacher();
                    ddlTeacher.SelectedValue = RTEnt.TEACHER_ID;
                    ddlLabTheory.SelectedValue = RTEnt.LAB_THEORY;
                    if (RTEnt.LAB_THEORY == "Theory")
                    {
                        lblClassNo.Visible = true;
                        txtClassNo.Visible = true;
                        lblLabNo.Visible = false;
                        txtLabNo.Visible = false;
                        lblAssistTeacher.Visible = false;
                        ddlAssistTeacher.Visible = false;
                        txtClassNo.Text = RTEnt.LAB_NO;
                    }
                    else
                    {
                        lblClassNo.Visible = false;
                        txtClassNo.Visible = false;
                        lblLabNo.Visible = true;
                        txtLabNo.Visible = true;
                        lblAssistTeacher.Visible = true;
                        ddlAssistTeacher.Visible = true;
                        txtLabNo.Text = RTEnt.LAB_NO;
                        loadTeacherLab();
                        if (RTEnt.ASSIST_TEACH_ID != "")
                            ddlAssistTeacher.SelectedValue = RTEnt.ASSIST_TEACH_ID;
                    }
                }
                else
                {
                    RTEnt = new ROUTINE();
                    RTEnt.PK_ID = lblRoutineID.Text;
                    RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                    if (RTEnt != null)
                    {
                        ddlPeriod.SelectedValue = RTEnt.PERIOD_ID;
                        ddlSubject.SelectedIndex = 0;
                        //ddlTeacher.SelectedItem.Text = "";
                    }
                }
            }
        }
    }

    protected void loadGridRoutinePrevDay()
    {
        PRDEnt = new PERIOD();
        PRDEnt.PROGRAM_ID = ddlProgram.SelectedValue;
        PRDEnt.SEMESTER_ID = ddlSemester.SelectedValue;
        PRDEnt.SECTION_ID = ddlSection.SelectedValue;
        gridRoutinePrevDay.DataSource = PRDSer.GetAll(PRDEnt);
        gridRoutinePrevDay.DataBind();
    }

    protected void gridRoutinePrevDay_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string routineDATE = "";
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblRoutine1 = e.Row.FindControl("lblRoutine1") as Label;


            Label lblDay = e.Row.FindControl("lblDay") as Label;
            Label lblPeriodId = e.Row.FindControl("lblPeriodId") as Label;
            Label lblPeriod = e.Row.FindControl("lblPeriod") as Label;

            Label lblRoutineID = e.Row.FindControl("lblRoutineID") as Label;



            if (lblPrevDayID.Text == "3")
            {
                routineDATE = txtPrevDate.Text;
                DateTime date = DateTime.ParseExact(txtPrevDate.Text, "dd.MMM.yyyy", CultureInfo.InvariantCulture);
                lblDay.Text = date.DayOfWeek.ToString().Substring(0, 3).ToUpper();
            }
            else
            {
                routineDATE = txtDate.Text;
                DateTime date = DateTime.ParseExact(txtDate.Text, "dd.MMM.yyyy", CultureInfo.InvariantCulture);
                lblDay.Text = date.DayOfWeek.ToString().Substring(0, 3).ToUpper();

            }

            EntityList theList = new EntityList();

            BTCEnt = new BatchYear();
            BTCEnt.BATCH = ddlBatch.SelectedValue;
            BTCEnt = (BatchYear)BTCSer.GetSingle(BTCEnt);
            if (BTCEnt != null)
            {
                RTEnt = new ROUTINE();
                RTEnt.ROUTINE_DATE = routineDATE;
                RTEnt.BATCH = ddlBatch.SelectedValue;
                RTEnt.SEMESTER = BTCEnt.SEMESTER;
                RTEnt.PERIOD_ID = lblPeriodId.Text;
                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                if (RTEnt != null)
                {

                    lblRoutineID.Text = RTEnt.PK_ID;

                    if (RTEnt.STATUS == "0")
                    {
                        lblRoutine1.Text = "Off";
                    }
                    else
                    {
                        SUBEnt = new HSS_SUBJECT();
                        SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                        SUBEnt.STATUS = "1";
                        SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                        if (SUBEnt != null)
                        {
                            lblRoutine1.Text = SUBEnt.SUBJECT_CODE;
                        }

                        EMPEnt = new Employees();
                        EMPEnt.EMPLOYEEID = RTEnt.TEACHER_ID;
                        EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
                        if (EMPEnt != null)
                        {
                            lblRoutine1.Text += "-" + EMPEnt.Abbrevation;
                        }
                        if (RTEnt.ASSIST_TEACH_ID != "")
                        {
                            EMPEnt = new Employees();
                            EMPEnt.EMPLOYEEID = RTEnt.ASSIST_TEACH_ID;
                            EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
                            if (EMPEnt != null)
                            {
                                lblRoutine1.Text += "/" + EMPEnt.Abbrevation;

                            }
                        }

                        if (RTEnt.LAB_THEORY == "Theory")
                        {
                            lblRoutine1.Text += "-TH";
                            if (RTEnt.LAB_NO != "")
                            {
                                lblRoutine1.Text += "-" + RTEnt.LAB_NO;
                            }
                        }
                        else
                        {
                            lblRoutine1.Text += "-LB";

                            if (RTEnt.LAB_NO != "")
                            {
                                lblRoutine1.Text += "-" + RTEnt.LAB_NO;
                            }
                        }
                    }

                }

            }

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string progCode = "";
        string semCode = "";
        string sectionCode = "";
        if (txtDate.Text != "" && ddlSemester.SelectedValue != "Select" && ddlTeacher.SelectedValue != "Select")
        {
            DistributedTransaction DT = new DistributedTransaction();

            string periodTimeU = "";
            EntityList PeriodList = new EntityList();
            bool flagU = false;
            bool flag = false;

            RTEnt = new ROUTINE();
            RTEnt.ROUTINE_DATE = txtDate.Text;
            string[] nepdate = hf.ConvertEnglishToNepali(txtDate.Text);
            RTEnt.ROUTINE_DAY = nepdate[0];
            RTEnt.ROUTINE_MONTH = nepdate[1];
            RTEnt.ROUTINE_YEAR = nepdate[2];

            RTEnt.BATCH = ddlBatch.SelectedValue;
            RTEnt.SEMESTER = ddlSemester.SelectedValue;
            RTEnt.SECTION = ddlSection.SelectedValue;
            RTEnt.PERIOD_ID = ddlPeriod.SelectedValue;
            RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
            if (RTEnt != null)
            {
                routineID = RTEnt.PK_ID;

                #region to check if there is duplicate routine
                PRDEnt = new PERIOD();
                PRDEnt.PK_ID = ddlPeriod.SelectedValue;
                PRDEnt = (PERIOD)PRDSer.GetSingle(PRDEnt);
                if (PRDEnt != null)
                {
                    periodTimeU = PRDEnt.TIME;
                    PRDEnt = new PERIOD();
                    PRDEnt.TIME = periodTimeU;
                    PeriodList = PRDSer.GetAll(PRDEnt);
                    if (PeriodList.Count > 0)
                    {
                        foreach (PERIOD prd in PeriodList)
                        {
                            ROUTINE RTEntNew = new ROUTINE();
                            RTEntNew.PERIOD_ID = prd.PK_ID;
                            RTEntNew.ROUTINE_DATE = txtDate.Text;
                            RTEntNew.TEACHER_ID = ddlTeacher.SelectedValue;
                            RTEntNew.STATUS = "1";
                            RTEntNew = (ROUTINE)RTSer.GetSingle(RTEntNew);
                            if (RTEntNew != null)
                            {
                                if (RTEntNew.PK_ID != routineID) //if same routine's info need to change then it must be changed
                                {
                                    flagU = true;
                                    DT.HAPPY = false;
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEntNew.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        progCode = PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEntNew.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        semCode = SMSEnt.SEMESTER_CODE;
                                    }
                                    sectionCode = RTEntNew.SECTION;
                                }
                            }
                        }

                    }
                }
                #endregion

                #region to update
                if (!flagU)
                {
                    #region to update in routine table
                    SUBEnt = new HSS_SUBJECT();
                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                    SUBEnt.STATUS = "1";
                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                    if (SUBEnt != null)
                    {
                        lblPreviousSubject.Text = SUBEnt.SUBJECT_CODE;
                    }

                    EMPEnt = new Employees();
                    EMPEnt.EMPLOYEEID = RTEnt.TEACHER_ID;
                    EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
                    if (EMPEnt != null)
                    {
                        lblPreviousSubject.Text += "-" + EMPEnt.Abbrevation;
                    }
                    if (RTEnt.ASSIST_TEACH_ID != "")
                    {
                        EMPEnt = new Employees();
                        EMPEnt.EMPLOYEEID = RTEnt.ASSIST_TEACH_ID;
                        EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
                        if (EMPEnt != null)
                        {
                            lblPreviousSubject.Text += "/" + EMPEnt.Abbrevation;
                        }
                    }

                    if (RTEnt.LAB_THEORY == "Theory")
                    {
                        lblPreviousSubject.Text += "-TH";
                        if (RTEnt.LAB_NO != "")
                        {
                            lblPreviousSubject.Text += "-" + RTEnt.LAB_NO;
                        }
                    }
                    else
                    {
                        lblPreviousSubject.Text += "-L";

                        if (RTEnt.LAB_NO != "")
                        {
                            lblPreviousSubject.Text += "-" + RTEnt.LAB_NO;
                        }
                    }


                    SUBEnt = new HSS_SUBJECT();
                    SUBEnt.PK_ID = ddlSubject.SelectedValue;
                    SUBEnt.STATUS = "1";
                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                    if (SUBEnt != null)
                    {
                        lblPresentSubject.Text = SUBEnt.SUBJECT_CODE;
                    }

                    EMPEnt = new Employees();
                    EMPEnt.EMPLOYEEID = ddlTeacher.SelectedValue;
                    EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
                    if (EMPEnt != null)
                    {
                        lblPresentSubject.Text += "-" + EMPEnt.Abbrevation;
                    }
                    EMPEnt = new Employees();
                    EMPEnt.EMPLOYEEID = ddlAssistTeacher.SelectedValue;
                    EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
                    if (EMPEnt != null && ddlAssistTeacher.SelectedValue != "")
                    {
                        lblPresentSubject.Text += "/" + EMPEnt.Abbrevation;
                    }

                    if (ddlLabTheory.SelectedValue == "Theory")
                    {
                        lblPresentSubject.Text += "-TH";
                        if (txtClassNo.Text != "")
                        {
                            lblPresentSubject.Text += "-" + txtClassNo.Text;
                        }
                    }
                    else
                    {
                        lblPresentSubject.Text += "-L";

                        if (txtLabNo.Text != "")
                        {
                            lblPresentSubject.Text += "-" + txtLabNo.Text;
                        }
                    }

                    RTEnt = new ROUTINE();
                    RTEnt.ROUTINE_DATE = txtDate.Text;
                    RTEnt.PROGRAM_ID = ddlProgram.SelectedValue;
                    RTEnt.SEMESTER = ddlSemester.SelectedValue;
                    RTEnt.SECTION = ddlSection.SelectedValue;
                    RTEnt.PERIOD_ID = ddlPeriod.SelectedValue;
                    if (RTEnt != null)
                    {
                        divupdate.Visible = true;
                    }
                    else
                    {
                        divupdate.Visible = false;
                    }


                    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "popp();", true);
                    #endregion

                }
                else
                {
                    if (ddlSubject.SelectedValue != "Select")
                        HelperFunction.MsgBox(this, this.GetType(), ddlTeacher.SelectedItem.ToString() + "Routine Already Exist in" + " " + progCode + " " + semCode + " Sec:" + sectionCode);
                }
                #endregion
            }


            else
            {
                string periodTime = "";
                #region to insert
                EntityList PeriodListI = new EntityList();


                #region to check if there is duplicate routine
                PRDEnt = new PERIOD();
                PRDEnt.PK_ID = ddlPeriod.SelectedValue;
                PRDEnt = (PERIOD)PRDSer.GetSingle(PRDEnt);
                if (PRDEnt != null)
                {
                    periodTime = PRDEnt.TIME;
                    PRDEnt = new PERIOD();
                    PRDEnt.TIME = periodTime;
                    PeriodListI = PRDSer.GetAll(PRDEnt);
                    if (PeriodListI.Count > 0)
                    {
                        foreach (PERIOD prd in PeriodListI)
                        {
                            ROUTINE RTEntNew = new ROUTINE();
                            RTEntNew.PERIOD_ID = prd.PK_ID;
                            RTEntNew.ROUTINE_DATE = txtDate.Text;
                            RTEntNew.TEACHER_ID = ddlTeacher.SelectedValue;
                            RTEntNew = (ROUTINE)RTSer.GetSingle(RTEntNew);
                            if (RTEntNew != null)
                            {
                                flag = true;
                                DT.HAPPY = false;
                                PEnt = new program();
                                PEnt.PK_ID = RTEntNew.PROGRAM_ID;
                                PEnt = (program)PSer.GetSingle(PEnt);
                                if (PEnt != null)
                                {
                                    progCode = PEnt.PROGRAM_CODE;
                                }
                                SMSEnt = new semester();
                                SMSEnt.PK_ID = RTEntNew.SEMESTER;
                                SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                if (SMSEnt != null)
                                {
                                    semCode = SMSEnt.SEMESTER_CODE;
                                }
                                sectionCode = RTEntNew.SECTION;

                            }
                        }

                    }
                }
                #endregion

                if (!flag)
                {
                    #region to insert in routine
                    //RTEnt = new ROUTINE();
                    //RTEnt.PERIOD_ID = ddlPeriod.SelectedValue;
                    //RTEnt.SECTION = ddlSection.SelectedValue;
                    //RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                    //if (RTEnt != null)
                    //{
                    //    HelperFunction.MsgBox(this, this.GetType(), "Routine is already made in this Period/Section");
                    //}
                    //else
                    //{
                    RTEnt = new ROUTINE();
                    RTEnt.ROUTINE_DATE = txtDate.Text;
                    string[] nepdate1 = hf.ConvertEnglishToNepali(txtDate.Text);
                    RTEnt.ROUTINE_DAY = nepdate1[0];
                    RTEnt.ROUTINE_MONTH = nepdate1[1];
                    RTEnt.ROUTINE_YEAR = nepdate1[2];

                    RTEnt.BATCH = ddlBatch.SelectedValue;
                    RTEnt.SEMESTER = ddlSemester.SelectedValue;
                    RTEnt.PROGRAM_ID = ddlProgram.SelectedValue;
                    RTEnt.SECTION = ddlSection.SelectedValue;
                    RTEnt.PERIOD_ID = ddlPeriod.SelectedValue;
                    RTEnt.SUBJECT_ID = ddlSubject.SelectedValue;
                    RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                    RTEnt.LAB_THEORY = ddlLabTheory.SelectedValue;
                    if (ddlLabTheory.SelectedValue == "Theory")
                    {
                        RTEnt.LAB_NO = txtClassNo.Text;
                    }
                    else
                    {
                        RTEnt.LAB_NO = txtLabNo.Text;
                    }

                    if (ddlAssistTeacher.SelectedValue != "Select")
                        RTEnt.ASSIST_TEACH_ID = ddlAssistTeacher.SelectedValue;
                    else
                    {
                        RTEnt.ASSIST_TEACH_ID = "";
                    }
                    RTEnt.STATUS = "1";
                    RTSer.Insert(RTEnt, DT);
                    // }
                    #endregion
                    #region to insert in class_progress_schedule

                    CPSEnt = new CLASS_PROGRESS_SCHEDULE();
                    CPSEnt.CLASS_DATE = txtDate.Text;
                    string[] nepdate2 = hf.ConvertEnglishToNepali(txtDate.Text);
                    CPSEnt.CLASS_DAY = nepdate2[0];
                    CPSEnt.CLASS_MONTH = nepdate2[1];
                    CPSEnt.CLASS_YEAR = nepdate2[2];
                    DateTime date = DateTime.ParseExact(txtDate.Text, "dd.MMM.yyyy", CultureInfo.InvariantCulture);

                    CPSEnt.CLASS_DAY_OF_WEEK = date.DayOfWeek.ToString().Substring(0, 3).ToUpper();
                    CPSEnt.BATCH = ddlBatch.SelectedValue;
                    CPSEnt.SEMESTER = ddlSemester.SelectedValue;
                    CPSEnt.SECTION = ddlSection.SelectedValue;

                    CPSEnt.SUBJECT_ID = ddlSubject.SelectedValue;
                    CPSEnt.TEACHER_ID = ddlTeacher.SelectedValue;

                    #region to get class hour from period
                    PRDEnt = new PERIOD();
                    PRDEnt.PROGRAM_ID = ddlProgram.SelectedValue;
                    PRDEnt.SEMESTER_ID = ddlSemester.SelectedValue;
                    PRDEnt.SECTION_ID = ddlSection.SelectedValue;
                    PRDEnt.PK_ID = ddlPeriod.SelectedValue;
                    PRDEnt = (PERIOD)PRDSer.GetSingle(PRDEnt);
                    if (PRDEnt != null)
                    {
                        CPSEnt.CLASS_HOUR = PRDEnt.CLASS_HOUR;
                    }

                    #endregion

                    SUBEnt = new HSS_SUBJECT();
                    SUBEnt.PK_ID = ddlSubject.SelectedValue;
                    SUBEnt.STATUS = "1";
                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                    if (SUBEnt != null)
                    {
                        CPSEnt.TOTAL_CLASS_HOUR = SUBEnt.CLASS_HOUR;
                    }

                    CPSEnt.LAB_THEORY = ddlLabTheory.SelectedValue;
                    CPSEnt.LAB_ASSIST = "0";
                    CPSSer.Insert(CPSEnt, DT);

                    #region to insert Lab Assistant in CLASS_PROGRESS_SCHEDULE
                    if (ddlLabTheory.SelectedValue == "Lab" && ddlAssistTeacher.SelectedValue != "Select")
                    {
                        CPSEnt = new CLASS_PROGRESS_SCHEDULE();
                        CPSEnt.CLASS_DATE = txtDate.Text;
                        string[] nepdate3 = hf.ConvertEnglishToNepali(txtDate.Text);
                        CPSEnt.CLASS_DAY = nepdate3[0];
                        CPSEnt.CLASS_MONTH = nepdate3[1];
                        CPSEnt.CLASS_YEAR = nepdate3[2];
                        DateTime date2 = DateTime.ParseExact(txtDate.Text, "dd.MMM.yyyy", CultureInfo.InvariantCulture);

                        CPSEnt.CLASS_DAY_OF_WEEK = date2.DayOfWeek.ToString().Substring(0, 3).ToUpper();
                        CPSEnt.BATCH = ddlBatch.SelectedValue;
                        CPSEnt.SEMESTER = ddlSemester.SelectedValue;
                        CPSEnt.SECTION = ddlSection.SelectedValue;

                        CPSEnt.SUBJECT_ID = ddlSubject.SelectedValue;
                        CPSEnt.TEACHER_ID = ddlAssistTeacher.SelectedValue;
                        CPSEnt.LAB_ASSIST = "1";
                        #region to get class hour from period
                        PRDEnt = new PERIOD();
                        PRDEnt.PROGRAM_ID = ddlProgram.SelectedValue;
                        PRDEnt.SEMESTER_ID = ddlSemester.SelectedValue;
                        PRDEnt.SECTION_ID = ddlSection.SelectedValue;
                        PRDEnt.PK_ID = ddlPeriod.SelectedValue;
                        PRDEnt = (PERIOD)PRDSer.GetSingle(PRDEnt);
                        if (PRDEnt != null)
                        {
                            CPSEnt.CLASS_HOUR = PRDEnt.CLASS_HOUR;
                        }

                        #endregion

                        SUBEnt = new HSS_SUBJECT();
                        SUBEnt.PK_ID = ddlSubject.SelectedValue;
                        SUBEnt.STATUS = "1";
                        SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                        if (SUBEnt != null)
                        {
                            CPSEnt.TOTAL_CLASS_HOUR = SUBEnt.CLASS_HOUR;
                        }

                        CPSEnt.LAB_THEORY = ddlLabTheory.SelectedValue;

                        CPSSer.Insert(CPSEnt, DT);
                    }
                    #endregion

                    #endregion
                }
                else
                {
                    if (ddlSubject.SelectedValue != "Select")
                        HelperFunction.MsgBox(this, this.GetType(), ddlTeacher.SelectedItem.ToString() + "Routine Already Exist in" + " " + progCode + " " + semCode + " Sec:" + sectionCode);
                }

                #endregion

            }

            if (DT.HAPPY == true)
            {
                DT.Commit();
                LoadGrid();
                HelperFunction.MsgBox(this, this.GetType(), "Successfull");
            }
            else
            {
                DT.Abort();
                if (!flagU && !flag) // if there is no repetation of routine but sth goes wrong while entering data
                    HelperFunction.MsgBox(this, this.GetType(), "Something goes wrong. Please Try Again");
            }
            DT.Dispose();
            LoadGrid();
        }
        else
        {
            HelperFunction.MsgBox(this, this.GetType(), "Please Complete all Fields");
        }
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddlBatch.Items.Clear();
        ddlSemester.Items.Clear();
        LoadProgram();

    }
    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSemester();
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBatch();
        LoadSubject();
        LoadPeriod();
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadPeriod();
    }

    protected void ddlLabTheory_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlLabTheory.SelectedValue == "Theory")
        {
            lblLabNo.Visible = false;
            txtLabNo.Visible = false;
            lblClassNo.Visible = true;
            txtClassNo.Visible = true;
            lblAssistTeacher.Visible = false;
            ddlAssistTeacher.Visible = false;
            ddlAssistTeacher.SelectedItem.Value = "";
        }
        else
        {
            lblLabNo.Visible = true;
            txtLabNo.Visible = true;
            lblClassNo.Visible = false;
            txtClassNo.Visible = false;
            lblAssistTeacher.Visible = true;
            ddlAssistTeacher.Visible = true;
            loadTeacherLab();
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        lblroutineIDe.Text = "";
        LoadGrid();
        if (gridRoutine.Rows.Count == 0)
        {
            gridRoutine.Visible = false;
            TRprd.Visible = false;
            trLabThr.Visible = false;
            trBtnSave.Visible = false;
            HelperFunction.MsgBox(this, this.GetType(), "Routine not Found. Go to New Routine");
        }
        else
        {
            gridRoutine.Visible = true;
            TRprd.Visible = false;
            trLabThr.Visible = false;
            trBtnSave.Visible = false;
        }

    }

    protected void btnViewPrvDate_Click(object sender, EventArgs e)
    {
        if (txtDate.Text == "" || txtPrevDate.Text == "")
        {
            HelperFunction.MsgBox(this, this.GetType(), "Please fill up the date fields.");
            txtDate.Focus();
        }
        else
        {
            loadGridRoutinePrevDay();
            gridRoutinePrevDay.Visible = true;
            TRprd.Visible = false;
            trLabThr.Visible = false;
            trBtnSave.Visible = false;
            btnSaveAboveDate.Visible = true;
        }

    }

    protected void clearUptoSec()
    {

        lblDayD.Text = "";
        lblPrevDay.Text = "";
        txtDate.Text = "";
        txtPrevDate.Text = "";
        ddlFaculty.SelectedIndex = 0;
        ddlLevel.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
    }

    protected void lblPrevDate_Click(object sender, EventArgs e)
    {
        TRprd.Visible = false;
        trLabThr.Visible = false;
        TRPrevDate.Visible = true;
    }

    protected void btnUpdateR_Click(object sender, EventArgs e)
    {
        divupdate.Visible = false;
        gridRoutinePrevDay.Visible = false;
        lblroutineIDe.Text = "";
        #region to generate ID 'lblMenubtnIDs'
        lblUpdateID.Text = "1";
        lblNewID.Text = "";
        lblPrevDayID.Text = "";
        lblWeekID.Text = "";
        #endregion

        #region Menu Button
        btnUpdateR.Font.Bold = true;
        btnNewR.Font.Bold = false;
        btnPrevDayR.Font.Bold = false;
        btnPrevWeekR.Font.Bold = false;
        #endregion

        #region to display date textfield
        trDate.Visible = true;
        TRPrevDate.Visible = false;
        tblPrevWeek.Visible = false;
        #endregion

        pnlVisibleUptoSec.Visible = true;

        #region visibility of buttons
        btnView.Visible = true;
        btnViewPrvDate.Visible = false;
        btnSaveAboveDate.Visible = false;
        btnViewWeek.Visible = false;
        btnSavePrevWeek.Visible = false;
        #endregion

        TRPrevDate.Visible = false;

        #region of dropdowns after section
        TRprd.Visible = false;
        trLabThr.Visible = false;
        trBtnSave.Visible = false;
        gridRoutine.Visible = false;
        #endregion
        clearUptoSec();
    }

    protected void btnNewR_Click(object sender, EventArgs e)
    {
        lblroutineIDe.Text = "";
        divupdate.Visible = false;
        gridRoutinePrevDay.Visible = false;
        #region to generate ID 'lblMenubtnIDs'
        lblNewID.Text = "2";
        lblUpdateID.Text = "";
        lblPrevDayID.Text = "";
        lblWeekID.Text = "";
        #endregion

        #region to display date textfield
        trDate.Visible = true;
        TRPrevDate.Visible = false;
        tblPrevWeek.Visible = false;
        #endregion

        #region Menu Button
        btnUpdateR.Font.Bold = false;
        btnNewR.Font.Bold = true;
        btnPrevDayR.Font.Bold = false;
        btnPrevWeekR.Font.Bold = false;
        #endregion

        pnlVisibleUptoSec.Visible = true;

        #region visibility of buttons
        btnView.Visible = false;
        btnViewPrvDate.Visible = false;
        btnSaveAboveDate.Visible = false;
        btnViewWeek.Visible = false;
        btnSavePrevWeek.Visible = false;
        #endregion

        TRPrevDate.Visible = false;

        #region of dropdowns after section
        TRprd.Visible = true;
        trLabThr.Visible = true;
        trBtnSave.Visible = true;
        gridRoutine.Visible = false;
        #endregion

        clearUptoSec();
    }

    protected void btnPrevDayR_Click(object sender, EventArgs e)
    {
        lblroutineIDe.Text = "";
        divupdate.Visible = false;
        gridRoutinePrevDay.Visible = false;
        #region to generate ID 'lblMenubtnIDs'
        lblPrevDayID.Text = "3";
        lblNewID.Text = "";
        lblUpdateID.Text = "";
        lblWeekID.Text = "";
        #endregion

        #region Menu Button
        btnUpdateR.Font.Bold = false;
        btnNewR.Font.Bold = false;
        btnPrevDayR.Font.Bold = true;
        btnPrevWeekR.Font.Bold = false;
        #endregion

        #region to display date textfield
        trDate.Visible = true;
        TRPrevDate.Visible = true;
        tblPrevWeek.Visible = false;
        #endregion

        TRPrevDate.Visible = true;
        pnlVisibleUptoSec.Visible = true;

        #region visibility of buttons
        btnViewPrvDate.Visible = true;
        btnView.Visible = false;
        btnSaveAboveDate.Visible = false;
        btnViewWeek.Visible = false;
        btnSavePrevWeek.Visible = false;
        #endregion

        #region of dropdowns after section
        TRprd.Visible = false;
        trLabThr.Visible = false;
        trBtnSave.Visible = false;
        gridRoutine.Visible = false;
        #endregion

        clearUptoSec();
    }

    protected void btnPrevWeekR_Click(object sender, EventArgs e)
    {
        lblroutineIDe.Text = "";
        divupdate.Visible = false;
        gridRoutinePrevDay.Visible = false;
        #region to generate ID 'lblMenubtnIDs'

        lblPrevDayID.Text = "";
        lblNewID.Text = "";
        lblUpdateID.Text = "";
        lblWeekID.Text = "4";
        #endregion

        #region Menu Button
        btnUpdateR.Font.Bold = false;
        btnNewR.Font.Bold = false;
        btnPrevDayR.Font.Bold = false;
        btnPrevWeekR.Font.Bold = true;
        btnSavePrevWeek.Visible = false;
        #endregion

        pnlVisibleUptoSec.Visible = true;

        #region to display date textfield
        trDate.Visible = false;
        TRPrevDate.Visible = false;
        trLabThr.Visible = false;
        tblPrevWeek.Visible = true;
        #endregion

        #region visibility of Buttons
        btnView.Visible = false;
        btnViewPrvDate.Visible = false;
        btnSaveAboveDate.Visible = false;
        gridRoutine.Visible = false;
        btnViewWeek.Visible = true;

        #endregion
        clearUptoSec();
    }

    protected void btnSaveAboveDate_Click(object sender, EventArgs e)
    {
        DistributedTransaction DT = new DistributedTransaction();
        foreach (GridViewRow gr in gridRoutinePrevDay.Rows)
        {
            Label lblRoutineID = gr.FindControl("lblRoutineID") as Label;
            Label lblDay = gr.FindControl("lblDay") as Label;
            Label lblPeriodId = gr.FindControl("lblPeriodId") as Label;
            Label lblPeriod = gr.FindControl("lblPeriod") as Label;
            Label lblTime = gr.FindControl("lblTime") as Label;
            Label lblRoutine1 = gr.FindControl("lblRoutine1") as Label;
            TextBox txtRoomNo = gr.FindControl("txtRoomNo") as TextBox;

            if (lblPeriod.Text != "Break")
            {

                #region to insert in Routine table
                ROUTINE RTEnt2 = new ROUTINE();
                ROUTINEService RTSer2 = new ROUTINEService();
                RTEnt = new ROUTINE();

                RTEnt2.PK_ID = lblRoutineID.Text;
                RTEnt2 = (ROUTINE)RTSer2.GetSingle(RTEnt2);
                if (RTEnt2 != null && lblRoutineID.Text != "")
                {

                    RTEnt.ROUTINE_DATE = txtDate.Text;
                    string[] nepdate = hf.ConvertEnglishToNepali(txtDate.Text);
                    RTEnt.ROUTINE_DAY = nepdate[0];
                    RTEnt.ROUTINE_MONTH = nepdate[1];
                    RTEnt.ROUTINE_YEAR = nepdate[2];
                    RTEnt.BATCH = RTEnt2.BATCH;
                    RTEnt.SEMESTER = RTEnt2.SEMESTER;
                    RTEnt.PROGRAM_ID = RTEnt2.PROGRAM_ID;
                    RTEnt.SECTION = RTEnt2.SECTION;
                    RTEnt.PERIOD_ID = RTEnt2.PERIOD_ID;
                    RTEnt.SUBJECT_ID = RTEnt2.SUBJECT_ID;
                    RTEnt.TEACHER_ID = RTEnt2.TEACHER_ID;
                    RTEnt.LAB_THEORY = RTEnt2.LAB_THEORY;
                    RTEnt.LAB_NO = RTEnt2.LAB_NO;
                    RTEnt.ASSIST_TEACH_ID = RTEnt2.ASSIST_TEACH_ID;
                    RTEnt.STATUS = "1";
                    RTSer.Insert(RTEnt, DT);

                    #region to insert in class_progress_schedule

                    CPSEnt = new CLASS_PROGRESS_SCHEDULE();
                    CPSEnt.CLASS_DATE = txtDate.Text;
                    string[] nepdate2 = hf.ConvertEnglishToNepali(txtDate.Text);
                    CPSEnt.CLASS_DAY = nepdate2[0];
                    CPSEnt.CLASS_MONTH = nepdate2[1];
                    CPSEnt.CLASS_YEAR = nepdate2[2];

                    DateTime date = DateTime.ParseExact(txtDate.Text, "dd.MMM.yyyy", CultureInfo.InvariantCulture);
                    CPSEnt.CLASS_DAY_OF_WEEK = date.DayOfWeek.ToString().Substring(0, 3).ToUpper();
                    CPSEnt.BATCH = RTEnt2.BATCH;
                    CPSEnt.SEMESTER = RTEnt2.SEMESTER;
                    CPSEnt.SECTION = RTEnt2.SECTION;
                    CPSEnt.SUBJECT_ID = RTEnt2.SUBJECT_ID;

                    SUBEnt = new HSS_SUBJECT();
                    SUBEnt.PK_ID = CPSEnt.SUBJECT_ID;
                    SUBEnt.STATUS = "1";
                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                    if (SUBEnt != null)
                    {
                        CPSEnt.TOTAL_CLASS_HOUR = SUBEnt.CLASS_HOUR;
                    }

                    CPSEnt.TEACHER_ID = RTEnt2.TEACHER_ID;
                    CPSEnt.LAB_ASSIST = "0";

                    #region to get class hour from period
                    PRDEnt = new PERIOD();
                    PRDEnt.PROGRAM_ID = ddlProgram.SelectedValue;
                    PRDEnt.SEMESTER_ID = ddlSemester.SelectedValue;
                    PRDEnt.SECTION_ID = ddlSection.SelectedValue;
                    PRDEnt.PK_ID = ddlPeriod.SelectedValue;
                    PRDEnt = (PERIOD)PRDSer.GetSingle(PRDEnt);
                    if (PRDEnt != null)
                    {
                        CPSEnt.CLASS_HOUR = PRDEnt.CLASS_HOUR;
                    }

                    #endregion

                    CPSEnt.LAB_THEORY = RTEnt2.LAB_THEORY;
                    CPSSer.Insert(CPSEnt, DT);




                    #endregion

                    if (RTEnt.LAB_THEORY == "Lab" && RTEnt.ASSIST_TEACH_ID != "")
                    {
                        CPSEnt = new CLASS_PROGRESS_SCHEDULE();
                        CPSEnt.CLASS_DATE = txtDate.Text;
                        nepdate2 = hf.ConvertEnglishToNepali(txtDate.Text);
                        CPSEnt.CLASS_DAY = nepdate2[0];
                        CPSEnt.CLASS_MONTH = nepdate2[1];
                        CPSEnt.CLASS_YEAR = nepdate2[2];

                        date = DateTime.ParseExact(txtDate.Text, "dd.MMM.yyyy", CultureInfo.InvariantCulture);
                        CPSEnt.CLASS_DAY_OF_WEEK = date.DayOfWeek.ToString().Substring(0, 3).ToUpper();
                        CPSEnt.BATCH = RTEnt2.BATCH;
                        CPSEnt.SEMESTER = RTEnt2.SEMESTER;
                        CPSEnt.SECTION = RTEnt2.SECTION;
                        CPSEnt.SUBJECT_ID = RTEnt2.SUBJECT_ID;

                        SUBEnt = new HSS_SUBJECT();
                        SUBEnt.PK_ID = CPSEnt.SUBJECT_ID;
                        SUBEnt.STATUS = "1";
                        SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                        if (SUBEnt != null)
                        {
                            CPSEnt.TOTAL_CLASS_HOUR = SUBEnt.CLASS_HOUR;
                        }

                        CPSEnt.TEACHER_ID = RTEnt2.ASSIST_TEACH_ID;
                        CPSEnt.LAB_ASSIST = "1";

                        #region to get class hour from period
                        PRDEnt = new PERIOD();
                        PRDEnt.PROGRAM_ID = ddlProgram.SelectedValue;
                        PRDEnt.SEMESTER_ID = ddlSemester.SelectedValue;
                        PRDEnt.SECTION_ID = ddlSection.SelectedValue;
                        PRDEnt.PK_ID = ddlPeriod.SelectedValue;
                        PRDEnt = (PERIOD)PRDSer.GetSingle(PRDEnt);
                        if (PRDEnt != null)
                        {
                            CPSEnt.CLASS_HOUR = PRDEnt.CLASS_HOUR;
                        }

                        #endregion

                        CPSEnt.LAB_THEORY = RTEnt2.LAB_THEORY;
                        CPSSer.Insert(CPSEnt, DT);

                    }


                }

                #endregion



            }
        }
        if (DT.HAPPY == true)
        {
            DT.Commit();
            HelperFunction.MsgBox(this, this.GetType(), "Successful.");
            btnSaveAboveDate.Visible = false;
        }
        else
        {
            DT.Abort();
            HelperFunction.MsgBox(this, this.GetType(), "Somethen went wrong.");
            btnSaveAboveDate.Visible = false;
        }
        DT.Dispose();

    }

    protected void btnViewWeek_Click(object sender, EventArgs e)
    {
        loadWeekGrid();
    }

    protected string day_number(string dayname)
    {
        string daynumber = "0";
        if (dayname == "Sunday")
            daynumber = "1";
        else if (dayname == "Monday")
            daynumber = "2";
        else if (dayname == "Tuesday")
            daynumber = "3";
        else if (dayname == "Wednesday")
            daynumber = "4";
        else if (dayname == "Thursday")
            daynumber = "5";
        else if (dayname == "Friday")
            daynumber = "6";
        else if (dayname == "Saturday")
            daynumber = "7";
        return daynumber;
    }
    protected void loadWeekGrid()
    {
        ACEnt = new ACADEMIC_CALENDAR();
        ACEnt.CAL_DATE = hf.ConvertNepaliTOEnglish(hf.NepaliDay(), hf.NepaliMonth(), hf.NepaliYear());
        ACEnt.BATCH = ddlBatch.SelectedValue;
        ACEnt.PROGRAMID = ddlProgram.SelectedValue;
        ACEnt.SEMESTER = ddlSemester.SelectedValue;
        ACEnt = (ACADEMIC_CALENDAR)ACSer.GetSingle(ACEnt);
        if (ACEnt != null)
        {
            int i = 0;
            i = Convert.ToInt32(day_number(ACEnt.CAL_DAY_OF_WEEK));
            theList = new EntityList();

            string routin_of_date = hf.ConvertNepaliTOEnglish(hf.NepaliDay(), hf.NepaliMonth(), hf.NepaliYear());
            DateTime day1 = Convert.ToDateTime(routin_of_date);
            routin_of_date = hf.CheckDate(day1.ToString());
            string[] date = routin_of_date.Split('/');
            routin_of_date = date[1] + "/" + date[0] + "/" + date[2];
            for (; i <= 7; i++)
            {
                #region to load grid upto saturday only
                ACEnt = new ACADEMIC_CALENDAR();
                ACEnt.CAL_DATE = routin_of_date;
                ACEnt.BATCH = ddlBatch.SelectedValue;
                ACEnt.PROGRAMID = ddlProgram.SelectedValue;
                ACEnt.SEMESTER = ddlSemester.SelectedValue;
                ACEnt = (ACADEMIC_CALENDAR)ACSer.GetSingle(ACEnt);
                if (ACEnt != null)
                {
                    theList.Add(ACEnt);
                }

                date = routin_of_date.Split('/');
                routin_of_date = date[1] + "/" + date[0] + "/" + date[2];

                day1 = Convert.ToDateTime(routin_of_date).AddDays(1);
                routin_of_date = hf.CheckDate(day1.ToString());

                date = routin_of_date.Split('/');
                routin_of_date = date[1] + "/" + date[0] + "/" + date[2];
                #endregion
            }
        }
        gridWeek.DataSource = theList;
        gridWeek.DataBind();

        if (gridWeek.Rows.Count == 0)
        {
            HelperFunction.MsgBox(this, this.GetType(), "Please provide valid input parameters");
        }
        else
        {
            btnSavePrevWeek.Visible = true;
        }
    }

    protected void gridWeek_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblDay = e.Row.FindControl("lblDay") as Label;
            Label lblDate = e.Row.FindControl("lblDate") as Label;
            Label lblAsOfDate = e.Row.FindControl("lblAsOfDate") as Label;
            Label lblPrevWorkingDay = e.Row.FindControl("lblPrevWorkingDay") as Label;
            Label lblWorkingDay = e.Row.FindControl("lblWorkingDay") as Label;

            string[] date = lblDate.Text.Split('/');

            DateTime day1 = Convert.ToDateTime(date[1] + "/" + date[0] + "/" + date[2]).AddDays(-7);

            string[] prevDate = hf.CheckDate(day1.ToString()).Split('/');
            lblAsOfDate.Text = prevDate[1] + "/" + prevDate[0] + "/" + prevDate[2];

            #region to find prev working day
            ACEnt = new ACADEMIC_CALENDAR();
            ACEnt.CAL_DATE = lblAsOfDate.Text;
            ACEnt = (ACADEMIC_CALENDAR)ACSer.GetSingle(ACEnt);
            if (ACEnt != null)
            {
                lblPrevWorkingDay.Text = ACEnt.WORKING_DAY;
            }
            #endregion

            #region to color cells of non-working day
            if (lblWorkingDay.Text == "0")
            {
                lblDate.ForeColor = System.Drawing.Color.Red;
            }
            if (lblPrevWorkingDay.Text == "0")
            {
                lblAsOfDate.ForeColor = System.Drawing.Color.Red;
            }
            #endregion

        }
    }


    protected void btnSavePrevWeek_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in gridWeek.Rows)
        {
            DistributedTransaction DT = new DistributedTransaction();
            string dayofweek = "";
            EntityList thelist = new EntityList();

            Label lblAsOfDate = gr.FindControl("lblAsOfDate") as Label;
            Label lblDate = gr.FindControl("lblDate") as Label;
            Label lblDay = gr.FindControl("lblDay") as Label;

            if (lblDay.Text == "Sunday")
            {
                dayofweek = "SUN";
            }
            else if (lblDay.Text == "Monday")
            {
                dayofweek = "MON";
            }
            else if (lblDay.Text == "Tuesday")
            {
                dayofweek = "TUE";
            }
            else if (lblDay.Text == "Wednesday")
            {
                dayofweek = "WED";
            }
            else if (lblDay.Text == "Thursday")
            {
                dayofweek = "THU";
            }
            else if (lblDay.Text == "Friday")
            {
                dayofweek = "FRI";
            }
            else if (lblDay.Text == "Saturday")
            {
                dayofweek = "SAT";
            }

            ACEnt = new ACADEMIC_CALENDAR();
            ACEnt.CAL_DATE = lblDate.Text;
            ACEnt = (ACADEMIC_CALENDAR)ACSer.GetSingle(ACEnt);
            if (ACEnt != null)
            {
                #region to know the day is holiday or not
                if (ACEnt.WORKING_DAY == "0")
                {
                    HelperFunction.MsgBox(this, this.GetType(), lblDay.Text + " is holiday");
                }
                else
                {
                    RTEnt = new ROUTINE();
                    RTEnt.ROUTINE_DATE = lblAsOfDate.Text;
                    RTEnt.PROGRAM_ID = ddlProgram.SelectedValue;
                    RTEnt.SEMESTER = ddlSemester.SelectedValue;
                    RTEnt.SECTION = ddlSection.SelectedValue;
                    thelist = RTSer.GetAll(RTEnt);
                    if (thelist.Count > 0)
                    {
                        foreach (ROUTINE rt in thelist)
                        {
                            #region to save routine of a week in Routine Table
                            RTEnt = new ROUTINE();
                            RTEnt.ROUTINE_DATE = lblDate.Text;
                            string[] nepDate = hf.ConvertEnglishToNepali(lblDate.Text);
                            RTEnt.ROUTINE_DAY = nepDate[0];
                            RTEnt.ROUTINE_MONTH = nepDate[1];
                            RTEnt.ROUTINE_YEAR = nepDate[2];
                            RTEnt.BATCH = rt.BATCH;
                            RTEnt.PERIOD_ID = rt.PERIOD_ID;
                            RTEnt.PROGRAM_ID = rt.PROGRAM_ID;
                            RTEnt.SEMESTER = rt.SEMESTER;
                            RTEnt.SECTION = rt.SECTION;
                            RTEnt.SUBJECT_ID = rt.SUBJECT_ID;
                            RTEnt.TEACHER_ID = rt.TEACHER_ID;
                            RTEnt.LAB_THEORY = rt.LAB_THEORY;
                            RTEnt.LAB_NO = rt.LAB_NO;
                            RTEnt.ASSIST_TEACH_ID = rt.ASSIST_TEACH_ID;
                            RTEnt.STATUS = "1";
                            RTSer.Insert(RTEnt, DT);
                            #endregion

                            #region to save routine data of a week in Class_Progress_schedule
                            CPSEnt = new CLASS_PROGRESS_SCHEDULE();

                            CPSEnt.CLASS_DATE = lblDate.Text;
                            CPSEnt.CLASS_DAY = nepDate[0];
                            CPSEnt.CLASS_MONTH = nepDate[1];
                            CPSEnt.CLASS_YEAR = nepDate[2];
                            CPSEnt.BATCH = rt.BATCH;
                            CPSEnt.SEMESTER = rt.SEMESTER;
                            CPSEnt.SECTION = rt.SECTION;
                            CPSEnt.SUBJECT_ID = rt.SUBJECT_ID;
                            CPSEnt.TEACHER_ID = rt.TEACHER_ID;
                            CPSEnt.LAB_THEORY = rt.LAB_THEORY;
                            CPSEnt.CLASS_DAY_OF_WEEK = dayofweek;
                            SUBEnt = new HSS_SUBJECT();
                            SUBEnt.PK_ID = CPSEnt.SUBJECT_ID;
                            SUBEnt.STATUS = "1";
                            SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                            if (SUBEnt != null)
                            {
                                CPSEnt.TOTAL_CLASS_HOUR = SUBEnt.CLASS_HOUR;
                            }
                            #region to get class hour from period
                            PRDEnt = new PERIOD();
                            PRDEnt.PROGRAM_ID = ddlProgram.SelectedValue;
                            PRDEnt.SEMESTER_ID = ddlSemester.SelectedValue;
                            PRDEnt.SECTION_ID = ddlSection.SelectedValue;
                            PRDEnt.PK_ID = ddlPeriod.SelectedValue;
                            PRDEnt = (PERIOD)PRDSer.GetSingle(PRDEnt);
                            if (PRDEnt != null)
                            {
                                CPSEnt.CLASS_HOUR = PRDEnt.CLASS_HOUR;
                            }
                            #endregion
                            CPSSer.Insert(CPSEnt, DT);
                            #endregion
                        }
                        if (DT.HAPPY == true)
                        {
                            DT.Commit();
                            HelperFunction.MsgBox(this, this.GetType(), "Successful");
                        }
                        else
                        {
                            DT.Abort();
                            HelperFunction.MsgBox(this, this.GetType(), "Error.");
                        }
                        DT.Dispose();
                    }
                    else
                    {
                        HelperFunction.MsgBox(this, this.GetType(), "No Routine Found in " + lblDay.Text);
                    }
                }
                #endregion
            }

        }

    }


}