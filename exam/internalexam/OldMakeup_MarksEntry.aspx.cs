using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Entity.Components;
using Entity.Framework;
using Service.Components;
using DataHelper.Framework;

public partial class exam_internalexam_OldMakeup_MarksEntry : System.Web.UI.Page
{
    HSS_LEVEL LVLEnt = new HSS_LEVEL();
    HSS_LEVELService LVLSer = new HSS_LEVELService();

    BatchYear BTEnt = new BatchYear();
    BatchYearService BTSer = new BatchYearService();

    HSS_STUDENT STDEnt = new HSS_STUDENT();
    HSS_STUDENTService STDSer = new HSS_STUDENTService();

    HSS_CURRENT_STUDENT CSUBEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSer = new HSS_CURRENT_STUDENTService();

    EXAM_MARKS EMEnt = new EXAM_MARKS();
    EXAM_MARKSService EMSer = new EXAM_MARKSService();

    EXAM_TYPE ETEnt = new EXAM_TYPE();
    EXAM_TYPEService ETSer = new EXAM_TYPEService();

    EXAM_TYPE_MASTER ETMEnt = new EXAM_TYPE_MASTER();
    EXAM_TYPE_MASTERService ETMSrv = new EXAM_TYPE_MASTERService();

    HSS_SUBJECT SUBEnt = new HSS_SUBJECT();
    HSS_SUBJECTService SUBSer = new HSS_SUBJECTService();

    hss_section SecEnt = new hss_section();
    hss_sectionService SecSer = new hss_sectionService();

    hss_faculty FCTEnt = new hss_faculty();
    hss_facultyService FCTSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    full_pass_marks FPMEnt = new full_pass_marks();
    full_pass_marksService FPMSer = new full_pass_marksService();

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    EntityList theList = new EntityList();

    HelperFunction hf = new HelperFunction();


    EXAM_MARKS EMKEnt = new EXAM_MARKS();
    EXAM_MARKSService EMKSrv = new EXAM_MARKSService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFaculty();
            loadLevel();
            LoadSection();
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

    protected void LoadBatch()
    {

        BTEnt = new BatchYear();
        BTEnt.PROGRAM = ddlProgram.SelectedValue;
        ddlBatch.DataSource = BTSer.GetAll(BTEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();

    }

    protected void LoadExamType()
    {

        ddlExamType.DataSource = hf.getExamType(ddlProgram.SelectedValue);
        ddlExamType.DataTextField = "EXAM_TYPE";
        ddlExamType.DataValueField = "PKID";
        ddlExamType.DataBind();



    }

    protected void LoadSubject()
    {

        SUBEnt = new HSS_SUBJECT();
        SUBEnt.PROGRAM = ddlProgram.SelectedValue;
        SUBEnt.SEMESTER = ddlSemester.SelectedValue;
        SUBEnt.STATUS = "1";
        ddlSubject.DataSource = SUBSer.GetAll(SUBEnt);
        ddlSubject.DataTextField = "SUBJECT_NAME";
        ddlSubject.DataValueField = "PK_ID";
        ddlSubject.DataBind();
        ddlSubject.Items.Insert(0, "Select");
    }

    protected void LoadSection()
    {
        SecEnt = new hss_section();
        ddlsection.DataSource = SecSer.GetAll(SecEnt);
        ddlsection.DataTextField = "SECTION";
        ddlsection.DataValueField = "SECTION";
        ddlsection.DataBind();
        ddlsection.Items.Insert(0, "Select");
    }

    protected void LoadSemester()
    {
        BTEnt = new BatchYear();
        BTEnt.PROGRAM = ddlProgram.SelectedValue;
        BTEnt.BATCH = ddlBatch.SelectedValue;
        BTEnt = (BatchYear)BTSer.GetSingle(BTEnt);
        if (BTEnt != null)
        {
            theList = new EntityList();
            SMEnt = new semester();
            SMEnt.SYLLABUS_YEAR = BTEnt.SYLLABUS_YEAR;
            SMEnt.PROGRAM_ID = ddlProgram.SelectedValue;
            theList = SMSer.GetAll(SMEnt);
            

            ddlSemester.DataSource = theList;
            ddlSemester.DataTextField = "SEMESTER_CODE";
            ddlSemester.DataValueField = "PK_ID";
            ddlSemester.DataBind();
            ddlSemester.Items.Insert(0, "Select");
        }
        

    }

    protected void grdMarksEntry_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblRegNo = e.Row.FindControl("lblRegNo") as Label;
            Label lblName = e.Row.FindControl("lblName") as Label;
            Label lblFullMarks = e.Row.FindControl("lblFullMarks") as Label;
            Label lblPassMarks = e.Row.FindControl("lblPassMarks") as Label;

            TextBox txtObtainMarks = e.Row.FindControl("txtObtainMarks") as TextBox;

            RadioButtonList rbtnRemarks = e.Row.FindControl("rbtnRemarks") as RadioButtonList;

            STDEnt = new HSS_STUDENT();
            STDEnt.STUDENT_ID = lblRegNo.Text;
            STDEnt = (HSS_STUDENT)STDSer.GetSingle(STDEnt);
            lblName.Text = STDEnt.NAME_ENGLISH;

            #region for full marks nad pass marks
            SUBEnt = new HSS_SUBJECT();
            SUBEnt.PK_ID = ddlSubject.SelectedValue;
            SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);


