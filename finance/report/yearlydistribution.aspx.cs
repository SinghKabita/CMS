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
using NCCSEncryption;
using DataAccess.Framework;
using DataHelper.Framework;
using Oracle.DataAccess.Client;

public partial class finance_report_yearlydistribution : System.Web.UI.Page
{

    DataTable dt = new DataTable();

    EntityList theList = new EntityList();

    static string year = "";

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            LoadTotalCollection();
        }
    }
    protected void imgbtn_Click(object sender, ImageClickEventArgs e)
    {
        RepeaterItem Item = ((ImageButton)sender).Parent as RepeaterItem;
        ImageButton imgbtn = (ImageButton)sender;
        HtmlGenericControl DivYearCollection = Item.FindControl("DivYearCollection") as HtmlGenericControl;
        string picture = "";
        string imgurl = imgbtn.ImageUrl.ToString();
        int lastInd = imgurl.LastIndexOf("/");
        string address = imgurl.Substring(0, lastInd + 1);
        picture = imgurl.Substring(lastInd + 1);
        if (picture == "plus.gif")
        {
            picture = "minus.gif";
            DivYearCollection.Visible = true;
            Repeater rptrYearCollection = Item.FindControl("rptrYearCollection") as Repeater;

            System.Web.UI.HtmlControls.HtmlTableCell tdCode = new System.Web.UI.HtmlControls.HtmlTableCell();
            tdCode = (System.Web.UI.HtmlControls.HtmlTableCell)Item.FindControl("tdCode");


            dt = new DataTable();
            dt = GetTotalYearlyCollection();
            rptrYearCollection.DataSource = dt;
            rptrYearCollection.DataBind();


        }
        else
        {
            DivYearCollection.Visible = false;
            picture = "plus.gif";

        }
        imgurl = address + picture;
        imgbtn.ImageUrl = imgurl;
    }

    protected void imgbtnClass_Click(object sender, ImageClickEventArgs e)
    {
        RepeaterItem Item = ((ImageButton)sender).Parent as RepeaterItem;
        ImageButton imgbtnYearCollection = (ImageButton)sender;
        HtmlGenericControl DivClassCollection = Item.FindControl("DivClassCollection") as HtmlGenericControl;
        string picture = "";
        string imgurl = imgbtnYearCollection.ImageUrl.ToString();
        int lastInd = imgurl.LastIndexOf("/");
        string address = imgurl.Substring(0, lastInd + 1);
        picture = imgurl.Substring(lastInd + 1);
        if (picture == "plus.gif")
        {
            picture = "minus.gif";
            DivClassCollection.Visible = true;
            Repeater rptrClassCollection = Item.FindControl("rptrClassCollection") as Repeater;

            System.Web.UI.HtmlControls.HtmlTableCell tdCode = new System.Web.UI.HtmlControls.HtmlTableCell();
            tdCode = (System.Web.UI.HtmlControls.HtmlTableCell)Item.FindControl("tdCode");

            year = imgbtnYearCollection.ToolTip;
            dt = new DataTable();
            dt = GetTotalYearlySemesterCollection(year);
            rptrClassCollection.DataSource = dt;
            rptrClassCollection.DataBind();


        }
        else
        {
            DivClassCollection.Visible = false;
            picture = "plus.gif";

        }
        imgurl = address + picture;
        imgbtnYearCollection.ImageUrl = imgurl;
    }



    protected void LoadTotalCollection()
    {
        dt = new DataTable();
        dt = GetTotalCollection();
        rptrTotalCollection.DataSource = dt;
        rptrTotalCollection.DataBind();
    }

    protected DataTable GetTotalCollection()
    {

        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();


        OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "PKJ_REPORTS.TotalCollection";


        objCmd.Connection = cn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        cn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        cn.Close();

        if (dtable != null)
            return dtable;
        else
            return null;


    }



    protected DataTable GetTotalYearlyCollection()
    {


        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();


        OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "PKJ_REPORTS.yearwisecollection";


        objCmd.Connection = cn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        cn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        cn.Close();

        if (dtable != null)
            return dtable;
        else

            return null;


    }


    protected DataTable GetTotalYearlySemesterCollection(string year)
    {


        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();


        OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "PKJ_REPORTS.yearsemesterwisecollection";


        objCmd.Connection = cn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter pram2 = new OracleParameter("VAR_YEAR", OracleDbType.Varchar2);
        pram2.Direction = ParameterDirection.Input;
        pram2.Value = year;
        objCmd.Parameters.Add(pram2);

        OracleParameter result = new OracleParameter("result", OracleDbType.RefCursor);
        result.Direction = ParameterDirection.Output;
        objCmd.Parameters.Add(result);

        cn.Open();
        ora_reader = objCmd.ExecuteReader();
        dtable.Load(ora_reader);
        cn.Close();

        if (dtable != null)
            return dtable;
        else

            return null;


    }








    protected void rptrTotalCollection_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        Label lblTotalAmount = (Label)e.Item.FindControl("lblTotalAmount");


        lblTotalAmount.Text = Convert.ToDouble(lblTotalAmount.Text).ToString("#,###.00");



    }

    protected void rptrYearCollection_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        Label lblBatchWiseTotal = (Label)e.Item.FindControl("lblBatchWiseTotal");

        lblBatchWiseTotal.Text = Convert.ToDouble(lblBatchWiseTotal.Text).ToString("#,###.00");

    }
    protected void rptrClassCollection_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {


        Label lblClassWiseTotal = (Label)e.Item.FindControl("lblClassWiseTotal");
        lblClassWiseTotal.Text = Convert.ToDouble(lblClassWiseTotal.Text).ToString("#,###.00");
    }


}