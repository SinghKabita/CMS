using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Entity.Framework;
using Service.Components;
using DataHelper.Framework;

public partial class finance_SMSReminder : System.Web.UI.Page
{
    BatchYear BYEnt = new BatchYear();
    BatchYearService BYSer = new BatchYearService();

    HSS_STUDENT SEnt = new HSS_STUDENT();
    HSS_STUDENTService SSer = new HSS_STUDENTService();

    sms_record SMSEnt = new sms_record();
    sms_recordService SMSSer = new sms_recordService();

    program PEnt = new program();
    programService PSer = new programService();

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    HSS_LEVEL LVLEnt = new HSS_LEVEL();
    HSS_LEVELService LVLSer = new HSS_LEVELService();

    UserProfileEntity userProfileEnt = new UserProfileEntity();

    HelperFunction hf = new HelperFunction();

    DistributedTransaction DT = new DistributedTransaction();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadLevel();
            loadFaculty();
            loadProgram();
            loadBatch();
        }
    }

    protected void loadLevel()
    {
        LVLEnt = new HSS_LEVEL();
        ddlLevel.DataSource = LVLSer.GetAll(LVLEnt);
        ddlLevel.DataTextField = "LEVEL_NAME";
        ddlLevel.DataValueField = "PK_ID";
        ddlLevel.DataBind();
    }

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {

    }



    protected void loadFaculty()
    {
        FCEnt = new hss_faculty();
        ddlFaculty.DataSource = FCSer.GetAll(FCEnt);
        ddlFaculty.DataTextField = "FACULTY";
        ddlFaculty.DataValueField = "PK_ID";
        ddlFaculty.DataBind();
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadProgram();

    }

    protected void loadProgram()
    {
        PEnt = new program();
        PEnt.FACULTY_ID = ddlFaculty.SelectedValue;
        ddlProgram.DataSource = PSer.GetAll(PEnt);
        ddlProgram.DataTextField = "PROGRAM_CODE";
        ddlProgram.DataValueField = "PK_ID";
        ddlProgram.DataBind();

    }

    protected void loadBatch()
    {
        BYEnt = new BatchYear();
        BYEnt.ACTIVE = "1";
        BYEnt.PROGRAM = ddlProgram.SelectedValue;
        ddlBatch.DataSource = BYSer.GetAll(BYEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();

    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadBatch();
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        gridRemainingBalance.DataSource = hf.getremainingbalofallstudent(ddlBatch.SelectedValue,ddlProgram.SelectedValue);
        gridRemainingBalance.DataBind();

    }
    protected void gridRemainingBalance_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblStudentId = e.Row.FindControl("lblStudentId") as Label;
            Label lblStudentName = e.Row.FindControl("lblStudentName") as Label;

            SEnt = new HSS_STUDENT();
            SEnt.STUDENT_ID = lblStudentId.Text;
            SEnt = (HSS_STUDENT)SSer.GetSingle(SEnt);
            if (SEnt != null)
            {
                lblStudentName.Text = SEnt.NAME_ENGLISH;
            }


        }
    }
    protected void btnSend_Click(object sender, EventArgs e)
    {

        foreach (GridViewRow gr in gridRemainingBalance.Rows)
        {
            Label lblStudentId = gr.FindControl("lblStudentId") as Label;
            Label lblStudentName = gr.FindControl("lblStudentName") as Label;
            Label lblAmount = gr.FindControl("lblAmount") as Label;

            gridSmsNoList.DataSource = hf.getcontactofStudent(lblStudentId.Text, "", ddlBatch.SelectedValue, rbtnGroup.SelectedValue);
            gridSmsNoList.DataBind();

            string smsnumber = "";
            string message = "";
            if (gridSmsNoList.Rows.Count > 0)
            {
                foreach (GridViewRow row in gridSmsNoList.Rows)
                {
                    Label lblSMSNo = row.FindControl("lblSMSNo") as Label;

                    smsnumber += lblSMSNo.Text + ",";
                }

                smsnumber = smsnumber.Substring(0, smsnumber.Length - 1);

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
                    userProfileEnt = (UserProfileEntity)HttpContext.Current.Session["UserProfile"];
                    SMSEnt.SEND_BY = userProfileEnt.UserName;
                    SMSEnt.SEND_MODULE = "Account->Reminder";

                    SMSSer.Insert(SMSEnt);


                }
                else
                {
                    HelperFunction.MsgBox(this, this.GetType(), "Send SMS Failed");
                }



            }

        }

        HelperFunction.MsgBox(this, this.GetType(), "Send SMS Success.");



    }
}