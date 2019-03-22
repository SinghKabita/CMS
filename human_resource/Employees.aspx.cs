using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Entity.Framework;
using Entity.Components;
using Service.Components;

using DataHelper.Framework;
using DataAccess.Framework;
using System.Collections;
using System.Data;

public partial class human_resource_Employees : System.Web.UI.Page
{

    String depname;
    String divname;
    String designame;
    String offcode;
    String empname;
    String Zname;
    String dsname;
    String cbname;
    String sbname;
    String offname;


    Employees EEnttemp = new Employees();
    EmployeesService ESertemp = new EmployeesService();

    Employees EEnt = new Employees();
    EmployeesService ESer = new EmployeesService();

    DesignationTable DEnt = new DesignationTable();
    DesignationTableService DSer = new DesignationTableService();

    DepartmentTable DPEnt = new DepartmentTable();
    DepartmentTableService DPSer = new DepartmentTableService();

    EmptypeTable ETEnt = new EmptypeTable();
    EmptypeTableService ETSer = new EmptypeTableService();

    zone ZEnt = new zone();
    zoneService ZSer = new zoneService();

    BankTable BEnt = new BankTable();
    BankTableService BSer = new BankTableService();

    OFFICE OEnt = new OFFICE();
    officeService OSer = new officeService();

    Division DVEnt = new Division();
    DivisionService DVSer = new DivisionService();

    COUNTRY CEnt = new COUNTRY();
    COUNTRYService CSrv = new COUNTRYService();

    State STEnt = new State();
    StateService STSrv = new StateService();

    District DREnt = new District();
    DistrictService DRSer = new DistrictService();

    Promotion PEnt = new Promotion();
    PromotionService PSer = new PromotionService();

    Resignation RSEnt = new Resignation();
    ResignationService RSSer = new ResignationService();

    ADDRESS AEnt = new ADDRESS();
    ADDRESSService ASrv = new ADDRESSService();

    EMP_ACC_DETAIL EADEnt = new EMP_ACC_DETAIL();
    EMP_ACC_DETAILService EADSrv = new EMP_ACC_DETAILService();

    Entity.Components.Login LEnt = new Entity.Components.Login();
    LoginService LSer = new LoginService();

    HelperFunction hf = new HelperFunction();

    EntityList theList = new EntityList();

    DistributedTransaction DT = new DistributedTransaction();
    string employees = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //btnPromote.Visible = false;
            //btnResignation.Visible = false;

            btnBack.Visible = false;
            LoadData();
            LoadCountryPA();
            LoadCountryCA();
            LoadState_PA();
            LoadState_CA();
            LoadZone();
            LoadPADistrict();
            LoadTADistrict();

