<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="markslist.aspx.cs" Inherits="uc_result_markslist" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                <td>
                    <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click" Style="width: 42px" />
                    
                </td>
                <td><asp:Button Text="Print" ID="btnPrint" OnClick="btnPrint_Click" runat="server" /></td>

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
    </div>



    <br />
    <div id="div_print">
        <table style="width: 100%">
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
                <td colspan="2"></td>
                <td colspan="5" style="text-align: center"></td>
                <td colspan="2"></td>
            </tr>
        </table>


        <table style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="gridMarksList" runat="server" Width="100%" CssClass="gridtable" AutoGenerateColumns="False" EnableModelValidation="True"
                        OnRowDataBound="gridMarksList_RowDataBound">
                        <Columns>
                            <asp:TemplateField>
                                <HeaderTemplate>
                                    <th id="groups" runat="server" style="text-align: center">
                                        <asp:Label ID="lblExamType" runat="server" Style="font-family: 'Arial'; font-weight: 700; font-size: medium;"></asp:Label>
                                    </th>

                                    <tr>
                                        <th rowspan="3"></th>
                                        <th rowspan="3">SN</th>
                                        <th rowspan="3">Student Id</th>
                                        <th rowspan="3">Student's Name</th>
                                        <th id="subjects" runat="server" style="text-align: center">Subjects</th>
                                        <th rowspan="3">Total</th>
                                        <th rowspan="3">Percent</th>
                                        <th rowspan="3">Result</th>
                                        <th rowspan="3">Rank</th>
                                    </tr>
                                    <tr>

                                        <th id="subj1th" runat="server">
                                            <asp:Label ID="lblSubj1Name" runat="server" Width="80px"></asp:Label></th>
                                        <th id="subj2th" runat="server">
                                            <asp:Label ID="lblSubj2Name" runat="server" Width="80px"></asp:Label></th>
                                        <th id="subj3th" runat="server">
                                            <asp:Label ID="lblSubj3Name" runat="server" Width="80px"></asp:Label></th>
                                        <th id="subj4th" runat="server">
                                            <asp:Label ID="lblSubj4Name" runat="server" Width="80px"></asp:Label></th>
                                        <th id="subj5th" runat="server">
                                            <asp:Label ID="lblSubj5Name" runat="server" Width="80px"></asp:Label></th>

                                        <th id="subj6th" runat="server">

                                            <asp:Label ID="lblSubj6Name" runat="server" Width="80px"></asp:Label>
                                        </th>
                                        <th id="subj7th" runat="server">
                                            <asp:Label ID="lblSubj7Name" runat="server" Width="80px"></asp:Label></th>

                                    </tr>
                                    <tr>

                                        <th id="subj1fmth" runat="server">FM:
                                        <asp:Label ID="lblFMS1" runat="server" Text=""></asp:Label>
                                            <br />
                                            PM:
                                            <asp:Label ID="lblPMS1" runat="server" Text=""></asp:Label>
                                        </th>
                                        <th id="subj2fmth" runat="server">FM:
                                        <asp:Label ID="lblFMS2" runat="server" Text=""></asp:Label>
                                            <br />
                                            PM:
                                            <asp:Label ID="lblPMS2" runat="server" Text=""></asp:Label>
                                        </th>
                                        <th id="subj3fmth" runat="server">FM:
                                         <asp:Label ID="lblFMS3" runat="server" Text=""></asp:Label>
                                            <br />
                                            PM:
                                            <asp:Label ID="lblPMS3" runat="server" Text=""></asp:Label>
                                        </th>
                                        <th id="subj4fmth" runat="server">FM:
              <asp:Label ID="lblFMS4" runat="server" Text=""></asp:Label>
                                            <br />
                                            PM:
                                            <asp:Label ID="lblPMS4" runat="server" Text=""></asp:Label>
                                        </th>
                                        <th id="subj5fmth" runat="server">FM:
              <asp:Label ID="lblFMS5" runat="server" Text=""></asp:Label>
                                            <br />
                                            PM:
                                            <asp:Label ID="lblPMS5" runat="server" Text=""></asp:Label>
                                        </th>

                                        <th id="subj6fmth" runat="server">FM:
              <asp:Label ID="lblFMS6" runat="server" Text=""></asp:Label>
                                            <br />
                                            PM:
                                            <asp:Label ID="lblPMS6" runat="server" Text=""></asp:Label>

                                        </th>

                                        <th id="subj7fmth" runat="server">FM:
              <asp:Label ID="lblFMS7" runat="server" Text=""></asp:Label>
                                            <br />
                                            PM:
                                            <asp:Label ID="lblPMS7" runat="server" Text=""></asp:Label>
                                        </th>

                                    </tr>
                                </HeaderTemplate>

                                <ItemTemplate>
                                    <td>
                                        <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblStudentId" runat="server" Text='<%# Bind("student_id") %>'></asp:Label></td>
                                    <td>
                                        <asp:Label ID="lblStudentName" runat="server" Text='<%# Bind("name_english") %>' Width="200px"></asp:Label></td>
                                    <td id="subj1td" runat="server" style="text-align: center">
                                        <asp:Label ID="lblSub1" runat="server" Visible="false" Text='<%# Bind("subj1") %>'></asp:Label>
                                        <asp:Label ID="lblSub1Marks" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td id="subj2td" runat="server" style="text-align: center">
                                        <asp:Label ID="lblSub2" runat="server" Visible="false" Text='<%# Bind("subj2") %>'></asp:Label>
                                        <asp:Label ID="lblSub2Marks" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td id="subj3td" runat="server" style="text-align: center">
                                        <asp:Label ID="lblSub3" runat="server" Visible="false" Text='<%# Bind("subj3") %>'></asp:Label>
                                        <asp:Label ID="lblSub3Marks" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td id="subj4td" runat="server" style="text-align: center">
                                        <asp:Label ID="lblSub4" runat="server" Visible="false" Text='<%# Bind("subj4") %>'></asp:Label>
                                        <asp:Label ID="lblSub4Marks" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td id="subj5td" runat="server" style="text-align: center">
                                        <asp:Label ID="lblSub5" runat="server" Visible="false" Text='<%# Bind("subj5") %>'></asp:Label>
                                        <asp:Label ID="lblSub5Marks" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td id="subj6td" runat="server" style="text-align: center">

                                        <asp:Label ID="lblSub6" runat="server" Visible="false" Text='<%# Bind("subj6") %>'></asp:Label>
                                        <asp:Label ID="lblSub6Marks" runat="server" Text=""></asp:Label>

                                    </td>
                                    <td id="subj7td" runat="server" style="text-align: center">
                                        <asp:Label ID="lblSub7" runat="server" Visible="false" Text='<%# Bind("subj7") %>'></asp:Label>
                                        <asp:Label ID="lblSub7Marks" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblTotal" runat="server" Text='<%# Bind("total") %>'></asp:Label>

                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblPercentage" runat="server" Text='<%# Bind("percentage") %>'></asp:Label>

                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblRemarks" runat="server" Text='<%# Bind("remarks") %>'></asp:Label>

                                    </td>
                                    <td style="text-align: center">
                                        <asp:Label ID="lblRank" runat="server" Text='<%# Bind("rank") %>'></asp:Label>

                                    </td>
                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </td>
            </tr>

        </table>
    </div>
    <%--<table>
        <tr>
            <td id="btnPrint" runat="server">
                <input name="b_print" onclick="printPartOfPage();" type="button" value="Print" />
            </td>
        </tr>
    </table>--%>
</asp:Content>

