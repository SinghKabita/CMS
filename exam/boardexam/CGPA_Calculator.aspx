 <%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CGPA_Calculator.aspx.cs" Inherits="exam_boardexam_CGPA_Calculator" %>



<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">


        function printPartOfPage() {
            var printContent = document.getElementById('print_div');
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
                    <asp:DropDownList ID="ddlFaculty" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>Program</td>
                <td>
                    <asp:DropDownList ID="ddlProgram" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>Batch
                </td>
                <td>
                    <asp:DropDownList ID="ddlBatch" Height="22px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"></asp:DropDownList>

                    <asp:Label Text="" runat="server" ID="lblBatch" Visible="false" />
                </td>
            </tr>

            <tr>
                <td>Student
                </td>
                <td>
                    <asp:DropDownList ID="ddlStudent" Height="22px" runat="server"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnView" runat="server" Text="View" OnClick="btnView_Click"></asp:Button>
                </td>
                <td>
                    <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" />
                </td>
            </tr>

        </table>

        <br />
        <br />
        <div id="print_div">
            <div id="hide" runat="server" visible="false">

                <table>
                   <tr>
                        <td colspan="4" style="font-size: 16pt; font-weight: bold">Student Detail</td>
                    </tr>
                   
                    <tr class="heading">
                        <td style="width: 30%">Batch:</td>
                        <td style="width: 35%">Registration No:</td>
                        <td style="width: 35%">Full Name:</td>
                        <td rowspan="14" style="vertical-align: top">
                            <asp:Image ID="imgStudent" runat="server" Height="100px" /></td>
                    </tr>

                    <tr style="font-weight: bold">
                        <td>
                            <asp:Label ID="lblBatchP" runat="server" Text=""></asp:Label></td>

                        <td>
                            <asp:Label ID="lblRegNo" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblFullNameEng" runat="server" Text=""></asp:Label></td>

                    </tr>
                </table>

                <br />
                <br />
                <table style="width:100%">
                    <tr>
                        <td style="width: 100%; text-align: center; font-weight: bold">First Semester</td>
                    </tr>
                    <tr>
                        <td style="width: 100%; text-align: center">

                            <asp:GridView ID="gridFirst" runat="server" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True"
                                 CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" OnRowDataBound="gridFirst_RowDataBound">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SN">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Subject" ItemStyle-Width="500px">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubjectId" runat="server" Text='<%# Bind("PK_ID") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblSubject" runat="server" Text='<%# Bind("SUBJECT_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCode" runat="server" Text='<%# Bind("SUBJECT_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalCredit" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCredit" runat="server" Text='<%# Bind("CREDIT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Points">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalpoints" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtPoint" width="40px" text="0" AutoPostBack="True" OnTextChanged="txtPoint_TextChanged"/>
                                         
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Grade">
                                        <ItemTemplate>
                                           
                                            <asp:Label Text="" runat="server" ID="lblGrade"/>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                  
                                    <asp:TemplateField HeaderText="Credit Points">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalCreditpoints" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>

                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreditPoints" runat="server" Text="0"></asp:Label>
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
                    <tr id="tr_totalsgpafirst" runat="server" visible="false">
                        <td style="text-align: right; padding-right: 40px; background-color: #5D7B9D; font-weight: bold; color: White; height: 30px;">SGPA &nbsp;
                        <asp:Label ID="lblTotalSGPAFirst" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td>&nbsp;</td>
                    </tr>

                    <tr>
                        <td style="width: 100%; text-align: center; font-weight: bold">Second Semester</td>

                    </tr>
                    <tr>
                        <td style="width: 100%; text-align: center">

                            <asp:GridView ID="gridSecond" runat="server" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True"
                                 CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" OnRowDataBound="gridFirst_RowDataBound">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SN">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Subject" ItemStyle-Width="500px">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubjectId" runat="server" Text='<%# Bind("PK_ID") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblSubject" runat="server" Text='<%# Bind("SUBJECT_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCode" runat="server" Text='<%# Bind("SUBJECT_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalCredit" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCredit" runat="server" Text='<%# Bind("CREDIT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                      <asp:TemplateField HeaderText="Points">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalpoints" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtPoint" width="40px" text="0" AutoPostBack="True" OnTextChanged="txtPoint_TextChanged"/>
                                           <%-- <asp:Label ID="lblPoints" runat="server" Text="0"></asp:Label>--%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Grade">
                                        <ItemTemplate>
                                            <%--<asp:DropDownList ID="ddlGrade" runat="server" Height="22px" Width="60px" AutoPostBack="True" OnSelectedIndexChanged="ddlGrade_SelectedIndexChanged">
                                            </asp:DropDownList>--%>

                                            <asp:Label Text="" runat="server" ID="lblGrade"/>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                  
                                    <asp:TemplateField HeaderText="Credit Points">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalCreditpoints" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>

                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreditPoints" runat="server" Text="0"></asp:Label>
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


                    <tr id="tr_totalsgpasecond" runat="server" visible="false">
                        <td style="text-align: right; padding-right: 40px; background-color: #5D7B9D; font-weight: bold; color: White; height: 30px;">SGPA &nbsp;
                        <asp:Label ID="lblTotalSGPASecond" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 100%; text-align: center; font-weight: bold">Third Semester</td>

                    </tr>
                    <tr>
                        <td style="width: 100%; text-align: center">

                            <asp:GridView ID="gridThird" runat="server" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" OnRowDataBound="gridThird_RowDataBound">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SN">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Subject" ItemStyle-Width="500px">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubjectId" runat="server" Text='<%# Bind("PK_ID") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblSubject" runat="server" Text='<%# Bind("SUBJECT_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCode" runat="server" Text='<%# Bind("SUBJECT_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalCredit" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCredit" runat="server" Text='<%# Bind("CREDIT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Points">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalpoints" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtPoint" width="40px" text="0" AutoPostBack="True" OnTextChanged="txtPoint_TextChanged"/>
                                           <%-- <asp:Label ID="lblPoints" runat="server" Text="0"></asp:Label>--%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Grade">
                                        <ItemTemplate>
                                            <%--<asp:DropDownList ID="ddlGrade" runat="server" Height="22px" Width="60px" AutoPostBack="True" OnSelectedIndexChanged="ddlGrade_SelectedIndexChanged">
                                            </asp:DropDownList>--%>

                                            <asp:Label Text="" runat="server" ID="lblGrade"/>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Credit Points">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalCreditpoints" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>


                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreditPoints" runat="server" Text="0"></asp:Label>
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
                    <tr id="tr_totalsgpathird" runat="server" visible="false">
                        <td style="text-align: right; padding-right: 40px; background-color: #5D7B9D; font-weight: bold; color: White; height: 30px;">SGPA &nbsp;
                        <asp:Label ID="lblTotalSGPAThird" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 100%; text-align: center; font-weight: bold">Fouth Semester</td>

                    </tr>
                    <tr>
                        <td style="width: 100%; text-align: center">

                            <asp:GridView ID="gridFouth" runat="server" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" OnRowDataBound="gridFouth_RowDataBound">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SN">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Subject" ItemStyle-Width="500px">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubjectId" runat="server" Text='<%# Bind("PK_ID") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblSubject" runat="server" Text='<%# Bind("SUBJECT_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCode" runat="server" Text='<%# Bind("SUBJECT_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalCredit" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCredit" runat="server" Text='<%# Bind("CREDIT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Points">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalpoints" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtPoint" width="40px" text="0" AutoPostBack="True" OnTextChanged="txtPoint_TextChanged"/>
                                           <%-- <asp:Label ID="lblPoints" runat="server" Text="0"></asp:Label>--%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Grade">
                                        <ItemTemplate>
                                            <%--<asp:DropDownList ID="ddlGrade" runat="server" Height="22px" Width="60px" AutoPostBack="True" OnSelectedIndexChanged="ddlGrade_SelectedIndexChanged">
                                            </asp:DropDownList>--%>

                                            <asp:Label Text="" runat="server" ID="lblGrade"/>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit Points">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalCreditpoints" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>


                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreditPoints" runat="server" Text="0"></asp:Label>
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
                    <tr id="tr_totalsgpafouth" runat="server" visible="false">
                        <td style="text-align: right; padding-right: 40px; background-color: #5D7B9D; font-weight: bold; color: White; height: 30px;">SGPA &nbsp;
                        <asp:Label ID="lblTotalSGPAFouth" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 100%; text-align: center; font-weight: bold">Fifth Semester</td>

                    </tr>
                    <tr>
                        <td style="width: 100%; text-align: center">

                            <asp:GridView ID="gridFifth" runat="server" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" OnRowDataBound="gridFifth_RowDataBound">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SN">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Subject" ItemStyle-Width="500px">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubjectId" runat="server" Text='<%# Bind("PK_ID") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblSubject" runat="server" Text='<%# Bind("SUBJECT_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCode" runat="server" Text='<%# Bind("SUBJECT_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalCredit" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCredit" runat="server" Text='<%# Bind("CREDIT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Points">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalpoints" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtPoint" width="40px"  text="0" AutoPostBack="True" OnTextChanged="txtPoint_TextChanged"/>
                                           <%-- <asp:Label ID="lblPoints" runat="server" Text="0"></asp:Label>--%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Grade">
                                        <ItemTemplate>
                                            <%--<asp:DropDownList ID="ddlGrade" runat="server" Height="22px" Width="60px" AutoPostBack="True" OnSelectedIndexChanged="ddlGrade_SelectedIndexChanged">
                                            </asp:DropDownList>--%>

                                            <asp:Label Text="" runat="server" ID="lblGrade"/>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit Points">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalCreditpoints" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>


                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreditPoints" runat="server" Text="0"></asp:Label>
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
                    <tr id="tr_totalsgpafifth" runat="server" visible="false">
                        <td style="text-align: right; padding-right: 40px; background-color: #5D7B9D; font-weight: bold; color: White; height: 30px;">SGPA &nbsp;
                        <asp:Label ID="lblTotalSGPAFifth" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>


                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 100%; text-align: center; font-weight: bold">Sixth Semester</td>

                    </tr>
                    <tr>
                        <td style="width: 100%; text-align: center">

                            <asp:GridView ID="gridSixth" runat="server" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" OnRowDataBound="gridSixth_RowDataBound">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SN">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Subject" ItemStyle-Width="500px">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubjectId" runat="server" Text='<%# Bind("PK_ID") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblSubject" runat="server" Text='<%# Bind("SUBJECT_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCode" runat="server" Text='<%# Bind("SUBJECT_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalCredit" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCredit" runat="server" Text='<%# Bind("CREDIT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                   <asp:TemplateField HeaderText="Points">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalpoints" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtPoint" width="40px" text="0" AutoPostBack="True" OnTextChanged="txtPoint_TextChanged"/>
                                           <%-- <asp:Label ID="lblPoints" runat="server" Text="0"></asp:Label>--%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Grade">
                                        <ItemTemplate>
                                            <%--<asp:DropDownList ID="ddlGrade" runat="server" Height="22px" Width="60px" AutoPostBack="True" OnSelectedIndexChanged="ddlGrade_SelectedIndexChanged">
                                            </asp:DropDownList>--%>

                                            <asp:Label Text="" runat="server" ID="lblGrade"/>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit Points">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalCreditpoints" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>


                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreditPoints" runat="server" Text="0"></asp:Label>
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
                    <tr id="tr_totalsgpasixth" runat="server" visible="false">
                        <td style="text-align: right; padding-right: 40px; background-color: #5D7B9D; font-weight: bold; color: White; height: 30px;">SGPA &nbsp;
                        <asp:Label ID="lblTotalSGPASixth" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>


                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 100%; text-align: center; font-weight: bold">Seventh Semester</td>

                    </tr>
                    <tr>
                        <td style="width: 100%; text-align: center">

                            <asp:GridView ID="gridSeventh" runat="server" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" OnRowDataBound="gridSeventh_RowDataBound">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SN">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Subject" ItemStyle-Width="500px">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubjectId" runat="server" Text='<%# Bind("PK_ID") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblSubject" runat="server" Text='<%# Bind("SUBJECT_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCode" runat="server" Text='<%# Bind("SUBJECT_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalCredit" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCredit" runat="server" Text='<%# Bind("CREDIT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Points">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalpoints" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtPoint" width="40px" text="0" AutoPostBack="True" OnTextChanged="txtPoint_TextChanged"/>
                                           <%-- <asp:Label ID="lblPoints" runat="server" Text="0"></asp:Label>--%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Grade">
                                        <ItemTemplate>
                                            <%--<asp:DropDownList ID="ddlGrade" runat="server" Height="22px" Width="60px" AutoPostBack="True" OnSelectedIndexChanged="ddlGrade_SelectedIndexChanged">
                                            </asp:DropDownList>--%>

                                            <asp:Label Text="" runat="server" ID="lblGrade"/>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit Points">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalCreditpoints" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>


                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreditPoints" runat="server" Text="0"></asp:Label>
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
                    <tr id="tr_totalsgpaseventh" runat="server" visible="false">
                        <td style="text-align: right; padding-right: 40px; background-color: #5D7B9D; font-weight: bold; color: White; height: 30px;">SGPA &nbsp;
                        <asp:Label ID="lblTotalSGPASeventh" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>


                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>

                    <tr>
                        <td style="width: 100%; text-align: center; font-weight: bold">Eight Semester</td>

                    </tr>
                    <tr>
                        <td style="width: 100%; text-align: center">

                            <asp:GridView ID="gridEight" runat="server" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" CellPadding="4" ForeColor="#333333" GridLines="None" ShowFooter="True" OnRowDataBound="gridEight_RowDataBound">
                                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                <Columns>
                                    <asp:TemplateField HeaderText="SN">
                                        <ItemTemplate>
                                            <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Subject" ItemStyle-Width="500px">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotal" runat="server" Text="Total"></asp:Label>
                                        </FooterTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSubjectId" runat="server" Text='<%# Bind("PK_ID") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblSubject" runat="server" Text='<%# Bind("SUBJECT_NAME") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Code">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCode" runat="server" Text='<%# Bind("SUBJECT_CODE") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Left" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalCredit" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCredit" runat="server" Text='<%# Bind("CREDIT") %>'></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Points">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalpoints" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:TextBox runat="server" ID="txtPoint" width="40px" text="0" AutoPostBack="True" OnTextChanged="txtPoint_TextChanged"/>
                                           <%-- <asp:Label ID="lblPoints" runat="server" Text="0"></asp:Label>--%>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Grade">
                                        <ItemTemplate>
                                            <%--<asp:DropDownList ID="ddlGrade" runat="server" Height="22px" Width="60px" AutoPostBack="True" OnSelectedIndexChanged="ddlGrade_SelectedIndexChanged">
                                            </asp:DropDownList>--%>

                                            <asp:Label Text="" runat="server" ID="lblGrade"/>

                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Credit Points">
                                        <FooterTemplate>
                                            <asp:Label ID="lblTotalCreditpoints" runat="server" Text=""></asp:Label>
                                        </FooterTemplate>


                                        <FooterStyle HorizontalAlign="Center" />
                                        <ItemTemplate>
                                            <asp:Label ID="lblCreditPoints" runat="server" Text="0"></asp:Label>
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
                    <tr id="tr_totalsgpaeight" runat="server" visible="false">
                        <td style="text-align: right; padding-right: 40px; background-color: #5D7B9D; font-weight: bold; color: White; height: 30px;">SGPA &nbsp;
                        <asp:Label ID="lblTotalSGPAEight" runat="server" Text=""></asp:Label>
                        </td>
                    </tr>


                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>



                </table>

                <table style="width: 30%; float: right; font-weight: bold; padding-right: 30px; 
                    border: 1px solid black; border-radius: 0 10px; padding: 10px">

                    <tr>

                        <td>Total Credit Points 
                        </td>
                        <td>
                            <asp:Label ID="lblGrandCreditPoints" runat="server" ></asp:Label></td>

                    </tr>
                    <tr>

                        <td>Total Credit 
                        </td>
                        <td>
                            <asp:Label ID="lblGrandCredits" runat="server" ></asp:Label></td>

                    </tr>
                    <tr>

                        <td>CGPA 
                        </td>
                        <td>
                            <asp:Label ID="lblCGPA" runat="server" ></asp:Label></td>

                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>