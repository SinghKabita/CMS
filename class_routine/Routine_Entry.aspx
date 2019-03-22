<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Routine_Entry.aspx.cs" Inherits="class_routine_Routine_Entry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function popp() {
            $('#overlay-div').addClass('overlay');
            $('#routine-div').removeClass('hidden');
            $('.cancel-control').css('color', 'black');

        }
    </script>
    <div class="container">


        <div class="row">
            <asp:Button Text="Update Routine" ID="btnUpdateR" OnClick="btnUpdateR_Click" runat="server" />
            <asp:Button Text="New Routine" OnClick="btnNewR_Click" ID="btnNewR" runat="server" />
            <asp:Button Text="Same as Prev Day" ID="btnPrevDayR" OnClick="btnPrevDayR_Click" runat="server" />
            <asp:Button Text="Same as Prev Week" ID="btnPrevWeekR" OnClick="btnPrevWeekR_Click" runat="server" />
        </div>
        <div class="row" runat="server" visible="false" id="IDs">
            <asp:Label Text="" ID="lblUpdateID" runat="server" />
            <asp:Label Text="" ID="lblNewID" runat="server" />
            <asp:Label Text="" ID="lblPrevDayID" runat="server" />
            <asp:Label Text="" ID="lblWeekID" runat="server" />
        </div>
        <asp:Panel ID="pnlVisibleUptoSec" Visible="false" runat="server">

            <table class="gridtable mt-15">

                <tr>
                    <td>Faculty
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFaculty" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"></asp:DropDownList>
                    </td>

                    <td>Level
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlLevel" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>Program
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlProgram" runat="server" Height="22px"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>

                <tr>
                    <td>Semester</td>
                    <td>
                        <asp:DropDownList runat="server" Height="22px" ID="ddlSemester"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged">
                        </asp:DropDownList></td>

                    <td>Section</td>
                    <td>
                        <asp:DropDownList runat="server" ID="ddlSection" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"></asp:DropDownList></td>

                </tr>
                <tr id="Tr1" runat="server" visible="false">
                    <td>Batch</td>
                    <td>
                        <asp:DropDownList runat="server" Height="22px" ID="ddlBatch" AutoPostBack="True"></asp:DropDownList></td>

                </tr>
                <tr runat="server" id="trDate">
                    <td>Date
                    </td>
                    <td>
                        <asp:TextBox ID="txtDate" AutoComplete="off" runat="server" Height="22px" AutoPostBack="True"
                            OnTextChanged="txtDate_TextChanged"></asp:TextBox>
                        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtDate"
                            Enabled="True" Format="dd.MMM.yyyy">
                        </cc1:CalendarExtender>

                        <asp:RequiredFieldValidator ErrorMessage="Required" ForeColor="Red" ValidationGroup="validateDate" ControlToValidate="txtDate" runat="server" />
                    </td>
                    <td>
                        <asp:Label Text="" Font-Size="Medium" Font-Bold="true" ForeColor="#009933" ID="lblDayD" runat="server" />
                    </td>

                </tr>
                <tr runat="server" visible="false" id="TRPrevDate">
                    <td>Same as of Date
                    </td>
                    <td>
                        <asp:TextBox ID="txtPrevDate" AutoComplete="off" runat="server" Height="22px" AutoPostBack="True" OnTextChanged="txtPrevDate_TextChanged" />
                        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtPrevDate"
                            Enabled="True" Format="dd.MMM.yyyy">
                        </cc1:CalendarExtender>
                        <asp:RequiredFieldValidator ErrorMessage="Required" ValidationGroup="validateDate" ForeColor="Red" ControlToValidate="txtPrevDate" runat="server" />
                    </td>
                    <td>
                        <asp:Label Text="" Font-Size="Medium" Font-Bold="true" ForeColor="#009933" ID="lblPrevDay" runat="server" />

                    </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td>
                        <asp:Button Text="View" ValidationGroup="validateDate" ID="btnView" Visible="false" OnClick="btnView_Click" runat="server" />
                        <asp:Button Text="View Routine" ID="btnViewPrvDate" Visible="false" OnClick="btnViewPrvDate_Click" runat="server" />
                        <asp:Button Text="View Routine for Week" ID="btnViewWeek" Visible="false" OnClick="btnViewWeek_Click" runat="server" />
                    </td>
                </tr>
            </table>
            <table style="width: 100%">
                <tr>
                    <td>
                        <%--when update btn is clicked then this grid is displayed--%>
                        <asp:GridView ID="gridRoutine" Visible="false" runat="server"
                            AutoGenerateColumns="False" EnableModelValidation="True" CssClass="gridtable"
                            OnRowDataBound="gridRoutine_RowDataBound"
                            OnRowCommand="gridRoutine_RowCommand" Width="75%">
                            <Columns>
                                <asp:TemplateField HeaderText="Day">
                                    <ItemTemplate>
                                        <asp:Label Text="" ID="lblRoutineID" Visible="false" runat="server" />
                                        <asp:Label ID="lblDay" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Period">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPeriodId" runat="server" Text='<%# Bind("PK_ID") %>' Visible="false"></asp:Label>
                                        <asp:Label Text='<%# Bind("PERIODS") %>' ID="lblPeriod" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Time">
                                    <ItemTemplate>

                                        <asp:Label ID="lblTime" runat="server" Text='<%# Bind("TIME") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Batch1A">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblBatch1AH" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBatch1A" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="lblRoutine1" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Button Text="Off" ID="btnOff" CommandName="off" runat="server" />
                                        <asp:Button Text="Edit" CommandName="change" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>

                        <%--when SAME AS PREV DAY btn clicked then grid below is displayed--%>
                        <asp:GridView ID="gridRoutinePrevDay" Visible="false" AutoGenerateColumns="false" CssClass="gridtable"
                            OnRowDataBound="gridRoutinePrevDay_RowDataBound" Width="75%"
                            runat="server">
                            <Columns>
                                <asp:TemplateField HeaderText="Day">
                                    <ItemTemplate>
                                        <asp:Label Text="" ID="lblRoutineID" Visible="false" runat="server" />
                                        <asp:Label ID="lblDay" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Period">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPeriodId" runat="server" Text='<%# Bind("PK_ID") %>' Visible="false"></asp:Label>
                                        <asp:Label Text='<%# Bind("PERIODS") %>' ID="lblPeriod" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Time">
                                    <ItemTemplate>

                                        <asp:Label ID="lblTime" runat="server" Text='<%# Bind("TIME") %>'></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Batch1A">
                                    <HeaderTemplate>
                                        <asp:Label ID="lblBatch1AH" runat="server" Text=""></asp:Label>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:Label ID="lblBatch1A" runat="server" Text="" Visible="false"></asp:Label>
                                        <asp:Label ID="lblRoutine1" runat="server" Text=""></asp:Label>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>

                            </Columns>
                        </asp:GridView>

                    </td>
                </tr>
            </table>
            <%--visible when EDIT button is clicked in UPDATE ROUTINE **start** --%>
            <table class="gridtable mt-15">
                <asp:Label Text="" ID="lblroutineIDe" Visible="false" runat="server" />
                <tr runat="server" visible="false" id="TRprd">
                    <td>Period
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlPeriod" Height="22px" runat="server"></asp:DropDownList>
                    </td>
                    <td>Subject
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlSubject" Height="22px" runat="server"
                            AutoPostBack="True" OnSelectedIndexChanged="ddlSubject_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>Teacher
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlTeacher" Height="22px" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>

                <tr runat="server" visible="false" id="trLabThr">
                    <td>Lab/Theory
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlLabTheory" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlLabTheory_SelectedIndexChanged" runat="server" Height="22px">

                            <asp:ListItem>Theory</asp:ListItem>
                            <asp:ListItem>Lab</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label Text="Class No" ID="lblClassNo" Visible="true" runat="server" />
                        <asp:Label Text="Lab No" ID="lblLabNo" Visible="false" runat="server" />
                    </td>
                    <td>
                        <asp:TextBox ID="txtClassNo" Visible="true" runat="server" Height="22px"></asp:TextBox>
                        <asp:TextBox ID="txtLabNo" Visible="false" runat="server" Height="22px"></asp:TextBox>
                    </td>

                    <td>
                        <asp:Label Text="Lab Assistant Teacher" ID="lblAssistTeacher" Visible="false" runat="server" />
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlAssistTeacher" Visible="false" Height="22px" runat="server">
                        </asp:DropDownList></td>

                </tr>

                <tr runat="server" visible="false" id="trBtnSave">

                    <td>
                        <asp:Button ID="btnSave" ValidationGroup="validateDate" runat="server"
                            Text="Save" OnClick="btnSave_Click" />
                    </td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            <%--visible when EDIT button is clicked **END** --%>
            <table runat="server" id="tblPrevWeek" class="gridtable mt-15">
                <tr runat="server" id="TRprevWeek">
                    <td>
                        <asp:GridView ID="gridWeek" OnRowDataBound="gridWeek_RowDataBound" AutoGenerateColumns="false" runat="server">
                            <Columns>
                                <asp:TemplateField HeaderText="Day">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# Bind("CAL_DAY_OF_WEEK") %>' ID="lblDay" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="As of Date">
                                    <ItemTemplate>
                                        <asp:Label Text="" ID="lblAsOfDate" runat="server" />
                                        <asp:Label Text='' ID="lblPrevWorkingDay" Visible="false" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:Label Text='<%# Bind("CAL_DATE") %>' ID="lblDate" runat="server" />
                                        <asp:Label Text='<%# Bind("WORKING_DAY") %>' ID="lblWorkingDay" Visible="false" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>

                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gridWeekRoutine" AutoGenerateColumns="false" runat="server">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </asp:Panel>



        <table class="mt-15">
            <tr>
                <td>
                    <asp:Button Text="Save as of Above Date" ID="btnSaveAboveDate" OnClick="btnSaveAboveDate_Click" Visible="false" runat="server" />
                    <asp:Button Text="Save as of Prev Week" ID="btnSavePrevWeek" OnClick="btnSavePrevWeek_Click" Visible="false" runat="server" /></td>
            </tr>
        </table>
        <br />
        <br />
        <br />


        <div id="divupdate" runat="server" visible="false">
            <div id="overlay-div">

                <div id="routine-div" style="background-color: gray">
                    <div class="popup-container-heading">
                        <div class="popup-title">Updating Routine</div>
                        <div class="cancel-control" onclick="clearField();">&#215;</div>
                    </div>
                    <div class="row popup-body">
                        <label class="bg-white">
                            Previous Subject:
                            <asp:Label ID="lblPreviousSubject" runat="server" Visible="true" Text=""></asp:Label>
                        </label>

                        <label class="bg-white">
                            Present Subject:
                            <asp:Label ID="lblPresentSubject" runat="server" Text=""></asp:Label>

                        </label>
                        <br />

                        <label class="label">
                            Remarks:
                        <br />
                            <asp:TextBox ID="txtRemarks" CssClass="m-l-5" runat="server" Width="75%" TextMode="MultiLine" Style="height: 50px; font-size: medium" ForeColor="#000000" Text=""></asp:TextBox>
                        </label>

                    </div>
                    <div class="row popover-content">

                        <label class="">
                            <asp:Button ID="btnConfirm" runat="server" Width="150px" Text="Confirm"
                                OnClick="btnConfirm_Click"
                                Style="height: 25px; font-size: small" ForeColor="#000000" />
                        </label>

                        <label class="label">
                            <asp:Button ID="btnReset" runat="server" Visible="false" Width="150px" Text="Reset"
                                OnClick="btnReset_Click" Style="height: 25px; font-size: small"
                                ForeColor="#000000" />
                        </label>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <script src="../js/jquery-1.11.0.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {

            $('.cancel-control').click(function () {

                $('#overlay-div').removeClass('overlay');

                $('#routine-div').addClass('hidden');
            });


            $('.cancel-control').mouseenter(function () {

                $('.cancel-control').css('color', 'red');
            });




            $('.row').mouseleave(function () {

                $('.cancel-control').css('color', 'black');
            });


        });

    </script>


</asp:Content>





