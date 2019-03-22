using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;
using DataHelper.Framework;
using System.Collections;
using Entity.Framework;
using System.IO;
public partial class exam_boardexam_CGPA_Calculator : System.Web.UI.Page
{
    HSS_SUBJECT SUBEnt = new HSS_SUBJECT();
    HSS_SUBJECTService SUBSer = new HSS_SUBJECTService();

    HSS_STUDENT STEnt = new HSS_STUDENT();
    HSS_STUDENTService STSer = new HSS_STUDENTService();

    BatchYear BTEnt = new BatchYear();
    BatchYearService BTSer = new BatchYearService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    SGPA_EIGHT SGEEnt = new SGPA_EIGHT();
    SGPA_EIGHTService SGESer = new SGPA_EIGHTService();

    SGPA_SIXTH SGSEnt = new SGPA_SIXTH();
    SGPA_SIXTHService SGSSer = new SGPA_SIXTHService();

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    GPA_GRADE GPAEnt = new GPA_GRADE();
    GPA_GRADEService GPASrv = new GPA_GRADEService();

    SGPA_EIGHT SGPAEEnt = new SGPA_EIGHT();
    SGPA_EIGHTService SGPAESrv = new SGPA_EIGHTService();

    HelperFunction hf = new HelperFunction();
    String imgfolder;

