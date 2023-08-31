using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class rpt_shift_in_out_time : System.Web.UI.Page
{
    DataTable dtInfo;
    Genreal gen = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                fillcompany();
                //BindSimpleAttendance();
            }
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentList();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        DateTime from =string.IsNullOrEmpty(txtFromDate.Text)?DateTime.Now: Convert.ToDateTime(txtFromDate.Text);
        DateTime toDate = string.IsNullOrEmpty(txtToDate.Text) ? DateTime.Now :Convert.ToDateTime(txtToDate.Text);

        string query = string.Format(@"SELECT        E.EmployeeId, E.CompanyId, E.EmployeeNo, E.FName + ' ' + E.Lname AS EmpName, D.DeptID, D.DeptName, D1.DesigName, S.Intime AS ShiftStart, S.Outtime AS ShiftEnd,CONVERT(char(5),IsNULL (AL.InTime, 'A')) AS InTime,CONVERT(char(5),IsNULL (AL.OutTime, 'A')) AS OutTime ,
                                 CONVERT(NUMERIC(18, 2), ISNULL(AL.LateBy, 0) / 60 + AL.LateBy % 60 / 100.0) AS LateBy, CONVERT(NUMERIC(18, 2), ISNULL(AL.EarlyBy, 0) / 60 + AL.EarlyBy % 60 / 100.0) AS EarlyBy, CONVERT(date, AL.AttendanceDate) AS ADate
        FROM            AttendaceLogTB AS AL INNER JOIN
                                 EmployeeTB AS E ON AL.EmployeeId = E.EmployeeId INNER JOIN
                                 MasterDeptTB AS D ON E.DeptId = D.DeptID INNER JOIN
                                 MasterDesgTB AS D1 ON E.DesgId = D1.DesigID INNER JOIN
                                 MasterShiftTB AS S ON AL.ShiftId = S.ShiftID
        WHERE      E.IsActive=1 AND  (CONVERT(date, AL.AttendanceDate) BETWEEN CONVERT(date, '{0}') AND CONVERT(date, '{1}')) ORDER BY ADate,D.DeptId", from.ToString("yyyy-MM-dd"), toDate.ToString("yyyy-MM-dd"));
        //        string query = string.Format(@"SELECT        E.EmployeeId, E.CompanyId, E.EmployeeNo, E.FName + ' ' + E.Lname AS EmpName, D.DeptID, D.DeptName, D1.DesigName, S.Intime AS ShiftStart, S.Outtime AS ShiftEnd, AL.InTime, AL.OutTime, 
        //                         CONVERT(NUMERIC(18, 2), ISNULL(AL.LateBy, 0) / 60 + AL.LateBy % 60 / 100.0) AS LateBy, '' AS EarlyBy, CONVERT(date, AL.AttendanceDate) AS ADate
        //FROM            AttendaceLogTB AS AL INNER JOIN
        //                         EmployeeTB AS E ON AL.EmployeeId = E.EmployeeId INNER JOIN
        //                         MasterDeptTB AS D ON E.DeptId = D.DeptID INNER JOIN
        //                         MasterDesgTB AS D1 ON E.DesgId = D1.DesigID INNER JOIN
        //                         MasterShiftTB AS S ON AL.ShiftId = S.ShiftID
        //WHERE      E.IsActive=1 AND  (CONVERT(date, AL.AttendanceDate) BETWEEN CONVERT(date, '{0}') AND CONVERT(date, '{1}')) ORDER BY ADate,D.DeptId", from.ToString("yyyy-MM-dd"), toDate.ToString("yyyy-MM-dd"));

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
            dv1.RowFilter = "DeptId= '" + ddlDepartment.SelectedValue + "'";
            data = dv1.ToTable();
        }
        ViewState["dtInfo"] = data;

        DataTable distData = data.DefaultView.ToTable(true, "DeptName", "DeptId", "ADate");

        rptrTables.DataSource = distData;
        rptrTables.DataBind();
    }

    protected void rptrTables_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Header)
        {
            var x = e.Item.FindControl("lblHeader") as Label;
            x.Text = "Shiftwaise Attendance Report with (In/Out) Time";
        }
        else
        {

            //dtInfo.Rows.Clear();
            RepeaterItem item = e.Item;
            string lblDeptId = (item.FindControl("lblDeptId") as Label).Text;
            string lblDate = (item.FindControl("lblDate") as Label).Text;
            GridView gv = e.Item.FindControl("grdOrder") as GridView;
            Literal litFooter = e.Item.FindControl("litFooter") as Literal;
            int deptId = Convert.ToInt32(lblDeptId);
            DataTable data = (DataTable)ViewState["dtInfo"];
            DataView dv1 = data.DefaultView;
            string filter = string.Format(@"DeptId={0} and ADate='{1}'", lblDeptId, lblDate);
            //dv1.RowFilter = "DeptId= '" + lblDeptId + "'";
            dv1.RowFilter = filter;
            DataTable dt = dv1.ToTable();
            DataTable distData = dt.DefaultView.ToTable(true, "EmployeeNo", "EmpName", "DesigName", "ShiftStart", "ShiftEnd", "InTime","OutTime","LateBy", "EarlyBy");

            gv.DataSource = distData;
            gv.DataBind();

        }
    }

    protected void grdOrder_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {
            //e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = e.Row.Cells[2].Visible = e.Row.Cells[5].Visible = e.Row.Cells[6].Visible = e.Row.Cells[7].Visible = e.Row.Cells[8].Visible = e.Row.Cells[9].Visible = false;
            //e.Row.Cells[0].Visible = false;
        }
    }
    protected void ExportToExcel(object sender, EventArgs e)
    {
        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename=shift_wise_in_out_time_report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        rptrTables.RenderControl(hw);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
    #region  Binding Methods
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
        }
        catch (Exception ex)
        {
            throw ex;
        }
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
    #endregion
}