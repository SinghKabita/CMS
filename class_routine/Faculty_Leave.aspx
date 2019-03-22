<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Faculty_Leave.aspx.cs" Inherits="class_routine_Faculty_Leave" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <table class="gridtable">
            <tr>
                <td>Faculty</td>
                <td>
                    <asp:DropDownList ID="ddlFaculty" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" AutoPostBack="true" runat="server">
                    </asp:DropDownList>
                </td>
                <td>Level</td>
                <td>
                    <asp:DropDownList ID="ddlLevel" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged" AutoPostBack="true" runat="server">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Program</td>
                <td>
                    <asp:DropDownList ID="ddlProgram" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" AutoPostBack="true" runat="server">
                    </asp:DropDownList>
                </td>
                <td>Faculty Member</td>
                <td>
                    <asp:DropDownList ID="ddlFacultyMember" Height="22px" AutoPostBack="True" Style="height: 22px; font-size: medium" runat="server" OnSelectedIndexChanged="ddlFacultyMember_SelectedIndexChanged"></asp:DropDownList>
                    <asp:Label ID="lblPKIDU" runat="server" Visible="False"></asp:Label>
                </td>
                <td></td>

            </tr>

            <tr>


                <td>Leave From Date</td>
                <td>
                    <asp:TextBox ID="txtLeaveFromDate" Style="height: 22px; width: 100%; font-size: medium" runat="server" AutoPostBack="True"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtLeaveFromDate"
                        Enabled="True" Format="dd.MMM.yyyy">
                    </cc1:CalendarExtender>
                </td>
                <td></td>
                <td>Leave To Date</td>
                <td>
                    <asp:TextBox ID="txtLeaveToDate" Style="height: 22px; font-size: medium" runat="server" AutoPostBack="True"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtLeaveToDate"
                        Enabled="True" Format="dd.MMM.yyyy">
                    </cc1:CalendarExtender>
                </td>

            </tr>
            <tr>
                <td>No of Period</td>
                <td>
                    <asp:TextBox ID="txtNoOfPeriods" runat="server" Height="22px"></asp:TextBox>
                </td>


            </tr>
            <tr>
                <td>Approved Date</td>
                <td>
                    <asp:TextBox ID="txtApprovedDate" Style="height: 22px; width: 100%; font-size: medium" runat="server"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtApprovedDate"
                        Enabled="True" Format="dd.MMM.yyyy">
                    </cc1:CalendarExtender>
                </td>
                <td></td>
                <td>Approved By</td>
                <td>
                    <asp:TextBox ID="txtApprovedBy" Style="height: 22px; width: 100%; font-size: medium" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>Description</td>
                <td colspan="4">
                    <asp:TextBox ID="txtDescription" Style="height: 65px; width: 100%; font-size: large" runat="server" TextMode="Multiline"></asp:TextBox>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Button ID="btnSave" runat="server" Style="height: 25px;" Text="Save" OnClick="btnSave_Click" />
                </td>
            </tr>

        </table>

        <table style="width: 100%">
            <tr>
                <td>
                    <asp:GridView runat="server" Width="100%" CssClass="gridtable" AutoGenerateColumns="False" EnableModelValidation="True" ID="gridFacultyLeave" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowCommand="gridFacultyLeave_RowCommand" OnRowDataBound="gridFacultyLeave_RowDataBound">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <ItemTemplate>
                                    <asp:Label ID="lblSN" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Employee Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblPKID" runat="server" Visible="false" Text='<%# Bind("PK_ID") %>'></asp:Label>

                                    <asp:Label ID="lblEmployeeId" runat="server" Text='<%# Bind("employee_id") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Employee Name">
                                <ItemTemplate>

                                    <asp:Label ID="lblEmployeeName" runat="server" Text=""></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Approved Date">
                                <ItemTemplate>

                                    <asp:Label ID="lblApprovedDate" runat="server" Text='<%# Bind("approve_date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Leave From Date">
                                <ItemTemplate>

                                    <asp:Label ID="lblLeaveFromDate" runat="server" Text='<%# Bind("leave_from_date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Leave To Date">
                                <ItemTemplate>

                                    <asp:Label ID="lblLeaveToDate" runat="server" Text='<%# Bind("leave_to_date") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="No of Period">
                                <ItemTemplate>

                                    <asp:Label ID="lblNoOfPeriod" runat="server" Text='<%# Bind("no_of_period") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Description">
                                <ItemTemplate>

                                    <asp:Label ID="lblDescription" runat="server" Text='<%# Bind("REMARKS") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Approved By">
                                <ItemTemplate>

                                    <asp:Label ID="lblApprovedBy" runat="server" Text='<%# Bind("approve_by") %>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgEdit" runat="server" CommandName="Change" ImageUrl="~/images/icons/edit.png" />
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
</asp:Content>

