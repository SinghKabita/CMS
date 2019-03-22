<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Employees.aspx.cs" Inherits="human_resource_Employees" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style>
        .form {
            overflow: hidden;
            background-color: #CCC;
            padding: 1em 1em 1em 1em;
            font-size: .9em;
            border: 1px solid black;
            box-sizing: border-box;
            min-width: 270px;
            margin-bottom: 10px;
            font-size: 16px;
        }

        .sub-container {
            background-color: #fff;
            border: 1px solid black;
            margin: auto;
            padding: 5px;
            box-sizing: border-box;
            margin-bottom: 30px;
            box-shadow: 0 0 5px black;
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

        .cancel-control {
            float: right;
            font-size: 50px;
            top: 5px;
        }

        .overlay {
            width: 100vw;
            height: 200vh;
            background-color: rgba(255,255,255, .9);
            z-index: 10000000;
            top: 0;
            position: absolute;
        }

        .popup-container {
            width: 300px;
            background-color: #fff;
            border: 1px solid black;
            margin: auto;
            top: 50%;
            left: calc(50% - 150px);
            transform: translateY(-50%);
            padding: 5px;
            box-sizing: border-box;
            position: fixed;
            z-index: 10000001;
            box-shadow: 0 0 5px black;
            display: block;
            clear: both;
            transition: top 300ms ease-in-out;
            transition: transform 300ms ease-in-out;
        }


        .hidden {
            transform: translateY(0%);
            top: -100%;
        }

        .popup-container-heading {
            height: 30px;
            border-bottom: 1px solid black;
            padding: 0 5px;
            box-sizing: border-box;
            margin-bottom: 10px;
        }

            .popup-container-heading > .popup-title {
                font-size: 18px;
                line-height: 30px;
                text-align: left;
                font-weight: bold;
                float: left;
            }

            .popup-container-heading > .cancel-control {
                font-size: 15px;
                top: 3px;
                right: 3px;
                float: right;
                font-family: Arial;
                color: black;
                cursor: pointer;
            }

                .popup-container-heading > .cancel-control:hover {
                    color: red;
                }


        .row {
            overflow: hidden;
            box-sizing: border-box;
            margin-left: 3px;
            margin-right: 3px;
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

        .form > .row > .col2 {
            width: calc((100%  / 2) - 1em);
        }

        .form > .row > .col3 {
            width: calc((100%  / 3) - 1em);
        }

        .form > .sub-container > .row > .col4 {
            width: calc((100%  / 4) - 1em);
            top: 0px;
            left: 0px;
        }

        .form > .popup-container > .row > .col4 {
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
                <div class="sub-container-heading">Employee Detail</div>
                <div class="row">

                    <label class="label col4">
                        Division:
			                    <br />
                        <asp:DropDownList ID="ddlDiv" Height="22px" Width="100%" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlDiv_SelectedIndexChanged">
                        </asp:DropDownList>

                    </label>

                    <label class="label col4">
                        Employee ID No:
			     <asp:TextBox ID="txtIdNo" runat="server" AutoPostBack="True" Height="22px" OnTextChanged="txtIdNo_TextChanged"></asp:TextBox>
                    </label>
                </div>
                <div class="row">
                    <label class="label col4">
                        First Name:
			                            <asp:TextBox ID="txtFirstname" runat="server" Height="22px"></asp:TextBox>

                    </label>

                    <label class="label col4">
                        Last Name:
			 <asp:TextBox ID="txtLastname" Height="22px" runat="server"></asp:TextBox>
                    </label>
                    <label class="label col4">
                        Abbrevation:
			 <asp:TextBox ID="txtAbbrevation" Height="22px" runat="server"></asp:TextBox>
                    </label>

                    <label class="label col4">
                        Name In Devnagiri:
			        <asp:TextBox ID="txtDevName" runat="server" Height="22px"></asp:TextBox>

                    </label>
                </div>

                <div class="row">
                    <label class="label col4">
                        Gender:
                     <br />
                        <asp:RadioButtonList ID="rbtnGender" runat="server" RepeatDirection="Horizontal">
                            <asp:ListItem Selected="True" Value="M">Male</asp:ListItem>
                            <asp:ListItem Value="F">Female</asp:ListItem>
                        </asp:RadioButtonList>

                    </label>
                    <label class="label col4">
                        Marital Status:
                    <br />
                        <asp:RadioButtonList ID="rbtnMaritalStatus" runat="server" RepeatDirection="Horizontal"  >
                            <asp:ListItem Selected="True">Unmarried</asp:ListItem>
                            <asp:ListItem>Married</asp:ListItem>
                        </asp:RadioButtonList>
                    </label>
                    <label class="label col4">
                        Photo:
			      <asp:FileUpload ID="fileUpload1" runat="server" Height="22px" />

                    </label>


                </div>

                <div class="row">
                    <label class="label col4">
                        Email:
			 <asp:TextBox ID="txtEmail" runat="server" Height="22px"></asp:TextBox>
                        <%--<asp:RequiredFieldValidator ID="reql" runat="server" ErrorMessage="Please enter correct email" ControlToValidate="txtEMAIL"
                            ForeColor="Red"></asp:RequiredFieldValidator>--%>
                        <asp:RegularExpressionValidator ID="ReExl"
                                runat="server" ControlToValidate="txtEMAIL" ErrorMessage="Please enter correct email" ForeColor="Red"
                                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                    </label>
                    <label class="label col4">
                        Phone No:
			                                        <asp:TextBox ID="txtPhone" Height="22px" runat="server"></asp:TextBox>

                    </label>
                    <label class="label col4">
                        Mobile No1:
			           <asp:TextBox ID="txtMobile1" Height="22px" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="M1Validation" runat="server"
                            ControlToValidate="txtMOBILE1" ErrorMessage="only numbers following 10 digits "
                            ValidationExpression="[0-9]{10}">
                        </asp:RegularExpressionValidator>

                    </label>
                    <label class="label col4">
                        Mobile No2:
			                                        <asp:TextBox ID="txtMobile2" Height="22px" runat="server"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                            ControlToValidate="txtMobile2" ErrorMessage="only numbers following 10 digits "
                            ValidationExpression="[0-9]{10}">
                        </asp:RegularExpressionValidator>

                    </label>
                </div>
                <div class="row">
                    <label class="label col4">
                        Citizenship No:
		                                        <asp:TextBox ID="txtCtzno" Height="22px" runat="server"></asp:TextBox>

                    </label>
                    <label class="label col4">
                        Birth Date (BS):
                    <br>
                        <asp:TextBox ID="txtBirthDay" runat="server" Height="22px" Width="10%" Placeholder="DD" Style="display: inline-block" MaxLength="3" AutoPostBack="True" OnTextChanged="txtBirthDay_TextChanged"></asp:TextBox>
                        /
                    <asp:TextBox ID="txtBirthMonth" runat="server" Height="22px" Width="10%" Placeholder="MM" Style="display: inline-block" MaxLength="3" AutoPostBack="True" OnTextChanged="txtBirthMonth_TextChanged"></asp:TextBox>
                        /
                    <asp:TextBox ID="txtBirthYear" runat="server" Height="22px" Width="30%" Placeholder="YYYY" Style="display: inline-block" MaxLength="5" AutoPostBack="True" OnTextChanged="txtBirthYear_TextChanged"></asp:TextBox>
                        <%-- <asp:RequiredFieldValidator ID="RFMBD" runat="server" ControlToValidate="txtBirthDay"
                                            ErrorMessage="Date of Birth Day must be entered">*</asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RFMBM" runat="server" ControlToValidate="txtBirthMonth"
                                            ErrorMessage="Date of Birth Month must be entered">*</asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RFMBY" runat="server" ControlToValidate="txtBirthYear"
                                            ErrorMessage="Date of Birth Year must be entered">*</asp:RequiredFieldValidator>--%>
                    </label>
                    <label class="label col4">
                        Birth Date (AD):
                    <br>
                        <asp:TextBox ID="txtBirthDay_AD" runat="server" Height="22px" Width="10%" Placeholder="DD" Style="display: inline-block" MaxLength="3" AutoPostBack="True" OnTextChanged="txtBirthDay_AD_TextChanged"></asp:TextBox>
                        /
                    <asp:TextBox ID="txtBirthMonth_AD" runat="server" Height="22px" Width="20%" Placeholder="MM" Style="display: inline-block" MaxLength="3" AutoPostBack="True" OnTextChanged="txtBirthMonth_AD_TextChanged"></asp:TextBox>
                        /
                    <asp:TextBox ID="txtBirthYear_AD" runat="server" Height="22px" Width="30%" Placeholder="YYYY" Style="display: inline-block" MaxLength="5" AutoPostBack="True" OnTextChanged="txtBirthYear_AD_TextChanged"></asp:TextBox>

                    </label>
                    <label class="label col4">
                        Appointment Date(BS):
                    <br />
                        <asp:TextBox ID="txtJobstartDay" runat="server" Width="10%" Height="22px" Placeholder="DD" Style="display: inline-block" MaxLength="2"></asp:TextBox>
                        /
                    <asp:TextBox ID="txtJobstartMonth" runat="server" Width="10%" Height="22px" Placeholder="MM" Style="display: inline-block" MaxLength="2"></asp:TextBox>
                        /
                    <asp:TextBox ID="txtJobstartYear" runat="server" Width="30%" Height="22px" Placeholder="YYYY" Style="display: inline-block" MaxLength="4"></asp:TextBox>
                        <%-- <asp:RequiredFieldValidator ID="RFMAPD" runat="server" ControlToValidate="txtJobstartDay"
                                            ErrorMessage="Appointment Day must be entered">*</asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RFMAPM" runat="server" ControlToValidate="txtJobstartMonth"
                                            ErrorMessage="Appointment Month must be entered">*</asp:RequiredFieldValidator>
                                        <asp:RequiredFieldValidator ID="RFMAPY" runat="server" ControlToValidate="txtJobstartYear"
                                            ErrorMessage="Appointment Year must be entered">*</asp:RequiredFieldValidator>--%>
                    </label>
                </div>

                <div class="row">


                    <label class="label col4">
                        Father's Name:
			                                        <asp:TextBox ID="txtFatherName" runat="server" Height="22px"></asp:TextBox>

                    </label>
                    <label class="label col4">
                        Grand Father's Name:
			                                        <asp:TextBox ID="txtGrandFName" runat="server" Height="22px"></asp:TextBox>

                    </label>
                    <label class="label col4">
                        Mother's Name:
			                                        <asp:TextBox ID="txtMotherName" Height="22px" runat="server"></asp:TextBox>

                    </label>
                    <label class="label col4" id="lblSpouse" runat="server" >
                        Spouse's Name:
			                                        <asp:TextBox ID="txtSpouseName" Height="22px" runat="server"></asp:TextBox>

                    </label>
                </div>

                <div class="row">

                    <label class="label col4">
                        Designation:
                    <br />
                        <asp:DropDownList ID="ddlDesignation" Height="22px" Width="100%" runat="server">
                        </asp:DropDownList>

                    </label>

                    <label class="label col4">
                        Department: 
                    <br />
                        <asp:DropDownList ID="ddlDept" Height="22px" runat="server" Width="100%" AutoPostBack="True">
                        </asp:DropDownList>


                    </label>
                    <label class="label col4">
                        Job Title:
			                                        <asp:TextBox ID="txtJobTitle" Height="22px" runat="server"></asp:TextBox>

                    </label>
                    <label class="label col4">
                        Employee Type: 
                        <br />
                        <asp:DropDownList ID="ddlEmployeeType" Height="22px" Width="100%" Style="display: inline-block" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlEmployeeType_SelectedIndexChanged">
                        </asp:DropDownList>

                    </label>
                </div>

            </div>

            <div id="address-div" class="sub-container">
                <div class="sub-container-heading">Permanent Address</div>
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
            </div>

            <div id="contactaddress-div" class="sub-container">
                <div class="sub-container-heading">Contact Address</div>
                <div class="row">

                    <label class="label col4" id="lblSameAsOf" runat="server">
                        Same as of &nbsp;&nbsp;<asp:DropDownList ID="ddlContactAddress" runat="server" Width="50%" Height="22px"
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



            </div>

            <div id="nominee-div" class="sub-container">
                <div class="sub-container-heading">Nominee Detail</div>
                <div class="row">
                    <label class="label col4">
                        Nominee Name:
                         <br />
                        <asp:TextBox ID="txtNomineeName" Width="100%" Height="22px" Style="display: inline-block" runat="server"></asp:TextBox>
                    </label>
                    <label class="label col4">
                        Nominee Address:
                         <br />
                        <asp:TextBox ID="txtNomineeAdd" Width="100%" Height="22px" Style="display: inline-block" runat="server"></asp:TextBox>
                    </label>
                    <label class="label col4">
                        Nominee Relation:
                         <br />
                        <asp:TextBox ID="txtNomineeRelation" Width="100%" Height="22px" Style="display: inline-block" runat="server"></asp:TextBox>
                    </label>
                    <label class="label col4">
                        Contact No:
                         <br />
                        <asp:TextBox ID="txtNomineeContact" Width="100%" Height="22px" Style="display: inline-block" runat="server"></asp:TextBox>
                    </label>

                </div>


            </div>

            <div id="other-detail" class="sub-container">
                <div class="sub-container-heading">Other Detail</div>
                <div class="row">
                    <label class="label col4">
                        PFID No:
                         <br />
                        <asp:TextBox ID="txtPfidno" Width="100%" Height="22px" Style="display: inline-block" runat="server"></asp:TextBox>
                    </label>
                    <label class="label col4">
                        PFID Amount:
                         <br />
                        <asp:TextBox ID="txtPfidAmt" Width="100%" Height="22px" Style="display: inline-block" runat="server"></asp:TextBox>
                    </label>
                    <label class="label col4" id="paytype_label" runat="server" visible="false">
                        Pay Type:
                         <br />
                        <asp:DropDownList ID="ddlPayType" Width="100%" Height="22px" Style="display: inline-block" runat="server">
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem>Hourly Basis</asp:ListItem>
                            <asp:ListItem>Package Wise</asp:ListItem>
                        </asp:DropDownList>
                    </label>

                    <label class="label col4" id="basicsalary_label" runat="server">
                        Basic Salary:
                         <br />
                        <asp:TextBox ID="txtBasicSal" Width="100%" Height="22px" Style="display: inline-block" runat="server"></asp:TextBox>
                    </label>
                    <label class="label col4" id="beneficiaryAmt_label" runat="server">
                        Beneficiary Amount:
                         <br />
                        <asp:TextBox ID="txtBeneficiaryAmt" Width="100%" Height="22px" Style="display: inline-block" runat="server"></asp:TextBox>
                    </label>

                </div>

                <div class="row">
                    <label class="label col4">
                        CIT No:
                         <br />
                        <asp:TextBox ID="txtCitno" Width="100%" Height="22px" Style="display: inline-block" runat="server"></asp:TextBox>
                    </label>
                    <label class="label col4">
                        CIT Amount:
                         <br />
                        <asp:TextBox ID="txtCitPer" Width="100%" Height="22px" Style="display: inline-block" runat="server"></asp:TextBox>
                    </label>
                    <label class="label col4">
                        Primary Bank Name:
                    <br />
                        <asp:DropDownList ID="ddlBankName" Width="100%" Height="22px" Style="display: inline-block" runat="server">
                        </asp:DropDownList>
                    </label>
                    <label class="label col4">
                        Primary Bank Ac No:
                         <br />
                        <asp:TextBox ID="txtCurrentbankaccount" Width="100%" Height="22px" Style="display: inline-block" runat="server"></asp:TextBox>
                    </label>

                </div>


                <div class="row">
                    <label class="label col4">
                        Secondary Bank Name:
			   <br />
                        <asp:DropDownList ID="ddBankName2" Width="100%" Height="22px" Style="display: inline-block" runat="server">
                        </asp:DropDownList>
                    </label>
                    <label class="label col4">
                        Secondary Bank Ac No:
                         <br />
                        <asp:TextBox ID="txtSecondbankaccount" Width="100%" Height="22px" Style="display: inline-block" runat="server"></asp:TextBox>
                    </label>
                    <label class="label col4">
                        Remarks:
                         <br />
                        <asp:TextBox ID="txtRemarks" Width="100%" Height="22px" Style="display: inline-block" runat="server"></asp:TextBox>
                    </label>
                    <label class="label col4">
                        PAN No:
                         <br />
                        <asp:TextBox ID="txtPANNo" Width="100%" Height="22px" Style="display: inline-block" runat="server"></asp:TextBox>
                    </label>


                </div>

                <div class="row">
                    <%--<label class="label col4">
                        Status:
                         <br />
                        <asp:CheckBox ID="chkStatus" runat="server" Width="100%" Height="22px" Style="display: inline-block" Text="Not Included in Payment Sheet" />
                    </label>--%>
                    <label class="label col4">
                        Citizenship:
                         <br />
                        <asp:FileUpload ID="FileUploadCitiznshp" runat="server" Width="100%" Height="22px" Style="display: inline-block" />
                    </label>
                    <label class="label col4">
                        Curriculumn Vitae(CV):
                         <br />
                        <asp:FileUpload ID="FileUploadCV1" runat="server" Width="100%" Height="22px" Style="display: inline-block" />
                    </label>
                    <label class="label col4">
                        Certificates:
                         <br />
                        <asp:FileUpload ID="FileUploadCertificate" runat="server" Width="100%" Height="22px" Style="display: inline-block" />
                    </label>


                </div>

            </div>

            <div id="buttons" class="sub-container">
                <div class="row">

                    <asp:Button ID="btnSave" runat="server" Height="22px" Width="10%" Style="display: inline-block" Text="Save" OnClick="btnSave_Click" />

                    <%--  <asp:Button ID="btnSave" runat="server" Height="25px" Text="Save" OnClick="btnSave_Click" />--%>

                    <%--<input id="btnPromote" type="button" style="display: inline-block; height: 22px; width: 10%" value="Promote" />

                    <input id="btnResignation" type="button" style="display: inline-block; height: 22px; width: 15%" value="Resignation/Retirement" />--%>


                    <asp:Button ID="btnReset" runat="server" Height="22px" Width="10%" Style="display: inline-block" Text="Reset" OnClick="btnReset_Click" />

                    <asp:Button ID="btnBack" runat="server" Height="22px" Width="10%" Style="display: inline-block" Text="Back" OnClick="btnBack_Click" />

                </div>
            </div>

        </div>

       <%-- <div id="overlay-div">

            <div id="promotion-div" class="popup-container hidden">
                <div class="popup-container-heading">
                    <div class="popup-title">Promotion</div>
                    <div class="cancel-control">x</div>
                </div>
                <div class="row">
                    <label class="label col4">
                        Date:
                    <br />
                        <asp:TextBox ID="PromotionDay" runat="server" Width="10%" Height="22px" Style="display: inline-block" MaxLength="2"></asp:TextBox>
                        /<asp:TextBox ID="PromotionMonth" runat="server" Width="10%" Height="22px" Style="display: inline-block" MaxLength="2"></asp:TextBox>
                        /<asp:TextBox ID="PromotionYear" runat="server" Width="30%" Height="22px" Style="display: inline-block" MaxLength="4"></asp:TextBox>
                    </label>
                    <label class="label col4">
                        Employee ID No:
			 <asp:TextBox ID="txtEmpIdP" Width="100%" Height="22px" Style="display: inline-block" runat="server" ReadOnly="True"></asp:TextBox>
                        <asp:Label ID="lblTempId" runat="server" Text="lblTempId" Visible="False"></asp:Label>
                    </label>
                    <label class="label col4">
                        Name:
			 <asp:TextBox ID="txtEmpNameP" Width="100%" Height="22px" Style="display: inline-block" runat="server" ReadOnly="True"></asp:TextBox>
                    </label>
                    <label class="label col4">
                        Promoted From:
			 <asp:TextBox ID="txtDesignation" Width="100%" Height="22px" Style="display: inline-block" runat="server" ReadOnly="True"></asp:TextBox>
                    </label>
                    <label class="label col4">
                        Promoted To:
                    <br />
                        <asp:DropDownList ID="ddlPromo" runat="server" Width="100%" Height="22px" Style="display: inline-block" AutoPostBack="True">
                        </asp:DropDownList>

                    </label>

                    <label class="label col4">
                        <asp:Button ID="btnSavePromotion" runat="server" Width="100%" Text="Save" OnClick="btnSavePromotion_Click" Visible="false" />

                    </label>

                </div>
            </div>



            <div id="resignation-div" class="popup-container hidden">
                <div class="popup-container-heading">
                    <div class="popup-title">Resignation</div>
                    <div class="cancel-control">x</div>
                </div>
                <div class="row">
                    <label class="label col4">
                        Date:
			  <br />
                        <asp:TextBox ID="txtResDay" runat="server" Width="10%" Height="22px" Style="display: inline-block" MaxLength="2"></asp:TextBox>
                        /<asp:TextBox ID="txtResMonth" runat="server" Width="10%" Height="22px" Style="display: inline-block" MaxLength="2"></asp:TextBox>
                        /<asp:TextBox ID="txtResYear" runat="server" Width="30%" Height="22px" Style="display: inline-block" MaxLength="4"></asp:TextBox>
                    </label>

                    <label class="label col4">
                        Employee ID No:
			 <asp:TextBox ID="txtEmpIdR" Width="100%" Height="22px" Style="display: inline-block" runat="server" ReadOnly="True"></asp:TextBox>
                    </label>
                    <label class="label col4">
                        Name:
			 <asp:TextBox ID="txtEmpNameR" Width="100%" Height="22px" Style="display: inline-block" runat="server" ReadOnly="True"></asp:TextBox>
                    </label>
                    <label class="label col4">
                        Resignation Type:
                    <br />
                        <asp:DropDownList ID="ddlResignationType" runat="server" Height="22px" Width="100%" Style="display: inline-block" AutoPostBack="True">
                            <asp:ListItem>Select</asp:ListItem>
                            <asp:ListItem>Resignation</asp:ListItem>
                            <asp:ListItem>Retirement</asp:ListItem>
                        </asp:DropDownList>
                    </label>

                </div>
                <div class="row">
                    <label class="label col1">
                        Description:
			
                <asp:TextBox ID="txtDescription" runat="server" Width="100%" Height="40px" TextMode="MultiLine"></asp:TextBox>


                    </label>

                    <label class="label col4">
                        <asp:Button ID="btnSaveResignation" runat="server" Width="100%" Text="Save" OnClick="btnSaveResignation_Click" />

                    </label>
                </div>
            </div>
        </div>--%>
    </div>


    <%--.............................jquery operation for displaying popup.............................................--%>
    <script src="http://localhost/nccshr/js/jquery.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {



            $('#btnPromote').click(function () {

                $('#overlay-div').addClass('overlay');

                $('.cancel-control').css('color', 'black');
                $('#promotion-div').removeClass('hidden');

            });

            $('#btnResignation').click(function () {

                $('#overlay-div').addClass('overlay');

                $('.cancel-control').css('color', 'black');
                $('#resignation-div').removeClass('hidden');

            });


            $('.cancel-control').click(function () {

                $('#overlay-div').removeClass('overlay');
                $('#resignation-div').addClass('hidden');
                $('#promotion-div').addClass('hidden');
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