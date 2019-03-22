using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using System.Collections;
using Entity.Framework;
using Service.Components;

using DataAccess.Components;
using DataAccess.Framework;
using DataHelper.Framework;

using System.Text;

public partial class uc_NewFolder1_marksheetreport : System.Web.UI.UserControl
{
    HelperFunction hf = new HelperFunction();

    string subject_pkid;

    HSS_CURRENT_STUDENT CSEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSer = new HSS_CURRENT_STUDENTService();

    HSS_STUDENT SEnt = new HSS_STUDENT();
    HSS_STUDENTService SSer = new HSS_STUDENTService();

    EXAM_TYPE EXTEnt = new EXAM_TYPE();
    EXAM_TYPEService EXTSer = new EXAM_TYPEService();

    EXAM_MARKS EXMEnt = new EXAM_MARKS();
    EXAM_MARKSService EXMSer = new EXAM_MARKSService();

    HSS_SUBJECT SUBEnt = new HSS_SUBJECT();
    HSS_SUBJECTService SUBSer = new HSS_SUBJECTService();

    HSS_NAME NEnt = new HSS_NAME();
    HSS_NAMEService NSer = new HSS_NAMEService();

    HSS_RESULT REnt = new HSS_RESULT();
    HSS_RESULTSService RSer = new HSS_RESULTSService();

    HSS_ADMINSTAFF ADEnt = new HSS_ADMINSTAFF();
    HSS_ADMINSTAFFService ADSer = new HSS_ADMINSTAFFService();

    HSS_ATTACHMENTS ATCEnt = new HSS_ATTACHMENTS();
    HSS_ATTACHMENTSService ATCSer = new HSS_ATTACHMENTSService();

    HSS_GRADE GEnt = new HSS_GRADE();
    HSS_GRADEService GSer = new HSS_GRADEService();

    string grade = "";
    string studentid = "";
    string section = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            grade = Request.QueryString["pclass"].ToString();
            studentid = Request.QueryString["pstudent"].ToString();
            section = Request.QueryString["psection"].ToString();

