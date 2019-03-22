using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;
using Entity.Framework;

public partial class library_library_DueBookList : System.Web.UI.Page
{
    HSS_LEVEL LVLEnt = new HSS_LEVEL();
    HSS_LEVELService LVLSer = new HSS_LEVELService();

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    sms_record SMSEnt = new sms_record();
    sms_recordService SMSSer = new sms_recordService();

    BatchYear BYEnt = new BatchYear();
    BatchYearService BYSer = new BatchYearService();

    program PEnt = new program();
    programService PSer = new programService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    UserProfileEntity userprofile = new UserProfileEntity();

    HelperFunction hf = new HelperFunction();
    int days;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFaculty();
            loadLevel();
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
        ddlBatch.Items.Insert(0, "Batch");
        ddlSemester.Items.Insert(0, "Select");
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        try
        {

            days = Convert.ToInt32(txtNumberOfDays.Text);
        }
        catch
        {

            HelperFunction.MsgBox(this, this.GetType(), "Please Enter Number Only");
        }
        string todaydate = System.DateTime.Today.ToString("dd/MM/yyyy");

        gridBookDue.DataSource = hf.DueBookList(ddlBatch.SelectedValue, ddlLevel.SelectedValue, ddlProgram.SelectedValue, ddlSemester.SelectedValue, todaydate, days);
        gridBookDue.DataBind();
        if (gridBookDue.Rows.Count == 0)
        {
            HelperFunction.MsgBox(this, this.GetType(), "Book Due Not found");
        }

    }

    protected void LoadBatch()
    {
        BYEnt = new BatchYear();
        BYEnt.ACTIVE = "1";
        BYEnt.SEMESTER = ddlSemester.SelectedValue;
        ddlBatch.DataSource = BYSer.GetAll(BYEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
    }
    protected void LoadProgram()
    {
        PEnt = new program();
        PEnt.PROGRAM_LEVEL = ddlLevel.SelectedValue;
        ddlProgram.DataSource = PSer.GetAll(PEnt);
        ddlProgram.DataTextField = "PROGRAM_CODE";
        ddlProgram.DataValueField = "PK_ID";
        ddlProgram.DataBind();
        ddlProgram.Items.Insert(0, "Select");
    }

    protected void LoadSemester()
    {
        EntityList theList = new EntityList();
        EntityList semList = new EntityList();
        BYEnt = new BatchYear();
        BYEnt.ACTIVE = "1";
        BYEnt.PROGRAM = ddlProgram.SelectedValue;

        theList = BYSer.GetAll(BYEnt);
        #region to get the active Semester
        foreach (BatchYear by in theList)
        {
            SMEnt = new semester();
            SMEnt.PK_ID = by.SEMESTER;
            SMEnt = (semester)SMSer.GetSingle(SMEnt);
            if (SMSEnt != null)
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

    protected void btnSendSMS_Click(object sender, EventArgs e)
    {
        Button btnSendSMS = (Button)sender;

        GridViewRow gr = (GridViewRow)btnSendSMS.NamingContainer;
        Label lblBatch = gr.FindControl("lblBatch") as Label;
        Label lblStudentId = gr.FindControl("lblStudentId") as Label;
        Label lblStudentName = gr.FindControl("lblStudentName") as Label;
        Label lblBookName = gr.FindControl("lblBookName") as Label;
        Label lblBookNumber = gr.FindControl("lblBookNumber") as Label;
        Label lblIssueDate = gr.FindControl("lblIssueDate") as Label;

        string smsnumber = hf.getcontactofStudentSingle(lblStudentId.Text, "", lblBatch.Text, "Primary");
        string message = "Please Return Book.";

        string status = hf.SendSms(smsnumber, message);
        if (status == "Success")
        {
            SMSEnt = new sms_record();
            SMSEnt.FACULTY = "1";
            SMSEnt.PROGRAM = "1";
            SMSEnt.SEMESTER = "";
            SMSEnt.SECTION = "";
            SMSEnt.MESSAGE = message;
            SMSEnt.PHONE_NUMBERS = smsnumber;
            SMSEnt.SEND_DATE = System.DateTime.Today.ToString("dd/MM/yyyy");
            userprofile = (UserProfileEntity)HttpContext.Current.Session["UserProfile"];
            SMSEnt.SEND_BY = userprofile.UserName;
            SMSEnt.SEND_MODULE = "Library";

            if (SMSSer.Insert(SMSEnt) >= 1)
            {
                HelperFunction.MsgBox(this, this.GetType(), "Send SMS Successfull");
            }


        }
        else
        {
            HelperFunction.MsgBox(this, this.GetType(), "Send SMS Failed");
        }


    }

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSemester();
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBatch();
    }
}