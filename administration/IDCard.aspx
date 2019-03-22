<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPage.master" AutoEventWireup="true" CodeFile="IDCard.aspx.cs" Inherits="administration_IDCard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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



    <style type="text/css">

        #idcard_top {
            top:80px; 
            position:relative; 
            font-size:14px;
            color:#030855;
            font-weight:bold;
        }

        #id_student_detail {

            width:100%;
             color:white;
             font-size:16px;
            
        }

        #name_td {
            font-size:19px; 
            font-weight:bold;
            padding-left:70px;
           
        }

        .idcard_label {
            color:yellow;
            -webkit-text-fill-color:yellow;
            -moz-text-decoration-color:yellow;

        }
        #id_footer {
            top: 105px;
            position: relative;
            font-size: 14px;
            font-weight:bold;
        }

      
        </style>
    <table class="gridtable">
        <tr>
            <td>Batch</td>
             <td> <asp:dropdownlist id="ddlBatch" runat="server" height="22px" AutoPostBack="True" OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"></asp:dropdownlist></td>
           
        </tr>
         <tr>
            <td>Semester</td>
             <td> <asp:dropdownlist id="ddlSemester" height="22px" runat="server"></asp:dropdownlist></td>
        </tr>
         <tr>
            <td>Issue Date</td>
             <td> <asp:Textbox id="txtIssueDate" height="22px" runat="server"></asp:Textbox>
                  
             </td>
        </tr>
         <tr>
            <td>Valid Till</td>
             <td> <asp:Textbox id="txtValidTill" height="22px" runat="server"></asp:Textbox>
                  
             </td>
        </tr>
         <tr> 
            <td><asp:Button id="btnView" runat="server" Text="View" OnClick="btnView_Click"></asp:Button></td>
            <td><asp:Button id="btnPrint" runat="server" Text="Print" OnClick="btnPrint_Click"></asp:Button></td>
        </tr>

    </table>

    <div id="div_print">
    <div id="printall" style="width: 100%">
            <asp:Repeater ID="rptrAllIdCard" runat="server" OnItemDataBound="rptrAllIdCard_ItemDataBound">
                <ItemTemplate>

        <div style="background:url(../images/namunaidcard.jpg) no-repeat center top;background-size: 8.5cm 5.5cm;width:8.5cm;height:5.5cm;font-family:Calibri;margin-bottom:5px; text-align:center">
            <div id="idcard_top" style="top:37px; position:relative; font-size:8px;color:#030855;font-weight:bold">
                Affiliated to PURBANCHAL UNIVERSITY<br>
                Bansbari, Kathmandu, Nepal | GPO Box: 8975, EPC 4144<br>
                Tel.: 4377201, 4377897<br>
                www.namuna.edu.np | ncft@wlink.com.np
            </div>


             <div style="top:34px; position:relative;color:#fff;">
               
                 <table  style="width:100%;font-size:9px;color:#fff;" >
                     <tr>
                         <td colspan="2" style="text-align:center"> <asp:label id="lblStudentCard" runat="server" text="STUDENT IDENTITY CARD NO: 13/72-75"></asp:label></td>
                     </tr>
                     <tr>
                         <td style="vertical-align:top">

                             <table id="id_student_detail" style="width:100%;font-size:8px;color:#fff;margin:0">
                                  <tr>
                         <td colspan="2" id="name_td" style="font-weight:bold;padding-left:40px;padding-bottom:0px">
                             <asp:label id="lblName" class="idcard_label" style="color:yellow;font-size:13px" runat="server" text=""></asp:label>
                             </td>
                        
                     </tr>
                                  <tr>
                         <td>Reg No: <asp:label id="lblRegdno" class="idcard_label" style="color:yellow;" runat="server" Text=' <%# DataBinder.Eval(Container, "DataItem.STUDENT_ID")%>'></asp:label></td>

                     </tr>
                     <tr>
                         <td>FACULTY: <asp:label id="lblFaculty" class="idcard_label" style="color:yellow;" runat="server" text="MANAGEMENT"></asp:label></td>
                         <td>LEVEL: <asp:label id="lblLevel" class="idcard_label" style="color:yellow;" runat="server" text="BACHELOR"></asp:label></td>
                        
                     </tr>
                     <tr>
                         <td colspan="2">PROGRAM: <asp:label id="lblProgram" class="idcard_label" style="color:yellow;" runat="server" text="BACHELOR OF FASHION DESIGN"></asp:label></td>
                        
                     </tr>
                                  <tr>
                        <td>DOB: <asp:label id="lblDOB" runat="server" class="idcard_label" style="color:yellow;" text=""></asp:label></td> 
                         <td>CONTACT: <asp:label id="lblContact" runat="server" class="idcard_label" style="color:yellow;" text=""></asp:label></td> 
                        
                     </tr>
                     <tr>
                        <td>ISSUED ON:<asp:label id="lblIssueOn" runat="server" class="idcard_label" style="color:yellow;" text=""></asp:label></td>  
                         <td>VALID TILL:<asp:label id="lblValidTill" runat="server" class="idcard_label" style="color:yellow;" text=""></asp:label></td>  
                        
                     </tr>
                             </table>


                         </td>
                          <td>
                              <asp:Image ID="imgStudent" runat="server" ImageUrl="~/images/testpp.jpg" height="100px" Width="85px" style="float:right;padding-right:8px;position:relative;top:0px;left: 0px;" />
                              <asp:Image ID="imgStamp" runat="server" ImageUrl="~/images/stamp.png" height="45px" style="float:right; padding-right:10px;top:75px;right:65px;position:absolute;"/>

                          </td>
                     </tr>
                    
                 </table>
              
            </div>
            <div id="id_footer" style=" top: 35px;position: relative;font-size: 9px;font-weight:bold;">
                This card is not tranferable. If lost or found please contact:01-4377201, 4377897
            </div>
             
        </div>

                    </ItemTemplate>
          
                  </asp:Repeater>


    </div>
        </div>
</asp:Content>