            LoadData(studentid, grade, section);
            LoadInfo(studentid, grade, section);

        }
    }


    private void LoadInfo(string studentid, string grade, string section)
    {
        NEnt = new HSS_NAME();
        ADEnt = new HSS_ADMINSTAFF();
        ATCEnt = new HSS_ATTACHMENTS();

        EntityList theList = new EntityList();


        NEnt = (HSS_NAME)NSer.GetSingle(NEnt);

        if (NEnt != null)
        {
            lblCollegeName.Text = NEnt.NAME;
            lblAddress.Text = NEnt.ADRESS;
            lblPhoneNo.Text = NEnt.CONTACT;
            lblEmail.Text = NEnt.WEBSITE;
        }

        ATCEnt.STUDENT_ID = studentid;
        ATCEnt = (HSS_ATTACHMENTS)ATCSer.GetSingle(ATCEnt);
        if (ATCEnt != null)
        {
            imgStudent.Visible = true;
            imgStudent.ImageUrl = ATCEnt.PHOTO;
        }


        theList = ADSer.GetAll(ADEnt);
        foreach (HSS_ADMINSTAFF ad in theList)
        {
            if (ad.PKID == "1")
            {
                if (ad.STATUS == "1")
                {
                    imga1.Visible = true;
                    lblPost1.Visible = true;
                    lblP1.Visible = true;
                    lblName1.Visible = true;
                    if (ad.SIGNATURE != "")
                        imga1.ImageUrl = @"~/images/signature/" + ad.SIGNATURE + ".jpg";
                    else
                        imga1.ImageUrl = @"~/images/signature/default.gif";
                    lblPost1.Text = ad.POST;
                    lblName1.Text = ad.NAME;

                }
            }
            else if (ad.PKID == "2")
            {
                if (ad.STATUS == "1")
                {
                    imga2.Visible = true;
                    lblPost2.Visible = true;
                    lblP2.Visible = true;
                    lblName2.Visible = true;

                    if (ad.SIGNATURE != "")
                        imga2.ImageUrl = @"~/images/signature/" + ad.SIGNATURE + ".jpg";
                    else
                        imga2.ImageUrl = @"~/images/signature/default.gif";
                    lblPost2.Text = ad.POST;
                    lblName2.Text = ad.NAME;

                }
            }
            else if (ad.PKID == "3")
            {
                if (ad.STATUS == "1")
                {
                    imga3.Visible = true;
                    lblPost3.Visible = true;
                    lblP3.Visible = true;
                    lblName3.Visible = true;

                    if (ad.SIGNATURE != "")
                        imga3.ImageUrl = @"~/images/signature/" + ad.SIGNATURE + ".jpg";
                    else
                        imga3.ImageUrl = @"~/images/signature/default.gif";
                    lblPost3.Text = ad.POST;
                    lblName3.Text = ad.NAME;

                }
            }
            else if (ad.PKID == "4")
            {
                if (ad.STATUS == "1")
                {
                    imga4.Visible = true;
                    lblPost4.Visible = true;
                    lblP4.Visible = true;
                    lblName4.Visible = true;

                    if (ad.SIGNATURE != "")
                        imga4.ImageUrl = @"~/images/signature/" + ad.SIGNATURE + ".jpg";
                    else
                        imga4.ImageUrl = @"~/images/signature/default.gif";
                    lblPost4.Text = ad.POST;
                    lblName4.Text = ad.NAME;

                }
            }
        }


        lblStName.Text = hf.getStudentName(studentid).ToUpper();
        lblClass.Text = grade;
        lblSection.Text = section;
        lblRegNo.Text = studentid.ToUpper();

        lblDt.Visible = true;
       

        lblMrkSht.Visible = true;
        lblStNameI.Visible = true;
        lblClassI.Visible = true;
        lblSecI.Visible = true;
        lblRegNoI.Visible = true;

        imgNccsLogo.Visible = true;
        imgISO.Visible = true;
        DataTable DT = new DataTable();
        DT=getResult_date(studentid,hf.CurrentYear(hf.NepaliMonth(),hf.NepaliYear()));
       
       lblDate.Text = DT.Rows[0][0].ToString();
        
       

        /*........................for loading principal name and post and so on..............*/

    }

    private void LoadData(string studentid, string grade, string section)
    {

        gridMrkSht.DataSource = getMarksheet(studentid, grade, section);
        gridMrkSht.DataBind();
        //StringBuilder vtxt = new StringBuilder();
        //gridMrkSht. SelectedRow.Cells[0].Text = vtxt.ToString();
    }

    private IDbDataParameter[] CreateParmans(string studentid, string grade, string section)
    {

        List<IDbDataParameter> cmdParams = new List<IDbDataParameter>();
        //input Parameters start
        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_studentid", studentid));
        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_grade", grade));
        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_section", section));


        //input Parameters ends
        cmdParams.Add(DataAccessFactory.CreateDataParameter("Result", ""));

        return cmdParams.ToArray();
    }


    public DataTable getMarksheet(string studentid, string grade, string section)
    {
        GenericGetValueService srvGen = new GenericGetValueService();
        DataTable DT = new DataTable();
        try
        {
            DT = srvGen.ReaderToTable("PKJ_REPORTS.student_marksheet", System.Data.CommandType.StoredProcedure, CreateParmans(studentid, grade, section));
        }
        catch
        {
        }
        return DT;
    }

    protected void gridMrkSht_RowDataBound(object sender, GridViewRowEventArgs e)
    {
       /* #region
        if (e.Row.RowType == DataControlRowType.Header)
        {
            Label lblFMTHE1 = e.Row.FindControl("lblFMTHE1") as Label;
            Label lblPMTHE1 = e.Row.FindControl("lblPMTHE1") as Label;
            Label lblFMPRE1 = e.Row.FindControl("lblFMPRE1") as Label;
            Label lblPMPRE1 = e.Row.FindControl("lblPMPRE1") as Label;

            EXTEnt = new EXAM_TYPE();
            EXTEnt.PKID = "1";

            EXTEnt = (EXAM_TYPE)EXTSer.GetSingle(EXTEnt);
            if (EXTEnt != null)
            {
                lblFMTHE1.Text = EXTEnt.FULLMARKS_THRCAL;

                lblPMTHE1.Text = EXTEnt.PASSMARKS_THRCAL;
                lblFMPRE1.Text = EXTEnt.FULLMARKS_PRCAL;
                lblPMPRE1.Text = EXTEnt.PASSMARKS_PRCAL;
            }

            Label lblFMTHE2 = e.Row.FindControl("lblFMTHE2") as Label;
            Label lblPMTHE2 = e.Row.FindControl("lblPMTHE2") as Label;
            Label lblFMPRE2 = e.Row.FindControl("lblFMPRE2") as Label;
            Label lblPMPRE2 = e.Row.FindControl("lblPMPRE2") as Label;

            EXTEnt = new EXAM_TYPE();
            EXTEnt.PKID = "2";

            EXTEnt = (EXAM_TYPE)EXTSer.GetSingle(EXTEnt);
            if (EXTEnt != null)
            {
                lblFMTHE2.Text = EXTEnt.FULLMARKS_THRCAL;
                lblPMTHE2.Text = EXTEnt.PASSMARKS_THRCAL;
                lblFMPRE2.Text = EXTEnt.FULLMARKS_PRCAL;
                lblPMPRE2.Text = EXTEnt.PASSMARKS_PRCAL;
            }

            Label lblFMTHE3 = e.Row.FindControl("lblFMTHE3") as Label;
            Label lblPMTHE3 = e.Row.FindControl("lblPMTHE3") as Label;
            Label lblFMPRE3 = e.Row.FindControl("lblFMPRE3") as Label;
            Label lblPMPRE3 = e.Row.FindControl("lblPMPRE3") as Label;

            EXTEnt = new EXAM_TYPE();
            EXTEnt.PKID = "3";

            EXTEnt = (EXAM_TYPE)EXTSer.GetSingle(EXTEnt);
            if (EXTEnt != null)
            {
                lblFMTHE3.Text = EXTEnt.FULLMARKS_THRCAL;
                lblPMTHE3.Text = EXTEnt.PASSMARKS_THRCAL;
                lblFMPRE3.Text = EXTEnt.FULLMARKS_PRCAL;
                lblPMPRE3.Text = EXTEnt.PASSMARKS_PRCAL;
            }

            Label lblFMTHE4 = e.Row.FindControl("lblFMTHE4") as Label;
            Label lblPMTHE4 = e.Row.FindControl("lblPMTHE4") as Label;
            Label lblFMPRE4 = e.Row.FindControl("lblFMPRE4") as Label;
            Label lblPMPRE4 = e.Row.FindControl("lblPMPRE4") as Label;

            EXTEnt = new EXAM_TYPE();
            EXTEnt.PKID = "4";

            EXTEnt = (EXAM_TYPE)EXTSer.GetSingle(EXTEnt);
            if (EXTEnt != null)
            {
                lblFMTHE4.Text = EXTEnt.FULLMARKS_THRCAL;
                lblPMTHE4.Text = EXTEnt.PASSMARKS_THRCAL;
                lblFMPRE4.Text = EXTEnt.FULLMARKS_PRCAL;
                lblPMPRE4.Text = EXTEnt.PASSMARKS_PRCAL;
            }

            Label lblFMTHE5 = e.Row.FindControl("lblFMTHE5") as Label;
            Label lblPMTHE5 = e.Row.FindControl("lblPMTHE5") as Label;
            Label lblFMPRE5 = e.Row.FindControl("lblFMPRE5") as Label;
            Label lblPMPRE5 = e.Row.FindControl("lblPMPRE5") as Label;

            EXTEnt = new EXAM_TYPE();
            EXTEnt.PKID = "5";

            EXTEnt = (EXAM_TYPE)EXTSer.GetSingle(EXTEnt);
            if (EXTEnt != null)
            {
                lblFMTHE5.Text = EXTEnt.FULLMARKS_THRCAL;
                lblPMTHE5.Text = EXTEnt.PASSMARKS_THRCAL;
                lblFMPRE5.Text = EXTEnt.FULLMARKS_PRCAL;
                lblPMPRE5.Text = EXTEnt.PASSMARKS_PRCAL;
            }

        }


        //................FOR OPTIONAL OR COMPULSARY....................

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblOPT = e.Row.FindControl("lblOPT") as Label;

            Label lblOption = e.Row.FindControl("lblOption") as Label;



            if (lblOPT.Text == "N")
            {
                lblOption.Text = "C<br>O<br>R<br>E";
            }
            else if (lblOPT.Text == "Y")
            {
                lblOption.Text = "E<br>L<br>E<br>C<br>T<br>I<br>V<br>E<br>S";
            }

        }



        //.........................for 1st class test

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblFCTMark = e.Row.FindControl("lblFCTMark") as Label;

            Label lblFCT = e.Row.FindControl("lblFCT") as Label;
            Label lblSubjectName = e.Row.FindControl("lblSubjectName") as Label;


            EXMEnt = new EXAM_MARKS();
            SUBEnt = new HSS_SUBJECT();

            SUBEnt.SUBJECT_NAME = lblSubjectName.Text;
            SUBEnt.SEMESTER = grade;

            SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
            if (SUBEnt != null)
            {
                subject_pkid = SUBEnt.PK_ID;
            }

            EXMEnt.SUBJECT = subject_pkid;
            EXMEnt.STUDENT_ID = studentid;
            EXMEnt.SEMESTER = grade;
            EXMEnt.EXAM_TYPE = lblFCT.Text;

            EXMEnt = (EXAM_MARKS)EXMSer.GetSingle(EXMEnt);
            if (EXMEnt != null)
            {
                lblFCTMark.Text = EXMEnt.MARKS;
            }

            else
            {
                lblFCTMark.Text = "-";
            }


        }

        //....................for 1st term exam...............

        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Label lblFTMark = e.Row.FindControl("lblFTMark") as Label;
            Label lblFT = e.Row.FindControl("lblFT") as Label;
            Label lblSubjectName = e.Row.FindControl("lblSubjectName") as Label;


            EXMEnt = new EXAM_MARKS();
            SUBEnt = new HSS_SUBJECT();

            SUBEnt.SUBJECT_NAME = lblSubjectName.Text;
            SUBEnt.SEMESTER = grade;

            SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
            if (SUBEnt != null)
            {
                subject_pkid = SUBEnt.PK_ID;

            }

            EXMEnt.SUBJECT = subject_pkid;
            EXMEnt.STUDENT_ID = studentid;
            EXMEnt.SEMESTER = grade;
            EXMEnt.EXAM_TYPE = lblFT.Text;

            EXMEnt = (EXAM_MARKS)EXMSer.GetSingle(EXMEnt);
            if (EXMEnt != null)
            {
                lblFTMark.Text = EXMEnt.MARKS;
            }
            else
            {
                lblFTMark.Text = "-";
            }

        }

        //.............................for 2nd class test exam..........................

        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Label lblSCTMark = e.Row.FindControl("lblSCTMark") as Label;
            Label lblSCT = e.Row.FindControl("lblSCT") as Label;
            Label lblSubjectName = e.Row.FindControl("lblSubjectName") as Label;


            EXMEnt = new EXAM_MARKS();
            SUBEnt = new HSS_SUBJECT();

            SUBEnt.SUBJECT_NAME = lblSubjectName.Text;
            SUBEnt.SEMESTER = grade;

            SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
            if (SUBEnt != null)
            {
                subject_pkid = SUBEnt.PK_ID;

            }

            EXMEnt.SUBJECT = subject_pkid;
            EXMEnt.STUDENT_ID = studentid;
            EXMEnt.SEMESTER = grade;
            EXMEnt.EXAM_TYPE = lblSCT.Text;

            EXMEnt = (EXAM_MARKS)EXMSer.GetSingle(EXMEnt);
            if (EXMEnt != null)
            {
                lblSCTMark.Text = EXMEnt.MARKS;

            }
            else
            {
                lblSCTMark.Text = "-";


            }


        }

        //.............................for MID TERM exam..........................

        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Label lblMTMark = e.Row.FindControl("lblMTMark") as Label;
            Label lblMT = e.Row.FindControl("lblMT") as Label;
            Label lblSubjectName = e.Row.FindControl("lblSubjectName") as Label;


            EXMEnt = new EXAM_MARKS();
            SUBEnt = new HSS_SUBJECT();

            SUBEnt.SUBJECT_NAME = lblSubjectName.Text;
            SUBEnt.SEMESTER = grade;

            SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
            if (SUBEnt != null)
            {
                subject_pkid = SUBEnt.PK_ID;

            }

            EXMEnt.SUBJECT = subject_pkid;
            EXMEnt.STUDENT_ID = studentid;
            EXMEnt.SEMESTER = grade;
            EXMEnt.EXAM_TYPE = lblMT.Text;

            EXMEnt = (EXAM_MARKS)EXMSer.GetSingle(EXMEnt);
            if (EXMEnt != null)
            {
                lblMTMark.Text = EXMEnt.MARKS;


            }
            else
            {
                lblMTMark.Text = "-";


            }

        }

        //.............................for FINAL TERM exam..........................

        if (e.Row.RowType == DataControlRowType.DataRow)
        {


            Label lblFNTMark = e.Row.FindControl("lblFNTMark") as Label;
            Label lblFNT = e.Row.FindControl("lblFNT") as Label;
            Label lblSubjectName = e.Row.FindControl("lblSubjectName") as Label;


            EXMEnt = new EXAM_MARKS();
            SUBEnt = new HSS_SUBJECT();

            SUBEnt.SUBJECT_NAME = lblSubjectName.Text;
            SUBEnt.SEMESTER = grade;

            SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
            if (SUBEnt != null)
            {
                subject_pkid = SUBEnt.PK_ID;

            }

            EXMEnt.SUBJECT = subject_pkid;
            EXMEnt.STUDENT_ID = studentid;
            EXMEnt.SEMESTER = grade;
            EXMEnt.EXAM_TYPE = lblFNT.Text;

            EXMEnt = (EXAM_MARKS)EXMSer.GetSingle(EXMEnt);
            if (EXMEnt != null)
            {
                lblFNTMark.Text = EXMEnt.MARKS;


            }
            else
            {
                lblFNTMark.Text = "-";


            }

        }

        //..........................for footer of 1st class test....................

        if (e.Row.RowType == DataControlRowType.Footer)
        {


            Label lblTotale1 = e.Row.FindControl("lblTotale1") as Label;
            Label lblPercente1 = e.Row.FindControl("lblPercente1") as Label;
            Label lblRemarkse1 = e.Row.FindControl("lblRemarkse1") as Label;
            Label lblRanke1 = e.Row.FindControl("lblRanke1") as Label;
            Label lblPrce1 = e.Row.FindControl("lblPrce1") as Label;
            Label lblGradee1 = e.Row.FindControl("lblGradee1") as Label;

            REnt = new HSS_RESULT();
            EXTEnt = new EXAM_TYPE();

            EXTEnt.PKID = "1";
            EXTEnt = (EXAM_TYPE)EXTSer.GetSingle(EXTEnt);
            if (EXTEnt != null)
            {
                REnt.STUDENTID = studentid;
                REnt.CLASS = grade;
                REnt.EXAMTYPE = EXTEnt.EXAMTYPE;


                REnt = (HSS_RESULT)RSer.GetSingle(REnt);
                if (REnt != null)
                {
                    lblPrce1.Visible = true;
                    lblTotale1.Text = REnt.TOTAL;
                    lblPercente1.Text = REnt.PERCENTAGE;
                    lblRemarkse1.Text = REnt.REMARKS;
                  //  lblRanke1.Text = REnt.RANK;
                    lblRanke1.Text = "-";
                    lblGradee1.Text = REnt.GRADE;
                }
                if (lblPercente1.Text == "")
                {
                    lblPrce1.Visible = false;
                    lblTotale1.Text = "-";
                    lblPercente1.Text = "-";
                    lblRemarkse1.Text = "-";
                    lblRanke1.Text = "-";
                    lblGradee1.Text = "-";
                }
                if (lblPercente1.Text == "-")
                {
                    lblPrce1.Visible = false;
                }
            }




        }

        //..........................for footer of 1st Term....................

        if (e.Row.RowType == DataControlRowType.Footer)
        {


            Label lblTotale2 = e.Row.FindControl("lblTotale2") as Label;
            Label lblPercente2 = e.Row.FindControl("lblPercente2") as Label;
            Label lblRemarkse2 = e.Row.FindControl("lblRemarkse2") as Label;
            Label lblRanke2 = e.Row.FindControl("lblRanke2") as Label;
            Label lblPrce2 = e.Row.FindControl("lblPrce2") as Label;
            Label lblGradee2 = e.Row.FindControl("lblGradee2") as Label;
            REnt = new HSS_RESULT();
            EXTEnt = new EXAM_TYPE();

            EXTEnt.PKID = "2";
            EXTEnt = (EXAM_TYPE)EXTSer.GetSingle(EXTEnt);
            if (EXTEnt != null)
            {

                REnt.STUDENTID = studentid;
                REnt.CLASS = grade;
                REnt.EXAMTYPE = EXTEnt.EXAMTYPE;

                REnt = (HSS_RESULT)RSer.GetSingle(REnt);
                if (REnt != null)
                {
                    lblPrce2.Visible = true;
                    lblTotale2.Text = REnt.TOTAL;
                    lblPercente2.Text = REnt.PERCENTAGE;
                    lblRemarkse2.Text = REnt.REMARKS;
                  //  lblRanke2.Text = REnt.RANK;
                    lblRanke2.Text = "-";
                    lblGradee2.Text = REnt.GRADE;
                }
                if (lblPercente2.Text == "")
                {
                    lblPrce2.Visible = false;
                    lblTotale2.Text = "-";
                    lblPercente2.Text = "-";
                    lblRemarkse2.Text = "-";
                    lblRanke2.Text = "-";
                    lblGradee2.Text = "-";
                }
                if (lblPercente2.Text == "-")
                {
                    lblPrce2.Visible = false;
                }

            }


        }

        //..........................for footer of 2nd Class Test....................

        if (e.Row.RowType == DataControlRowType.Footer)
        {


            Label lblTotale3 = e.Row.FindControl("lblTotale3") as Label;
            Label lblPercente3 = e.Row.FindControl("lblPercente3") as Label;
            Label lblRemarkse3 = e.Row.FindControl("lblRemarkse3") as Label;
            Label lblRanke3 = e.Row.FindControl("lblRanke3") as Label;
            Label lblPrce3 = e.Row.FindControl("lblPrce3") as Label;
            Label lblGradee3 = e.Row.FindControl("lblGradee3") as Label;

            REnt = new HSS_RESULT();
            EXTEnt = new EXAM_TYPE();

            EXTEnt.PKID = "3";
            EXTEnt = (EXAM_TYPE)EXTSer.GetSingle(EXTEnt);
            if (EXTEnt != null)
            {
                REnt.STUDENTID = studentid;
                REnt.CLASS = grade;
                REnt.EXAMTYPE = EXTEnt.EXAMTYPE;

                REnt = (HSS_RESULT)RSer.GetSingle(REnt);
                if (REnt != null)
                {
                    lblPrce3.Visible = true;
                    lblTotale3.Text = REnt.TOTAL;
                    lblPercente3.Text = REnt.PERCENTAGE;
                    lblRemarkse3.Text = REnt.REMARKS;
                   // lblRanke3.Text = REnt.RANK;
                    lblGradee3.Text = REnt.GRADE;
                }
                if (lblPercente3.Text == "")
                {
                    lblPrce3.Visible = false;
                    lblTotale3.Text = "-";
                    lblPercente3.Text = "-";
                    lblRemarkse3.Text = "-";
                    lblRanke3.Text = "-";
                    lblGradee3.Text = "-";
                }
                if (lblPercente3.Text == "-")
                {
                    lblPrce3.Visible = false;
                }

            }



        }

        //..........................for footer of Mid Term....................

        if (e.Row.RowType == DataControlRowType.Footer)
        {


            Label lblTotale4 = e.Row.FindControl("lblTotale4") as Label;
            Label lblPercente4 = e.Row.FindControl("lblPercente4") as Label;
            Label lblRemarkse4 = e.Row.FindControl("lblRemarkse4") as Label;
            Label lblRanke4 = e.Row.FindControl("lblRanke4") as Label;
            Label lblPrce4 = e.Row.FindControl("lblPrce3") as Label;
            Label lblGradee4 = e.Row.FindControl("lblGradee4") as Label;

            REnt = new HSS_RESULT();
            EXTEnt = new EXAM_TYPE();

            EXTEnt.PKID = "4";
            EXTEnt = (EXAM_TYPE)EXTSer.GetSingle(EXTEnt);
            if (EXTEnt != null)
            {
                REnt.STUDENTID = studentid;
                REnt.CLASS = grade;
                REnt.EXAMTYPE = EXTEnt.EXAMTYPE;

                REnt = (HSS_RESULT)RSer.GetSingle(REnt);
                if (REnt != null)
                {
                    lblPrce4.Visible = true;
                    lblTotale4.Text = REnt.TOTAL;
                    lblPercente4.Text = REnt.PERCENTAGE;
                    //lblRemarkse4.Text = REnt.REMARKS;
                    lblRanke4.Text = REnt.RANK;
                    lblGradee4.Text = REnt.GRADE;
                }
                if (lblPercente4.Text == "")
                {
                    lblPrce4.Visible = false;
                    lblTotale4.Text = "-";
                    lblPercente4.Text = "-";
                    lblRemarkse4.Text = "-";
                    lblRanke4.Text = "-";
                    lblGradee4.Text = "-";
                }
                if (lblPercente4.Text == "-")
                {
                    lblPrce4.Visible = false;
                }
            }
        }

        //..........................for footer of Final Term....................

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotale5 = e.Row.FindControl("lblTotale5") as Label;
            Label lblPercente5 = e.Row.FindControl("lblPercente5") as Label;
            Label lblRemarkse5 = e.Row.FindControl("lblRemarkse5") as Label;
            Label lblRanke5 = e.Row.FindControl("lblRanke5") as Label;
            Label lblPrce5 = e.Row.FindControl("lblPrce5") as Label;
            Label lblGradee5 = e.Row.FindControl("lblGradee5") as Label;

            REnt = new HSS_RESULT();
            EXTEnt = new EXAM_TYPE();

            EXTEnt.PKID = "5";
            EXTEnt = (EXAM_TYPE)EXTSer.GetSingle(EXTEnt);
            if (EXTEnt != null)
            {
                REnt.STUDENTID = studentid;
                REnt.CLASS = grade;
                REnt.EXAMTYPE = EXTEnt.EXAMTYPE;

                REnt = (HSS_RESULT)RSer.GetSingle(REnt);
                if (REnt != null)
                {
                    lblPrce5.Visible = true;
                    lblTotale5.Text = REnt.TOTAL;
                    lblPercente5.Text = REnt.PERCENTAGE;
                    lblRemarkse5.Text = REnt.REMARKS;
                    //lblRanke5.Text = REnt.RANK;
                    lblGradee5.Text = REnt.GRADE;
                }
                if (lblPercente5.Text == "")
                {
                    lblPrce5.Visible = false;
                    lblTotale5.Text = "-";
                    lblPercente5.Text = "-";
                    lblRemarkse5.Text = "-";
                    lblRanke5.Text = "-";
                    lblGradee5.Text = "-";
                }
                if (lblPercente5.Text == "-")
                {
                    lblPrce5.Visible = false;
                }

            }


        }
        #endregion
        for (int rowIndex = gridMrkSht.Rows.Count - 2; rowIndex >= 0; rowIndex--)
        {
            GridViewRow gvRow = gridMrkSht.Rows[rowIndex];
            GridViewRow gvPreviousRow = gridMrkSht.Rows[rowIndex + 1];
            if (((Label)gvRow.Cells[1].FindControl("lblOPT")).Text == ((Label)gvPreviousRow.Cells[0].FindControl("lblOPT")).Text)
            {
                if (gvPreviousRow.Cells[0].RowSpan < 2)
                {
                    gvRow.Cells[0].RowSpan = 2;
                }
                else
                {
                    gvRow.Cells[0].RowSpan = gvPreviousRow.Cells[0].RowSpan + 1;
                }
                gvPreviousRow.Cells[0].Visible = false;
            }

        }
        * */


    }

    private IDbDataParameter[] CreateParmans2(string studentid, string session_year)
    {

        List<IDbDataParameter> cmdParams = new List<IDbDataParameter>();
        //input Parameters start
        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_student_ID", studentid));

        cmdParams.Add(DataAccessFactory.CreateDataParameter("var_session_year", session_year));


        //input Parameters ends
        cmdParams.Add(DataAccessFactory.CreateDataParameter("Result", ""));

        return cmdParams.ToArray();
    }

    public DataTable getResult_date(string studentid, string session_year)
    {
        GenericGetValueService srvGen = new GenericGetValueService();
        DataTable DT = new DataTable();
        try
        {
            DT = srvGen.ReaderToTable("PKJ_REPORTS.result_date", System.Data.CommandType.StoredProcedure, CreateParmans2(studentid, session_year));
        }
        catch
        {
        }
        return DT;
    }



}






