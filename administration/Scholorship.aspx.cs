using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;
using Entity.Framework;
using DataHelper.Framework;


public partial class administration_Scholorship : System.Web.UI.Page
{
    HSS_LEVEL LVLEnt = new HSS_LEVEL();
    HSS_LEVELService LVLSer = new HSS_LEVELService();

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    BatchYear BTCEnt = new BatchYear();
    BatchYearService BTCSer = new BatchYearService();

    semester SMSEnt = new semester();
    semesterService SMSSer = new semesterService();

    Section SCEnt = new Section();
    SectionService SCSer = new SectionService();

    scholorship_discount SCDEnt = new scholorship_discount();
    scholorship_discountService SCDSer = new scholorship_discountService();

    HSS_STUDENT STEnt = new HSS_STUDENT();
    HSS_STUDENTService STSer = new HSS_STUDENTService();

    HSS_CURRENT_STUDENT CSEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSer = new HSS_CURRENT_STUDENTService();

    SEMESTER_INTALLMENT_MAPPING SIMEnt = new SEMESTER_INTALLMENT_MAPPING();
    SEMESTER_INTALLMENT_MAPPINGService SIMSer = new SEMESTER_INTALLMENT_MAPPINGService();

    STUDENT_PAY_SCHEDULE SPSEnt = new STUDENT_PAY_SCHEDULE();
    STUDENT_PAY_SCHEDULEService SPSSer = new STUDENT_PAY_SCHEDULEService();

    PAY_SCHEDULE_INSTALLMENT PSIEnt = new PAY_SCHEDULE_INSTALLMENT();
    PAY_SCHEDULE_INSTALLMENTService PSISer = new PAY_SCHEDULE_INSTALLMENTService();

    Particulars_Main MPEnt = new Particulars_Main();
    Particulars_MainService MPSer = new Particulars_MainService();

    EntityList theList = new EntityList();
    EntityList newList = new EntityList();

    HelperFunction hf = new HelperFunction();

    string scholorshipid = "";

    Boolean flag = true;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadLevel();
            LoadFaculty();
            LoadProgram();
            LoadSection();

            tblScholar_dis.Visible = false;
            tblDescription.Visible = false;
            tbltxtDescription.Visible = false;
            btnSave.Visible = false;

