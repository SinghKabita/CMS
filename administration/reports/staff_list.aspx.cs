using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using System.Collections;
using Entity.Framework;
using Service.Components;

using DataAccess.Components;
using DataAccess.Framework;
using DataHelper.Framework;

using System.Text;
using System.Configuration;

using System.Web.Security;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Oracle.DataAccess.Client;
using System.IO;


public partial class administration_reports_staff_list : System.Web.UI.Page
{
    program PEnt = new program();
    programService PSer = new programService();

    Employees EEnt = new Employees();
    EmployeesService ESrv = new EmployeesService();

    Division DVEnt = new Division();
    DivisionService DVSrv = new DivisionService();

    HSS_LEVEL LEnt = new HSS_LEVEL();
    HSS_LEVELService LSrv = new HSS_LEVELService();

    EntityList theList = new EntityList();
    EntityList newList = new EntityList();

    TEACHERPROGRAMMAPPING TPMEnt = new TEACHERPROGRAMMAPPING();
    TEACHERPROGRAMMAPPINGService TPMSrv = new TEACHERPROGRAMMAPPINGService();

    ADDRESS AEnt = new ADDRESS();
    ADDRESSService ASrv = new ADDRESSService();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDivision();

            string div = "";
            div = (Request.QueryString.Get("div"));

            if (div != null)
            {
                EEnt = new Employees();

                ddlDivision.SelectedValue = div;



                theList = new EntityList();
                EntityList newList = new EntityList();
                EEnt.STATUS = "0";
                EEnt.DIVISION = div;
                theList = ESrv.GetAll(EEnt);
                if (ddlProgram.SelectedValue.ToString() != "Select" && ddlDivision.SelectedValue == "2")
                {
                    foreach (Employees emp in theList)
                    {
                        TPMEnt = new TEACHERPROGRAMMAPPING();
                        TPMEnt.TEACHERID = emp.EMPLOYEEID;
                        TPMEnt.PROGRAMID = ddlProgram.SelectedValue;
                        TPMEnt = (TEACHERPROGRAMMAPPING)TPMSrv.GetSingle(TPMEnt);
                        if (TPMEnt != null)
                        {
                            newList.Add(emp);
                        }
                    }
                    gridEmployeeDiary.DataSource = newList;
                    gridEmployeeDiary.DataBind();
                }
                else
                {
                    gridEmployeeDiary.DataSource = theList;
                    gridEmployeeDiary.DataBind();
                }

            }

        }
    }

    protected void LoadDivision()
    {

        DVEnt = new Division();
        ddlDivision.DataSource = DVSrv.GetAll(DVEnt);
        ddlDivision.DataTextField = "DIVNAME";
        ddlDivision.DataValueField = "DIVID";
        ddlDivision.DataBind();
        ddlDivision.Items.Insert(0, "Select");

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

        PEnt.PROGRAM_LEVEL = ddlLevel.SelectedValue;

        ddlProgram.DataSource = PSer.GetAll(PEnt);
        ddlProgram.DataTextField = "PROGRAM_CODE";
        ddlProgram.DataValueField = "PK_ID";
        ddlProgram.DataBind();
        ddlProgram.Items.Insert(0, "Select");

    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        EEnt = new Employees();
        theList = new EntityList();
        EntityList newList = new EntityList();
        EEnt.STATUS = "0";
        EEnt.DIVISION = ddlDivision.SelectedValue;
        theList = ESrv.GetAll(EEnt);
        if (ddlProgram.SelectedValue.ToString() != "Select" && ddlDivision.SelectedValue == "2")
        {
            foreach (Employees emp in theList)
            {
                TPMEnt = new TEACHERPROGRAMMAPPING();
                TPMEnt.TEACHERID = emp.EMPLOYEEID;
                TPMEnt.PROGRAMID = ddlProgram.SelectedValue;
                TPMEnt = (TEACHERPROGRAMMAPPING)TPMSrv.GetSingle(TPMEnt);
                if (TPMEnt != null)
                {
                    newList.Add(emp);
                }
            }
            gridEmployeeDiary.DataSource = newList;
            gridEmployeeDiary.DataBind();
        }
        else
        {
            gridEmployeeDiary.DataSource = theList;
            gridEmployeeDiary.DataBind();
        }

    }

    protected void gridEmployeeDiary_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblEmpId = e.Row.FindControl("lblEmpId") as Label;
            Label lblAddressT = e.Row.FindControl("lblAddressT") as Label;
            Image imgStudent = e.Row.FindControl("imgStudent") as Image;

            AEnt = new ADDRESS();
            AEnt.ADDRESS_OF_ID = lblEmpId.Text;
            AEnt.ADDRESS_TYPE = "C";
            AEnt = (ADDRESS)ASrv.GetSingle(AEnt);
            if (AEnt != null)
            {
                lblAddressT.Text = AEnt.STREET_NAME;
            }

            string imgfolder = "";
            imgfolder = Server.MapPath(@"~/images/Employee/") + lblEmpId.Text + ".jpg";
            if (File.Exists(imgfolder))
            {
                imgStudent.ImageUrl = @"~/images/Employee/" + lblEmpId.Text + ".jpg";

            }
            else
            {



                imgStudent.ImageUrl = @"~/images/Employee/male.jpg";
            }

        }


    }
    protected void gridEmployeeDiary_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("View"))
        {
            GridViewRow gr = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            Label lblEmpId = gr.FindControl("lblEmpId") as Label;

            Response.Redirect("~/human_resource/reports/Staff_Detail.aspx?EmpId=" + lblEmpId.Text);
        }


        if (e.CommandName.Equals("Edit"))
        {
            GridViewRow gr = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            Label lblEmpId = gr.FindControl("lblEmpId") as Label;

            Response.Redirect("~/human_resource/Employees.aspx?EmpId=" + lblEmpId.Text);
        }

        if (e.CommandName.Equals("ViewSubject"))
        {
            GridViewRow gr = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            Label lblEmpId = gr.FindControl("lblEmpId") as Label;

            Response.Redirect("~/human_resource/reports/Subject_Detail.aspx?empId=" + lblEmpId.Text);
        }

    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDivision.SelectedValue == "2")
        {
            trAcademic.Visible = true;
            LoadLevel();
            LoadProgram();
        }
        else
        {
            trAcademic.Visible = false;

        }

    }
    protected void ddlLevel_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadProgram();
    }
}