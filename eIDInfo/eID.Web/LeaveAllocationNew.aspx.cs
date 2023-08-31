using System;
using System.Data;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
/// <summary>
/// Amit shinde changes of searching
/// </summary>
public partial class LeaveAllocationNew : System.Web.UI.Page
{

    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    DataTable dtt = new DataTable();
    //  DataTable DtExperience = new DataTable();
    string AttachPath;
    Genreal g = new Genreal();
    String Employeeid;
    Boolean status1 = false;
    string RenewCount;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {

            if (!IsPostBack)
            {
                BindRenewEmployee();
                BindAllEmp();
                fillcompany();
                txtStartdate.Attributes.Add("Readonly", "Readonly");
                txtEndDate.Attributes.Add("Readonly", "Readonly");
                fillLeaves();
                txtDate.Text = DateTime.Now.Date.ToShortDateString();
                txtDate.Attributes.Add("Readonly", "True");

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
    }

    public void BindAllEmp()
    {
        //bool Status = g.CheckAdmin(Convert.ToInt32(Session["UserId"]));

        //if (Status == true)
        //{
        #region admin
        var EmpData = (from d in HR.EmployeeTBs
                       join n in HR.MasterDeptTBs on d.DeptId equals n.DeptID
                       where d.RelivingStatus == null
                       select new
                       {
                           Name = d.Solitude == 0 ? "Mr" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 1 ? "Ms" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 2 ? "Mrs" + " " + d.FName + " " + d.MName + " " + d.Lname : "Dr." + " " + d.FName + " " + d.MName + " " + d.Lname,
                           Name1 = d.Solitude == 0 ? "Mr" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 1 ? "Ms" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 2 ? "Mrs" + " " + d.FName + " " + d.MName + " " + d.Lname : "Dr." + " " + d.FName + " " + d.MName + " " + d.Lname,
                           d.FName,
                           d.Lname,
                           d.CompanyId,
                           d.EmployeeId,
                           d.BirthDate,
                           d.ConfirmDate,
                           d.Email,
                           DOJ1 = d.DOJ,
                           d.PanNo,
                           d.ContactNo,
                           d.PassportNo,
                           n.DeptName,
                           emnae = d.FName + " " + d.Lname,
                           AllocationStatus = d.AllocationStatus == 1 ? "Allocated" : "Not Allocated"
                       });


        if (ddlCompanyList.SelectedIndex > 0)
        {
            EmpData = EmpData.Where(d => d.CompanyId.Equals(ddlCompanyList.SelectedValue));

            if (ddlCompanyList.SelectedIndex > 0 && ddlDepartment.SelectedIndex > 0)
            {
                EmpData = EmpData.Where(d => d.CompanyId.Equals(ddlCompanyList.SelectedValue) && d.DeptName.Equals(ddlDepartment.SelectedItem.Text));

                if (ddlCompanyList.SelectedIndex > 0 && ddlDepartment.SelectedIndex > 0 && ddlEmployeeList.SelectedIndex > 0)
                {
                    EmpData = EmpData.Where(d => d.CompanyId.Equals(ddlCompanyList.SelectedValue) && d.DeptName.Equals(ddlDepartment.SelectedItem.Text) && d.EmployeeId.Equals(ddlEmployeeList.SelectedValue));
                }
            }
        }


        grd_Emp.DataSource = EmpData;
        grd_Emp.DataBind();
        lblcnt.Text = EmpData.Count().ToString();
        for (int i = 0; i < grd_Emp.Rows.Count; i++)
        {
            ImageButton lblRenew = (ImageButton)grd_Emp.Rows[i].FindControl("lblRenew");
            ImageButton lblAllocate = (ImageButton)grd_Emp.Rows[i].FindControl("lblAllocate");
            //// LinkButton lblAllocate = (LinkButton)grd_Emp.Rows[i].FindControl("lblAllocate");
            var Allocateddata = from d in HR.EmployeeTBs
                                where d.AllocationStatus == 1
                                select new { d.EmployeeId };

            if (Allocateddata.Count() > 0)
            {

                if (grd_Emp.Rows[i].Cells[8].Text == "Allocated")
                {
                    lblAllocate.Visible = false;
                    lblRenew.Visible = true;
                }
                else
                {
                    lblRenew.Visible = false;
                    lblAllocate.Visible = true;

                }

            }
            else
            {
                lblRenew.Visible = false;
            }
        }
        #endregion
    }
    /// <summary>
    /// Amit Shinde
    /// </summary>

    #region bind dropdownlist
    private void fillcompany()
    {
        try
        {
            var data = (from dt in HR.CompanyInfoTBs
                        where dt.Status == 0
                        select dt).OrderBy(dt => dt.CompanyName);
            if (data != null && data.Count() > 0)
            {

                ddlCompanyList.DataSource = data;
                ddlCompanyList.DataTextField = "CompanyName";
                ddlCompanyList.DataValueField = "CompanyId";
                ddlCompanyList.DataBind();
                ddlCompanyList.Items.Insert(0, "--Select--");
            }
            else
            {
                ddlDepartment.Items.Clear();
                ddlEmployeeList.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
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
            if (ddlCompanyList.SelectedIndex > 0)
            {
                if (ddlDepartment.SelectedIndex != 0)
                {
                    var data = (from dtReportHead in HR.EmployeeTBs
                                //join dep in HR.MasterDeptTBs on dtReportHead.CompanyId equals dep.CompanyId
                                where dtReportHead.RelivingStatus == null && dtReportHead.CompanyId == Convert.ToInt32(ddlCompanyList.SelectedValue) && dtReportHead.DeptId == Convert.ToInt32(ddlDepartment.SelectedValue)
                                select new
                                {
                                    dtReportHead.EmployeeId,
                                    Name = dtReportHead.FName + ' ' + dtReportHead.MName + ' ' + dtReportHead.Lname
                                }).OrderBy(dt => dt.Name);


                    if (data != null && data.Count() > 0)
                    {
                        ddlEmployeeList.DataSource = data;
                        ddlEmployeeList.DataTextField = "Name";
                        ddlEmployeeList.DataValueField = "EmployeeId";
                        ddlEmployeeList.DataBind();
                        ddlEmployeeList.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        ddlEmployeeList.Items.Clear();
                    }
                }
                else
                {
                    ddlEmployeeList.Items.Clear();

                }
            }
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
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlstate_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        int emp3 = Convert.ToInt32(Session["empp4"]);

        var dataexistsemp = (from empd in HR.LeaveAllocationTBs
                             where empd.EmployeeID == emp3
                             select empd).ToList();
        if (dataexistsemp.Count() > 0)
        {
            dtt = ViewState["dtt"] as DataTable;
            if (ViewState["dtt"] != null)
            {
                // data table to match records which are already Exists
                for (int i = 0; i < dtt.Rows.Count; i++)
                {
                    DateTime StartDate = Convert.ToDateTime(dtt.Rows[i][2].ToString());
                    DateTime EndDate = Convert.ToDateTime(dtt.Rows[i][3].ToString());
                    int leaveid1 = Convert.ToInt32(dtt.Rows[i][0].ToString());

                    DataTable dt22 = g.ReturnData("select  Datepart(year,FromDateAllocation) as FromDateAllocation,Datepart(year,ToDateAllocation) as ToDateAllocation from LeaveAllocationTB where LeaveAllocationTB.EmployeeID = ('" + Convert.ToInt32(Session["empp4"]) + "') and  LeaveAllocationTB.LeaveID = ('" + Convert.ToInt32(leaveid1) + "') and   Datepart(year,LeaveAllocationTB.FromDateAllocation)>=  DATEPART(YEAR,'" + StartDate + "')  and Datepart(year,LeaveAllocationTB.ToDateAllocation) <= DATEPART(YEAR,'" + EndDate + "')");

                    if (dt22.Rows.Count > 0)
                    {
                        g.ShowMessage(this.Page, "Leaves Are Already Allocated To This Year");
                    }
                    else
                    {
                        LeaveAllocationTB leave = new LeaveAllocationTB();
                        leave.EmployeeID = Convert.ToInt32(Session["empp4"]);
                        leave.FromDateAllocation = Convert.ToDateTime(dtt.Rows[i][2].ToString());
                        leave.ToDateAllocation = Convert.ToDateTime(dtt.Rows[i][3].ToString());
                        leave.LeaveID = Convert.ToInt32(dtt.Rows[i][0].ToString());
                        leave.TotalAllocatedLeaves = Convert.ToInt32(dtt.Rows[i][4].ToString());
                        leave.PendingLeaves = Convert.ToInt32(dtt.Rows[i][4].ToString());
                        var RenewCountData = from d in HR.LeaveAllocationTBs
                                             where d.EmployeeID == Convert.ToInt32(Session["empp4"])
                                             select new { d.RenewStatus };
                        if (RenewCountData.Count() == 0)
                        {
                            RenewCount = "1";
                        }
                        else
                        {
                            foreach (var item in RenewCountData)
                            {
                                RenewCount = (Convert.ToInt32(item.RenewStatus) + 1).ToString();
                            }

                        }
                        leave.RenewStatus = Convert.ToInt32(RenewCount);
                        HR.LeaveAllocationTBs.InsertOnSubmit(leave);
                        HR.SubmitChanges();
                        BindAllEmp();
                        g.ShowMessage(this.Page, "Data Saved Successfully");

                        //modpop.Message = "..  ..";
                        //modpop.ShowPopUp();
                        //EmployeeTB emp = HR.EmployeeTBs.Where(d => d.EmployeeId == Convert.ToInt32(Session["empp2"])).First();
                        //emp.AllocationStatus = 1;
                        //HR.SubmitChanges();

                    }
                }



            }
            else
            {

            }


        }
        else
        {
            #region First Time Save
            if (btnsubmit.Text == "Save")
            {
                if (ViewState["dtt"] == null)
                {
                    g.ShowMessage(this.Page, "Please Enter Your all Details");
                }
                else
                {
                    dtt = ViewState["dtt"] as DataTable;
                    if (ViewState["dtt"] != null)
                    {
                        for (int i = 0; i < dtt.Rows.Count; i++)
                        {
                            DataTable dt22 = g.ReturnData("select  Datepart(year,FromDateAllocation) as FromDateAllocation,Datepart(year,ToDateAllocation) as ToDateAllocation from LeaveAllocationTB where LeaveAllocationTB.EmployeeID = ('" + Convert.ToInt32(Session["UserId"]) + "') and LeaveAllocationTB.LeaveID = ('" + ddlLeaveType.SelectedValue + "') and Datepart(year,LeaveAllocationTB.FromDateAllocation)>=  DATEPART(YEAR,'" + txtStartdate.Text + "')  and Datepart(year,LeaveAllocationTB.ToDateAllocation) <= DATEPART(YEAR,'" + txtEndDate.Text + "')");
                            if (dt22.Rows.Count > 0)
                            {
                                g.ShowMessage(this.Page, "Leaves are Already Allocated");
                                //modpop.Message = "..  ..";
                                //modpop.ShowPopUp();

                            }
                            else
                            {

                                LeaveAllocationTB leave = new LeaveAllocationTB();
                                leave.EmployeeID = Convert.ToInt32(Session["empp2"]);
                                leave.FromDateAllocation = Convert.ToDateTime(dtt.Rows[i][2].ToString());
                                leave.ToDateAllocation = Convert.ToDateTime(dtt.Rows[i][3].ToString());
                                leave.LeaveID = Convert.ToInt32(dtt.Rows[i][0].ToString());
                                leave.TotalAllocatedLeaves = Convert.ToInt32(dtt.Rows[i][4].ToString());
                                leave.PendingLeaves = Convert.ToInt32(dtt.Rows[i][4].ToString());
                                leave.AllocationStatus = 1;
                                HR.LeaveAllocationTBs.InsertOnSubmit(leave);
                                HR.SubmitChanges();
                                EmployeeTB emp = HR.EmployeeTBs.Where(d => d.EmployeeId == Convert.ToInt32(Session["empp2"])).First();
                                emp.AllocationStatus = 1;
                                HR.SubmitChanges();

                            }
                        }

                        g.ShowMessage(this.Page, "Data Saved Successfully");
                        //modpop.Message = "..  ..";
                        //modpop.ShowPopUp();

                    }
                    else
                    {

                    }
                }
            }
            else
            {
                int e1 = Convert.ToInt32(Session["empp1"]);
                LeaveAllocationTB leave = HR.LeaveAllocationTBs.Where(d => d.EmployeeID == e1).First();

                leave.EmployeeID = Convert.ToInt32(Session["UserId"]);
                leave.FromDateAllocation = DateTime.Parse(txtStartdate.Text);
                leave.ToDateAllocation = DateTime.Parse(txtEndDate.Text);
                leave.LeaveID = Convert.ToInt32(ddlLeaveType.SelectedValue);
                leave.TotalAllocatedLeaves = int.Parse(txtTotalAllocatedLeaves.Text);
                leave.PendingLeaves = int.Parse(txtTotalAllocatedLeaves.Text);


                HR.SubmitChanges();

            }

            #endregion
        }

        grd_Leaves.DataSource = null;
        grd_Leaves.DataBind();

        MultiView1.ActiveViewIndex = 0;

        Clear11();
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
        var data = from dt in HR.masterLeavesTBs
                   where dt.Status == 0
                   select dt;
        if (data != null && data.Count() > 0)
        {
            ddlLeaveType.DataSource = data;
            ddlLeaveType.DataTextField = "LeaveName";
            ddlLeaveType.DataValueField = "LeaveID";
            ddlLeaveType.DataBind();
            ddlLeaveType.Items.Insert(0, "--Select--");


        }
    }

    protected void BtnAdd1_Click(object sender, EventArgs e)
    {
        if (int.Parse(txtTotalAllocatedLeaves.Text) <= 365)
        {


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
                    if (ViewState["dtt"] != null)
                    {
                        dtt = (DataTable)ViewState["dtt"];

                        DataRow dr = dtt.NewRow();

                        dr[0] = ddlLeaveType.SelectedValue;
                        dr[1] = ddlLeaveType.SelectedItem.Text;
                        dr[2] = txtStartdate.Text;
                        dr[3] = txtEndDate.Text;
                        dr[4] = txtTotalAllocatedLeaves.Text;
                        dtt.Rows.Add(dr);
                        ViewState["dtt"] = dtt;


                    }
                }
            }
            else
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
                dr[2] = txtStartdate.Text;
                dr[3] = txtEndDate.Text;
                dr[4] = txtTotalAllocatedLeaves.Text;

                dtt.Rows.Add(dr);
                ViewState["dtt"] = dtt;

            }


            grd_Leaves.DataSource = dtt;
            grd_Leaves.DataBind();


            MultiView1.ActiveViewIndex = 1;
            Clear();
        }
        else
        {
            txtTotalAllocatedLeaves.Text = "";
            g.ShowMessage(this.Page, "InValid Days");

        }

    }
    public void Clear()
    {
        ddlLeaveType.SelectedIndex = -1;

        txtStartdate.Text = "";
        txtEndDate.Text = "";
        txtTotalAllocatedLeaves.Text = "";

    }

