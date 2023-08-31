using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class attend_summary : System.Web.UI.Page
{
    DataTable dtInfo;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                ddlMonth.SelectedIndex = DateTime.Now.Month;
                BindYear();
                fillcompany();
                //BindSimpleAttendance();
            }
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentList(); BindEmployeeList();
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployeeList();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DateTime from = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), ddlMonth.SelectedIndex, 1);
        int days = DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedValue), ddlMonth.SelectedIndex);
        DateTime toDate = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), ddlMonth.SelectedIndex, days);

        
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
                
                dtInfo = new DataTable();
                dtInfo.Columns.Add(new DataColumn("Status", typeof(string)));

                for (var day = from.Date; day.Date <= toDate.Date; day = day.AddDays(1))
                {
                    string no = day.Day.ToString();
                    dtInfo.Columns.Add(new DataColumn(no, typeof(string)));
                }
                ViewState["dtInfo"] = dtInfo;

            

            var data = (from d in db.EmployeeTBs
                        join c in db.CompanyInfoTBs on d.CompanyId equals c.CompanyId
                        join d1 in db.MasterDeptTBs on d.DeptId equals d1.DeptID
                        join d2 in db.MasterDesgTBs on d.DesgId equals d2.DesigID
                        where d.IsActive==true&& d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.TenantId == Convert.ToString(Session["TenantId"])
                        select new {EmpName = d.FName + " " + d.Lname, d.EmployeeNo, d1.DeptName, d2.DesigName, c.CompanyName,d.EmployeeId,d.DeptId,d.DesgId }).Distinct();

            if (ddlDepartment.SelectedIndex > 0)
            {
                data = data.Where(d => d.DeptId == Convert.ToInt32(ddlDepartment.SelectedValue)).Distinct();
            }
            if (ddlEmployee.SelectedIndex > 0)
            {
                data = data.Where(d => d.EmployeeId == Convert.ToInt32(ddlEmployee.SelectedValue)).Distinct();
            }
            if (!string.IsNullOrEmpty(txtEmpCode.Text))
            {
                data = data.Where(d => d.EmployeeNo == txtEmpCode.Text).Distinct();
            }
            rptrTables.DataSource = data;
            rptrTables.DataBind();
        }
    }

    protected void rptrTables_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Header)
        {
            var x = e.Item.FindControl("lblHeader") as Label;
            x.Text = "Report for month of " + ddlMonth.SelectedItem.Text + " " + ddlYear.SelectedValue;
        }
        else
        {

            dtInfo.Rows.Clear();
            RepeaterItem item = e.Item;
            string lblEmpIdId = (item.FindControl("lblEmpIdId") as Label).Text;
            GridView gv = e.Item.FindControl("grdOrder") as GridView;
            int empId = Convert.ToInt32(lblEmpIdId);
            string[] statusArray = { "In Time", "Out Time", "Status", "Total" };
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {

                for (int i = 0; i < statusArray.Length; i++)
                {
                    DateTime from = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), ddlMonth.SelectedIndex, 1);
                    int days = DateTime.DaysInMonth(Convert.ToInt32(ddlYear.SelectedValue), ddlMonth.SelectedIndex);
                    DateTime toDate = new DateTime(Convert.ToInt32(ddlYear.SelectedValue), ddlMonth.SelectedIndex, days);
                    DataRow dr = dtInfo.NewRow();
                    dr["Status"] = statusArray[i];

                    for (var day = from.Date; day.Date <= toDate.Date; day = day.AddDays(1))
                    {
                        var data = db.AttendaceLogTBs.Where(d => d.EmployeeId == empId && d.AttendanceDate.Date == day.Date).FirstOrDefault();

                        string val = "";
                        if (data != null)
                        {
                            if (statusArray[i] == "In Time")
                            {
                                val = string.IsNullOrEmpty(data.InTime) ? "" : data.InTime.Substring(0, data.InTime.LastIndexOf(':')); ;
                            }
                            else if (statusArray[i] == "Out Time")
                            {
                                val = string.IsNullOrEmpty(data.OutTime) ? "" : data.OutTime.Substring(0, data.OutTime.LastIndexOf(':'));
                            }
                            else if (statusArray[i] == "Status")
                            {
                                val = string.IsNullOrEmpty(data.Status) ? "" : data.Status.ToString();
                            }
                            else if (statusArray[i] == "Total")
                            {
                                val = string.IsNullOrEmpty(data.Duration.HasValue.ToString()) ? "" : String.Format("{0:0.00}", data.Duration);
                            }
                        }
                        string no = day.Day.ToString();
                        dr[no] = val.ToString();
                    }
                    dtInfo.Rows.Add(dr);
                }
            }


            gv.DataSource = dtInfo;
            gv.DataBind();
        }
    }

    private void fillcompany()
    {
        try
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
        catch (Exception ex)
        {
            throw ex;
        }
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
    private void BindEmployeeList()
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            ddlEmployee.Items.Clear();
            int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
            int dId = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
            var data = (from dt in db.EmployeeTBs
                        where (dt.RelivingStatus == null || dt.RelivingStatus == 0) && dt.IsActive == true
                        && dt.TenantId == Convert.ToString(Session["TenantId"]) && dt.CompanyId == cId && dt.DeptId == dId
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


    protected void ExportToExcel(object sender, EventArgs e)
    {
            Response.Clear();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment;filename=RepeaterExport.xls");
            Response.Charset = "";
            Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            rptrTables.RenderControl(hw);
            Response.Output.Write(sw.ToString());
            Response.Flush();
            Response.End();
        }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
}