using Entity.Components;
using Service.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class library_masterdata_book : System.Web.UI.Page
{

    book BKEnt = new book();
    bookService BKSer = new bookService();

    booktype BTEnt = new booktype();
    booktypeService BTSer = new booktypeService();

    HelperFunction mf = new HelperFunction();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadBookType();


        }
    }

    protected void LoadBookType()
    {
        BTEnt = new booktype();
        ddlBookType.DataSource = BTSer.GetAll(BTEnt);
        ddlBookType.DataTextField = "Booktype";
        ddlBookType.DataValueField = "Booktypeid";
        ddlBookType.DataBind();
        ddlBookType.Items.Insert(0, "Select");
    }

    private void LoadData()
    {
        BKEnt = new book();
        BKEnt.Booktypeid = ddlBookType.SelectedValue;
        gridBook.DataSource = BKSer.GetAll(BKEnt);
        //grdGroups.Sort = "GROUPID";
        gridBook.DataBind();

        if (gridBook.Rows.Count == 0)
        {
            ArrayList a1 = new ArrayList();
            BKEnt = new book();
            a1.Add(BKEnt);

            gridBook.DataSource = a1;
            gridBook.DataBind();
        }
    }




    protected void gridBook_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        GridViewRow row = gridBook.HeaderRow;

        TextBox txtBookCode = (TextBox)row.FindControl("txtBookCodeH");
        TextBox txtBookName = (TextBox)row.FindControl("txtBookNameH");
        TextBox txtPublishedBy = (TextBox)row.FindControl("txtPublishedByH");
        TextBox txtPublsihedDate = (TextBox)row.FindControl("txtPublishedDateH");
        TextBox txtAuthorName = (TextBox)row.FindControl("txtAuthorNameH");
        DropDownList ddlBookCategoryH = (DropDownList)row.FindControl("ddlBookCategoryH");
        DropDownList ddlIssuableH = (DropDownList)row.FindControl("ddlIssuableH");
        DropDownList ddlIssueTypeH = (DropDownList)row.FindControl("ddlIssueTypeH");

        BKEnt = new book();
        BKEnt.Bookcode = txtBookCode.Text;
        BKEnt.Bookname = txtBookName.Text;
        BKEnt.Publishedby = txtPublishedBy.Text;
        BKEnt.Publisheddate = txtPublsihedDate.Text;
        BKEnt.Authorname = txtAuthorName.Text;
        BKEnt.Booktypeid = ddlBookType.SelectedValue;
        BKEnt.Bookcategory = ddlBookCategoryH.SelectedValue;
        BKEnt.Issuable = ddlIssuableH.SelectedValue;
        if (ddlIssuableH.SelectedValue == "1")
        {
            BKEnt.Issuetype = ddlIssueTypeH.SelectedValue;
        }
        BKSer.Insert(BKEnt);
        LoadData();

    }


    protected void gridBook_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridBook.EditIndex = -1;
        LoadData();
    }
    protected void gridBook_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridBook.EditIndex = e.NewEditIndex;
        LoadData();
    }
    protected void gridBook_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gridBook.Rows[e.RowIndex];

        Label lblBookId = (Label)row.FindControl("lblBookIdU");
        TextBox txtBookCode = (TextBox)row.FindControl("txtBookCodeE");
        TextBox txtBookName = (TextBox)row.FindControl("txtBookNameE");
        TextBox txtPublishedBy = (TextBox)row.FindControl("txtPublishedByE");
        TextBox txtPublsihedDate = (TextBox)row.FindControl("txtPublishedDateE");
        TextBox txtAuthorName = (TextBox)row.FindControl("txtAuthorNameE");

        DropDownList ddlBookCategoryE = (DropDownList)row.FindControl("ddlBookCategoryE");
        DropDownList ddlIssuableE = (DropDownList)row.FindControl("ddlIssuableE");
        DropDownList ddlIssueTypeE = (DropDownList)row.FindControl("ddlIssueTypeE");

        BKEnt = new book();

        BKEnt.Bookid = lblBookId.Text;
        BKEnt.Bookcode = txtBookCode.Text;
        BKEnt.Bookname = txtBookName.Text;
        BKEnt.Publishedby = txtPublishedBy.Text;
        BKEnt.Publisheddate = txtPublsihedDate.Text;
        BKEnt.Authorname = txtAuthorName.Text;

        BKEnt.Booktypeid = ddlBookType.SelectedValue;
        BKEnt.Bookcategory = ddlBookCategoryE.SelectedValue;
        BKEnt.Issuable = ddlIssuableE.SelectedValue;
        if (ddlIssuableE.SelectedValue == "1")
        {
            BKEnt.Issuetype = ddlIssueTypeE.SelectedValue;
        }
        else
        {
            BKEnt.Issuetype = "";
        }

        BKSer.Update(BKEnt);
        gridBook.EditIndex = -1;
        LoadData();
    }
    protected void gridBook_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridBook.PageIndex = e.NewPageIndex;
        LoadData();
    }
    protected void ddlBookType_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
    }
    protected void gridBook_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            string maxbookcode = "";
            string[] bookcode;
            string bcode;
            TextBox txtBookCodeH = (TextBox)e.Row.FindControl("txtBookCodeH");

            txtBookCodeH.Text = Convert.ToDouble(mf.getMaxBookCode(ddlBookType.SelectedValue)).ToString("000");

            //  maxbookcode = mf.GetMaxBookCode(ddlBookType.SelectedValue).ToString();
            //  bookcode = maxbookcode.Split('-');
            //   bcode = (Convert.ToDouble(bookcode[1]) + 1).ToString("0000");
            //txtBookCodeH.Text = bookcode[0] + "-" + bcode.ToString();
        }

        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            Label lblBookCat = e.Row.FindControl("lblBookCat") as Label;
            Label lblBookCategory = e.Row.FindControl("lblBookCategory") as Label;
            Label lblIssuable = e.Row.FindControl("lblIssuable") as Label;
            Label lblIssueType = e.Row.FindControl("lblIssueType") as Label;

            if (lblBookCat.Text == "C")
            {
                lblBookCategory.Text = "Course Book";
            }
            if (lblBookCat.Text == "R")
            {
                lblBookCategory.Text = "Reference Book";
            }
            if (lblIssuable.Text == "1")
            {
                lblIssuable.Text = "Yes";
            }
            if (lblIssuable.Text == "0")
            {
                lblIssuable.Text = "No";
            }
        }

        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) != 0)
        {
            DropDownList ddlBookCategoryE = e.Row.FindControl("ddlBookCategoryE") as DropDownList;
            Label lblBookCategoryE = e.Row.FindControl("lblBookCategoryE") as Label;
            DropDownList ddlIssuableE = e.Row.FindControl("ddlIssuableE") as DropDownList;
            DropDownList ddlIssueTypeE = e.Row.FindControl("ddlIssueTypeE") as DropDownList;
            Label lblIssuableE = e.Row.FindControl("lblIssuableE") as Label;
            Label lblIssueTypeE = e.Row.FindControl("lblIssueTypeE") as Label;

            ddlBookCategoryE.SelectedValue = lblBookCategoryE.Text;
            ddlIssuableE.SelectedValue = lblIssuableE.Text;
            ddlIssueTypeE.SelectedValue = lblIssueTypeE.Text;
        }
    }

    protected void gridBook_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

        GridViewRow row = gridBook.Rows[e.RowIndex];

        Label lblBookId = (Label)row.FindControl("lblBookIdU");
        TextBox txtBookCode = (TextBox)row.FindControl("txtBookCodeE");
        TextBox txtBookName = (TextBox)row.FindControl("txtBookNameE");
        TextBox txtPublishedBy = (TextBox)row.FindControl("txtPublishedByE");
        TextBox txtPublsihedDate = (TextBox)row.FindControl("txtPublishedDateE");
        TextBox txtAuthorName = (TextBox)row.FindControl("txtAuthorNameE");

        DropDownList ddlBookCategoryE = (DropDownList)row.FindControl("ddlBookCategoryE");

        BKEnt = new book();

        BKEnt.Bookid = lblBookId.Text;
        BKEnt.Bookcode = txtBookCode.Text;
        BKEnt.Bookname = txtBookName.Text;
        BKEnt.Publishedby = txtPublishedBy.Text;
        BKEnt.Publisheddate = txtPublsihedDate.Text;
        BKEnt.Authorname = txtAuthorName.Text;

        BKEnt.Booktypeid = ddlBookType.SelectedValue;
        BKEnt.Bookcategory = ddlBookCategoryE.SelectedValue;
        BKSer.Delete(BKEnt);
        gridBook.EditIndex = -1;
        LoadData();

    }

    protected void ddlIssuableH_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gr = ((DropDownList)sender).Parent.Parent as GridViewRow;
        DropDownList ddlIssuableH = gr.FindControl("ddlIssuableH") as DropDownList;
        DropDownList ddlIssueTypeH = gr.FindControl("ddlIssueTypeH") as DropDownList;

        if (ddlIssuableH.SelectedValue == "1")
        {
            ddlIssueTypeH.Visible = true;
        }
        else
        {
            ddlIssueTypeH.Visible = false;
        }

    }

    protected void ddlIssuableE_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gr = ((DropDownList)sender).Parent.Parent as GridViewRow;
        DropDownList ddlIssuableE = gr.FindControl("ddlIssuableE") as DropDownList;
        DropDownList ddlIssueTypeE = gr.FindControl("ddlIssueTypeE") as DropDownList;

        if (ddlIssuableE.SelectedValue == "1")
        {
            ddlIssueTypeE.Visible = true;
        }
        else
        {
            ddlIssueTypeE.Visible = false;
        }
    }
}