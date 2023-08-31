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

public partial class EmployeeCompanydeptReport : System.Web.UI.Page
{
    Genreal g = new Genreal();
    HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
    protected void Page_Load(object sender, EventArgs e)
    {
       // ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
       // scriptManager.RegisterPostBackControl(this.btnPrintCurrent);

        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                fillcompany();
                txtfdate.Text = g.GetStartOfMonth(DateTime.Now.Date).ToShortDateString();
                txttdate.Text = g.EndOfMonth(DateTime.Now.Date).ToShortDateString();
                BindAllData();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }

    private void fillcompany()
    {
        var dt = from d in hr.CompanyInfoTBs select new { d.CompanyId,d.CompanyName };
        ddcomp.DataTextField = "CompanyName";
        ddcomp.DataValueField = "CompanyId";
        ddcomp.DataSource = dt;
        ddcomp.DataBind();
        ddcomp.Items.Insert(0, "All");
    }

    private void cleartextbox()
    {
        txtfdate.Text = g.GetStartOfMonth(DateTime.Now.Date).ToShortDateString();
        txttdate.Text = "";
    }
    protected void gv_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv.PageIndex = e.NewPageIndex;
        BindAllData();
        gv.DataBind();
    }
    protected void gv_Sorting(object sender, GridViewSortEventArgs e)
    {
      
    }
   


    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindAllData();
    }
    protected void PrintCurrentPage(object sender, EventArgs e)
    {
        gv.PagerSettings.Visible = false;
        BindAllData();
        gv.DataBind();
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);
        gv.RenderControl(hw);
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
        gv.PagerSettings.Visible = true;
    }

    private void BindAllData()
    {
        var data = from dt in hr.EmployeeTBs
                   join n in hr.MasterDeptTBs on dt.DeptId equals n.DeptID
                   join de in hr.MasterDesgTBs on dt.DesgId equals de.DesigID
                   join c in hr.CompanyInfoTBs on dt.CompanyId equals c.CompanyId
                   where dt.DOJ >= Convert.ToDateTime(txtfdate.Text) && dt.DOJ <= Convert.ToDateTime(txttdate.Text)
                   select new { EmployeeName = dt.MName == null ? dt.FName + ' ' + dt.Lname : dt.FName + ' ' + dt.MName + ' ' +
                       dt.Lname,
                                c.CompanyName,de.DesigName,
                               DeptName= n.DeptName,
                       //CompanyName = dt.CompanyInfoTB.CompanyName,
                      // DeptName = dt.MasterDeptTB.DeptName, DesigName = dt.MasterDesgTB.DesigName,
                      Gender = dt.Gender == "0" ? "Male" : "Female", Email = dt.EmailId, ContactNo = dt.ContactNo };
        if (ddcomp.SelectedIndex!=0)
        {
            data = data.Where(d => d.CompanyName == ddcomp.SelectedItem.Text);
        }
        
        
        if (data.Count()>0)
        {
            gv.DataSource = data;
            gv.DataBind();
        }
        else
        {
            gv.DataSource = null;
            gv.DataBind();
        }
        lblcnt.Text = data.Count().ToString();
        
    }
    protected void btnExpPDF_Click(object sender, EventArgs e)
    {
        using (StringWriter sw = new StringWriter())
        {
            using (HtmlTextWriter hw = new HtmlTextWriter(sw))
            {
                //To Export all pages
                gv.AllowPaging = false;
                BindAllData();
                //gv.DataBind();

                gv.HeaderRow.Style.Add("width", "15%");
                gv.HeaderRow.Style.Add("font-size", "10px");
                gv.Style.Add("text-decoration", "none");
                gv.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                gv.Style.Add("font-size", "8px");

                tbldisp.RenderControl(hw);
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





        //Response.Clear();
        //Response.Buffer = true;
        //Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.xls");
        //Response.Charset = "";
        //Response.ContentType = "application/vnd.ms-excel";

        //using (StringWriter sw = new StringWriter())
        //{
        //    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
        //    {
        //        //To Export all pages


        //        tbldisp.RenderControl(hw);
        //        StringReader sr = new StringReader(sw.ToString());
        //        Document pdfDoc = new Document(PageSize.A2, 10f, 10f, 0f, 10f);
        //        HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
        //        PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
        //        pdfDoc.Open();
        //        htmlparser.Parse(sr);
        //        pdfDoc.Close();

        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("content-disposition", "attachment;filename=GridViewExport.pdf");
        //        Response.Cache.SetCacheability(HttpCacheability.NoCache);
        //        Response.Write(pdfDoc);
        //        Response.End();
        //    }
        //}
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
        gv.AllowPaging = false;
        BindAllData();
        gv.RenderControl(hw);
        Response.Write(strwtr.ToString());
        Response.End();        
    }
}