            FPMEnt = new full_pass_marks();
            FPMEnt.EXAM_TYPE = ddlExamType.SelectedValue;
            FPMEnt.SUBJECT_ID = SUBEnt.PK_ID;
            FPMEnt = (full_pass_marks)FPMSer.GetSingle(FPMEnt);
            if (FPMEnt != null)
            {
                lblFullMarks.Text = FPMEnt.FULLMARKS_THRCL;
                lblPassMarks.Text = FPMEnt.PASSMARKS_THRCL;
            }

            //else if (SUBEnt.REMARKS == "P")
            //{
            //    ETEnt = new EXAM_TYPE();
            //    ETEnt.EXAM_TYPE_MASTERID = ddlExamType.SelectedValue;
            //    ETEnt.PROGRAM = ddlProgram.SelectedValue;
            //    ETEnt = (EXAM_TYPE)ETSer.GetSingle(ETEnt);
            //    if (ETEnt != null)
            //    {
            //        FPMEnt = new full_pass_marks();
            //        FPMEnt.EXAM_TYPE = ETEnt.PKID;
            //        FPMEnt.SUBJECT_ID = SUBEnt.PK_ID;
            //        FPMEnt = (full_pass_marks)FPMSer.GetSingle(FPMEnt);
            //        if (FPMEnt != null)
            //        {
            //            lblFullMarks.Text = FPMEnt.FULLMARKS_PRCL;
            //            lblPassMarks.Text = FPMEnt.PASSMARKS_PRCL;
            //        }

            //    }
            //}

            #endregion

            #region obtain marks

            ETEnt = new EXAM_TYPE();
            ETEnt.EXAM_TYPE_MASTERID = ddlExamType.SelectedValue;
            ETEnt.PROGRAM = ddlProgram.SelectedValue;
            ETEnt = (EXAM_TYPE)ETSer.GetSingle(ETEnt);
            if (ETEnt != null)
            {

                EMEnt = new EXAM_MARKS();
                EMEnt.STUDENT_ID = lblRegNo.Text;
                EMEnt.SEMESTER = ddlSemester.SelectedValue;
                EMEnt.EXAM_TYPE = ETEnt.EXAM_TYPE_MASTERID;
                EMEnt.SUBJECT = ddlSubject.SelectedValue;

                EMEnt = (EXAM_MARKS)EMSer.GetSingle(EMEnt);
                if (EMEnt != null)
                {
                    txtObtainMarks.Text = EMEnt.MAKEUP;
                    if (EMEnt.REMARKS == "A" || EMEnt.REMARKS == "E")
                        rbtnRemarks.SelectedValue = EMEnt.REMARKS;
                }
            }

