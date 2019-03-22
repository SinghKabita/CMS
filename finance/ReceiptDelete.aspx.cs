using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;
using DataHelper.Framework;
using Entity.Framework;

public partial class finance_ReceiptDelete : System.Web.UI.Page
{

    Amount_collection AMCEnt = new Amount_collection();
    Amount_collectionService AMCSer = new Amount_collectionService();

    STUDENT_PAY_SCHEDULE SPSEnt = new STUDENT_PAY_SCHEDULE();
    STUDENT_PAY_SCHEDULEService SPSSer = new STUDENT_PAY_SCHEDULEService();

    Receipt REnt = new Receipt();
    ReceiptService RSer = new ReceiptService();

    HelperFunction hf = new HelperFunction();
    DistributedTransaction DT = new DistributedTransaction();

    bill BLEnt = new bill();
    billService BLSer = new billService();

    masterbill MBLEnt = new masterbill();
    masterbillService MBLSer = new masterbillService();

    STUDENT_ADVANCE ADVEnt = new STUDENT_ADVANCE();
    STUDENT_ADVANCEService ADVSer = new STUDENT_ADVANCEService();

    UserProfileEntity userProfile = new UserProfileEntity();

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void LoadData()
    {
        gridReceiptList.DataSource = hf.getreceiptlist(txtReceiptDate.Text);
        gridReceiptList.DataBind();


        if (gridReceiptList.Rows.Count > 0)
        {
            monthly_head.Visible = true;
        }
        else
        {
            monthly_head.Visible = false;
        }


    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadInfo();
    }
    protected void btnDelete_Click(object sender, EventArgs e)
    {


        userProfile = (UserProfileEntity)HttpContext.Current.Session["UserProfile"];


        EntityList theList = new EntityList();
        EntityList particularlist = new EntityList();
        DT = new DistributedTransaction();

        #region calling the function to update the status of std_pay_schedule of the particular


        #region for single installment receipt
        if (lblInstallmentNoP.Text.Length < 3)
        {
            particularlist = new EntityList();
            SPSEnt = new STUDENT_PAY_SCHEDULE();
            SPSEnt.STUDENT_ID = lblStudentIdP.Text;
            SPSEnt.INSTALLMENT_NO = lblInstallmentNoP.Text;
            particularlist = SPSSer.GetAll(SPSEnt, DT);

            foreach (STUDENT_PAY_SCHEDULE sps in particularlist)
            {
                SPSEnt = new STUDENT_PAY_SCHEDULE();
                SPSEnt.STUDENT_ID = sps.STUDENT_ID;
                SPSEnt.INSTALLMENT_NO = sps.INSTALLMENT_NO;
                SPSEnt.PARTICULARS = sps.PARTICULARS;
                SPSEnt = (STUDENT_PAY_SCHEDULE)SPSSer.GetSingle(SPSEnt, DT);
                if (SPSEnt != null)
                {
                    SPSEnt.STATUS = "1";
                    SPSSer.Update(SPSEnt, DT);
                }
            }
        }
        #endregion


        #region for multiple installment receipt
        else
        {
            string[] installments = lblInstallmentNoP.Text.Split('-');
            for (int i = Convert.ToInt32(installments[0]); i <= Convert.ToInt32(installments[1]); i++)
            {
                particularlist = new EntityList();
                SPSEnt = new STUDENT_PAY_SCHEDULE();
                SPSEnt.STUDENT_ID = lblStudentIdP.Text;
                SPSEnt.INSTALLMENT_NO = i.ToString();
                particularlist = SPSSer.GetAll(SPSEnt, DT);


                foreach (STUDENT_PAY_SCHEDULE sps in particularlist)
                {
                    SPSEnt = new STUDENT_PAY_SCHEDULE();
                    SPSEnt.STUDENT_ID = sps.STUDENT_ID;
                    SPSEnt.INSTALLMENT_NO = sps.INSTALLMENT_NO;
                    SPSEnt.PARTICULARS = sps.PARTICULARS;
                    SPSEnt = (STUDENT_PAY_SCHEDULE)SPSSer.GetSingle(SPSEnt, DT);
                    if (SPSEnt != null)
                    {
                        SPSEnt.STATUS = "1";
                        SPSSer.Update(SPSEnt, DT);
                    }
                }
            }
        }
        #endregion

        #endregion


        #region to delete the advance amount of a student from student advance for the particular receipt no
        ADVEnt = new STUDENT_ADVANCE();
        ADVEnt.STUDENT_ID = lblStudentIdP.Text;
        ADVEnt.RECEIPT_ID = lblReceiptIdP.Text;
        ADVEnt.RECEIPT_NO = lblReceiptNoP.Text;
        ADVEnt = (STUDENT_ADVANCE)ADVSer.GetSingle(ADVEnt, DT);
        if (ADVEnt != null)
        {
            ADVSer.Delete(ADVEnt, DT);
        }
        #endregion


        #region to update the receipt table according to pkid for the cancelled receipt

        REnt = new Receipt();
        REnt.SNO = lblReceiptIdP.Text;
        REnt = (Receipt)RSer.GetSingle(REnt, DT);
        if (REnt != null)
        {
            REnt.CANCEL_STATUS = "1";
            REnt.CANCEL_DATEEN = hf.GetTodayDate();

            REnt.CANCEL_DAY = hf.NepaliDay();
            REnt.CANCEL_MONTH = hf.NepaliMonth();
            REnt.CANCEL_YEAR = hf.NepaliYear();
            REnt.CANCEL_FISCAL_YEAR = hf.checkFiscalYear(hf.NepaliMonth(), hf.NepaliYear());
            REnt.CANCEL_BY = userProfile.UserName;
            REnt.CANCEL_REASON = txtDeleteRemarks.Text;
            RSer.Update(REnt, DT);
        }


        #endregion




        if (DT.HAPPY == true)
        {
            DT.Commit();
            LoadData();
            HelperFunction.MsgBox(this, this.GetType(), "Successfully Delete");
        }
        else
        {
            DT.Abort();
            HelperFunction.MsgBox(this, this.GetType(), "Something goes wrong. Please try again.");
        }
        DT.Dispose();

    }
    protected void txtReceiptDate_TextChanged(object sender, EventArgs e)
    {
        if (txtReceiptDate.Text != "")
        {
            string[] nepdate = hf.ConvertEnglishToNepali(txtReceiptDate.Text);
            txtDay.Text = nepdate[0];
            txtMonth.Text = nepdate[1];
            txtYear.Text = nepdate[2];
        }


    }

