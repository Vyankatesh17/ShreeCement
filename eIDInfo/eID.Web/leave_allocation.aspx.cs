using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
public partial class leave_allocation : System.Web.UI.Page
{

    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    DataTable dtt = new DataTable();
    //  DataTable DtExperience = new DataTable();
    string AttachPath;
    Genreal g = new Genreal();
    String Employeeid;
    Boolean status1 = false;
    string RenewCount;
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
                BindRenewEmployee();
                BindAllEmp();
                fillcompany();
                //txtStartdate.Attributes.Add("Readonly", "Readonly");
                //txtEndDate.Attributes.Add("Readonly", "Readonly");
                fillLeaves();
                //txtDate.Text = DateTime.Now.Date.ToShortDateString();
                //txtDate.Attributes.Add("Readonly", "True");
                txtLeaveYear.Text = DateTime.Now.Year.ToString();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }

        dtt = new DataTable();
        dtt.Columns.Add(new DataColumn("LeaveID", typeof(string)));
        dtt.Columns.Add(new DataColumn("LeaveName", typeof(string)));
        dtt.Columns.Add(new DataColumn("FromDateAllocation", typeof(string)));
        dtt.Columns.Add(new DataColumn("ToDateAllocation", typeof(string)));
        dtt.Columns.Add(new DataColumn("TotalAllocatedLeaves", typeof(string)));
        dtt.Columns.Add(new DataColumn("PendingLeaves", typeof(string)));
        if (grd_Emp.Rows.Count > 0)
            grd_Emp.HeaderRow.TableSection = TableRowSection.TableHeader;
        BindJqFunctions();
    }

    public void BindAllEmp()
    {
        int cId = ddlCompanyList.SelectedIndex > 0 ? Convert.ToInt32(ddlCompanyList.SelectedValue) : 0;
        int dId = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
        int eId = ddlEmployeeList.SelectedIndex > 0 ? Convert.ToInt32(ddlEmployeeList.SelectedValue) : 0;
        #region admin
        var EmpData = (from d in HR.EmployeeTBs
                       join n in HR.MasterDeptTBs on d.DeptId equals n.DeptID
                       where (d.RelivingStatus == null ||d.RelivingStatus==0)&&d.IsActive==true&& d.TenantId==Convert.ToString(Session["TenantId"])
                       select new
                       {
                           Name = d.Solitude == 0 ? "Mr" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 1 ? "Ms" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 2 ? "Mrs" + " " + d.FName + " " + d.MName + " " + d.Lname : "Dr." + " " + d.FName + " " + d.MName + " " + d.Lname,
                           Name1 = d.Solitude == 0 ? "Mr" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 1 ? "Ms" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 2 ? "Mrs" + " " + d.FName + " " + d.MName + " " + d.Lname : "Dr." + " " + d.FName + " " + d.MName + " " + d.Lname,
                           d.FName,
                           d.Lname,
                           d.CompanyId,
                           d.DeptId,
                           d.EmployeeId,
                           d.BirthDate,
                           d.ConfirmDate,
                           d.Email,
                           DOJ1 = d.DOJ,
                           d.PanNo,
                           d.ContactNo,
                           d.PassportNo,
                           n.DeptName,
                           EmpName = d.FName + " " + d.Lname,
                           AllocationStatus = d.AllocationStatus == 1 ? "Allocated" : "Not Allocated"
                       }).Distinct();

        if (cId > 0)
        {
            EmpData = EmpData.Where(d => d.CompanyId == cId).Distinct() ;
        }
        if (dId > 0)
        {
            EmpData = EmpData.Where(d => d.DeptId==dId).Distinct();
        }
        if (eId > 0)
        {
            EmpData = EmpData.Where(d => d.EmployeeId==eId).Distinct();
        }
       
        grd_Emp.DataSource = EmpData;
        grd_Emp.DataBind();
       
        for (int i = 0; i < grd_Emp.Rows.Count; i++)
        {
            LinkButton lbtnRenew = (LinkButton)grd_Emp.Rows[i].FindControl("lbtnRenew");
            LinkButton lbtnAllocate = (LinkButton)grd_Emp.Rows[i].FindControl("lbtnAllocate");
           
            var Allocateddata = from d in HR.EmployeeTBs
                                where d.AllocationStatus == 1
                                select new { d.EmployeeId };

            if (Allocateddata.Count() > 0)
            {

                if (grd_Emp.Rows[i].Cells[8].Text == "Allocated")
                {
                    lbtnAllocate.Visible = false;
                    lbtnRenew.Visible = true;
                }
                else
                {
                    lbtnRenew.Visible = false;
                    lbtnAllocate.Visible = true;

                }

            }
            else
            {
                lbtnRenew.Visible = false;
            }
        }
        #endregion
    }
   
    #region bind dropdownlist
    private void fillcompany()
    {
        ddlCompanyList.Items.Clear();
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
        }
        ddlCompanyList.Items.Insert(0, new ListItem("--Select--", "0"));
        ddlCompany.Items.Insert(0, new ListItem("--Select--", "0"));
    }


    private void BindDepartment(string p)
    {
        try
        {
            if (ddlCompanyList.SelectedIndex > 0)
            {
                var data = (from dt in HR.CompanyInfoTBs
                            join dep in HR.MasterDeptTBs on dt.CompanyId equals dep.CompanyId
                            where dt.CompanyName == p
                            select dep).OrderBy(dt => dt.DeptName);

                if (data != null && data.Count() > 0)
                {

                    ddlDepartment.DataSource = data;
                    ddlDepartment.DataTextField = "DeptName";
                    ddlDepartment.DataValueField = "DeptID";
                    ddlDepartment.DataBind();
                    ddlDepartment.Items.Insert(0, "--Select--");
                }
                else
                {
                    ddlDepartment.DataSource = null;
                    ddlDepartment.DataBind();

                }

            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


    private void FillEmployeeList()
    {
        try
        {
            ddlEmployeeList.Items.Clear();
            int cId = ddlCompanyList.SelectedIndex > 0 ? Convert.ToInt32(ddlCompanyList.SelectedValue) : 0;
            int dId = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
            var data = (from d in HR.EmployeeTBs
                            //join dep in HR.MasterDeptTBs on dtReportHead.CompanyId equals dep.CompanyId
                        where (d.RelivingStatus == null||d.RelivingStatus==0)&&d.IsActive==true && d.CompanyId == cId && d.DeptId == dId
                        select new
                        {
                            d.EmployeeId,
                            Name = d.FName + ' ' + d.MName + ' ' + d.Lname
                        }).Distinct();
            if (data != null && data.Count() > 0)
            {
                ddlEmployeeList.DataSource = data;
                ddlEmployeeList.DataTextField = "Name";
                ddlEmployeeList.DataValueField = "EmployeeId";
                ddlEmployeeList.DataBind();
            }
            ddlEmployeeList.Items.Insert(0, new ListItem("--Select--", "0"));
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion
    protected void grd_Emp_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void OnClick_Edit(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        ImageButton Lnk1 = (ImageButton)sender;
        string emp1ID = Lnk1.CommandArgument;
        Session["empp2"] = emp1ID;
        ddlLeaveType.Focus();

    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            int counter = 0, dCounter = 0;
            db.CommandTimeout = 8 * 60;
            if (IsValid)
            {
                counter = 0;
                int leaveid = ddlmasterleavetype.SelectedIndex > 0 ? Convert.ToInt32(ddlmasterleavetype.SelectedValue) : 0;

                var leavedata = db.masterLeavesTBs.Where(a => a.LeaveID == leaveid).FirstOrDefault();
                foreach (GridViewRow gvr in grd_Emp.Rows)
                {
                    if (((CheckBox)gvr.FindControl("chkSelect")).Checked == true)
                    {
                        Label lblEmpId = (Label)gvr.FindControl("lblEmployeeId");
                        string empId = lblEmpId.Text;
                        LeaveAllocationTB leave = new LeaveAllocationTB();
                        leave.EmployeeID = Convert.ToInt32(empId);                     
                        leave.FromDateAllocation = new DateTime(DateTime.Now.Year, 1, 1);
                        leave.ToDateAllocation = new DateTime(DateTime.Now.Year, 12, 31); 
                        leave.LeaveID = leaveid;
                        leave.TotalAllocatedLeaves = leavedata.YearlyLimit;
                        leave.PendingLeaves = leavedata.YearlyLimit;
                        leave.AllocationStatus = 1;
                        leave.TenantId = Convert.ToString(Session["TenantId"]);
                        leave.CompanyId = Convert.ToInt32(ddlCompanyList.SelectedValue);
                        HR.LeaveAllocationTBs.InsertOnSubmit(leave);
                        HR.SubmitChanges();
                        EmployeeTB emp = HR.EmployeeTBs.Where(d => d.EmployeeId == Convert.ToInt32(empId)).First();
                        emp.AllocationStatus = 1;
                        HR.SubmitChanges();
                        counter++;
                    }
                }
                if (counter > 0)
                {
                    g.ShowMessage(this.Page, "Leave allocated successfully..");
                }
            }
           
        }
        }


        protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            dtt = ViewState["dtt"] as DataTable;
            if (ddlCompany.SelectedIndex > 0)
            {
                if (dtt.Rows.Count > 0)
                {

                    if (btnsubmit.Text == "Save")
                    {


                        int cnt = 0;
                        for (int i = 0; i < dtt.Rows.Count; i++)
                        {
                            string leaveId = dtt.Rows[i]["LeaveID"].ToString();
                            string fromDate = dtt.Rows[i]["FromDateAllocation"].ToString();
                            string toDate = dtt.Rows[i]["ToDateAllocation"].ToString();


                            string query = string.Format(@"SELECT        TOP (1) LeaveAllocateID
FROM            LeaveAllocationTB AS L
WHERE        (EmployeeID = {0}) AND L.LeaveID={1} AND (('{2}' BETWEEN FromDateAllocation AND ToDateAllocation) OR
                         ('{3}' BETWEEN FromDateAllocation AND ToDateAllocation))", hfKey.Value, leaveId, fromDate, toDate);



                            DataTable dtExists = g.ReturnData(query);
                            if (dtExists.Rows.Count > 0)
                            {
                                g.ShowMessage(this.Page, "Leaves already allocated..");
                            }
                            else
                            {
                                try
                                {
                                    DateTime fDate = Convert.ToDateTime(fromDate);
                                    DateTime tDate = Convert.ToDateTime(toDate);
                                    LeaveAllocationTB leave = new LeaveAllocationTB();
                                    leave.EmployeeID = Convert.ToInt32(hfKey.Value);
                                    //leave.FromDateAllocation = Genreal.GetDate(dtt.Rows[i][2].ToString());
                                    //leave.ToDateAllocation = Genreal.GetDate(dtt.Rows[i][3].ToString());
                                    leave.FromDateAllocation = new DateTime(fDate.Year, fDate.Month, fDate.Day);
                                    leave.ToDateAllocation = new DateTime(tDate.Year, tDate.Month, tDate.Day);
                                    leave.LeaveID = Convert.ToInt32(dtt.Rows[i][0].ToString());
                                    leave.TotalAllocatedLeaves = Convert.ToInt32(dtt.Rows[i][4].ToString());
                                    leave.PendingLeaves = Convert.ToInt32(dtt.Rows[i][4].ToString());
                                    leave.AllocationStatus = 1;
                                    leave.TenantId = Convert.ToString(Session["TenantId"]);
                                    leave.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                                    HR.LeaveAllocationTBs.InsertOnSubmit(leave);
                                    HR.SubmitChanges();
                                    EmployeeTB emp = HR.EmployeeTBs.Where(d => d.EmployeeId == Convert.ToInt32(hfKey.Value)).First();
                                    emp.AllocationStatus = 1;
                                    HR.SubmitChanges();
                                    cnt++;
                                }
                                catch (Exception ex)
                                {

                                }
                            }
                        }
                        if (cnt > 0)
                        {
                            g.ShowMessage(this.Page, "Leave allocated successfully..");
                        }
                    }
                    else if (btnsubmit.Text == "Update")
                    {
                        for (int i = 0; i < dtt.Rows.Count; i++)
                        {
                            string leaveId = dtt.Rows[i]["LeaveID"].ToString();
                            string fromDate = dtt.Rows[i]["FromDateAllocation"].ToString();
                            string toDate = dtt.Rows[i]["ToDateAllocation"].ToString();

                            try
                            {
                                var leave = HR.LeaveAllocationTBs.Where(a => a.LeaveAllocateID == Convert.ToInt32(hfleaveallocationid.Value)).FirstOrDefault();

                                DateTime fDate = Convert.ToDateTime(fromDate);
                                DateTime tDate = Convert.ToDateTime(toDate);
                                
                                leave.EmployeeID = Convert.ToInt32(hfKey.Value);                               
                                leave.FromDateAllocation = new DateTime(fDate.Year, fDate.Month, fDate.Day);
                                leave.ToDateAllocation = new DateTime(tDate.Year, tDate.Month, tDate.Day);
                                leave.LeaveID = Convert.ToInt32(dtt.Rows[i][0].ToString());
                                leave.TotalAllocatedLeaves = Convert.ToInt32(dtt.Rows[i][4].ToString());
                                leave.PendingLeaves = Convert.ToInt32(dtt.Rows[i][4].ToString());
                                leave.AllocationStatus = 1;
                                leave.TenantId = Convert.ToString(Session["TenantId"]);
                                leave.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                               
                                HR.SubmitChanges();
                                EmployeeTB emp = HR.EmployeeTBs.Where(d => d.EmployeeId == Convert.ToInt32(hfKey.Value)).First();
                                emp.AllocationStatus = 1;
                                HR.SubmitChanges();
                                //cnt++;
                            }
                            catch (Exception ex)
                            {

                            }


                        }
                    }
                }

                Clear11();
            }
            else
            {
                g.ShowMessage(this.Page, "Please select company..");
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
        MultiView1.ActiveViewIndex = 0;

    }
    public void Clear11()
    {
        ViewState["dtt"] = null;
        grd_Leaves.DataSource = null;
        grd_Leaves.DataBind(); BindAllEmp();
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        Clear11();

    }
    private void fillLeaves()
    {
        ddlLeaveType.Items.Clear();       
        int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
        var data = from dt in HR.masterLeavesTBs
                   where dt.Status == 1 && dt.CompanyId==cId&&dt.TenantId==Convert.ToString(Session["TenantId"])
                   select dt;
        if (data != null && data.Count() > 0)
        {
            ddlLeaveType.DataSource = data;
            ddlLeaveType.DataTextField = "LeaveName";
            ddlLeaveType.DataValueField = "LeaveID";
            ddlLeaveType.DataBind();


        }
        ddlLeaveType.Items.Insert(0, new ListItem("--Select--", "0"));       
    }

    private void fillMasterLeaves()
    {       
        ddlmasterleavetype.Items.Clear();
        int cId = ddlCompanyList.SelectedIndex > 0 ? Convert.ToInt32(ddlCompanyList.SelectedValue) : 0;
        var data = from dt in HR.masterLeavesTBs
                   where dt.Status == 1 && dt.CompanyId == cId && dt.TenantId == Convert.ToString(Session["TenantId"])
                   select dt;
        if (data != null && data.Count() > 0)
        {           
            ddlmasterleavetype.DataSource = data;
            ddlmasterleavetype.DataTextField = "LeaveName";
            ddlmasterleavetype.DataValueField = "LeaveID";
            ddlmasterleavetype.DataBind();
        }        
        ddlmasterleavetype.Items.Insert(0, new ListItem("--Select--", "0"));
    }

    protected void BtnAdd1_Click(object sender, EventArgs e)
    {
        if (IsValid)
        {
            if (int.Parse(txtLeaves.Text) <= 365)
            {
                //litDates.Text = "from date : " + txtStartdate.Text + " & to date : " + txtEndDate.Text;

            

                try
                {
                    int max = string.IsNullOrEmpty(hfMaxLeaves.Value) ? 0 : Convert.ToInt32(hfMaxLeaves.Value);
                    int selected = string.IsNullOrEmpty(txtLeaves.Text) ? 0 : Convert.ToInt32(txtLeaves.Text);
                    if (ViewState["dtt"] != null)
                    {
                        dtt = (DataTable)ViewState["dtt"];

                        for (int i = 0; i < dtt.Rows.Count; i++)
                        {
                            if (dtt.Rows[i][0].ToString() == ddlLeaveType.SelectedValue)
                            {

                                g.ShowMessage(this.Page, "Leave Type already Exists");
                                status1 = true;
                                // dtt.Rows[i].Delete();

                            }
                        }

                        if (status1 != true)
                        {
                            if (max >= selected)
                            {
                                if (ViewState["dtt"] != null)
                                {
                                    dtt = (DataTable)ViewState["dtt"];

                                    DataRow dr = dtt.NewRow();

                                    dr[0] = ddlLeaveType.SelectedValue;
                                    dr[1] = ddlLeaveType.SelectedItem.Text;
                                    dr[2] = new DateTime(Convert.ToInt32(txtLeaveYear.Text), 1, 1);
                                    dr[3] = new DateTime(Convert.ToInt32(txtLeaveYear.Text), 12, 31);
                                    dr[4] = txtLeaves.Text;
                                    dtt.Rows.Add(dr);
                                    ViewState["dtt"] = dtt;


                                }
                            }
                            else
                            {
                                g.ShowMessage(this.Page, "Leaves count should be less than yearly limit of : " + hfMaxLeaves.Value);
                            }
                        }
                    }
                    else
                    {
                        
                        if (max >= selected)
                        {
                            dtt = new DataTable();

                            DataColumn LeaveID = dtt.Columns.Add("LeaveID");
                            DataColumn LeaveName = dtt.Columns.Add("LeaveName");
                            DataColumn FromDateAllocation = dtt.Columns.Add("FromDateAllocation");
                            DataColumn ToDateAllocation = dtt.Columns.Add("ToDateAllocation");
                            DataColumn TotalAllocatedLeaves = dtt.Columns.Add("TotalAllocatedLeaves");
                            DataColumn PendingLeaves = dtt.Columns.Add("PendingLeaves");
                            DataRow dr = dtt.NewRow();
                            dr[0] = ddlLeaveType.SelectedValue;
                            dr[1] = ddlLeaveType.SelectedItem.Text;
                            dr[2] = new DateTime(Convert.ToInt32(txtLeaveYear.Text),1,1);
                            dr[3] = new DateTime(Convert.ToInt32(txtLeaveYear.Text), 12, 31);
                            dr[4] = txtLeaves.Text;

                            dtt.Rows.Add(dr);
                            ViewState["dtt"] = dtt;
                        }
                        else
                        {
                            g.ShowMessage(this.Page, "Leaves count should be less than yearly limit of : " + hfMaxLeaves.Value);
                        }
                    }


                    grd_Leaves.DataSource = dtt;
                    grd_Leaves.DataBind();


                    MultiView1.ActiveViewIndex = 1;
                    Clear();
                }
                catch(Exception ex)
                {
                    throw ex;
                }
                //litDates.Text += "chaNGED from date : " +txtStartdate.Text + " & to date : " + txtEndDate.Text;

            }
            else
            {
                txtLeaves.Text = "";
                g.ShowMessage(this.Page, "InValid Days");

            }
        }
    }
    public void Clear()
    {
        ddlLeaveType.SelectedIndex = -1;

        //txtStartdate.Text = "";
        //txtEndDate.Text = "";
        txtLeaveYear.Text = DateTime.Now.Year.ToString();
        txtLeaves.Text = "";

    }
    
    protected void imgdelete_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void imgeditleave_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;

        ImageButton imgedit = (ImageButton)sender;
        string ItemId = imgedit.CommandArgument;
        dtt = (DataTable)ViewState["dtt"];


        foreach (DataRow d in dtt.Rows)
        {
            if (d[1].ToString() == imgedit.CommandArgument)
            {


                ddlLeaveType.SelectedValue = d["LeaveID"].ToString();
                ddlLeaveType.SelectedItem.Text = d["LeaveName"].ToString();
                //txtStartdate.Text = d["FromDateAllocation"].ToString();
                //txtEndDate.Text = d["ToDateAllocation"].ToString();
                string year = d["FromDateAllocation"].ToString();
                txtLeaveYear.Text = Convert.ToDateTime(year).Year.ToString();
                txtLeaves.Text = d["TotalAllocatedLeaves"].ToString();
                //.Text = d["PendingLeaves"].ToString();

                d.Delete();
                dtt.AcceptChanges();

                break;
            }
        }

        grd_Leaves.DataSource = dtt;
        grd_Leaves.DataBind();
    }
    protected void imgdeleteleave_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        ImageButton imgdelete = (ImageButton)sender;
        string ServiceId = imgdelete.CommandArgument;
        dtt = (DataTable)ViewState["dtt"];

        foreach (DataRow d in dtt.Rows)
        {
            if (d[1].ToString() == imgdelete.CommandArgument)
            {

                d.Delete();
                dtt.AcceptChanges();
                break;
            }
        }

        grd_Leaves.DataSource = dtt;
        grd_Leaves.DataBind();
    }
    public void BindRenewEmployee()
    {
        var renewdata = from d in HR.LeaveAllocationTBs
                        join l in HR.masterLeavesTBs on d.LeaveID equals l.LeaveID
                        where d.EmployeeID == Convert.ToInt32(Session["empp7"])
                        && l.TenantId==Convert.ToString(Session["TenantId"])
                        select new
                        {
                            l.LeaveName,
                            d.LeaveAllocateID,
                            d.EmployeeID,
                            d.TotalAllocatedLeaves,
                            d.PendingLeaves,
                            d.FromDateAllocation,
                            d.ToDateAllocation
                        };

        if (renewdata.Count() > 0)
        {
            GridviewLeaveAllocationHistoryBind.DataSource = renewdata;
            GridviewLeaveAllocationHistoryBind.DataBind();
        }
        else
        {
            GridviewLeaveAllocationHistoryBind.DataSource = null;
            GridviewLeaveAllocationHistoryBind.DataBind();
        }
    }

    protected void lbtnRenew_Click(object sender, EventArgs e)
    {
        LinkButton Lnk2 = (LinkButton)sender;
        string emp1ID = Lnk2.CommandArgument;
        hfKey.Value = Lnk2.CommandArgument;
        Session["empp7"] = emp1ID;


        var EmpData1 = (from d in HR.EmployeeTBs
                        join n in HR.MasterDeptTBs on d.DeptId equals n.DeptID
                        join g in HR.LeaveAllocationTBs on d.EmployeeId equals g.EmployeeID
                        where g.EmployeeID == Convert.ToInt32(emp1ID)
                        select new
                        {
                            Name = d.Solitude == 0 ? "Mr" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 1 ? "Ms" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 2 ? "Mrs" + " " + d.FName + " " + d.MName + " " + d.Lname : "Dr." + " " + d.FName + " " + d.MName + " " + d.Lname,
                            Name1 = d.Solitude == 0 ? "Mr" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 1 ? "Ms" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 2 ? "Mrs" + " " + d.FName + " " + d.MName + " " + d.Lname : "Dr." + " " + d.FName + " " + d.MName + " " + d.Lname,
                            d.FName,
                            d.Lname,
                            d.EmployeeId,
                            d.BirthDate,
                            d.Email,

                            DOJ1 = d.DOJ,
                            d.ConfirmDate,
                            d.PanNo,
                            d.ContactNo,
                            d.PassportNo,
                            n.DeptName,
                            EmpName = d.FName + " " + d.Lname,
                            AllocationStatus = d.AllocationStatus == 1 ? "Allocate" : "Not Allocated"
                        }).Distinct();

        if (EmpData1.Count() > 0)
        {
            MultiView1.ActiveViewIndex = 2;
            GridViewRenew.DataSource = EmpData1;
            GridViewRenew.DataBind();

            BindRenewEmployee();
        }
        else
        {
            g.ShowMessage(this.Page, "No Details Available");
            MultiView1.ActiveViewIndex = 0;
            GridViewRenew.DataSource = null;
            GridViewRenew.DataBind();
        }


    }
    protected void lblRenewHistory_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        LinkButton Lnk4 = (LinkButton)sender;
        string emp1ID = Lnk4.CommandArgument;
        hfKey.Value=Lnk4.CommandArgument;
        Session["empp4"] = emp1ID;

        grd_Leaves.DataSource = null;
        grd_Leaves.DataBind();

        BindRenewEmployee();
    }


    protected void lblEditRenewHistory_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        ImageButton Lnk = (ImageButton)sender;       
        string LeaveallocationID = Lnk.CommandArgument;
        hfleaveallocationid.Value = LeaveallocationID;
        var leaveallocationdata = HR.LeaveAllocationTBs.Where(a => a.LeaveAllocateID == Convert.ToInt32(LeaveallocationID)).FirstOrDefault();

        ddlCompany.SelectedValue = leaveallocationdata.CompanyId.ToString();
        fillLeaves();
        ddlLeaveType.SelectedValue = leaveallocationdata.LeaveID.ToString();
        DateTime fromdate = Convert.ToDateTime(leaveallocationdata.FromDateAllocation);
        txtLeaveYear.Text = fromdate.Year.ToString();
        txtLeaves.Text = leaveallocationdata.TotalAllocatedLeaves.ToString();

        btnsubmit.Text = "Update";

        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            hfMaxLeaves.Value = "0";
            int lId = ddlLeaveType.SelectedIndex > 0 ? Convert.ToInt32(ddlLeaveType.SelectedValue) : 0;
            var data = db.masterLeavesTBs.Where(d => d.LeaveID == lId).FirstOrDefault();
            if (data != null)
            {
                hfMaxLeaves.Value = data.YearlyLimit.Value.ToString();
            }
        }

        grd_Leaves.DataSource = null;
        grd_Leaves.DataBind();

        BindRenewEmployee();
    }


    protected void grd_Emp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Emp.PageIndex = e.NewPageIndex;
        BindAllEmp();

    }
    protected void GridviewLeaveAllocationHistoryBind_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridviewLeaveAllocationHistoryBind.PageIndex = e.NewPageIndex;
        BindRenewEmployee();
    }
    protected void txtEndDate_TextChanged(object sender, EventArgs e)
    {
        //if (txtStartdate.Text == "" || txtEndDate.Text == "")
        //{
        //    lblduration.Text = "Please Enter Dates";
        //}
        //else
        //{

        ////TimeSpan ts = Convert.ToDateTime(txtEndDate.Text) - Convert.ToDateTime(txtStartdate.Text);
        ////int totalduration = ts.Days + 1;
        ////txtTotalAllocatedLeaves.Text = Convert.ToString(totalduration);
        ////int days = int.Parse(txtTotalAllocatedLeaves.Text);
        ////lblduration.Visible = false;
        ////btnsubmit.Enabled = true; 
        ////BtnAdd1.Enabled = true;
        //if (days <= 0)
        //{
        //    lblduration.Text = "InValid Data";
        //    lblduration.Visible = true;
        //    btnsubmit.Enabled = false; 
        //    BtnAdd1.Enabled = false;
        //}
        //if (days > 10)
        //{
        //    lblduration.Text = "More Than 10 Leaves Are not allowed";
        //    lblduration.Visible = true;
        //    btnsubmit.Enabled = false;
        //    BtnAdd1.Enabled = false;
        //}
        //}
    }
    protected void btnback_Click(object sender, EventArgs e)
    {
        Clear();
        Clear11();
        MultiView1.ActiveViewIndex = 0;
    }
    protected void ddlCompanyList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompanyList.SelectedIndex > 0)
        {
            BindDepartment(ddlCompanyList.SelectedItem.Text);
            FillEmployeeList();
            BindAllEmp();
            fillMasterLeaves();

        }
        else
        {
            ddlDepartment.Items.Clear();
            ddlEmployeeList.Items.Clear();
            BindAllEmp();
            ddlmasterleavetype.Items.Clear();
        }
    }
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlDepartment.SelectedIndex > 0)
        {
            FillEmployeeList();
            BindAllEmp();
        }
        else
        {
            ddlEmployeeList.Items.Clear();
            BindAllEmp();
        }
    }
    protected void ddlEmployeeList_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAllEmp();
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        BindAllEmp();
    }
    protected void btnReset_Click(object sender, EventArgs e)
    {
        ddlCompanyList.SelectedIndex = 0;
        ddlDepartment.Items.Clear();
        ddlEmployeeList.Items.Clear();
        BindAllEmp();
    }
    protected void OnClick_Edit(object sender, ImageClickEventArgs e)
    {
        try
        {
            MultiView1.ActiveViewIndex = 1;
            ImageButton Lnk1 = (ImageButton)sender;
            string emp1ID = Lnk1.CommandArgument;
            Session["empp2"] = emp1ID;
            ddlLeaveType.Focus();

            var dt = (from p in HR.EmployeeTBs
                     join k in HR.CompanyInfoTBs on p.CompanyId equals k.CompanyId
                     
                     where p.EmployeeId == int.Parse(emp1ID)
                     select new { k.CompanyId }).ToList();
            if (dt.Count() > 0)
            {
               
            }
        }
        catch
        {

        }
    }

    protected void grd_Emp_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grd_Emp.Rows.Count > 0)
        {
            grd_Emp.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            grd_Emp.HeaderRow.TableSection =
            TableRowSection.TableHeader;
        }
    }

    protected void lbtnAllocate_Click(object sender, EventArgs e)
    {
        try
        {
            MultiView1.ActiveViewIndex = 1;
            LinkButton Lnk1 = (LinkButton)sender;
            string emp1ID = Lnk1.CommandArgument;
            hfKey.Value = Lnk1.CommandArgument;
            Session["empp2"] = emp1ID;
            ddlLeaveType.Focus();

            var empData = HR.EmployeeTBs.Where(d => d.EmployeeId == Convert.ToInt32(hfKey.Value)).FirstOrDefault();
            ddlCompany.SelectedValue = empData.CompanyId.Value.ToString();
            ddlCompany.Enabled = false;
            fillLeaves();

            txtLeaveYear.Text = DateTime.Now.Year.ToString();
        }
        catch
        {

        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillLeaves();
    }

    protected void btnTestDate_Click(object sender, EventArgs e)
    {
        DateTime date;
        //DateTime tDate = Genreal.GetDate(txtEndDate.Text);

        ////TryParse()
        //if (DateTime.TryParse(txtEndDate.Text, out date))
        //{
        //    g.ShowMessage(this.Page, "\nParseExact() method: " + date);
        //}
        //else
        //{
        //    g.ShowMessage(this.Page, "Conversion failed for : "+txtEndDate.Text);
        //    Console.WriteLine("Conversion failed");
        //}
    }

    protected void ddlLeaveType_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                hfMaxLeaves.Value = "0";
                int lId = ddlLeaveType.SelectedIndex > 0 ? Convert.ToInt32(ddlLeaveType.SelectedValue) : 0;
                var data = db.masterLeavesTBs.Where(d => d.LeaveID == lId).FirstOrDefault();
                if (data != null)
                {
                    txtLeaves.Text = data.YearlyLimit.Value.ToString();
                    hfMaxLeaves.Value = data.YearlyLimit.Value.ToString();
                }

                txtLeaveYear.Text = DateTime.Now.Year.ToString();
            }
        }
        catch (Exception ex) { }
    }

    //protected void txtLeaves_TextChanged(object sender, EventArgs e)
    //{
    //    using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
    //    {
    //        hfMaxLeaves.Value = "0";
    //        int lId = ddlLeaveType.SelectedIndex > 0 ? Convert.ToInt32(ddlLeaveType.SelectedValue) : 0;
    //        var data = db.masterLeavesTBs.Where(d => d.LeaveID == lId).FirstOrDefault();
    //        if (data != null)
    //        {                
    //            hfMaxLeaves.Value = data.YearlyLimit.Value.ToString();
    //        }           
    //    }
    //}
}