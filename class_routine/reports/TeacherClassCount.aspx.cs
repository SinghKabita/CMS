using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;
using Entity.Framework;

public partial class class_routine_reports_TeacherClassCount : System.Web.UI.Page
{
    Employees EMPEnt = new Employees();
    EmployeesService EMPSer = new EmployeesService();

    HelperFunction hf = new HelperFunction();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadTeacher();
        }
    }

    protected void loadTeacher()
    {
        EMPEnt = new Employees();
        EMPEnt.DIVISION = "2";
        ddlTeacher.DataSource = EMPSer.GetAll(EMPEnt);
        ddlTeacher.DataValueField = "EMPLOYEEID";
        ddlTeacher.DataTextField = "FULLNAME";
        ddlTeacher.DataBind();
        ddlTeacher.Items.Insert(0, "Select Teacher");
    }


    protected void btnView_Click(object sender, EventArgs e)
    {
        string[] fDate, tDate;
        
        fDate = txtFromDate.Text.Split('.');
        tDate = txtToDate.Text.Split('.');
        gridTeacherClass.DataSource = hf.getIndividualTeacherClass(ddlTeacher.SelectedValue, txtFromDate.Text, txtToDate.Text);
        gridTeacherClass.DataBind();
    }
}