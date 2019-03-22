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

public partial class finance_ReceiptEntry : System.Web.UI.Page
{

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    BatchYear BYEnt = new BatchYear();
    BatchYearService BYSer = new BatchYearService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    HSS_STUDENT SIEnt = new HSS_STUDENT();
    HSS_STUDENTService SISer = new HSS_STUDENTService();

    HSS_CURRENT_STUDENT CSEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSer = new HSS_CURRENT_STUDENTService();

    Section SEnt = new Section();
    SectionService SSer = new SectionService();

    Months MEnt = new Months();
    MonthsService MSer = new MonthsService();

    Receipt REnt = new Receipt();
    ReceiptService RSer = new ReceiptService();

    STUDENT_PAY_SCHEDULE SPSEnt = new STUDENT_PAY_SCHEDULE();
    STUDENT_PAY_SCHEDULEService SPSSer = new STUDENT_PAY_SCHEDULEService();

    PAY_SCHEDULE_INSTALLMENT PSIEnt = new PAY_SCHEDULE_INSTALLMENT();
    PAY_SCHEDULE_INSTALLMENTService PSISer = new PAY_SCHEDULE_INSTALLMENTService();

    Amount_collection ACEnt = new Amount_collection();
    Amount_collectionService ACSer = new Amount_collectionService();

    Particulars_Main PMEnt = new Particulars_Main();
    Particulars_MainService PMSer = new Particulars_MainService();

    HelperFunction hf = new HelperFunction();

    UserProfileEntity userprofile = new UserProfileEntity();

    bill BLEnt = new bill();
    billService BLSer = new billService();

    masterbill MBLEnt = new masterbill();
    masterbillService MBLSer = new masterbillService();

    STUDENT_ADVANCE ADVEnt = new STUDENT_ADVANCE();
    STUDENT_ADVANCEService ADVSer = new STUDENT_ADVANCEService();

    HSS_NAME NEnt = new HSS_NAME();
    HSS_NAMEService NSer = new HSS_NAMEService();

    sms_record SMSEnt = new sms_record();
    sms_recordService SMSSer = new sms_recordService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    BatchYear BTEnt = new BatchYear();
    BatchYearService BTSer = new BatchYearService();

    STUDENT_ACCOUNT SAEnt = new STUDENT_ACCOUNT();
    STUDENT_ACCOUNTService SASer = new STUDENT_ACCOUNTService();

    RECEIPT_DETAIL RDEnt = new RECEIPT_DETAIL();
    RECEIPT_DETAILService RDSer = new RECEIPT_DETAILService();

    RECEIPT_HOLDING_BAL RHBEnt = new RECEIPT_HOLDING_BAL();
    RECEIPT_HOLDING_BALService RHBSer = new RECEIPT_HOLDING_BALService();

    EntityList theList = new EntityList();
    EntityList newList = new EntityList();


    int count = 0;
    string receiptid;
    string temp;
    int monthid;
    double total = 0, amount = 0;

