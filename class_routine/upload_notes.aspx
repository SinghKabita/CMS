<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="upload_notes.aspx.cs" Inherits="class_routine_upload_notes" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <table style="width: 40%" class="gridtable">
             <tr>
                <td>Faculty
                </td>
                <td>
                    <asp:DropDownList ID="ddlFaculty" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Level<br />
                </td>
                <td>

                    <asp:DropDownList ID="ddlLevel" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                        
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Program
                </td>
                <td>
                    <asp:DropDownList ID="ddlProgram" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>
            

            <tr>
                <td>Semester</td>
                <td>

                    <asp:DropDownList runat="server" ID="ddlSemester" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                    </asp:DropDownList>

                </td>
            </tr>

            <tr runat="server" visible="false">
                <td>Batch</td>
                <td>
                    <asp:DropDownList ID="ddlBatch" runat="server" Height="22px" AutoPostBack="True" Enabled="false">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Subject</td>
                <td colspan="3">
                    <asp:DropDownList ID="ddlSubject" Height="22px" runat="server"></asp:DropDownList></td>

            </tr>
            <tr>
                <td>Topic</td>
                <td colspan="3">
                    <asp:TextBox ID="txtTopic" Height="22px" Width="100%" Style="font-size: medium" runat="server"></asp:TextBox></td>

            </tr>
            <tr>
                <td>Upload Date</td>
                <td colspan="3">
                    <asp:TextBox ID="txtUploadDate" runat="server" Height="22px" Width="100%" Style="font-size: medium"></asp:TextBox>
                    <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtUploadDate"
                        Enabled="True" Format="dd/MM/yyyy">
                    </cc1:CalendarExtender>
                </td>


            </tr>
            <tr>
                <td>File</td>
                <td>
                    <asp:FileUpload ID="fileName" runat="server" Height="22px" Width="100%" Style="font-size: medium"></asp:FileUpload></td>

            </tr>
            <tr>

                <td>
                    <asp:Button ID="btnUpload" runat="server" Height="22px" Text="Upload" OnClick="btnUpload_Click" /></td>
                <td></td>

            </tr>
        </table>
    </div>
</asp:Content>

