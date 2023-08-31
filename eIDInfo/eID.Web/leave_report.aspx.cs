using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class leave_report : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    private void BindJqFunctions()
    {
        //jqFunctions
        if (gvDataList.Rows.Count > 0)
        {
            gvDataList.UseAccessibleHeader = true;
            gvDataList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
       
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
        {
            if (!IsPostBack)
            {
                fillcompany();
                BindLeaveNamesList();
                BindLeaveSummaryDetails();


                if (Session["UserType"].ToString() != "User")
                {
                    company.Visible = true;
                    employee.Visible = true;
                }
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
        BindJqFunctions();
    }
    
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindLeaveSummaryDetails();
    }
    private void BindLeaveNamesList()
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
            var LeaveTypeData = (from d in db.masterLeavesTBs
                                 join k in db.LeaveAllocationTBs on d.LeaveID equals k.LeaveID
                                 where d.Status == 1 && d.TenantId==Convert.ToString(Session["TenantId"])&&d.CompanyId==cId
                                 select new
                                 {
                                     d.LeaveID,
                                     d.LeaveName
                                 }).Distinct();

            List<ListItem> list = new List<ListItem>();
            list.Add(new ListItem("--All--", "0"));
            foreach (var item in LeaveTypeData)
            {

                list.Add(new ListItem(item.LeaveName, item.LeaveID.ToString()));
            }

            list.Add(new ListItem("Loss of Pay", (LeaveTypeData.Count() + 1).ToString()));
            ddlLeaveName.DataSource = list;
            ddlLeaveName.DataTextField = "Text";
            ddlLeaveName.DataValueField = "Value";
            ddlLeaveName.DataBind();
        }
    }
    private void BindLeaveSummaryDetails()
    {

        try
        {
            string query = @"SELECT        L.LeaveTypeId, L.ApplicationDate, L.LeaveReason, L.LeaveStartDate, L.LeaveEndDate, L.StartHalf, L.EndHalf, L.Duration, L.ManagerStatus, L.HrStatus, L.IsLossofPay, L.ManRemark, L.HrRemark, L.ApproveDate, M.LeaveName,
                          E.EmployeeNo, E.FName+' '+E.Lname AS EmpName
FROM            LeaveApplicationsTB AS L INNER JOIN
                         EmployeeTB AS E ON L.EmployeeId = E.EmployeeId LEFT OUTER JOIN
                         masterLeavesTB AS M ON L.LeaveTypeId = M.LeaveID
						 WHERE E.IsActive=1 AND E.TenantId='"+Convert.ToString(Session["TenantId"])+"'";

            if (!string.IsNullOrEmpty(txtEmployeeCode.Text))
            {
                query = query + " AND E.EmployeeNo='" + txtEmployeeCode.Text + "'";
            }
            if (!string.IsNullOrEmpty(txtEmpName.Text))
            {
                query = query + string.Format("AND (E.FName='{0}' OR E.Lname='{0}' OR E.FName+' '+E.Lname='{0}')", txtEmpName.Text);
            }
            if (!string.IsNullOrEmpty(txtEmpName.Text))
            {
                query = query + "M.LeaveName='" + ddlLeaveName.SelectedItem.Text + "'";
            }
            if (ddlCompany.SelectedIndex>0)
            {
                query = query + "M.CompanyId='" + ddlCompany.SelectedValue + "'";
            }
            if (Session["UserType"].ToString() == "User")
            {
                query = query + "E.EmployeeId='" + Convert.ToInt32(Session["EmpId"]) + "'";
            }
            DataTable data = gen.ReturnData(query);

            gvDataList.DataSource = data;
            gvDataList.DataBind();
        }
        catch(Exception ex) { }
      
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
            ddlCompany.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void gvDataList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (gvDataList.Rows.Count > 0)
        {
            gvDataList.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            gvDataList.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindLeaveNamesList();
    }
}