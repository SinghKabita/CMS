<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="subject.aspx.cs" Inherits="forms_subject" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                    <asp:dropdownlist id="ddlLevel" runat="server" height="22px" autopostback="True" onselectedindexchanged="ddlLevel_SelectedIndexChanged"></asp:dropdownlist>
                </td>
                <td>Program
                </td>
                <td>
                    <asp:dropdownlist id="ddlProgram" runat="server" height="22px" autopostback="True" onselectedindexchanged="ddlProgram_SelectedIndexChanged"></asp:dropdownlist>
                </td>
            </tr>
            <tr>
                <td>Syllabus Year
                </td>
                <td>
                    <%--<asp:textbox runat="server" height="22px" id="txtYear" autopostback="True" ontextchanged="txtYear_TextChanged"></asp:textbox>--%>
                    <asp:DropDownList runat="server" ID="ddlSyllabusYr" AutoPostBack="True" OnSelectedIndexChanged="ddlSyllabusYr_SelectedIndexChanged" >           
                    </asp:DropDownList>

                </td>
            </tr>
            <tr>
                <td>Semester</td>
                <td>
                    <asp:dropdownlist runat="server" height="22px" id="ddlSemester" autopostback="True" onselectedindexchanged="ddlSemester_SelectedIndexChanged">
                    </asp:dropdownlist>
                </td>

            </tr>
            <tr id="Tr1" runat="server" visible="false">
                <td>Batch</td>
                <td>
                    <asp:dropdownlist runat="server" height="22px" id="ddlBatch" autopostback="True"></asp:dropdownlist>
                </td>

            </tr>
            <tr>
                <td>
                    <asp:button id="btnView" CausesValidation="false" runat="server" onclick="btnView_Click" text="View" style="width: 42px" />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <table style="width: 100%">
            <tr>
                <td>
                    <asp:gridview id="gridSubject" runat="server" cssclass="gridtable" autogeneratecolumns="False"
                         enablemodelvalidation="True" onrowcancelingedit="gridSubject_RowCancelingEdit" onrowdatabound="gridSubject_RowDataBound"
                         onrowediting="gridSubject_RowEditing" onrowupdating="gridSubject_RowUpdating" >
                     <AlternatingRowStyle BackColor="#FFCCFF" />
                    <Columns>
                        <asp:TemplateField HeaderText="SN">
                            <ItemTemplate>
                                <asp:Label ID="lblSn" runat="server" Text="<%# Container.DataItemIndex+1 %>"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Course Code">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCourseCodeE" runat="server" Text='<%# Bind("COURSE_CODE") %>' Width="100px" height="22px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="id1" runat="server" 
                                 ControlToValidate="txtCourseCodeE" ErrorMessage="Please fill it."></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <HeaderTemplate>
                                Course Code<br />
                                <asp:TextBox ID="txtCourseCodeH" runat="server" height="22px" Width="100px" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="id2" runat="server" 
                                 ControlToValidate="txtCourseCodeH" ErrorMessage="Please fill it."></asp:RequiredFieldValidator>

                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCourseCode" runat="server" Text='<%# Bind("COURSE_CODE") %>' ></asp:Label>
                            </ItemTemplate>

                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Subject Name">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSubjectNameE" runat="server" Text='<%# Bind("SUBJECT_NAME") %>' Width="350px" height="22px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="id3" runat="server" 
                                 ControlToValidate="txtSubjectNameE" ErrorMessage="Please fill it."></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <HeaderTemplate>
                                Subject Name<br />
                                <asp:TextBox ID="txtSubjectNameH" runat="server" height="22px" Width="350px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="id4" runat="server" 
                                 ControlToValidate="txtSubjectNameH" ErrorMessage="Please fill it."></asp:RequiredFieldValidator>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSubjectName" runat="server" Text='<%# Bind("SUBJECT_NAME") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Abbrevation">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtSubjectCodeE" runat="server" Text='<%# Bind("SUBJECT_CODE") %>' height="22px" Width="100px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="id5" runat="server" 
                                 ControlToValidate="txtSubjectCodeE" ErrorMessage="Please fill it."></asp:RequiredFieldValidator>
                                <asp:Label ID="lblPKIDE" runat="server" Text='<%# Bind("PK_ID") %>' Visible="False"></asp:Label>
                            </EditItemTemplate>
                            <HeaderTemplate>
                                Abbrevation<br />
                                <asp:TextBox ID="txtSubjectCodeH" runat="server" height="22px" Width="100px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="id6" runat="server" 
                                 ControlToValidate="txtSubjectCodeH" ErrorMessage="Please fill it."></asp:RequiredFieldValidator>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSubjectCode" runat="server" Text='<%# Bind("SUBJECT_CODE") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Subject Type">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlSubjectTypeE" runat="server" height="22px" >
                                    <asp:ListItem Value="0">Select</asp:ListItem>
                                    <asp:ListItem Value="C">Compulsary</asp:ListItem>
                                    <asp:ListItem Value="E">Elective</asp:ListItem>
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ControlToValidate="ddlSubjectTypeE" ID="RequiredFieldValidator4"
                                ValidationGroup="g1" CssClass="errormesg" ErrorMessage="Please select a type"
                                InitialValue="0" runat="server"  Display="Dynamic">
                                </asp:RequiredFieldValidator>

                                <asp:Label ID="lblSubTypE" runat="server" Text='<%# Bind("OPT") %>' Visible="False"></asp:Label>
                            </EditItemTemplate>
                            <HeaderTemplate>
                                Subject Type<br />
                                <asp:DropDownList ID="ddlSubjectTypeH" runat="server" height="22px">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem Value="C">Compulsary</asp:ListItem>
                                    <asp:ListItem Value="E">Elective</asp:ListItem>
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ControlToValidate="ddlSubjectTypeH" ID="RequiredFieldValidator1"
                                ValidationGroup="g1" CssClass="errormesg" ErrorMessage="Please select a type"
                                InitialValue="0" runat="server"  Display="Dynamic">
                                </asp:RequiredFieldValidator>

                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSubjectType" runat="server"></asp:Label>
                                <asp:Label ID="lblSubTyp" runat="server" Text='<%# Bind("OPT") %>' Visible="False"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Remarks">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlRemarksE" runat="server" height="22px">
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem Value="T">Theory</asp:ListItem>
                                    <asp:ListItem Value="P">Practical</asp:ListItem>
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ControlToValidate="ddlRemarksE" ID="RequiredFieldValidator1"
                                ValidationGroup="g1" CssClass="errormesg" ErrorMessage="Please select a type"
                                InitialValue="0" runat="server"  Display="Dynamic">
                                </asp:RequiredFieldValidator>

                                <asp:Label ID="lblSubRemE" runat="server" Text='<%# Bind("REMARKS") %>' Visible="False"></asp:Label>
                            </EditItemTemplate>
                            <HeaderTemplate>
                                Remarks<br />
                                <asp:DropDownList ID="ddlRemarksH" runat="server" height="22px" >
                                    <asp:ListItem>Select</asp:ListItem>
                                    <asp:ListItem Value="T">Theory</asp:ListItem>
                                    <asp:ListItem Value="P">Practical</asp:ListItem>
                                </asp:DropDownList>

                                <asp:RequiredFieldValidator ControlToValidate="ddlRemarksH" ID="RequiredFieldValidator2"
                                ValidationGroup="g1" CssClass="errormesg" ErrorMessage="Please select a type"
                                InitialValue="0" runat="server"  Display="Dynamic">
                                </asp:RequiredFieldValidator>

                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblSubjectRemarks" runat="server"></asp:Label>
                                <asp:Label ID="lblSubRem" runat="server" Text='<%# Bind("REMARKS") %>' Visible="False"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Group By">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtOrderByE" runat="server" Text='<%# Bind("ORDER_BY") %>' height="22px" Width="75px"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="id7" runat="server" 
                                 ControlToValidate="txtOrderByE" ErrorMessage="Please fill it."></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <HeaderTemplate>
                                Order By<br />
                                <asp:TextBox ID="txtOrderByH" runat="server" height="22px" Width="75px"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="id8" runat="server" 
                                 ControlToValidate="txtOrderByH" ErrorMessage="Please fill it."></asp:RequiredFieldValidator>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblOrderBy" runat="server" Text='<%# Bind("ORDER_BY") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Credit">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtCreditE" runat="server" Text='<%# Bind("CREDIT") %>' height="22px" Width="75px"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="id9" runat="server" 
                                 ControlToValidate="txtCreditE" ErrorMessage="Please fill it."></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <HeaderTemplate>
                                Credit<br />
                                <asp:TextBox ID="txtCreditH" runat="server" height="22px" Width="75px"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="id10" runat="server" 
                                 ControlToValidate="txtCreditH" ErrorMessage="Please fill it."></asp:RequiredFieldValidator>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblCredit" runat="server" Text='<%# Bind("CREDIT") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>

                         <asp:TemplateField HeaderText="Class Hour">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtClassHourE" runat="server" Text='<%# Bind("CLASS_HOUR") %>' height="22px" Width="75px" ></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="id11" runat="server" 
                                 ControlToValidate="txtClassHourE" ErrorMessage="Please fill it."></asp:RequiredFieldValidator>
                            </EditItemTemplate>
                            <HeaderTemplate>
                                Class Hour<br />
                                <asp:TextBox ID="txtClassHourH" runat="server" height="22px" Width="75px"></asp:TextBox>
                                 <asp:RequiredFieldValidator ID="id12" runat="server" 
                                 ControlToValidate="txtClassHourH" ErrorMessage="Please fill it."></asp:RequiredFieldValidator>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lblClassHour" runat="server" Text='<%# Bind("CLASS_HOUR") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField HeaderText="Status">
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlStatusE" runat="server" height="22px">                                  
                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="0">Inactive</asp:ListItem>
                                </asp:DropDownList>

                                <asp:Label ID="lblStatusE" runat="server" Text='<%# Bind("STATUS") %>' Visible="False"></asp:Label>
                            </EditItemTemplate>
                            <HeaderTemplate>
                                Status<br />
                                <asp:DropDownList ID="ddlStatusH" runat="server" height="22px" >
                                    <asp:ListItem Value="1">Active</asp:ListItem>
                                    <asp:ListItem Value="0">Inactive</asp:ListItem>
                                </asp:DropDownList>

                            </HeaderTemplate>
                            <ItemTemplate>

                                <asp:Label ID="lblStatusV" runat="server"></asp:Label>                         
                                <asp:Label ID="lblStatus" runat="server" Text='<%# Bind("STATUS") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>


                        <asp:TemplateField>
                            <EditItemTemplate>
                                <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false" CommandName="Update" ImageUrl="~/images/icons/upload.png" />
                                <asp:ImageButton ID="ImageButton3" runat="server" CausesValidation="false" CommandName="Cancel" ImageUrl="~/images/icons/delete.gif" />
                            </EditItemTemplate>
                            <HeaderTemplate>
                                <asp:Button ID="btnAdd" runat="server" Text="Add" CausesValidation="true" OnClick="btnAdd_Click" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton2" runat="server" CausesValidation="false" CommandName="Edit" ImageUrl="~/images/icons/edit.png" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:gridview>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>


