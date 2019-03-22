using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Entity.Components;
using Entity.Framework;
using Service.Components;
using System.Data;

public partial class uc_StorageDeviceType : System.Web.UI.UserControl
{
    Groups anEntity = new Groups();
    GroupsService myService = new GroupsService();
    UserProfileEntity userProfileEnt = new UserProfileEntity();
    string m_strSortExp;
    System.Web.UI.WebControls.SortDirection m_SortDirection;
    DataSet theEntListdb;
    EntityList theEntList;
    DataView dv;
    UserPageAccess entUserPageAcc = new UserPageAccess();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
    }

    private void LoadData()
    {
        anEntity = new Groups();
        grdGroups.DataSource = myService.GetAll(anEntity);
        //grdGroups.Sort = "GROUPID";
        grdGroups.DataBind();

        if (grdGroups.Rows.Count == 0)
        {
            ArrayList a1 = new ArrayList();
            anEntity = new Groups();
            a1.Add(anEntity);

            grdGroups.DataSource = a1;            
            grdGroups.DataBind();
        }
    }

    protected void grdGroups_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdGroups.EditIndex = -1;
        LoadData();
    }

    protected void grdGroups_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblGroupid = (Label)e.Row.FindControl("lblGroupid");
            if (lblGroupid.Text == string.Empty)
            {
                ImageButton btnEdit = (ImageButton)e.Row.FindControl("btnEdit");
                e.Row.Visible = false;
            }
            if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) != 0)
            {
                ((ImageButton)e.Row.FindControl("btnDelete")).Attributes["onclick"] = "if(!confirm('Are you sure ?'))return false";
            }
        }
    }

    protected void grdGroups_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = grdGroups.Rows[e.RowIndex];
        Label lblGroupid = (Label)row.FindControl("lblGroupid");

        anEntity.GROUPID = lblGroupid.Text;
        myService.Delete(anEntity);

        grdGroups.EditIndex = -1;
        LoadData();
    }

    protected void grdGroups_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdGroups.EditIndex = e.NewEditIndex;
        LoadData();

    }

    protected void grdGroups_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        GridViewRow row = grdGroups.Rows[e.RowIndex];
        Label lblGroupid = (Label)row.FindControl("lblGroupid");
        TextBox txtGroupname = (TextBox)row.FindControl("txtGroupname");
        TextBox txtGroupprecedence = (TextBox)row.FindControl("txtGroupprecedence");
        TextBox txtGroupdetails = (TextBox)row.FindControl("txtGroupdetails");


        anEntity = new Groups();
        anEntity.GROUPID = lblGroupid.Text;
        anEntity.GROUPNAME = txtGroupname.Text;
        anEntity.GROUPPRECEDENCE = txtGroupprecedence.Text;
        anEntity.GROUPDETAILS = txtGroupdetails.Text;

        myService.Update(anEntity);

        grdGroups.EditIndex = -1;
        LoadData();

    }

    protected void btnAdd_Click1(object sender, ImageClickEventArgs e)
    {
        GridViewRow row = grdGroups.HeaderRow;
        TextBox txtGroupid = (TextBox)row.FindControl("txtGroupid");
        TextBox txtGroupname = (TextBox)row.FindControl("txtGroupname");
        TextBox txtGroupprecedence = (TextBox)row.FindControl("txtGroupprecedence");
        TextBox txtGroupdetails = (TextBox)row.FindControl("txtGroupdetails");

        anEntity = new Groups();

        anEntity.GROUPID = txtGroupid.Text;
        anEntity.GROUPNAME = txtGroupname.Text;
        anEntity.GROUPPRECEDENCE = txtGroupprecedence.Text;
        anEntity.GROUPDETAILS = txtGroupdetails.Text;

        myService.Insert(anEntity);
        LoadData();

    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        GridViewRow grdrow = (GridViewRow)grdGroups.FooterRow;
        TextBox txtPdoNo = (TextBox)grdGroups.FooterRow.FindControl("txtGroupid");
        TextBox txtProductName = (TextBox)grdGroups.FooterRow.FindControl("txtGroupname");
        TextBox txtPdoDate = (TextBox)grdGroups.FooterRow.FindControl("txtGroupprecedence");
        TextBox txtValidDate = (TextBox)grdGroups.FooterRow.FindControl("txtGroupdetails");
       
        try
        {
            theEntList = myService.GetAll(anEntity);

            //if (theEntList.Count == 0)
            //{
            //    theEntList.Add(new Pdo());
            //}
            grdGroups.DataSource = theEntList;
            grdGroups.DataBind();
        }
        catch (Exception error)
        {
            if (theEntList.Count > 0)
            {
                grdGroups.DataSource = theEntList;
                grdGroups.DataBind();
            }
            else
            {
                grdGroups.DataSource = null;
                grdGroups.DataBind();
            }
        }

    }     
    int GetSortColumnIndex(string SortExpr)
    {
        int i = 0;
        for (i = 0; i < grdGroups.Columns.Count; i++)
        {
            if (grdGroups.Columns[i].SortExpression == SortExpr)
                break;
        }
        return i;
    }
    void AddSortImage(GridViewRow headerRow)
    {
        Int32 iCol = GetSortColumnIndex(m_strSortExp);
        if (-1 == iCol || grdGroups.Columns.Count == iCol)
        {
            return;
        }
        // Create the sorting image based on the sort direction.
        Image sortImage = new Image();
        if (SortDirection.Ascending == m_SortDirection)
        {
            sortImage.ImageUrl = "../Images/sort_desc.gif";
            sortImage.AlternateText = "Ascending Order";
        }
        else
        {
            sortImage.ImageUrl = "../Images/sort_asc.gif";
            sortImage.AlternateText = "Descending Order";
        }

        // Add the image to the appropriate header cell.
        headerRow.Cells[iCol].Controls.Add(sortImage);
    }
    protected void grdGroups_Sorting1(object sender, GridViewSortEventArgs e)
    {
        try
        {
            if (String.Empty != m_strSortExp)
            {
                if (String.Compare(e.SortExpression, m_strSortExp, true) == 0)
                {
                    m_SortDirection = (m_SortDirection == System.Web.UI.WebControls.SortDirection.Ascending) ? System.Web.UI.WebControls.SortDirection.Descending : System.Web.UI.WebControls.SortDirection.Ascending;
                }
            }
            ViewState["_Direction_"] = m_SortDirection;
            ViewState["_SortExp_"] = m_strSortExp = e.SortExpression;
            //this.bindGridView();


            DataTable dt = theEntListdb.Tables[0];
            dv = new DataView(dt);
            if (m_SortDirection == System.Web.UI.WebControls.SortDirection.Ascending)
            {
                dv.Sort = e.SortExpression + " " + "Asc";

            }
            else if (m_SortDirection == System.Web.UI.WebControls.SortDirection.Descending)
            {
                dv.Sort = e.SortExpression + " " + "Desc";
            }
            grdGroups.DataSource = dv;
            grdGroups.DataBind();
        }
        catch
        {
        }
    }
    protected void grdGroups_PageIndexChanging1(object sender, GridViewPageEventArgs e)
    {
        grdGroups.PageIndex = e.NewPageIndex;
        LoadData();
    }
}
