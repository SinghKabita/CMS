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

public partial class exam_internalexam_MarksEntry : System.Web.UI.Page
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

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    EntityList theList = new EntityList();

    HelperFunction hf = new HelperFunction();


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
        BTEnt.ACTIVE = "1";
        BTEnt.SEMESTER = ddlSemester.SelectedValue;
        BTEnt.PROGRAM = ddlProgram.SelectedValue;
        ddlBatch.DataSource = BTSer.GetAll(BTEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();

    }

    protected void LoadExamType()
    {
        theList = new EntityList();
        EntityList semList = new EntityList();

        ETEnt = new EXAM_TYPE();
        ETEnt.PROGRAM = ddlProgram.SelectedValue;
        theList = ETSer.GetAll(ETEnt);

        foreach (EXAM_TYPE et in theList)
        {
            ETMEnt = new EXAM_TYPE_MASTER();
            ETMEnt.PKID = et.EXAM_TYPE_MASTERID;

            ETMEnt = (EXAM_TYPE_MASTER)ETMSrv.GetSingle(ETMEnt);
            if (ETMEnt != null)
            {
                if (ETMEnt.MAKEUP_OF == "")
                    semList.Add(ETMEnt);
            }
        }
        ddlExamType.DataSource = semList;
        ddlExamType.DataTextField = "EXAM_TYPE";
        ddlExamType.DataValueField = "PKID";
        ddlExamType.DataBind();
        ddlExamType.Items.Insert(0, "Select");
    }

    protected void LoadSubject()
    {

        SUBEnt = new HSS_SUBJECT();
        SUBEnt.PROGRAM = ddlProgram.SelectedValue;
        SUBEnt.SEMESTER = ddlSemester.SelectedValue;
        ddlSubject.DataSource = SUBSer.GetAll(SUBEnt);
        ddlSubject.DataTextField = "SUBJECT_NAME";
        ddlSubject.DataValueField = "PK_ID";
        ddlSubject.DataBind();
        ddlSubject.Items.Insert(0, "Select");
    }

    protected void LoadSection()
    {
        SecEnt = new Section();

        ddlsection.DataSource = SecSer.GetAll(SecEnt);
        ddlsection.DataTextField = "SECTION";
        ddlsection.DataValueField = "SECTION";
        ddlsection.DataBind();
        ddlsection.Items.Insert(0, "Select");
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
            CheckBox chkAbs = e.Row.FindControl("chkAbs") as CheckBox;
            CheckBox chkExpel = e.Row.FindControl("chkExpel") as CheckBox;

            STDEnt = new HSS_STUDENT();
            STDEnt.STUDENT_ID = lblRegNo.Text;
            STDEnt = (HSS_STUDENT)STDSer.GetSingle(STDEnt);
            lblName.Text = STDEnt.NAME_ENGLISH;

            #region for full marks and pass marks
            SUBEnt = new HSS_SUBJECT();
            SUBEnt.PK_ID = ddlSubject.SelectedValue;
            SUBEnt = (HSS_SUBJECT)SUBSer.GetSingle(SUBEnt);
            if (SUBEnt.REMARKS == "T")
            {

                ETEnt = new EXAM_TYPE();
                ETEnt.EXAM_TYPE_MASTERID = ddlExamType.SelectedValue;
                ETEnt.PROGRAM = ddlProgram.SelectedValue;
                ETEnt = (EXAM_TYPE)ETSer.GetSingle(ETEnt);
                if (ETEnt != null)
                {
                    FPMEnt = new full_pass_marks();
                    FPMEnt.EXAM_TYPE = ETEnt.PKID;
                    FPMEnt.SUBJECT_ID = SUBEnt.PK_ID;
                    FPMEnt = (full_pass_marks)FPMSer.GetSingle(FPMEnt);
                    if (FPMEnt != null)
                    {
                        lblFullMarks.Text = FPMEnt.FULLMARKS_THRCL;
                        lblPassMarks.Text = FPMEnt.PASSMARKS_THRCL;
                    }

                }
            }

            else if (SUBEnt.REMARKS == "P")
            {
                ETEnt = new EXAM_TYPE();
                ETEnt.EXAM_TYPE_MASTERID = ddlExamType.SelectedValue;
                ETEnt.PROGRAM = ddlProgram.SelectedValue;
                ETEnt = (EXAM_TYPE)ETSer.GetSingle(ETEnt);
                if (ETEnt != null)
                {
                    FPMEnt = new full_pass_marks();
                    FPMEnt.EXAM_TYPE = ETEnt.PKID;
                    FPMEnt.SUBJECT_ID = SUBEnt.PK_ID;
                    FPMEnt = (full_pass_marks)FPMSer.GetSingle(FPMEnt);
                    if (FPMEnt != null)
                    {
                        lblFullMarks.Text = FPMEnt.FULLMARKS_PRCL;
                        lblPassMarks.Text = FPMEnt.PASSMARKS_PRCL;
                    }

                }
            }

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
                EMEnt.EXAM_TYPE = ETEnt.PKID;
                EMEnt.SUBJECT = ddlSubject.SelectedValue;

                EMEnt = (EXAM_MARKS)EMSer.GetSingle(EMEnt);
                if (EMEnt != null)
                {
                    txtObtainMarks.Text = EMEnt.MARKS;
                    if (EMEnt.REMARKS == "A")
                    {
                        chkAbs.Checked = true;
                    }
                    else if (EMEnt.REMARKS == "E")
                    {
                        chkExpel.Checked = true;
                    }

                }
            }

            #endregion

            if (chkAbs.Checked == true)
            {
                chkExpel.Checked = false;
            }
            else if (chkExpel.Checked == true)
            {
                chkAbs.Checked = false;
            }

        }
    }
    protected void rbtnGrade_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSubject();
    }
    protected void btnShow_Click(object sender, EventArgs e)
    {
        CSUBEnt = new HSS_CURRENT_STUDENT();
        EntityList theList = new EntityList();
        CSUBEnt.SEMESTER = ddlSemester.SelectedValue;
        CSUBEnt.SECTION = ddlsection.SelectedValue;
        CSUBEnt.SUBJ1 = ddlSubject.SelectedValue;
        CSUBEnt.SUBJ2 = ddlSubject.SelectedValue;
        CSUBEnt.SUBJ3 = ddlSubject.SelectedValue;
        CSUBEnt.SUBJ4 = ddlSubject.SelectedValue;
        CSUBEnt.SUBJ5 = ddlSubject.SelectedValue;
        CSUBEnt.SUBJ6 = ddlSubject.SelectedValue;
        CSUBEnt.SUBJ7 = ddlSubject.SelectedValue;
        CSUBEnt.YEAR = hf.NepaliYear();
        CSUBEnt.STATUS = "1";
        theList = CSSer.GetAll(CSUBEnt);
        grdMarksEntry.DataSource = theList;

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
            CheckBox chkAbs = gr.FindControl("chkAbs") as CheckBox;
            CheckBox chkExpel = gr.FindControl("chkExpel") as CheckBox;

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
                }

                ETEnt = new EXAM_TYPE();
                ETEnt.PROGRAM = ddlProgram.SelectedValue;
                ETEnt.EXAM_TYPE_MASTERID = ddlExamType.SelectedValue;
                ETEnt = (EXAM_TYPE)ETSer.GetSingle(ETEnt);
                if (ETEnt != null)
                {


                    EMEnt = new EXAM_MARKS();
                    EMEnt.STUDENT_ID = lblRegNo.Text;
                    EMEnt.SEMESTER = ddlSemester.SelectedValue;
                    EMEnt.EXAM_TYPE = ETEnt.PKID;
                    EMEnt.SUBJECT = ddlSubject.SelectedValue;
                    EMEnt = (EXAM_MARKS)EMSer.GetSingle(EMEnt);
                    if (EMEnt != null)
                    {
                        #region update

                        if (string.IsNullOrEmpty(txtObtainMarks.Text))
                        {
                            txtObtainMarks.Text = "0";
                        }


                        if (chkAbs.Checked == false && chkExpel.Checked == false)
                        {
                            EMEnt.MARKS = txtObtainMarks.Text;
                            if (Convert.ToDouble(txtObtainMarks.Text) < Convert.ToDouble(lblPassMarks.Text))
                            {
                                EMEnt.REMARKS = "F";
                            }
                            else
                            {
                                EMEnt.REMARKS = "";
                            }

                        }
                        else
                        {
                            EMEnt.MARKS = "0";
                            if (chkAbs.Checked == true)
                            {
                                EMEnt.REMARKS = "A";
                            }
                            else
                            {
                                EMEnt.REMARKS = "E";
                            }
                            //EMEnt.REMARKS = rbtnRemarks.SelectedValue;
                        }

                        EMSer.Update(EMEnt, DT);
                    }
                    #endregion

                    else
                    {
                        #region insert


                        EMEnt = new EXAM_MARKS();
                        EMEnt.STUDENT_ID = lblRegNo.Text;
                        EMEnt.SEMESTER = ddlSemester.SelectedValue;
                        EMEnt.EXAM_TYPE = ETEnt.PKID;
                        EMEnt.SUBJECT = ddlSubject.SelectedValue;

                        EMEnt.MARKS = txtObtainMarks.Text;

                        if (chkAbs.Checked == false && chkExpel.Checked == false)
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
                            EMEnt.MARKS = "0";
                            if (chkAbs.Checked == true)
                            {
                                EMEnt.REMARKS = "A";
                            }
                            else
                            {
                                EMEnt.REMARKS = "E";
                            }
                            //EMEnt.REMARKS = rbtnRemarks.SelectedValue;
                        }


                        EMSer.Insert(EMEnt, DT);

                        #endregion
                    }
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
        LoadBatch();
        LoadSubject();
        LoadExamType();
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }


    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }


    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSemester();
    }

    protected void chkAbs_CheckedChanged(object sender, EventArgs e)
    {

        GridViewRow gr = ((CheckBox)sender).Parent.Parent as GridViewRow;

        CheckBox chkAbs = (CheckBox)gr.FindControl("chkAbs");
        CheckBox chkExpel = (CheckBox)gr.FindControl("chkExpel");

        chkExpel.Checked = false;

    }

    protected void chkExpel_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow gr = ((CheckBox)sender).Parent.Parent as GridViewRow;
        CheckBox chkAbs = (CheckBox)gr.FindControl("chkAbs");

        chkAbs.Checked = false;
    }
}