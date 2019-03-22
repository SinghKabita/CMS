using Entity.Components;
using Service.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DataHelper.Framework;

public partial class library_masterdata_bookdetails : System.Web.UI.Page
{
    bookdetails BDEnt = new bookdetails();
    bookdetailsService BDSer = new bookdetailsService();

    booktype BTEnt = new booktype();
    booktypeService BTSer = new booktypeService();

    BOOKSHELF BSEnt = new BOOKSHELF();
    BOOKSHELFService BSSer = new BOOKSHELFService();

    BOOKSHELFCOMPART BSCEnt = new BOOKSHELFCOMPART();
    BOOKSHELFCOMPARTService BSCSer = new BOOKSHELFCOMPARTService();

    book BEnt = new book();
    bookService BSer = new bookService();

    HelperFunction hf = new HelperFunction();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            LoadBookType();
            loadShelf();
            loadShelfS();
            loadShelfM();
        }
    }

    private void LoadData()
    {
        if (ddlBookTypeA.SelectedValue != "Select" && ddlBookNameA.SelectedValue != "Select" && ddlShelfS.SelectedValue == "select")
        {
            BDEnt = new bookdetails();
            BDEnt.Bookid = ddlBookNameA.SelectedValue;

            gridBookDetails.DataSource = BDSer.GetAll(BDEnt);
            //grdGroups.Sort = "GROUPID";
            gridBookDetails.DataBind();

            if (gridBookDetails.Rows.Count == 0)
            {
                gridBookDetails.DataSource = null;
                gridBookDetails.DataBind();
            }
        }
        if (ddlBookTypeA.SelectedValue != "Select" && ddlBookNameA.SelectedValue != "Select" && ddlShelfS.SelectedValue != "select")
        {
            BDEnt = new bookdetails();
            BDEnt.Bookid = ddlBookNameA.SelectedValue;
            BDEnt.COMPARTID = ddlCompartS.SelectedValue;
            gridBookDetails.DataSource = BDSer.GetAll(BDEnt);
            gridBookDetails.DataBind();

            if (gridBookDetails.Rows.Count == 0)
            {
                gridBookDetails.DataSource = null;
                gridBookDetails.DataBind();
            }
        }
    }

    protected void gridBookDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        gridBookDetails.Columns[6].Visible = false;
        gridBookDetails.Columns[1].Visible = false;
        gridBookDetails.Columns[2].Visible = false;
        gridBookDetails.Columns[9].Visible = false;


        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            Label lblBookId = (Label)e.Row.FindControl("lblBookId");
            Label lblBookName = (Label)e.Row.FindControl("lblBookName");
            Label lblShelf = (Label)e.Row.FindControl("lblShelf");
            Label lblCompartID = (Label)e.Row.FindControl("lblCompartID");
            Label lblCompartNo = (Label)e.Row.FindControl("lblCompartNo");

            Label lblBookType = (Label)e.Row.FindControl("lblBookType");
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            Label lblStat = (Label)e.Row.FindControl("lblStat");
            Label lblIssuable = (Label)e.Row.FindControl("lblIssuable");


            BEnt = new book();
            BEnt.Bookid = lblBookId.Text;
            BEnt = (book)BSer.GetSingle(BEnt);
            if (BEnt != null)
            {

                lblBookName.Text = BEnt.Bookname;
                if (BEnt.Issuable == "0")
                {
                    lblIssuable.Text = "Non-issuable";
                }
                else
                {
                    lblIssuable.Text = BEnt.Issuetype + " Issuable";
                }


                BTEnt = new booktype();
                BTEnt.Booktypeid = BEnt.Booktypeid;
                BTEnt = (booktype)BTSer.GetSingle(BTEnt);
                if (BTEnt != null)
                {
                    lblBookType.Text = BTEnt.Booktype;
                }

            }

            if (lblCompartID.Text == "") { }
            else
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


            if (lblStat.Text == "1")
            {
                lblStatus.Text = "Available";
            }
            else if (lblStat.Text == "0")
            {
                lblStatus.Text = "Unavailable";
            }



        }


        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) != 0)
        {
            DropDownList ddlBookTypeE = (DropDownList)e.Row.FindControl("ddlBookTypeE");
            DropDownList ddlBookNameE = (DropDownList)e.Row.FindControl("ddlBookNameE");
            Label lblBookTypeIdU = (Label)e.Row.FindControl("lblBookTypeIdU");
            Label lblBookIdU = (Label)e.Row.FindControl("lblBookIdU");
            Label lblBookDetailId = (Label)e.Row.FindControl("lblBookDetailId");

            TextBox txtRemarksE = (TextBox)e.Row.FindControl("txtRemarksE");
            TextBox txtBookNumberE = (TextBox)e.Row.FindControl("txtBookNumberE");
            DropDownList ddlStatusE = (DropDownList)e.Row.FindControl("ddlStatusE");


            Label lblStatusE = (Label)e.Row.FindControl("lblStatusE");


            BEnt = new book();

            ddlBookNameE.DataSource = BSer.GetAll(BEnt);
            ddlBookNameE.DataTextField = "BOOKNAME";
            ddlBookNameE.DataValueField = "BOOKID";
            ddlBookNameE.DataBind();
            ddlBookNameE.Items.Insert(0, "Select");
            ddlBookNameE.SelectedValue = lblBookIdU.Text;



            BTEnt = new booktype();
            ddlBookTypeE.DataSource = BTSer.GetAll(BTEnt);
            ddlBookTypeE.DataTextField = "BOOKTYPE";
            ddlBookTypeE.DataValueField = "BOOKTYPEID";
            ddlBookTypeE.DataBind();
            ddlBookTypeE.Items.Insert(0, "Select");





            BDEnt = new bookdetails();
            BDEnt.Bookdetailid = lblBookDetailId.Text;
            BDEnt = (bookdetails)BDSer.GetSingle(BDEnt);
            if (BDEnt != null)
            {
                lblStatusE.Text = BDEnt.Status;
            }


            ddlStatusE.SelectedValue = lblStatusE.Text;


            BEnt = new book();
            BEnt.Bookid = lblBookIdU.Text;
            BEnt = (book)BSer.GetSingle(BEnt);
            if (BEnt != null)
            {
                ddlBookTypeE.SelectedValue = BEnt.Booktypeid;
            }

        }


    }


    protected void LoadBookType()
    {

        BTEnt = new booktype();
        ddlBookTypeA.DataSource = BTSer.GetAll(BTEnt);
        ddlBookTypeA.DataTextField = "Booktype";
        ddlBookTypeA.DataValueField = "Booktypeid";
        ddlBookTypeA.DataBind();
        ddlBookTypeA.Items.Insert(0, "Select");

        ddlBookType.DataSource = BTSer.GetAll(BTEnt);
        ddlBookType.DataTextField = "BOOKTYPE";
        ddlBookType.DataValueField = "BOOKTYPEID";
        ddlBookType.DataBind();
        ddlBookType.Items.Insert(0, "Select");

    }
    protected void gridBookDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridBookDetails.EditIndex = e.NewEditIndex;
        LoadData();
    }
    protected void gridBookDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridBookDetails.EditIndex = -1;
        LoadData();
    }


    protected void gridBookDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gridBookDetails.Rows[e.RowIndex];

        DropDownList ddlBookNameE = (DropDownList)row.FindControl("ddlBookNameE");

        TextBox txtBookNumberE = (TextBox)row.FindControl("txtBookNumberE");
        TextBox txtRemarksE = (TextBox)row.FindControl("txtRemarksE");
        Label lblBookDetailId = (Label)row.FindControl("lblBookDetailId");
        DropDownList ddlStatusE = (DropDownList)row.FindControl("ddlStatusE");


        BDEnt = new bookdetails();
        BDEnt.Bookdetailid = lblBookDetailId.Text;
        BDEnt = (bookdetails)BDSer.GetSingle(BDEnt);
        if (BDEnt != null)
        {
            BDEnt.Bookid = ddlBookNameE.SelectedValue;
            BDEnt.Booknumber = txtBookNumberE.Text;
            BDEnt.Remarks = txtRemarksE.Text;
            BDEnt.Status = ddlStatusE.SelectedValue;


            BDSer.Update(BDEnt);
        }
        gridBookDetails.EditIndex = -1;

        LoadData();


    }
    protected void gridBookDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridBookDetails.PageIndex = e.NewPageIndex;
        LoadData();
    }
    protected void btnAddBook_Click(object sender, EventArgs e)
    {

        divEdit.Visible = true;
        tblBookDetail.Visible = false;
        gridBookDetails.Visible = false;
    }



    protected void btnAdd_Click1(object sender, EventArgs e)
    {
        double bookNo = Convert.ToDouble(hf.getMaxBookNumber(ddlBookName.SelectedValue));
        DistributedTransaction DT = new DistributedTransaction();
        for (int i = 0; i < Convert.ToInt32(txtBookNumber.Text); i++)
        {
            BDEnt = new bookdetails();
            BDEnt.Bookid = ddlBookName.SelectedValue;
            BDEnt.Booknumber = bookNo.ToString("000");
            BDEnt.Status = "1";
            BDEnt.Remarks = txtRemarks.Text;
            BDEnt.COMPARTID = ddlCompartNo.SelectedValue;

            BEnt = new book();
            BEnt.Bookid = ddlBookName.SelectedValue;
            BEnt = (book)BSer.GetSingle(BEnt);
            if (BEnt != null)
            {
                BDEnt.NSBN = BEnt.Bookcategory + "-" + ddlBookType.SelectedValue + "-" + BEnt.Bookcode + "-" + bookNo.ToString("000");
            }

            BDSer.Insert(BDEnt, DT);
            bookNo++;
        }
        if (DT.HAPPY == true)
        {
            DT.Commit();
            HelperFunction.MsgBox(this, this.GetType(), "Book Details Added");
            LoadData();
        }
        else
        {
            DT.Abort();
            HelperFunction.MsgBox(this, this.GetType(), "Unable to Add");
        }
        DT.Dispose();


    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        // LoadSelectedData();
    }



    protected void ddlBookTypeA_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlBookTypeA.SelectedValue != "Select")
        {
            BEnt = new book();
            BEnt.Booktypeid = ddlBookTypeA.SelectedValue;
            ddlBookNameA.DataSource = BSer.GetAll(BEnt);
            ddlBookNameA.DataTextField = "BOOKNAME";
            ddlBookNameA.DataValueField = "BOOKID";
            ddlBookNameA.DataBind();
            ddlBookNameA.Items.Insert(0, "Select");
            LoadData();
        }

    }

    protected void ddlBookType_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlBookType.SelectedValue != "Select")
        {
            BEnt = new book();
            BEnt.Booktypeid = ddlBookType.SelectedValue;

            ddlBookName.DataSource = BSer.GetAll(BEnt);
            ddlBookName.DataTextField = "BOOKNAME";
            ddlBookName.DataValueField = "BOOKID";
            ddlBookName.DataBind();
            ddlBookName.Items.Insert(0, "Select");

            divEdit.Visible = true;
        }

    }

    protected void ddlBookTypeE_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridViewRow gr = ((DropDownList)sender).Parent.Parent as GridViewRow;

        DropDownList ddlBookTypeE = (DropDownList)gr.FindControl("ddlBookTypeE");
        DropDownList ddlBookNameE = (DropDownList)gr.FindControl("ddlBookNameE");



        if (ddlBookTypeE.SelectedValue != "Select")
        {
            BEnt = new book();
            BEnt.Booktypeid = ddlBookTypeE.SelectedValue;
            ddlBookNameE.DataSource = BSer.GetAll(BEnt);
            ddlBookNameE.DataTextField = "BOOKNAME";
            ddlBookNameE.DataValueField = "BOOKID";
            ddlBookNameE.DataBind();
            ddlBookNameE.Items.Insert(0, "Select");
        }

    }
    protected void gridBookDetails_RowEditing1(object sender, GridViewEditEventArgs e)
    {

    }
    protected void gridBookDetails_RowUpdating1(object sender, GridViewUpdateEventArgs e)
    {

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        divEdit.Visible = false;
        tblBookDetail.Visible = true;
    }

    protected void loadShelf()
    {
        BSEnt = new BOOKSHELF();
        BSEnt.STATUS = "1";
        ddlShelfNo.DataSource = BSSer.GetAll(BSEnt);
        ddlShelfNo.DataTextField = "SHELFNO";
        ddlShelfNo.DataValueField = "PK_ID";
        ddlShelfNo.DataBind();
        ddlShelfNo.Items.Insert(0, "select");
    }

    protected void loadShelfS()
    {
        BSEnt = new BOOKSHELF();
        BSEnt.STATUS = "1";
        ddlShelfS.DataSource = BSSer.GetAll(BSEnt);
        ddlShelfS.DataTextField = "SHELFNO";
        ddlShelfS.DataValueField = "PK_ID";
        ddlShelfS.DataBind();
        ddlShelfS.Items.Insert(0, "select");
    }

    protected void loadShelfM()
    {
        BSEnt = new BOOKSHELF();
        BSEnt.STATUS = "1";
        ddlShelfM.DataSource = BSSer.GetAll(BSEnt);
        ddlShelfM.DataTextField = "SHELFNO";
        ddlShelfM.DataValueField = "PK_ID";
        ddlShelfM.DataBind();
        ddlShelfM.Items.Insert(0, "select");
    }

    protected void loadCompart()
    {
        BSCEnt = new BOOKSHELFCOMPART();
        BSCEnt.SHELFID = ddlShelfNo.SelectedValue;
        ddlCompartNo.DataSource = BSCSer.GetAll(BSCEnt);
        ddlCompartNo.DataTextField = "COMPARTNO";
        ddlCompartNo.DataValueField = "PK_ID";
        ddlCompartNo.DataBind();
    }

    protected void loadCompartS()
    {
        BSCEnt = new BOOKSHELFCOMPART();
        BSCEnt.SHELFID = ddlShelfS.SelectedValue;
        ddlCompartS.DataSource = BSCSer.GetAll(BSCEnt);
        ddlCompartS.DataTextField = "COMPARTNO";
        ddlCompartS.DataValueField = "PK_ID";
        ddlCompartS.DataBind();
    }

    protected void loadCompartM()
    {
        BSCEnt = new BOOKSHELFCOMPART();
        BSCEnt.SHELFID = ddlShelfM.SelectedValue;
        ddlCompartM.DataSource = BSCSer.GetAll(BSCEnt);
        ddlCompartM.DataTextField = "COMPARTNO";
        ddlCompartM.DataValueField = "PK_ID";
        ddlCompartM.DataBind();
    }

    protected void ddlShelfNo_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCompart();
    }

    protected void ddlShelfS_SelectedIndexChanged(object sender, EventArgs e)
    {

        loadCompartS();
    }

    protected void ddlShelfM_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadCompartM();
    }


    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        DistributedTransaction DT = new DistributedTransaction();
        foreach (GridViewRow gr in gridBookDetails.Rows)
        {
            CheckBox chkCheck = gr.FindControl("chkCheck") as CheckBox;
            Label lblBDID = (Label)gr.FindControl("lblBDID");
            BDEnt = new bookdetails();
            BDEnt.Bookdetailid = lblBDID.Text;
            BDEnt = (bookdetails)BDSer.GetSingle(BDEnt);
            if (BDEnt != null)
            {
                if (chkCheck.Checked == true)
                {
                    BDEnt.COMPARTID = ddlCompartS.SelectedValue;
                    BDSer.Update(BDEnt, DT);
                }
            }
        }
        if (DT.HAPPY == true)
        {
            DT.Commit();
            HelperFunction.MsgBox(this, this.GetType(), "Shelf/ Compart Updated");
            LoadData();
        }
        else
        {
            DT.Abort();
            HelperFunction.MsgBox(this, this.GetType(), "Unable to Update Shelf/ Compart");
        }
        DT.Dispose();
    }

    protected void chkCheck_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow row = gridBookDetails.HeaderRow;
        CheckBox chkCheckH = row.FindControl("chkCheckH") as CheckBox;

        GridViewRow gr = ((CheckBox)sender).Parent.Parent as GridViewRow;
        CheckBox chkCheck = gr.FindControl("chkCheck") as CheckBox;

        if (chkCheck.Checked == true)
        {
            if (ddlShelfS.SelectedIndex == 0)
            {
                HelperFunction.MsgBox(this, this.GetType(), "Please Select Shelf");
                chkCheck.Checked = false;
            }
        }
        else
        {
            chkCheckH.Checked = false;
        }
    }

    protected void chkCheckH_CheckedChanged(object sender, EventArgs e)
    {
        GridViewRow row = gridBookDetails.HeaderRow;
        CheckBox chkCheckH = row.FindControl("chkCheckH") as CheckBox;

        if (chkCheckH.Checked == true)
        {
            foreach (GridViewRow gr in gridBookDetails.Rows)
            {
                CheckBox chkCheck = gr.FindControl("chkCheck") as CheckBox;
                chkCheck.Checked = true;
            }
        }
        else
        {
            foreach (GridViewRow gr in gridBookDetails.Rows)
            {
                CheckBox chkCheck = gr.FindControl("chkCheck") as CheckBox;
                chkCheck.Checked = false;
            }
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        gridBookDetails.Visible = true;
        LoadData();
    }




    protected void btnMove_Click(object sender, EventArgs e)
    {
        DistributedTransaction DT = new DistributedTransaction();
        foreach (GridViewRow gr in gridBookDetails.Rows)
        {
            CheckBox chkCheck = gr.FindControl("chkCheck") as CheckBox;
            Label lblBDID = gr.FindControl("lblBDID") as Label;

            if (chkCheck.Checked == true)
            {
                BDEnt = new bookdetails();
                BDEnt.Bookdetailid = lblBDID.Text;
                BDEnt = (bookdetails)BDSer.GetSingle(BDEnt);
                if (BDEnt != null)
                {
                    BDEnt.COMPARTID = ddlCompartM.SelectedValue;
                    BDSer.Update(BDEnt, DT);
                }
            }
        }

        if (DT.HAPPY == true)
        {
            ddlShelfM.SelectedIndex = 0;
            DT.Commit();
            HelperFunction.MsgBox(this, this.GetType(), "Book/s Moved Successfully");
            LoadData();
        }
        else
        {
            ddlShelfM.SelectedIndex = 0;
            DT.Abort();
            HelperFunction.MsgBox(this, this.GetType(), "Unable to Move");
        }
        DT.Dispose();
    }
}