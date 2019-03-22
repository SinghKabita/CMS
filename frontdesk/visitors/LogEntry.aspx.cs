using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;
using DataHelper.Framework;


public partial class frontdesk_visitors_LogEntry : System.Web.UI.Page
{

    VISITORS_LOG VLEnt = new VISITORS_LOG();
    VISITORS_LOGService VLSer = new VISITORS_LOGService();

    HelperFunction hf = new HelperFunction();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtDate.Text = hf.GetTodayDate();
            LoadData();

        }
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (txtDate.Text == "")
        {
            HelperFunction.MsgBox(this, this.GetType(), "Please Enter Date.");
        }

        else if (txtVisitorsName.Text == "")
        {
            HelperFunction.MsgBox(this, this.GetType(), "Please Enter Visitors Name.");
        }


        else
        {
            VLEnt = new VISITORS_LOG();
            VLEnt.VISIT_DATE = txtDate.Text;
            string[] nepdate = hf.ConvertEnglishToNepali(txtDate.Text);
            VLEnt.VISIT_DAY = nepdate[0];
            VLEnt.VISIT_MONTH = nepdate[1];
            VLEnt.VISIT_YEAR = nepdate[2];
            VLEnt.VISIT_FISCAL_YEAR = hf.checkFiscalYear(nepdate[1], nepdate[2]);
            VLEnt.VISITORS_NAME = txtVisitorsName.Text;
            VLEnt.ORGANIZATION = txtOrganization.Text;
            VLEnt.PURPOSE = txtPurpose.Text;
            VLEnt.CONTACT_NO = txtPhoneNo.Text;
            VLEnt.VECHICLE_NO = txtVehicleNo.Text;
            VLEnt.TIME_IN = txtTimeIn.Text;
            VLEnt.TIME_OUT = txtTimeOut.Text;

            if (VLSer.Insert(VLEnt) >= 1)
            {
                HelperFunction.MsgBox(this, this.GetType(), "Entry Successfull");
                ClearFields();
                LoadData();
            }
            else
            {
                HelperFunction.MsgBox(this, this.GetType(), "Not Inserted");
            }
        }
    }

    protected void LoadData()
    {
        VLEnt = new VISITORS_LOG();
        VLEnt.VISIT_DATE = txtDate.Text;
        griLogList.DataSource = VLSer.GetAll(VLEnt);
        griLogList.DataBind();
    }

    protected void ClearFields()
    {
        txtVisitorsName.Text = "";
        txtOrganization.Text = "";
        txtPurpose.Text = "";
        txtPhoneNo.Text = "";
        txtVehicleNo.Text = "";
        txtTimeIn.Text = "";
        txtTimeOut.Text = "";
    }

    protected void txtDate_TextChanged(object sender, EventArgs e)
    {
        LoadData();
    }
}