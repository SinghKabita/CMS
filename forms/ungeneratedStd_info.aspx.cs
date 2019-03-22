using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;
using DataHelper.Framework;
using Entity.Framework;


public partial class forms_ungeneratedStd_info : System.Web.UI.Page
{

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    HSS_STUDENT SEnt = new HSS_STUDENT();
    HSS_STUDENTService SSer = new HSS_STUDENTService();

    HSS_EDUCATION_DETAIL EDEnt = new HSS_EDUCATION_DETAIL();
    HSS_EDUCATION_DETAILService EDSer = new HSS_EDUCATION_DETAILService();

    HSS_CURRENT_STUDENT CSEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSer = new HSS_CURRENT_STUDENTService();

    HSS_ATTACHMENTS AEnt = new HSS_ATTACHMENTS();
    HSS_ATTACHMENTSService ASer = new HSS_ATTACHMENTSService();

    HSS_NAME NAMEEnt = new HSS_NAME();
    HSS_NAMEService NAMESer = new HSS_NAMEService();

    HSS_SUBJECT SUBEnt = new HSS_SUBJECT();
    HSS_SUBJECTService SUBSer = new HSS_SUBJECTService();

    District DEnt = new District();
    DistrictService DSer = new DistrictService();

    zone ZEnt = new zone();
    zoneService ZSer = new zoneService();

    DistributedTransaction DT = new DistributedTransaction();
    HelperFunction Mf = new HelperFunction();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    BatchYear BTEnt = new BatchYear();
    BatchYearService BTSer = new BatchYearService();

    information_alert INAEnt = new information_alert();
    information_alertService INASer = new information_alertService();

    State STEnt = new State();
    StateService STSrv = new StateService();

    ADDRESS AddEnt = new ADDRESS();
    ADDRESSService AddSrv = new ADDRESSService();

    ADMISSION_DETAIL ADEnt = new ADMISSION_DETAIL();
    ADMISSION_DETAILService ADSrv = new ADMISSION_DETAILService();

    COUNTRY CEnt = new COUNTRY();
    COUNTRYService CSrv = new COUNTRYService();

    RELIGION REnt = new RELIGION();
    RELIGIONService RSrv = new RELIGIONService();

    FAMILY_RELATIONS FREnt = new FAMILY_RELATIONS();
    FAMILY_RELATIONSService FRSrv = new FAMILY_RELATIONSService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    EntityList theList = new EntityList();

    HelperFunction hf = new HelperFunction();

    protected void Page_Load(object sender, EventArgs e)
    {
        Page.MaintainScrollPositionOnPostBack = true;

        if (!IsPostBack)
        {
            //clearFields();
            LoadFaculty();
            LoadProgram();
            //LoadBatch();
            LoadState_SLC();
            LoadState_PT();
            LoadState_B();
            LoadState_PA();
            LoadState_CA();
            LoadZone();
            LoadNationality();
            LoadReligion();
            LoadCountrySLC();
            LoadCountryPT();
            LoadCountryB();
            LoadCountryPA();
            LoadCountryCA();
            LoadSLCDistrict();
            LoadPADistrict();
            LoadTADistrict();
            School_Zone();
            PlusTwo_Zone();
            LoadCollegeCode();
            LoadLevel();

            string pkId = "";
            pkId = (Request.QueryString.Get("pkId"));

            if (pkId != null)
            {
                LoadData(pkId);
            }
        }
    }

    protected void LoadState_SLC()
    {
        State STEnt = new State();
        ddlState_SLC.DataSource = STSrv.GetAll(STEnt);
        ddlState_SLC.DataTextField = "STATE";
        ddlState_SLC.DataValueField = "STATE_ID";
        ddlState_SLC.DataBind();
        ddlState_SLC.Items.Insert(0, "Select");
    }

    protected void LoadState_PT()
    {
        State STEnt = new State();
        ddlState_PT.DataSource = STSrv.GetAll(STEnt);
        ddlState_PT.DataTextField = "STATE";
        ddlState_PT.DataValueField = "STATE_ID";
        ddlState_PT.DataBind();
        ddlState_PT.Items.Insert(0, "Select");
    }

