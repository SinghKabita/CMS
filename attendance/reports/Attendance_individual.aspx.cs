using Entity.Components;
using Entity.Framework;
using Oracle.DataAccess.Client;
using Service.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class attendance_reports_Attendance_individual : System.Web.UI.Page
{
    HSS_CURRENT_STUDENT CSEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSer = new HSS_CURRENT_STUDENTService();

    HSS_ATTENDANCE AEnt = new HSS_ATTENDANCE();
    HSS_ATTENDANCEService ASer = new HSS_ATTENDANCEService();

    HSS_STUDENT STDEnt = new HSS_STUDENT();
    HSS_STUDENTService STDSer = new HSS_STUDENTService();

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    HSS_SUBJECT SUBEnt = new HSS_SUBJECT();
    HSS_SUBJECTService SUBSer = new HSS_SUBJECTService();

    BatchYear BTEnt = new BatchYear();
    BatchYearService BTSer = new BatchYearService();

    HSS_NAME NAMEEnt = new HSS_NAME();
    HSS_NAMEService NAMESer = new HSS_NAMEService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    Months MEnt = new Months();
    MonthsService MSer = new MonthsService();

    EntityList theList = new EntityList();
    EntityList newList = new EntityList();

    HelperFunction hf = new HelperFunction();

    string stdid = "";

    string sub1 = "";
    string sub2 = "";
    string sub3 = "";
    string sub4 = "";
    string sub5 = "";
    string sub6 = "";
    string sub7 = "";
    string subjectname = "";

    double totalpresent1, totalpresent2, totalpresent3, totalpresent4, totalpresent5, totalpresent6, totalpresent7;
    double totalabsent1, totalabsent2, totalabsent3, totalabsent4, totalabsent5, totalabsent6, totalabsent7;
    double totalleave1, totalleave2, totalleave3, totalleave4, totalleave5, totalleave6, totalleave7;
    double totaldays1, totaldays2, totaldays3, totaldays4, totaldays5, totaldays6, totaldays7;
    double totalpresentpercent1, totalpresentpercent2, totalpresentpercent3, totalpresentpercent4, totalpresentpercent5, totalpresentpercent6, totalpresentpercent7;
    double totalmarks1, totalmarks2, totalmarks3, totalmarks4, totalmarks5, totalmarks6, totalmarks7;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFaculty();
            LoadLevel();
            LoadProgram();
            LoadCollegeCode();

        }
    }
    protected void rbtnChooseDate_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlViewBy.SelectedValue == "1")
        {
            string xyz;
            try
            {
                xyz = ddlStudentName.SelectedValue;
                getSelectedSubject(xyz);
                LoadDetail(xyz);
            }
            catch
            {
                HelperFunction.MsgBox(this, this.GetType(), "Student not found");
                ddlStudentName.SelectedValue = "Select";
            }
        }


        else if (ddlViewBy.SelectedValue == "0")
        {
            string xyz;
            try
            {

                xyz = lblCode.Text + txtRegNo.Text;
                getSelectedSubject(xyz);
                LoadDetail(xyz);
            }
            catch
            {
                HelperFunction.MsgBox(this, this.GetType(), "Student not found");
                txtName.Text = "";
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
    }

    protected void LoadCollegeCode()
    {
        NAMEEnt = new HSS_NAME();
        NAMEEnt = (HSS_NAME)NAMESer.GetSingle(NAMEEnt);
        if (NAMEEnt != null)
        {
            if (ddlProgram.SelectedValue == "Select")
            {
                lblCode.Text = NAMEEnt.CODE;
            }
            else
            {
                lblCode.Text = NAMEEnt.CODE + ddlProgram.SelectedItem;
            }
        }
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

    protected string LoadSubjectname(string subid)
    {
        SUBEnt = new HSS_SUBJECT();
        SUBEnt.PK_ID = subid;
        SUBEnt.STATUS = "1";
        SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
        if (SUBEnt != null)
        {
            subjectname = SUBEnt.SUBJECT_NAME;
        }
        return subjectname;
    }

    protected void LoadStudentId()
    {
        if (ddlBatch.SelectedValue != "Select" && ddlSemester.SelectedValue != "Select")

        ddlStudentName.DataSource = hf.selectstudentinfo(ddlProgram.SelectedValue, ddlBatch.SelectedValue, ddlSemester.SelectedValue, "");
        ddlStudentName.DataTextField = "STUDENT_NAME";
        ddlStudentName.DataValueField = "STUDENT_ID";
        ddlStudentName.DataBind();
        ddlStudentName.Items.Insert(0, "Select");
    }

    protected void getSelectedSubject(string stdid)
    {
        CSEnt = new HSS_CURRENT_STUDENT();
        CSEnt.STUDENT_ID = stdid;
        CSEnt.SEMESTER = ddlSemester.SelectedValue;
        CSEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSEnt);
        if (CSEnt != null)
        {
            sub1 = CSEnt.SUBJ1;
            sub2 = CSEnt.SUBJ2;
            sub3 = CSEnt.SUBJ3;
            sub4 = CSEnt.SUBJ4;
            sub5 = CSEnt.SUBJ5;
            sub6 = CSEnt.SUBJ6;
            sub7 = CSEnt.SUBJ7;

            LoadGrdView(stdid, sub1, sub2, sub3, sub4, sub5, sub6, sub7);
        }
    }

    protected void LoadGrdView(string stdid, string sub1, string sub2, string sub3, string sub4, string sub5, string sub6, string sub7)
    {
        if (rbtnChooseDate.SelectedValue == "nepDate")
        {
            if (sub1 != "")
            {
                lblSub1.Visible = true;
                lblSub1.Text = LoadSubjectname(sub1);
                gridSub1.DataSource = GetData(stdid, ddlSemester.SelectedValue, sub1);
                gridSub1.DataBind();
            }
            else
            {
                lblSub1.Visible = false;
            }
            if (sub2 != "")
            {
                lblSub2.Visible = true;
                lblSub2.Text = LoadSubjectname(sub2);
                gridSub2.DataSource = GetData(stdid, ddlSemester.SelectedValue, sub2);
                gridSub2.DataBind();
            }
            else
            {
                lblSub2.Visible = false;
            }
            if (sub3 != "")
            {
                lblSub3.Visible = true;
                lblSub3.Text = LoadSubjectname(sub3);
                gridSub3.DataSource = GetData(stdid, ddlSemester.SelectedValue, sub3);
                gridSub3.DataBind();
            }
            else
            {
                lblSub3.Visible = false;
            }
            if (sub4 != "")
            {
                lblSub4.Visible = true;
                lblSub4.Text = LoadSubjectname(sub4);
                gridSub4.DataSource = GetData(stdid, ddlSemester.SelectedValue, sub4);
                gridSub4.DataBind();
            }
            else
            {
                lblSub4.Visible = false;
            }
            if (sub5 != "")
            {
                lblSub5.Visible = true;
                lblSub5.Text = LoadSubjectname(sub5);
                gridSub5.DataSource = GetData(stdid, ddlSemester.SelectedValue, sub5);
                gridSub5.DataBind();
            }
            else
            {
                lblSub5.Visible = false;
            }
            if (sub6 != "")
            {
                lblSub6.Visible = true;
                lblSub6.Text = LoadSubjectname(sub6);
                gridSub6.DataSource = GetData(stdid, ddlSemester.SelectedValue, sub6);
                gridSub6.DataBind();
            }
            else
            {
                lblSub6.Visible = false;
            }
            if (sub7 != "")
            {
                lblSub7.Visible = true;
                lblSub7.Text = LoadSubjectname(sub7);
                gridSub7.DataSource = GetData(stdid, ddlSemester.SelectedValue, sub7);
                gridSub7.DataBind();
            }
            else
            {
                lblSub7.Visible = false;
            }

            ArrayList alist = new ArrayList();
            alist.Add(1);

            gridAggregate.DataSource = alist;
            gridAggregate.DataBind();

        }
        else if (rbtnChooseDate.SelectedValue == "engDate")
        {
            if (sub1 != "")
            {
                lblSub1.Visible = true;
                lblSub1.Text = LoadSubjectname(sub1);
                gridSub1.DataSource = GetData_ad(stdid, ddlSemester.SelectedValue, sub1);
                gridSub1.DataBind();
            }
            else
            {
                lblSub1.Visible = false;
            }
            if (sub2 != "")
            {
                lblSub2.Visible = true;
                lblSub2.Text = LoadSubjectname(sub2);
                gridSub2.DataSource = GetData_ad(stdid, ddlSemester.SelectedValue, sub2);
                gridSub2.DataBind();
            }
            else
            {
                lblSub2.Visible = false;
            }
            if (sub3 != "")
            {
                lblSub3.Visible = true;
                lblSub3.Text = LoadSubjectname(sub3);
                gridSub3.DataSource = GetData_ad(stdid, ddlSemester.SelectedValue, sub3);
                gridSub3.DataBind();
            }
            else
            {
                lblSub3.Visible = false;
            }
            if (sub4 != "")
            {
                lblSub4.Visible = true;
                lblSub4.Text = LoadSubjectname(sub4);
                gridSub4.DataSource = GetData_ad(stdid, ddlSemester.SelectedValue, sub4);
                gridSub4.DataBind();
            }
            else
            {
                lblSub4.Visible = false;
            }
            if (sub5 != "")
            {
                lblSub5.Visible = true;
                lblSub5.Text = LoadSubjectname(sub5);
                gridSub5.DataSource = GetData_ad(stdid, ddlSemester.SelectedValue, sub5);
                gridSub5.DataBind();
            }
            else
            {
                lblSub5.Visible = false;
            }
            if (sub6 != "")
            {
                lblSub6.Visible = true;
                lblSub6.Text = LoadSubjectname(sub6);
                gridSub6.DataSource = GetData_ad(stdid, ddlSemester.SelectedValue, sub6);
                gridSub6.DataBind();
            }
            else
            {
                lblSub6.Visible = false;
            }
            if (sub7 != "")
            {
                lblSub7.Visible = true;
                lblSub7.Text = LoadSubjectname(sub7);
                gridSub7.DataSource = GetData_ad(stdid, ddlSemester.SelectedValue, sub7);
                gridSub7.DataBind();
            }
            else
            {
                lblSub7.Visible = false;
            }

            ArrayList alist = new ArrayList();
            alist.Add(1);

            gridAggregate.DataSource = alist;
            gridAggregate.DataBind();
        }
    }

    protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        #region item
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            
            Label lbltotalpresent_Agg = e.Row.FindControl("lbltotalpresent_Agg") as Label;
            Label lbltotalclass_Agg = e.Row.FindControl("lbltotalclass_Agg") as Label;

            Label lblTotalAbsent_Agg = e.Row.FindControl("lblTotalAbsent_Agg") as Label;
            Label lblTotalLeave_Agg = e.Row.FindControl("lblTotalLeave_Agg") as Label;
            Label lblPresentPercent_Agg = e.Row.FindControl("lblPresentPercent_Agg") as Label;
            Label lblMarks_Agg = e.Row.FindControl("lblMarks_Agg") as Label;
            
            lbltotalpresent_Agg.Text = (totalpresent1 + totalpresent2 + totalpresent3 + totalpresent4 + totalpresent5 + totalpresent6 + totalpresent7).ToString();
            lbltotalclass_Agg.Text = (totaldays1 + totaldays2 + totaldays3 + totaldays4 + totaldays5 + totaldays6 + totaldays7).ToString();

            lblTotalLeave_Agg.Text = (totalleave1 + totalleave2 + totalleave3 + totalleave4 + totalleave5 + totalleave6 + totalleave7).ToString();
            lblTotalAbsent_Agg.Text = (totalabsent1 + totalabsent2 + totalabsent3 + totalabsent4 + totalabsent5 + totalabsent6 + totalabsent7).ToString();

            lblPresentPercent_Agg.Text = ((Convert.ToDouble(lbltotalpresent_Agg.Text) / Convert.ToDouble(lbltotalclass_Agg.Text) * 100)).ToString("0.00");
            lblMarks_Agg.Text = ((Convert.ToDouble(lbltotalpresent_Agg.Text) / Convert.ToDouble(lbltotalclass_Agg.Text) * 5)).ToString("0.00");
            
        }
        #endregion

    }

    public DataTable GetData(string studentid, string semester, string subject)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.individual_attendance";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = studentid;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = semester;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = subject;
        objCmd.Parameters.Add(_p3);

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;
    }

    public DataTable GetData_ad(string studentid, string semester, string subject)
    {
        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();

        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.individual_attendance_ad";

        objCmd.Connection = conn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter _p1 = new OracleParameter();
        _p1.Direction = ParameterDirection.Input;
        _p1.Value = studentid;
        objCmd.Parameters.Add(_p1);

        OracleParameter _p2 = new OracleParameter();
        _p2.Direction = ParameterDirection.Input;
        _p2.Value = semester;
        objCmd.Parameters.Add(_p2);

        OracleParameter _p3 = new OracleParameter();
        _p3.Direction = ParameterDirection.Input;
        _p3.Value = subject;
        objCmd.Parameters.Add(_p3);

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        conn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        conn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;
    }

    protected void dlViewBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlViewBy.SelectedValue == "0")
        {
            txtRegNo.Visible = true;
            ddlStudentName.Visible = false;

        }
        if (ddlViewBy.SelectedValue == "1")
        {
            txtRegNo.Visible = false;
            ddlStudentName.Visible = true;

        }
    }
    protected void txtName_TextChanged(object sender, EventArgs e)
    {
        string[] xyz;
        try
        {
            xyz = txtName.Text.Split('-');
            txtRegNo.Text = xyz[1];
            getSelectedSubject(txtRegNo.Text);
            LoadDetail(txtRegNo.Text);
        }
        catch
        {
            HelperFunction.MsgBox(this, this.GetType(), "Student not found");
            txtName.Text = "";
        }
    }
    protected void txtRegNo_TextChanged(object sender, EventArgs e)
    {

        LoadDetail(lblCode.Text + txtRegNo.Text);


    }
    protected void LoadDetail(string stdid)
    {

        CSEnt = new HSS_CURRENT_STUDENT();

        CSEnt.STUDENT_ID = stdid;
        CSEnt.SEMESTER = ddlSemester.SelectedValue;

        CSEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSEnt);
        if (CSEnt != null)
        {
            STDEnt = new HSS_STUDENT();
            STDEnt.STUDENT_ID = stdid;
            STDEnt = (HSS_STUDENT)STDSer.GetSingle(STDEnt);
            if (STDEnt != null)
            {
                lblRegNo.Text = CSEnt.STUDENT_ID;
                lblName.Text = STDEnt.NAME_ENGLISH;
                //lblPhone.Text = STDEnt.MOBILE + "," + STDEnt.PHONE;
                //lblGuardian.Text = STDEnt.G_NAME + "  (" + STDEnt.G_RELATION + ")";
                //lblGuardianNo.Text = STDEnt.G_MOBILE + "," + STDEnt.G_PHONE;

                imgStudent.ImageUrl = "~/images/bachelorstudent/" + stdid + ".jpg";
                PEnt = new program();
                PEnt.PK_ID = STDEnt.PROGRAM;

                lblGrage.Text = ddlProgram.SelectedItem + " " + ddlSemester.SelectedItem.ToString() + " Semester";
                lblSection.Text = "'" + CSEnt.SECTION + "'";

                if (ddlViewBy.SelectedIndex == 0)
                {
                    getSelectedSubject(stdid);
                }
                detail.Visible = true;
            }
        }
        else
        {
            HelperFunction.MsgBox(this, this.GetType(), "Invalid Student ID");
            detail.Visible = false;
        }
    }
    protected void grdView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        #region item
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            double totaldays = 0;
            double totalpresent = 0;
            double totalabsent = 0;
            double totalleave = 0;

            Label lbltotalpresent = e.Row.FindControl("lbltotalpresent") as Label;
            Label lbltotaldays = e.Row.FindControl("lbltotaldays") as Label;

            Label lblTotalAbsent = e.Row.FindControl("lblTotalAbsent") as Label;
            Label lblTotalLeave = e.Row.FindControl("lblTotalLeave") as Label;
            Label lblPresentPercent = e.Row.FindControl("lblPresentPercent") as Label;
            Label lblMarks = e.Row.FindControl("lblMarks") as Label;
            Label lblMonth = e.Row.FindControl("lblMonth") as Label;
            Label lblMonthName = e.Row.FindControl("lblMonthName") as Label;
            #region to convert month id to month name
            if (rbtnChooseDate.SelectedValue == "nepDate")
            {
                MEnt = new Months();
                MEnt.MONTHID = lblMonth.Text;
                MEnt = (Months)MSer.GetSingle(MEnt);
                if (MEnt != null)
                {
                    lblMonthName.Text = MEnt.MONTHNAME;

                }
            }
            else
            {
                MEnt = new Months();
                MEnt.MONTHID = lblMonth.Text;
                MEnt = (Months)MSer.GetSingle(MEnt);
                if (MEnt != null)
                {
                    lblMonthName.Text = MEnt.MONTHNAME_ENG;

                }
            }

            #endregion

            string lblday = "lblday";
            for (int i = 1; i <= 32; i++)
            {
                Label lbldayn = e.Row.FindControl(lblday + i.ToString()) as Label;

                STDEnt = new HSS_STUDENT();
                STDEnt.STUDENT_ID = txtRegNo.Text;
                STDEnt = (HSS_STUDENT)STDSer.GetSingle(STDEnt);
                if (lbldayn.Text == "")
                {
                    lbldayn.Text = "-";
                }
                else if (lbldayn.Text == "A")
                {
                    lbldayn.Text = "<font color='red'>A</font>";
                    totaldays++;
                    totalabsent++;
                }
                else if (lbldayn.Text == "L")
                {
                    lbldayn.Text = "<font color='red'>P</font>";
                    totaldays++;
                    totalleave++;
                }
                else
                {
                    totaldays++;
                    totalpresent++;
                }
            }

            lbltotalpresent.Text = totalpresent.ToString();
            lbltotaldays.Text = totaldays.ToString();
            lblTotalAbsent.Text = totalabsent.ToString();
            lblTotalLeave.Text = totalleave.ToString();

            if (totaldays != 0)
            {
                lblPresentPercent.Text = ((totalpresent / totaldays) * 100).ToString("0.00");
                lblMarks.Text = ((totalpresent / totaldays) * 5).ToString("0.00");

                totalpresentpercent1 = totalpresentpercent1 + Convert.ToDouble(lblPresentPercent.Text);
                totalmarks1 = totalmarks1 + Convert.ToDouble(lblMarks.Text);
                totalpresent1 = totalpresent1 + Convert.ToDouble(lbltotalpresent.Text);
                totalabsent1 = totalabsent1 + Convert.ToDouble(lblTotalAbsent.Text);
                totalleave1 = totalleave1 + Convert.ToDouble(lblTotalLeave.Text);
                totaldays1 = totaldays1 + Convert.ToDouble(lbltotaldays.Text);

            }
            if (lbltotaldays.Text == "0")
            {
                e.Row.Visible = false;
            }
        }
        #endregion
    }
    protected void grdView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        #region item
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            double totaldays = 0;
            double totalpresent = 0;
            double totalabsent = 0;
            double totalleave = 0;

            Label lbltotalpresent = e.Row.FindControl("lbltotalpresent") as Label;
            Label lbltotaldays = e.Row.FindControl("lbltotaldays") as Label;

            Label lblTotalAbsent = e.Row.FindControl("lblTotalAbsent") as Label;
            Label lblTotalLeave = e.Row.FindControl("lblTotalLeave") as Label;
            Label lblPresentPercent = e.Row.FindControl("lblPresentPercent") as Label;
            Label lblMarks = e.Row.FindControl("lblMarks") as Label;
            Label lblMonth = e.Row.FindControl("lblMonth") as Label;
            Label lblMonthName = e.Row.FindControl("lblMonthName") as Label;
            #region to convert month id to month name
            if (rbtnChooseDate.SelectedValue == "nepDate")
            {
                MEnt = new Months();
                MEnt.MONTHID = lblMonth.Text;
                MEnt = (Months)MSer.GetSingle(MEnt);
                if (MEnt != null)
                {
                    lblMonthName.Text = MEnt.MONTHNAME;

                }
            }
            else
            {
                MEnt = new Months();
                MEnt.MONTHID = lblMonth.Text;
                MEnt = (Months)MSer.GetSingle(MEnt);
                if (MEnt != null)
                {
                    lblMonthName.Text = MEnt.MONTHNAME_ENG;

                }
            }

            #endregion


            string lblday = "lblday";
            for (int i = 1; i <= 32; i++)
            {
                Label lbldayn = e.Row.FindControl(lblday + i.ToString()) as Label;

                STDEnt = new HSS_STUDENT();
                STDEnt.STUDENT_ID = txtRegNo.Text;
                STDEnt = (HSS_STUDENT)STDSer.GetSingle(STDEnt);
                if (lbldayn.Text == "")
                {
                    lbldayn.Text = "-";
                }
                else if (lbldayn.Text == "A")
                {
                    lbldayn.Text = "<font color='red'>A</font>";
                    totaldays++;
                    totalabsent++;
                }
                else if (lbldayn.Text == "L")
                {
                    lbldayn.Text = "<font color='red'>P</font>";
                    totaldays++;
                    totalleave++;
                }
                else
                {
                    totaldays++;
                    totalpresent++;
                }


            }


            lbltotalpresent.Text = totalpresent.ToString();
            lbltotaldays.Text = totaldays.ToString();
            lblTotalAbsent.Text = totalabsent.ToString();
            lblTotalLeave.Text = totalleave.ToString();
            if (totaldays != 0)
            {
                lblPresentPercent.Text = ((totalpresent / totaldays) * 100).ToString("0.00");
                lblMarks.Text = ((totalpresent / totaldays) * 5).ToString("0.00");


                totalpresentpercent2 = totalpresentpercent2 + Convert.ToDouble(lblPresentPercent.Text);
                totalmarks2 = totalmarks2 + Convert.ToDouble(lblMarks.Text);
                totalpresent2 = totalpresent2 + Convert.ToDouble(lbltotalpresent.Text);
                totalabsent2 = totalabsent2 + Convert.ToDouble(lblTotalAbsent.Text);
                totalleave2 = totalleave2 + Convert.ToDouble(lblTotalLeave.Text);
                totaldays2 = totaldays2 + Convert.ToDouble(lbltotaldays.Text);
            }




            if (lbltotaldays.Text == "0")
            {

                e.Row.Visible = false;

            }
        }
        #endregion
    }
    protected void grdView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        #region item
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            double totaldays = 0;
            double totalpresent = 0;
            double totalabsent = 0;
            double totalleave = 0;

            Label lbltotalpresent = e.Row.FindControl("lbltotalpresent") as Label;
            Label lbltotaldays = e.Row.FindControl("lbltotaldays") as Label;

            Label lblTotalAbsent = e.Row.FindControl("lblTotalAbsent") as Label;
            Label lblTotalLeave = e.Row.FindControl("lblTotalLeave") as Label;
            Label lblPresentPercent = e.Row.FindControl("lblPresentPercent") as Label;
            Label lblMarks = e.Row.FindControl("lblMarks") as Label;
            Label lblMonth = e.Row.FindControl("lblMonth") as Label;
            Label lblMonthName = e.Row.FindControl("lblMonthName") as Label;
            #region to convert month id to month name
            if (rbtnChooseDate.SelectedValue == "nepDate")
            {
                MEnt = new Months();
                MEnt.MONTHID = lblMonth.Text;
                MEnt = (Months)MSer.GetSingle(MEnt);
                if (MEnt != null)
                {
                    lblMonthName.Text = MEnt.MONTHNAME;

                }
            }
            else
            {
                MEnt = new Months();
                MEnt.MONTHID = lblMonth.Text;
                MEnt = (Months)MSer.GetSingle(MEnt);
                if (MEnt != null)
                {
                    lblMonthName.Text = MEnt.MONTHNAME_ENG;

                }
            }

            #endregion


            string lblday = "lblday";
            for (int i = 1; i <= 32; i++)
            {
                Label lbldayn = e.Row.FindControl(lblday + i.ToString()) as Label;

                STDEnt = new HSS_STUDENT();
                STDEnt.STUDENT_ID = txtRegNo.Text;
                STDEnt = (HSS_STUDENT)STDSer.GetSingle(STDEnt);
                if (lbldayn.Text == "")
                {
                    lbldayn.Text = "-";
                }
                else if (lbldayn.Text == "A")
                {
                    lbldayn.Text = "<font color='red'>A</font>";
                    totaldays++;
                    totalabsent++;
                }
                else if (lbldayn.Text == "L")
                {
                    lbldayn.Text = "<font color='red'>P</font>";
                    totaldays++;
                    totalleave++;
                }
                else
                {
                    totaldays++;
                    totalpresent++;
                }
            }


            lbltotalpresent.Text = totalpresent.ToString();
            lbltotaldays.Text = totaldays.ToString();
            lblTotalAbsent.Text = totalabsent.ToString();
            lblTotalLeave.Text = totalleave.ToString();
            if (totaldays != 0)
            {
                lblPresentPercent.Text = ((totalpresent / totaldays) * 100).ToString("0.00");
                lblMarks.Text = ((totalpresent / totaldays) * 5).ToString("0.00");

                totalpresentpercent3 = totalpresentpercent3 + Convert.ToDouble(lblPresentPercent.Text);
                totalmarks3 = totalmarks3 + Convert.ToDouble(lblMarks.Text);
                totalpresent3 = totalpresent3 + Convert.ToDouble(lbltotalpresent.Text);
                totalabsent3 = totalabsent3 + Convert.ToDouble(lblTotalAbsent.Text);
                totalleave3 = totalleave3 + Convert.ToDouble(lblTotalLeave.Text);
                totaldays3 = totaldays3 + Convert.ToDouble(lbltotaldays.Text);
            }

            if (lbltotaldays.Text == "0")
            {

                e.Row.Visible = false;

            }
        }
        #endregion
    }
    protected void grdView4_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        #region item
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            double totaldays = 0;
            double totalpresent = 0;
            double totalabsent = 0;
            double totalleave = 0;

            Label lbltotalpresent = e.Row.FindControl("lbltotalpresent") as Label;
            Label lbltotaldays = e.Row.FindControl("lbltotaldays") as Label;

            Label lblTotalAbsent = e.Row.FindControl("lblTotalAbsent") as Label;
            Label lblTotalLeave = e.Row.FindControl("lblTotalLeave") as Label;
            Label lblPresentPercent = e.Row.FindControl("lblPresentPercent") as Label;
            Label lblMarks = e.Row.FindControl("lblMarks") as Label;
            Label lblMonth = e.Row.FindControl("lblMonth") as Label;
            Label lblMonthName = e.Row.FindControl("lblMonthName") as Label;
            #region to convert month id to month name
            if (rbtnChooseDate.SelectedValue == "nepDate")
            {
                MEnt = new Months();
                MEnt.MONTHID = lblMonth.Text;
                MEnt = (Months)MSer.GetSingle(MEnt);
                if (MEnt != null)
                {
                    lblMonthName.Text = MEnt.MONTHNAME;

                }
            }
            else
            {
                MEnt = new Months();
                MEnt.MONTHID = lblMonth.Text;
                MEnt = (Months)MSer.GetSingle(MEnt);
                if (MEnt != null)
                {
                    lblMonthName.Text = MEnt.MONTHNAME_ENG;

                }
            }

            #endregion

            string lblday = "lblday";
            for (int i = 1; i <= 32; i++)
            {
                Label lbldayn = e.Row.FindControl(lblday + i.ToString()) as Label;

                STDEnt = new HSS_STUDENT();
                STDEnt.STUDENT_ID = txtRegNo.Text;
                STDEnt = (HSS_STUDENT)STDSer.GetSingle(STDEnt);
                if (lbldayn.Text == "")
                {
                    lbldayn.Text = "-";
                }
                else if (lbldayn.Text == "A")
                {
                    lbldayn.Text = "<font color='red'>A</font>";
                    totaldays++;
                    totalabsent++;
                }
                else if (lbldayn.Text == "L")
                {
                    lbldayn.Text = "<font color='red'>P</font>";
                    totaldays++;
                    totalleave++;
                }
                else
                {
                    totaldays++;
                    totalpresent++;
                }
            }

            lbltotalpresent.Text = totalpresent.ToString();
            lbltotaldays.Text = totaldays.ToString();
            lblTotalAbsent.Text = totalabsent.ToString();
            lblTotalLeave.Text = totalleave.ToString();
            if (totaldays != 0)
            {
                lblPresentPercent.Text = ((totalpresent / totaldays) * 100).ToString("0.00");
                lblMarks.Text = ((totalpresent / totaldays) * 5).ToString("0.00");

                totalpresentpercent4 = totalpresentpercent4 + Convert.ToDouble(lblPresentPercent.Text);
                totalmarks4 = totalmarks4 + Convert.ToDouble(lblMarks.Text);
                totalpresent4 = totalpresent4 + Convert.ToDouble(lbltotalpresent.Text);
                totalabsent4 = totalabsent4 + Convert.ToDouble(lblTotalAbsent.Text);
                totalleave4 = totalleave4 + Convert.ToDouble(lblTotalLeave.Text);
                totaldays4 = totaldays4 + Convert.ToDouble(lbltotaldays.Text);
            }

            if (lbltotaldays.Text == "0")
            {

                e.Row.Visible = false;

            }
        }
        #endregion
    }
    protected void grdView5_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        #region item
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            double totaldays = 0;
            double totalpresent = 0;
            double totalabsent = 0;
            double totalleave = 0;

            Label lbltotalpresent = e.Row.FindControl("lbltotalpresent") as Label;
            Label lbltotaldays = e.Row.FindControl("lbltotaldays") as Label;

            Label lblTotalAbsent = e.Row.FindControl("lblTotalAbsent") as Label;
            Label lblTotalLeave = e.Row.FindControl("lblTotalLeave") as Label;
            Label lblPresentPercent = e.Row.FindControl("lblPresentPercent") as Label;
            Label lblMarks = e.Row.FindControl("lblMarks") as Label;
            Label lblMonth = e.Row.FindControl("lblMonth") as Label;
            Label lblMonthName = e.Row.FindControl("lblMonthName") as Label;
            #region to convert month id to month name
            if (rbtnChooseDate.SelectedValue == "nepDate")
            {
                MEnt = new Months();
                MEnt.MONTHID = lblMonth.Text;
                MEnt = (Months)MSer.GetSingle(MEnt);
                if (MEnt != null)
                {
                    lblMonthName.Text = MEnt.MONTHNAME;

                }
            }
            else
            {
                MEnt = new Months();
                MEnt.MONTHID = lblMonth.Text;
                MEnt = (Months)MSer.GetSingle(MEnt);
                if (MEnt != null)
                {
                    lblMonthName.Text = MEnt.MONTHNAME_ENG;

                }
            }

            #endregion

            string lblday = "lblday";
            for (int i = 1; i <= 32; i++)
            {
                Label lbldayn = e.Row.FindControl(lblday + i.ToString()) as Label;

                STDEnt = new HSS_STUDENT();
                STDEnt.STUDENT_ID = txtRegNo.Text;
                STDEnt = (HSS_STUDENT)STDSer.GetSingle(STDEnt);
                if (lbldayn.Text == "")
                {
                    lbldayn.Text = "-";
                }
                else if (lbldayn.Text == "A")
                {
                    lbldayn.Text = "<font color='red'>A</font>";
                    totaldays++;
                    totalabsent++;
                }
                else if (lbldayn.Text == "L")
                {
                    lbldayn.Text = "<font color='red'>P</font>";
                    totaldays++;
                    totalleave++;
                }
                else
                {
                    totaldays++;
                    totalpresent++;
                }
            }
            
            lbltotalpresent.Text = totalpresent.ToString();
            lbltotaldays.Text = totaldays.ToString();
            lblTotalAbsent.Text = totalabsent.ToString();
            lblTotalLeave.Text = totalleave.ToString();
            if (totaldays != 0)
            {
                lblPresentPercent.Text = ((totalpresent / totaldays) * 100).ToString("0.00");
                lblMarks.Text = ((totalpresent / totaldays) * 5).ToString("0.00");

                totalpresentpercent5 = totalpresentpercent5 + Convert.ToDouble(lblPresentPercent.Text);
                totalmarks5 = totalmarks5 + Convert.ToDouble(lblMarks.Text);
                totalpresent5 = totalpresent5 + Convert.ToDouble(lbltotalpresent.Text);
                totalabsent5 = totalabsent5 + Convert.ToDouble(lblTotalAbsent.Text);
                totalleave5 = totalleave5 + Convert.ToDouble(lblTotalLeave.Text);
                totaldays5 = totaldays5 + Convert.ToDouble(lbltotaldays.Text);
            }
            
            if (lbltotaldays.Text == "0")
            {

                e.Row.Visible = false;

            }
        }
        #endregion
    }
    protected void grdView6_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        #region item
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            double totaldays = 0;
            double totalpresent = 0;
            double totalabsent = 0;
            double totalleave = 0;

            Label lbltotalpresent = e.Row.FindControl("lbltotalpresent") as Label;
            Label lbltotaldays = e.Row.FindControl("lbltotaldays") as Label;

            Label lblTotalAbsent = e.Row.FindControl("lblTotalAbsent") as Label;
            Label lblTotalLeave = e.Row.FindControl("lblTotalLeave") as Label;
            Label lblPresentPercent = e.Row.FindControl("lblPresentPercent") as Label;
            Label lblMarks = e.Row.FindControl("lblMarks") as Label;
            Label lblMonth = e.Row.FindControl("lblMonth") as Label;
            Label lblMonthName = e.Row.FindControl("lblMonthName") as Label;
            #region to convert month id to month name
            if (rbtnChooseDate.SelectedValue == "nepDate")
            {
                MEnt = new Months();
                MEnt.MONTHID = lblMonth.Text;
                MEnt = (Months)MSer.GetSingle(MEnt);
                if (MEnt != null)
                {
                    lblMonthName.Text = MEnt.MONTHNAME;

                }
            }
            else
            {
                MEnt = new Months();
                MEnt.MONTHID = lblMonth.Text;
                MEnt = (Months)MSer.GetSingle(MEnt);
                if (MEnt != null)
                {
                    lblMonthName.Text = MEnt.MONTHNAME_ENG;

                }
            }

            #endregion

            string lblday = "lblday";
            for (int i = 1; i <= 32; i++)
            {
                Label lbldayn = e.Row.FindControl(lblday + i.ToString()) as Label;

                STDEnt = new HSS_STUDENT();
                STDEnt.STUDENT_ID = txtRegNo.Text;
                STDEnt = (HSS_STUDENT)STDSer.GetSingle(STDEnt);
                if (lbldayn.Text == "")
                {
                    lbldayn.Text = "-";
                }
                else if (lbldayn.Text == "A")
                {
                    lbldayn.Text = "<font color='red'>A</font>";
                    totaldays++;
                    totalabsent++;
                }
                else if (lbldayn.Text == "L")
                {
                    lbldayn.Text = "<font color='red'>P</font>";
                    totaldays++;
                    totalleave++;
                }
                else
                {
                    totaldays++;
                    totalpresent++;
                }


            }


            lbltotalpresent.Text = totalpresent.ToString();
            lbltotaldays.Text = totaldays.ToString();
            lblTotalAbsent.Text = totalabsent.ToString();
            lblTotalLeave.Text = totalleave.ToString();
            if (totaldays != 0)
            {
                lblPresentPercent.Text = ((totalpresent / totaldays) * 100).ToString("0.00");
                lblMarks.Text = ((totalpresent / totaldays) * 5).ToString("0.00");

                totalpresentpercent6 = totalpresentpercent6 + Convert.ToDouble(lblPresentPercent.Text);
                totalmarks6 = totalmarks6 + Convert.ToDouble(lblMarks.Text);
                totalpresent6 = totalpresent6 + Convert.ToDouble(lbltotalpresent.Text);
                totalabsent6 = totalabsent6 + Convert.ToDouble(lblTotalAbsent.Text);
                totalleave6 = totalleave6 + Convert.ToDouble(lblTotalLeave.Text);
                totaldays6 = totaldays6 + Convert.ToDouble(lbltotaldays.Text);
            }



            if (lbltotaldays.Text == "0")
            {

                e.Row.Visible = false;

            }
        }
        #endregion
    }
    protected void grdView7_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        #region item
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            double totaldays = 0;
            double totalpresent = 0;
            double totalabsent = 0;
            double totalleave = 0;

            Label lbltotalpresent = e.Row.FindControl("lbltotalpresent") as Label;
            Label lbltotaldays = e.Row.FindControl("lbltotaldays") as Label;

            Label lblTotalAbsent = e.Row.FindControl("lblTotalAbsent") as Label;
            Label lblTotalLeave = e.Row.FindControl("lblTotalLeave") as Label;
            Label lblPresentPercent = e.Row.FindControl("lblPresentPercent") as Label;
            Label lblMarks = e.Row.FindControl("lblMarks") as Label;
            Label lblMonth = e.Row.FindControl("lblMonth") as Label;
            Label lblMonthName = e.Row.FindControl("lblMonthName") as Label;

            #region to convert month id to month name
            if (rbtnChooseDate.SelectedValue == "nepDate")
            {
                MEnt = new Months();
                MEnt.MONTHID = lblMonth.Text;
                MEnt = (Months)MSer.GetSingle(MEnt);
                if (MEnt != null)
                {
                    lblMonthName.Text = MEnt.MONTHNAME;

                }
            }
            else
            {
                MEnt = new Months();
                MEnt.MONTHID = lblMonth.Text;
                MEnt = (Months)MSer.GetSingle(MEnt);
                if (MEnt != null)
                {
                    lblMonthName.Text = MEnt.MONTHNAME_ENG;

                }
            }

            #endregion

            string lblday = "lblday";
            for (int i = 1; i <= 32; i++)
            {
                Label lbldayn = e.Row.FindControl(lblday + i.ToString()) as Label;

                STDEnt = new HSS_STUDENT();
                STDEnt.STUDENT_ID = txtRegNo.Text;
                STDEnt = (HSS_STUDENT)STDSer.GetSingle(STDEnt);
                if (lbldayn.Text == "")
                {
                    lbldayn.Text = "-";
                }
                else if (lbldayn.Text == "A")
                {
                    lbldayn.Text = "<font color='red'>A</font>";
                    totaldays++;
                    totalabsent++;
                }
                else if (lbldayn.Text == "L")
                {
                    lbldayn.Text = "<font color='red'>P</font>";
                    totaldays++;
                    totalleave++;
                }
                else
                {
                    totaldays++;
                    totalpresent++;
                }
                
            }


            lbltotalpresent.Text = totalpresent.ToString();
            lbltotaldays.Text = totaldays.ToString();
            lblTotalAbsent.Text = totalabsent.ToString();
            lblTotalLeave.Text = totalleave.ToString();
            if (totaldays != 0)
            {
                lblPresentPercent.Text = ((totalpresent / totaldays) * 100).ToString("0.00");
                lblMarks.Text = ((totalpresent / totaldays) * 5).ToString("0.00");

                totalpresentpercent7 = totalpresentpercent7 + Convert.ToDouble(lblPresentPercent.Text);
                totalmarks7 = totalmarks7 + Convert.ToDouble(lblMarks.Text);
                totalpresent7 = totalpresent7 + Convert.ToDouble(lbltotalpresent.Text);
                totalabsent7 = totalabsent7 + Convert.ToDouble(lblTotalAbsent.Text);
                totalleave7 = totalleave7 + Convert.ToDouble(lblTotalLeave.Text);
                totaldays7 = totaldays7 + Convert.ToDouble(lbltotaldays.Text);

            }


            if (lbltotaldays.Text == "0")
            {

                e.Row.Visible = false;

            }
        }
        #endregion
    }

    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlBatch.SelectedValue != "Select")
        {

            LoadCollegeCode();
        }
        //else
        //{
        //    ddlSemester.Items.Clear();
        //    ddlSemester.Items.Insert(0, "Select");
        //}
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlFaculty.SelectedValue != "Select")
        //{
        LoadProgram();
        //}
        //else
        //{
        //    ddlProgram.Items.Clear();
        //    ddlProgram.Items.Insert(0, "Select");


        //}
        //if (ddlProgram.SelectedValue == "Select")
        //{

        //    ddlBatch.Items.Clear();
        //    ddlBatch.Items.Insert(0, "Select");

        //    ddlSemester.Items.Clear();
        //    ddlSemester.Items.Insert(0, "Select");
        //}

    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProgram.SelectedValue != "Select")
        {
            LoadSemester();
            LoadCollegeCode();

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

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        LoadBatch();
        LoadStudentId();
        //txtName_AutoCompleteExtender.ContextKey = ddlSemester.SelectedValue;
        
    }

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }
    protected void ddlStudentName_SelectedIndexChanged(object sender, EventArgs e)
    {
        string xyz;
        try
        {
            xyz = ddlStudentName.SelectedValue;
            getSelectedSubject(xyz);
            LoadDetail(xyz);
        }
        catch
        {
            HelperFunction.MsgBox(this, this.GetType(), "Student not found");
            ddlStudentName.SelectedValue = "Select";
        }
    }


}