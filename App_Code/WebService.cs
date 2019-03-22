using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data;

using System.Configuration;
using Oracle.DataAccess.Client;

/// <summary>
/// Summary description for WebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class WebService : System.Web.Services.WebService
{

    public WebService()
    {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string[] GetStudent(string prefixText, int count, string contextKey)
    {
        string[] xyz = contextKey.Split('~');
        string abc = ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString.ToString();
        OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString);
        string sql = "select std.NAME_ENGLISH || '~' || std.STUDENT_ID stdname from hss_student std " +
                        "inner join hss_current_student cs on (cs.STUDENT_ID=std.STUDENT_ID)" +
                        "where  Upper(std.NAME_ENGLISH) like upper('" + prefixText + "%')" +
                        "AND cs.CLASS='" + xyz[0] + "'" +
                        "AND cs.Year='" + xyz[1] + "'";
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
            items.SetValue(dr["stdname"].ToString(), i);
            i++;
        }
        cn.Close();
        return items;
    }

    [WebMethod]
    public string[] get_book_name(string prefixText)
    {
        OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnMIS"].ConnectionString);
        string sql = "Select bookname from book where Upper(bookname) like upper('%" + prefixText + "%')";

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
            items.SetValue(dr["bookname"].ToString(), i);
            i++;
        }
        cn.Close();
        return items;
    }


    [WebMethod]
    public string[] GetEmployees(string prefixText, int count, string contextKey)
    {
        string abc = ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString.ToString();
        OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString);
        string sql = "Select firstname from employees where Upper(firstname) like upper('" + prefixText + "%') and officecode='" + contextKey + "'";
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
            items.SetValue(dr["firstname"].ToString(), i);
            i++;
        }
        cn.Close();
        return items;
    }

    [WebMethod]
    public string[] GetOfficeposition(string prefixText, int count, string contextKey)
    {
        string abc = ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString.ToString();
        OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString);
        string sql = "select d.DESIGNATIONNAME ||', ' || l.LEVELNAME || ', ' ||s.SERVICENAME || ', ' ||GROUPNAME ||', '||sg.SUBGROUPNAME || '~' ||OAP.SERIALNO AS Officeposition" +
            " from OFFICEEMPAVAILPOSITION OAP " +
            " inner join Designation D on(OAP.DESIGNATIONID=d.DESIGNATIONID) " +
            " left outer join EMPLOYEELEVEL L on(OAP.EMPLEVELID=L.LEVELID) " +
            " left outer join empservice S on(OAP.SERVICEID=S.SERVICEID) " +
            " left outer join EMPGROUP G on (OAP.EMPGROUPID=G.GROUPID) " +
            " left outer join empSubGroup SG on(OAP.EMPSUBGROUPID=sg.SUBGROUPID) " +
            " where d.DESIGNATIONNAME ||', ' || l.LEVELNAME || ', ' ||s.SERVICENAME || ', ' ||GROUPNAME ||', '||sg.SUBGROUPNAME || '~' ||OAP.SERIALNO like'" + prefixText + "%' " +
            " and OAP.OFFICECODE='" + contextKey + "'";
        OracleDataAdapter da = new OracleDataAdapter(sql, cn);

        //da.SelectCommand.Parameters.Add("@prefixText", OracleDbType.Varchar2, 500).Value = prefixText + "%";
        //da.SelectCommand.Parameters.Add("@party_Type", OracleDbType.Varchar2, 50).Value = contextKey;
        //changes ends
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];

        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["Officeposition"].ToString(), i);
            i++;
        }
        //for(int i =0;i<10;i++)
        //{
        //    DataRow dr = dt.Rows[i];
        //    items.SetValue(dr["PartyName"].ToString(), i);
        //}
        cn.Close();
        return items;
    }

    [WebMethod]
    public string[] GetOfficeNames(string prefixText)
    {
        string abc = ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString.ToString();
        OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString);
        string sql = "Select OfficeName as OfficeName from Office where OfficeName like upper('" + prefixText + "%')";
        OracleDataAdapter da = new OracleDataAdapter(sql, cn);

        da.SelectCommand.Parameters.Add("@prefixText", OracleDbType.Varchar2, 500).Value = prefixText + "%";
        //changes ends
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["OfficeName"].ToString(), i);
            i++;
        }
        cn.Close();
        return items;
    }

    [WebMethod]
    public string[] GetOfficeTypes(string prefixText)
    {
        string abc = ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString.ToString();
        OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString);
        string sql = "select officetypename from OfficeType where officetypename like upper('" + prefixText + "%')";
        OracleDataAdapter da = new OracleDataAdapter(sql, cn);

        da.SelectCommand.Parameters.Add("@prefixText", OracleDbType.Varchar2, 500).Value = prefixText + "%";
        //changes ends
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["officetypename"].ToString(), i);
            i++;
        }
        cn.Close();
        return items;
    }

    [WebMethod]
    public string[] GetDepartmentsNames(string prefixText)
    {
        string abc = ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString.ToString();
        OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString);
        string sql = "Select DEPARTMENTNAME from Departments where DEPARTMENTNAME like upper('" + prefixText + "%')";
        OracleDataAdapter da = new OracleDataAdapter(sql, cn);

        da.SelectCommand.Parameters.Add("@prefixText", OracleDbType.Varchar2, 500).Value = prefixText + "%";
        //changes ends
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["DEPARTMENTNAME"].ToString(), i);
            i++;
        }
        cn.Close();
        return items;
    }

    [WebMethod]
    public string[] GetLevelNames(string prefixText)
    {
        string abc = ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString.ToString();
        OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString);
        string sql = "Select LEVELNAME from EmployeeLevel where LEVELNAME like upper('" + prefixText + "%')";
        OracleDataAdapter da = new OracleDataAdapter(sql, cn);

        da.SelectCommand.Parameters.Add("@prefixText", OracleDbType.Varchar2, 500).Value = prefixText + "%";
        //changes ends
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["LEVELNAME"].ToString(), i);
            i++;
        }
        cn.Close();
        return items;
    }

    [WebMethod]
    public string[] GetGroupNames(string prefixText)
    {
        string abc = ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString.ToString();
        OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString);
        string sql = "Select groupname from empgroup where groupname like upper('" + prefixText + "%')";
        OracleDataAdapter da = new OracleDataAdapter(sql, cn);

        da.SelectCommand.Parameters.Add("@prefixText", OracleDbType.Varchar2, 500).Value = prefixText + "%";
        //changes ends
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["groupname"].ToString(), i);
            i++;
        }
        cn.Close();
        return items;
    }

    [WebMethod]
    public string[] GetServiceNames(string prefixText)
    {
        string abc = ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString.ToString();
        OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString);
        string sql = "Select servicename from empservice where servicename like upper('" + prefixText + "%')";
        OracleDataAdapter da = new OracleDataAdapter(sql, cn);

        da.SelectCommand.Parameters.Add("@prefixText", OracleDbType.Varchar2, 500).Value = prefixText + "%";
        //changes ends
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["servicename"].ToString(), i);
            i++;
        }
        cn.Close();
        return items;
    }

    [WebMethod]
    public string[] GetSubGroupNames(string prefixText)
    {
        string abc = ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString.ToString();
        OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString);
        string sql = "Select subgroupname from empsubgroup where subgroupname like upper('" + prefixText + "%')";
        OracleDataAdapter da = new OracleDataAdapter(sql, cn);

        da.SelectCommand.Parameters.Add("@prefixText", OracleDbType.Varchar2, 500).Value = prefixText + "%";
        //changes ends
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["subgroupname"].ToString(), i);
            i++;
        }
        cn.Close();
        return items;
    }

    [WebMethod]
    public string[] GetPositionNames(string prefixText)
    {
        string abc = ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString.ToString();
        OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString);
        string sql = "Select designationname from designation where designationname like upper('" + prefixText + "%')";
        OracleDataAdapter da = new OracleDataAdapter(sql, cn);

        da.SelectCommand.Parameters.Add("@prefixText", OracleDbType.Varchar2, 500).Value = prefixText + "%";
        //changes ends
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["designationname"].ToString(), i);
            i++;
        }
        cn.Close();
        return items;
    }

    [WebMethod]
    public string[] GetZones(string prefixText)
    {
        string abc = ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString.ToString();
        OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString);
        string sql = "Select ZoneName from Zone where ZoneName like upper('" + prefixText + "%')";
        OracleDataAdapter da = new OracleDataAdapter(sql, cn);

        da.SelectCommand.Parameters.Add("@prefixText", OracleDbType.Varchar2, 500).Value = prefixText + "%";
        //changes ends
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["ZoneName"].ToString(), i);
            i++;
        }
        cn.Close();
        return items;
    }

    [WebMethod]
    public string[] GetDistricts(string prefixText)
    {
        string abc = ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString.ToString();
        OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString);
        string sql = "Select districtname from District where districtname like upper('" + prefixText + "%')";
        OracleDataAdapter da = new OracleDataAdapter(sql, cn);

        da.SelectCommand.Parameters.Add("@prefixText", OracleDbType.Varchar2, 500).Value = prefixText + "%";
        //changes ends
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["districtname"].ToString(), i);
            i++;
        }
        cn.Close();
        return items;
    }

    [WebMethod]
    public string[] GetEmployeeNames(string prefixText)
    {
        string abc = ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString.ToString();
        OracleConnection cn = new OracleConnection(ConfigurationManager.ConnectionStrings["cnNOC"].ConnectionString);
        string sql = " select firstname ||' ' || lastname name  from employees where firstname  like upper('" + prefixText + "%')";
        OracleDataAdapter da = new OracleDataAdapter(sql, cn);

        da.SelectCommand.Parameters.Add("@prefixText", OracleDbType.Varchar2, 500).Value = prefixText + "%";
        //changes ends
        DataTable dt = new DataTable();
        da.Fill(dt);

        string[] items = new string[dt.Rows.Count];
        int i = 0;
        foreach (DataRow dr in dt.Rows)
        {
            items.SetValue(dr["name"].ToString(), i);
            i++;
        }
        cn.Close();
        return items;
    }

}

