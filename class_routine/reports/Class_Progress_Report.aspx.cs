using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;
using Entity.Framework;

public partial class class_routine_reports_Class_Progress_Report : System.Web.UI.Page
{
    HSS_LEVEL LVLEnt = new HSS_LEVEL();
    HSS_LEVELService LVLSer = new HSS_LEVELService();

    Months MEnt = new Months();
    MonthsService MSer = new MonthsService();

    BatchYear BEnt = new BatchYear();
    BatchYearService BSer = new BatchYearService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    ACADEMIC_CALENDAR ACDEnt = new ACADEMIC_CALENDAR();
    ACADEMIC_CALENDARService ACDSer = new ACADEMIC_CALENDARService();

    HSS_SUBJECT SUBEnt = new HSS_SUBJECT();
    HSS_SUBJECTService SUBSer = new HSS_SUBJECTService();

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSrv = new hss_facultyService();

   program PEnt = new program();
   programService PSrv = new programService();

    Section SCEnt = new Section();
    SectionService SCSer = new SectionService();

    HelperFunction hf = new HelperFunction();

    CLASS_PROGRESS_SCHEDULE CPSEnt = new CLASS_PROGRESS_SCHEDULE();
    CLASS_PROGRESS_SCHEDULEService CPSSer = new CLASS_PROGRESS_SCHEDULEService();

    TEACHER_SUBJECT_MAPPING TCMEnt = new TEACHER_SUBJECT_MAPPING();
    TEACHER_SUBJECT_MAPPINGService TCMSer = new TEACHER_SUBJECT_MAPPINGService();

    EntityList theList = new EntityList();

    Employees EMPEnt = new Employees();
    EmployeesService EMPSer = new EmployeesService();

