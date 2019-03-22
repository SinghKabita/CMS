using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;
using System.IO;

public partial class human_resource_reports_Staff_Detail : System.Web.UI.Page
{

    Employees EmpEnt = new Employees();
    EmployeesService EMPSer = new EmployeesService();

    EmptypeTable ETPEnt = new EmptypeTable();
    EmptypeTableService ETPSer = new EmptypeTableService();

    Division DVEnt = new Division();
    DivisionService DVSer = new DivisionService();

    DesignationTable DSGEnt = new DesignationTable();
    DesignationTableService DSGSer = new DesignationTableService();

    DepartmentTable DPEnt = new DepartmentTable();
    DepartmentTableService DPSer = new DepartmentTableService();

    District DSEnt = new District();
    DistrictService DSSer = new DistrictService();

    zone ZEnt = new zone();
    zoneService ZSer = new zoneService();

    COUNTRY CEnt = new COUNTRY();
    COUNTRYService CSrv = new COUNTRYService();

    State SEnt = new State();
    StateService SSrv = new StateService();

    ADDRESS AddEnt = new ADDRESS();
    ADDRESSService AddSrv = new ADDRESSService();

    EMP_ACC_DETAIL EADEnt = new EMP_ACC_DETAIL();
    EMP_ACC_DETAILService EADSrv = new EMP_ACC_DETAILService();

    BankTable BTEnt = new BankTable();
    BankTableService BTSrv = new BankTableService();

