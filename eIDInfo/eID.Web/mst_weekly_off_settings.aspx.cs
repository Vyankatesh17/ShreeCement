using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class mst_weekly_off_settings : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    private void BindJqFunctions()
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserID"] != null)
        {
            if (!IsPostBack)
            {
                using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
                {
                    fillcompany();
                    var sysSet = db.SystemSettingsTBs.Where(d => d.CompanyId == Convert.ToInt32(ddlCompanyList.SelectedValue) && d.TenantId == Convert.ToString(Session["TenantId"])).FirstOrDefault();

                    if (sysSet.WeeklyOff_Calculation == "CompanyWise")
                    {
                        FillAllData();
                        MultiView1.ActiveViewIndex = 0;
                    }
                    else
                    {
                        FillEmployeeAllData();
                        MultiView1.ActiveViewIndex = 2;
                    }




                }

            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }

    protected void grdweekoffdata_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grdweekoffdata.Rows.Count > 0)
        {
            grdweekoffdata.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            grdweekoffdata.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.RawUrl);
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            try
            {
                using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
                {
                    WeeklyOffTB BT = new WeeklyOffTB();
                    BT.CompanyID = Convert.ToInt32(ddlCompany.SelectedValue);
                    BT.TrackHolidays = ddsatoff.Text;
                    BT.Days = dddays.Text;
                    BT.Effectdate = Convert.ToDateTime(txtFromDate.Text);
                    BT.TenantId = Session["TenantId"].ToString();
                    db.WeeklyOffTBs.InsertOnSubmit(BT);
                    db.SubmitChanges();
                    gen.ShowMessage(this.Page, "Weekly off details saved successfully..");

                    txtFromDate.Text = "";
                    ddlCompany.SelectedIndex = dddays.SelectedIndex = ddsatoff.SelectedIndex = 0;
                    MultiView1.ActiveViewIndex = 0;
                }
            }
            catch (Exception ex) { }
        }
    }
    private void FillAllData()
    {
        string query = @"select WeeklyOffid, CompanyName,TrackHolidays,Days ,CONVERT(varchar, Effectdate,101) Date from  WeeklyOffTB WO left outer join CompanyInfoTB MC on MC.CompanyId=WO.CompanyID where MC.status=1";

        if (Session["UserType"].ToString() != "SuperAdmin")
        {
            query += " AND MC.TenantId='" + Session["TenantId"].ToString() + "'";
        }

        query += " order by WeeklyOffid desc";
        DataTable dsweekoffdata = gen.ReturnData(query);

        grdweekoffdata.DataSource = dsweekoffdata;
        grdweekoffdata.DataBind();
    }
    private void fillcompany()
    {
        ddlCompany.Items.Clear();
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

            ddlEmpcompany.DataSource = data;
            ddlEmpcompany.DataTextField = "CompanyName";
            ddlEmpcompany.DataValueField = "CompanyId";
            ddlEmpcompany.DataBind();
        }
        ddlCompany.Items.Insert(0, new ListItem("--Select--", "0"));
        ddlCompanyList.Items.Insert(0, new ListItem("--Select--", "0"));
        ddlEmpcompany.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlCompanyList.SelectedIndex = 1;
        ddlEmpcompany.SelectedIndex = 1;
        BindDepartmentList();
    }

    protected void ddlCompanyList_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentList();

    }
    private void BindDepartmentList()
    {
        int cId = ddlCompanyList.SelectedIndex > 0 ? Convert.ToInt32(ddlCompanyList.SelectedValue) : 0;
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

                ddlEmpDepartment.DataSource = data;
                ddlEmpDepartment.DataTextField = "DeptName";
                ddlEmpDepartment.DataValueField = "DeptID";
                ddlEmpDepartment.DataBind();
            }
            ddlDepartment.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlEmpDepartment.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployeeList();
    }
    private void BindEmployeeList()
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            DateTime todaysdt = DateTime.Now;
            var month = todaysdt.Month;
            ddlEmployee.Items.Clear();

            int cId = ddlCompanyList.SelectedIndex > 0 ? Convert.ToInt32(ddlCompanyList.SelectedValue) : 0;
            int dId = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;

            var data = (from dt in db.EmployeeTBs
                        where dt.IsActive == true
                        && dt.TenantId == Convert.ToString(Session["TenantId"]) && dt.CompanyId == cId && dt.DeptId == dId
                        select new { name = dt.FName + ' ' + dt.Lname, dt.EmployeeId, dt.RelivingDate, dt.RelivingStatus }).OrderBy(d => d.name);
            List<EmployeeTB> emplist = new List<EmployeeTB>();

            if (data != null && data.Count() > 0)
            {
                foreach (var item in data)
                {
                    EmployeeTB emp = new EmployeeTB();
                    if (item.RelivingStatus == 1)
                    {
                        DateTime relivingdate = Convert.ToDateTime(item.RelivingDate);
                        var relivingmonth = relivingdate.Month;

                        if (relivingmonth == month)
                        {
                            emp.EmployeeId = item.EmployeeId;
                            emp.FName = item.name;

                            emplist.Add(emp);
                        }
                    }
                    else
                    {
                        emp.EmployeeId = item.EmployeeId;
                        emp.FName = item.name;
                        emplist.Add(emp);
                    }
                }

                ddlEmployee.DataSource = emplist;
                ddlEmployee.DataTextField = "FName";
                ddlEmployee.DataValueField = "EmployeeId";
                ddlEmployee.DataBind();
                
            }
            ddlEmployee.Items.Insert(0, new ListItem("--SELECT--", "0"));            
        }
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                string query = @"select WeeklyOffid,MC.CompanyId, CompanyName,WO.EmployeeNo,Days,DayId, Emp.FName +' '+ Emp.Lname as EmpName,dept.DeptID  from  EmployeeWeeklyOffTB WO 
left outer join CompanyInfoTB MC on MC.CompanyId=WO.CompanyID
left outer Join EmployeeTB Emp on Emp.EmployeeNo = WO.EmployeeNo 
left outer join MasterDeptTB dept on dept.DeptID = Emp.DeptId
 where MC.status=1";
                if (ddlCompanyList.SelectedIndex > 0)
                {
                    query += " AND MC.CompanyId=" + Convert.ToInt32(ddlCompanyList.SelectedValue) + "";
                }
                if (ddlDepartment.SelectedIndex > 0)
                {
                    query += " AND dept.DeptID=" + Convert.ToInt32(ddlDepartment.SelectedValue) + "";
                }
                if (ddlEmployee.SelectedIndex > 0)
                {
                    query += " AND Emp.EmployeeId=" + Convert.ToInt32(ddlEmployee.SelectedValue) + "";
                }

                query += "order by WeeklyOffid Asc";
                DataTable data = gen.ReturnData(query);

                grdemployeeWeeklyoff.DataSource = data;
                grdemployeeWeeklyoff.DataBind();
            }
        }
        catch (Exception ex) { }
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        string skey = Session["TenantId"].ToString().Replace("+", "key_plus");
        Response.Redirect("Import_EmployeeWise_WeeklyOff.aspx?key=" + skey);

    }

    protected void grdemployeeWeeklyoff_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grdemployeeWeeklyoff.Rows.Count > 0)
        {
            grdemployeeWeeklyoff.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            grdemployeeWeeklyoff.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }


    private void FillEmployeeAllData()
    {
        string query = @"select WeeklyOffid, CompanyName,WO.EmployeeNo,Days,DayId, Emp.FName +' '+ Emp.Lname as EmpName  from  EmployeeWeeklyOffTB WO 
left outer join CompanyInfoTB MC on MC.CompanyId=WO.CompanyID
left outer Join EmployeeTB Emp on Emp.EmployeeNo = WO.EmployeeNo 
 where MC.status=1";

        if (Session["UserType"].ToString() != "SuperAdmin")
        {
            query += " AND MC.TenantId='" + Session["TenantId"].ToString() + "'";
        }

        query += " order by WeeklyOffid Asc";
        DataTable dsweekoffdata = gen.ReturnData(query);

        grdemployeeWeeklyoff.DataSource = dsweekoffdata;
        grdemployeeWeeklyoff.DataBind();
    }






    protected void ddlEmpcompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentList();
    }

    protected void ddlEmpDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmpEmployeeList();
    }

    protected void OnClick_Edit(object sender, ImageClickEventArgs e)
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            ImageButton Lnk = (ImageButton)sender;
            string DeptId = Lnk.CommandArgument;
            lblempWeeklyid.Text = DeptId;
            MultiView1.ActiveViewIndex = 3;
            EmployeeWeeklyOffTB MT = db.EmployeeWeeklyOffTBs.Where(d => d.WeeklyOffId == Convert.ToInt32(DeptId)).First();
            ddlEmpcompany.SelectedValue = MT.CompanyId.ToString();

            var Empdata = db.EmployeeTBs.Where(a => a.EmployeeNo == MT.EmployeeNo && a.CompanyId == MT.CompanyId).FirstOrDefault();
            ddlEmpDepartment.SelectedValue = Empdata.DeptId.ToString();
            BindEmpEmployeeList();
            
            ddlEmpEmployee.SelectedValue = Empdata.EmployeeId.ToString();
            ddlEmpdays.SelectedValue = MT.Days;
            txtDayId.Text = MT.DayId.ToString();

            btnEmpWeeklyoffSubmit.Text = "Update";
        }

    }


    protected void btnEmpWeeklyoffSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                if(btnEmpWeeklyoffSubmit.Text == "Submit")
                {
                    var Empdata = db.EmployeeTBs.Where(a => a.EmployeeId == Convert.ToInt32(ddlEmpEmployee.SelectedValue) && a.CompanyId == Convert.ToInt32(ddlEmpcompany.SelectedValue)).FirstOrDefault();

                    var Weekeklyoffdata = db.EmployeeWeeklyOffTBs.Where(a => a.EmployeeNo == Empdata.EmployeeNo && a.CompanyId == Convert.ToInt32(ddlEmpcompany.SelectedValue)).FirstOrDefault();

                    if (Weekeklyoffdata == null)
                    {
                        EmployeeWeeklyOffTB BT = new EmployeeWeeklyOffTB();
                        BT.CompanyId = Convert.ToInt32(ddlEmpcompany.SelectedValue);
                        BT.EmployeeNo = Empdata.EmployeeNo;
                        BT.Days = ddlEmpdays.Text;
                        BT.UserId = 0;
                        BT.DayId = Convert.ToInt32(txtDayId.Text);
                        BT.TenantId = Session["TenantId"].ToString();
                        db.EmployeeWeeklyOffTBs.InsertOnSubmit(BT);
                        db.SubmitChanges();
                        gen.ShowMessage(this.Page, "Employee Weekly off details saved successfully..");

                        ddlEmpDepartment.SelectedIndex = ddlEmpdays.SelectedIndex = ddlEmpEmployee.SelectedIndex = 0;
                        txtDayId.Text = "";
                        MultiView1.ActiveViewIndex = 3;
                    }
                    else
                    {
                        gen.ShowMessage(this.Page, "Employee Weekly off details Already Exists..");
                        MultiView1.ActiveViewIndex = 3;
                    }
                }
                else if (btnEmpWeeklyoffSubmit.Text == "Update")
                {
                    EmployeeWeeklyOffTB MT = db.EmployeeWeeklyOffTBs.Where(d => d.WeeklyOffId == Convert.ToInt32(lblempWeeklyid.Text)).First();
                    ddlEmpcompany.SelectedValue = MT.CompanyId.ToString();

                    var Empdata = db.EmployeeTBs.Where(a => a.EmployeeNo == MT.EmployeeNo && a.CompanyId == MT.CompanyId).FirstOrDefault();

                    MT.CompanyId = Convert.ToInt32(ddlEmpcompany.SelectedValue);
                    MT.EmployeeNo = Empdata.EmployeeNo;
                    MT.Days = ddlEmpdays.Text;
                    MT.UserId = 0;
                    MT.DayId = Convert.ToInt32(txtDayId.Text);
                    MT.TenantId = Session["TenantId"].ToString();

                    db.SubmitChanges();
                    gen.ShowMessage(this.Page, "Employee Weekly off details Updated successfully..");

                }



            }
        }
        catch (Exception ex) { }
    }

    protected void btnEmpCancel_Click(object sender, EventArgs e)
    {

    }

    protected void btmEmpAdd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 3;
    }

    private void BindEmpEmployeeList()
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            DateTime todaysdt = DateTime.Now;
            var month = todaysdt.Month;
            ddlEmpEmployee.Items.Clear();

            int cId = ddlEmpcompany.SelectedIndex > 0 ? Convert.ToInt32(ddlEmpcompany.SelectedValue) : 0;
            int dId = ddlEmpDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlEmpDepartment.SelectedValue) : 0;

            var data = (from dt in db.EmployeeTBs
                        where dt.IsActive == true
                        && dt.TenantId == Convert.ToString(Session["TenantId"]) && dt.CompanyId == cId && dt.DeptId == dId
                        select new { name = dt.FName + ' ' + dt.Lname, dt.EmployeeId, dt.RelivingDate, dt.RelivingStatus }).OrderBy(d => d.name);
            List<EmployeeTB> emplist = new List<EmployeeTB>();

            if (data != null && data.Count() > 0)
            {
                foreach (var item in data)
                {
                    EmployeeTB emp = new EmployeeTB();
                    if (item.RelivingStatus == 1)
                    {
                        DateTime relivingdate = Convert.ToDateTime(item.RelivingDate);
                        var relivingmonth = relivingdate.Month;

                        if (relivingmonth == month)
                        {
                            emp.EmployeeId = item.EmployeeId;
                            emp.FName = item.name;

                            emplist.Add(emp);
                        }
                    }
                    else
                    {
                        emp.EmployeeId = item.EmployeeId;
                        emp.FName = item.name;
                        emplist.Add(emp);
                    }
                }
               
                ddlEmpEmployee.DataSource = emplist;
                ddlEmpEmployee.DataTextField = "FName";
                ddlEmpEmployee.DataValueField = "EmployeeId";
                ddlEmpEmployee.DataBind();
            }            
            ddlEmpEmployee.Items.Insert(0, new ListItem("--SELECT--", "0"));
        }
    }




}