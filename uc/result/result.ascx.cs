using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Configuration;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using System.Reflection;
using Entity.Components;
using Service.Components;
using DataAccess.Components;
using DataAccess.Framework;
using DataHelper.Framework;
using Oracle.DataAccess.Client;
using Entity.Framework;


public partial class uc_test_result : System.Web.UI.UserControl
{
    string grade;
    string studentid;

    BatchYear BTEnt = new BatchYear();
    BatchYearService BTSer = new BatchYearService();

    HelperFunction hf = new HelperFunction();

    EXAM_TYPE ETEnt = new EXAM_TYPE();
    EXAM_TYPEService ETSer = new EXAM_TYPEService();

    HSS_SUBJECT SBEnt = new HSS_SUBJECT();
    HSS_SUBJECTService SBSer = new HSS_SUBJECTService();

    EXAM_MARKS EXMEnt = new EXAM_MARKS();
    EXAM_MARKSService EXMSer = new EXAM_MARKSService();

    HSS_RESULT REnt = new HSS_RESULT();
    HSS_RESULTSService RSer = new HSS_RESULTSService();

    HSS_GRADE GEnt = new HSS_GRADE();
    HSS_GRADEService GSer = new HSS_GRADEService();

    hss_faculty FCTEnt = new hss_faculty();
    hss_facultyService FCTSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    TEMP TEnt = new TEMP();
    TEMPService TSer = new TEMPService();

    HSS_STUDENT STEnt = new HSS_STUDENT();
    HSS_STUDENTService STSer = new HSS_STUDENTService();

    full_pass_marks FPMEnt = new full_pass_marks();
    full_pass_marksService FPMSer = new full_pass_marksService();

