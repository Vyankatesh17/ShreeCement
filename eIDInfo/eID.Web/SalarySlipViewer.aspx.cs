using Microsoft.Reporting.WebForms;
using System;
using System.Data;
using Microsoft.SqlServer;

public partial class SalarySlipViewer : System.Web.UI.Page
{
    Genreal g = new Genreal();
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    string id, no;


    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            string netpayday = "";
            if (Session["netpaydays"] != null)
            {
                netpayday = Session["netpaydays"].ToString();
            }
            else
            {
                netpayday = "0";
            }

            int EMP = Convert.ToInt32(Session["idd"]);

            //               DataSet Result1 = g.ReturnData1(@"SELECT  isnull(SalaryComponentTB.fixedvalue,0)fixedvalue,       SalaryProcessTB.GrossSalary, SalaryProcessTB.NetSlary, SalaryDetail.Componentid, SalaryDetail.ComponentType, SalaryDetail.amount, 
            //                         SalaryComponentTB.ComponentName
            //FROM            SalaryProcessTB INNER JOIN
            //                         SalaryDetail ON SalaryProcessTB.SalSettingID = SalaryDetail.EmpSalarysettingid INNER JOIN
            //                         SalaryComponentTB ON SalaryDetail.Componentid = SalaryComponentTB.Componentid
            //where SalaryProcessTB.[SalProcessId]=" + EMP + "  and SalaryDetail.ComponentType='Earning'");

            //               DataSet Result2 = g.ReturnData1(@"SELECT  isnull(SalaryComponentTB.fixedvalue,0)fixedvalue,       SalaryProcessTB.GrossSalary, SalaryProcessTB.NetSlary, SalaryDetail.Componentid, SalaryDetail.ComponentType, SalaryDetail.amount, 
            //                     case when   SalaryComponentTB.ComponentName like 'Other' then 'Misc. Deduction' else SalaryComponentTB.ComponentName end ComponentName
            //FROM            SalaryProcessTB INNER JOIN
            //                         SalaryDetail ON SalaryProcessTB.SalSettingID = SalaryDetail.EmpSalarysettingid INNER JOIN
            //                         SalaryComponentTB ON SalaryDetail.Componentid = SalaryComponentTB.Componentid
            //where SalaryProcessTB.[SalProcessId]=" + EMP + "  and SalaryDetail.ComponentType='Deduction'");



