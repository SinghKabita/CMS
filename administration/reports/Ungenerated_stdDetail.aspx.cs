using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;
using System.IO;

public partial class administration_reports_Ungenerated_stdDetail : System.Web.UI.Page
{
    HSS_STUDENT STEnt = new HSS_STUDENT();
    HSS_STUDENTService STSer = new HSS_STUDENTService();

    HSS_EDUCATION_DETAIL EDUEnt = new HSS_EDUCATION_DETAIL();
    HSS_EDUCATION_DETAILService EDUSer = new HSS_EDUCATION_DETAILService();

    HSS_ATTACHMENTS ATTCEnt = new HSS_ATTACHMENTS();
    HSS_ATTACHMENTSService ATTCSer = new HSS_ATTACHMENTSService();

    information_alert INAEnt = new information_alert();
    information_alertService INASer = new information_alertService();

    District DSEnt = new District();
    DistrictService DSSer = new DistrictService();

    ADDRESS AEnt = new ADDRESS();
    ADDRESSService ASrv = new ADDRESSService();

    FAMILY_RELATIONS FREnt = new FAMILY_RELATIONS();
    FAMILY_RELATIONSService FRSrv = new FAMILY_RELATIONSService();

    ADMISSION_DETAIL ADEnt = new ADMISSION_DETAIL();
    ADMISSION_DETAILService ADSrv = new ADMISSION_DETAILService();

    zone ZEnt = new zone();
    zoneService ZSer = new zoneService();

    COUNTRY CEnt = new COUNTRY();
    COUNTRYService CSrv = new COUNTRYService();

    State SEnt = new State();
    StateService SSrv = new StateService();

    RELIGION REnt = new RELIGION();
    RELIGIONService RSrv = new RELIGIONService();

