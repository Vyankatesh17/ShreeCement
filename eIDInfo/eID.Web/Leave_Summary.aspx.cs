using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Leave_Summary : System.Web.UI.Page
{
    Genreal gen = new Genreal();
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
                fillcompany();
               
            }
        }        
        BindJqFunctions();
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
            ddlCompany.Items.Insert(0, new ListItem("--Select--", "0"));
            ddlCompany.SelectedIndex = 1;
            BindDepartmentList();            
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentList();        
    }

    protected void ddlDepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployeeList();
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

    private void BindEmployeeList()
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            DateTime todaysdt = DateTime.Now;
            var month = todaysdt.Month;
            ddlEmployee.Items.Clear();

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

    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=Subjectwise_Report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        DataTable data = (DataTable)ViewState["dtInfo"];
        GridView gv = new GridView();
        gv.DataSource = data;
        gv.DataBind();       
        gv.RenderControl(hw);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    protected void btnImport_Click(object sender, EventArgs e)
    {
        string skey = Session["TenantId"].ToString().Replace("+", "key_plus");
        Response.Redirect("Import_Leave_Balance.aspx?key=" + skey);

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {           
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                db.CommandTimeout = 15 * 60;             
                
                string query = "";

                query = string.Format(@"select EMP.EmployeeId,EMP.EmployeeNo,EMP.FName + ' '+ EMP.Lname as Empname,LA.LeaveAllocateID, LA.LeaveID,ML.LeaveName, DE.DeptID, LA.CompanyId, LA.TotalAllocatedLeaves, LA.PendingLeaves, LA.AllocationStatus from LeaveAllocationTB LA
inner Join EmployeeTB Emp on Emp.EmployeeId = LA.EmployeeID
inner Join MasterDeptTB DE on DE.DeptID = Emp.DeptId
inner Join masterLeavesTB ML on ML.LeaveID = LA.LeaveID order by Emp.EmployeeId Asc, Emp.Grade1 Asc, Emp.Grade_Sequence_No Asc");

                DataTable data = gen.ReturnData(query);

                if (ddlCompany.SelectedIndex > 0)
                {
                    DataView dv1 = data.DefaultView;
                    dv1.RowFilter = "CompanyId= '" + ddlCompany.SelectedValue + "'";
                    data = dv1.ToTable();
                }
                if (ddlDepartment.SelectedIndex > 0)
                {
                    DataView dv1 = data.DefaultView;
                    dv1.RowFilter = "DeptID= '" + ddlDepartment.SelectedValue + "'";
                    data = dv1.ToTable();
                }
                if (ddlEmployee.SelectedIndex > 0)
                {
                    DataView dv1 = data.DefaultView;
                    dv1.RowFilter = "EmployeeId= '" + ddlEmployee.SelectedValue + "'";
                    data = dv1.ToTable();
                }



                string Empquery = "";
                Empquery = string.Format(@"select Distinct(EmployeeId) from LeaveAllocationTB");
                DataTable EmpIddata = gen.ReturnData(Empquery);



                var LeaveData = db.masterLeavesTBs.Where(a => a.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)).ToList();


                DataTable dt = new DataTable();
                dt.Columns.Add("SR.NO.", typeof(string));
                dt.Columns.Add("CODE", typeof(string));
                dt.Columns.Add("NAME", typeof(string));

                dt.Columns[0].MaxLength = 30;
                dt.Columns[1].MaxLength = 150;

                foreach (var item in LeaveData)
                {
                    dt.Columns.Add(item.LeaveTypeSName, typeof(string));
                }

                int srno = 1;

                foreach (var dataitem in EmpIddata.Select())
                {
                    DataRow dr = dt.NewRow();
                    dr[0] = srno;
                    dr[1] = dataitem[1];
                    int Empid = Convert.ToInt32(dataitem[0]);
                    dr[2] = dataitem[2];

                    int i = 2;
                    foreach (var item in LeaveData)
                    {
                        i++;
                        if (item.LeaveName == dataitem[5].ToString())
                        {
                            dr[i] = dataitem[9];
                        }
                    }

                    dt.Rows.Add(dr);
                    srno++;
                }

                ViewState["dtInfo"] = dt;
                grdOrder.DataSource = dt;
                grdOrder.DataBind();

                for (int rowIndex = grdOrder.Rows.Count - 2; rowIndex >= 0; rowIndex--)
                {
                    GridViewRow row = grdOrder.Rows[rowIndex];
                    GridViewRow previousRow = grdOrder.Rows[rowIndex + 1];

                    for (int i = 0; i < 3; i++)
                    {
                        if (row.Cells[i].Text == previousRow.Cells[i].Text)
                        {
                            row.Cells[i].RowSpan = previousRow.Cells[i].RowSpan < 2 ? 2 :
                                              previousRow.Cells[i].RowSpan + 1;
                            previousRow.Cells[i].Visible = false;

                        }
                    }
                }





            }
        }
        catch (Exception ex)
        {

        }
    }


    protected void grdOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[0].Text == "TOTAL")
            {
                e.Row.Font.Bold = true;
                e.Row.Font.Size = 11;
            }
        }
    }



}