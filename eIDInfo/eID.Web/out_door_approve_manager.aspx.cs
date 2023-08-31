using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class out_door_approve_manager : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    private void BindJqFunctions()
    {
        //jqFunctions
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack) {
            fillcompany();
            BindPendingLeavesList();
        }
        if (gvPendingLeaves.Rows.Count > 0)
            gvPendingLeaves.HeaderRow.TableSection = TableRowSection.TableHeader;
        BindJqFunctions();
    }

    protected void lbtnApprove_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = (LinkButton)sender;
        hfKey.Value = linkButton.CommandArgument;
        MultiView1.ActiveViewIndex = 1;

        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                var data  =(from d in  db.OutDoorEntryTBs
                            join emp in db.EmployeeTBs on d.EmployeeId equals emp.EmployeeId
                            where d.OddId == Convert.ToInt32(linkButton.CommandArgument)
                            select new
                            {
                                d.OddId,
                                d.ManagerStatus,
                                d.FromDate,
                                d.ToDate,
                                d.TravelPlace,
                                d.TravelReason,
                                d.Description,
                                EmpName =emp.FName+ " "+emp.Lname,
                                d.ManagerRemark
                            }).FirstOrDefault();

                lblEmpName.Text = data.EmpName;
                lblFromdate.Text = data.FromDate.Value.Date.ToString("MM/dd/yyyy");
                lblLeaveType.Text = data.TravelPlace;
                lblReason.Text = data.TravelReason;
            }
        }
        catch (Exception ex) { }

        //try
        //{
        //    using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        //    {
        //        LeaveApplicationsTB applicationsTB = db.LeaveApplicationsTBs.Where(d => d.LeaveApplicationId == Convert.ToInt32(linkButton.CommandArgument)).FirstOrDefault();
        //        applicationsTB.HrStatus = "Approve";
        //        db.SubmitChanges();
        //        if (applicationsTB.LeaveTypeId > 0)
        //        {
        //            DateTime dateTime = applicationsTB.ApplicationDate.Value;
        //            LeaveAllocationTB allocationTB = db.LeaveAllocationTBs.Where(d => d.EmployeeID == applicationsTB.EmployeeId &&
        //            dateTime.Date >= d.FromDateAllocation.Value.Date && dateTime.Date <= d.ToDateAllocation.Value
        //            && d.LeaveID == applicationsTB.LeaveTypeId).FirstOrDefault();
        //            double duration = applicationsTB.Duration.Value;
        //            double pendingLeaves = allocationTB.PendingLeaves.Value;

        //            allocationTB.PendingLeaves = (pendingLeaves - duration);
        //            db.SubmitChanges();
        //        }

        //        gen.ShowMessage(this.Page, "Leave Approved Successfully..");

        //        // Send mail to employee code here

        //        BindPendingLeavesList();
        //    }
        //}
        //catch(Exception ex) { }
    }

    protected void lbtnReject_Click(object sender, EventArgs e)
    {
        LinkButton linkButton = (LinkButton)sender;
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                LeaveApplicationsTB applicationsTB = db.LeaveApplicationsTBs.Where(d => d.LeaveApplicationId == Convert.ToInt32(linkButton.CommandArgument)).FirstOrDefault();
                applicationsTB.ManagerStatus = "Reject";
                db.SubmitChanges();
                
                gen.ShowMessage(this.Page, "Leave Rejected..");

                // Send mail to employee code here

                BindPendingLeavesList();
            }
        }
        catch (Exception ex) { }
    }

    private void BindPendingLeavesList()
    {
        try {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                var data = (from d in db.OutDoorEntryTBs
                            join e in db.EmployeeTBs on d.EmployeeId equals e.EmployeeId
                            where e.IsActive == true && d.ManagerStatus == "Pending" && d.HrStatus=="Pending" && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.TenantId == Convert.ToString(Session["TenantId"])
                            select new
                            {
                                d.OddId,
                                d.ManagerStatus,
                                d.FromDate,
                                d.ToDate,
                                d.TravelPlace,
                                d.TravelReason,
                                d.Description,
                                EmpName = e.FName + " " + e.Lname,
                                e.EmployeeNo,
                                e.MachineID,
                                e.ReportingStatus,
                                e.EmployeeId
                            }).Distinct();
                if (Convert.ToInt32(Session["IsDeptHead"]) == 1)
                {
                    int manId = Convert.ToInt32(Session["EmpId"]);
                    data = data.Where(d => d.ReportingStatus == manId).Distinct();
                }
                gvPendingLeaves.DataSource = data;
                gvPendingLeaves.DataBind();
            }
        }
        catch(Exception ex) { }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                OutDoorEntryTB applicationsTB = db.OutDoorEntryTBs.Where(d => d.OddId == Convert.ToInt32(hfKey.Value)).FirstOrDefault();
                applicationsTB.ManagerStatus = "Approve";
                applicationsTB.ManagerRemark = txtReason.Text;
                db.SubmitChanges();


                // Send mail to employee code here
                MultiView1.ActiveViewIndex = 0;
                BindPendingLeavesList();
                gen.ShowMessage(this.Page, "OD Approved Successfully..");
            }
        }
        catch (Exception ex) { }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                OutDoorEntryTB applicationsTB = db.OutDoorEntryTBs.Where(d => d.OddId == Convert.ToInt32(hfKey.Value)).FirstOrDefault();
                applicationsTB.ManagerStatus = "Reject";
                applicationsTB.ManagerRemark = txtReason.Text;
                db.SubmitChanges();

                gen.ShowMessage(this.Page, "OD Rejected..");

                // Send mail to employee code here
                MultiView1.ActiveViewIndex = 0;
                BindPendingLeavesList();
            }
        }
        catch (Exception ex) { }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPendingLeavesList();
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
        }
        ddlCompany.Items.Insert(0, new ListItem("--Select--", "0"));

        ddlCompany.SelectedIndex = 1;
    }
}