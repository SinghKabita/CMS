<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="bookissue.aspx.cs" Inherits="library_library_bookissue" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <table class="gridtable">
            <tr>
                <td colspan="5">
                    <asp:TextBox ID="txtStdBarcode" OnTextChanged="txtStdBarcode_TextChanged" AutoPostBack="true" placeholder="..barcode Student ID" Width="300px" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblIssueToType" runat="server" Text="Issue To Type"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlIssueToType" runat="server" Width="128px" Height="22px" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlIssueToType_SelectedIndexChanged">
                        <asp:ListItem Selected="True">Student</asp:ListItem>
                        <%--<asp:ListItem>Teacher</asp:ListItem>--%>
                        <%--<asp:ListItem>Staff</asp:ListItem>--%>
                    </asp:DropDownList>
                </td>
                <td></td>
                <td></td>
                <td style="width: 79px"></td>
            </tr>
            <tr>
                <td>Program
                </td>
                <td>
                    <asp:DropDownList ID="ddlProgram" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>
                </td>
                <td style="width: 79px">&nbsp;</td>
            </tr>
            <tr>

                <td>
                    <%--<asp:Label ID="lblIssueTo" runat="server" Text="Issue To"></asp:Label>--%>
                    <asp:Label ID="lblBatch" runat="server" Text="Batch"></asp:Label>

                </td>
                <td>
                    <%-- <asp:DropDownList ID="ddlTeacher" runat="server" Width="128px" AutoPostBack="True" Height="22px"
                OnSelectedIndexChanged="ddlTeacher_SelectedIndexChanged" style="height: 22px">
            </asp:DropDownList>--%>
                    <asp:DropDownList ID="ddlBatch" runat="server" Width="128px" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblSemester" runat="server" Text="Semester"></asp:Label></td>
                <td>

                    <%--<asp:ListItem>Staff</asp:ListItem>--%>
                    <asp:DropDownList ID="ddlSemester" runat="server" Width="128px" Height="22px"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td style="width: 79px">&nbsp;
            <asp:Label ID="lblSection" runat="server" Text="Section"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlSection" runat="server" Width="128px" AutoPostBack="True" Height="22px"
                        OnSelectedIndexChanged="ddlSection_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblStudentName" runat="server" Text="Student ID"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlStudentName" runat="server" Height="22px" 
                        OnSelectedIndexChanged="ddlStudentName_SelectedIndexChanged" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td colspan="3">

                    <asp:TextBox ID="txtStudentName" runat="server" Height="22px" Visible="False" Font-Bold="True"
                        Width="299px"></asp:TextBox>

                </td>
            </tr>
        </table>
        <p>
            <asp:GridView ID="gridIssued" runat="server" CssClass="gridtable" AutoGenerateColumns="False"
                OnRowDataBound="gridIssued_RowDataBound" BackColor="White" BorderColor="#E7E7FF"
                BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal">
                <AlternatingRowStyle BackColor="#F7F7F7" />
                <Columns>
                    <asp:TemplateField HeaderText="SN.">
                        <ItemTemplate>
                            <asp:Label ID="Label1" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Issued Date">
                        <ItemTemplate>
                            <asp:Label ID="lblDate" runat="server"></asp:Label>
                            <asp:Label ID="lblMasterBookIssueId" runat="server" Text='<%# Bind("MASTERBOOKISSUEID") %>'
                                Visible="False"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Book Name">
                        <ItemTemplate>

                            <asp:Label ID="lblBookName" runat="server" Text=""></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Issuable">
                        <ItemTemplate>
                            <asp:Label Text="" ID="lblIssuable" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Issue Type">
                        <ItemTemplate>
                            <asp:Label Text="" ID="lblIssueType" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="NSBN">
                        <ItemTemplate>
                            <asp:Label ID="lblNSBN" runat="server" Text='<%# Bind("NSBN") %>'></asp:Label>

                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Remarks">
                        <ItemTemplate>
                            <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("REMARKS") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
            </asp:GridView>
        </p>
        <table>
            <tr>
                <td>&nbsp;
                </td>
                <td>
                    <asp:Button ID="btnIssue" runat="server" Text="Issue" Width="88px" OnClick="btnIssue_Click1" />
                </td>
                <td>&nbsp;
                </td>
                <td>
                    <asp:Button ID="btnReceive" runat="server" Text="Receive" Width="88px" OnClick="btnReceive_Click" />
                </td>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td>&nbsp;
                </td>
                <td>&nbsp;
                </td>
                <td>&nbsp;
                </td>
                <td>&nbsp;
                </td>
                <td>&nbsp;
                </td>
            </tr>
        </table>

        <div id="issue_div" runat="server" visible="false">
            <table cellpadding="10">
                <tr style="background-color: #8e3663; color: White;">
                    <td align="center">
                        <b>For Issuing Book</b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="gridtable">
                            <tr>
                                <td style="height: 26px">Issue Date
                                </td>
                                <td style="height: 26px">
                                    <asp:TextBox ID="txtEDate" runat="server" Width="250px" Height="22px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>NSBN</td>
                                <td>
                                    <asp:TextBox ID="txtNSBN" runat="server" Width="250px" Height="22px" AutoPostBack="True" OnTextChanged="txtNSBN_TextChanged">
                                    </asp:TextBox>
                                </td>
                            </tr>

                            <tr>
                                <td style="height: 26px">Remarks
                                </td>
                                <td style="height: 26px">
                                    <asp:TextBox ID="txtRemarks" runat="server" Width="250px" Height="22px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Issue By
                                </td>
                                <td>
                                    <asp:TextBox ID="txtIssueBy" runat="server" Width="250px" Height="22px"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="gridToIssue" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal"
                            Width="664px" OnRowDataBound="gridToIssue_RowDataBound">
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>
                                <asp:TemplateField HeaderText="SN.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSn" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Book Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBookId" runat="server" Text='<%# Bind("BOOKID") %>' Visible="False"></asp:Label>
                                        <asp:Label ID="lblBookName" runat="server" Text="Book Name"></asp:Label>
                                        <asp:Label ID="lblBookDetailId" runat="server" Text='<%# Bind("BOOKDETAILID") %>'
                                            Visible="False"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NSBN">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNSBN" runat="server" Text='<%# Bind("NSBN") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Status">
                                    <ItemTemplate>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("STATUS") %>' Visible="False"></asp:Label>
                                        <asp:Label ID="lblStat" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issuable">
                                    <ItemTemplate>
                                        <asp:Label Text="" ID="lblIssuable" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issue Type">
                                    <ItemTemplate>
                                        <asp:Label Text="" ID="lblIssueType" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Remarks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("REMARKS") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>

                            </Columns>
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        </asp:GridView>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnIssue1" runat="server" Text="Issue" Width="88px" OnClick="btnIssue1_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" Width="88px" OnClick="btnCancel_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>


        <div id="receive_div" runat="server" visible="false">
            <table cellpadding="10">
                <tr style="background-color: #8e3663; color: White;">
                    <td align="center">
                        <b>For Receiving Book</b>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table>
                            <tr>
                                <td>Receive Date
                                </td>
                                <td>
                                    <asp:TextBox ID="txtRDate" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Receive By
                                </td>
                                <td>
                                    <asp:TextBox ID="txtReceiveBy" runat="server" Width="250px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                                <td>&nbsp;
                                </td>
                            </tr>
                        </table>
                        <asp:GridView ID="gridToReceive" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#E7E7FF" BorderStyle="None" BorderWidth="1px" CellPadding="3" GridLines="Horizontal"
                            Width="668px" OnRowDataBound="gridToReceive_RowDataBound">
                            <AlternatingRowStyle BackColor="#F7F7F7" />
                            <Columns>
                                <asp:TemplateField HeaderText="SN.">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSn" runat="server" Text='<%#Container.DataItemIndex+1 %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issued Date">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDate" runat="server" Text='<%# Bind("ISSUEDATE") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Book Name">
                                    <ItemTemplate>

                                        <asp:Label ID="lblBookName" runat="server" Text=""></asp:Label>
                                        <asp:Label ID="lblMasterBookIssueId" runat="server" Text='<%# Bind("MASTERBOOKISSUEID") %>'
                                            Visible="False"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NSBN">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNSBN" runat="server" Text='<%# Bind("NSBN") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Shelf/ Compart">
                                    <ItemTemplate>
                                        <asp:Label Text="" ID="lblShelf" runat="server" />/
                                    <asp:Label Text="" ID="lblCompart" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Receive">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkReceive" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
                            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
                            <PagerStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" HorizontalAlign="Right" />
                            <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
                        </asp:GridView>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnReceive1" runat="server" Text="Receive" Width="88px" OnClick="btnReceive1_Click" />
                                </td>
                                <td>
                                    <asp:Button ID="btnCancelReceive" runat="server" Text="Cancel" Width="88px" OnClick="btnCancelReceive_Click" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

