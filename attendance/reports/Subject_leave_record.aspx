<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Subject_leave_record.aspx.cs" Inherits="attendance_reports_Semester_Leave_Record" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div class="container">

        <table class="gridtable">
        <tr>
            <td>Faculty
            </td>
            <td>
                <asp:DropDownList ID="ddlFaculty" runat="server" Style="height: 22px; width: 75%;" AutoPostBack="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"></asp:DropDownList>
            </td>

            <td>Level
            </td>
            <td>
                <asp:DropDownList ID="ddlLevel" runat="server" Style="height: 22px; width: 75%;" AutoPostBack="True" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                </asp:DropDownList>
            </td>

            <td>Program
            </td>
            <td>

                <asp:DropDownList ID="ddlProgram" runat="server" Style="height: 22px; width: 100%;" AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>
            </td>
        </tr>


        <tr>
            <td>Semester</td>
            <td>
                <asp:DropDownList ID="ddlSemester" runat="server" Style="height: 22px; width: 75%;" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"></asp:DropDownList>
            </td>

            <td runat="server" visible="false">Batch</td>
            <td runat="server" visible="false">
                <asp:DropDownList ID="ddlBatch" runat="server" Style="height: 22px; width: 75%;" AutoPostBack="True"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>From Date</td>
            <td>
                <asp:TextBox ID="txtFromdate" runat="server" Style="height: 22px; width: 100%; font-size: medium" AutoPostBack="True"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFromdate"
                    Enabled="True" Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
            </td>

            <td>To Date</td>
            <td>
                <asp:TextBox ID="txtToDate" runat="server" Style="height: 22px; width: 100%; font-size: medium" AutoPostBack="True"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtToDate"
                    Enabled="True" Format="dd/MM/yyyy">
                </cc1:CalendarExtender>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" /></td>

        </tr>
    </table>

    <table style="width: 100%">
        <tr>
            <td>
                <asp:GridView ID="gridSemesterLeaveRecord" Width="100%" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"
                    OnRowDataBound="gridSemesterLeaveRecord_RowDataBound" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="SN">
                            <ItemTemplate>
                                <asp:Label ID="lblSN" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student Id">
                            <ItemTemplate>
                                <asp:Label ID="lblStudentId" runat="server" Text='<%# Bind("student_id") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <asp:Label ID="lblStudentName" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sub1">
                            <HeaderTemplate>
                                <asp:Label ID="lblSub1Name" runat="server"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSub1" runat="server" Text='<%# Bind("sub1") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sub2">
                            <HeaderTemplate>
                                <asp:Label ID="lblSub2Name" runat="server"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSub2" runat="server" Text='<%# Bind("sub2") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sub3">
                            <HeaderTemplate>
                                <asp:Label ID="lblSub3Name" runat="server"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSub3" runat="server" Text='<%# Bind("sub3") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sub4">
                            <HeaderTemplate>
                                <asp:Label ID="lblSub4Name" runat="server"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSub4" runat="server" Text='<%# Bind("sub4") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sub5">
                            <HeaderTemplate>
                                <asp:Label ID="lblSub5Name" runat="server"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSub5" runat="server" Text='<%# Bind("sub5") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Sub6">
                            <HeaderTemplate>
                                <asp:Label ID="lblSub6Name" runat="server"></asp:Label>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSub6" runat="server" Text='<%# Bind("sub6") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Leave">

                            <ItemTemplate>
                                <asp:Label ID="lblTotalLeave" runat="server" Text='<%# Bind("total_leave") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
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

