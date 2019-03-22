using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;

public partial class library_masterdata_BOOKSHELF : System.Web.UI.Page
{
    BOOKSHELF BSEnt = new BOOKSHELF();
    BOOKSHELFService BSSer = new BOOKSHELFService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadShelfGrid();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (txtShelf.Text == "")
        {
            HelperFunction.MsgBox(this, this.GetType(), "Please enter Shelf No.");
            txtShelf.Focus();
        }
        else
        {
            BSEnt = new BOOKSHELF();
            BSEnt.SHELFNO = txtShelf.Text;
            BSEnt.STATUS = "1";
            BSSer.Insert(BSEnt);
            loadShelfGrid();
        }

    }

    protected void loadShelfGrid()
    {
        BSEnt = new BOOKSHELF();
        BSEnt.STATUS = "1";
        gridShelf.DataSource = BSSer.GetAll(BSEnt);
        gridShelf.DataBind();
        if (gridShelf.Rows.Count == 0)
        {
            gridShelf.Visible = false;
        }
        else
        {
            gridShelf.Visible = true;
        }
    }

    protected void gridShelf_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridShelf.EditIndex = e.NewEditIndex;
        loadShelfGrid();

    }

    protected void gridShelf_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) != 0)
        {
            Label lblShelfE = e.Row.FindControl("lblShelfE") as Label;
            TextBox txtShelfE = e.Row.FindControl("txtShelfE") as TextBox;

            txtShelfE.Text = lblShelfE.Text;
            lblShelfE.Visible = false;
        }
    }



    protected void gridShelf_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridShelf.EditIndex = -1;
        loadShelfGrid();
    }

    protected void gridShelf_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gridShelf.Rows[e.RowIndex];
        TextBox txtShelfE = row.FindControl("txtShelfE") as TextBox;
        Label lblShelfE = row.FindControl("lblShelfE") as Label;
        Label lblPK = row.FindControl("lblPK") as Label;

        BSEnt = new BOOKSHELF();
        BSEnt.PK_ID = lblPK.Text;
        BSEnt.SHELFNO = lblShelfE.Text;
        BSEnt = (BOOKSHELF)BSSer.GetSingle(BSEnt);
        if (BSEnt != null)
        {
            BSEnt.SHELFNO = txtShelfE.Text;
            BSSer.Update(BSEnt);
            gridShelf.EditIndex = -1;
            loadShelfGrid();
        }
    }
}