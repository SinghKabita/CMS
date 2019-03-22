<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BOOKSHELF.aspx.cs" Inherits="library_masterdata_BOOKSHELF" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row">
            <div class="col-md-6 panel shadow no-border-radius bg-white panel-info">
                <div class="panel-body">
                    <div class="row">
                        Shelf No. &nbsp;
                        <asp:TextBox ID="txtShelf" runat="server" />
                        &nbsp; 
                        <asp:Button Text="Add" ID="btnAdd" OnClick="btnAdd_Click" CssClass="btn btn-default input-sm" runat="server" />
                          
                    </div>
                    <div class="row">
                        <asp:GridView ID="gridShelf" AutoGenerateColumns="false" CssClass="gridtable" Width="500px" runat="server"
                            OnRowEditing="gridShelf_RowEditing"
                            OnRowDataBound="gridShelf_RowDataBound"
                            OnRowCancelingEdit="gridShelf_RowCancelingEdit"
                            OnRowUpdating="gridShelf_RowUpdating">
                            <HeaderStyle BackColor="Silver" />
                            <AlternatingRowStyle BackColor="#ccffff" />
                            <Columns>
                                <asp:TemplateField HeaderText="SNo.">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# Container.DataItemIndex+1 %>' ID="lblSno" runat="server" />
                                        <asp:Label Text='<%# Bind("PK_ID") %>' ID="lblPK" Visible="false" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shelf">
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtShelfE" runat="server" />
                                        <asp:Label Text='<%# Bind("SHELFNO") %>' Visible="false" ID="lblShelfE" runat="server" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:Label Text='<%# Bind("SHELFNO") %>' ID="lblShelf" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <EditItemTemplate>
                                        <asp:ImageButton ID="imgbtnUpdate" CommandName="Update" ImageUrl="~/images/icons/upload.png" ToolTip="Update" runat="server" />
                                        <asp:ImageButton ID="imgbtnCancel" CommandName="Cancel" ImageUrl="~/images/icons/cancel.png" ToolTip="Cancel" runat="server" />
                                    </EditItemTemplate>
                                    <ItemTemplate>
                                        <asp:ImageButton CommandName="Edit" ID="imgbtnEdit"
                                            ToolTip="Edit" runat="server" ImageUrl="~/images/icons/edit.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </div>

                </div>
            </div>
        </div>
    </div>
</asp:Content>


