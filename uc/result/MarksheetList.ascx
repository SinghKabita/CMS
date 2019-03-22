<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MarksheetList.ascx.cs" Inherits="uc_result_MarksheetList" %>
<table>
    <tr>
        <td>
            Faculty</td>
        <td>
            <asp:DropDownList ID="ddlFaculty" runat="server">
                <asp:ListItem Value="1">Management</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
       
    </tr>
    <tr>
        <td>
            Class
        </td>
        <td>
            <asp:DropDownList ID="ddlClass" runat="server" Width="80px">
                <asp:ListItem>11</asp:ListItem>
                <asp:ListItem>12</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="auto-style2">
            Section
        </td>
        <td>
            <asp:DropDownList ID="ddlSection" runat="server" Width="131px" AutoPostBack="True">
            </asp:DropDownList>
        </td>
       
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
        <td>
        </td>
        <td>
            <asp:Button ID="btnShow" runat="server" Text="Show Mark Ledger" Width="110px" OnClick="btnShow_Click" />
        </td>
        
    </tr>
    <tr>
        <td>
        </td>
        <td>
        </td>
        <td>
            &nbsp;</td>
        <td>
            &nbsp;</td>
       
    </tr>
    
</table>
<asp:GridView ID="grdList" runat="server" CssClass="gridtable" AutoGenerateColumns="False" 
    onrowdatabound="grdList_RowDataBound">
    <Columns>
        <asp:TemplateField HeaderText="Reg. No">
            <ItemTemplate>
                <asp:Label ID="lblStudentId" runat="server" Text='<%# BIND("STUDENT_ID") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Name">
            <ItemTemplate>
                <asp:Label ID="lblName" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="btnMarks" runat="server" Text="View" onclick="btnMarks_Click" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