    protected void imgedit_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        LinkButton Lnk = (LinkButton)sender;
        string emp1ID = Lnk.CommandArgument;
        Session["empp1"] = emp1ID;
        LeaveAllocationTB leave = HR.LeaveAllocationTBs.Where(d => d.EmployeeID == Convert.ToInt32(emp1ID)).First();


        (txtStartdate.Text) = Convert.ToDateTime(leave.FromDateAllocation).ToString();
        (txtEndDate.Text) = Convert.ToString(leave.ToDateAllocation);

        (ddlLeaveType.SelectedValue) = Convert.ToString(leave.LeaveID);
        (txtTotalAllocatedLeaves.Text) = Convert.ToString(leave.TotalAllocatedLeaves);
        (txtTotalAllocatedLeaves.Text) = Convert.ToString(leave.PendingLeaves);


        btnsubmit.Text = "Update";
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
                txtStartdate.Text = d["FromDateAllocation"].ToString();
                txtEndDate.Text = d["ToDateAllocation"].ToString();
                txtTotalAllocatedLeaves.Text = d["TotalAllocatedLeaves"].ToString();
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
                        select new
                        {
                            l.LeaveName,
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

    protected void lblRenew_Click(object sender, EventArgs e)
    {
        ImageButton Lnk2 = (ImageButton)sender;
        string emp1ID = Lnk2.CommandArgument;
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
                            emnae = d.FName + " " + d.Lname,
                            AllocationStatus = d.AllocationStatus == 1 ? "Allocate" : "Not Allocated"
                        }).Distinct();

        if (EmpData1.Count() > 0)
        {
            MultiView1.ActiveViewIndex = 2;
            GridViewRenew.DataSource = EmpData1;
            GridViewRenew.DataBind();
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
        Session["empp4"] = emp1ID;

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

        }
        else
        {
            ddlDepartment.Items.Clear();
            ddlEmployeeList.Items.Clear();
            BindAllEmp();
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
}