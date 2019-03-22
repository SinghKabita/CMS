<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BOOKSHELFCOMPART.aspx.cs" Inherits="library_masterdata_BOOKSHELFCOMPART" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row">
            <asp:Label Text="" ID="lblSelectedShelf" Visible="false" runat="server" />
            <asp:GridView ID="gridBookShelfCompart" CssClass="gridtable" Width="500px" AutoGenerateColumns="false" runat="server"
                OnRowEditing="gridBookShelfCompart_RowEditing"
                OnRowCancelingEdit="gridBookShelfCompart_RowCancelingEdit"
                OnRowUpdating="gridBookShelfCompart_RowUpdating"
                OnRowDataBound="gridBookShelfCompart_RowDataBound">
                <HeaderStyle BackColor="#6699ff" />
                <AlternatingRowStyle BackColor="#ccffff" />
                <HeaderStyle BackColor="Silver" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>SN</HeaderTemplate>
                        <ItemTemplate>
                            <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                            <asp:Label Text='<%# Bind("PK_ID") %>' ID="lblPK" Visible="false" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Shelf
                            <br />
                            <asp:DropDownList OnSelectedIndexChanged="ddlShelfH_SelectedIndexChanged" ID="ddlShelfH" AutoPostBack="true" runat="server">
                            </asp:DropDownList>
                            
                        </HeaderTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="ddlShelfE" runat="server">
                            </asp:DropDownList>
                            <asp:Label Text='<%# Bind("SHELFID") %>' Visible="false" ID="lblShelfidE" runat="server" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label Text='<%# Bind("SHELFID") %>' ID="lblShelfid" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            Compart No.
                            <asp:TextBox ID="txtCompartNoH" placeholder="..type here" Width="60px" runat="server" /><br />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="required" ValidationGroup="AddBtn" ControlToValidate="txtCompartNoH" runat="server" />
                        </HeaderTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtCompartnoE" runat="server" />
                            <asp:Label Text='<%# Bind("COMPARTNO") %>' Visible="false" ID="lblCompartNoE" runat="server" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:Label Text='<%# Bind("COMPARTNO") %>' ID="lblCompartNo" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:Button Text="Add" ID="btnAdd" OnClick="btnAdd_Click"  ValidationGroup="AddBtn" runat="server" />
                        </HeaderTemplate>
                        <EditItemTemplate>
                            <asp:ImageButton ID="imgbtnUpdate" runat="server" ImageUrl="~/images/icons/upload.png" CommandName="Update" />
                            <asp:ImageButton ID="imgbtnCancel" runat="server" ImageUrl="~/images/icons/delete.gif" CommandName="Cancel" />
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/icons/edit.png" CommandName="Edit" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

        </div>
    </div>
</asp:Content>

