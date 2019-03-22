<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ResultGenerate.aspx.cs" Inherits="exam_internalexam_ResultGenerate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="container">
    <table class="gridtable">
        <tr>
            <td>Level</td>
            <td>
                <asp:DropDownList ID="ddlLevel" runat="server" Height="22px" AutoPostBack="True" 
                    OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Faculty</td>
            <td>
                <asp:DropDownList ID="ddlFaculty" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Program</td>
            <td>
                <asp:DropDownList ID="ddlProgram" runat="server" Height="22px" AutoPostBack="True" 
                    OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td>Semester</td>
            <td>
                <asp:DropDownList ID="ddlSemester" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" runat="server" Height="22px" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>&nbsp;
            </td>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>Batch
            </td>
            <td>
                <asp:DropDownList ID="ddlBatch" runat="server" Height="22px" >
                </asp:DropDownList>
            </td>
            <td>&nbsp;
            </td>
            <td>&nbsp;
            </td>
        </tr>

        
        <tr>
            <td class="auto-style7">Exam Type</td>
            <td class="auto-style7">
                <asp:DropDownList ID="ddlExamType" runat="server" Height="22px">
                </asp:DropDownList>
            </td>
            <td class="auto-style8">&nbsp;</td>
            <%--<td class="auto-style8">Date (BS) </td>--%>
            <td class="auto-style8">
                <%--<asp:TextBox ID="txtDate" runat="server" Height="22px"></asp:TextBox>--%>
            </td>
        </tr>
        <tr>
            <td class="auto-style8">&nbsp;</td>
            <td class="auto-style9">
                <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" />
            </td>

        </tr>

    </table>




    <table class="auto-style1">
        <tr>
            <td class="auto-style3">

                <asp:GridView ID="gridResult" runat="server" AutoGenerateColumns="False" 
                    EnableModelValidation="True" 
                    OnRowDataBound="gridResult_RowDataBound" Width="742px" Visible="false">
                    <Columns>
                        <asp:TemplateField HeaderText="Sn">
                            <ItemTemplate>
                                <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student id">
                            <ItemTemplate>
                                <asp:Label ID="lblStudentId" runat="server" Text='<%# Bind("STUDENT_ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Semester">
                            <ItemTemplate>
                                <asp:Label ID="lblSemester" Visible="false" runat="server"></asp:Label>                          
                                <asp:Label ID="lblSemesterName" runat="server"></asp:Label>       
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Exam type">
                            <ItemTemplate>
                                <asp:Label ID="lblExamType" Visible="false" runat="server"></asp:Label>
                                <asp:Label ID="lblExamTypeName" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total">
                            <ItemTemplate>
                                <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("total") %>' Visible="False"></asp:Label>
                                <asp:Label ID="lblMKUPTotal" runat="server" Text='<%# Bind("totmkp") %>' Visible="False"></asp:Label>
                                <asp:Label ID="lblTot" runat="server" Text='<%# Bind("total") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Percentage">
                            <ItemTemplate>
                                <asp:Label ID="lblPercentage" runat="server" Text='<%# Bind("percent") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="lblRemarks" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Grade">
                            <ItemTemplate>
                                <asp:Label ID="lblGrade" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rank">
                            <ItemTemplate>
                                <asp:Label ID="lblRank" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                <br />
                <asp:GridView ID="gridTemp" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" Visible="False">
                    <Columns>
                        <asp:TemplateField HeaderText="sn">
                            <ItemTemplate>
                                <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="TOTAL">
                            <ItemTemplate>
                                <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("TOTAL") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                

                <asp:GridView ID="gridRank" runat="server" AutoGenerateColumns="False" 
                    EnableModelValidation="True" Width="742px" Visible="true"
                    OnRowDataBound="gridRank_RowDataBound">
                    <Columns>
                        <asp:TemplateField HeaderText="Sn">
                            <ItemTemplate>
                                <asp:Label ID="lblSn0" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student id">
                            <ItemTemplate>
                                <asp:Label ID="lblStuId" runat="server" Text='<%# Bind("STUDENTID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Semester">
                            <ItemTemplate>
                                <asp:Label ID="lblSemester" Visible="false" runat="server" Text='<%# Bind("CLASS") %>'></asp:Label>
                                <asp:Label ID="lblSemesterName" runat="server"></asp:Label> 
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Exam type">
                            <ItemTemplate>
                                <asp:Label ID="lblExtp" runat="server" Text='<%# Bind("EXAM_TYPE") %>' Visible="false"></asp:Label>
                                <asp:Label ID="lblExamType" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total">
                            <ItemTemplate>
                                <asp:Label ID="lblTTL" runat="server" Text='<%# Bind("Total") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Percentage">
                            <ItemTemplate>
                                <asp:Label ID="lblPercent" runat="server" Text='<%# Bind("PERCENTAGE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <ItemTemplate>
                                <asp:Label ID="lblRemark" runat="server" Text='<%# Bind("REMARKS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Grade">
                            <ItemTemplate>
                                <asp:Label ID="lblGrd" runat="server" Text='<%# Bind("GRADE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rank">
                            <ItemTemplate>
                                <asp:Label ID="lblRnk" runat="server" Text='<%# Bind("rank") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>


                <br />

                <asp:GridView ID="gridAttendance" Visible="false"
                    OnRowDataBound="gridAttendance_RowDataBound" AutoGenerateColumns="False" runat="server">
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField HeaderText="Present Days">
                            <ItemTemplate>
                                <asp:Label ID="lblPresentDays" runat="server" Text='<%# Bind("PRESENT_DAYS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Absent Days">
                            <ItemTemplate>
                                <asp:Label ID="lblAbsDays" runat="server" Text='<%# Bind("ABS_DAYS") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Days">
                            <ItemTemplate>
                                <asp:Label ID="lbltotalpresent" Text='<%# Bind("PRESENT_DAYS") %>' runat="server"></asp:Label>/
                                <asp:Label ID="lbltotaldays" runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>

                    <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click1" Text="Save" />
                
            </td>
        </tr>
    </table>
    </div>
</asp:Content>

