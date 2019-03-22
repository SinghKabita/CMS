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


public partial class uc_result_markslist : System.Web.UI.Page
{
    HSS_NAME NEnt = new HSS_NAME();
    HSS_NAMEService NSer = new HSS_NAMEService();

    HSS_LEVEL LVLEnt = new HSS_LEVEL();
    HSS_LEVELService LVLSer = new HSS_LEVELService();

    EXAM_MARKS EMEnt = new EXAM_MARKS();
    EXAM_MARKSService EMSer = new EXAM_MARKSService();

    EXAM_TYPE ETEnt = new EXAM_TYPE();
    EXAM_TYPEService ETSer = new EXAM_TYPEService();

    EXAM_TYPE_MASTER ETMEnt = new EXAM_TYPE_MASTER();
    EXAM_TYPE_MASTERService ETMSrv = new EXAM_TYPE_MASTERService();

    HSS_SUBJECT SEnt = new HSS_SUBJECT();
    HSS_SUBJECTService SSer = new HSS_SUBJECTService();

    Section SecEnt = new Section();
    SectionService SecSer = new SectionService();

    hss_faculty FCTEnt = new hss_faculty();
    hss_facultyService FCTSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    full_pass_marks FPMEnt = new full_pass_marks();
    full_pass_marksService FPMSer = new full_pass_marksService();

    BatchYear BTEnt = new BatchYear();
    BatchYearService BTSer = new BatchYearService();

    HSS_CURRENT_STUDENT CSEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSer = new HSS_CURRENT_STUDENTService();

    HelperFunction hf = new HelperFunction();

