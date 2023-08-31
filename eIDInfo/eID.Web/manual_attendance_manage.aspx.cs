using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class manual_attendance_manage : System.Web.UI.Page
{
    Genreal g = new Genreal();
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    private void BindJqFunctions()
    {
        //jqFunctions
        if (grdAtt.Rows.Count > 0)
        {
            grdAtt.UseAccessibleHeader = true;
            grdAtt.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Session["UserId"] != null)
            {
                if (!IsPostBack)
                {
                    txtDate.Text = g.GetCurrentDateTime().ToShortDateString();
                    //txtDate.Attributes.Add("readonly", "true");
                    MultiView1.ActiveViewIndex = 0;
                    BindAllDataGridview();
                    fillcompany();
                    BindEmployeeList();
                    BindEmployeeSearchList();
                    //BindDeviceList();
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
        }
        catch (Exception ex)
        {
        }
        BindJqFunctions();
    }
    
  
    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }

    #region btnsubmit_Click
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (IsValid)
            {
                using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
                {
                    var checkExists = db.DeviceLogsTBs.Where(d => d.EmpID == Convert.ToInt32(ddlEmployee.SelectedValue) && d.AttendDate.Value.Date == Convert.ToDateTime(txtDate.Text) && d.ATime.ToString() == txtInTime.Text).Distinct();
                    var devicedata = db.DevicesTBs.Where(a => a.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && a.DeviceName == "ME" && a.TenantId == Convert.ToString(Session["TenantId"])).FirstOrDefault();
                    if (checkExists.Count() > 0)
                    {
                        g.ShowMessage(this.Page, "Selected employees manual attendance already added on provided date..");
                    }
                    else
                    {
                        DateTime dt = Convert.ToDateTime(txtDate.Text);
                        DeviceLogsTB manual = new DeviceLogsTB();
                        manual.DeviceAccountId = Convert.ToInt32(devicedata.DeviceAccountId);
                        manual.DownloadDate = dt;
                        manual.AttendDate = dt.Date + TimeSpan.Parse(txtInTime.Text);
                        manual.PunchStatus = ddlStatus.SelectedValue;
                        manual.EmpID = Convert.ToInt32(ddlEmployee.SelectedValue);
                        manual.ADate = dt.Date;
                        manual.ATime = TimeSpan.Parse(txtInTime.Text);
                        manual.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                        manual.TenantId = Convert.ToString(Session["TenantId"]);
                        manual.Calculationflag = 0;                        
                        manual.Manual_Attendance_Flag = 1;                      
                       
                        db.DeviceLogsTBs.InsertOnSubmit(manual);
                        db.SubmitChanges();
                        g.ShowMessageRedirect(this.Page, "Attendance added successfully..", Request.RawUrl);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            g.ShowMessage(this.Page, ex.Message);
        }
    }
    #endregion


    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }
   
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue):0;
        BindDepartment(cId);
        BindEmployeeList();
    }

    protected void ddlCompanyList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int cId = ddlCompanyList.SelectedIndex > 0 ? Convert.ToInt32(ddlCompanyList.SelectedValue):0;
        BindDepartmentSearch(cId);
        BindEmployeeSearchList();
    }

    protected void ddlDepartmentList_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployeeSearchList();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindAllDataGridview();
    }


    protected void grdAtt_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grdAtt.Rows.Count > 0)
        {
            grdAtt.UseAccessibleHeader = true;
            grdAtt.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }



    #region Data Binding 
    private void BindAllDataGridview()
    {
        try {
            using(HrPortalDtaClassDataContext db=new HrPortalDtaClassDataContext())
            {
                DateTime dateTime = string.IsNullOrEmpty(txtAttDate.Text) ? DateTime.Now : Convert.ToDateTime(txtAttDate.Text);

                var data = (from d in db.ManualAttendanceTBs
                            join e in db.EmployeeTBs on d.EmpId equals e.EmployeeId
                            join c in db.CompanyInfoTBs on d.CompanyId equals c.CompanyId
                            join d1 in db.MasterDeptTBs on e.DeptId equals d1.DeptID
                            where e.IsActive == true && d.TenantId == Convert.ToString(Session["TenantId"]) && d.AttendanceDate.Value.Date == dateTime.Date
                            select new
                            {
                                d.AttendanceDate,
                                d.CompanyId,
                                d.CreatedBy,
                                d.CreatedDate,
                                d.EmpId,
                                d.InTime,
                                d.IsActive,
                                d.MAttendID,
                                d.ModifiedBy,
                                d.ModifiedDate,
                                d.OuTime,
                                d.Remarks,
                                d.Status,
                                d.TenantId,
                                EmpName = e.FName + " " + e.Lname,
                                DeviceCode = e.EmployeeNo,
                                EmployeeNo = d.MAttendID,
                                d1.DeptName,
                                c.CompanyName,
                                d1.DeptID
                            }
                           ).Distinct();

                if (ddlCompanyList.SelectedIndex > 0)
                {
                    data = data.Where(d => d.CompanyId == Convert.ToInt32(ddlCompanyList.SelectedValue));
                }
                if (ddlDepartmentList.SelectedIndex > 0)
                {
                    data = data.Where(d => d.DeptID == Convert.ToInt32(ddlDepartmentList.SelectedValue));
                }
                if (ddlEmployeeList.SelectedIndex > 0)
                {
                    data = data.Where(d => d.EmpId == Convert.ToInt32(ddlEmployeeList.SelectedValue));
                }
                grdAtt.DataSource = data;
                grdAtt.DataBind();
                
            }
        }
        catch(Exception ex) { }
    }
    #endregion

    #region fillcompany
    private void fillcompany()
    {
        ddlCompany.Items.Clear();
        ddlCompanyList.Items.Clear();
        List<CompanyInfoTB> data = SPCommon.DDLCompanyBind(Session["UserType"].ToString(), Session["TenantId"].ToString(), Session["CompanyID"].ToString());

        if (data != null && data.Count() > 0)
        {
            ddlCompany.DataSource = data;
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataValueField = "CompanyId";
            ddlCompany.DataBind();

            ddlCompanyList.DataSource = data;
            ddlCompanyList.DataTextField = "CompanyName";
            ddlCompanyList.DataValueField = "CompanyId";
            ddlCompanyList.DataBind();
        }
        ddlCompany.Items.Insert(0, new ListItem("--Select--", "0"));
        ddlCompanyList.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    #endregion

    //private void BindDeviceList()
    //{
    //    ddlDevice.Items.Clear();
    //    using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
    //    {
    //        var data = (from d in db.DevicesTBs
    //                    where d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.TenantId == Convert.ToString(Session["TenantId"])
    //                    select new { d.DeviceAccountId, DeviceName = d.DeviceName + " " + d.DeviceSerialNo }).Distinct();
    //        ddlDevice.DataSource = data;
    //        ddlDevice.DataTextField = "DeviceName";
    //        ddlDevice.DataValueField = "DeviceAccountId";
    //        ddlDevice.DataBind();
    //    }
    //    ddlDevice.Items.Insert(0, new ListItem("--Select--", "0"));
    //}

    #region BindEmployeeSearchList
    private void BindEmployeeSearchList()
    {
        ddlEmployeeList.Items.Clear();
        int cId = ddlCompanyList.SelectedIndex > 0 ? Convert.ToInt32(ddlCompanyList.SelectedValue) : 0;
        int dId = ddlDepartmentList.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartmentList.SelectedValue) : 0;
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from dt in db.EmployeeTBs
                        where dt.IsActive == true && (dt.RelivingStatus == null || dt.RelivingStatus == 0) && dt.CompanyId == cId && dt.DeptId==dId
                        select new { EmpName = dt.FName + ' ' + dt.Lname, dt.EmployeeId, dt.ManagerID }).Distinct();
            if (Session["UserType"].ToString().Equals("User"))
            {
                data = data.Where(d => d.EmployeeId == Convert.ToInt32(Session["EmpId"])).Distinct();
            }
            if (data != null && data.Count() > 0)
            {
                if (Convert.ToInt32(Session["IsDeptHead"]) == 1)
                {
                    int manId = Convert.ToInt32(Session["EmpId"]);
                    data = data.Where(d => d.ManagerID == manId || d.EmployeeId == manId).Distinct();
                }
                ddlEmployeeList.DataSource = data;
                ddlEmployeeList.DataTextField = "EmpName";
                ddlEmployeeList.DataValueField = "EmployeeId";
                ddlEmployeeList.DataBind();
            }
            ddlEmployeeList.Items.Insert(0, new ListItem("--Select--", "0"));
            if (Session["UserType"].ToString().Equals("User"))
            {
                ddlEmployeeList.SelectedValue = Session["EmpId"].ToString();
            }
        }
    }
    #endregion

    #region BindEmployeeList
    private void BindEmployeeList()
    {
        ddlEmployee.Items.Clear();
        int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
        int dId = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from dt in db.EmployeeTBs
                        where dt.IsActive == true && (dt.RelivingStatus == null || dt.RelivingStatus == 0) && dt.CompanyId == cId && dt.DeptId == dId
                        select new { EmpName = dt.FName + ' ' + dt.Lname, dt.EmployeeId, dt.ManagerID }).Distinct();
            if (Session["UserType"].ToString().Equals("User"))
            {
                data = data.Where(d => d.EmployeeId == Convert.ToInt32(Session["EmpId"])).Distinct();
            }
            if (data != null && data.Count() > 0)
            {
                if (Convert.ToInt32(Session["IsDeptHead"]) == 1)
                {
                    int manId = Convert.ToInt32(Session["EmpId"]);
                    data = data.Where(d => d.ManagerID == manId || d.EmployeeId == manId).Distinct();
                }
                ddlEmployee.DataSource = data;
                ddlEmployee.DataTextField = "EmpName";
                ddlEmployee.DataValueField = "EmployeeId";
                ddlEmployee.DataBind();
            }
            ddlEmployee.Items.Insert(0, new ListItem("--Select--", "0"));
            if (Session["UserType"].ToString().Equals("User"))
            {
                ddlEmployee.SelectedValue = Session["EmpId"].ToString();
            }
        }
    }
    #endregion

    #region BindDepartmentSearch
    private void BindDepartmentSearch(int companyid)
    {
        ddlDepartmentList.Items.Clear();
        var data = (from dt in HR.MasterDeptTBs
                    where dt.Status == 1 && dt.CompanyId == companyid
                    select dt).OrderBy(d => d.DeptName);
        if (data != null && data.Count() > 0)
        {

            ddlDepartmentList.DataSource = data;
            ddlDepartmentList.DataTextField = "DeptName";
            ddlDepartmentList.DataValueField = "DeptID";
            ddlDepartmentList.DataBind();
        }
        ddlDepartmentList.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    #endregion

    #region BindDepartment
    private void BindDepartment(int companyid)
    {
        ddlDepartment.Items.Clear();
        var data = (from dt in HR.MasterDeptTBs
                    where dt.Status == 1 && dt.CompanyId == companyid
                    select dt).OrderBy(d => d.DeptName);
        if (data != null && data.Count() > 0)
        {

            ddlDepartment.DataSource = data;
            ddlDepartment.DataTextField = "DeptName";
            ddlDepartment.DataValueField = "DeptID";
            ddlDepartment.DataBind();
        }
        ddlDepartment.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    #endregion

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployeeList();
    }
}