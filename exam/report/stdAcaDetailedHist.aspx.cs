using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;
using Service.Framework;
using Entity.Framework;
using System.IO;
using System.Data;

public partial class exam_report_stdAcaDetailedHist : System.Web.UI.Page
{
    HSS_NAME NEnt = new HSS_NAME();
    HSS_NAMEService NSer = new HSS_NAMEService();

    HSS_STUDENT STDEnt = new HSS_STUDENT();
    HSS_STUDENTService STDSer = new HSS_STUDENTService();

    HSS_EDUCATION_DETAIL EDUEnt = new HSS_EDUCATION_DETAIL();
    HSS_EDUCATION_DETAILService EDUSer = new HSS_EDUCATION_DETAILService();

    ADDRESS AEnt = new ADDRESS();
    ADDRESSService ASer = new ADDRESSService();

    FAMILY_RELATIONS FREnt = new FAMILY_RELATIONS();
    FAMILY_RELATIONSService FRSer = new FAMILY_RELATIONSService();

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    HSS_CURRENT_STUDENT CSEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSer = new HSS_CURRENT_STUDENTService();

    HSS_SUBJECT SUBEnt = new HSS_SUBJECT();
    HSS_SUBJECTService SUBSer = new HSS_SUBJECTService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    program PEnt = new program();
    programService PSer = new programService();

    BatchYear BYEnt = new BatchYear();
    BatchYearService BYSer = new BatchYearService();

    full_pass_marks FPMEnt = new full_pass_marks();
    full_pass_marksService FPMSer = new full_pass_marksService();

    EXAM_TYPE ETEnt = new EXAM_TYPE();
    EXAM_TYPEService ETSer = new EXAM_TYPEService();

    EXAM_TYPE_MASTER ETMEnt = new EXAM_TYPE_MASTER();
    EXAM_TYPE_MASTERService ETMSer = new EXAM_TYPE_MASTERService();

    EXAM_MARKS EMEnt = new EXAM_MARKS();
    EXAM_MARKSService EMSer = new EXAM_MARKSService();

    SGPA_EIGHT GPAEnt = new SGPA_EIGHT();
    SGPA_EIGHTService GPASer = new SGPA_EIGHTService();

    HelperFunction hf = new HelperFunction();

