<%@ Control Language="C#" AutoEventWireup="true" CodeFile="result.ascx.cs" Inherits="uc_test_result" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style type="text/css">
    
    .auto-style3
    {
        width: 100%;
    }
   
    .auto-style4
    {
        height: 23px;
    }
   
    .auto-style7
    {
        text-align: left;
        height: 24px;
    }
    .auto-style8
    {
        height: 24px;
    }
    .auto-style9
    {
        text-align: left;
        height: 30px;
    }
   
    .auto-style10 {
        height: 26px;
    }
   
    </style>

<table class="auto-style1">
    <tr>
            <td>
                Batch
            </td>
            <td>
                <asp:DropDownList ID="ddlBatch" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
   
        <tr>
            <td>
                Semester</td>
            <td>
                <asp:DropDownList ID="ddlSemester" runat="server" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
    <tr>
        <td class="auto-style7">Exam Type</td>
        <td class="auto-style7">
            <asp:DropDownList ID="ddlExamType" runat="server">
            </asp:DropDownList>
        </td>
        <td class="auto-style8"> &nbsp;</td>
          <td class="auto-style8">Date (BS) </td>
          <td class="auto-style8">
              <asp:TextBox ID="txtDate" runat="server"></asp:TextBox>
             
        </td>
    </tr>
    <tr>
        <td class="auto-style8">&nbsp;</td>
        <td class="auto-style9">
            <asp:Button ID="btnShow" runat="server" Text="Show" OnClick="btnShow_Click" />
        </td>
         <td></td>
    </tr>

</table>




<table class="auto-style1">
    <tr>
        <td class="auto-style3">

<asp:GridView ID="gridResult" runat="server" AutoGenerateColumns="False" EnableModelValidation="True" OnRowDataBound="gridResult_RowDataBound" Width="742px" Visible="False">
    <Columns>
        <asp:TemplateField HeaderText="Sn">
            <ItemTemplate>
                <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Student id">
            <ItemTemplate>
                <asp:Label ID="lblStudentId" runat="server" Text='<%# bind("STUDENT_ID") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Class">
            <ItemTemplate>
                <asp:Label ID="lblClass" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Exam type">
            <ItemTemplate>
                <asp:Label ID="lblExamType" runat="server"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Total">
            <ItemTemplate>
                <asp:Label ID="lblTotal" runat="server" Text='<%# bind("total") %>' Visible="False"></asp:Label>
                <asp:Label ID="lblMKUPTotal" runat="server" Text='<%# bind("totmkp") %>' Visible="False"></asp:Label>
                <asp:Label ID="lblTot" runat="server" Text='<%# bind("total") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Percentage">
            <ItemTemplate>
                <asp:Label ID="lblPercentage" runat="server" Text='<%# bind("percent") %>'></asp:Label>
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
                            <asp:Label ID="lblTotal" runat="server" Text='<%# BIND("TOTAL") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>


