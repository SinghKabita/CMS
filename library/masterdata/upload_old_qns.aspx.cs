using Entity.Components;
using Entity.Framework;
using Service.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class library_masterdata_upload_old_qns : System.Web.UI.Page
{
    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    program PEnt = new program();
    programService PSer = new programService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    HSS_SUBJECT SUBEnt = new HSS_SUBJECT();
    HSS_SUBJECTService SUBSer = new HSS_SUBJECTService();

    old_question OLDEnt = new old_question();
    old_questionService OLDSer = new old_questionService();

    EXAM_TYPE ETYPEnt = new EXAM_TYPE();
    EXAM_TYPEService ETYPSer = new EXAM_TYPEService();

    EXAM_TYPE_MASTER ETMEnt = new EXAM_TYPE_MASTER();
    EXAM_TYPE_MASTERService ETMSer = new EXAM_TYPE_MASTERService();

    BatchYear BEnt = new BatchYear();
    BatchYearService BSer = new BatchYearService();

    HelperFunction hf = new HelperFunction();

    EntityList theList = new EntityList();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFaculty();
            LoadLevel();
            LoadProgram();
            LoadExamType();

        }



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
        ddlSemester.Items.Insert(0, "Select");
        ddlSubject.Items.Insert(0, "Select");

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
        ddlProgram.Items.Insert(0, "Select");
        ddlSemester.Items.Insert(0, "Select");

    }

    protected void LoadExamType()
    {
        EntityList ETList = new EntityList();
        EntityList newlist = new EntityList();
        ETYPEnt = new EXAM_TYPE();
        ETYPEnt.PROGRAM = ddlProgram.SelectedValue;
        ETYPEnt.STATUS = "1";
        ETList = ETYPSer.GetAll(ETYPEnt);
        if (ETList.Count > 0)
        {
            foreach (EXAM_TYPE et in ETList)
            {
                ETMEnt = new EXAM_TYPE_MASTER();
                ETMEnt.PKID = et.EXAM_TYPE_MASTERID;
                ETMEnt = (EXAM_TYPE_MASTER)ETMSer.GetSingle(ETMEnt);
                if (ETMEnt != null)
                    newlist.Add(ETMEnt);
            }
        }
        ddlExamType.DataSource = newlist;
        ddlExamType.DataTextField = "EXAM_TYPE";
        ddlExamType.DataValueField = "PKID";
        ddlExamType.DataBind();
        ddlExamType.Items.Insert(0, "Select");


    }

    protected void LoadSemester()
    {
        theList = new EntityList();
        EntityList semList = new EntityList();
        BEnt = new BatchYear();
        BEnt.ACTIVE = "1";
        BEnt.PROGRAM = ddlProgram.SelectedValue;

        theList = BSer.GetAll(BEnt);
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

        ddlSemester.DataSource = semList;
        ddlSemester.DataTextField = "SEMESTER_CODE";
        ddlSemester.DataValueField = "PK_ID";
        ddlSemester.DataBind();
        ddlSemester.Items.Insert(0, "Select");
    }

    protected void LoadSubject()
    {
        string syllYear = hf.getSyllabusYear(ddlProgram.SelectedValue, txtYear.Text).ToString();

        if (syllYear == "")
        {
            HelperFunction.MsgBox(this, this.GetType(), "Syllabus Year not found");
        }
        else
        {
            SUBEnt = new HSS_SUBJECT();
            SUBEnt.YEAR = syllYear;
            SUBEnt.SEMESTER = ddlSemester.SelectedValue;
            ddlSubject.DataSource = SUBSer.GetAll(SUBEnt);
            ddlSubject.DataTextField = "SUBJECT_NAME";
            ddlSubject.DataValueField = "PK_ID";
            ddlSubject.DataBind();
            ddlSubject.Items.Insert(0, "Select");
        }

    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedValue != "Select")
        {
            LoadSubject();
        }
        else
        {
            ddlSubject.Items.Clear();
            ddlSubject.Items.Insert(0, "Select");
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        if (fileName.HasFile)
        {
            string fn = fileName.FileName;


            string SaveLocation = Server.MapPath("~/OldQuestions/") + fn;

            try
            {
                fileName.PostedFile.SaveAs(SaveLocation);

                if (txtYear.Text != "" && txtTopic.Text != "")
                {
                    OLDEnt = new old_question();
                    OLDEnt.SUBJECT = ddlSubject.SelectedValue;
                    OLDEnt.EXAM_TYPE = ddlExamType.SelectedValue;
                    OLDEnt.SEMESTER = ddlSemester.SelectedValue;
                    OLDEnt.PROGRAMID = ddlProgram.SelectedValue;
                    OLDEnt.YEAR = txtYear.Text;
                    OLDEnt.TOPIC = txtTopic.Text;

                    OLDEnt.FILE_NAME = SaveLocation;
                    if (OLDSer.Insert(OLDEnt) > 0)
                    {

                        HelperFunction.MsgBox(this, this.GetType(), "Uploaded Successfully");

                        clearFields();
                    }
                    else
                    {
                        HelperFunction.MsgBox(this, this.GetType(), "Something Goes Wrong");
                    }

                }
                else
                {
                    HelperFunction.MsgBox(this, this.GetType(), "Please Complete All Fields");
                }



            }
            catch
            {

            }
        }
        else
        {
            HelperFunction.MsgBox(this, this.GetType(), "Please Select File to Upload");
        }
    }

    protected void clearFields()
    {
        txtYear.Text = "";
        ddlExamType.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlSubject.SelectedIndex = 0;
        ddlSubject.Items.Clear();

    }
    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFaculty.SelectedValue != "Select")
        {
            LoadProgram();
        }
        else
        {
            ddlProgram.Items.Clear();
            ddlProgram.Items.Insert(0, "Select");


        }
        if (ddlProgram.SelectedValue == "Select")
        {

            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Select");

            ddlSubject.Items.Clear();
            ddlSubject.Items.Insert(0, "Select");
        }

    }
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProgram.SelectedValue != "Select")
        {
            LoadSemester();
            LoadExamType();
        }
    }



    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        ddlFaculty.SelectedIndex = 0;
        ddlExamType.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlSubject.Items.Clear();
    }
    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }
}