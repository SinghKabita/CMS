<%@ Page Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="DropStudent.aspx.cs" Inherits="administration_DropStudent" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function popp() {
            $('#overlay-div').addClass('overlay');
            $('#supplier-div').removeClass('hidden');
            $('.cancel-control').css('color', 'black');

        }
    </script>
    <div class="container">
        <table class="gridtable">
            <tr>
                <td>Faculty
                </td>
                <td>
                    <asp:DropDownList ID="ddlFaculty" runat="server" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"></asp:DropDownList>
                </td>
            </tr>

            <tr>
                <td>Level
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
                    <asp:DropDownList runat="server" Height="22px" ID="ddlSemester" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"></asp:DropDownList></td>
            </tr>

            <tr runat="server" visible="true">
                <td>Batch</td>
                <td>
                    <asp:DropDownList runat="server" Height="22px" ID="ddlBatch" AutoPostBack="True" Enabled="false"></asp:DropDownList></td>
            </tr>
            <tr>
                <td>Section</td>
                <td>
                    <asp:DropDownList runat="server" ID="ddlSection" Height="22px" AutoPostBack="True"></asp:DropDownList></td>

            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnView" runat="server" Text="View" Style="height: 22px;" OnClick="btnView_Click" />
                </td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:GridView ID="griStudentList" runat="server" CssClass="gridtable" Width="50%" AutoGenerateColumns="False" CellPadding="4" EnableModelValidation="True" ForeColor="#333333" GridLines="None" OnRowCommand="griStudentList_RowCommand">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:TemplateField HeaderText="SN">
                                <ItemTemplate>
                                    <asp:Label ID="lblSn" runat="server" Text='<%# Container.DataItemIndex+1 %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Id">
                                <ItemTemplate>
                                    <asp:Label ID="lblStudentId" runat="server" Text='<%# Bind("STUDENT_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Student Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblStudentName" runat="server" Text='<%# Bind("NAME_ENGLISH") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:Button ID="btnDrop" runat="server" Text="Drop" CommandName="Drop" OnClick="btnDrop_Click"></asp:Button>
                                </ItemTemplate>
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
        <br />
        <br />
        <br />

        <div id="divDrop" runat="server" visible="false">
            <div style="font-size: medium">
                Dropping Student
            </div>
            <br />
            <div id="overlay-div" style="background-color: gray">
                <div>
                    Student Name:             
                    <asp:Label ID="lblStudentIdP" runat="server" Width="100%" Text=""></asp:Label>

                </div>
                <div>
                    Semester:
                   
                    <asp:Label ID="lblSemester" runat="server" Text=""></asp:Label>
                </div>
            </div>
            <div>
                Drop Date:
                    
                        <asp:TextBox ID="txtDropDate" runat="server" Width="100%" Style="height: 22px; font-size: medium; z-index: inherit" Text="" AutoPostBack="True" OnTextChanged="txtDropDate_TextChanged"></asp:TextBox>
                <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtDropDate"
                    Enabled="True" Format="dd/MM/yyyy">
                </cc1:CalendarExtender>

                Date (BS):
                    <br />
                <asp:TextBox ID="txtDay" runat="server" Height="22px" Placeholder="DD" Width="30px" AutoPostBack="True" OnTextChanged="txtDay_TextChanged"></asp:TextBox>/
                  <asp:TextBox ID="txtMonth" runat="server" Height="22px" Placeholder="MM" Width="30px" AutoPostBack="True" OnTextChanged="txtMonth_TextChanged"></asp:TextBox>/
                  <asp:TextBox ID="txtYear" runat="server" Height="22px" Placeholder="YYYY" Width="50px" AutoPostBack="True" OnTextChanged="txtYear_TextChanged"></asp:TextBox>

            </div>
            <div>
                Approve By:
                    <br />
                <asp:TextBox ID="txtApproveBy" runat="server" Width="100%" Style="height: 22px; font-size: medium" Text=""></asp:TextBox>

                Fincancial Clearance:
                    <br />
                <asp:RadioButtonList ID="rbtnFinanceClear" runat="server" Width="100%" RepeatDirection="Horizontal">
                    <asp:ListItem Selected="True">Yes</asp:ListItem>
                    <asp:ListItem>No</asp:ListItem>
                </asp:RadioButtonList>


                Reason:
                    <br />
                <asp:TextBox ID="txtReason" runat="server" TextMode="MultiLine"
                    Width="100%" Height="50px"></asp:TextBox>

            </div>

            <div>

                <asp:Button ID="btnConfirm" runat="server" Width="100%" Text="Confirm" OnClick="btnConfirm_Click" />

                <asp:Button ID="btnReset" runat="server" Width="100%" Text="Reset" OnClick="btnReset_Click" />
            </div>
        </div>
    </div>


    <script src="../js/jquery-1.11.0.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {



            //$('#btnAddMore').click(function () {

            //    $('#overlay-div').addClass('overlay');

            //    $('.cancel-control').css('color', 'black');
            //    $('#doctor-div').removeClass('hidden');

            //});

            $('.cancel-control').click(function () {

                $('#overlay-div').removeClass('overlay');

                $('#supplier-div').addClass('hidden');
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