    double totdays = 0.0;
    double totpresentdays = 0.0;
    bool light = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadExamType();
            LoadBatch();
        }
    }
    protected void LoadExamType()
    {
        ETEnt = new EXAM_TYPE();

        ddlExamType.DataSource = ETSer.GetAll(ETEnt);
        ddlExamType.DataTextField = "EXAMTYPE";
        ddlExamType.DataValueField = "PKID";
        ddlExamType.DataBind();
    }
    protected void LoadBatch()
    {
        BTEnt = new BatchYear();
        ddlBatch.DataSource = BTSer.GetAll(BTEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
        ddlBatch.Items.Insert(0, "Select");
        ddlSemester.Items.Insert(0, "Select");
        ddlExamType.Items.Insert(0, "Select");
      
     

    }

    protected void LoadSemester()
    {
        BTEnt = new BatchYear();
        BTEnt.BATCH = ddlBatch.SelectedValue;
        BTEnt = (BatchYear)BTSer.GetSingle(BTEnt);
        if (BTEnt != null)
        {
            SMEnt = new semester();
            SMEnt.SYLLABUS_YEAR = BTEnt.SYLLABUS_YEAR;
            ddlSemester.DataSource = SMSer.GetAll(SMEnt);
            ddlSemester.DataTextField = "SEMESTER";
            ddlSemester.DataValueField = "PK_ID";
            ddlSemester.DataBind();
            ddlSemester.Items.Insert(0, "Select");

        }

    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        LoadData();

        insertRankinTemp();
        LoadSecondGrid();
        updateTemp();
        // LoadData();
        LoadDetailData();


    }

    private void LoadSecondGrid()
    {

        gridTemp.DataSource = getGroupedTotal(ddlSemester.SelectedValue, ddlExamType.SelectedValue);
        gridTemp.DataBind();
    }
    private void LoadData()
    {

        gridResult.DataSource = getResult(ddlSemester.SelectedValue, hf.NepaliYear(), ddlExamType.SelectedValue);
        gridResult.DataBind();


    }
    private void LoadDetailData()
    {

        gridRank.DataSource = getRankofStudent(ddlSemester.SelectedValue, ddlExamType.SelectedValue);
        gridRank.DataBind();


    }







    //..........funtion to find grade according to percentage...........
    private string getGrade(string percent)
    {

        double per = Convert.ToDouble(percent);


        for (int i = 1; i <= 6; i++)
        {
            GEnt = new HSS_GRADE();

            GEnt.PKID = i.ToString();

            GEnt = (HSS_GRADE)GSer.GetSingle(GEnt);

            if (per >= Convert.ToDouble(GEnt.PERCENT_FROM) && per < Convert.ToDouble(GEnt.PERCENT_TO))
            {
                grade = GEnt.GRADE;
                break;
            }

        }


        return grade;

    }


    private IDbDataParameter[] CreateParmans(string classno, string year, string examtype)
    {

        List<IDbDataParameter> cmdParams = new List<IDbDataParameter>();
        //input Parameters start
        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_class", classno));
        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_year", year));
        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_examtype", examtype));


        //input Parameters ends
        cmdParams.Add(DataAccessFactory.CreateDataParameter("Result", ""));

        return cmdParams.ToArray();
    }

    public DataTable getResult(string classno, string year, string examtype)
    {
        GenericGetValueService srvGen = new GenericGetValueService();
        DataTable DT = new DataTable();
        try
        {
            DT = srvGen.ReaderToTable("PKJ_REPORTS.student_result", System.Data.CommandType.StoredProcedure, CreateParmans(classno, year, examtype));
        }
        catch
        {
        }
        return DT;
    }



    private IDbDataParameter[] CreateParmans1(string classno, string examtype)
    {

        List<IDbDataParameter> cmdParams = new List<IDbDataParameter>();
        //input Parameters start
        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_class", classno));

        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_examtype", examtype));


        //input Parameters ends
        cmdParams.Add(DataAccessFactory.CreateDataParameter("Result", ""));

        return cmdParams.ToArray();
    }

    public DataTable getRank(string classno, string examtype)
    {
        GenericGetValueService srvGen = new GenericGetValueService();
        DataTable DT = new DataTable();
        try
        {
            DT = srvGen.ReaderToTable("PKJ_REPORTS.student_rank", System.Data.CommandType.StoredProcedure, CreateParmans1(classno, examtype));
        }
        catch
        {
        }
        return DT;
    }








    protected void gridResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblClass = e.Row.FindControl("lblClass") as Label;

            Label lblExamType = e.Row.FindControl("lblExamType") as Label;

            lblClass.Text = ddlSemester.SelectedValue;
            lblExamType.Text = ddlExamType.SelectedValue;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblTotal = e.Row.FindControl("lblTotal") as Label;
            Label lblTot = e.Row.FindControl("lblTot") as Label;
            Label lblMKUPTotal = e.Row.FindControl("lblMKUPTotal") as Label;
            Label lblStudentId = e.Row.FindControl("lblStudentId") as Label;
            Label lblRemarks = e.Row.FindControl("lblRemarks") as Label;
            Label lblPercentage = e.Row.FindControl("lblPercentage") as Label;
            Label lblRank = e.Row.FindControl("lblRank") as Label;
            EntityList theList = new EntityList();



            EXMEnt = new EXAM_MARKS();


            bool flag = false;

            EXMEnt.SEMESTER = ddlSemester.SelectedValue;
            EXMEnt.EXAM_TYPE = ddlExamType.SelectedValue;
            EXMEnt.STUDENT_ID = lblStudentId.Text;

            theList = EXMSer.GetAll(EXMEnt);

            foreach (EXAM_MARKS em in theList)
            {
                SBEnt = new HSS_SUBJECT();
                SBEnt.PK_ID = em.SUBJECT;

                SBEnt = (HSS_SUBJECT)SBSer.GetSingle(SBEnt);

                if (SBEnt != null)
                {
                    FPMEnt = new full_pass_marks();
                    FPMEnt.SUBJECT_ID = em.SUBJECT;
                    FPMEnt.EXAM_TYPE = em.EXAM_TYPE;
                    FPMEnt = (full_pass_marks)FPMSer.GetSingle(FPMEnt);

                    if (FPMEnt != null)
                    {
                        if (SBEnt.REMARKS == "T")
                        {
                            if (em.MAKEUP != "")
                            {
                                if (Convert.ToDouble(em.MAKEUP) < Convert.ToDouble(FPMEnt.PASSMARKS_THRCL))
                                {
                                    flag = true;
                                }

                            }
                            else
                            {
                                if (Convert.ToDouble(em.MARKS) < Convert.ToDouble(FPMEnt.PASSMARKS_THRCL))
                                {
                                    flag = true;
                                }
                            }

                        }
                        else if (SBEnt.REMARKS == "P")
                        {
                            if (em.MAKEUP != "")
                            {
                                if (Convert.ToDouble(em.MAKEUP) < Convert.ToDouble(FPMEnt.PASSMARKS_PRCL))
                                {
                                    flag = true;
                                }
                            }
                            else
                            {
                                if (Convert.ToDouble(em.MARKS) < Convert.ToDouble(FPMEnt.PASSMARKS_PRCL))
                                {
                                    flag = true;
                                }
                            }

                        }

                    }
                }
                
            }


            if (flag == true)
                lblRemarks.Text = "Fail";
            else
                lblRemarks.Text = "Pass";



            if (lblRemarks.Text == "Fail")
            {
                lblPercentage.Text = "-";
                lblRank.Text = "-";
            }

            EXMEnt = new EXAM_MARKS();

            EXMEnt.SEMESTER = ddlSemester.SelectedValue;
            EXMEnt.EXAM_TYPE = ddlExamType.SelectedValue;
            EXMEnt.STUDENT_ID = lblStudentId.Text;

            theList = EXMSer.GetAll(EXMEnt);

            foreach (EXAM_MARKS em in theList)
            {
                if (em.MAKEUP == "")
                {
                    light = false;
                }
                else if (em.MAKEUP != "")
                {
                    light = true;
                    break;
                }
            }
            if (light == true)
            {
                lblTot.Text = lblMKUPTotal.Text;


            }
            else
            {
                lblTot.Text = lblTotal.Text;
            }

        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblGrade = e.Row.FindControl("lblGrade") as Label;
            Label lblRemarks = e.Row.FindControl("lblRemarks") as Label;
            Label lblPercentage = e.Row.FindControl("lblPercentage") as Label;

            if (lblRemarks.Text == "Fail")
            {
                lblGrade.Text = "-";
            }
            else
            {
                lblGrade.Text = getGrade(lblPercentage.Text);
            }

        }


    }


    protected void insertDataInResult()
    {
        REnt = new HSS_RESULT();
        REnt.CLASS = ddlSemester.SelectedValue;
        REnt.EXAMTYPE = ddlExamType.SelectedValue;
        REnt.SESSION_YEAR = hf.CurrentYear(hf.NepaliMonth(), hf.NepaliYear());

        if (RSer.Delete(REnt) <= 1)
        {
            foreach (GridViewRow row in gridRank.Rows)
            {



                Label lblStudentId = row.FindControl("lblStuId") as Label;
                Label lblClass = row.FindControl("lblClas") as Label;
                Label lblExamType = row.FindControl("lblExtp") as Label;
                Label lblTotal = row.FindControl("lblTTL") as Label;
                Label lblPercentage = row.FindControl("lblPercent") as Label;
                Label lblRemarks = row.FindControl("lblRemark") as Label;
                Label lblGrade = row.FindControl("lblGrd") as Label;
                Label lblRank = row.FindControl("lblRnk") as Label;

                REnt.STUDENTID = lblStudentId.Text;
                REnt.CLASS = lblClass.Text;
                REnt.EXAMTYPE = lblExamType.Text;
                REnt.TOTAL = lblTotal.Text;
                REnt.PERCENTAGE = lblPercentage.Text;
                REnt.REMARKS = lblRemarks.Text;
                REnt.GRADE = lblGrade.Text;
                REnt.RANK = lblRank.Text;
                REnt.NDATE = txtDate.Text;
                REnt.SESSION_YEAR = hf.CurrentYear(hf.NepaliMonth(), hf.NepaliYear());

                if (RSer.Insert(REnt) >= 1)
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
                }

                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Insertion is not Successfull')", true);
                }
            }
        }

    }

    protected void btnSave_Click1(object sender, EventArgs e)
    {
        insertDataInResult();
        LoadAttendance();
        clearTemp();


    }

    protected void clearTemp()
    {
        TSer.Delete(TEnt);
    }


    protected void insertRankinTemp()
    {
        if (TSer.Delete(TEnt) <= 1)
        {
            foreach (GridViewRow row in gridResult.Rows)
            {



                Label lblStudentId = row.FindControl("lblStudentId") as Label;
                Label lblClass = row.FindControl("lblClass") as Label;
                Label lblExamType = row.FindControl("lblExamType") as Label;

                Label lblTot = row.FindControl("lblTot") as Label;
                Label lblPercentage = row.FindControl("lblPercentage") as Label;
                Label lblRemarks = row.FindControl("lblRemarks") as Label;
                Label lblGrade = row.FindControl("lblGrade") as Label;
                Label lblRank = row.FindControl("lblRank") as Label;


                TEnt.STUDENTID = lblStudentId.Text;
                TEnt.CLASS = lblClass.Text;
                TEnt.EXAM_TYPE = lblExamType.Text;

                TEnt.TOTAL = lblTot.Text;


                TEnt.PERCENTAGE = lblPercentage.Text;
                TEnt.REMARKS = lblRemarks.Text;
                TEnt.GRADE = lblGrade.Text;
                TEnt.RANK = lblRank.Text;

                TSer.Insert(TEnt);

            }
        }

    }

    protected void updateTemp()
    {

        foreach (GridViewRow row in gridTemp.Rows)
        {

            Label lblSn = row.FindControl("lblSn") as Label;
            Label lblTotal = row.FindControl("lblTotal") as Label;


            TEnt.TOTAL = lblTotal.Text;

            TEnt.RANK = lblSn.Text;

            TSer.Update(TEnt);

        }

    }



    protected void btnRank_Click(object sender, EventArgs e)
    {

        LoadData();

    }

    private IDbDataParameter[] CreateParmans(string classno, string examtype)
    {

        List<IDbDataParameter> cmdParams = new List<IDbDataParameter>();
        //input Parameters start
        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_class", classno));

        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_examtype", examtype));


        //input Parameters ends
        cmdParams.Add(DataAccessFactory.CreateDataParameter("Result", ""));

        return cmdParams.ToArray();
    }

    public DataTable getRankofStudent(string classno, string examtype)
    {
        GenericGetValueService srvGen = new GenericGetValueService();
        DataTable DT = new DataTable();
        try
        {
            DT = srvGen.ReaderToTable("PKJ_REPORTS.student_rank", System.Data.CommandType.StoredProcedure, CreateParmans(classno, examtype));
        }
        catch
        {
        }
        return DT;
    }





    private IDbDataParameter[] CreateParmans2(string classno, string examtype)
    {

        List<IDbDataParameter> cmdParams = new List<IDbDataParameter>();
        //input Parameters start
        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_class", classno));

        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_examtype", examtype));


        //input Parameters ends
        cmdParams.Add(DataAccessFactory.CreateDataParameter("Result", ""));

        return cmdParams.ToArray();
    }

    public DataTable getGroupedTotal(string classno, string examtype)
    {
        GenericGetValueService srvGen = new GenericGetValueService();
        DataTable DT = new DataTable();
        try
        {
            DT = srvGen.ReaderToTable("PKJ_REPORTS.student_total_intemp", System.Data.CommandType.StoredProcedure, CreateParmans2(classno, examtype));
        }
        catch
        {
        }
        return DT;
    }

    protected void LoadAttendance()
    {
        EntityList theList = new EntityList();
        REnt = new HSS_RESULT();
        REnt.CLASS = ddlSemester.SelectedValue;
        REnt.SESSION_YEAR = hf.CurrentYear(hf.NepaliMonth(), hf.NepaliYear());
        REnt.EXAMTYPE = ddlExamType.SelectedItem.Text;
        theList = RSer.GetAll(REnt);
        foreach (HSS_RESULT rs in theList)
        {
            totdays = 0;
            totpresentdays = 0;
            studentid = rs.STUDENTID;
            LoadGrdView(rs.STUDENTID, rs.CLASS);

            UpdateAttendanceInResult(studentid, ddlExamType.SelectedItem.Text, ddlSemester.SelectedValue);

        }


    }
    protected void LoadGrdView(string stdid, string cls)
    {

        grdView.DataSource = GetData(stdid, cls);
        grdView.DataBind();
    }

    protected void UpdateAttendanceInResult(string stdid, string extype, string clas)
    {
        REnt = new HSS_RESULT();
        REnt.STUDENTID = stdid;
        REnt.EXAMTYPE = extype;
        REnt.CLASS = clas;
        REnt.SESSION_YEAR = hf.CurrentYear(hf.NepaliMonth(), hf.NepaliYear());
        REnt = (HSS_RESULT)RSer.GetSingle(REnt);
        if (REnt != null)
        {
            REnt.ATTENDANCE = totpresentdays + "/" + totdays;
            RSer.Update(REnt);
        }

    }


    protected void grdView_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        #region item
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            int totaldays = 0;
            int totalpresent = 0;
            Label lblMonth = e.Row.FindControl("lblMonth") as Label;
            Label lblday1 = e.Row.FindControl("lblday1") as Label;
            Label lblday2 = e.Row.FindControl("lblday2") as Label;
            Label lblday3 = e.Row.FindControl("lblday3") as Label;
            Label lblday4 = e.Row.FindControl("lblday4") as Label;
            Label lblday5 = e.Row.FindControl("lblday5") as Label;
            Label lblday6 = e.Row.FindControl("lblday6") as Label;
            Label lblday7 = e.Row.FindControl("lblday7") as Label;
            Label lblday8 = e.Row.FindControl("lblday8") as Label;
            Label lblday9 = e.Row.FindControl("lblday9") as Label;
            Label lblday10 = e.Row.FindControl("lblday10") as Label;
            Label lblday11 = e.Row.FindControl("lblday11") as Label;
            Label lblday12 = e.Row.FindControl("lblday12") as Label;
            Label lblday13 = e.Row.FindControl("lblday13") as Label;
            Label lblday14 = e.Row.FindControl("lblday14") as Label;
            Label lblday15 = e.Row.FindControl("lblday15") as Label;
            Label lblday16 = e.Row.FindControl("lblday16") as Label;
            Label lblday17 = e.Row.FindControl("lblday17") as Label;
            Label lblday18 = e.Row.FindControl("lblday18") as Label;
            Label lblday19 = e.Row.FindControl("lblday19") as Label;
            Label lblday20 = e.Row.FindControl("lblday20") as Label;
            Label lblday21 = e.Row.FindControl("lblday21") as Label;
            Label lblday22 = e.Row.FindControl("lblday22") as Label;
            Label lblday23 = e.Row.FindControl("lblday23") as Label;
            Label lblday24 = e.Row.FindControl("lblday24") as Label;
            Label lblday25 = e.Row.FindControl("lblday25") as Label;
            Label lblday26 = e.Row.FindControl("lblday26") as Label;
            Label lblday27 = e.Row.FindControl("lblday27") as Label;
            Label lblday28 = e.Row.FindControl("lblday28") as Label;
            Label lblday29 = e.Row.FindControl("lblday29") as Label;
            Label lblday30 = e.Row.FindControl("lblday30") as Label;
            Label lblday31 = e.Row.FindControl("lblday31") as Label;
            Label lblday32 = e.Row.FindControl("lblday32") as Label;

            Label lbltotalpresent = e.Row.FindControl("lbltotalpresent") as Label;
            Label lbltotaldays = e.Row.FindControl("lbltotaldays") as Label;

            STEnt = new HSS_STUDENT();
            STEnt.STUDENT_ID = studentid;
            STEnt = (HSS_STUDENT)STSer.GetSingle(STEnt);
            //lblName.Text = STDEnt.NAME_ENGLISH;
            if (lblday1.Text == "") { lblday1.Text = "-"; } else if (lblday1.Text == "A") { lblday1.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday1.Text == "L") { lblday1.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday2.Text == "") { lblday2.Text = "-"; } else if (lblday2.Text == "A") { lblday2.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday2.Text == "L") { lblday2.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday3.Text == "") { lblday3.Text = "-"; } else if (lblday3.Text == "A") { lblday3.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday3.Text == "L") { lblday3.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday4.Text == "") { lblday4.Text = "-"; } else if (lblday4.Text == "A") { lblday4.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday4.Text == "L") { lblday4.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday5.Text == "") { lblday5.Text = "-"; } else if (lblday5.Text == "A") { lblday5.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday5.Text == "L") { lblday5.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday6.Text == "") { lblday6.Text = "-"; } else if (lblday6.Text == "A") { lblday6.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday6.Text == "L") { lblday6.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday7.Text == "") { lblday7.Text = "-"; } else if (lblday7.Text == "A") { lblday7.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday7.Text == "L") { lblday7.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday8.Text == "") { lblday8.Text = "-"; } else if (lblday8.Text == "A") { lblday8.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday8.Text == "L") { lblday8.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday9.Text == "") { lblday9.Text = "-"; } else if (lblday9.Text == "A") { lblday9.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday9.Text == "L") { lblday9.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday10.Text == "") { lblday10.Text = "-"; } else if (lblday10.Text == "A") { lblday10.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday10.Text == "L") { lblday10.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday11.Text == "") { lblday11.Text = "-"; } else if (lblday11.Text == "A") { lblday11.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday11.Text == "L") { lblday11.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday12.Text == "") { lblday12.Text = "-"; } else if (lblday12.Text == "A") { lblday12.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday12.Text == "L") { lblday12.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday13.Text == "") { lblday13.Text = "-"; } else if (lblday13.Text == "A") { lblday13.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday13.Text == "L") { lblday13.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday14.Text == "") { lblday14.Text = "-"; } else if (lblday14.Text == "A") { lblday14.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday14.Text == "L") { lblday14.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday15.Text == "") { lblday15.Text = "-"; } else if (lblday15.Text == "A") { lblday15.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday15.Text == "L") { lblday15.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday16.Text == "") { lblday16.Text = "-"; } else if (lblday16.Text == "A") { lblday16.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday16.Text == "L") { lblday16.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday17.Text == "") { lblday17.Text = "-"; } else if (lblday17.Text == "A") { lblday17.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday17.Text == "L") { lblday17.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday18.Text == "") { lblday18.Text = "-"; } else if (lblday18.Text == "A") { lblday18.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday18.Text == "L") { lblday18.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday19.Text == "") { lblday19.Text = "-"; } else if (lblday19.Text == "A") { lblday19.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday19.Text == "L") { lblday19.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday20.Text == "") { lblday20.Text = "-"; } else if (lblday20.Text == "A") { lblday20.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday20.Text == "L") { lblday20.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday21.Text == "") { lblday21.Text = "-"; } else if (lblday21.Text == "A") { lblday21.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday21.Text == "L") { lblday21.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday22.Text == "") { lblday22.Text = "-"; } else if (lblday22.Text == "A") { lblday22.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday22.Text == "L") { lblday22.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday23.Text == "") { lblday23.Text = "-"; } else if (lblday23.Text == "A") { lblday23.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday23.Text == "L") { lblday23.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday24.Text == "") { lblday24.Text = "-"; } else if (lblday24.Text == "A") { lblday24.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday24.Text == "L") { lblday24.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday25.Text == "") { lblday25.Text = "-"; } else if (lblday25.Text == "A") { lblday25.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday25.Text == "L") { lblday25.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday26.Text == "") { lblday26.Text = "-"; } else if (lblday26.Text == "A") { lblday26.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday26.Text == "L") { lblday26.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday27.Text == "") { lblday27.Text = "-"; } else if (lblday27.Text == "A") { lblday27.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday27.Text == "L") { lblday27.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday28.Text == "") { lblday28.Text = "-"; } else if (lblday28.Text == "A") { lblday28.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday28.Text == "L") { lblday28.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday29.Text == "") { lblday29.Text = "-"; } else if (lblday29.Text == "A") { lblday29.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday29.Text == "L") { lblday29.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday30.Text == "") { lblday30.Text = "-"; } else if (lblday30.Text == "A") { lblday30.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday30.Text == "L") { lblday30.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday31.Text == "") { lblday31.Text = "-"; } else if (lblday31.Text == "A") { lblday31.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday31.Text == "L") { lblday31.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }
            if (lblday32.Text == "") { lblday32.Text = "-"; } else if (lblday32.Text == "A") { lblday32.Text = "<font color='red'>A</font>"; totaldays++; } else if (lblday32.Text == "L") { lblday32.Text = "<font color='red'>P</font>"; totaldays++; totalpresent++; } else { totaldays++; totalpresent++; }

            lbltotalpresent.Text = totalpresent.ToString();
            lbltotaldays.Text = totaldays.ToString();

            totdays = totdays + Convert.ToDouble(lbltotaldays.Text);
            totpresentdays = totpresentdays + Convert.ToDouble(lbltotalpresent.Text);
        }
        #endregion

    }
    protected DataTable GetData(string stdid, string cls)
    {

        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();


        OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "pkj_reports.individual_attendance";


        objCmd.Connection = cn;
        objCmd.CommandType = CommandType.StoredProcedure;



        OracleParameter studentid = new OracleParameter("var_studentid", OracleDbType.Varchar2);
        studentid.Direction = ParameterDirection.Input;
        studentid.Value = stdid;
        objCmd.Parameters.Add(studentid);

        OracleParameter grade = new OracleParameter("var_grade", OracleDbType.Varchar2);
        grade.Direction = ParameterDirection.Input;
        grade.Value = cls;
        objCmd.Parameters.Add(grade);

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        cn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        cn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }

    protected void gridRank_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {

            Label lblRemark = e.Row.FindControl("lblRemark") as Label;
            Label lblRnk = e.Row.FindControl("lblRnk") as Label;

            if (lblRemark.Text == "Fail")
            {
                lblRnk.Text = "-";
            }
        }
    }
  
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSemester();
    }
}