    protected void Page_Load(object sender, EventArgs e)
    {

        userprofile = (UserProfileEntity)HttpContext.Current.Session["UserProfile"];

        if (!IsPostBack)
        {
            hide.Visible = true;
            NEnt = new HSS_NAME();

            NEnt = (HSS_NAME)NSer.GetSingle(NEnt);
            lblCollegeName.Text = NEnt.NAME;
            lblCollgeSign.Text = "For:" + NEnt.NAME;
            lblAddress.Text = NEnt.ADRESS;
            lblPhoneNo.Text = NEnt.CONTACT;
            lblEmail.Text = NEnt.WEBSITE;

            loadReceiptNo();

            LoadFaculty();
            LoadLevel();
            LoadProgram();


            txtReceiptDate.Text = hf.GetTodayDate();

            txtReceiptDate.Text = string.Format("{0:MM-dd-yyyy}", txtReceiptDate.Text);
            string[] NDate = hf.ConvertEnglishToNepali(txtReceiptDate.Text);
            txtDate.Text = NDate[0] + "/" + NDate[1] + "/" + NDate[2];


            //LoadCollegeCode();

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
        ddlBatch.Items.Insert(0, "Select");
        ddlSemester.Items.Insert(0, "Select");

    }

    protected void LoadLevel()
    {
        LEnt = new HSS_LEVEL();
        ddlLevel.DataSource = LSrv.GetAll(LEnt);
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
        ddlBatch.Items.Insert(0, "Select");
        ddlSemester.Items.Insert(0, "Select");

    }

    protected void LoadBatch()
    {
        BYEnt = new BatchYear();
        BYEnt.ACTIVE = "1";
        BYEnt.PROGRAM = ddlProgram.SelectedValue;
        ddlBatch.DataSource = BYSer.GetAll(BYEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
        ddlBatch.Items.Insert(0, "Select");
    }

    protected void LoadSemester()
    {
        theList = new EntityList();
        EntityList semList = new EntityList();
        BYEnt = new BatchYear();
        BYEnt.ACTIVE = "1";
        BYEnt.PROGRAM = ddlProgram.SelectedValue;
        BYEnt.BATCH = ddlBatch.SelectedValue;
        BYEnt = (BatchYear)BYSer.GetSingle(BYEnt);
        if (BYEnt != null)
        {
            SMEnt = new semester();
            SMEnt.PROGRAM_ID = BYEnt.PROGRAM;
            SMEnt.SYLLABUS_YEAR = BYEnt.SYLLABUS_YEAR;
            theList = SMSer.GetAll(SMEnt);

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

    protected void LoadCollegeCode()
    {
        //NEnt = new HSS_NAME();
        //NEnt = (HSS_NAME)NSer.GetSingle(NEnt);
        //if (NEnt != null)
        //{
        //    PEnt = new program();
        //    PEnt.PK_ID = ddlProgram.SelectedValue;
        //    PEnt = (program)PSer.GetSingle(PEnt);
        //    if (PEnt != null)
        //    {
        //        lblCode.Text = NEnt.CODE + PEnt.PROGRAM_CODE;
        //    }
        //}
    }

    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBatch.SelectedValue != "Select")
        {
            LoadSemester();

        }
        else
        {
            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Select");

            ddlStudentName.Items.Clear();
            ddlStudentName.Items.Insert(0, "Select");

        }

    }

    protected void LoadStudentName()
    {

        ddlStudentName.DataSource = hf.selectstudentinfo(ddlProgram.SelectedValue, ddlBatch.SelectedValue, ddlSemester.SelectedValue, "");
        ddlStudentName.DataTextField = "STUDENT_NAME";
        ddlStudentName.DataValueField = "STUDENT_ID";
        ddlStudentName.DataBind();
        ddlStudentName.Items.Insert(0, "Select");
    }

    protected void ddlStudentName_SelectedIndexChanged(object sender, EventArgs e)
    {
        CSEnt = new HSS_CURRENT_STUDENT();
        CSEnt.STUDENT_ID = ddlStudentName.SelectedValue;
        CSEnt.STATUS = "1";
        CSEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSEnt);
        if (CSEnt != null)
        {

            SIEnt = new HSS_STUDENT();
            SIEnt.STUDENT_ID = CSEnt.STUDENT_ID;
            SIEnt = (HSS_STUDENT)SISer.GetSingle(SIEnt);
            if (SIEnt != null)
            {

                PEnt = new program();
                PEnt.PROGRAM_CODE = SIEnt.PROGRAM;
                PEnt = (program)PSer.GetSingle(PEnt);
                if (PEnt != null)
                {
                    ddlFaculty.SelectedValue = PEnt.FACULTY_ID;
                    LoadProgram();
                    ddlProgram.SelectedValue = SIEnt.PROGRAM;
                    LoadBatch();
                    ddlBatch.SelectedValue = CSEnt.BATCH;
                    LoadSemester();
                    ddlSemester.SelectedValue = CSEnt.SEMESTER;
                    LoadStudentName();
                    ddlStudentName.SelectedValue = CSEnt.STUDENT_ID;

                    LoadDetail();
                    getForMonth();

                }
            }
        }

        string studentid = ddlStudentName.SelectedValue;

        //if (ddlStudentName.SelectedValue != "Select")
        //{

        //    txtStudentId.Text = studentid.Replace(lblCode.Text, "");
        //}
        //else
        //{
        //    txtStudentId.Text = "";
        //}

        //LoadDetail();


    }

    protected void loadStdDetail_Frm_stdID(string studentid)
    {

        MBLEnt = new masterbill();
        MBLEnt.STUDENT_ID = txtStudentId.Text;
        MBLEnt = (masterbill)MBLSer.GetSingle(MBLEnt);
        if (MBLEnt != null)
        {
            PEnt = new program();
            PEnt.PK_ID = MBLEnt.PROGRAM;
            PEnt = (program)PSer.GetSingle(PEnt);
            if (PEnt != null)
            {
                LoadFaculty();
                ddlFaculty.SelectedValue = PEnt.FACULTY_ID;
                LoadLevel();
                ddlLevel.SelectedValue = PEnt.PROGRAM_LEVEL;
            }
            LoadProgram();
            ddlProgram.SelectedValue = MBLEnt.PROGRAM;
            LoadBatch();
            ddlBatch.SelectedValue = MBLEnt.BATCH;
            LoadSemester();
            ddlSemester.SelectedValue = MBLEnt.SEMESTER;
            txtInstallments.Text = MBLEnt.INSTALLMENT;
        }

        SIEnt = new HSS_STUDENT();
        SIEnt.STUDENT_ID = txtStudentId.Text;
        SIEnt = (HSS_STUDENT)SISer.GetSingle(SIEnt);
        if (SIEnt != null)
        {
            SIEnt = new HSS_STUDENT();
            SIEnt.STUDENT_ID = txtStudentId.Text;
            SIEnt = (HSS_STUDENT)SISer.GetSingle(SIEnt);
            if (SIEnt != null)
            {
                LoadStudentName();
                ddlStudentName.SelectedValue = SIEnt.STUDENT_ID;

            }



            LoadDetail();
            getForMonth();

        }

        //LoadDetail();
        //getForMonth();

    }

    protected void txtStudentId_TextChanged(object sender, EventArgs e)
    {
        loadStdDetail_Frm_stdID(txtStudentId.Text);

    }

    protected void getForMonth()
    {

        if (txtNumInstallment.Text == "1")
        {
            SPSEnt = new STUDENT_PAY_SCHEDULE();
            SPSEnt.STUDENT_ID = ddlStudentName.SelectedValue;
            SPSEnt.INSTALLMENT_NO = ddlInstallments.SelectedValue;
            SPSEnt = (STUDENT_PAY_SCHEDULE)SPSSer.GetSingle(SPSEnt);
            if (SPSEnt != null)
            {
                txtForMonths.Text = SPSEnt.FOR_MONTH;
            }
        }
        else
        {
            SPSEnt = new STUDENT_PAY_SCHEDULE();
            SPSEnt.STUDENT_ID = CSEnt.STUDENT_ID;
            SPSEnt.INSTALLMENT_NO = ddlInstallments.SelectedValue;
            SPSEnt = (STUDENT_PAY_SCHEDULE)SPSSer.GetSingle(SPSEnt);
            if (SPSEnt != null)
            {
                txtForMonths.Text = SPSEnt.FOR_MONTH;
            }
            SPSEnt = new STUDENT_PAY_SCHEDULE();
            SPSEnt.STUDENT_ID = CSEnt.STUDENT_ID;
            SPSEnt.INSTALLMENT_NO = (Convert.ToDouble(ddlInstallments.SelectedValue) + Convert.ToDouble(txtNumInstallment.Text) - 1).ToString();
            SPSEnt = (STUDENT_PAY_SCHEDULE)SPSSer.GetSingle(SPSEnt);
            if (SPSEnt != null)
            {
                txtForMonths.Text += "-" + SPSEnt.FOR_MONTH;
            }
        }


    }

    protected void loadReceiptNo()
    {
        #region documentation of loadReceiptNo()
        /*
        This methods call the getMaxReceiptNo(FiscalYear) defined in HelperFunction and Load the Receipt Number in TextBox (txtReceiptNo).
        */

        #endregion

        string aaa = hf.getMaxReceiptNo(hf.checkFiscalYear(hf.NepaliMonth(), hf.NepaliYear()));
        if (aaa == "")
        {
            txtReceiptNo.Text = "1";
        }
        else
        {
            txtReceiptNo.Text = Convert.ToDouble(hf.getMaxReceiptNo(hf.checkFiscalYear(hf.NepaliMonth(), hf.NepaliYear()))).ToString();
        }
    }

    protected void LoadDetail()
    {

        CSEnt = new HSS_CURRENT_STUDENT();

        CSEnt.STUDENT_ID = ddlStudentName.SelectedValue;
        CSEnt.BATCH = ddlBatch.SelectedValue;
        CSEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSEnt);
        if (CSEnt != null)
        {
            labelrow.Visible = true;

            SMEnt = new semester();
            SMEnt.PK_ID = ddlSemester.SelectedValue;
            SMEnt = (semester)SMSer.GetSingle(SMEnt);
            if (SMEnt != null)
            {
                lblSemester.Text = SMEnt.SEMESTER_CODE;
                lblSection.Text = CSEnt.SECTION;
            }

            lblStudentId.Text = CSEnt.STUDENT_ID;



            SIEnt = new HSS_STUDENT();
            SIEnt.STUDENT_ID = ddlStudentName.SelectedValue;
            SIEnt = (HSS_STUDENT)SISer.GetSingle(SIEnt);
            if (SIEnt != null)
            {
                lblBatchYear.Text = CSEnt.BATCH;
                lblStudentName.Text = SIEnt.NAME_ENGLISH;
            }

            //  LoadData();
            //  LoadMonthlyAmount();
            LoadInstallments();
            LoadNumberofInstallment();
            loadReceiptNo();
            txtReceiptDate.Focus();

            LoadData();

        }
        else
        {
            lblSemester.Text = "";
            lblStudentId.Text = "";
            lblBatchYear.Text = "";
            lblStudentName.Text = "";
            lblRemainingAmt.Text = "";
            //txtStudentId.Text = "";
            //txtStudentId.Focus();
            labelrow.Visible = false;
            HelperFunction.MsgBox(this, this.GetType(), "Invalid Student Id");
        }
    }

