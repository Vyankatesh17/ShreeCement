using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class od_report : System.Web.UI.Page
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
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindLeaveSummaryDetails();
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
    private void BindLeaveSummaryDetails()
    {
        bool AdminStatus = Session["UserType"].ToString() == "User" ? false : true;
        try
        {
            string query = @"SELECT        D.Description, D.FromTime, D.HrRemark, D.HrStatus, D.ManagerRemark, D.ManagerStatus, D.OddId, D.ToTime, D.TravelDate, D.TravelPlace, D.TravelReason, E.EmployeeNo, E.FName + ' ' + E.Lname AS EmpName
FROM            OutDoorEntryTB AS D INNER JOIN
                         EmployeeTB AS E ON D.EmployeeId = E.EmployeeId
WHERE        (1 = 1) AND D.TenantId='"+Session["TenantId"].ToString()+"'";

            if (!string.IsNullOrEmpty(txtEmployeeCode.Text))
            {
                query = query + " AND E.EmployeeNo='" + txtEmployeeCode.Text + "'";
            }
            if (!string.IsNullOrEmpty(txtEmpName.Text))
            {
                query = query + string.Format("AND (E.FName='{0}' OR E.Lname='{0}' OR E.FName+' '+E.Lname='{0}')", txtEmpName.Text);
            }
            if (ddlCompany.SelectedIndex > 0)
            {
                query = query + " AND E.CompanyId=" + ddlCompany.SelectedValue;
            }
            if (AdminStatus == false)
            {
                query = query + " AND E.EmployeeId=" + Convert.ToInt32(Session["EmpId"]);
            }
            DataTable data = gen.ReturnData(query);

            gvDataList.DataSource = data;
            gvDataList.DataBind();
        }
        catch (Exception ex) { }

    }

    protected void gvDataList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (gvDataList.Rows.Count > 0)
        {
            gvDataList.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            gvDataList.HeaderRow.TableSection =
            TableRowSection.TableHeader;
        }
    }
}