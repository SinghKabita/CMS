<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="Student_Detail.aspx.cs" Inherits="administration_reports_Student_Detail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <style type="text/css">
        #student_detail {
             background-color: #fff;
            border: 1px solid black;
            margin: auto;
            padding: 5px;
            box-sizing: border-box;
            margin-bottom: 30px;
            box-shadow: 0 0 10px black;
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
                        <td colspan="4" style="font-size: 16pt; font-weight: bold">Student Detail</td>
                    </tr>
                    <tr class="heading">
                        <td>Batch</td>
                        <td style="width: 25%">Registration No</td>
                        <td style="width: 25%">Full Name</td>
                        <td rowspan="14" style="vertical-align: top">
                            <asp:Image ID="imgStudent" runat="server" Height="200px" /></td>
                    </tr>

                    <tr style="font-weight: bold">
                        <td>
                            <asp:Label ID="lblBatch" runat="server" Text=""></asp:Label></td>

                        <td>
                            <asp:Label ID="lblRegNo" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblFullNameEng" runat="server" Text=""></asp:Label></td>

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
                        <td>Nationality</td>
                        <td style="width: 25%">Religion</td>
                        <td>Citizenship No</td>


                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblNationality" runat="server" Text=""></asp:Label></td>

                        <td>
                            <asp:Label ID="lblReligion" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblCitizenship" runat="server" Text=""></asp:Label></td>
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>

                    </tr>
                    <tr class="heading">

                        <td style="width: 25%">Email</td>
                        <td style="width: 25%">Phone No</td>
                        <td style="width: 25%">Mobile No_1</td>
                       

                    </tr>
                    <tr>


                        <td>
                            <asp:Label ID="lblEmail" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblPhoneNo" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblMobileNo1" runat="server" Text=""></asp:Label></td>
                         

                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>

                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">DOB (BS)</td>
                        <td style="width: 25%">DOB (AD)</td>
                        <td style="width: 25%">Mobile No_2</td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDOBBS" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblDOBAD" runat="server" Text=""></asp:Label></td>
                       <td>
                            <asp:Label ID="lblMobileNo2" runat="server" Text=""></asp:Label></td>
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
                            <asp:Label ID="lblVDCMuniP" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblWardnoP" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblStreetNameP" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblHousenoP" runat="server" Text=""></asp:Label></td>
                        


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
                            <asp:Label ID="lblHousenoC" runat="server" Text=""></asp:Label></td>


                    </tr>

                </table>

                <br>

                <table style="width: 100%">
                    <tr>
                        <td colspan="4" style="font-size: 16pt; font-weight: bold">Father's Information</td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">Father's Name</td>
                        <td style="width: 25%">Address</td>
                        <td style="width: 25%">Phone No</td>
                        <td style="width: 25%">Mobile No1</td>
                        
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblFName" runat="server" Text=""></asp:Label></td>

                        <td>
                            <asp:Label ID="lblFAddress" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblFPhoneNo" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblFMobileNo1" runat="server" Text=""></asp:Label></td>
                       
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">Mobile No2</td>
                        <td style="width: 25%">Email</td>
                        <td style="width: 25%">Occupation</td>
                        <td style="width: 25%">Designation</td>
                        

                    </tr>
                    <tr>
                         <td>
                            <asp:Label ID="lblFMobileNo2" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblFEmail" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblFOccupation" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblFDesignation" runat="server" Text=""></asp:Label></td>
                        
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">Name of Company/Office</td>
                        <td style="width: 25%">Office Phone No.</td>
                        <td style="width: 25%"></td>
                        <td style="width: 25%"></td>
                       

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFOfficeName" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblFOfficeNo" runat="server" Text=""></asp:Label></td>
                        <td></td>
                        <td></td>
                       
                    </tr>

                </table>

                <br>

                <table style="width: 100%">
                    <tr>
                        <td colspan="4" style="font-size: 16pt; font-weight: bold">Mother's Information</td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">Mother's Name</td>
                        <td style="width: 25%">Address</td>
                        <td style="width: 25%">Phone No</td>
                        <td style="width: 25%">Mobile No1</td>
                        
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblMName" runat="server" Text=""></asp:Label></td>

                        <td>
                            <asp:Label ID="lblMAddress" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblMPhoneNo" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblMMobileNo1" runat="server" Text=""></asp:Label></td>

                        
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">Mobile No2</td>
                        <td style="width: 25%">Email</td>
                        <td style="width: 25%">Occupation</td>
                        <td style="width: 25%">Designation</td>
                        

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMMobileNo2" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblMEmail" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblMOccupation" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblMDesignation" runat="server" Text=""></asp:Label></td>
                        
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">Name of Company/Office</td>
                        <td style="width: 25%">Office Phone No.</td>
                        <td style="width: 25%"></td>
                        <td style="width: 25%"></td>
                      
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblMOfficeName" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblMOfficeNo" runat="server" Text=""></asp:Label></td>
                        <td></td>
                        <td></td>                       
                    </tr>
                </table>
                <br>

                <table style="width: 100%">
                    <tr>
                        <td colspan="4" style="font-size: 16pt; font-weight: bold">Guardian's Information</td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">Guardian's Name</td>
                        <td style="width: 25%">Address</td>
                        <td style="width: 25%">Phone No</td>
                        <td style="width: 25%">Mobile No1</td>
                         
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblGName" runat="server" Text=""></asp:Label></td>

                        <td>
                            <asp:Label ID="lblGAddress" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblGPhoneNo" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblGMobileNo1" runat="server" Text=""></asp:Label></td>

                       
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">Mobile No2</td>
                        <td style="width: 25%">Email</td>
                        <td style="width: 25%">Occupation</td>
                        <td style="width: 25%">Designation</td>
                        

                    </tr>
                    <tr>
                         <td>
                            <asp:Label ID="lblGMobileNo2" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblGEmail" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblGOccupation" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblGDesignation" runat="server" Text=""></asp:Label></td>
                        


                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">Name of Company/Office</td>
                        <td style="width: 25%">Office Phone No.</td>
                        <td style="width: 25%">Relation</td>
                        <td style="width: 25%"></td>
                      

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblGOfficeName" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblGOfficeNo" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblGRelation" runat="server" Text=""></asp:Label></td>
                        <td></td>
                       
                    </tr>
                </table>
                <br>

                <table style="width: 100%" runat="server" id="tblSpouse">
                    <tr>
                        <td colspan="4" style="font-size: 16pt; font-weight: bold">Spouse's Information</td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">Spouse's Name</td>
                        <td style="width: 25%">Address</td>
                        <td style="width: 25%">Phone No</td>
                        <td style="width: 25%">Mobile No1</td>
                        
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSName" runat="server" Text=""></asp:Label></td>

                        <td>
                            <asp:Label ID="lblSAddress" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblSPhoneNo" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblSMobileNo1" runat="server" Text=""></asp:Label></td>

                        
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">Mobile No2</td>
                        <td style="width: 25%">Email</td>
                        <td style="width: 25%">Occupation</td>
                        <td style="width: 25%">Designation</td>
                        

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSMobileNo2" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblSEmail" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblSOccupation" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblSDesignation" runat="server" Text=""></asp:Label></td>
                        
                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">Name of Company/Office</td>
                        <td style="width: 25%">Office Phone No.</td>
                        <td style="width: 25%"></td>
                        <td style="width: 25%"></td>
                       

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSOfficeName" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblSOfficeNo" runat="server" Text=""></asp:Label></td>
                        <td></td>
                        <td></td>
                        
                    </tr>

                </table>

                <br>

                <table style="width: 100%">
                    <tr>
                        <td colspan="4" style="font-size: 16pt; font-weight: bold">SLC's Information</td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">Board</td>
                        <td style="width: 25%">School Name</td>
                        <td style="width: 25%">Zone</td>
                        <td style="width: 25%">District</td>

                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblSLCBoard" runat="server" Text=""></asp:Label></td>

                        <td>
                            <asp:Label ID="lblSchoolName" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblSchoolZone" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblSchoolDistrict" runat="server" Text=""></asp:Label></td>

                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">Address</td>
                        <td style="width: 25%">Passed Year</td>
                        <td style="width: 25%">Passed Division</td>
                        <td style="width: 25%">Percentage</td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSchoolAddress" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblSLCPassedYear" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblSLCDivision" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblSLCPercentage" runat="server" Text=""></asp:Label></td>


                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%; height: 23px;">Symbol No</td>
                        <td style="width: 25%; height: 23px;">Opt Subject 1</td>
                        <td style="width: 25%; height: 23px;">Opt Subject 2</td>
                        <td style="width: 25%; height: 23px;">Opt Subject 3</td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblSLCSymbolNo" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblSLCOPT1" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblSLCOPT2" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblSLCOPT3" runat="server" Text=""></asp:Label></td>


                    </tr>

                </table>

                <br>

                <table style="width: 100%">
                    <tr>
                        <td colspan="4" style="font-size: 16pt; font-weight: bold">+2's Information</td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">Board</td>
                        <td style="width: 25%">College Name</td>
                        <td style="width: 25%">Zone</td>
                        <td style="width: 25%">District</td>

                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblPTBoard" runat="server" Text=""></asp:Label></td>

                        <td>
                            <asp:Label ID="lblCollegeName" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblCollegeZone" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblCollegeDistrict" runat="server" Text=""></asp:Label></td>

                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">Address</td>
                        <td style="width: 25%">Faculty</td>
                        <td style="width: 25%">Passed Year</td>
                        <td style="width: 25%">Passed Division</td>


                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblCollegeAddress" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblPTFaculty" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblPTPassedYear" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblPTDivision" runat="server" Text=""></asp:Label></td>


                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">Percentage</td>
                        <td style="width: 25%;">Symbol No</td>
                        <td style="width: 25%;">Opt Subject 1</td>
                        <td style="width: 25%;">Opt Subject 2</td>


                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPTPercentage" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblPTSymbolNo" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblPTOPT1" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblPTOPT2" runat="server" Text=""></asp:Label></td>


                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%;">Opt Subject 3</td>
                        <td style="width: 25%"></td>
                        <td style="width: 25%;"></td>
                        <td style="width: 25%;"></td>



                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPTOPT3" runat="server" Text=""></asp:Label></td>
                        <td></td>
                        <td></td>
                        <td></td>


                    </tr>

                </table>

                <br>

                <table style="width: 100%">
                    <tr>
                        <td colspan="4" style="font-size: 16pt; font-weight: bold">Document Submitted</td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">Photo</td>
                        <td style="width: 25%">SLC Transcript Photocopy</td>
                        <td style="width: 25%">SLC Character Certificate photocopy</td>
                        <td style="width: 25%">+2 Transcript Photocopy</td>

                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblPhotoAttached" runat="server" Text=""></asp:Label></td>

                        <td>
                            <asp:Label ID="lblSLCTranscriptAttached" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblSLCCharacterAttached" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblPTTranscriptAttached" runat="server" Text=""></asp:Label></td>

                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">+2 Character Certificate Photocopy</td>
                        <td style="width: 25%">Citizenship Photocopy</td>
                        <td style="width: 25%">Government Certificate Photocopy</td>
                        <td style="width: 25%">+2 Provisional Certificate</td>


                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPTCharacterAttached" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblCitizenshipAttached" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblGovernmentCerificatedAttached" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblPTProvisionalAttached" runat="server" Text=""></asp:Label></td>

                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">+2 Migration Certificate</td>
                        <td style="width: 25%;"></td>
                        <td style="width: 25%;"></td>
                        <td style="width: 25%;"></td>


                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPTMigrationAttached" runat="server" Text=""></asp:Label></td>
                        <td></td>
                        <td></td>
                        <td></td>


                    </tr>

                </table>

                <br>

                <table style="width: 100%">
                    <tr>
                        <td colspan="4" style="font-size: 16pt; font-weight: bold">Notification Detail</td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">Primary Number</td>
                        <td style="width: 25%">Primary Number Alert</td>
                        <td style="width: 25%">Secondary Number</td>
                        <td style="width: 25%">Secondary Number Alert</td>

                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblPrimaryNumber" runat="server" Text=""></asp:Label></td>

                        <td>
                            <asp:Label ID="lblPrimaryNoAlert" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblSecondaryNumber" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblSecondaryNoAlert" runat="server" Text=""></asp:Label></td>

                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">Primary Email</td>
                        <td style="width: 25%">Primary Email Alert</td>
                        <td style="width: 25%">Secondary Email</td>
                        <td style="width: 25%">Secondary Email Alert</td>


                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblPrimaryEmail" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblPrimaryEmailAlert" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblSecondaryEmail" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblSecondaryEmailAlert" runat="server" Text=""></asp:Label></td>

                    </tr>
                </table>

                <br>

                <table style="width: 100%">
                    <tr>
                        <td colspan="4" style="font-size: 16pt; font-weight: bold">For Official Use</td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">Entrance Roll Number</td>
                        <td style="width: 25%">Entrance Date</td>
                        <td style="width: 25%">Entrance Marks</td>
                        <td style="width: 25%">Interview Date</td>

                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblEntranceRollNo" runat="server" Text=""></asp:Label></td>

                        <td>
                            <asp:Label ID="lblEntranceDate" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblEntranceMarks" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblInterviewDate" runat="server" Text=""></asp:Label></td>

                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">Interview By</td>
                        <td style="width: 25%">Interview Marks</td>
                        <td style="width: 25%">Admission No</td>
                        <td style="width: 25%">Admission Date</td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblInterviewBy" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblInterviewMarks" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblAdmissionNo" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblAdmissionDate" runat="server" Text=""></asp:Label></td>


                    </tr>

                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">University Regd No</td>
                        <td style="width: 25%">Filled By</td>
                        <td style="width: 25%">Filled Date</td>
                        <td style="width: 25%">Verified By</td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblUniRegdNo" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblFilledBy" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblFilledDate" runat="server" Text=""></asp:Label></td>
                        <td>
                            <asp:Label ID="lblVerifiedBy" runat="server" Text=""></asp:Label></td>

                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr class="heading">
                        <td style="width: 25%">Verified Date</td>
                        <td style="width: 25%"></td>
                        <td style="width: 25%"></td>
                        <td style="width: 25%"></td>

                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblVerifiedDate" runat="server" Text=""></asp:Label></td>
                        <td></td>
                        <td></td>
                        <td></td>

                    </tr>
                    <tr>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr class="heading">
                        <td colspan="4" style="width: 100%">List of Intrests and future plan after graduation?</td>

                    </tr>
                    <tr>
                        <td colspan="4" style="width: 100%">
                            <asp:TextBox ID="txtRemarks" Width="100%" Height="100px" TextMode="MultiLine" runat="server" Text=""></asp:TextBox></td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>
