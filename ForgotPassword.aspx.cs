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
using System.Net.Mail;
using System.Net;
public partial class Login1 : System.Web.UI.Page
{
    HelperFunction hf = new HelperFunction();
    Entity.Components.Login theLEntity = new Entity.Components.Login();
    LoginService theLService = new LoginService();

    Entity.Components.PasswordReset PREnt = new Entity.Components.PasswordReset();
    PasswordResetService PRSer = new PasswordResetService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack != true)
        {
            
            lblErrorMessage.Text = "";
            
        }
    }
    protected void txtUserName_TextChanged(object sender, System.EventArgs e)
    {
        if (txtUserName.Text != "")
        {
            Entity.Components.Login entlogin = new Entity.Components.Login();
            LoginService srvLogin = new LoginService();
            entlogin.LOGINID = txtUserName.Text;
            entlogin = (Entity.Components.Login)srvLogin.GetSingle(entlogin);
            if (entlogin != null)
            {


                Employees entEmp = new Employees();
                EmployeesService srvEmp = new EmployeesService();
                entEmp.EMPLOYEEID = entlogin.EMPLOYEEID;
                entEmp = (Employees)srvEmp.GetSingle(entEmp);
                if (entEmp != null)
                {

                    txtEmpId.Text = entEmp.EMPLOYEEID;
                    txtName.Text = hf.getEmployeeName(entEmp.EMPLOYEEID);

                    txtEmail.Text = entlogin.EMAIL;

                    hidden.Visible = true;
                    lblErrorMessage.Text = "";

                }


            }
            else
            {

                lblErrorMessage.Text="There is no such user";
                hidden.Visible = false;
            }
        }

    }
  
       protected void btnSend_Click(object sender, System.EventArgs e)
    {
        if (txtEmail.Text != "")
        {


            Random randomNumber = new Random();
            string confirmationcode = "";
            int generatedNo = randomNumber.Next(111111, 999999);

            confirmationcode = generatedNo.ToString();

            PREnt = new Entity.Components.PasswordReset();
            PREnt.EMPLOYEE_ID = txtEmpId.Text;
            PREnt = (Entity.Components.PasswordReset)PRSer.GetSingle(PREnt);
            if (PREnt != null)
            {
                PREnt.CONFIRMATION_CODE = confirmationcode;
                PRSer.Update(PREnt);

            }
            else
            {
                PREnt = new Entity.Components.PasswordReset();
                PREnt.EMPLOYEE_ID = txtEmpId.Text;
                PREnt.CONFIRMATION_CODE = confirmationcode;
                PRSer.Insert(PREnt);

            }




            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
            mail.From = new MailAddress("noreplyphyegan@gmail.com");
            mail.To.Add(txtEmail.Text);
          
            mail.Subject = "Password Recovery ";
            mail.Body += " <html>";
            mail.Body += "<head>";

            mail.Body += "</head>";
            mail.Body += "<body>";
            mail.Body += "<div style='width: 450px;margin:auto;text-align: center;padding: 10px;background-color:#ccc;'><div style='background-color: white; overflow: hidden;'><h1 style='color: #5379ff;'>Hi, " + txtName.Text + "!</h1><p style='font-size: 16px; margin:30px 0;'></p><p style='font-size: 16px; margin:30px 0;'>Click on that button will take you to new page <br>which will you to reset your password.</p><p style='font-size: 16px; margin:30px 0;'><a href='http:192.168.0.5/cms/PasswordReset.aspx?id=" + txtEmpId.Text + "&confirm=" + confirmationcode + "' style='font-weight: bold;padding: 5px 10px;background-color: #3356ff;text-align: center;font-size: 24px;text-decoration: none;color:white;border:1px inset #5656ff;border-radius: 5px;'>Click Here</a></p><p style='font-size: 16px; margin:30px 0;'>If You are unable to click the button, follow the link:</p><p style='font-size: 16px; margin:30px 0;'><a href='http:192.168.0.5/cms/PasswordReset.aspx?id=" + txtEmpId.Text + "&confirm=" + confirmationcode + "'>http:~/PasswordReset.aspx</a></p><br><p style='font-size: 16px; margin:30px 0;'>For More Support <br>Contact Phyegan.</p><p style='font-size: 16px; margin:30px 0;'><img style='height: 10px ;margin-right: 10px;' src='http://phyegan.com/phyegan.gif'></p></div></div>";
            mail.Body += "</body>";
            mail.Body += "</html>";

            mail.IsBodyHtml = true;
            SmtpServer.Port = 587;
            SmtpServer.Credentials = new System.Net.NetworkCredential("noreplyphyegan@gmail.com", "~!@#$%^^%$#@!~");
            SmtpServer.EnableSsl = true;
            SmtpServer.Send(mail);
            HelperFunction.MsgBox(this, this.GetType(), "Successfully Email has been Sent.");
        }
        else
        {

            HelperFunction.MsgBox(this, this.GetType(), "Sorry You haven't included your Email Id. For more help visit NCCS IT Department.");


        }


       


        
    }
}