    string imgfolder;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string empId = "";
            if (!IsPostBack)
            {
                try
                {
                    empId = Request.QueryString["empId"].ToString();
                    if (empId != "")
                        LoadAllData(empId);
                }
                catch { }
            }
        }
    }

    protected void LoadAllData(string empId)
    {
        LoadEmployeeDetail(empId);

    }

    protected void LoadEmployeeDetail(string empId)
    {

        EmpEnt = new Employees();
        EmpEnt.EMPLOYEEID = empId;
        EmpEnt = (Employees)EMPSer.GetSingle(EmpEnt);
        if (EmpEnt != null)
        {


            if (!string.IsNullOrEmpty(empId))
            {
                imgfolder = Server.MapPath(@"~/images/Employee/") + empId + ".jpg";
                if (File.Exists(imgfolder))
                {
                    imgStudent.ImageUrl = "~/images/Employee/" + empId + ".jpg";

                }
                else
                {
                    if (EmpEnt.GENDER.Trim() == "M")
                    {
                        imgStudent.ImageUrl = "~/images/user/male.jpeg";
                    }
                    if (EmpEnt.GENDER.Trim() == "F")
                    {
                        imgStudent.ImageUrl = "~/images/user/female.jpeg";
                    }
                }
            }

            lblEmployeeId.Text = EmpEnt.EMPLOYEEID;
            lblFullNameEng.Text = EmpEnt.FIRSTNAME + " " + EmpEnt.LASTNAME;
            lblAbbrevation.Text = EmpEnt.Abbrevation;
            lblFullNameNep.Text = EmpEnt.DEVNAGARINAME;
            lblGender.Text = EmpEnt.GENDER;
            lblMaritalStatus.Text = EmpEnt.MARITALSTATUS;
            lblFatherName.Text = EmpEnt.FATHERNAME;
            lblGrandFatherName.Text = EmpEnt.GRANDFATHERNAME;
            lblMotherName.Text = EmpEnt.MOTHERNAME;
            lblSpouseName.Text = EmpEnt.Spouse_name;


            DVEnt = new Division();
            DVEnt.DIVID = EmpEnt.DIVISION;
            DVEnt = (Division)DVSer.GetSingle(DVEnt);
            if (DVEnt != null)
            {
                lblDivision.Text = DVEnt.DIVNAME;
            }


            DSGEnt = new DesignationTable();
            DSGEnt.DESIGNATIONID = EmpEnt.DESIGNATIONID;
            DSGEnt = (DesignationTable)DSGSer.GetSingle(DSGEnt);
            if (DSGEnt != null)
            {
                lblDesignation.Text = DSGEnt.DESIGNATIONNAME;
            }


            DPEnt = new DepartmentTable();
            DPEnt.DEPTID = EmpEnt.DEPARTMENTID;
            DPEnt = (DepartmentTable)DPSer.GetSingle(DPEnt);
            if (DPEnt != null)
            {
                lblDepartment.Text = DPEnt.DEPTNAME;
            }

            ETPEnt = new EmptypeTable();
            ETPEnt.EMPTYPEID = EmpEnt.EMPLOYEETYPE;
            ETPEnt = (EmptypeTable)ETPSer.GetSingle(ETPEnt);
            if (ETPEnt != null)
            {
                lblEmployeeType.Text = ETPEnt.EMPTYPENAME;
            }

            lblEmail.Text = EmpEnt.EMAIL;
            lblPhoneNo.Text = EmpEnt.PHONE;
            lblMobileNo1.Text = EmpEnt.MOBILE1;
            lblMobileNo2.Text = EmpEnt.MOBILE2;

            lblCitizenshipNo.Text = EmpEnt.CITIZENSHIPNO;
            lblDOBBS.Text = EmpEnt.BIRTHDAY + "/" + EmpEnt.BIRTHMONTH + "/" + EmpEnt.BIRTHYEAR;
            lblDOBAD.Text = EmpEnt.BIRTHDATE;
            lblAppointmentDate.Text = EmpEnt.APPOINTMENTDATE;

            #region  Address

            #region for Both

            AddEnt = new ADDRESS();
            AddEnt.ADDRESS_OF_ID = EmpEnt.EMPLOYEEID;
            AddEnt.ADDRESS_TYPE = "Both";
            AddEnt = (ADDRESS)AddSrv.GetSingle(AddEnt);
            if (AddEnt != null)
            {


                DSEnt = new District();
                DSEnt.ID = AddEnt.DISTRICT;
                DSEnt = (District)DSSer.GetSingle(DSEnt);
                if (DSEnt != null)
                {
                    lblDistrictP.Text = DSEnt.DISTRICTNAME;
                    lblDistrictC.Text = DSEnt.DISTRICTNAME;

                }

                CEnt = new COUNTRY();
                CEnt.PK_ID = AddEnt.COUNTRY;
                CEnt = (COUNTRY)CSrv.GetSingle(CEnt);
                if (CEnt != null)
                {
                    lblCountryP.Text = CEnt.COUNTRY_NAME;
                    lblCountryC.Text = CEnt.COUNTRY_NAME;
                }

                SEnt = new State();
                SEnt.STATE_ID = AddEnt.STATE;
                SEnt = (State)SSrv.GetSingle(SEnt);
                if (SEnt != null)
                {
                    lblStateP.Text = SEnt.STATE;
                    lblStateC.Text = SEnt.STATE;
                }

                ZEnt = new zone();
                ZEnt.ZONE_ID = AddEnt.ZONE;
                ZEnt = (zone)ZSer.GetSingle(ZEnt);
                if (ZEnt != null)
                {
                    lblZoneP.Text = ZEnt.ZONE_NAME;
                    lblZoneC.Text = ZEnt.ZONE_NAME;
                }

                lblVDCMuniP.Text = AddEnt.VDC_MUNICIPALITY;
                lblWardnoP.Text = AddEnt.WARD_NO;
                lblStreetNameP.Text = AddEnt.STREET_NAME;
                lblHouseNoP.Text = AddEnt.HOUSE_NO;


                lblVDCMuniC.Text = AddEnt.VDC_MUNICIPALITY;
                lblWardnoC.Text = AddEnt.WARD_NO;
                lblStreetNameC.Text = AddEnt.STREET_NAME;
                lblHouseNoC.Text = AddEnt.HOUSE_NO;
            }
            #endregion

            #region for Permanent

            AddEnt = new ADDRESS();
            AddEnt.ADDRESS_OF_ID = EmpEnt.EMPLOYEEID;
            AddEnt.ADDRESS_TYPE = "P";
            AddEnt = (ADDRESS)AddSrv.GetSingle(AddEnt);
            if (AddEnt != null)
            {

                DSEnt = new District();
                DSEnt.ID = AddEnt.DISTRICT;
                DSEnt = (District)DSSer.GetSingle(DSEnt);
                if (DSEnt != null)
                {
                    lblDistrictP.Text = DSEnt.DISTRICTNAME;

                }

                CEnt = new COUNTRY();
                CEnt.PK_ID = AddEnt.COUNTRY;
                CEnt = (COUNTRY)CSrv.GetSingle(CEnt);
                if (CEnt != null)
                {
                    lblCountryP.Text = CEnt.COUNTRY_NAME;

                }

                SEnt = new State();
                SEnt.STATE_ID = AddEnt.STATE;
                SEnt = (State)SSrv.GetSingle(SEnt);
                if (SEnt != null)
                {
                    lblStateP.Text = SEnt.STATE;

                }

                ZEnt = new zone();
                ZEnt.ZONE_ID = AddEnt.ZONE;
                ZEnt = (zone)ZSer.GetSingle(ZEnt);
                if (ZEnt != null)
                {
                    lblZoneP.Text = ZEnt.ZONE_NAME;

                }

                lblVDCMuniP.Text = AddEnt.VDC_MUNICIPALITY;
                lblWardnoP.Text = AddEnt.WARD_NO;
                lblStreetNameP.Text = AddEnt.STREET_NAME;
                lblHouseNoP.Text = AddEnt.HOUSE_NO;

            }


            #endregion

            #region for Contact

            AddEnt = new ADDRESS();
            AddEnt.ADDRESS_OF_ID = EmpEnt.EMPLOYEEID;
            AddEnt.ADDRESS_TYPE = "C";
            AddEnt = (ADDRESS)AddSrv.GetSingle(AddEnt);
            if (AddEnt != null)
            {

                DSEnt = new District();
                DSEnt.ID = AddEnt.DISTRICT;
                DSEnt = (District)DSSer.GetSingle(DSEnt);
                if (DSEnt != null)
                {
                    lblDistrictC.Text = DSEnt.DISTRICTNAME;

                }

                CEnt = new COUNTRY();
                CEnt.PK_ID = AddEnt.COUNTRY;
                CEnt = (COUNTRY)CSrv.GetSingle(CEnt);
                if (CEnt != null)
                {
                    lblCountryC.Text = CEnt.COUNTRY_NAME;

                }

                SEnt = new State();
                SEnt.STATE_ID = AddEnt.STATE;
                SEnt = (State)SSrv.GetSingle(SEnt);
                if (SEnt != null)
                {
                    lblStateC.Text = SEnt.STATE;

                }

                ZEnt = new zone();
                ZEnt.ZONE_ID = AddEnt.ZONE;
                ZEnt = (zone)ZSer.GetSingle(ZEnt);
                if (ZEnt != null)
                {
                    lblZoneC.Text = ZEnt.ZONE_NAME;

                }

                lblVDCMuniC.Text = AddEnt.VDC_MUNICIPALITY;
                lblWardnoC.Text = AddEnt.WARD_NO;
                lblStreetNameC.Text = AddEnt.STREET_NAME;
                lblHouseNoC.Text = AddEnt.HOUSE_NO;
            }
            #endregion

            #endregion


            lblNName.Text = EmpEnt.NOMINEENAME;
            lblNAddress.Text = EmpEnt.NOMINEEADDRESS;
            lblNRelation.Text = EmpEnt.NOMINEERELATION;
            lblNContactNo.Text = EmpEnt.Nominee_contact;
            lblPFIDNo.Text = EmpEnt.PFIDNO;
            lblCITNo.Text = EmpEnt.CITNO;
            lblPrimaryAcNo.Text = EmpEnt.CURRENTBANKACCNO;

            lblSecondaryAcNo.Text = EmpEnt.SECONDBANKACCNO;
            lblRemarks.Text = EmpEnt.REMARKS;
            lblPanNo.Text = EmpEnt.Panno;

            BTEnt = new BankTable();
            BTEnt.BANKID = EmpEnt.CURRENTBANK;
            BTEnt = (BankTable)BTSrv.GetSingle(BTEnt);
            if (BTEnt != null)
            {

                lblPrimaryBank.Text = BTEnt.BANKNAME;
            }

            BTEnt = new BankTable();
            BTEnt.BANKID = EmpEnt.SECONDBANK;
            BTEnt = (BankTable)BTSrv.GetSingle(BTEnt);
            if (BTEnt != null)
            {

                lblSecondaryBank.Text = BTEnt.BANKNAME;
            }

            #region Employee Account detail
            EADEnt = new EMP_ACC_DETAIL();
            EADEnt.EMPLOYEE_ID = EmpEnt.EMPLOYEEID;
            EADEnt = (EMP_ACC_DETAIL)EADSrv.GetSingle(EADEnt);
            if (EADEnt != null)
            {

                lblBasicSalary.Text = EADEnt.BASIC_SALARY;
                lblPFIDAmount.Text = EADEnt.PF_AMOUNT;
                lblCITAmount.Text = EADEnt.CIT_AMOUNT;
                lblBeneficiaryAmt.Text = EADEnt.BENEFICIARY_AMOUNT;
            }
            #endregion


        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "printPartOfPage();", true);
    }
}