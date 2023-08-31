
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using Microsoft.Reporting.WebForms;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;

public partial class Attendance_Report_new : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {

            }
        }
        else
        {
            Response.Redirect("Default.aspx");
        }
    }




    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void ddEmp_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        //All Com Data..
        var empdata = (from emp in HR.EmployeeTBs select emp);

        var Atdlog = (from ALT in HR.AttendaceLogTBs select ALT);

        var DeptTb = (from Dept in HR.MasterDeptTBs select Dept);

        DataTable Atddt = new DataTable();
        Atddt.Clear();
        Atddt.Columns.Add(" ");//ColumnName
        for (var i = 1; i <= 31; i++)
        {
            Atddt.Columns.Add("Day" + i + "");
        }
        Atddt.Columns.Add("ColumnMonthDate");

        foreach (var empdatas in empdata)
        {
            DataRow empdetailsrow = Atddt.NewRow();


            var empdatadatewise = Atdlog.Where(x => x.EmployeeId == empdatas.EmployeeId && x.AttendanceDate.Month == ddmonth.SelectedIndex && x.AttendanceDate.Year == Convert.ToInt32(ddyear.SelectedItem.Value)).Select(x => x).ToList().Take(31);

            if (empdatadatewise != null)
            {
                empdetailsrow[0] = "Person ID";
                empdetailsrow[1] = empdatas.EmployeeId;
                empdetailsrow[2] = "Employee Name";
                empdetailsrow[3] = empdatas.FName + " " + empdatas.MName + " " + empdatas.Lname;
                empdetailsrow[4] = "Department Name";
                empdetailsrow[5] = DeptTb.Where(x=>x.DeptID==empdatas.DeptId).Select(x=>x.DeptName).FirstOrDefault();
                empdetailsrow[32] = "01/" + ddmonth.SelectedIndex + "/" + ddyear.SelectedValue + " TO " + DateTime.DaysInMonth(ddyear.SelectedIndex, ddmonth.SelectedIndex) + "/" + ddmonth.SelectedIndex + "/" + ddyear.SelectedValue;
                Atddt.Rows.Add(empdetailsrow);



                DataRow Daterow = Atddt.NewRow();
                DataRow checkinrow = Atddt.NewRow();
                DataRow checkoutrow = Atddt.NewRow();
                DataRow otrow = Atddt.NewRow();
                DataRow Laterow = Atddt.NewRow();
                DataRow earlyleaverow = Atddt.NewRow();
                DataRow attendedrow = Atddt.NewRow();
                DataRow breakrow = Atddt.NewRow();
                DataRow statusrow = Atddt.NewRow();
                DataRow summueryrow = Atddt.NewRow();



                int colmncount = 1;
                Daterow[0] = "Date";
                foreach (var atd in empdatadatewise)
                {
                    Daterow[colmncount] = atd.AttendanceDate.Day;
                    colmncount++;
                }
                Atddt.Rows.Add(Daterow);

                colmncount = 1;
                checkinrow[0] = "Check In";
                foreach (var atd in empdatadatewise)
                {
                    checkinrow[colmncount] =  (atd.InTime!="" && atd.InTime !=null)?TimeSpan.Parse(atd.InTime).ToString(@"hh\:mm"):"";
                    colmncount++;
                }
                Atddt.Rows.Add(checkinrow);

                colmncount = 1;
                checkoutrow[0] = "Check Out";
                foreach (var atd in empdatadatewise)
                {
                    checkoutrow[colmncount] = (atd.OutTime != "" && atd.OutTime != null) ? TimeSpan.Parse(atd.OutTime).ToString(@"hh\:mm") : "";
                    colmncount++;
                }
                Atddt.Rows.Add(checkoutrow);

                colmncount = 1;
                otrow[0] = "OT";
                foreach (var atd in empdatadatewise)
                {
                    otrow[colmncount] = atd.OverTime;
                    colmncount++;
                }
                Atddt.Rows.Add(otrow);

                colmncount = 1;
                Laterow[0] = "LateBy";
                foreach (var atd in empdatadatewise)
                {
                    Laterow[colmncount] = atd.LateBy;
                    colmncount++;
                }
                Atddt.Rows.Add(Laterow);


                colmncount = 1;
                earlyleaverow[0] = "EarlyBy";
                foreach (var atd in empdatadatewise)
                {
                    earlyleaverow[colmncount] = atd.EarlyBy;
                    colmncount++;
                }
                Atddt.Rows.Add(earlyleaverow);

                colmncount = 1;
                attendedrow[0] = "Attended";
                foreach (var atd in empdatadatewise)
                {
                    attendedrow[colmncount] = atd.Present;
                    colmncount++;
                }
                Atddt.Rows.Add(attendedrow);

                colmncount = 1;
                breakrow[0] = "Break";
                foreach (var atd in empdatadatewise)
                {
                    breakrow[colmncount] = 0;
                    colmncount++;
                }
                Atddt.Rows.Add(breakrow);


                colmncount = 1;
                statusrow[0] = "Status";
                foreach (var atd in empdatadatewise)
                {
                    statusrow[colmncount] = atd.Status;
                    colmncount++;
                }
                Atddt.Rows.Add(statusrow);

                colmncount = 1;
                summueryrow[0] = "Summury";
                foreach (var atd in empdatadatewise)
                {
                    summueryrow[colmncount] = atd.Remarks;
                    colmncount++;
                }
                Atddt.Rows.Add(summueryrow);

            }
        }

        //ReportViewer1.ProcessingMode = ProcessingMode.Local;
        //ReportViewer1.LocalReport.ReportPath = Server.MapPath("Attendance_Report.rdlc");
        //ReportDataSource datasource = new ReportDataSource("Attendance_Report", Atddt);
        //ReportViewer1.LocalReport.DataSources.Clear();
        //ReportViewer1.LocalReport.DataSources.Add(datasource);
        Atddt.Columns.Remove(Atddt.Columns[32]);
        GridView1.DataSource = Atddt;
        GridView1.DataBind();

    }

    public override void VerifyRenderingInServerForm(Control control)
    {
        //required to avoid the runtime error "  
        //Control 'GridView1' of type 'GridView' must be placed inside a form tag with runat=server."  
    }
    protected void pddfdowload_Click(object sender, EventArgs e)
    {
        
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename="+ddmonth.SelectedItem.ToString()+".pdf");
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            GridView1.RenderControl(hw);
            string header = "'<div class='row'><div class='col-lg-12' style='text-align:center'><label>Attendance Summary Report</label></div></div><br/><br/>";
            StringReader sr = new StringReader((header+sw).ToString());
            Document pdfDoc = new Document(PageSize.A4.Rotate(), 10f, 10f, 10f, 0f);
            HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
            PdfWriter.GetInstance(pdfDoc, Response.OutputStream);
            pdfDoc.Open();
            htmlparser.Parse(sr);
            pdfDoc.Close();
            Response.Write(pdfDoc);
            Response.End();
            GridView1.AllowPaging = true;
            GridView1.DataBind();
        
    }   
}