<asp:GridView ID="gridRank" runat="server" AutoGenerateColumns="False" EnableModelValidation="True"  Width="742px" OnRowDataBound="gridRank_RowDataBound">
    <Columns>
        <asp:TemplateField HeaderText="Sn">
            <ItemTemplate>
                <asp:Label ID="lblSn0" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Student id">
            <ItemTemplate>
                <asp:Label ID="lblStuId" runat="server" Text='<%# bind("STUDENTID") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Class">
            <ItemTemplate>
                <asp:Label ID="lblClas" runat="server" Text='<%# bind("CLASS") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Exam type">
            <ItemTemplate>
                <asp:Label ID="lblExtp" runat="server" Text='<%# bind("EXAM_TYPE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Total">
            <ItemTemplate>
                <asp:Label ID="lblTTL" runat="server" Text='<%# bind("Total") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Percentage">
            <ItemTemplate>
                <asp:Label ID="lblPercent" runat="server" Text='<%# bind("PERCENTAGE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Remarks">
            <ItemTemplate>
                <asp:Label ID="lblRemark" runat="server" Text='<%# BIND("REMARKS") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Grade">
            <ItemTemplate>
                <asp:Label ID="lblGrd" runat="server" Text='<%# BIND("GRADE") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Rank">
            <ItemTemplate>
                <asp:Label ID="lblRnk" runat="server" Text='<%# bind("rank") %>'></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>


            <br />
                                    
     <asp:GridView ID="grdView" runat="server" CssClass="gridtable" AutoGenerateColumns="False" OnRowDataBound="grdView_RowDataBound"
                    BackColor="White" Width="100%" Visible="False" >
                    <Columns>
                        <asp:TemplateField HeaderText="No.">
                            <ItemTemplate>
                                <%#Container.DataItemIndex+1 %>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Month">
                            <ItemTemplate>
                                <asp:Label ID="lblMonth" runat="server" Text='<%# BIND("monthname") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="01">
                            <ItemTemplate>
                                <asp:Label ID="lblday1" runat="server" Text='<%# BIND("day1") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="02">
                            <ItemTemplate>
                                <asp:Label ID="lblday2" runat="server" Text='<%# BIND("day2") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="03">
                            <ItemTemplate>
                                <asp:Label ID="lblday3" runat="server" Text='<%# BIND("day3") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="04">
                            <ItemTemplate>
                                <asp:Label ID="lblday4" runat="server" Text='<%# BIND("day4") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="05">
                            <ItemTemplate>
                                <asp:Label ID="lblday5" runat="server" Text='<%# BIND("day5") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="06">
                            <ItemTemplate>
                                <asp:Label ID="lblday6" runat="server" Text='<%# BIND("day6") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="07">
                            <ItemTemplate>
                                <asp:Label ID="lblday7" runat="server" Text='<%# BIND("day7") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="08">
                            <ItemTemplate>
                                <asp:Label ID="lblday8" runat="server" Text='<%# BIND("day8") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="09">
                            <ItemTemplate>
                                <asp:Label ID="lblday9" runat="server" Text='<%# BIND("day9") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="10">
                            <ItemTemplate>
                                <asp:Label ID="lblday10" runat="server" Text='<%# BIND("day10") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="11">
                            <ItemTemplate>
                                <asp:Label ID="lblday11" runat="server" Text='<%# BIND("day11") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="12">
                            <ItemTemplate>
                                <asp:Label ID="lblday12" runat="server" Text='<%# BIND("day12") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="13">
                            <ItemTemplate>
                                <asp:Label ID="lblday13" runat="server" Text='<%# BIND("day13") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="14">
                            <ItemTemplate>
                                <asp:Label ID="lblday14" runat="server" Text='<%# BIND("day14") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="15">
                            <ItemTemplate>
                                <asp:Label ID="lblday15" runat="server" Text='<%# BIND("day15") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="16">
                            <ItemTemplate>
                                <asp:Label ID="lblday16" runat="server" Text='<%# BIND("day16") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="17">
                            <ItemTemplate>
                                <asp:Label ID="lblday17" runat="server" Text='<%# BIND("day17") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="18">
                            <ItemTemplate>
                                <asp:Label ID="lblday18" runat="server" Text='<%# BIND("day18") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="19">
                            <ItemTemplate>
                                <asp:Label ID="lblday19" runat="server" Text='<%# BIND("day19") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="20">
                            <ItemTemplate>
                                <asp:Label ID="lblday20" runat="server" Text='<%# BIND("day20") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="21">
                            <ItemTemplate>
                                <asp:Label ID="lblday21" runat="server" Text='<%# BIND("day21") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="22">
                            <ItemTemplate>
                                <asp:Label ID="lblday22" runat="server" Text='<%# BIND("day22") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="23">
                            <ItemTemplate>
                                <asp:Label ID="lblday23" runat="server" Text='<%# BIND("day23") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="24">
                            <ItemTemplate>
                                <asp:Label ID="lblday24" runat="server" Text='<%# BIND("day24") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="25">
                            <ItemTemplate>
                                <asp:Label ID="lblday25" runat="server" Text='<%# BIND("day25") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="26">
                            <ItemTemplate>
                                <asp:Label ID="lblday26" runat="server" Text='<%# BIND("day26") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="27">
                            <ItemTemplate>
                                <asp:Label ID="lblday27" runat="server" Text='<%# BIND("day27") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="28">
                            <ItemTemplate>
                                <asp:Label ID="lblday28" runat="server" Text='<%# BIND("day28") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="29">
                            <ItemTemplate>
                                <asp:Label ID="lblday29" runat="server" Text='<%# BIND("day29") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="30">
                            <ItemTemplate>
                                <asp:Label ID="lblday30" runat="server" Text='<%# BIND("day30") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="31">
                            <ItemTemplate>
                                <asp:Label ID="lblday31" runat="server" Text='<%# BIND("day31") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="32">
                            <ItemTemplate>
                                <asp:Label ID="lblday32" runat="server" Text='<%# BIND("day32") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total">
                            <ItemTemplate>
                                <asp:Label ID="lbltotalpresent" runat="server"></asp:Label>/<asp:Label ID="lbltotaldays"
                                    runat="server"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>


        </td>
        </tr>
</table>




<table class="auto-style3">
    <tr>
        <td class="auto-style4"></td>
    </tr>
    <tr>
        <td>
            &nbsp;</td>
        <td>
            <asp:Button ID="btnSave" runat="server" OnClick="btnSave_Click1" Text="Save" />

        </td>
        <td>
            &nbsp;</td>
    </tr>
</table>








        





