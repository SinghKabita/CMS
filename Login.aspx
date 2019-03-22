<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login1" %>

<!DOCTYPE html5>
<html>
<head runat="server">
    <title>College ERP System</title>
    <link rel="icon" href="images/icons/bwonekuthi.png" type="image/x-icon"/>
    <link rel="stylesheet" type="text/css" href="~/css/layout.css" />
</head>
<body>
    <div id="main-login">

        <div id="login">
            <div>
              <asp:Image ID="imgCollege" runat="server" Style="height:80px"  ImageUrl="~/images/nccs.png"/>
            </div>

            <div id="heading">
                <div class="big">College ERP System</div>
                
            </div>

            <div id="form">
                <form id="loginform" runat="server">
                <div id="profile-pic">
                    <asp:Image ID="imgProfilePic" runat="server" Style="height:100%; border-radius: 100%"/>
                </div>

                <div class="input">
                    <asp:TextBox ID="txtUserName" runat="server" placeholder="Username or Email" autocomplete="off" width="250px" Height="30px" style="padding:5px 10px; margin:5px 0; display:block"></asp:TextBox>
                </div>
                <div class="input">
                    <asp:TextBox ID="txtPassword" runat="server" placeholder="Password" autocomplete="off" TextMode="Password" width="250px" Height="30px" style="padding:5px 10px; margin:5px 0; display:block"></asp:TextBox>
                </div>
                <div id="error-message">
                    <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                </div>
                <div class="input">
                    <asp:Button ID="btnSignIn" runat="server" Text="Sign In" width="250px" Height="30px" style="padding:5px 10px; margin:5px 0; display:block; color:white; background-color:#4d90fe;" OnClick="btnSignIn_Click"/>
                </div>

                 <a id="forgot-password" href="~/ForgotPassword.aspx">Forgot Password?</a>
                     </form>
            </div>
           
            <div id="other-systems">
               
               
                    
            </div>

        </div>
    </div>

</body>
</html>
