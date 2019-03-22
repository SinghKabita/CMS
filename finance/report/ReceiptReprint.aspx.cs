using Entity.Components;
using Service.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class finance_report_ReceiptReprint : System.Web.UI.Page
{
    HSS_NAME NEnt = new HSS_NAME();
    HSS_NAMEService NSer = new HSS_NAMEService();

    Receipt REnt = new Receipt();
    ReceiptService RSer = new ReceiptService();

    HSS_STUDENT SIEnt = new HSS_STUDENT();
    HSS_STUDENTService SISer = new HSS_STUDENTService();

    HSS_CURRENT_STUDENT CSEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSer = new HSS_CURRENT_STUDENTService();

    program PEnt = new program();
    programService PSer = new programService();

    HelperFunction hf = new HelperFunction();
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            LoadProgram();

            hide.Visible = true;
            NEnt = new HSS_NAME();

            NEnt = (HSS_NAME)NSer.GetSingle(NEnt);
            lblCollegeName.Text = NEnt.NAME;
            lblCollgeSignP.Text = "For:" + NEnt.NAME;
            lblAddress.Text = NEnt.ADRESS;
            lblPhoneNo.Text = NEnt.CONTACT;
            lblEmail.Text = NEnt.WEBSITE;
        }
    }

    protected void LoadProgram()
    {
        PEnt = new program();
        ddlProgram.DataSource = PSer.GetAll(PEnt);
        ddlProgram.DataTextField = "PROGRAM_CODE";
        ddlProgram.DataValueField = "PK_ID";
        ddlProgram.DataBind();
        ddlProgram.Items.Insert(0, "Select");
    }


    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadData();
    }

    private void LoadData()
    {

        gridDailyCollection.DataSource = hf.getDailyCollection(txtFDate.Text, txtTDate.Text, hf.checkFiscalYear(hf.NepaliMonth(), hf.NepaliYear()),ddlProgram.SelectedValue);
        gridDailyCollection.DataBind();


        gridDailyWithDraw.DataSource = hf.getDailyWithdraw(txtFDate.Text, txtTDate.Text, hf.checkFiscalYear(hf.NepaliMonth(), hf.NepaliYear()));
        gridDailyWithDraw.DataBind();


        if (gridDailyCollection.Rows.Count != 0)
        {
            hide.Visible = true;
        }
        else
        {
            hide.Visible = false;

        }

        if (gridDailyWithDraw.Rows.Count != 0)
        {
            tr_withdraw.Visible = true;
            tr_withdraw_detail.Visible = true;
        }
        else
        {
            tr_withdraw.Visible = false;
            tr_withdraw_detail.Visible = false;

        }



    }
    



    protected void gridDailyCollection_RowDataBound(object sender, GridViewRowEventArgs e)
    {


        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPaidAmount = e.Row.FindControl("lblPaidAmount") as Label;

            lblPaidAmount.Text = Convert.ToDouble(lblPaidAmount.Text).ToString("#0.00");

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            double total = 0;
            Label lblTotal = e.Row.FindControl("lblTotal") as Label;

            foreach (GridViewRow gr in gridDailyCollection.Rows)
            {
                Label lblPaidAmount = gr.FindControl("lblPaidAmount") as Label;

                total = total + Convert.ToDouble(lblPaidAmount.Text);
            }

            lblTotal.Text = total.ToString("#0.00");
        }


    }

    protected void gridDailyWithDraw_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblPaidAmount = e.Row.FindControl("lblPaidAmount") as Label;

            lblPaidAmount.Text = Convert.ToDouble(lblPaidAmount.Text).ToString("#0.00");

        }

        if (e.Row.RowType == DataControlRowType.Footer)
        {
            double total = 0;
            Label lblTotal = e.Row.FindControl("lblTotal") as Label;

            foreach (GridViewRow gr in gridDailyWithDraw.Rows)
            {
                Label lblPaidAmount = gr.FindControl("lblPaidAmount") as Label;

                total = total + Convert.ToDouble(lblPaidAmount.Text);
            }

            lblTotal.Text = total.ToString("#0.00");
        }
    }

    protected void gridDailyCollection_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Print"))
        {
            GridViewRow gr = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            Label lblReceiptNo = gr.FindControl("lblReceiptNo") as Label;
            Label lblDate = gr.FindControl("lblDate") as Label;
            Label lblDatebs = gr.FindControl("lblDatebs") as Label;
            Label lblStudentName = gr.FindControl("lblStudentName") as Label;
            Label lblStudentId = gr.FindControl("lblStudentId") as Label;
            Label lblBatch = gr.FindControl("lblBatch") as Label;
            Label lblInstallmentNo = gr.FindControl("lblInstallmentNo") as Label;
            Label lblPaidAmount = gr.FindControl("lblPaidAmount") as Label;
            Label lblReceivedBy = gr.FindControl("lblReceivedBy") as Label;
            Label lblRemarks = gr.FindControl("lblRemarks") as Label;
            Label lblRefBillNo = gr.FindControl("lblRefBillNo") as Label;

            double paidAmt = Convert.ToDouble(lblPaidAmount.Text);

            lblReceiptNoP.Text = lblReceiptNo.Text;
            lblDateP.Text = lblDate.Text;
            lblDateNepP.Text = lblDatebs.Text;
            lblNameP.Text = lblStudentName.Text + "-" + lblStudentId.Text + "(" + lblBatch.Text + ")";
            lblSemesterP.Text = lblInstallmentNo.Text;
            lblRemarksP.Text = lblRemarks.Text;
            lblAmountWordP.Text = hf.NumWordsWrapper(Convert.ToDouble(lblPaidAmount.Text)).ToUpper() + " ONLY";
            lblAmountP.Text = "Rs. " + paidAmt.ToString("#0") + "/-";
            lblUserNameP.Text = "Received By: " + lblReceivedBy.Text;
            lblBillNoP.Text = lblRefBillNo.Text;

            hide.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "", "printPartOfPage();", true);
        }
    }
}