<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Staff_Detail.aspx.cs" Inherits="human_resource_reports_Staff_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        #student_detail {
            background-color: #CCC;
            width: 100%;
            padding: 10px;
        }

            #student_detail table {
                border: 1px solid black;
                background-color: white;
                box-shadow: 2px 2px 5px black;
            }

        .heading {
            color: blue;
            font-weight: bold;
        }

        #student_detail table td {
            /*padding-left:5px;
            padding-right:5px;*/
        }



        .auto-style1 {
            width: 25%;
            height: 23px;
        }

        .auto-style2 {
            height: 23px;
        }
    </style>

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
        <table>
            <tr>
                <td>
                    <asp:Button ID="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click" />
                </td>
            </tr>
        </table>
        <br>
        <div id="div_print">
            <div id="student_detail">
                <table style="width: 100%">
                    <tr>
                        <td colspan="4" style="font-size: 16pt; font-weight: bold">Employee Detail</td>
                    </tr>
                    <tr class="heading">
                        <td>Employee Id</td>
                        <td style="width: 25%">Full Name</td>
                        <td style="width: 25%">Abbrevation</td>
                        <td rowspan="14" style="vertical-align: top">
                            <asp:Image ID="imgStudent" runat="server" Height="200px" /></td>
                    </tr>

                    <tr style="font-weight: bold">
                        <td>
                            <asp:Label ID="lblEmployeeId" runat="server" Text=""></asp:Label></td>

                        <td>
                            <asp:Label ID="lblFullNameEng" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblAbbrevation" runat="server" Text=""></asp:Label></td>

                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>

                    </tr>
                    <tr class="heading">
                        <td>Full Name(Dev.)</td>
                        <td style="width: 25%">Gender</td>
                        <td style="width: 25%">Marital Status</td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFullNameNep" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblGender" runat="server" Text=""></asp:Label></td>

                        <td>
                            <asp:Label ID="lblMaritalStatus" runat="server" Text=""></asp:Label></td>


                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>

                    </tr>
                    <tr class="heading">
                        <td>Father's Name</td>
                        <td style="width: 25%">GrandFather's Name</td>
                        <td>Mother's Name</td>


                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFatherName" runat="server" Text=""></asp:Label></td>

                        <td>
                            <asp:Label ID="lblGrandFatherName" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblMotherName" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>

                    </tr>
                    <tr class="heading">

                        <td style="width: 25%">Spouse's Name</td>
                        <td style="width: 25%">Division</td>
                        <td style="width: 25%">Designation</td>

                    </tr>
                    <tr>


                        <td>
                            <asp:Label ID="lblSpouseName" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblDivision" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblDesignation" runat="server" Text=""></asp:Label></td>

                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>

                    </tr>
                    <tr class="heading">
                        <td class="auto-style1">Department</td>
                        <td class="auto-style1">Employee Type</td>
                        <td class="auto-style2">Email</td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDepartment" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblEmployeeType" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="auto-style2"></td>
                        <td class="auto-style2"></td>
                        <td class="auto-style2"></td>
                        <td class="auto-style2"></td>

                    </tr>
                    <tr class="heading">
                        <td class="auto-style1">Phone No</td>
                        <td class="auto-style1">Mobile No1</td>
                        <td class="auto-style1">Mobile No2</td>
                        <td class="auto-style2">Citizenship No</td>
                        

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPhoneNo" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblMobileNo1" runat="server" Text=""></asp:Label></td>
                         <td>
                            <asp:Label ID="lblMobileNo2" runat="server" Text=""></asp:Label></td>
                        
                        <td>
                            <asp:Label ID="lblCitizenshipNo" runat="server" Text=""></asp:Label></td>
                        
                    </tr>

                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>

                    </tr>
                    <tr class="heading">
                        <td class="auto-style2">DOB BS</td>
                        <td class="auto-style1">DOB AD</td>
                        <td class="auto-style1">Appointment Date</td>
                       
                        <td class="auto-style2"></td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDOBBS" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblDOBAD" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblAppointmentDate" runat="server" Text=""></asp:Label></td>
                        
                        <td></td>
                    </tr>



                </table>
                <br>
                <table style="width: 100%">
                    <tr>
                        <td colspan="4" style="font-size: 16pt; font-weight: bold">Permanent Address</td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">Country</td>
                        <td style="width: 25%">State</td>
                        <td style="width: 25%">Zone</td>
                        <td style="width: 25%">District</td>
                        
                        

                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblCountryP" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblStateP" runat="server" Text=""></asp:Label></td>

                        <td>
                            <asp:Label ID="lblZoneP" runat="server" Text=""></asp:Label></td>

                        <td>
                            <asp:Label ID="lblDistrictP" runat="server" Text=""></asp:Label></td>
                       

                    </tr>
                    <tr>
                        <td class="auto-style2"></td>
                        <td class="auto-style2"></td>
                        <td class="auto-style2"></td>
                        <td class="auto-style2"></td>
                    </tr>
                    <tr class="heading">

                        <td style="width: 25%">VDC/ Municipality</td>
                        <td style="width: 25%">Ward No</td>
                        <td style="width: 25%">Street Name</td>
                        <td style="width: 25%">House No</td>
                       
                     
                    </tr>
                    <tr>
                         <td>
                            <asp:Label ID="lblVDCMuniP" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblWardnoP" runat="server" Text=""></asp:Label></td>
                
                        <td><asp:Label ID="lblStreetNameP" runat="server" Text=""></asp:Label></td>
                        <td><asp:Label ID="lblHouseNoP" runat="server" Text=""></asp:Label></td>
                      
                    </tr>

                </table>
                <br>

                <table style="width: 100%">
                    <tr>
                        <td colspan="4" style="font-size: 16pt; font-weight: bold">Contact Address</td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">Country</td>
                        <td style="width: 25%">State</td>
                        <td style="width: 25%">Zone</td>
                        <td style="width: 25%">District</td>

                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblCountryC" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblStateC" runat="server" Text=""></asp:Label></td>

                        <td>
                            <asp:Label ID="lblZoneC" runat="server" Text=""></asp:Label></td>

                        <td>
                            <asp:Label ID="lblDistrictC" runat="server" Text=""></asp:Label></td>
                     
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr class="heading">

                        <td style="width: 25%">VDC/ Municipality</td>
                        <td style="width: 25%">Ward No</td>
                        <td style="width: 25%">Street Name</td>
                        <td style="width: 25%">House No</td>

                    </tr>
                    <tr>
                         <td>
                            <asp:Label ID="lblVDCMuniC" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblWardnoC" runat="server" Text=""></asp:Label></td>

                        <td>
                            <asp:Label ID="lblStreetNameC" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblHouseNoC" runat="server" Text=""></asp:Label></td>

                        <td></td>
                        <td></td>
                        <td></td>


                    </tr>

                </table>

                <br>

                <table style="width: 100%">
                    <tr>
                        <td colspan="4" style="font-size: 16pt; font-weight: bold">Nominee's Information</td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">Nominee's Name</td>
                        <td style="width: 25%">Address</td>
                        <td style="width: 25%">Relation</td>
                        <td style="width: 25%">Contact No</td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblNName" runat="server" Text=""></asp:Label></td>

                        <td>
                            <asp:Label ID="lblNAddress" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblNRelation" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblNContactNo" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>

                </table>

                <br>

                <table style="width: 100%">
                    <tr>
                        <td colspan="4" style="font-size: 16pt; font-weight: bold">Other Information</td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">PFID No</td>
                        <td style="width: 25%">PFID Amount</td>
                        <td style="width: 25%">Basic Salary</td>
                        <td style="width: 25%">Beneficiary Amount</td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblPFIDNo" runat="server" Text=""></asp:Label></td>

                        <td>
                            <asp:Label ID="lblPFIDAmount" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblBasicSalary" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblBeneficiaryAmt" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">CIT No</td>
                        <td style="width: 25%">CIT Amount</td>
                        <td style="width: 25%">Primary Bank</td>
                        <td style="width: 25%">Primary A/C No</td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCITNo" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblCITAmount" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblPrimaryBank" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblPrimaryAcNo" runat="server" Text=""></asp:Label></td>


                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">Secondary Bank</td>
                        <td style="width: 25%">Secondary A/C No</td>
                        <td style="width: 25%">Remarks</td>
                        <td style="width: 25%">PAN No</td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSecondaryBank" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblSecondaryAcNo" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblRemarks" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblPanNo" runat="server" Text=""></asp:Label></td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