    protected void LoadState_B()
    {
        State STEnt = new State();
        ddlState_B.DataSource = STSrv.GetAll(STEnt);
        ddlState_B.DataTextField = "STATE";
        ddlState_B.DataValueField = "STATE_ID";
        ddlState_B.DataBind();
        ddlState_B.Items.Insert(0, "Select");
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

    protected void LoadFaculty()
    {
        FCEnt = new hss_faculty();
        ddlFaculty.DataSource = FCSer.GetAll(FCEnt);
        ddlFaculty.DataTextField = "FACULTY";
        ddlFaculty.DataValueField = "PK_ID";
        ddlFaculty.DataBind();
        ddlFaculty.Items.Insert(0, "Select");
        ddlProgram.Items.Insert(0, "Select");


    }

    protected void LoadLevel()
    {
        LEnt = new HSS_LEVEL();
        ddlLevel.DataSource = LSrv.GetAll(LEnt);
        ddlLevel.DataTextField = "LEVEL_NAME";
        ddlLevel.DataValueField = "LEVEL_NAME";
        ddlLevel.DataBind();

    }

    protected void LoadProgram()
    {
        PEnt = new program();
        PEnt.FACULTY_ID = ddlFaculty.SelectedValue;
        PEnt.PROGRAM_LEVEL = ddlLevel.SelectedValue;
        ddlProgram.DataSource = PSer.GetAll(PEnt);
        ddlProgram.DataTextField = "PROGRAM_CODE";
        ddlProgram.DataValueField = "PK_ID";
        ddlProgram.DataBind();

    }

    protected void LoadBatch()
    {
        BTEnt = new BatchYear();
        BTEnt.ACTIVE = "1";
        BTEnt.PROGRAM = ddlProgram.SelectedValue;
        ddlBatch.DataSource = BTSer.GetAll(BTEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();

        LoadCollegeCode();
    }

    protected void LoadSemester()
    {

        theList = new EntityList();
        EntityList semList = new EntityList();
        BTEnt = new BatchYear();
        BTEnt.ACTIVE = "1";
        BTEnt.PROGRAM = ddlProgram.SelectedValue;
        BTEnt.BATCH = ddlBatch.SelectedValue;
        theList = BTSer.GetAll(BTEnt);
        #region to get the active Semester
        foreach (BatchYear by in theList)
        {
            SMEnt = new semester();
            SMEnt.PK_ID = by.SEMESTER;
            SMEnt = (semester)SMSer.GetSingle(SMEnt);
            if (SMEnt != null)
            {
                semList.Add(SMEnt);

            }
        }
        #endregion

    }

    protected void LoadCollegeCode()
    {
        //NAMEEnt = new HSS_NAME();
        //NAMEEnt = (HSS_NAME)NAMESer.GetSingle(NAMEEnt);
        //if (NAMEEnt != null)
        //{
        //    if (ddlProgram.SelectedValue != "Select")
        //    {

        //        lblCode.Text = NAMEEnt.CODE + ddlProgram.SelectedItem;
        //    }
        //    else
        //    {
        //        lblCode.Text = NAMEEnt.CODE;
        //    }
        //}
    }

    protected void LoadPADistrict()
    {
        DEnt = new District();
        DEnt.ZONE_ID = ddlPA_Zone.SelectedValue;
        ddlPA_DISTRICT.DataSource = DSer.GetAll(DEnt);
        ddlPA_DISTRICT.DataTextField = "DISTRICTNAME";
        ddlPA_DISTRICT.DataValueField = "ID";
        ddlPA_DISTRICT.DataBind();
        ddlPA_DISTRICT.Items.Insert(0, "Select");
    }

    protected void LoadTADistrict()
    {

        DEnt = new District();
        DEnt.ZONE_ID = ddlTA_Zone.SelectedValue;
        ddlTA_DISTRICT.DataSource = DSer.GetAll(DEnt);
        ddlTA_DISTRICT.DataTextField = "DISTRICTNAME";
        ddlTA_DISTRICT.DataValueField = "ID";
        ddlTA_DISTRICT.DataBind();
        ddlTA_DISTRICT.Items.Insert(0, "Select");
    }

    protected void LoadBDistrict()
    {

        DEnt = new District();
        DEnt.ZONE_ID = ddlZone_B.SelectedValue;
        ddlDistrict_B.DataSource = DSer.GetAll(DEnt);
        ddlDistrict_B.DataTextField = "DISTRICTNAME";
        ddlDistrict_B.DataValueField = "ID";
        ddlDistrict_B.DataBind();
        ddlDistrict_B.Items.Insert(0, "Select");

    }

    protected void LoadPTDistrict()
    {
        DEnt = new District();
        DEnt.ZONE_ID = ddlZone_PT.SelectedValue;
        ddlDistrict_PT.DataSource = DSer.GetAll(DEnt);
        ddlDistrict_PT.DataTextField = "DISTRICTNAME";
        ddlDistrict_PT.DataValueField = "ID";
        ddlDistrict_PT.DataBind();
        ddlDistrict_PT.Items.Insert(0, "Select");

    }

    protected void LoadSLCDistrict()
    {
        DEnt = new District();
        DEnt.ZONE_ID = ddlZone_School.SelectedValue;
        ddlDistrict_School.DataSource = DSer.GetAll(DEnt);
        ddlDistrict_School.DataTextField = "DISTRICTNAME";
        ddlDistrict_School.DataValueField = "ID";
        ddlDistrict_School.DataBind();
        ddlDistrict_School.Items.Insert(0, "Select");
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


        ZEnt = new zone();
        ddlZone_School.DataSource = ZSer.GetAll(ZEnt);
        ddlZone_School.DataTextField = "ZONE_NAME";
        ddlZone_School.DataValueField = "ZONE_ID";
        ddlZone_School.DataBind();
        ddlZone_School.Items.Insert(0, "Select");



        ZEnt = new zone();
        ddlZone_PT.DataSource = ZSer.GetAll(ZEnt);
        ddlZone_PT.DataTextField = "ZONE_NAME";
        ddlZone_PT.DataValueField = "ZONE_ID";
        ddlZone_PT.DataBind();
        ddlZone_PT.Items.Insert(0, "Select");


        ZEnt = new zone();
        ddlZone_B.DataSource = ZSer.GetAll(ZEnt);
        ddlZone_B.DataTextField = "ZONE_NAME";
        ddlZone_B.DataValueField = "ZONE_ID";
        ddlZone_B.DataBind();
        ddlZone_B.Items.Insert(0, "Select");
    }

    protected void LoadNationality()
    {
        CEnt = new COUNTRY();
        ddlNATIONALITY.DataSource = CSrv.GetAll(CEnt);
        ddlNATIONALITY.DataTextField = "NATIONALITY";
        ddlNATIONALITY.DataValueField = "PK_ID";
        ddlNATIONALITY.DataBind();
        //ddlNATIONALITY.Items.Insert(0, "Select");
    }

    protected void LoadReligion()
    {
        REnt = new RELIGION();
        ddlRELIGION.DataSource = RSrv.GetAll(REnt);
        ddlRELIGION.DataTextField = "RELIGION_NAME";
        ddlRELIGION.DataValueField = "PK_ID";
        ddlRELIGION.DataBind();
    }

    protected void LoadCountrySLC()
    {
        CEnt = new COUNTRY();
        ddlCountrySLC.DataSource = CSrv.GetAll(CEnt);
        ddlCountrySLC.DataTextField = "COUNTRY_NAME";
        ddlCountrySLC.DataValueField = "PK_ID";
        ddlCountrySLC.DataBind();
    }

    protected void LoadCountryPT()
    {
        CEnt = new COUNTRY();
        ddlCountryPT.DataSource = CSrv.GetAll(CEnt);
        ddlCountryPT.DataTextField = "COUNTRY_NAME";
        ddlCountryPT.DataValueField = "PK_ID";
        ddlCountryPT.DataBind();
    }

    protected void LoadCountryB()
    {
        CEnt = new COUNTRY();
        ddlCountryB.DataSource = CSrv.GetAll(CEnt);
        ddlCountryB.DataTextField = "COUNTRY_NAME";
        ddlCountryB.DataValueField = "PK_ID";
        ddlCountryB.DataBind();
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

    public void LoadData(string pkId)
    {
        ddlFaculty.Enabled = false;
        ddlLevel.Enabled = false;
        ddlProgram.Enabled = false;
        ddlBatch.Enabled = false;

        if (ddlGuardian.SelectedValue != "Select")
        {
            txtG_NAME.Enabled = false;
            txtG_RELATION.Enabled = false;
            txtG_OCCUPATION.Enabled = false;
            txtG_ADDRESS.Enabled = false;
            txtG_PHONE.Enabled = false;
            txtG_MOBILE1.Enabled = false;
            txtG_MOBILE2.Enabled = false;
            txtG_EMAIL.Enabled = false;
            txtG_POSITION.Enabled = false;
            txtG_OFFICE.Enabled = false;
            txtG_OFFICE_PHONE.Enabled = false;
        }

        SEnt = new HSS_STUDENT();
        SEnt.PK_ID = pkId;
        SEnt = (HSS_STUDENT)SSer.GetSingle(SEnt);
        if (SEnt != null)
        {
            lblpkid_hss_Std.Text = pkId;

            LoadFaculty();
            LoadLevel();

            PEnt = new program();
            PEnt.PK_ID = SEnt.PROGRAM;
            PEnt = (program)PSer.GetSingle(PEnt);
            if (PEnt != null)
            {

                ddlLevel.SelectedValue = PEnt.PROGRAM_LEVEL;
                ddlFaculty.SelectedValue = PEnt.FACULTY_ID;
            }
            LoadProgram();
            ddlProgram.SelectedValue = SEnt.PROGRAM;
            LoadBatch();
            ddlBatch.SelectedValue = SEnt.BAT_CH;
            txtUniRegdNo.Text = SEnt.UNIVERSITY_REG_NO;
            txtNAME_ENGLISH.Text = SEnt.NAME_ENGLISH;
            txtNAME_DEVANAGARI.Text = SEnt.NAME_DEVANAGARI;
            rbtnGENDER.SelectedValue = SEnt.GENDER.Trim();
            rbtnMaritalStatus.SelectedValue = SEnt.MARITAL_STATUS.Trim();
            loadMarital_Status();
            if (SEnt.DOB_BS != null)
            {
                try
                {
                    string[] BS = SEnt.DOB_BS.ToString().Split('/');
                    txtDOB_BSDay.Text = BS[0];
                    txtDOB_BSMth.Text = BS[1];
                    txtDOB_BSYear.Text = BS[2];
                }
                catch { }
            }
            if (SEnt.DOB_AD != null)
            {
                try
                {
                    string[] AD = SEnt.DOB_AD.ToString().Split('/');
                    txtDOB_ADDay.Text = AD[0];
                    txtDOB_ADMth.Text = AD[1];
                    txtDOB_ADYear.Text = AD[2];
                }
                catch { }
            }
            LoadNationality();
            ddlNATIONALITY.SelectedValue = SEnt.NATIONALITY;
            LoadReligion();
            ddlRELIGION.SelectedValue = SEnt.RELIGION;
            txtCITIZENSHIP_NO.Text = SEnt.CITIZENSHIP_NO;
            txtPHONE.Text = SEnt.PHONE;
            txtMOBILE_1.Text = SEnt.MOBILE_1;
            txtMOBILE_2.Text = SEnt.MOBILE_2;
            txtEMAIL.Text = SEnt.EMAIL;

            #region Address

            AddEnt = new ADDRESS();
            AddEnt.ADDRESS_OF_ID = SEnt.PK_ID;
            AddEnt.ADDRESS_TYPE = "Both";
            AddEnt = (ADDRESS)AddSrv.GetSingle(AddEnt);
            if (AddEnt != null)
            {
                txtPA_HOUSE_NO.Text = AddEnt.HOUSE_NO;
                txtPA_STREET.Text = AddEnt.STREET_NAME;
                txtPA_VDC_MUNI.Text = AddEnt.VDC_MUNICIPALITY;
                txtPA_WARD_NO.Text = AddEnt.WARD_NO;
                LoadCountryPA();
                ddlCountryPA.SelectedValue = AddEnt.COUNTRY;
                LoadState_PA();
                ddlStatePA.SelectedValue = AddEnt.STATE;
                ZEnt = new zone();
                ddlPA_Zone.DataSource = ZSer.GetAll(ZEnt);
                ddlPA_Zone.DataTextField = "ZONE_NAME";
                ddlPA_Zone.DataValueField = "ZONE_ID";
                ddlPA_Zone.DataBind();
                ddlPA_Zone.Items.Insert(0, "Select");
                ddlPA_Zone.SelectedValue = AddEnt.ZONE;
                LoadPADistrict();
                ddlPA_DISTRICT.SelectedValue = AddEnt.DISTRICT;

                if (AddEnt.ADDRESS_TYPE == "Both")
                {
                    ddlContactAddress.SelectedIndex = 1;
                }
                else
                {
                    ddlContactAddress.SelectedIndex = 0;
                }

                ddlCountry_CA.Enabled = false;
                ddlState_CA.Enabled = false;
                ddlTA_Zone.Enabled = false;
                ddlTA_DISTRICT.Enabled = false;
                txtTA_VDC_MUNI.Enabled = false;
                txtTA_WARD_NO.Enabled = false;
                txtTA_STREET.Enabled = false;
                txtTA_HOUSE_NO.Enabled = false;

                txtTA_HOUSE_NO.Text = AddEnt.HOUSE_NO;
                txtTA_STREET.Text = AddEnt.STREET_NAME;
                txtTA_VDC_MUNI.Text = AddEnt.VDC_MUNICIPALITY;
                txtTA_WARD_NO.Text = AddEnt.WARD_NO;
                LoadCountryCA();
                ddlCountry_CA.SelectedValue = AddEnt.COUNTRY;
                LoadState_CA();
                ddlState_CA.SelectedValue = AddEnt.STATE;
                ZEnt = new zone();
                ddlTA_Zone.DataSource = ZSer.GetAll(ZEnt);
                ddlTA_Zone.DataTextField = "ZONE_NAME";
                ddlTA_Zone.DataValueField = "ZONE_ID";
                ddlTA_Zone.DataBind();
                ddlTA_Zone.Items.Insert(0, "Select");
                ddlTA_Zone.SelectedValue = AddEnt.ZONE;
                LoadTADistrict();
                ddlTA_DISTRICT.SelectedValue = AddEnt.DISTRICT;
            }


            AddEnt = new ADDRESS();
            AddEnt.ADDRESS_OF_ID = SEnt.PK_ID;
            AddEnt.ADDRESS_TYPE = "P";
            AddEnt = (ADDRESS)AddSrv.GetSingle(AddEnt);
            if (AddEnt != null)
            {
                if (AddEnt.COUNTRY != "1")
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

                txtPA_HOUSE_NO.Text = AddEnt.HOUSE_NO;
                txtPA_STREET.Text = AddEnt.STREET_NAME;
                txtPA_VDC_MUNI.Text = AddEnt.VDC_MUNICIPALITY;
                txtPA_WARD_NO.Text = AddEnt.WARD_NO;
                LoadCountryPA();
                ddlCountryPA.SelectedValue = AddEnt.COUNTRY;
                LoadState_PA();
                ddlStatePA.SelectedValue = AddEnt.STATE;
                ZEnt = new zone();
                ddlPA_Zone.DataSource = ZSer.GetAll(ZEnt);
                ddlPA_Zone.DataTextField = "ZONE_NAME";
                ddlPA_Zone.DataValueField = "ZONE_ID";
                ddlPA_Zone.DataBind();
                ddlPA_Zone.Items.Insert(0, "Select");
                ddlPA_Zone.SelectedValue = AddEnt.ZONE;
                LoadPADistrict();
                ddlPA_DISTRICT.SelectedValue = AddEnt.DISTRICT;

            }

            AddEnt = new ADDRESS();
            AddEnt.ADDRESS_OF_ID = SEnt.PK_ID;
            AddEnt.ADDRESS_TYPE = "C";
            AddEnt = (ADDRESS)AddSrv.GetSingle(AddEnt);
            if (AddEnt != null)
            {

                txtTA_HOUSE_NO.Text = AddEnt.HOUSE_NO;
                txtTA_STREET.Text = AddEnt.STREET_NAME;
                txtTA_VDC_MUNI.Text = AddEnt.VDC_MUNICIPALITY;
                txtTA_WARD_NO.Text = AddEnt.WARD_NO;
                LoadCountryCA();
                ddlCountry_CA.SelectedValue = AddEnt.COUNTRY;
                LoadState_CA();
                ddlState_CA.SelectedValue = AddEnt.STATE;
                ZEnt = new zone();
                ddlTA_Zone.DataSource = ZSer.GetAll(ZEnt);
                ddlTA_Zone.DataTextField = "ZONE_NAME";
                ddlTA_Zone.DataValueField = "ZONE_ID";
                ddlTA_Zone.DataBind();
                ddlTA_Zone.Items.Insert(0, "Select");
                ddlTA_Zone.SelectedValue = AddEnt.ZONE;
                LoadTADistrict();
                ddlTA_DISTRICT.SelectedValue = AddEnt.DISTRICT;

            }

            #endregion

            #region Family Relation

            FREnt = new FAMILY_RELATIONS();
            FREnt.RELATION_OF_ID = SEnt.PK_ID;
            FREnt.RELATION = "Father";
            FREnt = (FAMILY_RELATIONS)FRSrv.GetSingle(FREnt);
            if (FREnt != null)
            {
                txtF_NAME.Text = FREnt.R_NAME;
                txtF_OCCUPATION.Text = FREnt.OCCUPATION;
                txtF_ADDRESS.Text = FREnt.ADDRESS;
                txtF_PHONE.Text = FREnt.PHONE;
                txtF_MOBILE1.Text = FREnt.MOBILE1;
                txtF_MOBILE2.Text = FREnt.MOBILE2;
                txtF_EMAIL.Text = FREnt.EMAIL;
                txtF_Position.Text = FREnt.DESIGNATION;
                txtF_Office.Text = FREnt.NAME_OF_COMPANY;
                txtF_Office_Phone.Text = FREnt.OFFICE_PHONE_NO;
            }

            FREnt = new FAMILY_RELATIONS();
            FREnt.RELATION_OF_ID = SEnt.PK_ID;
            FREnt.RELATION = "Mother";
            FREnt = (FAMILY_RELATIONS)FRSrv.GetSingle(FREnt);
            if (FREnt != null)
            {
                txtM_NAME.Text = FREnt.R_NAME;
                txtM_OCCUPATION.Text = FREnt.OCCUPATION;
                txtM_ADDRESS.Text = FREnt.ADDRESS;
                txtM_PHONE.Text = FREnt.PHONE;
                txtM_MOBILE1.Text = FREnt.MOBILE1;
                txtM_MOBILE2.Text = FREnt.MOBILE2;
                txtM_EMAIL.Text = FREnt.EMAIL;
                txtM_POSITION.Text = FREnt.DESIGNATION;
                txtM_OFFICE.Text = FREnt.NAME_OF_COMPANY;
                txtM_OFFICE_PHONE.Text = FREnt.OFFICE_PHONE_NO;
            }

            FREnt = new FAMILY_RELATIONS();
            FREnt.RELATION_OF_ID = SEnt.PK_ID;
            FREnt.RELATION = "Spouse";
            FREnt = (FAMILY_RELATIONS)FRSrv.GetSingle(FREnt);
            if (FREnt != null)
            {
                txtS_NAME.Text = FREnt.R_NAME;
                txtS_ADDRESS.Text = FREnt.ADDRESS;
                txtS_OCCUPATION.Text = FREnt.OCCUPATION;
                txtS_PHONE.Text = FREnt.PHONE;
                txtS_MOBILE1.Text = FREnt.MOBILE1;
                txtS_MOBILE2.Text = FREnt.MOBILE2;
                txtS_EMAIL.Text = FREnt.EMAIL;
                txtS_POSITION.Text = FREnt.DESIGNATION;
                txtS_OFFICE.Text = FREnt.NAME_OF_COMPANY;
                txtS_OFFICE_PHONE.Text = FREnt.OFFICE_PHONE_NO;

                spouse_detail.Visible = true;
                spouse_detail1.Visible = true;
                spouse_detail2.Visible = true;
                spouse_fieldset.Visible = true;

            }
            FREnt = new FAMILY_RELATIONS();
            FREnt.RELATION_OF_ID = SEnt.PK_ID;
            FREnt.IS_GUARDIAN = "TRUE";
            FREnt = (FAMILY_RELATIONS)FRSrv.GetSingle(FREnt);
            if (FREnt != null)
            {
                if (ddlGuardian.SelectedValue != "Select")
                {
                    txtG_NAME.Enabled = false;
                    txtG_RELATION.Enabled = false;
                    txtG_OCCUPATION.Enabled = false;
                    txtG_ADDRESS.Enabled = false;
                    txtG_PHONE.Enabled = false;
                    txtG_MOBILE1.Enabled = false;
                    txtG_MOBILE2.Enabled = false;
                    txtG_EMAIL.Enabled = false;
                    txtG_POSITION.Enabled = false;
                    txtG_OFFICE.Enabled = false;
                    txtG_OFFICE_PHONE.Enabled = false;
                }

                txtG_NAME.Text = FREnt.R_NAME;
                txtG_RELATION.Text = FREnt.RELATION;
                txtG_OCCUPATION.Text = FREnt.OCCUPATION;
                txtG_ADDRESS.Text = FREnt.ADDRESS;
                txtG_PHONE.Text = FREnt.PHONE;
                txtG_MOBILE1.Text = FREnt.MOBILE1;
                txtG_MOBILE2.Text = FREnt.MOBILE2;
                txtG_EMAIL.Text = FREnt.EMAIL;
                txtG_POSITION.Text = FREnt.DESIGNATION;
                txtG_OFFICE.Text = FREnt.NAME_OF_COMPANY;
                txtG_OFFICE_PHONE.Text = FREnt.OFFICE_PHONE_NO;

                if (FREnt.RELATION == "Father")
                {
                    ddlGuardian.SelectedIndex = 1;
                }
                else if (FREnt.RELATION == "Mother")
                {
                    ddlGuardian.SelectedIndex = 2;
                }
                else if (FREnt.RELATION == "Spouse")
                {
                    ddlGuardian.SelectedIndex = 3;
                }
                else
                {
                    ddlGuardian.SelectedIndex = 0;
                }
            }
            ddlBatch.SelectedValue = SEnt.BAT_CH;

            #endregion

            #region Admission_detail

            ADEnt = new ADMISSION_DETAIL();
            ADEnt.STUDENT_PKID = SEnt.PK_ID;
            ADEnt = (ADMISSION_DETAIL)ADSrv.GetSingle(ADEnt);
            if (ADEnt != null)
            {
                txtRemarks.Text = ADEnt.REMARKS;

                txtEntranceRollNo.Text = ADEnt.ENTRANCE_ROLLNO;
                txtEntranceDate.Text = ADEnt.ENTRANCE_DATE;
                txtMarksObtained.Text = ADEnt.OBTAINED_MARKS;
                txtInterviewBy.Text = ADEnt.INTERVIEW_BY;
                txtInterviewDate.Text = ADEnt.INTERVIEW_DATE;
                txtInterviewResult.Text = ADEnt.INTERVIEW_MARKS;

                txtAdmissionDate.Text = ADEnt.ADMISSION_DATE;
                txtFilledBy.Text = ADEnt.FORM_FILLED_BY;
                txtFilledDate.Text = ADEnt.FORM_FILLED_DATE;
                txtVerifiedBy.Text = ADEnt.VERIFIED_BY;
                txtVerifiedDate.Text = ADEnt.VERIFIED_DATE;
            }

            #endregion

            #region hss_education_detail

            EDEnt = new HSS_EDUCATION_DETAIL();
            EDEnt.STUDENT_PKID = SEnt.PK_ID;
            EDEnt.PROGRAM_LEVEL = "SLC Information";
            EDEnt = (HSS_EDUCATION_DETAIL)EDSer.GetSingle(EDEnt);
            if (EDEnt != null)
            {
                #region SLC
                txtBOARD.Text = EDEnt.BOARD;
                txtSCHOOL.Text = EDEnt.INSTITUTION;
                LoadCountrySLC();
                ddlCountrySLC.SelectedValue = EDEnt.COUNTRY;
                LoadState_SLC();
                ddlState_SLC.SelectedValue = EDEnt.STATE;
                School_Zone();
                ddlZone_School.SelectedValue = EDEnt.ZONE;


                if (EDEnt.DISTRICT == "")
                {
                    ddlDistrict_School.SelectedIndex = 0;
                }
                else
                {

                    DEnt = new District();
                    DEnt.ZONE_ID = ddlZone_School.SelectedValue;
                    ddlDistrict_School.DataSource = DSer.GetAll(DEnt);
                    ddlDistrict_School.DataTextField = "DISTRICTNAME";
                    ddlDistrict_School.DataValueField = "ID";
                    ddlDistrict_School.DataBind();
                    ddlDistrict_School.SelectedValue = EDEnt.DISTRICT;
                }
                txtPASSED_YEAR.Text = EDEnt.PASSED_YEAR;
                txtPERCENTAGE.Text = EDEnt.PERCENTAGE;
                txtDIVISION.Text = EDEnt.DIVISION;
                txtSYMBOLE_NO.Text = EDEnt.SYMBOLE_NO;
                txtOPT_SUBJ1.Text = EDEnt.OPT_SUBJ1;
                txtOPT_SUBJ2.Text = EDEnt.OPT_SUBJ2;
                txtOPT_SUBJ3.Text = EDEnt.OPT_SUBJ3;


                #endregion
            }

            EDEnt = new HSS_EDUCATION_DETAIL();
            EDEnt.STUDENT_PKID = SEnt.PK_ID;
            EDEnt.PROGRAM_LEVEL = "+2 Information";
            EDEnt = (HSS_EDUCATION_DETAIL)EDSer.GetSingle(EDEnt);
            if (EDEnt != null)
            {
                #region +2
                txtPTBoard.Text = EDEnt.BOARD;
                txtCOLLEGE.Text = EDEnt.INSTITUTION;
                LoadCountryPT();
                ddlCountryPT.SelectedValue = EDEnt.COUNTRY;
                LoadState_PT();
                ddlState_PT.SelectedValue = EDEnt.STATE;

                if (EDEnt.ZONE == "")
                {
                    ddlZone_PT.SelectedIndex = 0;
                }
                else
                {

                    ZEnt = new zone();
                    ddlZone_PT.DataSource = ZSer.GetAll(ZEnt);
                    ddlZone_PT.DataTextField = "ZONE_NAME";
                    ddlZone_PT.DataValueField = "ZONE_ID";
                    ddlZone_PT.DataBind();
                    ddlZone_PT.SelectedValue = EDEnt.ZONE;
                }


                LoadPTDistrict();
                ddlDistrict_PT.SelectedValue = EDEnt.DISTRICT;

                ddlFacultyPT.SelectedValue = EDEnt.FACULTY;

                txtPTPASSED_YEAR.Text = EDEnt.PASSED_YEAR;
                txtPTPERCENTAGE.Text = EDEnt.PERCENTAGE;
                txtPTDIVISION.Text = EDEnt.DIVISION;
                txtPTSYMBOLE_NO.Text = EDEnt.SYMBOLE_NO;
                txtPTOPT_SUBJ1.Text = EDEnt.OPT_SUBJ1;
                txtPTOPT_SUBJ2.Text = EDEnt.OPT_SUBJ2;
                txtPTOPT_SUBJ3.Text = EDEnt.OPT_SUBJ3;

                #endregion


                lblEducationDetailPkid.Text = EDEnt.PK_ID;
            }

            #region master
            if (ddlLevel.SelectedValue == "Master")
            {
                divBachelorInfo.Visible = true;

                EDEnt = new HSS_EDUCATION_DETAIL();
                EDEnt.STUDENT_PKID = SEnt.PK_ID;
                EDEnt.PROGRAM_LEVEL = "Bachelor Information";
                EDEnt = (HSS_EDUCATION_DETAIL)EDSer.GetSingle(EDEnt);
                if (EDEnt != null)
                {
                    #region Bachelor
                    txtUniversity.Text = EDEnt.BOARD;
                    txtCollege_B.Text = EDEnt.INSTITUTION;
                    LoadCountryB();
                    ddlCountryB.SelectedValue = EDEnt.COUNTRY;
                    LoadState_B();
                    ddlState_B.SelectedValue = EDEnt.STATE;

                    ZEnt = new zone();
                    ddlZone_B.DataSource = ZSer.GetAll(ZEnt);
                    ddlZone_B.DataTextField = "ZONE_NAME";
                    ddlZone_B.DataValueField = "ZONE_ID";
                    ddlZone_B.DataBind();
                    ddlZone_B.SelectedValue = EDEnt.ZONE;


                    LoadPTDistrict();
                    ddlDistrict_PT.SelectedValue = EDEnt.DISTRICT;

                    ddlFacultyPT.SelectedValue = EDEnt.FACULTY;

                    txtPTPASSED_YEAR.Text = EDEnt.PASSED_YEAR;
                    txtPTPERCENTAGE.Text = EDEnt.PERCENTAGE;
                    txtPTDIVISION.Text = EDEnt.DIVISION;
                    txtPTSYMBOLE_NO.Text = EDEnt.SYMBOLE_NO;
                    txtPTOPT_SUBJ1.Text = EDEnt.OPT_SUBJ1;
                    txtPTOPT_SUBJ2.Text = EDEnt.OPT_SUBJ2;
                    txtPTOPT_SUBJ3.Text = EDEnt.OPT_SUBJ3;

                    #endregion

                }
            }
            #endregion

            #endregion

            #region hss_attachments

            AEnt = new HSS_ATTACHMENTS();
            AEnt.STUDENT_PKID = SEnt.PK_ID;
            AEnt = (HSS_ATTACHMENTS)ASer.GetSingle(AEnt);
            if (AEnt != null)
            {
                lblAttachmentPkid.Text = AEnt.PK_ID;
                if (AEnt.PHOTO == "True")
                    chkPHOTO.Checked = true;
                else
                    chkPHOTO.Checked = false;

                if (AEnt.TRANSCRIPT == "True")
                    chkTRANSCRIPT.Checked = true;
                else
                    chkTRANSCRIPT.Checked = false;

                if (AEnt.PT_TRANSCRIPT == "True")
                    chkPTTRANSCRIPT.Checked = true;
                else
                    chkPTTRANSCRIPT.Checked = false;

                if (AEnt.CERTIFICATE == "True")
                    chkCERTIFICATE.Checked = true;
                else
                    chkCERTIFICATE.Checked = false;

                if (AEnt.CHARACTER_CERTIFICATE == "True")
                    chkCHARACTER_CERTIFICATE.Checked = true;
                else
                    chkCHARACTER_CERTIFICATE.Checked = false;

                if (AEnt.PT_CHARACTER_CERTIFICATE == "True")
                    chkPTCHARACTER_CERTIFICATE.Checked = true;
                else
                    chkPTCHARACTER_CERTIFICATE.Checked = false;

                if (AEnt.CITIZENSHIP == "True")
                    chkCITIZENSHIP.Checked = true;
                else
                    chkCITIZENSHIP.Checked = false;


                if (AEnt.PT_PROVISIONAL_CERTIFICATE == "True")
                    chkPROVISIONCERTIFICATE.Checked = true;
                else
                    chkPROVISIONCERTIFICATE.Checked = false;

                if (AEnt.PT_MIGRATION_CERTIFICATE == "True")
                    chkMIGRATIONCERTIFICATE.Checked = true;
                else
                    chkMIGRATIONCERTIFICATE.Checked = false;


                if (AEnt.B_TRANSCRIPT == "True")
                    chkBTranscript.Checked = true;
                else
                    chkBTranscript.Checked = false;

                if (AEnt.B_MIGRATION == "True")
                    chkBMigrationCertificate.Checked = true;
                else
                    chkBMigrationCertificate.Checked = false;

            }

            #endregion

            #region information alert

            INAEnt = new information_alert();
            INAEnt.STUDENT_PKID = SEnt.PK_ID;
            INAEnt = (information_alert)INASer.GetSingle(INAEnt);
            if (INAEnt != null)
            {
                txtSMSAlert1.Text = INAEnt.SMS_ALERT1;
                txtSMSAlert2.Text = INAEnt.SMS_ALERT2;
                txtEmailAlert1.Text = INAEnt.EMAIL_ALERT1;
                txtEmailAlert2.Text = INAEnt.EMAIL_ALERT2;

                if (INAEnt.SMS_ALERT1_STATUS == "1")
                {
                    chkSMSAlert1Status.Checked = true;
                }
                else
                {
                    chkSMSAlert1Status.Checked = false;
                }

                if (INAEnt.SMS_ALERT2_STATUS == "1")
                {
                    chkSMSAlert2Status.Checked = true;
                }
                else
                {
                    chkSMSAlert2Status.Checked = false;
                }

                if (INAEnt.EMAIL_ALERT1_STATUS == "1")
                {
                    chkEmailAlert1Status.Checked = true;
                }
                else
                {
                    chkEmailAlert1Status.Checked = false;
                }
                if (INAEnt.EMAIL_ALERT2_STATUS == "1")
                {
                    chkEmailAlert2Status.Checked = true;
                }
                else
                {
                    chkEmailAlert2Status.Checked = false;
                }
            }

            #endregion
        }
    }

    protected void clearFields()
    {
        lblpkid_hss_Std.Text = "";

        ddlFaculty.SelectedIndex = 0;
        txtNAME_ENGLISH.Text = "";
        txtNAME_DEVANAGARI.Text = "";
        rbtnGENDER.SelectedValue = "M";
        rbtnMaritalStatus.SelectedIndex = 0;
        txtDOB_BSDay.Text = "";
        txtDOB_BSMth.Text = "";
        txtDOB_BSYear.Text = "";

        txtDOB_ADDay.Text = "";
        txtDOB_ADMth.Text = "";
        txtDOB_ADYear.Text = "";

        ddlNATIONALITY.SelectedIndex = 0;
        ddlRELIGION.SelectedIndex = 0;
        txtCITIZENSHIP_NO.Text = "";
        txtPHONE.Text = "";
        txtMOBILE_1.Text = "";
        txtMOBILE_2.Text = "";
        txtEMAIL.Text = "";

        ddlCountryPA.SelectedIndex = 0;
        ddlStatePA.SelectedIndex = 0;
        ddlPA_Zone.SelectedIndex = 0;
        ddlPA_DISTRICT.SelectedIndex = 0;

        ddlCountry_CA.SelectedIndex = 0;
        ddlState_CA.SelectedIndex = 0;
        ddlTA_Zone.SelectedIndex = 0;
        ddlTA_DISTRICT.SelectedIndex = 0;

        txtPA_HOUSE_NO.Text = "";
        txtPA_STREET.Text = "";
        txtPA_VDC_MUNI.Text = "";
        ddlPA_DISTRICT.SelectedIndex = 0;
        txtPA_WARD_NO.Text = "";

        txtTA_HOUSE_NO.Text = "";
        txtTA_STREET.Text = "";
        txtTA_VDC_MUNI.Text = "";
        ddlTA_DISTRICT.SelectedIndex = 0;
        txtTA_WARD_NO.Text = "";

        ddlCountry_CA.Enabled = true;
        ddlState_CA.Enabled = true;
        ddlTA_Zone.Enabled = true;
        ddlTA_DISTRICT.Enabled = true;
        txtTA_VDC_MUNI.Enabled = true;
        txtTA_WARD_NO.Enabled = true;
        txtTA_STREET.Enabled = true;
        txtTA_HOUSE_NO.Enabled = true;

        txtF_NAME.Text = "";
        txtF_OCCUPATION.Text = "";
        txtF_ADDRESS.Text = "";
        txtF_PHONE.Text = "";
        txtF_MOBILE1.Text = "";
        txtF_MOBILE2.Text = "";
        txtF_EMAIL.Text = "";
        txtM_NAME.Text = "";
        txtM_OCCUPATION.Text = "";
        txtM_ADDRESS.Text = "";
        txtM_PHONE.Text = "";
        txtM_MOBILE1.Text = "";
        txtM_MOBILE2.Text = "";
        txtM_EMAIL.Text = "";
        txtG_NAME.Text = "";
        txtG_ADDRESS.Text = "";
        txtG_OCCUPATION.Text = "";
        txtG_PHONE.Text = "";
        txtG_MOBILE1.Text = "";
        txtG_MOBILE2.Text = "";
        txtG_EMAIL.Text = "";
        txtG_RELATION.Text = "";

        txtG_NAME.Enabled = true;
        txtG_RELATION.Enabled = true;
        txtG_ADDRESS.Enabled = true;
        txtG_PHONE.Enabled = true;
        txtG_MOBILE1.Enabled = true;
        txtG_MOBILE2.Enabled = true;
        txtG_EMAIL.Enabled = true;
        txtG_OCCUPATION.Enabled = true;
        txtG_POSITION.Enabled = true;
        txtG_OFFICE.Enabled = true;
        txtG_OFFICE_PHONE.Enabled = true;

        ddlBatch.SelectedIndex = 0;

        lblAdmissionNo.Text = "";

        txtS_NAME.Text = "";
        txtS_ADDRESS.Text = "";
        txtS_OCCUPATION.Text = "";
        txtS_PHONE.Text = "";
        txtS_MOBILE1.Text = "";
        txtS_MOBILE2.Text = "";
        txtS_EMAIL.Text = "";

        txtRemarks.Text = "";

        txtEntranceRollNo.Text = "";
        txtEntranceDate.Text = "";
        txtMarksObtained.Text = "";
        txtInterviewBy.Text = "";
        txtInterviewDate.Text = "";
        txtInterviewResult.Text = "";

        txtAdmissionDate.Text = "";
        txtUniRegdNo.Text = "";
        txtFilledBy.Text = "";
        txtFilledDate.Text = "";
        txtVerifiedBy.Text = "";
        txtVerifiedDate.Text = "";

        txtF_Position.Text = "";
        txtF_Office.Text = "";
        txtF_Office_Phone.Text = "";

        txtM_POSITION.Text = "";
        txtM_OFFICE.Text = "";
        txtM_OFFICE_PHONE.Text = "";

        txtG_POSITION.Text = "";
        txtG_OFFICE.Text = "";
        txtG_OFFICE_PHONE.Text = "";

        txtS_POSITION.Text = "";
        txtS_OFFICE.Text = "";
        txtS_OFFICE_PHONE.Text = "";

        lblCurrentStudentPkid.Text = "";
        ddlContactAddress.SelectedIndex = 0;

        txtBOARD.Text = "";
        txtSCHOOL.Text = "";
        ddlCountrySLC.SelectedIndex = 0;
        txtcountryAddSLC.Text = "";
        txtPASSED_YEAR.Text = "";
        txtPERCENTAGE.Text = "";
        txtDIVISION.Text = "";
        txtSYMBOLE_NO.Text = "";
        txtOPT_SUBJ1.Text = "";
        txtOPT_SUBJ2.Text = "";
        txtOPT_SUBJ3.Text = "";

        txtPTBoard.Text = "";
        txtCOLLEGE.Text = "";
        txtPTPASSED_YEAR.Text = "";
        txtPTPERCENTAGE.Text = "";
        txtPTDIVISION.Text = "";
        txtPTSYMBOLE_NO.Text = "";
        txtPTOPT_SUBJ1.Text = "";
        txtPTOPT_SUBJ2.Text = "";
        txtPTOPT_SUBJ3.Text = "";

        ddlState_SLC.SelectedIndex = 0;
        ddlZone_School.SelectedIndex = 0;
        ddlDistrict_School.SelectedIndex = 0;

        ddlState_PT.SelectedIndex = 0;
        ddlZone_PT.SelectedIndex = 0;
        ddlDistrict_PT.SelectedIndex = 0;

        ddlFacultyPT.SelectedIndex = 0;
        ddlGuardian.SelectedIndex = 0;

        lblEducationDetailPkid.Text = "";

        lblAttachmentPkid.Text = "";

        chkPHOTO.Checked = false;

        chkTRANSCRIPT.Checked = false;

        chkCERTIFICATE.Checked = false;

        chkPTTRANSCRIPT.Checked = false;

        chkPTCHARACTER_CERTIFICATE.Checked = false;

        chkCHARACTER_CERTIFICATE.Checked = false;

        chkCITIZENSHIP.Checked = false;

        chkPROVISIONCERTIFICATE.Checked = false;

        chkMIGRATIONCERTIFICATE.Checked = false;

        chkBTranscript.Checked = false;

        chkBCharacterCertificate.Checked = false;

        chkBMigrationCertificate.Checked = false;

        txtSMSAlert1.Text = "";
        txtSMSAlert2.Text = "";
        txtEmailAlert1.Text = "";
        txtEmailAlert2.Text = "";
        chkSMSAlert1Status.Checked = false;
        chkSMSAlert2Status.Checked = false;
        chkEmailAlert1Status.Checked = false;
        chkEmailAlert2Status.Checked = false;

    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        clearFields();
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (lblpkid_hss_Std.Text != "")
        {
            UpdateData(lblpkid_hss_Std.Text);
        }
        else
        {

            SaveData();
        }
    }
    public void SaveData()
    {
        DT = new DistributedTransaction();
        string filename = "";
        try
        {
            filename = lblpkid_hss_Std.Text + FileUpload1.FileName.Substring(FileUpload1.FileName.IndexOf('.'));
            if (FileUpload1.HasFile)
            {
                FileUpload1.SaveAs(Server.MapPath("~/images/bachelorstudent/" + filename));
            }
        }
        catch
        {
        }

        #region HSS_STUDENT
        SEnt = new HSS_STUDENT();

        SEnt.NAME_ENGLISH = txtNAME_ENGLISH.Text;
        SEnt.NAME_DEVANAGARI = txtNAME_DEVANAGARI.Text;
        SEnt.GENDER = rbtnGENDER.SelectedValue;
        if (txtDOB_BSDay.Text != "" && txtDOB_BSMth.Text != "" && txtDOB_BSYear.Text != "")
        {
            SEnt.DOB_BS = txtDOB_BSDay.Text + "/" + txtDOB_BSMth.Text + "/" + txtDOB_BSYear.Text;
        }
        if (txtDOB_ADDay.Text != "" && txtDOB_ADMth.Text != "" && txtDOB_ADYear.Text != "")
        {
            SEnt.DOB_AD = txtDOB_ADDay.Text + "/" + txtDOB_ADMth.Text + "/" + txtDOB_ADYear.Text;
        }
        SEnt.NATIONALITY = ddlNATIONALITY.SelectedValue;
        SEnt.RELIGION = ddlRELIGION.SelectedValue;
        SEnt.MARITAL_STATUS = rbtnMaritalStatus.SelectedValue;
        SEnt.CITIZENSHIP_NO = txtCITIZENSHIP_NO.Text;
        SEnt.PHONE = txtPHONE.Text;
        SEnt.MOBILE_1 = txtMOBILE_1.Text;
        SEnt.MOBILE_2 = txtMOBILE_2.Text;
        SEnt.EMAIL = txtEMAIL.Text;
        SEnt.BAT_CH = ddlBatch.SelectedValue;
        SEnt.PROGRAM = ddlProgram.SelectedValue;
        SEnt.UNIVERSITY_REG_NO = txtUniRegdNo.Text;

        string student_PKID = SSer.Insert(SEnt, DT).ToString();

        #endregion

        #region ADMISSION_DETAIL
        ADEnt = new ADMISSION_DETAIL();
        ADEnt.STUDENT_PKID = student_PKID;
        ADEnt.ENTRANCE_ROLLNO = txtEntranceRollNo.Text;
        ADEnt.ENTRANCE_DATE = txtEntranceDate.Text;
        ADEnt.OBTAINED_MARKS = txtMarksObtained.Text;
        ADEnt.INTERVIEW_DATE = txtInterviewDate.Text;
        ADEnt.INTERVIEW_BY = txtInterviewBy.Text;
        ADEnt.INTERVIEW_MARKS = txtInterviewResult.Text;
        ADEnt.ADMISSION_NO = txtAdmissionNo.Text;
        ADEnt.ADMISSION_DATE = txtAdmissionDate.Text;
        ADEnt.FORM_FILLED_BY = txtFilledBy.Text;
        ADEnt.FORM_FILLED_DATE = txtFilledDate.Text;
        ADEnt.VERIFIED_BY = txtVerifiedBy.Text;
        ADEnt.VERIFIED_DATE = txtVerifiedDate.Text;
        ADEnt.REMARKS = txtRemarks.Text;

        ADSrv.Insert(ADEnt, DT);

        #endregion

        #region HSS_EDUCATION_DETAIL

        #region for SLC

        if (ddlCountrySLC.SelectedValue == "1")
        {
            EDEnt = new HSS_EDUCATION_DETAIL();
            EDEnt.STUDENT_PKID = student_PKID;
            EDEnt.BOARD = txtBOARD.Text;
            EDEnt.INSTITUTION = txtSCHOOL.Text;
            EDEnt.PASSED_YEAR = txtPASSED_YEAR.Text;
            EDEnt.PERCENTAGE = txtPERCENTAGE.Text;
            EDEnt.DIVISION = txtDIVISION.Text;
            EDEnt.SYMBOLE_NO = txtSYMBOLE_NO.Text;
            EDEnt.OPT_SUBJ1 = txtOPT_SUBJ1.Text;
            EDEnt.OPT_SUBJ2 = txtOPT_SUBJ2.Text;
            EDEnt.OPT_SUBJ3 = txtOPT_SUBJ3.Text;
            EDEnt.PROGRAM_LEVEL = lblSchLevel.InnerText;
            if (ddlState_SLC.SelectedValue == "Select")
            {
                EDEnt.STATE = "";
            }
            else
            {
                EDEnt.STATE = ddlState_SLC.SelectedValue;
            }
            if (ddlZone_School.SelectedValue == "Select")
            {
                EDEnt.ZONE = "";
            }
            else
            {
                EDEnt.ZONE = ddlZone_School.SelectedValue;
            }

            if (ddlDistrict_School.SelectedValue == "Select")
            {
                EDEnt.DISTRICT = "";
            }
            else
            {
                EDEnt.DISTRICT = ddlDistrict_School.SelectedValue;
            }

            EDEnt.COUNTRY = ddlCountrySLC.SelectedValue;
            EDEnt.ADDRESS = txtcountryAddSLC.Text;

            EDSer.Insert(EDEnt, DT);
        }
        else
        {
            EDEnt = new HSS_EDUCATION_DETAIL();
            EDEnt.STUDENT_PKID = student_PKID;
            EDEnt.BOARD = txtBOARD.Text;
            EDEnt.INSTITUTION = txtSCHOOL.Text;
            EDEnt.PASSED_YEAR = txtPASSED_YEAR.Text;
            EDEnt.PERCENTAGE = txtPERCENTAGE.Text;
            EDEnt.DIVISION = txtDIVISION.Text;
            EDEnt.SYMBOLE_NO = txtSYMBOLE_NO.Text;
            EDEnt.OPT_SUBJ1 = txtOPT_SUBJ1.Text;
            EDEnt.OPT_SUBJ2 = txtOPT_SUBJ2.Text;
            EDEnt.OPT_SUBJ3 = txtOPT_SUBJ3.Text;
            EDEnt.PROGRAM_LEVEL = lblSchLevel.InnerText;
            EDEnt.STATE = "";
            EDEnt.ZONE = "";
            EDEnt.DISTRICT = "";
            EDEnt.COUNTRY = ddlCountrySLC.SelectedValue;
            EDEnt.ADDRESS = txtcountryAddSLC.Text;

            EDSer.Insert(EDEnt, DT);
        }

        #endregion

        #region for +2
        if (ddlCountryPT.SelectedValue == "1")
        {

            EDEnt = new HSS_EDUCATION_DETAIL();
            EDEnt.STUDENT_PKID = student_PKID;
            EDEnt.BOARD = txtPTBoard.Text;
            EDEnt.INSTITUTION = txtCOLLEGE.Text;
            EDEnt.PASSED_YEAR = txtPTPASSED_YEAR.Text;
            EDEnt.PERCENTAGE = txtPTPERCENTAGE.Text;
            EDEnt.DIVISION = txtPTDIVISION.Text;
            EDEnt.SYMBOLE_NO = txtPTSYMBOLE_NO.Text;
            EDEnt.OPT_SUBJ1 = txtPTOPT_SUBJ1.Text;
            EDEnt.OPT_SUBJ2 = txtPTOPT_SUBJ2.Text;
            EDEnt.OPT_SUBJ3 = txtPTOPT_SUBJ3.Text;
            EDEnt.FACULTY = ddlFacultyPT.SelectedValue;
            EDEnt.PROGRAM_LEVEL = lblPTLevel.InnerText;
            if (ddlState_PT.SelectedValue == "Select")
            {
                EDEnt.STATE = "";
            }
            else
            {
                EDEnt.STATE = ddlState_PT.SelectedValue;
            }
            if (ddlZone_PT.SelectedValue == "Select")
            {
                EDEnt.ZONE = "";
            }
            else
            {

                EDEnt.ZONE = ddlZone_PT.SelectedValue;
            }
            if (ddlDistrict_PT.SelectedValue == "Select")
            {
                EDEnt.DISTRICT = "";
            }
            else
            {
                EDEnt.DISTRICT = ddlDistrict_PT.SelectedValue;
            }
            EDEnt.COUNTRY = ddlCountryPT.SelectedValue;
            EDEnt.ADDRESS = txtCountryAddPT.Text;

            EDSer.Insert(EDEnt, DT);
        }
        else
        {
            EDEnt = new HSS_EDUCATION_DETAIL();
            EDEnt.STUDENT_PKID = student_PKID;
            EDEnt.BOARD = txtPTBoard.Text;
            EDEnt.INSTITUTION = txtCOLLEGE.Text;
            EDEnt.PASSED_YEAR = txtPTPASSED_YEAR.Text;
            EDEnt.PERCENTAGE = txtPTPERCENTAGE.Text;
            EDEnt.DIVISION = txtPTDIVISION.Text;
            EDEnt.SYMBOLE_NO = txtPTSYMBOLE_NO.Text;
            EDEnt.OPT_SUBJ1 = txtPTOPT_SUBJ1.Text;
            EDEnt.OPT_SUBJ2 = txtPTOPT_SUBJ2.Text;
            EDEnt.OPT_SUBJ3 = txtPTOPT_SUBJ3.Text;
            EDEnt.FACULTY = ddlFacultyPT.SelectedValue;
            EDEnt.PROGRAM_LEVEL = lblPTLevel.InnerText;
            EDEnt.STATE = "";
            EDEnt.ZONE = "";
            EDEnt.DISTRICT = "";
            EDEnt.COUNTRY = ddlCountryPT.SelectedValue;
            EDEnt.ADDRESS = txtCountryAddPT.Text;

            EDSer.Insert(EDEnt, DT);
        }

        #endregion

        if (ddlLevel.SelectedValue == "Master")
        {

            #region for bachelor

            if (ddlCountryB.SelectedValue == "1")
            {

                EDEnt = new HSS_EDUCATION_DETAIL();
                EDEnt.STUDENT_PKID = student_PKID;
                EDEnt.BOARD = txtUniversity.Text;
                EDEnt.INSTITUTION = txtCollege_B.Text;
                EDEnt.PASSED_YEAR = txtPassedyear_B.Text;
                EDEnt.PERCENTAGE = txtPercentage_B.Text;
                EDEnt.DIVISION = txtDivision_B.Text;
                EDEnt.SYMBOLE_NO = txtSymbol_B.Text;
                EDEnt.OPT_SUBJ1 = txtSub1_B.Text;
                EDEnt.OPT_SUBJ2 = txtSub2_B.Text;
                EDEnt.OPT_SUBJ3 = txtSub3_B.Text;
                EDEnt.FACULTY = ddlFaculty_B.SelectedValue;
                EDEnt.PROGRAM_LEVEL = lblBLevel.InnerText;
                EDEnt.PROGRAM = txtBProgram.Text;
                if (ddlState_B.SelectedValue == "Select")
                {
                    EDEnt.STATE = "";
                }
                else
                {
                    EDEnt.STATE = ddlState_B.SelectedValue;
                }

                if (ddlZone_B.SelectedValue == "Select")
                {
                    EDEnt.ZONE = "";
                }
                else
                {
                    EDEnt.ZONE = ddlZone_B.SelectedValue;
                }

                if (ddlDistrict_B.SelectedValue == "Select")
                {
                    EDEnt.DISTRICT = "";
                }
                else
                {
                    EDEnt.DISTRICT = ddlDistrict_B.SelectedValue;
                }
                EDEnt.COUNTRY = ddlCountryB.SelectedValue;
                EDEnt.ADDRESS = txtCountryAddB.Text;

                EDSer.Insert(EDEnt, DT);
            }
            else
            {
                EDEnt = new HSS_EDUCATION_DETAIL();
                EDEnt.STUDENT_PKID = student_PKID;
                EDEnt.BOARD = txtUniversity.Text;
                EDEnt.INSTITUTION = txtCollege_B.Text;
                EDEnt.PASSED_YEAR = txtPassedyear_B.Text;
                EDEnt.PERCENTAGE = txtPercentage_B.Text;
                EDEnt.DIVISION = txtDivision_B.Text;
                EDEnt.SYMBOLE_NO = txtSymbol_B.Text;
                EDEnt.OPT_SUBJ1 = txtSub1_B.Text;
                EDEnt.OPT_SUBJ2 = txtSub2_B.Text;
                EDEnt.OPT_SUBJ3 = txtSub3_B.Text;
                EDEnt.FACULTY = ddlFaculty_B.SelectedValue;
                EDEnt.PROGRAM_LEVEL = lblBLevel.InnerText;
                EDEnt.PROGRAM = txtBProgram.Text;
                EDEnt.STATE = "";
                EDEnt.ZONE = "";
                EDEnt.DISTRICT = "";
                EDEnt.COUNTRY = ddlCountryB.SelectedValue;
                EDEnt.ADDRESS = txtCountryAddB.Text;

                EDSer.Insert(EDEnt, DT);
            }

            #endregion
        }
        #endregion

        #region HSS_ATTACHMENTS

        AEnt = new HSS_ATTACHMENTS();
        AEnt.STUDENT_PKID = student_PKID;
        AEnt.PHOTO = chkPHOTO.Checked.ToString();
        AEnt.TRANSCRIPT = chkTRANSCRIPT.Checked.ToString();
        AEnt.CERTIFICATE = chkCERTIFICATE.Checked.ToString();
        AEnt.PT_TRANSCRIPT = chkPTTRANSCRIPT.Checked.ToString();
        AEnt.PT_CHARACTER_CERTIFICATE = chkPTCHARACTER_CERTIFICATE.Checked.ToString();
        AEnt.CHARACTER_CERTIFICATE = chkCHARACTER_CERTIFICATE.Checked.ToString();
        AEnt.CITIZENSHIP = chkCITIZENSHIP.Checked.ToString();
        AEnt.PT_PROVISIONAL_CERTIFICATE = chkPROVISIONCERTIFICATE.Checked.ToString();
        AEnt.PT_MIGRATION_CERTIFICATE = chkMIGRATIONCERTIFICATE.Checked.ToString();
        AEnt.B_TRANSCRIPT = chkBTranscript.Checked.ToString();
        AEnt.B_CHARACTER_CERTIFICATE = chkBCharacterCertificate.Checked.ToString();
        AEnt.B_MIGRATION = chkBMigrationCertificate.Checked.ToString();

        ASer.Insert(AEnt, DT);

        #endregion

        #region information_alert

        INAEnt = new information_alert();
        INAEnt.STUDENT_PKID = student_PKID;
        INAEnt.SMS_ALERT1 = txtSMSAlert1.Text;
        INAEnt.SMS_ALERT2 = txtSMSAlert2.Text;
        INAEnt.EMAIL_ALERT1 = txtEmailAlert1.Text;
        INAEnt.EMAIL_ALERT2 = txtEmailAlert2.Text;

        if (chkSMSAlert1Status.Checked)
        {
            INAEnt.SMS_ALERT1_STATUS = "1";
        }
        else
        {
            INAEnt.SMS_ALERT1_STATUS = "0";
        }

        if (chkSMSAlert2Status.Checked)
        {
            INAEnt.SMS_ALERT2_STATUS = "1";
        }
        else
        {
            INAEnt.SMS_ALERT2_STATUS = "0";
        }

        if (chkEmailAlert1Status.Checked)
        {
            INAEnt.EMAIL_ALERT1_STATUS = "1";
        }
        else
        {
            INAEnt.EMAIL_ALERT1_STATUS = "0";
        }

        if (chkEmailAlert2Status.Checked)
        {
            INAEnt.EMAIL_ALERT2_STATUS = "1";
        }
        else
        {
            INAEnt.EMAIL_ALERT2_STATUS = "0";
        }

        INASer.Insert(INAEnt, DT);

        #endregion

        #region Father's info
        string father_id = "";
        string mother_id = "";
        string spouse_id = "";

        FREnt = new FAMILY_RELATIONS();
        FREnt.R_NAME = txtF_NAME.Text;
        FREnt.OCCUPATION = txtF_OCCUPATION.Text;
        FREnt.ADDRESS = txtF_ADDRESS.Text;
        FREnt.PHONE = txtF_PHONE.Text;
        FREnt.MOBILE1 = txtF_MOBILE1.Text;
        FREnt.MOBILE2 = txtF_MOBILE2.Text;
        FREnt.EMAIL = txtF_EMAIL.Text;
        FREnt.RELATION = "Father";
        FREnt.RELATION_OF = "Student";
        FREnt.RELATION_OF_ID = student_PKID;
        FREnt.DESIGNATION = txtF_Position.Text;
        FREnt.NAME_OF_COMPANY = txtF_Office.Text;
        FREnt.OFFICE_PHONE_NO = txtF_Office_Phone.Text;
        FREnt.IS_GUARDIAN = "FALSE";

        father_id = FRSrv.Insert(FREnt, DT).ToString();


        #endregion

        #region Mother's info

        FREnt = new FAMILY_RELATIONS();
        FREnt.R_NAME = txtM_NAME.Text;
        FREnt.OCCUPATION = txtM_OCCUPATION.Text;
        FREnt.ADDRESS = txtM_ADDRESS.Text;
        FREnt.PHONE = txtM_PHONE.Text;
        FREnt.MOBILE1 = txtM_MOBILE1.Text;
        FREnt.MOBILE2 = txtM_MOBILE2.Text;
        FREnt.EMAIL = txtM_EMAIL.Text;
        FREnt.RELATION = "Mother";
        FREnt.RELATION_OF = "Student";
        FREnt.RELATION_OF_ID = student_PKID;
        FREnt.DESIGNATION = txtM_POSITION.Text;
        FREnt.NAME_OF_COMPANY = txtM_OFFICE.Text;
        FREnt.OFFICE_PHONE_NO = txtM_OFFICE_PHONE.Text;
        FREnt.IS_GUARDIAN = "FALSE";

        mother_id = FRSrv.Insert(FREnt, DT).ToString();

        #endregion

        #region Spouse's info
        if (rbtnMaritalStatus.SelectedValue == "Married")
        {
            FREnt = new FAMILY_RELATIONS();
            FREnt.R_NAME = txtS_NAME.Text;
            FREnt.OCCUPATION = txtS_OCCUPATION.Text;
            FREnt.ADDRESS = txtS_ADDRESS.Text;
            FREnt.PHONE = txtS_PHONE.Text;
            FREnt.MOBILE1 = txtS_MOBILE1.Text;
            FREnt.MOBILE2 = txtS_MOBILE2.Text;
            FREnt.EMAIL = txtS_EMAIL.Text;
            FREnt.RELATION = "Spouse";
            FREnt.RELATION_OF = "Student";
            FREnt.RELATION_OF_ID = student_PKID;
            FREnt.DESIGNATION = txtS_POSITION.Text;
            FREnt.NAME_OF_COMPANY = txtS_OFFICE.Text;
            FREnt.OFFICE_PHONE_NO = txtS_OFFICE_PHONE.Text;
            FREnt.IS_GUARDIAN = "FALSE";


            spouse_id = FRSrv.Insert(FREnt, DT).ToString();
        }

        #endregion

        #region Guardian's info
        if (ddlGuardian.SelectedValue == "Select")
        {

            FREnt = new FAMILY_RELATIONS();
            FREnt.R_NAME = txtG_NAME.Text;
            FREnt.OCCUPATION = txtG_OCCUPATION.Text;
            FREnt.ADDRESS = txtG_ADDRESS.Text;
            FREnt.PHONE = txtG_PHONE.Text;
            FREnt.MOBILE1 = txtG_MOBILE1.Text;
            FREnt.MOBILE2 = txtG_MOBILE2.Text;
            FREnt.EMAIL = txtG_EMAIL.Text;
            FREnt.RELATION = txtG_RELATION.Text;
            FREnt.RELATION_OF = "Student";
            FREnt.RELATION_OF_ID = student_PKID;
            FREnt.DESIGNATION = txtG_POSITION.Text;
            FREnt.NAME_OF_COMPANY = txtG_OFFICE.Text;
            FREnt.OFFICE_PHONE_NO = txtG_OFFICE_PHONE.Text;
            FREnt.IS_GUARDIAN = "TRUE";

            FRSrv.Insert(FREnt, DT);


        }
        else
        {
            FREnt = new FAMILY_RELATIONS();

            if (ddlGuardian.SelectedValue == "Father")
                FREnt.PK_ID = father_id;
            else if (ddlGuardian.SelectedValue == "Mother")
                FREnt.PK_ID = mother_id;
            else if (ddlGuardian.SelectedValue == "Spouse")
                FREnt.PK_ID = spouse_id;

            FREnt = (FAMILY_RELATIONS)FRSrv.GetSingle(FREnt, DT);
            if (FREnt != null)
            {
                FREnt.IS_GUARDIAN = "TRUE";
            }
            FRSrv.Update(FREnt, DT);

        }

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

                    AddEnt = new ADDRESS();
                    AddEnt.COUNTRY = ddlCountryPA.SelectedValue;

                    if (ddlStatePA.SelectedIndex == 0)
                    {

                        AddEnt.STATE = "";
                    }
                    else
                    {
                        AddEnt.STATE = ddlStatePA.SelectedValue;
                    }
                    if (ddlPA_Zone.SelectedIndex == 0)
                    {
                        AddEnt.ZONE = "";
                    }
                    else
                    {
                        AddEnt.ZONE = ddlPA_Zone.SelectedValue;
                    }


                    if (ddlPA_DISTRICT.SelectedIndex == 0)
                    {
                        AddEnt.DISTRICT = "";
                    }
                    else
                    {
                        AddEnt.DISTRICT = ddlPA_DISTRICT.SelectedValue;
                    }


                    AddEnt.VDC_MUNICIPALITY = txtPA_VDC_MUNI.Text;
                    AddEnt.WARD_NO = txtPA_WARD_NO.Text;
                    AddEnt.STREET_NAME = txtPA_STREET.Text;
                    AddEnt.HOUSE_NO = txtPA_HOUSE_NO.Text;
                    AddEnt.ADDRESS_OF = "Student";
                    AddEnt.ADDRESS_OF_ID = student_PKID;
                    AddEnt.ADDRESS_TYPE = "Both";

                    AddSrv.Insert(AddEnt, DT);
                    #endregion
                }
                else
                {
                    #region permanent Address

                    AddEnt = new ADDRESS();
                    AddEnt.COUNTRY = ddlCountryPA.SelectedValue;
                    if (ddlStatePA.SelectedIndex == 0)
                    {

                        AddEnt.STATE = "";
                    }
                    else
                    {
                        AddEnt.STATE = ddlStatePA.SelectedValue;
                    }
                    if (ddlPA_Zone.SelectedIndex == 0)
                    {
                        AddEnt.ZONE = "";
                    }
                    else
                    {
                        AddEnt.ZONE = ddlPA_Zone.SelectedValue;
                    }


                    if (ddlPA_DISTRICT.SelectedIndex == 0)
                    {
                        AddEnt.DISTRICT = "";
                    }
                    else
                    {
                        AddEnt.DISTRICT = ddlPA_DISTRICT.SelectedValue;
                    }
                    AddEnt.VDC_MUNICIPALITY = txtPA_VDC_MUNI.Text;
                    AddEnt.WARD_NO = txtPA_WARD_NO.Text;
                    AddEnt.STREET_NAME = txtPA_STREET.Text;
                    AddEnt.HOUSE_NO = txtPA_HOUSE_NO.Text;
                    AddEnt.ADDRESS_OF = "Student";
                    AddEnt.ADDRESS_OF_ID = student_PKID;
                    AddEnt.ADDRESS_TYPE = "P";

                    AddSrv.Insert(AddEnt, DT);

                    #endregion


                    #region Contact Address

                    AddEnt = new ADDRESS();
                    AddEnt.COUNTRY = ddlCountry_CA.SelectedValue;

                    if (ddlState_CA.SelectedIndex == 0)
                    {

                        AddEnt.STATE = "";
                    }
                    else
                    {
                        AddEnt.STATE = ddlState_CA.SelectedValue;
                    }
                    if (ddlTA_Zone.SelectedIndex == 0)
                    {
                        AddEnt.ZONE = "";
                    }
                    else
                    {
                        AddEnt.ZONE = ddlTA_Zone.SelectedValue;
                    }


                    if (ddlTA_DISTRICT.SelectedIndex == 0)
                    {
                        AddEnt.DISTRICT = "";
                    }
                    else
                    {
                        AddEnt.DISTRICT = ddlTA_DISTRICT.SelectedValue;
                    }


                    AddEnt.VDC_MUNICIPALITY = txtTA_VDC_MUNI.Text;
                    AddEnt.WARD_NO = txtTA_WARD_NO.Text;
                    AddEnt.STREET_NAME = txtTA_STREET.Text;
                    AddEnt.HOUSE_NO = txtTA_HOUSE_NO.Text;
                    AddEnt.ADDRESS_OF = "Student";
                    AddEnt.ADDRESS_OF_ID = student_PKID;
                    AddEnt.ADDRESS_TYPE = "C";

                    AddSrv.Insert(AddEnt, DT);

                    #endregion

                }
                #endregion
            }

            else
            {
                #region for India

                #region parmanent Address


                AddEnt = new ADDRESS();
                AddEnt.COUNTRY = ddlCountryPA.SelectedValue;
                AddEnt.STATE = "";
                AddEnt.ZONE = "";
                AddEnt.DISTRICT = "";
                AddEnt.VDC_MUNICIPALITY = txtPA_VDC_MUNI.Text;
                AddEnt.WARD_NO = txtPA_WARD_NO.Text;
                AddEnt.STREET_NAME = txtPA_STREET.Text;
                AddEnt.HOUSE_NO = txtPA_HOUSE_NO.Text;
                AddEnt.ADDRESS_OF = "Student";
                AddEnt.ADDRESS_OF_ID = student_PKID;
                AddEnt.ADDRESS_TYPE = "P";


                AddSrv.Insert(AddEnt, DT);

                #endregion

                #region Contact Address

                AddEnt = new ADDRESS();
                AddEnt.COUNTRY = ddlCountry_CA.SelectedValue;

                if (ddlState_CA.SelectedIndex == 0)
                {

                    AddEnt.STATE = "";
                }
                else
                {
                    AddEnt.STATE = ddlState_CA.SelectedValue;
                }
                if (ddlTA_Zone.SelectedIndex == 0)
                {
                    AddEnt.ZONE = "";
                }
                else
                {
                    AddEnt.ZONE = ddlTA_Zone.SelectedValue;
                }

                AddEnt.VDC_MUNICIPALITY = txtTA_VDC_MUNI.Text;
                AddEnt.WARD_NO = txtTA_WARD_NO.Text;
                AddEnt.STREET_NAME = txtTA_STREET.Text;
                AddEnt.HOUSE_NO = txtTA_HOUSE_NO.Text;
                AddEnt.ADDRESS_OF = "Student";
                AddEnt.ADDRESS_OF_ID = student_PKID;
                AddEnt.ADDRESS_TYPE = "C";

                AddSrv.Insert(AddEnt, DT);

                #endregion

                #endregion

            }
        }



        else
        {
            HelperFunction.MsgBox(this, this.GetType(), "Data Not Saved. Contact Address must be Nepal");
        }

        #endregion

        if (DT.HAPPY == true)
        {
            DT.Commit();
            HelperFunction.MsgBox(this, this.GetType(), "Data Saved Succesfully");
            LoadCollegeCode();
            clearFields();
        }
        else
        {
            DT.Abort();
            HelperFunction.MsgBox(this, this.GetType(), "Data Not Saved. There is some problem");
        }
    }

