using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class LeaveSchoolReport : System.Web.UI.Page
{
    Genreal g = new Genreal();
    HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
    private void BindJqFunctions()
    {
        //jqFunctions
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                BindCompanyList();
                BindDepartmentList();
                BindEmployeeList();
                BindDataGridView();

                if (Session["UserType"].ToString() != "User")
                {
                    company.Visible = true;
                    department.Visible = true;
                    employee.Visible = true;
                }
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
        if (gv.Rows.Count > 0)
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        BindJqFunctions();
    }

    private void BindDepartmentList()
    {
        int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
        ddlDepartment.Items.Clear();
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.MasterDeptTBs
                        where d.CompanyId == cId && d.TenantId == Convert.ToString(Session["TenantId"])
                        select new { d.DeptName, d.DeptID }).Distinct();
            if (data != null && data.Count() > 0)
            {
                ddlDepartment.DataSource = data;
                ddlDepartment.DataTextField = "DeptName";
                ddlDepartment.DataValueField = "DeptID";
                ddlDepartment.DataBind();
            }

        }
        ddlDepartment.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployeeList();
    }

    private void BindDataGridView()
    {
        try
        {
            bool AdminStatus = Session["UserType"].ToString() == "User" ? false : true;
            litMessage.Text = string.Format("<tr><td>Before Conversion Dates</td><td>From Date : {0} </td><td> To Date : {1}</td></tr>", txtFromDate.Text, txtToDate.Text);
            string query = string.Format(@"SELECT       e.FName+' '+e.Lname AS EmpName , E.EmployeeNo,E.AccessCardNo,E.WorkLocation,E.CurPin , E.MachineID ,CONVERT(varchar, AL.AttendanceDate, 101) AS Date,
                                        E.ContactNo,E.AltContactNo, AL.InTime AS PunchIn, AL.OutTime AS PunchOut,  AL.Status, com.CompanyName, Dp.DeptName
                                    FROM            AttendaceLogTB AS AL INNER JOIN
                         EmployeeTB AS E ON AL.EmployeeId = E.EmployeeId Left Join 
						 CompanyInfoTB As com ON AL.CompanyId = E.CompanyId Left Join
						 MasterDeptTB As Dp ON Dp.DeptID = E.DeptId 
                        WHERE     (E.IsActive=1) AND   (AL.Status IN ('L')) AND (CONVERT(date,AL.AttendanceDate) BETWEEN CONVERT(date,'{0}') AND CONVERT(date,'{1}'))  AND AL.TenantId='{2}'", txtFromDate.Text, txtToDate.Text,
                        Convert.ToString(Session["TenantId"]));

            if (ddlCompany.SelectedIndex > 0)
            {
                query += " AND (com.CompanyId = '" + ddlCompany.SelectedValue + "')";
            }
            if (ddlDepartment.SelectedIndex > 0)
            {
                query += " AND (E.DeptID = '" + ddlDepartment.SelectedValue + "')";
            }
            if (ddlEmployee.SelectedIndex > 0)
            {
                query += " AND (E.EmployeeId = '" + ddlEmployee.SelectedValue + "')";
            }
            if (Session["UserType"].ToString() == "User")
            {
                query += " AND (E.EmployeeId = '" + Convert.ToInt32(Session["EmpId"]) + "')";
            }
            if (AdminStatus == false)
            {
                query += " AND E.EmployeeId=" + Convert.ToInt32(Session["EmpId"]);
            }
            litMessage.Text += string.Format("<tr><td>Before Conversion Query</td><td colspan='2'>{0}</td></tr>", query);
            DateTime fromDate = string.IsNullOrEmpty(txtFromDate.Text) ? DateTime.Now : Genreal.GetDate(txtFromDate.Text);
            DateTime toDate = string.IsNullOrEmpty(txtToDate.Text) ? DateTime.Now : Genreal.GetDate(txtToDate.Text);
            litMessage.Text += string.Format("<tr><td>After Conversion Dates</td><td>From Date : {0} </td><td> To Date : {1}</td></tr>", fromDate, toDate);
            //query += string.Format(" AND (CONVERT(date, AL.AttendanceDate) BETWEEN CONVERT(date, '{0}') AND CONVERT(date, '{1}'))", txtFromDate.Text, txtToDate.Text);
            litMessage.Text += string.Format("<tr><td>After Conversion Query</td><td colspan='2'>{0}</td></tr>", query);

            DataTable dataTable = g.ReturnData(query);
            gv.DataSource = dataTable;
            gv.DataBind();
        }
        catch (Exception ex)
        {
            Console.WriteLine(txtToDate.Text);
        }
    }



    protected void btnsearch_Click(object sender, EventArgs e)
    {
        BindDataGridView();
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentList();
        BindEmployeeList();
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

    private void BindEmployeeList()
    {
        bool AdminStatus = Session["UserType"].ToString() == "User" ? false : true;
        int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.EmployeeTBs
                        where d.IsActive == true && (d.RelivingStatus == null || d.RelivingStatus == 0)
                        && d.CompanyId == cId
                        select new
                        {
                            d.EmployeeId,
                            EmpName = d.FName + " " + d.Lname
                        }).Distinct();

            if (AdminStatus == false)
            {
                data = data.Where(d => d.EmployeeId == Convert.ToInt32(Session["EmpId"])).Distinct();
            }

            ddlEmployee.DataSource = data;
            ddlEmployee.DataBind();
            ddlEmployee.Items.Insert(0, "--Select--");
        }
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (gv.Rows.Count > 0)
        {
            gv.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            gv.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }




}