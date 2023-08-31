using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class view_employee_roster : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    private void BindJqFunctions()
    {
        //jqFunctions
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                if (!IsPostBack)
                {
                    BindDataGridView();
                }
            }
        }
    }
    private void BindDataGridView()
    {
        using(HrPortalDtaClassDataContext db=new HrPortalDtaClassDataContext())
        {
            int cId =Convert.ToInt32(Request.QueryString["id"]);
            string query = @"SELECT        E.FName + ' ' + E.Lname AS EmpName, E.EmployeeNo, RD.Type, RD.Date,Day
FROM            RosterDetailsTB AS RD INNER JOIN
                         EmployeeTB AS E ON RD.EmpID = E.EmployeeId
WHERE      (E.IsActive=1) AND  (RD.RosterId = " + cId + ")";
            DataTable dataTable = gen.ReturnData(query);
            
            
            datalistdisplay.DataSource = dataTable;
            datalistdisplay.DataBind();

        }
    }
}