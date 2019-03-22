using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Web.Script.Services;
using System.Configuration;

using System.Data.OleDb;

using Oracle.DataAccess.Client;

/// <summary>
/// Summary description for ProductServices
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
[ScriptService]
public class ProductServices : System.Web.Services.WebService {

    public ProductServices () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }


    [WebMethod]
    public string[] get_product_name(string prefixText)
    {

   
        OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        string sql = "select PRODUCT_NAME from product where upper(PRODUCT_NAME) like upper('" + prefixText + "%')";
            
           
        OracleDataAdapter da = new OracleDataAdapter(sql, cn);
        // da.SelectCommand.Parameters.Add("@prefixText",OracleDbType.varchar2, 50).Value = prefixText + "%";

        //added by anil
        da.SelectCommand.Parameters.Add("@prefixText", OracleDbType.Varchar2, 500).Value = prefixText + "%";
        //changes ends
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["PRODUCT_NAME"].ToString(), i);
            i++;
        }
        cn.Close();
        return items;
    }

    [WebMethod]
    public string[] get_disposableproduct_name(string prefixText)
    {


        OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        string sql = "select PRODUCT_NAME from product where upper(PRODUCT_NAME) like upper('" + prefixText + "%') AND NATURE_CATEGORY='Disposable'";


        OracleDataAdapter da = new OracleDataAdapter(sql, cn);
        // da.SelectCommand.Parameters.Add("@prefixText",OracleDbType.varchar2, 50).Value = prefixText + "%";

        //added by anil
        da.SelectCommand.Parameters.Add("@prefixText", OracleDbType.Varchar2, 500).Value = prefixText + "%";
        //changes ends
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["PRODUCT_NAME"].ToString(), i);
            i++;
        }
        cn.Close();
        return items;
    }

    [WebMethod]
    public string[] get_nondisposableproduct_name(string prefixText)
    {


        OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        string sql = "select PRODUCT_NAME from product where upper(PRODUCT_NAME) like upper('" + prefixText + "%') AND NATURE_CATEGORY='Non Disposable'";


        OracleDataAdapter da = new OracleDataAdapter(sql, cn);
        // da.SelectCommand.Parameters.Add("@prefixText",OracleDbType.varchar2, 50).Value = prefixText + "%";

        //added by anil
        da.SelectCommand.Parameters.Add("@prefixText", OracleDbType.Varchar2, 500).Value = prefixText + "%";
        //changes ends
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["PRODUCT_NAME"].ToString(), i);
            i++;
        }
        cn.Close();
        return items;
    }
}
