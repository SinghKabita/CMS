<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="MarksRecord.aspx.cs" Inherits="exam_report_MarksRecord" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <table class="gridtable">

        <tr>
            <td>Batch</td> <td>
                <asp:DropDownList ID="ddlBatch" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged">
                </asp:DropDownList>
            </td>
            <td style="width: 24px">
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
                <asp:DropDownList ID="ddlSemester" runat="server" Height="22px">
                </asp:DropDownList>
            </td>
            <td style="width: 24px">
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
         
        <tr>
            <td style="height: 34px">
                Exam Type</td>
            <td style="height: 34px">
                <asp:DropDownList ID="ddlExamType" runat="server" Height="22px">
                </asp:DropDownList>
            </td>
            <td style="width: 24px; height: 34px;">
                &nbsp;
            </td>
            <td style="height: 34px">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>

                <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" />

            </td>
             <td>

            </td>
             <td>

            </td>
             <td>

            </td>
        </tr>
       
     
    </table>
</asp:Content>

