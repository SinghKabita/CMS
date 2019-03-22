using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;
using Entity.Framework;

public partial class administration_reports_Scholorship_Record : System.Web.UI.Page
{

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    program PEnt = new program();
    programService PSer = new programService();

    BatchYear BTEnt = new BatchYear();
    BatchYearService BTSer = new BatchYearService();

    BatchYear BTCEnt = new BatchYear();
    BatchYearService BTCSer = new BatchYearService();

    semester SMSEnt = new semester();
    semesterService SMSSer = new semesterService();

    Section SCEnt = new Section();
    SectionService SCSer = new SectionService();

    scholorship_discount SCDEnt = new scholorship_discount();
    scholorship_discountService SCDSer = new scholorship_discountService();

    HSS_STUDENT STEnt = new HSS_STUDENT();
    HSS_STUDENTService STSer = new HSS_STUDENTService();

    EntityList theList = new EntityList();

    HelperFunction hf = new HelperFunction();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFaculty();
            LoadProgram();
            LoadLevel();
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
        ddlBatch.Items.Insert(0, "Select");
        ddlSemester.Items.Insert(0, "Select");

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
        ddlBatch.Items.Insert(0, "Select");
        ddlSemester.Items.Insert(0, "Select");

    }

    protected void LoadBatch()
    {
        BTCEnt = new BatchYear();
        BTCEnt.ACTIVE = "1";
        BTCEnt.PROGRAM = ddlProgram.SelectedValue;
        ddlBatch.DataSource = BTCSer.GetAll(BTCEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
        ddlBatch.Items.Insert(0, "Select");
    }

    protected void LoadSemester()
    {
        theList = new EntityList();
        EntityList semList = new EntityList();
        BTCEnt = new BatchYear();
        BTCEnt.ACTIVE = "1";
        BTCEnt.PROGRAM = ddlProgram.SelectedValue;
        BTCEnt.BATCH = ddlBatch.SelectedValue;
        BTCEnt = (BatchYear)BTCSer.GetSingle(BTCEnt);
        if (BTCEnt != null)
        {
            SMSEnt = new semester();
            SMSEnt.PROGRAM_ID = BTCEnt.PROGRAM;
            SMSEnt.SYLLABUS_YEAR = BTCEnt.SYLLABUS_YEAR;
            theList = SMSSer.GetAll(SMSEnt);

            foreach (semester sem in theList)
            {
                semList.Add(sem);
            }
        }

        ddlSemester.DataSource = semList;
        ddlSemester.DataTextField = "SEMESTER_CODE";
        ddlSemester.DataValueField = "PK_ID";
        ddlSemester.DataBind();
        ddlSemester.Items.Insert(0, "Select");

    }

    protected void LoadGrid()
    {
        EntityList theList = new EntityList();
        EntityList studentList = new EntityList();

        SCDEnt = new scholorship_discount();
        SCDEnt.SEMESTER_ID = ddlSemester.SelectedValue;
        theList = SCDSer.GetAll(SCDEnt);

        foreach (scholorship_discount sd in theList)
        {
            STEnt = new HSS_STUDENT();
            STEnt.STUDENT_ID = sd.STUDENT_ID;
            STEnt.BAT_CH = ddlBatch.SelectedValue;
            STEnt = (HSS_STUDENT)STSer.GetSingle(STEnt);
            if (STEnt != null)
            {
                studentList.Add(sd);
            }
        }

        gridScholorship.DataSource = studentList;
        gridScholorship.DataBind();
    }

    protected void gridScholorship_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStudentId = e.Row.FindControl("lblStudentId") as Label;
            Label lblStudentName = e.Row.FindControl("lblStudentName") as Label;

            STEnt = new HSS_STUDENT();
            STEnt.STUDENT_ID = lblStudentId.Text;
            STEnt = (HSS_STUDENT)STSer.GetSingle(STEnt);
            if (STEnt != null)
            {
                lblStudentName.Text = STEnt.NAME_ENGLISH;
            }

        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadGrid();
    }
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSemester();
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
        }
    }
    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProgram.SelectedValue != "Select")
        {
            LoadBatch();
        }
    }

}