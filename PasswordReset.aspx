<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PasswordReset.aspx.cs" Inherits="Login1" %>

<!DOCTYPE html5>
<html>
<head runat="server">
    <title>College ERP System</title>
    <link rel="stylesheet" type="text/css" href="~/styles/layout.css" media="screen" />
    <link id="csstheme" rel="stylesheet" type="text/css" href="~/styles/theme/default.css" />
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
                <div class="big">&nbsp;Password Reset</div>

            </div>

            <div id="form">
                <form id="loginform" runat="server">

                    <div class="input">
                        <asp:TextBox ID="txtEmployeeID" runat="server" Width="250px" Height="30px" Style="padding: 5px 10px; margin: 5px 0; display: block" ReadOnly="True"></asp:TextBox>
                    </div>
                    <div class="input">
                        <asp:TextBox ID="txtUserName" runat="server" placeholder="Enter Your Username" autocomplete="off" Width="250px" Height="30px" Style="padding: 5px 10px; margin: 5px 0; display: block" ReadOnly="True"></asp:TextBox>
                    </div>
                      <div class="input">
                        <asp:Label ID="txtConfirmCode" runat="server"  Width="250px" Height="30px" Style="padding: 5px 10px; margin: 5px 0; display: block" Visible="false"></asp:Label>
                    </div>
                     <div class="input">
                        <asp:TextBox ID="txtPassword" runat="server" placeholder="New Password" Width="250px" Height="30px" Style="padding: 5px 10px; margin: 5px 0; display: block" TextMode="Password"></asp:TextBox>
                    </div>
                    <div class="input">
                        <asp:TextBox ID="txtRePassword" runat="server" placeholder="Confirm Password" Width="250px" Height="30px" Style="padding: 5px 10px; margin: 5px 0; display: block" TextMode="Password"></asp:TextBox>
                    </div>
                    <div id="error-message">
                        <asp:Label ID="lblErrorMessage" runat="server" ForeColor="Red"></asp:Label>
                    </div>

                    <div class="input">
                        <asp:Button ID="btnSave" runat="server" Text="Save" Width="250px" Height="30px" Style="padding: 5px 10px; margin: 5px 0; display: block; color: white; background-color: #4d90fe;" OnClick="btnSave_Click"/>
                    </div>



                </form>
            </div>


        </div>
    </div>

</body>
</html>
