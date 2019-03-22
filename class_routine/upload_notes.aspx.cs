using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;
using Entity.Framework;

public partial class class_routine_upload_notes : System.Web.UI.Page
{
    HSS_LEVEL LVLEnt = new HSS_LEVEL();
    HSS_LEVELService LVLSer = new HSS_LEVELService();

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    BatchYear BTCEnt = new BatchYear();
    BatchYearService BTCSer = new BatchYearService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    HSS_SUBJECT SUBEnt = new HSS_SUBJECT();
    HSS_SUBJECTService SUBSer = new HSS_SUBJECTService();

    notes_detail NDEnt = new notes_detail();
    notes_detailService NDSer = new notes_detailService();

    EntityList theList = new EntityList();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadLevel();
            LoadFaculty();
            LoadProgram();
        }
    }

    protected void loadLevel()
    {
        LVLEnt = new HSS_LEVEL();
        ddlLevel.DataSource = LVLSer.GetAll(LVLEnt);
        ddlLevel.DataTextField = "LEVEL_NAME";
        ddlLevel.DataValueField = "LEVEL_NAME";
        ddlLevel.DataBind();
        
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
        ddlBatch.Items.Insert(0, "Select");
        ddlSemester.Items.Insert(0, "Select");
        ddlSubject.Items.Insert(0, "Select");

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
        ddlBatch.Items.Insert(0, "Select");
        ddlSemester.Items.Insert(0, "Select");
        ddlSubject.Items.Insert(0, "Select");

    }

    protected void LoadBatch()
    {
        BTCEnt = new BatchYear();
        BTCEnt.ACTIVE = "1";
        BTCEnt.SEMESTER = ddlSemester.SelectedValue;
        ddlBatch.DataSource = BTCSer.GetAll(BTCEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
    }

    protected void LoadSemester()
    {
        theList = new EntityList();
        EntityList semList = new EntityList();
        BTCEnt = new BatchYear();
        BTCEnt.ACTIVE = "1";
        BTCEnt.PROGRAM = ddlProgram.SelectedValue;

        theList = BTCSer.GetAll(BTCEnt);
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
        BTCEnt = new BatchYear();
        BTCEnt.BATCH = ddlBatch.SelectedValue;
        BTCEnt = (BatchYear)BTCSer.GetSingle(BTCEnt);
        if (BTCEnt != null)
        {
            SUBEnt = new HSS_SUBJECT();
            SUBEnt.SEMESTER = ddlSemester.SelectedValue;
            SUBEnt.PROGRAM = ddlProgram.SelectedValue;
            SUBEnt.YEAR = BTCEnt.SYLLABUS_YEAR;
            SUBEnt.STATUS = "1";
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
            LoadBatch();
            LoadSubject();
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


            string SaveLocation = Server.MapPath("~/Notes/") + fn;

            try
            {
                fileName.PostedFile.SaveAs(SaveLocation);

                NDEnt = new notes_detail();
                NDEnt.PROGRAMID = ddlProgram.SelectedValue;
                NDEnt.SEMESTERID = ddlSemester.SelectedValue;
                NDEnt.SUBJECT_ID = ddlSubject.SelectedValue;
                NDEnt.TOPIC = txtTopic.Text;
                NDEnt.UPLOAD_DATE = txtUploadDate.Text;
                NDEnt.LINK_NAME = SaveLocation;
                if (NDSer.Insert(NDEnt) > 0)
                {

                    HelperFunction.MsgBox(this, this.GetType(), "Uploaded Successfully");
                }
                else
                {
                    HelperFunction.MsgBox(this, this.GetType(), "Something Goes Wrong");
                }

            }
            catch (Exception ex)
            {

            }
        }
        else
        {
            HelperFunction.MsgBox(this, this.GetType(), "Please Select File to Upload");
        }
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

            ddlBatch.Items.Clear();
            ddlBatch.Items.Insert(0, "Select");

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

        }
        else
        {
            ddlBatch.Items.Clear();
            ddlBatch.Items.Insert(0, "Select");

            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Select");

            ddlSubject.Items.Clear();
            ddlSubject.Items.Insert(0, "Select");


        }
    }

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }
}