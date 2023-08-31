using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Salary_Process_Report : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    private void BindJqFunctions()
    {
        //jqFunctions
        if (gvAttendance.Rows.Count > 0)
        {
            //gvAttendance.UseAccessibleHeader = true;
            gvAttendance.HeaderRow.TableSection = TableRowSection.TableHeader;

        }
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
        {
            if (!IsPostBack)
            {
                txtYear.Attributes.Add("min", DateTime.Now.AddYears(-5).Year.ToString());
                txtYear.Attributes.Add("max", DateTime.Now.Year.ToString());
                txtYear.Text = DateTime.Now.Year.ToString();
                ddlMonth.SelectedIndex = DateTime.Now.Month == 1 ? 12 : DateTime.Now.Month - 1;
                BindCompanyList();                                

                ddlEmployee.SelectedValue = Convert.ToString(Session["UserId"]);
                if (Convert.ToString(Session["UserId"]).Equals("User"))
                {
                    ddlEmployee.Enabled = false;
                }
                else { ddlEmployee.Enabled = true; }


            }
            BindJqFunctions();
        }
    }

    private void BindCompanyList()
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
        ddlCompany.Items.Insert(0, new ListItem("--All--", "0"));

        ddlCompany.SelectedIndex = 1;
        BindDepartmentList();
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentList();        
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
    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployeeList();
    }

    private void BindEmployeeList()
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            DateTime todaysdt = DateTime.Now;
            var todaysmonth = todaysdt.Month;
            var todaysyear = todaysdt.Year;
            ddlEmployee.Items.Clear();

            DateTime todate = todaysdt;
            var tomonth = todate.Month;
            var toyear = todate.Year;

            int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
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
                        var relivingyear = relivingdate.Year;

                        if (relivingmonth == todaysmonth && relivingyear == todaysyear)
                        {
                            emp.EmployeeId = item.EmployeeId;
                            emp.FName = item.name;
                            emplist.Add(emp);
                        }
                        else if (todate <= relivingdate || tomonth == relivingmonth && toyear == relivingyear)
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
            try
            {
                int year = Convert.ToInt32(txtYear.Text);
                int month = ddlMonth.SelectedIndex;                
                using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
                {
                    var getQuery = string.Format(@"SELECT ES.ProcessId, ES.EmployeeId,E.Grade1 As Grade,E.Grade_Sequence_No, ES.DeptId,D1.DeptName, ES.DesigId, ES.ProcessMonth, ES.ProcessYear, ES.TotalDays, ES.WorkingDays, ES.PresentDays, ES.AbsentDays, ES.PaidLeaves, ES.E_BasicSalary, ES.E_HRA, ES.E_DA, ES.E_SpecialAllowance, 
                  ES.E_CityAllowance, ES.E_MedicalAllowance, ES.E_ConveyanceAllowance, ES.E_WashingAllowance, ES.E_AttendanceAllowance, ES.E_SpacialAllowance1, ES.E_SpacialAllowance2, ES.E_OthersAllowance, ES.IsPFApplicable, 
                  ES.D_ProvidentFundDeduct, ES.IsESIApplicable, ES.D_ESIDeduct, ES.IsPTApplicable, ES.D_ProffessionalTaxDeduct, ES.D_ServantInsuranceDeduct, ES.D_IncomeTaxDeduct, ES.D_PostalDepositDeduct, ES.D_EducationDonationsDeduct, 
                  ES.D_HomeRentDeduct, ES.D_ServantMilkDeduct, ES.D_AmritEMIDeduct, ES.D_AmritCreditDeduct, ES.D_AmritThevDeduct, ES.D_ShortLoanDeduct, ES.D_ElectricityBillDeduct, ES.D_WaterBillDeduct, ES.D_WorkerWelfareDeduct, 
                  ES.D_LabourWelfareDeduct, ES.D_AdTasalmat, ES.D_BankRecovery, ES.D_BankOfIndia, ES.D_WaranaBank, ES.D_WaranaMahila, ES.D_Other1Deduct, ES.D_Other2Deduct, ES.D_Other3Deduct, ES.D_Other4Deduct, ES.D_AdInsurance, 
                  ES.CostToCompany, ES.GrossSalary, ES.NetSalary, ES.Stamp, ES.EmployeerPFContribution, ES.IsActive, ES.CreatedBy, ES.CreatedDate, ES.IsProcessed, E.DesgId, E.EmployeeNo, E.FName + ' ' + E.Lname AS EmpName, E.Gender, 
                  D1.DeptName, D2.DesigName, DATENAME(MONTH, DATEADD(MONTH, ES.ProcessMonth, '2000-12-01')) AS MonthName
FROM     EmpSalaryProcessTB AS ES INNER JOIN
                  EmployeeTB AS E ON E.EmployeeId = ES.EmployeeId INNER JOIN
                  MasterDesgTB AS D2 ON E.DesgId = D2.DesigID INNER JOIN
                  MasterDeptTB AS D1 ON E.DeptId = D1.DeptID
WHERE  (ES.IsProcessed = 1) AND (ES.IsActive = 1) AND (ES.ProcessMonth = '{0}') AND (ES.ProcessYear = '{1}')", month, year);

                    if (ddlEmployee.SelectedIndex > 0)
                    {
                        getQuery += " AND ES.EmployeeId=" + ddlEmployee.SelectedValue;
                    }                   
                    if (ddlDepartment.SelectedIndex > 0)
                    {
                        getQuery += " AND ES.DeptId=" + ddlDepartment.SelectedValue;
                    }


                    getQuery += " ORDER BY ES.ProcessId";
                    DataTable data = gen.ReturnData(getQuery);

                    data.DefaultView.Sort = "DeptName ASC, Grade Asc, Grade_Sequence_No ASC";

                    gvAttendance.DataSource = data;
                    gvAttendance.DataBind();

                   

                   
                }
            }
            catch (Exception ex)
            {
               
            }
        }
        catch (Exception ex) {  }
    }

    protected void gvAttendance_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (gvAttendance.Rows.Count > 0)
        {
            gvAttendance.UseAccessibleHeader = true;
            //adds <thead> and <tbody> elements
            gvAttendance.HeaderRow.TableSection =
            TableRowSection.TableHeader;


        }
    }

    protected void gvAttendance_DataBound(object sender, EventArgs e)
    {
        GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
        TableHeaderCell cell = new TableHeaderCell();
        cell.Text = "Shree Warana Sah. Dudh Utpadak Sangh.";
        cell.ColumnSpan = 4;
        row.Controls.Add(cell);

        cell = new TableHeaderCell();
        cell.ColumnSpan = 2;
        cell.Text = "Dept:" + ddlDepartment.SelectedItem;
        row.Controls.Add(cell);

        row.BackColor = ColorTranslator.FromHtml("#3AC0F2");
        gvAttendance.HeaderRow.Parent.Controls.AddAt(0, row);
    }

    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Attendance_Report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        Panel1.RenderControl(hw);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

















}