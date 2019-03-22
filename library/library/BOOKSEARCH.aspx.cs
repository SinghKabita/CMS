using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Entity.Framework;
using Service.Components;

public partial class library_library_BOOKSEARCH : System.Web.UI.Page
{

    book BEnt = new book();
    bookService BSer = new bookService();

    bookdetails BDEnt = new bookdetails();
    bookdetailsService BDSer = new bookdetailsService();

    BOOKSHELFCOMPART BSCEnt = new BOOKSHELFCOMPART();
    BOOKSHELFCOMPARTService BSCSer = new BOOKSHELFCOMPARTService();

    BOOKSHELF BSEnt = new BOOKSHELF();
    BOOKSHELFService BSSer = new BOOKSHELFService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtBook.Focus();
        }
    }

    protected void txtBook_TextChanged(object sender, EventArgs e)
    {
        BEnt = new book();
        BEnt.Bookname = txtBook.Text;
        BEnt = (book)BSer.GetSingle(BEnt);
        if (BEnt != null)
        {
            BDEnt = new bookdetails();
            BDEnt.Bookid = BEnt.Bookid;
            BDEnt.Status = "1";
            BDEnt = (bookdetails)BDSer.GetSingle(BDEnt);
            if (BDEnt != null)
            {
                gridBookDetail.DataSource = BDSer.GetAll(BDEnt);
                gridBookDetail.DataBind();
            }
        }
    }

    protected void gridBookDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        EntityList theList = new EntityList();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblIssuable = e.Row.FindControl("lblIssuable") as Label;
            Label lblStatus = e.Row.FindControl("lblStatus") as Label;
            Label lblCompart = e.Row.FindControl("lblCompart") as Label;
            Label lblShelf = e.Row.FindControl("lblShelf") as Label;

            BEnt = new book();
            BEnt.Bookname = txtBook.Text;
            BEnt = (book)BSer.GetSingle(BEnt);
            if (BEnt != null)
            {
                if (BEnt.Issuable == "0")
                {
                    lblIssuable.Text = "Non-issuable";
                }
                else
                {
                    lblIssuable.Text = BEnt.Issuetype + " Issuable";
                }
                BDEnt = new bookdetails();
                BDEnt.Bookid = BEnt.Bookid;
                theList = BDSer.GetAll(BDEnt);
                if (theList.Count > 0)
                {
                    foreach (bookdetails bd in theList)
                    {
                        if (bd.Status == "1")
                        {
                            lblStatus.Text = "Available";
                        }
                        else
                        {
                            lblStatus.Text = "Unavailable";
                        }

                        BSCEnt = new BOOKSHELFCOMPART();
                        BSCEnt.PK_ID = bd.COMPARTID;
                        BSCEnt = (BOOKSHELFCOMPART)BSCSer.GetSingle(BSCEnt);
                        if (BSCEnt != null && BDEnt.COMPARTID != "")
                        {
                            lblCompart.Text = BSCEnt.COMPARTNO;
                            BSEnt = new BOOKSHELF();
                            BSEnt.PK_ID = BSCEnt.SHELFID;
                            BSEnt = (BOOKSHELF)BSSer.GetSingle(BSEnt);
                            if (BSEnt != null)
                            {
                                lblShelf.Text = BSEnt.SHELFNO;
                            }
                        }
                    }

                }
                else
                {
                    HelperFunction.MsgBox(this, this.GetType(), "Books are not Available");
                }
            }
        }
    }
}