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


public partial class finance_MainParticular : System.Web.UI.Page
{
    Particulars_Main PMEnt = new Particulars_Main();
    Particulars_MainService PMSer = new Particulars_MainService();

    HelperFunction hf = new HelperFunction();

    EntityList theList = new EntityList();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
    }

    private void LoadData()
    {
        PMEnt = new Particulars_Main();
        gridMainParticular.DataSource = PMSer.GetAll(PMEnt);
        //grdGroups.Sort = "GROUPID";
        gridMainParticular.DataBind();

        if (gridMainParticular.Rows.Count == 0)
        {
            ArrayList a1 = new ArrayList();
            PMEnt = new Particulars_Main();
            a1.Add(PMEnt);

            gridMainParticular.DataSource = a1;
            gridMainParticular.DataBind();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        GridViewRow row = gridMainParticular.HeaderRow;

        TextBox txtParticularName = (TextBox)row.FindControl("txtParticularName");

        CheckBox chkOneTime = (CheckBox)row.FindControl("chkOneTime");

        PMEnt = new Particulars_Main();
        PMEnt.PARTICULAR_NAME = txtParticularName.Text;

        if (chkOneTime.Checked == true)
        {
            PMEnt.ONETIME = "1";
        }
        else if (chkOneTime.Checked == false)
        {
            PMEnt.ONETIME = "0";
        }


        PMSer.Insert(PMEnt);
        LoadData();


    }
    protected void gridMainParticular_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblOneTime = e.Row.FindControl("lblOneTime") as Label;

            CheckBox chkOneTimeI = e.Row.FindControl("chkOneTimeI") as CheckBox;

            if (lblOneTime.Text == "1")
            {
                chkOneTimeI.Checked = true;
            }
            if (lblOneTime.Text == "0")
            {
                chkOneTimeI.Checked = false;
            }
        }


        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            CheckBox chkOneTimeI = e.Row.FindControl("chkOneTimeI") as CheckBox;

            chkOneTimeI.Enabled = false;

        }

        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) != 0)
        {
            CheckBox chkOneTimeI = e.Row.FindControl("chkOneTimeI") as CheckBox;


            chkOneTimeI.Enabled = true;
        }


    }
    protected void gridMainParticular_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridMainParticular.EditIndex = -1;
        LoadData();
    }
    protected void gridMainParticular_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridMainParticular.EditIndex = e.NewEditIndex;
        LoadData();
    }

    protected void gridMainParticular_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gridMainParticular.Rows[e.RowIndex];

        Label lblMainIDE = row.FindControl("lblMainIDE") as Label;
        TextBox txtParticularNameE = row.FindControl("txtParticularNameE") as TextBox;
        CheckBox chkOneTimeI = row.FindControl("chkOneTimeI") as CheckBox;

        PMEnt = new Particulars_Main();
        PMEnt.MAIN_ID = lblMainIDE.Text;
        PMEnt.PARTICULAR_NAME = txtParticularNameE.Text;

        chkOneTimeI.Enabled = true;

        if (chkOneTimeI.Checked == true)
        {
            PMEnt.ONETIME = "1";
        }
        else if (chkOneTimeI.Checked == false)
        {
            PMEnt.ONETIME = "0";
        }

        PMSer.Update(PMEnt);
        gridMainParticular.EditIndex = -1;
        LoadData();

    }
}