<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="PERIOD.aspx.cs" Inherits="administration_PERIOD" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">
        <div class="col-md-6 panel shadow no-border-radius bg-white panel-info">

            <div class="panel-heading">
                <span class="title"><i class="fa fa-font-awesome " aria-hidden="true"></i>&nbsp;Period Form </span>
            </div>
            <div class="panel-body">
                <asp:Label Text="" ID="lblID" Visible="false" runat="server" />
                <table class="gridtable">
                    <tr>
                        <td>Faculty</td>
                        <td>
                            <asp:DropDownList ID="ddlFaculty" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged" AutoPostBack="true" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>Level</td>
                        <td>
                            <asp:DropDownList ID="ddlLevel" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged" AutoPostBack="true" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>Program</td>
                        <td>
                            <asp:DropDownList ID="ddlProgram" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged" Height="22px" AutoPostBack="true" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>Semester</td>
                        <td>
                            <asp:DropDownList ID="ddlSemester" runat="server" Height="22px"></asp:DropDownList>
                        </td>

                    </tr>
                    <tr>
                        <td>Section</td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlSection" Height="22px"></asp:DropDownList>
                        </td>
                        <td>Period</td>
                        <td>
                            <asp:DropDownList ID="ddlPeriod" Height="22px" runat="server">
                                <asp:ListItem>Select</asp:ListItem>
                                <asp:ListItem>First</asp:ListItem>
                                <asp:ListItem>Second</asp:ListItem>
                                <asp:ListItem>Third</asp:ListItem>
                                <asp:ListItem>Fourth</asp:ListItem>
                                <asp:ListItem>Fifth</asp:ListItem>
                                <asp:ListItem>Sixth</asp:ListItem>
                                <asp:ListItem>Break</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        
                        <td colspan="4"></td>
                    </tr>
                    <tr>

                        <td>Time From</td>
                        <td>
                            <asp:DropDownList ID="ddlTimeFromHH" Height="22px" runat="server">
                                <asp:ListItem>HH</asp:ListItem>
                                <%--<asp:ListItem>00</asp:ListItem>
                                <asp:ListItem>01</asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem>03</asp:ListItem>
                                <asp:ListItem>04</asp:ListItem>--%>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>06</asp:ListItem>
                                <asp:ListItem>07</asp:ListItem>
                                <asp:ListItem>08</asp:ListItem>
                                <asp:ListItem>09</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <%--<asp:ListItem>21</asp:ListItem>
                                <asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>--%>
                            </asp:DropDownList>

                            <asp:DropDownList ID="ddlTimeFromMM" Height="22px" runat="server">
                                <asp:ListItem>MM</asp:ListItem>
                                <asp:ListItem>00</asp:ListItem>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>35</asp:ListItem>
                                <asp:ListItem>40</asp:ListItem>
                                <asp:ListItem>45</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>55</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>Time To</td>
                        <td>
                            <asp:DropDownList ID="ddlTimeToHH" Height="22px" runat="server">
                                <asp:ListItem>HH</asp:ListItem>
                                <%--<asp:ListItem>00</asp:ListItem>
                                <asp:ListItem>01</asp:ListItem>
                                <asp:ListItem>02</asp:ListItem>
                                <asp:ListItem>03</asp:ListItem>
                                <asp:ListItem>04</asp:ListItem>--%>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>06</asp:ListItem>
                                <asp:ListItem>07</asp:ListItem>
                                <asp:ListItem>08</asp:ListItem>
                                <asp:ListItem>09</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>11</asp:ListItem>
                                <asp:ListItem>12</asp:ListItem>
                                <asp:ListItem>13</asp:ListItem>
                                <asp:ListItem>14</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>16</asp:ListItem>
                                <asp:ListItem>17</asp:ListItem>
                                <asp:ListItem>18</asp:ListItem>
                                <asp:ListItem>19</asp:ListItem>
                                <asp:ListItem>20</asp:ListItem>
                                <asp:ListItem>21</asp:ListItem>
                                <%--<asp:ListItem>22</asp:ListItem>
                                <asp:ListItem>23</asp:ListItem>
                                <asp:ListItem>24</asp:ListItem>--%>
                            </asp:DropDownList>

                            <asp:DropDownList ID="ddlTimeToMM" Height="22px" runat="server">
                                <asp:ListItem>MM</asp:ListItem>
                                <asp:ListItem>00</asp:ListItem>
                                <asp:ListItem>05</asp:ListItem>
                                <asp:ListItem>10</asp:ListItem>
                                <asp:ListItem>15</asp:ListItem>
                                <asp:ListItem>25</asp:ListItem>
                                <asp:ListItem>30</asp:ListItem>
                                <asp:ListItem>35</asp:ListItem>
                                <asp:ListItem>40</asp:ListItem>
                                <asp:ListItem>45</asp:ListItem>
                                <asp:ListItem>50</asp:ListItem>
                                <asp:ListItem>55</asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:CompareValidator ErrorMessage="Time Defined Invalid" ControlToValidate="ddlTimeFromHH" ControlToCompare="ddlTimeToHH" Type="Integer" Operator="LessThanEqual" runat="server" />
                        </td>
                        <td></td>
                        <td></td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Button Text="Save" ID="btnSave" OnClick="btnSave_Click" runat="server" />
                            <asp:Button Text="Clear" ID="btnClear" OnClick="btnClear_Click" CausesValidation="false" runat="server" />
                        </td>
                        <td>
                            <asp:Label Text="" ID="lblTime" Visible="false" runat="server" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>

        <div class="col-md-6">

            <table class="gridtable">
                <tr>
                    <td>Faculty</td>
                        <td>
                            <asp:DropDownList ID="ddlFacultyG" OnSelectedIndexChanged="ddlFacultyG_SelectedIndexChanged" AutoPostBack="true" runat="server">
                            </asp:DropDownList>
                        </td>
                        <td>Level</td>
                        <td>
                            <asp:DropDownList ID="ddlLevelG" OnSelectedIndexChanged="ddlLevelG_SelectedIndexChanged" AutoPostBack="true" runat="server">
                            </asp:DropDownList>
                        </td>
                    <td>Program</td>
                    <td>
                        <asp:DropDownList ID="ddlProgramG" OnSelectedIndexChanged="ddlProgramG_SelectedIndexChanged" Height="22px" AutoPostBack="true" runat="server">
                        </asp:DropDownList>
                    </td>
                    <td>Semester</td>
                    <td>
                        <asp:DropDownList ID="ddlSemesterG" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlSemesterG_SelectedIndexChanged" Height="22px"></asp:DropDownList>
                    </td>
                    <td>Section</td>
                    <td>
                        <asp:DropDownList runat="server" OnSelectedIndexChanged="ddlSectionG_SelectedIndexChanged" ID="ddlSectionG" AutoPostBack="true" Height="22px"></asp:DropDownList>
                    </td>
                </tr>
            </table>

            <asp:GridView ID="gridPeriod" CssClass="gridtable" OnRowCommand="gridPeriod_RowCommand" Width="300px" AutoGenerateColumns="false" runat="server">
                <Columns>
                    <asp:TemplateField HeaderText="Period">
                        <ItemTemplate>
                            <asp:Label Text='<%# Bind("PK_ID") %>' ID="lblPK" Visible="false" runat="server" />
                            <asp:Label Text='<%# Bind("PERIODS") %>' ID="lblPeriod" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Time">
                        <ItemTemplate>
                            <asp:Label Text='<%# Bind("TIME") %>' ID="lblTime" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Class Hour">
                        <ItemTemplate>
                            <asp:Label Text='<%# Bind("CLASS_HOUR") %>' ID="lblCLASS_HOUR" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:ImageButton CommandName="change" CausesValidation="false" ImageUrl="~/images/icons/edit.png" ToolTip="Edit" runat="server" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

    </div>
</asp:Content>

