using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Text;



public partial class SalarySlipReport : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();

    Genreal g = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnPrintCurrent);
        if (!IsPostBack)
        {
            fillYear();
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            tbl_t1.Visible = false;
            ddlYear.Items.FindByValue(DateTime.Now.Year.ToString()).Selected = true;
            ddlMonth.Items.FindByValue(DateTime.Now.Month.ToString()).Selected = true;
          
        }
        ////else
        ////{
        ////    Response.Redirect("Login.aspx");
        ////}
    }
    private void fillYear()
    {
        int i = int.Parse(DateTime.Now.AddYears(0).Date.Year.ToString());
        for (int j = 0; j <= 50; j++)
        {

            ddlYear.Items.Add(i.ToString());
            i++;

        }

    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        tbl_t1.Visible = true;
        BindAllEmp();
    }
    
    public void BindAllEmp()
    {

        DataTable ds ;

        ds = g.ReturnData("SELECT  distinct   t0.FName +' ' + t0.MName +' '+ t0.Lname AS Name, t0.FName AS Expr1, t0.Lname, t0.EmployeeId, t1.SalaryAccountNo, t1.Month, t1.Year, t1.WorkingDays, t1.Netpaybledays, t1.PFAccountNo, t1.GrossSalary, t1.NetSlary,  t1.BankName, t1.SalaryMode, t1.GrossSalary, t1.NetSlary FROM EmployeeTB AS t0 left OUTER JOIN SalaryProcessTB AS t1 ON t0.EmployeeId = t1.EmployeeID ");

       /****   dataset for  Search      ****/  

        if (txtFirstNameSearch.Text != "" && txtempidsearch.Text != "" && ddlMonth.SelectedIndex > 0 && ddlYear.SelectedIndex > 0)
        {
             ds = g.ReturnData("SELECT  distinct  t0.FName +' ' + t0.MName +' '+ t0.Lname AS Name, t0.FName AS Expr1, t0.Lname, t0.EmployeeId, t1.SalaryAccountNo, t1.Month, t1.Year, t1.WorkingDays, t1.Netpaybledays, t1.PFAccountNo, t1.GrossSalary, t1.NetSlary,  t1.BankName, t1.SalaryMode, t1.GrossSalary, t1.NetSlary FROM EmployeeTB AS t0 left OUTER JOIN SalaryProcessTB AS t1 ON t0.EmployeeId = t1.EmployeeID where t0.FName +' ' + t0.MName +' '+ t0.Lname='" + txtFirstNameSearch.Text + "' and t0.EmployeeId='" + txtempidsearch.Text + "'    and  t1.Year='" + ddlYear.Text + "' and t1.Month ='" + ddlMonth.SelectedValue + "' ");
        }
        else if (txtFirstNameSearch.Text != "" && txtempidsearch.Text != "" && ddlMonth.SelectedIndex > 0 && ddlYear.SelectedIndex == 0)
        {
            ds = g.ReturnData("SELECT  distinct  t0.FName +' ' + t0.MName +' '+ t0.Lname AS Name, t0.FName AS Expr1, t0.Lname, t0.EmployeeId, t1.SalaryAccountNo, t1.Month, t1.Year, t1.WorkingDays, t1.Netpaybledays, t1.PFAccountNo, t1.GrossSalary, t1.NetSlary,  t1.BankName, t1.SalaryMode, t1.GrossSalary, t1.NetSlary FROM EmployeeTB AS t0 left OUTER JOIN SalaryProcessTB AS t1 ON t0.EmployeeId = t1.EmployeeID where t0.FName +' ' + t0.MName +' '+ t0.Lname='" + txtFirstNameSearch.Text + "' and t0.EmployeeId='" + txtempidsearch.Text + "'  and t1.Month ='" + ddlMonth.SelectedValue + "' ");
        }
        else if (txtFirstNameSearch.Text != "" && txtempidsearch.Text != "" && ddlYear.SelectedIndex > 0 && ddlMonth.SelectedIndex == 0)
        {
            ds = g.ReturnData("SELECT  distinct  t0.FName +' ' + t0.MName +' '+ t0.Lname AS Name, t0.FName AS Expr1, t0.Lname, t0.EmployeeId, t1.SalaryAccountNo, t1.Month, t1.Year, t1.WorkingDays, t1.Netpaybledays, t1.PFAccountNo, t1.GrossSalary, t1.NetSlary,  t1.BankName, t1.SalaryMode, t1.GrossSalary, t1.NetSlary FROM EmployeeTB AS t0 left OUTER JOIN SalaryProcessTB AS t1 ON t0.EmployeeId = t1.EmployeeID where t0.FName +' ' + t0.MName +' '+ t0.Lname='" + txtFirstNameSearch.Text + "' and t0.EmployeeId='" + txtempidsearch.Text + "'  and t1.Year='" + ddlYear.Text + "'  ");
        }
        else if (ddlMonth.SelectedIndex > 0 && txtempidsearch.Text != "" && ddlYear.SelectedIndex > 0 && txtFirstNameSearch.Text == "")
        {
            ds = g.ReturnData("SELECT  distinct  t0.FName +' ' + t0.MName +' '+ t0.Lname AS Name, t0.FName AS Expr1, t0.Lname, t0.EmployeeId, t1.SalaryAccountNo, t1.Month, t1.Year, t1.WorkingDays, t1.Netpaybledays, t1.PFAccountNo, t1.GrossSalary, t1.NetSlary,  t1.BankName, t1.SalaryMode, t1.GrossSalary, t1.NetSlary FROM EmployeeTB AS t0 left OUTER JOIN SalaryProcessTB AS t1 ON t0.EmployeeId = t1.EmployeeID where t0.EmployeeId='" + txtempidsearch.Text + "'  and t1.Year='" + ddlYear.Text + "'  and t1.Month ='" + ddlMonth.SelectedValue + "' ");
        }
        else if (txtFirstNameSearch.Text != "" && txtempidsearch.Text != "" && ddlMonth.SelectedIndex == 0 && ddlYear.SelectedIndex == 0)
        {
            ds = g.ReturnData("SELECT  distinct  t0.FName +' ' + t0.MName +' '+ t0.Lname AS Name, t0.FName AS Expr1, t0.Lname, t0.EmployeeId, t1.SalaryAccountNo, t1.Month, t1.Year, t1.WorkingDays, t1.Netpaybledays, t1.PFAccountNo, t1.GrossSalary, t1.NetSlary,  t1.BankName, t1.SalaryMode, t1.GrossSalary, t1.NetSlary FROM EmployeeTB AS t0 left OUTER JOIN SalaryProcessTB AS t1 ON t0.EmployeeId = t1.EmployeeID where t0.FName +' ' + t0.MName +' '+ t0.Lname='" + txtFirstNameSearch.Text + "' and t0.EmployeeId='" + txtempidsearch.Text + "' ");
        }
        else if (txtFirstNameSearch.Text != "" && ddlMonth.SelectedIndex > 0 && txtempidsearch.Text == "" && ddlYear.SelectedIndex == 0)
        {
            ds = g.ReturnData("SELECT  distinct  t0.FName +' ' + t0.MName +' '+ t0.Lname AS Name, t0.FName AS Expr1, t0.Lname, t0.EmployeeId, t1.SalaryAccountNo, t1.Month, t1.Year, t1.WorkingDays, t1.Netpaybledays, t1.PFAccountNo, t1.GrossSalary, t1.NetSlary,  t1.BankName, t1.SalaryMode, t1.GrossSalary, t1.NetSlary FROM EmployeeTB AS t0 left OUTER JOIN SalaryProcessTB AS t1 ON t0.EmployeeId = t1.EmployeeID where t0.FName +' ' + t0.MName +' '+ t0.Lname='" + txtFirstNameSearch.Text + "' and t1.Month ='" + ddlMonth.SelectedValue + "' ");
        }
        else if (txtFirstNameSearch.Text != "" && ddlYear.SelectedIndex > 0 && txtempidsearch.Text == "" && ddlMonth.SelectedIndex == 0)
        {
            ds = g.ReturnData("SELECT  distinct  t0.FName +' ' + t0.MName +' '+ t0.Lname AS Name, t0.FName AS Expr1, t0.Lname, t0.EmployeeId, t1.SalaryAccountNo, t1.Month, t1.Year, t1.WorkingDays, t1.Netpaybledays, t1.PFAccountNo, t1.GrossSalary, t1.NetSlary,  t1.BankName, t1.SalaryMode, t1.GrossSalary, t1.NetSlary FROM EmployeeTB AS t0 left OUTER JOIN SalaryProcessTB AS t1 ON t0.EmployeeId = t1.EmployeeID where t0.FName +' ' + t0.MName +' '+ t0.Lname='" + txtFirstNameSearch.Text + "' and t1.Year='" + ddlYear.Text + "' ");
        }
        else if (txtempidsearch.Text != "" && ddlYear.SelectedIndex > 0 && txtFirstNameSearch.Text == "" && ddlMonth.SelectedIndex == 0)
        {
            ds = g.ReturnData("SELECT  distinct  t0.FName +' ' + t0.MName +' '+ t0.Lname AS Name, t0.FName AS Expr1, t0.Lname, t0.EmployeeId, t1.SalaryAccountNo, t1.Month, t1.Year, t1.WorkingDays, t1.Netpaybledays, t1.PFAccountNo, t1.GrossSalary, t1.NetSlary,  t1.BankName, t1.SalaryMode, t1.GrossSalary, t1.NetSlary FROM EmployeeTB AS t0 left OUTER JOIN SalaryProcessTB AS t1 ON t0.EmployeeId = t1.EmployeeID where t0.EmployeeId='" + txtempidsearch.Text + "'  and t1.Year='" + ddlYear.Text + "'  ");
        }
        else if (txtempidsearch.Text != "" && ddlMonth.SelectedIndex > 0 && txtFirstNameSearch.Text == "" && ddlYear.SelectedIndex == 0)
        {
            ds = g.ReturnData("SELECT  distinct  t0.FName +' ' + t0.MName +' '+ t0.Lname AS Name, t0.FName AS Expr1, t0.Lname, t0.EmployeeId, t1.SalaryAccountNo, t1.Month, t1.Year, t1.WorkingDays, t1.Netpaybledays, t1.PFAccountNo, t1.GrossSalary, t1.NetSlary,  t1.BankName, t1.SalaryMode, t1.GrossSalary, t1.NetSlary FROM EmployeeTB AS t0 left OUTER JOIN SalaryProcessTB AS t1 ON t0.EmployeeId = t1.EmployeeID where t0.EmployeeId='" + txtempidsearch.Text + "'  and t1.Month ='" + ddlMonth.SelectedValue + "'  ");
        }
        if (ddlMonth.SelectedIndex > 0 && ddlYear.SelectedIndex > 0 && txtempidsearch.Text == "" && txtFirstNameSearch.Text == "")
        {
            ds = g.ReturnData("SELECT  distinct  t0.FName +' ' + t0.MName +' '+ t0.Lname AS Name, t0.FName AS Expr1, t0.Lname, t0.EmployeeId, t1.SalaryAccountNo, t1.Month, t1.Year, t1.WorkingDays, t1.Netpaybledays, t1.PFAccountNo, t1.GrossSalary, t1.NetSlary,  t1.BankName, t1.SalaryMode, t1.GrossSalary, t1.NetSlary FROM EmployeeTB AS t0 left OUTER JOIN SalaryProcessTB AS t1 ON t0.EmployeeId = t1.EmployeeID where   t1.Year='" + ddlYear.Text + "' and t1.Month ='" + ddlMonth.SelectedValue + "' ");
        }
        else if (txtFirstNameSearch.Text != ""  && txtempidsearch.Text == "" && ddlMonth.SelectedIndex == 0 && ddlYear.SelectedIndex == 0)
        {
            ds = g.ReturnData("SELECT  distinct  t0.FName +' ' + t0.MName +' '+ t0.Lname AS Name, t0.FName AS Expr1, t0.Lname, t0.EmployeeId, t1.SalaryAccountNo, t1.Month, t1.Year, t1.WorkingDays, t1.Netpaybledays, t1.PFAccountNo, t1.GrossSalary, t1.NetSlary,  t1.BankName, t1.SalaryMode, t1.GrossSalary, t1.NetSlary FROM EmployeeTB AS t0 left OUTER JOIN SalaryProcessTB AS t1 ON t0.EmployeeId = t1.EmployeeID where t0.FName +' ' + t0.MName +' '+ t0.Lname='" + txtFirstNameSearch.Text + "' ");
        }
        else if (txtempidsearch.Text != "" && txtFirstNameSearch.Text == "" && ddlMonth.SelectedIndex == 0 && ddlYear.SelectedIndex == 0 )
        {
            ds = g.ReturnData("SELECT  distinct t0.FName +' ' + t0.MName +' '+ t0.Lname AS Name, t0.FName AS Expr1, t0.Lname, t0.EmployeeId, t1.SalaryAccountNo, t1.Month, t1.Year, t1.WorkingDays, t1.Netpaybledays, t1.PFAccountNo,t1.GrossSalary, t1.NetSlary,  t1.BankName, t1.SalaryMode, t1.GrossSalary, t1.NetSlary FROM EmployeeTB AS t0 left OUTER JOIN SalaryProcessTB AS t1 ON t0.EmployeeId = t1.EmployeeID where t0.EmployeeId='" + txtempidsearch.Text + "' ");

        }
        else if (ddlMonth.SelectedIndex > 0 && txtempidsearch.Text == "" && txtFirstNameSearch.Text == "" &&  ddlYear.SelectedIndex == 0)
        {
            ds = g.ReturnData("SELECT  distinct  t0.FName +' ' + t0.MName +' '+ t0.Lname AS Name, t0.FName AS Expr1, t0.Lname, t0.EmployeeId, t1.SalaryAccountNo, t1.Month, t1.Year, t1.WorkingDays, t1.Netpaybledays, t1.PFAccountNo, t1.GrossSalary, t1.NetSlary,  t1.BankName, t1.SalaryMode, t1.GrossSalary, t1.NetSlary FROM EmployeeTB AS t0 left OUTER JOIN SalaryProcessTB AS t1 ON t0.EmployeeId = t1.EmployeeID where t1.Month ='" + ddlMonth.SelectedValue + "' ");

        }
        else if (ddlYear.SelectedIndex > 0 && txtempidsearch.Text == "" && txtFirstNameSearch.Text == "" && ddlMonth.SelectedIndex == 0)
        {
            ds = g.ReturnData("SELECT  distinct  t0.FName +' ' + t0.MName +' '+ t0.Lname AS Name, t0.FName AS Expr1, t0.Lname, t0.EmployeeId, t1.SalaryAccountNo, t1.Month, t1.Year, t1.WorkingDays, t1.Netpaybledays, t1.PFAccountNo, t1.GrossSalary, t1.NetSlary,  t1.BankName, t1.SalaryMode, t1.GrossSalary, t1.NetSlary FROM EmployeeTB AS t0 left OUTER JOIN SalaryProcessTB AS t1 ON t0.EmployeeId = t1.EmployeeID where t1.Year='" + ddlYear.Text + "' ");

        }
        else if (ddlYear.SelectedIndex > 0 && txtFirstNameSearch.Text != "" && ddlMonth.SelectedIndex >0 && txtempidsearch.Text == "")
        {
            ds = g.ReturnData("SELECT  distinct  t0.FName +' ' + t0.MName +' '+ t0.Lname AS Name, t0.FName AS Expr1, t0.Lname, t0.EmployeeId, t1.SalaryAccountNo, t1.Month, t1.Year, t1.WorkingDays, t1.Netpaybledays, t1.PFAccountNo, t1.GrossSalary, t1.NetSlary,  t1.BankName, t1.SalaryMode, t1.GrossSalary, t1.NetSlary FROM EmployeeTB AS t0 left OUTER JOIN SalaryProcessTB AS t1 ON t0.EmployeeId = t1.EmployeeID where t1.Year='" + ddlYear.Text + "' and t1.Month ='" + ddlMonth.SelectedValue + "' and t0.FName +' ' + t0.MName +' '+ t0.Lname='" + txtFirstNameSearch.Text + "' ");

        }
        if (ds.Rows.Count > 0)
        {
            grd_Emp.DataSource = ds;
            grd_Emp.DataBind();
        }
        else
        {
            grd_Emp.DataSource = null;
            grd_Emp.DataBind();
        }
        lblcnt.Text = grd_Emp.Rows.Count.ToString();
    }
   

    protected void PrintCurrentPage(object sender, EventArgs e)
    {
        try
        {
            if (grd_Emp.Rows.Count > 0)
            {
                grd_Emp.PagerSettings.Visible = false;
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
                grd_Emp.PagerSettings.Visible = true;
                //grd_Emp.DataBind();
            }
            else
            {
                g.ShowMessage(this.Page, "Data Is Not Present");
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    protected void btnPrint_Click1(object sender, EventArgs e)
    {

    }
    
    protected void grd_Emp_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnExpPDF_Click(object sender, EventArgs e)
    {
        try
        {
            if (grd_Emp.Rows.Count > 0)
            {
                using (StringWriter sw = new StringWriter())
                {
                    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                    {
                        //To Export all pages
                        grd_Emp.AllowPaging = false;
                        BindAllEmp();
                        //grd_Emp.DataBind();

                        grd_Emp.HeaderRow.Style.Add("width", "15%");
                        grd_Emp.HeaderRow.Style.Add("font-size", "10px");
                        grd_Emp.Style.Add("text-decoration", "none");
                        grd_Emp.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                        grd_Emp.Style.Add("font-size", "8px");

                        tbl_t1.RenderControl(hw);
                        StringReader sr = new StringReader(sw.ToString());
                        Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 0f, 10f);
                        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                        pdfDoc.Open();
                        htmlparser.Parse(sr);
                        pdfDoc.Close();

                        Response.ContentType = "application/pdf";
                        Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
                        Response.Cache.SetCacheability(HttpCacheability.NoCache);
                        Response.Write(pdfDoc);
                        Response.End();
                    }
                }
            }
            else
            {
                g.ShowMessage(this.Page, "Data Is Not Present");
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }


    }
    protected void PrintAllPages(object sender, EventArgs e)
    {
        try
        {
            if (grd_Emp.Rows.Count > 0)
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
            else
            {
                g.ShowMessage(this.Page, "Data Is Not Present");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    protected void btnExpExcel_Click(object sender, EventArgs e)
    {
        try
        {
            if (grd_Emp.Rows.Count > 0)
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
            else
            {
                g.ShowMessage(this.Page, "Data Is Not Present");
            }
        }
        catch(Exception ex)
        {
            throw ex;
        }

 


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
                             where
                                 (d.FName + " " + d.MName + " " + d.Lname).StartsWith(prefixText)
                             select d.FName + " " + d.MName + " " + d.Lname).Distinct().ToList();
         return Name;
       
    }
    protected void btnPrintCurrent_Click(object sender, EventArgs e)
    {
        Response.Redirect("SalarySlipReport.aspx");
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("SalarySlipReport.aspx");

    }
}