            DataSet Result1 = g.ReturnData1(@"SELECT  isnull(SalaryComponentTB.fixedvalue,0)fixedvalue,       SalaryProcessTB.GrossSalary, SalaryProcessTB.NetSlary, 
SalaryProcessDetailsTB.Componentid, SalaryProcessDetailsTB.ComponentType, isnull(cast(SalaryProcessDetailsTB.amount as decimal(18,2) ),0)as amount, 
                     case when  isnull(SalaryComponentTB.ComponentName,SalaryProcessDetailsTB.Componentid) 
					 like 'Other' then 'Misc. Deduction' 
					 else isnull(SalaryComponentTB.ComponentName,SalaryProcessDetailsTB.Componentid) end ComponentName,
					 isnull(case SalaryProcessDetailsTB.flag  when 0 then cast(SalaryProcessDetailsTB.amount as decimal(18,2) ) when 1 then '0.00' end,0) as Amt
FROM            SalaryProcessTB left outer JOIN
                         SalaryProcessDetailsTB ON SalaryProcessDetailsTB.EmpSalaryProcessid = SalaryProcessTB.SalProcessId left outer JOIN
                         SalaryComponentTB ON SalaryProcessDetailsTB.Componentid = SalaryComponentTB.componentname
where SalaryProcessTB.[SalProcessId]='" + EMP + "'  and SalaryProcessDetailsTB.ComponentType='Earning'");

            DataSet Result2 = g.ReturnData1(@"SELECT  isnull(SalaryComponentTB.fixedvalue,0)fixedvalue,       SalaryProcessTB.GrossSalary, SalaryProcessTB.NetSlary, SalaryProcessDetailsTB.Componentid, SalaryProcessDetailsTB.ComponentType, cast(round(SalaryProcessDetailsTB.amount,2) as numeric(36,2)) amount, 
                     case when  isnull(SalaryComponentTB.ComponentName,SalaryProcessDetailsTB.Componentid) like 'Other' then 'Misc. Deduction' else isnull(SalaryComponentTB.ComponentName,SalaryProcessDetailsTB.Componentid) end ComponentName
FROM            SalaryProcessTB left outer JOIN
                         SalaryProcessDetailsTB ON SalaryProcessDetailsTB.EmpSalaryProcessid = SalaryProcessTB.SalProcessId left outer JOIN
                         SalaryComponentTB ON SalaryProcessDetailsTB.Componentid = SalaryComponentTB.componentname
where SalaryProcessTB.[SalProcessId]='" + EMP + "'  and SalaryProcessDetailsTB.ComponentType='Deduction'");



            //               DataSet Result3 = g.ReturnData1(@"SELECT DISTINCT 
            //                         DATENAME(Month, DATEADD(Month, SalaryProcessTB.Month - 1, CAST('2008-01-01' AS datetime))) AS Month, 
            //                         EmployeeTB.FName + ' ' + EmployeeTB.MName + ' ' + EmployeeTB.Lname AS Name, 
            //                         CASE WHEN EmployeeTB.Grade = '--Select--' THEN '--' ELSE EmployeeTB.Grade END AS Grade, CompanyInfoTB.CompanyName, MasterDeptTB.DeptName, 
            //                         MasterDesgTB.DesigName, MasterDeptTB.CompanyId, SalaryProcessTB.Month AS Expr1, SalaryProcessTB.Year, SalaryProcessTB.WorkingDays, 
            //                         SalaryProcessTB.Netpaybledays, SalaryProcessTB.PFAccountNo, SalaryProcessTB.ESICAccountNo, SalaryProcessTB.BankName, SalaryProcessTB.SalaryMode, 
            //                         SalaryProcessTB.SalaryAccountNo, SalaryProcessTB.NetSlary, SalaryProcessTB.GrossSalary, BeforeSalProcessTB.absentdays
            //FROM            EmployeeTB INNER JOIN
            //                         MasterDeptTB ON EmployeeTB.DeptId = MasterDeptTB.DeptID INNER JOIN
            //                         SalaryProcessTB ON EmployeeTB.EmployeeId = SalaryProcessTB.EmployeeID INNER JOIN
            //                         BeforeSalProcessTB ON EmployeeTB.EmployeeId = BeforeSalProcessTB.empid AND SalaryProcessTB.Month = BeforeSalProcessTB.month AND 
            //                         SalaryProcessTB.Year = BeforeSalProcessTB.year INNER JOIN
            //                         MasterDesgTB ON EmployeeTB.DesgId = MasterDesgTB.DesigID LEFT OUTER JOIN
            //                         CompanyInfoTB ON EmployeeTB.CompanyId = CompanyInfoTB.CompanyId
            //      where  SalProcessId='" + EMP + "'");


            //QRY By Shivaji 06 Jan 2014.
            DataSet Result3 = g.ReturnData1(@"SELECT DISTINCT 
                         DATENAME(Month, DATEADD(Month, SalaryProcessTB.Month - 1, CAST('2008-01-01' AS datetime))) AS Month, 
                         EmployeeTB.FName + ' ' + EmployeeTB.MName + ' ' + EmployeeTB.Lname AS Name, 
                         CASE WHEN EmployeeTB.Grade = '--Select--' THEN '--' ELSE EmployeeTB.Grade END AS Grade, CompanyInfoTB.CompanyName, MasterDeptTB.DeptName, 
                         MasterDesgTB.DesigName, MasterDeptTB.CompanyId, SalaryProcessTB.Month AS Expr1, SalaryProcessTB.Year, SalaryProcessTB.WorkingDays, 
                         SalaryProcessTB.Netpaybledays, EmployeeTB.PFAccountNo, EmployeeTB.ESICAccountNo, EmployeeTB.BankName, SalaryProcessTB.SalaryMode, 
                         EmployeeTB.SalaryAccountNo, SalaryProcessTB.NetSlary, SalaryProcessTB.GrossSalary, BeforeSalProcessTB.absentdays, EmployeeTB.PanNo PANNO,EmployeeTB.DOJ
FROM            EmployeeTB INNER JOIN
                         MasterDeptTB ON EmployeeTB.DeptId = MasterDeptTB.DeptID INNER JOIN
                         SalaryProcessTB ON EmployeeTB.EmployeeId = SalaryProcessTB.EmployeeID INNER JOIN
                         BeforeSalProcessTB ON EmployeeTB.EmployeeId = BeforeSalProcessTB.empid AND SalaryProcessTB.Month = BeforeSalProcessTB.month AND 
                         SalaryProcessTB.Year = BeforeSalProcessTB.year INNER JOIN
                         MasterDesgTB ON EmployeeTB.DesgId = MasterDesgTB.DesigID LEFT OUTER JOIN
                         CompanyInfoTB ON EmployeeTB.CompanyId = CompanyInfoTB.CompanyId
      where  SalProcessId='" + EMP + "'");



            string mon = Result3.Tables[0].Rows[0]["Month"].ToString();
            string earnamt = "", DeductAmt = "";

            //             DataSet dsEarn = g.ReturnData1(@"SELECT Sum(isnull(Convert(Decimal(18,2),SalaryComponentTB.fixedvalue),0)) Earnamt   FROM            SalaryProcessTB INNER JOIN SalaryDetail ON SalaryProcessTB.SalSettingID = SalaryDetail.EmpSalarysettingid INNER JOIN
            //SalaryComponentTB ON SalaryDetail.Componentid = SalaryComponentTB.Componentid where  SalaryProcessTB.[SalProcessId]='" + EMP + "'  and SalaryDetail.ComponentType='Earning'");

            //             DataSet dsDed = g.ReturnData1(@"SELECT Sum(isnull(Convert(Decimal(18,2),SalaryDetail.amount),0)) Deductamt   FROM            SalaryProcessTB INNER JOIN SalaryDetail ON SalaryProcessTB.SalSettingID = SalaryDetail.EmpSalarysettingid INNER JOIN
            //SalaryComponentTB ON SalaryDetail.Componentid = SalaryComponentTB.Componentid where  SalaryProcessTB.[SalProcessId]='" + EMP + "'  and SalaryDetail.ComponentType='Deduction'");

            //                 string totall="" ;
            //             DataSet TOTAL = g.ReturnData1(@"SELECT Sum(isnull(Convert(Decimal(18,2),SalaryDetail.amount),0))fixedvalue   FROM            SalaryProcessTB INNER JOIN SalaryDetail ON SalaryProcessTB.SalSettingID = SalaryDetail.EmpSalarysettingid INNER JOIN
            //SalaryComponentTB ON SalaryDetail.Componentid = SalaryComponentTB.Componentid where  SalaryProcessTB.[SalProcessId]='" + EMP + "'  and SalaryDetail.ComponentType='Earning'");


            //             DataSet dsEarn = g.ReturnData1(@"SELECT isnull(Sum(isnull(Convert(Decimal(18,2),SalaryComponentTB.fixedvalue),0)),0) Earnamt   FROM             SalaryProcessTB left outer JOIN
            //                         SalaryProcessDetailsTB ON SalaryProcessDetailsTB.EmpSalaryProcessid = SalaryProcessTB.SalProcessId left outer JOIN
            //                         SalaryComponentTB ON SalaryProcessDetailsTB.Componentid = SalaryComponentTB.componentname where  SalaryProcessTB.[SalProcessId]='"+EMP+"'  and SalaryProcessDetailsTB.ComponentType='Earning'");
            DataSet dsEarn = g.ReturnData1(@"SELECT isnull(Sum(isnull(Convert(Decimal(18,2),SalaryProcessDetailsTB.amount),0)),0) Earnamt   FROM             SalaryProcessTB left outer JOIN
                         SalaryProcessDetailsTB ON SalaryProcessDetailsTB.EmpSalaryProcessid = SalaryProcessTB.SalProcessId left outer JOIN
                         SalaryComponentTB ON SalaryProcessDetailsTB.Componentid = SalaryComponentTB.componentname where  SalaryProcessTB.[SalProcessId]='" + EMP + "'  and SalaryProcessDetailsTB.ComponentType='Earning'");

            DataSet dsDed = g.ReturnData1(@"SELECT isnull(Sum(isnull(Convert(Decimal(18,2),SalaryProcessDetailsTB.amount),0)),0) Deductamt   FROM             SalaryProcessTB left outer JOIN
                         SalaryProcessDetailsTB ON SalaryProcessDetailsTB.EmpSalaryProcessid = SalaryProcessTB.SalProcessId left outer JOIN
                         SalaryComponentTB ON SalaryProcessDetailsTB.Componentid = SalaryComponentTB.componentname where  SalaryProcessTB.[SalProcessId]='" + EMP + "'  and SalaryProcessDetailsTB.ComponentType='Deduction'");

            string totall = "";
            DataSet TOTAL = g.ReturnData1(@"SELECT isnull(Sum(isnull(Convert(Decimal(18,2),SalaryProcessDetailsTB.amount),0)),0)fixedvalue   FROM             SalaryProcessTB left outer JOIN
                         SalaryProcessDetailsTB ON SalaryProcessDetailsTB.EmpSalaryProcessid = SalaryProcessTB.SalProcessId left outer JOIN
                         SalaryComponentTB ON SalaryProcessDetailsTB.Componentid = SalaryComponentTB.componentname where  SalaryProcessTB.[SalProcessId]='" + EMP + "'  and SalaryProcessDetailsTB.ComponentType='Earning'");


            if (dsEarn.Tables[0].Rows.Count > 0)
            {
                earnamt = dsEarn.Tables[0].Rows[0]["Earnamt"].ToString();
            }
            if (dsEarn.Tables[0].Rows.Count > 0)
            {
                DeductAmt = dsDed.Tables[0].Rows[0]["Deductamt"].ToString();
            }
            if (TOTAL.Tables[0].Rows.Count > 0)
            {
                totall = TOTAL.Tables[0].Rows[0]["fixedvalue"].ToString();
            }


            string EarnTotal = Result3.Tables[0].Rows[0]["Month"].ToString();



            string netamt = (Convert.ToDecimal(totall) - Convert.ToDecimal(DeductAmt)).ToString();
            decimal number = Math.Round(Convert.ToDecimal(netamt));
            string s = retWord(Convert.ToInt32(number));

            ReportViewer1.LocalReport.ReportPath = MapPath("Sal.rdlc");
            //Earning
            //if (Result1.Tables[0].Rows[0]["ComponentName"].ToString() == "OT" && Result1.Tables[0].Rows[0]["amount"].ToString()=="0.00")
            //{
            //    Result1.Tables[0].Rows[0];
            //}
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet4", Result1.Tables[0]));//For Earning
            //Deduct
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet2", Result2.Tables[0]));///For deduction
            //Emp
            ReportViewer1.LocalReport.DataSources.Add(new ReportDataSource("DataSet3", Result3.Tables[0]));//For header information
            ReportParameter p = new ReportParameter("p1", s);
            this.ReportViewer1.LocalReport.SetParameters(p);
            ReportParameter netamts = new ReportParameter("netamt", netamt);
            this.ReportViewer1.LocalReport.SetParameters(netamts);
            ReportParameter netpaydays = new ReportParameter("netpaydays", netpayday);
            this.ReportViewer1.LocalReport.SetParameters(netpaydays);

            ReportParameter mont = new ReportParameter("month", mon);
            this.ReportViewer1.LocalReport.SetParameters(mont);

            ReportParameter earnamtt = new ReportParameter("earn", earnamt);
            this.ReportViewer1.LocalReport.SetParameters(earnamtt);

            ReportParameter deductamtt = new ReportParameter("deduct", DeductAmt);
            this.ReportViewer1.LocalReport.SetParameters(deductamtt);

            ReportParameter To = new ReportParameter("totalearn", totall);
            this.ReportViewer1.LocalReport.SetParameters(To);


            ReportParameter CompName = new ReportParameter("CompanyName", "Demo");
            this.ReportViewer1.LocalReport.SetParameters(CompName);

        }
    }
    public string retWord(int number)
    {

        if (number == 0) return "Zero";

        if (number == -2147483648) return "Minus Two Hundred and Fourteen Crore Seventy Four Lakh Eighty Three Thousand Six Hundred and Forty Eight";

        int[] num = new int[4];

        int first = 0;

        int u, h, t;

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        if (number < 0)
        {

            sb.Append("Minus ");

            number = -number;

        }

        string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };

        string[] words = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };

        string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };

        string[] words3 = { "Thousand ", "Lakh ", "Crore " };

        num[0] = number % 1000; // units

        num[1] = number / 1000;

        num[2] = number / 100000;

        num[1] = num[1] - 100 * num[2]; // thousands

        num[3] = number / 10000000; // crores

        num[2] = num[2] - 100 * num[3]; // lakhs

        for (int i = 3; i > 0; i--)
        {

            if (num[i] != 0)
            {

                first = i;

                break;

            }

        }

        for (int i = first; i >= 0; i--)
        {

            if (num[i] == 0) continue;

            u = num[i] % 10; // ones

            t = num[i] / 10;

            h = num[i] / 100; // hundreds

            t = t - 10 * h; // tens

            if (h > 0) sb.Append(words0[h] + "Hundred ");

            if (u > 0 || t > 0)
            {

                if (num.Length > 1)
                    if (h > 0 || i == 1) ;// sb.Append("and ");

                if (t == 0)

                    sb.Append(words0[u]);

                else if (t == 1)

                    sb.Append(words[u]);

                else

                    sb.Append(words2[t - 2] + words0[u]);

                if (i != 0) sb.Append(words3[i - 1]);

            }
        }


        return sb.ToString();

    }
}