    static string subj1 = "";
    static string subj2 = "";
    static string subj3 = "";
    static string subj4 = "";
    static string subj5 = "";
    static string subj6 = "";
    static string subj7 = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFaculty();
            loadLevel();
            LoadBatch();
            btnPrint.Visible = false;
        }
    }

    protected void LoadFaculty()
    {
        FCTEnt = new hss_faculty();
        ddlFaculty.DataSource = FCTSer.GetAll(FCTEnt);
        ddlFaculty.DataTextField = "FACULTY";
        ddlFaculty.DataValueField = "PK_ID";
        ddlFaculty.DataBind();
        ddlFaculty.Items.Insert(0, "Select");
        ddlProgram.Items.Insert(0, "Select");

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
        PEnt.FACULTY_ID = ddlFaculty.SelectedValue;
        PEnt.PROGRAM_LEVEL = ddlLevel.SelectedValue;
        ddlProgram.DataSource = PSer.GetAll(PEnt);
        ddlProgram.DataTextField = "PROGRAM_CODE";
        ddlProgram.DataValueField = "PK_ID";
        ddlProgram.DataBind();
        ddlProgram.Items.Insert(0, "Select");
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
        BTEnt.ACTIVE = "1";
        BTEnt.SEMESTER = ddlSemester.SelectedValue;
        BTEnt.PROGRAM = ddlProgram.SelectedValue;
        ddlBatch.DataSource = BTSer.GetAll(BTEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
    }

    protected void LoadSemester()
    {
        EntityList theList = new EntityList();
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

    protected void LoadSection()
    {
        SecEnt = new Section();
        ddlSection.DataSource = SecSer.GetAll(SecEnt);
        ddlSection.DataTextField = "SECTION";
        ddlSection.DataValueField = "SECTION";
        ddlSection.DataBind();
        ddlSection.Items.Insert(0, "Select");
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBatch();
        LoadExamType();
        LoadSection();
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        if (ddlBatch.SelectedValue != "Select" && ddlSemester.SelectedValue != "Select" && ddlExamType.SelectedValue != "Select")
        {
            LoadInfo();

            LoadGrid();
            btnPrint.Visible = true;
            LoadGrid();
        }
    }
    protected void LoadGrid()
    {

        gridMarksList.DataSource = getMarkslist(ddlBatch.SelectedValue, ddlSemester.SelectedValue, ddlExamType.SelectedValue, ddlSection.SelectedValue);
        gridMarksList.DataBind();
    }

    protected void LoadInfo()
    {
        string semester = "";
        string sem = "";
        string bt;
        string[] batch;
        NEnt = new HSS_NAME();
        NEnt = (HSS_NAME)NSer.GetSingle(NEnt);

        if (NEnt != null)
        {
            // string[] name = NEnt.NAME.Split(',');
            lblCollegeName.Text = NEnt.NAME;
            // lblCollege.Text = name[1];
        }
        //if (ddlSemester.SelectedItem.ToString() == "First")
        //{
        //    sem = "st";
        //    semester = "1";

        //}
        //else if (ddlSemester.SelectedItem.ToString() == "Second")
        //{
        //    sem = "nd";
        //    semester = "2";
        //}
        //else if (ddlSemester.SelectedItem.ToString() == "Third")
        //{
        //    sem = "rd";
        //    semester = "3";
        //}
        //else if (ddlSemester.SelectedItem.ToString() == "Fouth")
        //{
        //    sem = "th";
        //    semester = "4";
        //}
        //else if (ddlSemester.SelectedItem.ToString() == "Fifth")
        //{
        //    sem = "th";
        //    semester = "5";
        //}
        //else if (ddlSemester.SelectedItem.ToString() == "Sixth")
        //{
        //    sem = "th";
        //    semester = "6";
        //}
        //else if (ddlSemester.SelectedItem.ToString() == "Seventh")
        //{
        //    sem = "th";
        //    semester = "7";
        //}
        //else if (ddlSemester.SelectedItem.ToString() == "Eight")
        //{
        //    sem = "th";
        //    semester = "8";
        //}


        lblProgram.Text = ddlProgram.SelectedItem + " " + ddlSemester.SelectedItem.ToString() + semester + sem + " Semester (" + ddlBatch.SelectedValue + " Batch)";
        lblSection.Text = "Section " + "'" + ddlSection.SelectedItem + "'";
        //if (Convert.ToDouble(ddlBatch.SelectedValue) < 15)
        //{
        //    lblProgram.Text = "BFD " + semester + sem + " Semester (" + ddlBatch.SelectedValue + " Batch)";
        //}
        //else
        //{
        //  lblProgram.Text = "BFDM " + semester + sem + " Semester (" + ddlBatch.SelectedValue + " Batch)";
        //}

    }

    private IDbDataParameter[] CreateParmans(string batch, string semester, string examtype, string section)
    {

        List<IDbDataParameter> cmdParams = new List<IDbDataParameter>();
        //input Parameters start
        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_batch", batch));
        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_semester", semester));
        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_examtype", examtype));
        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_section", section));


        //input Parameters ends
        cmdParams.Add(DataAccessFactory.CreateDataParameter("Result", ""));

        return cmdParams.ToArray();
    }

    public DataTable getMarkslist(string batch, string semester, string examtype, string section)
    {
        GenericGetValueService srvGen = new GenericGetValueService();
        DataTable DT = new DataTable();
        try
        {
            DT = srvGen.ReaderToTable("PKJ_REPORTS.getmarkslist", System.Data.CommandType.StoredProcedure, CreateParmans(batch, semester, examtype, section));
        }
        catch
        {
        }
        return DT;
    }
    protected void gridMarksList_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        #region to define the subject id for header field from datarow
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSub1 = e.Row.FindControl("lblSub1") as Label;
            Label lblSub2 = e.Row.FindControl("lblSub2") as Label;
            Label lblSub3 = e.Row.FindControl("lblSub3") as Label;
            Label lblSub4 = e.Row.FindControl("lblSub4") as Label;
            Label lblSub5 = e.Row.FindControl("lblSub5") as Label;
            Label lblSub6 = e.Row.FindControl("lblSub6") as Label;
            Label lblSub7 = e.Row.FindControl("lblSub7") as Label;

            subj1 = lblSub1.Text;
            subj2 = lblSub2.Text;
            subj3 = lblSub3.Text;
            subj4 = lblSub4.Text;
            subj5 = lblSub5.Text;
            subj6 = lblSub6.Text;
            subj7 = lblSub7.Text;

        }
        #endregion

        #region for header field
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Label lblSection = e.Row.FindControl("lblSection") as Label;
            Label lblExamType = e.Row.FindControl("lblExamType") as Label;

            lblExamType.Text = ddlExamType.SelectedItem.ToString() + " Result";
            // lblSection.Text = ddlSection.SelectedValue;

            #region for subject 7
            if (subj7 != "")
            {

                Label lblSubj7Name = e.Row.FindControl("lblSubj7Name") as Label;
                Label lblFMS7 = e.Row.FindControl("lblFMS7") as Label;
                Label lblPMS7 = e.Row.FindControl("lblPMS7") as Label;

                SEnt = new HSS_SUBJECT();
                SEnt.PK_ID = subj7;
                SEnt.STATUS = "1";
                SEnt = (HSS_SUBJECT)SSer.GetSingle(SEnt);
                if (SEnt != null)
                {
                    lblSubj7Name.Text = SEnt.SUBJECT_NAME;
                    FPMEnt = new full_pass_marks();
                    FPMEnt.SUBJECT_ID = subj7;

                    FPMEnt.EXAM_TYPE = ddlExamType.SelectedValue;

                    FPMEnt = (full_pass_marks)FPMSer.GetSingle(FPMEnt);
                    if (FPMEnt != null)
                    {
                        //if (SEnt.REMARKS == "T")
                        //{
                        lblFMS7.Text = FPMEnt.FULLMARKS_THRCL;
                        lblPMS7.Text = FPMEnt.PASSMARKS_THRCL;
                        //}
                        //else if (SEnt.REMARKS == "P")
                        //{
                        //    lblFMS7.Text = FPMEnt.FULLMARKS_PRCL;
                        //    lblPMS7.Text = FPMEnt.PASSMARKS_PRCL;

                        //}
                    }
                }
            }
            else
            {
                HtmlTableCell subj7th = (HtmlTableCell)e.Row.FindControl("subj7th");
                subj7th.Visible = false;
                HtmlTableCell subj7fmth = (HtmlTableCell)e.Row.FindControl("subj7fmth");
                subj7fmth.Visible = false;
                HtmlTableCell subjects = (HtmlTableCell)e.Row.FindControl("subjects");
                subjects.ColSpan = 6;
                HtmlTableCell groups = (HtmlTableCell)e.Row.FindControl("groups");
                groups.ColSpan = 13;
            }

            #endregion

            #region for subject 6
            if (subj6 != "")
            {


                Label lblSubj6Name = e.Row.FindControl("lblSubj6Name") as Label;
                Label lblFMS6 = e.Row.FindControl("lblFMS6") as Label;
                Label lblPMS6 = e.Row.FindControl("lblPMS6") as Label;

                SEnt = new HSS_SUBJECT();
                SEnt.PK_ID = subj6;
                SEnt.STATUS = "1";
                SEnt = (HSS_SUBJECT)SSer.GetSingle(SEnt);
                if (SEnt != null)
                {
                    lblSubj6Name.Text = SEnt.SUBJECT_NAME;
                    FPMEnt = new full_pass_marks();
                    FPMEnt.SUBJECT_ID = subj6;

                    FPMEnt.EXAM_TYPE = ddlExamType.SelectedValue;
                    FPMEnt = (full_pass_marks)FPMSer.GetSingle(FPMEnt);
                    if (FPMEnt != null)
                    {
                        lblFMS6.Text = FPMEnt.FULLMARKS_THRCL;
                        lblPMS6.Text = FPMEnt.PASSMARKS_THRCL;

                    }
                }
            }
            else
            {
                HtmlTableCell subj6th = (HtmlTableCell)e.Row.FindControl("subj6th");
                subj6th.Visible = false;
                HtmlTableCell subj6fmth = (HtmlTableCell)e.Row.FindControl("subj6fmth");
                subj6fmth.Visible = false;
                HtmlTableCell subjects = (HtmlTableCell)e.Row.FindControl("subjects");
                subjects.ColSpan = 5;
                HtmlTableCell groups = (HtmlTableCell)e.Row.FindControl("groups");
                groups.ColSpan = 12;
            }


            #endregion

            #region for subject 5
            if (subj5 != "")
            {
                Label lblSubj5Name = e.Row.FindControl("lblSubj5Name") as Label;
                Label lblFMS5 = e.Row.FindControl("lblFMS5") as Label;
                Label lblPMS5 = e.Row.FindControl("lblPMS5") as Label;

                SEnt = new HSS_SUBJECT();
                SEnt.PK_ID = subj5;
                SEnt.STATUS = "1";
                SEnt = (HSS_SUBJECT)SSer.GetSingle(SEnt);
                if (SEnt != null)
                {
                    lblSubj5Name.Text = SEnt.SUBJECT_NAME;
                    FPMEnt = new full_pass_marks();
                    FPMEnt.SUBJECT_ID = subj5;

                    FPMEnt.EXAM_TYPE = ddlExamType.SelectedValue;

                    FPMEnt = (full_pass_marks)FPMSer.GetSingle(FPMEnt);
                    if (FPMEnt != null)
                    {
                        lblFMS5.Text = FPMEnt.FULLMARKS_THRCL;
                        lblPMS5.Text = FPMEnt.PASSMARKS_THRCL;

                    }
                }
            }
            else
            {
                HtmlTableCell subj5th = (HtmlTableCell)e.Row.FindControl("subj5th");
                subj5th.Visible = false;
                HtmlTableCell subj5fmth = (HtmlTableCell)e.Row.FindControl("subj5fmth");
                subj5fmth.Visible = false;
                HtmlTableCell subjects = (HtmlTableCell)e.Row.FindControl("subjects");
                subjects.ColSpan = 4;
                HtmlTableCell groups = (HtmlTableCell)e.Row.FindControl("groups");
                groups.ColSpan = 11;
            }

            #endregion

            #region for subject 4
            if (subj4 != "")
            {


                Label lblSubj4Name = e.Row.FindControl("lblSubj4Name") as Label;
                Label lblFMS4 = e.Row.FindControl("lblFMS4") as Label;
                Label lblPMS4 = e.Row.FindControl("lblPMS4") as Label;

                SEnt = new HSS_SUBJECT();
                SEnt.PK_ID = subj4;
                SEnt.STATUS = "1";
                SEnt = (HSS_SUBJECT)SSer.GetSingle(SEnt);
                if (SEnt != null)
                {
                    lblSubj4Name.Text = SEnt.SUBJECT_NAME;
                    FPMEnt = new full_pass_marks();
                    FPMEnt.SUBJECT_ID = subj4;

                    FPMEnt.EXAM_TYPE = ddlExamType.SelectedValue;

                    FPMEnt = (full_pass_marks)FPMSer.GetSingle(FPMEnt);
                    if (FPMEnt != null)
                    {

                        lblFMS4.Text = FPMEnt.FULLMARKS_THRCL;
                        lblPMS4.Text = FPMEnt.PASSMARKS_THRCL;


                    }
                }
            }
            else
            {
                HtmlTableCell subj4th = (HtmlTableCell)e.Row.FindControl("subj4th");
                subj4th.Visible = false;
                HtmlTableCell subj4fmth = (HtmlTableCell)e.Row.FindControl("subj4fmth");
                subj4fmth.Visible = false;
                HtmlTableCell subjects = (HtmlTableCell)e.Row.FindControl("subjects");
                subjects.ColSpan = 3;
                HtmlTableCell groups = (HtmlTableCell)e.Row.FindControl("groups");
                groups.ColSpan = 10;
            }

            #endregion

            #region for subject 3
            if (subj3 != "")
            {


                Label lblSubj3Name = e.Row.FindControl("lblSubj3Name") as Label;
                Label lblFMS3 = e.Row.FindControl("lblFMS3") as Label;
                Label lblPMS3 = e.Row.FindControl("lblPMS3") as Label;

                SEnt = new HSS_SUBJECT();
                SEnt.PK_ID = subj3;
                SEnt.STATUS = "1";
                SEnt = (HSS_SUBJECT)SSer.GetSingle(SEnt);
                if (SEnt != null)
                {
                    lblSubj3Name.Text = SEnt.SUBJECT_NAME;
                    FPMEnt = new full_pass_marks();
                    FPMEnt.SUBJECT_ID = subj3;

                    FPMEnt.EXAM_TYPE = ddlExamType.SelectedValue;

                    FPMEnt = (full_pass_marks)FPMSer.GetSingle(FPMEnt);
                    if (FPMEnt != null)
                    {

                        lblFMS3.Text = FPMEnt.FULLMARKS_THRCL;
                        lblPMS3.Text = FPMEnt.PASSMARKS_THRCL;

                    }
                }
            }
            else
            {
                HtmlTableCell subj3th = (HtmlTableCell)e.Row.FindControl("subj3th");
                subj3th.Visible = false;
                HtmlTableCell subj3fmth = (HtmlTableCell)e.Row.FindControl("subj3fmth");
                subj3fmth.Visible = false;
                HtmlTableCell subjects = (HtmlTableCell)e.Row.FindControl("subjects");
                subjects.ColSpan = 2;
                HtmlTableCell groups = (HtmlTableCell)e.Row.FindControl("groups");
                groups.ColSpan = 9;
            }

            #endregion

            #region for subject 2
            if (subj2 != "")
            {


                Label lblSubj2Name = e.Row.FindControl("lblSubj2Name") as Label;
                Label lblFMS2 = e.Row.FindControl("lblFMS2") as Label;
                Label lblPMS2 = e.Row.FindControl("lblPMS2") as Label;

                SEnt = new HSS_SUBJECT();
                SEnt.PK_ID = subj2;
                SEnt.STATUS = "1";
                SEnt = (HSS_SUBJECT)SSer.GetSingle(SEnt);
                if (SEnt != null)
                {
                    lblSubj2Name.Text = SEnt.SUBJECT_NAME;
                    FPMEnt = new full_pass_marks();
                    FPMEnt.SUBJECT_ID = subj2;

                    FPMEnt.EXAM_TYPE = ddlExamType.SelectedValue;

                    FPMEnt = (full_pass_marks)FPMSer.GetSingle(FPMEnt);
                    if (FPMEnt != null)
                    {
                        lblFMS2.Text = FPMEnt.FULLMARKS_THRCL;
                        lblPMS2.Text = FPMEnt.PASSMARKS_THRCL;
                    }
                }
            }
            else
            {
                HtmlTableCell subj2th = (HtmlTableCell)e.Row.FindControl("subj2th");
                subj2th.Visible = false;
                HtmlTableCell subj2fmth = (HtmlTableCell)e.Row.FindControl("subj2fmth");
                subj2fmth.Visible = false;
                HtmlTableCell subjects = (HtmlTableCell)e.Row.FindControl("subjects");
                subjects.ColSpan = 1;
                HtmlTableCell groups = (HtmlTableCell)e.Row.FindControl("groups");
                groups.ColSpan = 8;
            }
            #endregion

            #region for subject 1

            if (subj1 != "")
            {

                Label lblSubj1Name = e.Row.FindControl("lblSubj1Name") as Label;
                Label lblFMS1 = e.Row.FindControl("lblFMS1") as Label;
                Label lblPMS1 = e.Row.FindControl("lblPMS1") as Label;
                SEnt = new HSS_SUBJECT();
                SEnt.PK_ID = subj1;
                SEnt.STATUS = "1";
                SEnt = (HSS_SUBJECT)SSer.GetSingle(SEnt);
                if (SEnt != null)
                {
                    lblSubj1Name.Text = SEnt.SUBJECT_NAME;
                    FPMEnt = new full_pass_marks();
                    FPMEnt.SUBJECT_ID = subj1;

                    FPMEnt.EXAM_TYPE = ddlExamType.SelectedValue;

                    FPMEnt = (full_pass_marks)FPMSer.GetSingle(FPMEnt);
                    if (FPMEnt != null)
                    {

                        lblFMS1.Text = FPMEnt.FULLMARKS_THRCL;
                        lblPMS1.Text = FPMEnt.PASSMARKS_THRCL;

                    }
                }
            }
            else
            {
                HtmlTableCell subj1th = (HtmlTableCell)e.Row.FindControl("subj1th");
                subj1th.Visible = false;
                HtmlTableCell subj1fmth = (HtmlTableCell)e.Row.FindControl("subj1fmth");
                subj1fmth.Visible = false;
                HtmlTableCell subjects = (HtmlTableCell)e.Row.FindControl("subjects");
                subjects.ColSpan = 0;
                HtmlTableCell groups = (HtmlTableCell)e.Row.FindControl("groups");
                groups.ColSpan = 7;
            }

            #endregion

        }
        #endregion

        #region to display the marks obtained by student
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStudentId = e.Row.FindControl("lblStudentId") as Label;

            #region for marks of subject 1
            Label lblSub1 = e.Row.FindControl("lblSub1") as Label;
            Label lblSub1Marks = e.Row.FindControl("lblSub1Marks") as Label;
            if (lblSub1.Text != "")
            {
                EMEnt = new EXAM_MARKS();

                EMEnt.EXAM_TYPE = ddlExamType.SelectedValue;
                EMEnt.STUDENT_ID = lblStudentId.Text;
                EMEnt.SEMESTER = ddlSemester.Text;
                EMEnt.SUBJECT = lblSub1.Text;
                EMEnt = (EXAM_MARKS)EMSer.GetSingle(EMEnt);
                if (EMEnt != null)
                {
                    if (EMEnt.REMARKS == "A")
                    {
                        lblSub1Marks.Text = "Abs";
                    }
                    else if (EMEnt.REMARKS == "E")
                    {
                        lblSub1Marks.Text = "Exp";
                    }
                    else
                    {
                        lblSub1Marks.Text = EMEnt.MARKS;
                    }
                    
                    //if (EMEnt.MAKEUP != "")
                    //{
                    //    lblSub1Marks.Text = EMEnt.MAKEUP;
                    //}
                    //else if (EMEnt.MAKEUP == "")
                    //{
                    //    lblSub1Marks.Text = EMEnt.MARKS;
                    //}
                }
            }
            else
            {

                HtmlTableCell subj1td = (HtmlTableCell)e.Row.FindControl("subj1td");
                subj1td.Visible = false;

            }
            #endregion

            #region for marks of subject 2
            Label lblSub2 = e.Row.FindControl("lblSub2") as Label;
            Label lblSub2Marks = e.Row.FindControl("lblSub2Marks") as Label;
            if (lblSub2.Text != "")
            {
                EMEnt = new EXAM_MARKS();

                EMEnt.EXAM_TYPE = ddlExamType.SelectedValue;
                EMEnt.STUDENT_ID = lblStudentId.Text;
                EMEnt.SEMESTER = ddlSemester.Text;
                EMEnt.SUBJECT = lblSub2.Text;
                EMEnt = (EXAM_MARKS)EMSer.GetSingle(EMEnt);
                if (EMEnt != null)
                {
                    if (EMEnt.REMARKS == "A")
                        lblSub2Marks.Text = "Abs";
                    else if (EMEnt.REMARKS == "E")
                        lblSub2Marks.Text = "Exp";
                    else
                        lblSub2Marks.Text = EMEnt.MARKS;
                    //if (EMEnt.MAKEUP != "")
                    //{
                    //    lblSub2Marks.Text = EMEnt.MAKEUP;
                    //}
                    //else if (EMEnt.MAKEUP == "")
                    //{
                    //    lblSub2Marks.Text = EMEnt.MARKS;
                    //}
                }
            }
            else
            {

                HtmlTableCell subj2td = (HtmlTableCell)e.Row.FindControl("subj2td");
                subj2td.Visible = false;

            }
            #endregion

            #region for marks of subject 3
            Label lblSub3 = e.Row.FindControl("lblSub3") as Label;
            Label lblSub3Marks = e.Row.FindControl("lblSub3Marks") as Label;
            if (lblSub3.Text != "")
            {
                EMEnt = new EXAM_MARKS();

                EMEnt.EXAM_TYPE = ddlExamType.SelectedValue;
                EMEnt.STUDENT_ID = lblStudentId.Text;
                EMEnt.SEMESTER = ddlSemester.Text;
                EMEnt.SUBJECT = lblSub3.Text;
                EMEnt = (EXAM_MARKS)EMSer.GetSingle(EMEnt);
                if (EMEnt != null)
                {
                    if (EMEnt.REMARKS == "A")
                        lblSub3Marks.Text = "Abs";
                    else if (EMEnt.REMARKS == "E")
                        lblSub3Marks.Text = "Exp";
                    else
                        lblSub3Marks.Text = EMEnt.MARKS;
                    //if (EMEnt.MAKEUP != "")
                    //{
                    //    lblSub3Marks.Text = EMEnt.MAKEUP;
                    //}
                    //else if (EMEnt.MAKEUP == "")
                    //{
                    //    lblSub3Marks.Text = EMEnt.MARKS;
                    //}
                }
            }
            else
            {

                HtmlTableCell subj3td = (HtmlTableCell)e.Row.FindControl("subj3td");
                subj3td.Visible = false;

            }
            #endregion

            #region for marks of subject 4
            Label lblSub4 = e.Row.FindControl("lblSub4") as Label;
            Label lblSub4Marks = e.Row.FindControl("lblSub4Marks") as Label;
            if (lblSub4.Text != "")
            {
                EMEnt = new EXAM_MARKS();

                EMEnt.EXAM_TYPE = ddlExamType.SelectedValue;

                EMEnt.STUDENT_ID = lblStudentId.Text;
                EMEnt.SEMESTER = ddlSemester.Text;
                EMEnt.SUBJECT = lblSub4.Text;
                EMEnt = (EXAM_MARKS)EMSer.GetSingle(EMEnt);
                if (EMEnt != null)
                {
                    if (EMEnt.REMARKS == "A")
                        lblSub4Marks.Text = "Abs";
                    else if (EMEnt.REMARKS == "E")
                        lblSub4Marks.Text = "Exp";
                    else
                        lblSub4Marks.Text = EMEnt.MARKS;
                    //if (EMEnt.MAKEUP != "")
                    //{
                    //    lblSub4Marks.Text = EMEnt.MAKEUP;
                    //}
                    //else if (EMEnt.MAKEUP == "")
                    //{
                    //    lblSub4Marks.Text = EMEnt.MARKS;
                    //}
                }
            }
            else
            {

                HtmlTableCell subj4td = (HtmlTableCell)e.Row.FindControl("subj4td");
                subj4td.Visible = false;

            }
            #endregion

            #region for marks of subject 5
            Label lblSub5 = e.Row.FindControl("lblSub5") as Label;
            Label lblSub5Marks = e.Row.FindControl("lblSub5Marks") as Label;
            if (lblSub5.Text != "")
            {
                EMEnt = new EXAM_MARKS();

                EMEnt.EXAM_TYPE = ddlExamType.SelectedValue;

                EMEnt.STUDENT_ID = lblStudentId.Text;
                EMEnt.SEMESTER = ddlSemester.Text;
                EMEnt.SUBJECT = lblSub5.Text;
                EMEnt = (EXAM_MARKS)EMSer.GetSingle(EMEnt);
                if (EMEnt != null)
                {
                    if (EMEnt.REMARKS == "A")
                        lblSub5Marks.Text = "Abs";
                    else if (EMEnt.REMARKS == "E")
                        lblSub5Marks.Text = "Exp";
                    else
                        lblSub5Marks.Text = EMEnt.MARKS;
                    
                    //if (EMEnt.MAKEUP != "")
                    //{
                    //    lblSub5Marks.Text = EMEnt.MAKEUP;
                    //}
                    //else if (EMEnt.MAKEUP == "")
                    //{
                    //    lblSub5Marks.Text = EMEnt.MARKS;
                    //}
                }
            }
            else
            {

                HtmlTableCell subj5td = (HtmlTableCell)e.Row.FindControl("subj5td");
                subj5td.Visible = false;

            }
            #endregion

            #region for marks of subject 6
            Label lblSub6 = e.Row.FindControl("lblSub6") as Label;
            Label lblSub6Marks = e.Row.FindControl("lblSub6Marks") as Label;
            if (lblSub6.Text != "")
            {
                EMEnt = new EXAM_MARKS();

                EMEnt.EXAM_TYPE = ddlExamType.SelectedValue;

                EMEnt.STUDENT_ID = lblStudentId.Text;
                EMEnt.SEMESTER = ddlSemester.Text;
                EMEnt.SUBJECT = lblSub6.Text;
                EMEnt = (EXAM_MARKS)EMSer.GetSingle(EMEnt);
                if (EMEnt != null)
                {
                    if (EMEnt.REMARKS == "A")
                        lblSub6Marks.Text = "Abs";
                    else if (EMEnt.REMARKS == "E")
                        lblSub6Marks.Text = "Exp";
                    else
                        lblSub6Marks.Text = EMEnt.MARKS;
                    //if (EMEnt.MAKEUP != "")
                    //{
                    //    lblSub6Marks.Text = EMEnt.MAKEUP;
                    //}
                    //else if (EMEnt.MAKEUP == "")
                    //{
                    //    lblSub6Marks.Text = EMEnt.MARKS;
                    //}
                }
            }
            else
            {

                HtmlTableCell subj6td = (HtmlTableCell)e.Row.FindControl("subj6td");
                subj6td.Visible = false;

            }
            #endregion

            #region for marks of subject 7
            Label lblSub7 = e.Row.FindControl("lblSub7") as Label;
            Label lblSub7Marks = e.Row.FindControl("lblSub7Marks") as Label;
            if (lblSub7.Text != "")
            {
                EMEnt = new EXAM_MARKS();

                EMEnt.EXAM_TYPE = ddlExamType.SelectedValue;

                EMEnt.STUDENT_ID = lblStudentId.Text;
                EMEnt.SEMESTER = ddlSemester.Text;
                EMEnt.SUBJECT = lblSub7.Text;
                EMEnt = (EXAM_MARKS)EMSer.GetSingle(EMEnt);
                if (EMEnt != null)
                {
                    if (EMEnt.REMARKS == "A")
                        lblSub7Marks.Text = "Abs";
                    else if (EMEnt.REMARKS == "E")
                        lblSub7Marks.Text = "Exp";
                    else
                        lblSub7Marks.Text = EMEnt.MARKS;
                    //if (EMEnt.MAKEUP != "")
                    //{
                    //    lblSub7Marks.Text = EMEnt.MAKEUP;
                    //}
                    //else if (EMEnt.MAKEUP == "")
                    //{
                    //    lblSub7Marks.Text = EMEnt.MARKS;
                    //}
                }
            }
            else
            {

                HtmlTableCell subj7td = (HtmlTableCell)e.Row.FindControl("subj7td");
                subj7td.Visible = false;

            }
            #endregion
        }
        #endregion
    }

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }

    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSemester();
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        //LoadBatch();
        LoadSemester();
        LoadExamType();
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "printPartOfPage();", true);
    }
}