    EntityList semesterList = new EntityList();
    string semList = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFaculty();
            //LoadBatch();          
        }
        //lblGrandCreditPoints.Text = "0";
        //lblGrandCredits.Text = "0";
        //lblCGPA.Text = "0.00";
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
        ddlBatch.Items.Insert(0, "Select");

        ddlStudent.Items.Insert(0, "Select");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        lblGrandCreditPoints.Text = "0";
        lblGrandCredits.Text = "0";
        lblCGPA.Text = "0.00";
        LoadData();
    }

    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadStudent();
    }

    protected void LoadStudent()
    {
        if (ddlBatch.SelectedValue != "Select")
        {
            ddlStudent.DataSource = hf.getStudentInfo(ddlBatch.SelectedValue, "");
            ddlStudent.DataTextField = "name";
            ddlStudent.DataValueField = "STUDENT_ID";
            ddlStudent.DataBind();
            ddlStudent.Items.Insert(0, "Select");
        }
        else
        {
            //gridCalculator.DataSource = null;
            //  gridCalculator.DataBind();
        }
    }

    //protected void ddlGrade_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string[] xyz = ddlBatch.SelectedValue.Split('-');
    //    lblBatch.Text = xyz[2];

    //    GridViewRow gr = ((DropDownList)sender).Parent.Parent as GridViewRow;

    //    Label lblCredit = (Label)gr.FindControl("lblCredit");
    //    Label lblPoints = (Label)gr.FindControl("lblPoints");
    //    Label lblCreditPoints = (Label)gr.FindControl("lblCreditPoints");
    //    DropDownList ddlGrade = (DropDownList)gr.FindControl("ddlGrade");

    //    //if (ddlGrade.SelectedValue == "A+")
    //    //{
    //    //    lblPoints.Text = "4";
    //    //}
    //    //else if (ddlGrade.SelectedValue == "A" && (Convert.ToDouble(lblBatch.Text) < 15))
    //    //{
    //    //    lblPoints.Text = "4";
    //    //}
    //    //else if (ddlGrade.SelectedValue == "A" && (Convert.ToDouble(lblBatch.Text) >= 15))
    //    //{
    //    //    lblPoints.Text = "3.75";
    //    //}
    //    //else if (ddlGrade.SelectedValue == "B+")
    //    //{
    //    //    lblPoints.Text = "3.50";
    //    //}
    //    //else if (ddlGrade.SelectedValue == "B")
    //    //{
    //    //    lblPoints.Text = "3";
    //    //}

    //    //else if (ddlGrade.SelectedValue == "C" && (Convert.ToDouble(lblBatch.Text) < 15))
    //    //{
    //    //    lblPoints.Text = "2";
    //    //}
    //    //else if (ddlGrade.SelectedValue == "C" && (Convert.ToDouble(lblBatch.Text) >= 15))
    //    //{
    //    //    lblPoints.Text = "2.50";
    //    //}
    //    //else if (ddlGrade.SelectedValue == "D" && (Convert.ToDouble(lblBatch.Text) < 15))
    //    //{
    //    //    lblPoints.Text = "1";
    //    //}

    //    //else if (ddlGrade.SelectedValue == "D" && (Convert.ToDouble(lblBatch.Text) >= 15))
    //    //{
    //    //    lblPoints.Text = "1.75";
    //    //}
    //    //else
    //    //{
    //    //    lblPoints.Text = "0";
    //    //}

    //    lblCreditPoints.Text = (Convert.ToDouble(lblCredit.Text) * Convert.ToDouble(lblPoints.Text)).ToString();
    //    CalculateTotalFirst();
    //    CalculateTotalSecond();
    //    CalculateTotalThird();
    //    CalculateTotalFouth();
    //    CalculateTotalFifth();
    //    CalculateTotalSixth();
    //    CalculateTotalSeventh();
    //    CalculateTotalEight();


    //}

    protected void CalculateTotalFirst()
    {
        double totalcredit = 0;
        double totalpoints = 0;
        double totalcreditpoints = 0;

        GridViewRow row = gridFirst.FooterRow;

        Label lblTotalCredit = row.FindControl("lblTotalCredit") as Label;
        Label lblTotalpoints = row.FindControl("lblTotalpoints") as Label;
        Label lblTotalCreditpoints = row.FindControl("lblTotalCreditpoints") as Label;

        foreach (GridViewRow gr in gridFirst.Rows)
        {
            Label lblCredit = gr.FindControl("lblCredit") as Label;
            TextBox txtPoint = gr.FindControl("txtPoint") as TextBox;
            Label lblCreditPoints = gr.FindControl("lblCreditPoints") as Label;
            Label lblSubjectId = gr.FindControl("lblSubjectId") as Label;
            Label lblGrade = gr.FindControl("lblGrade") as Label;

            totalcredit = totalcredit + Convert.ToDouble(lblCredit.Text);
            totalpoints = totalpoints + Convert.ToDouble(txtPoint.Text);
            totalcreditpoints = totalcreditpoints + Convert.ToDouble(lblCreditPoints.Text);
        }

        lblTotalCredit.Text = totalcredit.ToString();
        lblTotalpoints.Text = totalpoints.ToString();
        lblTotalCreditpoints.Text = totalcreditpoints.ToString();


        lblTotalSGPAFirst.Text = (totalcreditpoints / totalcredit).ToString("#0.00");

        lblGrandCreditPoints.Text = (Convert.ToDouble(lblGrandCreditPoints.Text) + totalcreditpoints).ToString();
        lblGrandCredits.Text = (Convert.ToDouble(lblGrandCredits.Text) + totalcredit).ToString();

        lblCGPA.Text = (Convert.ToDouble(lblGrandCreditPoints.Text) / (Convert.ToDouble(lblGrandCredits.Text))).ToString("0.00");

    }

    protected void CalculateTotalSecond()
    {
        double totalcredit = 0;
        double totalpoints = 0;
        double totalcreditpoints = 0;

        GridViewRow row = gridSecond.FooterRow;

        Label lblTotalCredit = row.FindControl("lblTotalCredit") as Label;
        Label lblTotalpoints = row.FindControl("lblTotalpoints") as Label;
        Label lblTotalCreditpoints = row.FindControl("lblTotalCreditpoints") as Label;

        foreach (GridViewRow gr in gridSecond.Rows)
        {
            Label lblCredit = gr.FindControl("lblCredit") as Label;
            TextBox txtPoint = gr.FindControl("txtPoint") as TextBox;
            Label lblCreditPoints = gr.FindControl("lblCreditPoints") as Label;
            Label lblSubjectId = gr.FindControl("lblSubjectId") as Label;
            Label lblGrade = gr.FindControl("lblGrade") as Label;

            if (ddlBatch.SelectedValue != "Select" && ddlStudent.SelectedValue != "Select")
            {
                BTEnt = new BatchYear();
                BTEnt.BATCH = ddlBatch.SelectedValue;
                BTEnt.PROGRAM = ddlProgram.SelectedValue;
                BTEnt.ACTIVE = "1";
                BTEnt = (BatchYear)BTSer.GetSingle(BTEnt);
                if (BTEnt != null)
                {
                    SMEnt = new semester();
                    SMEnt.SYLLABUS_YEAR = BTEnt.SYLLABUS_YEAR;
                    SMEnt.PROGRAM_ID = BTEnt.PROGRAM;
                    semesterList = SMSer.GetAll(SMEnt);
                }

                foreach (semester sem in semesterList)
                {
                    semList += sem.PK_ID + ",";
                }

                semList = semList.Substring(0, semList.Length - 1);
                string[] semester = semList.Split(',');

                SGPAEEnt = new SGPA_EIGHT();
                SGPAEEnt.BATCH = ddlBatch.SelectedValue;
                SGPAEEnt.SEMESTER = semester[1];
                SGPAEEnt.STUDENT_ID = ddlStudent.SelectedValue;
                SGPAEEnt.SUBJECT_ID = lblSubjectId.Text;
                SGPAEEnt = (SGPA_EIGHT)SGPAESrv.GetSingle(SGPAEEnt);
                if (SGPAEEnt != null)
                {
                    txtPoint.Text = SGPAEEnt.POINTS;
                    lblCreditPoints.Text = SGPAEEnt.CREDIT_POINTS;
                    lblGrade.Text = SGPAEEnt.GRADE;

                }



                totalcredit = totalcredit + Convert.ToDouble(lblCredit.Text);
                totalpoints = totalpoints + Convert.ToDouble(txtPoint.Text);
                totalcreditpoints = totalcreditpoints + Convert.ToDouble(lblCreditPoints.Text);


            }


            lblTotalCredit.Text = totalcredit.ToString();
            lblTotalpoints.Text = totalpoints.ToString();
            lblTotalCreditpoints.Text = totalcreditpoints.ToString();


            lblTotalSGPASecond.Text = (totalcreditpoints / totalcredit).ToString("#0.00");

            lblGrandCreditPoints.Text = (Convert.ToDouble(lblGrandCreditPoints.Text) + totalcreditpoints).ToString();
            lblGrandCredits.Text = (Convert.ToDouble(lblGrandCredits.Text) + totalcredit).ToString();


            lblCGPA.Text = (Convert.ToDouble(lblGrandCreditPoints.Text) / (Convert.ToDouble(lblGrandCredits.Text))).ToString("0.00");

        }
    }

    protected void CalculateTotalThird()
    {
        double totalcredit = 0;
        double totalpoints = 0;
        double totalcreditpoints = 0;

        GridViewRow row = gridThird.FooterRow;

        Label lblTotalCredit = row.FindControl("lblTotalCredit") as Label;
        Label lblTotalpoints = row.FindControl("lblTotalpoints") as Label;
        Label lblTotalCreditpoints = row.FindControl("lblTotalCreditpoints") as Label;

        foreach (GridViewRow gr in gridThird.Rows)
        {
            Label lblCredit = gr.FindControl("lblCredit") as Label;
            TextBox txtPoint = gr.FindControl("txtPoint") as TextBox;
            Label lblCreditPoints = gr.FindControl("lblCreditPoints") as Label;
            Label lblSubjectId = gr.FindControl("lblSubjectId") as Label;
            Label lblGrade = gr.FindControl("lblGrade") as Label;


            if (ddlBatch.SelectedValue != "Select" && ddlStudent.SelectedValue != "Select")
            {
                BTEnt = new BatchYear();
                BTEnt.BATCH = ddlBatch.SelectedValue;
                BTEnt.PROGRAM = ddlProgram.SelectedValue;
                BTEnt.ACTIVE = "1";
                BTEnt = (BatchYear)BTSer.GetSingle(BTEnt);
                if (BTEnt != null)
                {
                    SMEnt = new semester();
                    SMEnt.SYLLABUS_YEAR = BTEnt.SYLLABUS_YEAR;
                    SMEnt.PROGRAM_ID = BTEnt.PROGRAM;
                    semesterList = SMSer.GetAll(SMEnt);
                }

                foreach (semester sem in semesterList)
                {
                    semList += sem.PK_ID + ",";
                }

                semList = semList.Substring(0, semList.Length - 1);
                string[] semester = semList.Split(',');

                SGPAEEnt = new SGPA_EIGHT();
                SGPAEEnt.BATCH = ddlBatch.SelectedValue;
                SGPAEEnt.SEMESTER = semester[2];
                SGPAEEnt.STUDENT_ID = ddlStudent.SelectedValue;
                SGPAEEnt.SUBJECT_ID = lblSubjectId.Text;
                SGPAEEnt = (SGPA_EIGHT)SGPAESrv.GetSingle(SGPAEEnt);
                if (SGPAEEnt != null)
                {
                    txtPoint.Text = SGPAEEnt.POINTS;
                    lblCreditPoints.Text = SGPAEEnt.CREDIT_POINTS;
                    lblGrade.Text = SGPAEEnt.GRADE;
                }

                totalcredit = totalcredit + Convert.ToDouble(lblCredit.Text);


                try
                {

                    Double GPA = Convert.ToDouble(txtPoint.Text);

                    if (GPA > 4 || GPA < 0)
                    {
                        HelperFunction.MsgBox(this, this.GetType(), "The Point must not be more than 4 or less than 0.");
                        txtPoint.Focus();
                    }
                    else
                    {

                        totalpoints = totalpoints + Convert.ToDouble(txtPoint.Text);
                        totalcreditpoints = totalcreditpoints + Convert.ToDouble(lblCreditPoints.Text);
                    }
                }
                catch
                {
                    HelperFunction.MsgBox(this, this.GetType(), "The Point must not be more than 4 or less than 0.");
                    txtPoint.Focus();
                }


            }

        }


        lblTotalCredit.Text = totalcredit.ToString();
        lblTotalpoints.Text = totalpoints.ToString();
        lblTotalCreditpoints.Text = totalcreditpoints.ToString();


        lblTotalSGPAThird.Text = (totalcreditpoints / totalcredit).ToString("#0.00");

        lblGrandCreditPoints.Text = (Convert.ToDouble(lblGrandCreditPoints.Text) + totalcreditpoints).ToString();
        lblGrandCredits.Text = (Convert.ToDouble(lblGrandCredits.Text) + totalcredit).ToString();

        lblCGPA.Text = (Convert.ToDouble(lblGrandCreditPoints.Text) / (Convert.ToDouble(lblGrandCredits.Text))).ToString("0.00");

    }

    protected void CalculateTotalFouth()
    {
        double totalcredit = 0;
        double totalpoints = 0;
        double totalcreditpoints = 0;

        GridViewRow row = gridFouth.FooterRow;

        Label lblTotalCredit = row.FindControl("lblTotalCredit") as Label;
        Label lblTotalpoints = row.FindControl("lblTotalpoints") as Label;
        Label lblTotalCreditpoints = row.FindControl("lblTotalCreditpoints") as Label;

        foreach (GridViewRow gr in gridFouth.Rows)
        {
            Label lblCredit = gr.FindControl("lblCredit") as Label;
            TextBox txtPoint = gr.FindControl("txtPoint") as TextBox;
            Label lblCreditPoints = gr.FindControl("lblCreditPoints") as Label;

            Label lblSubjectId = gr.FindControl("lblSubjectId") as Label;
            Label lblGrade = gr.FindControl("lblGrade") as Label;


            if (ddlBatch.SelectedValue != "Select" && ddlStudent.SelectedValue != "Select")
            {
                BTEnt = new BatchYear();
                BTEnt.BATCH = ddlBatch.SelectedValue;
                BTEnt.PROGRAM = ddlProgram.SelectedValue;
                BTEnt.ACTIVE = "1";
                BTEnt = (BatchYear)BTSer.GetSingle(BTEnt);
                if (BTEnt != null)
                {
                    SMEnt = new semester();
                    SMEnt.SYLLABUS_YEAR = BTEnt.SYLLABUS_YEAR;
                    SMEnt.PROGRAM_ID = BTEnt.PROGRAM;
                    semesterList = SMSer.GetAll(SMEnt);
                }

                foreach (semester sem in semesterList)
                {
                    semList += sem.PK_ID + ",";
                }

                semList = semList.Substring(0, semList.Length - 1);
                string[] semester = semList.Split(',');

                SGPAEEnt = new SGPA_EIGHT();
                SGPAEEnt.BATCH = ddlBatch.SelectedValue;
                SGPAEEnt.SEMESTER = semester[3];
                SGPAEEnt.STUDENT_ID = ddlStudent.SelectedValue;
                SGPAEEnt.SUBJECT_ID = lblSubjectId.Text;
                SGPAEEnt = (SGPA_EIGHT)SGPAESrv.GetSingle(SGPAEEnt);
                if (SGPAEEnt != null)
                {
                    txtPoint.Text = SGPAEEnt.POINTS;
                    lblCreditPoints.Text = SGPAEEnt.CREDIT_POINTS;
                    lblGrade.Text = SGPAEEnt.GRADE;

                }

                totalcredit = totalcredit + Convert.ToDouble(lblCredit.Text);
                totalpoints = totalpoints + Convert.ToDouble(txtPoint.Text);
                totalcreditpoints = totalcreditpoints + Convert.ToDouble(lblCreditPoints.Text);

            }


        }


        lblTotalCredit.Text = totalcredit.ToString();
        lblTotalpoints.Text = totalpoints.ToString();
        lblTotalCreditpoints.Text = totalcreditpoints.ToString();


        lblTotalSGPAFouth.Text = (totalcreditpoints / totalcredit).ToString("#0.00");

        lblGrandCreditPoints.Text = (Convert.ToDouble(lblGrandCreditPoints.Text) + totalcreditpoints).ToString();
        lblGrandCredits.Text = (Convert.ToDouble(lblGrandCredits.Text) + totalcredit).ToString();

        lblCGPA.Text = (Convert.ToDouble(lblGrandCreditPoints.Text) / (Convert.ToDouble(lblGrandCredits.Text))).ToString("0.00");

    }

    protected void CalculateTotalFifth()
    {
        double totalcredit = 0;
        double totalpoints = 0;
        double totalcreditpoints = 0;

        GridViewRow row = gridFifth.FooterRow;

        Label lblTotalCredit = row.FindControl("lblTotalCredit") as Label;
        Label lblTotalpoints = row.FindControl("lblTotalpoints") as Label;
        Label lblTotalCreditpoints = row.FindControl("lblTotalCreditpoints") as Label;

        foreach (GridViewRow gr in gridFifth.Rows)
        {
            Label lblCredit = gr.FindControl("lblCredit") as Label;
            TextBox txtPoint = gr.FindControl("txtPoint") as TextBox;
            Label lblCreditPoints = gr.FindControl("lblCreditPoints") as Label;

            Label lblSubjectId = gr.FindControl("lblSubjectId") as Label;
            Label lblGrade = gr.FindControl("lblGrade") as Label;


            if (ddlBatch.SelectedValue != "Select" && ddlStudent.SelectedValue != "Select")
            {
                BTEnt = new BatchYear();
                BTEnt.BATCH = ddlBatch.SelectedValue;
                BTEnt.PROGRAM = ddlProgram.SelectedValue;
                BTEnt.ACTIVE = "1";
                BTEnt = (BatchYear)BTSer.GetSingle(BTEnt);
                if (BTEnt != null)
                {
                    SMEnt = new semester();
                    SMEnt.SYLLABUS_YEAR = BTEnt.SYLLABUS_YEAR;
                    SMEnt.PROGRAM_ID = BTEnt.PROGRAM;
                    semesterList = SMSer.GetAll(SMEnt);
                }

                foreach (semester sem in semesterList)
                {
                    semList += sem.PK_ID + ",";
                }

                semList = semList.Substring(0, semList.Length - 1);
                string[] semester = semList.Split(',');

                SGPAEEnt = new SGPA_EIGHT();
                SGPAEEnt.BATCH = ddlBatch.SelectedValue;
                SGPAEEnt.SEMESTER = semester[4];
                SGPAEEnt.STUDENT_ID = ddlStudent.SelectedValue;
                SGPAEEnt.SUBJECT_ID = lblSubjectId.Text;
                SGPAEEnt = (SGPA_EIGHT)SGPAESrv.GetSingle(SGPAEEnt);
                if (SGPAEEnt != null)
                {
                    txtPoint.Text = SGPAEEnt.POINTS;
                    lblCreditPoints.Text = SGPAEEnt.CREDIT_POINTS;
                    lblGrade.Text = SGPAEEnt.GRADE;

                }

                totalcredit = totalcredit + Convert.ToDouble(lblCredit.Text);
                totalpoints = totalpoints + Convert.ToDouble(txtPoint.Text);
                totalcreditpoints = totalcreditpoints + Convert.ToDouble(lblCreditPoints.Text);

            }


        }


        lblTotalCredit.Text = totalcredit.ToString();
        lblTotalpoints.Text = totalpoints.ToString();
        lblTotalCreditpoints.Text = totalcreditpoints.ToString();


        lblTotalSGPAFifth.Text = (totalcreditpoints / totalcredit).ToString("#0.00");

        lblGrandCreditPoints.Text = (Convert.ToDouble(lblGrandCreditPoints.Text) + totalcreditpoints).ToString();
        lblGrandCredits.Text = (Convert.ToDouble(lblGrandCredits.Text) + totalcredit).ToString();

        lblCGPA.Text = (Convert.ToDouble(lblGrandCreditPoints.Text) / (Convert.ToDouble(lblGrandCredits.Text))).ToString("0.00");

    }

    protected void CalculateTotalSixth()
    {
        double totalcredit = 0;
        double totalpoints = 0;
        double totalcreditpoints = 0;

        GridViewRow row = gridSixth.FooterRow;

        Label lblTotalCredit = row.FindControl("lblTotalCredit") as Label;
        Label lblTotalpoints = row.FindControl("lblTotalpoints") as Label;
        Label lblTotalCreditpoints = row.FindControl("lblTotalCreditpoints") as Label;

        foreach (GridViewRow gr in gridSixth.Rows)
        {
            Label lblCredit = gr.FindControl("lblCredit") as Label;
            TextBox txtPoint = gr.FindControl("txtPoint") as TextBox;
            Label lblCreditPoints = gr.FindControl("lblCreditPoints") as Label;

            Label lblSubjectId = gr.FindControl("lblSubjectId") as Label;
            Label lblGrade = gr.FindControl("lblGrade") as Label;


            if (ddlBatch.SelectedValue != "Select" && ddlStudent.SelectedValue != "Select")
            {
                BTEnt = new BatchYear();
                BTEnt.BATCH = ddlBatch.SelectedValue;
                BTEnt.PROGRAM = ddlProgram.SelectedValue;
                BTEnt.ACTIVE = "1";
                BTEnt = (BatchYear)BTSer.GetSingle(BTEnt);
                if (BTEnt != null)
                {
                    SMEnt = new semester();
                    SMEnt.SYLLABUS_YEAR = BTEnt.SYLLABUS_YEAR;
                    SMEnt.PROGRAM_ID = BTEnt.PROGRAM;
                    semesterList = SMSer.GetAll(SMEnt);
                }

                foreach (semester sem in semesterList)
                {
                    semList += sem.PK_ID + ",";
                }

                semList = semList.Substring(0, semList.Length - 1);
                string[] semester = semList.Split(',');

                SGPAEEnt = new SGPA_EIGHT();
                SGPAEEnt.BATCH = ddlBatch.SelectedValue;
                SGPAEEnt.SEMESTER = semester[5];
                SGPAEEnt.STUDENT_ID = ddlStudent.SelectedValue;
                SGPAEEnt.SUBJECT_ID = lblSubjectId.Text;
                SGPAEEnt = (SGPA_EIGHT)SGPAESrv.GetSingle(SGPAEEnt);
                if (SGPAEEnt != null)
                {
                    txtPoint.Text = SGPAEEnt.POINTS;
                    lblCreditPoints.Text = SGPAEEnt.CREDIT_POINTS;
                    lblGrade.Text = SGPAEEnt.GRADE;

                }

                totalcredit = totalcredit + Convert.ToDouble(lblCredit.Text);
                totalpoints = totalpoints + Convert.ToDouble(txtPoint.Text);
                totalcreditpoints = totalcreditpoints + Convert.ToDouble(lblCreditPoints.Text);

            }

        }
        lblTotalCredit.Text = totalcredit.ToString();
        lblTotalpoints.Text = totalpoints.ToString();
        lblTotalCreditpoints.Text = totalcreditpoints.ToString();


        lblTotalSGPASixth.Text = (totalcreditpoints / totalcredit).ToString("#0.00");


        lblGrandCreditPoints.Text = (Convert.ToDouble(lblGrandCreditPoints.Text) + totalcreditpoints).ToString();
        lblGrandCredits.Text = (Convert.ToDouble(lblGrandCredits.Text) + totalcredit).ToString();

        lblCGPA.Text = (Convert.ToDouble(lblGrandCreditPoints.Text) / (Convert.ToDouble(lblGrandCredits.Text))).ToString("0.00");

    }

    protected void CalculateTotalSeventh()
    {
        double totalcredit = 0;
        double totalpoints = 0;
        double totalcreditpoints = 0;

        GridViewRow row = gridSeventh.FooterRow;

        Label lblTotalCredit = row.FindControl("lblTotalCredit") as Label;
        Label lblTotalpoints = row.FindControl("lblTotalpoints") as Label;
        Label lblTotalCreditpoints = row.FindControl("lblTotalCreditpoints") as Label;

        foreach (GridViewRow gr in gridSeventh.Rows)
        {
            Label lblCredit = gr.FindControl("lblCredit") as Label;
            TextBox txtPoint = gr.FindControl("txtPoint") as TextBox;
            Label lblCreditPoints = gr.FindControl("lblCreditPoints") as Label;

            Label lblSubjectId = gr.FindControl("lblSubjectId") as Label;
            Label lblGrade = gr.FindControl("lblGrade") as Label;


            if (ddlBatch.SelectedValue != "Select" && ddlStudent.SelectedValue != "Select")
            {
                BTEnt = new BatchYear();
                BTEnt.BATCH = ddlBatch.SelectedValue;
                BTEnt.PROGRAM = ddlProgram.SelectedValue;
                BTEnt.ACTIVE = "1";
                BTEnt = (BatchYear)BTSer.GetSingle(BTEnt);
                if (BTEnt != null)
                {
                    SMEnt = new semester();
                    SMEnt.SYLLABUS_YEAR = BTEnt.SYLLABUS_YEAR;
                    SMEnt.PROGRAM_ID = BTEnt.PROGRAM;
                    semesterList = SMSer.GetAll(SMEnt);
                }

                foreach (semester sem in semesterList)
                {
                    semList += sem.PK_ID + ",";
                }

                semList = semList.Substring(0, semList.Length - 1);
                string[] semester = semList.Split(',');

                SGPAEEnt = new SGPA_EIGHT();
                SGPAEEnt.BATCH = ddlBatch.SelectedValue;
                SGPAEEnt.SEMESTER = semester[6];
                SGPAEEnt.STUDENT_ID = ddlStudent.SelectedValue;
                SGPAEEnt.SUBJECT_ID = lblSubjectId.Text;
                SGPAEEnt = (SGPA_EIGHT)SGPAESrv.GetSingle(SGPAEEnt);
                if (SGPAEEnt != null)
                {
                    txtPoint.Text = SGPAEEnt.POINTS;
                    lblCreditPoints.Text = SGPAEEnt.CREDIT_POINTS;
                    lblGrade.Text = SGPAEEnt.GRADE;

                }

                totalcredit = totalcredit + Convert.ToDouble(lblCredit.Text);
                totalpoints = totalpoints + Convert.ToDouble(txtPoint.Text);
                totalcreditpoints = totalcreditpoints + Convert.ToDouble(lblCreditPoints.Text);

            }

        }
        lblTotalCredit.Text = totalcredit.ToString();
        lblTotalpoints.Text = totalpoints.ToString();
        lblTotalCreditpoints.Text = totalcreditpoints.ToString();


        lblTotalSGPASeventh.Text = (totalcreditpoints / totalcredit).ToString("#0.00");



        lblGrandCreditPoints.Text = (Convert.ToDouble(lblGrandCreditPoints.Text) + totalcreditpoints).ToString();
        lblGrandCredits.Text = (Convert.ToDouble(lblGrandCredits.Text) + totalcredit).ToString();

        lblCGPA.Text = (Convert.ToDouble(lblGrandCreditPoints.Text) / (Convert.ToDouble(lblGrandCredits.Text))).ToString("0.00");

    }

    protected void CalculateTotalEight()
    {
        double totalcredit = 0;
        double totalpoints = 0;
        double totalcreditpoints = 0;

        GridViewRow row = gridEight.FooterRow;

        Label lblTotalCredit = row.FindControl("lblTotalCredit") as Label;
        Label lblTotalpoints = row.FindControl("lblTotalpoints") as Label;
        Label lblTotalCreditpoints = row.FindControl("lblTotalCreditpoints") as Label;

        foreach (GridViewRow gr in gridEight.Rows)
        {
            Label lblCredit = gr.FindControl("lblCredit") as Label;
            TextBox txtPoint = gr.FindControl("txtPoint") as TextBox;
            Label lblCreditPoints = gr.FindControl("lblCreditPoints") as Label;

            Label lblSubjectId = gr.FindControl("lblSubjectId") as Label;
            Label lblGrade = gr.FindControl("lblGrade") as Label;


            if (ddlBatch.SelectedValue != "Select" && ddlStudent.SelectedValue != "Select")
            {
                BTEnt = new BatchYear();
                BTEnt.BATCH = ddlBatch.SelectedValue;
                BTEnt.PROGRAM = ddlProgram.SelectedValue;
                BTEnt.ACTIVE = "1";
                BTEnt = (BatchYear)BTSer.GetSingle(BTEnt);
                if (BTEnt != null)
                {
                    SMEnt = new semester();
                    SMEnt.SYLLABUS_YEAR = BTEnt.SYLLABUS_YEAR;
                    SMEnt.PROGRAM_ID = BTEnt.PROGRAM;
                    semesterList = SMSer.GetAll(SMEnt);
                }

                foreach (semester sem in semesterList)
                {
                    semList += sem.PK_ID + ",";
                }

                semList = semList.Substring(0, semList.Length - 1);
                string[] semester = semList.Split(',');

                SGPAEEnt = new SGPA_EIGHT();
                SGPAEEnt.BATCH = ddlBatch.SelectedValue;
                SGPAEEnt.SEMESTER = semester[7];
                SGPAEEnt.STUDENT_ID = ddlStudent.SelectedValue;
                SGPAEEnt.SUBJECT_ID = lblSubjectId.Text;
                SGPAEEnt = (SGPA_EIGHT)SGPAESrv.GetSingle(SGPAEEnt);
                if (SGPAEEnt != null)
                {
                    txtPoint.Text = SGPAEEnt.POINTS;
                    lblCreditPoints.Text = SGPAEEnt.CREDIT_POINTS;
                    lblGrade.Text = SGPAEEnt.GRADE;

                }

                totalcredit = totalcredit + Convert.ToDouble(lblCredit.Text);
                totalpoints = totalpoints + Convert.ToDouble(txtPoint.Text);
                totalcreditpoints = totalcreditpoints + Convert.ToDouble(lblCreditPoints.Text);

            }


        }


        lblTotalCredit.Text = totalcredit.ToString();
        lblTotalpoints.Text = totalpoints.ToString();
        lblTotalCreditpoints.Text = totalcreditpoints.ToString();


        lblTotalSGPAEight.Text = (totalcreditpoints / totalcredit).ToString("#0.00");


        lblGrandCreditPoints.Text = (Convert.ToDouble(lblGrandCreditPoints.Text) + totalcreditpoints).ToString();
        lblGrandCredits.Text = (Convert.ToDouble(lblGrandCredits.Text) + totalcredit).ToString();

        lblCGPA.Text = (Convert.ToDouble(lblGrandCreditPoints.Text) / (Convert.ToDouble(lblGrandCredits.Text))).ToString("0.00");


    }

    protected void LoadData()
    {
        lblBatchP.Text = ddlBatch.SelectedValue;
        lblRegNo.Text = ddlStudent.SelectedValue;

        STEnt = new HSS_STUDENT();
        STEnt.STUDENT_ID = ddlStudent.SelectedValue;
        STEnt = (HSS_STUDENT)STSer.GetSingle(STEnt);
        if (STEnt != null)
        {
            if (!string.IsNullOrEmpty(ddlStudent.SelectedValue))
            {

                imgfolder = Server.MapPath(@"~/images/bachelorstudent/") + ddlStudent.SelectedValue + ".jpg";
                if (File.Exists(imgfolder))
                {
                    imgStudent.ImageUrl = "~/images/bachelorstudent/" + ddlStudent.SelectedValue + ".jpg";

                }
                else
                {
                    if (STEnt.GENDER.Trim() == "M")
                    {
                        imgStudent.ImageUrl = "~/images/user/male.jpeg";
                    }
                    if (STEnt.GENDER.Trim() == "F")
                    {
                        imgStudent.ImageUrl = "~/images/user/female.jpeg";
                    }
                }
            }

            lblFullNameEng.Text = STEnt.NAME_ENGLISH;
        }

        semesterList = new EntityList();

        string[] xyz = ddlBatch.SelectedValue.Split('-');
        lblBatch.Text = xyz[2];


        if (ddlBatch.SelectedValue != "Select" && ddlStudent.SelectedValue != "Select")
        {
            BTEnt = new BatchYear();
            BTEnt.BATCH = ddlBatch.SelectedValue;
            BTEnt.PROGRAM = ddlProgram.SelectedValue;
            BTEnt.ACTIVE = "1";
            BTEnt = (BatchYear)BTSer.GetSingle(BTEnt);
            if (BTEnt != null)
            {
                SMEnt = new semester();
                SMEnt.SYLLABUS_YEAR = BTEnt.SYLLABUS_YEAR;
                SMEnt.PROGRAM_ID = BTEnt.PROGRAM;
                semesterList = SMSer.GetAll(SMEnt);
            }

            foreach (semester sem in semesterList)
            {
                semList += sem.PK_ID + ",";
            }

            semList = semList.Substring(0, semList.Length - 1);
            string[] semester = semList.Split(',');


            BTEnt = new BatchYear();
            BTEnt.BATCH = ddlBatch.SelectedValue;
            BTEnt.PROGRAM = ddlProgram.SelectedValue;
            BTEnt = (BatchYear)BTSer.GetSingle(BTEnt);
            if (BTEnt != null)
            {
                SUBEnt = new HSS_SUBJECT();
                SUBEnt.SEMESTER = semester[0];
                SUBEnt.YEAR = BTEnt.SYLLABUS_YEAR;
                SUBEnt.STATUS = "1";
                gridFirst.DataSource = SUBSer.GetAll(SUBEnt);
                gridFirst.DataBind();

                CalculateTotalFirst();
                tr_totalsgpafirst.Visible = true;

                hide.Visible = true;
            }


            BTEnt = new BatchYear();
            BTEnt.BATCH = ddlBatch.SelectedValue;
            BTEnt = (BatchYear)BTSer.GetSingle(BTEnt);
            if (BTEnt != null)
            {
                SUBEnt = new HSS_SUBJECT();
                SUBEnt.SEMESTER = semester[1];
                SUBEnt.YEAR = BTEnt.SYLLABUS_YEAR;
                SUBEnt.STATUS = "1";
                gridSecond.DataSource = SUBSer.GetAll(SUBEnt);
                gridSecond.DataBind();
                CalculateTotalSecond();
                tr_totalsgpasecond.Visible = true;

            }

            BTEnt = new BatchYear();
            BTEnt.BATCH = ddlBatch.SelectedValue;
            BTEnt = (BatchYear)BTSer.GetSingle(BTEnt);
            if (BTEnt != null)
            {
                SUBEnt = new HSS_SUBJECT();
                SUBEnt.SEMESTER = semester[2];
                SUBEnt.YEAR = BTEnt.SYLLABUS_YEAR;
                SUBEnt.STATUS = "1";
                gridThird.DataSource = SUBSer.GetAll(SUBEnt);
                gridThird.DataBind();
                CalculateTotalThird();
                tr_totalsgpathird.Visible = true;
            }

            BTEnt = new BatchYear();
            BTEnt.BATCH = ddlBatch.SelectedValue;
            BTEnt = (BatchYear)BTSer.GetSingle(BTEnt);
            if (BTEnt != null)
            {
                SUBEnt = new HSS_SUBJECT();
                SUBEnt.SEMESTER = semester[3];
                SUBEnt.YEAR = BTEnt.SYLLABUS_YEAR;
                SUBEnt.STATUS = "1";
                gridFouth.DataSource = SUBSer.GetAll(SUBEnt);
                gridFouth.DataBind();
                CalculateTotalFouth();
                tr_totalsgpafouth.Visible = true;

            }

            BTEnt = new BatchYear();
            BTEnt.BATCH = ddlBatch.SelectedValue;
            BTEnt = (BatchYear)BTSer.GetSingle(BTEnt);
            if (BTEnt != null)
            {
                SUBEnt = new HSS_SUBJECT();
                SUBEnt.SEMESTER = semester[4];
                SUBEnt.YEAR = BTEnt.SYLLABUS_YEAR;
                SUBEnt.STATUS = "1";
                gridFifth.DataSource = SUBSer.GetAll(SUBEnt);
                gridFifth.DataBind();
                CalculateTotalFifth();
                tr_totalsgpafifth.Visible = true;

            }

            BTEnt = new BatchYear();
            BTEnt.BATCH = ddlBatch.SelectedValue;
            BTEnt = (BatchYear)BTSer.GetSingle(BTEnt);
            if (BTEnt != null)
            {
                SUBEnt = new HSS_SUBJECT();
                SUBEnt.SEMESTER = semester[5];
                SUBEnt.YEAR = BTEnt.SYLLABUS_YEAR;
                SUBEnt.STATUS = "1";
                gridSixth.DataSource = SUBSer.GetAll(SUBEnt);
                gridSixth.DataBind();
                CalculateTotalSixth();
                tr_totalsgpasixth.Visible = true;

            }

            //if (Convert.ToDouble(lblBatch.Text) >= 15)
            //{
            BTEnt = new BatchYear();
            BTEnt.BATCH = ddlBatch.SelectedValue;
            BTEnt = (BatchYear)BTSer.GetSingle(BTEnt);
            if (BTEnt != null)
            {
                SUBEnt = new HSS_SUBJECT();
                SUBEnt.SEMESTER = semester[6];
                SUBEnt.YEAR = BTEnt.SYLLABUS_YEAR;
                SUBEnt.STATUS = "1";
                gridSeventh.DataSource = SUBSer.GetAll(SUBEnt);
                gridSeventh.DataBind();
                CalculateTotalSeventh();
                tr_totalsgpaseventh.Visible = true;

            }

            BTEnt = new BatchYear();
            BTEnt.BATCH = ddlBatch.SelectedValue;
            BTEnt = (BatchYear)BTSer.GetSingle(BTEnt);
            if (BTEnt != null)
            {
                SUBEnt = new HSS_SUBJECT();
                SUBEnt.SEMESTER = semester[7];
                SUBEnt.YEAR = BTEnt.SYLLABUS_YEAR;
                SUBEnt.STATUS = "1";
                gridEight.DataSource = SUBSer.GetAll(SUBEnt);
                gridEight.DataBind();
                CalculateTotalEight();
                tr_totalsgpaeight.Visible = true;

            }
            //}

        }
        else
        {
            HelperFunction.MsgBox(this, this.GetType(), "Please Select Student");
            //gridFirst.DataSource = null;
            //gridFirst.DataBind();
            tr_totalsgpafirst.Visible = false;
            tr_totalsgpasecond.Visible = false;
            tr_totalsgpathird.Visible = false;
            tr_totalsgpafouth.Visible = false;
            tr_totalsgpafifth.Visible = false;
            tr_totalsgpasixth.Visible = false;
            tr_totalsgpaseventh.Visible = false;
            tr_totalsgpaeight.Visible = false;
            hide.Visible = false;
        }
    }

    protected void gridFirst_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string[] xyz = ddlBatch.SelectedValue.Split('-');
        lblBatch.Text = xyz[2];

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblCreditPoints = e.Row.FindControl("lblCreditPoints") as Label;
            Label lblSubjectId = e.Row.FindControl("lblSubjectId") as Label;
            Label lblGrade = e.Row.FindControl("lblGrade") as Label;
            TextBox txtPoint = e.Row.FindControl("txtPoint") as TextBox;


            string[] semester = semList.Split(',');


            if (Convert.ToDouble(lblBatch.Text) < 15)
            {


                SGSEnt = new SGPA_SIXTH();
                SGSEnt.BATCH = ddlBatch.SelectedValue;
                SGSEnt.SEMESTER = semester[0];
                SGSEnt.STUDENT_ID = ddlStudent.SelectedValue;
                SGSEnt.SUBJECT_ID = lblSubjectId.Text;
                SGSEnt = (SGPA_SIXTH)SGSSer.GetSingle(SGSEnt);
                if (SGSEnt != null)
                {
                    lblGrade.Text = SGSEnt.GRADE;
                    txtPoint.Text = SGSEnt.POINTS;
                    lblCreditPoints.Text = SGSEnt.CREDIT_POINTS;

                }

            }
            else
            {
                SGEEnt = new SGPA_EIGHT();
                SGEEnt.BATCH = ddlBatch.SelectedValue;
                SGEEnt.SEMESTER = semester[0];
                SGEEnt.STUDENT_ID = ddlStudent.SelectedValue;
                SGEEnt.SUBJECT_ID = lblSubjectId.Text;
                SGEEnt = (SGPA_EIGHT)SGESer.GetSingle(SGEEnt);
                if (SGEEnt != null)
                {
                    lblGrade.Text = SGEEnt.GRADE;
                    txtPoint.Text = SGEEnt.POINTS;
                    lblCreditPoints.Text = SGEEnt.CREDIT_POINTS;

                }
            }
        }
    }
    protected void gridSecond_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string[] xyz = ddlBatch.SelectedValue.Split('-');
        lblBatch.Text = xyz[2];


        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblCreditPoints = e.Row.FindControl("lblCreditPoints") as Label;
            Label lblSubjectId = e.Row.FindControl("lblSubjectId") as Label;
            Label lblGrade = e.Row.FindControl("lblGrade") as Label;
            TextBox txtPoint = e.Row.FindControl("txtPoint") as TextBox;


            string[] semester = semList.Split(',');

            if (Convert.ToDouble(lblBatch.Text) < 15)
            {


                SGSEnt = new SGPA_SIXTH();
                SGSEnt.BATCH = ddlBatch.SelectedValue;
                SGSEnt.SEMESTER = semester[1];
                SGSEnt.STUDENT_ID = ddlStudent.SelectedValue;
                SGSEnt.SUBJECT_ID = lblSubjectId.Text;
                SGSEnt = (SGPA_SIXTH)SGSSer.GetSingle(SGSEnt);
                if (SGSEnt != null)
                {
                    lblGrade.Text = SGSEnt.GRADE;
                    txtPoint.Text = SGSEnt.POINTS;
                    lblCreditPoints.Text = SGSEnt.CREDIT_POINTS;

                }


            }
            else
            {
                SGEEnt = new SGPA_EIGHT();
                SGEEnt.BATCH = ddlBatch.SelectedValue;
                SGEEnt.SEMESTER = semester[1];
                SGEEnt.STUDENT_ID = ddlStudent.SelectedValue;
                SGEEnt.SUBJECT_ID = lblSubjectId.Text;
                SGEEnt = (SGPA_EIGHT)SGESer.GetSingle(SGEEnt);
                if (SGEEnt != null)
                {
                    lblGrade.Text = SGSEnt.GRADE;
                    txtPoint.Text = SGSEnt.POINTS;
                    lblCreditPoints.Text = SGSEnt.CREDIT_POINTS;
                }

            }
        }
    }
    protected void gridThird_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        string[] xyz = ddlBatch.SelectedValue.Split('-');
        lblBatch.Text = xyz[2];

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblCreditPoints = e.Row.FindControl("lblCreditPoints") as Label;
            Label lblSubjectId = e.Row.FindControl("lblSubjectId") as Label;
            Label lblGrade = e.Row.FindControl("lblGrade") as Label;
            TextBox txtPoint = e.Row.FindControl("txtPoint") as TextBox;

            string[] semester = semList.Split(',');

            if (Convert.ToDouble(lblBatch.Text) < 15)
            {


                SGSEnt = new SGPA_SIXTH();
                SGSEnt.BATCH = ddlBatch.SelectedValue;
                SGSEnt.SEMESTER = semester[2];
                SGSEnt.STUDENT_ID = ddlStudent.SelectedValue;
                SGSEnt.SUBJECT_ID = lblSubjectId.Text;
                SGSEnt = (SGPA_SIXTH)SGSSer.GetSingle(SGSEnt);
                if (SGSEnt != null)
                {
                    lblGrade.Text = SGSEnt.GRADE;
                    txtPoint.Text = SGSEnt.POINTS;
                    lblCreditPoints.Text = SGSEnt.CREDIT_POINTS;

                }


            }
            else
            {
                SGEEnt = new SGPA_EIGHT();
                SGEEnt.BATCH = ddlBatch.SelectedValue;
                SGEEnt.SEMESTER = semester[2];
                SGEEnt.STUDENT_ID = ddlStudent.SelectedValue;
                SGEEnt.SUBJECT_ID = lblSubjectId.Text;
                SGEEnt = (SGPA_EIGHT)SGESer.GetSingle(SGEEnt);
                if (SGEEnt != null)
                {
                    lblGrade.Text = SGSEnt.GRADE;
                    txtPoint.Text = SGSEnt.POINTS;
                    lblCreditPoints.Text = SGSEnt.CREDIT_POINTS;

                }

            }

        }
    }
    protected void gridFouth_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string[] xyz = ddlBatch.SelectedValue.Split('-');
        lblBatch.Text = xyz[2];

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblCreditPoints = e.Row.FindControl("lblCreditPoints") as Label;
            Label lblSubjectId = e.Row.FindControl("lblSubjectId") as Label;
            Label lblGrade = e.Row.FindControl("lblGrade") as Label;
            TextBox txtPoint = e.Row.FindControl("txtPoint") as TextBox;


            string[] semester = semList.Split(',');

            if (Convert.ToDouble(lblBatch.Text) < 15)
            {
                SGSEnt = new SGPA_SIXTH();
                SGSEnt.BATCH = ddlBatch.SelectedValue;
                SGSEnt.SEMESTER = semester[3];
                SGSEnt.STUDENT_ID = ddlStudent.SelectedValue;
                SGSEnt.SUBJECT_ID = lblSubjectId.Text;
                SGSEnt = (SGPA_SIXTH)SGSSer.GetSingle(SGSEnt);
                if (SGSEnt != null)
                {
                    lblGrade.Text = SGSEnt.GRADE;
                    txtPoint.Text = SGSEnt.POINTS;
                    lblCreditPoints.Text = SGSEnt.CREDIT_POINTS;
                }
            }
            else
            {
                SGEEnt = new SGPA_EIGHT();
                SGEEnt.BATCH = ddlBatch.SelectedValue;
                SGEEnt.SEMESTER = semester[3];
                SGEEnt.STUDENT_ID = ddlStudent.SelectedValue;
                SGEEnt.SUBJECT_ID = lblSubjectId.Text;
                SGEEnt = (SGPA_EIGHT)SGESer.GetSingle(SGEEnt);
                if (SGEEnt != null)
                {
                    lblGrade.Text = SGSEnt.GRADE;
                    txtPoint.Text = SGSEnt.POINTS;
                    lblCreditPoints.Text = SGSEnt.CREDIT_POINTS;

                }
            }
        }
    }
    protected void gridFifth_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string[] xyz = ddlBatch.SelectedValue.Split('-');
        lblBatch.Text = xyz[2];

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblCreditPoints = e.Row.FindControl("lblCreditPoints") as Label;
            Label lblSubjectId = e.Row.FindControl("lblSubjectId") as Label;
            Label lblGrade = e.Row.FindControl("lblGrade") as Label;
            TextBox txtPoint = e.Row.FindControl("txtPoint") as TextBox;


            string[] semester = semList.Split(',');

            if (Convert.ToDouble(lblBatch.Text) < 15)
            {


                SGSEnt = new SGPA_SIXTH();
                SGSEnt.BATCH = ddlBatch.SelectedValue;
                SGSEnt.SEMESTER = semester[4];
                SGSEnt.STUDENT_ID = ddlStudent.SelectedValue;
                SGSEnt.SUBJECT_ID = lblSubjectId.Text;
                SGSEnt = (SGPA_SIXTH)SGSSer.GetSingle(SGSEnt);
                if (SGSEnt != null)
                {
                    lblGrade.Text = SGSEnt.GRADE;
                    txtPoint.Text = SGSEnt.POINTS;
                    lblCreditPoints.Text = SGSEnt.CREDIT_POINTS;
                }
            }
            else
            {
                SGEEnt = new SGPA_EIGHT();
                SGEEnt.BATCH = ddlBatch.SelectedValue;
                SGEEnt.SEMESTER = semester[4];
                SGEEnt.STUDENT_ID = ddlStudent.SelectedValue;
                SGEEnt.SUBJECT_ID = lblSubjectId.Text;
                SGEEnt = (SGPA_EIGHT)SGESer.GetSingle(SGEEnt);
                if (SGEEnt != null)
                {
                    lblGrade.Text = SGSEnt.GRADE;
                    txtPoint.Text = SGSEnt.POINTS;
                    lblCreditPoints.Text = SGSEnt.CREDIT_POINTS;
                }
            }
        }
    }
    protected void gridSixth_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string[] xyz = ddlBatch.SelectedValue.Split('-');
        lblBatch.Text = xyz[2];

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblCreditPoints = e.Row.FindControl("lblCreditPoints") as Label;
            Label lblSubjectId = e.Row.FindControl("lblSubjectId") as Label;
            Label lblGrade = e.Row.FindControl("lblGrade") as Label;
            TextBox txtPoint = e.Row.FindControl("txtPoint") as TextBox;

            string[] semester = semList.Split(',');

            if (Convert.ToDouble(lblBatch.Text) < 15)
            {

                SGSEnt = new SGPA_SIXTH();
                SGSEnt.BATCH = ddlBatch.SelectedValue;
                SGSEnt.SEMESTER = semester[5];
                SGSEnt.STUDENT_ID = ddlStudent.SelectedValue;
                SGSEnt.SUBJECT_ID = lblSubjectId.Text;
                SGSEnt = (SGPA_SIXTH)SGSSer.GetSingle(SGSEnt);
                if (SGSEnt != null)
                {
                    lblGrade.Text = SGSEnt.GRADE;
                    txtPoint.Text = SGSEnt.POINTS;
                    lblCreditPoints.Text = SGSEnt.CREDIT_POINTS;

                }
            }
            else
            {
                SGEEnt = new SGPA_EIGHT();
                SGEEnt.BATCH = ddlBatch.SelectedValue;
                SGEEnt.SEMESTER = semester[5];
                SGEEnt.STUDENT_ID = ddlStudent.SelectedValue;
                SGEEnt.SUBJECT_ID = lblSubjectId.Text;
                SGEEnt = (SGPA_EIGHT)SGESer.GetSingle(SGEEnt);
                if (SGEEnt != null)
                {
                    lblGrade.Text = SGSEnt.GRADE;
                    txtPoint.Text = SGSEnt.POINTS;
                    lblCreditPoints.Text = SGEEnt.CREDIT_POINTS;
                }
            }
        }
    }
    protected void gridSeventh_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string[] xyz = ddlBatch.SelectedValue.Split('-');
        lblBatch.Text = xyz[2];

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblCreditPoints = e.Row.FindControl("lblCreditPoints") as Label;
            Label lblSubjectId = e.Row.FindControl("lblSubjectId") as Label;
            Label lblGrade = e.Row.FindControl("lblGrade") as Label;
            TextBox txtPoint = e.Row.FindControl("txtPoint") as TextBox;


            string[] semester = semList.Split(',');

            SGEEnt = new SGPA_EIGHT();
            SGEEnt.BATCH = ddlBatch.SelectedValue;
            SGEEnt.SEMESTER = semester[6];
            SGEEnt.STUDENT_ID = ddlStudent.SelectedValue;
            SGEEnt.SUBJECT_ID = lblSubjectId.Text;
            SGEEnt = (SGPA_EIGHT)SGESer.GetSingle(SGEEnt);
            if (SGEEnt != null)
            {
                lblGrade.Text = SGSEnt.GRADE;
                txtPoint.Text = SGSEnt.POINTS;
                lblCreditPoints.Text = SGSEnt.CREDIT_POINTS;

            }
        }
    }
    protected void gridEight_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string[] xyz = ddlBatch.SelectedValue.Split('-');
        lblBatch.Text = xyz[2];

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            Label lblCreditPoints = e.Row.FindControl("lblCreditPoints") as Label;
            Label lblSubjectId = e.Row.FindControl("lblSubjectId") as Label;
            Label lblGrade = e.Row.FindControl("lblGrade") as Label;
            TextBox txtPoint = e.Row.FindControl("txtPoint") as TextBox;


            string[] semester = semList.Split(',');

            SGEEnt = new SGPA_EIGHT();
            SGEEnt.BATCH = ddlBatch.SelectedValue;
            SGEEnt.SEMESTER = semester[7];
            SGEEnt.STUDENT_ID = ddlStudent.SelectedValue;
            SGEEnt.SUBJECT_ID = lblSubjectId.Text;
            SGEEnt = (SGPA_EIGHT)SGESer.GetSingle(SGEEnt);
            if (SGEEnt != null)
            {
                lblGrade.Text = SGSEnt.GRADE;
                txtPoint.Text = SGSEnt.POINTS;
                lblCreditPoints.Text = SGSEnt.CREDIT_POINTS;
            }
        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "printPartOfPage();", true);
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBatch();
    }

    protected void txtPoint_TextChanged(object sender, EventArgs e)
    {
        GridViewRow gr = ((TextBox)sender).Parent.Parent as GridViewRow;
        TextBox txtPoint = (TextBox)gr.FindControl("txtPoint");
        Label lblGrade = (Label)gr.FindControl("lblGrade");
        Label lblCreditPoints = (Label)gr.FindControl("lblCreditPoints");
        Label lblCredit = (Label)gr.FindControl("lblCredit");

        EntityList theList = new EntityList();

        GPAEnt = new GPA_GRADE();
        GPAEnt.PROGRAM = ddlProgram.SelectedValue;
        theList = GPASrv.GetAll(GPAEnt);
        try
        {
            Double GPA = Convert.ToDouble(txtPoint.Text);

            if (GPA > 4 || GPA < 0)
            {
                HelperFunction.MsgBox(this, this.GetType(), "The Point must not be more than 4 or less than 0.");
                txtPoint.Focus();
            }
            else
            {
                foreach (GPA_GRADE GG in theList)
                {
                    Double GPAMax = Convert.ToDouble(GG.GPA_MAX);
                    Double GPAMin = Convert.ToDouble(GG.GPA_MIN);

                    if ((Convert.ToDouble(GPA) >= Convert.ToDouble(GPAMin)) && (Convert.ToDouble(GPA) <= Convert.ToDouble(GPAMax)))
                    {
                        GPAEnt = (GPA_GRADE)GPASrv.GetSingle(GPAEnt);
                        if (GPAEnt != null)
                        {
                            lblGrade.Text = GG.GRADE;
                            lblCreditPoints.Text = (Convert.ToDouble(lblCredit.Text) * Convert.ToDouble(txtPoint.Text)).ToString();
                        }
                        else
                        {
                            HelperFunction.MsgBox(this, this.GetType(), "Sorry!!! Please Enter correct GPA Point.");
                        }
                    }
                }

            }
        }
        catch
        {
            HelperFunction.MsgBox(this, this.GetType(), "Please Enter correct GPA Point.");
            txtPoint.Focus();
        }

        CalculateTotalFirst();
        CalculateTotalSecond();
        CalculateTotalThird();
        CalculateTotalFouth();
        CalculateTotalFifth();
        CalculateTotalSixth();
        CalculateTotalSeventh();
        CalculateTotalEight();

    }
}


