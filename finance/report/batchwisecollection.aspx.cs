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

public partial class finance_report_batchwisecollection : System.Web.UI.Page
{

    DataTable dt = new DataTable();

    EntityList theList = new EntityList();

    static string batch = "";

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

            batch = imgbtnYearCollection.ToolTip;
            dt = new DataTable();
            dt = GetTotalClassCollection(batch);
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


    protected void imgbtnMainLedger_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton imgbtnClassCollection = (ImageButton)sender;

        GridView GVClassWise = imgbtnClassCollection.Parent.FindControl("GVClassWise") as GridView;
        string picture = "";
        string imgurl = imgbtnClassCollection.ImageUrl.ToString();
        int lastInd = imgurl.LastIndexOf("/");
        string address = imgurl.Substring(0, lastInd + 1);
        picture = imgurl.Substring(lastInd + 1);
        if (picture == "plus.gif")
        {
            picture = "minus.gif";


            string clas = imgbtnClassCollection.ToolTip;
            if (clas != "" && batch != "")
            {
                GVClassWise.DataSource = GetTotalYearlyClsCollection(clas, batch);
                GVClassWise.DataBind();
                GVClassWise.Visible = true;
            }
            else
            {
                picture = "plus.gif";
            }
        }
        else
        {
            GVClassWise.Visible = false;
            picture = "plus.gif";
        }
        imgurl = address + picture;
        imgbtnClassCollection.ImageUrl = imgurl;

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
        objCmd.CommandText = "PKJ_REPORTS.BatchWiseCollection";


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


    protected DataTable GetTotalClassCollection(string batch)
    {


        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();


        OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "PKJ_REPORTS.batchsemesterwisecollection";


        objCmd.Connection = cn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter pram2 = new OracleParameter("VAR_BATCH", OracleDbType.Varchar2);
        pram2.Direction = ParameterDirection.Input;
        pram2.Value = batch;
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




    protected DataTable GetTotalYearlyClsCollection(string clas, string batch)
    {

        OracleDataReader ora_reader;
        DataTable dtable = new DataTable();


        OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        OracleCommand objCmd = new OracleCommand();
        objCmd.CommandText = "PKJ_REPORTS.ClassParticularCollection";


        objCmd.Connection = cn;
        objCmd.CommandType = CommandType.StoredProcedure;

        OracleParameter pram1 = new OracleParameter("VAR_CLASS", OracleDbType.Varchar2);
        pram1.Direction = ParameterDirection.Input;
        pram1.Value = clas;
        objCmd.Parameters.Add(pram1);

        OracleParameter pram2 = new OracleParameter("VAR_BATCH", OracleDbType.Varchar2);
        pram2.Direction = ParameterDirection.Input;
        pram2.Value = batch;
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

    //protected void GVClassWise_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        Label lblAmount = e.Row.FindControl("lblAmount") as Label;
    //        lblAmount.Text = Convert.ToDouble(lblAmount.Text).ToString("#,###.00");

    //    }
    //}
}