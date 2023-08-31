using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Daily_Detailed_Summary_Report1 : System.Web.UI.Page
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
        Response.AddHeader("content-disposition", "attachment;filename=Monthly_Details_Report1.xls");
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

                lblHeader.Text = "Daily Detailed Summary Report for month of " + txtFromDate.Text + " - " + txtToDate.Text;
                string query = "";

                if (ddlCompany.SelectedIndex > 0)
                {
                    query = string.Format(@"SELECT E.CompanyId,E.DeptId,A.EmployeeId,E.FName + ' ' + E.Lname AS EMPNAME,
E.EmployeeNo As EMPNO, 
CAST(A.AttendanceDate AS date) As Date,                                                                               
                                    CONVERT(varchar(5), A.InTime) As FIRSTIN,
                                    CONVERT(varchar(5), A.OutTime) As LASTOUT,                                    
                                    CONVERT(varchar(5), DATEADD(MINUTE, 60*Duration,0),108) AS WorkingHRS,
									A.Actual_Working_Hours As DHRS                                   
                                    
                                                            FROM   AttendaceLogTB AS A INNER JOIN
                                                        EmployeeTB AS E ON A.EmployeeId = E.EmployeeId INNER JOIN
                                                        MasterDeptTB AS D1 ON E.DeptId = D1.DeptID INNER JOIN
                                                        MasterDesgTB AS D2 ON E.DesgId = D2.DesigID  
                              WHERE     E.IsActive=1 AND ((A.AttendanceDate BETWEEN '{0}' AND '{1}'))
                                Order By A.EmployeeId, A.AttendanceDate Asc", txtFromDate.Text, txtToDate.Text);
                }
                else
                {
                    gen.ShowMessage(this.Page, "Please select Company!..");
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
                if (Session["UserType"].ToString() == "User")
                {
                    DataView dv1 = data.DefaultView;
                    dv1.RowFilter = "EmployeeId= '" + Convert.ToInt32(Session["EmpId"]) + "'";
                    data = dv1.ToTable();
                }



                DataTable dt = new DataTable();
                dt.Columns.Add("EmpName", typeof(string));
                dt.Columns.Add("EmpNo", typeof(string));
                dt.Columns.Add("Date", typeof(string));
                dt.Columns.Add("First In", typeof(string));
                dt.Columns.Add("Last Out", typeof(string));
                dt.Columns.Add("Working Hrs", typeof(string));
                dt.Columns.Add("D Hrs", typeof(string));
                dt.Columns.Add("Total Hrs", typeof(string));
                dt.Columns.Add("Push Time With Device Location Name", typeof(string));
                foreach (var row in data.Select())
                {
                    DataRow dr = dt.NewRow();
                    if (dt.Rows.Count > 0)
                    {
                        DataRow[] dtrow = dt.Select("EmpNo=" + "'" + row[4].ToString() + "'");
                        int n = dtrow.Length;

                        if (n == 0)
                        {
                            dr[0] = row[3];
                            dr[1] = row[4];
                        }
                        else
                        {
                            dr[0] = "";
                            dr[1] = "";
                        }
                    }
                    else
                    {
                        dr[0] = row[3];
                        dr[1] = row[4];

                    }

                    string atdate = row[5].ToString();
                    string result = atdate.Remove(10);
                    dr[2] = result;
                    dr[3] = row[6];
                    dr[4] = row[7];
                    dr[5] = row[8];
                    dr[6] = "00:00";
                    dr[7] = "00:00";

                    TimeSpan working = TimeSpan.Parse("00:00");
                    TimeSpan Dhr = TimeSpan.Parse("00:00");

                    if (row[8].ToString() != "")
                    {
                        working = TimeSpan.Parse(row[8].ToString());
                    }

                    int Empid = Convert.ToInt32(row[2]);
                    DateTime Adate = Convert.ToDateTime(row[5]);
                    var devicelogdata = db.DeviceLogsTBs.Where(a => a.EmpID == Empid && a.ADate == Adate).ToList();

                    string punchlist = "";
                    if (devicelogdata.Count > 0)
                    {
                        TimeSpan deductionhr = TimeSpan.Parse("00:00");
                        foreach (var item in devicelogdata)
                        {
                            var devicedata = db.DevicesTBs.Where(a => a.DeviceAccountId == item.DeviceAccountId.ToString()).FirstOrDefault();
                            punchlist += item.ATime + " " + item.PunchStatus + "-" + devicedata.DeviceName + ", ";

                            //punchlist += item.ATime + " " + item.PunchStatus;
                            TimeSpan outt = TimeSpan.Parse("00:00");
                            TimeSpan intime = TimeSpan.Parse("00:00");
                            if (item.PunchStatus == "Out")
                            {
                                outt = TimeSpan.Parse(item.ATime.ToString());
                            }

                            if (outt != TimeSpan.Parse("00:00") && item.PunchStatus == "In")
                            {
                                intime = TimeSpan.Parse(item.ATime.ToString());
                            }

                            if (outt > intime && intime != TimeSpan.Parse("00:00"))
                            {
                                deductionhr = outt.Subtract(intime);
                            }
                        }

                        //TimeSpan testtime = TimeSpan.Parse(row[9].ToString());
                        Dhr = TimeSpan.Parse((deductionhr.Hours + ":" + deductionhr.Minutes + ":00").ToString());
                        dr[7] = deductionhr.Hours + ":" + deductionhr.Minutes;
                    }

                    if (working != TimeSpan.Parse("00:00") && Dhr != TimeSpan.Parse("00:00"))
                    {
                        TimeSpan tt = working.Subtract(Dhr);
                        dr[6] = tt.Hours + ":" + tt.Minutes;
                    }

                    //if(row[6].ToString() != "" && row[7].ToString() != "")
                    //{
                    //string punchstring = row[6].ToString() + " IN " + row[7].ToString() + " OUT ";
                    dr[8] = punchlist;
                    //}                   

                    dt.Rows.Add(dr);
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


}