    protected void LoadInstallments()
    {
        EntityList theList = new EntityList();
        EntityList payscheduleList = new EntityList();
        SPSEnt = new STUDENT_PAY_SCHEDULE();
        SPSEnt.STUDENT_ID = ddlStudentName.SelectedValue;
        theList = SPSSer.GetAll(SPSEnt);
        foreach (STUDENT_PAY_SCHEDULE sp in theList)
        {
            if (sp.STATUS != "2")
            {
                payscheduleList.Add(sp);
            }
        }


        ddlInstallments.DataSource = payscheduleList;
        ddlInstallments.DataTextField = "INSTALLMENT_NO";
        ddlInstallments.DataValueField = "INSTALLMENT_NO";
        ddlInstallments.DataBind();
    }
    protected void txtReceiptDate_TextChanged(object sender, EventArgs e)
    {
        txtReceiptDate.Text = string.Format("{0:MM-dd-yyyy}", txtReceiptDate.Text);
        string[] NDate = hf.ConvertEnglishToNepali(txtReceiptDate.Text);
        txtDate.Text = NDate[0] + "/" + NDate[1] + "/" + NDate[2];
    }
    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        if (txtDate.Text != "")
        {
            string[] nepdate = txtDate.Text.Split('/');
            txtReceiptDate.Text = hf.ConvertNepaliTOEnglish(nepdate[0], nepdate[1], nepdate[2]);

            if (hf.ConvertNepaliTOEnglish(nepdate[0], nepdate[1], nepdate[2]) == "")
            {
                HelperFunction.MsgBox(this, this.GetType(), "Wrong Date Format");
                txtDate.Text = "";
                txtReceiptDate.Text = "";
                txtDate.Focus();
            }

        }
    }
    protected void ddlPaymentType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPaymentType.SelectedValue == "Installment")
        {
            ddlInstallments.Visible = true;
            lblInstallments.Visible = true;
            lblNumInstallment.Visible = true;
            txtNumInstallment.Visible = true;
            txtInstallments.Visible = true;
            gridMiscelleneous.Visible = false;
            tdmonths.Attributes["Colspan"] = "1";
            txtAmount.ReadOnly = false;
        }
        else if (ddlPaymentType.SelectedValue == "Miscellaneous")
        {
            EntityList theList = new EntityList();
            EntityList pList = new EntityList();
            lblInstallments.Visible = false;
            lblNumInstallment.Visible = false;
            txtNumInstallment.Visible = false;
            txtInstallments.Visible = false;
            ddlInstallments.Visible = false;
            tdmonths.Attributes["Colspan"] = "2";
        }
    }
    protected void txtNumInstallment_TextChanged(object sender, EventArgs e)
    {
        LoadNumberofInstallment();
        getForMonth();
    }

    protected void LoadNumberofInstallment()
    {
        //    try
        //    {
        //        if (txtNumInstallment.Text != "")
        //        {
        //            int frominstallment = Convert.ToInt32(ddlInstallments.SelectedValue);
        //            int numofinstallment = Convert.ToInt32(txtNumInstallment.Text);

        //            if (numofinstallment == 1)
        //            {
        //                txtInstallments.Text = frominstallment.ToString();
        //            }
        //            else
        //            {
        //                txtInstallments.Text = frominstallment.ToString() + "-" + (frominstallment + numofinstallment - 1).ToString();
        //            }
        //            gridFeeStructure.DataSource = hf.getInstallmentStructure(frominstallment.ToString(), (frominstallment + numofinstallment - 1).ToString(), lblStudentId.Text);
        //            gridFeeStructure.DataBind();




        //        }
        //        else
        //        {
        //            HelperFunction.MsgBox(this, this.GetType(), "Please Enter Number of Installment.");
        //        }

        //    }
        //    catch
        //    {
        //        txtInstallments.Text = "";
        //        //HelperFunction.MsgBox(this, this.GetType(), "Only Number Accepted.");
        //    }
    }
    protected void gridFeeStructure_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblAmount = e.Row.FindControl("lblAmount") as Label;
            total = total + Convert.ToDouble(lblAmount.Text);
        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            Label lblTotal = e.Row.FindControl("lblTotal") as Label;
            lblTotal.Text = total.ToString();

            lblGrandTotal.Text = total.ToString();
        }
    }
    protected void txtAmount_TextChanged(object sender, EventArgs e)
    {
        if (txtAmount.Text != "")
        {

            try
            {

                double amt = Convert.ToDouble(txtAmount.Text);

                //checkEnoughAmount();


                if (amt > Convert.ToDouble(lblTotalAmt.Text))
                {
                    HelperFunction.MsgBox(this, this.GetType(), "Amount is more than payable amount.");
                    txtAmount.Text = "";
                }



            }
            catch
            {
                HelperFunction.MsgBox(this, this.GetType(), "Enter Number Only in Amount Field");
                txtAmount.Text = "";
            }
        }
    }

    //protected void checkEnoughAmount()
    //{
    //    try
    //    {

    //        amount = Convert.ToDouble(txtAmount.Text) + Convert.ToDouble(lblAdvance.Text);

    //        if (amount < Convert.ToDouble(lblGrandTotal.Text))
    //        {
    //            HelperFunction.MsgBox(this, this.GetType(), "Amount Not Enough");
    //            txtAmount.Text = "";
    //        }



    //    }
    //    catch { }
    //}

    protected void rbtnPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnPaymentMode.SelectedValue == "Cheque" || rbtnPaymentMode.SelectedValue == "Voucher")
        {
            chequedetail.Visible = true;
            chequedetails.Visible = true;
            txtChequeDate.Focus();
        }
        else
        {
            chequedetail.Visible = false;
            chequedetails.Visible = false;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        DistributedTransaction DT = new DistributedTransaction();
        string[] instNo;
        userprofile = (UserProfileEntity)HttpContext.Current.Session["UserProfile"];
        string[] sem = lblSemester.Text.Split(' ');
        string[] chkdate = txtDate.Text.Split('/');


        #region to insert in receipt
        REnt = new Receipt();
        REnt.RECEIPTNO = txtReceiptNo.Text;
        REnt.BATCHNO = lblBatchYear.Text;
        //REnt.SEMESTER = sem[0];
        REnt.SEMESTER = ddlSemester.SelectedValue;
        REnt.STUDENTID = lblStudentId.Text;

        REnt.DAY = chkdate[0];
        REnt.MONTH = chkdate[1];
        REnt.YEAR = chkdate[2];
        REnt.FISCALYEAR = hf.checkFiscalYear(chkdate[1], chkdate[2]);
        REnt.SYSTEMDATE = hf.ConvertNepaliTOEnglish(hf.NepaliDay(), hf.NepaliMonth(), hf.NepaliYear());
        REnt.REMARKS = txtRemarks.Text;
        REnt.RECEIPTDATEEN = txtReceiptDate.Text;
        REnt.PAYMENT_TYPE = ddlPaymentType.SelectedValue;
        REnt.PAIDAMOUNT = txtAmount.Text;
        REnt.RECEIVED_BY = userprofile.UserName;
        REnt.INSTALLMENT_NO = txtInstallments.Text;

        REnt.FORMONTH = txtForMonths.Text;

        if (rbtnPaymentMode.SelectedValue == "Cheque")
        {
            REnt.CHEQUE_DATE = txtChequeDate.Text;
            REnt.BANK_NAME = txtBankName.Text;
            REnt.CHEQUE_NO = txtChequeNo.Text;
            REnt.RECEIPT_MODE = "Cheque";
        }
        else if (rbtnPaymentMode.SelectedValue == "Voucher")
        {
            REnt.CHEQUE_DATE = txtChequeDate.Text;
            REnt.BANK_NAME = txtBankName.Text;
            REnt.CHEQUE_NO = txtChequeNo.Text;
            REnt.RECEIPT_MODE = "Voucher";
        }
        else if (rbtnPaymentMode.SelectedValue == "Cash")
        {

            REnt.RECEIPT_MODE = "Cash";
        }
        REnt.REFRENCE_BILL_NO = txtRefBillNo.Text;
        REnt.CANCEL_STATUS = "0";

        receiptid = RSer.Insert(REnt, DT).ToString();
        #endregion

        #region to insert in student_account

        SAEnt = new STUDENT_ACCOUNT();
        SAEnt.STUDENT_ID = txtStudentId.Text;
        SAEnt.RECEIPT_ID = receiptid;
        SAEnt.RECEIPT_AMOUNT = txtAmount.Text;

        SASer.Insert(SAEnt, DT);

        #endregion

        #region to update status of pay_schedule through inst_no in receipt
        //int inst = txtInstallments.Text.Length;
        //int x, y;
        //double rembal = 0.0;
        //RHBEnt = new RECEIPT_HOLDING_BAL();
        //RHBEnt.STUDENT_ID = lblStudentId.Text;
        //RHBEnt = (RECEIPT_HOLDING_BAL)RHBSer.GetSingle(RHBEnt);
        //if (RHBEnt != null)
        //{
        //    rembal = Convert.ToDouble(txtAmount.Text) + Convert.ToDouble(RHBEnt.AMOUNT);
        //}

        //if (inst > 2)
        //{
        //    string[] installments = txtInstallments.Text.Split('-');
        //    x = Convert.ToInt32(installments[0]);
        //    y = Convert.ToInt32(installments[1]);
        //}
        //else
        //{
        //    x = y = Convert.ToInt32(txtInstallments.Text);
        //}
        //for (int i = x; i <= y; i++)
        //{
        //    double semAmt = 0.0;
        //    string semID = "";

        //    SPSEnt = new STUDENT_PAY_SCHEDULE();
        //    EntityList InstList = new EntityList();
        //    SPSEnt.STUDENT_ID = lblStudentId.Text;
        //    SPSEnt.INSTALLMENT_NO = i.ToString();
        //    SPSEnt.STATUS = "1";
        //    InstList = SPSSer.GetAll(SPSEnt, DT);
        //    foreach (STUDENT_PAY_SCHEDULE sp in InstList)
        //    {
        //        #region to calculate paid amt in a sem
        //        semAmt += Convert.ToDouble(sp.AMOUNT) - Convert.ToDouble(sp.DISCOUNT);

        //        semID = sp.SEMESTER;
        //        #endregion

        //    }

        //}
        #endregion

        #region to insert/update in receipt_holding_bal if payable amt exceeds paid amt and when prev remaining bal paid then amount=0
        if (Convert.ToDouble(txtAmount.Text) < Convert.ToDouble(lblTotalAmt.Text))
        {
            double totAmt = 0.0;
            double totAmtRD = 0.0;
            EntityList spslist = new EntityList();
            RHBEnt = new RECEIPT_HOLDING_BAL();
            RHBEnt.STUDENT_ID = lblStudentId.Text;
            RHBEnt = (RECEIPT_HOLDING_BAL)RHBSer.GetSingle(RHBEnt);
            if (RHBEnt != null)
            {
                #region to update Student_pay_schedule status to 2 wherever the paid balance exceeds student_pay_schedule amt
                if (RHBEnt.AMOUNT == "0")
                {
                    totAmtRD = totAmt = Convert.ToDouble(txtAmount.Text);
                }
                else
                {
                    totAmtRD = totAmt = Convert.ToDouble(RHBEnt.AMOUNT);
                }

                SPSEnt = new STUDENT_PAY_SCHEDULE();
                SPSEnt.STUDENT_ID = lblStudentId.Text;
                SPSEnt.STATUS = "1";
                spslist = SPSSer.GetAll(SPSEnt);
                if (spslist.Count > 0)
                {
                    foreach (STUDENT_PAY_SCHEDULE sps in spslist)
                    {
                        #region to update Student_pay_schedule status to 2
                        if (totAmt >= (Convert.ToDouble(sps.AMOUNT) - Convert.ToDouble(sps.DISCOUNT)))
                        {
                            sps.STATUS = "2";
                            SPSSer.Update(sps, DT);
                            totAmt = totAmt - (Convert.ToDouble(sps.AMOUNT) - Convert.ToDouble(sps.DISCOUNT));
                        }
                        else
                        {
                            RHBEnt.AMOUNT = totAmt.ToString();
                            RHBSer.Update(RHBEnt, DT);
                        }
                        #endregion

                        #region to insert in receipt_detail
                        if (totAmtRD >= (Convert.ToDouble(sps.AMOUNT) - Convert.ToDouble(sps.DISCOUNT)))
                        {
                            RDEnt = new RECEIPT_DETAIL();
                            RDEnt.RECEIPT_SNO = receiptid;
                            RDEnt.AMOUNT = (Convert.ToDouble(sps.AMOUNT) - Convert.ToDouble(sps.DISCOUNT)).ToString();
                            RDEnt.SEMESTER = sps.SEMESTER;
                            RDSer.Insert(RDEnt, DT);
                            totAmtRD = totAmtRD - (Convert.ToDouble(sps.AMOUNT) - Convert.ToDouble(sps.DISCOUNT));
                        }
                        else
                        {
                            RDEnt = new RECEIPT_DETAIL();
                            RDEnt.RECEIPT_SNO = receiptid;
                            RDEnt.AMOUNT = totAmtRD.ToString();
                            RDEnt.SEMESTER = sps.SEMESTER;
                            RDSer.Insert(RDEnt, DT);
                        }

                        #endregion

                    }
                }
                else
                {
                    HelperFunction.MsgBox(this, this.GetType(), "Student not Found in Pay Schedule");
                }
                #endregion

            }

        }
        else if (Convert.ToDouble(txtAmount.Text) == Convert.ToDouble(lblTotalAmt.Text))
        {
            double totAmt = 0.0;
            double totAmtRD = 0.0;
            EntityList spslist = new EntityList();
            RHBEnt = new RECEIPT_HOLDING_BAL();
            RHBEnt.STUDENT_ID = lblStudentId.Text;
            RHBEnt = (RECEIPT_HOLDING_BAL)RHBSer.GetSingle(RHBEnt);
            if (RHBEnt != null)
            {
                #region to update Student_pay_schedule status to 2 wherever the paid balance exceeds student_pay_schedule amt
                if (RHBEnt.AMOUNT == "0")
                {
                    totAmtRD = totAmt = Convert.ToDouble(txtAmount.Text);
                }
                else
                {
                    totAmtRD = totAmt = Convert.ToDouble(txtAmount.Text) + Convert.ToDouble(RHBEnt.AMOUNT);
                }
                SPSEnt = new STUDENT_PAY_SCHEDULE();
                SPSEnt.STUDENT_ID = lblStudentId.Text;
                SPSEnt.STATUS = "1";
                spslist = SPSSer.GetAll(SPSEnt);
                if (spslist.Count > 0)
                {
                    foreach (STUDENT_PAY_SCHEDULE sps in spslist)
                    {
                        if (totAmt >= (Convert.ToDouble(sps.AMOUNT) - Convert.ToDouble(sps.DISCOUNT)))
                        {
                            sps.STATUS = "2";
                            SPSSer.Update(sps, DT);
                            totAmt = totAmt - (Convert.ToDouble(sps.AMOUNT) - Convert.ToDouble(sps.DISCOUNT));
                        }
                        else
                        {
                            RHBEnt.AMOUNT = (Convert.ToDouble(sps.AMOUNT) - Convert.ToDouble(sps.DISCOUNT)).ToString();
                            RHBSer.Update(RHBEnt, DT);
                        }
                        #region to insert in receipt_detail
                        if (totAmtRD >= (Convert.ToDouble(sps.AMOUNT) - Convert.ToDouble(sps.DISCOUNT)))
                        {

                            RDEnt = new RECEIPT_DETAIL();
                            RDEnt.RECEIPT_SNO = receiptid;
                            //RDEnt.AMOUNT = totAmt.ToString();
                            RDEnt.AMOUNT = (Convert.ToDouble(sps.AMOUNT) - Convert.ToDouble(sps.DISCOUNT) - Convert.ToDouble(RHBEnt.AMOUNT)).ToString();
                            RDEnt.SEMESTER = sps.SEMESTER;
                            RDSer.Insert(RDEnt, DT);
                            totAmtRD = totAmtRD - Convert.ToDouble(sps.AMOUNT) - Convert.ToDouble(sps.DISCOUNT);
                        }
                        else
                        {
                            RDEnt = new RECEIPT_DETAIL();
                            RDEnt.RECEIPT_SNO = receiptid;
                            RDEnt.AMOUNT = totAmtRD.ToString();
                            //RDEnt.AMOUNT = (Convert.ToDouble(sps.AMOUNT) - Convert.ToDouble(sps.DISCOUNT)).ToString();
                            RDEnt.SEMESTER = sps.SEMESTER;
                            RDSer.Insert(RDEnt, DT);
                        }

                        #endregion
                    }
                }
                else
                {
                    HelperFunction.MsgBox(this, this.GetType(), "Student not Found in Pay Schedule");
                }
                #endregion

                RHBEnt.AMOUNT = "0";
                RHBSer.Update(RHBEnt, DT);
            }

        }


        #endregion

        #region update status as 1 in Master_Bill table

        EntityList newList = new EntityList();

        MBLEnt = new masterbill();
        MBLEnt.MBILL_ID = txtRefBillNo.Text;
        MBLEnt = (masterbill)MBLSer.GetSingle(MBLEnt);
        if (MBLEnt != null)
        {

            MBLEnt.STATUS = "1";
            MBLSer.Update(MBLEnt, DT);
        }

        #endregion



        if (DT.HAPPY == true)
        {
            DT.Commit();

            //ToSendSMS();

            HelperFunction.MsgBox(this, this.GetType(), "Successfully Receipt");
            LoadtoPrint(txtReceiptNo.Text, txtReceiptDate.Text);
            hide.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "printPartOfPage();", true);
            clearFields();
        }
        else
        {
            DT.Abort();
            hide.Visible = false;
            HelperFunction.MsgBox(this, this.GetType(), "Data Not Saved");
        }
        DT.Dispose();
        LoadData();
    }

    protected void ToSendSMS()
    {
        string smsnumber = hf.getcontactofStudentSingle(lblStudentId.Text, "", lblBatchYear.Text, "Both");

        if (smsnumber != "" && smsnumber != null)
        {
            string message = "";

            string[] nepdate = txtDate.Text.Split('/');
            string formonth = txtForMonths.Text + " " + nepdate[2];


            DataTable dt = new DataTable();
            dt = hf.getRemBal(lblStudentId.Text, "0");
            double remaining = Convert.ToDouble(dt.Rows[0][1].ToString());

            //double remaining = Convert.ToDouble(lblGrandTotal.Text) - (Convert.ToDouble(txtAmount.Text) + Convert.ToDouble(lblRemainingAmt.Text));

            if (remaining >= 0)
            {
                message = "Dear " + lblStudentName.Text + ", your payment has been received." + Environment.NewLine + "Fee bill: " + formonth + Environment.NewLine + "Amount Received: Rs. " + txtAmount.Text + Environment.NewLine + "Balance Remaining: " + remaining.ToString() + Environment.NewLine + "Thank you - NCFT";
            }
            else
            {
                message = "Dear " + lblStudentName.Text + ", your payment has been received." + Environment.NewLine + "Fee bill: " + formonth + Environment.NewLine + "Amount Received: Rs. " + txtAmount.Text + Environment.NewLine + "Advance: " + (remaining * -1).ToString() + Environment.NewLine + "Thank you - NCFT";
            }

            string status = hf.SendSms(smsnumber, message);
            if (status == "Success")
            {
                SMSEnt = new sms_record();
                SMSEnt.FACULTY = "1";
                SMSEnt.PROGRAM = "1";
                SMSEnt.SEMESTER = "";
                SMSEnt.SECTION = "";
                SMSEnt.MESSAGE = message;
                SMSEnt.PHONE_NUMBERS = smsnumber;
                SMSEnt.SEND_DATE = System.DateTime.Today.ToString("dd/MM/yyyy");
                userprofile = (UserProfileEntity)HttpContext.Current.Session["UserProfile"];
                SMSEnt.SEND_BY = userprofile.UserName;
                SMSEnt.SEND_MODULE = "Account";

                SMSSer.Insert(SMSEnt);


            }
            else
            {

            }
        }
    }

    protected void clearFields()
    {

        hide.Visible = true;
        NEnt = new HSS_NAME();

        NEnt = (HSS_NAME)NSer.GetSingle(NEnt);
        lblCollegeName.Text = NEnt.NAME;
        lblAddress.Text = NEnt.ADRESS;
        lblPhoneNo.Text = NEnt.CONTACT;
        lblEmail.Text = NEnt.WEBSITE;

        loadReceiptNo();

        LoadFaculty();
        LoadLevel();
        LoadProgram();


        txtReceiptDate.Text = "";
        txtDate.Text = "";
        ddlPaymentType.SelectedIndex = 0;


        txtAmount.Text = "";

        txtRemarks.Text = "";
        txtChequeDate.Text = "";
        txtChequeNo.Text = "";
        txtBankName.Text = "";
        rbtnPaymentMode.SelectedIndex = 0;
        //rbtnSendSMS.SelectedIndex = 0;


    }
    protected void LoadData()
    {
        REnt = new Receipt();
        //string[] st_name = txtStudentId.Text.Split('-');
        REnt.BATCHNO = lblBatchYear.Text;
        REnt.STUDENTID = ddlStudentName.SelectedValue;
        REnt.CANCEL_STATUS = "0";
        grdPayment.DataSource = RSer.GetAll(REnt);
        grdPayment.DataBind();

        if (grdPayment.Rows.Count != 0)
        {
            grdPayment.Visible = true;
        }
        else
        {
            grdPayment.Visible = false;
        }
    }

    protected void LoadtoPrint(string receiptno, string date)
    {
        lblReceiptNo.Text = "";
        lblDate.Text = "";
        lblAmount.Text = "";

        lblAmountWord.Text = "";
        lblCash.Text = "";
        lblName.Text = "";
        lblUserName.Text = "";
        lblDrownDate.Text = "";
        lblBillNo.Text = "";
        lblSemesterP.Text = "";
        lblRemarks.Text = "";
        REnt = new Receipt();
        REnt.RECEIPTNO = receiptno;
        REnt.RECEIPTDATEEN = date;
        REnt = (Receipt)RSer.GetSingle(REnt);
        if (REnt != null)
        {
            lblReceiptNo.Text = REnt.RECEIPTNO;
            lblDate.Text = REnt.RECEIPTDATEEN;
            lblDateNep.Text = REnt.DAY + "/" + REnt.MONTH + "/" + REnt.YEAR;
            lblAmount.Text = "Rs. " + REnt.PAIDAMOUNT + "/-";

            lblAmountWord.Text = hf.NumWordsWrapper(Convert.ToDouble(REnt.PAIDAMOUNT)).ToUpper() + " ONLY";
            lblBillNo.Text = REnt.REFRENCE_BILL_NO;
            //nibesh

            lblSemesterP.Text = "Sem " + lblSemester.Text + "-" + txtInstallments.Text;

            lblRemarks.Text = REnt.REMARKS;

            if (REnt.RECEIPT_MODE == "Cheque")
            {

                lblCash.Text = "Cheque";
                lblDrownDate.Text = "Vide Cheque No: " + REnt.CHEQUE_NO + "Drawn on " + REnt.BANK_NAME + " Dated " + REnt.CHEQUE_DATE;

                lblDrownDate.Visible = true;
                tr_drownon.Visible = true;
            }
            else if (REnt.RECEIPT_MODE == "Voucher")
            {

                lblCash.Text = "Cash";
                lblDrownDate.Text = "Vide Voucher No: " + REnt.CHEQUE_NO + " Drawn on " + REnt.BANK_NAME + " Dated " + REnt.CHEQUE_DATE;

                lblDrownDate.Visible = true;
                tr_drownon.Visible = true;
            }
            else
            {
                lblCash.Text = "Cash";
                lblDrownDate.Text = "";

                lblDrownDate.Visible = false;
                tr_drownon.Visible = false;
            }

            SIEnt = new HSS_STUDENT();
            SIEnt.STUDENT_ID = REnt.STUDENTID;
            SIEnt = (HSS_STUDENT)SISer.GetSingle(SIEnt);
            if (SIEnt != null)
            {
                CSEnt = new HSS_CURRENT_STUDENT();
                CSEnt.STUDENT_ID = SIEnt.STUDENT_ID;
                CSEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSEnt);
                if (CSEnt != null)
                {
                    lblName.Text = SIEnt.NAME_ENGLISH + " - " + SIEnt.STUDENT_ID + " (" + SIEnt.BAT_CH + "-" + CSEnt.SECTION + ")";
                }
            }
            lblUserName.Text = "Received By: " + userprofile.EmployeeName;
        }
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFaculty.SelectedValue != "Select")
        {
            LoadProgram();
        }
        else
        {
            ddlProgram.Items.Clear();
            ddlProgram.Items.Insert(0, "Select");

            ddlBatch.Items.Clear();
            ddlBatch.Items.Insert(0, "Select");

            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Select");

            ddlStudentName.Items.Clear();
            ddlStudentName.Items.Insert(0, "Select");
        }

        if (ddlProgram.SelectedValue == "Select")
        {
            ddlBatch.Items.Clear();
            ddlBatch.Items.Insert(0, "Select");

            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Select");

            ddlStudentName.Items.Clear();
            ddlStudentName.Items.Insert(0, "Select");
        }
    }
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProgram.SelectedValue != "Select")
        {
            LoadBatch();
            LoadSemester();


        }

    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedValue != "Select")
        {

            LoadStudentName();

        }
        else
        {
            ddlStudentName.Items.Clear();
            ddlStudentName.Items.Insert(0, "Select");
        }
    }

    protected void txtRefBillNo_TextChanged(object sender, EventArgs e)
    {
        MBLEnt = new masterbill();
        MBLEnt.MBILL_ID = txtRefBillNo.Text;
        MBLEnt = (masterbill)MBLSer.GetSingle(MBLEnt);
        if (MBLEnt != null)
        {
            txtInstallments.Text = MBLEnt.INSTALLMENT;
            if (MBLEnt.STATUS != "2")
                LoadStudentDetail(txtRefBillNo.Text);
            else
            {
                HelperFunction.MsgBox(this, this.GetType(), "You have entered old reference bill no. Please enter new reference no.");
            }
        }

    }

    protected void LoadStudentDetail(string RefBillNo)
    {

        MBLEnt = new masterbill();
        MBLEnt.MBILL_ID = RefBillNo;
        MBLEnt = (masterbill)MBLSer.GetSingle(MBLEnt);
        if (MBLEnt != null)
        {
            txtBillNo.Text = MBLEnt.BILLNO;
            txtStudentId.Text = MBLEnt.STUDENT_ID;
            txtForMonths.Text = MBLEnt.FOR_MONTHS;

            PEnt = new program();
            PEnt.PK_ID = MBLEnt.PROGRAM;
            PEnt = (program)PSer.GetSingle(PEnt);
            if (PEnt != null)
            {
                LoadFaculty();
                ddlFaculty.SelectedValue = PEnt.FACULTY_ID;
                LoadLevel();
                ddlLevel.SelectedValue = PEnt.PROGRAM_LEVEL;
            }
            LoadProgram();
            ddlProgram.SelectedValue = MBLEnt.PROGRAM;
            LoadBatch();
            ddlBatch.SelectedValue = MBLEnt.BATCH;
            LoadSemester();
            ddlSemester.SelectedValue = MBLEnt.SEMESTER;

            SIEnt = new HSS_STUDENT();
            SIEnt.STUDENT_ID = txtStudentId.Text;
            SIEnt = (HSS_STUDENT)SISer.GetSingle(SIEnt);
            if (SIEnt != null)
            {
                LoadStudentName();
                ddlStudentName.SelectedValue = SIEnt.STUDENT_ID;

            }


            #region calculating remaining balance

            double remaining = 0;

            SAEnt = new STUDENT_ACCOUNT();
            SAEnt.STUDENT_ID = txtStudentId.Text;
            SAEnt = (STUDENT_ACCOUNT)SASer.GetSingle(SAEnt);
            if (SAEnt != null)
            {
                MBLEnt = new masterbill();
                MBLEnt.MBILL_ID = RefBillNo;
                MBLEnt = (masterbill)MBLSer.GetSingle(MBLEnt);
                if (MBLEnt != null)
                {

                    if (MBLEnt.STATUS == "0")
                    {

                        lblInstallmentAmt.Text = (Convert.ToDouble(MBLEnt.GRANDTOTAL) - Convert.ToDouble(MBLEnt.DISCOUNT)).ToString();
                        lblRemainingAmt.Text = MBLEnt.REMAINING_BALANCE;
                        lblTotalAmt.Text = MBLEnt.F_GRANDTOTAL;
                    }

                    else
                    {

                        DataTable dt = new DataTable();
                        dt = hf.getRemBal(txtStudentId.Text, "");
                        try
                        {
                            remaining = Convert.ToDouble(dt.Rows[0][1].ToString());
                        }
                        catch
                        { }

                        lblInstallmentAmt.Text = "0";
                        lblRemainingAmt.Text = (remaining).ToString("#0.00");
                        try
                        {
                            lblTotalAmt.Text = ((remaining) + Convert.ToDouble(hf.getMasterBillTotal(txtStudentId.Text, ddlSemester.SelectedValue, "", ""))).ToString();
                        }
                        catch
                        {

                            lblTotalAmt.Text = remaining.ToString();
                        }


                    }
                }
            }

            #endregion


            LoadDetail();
            getForMonth();
        }
        else
        {

            //btnPromote.Visible = false;
            //btnResignation.Visible = false;
            string tempid = txtStudentId.Text;
            clearFields();
            txtStudentId.Text = tempid;
        }

    }

    protected void grdPayment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblSem = e.Row.FindControl("lblSem") as Label;
            Label lblSemCode = e.Row.FindControl("lblSemCode") as Label;

            SMEnt = new semester();
            SMEnt.PK_ID = lblSem.Text;
            SMEnt = (semester)SMSer.GetSingle(SMEnt);
            if (SMEnt != null)
            {
                lblSemCode.Text = SMEnt.SEMESTER_CODE;
            }
        }

    }
}