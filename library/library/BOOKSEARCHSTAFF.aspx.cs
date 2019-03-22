using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Entity.Framework;
using Service.Components;

public partial class library_library_BOOKSEARCHSTAFF : System.Web.UI.Page
{
    book BEnt = new book();
    bookService BSer = new bookService();

    bookdetails BDEnt = new bookdetails();
    bookdetailsService BDSer = new bookdetailsService();

    BOOKSHELFCOMPART BSCEnt = new BOOKSHELFCOMPART();
    BOOKSHELFCOMPARTService BSCSer = new BOOKSHELFCOMPARTService();

    BOOKSHELF BSEnt = new BOOKSHELF();
    BOOKSHELFService BSSer = new BOOKSHELFService();

    bookissue BIEnt = new bookissue();
    bookissueService BISer = new bookissueService();

    HSS_STUDENT STDEnt = new HSS_STUDENT();
    HSS_STUDENTService STDSer = new HSS_STUDENTService();

    HSS_CURRENT_STUDENT CSEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSer = new HSS_CURRENT_STUDENTService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            txtBook.Focus();
        }
    }

    protected void txtBook_TextChanged(object sender, EventArgs e)
    {

    }

    protected void gridBookDetail_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        EntityList theList = new EntityList();
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblNSBN = e.Row.FindControl("lblNSBN") as Label;
            Label lblIssuable = e.Row.FindControl("lblIssuable") as Label;
            Label lblStatus = e.Row.FindControl("lblStatus") as Label;
            Label lblCompartID = (Label)e.Row.FindControl("lblCompartID");
            Label lblCompartNo = (Label)e.Row.FindControl("lblCompartNo");
            Label lblShelf = e.Row.FindControl("lblShelf") as Label;
            Label lblIssuedTo = e.Row.FindControl("lblIssuedTo") as Label;
            Label lblIssuedToName = e.Row.FindControl("lblIssuedToName") as Label;
            Label lblSemSec = e.Row.FindControl("lblSemSec") as Label;
            Label lblIssuedDate = e.Row.FindControl("lblIssuedDate") as Label;

            if (lblStatus.Text == "0")
            {
                lblStatus.Text = "Unavailable";
            }
            else
            {
                lblStatus.Text = "Available";
            }

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

            }

            if (lblCompartID.Text != "")
            {
                BSCEnt = new BOOKSHELFCOMPART();
                BSCEnt.PK_ID = lblCompartID.Text;
                BSCEnt = (BOOKSHELFCOMPART)BSCSer.GetSingle(BSCEnt);
                if (BSCEnt != null)
                {
                    lblCompartNo.Text = BSCEnt.COMPARTNO;
                    BSEnt = new BOOKSHELF();
                    BSEnt.PK_ID = BSCEnt.SHELFID;
                    BSEnt = (BOOKSHELF)BSSer.GetSingle(BSEnt);
                    if (BSEnt != null)
                    {
                        lblShelf.Text = BSEnt.SHELFNO;
                    }
                }
            }
            else { }



            if (lblStatus.Text == "Unavailable")
            {
                BIEnt = new bookissue();
                BIEnt.NSBN = lblNSBN.Text;
                BIEnt = (bookissue)BISer.GetSingle(BIEnt);
                if (BIEnt != null)
                {
                    lblIssuedDate.Text = BIEnt.Issueday + "/" + BIEnt.Issuemonth + "/" + BIEnt.Issueyear;

                    STDEnt = new HSS_STUDENT();
                    STDEnt.STUDENT_ID = BIEnt.Issueto;
                    STDEnt = (HSS_STUDENT)STDSer.GetSingle(STDEnt);
                    if (STDEnt != null)
                    {
                        lblIssuedToName.Text = STDEnt.NAME_ENGLISH;
                    }
                    CSEnt = new HSS_CURRENT_STUDENT();
                    CSEnt.STUDENT_ID = BIEnt.Issueto;
                    CSEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSEnt);
                    if (CSEnt != null)
                    {
                        lblSemSec.Text = "[" + CSEnt.SEMESTER + "-" + CSEnt.SECTION + "]";
                    }

                }
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BEnt = new book();
        BEnt.Bookname = txtBook.Text;
        BEnt = (book)BSer.GetSingle(BEnt);
        if (BEnt != null)
        {
            if (ddlStatus.SelectedValue == "Both")
            {
                BDEnt = new bookdetails();
                BDEnt.Bookid = BEnt.Bookid;
                gridBookDetail.DataSource = BDSer.GetAll(BDEnt);
                gridBookDetail.DataBind();
            }
            else
            {

                BDEnt = new bookdetails();
                BDEnt.Bookid = BEnt.Bookid;
                BDEnt.Status = ddlStatus.Text;
                if (ddlStatus.SelectedValue == "0")
                {
                    gridBookDetail.DataSource = BDSer.GetAll(BDEnt);
                    gridBookDetail.DataBind();
                }
                else
                {
                    BDEnt = (bookdetails)BDSer.GetSingle(BDEnt);
                    if (BDEnt != null)
                    {
                        gridBookDetail.DataSource = BDSer.GetAll(BDEnt);
                        gridBookDetail.DataBind();
                    }

                }

            }
        }
    }
}
