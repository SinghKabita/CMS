using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;

public partial class frontdesk_graduates_achievement : System.Web.UI.Page
{

    BatchYear BTEnt = new BatchYear();
    BatchYearService BTSer = new BatchYearService();

    GRADUATE_ACHIEVEMENT ACHEnt = new GRADUATE_ACHIEVEMENT();
    GRADUATE_ACHIEVEMENTService ACHSer = new GRADUATE_ACHIEVEMENTService();

    HSS_STUDENT SEnt = new HSS_STUDENT();
    HSS_STUDENTService SSer = new HSS_STUDENTService();

    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    HelperFunction hf = new HelperFunction();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFaculty();
            LoadProgram();
            LoadLevel();
            LoadBatch();
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
        

    }


    protected void LoadBatch()
    {
        //BTEnt = new BatchYear();
        //ddlBatch.DataSource = BTSer.GetAll(BTEnt);
        //ddlBatch.DataTextField = "BATCH";
        //ddlBatch.DataValueField = "BATCH";
        //ddlBatch.DataBind();
        //ddlBatch.Items.Insert(0, "Select");
        //ddlStudentId.Items.Insert(0, "Select");

        BTEnt = new BatchYear();
        BTEnt.ACTIVE = "1";
        BTEnt.PROGRAM = ddlProgram.SelectedValue;
        ddlBatch.DataSource = BTSer.GetAll(BTEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();

    }
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadStudent();
    }

    protected void LoadStudent()
    {
        if (ddlBatch.SelectedValue != "Select")
        {
            ddlStudentId.DataSource = hf.getStudentInfo(ddlBatch.SelectedValue, "");
            ddlStudentId.DataTextField = "name";
            ddlStudentId.DataValueField = "STUDENT_ID";
            ddlStudentId.DataBind();
            ddlStudentId.Items.Insert(0, "Select");
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlBatch.SelectedValue == "Select")
        {
            HelperFunction.MsgBox(this, this.GetType(), "Please Select Batch");
        }
        else if (ddlStudentId.SelectedValue == "Select" || ddlStudentId.SelectedValue == "")
        {
            HelperFunction.MsgBox(this, this.GetType(), "Please Select Student");
        }
        else if (txtAchievementDate.Text == "")
        {
            HelperFunction.MsgBox(this, this.GetType(), "Please Enter Achievement Date");
        }
        else if (txtAchievement.Text == "")
        {
            HelperFunction.MsgBox(this, this.GetType(), "Please Enter Achievement");
        }
        else
        {
            if (lblPKIDU.Text == "")
            {
                try
                {
                    ACHEnt = new GRADUATE_ACHIEVEMENT();
                    ACHEnt.BATCH = ddlBatch.SelectedValue;
                    ACHEnt.STUDENT_ID = ddlStudentId.SelectedValue;
                    ACHEnt.ACHIEVE_DATE = txtAchievementDate.Text;
                    string[] nepdate = hf.ConvertEnglishToNepali(txtAchievementDate.Text);
                    ACHEnt.ACHIEVE_DAY = nepdate[0];
                    ACHEnt.ACHIEVE_MONTH = nepdate[1];
                    ACHEnt.ACHIEVE_YEAR = nepdate[2];
                    ACHEnt.ACHIEVEMENTS = txtAchievement.Text;
                    ACHEnt.REMARKS = txtRemarks.Text;
                    if (ACHSer.Insert(ACHEnt) >= 1)
                    {
                        HelperFunction.MsgBox(this, this.GetType(), "Insert Successfull");
                        ClearFields();
                    }
                    else
                    {
                        HelperFunction.MsgBox(this, this.GetType(), "Sorry Not Successfull");
                    }
                }
                catch
                {
                    HelperFunction.MsgBox(this, this.GetType(), "Wrong Date Format");
                }

            }
            else
            {
                try
                {
                    ACHEnt = new GRADUATE_ACHIEVEMENT();
                    ACHEnt.PK_ID = lblPKIDU.Text;
                    ACHEnt = (GRADUATE_ACHIEVEMENT)ACHSer.GetSingle(ACHEnt);
                    if (ACHEnt != null)
                    {
                        ACHEnt.BATCH = ddlBatch.SelectedValue;
                        ACHEnt.STUDENT_ID = ddlStudentId.SelectedValue;
                        ACHEnt.ACHIEVE_DATE = txtAchievementDate.Text;
                        string[] nepdate = hf.ConvertEnglishToNepali(txtAchievementDate.Text);
                        ACHEnt.ACHIEVE_DAY = nepdate[0];
                        ACHEnt.ACHIEVE_MONTH = nepdate[1];
                        ACHEnt.ACHIEVE_YEAR = nepdate[2];
                        ACHEnt.ACHIEVEMENTS = txtAchievement.Text;
                        ACHEnt.REMARKS = txtRemarks.Text;
                        if (ACHSer.Update(ACHEnt) >= 1)
                        {
                            HelperFunction.MsgBox(this, this.GetType(), "Update Successfull");
                            ClearFields();
                        }
                        else
                        {
                            HelperFunction.MsgBox(this, this.GetType(), "Sorry Not Successfull");
                        }
                    }
                }
                catch
                {
                    HelperFunction.MsgBox(this, this.GetType(), "Wrong Date Format");
                }
            }
        }
    }

    protected void ClearFields()
    {
        txtAchievement.Text = "";
        txtAchievementDate.Text = "";
        lblPKIDU.Text = "";
        txtRemarks.Text = "";


        ddlStudentId.Items.Clear();
        LoadBatch();

        gridAchievement.DataSource = null;
        gridAchievement.DataBind();

    }

    protected void LoadData()
    {
        if (ddlStudentId.SelectedValue != "Select" && ddlBatch.SelectedValue != "Select")
        {
            ACHEnt = new GRADUATE_ACHIEVEMENT();
            ACHEnt.BATCH = ddlBatch.SelectedValue;
            ACHEnt.STUDENT_ID = ddlStudentId.SelectedValue;
            gridAchievement.DataSource = ACHSer.GetAll(ACHEnt);
            gridAchievement.DataBind();
        }
    }
    protected void ddlStudentId_SelectedIndexChanged(object sender, EventArgs e)
    {

        LoadData();

    }
    protected void gridAchievement_RowDataBound(object sender, GridViewRowEventArgs e)
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
    protected void gridAchievement_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Change"))
        {
            GridViewRow gr = ((ImageButton)e.CommandSource).Parent.Parent as GridViewRow;
            Label lblStudentId = gr.FindControl("lblStudentId") as Label;
            Label lblPKID = gr.FindControl("lblPKID") as Label;
            Label lblBatch = gr.FindControl("lblBatch") as Label;
            Label lblAchievementDate = gr.FindControl("lblAchievementDate") as Label;
            Label lblAchievement = gr.FindControl("lblAchievement") as Label;
            Label lblRemarks = gr.FindControl("lblRemarks") as Label;

            lblPKIDU.Text = lblPKID.Text;
            ddlBatch.SelectedValue = lblBatch.Text;
            LoadStudent();
            ddlStudentId.SelectedValue = lblStudentId.Text;
            txtAchievementDate.Text = lblAchievementDate.Text;
            txtAchievement.Text = lblAchievement.Text;
            txtRemarks.Text = lblRemarks.Text;

        }
    }
    protected void txtAchievementDate_TextChanged(object sender, EventArgs e)
    {
        if (txtAchievementDate.Text != "")
        {
            string[] nepdate = hf.ConvertEnglishToNepali(txtAchievementDate.Text);
            txtDay.Text = nepdate[0];
            txtMonth.Text = nepdate[1];
            txtYear.Text = nepdate[2];
        }
    }
    protected void txtDay_TextChanged(object sender, EventArgs e)
    {
        txtAchievementDate.Text = GetEnglishDate();
        txtMonth.Focus();
    }
    protected void txtMonth_TextChanged(object sender, EventArgs e)
    {
        txtAchievementDate.Text = GetEnglishDate();
        txtYear.Focus();
    }
    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        txtAchievementDate.Text = GetEnglishDate();
        txtAchievement.Focus();
    }

    protected string GetEnglishDate()
    {
        string engdate = "";
        if (txtDay.Text != "" && txtMonth.Text != "" && txtYear.Text != "")
        {
            try
            {
                engdate = hf.ConvertNepaliTOEnglish(txtDay.Text, txtMonth.Text, txtYear.Text);


            }
            catch { }
        }
        return engdate;
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
            LoadStudent();

        }
    }
}