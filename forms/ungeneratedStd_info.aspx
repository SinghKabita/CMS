<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="ungeneratedStd_info.aspx.cs" Inherits="forms_ungeneratedStd_info" %>



<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .form {
            overflow: hidden;
            background-color: #ddd;
            padding: 1em 1em 1em 1em;
            font-size: .9em;
            border: 1px solid black;
            box-sizing: border-box;
            min-width: 270px;
            margin-bottom: 10px;
        }

        .sub-container {
            background-color: #fff;
            border: 1px solid black;
            margin: auto;
            padding: 5px;
            box-sizing: border-box;
            margin-bottom: 30px;
            box-shadow: 0 0 10px black;
        }

            .sub-container:last-child {
                background-color: #fff;
                border: 1px solid black;
                margin: auto;
                padding: 5px;
                box-sizing: border-box;
                box-shadow: 0 0 5px black;
            }

        .sub-container-heading {
            height: 30px;
            border-bottom: 1px solid black;
            font-size: 18px;
            line-height: 30px;
            text-align: left;
            font-weight: bold;
            padding: 0 5px;
            box-sizing: border-box;
            margin-bottom: 10px;
        }



        .row {
            overflow: hidden;
            box-sizing: border-box;
            margin-left: 3px;
            margin-right: 3px;
            font-size: medium;
        }

        .label {
            text-align: left;
            position: relative;
            display: block;
            /*background-color: tomato;*/
            padding: .25em;
            float: left;
            box-sizing: border-box;
            margin-right: 1em;
            margin-bottom: 1em;
            width: 100%;
            min-width: 200px;
            /*max-width:calc(100% / 2);*/
            font-weight: bold;
            box-sizing: border-box;
            color: black;
        }

        .labelchkbox {
            text-align: left;
            position: relative;
            display: inline-block;
            /*background-color: tomato;*/
            padding: .25em;
            float: left;
            box-sizing: border-box;
            margin-right: 1em;
            margin-bottom: 1em;
            width: 100%;
            min-width: 200px;
            /*max-width:calc(100% / 2);*/
            font-weight: bold;
            box-sizing: border-box;
            color: black;
            font-size: small;
        }


        .labeltitle {
            text-align: left;
            position: relative;
            display: block;
            padding: .25em;
            float: left;
            box-sizing: border-box;
            margin-right: 1em;
            margin-bottom: 1em;
            width: 100%;
            min-width: 200px;
            font-size: 14px;
            font-weight: bold;
            box-sizing: border-box;
            color: #0000ff;
            text-decoration: underline;
        }

        .label > .input {
            display: block;
            padding: .25em;
            width: 100%;
            margin-top: .25em;
            font-family: inherit;
            font-size: inherit;
        }

        .label > input {
            display: block;
            padding: .25em;
            width: 100%;
            margin-top: .25em;
            font-family: inherit;
            font-size: inherit;
        }



        .label > div {
            margin-top: .25em;
            overflow: hidden;
            font-family: inherit;
            font-size: inherit;
            background-color: rgba(255, 255, 255, .2);
        }

            .label > div > div {
                float: left;
                margin-right: .75em;
            }

                .label > div > div:last-child {
                    float: left;
                    margin-right: 0;
                }

        .form > .row > .col1 {
            width: calc((100%  / 1) - 1em);
        }

        .row > .col2 {
            width: calc((100%  / 2) - 1em);
        }

        .row > .col3 {
            width: calc((100%  / 3) - 1em);
        }

        .row > .col4 {
            width: calc((100%  / 4) - 1em);
            top: 0px;
            left: 0px;
        }



        .form > .sub-container > .row > .col5 {
            width: calc((100%  / 8));
            top: 0px;
            left: 0px;
        }

        .form > .row > .col6 {
            width: calc((100%  / 6) - 1em);
        }

        .form > .row > .col7 {
            width: calc((100%  / 7) - 1em);
        }

        .form > .row > .col8 {
            width: calc((100%  / 8) - 1em);
        }

        .form > .row > .col9 {
            width: calc((100%  / 9) - 1em);
        }

        .form > .row > .col10 {
            width: calc((100%  / 10) - 1em);
        }

        .form > .row > .col11 {
            width: calc((100%  / 11) - 1em);
        }

        .form > .row > .col12 {
            width: calc((100%  / 12) - 1em);
        }

        .show-popup {
            top: -500px;
        }
    </style>

    <div class="container">
        <div class="form">


            <div id="employee-detail" class="sub-container">
                <div class="sub-container-heading">Student Detail</div>
                <div class="row">
                    <label class="label col4">
                        <asp:Label ID="lblpkid_hss_Std" Text="" runat="server" Visible="false" />
                    </label>
                </div>
                <div class="row">
                    <label class="label col4">
                        Faculty:<br />
                        <asp:DropDownList ID="ddlFaculty" runat="server" Height="22px" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlFaculty_SelectedIndexChanged"></asp:DropDownList>
                    </label>

                    <label class="label col4">
                        Level:<br />
                        <asp:DropDownList ID="ddlLevel" runat="server" Width="100%" Height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlLevel_SelectedIndexChanged"></asp:DropDownList>
                    </label>

                    <label class="label col4">
                        Program:<br />
                        <asp:DropDownList ID="ddlProgram" runat="server" Height="22px" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlProgram_SelectedIndexChanged"></asp:DropDownList>
                    </label>
                    <label class="label col4">
                        Batch:<br />
                        <asp:DropDownList ID="ddlBatch" runat="server" Height="22px" Width="100%" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"></asp:DropDownList>
                    </label>

                </div>
                <div class="row">

                    <label class="label col4">
                        Full Name:
			            <asp:TextBox ID="txtNAME_ENGLISH" runat="server" Height="22px"></asp:TextBox>
                          <asp:RequiredFieldValidator ID="id13" runat="server"
                            ControlToValidate="txtNAME_ENGLISH" ErrorMessage="Required" SetFocusOnError="true"></asp:RequiredFieldValidator>
                        <br />

                    </label>
                    <label class="label col4">
                        Full Name (Devnagiri):
			            <asp:TextBox ID="txtNAME_DEVANAGARI" runat="server" Height="22px"></asp:TextBox>
                    </label>
                    <label class="label col4">
                        Gender:
                   <asp:RadioButtonList ID="rbtnGENDER" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="rbtnGENDER_SelectedIndexChanged">
                       <asp:ListItem Value="M" Selected="True">Male</asp:ListItem>
                       <asp:ListItem Value="F">Female</asp:ListItem>
                       <asp:ListItem Value="O">Other</asp:ListItem>
                   </asp:RadioButtonList>
                    </label>
                    <label class="label col4">
                    </label>
                </div>

                <div class="row">

                    <label class="label col4">
                        Photo:
			        <asp:FileUpload ID="FileUpload1" runat="server" Height="22px" />

                    </label>

                    <label class="label col4">
                        Marital Status:
                   <asp:RadioButtonList ID="rbtnMaritalStatus" runat="server" RepeatDirection="Horizontal" AutoPostBack="True" OnSelectedIndexChanged="rbtnMaritalStatus_SelectedIndexChanged">
                       <asp:ListItem Value="Unmarried" Selected="True">Unmarried</asp:ListItem>
                       <asp:ListItem Value="Married">Married</asp:ListItem>

                   </asp:RadioButtonList>
                    </label>

                    <label class="label col4">
                        Nationality:
                    <br />
                        <asp:DropDownList ID="ddlNATIONALITY" runat="server" Height="22px" Width="100%">
                        </asp:DropDownList>

                    </label>
                    <label class="label col4">
                        Religion: 
                    <br />
                        <asp:DropDownList ID="ddlRELIGION" runat="server" Height="22px" Width="100%">
                        </asp:DropDownList>


                    </label>
                </div>

                <div class="row">

                    <label class="label col4">
                        DOB [A.D]:
                    
                   <br />
                        <asp:TextBox ID="txtDOB_ADDay" runat="server" Height="22px" Placeholder="DD" Width="15%" Style="display: inline-block" MaxLength="3" AutoPostBack="true" OnTextChanged="txtDOB_ADDay_TextChanged"></asp:TextBox>


                        /
                    <asp:TextBox ID="txtDOB_ADMth" runat="server" Height="22px" Placeholder="MM" Width="15%" Style="display: inline-block" MaxLength="3" AutoPostBack="true" OnTextChanged="txtDOB_ADMth_TextChanged"></asp:TextBox>
                        /
                    <asp:TextBox ID="txtDOB_ADYear" runat="server" Height="22px" Placeholder="YYYY" Width="20%" Style="display: inline-block" MaxLength="5"></asp:TextBox>

                        <asp:Button ID="btnCalcEngtoNep" runat="server" CausesValidation="false" Text="Calculate" Height="22px" Width="30%" Style="display: inline-block" OnClick="btnCalcEngtoNep_Click" />

                    </label>
                    <label class="label col4">
                        DOB [B.S]:
			                   <br />
                        <asp:TextBox ID="txtDOB_BSDay" runat="server" Height="22px" Width="15%" Placeholder="DD" Style="display: inline-block" MaxLength="3"></asp:TextBox>
                        /
                    <asp:TextBox ID="txtDOB_BSMth" runat="server" Height="22px" Width="15%" Placeholder="MM" Style="display: inline-block" MaxLength="3"></asp:TextBox>
                        /
                    <asp:TextBox ID="txtDOB_BSYear" runat="server" Height="22px" Width="20%" Placeholder="YYYY" Style="display: inline-block" MaxLength="5"></asp:TextBox>

                        <asp:Button ID="btnCalcNeptoEng" runat="server" Text="Calculate" CausesValidation="false" Height="22px" Width="30%" Style="display: inline-block" OnClick="btnCalcNeptoEng_Click" />
                    </label>

                    <label class="label col4">
                        Citizenship No:
			                                        <asp:TextBox ID="txtCITIZENSHIP_NO" Height="22px" runat="server"></asp:TextBox>

                    </label>
                    <label class="label col4">
                        Phone No:
                    <asp:TextBox ID="txtPHONE" Height="22px" runat="server"></asp:TextBox>

                    </label>

                </div>

                <div class="row">


                    <label class="label col4">
                        Mobile No_1:
                     <asp:TextBox ID="txtMOBILE_1" Height="22px" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="M1Validation" runat="server"
                            ControlToValidate="txtMOBILE_1" ErrorMessage="only numbers following 10 digits "
                            ValidationExpression="[0-9]{10}">
                        </asp:RegularExpressionValidator>

                    </label>

                    <label class="label col4">
                        Mobile No_2:
                     <asp:TextBox ID="txtMOBILE_2" Height="22px" runat="server"></asp:TextBox>

                        <asp:RegularExpressionValidator ID="M2Validation" runat="server"
                            ControlToValidate="txtMOBILE_2" ErrorMessage="only numbers following 10 digits "
                            ValidationExpression="[0-9]{10}">
                        </asp:RegularExpressionValidator>
                    </label>

                    <label class="label col4">
                        Email: 
                       <asp:TextBox ID="txtEMAIL" Height="22px" runat="server"></asp:TextBox>

                       <%-- <asp:RequiredFieldValidator ID="id13" runat="server"
                            ControlToValidate="txtEMAIL" ErrorMessage="Please enter email"></asp:RequiredFieldValidator>
                        <br />--%>
                        <asp:RegularExpressionValidator ID="id14" runat="server"
                            ControlToValidate="txtEMAIL" ErrorMessage="Please enter correct email"
                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                    </label>


                </div>

                <div class="row">

                    <label class="label col4">
                        <asp:Label ID="lblAdmissionNo" runat="server" Visible="False"></asp:Label>

                    </label>

                    <label class="label col4">
                        <asp:Label ID="lblCurrentStudentPkid" runat="server" Visible="False"></asp:Label>
                    </label>

                    <label class="label col4">
                        <asp:Label ID="lblEducationDetailPkid" runat="server" Visible="False"></asp:Label>
                        <asp:Label ID="lblAttachmentPkid" runat="server" Visible="False"></asp:Label>

                    </label>
                </div>
            </div>


            <div id=" tabpanel" class="sub-container">
                <asp:Panel ID="Panel1" runat="server">
                    <cc1:TabContainer ID="TabContainer1" Width="100%" align="left" runat="server" ActiveTabIndex="0">

                        <cc1:TabPanel runat="server" HeaderText="Address Detail" ID="TabPanel1">
                            <HeaderTemplate>
                                <b>Address Detail</b>
                            </HeaderTemplate>
                            <ContentTemplate>

                                <fieldset style="margin: 15px;" class="sub-container">
                                    <div class="row">
                                        <label class="labeltitle col2">
                                            Permanent Address 
                                        </label>
                                    </div>
                                    <div class="row">
                                        <label class="label col4" id="lblCountry_PA" runat="server">
                                            Country:
                                             <br />
                                            <asp:DropDownList ID="ddlCountryPA" Width="100%" Height="22px" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlCountryPA_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>


                                        <label class="label col4" id="lblState_PA" runat="server">
                                            State:
                                             <br />
                                            <asp:DropDownList ID="ddlStatePA" Width="100%" Height="22px" runat="server" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </label>

                                        <label class="label col4" runat="server" id="lblZone_PA">
                                            Zone:
                                             <br />
                                            <asp:DropDownList ID="ddlPA_Zone" Width="100%" Height="22px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPA_Zone_SelectedIndexChanged"></asp:DropDownList>
                                        </label>
                                        <label class="label col4" runat="server" id="lblDistrict_PA">
                                            District:
                    <br />
                                            <asp:DropDownList ID="ddlPA_DISTRICT" Width="100%" Height="22px" runat="server"></asp:DropDownList>
                                        </label>


                                    </div>

                                    <div class="row">

                                        <label class="label col4">
                                            VDC/Municipality:
			        <asp:TextBox ID="txtPA_VDC_MUNI" Width="100%" Height="22px" runat="server"></asp:TextBox>

                                        </label>
                                        <label class="label col4">
                                            Ward No:
			 <asp:TextBox ID="txtPA_WARD_NO" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>

                                        <label class="label col4">
                                            Street Name:
			 <asp:TextBox ID="txtPA_STREET" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                        <label class="label col4">
                                            House No:
			 <asp:TextBox ID="txtPA_HOUSE_NO" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>

                                    </div>
                                </fieldset>

                                <fieldset style="margin: 15px;" class="sub-container">

                                    <div class="row">
                                        <label class="labeltitle col4">
                                            Contact Address 
                                        </label>
                                        <label class="label col4" id="lblSameAsOf" runat="server">
                                            Same as of &nbsp;&nbsp;<asp:DropDownList ID="ddlContactAddress" runat="server" Width="100%" Height="22px"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlContactAddress_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="Select" />
                                                <asp:ListItem Text="Parmenent Address" Value="Parmenent Address" />


                                            </asp:DropDownList>

                                        </label>

                                    </div>
                                    <div class="row">
                                        <label class="label col4" id="lblCountryCA" runat="server">
                                            Country:
                                             <br />
                                            <asp:DropDownList ID="ddlCountry_CA" Width="100%" Height="22px" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlCountry_CA_SelectedIndexChanged" Enabled="true">
                                            </asp:DropDownList>
                                        </label>


                                        <label class="label col4" id="lblState_CA" runat="server">
                                            State:
                                             <br />
                                            <asp:DropDownList ID="ddlState_CA" Width="100%" Height="22px" runat="server"
                                                AutoPostBack="True" Enabled="true">
                                            </asp:DropDownList>
                                        </label>


                                        <label class="label col4" runat="server" id="lblZone_CA">
                                            Zone:
                    <br />
                                            <asp:DropDownList ID="ddlTA_Zone" Width="100%" Height="22px" runat="server"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlTA_Zone_SelectedIndexChanged" Enabled="true">
                                            </asp:DropDownList>


                                        </label>
                                        <label class="label col4" runat="server" id="lblDistrict_CA">
                                            District:
                    <br />
                                            <asp:DropDownList ID="ddlTA_DISTRICT" Width="100%" Height="22px" runat="server" Enabled="true"></asp:DropDownList>
                                        </label>

                                    </div>

                                    <div class="row">
                                        <label class="label col4">
                                            VDC/Municipality:
			        <asp:TextBox ID="txtTA_VDC_MUNI" Width="100%" Height="22px" runat="server"></asp:TextBox>

                                        </label>
                                        <label class="label col4">
                                            Ward No:
			 <asp:TextBox ID="txtTA_WARD_NO" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>

                                        <label class="label col4">
                                            Street Name:
			 <asp:TextBox ID="txtTA_STREET" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                        <label class="label col4">
                                            House No:
			 <asp:TextBox ID="txtTA_HOUSE_NO" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>

                                    </div>
                                </fieldset>

                            </ContentTemplate>
                        </cc1:TabPanel>

                        <cc1:TabPanel runat="server" HeaderText="Parental Information" ID="TabPanel2">
                            <HeaderTemplate>
                                <b>Parental Information</b>
                            </HeaderTemplate>
                            <ContentTemplate>
                                <fieldset style="margin: 15px;" class="sub-container">
                                    <div class="row">
                                        <label class="labeltitle col2">
                                            Father's Information
                                        </label>
                                    </div>
                                    <div class="row">
                                        <label class="label col4">
                                            Father's Name:
			 <asp:TextBox ID="txtF_NAME" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>

                                        <label class="label col4">
                                            Address:
			 <asp:TextBox ID="txtF_ADDRESS" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>

                                        <label class="label col4">
                                            Phone No:
			 <asp:TextBox ID="txtF_PHONE" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                        <label class="label col4">
                                            Mobile No_1:
			 <asp:TextBox ID="txtF_MOBILE1" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="M1Validation_F" runat="server"
                                                ControlToValidate="txtF_MOBILE1" ErrorMessage="only numbers following 10 digits "
                                                ValidationExpression="[0-9]{10}">
                                            </asp:RegularExpressionValidator>

                                        </label>


                                    </div>

                                    <div class="row">

                                        <label class="label col4">
                                            Mobile No_2:
			 <asp:TextBox ID="txtF_MOBILE2" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="M2Validation_F" runat="server"
                                                ControlToValidate="txtF_MOBILE2" ErrorMessage="only numbers following 10 digits "
                                                ValidationExpression="[0-9]{10}">
                                            </asp:RegularExpressionValidator>
                                        </label>
                                        <label class="label col4">
                                            Email:
			 <asp:TextBox ID="txtF_EMAIL" Width="100%" Height="22px" runat="server"></asp:TextBox>

                                            <asp:RegularExpressionValidator ID="emailValidationF" runat="server"
                                                ControlToValidate="txtF_EMAIL" ErrorMessage="Please enter correct email"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                                        </label>
                                        <label class="label col4">
                                            Occupation:
			 <asp:TextBox ID="txtF_OCCUPATION" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>

                                        <label class="label col4">
                                            Designation:
			 <asp:TextBox ID="txtF_Position" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>


                                    </div>
                                    <div class="row">

                                        <label class="label col4">
                                            Name of the Company/Office:
			 <asp:TextBox ID="txtF_Office" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>

                                        <label class="label col4">
                                            Office Phone No:
			 <asp:TextBox ID="txtF_Office_Phone" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                    </div>

                                </fieldset>
                                <fieldset style="margin: 15px;" class="sub-container">
                                    <div class="row">
                                        <label class="labeltitle col2">
                                            Mother's Information
                                        </label>
                                    </div>
                                    <div class="row">
                                        <label class="label col4">
                                            Mother's Name:
			 <asp:TextBox ID="txtM_NAME" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>

                                        <label class="label col4">
                                            Address:
			 <asp:TextBox ID="txtM_ADDRESS" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>

                                        <label class="label col4">
                                            Phone No:
			 <asp:TextBox ID="txtM_PHONE" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                        <label class="label col4">
                                            Mobile No_1:
			 <asp:TextBox ID="txtM_MOBILE1" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="r1" runat="server"
                                                ControlToValidate="txtM_MOBILE1" ErrorMessage="only numbers following 10 digits "
                                                ValidationExpression="[0-9]{10}">
                                            </asp:RegularExpressionValidator>
                                        </label>

                                    </div>

                                    <div class="row">

                                        <label class="label col4">
                                            Mobile No_2:
			 <asp:TextBox ID="txtM_MOBILE2" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="r2" runat="server"
                                                ControlToValidate="txtM_MOBILE2" ErrorMessage="only numbers following 10 digits "
                                                ValidationExpression="[0-9]{10}">
                                            </asp:RegularExpressionValidator>
                                        </label>

                                        <label class="label col4">
                                            Email:
			 <asp:TextBox ID="txtM_EMAIL" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="emailValidationM" runat="server"
                                                ControlToValidate="txtM_EMAIL" ErrorMessage="Please enter correct email"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                                        </label>
                                        <label class="label col4">
                                            Occupation:
			 <asp:TextBox ID="txtM_OCCUPATION" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>

                                        <label class="label col4">
                                            Designation:
			 <asp:TextBox ID="txtM_POSITION" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>


                                    </div>
                                    <div class="row">
                                        <label class="label col4">
                                            Name of the Company/Office:
			 <asp:TextBox ID="txtM_OFFICE" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                        <label class="label col4">
                                            Office Phone No:
			 <asp:TextBox ID="txtM_OFFICE_PHONE" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                    </div>

                                </fieldset>


                                <fieldset id="spouse_fieldset" runat="server" visible="false" style="margin: 15px;" class="sub-container">

                                    <div id="spouse_detail" class="row" runat="server" visible="true">
                                        <label class="labeltitle col2">
                                            Spouse's Information
                                        </label>
                                    </div>
                                    <div id="spouse_detail1" class="row" runat="server" visible="true">
                                        <label class="label col4">
                                            Spouse's Name:
			 <asp:TextBox ID="txtS_NAME" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>


                                        <label class="label col4">
                                            Address:
			 <asp:TextBox ID="txtS_ADDRESS" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                        <label class="label col4">
                                            Phone No:
			 <asp:TextBox ID="txtS_PHONE" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                        <label class="label col4">
                                            Mobile No_1:
			 <asp:TextBox ID="txtS_MOBILE1" Height="22px" runat="server"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="r3" runat="server"
                                                ControlToValidate="txtS_MOBILE1" ErrorMessage="only numbers following 10 digits "
                                                ValidationExpression="[0-9]{10}">
                                            </asp:RegularExpressionValidator>
                                        </label>
                                    </div>

                                    <div id="spouse_detail2" class="row" runat="server" visible="true">

                                        <label class="label col4">
                                            Mobile No_2:
			 <asp:TextBox ID="txtS_MOBILE2" Height="22px" runat="server"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="r4" runat="server"
                                                ControlToValidate="txtS_MOBILE2" ErrorMessage="only numbers following 10 digits "
                                                ValidationExpression="[0-9]{10}">
                                            </asp:RegularExpressionValidator>
                                        </label>

                                        <label class="label col4">
                                            Email:
			 <asp:TextBox ID="txtS_EMAIL" Height="22px" runat="server"></asp:TextBox>

                                            <asp:RegularExpressionValidator ID="emailValidationS" runat="server"
                                                ControlToValidate="txtS_EMAIL" ErrorMessage="Please enter correct email"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                        </label>
                                        <label class="label col4">
                                            Occupation:
			 <asp:TextBox ID="txtS_OCCUPATION" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>

                                        <label class="label col4">
                                            Designation:
			 <asp:TextBox ID="txtS_POSITION" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>

                                    </div>
                                    <div class="row">

                                        <label class="label col4">
                                            Name of the Company/Office:
			 <asp:TextBox ID="txtS_OFFICE" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>

                                        <label class="label col4">
                                            Office Phone No:
			 <asp:TextBox ID="txtS_OFFICE_PHONE" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                    </div>
                                </fieldset>


                                <fieldset style="margin: 15px;" class="sub-container">

                                    <div class="row">
                                        <label class="labeltitle col4">
                                            Guardian's Information
                                        </label>
                                        <label class="label col4">
                                            Same as of &nbsp;&nbsp;<asp:DropDownList ID="ddlGuardian" runat="server" Width="50%" Height="22px"
                                                AutoPostBack="True" OnSelectedIndexChanged="ddlGuardian_SelectedIndexChanged">
                                                <asp:ListItem Text="Select" Value="Select" />
                                                <asp:ListItem Text="Father" Value="Father" />
                                                <asp:ListItem Text="Mother" Value="Mother" />


                                            </asp:DropDownList>
                                        </label>

                                    </div>
                                    <div class="row">
                                        <label class="label col4">
                                            Guardian's Name:
			 <asp:TextBox ID="txtG_NAME" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                        <label class="label col4">
                                            Relation:
			 <asp:TextBox ID="txtG_RELATION" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>

                                        <label class="label col4">
                                            Address:
			 <asp:TextBox ID="txtG_ADDRESS" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                        <label class="label col4">
                                            Phone No:
			 <asp:TextBox ID="txtG_PHONE" Height="22px" runat="server"></asp:TextBox>
                                        </label>

                                    </div>
                                    <div class="row">

                                        <label class="label col4">
                                            Mobile No_1:
			 <asp:TextBox ID="txtG_MOBILE1" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                        <label class="label col4">
                                            Mobile No_2:
			 <asp:TextBox ID="txtG_MOBILE2" Height="22px" runat="server"></asp:TextBox>
                                        </label>

                                        <label class="label col4">
                                            Email:
			 <asp:TextBox ID="txtG_EMAIL" Height="22px" runat="server"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="emailValidationG" runat="server"
                                                ControlToValidate="txtG_EMAIL" ErrorMessage="Please enter correct email"
                                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>

                                        </label>
                                        <label class="label col4">
                                            Occupation:
			 <asp:TextBox ID="txtG_OCCUPATION" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>


                                    </div>
                                    <div class="row">

                                        <label class="label col4">
                                            Designation:
			 <asp:TextBox ID="txtG_POSITION" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>

                                        <label class="label col4">
                                            Name of the Company/Office:
			 <asp:TextBox ID="txtG_OFFICE" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                        <label class="label col4">
                                            Office Phone No:
			 <asp:TextBox ID="txtG_OFFICE_PHONE" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                    </div>


                                </fieldset>


                            </ContentTemplate>
                        </cc1:TabPanel>

                        <cc1:TabPanel runat="server" HeaderText="Education Detail" ID="TabPanel3">
                            <HeaderTemplate>
                                <b>Educational Detail</b>
                            </HeaderTemplate>
                            <ContentTemplate>
                                <fieldset style="margin: 15px;" class="sub-container">
                                    <div class="row">
                                        <label class="labeltitle col2" id="lblSchLevel" runat="server">SLC Information</label>
                                    </div>
                                    <div class="row">
                                        <label class="label col4">
                                            Board:
			                                <asp:TextBox ID="txtBOARD" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                        <label class="label col4">
                                            School:
			                                <asp:TextBox ID="txtSCHOOL" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                        <label class="label col4">
                                            Country:
                                            <br />
                                            <asp:DropDownList ID="ddlCountrySLC" Width="100%" Height="22px" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlCountry_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>

                                        <label class="label col4" runat="server" id="lblCountryAdd" visible="false">
                                            Address:
                                            <asp:TextBox ID="txtcountryAddSLC" Width="100%" Height="22px" runat="server"></asp:TextBox>

                                        </label>


                                    </div>

                                    <div class="row">

                                        <label class="label col4" id="lblStateSLC" runat="server">
                                            State:
                    <br />
                                            <asp:DropDownList ID="ddlState_SLC" Width="100%" Height="22px" runat="server"></asp:DropDownList>
                                        </label>

                                        <label class="label col4" id="lblZoneSLC" runat="server">
                                            Zone:
                    <br />
                                            <asp:DropDownList ID="ddlZone_School" Width="100%" Height="22px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlZone_School_SelectedIndexChanged"></asp:DropDownList>


                                        </label>
                                        <label class="label col4" id="lblDistrictSLC" runat="server">
                                            District:
                    <br />
                                            <asp:DropDownList ID="ddlDistrict_School" Width="100%" Height="22px" runat="server"></asp:DropDownList>
                                        </label>
                                    </div>
                                    <div class="row">
                                        <label class="label col4">
                                            Passed Year:
			 <asp:TextBox ID="txtPASSED_YEAR" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                        <label class="label col4">
                                            Passed Division:
			 <asp:TextBox ID="txtDIVISION" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                        <label class="label col4">
                                            Percentange/GPA:
			 <asp:TextBox ID="txtPERCENTAGE" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                        <label class="label col4">
                                            Symbol No:
			 <asp:TextBox ID="txtSYMBOLE_NO" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                    </div>


                                    <div class="row">
                                        <label class="label col4">
                                            Opt Subject 1:
                    <asp:TextBox ID="txtOPT_SUBJ1" runat="server" Height="22px"></asp:TextBox>
                                        </label>
                                        <label class="label col4">
                                            Opt Subject 2:
			 <asp:TextBox ID="txtOPT_SUBJ2" runat="server" Height="22px"></asp:TextBox>
                                        </label>

                                        <label class="label col4">
                                            Opt Subject 3:
			 <asp:TextBox ID="txtOPT_SUBJ3" runat="server" Height="22px"></asp:TextBox>
                                        </label>

                                    </div>
                                </fieldset>

                                <fieldset style="margin: 15px;" class="sub-container">
                                    <div class="row">
                                        <label class="labeltitle col2" id="lblPTLevel" runat="server">+2 Information</label>
                                    </div>
                                    <div class="row">
                                        <label class="label col4">
                                            Board:
			 <asp:TextBox ID="txtPTBoard" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                        <label class="label col4">
                                            College:
			 <asp:TextBox ID="txtCOLLEGE" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>

                                        <label class="label col4">
                                            Country:
                                            <br />
                                            <asp:DropDownList ID="ddlCountryPT" Width="75%" Height="22px" runat="server" AutoPostBack="True"
                                                OnSelectedIndexChanged="ddlCountryPT_SelectedIndexChanged">
                                            </asp:DropDownList>
                                        </label>

                                        <label class="label col4" runat="server" id="lblCountryAddPT" visible="false">
                                            Address:
                                            <asp:TextBox ID="txtCountryAddPT" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                    </div>
                                    <div class="row">
                                        <label class="label col4" id="lblStatePT" runat="server">
                                            State:
                    <br />
                                            <asp:DropDownList ID="ddlState_PT" Width="100%" Height="22px" runat="server" AutoPostBack="True"></asp:DropDownList>

                                        </label>

                                        <label class="label col4" id="lblZonePT" runat="server">
                                            Zone:
                    <br />
                                            <asp:DropDownList ID="ddlZone_PT" Width="100%" Height="22px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlZone_PT_SelectedIndexChanged"></asp:DropDownList>

                                        </label>
                                        <label class="label col4" id="lblDistrictPT" runat="server">
                                            District:
                    <br />
                                            <asp:DropDownList ID="ddlDistrict_PT" Width="100%" Height="22px" runat="server"></asp:DropDownList>
                                        </label>

                                        <label class="label col4">
                                            Faculty:
                                            <br />
                                            <asp:DropDownList ID="ddlFacultyPT" Width="100%" Height="22px" runat="server">
                                                <asp:ListItem>Management</asp:ListItem>
                                                <asp:ListItem>Science</asp:ListItem>
                                                <asp:ListItem>Humanities</asp:ListItem>
                                                <asp:ListItem>Education</asp:ListItem>
                                                <asp:ListItem>Arts</asp:ListItem>
                                            </asp:DropDownList>
                                        </label>

                                    </div>

                                    <div class="row">
                                        <label class="label col4">
                                            Passed Year:
			 <asp:TextBox ID="txtPTPASSED_YEAR" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                        <label class="label col4">
                                            Passed Division:
			 <asp:TextBox ID="txtPTDIVISION" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                        <label class="label col4">
                                            Percentange/GPA:
			 <asp:TextBox ID="txtPTPERCENTAGE" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                        <label class="label col4">
                                            Symbol No:
			 <asp:TextBox ID="txtPTSYMBOLE_NO" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                    </div>

                                    <div class="row">
                                        <label class="label col4">
                                            Opt Subject 1:
                   <asp:TextBox ID="txtPTOPT_SUBJ1" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                        <label class="label col4">
                                            Opt Subject 2:
			 <asp:TextBox ID="txtPTOPT_SUBJ2" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>

                                        <label class="label col4">
                                            Opt Subject 3:
			 <asp:TextBox ID="txtPTOPT_SUBJ3" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>

                                    </div>
                                </fieldset>

                                <div id="divBachelorInfo" runat="server" visible="false">
                                    <fieldset style="margin: 15px;" class="sub-container">
                                        <div class="row">
                                            <label class="labeltitle col2" id="lblBLevel" runat="server">Bachelor Information</label>

                                        </div>
                                        <div class="row">
                                            <label class="label col4">
                                                University:
			 <asp:TextBox ID="txtUniversity" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                            </label>
                                            <label class="label col4">
                                                College:
			 <asp:TextBox ID="txtCollege_B" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                            </label>

                                            <label class="label col4">
                                                Country:
                                            <br />
                                                <asp:DropDownList ID="ddlCountryB" Width="75%" Height="22px" runat="server" AutoPostBack="True"
                                                    OnSelectedIndexChanged="ddlCountryB_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </label>

                                            <label class="label col4" runat="server" id="lblCountryAddB" visible="false">
                                                Address:
                                            <asp:TextBox ID="txtCountryAddB" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                            </label>


                                        </div>
                                        <div class="row">
                                            <label class="label col4" id="lblStateB" runat="server">
                                                State:
                    <br />
                                                <asp:DropDownList ID="ddlState_B" Width="100%" Height="22px" runat="server" AutoPostBack="True"></asp:DropDownList>
                                            </label>

                                            <label class="label col4" id="lblZoneB" runat="server">
                                                Zone:
                    <br />
                                                <asp:DropDownList ID="ddlZone_B" Width="100%" Height="22px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlZone_B_SelectedIndexChanged"></asp:DropDownList>
                                            </label>
                                            <label class="label col4" id="lblDistrictB" runat="server">
                                                District:
                    <br />
                                                <asp:DropDownList ID="ddlDistrict_B" Width="100%" Height="22px" runat="server"></asp:DropDownList>
                                            </label>
                                        </div>
                                        <div class="row">
                                            <label class="label col4">
                                                Faculty:
                                            <br />
                                                <asp:DropDownList ID="ddlFaculty_B" Width="100%" Height="20px" runat="server">
                                                    <asp:ListItem>Management</asp:ListItem>
                                                    <asp:ListItem>Science</asp:ListItem>
                                                    <asp:ListItem>Humanities</asp:ListItem>
                                                    <asp:ListItem>Education</asp:ListItem>
                                                    <asp:ListItem>Arts</asp:ListItem>
                                                </asp:DropDownList>

                                            </label>

                                            <label class="label col4">
                                                Program:
			        <asp:TextBox ID="txtBProgram" Width="100%" Height="22px" runat="server"></asp:TextBox>

                                            </label>
                                        </div>

                                        <div class="row">
                                            <label class="label col4">
                                                Passed Year:
			 <asp:TextBox ID="txtPassedyear_B" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                            </label>
                                            <label class="label col4">
                                                Passed Division:
			 <asp:TextBox ID="txtDivision_B" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                            </label>
                                            <label class="label col4">
                                                Percentange/GPA:
			 <asp:TextBox ID="txtPercentage_B" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                            </label>
                                            <label class="label col4">
                                                Symbol No:
			 <asp:TextBox ID="txtSymbol_B" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                            </label>

                                        </div>


                                        <div class="row">
                                            <label class="label col4">
                                                Opt Subject 1:
                   <asp:TextBox ID="txtSub1_B" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                            </label>
                                            <label class="label col4">
                                                Opt Subject 2:
			 <asp:TextBox ID="txtSub2_B" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                            </label>

                                            <label class="label col4">
                                                Opt Subject 3:
			 <asp:TextBox ID="txtSub3_B" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                            </label>

                                        </div>
                                    </fieldset>
                                </div>



                            </ContentTemplate>
                        </cc1:TabPanel>

                        <cc1:TabPanel runat="server" HeaderText="Document Attached" ID="TabPanel4">
                            <HeaderTemplate>
                                <b>Document Submitted</b>
                            </HeaderTemplate>
                            <ContentTemplate>

                                <div class="row">

                                    <label class="labelchkbox col3">
                                        <asp:CheckBox ID="chkPHOTO" runat="server" Text="Photo" />
                                    </label>
                                    <label class="labelchkbox col3">
                                        <asp:CheckBox ID="chkTRANSCRIPT" runat="server" Text="SLC Transcript Photocopy" />
                                    </label>
                                    <label class="labelchkbox col3">

                                        <asp:CheckBox ID="chkCHARACTER_CERTIFICATE" runat="server" Text="SLC Character Certificate Photocopy" />
                                    </label>
                                </div>


                                <div class="row">
                                    <label class="labelchkbox col3">
                                        <asp:CheckBox ID="chkPTTRANSCRIPT" runat="server" Text="+2 Transcript Photocopy" />
                                    </label>

                                    <label class="labelchkbox col3">
                                        <asp:CheckBox ID="chkPTCHARACTER_CERTIFICATE" runat="server" Text="+2 Character Certificate Photocopy" />
                                    </label>
                                    <label class="labelchkbox col3">
                                        <asp:CheckBox ID="chkCITIZENSHIP" runat="server" Text="Citizenship Photocopy" />
                                    </label>



                                </div>

                                <div class="row">
                                    <label class="labelchkbox col3">
                                        <asp:CheckBox ID="chkCERTIFICATE" runat="server" Text="Government Certificate Photocopy" />
                                    </label>

                                    <label class="labelchkbox col3">
                                        <asp:CheckBox ID="chkPROVISIONCERTIFICATE" runat="server" Text="+2 Provisional/Orginal Certificate" />
                                    </label>

                                    <label class="labelchkbox col3">
                                        <asp:CheckBox ID="chkMIGRATIONCERTIFICATE" runat="server" Text="+2 Migration Certificate" />
                                    </label>
                                </div>


                                <div class="row" id="divBachelorDocument" runat="server" visible="false">
                                    <label class="labelchkbox col3">
                                        <asp:CheckBox ID="chkBTranscript" runat="server" Text="Bachelor Transcript" />
                                    </label>

                                    <label class="labelchkbox col3">
                                        <asp:CheckBox ID="chkBCharacterCertificate" runat="server" Text="Bachelor Character Certificate" />
                                    </label>

                                    <label class="labelchkbox col3">
                                        <asp:CheckBox ID="chkBMigrationCertificate" runat="server" Text="Bachelor Migration Certificate" />
                                    </label>
                                </div>

                            </ContentTemplate>
                        </cc1:TabPanel>


                        <cc1:TabPanel runat="server" HeaderText="Other Detail" ID="TabPanel5">
                            <HeaderTemplate>
                                <b>Notification Detail</b>
                            </HeaderTemplate>
                            <ContentTemplate>

                                <div class="row">
                                    <label class="labelchkbox col4">
                                        Primary Number:
                                        <br />
                                        <asp:TextBox ID="txtSMSAlert1" Height="22px" Width="200px" runat="server"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="PNoValidation" runat="server"
                                            ControlToValidate="txtSMSAlert1" ErrorMessage="only numbers following 10 digits"
                                            ValidationExpression="[0-9]{10}">
                                        </asp:RegularExpressionValidator>
                                    </label>
                                    <label class="labelchkbox col4">
                                        <asp:CheckBox ID="chkSMSAlert1Status" runat="server" />
                                        Primary Alert
                                       
                                    </label>
                                </div>

                                <div class="row">
                                    <label class="labelchkbox col4">
                                        Secondary Number:
                                        <br />
                                        <asp:TextBox ID="txtSMSAlert2" Height="22px" Width="200px" runat="server"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="SNoValidation" runat="server"
                                            ControlToValidate="txtSMSAlert2" ErrorMessage="only numbers following 10 digits"
                                            ValidationExpression="[0-9]{10}">
                                        </asp:RegularExpressionValidator>

                                    </label>
                                    <label class="labelchkbox col4">
                                        <asp:CheckBox ID="chkSMSAlert2Status" runat="server" />
                                        Secondary Alert
                                        
                                    </label>
                                </div>

                                <div class="row">
                                    <label class="labelchkbox col4">
                                        Primary Email:
                                        <br />
                                        <asp:TextBox ID="txtEmailAlert1" Height="22px" Width="200px" runat="server"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="P_Email1" runat="server"
                                            ControlToValidate="txtEmailAlert1" ErrorMessage="Please enter correct email"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                        </asp:RegularExpressionValidator>
                                    </label>
                                    <label class="labelchkbox col4">
                                        <asp:CheckBox ID="chkEmailAlert1Status" runat="server" />
                                        Primary Email Alert
                                        
                                    </label>
                                </div>

                                <div class="row">
                                    <label class="labelchkbox col4">
                                        Secondary Email:
                                        
                                        <asp:TextBox ID="txtEmailAlert2" Height="22px" Width="200px" runat="server"></asp:TextBox>
                                        <br />
                                        <asp:RegularExpressionValidator ID="P_Email2" runat="server"
                                            ControlToValidate="txtEmailAlert2" ErrorMessage="Please enter correct email"
                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                                        </asp:RegularExpressionValidator>

                                    </label>
                                    <label class="labelchkbox col4">
                                        <asp:CheckBox ID="chkEmailAlert2Status" runat="server" />
                                        Secondary Email Alert
                                        
                                    </label>
                                </div>
                            </ContentTemplate>
                        </cc1:TabPanel>


                        <cc1:TabPanel runat="server" ID="TabPanel6">
                            <HeaderTemplate>
                                <b>For Official Use</b>
                            </HeaderTemplate>
                            <ContentTemplate>
                                <fieldset style="margin: 15px;" class="sub-container">
                                    <div class="row">
                                        <label class="labeltitle col2">
                                            Remarks
                                        </label>
                                    </div>
                                    <div class="row">
                                        <label class="label col1">
                                            List of Intrests and future plan after graduation?<br />
                                            <asp:TextBox ID="txtRemarks" Width="100%" Height="200px" TextMode="MultiLine" runat="server"></asp:TextBox>
                                        </label>
                                    </div>


                                </fieldset>

                                <fieldset style="margin: 15px;" class="sub-container">
                                    <div class="row">
                                        <label class="labeltitle col2">
                                            For Official Use Only
                                        </label>
                                    </div>
                                    <div class="row">
                                        <label class="label col3">
                                            Entrance Roll No:
			 <asp:TextBox ID="txtEntranceRollNo" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                        <label class="label col3">
                                            Entrance Date:
			 <asp:TextBox ID="txtEntranceDate" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtEntranceDate"
                                                Enabled="True" Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                        </label>
                                        <label class="label col3">
                                            Marks Obtained:
			 <asp:TextBox ID="txtMarksObtained" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>

                                    </div>

                                    <div class="row">
                                        <label class="label col3">
                                            Interview Date:
			 <asp:TextBox ID="txtInterviewDate" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtInterviewDate"
                                                Enabled="True" Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                        </label>
                                        <label class="label col3">
                                            Interview By:
			 <asp:TextBox ID="txtInterviewBy" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                        <label class="label col3">
                                            Interview Marks:
			 <asp:TextBox ID="txtInterviewResult" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                    </div>

                                    <div class="row">
                                        <label class="label col3">
                                            Admission No:
                   <asp:TextBox ID="txtAdmissionNo" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                        <label class="label col3">
                                            Admission Date:
			 <asp:TextBox ID="txtAdmissionDate" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtAdmissionDate"
                                                Enabled="True" Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                        </label>

                                        <label class="label col3">
                                            University Registration No:
			 <asp:TextBox ID="txtUniRegdNo" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>

                                    </div>
                                </fieldset>


                                <fieldset style="margin: 15px;" class="sub-container">

                                    <div class="row">
                                        <label class="label col4">
                                            Filled By:
			 <asp:TextBox ID="txtFilledBy" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                        <label class="label col4">
                                            Filled Date:
			 <asp:TextBox ID="txtFilledDate" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtFilledDate"
                                                Enabled="True" Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                        </label>
                                        <label class="label col4">
                                            Verified By:
			 <asp:TextBox ID="txtVerifiedBy" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                        </label>
                                        <label class="label col4">
                                            Verified Date:
			 <asp:TextBox ID="txtVerifiedDate" Width="100%" Height="22px" runat="server"></asp:TextBox>
                                            <cc1:CalendarExtender ID="CalendarExtender5" runat="server" TargetControlID="txtVerifiedDate"
                                                Enabled="True" Format="dd/MM/yyyy">
                                            </cc1:CalendarExtender>
                                        </label>
                                    </div>
                                    </div>

                                </fieldset>
                            </ContentTemplate>
                        </cc1:TabPanel>

                    </cc1:TabContainer>
                </asp:Panel>
            </div>


            <div id="buttons" class="sub-container">
                <div class="row">

                    <label class="label col4">
                    </label>

                    <label class="label col4">
                        <asp:Button ID="btnSave" runat="server" Height="25px" Text="Save" OnClick="btnSave_Click" />
                    </label>

                    <label class="label col4">
                        <asp:Button ID="btnReset" runat="server" Height="25px" Text="Reset" OnClick="btnReset_Click" />
                    </label>
                    <label class="label col4">
                    </label>

                </div>
            </div>

        </div>

    </div>

    <%-- <script type="text/javascript">
        var xPos, yPos;
        var prm = Sys.WebForms.PageRequestManager.getInstance();

        function BeginRequestHandler(sender, args) {
            if ($get('<%=Panel1.ClientID%>') != null) {
                xPos = $get('<%=Panel1.ClientID%>').scrollLeft;
                yPos = $get('<%=Panel1.ClientID%>').scrollTop;
            }
        }

        function EndRequestHandler(sender, args) {
            if ($get('<%=Panel1.ClientID%>') != null) {
                    $get('<%=Panel1.ClientID%>').scrollLeft = xPos;
                    $get('<%=Panel1.ClientID%>').scrollTop = yPos;
                }
            }
            prm.add_beginRequest(BeginRequestHandler);
            prm.add_endRequest(EndRequestHandler);
    </script>--%>
</asp:Content>
