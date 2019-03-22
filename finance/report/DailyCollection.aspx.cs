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

public partial class finance_report_DailyCollection : System.Web.UI.Page
{

    HSS_NAME NEnt = new HSS_NAME();
    HSS_NAMEService NSer = new HSS_NAMEService();

    program PEnt = new program();
    programService PSer = new programService();


    HelperFunction hf = new HelperFunction();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadProgram();
        }
    }

    protected void loadProgram()
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

        gridDailyCollection.DataSource = hf.getDailyCollection(txtFDate.Text, txtTDate.Text, hf.checkFiscalYear(hf.NepaliMonth(), hf.NepaliYear()), ddlProgram.SelectedValue);
        gridDailyCollection.DataBind();


        gridDailyWithDraw.DataSource = hf.getDailyWithdraw(txtFDate.Text, txtTDate.Text, hf.checkFiscalYear(hf.NepaliMonth(), hf.NepaliYear()));
        gridDailyWithDraw.DataBind();


        if (gridDailyCollection.Rows.Count != 0)
        {
            hide.Visible = true;
            LoadCompanyDetail();
            lblFDate.Text = txtFDate.Text;
            lblTDate.Text = txtTDate.Text;
            lblReport.Text = "Daily Collection";
        }
        else
        {
            hide.Visible = false;

        }

        if (gridDailyWithDraw.Rows.Count != 0)
        {
            tr_withdraw.Visible = true;
            tr_withdraw_detail.Visible = true;
            LoadCompanyDetail();
            lblFDate.Text = txtFDate.Text;
            lblTDate.Text = txtTDate.Text;
            lblReport.Text = "Daily Collection";
        }
        else
        {
            tr_withdraw.Visible = false;
            tr_withdraw_detail.Visible = false;

        }



    }

    protected void LoadCompanyDetail()
    {
        NEnt = new HSS_NAME();
        NEnt = (HSS_NAME)NSer.GetSingle(NEnt);
        if (NEnt != null)
        {
            lblCompanyName.Text = NEnt.NAME;
            lblAddress.Text = NEnt.ADRESS;
            lblContactNo.Text = NEnt.CONTACT;
            lblWebsite.Text = NEnt.WEBSITE;
        }
    }




    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "printPartOfPage();", true);
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
}