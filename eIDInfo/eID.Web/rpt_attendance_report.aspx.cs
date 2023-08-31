
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Microsoft.Reporting.WebForms;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class rpt_attendance_report : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                BindYear();
                BindCompanyList();
                BindDepartmentList();
                BindEmployeeList();
                ddmonth.SelectedIndex = DateTime.Now.Month;


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
            Response.Redirect("login.aspx");
        }
    }
    
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentList();
        BindEmployeeList();
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        bool AdminStatus = Session["UserType"].ToString() == "User" ? false : true;
        //All Com Data..
        var empdata = (from d in HR.EmployeeTBs
                       join d1 in HR.MasterDeptTBs on d.DeptId equals d1.DeptID where d.TenantId == Convert.ToString(Session["TenantId"]) select new {
                           d1.DeptName,
                           d.CompanyId,
                           d.EmployeeId,
                           d.TenantId,
                           d.FName,d.Lname,
                           d.DeptId
                       }).Distinct();

        var Atdlog = (from d in HR.AttendaceLogTBs where d.TenantId==Convert.ToString(Session["TenantId"]) select d).Distinct();


        if (ddlCompany.SelectedIndex > 0)
        {
            empdata = empdata.Where(d => d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue));
            Atdlog = Atdlog.Where(d => d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue));
        }
        if (ddlDepartment.SelectedIndex > 0)
        {
            empdata = empdata.Where(d => d.DeptId == Convert.ToInt32(ddlDepartment.SelectedValue));
        }
        if (ddlEmployee.SelectedIndex > 0)
        {
            empdata = empdata.Where(d => d.EmployeeId == Convert.ToInt32(ddlEmployee.SelectedValue));
            Atdlog = Atdlog.Where(d => d.EmployeeId == Convert.ToInt32(ddlEmployee.SelectedValue));
        }
        if (AdminStatus == false)
        {
            empdata = empdata.Where(d => d.EmployeeId == Convert.ToInt32(Session["EmpId"]));
            Atdlog = Atdlog.Where(d => d.EmployeeId == Convert.ToInt32(Session["EmpId"]));
        }
        DataTable Atddt = new DataTable();
        Atddt.Clear();
        Atddt.Columns.Add("ColumnName");
        for (var i = 1; i <= 31; i++)
        {
            Atddt.Columns.Add("Day" + i + "");
        }
        Atddt.Columns.Add("ColumnMonthDate");
        foreach (var empdatas in empdata)
        {
            DataRow empdetailsrow = Atddt.NewRow();


            var empdatadatewise = Atdlog.Where(x => x.EmployeeId == empdatas.EmployeeId && x.AttendanceDate.Month==ddmonth.SelectedIndex && x.AttendanceDate.Year==Convert.ToInt32(ddlYear.SelectedItem.Value)).Select(x => x).ToList().Take(31);
            if (empdatadatewise != null)
            {
                empdetailsrow[0] = "Person ID";
                empdetailsrow[1] = empdatas.EmployeeId;
                empdetailsrow[2] = "Employee Name";
                empdetailsrow[3] = empdatas.FName + " " + empdatas.Lname;
                empdetailsrow[4] = "Department Name";
                empdetailsrow[5] = empdatas.DeptName;
                empdetailsrow[32] = "01/" + ddmonth.SelectedIndex + ddlYear.SelectedValue + "/" + " TO " + DateTime.DaysInMonth(ddlYear.SelectedIndex, ddmonth.SelectedIndex) + "/" + ddmonth.SelectedIndex + "/" + ddlYear.SelectedValue;
                Atddt.Rows.Add(empdetailsrow);



                DataRow Daterow = Atddt.NewRow();
                DataRow checkinrow = Atddt.NewRow();
                DataRow checkoutrow = Atddt.NewRow();
                DataRow otrow = Atddt.NewRow();
                DataRow Laterow = Atddt.NewRow();
                DataRow earlyleaverow = Atddt.NewRow();
                DataRow attendedrow = Atddt.NewRow();
                DataRow breakrow = Atddt.NewRow();
                DataRow statusrow = Atddt.NewRow();
                DataRow summueryrow = Atddt.NewRow();



                int colmncount = 1;
                Daterow[0] = "Date";
                foreach (var atd in empdatadatewise)
                {
                    Daterow[colmncount] = atd.AttendanceDate.Day;
                    colmncount++;
                }
                Atddt.Rows.Add(Daterow);

                colmncount = 1;
                checkinrow[0] = "Check in";
                foreach (var atd in empdatadatewise)
                {
                    checkinrow[colmncount] = atd.InTime;
                    colmncount++;
                }
                Atddt.Rows.Add(checkinrow);

                colmncount = 1;
                checkoutrow[0] = "Check Out";
                foreach (var atd in empdatadatewise)
                {
                    checkinrow[colmncount] = atd.OutTime;
                    colmncount++;
                }
                Atddt.Rows.Add(checkoutrow);

                colmncount = 1;
                otrow[0] = "OT";
                foreach (var atd in empdatadatewise)
                {
                    otrow[colmncount] = atd.OverTime;
                    colmncount++;
                }
                Atddt.Rows.Add(otrow);

                colmncount = 1;
                Laterow[0] = "LateBy";
                foreach (var atd in empdatadatewise)
                {
                    Laterow[colmncount] = atd.LateBy;
                    colmncount++;
                }
                Atddt.Rows.Add(Laterow);


                colmncount = 1;
                earlyleaverow[0] = "EarlyBy";
                foreach (var atd in empdatadatewise)
                {
                    earlyleaverow[colmncount] = atd.EarlyBy;
                    colmncount++;
                }
                Atddt.Rows.Add(earlyleaverow);

                colmncount = 1;
                attendedrow[0] = "Attended";
                foreach (var atd in empdatadatewise)
                {
                    attendedrow[colmncount] = atd.Present;
                    colmncount++;
                }
                Atddt.Rows.Add(attendedrow);

                colmncount = 1;
                breakrow[0] = "Break";
                foreach (var atd in empdatadatewise)
                {
                    breakrow[colmncount] = 0;
                    colmncount++;
                }
                Atddt.Rows.Add(breakrow);


                colmncount = 1;
                statusrow[0] = "Status";
                foreach (var atd in empdatadatewise)
                {
                    statusrow[colmncount] = atd.Status;
                    colmncount++;
                }
                Atddt.Rows.Add(statusrow);

                colmncount = 1;
                summueryrow[0] = "Summury";
                foreach (var atd in empdatadatewise)
                {
                    summueryrow[colmncount] = atd.Remarks;
                    colmncount++;
                }
                Atddt.Rows.Add(summueryrow);
            }

        }


        ReportViewer1.ProcessingMode = ProcessingMode.Local;
        ReportViewer1.LocalReport.ReportPath = Server.MapPath("Attendance_Report.rdlc");
        ReportDataSource datasource = new ReportDataSource("Attendance_Report", Atddt);
        ReportViewer1.LocalReport.DataSources.Clear();
        ReportViewer1.LocalReport.DataSources.Add(datasource);
    }
    private void BindEmployeeList()
    {
        ddlEmployee.Items.Clear();
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
            int dId = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
            var data = (from dt in db.EmployeeTBs
                        where (dt.RelivingStatus == null || dt.RelivingStatus == 0) && dt.IsActive == true
                        && dt.TenantId == Convert.ToString(Session["TenantId"]) && dt.CompanyId == cId && dt.DeptId==dId
                        select new { name = dt.FName + ' ' + dt.Lname, dt.EmployeeId }).OrderBy(d => d.name);
            if (data != null && data.Count() > 0)
            {
                ddlEmployee.DataSource = data;
                ddlEmployee.DataTextField = "name";
                ddlEmployee.DataValueField = "EmployeeId";
                ddlEmployee.DataBind();
            }
            ddlEmployee.Items.Insert(0, new ListItem("--SELECT--", "0"));
        }

    }
    private void BindYear()
    {
        ddlYear.Items.Clear();
        int year = DateTime.Now.AddYears(-75).Year;
        for (int i = year; i < DateTime.Now.AddYears(2).Year; i++)
        {
            ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
        }
        ddlYear.SelectedValue = DateTime.Now.Year.ToString();
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
            ddlDepartment.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployeeList();
    }
}