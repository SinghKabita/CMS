using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Entity.Components;
using Service.Components;

public partial class uc_test_AdminStaff : System.Web.UI.UserControl
{

    HSS_ADMINSTAFF ADSEnt = new HSS_ADMINSTAFF();
    HSS_ADMINSTAFFService ADSSer = new HSS_ADMINSTAFFService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();

        }
    }

    private void LoadData()
    {
        ADSEnt = new HSS_ADMINSTAFF();
        gridAdminStaff.DataSource = ADSSer.GetAll(ADSEnt);
        //grdGroups.Sort = "GROUPID";
        gridAdminStaff.DataBind();

        if (gridAdminStaff.Rows.Count == 0)
        {
            ArrayList a1 = new ArrayList();
            ADSEnt = new HSS_ADMINSTAFF();
            a1.Add(ADSEnt);

            gridAdminStaff.DataSource = a1;
            gridAdminStaff.DataBind();
        }
    }
   
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        //btnPopup_ModalPopupExtender.Show();
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {
       
    }
    protected void gridAdminStaff_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("View"))
        {
            GridViewRow gr = ((ImageButton)e.CommandSource).Parent.Parent as GridViewRow;

            Label lblPost = gr.FindControl("lblPost") as Label;
            Label lblName = gr.FindControl("lblName") as Label;
            Label lblStatus = gr.FindControl("lblStatus") as Label;
            Label lblPKID = gr.FindControl("lblPKID") as Label;

            lblPKIDU.Text = lblPKID.Text;
            txtPost.Text = lblPost.Text;
            txtName.Text = lblName.Text;
            ddlStatus.SelectedValue = lblStatus.Text;

            btnUpdate.Visible = true;
            
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        ADSEnt = new HSS_ADMINSTAFF();
       
        string filename = "";
        try
        {
            filename = lblPKIDU.Text + fuSignature.FileName.Substring(fuSignature.FileName.IndexOf('.'));
            if (fuSignature.HasFile)
            {
                fuSignature.SaveAs(Server.MapPath("~/images/signature" + filename));
                ADSEnt.SIGNATURE = lblPKIDU.Text;
            }
        }
        catch
        {
        }

        if (lblPKIDU.Text == "")
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Update not Successful')", true);
        }

        ADSEnt.PKID = lblPKIDU.Text;
        ADSEnt.POST = txtPost.Text;
        ADSEnt.NAME = txtName.Text;
        ADSEnt.STATUS = ddlStatus.SelectedValue;
        ADSSer.Update(ADSEnt);
        LoadData();
        ClearData();
    }
    protected void gridAdminStaff_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStatus = e.Row.FindControl("lblStatus") as Label;

            Label lblStatusName = e.Row.FindControl("lblStatusName") as Label;

            if (lblStatus.Text == "1")
            {
                lblStatusName.Text = "Active";
            }
            else
            {
                lblStatusName.Text = "Inactive";
            }
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        ClearData();
    }

    protected void ClearData()
    {
        txtPost.Text = "";
        txtName.Text = "";
        lblPKIDU.Text = "";
        ddlStatus.SelectedValue = "1";
    }

}