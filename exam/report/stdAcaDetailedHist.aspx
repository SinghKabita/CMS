<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="stdAcaDetailedHist.aspx.cs" Inherits="exam_report_stdAcaDetailedHist" %>

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
        <asp:Button Text="Print" ID="btnPrint" OnClick="btnPrint_Click" runat="server" />
        <asp:Button Text="Go Back" ID="btnGoBack" OnClick="btnGoBack_Click" runat="server" />
    </div>
    <div id="print_div">
        <%--<div class="jumbotron">--%>
        <div class="container">
            <div class="row">
                <div class="col-md-12" style="text-align: center">
                    <asp:Label Text="" ID="lblOrgName" Font-Bold="true" Font-Size="20pt" runat="server" />
                    <asp:Label ID="lblOrgAdd" Font-Bold="true" Font-Size="20pt" runat="server" />
                </div>
            </div>

            <table style="width: 100%;">
                <tr>
                    <td style="width: 25%;">
                        <asp:Label Text="FullName: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblStdFullName" runat="server" /></td>
                    <td>
                        <asp:Label Text="Year/Batch: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblBatchYear" runat="server" /></td>
                    <td>
                        <asp:Label Text="Date of Birth: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblDOB" runat="server" /></td>
                    <td rowspan="5">
                        <asp:Image ImageUrl="" ID="imgStudent" Width="100px" runat="server" /></td>
                </tr>
                <tr>
                    <td style="width: 25%;">
                        <asp:Label Text="NCCS Reg No.: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblNCCSRegNo" runat="server" /></td>
                    <td>
                        <asp:Label Text="TU Exam Roll No.: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblTURoll" runat="server" /></td>
                    <td>
                        <asp:Label Text="Self Mobile No.: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblSelfMobileNo" runat="server" /></td>

                </tr>
                <tr>
                    <td style="width: 25%;">
                        <asp:Label Text="Faculty: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblBachelorFaculty" runat="server" /></td>
                    <td>
                        <asp:Label Text="Level: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblLevel" runat="server" /></td>
                    <td>
                        <asp:Label Text="Program: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblProgram" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblProgramID" Visible="false" runat="server" /></td>
                </tr>
                <tr>
                    <td style="width: 25%;">
                        <asp:Label Text="+2 College: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblPlus2College" runat="server" /></td>
                    <td>
                        <asp:Label Text="+2 Percentage/GPA: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblPlus2Perc" runat="server" /></td>
                    <td>
                        <asp:Label Text="+2 Faculty: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblPlus2Faculty" runat="server" /></td>
                </tr>
                <tr>
                    <td style="width: 25%;">
                        <asp:Label Text="Perm Address: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblPermAddress" runat="server" /></td>
                    <td>
                        <asp:Label Text="Temp Address: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblTempAddress" runat="server" /></td>
                    <td>
                        <asp:Label Text="CMAT Score: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblCMATScore" runat="server" /></td>
                </tr>
            </table>

            <table style="width: 100%;">
                <tr>
                    <td style="width: 25%">
                        <asp:Label Text="Father's Name: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblFatherName" runat="server" /></td>
                    <td>
                        <asp:Label Text="Mother's Name: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblMotherName" runat="server" /></td>
                    <td>
                        <asp:Label Text="Spouse's Name: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblSpouseName" runat="server" /></td>
                    <td>
                        <asp:Label Text="Guardian's Name: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblGuardianName" runat="server" /></td>
                </tr>
                <tr>
                    <td style="width: 25%">
                        <asp:Label Text="Occupation: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblFatherOccupation" runat="server" /></td>
                    <td>
                        <asp:Label Text="Occupation: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblMotherOccupation" runat="server" /></td>
                    <td>
                        <asp:Label Text="Occupation: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblSpouseOccupation" runat="server" /></td>
                    <td>
                        <asp:Label Text="Occupation: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblGuardianOccupation" runat="server" /></td>
                </tr>
                <tr>
                    <td style="width: 25%">
                        <asp:Label Text="Address: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblFatherAddress" runat="server" /></td>
                    <td>
                        <asp:Label Text="Address: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblMotherAddress" runat="server" /></td>
                    <td>
                        <asp:Label Text="Address: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblSpouseAddress" runat="server" /></td>
                    <td>
                        <asp:Label Text="Address: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblGuardianAddress" runat="server" /></td>
                </tr>
                <tr>
                    <td style="width: 25%">
                        <asp:Label Text="Tel. No.: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblFatherTelNo" runat="server" /></td>
                    <td>
                        <asp:Label Text="Tel. No.: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblMotherTelNo" runat="server" /></td>
                    <td>
                        <asp:Label Text="Tel. No.: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblSpouseTelNo" runat="server" /></td>
                    <td>
                        <asp:Label Text="Tel. No.: " Font-Bold="true" Font-Size="10" runat="server" /><asp:Label Text="" Font-Bold="true" Font-Size="10" ID="lblGuardianTelNo" runat="server" /></td>
                </tr>
            </table>


            <div class="row mt-15">
                <asp:GridView ID="gridAcaHistory"
                    OnRowDataBound="gridAcaHistory_RowDataBound"
                    AutoGenerateColumns="false" CssClass="gridtable" Width="100%" runat="server">
                    <Columns>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:Label Text="Semester" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label Text='<%# Bind("SEMESTER") %>' ID="lblSemester" runat="server" />
                                <asp:Label Text='<%# Bind("SEMID") %>' ID="lblSemesterID" Visible="false" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Subject">

                            <ItemTemplate>
                                <asp:Label Text='<%# Bind("SUBJECTID") %>' ID="lblSubjectID" Visible="false" runat="server" />
                                <asp:Label Text='<%# Bind("SUBJECT_NAME") %>' ID="lblSubjectName" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-----------------------------------**************ExamTypeMaster1********************----------------------------------------%>
                        <asp:TemplateField HeaderText="Theory">
                            <HeaderTemplate>
                                <asp:Label Text="Theory" ID="lblFCTTheoryH" runat="server" />
                                <asp:Label Text="" ID="lblExamTypeMasterTh1" Visible="false" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label Text="FM:" runat="server" /><asp:Label Text="" ID="lblFMFCT" runat="server" /><br />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                <asp:Label Text="" ID="lblExamTypeMaster1" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label Text="" ID="lblFCTID" Visible="false" runat="server" />
                                <asp:Label Text="" ID="lblFCTMarks" Visible="true" runat="server" />
                                <asp:Label Text="*" Visible="false" ID="lblAstrikFCT" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Make Up">
                            <HeaderTemplate>
                                <asp:Label Text="Make Up" ID="lblFCTMakeUpH" runat="server" />
                                <asp:Label Text="" ID="lblExamTypeMasterMu1" Visible="false" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label Text="" ID="lblFCTMakeUp" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-----------------------------------***************ExamTypeMaster2*******************----------------------------------------%>
                        <asp:TemplateField HeaderText="Theory">
                            <HeaderTemplate>
                                <asp:Label Text="Theory" ID="lblFTTheoryH" runat="server" />
                                <asp:Label Text="" ID="lblExamTypeMasterTh2" Visible="false" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label Text="FM:" runat="server" /><asp:Label Text="" ID="lblFMFT" runat="server" /><br />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                <asp:Label Text="" ID="lblExamTypeMaster2" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label Text="" ID="lblFTID" Visible="false" runat="server" />
                                <asp:Label Text="" ID="lblFTMarks" Visible="true" runat="server" />
                                <asp:Label Text="*" Visible="false" ID="lblAstrikFT" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Make Up">
                            <HeaderTemplate>
                                <asp:Label Text="Make Up" ID="lblFTMakeUpH" runat="server" />
                                <asp:Label Text="" ID="lblExamTypeMasterMu2" Visible="false" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label Text="" ID="lblFTMakeUp" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <%-----------------------------------*****************ExamTypeMaster3*****************----------------------------------------%>
                        <asp:TemplateField HeaderText="Theory">
                            <HeaderTemplate>
                                <asp:Label Text="Theory" ID="lblSCTTheoryH" runat="server" />
                                <asp:Label Text="" ID="lblExamTypeMasterTh3" Visible="false" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label Text="FM:" runat="server" /><asp:Label Text="" ID="lblFMSCT" runat="server" /><br />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                <asp:Label Text="" ID="lblExamTypeMaster3" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label Text="" ID="lblSCTID" Visible="false" runat="server" />
                                <asp:Label Text="" ID="lblSCTMarks" Visible="true" runat="server" />
                                <asp:Label Text="*" Visible="false" ID="lblAstrikSCT" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Make Up">
                            <HeaderTemplate>
                                <asp:Label Text="Make Up" ID="lblSCTMakeUpH" runat="server" />
                                <asp:Label Text="" ID="lblExamTypeMasterMu3" Visible="false" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label Text="" ID="lblSCTMakeUp" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-----------------------------------*****************ExamTypeMaster4*****************----------------------------------------%>
                        <asp:TemplateField HeaderText="Theory">
                            <HeaderTemplate>
                                <asp:Label Text="Theory" ID="lblMTTheoryH" runat="server" />
                                <asp:Label Text="" ID="lblExamTypeMasterTh4" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label Text="FM:" runat="server" /><asp:Label Text="" ID="lblFMMT" runat="server" /><br />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                <asp:Label Text="" ID="lblExamTypeMaster4" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label Text="" ID="lblMTID" Visible="false" runat="server" />
                                <asp:Label Text="" ID="lblMTMarks" Visible="true" runat="server" />
                                <asp:Label Text="*" Visible="false" ID="lblAstrikMT" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Make Up">
                            <HeaderTemplate>
                                <asp:Label Text="Make Up" ID="lblMTMakeUpH" runat="server" />
                                <asp:Label Text="" ID="lblExamTypeMasterMu4" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label Text="" ID="lblMTMakeUp" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-----------------------------------****************ExamTypeMaster5******************----------------------------------------%>
                        <asp:TemplateField HeaderText="Theory">
                            <HeaderTemplate>
                                <asp:Label Text="Theory" ID="lblTCTTheoryH" runat="server" />
                                <asp:Label Text="" ID="lblExamTypeMasterTh5" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label Text="FM:" ID="labelFMTCT" runat="server" /><asp:Label Text="" ID="lblFMTCT" runat="server" /><br />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <HeaderTemplate>
                                <asp:Label Text="" ID="lblExamTypeMaster5" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label Text="" ID="lblTCTID" Visible="false" runat="server" />
                                <asp:Label Text="" ID="lblTCTMarks" Visible="true" runat="server" />
                                <asp:Label Text="*" Visible="false" ID="lblAstrikTCT" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Make Up">
                            <HeaderTemplate>
                                <asp:Label Text="Make Up" ID="lblTCTMakeUpH" runat="server" />
                                <asp:Label Text="" ID="lblExamTypeMasterMu5" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label Text="" ID="lblTCTMakeUp" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <%-----------------------------------****************ExamTypeMaster6******************----------------------------------------%>
                        <asp:TemplateField HeaderText="Theory">
                            <HeaderTemplate>
                                <asp:Label Text="Theory" ID="lblFETheoryH" runat="server" />
                                <asp:Label Text="" ID="lblExamTypeMasterTh6" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label Text="FM:" ID="labelFMFE" runat="server" /><asp:Label Text="" ID="lblFMFE" runat="server" /><br />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Final Exam">
                            <HeaderTemplate>
                                <asp:Label Text="" ID="lblExamTypeMaster6" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label Text="" ID="lblFEID" Visible="false" runat="server" />
                                <asp:Label Text="" ID="lblFEMarks" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Make Up">
                            <HeaderTemplate>
                                <asp:Label Text="Make Up" ID="lblFEMakeUp" runat="server" />
                                <asp:Label Text="" ID="lblExamTypeMasterMu6" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label Text="" ID="lblFEMakeUp" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>


                        <%-----------------------------------**********************************----------------------------------------%>
                        <asp:TemplateField HeaderText="Atten">
                            <HeaderTemplate>
                                <asp:Label Text="Atten" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label Text="" ID="lblAtten" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="TU GPA">
                            <HeaderTemplate>
                                <asp:Label Text="TU GPA" runat="server" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label Text="" ID="lblGPA" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>
            </div>

        </div>
    </div>
    <%--</div>--%>
</asp:Content>

