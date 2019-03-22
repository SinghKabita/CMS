<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ActionReward.ascx.cs" Inherits="uc_test_ActionRewardType" %>
<asp:GridView ID="gridActionReward" runat="server" CssClass="gridtable" AutoGenerateColumns="False" EnableModelValidation="True" OnRowCancelingEdit="gridActionReward_RowCancelingEdit" OnRowDataBound="gridActionReward_RowDataBound" OnRowEditing="gridActionReward_RowEditing" OnRowUpdating="gridActionReward_RowUpdating">
   <AlternatingRowStyle BackColor="#FFCCFF" />
     <Columns>
        <asp:TemplateField HeaderText="Action/Reward">
            <EditItemTemplate>
                <asp:DropDownList ID="ddlARE" runat="server">
                    <asp:ListItem>Action</asp:ListItem>
                    <asp:ListItem>Remarks</asp:ListItem>
                </asp:DropDownList>
                <asp:Label ID="lblPkidE" runat="server" Text='<%# bind("PKID") %>' Visible="False"></asp:Label>
            </EditItemTemplate>
            <HeaderTemplate>
                Action/Remarks<br />
                <asp:DropDownList ID="ddlARH" runat="server">
                    <asp:ListItem>Action</asp:ListItem>
                    <asp:ListItem>Remarks</asp:ListItem>
                </asp:DropDownList>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Label ID="lblActionReward" runat="server" Text='<%# BIND("ACTION_REWARDS") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Action/Reward Type">
            <EditItemTemplate>
                <asp:TextBox ID="txtARTypeE" runat="server" Text='<%# BIND("ACTIVITY") %>'></asp:TextBox>
            </EditItemTemplate>
            <HeaderTemplate>
                Action/Reward Type<br />
                <asp:TextBox ID="txtARTypeH" runat="server"></asp:TextBox>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Label ID="lblActionRewardType" runat="server" Text='<%# BIND("ACTIVITY") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            <EditItemTemplate>
                <asp:ImageButton ID="btnUpdate" runat="server" CommandName="Update" ImageUrl="~/images/icons/upload.png" />
                <asp:ImageButton ID="btnCancel" runat="server" CommandName="Cancel" ImageUrl="~/images/icons/cancel.png" />
            </EditItemTemplate>
            <HeaderTemplate>
                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:ImageButton ID="btnEdit" runat="server" CommandName="Edit" ImageUrl="~/images/icons/edit.png" />
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

