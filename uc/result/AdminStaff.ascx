<%@ Control Language="C#" AutoEventWireup="true" CodeFile="AdminStaff.ascx.cs" Inherits="uc_test_AdminStaff" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

 <table class="auto-style1">
    <tr>
        <td class="auto-style1">
            <asp:Label ID="lblPost" runat="server" Text="Post"></asp:Label>
        </td>
        <td class="auto-style1">
            <asp:TextBox ID="txtPost" runat="server"></asp:TextBox>
            <asp:Label ID="lblPKIDU" runat="server" Visible="False"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="auto-style1">
            <asp:Label ID="lblName" runat="server" Text="Full Name"></asp:Label>
        </td>
        <td class="auto-style1">
            <asp:TextBox ID="txtName" runat="server"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="auto-style1">
            <asp:Label ID="lblSign" runat="server" Text="Signature Image"></asp:Label>
        </td>
        <td class="auto-style1">
            <asp:FileUpload ID="fuSignature" runat="server"/>
        </td>
    </tr>
    <tr>
        <td class="auto-style1">
            <asp:Label ID="lblStat" runat="server" Text="Status"></asp:Label>
        </td>
        <td class="auto-style1">
            <asp:DropDownList ID="ddlStatus" runat="server">
                <asp:ListItem Value="1">Active</asp:ListItem>
                <asp:ListItem Value="0">Inactive</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
    <tr>
        <td class="auto-style2">
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" Height="26px" Visible="False"/>
        </td>
        <td class="auto-style1">
            <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"/>
        </td>

    </tr>
    <tr>
        <td class="auto-style2">
            &nbsp;</td>
        <td class="auto-style2">
            &nbsp;</td>
    </tr>
</table>
<asp:GridView ID="gridAdminStaff" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" OnRowCommand="gridAdminStaff_RowCommand" OnRowDataBound="gridAdminStaff_RowDataBound">
  
      <Columns>
        <asp:TemplateField HeaderText="Sn">
            <ItemTemplate>
                <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Post">
            <ItemTemplate>
                <asp:Label ID="lblPost" runat="server" Text='<%# bind("POST") %>'></asp:Label>
                <asp:Label ID="lblPKID" runat="server" Text='<%# bind("pkid") %>' Visible="False"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Name">
            <ItemTemplate>
                <asp:Label ID="lblName" runat="server" Text='<%# bind("NAME") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Signature">
            <ItemTemplate>
                <asp:Image ID="imgSign" runat="server" ImageUrl='<%# bind("SIGNATURE") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Status">
            <ItemTemplate>
                <asp:Label ID="lblStatus" runat="server" Text='<%# bind("Status") %>' Visible="False"></asp:Label>
                <asp:Label ID="lblStatusName" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/icons/edit.png" CommandName="View" OnClick="ImageButton1_Click" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>



