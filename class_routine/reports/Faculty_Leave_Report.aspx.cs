using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;
using Entity.Framework;

public partial class class_routine_reports_Faculty_Leave_Report : System.Web.UI.Page
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

    OFFICE OFCEnt = new OFFICE();
    officeService OFCSer = new officeService();

    TEACHERPROGRAMMAPPING TPMEnt = new TEACHERPROGRAMMAPPING();
    TEACHERPROGRAMMAPPINGService TPMSer = new TEACHERPROGRAMMAPPINGService();

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

    protected void loadProgram()
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

    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        if (ddlFacultyMember.SelectedValue != "")
        {

            FLVEnt = new FACULTY_LEAVE();
            FLVEnt.EMPLOYEE_ID = ddlFacultyMember.SelectedValue;
            gridFacultyLeave.DataSource = FLVSer.GetAll(FLVEnt);
            gridFacultyLeave.DataBind();
            if (gridFacultyLeave.Rows.Count == 0)
            {
                btnPrint.Visible = false;
            }
            else
            {
                btnPrint.Visible = true;
            }
        }
        else
        {
            btnPrint.Visible = false;
            gridFacultyLeave.Visible = false;
        }
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

    protected void loadRoutineHeadings()
    {
        OFCEnt = new OFFICE();
        OFCEnt = (OFFICE)OFCSer.GetSingle(OFCEnt);
        if (OFCEnt != null)
        {
            lblCollegeName.Text = OFCEnt.OFFICENAME;
            lblAddress.Text = OFCEnt.ADDRESS;

        }
    }

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        loadRoutineHeadings();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", "printPartOfPage();", true);
    }
}