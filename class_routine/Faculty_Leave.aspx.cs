using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Entity.Framework;
using Service.Components;

public partial class class_routine_Faculty_Leave : System.Web.UI.Page
{
    program PRGEnt = new program();
    programService PRGSer = new programService();

    hss_faculty FEnt = new hss_faculty();
    hss_facultyService FSer = new hss_facultyService();

    HSS_LEVEL LVLEnt = new HSS_LEVEL();
    HSS_LEVELService LVLSer = new HSS_LEVELService();

    FACULTY_LEAVE FLVEnt = new FACULTY_LEAVE();
    FACULTY_LEAVEService FLVSer = new FACULTY_LEAVEService();

    Employees EMPEnt = new Employees();
    EmployeesService EMPSer = new EmployeesService();

    TEACHERPROGRAMMAPPING TPMEnt = new TEACHERPROGRAMMAPPING();
    TEACHERPROGRAMMAPPINGService TPMSer = new TEACHERPROGRAMMAPPINGService();
    
    HelperFunction hf = new HelperFunction();

    UserProfileEntity userProfileEnt = new UserProfileEntity();

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
    }

    protected void ddlFaculty_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadPrgram();
    }

    protected void loadLevel()
    {
        LVLEnt = new HSS_LEVEL();
        ddlLevel.DataSource = LVLSer.GetAll(LVLEnt);
        ddlLevel.DataTextField = "LEVEL_NAME";
        ddlLevel.DataValueField = "LEVEL_NAME";
        ddlLevel.DataBind();
        ddlFaculty.Items.Insert(0, "Select");
    }

    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        loadPrgram();
    }

    protected void loadPrgram()
    {
        PRGEnt = new program();
        PRGEnt.FACULTY_ID = ddlFaculty.SelectedValue;
        PRGEnt.PROGRAM_LEVEL = ddlLevel.SelectedValue;
        ddlProgram.DataSource = PRGSer.GetAll(PRGEnt);
        ddlProgram.DataTextField = "PROGRAM_CODE";
        ddlProgram.DataValueField = "PK_ID";
        ddlProgram.DataBind();
        ddlProgram.Items.Insert(0, "Select");
    }

    protected void ddlProgram_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadFacultyMember();
    }

    protected void LoadFacultyMember()
    {
        EntityList thelist = new EntityList();
        EntityList newlist = new EntityList();
        TPMEnt = new TEACHERPROGRAMMAPPING();
        TPMEnt.PROGRAMID = ddlProgram.SelectedValue;
        TPMEnt.STATUS = "1";
        thelist = TPMSer.GetAll(TPMEnt);
        if (thelist.Count > 0)
        {
            foreach (TEACHERPROGRAMMAPPING tpm in thelist)
            {
                EMPEnt = new Employees();
                EMPEnt.EMPLOYEEID = tpm.TEACHERID;
                EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
                if (EMPEnt != null)
                {
                    newlist.Add(EMPEnt);
                }
            }
        }
        ddlFacultyMember.DataSource = newlist;
        ddlFacultyMember.DataTextField = "FIRSTNAME";
        ddlFacultyMember.DataValueField = "EMPLOYEEID";
        ddlFacultyMember.DataBind();

        LoadGrid();

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {

        if (lblPKIDU.Text != "")
        {
            FLVEnt = new FACULTY_LEAVE();
            FLVEnt.PK_ID = lblPKIDU.Text;
            FLVEnt = (FACULTY_LEAVE)FLVSer.GetSingle(FLVEnt);
            if (FLVEnt != null)
            {

                FLVEnt.EMPLOYEE_ID = ddlFacultyMember.SelectedValue;


                FLVEnt.APPROVE_DATE = txtApprovedDate.Text;


                FLVEnt.LEAVE_FROM_DATE = txtLeaveFromDate.Text;
                try
                {
                    string[] leavefromdate = hf.ConvertEnglishToNepali(txtLeaveFromDate.Text);
                    FLVEnt.LEAVE_FROM_DAY = leavefromdate[0];
                    FLVEnt.LEAVE_FROM_MONTH = leavefromdate[1];
                    FLVEnt.LEAVE_FROM_YEAR = leavefromdate[2];
                }
                catch
                {
                    txtLeaveFromDate.Focus();
                    HelperFunction.MsgBox(this, this.GetType(), "Wrong Date Format in Leave From Date");


                }

                FLVEnt.LEAVE_TO_DATE = txtLeaveToDate.Text;

                try
                {
                    string[] leavetodate = hf.ConvertEnglishToNepali(txtLeaveToDate.Text);
                    FLVEnt.LEAVE_TO_DAY = leavetodate[0];
                    FLVEnt.LEAVE_TO_MONTH = leavetodate[1];
                    FLVEnt.LEAVE_TO_YEAR = leavetodate[2];
                }
                catch
                {
                    txtLeaveToDate.Focus();
                    HelperFunction.MsgBox(this, this.GetType(), "Wrong Date Format in Leave To Date");


                }

                FLVEnt.REMARKS = txtDescription.Text;
                FLVEnt.APPROVE_BY = txtApprovedBy.Text;

                FLVEnt.NO_OF_PERIOD = txtNoOfPeriods.Text;


                if (FLVSer.Update(FLVEnt) >= 1)
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

            FLVEnt = new FACULTY_LEAVE();


            FLVEnt.EMPLOYEE_ID = ddlFacultyMember.SelectedValue;


            FLVEnt.APPROVE_DATE = txtApprovedDate.Text;


            FLVEnt.LEAVE_FROM_DATE = txtLeaveFromDate.Text;
            try
            {
                string[] leavefromdate = hf.ConvertEnglishToNepali(txtLeaveFromDate.Text);
                FLVEnt.LEAVE_FROM_DAY = leavefromdate[0];
                FLVEnt.LEAVE_FROM_MONTH = leavefromdate[1];
                FLVEnt.LEAVE_FROM_YEAR = leavefromdate[2];
            }
            catch
            {
                txtLeaveFromDate.Focus();
                HelperFunction.MsgBox(this, this.GetType(), "Wrong Date Format in Leave From Date");


            }

            FLVEnt.LEAVE_TO_DATE = txtLeaveToDate.Text;

            try
            {
                string[] leavetodate = hf.ConvertEnglishToNepali(txtLeaveToDate.Text);
                FLVEnt.LEAVE_TO_DAY = leavetodate[0];
                FLVEnt.LEAVE_TO_MONTH = leavetodate[1];
                FLVEnt.LEAVE_TO_YEAR = leavetodate[2];
            }
            catch
            {
                txtLeaveToDate.Focus();
                HelperFunction.MsgBox(this, this.GetType(), "Wrong Date Format in Leave To Date");


            }

            FLVEnt.REMARKS = txtDescription.Text;
            FLVEnt.APPROVE_BY = txtApprovedBy.Text;

            FLVEnt.NO_OF_PERIOD = txtNoOfPeriods.Text;


            if (FLVSer.Insert(FLVEnt) >= 1)
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


    protected void LoadGrid()
    {
        if (ddlFacultyMember.SelectedValue != "")
        {
            FLVEnt = new FACULTY_LEAVE();
            FLVEnt.EMPLOYEE_ID = ddlFacultyMember.SelectedValue;
            gridFacultyLeave.Visible = true;
            gridFacultyLeave.DataSource = FLVSer.GetAll(FLVEnt);
            gridFacultyLeave.DataBind();
        }
        else
        {
            gridFacultyLeave.Visible = false;
        }
        
    }


    protected void clearFields()
    {
        lblPKIDU.Text = "";


        txtDescription.Text = "";
        txtApprovedBy.Text = "";
        txtApprovedDate.Text = "";
        txtLeaveFromDate.Text = "";
        txtLeaveToDate.Text = "";
        txtNoOfPeriods.Text = "";


    }



    protected void gridFacultyLeave_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblEmployeeId = e.Row.FindControl("lblEmployeeId") as Label;
            Label lblEmployeeName = e.Row.FindControl("lblEmployeeName") as Label;

            EMPEnt = new Employees();
            EMPEnt.EMPLOYEEID = lblEmployeeId.Text;
            EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
            if (EMPEnt != null)
            {
                lblEmployeeName.Text = EMPEnt.FIRSTNAME;
            }



        }
    }
    protected void gridFacultyLeave_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Change"))
        {
            GridViewRow gr = ((ImageButton)e.CommandSource).Parent.Parent as GridViewRow;

            Label lblPKID = gr.FindControl("lblPKID") as Label;

            Label lblEmployeeId = gr.FindControl("lblEmployeeId") as Label;

            Label lblApprovedDate = gr.FindControl("lblApprovedDate") as Label;
            Label lblLeaveFromDate = gr.FindControl("lblLeaveFromDate") as Label;
            Label lblLeaveToDate = gr.FindControl("lblLeaveToDate") as Label;
            Label lblDescription = gr.FindControl("lblDescription") as Label;
            Label lblApprovedBy = gr.FindControl("lblApprovedBy") as Label;
            Label lblNoOfPeriod = gr.FindControl("lblNoOfPeriod") as Label;




            lblPKIDU.Text = lblPKID.Text;

            ddlFacultyMember.SelectedValue = lblEmployeeId.Text;

            txtApprovedDate.Text = lblApprovedDate.Text;
            txtLeaveFromDate.Text = lblLeaveFromDate.Text;
            txtLeaveToDate.Text = lblLeaveToDate.Text;
            txtApprovedBy.Text = lblApprovedBy.Text;
            txtNoOfPeriods.Text = lblNoOfPeriod.Text;
            txtDescription.Text = lblDescription.Text;


        }

    }
    protected void ddlFacultyMember_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadGrid();
    }
    
    
}