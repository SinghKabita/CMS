using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;

public partial class frontdesk_visitors_LogBookReport : System.Web.UI.Page
{
    HSS_NAME NEnt = new HSS_NAME();
    HSS_NAMEService NSer = new HSS_NAMEService();

    HelperFunction hf = new HelperFunction();

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadData();
    }

    private void LoadData()
    {

        griLogList.DataSource = hf.getLogBookReport(txtName.Text, txtFDate.Text, txtTDate.Text);
        //griLogList.DataSource = hf.getLogBookReport( txtFDate.Text, txtTDate.Text);
        griLogList.DataBind();

        if (griLogList.Rows.Count != 0)
        {
            hide.Visible = true;
            LoadCompanyDetail();
            lblFDate.Text = txtFDate.Text;
            lblTDate.Text = txtTDate.Text;
            lblReport.Text = "Log Book Report";
        }
        else
        {
            hide.Visible = false;

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
}