using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Entity.Components;
using Service.Components;

public partial class uc_test_ActionRewardType : System.Web.UI.UserControl
{
    ACTION_REWARD AREnt = new ACTION_REWARD();
    ACTION_REWARDService ARSer = new ACTION_REWARDService();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();

        }
    }

    private void LoadData()
    {
        AREnt = new ACTION_REWARD();
        gridActionReward.DataSource = ARSer.GetAll(AREnt);
        //grdGroups.Sort = "GROUPID";
        gridActionReward.DataBind();

        if (gridActionReward.Rows.Count == 0)
        {
            ArrayList a1 = new ArrayList();
            AREnt = new ACTION_REWARD();
            a1.Add(AREnt);

            gridActionReward.DataSource = a1;
            gridActionReward.DataBind();
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        GridViewRow row = gridActionReward.HeaderRow;

        DropDownList ddlARH = (DropDownList)row.FindControl("ddlARH");

        TextBox txtARTypeH = (TextBox)row.FindControl("txtARTypeH");

        AREnt = new ACTION_REWARD();
        AREnt.ACTION_REWARDS = ddlARH.SelectedValue;
        AREnt.ACTIVITY = txtARTypeH.Text;


          
            ARSer.Insert(AREnt);
            LoadData();
            HelperFunction.MsgBox(this, this.GetType(), "Record added successfully.");
        



    }
    protected void gridActionReward_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridActionReward.EditIndex = e.NewEditIndex;
        LoadData();
    }

    protected void gridActionReward_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridActionReward.EditIndex = -1;
        LoadData();
    }
    protected void gridActionReward_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gridActionReward.Rows[e.RowIndex];
        Label lblPkidE = (Label)row.FindControl("lblPkidE");
        DropDownList ddlARE = (DropDownList)row.FindControl("ddlARE");

        TextBox txtARTypeE = (TextBox)row.FindControl("txtARTypeE");



        AREnt = new ACTION_REWARD();

        AREnt.PKID = lblPkidE.Text;
        AREnt.ACTION_REWARDS = ddlARE.SelectedValue;
        AREnt.ACTIVITY = txtARTypeE.Text;


       ARSer.Update(AREnt);

       gridActionReward.EditIndex = -1;
        LoadData();
    }
    protected void gridActionReward_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) != 0)
        {
            DropDownList ddlARE = e.Row.FindControl("ddlARE") as DropDownList;
            TextBox txtARTypeE = e.Row.FindControl("txtARTypeE") as TextBox;
            Label lblPkidE = e.Row.FindControl("lblPkidE") as Label;

            AREnt = new ACTION_REWARD();

            AREnt.PKID = lblPkidE.Text;
            AREnt = (ACTION_REWARD)ARSer.GetSingle(AREnt);
            if (AREnt != null)
            {
                ddlARE.SelectedValue = AREnt.ACTION_REWARDS;
            
            }
        }
      
    }
}