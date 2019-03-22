<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Attendance_individual.aspx.cs" Inherits="attendance_reports_Attendance_individual" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:RadioButtonList runat="server" CssClass="chooseDate" ID="rbtnChooseDate" AutoPostBack="true"
        OnSelectedIndexChanged="rbtnChooseDate_SelectedIndexChanged"
        RepeatDirection="Horizontal" RepeatLayout="Flow">
        <asp:ListItem Value="nepDate" Text="Nepali Date" Selected />
        <asp:ListItem Value="engDate" Text="English Date" />
    </asp:RadioButtonList>
    <div class="container">
        <table class="gridtable">
            <tr>
                <td>View by
                </td>
                <td>
                    <asp:DropDownList ID="ddlViewBy" runat="server" Height="22px"
                        OnSelectedIndexChanged="dlViewBy_SelectedIndexChanged" AutoPostBack="true">
                        <asp:ListItem Value="0" Selected="True">Register No</asp:ListItem>
                        <asp:ListItem Value="1">Name</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td></td>
                <td></td>
            </tr>
            <tr>
                <td>Faculty
                </td>
                <td>
                    <asp:DropDownList ID="ddlFaculty" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"></asp:DropDownList>
                </td>

                <td>Level
                </td>
                <td>
                    <asp:DropDownList ID="ddlLevel" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged"></asp:DropDownList>
                </td>

                <td>Program
                </td>
                <td>

                    <asp:DropDownList ID="ddlProgram" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Semester</td>
                <td>

                    <asp:DropDownList ID="ddlSemester" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>

                <td runat="server" visible="false">Batch</td>
                <td runat="server" visible="false">
                    <asp:DropDownList ID="ddlBatch" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>

            </tr>

            <tr>
                <td>&nbsp;
                </td>
                <td colspan="3">
                    <asp:Label ID="lblCode" runat="server" Height="22px"></asp:Label>
                    <asp:TextBox ID="txtRegNo" runat="server" OnTextChanged="txtRegNo_TextChanged" AutoPostBack="true" Height="22px"></asp:TextBox>
                    <asp:TextBox ID="txtName" runat="server" Width="400px" Visible="false"
                        OnTextChanged="txtName_TextChanged" AutoPostBack="true"></asp:TextBox>

                    <asp:DropDownList runat="server" ID="ddlStudentName" OnSelectedIndexChanged="ddlStudentName_SelectedIndexChanged" AutoPostBack="True"
                        Visible="false">
                    </asp:DropDownList>

                    <%--<cc1:AutoCompleteExtender ID="txtName_AutoCompleteExtender" runat="server" UseContextKey="true" 
                Enabled="True" ServicePath="~/WebService.asmx" ServiceMethod="GetStudent" TargetControlID="txtName"
                MinimumPrefixLength="1" CompletionSetCount="1">
            </cc1:AutoCompleteExtender>--%>

                    <cc1:AutoCompleteExtender ID="txtName_AutoCompleteExtender"
                        runat="server"
                        ServicePath="~/WebService.asmx"
                        ServiceMethod="GetStudent"
                        TargetControlID="txtName"
                        MinimumPrefixLength="1"
                        CompletionSetCount="0"
                        UseContextKey="true"
                        EnableCaching="true">
                    </cc1:AutoCompleteExtender>

                </td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <div id="detail" runat="server" visible="false">
            <table class="gridtable">
                <tr>
                    <td>Reg No
                    </td>
                    <td style="width: 140px">
                        <asp:Label ID="lblRegNo" runat="server" Style="font-weight: 700"></asp:Label>
                    </td>
                    <td rowspan="6" style="width: 45px">&nbsp;
                &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    </td>
                    <td rowspan="6" valign="top">&nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
                &nbsp;
                <asp:Image ID="imgStudent" runat="server" Width="80px" />
                    </td>
                </tr>
                <tr>
                    <td>Name
                    </td>
                    <td style="width: 140px">
                        <asp:Label ID="lblName" runat="server" Style="font-weight: 700"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Grade
                    </td>
                    <td style="width: 140px">
                        <asp:Label ID="lblGrage" runat="server"></asp:Label>
                        <asp:Label ID="lblSection" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Phone
                    </td>
                    <td style="width: 140px">
                        <asp:Label ID="lblPhone" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Guardian
                    </td>
                    <td style="width: 140px">
                        <asp:Label ID="lblGuardian" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>Guardian Phone&nbsp;&nbsp;
                    </td>
                    <td style="width: 140px">
                        <asp:Label ID="lblGuardianNo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td style="width: 140px">&nbsp;
                    </td>
                    <td style="width: 45px">&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                </tr>
                <tr>
                    <td colspan="4" style="font-size: x-large">

                        <b>Attendance</b></td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td style="text-align: left">
                        <asp:Label ID="lblSub1" runat="server" Style="font-weight: 700; font-size: large"></asp:Label>

                        <asp:GridView ID="gridSub1" runat="server" CssClass="gridtable" Width="100%"
                            AutoGenerateColumns="False" OnRowDataBound="grdView1_RowDataBound"
                            BackColor="White">
                            <AlternatingRowStyle BackColor="#FFCCFF" />
                            <Columns>
                                <%-- <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Year">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" Visible="true" runat="server" Text='<%# Bind("YEAR") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth" Visible="false" runat="server" Text='<%# Bind("monthid") %>'></asp:Label>
                                        <asp:Label Text="" ID="lblMonthName" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="01">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday1" runat="server" Text='<%# Bind("day1") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="02">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday2" runat="server" Text='<%# Bind("day2") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="03">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday3" runat="server" Text='<%# Bind("day3") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="04">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday4" runat="server" Text='<%# Bind("day4") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="05">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday5" runat="server" Text='<%# Bind("day5") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="06">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday6" runat="server" Text='<%# Bind("day6") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="07">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday7" runat="server" Text='<%# Bind("day7") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="08">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday8" runat="server" Text='<%# Bind("day8") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="09">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday9" runat="server" Text='<%# Bind("day9") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="10">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday10" runat="server" Text='<%# Bind("day10") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="11">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday11" runat="server" Text='<%# Bind("day11") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="12">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday12" runat="server" Text='<%# Bind("day12") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="13">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday13" runat="server" Text='<%# Bind("day13") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="14">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday14" runat="server" Text='<%# Bind("day14") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="15">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday15" runat="server" Text='<%# Bind("day15") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="16">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday16" runat="server" Text='<%# Bind("day16") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="17">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday17" runat="server" Text='<%# Bind("day17") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="18">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday18" runat="server" Text='<%# Bind("day18") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="19">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday19" runat="server" Text='<%# Bind("day19") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="20">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday20" runat="server" Text='<%# Bind("day20") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="21">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday21" runat="server" Text='<%# Bind("day21") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="22">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday22" runat="server" Text='<%# Bind("day22") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="23">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday23" runat="server" Text='<%# Bind("day23") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="24">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday24" runat="server" Text='<%# Bind("day24") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="25">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday25" runat="server" Text='<%# Bind("day25") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="26">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday26" runat="server" Text='<%# Bind("day26") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="27">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday27" runat="server" Text='<%# Bind("day27") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="28">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday28" runat="server" Text='<%# Bind("day28") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="29">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday29" runat="server" Text='<%# Bind("day29") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="30">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday30" runat="server" Text='<%# Bind("day30") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="31">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday31" runat="server" Text='<%# Bind("day31") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="32">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday32" runat="server" Text='<%# Bind("day32") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Present">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotalpresent" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Absent">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalAbsent" runat="server"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Leave">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalLeave" runat="server"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Days">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotaldays" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Present Percent">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPresentPercent" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Marks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMarks" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td style="text-align: left">

                        <asp:Label ID="lblSub2" runat="server" Style="font-weight: 700; font-size: large"></asp:Label>

                        <asp:GridView ID="gridSub2" runat="server" CssClass="gridtable" Width="100%" AutoGenerateColumns="False" OnRowDataBound="grdView2_RowDataBound"
                            BackColor="White">
                            <AlternatingRowStyle BackColor="#FFCCFF" />
                            <Columns>
                                <%-- <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Year">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" Visible="true" runat="server" Text='<%# Bind("YEAR") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth" Visible="false" runat="server" Text='<%# Bind("MONTHID") %>'></asp:Label>
                                        <asp:Label Text="" ID="lblMonthName" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="01">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday1" runat="server" Text='<%# Bind("day1") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="02">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday2" runat="server" Text='<%# Bind("day2") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="03">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday3" runat="server" Text='<%# Bind("day3") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="04">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday4" runat="server" Text='<%# Bind("day4") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="05">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday5" runat="server" Text='<%# Bind("day5") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="06">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday6" runat="server" Text='<%# Bind("day6") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="07">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday7" runat="server" Text='<%# Bind("day7") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="08">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday8" runat="server" Text='<%# Bind("day8") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="09">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday9" runat="server" Text='<%# Bind("day9") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="10">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday10" runat="server" Text='<%# Bind("day10") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="11">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday11" runat="server" Text='<%# Bind("day11") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="12">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday12" runat="server" Text='<%# Bind("day12") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="13">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday13" runat="server" Text='<%# Bind("day13") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="14">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday14" runat="server" Text='<%# Bind("day14") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="15">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday15" runat="server" Text='<%# Bind("day15") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="16">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday16" runat="server" Text='<%# Bind("day16") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="17">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday17" runat="server" Text='<%# Bind("day17") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="18">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday18" runat="server" Text='<%# Bind("day18") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="19">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday19" runat="server" Text='<%# Bind("day19") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="20">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday20" runat="server" Text='<%# Bind("day20") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="21">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday21" runat="server" Text='<%# Bind("day21") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="22">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday22" runat="server" Text='<%# Bind("day22") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="23">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday23" runat="server" Text='<%# Bind("day23") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="24">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday24" runat="server" Text='<%# Bind("day24") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="25">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday25" runat="server" Text='<%# Bind("day25") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="26">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday26" runat="server" Text='<%# Bind("day26") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="27">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday27" runat="server" Text='<%# Bind("day27") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="28">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday28" runat="server" Text='<%# Bind("day28") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="29">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday29" runat="server" Text='<%# Bind("day29") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="30">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday30" runat="server" Text='<%# Bind("day30") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="31">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday31" runat="server" Text='<%# Bind("day31") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="32">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday32" runat="server" Text='<%# Bind("day32") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Present">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotalpresent" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Absent">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalAbsent" runat="server"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Leave">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalLeave" runat="server"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Days">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotaldays" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Present Percent">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPresentPercent" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Marks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMarks" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td style="text-align: left">

                        <asp:Label ID="lblSub3" runat="server" Style="font-weight: 700; font-size: large"></asp:Label>

                        <asp:GridView ID="gridSub3" runat="server" CssClass="gridtable" Width="100%" AutoGenerateColumns="False" OnRowDataBound="grdView3_RowDataBound"
                            BackColor="White">
                            <AlternatingRowStyle BackColor="#FFCCFF" />
                            <Columns>
                                <%--<asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Year">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" Visible="true" runat="server" Text='<%# Bind("YEAR") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth" Visible="false" runat="server" Text='<%# Bind("MONTHID") %>'></asp:Label>
                                        <asp:Label Text="" ID="lblMonthName" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="01">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday1" runat="server" Text='<%# Bind("day1") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="02">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday2" runat="server" Text='<%# Bind("day2") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="03">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday3" runat="server" Text='<%# Bind("day3") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="04">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday4" runat="server" Text='<%# Bind("day4") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="05">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday5" runat="server" Text='<%# Bind("day5") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="06">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday6" runat="server" Text='<%# Bind("day6") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="07">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday7" runat="server" Text='<%# Bind("day7") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="08">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday8" runat="server" Text='<%# Bind("day8") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="09">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday9" runat="server" Text='<%# Bind("day9") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="10">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday10" runat="server" Text='<%# Bind("day10") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="11">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday11" runat="server" Text='<%# Bind("day11") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="12">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday12" runat="server" Text='<%# Bind("day12") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="13">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday13" runat="server" Text='<%# Bind("day13") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="14">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday14" runat="server" Text='<%# Bind("day14") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="15">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday15" runat="server" Text='<%# Bind("day15") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="16">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday16" runat="server" Text='<%# Bind("day16") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="17">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday17" runat="server" Text='<%# Bind("day17") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="18">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday18" runat="server" Text='<%# Bind("day18") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="19">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday19" runat="server" Text='<%# Bind("day19") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="20">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday20" runat="server" Text='<%# Bind("day20") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="21">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday21" runat="server" Text='<%# Bind("day21") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="22">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday22" runat="server" Text='<%# Bind("day22") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="23">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday23" runat="server" Text='<%# Bind("day23") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="24">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday24" runat="server" Text='<%# Bind("day24") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="25">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday25" runat="server" Text='<%# Bind("day25") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="26">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday26" runat="server" Text='<%# Bind("day26") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="27">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday27" runat="server" Text='<%# Bind("day27") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="28">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday28" runat="server" Text='<%# Bind("day28") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="29">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday29" runat="server" Text='<%# Bind("day29") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="30">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday30" runat="server" Text='<%# Bind("day30") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="31">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday31" runat="server" Text='<%# Bind("day31") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="32">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday32" runat="server" Text='<%# Bind("day32") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Present">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotalpresent" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Absent">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalAbsent" runat="server"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Leave">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalLeave" runat="server"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Days">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotaldays" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Present Percent">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPresentPercent" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Marks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMarks" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td style="text-align: left">

                        <asp:Label ID="lblSub4" runat="server" Style="font-weight: 700; font-size: large"></asp:Label>

                        <asp:GridView ID="gridSub4" runat="server" CssClass="gridtable" Width="100%" AutoGenerateColumns="False" OnRowDataBound="grdView4_RowDataBound"
                            BackColor="White">
                            <AlternatingRowStyle BackColor="#FFCCFF" />
                            <Columns>
                                <%--  <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Year">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" Visible="true" runat="server" Text='<%# Bind("YEAR") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth" Visible="false" runat="server" Text='<%# Bind("MONTHID") %>'></asp:Label>
                                        <asp:Label Text="" ID="lblMonthName" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="01">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday1" runat="server" Text='<%# Bind("day1") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="02">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday2" runat="server" Text='<%# Bind("day2") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="03">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday3" runat="server" Text='<%# Bind("day3") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="04">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday4" runat="server" Text='<%# Bind("day4") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="05">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday5" runat="server" Text='<%# Bind("day5") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="06">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday6" runat="server" Text='<%# Bind("day6") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="07">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday7" runat="server" Text='<%# Bind("day7") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="08">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday8" runat="server" Text='<%# Bind("day8") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="09">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday9" runat="server" Text='<%# Bind("day9") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="10">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday10" runat="server" Text='<%# Bind("day10") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="11">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday11" runat="server" Text='<%# Bind("day11") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="12">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday12" runat="server" Text='<%# Bind("day12") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="13">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday13" runat="server" Text='<%# Bind("day13") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="14">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday14" runat="server" Text='<%# Bind("day14") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="15">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday15" runat="server" Text='<%# Bind("day15") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="16">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday16" runat="server" Text='<%# Bind("day16") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="17">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday17" runat="server" Text='<%# Bind("day17") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="18">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday18" runat="server" Text='<%# Bind("day18") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="19">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday19" runat="server" Text='<%# Bind("day19") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="20">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday20" runat="server" Text='<%# Bind("day20") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="21">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday21" runat="server" Text='<%# Bind("day21") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="22">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday22" runat="server" Text='<%# Bind("day22") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="23">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday23" runat="server" Text='<%# Bind("day23") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="24">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday24" runat="server" Text='<%# Bind("day24") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="25">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday25" runat="server" Text='<%# Bind("day25") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="26">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday26" runat="server" Text='<%# Bind("day26") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="27">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday27" runat="server" Text='<%# Bind("day27") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="28">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday28" runat="server" Text='<%# Bind("day28") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="29">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday29" runat="server" Text='<%# Bind("day29") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="30">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday30" runat="server" Text='<%# Bind("day30") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="31">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday31" runat="server" Text='<%# Bind("day31") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="32">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday32" runat="server" Text='<%# Bind("day32") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Present">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotalpresent" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Absent">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalAbsent" runat="server"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Leave">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalLeave" runat="server"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Days">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotaldays" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Present Percent">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPresentPercent" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Marks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMarks" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: left">

                        <asp:Label ID="lblSub5" runat="server" Style="font-weight: 700; font-size: large"></asp:Label>

                        <asp:GridView ID="gridSub5" runat="server" CssClass="gridtable" Width="100%" AutoGenerateColumns="False" OnRowDataBound="grdView5_RowDataBound"
                            BackColor="White">
                            <AlternatingRowStyle BackColor="#FFCCFF" />
                            <Columns>
                                <%-- <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Year">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" Visible="true" runat="server" Text='<%# Bind("YEAR") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth" Visible="false" runat="server" Text='<%# Bind("MONTHID") %>'></asp:Label>
                                        <asp:Label Text="" ID="lblMonthName" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="01">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday1" runat="server" Text='<%# Bind("day1") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="02">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday2" runat="server" Text='<%# Bind("day2") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="03">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday3" runat="server" Text='<%# Bind("day3") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="04">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday4" runat="server" Text='<%# Bind("day4") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="05">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday5" runat="server" Text='<%# Bind("day5") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="06">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday6" runat="server" Text='<%# Bind("day6") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="07">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday7" runat="server" Text='<%# Bind("day7") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="08">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday8" runat="server" Text='<%# Bind("day8") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="09">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday9" runat="server" Text='<%# Bind("day9") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="10">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday10" runat="server" Text='<%# Bind("day10") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="11">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday11" runat="server" Text='<%# Bind("day11") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="12">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday12" runat="server" Text='<%# Bind("day12") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="13">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday13" runat="server" Text='<%# Bind("day13") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="14">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday14" runat="server" Text='<%# Bind("day14") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="15">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday15" runat="server" Text='<%# Bind("day15") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="16">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday16" runat="server" Text='<%# Bind("day16") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="17">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday17" runat="server" Text='<%# Bind("day17") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="18">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday18" runat="server" Text='<%# Bind("day18") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="19">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday19" runat="server" Text='<%# Bind("day19") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="20">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday20" runat="server" Text='<%# Bind("day20") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="21">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday21" runat="server" Text='<%# Bind("day21") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="22">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday22" runat="server" Text='<%# Bind("day22") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="23">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday23" runat="server" Text='<%# Bind("day23") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="24">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday24" runat="server" Text='<%# Bind("day24") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="25">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday25" runat="server" Text='<%# Bind("day25") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="26">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday26" runat="server" Text='<%# Bind("day26") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="27">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday27" runat="server" Text='<%# Bind("day27") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="28">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday28" runat="server" Text='<%# Bind("day28") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="29">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday29" runat="server" Text='<%# Bind("day29") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="30">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday30" runat="server" Text='<%# Bind("day30") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="31">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday31" runat="server" Text='<%# Bind("day31") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="32">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday32" runat="server" Text='<%# Bind("day32") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Present">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotalpresent" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Absent">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalAbsent" runat="server"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Leave">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalLeave" runat="server"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Days">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotaldays" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Present Percent">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPresentPercent" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Marks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMarks" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td style="text-align: left">

                        <asp:Label ID="lblSub6" runat="server" Style="font-weight: 700; font-size: large"></asp:Label>

                        <asp:GridView ID="gridSub6" runat="server" CssClass="gridtable" Width="100%" AutoGenerateColumns="False" OnRowDataBound="grdView6_RowDataBound"
                            BackColor="White">
                            <AlternatingRowStyle BackColor="#FFCCFF" />
                            <Columns>
                                <%-- <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Year">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" Visible="true" runat="server" Text='<%# Bind("YEAR") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth" Visible="false" runat="server" Text='<%# Bind("MONTHID") %>'></asp:Label>
                                        <asp:Label Text="" ID="lblMonthName" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="01">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday1" runat="server" Text='<%# Bind("day1") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="02">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday2" runat="server" Text='<%# Bind("day2") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="03">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday3" runat="server" Text='<%# Bind("day3") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="04">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday4" runat="server" Text='<%# Bind("day4") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="05">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday5" runat="server" Text='<%# Bind("day5") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="06">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday6" runat="server" Text='<%# Bind("day6") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="07">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday7" runat="server" Text='<%# Bind("day7") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="08">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday8" runat="server" Text='<%# Bind("day8") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="09">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday9" runat="server" Text='<%# Bind("day9") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="10">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday10" runat="server" Text='<%# Bind("day10") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="11">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday11" runat="server" Text='<%# Bind("day11") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="12">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday12" runat="server" Text='<%# Bind("day12") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="13">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday13" runat="server" Text='<%# Bind("day13") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="14">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday14" runat="server" Text='<%# Bind("day14") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="15">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday15" runat="server" Text='<%# Bind("day15") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="16">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday16" runat="server" Text='<%# Bind("day16") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="17">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday17" runat="server" Text='<%# Bind("day17") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="18">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday18" runat="server" Text='<%# Bind("day18") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="19">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday19" runat="server" Text='<%# Bind("day19") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="20">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday20" runat="server" Text='<%# Bind("day20") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="21">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday21" runat="server" Text='<%# Bind("day21") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="22">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday22" runat="server" Text='<%# Bind("day22") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="23">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday23" runat="server" Text='<%# Bind("day23") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="24">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday24" runat="server" Text='<%# Bind("day24") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="25">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday25" runat="server" Text='<%# Bind("day25") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="26">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday26" runat="server" Text='<%# Bind("day26") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="27">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday27" runat="server" Text='<%# Bind("day27") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="28">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday28" runat="server" Text='<%# Bind("day28") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="29">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday29" runat="server" Text='<%# Bind("day29") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="30">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday30" runat="server" Text='<%# Bind("day30") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="31">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday31" runat="server" Text='<%# Bind("day31") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="32">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday32" runat="server" Text='<%# Bind("day32") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Present">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotalpresent" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Absent">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalAbsent" runat="server"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Leave">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalLeave" runat="server"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Days">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotaldays" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Present Percent">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPresentPercent" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Marks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMarks" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td style="text-align: left">

                        <asp:Label ID="lblSub7" runat="server" Style="font-weight: 700; font-size: large"></asp:Label>

                        <asp:GridView ID="gridSub7" runat="server" CssClass="gridtable" Width="100%" AutoGenerateColumns="False" OnRowDataBound="grdView7_RowDataBound"
                            BackColor="White">
                            <AlternatingRowStyle BackColor="#FFCCFF" />
                            <Columns>
                                <%-- <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                <asp:TemplateField HeaderText="Year">
                                    <ItemTemplate>
                                        <asp:Label ID="lblYear" Visible="true" runat="server" Text='<%# Bind("YEAR") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Month">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMonth" Visible="false" runat="server" Text='<%# Bind("MONTHID") %>'></asp:Label>
                                        <asp:Label Text="" ID="lblMonthName" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="01">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday1" runat="server" Text='<%# Bind("day1") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="02">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday2" runat="server" Text='<%# Bind("day2") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="03">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday3" runat="server" Text='<%# Bind("day3") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="04">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday4" runat="server" Text='<%# Bind("day4") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="05">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday5" runat="server" Text='<%# Bind("day5") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="06">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday6" runat="server" Text='<%# Bind("day6") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="07">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday7" runat="server" Text='<%# Bind("day7") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="08">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday8" runat="server" Text='<%# Bind("day8") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="09">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday9" runat="server" Text='<%# Bind("day9") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="10">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday10" runat="server" Text='<%# Bind("day10") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="11">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday11" runat="server" Text='<%# Bind("day11") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="12">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday12" runat="server" Text='<%# Bind("day12") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="13">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday13" runat="server" Text='<%# Bind("day13") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="14">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday14" runat="server" Text='<%# Bind("day14") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="15">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday15" runat="server" Text='<%# Bind("day15") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="16">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday16" runat="server" Text='<%# Bind("day16") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="17">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday17" runat="server" Text='<%# Bind("day17") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="18">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday18" runat="server" Text='<%# Bind("day18") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="19">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday19" runat="server" Text='<%# Bind("day19") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="20">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday20" runat="server" Text='<%# Bind("day20") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="21">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday21" runat="server" Text='<%# Bind("day21") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="22">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday22" runat="server" Text='<%# Bind("day22") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="23">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday23" runat="server" Text='<%# Bind("day23") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="24">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday24" runat="server" Text='<%# Bind("day24") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="25">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday25" runat="server" Text='<%# Bind("day25") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="26">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday26" runat="server" Text='<%# Bind("day26") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="27">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday27" runat="server" Text='<%# Bind("day27") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="28">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday28" runat="server" Text='<%# Bind("day28") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="29">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday29" runat="server" Text='<%# Bind("day29") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="30">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday30" runat="server" Text='<%# Bind("day30") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="31">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday31" runat="server" Text='<%# Bind("day31") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="32">
                                    <ItemTemplate>
                                        <asp:Label ID="lblday32" runat="server" Text='<%# Bind("day32") %>'></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Present">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotalpresent" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Absent">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalAbsent" runat="server"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Leave">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalLeave" runat="server"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Days">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotaldays" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Present Percent">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPresentPercent" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Marks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMarks" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>

                <tr>
                    <td></td>
                </tr>

                <tr>
                    <td style="text-align: left">

                        <asp:Label ID="lblAggregate" runat="server" Style="font-weight: 700; font-size: large" Text="Aggregate Attendance"></asp:Label>

                        <asp:GridView ID="gridAggregate" runat="server" CssClass="gridtable" AutoGenerateColumns="False" OnRowDataBound="grdView_RowDataBound"
                            BackColor="White" EnableModelValidation="True">
                            <AlternatingRowStyle BackColor="#FFCCFF" />
                            <Columns>

                                <asp:TemplateField HeaderText="Total Present">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotalpresent_Agg" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Absent">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalAbsent_Agg" runat="server"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Leave">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTotalLeave_Agg" runat="server"></asp:Label>

                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField HeaderText="Total Class">
                                    <ItemTemplate>
                                        <asp:Label ID="lbltotalclass_Agg" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Present Percent">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPresentPercent_Agg" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Marks">
                                    <ItemTemplate>
                                        <asp:Label ID="lblMarks_Agg" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