            #endregion

        }
    }
    protected void rbtnGrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSubject();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        EntityList theList = new EntityList();
        EntityList newList = new EntityList();
        EMKEnt = new EXAM_MARKS();
        EMKEnt.SEMESTER = ddlSemester.SelectedValue;
        EMKEnt.SUBJECT = ddlSubject.SelectedValue;
        EMKEnt.EXAM_TYPE = ddlExamType.SelectedValue;

        theList = EMKSrv.GetAll(EMKEnt);
        foreach (EXAM_MARKS em in theList)
        {
            if (em.REMARKS != "")
            {
                CSUBEnt = new HSS_CURRENT_STUDENT();
                CSUBEnt.STUDENT_ID = em.STUDENT_ID;
                CSUBEnt.SECTION = ddlsection.SelectedValue;
                CSUBEnt.BATCH = ddlBatch.SelectedValue;
                CSUBEnt.YEAR = hf.NepaliYear();
                
                CSUBEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSUBEnt);
                if (CSUBEnt != null)
                {
                    newList.Add(em);
                }
            }
        }

        grdMarksEntry.DataSource = newList;
        grdMarksEntry.DataBind();
        btnSave.Visible = true;

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DistributedTransaction DT = new DistributedTransaction();
        foreach (GridViewRow gr in grdMarksEntry.Rows)
        {
            Label lblRegNo = gr.FindControl("lblRegNo") as Label;
            TextBox txtObtainMarks = gr.FindControl("txtObtainMarks") as TextBox;
            Label lblFullMarks = gr.FindControl("lblFullMarks") as Label;
            Label lblPassMarks = gr.FindControl("lblPassMarks") as Label;
            RadioButtonList rbtnRemarks = gr.FindControl("rbtnRemarks") as RadioButtonList;
            try
            {
                if (string.IsNullOrEmpty(txtObtainMarks.Text))
                {
                    txtObtainMarks.Text = "0";
                }
                double mm = Convert.ToDouble(txtObtainMarks.Text);
                if (mm > Convert.ToDouble(lblFullMarks.Text) || mm < 0)
                {
                    DT.HAPPY = false;
                    HelperFunction.MsgBox(this, this.GetType(), "Obtained Mark is greater than Full Mark");
                    break;
                }
                
                EMEnt = new EXAM_MARKS();
                EMEnt.STUDENT_ID = lblRegNo.Text;
                EMEnt.SEMESTER = ddlSemester.SelectedValue;
                EMEnt.EXAM_TYPE = ddlExamType.SelectedValue;
                EMEnt.SUBJECT = ddlSubject.SelectedValue;
                EMEnt = (EXAM_MARKS)EMSer.GetSingle(EMEnt);
                if (EMEnt != null)
                {
                    #region update

                    if (string.IsNullOrEmpty(txtObtainMarks.Text))
                    {
                        txtObtainMarks.Text = "0";
                    }
                    EMEnt.MAKEUP = txtObtainMarks.Text;

                    if (rbtnRemarks.SelectedValue == "")
                    {
                        if (Convert.ToDouble(txtObtainMarks.Text) < Convert.ToDouble(lblPassMarks.Text))
                        {
                            EMEnt.REMARKS = "F";
                        }
                        else
                            EMEnt.REMARKS = "";
                    }
                    else
                        EMEnt.REMARKS = rbtnRemarks.SelectedValue;

                    EMSer.Update(EMEnt, DT);
                }
                #endregion

                else
                {
                    #region insert
                    EMEnt = new EXAM_MARKS();
                    EMEnt.STUDENT_ID = lblRegNo.Text;
                    EMEnt.SEMESTER = ddlSemester.SelectedValue;
                    EMEnt.EXAM_TYPE = ddlExamType.SelectedValue;
                    EMEnt.SUBJECT = ddlSubject.SelectedValue;

                    EMEnt.MARKS = txtObtainMarks.Text;

                    if (rbtnRemarks.SelectedValue == "")
                    {
                        if (Convert.ToDouble(txtObtainMarks.Text) < Convert.ToDouble(lblPassMarks.Text))
                        {
                            EMEnt.REMARKS = "F";
                        }
                        else
                            EMEnt.REMARKS = "";
                    }
                    else
                    {
                        EMEnt.REMARKS = rbtnRemarks.SelectedValue;
                    }


                    EMSer.Insert(EMEnt, DT);

                    #endregion
                }

            }
            catch
            {
                DT.HAPPY = false;
            }


        }
        if (DT.HAPPY == true)
        {
            DT.Commit();
            HelperFunction.MsgBox(this, this.GetType(), "Data Saved Sucessfully");
        }
        else
        {
            DT.Abort();
            HelperFunction.MsgBox(this, this.GetType(), "Data not Saved.Please enter the marks in number only.");
        }
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSubject();
        LoadExamType();
    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBatch();
        
    }

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }

    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSemester();
    }
}