    protected void LoadInfo()
    {
        if (txtReceiptDate.Text != "")
        {
            LoadData();


            if (gridReceiptList.Rows.Count == 0)
            {
                HelperFunction.MsgBox(this, this.GetType(), "No Data Found.");
            }
        }
        else
        {
            HelperFunction.MsgBox(this, this.GetType(), "Please Enter Receipt Date.");
        }
    }

    protected void gridReceiptList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Remove"))
        {
            GridViewRow gr = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            Label lblReceiptNo = gr.FindControl("lblReceiptNo") as Label;
            Label lblReceiptId = gr.FindControl("lblReceiptId") as Label;
            Label lblStudentId = gr.FindControl("lblStudentId") as Label;
            Label lblStudentName = gr.FindControl("lblStudentName") as Label;
            Label lblInstallment = gr.FindControl("lblInstallment") as Label;
            Label lblBatch = gr.FindControl("lblBatch") as Label;

            lblReceiptIdP.Text = lblReceiptId.Text;
            lblReceiptNoP.Text = lblReceiptNo.Text;
            lblStudentIdP.Text = lblStudentId.Text;
            lblStudentNameP.Text = lblStudentName.Text;
            lblBatchP.Text = lblBatch.Text;
            lblInstallmentNoP.Text = lblInstallment.Text;


            GetRemarks_ModalPopupExtender.Show();

        }
    }


    protected void txtDay_TextChanged(object sender, EventArgs e)
    {
        txtReceiptDate.Text = GetEnglishDate();
        txtMonth.Focus();

    }
    protected void txtMonth_TextChanged(object sender, EventArgs e)
    {
        txtReceiptDate.Text = GetEnglishDate();
        txtYear.Focus();

    }
    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        txtReceiptDate.Text = GetEnglishDate();


    }

    protected string GetEnglishDate()
    {
        string engdate = "";
        if (txtDay.Text != "" && txtMonth.Text != "" && txtYear.Text != "")
        {
            try
            {
                engdate = hf.ConvertNepaliTOEnglish(txtDay.Text, txtMonth.Text, txtYear.Text);


            }
            catch { }
        }
        return engdate;
    }
}