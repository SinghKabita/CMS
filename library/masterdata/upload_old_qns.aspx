<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="upload_old_qns.aspx.cs" Inherits="library_masterdata_upload_old_qns" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <table style="width: 50%;" class="gridtable">
            <tr>
                <td>Year</td>
                <td colspan="2">
                    <asp:TextBox ID="txtYear" OnTextChanged="txtYear_TextChanged" AutoPostBack="true" runat="server" Height="22px" Style="font-size: medium"></asp:TextBox>
                    <asp:Label ID="Label1" Text="in BS" ForeColor="RosyBrown" runat="server" />
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="Required" ControlToValidate="txtYear" runat="server" />
                    <asp:CompareValidator ID="cv" runat="server" ControlToValidate="txtYear" Type="Integer"
                        Operator="DataTypeCheck" ErrorMessage="Value must be numeric!" />

                </td>

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
                <td>Semester</td>
                <td>
                    <asp:DropDownList ID="ddlSemester" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged" Height="22px" runat="server" AutoPostBack="True" ></asp:DropDownList></td>

            </tr>
            <tr>
                <td>Subject</td>
                <td>
                    <asp:DropDownList ID="ddlSubject" Height="22px" runat="server"></asp:DropDownList></td>


                <td>Exam Type</td>
                <td>
                    <asp:DropDownList ID="ddlExamType" Height="22px" runat="server"></asp:DropDownList></td>

            </tr>
            <tr>
                <td>Topic</td>
                <td colspan="3">
                    <asp:TextBox ID="txtTopic" runat="server" Height="22px" Width="100%"></asp:TextBox></td>

            </tr>

            <tr>
                <td>File</td>
                <td colspan="3">
                    <asp:FileUpload ID="fileName" runat="server" Height="22px" Width="100%"></asp:FileUpload></td>

            </tr>
            <tr>

                <td>
                    <asp:Button ID="btnUpload" runat="server" Height="22px" Text="Upload" OnClick="btnUpload_Click" /></td>
                <td></td>

            </tr>
        </table>
    </div>
</asp:Content>


