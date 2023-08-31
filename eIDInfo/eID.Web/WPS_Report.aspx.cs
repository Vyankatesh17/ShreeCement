using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.Threading;


/// <summary>
/// Amit Shinde
/// </summary>
public partial class WPS_Report : System.Web.UI.Page
{
    Genreal g = new Genreal();
    HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();


    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnPrint);
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                // fillYear();


                DateTime dtcurrenttimes = g.GetCurrentDateTime();
                int dt = DateTime.Now.Month;
                ddlmonth.SelectedIndex = dt;

                DateTime dtCurrentyear = g.GetCurrentDateTime();
                int dt1 = DateTime.Now.Year;
                // ddlyear.SelectedIndex = dt1;
                //ddlyear.Items.FindByValue(DateTime.Now.Year.ToString()).Selected = true;


                ddlyear.SelectedIndex = -1;
                ddlyear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;
                BindAllData();

            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }


    private void fillYear()
    {
        int i = int.Parse(DateTime.Now.AddYears(0).Date.Year.ToString());
        for (int j = 0; j <= 50; j++)
        {

            ddlyear.Items.Add(i.ToString());
            i++;

        }

    }

    private System.Web.UI.WebControls.ListItem ListItem(string p1, string p2)
    {
        throw new NotImplementedException();
    }

    private void BindAllData()
    {
        try
        {

            string query = "select EM.EmployeeID,EM.FName++EM.MName++EM.Lname AS EmpName,MC.CompanyName,MD.DeptName,DT.DesigName,EM.SalaryAccountNo,(Convert(int,wd.presentdays)+Convert(int,wd.SundayHolidays)) as [WorkingDays],ST.netpay,WD.Month,WD.year,EM.EmpCardID FROM EmployeeTB EM LEFT OUTER JOIN CompanyInfoTB MC ON MC.CompanyId=EM.CompanyId left outer join MasterDeptTB MD ON MD.DeptID=EM.DeptId left outer join MasterDesgTB DT  on DT.DesigID=EM.DesgId left outer join BeforeSalProcessTB WD on WD.empid=EM.EmployeeID left outer join EmployeeSalarySettingsTB ST ON ST.Empid=EM.EmployeeID where EM.Relivingstatus is null ";


            if (ddlmonth.SelectedIndex > 0 && ddlyear.SelectedIndex == 0)
            {
                query = query + " and WD.Month='" + ddlmonth.SelectedIndex + "' and WD.year='" + ddlyear.SelectedItem.Text + "'";
            }
            else if (ddlmonth.SelectedIndex > 0 && ddlyear.SelectedIndex > 0)
            {
                query = query + " and WD.Month='" + ddlmonth.SelectedIndex + "' and WD.year='" + ddlyear.SelectedItem.Text + "'";
            }
            else if (ddlyear.SelectedIndex > 0 && ddlmonth.SelectedIndex == 0)
            {
                query = query + " and WD.year='" + ddlyear.SelectedItem.Text + "'";
            }


            DataSet dsalldata = g.ReturnData1(query);
            if (dsalldata.Tables[0].Rows.Count > 0)
            {
                gv.DataSource = dsalldata.Tables[0];
                gv.DataBind();
            }
            else
            {
                gv.DataSource = null;
                gv.DataBind();
            }
            lblcnt.Text = dsalldata.Tables[0].Rows.Count.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindAllData();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        DateTime dtcurrenttimes = g.GetCurrentDateTime();
                int dt = DateTime.Now.Month;
                ddlmonth.SelectedIndex = dt;

                DateTime dtCurrentyear = g.GetCurrentDateTime();
                int dt1 = DateTime.Now.Year;
                // ddlyear.SelectedIndex = dt1;
                //ddlyear.Items.FindByValue(DateTime.Now.Year.ToString()).Selected = true;


                ddlyear.SelectedIndex = -1;
                ddlyear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;
        BindAllData();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
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
                    gv.AllowPaging = false;
                    BindAllData();

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
                    Response.AddHeader("content-disposition", "attachment;filename=WPSEmployeeReport.pdf");
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

        gv.AllowPaging = false;
        BindAllData();
        // ..bindGrid();



        gv.RenderControl(hw);

        Response.Write(strwtr.ToString());

        Response.End();
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
        //gv.PagerSettings.Visible = false;
        //gv.DataBind();
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gv.RenderControl(hw);
        string gridHTML = sw.ToString().Replace("\"", "'").Replace(System.Environment.NewLine, "");
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
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
        gv.PagerSettings.Visible = true;
        gv.DataBind();
        BindAllData();
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindAllData();
    }
}