using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Entity.Components;
using Service.Components;
using DataHelper.Framework;
using Entity.Framework;

public partial class administration_DropStudent : System.Web.UI.Page
{
    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    program PEnt = new program();
    programService PSer = new programService();

    BatchYear BEnt = new BatchYear();
    BatchYearService BSer = new BatchYearService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    hss_section SCEnt = new hss_section();
    hss_sectionService SCSer = new hss_sectionService();

    HelperFunction hf = new HelperFunction();

    HSS_CURRENT_STUDENT CSEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSer = new HSS_CURRENT_STUDENTService();

    HSS_STUDENT STDEnt = new HSS_STUDENT();
    HSS_STUDENTService STDSrv = new HSS_STUDENTService();

    dropped_student DSEnt = new dropped_student();
    dropped_studentService DSSer = new dropped_studentService();

    UserProfileEntity userProfileEnt = new UserProfileEntity();

    EntityList theList = new EntityList();
    EntityList newList = new EntityList();

    DistributedTransaction DT = new DistributedTransaction();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadFaculty();
            //LoadProgram();
            //LoadSection();
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
        BEnt = new BatchYear();
        BEnt.ACTIVE = "1";
        BEnt.SEMESTER = ddlSemester.SelectedValue;
        ddlBatch.DataSource = BSer.GetAll(BEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
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


    protected void LoadSection()
    {
        SCEnt = new hss_section();
        ddlSection.DataSource = SCSer.GetAll(SCEnt);
        ddlSection.DataTextField = "SECTION";
        ddlSection.DataValueField = "SECTION";
        ddlSection.DataBind();
        ddlSection.Items.Insert(0, "Select");
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadData();
    }

    protected void LoadData()
    {
        griStudentList.DataSource = hf.selectstudentinfo(ddlProgram.SelectedValue, ddlBatch.SelectedValue, ddlSemester.SelectedValue, ddlSection.SelectedValue);
        griStudentList.DataBind();
    }


    protected void btnDrop_Click(object sender, EventArgs e)
    {
        string confirmValue = Request.Form["confirm_value"];
        if (confirmValue == "Yes")
        {
            GridViewRow gr = ((Button)sender).Parent.Parent as GridViewRow;
            Label lblStudentName = gr.FindControl("lblStudentName") as Label;
            Label lblStudentId = gr.FindControl("lblStudentId") as Label;

            CSEnt = new HSS_CURRENT_STUDENT();
            CSEnt.STUDENT_ID = lblStudentId.Text;
            CSEnt.BATCH = ddlBatch.SelectedValue;
            CSEnt.SEMESTER = ddlSemester.SelectedValue;
            CSEnt.SECTION = ddlSection.SelectedValue;
            CSEnt.YEAR = hf.CurrentYear(hf.NepaliMonth(), hf.NepaliYear());
            CSEnt.STATUS = "1";
            CSEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSEnt);
            if (CSEnt != null)
            {
                CSEnt.STATUS = "0";
                if (CSSer.Update(CSEnt) >= 1)
                {
                    HelperFunction.MsgBox(this, this.GetType(), lblStudentName.Text + "-" + lblStudentId.Text + " dropped Successfully");
                    LoadData();
                }
                else
                {
                    HelperFunction.MsgBox(this, this.GetType(), "Something Goes Wrong. Try Again Please");
                    LoadData();
                }
            }

        }
    }


