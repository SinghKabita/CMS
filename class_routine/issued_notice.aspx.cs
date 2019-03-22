using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Entity.Framework;
using Service.Components;

public partial class class_routine_issued_notice : System.Web.UI.Page
{
    hss_faculty FEnt = new hss_faculty();
    hss_facultyService FSer = new hss_facultyService();

    HSS_LEVEL LVLEnt = new HSS_LEVEL();
    HSS_LEVELService LVLSer = new HSS_LEVELService();

    issue_notice INEnt = new issue_notice();
    issue_noticeService INSer = new issue_noticeService();

    program PEnt = new program();
    programService PSer = new programService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    BatchYear BEnt = new BatchYear();
    BatchYearService BSer = new BatchYearService();

    HelperFunction hf = new HelperFunction();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadFaculty();
            loadLevel();

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

    protected void loadLevel()
    {
        LVLEnt = new HSS_LEVEL();
        ddlLevel.DataSource = LVLSer.GetAll(LVLEnt);
        ddlLevel.DataTextField = "LEVEL_NAME";
        ddlLevel.DataValueField = "LEVEL_NAME";
        ddlLevel.DataBind();
    }

    protected void clearFields()
    {
        lblPKIDU.Text = "";
        txtDescription.Text = "";
        txtHeading.Text = "";
        txtIssueDate.Text = "";
        txtIssuedBy.Text = "";
    }

    protected void btnAddMore_Click(object sender, EventArgs e)
    {
        clearFields();
        divAddNotice.Visible = true;
        divGrid.Visible = false;
    }

    protected void btnReset_Click(object sender, EventArgs e)
    {
        clearFields();
        divAddNotice.Visible = true;
    }

    protected void LoadGrid()
    {
        INEnt = new issue_notice();
        gridNotices.DataSource = hf.getAllnotices(ddlProgram.SelectedValue, ddlSemester.SelectedValue, txtFromDate.Text, txtToDate.Text);
        gridNotices.DataBind();

        if (gridNotices.Rows.Count != 0)
        {
            divGrid.Visible = true;
        }
        else
        {
            divGrid.Visible = false;
        }
    }

    protected void LoadProgram()
    {
        PEnt = new program();
        PEnt.PROGRAM_LEVEL = ddlLevel.SelectedValue;
        PEnt.FACULTY_ID = ddlFaculty.SelectedValue;
        ddlProgram.DataSource = PSer.GetAll(PEnt);
        ddlProgram.DataTextField = "PROGRAM_CODE";
        ddlProgram.DataValueField = "PK_ID";
        ddlProgram.DataBind();
        ddlProgram.Items.Insert(0, "Select");
        ddlSemester.Items.Insert(0, "Select");

    }

    protected void LoadSemester()
    {

        EntityList theList = new EntityList();
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
    }

    protected void LoadBatch()
    {
        BEnt = new BatchYear();
        BEnt.ACTIVE = "1";
        BEnt.PROGRAM = ddlProgram.SelectedValue;
        ddlBatch.DataSource = BSer.GetAll(BEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
    }

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
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
            ddlSemester.Items.Clear();
            ddlSemester.Items.Insert(0, "Select");

        }
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        INEnt = new issue_notice();
        INEnt.PK_ID = lblPKIDU.Text;
        INEnt = (issue_notice)INSer.GetSingle(INEnt);
        if (INEnt != null && lblPKIDU.Text != "")
        {
            INEnt.NOTICE_ISSUE_DATE = txtIssueDate.Text;
            string[] nepdate = hf.ConvertEnglishToNepali(txtIssueDate.Text);
            INEnt.NOTICE_ISSUE_DAY = nepdate[0];
            INEnt.NOTICE_ISSUE_MONTH = nepdate[1];
            INEnt.NOTICE_ISSUE_YEAR = nepdate[2];
            INEnt.NOTICE_HEADING = txtHeading.Text;
            INEnt.NOTICE_ISSUED_BY = txtIssuedBy.Text;
            INEnt.DESCRIPTION = txtDescription.Text;
            if (INSer.Update(INEnt) > 0)
            {
                HelperFunction.MsgBox(this, this.GetType(), "Updated Successfully");
                clearFields();
                divAddNotice.Visible = false;
            }
            else
            {
                HelperFunction.MsgBox(this, this.GetType(), "Unable to update");
            }
        }
        else
        {
            INEnt = new issue_notice();
            INEnt.NOTICE_ISSUE_DATE = txtIssueDate.Text;
            string[] nepdate = hf.ConvertEnglishToNepali(txtIssueDate.Text);
            INEnt.NOTICE_ISSUE_DAY = nepdate[0];
            INEnt.NOTICE_ISSUE_MONTH = nepdate[1];
            INEnt.NOTICE_ISSUE_YEAR = nepdate[2];
            INEnt.NOTICE_HEADING = txtHeading.Text;
            INEnt.NOTICE_ISSUED_BY = txtIssuedBy.Text;
            INEnt.DESCRIPTION = txtDescription.Text;

            if (INSer.Insert(INEnt) > 0)
            {
                HelperFunction.MsgBox(this, this.GetType(), "Inserted Successfully");
                clearFields();
                divAddNotice.Visible = false;
            }
            else
            {
                HelperFunction.MsgBox(this, this.GetType(), "Unable to Insert");
            }
        }


        LoadGrid();


    }
    protected void gridNotices_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) != 0)
        {
            TextBox txtIssueDateE = e.Row.FindControl("txtIssueDateE") as TextBox;



        }
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        divAddNotice.Visible = false;
        divGrid.Visible = true;
        LoadGrid();
    }

    protected void gridNotices_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("change"))
        {
            divAddNotice.Visible = true;
            GridViewRow gr = ((ImageButton)e.CommandSource).Parent.Parent as GridViewRow;
            Label lblPkid = gr.FindControl("lblPkid") as Label;
            lblPKIDU.Text = lblPkid.Text;
            INEnt = new issue_notice();
            INEnt.PK_ID = lblPkid.Text;
            INEnt = (issue_notice)INSer.GetSingle(INEnt);
            if (INEnt != null)
            {
                txtIssueDate.Text = INEnt.NOTICE_ISSUE_DATE;
                txtHeading.Text = INEnt.NOTICE_HEADING;
                txtIssuedBy.Text = INEnt.NOTICE_ISSUED_BY;
                txtDescription.Text = INEnt.DESCRIPTION;
            }

        }
    }


    protected void ddlSemester_SelectedIndexChanged(object sender, EventArgs e)
    {
        BEnt = new BatchYear();
        BEnt.SEMESTER = ddlSemester.SelectedValue;
        BEnt = (BatchYear)BSer.GetSingle(BEnt);
        if (BEnt != null)
        {
            ddlBatch.SelectedValue = BEnt.BATCH;
        }
    }

}