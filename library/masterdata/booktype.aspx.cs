using Entity.Components;
using Service.Components;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class library_masterdata_booktype : System.Web.UI.Page
{

    booktype BTEnt = new booktype();
    booktypeService BTSer = new booktypeService();

    HelperFunction hf = new HelperFunction();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();

        }
    }

    private void LoadData()
    {
        BTEnt = new booktype();
        gridBookType.DataSource = BTSer.GetAll(BTEnt);
        //grdGroups.Sort = "GROUPID";
        gridBookType.DataBind();

        if (gridBookType.Rows.Count == 0)
        {
            ArrayList a1 = new ArrayList();
            BTEnt = new booktype();
            a1.Add(BTEnt);

            gridBookType.DataSource = a1;
            gridBookType.DataBind();
        }
    }

    protected void btnAdd_Click(object sender, EventArgs e)
    {
        GridViewRow row = gridBookType.HeaderRow;


        TextBox txtBookTypeIdH = (TextBox)row.FindControl("txtBookTypeIdH");
        TextBox txtBookType = (TextBox)row.FindControl("txtBookTypeH");




        booktype BTEnt = new booktype();

        BTEnt.Booktypeid = txtBookTypeIdH.Text;
        BTEnt.Booktype = txtBookType.Text;




        BTSer.Insert(BTEnt);


        // gridSubProductDetails.EditIndex = -1;
        LoadData();
    }

    protected void gridBookType_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = gridBookType.Rows[e.RowIndex];
        Label lblBookTypeIdU = (Label)row.FindControl("lblBookTypeIdU");
        TextBox txtBookTypeE = (TextBox)row.FindControl("txtBookTypeE");
        TextBox txtBookTypeIdE = (TextBox)row.FindControl("txtBookTypeIdE");





        BTEnt = new booktype();

        BTEnt.Booktypeid = txtBookTypeIdE.Text;

        BTEnt.Booktype = txtBookTypeE.Text;





        BTSer.Update(BTEnt);

        gridBookType.EditIndex = -1;

        LoadData();
    }

    protected void gridBookType_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gridBookType.EditIndex = e.NewEditIndex;
        LoadData();
    }
    protected void gridBookType_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gridBookType.EditIndex = -1;
        LoadData();
    }

    protected void gridBookType_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gridBookType.PageIndex = e.NewPageIndex;
        LoadData();
    }
    protected void gridBookType_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            TextBox txtBookTypeIdH = e.Row.FindControl("txtBookTypeIdH") as TextBox;

            txtBookTypeIdH.Text = Convert.ToDouble(hf.getMaxBookTypeId()).ToString("000");
        }
    }
}