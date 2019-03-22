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

public partial class finance_WithdrawEntry : System.Web.UI.Page
{
    BatchYear BYEnt = new BatchYear();
    BatchYearService BYSer = new BatchYearService();

    HSS_STUDENT SIEnt = new HSS_STUDENT();
    HSS_STUDENTService SISer = new HSS_STUDENTService();

    HSS_CURRENT_STUDENT CSEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSer = new HSS_CURRENT_STUDENTService();

    Section SEnt = new Section();
    SectionService SSer = new SectionService();

    Months MEnt = new Months();
    MonthsService MSer = new MonthsService();

    Withdraw WDEnt = new Withdraw();
    WithdrawService WDSer = new WithdrawService();

    HSS_NAME NEnt = new HSS_NAME();
    HSS_NAMEService NSer = new HSS_NAMEService();


    HelperFunction hf = new HelperFunction();

    UserProfileEntity userprofile = new UserProfileEntity();


    DistributedTransaction DT = new DistributedTransaction();


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

            LoadBatch();

            LoadCollegeCode();
        }
    }


    protected void LoadCollegeCode()
    {

        if (ddlBatch.SelectedValue != "Select")
        {

            if (Convert.ToDouble(ddlBatch.SelectedValue) < 15)
            {
                lblCode.Text = "BFD";
            }
            else
            {
                NEnt = new HSS_NAME();
                NEnt = (HSS_NAME)NSer.GetSingle(NEnt);
                if (NEnt != null)
                {
                    lblCode.Text = NEnt.CODE;
                }
            }
        }
        else
        {
            NEnt = new HSS_NAME();
            NEnt = (HSS_NAME)NSer.GetSingle(NEnt);
            if (NEnt != null)
            {
                lblCode.Text = NEnt.CODE;
            }
        }

    }



    protected void LoadBatch()
    {
        BYEnt = new BatchYear();
        BYEnt.ACTIVE = "1";
        ddlBatch.DataSource = BYSer.GetAll(BYEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
        ddlBatch.Items.Insert(0, "Select");
    }

    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadCollegeCode();
        LoadStudentName();
    }

    protected void LoadStudentName()
    {
        EntityList theList = new EntityList();
        EntityList stdList = new EntityList();

        SIEnt = new HSS_STUDENT();
        SIEnt.BAT_CH = ddlBatch.SelectedValue;
        theList = SISer.GetAll(SIEnt);
        foreach (HSS_STUDENT ss in theList)
        {

            CSEnt = new HSS_CURRENT_STUDENT();
            CSEnt.STUDENT_ID = ss.STUDENT_ID;
            CSEnt.BATCH = ddlBatch.SelectedValue;
            CSEnt.STATUS = "1";
            CSEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSEnt);
            if (CSEnt != null)
            {
                stdList.Add(ss);
            }

        }


        ddlStudentName.DataSource = stdList;
        ddlStudentName.DataTextField = "NAME_ENGLISH";
        ddlStudentName.DataValueField = "STUDENT_ID";
        ddlStudentName.DataBind();
        ddlStudentName.Items.Insert(0, "Select");
    }
    protected void ddlStudentName_SelectedIndexChanged(object sender, EventArgs e)
    {

        string studentid = ddlStudentName.SelectedValue;
        if (ddlStudentName.SelectedValue != "Select")
        {
            LoadCollegeCode();
            txtStudentId.Text = studentid.Replace(lblCode.Text, "");
        }
        else
        {
            txtStudentId.Text = "";
        }

        LoadDetail();
    }
    protected void txtStudentId_TextChanged(object sender, EventArgs e)
    {
        fillBatch();

        LoadDetail();
    }

    protected void fillBatch()
    {
        CSEnt = new HSS_CURRENT_STUDENT();
        CSEnt.STUDENT_ID = lblCode.Text + txtStudentId.Text;
        CSEnt.STATUS = "1";
        CSEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSEnt);
        if (CSEnt != null)
        {
            ddlBatch.SelectedValue = CSEnt.BATCH;
            LoadStudentName();
            ddlStudentName.SelectedValue = lblCode.Text + txtStudentId.Text;


        }
        else
        {
            ddlBatch.SelectedIndex = 0;


        }
    }

    protected void LoadDetail()
    {

        SIEnt = new HSS_STUDENT();

        CSEnt = new HSS_CURRENT_STUDENT();

        CSEnt.STUDENT_ID = lblCode.Text + txtStudentId.Text;
        CSEnt.BATCH = ddlBatch.SelectedValue;

        CSEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSEnt);
        if (CSEnt != null)
        {


            labelrow.Visible = true;
            lblSemester.Text = CSEnt.SEMESTER + " " + CSEnt.SECTION;


            lblStudentId.Text = CSEnt.STUDENT_ID;

            SIEnt = new HSS_STUDENT();
            SIEnt.STUDENT_ID = lblCode.Text + txtStudentId.Text;
            SIEnt = (HSS_STUDENT)SISer.GetSingle(SIEnt);
            if (SIEnt != null)
            {
                lblBatchYear.Text = SIEnt.BAT_CH;
                lblStudentName.Text = SIEnt.NAME_ENGLISH;
            }


            txtReceiptDate.Focus();

            LoadData();
        }
        else
        {
            lblSemester.Text = "";
            lblStudentId.Text = "";
            lblBatchYear.Text = "";
            lblStudentName.Text = "";

            txtStudentId.Text = "";
            txtStudentId.Focus();
            labelrow.Visible = false;
            HelperFunction.MsgBox(this, this.GetType(), "Invalid Student Id");
        }
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
        DT = new DistributedTransaction();
        userprofile = (UserProfileEntity)HttpContext.Current.Session["UserProfile"];
        string[] sem = lblSemester.Text.Split(' ');
        string[] chkdate = txtDate.Text.Split('/');

        #region to insert in withdraw

        WDEnt = new Withdraw();
        WDEnt.RECEIPTNO = txtReceiptNo.Text;
        WDEnt.BATCHNO = lblBatchYear.Text;
        WDEnt.SEMESTER = sem[0];
        WDEnt.STUDENTID = lblStudentId.Text;

        WDEnt.DAY = chkdate[0];
        WDEnt.MONTH = chkdate[1];
        WDEnt.YEAR = chkdate[2];
        WDEnt.FISCALYEAR = hf.checkFiscalYear(chkdate[1], chkdate[2]);
        WDEnt.SYSTEMDATE = hf.ConvertNepaliTOEnglish(hf.NepaliDay(), hf.NepaliMonth(), hf.NepaliYear());
        WDEnt.REMARKS = txtRemarks.Text;
        WDEnt.RECEIPTDATEEN = txtReceiptDate.Text;

        WDEnt.PAIDAMOUNT = txtAmount.Text;
        WDEnt.RECEIVED_BY = userprofile.UserName;



        if (rbtnPaymentMode.SelectedValue == "Cheque")
        {
            WDEnt.CHEQUE_DATE = txtChequeDate.Text;
            WDEnt.BANK_NAME = txtBankName.Text;
            WDEnt.CHEQUE_NO = txtChequeNo.Text;
            WDEnt.RECEIPT_MODE = "Cheque";
        }
        else if (rbtnPaymentMode.SelectedValue == "Voucher")
        {
            WDEnt.CHEQUE_DATE = txtChequeDate.Text;
            WDEnt.BANK_NAME = txtBankName.Text;
            WDEnt.CHEQUE_NO = txtChequeNo.Text;
            WDEnt.RECEIPT_MODE = "Voucher";
        }
        else if (rbtnPaymentMode.SelectedValue == "Cash")
        {

            WDEnt.RECEIPT_MODE = "Cash";
        }

        WDEnt.CANCEL_STATUS = "0";

        WDSer.Insert(WDEnt, DT);

        #endregion




        if (DT.HAPPY == true)
        {
            DT.Commit();



            HelperFunction.MsgBox(this, this.GetType(), "Successfully Inserted");



            clearFields();
        }
        else
        {
            DT.Abort();

            HelperFunction.MsgBox(this, this.GetType(), "Data Not Saved");
        }
        DT.Dispose();
        LoadData();
    }





    protected void clearFields()
    {
        txtReceiptDate.Text = "";
        txtDate.Text = "";



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
        WDEnt = new Withdraw();
        //string[] st_name = txtStudentId.Text.Split('-');
        WDEnt.BATCHNO = lblBatchYear.Text;
        WDEnt.STUDENTID = lblCode.Text + txtStudentId.Text;
        WDEnt.CANCEL_STATUS = "0";
        grdPayment.DataSource = WDSer.GetAll(WDEnt);
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


}