<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="bookdetails.aspx.cs" Inherits="library_masterdata_bookdetails" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <table runat="server" visible="true" id="tblBookDetail" class="">
            <tr>
                <td>
                    <asp:Button ID="btnAddBook" runat="server" Text="Add Book Detail" Width="119px" OnClick="btnAddBook_Click" />
                </td>
                <td>
                    <asp:Label ID="lblBookName" runat="server" Text="Label" Visible="False"></asp:Label>
                </td>
                <td>
                    <asp:Label ID="lblBookNumber" runat="server" Text="Label" Visible="False"></asp:Label>
                </td>
                <td colspan="11"></td>
            </tr>

            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td>Book Type</td>
                <td>
                    <asp:DropDownList ID="ddlBookTypeA" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlBookTypeA_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td>&nbsp;</td>
                <td>Book Name</td>
                <td>
                    <asp:DropDownList ID="ddlBookNameA" runat="server" Height="22px"></asp:DropDownList>
                </td>
                <td>&nbsp;</td>
                <td>Shelf</td>
                <td>
                    <asp:DropDownList ID="ddlShelfS" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlShelfS_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td>&nbsp;</td>
                <td>Compart</td>
                <td>
                    <asp:DropDownList ID="ddlCompartS" runat="server" Height="22px"></asp:DropDownList>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <asp:Button Text="Search" ID="btnSearch" OnClick="btnSearch_Click" runat="server" />
                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>

            <tr>
                <td colspan="6">&nbsp;</td>
                <td>Shelf</td>
                <td>
                    <asp:DropDownList ID="ddlShelfM" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlShelfM_SelectedIndexChanged"></asp:DropDownList>

                </td>
                <td>&nbsp;</td>
                <td>Compart</td>
                <td>
                    <asp:DropDownList ID="ddlCompartM" runat="server" Height="22px"></asp:DropDownList>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
                <td>
                    <asp:Button Text="Move" ID="btnMove" OnClick="btnMove_Click" ValidationGroup="shelfmove" runat="server" />
                </td>
            </tr>

            <tr>
                <td colspan="7">&nbsp;</td>
                <td>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Select Shelf" ValidationGroup="shelfmove" InitialValue="select" ControlToValidate="ddlShelfM" runat="server" />

                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="gridBookDetails" runat="server" Width="100%" CssClass="gridtable" AutoGenerateColumns="False"
                        OnRowDataBound="gridBookDetails_RowDataBound"
                        OnRowCancelingEdit="gridBookDetails_RowCancelingEdit"
                        OnRowEditing="gridBookDetails_RowEditing"
                        OnRowUpdating="gridBookDetails_RowUpdating" BackColor="White" BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal" AllowPaging="True" OnPageIndexChanging="gridBookDetails_PageIndexChanging" PageSize="20" Style="margin-top: 1px">
                        <AlternatingRowStyle BackColor="#F7F7F7" />
                        <Columns>
                            <asp:TemplateField HeaderText="SN.">
                                <ItemTemplate>
                                    <asp:Label ID="Label1" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                    <asp:Label Text='<%# Bind("BOOKDETAILID") %>' Visible="false" ID="lblBDID" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Book Type">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlBookTypeE" runat="server" AutoPostBack="True" Height="22px" OnSelectedIndexChanged="ddlBookTypeE_SelectedIndexChanged">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:Label ID="lblBookTypeIdU" runat="server" Visible="False"></asp:Label>
                                    <asp:Label ID="lblBookDetailId" runat="server" Text='<%# Bind("bookdetailid") %>' Visible="False"></asp:Label>
                                </EditItemTemplate>
                                <HeaderTemplate>
                                    Book Type<br />
                                </HeaderTemplate>
                                <ItemTemplate>

                                    <asp:Label ID="lblBookType" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Book Name">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlBookNameE" runat="server" Height="22px">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:Label ID="lblBookIdU" runat="server" Text='<%# Bind("bookid") %>' Visible="False"></asp:Label>

                                </EditItemTemplate>
                                <HeaderTemplate>
                                    Book Name<br />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblBookId" runat="server" Text='<%# Bind("bookid") %>' Visible="False"></asp:Label>
                                    <asp:Label ID="lblBookName" runat="server" Text=""></asp:Label>
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

                            <asp:TemplateField ItemStyle-Width="130px" HeaderText="Book Number">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtBookNumberE" runat="server" Text='<%# Bind("booknumber") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderTemplate>
                                    Book Number<br />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblBookNo" runat="server" Text='<%# Bind("booknumber") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Status">
                                <EditItemTemplate>
                                    <asp:DropDownList ID="ddlStatusE" runat="server" Height="22px">
                                        <asp:ListItem Value="1">Available</asp:ListItem>
                                        <asp:ListItem Value="0">Unavailable</asp:ListItem>
                                    </asp:DropDownList>
                                    <br />
                                    <asp:Label ID="lblStatusE" runat="server" Visible="False" Text='<%# Bind("status") %>'></asp:Label>
                                </EditItemTemplate>
                                <HeaderTemplate>
                                    Status<br />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                    <asp:Label ID="lblStat" runat="server" Text='<%# Bind("status") %>' Visible="false"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRemarksE" runat="server" Text='<%# Bind("remarks") %>'></asp:TextBox>
                                </EditItemTemplate>
                                <HeaderTemplate>
                                    Remarks<br />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Issue Type">
                                <ItemTemplate>
                                    <asp:Label Text="" ID="lblIssuable" runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="NSBN">

                                <HeaderTemplate>
                                    NSBN<br />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblNSBN" runat="server" Text='<%# Bind("NSBN") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="btnEdit" runat="server" CommandName="update" ImageUrl="~/images/icons/upload.png" ToolTip="Update" />
                                    <asp:ImageButton ID="btnCancel" runat="server" CommandName="cancel" ImageUrl="~/images/icons/delete.gif" ToolTip="Cancel" />
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/images/icons/edit.png" CommandName="edit" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <asp:CheckBox Text="" ID="chkCheckH" Visible="true" OnCheckedChanged="chkCheckH_CheckedChanged" AutoPostBack="true" runat="server" />
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox Text="" ID="chkCheck" Visible="true" OnCheckedChanged="chkCheck_CheckedChanged" AutoPostBack="true" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>

                        </Columns>
                        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                        <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />

                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button Text="Update" Visible="false" ID="btnUpdate" OnClick="btnUpdate_Click" runat="server" />
                </td>
            </tr>
        </table>


        <div id="divEdit" runat="server" visible="false" class="PrioritymodalPopup">
            <h3>For Adding Book Details:</h3>
            <table>
                <tr>
                    <td class="auto-style17">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>

                <tr>
                    <td class="auto-style17">Book Type</td>
                    <td>
                        <asp:DropDownList ID="ddlBookType" runat="server" Width="200px" AutoPostBack="True" OnSelectedIndexChanged="ddlBookType_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style17">Book Name</td>
                    <td>
                        <asp:DropDownList ID="ddlBookName" runat="server" Width="350px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style17">Number of Books</td>
                    <td>
                        <asp:TextBox ID="txtBookNumber" runat="server" Width="200px"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="Required" ValidationGroup="bookno" ControlToValidate="txtBookNumber" runat="server" />
                    </td>
                </tr>

                <tr>
                    <td>Remarks</td>
                    <td>
                        <asp:TextBox ID="txtRemarks" runat="server" Width="200px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>Shelf No.</td>
                    <td>
                        <asp:DropDownList ID="ddlShelfNo" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlShelfNo_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>Compart No.
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCompartNo" runat="server" Height="22px"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        <asp:Button ID="btnAdd" runat="server" ValidationGroup="bookno" Text="Add" Width="88px" OnClick="btnAdd_Click1" />
                    </td>
                    <td class="style2">
                        <asp:Button ID="btnCancel" OnClick="btnCancel_Click"
                            runat="server" Text="Cancel" Width="88px" />
                    </td>

                </tr>
                <tr>
                    <td class="style2" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;
              
                    </td>
                </tr>

            </table>
        </div>
    </div>
</asp:Content>