    double workingdays = 0, prevvalue1 = 0, prevvalue2 = 0, prevvalue3 = 0, prevvalue4 = 0, prevvalue5 = 0, prevvalue6 = 0, prevvalue7 = 0, prevvalue8 = 0, prevvalue9 = 0, prevvalue10 = 0, prevvalue11 = 0, prevvalue12 = 0, prevvalue13 = 0, prevvalue14 = 0, prevvalue15 = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFaculty();
            LoadProgram();
            LoadSection();
            loadLevel();

        }


    }

    protected void loadLevel()
    {
        LVLEnt = new HSS_LEVEL();
        ddlLevel.DataSource = LVLSer.GetAll(LVLEnt);
        ddlLevel.DataTextField = "LEVEL_NAME";
        ddlLevel.DataValueField = "LEVEL_NAME";
        ddlLevel.DataBind();
    }

    protected void LoadFaculty()
    {
        FCEnt = new hss_faculty();
        ddlFaculty.DataSource = FCSrv.GetAll(FCEnt);
        ddlFaculty.DataTextField = "FACULTY";
        ddlFaculty.DataValueField = "PK_ID";
        ddlFaculty.DataBind();
        ddlFaculty.Items.Insert(0, "Select");
    }

    protected void LoadProgram()
    {
        PEnt = new program();
        PEnt.FACULTY_ID = ddlFaculty.SelectedValue;
        PEnt.PROGRAM_LEVEL = ddlLevel.SelectedValue;
        ddlProgram.DataSource = PSrv.GetAll(PEnt);
        ddlProgram.DataTextField = "PROGRAM_CODE";
        ddlProgram.DataValueField = "PK_ID";
        ddlProgram.DataBind();
        ddlProgram.Items.Insert(0, "Select");
    }

    protected void LoadBatch()
    {
        BEnt = new BatchYear();
        BEnt.ACTIVE = "1";
        BEnt.SEMESTER = ddlSemester.SelectedValue;
        ddlBatch.DataSource = BSer.GetAll(BEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
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

    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSemester();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        ACDEnt = new ACADEMIC_CALENDAR();
        ACDEnt.BATCH = ddlBatch.SelectedValue;
        ACDEnt.SEMESTER = ddlSemester.SelectedValue;
        gridAcademicCalendar.DataSource = ACDSer.GetAll(ACDEnt);



        gridAcademicCalendar.DataBind();

    }


    protected string getSubjectNameForHeading(string teachersubmapid)
    {
        string subname = "";
        TCMEnt = new TEACHER_SUBJECT_MAPPING();
        TCMEnt.PK_ID = teachersubmapid;
        TCMEnt = (TEACHER_SUBJECT_MAPPING)TCMSer.GetSingle(TCMEnt);
        if (TCMEnt != null)
        {
            SUBEnt = new HSS_SUBJECT();
            SUBEnt.PK_ID = TCMEnt.SUBJECT_ID;
            SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
            if (SUBEnt != null)
            {
                subname = SUBEnt.SUBJECT_CODE;
            }
            EMPEnt = new Employees();
            EMPEnt.EMPLOYEEID = TCMEnt.TEACHER_ID;
            EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
            if (EMPEnt != null)
            {
                subname += "-" + EMPEnt.Abbrevation;
            }
        }
        return subname;
    }







    protected void gridAcademicCalendar_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            GridView HeaderGrid = (GridView)sender;
            GridViewRow HeaderGridRow = new GridViewRow(2, 0, DataControlRowType.Header, DataControlRowState.Insert);
            HeaderGridRow.Style.Add("position", "relative");
            TableCell HeaderCell = new TableCell();

            string sub1 = "", sub2 = "", sub3 = "", sub4 = "", sub5 = "", sub6 = "", sub7 = "", sub8 = "", sub9 = "", sub10 = "", sub11 = "", sub12 = "", sub13 = "", sub14 = "", sub15 = "";


            #region getting and loading all subjects

            EntityList theList = new EntityList();
            //SUBEnt = new HSS_SUBJECT();

            //SUBEnt.SEMESTER = ddlSemester.SelectedValue;
            //theList = SUBSer.GetAll(SUBEnt);
            //foreach (HSS_SUBJECT sub in theList)
            //{
            //    if (sub1 == "")
            //    {
            //        sub1 = sub.PK_ID;
            //        continue;
            //    }
            //    if (sub2 == "")
            //    {
            //        sub2 = sub.PK_ID;
            //        continue;
            //    }
            //    if (sub3 == "")
            //    {
            //        sub3 = sub.PK_ID;
            //        continue;
            //    }
            //    if (sub4 == "")
            //    {
            //        sub4 = sub.PK_ID;
            //        continue;
            //    }
            //    if (sub5 == "")
            //    {
            //        sub5 = sub.PK_ID;
            //        continue;
            //    }
            //    if (sub6 == "")
            //    {
            //        sub6 = sub.PK_ID;
            //        continue;
            //    }
            //}

            TCMEnt = new TEACHER_SUBJECT_MAPPING();
            TCMEnt.SEMESTER = ddlSemester.SelectedValue;
            TCMEnt.BATCH = ddlBatch.SelectedValue;
            theList = TCMSer.GetAll(TCMEnt);
            foreach (TEACHER_SUBJECT_MAPPING tsm in theList)
            {
                if (sub1 == "")
                {
                    sub1 = tsm.PK_ID;
                    continue;
                }
                if (sub2 == "")
                {
                    sub2 = tsm.PK_ID;
                    continue;
                }
                if (sub3 == "")
                {
                    sub3 = tsm.PK_ID;
                    continue;
                }
                if (sub4 == "")
                {
                    sub4 = tsm.PK_ID;
                    continue;
                }
                if (sub5 == "")
                {
                    sub5 = tsm.PK_ID;
                    continue;
                }
                if (sub6 == "")
                {
                    sub6 = tsm.PK_ID;
                    continue;
                }

                if (sub7 == "")
                {
                    sub7 = tsm.PK_ID;
                    continue;
                }
                if (sub8 == "")
                {
                    sub8 = tsm.PK_ID;
                    continue;
                }
                if (sub9 == "")
                {
                    sub9 = tsm.PK_ID;
                    continue;
                }
                if (sub10 == "")
                {
                    sub10 = tsm.PK_ID;
                    continue;
                }
                if (sub11 == "")
                {
                    sub11 = tsm.PK_ID;
                    continue;
                }
                if (sub12 == "")
                {
                    sub12 = tsm.PK_ID;
                    continue;
                }


                if (sub13 == "")
                {
                    sub13 = tsm.PK_ID;
                    continue;
                }
                if (sub14 == "")
                {
                    sub14 = tsm.PK_ID;
                    continue;
                }
                if (sub15 == "")
                {
                    sub15 = tsm.PK_ID;
                    continue;
                }
            }



            #endregion






            HeaderCell = new TableCell();
            HeaderCell.Text = "";
            HeaderCell.ColumnSpan = 4;
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = getSubjectNameForHeading(sub1);
            HeaderCell.ColumnSpan = 2;
            HeaderCell.Style.Add("font-weight", "bold");

            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = getSubjectNameForHeading(sub2);
            HeaderCell.ColumnSpan = 2;
            HeaderCell.Style.Add("font-weight", "bold");
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = getSubjectNameForHeading(sub3);
            HeaderCell.ColumnSpan = 2;
            HeaderCell.Style.Add("font-weight", "bold");

            if (sub3 == "")
            {
                HeaderCell.Visible = false;
            }
            else
            {
                HeaderCell.Visible = true;
            }


            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = getSubjectNameForHeading(sub4);
            HeaderCell.ColumnSpan = 2;
            HeaderCell.Style.Add("font-weight", "bold");
            if (sub4 == "")
            {
                HeaderCell.Visible = false;
            }
            else
            {
                HeaderCell.Visible = true;
            }
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = getSubjectNameForHeading(sub5);
            HeaderCell.ColumnSpan = 2;
            HeaderCell.Style.Add("font-weight", "bold");
            if (sub5 == "")
            {
                HeaderCell.Visible = false;
            }
            else
            {
                HeaderCell.Visible = true;
            }
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = getSubjectNameForHeading(sub6);
            HeaderCell.ColumnSpan = 2;
            HeaderCell.Style.Add("font-weight", "bold");
            if (sub6 == "")
            {
                HeaderCell.Visible = false;
            }
            else
            {
                HeaderCell.Visible = true;
            }
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = getSubjectNameForHeading(sub7);
            HeaderCell.ColumnSpan = 2;
            HeaderCell.Style.Add("font-weight", "bold");
            if (sub7 == "")
            {
                HeaderCell.Visible = false;
            }
            else
            {
                HeaderCell.Visible = true;
            }
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = getSubjectNameForHeading(sub8);
            HeaderCell.ColumnSpan = 2;
            HeaderCell.Style.Add("font-weight", "bold");
            if (sub8 == "")
            {
                HeaderCell.Visible = false;
            }
            else
            {
                HeaderCell.Visible = true;
            }
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = getSubjectNameForHeading(sub9);
            HeaderCell.ColumnSpan = 2;
            HeaderCell.Style.Add("font-weight", "bold");
            if (sub9 == "")
            {
                HeaderCell.Visible = false;
            }
            else
            {
                HeaderCell.Visible = true;
            }
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = getSubjectNameForHeading(sub10);
            HeaderCell.ColumnSpan = 2;
            HeaderCell.Style.Add("font-weight", "bold");
            if (sub10 == "")
            {
                HeaderCell.Visible = false;
            }
            else
            {
                HeaderCell.Visible = true;
            }
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = getSubjectNameForHeading(sub11);
            HeaderCell.ColumnSpan = 2;
            HeaderCell.Style.Add("font-weight", "bold");
            if (sub11 == "")
            {
                HeaderCell.Visible = false;
            }
            else
            {
                HeaderCell.Visible = true;
            }
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = getSubjectNameForHeading(sub12);
            HeaderCell.ColumnSpan = 2;
            HeaderCell.Style.Add("font-weight", "bold");
            if (sub12 == "")
            {
                HeaderCell.Visible = false;
            }
            else
            {
                HeaderCell.Visible = true;
            }
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = getSubjectNameForHeading(sub13);
            HeaderCell.ColumnSpan = 2;
            HeaderCell.Style.Add("font-weight", "bold");
            if (sub13 == "")
            {
                HeaderCell.Visible = false;
            }
            else
            {
                HeaderCell.Visible = true;
            }
            HeaderGridRow.Cells.Add(HeaderCell);


            HeaderCell = new TableCell();
            HeaderCell.Text = getSubjectNameForHeading(sub14);
            HeaderCell.ColumnSpan = 2;
            HeaderCell.Style.Add("font-weight", "bold");
            if (sub14 == "")
            {
                HeaderCell.Visible = false;
            }
            else
            {
                HeaderCell.Visible = true;
            }
            HeaderGridRow.Cells.Add(HeaderCell);

            HeaderCell = new TableCell();
            HeaderCell.Text = getSubjectNameForHeading(sub15);
            HeaderCell.ColumnSpan = 2;
            HeaderCell.Style.Add("font-weight", "bold");
            if (sub15 == "")
            {
                HeaderCell.Visible = false;
            }
            else
            {
                HeaderCell.Visible = true;
            }
            HeaderGridRow.Cells.Add(HeaderCell);



            HeaderCell = new TableCell();
            HeaderCell.Text = "";

            HeaderGridRow.Cells.Add(HeaderCell);




            gridAcademicCalendar.Controls[0].Controls.AddAt(0, HeaderGridRow);

        }


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblMonth = e.Row.FindControl("lblMonth") as Label;
            Label lblDay = e.Row.FindControl("lblDay") as Label;
            Label lblDayofWeek = e.Row.FindControl("lblDayofWeek") as Label;
            Label lblMonthName = e.Row.FindControl("lblMonthName") as Label;
            Label lblDays = e.Row.FindControl("lblDays") as Label;
            Label lblWorkingDays = e.Row.FindControl("lblWorkingDays") as Label;
            Label lblSubject1 = e.Row.FindControl("lblSubject1") as Label;
            Label lblSubject2 = e.Row.FindControl("lblSubject2") as Label;
            Label lblSubject3 = e.Row.FindControl("lblSubject3") as Label;
            Label lblSubject4 = e.Row.FindControl("lblSubject4") as Label;
            Label lblSubject5 = e.Row.FindControl("lblSubject5") as Label;
            Label lblSubject6 = e.Row.FindControl("lblSubject6") as Label;
            Label lblSubject7 = e.Row.FindControl("lblSubject7") as Label;
            Label lblSubject8 = e.Row.FindControl("lblSubject8") as Label;
            Label lblSubject9 = e.Row.FindControl("lblSubject9") as Label;
            Label lblSubject10 = e.Row.FindControl("lblSubject10") as Label;
            Label lblSubject11 = e.Row.FindControl("lblSubject11") as Label;
            Label lblSubject12 = e.Row.FindControl("lblSubject12") as Label;
            Label lblSubject13 = e.Row.FindControl("lblSubject13") as Label;
            Label lblSubject14 = e.Row.FindControl("lblSubject14") as Label;
            Label lblSubject15 = e.Row.FindControl("lblSubject15") as Label;




            Label lblClassHr1 = e.Row.FindControl("lblClassHr1") as Label;
            Label lblCumClassHr1 = e.Row.FindControl("lblCumClassHr1") as Label;

            Label lblClassHr2 = e.Row.FindControl("lblClassHr2") as Label;
            Label lblCumClassHr2 = e.Row.FindControl("lblCumClassHr2") as Label;

            Label lblClassHr3 = e.Row.FindControl("lblClassHr3") as Label;
            Label lblCumClassHr3 = e.Row.FindControl("lblCumClassHr3") as Label;

            Label lblClassHr4 = e.Row.FindControl("lblClassHr4") as Label;
            Label lblCumClassHr4 = e.Row.FindControl("lblCumClassHr4") as Label;

            Label lblClassHr5 = e.Row.FindControl("lblClassHr5") as Label;
            Label lblCumClassHr5 = e.Row.FindControl("lblCumClassHr5") as Label;

            Label lblClassHr6 = e.Row.FindControl("lblClassHr6") as Label;
            Label lblCumClassHr6 = e.Row.FindControl("lblCumClassHr6") as Label;


            Label lblClassHr7 = e.Row.FindControl("lblClassHr7") as Label;
            Label lblCumClassHr7 = e.Row.FindControl("lblCumClassHr7") as Label;

            Label lblClassHr8 = e.Row.FindControl("lblClassHr8") as Label;
            Label lblCumClassHr8 = e.Row.FindControl("lblCumClassHr8") as Label;

            Label lblClassHr9 = e.Row.FindControl("lblClassHr9") as Label;
            Label lblCumClassHr9 = e.Row.FindControl("lblCumClassHr9") as Label;

            Label lblClassHr10 = e.Row.FindControl("lblClassHr10") as Label;
            Label lblCumClassHr10 = e.Row.FindControl("lblCumClassHr10") as Label;

            Label lblClassHr11 = e.Row.FindControl("lblClassHr11") as Label;
            Label lblCumClassHr11 = e.Row.FindControl("lblCumClassHr11") as Label;

            Label lblClassHr12 = e.Row.FindControl("lblClassHr12") as Label;
            Label lblCumClassHr12 = e.Row.FindControl("lblCumClassHr12") as Label;

            Label lblClassHr13 = e.Row.FindControl("lblClassHr13") as Label;
            Label lblCumClassHr13 = e.Row.FindControl("lblCumClassHr13") as Label;

            Label lblClassHr14 = e.Row.FindControl("lblClassHr14") as Label;
            Label lblCumClassHr14 = e.Row.FindControl("lblCumClassHr14") as Label;

            Label lblClassHr15 = e.Row.FindControl("lblClassHr15") as Label;
            Label lblCumClassHr15 = e.Row.FindControl("lblCumClassHr15") as Label;




            Label lblRemarks = e.Row.FindControl("lblRemarks") as Label;


            workingdays = workingdays + Convert.ToDouble(lblWorkingDays.Text);


            lblDays.Text = workingdays.ToString();




            lblDayofWeek.Text = lblDayofWeek.Text.Substring(0, 3).ToUpper();

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


            //#region getting and loading all subjects

            //EntityList theList = new EntityList();
            //SUBEnt = new HSS_SUBJECT();
            //SUBEnt.SEMESTER = ddlSemester.SelectedValue;
            //theList = SUBSer.GetAll(SUBEnt);
            //foreach (HSS_SUBJECT sub in theList)
            //{
            //    if (lblSubject1.Text == "")
            //    {
            //        lblSubject1.Text = sub.PK_ID;
            //        continue;
            //    }
            //    if (lblSubject2.Text == "")
            //    {
            //        lblSubject2.Text = sub.PK_ID;
            //        continue;
            //    }

            //    if (lblSubject3.Text == "")
            //    {
            //        lblSubject3.Text = sub.PK_ID;
            //        continue;
            //    }
            //    if (lblSubject4.Text == "")
            //    {
            //        lblSubject4.Text = sub.PK_ID;
            //        continue;
            //    }
            //    if (lblSubject5.Text == "")
            //    {
            //        lblSubject5.Text = sub.PK_ID;
            //        continue;
            //    }
            //    if (lblSubject6.Text == "")
            //    {
            //        lblSubject6.Text = sub.PK_ID;
            //        continue;
            //    }
            //}
            //#endregion

            #region getting and loading all subjects

            EntityList theList = new EntityList();
            TCMEnt = new TEACHER_SUBJECT_MAPPING();
            TCMEnt.SEMESTER = ddlSemester.SelectedValue;
            TCMEnt.BATCH = ddlBatch.SelectedValue;
            theList = TCMSer.GetAll(TCMEnt);
            foreach (TEACHER_SUBJECT_MAPPING tsm in theList)
            {
                if (lblSubject1.Text == "")
                {
                    lblSubject1.Text = tsm.PK_ID;
                    continue;
                }
                if (lblSubject2.Text == "")
                {
                    lblSubject2.Text = tsm.PK_ID;
                    continue;
                }

                if (lblSubject3.Text == "")
                {
                    lblSubject3.Text = tsm.PK_ID;
                    continue;
                }
                if (lblSubject4.Text == "")
                {
                    lblSubject4.Text = tsm.PK_ID;
                    continue;
                }
                if (lblSubject5.Text == "")
                {
                    lblSubject5.Text = tsm.PK_ID;
                    continue;
                }
                if (lblSubject6.Text == "")
                {
                    lblSubject6.Text = tsm.PK_ID;
                    continue;
                }

                if (lblSubject7.Text == "")
                {
                    lblSubject7.Text = tsm.PK_ID;
                    continue;
                }
                if (lblSubject8.Text == "")
                {
                    lblSubject8.Text = tsm.PK_ID;
                    continue;
                }

                if (lblSubject9.Text == "")
                {
                    lblSubject9.Text = tsm.PK_ID;
                    continue;
                }
                if (lblSubject10.Text == "")
                {
                    lblSubject10.Text = tsm.PK_ID;
                    continue;
                }
                if (lblSubject11.Text == "")
                {
                    lblSubject11.Text = tsm.PK_ID;
                    continue;
                }
                if (lblSubject12.Text == "")
                {
                    lblSubject12.Text = tsm.PK_ID;
                    continue;
                }
                if (lblSubject13.Text == "")
                {
                    lblSubject13.Text = tsm.PK_ID;
                    continue;
                }
                if (lblSubject14.Text == "")
                {
                    lblSubject14.Text = tsm.PK_ID;
                    continue;
                }
                if (lblSubject15.Text == "")
                {
                    lblSubject15.Text = tsm.PK_ID;
                    continue;
                }


            }
            #endregion

            #region to get the class hour and aggregate for subject 1

            if (lblSubject1.Text != "" && lblSubject1.Text != null)
            {


                CPSEnt = new CLASS_PROGRESS_SCHEDULE();
                CPSEnt.BATCH = ddlBatch.SelectedValue;
                CPSEnt.SEMESTER = ddlSemester.SelectedValue;
                CPSEnt.CLASS_MONTH = lblMonth.Text;
                CPSEnt.CLASS_DAY = lblDay.Text;
                CPSEnt.SECTION = ddlSection.SelectedValue;

                TCMEnt = new TEACHER_SUBJECT_MAPPING();
                TCMEnt.PK_ID = lblSubject1.Text;
                TCMEnt = (TEACHER_SUBJECT_MAPPING)TCMSer.GetSingle(TCMEnt);
                if (TCMEnt != null)
                {
                    CPSEnt.SUBJECT_ID = TCMEnt.SUBJECT_ID;
                    CPSEnt.TEACHER_ID = TCMEnt.TEACHER_ID;

                    CPSEnt = (CLASS_PROGRESS_SCHEDULE)CPSSer.GetSingle(CPSEnt);
                    if (CPSEnt != null)
                    {
                        lblClassHr1.Text = CPSEnt.CLASS_HOUR;
                        prevvalue1 = prevvalue1 + Convert.ToDouble(lblClassHr1.Text);


                        lblCumClassHr1.Text = prevvalue1.ToString();

                        lblRemarks.Text = lblRemarks.Text + " " + CPSEnt.REMARKS;

                    }
                    else
                    {
                        lblClassHr1.Text = "0";
                        lblCumClassHr1.Text = prevvalue1.ToString();
                    }



                }
            }
            else
            {
                gridAcademicCalendar.Columns[4].Visible = false;
                gridAcademicCalendar.Columns[5].Visible = false;
            }



            #endregion

            #region to get the class hour and aggregate for subject 2

            if (lblSubject2.Text != "" && lblSubject2.Text != null)
            {
                CPSEnt = new CLASS_PROGRESS_SCHEDULE();
                CPSEnt.BATCH = ddlBatch.SelectedValue;
                CPSEnt.SEMESTER = ddlSemester.SelectedValue;
                CPSEnt.CLASS_MONTH = lblMonth.Text;
                CPSEnt.CLASS_DAY = lblDay.Text;

                CPSEnt.SECTION = ddlSection.SelectedValue;

                TCMEnt = new TEACHER_SUBJECT_MAPPING();
                TCMEnt.PK_ID = lblSubject2.Text;
                TCMEnt = (TEACHER_SUBJECT_MAPPING)TCMSer.GetSingle(TCMEnt);
                if (TCMEnt != null)
                {
                    CPSEnt.SUBJECT_ID = TCMEnt.SUBJECT_ID;
                    CPSEnt.TEACHER_ID = TCMEnt.TEACHER_ID;

                    CPSEnt = (CLASS_PROGRESS_SCHEDULE)CPSSer.GetSingle(CPSEnt);
                    if (CPSEnt != null)
                    {
                        lblClassHr2.Text = CPSEnt.CLASS_HOUR;
                        prevvalue2 = prevvalue2 + Convert.ToDouble(lblClassHr2.Text);


                        lblCumClassHr2.Text = prevvalue2.ToString();
                        lblRemarks.Text = lblRemarks.Text + " " + CPSEnt.REMARKS;
                    }
                    else
                    {
                        lblClassHr2.Text = "0";
                        lblCumClassHr2.Text = prevvalue2.ToString();
                    }
                }
            }
            else
            {
                gridAcademicCalendar.Columns[6].Visible = false;
                gridAcademicCalendar.Columns[7].Visible = false;

            }
            #endregion

            #region to get the class hour and aggregate for subject 3
            if (lblSubject3.Text != "" && lblSubject3.Text != null)
            {
                CPSEnt = new CLASS_PROGRESS_SCHEDULE();
                CPSEnt.BATCH = ddlBatch.SelectedValue;
                CPSEnt.SEMESTER = ddlSemester.SelectedValue;
                CPSEnt.CLASS_MONTH = lblMonth.Text;
                CPSEnt.CLASS_DAY = lblDay.Text;

                CPSEnt.SECTION = ddlSection.SelectedValue;

                TCMEnt = new TEACHER_SUBJECT_MAPPING();
                TCMEnt.PK_ID = lblSubject3.Text;
                TCMEnt = (TEACHER_SUBJECT_MAPPING)TCMSer.GetSingle(TCMEnt);
                if (TCMEnt != null)
                {
                    CPSEnt.SUBJECT_ID = TCMEnt.SUBJECT_ID;
                    CPSEnt.TEACHER_ID = TCMEnt.TEACHER_ID;

                    CPSEnt = (CLASS_PROGRESS_SCHEDULE)CPSSer.GetSingle(CPSEnt);
                    if (CPSEnt != null)
                    {
                        lblClassHr3.Text = CPSEnt.CLASS_HOUR;
                        prevvalue3 = prevvalue3 + Convert.ToDouble(lblClassHr3.Text);


                        lblCumClassHr3.Text = prevvalue3.ToString();
                        lblRemarks.Text = lblRemarks.Text + " " + CPSEnt.REMARKS;
                    }
                    else
                    {
                        lblClassHr3.Text = "0";
                        lblCumClassHr3.Text = prevvalue3.ToString();
                    }
                }
            }
            else
            {
                gridAcademicCalendar.Columns[8].Visible = false;
                gridAcademicCalendar.Columns[9].Visible = false;

            }
            #endregion

            #region to get the class hour and aggregate for subject 4
            if (lblSubject4.Text != "" && lblSubject4.Text != null)
            {
                CPSEnt = new CLASS_PROGRESS_SCHEDULE();
                CPSEnt.BATCH = ddlBatch.SelectedValue;
                CPSEnt.SEMESTER = ddlSemester.SelectedValue;
                CPSEnt.CLASS_MONTH = lblMonth.Text;
                CPSEnt.CLASS_DAY = lblDay.Text;

                CPSEnt.SECTION = ddlSection.SelectedValue;
                TCMEnt = new TEACHER_SUBJECT_MAPPING();
                TCMEnt.PK_ID = lblSubject4.Text;
                TCMEnt = (TEACHER_SUBJECT_MAPPING)TCMSer.GetSingle(TCMEnt);
                if (TCMEnt != null)
                {
                    CPSEnt.SUBJECT_ID = TCMEnt.SUBJECT_ID;
                    CPSEnt.TEACHER_ID = TCMEnt.TEACHER_ID;

                    CPSEnt = (CLASS_PROGRESS_SCHEDULE)CPSSer.GetSingle(CPSEnt);
                    if (CPSEnt != null)
                    {
                        lblClassHr4.Text = CPSEnt.CLASS_HOUR;
                        prevvalue4 = prevvalue4 + Convert.ToDouble(lblClassHr4.Text);


                        lblCumClassHr4.Text = prevvalue4.ToString();
                        lblRemarks.Text = lblRemarks.Text + " " + CPSEnt.REMARKS;
                    }
                    else
                    {
                        lblClassHr4.Text = "0";
                        lblCumClassHr4.Text = prevvalue4.ToString();
                    }
                }
            }
            else
            {
                gridAcademicCalendar.Columns[10].Visible = false;
                gridAcademicCalendar.Columns[11].Visible = false;

            }
            #endregion

            #region to get the class hour and aggregate for subject 5
            if (lblSubject5.Text != "" && lblSubject5.Text != null)
            {
                CPSEnt = new CLASS_PROGRESS_SCHEDULE();
                CPSEnt.BATCH = ddlBatch.SelectedValue;
                CPSEnt.SEMESTER = ddlSemester.SelectedValue;
                CPSEnt.CLASS_MONTH = lblMonth.Text;
                CPSEnt.CLASS_DAY = lblDay.Text;

                CPSEnt.SECTION = ddlSection.SelectedValue;
                TCMEnt = new TEACHER_SUBJECT_MAPPING();
                TCMEnt.PK_ID = lblSubject5.Text;
                TCMEnt = (TEACHER_SUBJECT_MAPPING)TCMSer.GetSingle(TCMEnt);
                if (TCMEnt != null)
                {
                    CPSEnt.SUBJECT_ID = TCMEnt.SUBJECT_ID;
                    CPSEnt.TEACHER_ID = TCMEnt.TEACHER_ID;

                    CPSEnt = (CLASS_PROGRESS_SCHEDULE)CPSSer.GetSingle(CPSEnt);
                    if (CPSEnt != null)
                    {
                        lblClassHr5.Text = CPSEnt.CLASS_HOUR;
                        prevvalue5 = prevvalue5 + Convert.ToDouble(lblClassHr5.Text);


                        lblCumClassHr5.Text = prevvalue5.ToString();
                        lblRemarks.Text = lblRemarks.Text + " " + CPSEnt.REMARKS;
                    }
                    else
                    {
                        lblClassHr5.Text = "0";
                        lblCumClassHr5.Text = prevvalue5.ToString();
                    }
                }
            }
            else
            {
                gridAcademicCalendar.Columns[12].Visible = false;
                gridAcademicCalendar.Columns[13].Visible = false;
            }
            #endregion

            #region to get the class hour and aggregate for subject 6
            if (lblSubject6.Text != "" && lblSubject6.Text != null)
            {
                CPSEnt = new CLASS_PROGRESS_SCHEDULE();
                CPSEnt.BATCH = ddlBatch.SelectedValue;
                CPSEnt.SEMESTER = ddlSemester.SelectedValue;
                CPSEnt.CLASS_MONTH = lblMonth.Text;
                CPSEnt.CLASS_DAY = lblDay.Text;

                CPSEnt.SECTION = ddlSection.SelectedValue;
                TCMEnt = new TEACHER_SUBJECT_MAPPING();
                TCMEnt.PK_ID = lblSubject6.Text;
                TCMEnt = (TEACHER_SUBJECT_MAPPING)TCMSer.GetSingle(TCMEnt);
                if (TCMEnt != null)
                {
                    CPSEnt.SUBJECT_ID = TCMEnt.SUBJECT_ID;
                    CPSEnt.TEACHER_ID = TCMEnt.TEACHER_ID;

                    CPSEnt = (CLASS_PROGRESS_SCHEDULE)CPSSer.GetSingle(CPSEnt);
                    if (CPSEnt != null)
                    {
                        lblClassHr6.Text = CPSEnt.CLASS_HOUR;
                        prevvalue6 = prevvalue6 + Convert.ToDouble(lblClassHr6.Text);


                        lblCumClassHr6.Text = prevvalue6.ToString();
                        lblRemarks.Text = lblRemarks.Text + " " + CPSEnt.REMARKS;
                    }
                    else
                    {
                        lblClassHr6.Text = "0";
                        lblCumClassHr6.Text = prevvalue6.ToString();
                    }
                }
            }
            else
            {
                gridAcademicCalendar.Columns[14].Visible = false;
                gridAcademicCalendar.Columns[15].Visible = false;

            }
            #endregion

            #region to get the class hour and aggregate for subject 7
            if (lblSubject7.Text != "" && lblSubject7.Text != null)
            {
                CPSEnt = new CLASS_PROGRESS_SCHEDULE();
                CPSEnt.BATCH = ddlBatch.SelectedValue;
                CPSEnt.SEMESTER = ddlSemester.SelectedValue;
                CPSEnt.CLASS_MONTH = lblMonth.Text;
                CPSEnt.CLASS_DAY = lblDay.Text;

                CPSEnt.SECTION = ddlSection.SelectedValue;
                TCMEnt = new TEACHER_SUBJECT_MAPPING();
                TCMEnt.PK_ID = lblSubject7.Text;
                TCMEnt = (TEACHER_SUBJECT_MAPPING)TCMSer.GetSingle(TCMEnt);
                if (TCMEnt != null)
                {
                    CPSEnt.SUBJECT_ID = TCMEnt.SUBJECT_ID;
                    CPSEnt.TEACHER_ID = TCMEnt.TEACHER_ID;

                    CPSEnt = (CLASS_PROGRESS_SCHEDULE)CPSSer.GetSingle(CPSEnt);
                    if (CPSEnt != null)
                    {
                        lblClassHr7.Text = CPSEnt.CLASS_HOUR;
                        prevvalue7 = prevvalue7 + Convert.ToDouble(lblClassHr7.Text);


                        lblCumClassHr7.Text = prevvalue7.ToString();
                        lblRemarks.Text = lblRemarks.Text + " " + CPSEnt.REMARKS;
                    }
                    else
                    {
                        lblClassHr7.Text = "0";
                        lblCumClassHr7.Text = prevvalue7.ToString();
                    }
                }
            }
            else
            {
                gridAcademicCalendar.Columns[16].Visible = false;
                gridAcademicCalendar.Columns[17].Visible = false;

            }
            #endregion

            #region to get the class hour and aggregate for subject 8
            if (lblSubject8.Text != "" && lblSubject8.Text != null)
            {
                CPSEnt = new CLASS_PROGRESS_SCHEDULE();
                CPSEnt.BATCH = ddlBatch.SelectedValue;
                CPSEnt.SEMESTER = ddlSemester.SelectedValue;
                CPSEnt.CLASS_MONTH = lblMonth.Text;
                CPSEnt.CLASS_DAY = lblDay.Text;

                CPSEnt.SECTION = ddlSection.SelectedValue;
                TCMEnt = new TEACHER_SUBJECT_MAPPING();
                TCMEnt.PK_ID = lblSubject8.Text;
                TCMEnt = (TEACHER_SUBJECT_MAPPING)TCMSer.GetSingle(TCMEnt);
                if (TCMEnt != null)
                {
                    CPSEnt.SUBJECT_ID = TCMEnt.SUBJECT_ID;
                    CPSEnt.TEACHER_ID = TCMEnt.TEACHER_ID;

                    CPSEnt = (CLASS_PROGRESS_SCHEDULE)CPSSer.GetSingle(CPSEnt);
                    if (CPSEnt != null)
                    {
                        lblClassHr8.Text = CPSEnt.CLASS_HOUR;
                        prevvalue8 = prevvalue8 + Convert.ToDouble(lblClassHr8.Text);


                        lblCumClassHr8.Text = prevvalue8.ToString();
                        lblRemarks.Text = lblRemarks.Text + " " + CPSEnt.REMARKS;
                    }
                    else
                    {
                        lblClassHr8.Text = "0";
                        lblCumClassHr8.Text = prevvalue8.ToString();
                    }
                }
            }
            else
            {
                gridAcademicCalendar.Columns[18].Visible = false;
                gridAcademicCalendar.Columns[19].Visible = false;
            }
            #endregion


            #region to get the class hour and aggregate for subject 9

            if (lblSubject9.Text != "" && lblSubject9.Text != null)
            {

                CPSEnt = new CLASS_PROGRESS_SCHEDULE();
                CPSEnt.BATCH = ddlBatch.SelectedValue;
                CPSEnt.SEMESTER = ddlSemester.SelectedValue;
                CPSEnt.CLASS_MONTH = lblMonth.Text;
                CPSEnt.CLASS_DAY = lblDay.Text;

                CPSEnt.SECTION = ddlSection.SelectedValue;
                TCMEnt = new TEACHER_SUBJECT_MAPPING();
                TCMEnt.PK_ID = lblSubject9.Text;
                TCMEnt = (TEACHER_SUBJECT_MAPPING)TCMSer.GetSingle(TCMEnt);
                if (TCMEnt != null)
                {
                    CPSEnt.SUBJECT_ID = TCMEnt.SUBJECT_ID;
                    CPSEnt.TEACHER_ID = TCMEnt.TEACHER_ID;

                    CPSEnt = (CLASS_PROGRESS_SCHEDULE)CPSSer.GetSingle(CPSEnt);
                    if (CPSEnt != null)
                    {
                        lblClassHr9.Text = CPSEnt.CLASS_HOUR;
                        prevvalue9 = prevvalue9 + Convert.ToDouble(lblClassHr9.Text);


                        lblCumClassHr9.Text = prevvalue9.ToString();
                        lblRemarks.Text = lblRemarks.Text + " " + CPSEnt.REMARKS;
                    }
                    else
                    {
                        lblClassHr9.Text = "0";
                        lblCumClassHr9.Text = prevvalue9.ToString();
                    }
                }
            }
            else
            {
                gridAcademicCalendar.Columns[20].Visible = false;
                gridAcademicCalendar.Columns[21].Visible = false;

            }
            #endregion


            #region to get the class hour and aggregate for subject 10
            if (lblSubject10.Text != "" && lblSubject10.Text != null)
            {
                CPSEnt = new CLASS_PROGRESS_SCHEDULE();
                CPSEnt.BATCH = ddlBatch.SelectedValue;
                CPSEnt.SEMESTER = ddlSemester.SelectedValue;
                CPSEnt.CLASS_MONTH = lblMonth.Text;
                CPSEnt.CLASS_DAY = lblDay.Text;

                CPSEnt.SECTION = ddlSection.SelectedValue;
                TCMEnt = new TEACHER_SUBJECT_MAPPING();
                TCMEnt.PK_ID = lblSubject10.Text;
                TCMEnt = (TEACHER_SUBJECT_MAPPING)TCMSer.GetSingle(TCMEnt);
                if (TCMEnt != null)
                {
                    CPSEnt.SUBJECT_ID = TCMEnt.SUBJECT_ID;
                    CPSEnt.TEACHER_ID = TCMEnt.TEACHER_ID;

                    CPSEnt = (CLASS_PROGRESS_SCHEDULE)CPSSer.GetSingle(CPSEnt);
                    if (CPSEnt != null)
                    {
                        lblClassHr10.Text = CPSEnt.CLASS_HOUR;
                        prevvalue10 = prevvalue10 + Convert.ToDouble(lblClassHr10.Text);


                        lblCumClassHr10.Text = prevvalue10.ToString();
                        lblRemarks.Text = lblRemarks.Text + " " + CPSEnt.REMARKS;
                    }
                    else
                    {
                        lblClassHr10.Text = "0";
                        lblCumClassHr10.Text = prevvalue10.ToString();
                    }
                }
            }
            else
            {
                gridAcademicCalendar.Columns[22].Visible = false;
                gridAcademicCalendar.Columns[23].Visible = false;
            }
            #endregion

            #region to get the class hour and aggregate for subject 11
            if (lblSubject11.Text != "" && lblSubject11.Text != null)
            {
                CPSEnt = new CLASS_PROGRESS_SCHEDULE();
                CPSEnt.BATCH = ddlBatch.SelectedValue;
                CPSEnt.SEMESTER = ddlSemester.SelectedValue;
                CPSEnt.CLASS_MONTH = lblMonth.Text;
                CPSEnt.CLASS_DAY = lblDay.Text;

                CPSEnt.SECTION = ddlSection.SelectedValue;
                TCMEnt = new TEACHER_SUBJECT_MAPPING();
                TCMEnt.PK_ID = lblSubject11.Text;
                TCMEnt = (TEACHER_SUBJECT_MAPPING)TCMSer.GetSingle(TCMEnt);
                if (TCMEnt != null)
                {
                    CPSEnt.SUBJECT_ID = TCMEnt.SUBJECT_ID;
                    CPSEnt.TEACHER_ID = TCMEnt.TEACHER_ID;

                    CPSEnt = (CLASS_PROGRESS_SCHEDULE)CPSSer.GetSingle(CPSEnt);
                    if (CPSEnt != null)
                    {
                        lblClassHr11.Text = CPSEnt.CLASS_HOUR;
                        prevvalue11 = prevvalue11 + Convert.ToDouble(lblClassHr11.Text);


                        lblCumClassHr11.Text = prevvalue11.ToString();
                        lblRemarks.Text = lblRemarks.Text + " " + CPSEnt.REMARKS;
                    }
                    else
                    {
                        lblClassHr11.Text = "0";
                        lblCumClassHr11.Text = prevvalue11.ToString();
                    }
                }
            }
            else
            {
                gridAcademicCalendar.Columns[24].Visible = false;
                gridAcademicCalendar.Columns[25].Visible = false;

            }
            #endregion

            #region to get the class hour and aggregate for subject 12
            if (lblSubject12.Text != "" && lblSubject12.Text != null)
            {
                CPSEnt = new CLASS_PROGRESS_SCHEDULE();
                CPSEnt.BATCH = ddlBatch.SelectedValue;
                CPSEnt.SEMESTER = ddlSemester.SelectedValue;
                CPSEnt.CLASS_MONTH = lblMonth.Text;
                CPSEnt.CLASS_DAY = lblDay.Text;

                CPSEnt.SECTION = ddlSection.SelectedValue;
                TCMEnt = new TEACHER_SUBJECT_MAPPING();
                TCMEnt.PK_ID = lblSubject12.Text;
                TCMEnt = (TEACHER_SUBJECT_MAPPING)TCMSer.GetSingle(TCMEnt);
                if (TCMEnt != null)
                {
                    CPSEnt.SUBJECT_ID = TCMEnt.SUBJECT_ID;
                    CPSEnt.TEACHER_ID = TCMEnt.TEACHER_ID;

                    CPSEnt = (CLASS_PROGRESS_SCHEDULE)CPSSer.GetSingle(CPSEnt);
                    if (CPSEnt != null)
                    {
                        lblClassHr12.Text = CPSEnt.CLASS_HOUR;
                        prevvalue12 = prevvalue12 + Convert.ToDouble(lblClassHr12.Text);


                        lblCumClassHr12.Text = prevvalue12.ToString();
                        lblRemarks.Text = lblRemarks.Text + " " + CPSEnt.REMARKS;
                    }
                    else
                    {
                        lblClassHr12.Text = "0";
                        lblCumClassHr12.Text = prevvalue12.ToString();
                    }
                }
            }
            else
            {
                gridAcademicCalendar.Columns[26].Visible = false;
                gridAcademicCalendar.Columns[27].Visible = false;

            }
            #endregion

            #region to get the class hour and aggregate for subject 13
            if (lblSubject13.Text != "" && lblSubject13.Text != null)
            {
                CPSEnt = new CLASS_PROGRESS_SCHEDULE();
                CPSEnt.BATCH = ddlBatch.SelectedValue;
                CPSEnt.SEMESTER = ddlSemester.SelectedValue;
                CPSEnt.CLASS_MONTH = lblMonth.Text;
                CPSEnt.CLASS_DAY = lblDay.Text;

                CPSEnt.SECTION = ddlSection.SelectedValue;
                TCMEnt = new TEACHER_SUBJECT_MAPPING();
                TCMEnt.PK_ID = lblSubject13.Text;
                TCMEnt = (TEACHER_SUBJECT_MAPPING)TCMSer.GetSingle(TCMEnt);
                if (TCMEnt != null)
                {
                    CPSEnt.SUBJECT_ID = TCMEnt.SUBJECT_ID;
                    CPSEnt.TEACHER_ID = TCMEnt.TEACHER_ID;

                    CPSEnt = (CLASS_PROGRESS_SCHEDULE)CPSSer.GetSingle(CPSEnt);
                    if (CPSEnt != null)
                    {
                        lblClassHr13.Text = CPSEnt.CLASS_HOUR;
                        prevvalue13 = prevvalue13 + Convert.ToDouble(lblClassHr13.Text);


                        lblCumClassHr13.Text = prevvalue13.ToString();
                        lblRemarks.Text = lblRemarks.Text + " " + CPSEnt.REMARKS;
                    }
                    else
                    {
                        lblClassHr13.Text = "0";
                        lblCumClassHr13.Text = prevvalue13.ToString();
                    }
                }
            }
            else
            {
                gridAcademicCalendar.Columns[28].Visible = false;
                gridAcademicCalendar.Columns[29].Visible = false;

            }
            #endregion


            #region to get the class hour and aggregate for subject 14
            if (lblSubject14.Text != "" && lblSubject14.Text != null)
            {
                CPSEnt = new CLASS_PROGRESS_SCHEDULE();
                CPSEnt.BATCH = ddlBatch.SelectedValue;
                CPSEnt.SEMESTER = ddlSemester.SelectedValue;
                CPSEnt.CLASS_MONTH = lblMonth.Text;
                CPSEnt.CLASS_DAY = lblDay.Text;

                CPSEnt.SECTION = ddlSection.SelectedValue;
                TCMEnt = new TEACHER_SUBJECT_MAPPING();
                TCMEnt.PK_ID = lblSubject14.Text;
                TCMEnt = (TEACHER_SUBJECT_MAPPING)TCMSer.GetSingle(TCMEnt);
                if (TCMEnt != null)
                {
                    CPSEnt.SUBJECT_ID = TCMEnt.SUBJECT_ID;
                    CPSEnt.TEACHER_ID = TCMEnt.TEACHER_ID;

                    CPSEnt = (CLASS_PROGRESS_SCHEDULE)CPSSer.GetSingle(CPSEnt);
                    if (CPSEnt != null)
                    {
                        lblClassHr14.Text = CPSEnt.CLASS_HOUR;
                        prevvalue14 = prevvalue14 + Convert.ToDouble(lblClassHr14.Text);


                        lblCumClassHr14.Text = prevvalue14.ToString();
                        lblRemarks.Text = lblRemarks.Text + " " + CPSEnt.REMARKS;
                    }
                    else
                    {
                        lblClassHr14.Text = "0";
                        lblCumClassHr14.Text = prevvalue14.ToString();
                    }
                }
            }
            else
            {
                gridAcademicCalendar.Columns[30].Visible = false;
                gridAcademicCalendar.Columns[31].Visible = false;

            }
            #endregion

            #region to get the class hour and aggregate for subject 15
            if (lblSubject15.Text != "" && lblSubject15.Text != null)
            {

                CPSEnt = new CLASS_PROGRESS_SCHEDULE();
                CPSEnt.BATCH = ddlBatch.SelectedValue;
                CPSEnt.SEMESTER = ddlSemester.SelectedValue;
                CPSEnt.CLASS_MONTH = lblMonth.Text;
                CPSEnt.CLASS_DAY = lblDay.Text;

                CPSEnt.SECTION = ddlSection.SelectedValue;
                TCMEnt = new TEACHER_SUBJECT_MAPPING();
                TCMEnt.PK_ID = lblSubject15.Text;
                TCMEnt = (TEACHER_SUBJECT_MAPPING)TCMSer.GetSingle(TCMEnt);
                if (TCMEnt != null)
                {
                    CPSEnt.SUBJECT_ID = TCMEnt.SUBJECT_ID;
                    CPSEnt.TEACHER_ID = TCMEnt.TEACHER_ID;

                    CPSEnt = (CLASS_PROGRESS_SCHEDULE)CPSSer.GetSingle(CPSEnt);
                    if (CPSEnt != null)
                    {
                        lblClassHr15.Text = CPSEnt.CLASS_HOUR;
                        prevvalue15 = prevvalue15 + Convert.ToDouble(lblClassHr15.Text);


                        lblCumClassHr15.Text = prevvalue15.ToString();
                        lblRemarks.Text = lblRemarks.Text + " " + CPSEnt.REMARKS;
                    }
                    else
                    {
                        lblClassHr15.Text = "0";
                        lblCumClassHr15.Text = prevvalue15.ToString();
                    }
                }
            }
            else
            {
                gridAcademicCalendar.Columns[32].Visible = false;
                gridAcademicCalendar.Columns[33].Visible = false;

            }
            #endregion

            //#region to display or columns according to number of subject

            //if (lblSubject3.Text == "")
            //{
            //    gridAcademicCalendar.Columns[8].Visible = false;
            //    gridAcademicCalendar.Columns[9].Visible = false;

            //    gridAcademicCalendar.Columns[10].Visible = false;
            //    gridAcademicCalendar.Columns[11].Visible = false;

            //    gridAcademicCalendar.Columns[12].Visible = false;
            //    gridAcademicCalendar.Columns[13].Visible = false;

            //    gridAcademicCalendar.Columns[14].Visible = false;
            //    gridAcademicCalendar.Columns[15].Visible = false;
            //}
            //else
            //{
            //    gridAcademicCalendar.Columns[8].Visible = true;
            //    gridAcademicCalendar.Columns[9].Visible = true;

            //    gridAcademicCalendar.Columns[10].Visible = true;
            //    gridAcademicCalendar.Columns[11].Visible = true;

            //    gridAcademicCalendar.Columns[12].Visible = true;
            //    gridAcademicCalendar.Columns[13].Visible = true;

            //    gridAcademicCalendar.Columns[14].Visible = true;
            //    gridAcademicCalendar.Columns[15].Visible = true;
            //}

            //if (lblSubject4.Text == "")
            //{


            //    gridAcademicCalendar.Columns[10].Visible = false;
            //    gridAcademicCalendar.Columns[11].Visible = false;

            //    gridAcademicCalendar.Columns[12].Visible = false;
            //    gridAcademicCalendar.Columns[13].Visible = false;

            //    gridAcademicCalendar.Columns[14].Visible = false;
            //    gridAcademicCalendar.Columns[15].Visible = false;
            //}
            //else
            //{


            //    gridAcademicCalendar.Columns[10].Visible = true;
            //    gridAcademicCalendar.Columns[11].Visible = true;

            //    gridAcademicCalendar.Columns[12].Visible = true;
            //    gridAcademicCalendar.Columns[13].Visible = true;

            //    gridAcademicCalendar.Columns[14].Visible = true;
            //    gridAcademicCalendar.Columns[15].Visible = true;
            //}

            //if (lblSubject5.Text == "")
            //{

            //    gridAcademicCalendar.Columns[12].Visible = false;
            //    gridAcademicCalendar.Columns[13].Visible = false;

            //    gridAcademicCalendar.Columns[14].Visible = false;
            //    gridAcademicCalendar.Columns[15].Visible = false;
            //}
            //else
            //{

            //    gridAcademicCalendar.Columns[12].Visible = true;
            //    gridAcademicCalendar.Columns[13].Visible = true;

            //    gridAcademicCalendar.Columns[14].Visible = true;
            //    gridAcademicCalendar.Columns[15].Visible = true;
            //}
            //if (lblSubject6.Text == "")
            //{


            //    gridAcademicCalendar.Columns[14].Visible = false;
            //    gridAcademicCalendar.Columns[15].Visible = false;
            //}
            //else
            //{

            //    gridAcademicCalendar.Columns[14].Visible = true;
            //    gridAcademicCalendar.Columns[15].Visible = true;
            //}
            //#endregion



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
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
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
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProgram.SelectedValue != "Select")
        {
            LoadSemester();
        }
    }
    
}