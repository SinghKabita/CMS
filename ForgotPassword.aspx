<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ForgotPassword.aspx.cs" Inherits="Login1" %>

<!DOCTYPE html5>
<html>
<head runat="server">
    <title></title>
  <link rel="stylesheet" type="text/css" href="~/css/layout.css" media="screen" />
       <link id="csstheme" rel="stylesheet" type="text/css" href="~/css/theme/default.css" />
</head>
<body>
     <div class="header">
    
         
                   <a id="logo" href="//localhost/college/Login.aspx">CMS</a>
          
      
       
    </div>

    <div id="main-login">

        <div id="login">
            <div id="login-logo">
                NCCS
            </div>

            <div id="heading">
                <div class="big">Forgot Password Recovery</div>
               
            </div>

            <div id="form">
                <form id="loginform" runat="server">
                
                <div class="input">
                    <asp:TextBox ID="txtUserName" runat="server" placeholder="Enter Your Username" autocomplete="off" width="250px" Height="30px" style="padding:5px 10px; margin:5px 0; display:block" AutoPostBack="True" OnTextChanged="txtUserName_TextChanged"></asp:TextBox>
                </div>
                     <div id="error-message">
                    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                </div>
                    <div id="hidden" runat="server" visible="false">
                         <div class="input">
                    <asp:TextBox ID="txtEmpId" runat="server"  width="250px" Height="30px" style="padding:5px 10px; margin:5px 0; display:block" ReadOnly="True"></asp:TextBox>
                </div>
                <div class="input">
                    <asp:TextBox ID="txtName" runat="server"  width="250px" Height="30px" style="padding:5px 10px; margin:5px 0; display:block" ReadOnly="True"></asp:TextBox>
                </div>
                     <div class="input">
                    <asp:TextBox ID="txtEmail" runat="server"  width="250px" Height="30px" style="padding:5px 10px; margin:5px 0; display:block" ReadOnly="True"></asp:TextBox>
                </div>
               
                <div class="input">
                    <asp:Button ID="btnSend" runat="server" Text="Send" width="250px" Height="30px" style="padding:5px 10px; margin:5px 0; display:block; color:white; background-color:#4d90fe;" OnClick="btnSend_Click"/>
                </div>
                        </div>

                
                     </form>
            </div>
           
            
        </div>
    </div>

</body>
</html>
