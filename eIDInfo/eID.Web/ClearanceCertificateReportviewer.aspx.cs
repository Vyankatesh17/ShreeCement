using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;

public partial class ClearanceCertificateReportviewer : System.Web.UI.Page
{
    Genreal g = new Genreal();
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    string idnew, no;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if(Request.QueryString["id"].ToString() !=null)
            {

            int id = Convert.ToInt32(Request.QueryString["id"].ToString());
            //  DataSet dsNextSchedule = g.ReturnData(@"select convert(varchar,Date,103) as Date,Amount,case when Status=0 then 'Pending' else 'Paid' end Status,ISNULL(convert(varchar,PaidDate,103),'-') PaidDate from PaymentScheduleTB where AdmissionId='" + Admissionid + "'");

            var clearancedata = (from d in HR.ClearanceCertificateSP(Convert.ToInt32(id)).ToList() select d); 

            ReportViewer1.LocalReport.ReportPath = MapPath("ClearanceCertificateReport.rdlc");

            ReportDataSource rdc = new ReportDataSource("DataSet1", clearancedata);
          

            ReportViewer1.LocalReport.DataSources.Add(rdc);  
            }
        }
           
    }
}