<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Atten.aspx.cs" Inherits="atten_Attendance" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
        <asp:radiobuttonlist runat="server" cssClass="chooseDate" ID="rbtnChooseDate" AutoPostBack="true"
            OnSelectedIndexChanged="rbtnChooseDate_SelectedIndexChanged"
             repeatdirection="Horizontal" repeatlayout="Flow">
                    <asp:ListItem Value="nepDate" Text="Nepali Date" Selected />
                    <asp:ListItem Value="engDate" Text="English Date" />
                </asp:radiobuttonlist>
   
    <div class="container">
        <table class="gridtable">
            <tr>
                <td>Faculty
                </td>
                <td>
                    <asp:dropdownlist id="ddlFaculty" runat="server" height="22px" autopostback="True" onselectedindexchanged="ddlFaculty_SelectedIndexChanged"></asp:dropdownlist>
                </td>
                <td>Level
                </td>
                <td>
                    <asp:dropdownlist id="ddlLevel" runat="server" height="22px" autopostback="True"></asp:dropdownlist>
                </td>
                <td>Program</td>
                <td>
                    <asp:dropdownlist id="ddlProgram" runat="server" height="22px" autopostback="True" onselectedindexchanged="ddlProgram_SelectedIndexChanged"></asp:dropdownlist>
                </td>
            </tr>

            <tr>
                <td>Semester</td>
                <td>
                    <asp:dropdownlist runat="server" height="22px" id="ddlSemester" autopostback="True" onselectedindexchanged="ddlSemester_SelectedIndexChanged"></asp:dropdownlist>
                </td>

                <td>Batch</td>
                <td>
                    <asp:dropdownlist runat="server" height="22px" id="ddlBatch" autopostback="True"></asp:dropdownlist>
                </td>

                <td>Section
                </td>
                <td>
                    <asp:dropdownlist id="ddlSection" runat="server" height="22px">
                    </asp:dropdownlist>
                </td>
                <td>Teacher
                </td>
                <td>

                    <asp:dropdownlist id="ddlTeacher" runat="server" height="22px" autopostback="True" onselectedindexchanged="ddlTeacher_SelectedIndexChanged">
                    </asp:dropdownlist>
                </td>

                <td>Subject
                </td>
                <td>

                    <asp:dropdownlist id="ddlSubject" runat="server" height="22px" autopostback="True">
                    </asp:dropdownlist>
                </td>

            </tr>
            </table>
        <table class="gridtable">
            <tr visible="true" runat="server" id="trNepDate">
                <td>Date
                </td>
                <td colspan="5">
                    <asp:dropdownlist id="ddlDay" OnSelectedIndexChanged="ddlDay_SelectedIndexChanged" AutoPostBack="true" runat="server" height="22px">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
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
                        <asp:ListItem>22</asp:ListItem>
                        <asp:ListItem>23</asp:ListItem>
                        <asp:ListItem>24</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>26</asp:ListItem>
                        <asp:ListItem>27</asp:ListItem>
                        <asp:ListItem>28</asp:ListItem>
                        <asp:ListItem>29</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                        <asp:ListItem>31</asp:ListItem>
                        <asp:ListItem>32</asp:ListItem>
                    </asp:dropdownlist>
                    <asp:dropdownlist id="ddlMonth" OnSelectedIndexChanged="ddlMonth_SelectedIndexChanged" AutoPostBack="true" runat="server" height="22px">
                    </asp:dropdownlist>
                    <asp:textbox runat="server" ID="txtYear" OnTextChanged="txtYear_TextChanged" AutoPostBack="true" width="50px" height="22px" />
                </td>

            </tr>
            <tr runat="server" visible="false" id="trEngDate">
                <td>Date
                </td>
                <td colspan="5">
                    <asp:dropdownlist id="ddlDayEng" OnSelectedIndexChanged="ddlDayEng_SelectedIndexChanged" AutoPostBack="true" runat="server" height="22px">
                        <asp:ListItem Value="01">1</asp:ListItem>
                        <asp:ListItem Value="02">2</asp:ListItem>
                        <asp:ListItem Value="03">3</asp:ListItem>
                        <asp:ListItem Value="04">4</asp:ListItem>
                        <asp:ListItem Value="05">5</asp:ListItem>
                        <asp:ListItem Value="06">6</asp:ListItem>
                        <asp:ListItem Value="07">7</asp:ListItem>
                        <asp:ListItem Value="08">8</asp:ListItem>
                        <asp:ListItem Value="09">9</asp:ListItem>
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
                        <asp:ListItem>22</asp:ListItem>
                        <asp:ListItem>23</asp:ListItem>
                        <asp:ListItem>24</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>26</asp:ListItem>
                        <asp:ListItem>27</asp:ListItem>
                        <asp:ListItem>28</asp:ListItem>
                        <asp:ListItem>29</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                        <asp:ListItem>31</asp:ListItem>
                    </asp:dropdownlist>
                    <asp:dropdownlist id="ddlMonthEng" OnSelectedIndexChanged="ddlMonthEng_SelectedIndexChanged" AutoPostBack="true" runat="server" height="22px">
                        <asp:ListItem Value="01">January</asp:ListItem>
                        <asp:ListItem Value="02">February</asp:ListItem>
                        <asp:ListItem Value="03">March</asp:ListItem>
                        <asp:ListItem Value="04">April</asp:ListItem>
                        <asp:ListItem Value="05">May</asp:ListItem>
                        <asp:ListItem Value="06">June</asp:ListItem>
                        <asp:ListItem Value="07">July</asp:ListItem>
                        <asp:ListItem Value="08">August</asp:ListItem>
                        <asp:ListItem Value="09">September</asp:ListItem>
                        <asp:ListItem Value="10">October</asp:ListItem>
                        <asp:ListItem Value="11">November</asp:ListItem>
                        <asp:ListItem Value="12">December</asp:ListItem>
                    </asp:dropdownlist>
                    <asp:textbox runat="server" id="txtYearEng" OnTextChanged="txtYearEng_TextChanged" AutoPostBack="true" width="50px" height="22px" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:label text="" ID="lblConvertedDate" runat="server" />
                </td>
            </tr>
            </table>
        <table class="gridtable">

            <tr>
                <td>
                    <asp:button id="btnShow" runat="server" onclick="btnShow_Click" text="Show" />
                </td>
                <td colspan="5">&nbsp;
                <asp:button id="btnSave" runat="server" text="Save" onclick="btnSave_Click" />
                    <asp:button id="btnReset" runat="server" text="Reset"
                        onclick="btnReset_Click" />
                    <asp:button id="btnHideTable" runat="server" text="Hide Entry Table"
                        onclick="btnHideTable_Click" />
                    <asp:button id="btnShowTable" runat="server" text="Show Entry Table"
                        visible="false" onclick="btnShowTable_Click" />
                </td>
            </tr>
        </table>
        <table>
            <tr>
                <td valign="top">
                    <asp:gridview id="grdEntry" runat="server" cssclass="gridtable" autogeneratecolumns="False"
                        font-size="Small" backcolor="White" enablemodelvalidation="True">
                        <AlternatingRowStyle BackColor="#FFCCFF" />
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reg. No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblRegNo" runat="server" Text='<%# Bind("STUDENT_ID") %>'></asp:Label>
                                    <%--<asp:Label ID="lblSubjectPkId" runat="server" Text='<%# Bind("PK_ID") %>' Visible="False"></asp:Label>--%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Width="200px" Text='<%# Bind("name_english") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Attendance">
                                <ItemTemplate>
                                    <asp:RadioButtonList ID="rbtnAttendance" runat="server" RepeatDirection="Horizontal"
                                        Width="50px" TextAlign="Left">
                                        <asp:ListItem Selected="True">P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>

                                    </asp:RadioButtonList>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:gridview>
                </td>
                <td valign="top">
                    <asp:gridview id="grdView" runat="server" cssclass="gridtable" autogeneratecolumns="False" onrowdatabound="grdView_RowDataBound"
                        backcolor="White" onrowediting="grdView_RowEditing"
                        onrowcancelingedit="grdView_RowCancelingEdit"
                        onrowupdating="grdView_RowUpdating">
                        <AlternatingRowStyle BackColor="#FFCCFF" />
                        <Columns>
                            <asp:TemplateField HeaderText="No.">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reg. No">
                                <EditItemTemplate>
                                    <asp:Label ID="lblRegNoE" runat="server" Text='<%# Bind("STUDENT_ID") %>'></asp:Label>
                                    <asp:Label ID="lblID" runat="server" Text='<%# Bind("PK_ID") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblRegNo" runat="server" Text='<%# Bind("STUDENT_ID") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Name">
                                <EditItemTemplate>
                                    <asp:Label ID="lblNameE" runat="server" Width="200px"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblName" runat="server" Width="200px"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="01">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay1" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday1E" runat="server" Text='<%# Bind("day1") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday1" runat="server" Text='<%# Bind("day1") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="02">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay2" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday2E" runat="server" Text='<%# Bind("day2") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday2" runat="server" Text='<%# Bind("day2") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="03">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay3" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday3E" runat="server" Text='<%# Bind("day3") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday3" runat="server" Text='<%# Bind("day3") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="04">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay4" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday4E" runat="server" Text='<%# Bind("day4") %>'
                                        Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday4" runat="server" Text='<%# Bind("day4") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="05">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay5" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday5E" runat="server" Text='<%# Bind("day5") %>'
                                        Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday5" runat="server" Text='<%# Bind("day5") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="06">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay6" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday6E" runat="server" Text='<%# Bind("day6") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday6" runat="server" Text='<%# Bind("day6") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="07">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay7" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday7E" runat="server" Text='<%# Bind("day7") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday7" runat="server" Text='<%# Bind("day7") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="08">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay8" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday8E" runat="server" Text='<%# Bind("day8") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday8" runat="server" Text='<%# Bind("day8") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="09">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay9" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday9E" runat="server" Text='<%# Bind("day9") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday9" runat="server" Text='<%# Bind("day9") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="10">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay10" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday10E" runat="server" Text='<%# Bind("day10") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday10" runat="server" Text='<%# Bind("day10") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="11">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay11" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday11E" runat="server" Text='<%# Bind("day11") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday11" runat="server" Text='<%# Bind("day11") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="12">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay12" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday12E" runat="server" Text='<%# Bind("day12") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday12" runat="server" Text='<%# Bind("day12") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="13">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay13" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday13E" runat="server" Text='<%# Bind("day13") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday13" runat="server" Text='<%# Bind("day13") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="14">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay14" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday14E" runat="server" Text='<%# Bind("day14") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday14" runat="server" Text='<%# Bind("day14") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="15">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay15" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday15E" runat="server" Text='<%# Bind("day15") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday15" runat="server" Text='<%# Bind("day15") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="16">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay16" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday16E" runat="server" Text='<%# Bind("day16") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday16" runat="server" Text='<%# Bind("day16") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="17">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay17" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday17E" runat="server" Text='<%# Bind("day17") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday17" runat="server" Text='<%# Bind("day17") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="18">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay18" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday18E" runat="server" Text='<%# Bind("day18") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday18" runat="server" Text='<%# Bind("day18") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="19">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay19" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday19E" runat="server" Text='<%# Bind("day19") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday19" runat="server" Text='<%# Bind("day19") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="20">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay20" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday20E" runat="server" Text='<%# Bind("day20") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday20" runat="server" Text='<%# Bind("day20") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="21">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay21" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday21E" runat="server" Text='<%# Bind("day21") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday21" runat="server" Text='<%# Bind("day21") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="22">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay22" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday22E" runat="server" Text='<%# Bind("day22") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday22" runat="server" Text='<%# Bind("day22") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="23">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay23" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday23E" runat="server" Text='<%# Bind("day23") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday23" runat="server" Text='<%# Bind("day23") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="24">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay24" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday24E" runat="server" Text='<%# Bind("day24") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday24" runat="server" Text='<%# Bind("day24") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="25">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay25" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday25E" runat="server" Text='<%# Bind("day25") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday25" runat="server" Text='<%# Bind("day25") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="26">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay26" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday26E" runat="server" Text='<%# Bind("day26") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday26" runat="server" Text='<%# Bind("day26") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="27">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay27" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday27E" runat="server" Text='<%# Bind("day27") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday27" runat="server" Text='<%# Bind("day27") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="28">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay28" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday28E" runat="server" Text='<%# Bind("day28") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday28" runat="server" Text='<%# Bind("day28") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="29">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay29" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday29E" runat="server" Text='<%# Bind("day29") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday29" runat="server" Text='<%# Bind("day29") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="30">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay30" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday30E" runat="server" Text='<%# Bind("day30") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday30" runat="server" Text='<%# Bind("day30") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="31">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay31" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday31E" runat="server" Text='<%# Bind("day31") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday31" runat="server" Text='<%# Bind("day31") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="32">
                                <EditItemTemplate>
                                    <asp:RadioButtonList ID="rbtnDay32" runat="server">
                                        <asp:ListItem>P</asp:ListItem>
                                        <asp:ListItem>A</asp:ListItem>
                                        <asp:ListItem>L</asp:ListItem>
                                    </asp:RadioButtonList>
                                    <asp:Label ID="lblday32E" runat="server" Text='<%# Bind("day32") %>' Visible="false"></asp:Label>
                                </EditItemTemplate>
                                <ItemTemplate>
                                    <asp:Label ID="lblday32" runat="server" Text='<%# Bind("day32") %>'></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Total">
                                <ItemTemplate>
                                    <asp:Label ID="lbltotalpresent" runat="server"></asp:Label>/<asp:Label ID="lbltotaldays" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField>
                                <ItemTemplate>

                                    <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="~/images/icons/edit.png" CommandName="Edit" />
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="~/images/icons/upload.png" CommandName="Update" ToolTip="Update" />&nbsp;&nbsp;&nbsp;
                                <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="~/images/icons/delete.gif" CommandName="Cancel" ToolTip="Cancel" />
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:gridview>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>


