<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="CGPAList.aspx.cs" Inherits="exam_report_CGPAList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
    
    
      <table class="gridtable">
        <tr>
        <td>
            Batch
        </td>
            <td>
           <asp:DropDownList id="ddlBatch" height="22px" runat="server"></asp:DropDownList>
        </td>
           </tr>
         <tr>
            <td>
                <asp:Button id="btnView" runat="server" Text="View" OnClick="btnView_Click"></asp:Button>
            </td>
             <td>
                 <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" />
            </td>
           
        </tr>
        </table>
    <div id="hide" runat="server" visible="false">
    <div id="print_div">
    <table style="width:100%">
        <tr>
            <td style="text-align:center;font-size:18px;font-weight:bold">
                <asp:Label ID="lblBatch" runat="server"></asp:Label><br>
                  <asp:Label ID="lblReport" runat="server" Text="CGPA List"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <asp:GridView ID="gridCGPAList" runat="server" Width="100%" AutoGenerateColumns="False" EnableModelValidation="True" CellPadding="4" ForeColor="#333333" GridLines="None" OnRowDataBound="gridCGPAList_RowDataBound">
                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                    <Columns>
                        <asp:TemplateField HeaderText="SN">
                            <ItemTemplate>
                                <asp:Label ID="lblSN" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Student Id">
                             <ItemTemplate>
                                <asp:Label ID="lblStudentId" runat="server" Text='<%# bind("STUDENT_ID") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Name">
                            <ItemTemplate>
                                <asp:Label ID="lblStudentName" runat="server" Text=""></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="1st Sem">
                            <ItemTemplate>
                                <asp:Label ID="lblFirst" runat="server" Text='<%# bind("firstsem") %>'></asp:Label>
                            </ItemTemplate>
                            <itemstyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="2nd Sem">
                            <ItemTemplate>
                                <asp:Label ID="lblSecond" runat="server" Text='<%# bind("secondsem") %>'></asp:Label>
                            </ItemTemplate>
                            <itemstyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="3rd Sem">
                            <ItemTemplate>
                                <asp:Label ID="lblThird" runat="server" Text='<%# bind("thirdsem") %>'></asp:Label>
                            </ItemTemplate>
                            <itemstyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="4th Sem">
                            <ItemTemplate>
                                <asp:Label ID="lblFouth" runat="server" Text='<%# bind("fouthsem") %>'></asp:Label>
                            </ItemTemplate>
                            <itemstyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="5th Sem">
                            <ItemTemplate>
                                <asp:Label ID="lblFifth" runat="server" Text='<%# bind("fifthsem") %>'></asp:Label>
                            </ItemTemplate>
                            <itemstyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="6th Sem">
                            <ItemTemplate>
                                <asp:Label ID="lblSixth" runat="server" Text='<%# bind("sixthsem") %>'></asp:Label>
                            </ItemTemplate>
                            <itemstyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="7th Sem">
                            <ItemTemplate>
                                <asp:Label ID="lblSeventh" runat="server" Text='<%# bind("seventhsem") %>'></asp:Label>
                            </ItemTemplate>
                            <itemstyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="8th Sem">
                            <ItemTemplate>
                                <asp:Label ID="lblEight" runat="server" Text='<%# bind("eightsem") %>'></asp:Label>
                            </ItemTemplate>
                            <itemstyle HorizontalAlign="Center" />
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="CGPA">
                             <ItemTemplate>
                                <asp:Label ID="lblCGPA" runat="server" Text='<%# bind("CGPA") %>'></asp:Label>
                            </ItemTemplate>
                            <itemstyle HorizontalAlign="Center" Font-Bold="true" />
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
    </table>
        </div>
        </div>
</asp:Content>