            ddlInstallmentNo.Enabled = true;
            ddlStudentId.Enabled = true;
            ddlBatch.Enabled = true;
            //ddlSection.Enabled = true;
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
        ddlBatch.Items.Insert(0, "Select");
        ddlSemester.Items.Insert(0, "Select");
        //ddlSection.Items.Insert(0, "Select");
        //ddlStudentId.Items.Insert(0, "Select");
        //ddlInstallmentNo.Items.Insert(0, "Select");
    }

    protected void LoadProgram()
    {
        PEnt = new program();
        PEnt.FACULTY_ID = ddlFaculty.SelectedValue;
        PEnt.PROGRAM_LEVEL = ddlLevel.SelectedValue;
        ddlProgram.DataSource = PSer.GetAll(PEnt);
        ddlProgram.DataTextField = "PROGRAM_CODE";
        //ddlProgram.DataValueField = "PROGRAM_CODE";
        ddlProgram.DataValueField = "PK_ID";
        ddlProgram.DataBind();
        ddlProgram.Items.Insert(0, "Select");
        ddlBatch.Items.Insert(0, "Select");
        ddlSemester.Items.Insert(0, "Select");
        //ddlSection.Items.Insert(0, "Select");
        //ddlStudentId.Items.Insert(0, "Select");
        //ddlInstallmentNo.Items.Insert(0, "Select");
    }

    protected void LoadBatch()
    {
        BTCEnt = new BatchYear();
        BTCEnt.ACTIVE = "1";
        BTCEnt.PROGRAM = ddlProgram.SelectedValue;
        ddlBatch.DataSource = BTCSer.GetAll(BTCEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
        ddlBatch.Items.Insert(0, "Select");
    }

    protected void LoadSemester()
    {
        theList = new EntityList();
        EntityList semList = new EntityList();
        BTCEnt = new BatchYear();
        BTCEnt.ACTIVE = "1";
        BTCEnt.PROGRAM = ddlProgram.SelectedValue;
        BTCEnt.BATCH = ddlBatch.SelectedValue;
        BTCEnt = (BatchYear)BTCSer.GetSingle(BTCEnt);
        if (BTCEnt != null)
        {
            SMSEnt = new semester();
            SMSEnt.PROGRAM_ID = BTCEnt.PROGRAM;
            SMSEnt.SYLLABUS_YEAR = BTCEnt.SYLLABUS_YEAR;
            theList = SMSSer.GetAll(SMSEnt);

            foreach (semester sem in theList)
            {
                semList.Add(sem);
            }
        }

        ddlSemester.DataSource = semList;
        ddlSemester.DataTextField = "SEMESTER_CODE";
        ddlSemester.DataValueField = "PK_ID";
        ddlSemester.DataBind();
        ddlSemester.Items.Insert(0, "Select");



    }
    protected void LoadSection()
    {
        //SCEnt = new Section();

        //ddlSection.DataSource = SCSer.GetAll(SCEnt);
        //ddlSection.DataTextField = "SECTION";
        //ddlSection.DataValueField = "SECTION";
        //ddlSection.DataBind();
        //ddlSection.Items.Insert(0, "Select");
    }
    protected void LoadStudent()
    {

        //ddlStudentId.DataSource = hf.selectstudentinfo(ddlProgram.SelectedValue, ddlBatch.SelectedValue, ddlSemester.SelectedValue, "");
        //ddlStudentId.DataTextField = "STUDENT_NAME";
        //ddlStudentId.DataValueField = "STUDENT_ID";
        //ddlStudentId.DataBind();
        //ddlStudentId.Items.Insert(0, "Select");



        STEnt = new HSS_STUDENT();
        STEnt.PROGRAM = ddlProgram.SelectedValue;
        STEnt.BAT_CH = ddlBatch.SelectedValue;
        STEnt.STATUS = "1";
        ddlStudentId.DataSource = STSer.GetAll(STEnt);
        ddlStudentId.DataTextField = "STUDENT_NI";
        ddlStudentId.DataValueField = "STUDENT_ID";
        ddlStudentId.DataBind();
        ddlStudentId.Items.Insert(0, "Select");



    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        double totalscholorship = 0;
        String Desc = "";
        DistributedTransaction DT = new DistributedTransaction();

        if (ddlStudentId.SelectedValue == "Select" || ddlBatch.SelectedValue == null)
        {
            HelperFunction.MsgBox(this, this.GetType(), "Please Select Student.");
        }


        else if (gridParticulars.Rows.Count > 0)
        {

            #region to calculate the total from the grid

            foreach (GridViewRow gr in gridParticulars.Rows)
            {
                TextBox txtAmount = gr.FindControl("txtAmount") as TextBox;


                if (txtAmount.Text == "") //if Amount field is empty then make it 0;
                {
                    txtAmount.Text = "0";
                }
                try
                {
                    double amount = Convert.ToDouble(txtAmount.Text); //if any alphabet found in Amount Field then make DT false;

                    totalscholorship = totalscholorship + amount;

                }
                catch
                {
                    DT.HAPPY = false;

                }

                Desc = txtDescription.Text;
            }

            #endregion

            #region to update and insert in scholorship_discount table and also update the student_pay_schedule table

            //GridViewRow row = gridParticulars.HeaderRow;

            //TextBox txtDescription = (TextBox)row.FindControl("txtDescription");


            //if (lblPKIDU.Text != "") //check if lblPKIDU text is empty or not. if not empty then update scholorship_discount table.
            //{

            SCDEnt = new scholorship_discount();
            //SCDEnt.PK_ID = lblPKIDU.Text;
            SCDEnt.INSTALLMENT_NO = ddlInstallmentNo.SelectedValue;
            SCDEnt.SEMESTER_ID = ddlSemester.SelectedValue;
            SCDEnt.STUDENT_ID = ddlStudentId.SelectedValue;
            SCDEnt = (scholorship_discount)SCDSer.GetSingle(SCDEnt, DT);
            if (SCDEnt != null)
            {

                SCDEnt.SEMESTER_ID = ddlSemester.SelectedValue;
                SCDEnt.STUDENT_ID = ddlStudentId.SelectedValue;
                SCDEnt.AMOUNT = totalscholorship.ToString();
                SCDEnt.DESCRIPTION = Desc;
                SCDEnt.STATUS = ddlStatus.SelectedValue;
                SCDEnt.INSTALLMENT_NO = ddlInstallmentNo.SelectedValue;
                scholorshipid = SCDSer.Update(SCDEnt, DT).ToString();

                //updatePaySchedule(scholorshipid, DT); //calling the function to update the amount in student_pay_schedule table
            }

            //}
            else //check if lblPKIDU text is empty or not. if empty then insert in scholorship_discount table.
            {

                if (totalscholorship > 0)
                {

                    SCDEnt = new scholorship_discount();
                    SCDEnt.SEMESTER_ID = ddlSemester.SelectedValue;
                    SCDEnt.STUDENT_ID = ddlStudentId.SelectedValue;
                    SCDEnt.AMOUNT = totalscholorship.ToString();
                    SCDEnt.DESCRIPTION = Desc;
                    SCDEnt.STATUS = ddlStatus.SelectedValue;
                    SCDEnt.INSTALLMENT_NO = ddlInstallmentNo.SelectedValue;
                    scholorshipid = SCDSer.Insert(SCDEnt, DT).ToString();


                    updatePaySchedule(scholorshipid, DT);  //calling the function to update the amount in student_pay_schedule table 
                }
            }
            #endregion

            if (DT.HAPPY == true)
            {
                DT.Commit();
                HelperFunction.MsgBox(this, this.GetType(), "Successfull");

            }
            else
            {
                DT.Abort();
                HelperFunction.MsgBox(this, this.GetType(), "Something Goes Wrong");
            }
            DT.Dispose();

            LoadGrid();
            //clearFields();
        }
    }

    protected void updatePaySchedule(string scholorshipid, DistributedTransaction DT)
    {
        foreach (GridViewRow gr in gridParticulars.Rows)
        {
            Label lblPaticularId = gr.FindControl("lblParticularId") as Label;
            TextBox txtAmount = gr.FindControl("txtAmount") as TextBox;
            TextBox txtDescription = gr.FindControl("txtDescription") as TextBox;


            SPSEnt = new STUDENT_PAY_SCHEDULE();
            SPSEnt.STUDENT_ID = ddlStudentId.SelectedValue;
            SPSEnt.INSTALLMENT_NO = ddlInstallmentNo.SelectedValue;
            SPSEnt.SEMESTER = ddlSemester.SelectedValue;
            SPSEnt.PARTICULARS = lblPaticularId.Text;
            SPSEnt = (STUDENT_PAY_SCHEDULE)SPSSer.GetSingle(SPSEnt, DT);
            if (SPSEnt != null && txtAmount.Text != "0")
            {
                SPSEnt.DISCOUNT = txtAmount.Text;
                SPSEnt.DISCOUNT_SCHOLARSHIP_ID = scholorshipid;
                SPSEnt.REMARKS = ddlStatus.SelectedItem.ToString();
                SPSEnt.STATUS = "0";
                SPSSer.Update(SPSEnt, DT);
            }

        }
    }

    //protected string InstallmentToSemester(string installment)
    //{
    //    string semester = "";
    //    SIMEnt = new SEMESTER_INTALLMENT_MAPPING();
    //    SIMEnt.INSTALLMENT_NO = installment;
    //    SIMEnt = (SEMESTER_INTALLMENT_MAPPING)SIMSer.GetSingle(SIMEnt);
    //    if (SIMEnt != null)
    //    {
    //        semester = SIMEnt.SEMESTER_ID;
    //    }
    //    return semester;
    //}

    protected void ddlStudentId_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadInstallment();
        tblScholar_dis.Visible = false;
        tblDescription.Visible = false;
        tbltxtDescription.Visible = false;
        btnSave.Visible = false;
        gridScholorship.Visible = false;
        gridParticulars.Visible = false;
    }


    protected void LoadInstallment()
    {

        theList = new EntityList();

        PSIEnt = new PAY_SCHEDULE_INSTALLMENT();
        PSIEnt.PROGRAMID = ddlProgram.SelectedValue;
        PSIEnt.BATCH = ddlBatch.SelectedValue;
        PSIEnt.SEMESTERID = ddlSemester.SelectedValue;
        theList = PSISer.GetAll(PSIEnt);
        EntityList newList = new EntityList();

        if (theList.Count != 0)
        {
            foreach (PAY_SCHEDULE_INSTALLMENT psy in theList)
            {
                SPSEnt = new STUDENT_PAY_SCHEDULE();
                SPSEnt.INSTALLMENT_NO = psy.INST_NO;
                if (SPSEnt != null)
                {
                    newList.Add(psy);

                    ddlInstallmentNo.DataSource = newList;
                    ddlInstallmentNo.DataTextField = "CUM_INST_NO";
                    ddlInstallmentNo.DataValueField = "CUM_INST_NO";
                    ddlInstallmentNo.DataBind();
                    ddlInstallmentNo.Items.Insert(0, "Select");
                }
            }
            //gridParticulars.Visible = true;
            //btnSave.Visible = true;
        }
        else
        {
            newList = new EntityList();
            ddlInstallmentNo.DataSource = newList;
            ddlInstallmentNo.DataTextField = "INST_NO";
            ddlInstallmentNo.DataValueField = "INST_NO";
            ddlInstallmentNo.DataBind();
            ddlInstallmentNo.Items.Insert(0, "Select");
            //txtMonth.Text = "";
            //txtYear.Text = "";
            //chkParticular.ClearSelection();

            gridParticulars.Visible = false;
            btnSave.Visible = false;

        }

    }

    protected void LoadGrid()
    {
        SCDEnt = new scholorship_discount();
        SCDEnt.SEMESTER_ID = ddlSemester.SelectedValue;
        SCDEnt.INSTALLMENT_NO = ddlInstallmentNo.SelectedValue;
        SCDEnt.STUDENT_ID = ddlStudentId.SelectedValue;
        gridScholorship.DataSource = SCDSer.GetAll(SCDEnt);
        gridScholorship.DataBind();
    }

    protected void gridScholorship_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStudentId = e.Row.FindControl("lblStudentId") as Label;
            Label lblStudentName = e.Row.FindControl("lblStudentName") as Label;

            STEnt = new HSS_STUDENT();
            STEnt.STUDENT_ID = lblStudentId.Text;
            STEnt = (HSS_STUDENT)STSer.GetSingle(STEnt);
            if (STEnt != null)
            {
                lblStudentName.Text = STEnt.NAME_ENGLISH;
            }

        }
    }
    protected void gridScholorship_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName.Equals("Change"))
        //{
        //    GridViewRow gr = ((ImageButton)e.CommandSource).Parent.Parent as GridViewRow;

        //    Label lblPKID = gr.FindControl("lblPKID") as Label;
        //    Label lblSemester = gr.FindControl("lblSemester") as Label;
        //    Label lblStudentId = gr.FindControl("lblStudentId") as Label;
        //    Label lblStatus = gr.FindControl("lblStatus") as Label;
        //    Label lblDescription = gr.FindControl("lblDescription") as Label;
        //    Label lblAmount = gr.FindControl("lblAmount") as Label;
        //    Label lblInstallmentNo = gr.FindControl("lblInstallmentNo") as Label;

        //    TextBox txtDescription = (TextBox)gr.FindControl("txtDescription");

        //    lblPKIDU.Text = lblPKID.Text;
        //    // ddlSemester.SelectedValue = lblSemester.Text;
        //    ddlInstallmentNo.SelectedValue = lblInstallmentNo.Text;
        //    ddlStudentId.SelectedValue = lblStudentId.Text;
        //    ddlStatus.SelectedValue = lblStatus.Text;
        //    //txtDescription.Text = lblDescription.Text;

        //    CSEnt = new HSS_CURRENT_STUDENT();
        //    CSEnt.STUDENT_ID = lblStudentId.Text;
        //    CSEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSEnt);
        //    if (CSEnt != null)
        //    {
        //        ddlBatch.SelectedValue = CSEnt.BATCH;
        //        LoadStudent();
        //        ddlStudentId.SelectedValue = CSEnt.STUDENT_ID;
        //    }

        //    ddlInstallmentNo.Enabled = false;
        //    ddlStudentId.Enabled = false;
        //    ddlBatch.Enabled = false;
        //    ddlSection.Enabled = false;

        //    LoadParticular();
        //}
    }

    protected void clearFields()
    {
        lblPKIDU.Text = "";
        ddlFaculty.SelectedIndex = 0;
        ddlProgram.SelectedIndex = 0;
        ddlBatch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;

        //ddlSection.SelectedIndex = 0;

        ddlInstallmentNo.SelectedIndex = 0;
        ddlStatus.SelectedIndex = 0;

        LoadStudent();

        ddlInstallmentNo.Enabled = true;
        ddlStudentId.Enabled = true;
        ddlBatch.Enabled = true;
        //ddlSection.Enabled = true;

        gridParticulars.DataSource = null;
        gridParticulars.DataBind();

        gridScholorship.DataSource = null;
        gridScholorship.DataBind();

    }

    protected void btnView_Click(object sender, EventArgs e)
    {

        LoadParticular();
        txtDescription.Text = "";
        SCDEnt = new scholorship_discount();
        SCDEnt.STUDENT_ID = ddlStudentId.SelectedValue;
        SCDEnt.SEMESTER_ID = ddlSemester.SelectedValue;
        SCDEnt.INSTALLMENT_NO = ddlInstallmentNo.SelectedValue;
        SCDEnt = (scholorship_discount)SCDSer.GetSingle(SCDEnt);
        if (SCDEnt != null)
        {
            txtDescription.Text = SCDEnt.DESCRIPTION;
        }

        if (ddlSemester.SelectedValue != "Select" && ddlStudentId.SelectedValue != "Select" && ddlInstallmentNo.SelectedValue != "Select")
        {

            tblScholar_dis.Visible = true;
            tblDescription.Visible = true;
            tbltxtDescription.Visible = true;

            gridParticulars.Visible = true;
            btnSave.Visible = true;

            gridScholorship.Visible = true;
            LoadGrid();
        }
        else
        {

            tblScholar_dis.Visible = false;
            tblDescription.Visible = false;
            tbltxtDescription.Visible = false;
            btnSave.Visible = false;
            gridParticulars.Visible = true;
            gridScholorship.Visible = false;

        }
    }

    protected void LoadParticular()
    {
        if (ddlStudentId.SelectedValue != "Select" && ddlSemester.SelectedValue != "Select" && ddlInstallmentNo.SelectedValue != "Select")
        {
            SPSEnt = new STUDENT_PAY_SCHEDULE();
            SPSEnt.SEMESTER = ddlSemester.SelectedValue;
            SPSEnt.STUDENT_ID = ddlStudentId.SelectedValue;
            //SPSEnt.YEAR = hf.NepaliYear();
            SPSEnt.INSTALLMENT_NO = ddlInstallmentNo.SelectedValue;
            gridParticulars.DataSource = SPSSer.GetAll(SPSEnt);
            gridParticulars.DataBind();
        }
        else
        {
            gridParticulars.DataSource = null;
            gridParticulars.DataBind();
        }
    }

    protected void gridParticulars_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblParticularId = e.Row.FindControl("lblParticularId") as Label;
            Label lblParticular = e.Row.FindControl("lblParticular") as Label;


            MPEnt = new Particulars_Main();
            MPEnt.MAIN_ID = lblParticularId.Text;
            MPEnt = (Particulars_Main)MPSer.GetSingle(MPEnt);
            if (MPEnt != null)
            {
                lblParticular.Text = MPEnt.PARTICULAR_NAME;
            }



        }
    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProgram.SelectedValue != "Select")
        {
            LoadBatch();

        }
    }
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {

        LoadSemester();
        LoadSection();
    }

    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {

        //if (ddlSection.SelectedValue != "Select")
        //{
        //    LoadStudent();
        //}
        //else
        //{
        //    ddlStudentId.Items.Clear();
        //    ddlStudentId.Items.Insert(0, "Select");

        //    ddlInstallmentNo.Items.Clear();
        //    ddlInstallmentNo.Items.Insert(0, "Select");
        //}
    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSection();
        LoadStudent();



    }

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFaculty.SelectedValue != "Select")
        {
            LoadProgram();
        }
        else
        {
            ddlProgram.Items.Clear();
            ddlProgram.Items.Insert(0, "Select");


        }
        if (ddlProgram.SelectedValue == "Select")
        {

            ddlBatch.Items.Clear();
            ddlBatch.Items.Insert(0, "Select");

            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Select");


        }
    }

    protected void txtName_TextChanged(object sender, EventArgs e)
    {
        string[] xyz;
        try
        {
            xyz = txtName.Text.Split('-');
            txtRegNo.Text = xyz[1];
            LoadInstallment();

        }
        catch
        {
            HelperFunction.MsgBox(this, this.GetType(), "Student not found");
            txtName.Text = "";
        }
    }

    protected void txtRegNo_TextChanged(object sender, EventArgs e)
    {
        LoadInstallment();
    }
}