    protected void UpdateData(string pkId)
    {
        DT = new DistributedTransaction();
        string filename = "";
        string path = "../images/bachelorstudent/" + ".jpg";
        try
        {
            filename = lblpkid_hss_Std.Text + FileUpload1.FileName.Substring(FileUpload1.FileName.IndexOf('.'));
            if (FileUpload1.HasFile)
            {
                FileUpload1.SaveAs(Server.MapPath("~/images/bachelorstudent/" + filename));
            }
        }
        catch
        {
        }

        #region HSS_STUDENT

        SEnt = new HSS_STUDENT();
        SEnt.PK_ID = lblpkid_hss_Std.Text;
        SEnt = (HSS_STUDENT)SSer.GetSingle(SEnt);
        if (SEnt != null)
        {
            SEnt.NAME_ENGLISH = txtNAME_ENGLISH.Text;
            SEnt.NAME_DEVANAGARI = txtNAME_DEVANAGARI.Text;
            SEnt.GENDER = rbtnGENDER.SelectedValue;
            if (txtDOB_BSDay.Text != "" && txtDOB_BSMth.Text != "" && txtDOB_BSYear.Text != "")
            {
                SEnt.DOB_BS = txtDOB_BSDay.Text + "/" + txtDOB_BSMth.Text + "/" + txtDOB_BSYear.Text;
            }
            if (txtDOB_ADDay.Text != "" && txtDOB_ADMth.Text != "" && txtDOB_ADYear.Text != "")
            {
                SEnt.DOB_AD = txtDOB_ADDay.Text + "/" + txtDOB_ADMth.Text + "/" + txtDOB_ADYear.Text;
            }
            SEnt.NATIONALITY = ddlNATIONALITY.SelectedValue;
            SEnt.RELIGION = ddlRELIGION.SelectedValue;
            SEnt.MARITAL_STATUS = rbtnMaritalStatus.SelectedValue;
            SEnt.CITIZENSHIP_NO = txtCITIZENSHIP_NO.Text;
            SEnt.PHONE = txtPHONE.Text;
            SEnt.MOBILE_1 = txtMOBILE_1.Text;
            SEnt.MOBILE_2 = txtMOBILE_2.Text;
            SEnt.EMAIL = txtEMAIL.Text;
            SEnt.BAT_CH = ddlBatch.SelectedValue;
            SEnt.PROGRAM = ddlProgram.SelectedValue;
            SEnt.UNIVERSITY_REG_NO = txtUniRegdNo.Text;

            SSer.Update(SEnt, DT);
        }

        #endregion

        #region Education Detail

        #region for SLC
        EDEnt = new HSS_EDUCATION_DETAIL();
        //EDEnt.STUDENT_PKID = SEnt.PK_ID;
        //EDEnt.STUDENT_ID = lblpkid_hss_Std.Text;

        EDEnt.STUDENT_PKID = lblpkid_hss_Std.Text;
        EDEnt.PROGRAM_LEVEL = "SLC Information";
        EDEnt = (HSS_EDUCATION_DETAIL)EDSer.GetSingle(EDEnt);
        if (EDEnt != null)
        {
            if (ddlCountrySLC.SelectedValue == "1")
            {

                EDEnt.BOARD = txtBOARD.Text;
                EDEnt.INSTITUTION = txtSCHOOL.Text;
                EDEnt.PASSED_YEAR = txtPASSED_YEAR.Text;
                EDEnt.PERCENTAGE = txtPERCENTAGE.Text;
                EDEnt.DIVISION = txtDIVISION.Text;
                EDEnt.SYMBOLE_NO = txtSYMBOLE_NO.Text;
                EDEnt.OPT_SUBJ1 = txtOPT_SUBJ1.Text;
                EDEnt.OPT_SUBJ2 = txtOPT_SUBJ2.Text;
                EDEnt.OPT_SUBJ3 = txtOPT_SUBJ3.Text;
                EDEnt.PROGRAM_LEVEL = lblSchLevel.InnerText;

                if (ddlState_SLC.SelectedValue == "Select")
                {
                    EDEnt.STATE = "";
                }
                else
                {
                    EDEnt.STATE = ddlState_SLC.SelectedValue;
                }

                if (ddlZone_School.SelectedValue == "Select")
                {
                    EDEnt.ZONE = "";
                }
                else
                {
                    EDEnt.ZONE = ddlZone_School.SelectedValue;
                }

                if (ddlDistrict_School.SelectedValue == "Select")
                {
                    EDEnt.DISTRICT = "";
                }
                else
                {
                    EDEnt.DISTRICT = ddlDistrict_School.SelectedValue;
                }
                EDEnt.COUNTRY = ddlCountrySLC.SelectedValue;
                EDEnt.ADDRESS = txtcountryAddSLC.Text;

                EDSer.Update(EDEnt, DT);
            }
            else
            {

                EDEnt.BOARD = txtBOARD.Text;
                EDEnt.INSTITUTION = txtSCHOOL.Text;
                EDEnt.PASSED_YEAR = txtPASSED_YEAR.Text;
                EDEnt.PERCENTAGE = txtPERCENTAGE.Text;
                EDEnt.DIVISION = txtDIVISION.Text;
                EDEnt.SYMBOLE_NO = txtSYMBOLE_NO.Text;
                EDEnt.OPT_SUBJ1 = txtOPT_SUBJ1.Text;
                EDEnt.OPT_SUBJ2 = txtOPT_SUBJ2.Text;
                EDEnt.OPT_SUBJ3 = txtOPT_SUBJ3.Text;
                EDEnt.PROGRAM_LEVEL = lblSchLevel.InnerText;
                EDEnt.STATE = "";
                EDEnt.ZONE = "";
                EDEnt.DISTRICT = "";
                EDEnt.COUNTRY = ddlCountrySLC.SelectedValue;
                EDEnt.ADDRESS = txtcountryAddSLC.Text;

                EDSer.Update(EDEnt, DT);
            }
        }
        #endregion

        #region for +2

        EDEnt = new HSS_EDUCATION_DETAIL();
        EDEnt.STUDENT_PKID = lblpkid_hss_Std.Text;
        EDEnt.PROGRAM_LEVEL = "+2 Information";
        EDEnt = (HSS_EDUCATION_DETAIL)EDSer.GetSingle(EDEnt);
        if (EDEnt != null)
        {

            if (ddlCountryPT.SelectedValue == "1")
            {

                EDEnt.BOARD = txtPTBoard.Text;
                EDEnt.INSTITUTION = txtCOLLEGE.Text;
                EDEnt.PASSED_YEAR = txtPTPASSED_YEAR.Text;
                EDEnt.PERCENTAGE = txtPTPERCENTAGE.Text;
                EDEnt.DIVISION = txtPTDIVISION.Text;
                EDEnt.SYMBOLE_NO = txtPTSYMBOLE_NO.Text;
                EDEnt.OPT_SUBJ1 = txtPTOPT_SUBJ1.Text;
                EDEnt.OPT_SUBJ2 = txtPTOPT_SUBJ2.Text;
                EDEnt.OPT_SUBJ3 = txtPTOPT_SUBJ3.Text;
                EDEnt.FACULTY = ddlFacultyPT.SelectedValue;
                EDEnt.PROGRAM_LEVEL = lblPTLevel.InnerText;
                if (ddlState_PT.SelectedValue == "Select")
                {
                    EDEnt.STATE = "";
                }
                else
                {
                    EDEnt.STATE = ddlState_PT.SelectedValue;
                }

                if (ddlZone_PT.SelectedValue == "Select")
                {
                    EDEnt.ZONE = "";
                }
                else
                {

                    EDEnt.ZONE = ddlZone_PT.SelectedValue;
                }
                if (ddlDistrict_PT.SelectedValue == "Select")
                {
                    EDEnt.DISTRICT = "";
                }
                else
                {
                    EDEnt.DISTRICT = ddlDistrict_PT.SelectedValue;
                }
                EDEnt.COUNTRY = ddlCountryPT.SelectedValue;
                EDEnt.ADDRESS = txtCountryAddPT.Text;


                EDSer.Update(EDEnt, DT);
            }
            else
            {
                EDEnt.BOARD = txtPTBoard.Text;
                EDEnt.INSTITUTION = txtCOLLEGE.Text;
                EDEnt.PASSED_YEAR = txtPTPASSED_YEAR.Text;
                EDEnt.PERCENTAGE = txtPTPERCENTAGE.Text;
                EDEnt.DIVISION = txtPTDIVISION.Text;
                EDEnt.SYMBOLE_NO = txtPTSYMBOLE_NO.Text;
                EDEnt.OPT_SUBJ1 = txtPTOPT_SUBJ1.Text;
                EDEnt.OPT_SUBJ2 = txtPTOPT_SUBJ2.Text;
                EDEnt.OPT_SUBJ3 = txtPTOPT_SUBJ3.Text;
                EDEnt.FACULTY = ddlFacultyPT.SelectedValue;
                EDEnt.PROGRAM_LEVEL = lblPTLevel.InnerText;
                EDEnt.STATE = "";
                EDEnt.ZONE = "";
                EDEnt.DISTRICT = "";
                EDEnt.COUNTRY = ddlCountryPT.SelectedValue;
                EDEnt.ADDRESS = txtCountryAddPT.Text;

                EDSer.Update(EDEnt, DT);

            }
        }
        #endregion

        #region for bachelor

        EDEnt = new HSS_EDUCATION_DETAIL();
        EDEnt.STUDENT_PKID = lblpkid_hss_Std.Text;
        EDEnt.PROGRAM_LEVEL = "Bachelor Information";
        EDEnt = (HSS_EDUCATION_DETAIL)EDSer.GetSingle(EDEnt);
        if (EDEnt != null)
        {
            if (ddlCountryB.SelectedValue == "1")
            {
                EDEnt.BOARD = txtUniversity.Text;
                EDEnt.INSTITUTION = txtCollege_B.Text;
                EDEnt.PASSED_YEAR = txtPassedyear_B.Text;
                EDEnt.PERCENTAGE = txtPercentage_B.Text;
                EDEnt.DIVISION = txtDivision_B.Text;
                EDEnt.SYMBOLE_NO = txtSymbol_B.Text;
                EDEnt.OPT_SUBJ1 = txtSub1_B.Text;
                EDEnt.OPT_SUBJ2 = txtSub2_B.Text;
                EDEnt.OPT_SUBJ3 = txtSub3_B.Text;
                EDEnt.FACULTY = ddlFaculty_B.SelectedValue;
                EDEnt.PROGRAM_LEVEL = lblBLevel.InnerText;
                EDEnt.PROGRAM = txtBProgram.Text;

                if (ddlState_PT.SelectedValue == "Select")
                {
                    EDEnt.STATE = "";
                }
                else
                {
                    EDEnt.STATE = ddlState_PT.SelectedValue;
                }
                if (ddlZone_PT.SelectedValue == "Select")
                {
                    EDEnt.ZONE = "";
                }
                else
                {
                    EDEnt.ZONE = ddlZone_PT.SelectedValue;
                }

                if (ddlDistrict_PT.SelectedValue == "Select")
                {
                    EDEnt.DISTRICT = "";
                }
                else
                {
                    EDEnt.DISTRICT = ddlDistrict_PT.SelectedValue;
                }
                EDEnt.COUNTRY = ddlCountryB.SelectedValue;
                EDEnt.ADDRESS = txtCountryAddB.Text;

                EDSer.Update(EDEnt, DT);

            }
            else
            {
                EDEnt.BOARD = txtUniversity.Text;
                EDEnt.INSTITUTION = txtCollege_B.Text;
                EDEnt.PASSED_YEAR = txtPassedyear_B.Text;
                EDEnt.PERCENTAGE = txtPercentage_B.Text;
                EDEnt.DIVISION = txtDivision_B.Text;
                EDEnt.SYMBOLE_NO = txtSymbol_B.Text;
                EDEnt.OPT_SUBJ1 = txtSub1_B.Text;
                EDEnt.OPT_SUBJ2 = txtSub2_B.Text;
                EDEnt.OPT_SUBJ3 = txtSub3_B.Text;
                EDEnt.FACULTY = ddlFaculty_B.SelectedValue;
                EDEnt.PROGRAM_LEVEL = lblBLevel.InnerText;
                EDEnt.PROGRAM = txtBProgram.Text;
                EDEnt.STATE = "";
                EDEnt.ZONE = "";
                EDEnt.DISTRICT = "";
                EDEnt.COUNTRY = ddlCountryB.SelectedValue;
                EDEnt.ADDRESS = txtCountryAddB.Text;

                EDSer.Update(EDEnt, DT);
            }
        }
        #endregion

        #endregion

        #region information_alert

        INAEnt = new information_alert();
        INAEnt.STUDENT_PKID = lblpkid_hss_Std.Text;
        INAEnt = (information_alert)INASer.GetSingle(INAEnt);
        if (INAEnt != null)
        {
            INAEnt.SMS_ALERT1 = txtSMSAlert1.Text;
            INAEnt.SMS_ALERT2 = txtSMSAlert2.Text;
            INAEnt.EMAIL_ALERT1 = txtEmailAlert1.Text;
            INAEnt.EMAIL_ALERT2 = txtEmailAlert2.Text;

            if (chkSMSAlert1Status.Checked)
            {
                INAEnt.SMS_ALERT1_STATUS = "1";
            }
            else
            {
                INAEnt.SMS_ALERT1_STATUS = "0";
            }

            if (chkSMSAlert2Status.Checked)
            {
                INAEnt.SMS_ALERT2_STATUS = "1";
            }
            else
            {
                INAEnt.SMS_ALERT2_STATUS = "0";
            }

            if (chkEmailAlert1Status.Checked)
            {
                INAEnt.EMAIL_ALERT1_STATUS = "1";
            }
            else
            {
                INAEnt.EMAIL_ALERT1_STATUS = "0";
            }

            if (chkEmailAlert2Status.Checked)
            {
                INAEnt.EMAIL_ALERT2_STATUS = "1";
            }
            else
            {
                INAEnt.EMAIL_ALERT2_STATUS = "0";
            }

            INASer.Update(INAEnt, DT);

        }
        else
        {
            INAEnt = new information_alert();

            //INAEnt.STUDENT_PKID = STUDENT_ID;

            INAEnt.SMS_ALERT1 = txtSMSAlert1.Text;
            INAEnt.SMS_ALERT2 = txtSMSAlert2.Text;
            INAEnt.EMAIL_ALERT1 = txtEmailAlert1.Text;
            INAEnt.EMAIL_ALERT2 = txtEmailAlert2.Text;

            if (chkSMSAlert1Status.Checked)
            {
                INAEnt.SMS_ALERT1_STATUS = "1";
            }
            else
            {
                INAEnt.SMS_ALERT1_STATUS = "0";
            }

            if (chkSMSAlert2Status.Checked)
            {
                INAEnt.SMS_ALERT2_STATUS = "1";
            }
            else
            {
                INAEnt.SMS_ALERT2_STATUS = "0";
            }

            if (chkEmailAlert1Status.Checked)
            {
                INAEnt.EMAIL_ALERT1_STATUS = "1";
            }
            else
            {
                INAEnt.EMAIL_ALERT1_STATUS = "0";
            }

            if (chkEmailAlert2Status.Checked)
            {
                INAEnt.EMAIL_ALERT2_STATUS = "1";
            }
            else
            {
                INAEnt.EMAIL_ALERT2_STATUS = "0";
            }

            INASer.Insert(INAEnt, DT);

        }

        #endregion

        #region HSS_ATTACHMENTS

        AEnt = new HSS_ATTACHMENTS();
        AEnt.STUDENT_PKID = lblpkid_hss_Std.Text;
        AEnt = (HSS_ATTACHMENTS)ASer.GetSingle(AEnt);
        if (AEnt != null)
        {
            AEnt.PHOTO = chkPHOTO.Checked.ToString();
            AEnt.TRANSCRIPT = chkTRANSCRIPT.Checked.ToString();
            AEnt.CERTIFICATE = chkCERTIFICATE.Checked.ToString();
            AEnt.PT_TRANSCRIPT = chkPTTRANSCRIPT.Checked.ToString();
            AEnt.PT_CHARACTER_CERTIFICATE = chkPTCHARACTER_CERTIFICATE.Checked.ToString();
            AEnt.CHARACTER_CERTIFICATE = chkCHARACTER_CERTIFICATE.Checked.ToString();
            AEnt.CITIZENSHIP = chkCITIZENSHIP.Checked.ToString();
            AEnt.PT_PROVISIONAL_CERTIFICATE = chkPROVISIONCERTIFICATE.Checked.ToString();
            AEnt.PT_MIGRATION_CERTIFICATE = chkMIGRATIONCERTIFICATE.Checked.ToString();
            AEnt.B_TRANSCRIPT = chkBTranscript.Checked.ToString();
            AEnt.B_CHARACTER_CERTIFICATE = chkBCharacterCertificate.Checked.ToString();
            AEnt.B_MIGRATION = chkBMigrationCertificate.Checked.ToString();

            ASer.Update(AEnt, DT);
        }

        #endregion

        #region ADMISSION_DETAIL

        ADEnt = new ADMISSION_DETAIL();
        ADEnt.STUDENT_PKID = SEnt.PK_ID;
        ADEnt = (ADMISSION_DETAIL)ADSrv.GetSingle(ADEnt);
        if (ADEnt != null)
        {

            ADEnt.ENTRANCE_ROLLNO = txtEntranceRollNo.Text;
            ADEnt.ENTRANCE_DATE = txtEntranceDate.Text;
            ADEnt.OBTAINED_MARKS = txtMarksObtained.Text;
            ADEnt.INTERVIEW_DATE = txtInterviewDate.Text;
            ADEnt.INTERVIEW_BY = txtInterviewBy.Text;
            ADEnt.INTERVIEW_MARKS = txtInterviewResult.Text;
            ADEnt.ADMISSION_NO = txtAdmissionNo.Text;
            ADEnt.ADMISSION_DATE = txtAdmissionDate.Text;
            ADEnt.FORM_FILLED_BY = txtFilledBy.Text;
            ADEnt.FORM_FILLED_DATE = txtFilledDate.Text;
            ADEnt.VERIFIED_BY = txtVerifiedBy.Text;
            ADEnt.VERIFIED_DATE = txtVerifiedDate.Text;
            ADEnt.REMARKS = txtRemarks.Text;

            ADSrv.Update(ADEnt, DT);

        }

        #endregion

        #region Father's info

        //if (!String.IsNullOrEmpty(txtF_NAME.Text))
        //{

        FREnt = new FAMILY_RELATIONS();
        FREnt.RELATION_OF_ID = SEnt.PK_ID;
        FREnt.RELATION = "Father";
        FREnt.RELATION_OF = "Student";
        FREnt = (FAMILY_RELATIONS)FRSrv.GetSingle(FREnt);
        if (FREnt != null)
        {
            FREnt.R_NAME = txtF_NAME.Text;
            FREnt.OCCUPATION = txtF_OCCUPATION.Text;
            FREnt.ADDRESS = txtF_ADDRESS.Text;
            FREnt.PHONE = txtF_PHONE.Text;
            FREnt.MOBILE1 = txtF_MOBILE1.Text;
            FREnt.MOBILE2 = txtF_MOBILE2.Text;
            FREnt.EMAIL = txtF_EMAIL.Text;

            FREnt.DESIGNATION = txtF_Position.Text;
            FREnt.NAME_OF_COMPANY = txtF_Office.Text;
            FREnt.OFFICE_PHONE_NO = txtF_Office_Phone.Text;
            //FREnt.IS_GUARDIAN = "FALSE";

            if (ddlGuardian.SelectedValue == "Father")
            {
                FREnt.IS_GUARDIAN = "TRUE";
            }
            else
            {
                FREnt.IS_GUARDIAN = "FALSE";
            }

            FRSrv.Update(FREnt, DT);
        }
        //}

        #endregion

        #region Mother's info
        if (!String.IsNullOrEmpty(txtM_NAME.Text))
        {
            FREnt = new FAMILY_RELATIONS();
            FREnt.RELATION_OF_ID = SEnt.PK_ID;
            FREnt.RELATION = "Mother";
            FREnt.RELATION_OF = "Student";
            FREnt = (FAMILY_RELATIONS)FRSrv.GetSingle(FREnt);
            if (FREnt != null)
            {

                FREnt.R_NAME = txtM_NAME.Text;
                FREnt.OCCUPATION = txtM_OCCUPATION.Text;
                FREnt.ADDRESS = txtM_ADDRESS.Text;
                FREnt.PHONE = txtM_PHONE.Text;
                FREnt.MOBILE1 = txtM_MOBILE1.Text;
                FREnt.MOBILE2 = txtM_MOBILE2.Text;
                FREnt.EMAIL = txtM_EMAIL.Text;

                FREnt.DESIGNATION = txtM_POSITION.Text;
                FREnt.NAME_OF_COMPANY = txtM_OFFICE.Text;
                FREnt.OFFICE_PHONE_NO = txtM_OFFICE_PHONE.Text;
                //FREnt.IS_GUARDIAN = "FALSE";

                if (ddlGuardian.SelectedValue == "Mother")
                {
                    FREnt.IS_GUARDIAN = "TRUE";
                }
                else
                {
                    FREnt.IS_GUARDIAN = "FALSE";
                }

                FRSrv.Update(FREnt, DT);
            }
        }
        #endregion

        #region Spouse's info
        if (rbtnMaritalStatus.SelectedValue == "Married" && !String.IsNullOrEmpty(txtS_NAME.Text))
        {

            FREnt = new FAMILY_RELATIONS();
            FREnt.RELATION_OF_ID = SEnt.PK_ID;
            FREnt.RELATION = "Spouse";
            FREnt.RELATION_OF = "Student";
            FREnt = (FAMILY_RELATIONS)FRSrv.GetSingle(FREnt);
            if (FREnt != null)
            {
                FREnt.RELATION = "Spouse";
                FREnt.RELATION_OF = "Student";
                FREnt.R_NAME = txtS_NAME.Text;
                FREnt.OCCUPATION = txtS_OCCUPATION.Text;
                FREnt.ADDRESS = txtS_ADDRESS.Text;
                FREnt.PHONE = txtS_PHONE.Text;
                FREnt.MOBILE1 = txtS_MOBILE1.Text;
                FREnt.MOBILE2 = txtS_MOBILE2.Text;
                FREnt.EMAIL = txtS_EMAIL.Text;

                FREnt.DESIGNATION = txtS_POSITION.Text;
                FREnt.NAME_OF_COMPANY = txtS_OFFICE.Text;
                FREnt.OFFICE_PHONE_NO = txtS_OFFICE_PHONE.Text;
                //FREnt.IS_GUARDIAN = "FALSE";

                if (ddlGuardian.SelectedValue == "Spouse")
                {
                    FREnt.IS_GUARDIAN = "TRUE";
                }
                else
                {
                    FREnt.IS_GUARDIAN = "FALSE";
                }


                FRSrv.Update(FREnt, DT);
            }
            else
            {

                FREnt = new FAMILY_RELATIONS();

                FREnt.R_NAME = txtS_NAME.Text;
                FREnt.OCCUPATION = txtS_OCCUPATION.Text;
                FREnt.ADDRESS = txtS_ADDRESS.Text;
                FREnt.PHONE = txtS_PHONE.Text;
                FREnt.MOBILE1 = txtS_MOBILE1.Text;
                FREnt.MOBILE2 = txtS_MOBILE2.Text;
                FREnt.EMAIL = txtS_EMAIL.Text;
                FREnt.RELATION_OF_ID = SEnt.PK_ID;
                FREnt.DESIGNATION = txtS_POSITION.Text;
                FREnt.NAME_OF_COMPANY = txtS_OFFICE.Text;
                FREnt.OFFICE_PHONE_NO = txtS_OFFICE_PHONE.Text;
                FREnt.IS_GUARDIAN = "FALSE";


                FRSrv.Insert(FREnt, DT);

            }
        }


        #endregion

        #region Guardian's info

        theList = new EntityList();

        FREnt = new FAMILY_RELATIONS();
        FREnt.RELATION_OF_ID = SEnt.PK_ID;
        theList = FRSrv.GetAll(FREnt);
        if (theList.Count == 3)
        {
            if (ddlGuardian.SelectedValue == "Select")
            {

                #region update info
                FREnt = new FAMILY_RELATIONS();
                FREnt.RELATION_OF_ID = SEnt.PK_ID;
                FREnt.IS_GUARDIAN = "TRUE";
                FREnt = (FAMILY_RELATIONS)FRSrv.GetSingle(FREnt, DT);
                if (FREnt != null)
                {
                    FREnt.R_NAME = txtG_NAME.Text;
                    FREnt.OCCUPATION = txtG_OCCUPATION.Text;
                    FREnt.ADDRESS = txtG_ADDRESS.Text;
                    FREnt.PHONE = txtG_PHONE.Text;
                    FREnt.MOBILE1 = txtG_MOBILE1.Text;
                    FREnt.MOBILE2 = txtG_MOBILE2.Text;
                    FREnt.EMAIL = txtG_EMAIL.Text;
                    FREnt.RELATION = txtG_RELATION.Text;
                    FREnt.DESIGNATION = txtG_POSITION.Text;
                    FREnt.NAME_OF_COMPANY = txtG_OFFICE.Text;
                    FREnt.OFFICE_PHONE_NO = txtG_OFFICE_PHONE.Text;

                    FRSrv.Update(FREnt, DT);
                }
                #endregion
            }
            else
            {

                FREnt = new FAMILY_RELATIONS();
                FREnt.RELATION_OF_ID = SEnt.PK_ID;
                FREnt.IS_GUARDIAN = "TRUE";
                FREnt = (FAMILY_RELATIONS)FRSrv.GetSingle(FREnt, DT);
                if (FREnt != null)
                {
                    FRSrv.Delete(FREnt, DT);
                }

                #region update info
                FREnt = new FAMILY_RELATIONS();
                FREnt.RELATION_OF_ID = SEnt.PK_ID;
                FREnt.RELATION = ddlGuardian.SelectedValue;
                FREnt = (FAMILY_RELATIONS)FRSrv.GetSingle(FREnt, DT);
                if (FREnt != null)
                {
                    FREnt.IS_GUARDIAN = "TRUE";

                    FRSrv.Update(FREnt, DT);
                }

                #endregion
            }

        }
        else
        {
            #region insert info
            FREnt = new FAMILY_RELATIONS();
            FREnt.R_NAME = txtG_NAME.Text;
            FREnt.OCCUPATION = txtG_OCCUPATION.Text;
            FREnt.ADDRESS = txtG_ADDRESS.Text;
            FREnt.PHONE = txtG_PHONE.Text;
            FREnt.MOBILE1 = txtG_MOBILE1.Text;
            FREnt.MOBILE2 = txtG_MOBILE2.Text;
            FREnt.EMAIL = txtG_EMAIL.Text;
            FREnt.RELATION = txtG_RELATION.Text;
            FREnt.RELATION_OF = "Student";
            FREnt.RELATION_OF_ID = pkId;
            FREnt.DESIGNATION = txtG_POSITION.Text;
            FREnt.NAME_OF_COMPANY = txtG_OFFICE.Text;
            FREnt.OFFICE_PHONE_NO = txtG_OFFICE_PHONE.Text;
            FREnt.IS_GUARDIAN = "TRUE";

            FRSrv.Insert(FREnt, DT);
            #endregion
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

        #region
        if (ddlContactAddress.SelectedValue != "Select") // Both Permanant and Contact is same
        {

            #region both ADDRESS is same when updating new data and old data is different

            theList = new EntityList();
            AddEnt = new ADDRESS();
            AddEnt.ADDRESS_OF_ID = SEnt.PK_ID;
            theList = AddSrv.GetAll(AddEnt);
            if (theList.Count > 1)
            {
                AddEnt = new ADDRESS();
                AddEnt.ADDRESS_OF_ID = SEnt.PK_ID;
                AddEnt.ADDRESS_TYPE = "C";
                AddEnt = (ADDRESS)AddSrv.GetSingle(AddEnt, DT);
                if (AddEnt != null)
                {
                    AddSrv.Delete(AddEnt, DT);
                }

                AddEnt = new ADDRESS();
                AddEnt.ADDRESS_OF_ID = SEnt.PK_ID;
                AddEnt.ADDRESS_TYPE = "P";
                AddEnt = (ADDRESS)AddSrv.GetSingle(AddEnt, DT);
                if (AddEnt != null)
                {

                    AddEnt.COUNTRY = ddlCountryPA.SelectedValue;
                    if (stateP == "Select")
                    {
                        AddEnt.STATE = "";
                    }
                    else
                    {
                        AddEnt.STATE = stateP;
                    }

                    if (zoneP == "Select")
                    {
                        AddEnt.ZONE = "";
                    }
                    else
                    {
                        AddEnt.ZONE = zoneP;
                    }

                    if (districtP == "Select")
                    {
                        AddEnt.DISTRICT = "";
                    }
                    else
                    {
                        AddEnt.DISTRICT = districtP;
                    }

                    AddEnt.VDC_MUNICIPALITY = txtPA_VDC_MUNI.Text;
                    AddEnt.WARD_NO = txtPA_WARD_NO.Text;
                    AddEnt.STREET_NAME = txtPA_STREET.Text;
                    AddEnt.HOUSE_NO = txtPA_HOUSE_NO.Text;
                    AddEnt.ADDRESS_OF = "Student";
                    AddEnt.ADDRESS_TYPE = "Both";
                    AddSrv.Update(AddEnt, DT);
                }
            }

            else      // both ADDRESS is same when updateing new data and old data is also same
            {
                AddEnt = new ADDRESS();
                AddEnt.ADDRESS_OF_ID = SEnt.PK_ID;
                AddEnt = (ADDRESS)AddSrv.GetSingle(AddEnt, DT);
                if (AddEnt != null)
                {
                    AddEnt.COUNTRY = ddlCountryPA.SelectedValue;
                    if (stateP == "Select")
                    {
                        AddEnt.STATE = "";
                    }
                    else
                    {
                        AddEnt.STATE = stateP;
                    }

                    if (zoneP == "Select")
                    {
                        AddEnt.ZONE = "";
                    }
                    else
                    {
                        AddEnt.ZONE = zoneP;
                    }

                    if (districtP == "Select")
                    {
                        AddEnt.DISTRICT = "";
                    }
                    else
                    {
                        AddEnt.DISTRICT = districtP;
                    }
                    AddEnt.VDC_MUNICIPALITY = txtPA_VDC_MUNI.Text;
                    AddEnt.WARD_NO = txtPA_WARD_NO.Text;
                    AddEnt.STREET_NAME = txtPA_STREET.Text;
                    AddEnt.HOUSE_NO = txtPA_HOUSE_NO.Text;
                    AddEnt.ADDRESS_OF = "Student";
                    AddEnt.ADDRESS_TYPE = "Both";
                    AddSrv.Update(AddEnt, DT);
                }
            }

            #endregion
        }
        else  //  Permanant address is differetn from Contact address
        {

            theList = new EntityList();
            AddEnt = new ADDRESS();
            AddEnt.ADDRESS_OF_ID = SEnt.PK_ID;
            theList = AddSrv.GetAll(AddEnt);
            if (theList.Count == 1)
            {
                #region if both address in old data is same
                AddEnt = new ADDRESS();
                AddEnt.ADDRESS_OF_ID = SEnt.PK_ID;
                AddEnt = (ADDRESS)AddSrv.GetSingle(AddEnt, DT);
                if (AddEnt != null)
                {
                    #region for permanent address

                    AddEnt.COUNTRY = ddlCountryPA.SelectedValue;
                    if (stateP == "Select")
                    {
                        AddEnt.STATE = "";
                    }
                    else
                    {
                        AddEnt.STATE = stateP;
                    }

                    if (zoneP == "Select")
                    {
                        AddEnt.ZONE = "";
                    }
                    else
                    {
                        AddEnt.ZONE = zoneP;
                    }

                    if (districtP == "Select")
                    {
                        AddEnt.DISTRICT = "";
                    }
                    else
                    {
                        AddEnt.DISTRICT = districtP;
                    }
                    AddEnt.VDC_MUNICIPALITY = txtPA_VDC_MUNI.Text;
                    AddEnt.WARD_NO = txtPA_WARD_NO.Text;
                    AddEnt.STREET_NAME = txtPA_STREET.Text;
                    AddEnt.HOUSE_NO = txtPA_HOUSE_NO.Text;
                    AddEnt.ADDRESS_OF = "Student";
                    AddEnt.ADDRESS_TYPE = "P";

                    AddSrv.Update(AddEnt, DT);
                    #endregion

                }

                #region for Contact Address
                AddEnt = new ADDRESS();
                AddEnt.COUNTRY = ddlCountry_CA.SelectedValue;

                if (stateC == "Select")
                {
                    AddEnt.STATE = "";
                }
                else
                {
                    AddEnt.STATE = stateC;
                }

                if (zoneC == "Select")
                {
                    AddEnt.ZONE = "";
                }
                else
                {
                    AddEnt.ZONE = zoneC;
                }

                if (districtC == "Select")
                {
                    AddEnt.DISTRICT = "";
                }
                else
                {
                    AddEnt.DISTRICT = districtC;
                }

                AddEnt.VDC_MUNICIPALITY = txtTA_VDC_MUNI.Text;
                AddEnt.WARD_NO = txtTA_WARD_NO.Text;
                AddEnt.STREET_NAME = txtTA_STREET.Text;
                AddEnt.HOUSE_NO = txtTA_HOUSE_NO.Text;
                AddEnt.ADDRESS_OF = "Student";
                AddEnt.ADDRESS_OF_ID = SEnt.PK_ID;
                AddEnt.ADDRESS_TYPE = "C";

                AddSrv.Insert(AddEnt, DT);
                #endregion
                #endregion
            }
            else
            {
                #region if both data is different
                AddEnt = new ADDRESS();
                AddEnt.ADDRESS_OF_ID = SEnt.PK_ID;
                AddEnt.ADDRESS_TYPE = "P";
                AddEnt = (ADDRESS)AddSrv.GetSingle(AddEnt, DT);
                if (AddEnt != null)
                {
                    #region for permananta address
                    AddEnt.COUNTRY = ddlCountryPA.SelectedValue;
                    if (stateP == "Select")
                    {
                        AddEnt.STATE = "";
                    }
                    else
                    {
                        AddEnt.STATE = stateP;
                    }

                    if (zoneP == "Select")
                    {
                        AddEnt.ZONE = "";
                    }
                    else
                    {
                        AddEnt.ZONE = zoneP;
                    }

                    if (districtP == "Select")
                    {
                        AddEnt.DISTRICT = "";
                    }
                    else
                    {
                        AddEnt.DISTRICT = districtP;
                    }
                    AddEnt.VDC_MUNICIPALITY = txtPA_VDC_MUNI.Text;
                    AddEnt.WARD_NO = txtPA_WARD_NO.Text;
                    AddEnt.STREET_NAME = txtPA_STREET.Text;
                    AddEnt.HOUSE_NO = txtPA_HOUSE_NO.Text;
                    AddEnt.ADDRESS_OF = "Student";
                    AddSrv.Update(AddEnt, DT);
                    #endregion

                }

                #region for Contact Address
                AddEnt = new ADDRESS();
                AddEnt.ADDRESS_OF_ID = SEnt.PK_ID;
                AddEnt.ADDRESS_TYPE = "C";
                AddEnt = (ADDRESS)AddSrv.GetSingle(AddEnt, DT);
                if (AddEnt != null)
                {
                    AddEnt.COUNTRY = ddlCountry_CA.SelectedValue;
                    if (stateC == "Select")
                    {
                        AddEnt.STATE = "";
                    }
                    else
                    {
                        AddEnt.STATE = stateC;
                    }

                    if (zoneC == "Select")
                    {
                        AddEnt.ZONE = "";
                    }
                    else
                    {
                        AddEnt.ZONE = zoneC;
                    }

                    if (districtC == "Select")
                    {
                        AddEnt.DISTRICT = "";
                    }
                    else
                    {
                        AddEnt.DISTRICT = districtC;
                    }
                    AddEnt.VDC_MUNICIPALITY = txtTA_VDC_MUNI.Text;
                    AddEnt.WARD_NO = txtTA_WARD_NO.Text;
                    AddEnt.STREET_NAME = txtTA_STREET.Text;
                    AddEnt.HOUSE_NO = txtTA_HOUSE_NO.Text;
                    AddEnt.ADDRESS_OF = "Student";
                    AddEnt.ADDRESS_OF_ID = SEnt.PK_ID;

                    AddSrv.Update(AddEnt, DT);
                }
                #endregion
                #endregion
            }

        }
        #endregion
        #endregion

        if (DT.HAPPY == true)
        {
            DT.Commit();
            HelperFunction.MsgBox(this, this.GetType(), "Data Updated Succesfully");
            LoadCollegeCode();
            //clearFields();
            Response.Redirect("~/administration/reports/studentlist_ungenerated.aspx?program=" + ddlProgram.SelectedValue + "?" + ddlBatch.SelectedValue);

        }
        else
        {
            DT.Abort();
            HelperFunction.MsgBox(this, this.GetType(), "Data Not Updated. There is some problem");
        }
    }
    protected void ddlPA_Zone_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadPADistrict();
    }
    protected void ddlTA_Zone_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadTADistrict();
    }
    protected void btnCalcNeptoEng_Click(object sender, EventArgs e)
    {
        if (txtDOB_BSDay.Text != "" && txtDOB_BSMth.Text != "" && txtDOB_BSYear.Text != "")
        {
            string engdate = hf.ConvertNepaliTOEnglishDate(txtDOB_BSDay.Text, txtDOB_BSMth.Text, txtDOB_BSYear.Text);
            string[] englishdate = engdate.Split('/');
            txtDOB_ADDay.Text = englishdate[0];
            txtDOB_ADMth.Text = englishdate[1];
            txtDOB_ADYear.Text = englishdate[2];

        }
    }
    protected void btnCalcEngtoNep_Click(object sender, EventArgs e)
    {

        if (txtDOB_ADDay.Text != "" && txtDOB_ADMth.Text != "" && txtDOB_ADYear.Text != "")
        {
            string[] nepalidate = hf.ConvertEnglishToNepali(txtDOB_ADMth.Text + "/" + txtDOB_ADDay.Text + "/" + txtDOB_ADYear.Text);
            txtDOB_BSDay.Text = Convert.ToDouble(nepalidate[0]).ToString("00");
            txtDOB_BSMth.Text = Convert.ToDouble(nepalidate[1]).ToString("00");
            txtDOB_BSYear.Text = nepalidate[2];
        }

    }
    protected void rbtnMaritalStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadMarital_Status();
    }
    protected void loadMarital_Status()
    {
        if (rbtnMaritalStatus.SelectedValue == "Married")
        {
            spouse_fieldset.Visible = true;
            ddlGuardian.Items.Insert(3, "Spouse");
        }
        else
        {
            spouse_fieldset.Visible = false;
            ddlGuardian.Items.Remove("Spouse");

            txtS_ADDRESS.Text = "";
            txtS_EMAIL.Text = "";
            txtS_MOBILE1.Text = "";
            txtS_MOBILE2.Text = "";
            txtS_NAME.Text = "";
            txtS_OCCUPATION.Text = "";
            txtS_PHONE.Text = "";
        }
    }
    protected void rbtnGENDER_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlZone_PT_SelectedIndexChanged(object sender, EventArgs e)
    {
        PlusTwo_Zone();

    }
    protected void PlusTwo_Zone()
    {
        DEnt = new District();
        DEnt.ZONE_ID = ddlZone_PT.SelectedValue;
        ddlDistrict_PT.DataSource = DSer.GetAll(DEnt);
        ddlDistrict_PT.DataTextField = "DISTRICTNAME";
        ddlDistrict_PT.DataValueField = "ID";
        ddlDistrict_PT.DataBind();
        ddlDistrict_PT.Items.Insert(0, "Select");
    }
    protected void ddlZone_School_SelectedIndexChanged(object sender, EventArgs e)
    {
        School_Zone();
    }
    protected void School_Zone()
    {
        DEnt = new District();
        DEnt.ZONE_ID = ddlZone_School.SelectedValue;
        ddlDistrict_School.DataSource = DSer.GetAll(DEnt);
        ddlDistrict_School.DataTextField = "DISTRICTNAME";
        ddlDistrict_School.DataValueField = "ID";
        ddlDistrict_School.DataBind();
        ddlDistrict_School.Items.Insert(0, "Select");
    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFaculty.SelectedValue != "Select")
        {
            LoadProgram();
            LoadBatch();
        }
        else
        {
            ddlProgram.Items.Clear();
            ddlProgram.Items.Insert(0, "Select");

            ddlBatch.Items.Clear();
            ddlBatch.Items.Insert(0, "Select");
        }

        if (ddlProgram.SelectedValue == "Select")
        {
            ddlBatch.Items.Clear();
            ddlBatch.Items.Insert(0, "Select");
        }
        LoadCollegeCode();
    }
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBatch();
        LoadCollegeCode();
    }
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlBatch.SelectedValue != "Select")
        {
            LoadSemester();
        }

    }
    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
        LoadBatch();
        if (ddlLevel.SelectedValue == "Master")
        {
            divBachelorInfo.Visible = true;
            divBachelorDocument.Visible = true;
        }
        else
        {
            divBachelorInfo.Visible = false;
            divBachelorDocument.Visible = false;
        }
    }
    protected void ddlZone_B_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBDistrict();

    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountrySLC.SelectedValue == "1")
        {
            lblCountryAdd.Visible = false;
            lblStateSLC.Visible = true;
            lblZoneSLC.Visible = true;
            lblDistrictSLC.Visible = true;
            ddlState_SLC.Focus();

        }
        else
        {
            txtcountryAddSLC.Focus();
            lblCountryAdd.Visible = true;
            lblStateSLC.Visible = false;
            lblZoneSLC.Visible = false;
            lblDistrictSLC.Visible = false;
        }
    }
    protected void ddlCountryPT_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountryPT.SelectedValue == "1")
        {
            lblCountryAddPT.Visible = false;
            lblStatePT.Visible = true;
            lblZonePT.Visible = true;
            lblDistrictPT.Visible = true;
        }
        else
        {
            lblCountryAddPT.Visible = true;
            lblStatePT.Visible = false;
            lblZonePT.Visible = false;
            lblDistrictPT.Visible = false;
        }
    }
    protected void ddlCountryB_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountryB.SelectedValue == "1")
        {
            lblCountryAddB.Visible = false;
            lblStateB.Visible = true;
            lblZoneB.Visible = true;
            lblDistrictB.Visible = true;
        }
        else
        {
            lblCountryAddB.Visible = true;
            lblStateB.Visible = false;
            lblZoneB.Visible = false;
            lblDistrictB.Visible = false;


        }
    }
    protected void ddlGuardian_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlGuardian.SelectedValue == "Father")
        {
            txtG_NAME.Text = txtF_NAME.Text;
            txtG_RELATION.Text = ddlGuardian.SelectedValue;
            txtG_ADDRESS.Text = txtF_ADDRESS.Text;
            txtG_PHONE.Text = txtF_PHONE.Text;
            txtG_MOBILE1.Text = txtF_MOBILE1.Text;
            txtG_MOBILE2.Text = txtF_MOBILE2.Text;
            txtG_EMAIL.Text = txtF_EMAIL.Text;
            txtG_OCCUPATION.Text = txtF_OCCUPATION.Text;
            txtG_POSITION.Text = txtF_Position.Text;
            txtG_OFFICE.Text = txtF_Office.Text;
            txtG_OFFICE_PHONE.Text = txtF_Office_Phone.Text;

            //ddlGuardian.Enabled = false;
            txtG_NAME.Enabled = false;
            txtG_RELATION.Enabled = false;
            txtG_ADDRESS.Enabled = false;
            txtG_PHONE.Enabled = false;
            txtG_MOBILE1.Enabled = false;
            txtG_MOBILE2.Enabled = false;
            txtG_EMAIL.Enabled = false;
            txtG_OCCUPATION.Enabled = false;
            txtG_POSITION.Enabled = false;
            txtG_OFFICE.Enabled = false;
            txtG_OFFICE_PHONE.Enabled = false;

        }

        else if (ddlGuardian.SelectedValue == "Mother")
        {
            txtG_NAME.Text = txtM_NAME.Text;
            txtG_RELATION.Text = ddlGuardian.SelectedValue;
            txtG_ADDRESS.Text = txtM_ADDRESS.Text;
            txtG_PHONE.Text = txtM_PHONE.Text;
            txtG_MOBILE1.Text = txtM_MOBILE1.Text;
            txtG_MOBILE2.Text = txtM_MOBILE2.Text;
            txtG_EMAIL.Text = txtM_EMAIL.Text;
            txtG_OCCUPATION.Text = txtM_OCCUPATION.Text;
            txtG_POSITION.Text = txtM_POSITION.Text;
            txtG_OFFICE.Text = txtM_OFFICE.Text;
            txtG_OFFICE_PHONE.Text = txtM_OFFICE_PHONE.Text;

            txtG_NAME.Enabled = false;
            txtG_RELATION.Enabled = false;
            txtG_ADDRESS.Enabled = false;
            txtG_PHONE.Enabled = false;
            txtG_MOBILE1.Enabled = false;
            txtG_MOBILE2.Enabled = false;
            txtG_EMAIL.Enabled = false;
            txtG_OCCUPATION.Enabled = false;
            txtG_POSITION.Enabled = false;
            txtG_OFFICE.Enabled = false;
            txtG_OFFICE_PHONE.Enabled = false;

        }

        else if (ddlGuardian.SelectedValue == "Spouse")
        {
            txtG_NAME.Text = txtS_NAME.Text;
            txtG_RELATION.Text = ddlGuardian.SelectedValue;
            txtG_ADDRESS.Text = txtS_ADDRESS.Text;
            txtG_PHONE.Text = txtS_PHONE.Text;
            txtG_MOBILE1.Text = txtS_MOBILE1.Text;
            txtG_MOBILE2.Text = txtS_MOBILE2.Text;
            txtG_EMAIL.Text = txtS_EMAIL.Text;
            txtG_OCCUPATION.Text = txtS_OCCUPATION.Text;
            txtG_POSITION.Text = txtS_POSITION.Text;
            txtG_OFFICE.Text = txtS_OFFICE.Text;
            txtG_OFFICE_PHONE.Text = txtS_OFFICE_PHONE.Text;

            //ddlGuardian.Enabled = false;
            txtG_NAME.Enabled = false;
            txtG_RELATION.Enabled = false;
            txtG_ADDRESS.Enabled = false;
            txtG_PHONE.Enabled = false;
            txtG_MOBILE1.Enabled = false;
            txtG_MOBILE2.Enabled = false;
            txtG_EMAIL.Enabled = false;
            txtG_OCCUPATION.Enabled = false;
            txtG_POSITION.Enabled = false;
            txtG_OFFICE.Enabled = false;
            txtG_OFFICE_PHONE.Enabled = false;

        }
        else
        {
            ddlGuardian.SelectedIndex = 0;
            txtG_NAME.Text = "";
            txtG_RELATION.Text = "";
            txtG_ADDRESS.Text = "";
            txtG_PHONE.Text = "";
            txtG_MOBILE1.Text = "";
            txtG_MOBILE2.Text = "";
            txtG_EMAIL.Text = "";
            txtG_OCCUPATION.Text = "";
            txtG_POSITION.Text = "";
            txtG_OFFICE.Text = "";
            txtG_OFFICE_PHONE.Text = "";


            txtG_NAME.Enabled = true;
            txtG_RELATION.Enabled = true;
            txtG_ADDRESS.Enabled = true;
            txtG_PHONE.Enabled = true;
            txtG_MOBILE1.Enabled = true;
            txtG_MOBILE2.Enabled = true;
            txtG_EMAIL.Enabled = true;
            txtG_OCCUPATION.Enabled = true;
            txtG_POSITION.Enabled = true;
            txtG_OFFICE.Enabled = true;
            txtG_OFFICE_PHONE.Enabled = true;

        }

    }
    protected void ddlCountryPA_SelectedIndexChanged(object sender, EventArgs e)
    {
        Country_PA();
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

                ddlCountry_CA.Enabled = true;
                ddlState_CA.Enabled = true;
                ddlTA_Zone.Enabled = true;
                ddlTA_DISTRICT.Enabled = true;
                txtTA_VDC_MUNI.Enabled = true;
                txtTA_WARD_NO.Enabled = true;
                txtTA_STREET.Enabled = true;
                txtTA_HOUSE_NO.Enabled = true;

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
    protected void txtDOB_ADDay_TextChanged(object sender, EventArgs e)
    {
        int day = Convert.ToInt32(txtDOB_ADDay.Text);

        if (day >= 32)
        {
            HelperFunction.MsgBox(this, this.GetType(), "Invalid day");
            txtDOB_ADDay.Focus();
        }
        else
        {
            txtDOB_ADMth.Focus();
        }


    }
    protected void txtDOB_ADMth_TextChanged(object sender, EventArgs e)
    {
        int month = Convert.ToInt32(txtDOB_ADMth.Text);

        if (month >= 13)
        {
            HelperFunction.MsgBox(this, this.GetType(), "Invalid month");
            txtDOB_ADMth.Focus();
        }
        else
        {
            txtDOB_ADYear.Focus();

        }


    }
}