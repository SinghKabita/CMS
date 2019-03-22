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
public partial class exam_internalexam_ResultGenerate : System.Web.UI.Page
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

    EXAM_TYPE ETPEnt = new EXAM_TYPE();
    EXAM_TYPEService ETPSer = new EXAM_TYPEService();

    EXAM_TYPE_MASTER ETMEnt = new EXAM_TYPE_MASTER();
    EXAM_TYPE_MASTERService ETMSer = new EXAM_TYPE_MASTERService();

    HSS_RESULT REnt = new HSS_RESULT();
    HSS_RESULTSService RSer = new HSS_RESULTSService();

    HSS_GRADE GEnt = new HSS_GRADE();
    HSS_GRADEService GSer = new HSS_GRADEService();

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

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

    HSS_LEVEL LVLEnt = new HSS_LEVEL();
    HSS_LEVELService LVLSer = new HSS_LEVELService();

    double totdays = 0.0;
    double totpresentdays = 0.0;
    bool light = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadLevel();
            LoadFaculty();
            LoadProgram();
            //LoadExamType();
            //LoadBatch();
            btnSave.Visible = false;
        }
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
    protected void LoadFaculty()
    {
        FCEnt = new hss_faculty();
        ddlFaculty.DataSource = FCSer.GetAll(FCEnt);
        ddlFaculty.DataTextField = "FACULTY";
        ddlFaculty.DataValueField = "PK_ID";
        ddlFaculty.DataBind();
        ddlFaculty.Items.Insert(0, "Select");
        ddlProgram.Items.Insert(0, "Select");

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
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }
    protected void LoadExamType()
    {
        ddlExamType.DataSource = hf.getExamType(ddlProgram.SelectedValue);
        ddlExamType.DataTextField = "EXAM_TYPE";
        ddlExamType.DataValueField = "PKID";
        ddlExamType.DataBind();

    }

    protected void LoadBatch()
    {
        BTEnt = new BatchYear();
        BTEnt.SEMESTER = ddlSemester.SelectedValue;
        BTEnt.ACTIVE = "1";
        BTEnt.SYLLABUS_YEAR = hf.getSyllabusYear(ddlProgram.SelectedValue, hf.NepaliYear());
        ddlBatch.DataSource = BTSer.GetAll(BTEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
    }

    protected void LoadSemester()
    {
        EntityList activeList = new EntityList();
        EntityList semList = new EntityList();
        BTEnt = new BatchYear();
        BTEnt.ACTIVE = "1";
        BTEnt.PROGRAM = ddlProgram.SelectedValue;
        BTEnt.SYLLABUS_YEAR = hf.getSyllabusYear(ddlProgram.SelectedValue, hf.NepaliYear());
        activeList = BTSer.GetAll(BTEnt);
        if (activeList.Count > 0)
        {
            foreach (BatchYear by in activeList)
            {
                SMEnt = new semester();
                SMEnt.PK_ID = by.SEMESTER;
                SMEnt = (semester)SMSer.GetSingle(SMEnt);
                if (SMEnt != null)
                    semList.Add(SMEnt);
            }
            ddlSemester.DataSource = semList;
            ddlSemester.DataTextField = "SEMESTER_CODE";
            ddlSemester.DataValueField = "PK_ID";
            ddlSemester.DataBind();
            ddlSemester.Items.Insert(0, "Select");
        }
        else
        {
            HelperFunction.MsgBox(this, this.GetType(), "Semester not Added to this Program");
        }

    }

    protected void Clear()
    {
        ddlFaculty.SelectedIndex = 0;
        ddlProgram.SelectedIndex = 0;
        ddlBatch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlExamType.SelectedIndex = 0;
    }

    protected void btnShow_Click(object sender, EventArgs e)
    {
        LoadData();
        insertRankinTemp();
        LoadSecondGrid();
        updateTemp();
        LoadDetailData();
        btnSave.Visible = true;
    }

    private void LoadSecondGrid()
    {
        string exmTypID = "";
        ETEnt = new EXAM_TYPE();
        ETEnt.EXAM_TYPE_MASTERID = ddlExamType.SelectedValue;
        ETEnt.PROGRAM = ddlProgram.SelectedValue;
        ETEnt.STATUS = "1";
        ETEnt = (EXAM_TYPE)ETSer.GetSingle(ETEnt);
        if (ETEnt != null)
        {
            exmTypID = ETEnt.PKID;
        }
        gridTemp.DataSource = getGroupedTotal(ddlSemester.SelectedValue, ddlExamType.SelectedValue);
        gridTemp.DataBind();
    }

    private void LoadData()
    {
        //string exmTypID = "";
        //ETEnt = new EXAM_TYPE();
        //ETEnt.EXAM_TYPE_MASTERID = ddlExamType.SelectedValue;
        //ETEnt.PROGRAM = ddlProgram.SelectedValue;
        //ETEnt.STATUS = "1";
        //ETEnt = (EXAM_TYPE)ETSer.GetSingle(ETEnt);
        //if (ETEnt != null)
        //{
        //    exmTypID = ETEnt.PKID;
        //}
        gridResult.DataSource = getResult(ddlSemester.SelectedValue, hf.NepaliYear(), ddlExamType.SelectedValue);
        gridResult.DataBind();
    }

    private void LoadDetailData()
    {
        string exmTypID = "";
        ETEnt = new EXAM_TYPE();
        ETEnt.EXAM_TYPE_MASTERID = ddlExamType.SelectedValue;
        ETEnt.PROGRAM = ddlProgram.SelectedValue;
        ETEnt.STATUS = "1";
        ETEnt = (EXAM_TYPE)ETSer.GetSingle(ETEnt);
        if (ETEnt != null)
        {
            exmTypID = ETEnt.PKID;
        }
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

    private IDbDataParameter[] CreateParmans(string semester, string year, string examtype)
    {

        List<IDbDataParameter> cmdParams = new List<IDbDataParameter>();
        //input Parameters start
        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_semester", semester));
        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_year", year));
        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_examtype", examtype));


        //input Parameters ends
        cmdParams.Add(DataAccessFactory.CreateDataParameter("Result", ""));

        return cmdParams.ToArray();
    }

    public DataTable getResult(string semester, string year, string examtype)
    {
        GenericGetValueService srvGen = new GenericGetValueService();
        DataTable DT = new DataTable();
        try
        {
            DT = srvGen.ReaderToTable("PKJ_REPORTS.student_result", System.Data.CommandType.StoredProcedure, CreateParmans(semester, year, examtype));
        }
        catch (Exception e)
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


    protected void gridResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblSemesterName = e.Row.FindControl("lblSemesterName") as Label;
            Label lblSemester = e.Row.FindControl("lblSemester") as Label;
            Label lblExamType = e.Row.FindControl("lblExamType") as Label;
            Label lblExamTypeName = e.Row.FindControl("lblExamTypeName") as Label;

            SMEnt = new semester();
            SMEnt.PK_ID = ddlSemester.SelectedValue;
            SMEnt = (semester)SMSer.GetSingle(SMEnt);
            if (SMEnt != null)
                lblSemesterName.Text = SMEnt.SEMESTER_CODE;

            ETEnt = new EXAM_TYPE();
            ETEnt.PKID = ddlExamType.SelectedValue;
            ETEnt = (EXAM_TYPE)ETSer.GetSingle(ETEnt);
            if (ETEnt != null)
            {
                ETMEnt = new EXAM_TYPE_MASTER();
                ETMEnt.PKID = ETEnt.EXAM_TYPE_MASTERID;
                ETMEnt = (EXAM_TYPE_MASTER)ETMSer.GetSingle(ETMEnt);
                if (ETMEnt != null)
                {
                    lblExamTypeName.Text = ETMEnt.EXAM_TYPE;
                }
                lblExamType.Text = ETEnt.PKID;
            }



            lblSemester.Text = ddlSemester.SelectedValue;

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
            bool flag = false;

            EXMEnt = new EXAM_MARKS();
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
                //lblGrade.Text = getGrade(lblPercentage.Text);
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
                Label lblSemester = row.FindControl("lblSemester") as Label;
                Label lblExamType = row.FindControl("lblExtp") as Label;
                Label lblTotal = row.FindControl("lblTTL") as Label;
                Label lblPercentage = row.FindControl("lblPercent") as Label;
                Label lblRemarks = row.FindControl("lblRemark") as Label;
                Label lblGrade = row.FindControl("lblGrd") as Label;
                Label lblRank = row.FindControl("lblRnk") as Label;

                REnt.STUDENTID = lblStudentId.Text;
                REnt.CLASS = lblSemester.Text;
                REnt.EXAMTYPE = lblExamType.Text;
                REnt.TOTAL = lblTotal.Text;
                REnt.PERCENTAGE = lblPercentage.Text;
                REnt.REMARKS = lblRemarks.Text;
                REnt.GRADE = lblGrade.Text;
                REnt.RANK = lblRank.Text;
                // REnt.NDATE = txtDate.Text;
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
                Label lblSemester = row.FindControl("lblSemester") as Label;
                Label lblExamType = row.FindControl("lblExamType") as Label;

                Label lblTot = row.FindControl("lblTot") as Label;
                Label lblPercentage = row.FindControl("lblPercentage") as Label;
                Label lblRemarks = row.FindControl("lblRemarks") as Label;
                Label lblGrade = row.FindControl("lblGrade") as Label;
                Label lblRank = row.FindControl("lblRank") as Label;


                TEnt.STUDENTID = lblStudentId.Text;
                TEnt.CLASS = lblSemester.Text;
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

    private IDbDataParameter[] CreateParmans(string semester, string examtype)
    {

        List<IDbDataParameter> cmdParams = new List<IDbDataParameter>();
        //input Parameters start
        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_semester", semester));

        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_examtype", examtype));


        //input Parameters ends
        cmdParams.Add(DataAccessFactory.CreateDataParameter("Result", ""));

        return cmdParams.ToArray();
    }

    public DataTable getRankofStudent(string semester, string examtype)
    {
        GenericGetValueService srvGen = new GenericGetValueService();
        DataTable DT = new DataTable();
        try
        {
            DT = srvGen.ReaderToTable("PKJ_REPORTS.student_rank", System.Data.CommandType.StoredProcedure, CreateParmans(semester, examtype));
        }
        catch
        {
        }
        return DT;
    }

    private IDbDataParameter[] CreateParmans2(string semester, string examtype)
    {

        List<IDbDataParameter> cmdParams = new List<IDbDataParameter>();
        //input Parameters start
        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_semester", semester));

        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_examtype", examtype));


        //input Parameters ends
        cmdParams.Add(DataAccessFactory.CreateDataParameter("Result", ""));

        return cmdParams.ToArray();
    }

    public DataTable getGroupedTotal(string semester, string examtype)
    {
        GenericGetValueService srvGen = new GenericGetValueService();
        DataTable DT = new DataTable();
        try
        {
            DT = srvGen.ReaderToTable("PKJ_REPORTS.student_total_intemp", System.Data.CommandType.StoredProcedure, CreateParmans2(semester, examtype));
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
        REnt.EXAMTYPE = ddlExamType.SelectedValue;
        theList = RSer.GetAll(REnt);
        foreach (HSS_RESULT rs in theList)
        {
            totdays = 0;
            totpresentdays = 0;
            studentid = rs.STUDENTID;
            LoadGridAttendance(rs.STUDENTID, rs.CLASS, "");

            UpdateAttendanceInResult(studentid, rs.EXAMTYPE, ddlSemester.SelectedValue);
        }
    }

    protected void LoadGridAttendance(string stdid, string semester, string subject)
    {

        gridAttendance.DataSource = hf.getTotalAttendance(stdid, semester, subject);
        gridAttendance.DataBind();
    }

    protected void UpdateAttendanceInResult(string stdid, string extype, string semester)
    {
        REnt = new HSS_RESULT();
        REnt.STUDENTID = stdid;
        REnt.EXAMTYPE = extype;
        REnt.CLASS = semester;
        REnt.SESSION_YEAR = hf.CurrentYear(hf.NepaliMonth(), hf.NepaliYear());
        REnt = (HSS_RESULT)RSer.GetSingle(REnt);
        if (REnt != null)
        {
            REnt.ATTENDANCE = totpresentdays + "/" + totdays;
            RSer.Update(REnt);
        }

    }


    protected void gridAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPresentDays = e.Row.FindControl("lblPresentDays") as Label;
            Label lblAbsDays = e.Row.FindControl("lblAbsDays") as Label;
            Label lbltotaldays = e.Row.FindControl("lbltotaldays") as Label;

            totdays = Convert.ToDouble(lblPresentDays.Text) + Convert.ToDouble(lblAbsDays.Text);
            totpresentdays = Convert.ToDouble(lblPresentDays.Text);
            lbltotaldays.Text = totdays.ToString();
        }
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

        OracleParameter semester = new OracleParameter("var_semester", OracleDbType.Varchar2);
        semester.Direction = ParameterDirection.Input;
        semester.Value = cls;
        objCmd.Parameters.Add(semester);

        OracleParameter subject = new OracleParameter("var_subject", OracleDbType.Varchar2);
        subject.Direction = ParameterDirection.Input;
        subject.Value = "1";
        objCmd.Parameters.Add(subject);

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
            gridRank.Columns[7].Visible = false;

            Label lblRemark = e.Row.FindControl("lblRemark") as Label;
            Label lblSemester = e.Row.FindControl("lblSemester") as Label;
            Label lblSemesterName = e.Row.FindControl("lblSemesterName") as Label;
            Label lblRnk = e.Row.FindControl("lblRnk") as Label;
            Label lblExtp = e.Row.FindControl("lblExtp") as Label;
            Label lblExamType = e.Row.FindControl("lblExamType") as Label;
            Label lblTTL = e.Row.FindControl("lblTTL") as Label;

            double tot = Convert.ToDouble(lblTTL.Text);
            lblTTL.Text = tot.ToString("0.00");

            if (lblRemark.Text == "Fail")
            {
                lblRnk.Text = "-";
            }

            SMEnt = new semester();
            SMEnt.PK_ID = lblSemester.Text;
            SMEnt = (semester)SMSer.GetSingle(SMEnt);
            if (SMEnt != null)
                lblSemesterName.Text = SMEnt.SEMESTER_CODE;

            ETEnt = new EXAM_TYPE();
            ETEnt.PKID = lblExtp.Text;
            ETEnt = (EXAM_TYPE)ETSer.GetSingle(ETEnt);
            if (ETEnt != null)
            {
                ETMEnt = new EXAM_TYPE_MASTER();
                ETMEnt.PKID = ETEnt.EXAM_TYPE_MASTERID;
                ETMEnt = (EXAM_TYPE_MASTER)ETMSer.GetSingle(ETMEnt);
                if (ETMEnt != null)
                    lblExamType.Text = ETMEnt.EXAM_TYPE;
            }
            

        }
    }



    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {

        LoadSemester();
        //LoadExamType();
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBatch();
        LoadExamType();
    }

}