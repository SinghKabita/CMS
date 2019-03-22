<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="TeacherClassCount.aspx.cs" Inherits="class_routine_reports_TeacherClassCount" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="row">
            <div class="col-md-2">
                <asp:DropDownList ID="ddlTeacher" Height="25px" runat="server">
                </asp:DropDownList>
            </div>
            <div class="col-md-2">
                <asp:TextBox placeholder="..From Date" Height="25px" AutoComplete="off" ID="txtFromDate" runat="server" />
                <cc1:CalendarExtender TargetControlID="txtFromDate" ID="CalendarExtender1"
                    Enabled="True" Format="dd/MM/yyyy" runat="server">
                </cc1:CalendarExtender>
            </div>
            <div class="col-md-2">
                <asp:TextBox placeholder="..To Date" Height="25px" AutoComplete="off" ID="txtToDate" runat="server" />
                <cc1:CalendarExtender TargetControlID="txtToDate" ID="CalendarExtender2"
                    Enabled="True" Format="dd/MM/yyyy" runat="server">
                </cc1:CalendarExtender>
            </div>
            <div class="col-md-2">
                <asp:Button Text="View" ID="btnView" OnClick="btnView_Click" runat="server" />
            </div>
        </div>
        <div class="row mt-15">
            <div class="col-md-12">
                <asp:GridView ID="gridTeacherClass" AutoGenerateColumns="false" CssClass="gridtable" runat="server">
                    <Columns>

                        <asp:TemplateField HeaderText="Program">
                            <ItemTemplate>
                                <asp:Label Text='<%# Bind("Program_code") %>' ID="lblProgramCode" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Semester">
                            <ItemTemplate>
                                <asp:Label Text='<%# Bind("semester_code") %>' ID="lblSemesterCode" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Section">
                            <ItemTemplate>
                                <asp:Label Text='<%# Bind("SECTION") %>' ID="lblSection" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="L/T">
                            <ItemTemplate>
                                <asp:Label Text='<%# Bind("LAB_THEORY") %>' ID="lblLAB_THEORY" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Total Class">
                            <ItemTemplate>
                                <asp:Label Text='<%# Bind("TOTALCLASS") %>' ID="lblTotalClass" runat="server" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>

