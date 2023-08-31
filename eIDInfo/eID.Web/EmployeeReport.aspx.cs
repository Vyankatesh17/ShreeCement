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


public partial class EmployeeReport : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();

    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnPrint);
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {

                tbldisp.Visible = false;
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                tbldisp.Visible = true;
                BindAllEmp();
                fillcompany();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }

    }


    /// <summary>
    /// Amit Shinde
    /// </summary>

    #region bind methods
    private void fillcompany()
    {
        try
        {
            var data = (from dt in HR.CompanyInfoTBs
                        where dt.Status == 0
                        select dt).OrderBy(dt => dt.CompanyName);
            if (data != null && data.Count() > 0)
            {

                ddlCompanyList.DataSource = data;
                ddlCompanyList.DataTextField = "CompanyName";
                ddlCompanyList.DataValueField = "CompanyId";
                ddlCompanyList.DataBind();
                ddlCompanyList.Items.Insert(0, "--Select--");
            }
            else
            {
                ddldept.Items.Clear();
                ddlemp.Items.Clear();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void BindDepartment(string p)
    {
        try
        {
            if (ddlCompanyList.SelectedIndex > 0)
            {
                var data = (from dt in HR.CompanyInfoTBs
                            join dep in HR.MasterDeptTBs on dt.CompanyId equals dep.CompanyId
                            where dt.CompanyName == p
                            select dep).OrderBy(dt => dt.DeptName);

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
            if (ddlCompanyList.SelectedIndex > 0)
            {
                if (ddldept.SelectedIndex != 0)
                {
                    var data = (from dtReportHead in HR.EmployeeTBs
                                //join dep in HR.MasterDeptTBs on dtReportHead.CompanyId equals dep.CompanyId
                                where dtReportHead.RelivingStatus == null && dtReportHead.CompanyId == Convert.ToInt32(ddlCompanyList.SelectedValue) && dtReportHead.DeptId == Convert.ToInt32(ddldept.SelectedValue)
                                select new
                                {
                                    dtReportHead.EmployeeId,
                                    Name = dtReportHead.FName + ' ' + dtReportHead.MName + ' ' + dtReportHead.Lname
                                }).OrderBy(dt => dt.Name);


                    if (data != null && data.Count() > 0)
                    {
                        ddlemp.DataSource = data;
                        ddlemp.DataTextField = "Name";
                        ddlemp.DataValueField = "EmployeeId";
                        ddlemp.DataBind();
                        ddlemp.Items.Insert(0, "--Select--");
                    }
                    else
                    {
                        ddlemp.Items.Clear();
                    }
                }
                else
                {
                    ddlemp.Items.Clear();

                }
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
            string Query = "select EM.EmployeeId,EM.CompanyId,EM.DeptID,EM.FName+ +EM.LName as EmpName,EMailID,EM.ContactNo,em.DOJ,MC.CompanyName,MD.DeptName,dt.DesigName,EM.PanNo,EM.PassportNo, case when EM.RelivingStatus is null then 'Working-Employee' else 'Relieved-Employee' end RelivingStatus  from EmployeeTB EM  Left outer join CompanyInfoTB MC ON MC.CompanyId=EM.CompanyId left outer join MasterDeptTB MD ON MD.DeptID = EM.DeptId left outer join MasterDesgTB DT ON DT.DesigID=EM.DesgId WHERE (1=1)";
                

            if (ddlCompanyList.SelectedIndex > 0)
            {
                Query =Query+ " AND EM.CompanyId='"+Convert.ToInt32(ddlCompanyList.SelectedValue)+"'";
            }
            if (ddldept.SelectedIndex > 0)
            {
                Query = Query + " AND EM.DeptId='"+Convert.ToInt32(ddldept.SelectedValue)+"'";
            }
            if (ddlemp.SelectedIndex > 0)
            {
                Query = Query + " AND EM.EmployeeId='"+Convert.ToInt32(ddlemp.SelectedValue)+"'";
            }

            if(!string.IsNullOrEmpty(txtEmpCode.Text))
            {
                Query += " AND EM.EmployeeNo='" + txtEmpCode.Text + "'";
            }
            if(!string.IsNullOrEmpty(ddlStatus.SelectedValue))
            {
                if (ddlStatus.SelectedIndex == 1)
                    Query += " AND EM.RelivingStatus=0";
            }

            DataSet dsemp = g.ReturnData1(Query);

            if(dsemp.Tables[0].Rows.Count > 0)
            {
                grd_Emp.DataSource = dsemp.Tables[0];
                grd_Emp.DataBind();
            }
            else
            {
                grd_Emp.DataSource = dsemp.Tables[0];
                grd_Emp.DataBind();
            }
            lblcnt.Text = dsemp.Tables[0].Rows.Count.ToString();
        }
        catch(Exception ex)
        {
            throw ex;
        }

        //var ds = (HR.SPEmployeeReportDetails("0", "0", "0", 1, "0")).ToList();

        //if (txtFirstNameSearch.Text != "")
        //{
        //    ds = (HR.SPEmployeeReportDetails("0", txtFirstNameSearch.Text, "0", 2, "0")).ToList();
        //}
        //if (txtempidsearch.Text != "")
        //{
        //    ds = (HR.SPEmployeeReportDetails(txtempidsearch.Text, "0", "0", 5, "0")).ToList();
        //}

        //if (txtDOJsearch.Text != "")
        //{
        //    ds = (HR.SPEmployeeReportDetails("0", "0", "0", 3, txtDOJsearch.Text)).ToList();
        //}

        //if (txtdeptsearch.Text != "")
        //{
        //    ds = (HR.SPEmployeeReportDetails("0", "0", txtdeptsearch.Text, 4, "0")).ToList();
        //}

        //// for first name and emp id 
        //if (txtFirstNameSearch.Text != "" && txtempidsearch.Text != "")
        //{
        //    ds = (HR.SPEmployeeReportDetails(txtempidsearch.Text, txtFirstNameSearch.Text, "0", 6, "0")).ToList();
        //}

        //// for first name and date of joining 
        //if (txtFirstNameSearch.Text != "" && txtDOJsearch.Text != "")
        //{
        //    ds = (HR.SPEmployeeReportDetails("", txtFirstNameSearch.Text, "0", 7, txtDOJsearch.Text)).ToList();
        //}

        //// for first name and department
        //if (txtFirstNameSearch.Text != "" && txtdeptsearch.Text != "")
        //{
        //    ds = (HR.SPEmployeeReportDetails("", txtFirstNameSearch.Text, txtdeptsearch.Text, 8, "0")).ToList();
        //}

        //// for first name and emp id , date of joining 
        //if (txtFirstNameSearch.Text != "" && txtempidsearch.Text != "" && txtDOJsearch.Text != "")
        //{
        //    ds = (HR.SPEmployeeReportDetails(txtempidsearch.Text, txtFirstNameSearch.Text, "0", 12, txtDOJsearch.Text)).ToList();
        //}

        //// for first name and emp id , department
        //if (txtFirstNameSearch.Text != "" && txtempidsearch.Text != "" && txtdeptsearch.Text != "")
        //{
        //    ds = (HR.SPEmployeeReportDetails("", txtFirstNameSearch.Text, txtdeptsearch.Text, 13, txtDOJsearch.Text)).ToList();
        //}

        //// for first name and emp id , department, date of joining 
        //if (txtFirstNameSearch.Text != "" && txtempidsearch.Text != "" && txtdeptsearch.Text != "" && txtDOJsearch.Text != "")
        //{
        //    ds = (HR.SPEmployeeReportDetails(txtempidsearch.Text, txtFirstNameSearch.Text, txtdeptsearch.Text, 14, txtDOJsearch.Text)).ToList();
        //}

        //// for emp id and department
        //if (txtempidsearch.Text != "" && txtdeptsearch.Text != "")
        //{
        //    ds = (HR.SPEmployeeReportDetails(txtempidsearch.Text, "0", txtdeptsearch.Text, 10, "")).ToList();
        //}

        //// for date of joining and emp id 

        //if (txtempidsearch.Text != "" && txtDOJsearch.Text != "")
        //{
        //    ds = (HR.SPEmployeeReportDetails(txtempidsearch.Text, "0", "0", 9, txtDOJsearch.Text)).ToList();
        //}
        //if (ds.Count() > 0)
        //{
        //    grd_Emp.DataSource = ds;
        //    grd_Emp.DataBind();
        //}
        //else
        //{
        //    grd_Emp.DataSource = null;
        //    grd_Emp.DataBind();
        //}

    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        tbldisp.Visible = true;
        BindAllEmp();
    }

    protected void grd_Emp_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grd_Dept_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {

    }
    protected void btnPrint_Click1(object sender, EventArgs e)
    {
        //grd_Emp.PagerSettings.Visible = false;
        //grd_Emp.DataBind();
        //StringWriter sw = new StringWriter();
        //HtmlTextWriter hw = new HtmlTextWriter(sw);
        //grd_Emp.RenderControl(hw);
        //string gridHTML = sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");
        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //sb.Append("<script type = 'text/javascript'>");
        //sb.Append("window.onload = new function(){");
        //sb.Append("var printWin = window.open('', '', 'left=0");
        //sb.Append(",top=0,width=1000,height=600,status=0');");
        //sb.Append("printWin.document.write(\"");
        //sb.Append(gridHTML);
        //sb.Append("\");");
        //sb.Append("printWin.document.close();");
        //sb.Append("printWin.focus();");
        //sb.Append("printWin.print();");
        //sb.Append("printWin.close();};");
        //sb.Append("</script>");
        //ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
        //grd_Emp.PagerSettings.Visible = true;
        //grd_Emp.DataBind();
        BindAllEmp();
    }
    protected void btnExpPDF_Click(object sender, EventArgs e)
    {


        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                //To Export all pages

                try
                {
                    grd_Emp.AllowPaging = false;
                    BindAllEmp();

                    // tbldisp.RenderControl(hw);
                    //StringReader sr = new StringReader(sw.ToString());
                    //Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 0f, 10f);
                    //HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    //PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    //pdfDoc.Open();
                    //htmlparser.Parse(sr);
                    //pdfDoc.Close();

                    //Response.ContentType = "application/pdf";
                    //Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
                    //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //Response.Write(pdfDoc);
                    //Response.End();

                    tbldisp.Width = "80%";
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=EmployeeReport.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    //StringWriter sw = new StringWriter();
                    //HtmlTextWriter hw = new HtmlTextWriter(sw);
                    tbldisp.RenderControl(hw);
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 0f, 10f);
                    //  Document pdfDoc = new Document(PageSize.A4, 10f, 10f, 100f, 0f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();
                    Response.Write(pdfDoc);
                    Response.End();


                }
                catch (Exception ex)
                {

                    throw;
                }

            }
        }
    }
    protected void btnExpExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();

        Response.ContentType = "application/ms-excel";

        Response.Charset = "";

        Page.EnableViewState = false;

        Response.AddHeader("Content-Disposition", "attachment;filename=Employees.xls");

        StringWriter strwtr = new StringWriter();

        HtmlTextWriter hw = new HtmlTextWriter(strwtr);
        //turn off paging

        grd_Emp.AllowPaging = false;
        BindAllEmp();
        // ..bindGrid();



        grd_Emp.RenderControl(hw);

        Response.Write(strwtr.ToString());

        Response.End();


    }
    protected void PrintAllPages(object sender, EventArgs e)
    {
        grd_Emp.AllowPaging = false;
        BindAllEmp();
        grd_Emp.DataBind();
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        grd_Emp.RenderControl(hw);
        string gridHTML = sw.ToString().Replace("\"", "'")
            .Replace(System.Environment.NewLine, "");
        StringBuilder sb = new StringBuilder();
        sb.Append("<script type = 'text/javascript'>");
        sb.Append("window.onload = new function(){");
        sb.Append("var printWin = window.open('', '', 'left=0");
        sb.Append(",top=0,width=1000,height=600,status=0');");
        sb.Append("printWin.document.write(\"");
        sb.Append(gridHTML);
        sb.Append("\");");
        sb.Append("printWin.document.close();");
        sb.Append("printWin.focus();");
        sb.Append("printWin.print();");
        sb.Append("printWin.close();};");
        sb.Append("</script>");
        ClientScript.RegisterStartupScript(this.GetType(), "GridPrint", sb.ToString());
        grd_Emp.AllowPaging = true;
        grd_Emp.DataBind();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
    protected void grd_Emp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {

        grd_Emp.PageIndex = e.NewPageIndex;
        BindAllEmp();

        grd_Emp.DataBind();
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetCountries(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();

        List<string> Name = (from d in HR.EmployeeTBs
                             where d.RelivingStatus == null &&
                                 (d.FName + " " + d.MName + " " + d.Lname).StartsWith(prefixText)
                             select d.FName + " " + d.MName + " " + d.Lname).Distinct().ToList();
        return Name;

    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetEMPID(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
        List<string> Name = (from d in HR.EmployeeTBs
                             where d.RelivingStatus == null && (d.EmployeeId.ToString()).StartsWith(prefixText)
                             select d.EmployeeId.ToString()).Distinct().ToList();
        return Name;

    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetDept(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();

        List<string> Name = (from d in HR.MasterDeptTBs
                             where d.Status == 0 &&
                                 (d.DeptName).StartsWith(prefixText)
                             select d.DeptName).Distinct().ToList();
        return Name;

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
}