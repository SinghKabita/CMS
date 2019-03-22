using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Entity.Components;
using Service.Components;

public partial class uc_test_ActionReward : System.Web.UI.UserControl
{
    string studentname, grade;

    HelperFunction hf = new HelperFunction();

    ACTION_REWARD AREnt = new ACTION_REWARD();
    ACTION_REWARDService ARSer = new ACTION_REWARDService();

    ACTION_REWARD_TYPE ARTEnt = new ACTION_REWARD_TYPE();
    ACTION_REWARD_TYPEService ARTSer = new ACTION_REWARD_TYPEService();

    HSS_STUDENT SEnt = new HSS_STUDENT();
    HSS_STUDENTService SSer = new HSS_STUDENTService();

    HSS_CURRENT_STUDENT CSEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSer = new HSS_CURRENT_STUDENTService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    program PEnt = new program();
    programService PSer = new programService();

    string semester = "";
    string program = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadDate();
        }
    }

    protected void LoadDate()
    {
        string[] date = hf.GetTodayDate().Split('/');
        txtDay1.Text = date[0];
        //txtDay2.Text = date[0];
        //txtDay3.Text = date[0];

        txtMonth1.Text = date[1];
        //txtMonth2.Text = date[1];
        //txtMonth3.Text = date[1];

        txtYear1.Text = date[2];
        //txtYear2.Text = date[2];
        //txtYear3.Text = date[2];
    }

    protected void ddlActionReward_SelectedIndexChanged(object sender, EventArgs e)
    {

       LoadActioRewardType();
    }

      protected void LoadActioRewardType()
    {
        AREnt = new ACTION_REWARD();
        AREnt.ACTION_REWARDS = ddlActionReward.SelectedValue;


        ddlActionRewardType.DataSource = ARSer.GetAll(AREnt);
        ddlActionRewardType.DataTextField = "Activity";
        ddlActionRewardType.DataValueField = "Activity";
        ddlActionRewardType.DataBind();
        ddlActionRewardType.Items.Insert(0, "-SELECT-");
    }
    
    protected void txtStudentId_TextChanged(object sender, EventArgs e)
    {
        if (txtStudentId.Text != "")
        {
            SEnt = new HSS_STUDENT();
            SEnt.STUDENT_ID = txtStudentId.Text;
            SEnt = (HSS_STUDENT)SSer.GetSingle(SEnt);
            if (SEnt != null)
            {
                detaildiv.Visible = true;

                txtStudentName.Text = getStudentName(txtStudentId.Text);
                lblPrg.Text = getStudentProgram(txtStudentId.Text);
                lblSem.Text = getStudentSemester(txtStudentId.Text);
                LoadGrid();
            }

            else
            {
                detaildiv.Visible = false;
                HelperFunction.MsgBox(this, this.GetType(), "Invalid Student ID.");
            }
        }
        else
        {
            HelperFunction.MsgBox(this, this.GetType(), "Student ID Field is Empty");
        }
    }

    protected void LoadGrid()
    {
        ARTEnt = new ACTION_REWARD_TYPE();
        ARTEnt.STUDENTID = txtStudentId.Text;
        gridActionReward.DataSource = ARTSer.GetAll(ARTEnt);
        gridActionReward.DataBind();
    }

    protected string getStudentName(string studentid)
    {
        SEnt = new HSS_STUDENT();

        SEnt.STUDENT_ID = studentid;

        SEnt = (HSS_STUDENT)SSer.GetSingle(SEnt);
        if (SEnt != null)
        {

            studentname = SEnt.NAME_ENGLISH;
        }
        return studentname;

    }

    protected string getStudentSemester(string studentid)
    {
        CSEnt = new HSS_CURRENT_STUDENT();

        CSEnt.STUDENT_ID = studentid;
        CSEnt.STATUS = "1";

        CSEnt = (HSS_CURRENT_STUDENT)CSSer.GetSingle(CSEnt);
        if (CSEnt != null)
        {
            SMEnt = new semester();
            SMEnt.PK_ID = CSEnt.SEMESTER;
            SMEnt = (semester)SMSer.GetSingle(SMEnt);
            if (SMEnt != null)
            {
                semester = SMEnt.SEMESTER_CODE;
            }


        }
        return semester;

    }

    protected string getStudentProgram(string studentid)
    {
        SEnt = new HSS_STUDENT();

        SEnt.STUDENT_ID = studentid;

        SEnt = (HSS_STUDENT)SSer.GetSingle(SEnt);
        if (SEnt != null)
        {

            PEnt = new program();
            PEnt.PK_ID = SEnt.PROGRAM;
            PEnt = (program)PSer.GetSingle(PEnt);
            if (PEnt != null)
            {
                program = PEnt.PROGRAM_CODE;
            }



        }
        return program;

    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        if(lblPKIDU.Text=="")
        {
        ARTEnt = new ACTION_REWARD_TYPE();

        ARTEnt.STUDENTID = txtStudentId.Text;
        ARTEnt.CLASS = lblSem.Text;
        ARTEnt.ACTION_REWARDS = ddlActionReward.SelectedValue;
        ARTEnt.ACTION_REWARD_TYPE1 = ddlActionRewardType.SelectedValue;
        ARTEnt.DETAIL = txtDetail.Text;

        ARTEnt.ENTRYDAY = txtDay1.Text;
        ARTEnt.ENTRYMONTH = txtMonth1.Text;
        ARTEnt.ENTRYYEAR = txtYear1.Text;
        ARTEnt.ENTRYDATE = txtDay1.Text + "/" + txtMonth1.Text + "/" + txtYear1.Text;

        //ARTEnt.EFFECTIVEFROM_DAY = txtDay2.Text;
        //ARTEnt.EFFECTIVEFROM_MONTH = txtMonth2.Text;
        //ARTEnt.EFFECTIVEFROM_YEAR = txtYear2.Text;
        //ARTEnt.EFFECTIVEFROM_DATE = txtDay2.Text + "/" + txtMonth2.Text + "/" + txtYear2.Text;

        //ARTEnt.EFFECTIVETO_DAY = txtDay3.Text;
        //ARTEnt.EFFECTIVETO_MONTH = txtMonth3.Text;
        //ARTEnt.EFFECTIVETO_YEAR = txtYear3.Text;
        //ARTEnt.EFFECTIVETO_DATE = txtDay3.Text + "/" + txtMonth3.Text + "/" + txtYear3.Text;

        //if (ddlActionReward.SelectedValue == "Remarks")
        //{

        //    ARTEnt.EFFECTIVEFROM_DATE = "";
        //    ARTEnt.EFFECTIVETO_DATE = "";
        //}

        if (ARTSer.Insert(ARTEnt) >= 1)
        {

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Inserted Successfully')", true);
            ClearData();
        }

        else
        {
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Record Insertion is not Successfull')", true);
        }
    }
          else if (lblPKIDU.Text != "")
        {
            ARTEnt = new ACTION_REWARD_TYPE();
            ARTEnt.PKID = lblPKIDU.Text;
            ARTEnt = (ACTION_REWARD_TYPE)ARTSer.GetSingle(ARTEnt);
            if (ARTEnt != null)
            {
                ARTEnt.ACTION_REWARDS = ddlActionReward.SelectedValue;
                ARTEnt.ACTION_REWARD_TYPE1 = ddlActionRewardType.SelectedValue;
                ARTEnt.DETAIL = txtDetail.Text;

                ARTEnt.ENTRYDAY=txtDay1.Text;
                ARTEnt.ENTRYMONTH= txtMonth1.Text;
                ARTEnt.ENTRYYEAR= txtYear1.Text;
                ARTEnt.ENTRYDATE = txtDay1.Text + "/" + txtMonth1.Text + "/" + txtYear1.Text;

                //ARTEnt.EFFECTIVEFROM_DAY= txtDay2.Text;
                //ARTEnt.EFFECTIVEFROM_MONTH=txtMonth2.Text;
                //ARTEnt.EFFECTIVEFROM_YEAR=txtYear2.Text;
                //ARTEnt.EFFECTIVEFROM_DATE = txtDay2.Text + "/" + txtMonth2.Text + "/" + txtYear2.Text;

                // ARTEnt.EFFECTIVETO_DAY=txtDay3.Text;
                // ARTEnt.EFFECTIVETO_MONTH=txtMonth3.Text;
                // ARTEnt.EFFECTIVETO_YEAR=txtYear3.Text;
                // ARTEnt.EFFECTIVETO_DATE = txtDay3.Text + "/" + txtMonth3.Text + "/" + txtYear3.Text;
                 if (ARTSer.Update(ARTEnt) >= 1)
                 {

                    HelperFunction.MsgBox(this, this.GetType(),"Record Updated Successfully");
                    ClearDataNext();
               
                    LoadGrid();
                     

                 }

                 else
                 {
                     HelperFunction.MsgBox(this, this.GetType(), "Something goes wrong");
                 }
            }
        
        }

    }

    protected void ClearDataNext()
    {

        lblPKIDU.Text = "";

        ddlActionReward.SelectedValue = "-SELECT-";
        ddlActionRewardType.SelectedValue = "-SELECT-";
        txtDetail.Text = "";
        txtDay1.Text = "";
        txtMonth1.Text = "";
        txtYear1.Text = "";
        //txtDay2.Text = "";
        //txtMonth2.Text = "";
        //txtYear2.Text = "";
        //txtDay3.Text = "";
        //txtMonth3.Text = "";
        //txtYear3.Text = "";

    }

    protected void ClearData()
    {
        txtStudentId.Text = "";
        txtStudentName.Text = "";

        ddlActionReward.SelectedValue = "-SELECT-";
        ddlActionRewardType.SelectedValue = "-SELECT-";
        txtDetail.Text = "";
        txtDay1.Text = "";
        txtMonth1.Text = "";
        txtYear1.Text = "";
        //txtDay2.Text = "";
        //txtMonth2.Text = "";
        //txtYear2.Text = "";
        //txtDay3.Text = "";
        //txtMonth3.Text = "";
        //txtYear3.Text = "";

    }
    protected void gridActionReward_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow && (e.Row.RowState & DataControlRowState.Edit) == 0)
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
    protected void gridActionReward_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Change"))
        {
            GridViewRow gr = ((ImageButton)e.CommandSource).Parent.Parent as GridViewRow;

            Label lblPKID = gr.FindControl("lblPKID") as Label;


            ARTEnt = new ACTION_REWARD_TYPE();
            ARTEnt.PKID = lblPKID.Text;
            ARTEnt = (ACTION_REWARD_TYPE)ARTSer.GetSingle(ARTEnt);
            if (ARTEnt != null)
            {

                lblPKIDU.Text = lblPKID.Text;
                txtDetail.Text = ARTEnt.DETAIL;
                txtDay1.Text = ARTEnt.ENTRYDAY;
                txtMonth1.Text = ARTEnt.ENTRYMONTH;
                txtYear1.Text = ARTEnt.ENTRYYEAR;

                //txtDay2.Text = ARTEnt.EFFECTIVEFROM_DAY;
                //txtMonth2.Text = ARTEnt.EFFECTIVEFROM_MONTH;
                //txtYear2.Text = ARTEnt.EFFECTIVEFROM_YEAR;

                //txtDay3.Text = ARTEnt.EFFECTIVETO_DAY;
                //txtMonth3.Text = ARTEnt.EFFECTIVETO_MONTH;
                //txtYear3.Text = ARTEnt.EFFECTIVETO_YEAR;

                ddlActionReward.SelectedValue = ARTEnt.ACTION_REWARDS;

                LoadActioRewardType();

                ddlActionRewardType.SelectedValue = ARTEnt.ACTION_REWARD_TYPE1;



            }

        }
    }
    protected void gridActionReward_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        GridViewRow gr = gridActionReward.Rows[e.RowIndex];

        Label lblPKID = gr.FindControl("lblPKID") as Label;

        ARTEnt = new ACTION_REWARD_TYPE();
        ARTEnt.PKID = lblPKID.Text;
        ARTEnt = (ACTION_REWARD_TYPE)ARTSer.GetSingle(ARTEnt);
        if (ARTEnt != null)
        {
            ARTSer.Delete(ARTEnt);
            LoadGrid();
        }


    }
}