    string imgfolder;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string pkId = "";
            if (!IsPostBack)
            {
                try
                {
                    pkId = Request.QueryString["pkId"].ToString();
                    if (pkId != "")
                        LoadAllData(pkId);
                }
                catch { }
            }
        }
    }

    protected void LoadAllData(string pkId)
    {
        LoadStudentDetail(pkId);

        LoadEducationInfo(pkId);

        LoadDocumentInfo(pkId);
        LoadNotificationInfo(pkId);

    }

    protected void LoadStudentDetail(string pkId)
    {

        STEnt = new HSS_STUDENT();
        STEnt.PK_ID = pkId;
        STEnt = (HSS_STUDENT)STSer.GetSingle(STEnt);
        if (STEnt != null)
        {
            if (!string.IsNullOrEmpty(pkId))
            {

                imgfolder = Server.MapPath(@"~/images/bachelorstudent/") + pkId + ".jpg";
                if (File.Exists(imgfolder))
                {
                    imgStudent.ImageUrl = "~/images/bachelorstudent/" + pkId + ".jpg";

                }
                else
                {
                    if (STEnt.GENDER.Trim() == "M")
                    {
                        imgStudent.ImageUrl = "~/images/user/male.jpg";
                    }
                    if (STEnt.GENDER.Trim() == "F")
                    {
                        imgStudent.ImageUrl = "~/images/user/female.jpg";
                    }
                }
            }


            lblBatch.Text = STEnt.BAT_CH;
            lblRegNo.Text = STEnt.STUDENT_ID;
            lblFullNameEng.Text = STEnt.NAME_ENGLISH;
            lblFullNameNep.Text = STEnt.NAME_DEVANAGARI;
            lblGender.Text = STEnt.GENDER;
            lblMaritalStatus.Text = STEnt.MARITAL_STATUS;
            lblNationality.Text = STEnt.NATIONALITY;
            lblReligion.Text = STEnt.RELIGION;
            lblDOBBS.Text = STEnt.DOB_BS;
            lblDOBAD.Text = STEnt.DOB_AD;
            lblCitizenship.Text = STEnt.CITIZENSHIP_NO;
            lblEmail.Text = STEnt.EMAIL;
            lblPhoneNo.Text = STEnt.PHONE;
            lblMobileNo1.Text = STEnt.MOBILE_1;
            lblMobileNo2.Text = STEnt.MOBILE_2;


            #region  Address



            #region for Both

            AEnt = new ADDRESS();
            AEnt.ADDRESS_OF_ID = STEnt.PK_ID;
            AEnt.ADDRESS_TYPE = "Both";
            AEnt = (ADDRESS)ASrv.GetSingle(AEnt);
            if (AEnt != null)
            {
                CEnt = new COUNTRY();
                CEnt.PK_ID = AEnt.COUNTRY;
                CEnt = (COUNTRY)CSrv.GetSingle(CEnt);
                if (CEnt != null)
                {
                    lblCountryP.Text = CEnt.COUNTRY_NAME;
                    lblCountryC.Text = CEnt.COUNTRY_NAME;
                }

                if (AEnt.STATE == "")
                {
                    lblStateP.Text = "";
                    lblStateC.Text = "";
                }
                else
                {
                    SEnt = new State();
                    SEnt.STATE_ID = AEnt.STATE;
                    SEnt = (State)SSrv.GetSingle(SEnt);
                    if (SEnt != null)
                    {
                        lblStateP.Text = SEnt.STATE;
                        lblStateC.Text = SEnt.STATE;
                    }
                }

                if (AEnt.ZONE == "")
                {
                    lblZoneP.Text = "";
                    lblZoneC.Text = "";
                }
                else
                {
                    ZEnt = new zone();
                    ZEnt.ZONE_ID = AEnt.ZONE;
                    ZEnt = (zone)ZSer.GetSingle(ZEnt);
                    if (ZEnt != null)
                    {
                        lblZoneP.Text = ZEnt.ZONE_NAME;
                        lblZoneC.Text = ZEnt.ZONE_NAME;
                    }
                }


                if (AEnt.DISTRICT == "")
                {
                    lblDistrictP.Text = "";
                    lblDistrictC.Text = "";
                }
                else
                {
                    DSEnt = new District();
                    DSEnt.ID = AEnt.DISTRICT;
                    DSEnt = (District)DSSer.GetSingle(DSEnt);
                    if (DSEnt != null)
                    {
                        lblDistrictP.Text = DSEnt.DISTRICTNAME;
                        lblDistrictC.Text = DSEnt.DISTRICTNAME;

                    }
                }

               

                

                lblVDCMuniP.Text = AEnt.VDC_MUNICIPALITY;
                lblWardnoP.Text = AEnt.WARD_NO;
                lblStreetNameP.Text = AEnt.STREET_NAME;
                lblHousenoP.Text = AEnt.HOUSE_NO;


                lblVDCMuniC.Text = AEnt.VDC_MUNICIPALITY;
                lblWardnoC.Text = AEnt.WARD_NO;
                lblStreetNameC.Text = AEnt.STREET_NAME;
                lblHousenoC.Text = AEnt.HOUSE_NO;
            }
            #endregion

            #region for Permanent

            AEnt = new ADDRESS();
            AEnt.ADDRESS_OF_ID = STEnt.PK_ID;
            AEnt.ADDRESS_TYPE = "P";
            AEnt = (ADDRESS)ASrv.GetSingle(AEnt);
            if (AEnt != null)
            {

                CEnt = new COUNTRY();
                CEnt.PK_ID = AEnt.COUNTRY;
                CEnt = (COUNTRY)CSrv.GetSingle(CEnt);
                if (CEnt != null)
                {
                    lblCountryP.Text = CEnt.COUNTRY_NAME;

                }

                if (AEnt.STATE == "")
                {
                    lblZoneP.Text = "";
                }
                else
                {
                    SEnt = new State();
                    SEnt.STATE_ID = AEnt.STATE;
                    SEnt = (State)SSrv.GetSingle(SEnt);
                    if (SEnt != null)
                    {
                        lblStateP.Text = SEnt.STATE;

                    }
                }

                if (AEnt.ZONE == "")
                {
                    lblZoneP.Text = "";
                }
                else
                {
                    ZEnt = new zone();
                    ZEnt.ZONE_ID = AEnt.ZONE;
                    ZEnt = (zone)ZSer.GetSingle(ZEnt);
                    if (ZEnt != null)
                    {
                        lblZoneP.Text = ZEnt.ZONE_NAME;

                    }
                }

                if (AEnt.DISTRICT == "")
                {
                    lblDistrictP.Text = "";
                }
                else
                {

                    DSEnt = new District();
                    DSEnt.ID = AEnt.DISTRICT;
                    DSEnt = (District)DSSer.GetSingle(DSEnt);
                    if (DSEnt != null)
                    {
                        lblDistrictP.Text = DSEnt.DISTRICTNAME;

                    }
                }

               
                lblVDCMuniP.Text = AEnt.VDC_MUNICIPALITY;
                lblWardnoP.Text = AEnt.WARD_NO;
                lblStreetNameP.Text = AEnt.STREET_NAME;
                lblHousenoP.Text = AEnt.HOUSE_NO;

            }


            #endregion

            #region for Contact
          

            AEnt = new ADDRESS();
            AEnt.ADDRESS_OF_ID = STEnt.PK_ID;
            AEnt.ADDRESS_TYPE = "C";
            AEnt = (ADDRESS)ASrv.GetSingle(AEnt);
            if (AEnt != null)
            {

                CEnt = new COUNTRY();
                CEnt.PK_ID = AEnt.COUNTRY;
                CEnt = (COUNTRY)CSrv.GetSingle(CEnt);
                if (CEnt != null)
                {
                    lblCountryC.Text = CEnt.COUNTRY_NAME;

                }

                if (AEnt.STATE == "")
                {
                    lblStateC.Text = "";

                }
                else
                {
                    SEnt = new State();
                    SEnt.STATE_ID = AEnt.STATE;
                    SEnt = (State)SSrv.GetSingle(SEnt);
                    if (SEnt != null)
                    {
                        lblStateC.Text = SEnt.STATE;

                    }
                }

                if (AEnt.ZONE == "")
                {
                    lblZoneC.Text = "";
                }
                else
                {
                    ZEnt = new zone();
                    ZEnt.ZONE_ID = AEnt.ZONE;
                    ZEnt = (zone)ZSer.GetSingle(ZEnt);
                    if (ZEnt != null)
                    {
                        lblZoneC.Text = ZEnt.ZONE_NAME;

                    }
                }


                if (AEnt.DISTRICT == "")
                {
                    lblDistrictC.Text = "";
                }
                else
                {
                    DSEnt = new District();
                    DSEnt.ID = AEnt.DISTRICT;
                    DSEnt = (District)DSSer.GetSingle(DSEnt);
                    if (DSEnt != null)
                    {
                        lblDistrictC.Text = DSEnt.DISTRICTNAME;

                    }
                }

                lblVDCMuniC.Text = AEnt.VDC_MUNICIPALITY;
                lblWardnoC.Text = AEnt.WARD_NO;
                lblStreetNameC.Text = AEnt.STREET_NAME;
                lblHousenoC.Text = AEnt.HOUSE_NO;
            }
            #endregion

            #endregion

            #region Family relation

            #region father

            FREnt = new FAMILY_RELATIONS();
            FREnt.RELATION_OF_ID = STEnt.PK_ID;
            FREnt.RELATION = "Father";
            FREnt = (FAMILY_RELATIONS)FRSrv.GetSingle(FREnt);
            if (FREnt != null)
            {
                lblFName.Text = FREnt.R_NAME;
                lblFAddress.Text = FREnt.ADDRESS;
                lblFPhoneNo.Text = FREnt.PHONE;
                lblFMobileNo1.Text = FREnt.MOBILE1;
                lblFMobileNo2.Text = FREnt.MOBILE1;
                lblFEmail.Text = FREnt.EMAIL;
                lblFOccupation.Text = FREnt.OCCUPATION;
                lblFDesignation.Text = FREnt.DESIGNATION;
                lblFOfficeName.Text = FREnt.NAME_OF_COMPANY;
                lblFOfficeNo.Text = FREnt.OFFICE_PHONE_NO;
            }
            #endregion

            #region mother

            FREnt = new FAMILY_RELATIONS();
            FREnt.RELATION_OF_ID = STEnt.PK_ID;
            FREnt.RELATION = "Mother";
            FREnt = (FAMILY_RELATIONS)FRSrv.GetSingle(FREnt);
            if (FREnt != null)
            {

                lblMName.Text = FREnt.R_NAME;
                lblMAddress.Text = FREnt.ADDRESS;
                lblMPhoneNo.Text = FREnt.PHONE;
                lblMMobileNo1.Text = FREnt.MOBILE1;
                lblMMobileNo2.Text = FREnt.MOBILE1;
                lblMEmail.Text = FREnt.EMAIL;
                lblMOccupation.Text = FREnt.OCCUPATION;
                lblMDesignation.Text = FREnt.DESIGNATION;
                lblMOfficeName.Text = FREnt.NAME_OF_COMPANY;
                lblMOfficeNo.Text = FREnt.OFFICE_PHONE_NO;
            }
            #endregion

            #region Spouse

            if (lblMaritalStatus.Text == "Married")
            {
                tblSpouse.Visible = true;
            }
            else
            {
                tblSpouse.Visible = false;
            }

            FREnt = new FAMILY_RELATIONS();
            FREnt.RELATION_OF_ID = STEnt.PK_ID;
            FREnt.RELATION = "Spouse";
            FREnt = (FAMILY_RELATIONS)FRSrv.GetSingle(FREnt);
            if (FREnt != null)
            {

                lblSName.Text = FREnt.R_NAME;
                lblSAddress.Text = FREnt.ADDRESS;
                lblSPhoneNo.Text = FREnt.PHONE;
                lblSMobileNo1.Text = FREnt.MOBILE1;
                lblSMobileNo2.Text = FREnt.MOBILE1;
                lblSEmail.Text = FREnt.EMAIL;
                lblSOccupation.Text = FREnt.OCCUPATION;
                lblSDesignation.Text = FREnt.DESIGNATION;
                lblSOfficeName.Text = FREnt.NAME_OF_COMPANY;
                lblSOfficeNo.Text = FREnt.OFFICE_PHONE_NO;
            }

            #endregion

            #region Guardian

            FREnt = new FAMILY_RELATIONS();
            FREnt.RELATION_OF_ID = STEnt.PK_ID;
            FREnt.IS_GUARDIAN = "TRUE";
            FREnt = (FAMILY_RELATIONS)FRSrv.GetSingle(FREnt);
            if (FREnt != null)
            {

                lblGName.Text = FREnt.R_NAME;
                lblGAddress.Text = FREnt.ADDRESS;
                lblGPhoneNo.Text = FREnt.PHONE;
                lblGMobileNo1.Text = FREnt.MOBILE1;
                lblGMobileNo2.Text = FREnt.MOBILE1;
                lblGEmail.Text = FREnt.EMAIL;
                lblGOccupation.Text = FREnt.OCCUPATION;
                lblGDesignation.Text = FREnt.DESIGNATION;
                lblGOfficeName.Text = FREnt.NAME_OF_COMPANY;
                lblGOfficeNo.Text = FREnt.OFFICE_PHONE_NO;
            }
            #endregion

            #endregion

            #region for Admission Detail
            ADEnt = new ADMISSION_DETAIL();
            ADEnt.STUDENT_PKID = STEnt.PK_ID;
            ADEnt = (ADMISSION_DETAIL)ADSrv.GetSingle(ADEnt);
            if (ADEnt != null)
            {

                lblEntranceRollNo.Text = ADEnt.ENTRANCE_ROLLNO;
                lblEntranceDate.Text = ADEnt.ENTRANCE_DATE;
                lblEntranceMarks.Text = ADEnt.OBTAINED_MARKS;
                lblInterviewDate.Text = ADEnt.INTERVIEW_DATE;
                lblInterviewBy.Text = ADEnt.INTERVIEW_BY;
                lblInterviewMarks.Text = ADEnt.INTERVIEW_MARKS;
                lblAdmissionNo.Text = ADEnt.ADMISSION_NO;
                lblAdmissionDate.Text = ADEnt.ADMISSION_DATE;

                lblFilledBy.Text = ADEnt.FORM_FILLED_BY;
                lblFilledDate.Text = ADEnt.FORM_FILLED_DATE;
                lblVerifiedBy.Text = ADEnt.VERIFIED_BY;
                lblVerifiedDate.Text = ADEnt.VERIFIED_DATE;
                txtRemarks.Text = ADEnt.REMARKS;

                //lblUniRegdNo.Text = ADEnt.REGISTRATION_NO;

            }
            #endregion

            CEnt = new COUNTRY();
            CEnt.PK_ID = STEnt.NATIONALITY;
            CEnt = (COUNTRY)CSrv.GetSingle(CEnt);
            if (CEnt != null)
            {
                lblNationality.Text = CEnt.NATIONALITY;
            }

            REnt = new RELIGION();
            REnt.PK_ID = STEnt.RELIGION;
            REnt = (RELIGION)RSrv.GetSingle(REnt);
            if (REnt != null)
            {
                lblReligion.Text = REnt.RELIGION_NAME;
            }

        }
    }

    protected void LoadEducationInfo(string pkId)
    {

        #region for SLC
        EDUEnt = new HSS_EDUCATION_DETAIL();
        EDUEnt.STUDENT_PKID = pkId;
        EDUEnt.PROGRAM_LEVEL = "SLC Information";
        EDUEnt = (HSS_EDUCATION_DETAIL)EDUSer.GetSingle(EDUEnt);
        if (EDUEnt != null)
        {
            lblSLCBoard.Text = EDUEnt.BOARD;
            lblSchoolName.Text = EDUEnt.INSTITUTION;


            if (EDUEnt.ZONE == "")
            {
                lblSchoolZone.Text = "";
            }
            else
            {

                ZEnt = new zone();
                ZEnt.ZONE_ID = EDUEnt.ZONE;
                ZEnt = (zone)ZSer.GetSingle(ZEnt);
                if (ZEnt != null)
                {
                    lblSchoolZone.Text = ZEnt.ZONE_NAME;
                }
            }


            if (EDUEnt.DISTRICT == "")
            {
                lblSchoolDistrict.Text = "";
            }

            else
            {
                DSEnt = new District();
                DSEnt.ID = EDUEnt.DISTRICT;
                DSEnt = (District)DSSer.GetSingle(DSEnt);
                if (DSEnt != null)
                {
                    lblSchoolDistrict.Text = DSEnt.DISTRICTNAME;
                }
            }

            //lblSchoolAddress.Text = EDUEnt.SCHOOL_MUNI;

            lblSLCPassedYear.Text = EDUEnt.PASSED_YEAR;
            lblSLCDivision.Text = EDUEnt.DIVISION;
            lblSLCPercentage.Text = EDUEnt.PERCENTAGE;
            lblSLCSymbolNo.Text = EDUEnt.SYMBOLE_NO;
            lblSLCOPT1.Text = EDUEnt.OPT_SUBJ1;
            lblSLCOPT2.Text = EDUEnt.OPT_SUBJ2;
            lblSLCOPT3.Text = EDUEnt.OPT_SUBJ3;
        }
        #endregion

        #region for +2
        EDUEnt = new HSS_EDUCATION_DETAIL();
        EDUEnt.STUDENT_PKID = pkId;
        EDUEnt.PROGRAM_LEVEL = "+2 Information";
        EDUEnt = (HSS_EDUCATION_DETAIL)EDUSer.GetSingle(EDUEnt);
        if (EDUEnt != null)
        {

            lblPTBoard.Text = EDUEnt.BOARD;
            lblCollegeName.Text = EDUEnt.INSTITUTION;

            if (EDUEnt.ZONE == "")
            {
                lblCollegeZone.Text = "";
            }
            else
            {

                ZEnt = new zone();
                ZEnt.ZONE_ID = EDUEnt.ZONE;
                ZEnt = (zone)ZSer.GetSingle(ZEnt);
                if (ZEnt != null)
                {
                    lblCollegeZone.Text = ZEnt.ZONE_NAME;
                }
            }

            if (EDUEnt.DISTRICT == "")
            {
                lblSchoolDistrict.Text = "";
            }

            else
            {

                DSEnt = new District();
                DSEnt.ID = EDUEnt.DISTRICT;
                DSEnt = (District)DSSer.GetSingle(DSEnt);
                if (DSEnt != null)
                {
                    lblCollegeDistrict.Text = DSEnt.DISTRICTNAME;
                }

            }
            //lblCollegeAddress.Text = EDUEnt.PT_MUNI;

            lblPTPassedYear.Text = EDUEnt.PASSED_YEAR;
            lblPTDivision.Text = EDUEnt.DIVISION;
            lblPTPercentage.Text = EDUEnt.PERCENTAGE;
            lblPTSymbolNo.Text = EDUEnt.SYMBOLE_NO;
            lblPTOPT1.Text = EDUEnt.OPT_SUBJ1;
            lblPTOPT2.Text = EDUEnt.OPT_SUBJ2;
            lblPTOPT3.Text = EDUEnt.OPT_SUBJ3;
            lblPTFaculty.Text = EDUEnt.FACULTY;
        }
        #endregion
    }

    protected void LoadDocumentInfo(string pkId)
    {
        ATTCEnt = new HSS_ATTACHMENTS();
        ATTCEnt.STUDENT_PKID = pkId;
        ATTCEnt = (HSS_ATTACHMENTS)ATTCSer.GetSingle(ATTCEnt);
        if (ATTCEnt != null)
        {

            if (ATTCEnt.PHOTO == "True")
            {
                lblPhotoAttached.Text = "YES";
            }
            else
            {
                lblPhotoAttached.Text = "NO";
            }
            if (ATTCEnt.TRANSCRIPT == "True")
            {
                lblSLCTranscriptAttached.Text = "YES";
            }
            else
            {
                lblSLCTranscriptAttached.Text = "NO";
            }
            if (ATTCEnt.CHARACTER_CERTIFICATE == "True")
            {
                lblSLCCharacterAttached.Text = "YES";
            }
            else
            {
                lblSLCCharacterAttached.Text = "NO";
            }

            if (ATTCEnt.PT_TRANSCRIPT == "True")
            {
                lblPTTranscriptAttached.Text = "YES";
            }
            else
            {
                lblPTTranscriptAttached.Text = "NO";
            }
            if (ATTCEnt.PT_CHARACTER_CERTIFICATE == "True")
            {
                lblPTCharacterAttached.Text = "YES";
            }
            else
            {
                lblPTCharacterAttached.Text = "NO";
            }

            if (ATTCEnt.CITIZENSHIP == "True")
            {
                lblCitizenshipAttached.Text = "YES";
            }
            else
            {
                lblCitizenshipAttached.Text = "NO";
            }
            if (ATTCEnt.CERTIFICATE == "True")
            {
                lblGovernmentCerificatedAttached.Text = "YES";
            }
            else
            {
                lblGovernmentCerificatedAttached.Text = "NO";
            }
            if (ATTCEnt.PT_PROVISIONAL_CERTIFICATE == "True")
            {
                lblPTProvisionalAttached.Text = "YES";
            }
            else
            {
                lblPTProvisionalAttached.Text = "NO";
            }
            if (ATTCEnt.PT_MIGRATION_CERTIFICATE == "True")
            {
                lblPTMigrationAttached.Text = "YES";
            }
            else
            {
                lblPTMigrationAttached.Text = "NO";
            }


        }

    }

    protected void LoadNotificationInfo(string pkId)
    {

        INAEnt = new information_alert();
        INAEnt.STUDENT_PKID = pkId;
        INAEnt = (information_alert)INASer.GetSingle(INAEnt);
        if (INAEnt != null)
        {
            lblPrimaryNumber.Text = INAEnt.SMS_ALERT1;
            if (INAEnt.SMS_ALERT1_STATUS == "1")
            {
                lblPrimaryNoAlert.Text = "YES";
            }
            else
            {
                lblPrimaryNoAlert.Text = "NO";
            }

            lblSecondaryNumber.Text = INAEnt.SMS_ALERT2;

            if (INAEnt.SMS_ALERT2_STATUS == "1")
            {
                lblSecondaryNoAlert.Text = "YES";
            }
            else
            {
                lblSecondaryNoAlert.Text = "NO";
            }


            lblPrimaryEmail.Text = INAEnt.EMAIL_ALERT1;
            if (INAEnt.EMAIL_ALERT1_STATUS == "1")
            {
                lblPrimaryEmailAlert.Text = "YES";
            }
            else
            {
                lblPrimaryEmailAlert.Text = "NO";
            }


            lblSecondaryEmail.Text = INAEnt.EMAIL_ALERT2;

            if (INAEnt.EMAIL_ALERT2_STATUS == "1")
            {
                lblSecondaryEmailAlert.Text = "YES";
            }
            else
            {
                lblSecondaryEmailAlert.Text = "NO";
            }

        }

    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "printPartOfPage();", true);
    }
}