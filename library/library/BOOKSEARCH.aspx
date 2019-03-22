<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="BOOKSEARCH.aspx.cs" Inherits="library_library_BOOKSEARCH" %>


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
                    </div>
                    <div class="row">
                        <asp:GridView ID="gridBookDetail" CssClass="gridtable" AutoGenerateColumns="false" runat="server"
                            OnRowDataBound="gridBookDetail_RowDataBound">
                            <Columns>
                                <asp:TemplateField HeaderText="SN">
                                    <ItemTemplate>
                                        <asp:label text='<%# Container.DataItemIndex+1 %>' ID="lblSN" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NSBN">
                                    <ItemTemplate>
                                        <asp:label text='<%# Bind("NSBN") %>' ID="lblNSBN" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issuable">
                                    <ItemTemplate>
                                        <asp:label text='' ID="lblIssuable" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:label text='' ID="lblStatus" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                <HeaderTemplate>
                                    Shelf/Compart
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label Text="" ID="lblShelf" runat="server" />/
                                    <asp:Label Text="" ID="lblCompart" runat="server" />
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

