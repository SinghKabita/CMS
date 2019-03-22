<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="issued_notice.aspx.cs" Inherits="class_routine_issued_notice" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <div class="container">
        <table class="gridtable" style="text-align: left">
            <tr>
                <td style="height: 29px">Faculty</td>
                <td style="height: 29px">
                    <asp:DropDownList ID="ddlFaculty" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged">
                        </asp:DropDownList>
                </td>
                <td style="height: 29px">Level</td>
                <td style="height: 29px">
                    <asp:DropDownList ID="ddlLevel" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                        </asp:DropDownList>
                </td>
                    <td style="height: 29px">Program</td>
                    <td style="height: 29px">
                        <asp:DropDownList ID="ddlProgram" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>
                        
                    </td>
                    <td style="height: 29px">Semester</td>
                    <td style="height: 29px">
                        <asp:DropDownList runat="server" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" AutoPostBack="true" ID="ddlSemester" Height="22px" >
                    </asp:DropDownList>
                    </td>
            </tr>
            <tr runat="server" visible="false">
                <td>Batch</td>
                <td>
                    <asp:DropDownList ID="ddlBatch" runat="server" Height="22px"></asp:DropDownList></td>
            </tr>
        </table>
        <table style="text-align: left">

            <tr>
                <td style="height: 29px">From Date</td>
                <td style="height: 29px">
                    <asp:TextBox ID="txtFromDate" runat="server" Width="100%" Height="22px"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFromDate"
                        Enabled="True" Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                </td>
                <td style="height: 29px">To Date</td>
                <td style="height: 29px">
                    <asp:TextBox ID="txtToDate" runat="server" Width="100%" Height="22px"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtToDate"
                        Enabled="True" Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                </td>

                <td style="height: 29px">
                    <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" /></td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="ButtonAddMore" runat="server" Text="Add More" OnClick="btnAddMore_Click" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblPKIDU" runat="server" Visible="true"></asp:Label>
                </td>
            </tr>
        </table>

        <div id="divGrid" runat="server" visible="false">
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:GridView ID="gridNotices" runat="server" CssClass="gridtable" Width="100%" GridLines="None"
                            AutoGenerateColumns="False" EnableModelValidation="True" CellPadding="4" ForeColor="#333333"
                            OnRowCommand="gridNotices_RowCommand"
                            OnRowDataBound="gridNotices_RowDataBound">
                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                            <Columns>
                                <asp:TemplateField HeaderText="SN">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                        <asp:Label ID="lblPkid" runat="server" Text='<%# Bind("PK_ID") %>' Visible="False"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issue Date (AD)">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEngDate" runat="server" Text='<%# Bind("eng_date") %>'></asp:Label>
                                    </ItemTemplate>

                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issue Date (BS)">

                                    <ItemTemplate>
                                        <asp:Label ID="lblNepDate" runat="server" Text='<%# Bind("nep_date") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Heading">

                                    <ItemTemplate>
                                        <asp:Label ID="lblHeading" runat="server" Text='<%# Bind("notice_heading") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Issued By">

                                    <ItemTemplate>
                                        <asp:Label ID="lblIssuedBy" runat="server" Text='<%# Bind("notice_issued_by") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Description">

                                    <ItemTemplate>
                                        <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("description") %>'></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>

                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEdit" CommandName="change" runat="server" ToolTip="Edit" ImageUrl="~/images/icons/edit.png" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <EditRowStyle BackColor="#999999" />
                            <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                            <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                            <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divAddNotice" visible="false" runat="server" class="row">
            <div class="col-md-6 panel shadow no-border-radius bg-white panel-info">
                <div class="panel-heading">
                    Add Notice
                </div>
                <div class="panel-body">
                    <div class="row">
                        Issue Date:
                    <br />
                        <asp:TextBox ID="txtIssueDate" Width="150px" Height="22px" runat="server"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtIssueDate"
                            Enabled="True" Format="dd/MM/yyyy">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="CalendarExtender1" runat="server" />
                    </div>
                    <div class="row">
                        Heading:
                    <br />
                        <asp:TextBox ID="txtHeading" runat="server" Width="100%" Height="22px" Style="display: inline-block"></asp:TextBox>
                        <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="txtHeading" runat="server" />
                        Issued By:
                    <br />
                        <asp:TextBox ID="txtIssuedBy" runat="server" Width="100%" Height="22px" Style="display: inline-block"></asp:TextBox>
                        <asp:RequiredFieldValidator ErrorMessage="Required" ControlToValidate="txtIssuedBy" runat="server" />
                        Description:
                    <br />
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="100%" Height="45px" Style="display: inline-block"></asp:TextBox>

                    </div>

                    <div class="row">

                        <asp:Button ID="btnSave" runat="server" Width="100%" Text="Save" OnClick="btnSave_Click" />

                        <asp:Button ID="btnReset" runat="server" Width="100%" Text="Reset" CausesValidation="false" OnClick="btnReset_Click" />

                    </div>
                </div>
            </div>
        </div>
    </div>

    <script src="http://localhost/college/js/jquery.min.js"></script>

</asp:Content>
