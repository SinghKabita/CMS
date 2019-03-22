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

public partial class class_routine_reports_TeacherRoutinePrint : System.Web.UI.Page
{
    daysofweek DOWEnt = new daysofweek();
    daysofweekService DOWSer = new daysofweekService();

    ACADEMIC_CALENDAR ACEnt = new ACADEMIC_CALENDAR();
    ACADEMIC_CALENDARService ACSer = new ACADEMIC_CALENDARService();

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

    semester SMSEnt = new semester();
    semesterService SMSSer = new semesterService();

    program PEnt = new program();
    programService PSer = new programService();

    HelperFunction hf = new HelperFunction();



    Months MEnt = new Months();
    MonthsService MSer = new MonthsService();

    static string date1 = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadTeacher();
        }
    }

    protected void loadTeacher()
    {
        EntityList theList = new EntityList();
        EMPEnt = new Employees();
        EMPEnt.DIVISION = "2";
        ddlTeacher.DataSource = EMPSer.GetAll(EMPEnt);
        ddlTeacher.DataValueField = "EMPLOYEEID";
        ddlTeacher.DataTextField = "FULLNAME";
        ddlTeacher.DataBind();
        ddlTeacher.Items.Insert(0, "Select");
    }

    protected void loadTeacherName()
    {
        EMPEnt = new Employees();
        EMPEnt.DIVISION = "2";
        EMPEnt.EMPLOYEEID = ddlTeacher.SelectedValue;
        EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
        if (EMPEnt != null)
        {
            lblTeacherName.Text = EMPEnt.FULLNAME;
        }
    }

    protected void LoadGrid()
    {
        hide.Visible = true;
        DOWEnt = new daysofweek();
        gridRoutine1.DataSource = DOWSer.GetAll(DOWEnt);
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
    string periodTime1 = "";
    string periodTime2 = "";
    string periodTime3 = "";
    string periodTime4 = "";
    string periodTime5 = "";
    string periodBrk = "";
    protected void gridRoutine1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        #region header
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Label lblPeriod1 = e.Row.FindControl("lblPeriod1") as Label;
            Label lblPeriod2 = e.Row.FindControl("lblPeriod2") as Label;
            Label lblPeriod3 = e.Row.FindControl("lblPeriod3") as Label;
            Label lblPeriod4 = e.Row.FindControl("lblPeriod4") as Label;
            Label lblPeriod5 = e.Row.FindControl("lblPeriod5") as Label;
            Label lblPeriodBrk = e.Row.FindControl("lblPeriodBrk") as Label;
            periodTime1 = lblPeriod1.Text;
            periodTime2 = lblPeriod2.Text;
            periodTime3 = lblPeriod3.Text;
            periodTime4 = lblPeriod4.Text;
            periodTime5 = lblPeriod5.Text;
            periodBrk = lblPeriodBrk.Text;
        }
        #endregion

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string[] date;
            string String_Date = "";
            Label lblWeekDays = e.Row.FindControl("lblWeekDays") as Label;
            Label lblDate = e.Row.FindControl("lblDate") as Label;
            
            #region to make date visible or not from Checkbox
            if (chkShowDates.Checked == true)
            {
                lblDate.Visible = true;
            }
            else
            {
                lblDate.Visible = false;
            }
            #endregion

            #region to assign date
            ACEnt = new ACADEMIC_CALENDAR();
            ACEnt.CAL_DATE = txtDate.Text;
            ACEnt = (ACADEMIC_CALENDAR)ACSer.GetSingle(ACEnt);
            if (ACEnt != null)
            {
                DateTime Routine_Date = Convert.ToDateTime(txtDate.Text).AddDays(-Convert.ToInt32(day_number(ACEnt.CAL_DAY_OF_WEEK)) + 1); // convert to sunday's date;
                #region to load Date in week Days

                #region Sunday
                String_Date = hf.CheckDate(Routine_Date.ToString());  // mm/dd/yyyy format
                date = String_Date.Split('/');
                lblDate.Text = date[1] + "/" + date[0] + "/" + date[2]; // dd/mm/yyyy format
                sunDate = lblDate.Text;

                #endregion

                #region Monday
                if (lblWeekDays.Text == "Monday")
                {
                    Routine_Date = Convert.ToDateTime(String_Date).AddDays(1);
                    String_Date = hf.CheckDate(Routine_Date.ToString());
                    date = hf.CheckDate(Routine_Date.ToString()).Split('/');
                    lblDate.Text = date[1] + "/" + date[0] + "/" + date[2];
                    monDate = lblDate.Text;
                }

                #endregion

                #region Tuesday
                if (lblWeekDays.Text == "Tuesday")
                {
                    Routine_Date = Convert.ToDateTime(String_Date).AddDays(2);
                    String_Date = hf.CheckDate(Routine_Date.ToString());
                    date = hf.CheckDate(Routine_Date.ToString()).Split('/');
                    lblDate.Text = date[1] + "/" + date[0] + "/" + date[2];
                    tuesDate = lblDate.Text;
                }
                //Routine_Date = Convert.ToDateTime(String_Date).AddDays(1);
                //String_Date = hf.CheckDate(Routine_Date.ToString());
                //date = hf.CheckDate(Routine_Date.ToString()).Split('/');
                //lblTuesDateH.Text = date[1] + "/" + date[0] + "/" + date[2];
                //tuesDate = lblTuesDateH.Text;
                #endregion

                #region Wenesday
                if (lblWeekDays.Text == "Wednesday")
                {
                    Routine_Date = Convert.ToDateTime(String_Date).AddDays(3);
                    String_Date = hf.CheckDate(Routine_Date.ToString());
                    date = hf.CheckDate(Routine_Date.ToString()).Split('/');
                    lblDate.Text = date[1] + "/" + date[0] + "/" + date[2];
                    wedDate = lblDate.Text;
                }
                #endregion

                #region Thursday
                if (lblWeekDays.Text == "Thursday")
                {
                    Routine_Date = Convert.ToDateTime(String_Date).AddDays(4);
                    String_Date = hf.CheckDate(Routine_Date.ToString());
                    date = hf.CheckDate(Routine_Date.ToString()).Split('/');
                    lblDate.Text = date[1] + "/" + date[0] + "/" + date[2];
                    thursDate = lblDate.Text;
                }
                #endregion

                #region Friday
                if (lblWeekDays.Text == "Friday")
                {
                    Routine_Date = Convert.ToDateTime(String_Date).AddDays(5);
                    String_Date = hf.CheckDate(Routine_Date.ToString());
                    date = hf.CheckDate(Routine_Date.ToString()).Split('/');
                    lblDate.Text = date[1] + "/" + date[0] + "/" + date[2];
                    friDate = lblDate.Text;
                }
                #endregion

                #endregion
            }

            #endregion

            Label lblFirstPeriod = e.Row.FindControl("lblFirstPeriod") as Label;
            Label lblSecondPeriod = e.Row.FindControl("lblSecondPeriod") as Label;
            Label lblBreakPeriod = e.Row.FindControl("lblBreakPeriod") as Label;
            Label lblThirdPeriod = e.Row.FindControl("lblThirdPeriod") as Label;
            Label lblFourthPeriod = e.Row.FindControl("lblFourthPeriod") as Label;
            Label lblFifthPeriod = e.Row.FindControl("lblFifthPeriod") as Label;


            if (lblWeekDays.Text == "Sunday")
            {
                #region to load Sunday routine 
                ACEnt = new ACADEMIC_CALENDAR();
                ACEnt.CAL_DATE = sunDate;
                ACEnt = (ACADEMIC_CALENDAR)ACSer.GetSingle(ACEnt);
                if (ACEnt != null)
                {
                    #region for 1st Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblFirstPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime1;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = sunDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblFirstPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblFirstPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblFirstPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }
                                    

                                    lblFirstPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                    //lblFirstPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for 2st Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblSecondPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime2;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = sunDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblSecondPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblSecondPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblSecondPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblSecondPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                    //lblSecondPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for Break
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblBreakPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodBrk;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = sunDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    lblBreakPeriod.Text = "Break";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for Third Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblThirdPeriod.Text = "Off";
                    }
                    else
                    {

                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime3;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = sunDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblThirdPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblThirdPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblThirdPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblThirdPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                    //lblThirdPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for Fourth Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblFourthPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime4;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = sunDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblFourthPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblFourthPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblFourthPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblFourthPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                   // lblFourthPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for Fifth Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblFifthPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime5;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = sunDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblFifthPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblFifthPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblFifthPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblFifthPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                    //lblFifthPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                }
                #endregion
            }

            else if (lblWeekDays.Text == "Monday")
            {
                #region to load Monday routine 
                ACEnt = new ACADEMIC_CALENDAR();
                ACEnt.CAL_DATE = monDate;
                ACEnt = (ACADEMIC_CALENDAR)ACSer.GetSingle(ACEnt);
                if (ACEnt != null)
                {
                    #region for 1st Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblFirstPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime1;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = monDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt.STATUS = "1";
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblFirstPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblFirstPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblFirstPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblFirstPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                   // lblFirstPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for 2st Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblSecondPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime2;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = monDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt.STATUS = "1";
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblSecondPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblSecondPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblSecondPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblSecondPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                   // lblSecondPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for Break
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblBreakPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodBrk;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = monDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    lblBreakPeriod.Text = "Break";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for Third Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblThirdPeriod.Text = "Off";
                    }
                    else
                    {

                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime3;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = monDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt.STATUS = "1";
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblThirdPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblThirdPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblThirdPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblThirdPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                   // lblThirdPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for Fourth Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblFourthPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime4;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = monDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblFourthPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblFourthPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblFourthPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblFourthPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                   // lblFourthPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for Fifth Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblFifthPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime5;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = monDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblFifthPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblFifthPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblFifthPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblFifthPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                    //lblFifthPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                }

                #endregion
            }
            else if (lblWeekDays.Text == "Tuesday")
            {

                #region to load Tuesday routine 
                ACEnt = new ACADEMIC_CALENDAR();
                ACEnt.CAL_DATE = tuesDate;
                ACEnt = (ACADEMIC_CALENDAR)ACSer.GetSingle(ACEnt);
                if (ACEnt != null)
                {
                    #region for 1st Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblFirstPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime1;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = tuesDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblFirstPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblFirstPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblFirstPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblFirstPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                    //lblFirstPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for 2st Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblSecondPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime2;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = tuesDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblSecondPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblSecondPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblSecondPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblSecondPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                    //lblSecondPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for Break
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblBreakPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodBrk;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = tuesDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    lblBreakPeriod.Text = "Break";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for Third Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblThirdPeriod.Text = "Off";
                    }
                    else
                    {

                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime3;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = tuesDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblThirdPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblThirdPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblThirdPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblThirdPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                    //lblThirdPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for Fourth Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblFourthPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime4;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = tuesDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblFourthPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblFourthPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblFourthPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblFourthPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                    //lblFourthPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for Fifth Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblFifthPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime5;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = tuesDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblFifthPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblFifthPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblFifthPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblFifthPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                    //lblFifthPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                }

                #endregion
            }
            else if (lblWeekDays.Text == "Wednesday")
            {
                #region to load Wednesday routine 
                ACEnt = new ACADEMIC_CALENDAR();
                ACEnt.CAL_DATE = wedDate;
                ACEnt = (ACADEMIC_CALENDAR)ACSer.GetSingle(ACEnt);
                if (ACEnt != null)
                {
                    #region for First Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblFirstPeriod.Text = "Off";
                    }
                    else
                    {

                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime1;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = wedDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblFirstPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblFirstPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblFirstPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblFirstPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                    //lblFirstPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for Second Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblSecondPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime2;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = wedDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblSecondPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblSecondPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblSecondPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblSecondPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                    //lblSecondPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for Break 
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblBreakPeriod.Text = "Off";
                    }
                    else
                    {
                       
                                    lblBreakPeriod.Text =  "Break";
                               
                        
                    }
                    #endregion
                    #region for Third Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblThirdPeriod.Text = "Off";
                    }
                    else
                    {

                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime3;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = wedDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblThirdPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblThirdPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblThirdPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblThirdPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                    //lblThirdPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for Fourth Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblFourthPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime4;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = wedDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblFourthPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblFourthPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblFourthPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblFourthPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                    //lblFourthPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for Fifth Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblFifthPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime5;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = wedDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblFifthPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblFifthPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblFifthPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblFifthPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                    //lblFifthPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                }

                #endregion
            }
            else if (lblWeekDays.Text == "Thursday")
            {
                #region to load Thursday routine 
                ACEnt = new ACADEMIC_CALENDAR();
                ACEnt.CAL_DATE = thursDate;
                ACEnt = (ACADEMIC_CALENDAR)ACSer.GetSingle(ACEnt);
                if (ACEnt != null)
                {
                    #region for 1st Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblFirstPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime1;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = thursDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblFirstPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblFirstPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblFirstPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblFirstPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                    //lblFirstPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for 2st Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblSecondPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime2;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = thursDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblSecondPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblSecondPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblSecondPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblSecondPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                    //lblSecondPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for Break
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblBreakPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodBrk;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = thursDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    lblBreakPeriod.Text = "Break";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for Third Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblThirdPeriod.Text = "Off";
                    }
                    else
                    {

                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime3;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = thursDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblThirdPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblThirdPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblThirdPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblThirdPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                    //lblThirdPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for Fourth Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblFourthPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime4;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = thursDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblFourthPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblFourthPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblFourthPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblFourthPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                    //lblFourthPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for Fifth Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblFifthPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime5;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = thursDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblFifthPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblFifthPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblFifthPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblFifthPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                    //lblFifthPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                    
                }

                #endregion
            }
            else if (lblWeekDays.Text == "Friday")
            {

                #region to load Friday routine 
                ACEnt = new ACADEMIC_CALENDAR();
                ACEnt.CAL_DATE = friDate;
                ACEnt = (ACADEMIC_CALENDAR)ACSer.GetSingle(ACEnt);
                if (ACEnt != null)
                {
                    #region for 1st Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblFirstPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime1;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = friDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblFirstPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblFirstPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblFirstPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblFirstPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                    //lblFirstPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for 2st Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblSecondPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime2;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = friDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblSecondPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblSecondPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblSecondPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblSecondPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                    //lblSecondPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for Break
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblBreakPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodBrk;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = friDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    lblBreakPeriod.Text = "Break";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for Third Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblThirdPeriod.Text = "Off";
                    }
                    else
                    {

                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime3;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = friDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblThirdPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblThirdPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblThirdPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblThirdPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                    //lblThirdPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for Fourth Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblFourthPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime4;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = friDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblFourthPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblFourthPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblFourthPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblFourthPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                    //lblFourthPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                    #region for Fifth Period
                    if (ACEnt.WORKING_DAY == "0")
                    {
                        lblFifthPeriod.Text = "Off";
                    }
                    else
                    {
                        EntityList prdList = new EntityList();
                        PRDEnt = new PERIOD();
                        PRDEnt.TIME = periodTime5;
                        prdList = PRDSer.GetAll(PRDEnt);
                        if (prdList.Count > 0)
                        {
                            foreach (PERIOD prd in prdList)
                            {
                                RTEnt = new ROUTINE();
                                RTEnt.ROUTINE_DATE = friDate;
                                RTEnt.TEACHER_ID = ddlTeacher.SelectedValue;
                                RTEnt.PERIOD_ID = prd.PK_ID;
                                RTEnt = (ROUTINE)RTSer.GetSingle(RTEnt);
                                if (RTEnt != null)
                                {
                                    SUBEnt = new HSS_SUBJECT();
                                    SUBEnt.PK_ID = RTEnt.SUBJECT_ID;
                                    SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
                                    if (SUBEnt != null)
                                    {
                                        lblFifthPeriod.Text = SUBEnt.SUBJECT_CODE;
                                    }
                                    PEnt = new program();
                                    PEnt.PK_ID = RTEnt.PROGRAM_ID;
                                    PEnt = (program)PSer.GetSingle(PEnt);
                                    if (PEnt != null)
                                    {
                                        lblFifthPeriod.Text += "<br>" + PEnt.PROGRAM_CODE;
                                    }
                                    SMSEnt = new semester();
                                    SMSEnt.PK_ID = RTEnt.SEMESTER;
                                    SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
                                    if (SMSEnt != null)
                                    {
                                        lblFifthPeriod.Text += " " + SMSEnt.SEMESTER_CODE;
                                    }


                                    lblFifthPeriod.Text += " " + "'" + RTEnt.SECTION + "'";
                                }
                                else
                                {
                                    //lblFifthPeriod.Text = "Off";
                                }
                            }
                        }
                    }
                    #endregion
                }

                #endregion
            }
        }
}

protected void btnView_Click(object sender, EventArgs e)
{
    loadTeacherName();
    LoadGrid();
}
protected void btnPrint_Click(object sender, EventArgs e)
{
    ScriptManager.RegisterStartupScript(this, this.GetType(), "", "printPartOfPage();", true);
}


    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        hide.Visible = false;
    }
}
