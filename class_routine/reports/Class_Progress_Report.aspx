<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Class_Progress_Report.aspx.cs" Inherits="class_routine_reports_Class_Progress_Report" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="container">

        <table class="gridtable">
            <tr>
             <td>
                 Faculty
             </td>
             <td>
                <asp:DropDownList ID="ddlFaculty" runat="server" height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"></asp:DropDownList>
             </td>         
                <td>Level
                </td>
                <td>
                    <asp:DropDownList ID="ddlLevel" runat="server" Height="22px" AutoPostBack="True" >
                        </asp:DropDownList>
                </td>       
           <td>
                 Program
             </td>
            <td>
             
                <asp:DropDownList ID="ddlProgram" runat="server" height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>
            </td>
             </tr>
       <tr>

           <td>Semester</td>
           <td>
               <asp:DropDownList id="ddlSemester" runat="server" height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlSemester_SelectedIndexChanged"></asp:DropDownList>
           </td>  

           <td id="Td1" runat="server" visible="false">
               Batch
           </td>
           <td id="Td2" runat="server" visible="false">
               <asp:DropDownList id="ddlBatch" runat="server" height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"></asp:DropDownList>
               <asp:Label ID="lblPKIDU" runat="server" visible="false"></asp:Label>
           </td>
            <td>Section</td>
           <td>
               <asp:DropDownList id="ddlSection" runat="server" height="22px" AutoPostBack="True" ></asp:DropDownList>
           </td>
           </tr>
            <tr>
                <td>
                     <asp:Button ID="btnView" Text="View" runat="server" OnClick="btnView_Click"></asp:Button>
                </td>
            </tr>
                   
            
        </table>
        <table style="width: 100%;">

            <tr>

                <td>
                    <asp:GridView runat="server" CssClass="gridtable" AutoGenerateColumns="false" Width="100%" EnableModelValidation="True" ID="gridAcademicCalendar" OnRowDataBound="gridAcademicCalendar_RowDataBound">

                        <Columns>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:Label ID="lblMonth" runat="server" Text='<%# Bind("CAL_MONTH") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="lblMonthName" runat="server" Width="15px" Style="font-size: 18pt; font-weight: bold"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />

                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date">
                                <ItemTemplate>
                                    <asp:Label ID="lblDay" runat="server" Text='<%# Bind("CAL_DAY") %>' Width="10px"></asp:Label>


                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Day">
                                <ItemTemplate>
                                    <asp:Label ID="lblDayofWeek" runat="server" Text='<%# Bind("CAL_DAY_OF_WEEK") %>'></asp:Label>


                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Days">
                                <ItemTemplate>
                                    <asp:Label ID="lblDays" runat="server" Text=""></asp:Label>
                                    <asp:Label ID="lblWorkingDays" runat="server" Text='<%# Bind("WORKING_DAY") %>' Visible="false"></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Hr">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubject1" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblClassHr1" runat="server" Text=""></asp:Label>



                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Agg">
                                <ItemTemplate>

                                    <asp:Label ID="lblCumClassHr1" runat="server" Text=""></asp:Label>


                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Hr">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubject2" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblClassHr2" runat="server" Text=""></asp:Label>



                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Agg">
                                <ItemTemplate>

                                    <asp:Label ID="lblCumClassHr2" runat="server" Text=""></asp:Label>


                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Hr">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubject3" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblClassHr3" runat="server" Text=""></asp:Label>



                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Agg">
                                <ItemTemplate>

                                    <asp:Label ID="lblCumClassHr3" runat="server" Text=""></asp:Label>


                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Hr">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubject4" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblClassHr4" runat="server" Text=""></asp:Label>



                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Agg">
                                <ItemTemplate>

                                    <asp:Label ID="lblCumClassHr4" runat="server" Text=""></asp:Label>


                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Hr">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubject5" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblClassHr5" runat="server" Text=""></asp:Label>



                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Agg">
                                <ItemTemplate>

                                    <asp:Label ID="lblCumClassHr5" runat="server" Text=""></asp:Label>


                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Hr">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubject6" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblClassHr6" runat="server" Text=""></asp:Label>



                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Agg">
                                <ItemTemplate>

                                    <asp:Label ID="lblCumClassHr6" runat="server" Text=""></asp:Label>


                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Hr">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubject7" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblClassHr7" runat="server" Text=""></asp:Label>



                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Agg">
                                <ItemTemplate>

                                    <asp:Label ID="lblCumClassHr7" runat="server" Text=""></asp:Label>


                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Hr">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubject8" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblClassHr8" runat="server" Text=""></asp:Label>



                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Agg">
                                <ItemTemplate>

                                    <asp:Label ID="lblCumClassHr8" runat="server" Text=""></asp:Label>


                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Hr">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubject9" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblClassHr9" runat="server" Text=""></asp:Label>



                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Agg">
                                <ItemTemplate>

                                    <asp:Label ID="lblCumClassHr9" runat="server" Text=""></asp:Label>


                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Hr">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubject10" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblClassHr10" runat="server" Text=""></asp:Label>



                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Agg">
                                <ItemTemplate>

                                    <asp:Label ID="lblCumClassHr10" runat="server" Text=""></asp:Label>


                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Hr">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubject11" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblClassHr11" runat="server" Text=""></asp:Label>



                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Agg">
                                <ItemTemplate>

                                    <asp:Label ID="lblCumClassHr11" runat="server" Text=""></asp:Label>


                                </ItemTemplate>
                            </asp:TemplateField>



                            <asp:TemplateField HeaderText="Hr">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubject12" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblClassHr12" runat="server" Text=""></asp:Label>



                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Agg">
                                <ItemTemplate>

                                    <asp:Label ID="lblCumClassHr12" runat="server" Text=""></asp:Label>


                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Hr">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubject13" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblClassHr13" runat="server" Text=""></asp:Label>



                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Agg">
                                <ItemTemplate>

                                    <asp:Label ID="lblCumClassHr13" runat="server" Text=""></asp:Label>


                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Hr">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubject14" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblClassHr14" runat="server" Text=""></asp:Label>



                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Agg">
                                <ItemTemplate>

                                    <asp:Label ID="lblCumClassHr14" runat="server" Text=""></asp:Label>


                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:TemplateField HeaderText="Hr">
                                <ItemTemplate>
                                    <asp:Label ID="lblSubject15" runat="server" Text="" Visible="false"></asp:Label>
                                    <asp:Label ID="lblClassHr15" runat="server" Text=""></asp:Label>



                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Agg">
                                <ItemTemplate>

                                    <asp:Label ID="lblCumClassHr15" runat="server" Text=""></asp:Label>


                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Remarks">
                                <ItemTemplate>
                                    <asp:Label ID="lblRemarks" runat="server" Width="150px" Text='<%# Bind("REMARKS") %>'></asp:Label>

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </td>
            </tr>
        </table>
    </div>
</asp:Content>
