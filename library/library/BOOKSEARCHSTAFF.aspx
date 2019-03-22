<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BOOKSEARCHSTAFF.aspx.cs" Inherits="library_library_BOOKSEARCHSTAFF" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="CC1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row">
            <div class="col-md-8 panel shadow no-border-radius bg-white panel-info">
                <div class="panel-heading">
                    Search Book
                </div>
                <div class="panel-body">
                    <div class="row">

                        <asp:TextBox ID="txtBook" AutoPostBack="true" OnTextChanged="txtBook_TextChanged" placeholder="..book name here" Width="420px" runat="server" />
                        <CC1:AutoCompleteExtender ID="txtBook_AutoCompleteExtender" runat="server"
                            ServicePath="~/WebService.asmx" ServiceMethod="get_book_name"
                            TargetControlID="txtBook"
                            MinimumPrefixLength="1" CompletionSetCount="0">
                        </CC1:AutoCompleteExtender>
                        &nbsp;
                        <asp:DropDownList ID="ddlStatus" Height="26px" runat="server">
                            <asp:ListItem Value="Both" Text="Both" />
                            <asp:ListItem Value="1" Text="Available" />
                            <asp:ListItem Value="0" Text="Unavailable" />
                        </asp:DropDownList>
                        &nbsp;
                        <asp:Button Text="Search" ID="btnSearch" OnClick="btnSearch_Click" runat="server" />
                    </div>
                    <div class="row mt-10">
                        <asp:GridView ID="gridBookDetail" CssClass="gridtable" AutoGenerateColumns="false" runat="server"
                            OnRowDataBound="gridBookDetail_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="SN">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# Container.DataItemIndex+1 %>' ID="lblSN" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NSBN">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# Bind("NSBN") %>' ID="lblNSBN" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issuable">
                                    <ItemTemplate>
                                        <asp:Label Text='' ID="lblIssuable" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# Bind("STATUS") %>' ID="lblStatus" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Shelf/Compart
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label Text="" ID="lblShelf" runat="server" />/
                                    <asp:Label Text='<%# Bind("COMPARTID") %>' ID="lblCompartID" Visible="false" runat="server" />
                                         <asp:Label Text="" ID="lblCompartNo" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issued To">
                                    <ItemTemplate>
                                        <asp:Label Text="" ID="lblIssuedTo" runat="server" />
                                        <asp:Label Text="" ID="lblIssuedToName" runat="server" />
                                        <asp:Label Text="" ID="lblSemSec" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issued Date">
                                    <ItemTemplate>
                                        <asp:Label Text="" ID="lblIssuedDate" runat="server" />
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

