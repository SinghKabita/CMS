using DataHelper.Framework;
using Entity.Components;
using Entity.Framework;
using Service.Components;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class atten_Attendance : System.Web.UI.Page
{
    Months MEnt = new Months();
    MonthsService MSer = new MonthsService();

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    HSS_SUBJECT SUBEnt = new HSS_SUBJECT();
    HSS_SUBJECTService SUBSer = new HSS_SUBJECTService();

    Section SCEnt = new Section();
    SectionService SCSer = new SectionService();

    HSS_ATTENDANCE AEnt = new HSS_ATTENDANCE();
    HSS_ATTENDANCEService ASer = new HSS_ATTENDANCEService();

    HSS_ATTENDANCE_AD AADEnt = new HSS_ATTENDANCE_AD();
    HSS_ATTENDANCE_ADService AADSer = new HSS_ATTENDANCE_ADService();

    attendance_for_report AREnt = new attendance_for_report();
    attendance_for_reportService ARSer = new attendance_for_reportService();

    HSS_CURRENT_STUDENT CSEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSer = new HSS_CURRENT_STUDENTService();

    HSS_STUDENT STEnt = new HSS_STUDENT();
    HSS_STUDENTService STSer = new HSS_STUDENTService();

    BatchYear BTEnt = new BatchYear();
    BatchYearService BTSer = new BatchYearService();

    HelperFunction hf = new HelperFunction();

    EntityList theList = new EntityList();
    EntityList newList = new EntityList();

    TEACHER_SUBJECT_MAPPING TSUBEnt = new TEACHER_SUBJECT_MAPPING();
    TEACHER_SUBJECT_MAPPINGService TSUBSer = new TEACHER_SUBJECT_MAPPINGService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    Employees EMPEnt = new Employees();
    EmployeesService EMPSrv = new EmployeesService();


    UserProfileEntity userProfileEnt = new UserProfileEntity();

    static Boolean status = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFaculty();
            LoadLevel();
            LoadProgram();
            LoadMonth();
            LoadSection();
            ddlMonth.SelectedValue = hf.NepaliMonth();
            ddlDay.SelectedValue = hf.NepaliDay();
            txtYear.Text = hf.NepaliYear();
            string[] englishDate = hf.GetTodayDate().Split('/');
            ddlMonthEng.SelectedValue = englishDate[1];
            ddlDayEng.SelectedValue = englishDate[0];
            txtYearEng.Text = englishDate[2];

            userProfileEnt = (UserProfileEntity)HttpContext.Current.Session["UserProfile"];

            string groupid = userProfileEnt.UserGroupID.ToString();

            string path = HttpContext.Current.Request.Url.AbsolutePath;

            path = path.Replace("/COLLEGE/", "");
            status = hf.checkPageAccess(path, groupid);

            if (status == true)
            {
                ddlDay.Enabled = false;
                ddlMonth.Enabled = false;
                grdView.Columns[36].Visible = false;
            }
            else
            {
                ddlDay.Enabled = true;
                ddlMonth.Enabled = true;
                grdView.Columns[36].Visible = true;
            }
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
        ddlBatch.Items.Insert(0, "Select");
        ddlSemester.Items.Insert(0, "Select");
        ddlSubject.Items.Insert(0, "Select");

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
        ddlBatch.Items.Insert(0, "Select");
        ddlSemester.Items.Insert(0, "Select");
        ddlSubject.Items.Insert(0, "Select");

    }

    protected void LoadBatch()
    {
        BTEnt = new BatchYear();
        BTEnt.ACTIVE = "1";
        BTEnt.SEMESTER = ddlSemester.SelectedValue;
        ddlBatch.DataSource = BTSer.GetAll(BTEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();

    }

    protected void LoadSemester()
    {
        theList = new EntityList();
        EntityList semList = new EntityList();
        BTEnt = new BatchYear();
        BTEnt.ACTIVE = "1";
        BTEnt.PROGRAM = ddlProgram.SelectedValue;
        theList = BTSer.GetAll(BTEnt);
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
        ddlSemester.Items.Insert(0, "Select");
    }

    protected void LoadMonth()
    {
        MEnt = new Months();

        ddlMonth.DataSource = MSer.GetAll(MEnt);
        ddlMonth.DataTextField = "MONTHNAME";
        ddlMonth.DataValueField = "MONTHID";
        ddlMonth.DataBind();
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

        EntityList theList = new EntityList();
        EntityList semList = new EntityList();

        TSUBEnt = new TEACHER_SUBJECT_MAPPING();
        TSUBEnt.SEMESTER = ddlSemester.SelectedValue;
        TSUBEnt.BATCH = ddlBatch.SelectedValue;
        TSUBEnt.TEACHER_ID = ddlTeacher.SelectedValue;
        theList = TSUBSer.GetAll(TSUBEnt);
        foreach (TEACHER_SUBJECT_MAPPING tsm in theList)
        {
            SUBEnt = new HSS_SUBJECT();
            SUBEnt.PK_ID = tsm.SUBJECT_ID;
            SUBEnt.STATUS = "1";
            SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
            if (SUBEnt != null)
            {
                semList.Add(SUBEnt);
            }

        }
        ddlSubject.DataSource = semList;
        ddlSubject.DataTextField = "SUBJECT_NAME";
        ddlSubject.DataValueField = "PK_ID";
        ddlSubject.DataBind();

    }

    protected void LoadTeacher()
    {
        EntityList theList = new EntityList();
        EntityList semList = new EntityList();

        TSUBEnt = new TEACHER_SUBJECT_MAPPING();
        TSUBEnt.SEMESTER = ddlSemester.SelectedValue;
        TSUBEnt.BATCH = ddlBatch.SelectedValue;
        theList = TSUBSer.GetAll(TSUBEnt);

        foreach (TEACHER_SUBJECT_MAPPING tsm in theList)
        {
            EMPEnt = new Employees();
            EMPEnt.EMPLOYEEID = tsm.TEACHER_ID;
            EMPEnt = (Employees)EMPSrv.GetSingle(EMPEnt);
            if (EMPEnt != null)
            {
                semList.Add(EMPEnt);
            }
        }
        ddlTeacher.DataSource = semList;
        ddlTeacher.DataTextField = "fullname";
        ddlTeacher.DataValueField = "EMPLOYEEID";
        ddlTeacher.DataBind();
        ddlTeacher.Items.Insert(0, "Select");
    }

    protected string GetSubjectName(string subjectid)
    {
        string subjectname = "";
        SUBEnt = new HSS_SUBJECT();
        SUBEnt.PK_ID = subjectid;
        SUBEnt.STATUS = "1";
        SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
        if (SUBEnt != null)
        {
            subjectname = SUBEnt.SUBJECT_NAME;
        }
        return subjectname;
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        grdEntry.DataSource = getStudentData(ddlProgram.SelectedValue, ddlBatch.SelectedValue, ddlSemester.SelectedValue, ddlSection.SelectedValue, ddlSubject.SelectedValue);
        grdEntry.DataBind();

        LoadGrdView();
        grdEntry.Visible = true;
    }

    protected void LoadGrdView()
    {
        if (rbtnChooseDate.SelectedValue == "nepDate")
        {
            AEnt = new HSS_ATTENDANCE();
            theList = new EntityList();
            newList = new EntityList();

            AEnt.MONTH = ddlMonth.SelectedValue;
            AEnt.YEAR = txtYear.Text;
            AEnt.SUBJECT = ddlSubject.SelectedValue;
            theList = ASer.GetAll(AEnt);

            foreach (HSS_ATTENDANCE ATT in theList)
            {
                CSEnt = new HSS_CURRENT_STUDENT();
                CSEnt.BATCH = ddlBatch.SelectedValue;
                CSEnt.SEMESTER = ddlSemester.SelectedValue;
                CSEnt.SECTION = ddlSection.SelectedValue;
                CSEnt.YEAR = txtYear.Text;
                CSEnt.STUDENT_ID = ATT.STUDENT_ID;
                CSEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSEnt);
                if (CSEnt != null)
                {
                    newList.Add(ATT);
                }
            }
            grdView.DataSource = newList;
            grdView.DataBind();
        }
        else
        {
            AADEnt = new HSS_ATTENDANCE_AD();
            theList = new EntityList();
            newList = new EntityList();

            AADEnt.MONTH = ddlMonthEng.SelectedValue;
            AADEnt.YEAR = txtYearEng.Text;
            AADEnt.SUBJECT = ddlSubject.SelectedValue;
            theList = AADSer.GetAll(AADEnt);

            foreach (HSS_ATTENDANCE_AD ATT in theList)
            {
                CSEnt = new HSS_CURRENT_STUDENT();
                CSEnt.BATCH = ddlBatch.SelectedValue;
                CSEnt.SEMESTER = ddlSemester.SelectedValue;
                CSEnt.SECTION = ddlSection.SelectedValue;
                CSEnt.YEAR = txtYear.Text;
                CSEnt.STUDENT_ID = ATT.STUDENT_ID;
                CSEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSEnt);
                if (CSEnt != null)
                {
                    newList.Add(ATT);
                }
            }
            grdView.DataSource = newList;
            grdView.DataBind();
        }
    }

    protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        #region item
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            int totaldays = 0;
            int totalpresent = 0;
            Label lblRegNo = e.Row.FindControl("lblRegNo") as Label;
            Label lblName = e.Row.FindControl("lblName") as Label;
            Label lbltotalpresent = e.Row.FindControl("lbltotalpresent") as Label;
            Label lbltotaldays = e.Row.FindControl("lbltotaldays") as Label;

            STEnt = new HSS_STUDENT();
            STEnt.STUDENT_ID = lblRegNo.Text;
            STEnt = (HSS_STUDENT)STSer.GetSingle(STEnt);
            lblName.Text = STEnt.NAME_ENGLISH;


            string lblday = "lblday";


            for (int i = 1; i <= 32; i++)
            {
                Label lbldayn = e.Row.FindControl(lblday + i) as Label;

                if (lbldayn.Text == "")
                {
                    lbldayn.Text = "-";
                }
                else if (lbldayn.Text == "A")
                {
                    lbldayn.Text = "<font color='red'>A</font>";
                    totaldays++;
                }
                else if (lbldayn.Text == "P")
                {
                    totaldays++;
                    totalpresent++;
                }
                else if (lbldayn.Text == "L")
                {
                    lbldayn.Text = "<font color='red'>P</font>";
                    totaldays++;
                    totalpresent++;
                }
            }
            lbltotalpresent.Text = totalpresent.ToString();
            lbltotaldays.Text = totaldays.ToString();

        }
        #endregion
        #region edit
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) != 0)
        {

            Label lblRegNo = e.Row.FindControl("lblRegNoE") as Label;
            Label lblName = e.Row.FindControl("lblNameE") as Label;


            string rbtnDay = "rbtnDay";
            string lblDay = "lblday";
            for (int i = 1; i <= 32; i++)
            {
                RadioButtonList rbtnDayn = e.Row.FindControl(rbtnDay + i) as RadioButtonList;
                Label lblDayn = e.Row.FindControl(lblDay + i + "E") as Label;

                //this code generate 32 radiobuttonlists and 32 labels and set the value of label to radio button
                //also the radiobutton which doesnt have any value will be set enable false else true;

                rbtnDayn.SelectedValue = lblDayn.Text;


                if (rbtnDayn.SelectedValue == "")
                {
                    rbtnDayn.Enabled = false;
                }
                else
                {
                    rbtnDayn.Enabled = true;
                }
            }




        }
        #endregion
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        DistributedTransaction DT = new DistributedTransaction();
        foreach (GridViewRow gr in grdEntry.Rows)
        {
            Label lblRegNo = gr.FindControl("lblRegNo") as Label;
            Label lblSubjectPkId = gr.FindControl("lblSubjectPkId") as Label;
            RadioButtonList rbtnAttendance = gr.FindControl("rbtnAttendance") as RadioButtonList;

            #region to save and update in hss_attendance_table
            AEnt = new HSS_ATTENDANCE();
            AEnt.MONTH = ddlMonth.SelectedValue;
            AEnt.YEAR = txtYear.Text;
            AEnt.STUDENT_ID = lblRegNo.Text;
            AEnt.SEMESTER = ddlSemester.SelectedValue;
            AEnt.SUBJECT = ddlSubject.SelectedValue;
            AEnt.TEACHER_ID = ddlTeacher.SelectedValue;
            AEnt = (HSS_ATTENDANCE)ASer.GetSingle(AEnt);
            if (AEnt == null)
            {
                AEnt = new HSS_ATTENDANCE();
                AEnt.MONTH = ddlMonth.SelectedValue;
                AEnt.YEAR = txtYear.Text;
                AEnt.STUDENT_ID = lblRegNo.Text;
                AEnt.SEMESTER = ddlSemester.SelectedValue;
                AEnt.SUBJECT = ddlSubject.SelectedValue;
                AEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                if (ddlDay.SelectedValue == "1")
                    AEnt.DAY1 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "2")
                    AEnt.DAY2 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "3")
                    AEnt.DAY3 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "4")
                    AEnt.DAY4 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "5")
                    AEnt.DAY5 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "6")
                    AEnt.DAY6 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "7")
                    AEnt.DAY7 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "8")
                    AEnt.DAY8 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "9")
                    AEnt.DAY9 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "10")
                    AEnt.DAY10 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "11")
                    AEnt.DAY11 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "12")
                    AEnt.DAY12 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "13")
                    AEnt.DAY13 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "14")
                    AEnt.DAY14 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "15")
                    AEnt.DAY15 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "16")
                    AEnt.DAY16 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "17")
                    AEnt.DAY17 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "18")
                    AEnt.DAY18 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "19")
                    AEnt.DAY19 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "20")
                    AEnt.DAY20 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "21")
                    AEnt.DAY21 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "22")
                    AEnt.DAY22 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "23")
                    AEnt.DAY23 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "24")
                    AEnt.DAY24 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "25")
                    AEnt.DAY25 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "26")
                    AEnt.DAY26 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "27")
                    AEnt.DAY27 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "28")
                    AEnt.DAY28 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "29")
                    AEnt.DAY29 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "30")
                    AEnt.DAY30 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "31")
                    AEnt.DAY31 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "32")
                    AEnt.DAY32 = rbtnAttendance.SelectedValue;
                ASer.Insert(AEnt, DT);

            }
            else
            {
                //AEnt = new HSS_ATTENDANCE();
                //AEnt.MONTH = ddlMonth.SelectedValue;
                //AEnt.YEAR = txtYear.Text;
                //AEnt.STUDENT_ID = lblRegNo.Text;
                //AEnt.SEMESTER = ddlSemester.SelectedValue;
                //AEnt.SUBJECT = ddlSubject.SelectedValue;
                //AEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                //AEnt = (HSS_ATTENDANCE)ASer.GetSingle(AEnt);
                //if (AEnt != null)
                //{
                if (ddlDay.SelectedValue == "1")
                    AEnt.DAY1 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "2")
                    AEnt.DAY2 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "3")
                    AEnt.DAY3 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "4")
                    AEnt.DAY4 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "5")
                    AEnt.DAY5 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "6")
                    AEnt.DAY6 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "7")
                    AEnt.DAY7 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "8")
                    AEnt.DAY8 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "9")
                    AEnt.DAY9 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "10")
                    AEnt.DAY10 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "11")
                    AEnt.DAY11 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "12")
                    AEnt.DAY12 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "13")
                    AEnt.DAY13 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "14")
                    AEnt.DAY14 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "15")
                    AEnt.DAY15 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "16")
                    AEnt.DAY16 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "17")
                    AEnt.DAY17 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "18")
                    AEnt.DAY18 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "19")
                    AEnt.DAY19 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "20")
                    AEnt.DAY20 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "21")
                    AEnt.DAY21 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "22")
                    AEnt.DAY22 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "23")
                    AEnt.DAY23 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "24")
                    AEnt.DAY24 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "25")
                    AEnt.DAY25 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "26")
                    AEnt.DAY26 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "27")
                    AEnt.DAY27 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "28")
                    AEnt.DAY28 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "29")
                    AEnt.DAY29 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "30")
                    AEnt.DAY30 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "31")
                    AEnt.DAY31 = rbtnAttendance.SelectedValue;
                else if (ddlDay.SelectedValue == "32")
                    AEnt.DAY32 = rbtnAttendance.SelectedValue;
                ASer.Update(AEnt, DT);

                //}

            }

            #endregion
            #region to save and update in hss_attendance_AD

            AADEnt = new HSS_ATTENDANCE_AD();
            AADEnt.MONTH = ddlMonthEng.SelectedValue;
            AADEnt.YEAR = txtYearEng.Text;
            AADEnt.STUDENT_ID = lblRegNo.Text;
            AADEnt.SEMESTER = ddlSemester.SelectedValue;
            AADEnt.SUBJECT = ddlSubject.SelectedValue;
            AADEnt.TEACHER_ID = ddlTeacher.SelectedValue;
            AADEnt = (HSS_ATTENDANCE_AD)AADSer.GetSingle(AADEnt);
            if (AADEnt == null)
            {
                AADEnt = new HSS_ATTENDANCE_AD();
                AADEnt.MONTH = ddlMonthEng.SelectedValue;
                AADEnt.YEAR = txtYearEng.Text;
                AADEnt.STUDENT_ID = lblRegNo.Text;
                AADEnt.SEMESTER = ddlSemester.SelectedValue;
                AADEnt.SUBJECT = ddlSubject.SelectedValue;
                AADEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                if (ddlDayEng.SelectedValue == "01")
                {
                    AADEnt.DAY1 = rbtnAttendance.SelectedValue;
                }

                else if (ddlDayEng.SelectedValue == "02")
                    AADEnt.DAY2 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "03")
                    AADEnt.DAY3 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "04")
                    AADEnt.DAY4 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "05")
                    AADEnt.DAY5 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "06")
                    AADEnt.DAY6 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "07")
                {
                    AADEnt.DAY7 = rbtnAttendance.SelectedValue;
                }

                else if (ddlDayEng.SelectedValue == "08")
                    AADEnt.DAY8 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "09")
                    AADEnt.DAY9 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "10")
                    AADEnt.DAY10 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "11")
                    AADEnt.DAY11 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "12")
                    AADEnt.DAY12 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "13")
                    AADEnt.DAY13 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "14")
                    AADEnt.DAY14 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "15")
                    AADEnt.DAY15 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "16")
                    AADEnt.DAY16 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "17")
                    AADEnt.DAY17 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "18")
                    AADEnt.DAY18 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "19")
                    AADEnt.DAY19 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "20")
                    AADEnt.DAY20 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "21")
                {
                    AADEnt.DAY21 = rbtnAttendance.SelectedValue;
                }

                else if (ddlDayEng.SelectedValue == "22")
                    AADEnt.DAY22 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "23")
                    AADEnt.DAY23 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "24")
                    AADEnt.DAY24 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "25")
                    AADEnt.DAY25 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "26")
                    AADEnt.DAY26 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "27")
                    AADEnt.DAY27 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "28")
                    AADEnt.DAY28 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "29")
                    AADEnt.DAY29 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "30")
                    AADEnt.DAY30 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "31")
                    AADEnt.DAY31 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "32")
                    AADEnt.DAY32 = rbtnAttendance.SelectedValue;
                AADSer.Insert(AADEnt, DT);
            }
            else
            {
                AADEnt.STUDENT_ID = lblRegNo.Text;
                AADEnt.SEMESTER = ddlSemester.SelectedValue;
                AADEnt.SUBJECT = ddlSubject.SelectedValue;
                AADEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                if (ddlDayEng.SelectedValue == "01")
                    AADEnt.DAY1 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "02")
                    AADEnt.DAY2 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "03")
                    AADEnt.DAY3 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "04")
                    AADEnt.DAY4 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "05")
                    AADEnt.DAY5 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "06")
                    AADEnt.DAY6 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "07")
                    AADEnt.DAY7 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "08")
                    AADEnt.DAY8 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "09")
                    AADEnt.DAY9 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "10")
                    AADEnt.DAY10 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "11")
                    AADEnt.DAY11 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "12")
                    AADEnt.DAY12 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "13")
                    AADEnt.DAY13 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "14")
                    AADEnt.DAY14 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "15")
                    AADEnt.DAY15 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "16")
                    AADEnt.DAY16 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "17")
                    AADEnt.DAY17 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "18")
                    AADEnt.DAY18 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "19")
                    AADEnt.DAY19 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "20")
                    AADEnt.DAY20 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "21")
                {
                    AADEnt.DAY21 = rbtnAttendance.SelectedValue;
                }

                else if (ddlDayEng.SelectedValue == "22")
                    AADEnt.DAY22 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "23")
                    AADEnt.DAY23 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "24")
                    AADEnt.DAY24 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "25")
                    AADEnt.DAY25 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "26")
                    AADEnt.DAY26 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "27")
                    AADEnt.DAY27 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "28")
                    AADEnt.DAY28 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "29")
                    AADEnt.DAY29 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "30")
                    AADEnt.DAY30 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "31")
                    AADEnt.DAY31 = rbtnAttendance.SelectedValue;
                else if (ddlDayEng.SelectedValue == "32")
                    AADEnt.DAY32 = rbtnAttendance.SelectedValue;
                AADSer.Update(AADEnt, DT);
            }



            #endregion

            #region to save and update in attendance_for_report table
            AREnt = new attendance_for_report();
            AREnt.SEMESTER = ddlSemester.SelectedValue;
            AREnt.SUBJECT_ID = ddlSubject.SelectedValue;
            AREnt.STUDENT_ID = lblRegNo.Text;
            AREnt.YEAR = txtYear.Text;
            AREnt.MONTH = ddlMonth.SelectedValue;
            AREnt.DAY = ddlDay.SelectedValue;
            AREnt.TEACHER_ID = ddlTeacher.SelectedValue;
            AREnt = (attendance_for_report)ARSer.GetSingle(AREnt, DT);
            if (AREnt != null)
            {

                AREnt.SEMESTER = ddlSemester.SelectedValue;
                AREnt.SUBJECT_ID = ddlSubject.SelectedValue;
                AREnt.STUDENT_ID = lblRegNo.Text;
                AREnt.YEAR = txtYear.Text;
                AREnt.MONTH = ddlMonth.SelectedValue;
                AREnt.DAY = ddlDay.SelectedValue;
                AREnt.ENG_DATE = hf.ConvertNepaliTOEnglish(ddlDay.SelectedValue, ddlMonth.SelectedValue, hf.NepaliYear());
                AREnt.STATUS = rbtnAttendance.SelectedValue;
                AREnt.TEACHER_ID = ddlTeacher.SelectedValue;
                ARSer.Update(AREnt, DT);

            }
            else
            {
                AREnt = new attendance_for_report();
                AREnt.SEMESTER = ddlSemester.SelectedValue;
                AREnt.SUBJECT_ID = ddlSubject.SelectedValue;
                AREnt.STUDENT_ID = lblRegNo.Text;
                AREnt.YEAR = txtYear.Text;
                AREnt.MONTH = ddlMonth.SelectedValue;
                AREnt.DAY = ddlDay.SelectedValue;
                AREnt.STATUS = rbtnAttendance.SelectedValue;
                AREnt.ENG_DATE = hf.ConvertNepaliTOEnglish(ddlDay.SelectedValue, ddlMonth.SelectedValue, hf.NepaliYear());
                AREnt.TEACHER_ID = ddlTeacher.SelectedValue;
                ARSer.Insert(AREnt, DT);
            }


            #endregion


        }
        if (DT.HAPPY == true)
        {
            DT.Commit();
            HelperFunction.MsgBox(this, this.GetType(), "Data Saved Sucessfully");
            LoadGrdView();
            grdEntry.Visible = false;
        }
        else
        {
            DT.Abort();
            HelperFunction.MsgBox(this, this.GetType(), "Data Not Saved!!!");
            LoadGrdView();
            grdEntry.Visible = true;
        }


    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        btnReset.Attributes.Add("onclick", "if(confirm('Are you sure to reset to Holiday ?')){}else{return false}");
        DistributedTransaction DT = new DistributedTransaction();
        foreach (GridViewRow gr in grdEntry.Rows)
        {
            Label lblRegNo = gr.FindControl("lblRegNo") as Label;
            #region to reset in hss_attendance
            AEnt = new HSS_ATTENDANCE();
            AEnt.MONTH = ddlMonth.SelectedValue;
            AEnt.YEAR = txtYear.Text;
            AEnt.STUDENT_ID = lblRegNo.Text;
            AEnt.SEMESTER = ddlSemester.SelectedValue;
            AEnt = (HSS_ATTENDANCE)ASer.GetSingle(AEnt);
            if (AEnt != null)
            {
                if (ddlDay.SelectedValue == "1")
                    AEnt.DAY1 = "";
                else if (ddlDay.SelectedValue == "2")
                    AEnt.DAY2 = "";
                else if (ddlDay.SelectedValue == "3")
                    AEnt.DAY3 = "";
                else if (ddlDay.SelectedValue == "4")
                    AEnt.DAY4 = "";
                else if (ddlDay.SelectedValue == "5")
                    AEnt.DAY5 = "";
                else if (ddlDay.SelectedValue == "6")
                    AEnt.DAY6 = "";
                else if (ddlDay.SelectedValue == "7")
                    AEnt.DAY7 = "";
                else if (ddlDay.SelectedValue == "8")
                    AEnt.DAY8 = "";
                else if (ddlDay.SelectedValue == "9")
                    AEnt.DAY9 = "";
                else if (ddlDay.SelectedValue == "10")
                    AEnt.DAY10 = "";
                else if (ddlDay.SelectedValue == "11")
                    AEnt.DAY11 = "";
                else if (ddlDay.SelectedValue == "12")
                    AEnt.DAY12 = "";
                else if (ddlDay.SelectedValue == "13")
                    AEnt.DAY13 = "";
                else if (ddlDay.SelectedValue == "14")
                    AEnt.DAY14 = "";
                else if (ddlDay.SelectedValue == "15")
                    AEnt.DAY15 = "";
                else if (ddlDay.SelectedValue == "16")
                    AEnt.DAY16 = "";
                else if (ddlDay.SelectedValue == "17")
                    AEnt.DAY17 = "";
                else if (ddlDay.SelectedValue == "18")
                    AEnt.DAY18 = "";
                else if (ddlDay.SelectedValue == "19")
                    AEnt.DAY19 = "";
                else if (ddlDay.SelectedValue == "20")
                    AEnt.DAY20 = "";
                else if (ddlDay.SelectedValue == "21")
                    AEnt.DAY21 = "";
                else if (ddlDay.SelectedValue == "22")
                    AEnt.DAY22 = "";
                else if (ddlDay.SelectedValue == "23")
                    AEnt.DAY23 = "";
                else if (ddlDay.SelectedValue == "24")
                    AEnt.DAY24 = "";
                else if (ddlDay.SelectedValue == "25")
                    AEnt.DAY25 = "";
                else if (ddlDay.SelectedValue == "26")
                    AEnt.DAY26 = "";
                else if (ddlDay.SelectedValue == "27")
                    AEnt.DAY27 = "";
                else if (ddlDay.SelectedValue == "28")
                    AEnt.DAY28 = "";
                else if (ddlDay.SelectedValue == "29")
                    AEnt.DAY29 = "";
                else if (ddlDay.SelectedValue == "30")
                    AEnt.DAY30 = "";
                else if (ddlDay.SelectedValue == "31")
                    AEnt.DAY31 = "";
                else if (ddlDay.SelectedValue == "32")
                    AEnt.DAY32 = "";

                ASer.Update(AEnt, DT);

            }
            #endregion
            #region to reset in hss_attendance_AD
            AADEnt = new HSS_ATTENDANCE_AD();
            AADEnt.MONTH = ddlMonthEng.SelectedValue;
            AADEnt.YEAR = txtYearEng.Text;
            AADEnt.STUDENT_ID = lblRegNo.Text;
            AADEnt.SEMESTER = ddlSemester.SelectedValue;
            AADEnt = (HSS_ATTENDANCE_AD)AADSer.GetSingle(AADEnt);
            if (AADEnt != null)
            {
                if (ddlDayEng.SelectedValue == "01")
                    AADEnt.DAY1 = "";
                else if (ddlDayEng.SelectedValue == "02")
                    AADEnt.DAY2 = "";
                else if (ddlDayEng.SelectedValue == "03")
                    AADEnt.DAY3 = "";
                else if (ddlDayEng.SelectedValue == "04")
                    AADEnt.DAY4 = "";
                else if (ddlDayEng.SelectedValue == "05")
                    AADEnt.DAY5 = "";
                else if (ddlDayEng.SelectedValue == "06")
                    AADEnt.DAY6 = "";
                else if (ddlDayEng.SelectedValue == "07")
                    AADEnt.DAY7 = "";
                else if (ddlDayEng.SelectedValue == "08")
                    AADEnt.DAY8 = "";
                else if (ddlDayEng.SelectedValue == "09")
                    AADEnt.DAY9 = "";
                else if (ddlDayEng.SelectedValue == "10")
                    AADEnt.DAY10 = "";
                else if (ddlDayEng.SelectedValue == "11")
                    AADEnt.DAY11 = "";
                else if (ddlDayEng.SelectedValue == "12")
                    AADEnt.DAY12 = "";
                else if (ddlDayEng.SelectedValue == "13")
                    AADEnt.DAY13 = "";
                else if (ddlDayEng.SelectedValue == "14")
                    AADEnt.DAY14 = "";
                else if (ddlDayEng.SelectedValue == "15")
                    AADEnt.DAY15 = "";
                else if (ddlDayEng.SelectedValue == "16")
                    AADEnt.DAY16 = "";
                else if (ddlDayEng.SelectedValue == "17")
                    AADEnt.DAY17 = "";
                else if (ddlDayEng.SelectedValue == "18")
                    AADEnt.DAY18 = "";
                else if (ddlDayEng.SelectedValue == "19")
                    AADEnt.DAY19 = "";
                else if (ddlDayEng.SelectedValue == "20")
                    AADEnt.DAY20 = "";
                else if (ddlDayEng.SelectedValue == "21")
                    AADEnt.DAY21 = "";
                else if (ddlDayEng.SelectedValue == "22")
                    AADEnt.DAY22 = "";
                else if (ddlDayEng.SelectedValue == "23")
                    AADEnt.DAY23 = "";
                else if (ddlDayEng.SelectedValue == "24")
                    AADEnt.DAY24 = "";
                else if (ddlDayEng.SelectedValue == "25")
                    AADEnt.DAY25 = "";
                else if (ddlDayEng.SelectedValue == "26")
                    AADEnt.DAY26 = "";
                else if (ddlDayEng.SelectedValue == "27")
                    AADEnt.DAY27 = "";
                else if (ddlDayEng.SelectedValue == "28")
                    AADEnt.DAY28 = "";
                else if (ddlDayEng.SelectedValue == "29")
                    AADEnt.DAY29 = "";
                else if (ddlDayEng.SelectedValue == "30")
                    AADEnt.DAY30 = "";
                else if (ddlDayEng.SelectedValue == "31")
                    AADEnt.DAY31 = "";
                else if (ddlDayEng.SelectedValue == "32")
                    AADEnt.DAY32 = "";

                AADSer.Update(AADEnt, DT);

            }
            #endregion
            #region to reset in attendance_for_report
            AREnt = new attendance_for_report();
            AREnt.SEMESTER = ddlSemester.SelectedValue;
            AREnt.SUBJECT_ID = ddlSubject.SelectedValue;
            AREnt.STUDENT_ID = lblRegNo.Text;
            AREnt.YEAR = txtYear.Text;
            AREnt.MONTH = ddlMonth.SelectedValue;
            AREnt.DAY = ddlDay.SelectedValue;
            AREnt.TEACHER_ID = ddlTeacher.SelectedValue;
            AREnt = (attendance_for_report)ARSer.GetSingle(AREnt, DT);
            if (AREnt != null)
            {
                AREnt.STATUS = "";
                ARSer.Update(AREnt, DT);
            }
            #endregion

        }
        if (DT.HAPPY == true)
        {
            DT.Commit();
            HelperFunction.MsgBox(this, this.GetType(), "Data Reset Sucessfully");
            LoadGrdView();

        }
        else
        {
            DT.Abort();
            HelperFunction.MsgBox(this, this.GetType(), "Data Not Reset!!!");
            LoadGrdView();

        }
    }

    protected void grdView_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdView.EditIndex = e.NewEditIndex;
        LoadGrdView();
    }
    protected void grdView_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdView.EditIndex = -1;
        LoadGrdView();
    }

    protected void grdView_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = grdView.Rows[e.RowIndex];
        Label lblID = row.FindControl("lblID") as Label;


        RadioButtonList rbtnDay1 = row.FindControl("rbtnDay1") as RadioButtonList;
        RadioButtonList rbtnDay2 = row.FindControl("rbtnDay2") as RadioButtonList;
        RadioButtonList rbtnDay3 = row.FindControl("rbtnDay3") as RadioButtonList;
        RadioButtonList rbtnDay4 = row.FindControl("rbtnDay4") as RadioButtonList;
        RadioButtonList rbtnDay5 = row.FindControl("rbtnDay5") as RadioButtonList;
        RadioButtonList rbtnDay6 = row.FindControl("rbtnDay6") as RadioButtonList;
        RadioButtonList rbtnDay7 = row.FindControl("rbtnDay7") as RadioButtonList;
        RadioButtonList rbtnDay8 = row.FindControl("rbtnDay8") as RadioButtonList;
        RadioButtonList rbtnDay9 = row.FindControl("rbtnDay9") as RadioButtonList;
        RadioButtonList rbtnDay10 = row.FindControl("rbtnDay10") as RadioButtonList;
        RadioButtonList rbtnDay11 = row.FindControl("rbtnDay11") as RadioButtonList;
        RadioButtonList rbtnDay12 = row.FindControl("rbtnDay12") as RadioButtonList;
        RadioButtonList rbtnDay13 = row.FindControl("rbtnDay13") as RadioButtonList;
        RadioButtonList rbtnDay14 = row.FindControl("rbtnDay14") as RadioButtonList;
        RadioButtonList rbtnDay15 = row.FindControl("rbtnDay15") as RadioButtonList;
        RadioButtonList rbtnDay16 = row.FindControl("rbtnDay16") as RadioButtonList;
        RadioButtonList rbtnDay17 = row.FindControl("rbtnDay17") as RadioButtonList;
        RadioButtonList rbtnDay18 = row.FindControl("rbtnDay18") as RadioButtonList;
        RadioButtonList rbtnDay19 = row.FindControl("rbtnDay19") as RadioButtonList;
        RadioButtonList rbtnDay20 = row.FindControl("rbtnDay20") as RadioButtonList;
        RadioButtonList rbtnDay21 = row.FindControl("rbtnDay21") as RadioButtonList;
        RadioButtonList rbtnDay22 = row.FindControl("rbtnDay22") as RadioButtonList;
        RadioButtonList rbtnDay23 = row.FindControl("rbtnDay23") as RadioButtonList;
        RadioButtonList rbtnDay24 = row.FindControl("rbtnDay24") as RadioButtonList;
        RadioButtonList rbtnDay25 = row.FindControl("rbtnDay25") as RadioButtonList;
        RadioButtonList rbtnDay26 = row.FindControl("rbtnDay26") as RadioButtonList;
        RadioButtonList rbtnDay27 = row.FindControl("rbtnDay27") as RadioButtonList;
        RadioButtonList rbtnDay28 = row.FindControl("rbtnDay28") as RadioButtonList;
        RadioButtonList rbtnDay29 = row.FindControl("rbtnDay29") as RadioButtonList;
        RadioButtonList rbtnDay30 = row.FindControl("rbtnDay30") as RadioButtonList;
        RadioButtonList rbtnDay31 = row.FindControl("rbtnDay31") as RadioButtonList;
        RadioButtonList rbtnDay32 = row.FindControl("rbtnDay32") as RadioButtonList;

        AEnt = new HSS_ATTENDANCE();
        AEnt.PK_ID = lblID.Text;
        AEnt = (HSS_ATTENDANCE)ASer.GetSingle(AEnt);

        if (AEnt != null)
        {
            AEnt.DAY1 = rbtnDay1.SelectedValue;
            AEnt.DAY2 = rbtnDay2.SelectedValue;
            AEnt.DAY3 = rbtnDay3.SelectedValue;
            AEnt.DAY4 = rbtnDay4.SelectedValue;
            AEnt.DAY5 = rbtnDay5.SelectedValue;
            AEnt.DAY6 = rbtnDay6.SelectedValue;
            AEnt.DAY7 = rbtnDay7.SelectedValue;
            AEnt.DAY8 = rbtnDay8.SelectedValue;
            AEnt.DAY9 = rbtnDay9.SelectedValue;
            AEnt.DAY10 = rbtnDay10.SelectedValue;
            AEnt.DAY11 = rbtnDay11.SelectedValue;
            AEnt.DAY12 = rbtnDay12.SelectedValue;
            AEnt.DAY13 = rbtnDay13.SelectedValue;
            AEnt.DAY14 = rbtnDay14.SelectedValue;
            AEnt.DAY15 = rbtnDay15.SelectedValue;
            AEnt.DAY16 = rbtnDay16.SelectedValue;
            AEnt.DAY17 = rbtnDay17.SelectedValue;
            AEnt.DAY18 = rbtnDay18.SelectedValue;
            AEnt.DAY19 = rbtnDay19.SelectedValue;
            AEnt.DAY20 = rbtnDay20.SelectedValue;
            AEnt.DAY21 = rbtnDay21.SelectedValue;
            AEnt.DAY22 = rbtnDay22.SelectedValue;
            AEnt.DAY23 = rbtnDay23.SelectedValue;
            AEnt.DAY24 = rbtnDay24.SelectedValue;
            AEnt.DAY25 = rbtnDay25.SelectedValue;
            AEnt.DAY26 = rbtnDay26.SelectedValue;
            AEnt.DAY27 = rbtnDay27.SelectedValue;
            AEnt.DAY28 = rbtnDay28.SelectedValue;
            AEnt.DAY29 = rbtnDay29.SelectedValue;
            AEnt.DAY30 = rbtnDay30.SelectedValue;
            AEnt.DAY31 = rbtnDay31.SelectedValue;
            AEnt.DAY32 = rbtnDay32.SelectedValue;

            ASer.Update(AEnt);
        }


        string rbtnDay = "rbtnDay";


        for (int i = 1; i <= 32; i++)
        {
            RadioButtonList rbtnDayn = row.FindControl(rbtnDay + i) as RadioButtonList;

            AEnt = new HSS_ATTENDANCE();
            AEnt.PK_ID = lblID.Text;
            AEnt = (HSS_ATTENDANCE)ASer.GetSingle(AEnt);

            if (AEnt != null)
            {
                AREnt = new attendance_for_report();
                AREnt.STUDENT_ID = AEnt.STUDENT_ID;
                AREnt.SUBJECT_ID = AEnt.SUBJECT;
                AREnt.SEMESTER = AEnt.SEMESTER;
                AREnt.YEAR = AEnt.YEAR;
                AREnt.MONTH = AEnt.MONTH;
                AREnt.DAY = i.ToString();
                AREnt = (attendance_for_report)ARSer.GetSingle(AREnt);
                if (AREnt != null && rbtnDayn.SelectedValue != "")
                {
                    AREnt.STATUS = rbtnDayn.SelectedValue;
                    AREnt.ENG_DATE = hf.ConvertNepaliTOEnglish(i.ToString(), AEnt.MONTH, AEnt.YEAR);
                    ARSer.Update(AREnt);

                }
            }

        }
        grdView.EditIndex = -1;
        LoadGrdView();

    }

    protected void btnHideTable_Click(object sender, EventArgs e)
    {
        grdEntry.Visible = false;
        btnShowTable.Visible = true;
        btnHideTable.Visible = false;
    }

    protected void btnShowTable_Click(object sender, EventArgs e)
    {
        grdEntry.Visible = true;
        btnHideTable.Visible = true;
        btnShowTable.Visible = false;
    }


    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedValue != "Select")
        {
            LoadBatch();
            LoadTeacher();
        }
    }

    private IDbDataParameter[] CreateParmans(string program, string batch, string semester, string section, string subject)
    {

        List<IDbDataParameter> cmdParams = new List<IDbDataParameter>();
        //input Parameters start
        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_program", program));
        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_batch", batch));
        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_semester", semester));

        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_SECTION", section));
        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_SUBJECT", subject));
        //input Parameters ends
        cmdParams.Add(DataAccessFactory.CreateDataParameter("Result", ""));

        return cmdParams.ToArray();
    }

    public DataTable getStudentData(string program, string batch, string semester, string section, string subject)// getDSR->function name
    {
        GenericGetValueService srvGen = new GenericGetValueService();
        DataTable DT = new DataTable();
        try
        {
            DT = srvGen.ReaderToTable("PKJ_SELECT.getstudentforattendance", System.Data.CommandType.StoredProcedure, CreateParmans(program, batch, semester, section, subject));
        }
        catch
        {
        }
        return DT;
    }
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBatch.SelectedValue != "Select")
        {
            LoadSemester();
        }
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();

    }
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProgram.SelectedValue != "Select")
        {
            LoadSemester();

        }
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

            ddlSubject.Items.Clear();
        }

    }
    protected void ddlTeacher_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSubject();
    }

    protected void rbtnChooseDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnChooseDate.SelectedValue == "nepDate")
        {
            trNepDate.Visible = true;
            trEngDate.Visible = false;
            LoadGrdView();
        }
        else
        {
            trNepDate.Visible = false;
            trEngDate.Visible = true;
            LoadGrdView();
        }
    }

    protected void ddlDay_SelectedIndexChanged(object sender, EventArgs e)
    {
        string[] engdate = (hf.ConvertNepaliTOEnglishDate(ddlDay.Text, ddlMonth.Text, txtYear.Text)).Split('/');
        ddlDayEng.SelectedValue = engdate[0];
        ddlMonthEng.SelectedValue = engdate[1];
        txtYearEng.Text = engdate[2];
    }

    protected void ddlMonth_SelectedIndexChanged(object sender, EventArgs e)
    {
        string[] engdate = (hf.ConvertNepaliTOEnglishDate(ddlDay.Text, ddlMonth.Text, txtYear.Text)).Split('/');
        ddlDayEng.SelectedValue = engdate[0];
        ddlMonthEng.SelectedValue = engdate[1];
        txtYearEng.Text = engdate[2];
    }

    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        string[] engdate = (hf.ConvertNepaliTOEnglishDate(ddlDay.Text, ddlMonth.Text, txtYear.Text)).Split('/');
        ddlDayEng.SelectedValue = engdate[0];
        ddlMonthEng.SelectedValue = engdate[1];
        txtYearEng.Text = engdate[2];
    }

    protected void ddlDayEng_SelectedIndexChanged(object sender, EventArgs e)
    {
        string[] nepdate = hf.ConvertEnglishToNepali(ddlMonthEng.SelectedValue + "/" + ddlDayEng.SelectedValue + "/" + txtYearEng.Text);
        ddlDay.SelectedValue = nepdate[0];
        ddlMonth.SelectedValue = nepdate[1];
        txtYear.Text = nepdate[2];
    }

    protected void ddlMonthEng_SelectedIndexChanged(object sender, EventArgs e)
    {
        string[] nepdate = hf.ConvertEnglishToNepali(ddlMonthEng.SelectedValue + "/" + ddlDayEng.SelectedValue + "/" + txtYearEng.Text);
        ddlDay.SelectedValue = nepdate[0];
        ddlMonth.SelectedValue = nepdate[1];
        txtYear.Text = nepdate[2];
    }

    protected void txtYearEng_TextChanged(object sender, EventArgs e)
    {
        string[] nepdate = hf.ConvertEnglishToNepali(ddlMonthEng.SelectedValue + "/" + ddlDayEng.SelectedValue + "/" + txtYearEng.Text);
        ddlDay.SelectedValue = nepdate[0];
        ddlMonth.SelectedValue = nepdate[1];
        txtYear.Text = nepdate[2];
    }
}