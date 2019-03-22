using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Service.Components;
using Service.Framework;
using System.Security;
using System.Security.Authentication;
using System.Web.Security;
using Entity.Components;
using Entity.Framework;
using System.Data.OracleClient;
using System.Net.NetworkInformation;


using System.Xml.Linq;
using System.Xml;
using System.Net;
using System.Net.Sockets;
using System.Web.Script.Serialization;
using System.IO;

public partial class Login1 : System.Web.UI.Page
{
    UserProfileEntity userProfileEnt = new UserProfileEntity();

    fiscalyear theFYEntity = new fiscalyear();
    fiscalyearService theFYService = new fiscalyearService();

    Entity.Components.Login theLEntity = new Entity.Components.Login();
    LoginService theLService = new LoginService();
    HelperFunction hf = new HelperFunction();
    string EmployeeId, OfficeId;
    string imgfolder;
    string cokUsername, cokPassword;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            if (IsPostBack != true)
            {
                if (User.Identity.IsAuthenticated)
                {

                    FormsAuthentication.SignOut();
                    Session.Clear();


                }
                string photo = "";
                try
                {
                    photo = Request.Cookies["uid"].Value;
                }
                catch { }

                if (!string.IsNullOrEmpty(photo))
                {
                    imgfolder = Server.MapPath(@"~/images/user/") + photo + ".jpg";
                    if (File.Exists(imgfolder))
                        imgProfilePic.ImageUrl = @"~/images/user/" + photo + ".jpg";
                    else
                        imgProfilePic.ImageUrl = @"~/images/profile.png";
                }
                else
                    imgProfilePic.ImageUrl = @"~/images/profile.png";


            }
            txtUserName.Focus();
        }
    }


    protected void LoginCheck(string username, string password)
    {
        LoginServices loginServ = new LoginServices();
        UserProfileEntity userProfileEnt = new UserProfileEntity();

        userProfileEnt = (UserProfileEntity)loginServ.Validate(username, password, null);

        theLEntity = new Entity.Components.Login();
        theLService = new LoginService();

        theLEntity.LOGINID = username;
        theLEntity.PASSWORD = password;
        theLEntity = (Entity.Components.Login)theLService.GetSingle(theLEntity);
        if (theLEntity != null)
        {
            EmployeeId = theLEntity.EMPLOYEEID;
        }
        else
        {
            EmployeeId = "0";
            lblErrorMessage.Visible = true;
            lblErrorMessage.Text = "Can not Login";
            return;
        }
        // start of new code
        OracleConnection conn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        string sql = "select  officecode from employees where employeeid='" + EmployeeId + "'";
        OracleCommand cmd = new OracleCommand(sql, conn);
        conn.Open();
        try
        {
            OfficeId = Convert.ToString(cmd.ExecuteScalar()) == "" ? "0" : Convert.ToString(cmd.ExecuteScalar());
            if (!string.IsNullOrEmpty(OfficeId))
                userProfileEnt.LocationID = OfficeId;
        }
        catch { }
        finally
        {
            conn.Close();
        }
        OFFICE oEntity = new OFFICE();
        officeService oService = new officeService();
        oEntity.OFFICECODE = OfficeId;
        oEntity = (OFFICE)oService.GetSingle(oEntity);
        if (oEntity != null)
        {
            userProfileEnt.LocationName = oEntity.OFFICENAME;
        }




        if (userProfileEnt.LoginAccess == true)
        {
            string NYear = hf.NepaliYear();
            string NMonth = hf.NepaliMonth();
            Page.Session["UserProfile"] = userProfileEnt;
            userProfileEnt.FiscalYear = hf.checkFiscalYear(NMonth, NYear);
            userProfileEnt.EmployeeID = theLEntity.EMPLOYEEID;
            Response.Redirect("~/Default.aspx");
            //FormsAuthentication.RedirectFromLoginPage(userProfileEnt.UserName, true);
        }
        else
        {
            //userProfileEnt.FiscalYear = this.ddFiscalYear.SelectedValue;
            
            //this.lblError.Text = userProfileEnt.Remarks;
            //this.lblError.Visible = true;


            Page.Session["UserProfile"] = userProfileEnt;
            //if (RememberMe.Checked)
            //{
            //    HttpCookie UserInfo = new HttpCookie("UserInfo");
            //    UserInfo.Values["username"] = username;
            //    UserInfo.Values["password"] = password;
            //    Response.Cookies.Add(UserInfo);
            //}

            FormsAuthentication.RedirectFromLoginPage(userProfileEnt.UserName, true);
        }
    }
    protected void btnSignIn_Click(object sender, EventArgs e)
    {
        if (txtUserName.Text != "" && txtPassword.Text != "")
        {
            LoginCheck(txtUserName.Text, txtPassword.Text);
        }
        else
        {
            lblErrorMessage.Visible = true;
            lblErrorMessage.Text = "Please Complete Fields";
        }
    }
}