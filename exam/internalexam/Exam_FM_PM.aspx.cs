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

public partial class exam_internalexam_Exam_FM_PM : System.Web.UI.Page
{

    HSS_LEVEL LVLEnt = new HSS_LEVEL();
    HSS_LEVELService LVLSer = new HSS_LEVELService();

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    SYLLABUS_YEAR SYEnt = new SYLLABUS_YEAR();
    SYLLABUS_YEARService SYSrv = new SYLLABUS_YEARService();

    BatchYear BTEnt = new BatchYear();
    BatchYearService BTSer = new BatchYearService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    EXAM_TYPE ETEnt = new EXAM_TYPE();
    EXAM_TYPEService ETSrv = new EXAM_TYPEService();

    EXAM_TYPE_MASTER ETMEnt = new EXAM_TYPE_MASTER();
    EXAM_TYPE_MASTERService ETMSrv = new EXAM_TYPE_MASTERService();

    HSS_SUBJECT HSEnt = new HSS_SUBJECT();
    HSS_SUBJECTService HSSrv = new HSS_SUBJECTService();

    full_pass_marks FPMEnt = new full_pass_marks();
    full_pass_marksService FPMSrv = new full_pass_marksService();



    EntityList theList = new EntityList();

    HelperFunction hf = new HelperFunction();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            loadLevel();
            LoadFaculty();
            LoadProgram();
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

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }

    protected void Clear()
    {
        //ddlFaculty.SelectedIndex = 0;
        // ddlProgram.SelectedIndex = 0;
        //ddlExamType.SelectedIndex = 0;
        //ddlBatch.SelectedIndex = 0;
        //ddlSemester.SelectedIndex = 0;
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

    protected void LoadSyllabusYr()
    {
        SYEnt = new SYLLABUS_YEAR();
        SYEnt.PROGRAM = ddlProgram.SelectedValue;

        ddlSyllabusYr.DataSource = SYSrv.GetAll(SYEnt);
        ddlSyllabusYr.DataTextField = "YEAR";
        ddlSyllabusYr.DataValueField = "YEAR";
        ddlSyllabusYr.DataBind();
        ddlSyllabusYr.Items.Insert(0, "Select");

    }

    protected void LoadSemester()
    {
        SMEnt = new semester();
        SMEnt.PROGRAM_ID = ddlProgram.SelectedValue;
        SMEnt.SYLLABUS_YEAR = hf.getSyllabusYear(ddlProgram.SelectedValue, ddlSyllabusYr.SelectedValue);

        ddlSemester.DataSource = SMSer.GetAll(SMEnt);
        ddlSemester.DataTextField = "SEMESTER_CODE";
        ddlSemester.DataValueField = "PK_ID";
        ddlSemester.DataBind();
        ddlSemester.Items.Insert(0, "Select");
    }

    protected void LoadExamType()
    {
        ddlExamType.DataSource = hf.getExamType(ddlProgram.SelectedValue);
        ddlExamType.DataTextField = "EXAM_TYPE";
        ddlExamType.DataValueField = "PKID";
        ddlExamType.DataBind();

    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {

        LoadExamType();
        LoadSyllabusYr();
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void gridExamFMPM_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPKID = e.Row.FindControl("lblPKID") as Label;

            TextBox txtFMTheory = e.Row.FindControl("txtFMTheory") as TextBox;
            TextBox txtPMTheory = e.Row.FindControl("txtPMTheory") as TextBox;
            TextBox txtFMPractical = e.Row.FindControl("txtFMPractical") as TextBox;
            TextBox txtPMPractical = e.Row.FindControl("txtPMPractical") as TextBox;

            ETEnt = new EXAM_TYPE();
            ETEnt.EXAM_TYPE_MASTERID = ddlExamType.SelectedValue;
            ETEnt.PROGRAM = ddlProgram.SelectedValue;
            ETEnt = (EXAM_TYPE)ETSrv.GetSingle(ETEnt);
            if (ETEnt != null)
            {
                FPMEnt = new full_pass_marks();
                FPMEnt.EXAM_TYPE = ETEnt.PKID;
                FPMEnt.SUBJECT_ID = lblPKID.Text;
                FPMEnt = (full_pass_marks)FPMSrv.GetSingle(FPMEnt);
                if (FPMEnt != null)
                {
                    txtFMTheory.Text = FPMEnt.FULLMARKS_THRCL;

                    txtFMTheory.Text = FPMEnt.FULLMARKS_THRCL;
                    txtPMTheory.Text = FPMEnt.PASSMARKS_THRCL;
                    txtFMPractical.Text = FPMEnt.FULLMARKS_PRCL;
                    txtPMPractical.Text = FPMEnt.PASSMARKS_PRCL;
                }

            }
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {

        //save detail data


        DistributedTransaction DT = new DistributedTransaction();
        foreach (GridViewRow gd in gridExamFMPM.Rows)
        {
            Label lblPKID = gd.FindControl("lblPKID") as Label;
            TextBox txtFMTheory = gd.FindControl("txtFMTheory") as TextBox;
            TextBox txtPMTheory = gd.FindControl("txtPMTheory") as TextBox;
            TextBox txtFMPractical = gd.FindControl("txtFMPractical") as TextBox;
            TextBox txtPMPractical = gd.FindControl("txtPMPractical") as TextBox;

            if (ddlExamType.SelectedValue != "Select")
            {
                try
                {
                    if (string.IsNullOrEmpty(txtFMTheory.Text))
                    {
                        txtFMTheory.Text = "0";
                    }

                    if (string.IsNullOrEmpty(txtPMTheory.Text))
                    {
                        txtPMTheory.Text = "0";
                    }

                    if (string.IsNullOrEmpty(txtFMPractical.Text))
                    {
                        txtFMPractical.Text = "0";
                    }

                    if (string.IsNullOrEmpty(txtPMPractical.Text))
                    {
                        txtPMPractical.Text = "0";
                    }

                    double ft = Convert.ToDouble(txtFMTheory.Text);
                    if (ft < 0)
                    {
                        DT.HAPPY = false;
                    }

                    double Pt = Convert.ToDouble(txtPMTheory.Text);
                    if (Pt < 0)
                    {
                        DT.HAPPY = false;
                    }

                    double fP = Convert.ToDouble(txtFMPractical.Text);
                    if (fP < 0)
                    {
                        DT.HAPPY = false;
                    }

                    double pp = Convert.ToDouble(txtPMPractical.Text);
                    if (pp < 0)
                    {
                        DT.HAPPY = false;
                    }
                }
                catch
                {
                    DT.HAPPY = false;
                }


                FPMEnt = new full_pass_marks();
                FPMEnt.SUBJECT_ID = lblPKID.Text;
                FPMEnt.EXAM_TYPE = ddlExamType.SelectedValue;
                FPMEnt = (full_pass_marks)FPMSrv.GetSingle(FPMEnt);
                if (FPMEnt != null)
                {
                    FPMEnt.FULLMARKS_THRCL = txtFMTheory.Text;
                    FPMEnt.FULLMARKS_PRCL = txtFMPractical.Text;
                    FPMEnt.PASSMARKS_THRCL = txtPMTheory.Text;
                    FPMEnt.PASSMARKS_PRCL = txtPMPractical.Text;

                    FPMSrv.Update(FPMEnt, DT);
                }
                else
                {
                    FPMEnt = new full_pass_marks();
                    FPMEnt.SUBJECT_ID = lblPKID.Text;
                    FPMEnt.EXAM_TYPE = ddlExamType.SelectedValue;
                    FPMEnt.FULLMARKS_THRCL = txtFMTheory.Text;
                    FPMEnt.FULLMARKS_PRCL = txtFMPractical.Text;
                    FPMEnt.PASSMARKS_THRCL = txtPMTheory.Text;
                    FPMEnt.PASSMARKS_PRCL = txtPMPractical.Text;

                    FPMSrv.Insert(FPMEnt, DT);

                }
            }
            else
            {
                HelperFunction.MsgBox(this, this.GetType(), "Data not saved.Please select exam type");
            }
        }


        if (DT.HAPPY == true)
        {
            DT.Commit();
            HelperFunction.MsgBox(this, this.GetType(), "Data Saved Sucessfully");
            Clear();

        }
        else
        {
            DT.Abort();
            HelperFunction.MsgBox(this, this.GetType(), "There is some thing wrong with data provided. Please Enter Number only.");
        }
    }
    
    protected void btnView_Click(object sender, EventArgs e)
    {

        HSEnt = new HSS_SUBJECT();

        if (ddlProgram.SelectedItem.ToString() != "Select")
        {
            HSEnt.PROGRAM = ddlProgram.SelectedValue;
            HSEnt.SEMESTER = ddlSemester.SelectedValue;
            HSEnt.STATUS = "1";
            HSEnt.YEAR = hf.getSyllabusYear(ddlProgram.SelectedValue, ddlSyllabusYr.SelectedValue);
            gridExamFMPM.DataSource = HSSrv.GetAll(HSEnt);

            gridExamFMPM.DataBind();

            if (gridExamFMPM.Rows.Count == 0)
            {
                ArrayList a1 = new ArrayList();
                HSEnt = new HSS_SUBJECT();
                a1.Add(HSEnt);

                gridExamFMPM.DataSource = a1;
                gridExamFMPM.DataBind();
            }
            foreach (GridViewRow gr in gridExamFMPM.Rows)
            {
                Label lblPKID = gr.FindControl("lblPKID") as Label;
                TextBox txtFMTheory = gr.FindControl("txtFMTheory") as TextBox;
                TextBox txtPMTheory = gr.FindControl("txtPMTheory") as TextBox;
                TextBox txtFMPractical = gr.FindControl("txtFMPractical") as TextBox;
                TextBox txtPMPractical = gr.FindControl("txtPMPractical") as TextBox;

                FPMEnt = new full_pass_marks();
                FPMEnt.EXAM_TYPE = ddlExamType.SelectedValue;
                FPMEnt.SUBJECT_ID = lblPKID.Text;
                FPMEnt = (full_pass_marks)FPMSrv.GetSingle(FPMEnt);
                if (FPMEnt != null)
                {
                    txtFMTheory.Text = FPMEnt.FULLMARKS_THRCL;
                    txtPMTheory.Text = FPMEnt.PASSMARKS_THRCL;

                    txtFMPractical.Text = FPMEnt.FULLMARKS_PRCL;
                    txtPMPractical.Text = FPMEnt.PASSMARKS_PRCL;
                }

            }

        }

        btnAdd.Visible = true;
    }

    protected void ddlSyllabusYr_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSemester();
    }
}
