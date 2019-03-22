<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Routine_Print.aspx.cs" Inherits="class_routine_reports_Routine_Print" %>

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
        <div class="row">
            <div class="col-md-2">
                Faculty
                <asp:DropDownList ID="ddlFaculty" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" AutoPostBack="true" runat="server">
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                Level
                <asp:DropDownList ID="ddlLevel" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                Program
                <asp:DropDownList ID="ddlProgram" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>
            </div>
        </div>
        <div class="row mt-10">
            <div class="col-md-2">
                Semester
                <asp:DropDownList runat="server" Height="22px" ID="ddlSemester">
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                Section
                <asp:DropDownList runat="server" Height="22px" ID="ddlSection">
                </asp:DropDownList>
            </div>
        </div>

        <div class="row mt-10">
            <div class="col-md-2">
                Date
                <asp:TextBox ID="txtDate" AutoComplete="off" runat="server" Height="22px"></asp:TextBox>
                <asp:RequiredFieldValidator ErrorMessage="required" ControlToValidate="txtDate" runat="server" />
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDate"
                    Enabled="True" Format="dd.MMM.yyyy">
                </cc1:CalendarExtender>
            </div>
            <div class="col-md-4 mt-20">
                <asp:CheckBox Text="Include Saturday" ID="chkIncSaturday" CssClass="input-lg" runat="server" />
                <asp:CheckBox Text="Include Date" ID="chkShowDates" CssClass="input-lg" runat="server" />
            </div>

        </div>
        <div class="row">
            <div class="col-md-8">
                Comment
                
                <textarea id="txtComment" cols="60" rows="4" class=" form-control" runat="server"></textarea>
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
                        <td style="text-align: center; font-weight: bold;"><span style="font-size: large">
                            <asp:Label Text="" ID="lblCollegeName" runat="server" />
                        </span></td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-weight: bold;">
                            <asp:Label ID="lblAddress" runat="server" Text=""></asp:Label></td>
                    </tr>
                </table>
                <table style="width: 100%; border: 1px solid black;">
                    <tr>
                        <td style="text-align: center; font-weight: bold;"><span style="font-size: large">CLASS ROUTINES</span>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: center; font-weight: bold;">
                            <asp:Label ID="lblProgramP" runat="server" Text=""></asp:Label></td>
                    </tr>

                    <tr>
                        <td style="text-align: center; font-weight: bold">Semester:<asp:Label Text="" ID="lblSemesterP" runat="server" />
                            Section:<asp:Label Text="" ID="lblSectionP" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="gridRoutine1" runat="server" CssClass="gridroutine"
                                AutoGenerateColumns="False" EnableModelValidation="True"
                                OnRowDataBound="gridRoutine1_RowDataBound" Width="100%">
                                <Columns>
                                    <asp:TemplateField HeaderText="Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPeriodId" runat="server" Text='<%# Bind("PK_ID") %>' Visible="false"></asp:Label>
                                            <asp:Label ID="lblPeriod" runat="server" Visible="true" Text='<%# Bind("PERIODS") %>'></asp:Label>
                                            <br />
                                            <asp:Label ID="lblTime" Visible="false" runat="server" Text='<%# Bind("TIME") %>'></asp:Label>
                                            <asp:Label Text="" ID="lblfromTime" runat="server" />
                                            <asp:Label Text="" ID="lbltoTime" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="100px" Font-Size="14pt" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Sunday
                                            <br />
                                            <asp:Label Text="" ID="lblSunDateH" CssClass="input-sm" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label Text="" ID="lblSundaySub" runat="server" />
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="150px" Font-Size="14pt" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            Monday
                                            <br />
                                            <asp:Label Text="" ID="lblMonDateH" CssClass="input-sm" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblMondaySub" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="150px" Font-Size="14pt" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            Tuesday
                                            <br />
                                            <asp:Label Text="" ID="lblTuesDateH" CssClass="input-sm" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblTuesdaySub" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="150px" Font-Size="14pt" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            Wednesday
                                            <br />
                                            <asp:Label Text="" ID="lblWedDateH" CssClass="input-sm" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblWednesdaySub" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="150px" Font-Size="14pt" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            Thursday
                                            <br />
                                            <asp:Label Text="" ID="lblThursDateH" CssClass="input-sm" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblThursdaySub" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="150px" Font-Size="14pt" />
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="">
                                        <HeaderTemplate>
                                            Friday
                                            <br />
                                            <asp:Label Text="" ID="lblFriDateH" CssClass="input-sm" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblFridaySub" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" Width="150px" Font-Size="14pt" />
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:Label Text="Saturday" Visible="true" ID="lblSaturdayH" runat="server" />
                                            <br />
                                            <asp:Label Text="" ID="lblSatDateH" CssClass="input-sm" runat="server" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:Label ID="lblSaturdaySub" runat="server"></asp:Label>
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
                    <tr>
                        <td>
                            <asp:GridView ID="gridTeacherFullName" Visible="false" AutoGenerateColumns="false" CssClass="gridtable"
                                OnRowDataBound="gridTeacherFullName_RowDataBound" runat="server">
                                <Columns>
                                    <asp:TemplateField HeaderText="Abbr">
                                        <ItemTemplate>
                                            <asp:Label Text='<%# Bind("TEACHER_ID") %>' ID="lblTeacherID" Visible="false" runat="server" />
                                            <asp:Label Text="" ID="lblAbbr" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Full Form">
                                        <ItemTemplate>
                                            <asp:Label Text="" ID="lblFullName" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <asp:Label Text="" ID="lblTeacherNameAbbr" Visible="false" runat="server" />

                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:GridView AutoGenerateColumns="false" ID="gridSubjectFullName" Visible="false" runat="server">
                                <Columns>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label Text='<%# Bind("SUBJECT_CODE") %>' ID="lblSubCode" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Label Text='<%# Bind("SUBJECT_NAME") %>' ID="lblSubName" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>

                            <asp:Label Text="" ID="lblSubNameAbbr" runat="server" />

                        </td>
                    </tr>
                    <tr>
                        <td style="padding-left: 5px;">
                            <asp:Label Text="" Visible="false" ID="lblComment" runat="server" />
                        </td>
                    </tr>

                </table>

            </div>
        </div>
    </div>
</asp:Content>

