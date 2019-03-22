<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Barcode_Generate.aspx.cs" Inherits="library_masterdata_Barcode_Generate" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">


        function printPartOfPage() {
            var printContent = document.getElementById('print_div');
            var windowUrl = 'about:blank';
            var uniqueName = new Date();
            var windowName = 'Print' + uniqueName.getTime();
            var printWindow = window.open(windowUrl, windowName, 'left=0,top=0,width=0,height=0');

            printWindow.document.write(printContent.innerHTML);
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();
            printWindow.close();
        }
    </script>
    <div class="container">
        <table class="gridtable">
            <tr>
                <td>Book Type</td>
                <td>
                    <asp:DropDownList ID="ddlBookType" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlBookType_SelectedIndexChanged"></asp:DropDownList>
                </td>

            </tr>
            <tr>
                <td>Book Name</td>
                <td>
                    <asp:DropDownList ID="ddlBookName" runat="server" Height="22px"></asp:DropDownList>
                </td>

            </tr>
            <tr>
                <td>From Number</td>
                <td>
                    <asp:TextBox ID="txtFromNumber" runat="server" Height="22px"></asp:TextBox>
                </td>

                <td>To Number</td>
                <td>
                    <asp:TextBox ID="txtToNumber" runat="server" Height="22px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnGenerate" runat="server" Text="Generate" OnClick="btnGenerate_Click" /></td>
                <td>
                    <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" /></td>
            </tr>
        </table>

        <table>
            <tr>
                <td>
                    <asp:GridView ID="gridBarcode" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Visible="False">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>

                                    <asp:Label ID="lblNSBN" runat="server" Text='<%# Bind("NSBN") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <div id="print_div">
            <div style="width: 21cm; height: 27.9cm;">

                <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Width="100%" OnRowDataBound="gridView_RowDataBound" ShowHeader="False">
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>

                                <br />

                                <asp:Label ID="lblHeader1" runat="server" Style="font-size: 10pt; font-weight: bold" Text="NSBN-NCCS-PK"></asp:Label>
                                <asp:Image ID="img1" runat="server" />
                                <asp:Label ID="lblNSBN1" runat="server" Text='<%# Bind("Col1") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <br />
                                <asp:Label ID="lblHeader2" runat="server" Style="font-size: 10pt; font-weight: bold" Text="NSBN-NCCS-PK"></asp:Label>
                                <asp:Image ID="img2" runat="server" />
                                <asp:Label ID="lblNSBN2" runat="server" Text='<%# Bind("Col2") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <br />
                                <asp:Label ID="lblHeader3" runat="server" Style="font-size: 10pt; font-weight: bold" Text="NSBN-NCCS-PK"></asp:Label>
                                <asp:Image ID="img3" runat="server" />
                                <asp:Label ID="lblNSBN3" runat="server" Text='<%# Bind("Col3") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <br />
                                <asp:Label ID="lblHeader4" runat="server" Style="font-size: 10pt; font-weight: bold" Text="NSBN-NCCS-PK"></asp:Label>
                                <asp:Image ID="img4" runat="server" />
                                <asp:Label ID="lblNSBN4" runat="server" Text='<%# Bind("Col4") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <br />
                                <asp:Label ID="lblHeader5" runat="server" Style="font-size: 10pt; font-weight: bold" Text="NSBN-NCCS-PK"></asp:Label>
                                <asp:Image ID="img5" runat="server" />
                                <asp:Label ID="lblNSBN5" runat="server" Text='<%# Bind("Col5") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>



                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

