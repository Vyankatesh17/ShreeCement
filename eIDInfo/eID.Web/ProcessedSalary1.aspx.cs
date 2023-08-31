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
using System.Threading;
using System.Globalization;

public partial class ProcessedSalary1 : System.Web.UI.Page
{
    static DataTable dt;
    static DataTable dt1;
    DataRow dr = null;
    DataRow dr2 = null;
    DataRow dr1 = null;
    decimal earnings, deductions, pf, pt;
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    DataTable Dt = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
                BindAllEmp();
                FillYear();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
        dt = new DataTable();
        dt1 = new DataTable();
        dt.Columns.Add(new DataColumn("EmployeeName", typeof(string)));
        dt.Columns.Add(new DataColumn("EmpId", typeof(string)));
        dt.Columns.Add(new DataColumn("Grade", typeof(string)));
        dt.Columns.Add(new DataColumn("Department", typeof(string)));
        dt.Columns.Add(new DataColumn("Designation", typeof(string)));
        dt.Columns.Add(new DataColumn("PAN", typeof(string)));
        dt.Columns.Add(new DataColumn("DOJ", typeof(string)));
        dt.Columns.Add(new DataColumn("SalaryMonth", typeof(string)));
        dt.Columns.Add(new DataColumn("WorkingDays", typeof(string)));
        dt.Columns.Add(new DataColumn("Netpaybledays", typeof(string)));
        dt.Columns.Add(new DataColumn("PFAccountNo", typeof(string)));
        dt.Columns.Add(new DataColumn("ESICAccountNo", typeof(string)));
        dt.Columns.Add(new DataColumn("SalaryMode", typeof(string)));
        dt.Columns.Add(new DataColumn("BankName", typeof(string)));
        dt.Columns.Add(new DataColumn("SalaryAccountNo", typeof(string)));
        //14
        dt.Columns.Add(new DataColumn("Basic", typeof(string)));
        dt.Columns.Add(new DataColumn("HRA", typeof(string)));
        dt.Columns.Add(new DataColumn("CONVEYANCEALLOWANCE", typeof(string)));
        dt.Columns.Add(new DataColumn("MEDICALALLOWANCE", typeof(string)));
        dt.Columns.Add(new DataColumn("specialALLOWANCE", typeof(string)));
        dt.Columns.Add(new DataColumn("TotalSalary", typeof(string)));
        dt.Columns.Add(new DataColumn("PFScale", typeof(string)));
        dt.Columns.Add(new DataColumn("PFAmount", typeof(string)));
        //22
        dt.Columns.Add(new DataColumn("ProfessionalTaxScale", typeof(string)));
        dt.Columns.Add(new DataColumn("ProfessionalTaxAmount", typeof(string)));
        dt.Columns.Add(new DataColumn("TDSScale", typeof(string)));
        dt.Columns.Add(new DataColumn("TDSAmount", typeof(string)));
        dt.Columns.Add(new DataColumn("ADVANCEScale", typeof(string)));
        dt.Columns.Add(new DataColumn("ADVANCEAmount", typeof(string)));
        dt.Columns.Add(new DataColumn("ABSENTScale", typeof(string)));
        dt.Columns.Add(new DataColumn("ABSENTAmount", typeof(string)));
        //30
        dt.Columns.Add(new DataColumn("MOBALLOWANCEScale", typeof(string)));
        dt.Columns.Add(new DataColumn("MOBALLOWANCEAmount", typeof(string)));
        dt.Columns.Add(new DataColumn("OtherDeductionScale", typeof(string)));
        dt.Columns.Add(new DataColumn("OtherDeductionAmount", typeof(string)));
        dt.Columns.Add(new DataColumn("DeductionTotalScale", typeof(string)));
        dt.Columns.Add(new DataColumn("DeductionTotalAmount", typeof(string)));
        dt.Columns.Add(new DataColumn("NetPay", typeof(string)));
        dt.Columns.Add(new DataColumn("Inwords", typeof(string)));
        dt.Columns.Add(new DataColumn("Absentdays", typeof(string)));
        dt.Columns.Add(new DataColumn("Grosssalary", typeof(string)));
        dt1.Columns.Add(new DataColumn("ABSENTScale", typeof(string)));
        dt1.Columns.Add(new DataColumn("ABSENTAmount", typeof(string)));
        DataColumn Component = Dt.Columns.Add("Componentid");
        DataColumn percentageValue = Dt.Columns.Add("ComponentName");
        DataColumn amount = Dt.Columns.Add("amount");
    }
    private void FillYear()
    {
        int i = int.Parse(DateTime.Now.AddYears(-1).Date.Year.ToString());
        DataTable dtadd = new DataTable();
        dtadd.Columns.Add("Year");
        for (int j = 0; j <= 10; j++)
        {
            DataRow dr = dtadd.NewRow();
            dr[0] = i.ToString();
            i++;
            dtadd.Rows.Add(dr);
        }
        ddlYear.DataSource = dtadd;
        ddlYear.DataTextField = "Year";
        ddlYear.DataValueField = "Year";
        ddlYear.DataBind();
        ddlYear.Items.Insert(0, "--Select--");
    }
    public void BindAllEmp()
    {
        //var EmpData = (from d in HR.EmployeeTBs
        //               join n in HR.SalaryProcessTBs on d.EmployeeId equals n.EmployeeID
        //               join k in HR.EmployeeSalarySettingsTBs on n.SalSettingID equals k.EmpSalaryid
        //               select new
        //               {
        //                   Name = d.FName + " " + d.MName + " " + d.Lname,
        //                   d.FName,
        //                   d.Lname,k.netpay,
        //                   d.EmployeeId,
        //                   GrossSalary = n.GrossSalary,
        //                   netpay1 = n.NetSlary,
        //                   n.Month,
        //                   MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(n.Month)),
        //                   //n.MonthName,
        //                    n.Year,
        //                    n.SalProcessId
        //                });
        // n  chnages 24 dec
        if (Session["UserType"].ToString() != "Admin")
        {
            var EmpData = (from d in HR.EmployeeTBs
                           join n in HR.SalaryProcessTBs on d.EmployeeId equals n.EmployeeID
                           where d.EmployeeId == Convert.ToInt32(Session["UserId"].ToString())
                           select new
                           {
                               Name = d.FName + " " + d.MName + " " + d.Lname,
                               d.FName,
                               d.Lname,
                               netpay = n.NetSlary,
                               d.EmployeeId,
                               GrossSalary = n.GrossSalary,
                               netpay1 = n.NetSlary,
                               n.Month,
                               MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(n.Month)),
                               //n.MonthName,
                               n.Year,
                               n.SalProcessId
                           });
            if (!string.IsNullOrEmpty(txtLastNameSearch.Text))
            {
                EmpData = EmpData.Where(d => d.Lname.Contains(txtLastNameSearch.Text));
            }
            if (!string.IsNullOrEmpty(txtFirstNameSearch.Text))
            {
                EmpData = EmpData.Where(d => d.FName.Contains(txtFirstNameSearch.Text));
            }
            if (!string.IsNullOrEmpty(txtempidsearch.Text))
            {
                EmpData = EmpData.Where(d => d.EmployeeId.Equals(txtempidsearch.Text));
            }
            if (ddlYear.SelectedIndex > 0)
            {
                EmpData = EmpData.Where(d => d.Year.Equals(ddlYear.SelectedItem.Text));
            }
            if (ddlMonths.SelectedIndex > 0)
            {
                EmpData = EmpData.Where(d => d.Month == (ddlMonths.SelectedIndex.ToString()));
            }
            if (EmpData.Count() > 0)
            {
                grd_Emp.DataSource = EmpData;
                grd_Emp.DataBind();
            }
            else
            {
                grd_Emp.DataSource = null;
                grd_Emp.DataBind();
            }
        }
        else
        {
            var EmpData = (from d in HR.EmployeeTBs
                           join n in HR.SalaryProcessTBs on d.EmployeeId equals n.EmployeeID
                          // where d.EmployeeId == Convert.ToInt32(Session["UserId"].ToString())
                           select new
                           {
                               Name = d.FName + " " + d.MName + " " + d.Lname,
                               d.FName,
                               d.Lname,
                               netpay = n.NetSlary,
                               d.EmployeeId,
                               d.EmployeeNo,
                               GrossSalary = n.GrossSalary,
                               netpay1 = n.NetSlary,
                               n.Month,
                               MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(n.Month)),
                               //n.MonthName,
                               n.Year,
                               n.SalProcessId
                           });
            if (!string.IsNullOrEmpty(txtLastNameSearch.Text))
            {
                EmpData = EmpData.Where(d => d.Lname.Contains(txtLastNameSearch.Text));
            }
            if (!string.IsNullOrEmpty(txtFirstNameSearch.Text))
            {
                EmpData = EmpData.Where(d => d.FName.Contains(txtFirstNameSearch.Text));
            }
            if (!string.IsNullOrEmpty(txtempidsearch.Text))
            {
                EmpData = EmpData.Where(d => ("100" + d.EmployeeId).Contains(txtempidsearch.Text));
            }
            if (ddlYear.SelectedIndex > 0)
            {
                EmpData = EmpData.Where(d => d.Year.Equals(ddlYear.SelectedItem.Text));
            }
            if (ddlMonths.SelectedIndex > 0)
            {
                EmpData = EmpData.Where(d => d.Month == (ddlMonths.SelectedIndex.ToString()));
            }
            if (EmpData.Count() > 0)
            {
                grd_Emp.DataSource = EmpData;
                grd_Emp.DataBind();
            }
            else
            {
                grd_Emp.DataSource = null;
                grd_Emp.DataBind();
            }
        }
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        BindAllEmp();
        //txtFirstNameSearch.Text = null;
        //txtLastNameSearch.Text = null;
        //txtempidsearch.Text = null;
        //txtYear.Text = null;
        //ddlMonths.SelectedIndex = 0;
        //txtempidsearch.Text = null;
    }
    protected void Edit_Click(object sender, EventArgs e)
    {
        ImageButton Lnk = (ImageButton)sender;
        string Processid = Lnk.CommandArgument;
        MultiView1.ActiveViewIndex = 0;
        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        var saldetails = (from n in HR.SalaryProcessTBs
                          join k in HR.EmployeeSalarySettingsTBs on n.SalSettingID equals k.EmpSalaryid
                          where n.SalProcessId == int.Parse(Processid)
                          select new
                          {
                              n.EmployeeID,
                              n.BankName,
                              n.ESICAccountNo,
                              n.GrossSalary,
                              n.Month,
                              n.Netpaybledays,
                              k.netpay,
                              n.PFAccountNo,
                              n.SalaryAccountNo,
                              n.SalaryMode,
                              n.WorkingDays,
                              n.Year
                          });
        string strMonthName = mfi.GetMonthName(int.Parse(saldetails.First().Month)).ToString();
        lblempcode.Text = saldetails.First().EmployeeID.ToString();
        lblbankname.Text = saldetails.First().BankName;
        lblaccountno.Text = saldetails.First().SalaryAccountNo;
        lblesino.Text = saldetails.First().ESICAccountNo;
        lblmode.Text = saldetails.First().SalaryMode;
        lblpfacc.Text = saldetails.First().PFAccountNo;
        lblmonth.Text = strMonthName + "-" + saldetails.First().Year;
        lblgrosssalary.Text = saldetails.First().GrossSalary;
        lblnetpay.Text = saldetails.First().netpay;
        var workingData = (from m in HR.MonthlyAttendenceTBs
                           where m.Employee_Id == saldetails.First().EmployeeID
                           orderby m.attendence_Id descending
                           select new
                           {
                               m.WorkingDays,
                               m.PresentDays,
                               m.AbsentDays
                           }).First();
        lblworkingday.Text = workingData.WorkingDays.ToString();
        lblnetpaybleday.Text = workingData.PresentDays.ToString();
        lbllopdays.Text = workingData.AbsentDays.ToString();
        var emp = (from m in HR.EmployeeTBs
                   where m.EmployeeId == saldetails.First().EmployeeID
                   select new
                   {
                       m.EmployeeId,
                       m.FName,
                       m.MName,
                       m.Lname,
                       m.Grade,
                       m.DOJ,
                       m.DeptId,
                       m.DesgId,
                       m.NetSalary,
                       //m.CompanyInfoTB.CompanyName
                   }).First();
        //Session["CompanyName"] = emp.CompanyName;
        lblempname.Text = emp.FName + " " + emp.MName + " " + emp.Lname;
        lblgrade.Text = emp.Grade;
        lbldoj.Text = emp.DOJ.Value.ToString("MM/dd/yyyy");
        var deptname = (from m in HR.MasterDeptTBs
                        where m.DeptID == emp.DeptId
                        select m.DeptName).First();
        var desgname = (from m in HR.MasterDesgTBs
                        where m.DesigID == emp.DesgId
                        select m.DesigName).First();
        lbldept.Text = deptname.ToString();
        lbldesg.Text = desgname.ToString();
        //var Salarydetails = (from m in HR.SalaryProcessTBs
        //                     join n in HR.SalaryProcessDetailsTBs on m.SalProcessId equals n.EmpSalaryProcessid
        //                     where m.SalProcessId == int.Parse(Processid)
        //                     select n);
        //int empSalId = Convert.ToInt32(Salarydetails.First().EmpSalaryProcessid);
        DataTable Result1 = g.ReturnData("select EmpSalaryProcessid,EmployeeId,SalaryDate,Componentid,ComponentType,isnull(amount,0) as amount from  SalaryProcessDetailsTB SalaryDetail  where SalaryDetail.EmployeeId='" + saldetails.First().EmployeeID + "' ");
        DataTable Result = g.ReturnData("SELECT SalaryProcessTB.GrossSalary, SalaryProcessTB.NetSlary, SalaryDetail.Componentid, SalaryDetail.ComponentType,isnull(SalaryDetail.amount,0) as amount , SalaryDetail.Componentid ComponentName FROM   SalaryProcessTB INNER JOIN SalaryProcessDetailsTB  SalaryDetail ON SalaryProcessTB.SalProcessId = SalaryDetail.EmpSalaryProcessid  where SalaryProcessTB.SalProcessId=" + int.Parse(Processid) + "");
        DataSet Result2 = g.ReturnData1("SELECT SalaryProcessTB.GrossSalary, SalaryProcessTB.NetSlary, SalaryDetail.Componentid, SalaryDetail.ComponentType,isnull(SalaryDetail.amount,0) as amount ,SalaryDetail.Componentid ComponentName FROM   SalaryProcessTB INNER JOIN SalaryProcessDetailsTB SalaryDetail ON SalaryProcessTB.SalProcessId = SalaryDetail.EmpSalaryProcessid  where SalaryProcessTB.SalProcessId=" + int.Parse(Processid) + " and  SalaryDetail.ComponentType='Earning'");
        DataSet Result3 = g.ReturnData1("SELECT SalaryProcessTB.GrossSalary, SalaryProcessTB.NetSlary, SalaryDetail.Componentid, SalaryDetail.ComponentType,isnull(SalaryDetail.amount,0) as amount ,SalaryDetail.Componentid ComponentName FROM   SalaryProcessTB INNER JOIN SalaryProcessDetailsTB SalaryDetail ON SalaryProcessTB.SalProcessId = SalaryDetail.EmpSalaryProcessid   where SalaryProcessTB.SalProcessId=" + int.Parse(Processid) + " and  SalaryDetail.ComponentType='Deduction'");
        Session["EMPSALID"] = Processid;
        grd_Earning.DataSource = Result2;
        grd_Earning.DataBind();
        GridViewDeduction.DataSource = Result3;
        GridViewDeduction.DataBind();
        for (int i = 0; i < Result.Rows.Count; i++)
        {
            if (Result.Rows[i]["ComponentName"].ToString() == "Basic")
            {
                lblbasic.Text = Result.Rows[i]["amount"].ToString();
            }
            else if (Result.Rows[i]["ComponentName"].ToString() == "H.R.A.")
            {
                lblhra.Text = Result.Rows[i]["amount"].ToString();
            }
            else if (Result.Rows[i]["ComponentName"].ToString() == "Conveyance Allowance")
            {
                lblCONVEYANCE.Text = Result.Rows[i]["amount"].ToString();
            }
            else if (Result.Rows[i]["ComponentName"].ToString() == "Medical Allownce")
            {
                lblMEDICAL.Text = Result.Rows[i]["amount"].ToString();
            }
            else if (Result.Rows[i]["ComponentName"].ToString() == "Special Allowance")
            {
                lblspecial.Text = Result.Rows[i]["amount"].ToString();
            }
            else if (Result.Rows[i]["ComponentName"].ToString() == "Professional Tax")
            {
                lblpt1.Text = Result.Rows[i]["amount"].ToString();
                lblpt2.Text = Result.Rows[i]["amount"].ToString();
                pt = decimal.Parse(Result.Rows[i]["amount"].ToString());
            }
            else if (Result.Rows[i]["ComponentName"].ToString() == "P.F.")
            {
                lblpf1.Text = Result.Rows[i]["amount"].ToString();
                lblpf2.Text = Result.Rows[i]["amount"].ToString();
                pf = decimal.Parse(Result.Rows[i]["amount"].ToString());
            }
            if (Result.Rows[i]["ComponentType"].ToString() == "Earning")
            {
                earnings = earnings + (decimal.Parse(Result.Rows[i]["amount"].ToString()));
            }
            else
            {
                deductions = deductions + (decimal.Parse(Result.Rows[i]["amount"].ToString()));
            }
        }
        int daycount = System.DateTime.DaysInMonth(int.Parse(saldetails.First().Year), int.Parse(saldetails.First().Month));
        var CurrentSlot = (from m in HR.BeforeSalProcessTBs
                           where m.empid == Convert.ToInt32(emp.EmployeeId) && m.month == saldetails.First().Month && m.year == saldetails.First().Year
                           select m).ToList();
        // int Leavdeductionamt = (int.Parse(saldetails.First().GrossSalary) / daycount) * Convert.ToInt32(CurrentSlot.First().absentdays);
        // n changes 05 jan
        decimal Leavdeductionamt = (Convert.ToDecimal(saldetails.First().GrossSalary) / 30) * Convert.ToInt32(CurrentSlot.First().absentdays);
        //Absent Days Deductions
        // int Leavdeductionamt = (int.Parse(saldetails.First().GrossSalary) / days) * decductiondays;
        decimal a = pt + pf;
        a = deductions - a;
        deductions = deductions + Leavdeductionamt;
        lblearningtotal1.Text = earnings.ToString();
        lbldeductiontotal1.Text = deductions.ToString();
        lbldeductiontotal2.Text = deductions.ToString();
        lblabsentscal.Text = Leavdeductionamt.ToString();
        lblabsentamt.Text = Leavdeductionamt.ToString();
        lblOther1.Text = a.ToString();
        lblOther2.Text = a.ToString();
        //25NOV
        //var netpayamount = (earnings - deductions).ToString();
        //lblnetpay.Text = Convert.ToString(netpayamount);
        decimal netsalamount = Convert.ToDecimal(lblnetpay.Text);
        int amount = Convert.ToInt32(netsalamount);
        lblwords.Text = retWord(amount) + "Rupees Only";
        dr = dt.NewRow();
        dr["EmployeeName"] = lblempname.Text;
        dr["EmpId"] = lblempcode.Text;
        dr["Grade"] = lblgrade.Text;
        dr["Department"] = lbldept.Text;
        dr["Designation"] = lbldesg.Text;
        dr["PAN"] = lblpan.Text;
        dr["DOJ"] = lbldoj.Text;
        dr["SalaryMonth"] = lblmonth.Text;
        dr["WorkingDays"] = lblworkingday.Text;
        dr["Netpaybledays"] = lblnetpaybleday.Text;
        dr["PFAccountNo"] = lblpfacc.Text;
        dr["ESICAccountNo"] = lblesino.Text;
        dr["SalaryMode"] = lblmode.Text;
        dr["BankName"] = lblbankname.Text;
        dr["SalaryAccountNo"] = lblaccountno.Text;
        // Earnings
        dr["Basic"] = lblbasic.Text;
        dr["HRA"] = lblhra.Text;
        dr["CONVEYANCEALLOWANCE"] = lblCONVEYANCE.Text;
        dr["MEDICALALLOWANCE"] = lblMEDICAL.Text;
        dr["specialALLOWANCE"] = lblspecial.Text;
        dr["TotalSalary"] = lblearningtotal1.Text;
        // Deduction Details
        dr["PFScale"] = lblpf2.Text;
        dr["PFAmount"] = lblpf1.Text;
        dr["ProfessionalTaxScale"] = lblpt1.Text;
        dr["ProfessionalTaxAmount"] = lblpt2.Text;
        dr["TDSScale"] = Label8.Text;
        dr["TDSAmount"] = Label3.Text;
        dr["ADVANCEScale"] = Label9.Text;
        dr["ADVANCEAmount"] = Label4.Text;
        dr["ABSENTScale"] = lblabsentscal.Text;
        dr["ABSENTAmount"] = lblabsentamt.Text;
        dr["MOBALLOWANCEScale"] = Label11.Text;
        dr["MOBALLOWANCEAmount"] = Label5.Text;
        dr["OtherDeductionScale"] = lblOther1.Text;
        dr["OtherDeductionAmount"] = lblOther2.Text;
        dr["DeductionTotalScale"] = lbldeductiontotal1.Text;
        dr["DeductionTotalAmount"] = lbldeductiontotal2.Text;
        dr["NetPay"] = lblnetpay.Text;
        dr["Inwords"] = lblwords.Text;
        dr["Absentdays"] = lbllopdays.Text;
        dr["Grosssalary"] = lblgrosssalary.Text;
        dt.Rows.Add(dr);
        Session["ab"] = dt;
        // Session["id"] = lbledit.Text;
        dr2 = dt1.NewRow();
        dr2["ABSENTScale"] = lblabsentscal.Text;
        dr2["ABSENTAmount"] = lblabsentamt.Text;
        dt1.Rows.Add(dr2);
        Session["absent"] = dt1;
        //Session["netpaydays"] = lblnetpaybleday.Text;
        Session["netpaydays"] = Convert.ToDecimal(workingData.WorkingDays)-Convert.ToDecimal(workingData.AbsentDays);
        Session["idd"] = Lnk.CommandArgument;
        //   Session["idd"] = saldetails.First().EmployeeID.ToString();
        Session["amt"] = lblwords.Text;
        Session["netamt"] = lblnetpay.Text;
        string ss = "window.open('SalarySlipViewer.aspx?Type=All','mywindow','width=1000,height=700,left=200,top=1,screenX=100,screenY=100,toolbar=no,location=no,directories=no,status=yes,menubar=no,scrollbars=yes,copyhistory=yes,resizable=no')";
        string script = "<script language='javascript'>" + ss + "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUpWindow", script, false);
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
                if (num.Count() > 1)
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
    private string GetNumberOfSundays(Int32 Month, Int32 Year)
    {
        DateTime StartDate = Convert.ToDateTime(Month.ToString() + "/01/" + Year.ToString());
        Int32 iDays = DateTime.DaysInMonth(Year, Month);
        DateTime EndDate = StartDate.AddDays(iDays - 1);
        Int32 Count = 0;
        Int32 SundayCount = 0;
        while (StartDate.DayOfWeek != DayOfWeek.Sunday)
        {
            StartDate = StartDate.AddDays(1);
        }
        SundayCount = 1;
        StartDate = StartDate.AddDays(7);
        while (StartDate <= EndDate)
        {
            SundayCount += 1; StartDate = StartDate.AddDays(7);
        }
        return SundayCount.ToString();
    }
    protected void btnPDF_Click(object sender, EventArgs e)
    {
        string ss = "window.open('PayslipCrystalViewer.aspx?Type=All','mywindow','width=1000,height=700,left=200,top=1,screenX=100,screenY=100,toolbar=no,location=no,directories=no,status=yes,menubar=no,scrollbars=yes,copyhistory=yes,resizable=no')";
        string script = "<script language='javascript'>" + ss + "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUpWindow", script, false);
        #region
        //using (StringWriter sw = new StringWriter())
        //{
        //    using (HtmlTextWriter hw = new HtmlTextWriter(sw))
        //    {
        //        //To Export all pages
        //        //  GridView1.AllowPaging = false;
        //        //getProjectedSales();
        //        tblpayslip.RenderControl(hw);
        //        //tbl1.RenderControl(hw);
        //        tblpayslip.Style.Add("width", "15%");
        //        tblpayslip.Style.Add("font-size", "10px");
        //        tblpayslip.Style.Add("text-decoration", "none");
        //        tblpayslip.Style.Add("font-family", "Arial, Helvetica, sans-serif;");
        //        tblpayslip.Style.Add("font-size", "8px");
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
        #endregion
    }
    protected void btnExcel_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.AddHeader("content-disposition", "attachment;filename=FileName.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.xls";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        tblpayslip.RenderControl(htmlWrite);
        Response.Write(stringWrite.ToString());
        Response.End();
    }
    public override void VerifyRenderingInServerForm(Control control)
    {
        /* Verifies that the control is rendered */
    }
    protected void ddlMonths_SelectedIndexChanged(object sender, EventArgs e)
    {
    }
    protected void grd_Emp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Emp.PageIndex = e.NewPageIndex;
        grd_Emp.DataBind();
        BindAllEmp();
    }
}