using Entity.Components;
using Service.Components;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class exam_report_stdAcademicHistory : System.Web.UI.Page
{
    hss_faculty FCEnt = new hss_faculty();
    hss_facultyService FCSer = new hss_facultyService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    program PEnt = new program();
    programService PSer = new programService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    BatchYear BTEnt = new BatchYear();
    BatchYearService BTSer = new BatchYearService();

    HSS_STUDENT STDEnt = new HSS_STUDENT();
    HSS_STUDENTService STDSer = new HSS_STUDENTService();

    HSS_EDUCATION_DETAIL EDDEnt = new HSS_EDUCATION_DETAIL();
    HSS_EDUCATION_DETAILService EDDSer = new HSS_EDUCATION_DETAILService();

    ADDRESS AEnt = new ADDRESS();
    ADDRESSService ASer = new ADDRESSService();

    string progID = "";
    string batch = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                progID = Request.QueryString["PrgID"].ToString();
                batch = Request.QueryString["Batch"].ToString();
            }
            catch (Exception)
            {

            }
            LoadFaculty();
            LoadLevel();
            LoadProgram();
            LoadBatch();
            loadGridStdHist();
        }
    }

    protected void LoadFaculty()
    {
        if (progID != "")
        {
            PEnt = new program();
            PEnt.PK_ID = progID;
            PEnt = (program)PSer.GetSingle(PEnt);
            if (PEnt != null)
            {
                FCEnt = new hss_faculty();
                ddlFaculty.DataSource = FCSer.GetAll(FCEnt);
                ddlFaculty.DataTextField = "FACULTY";
                ddlFaculty.DataValueField = "PK_ID";
                ddlFaculty.DataBind();
                ddlFaculty.SelectedValue = PEnt.FACULTY_ID;
            }

        }
        else
        {
            FCEnt = new hss_faculty();
            ddlFaculty.DataSource = FCSer.GetAll(FCEnt);
            ddlFaculty.DataTextField = "FACULTY";
            ddlFaculty.DataValueField = "PK_ID";
            ddlFaculty.DataBind();
            ddlFaculty.Items.Insert(0, "Select");
        }


    }

    protected void LoadLevel()
    {
        if (progID != "")
        {
            PEnt = new program();
            PEnt.PK_ID = progID;
            PEnt = (program)PSer.GetSingle(PEnt);
            if (PEnt != null)
            {
                LEnt = new HSS_LEVEL();
                ddlLevel.DataSource = LSrv.GetAll(LEnt);
                ddlLevel.DataTextField = "LEVEL_NAME";
                ddlLevel.DataValueField = "LEVEL_NAME";
                ddlLevel.DataBind();
                ddlLevel.SelectedValue = PEnt.PROGRAM_LEVEL;
            }
        }
        else
        {
            LEnt = new HSS_LEVEL();
            ddlLevel.DataSource = LSrv.GetAll(LEnt);
            ddlLevel.DataTextField = "LEVEL_NAME";
            ddlLevel.DataValueField = "LEVEL_NAME";
            ddlLevel.DataBind();
        }

    }

    protected void LoadProgram()
    {
        if (progID != "")
        {
            PEnt = new program();
            PEnt.FACULTY_ID = ddlFaculty.SelectedValue;
            PEnt.PROGRAM_LEVEL = ddlLevel.SelectedValue;
            ddlProgram.DataSource = PSer.GetAll(PEnt);
            ddlProgram.DataTextField = "PROGRAM_CODE";
            ddlProgram.DataValueField = "PK_ID";
            ddlProgram.DataBind();
            ddlProgram.SelectedValue = progID;
        }
        else
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


    }

    protected void LoadBatch()
    {
        if (batch != "")
        {
            BTEnt = new BatchYear();
            BTEnt.PROGRAM = ddlProgram.SelectedValue;
            BTEnt.ACTIVE = "1";
            ddlBatch.DataSource = BTSer.GetAll(BTEnt);
            ddlBatch.DataTextField = "BATCH";
            ddlBatch.DataValueField = "BATCH";
            ddlBatch.DataBind();
            ddlBatch.SelectedValue = batch;

        }
        else
        {
            BTEnt = new BatchYear();
            BTEnt.PROGRAM = ddlProgram.SelectedValue;
            BTEnt.ACTIVE = "1";
            ddlBatch.DataSource = BTSer.GetAll(BTEnt);
            ddlBatch.DataTextField = "BATCH";
            ddlBatch.DataValueField = "BATCH";
            ddlBatch.DataBind();
            ddlBatch.Items.Insert(0, "Select");
        }

    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFaculty.SelectedValue != "Select")
        {
            LoadProgram();
        }
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadBatch();
    }

    protected void loadGridStdHist()
    {
        if (batch != "" && progID != "")
        {
            STDEnt = new HSS_STUDENT();
            STDEnt.PROGRAM = progID;
            STDEnt.BAT_CH = batch;
            STDEnt.STATUS = "1";
            gridStdAcadHistory.DataSource = STDSer.GetAll(STDEnt);
            gridStdAcadHistory.DataBind();
        }
        else
        {
            STDEnt = new HSS_STUDENT();
            STDEnt.PROGRAM = ddlProgram.SelectedValue;
            STDEnt.BAT_CH = ddlBatch.SelectedValue;
            STDEnt.STATUS = "1";
            gridStdAcadHistory.DataSource = STDSer.GetAll(STDEnt);
            gridStdAcadHistory.DataBind();
        }

    }


    protected void btnView_Click(object sender, EventArgs e)
    {
        loadGridStdHist();
    }

    protected void gridStdAcadHistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Image imgStudent = e.Row.FindControl("imgStudent") as Image;
            Label lblpkid = e.Row.FindControl("lblpkid") as Label;
            Label lblStudentId = e.Row.FindControl("lblStudentId") as Label;

            Label lblAddressT = e.Row.FindControl("lblAddressT") as Label;

            EDDEnt = new HSS_EDUCATION_DETAIL();
            EDDEnt.STUDENT_ID = lblStudentId.Text;
            EDDEnt = (HSS_EDUCATION_DETAIL)EDDSer.GetSingle(EDDEnt);
            if (EDDEnt != null)
            {
                //lblAddressT.Text = AEnt.STREET_NAME;
            }
            if (!string.IsNullOrEmpty(lblStudentId.Text))
            {
                string imgfolder = Server.MapPath(@"~/images/bachelorstudent/") + lblStudentId.Text + ".jpg";
                if (File.Exists(imgfolder))
                {
                    imgStudent.ImageUrl = "~/images/bachelorstudent/" + lblStudentId.Text + ".jpg";

                }
                else
                {
                    STDEnt = new HSS_STUDENT();
                    STDEnt.STUDENT_ID = lblStudentId.Text;
                    STDEnt = (HSS_STUDENT)STDSer.GetSingle(STDEnt);
                    if (STDEnt != null)
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

                AEnt = new ADDRESS();
                AEnt.ADDRESS_OF_ID = lblpkid.Text;
                AEnt = (ADDRESS)ASer.GetSingle(AEnt);
                if (AEnt != null)
                {
                    lblAddressT.Text = AEnt.STREET_NAME;
                }
            }


        }
    }

    protected void gridStdAcadHistory_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("View"))
        {
            GridViewRow gr = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            Label lblpkid = gr.FindControl("lblpkid") as Label;

            Response.Redirect("~/exam/report/stdAcaDetailedHist.aspx?StdPK=" + lblpkid.Text);

        }
    }
}