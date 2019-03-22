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

public partial class exam_boardexam_SGPA_Calculator : System.Web.UI.Page
{
    HSS_SUBJECT SUBEnt = new HSS_SUBJECT();
    HSS_SUBJECTService SUBSer = new HSS_SUBJECTService();

    HSS_STUDENT STEnt = new HSS_STUDENT();
    HSS_STUDENTService STSer = new HSS_STUDENTService();

    HSS_CURRENT_STUDENT CSEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSrv = new HSS_CURRENT_STUDENTService();

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

    HelperFunction hf = new HelperFunction();

    GPA_GRADE GPAEnt = new GPA_GRADE();
    GPA_GRADEService GPASrv = new GPA_GRADEService();

    EntityList mparticularList = new EntityList();

    DistributedTransaction DT = new DistributedTransaction();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFaculty();
            btnSave.Visible = false;
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
        BTEnt.ACTIVE = "1";
        ddlBatch.DataSource = BTSer.GetAll(BTEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
        ddlBatch.Items.Insert(0, "Select");

        ddlStudent.Items.Insert(0, "Select");

    }

    protected void LoadSemester()
    {
        BTEnt = new BatchYear();
        BTEnt.BATCH = ddlBatch.SelectedValue;
        BTEnt = (BatchYear)BTSer.GetSingle(BTEnt);
        if (BTEnt != null)
        {
            SMEnt = new semester();
            SMEnt.PROGRAM_ID = ddlProgram.SelectedValue;
            SMEnt.SYLLABUS_YEAR = BTEnt.SYLLABUS_YEAR;
            ddlSemester.DataSource = SMSer.GetAll(SMEnt);
            ddlSemester.DataTextField = "SEMESTER_CODE";
            ddlSemester.DataValueField = "PK_ID";
            ddlSemester.DataBind();
            ddlSemester.Items.Insert(0, "Select");
        }
    }

    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSemester();
        LoadStudent();
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadStudent();
    }

    protected void LoadStudent()
    {
        if (ddlBatch.SelectedValue != "Select" && ddlSemester.SelectedValue != "Select")
        {
            ddlStudent.DataSource = hf.getStudentInfo(ddlBatch.SelectedValue, "");
            ddlStudent.DataTextField = "name";
            ddlStudent.DataValueField = "STUDENT_ID";
            ddlStudent.DataBind();
            ddlStudent.Items.Insert(0, "Select");
        }
        else
        {
            gridCalculator.DataSource = null;
            gridCalculator.DataBind();
        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        PEnt = new program();
        PEnt.PK_ID = ddlProgram.SelectedValue;
        PEnt.RESULT_TYPE = "GPA";
        PEnt = (program)PSer.GetSingle(PEnt);
        if (PEnt != null)
        {
            GPAGrid.Visible = true;
            PerGrid.Visible = false;
            LoadData();
            btnSave.Visible = true;
        }

        PEnt = new program();
        PEnt.PK_ID = ddlProgram.SelectedValue;
        PEnt.RESULT_TYPE = "Percentage";
        PEnt = (program)PSer.GetSingle(PEnt);
        if (PEnt != null)
        {
            PerGrid.Visible = true;
            GPAGrid.Visible = false;
            LoadData();
            btnSave.Visible = true;
        }
    }

    protected void LoadData()
    {
        PEnt = new program();
        PEnt.PK_ID = ddlProgram.SelectedValue;
        PEnt = (program)PSer.GetSingle(PEnt);
        if (PEnt != null)
        {
            if (PEnt.RESULT_TYPE == "GPA")
            {
                if (ddlBatch.SelectedValue != "Select" && ddlSemester.SelectedValue != "Select" && ddlStudent.SelectedValue != "Select")
                {
                    BTEnt = new BatchYear();
                    BTEnt.BATCH = ddlBatch.SelectedValue;
                    BTEnt = (BatchYear)BTSer.GetSingle(BTEnt);
                    if (BTEnt != null)
                    {
                        SUBEnt = new HSS_SUBJECT();
                        SUBEnt.SEMESTER = ddlSemester.SelectedValue;
                        SUBEnt.YEAR = BTEnt.SYLLABUS_YEAR;
                        
                        gridCalculator.DataSource = SUBSer.GetAll(SUBEnt);
                        gridCalculator.DataBind();
                        CalculateTotal();
                        tr_totalsgpa.Visible = true;
                    }
                }
                else
                {
                    HelperFunction.MsgBox(this, this.GetType(), "Please Select Student");
                    gridCalculator.DataSource = null;
                    gridCalculator.DataBind();
                    tr_totalsgpa.Visible = false;
                }
            }

            if (PEnt.RESULT_TYPE == "Percentage")
            {
                if (ddlBatch.SelectedValue != "Select" && ddlSemester.SelectedValue != "Select" && ddlStudent.SelectedValue != "Select")
                {
                    BTEnt = new BatchYear();
                    BTEnt.BATCH = ddlBatch.SelectedValue;
                    BTEnt = (BatchYear)BTSer.GetSingle(BTEnt);
                    if (BTEnt != null)
                    {
                        SUBEnt = new HSS_SUBJECT();
                        SUBEnt.SEMESTER = ddlSemester.SelectedValue;
                        SUBEnt.YEAR = BTEnt.SYLLABUS_YEAR;


                        gridPercentage.DataSource = SUBSer.GetAll(SUBEnt);
                        gridPercentage.DataBind();
                        CalculateTotal();                       
                    }
                }
                else
                {
                    HelperFunction.MsgBox(this, this.GetType(), "Please Select Student");
                    gridPercentage.DataSource = null;
                    gridPercentage.DataBind();                   
                }
            }

        }
    }
    
    protected void gridCalculator_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string[] xyz = ddlBatch.SelectedValue.Split('-');
        lblBatch.Text = xyz[2];

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            TextBox txtPoint = e.Row.FindControl("txtPoint") as TextBox;
            Label lblCreditPoints = e.Row.FindControl("lblCreditPoints") as Label;
            Label lblSubjectId = e.Row.FindControl("lblSubjectId") as Label;
            Label lblGrade = e.Row.FindControl("lblGrade") as Label;
            DropDownList ddlResult = e.Row.FindControl("ddlResult") as DropDownList;
            CheckBox chkTheory = e.Row.FindControl("chkTheory") as CheckBox;
            CheckBox chkPractical = e.Row.FindControl("chkPractical") as CheckBox;
            
            ArrayList alist = new ArrayList();

            if (ddlResult.SelectedValue == "Pass")
            {
                SGEEnt = new SGPA_EIGHT();
                SGEEnt.BATCH = ddlBatch.SelectedValue;
                SGEEnt.SEMESTER = ddlSemester.SelectedValue;
                SGEEnt.STUDENT_ID = ddlStudent.SelectedValue;
                SGEEnt.SUBJECT_ID = lblSubjectId.Text;
                SGEEnt = (SGPA_EIGHT)SGESer.GetSingle(SGEEnt, DT);
                if (SGEEnt != null)
                {
                    lblGrade.Text = SGEEnt.GRADE;
                    txtPoint.Text = SGEEnt.POINTS;
                    lblCreditPoints.Text = SGEEnt.CREDIT_POINTS;
                    if (lblGrade.Text == "Fail")
                    {
                        ddlResult.SelectedIndex = 1;
                    }
                    else if (lblGrade.Text == "Abs")
                    {
                        ddlResult.SelectedIndex = 2;
                    }
                    else if (lblGrade.Text == "Partial")
                    {
                        ddlResult.SelectedIndex = 3;
                        chkTheory.Visible = true;
                        chkPractical.Visible = true;

                        if (SGEEnt.THEORY == "1")
                        {
                            chkTheory.Checked = true;
                        }
                        if (SGEEnt.PRACTICAL == "1")
                        {
                            chkPractical.Checked = true;
                        }
                    }
                }
            }

            else if (ddlResult.SelectedValue == "Fail")
            {
                txtPoint.Text = "0";
                lblGrade.Text = "Fail";
                lblCreditPoints.Text = "0";
                txtPoint.Enabled = false;

            }
            else if (ddlResult.SelectedValue == "Absent")
            {
                txtPoint.Text = "0";
                lblGrade.Text = "Abs";
                lblCreditPoints.Text = "0";
                txtPoint.Enabled = false;
            }
            else
            {
                txtPoint.Text = "0";
                lblGrade.Text = "Partial";
                lblCreditPoints.Text = "0";
                txtPoint.Enabled = false;
                if (SGEEnt.THEORY == "1")
                {
                    chkTheory.Checked = true;
                }
                if (SGEEnt.PRACTICAL == "1")
                {
                    chkPractical.Checked = true;
                }

            }

        }
    }

    protected void CalculateTotal()
    {
        double totalcredit = 0;
        double totalpoints = 0;
        double totalcreditpoints = 0;

        PEnt = new program();
        PEnt.PK_ID = ddlProgram.SelectedValue;
        PEnt = (program)PSer.GetSingle(PEnt);
        if (PEnt != null)
        {
            #region for GPA

            if (PEnt.RESULT_TYPE == "GPA")
            {
                GridViewRow row = gridCalculator.FooterRow;

                Label lblTotalCredit = row.FindControl("lblTotalCredit") as Label;
                Label lblTotalpoints = row.FindControl("lblTotalpoints") as Label;
                Label lblTotalCreditpoints = row.FindControl("lblTotalCreditpoints") as Label;
                
                foreach (GridViewRow gr in gridCalculator.Rows)
                {
                    Label lblCredit = gr.FindControl("lblCredit") as Label;
                    TextBox txtPoint = gr.FindControl("txtPoint") as TextBox;
                    Label lblCreditPoints = gr.FindControl("lblCreditPoints") as Label;

                    try
                    {
                        Double GPA = Convert.ToDouble(txtPoint.Text);

                        totalcredit = totalcredit + Convert.ToDouble(lblCredit.Text);
                        totalpoints = totalpoints + Convert.ToDouble(GPA);
                        totalcreditpoints = totalcreditpoints + Convert.ToDouble(lblCreditPoints.Text);
                    }
                    catch
                    {
                        HelperFunction.MsgBox(this, this.GetType(), "Please enter correct Points.");
                        txtPoint.Focus();
                    }
                }

                lblTotalCredit.Text = totalcredit.ToString();
                lblTotalpoints.Text = totalpoints.ToString();
                lblTotalCreditpoints.Text = totalcreditpoints.ToString();

                lblTotalSGPA.Text = (totalcreditpoints / totalcredit).ToString("#0.00");
            }

            #endregion

            #region for Percentage

            if (PEnt.RESULT_TYPE == "Percentage")
            {
                GridViewRow row = gridPercentage.FooterRow;

                Label lblTotalCredit = row.FindControl("lblTotalCredit") as Label;
                Label lblTotalpoints = row.FindControl("lblTotalpoints") as Label;
                Label lblTotalCreditpoints = row.FindControl("lblTotalCreditpoints") as Label;

                PEnt = new program();
                PEnt.PK_ID = ddlProgram.SelectedValue;
                PEnt = (program)PSer.GetSingle(PEnt);
                if (PEnt != null)
                {
                    foreach (GridViewRow gr in gridPercentage.Rows)
                    {
                        Label lblCredit = gr.FindControl("lblCredit") as Label;
                        TextBox txtPoint = gr.FindControl("txtPoint") as TextBox;
                        //Label lblCreditPoints = gr.FindControl("lblCreditPoints") as Label;

                        try
                        {
                            Double Percentage = Convert.ToDouble(txtPoint.Text);

                            totalcredit = totalcredit + Convert.ToDouble(lblCredit.Text);
                            totalpoints = totalpoints + Convert.ToDouble(Percentage);
                            // totalcreditpoints = totalcreditpoints + Convert.ToDouble(lblCreditPoints.Text);
                        }
                        catch
                        {
                            HelperFunction.MsgBox(this, this.GetType(), "Please enter correct Points.");
                            txtPoint.Focus();
                        }                       
                    }
                    lblTotalCredit.Text = totalcredit.ToString();
                    lblTotalpoints.Text = totalpoints.ToString();
                }
            }
            #endregion
        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        string[] xyz = ddlBatch.SelectedValue.Split('-');
        lblBatch.Text = xyz[2];

        PEnt = new program();
        PEnt.PK_ID = ddlProgram.SelectedValue;
        PEnt = (program)PSer.GetSingle(PEnt);
        if (PEnt != null)
        {
            #region for GPA
            if (PEnt.RESULT_TYPE == "GPA")
            {

                DT = new DistributedTransaction();
                if (ddlStudent.SelectedValue == "Select")
                {
                    HelperFunction.MsgBox(this, this.GetType(), "Please Select Student");
                }
                else if (gridCalculator.Rows.Count == 0)
                {
                    HelperFunction.MsgBox(this, this.GetType(), "Please Click View Button to load grid before Save");
                }
                else
                {
                    foreach (GridViewRow gr in gridCalculator.Rows)
                    {
                        Label lblSubjectId = gr.FindControl("lblSubjectId") as Label;
                        Label lblCredit = gr.FindControl("lblCredit") as Label;
                        Label lblGrade = gr.FindControl("lblGrade") as Label;
                        TextBox txtPoint = gr.FindControl("txtPoint") as TextBox;
                        Label lblCreditPoints = gr.FindControl("lblCreditPoints") as Label;
                        DropDownList ddlResult = gr.FindControl("ddlResult") as DropDownList;
                        CheckBox chkTheory = gr.FindControl("chkTheory") as CheckBox;
                        CheckBox chkPractical = gr.FindControl("chkPractical") as CheckBox;


                        SGEEnt = new SGPA_EIGHT();
                        SGEEnt.PROGRAM = ddlProgram.SelectedValue;
                        SGEEnt.BATCH = ddlBatch.SelectedValue;
                        SGEEnt.SEMESTER = ddlSemester.SelectedValue;
                        SGEEnt.STUDENT_ID = ddlStudent.SelectedValue;
                        SGEEnt.SUBJECT_ID = lblSubjectId.Text;
                        SGEEnt = (SGPA_EIGHT)SGESer.GetSingle(SGEEnt, DT);
                        if (SGEEnt != null)
                        {
                            SGEEnt.SUBJECT_ID = lblSubjectId.Text;
                            SGEEnt.CREDIT = lblCredit.Text;
                            SGEEnt.GRADE = lblGrade.Text;

                            if (ddlResult.SelectedValue == "Partial")
                            {

                                if (chkTheory.Checked == true)
                                {
                                    SGEEnt.THEORY = "1";
                                    SGEEnt.PRACTICAL = "0";

                                }
                                else if (chkPractical.Text == "Practical")
                                {
                                    SGEEnt.THEORY = "0";
                                    SGEEnt.PRACTICAL = "1";

                                }
                                else
                                {
                                    SGEEnt.THEORY = "0";
                                    SGEEnt.PRACTICAL = "0";
                                }
                            }
                            else
                            {
                                SGEEnt.THEORY = "0";
                                SGEEnt.PRACTICAL = "0";
                            }

                            try
                            {
                                Double GPA = Convert.ToDouble(txtPoint.Text);

                                if (GPA > 4 || GPA < 0)
                                {
                                    HelperFunction.MsgBox(this, this.GetType(), "The Point must not be more than 4 or less than 0.");
                                    txtPoint.Focus();
                                    DT.HAPPY = false;
                                }
                                else
                                {

                                    SGEEnt.POINTS = Convert.ToString(txtPoint.Text);

                                    //SGEEnt.POINTS = txtPoint.Text;
                                    SGEEnt.CREDIT_POINTS = lblCreditPoints.Text;
                                    SGESer.Update(SGEEnt, DT);
                                }

                            }
                            catch
                            {
                                HelperFunction.MsgBox(this, this.GetType(), "Please enter correct Points.");
                                DT.HAPPY = false;
                            }
                        }
                        else
                        {
                            SGEEnt = new SGPA_EIGHT();
                            SGEEnt.PROGRAM = ddlProgram.SelectedValue;
                            SGEEnt.BATCH = ddlBatch.SelectedValue;
                            SGEEnt.SEMESTER = ddlSemester.SelectedValue;
                            SGEEnt.STUDENT_ID = ddlStudent.SelectedValue;
                            SGEEnt.SUBJECT_ID = lblSubjectId.Text;
                            SGEEnt.CREDIT = lblCredit.Text;
                            SGEEnt.GRADE = lblGrade.Text;

                            if (ddlResult.SelectedValue == "Partial")
                            {

                                if (chkTheory.Checked == true)
                                {
                                    SGEEnt.THEORY = "1";
                                    SGEEnt.PRACTICAL = "0";

                                }
                                else if (chkPractical.Text == "Practical")
                                {
                                    SGEEnt.THEORY = "0";
                                    SGEEnt.PRACTICAL = "1";


                                }
                                else
                                {
                                    SGEEnt.THEORY = "0";
                                    SGEEnt.PRACTICAL = "0";
                                }
                            }
                            else
                            {
                                SGEEnt.THEORY = "0";
                                SGEEnt.PRACTICAL = "0";
                            }


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

                                    SGEEnt.POINTS = Convert.ToString(txtPoint.Text);
                                }
                            }
                            catch
                            {
                                HelperFunction.MsgBox(this, this.GetType(), "Please enter correct Points.");
                            }

                            SGEEnt.CREDIT_POINTS = lblCreditPoints.Text;
                            SGESer.Insert(SGEEnt, DT);

                        }
                    }

                    if (DT.HAPPY == true)
                    {
                        DT.Commit();
                        HelperFunction.MsgBox(this, this.GetType(), "Successfull");
                        ClearFields();
                        btnSave.Visible = false;
                    }
                    else
                    {
                        DT.Abort();
                        HelperFunction.MsgBox(this, this.GetType(), "Something Goes wrong");
                    }
                    DT.Dispose();
                }
            }
            #endregion 

            #region for Percentage
            if (PEnt.RESULT_TYPE == "Percentage")
            {
                DT = new DistributedTransaction();
                if (ddlStudent.SelectedValue == "Select")
                {
                    HelperFunction.MsgBox(this, this.GetType(), "Please Select Student");
                }
                else if (gridPercentage.Rows.Count == 0)
                {
                    HelperFunction.MsgBox(this, this.GetType(), "Please Click View Button to load grid before Save");
                }
                else
                {
                    foreach (GridViewRow gr in gridPercentage.Rows)
                    {
                        Label lblSubjectId = gr.FindControl("lblSubjectId") as Label;
                        Label lblCredit = gr.FindControl("lblCredit") as Label;
                        Label lblGrade = gr.FindControl("lblGrade") as Label;
                        TextBox txtPoint = gr.FindControl("txtPoint") as TextBox;
                        Label lblCreditPoints = gr.FindControl("lblCreditPoints") as Label;
                        DropDownList ddlResult = gr.FindControl("ddlResult") as DropDownList;
                        CheckBox chkTheory = gr.FindControl("chkTheory") as CheckBox;
                        CheckBox chkPractical = gr.FindControl("chkPractical") as CheckBox;


                        SGEEnt = new SGPA_EIGHT();
                        SGEEnt.PROGRAM = ddlProgram.SelectedValue;
                        SGEEnt.BATCH = ddlBatch.SelectedValue;
                        SGEEnt.SEMESTER = ddlSemester.SelectedValue;
                        SGEEnt.STUDENT_ID = ddlStudent.SelectedValue;
                        SGEEnt.SUBJECT_ID = lblSubjectId.Text;
                        SGEEnt = (SGPA_EIGHT)SGESer.GetSingle(SGEEnt, DT);
                        if (SGEEnt != null)
                        {
                            SGEEnt.SUBJECT_ID = lblSubjectId.Text;
                            SGEEnt.CREDIT = lblCredit.Text;
                            SGEEnt.GRADE = lblGrade.Text;

                            if (ddlResult.SelectedValue == "Partial")
                            {

                                if (chkTheory.Checked == true)
                                {
                                    SGEEnt.THEORY = "1";
                                    SGEEnt.PRACTICAL = "0";

                                }
                                else if (chkPractical.Text == "Practical")
                                {
                                    SGEEnt.THEORY = "0";
                                    SGEEnt.PRACTICAL = "1";

                                }
                                else
                                {
                                    SGEEnt.THEORY = "0";
                                    SGEEnt.PRACTICAL = "0";
                                }
                            }
                            else
                            {
                                SGEEnt.THEORY = "0";
                                SGEEnt.PRACTICAL = "0";
                            }

                            try
                            {
                                Double Total = Convert.ToDouble(txtPoint.Text);

                                if (Total > 100 || Total < 0)
                                {
                                    HelperFunction.MsgBox(this, this.GetType(), "The Point must not be more than 100 or less than 0.");
                                    txtPoint.Focus();
                                    DT.HAPPY = false;
                                }
                                else
                                {

                                    SGEEnt.POINTS = Convert.ToString(txtPoint.Text);

                                    SGEEnt.CREDIT_POINTS = "0";
                                    SGESer.Update(SGEEnt, DT);
                                }
                            }
                            catch
                            {
                                HelperFunction.MsgBox(this, this.GetType(), "Please enter correct Points.");
                                DT.HAPPY = false;
                            }

                        }
                        else
                        {
                            SGEEnt = new SGPA_EIGHT();
                            SGEEnt.PROGRAM = ddlProgram.SelectedValue;
                            SGEEnt.BATCH = ddlBatch.SelectedValue;
                            SGEEnt.SEMESTER = ddlSemester.SelectedValue;
                            SGEEnt.STUDENT_ID = ddlStudent.SelectedValue;
                            SGEEnt.SUBJECT_ID = lblSubjectId.Text;
                            SGEEnt.CREDIT = lblCredit.Text;
                            SGEEnt.GRADE = lblGrade.Text;

                            if (ddlResult.SelectedValue == "Partial")
                            {

                                if (chkTheory.Checked == true)
                                {
                                    SGEEnt.THEORY = "1";
                                    SGEEnt.PRACTICAL = "0";

                                }
                                else if (chkPractical.Text == "Practical")
                                {
                                    SGEEnt.THEORY = "0";
                                    SGEEnt.PRACTICAL = "1";

                                }
                                else
                                {
                                    SGEEnt.THEORY = "0";
                                    SGEEnt.PRACTICAL = "0";
                                }
                            }
                            else
                            {
                                SGEEnt.THEORY = "0";
                                SGEEnt.PRACTICAL = "0";
                            }
                            try
                            {
                                Double GPA = Convert.ToDouble(txtPoint.Text);
                                if (GPA > 100 || GPA < 0)
                                {
                                    HelperFunction.MsgBox(this, this.GetType(), "The Point must not be more than 100 or less than 0.");
                                    txtPoint.Focus();
                                }
                                else
                                {

                                    SGEEnt.POINTS = Convert.ToString(txtPoint.Text);
                                }
                            }
                            catch
                            {
                                HelperFunction.MsgBox(this, this.GetType(), "Please enter correct Points.");
                            }

                            SGEEnt.CREDIT_POINTS = "0";
                            SGESer.Insert(SGEEnt, DT);

                        }
                    }

                    if (DT.HAPPY == true)
                    {
                        DT.Commit();
                        HelperFunction.MsgBox(this, this.GetType(), "Successfull");
                        ClearFields();
                        btnSave.Visible = false;
                    }
                    else
                    {
                        DT.Abort();
                        HelperFunction.MsgBox(this, this.GetType(), "Something Goes wrong");
                    }
                    DT.Dispose();
                }
            }
            #endregion 
        }
    }

    protected void ClearFields()
    {
        gridCalculator.DataSource = null;
        gridCalculator.DataBind();

        tr_totalsgpa.Visible = false;
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
        DropDownList ddlResult = (DropDownList)gr.FindControl("ddlResult");

        PEnt = new program();
        PEnt.PK_ID = ddlProgram.SelectedValue;
        PEnt = (program)PSer.GetSingle(PEnt);
        if (PEnt != null)
        {
            #region GPA
            if (PEnt.RESULT_TYPE == "GPA")
            {


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
                                    if (ddlResult.SelectedValue == "Pass")
                                    {
                                        lblGrade.Text = GG.GRADE;
                                    }

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
                    //HelperFunction.MsgBox(this, this.GetType(), "Please Enter correct GPA Point.");
                    txtPoint.Focus();
                }

                CalculateTotal();
            }
            #endregion

            #region Percentage
            if (PEnt.RESULT_TYPE == "Percentage")
            {

                CalculateTotal();
            }
            #endregion
        }

    }

    protected void ddlResult_SelectedIndexChanged(object sender, EventArgs e)
    {

        GridViewRow gr = ((DropDownList)sender).Parent.Parent as GridViewRow;
        TextBox txtPoint = (TextBox)gr.FindControl("txtPoint");
        DropDownList ddlResult = (DropDownList)gr.FindControl("ddlResult");
        Label lblGrade = (Label)gr.FindControl("lblGrade");
        Label lblCreditPoints = (Label)gr.FindControl("lblCreditPoints");
        CheckBox chkTheory = (CheckBox)gr.FindControl("chkTheory");
        CheckBox chkPractical = (CheckBox)gr.FindControl("chkPractical");

        PEnt = new program();
        PEnt.PK_ID = ddlProgram.SelectedValue;
        PEnt = (program)PSer.GetSingle(PEnt);
        if (PEnt != null)
        {
            #region GPA
            if (PEnt.RESULT_TYPE == "GPA")
            {

                if (ddlResult.SelectedValue == "Fail")
                {
                    txtPoint.Text = "0";
                    txtPoint.Enabled = false;
                    chkTheory.Visible = false;
                    chkPractical.Visible = false;
                    lblGrade.Text = "Fail";
                    lblCreditPoints.Text = "0";
                    CalculateTotal();

                }
                else if (ddlResult.SelectedValue == "Absent")
                {
                    txtPoint.Text = "0";
                    txtPoint.Enabled = false;
                    chkTheory.Visible = false;
                    chkPractical.Visible = false;
                    lblGrade.Text = "Abs";
                    lblCreditPoints.Text = "0";
                    CalculateTotal();
                }
                else if (ddlResult.SelectedValue == "Partial")
                {
                    chkTheory.Visible = true;
                    chkPractical.Visible = true;
                    txtPoint.Text = "0";
                    lblCreditPoints.Text = "0";
                    txtPoint.Enabled = false;
                    lblGrade.Text = "Partial";
                    CalculateTotal();

                }
                else if (ddlResult.SelectedValue == "Pass")
                {
                    txtPoint.Enabled = true;
                    chkTheory.Visible = false;
                    chkPractical.Visible = false;
                    lblGrade.Text = "";
                    CalculateTotal();
                }
            }
            #endregion

            #region Percentage
            if (PEnt.RESULT_TYPE == "Percentage")
            {

                if (ddlResult.SelectedValue == "Fail")
                {

                    txtPoint.Text = "0";
                    txtPoint.Enabled = false;
                    chkTheory.Visible = false;
                    chkPractical.Visible = false;
                    lblGrade.Text = "Fail";
                    // lblCreditPoints.Text = "0";
                    CalculateTotal();

                }
                else if (ddlResult.SelectedValue == "Absent")
                {
                    txtPoint.Text = "0";
                    txtPoint.Enabled = false;
                    chkTheory.Visible = false;
                    chkPractical.Visible = false;
                    lblGrade.Text = "Abs";
                    // lblCreditPoints.Text = "0";
                    CalculateTotal();
                }
                else if (ddlResult.SelectedValue == "Partial")
                {
                    chkTheory.Visible = true;
                    chkPractical.Visible = true;
                    txtPoint.Text = "0";
                    /// lblCreditPoints.Text = "0";
                    txtPoint.Enabled = false;
                    lblGrade.Text = "Partial";
                    CalculateTotal();



                }
                else if (ddlResult.SelectedValue == "Pass")
                {
                    txtPoint.Enabled = true;
                    chkTheory.Visible = false;
                    chkPractical.Visible = false;
                    lblGrade.Text = "";
                    CalculateTotal();
                }
            }
            #endregion
        }
    }

    protected void gridPercentage_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string[] xyz = ddlBatch.SelectedValue.Split('-');
        lblBatch.Text = xyz[2];
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox txtPoint = e.Row.FindControl("txtPoint") as TextBox;
            Label lblCreditPoints = e.Row.FindControl("lblCreditPoints") as Label;
            Label lblSubjectId = e.Row.FindControl("lblSubjectId") as Label;
            Label lblGrade = e.Row.FindControl("lblGrade") as Label;
            DropDownList ddlResult = e.Row.FindControl("ddlResult") as DropDownList;
            CheckBox chkTheory = e.Row.FindControl("chkTheory") as CheckBox;
            CheckBox chkPractical = e.Row.FindControl("chkPractical") as CheckBox;

            ArrayList alist = new ArrayList();
            if (ddlResult.SelectedValue == "Pass")
            {
                SGEEnt = new SGPA_EIGHT();
                SGEEnt.BATCH = ddlBatch.SelectedValue;
                SGEEnt.SEMESTER = ddlSemester.SelectedValue;
                SGEEnt.STUDENT_ID = ddlStudent.SelectedValue;
                SGEEnt.SUBJECT_ID = lblSubjectId.Text;
                SGEEnt = (SGPA_EIGHT)SGESer.GetSingle(SGEEnt, DT);
                if (SGEEnt != null)
                {
                    lblGrade.Text = SGEEnt.GRADE;
                    txtPoint.Text = SGEEnt.POINTS;
                    //lblCreditPoints.Text = SGEEnt.CREDIT_POINTS;
                    if (lblGrade.Text == "Fail")
                    {
                        ddlResult.SelectedIndex = 1;
                    }
                    else if (lblGrade.Text == "Abs")
                    {
                        ddlResult.SelectedIndex = 2;
                    }
                    else if (lblGrade.Text == "Partial")
                    {
                        ddlResult.SelectedIndex = 3;
                        chkTheory.Visible = true;
                        chkPractical.Visible = true;

                        if (SGEEnt.THEORY == "1")
                        {
                            chkTheory.Checked = true;
                        }
                        if (SGEEnt.PRACTICAL == "1")
                        {
                            chkPractical.Checked = true;
                        }
                    }
                }
            }

            else if (ddlResult.SelectedValue == "Fail")
            {
                txtPoint.Text = "0";
                lblGrade.Text = "Fail";
                lblCreditPoints.Text = "0";
                txtPoint.Enabled = false;

            }
            else if (ddlResult.SelectedValue == "Absent")
            {
                txtPoint.Text = "0";
                lblGrade.Text = "Abs";
                lblCreditPoints.Text = "0";
                txtPoint.Enabled = false;
            }
            else
            {
                txtPoint.Text = "0";
                lblGrade.Text = "Partial";
                lblCreditPoints.Text = "0";
                txtPoint.Enabled = false;
                if (SGEEnt.THEORY == "1")
                {
                    chkTheory.Checked = true;
                }
                if (SGEEnt.PRACTICAL == "1")
                {
                    chkPractical.Checked = true;
                }
            }
        }
    }
    
}