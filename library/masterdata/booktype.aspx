<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="booktype.aspx.cs" Inherits="library_masterdata_booktype" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">
        <asp:GridView ID="gridBookType" runat="server" CssClass="gridtable" AutoGenerateColumns="False" Height="39px" style="margin-top: 34px" OnRowEditing="gridBookType_RowEditing" OnRowCancelingEdit="gridBookType_RowCancelingEdit" OnRowUpdating="gridBookType_RowUpdating" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" AllowPaging="True" OnPageIndexChanging="gridBookType_PageIndexChanging" PageSize="20" EnableModelValidation="True" OnRowDataBound="gridBookType_RowDataBound">
    <AlternatingRowStyle BackColor="#F7F7F7" />
    <Columns>
        <asp:TemplateField HeaderText="SN.">
            <ItemTemplate>
                <asp:Label ID="Label1" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Book Type Id">
            <EditItemTemplate>
                <asp:TextBox ID="txtBookTypeIdE" runat="server" Width="100%" Text='<%# Bind("BOOKTYPEID") %>' Enabled="false"></asp:TextBox>
              
            </EditItemTemplate>
            <HeaderTemplate>
                Book Type Id<br />
                <asp:TextBox ID="txtBookTypeIdH" runat="server" Width="100%" Enabled="false"></asp:TextBox>
            </HeaderTemplate>
            <ItemTemplate>
             <asp:Label ID="lblBookTypeId" runat="server" Text='<%# Bind("BOOKTYPEID") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>


        <asp:TemplateField HeaderText="Book Type">
            <EditItemTemplate>
                <asp:TextBox ID="txtBookTypeE" runat="server" Width="100%" Text='<%# Bind("BOOKTYPE") %>'></asp:TextBox>
            
            </EditItemTemplate>
            <HeaderTemplate>
                Book Type<br />
                <asp:TextBox ID="txtBookTypeH" runat="server" Width="100%"></asp:TextBox>
            </HeaderTemplate>
            <ItemTemplate>
                <asp:Label ID="lblBookType" runat="server" Text='<%# Bind("BOOKTYPE") %>'></asp:Label>
             
            </ItemTemplate>
        </asp:TemplateField>
      
        <asp:TemplateField HeaderText="Edit">
            <EditItemTemplate>
                <asp:ImageButton ID="btnEdit" runat="server" CommandName="update" ImageUrl="~/images/icons/upload.png" ToolTip="Update" />
                <asp:ImageButton ID="btnCancel" runat="server" CommandName="cancel" ImageUrl="~/images/icons/delete.gif" ToolTip="Cancel" />
            </EditItemTemplate>
            <HeaderTemplate>
                <asp:Button ID="btnAdd" runat="server" Text="Add" OnClick="btnAdd_Click" />
            </HeaderTemplate>
            <ItemTemplate>
                <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/icons/edit.png" CommandName="edit"/>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
    <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
    <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
    <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
    <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
   
</asp:GridView>
    </div>
</asp:Content>

