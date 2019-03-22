using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;

public partial class library_library_LostBook : System.Web.UI.Page
{

    bookdetails BDEnt = new bookdetails();
    bookdetailsService BDSer = new bookdetailsService();

    booktype BTEnt = new booktype();
    booktypeService BTSer = new booktypeService();

    book BEnt = new book();
    bookService BSer = new bookService();

    HelperFunction hf = new HelperFunction();


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
        ddlBookType.DataTextField = "BOOKTYPE";
        ddlBookType.DataValueField = "BOOKTYPEID";
        ddlBookType.DataBind();
        ddlBookType.Items.Insert(0, "Select");

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
            LoadData();
        }
    }
    protected void ddlBookName_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadData();
    }

    private void LoadData()
    {



        if (ddlBookType.SelectedValue != "Select" && ddlBookName.SelectedValue != "Select")
        {
            BDEnt = new bookdetails();
            BDEnt.Bookid = ddlBookName.SelectedValue;

            gridBookDetails.DataSource = BDSer.GetAll(BDEnt);
            //grdGroups.Sort = "GROUPID";
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
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            Label lblBookId = (Label)e.Row.FindControl("lblBookId");
            Label lblBookName = (Label)e.Row.FindControl("lblBookName");


            Label lblBookType = (Label)e.Row.FindControl("lblBookType");
            Label lblStatus = (Label)e.Row.FindControl("lblStatus");
            Label lblStat = (Label)e.Row.FindControl("lblStat");

            BEnt = new book();
            BEnt.Bookid = lblBookId.Text;
            BEnt = (book)BSer.GetSingle(BEnt);
            if (BEnt != null)
            {

                lblBookName.Text = BEnt.Bookname;


                BTEnt = new booktype();
                BTEnt.Booktypeid = BEnt.Booktypeid;
                BTEnt = (booktype)BTSer.GetSingle(BTEnt);
                if (BTEnt != null)
                {
                    lblBookType.Text = BTEnt.Booktype;
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
    }
    protected void gridBookDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void gridBookDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void btnLost_Click(object sender, EventArgs e)
    {
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {
            GridViewRow gr = ((Button)sender).Parent.Parent as GridViewRow;
            Label lblBookDetailId = gr.FindControl("lblBookDetailId") as Label;
            BDEnt = new bookdetails();
            BDEnt.Bookdetailid = lblBookDetailId.Text;
            BDEnt = (bookdetails)BDSer.GetSingle(BDEnt);
            if (BDEnt != null)
            {
                BDEnt.Status = "0";
                BDEnt.Remarks = "Lost";
                BDSer.Update(BDEnt);
                LoadData();
            }

        }
        else
        { }
    }
}