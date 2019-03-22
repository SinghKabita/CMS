using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;
using Entity.Framework;
using System.Globalization;

public partial class class_routine_reports_Routine_Print : System.Web.UI.Page
{
    hss_faculty FEnt = new hss_faculty();
    hss_facultyService FSer = new hss_facultyService();

    HSS_LEVEL LVLEnt = new HSS_LEVEL();
    HSS_LEVELService LVLSer = new HSS_LEVELService();

    OFFICE OFCEnt = new OFFICE();
    officeService OFCSer = new officeService();

    BatchYear BTCEnt = new BatchYear();
    BatchYearService BTCSer = new BatchYearService();

    semester SMSEnt = new semester();
    semesterService SMSSer = new semesterService();

    Section SCEnt = new Section();
    SectionService SCSer = new SectionService();

    ACADEMIC_CALENDAR ACEnt = new ACADEMIC_CALENDAR();
    ACADEMIC_CALENDARService ACSer = new ACADEMIC_CALENDARService();

    HSS_SUBJECT SUBEnt = new HSS_SUBJECT();
    HSS_SUBJECTService SUBSer = new HSS_SUBJECTService();

    TEACHER_SUBJECT_MAPPING TSUBEnt = new TEACHER_SUBJECT_MAPPING();
    TEACHER_SUBJECT_MAPPINGService TSUBSer = new TEACHER_SUBJECT_MAPPINGService();

    Employees EMPEnt = new Employees();
    EmployeesService EMPSer = new EmployeesService();

    program PEnt = new program();
    programService PSer = new programService();

    PERIOD PRDEnt = new PERIOD();
    PERIODService PRDSer = new PERIODService();

    ROUTINE RTEnt = new ROUTINE();
    ROUTINEService RTSer = new ROUTINEService();

    HelperFunction hf = new HelperFunction();


    ACTIVE_SEMESTER_BATCH ACTEnt = new ACTIVE_SEMESTER_BATCH();
    ACTIVE_SEMESTER_BATCHService ACTSer = new ACTIVE_SEMESTER_BATCHService();

    Months MEnt = new Months();
    MonthsService MSer = new MonthsService();

