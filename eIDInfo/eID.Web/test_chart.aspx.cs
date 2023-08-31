using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using ChartJsHelper;
public partial class test_chart : System.Web.UI.Page
{
    private void BindJqFunctions()
    {
        //jqFunctions
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCompanyList();
        }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    private void BindCompanyList()
    {
        ddlCompany.Items.Clear();
        List<CompanyInfoTB> data = SPCommon.DDLCompanyBind(Session["UserType"].ToString(), Session["TenantId"].ToString(), Session["CompanyID"].ToString());
        if (data != null && data.Count() > 0)
        {

            ddlCompany.DataSource = data;
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataValueField = "CompanyId";
            ddlCompany.DataBind();
        }
        ddlCompany.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    [WebMethod]

    public static List<chartSourceData> getTrafficSourceData(string companyId)
    {
        List<chartSourceData> t = new List<chartSourceData>();
        string[] arrColor = new string[] { "#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850","#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850",
                    "#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850","#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850",
                    "#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850","#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850",
                    "#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850","#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850",
                    "#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850","#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850",
                    "#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850","#3e95cd", "#8e5ea2","#3cba9f","#e8c3b9","#c45850"};

        Genreal gen = new Genreal();
        string query = string.Format(@"SELECT        COUNT(E.EmployeeId) AS Cnt, 'Total' AS Type
FROM            EmployeeTB AS E LEFT OUTER JOIN
                         DeviceLogsTB AS D ON D.EmpID = E.EmployeeId
WHERE        (CONVERT(date, D.AttendDate) = CONVERT(date, GETDATE())) OR D.AttendDate IS NULL AND (E.TenantId = '{0}') AND (E.CompanyId = '{1}')
UNION ALL
SELECT         COUNT(D.AttendDate)  AS Cnt, 'Presents' AS Type
FROM            EmployeeTB AS E LEFT OUTER JOIN
                         DeviceLogsTB AS D ON D.EmpID = E.EmployeeId
WHERE        (CONVERT(date, D.AttendDate) = CONVERT(date, GETDATE())) OR D.AttendDate IS NULL AND (E.TenantId = '{0}') AND (E.CompanyId = '{1}')
UNION ALL
SELECT        COUNT(E.EmployeeId)- COUNT(D.AttendDate)  AS Cnt, 'Absents' AS Type
FROM            EmployeeTB AS E LEFT OUTER JOIN
                         DeviceLogsTB AS D ON D.EmpID = E.EmployeeId
WHERE        (CONVERT(date, D.AttendDate) = CONVERT(date, GETDATE())) OR D.AttendDate IS NULL AND (E.TenantId = '{0}') AND (E.CompanyId = '{1}')
UNION ALL
SELECT        COUNT(E.EmployeeId)  AS Cnt, 'OnLeaves' AS Type
FROM            LeaveApplicationsTB AS E 
WHERE        (CONVERT(date,GETDATE()) BETWEEN CONVERT(date, E.LeaveStartDate) AND CONVERT(date, E.LeaveEndDate)) AND (E.TenantId = '{0}') AND (E.CompanyId = '{1}')", HttpContext.Current.Session["TenantId"].ToString(), companyId);
    
        DataTable data = gen.ReturnData(query);
        int counter = 0;
        foreach (DataRow item in data.Rows)
        {
            chartSourceData tsData = new chartSourceData();
            tsData.value = item["Cnt"].ToString();
            tsData.label = item["Type"].ToString();
            tsData.color = arrColor[counter];
            t.Add(tsData);
            counter++;
        }
        
        return t;
    }
    public class chartSourceData
    {
        public string label { get; set; }
        public string value { get; set; }
        public string color { get; set; }
        public string hightlight { get; set; }
    }

}