    protected void griStudentList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Drop"))
        {
            GridViewRow gr = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            Label lblStudentId = gr.FindControl("lblStudentId") as Label;
            Label lblStudentName = gr.FindControl("lblStudentName") as Label;

            lblStudentIdP.Text = lblStudentName.Text + "-" + lblStudentId.Text;
            lblSemester.Text = ddlSemester.SelectedItem.ToString();

            //ScriptManager.RegisterStartupScript(this, this.GetType(), "", "popp();", true);
            divDrop.Visible = true;

        }
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        clearFields();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "popp();", true);
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        try
        {
            DT = new DistributedTransaction();
            CSEnt = new HSS_CURRENT_STUDENT();
            String[] stdname = lblStudentIdP.Text.Split('-');
            CSEnt.STUDENT_ID = stdname[1];
            CSEnt.BATCH = ddlBatch.SelectedValue;
            CSEnt.SEMESTER = ddlSemester.SelectedValue;
            CSEnt.SECTION = ddlSection.SelectedValue;

            CSEnt.STATUS = "1";
            CSEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSEnt, DT);
            if (CSEnt != null)
            {
                userProfileEnt = new UserProfileEntity();

                userProfileEnt = (UserProfileEntity)HttpContext.Current.Session["UserProfile"];
                DSEnt = new dropped_student();
                DSEnt.STUDENT_ID = stdname[1];
                DSEnt.SEMESTER_ID = ddlSemester.SelectedValue;
                DSEnt.DROP_DATE = txtDropDate.Text;
                string[] nepdate = hf.ConvertEnglishToNepali(txtDropDate.Text);
                DSEnt.DROP_DAY = nepdate[0];
                DSEnt.DROP_MONTH = nepdate[1];
                DSEnt.DROP_YEAR = nepdate[2];
                DSEnt.APPROVE_BY = txtApproveBy.Text;
                DSEnt.DESCRIPTION = txtReason.Text;
                DSEnt.FINCANCIAL_CLEARANCE_STATUS = rbtnFinanceClear.SelectedValue;
                DSEnt.USER_ID = userProfileEnt.UserName;

                DSSer.Insert(DSEnt, DT);

                CSEnt.STATUS = "0";
                CSSer.Update(CSEnt, DT);
            }

            STDEnt = new HSS_STUDENT();
            STDEnt.STUDENT_ID = stdname[1];
            STDEnt.BAT_CH = ddlBatch.SelectedValue;
            STDEnt.PROGRAM = ddlProgram.SelectedValue;
            STDEnt.STATUS = "1";
            STDEnt = (HSS_STUDENT)STDSrv.GetSingle(STDEnt);
            if (STDEnt != null)
            {
                STDEnt.STATUS = "0";
                STDSrv.Update(STDEnt, DT);
            }



            if (DT.HAPPY == true)
            {
                DT.Commit();
                HelperFunction.MsgBox(this, this.GetType(), "Dropped Successfully");
                LoadData();
                clearFields();
            }
            else
            {
                DT.Abort();
                HelperFunction.MsgBox(this, this.GetType(), "Something Goes Wrong. Try Again Please");
                LoadData();
                clearFields();
            }
            DT.Dispose();
            divDrop.Visible = false;
        }
        catch
        {
            HelperFunction.MsgBox(this, this.GetType(), "Something Goes Wrong. Try Again Please");
        }


    }
    protected void clearFields()
    {
        txtDropDate.Text = "";
        txtReason.Text = "";
        txtApproveBy.Text = "";
        txtDay.Text = "";
        txtMonth.Text = "";
        txtYear.Text = "";
        rbtnFinanceClear.SelectedIndex = 0;
    }
    //protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    LoadSemester();
    //}

    protected void txtDay_TextChanged(object sender, EventArgs e)
    {
        txtDropDate.Text = GetEnglishDate();
        txtMonth.Focus();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "popp();", true);
    }
    protected void txtMonth_TextChanged(object sender, EventArgs e)
    {
        txtDropDate.Text = GetEnglishDate();
        txtYear.Focus();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "popp();", true);
    }
    protected void txtYear_TextChanged(object sender, EventArgs e)
    {
        txtDropDate.Text = GetEnglishDate();
        txtApproveBy.Focus();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "popp();", true);
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


    protected void txtDropDate_TextChanged(object sender, EventArgs e)
    {
        if (txtDropDate.Text != "")
        {
            string[] nepdate = hf.ConvertEnglishToNepali(txtDropDate.Text);
            txtDay.Text = nepdate[0];
            txtMonth.Text = nepdate[1];
            txtYear.Text = nepdate[2];

        }
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "popp();", true);
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadLevel();
        LoadProgram();
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
        }
        //if (ddlBatch.SelectedValue == "Select")
        //{
        //    ddlSemester.Items.Clear();
        //    ddlSemester.Items.Insert(0, "Select");
        //}
    }

    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBatch();
        LoadSection();
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
        }
    }
}
