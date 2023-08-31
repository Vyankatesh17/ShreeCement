using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Monthly_Details_Report2 : System.Web.UI.Page
{
    Genreal gen = new Genreal();

   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                fillcompany();
            }
        }       
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
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentList(); BindEmployeeList();
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
            ddlEmployee.Items.Clear();
            int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
            int dId = ddlDepartment.SelectedIndex > 0 ? Convert.ToInt32(ddlDepartment.SelectedValue) : 0;
            var data = (from dt in db.EmployeeTBs
                        where (dt.RelivingStatus == null || dt.RelivingStatus == 0) && dt.IsActive == true
                        && dt.TenantId == Convert.ToString(Session["TenantId"]) && dt.CompanyId == cId && dt.DeptId == dId
                        select new { name = dt.FName + ' ' + dt.Lname, dt.EmployeeId }).OrderBy(d => d.name);
            if (data != null && data.Count() > 0)
            {
                ddlEmployee.DataSource = data;
                ddlEmployee.DataTextField = "name";
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
        Response.AddHeader("content-disposition", "attachment;filename=Monthly_Details_Report2.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);


        DataTable data = (DataTable)ViewState["dtInfo"];

        GridView gv = new GridView();
        gv.DataSource = data;
        gv.DataBind();

        //gv.GridLines = GridLines.Both;
        //gv.HeaderStyle.Font.Bold = true;
        //GridView1.RenderControl(htmltextwrtter);
        gv.RenderControl(hw);
        Response.Output.Write(sw.ToString());
        Response.Flush();
        Response.End();
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        try
        {
            DateTime FromDate = Convert.ToDateTime(txtFromDate.Text);
            string usertype = Session["UserType"].ToString();
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                db.CommandTimeout = 15 * 60;

                DateTime from = Convert.ToDateTime(txtFromDate.Text);
                DateTime toDate = Convert.ToDateTime(txtToDate.Text);
                string query = "";
                lblHeader.Text = "Details Report for month of " + txtFromDate.Text + " - " + txtToDate.Text;
                if (usertype == "Admin")
                {
                     query = string.Format(@"SELECT        E.CompanyId,E.DeptId,A.EmployeeId,CAST(A.AttendanceDate AS date) As AttDate,
                                    E.EmployeeNo As CODE, 
                                    E.Grade As TYPE,
                                    E.FName + ' ' + E.Lname AS NAME,                               
                                CONVERT(varchar(5), A.InTime) As INTIME,
                                CONVERT(varchar(5), A.OutTime) As OUTTIME,
                                CONVERT(varchar(5),DATEADD(MINUTE, convert(int,A.OverTime), '19000101'), 108) AS OT,
                                CONVERT(varchar(5), DATEADD(MINUTE, 60*Duration,0),108) AS TOTAL,
                                CONVERT(varchar(5),DATEADD(MINUTE, convert(int,A.LateBy), '19000101'), 108) AS LATE,
                                CONVERT(varchar, A.Status) AS REMARK,
                                CONVERT(varchar(5), S.InTime) As SIntime,
                                CONVERT(varchar(5), S.OutTime) As SOuttime
                                                        FROM   AttendaceLogTB AS A INNER JOIN
                                                    EmployeeTB AS E ON A.EmployeeId = E.EmployeeId INNER JOIN
                                                    MasterDeptTB AS D1 ON E.DeptId = D1.DeptID INNER JOIN
                                                    MasterDesgTB AS D2 ON E.DesgId = D2.DesigID INNER JOIN
                                                    MAsterShiftTB S ON A.ShiftId=S.ShiftId
                          WHERE     E.IsActive=1 AND ((A.AttendanceDate BETWEEN '{0}' AND '{1}'))
                            Order By A.EmployeeId, A.AttendanceDate Asc  ", txtFromDate.Text, txtToDate.Text);
                }
                else
                {
                    if (ddlCompany.SelectedIndex > 0)
                    {
                         query = string.Format(@"SELECT        E.CompanyId,E.DeptId,A.EmployeeId,CAST(A.AttendanceDate AS date) As AttDate,
                                    E.EmployeeNo As CODE, 
                                    E.Grade As TYPE,
                                    E.FName + ' ' + E.Lname AS NAME,                               
                                CONVERT(varchar(5), A.InTime) As INTIME,
                                CONVERT(varchar(5), A.OutTime) As OUTTIME,
                                CONVERT(varchar(5),DATEADD(MINUTE, convert(int,A.OverTime), '19000101'), 108) AS OT,
                                CONVERT(varchar(5), DATEADD(MINUTE, 60*Duration,0),108) AS TOTAL,
                                CONVERT(varchar(5),DATEADD(MINUTE, convert(int,A.LateBy), '19000101'), 108) AS LATE,
                                CONVERT(varchar, A.Status) AS REMARK,
                                CONVERT(varchar(5), S.InTime) As SIntime,
                                CONVERT(varchar(5), S.OutTime) As SOuttime
                                                        FROM   AttendaceLogTB AS A INNER JOIN
                                                    EmployeeTB AS E ON A.EmployeeId = E.EmployeeId INNER JOIN
                                                    MasterDeptTB AS D1 ON E.DeptId = D1.DeptID INNER JOIN
                                                    MasterDesgTB AS D2 ON E.DesgId = D2.DesigID INNER JOIN
                                                    MAsterShiftTB S ON A.ShiftId=S.ShiftId
                          WHERE     E.IsActive=1 AND ((A.AttendanceDate BETWEEN '{0}' AND '{1}'))
                            Order By A.EmployeeId, A.AttendanceDate Asc  ", txtFromDate.Text, txtToDate.Text);
                    }
                    else
                    {
                        gen.ShowMessage(this.Page, "Please select Company!..");
                    }
                }

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
                if (ddlEmployee.SelectedIndex > 0)
                {
                    DataView dv1 = data.DefaultView;
                    dv1.RowFilter = "EmployeeId= '" + ddlEmployee.SelectedValue + "'";
                    data = dv1.ToTable();
                }
                if (ddlEmpType.SelectedIndex > 0)
                {
                    DataView dv1 = data.DefaultView;
                    dv1.RowFilter = "TYPE= '" + ddlEmpType.SelectedValue + "'";
                    data = dv1.ToTable();
                }
                if (Session["UserType"].ToString() == "User")
                {
                    DataView dv1 = data.DefaultView;
                    dv1.RowFilter = "EmployeeId= '" + Convert.ToInt32(Session["EmpId"]) + "'";
                    data = dv1.ToTable();
                }



                DataTable dt = new DataTable();

                dt.Columns.Add("AttDate", typeof(string));
                dt.Columns.Add("CODE", typeof(string));
                dt.Columns.Add("TYPE", typeof(string));
                dt.Columns.Add("NAME", typeof(string));
                dt.Columns.Add("INTIME", typeof(string));
                dt.Columns.Add("OUTTIME", typeof(string));
                dt.Columns.Add("OT", typeof(string));
                dt.Columns.Add("TOTAL", typeof(string));
                dt.Columns.Add("LATE", typeof(string));
                dt.Columns.Add("REMARK", typeof(string));

                foreach (var row in data.Select())
                {
                    DataRow dr = dt.NewRow();
                    if (dt.Rows.Count > 0)
                    {
                        DataRow[] dtrow = dt.Select("CODE=" + "'" + row[4].ToString() + "'");
                        int n = dtrow.Length;

                        if (n == 0)
                        {
                            dr[1] = row[4];
                            dr[2] = row[5];
                            dr[3] = row[6];
                        }
                        else
                        {
                            dr[1] = "";
                            dr[2] = "";
                            dr[3] = "";
                        }
                    }
                    else
                    {
                        dr[1] = row[4];
                        dr[2] = row[5];
                        dr[3] = row[6];

                    }

                    string atdate = row[3].ToString();
                    string result = atdate.Remove(10);
                    dr[0] = result;
                    dr[4] = row[7];
                    dr[5] = row[8];
                    dr[7] = row[10];
                    string OT = row[9].ToString();
                   // Double Ottime = Convert.ToDouble(OT);
                    if(OT != "")
                    {
                        TimeSpan Ottime = TimeSpan.Parse(OT);
                        TimeSpan DefaultOT = TimeSpan.Parse("02:00");
                        if(Ottime >= DefaultOT)
                        {
                            Random r = new Random();
                            int sec = r.Next(0, 5);
                            string Default = "02:0" + sec;
                            TimeSpan Outtime = TimeSpan.Parse(row[14].ToString());
                            Outtime += TimeSpan.Parse(Default);
                            dr[5] = Outtime.Hours + ":" + Outtime.Minutes;
                            dr[6] = Default;

                            TimeSpan intime = TimeSpan.Parse(row[7].ToString());
                            TimeSpan totaltime = Outtime.Subtract(intime);
                            dr[7] = totaltime.Hours + ":" + totaltime.Minutes; 
                        }
                        else
                        {
                            dr[5] = row[8];
                            dr[6] = row[9];
                            dr[7] = row[10];
                        }
                    }                   
                    else
                    {
                        dr[6] = "00:00";
                    }                    
                    
                    dr[8] = row[11];
                    dr[9] = row[12];
                    dt.Rows.Add(dr);
                }

                ViewState["dtInfo"] = dt;


                grdOrder.DataSource = dt;
                grdOrder.DataBind();

                for (int rowIndex = grdOrder.Rows.Count - 2; rowIndex >= 0; rowIndex--)
                {
                    GridViewRow row = grdOrder.Rows[rowIndex];
                    GridViewRow previousRow = grdOrder.Rows[rowIndex + 1];

                    for (int i = 0; i < 4; i++)
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

        if (e.Row.RowType == DataControlRowType.Header || e.Row.RowType == DataControlRowType.DataRow)
        {

            //e.Row.Cells[0].Visible = e.Row.Cells[1].Visible = e.Row.Cells[2].Visible =  false;
            //e.Row.Cells[0].Visible = false;
        }
    }

}