    static string date1 = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadSection();
            loadLevel();
            loadFaculty();
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
        ddlSemester.Items.Insert(0, "Select");
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }

    protected void LoadSemester()
    {
        EntityList theList = new EntityList();
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

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSemester();
    }

    protected void LoadGrid()
    {
        hide.Visible = true;
        PRDEnt = new PERIOD();
        PRDEnt.PROGRAM_ID = ddlProgram.SelectedValue;
        PRDEnt.SEMESTER_ID = ddlSemester.SelectedValue;
        PRDEnt.SECTION_ID = ddlSection.SelectedValue;
        gridRoutine1.DataSource = PRDSer.GetAll(PRDEnt);
        gridRoutine1.DataBind();
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
    string sunDate = "";
    string monDate = "";
    string tuesDate = "";
    string wedDate = "";
    string thursDate = "";
    string friDate = "";
    string satDate = "";

    protected void gridRoutine1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            string[] date;
            string String_Date = "";
            Label lblSunDateH = e.Row.FindControl("lblSunDateH") as Label;
            Label lblMonDateH = e.Row.FindControl("lblMonDateH") as Label;
            Label lblTuesDateH = e.Row.FindControl("lblTuesDateH") as Label;
            Label lblWedDateH = e.Row.FindControl("lblWedDateH") as Label;
            Label lblThursDateH = e.Row.FindControl("lblThursDateH") as Label;
            Label lblFriDateH = e.Row.FindControl("lblFriDateH") as Label;
            Label lblSatDateH = e.Row.FindControl("lblSatDateH") as Label;


            #region to load date in header
            ACEnt = new ACADEMIC_CALENDAR();
            ACEnt.CAL_DATE = txtDate.Text;
            ACEnt = (ACADEMIC_CALENDAR)ACSer.GetSingle(ACEnt);
            if (ACEnt != null)
            {
                #region Sunday
                DateTime Routine_Date = Convert.ToDateTime(txtDate.Text).AddDays(-Convert.ToInt32(day_number(ACEnt.CAL_DAY_OF_WEEK)) + 1); // convert to sunday's date
                String_Date = hf.CheckDate(Routine_Date.ToString());  // mm/dd/yyyy format
                date = String_Date.Split('/');
                lblSunDateH.Text = date[1] + "/" + date[0] + "/" + date[2]; // dd/mm/yyyy format
                sunDate = lblSunDateH.Text;
                #endregion
                #region Monday
                Routine_Date = Convert.ToDateTime(String_Date).AddDays(1);
                String_Date = hf.CheckDate(Routine_Date.ToString());
                date = hf.CheckDate(Routine_Date.ToString()).Split('/');
                lblMonDateH.Text = date[1] + "/" + date[0] + "/" + date[2];
                monDate = lblMonDateH.Text;
                #endregion
                #region Tuesday
                Routine_Date = Convert.ToDateTime(String_Date).AddDays(1);
                String_Date = hf.CheckDate(Routine_Date.ToString());
                date = hf.CheckDate(Routine_Date.ToString()).Split('/');
                lblTuesDateH.Text = date[1] + "/" + date[0] + "/" + date[2];
                tuesDate = lblTuesDateH.Text;
                #endregion
                #region Wenesday
                Routine_Date = Convert.ToDateTime(String_Date).AddDays(1);
                String_Date = hf.CheckDate(Routine_Date.ToString());
                date = hf.CheckDate(Routine_Date.ToString()).Split('/');
                lblWedDateH.Text = date[1] + "/" + date[0] + "/" + date[2];
                wedDate = lblWedDateH.Text;
                #endregion
                #region Thursday
                Routine_Date = Convert.ToDateTime(String_Date).AddDays(1);
                String_Date = hf.CheckDate(Routine_Date.ToString());
                date = hf.CheckDate(Routine_Date.ToString()).Split('/');
                lblThursDateH.Text = date[1] + "/" + date[0] + "/" + date[2];
                thursDate = lblThursDateH.Text;
                #endregion
                #region Friday
                Routine_Date = Convert.ToDateTime(String_Date).AddDays(1);
                String_Date = hf.CheckDate(Routine_Date.ToString());
                date = hf.CheckDate(Routine_Date.ToString()).Split('/');
                lblFriDateH.Text = date[1] + "/" + date[0] + "/" + date[2];
                friDate = lblFriDateH.Text;
                #endregion
                #region Saturday
                Routine_Date = Convert.ToDateTime(String_Date).AddDays(1);
                String_Date = hf.CheckDate(Routine_Date.ToString());
                date = hf.CheckDate(Routine_Date.ToString()).Split('/');
                lblSatDateH.Text = date[1] + "/" + date[0] + "/" + date[2];
                satDate = lblSatDateH.Text;
                #endregion
            }

            #endregion
            #region visibility of date in grid header
            if (chkShowDates.Checked == true)
            {
                lblSunDateH.Visible = true;
                lblMonDateH.Visible = true;
                lblTuesDateH.Visible = true;
                lblWedDateH.Visible = true;
                lblThursDateH.Visible = true;
                lblFriDateH.Visible = true;
                lblSatDateH.Visible = true;
            }
            else
            {
                lblSunDateH.Visible = false;
                lblMonDateH.Visible = false;
                lblTuesDateH.Visible = false;
                lblWedDateH.Visible = false;
                lblThursDateH.Visible = false;
                lblFriDateH.Visible = false;
                lblSatDateH.Visible = false;
            }
            #endregion
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSundaySub = e.Row.FindControl("lblSundaySub") as Label;
            Label lblMondaySub = e.Row.FindControl("lblMondaySub") as Label;
            Label lblTuesdaySub = e.Row.FindControl("lblTuesdaySub") as Label;
            Label lblWednesdaySub = e.Row.FindControl("lblWednesdaySub") as Label;
            Label lblThursdaySub = e.Row.FindControl("lblThursdaySub") as Label;
            Label lblFridaySub = e.Row.FindControl("lblFridaySub") as Label;
            Label lblSaturdaySub = e.Row.FindControl("lblSaturdaySub") as Label;

            Label lblPeriodId = e.Row.FindControl("lblPeriodId") as Label;
            Label lblPeriod = e.Row.FindControl("lblPeriod") as Label;
            Label lblTime = e.Row.FindControl("lblTime") as Label;
            Label lblfromTime = e.Row.FindControl("lblfromTime") as Label;
            Label lbltoTime = e.Row.FindControl("lbltoTime") as Label;

            #region time conversion from 13, 14,... to 1,2,...
            string[] time;
            string[] timehourFrom;
            string[] timehourTo;
            int hourFrom;
            int hourTo;
            time = lblTime.Text.Split(' ');

            timehourFrom = time[0].Split(':');
            hourFrom = Convert.ToInt32(timehourFrom[0]);
            if (hourFrom > 12)
            {
                hourFrom = hourFrom - 12;
                lblfromTime.Text = hourFrom.ToString() + ":" + timehourFrom[1];
            }
            else
            {
                lblfromTime.Text = timehourFrom[0] + ":" + timehourFrom[1];
            }

            timehourTo = time[1].Split(':');
            hourTo = Convert.ToInt32(timehourTo[0]);
            if (hourTo > 12)
            {
                hourTo = hourTo - 12;
                lbltoTime.Text = hourTo.ToString() + ":" + timehourTo[1];
            }
            else
            {
                lbltoTime.Text = timehourTo[0] + ":" + timehourTo[1];
            }
            #endregion

            #region to load saturday routine if INCLUDE SATURDAY is checked
            if (chkIncSaturday.Checked == true)
            {
                gridRoutine1.Columns[7].Visible = true;
            }
            else
            {
                gridRoutine1.Columns[7].Visible = false;
            }
            #endregion


            #region to load Sunday routine 
            ACEnt = new ACADEMIC_CALENDAR();
            ACEnt.CAL_DATE = sunDate;
            ACEnt.PROGRAMID = ddlProgram.SelectedValue;
            ACEnt.SEMESTER = ddlSemester.SelectedValue;
            ACEnt = (ACADEMIC_CALENDAR)ACSer.GetSingle(ACEnt);
            if (ACEnt != null)
            {
                if (ACEnt.WORKING_DAY == "0")
                {
                    lblSundaySub.Text = "Holiday";
                }
                else
                {
                    RTEnt = new ROUTINE();
                    RTEnt.PERIOD_ID = lblPeriodId.Text;
                    RTEnt.PROGRAM_ID = ddlProgram.SelectedValue;
                    RTEnt.SEMESTER = ddlSemester.SelectedValue;
                    RTEnt.SECTION = ddlSection.SelectedValue;
                    RTEnt.ROUTINE_DATE = sunDate;
                    RTEnt.STATUS = "1";
                    RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                    if (RTEnt != null)
                    {
                        SUBEnt = new HSS_SUBJECT();
                        SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                        SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                        if (SUBEnt != null)
                        {
                            lblSundaySub.Text = "<b>" + SUBEnt.SUBJECT_CODE + "</b>";
                        }

                        if (RTEnt.LAB_THEORY == "Theory")
                        {
                            lblSundaySub.Text += "-" + "TH" + "<br>";
                        }
                        else if (RTEnt.LAB_THEORY == "Lab")
                            lblSundaySub.Text += "-" + "LAB" + "<br>";

                        EMPEnt = new Employees();
                        EMPEnt.EMPLOYEEID = RTEnt.TEACHER_ID;
                        EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
                        if (EMPEnt != null)
                        {
                            if (RTEnt.ASSIST_TEACH_ID != "")
                            {
                                lblSundaySub.Text += EMPEnt.Abbrevation;
                                EMPEnt = new Employees();
                                EMPEnt.EMPLOYEEID = RTEnt.ASSIST_TEACH_ID;
                                EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
                                if (EMPEnt != null)
                                    lblSundaySub.Text += "/" + EMPEnt.Abbrevation + "<br>";

                            }
                            else
                            {
                                lblSundaySub.Text += EMPEnt.Abbrevation + "<br>";
                            }
                        }

                        if (RTEnt.LAB_THEORY == "Lab")
                        {
                            lblSundaySub.Text += "<b>" + RTEnt.LAB_THEORY + "-" + RTEnt.LAB_NO + "</b>";
                        }
                        else if (RTEnt.LAB_THEORY == "Theory")
                        {
                            lblSundaySub.Text += "<b>" + "Class" + "-" + RTEnt.LAB_NO + "</b>";
                        }

                    }
                    else
                    {
                        PRDEnt = new PERIOD();
                        PRDEnt.PK_ID = lblPeriodId.Text;
                        PRDEnt = (PERIOD)PRDSer.GetSingle(PRDEnt);
                        if (PRDEnt != null && PRDEnt.PERIODS == "Break")
                        {
                            lblSundaySub.Text = "Break";
                        }
                        else
                            lblSundaySub.Text = "Off";
                    }
                }
            }



            #endregion
            #region to load Monday routine 
            ACEnt = new ACADEMIC_CALENDAR();
            ACEnt.CAL_DATE = monDate;
            ACEnt.PROGRAMID = ddlProgram.SelectedValue;
            ACEnt.SEMESTER = ddlSemester.SelectedValue;
            ACEnt = (ACADEMIC_CALENDAR)ACSer.GetSingle(ACEnt);
            if (ACEnt != null)
            {
                if (ACEnt.WORKING_DAY == "0")
                {
                    lblMondaySub.Text = "Holiday";
                }
                else
                {
                    RTEnt = new ROUTINE();
                    RTEnt.PERIOD_ID = lblPeriodId.Text;
                    RTEnt.PROGRAM_ID = ddlProgram.SelectedValue;
                    RTEnt.SEMESTER = ddlSemester.SelectedValue;
                    RTEnt.SECTION = ddlSection.SelectedValue;
                    RTEnt.ROUTINE_DATE = monDate;
                    RTEnt.STATUS = "1";
                    RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                    if (RTEnt != null)
                    {
                        SUBEnt = new HSS_SUBJECT();
                        SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                        SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                        if (SUBEnt != null)
                        {
                            lblMondaySub.Text = "<b>" + SUBEnt.SUBJECT_CODE + "</b>";
                        }

                        if (RTEnt.LAB_THEORY == "Theory")
                        {
                            lblMondaySub.Text += "-" + "TH" + "<br>";
                        }
                        else if (RTEnt.LAB_THEORY == "Lab")
                            lblMondaySub.Text += "-" + "LAB" + "<br>";

                        EMPEnt = new Employees();
                        EMPEnt.EMPLOYEEID = RTEnt.TEACHER_ID;
                        EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
                        if (EMPEnt != null)
                        {
                            if (RTEnt.ASSIST_TEACH_ID != "")
                            {
                                lblMondaySub.Text += EMPEnt.Abbrevation;
                                EMPEnt = new Employees();
                                EMPEnt.EMPLOYEEID = RTEnt.ASSIST_TEACH_ID;
                                EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
                                if (EMPEnt != null)
                                    lblMondaySub.Text += "/" + EMPEnt.Abbrevation + "<br>";

                            }
                            else
                            {
                                lblMondaySub.Text += EMPEnt.Abbrevation + "<br>";
                            }
                        }


                        if (RTEnt.LAB_THEORY == "Lab")
                        {
                            lblMondaySub.Text += "<b>" + RTEnt.LAB_THEORY + "-" + RTEnt.LAB_NO + "</b>";
                        }
                        else if (RTEnt.LAB_THEORY == "Theory")
                        {
                            lblMondaySub.Text += "<b>" + "Class" + "-" + RTEnt.LAB_NO + "</b>";
                        }

                    }
                    else
                    {
                        PRDEnt = new PERIOD();
                        PRDEnt.PK_ID = lblPeriodId.Text;
                        PRDEnt = (PERIOD)PRDSer.GetSingle(PRDEnt);
                        if (PRDEnt != null && PRDEnt.PERIODS == "Break")
                        {
                            lblMondaySub.Text = "Break";
                        }
                        else
                            lblMondaySub.Text = "Off";
                    }
                }
            }


            #endregion
            #region to load Tuesday routine 
            ACEnt = new ACADEMIC_CALENDAR();
            ACEnt.CAL_DATE = tuesDate;
            ACEnt.PROGRAMID = ddlProgram.SelectedValue;
            ACEnt.SEMESTER = ddlSemester.SelectedValue;
            ACEnt = (ACADEMIC_CALENDAR)ACSer.GetSingle(ACEnt);
            if (ACEnt != null)
            {
                if (ACEnt.WORKING_DAY == "0")
                {
                    lblTuesdaySub.Text = "Holiday";
                }
                else
                {
                    RTEnt = new ROUTINE();
                    RTEnt.PERIOD_ID = lblPeriodId.Text;
                    RTEnt.PROGRAM_ID = ddlProgram.SelectedValue;
                    RTEnt.SEMESTER = ddlSemester.SelectedValue;
                    RTEnt.SECTION = ddlSection.SelectedValue;
                    RTEnt.ROUTINE_DATE = tuesDate;
                    RTEnt.STATUS = "1";
                    RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                    if (RTEnt != null)
                    {
                        SUBEnt = new HSS_SUBJECT();
                        SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                        SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                        if (SUBEnt != null)
                        {
                            lblTuesdaySub.Text = "<b>" + SUBEnt.SUBJECT_CODE + "</b>";
                        }

                        if (RTEnt.LAB_THEORY == "Theory")
                        {
                            lblTuesdaySub.Text += "-" + "TH" + "<br>";
                        }
                        else if (RTEnt.LAB_THEORY == "Lab")
                            lblTuesdaySub.Text += "-" + "LAB" + "<br>";

                        EMPEnt = new Employees();
                        EMPEnt.EMPLOYEEID = RTEnt.TEACHER_ID;
                        EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
                        if (EMPEnt != null)
                        {
                            if (RTEnt.ASSIST_TEACH_ID != "")
                            {
                                lblTuesdaySub.Text += EMPEnt.Abbrevation;
                                EMPEnt = new Employees();
                                EMPEnt.EMPLOYEEID = RTEnt.ASSIST_TEACH_ID;
                                EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
                                if (EMPEnt != null)
                                    lblTuesdaySub.Text += "/" + EMPEnt.Abbrevation + "<br>";

                            }
                            else
                            {
                                lblTuesdaySub.Text += EMPEnt.Abbrevation + "<br>";
                            }
                        }


                        if (RTEnt.LAB_THEORY == "Lab")
                        {
                            lblTuesdaySub.Text += "<b>" + RTEnt.LAB_THEORY + "-" + RTEnt.LAB_NO + "</b>";
                        }
                        else if (RTEnt.LAB_THEORY == "Theory")
                        {
                            lblTuesdaySub.Text += "<b>" + "Class" + "-" + RTEnt.LAB_NO + "<b>";
                        }

                    }
                    else
                    {
                        PRDEnt = new PERIOD();
                        PRDEnt.PK_ID = lblPeriodId.Text;
                        PRDEnt = (PERIOD)PRDSer.GetSingle(PRDEnt);
                        if (PRDEnt != null && PRDEnt.PERIODS == "Break")
                        {
                            lblTuesdaySub.Text = "Break";
                        }
                        else
                            lblTuesdaySub.Text = "Off";
                    }
                }
            }

            #endregion
            #region to load Wednesday routine 
            ACEnt = new ACADEMIC_CALENDAR();
            ACEnt.CAL_DATE = wedDate;
            ACEnt.PROGRAMID = ddlProgram.SelectedValue;
            ACEnt.SEMESTER = ddlSemester.SelectedValue;
            ACEnt = (ACADEMIC_CALENDAR)ACSer.GetSingle(ACEnt);
            if (ACEnt != null)
            {
                if (ACEnt.WORKING_DAY == "0")
                {
                    lblWednesdaySub.Text = "Holiday";
                }
                else
                {
                    RTEnt = new ROUTINE();
                    RTEnt.PERIOD_ID = lblPeriodId.Text;
                    RTEnt.PROGRAM_ID = ddlProgram.SelectedValue;
                    RTEnt.SEMESTER = ddlSemester.SelectedValue;
                    RTEnt.SECTION = ddlSection.SelectedValue;
                    RTEnt.ROUTINE_DATE = wedDate;
                    RTEnt.STATUS = "1";
                    RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                    if (RTEnt != null)
                    {
                        SUBEnt = new HSS_SUBJECT();
                        SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                        SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                        if (SUBEnt != null)
                        {
                            lblWednesdaySub.Text = "<b>" + SUBEnt.SUBJECT_CODE + "</b>";
                        }

                        if (RTEnt.LAB_THEORY == "Theory")
                        {
                            lblWednesdaySub.Text += "-" + "TH" + "<br>";
                        }
                        else if (RTEnt.LAB_THEORY == "Lab")
                            lblWednesdaySub.Text += "-" + "LAB" + "<br>";

                        EMPEnt = new Employees();
                        EMPEnt.EMPLOYEEID = RTEnt.TEACHER_ID;
                        EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
                        if (EMPEnt != null)
                        {
                            if (RTEnt.ASSIST_TEACH_ID != "")
                            {
                                lblWednesdaySub.Text += EMPEnt.Abbrevation;
                                EMPEnt = new Employees();
                                EMPEnt.EMPLOYEEID = RTEnt.ASSIST_TEACH_ID;
                                EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
                                if (EMPEnt != null)
                                    lblWednesdaySub.Text += "/" + EMPEnt.Abbrevation + "<br>";

                            }
                            else
                            {
                                lblWednesdaySub.Text += EMPEnt.Abbrevation + "<br>";
                            }
                        }

                        if (RTEnt.LAB_THEORY == "Lab")
                        {
                            lblWednesdaySub.Text += "<b>" + RTEnt.LAB_THEORY + "-" + RTEnt.LAB_NO + "<b>";
                        }
                        else if (RTEnt.LAB_THEORY == "Theory")
                        {
                            lblWednesdaySub.Text += "<b>" + "Class" + "-" + RTEnt.LAB_NO + "<b>";
                        }

                    }
                    else
                    {
                        PRDEnt = new PERIOD();
                        PRDEnt.PK_ID = lblPeriodId.Text;
                        PRDEnt = (PERIOD)PRDSer.GetSingle(PRDEnt);
                        if (PRDEnt != null && PRDEnt.PERIODS == "Break")
                        {
                            lblWednesdaySub.Text = "Break";
                        }
                        else
                            lblWednesdaySub.Text = "Off";

                    }
                }
            }
            #endregion
            #region to load Thursday routine 
            ACEnt = new ACADEMIC_CALENDAR();
            ACEnt.CAL_DATE = thursDate;
            ACEnt.PROGRAMID = ddlProgram.SelectedValue;
            ACEnt.SEMESTER = ddlSemester.SelectedValue;
            ACEnt = (ACADEMIC_CALENDAR)ACSer.GetSingle(ACEnt);
            if (ACEnt != null)
            {
                if (ACEnt.WORKING_DAY == "0")
                {
                    lblThursdaySub.Text = "Holiday";
                }
                else
                {
                    RTEnt = new ROUTINE();
                    RTEnt.PERIOD_ID = lblPeriodId.Text;
                    RTEnt.PROGRAM_ID = ddlProgram.SelectedValue;
                    RTEnt.SEMESTER = ddlSemester.SelectedValue;
                    RTEnt.SECTION = ddlSection.SelectedValue;
                    RTEnt.ROUTINE_DATE = thursDate;
                    RTEnt.STATUS = "1";
                    RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                    if (RTEnt != null)
                    {
                        SUBEnt = new HSS_SUBJECT();
                        SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                        SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                        if (SUBEnt != null)
                        {
                            lblThursdaySub.Text = "<b>" + SUBEnt.SUBJECT_CODE + "</b>";
                        }

                        if (RTEnt.LAB_THEORY == "Theory")
                        {
                            lblThursdaySub.Text += "-" + "TH" + "<br>";
                        }
                        else if (RTEnt.LAB_THEORY == "Lab")
                            lblThursdaySub.Text += "-" + "LAB" + "<br>";

                        EMPEnt = new Employees();
                        EMPEnt.EMPLOYEEID = RTEnt.TEACHER_ID;
                        EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
                        if (EMPEnt != null)
                        {
                            if (RTEnt.ASSIST_TEACH_ID != "")
                            {
                                lblThursdaySub.Text += EMPEnt.Abbrevation;
                                EMPEnt = new Employees();
                                EMPEnt.EMPLOYEEID = RTEnt.ASSIST_TEACH_ID;
                                EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
                                if (EMPEnt != null)
                                    lblThursdaySub.Text += "/" + EMPEnt.Abbrevation + "<br>";

                            }
                            else
                            {
                                lblThursdaySub.Text += EMPEnt.Abbrevation + "<br>";
                            }
                        }


                        if (RTEnt.LAB_THEORY == "Lab")
                        {
                            lblThursdaySub.Text += "<b>" + RTEnt.LAB_THEORY + "-" + RTEnt.LAB_NO + "</b>";
                        }
                        else if (RTEnt.LAB_THEORY == "Theory")
                        {
                            lblThursdaySub.Text += "<b>" + "Class" + "-" + RTEnt.LAB_NO + "<b>";
                        }

                    }
                    else
                    {
                        PRDEnt = new PERIOD();
                        PRDEnt.PK_ID = lblPeriodId.Text;
                        PRDEnt = (PERIOD)PRDSer.GetSingle(PRDEnt);
                        if (PRDEnt != null && PRDEnt.PERIODS == "Break")
                        {
                            lblThursdaySub.Text = "Break";
                        }
                        else
                            lblThursdaySub.Text = "Off";
                    }
                }
            }

            #endregion
            #region to load Friday routine 
            ACEnt = new ACADEMIC_CALENDAR();
            ACEnt.CAL_DATE = friDate;
            ACEnt.PROGRAMID = ddlProgram.SelectedValue;
            ACEnt.SEMESTER = ddlSemester.SelectedValue;
            ACEnt = (ACADEMIC_CALENDAR)ACSer.GetSingle(ACEnt);
            if (ACEnt != null)
            {
                if (ACEnt.WORKING_DAY == "0")
                {
                    lblFridaySub.Text = "Holiday";
                }
                else
                {
                    RTEnt = new ROUTINE();
                    RTEnt.PERIOD_ID = lblPeriodId.Text;
                    RTEnt.PROGRAM_ID = ddlProgram.SelectedValue;
                    RTEnt.SEMESTER = ddlSemester.SelectedValue;
                    RTEnt.SECTION = ddlSection.SelectedValue;
                    RTEnt.ROUTINE_DATE = friDate;
                    RTEnt.STATUS = "1";
                    RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                    if (RTEnt != null)
                    {
                        SUBEnt = new HSS_SUBJECT();
                        SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                        SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                        if (SUBEnt != null)
                        {
                            lblFridaySub.Text = "<b>" + SUBEnt.SUBJECT_CODE + "</b>";
                        }

                        if (RTEnt.LAB_THEORY == "Theory")
                        {
                            lblFridaySub.Text += "-" + "TH" + "<br>";
                        }
                        else if (RTEnt.LAB_THEORY == "Lab")
                            lblFridaySub.Text += "-" + "LAB" + "<br>";

                        EMPEnt = new Employees();
                        EMPEnt.EMPLOYEEID = RTEnt.TEACHER_ID;
                        EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
                        if (EMPEnt != null)
                        {
                            if (RTEnt.ASSIST_TEACH_ID != "")
                            {
                                lblFridaySub.Text += EMPEnt.Abbrevation;
                                EMPEnt = new Employees();
                                EMPEnt.EMPLOYEEID = RTEnt.ASSIST_TEACH_ID;
                                EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
                                if (EMPEnt != null)
                                    lblFridaySub.Text += "/" + EMPEnt.Abbrevation + "<br>";

                            }
                            else
                            {
                                lblFridaySub.Text += EMPEnt.Abbrevation + "<br>";
                            }
                        }


                        if (RTEnt.LAB_THEORY == "Lab")
                        {
                            lblFridaySub.Text += "<b>" + RTEnt.LAB_THEORY + "-" + RTEnt.LAB_NO + "</b>";
                        }
                        else if (RTEnt.LAB_THEORY == "Theory")
                        {
                            lblFridaySub.Text += "<b>" + "Class" + "-" + RTEnt.LAB_NO + "<b>";
                        }

                    }
                    else
                    {
                        PRDEnt = new PERIOD();
                        PRDEnt.PK_ID = lblPeriodId.Text;
                        PRDEnt = (PERIOD)PRDSer.GetSingle(PRDEnt);
                        if (PRDEnt != null && PRDEnt.PERIODS == "Break")
                        {
                            lblFridaySub.Text = "Break";
                        }
                        else
                            lblFridaySub.Text = "Off";
                    }
                }
            }

            #endregion
            #region to load Saturday routine 
            ACEnt = new ACADEMIC_CALENDAR();
            ACEnt.CAL_DATE = satDate;
            ACEnt.PROGRAMID = ddlProgram.SelectedValue;
            ACEnt.SEMESTER = ddlSemester.SelectedValue;
            ACEnt = (ACADEMIC_CALENDAR)ACSer.GetSingle(ACEnt);
            if (ACEnt != null)
            {
                if (ACEnt.WORKING_DAY == "0")
                {
                    lblSaturdaySub.Text = "Holiday";
                }
                else
                {
                    RTEnt = new ROUTINE();
                    RTEnt.PERIOD_ID = lblPeriodId.Text;
                    RTEnt.PROGRAM_ID = ddlProgram.SelectedValue;
                    RTEnt.SEMESTER = ddlSemester.SelectedValue;
                    RTEnt.SECTION = ddlSection.SelectedValue;
                    RTEnt.ROUTINE_DATE = satDate;
                    RTEnt.STATUS = "1";
                    RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                    if (RTEnt != null)
                    {
                        SUBEnt = new HSS_SUBJECT();
                        SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                        SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                        if (SUBEnt != null)
                        {
                            lblSaturdaySub.Text = "<b>" + SUBEnt.SUBJECT_CODE + "</b>";
                        }

                        if (RTEnt.LAB_THEORY == "Theory")
                        {
                            lblSaturdaySub.Text += "-" + "TH" + "<br>";
                        }
                        else if (RTEnt.LAB_THEORY == "Lab")
                            lblSaturdaySub.Text += "-" + "LAB" + "<br>";

                        EMPEnt = new Employees();
                        EMPEnt.EMPLOYEEID = RTEnt.TEACHER_ID;
                        EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
                        if (EMPEnt != null)
                        {
                            if (RTEnt.ASSIST_TEACH_ID != "")
                            {
                                lblSaturdaySub.Text += EMPEnt.Abbrevation;
                                EMPEnt = new Employees();
                                EMPEnt.EMPLOYEEID = RTEnt.ASSIST_TEACH_ID;
                                EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
                                if (EMPEnt != null)
                                    lblSaturdaySub.Text += "/" + EMPEnt.Abbrevation + "<br>";

                            }
                            else
                            {
                                lblSaturdaySub.Text += EMPEnt.Abbrevation + "<br>";
                            }
                        }


                        if (RTEnt.LAB_THEORY == "Lab")
                        {
                            lblSaturdaySub.Text += "<b>" + RTEnt.LAB_THEORY + "-" + RTEnt.LAB_NO + "</b>";
                        }
                        else if (RTEnt.LAB_THEORY == "Theory")
                        {
                            lblSaturdaySub.Text += "<b>" + "Class" + "-" + RTEnt.LAB_NO + "<b>";
                        }

                    }
                    else
                    {
                        PRDEnt = new PERIOD();
                        PRDEnt.PK_ID = lblPeriodId.Text;
                        PRDEnt = (PERIOD)PRDSer.GetSingle(PRDEnt);
                        if (PRDEnt != null && PRDEnt.PERIODS == "Break")
                        {
                            lblSaturdaySub.Text = "Break";
                        }
                        else
                            lblSaturdaySub.Text = "Off";
                    }
                }
            }

            #endregion

            #region to hide 'Break' from Time column of grid
            if (lblPeriod.Text == "Break")
            {
                lblPeriod.Visible = false;
            }
            else
                lblPeriod.Visible = true;
            #endregion


        }
    }


    protected void loadRoutineHeadings()
    {
        OFCEnt = new OFFICE();
        OFCEnt = (OFFICE)OFCSer.GetSingle(OFCEnt);
        if (OFCEnt != null)
        {
            lblCollegeName.Text = OFCEnt.OFFICENAME;
            lblAddress.Text = OFCEnt.ADDRESS;

        }

        PEnt = new program();
        PEnt.PK_ID = ddlProgram.SelectedValue;
        PEnt = (program)PSer.GetSingle(PEnt);
        if (PEnt != null)
            lblProgramP.Text = PEnt.PROGRAM_CODE;

        SMSEnt = new semester();
        SMSEnt.PK_ID = ddlSemester.SelectedValue;
        SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
        if (SMSEnt != null)
            lblSemesterP.Text = SMSEnt.SEMESTER_CODE;

        lblSectionP.Text = ddlSection.SelectedValue;


        if (txtComment.InnerText != "")
        {
            lblComment.Visible = true;
            lblComment.Text = "<b> Note: </b>" + "<br/>" + txtComment.InnerText.Replace("\n", "<br/>");
        }
        else
        {
            lblComment.Visible = false;
        }

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadGrid();
        loadRoutineHeadings();
        loadTeacherFullNameGrid();
        loadSubFullName();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "printPartOfPage();", true);
    }

    protected void loadTeacherFullNameGrid()
    {
        TSUBEnt = new TEACHER_SUBJECT_MAPPING();
        TSUBEnt.SEMESTER = ddlSemester.SelectedValue;
        gridTeacherFullName.DataSource = TSUBSer.GetAll(TSUBEnt);
        gridTeacherFullName.DataBind();

        #region to display this grid horizontally as the routine exceeds to another page
        lblTeacherNameAbbr.Text = "";
        foreach (GridViewRow gr in gridTeacherFullName.Rows)
        {
            Label lblAbbr = gr.FindControl("lblAbbr") as Label;
            Label lblFullName = gr.FindControl("lblFullName") as Label;
            lblTeacherNameAbbr.Text += "►";
            lblTeacherNameAbbr.Text += lblFullName.Text + "(" + lblAbbr.Text + ")" + " ";
        }
        #endregion
    }


    protected void gridTeacherFullName_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTeacherID = e.Row.FindControl("lblTeacherID") as Label;
            Label lblAbbr = e.Row.FindControl("lblAbbr") as Label;
            Label lblFullName = e.Row.FindControl("lblFullName") as Label;

            EMPEnt = new Employees();
            EMPEnt.EMPLOYEEID = lblTeacherID.Text;
            EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
            if (EMPEnt != null)
            {
                lblAbbr.Text = EMPEnt.Abbrevation;
                lblFullName.Text = EMPEnt.FULLNAME;
            }
        }
    }

    protected void loadSubFullName()
    {
        SUBEnt = new HSS_SUBJECT();
        SUBEnt.SEMESTER = ddlSemester.SelectedValue;
        SUBEnt.YEAR = hf.getSyllabusYear(ddlProgram.SelectedValue, hf.NepaliYear());
        gridSubjectFullName.DataSource = SUBSer.GetAll(SUBEnt);
        gridSubjectFullName.DataBind();

        #region to display this grid horizontally as the routine exceeds to another page
        lblSubNameAbbr.Text = "";
        foreach (GridViewRow gr in gridSubjectFullName.Rows)
        {
            Label lblSubCode = gr.FindControl("lblSubCode") as Label;
            Label lblSubName = gr.FindControl("lblSubName") as Label;
            lblSubNameAbbr.Text += "►";
            lblSubNameAbbr.Text += lblSubName.Text + "(" + lblSubCode.Text + ")" + " ";
        }
        #endregion

    }

}