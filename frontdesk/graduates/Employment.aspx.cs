using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;

public partial class frontdesk_graduates_Employment : System.Web.UI.Page
{

    BatchYear BTEnt = new BatchYear();
    BatchYearService BTSer = new BatchYearService();

    GRADUATE_EMPLOYMENT EMPEnt = new GRADUATE_EMPLOYMENT();
    GRADUATE_EMPLOYMENTService EMPSer = new GRADUATE_EMPLOYMENTService();



    HSS_STUDENT SEnt = new HSS_STUDENT();
    HSS_STUDENTService SSer = new HSS_STUDENTService();

    HelperFunction hf = new HelperFunction();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadBatch();
        }

    }

    protected void LoadBatch()
    {
        BTEnt = new BatchYear();
        ddlBatch.DataSource = BTSer.GetAll(BTEnt);
        ddlBatch.DataTextField = "BATCH";
        ddlBatch.DataValueField = "BATCH";
        ddlBatch.DataBind();
        ddlBatch.Items.Insert(0, "Select");
        ddlStudentId.Items.Insert(0, "Select");
    }
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadStudent();
    }

    protected void LoadStudent()
    {
        if (ddlBatch.SelectedValue != "Select")
        {
            ddlStudentId.DataSource = hf.getStudentInfo(ddlBatch.SelectedValue, "");
            ddlStudentId.DataTextField = "name";
            ddlStudentId.DataValueField = "STUDENT_ID";
            ddlStudentId.DataBind();
            ddlStudentId.Items.Insert(0, "Select");
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (ddlBatch.SelectedValue == "Select")
        {
            HelperFunction.MsgBox(this, this.GetType(), "Please Select Batch");
        }
        else if (ddlStudentId.SelectedValue == "Select" || ddlStudentId.SelectedValue == "")
        {
            HelperFunction.MsgBox(this, this.GetType(), "Please Select Student");
        }

        else
        {
            if (lblPKIDU.Text == "")
            {
                try
                {
                    EMPEnt = new GRADUATE_EMPLOYMENT();
                    EMPEnt.BATCH = ddlBatch.SelectedValue;
                    EMPEnt.STUDENT_ID = ddlStudentId.SelectedValue;
                    EMPEnt.ORGANIZATION = txtOrganization.Text;

                    if (txtFromDate.Text != "")
                    {
                        EMPEnt.FROM_DATE = txtFromDate.Text;
                        string[] nepdate = hf.ConvertEnglishToNepali(txtFromDate.Text);
                        EMPEnt.FROM_DAY = nepdate[0];
                        EMPEnt.FROM_MONTH = nepdate[1];
                        EMPEnt.FROM_YEAR = nepdate[2];
                    }

                    if (txtToDate.Text != "")
                    {
                        EMPEnt.TO_DATE = txtToDate.Text;
                        string[] nepdate = hf.ConvertEnglishToNepali(txtToDate.Text);
                        EMPEnt.TO_DAY = nepdate[0];
                        EMPEnt.TO_MONTH = nepdate[1];
                        EMPEnt.TO_YEAR = nepdate[2];
                    }

                    EMPEnt.POSITION = txtPosition.Text;
                    EMPEnt.WORKING_STATUS = rbtnStatus.SelectedValue;
                    if (EMPSer.Insert(EMPEnt) >= 1)
                    {
                        HelperFunction.MsgBox(this, this.GetType(), "Insert Successfull");
                        ClearFields();
                    }
                    else
                    {
                        HelperFunction.MsgBox(this, this.GetType(), "Sorry Not Successfull");
                    }
                }
                catch
                {
                    HelperFunction.MsgBox(this, this.GetType(), "Wrong Date Format");
                }

            }
            else
            {

                try
                {
                    EMPEnt = new GRADUATE_EMPLOYMENT();
                    EMPEnt.PK_ID = lblPKIDU.Text;
                    EMPEnt = (GRADUATE_EMPLOYMENT)EMPSer.GetSingle(EMPEnt);
                    if (EMPEnt != null)
                    {
                        EMPEnt.BATCH = ddlBatch.SelectedValue;
                        EMPEnt.STUDENT_ID = ddlStudentId.SelectedValue;
                        EMPEnt.ORGANIZATION = txtOrganization.Text;

                        if (txtFromDate.Text != "")
                        {
                            EMPEnt.FROM_DATE = txtFromDate.Text;
                            string[] nepdate = hf.ConvertEnglishToNepali(txtFromDate.Text);
                            EMPEnt.FROM_DAY = nepdate[0];
                            EMPEnt.FROM_MONTH = nepdate[1];
                            EMPEnt.FROM_YEAR = nepdate[2];
                        }

                        if (txtToDate.Text != "")
                        {
                            EMPEnt.TO_DATE = txtToDate.Text;
                            string[] nepdate = hf.ConvertEnglishToNepali(txtToDate.Text);
                            EMPEnt.TO_DAY = nepdate[0];
                            EMPEnt.TO_MONTH = nepdate[1];
                            EMPEnt.TO_YEAR = nepdate[2];
                        }

                        EMPEnt.POSITION = txtPosition.Text;
                        EMPEnt.WORKING_STATUS = rbtnStatus.SelectedValue;
                        if (EMPSer.Update(EMPEnt) >= 1)
                        {
                            HelperFunction.MsgBox(this, this.GetType(), "Update Successfull");
                            ClearFields();
                        }
                        else
                        {
                            HelperFunction.MsgBox(this, this.GetType(), "Sorry Not Successfull");
                        }
                    }
                }
                catch
                {
                    HelperFunction.MsgBox(this, this.GetType(), "Wrong Date Format");
                }


            }


        }

    }

    protected void ClearFields()
    {
        txtOrganization.Text = "";
        txtFromDate.Text = "";
        txtToDate.Text = "";
        lblPKIDU.Text = "";
        txtPosition.Text = "";
        rbtnStatus.SelectedIndex = 0;


        ddlStudentId.Items.Clear();
        LoadBatch();

        gridEmployment.DataSource = null;
        gridEmployment.DataBind();

    }

    protected void LoadData()
    {
        if (ddlStudentId.SelectedValue != "Select" && ddlBatch.SelectedValue != "Select")
        {
            EMPEnt = new GRADUATE_EMPLOYMENT();
            EMPEnt.BATCH = ddlBatch.SelectedValue;
            EMPEnt.STUDENT_ID = ddlStudentId.SelectedValue;
            gridEmployment.DataSource = EMPSer.GetAll(EMPEnt);
            gridEmployment.DataBind();
        }
    }
    protected void ddlStudentId_SelectedIndexChanged(object sender, EventArgs e)
    {

        LoadData();

    }
    protected void gridEmployment_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
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
    protected void gridEmployment_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName.Equals("Change"))
        {
            GridViewRow gr = ((ImageButton)e.CommandSource).Parent.Parent as GridViewRow;
            Label lblStudentId = gr.FindControl("lblStudentId") as Label;
            Label lblPKID = gr.FindControl("lblPKID") as Label;
            Label lblBatch = gr.FindControl("lblBatch") as Label;
            Label lblFromDate = gr.FindControl("lblFromDate") as Label;
            Label lblToDate = gr.FindControl("lblToDate") as Label;
            Label lblOrganization = gr.FindControl("lblOrganization") as Label;
            Label lblPosition = gr.FindControl("lblPosition") as Label;
            Label lblCurrentlyWorking = gr.FindControl("lblCurrentlyWorking") as Label;

            lblPKIDU.Text = lblPKID.Text;
            ddlBatch.SelectedValue = lblBatch.Text;
            LoadStudent();
            ddlStudentId.SelectedValue = lblStudentId.Text;
            txtOrganization.Text = lblOrganization.Text;
            txtFromDate.Text = lblFromDate.Text;
            txtToDate.Text = lblToDate.Text;
            txtPosition.Text = lblPosition.Text;
            rbtnStatus.SelectedValue = lblCurrentlyWorking.Text;

        }
    }

    protected void txtFDay_TextChanged(object sender, EventArgs e)
    {
        txtFromDate.Text = GetFromEnglishDate();
        txtFMonth.Focus();

    }
    protected void txtFMonth_TextChanged(object sender, EventArgs e)
    {
        txtFromDate.Text = GetFromEnglishDate();
        txtFYear.Focus();
    }
    protected void txtFYear_TextChanged(object sender, EventArgs e)
    {
        txtFromDate.Text = GetFromEnglishDate();
        txtToDate.Focus();
    }
    protected void txtTDay_TextChanged(object sender, EventArgs e)
    {
        txtToDate.Text = GetToEnglishDate();
        txtTMonth.Focus();

    }
    protected void txtTMonth_TextChanged(object sender, EventArgs e)
    {
        txtToDate.Text = GetToEnglishDate();
        txtTYear.Focus();
    }
    protected void txtTYear_TextChanged(object sender, EventArgs e)
    {
        txtToDate.Text = GetToEnglishDate();
        txtPosition.Focus();
    }
    protected void txtFromDate_TextChanged(object sender, EventArgs e)
    {
        if (txtFromDate.Text != "")
        {
            try
            {
                string[] nepdate = hf.ConvertEnglishToNepali(txtFromDate.Text);
                txtFDay.Text = nepdate[0];
                txtFMonth.Text = nepdate[1];
                txtFYear.Text = nepdate[2];
            }
            catch
            {
                HelperFunction.MsgBox(this, this.GetType(), "Wrong Date Format");

            }
        }
    }
    protected void txtToDate_TextChanged(object sender, EventArgs e)
    {
        if (txtToDate.Text != "")
        {
            try
            {
                string[] nepdate = hf.ConvertEnglishToNepali(txtToDate.Text);
                txtTDay.Text = nepdate[0];
                txtTMonth.Text = nepdate[1];
                txtTYear.Text = nepdate[2];
            }
            catch
            {
                HelperFunction.MsgBox(this, this.GetType(), "Wrong Date Format");

            }
        }
    }


    protected string GetFromEnglishDate()
    {
        string engdate = "";
        if (txtFDay.Text != "" && txtFMonth.Text != "" && txtFYear.Text != "")
        {
            try
            {
                engdate = hf.ConvertNepaliTOEnglish(txtFDay.Text, txtFMonth.Text, txtFYear.Text);


            }
            catch
            {
                HelperFunction.MsgBox(this, this.GetType(), "Wrong Date Format");

            }
        }
        return engdate;
    }

    protected string GetToEnglishDate()
    {
        string engdate = "";
        if (txtTDay.Text != "" && txtTMonth.Text != "" && txtTYear.Text != "")
        {
            try
            {
                engdate = hf.ConvertNepaliTOEnglish(txtTDay.Text, txtTMonth.Text, txtTYear.Text);


            }
            catch
            {
                HelperFunction.MsgBox(this, this.GetType(), "Wrong Date Format");

            }
        }
        return engdate;
    }

}