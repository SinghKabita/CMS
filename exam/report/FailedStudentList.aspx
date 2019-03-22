<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="FailedStudentList.aspx.cs" Inherits="exam_report_FailedStudentList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">


        function printPartOfPage() {
            var printContent = document.getElementById('div_print');
            var windowUrl = 'about:blank';
            var uniqueName = new Date();
            var windowName = 'Print' + uniqueName.getTime();
            var printWindow = window.open(windowUrl, windowName, 'left=0,top=0,width=0,height=0');

            printWindow.document.write(printContent.innerHTML);
            printWindow.document.close();
            printWindow.focus();
            printWindow.print();
            printWindow.close();
        }
    </script>

    <div class="container">
        <table class="gridtable">

            <tr>
                <td>Faculty</td>
                <td>
                    <asp:DropDownList ID="ddlFaculty" runat="server" Height="22px" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>Level</td>
                <td>
                    <asp:DropDownList ID="ddlLevel" runat="server" Height="22px" AutoPostBack="True"
                        OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
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
                    <asp:DropDownList ID="ddlSemester" runat="server" Height="22px"
                        AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>

                <td>Section</td>
                <td>
                    <asp:DropDownList ID="ddlSection" runat="server" Height="22px">
                    </asp:DropDownList>
                </td>

                <td>Exam Type</td>
                <td>
                    <asp:DropDownList ID="ddlExamType" runat="server" Height="22px">
                    </asp:DropDownList>
                </td>

            </tr>
            <tr>
                <td>Subject
                </td>
                <td>
                    <asp:DropDownList ID="ddlSubject" runat="server" Height="22px">
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Button ID="btnView" runat="server" OnClick="btnView_Click" Text="View" Style="width: 42px" />
                </td>
                <td>
                    <asp:Button Text="Print" ID="btnPrint" OnClick="btnPrint_Click" runat="server" /></td>

            </tr>
            <tr runat="server" visible="false">
                <td>Batch</td>
                <td>
                    <asp:DropDownList ID="ddlBatch" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
                <td>&nbsp;
                </td>
                <td>&nbsp;
                </td>
            </tr>

        </table>
        <div id="div_print">
            <table style="width: 35%">
                <tr>
                    <td colspan="2"></td>
                    <td colspan="5" style="text-align: center">

                        <asp:Label ID="lblCollegeName" runat="server" Style="font-weight: 700; font-size: large;"></asp:Label>

                    </td>
                    <td colspan="2"></td>
                </tr>

                <tr>
                    <td colspan="2"></td>
                    <td colspan="5" style="text-align: center">

                        <asp:Label ID="lblProgram" runat="server" Style="font-family: 'Arial'; font-weight: 700; font-size: medium;"></asp:Label>

                    </td>
                    <td colspan="2"></td>
                </tr>
                <tr>
                    <td colspan="9" style="text-align: center">
                        <asp:Label ID="lblSection" runat="server" Style="font-family: 'Arial'; font-weight: 700; font-size: medium;"></asp:Label>
                        
                    </td>
                </tr>
                <tr>
                    <td colspan="9" style="text-align: center">
                        <asp:Label ID="lblSubjectP" runat="server" Style="font-family: 'Arial'; font-weight: 700; font-size: medium; font-style:italic"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                    <td colspan="5" style="text-align: center"></td>
                    <td colspan="2"></td>
                </tr>
            </table>

            <table style="width: 50%">
                <tr>
                    <td>
                        <asp:GridView ID="gridFailedStudent" AutoGenerateColumns="false"
                            OnRowDataBound="gridFailedStudent_RowDataBound"
                            CssClass="gridtable" runat="server">
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        SN
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label Text='<%# Container.DataItemIndex+1 %>' ID="lblSN" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        STUDENT ID
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label Text='<%# Bind("STUDENT_ID") %>' ID="lblStudentID" Width="120px" Visible="true" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Name
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label Text="" ID="lblStudentName"  Width="200px" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Marks
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label Text='<%# Bind("MARKS") %>' ID="lblMarks" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        Remarks
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label Text='<%# Bind("REMARKS") %>' ID="lblRemarks" runat="server" />
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

