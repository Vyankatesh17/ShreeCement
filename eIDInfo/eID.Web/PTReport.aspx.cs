using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Text;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Data;

public partial class PTReport : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        ScriptManager scriptManager = ScriptManager.GetCurrent(this.Page);
        scriptManager.RegisterPostBackControl(this.btnPrintCurrent);
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                Response.Cache.SetCacheability(HttpCacheability.NoCache);
                Bindstate();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void BindAllData()
      {
          try
          {
              var AllData = from ad in HR.PTMasterTBs
                            where ad.SlabFrom >= Convert.ToDecimal(txtslabfrom.Text.Trim())
                            && ad.SlabTo <= Convert.ToDecimal(txtslabto.Text.Trim())
                            select new
                            {
                                slab = ad.SlabFrom + "-" + ad.SlabTo, //show likke 01-5000  =slabfrom= 01 and slabTo =5000
                                ad.Charge,
                                ad.Jan,
                                ad.Feb,
                                ad.Mar,
                                ad.Apr,
                                ad.May,
                                ad.Jun,
                                ad.Jul,
                                ad.Aug,
                                ad.Sep,
                                ad.Oct,
                                ad.Nov,
                                ad.Dec,
                                StateName =""


                            };

              //String query = "select (Convert(varchar,PT.SlabFrom,101)+ convert(varchar,PT.SlabTo,101)) as Slab,PT.Charge,PT.Jan,PT.Feb,PT.Mar,PT.[Apr], PT.[May], PT.[Jun], PT.[Jul], PT.[Aug], PT.[Sep], PT.[Oct], PT.[Nov], PT.[Dec],ST.StateName from PTMasterTB PT LEFT OUTER JOIN StateTB ST ON ST.StateId=PT.Location where PT.SlabFrom >='" + Convert.ToDecimal(txtslabfrom.Text.Trim() + "' and  PT.SlabTo <='" + Convert.ToDecimal(txtslabto.Text.Trim() + "'"));


              if (ddlstate.SelectedIndex != 0)
              {
                  AllData = AllData.Where(d => d.StateName == ddlstate.SelectedItem.Text);
              }
              if (AllData.Count() > 0)
              {
                  gv1.DataSource = AllData;
                  gv1.DataBind();

              }
              else
              {
                  gv1.DataSource = null;
                  gv1.DataBind();
              }
              lblcount.Text = AllData.Count().ToString();
          }
        catch(Exception ex)
          {
              throw ex;
        }
        
    }

    private void Bindstate()
    {
        //var datac = (from dt in HR.StateTBs
        //            where dt.Status==0
        //            select new { dt.StateId, dt.StateName }).OrderBy(d=>d.StateName);


        //ddlstate.DataSource = datac;
        //ddlstate.DataTextField = "StateName";
        //ddlstate.DataValueField = "StateId";
        //ddlstate.DataBind();
        //ddlstate.Items.Insert(0, "All");
    }
    protected void btnshow_Click(object sender, EventArgs e)
    {
       BindAllData();
     
    }
    protected void gv1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gv1.PageIndex=e.NewPageIndex;
        BindAllData();
        gv1.DataBind();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        cleardata();

    }

    private void cleardata()
    {
        txtslabto.Text = "0.00";
        txtslabfrom.Text = "0.00";
        ddlstate.SelectedIndex = 0;
        BindAllData();
    }
    protected void PrintCurrentPage(object sender, EventArgs e)
    {

        if (gv1.Rows.Count > 0)
        {
            gv1.PagerSettings.Visible = false;
            BindAllData();
            gv1.DataBind();
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            gv1.RenderControl(hw);
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
            gv1.PagerSettings.Visible = true;
        }
        else
        {
            g.ShowMessage(this.Page, "Data Is Not Present");
        }
    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        /*Verifies that the control is rendered */
    }

    protected void btnExpPDF_Click(object sender, EventArgs e)
    {
        if (gv1.Rows.Count > 0)
        {
            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter hw = new HtmlTextWriter(sw))
                {
                    //To Export all pages
                    gv1.AllowPaging = false;
                    BindAllData();
                    //gv1.DataBind();

                    gv1.HeaderRow.Style.Add("width", "15%");
                    gv1.HeaderRow.Style.Add("font-size", "10px");
                    gv1.Style.Add("text-decoration", "none");
                    gv1.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
                    gv1.Style.Add("font-size", "8px");

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
        }
        else
        {
            g.ShowMessage(this.Page, "Data Is Not Present");
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
        if (gv1.Rows.Count > 0)
        {
            Response.Clear();

            Response.ContentType = "application/ms-excel";

            Response.Charset = "";

            Page.EnableViewState = false;

            Response.AddHeader("Content-Disposition", "attachment;filename=Employees.xls");

            StringWriter strwtr = new StringWriter();

            HtmlTextWriter hw = new HtmlTextWriter(strwtr);
            gv1.AllowPaging = false;
            BindAllData();
            gv1.RenderControl(hw);
            Response.Write(strwtr.ToString());
            Response.End();
        }
        else
        {
            g.ShowMessage(this.Page, "Data Is Not Present");
        }
    }
}