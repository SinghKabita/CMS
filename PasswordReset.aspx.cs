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
using System.Web.Mail;
using Entity.Components;
using Service.Components;



public partial class Login1 : System.Web.UI.Page
{
  
    Entity.Components.Login LEnt;
    LoginService LSer = new LoginService();
   

    Entity.Components.PasswordReset PREnt = new Entity.Components.PasswordReset();
    Service.Components.PasswordResetService PRSer = new Service.Components.PasswordResetService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            Session.Clear();
            

            try
            {
                 string id = Request.QueryString["id"];
                 string confirm = Request.QueryString["confirm"];

                 LoadDetail(id,confirm);
            }
            catch { }
        }
    }

     protected void LoadDetail(string id,string confirm)
    {
        txtEmployeeID.Text = id;
        txtConfirmCode.Text = confirm;

        LEnt = new Entity.Components.Login();
        LEnt.EMPLOYEEID = id;
        LEnt = (Entity.Components.Login)LSer.GetSingle(LEnt);
        if (LEnt != null)
        {
            txtUserName.Text = LEnt.LOGINID;
        }

        
    
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        PREnt = new Entity.Components.PasswordReset();
        PREnt.EMPLOYEE_ID = txtEmployeeID.Text;
        PREnt.CONFIRMATION_CODE = txtConfirmCode.Text;
        PREnt = (Entity.Components.PasswordReset)PRSer.GetSingle(PREnt);
        if (PREnt != null)
        {
            if (txtPassword.Text == txtRePassword.Text)
            {
                LEnt = new Entity.Components.Login();
                LEnt.EMPLOYEEID = txtEmployeeID.Text;

                LEnt = (Entity.Components.Login)LSer.GetSingle(LEnt);
                if (LEnt != null)
                {
                    LEnt.PASSWORD = txtPassword.Text;
                    if (LSer.Update(LEnt) >= 1)
                    {
                        lblErrorMessage.Text="Successfully Reset your Password!!";
                        lblErrorMessage.ForeColor = System.Drawing.Color.Green;
                        PREnt = new Entity.Components.PasswordReset();
                        PREnt.EMPLOYEE_ID = txtEmployeeID.Text;
                        PRSer.Delete(PREnt);
                        clearFields();

                    }
                    else
                    {
                        lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                        lblErrorMessage.Text="Sorry Something goes wrong.";

                    }



                }

            }
            else
            {
                lblErrorMessage.ForeColor = System.Drawing.Color.Red;
                lblErrorMessage.Text="Password Not Matched. Try Again";
            }

        }
        else
        {
            lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            lblErrorMessage.Text="Invalid Confirmation Code";
        }
    }


    protected void clearFields()
    {
        txtEmployeeID.Text = "";
        txtConfirmCode.Text = "";
        txtPassword.Text = "";
        txtRePassword.Text = "";
        txtUserName.Text = "";

    }
}
  


    

 
