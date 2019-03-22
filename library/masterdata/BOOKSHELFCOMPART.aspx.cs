using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;
using System.Collections;

public partial class library_masterdata_BOOKSHELFCOMPART : System.Web.UI.Page
{

    BOOKSHELF BSEnt = new BOOKSHELF();
    BOOKSHELFService BSSer = new BOOKSHELFService();

    BOOKSHELFCOMPART BSCEnt = new BOOKSHELFCOMPART();
    BOOKSHELFCOMPARTService BSCSer = new BOOKSHELFCOMPARTService();

    Boolean IsPageRefresh = false;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["postids"] = System.Guid.NewGuid().ToString();
            Session["postid"] = ViewState["postids"].ToString();
            try
            {

                loadGrid();
            }
            catch { }
        }
        else
        {
            if (ViewState["postids"].ToString() != Session["postid"].ToString())
            {
                IsPageRefresh = true;
            }
            Session["postid"] = System.Guid.NewGuid().ToString();
            ViewState["postids"] = Session["postid"].ToString();

        }
    }

    protected void loadGrid()
    {

        BSCEnt = new BOOKSHELFCOMPART();
        gridBookShelfCompart.DataSource = BSCSer.GetAll(BSCEnt);
        gridBookShelfCompart.DataBind();
        if (gridBookShelfCompart.Rows.Count == 0)
        {
            ArrayList a1 = new ArrayList();
            a1.Add(BSCEnt);
            gridBookShelfCompart.DataSource = a1;
            gridBookShelfCompart.DataBind();
        }

    }

    protected void gridBookShelfCompart_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            DropDownList ddlShelfH = e.Row.FindControl("ddlShelfH") as DropDownList;

            BSEnt = new BOOKSHELF();
            BSEnt.STATUS = "1";
            ddlShelfH.DataSource = BSSer.GetAll(BSEnt);
            ddlShelfH.DataTextField = "SHELFNO";
            ddlShelfH.DataValueField = "PK_ID";
            ddlShelfH.DataBind();
            ddlShelfH.Items.Insert(0, "select");
            if (lblSelectedShelf.Text != "0")
            {
                ddlShelfH.SelectedValue = lblSelectedShelf.Text;
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            Label lblShelfid = e.Row.FindControl("lblShelfid") as Label;
            BSEnt = new BOOKSHELF();
            BSEnt.PK_ID = lblShelfid.Text;
            BSEnt = (BOOKSHELF)BSSer.GetSingle(BSEnt);
            if (BSEnt != null)
            {
                lblShelfid.Text = BSEnt.SHELFNO;
            }
        }
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) != 0)
        {
            Label lblCompartNoE = e.Row.FindControl("lblCompartNoE") as Label;
            TextBox txtCompartnoE = e.Row.FindControl("txtCompartnoE") as TextBox;
            DropDownList ddlShelfE = e.Row.FindControl("ddlShelfE") as DropDownList;
            txtCompartnoE.Text = lblCompartNoE.Text;
            BSEnt = new BOOKSHELF();
            ddlShelfE.DataSource = BSSer.GetAll(BSEnt);
            ddlShelfE.DataTextField = "SHELFNO";
            ddlShelfE.DataValueField = "PK_ID";
            ddlShelfE.DataBind();

        }

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        GridViewRow row = gridBookShelfCompart.HeaderRow;
        DropDownList ddlShelfH = (DropDownList)row.FindControl("ddlShelfH");
        TextBox txtCompartNoH = (TextBox)row.FindControl("txtCompartNoH");

        BSCEnt = new BOOKSHELFCOMPART();
        BSCEnt.SHELFID = ddlShelfH.SelectedValue;
        BSCEnt.COMPARTNO = txtCompartNoH.Text;
        BSCSer.Insert(BSCEnt);
        loadGrid();
    }

    protected void gridBookShelfCompart_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridBookShelfCompart.EditIndex = -1;
        loadGrid();
    }

    protected void gridBookShelfCompart_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridBookShelfCompart.EditIndex = e.NewEditIndex;
        loadGrid();
    }

    protected void gridBookShelfCompart_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gridBookShelfCompart.Rows[e.RowIndex];
        DropDownList ddlShelfE = row.FindControl("ddlShelfE") as DropDownList;
        TextBox txtCompartnoE = row.FindControl("txtCompartnoE") as TextBox;
        Label lblPK = row.FindControl("lblPK") as Label;

        BSCEnt = new BOOKSHELFCOMPART();
        BSCEnt.PK_ID = lblPK.Text;
        BSCEnt = (BOOKSHELFCOMPART)BSCSer.GetSingle(BSCEnt);
        if (BSCEnt != null)
        {
            BSCEnt.SHELFID = ddlShelfE.SelectedValue;
            BSCEnt.COMPARTNO = txtCompartnoE.Text;
            BSCSer.Update(BSCEnt);
            gridBookShelfCompart.EditIndex = -1;
            loadGrid();
        }
    }

    protected void ddlShelfH_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (!IsPageRefresh)
        {
            GridViewRow gr = ((DropDownList)sender).Parent.Parent as GridViewRow;
            DropDownList ddlShelfH = gr.FindControl("ddlShelfH") as DropDownList;

            if (ddlShelfH.SelectedIndex == 0)
            {
                lblSelectedShelf.Text = "0";
                BSCEnt = new BOOKSHELFCOMPART();
                gridBookShelfCompart.DataSource = BSCSer.GetAll(BSCEnt);
                gridBookShelfCompart.DataBind();
                if (gridBookShelfCompart.Rows.Count == 0)
                {
                    ArrayList a1 = new ArrayList();
                    a1.Add(BSCEnt);
                    gridBookShelfCompart.DataSource = a1;
                    gridBookShelfCompart.DataBind();
                }
            }
            else
            {
                lblSelectedShelf.Text = ddlShelfH.SelectedValue;
                BSCEnt = new BOOKSHELFCOMPART();
                BSCEnt.SHELFID = ddlShelfH.SelectedValue;
                gridBookShelfCompart.DataSource = BSCSer.GetAll(BSCEnt);
                gridBookShelfCompart.DataBind();
                if (gridBookShelfCompart.Rows.Count == 0)
                {
                    ArrayList a1 = new ArrayList();
                    a1.Add(BSCEnt);
                    gridBookShelfCompart.DataSource = a1;
                    gridBookShelfCompart.DataBind();
                }
            }
        }
    }
}