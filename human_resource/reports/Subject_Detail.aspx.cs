using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Entity.Components;
using Service.Components;
using System.IO;

public partial class human_resource_reports_Subject_Detail : System.Web.UI.Page
{
    TEACHER_SUBJECT_MAPPING TSMEnt = new TEACHER_SUBJECT_MAPPING();
    TEACHER_SUBJECT_MAPPINGService TSMSrv = new TEACHER_SUBJECT_MAPPINGService();

    HSS_SUBJECT SUBEnt = new HSS_SUBJECT();
    HSS_SUBJECTService SUBSrv = new HSS_SUBJECTService();

    program PEnt = new program();
    programService PSrv = new programService();

    semester SEnt = new semester();
    semesterService SSrv = new semesterService();


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadSubject();
        }

    }
    protected void LoadSubject()
    {

        TSMEnt = new TEACHER_SUBJECT_MAPPING();
        TSMEnt.TEACHER_ID =Request.QueryString["empId"];
        gridSubject.DataSource = TSMSrv.GetAll(TSMEnt);
        gridSubject.DataBind();


    }

    protected void gridSubject_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblProgram = e.Row.FindControl("lblProgram") as Label;
            Label lblSubjectName = e.Row.FindControl("lblSubjectName") as Label;
            Label lblSemesterN = e.Row.FindControl("lblSemesterN") as Label;
            Label lblSubjectID = e.Row.FindControl("lblSubjectID") as Label;
            Label lblEmpID = e.Row.FindControl("lblEmpID") as Label;
            Label lblSemester = e.Row.FindControl("lblSemester") as Label;


            SUBEnt = new HSS_SUBJECT();
            SUBEnt.PK_ID = lblSubjectID.Text;
            SUBEnt = (HSS_SUBJECT)SUBSrv.GetSingle(SUBEnt);
            if (SUBEnt != null)
            {
                lblSubjectName.Text = SUBEnt.SUBJECT_NAME;

                PEnt = new program();
                PEnt.PK_ID = SUBEnt.PROGRAM;
                PEnt = (program)PSrv.GetSingle(PEnt);
                if (PEnt != null)
                {


                    lblProgram.Text = PEnt.PROGRAM_CODE;
                }
            }

            TSMEnt = new TEACHER_SUBJECT_MAPPING();
            TSMEnt.SEMESTER = lblSemester.Text;
            TSMEnt = (TEACHER_SUBJECT_MAPPING)TSMSrv.GetSingle(TSMEnt);
            if (TSMEnt != null)
            {
                SEnt = new semester();
                SEnt.PK_ID = TSMEnt.SEMESTER;
                SEnt = (semester)SSrv.GetSingle(SEnt);
                if (SEnt != null)
                {

                    lblSemesterN.Text = SEnt.SEMESTER_CODE;
                    
                }

            }


            //SUBEnt = new HSS_SUBJECT();
            //SUBEnt.PK_ID = lblSubjectID.Text;
            //SUBEnt = (HSS_SUBJECT)SUBSrv.GetSingle(SUBEnt);
            //if (SUBEnt != null)
            //{

            //}

        }
    }
    protected void gridSubject_RowCommand(object sender, GridViewCommandEventArgs e)
    {

    }
}