using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;
using System.IO;
using Entity.Framework;

public partial class frontdesk_diary_teacherdiary : System.Web.UI.Page
{

    Employees EMPEnt = new Employees();
    EmployeesService EMPSer = new EmployeesService();

    ADDRESS AEnt = new ADDRESS();
    ADDRESSService ASrv = new ADDRESSService();

    EntityList theList = new EntityList();
    EntityList newList = new EntityList();

    HelperFunction hf = new HelperFunction();
    string imgfolder = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadData();
        }
    }

    protected void LoadData()
    {
        //gridTeacher.DataSource = hf.getTeacherInfo();
        //gridTeacher.DataBind();

        EMPEnt = new Employees();
        theList = new EntityList();
        EntityList newList = new EntityList();
        EMPEnt.STATUS = "0";
        EMPEnt.DIVISION = "2";
        theList = EMPSer.GetAll(EMPEnt);

        gridTeacher.DataSource = theList;
        gridTeacher.DataBind();

    }
    protected void gridTeacher_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Image imgTeacher = e.Row.FindControl("imgTeacher") as Image;
            Label lblEmployeeId = e.Row.FindControl("lblEmployeeId") as Label;
            Label lblAddress = e.Row.FindControl("lblAddress") as Label;

            AEnt = new ADDRESS();
            AEnt.ADDRESS_OF_ID = lblEmployeeId.Text;
            AEnt = (ADDRESS)ASrv.GetSingle(AEnt);
            if (AEnt != null)
            {
                lblAddress.Text = AEnt.STREET_NAME;
            }


            if (!string.IsNullOrEmpty(lblEmployeeId.Text))
            {
                imgfolder = Server.MapPath(@"~/images/Employee/") + lblEmployeeId.Text + ".jpg";
                if (File.Exists(imgfolder))
                {
                    imgTeacher.ImageUrl = "~/images/Employee/" + lblEmployeeId.Text + ".jpg";

                }
                else
                {
                    EMPEnt = new Employees();
                    EMPEnt.EMPLOYEEID = lblEmployeeId.Text;
                    EMPEnt = (Employees)EMPSer.GetSingle(EMPEnt);
                    if (EMPEnt != null)
                    {
                        if (EMPEnt.GENDER.Trim() == "M")
                        {
                            imgTeacher.ImageUrl = "~/images/user/male.jpg";
                        }
                        if (EMPEnt.GENDER.Trim() == "F")
                        {
                            imgTeacher.ImageUrl = "~/images/user/female.jpeg";
                        }
                    }
                }
            }
        }
    }
    protected void gridTeacher_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        if (e.CommandName.Equals("View"))
        {
            GridViewRow gr = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            Label lblEmployeeId = gr.FindControl("lblEmployeeId") as Label;

            Response.Redirect("~/human_resource/reports/Staff_Detail.aspx?EmpId=" + lblEmployeeId.Text);

        }

        if (e.CommandName.Equals("ViewSubject"))
        {
            GridViewRow gr = ((Button)e.CommandSource).Parent.Parent as GridViewRow;
            Label lblEmployeeId = gr.FindControl("lblEmployeeId") as Label;

            Response.Redirect("~/human_resource/reports/Subject_Detail.aspx?EmpId=" + lblEmployeeId.Text);
        }
    }
}