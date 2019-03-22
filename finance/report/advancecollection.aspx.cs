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

public partial class finance_advancecollection : System.Web.UI.Page
{
    HSS_STUDENT STEnt = new HSS_STUDENT();
    HSS_STUDENTService STSer = new HSS_STUDENTService();

    HSS_CURRENT_STUDENT CSTEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSTSer = new HSS_CURRENT_STUDENTService();

    Months MEnt = new Months();
    MonthsService MSer = new MonthsService();

    STUDENT_ADVANCE ADVEnt = new STUDENT_ADVANCE();
    STUDENT_ADVANCEService ADVSer = new STUDENT_ADVANCEService();


    BatchYear BYEnt = new BatchYear();
    BatchYearService BYSer = new BatchYearService();

    HelperFunction hf = new HelperFunction();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            loadBatch();

        }
    }

    protected void loadBatch()
    {
        BYEnt = new BatchYear();
        BYEnt.ACTIVE = "1";
        ddlBatch.DataSource = BYSer.GetAll(BYEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
        ddlBatch.Items.Insert(0, "Select");
    }

    protected void LoadData()
    {
        EntityList studentlist = new EntityList();
        EntityList advancelist = new EntityList();
        CSTEnt = new HSS_CURRENT_STUDENT();
        CSTEnt.BATCH = ddlBatch.SelectedValue;
        studentlist = CSTSer.GetAll(CSTEnt);

        foreach (HSS_CURRENT_STUDENT cs in studentlist)
        {
            ADVEnt = new STUDENT_ADVANCE();
            ADVEnt.STUDENT_ID = cs.STUDENT_ID;
            ADVEnt = (STUDENT_ADVANCE)ADVSer.GetSingle(ADVEnt);
            if (ADVEnt != null)
            {
                advancelist.Add(ADVEnt);
            }
        }




        gridAdvCollection.DataSource = advancelist;
        gridAdvCollection.DataBind();


    }





    protected void gridAdvCollection_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
        {
            LinkButton lnkbtnStudentId = e.Row.FindControl("lnkbtnStudentId") as LinkButton;

            Label lblAdvance = e.Row.FindControl("lblAdvance") as Label;

            Label lblStudentName = e.Row.FindControl("lblStudentName") as Label;

            Label lblBatch = e.Row.FindControl("lblBatch") as Label;

            STEnt = new HSS_STUDENT();
            STEnt.STUDENT_ID = lnkbtnStudentId.Text;
            STEnt = (HSS_STUDENT)STSer.GetSingle(STEnt);
            if (STEnt != null)
            {
                lblStudentName.Text = STEnt.NAME_ENGLISH;
            }
            lblBatch.Text = ddlBatch.SelectedValue;



            lblAdvance.Text = Convert.ToDouble(lblAdvance.Text).ToString("#0.00");


        }
    }
    protected void lnkbtnStudentId_Click(object sender, EventArgs e)
    {
        GridViewRow gr = ((LinkButton)sender).Parent.Parent as GridViewRow;
        LinkButton lnkbtnStudentId = (LinkButton)gr.FindControl("lnkbtnStudentId");
        Response.Redirect("studentpaymenthistory.aspx?studentId=" + lnkbtnStudentId.Text);
    }
    protected void btnView_Click(object sender, EventArgs e)
    {
        LoadData();
    }
}