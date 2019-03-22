using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using Entity.Components;
using Entity.Framework;
using Service.Components;
using DataHelper.Framework;

public partial class exam_report_MarksRecord : System.Web.UI.Page
{
    BatchYear BTEnt = new BatchYear();
    BatchYearService BTSer = new BatchYearService();

    HSS_STUDENT STDEnt = new HSS_STUDENT();
    HSS_STUDENTService STDSer = new HSS_STUDENTService();

    HSS_CURRENT_STUDENT CSUBEnt = new HSS_CURRENT_STUDENT();
    HSS_CURRENT_STUDENTService CSSer = new HSS_CURRENT_STUDENTService();

    EXAM_MARKS EMEnt = new EXAM_MARKS();
    EXAM_MARKSService EMSer = new EXAM_MARKSService();

    EXAM_TYPE ETEnt = new EXAM_TYPE();
    EXAM_TYPEService ETSer = new EXAM_TYPEService();

    HSS_SUBJECT SUBEnt = new HSS_SUBJECT();
    HSS_SUBJECTService SUBSer = new HSS_SUBJECTService();

    Section SecEnt = new Section();
    SectionService SecSer = new SectionService();

    hss_faculty FCTEnt = new hss_faculty();
    hss_facultyService FCTSer = new hss_facultyService();

    program PEnt = new program();
    programService PSer = new programService();

    semester SMEnt = new semester();
    semesterService SMSer = new semesterService();

    full_pass_marks FPMEnt = new full_pass_marks();
    full_pass_marksService FPMSer = new full_pass_marksService();

    HelperFunction hf = new HelperFunction();



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadBatch();
            LoadExamType();
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
        ddlSemester.Items.Insert(0, "Select");
        ddlExamType.Items.Insert(0, "Select");
       

    }
    protected void LoadExamType()
    {
        ETEnt = new EXAM_TYPE();

        ddlExamType.DataSource = ETSer.GetAll(ETEnt);
        ddlExamType.DataTextField = "EXAMTYPE";
        ddlExamType.DataValueField = "PKID";
        ddlExamType.DataBind();
        ddlExamType.Items.Insert(0, "Select");
    }


    protected void LoadSemester()
    {
        BTEnt = new BatchYear();
        BTEnt.BATCH = ddlBatch.SelectedValue;
        BTEnt = (BatchYear)BTSer.GetSingle(BTEnt);
        if (BTEnt != null)
        {
            SMEnt = new semester();
            SMEnt.SYLLABUS_YEAR = BTEnt.SYLLABUS_YEAR;
            ddlSemester.DataSource = SMSer.GetAll(SMEnt);
            ddlSemester.DataTextField = "SEMESTER_CODE";
            ddlSemester.DataValueField = "PK_ID";
            ddlSemester.DataBind();
            ddlSemester.Items.Insert(0, "Select");

        }

    }
    protected void ddlBatch_SelectedIndexChanged(object sender, EventArgs e)
    {
        LoadSemester();
    }
    protected void btnView_Click(object sender, EventArgs e)
    {

    }
}