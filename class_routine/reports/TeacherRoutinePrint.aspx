<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TeacherRoutinePrint.aspx.cs" Inherits="class_routine_reports_TeacherRoutinePrint" %>

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

        <div class="row mt-10">
            <div class="col-md-2">
                Date
                <asp:TextBox ID="txtDate" runat="server" OnTextChanged="txtDate_TextChanged" AutoPostBack="true" AutoComplete="off" Height="22px"></asp:TextBox>
                <asp:RequiredFieldValidator ErrorMessage="required" ControlToValidate="txtDate" runat="server" />
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDate"
                    Enabled="True" Format="dd.MMM.yyyy">
                </cc1:CalendarExtender>
            </div>
            <div class="col-md-2">
                Teacher
                <asp:DropDownList ID="ddlTeacher" CausesValidation="true" runat="server">
                </asp:DropDownList>
            </div>
            <div class="col-md-4 mt-20">
                <asp:CheckBox Text="Include Date" ID="chkShowDates" CssClass="input-lg" runat="server" />
            </div>

        </div>

        <div class="row mt-10">
            <div class="col-md-4">
                <asp:Button ID="btnView" Text="View" runat="server" OnClick="btnView_Click"></asp:Button>
                <asp:Button ID="btnPrint" Text="Print" runat="server" OnClick="btnPrint_Click"></asp:Button>
            </div>
        </div>

        <div id="div_print">
            <div id="hide" runat="server" visible="false">
                <table style="width: 100%; border: 1px solid black;">
                    <tr>
                        <td style="text-align: center; font-weight: bold;"><span style="font-size: medium">
                            <asp:Label Text="" ID="lblTeacherName" runat="server" />
                        </span></td>
                    </tr>

                </table>
                <table style="width: 100%; border: 1px solid black;">

                    <tr>
                        <td>
                            <asp:GridView ID="gridRoutine1" runat="server" CssClass="gridroutine"
                                AutoGenerateColumns="False" EnableModelValidation="True"
                                OnRowDataBound="gridRoutine1_RowDataBound" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Day/Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblWeekDays" runat="server" Text='<%# Bind("DAYS") %>' Visible="true"></asp:Label>
                                            <asp:Label Text="" ID="lblDate" Visible="false" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="100px" Font-Size="14pt" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label Text="11:00 12:00" ID="lblPeriod1" CssClass="input-sm" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label Text="" ID="lblFirstPeriod" runat="server"  />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="150px" Font-Size="14pt" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label Text="12:00 13:00" ID="lblPeriod2" CssClass="input-sm" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label Text="" ID="lblSecondPeriod" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="150px" Font-Size="14pt" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            <asp:Label Text="13:00 14:00" ID="lblPeriod3" CssClass="input-sm" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label Text="" ID="lblThirdPeriod" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="150px" Font-Size="14pt" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            <asp:Label Text="14:00 14:30" ID="lblPeriodBrk" CssClass="input-sm" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label Text="Break" ID="lblBreakPeriod" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="150px" Font-Size="14pt" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            <asp:Label Text="14:30 15:30" ID="lblPeriod4" CssClass="input-sm" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label Text="" ID="lblFourthPeriod" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="150px" Font-Size="14pt" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            <asp:Label Text="15:30 16:30" ID="lblPeriod5" CssClass="input-sm" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label Text="" ID="lblFifthPeriod" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="150px" Font-Size="14pt" />
                                    </asp:TemplateField>
                                   
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>

                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                </table>

            </div>
        </div>
    </div>
</asp:Content>

