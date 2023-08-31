using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.IO;
using System.Data.SqlClient;

public partial class EmployePerformanceReport : System.Web.UI.Page
{

    HrPortalDtaClassDataContext EM = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();


    protected void Page_Load(object sender, EventArgs e)
    {

         ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnPrint);
        if(Session["UserId"] !=null)
        {
            if(!IsPostBack)
            {

                txtfromdate.Text = g.GetStartOfMonth(g.GetCurrentDateTime()).ToShortDateString();

                DateTime StartDate = Convert.ToDateTime(txtfromdate.Text);
                txtfromdate.Text = StartDate.ToString("MM/dd/yyyy");

                txttodate.Text = g.EndOfMonth(g.GetCurrentDateTime()).ToShortDateString();

                DateTime EndtDate = Convert.ToDateTime(txttodate.Text);
                txttodate.Text = EndtDate.ToString("MM/dd/yyyy");
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployePerformanceReport.aspx");
    }

    private void DataBind()
    {
        var data = from d in EM.EmployePerformances
                   where d.DateofEval>=Convert.ToDateTime(txtfromdate.Text) && d.DateofEval<= Convert.ToDateTime(txttodate.Text)
                   select new
                   {
                       d.EmpName,
                       d.DateofEval,
                       d.EmpPosition,
                       d.EvaluatorName,
                       d.EvalType,
                   };
        if (data != null && data.Count() > 0)
        {
            grd_EmpPerformance.DataSource = data;
            grd_EmpPerformance.DataBind();
            lblcnt.Text = data.Count().ToString();
        }
        else
        {
            grd_EmpPerformance.DataSource = null;
            grd_EmpPerformance.DataBind();
            lblcnt.Text = "0";
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        DataBind();
    }
    protected void grd_EmpPerformance_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_EmpPerformance.PageIndex = e.NewPageIndex;
        DataBind();
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }
    protected void btnPrint_Click(object sender, EventArgs e)
    {
         if (grd_EmpPerformance.Rows.Count > 0)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    //    div1.Visible = true;
                    div2.Visible = true;
                    div3.Visible = true;
                    grd_EmpPerformance.AllowPaging = false;
                    //To Export all pages
                   
                   
                   // lblfromdate.Text = txtfromdate.Text;
                    lblTodate.Text = txttodate.Text;
                    grd_EmpPerformance.Style.Add("Border","1px #333 solid");
                    grd_EmpPerformance.AllowPaging = false;
                    DataBind();
                    tbldisplay.RenderControl(hw);
                    tbldisplay.Style.Add("width", "100%");
                    tbldisplay.Style.Add("font-size", "10px");
                    tbldisplay.Style.Add("text-align", "left");
                    tbldisplay.Style.Add("text-decoration", "none");
                    tbldisplay.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    tbldisplay.Style.Add("font-size", "8px");
                    grd_EmpPerformance.AllowPaging = false;
                    StringReader sr = new StringReader(sw.ToString());
                    Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 0f, 10f);
                    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
                    PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
                    pdfDoc.Open();
                    htmlparser.Parse(sr);
                    pdfDoc.Close();
                    grd_EmpPerformance.AllowPaging = false;
                    Response.ContentType = "application/pdf";
                    Response.AddHeader("content-disposition", "attachment;filename=Contractor-Payment-Report.pdf");
                    Response.Cache.SetCacheability(HttpCacheability.NoCache);
                    Response.Write(pdfDoc);
                    Response.End();
                }
            }
        }
        else
        {
            div2.Visible = false;
            div3.Visible = false;
            g.ShowMessage(this.Page, "Data is not present.");
        }
    }
    protected void grd_EmpPerformance_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
