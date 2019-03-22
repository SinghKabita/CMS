using Entity.Components;
using Entity.Framework;
using Service.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class administration_PERIOD : System.Web.UI.Page
{
    hss_faculty FEnt = new hss_faculty();
    hss_facultyService FSer = new hss_facultyService();

    HSS_LEVEL LVLEnt = new HSS_LEVEL();
    HSS_LEVELService LVLSer = new HSS_LEVELService();

    program PEnt = new program();
    programService PSer = new programService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    Section SCEnt = new Section();
    SectionService SCSer = new SectionService();

    PERIOD PEREnt = new PERIOD();
    PERIODService PERSer = new PERIODService();

    HelperFunction hf = new HelperFunction();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadFaculty();
            loadLevel();
            loadFacultyG();
            loadLevelG();

            loadProgram();
            LoadSection();

        }
    }

    protected void loadFaculty()
    {
        FEnt = new hss_faculty();
        ddlFaculty.DataSource = FSer.GetAll(FEnt);
        ddlFaculty.DataTextField = "FACULTY";
        ddlFaculty.DataValueField = "PK_ID";
        ddlFaculty.DataBind();
        ddlFaculty.Items.Insert(0, "Select");
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadProgram();
    }

    protected void loadLevel()
    {
        LVLEnt = new HSS_LEVEL();
        ddlLevel.DataSource = LVLSer.GetAll(LVLEnt);
        ddlLevel.DataTextField = "LEVEL_NAME";
        ddlLevel.DataValueField = "LEVEL_NAME";
        ddlLevel.DataBind();

    }

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadProgram();
    }

    protected void LoadSection()
    {
        SCEnt = new Section();
        ddlSection.DataSource = SCSer.GetAll(SCEnt);
        ddlSection.DataTextField = "SECTION";
        ddlSection.DataValueField = "SECTION";
        ddlSection.DataBind();
        ddlSection.Items.Insert(0, "Select");
    }

    protected void loadProgram()
    {
        PEnt = new program();
        PEnt.FACULTY_ID = ddlFaculty.SelectedValue;
        PEnt.PROGRAM_LEVEL = ddlLevel.SelectedValue;
        ddlProgram.DataSource = PSer.GetAll(PEnt);
        ddlProgram.DataTextField = "PROGRAM_CODE";
        ddlProgram.DataValueField = "PK_ID";
        ddlProgram.DataBind();
        ddlProgram.Items.Insert(0, "Select");
    }

    protected void LoadSemester()
    {

        SMEnt = new semester();
        SMEnt.PROGRAM_ID = ddlProgram.SelectedValue;
        SMEnt.SYLLABUS_YEAR = hf.getSyllabusYear(ddlProgram.SelectedValue, hf.NepaliYear());
        ddlSemester.DataSource = SMSer.GetAll(SMEnt);
        ddlSemester.DataTextField = "SEMESTER_CODE";
        ddlSemester.DataValueField = "PK_ID";
        ddlSemester.DataBind();
        ddlSemester.Items.Insert(0, "Select");
    }

    protected void loadFacultyG()
    {
        FEnt = new hss_faculty();
        ddlFacultyG.DataSource = FSer.GetAll(FEnt);
        ddlFacultyG.DataTextField = "FACULTY";
        ddlFacultyG.DataValueField = "PK_ID";
        ddlFacultyG.DataBind();
        ddlFacultyG.Items.Insert(0, "Select");
    }

    protected void ddlFacultyG_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadProgramG();
    }

    protected void loadLevelG()
    {
        LVLEnt = new HSS_LEVEL();
        ddlLevelG.DataSource = LVLSer.GetAll(LVLEnt);
        ddlLevelG.DataTextField = "LEVEL_NAME";
        ddlLevelG.DataValueField = "LEVEL_NAME";
        ddlLevelG.DataBind();

    }

    protected void ddlLevelG_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadProgramG();
    }

    protected void LoadSectionG()
    {
        SCEnt = new Section();
        ddlSectionG.DataSource = SCSer.GetAll(SCEnt);
        ddlSectionG.DataTextField = "SECTION";
        ddlSectionG.DataValueField = "SECTION";
        ddlSectionG.DataBind();
        ddlSectionG.Items.Insert(0, "Select");
    }

    protected void loadProgramG()
    {
        PEnt = new program();
        PEnt.FACULTY_ID = ddlFacultyG.SelectedValue;
        PEnt.PROGRAM_LEVEL = ddlLevelG.SelectedValue;
        ddlProgramG.DataSource = PSer.GetAll(PEnt);
        ddlProgramG.DataTextField = "PROGRAM_CODE";
        ddlProgramG.DataValueField = "PK_ID";
        ddlProgramG.DataBind();
        ddlProgramG.Items.Insert(0, "Select");
    }

    protected void LoadSemesterG()
    {
        SMEnt = new semester();
        SMEnt.PROGRAM_ID = ddlProgramG.SelectedValue;
        SMEnt.SYLLABUS_YEAR = hf.getSyllabusYear(ddlProgramG.SelectedValue, hf.NepaliYear());
        ddlSemesterG.DataSource = SMSer.GetAll(SMEnt);
        ddlSemesterG.DataTextField = "SEMESTER_CODE";
        ddlSemesterG.DataValueField = "PK_ID";
        ddlSemesterG.DataBind();
        ddlSemesterG.Items.Insert(0, "Select");
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSemester();
    }

    protected void ddlProgramG_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSemesterG();
    }

    protected void ddlSemesterG_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSectionG();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (lblID.Text == "")
        {
            PEREnt = new PERIOD();
            PEREnt.PROGRAM_ID = ddlProgram.SelectedValue;
            PEREnt.SEMESTER_ID = ddlSemester.SelectedValue;
            PEREnt.SECTION_ID = ddlSection.SelectedValue;
            PEREnt.PERIODS = ddlPeriod.SelectedValue;
            PEREnt.TIME = ddlTimeFromHH.SelectedValue + ":" + ddlTimeFromMM.SelectedValue + " " + ddlTimeToHH.SelectedValue + ":" + ddlTimeToMM.SelectedValue;
            calculateTime();
            string[] classHour = lblTime.Text.Split('.');
            if (classHour[1] == "00")
            {
                PEREnt.CLASS_HOUR = classHour[0];
            }
            else
            {
                PEREnt.CLASS_HOUR = lblTime.Text;
            }
            if (PERSer.Insert(PEREnt) > 0)
            {
                //HelperFunction.MsgBox(this, this.GetType(), "Saved Successfully");
                loadGrid();
                clear();
            }
            else
            {
                HelperFunction.MsgBox(this, this.GetType(), "Unable to Save");
            }
        }
        else
        {
            PEREnt = new PERIOD();
            PEREnt.PK_ID = lblID.Text;
            PEREnt = (PERIOD)PERSer.GetSingle(PEREnt);
            if (PEREnt != null)
            {
                PEREnt.PROGRAM_ID = ddlProgram.SelectedValue;
                PEREnt.SEMESTER_ID = ddlSemester.SelectedValue;
                PEREnt.SECTION_ID = ddlSection.SelectedValue;
                PEREnt.PERIODS = ddlPeriod.SelectedValue;
                PEREnt.TIME = ddlTimeFromHH.SelectedValue + ":" + ddlTimeFromMM.SelectedValue + " " + ddlTimeToHH.SelectedValue + ":" + ddlTimeToMM.SelectedValue;
                calculateTime();
                string[] classHour = lblTime.Text.Split('.');
                if (classHour[1] == "00")
                {
                    PEREnt.CLASS_HOUR = classHour[0];
                }
                else
                {
                    PEREnt.CLASS_HOUR = lblTime.Text;
                }

                if (PERSer.Update(PEREnt) > 0)
                {
                    HelperFunction.MsgBox(this, this.GetType(), "Updated Successfully");
                    loadGrid();
                    clear();
                }
                else
                {
                    HelperFunction.MsgBox(this, this.GetType(), "Unable to Update");
                }
            }
        }
    }

    protected void calculateTime()
    {
        double timefromHH = 0.00;
        double timetoHH = 0.00;
        double fromTimeHHtoMin = 0.00;
        double toTimeHHtoMin = 0.00;
        double fromTimeTotal = 0.00;
        double toTimeTotal = 0.00;
        double resultINmin = 0.00;
        double resultINhr = 0.00;

        timefromHH = Convert.ToDouble(ddlTimeFromHH.SelectedValue);
        timetoHH = Convert.ToDouble(ddlTimeToHH.SelectedValue);
        fromTimeHHtoMin = timefromHH * 60;
        toTimeHHtoMin = timetoHH * 60;
        fromTimeTotal = fromTimeHHtoMin + Convert.ToDouble(ddlTimeFromMM.SelectedValue);
        toTimeTotal = toTimeHHtoMin + Convert.ToDouble(ddlTimeToMM.SelectedValue);

        resultINmin = toTimeTotal - fromTimeTotal;
        resultINhr = resultINmin / 60;
        lblTime.Text = resultINhr.ToString("0.00");
    }

    protected void loadGrid()
    {
        PEREnt = new PERIOD();
        PEREnt.PROGRAM_ID = ddlProgramG.SelectedValue;
        PEREnt.SEMESTER_ID = ddlSemesterG.SelectedValue;
        PEREnt.SECTION_ID = ddlSectionG.SelectedValue;
        gridPeriod.DataSource = PERSer.GetAll(PEREnt);
        gridPeriod.DataBind();
    }


    protected void ddlSectionG_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadGrid();
    }

    protected void gridPeriod_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("change"))
        {
            GridViewRow gr = ((ImageButton)e.CommandSource).Parent.Parent as GridViewRow;
            Label lblPK = gr.FindControl("lblPK") as Label;
            lblID.Text = lblPK.Text;
            PEREnt = new PERIOD();
            PEREnt.PK_ID = lblPK.Text;
            PEREnt = (PERIOD)PERSer.GetSingle(PEREnt);
            if (PEREnt != null)
            {
                ddlProgram.Enabled = false;
                ddlSemester.Enabled = false;
                ddlSection.Enabled = false;
                ddlProgram.SelectedValue = PEREnt.PROGRAM_ID;
                LoadSemester();
                ddlSemester.SelectedValue = PEREnt.SEMESTER_ID;
                ddlSection.SelectedValue = PEREnt.SECTION_ID;
                ddlPeriod.SelectedValue = PEREnt.PERIODS;

                string[] time = PEREnt.TIME.Split(' ');
                string[] fromtime = time[0].Split(':');
                ddlTimeFromHH.SelectedValue = fromtime[0];
                ddlTimeFromMM.SelectedValue = fromtime[1];

                string[] totime = time[1].Split(':');
                ddlTimeToHH.SelectedValue = totime[0];
                ddlTimeToMM.SelectedValue = totime[1];
            }
        }
    }

    protected void btnClear_Click(object sender, EventArgs e)
    {
        clear();
    }

    protected void clear()
    {
        lblID.Text = "";
        ddlTimeFromHH.SelectedIndex = 0;
        ddlTimeFromMM.SelectedIndex = 0;
        ddlTimeToHH.SelectedIndex = 0;
        ddlTimeToMM.SelectedIndex = 0;
    }

}