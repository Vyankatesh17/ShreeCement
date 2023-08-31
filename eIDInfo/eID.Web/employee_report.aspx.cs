using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Text;
using System.Data;


public partial class employee_report : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    private void BindJqFunctions()
    {
        //jqFunctions
        if (grd_Emp.Rows.Count > 0)
        {
            grd_Emp.UseAccessibleHeader = true;
            grd_Emp.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {              
                fillcompany();
                BindAllEmp();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
        BindJqFunctions();
    }


    /// <summary>
    /// Amit Shinde
    /// </summary>

    #region bind methods
    private void fillcompany()
    {
        try
        {
            ddlCompanyList.Items.Clear();
            List<CompanyInfoTB> data = SPCommon.DDLCompanyBind(Session["UserType"].ToString(), Session["TenantId"].ToString(), Session["CompanyID"].ToString());
            if (data != null && data.Count() > 0)
            {
                ddlCompanyList.DataSource = data;
                ddlCompanyList.DataTextField = "CompanyName";
                ddlCompanyList.DataValueField = "CompanyId";
                ddlCompanyList.DataBind();
            }
            ddlCompanyList.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindDepartment(string Companyname)
    {
        try
        {
            if (ddlCompanyList.SelectedIndex > 0)
            {
                //var data = (from dt in HR.CompanyInfoTBs
                //            join dep in HR.MasterDeptTBs on dt.CompanyId equals dep.CompanyId
                //            where dt.CompanyName == p
                //            select dep).OrderBy(dt => dt.DeptName);

                List<MasterDeptTB> data = SPCommon.DDLDepartmentBind(Companyname);              

                if (data != null && data.Count() > 0)
                {
                    ddldept.DataSource = data;
                    ddldept.DataTextField = "DeptName";
                    ddldept.DataValueField = "DeptID";
                    ddldept.DataBind();
                    ddldept.Items.Insert(0, "--Select--");
                }
                else
                {
                    ddldept.DataSource = null;
                    ddldept.DataBind();

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
            int cId = ddlCompanyList.SelectedIndex > 0 ? Convert.ToInt32(ddlCompanyList.SelectedValue) : 0;
            int dId = ddldept.SelectedIndex > 0 ? Convert.ToInt32(ddldept.SelectedValue) : 0;

            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                var data = (from dt in db.EmployeeTBs
                            where (dt.RelivingStatus == null || dt.RelivingStatus == 0) && dt.IsActive == true
                            && dt.TenantId == Convert.ToString(Session["TenantId"]) && dt.CompanyId == cId&& dt.DeptId==dId
                            select new { name = dt.FName + ' ' + dt.Lname, dt.EmployeeId }).OrderBy(d => d.name);
                if (data != null && data.Count() > 0)
                {
                    ddlemp.DataSource = data;
                    ddlemp.DataTextField = "name";
                    ddlemp.DataValueField = "EmployeeId";
                    ddlemp.DataBind();
                }
                ddlemp.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--SELECT--", "0"));
            }
            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    #endregion


    public void BindAllEmp()
    {
        try
        {
            int companyid = Convert.ToInt32(Session["CompanyID"]);

            string Query =string.Format(@"SELECT        E.EmployeeId, E.CompanyId, E.DeptId, E.FName +' '+ E.Lname AS EmpName,E.EmployeeNo, E.MachineID, E.Email, E.ContactNo, E.DOJ, C.CompanyName, D.DeptName, D1.DesigName, E.PanNo, E.PassportNo, CASE WHEN E.RelivingStatus IS NULL 
                         THEN 'Working' WHEN e.RelivingStatus = 0 THEN 'Working' ELSE 'Relieved' END AS RelivingStatus, E.Grade
FROM            EmployeeTB AS E LEFT OUTER JOIN
                         CompanyInfoTB AS C ON C.CompanyId = E.CompanyId LEFT OUTER JOIN
                         MasterDeptTB AS D ON D.DeptID = E.DeptId LEFT OUTER JOIN
                         MasterDesgTB AS D1 ON D1.DesigID = E.DesgId
WHERE        (E.IsActive=1)  AND E.TenantId='" + Convert.ToString(Session["TenantId"])+"'");
                

            if (ddlCompanyList.SelectedIndex > 0)
            {
                Query =Query+ " AND E.CompanyId='"+Convert.ToInt32(ddlCompanyList.SelectedValue)+"'";
            }
            if (ddldept.SelectedIndex > 0)
            {
                Query = Query + " AND E.DeptId='"+Convert.ToInt32(ddldept.SelectedValue)+"'";
            }
            if (ddlemp.SelectedIndex > 0)
            {
                Query = Query + " AND E.EmployeeId='"+Convert.ToInt32(ddlemp.SelectedValue)+"'";
            }

            if(!string.IsNullOrEmpty(txtEmpCode.Text))
            {
                Query += " AND E.EmployeeNo='" + txtEmpCode.Text + "'";
            }

            if (ddlEmpType.SelectedIndex > 0)
            {
                Query = Query + " AND E.Grade='" + ddlEmpType.SelectedValue + "'";
            }

            if (ddlStatus.SelectedIndex>0)
            {
                int stat = ddlStatus.SelectedIndex == 1 ? 1 : 0;
                Query += " AND E.RelivingStatus=" + stat;
            }

            if (Session["UserType"].ToString() == "LocationAdmin")
            {
                Query = Query + " AND E.CompanyId='" + companyid + "'";
            }

            DataTable dsemp = g.ReturnData(Query);

            dsemp.DefaultView.Sort = "DeptName ASC, EmpName ASC";


           
            grd_Emp.DataSource = dsemp;
                grd_Emp.DataBind();
            //}
          
        }
        catch(Exception ex)
        {
            throw ex;
        }
        
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        
        BindAllEmp();
    }
    
   

    protected void ddlCompanyList_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompanyList.SelectedIndex > 0)
        {
            BindDepartment(ddlCompanyList.SelectedItem.Text);
            //FillEmployeeList();
            BindAllEmp();

        }
        else
        {
            ddldept.Items.Clear();
            ddlemp.Items.Clear();
            BindAllEmp();
        }
    }
    protected void ddldept_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldept.SelectedIndex > 0)
        {
            FillEmployeeList();
            BindAllEmp();
        }
        else
        {
            ddlemp.Items.Clear();
            BindAllEmp();
        }
    }
    protected void ddldesign_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAllEmp();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        ddlCompanyList.SelectedIndex = 0;
        ddldept.Items.Clear();
        ddlemp.Items.Clear();
        BindAllEmp();
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
}