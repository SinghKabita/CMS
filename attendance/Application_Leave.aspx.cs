using Entity.Components;
using Entity.Framework;
using Service.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class attendance_Application_Leave : System.Web.UI.Page
{
    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();


    BatchYear BTCEnt = new BatchYear();
    BatchYearService BTCSer = new BatchYearService();

    semester SMSEnt = new semester();
    semesterService SMSSer = new semesterService();

    Section SCEnt = new Section();
    SectionService SCSer = new SectionService();

    student_leave STLVEnt = new student_leave();
    student_leaveService STLVSer = new student_leaveService();

    scholorship_discount SCDEnt = new scholorship_discount();
    scholorship_discountService SCDSer = new scholorship_discountService();

    HSS_STUDENT STEnt = new HSS_STUDENT();
    HSS_STUDENTService STSer = new HSS_STUDENTService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    EntityList theList = new EntityList();
    EntityList newList = new EntityList();

    HelperFunction hf = new HelperFunction();

    UserProfileEntity userProfileEnt = new UserProfileEntity();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFaculty();
            LoadLevel();
            LoadProgram();
            LoadSection();

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
            SMSEnt = new semester();
            SMSEnt.PK_ID = by.SEMESTER;
            SMSEnt = (semester)SMSSer.GetSingle(SMSEnt);
            if (SMSEnt != null)
            {
                semList.Add(SMSEnt);
            }
        }
        #endregion

        ddlSemester.DataSource = semList;
        ddlSemester.DataTextField = "SEMESTER_CODE";
        ddlSemester.DataValueField = "PK_ID";
        ddlSemester.DataBind();
        ddlSemester.Items.Insert(0, "Select");
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

    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlBatch.SelectedValue != "Select")
        {
            LoadStudent();

        }
        //else
        //{
        //    ddlSemester.Items.Clear();
        //    ddlSemester.Items.Insert(0, "Select");

        //    ddlStudentId.Items.Clear();
        //    ddlStudentId.Items.Insert(0, "Select");
        //}

    }
    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSemester.SelectedValue != "Select")
        {
            LoadBatch();
        }
        else
        {


            ddlStudentId.Items.Clear();
            ddlStudentId.Items.Insert(0, "Select");
        }
    }
    protected void ddlSection_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlSection.SelectedValue != "Select")
        {
            LoadStudent();
        }
        //else {        
        //    ddlStudentId.Items.Clear();
        //    ddlStudentId.Items.Insert(0, "Select");
        //}
    }

    protected void LoadStudent()
    {
        ddlStudentId.DataSource = hf.selectstudentinfo(ddlProgram.SelectedValue, ddlBatch.SelectedValue, ddlSemester.SelectedValue, ddlSection.SelectedValue);
        ddlStudentId.DataTextField = "STUDENT_NAME";
        ddlStudentId.DataValueField = "STUDENT_ID";
        ddlStudentId.DataBind();
        ddlStudentId.Items.Insert(0, "Select");
        LoadGrid();

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

        userProfileEnt = new UserProfileEntity();

        userProfileEnt = (UserProfileEntity)HttpContext.Current.Session["UserProfile"];
        try
        {
            // double amount = Convert.ToDouble(txtAmount.Text);


            if (lblPKIDU.Text != "")
            {
                STLVEnt = new student_leave();
                STLVEnt.PK_ID = lblPKIDU.Text;
                STLVEnt = (student_leave)STLVSer.GetSingle(STLVEnt);
                if (STLVEnt != null)
                {

                    STLVEnt.SEMESTER_ID = ddlSemester.SelectedValue;
                    STLVEnt.STUDENT_ID = ddlStudentId.SelectedValue;
                    //  STLVEnt.SUBJECT_ID = ddlSubject.SelectedValue;
                    STLVEnt.APPLICATION_DATE = txtApplicationDate.Text;
                    try
                    {
                        string[] appdate = hf.ConvertEnglishToNepali(txtApplicationDate.Text);
                        STLVEnt.APPLICATION_DAY = appdate[0];
                        STLVEnt.APPLICATION_MONTH = appdate[1];
                        STLVEnt.APPLICATION_YEAR = appdate[2];
                    }
                    catch
                    {
                        txtApplicationDate.Focus();
                        HelperFunction.MsgBox(this, this.GetType(), "Wrong Date Format in Application Date");


                    }

                    STLVEnt.LEAVE_FROM_DATE = txtLeaveFromDate.Text;
                    try
                    {
                        string[] leavefromdate = hf.ConvertEnglishToNepali(txtLeaveFromDate.Text);
                        STLVEnt.LEAVE_FROM_DAY = leavefromdate[0];
                        STLVEnt.LEAVE_FROM_MONTH = leavefromdate[1];
                        STLVEnt.LEAVE_FROM_YEAR = leavefromdate[2];
                    }
                    catch
                    {
                        txtLeaveFromDate.Focus();
                        HelperFunction.MsgBox(this, this.GetType(), "Wrong Date Format in Leave From Date");
                    }

                    STLVEnt.LEAVE_TO_DATE = txtLeaveToDate.Text;

                    try
                    {
                        string[] leavetodate = hf.ConvertEnglishToNepali(txtLeaveToDate.Text);
                        STLVEnt.LEAVE_TO_DAY = leavetodate[0];
                        STLVEnt.LEAVE_TO_MONTH = leavetodate[1];
                        STLVEnt.LEAVE_TO_YEAR = leavetodate[2];
                    }
                    catch
                    {
                        txtLeaveToDate.Focus();
                        HelperFunction.MsgBox(this, this.GetType(), "Wrong Date Format in Leave To Date");


                    }

                    STLVEnt.DESCRIPTION = txtDescription.Text;
                    STLVEnt.APPROVED_BY = txtApprovedBy.Text;
                    string[] days = lblNoofDays.Text.Split(' ');
                    STLVEnt.NO_OF_DAYS = days[0];
                    STLVEnt.ENTRY_BY = userProfileEnt.UserName;


                    if (STLVSer.Update(STLVEnt) >= 1)
                    {


                        HelperFunction.MsgBox(this, this.GetType(), "Successfully Updated");
                        LoadGrid();
                        clearFields();
                    }
                    else
                    {
                        HelperFunction.MsgBox(this, this.GetType(), "Something Goes Wrong");
                    }
                }
            }
            else
            {

                STLVEnt = new student_leave();
                STLVEnt.SEMESTER_ID = ddlSemester.SelectedValue;
                STLVEnt.STUDENT_ID = ddlStudentId.SelectedValue;
                // STLVEnt.SUBJECT_ID = ddlSubject.SelectedValue;
                STLVEnt.APPLICATION_DATE = txtApplicationDate.Text;
                try
                {
                    string[] appdate = hf.ConvertEnglishToNepali(txtApplicationDate.Text);
                    STLVEnt.APPLICATION_DAY = appdate[0];
                    STLVEnt.APPLICATION_MONTH = appdate[1];
                    STLVEnt.APPLICATION_YEAR = appdate[2];
                }
                catch
                {
                    txtApplicationDate.Focus();
                    HelperFunction.MsgBox(this, this.GetType(), "Wrong Date Format in Application Date");


                }

                STLVEnt.LEAVE_FROM_DATE = txtLeaveFromDate.Text;
                try
                {

                    string[] leavefromdate = hf.ConvertEnglishToNepali(txtLeaveFromDate.Text);
                    STLVEnt.LEAVE_FROM_DAY = leavefromdate[0];
                    STLVEnt.LEAVE_FROM_MONTH = leavefromdate[1];
                    STLVEnt.LEAVE_FROM_YEAR = leavefromdate[2];
                }
                catch
                {
                    txtLeaveFromDate.Focus();
                    HelperFunction.MsgBox(this, this.GetType(), "Wrong Date Format in Leave From Date");


                }


                STLVEnt.LEAVE_TO_DATE = txtLeaveToDate.Text;
                try
                {
                    string[] leavetodate = hf.ConvertEnglishToNepali(txtLeaveToDate.Text);
                    STLVEnt.LEAVE_TO_DAY = leavetodate[0];
                    STLVEnt.LEAVE_TO_MONTH = leavetodate[1];
                    STLVEnt.LEAVE_TO_YEAR = leavetodate[2];
                }
                catch
                {
                    txtLeaveToDate.Focus();
                    HelperFunction.MsgBox(this, this.GetType(), "Wrong Date Format in Leave To Date");


                }


                STLVEnt.DESCRIPTION = txtDescription.Text;
                STLVEnt.APPROVED_BY = txtApprovedBy.Text;
                string[] days = lblNoofDays.Text.Split(' ');
                STLVEnt.NO_OF_DAYS = days[0];


                STLVEnt.ENTRY_BY = userProfileEnt.UserName;

                if (STLVSer.Insert(STLVEnt) > 0)
                {
                    HelperFunction.MsgBox(this, this.GetType(), "Successfully Inserted");
                    LoadGrid();
                    clearFields();
                }
                else
                {
                    HelperFunction.MsgBox(this, this.GetType(), "Something Goes Wrong");
                }
            }
        }
        catch
        {
            HelperFunction.MsgBox(this, this.GetType(), "Amount not Correct Format");

        }

    }
    protected void ddlStudentId_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();
    }

    protected void LoadGrid()
    {
        STLVEnt = new student_leave();
        STLVEnt.SEMESTER_ID = ddlSemester.SelectedValue;
        STLVEnt.STUDENT_ID = ddlStudentId.SelectedValue;
        gridStudentLeave.DataSource = STLVSer.GetAll(STLVEnt);
        gridStudentLeave.DataBind();
    }


    protected void clearFields()
    {
        lblPKIDU.Text = "";
        ddlBatch.SelectedIndex = 0;
        ddlSemester.SelectedIndex = 0;
        ddlSection.SelectedIndex = 0;
        // ddlSubject.SelectedIndex = 0;
        ddlStudentId.SelectedIndex = 0;

        txtDescription.Text = "";
        txtApprovedBy.Text = "";
        txtApplicationDate.Text = "";
        txtLeaveFromDate.Text = "";
        txtLeaveToDate.Text = "";
        lblNoofDays.Text = "";


    }
    protected void txtLeaveFromDate_TextChanged(object sender, EventArgs e)
    {
        CalculateDays();
    }
    protected void txtLeaveToDate_TextChanged(object sender, EventArgs e)
    {
        CalculateDays();
    }

    protected void CalculateDays()
    {
        if (txtLeaveFromDate.Text != "" && txtLeaveToDate.Text != "")
        {
            try
            {
                DateTime datefrom = DateTime.ParseExact(txtLeaveFromDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);
                DateTime dateto = DateTime.ParseExact(txtLeaveToDate.Text, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);

                TimeSpan ts = dateto.Subtract(datefrom);

                if (ts.Days + 1 == 1)
                {
                    lblNoofDays.Text = (ts.Days + 1).ToString() + " Day";
                }
                else if (ts.Days + 1 > 1)
                {
                    lblNoofDays.Text = (ts.Days + 1).ToString() + " Days";
                }
                else if (ts.Days + 1 <= 0)
                {
                    HelperFunction.MsgBox(this, this.GetType(), "Wrong Date Entry");
                    txtLeaveFromDate.Text = "";
                    txtLeaveToDate.Text = "";
                    lblNoofDays.Text = "";
                    txtLeaveFromDate.Focus();
                }

            }
            catch
            {
                HelperFunction.MsgBox(this, this.GetType(), "Wrong Date Entry");
            }
        }
    }
    protected void gridStudentLeave_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void gridStudentLeave_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Change"))
        {
            GridViewRow gr = ((ImageButton)e.CommandSource).Parent.Parent as GridViewRow;

            Label lblPKID = gr.FindControl("lblPKID") as Label;
            Label lblSemester = gr.FindControl("lblSemester") as Label;
            Label lblStudentId = gr.FindControl("lblStudentId") as Label;

            Label lblApplicationDate = gr.FindControl("lblApplicationDate") as Label;
            Label lblLeaveFromDate = gr.FindControl("lblLeaveFromDate") as Label;
            Label lblLeaveToDate = gr.FindControl("lblLeaveToDate") as Label;
            Label lblDescription = gr.FindControl("lblDescription") as Label;
            Label lblApprovedBy = gr.FindControl("lblApprovedBy") as Label;
            Label lblNoOfDays = gr.FindControl("lblNoOfDays") as Label;


            lblPKIDU.Text = lblPKID.Text;
            ddlSemester.SelectedValue = lblSemester.Text;
            ddlStudentId.SelectedValue = lblStudentId.Text;

            txtApplicationDate.Text = lblApplicationDate.Text;
            txtLeaveFromDate.Text = lblLeaveFromDate.Text;
            txtLeaveToDate.Text = lblLeaveToDate.Text;
            txtApprovedBy.Text = lblApprovedBy.Text;
            lblNoofDays.Text = lblNoOfDays.Text + " Days";
            txtDescription.Text = lblDescription.Text;
        }
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();

    }
    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlProgram.SelectedValue != "Select")
        {
            LoadSemester();
        }
    }

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
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

            ddlStudentId.Items.Clear();
            ddlStudentId.Items.Insert(0, "Select");
        }
    }
}