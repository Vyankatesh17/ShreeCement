using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WorkFromHome_Approve_HR : System.Web.UI.Page
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
            if (!IsPostBack)
            {
                fillcompany();
                BindPendingWorkfromhomeList();
            }
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
                var data = (from d in db.WorkFromHomeTBs
                            join emp in db.EmployeeTBs on d.EmployeeId equals emp.EmployeeId
                            where d.Work_From_Home_Id == Convert.ToInt32(linkButton.CommandArgument)
                            select new
                            {
                                d.Work_From_Home_Id,
                                d.Manager_Status,
                                d.FromDate,
                                d.ToDate,                               
                                d.Reason,
                                d.Description,
                                EmpName = emp.FName + " " + emp.Lname,
                                d.Manager_Remark
                            }).FirstOrDefault();

                lblEmpName.Text = data.EmpName;
                lblFromdate.Text = data.FromDate.Value.Date.ToString("MM/dd/yyyy");
                lblTodate.Text = data.ToDate.Value.Date.ToString("MM/dd/yyyy");
                lblReason.Text = data.Reason;
                lblManagerComment.Text = data.Manager_Remark;
            }
        }
        catch (Exception ex) { throw ex; }
    }

    private void BindPendingWorkfromhomeList()
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                var data = (from d in db.WorkFromHomeTBs
                            join e in db.EmployeeTBs on d.EmployeeId equals e.EmployeeId
                            where e.IsActive == true && d.Manager_Status == "Approve" && d.HR_Status == "Pending" && e.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.TenantId == Convert.ToString(Session["TenantId"])
                            select new
                            {
                                d.Work_From_Home_Id,
                                d.Manager_Status,
                                d.FromDate,
                                d.ToDate,                                
                                d.Reason,
                                d.Description,
                                EmpName = e.FName + " " + e.Lname,
                                e.EmployeeNo,
                                e.MachineID
                            }).Distinct();

                gvPendingLeaves.DataSource = data;
                gvPendingLeaves.DataBind();
            }
        }
        catch (Exception ex) { throw ex; }
    }

    protected void btnApprove_Click(object sender, EventArgs e)
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                WorkFromHomeTB applicationsTB = db.WorkFromHomeTBs.Where(d => d.Work_From_Home_Id == Convert.ToInt32(hfKey.Value)).FirstOrDefault();
                applicationsTB.HR_Status = "Approve";
                applicationsTB.HR_Remark = txtReason.Text;
                db.SubmitChanges();

                //#region Send mail to department head
                //SendMail(applicationsTB.EmployeeId.Value, applicationsTB.FromDate.Value.ToShortDateString(), applicationsTB.ToDate.Value.ToShortDateString(), applicationsTB.FromTime.Value.ToString(), applicationsTB.ToTime.Value.ToString(), applicationsTB.TravelReason, applicationsTB.TravelPlace, "Approve");
                //#endregion
                // Send mail to employee code here
                MultiView1.ActiveViewIndex = 0;
                BindPendingWorkfromhomeList();
                gen.ShowMessage(this.Page, "Work From Home Approved Successfully..");
            }
        }
        catch (Exception ex) { throw ex; }
    }

    protected void btnReject_Click(object sender, EventArgs e)
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                WorkFromHomeTB applicationsTB = db.WorkFromHomeTBs.Where(d => d.Work_From_Home_Id == Convert.ToInt32(hfKey.Value)).FirstOrDefault();
                applicationsTB.HR_Status = "Reject";
                applicationsTB.HR_Remark = txtReason.Text;
                db.SubmitChanges();
                //#region Send mail to department head
                //SendMail(applicationsTB.EmployeeId.Value, applicationsTB.FromDate.Value.ToShortDateString(), applicationsTB.ToDate.Value.ToShortDateString(), applicationsTB.FromTime.Value.ToString(), applicationsTB.ToTime.Value.ToString(), applicationsTB.TravelReason, applicationsTB.TravelPlace, "Rejected");
                //#endregion
                gen.ShowMessage(this.Page, "Work From Home Rejected..");

                // Send mail to employee code here
                MultiView1.ActiveViewIndex = 0;
                BindPendingWorkfromhomeList();
            }
        }
        catch (Exception ex) { throw ex; }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindPendingWorkfromhomeList();
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
    }












}