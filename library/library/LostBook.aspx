<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="LostBook.aspx.cs" Inherits="library_library_LostBook" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <script type="text/javascript">
            function Confirm() {
                var confirm_value = document.createElement("INPUT");
                confirm_value.type = "hidden";
                confirm_value.name = "confirm_value";
                if (confirm("Are You Sure this book has been lost?")) {
                    confirm_value.value = "Yes";
                } else {
                    confirm_value.value = "No";
                }
                document.forms[0].appendChild(confirm_value);
            }
        </script>

        <table class="gridtable">
            <tr>

                <td>Book Type</td>
                <td>
                    <asp:DropDownList ID="ddlBookType" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlBookType_SelectedIndexChanged"></asp:DropDownList>
                </td>

                <td></td>
                <td>Book Name</td>
                <td>
                    <asp:DropDownList ID="ddlBookName" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlBookName_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <asp:GridView ID="gridBookDetails" runat="server" CssClass="gridtable" AutoGenerateColumns="False" OnRowDataBound="gridBookDetails_RowDataBound" OnRowCancelingEdit="gridBookDetails_RowCancelingEdit" CellPadding="4" GridLines="None" AllowPaging="True" OnPageIndexChanging="gridBookDetails_PageIndexChanging" PageSize="20" Style="margin-top: 1px" EnableModelValidation="True" ForeColor="#333333">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:TemplateField HeaderText="SN.">
                    <ItemTemplate>
                        <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Book Type">

                    <HeaderTemplate>
                        Book Type<br />
                    </HeaderTemplate>
                    <ItemTemplate>

                        <asp:Label ID="lblBookType" runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Book Name">

                    <HeaderTemplate>
                        Book Name<br />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblBookId" runat="server" Text='<%# bind("bookid") %>' Visible="False"></asp:Label>
                        <asp:Label ID="lblBookDetailId" runat="server" Text='<%# bind("bookdetailid") %>' Visible="False"></asp:Label>
                        <asp:Label ID="lblBookName" runat="server" Text=""></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Book Number">

                    <HeaderTemplate>
                        Book Number<br />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblBookNumber" runat="server" Text='<%# bind("booknumber") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="NSBN">

                    <HeaderTemplate>
                        NSBN<br />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblNSBN" runat="server" Text='<%# bind("NSBN") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">

                    <HeaderTemplate>
                        Status<br />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblStatus" runat="server"></asp:Label>
                        <asp:Label ID="lblStat" runat="server" Text='<%# bind("status") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Remarks">

                    <HeaderTemplate>
                        Remarks<br />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblRemarks" runat="server" Text='<%# bind("remarks") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:Button ID="btnLost" runat="server" Text="Lost" OnClick="btnLost_Click" OnClientClick="Confirm();" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EditRowStyle BackColor="#999999" />
            <FooterStyle BackColor="#5D7B9D" ForeColor="White" Font-Bold="True" />
            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />

        </asp:GridView>
    </div>
</asp:Content>