    string studentPK = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                studentPK = Request.QueryString["StdPK"].ToString();
                if (studentPK != "")
                    LoadAllData(studentPK);


            }
            catch { }
        }
    }

    protected void LoadAllData(string studentPK)
    {
        NEnt = new HSS_NAME();
        NEnt = (HSS_NAME)NSer.GetSingle(NEnt);
        if (NEnt != null)
        {
            lblOrgName.Text = NEnt.NAME + "(" + NEnt.CODE + ")" + ",";
            string[] address = NEnt.ADRESS.Split(',');
            lblOrgAdd.Text = address[0];
        }
        loadStudentDetail(studentPK);
        loadStdEduDetail(studentPK);
        loadStdAddressDetail(studentPK);
        loadStdFamilyDetail(studentPK);
        loadGridAcaHistory(studentPK);
    }

    protected void loadStudentDetail(string studentPK)
    {
        STDEnt = new HSS_STUDENT();
        STDEnt.PK_ID = studentPK;
        STDEnt = (HSS_STUDENT)STDSer.GetSingle(STDEnt);
        if (STDEnt != null)
        {
            lblStdFullName.Text = STDEnt.NAME_ENGLISH;
            lblNCCSRegNo.Text = STDEnt.STUDENT_ID;
            lblDOB.Text = STDEnt.DOB_AD;
            lblSelfMobileNo.Text = STDEnt.MOBILE_1;
            lblTURoll.Text = STDEnt.UNIVERSITY_REG_NO;
            lblBatchYear.Text = STDEnt.BAT_CH;

            PEnt = new program();
            PEnt.PK_ID = STDEnt.PROGRAM;
            PEnt = (program)PSer.GetSingle(PEnt);
            if (PEnt != null)
            {
                lblProgramID.Text = STDEnt.PROGRAM;
                lblProgram.Text = PEnt.PROGRAM_CODE;
                lblLevel.Text = PEnt.PROGRAM_LEVEL;
                FCEnt = new hss_faculty();
                FCEnt.PK_ID = PEnt.FACULTY_ID;
                FCEnt = (hss_faculty)FCSer.GetSingle(FCEnt);
                if (FCEnt != null)
                    lblBachelorFaculty.Text = FCEnt.FACULTY;
            }

            string imgfolder = Server.MapPath(@"~/images/bachelorstudent/") + STDEnt.STUDENT_ID + ".jpg";
            if (File.Exists(imgfolder))
            {
                imgStudent.ImageUrl = "~/images/bachelorstudent/" + STDEnt.STUDENT_ID + ".jpg";

            }
            else
            {
                if (STDEnt.GENDER.Trim() == "M")
                {
                    imgStudent.ImageUrl = "~/images/bachelorstudent/male.jpeg";
                }
                if (STDEnt.GENDER.Trim() == "F")
                {
                    imgStudent.ImageUrl = "~/images/bachelorstudent/female.jpeg";
                }
            }

        }
    }

    protected void loadStdEduDetail(string studentPK)
    {
        EDUEnt = new HSS_EDUCATION_DETAIL();
        EDUEnt.STUDENT_PKID = studentPK;
        EDUEnt.PROGRAM_LEVEL = "+2 Information";
        EDUEnt = (HSS_EDUCATION_DETAIL)EDUSer.GetSingle(EDUEnt);
        if (EDUEnt != null)
        {
            lblPlus2College.Text = EDUEnt.INSTITUTION;
            lblPlus2Perc.Text = EDUEnt.PERCENTAGE;
            lblPlus2Faculty.Text = EDUEnt.FACULTY;
        }
    }

    protected void loadStdAddressDetail(string studentPK)
    {
        EntityList thelist = new EntityList();
        AEnt = new ADDRESS();
        AEnt.ADDRESS_OF_ID = studentPK;
        AEnt.ADDRESS_TYPE = "Both";
        AEnt = (ADDRESS)ASer.GetSingle(AEnt);
        if (AEnt != null)
        {

            if (AEnt.STREET_NAME != "")
            {
                lblPermAddress.Text += AEnt.STREET_NAME + ",";
                lblTempAddress.Text += AEnt.STREET_NAME + ",";
            }
            lblPermAddress.Text += AEnt.VDC_MUNICIPALITY + "-" + AEnt.WARD_NO;
            lblTempAddress.Text += AEnt.VDC_MUNICIPALITY + "-" + AEnt.WARD_NO;
        }
        else
        {

            AEnt = new ADDRESS();
            AEnt.ADDRESS_OF_ID = studentPK;
            thelist = ASer.GetAll(AEnt);
            if (thelist.Count > 0)
            {
                foreach (ADDRESS ad in thelist)
                {
                    if (ad.ADDRESS_TYPE == "P")
                    {
                        if (ad.STREET_NAME != "")
                            lblPermAddress.Text += ad.STREET_NAME + ",";
                        lblPermAddress.Text += ad.VDC_MUNICIPALITY + "-" + ad.WARD_NO;
                    }
                    if (ad.ADDRESS_TYPE == "C")
                    {
                        if (ad.STREET_NAME != "")
                            lblTempAddress.Text += ad.STREET_NAME + ",";
                        lblTempAddress.Text += ad.VDC_MUNICIPALITY + "-" + ad.WARD_NO;
                    }
                }
            }

        }
    }

    protected void loadStdFamilyDetail(string studentPK)
    {
        EntityList FamRelList = new EntityList();
        FREnt = new FAMILY_RELATIONS();
        FREnt.RELATION_OF_ID = studentPK;
        FamRelList = FRSer.GetAll(FREnt);
        if (FamRelList.Count > 0)
        {
            foreach (FAMILY_RELATIONS fr in FamRelList)
            {
                if (fr.RELATION == "Father")
                {
                    lblFatherName.Text = fr.R_NAME;
                    lblFatherAddress.Text = fr.ADDRESS;
                    lblFatherOccupation.Text = fr.OCCUPATION;
                    if (fr.DESIGNATION != "")
                        lblFatherOccupation.Text += fr.DESIGNATION;

                    lblFatherTelNo.Text = fr.MOBILE1;

                    if (fr.IS_GUARDIAN == "TRUE")
                    {
                        lblGuardianName.Text = fr.R_NAME;
                        lblGuardianAddress.Text = fr.ADDRESS;
                        lblGuardianOccupation.Text = fr.OCCUPATION;
                        if (fr.DESIGNATION != "")
                            lblGuardianOccupation.Text += fr.DESIGNATION;
                        lblGuardianTelNo.Text = fr.MOBILE1;
                    }
                }

                if (fr.RELATION == "Mother")
                {
                    lblMotherName.Text = fr.R_NAME;
                    lblMotherAddress.Text = fr.ADDRESS;
                    lblMotherOccupation.Text = fr.OCCUPATION;
                    if (fr.DESIGNATION != "")
                        lblMotherOccupation.Text += fr.DESIGNATION;
                    lblMotherTelNo.Text = fr.MOBILE1;

                    if (fr.IS_GUARDIAN == "TRUE")
                    {
                        lblGuardianName.Text = fr.R_NAME;
                        lblGuardianAddress.Text = fr.ADDRESS;
                        lblGuardianOccupation.Text = fr.OCCUPATION;
                        if (fr.DESIGNATION != "")
                            lblGuardianOccupation.Text += fr.DESIGNATION;
                        lblGuardianTelNo.Text = fr.MOBILE1;
                    }
                }

                if (fr.RELATION == "Spouse")
                {
                    lblSpouseName.Text = fr.R_NAME;
                    lblSpouseAddress.Text = fr.ADDRESS;
                    lblSpouseOccupation.Text = fr.OCCUPATION;
                    if (fr.DESIGNATION != "")
                        lblSpouseOccupation.Text += fr.DESIGNATION;
                    lblSpouseTelNo.Text = fr.MOBILE1;

                    if (fr.IS_GUARDIAN == "TRUE")
                    {
                        lblGuardianName.Text = fr.R_NAME;
                        lblGuardianAddress.Text = fr.ADDRESS;
                        lblGuardianOccupation.Text = fr.OCCUPATION;
                        if (fr.DESIGNATION != "")
                            lblGuardianOccupation.Text += fr.DESIGNATION;
                        lblGuardianTelNo.Text = fr.MOBILE1;
                    }
                }

            }
        }
    }

    protected void loadGridAcaHistory(string studentPK)
    {
        gridAcaHistory.DataSource = hf.getStd_Sem_Sub(lblProgramID.Text, hf.getSyllabusYearfromBatch(lblProgramID.Text, lblBatchYear.Text));
        gridAcaHistory.DataBind();

    }


    protected void gridAcaHistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.Header)
        {

            #region for dynamic heading
            string examtypeMaster1 = "", examtypeMaster2 = "", examtypeMaster3 = "", examtypeMaster4 = "", examtypeMaster5 = "", examtypeMaster6 = "", examtypeMaster7 = "";


            #region getting and loading all examtypeMaster and hiding inactive examtypes
            EntityList theListMaster = new EntityList();
            ETMEnt = new EXAM_TYPE_MASTER();
            ETMEnt.STATUS = "1";
            theListMaster = ETMSer.GetAll(ETMEnt);
            if (theListMaster.Count > 0)
            {
                foreach (EXAM_TYPE_MASTER etm in theListMaster)
                {
                    if (examtypeMaster1 == "")
                    {
                        examtypeMaster1 = etm.PKID;
                        continue;
                    }
                    if (examtypeMaster2 == "")
                    {
                        examtypeMaster2 = etm.PKID;
                        continue;
                    }
                    if (examtypeMaster3 == "")
                    {
                        examtypeMaster3 = etm.PKID;
                        continue;
                    }
                    if (examtypeMaster4 == "")
                    {
                        examtypeMaster4 = etm.PKID;
                        continue;
                    }
                    if (examtypeMaster5 == "")
                    {
                        examtypeMaster5 = etm.PKID;
                        continue;
                    }
                    if (examtypeMaster6 == "")
                    {
                        examtypeMaster6 = etm.PKID;
                        continue;
                    }
                    if (examtypeMaster7 == "")
                    {
                        examtypeMaster7 = etm.PKID;
                        continue;
                    }
                }
            }

            //Label lblExamTypeMasterTh1 = e.Row.FindControl("lblExamTypeMasterTh1") as Label;
            //Label lblExamTypeMasterTh2 = e.Row.FindControl("lblExamTypeMasterTh2") as Label;
            //Label lblExamTypeMasterTh3 = e.Row.FindControl("lblExamTypeMasterTh3") as Label;
            //Label lblExamTypeMasterTh4 = e.Row.FindControl("lblExamTypeMasterTh4") as Label;
            //Label lblExamTypeMasterTh5 = e.Row.FindControl("lblExamTypeMasterTh5") as Label;
            //Label lblExamTypeMasterTh6 = e.Row.FindControl("lblExamTypeMasterTh6") as Label;
            //Label lblExamTypeMasterTh7 = e.Row.FindControl("lblExamTypeMasterTh7") as Label;

            Label lblExamTypeMaster1 = e.Row.FindControl("lblExamTypeMaster1") as Label;
            Label lblExamTypeMaster2 = e.Row.FindControl("lblExamTypeMaster2") as Label;
            Label lblExamTypeMaster3 = e.Row.FindControl("lblExamTypeMaster3") as Label;
            Label lblExamTypeMaster4 = e.Row.FindControl("lblExamTypeMaster4") as Label;
            Label lblExamTypeMaster5 = e.Row.FindControl("lblExamTypeMaster5") as Label;
            Label lblExamTypeMaster6 = e.Row.FindControl("lblExamTypeMaster6") as Label;
            Label lblExamTypeMaster7 = e.Row.FindControl("lblExamTypeMaster7") as Label;

            //Label lblExamTypeMasterMu1 = e.Row.FindControl("lblExamTypeMasterMu1") as Label;
            //Label lblExamTypeMasterMu2 = e.Row.FindControl("lblExamTypeMasterMu2") as Label;
            //Label lblExamTypeMasterMu3 = e.Row.FindControl("lblExamTypeMasterMu3") as Label;
            //Label lblExamTypeMasterMu4 = e.Row.FindControl("lblExamTypeMasterMu4") as Label;
            //Label lblExamTypeMasterMu5 = e.Row.FindControl("lblExamTypeMasterMu5") as Label;
            //Label lblExamTypeMasterMu6 = e.Row.FindControl("lblExamTypeMasterMu6") as Label;
            //Label lblExamTypeMasterMu7 = e.Row.FindControl("lblExamTypeMasterMu7") as Label;

            //lblExamTypeMasterTh1.Text = examtypeMaster1;
            //lblExamTypeMasterTh2.Text = examtypeMaster2;
            //lblExamTypeMasterTh3.Text = examtypeMaster3;
            //lblExamTypeMasterTh4.Text = examtypeMaster4;
            //lblExamTypeMasterTh5.Text = examtypeMaster5;
            //lblExamTypeMasterTh6.Text = examtypeMaster6;

            //lblExamTypeMaster1.Text = examtypeMaster1;
            //lblExamTypeMaster2.Text = examtypeMaster2;
            //lblExamTypeMaster3.Text = examtypeMaster3;
            //lblExamTypeMaster4.Text = examtypeMaster4;
            //lblExamTypeMaster5.Text = examtypeMaster5;
            //lblExamTypeMaster6.Text = examtypeMaster6;

            //lblExamTypeMasterMu1.Text = examtypeMaster1;
            //lblExamTypeMasterMu2.Text = examtypeMaster2;
            //lblExamTypeMasterMu3.Text = examtypeMaster3;
            //lblExamTypeMasterMu4.Text = examtypeMaster4;
            //lblExamTypeMasterMu5.Text = examtypeMaster5;
            //lblExamTypeMasterMu6.Text = examtypeMaster6;

            #region to hide inactive exam types
            #region for first class test
            ETEnt = new EXAM_TYPE();
            ETEnt.PROGRAM = lblProgramID.Text;
            ETEnt.EXAM_TYPE_MASTERID = examtypeMaster1;
            ETEnt.STATUS = "1";
            ETEnt = (EXAM_TYPE)ETSer.GetSingle(ETEnt);
            if (ETEnt == null)
            {
                gridAcaHistory.Columns[2].Visible = false;
                gridAcaHistory.Columns[3].Visible = false;
                gridAcaHistory.Columns[4].Visible = false;
            }
            else
            {
                gridAcaHistory.Columns[2].Visible = true;
                gridAcaHistory.Columns[3].Visible = true;
                gridAcaHistory.Columns[4].Visible = true;
            }
            #endregion

            #region for First Term
            ETEnt = new EXAM_TYPE();
            ETEnt.PROGRAM = lblProgramID.Text;
            ETEnt.EXAM_TYPE_MASTERID = examtypeMaster2;
            ETEnt.STATUS = "1";
            ETEnt = (EXAM_TYPE)ETSer.GetSingle(ETEnt);
            if (ETEnt == null)
            {
                gridAcaHistory.Columns[5].Visible = false;
                gridAcaHistory.Columns[6].Visible = false;
                gridAcaHistory.Columns[7].Visible = false;
            }
            else
            {
                gridAcaHistory.Columns[5].Visible = true;
                gridAcaHistory.Columns[6].Visible = true;
                gridAcaHistory.Columns[7].Visible = true;
            }
            #endregion

            #region for Second Class Test
            ETEnt = new EXAM_TYPE();
            ETEnt.PROGRAM = lblProgramID.Text;
            ETEnt.EXAM_TYPE_MASTERID = examtypeMaster3;
            ETEnt.STATUS = "1";
            ETEnt = (EXAM_TYPE)ETSer.GetSingle(ETEnt);
            if (ETEnt == null)
            {
                gridAcaHistory.Columns[8].Visible = false;
                gridAcaHistory.Columns[9].Visible = false;
                gridAcaHistory.Columns[10].Visible = false;
            }
            else
            {
                gridAcaHistory.Columns[8].Visible = true;
                gridAcaHistory.Columns[9].Visible = true;
                gridAcaHistory.Columns[10].Visible = true;
            }
            #endregion

            #region for Mid Term
            ETEnt = new EXAM_TYPE();
            ETEnt.PROGRAM = lblProgramID.Text;
            ETEnt.EXAM_TYPE_MASTERID = examtypeMaster4;
            ETEnt.STATUS = "1";
            ETEnt = (EXAM_TYPE)ETSer.GetSingle(ETEnt);
            if (ETEnt == null)
            {
                gridAcaHistory.Columns[11].Visible = false;
                gridAcaHistory.Columns[12].Visible = false;
                gridAcaHistory.Columns[13].Visible = false;
            }
            else
            {
                gridAcaHistory.Columns[11].Visible = true;
                gridAcaHistory.Columns[12].Visible = true;
                gridAcaHistory.Columns[13].Visible = true;
            }
            #endregion

            #region for Third Class Test
            ETEnt = new EXAM_TYPE();
            ETEnt.PROGRAM = lblProgramID.Text;
            ETEnt.EXAM_TYPE_MASTERID = examtypeMaster5;
            ETEnt.STATUS = "1";
            ETEnt = (EXAM_TYPE)ETSer.GetSingle(ETEnt);
            if (ETEnt == null)
            {
                gridAcaHistory.Columns[14].Visible = false;
                gridAcaHistory.Columns[15].Visible = false;
                gridAcaHistory.Columns[16].Visible = false;
            }
            else
            {
                gridAcaHistory.Columns[14].Visible = true;
                gridAcaHistory.Columns[15].Visible = true;
                gridAcaHistory.Columns[16].Visible = true;
            }
            #endregion

            #region for Final Exam
            ETEnt = new EXAM_TYPE();
            ETEnt.PROGRAM = lblProgramID.Text;
            ETEnt.EXAM_TYPE_MASTERID = examtypeMaster6;
            ETEnt.STATUS = "1";
            ETEnt = (EXAM_TYPE)ETSer.GetSingle(ETEnt);
            if (ETEnt == null)
            {
                gridAcaHistory.Columns[17].Visible = false;
                gridAcaHistory.Columns[18].Visible = false;
                gridAcaHistory.Columns[19].Visible = false;
            }
            else
            {
                gridAcaHistory.Columns[17].Visible = true;
                gridAcaHistory.Columns[18].Visible = true;
                gridAcaHistory.Columns[19].Visible = true;
            }
            #endregion
            #endregion

            #endregion


            #endregion

            #region to display exam type name from master

            ETMEnt = new EXAM_TYPE_MASTER();
            ETMEnt.PKID = examtypeMaster1;
            ETMEnt = (EXAM_TYPE_MASTER)ETMSer.GetSingle(ETMEnt);
            if (ETMEnt != null)
            {
                lblExamTypeMaster1.Text = ETMEnt.EXAM_TYPE;
            }

            ETMEnt = new EXAM_TYPE_MASTER();
            ETMEnt.PKID = examtypeMaster2;
            ETMEnt = (EXAM_TYPE_MASTER)ETMSer.GetSingle(ETMEnt);
            if (ETMEnt != null)
            {
                lblExamTypeMaster2.Text = ETMEnt.EXAM_TYPE;
            }

            ETMEnt = new EXAM_TYPE_MASTER();
            ETMEnt.PKID = examtypeMaster3;
            ETMEnt = (EXAM_TYPE_MASTER)ETMSer.GetSingle(ETMEnt);
            if (ETMEnt != null)
            {
                lblExamTypeMaster3.Text = ETMEnt.EXAM_TYPE;
            }

            ETMEnt = new EXAM_TYPE_MASTER();
            ETMEnt.PKID = examtypeMaster4;
            ETMEnt = (EXAM_TYPE_MASTER)ETMSer.GetSingle(ETMEnt);
            if (ETMEnt != null)
            {
                lblExamTypeMaster4.Text = ETMEnt.EXAM_TYPE;
            }

            ETMEnt = new EXAM_TYPE_MASTER();
            ETMEnt.PKID = examtypeMaster5;
            ETMEnt = (EXAM_TYPE_MASTER)ETMSer.GetSingle(ETMEnt);
            if (ETMEnt != null)
            {
                lblExamTypeMaster5.Text = ETMEnt.EXAM_TYPE;
            }

            ETMEnt = new EXAM_TYPE_MASTER();
            ETMEnt.PKID = examtypeMaster6;
            ETMEnt = (EXAM_TYPE_MASTER)ETMSer.GetSingle(ETMEnt);
            if (ETMEnt != null)
            {
                lblExamTypeMaster6.Text = ETMEnt.EXAM_TYPE;
            }

            #endregion

        }

        #region grid body part
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            #region to load examtypemaster id in body
            string examtypeMaster1 = "", examtypeMaster2 = "", examtypeMaster3 = "", examtypeMaster4 = "", examtypeMaster5 = "", examtypeMaster6 = "", examtypeMaster7 = "";

            EntityList theListMaster = new EntityList();
            ETMEnt = new EXAM_TYPE_MASTER();
            ETMEnt.STATUS = "1";
            theListMaster = ETMSer.GetAll(ETMEnt);
            if (theListMaster.Count > 0)
            {
                foreach (EXAM_TYPE_MASTER etm in theListMaster)
                {
                    if (examtypeMaster1 == "")
                    {
                        examtypeMaster1 = etm.PKID;
                        continue;
                    }
                    if (examtypeMaster2 == "")
                    {
                        examtypeMaster2 = etm.PKID;
                        continue;
                    }
                    if (examtypeMaster3 == "")
                    {
                        examtypeMaster3 = etm.PKID;
                        continue;
                    }
                    if (examtypeMaster4 == "")
                    {
                        examtypeMaster4 = etm.PKID;
                        continue;
                    }
                    if (examtypeMaster5 == "")
                    {
                        examtypeMaster5 = etm.PKID;
                        continue;
                    }
                    if (examtypeMaster6 == "")
                    {
                        examtypeMaster6 = etm.PKID;
                        continue;
                    }
                    if (examtypeMaster7 == "")
                    {
                        examtypeMaster7 = etm.PKID;
                        continue;
                    }
                }
            }

            #endregion

            Label lblSubjectID = e.Row.FindControl("lblSubjectID") as Label;
            Label lblSemesterID = e.Row.FindControl("lblSemesterID") as Label;
            Label lblAtten = e.Row.FindControl("lblAtten") as Label;
            Label lblSemester = e.Row.FindControl("lblSemester") as Label;

            lblSemester.Text += " Sem";

            #region First Class test
            Label lblFCTID = e.Row.FindControl("lblFCTID") as Label;
            Label lblFMFCT = e.Row.FindControl("lblFMFCT") as Label;

            Label lblFCTMarks = e.Row.FindControl("lblFCTMarks") as Label;
            Label lblFCTMakeUp = e.Row.FindControl("lblFCTMakeUp") as Label;

            Label lblAstrikFCT = e.Row.FindControl("lblAstrikFCT") as Label;
            //Label lblAstrikFCTmakeup = e.Row.FindControl("lblAstrikFCTmakeup") as Label;


            #region to load examtype PKID as per PROGRAM
            ETMEnt = new EXAM_TYPE_MASTER();
            ETMEnt.PKID = examtypeMaster1;
            ETMEnt = (EXAM_TYPE_MASTER)ETMSer.GetSingle(ETMEnt);
            if (ETMEnt != null)
            {
                ETEnt = new EXAM_TYPE();
                ETEnt.PROGRAM = lblProgramID.Text;
                ETEnt.EXAM_TYPE_MASTERID = ETMEnt.PKID;
                ETEnt.STATUS = "1";
                ETEnt = (EXAM_TYPE)ETSer.GetSingle(ETEnt);
                if (ETEnt != null)
                {
                    lblFCTID.Text = ETEnt.PKID;
                }
            }
            #endregion

            #region to load FULL_PASS marks
            FPMEnt = new full_pass_marks();
            FPMEnt.SUBJECT_ID = lblSubjectID.Text;
            FPMEnt.EXAM_TYPE = lblFCTID.Text;
            FPMEnt = (full_pass_marks)FPMSer.GetSingle(FPMEnt);
            if (FPMEnt != null)
            {
                lblFMFCT.Text = FPMEnt.FULLMARKS_THRCL + "<br />" + "PM:" + FPMEnt.PASSMARKS_THRCL;

            }
            #endregion
            #region to load marks/make up marks
            EMEnt = new EXAM_MARKS();
            EMEnt.STUDENT_ID = lblNCCSRegNo.Text;
            EMEnt.SEMESTER = lblSemesterID.Text;
            EMEnt.EXAM_TYPE = lblFCTID.Text;
            EMEnt.SUBJECT = lblSubjectID.Text;
            EMEnt = (EXAM_MARKS)EMSer.GetSingle(EMEnt);
            if (EMEnt != null)
            {
                lblFCTMarks.Text = EMEnt.MARKS;
                lblFCTMakeUp.Text = EMEnt.MAKEUP;
            }
            #endregion
            #region to add * in failed sub
            //FPMEnt = new full_pass_marks();
            //FPMEnt.SUBJECT_ID = lblSubjectID.Text;
            //FPMEnt.EXAM_TYPE = lblFCTID.Text;
            //FPMEnt = (full_pass_marks)FPMSer.GetSingle(FPMEnt);
            //if (FPMEnt != null)
            //{
            //    if (Convert.ToDouble(lblFCTMarks.Text) < Convert.ToDouble(FPMEnt.PASSMARKS_THRCL))
            //    {
            //        lblAstrikFCT.Visible = true;
            //    }
            //    else
            //    {
            //        lblAstrikFCT.Visible = false;
            //    }

            //}

            #endregion


            #endregion

            #region First Term
            Label lblFTID = e.Row.FindControl("lblFTID") as Label;
            Label lblFMFT = e.Row.FindControl("lblFMFT") as Label;
            Label lblFTMarks = e.Row.FindControl("lblFTMarks") as Label;
            Label lblFTMakeUp = e.Row.FindControl("lblFTMakeUp") as Label;
            //Label lblAstrikTE = e.Row.FindControl("lblAstrikTE") as Label;
            //Label lblAstrikTEmakeup = e.Row.FindControl("lblAstrikTEmakeup") as Label;

            #region to load examtype PKID as per PROGRAM
            ETMEnt = new EXAM_TYPE_MASTER();
            ETMEnt.PKID = examtypeMaster2;
            ETMEnt = (EXAM_TYPE_MASTER)ETMSer.GetSingle(ETMEnt);
            if (ETMEnt != null)
            {
                ETEnt = new EXAM_TYPE();
                ETEnt.PROGRAM = lblProgramID.Text;
                ETEnt.EXAM_TYPE_MASTERID = ETMEnt.PKID;
                ETEnt.STATUS = "1";
                ETEnt = (EXAM_TYPE)ETSer.GetSingle(ETEnt);
                if (ETEnt != null)
                {
                    lblFTID.Text = ETEnt.PKID;
                }
            }
            #endregion

            #region to load FULL_PASS marks
            FPMEnt = new full_pass_marks();
            FPMEnt.SUBJECT_ID = lblSubjectID.Text;
            FPMEnt.EXAM_TYPE = lblFTID.Text;
            FPMEnt = (full_pass_marks)FPMSer.GetSingle(FPMEnt);
            if (FPMEnt != null && lblFTID.Text != "")
            {
                lblFMFT.Text = FPMEnt.FULLMARKS_THRCL + "<br />" + "PM:" + FPMEnt.PASSMARKS_THRCL;

                #region to load marks/make up marks
                EMEnt = new EXAM_MARKS();
                EMEnt.STUDENT_ID = lblNCCSRegNo.Text;
                EMEnt.SEMESTER = lblSemesterID.Text;
                EMEnt.EXAM_TYPE = lblFTID.Text;
                EMEnt.SUBJECT = lblSubjectID.Text;
                EMEnt = (EXAM_MARKS)EMSer.GetSingle(EMEnt);
                if (EMEnt != null && lblFTID.Text != "")
                {
                    lblFTMarks.Text = EMEnt.MARKS;
                    lblFTMakeUp.Text = EMEnt.MAKEUP;
                }
                #endregion
                #region to add * in failed sub
                //if (Convert.ToDouble(lblTEMarks.Text) < Convert.ToDouble(FPMEnt.PASSMARKS_THRCL))
                //{
                //    lblAstrikTE.Visible = true;
                //}
                //if (Convert.ToDouble(lblTEMakeUp.Text) < Convert.ToDouble(FPMEnt.PASSMARKS_THRCL))
                //{
                //    lblAstrikTEmakeup.Visible = true;
                //}
                #endregion
            }
            #endregion

            #endregion

            #region Second Class Test
            Label lblSCTID = e.Row.FindControl("lblSCTID") as Label;
            Label lblFMSCT = e.Row.FindControl("lblFMSCT") as Label;
            Label lblSCTMarks = e.Row.FindControl("lblSCTMarks") as Label;
            Label lblSCTMakeUp = e.Row.FindControl("lblSCTMakeUp") as Label;

            #region to load examtype PKID as per PROGRAM
            ETMEnt = new EXAM_TYPE_MASTER();
            ETMEnt.PKID = examtypeMaster3;
            ETMEnt = (EXAM_TYPE_MASTER)ETMSer.GetSingle(ETMEnt);
            if (ETMEnt != null)
            {
                ETEnt = new EXAM_TYPE();
                ETEnt.PROGRAM = lblProgramID.Text;
                ETEnt.EXAM_TYPE_MASTERID = ETMEnt.PKID;
                ETEnt.STATUS = "1";
                ETEnt = (EXAM_TYPE)ETSer.GetSingle(ETEnt);
                if (ETEnt != null)
                {
                    lblSCTID.Text = ETEnt.PKID;
                }
            }
            #endregion

            #region to load FULL_PASS marks
            FPMEnt = new full_pass_marks();
            FPMEnt.SUBJECT_ID = lblSubjectID.Text;
            FPMEnt.EXAM_TYPE = lblSCTID.Text;
            FPMEnt = (full_pass_marks)FPMSer.GetSingle(FPMEnt);
            if (FPMEnt != null && lblSCTID.Text != "")
            {
                lblFMSCT.Text = FPMEnt.FULLMARKS_THRCL + "<br />" + "PM:" + FPMEnt.PASSMARKS_THRCL;
            }
            #endregion

            #region to load marks/make up marks
            EMEnt = new EXAM_MARKS();
            EMEnt.STUDENT_ID = lblNCCSRegNo.Text;
            EMEnt.SEMESTER = lblSemesterID.Text;
            EMEnt.EXAM_TYPE = lblSCTID.Text;
            EMEnt.SUBJECT = lblSubjectID.Text;
            EMEnt = (EXAM_MARKS)EMSer.GetSingle(EMEnt);
            if (EMEnt != null && lblSCTID.Text != "")
            {
                lblSCTMarks.Text = EMEnt.MARKS;
                lblSCTMakeUp.Text = EMEnt.MAKEUP;
            }
            #endregion

            #endregion

            #region Mid Term
            Label lblMTID = e.Row.FindControl("lblMTID") as Label;
            Label lblFMMT = e.Row.FindControl("lblFMMT") as Label;
            Label lblMTMarks = e.Row.FindControl("lblMTMarks") as Label;
            Label lblMTMakeUp = e.Row.FindControl("lblMTMakeUp") as Label;

            #region to load examtype PKID as per PROGRAM
            ETMEnt = new EXAM_TYPE_MASTER();
            ETMEnt.PKID = examtypeMaster4;
            ETMEnt = (EXAM_TYPE_MASTER)ETMSer.GetSingle(ETMEnt);
            if (ETMEnt != null)
            {
                ETEnt = new EXAM_TYPE();
                ETEnt.PROGRAM = lblProgramID.Text;
                ETEnt.EXAM_TYPE_MASTERID = ETMEnt.PKID;
                ETEnt.STATUS = "1";
                ETEnt = (EXAM_TYPE)ETSer.GetSingle(ETEnt);
                if (ETEnt != null)
                {
                    lblMTID.Text = ETEnt.PKID;
                }
            }
            #endregion

            #region to load FULL_PASS marks
            FPMEnt = new full_pass_marks();
            FPMEnt.SUBJECT_ID = lblSubjectID.Text;
            FPMEnt.EXAM_TYPE = lblMTID.Text;
            FPMEnt = (full_pass_marks)FPMSer.GetSingle(FPMEnt);
            if (FPMEnt != null && lblMTID.Text != "")
            {
                lblFMMT.Text = FPMEnt.FULLMARKS_THRCL + "<br />" + "PM:" + FPMEnt.PASSMARKS_THRCL;
            }
            #endregion

            #region to load marks/make up marks
            EMEnt = new EXAM_MARKS();
            EMEnt.STUDENT_ID = lblNCCSRegNo.Text;
            EMEnt.SEMESTER = lblSemesterID.Text;
            EMEnt.EXAM_TYPE = lblMTID.Text;
            EMEnt.SUBJECT = lblSubjectID.Text;
            EMEnt = (EXAM_MARKS)EMSer.GetSingle(EMEnt);
            if (EMEnt != null && lblMTID.Text != "")
            {
                lblMTMarks.Text = EMEnt.MARKS;
                lblMTMakeUp.Text = EMEnt.MAKEUP;
            }
            #endregion

            #endregion

            #region Third Class Test
            Label lblTCTID = e.Row.FindControl("lblTCTID") as Label;
            Label lblFMTCT = e.Row.FindControl("lblFMTCT") as Label;
            Label lblTCTMarks = e.Row.FindControl("lblTCTMarks") as Label;
            Label lblTCTMakeUp = e.Row.FindControl("lblTCTMakeUp") as Label;

            #region to load examtype PKID as per PROGRAM
            ETMEnt = new EXAM_TYPE_MASTER();
            ETMEnt.PKID = examtypeMaster5;
            ETMEnt = (EXAM_TYPE_MASTER)ETMSer.GetSingle(ETMEnt);
            if (ETMEnt != null)
            {
                ETEnt = new EXAM_TYPE();
                ETEnt.PROGRAM = lblProgramID.Text;
                ETEnt.EXAM_TYPE_MASTERID = ETMEnt.PKID;
                ETEnt.STATUS = "1";
                ETEnt = (EXAM_TYPE)ETSer.GetSingle(ETEnt);
                if (ETEnt != null)
                {
                    lblTCTID.Text = ETEnt.PKID;
                }
            }
            #endregion

            #region to load FULL_PASS marks
            FPMEnt = new full_pass_marks();
            FPMEnt.SUBJECT_ID = lblSubjectID.Text;
            FPMEnt.EXAM_TYPE = lblTCTID.Text;
            FPMEnt = (full_pass_marks)FPMSer.GetSingle(FPMEnt);
            if (FPMEnt != null && lblTCTID.Text != "")
            {
                lblFMTCT.Text = FPMEnt.FULLMARKS_THRCL + "<br />" + "PM:" + FPMEnt.PASSMARKS_THRCL;
            }
            #endregion

            #region to load marks/make up marks
            EMEnt = new EXAM_MARKS();
            EMEnt.STUDENT_ID = lblNCCSRegNo.Text;
            EMEnt.SEMESTER = lblSemesterID.Text;
            EMEnt.EXAM_TYPE = lblTCTID.Text;
            EMEnt.SUBJECT = lblSubjectID.Text;
            EMEnt = (EXAM_MARKS)EMSer.GetSingle(EMEnt);
            if (EMEnt != null && lblTCTID.Text != "")
            {
                lblTCTMarks.Text = EMEnt.MARKS;
                lblTCTMakeUp.Text = EMEnt.MAKEUP;
            }
            #endregion

            #endregion

            #region Final Exam
            Label lblFEID = e.Row.FindControl("lblFEID") as Label;
            Label lblFMFE = e.Row.FindControl("lblFMFE") as Label;
            Label lblFEMarks = e.Row.FindControl("lblFEMarks") as Label;
            Label lblFEMakeUp = e.Row.FindControl("lblFEMakeUp") as Label;

            #region to load examtype PKID as per PROGRAM
            ETMEnt = new EXAM_TYPE_MASTER();
            ETMEnt.PKID = examtypeMaster6;
            ETMEnt = (EXAM_TYPE_MASTER)ETMSer.GetSingle(ETMEnt);
            if (ETMEnt != null)
            {
                ETEnt = new EXAM_TYPE();
                ETEnt.PROGRAM = lblProgramID.Text;
                ETEnt.EXAM_TYPE_MASTERID = ETMEnt.PKID;
                ETEnt.STATUS = "1";
                ETEnt = (EXAM_TYPE)ETSer.GetSingle(ETEnt);
                if (ETEnt != null)
                {
                    lblFEID.Text = ETEnt.PKID;
                }
            }
            #endregion

            #region to load FULL_PASS marks
            FPMEnt = new full_pass_marks();
            FPMEnt.SUBJECT_ID = lblSubjectID.Text;
            FPMEnt.EXAM_TYPE = lblFEID.Text;
            FPMEnt = (full_pass_marks)FPMSer.GetSingle(FPMEnt);
            if (FPMEnt != null && lblFEID.Text != "")
            {
                lblFMFE.Text = FPMEnt.FULLMARKS_THRCL + "<br />" + "PM:" + FPMEnt.PASSMARKS_THRCL;
            }
            #endregion

            #region to load marks/make up marks
            EMEnt = new EXAM_MARKS();
            EMEnt.STUDENT_ID = lblNCCSRegNo.Text;
            EMEnt.SEMESTER = lblSemesterID.Text;
            EMEnt.EXAM_TYPE = lblFEID.Text;
            EMEnt.SUBJECT = lblSubjectID.Text;
            EMEnt = (EXAM_MARKS)EMSer.GetSingle(EMEnt);
            if (EMEnt != null && lblFEID.Text != "")
            {
                lblFEMarks.Text = EMEnt.MARKS;
                lblFEMakeUp.Text = EMEnt.MAKEUP;
            }
            #endregion

            #endregion



            #region to load attendance
            DataTable dtable = new DataTable();
            dtable = hf.getSemesterAttendance(lblBatchYear.Text, lblSemesterID.Text, lblNCCSRegNo.Text);
            try
            {
                lblAtten.Text = (((Convert.ToDouble(dtable.Rows[0][2]) + Convert.ToDouble(dtable.Rows[0][4])) / Convert.ToDouble(dtable.Rows[0][5])) * 100).ToString("0.00") + "%";
            }
            catch (Exception)
            {
                lblAtten.Text = "0 %";
            }


            #endregion

            #region to load TU GPA
            Label lblGPA = e.Row.FindControl("lblGPA") as Label;
            GPAEnt = new SGPA_EIGHT();
            GPAEnt.STUDENT_ID = lblNCCSRegNo.Text;
            GPAEnt.SEMESTER = lblSemesterID.Text;
            GPAEnt.PROGRAM = lblProgramID.Text;
            GPAEnt.SUBJECT_ID = lblSubjectID.Text;
            GPAEnt = (SGPA_EIGHT)GPASer.GetSingle(GPAEnt);
            if (GPAEnt != null)
            {
                if (GPAEnt.POINTS == "0")
                {

                    lblGPA.Text = GPAEnt.GRADE;
                    if (GPAEnt.GRADE == "Partial")
                    {
                        if (GPAEnt.THEORY == "1")
                            lblGPA.Text += "-T";
                        if (GPAEnt.PRACTICAL == "1")
                            lblGPA.Text += "-P";
                    }
                }
                else
                    lblGPA.Text = GPAEnt.POINTS;
            }

            #endregion

            #region to merge  rows 

            for (int rowIndex = gridAcaHistory.Rows.Count - 2; rowIndex >= 0; rowIndex--)
            {
                GridViewRow gvRow = gridAcaHistory.Rows[rowIndex];
                GridViewRow gvPreviousRow = gridAcaHistory.Rows[rowIndex + 1];
                if (((Label)gvRow.Cells[1].FindControl("lblSemester")).Text == ((Label)gvPreviousRow.Cells[0].FindControl("lblSemester")).Text)
                {
                    if (gvPreviousRow.Cells[0].RowSpan < 2)
                    {
                        gvRow.Cells[0].RowSpan = 2;

                    }
                    else
                    {
                        gvRow.Cells[0].RowSpan = gvPreviousRow.Cells[0].RowSpan + 1;
                    }
                    gvPreviousRow.Cells[0].Visible = false;
                }

            }

            #endregion

        }
        #endregion
    }


    protected void btnPrint_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "printPartOfPage();", true);
    }

    protected void btnGoBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/exam/report/stdAcademicHistory.aspx?PrgID=" + lblProgramID.Text + "&Batch=" + lblBatchYear.Text);
    }

}