            string employees = "";
            employees = (Request.QueryString.Get("EmpId"));
            if (employees != null)
            {
                LoadEmployeesDetail(employees);
                LoadPromote();
                LoadResignation();
                btnBack.Visible = true;
            }
            else
            {
                btnBack.Visible = false;
            }
        }
    }

    protected void LoadData()
    {
        LoadDistrict();
        LoadDivision();
        LoadDesignation();
        LoadDepartment();
        LoadEmptype();
        LoadZone();
        LoadBank1();
        LoadBank2();

        LoadDivision();

    }

    protected void LoadDistrict()
    {
        DREnt = new District();

        //ddlDistrict.DataSource = DRSer.GetAll(DREnt);
        //ddlDistrict.DataTextField = "DISTRICTNAME";
        //ddlDistrict.DataValueField = "ID";
        //ddlDistrict.DataBind();
        //ddlDistrict.Items.Insert(0, "Please Select");

        //ddlDistrict_C.DataSource = DRSer.GetAll(DREnt);
        //ddlDistrict_C.DataTextField = "DISTRICTNAME";
        //ddlDistrict_C.DataValueField = "ID";
        //ddlDistrict_C.DataBind();
        //ddlDistrict_C.Items.Insert(0, "Please Select");
    }

    protected void LoadCountryPA()
    {
        CEnt = new COUNTRY();
        ddlCountryPA.DataSource = CSrv.GetAll(CEnt);
        ddlCountryPA.DataTextField = "COUNTRY_NAME";
        ddlCountryPA.DataValueField = "PK_ID";
        ddlCountryPA.DataBind();
    }

    protected void LoadCountryCA()
    {
        CEnt = new COUNTRY();
        ddlCountry_CA.DataSource = CSrv.GetAll(CEnt);
        ddlCountry_CA.DataTextField = "COUNTRY_NAME";
        ddlCountry_CA.DataValueField = "PK_ID";
        ddlCountry_CA.DataBind();
        ddlCountry_CA.SelectedIndex = 0;
        ddlCountry_CA.Enabled = false;
    }

    protected void LoadState_PA()
    {
        State STEnt = new State();
        ddlStatePA.DataSource = STSrv.GetAll(STEnt);
        ddlStatePA.DataTextField = "STATE";
        ddlStatePA.DataValueField = "STATE_ID";
        ddlStatePA.DataBind();
        ddlStatePA.Items.Insert(0, "Select");
    }

    protected void LoadState_CA()
    {
        State STEnt = new State();
        ddlState_CA.DataSource = STSrv.GetAll(STEnt);
        ddlState_CA.DataTextField = "STATE";
        ddlState_CA.DataValueField = "STATE_ID";
        ddlState_CA.DataBind();
        ddlState_CA.Items.Insert(0, "Select");
    }

    protected void LoadPADistrict()
    {
        DREnt = new District();
        DREnt.ZONE_ID = ddlPA_Zone.SelectedValue;
        ddlPA_DISTRICT.DataSource = DRSer.GetAll(DREnt);
        ddlPA_DISTRICT.DataTextField = "DISTRICTNAME";
        ddlPA_DISTRICT.DataValueField = "ID";
        ddlPA_DISTRICT.DataBind();
        ddlPA_DISTRICT.Items.Insert(0, "Select");
    }

    protected void LoadTADistrict()
    {

        DREnt = new District();
        DREnt.ZONE_ID = ddlTA_Zone.SelectedValue;
        ddlTA_DISTRICT.DataSource = DRSer.GetAll(DREnt);
        ddlTA_DISTRICT.DataTextField = "DISTRICTNAME";
        ddlTA_DISTRICT.DataValueField = "ID";
        ddlTA_DISTRICT.DataBind();
        ddlTA_DISTRICT.Items.Insert(0, "Select");
    }

    protected void LoadDivision()
    {
        DVEnt = new Division();

        ddlDiv.DataSource = DVSer.GetAll(DVEnt);
        ddlDiv.DataTextField = "DIVNAME";
        ddlDiv.DataValueField = "DIVID";
        ddlDiv.DataBind();

    }

    protected void LoadDesignation()
    {
        DEnt = new DesignationTable();

        ddlDesignation.DataSource = DSer.GetAll(DEnt);
        ddlDesignation.DataTextField = "DESIGNATIONNAME";
        ddlDesignation.DataValueField = "DESIGNATIONID";
        ddlDesignation.DataBind();
        //ddlDesignation.Items.Insert(0, "Please Select");

    }

    protected void LoadDepartment()
    {
        DPEnt = new DepartmentTable();

        ddlDept.DataSource = DPSer.GetAll(DPEnt);
        ddlDept.DataTextField = "DEPTNAME";
        ddlDept.DataValueField = "DEPTID";
        ddlDept.DataBind();
        ddlDept.Items.Insert(0, "Please Select");

    }

    protected void LoadEmptype()
    {
        ETEnt = new EmptypeTable();

        ddlEmployeeType.DataSource = ETSer.GetAll(ETEnt);
        ddlEmployeeType.DataTextField = "EMPTYPENAME";
        ddlEmployeeType.DataValueField = "EMPTYPEID";
        ddlEmployeeType.DataBind();
        ddlEmployeeType.Items.Insert(0, "Please Select");
    }

    protected void LoadZone()
    {
        ZEnt = new zone();

        ddlPA_Zone.DataSource = ZSer.GetAll(ZEnt);
        ddlPA_Zone.DataTextField = "ZONE_NAME";
        ddlPA_Zone.DataValueField = "ZONE_ID";
        ddlPA_Zone.DataBind();
        ddlPA_Zone.Items.Insert(0, "Select");



        ZEnt = new zone();
        ddlTA_Zone.DataSource = ZSer.GetAll(ZEnt);
        ddlTA_Zone.DataTextField = "ZONE_NAME";
        ddlTA_Zone.DataValueField = "ZONE_ID";
        ddlTA_Zone.DataBind();
        ddlTA_Zone.Items.Insert(0, "Select");

    }

    protected void LoadBank1()
    {
        BEnt = new BankTable();

        ddlBankName.DataSource = BSer.GetAll(BEnt);
        ddlBankName.DataTextField = "BANKNAME";
        ddlBankName.DataValueField = "BANKID";
        ddlBankName.DataBind();
        ddlBankName.Items.Insert(0, "Please Select");

    }
    protected void LoadBank2()
    {
        BEnt = new BankTable();

        ddBankName2.DataSource = BSer.GetAll(BEnt);
        ddBankName2.DataTextField = "BANKNAME";
        ddBankName2.DataValueField = "BANKID";
        ddBankName2.DataBind();
        ddBankName2.Items.Insert(0, "Please Select");
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/administration/reports/staff_list.aspx");
    }
    protected void txtIdNo_TextChanged(object sender, EventArgs e)
    {
        LoadEmployeesDetail(txtIdNo.Text);

    }

    protected void LoadEmployeesDetail(string employeeid)
    {

        EEnt = new Employees();
        EEnt.EMPLOYEEID = employeeid;
        EEnt = (Employees)ESer.GetSingle(EEnt);

        if (EEnt != null)
        {
            txtIdNo.Text = employeeid;

            txtFirstname.Text = EEnt.FIRSTNAME;
            txtLastname.Text = EEnt.LASTNAME;
            ddlDesignation.SelectedValue = EEnt.DESIGNATIONID;
            if (EEnt.DEPARTMENTID == "")
            {
                ddlDept.SelectedIndex = 0;
            }
            else
            {
                ddlDept.SelectedValue = EEnt.DEPARTMENTID;
            }
            ddlDesignation.SelectedValue = EEnt.DESIGNATIONID;
            txtJobTitle.Text = EEnt.JOBTITLE;
            ddlDiv.SelectedValue = EEnt.DIVISION;
            txtPhone.Text = EEnt.PHONE;
            txtMobile1.Text = EEnt.MOBILE1;
            txtMobile2.Text = EEnt.MOBILE2;
            txtFatherName.Text = EEnt.FATHERNAME;
            txtGrandFName.Text = EEnt.GRANDFATHERNAME;
            if (EEnt.EMPLOYEETYPE == "")
            {
                ddlEmployeeType.SelectedIndex = 0;
            }
            else
            {
                ddlEmployeeType.SelectedValue = EEnt.EMPLOYEETYPE;
            }

            if (EEnt.GENDER == "M")
            {
                rbtnGender.SelectedValue = "M";
            }
            else if (EEnt.GENDER == "F")
            {
                rbtnGender.SelectedValue = "F";
            }

            rbtnMaritalStatus.SelectedValue = EEnt.MARITALSTATUS;

            txtCtzno.Text = EEnt.CITIZENSHIPNO;

            txtNomineeName.Text = EEnt.NOMINEENAME;
            txtNomineeAdd.Text = EEnt.NOMINEEADDRESS;
            txtNomineeRelation.Text = EEnt.NOMINEERELATION;
            txtPfidno.Text = EEnt.PFIDNO;
            txtCitno.Text = EEnt.CITNO;

            txtEmail.Text = EEnt.EMAIL;
            txtDevName.Text = EEnt.DEVNAGARINAME;
            txtMotherName.Text = EEnt.MOTHERNAME;
            if (EEnt.CURRENTBANK == "")
            {
                ddlBankName.SelectedIndex = 0;
            }
            else
            {
                ddlBankName.SelectedValue = EEnt.CURRENTBANK;
            }
            txtCurrentbankaccount.Text = EEnt.CURRENTBANKACCNO;

            EADEnt = new EMP_ACC_DETAIL();
            EADEnt.EMPLOYEE_ID = EEnt.EMPLOYEEID;
            EADEnt = (EMP_ACC_DETAIL)EADSrv.GetSingle(EADEnt);
            if (EADEnt != null)
            {
                txtPfidAmt.Text = EADEnt.PF_AMOUNT;
                txtBasicSal.Text = EADEnt.BASIC_SALARY;
                txtBeneficiaryAmt.Text = EADEnt.BENEFICIARY_AMOUNT;
                txtCitPer.Text = EADEnt.CIT_AMOUNT;
            }

            if (EEnt.SECONDBANK == "")
            {
                ddBankName2.SelectedIndex = 0;
            }
            else
            {
                ddBankName2.SelectedValue = EEnt.SECONDBANK;
            }

            txtSecondbankaccount.Text = EEnt.SECONDBANKACCNO;

            //if (EEnt.STATUS == "0")
            //{
            //    chkStatus.Checked = false;
            //}
            //else
            //{
            //    chkStatus.Checked = true;
            //}

            txtRemarks.Text = EEnt.REMARKS;
            txtBirthDay.Text = EEnt.BIRTHDAY;
            txtBirthMonth.Text = EEnt.BIRTHMONTH;
            txtBirthYear.Text = EEnt.BIRTHYEAR;
            GetEnglishDate();
            txtJobstartDay.Text = EEnt.APPOINTMENTDAY;
            txtJobstartMonth.Text = EEnt.APPOINTMENTMONTH;
            txtJobstartYear.Text = EEnt.APPOINTMENTYEAR;

            txtAbbrevation.Text = EEnt.Abbrevation;

            txtSpouseName.Text = EEnt.Spouse_name;

            txtNomineeContact.Text = EEnt.Nominee_contact;
            txtPANNo.Text = EEnt.Panno;


            #region Address

            AEnt = new ADDRESS();
            AEnt.ADDRESS_OF_ID = EEnt.EMPLOYEEID;
            AEnt.ADDRESS_OF = "Employee";
            AEnt.ADDRESS_TYPE = "Both";
            AEnt = (ADDRESS)ASrv.GetSingle(AEnt);
            if (AEnt != null)
            {

                txtPA_HOUSE_NO.Text = AEnt.HOUSE_NO;
                txtPA_STREET.Text = AEnt.STREET_NAME;
                txtPA_VDC_MUNI.Text = AEnt.VDC_MUNICIPALITY;
                txtPA_WARD_NO.Text = AEnt.WARD_NO;
                LoadCountryPA();
                ddlCountryPA.SelectedValue = AEnt.COUNTRY;
                LoadState_PA();
                if (AEnt.STATE == "")
                {
                    ddlStatePA.SelectedIndex = 0;
                }
                else
                {
                    ddlStatePA.SelectedValue = AEnt.STATE;
                }
                ZEnt = new zone();
                ddlPA_Zone.DataSource = ZSer.GetAll(ZEnt);
                ddlPA_Zone.DataTextField = "ZONE_NAME";
                ddlPA_Zone.DataValueField = "ZONE_ID";
                ddlPA_Zone.DataBind();
                ddlPA_Zone.Items.Insert(0, "Select");
                if (AEnt.ZONE == "")
                {
                    ddlPA_Zone.SelectedIndex = 0;
                }
                else
                {
                    ddlPA_Zone.SelectedValue = AEnt.ZONE;
                }
                LoadPADistrict();
                if (AEnt.DISTRICT == "")
                {
                    ddlPA_DISTRICT.SelectedIndex = 0;
                }
                else
                {
                    ddlPA_DISTRICT.SelectedValue = AEnt.DISTRICT;
                }

                //if (AEnt.ADDRESS_TYPE == "Both")
                //{
                ddlContactAddress.SelectedIndex = 1;
                //}
                //else
                //{
                //    ddlContactAddress.SelectedIndex = 0;
                //}

                txtTA_HOUSE_NO.Text = AEnt.HOUSE_NO;
                txtTA_STREET.Text = AEnt.STREET_NAME;
                txtTA_VDC_MUNI.Text = AEnt.VDC_MUNICIPALITY;
                txtTA_WARD_NO.Text = AEnt.WARD_NO;
                LoadCountryCA();
                ddlCountry_CA.SelectedValue = AEnt.COUNTRY;
                LoadState_CA();
                if (AEnt.STATE == "")
                {
                    ddlState_CA.SelectedIndex = 0;
                }
                else
                {
                    ddlState_CA.SelectedValue = AEnt.STATE;
                }
                ZEnt = new zone();
                ddlTA_Zone.DataSource = ZSer.GetAll(ZEnt);
                ddlTA_Zone.DataTextField = "ZONE_NAME";
                ddlTA_Zone.DataValueField = "ZONE_ID";
                ddlTA_Zone.DataBind();
                ddlTA_Zone.Items.Insert(0, "Select");
                if (AEnt.ZONE == "")
                {
                    ddlTA_Zone.SelectedIndex = 0;
                }
                else
                {
                    ddlTA_Zone.SelectedValue = AEnt.ZONE;
                }
                LoadTADistrict();
                if (AEnt.DISTRICT == "")
                {
                    ddlTA_DISTRICT.SelectedIndex = 0;
                }
                else
                {
                    ddlTA_DISTRICT.SelectedValue = AEnt.DISTRICT;
                }
            }


            AEnt = new ADDRESS();
            AEnt.ADDRESS_OF_ID = EEnt.EMPLOYEEID;
            AEnt.ADDRESS_OF = "Employee";
            AEnt.ADDRESS_TYPE = "P";
            AEnt = (ADDRESS)ASrv.GetSingle(AEnt);
            if (AEnt != null)
            {
                if (AEnt.COUNTRY != "1")
                {
                    lblState_PA.Visible = false;
                    lblZone_PA.Visible = false;
                    lblDistrict_PA.Visible = false;
                    lblSameAsOf.Visible = false;

                }
                else
                {
                    lblState_PA.Visible = true;
                    lblZone_PA.Visible = true;
                    lblDistrict_PA.Visible = true;
                    lblSameAsOf.Visible = true;
                }

                txtPA_HOUSE_NO.Text = AEnt.HOUSE_NO;
                txtPA_STREET.Text = AEnt.STREET_NAME;
                txtPA_VDC_MUNI.Text = AEnt.VDC_MUNICIPALITY;
                txtPA_WARD_NO.Text = AEnt.WARD_NO;
                LoadCountryPA();
                ddlCountryPA.SelectedValue = AEnt.COUNTRY;
                LoadState_PA();
                if (AEnt.STATE == "")
                {
                    ddlStatePA.SelectedIndex = 0;
                }
                else
                {
                    ddlStatePA.SelectedValue = AEnt.STATE;
                }
                ZEnt = new zone();
                ddlPA_Zone.DataSource = ZSer.GetAll(ZEnt);
                ddlPA_Zone.DataTextField = "ZONE_NAME";
                ddlPA_Zone.DataValueField = "ZONE_ID";
                ddlPA_Zone.DataBind();
                ddlPA_Zone.Items.Insert(0, "Select");
                if (AEnt.ZONE == "")
                {
                    ddlPA_Zone.SelectedIndex = 0;
                }
                else
                {
                    ddlPA_Zone.SelectedValue = AEnt.ZONE;
                }
                LoadPADistrict();
                if (AEnt.DISTRICT == "")
                {
                    ddlPA_DISTRICT.SelectedIndex = 0;
                }
                else
                {
                    ddlPA_DISTRICT.SelectedValue = AEnt.DISTRICT;
                }

            }

            AEnt = new ADDRESS();
            AEnt.ADDRESS_OF_ID = EEnt.EMPLOYEEID;
            AEnt.ADDRESS_OF = "Employee";
            AEnt.ADDRESS_TYPE = "C";
            AEnt = (ADDRESS)ASrv.GetSingle(AEnt);
            if (AEnt != null)
            {

                txtTA_HOUSE_NO.Text = AEnt.HOUSE_NO;
                txtTA_STREET.Text = AEnt.STREET_NAME;
                txtTA_VDC_MUNI.Text = AEnt.VDC_MUNICIPALITY;
                txtTA_WARD_NO.Text = AEnt.WARD_NO;
                LoadCountryCA();
                ddlCountry_CA.SelectedValue = AEnt.COUNTRY;
                LoadState_CA();
                if (AEnt.STATE == "")
                {
                    ddlState_CA.SelectedIndex = 0;
                }
                else
                {
                    ddlState_CA.SelectedValue = AEnt.STATE;
                }
                ZEnt = new zone();
                ddlTA_Zone.DataSource = ZSer.GetAll(ZEnt);
                ddlTA_Zone.DataTextField = "ZONE_NAME";
                ddlTA_Zone.DataValueField = "ZONE_ID";
                ddlTA_Zone.DataBind();
                ddlTA_Zone.Items.Insert(0, "Select");
                if (AEnt.ZONE == "")
                {
                    ddlTA_Zone.SelectedIndex = 0;
                }
                else
                {
                    ddlTA_Zone.SelectedValue = AEnt.ZONE;
                }
                LoadTADistrict();
                if (AEnt.DISTRICT == "")
                {
                    ddlTA_DISTRICT.SelectedIndex = 0;
                }
                else
                {
                    ddlTA_DISTRICT.SelectedValue = AEnt.DISTRICT;
                }

            }

            #endregion

            //btnPromote.Visible = true;
            //btnResignation.Visible = true;
            LoadPromote();
            LoadResignation();

            choosePayType();

        }
        else
        {

            //btnPromote.Visible = false;
            //btnResignation.Visible = false;
            string tempid = txtIdNo.Text;
            ClearField();
            txtIdNo.Text = tempid;
        }

    }

    protected void LoadPromote()
    {

        //EEnt = new Employees();
        //EEnt.EMPLOYEEID = txtIdNo.Text;
        //EEnt = (Employees)ESer.GetSingle(EEnt);

        //if (EEnt != null)
        //{
        //    txtEmpIdP.Text = EEnt.EMPLOYEEID;
        //    txtEmpNameP.Text = EEnt.FIRSTNAME + " " + EEnt.LASTNAME;

        //    DEnt = new DesignationTable();
        //    DEnt.DESIGNATIONID = EEnt.DESIGNATIONID;
        //    DEnt = (DesignationTable)DSer.GetSingle(DEnt);
        //    if (DEnt != null)
        //    {
        //        txtDesignation.Text = DEnt.DESIGNATIONNAME;

        //    }

        //    lblTempId.Text = EEnt.DIVISION;

        //    DEnt = new DesignationTable();
        //    DEnt.DIVID = ddlDiv.SelectedValue;

        //    ddlPromo.DataSource = DSer.GetAll(DEnt);
        //    ddlPromo.DataTextField = "DESIGNATIONNAME";
        //    ddlPromo.DataValueField = "DESIGNATIONID";
        //    ddlPromo.DataBind();
        //    ddlPromo.Items.Insert(0, "Select");
        //    ddlPromo.SelectedValue = EEnt.DESIGNATIONID;

        //}

    }

    protected void LoadResignation()
    {

        //EEnt = new Employees();
        //EEnt.EMPLOYEEID = txtIdNo.Text;
        //EEnt = (Employees)ESer.GetSingle(EEnt);

        //if (EEnt != null)
        //{
        //    txtEmpIdR.Text = EEnt.EMPLOYEEID;
        //    txtEmpNameR.Text = EEnt.FIRSTNAME + " " + EEnt.LASTNAME;

        //}

    }

    protected void ddlDiv_SelectedIndexChanged(object sender, EventArgs e)
    {
        DEnt = new DesignationTable();
        DEnt.DIVID = ddlDiv.SelectedValue;

        ddlDesignation.DataSource = DSer.GetAll(DEnt);
        ddlDesignation.DataTextField = "DESIGNATIONNAME";
        ddlDesignation.DataValueField = "DESIGNATIONID";
        ddlDesignation.DataBind();

        choosePayType();
    }
    protected void ddlZone_SelectedIndexChanged(object sender, EventArgs e)
    {
        DREnt = new District();
        //DREnt.ZONE_ID = ddlZone.SelectedValue;

        //ddlDistrict.DataSource = DRSer.GetAll(DREnt);
        //ddlDistrict.DataTextField = "DISTRICTNAME";
        //ddlDistrict.DataValueField = "ID";
        //ddlDistrict.DataBind();
        //ddlDistrict.Items.Insert(0, "Please Select");
    }

    protected void saveEmployee()
    {

        DT = new DistributedTransaction();
        string filename = "";
        string ctznspname = "";

        #region employee

        //if (txtBirthDay.Text != "" && txtBirthMonth.Text != "" && txtBirthYear.Text != "")
        //{

        EEnt = new Employees();

        EEnt.EMPLOYEEID = txtIdNo.Text;
        EEnt.FIRSTNAME = txtFirstname.Text;
        EEnt.LASTNAME = txtLastname.Text;
        EEnt.Abbrevation = txtAbbrevation.Text;
        EEnt.DESIGNATIONID = ddlDesignation.SelectedValue;
        EEnt.JOBTITLE = txtJobTitle.Text;

        if (ddlDept.SelectedValue == "Please Select")
        {
            EEnt.DEPARTMENTID = "";
        }
        else
        {
            EEnt.DEPARTMENTID = ddlDept.SelectedValue;
        }
        EEnt.BIRTHDATE = hf.ConvertNepaliTOEnglish(txtBirthDay.Text, txtBirthMonth.Text, txtBirthYear.Text);
        EEnt.EMAIL = txtEmail.Text;
        EEnt.PHONE = txtPhone.Text;
        EEnt.MOBILE1 = txtMobile1.Text;
        EEnt.MOBILE2 = txtMobile2.Text;

        //for  saving image in folder
        try
        {
            filename = txtIdNo.Text + fileUpload1.FileName.Substring(fileUpload1.FileName.IndexOf('.'));
            if (fileUpload1.HasFile)
            {
                fileUpload1.SaveAs(Server.MapPath("~/images/Employee/" + filename));
                // EEnt.IMAGE = ddlItemCategory.SelectedValue + "_" + txtItemCode.Text;
            }
        }
        catch
        {
        }

        //for  saving cv in folder
        try
        {
            if (FileUploadCV1.HasFile)
            {
                string file1name = txtIdNo.Text + "_cv" + FileUploadCV1.FileName.Substring(FileUploadCV1.FileName.IndexOf('.'));
                FileUploadCV1.SaveAs(Server.MapPath("~/images/Employee_CV/" + file1name));
            }
        }
        catch
        {
        }

        //for  saving citizenhip image in folder
        try
        {
            ctznspname = txtIdNo.Text + "_ctz" + FileUploadCitiznshp.FileName.Substring(FileUploadCitiznshp.FileName.IndexOf('.'));
            if (FileUploadCitiznshp.HasFile)
            {
                FileUploadCitiznshp.SaveAs(Server.MapPath("~/images/Employee_Citizenship/" + ctznspname));
                // EEnt.IMAGE = ddlItemCategory.SelectedValue + "_" + txtItemCode.Text;
            }
        }
        catch
        {
        }

        //for  saving certificates in folder
        try
        {
            if (FileUploadCertificate.HasFile)
            {

                string file1name = txtIdNo.Text + "_certificate" + FileUploadCertificate.FileName.Substring(FileUploadCertificate.FileName.IndexOf('.'));
                FileUploadCertificate.SaveAs(Server.MapPath("~/images/Employee_Certificates/" + file1name));
            }
        }
        catch
        {
        }
        EEnt.DEVNAGARINAME = txtDevName.Text;
        EEnt.FATHERNAME = txtFatherName.Text;
        EEnt.MOTHERNAME = txtMotherName.Text;
        EEnt.GRANDFATHERNAME = txtGrandFName.Text;

        EEnt.DIVISION = ddlDiv.SelectedValue;
        if (ddlEmployeeType.SelectedValue == "Please Select")
        {

            EEnt.EMPLOYEETYPE = "";
        }
        else
        {
            EEnt.EMPLOYEETYPE = ddlEmployeeType.SelectedValue;
        }
        EEnt.STATUS = "0";
        EEnt.GENDER = rbtnGender.SelectedValue;
        EEnt.MARITALSTATUS = rbtnMaritalStatus.SelectedValue;

        EEnt.CITIZENSHIPNO = txtCtzno.Text;
        EEnt.APPOINTMENTDATE = hf.ConvertNepaliTOEnglish(txtJobstartDay.Text, txtJobstartMonth.Text, txtJobstartYear.Text);

        EEnt.NOMINEENAME = txtNomineeName.Text;
        EEnt.NOMINEEADDRESS = txtNomineeAdd.Text;
        EEnt.NOMINEERELATION = txtNomineeRelation.Text;
        EEnt.PFIDNO = txtPfidno.Text;
        EEnt.CITNO = txtCitno.Text;

        if (ddlBankName.SelectedValue == "Please Select")
        {
            EEnt.CURRENTBANK = "";
        }
        else
        {
            EEnt.CURRENTBANK = ddlBankName.SelectedValue;
        }

        EEnt.CURRENTBANKACCNO = txtCurrentbankaccount.Text;
        if (ddBankName2.SelectedValue == "Please Select")
        {
            EEnt.SECONDBANK = "";
        }
        else
        {
            EEnt.SECONDBANK = ddBankName2.SelectedValue;
        }
        EEnt.SECONDBANKACCNO = txtSecondbankaccount.Text;

        //if (chkStatus.Checked == true)
        //    EEnt.STATUS = "1";
        //else
        //    EEnt.STATUS = "0";

        EEnt.REMARKS = txtRemarks.Text;
        EEnt.BIRTHDAY = txtBirthDay.Text;
        EEnt.BIRTHMONTH = txtBirthMonth.Text;
        EEnt.BIRTHYEAR = txtBirthYear.Text;
        EEnt.APPOINTMENTDAY = txtJobstartDay.Text;
        EEnt.APPOINTMENTMONTH = txtJobstartMonth.Text;
        EEnt.APPOINTMENTYEAR = txtJobstartYear.Text;

        EEnt.Spouse_name = txtSpouseName.Text;

        EEnt.Nominee_contact = txtNomineeContact.Text;
        EEnt.Panno = txtPANNo.Text;

        ESer.Insert(EEnt, DT);
        //}

        //else
        //{
        //    HelperFunction.MsgBox(this, this.GetType(), "Data Not Saved. Date of Birth must be inserted");
        //}

        #endregion

        #region Address Detail

        if (ddlCountry_CA.SelectedValue != "2")
        {

            if (ddlCountryPA.SelectedValue == "1")
            {
                #region for Nepal
                if (ddlContactAddress.SelectedValue != "Select")
                {

                    #region both ADDRESS as same

                    AEnt = new ADDRESS();
                    AEnt.COUNTRY = ddlCountryPA.SelectedValue;

                    if (ddlStatePA.SelectedIndex == 0)
                    {

                        AEnt.STATE = "";
                    }
                    else
                    {
                        AEnt.STATE = ddlStatePA.SelectedValue;
                    }


                    if (ddlPA_Zone.SelectedIndex == 0)
                    {
                        AEnt.ZONE = "";
                    }
                    else
                    {

                        AEnt.ZONE = ddlPA_Zone.SelectedValue;
                    }

                    if (ddlPA_DISTRICT.SelectedIndex == 0)
                    {
                        AEnt.DISTRICT = "";
                    }
                    else
                    {
                        AEnt.DISTRICT = ddlPA_DISTRICT.SelectedValue;
                    }

                    AEnt.VDC_MUNICIPALITY = txtPA_VDC_MUNI.Text;
                    AEnt.WARD_NO = txtPA_WARD_NO.Text;
                    AEnt.STREET_NAME = txtPA_STREET.Text;
                    AEnt.HOUSE_NO = txtPA_HOUSE_NO.Text;
                    AEnt.ADDRESS_OF = "Employee";
                    AEnt.ADDRESS_OF_ID = txtIdNo.Text; ;
                    AEnt.ADDRESS_TYPE = "Both";

                    ASrv.Insert(AEnt, DT);



                    #endregion
                }
                else
                {
                    #region permanent Address

                    AEnt = new ADDRESS();
                    AEnt.COUNTRY = ddlCountryPA.SelectedValue;

                    if (ddlStatePA.SelectedIndex == 0)
                    {

                        AEnt.STATE = "";
                    }
                    else
                    {
                        AEnt.STATE = ddlStatePA.SelectedValue;
                    }


                    if (ddlPA_Zone.SelectedIndex == 0)
                    {
                        AEnt.ZONE = "";
                    }
                    else
                    {

                        AEnt.ZONE = ddlPA_Zone.SelectedValue;
                    }

                    if (ddlPA_DISTRICT.SelectedIndex == 0)
                    {
                        AEnt.DISTRICT = "";
                    }
                    else
                    {
                        AEnt.DISTRICT = ddlPA_DISTRICT.SelectedValue;
                    }
                    AEnt.VDC_MUNICIPALITY = txtPA_VDC_MUNI.Text;
                    AEnt.WARD_NO = txtPA_WARD_NO.Text;
                    AEnt.STREET_NAME = txtPA_STREET.Text;
                    AEnt.HOUSE_NO = txtPA_HOUSE_NO.Text;
                    AEnt.ADDRESS_OF = "Employee";
                    AEnt.ADDRESS_OF_ID = txtIdNo.Text; ;
                    AEnt.ADDRESS_TYPE = "P";

                    ASrv.Insert(AEnt, DT);

                    #endregion

                    #region Contact Address

                    AEnt = new ADDRESS();
                    AEnt.COUNTRY = ddlCountry_CA.SelectedValue;

                    if (ddlStatePA.SelectedIndex == 0)
                    {

                        AEnt.STATE = "";
                    }
                    else
                    {
                        AEnt.STATE = ddlStatePA.SelectedValue;
                    }


                    if (ddlPA_Zone.SelectedIndex == 0)
                    {
                        AEnt.ZONE = "";
                    }
                    else
                    {

                        AEnt.ZONE = ddlPA_Zone.SelectedValue;
                    }

                    if (ddlPA_DISTRICT.SelectedIndex == 0)
                    {
                        AEnt.DISTRICT = "";
                    }
                    else
                    {
                        AEnt.DISTRICT = ddlPA_DISTRICT.SelectedValue;
                    }
                    AEnt.VDC_MUNICIPALITY = txtTA_VDC_MUNI.Text;
                    AEnt.WARD_NO = txtTA_WARD_NO.Text;
                    AEnt.STREET_NAME = txtTA_STREET.Text;
                    AEnt.HOUSE_NO = txtTA_HOUSE_NO.Text;
                    AEnt.ADDRESS_OF = "Employee";
                    AEnt.ADDRESS_OF_ID = txtIdNo.Text; ;
                    AEnt.ADDRESS_TYPE = "C";

                    ASrv.Insert(AEnt, DT);

                    #endregion

                }
                #endregion
            }

            else
            {
                #region for India

                #region parmanent Address


                AEnt = new ADDRESS();
                AEnt.COUNTRY = ddlCountryPA.SelectedValue;
                AEnt.STATE = "";
                AEnt.ZONE = "";
                AEnt.DISTRICT = "";
                AEnt.VDC_MUNICIPALITY = txtPA_VDC_MUNI.Text;
                AEnt.WARD_NO = txtPA_WARD_NO.Text;
                AEnt.STREET_NAME = txtPA_STREET.Text;
                AEnt.HOUSE_NO = txtPA_HOUSE_NO.Text;
                AEnt.ADDRESS_OF = "Employee";
                AEnt.ADDRESS_OF_ID = txtIdNo.Text; ;
                AEnt.ADDRESS_TYPE = "P";


                ASrv.Insert(AEnt, DT);

                #endregion

                #region Contact Address

                AEnt = new ADDRESS();
                AEnt.COUNTRY = ddlCountry_CA.SelectedValue;
                if (ddlStatePA.SelectedIndex == 0)
                {

                    AEnt.STATE = "";
                }
                else
                {
                    AEnt.STATE = ddlStatePA.SelectedValue;
                }


                if (ddlPA_Zone.SelectedIndex == 0)
                {
                    AEnt.ZONE = "";
                }
                else
                {

                    AEnt.ZONE = ddlPA_Zone.SelectedValue;
                }

                if (ddlPA_DISTRICT.SelectedIndex == 0)
                {
                    AEnt.DISTRICT = "";
                }
                else
                {
                    AEnt.DISTRICT = ddlPA_DISTRICT.SelectedValue;
                }

                AEnt.VDC_MUNICIPALITY = txtTA_VDC_MUNI.Text;
                AEnt.WARD_NO = txtTA_WARD_NO.Text;
                AEnt.STREET_NAME = txtTA_STREET.Text;
                AEnt.HOUSE_NO = txtTA_HOUSE_NO.Text;
                AEnt.ADDRESS_OF = "Employee";
                AEnt.ADDRESS_OF_ID = txtIdNo.Text;
                AEnt.ADDRESS_TYPE = "C";

                ASrv.Insert(AEnt, DT);


                #endregion

                #endregion

            }
        }
        else
        {
            HelperFunction.MsgBox(this, this.GetType(), "Data Not Saved. Contact Address must be Nepal");
        }

        #endregion

        #region Employee Account Detail
        EADEnt = new EMP_ACC_DETAIL();
        EADEnt.EMPLOYEE_ID = txtIdNo.Text;
        EADEnt.BASIC_SALARY = txtBasicSal.Text;
        EADEnt.PF_AMOUNT = txtPfidAmt.Text;
        EADEnt.CIT_AMOUNT = txtCitPer.Text;
        EADEnt.BENEFICIARY_AMOUNT = txtBeneficiaryAmt.Text;
        EADEnt.ACC_EFFECTIVE_DATE = hf.GetTodayDate();

        EADSrv.Insert(EADEnt, DT);


        #endregion

        #region login

        LEnt = new Entity.Components.Login();
        //string[] fname = txtFirstname.Text.Split(' ');
        LEnt.LOGINID = txtIdNo.Text;
        LEnt.FULLDETAILS = txtFirstname.Text;
        LEnt.EMPLOYEEID = txtIdNo.Text;
        //if (ddlDiv.SelectedItem.ToString() == "Administrative")

        if (ddlDiv.SelectedItem.ToString() == "Management")
        {
            LEnt.GROUPID = "1";
        }
        else if (ddlDiv.SelectedItem.ToString() == "Academic")
        {
            LEnt.GROUPID = "3";
        }

        LEnt.EMAIL = txtEmail.Text;
        LEnt.PASSWORD = txtIdNo.Text;
        LSer.Insert(LEnt, DT);

        #endregion

        if (DT.HAPPY == true)
        {
            DT.Commit();
            HelperFunction.MsgBox(this, this.GetType(), "Data inserted successfully");
            ClearField();
        }
        else
        {
            DT.Abort();
            HelperFunction.MsgBox(this, this.GetType(), "Please fill the form properly");
        }
        DT.Dispose();


        LoadData();
    }

    protected void ClearField()
    {
        txtIdNo.Text = "";
        txtFirstname.Text = "";
        txtLastname.Text = "";

        ddlCountryPA.SelectedIndex = 0;
        ddlStatePA.SelectedIndex = 0;
        ddlPA_Zone.SelectedIndex = 0;
        ddlPA_DISTRICT.SelectedIndex = 0;
        txtPA_VDC_MUNI.Text = "";
        txtPA_WARD_NO.Text = "";
        txtPA_STREET.Text = "";
        txtPA_HOUSE_NO.Text = "";

        ddlCountry_CA.SelectedIndex = 0;
        ddlState_CA.SelectedIndex = 0;
        ddlTA_Zone.SelectedIndex = 0;
        ddlTA_DISTRICT.SelectedIndex = 0;
        txtTA_VDC_MUNI.Text = "";
        txtTA_WARD_NO.Text = "";
        txtTA_STREET.Text = "";
        txtTA_HOUSE_NO.Text = "";

        txtPhone.Text = "";
        txtMobile1.Text = "";
        txtMobile2.Text = "";
        txtFatherName.Text = "";
        txtGrandFName.Text = "";
        txtEmail.Text = "";

        txtCtzno.Text = "";

        txtNomineeName.Text = "";
        txtNomineeAdd.Text = "";
        txtNomineeRelation.Text = "";
        txtPfidno.Text = "";
        txtCitno.Text = "";
        txtBasicSal.Text = "";
        txtCitPer.Text = "";

        txtCurrentbankaccount.Text = "";

        txtSecondbankaccount.Text = "";
        //chkStatus.Checked = false;

        txtRemarks.Text = "";
        txtBirthDay.Text = "";
        txtBirthMonth.Text = "";
        txtBirthYear.Text = "";

        txtBirthDay_AD.Text = "";
        txtBirthMonth_AD.Text = "";
        txtBirthYear_AD.Text = "";

        txtJobstartDay.Text = "";
        txtJobstartMonth.Text = "";
        txtJobstartYear.Text = "";

        txtDevName.Text = "";
        txtMotherName.Text = "";
        txtJobTitle.Text = "";

        txtPfidAmt.Text = "";
        txtBeneficiaryAmt.Text = "";

        rbtnGender.SelectedValue = "M";
        rbtnMaritalStatus.SelectedValue = "1";

        //txtEmpIdP.Text = "";
        //txtEmpNameP.Text = "";
        //txtEmpIdR.Text = "";
        //txtEmpNameR.Text = "";
        //txtDescription.Text = "";
        //txtDesignation.Text = "";

        txtAbbrevation.Text = "";
        ddlPayType.SelectedIndex = 0;

        txtSpouseName.Text = "";

        txtNomineeContact.Text = "";
        txtPANNo.Text = "";
        ddlContactAddress.SelectedIndex = 0;



    }

    protected void updateEmployee()
    {
        DT = new DistributedTransaction();
        string filename = "";
        //bttnPromote.Visible = true;

        #region Employees
        EEnt = new Employees();
        EEnt.EMPLOYEEID = txtIdNo.Text;
        EEnt = (Employees)ESer.GetSingle(EEnt, DT);
        if (EEnt != null)
        {
            EEnt.FIRSTNAME = txtFirstname.Text;
            EEnt.LASTNAME = txtLastname.Text;
            EEnt.Abbrevation = txtAbbrevation.Text;
            EEnt.DESIGNATIONID = ddlDesignation.SelectedValue;

            if (ddlDept.SelectedValue == "Please Select")
            {
                EEnt.DEPARTMENTID = "";
            }
            else
            {
                EEnt.DEPARTMENTID = ddlDept.SelectedValue;
            }

            EEnt.JOBTITLE = txtJobTitle.Text;
            EEnt.BIRTHDATE = hf.ConvertNepaliTOEnglish(txtBirthDay.Text, txtBirthMonth.Text, txtBirthYear.Text);
            EEnt.EMAIL = txtEmail.Text;
            EEnt.DIVISION = ddlDiv.SelectedValue;
            EEnt.DEVNAGARINAME = txtDevName.Text;

            EEnt.PHONE = txtPhone.Text;
            EEnt.MOBILE1 = txtMobile1.Text;
            EEnt.MOBILE2 = txtMobile2.Text;

            EEnt.FATHERNAME = txtFatherName.Text;
            EEnt.MOTHERNAME = txtMotherName.Text;
            EEnt.GRANDFATHERNAME = txtGrandFName.Text;

            if (ddlEmployeeType.SelectedValue == "Please Select")
            {

                EEnt.EMPLOYEETYPE = "";
            }
            else
            {
                EEnt.EMPLOYEETYPE = ddlEmployeeType.SelectedValue;
            }
            EEnt.GENDER = rbtnGender.SelectedValue;
            EEnt.MARITALSTATUS = rbtnMaritalStatus.SelectedValue;

            EEnt.CITIZENSHIPNO = txtCtzno.Text;
            EEnt.APPOINTMENTDATE = hf.ConvertNepaliTOEnglish(txtJobstartDay.Text, txtJobstartMonth.Text, txtJobstartYear.Text);

            EEnt.NOMINEENAME = txtNomineeName.Text;
            EEnt.NOMINEEADDRESS = txtNomineeAdd.Text;
            EEnt.NOMINEERELATION = txtNomineeRelation.Text;
            EEnt.Nominee_contact = txtNomineeContact.Text;
            EEnt.PFIDNO = txtPfidno.Text;
            EEnt.CITNO = txtCitno.Text;

            if (ddlBankName.SelectedValue == "Please Select")
            {
                EEnt.CURRENTBANK = "";
            }
            else
            {
                EEnt.CURRENTBANK = ddlBankName.SelectedValue;
            }

            EEnt.CURRENTBANKACCNO = txtCurrentbankaccount.Text;
            if (ddBankName2.SelectedValue == "Please Select")
            {
                EEnt.SECONDBANK = "";
            }
            else
            {
                EEnt.SECONDBANK = ddBankName2.SelectedValue;
            }
            EEnt.SECONDBANKACCNO = txtSecondbankaccount.Text;

            //if (chkStatus.Checked == true)
            //    EEnt.STATUS = "1";
            //else
            //    EEnt.STATUS = "0";

            EEnt.REMARKS = txtRemarks.Text;
            EEnt.BIRTHDAY = txtBirthDay.Text;
            EEnt.BIRTHMONTH = txtBirthMonth.Text;
            EEnt.BIRTHYEAR = txtBirthYear.Text;
            EEnt.APPOINTMENTDAY = txtJobstartDay.Text;
            EEnt.APPOINTMENTMONTH = txtJobstartMonth.Text;
            EEnt.APPOINTMENTYEAR = txtJobstartYear.Text;

            EEnt.Spouse_name = txtSpouseName.Text;

            EEnt.Panno = txtPANNo.Text;

            //for  saving image in folder
            try
            {
                filename = txtIdNo.Text + fileUpload1.FileName.Substring(fileUpload1.FileName.IndexOf('.'));
                if (fileUpload1.HasFile)
                {
                    fileUpload1.SaveAs(Server.MapPath("~/images/Employee/" + filename));
                    fileUpload1.SaveAs(Server.MapPath("~/images/User/" + filename));
                    // EEnt.IMAGE = ddlItemCategory.SelectedValue + "_" + txtItemCode.Text;
                }
            }
            catch
            {
            }

            //for  saving cv in folder
            try
            {
                if (FileUploadCV1.HasFile)
                {
                    string filename1 = txtIdNo.Text + "_cv" + FileUploadCV1.FileName.Substring(FileUploadCV1.FileName.IndexOf('.'));
                    FileUploadCV1.SaveAs(Server.MapPath("~/images/Employee_CV/" + filename1));
                }
            }
            catch
            {
            }

            //for  saving citizenhip image in folder
            try
            {
                string ctznspname = txtIdNo.Text + "_ctz" + FileUploadCitiznshp.FileName.Substring(FileUploadCitiznshp.FileName.IndexOf('.'));
                if (FileUploadCitiznshp.HasFile)
                {
                    FileUploadCitiznshp.SaveAs(Server.MapPath("~/images/Employee_Citizenship/" + ctznspname));
                    // EEnt.IMAGE = ddlItemCategory.SelectedValue + "_" + txtItemCode.Text;
                }
            }
            catch
            {
            }

            //for  saving certificates in folder
            try
            {
                if (FileUploadCertificate.HasFile)
                {

                    string file1name = txtIdNo.Text + "_certificate" + FileUploadCertificate.FileName.Substring(FileUploadCertificate.FileName.IndexOf('.'));
                    FileUploadCertificate.SaveAs(Server.MapPath("~/images/Employee_Certificates/" + file1name));


                }
            }
            catch
            {
            }


            ESer.Update(EEnt, DT);

        }

        #endregion

        #region Address Detail
        string stateP = "";
        string zoneP = "";
        string districtP = "";
        string stateC = "";
        string zoneC = "";
        string districtC = "";
        if (ddlCountryPA.SelectedValue == "1") // if permanant country is Nepal
        {
            stateP = ddlStatePA.SelectedValue;
            districtP = ddlPA_DISTRICT.SelectedValue;
            zoneP = ddlPA_Zone.SelectedValue;

        }
        if (ddlCountry_CA.SelectedValue == "1") // if Contact country is Nepal
        {
            stateC = ddlState_CA.SelectedValue;
            zoneC = ddlTA_Zone.SelectedValue;
            districtC = ddlTA_DISTRICT.SelectedValue;
        }


        if (ddlContactAddress.SelectedValue != "Select") // Both Permanant and Contact is same
        {

            #region both ADDRESS is same when updateing new data and old data is different

            theList = new EntityList();
            AEnt = new ADDRESS();
            AEnt.ADDRESS_OF_ID = EEnt.EMPLOYEEID;
            theList = ASrv.GetAll(AEnt);
            if (theList.Count > 1)
            {
                AEnt = new ADDRESS();
                AEnt.ADDRESS_OF_ID = EEnt.EMPLOYEEID;
                AEnt.ADDRESS_TYPE = "C";
                AEnt = (ADDRESS)ASrv.GetSingle(AEnt, DT);
                if (AEnt != null)
                {
                    ASrv.Delete(AEnt, DT);
                }

                AEnt = new ADDRESS();
                AEnt.ADDRESS_OF_ID = EEnt.EMPLOYEEID;
                AEnt.ADDRESS_TYPE = "P";
                AEnt = (ADDRESS)ASrv.GetSingle(AEnt, DT);
                if (AEnt != null)
                {
                    AEnt.COUNTRY = ddlCountryPA.SelectedValue;

                    if (ddlStatePA.SelectedIndex == 0)
                    {

                        AEnt.STATE = "";
                    }
                    else
                    {
                        AEnt.STATE = ddlStatePA.SelectedValue;
                    }


                    if (ddlPA_Zone.SelectedIndex == 0)
                    {
                        AEnt.ZONE = "";
                    }
                    else
                    {

                        AEnt.ZONE = ddlPA_Zone.SelectedValue;
                    }

                    if (ddlPA_DISTRICT.SelectedIndex == 0)
                    {
                        AEnt.DISTRICT = "";
                    }
                    else
                    {
                        AEnt.DISTRICT = ddlPA_DISTRICT.SelectedValue;
                    }

                    AEnt.VDC_MUNICIPALITY = txtPA_VDC_MUNI.Text;
                    AEnt.WARD_NO = txtPA_WARD_NO.Text;
                    AEnt.STREET_NAME = txtPA_STREET.Text;
                    AEnt.HOUSE_NO = txtPA_HOUSE_NO.Text;
                    AEnt.ADDRESS_OF = "Employee";
                    AEnt.ADDRESS_TYPE = "Both";
                    ASrv.Update(AEnt, DT);
                }
            }

            else      // both ADDRESS is same when updateing new data and old data is also same
            {
                AEnt = new ADDRESS();
                AEnt.ADDRESS_OF_ID = EEnt.EMPLOYEEID;
                AEnt = (ADDRESS)ASrv.GetSingle(AEnt, DT);
                if (AEnt != null)
                {
                    AEnt.COUNTRY = ddlCountryPA.SelectedValue;
                    if (ddlStatePA.SelectedIndex == 0)
                    {

                        AEnt.STATE = "";
                    }
                    else
                    {
                        AEnt.STATE = ddlStatePA.SelectedValue;
                    }


                    if (ddlPA_Zone.SelectedIndex == 0)
                    {
                        AEnt.ZONE = "";
                    }
                    else
                    {

                        AEnt.ZONE = ddlPA_Zone.SelectedValue;
                    }

                    if (ddlPA_DISTRICT.SelectedIndex == 0)
                    {
                        AEnt.DISTRICT = "";
                    }
                    else
                    {
                        AEnt.DISTRICT = ddlPA_DISTRICT.SelectedValue;
                    }

                    AEnt.VDC_MUNICIPALITY = txtPA_VDC_MUNI.Text;
                    AEnt.WARD_NO = txtPA_WARD_NO.Text;
                    AEnt.STREET_NAME = txtPA_STREET.Text;
                    AEnt.HOUSE_NO = txtPA_HOUSE_NO.Text;
                    AEnt.ADDRESS_OF = "Employee";
                    AEnt.ADDRESS_TYPE = "Both";
                    ASrv.Update(AEnt, DT);
                }
            }

            #endregion
        }
        else  //  Permanant address is differetn from Contact address
        {

            theList = new EntityList();
            AEnt = new ADDRESS();
            AEnt.ADDRESS_OF_ID = EEnt.EMPLOYEEID;
            theList = ASrv.GetAll(AEnt);
            if (theList.Count == 1)
            {
                #region if both address in old data is same
                AEnt = new ADDRESS();
                AEnt.ADDRESS_OF_ID = EEnt.EMPLOYEEID;
                AEnt = (ADDRESS)ASrv.GetSingle(AEnt, DT);
                if (AEnt != null)
                {
                    #region for permananta address

                    AEnt.COUNTRY = ddlCountryPA.SelectedValue;

                    if (ddlStatePA.SelectedIndex == 0)
                    {

                        AEnt.STATE = "";
                    }
                    else
                    {
                        AEnt.STATE = ddlStatePA.SelectedValue;
                    }


                    if (ddlPA_Zone.SelectedIndex == 0)
                    {
                        AEnt.ZONE = "";
                    }
                    else
                    {

                        AEnt.ZONE = ddlPA_Zone.SelectedValue;
                    }

                    if (ddlPA_DISTRICT.SelectedIndex == 0)
                    {
                        AEnt.DISTRICT = "";
                    }
                    else
                    {
                        AEnt.DISTRICT = ddlPA_DISTRICT.SelectedValue;
                    }

                    AEnt.VDC_MUNICIPALITY = txtPA_VDC_MUNI.Text;
                    AEnt.WARD_NO = txtPA_WARD_NO.Text;
                    AEnt.STREET_NAME = txtPA_STREET.Text;
                    AEnt.HOUSE_NO = txtPA_HOUSE_NO.Text;
                    AEnt.ADDRESS_OF = "Employee";
                    AEnt.ADDRESS_TYPE = "P";

                    ASrv.Update(AEnt, DT);
                    #endregion

                }

                #region for Contact Address

                AEnt = new ADDRESS();
                AEnt.COUNTRY = ddlCountry_CA.SelectedValue;

                if (ddlState_CA.SelectedIndex == 0)
                {

                    AEnt.STATE = "";
                }
                else
                {
                    AEnt.STATE = ddlState_CA.SelectedValue;
                }
                if (ddlTA_Zone.SelectedIndex == 0)
                {
                    AEnt.ZONE = "";
                }
                else
                {
                    AEnt.ZONE = ddlTA_Zone.SelectedValue;
                }


                if (ddlTA_DISTRICT.SelectedIndex == 0)
                {
                    AEnt.DISTRICT = "";
                }
                else
                {
                    AEnt.DISTRICT = ddlTA_DISTRICT.SelectedValue;
                }

                AEnt.VDC_MUNICIPALITY = txtTA_VDC_MUNI.Text;
                AEnt.WARD_NO = txtTA_WARD_NO.Text;
                AEnt.STREET_NAME = txtTA_STREET.Text;
                AEnt.HOUSE_NO = txtTA_HOUSE_NO.Text;
                AEnt.ADDRESS_OF = "Employee";
                AEnt.ADDRESS_OF_ID = EEnt.EMPLOYEEID;
                AEnt.ADDRESS_TYPE = "C";

                ASrv.Insert(AEnt, DT);
                #endregion
                #endregion
            }
            else
            {
                #region if both data is different
                AEnt = new ADDRESS();
                AEnt.ADDRESS_OF_ID = EEnt.EMPLOYEEID;
                AEnt.ADDRESS_TYPE = "P";
                AEnt = (ADDRESS)ASrv.GetSingle(AEnt, DT);
                if (AEnt != null)
                {
                    #region for permananta address
                    AEnt.COUNTRY = ddlCountryPA.SelectedValue;

                    if (ddlStatePA.SelectedIndex == 0)
                    {

                        AEnt.STATE = "";
                    }
                    else
                    {
                        AEnt.STATE = ddlStatePA.SelectedValue;
                    }


                    if (ddlPA_Zone.SelectedIndex == 0)
                    {
                        AEnt.ZONE = "";
                    }
                    else
                    {

                        AEnt.ZONE = ddlPA_Zone.SelectedValue;
                    }

                    if (ddlPA_DISTRICT.SelectedIndex == 0)
                    {
                        AEnt.DISTRICT = "";
                    }
                    else
                    {
                        AEnt.DISTRICT = ddlPA_DISTRICT.SelectedValue;
                    }


                    AEnt.VDC_MUNICIPALITY = txtPA_VDC_MUNI.Text;
                    AEnt.WARD_NO = txtPA_WARD_NO.Text;
                    AEnt.STREET_NAME = txtPA_STREET.Text;
                    AEnt.HOUSE_NO = txtPA_HOUSE_NO.Text;
                    AEnt.ADDRESS_OF = "Employee";
                    ASrv.Update(AEnt, DT);
                    #endregion

                }

                #region for Contact Address
                AEnt = new ADDRESS();
                AEnt.ADDRESS_OF_ID = EEnt.EMPLOYEEID;
                AEnt.ADDRESS_TYPE = "C";
                AEnt = (ADDRESS)ASrv.GetSingle(AEnt, DT);
                if (AEnt != null)
                {
                    AEnt.COUNTRY = ddlCountry_CA.SelectedValue;

                    if (ddlState_CA.SelectedIndex == 0)
                    {

                        AEnt.STATE = "";
                    }
                    else
                    {
                        AEnt.STATE = ddlState_CA.SelectedValue;
                    }
                    if (ddlTA_Zone.SelectedIndex == 0)
                    {
                        AEnt.ZONE = "";
                    }
                    else
                    {
                        AEnt.ZONE = ddlTA_Zone.SelectedValue;
                    }


                    if (ddlTA_DISTRICT.SelectedIndex == 0)
                    {
                        AEnt.DISTRICT = "";
                    }
                    else
                    {
                        AEnt.DISTRICT = ddlTA_DISTRICT.SelectedValue;
                    }

                    AEnt.VDC_MUNICIPALITY = txtTA_VDC_MUNI.Text;
                    AEnt.WARD_NO = txtTA_WARD_NO.Text;
                    AEnt.STREET_NAME = txtTA_STREET.Text;
                    AEnt.HOUSE_NO = txtTA_HOUSE_NO.Text;
                    AEnt.ADDRESS_OF = "Employee";
                    AEnt.ADDRESS_OF_ID = EEnt.EMPLOYEEID;

                    ASrv.Update(AEnt, DT);
                }
                #endregion
                #endregion
            }

        }

        #endregion

        #region Employee Account Detail
        EADEnt = new EMP_ACC_DETAIL();
        EADEnt.EMPLOYEE_ID = EEnt.EMPLOYEEID;
        EADEnt = (EMP_ACC_DETAIL)EADSrv.GetSingle(EADEnt, DT);
        if (EADEnt != null)
        {

            EADEnt.BASIC_SALARY = txtBasicSal.Text;
            EADEnt.PF_AMOUNT = txtPfidAmt.Text;
            EADEnt.CIT_AMOUNT = txtCitPer.Text;
            EADEnt.BENEFICIARY_AMOUNT = txtBeneficiaryAmt.Text;
            EADEnt.ACC_EFFECTIVE_DATE = hf.GetTodayDate();

            EADSrv.Update(EADEnt, DT);
        }


        #endregion

        #region login
        LEnt = new Entity.Components.Login();
        LEnt.EMPLOYEEID = txtIdNo.Text;

        LEnt = (Entity.Components.Login)LSer.GetSingle(LEnt, DT);
        if (LEnt != null)
        {
            string[] fname = txtFirstname.Text.Split(' ');
            LEnt.LOGINID = fname[0].ToLower();
            LEnt.FULLDETAILS = txtFirstname.Text;

            if (ddlDiv.SelectedItem.ToString() == "Administrative")
            {
                LEnt.GROUPID = "4";
            }
            else if (ddlDiv.SelectedItem.ToString() == "Academic")
            {
                LEnt.GROUPID = "3";
            }
            LEnt.EMAIL = txtEmail.Text;
            LEnt.PASSWORD = fname[0].ToLower();
            LSer.Update(LEnt, DT);
        }
        else
        {

            LEnt = new Entity.Components.Login();
            string[] fname = txtFirstname.Text.Split(' ');
            LEnt.LOGINID = fname[0].ToLower();
            LEnt.FULLDETAILS = txtFirstname.Text;
            LEnt.EMPLOYEEID = txtIdNo.Text;
            if (ddlDiv.SelectedItem.ToString() == "Administrative")
            {
                LEnt.GROUPID = "4";
            }
            else if (ddlDiv.SelectedItem.ToString() == "Academic")
            {
                LEnt.GROUPID = "3";
            }
            LEnt.EMAIL = txtEmail.Text;
            LEnt.PASSWORD = fname[0].ToLower();
            LSer.Insert(LEnt, DT);
        }

        #endregion

        if (DT.HAPPY == true)
        {
            DT.Commit();
            HelperFunction.MsgBox(this, this.GetType(), "Data Updated successfully");
            Response.Redirect("~/administration/reports/staff_list.aspx?div=" + ddlDiv.SelectedValue);
        }
        else
        {
            DT.Abort();
            HelperFunction.MsgBox(this, this.GetType(), "Something goes wrong");
        }

        DT.Dispose();
        ClearField();
        LoadData();


    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ClearField();
        LoadData();
    }

    protected void btnSavePromotion_Click(object sender, EventArgs e)
    {
        //PEnt = new Promotion();

        //PEnt.PROMOTIONDAY = PromotionDay.Text;
        //PEnt.PROMOTIONMONTH = PromotionMonth.Text;
        //PEnt.PROMOTIONYEAR = PromotionYear.Text;
        //PEnt.PROMOTIONDATE = hf.ConvertNepaliTOEnglish(PromotionDay.Text, PromotionMonth.Text, PromotionYear.Text);
        //PEnt.EMPLOYEEID = txtEmpIdP.Text;
        //PEnt.DESIGNATIONID = ddlPromo.SelectedValue;

        //if (PSer.Insert(PEnt) > 0)
        //{
        //    EEnt = new Employees();
        //    EEnt.EMPLOYEEID = txtEmpIdP.Text;
        //    EEnt = (Employees)ESer.GetSingle(EEnt);
        //    if (EEnt != null)
        //    {
        //        EEnt.DESIGNATIONID = ddlPromo.SelectedValue;
        //        if (ESer.Update(EEnt) > 0)
        //        {
        //            HelperFunction.MsgBox(this, this.GetType(), "DONE");
        //        }
        //    }
        //}

    }

    protected void btnSaveResignation_Click(object sender, EventArgs e)
    {
        //RSEnt = new Resignation();

        //RSEnt.RESIGNATION_TYPE = ddlResignationType.SelectedValue;
        //RSEnt.DAY = txtResDay.Text;
        //RSEnt.MONTH = txtResMonth.Text;
        //RSEnt.YEAR = txtResYear.Text;
        //RSEnt.ENG_DATE = hf.ConvertNepaliTOEnglish(txtResDay.Text, txtResMonth.Text, txtResYear.Text);
        //RSEnt.EMPLOYEE_ID = txtEmpIdR.Text;
        //RSEnt.DESCRIPTION = txtDescription.Text;

        //if (RSSer.Insert(RSEnt) > 0)
        //{
        //    EEnt = new Employees();
        //    EEnt.EMPLOYEEID = txtEmpIdR.Text;
        //    EEnt = (Employees)ESer.GetSingle(EEnt);
        //    if (EEnt != null)
        //    {
        //        EEnt.STATUS = "1";
        //        if (ESer.Update(EEnt) > 0)
        //        {
        //            HelperFunction.MsgBox(this, this.GetType(), "DONE");
        //        }
        //    }
        //}

    }
    protected void ddlEmployeeType_SelectedIndexChanged(object sender, EventArgs e)
    {
        choosePayType();

    }
    protected void choosePayType()
    {
        if (ddlDiv.SelectedItem.ToString() == "Academic")
        {

            if (ddlEmployeeType.SelectedItem.ToString() == "Part Time")
            {
                beneficiaryAmt_label.Visible = false;
                txtBeneficiaryAmt.Text = "";

                paytype_label.Visible = true;
            }
            else
            {
                beneficiaryAmt_label.Visible = true;

                paytype_label.Visible = false;
            }
        }
        else
        {
            beneficiaryAmt_label.Visible = true;

            paytype_label.Visible = false;
        }
    }
    protected void txtBirthDay_TextChanged(object sender, EventArgs e)
    {
        //txtBirthMonth.Focus();
        //GetEnglishDate();

        int day = Convert.ToInt32(txtBirthDay.Text);

        if (day >= 32)
        {
            HelperFunction.MsgBox(this, this.GetType(), "Invalid day");
            txtBirthDay.Focus();
        }
        else
        {
            txtBirthMonth.Focus();
        }
    }
    protected void txtBirthMonth_TextChanged(object sender, EventArgs e)
    {
        //txtBirthYear.Focus();
        //GetEnglishDate();

        int month = Convert.ToInt32(txtBirthMonth.Text);

        if (month >= 13)
        {
            HelperFunction.MsgBox(this, this.GetType(), "Invalid month");
            txtBirthMonth.Focus();
        }
        else
        {
            txtBirthYear.Focus();

        }
    }
    protected void txtBirthYear_TextChanged(object sender, EventArgs e)
    {
        GetEnglishDate();
    }
    protected void GetEnglishDate()
    {
        if (txtBirthDay.Text != "" && txtBirthMonth.Text != "" && txtBirthYear.Text != "")
        {
            try
            {

                int day = Convert.ToInt32(txtBirthDay.Text);
                int month = Convert.ToInt32(txtBirthMonth.Text);
                int year = Convert.ToInt32(txtBirthYear.Text);

                string englishdate = hf.ConvertNepaliTOEnglishDate(txtBirthDay.Text, txtBirthMonth.Text, txtBirthYear.Text);

                string[] date = englishdate.Split('/');

                txtBirthDay_AD.Text = date[0];
                txtBirthMonth_AD.Text = date[1];
                txtBirthYear_AD.Text = date[2];


            }
            catch
            {

                HelperFunction.MsgBox(this, this.GetType(), "Please Enter Correct Date");
            }

        }

    }
    protected void txtBirthDay_AD_TextChanged(object sender, EventArgs e)
    {
        txtBirthMonth_AD.Focus();
        GetNepaliDate();
    }
    protected void txtBirthMonth_AD_TextChanged(object sender, EventArgs e)
    {
        txtBirthYear_AD.Focus();
        GetNepaliDate();
    }
    protected void txtBirthYear_AD_TextChanged(object sender, EventArgs e)
    {
        GetNepaliDate();
    }

    protected void GetNepaliDate()
    {
        if (txtBirthDay_AD.Text != "" && txtBirthMonth_AD.Text != "" && txtBirthYear_AD.Text != "")
        {
            try
            {

                int day = Convert.ToInt32(txtBirthDay_AD.Text);
                int month = Convert.ToInt32(txtBirthMonth_AD.Text);
                int year = Convert.ToInt32(txtBirthYear_AD.Text);



                string[] date = hf.ConvertEnglishToNepali(txtBirthMonth_AD.Text + "/" + txtBirthDay_AD.Text + "/" + txtBirthYear_AD.Text);

                txtBirthDay.Text = date[0];
                txtBirthMonth.Text = date[1];
                txtBirthYear.Text = date[2];

            }
            catch
            {

                HelperFunction.MsgBox(this, this.GetType(), "Please Enter Correct Date");
            }

        }

    }

    protected void ddlZone_C_SelectedIndexChanged(object sender, EventArgs e)
    {
        DREnt = new District();
        //DREnt.ZONE_ID = ddlZone.SelectedValue;

        //ddlDistrict_C.DataSource = DRSer.GetAll(DREnt);
        //ddlDistrict_C.DataTextField = "DISTRICTNAME";
        //ddlDistrict_C.DataValueField = "ID";
        //ddlDistrict_C.DataBind();
        //ddlDistrict_C.Items.Insert(0, "Please Select");
    }

    protected void Country_PA()
    {
        if (ddlCountryPA.SelectedValue == "1")
        {
            lblSameAsOf.Visible = true;
            lblState_PA.Visible = true;
            lblZone_PA.Visible = true;
            lblDistrict_PA.Visible = true;
            ddlContactAddress.SelectedIndex = 0;
        }
        else
        {
            lblSameAsOf.Visible = false;
            lblState_PA.Visible = false;
            lblZone_PA.Visible = false;
            lblDistrict_PA.Visible = false;

            ddlContactAddress.SelectedIndex = 0;
            ddlStatePA.SelectedIndex = 0;
            ddlPA_Zone.SelectedIndex = 0;
            ddlPA_DISTRICT.SelectedIndex = 0;
            txtPA_VDC_MUNI.Text = "";
            txtPA_WARD_NO.Text = "";
            txtPA_STREET.Text = "";
            txtPA_HOUSE_NO.Text = "";

            //lblSameAsOf.Visible = false;

        }
    }

    protected void ddlCountryPA_SelectedIndexChanged(object sender, EventArgs e)
    {
        Country_PA();
    }
    protected void ddlPA_Zone_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadPADistrict();
    }
    protected void ddlContactAddress_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlContactAddress.SelectedValue != "Select")
        {

            if (ddlCountryPA.SelectedValue == "1")
            {
                lblState_CA.Visible = true;
                lblZone_CA.Visible = true;
                lblDistrict_CA.Visible = true;

                ddlCountry_CA.Enabled = false;
                ddlState_CA.Enabled = false;
                ddlTA_Zone.Enabled = false;
                ddlTA_DISTRICT.Enabled = false;
                txtTA_VDC_MUNI.Enabled = false;
                txtTA_WARD_NO.Enabled = false;
                txtTA_STREET.Enabled = false;
                txtTA_HOUSE_NO.Enabled = false;

                ddlCountry_CA.SelectedValue = ddlCountryPA.SelectedValue;
                ddlState_CA.SelectedValue = ddlStatePA.SelectedValue;

                #region load Contact Zone

                ZEnt = new zone();
                ddlTA_Zone.DataSource = ZSer.GetAll(ZEnt);
                ddlTA_Zone.DataTextField = "ZONE_NAME";
                ddlTA_Zone.DataValueField = "ZONE_ID";
                ddlTA_Zone.DataBind();
                ddlTA_Zone.Items.Insert(0, "Select");

                #endregion

                ddlTA_Zone.SelectedValue = ddlPA_Zone.SelectedValue;
                LoadTADistrict();

                ddlTA_DISTRICT.SelectedValue = ddlPA_DISTRICT.SelectedValue;
                txtTA_VDC_MUNI.Text = txtPA_VDC_MUNI.Text;
                txtTA_WARD_NO.Text = txtPA_WARD_NO.Text;
                txtTA_STREET.Text = txtPA_STREET.Text;
                txtTA_HOUSE_NO.Text = txtPA_HOUSE_NO.Text;

            }
            else
            {
                lblState_CA.Visible = false;
                lblZone_CA.Visible = false;
                lblDistrict_CA.Visible = false;

                ddlCountry_CA.SelectedValue = ddlCountryPA.SelectedValue;

                txtTA_VDC_MUNI.Text = txtPA_VDC_MUNI.Text;
                txtTA_WARD_NO.Text = txtPA_WARD_NO.Text;
                txtTA_STREET.Text = txtPA_STREET.Text;
                txtTA_HOUSE_NO.Text = txtPA_HOUSE_NO.Text;
            }
        }
        else
        {
            lblCountryCA.Visible = true;

            if (ddlCountryPA.SelectedValue == "1")
            {
                lblState_CA.Visible = true;
                lblZone_CA.Visible = true;
                lblDistrict_CA.Visible = true;

                ddlCountry_CA.SelectedIndex = 0;
                ddlState_CA.SelectedIndex = 0;
                ddlTA_Zone.SelectedIndex = 0;
                ddlTA_DISTRICT.SelectedIndex = 0;
                txtTA_VDC_MUNI.Text = "";
                txtTA_WARD_NO.Text = "";
                txtTA_STREET.Text = "";
                txtTA_HOUSE_NO.Text = "";

            }
            else
            {
                lblState_CA.Visible = false;
                lblZone_CA.Visible = false;
                lblDistrict_CA.Visible = false;

                ddlCountry_CA.SelectedIndex = 0;

                txtTA_VDC_MUNI.Text = "";
                txtTA_WARD_NO.Text = "";
                txtTA_STREET.Text = "";
                txtTA_HOUSE_NO.Text = "";
            }

        }

    }
    protected void ddlCountry_CA_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountry_CA.SelectedValue == "1")
        {

            lblState_CA.Visible = true;
            lblZone_CA.Visible = true;
            lblDistrict_CA.Visible = true;
        }
        else
        {
            lblCountryCA.Visible = true;
            lblState_CA.Visible = false;
            lblZone_CA.Visible = false;
            lblDistrict_CA.Visible = false;


        }

    }
    protected void ddlTA_Zone_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTADistrict();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        EEnt = new Employees();

        EEnt.EMPLOYEEID = txtIdNo.Text;
        EEnt = (Employees)ESer.GetSingle(EEnt);
        if (EEnt != null)
        {
            updateEmployee();
        }
        else
        {
            saveEmployee();
        }
    }

    protected void rbtnMaritalStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rbtnMaritalStatus.SelectedValue == "Married")
        {
            lblSpouse.Visible = true;


        }
        else
        {
            lblSpouse.Visible = false;

        }
    }
}