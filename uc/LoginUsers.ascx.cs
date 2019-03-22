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
using NCCSEncryption;
using DataAccess.Framework;
using DataHelper.Framework;
//using System.Data.OracleClient;

public partial class uc_LoginUsers : System.Web.UI.UserControl
{
    UserProfileEntity userProfileEnt = new UserProfileEntity();
    string m_strSortExp;
    System.Web.UI.WebControls.SortDirection m_SortDirection;
    DataSet theEntListdb;
    EntityList theEntList;
    DataView dv;
    UserPageAccess entUserPageAcc = new UserPageAccess();
    HelperFunction hf = new HelperFunction();

    Entity.Components.Login LEnt = new Entity.Components.Login();
    LoginService LSSer = new LoginService();
    HelperFunction mf = new HelperFunction();

    private void ClearFields()
    {
        txtLoginid.Text = string.Empty;
        txtFulldetails.Text = string.Empty;
        empId.Text = string.Empty;
        ddlGroupid.SelectedIndex = 0;
     
       

    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            userProfileEnt = (UserProfileEntity)HttpContext.Current.Session["UserProfile"];
            LoadGroup();
            LoadGrid();
        }

    }

    public void LoadGroup()
    {
        Groups anEntity = new Groups();
        GroupsService myService = new GroupsService();
        ddlGroupid.DataSource = myService.GetAll(anEntity);
        ddlGroupid.DataTextField = "GROUPNAME";
        ddlGroupid.DataValueField = "GROUPID";
        ddlGroupid.DataBind();
        ListItem item = new ListItem("Please Select", "0");
        ddlGroupid.Items.Insert(0, item);
    }

    public void LoadData(string loginid)
    {

        LEnt = new Entity.Components.Login();
       

        LEnt.LOGINID = loginid;
        LEnt = (Entity.Components.Login)LSSer.GetSingle(LEnt);

        if (LEnt != null)
        {
            txtLoginid.Text = LEnt.LOGINID;
            txtFulldetails.Text = LEnt.FULLDETAILS;
            empId.Text = LEnt.EMPLOYEEID;
            ddlGroupid.SelectedValue = LEnt.GROUPID;


            if (LEnt.ACCESSBLOCKED == "1")
            {
                rbtnBlocked.Checked = true;
                rbtnUnblocked.Checked = false;
            }
            else
            {
                rbtnBlocked.Checked = false;
                rbtnUnblocked.Checked = true;
            }
        }
        else
        {
            txtLoginid.Text = null;
            txtFulldetails.Text = null;
            ddlGroupid.SelectedIndex = 0;
         
            
        }
    }

    
    private void LoadGrid()
    {
        LEnt = new Entity.Components.Login();
        EntityList theList = new EntityList();
        theList = LSSer.GetAll(LEnt);
        EntityList theListNew = new EntityList();
        userProfileEnt = (UserProfileEntity)HttpContext.Current.Session["UserProfile"];
        if (userProfileEnt.UserName == "phyegan")
        {
            GridView1.DataSource = theList;
            GridView1.DataBind();
        }
        else
        {
            foreach (Entity.Components.Login ln in theList)
            {
                if (ln.LOGINID != "phyegan")
                    theListNew.Add(ln);
            }
            GridView1.DataSource = theListNew;
            GridView1.DataBind();
        }

        if (GridView1.Rows.Count == 0)
        {
            ArrayList a1 = new ArrayList();
            LEnt = new Entity.Components.Login();
            a1.Add(LEnt);

            GridView1.DataSource = a1;
            GridView1.DataBind();
        }
    }


    
    protected void btnSave_Click(object sender, EventArgs e)
    {

        LEnt = new Entity.Components.Login();
        LEnt.EMPLOYEEID = empId.Text;
        LEnt = (Entity.Components.Login)LSSer.GetSingle(LEnt);
        {
            if (LEnt != null)
            {
                LEnt.LOGINID = txtLoginid.Text;

                LEnt.FULLDETAILS = txtFulldetails.Text;
                LEnt.EMPLOYEEID = empId.Text;
                LEnt.GROUPID = ddlGroupid.SelectedValue;




                if (rbtnBlocked.Checked == true)
                {
                    LEnt.ACCESSBLOCKED = "1";
                }
                else
                {
                    LEnt.ACCESSBLOCKED = "0";
                }


                LSSer.Update(LEnt);


                HelperFunction.MsgBox(this, this.GetType(), "Data saved Succesfully!!!");
                LoadGrid();
            }
        }
       

    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        GridViewRow row = (GridViewRow)((ImageButton)sender).NamingContainer;
        Label lblLoginid = (Label)row.FindControl("lblLoginid");
        txtLoginid.Enabled = false;
        LoadData(lblLoginid.Text);
        btnPopup_ModalPopupExtender.Show();

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow row = GridView1.Rows[e.RowIndex];
        Label lblLoginid = (Label)row.FindControl("lblLoginid");
        LEnt.LOGINID = lblLoginid.Text;
        LSSer.Delete(LEnt);

        GridView1.EditIndex = -1;
        LoadGrid();
    }
    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        ClearFields();
        btnPopup_ModalPopupExtender.Show();
        txtLoginid.ReadOnly = false;
        txtLoginid.Enabled = true;
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        LoadGrid();
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    {
        GridViewRow grdrow = (GridViewRow)GridView1.FooterRow;
        TextBox txtLoginid = (TextBox)GridView1.FooterRow.FindControl("txtLoginid");
        TextBox txtFulldetails = (TextBox)GridView1.FooterRow.FindControl("txtFulldetails");
        TextBox txtEmployeeid = (TextBox)GridView1.FooterRow.FindControl("txtEmployeeid");
        TextBox txtGroupid = (TextBox)GridView1.FooterRow.FindControl("txtGroupid");
        TextBox txtEmail = (TextBox)GridView1.FooterRow.FindControl("txtEmail");
        TextBox txtbu = (TextBox)GridView1.FooterRow.FindControl("txtbu");


        LEnt = new Entity.Components.Login();
      
        EntityList theList = new EntityList();
        if (txtLoginid != null && txtLoginid.Text != "")
        {
            LEnt.LOGINID = txtLoginid.Text;
        }
        if (txtFulldetails != null && txtFulldetails.Text != "")
        {
            LEnt.FULLDETAILS = txtFulldetails.Text;
        }
        if (txtEmployeeid != null && txtEmployeeid.Text != "")
        {
            LEnt.EMPLOYEEID = txtEmployeeid.Text;
        }
        if (txtGroupid != null && txtGroupid.Text != "")
        {
            LEnt.EMPLOYEEID = txtEmployeeid.Text;
        }
        theList = LSSer.GetAll(LEnt);
        if (theList.Count == 0)
        {
            LEnt = new Entity.Components.Login();
          theList.Add(LEnt);
        }
        GridView1.DataSource = theList;
        GridView1.DataBind();

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {
            Label lblGroupid = e.Row.FindControl("lblGroupid") as Label;
            Label lblGroupName = e.Row.FindControl("lblGroupName") as Label;

            Label lblOfficeName = e.Row.FindControl("lblOfficeName") as Label;
            Label lblEmployeeid = e.Row.FindControl("lblEmployeeid") as Label;

            
            Groups entGroup = new Groups();
            GroupsService srvGroups = new GroupsService();

            if (lblGroupid != null)
            {
                if (lblGroupid.Text != "")
                {
                    entGroup.GROUPID = lblGroupid.Text;
                    entGroup = (Groups)srvGroups.GetSingle(entGroup);
                    if (entGroup != null)
                        lblGroupName.Text = entGroup.GROUPNAME;
                    else
                        lblGroupName.Text = "N/A";
                }
                else
                {
                    lblGroupName.Text = "N/A";
                }
            }
            else
            {
                lblGroupName.Text = "N/A";
            }
        }
    }
   
}
