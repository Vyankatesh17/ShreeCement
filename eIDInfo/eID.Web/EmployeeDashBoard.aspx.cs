using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmployeeDashBoard : System.Web.UI.Page
{
    HrPortalDtaClassDataContext EX = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    int year = 0;
    public int NumberOfDays = 0;
    DataTable dtd = new DataTable();
    #region Declaration for salary slip
    static DataTable dt;
    static DataTable dt1;
    DataRow dr = null;
    DataRow dr2 = null;
    DataRow dr1 = null;
    decimal earnings, deductions, pf, pt;
    DataTable Dt = new DataTable();
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                BindGrid();
                ddmonth.Items.FindByValue(Convert.ToString(DateTime.Now.Month)).Selected = true;
                year = DateTime.Now.Year;
                ddyear.SelectedIndex = -1;
                ddyear.Items.FindByValue(System.DateTime.Now.Year.ToString()).Selected = true;
                BindAllEmp();
                FillYear();
                EmpAttReport();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
        #region code for salary slip
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
        #endregion
    }
    #region code for salary slip
    private void FillYear()
    {
        int i = int.Parse(DateTime.Now.AddYears(0).Date.Year.ToString());
        DataTable DTS = new DataTable();

        DTS = new DataTable();
        DataColumn yearid = DTS.Columns.Add("yearid");
        DataColumn yearname = DTS.Columns.Add("yearname");

        for (int j = 0; j <= 50; j++)
        {
            DataRow dr = DTS.NewRow();
            dr[0] = i.ToString();
            dr[1] = i.ToString();
            DTS.Rows.Add(dr);
            //ddlYear.Items.Add(new System.Web.UI.WebControls.ListItem(i.ToString(), string.Empty));
            i++;
        }
        ddlYear.DataSource = DTS;
        ddlYear.DataTextField = "yearname";
        ddlYear.DataValueField = "yearid";
        ddlYear.DataBind();
        ddlYear.Items.Insert(0, "--Select--");
        ddlYear.SelectedIndex = 0;
    }
    public void BindAllEmp()
    {
        var EmpData = (from d in EX.EmployeeTBs
                       join n in EX.SalaryProcessTBs on d.EmployeeId equals n.EmployeeID
                       join k in EX.EmployeeSalarySettingsTBs on n.SalSettingID equals k.EmpSalaryid
                       where d.EmployeeId == Convert.ToInt32(Session["UserId"].ToString())
                       select new
                       {
                           Name = d.FName + " " + d.MName + " " + d.Lname,
                           d.FName,
                           d.Lname,
                           k.netpay,
                           d.EmployeeId,
                           GrossSalary = n.GrossSalary,
                           netpay1 = n.NetSlary,
                           n.Month,
                           MonthName = CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(Convert.ToInt32(n.Month)),
                           //n.MonthName,
                           n.Year,
                           n.SalProcessId
                       });

        if (ddlMonths.SelectedIndex == 0 && ddlYear.SelectedIndex > 0)
        {
            EmpData = EmpData.Where(d => d.Year.Equals(ddlYear.SelectedItem.Text));
        }
        if (ddlMonths.SelectedIndex != 0 && ddlYear.SelectedIndex == 0)
        {
            EmpData = EmpData.Where(d => d.Month == (ddlMonths.SelectedValue));
        }
        if (ddlMonths.SelectedIndex != 0 && ddlYear.SelectedIndex != 0)
        {
            EmpData = EmpData.Where(d => d.Month == (ddlMonths.SelectedValue) && d.Year.Equals(ddlYear.SelectedItem.Text));
        }
        //if (ddlYear.SelectedIndex)
        //{

        //}
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
    protected void btnSearchSalary_Click(object sender, EventArgs e)
    {
        BindAllEmp();
    }
    protected void Edit_Click(object sender, EventArgs e)
    {

        ImageButton Lnk = (ImageButton)sender;
        string Processid = Lnk.CommandArgument;

        System.Globalization.DateTimeFormatInfo mfi = new System.Globalization.DateTimeFormatInfo();
        var saldetails = (from n in EX.SalaryProcessTBs
                          join k in EX.EmployeeSalarySettingsTBs on n.SalSettingID equals k.EmpSalaryid
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

        var workingData = (from m in EX.MonthlyAttendenceTBs
                           where m.Employee_Id == saldetails.First().EmployeeID
                           select new
                           {
                               m.WorkingDays,
                               m.PresentDays,
                               m.AbsentDays

                           }).First();
        lblworkingday.Text = workingData.WorkingDays.ToString();
        lblnetpaybleday.Text = workingData.PresentDays.ToString();
        lbllopdays.Text = workingData.AbsentDays.ToString();

        var emp = (from m in EX.EmployeeTBs
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
                       m.NetSalary
                   }).First();
        lblempname.Text = emp.FName + " " + emp.MName + " " + emp.Lname;
        lblgrade.Text = emp.Grade;
        lbldoj.Text = emp.DOJ.Value.ToString("MM/dd/yyyy");
        var deptname = (from m in EX.MasterDeptTBs
                        where m.DeptID == emp.DeptId
                        select m.DeptName).First();
        var desgname = (from m in EX.MasterDesgTBs
                        where m.DesigID == emp.DesgId
                        select m.DesigName).First();
        lbldept.Text = deptname.ToString();
        lbldesg.Text = desgname.ToString();
        var Salarydetails = (from m in EX.SalaryProcessTBs
                             join n in EX.SalaryProcessDetailsTBs on m.SalProcessId equals n.EmpSalaryProcessid
                             where m.SalProcessId == int.Parse(Processid)
                             select n);

        int empSalId = Convert.ToInt32(Salarydetails.First().EmpSalaryProcessid);

        DataTable Result1 = g.ReturnData("select EmpSalaryProcessid,EmployeeId,SalaryDate,Componentid,ComponentType,amount from  SalaryProcessDetailsTB SalaryDetail  where SalaryDetail.EmployeeId='" + saldetails.First().EmployeeID + "' ");



        DataTable Result = g.ReturnData("SELECT SalaryProcessTB.GrossSalary, SalaryProcessTB.NetSlary, SalaryDetail.Componentid, SalaryDetail.ComponentType,SalaryDetail.amount, SalaryDetail.Componentid ComponentName FROM   SalaryProcessTB INNER JOIN SalaryProcessDetailsTB  SalaryDetail ON SalaryProcessTB.SalProcessId = SalaryDetail.EmpSalaryProcessid  where SalaryProcessTB.SalProcessId=" + int.Parse(Processid) + "");

        DataSet Result2 = g.ReturnData1("SELECT SalaryProcessTB.GrossSalary, SalaryProcessTB.NetSlary, SalaryDetail.Componentid, SalaryDetail.ComponentType,SalaryDetail.amount,SalaryDetail.Componentid ComponentName FROM   SalaryProcessTB INNER JOIN SalaryProcessDetailsTB SalaryDetail ON SalaryProcessTB.SalProcessId = SalaryDetail.EmpSalaryProcessid  where SalaryProcessTB.SalProcessId=" + int.Parse(Processid) + " and  SalaryDetail.ComponentType='Earning'");

        DataSet Result3 = g.ReturnData1("SELECT SalaryProcessTB.GrossSalary, SalaryProcessTB.NetSlary, SalaryDetail.Componentid, SalaryDetail.ComponentType,SalaryDetail.amount,SalaryDetail.Componentid ComponentName FROM   SalaryProcessTB INNER JOIN SalaryProcessDetailsTB SalaryDetail ON SalaryProcessTB.SalProcessId = SalaryDetail.EmpSalaryProcessid   where SalaryProcessTB.SalProcessId=" + int.Parse(Processid) + " and  SalaryDetail.ComponentType='Deduction'");

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
        var CurrentSlot = (from m in EX.BeforeSalProcessTBs
                           where m.empid == Convert.ToInt32(emp.EmployeeId) && m.month == saldetails.First().Month && m.year == saldetails.First().Year
                           select m).ToList();


        int Leavdeductionamt = (int.Parse(saldetails.First().GrossSalary) / daycount) * Convert.ToInt32(CurrentSlot.First().absentdays);

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
        Session["netpaydays"] = lblnetpaybleday.Text;

        Session["idd"] = Lnk.CommandArgument;
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
    #endregion
    #region code for employee attendance
    private void DispLAYCOUNT()
    {
        int k = System.DateTime.DaysInMonth(int.Parse(DateTime.Now.Year.ToString()), (ddmonth.SelectedIndex));
        for (int i = 0; i < gv.Rows.Count; i++)
        {
            int cntp = 0;
            int cnts = 0;
            int cntL = 0;
            int cntA = 0;
            int cntH = 0;
            for (int J = 1; J < gv.Rows[i].Cells.Count - 3; J++)
            {
                if (gv.Rows[i].Cells[J].Text == "P")
                {
                    cntp++;

                }
                if (gv.Rows[i].Cells[J].Text == "A")
                {
                    cntA++;

                }
                if (gv.Rows[i].Cells[J].Text == "S")
                {
                    cnts++;

                }

                if (gv.Rows[i].Cells[J].Text == "WO")
                {
                    cnts++;

                }

                if (gv.Rows[i].Cells[J].Text == "L")
                {
                    cntL++;

                }
                if (gv.Rows[i].Cells[J].Text == "H")
                {
                    cntH++;

                }
                lblPDay.Text = cntp.ToString();
                lblADay.Text = cntA.ToString();
                lblWO.Text = cnts.ToString();
                lblHoliday.Text = cntH.ToString();
                lblLeave.Text = cntL.ToString();



            }
        }

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        EmpAttReport();
    }

    private void EmpAttReport()
    {
        #region

        
        panalcount.Visible = true;
        var companyiddata = from dt in EX.EmployeeTBs
                            where dt.EmployeeId == Convert.ToInt32(Session["UserId"].ToString())
                            select new { dt.CompanyId };
        foreach (var item in companyiddata)
        {
            lblcompaniId.Text = Convert.ToString(item.CompanyId);
        }

        string date = "";
        string year = ddyear.SelectedValue;
        date = ddmonth.SelectedValue + '/' + 1 + '/' + year;
        #region
        dtd = new DataTable();
        int k = System.DateTime.DaysInMonth(int.Parse(DateTime.Now.Year.ToString()), (ddmonth.SelectedIndex));
        #region
        for (int j = 0; j < k + 1; j++)
        {//Columns
            DataColumn drmon = new DataColumn();
            if (j == 0)
            {
                drmon.ColumnName = "Days";
            }
            else
            {
                drmon.ColumnName = j.ToString();
            }
            dtd.Columns.Add(drmon.ToString());
        }
        DataSet dsEmp = g.ReturnData1(@"SELECT DISTINCT [t1].[value] AS [Name], [t1].[EmployeeId] FROM (SELECT [t0].[FName] +' '+ [t0].[Mname] +' ' + [t0].[Lname] AS [value], [t0].[EmployeeId], [t0].[RelivingStatus], [t0].[CompanyId]
               FROM [dbo].[EmployeeTB] AS [t0] ) AS [t1] WHERE ([t1].[RelivingStatus] IS NULL)  and t1.CompanyId='" + Convert.ToInt32(lblcompaniId.Text) + "'  and t1.EmployeeId='" + Convert.ToInt32(Session["UserId"].ToString()) + "'");
        if (dsEmp.Tables[0].Rows.Count > 0)
        {


            try
            {
                DataRow drEmp = dtd.NewRow();
                drEmp[0] = dsEmp.Tables[0].Rows[0]["Name"];

                dtd.Rows.Add(drEmp[0].ToString());

            }
            catch (Exception)
            {

                throw;
            }


        }
        gv.DataSource = dtd;
        gv.DataBind();
        #endregion
        try
        {

            for (int j = 0; j < dtd.Rows.Count; j++)
            {
                int countp = 0;

                gv.Rows[0].Cells[0].Width = 50;
                for (int kk = 1; kk < gv.Rows[j].Cells.Count; kk++)
                {
                    int countL = 0;
                    int CountS = 0;
                    int countweakOf = 0;
                    DataSet dsP = g.ReturnData1("select * from LogRecordsDetails left outer join EmployeeTB on LogRecordsDetails.Employee_id= EmployeeTB.EmployeeId where DATEPART(day, Log_Date_Time)='" + kk + "'  and  DATEPART(Month, Log_Date_Time)='" + ddmonth.SelectedValue + "' and DATEPART(Year, Log_Date_Time)='" + year + "'   and status is null  and EmployeeTB.CompanyId='" + Convert.ToInt32(lblcompaniId.Text) + "' and LogRecordsDetails.Employee_id='" + Convert.ToInt32(Session["UserId"].ToString()) + "'");
                    if (dsP.Tables[0].Rows.Count > 0)
                    {//Present
                        gv.Rows[j].Cells[kk].Text = "P";
                        gv.Rows[j].Cells[kk].BackColor = Color.GreenYellow;


                    }
                    else
                    {
                        DataSet dsMP = g.ReturnData1("select * from LogRecordsDetails left outer join EmployeeTB on LogRecordsDetails.Employee_id= EmployeeTB.EmployeeId where DATEPART(day, Log_Date_Time)='" + kk + "'  and status='MP' and  DATEPART(Month, Log_Date_Time)='" + ddmonth.SelectedValue + "'  and DATEPART(Year, Log_Date_Time)='" + year + "'  and EmployeeTB.CompanyId='" + Convert.ToInt32(lblcompaniId.Text) + "' and LogRecordsDetails.Employee_id='" + Convert.ToInt32(Session["UserId"].ToString()) + "'");
                        if (dsMP.Tables[0].Rows.Count > 0)
                        {//MP
                            gv.Rows[j].Cells[kk].Text = "MP";
                            gv.Rows[j].Cells[kk].BackColor = Color.GreenYellow;
                            gv.Rows[j].Cells[kk].ForeColor = Color.Blue;
                            gv.Rows[j].Cells[kk].Width = 10;

                        }
                        DataSet dsMH = g.ReturnData1("select * from LogRecordsDetails left outer join EmployeeTB on LogRecordsDetails.Employee_id= EmployeeTB.EmployeeId where DATEPART(day, Log_Date_Time)='" + kk + "' and  DATEPART(Month, Log_Date_Time)='" + ddmonth.SelectedValue + "'  and status='MH' and DATEPART(Year, Log_Date_Time)='" + year + "'  and EmployeeTB.CompanyId='" + Convert.ToInt32(lblcompaniId.Text) + "' and LogRecordsDetails.Employee_id='" + Convert.ToInt32(Session["UserId"].ToString()) + "'");
                        if (dsMH.Tables[0].Rows.Count > 0)
                        {//MH
                            gv.Rows[j].Cells[kk].Text = "MH";
                            gv.Rows[j].Cells[kk].BackColor = Color.Yellow;
                            gv.Rows[j].Cells[kk].Width = 100;
                            gv.Rows[j].Cells[kk].Width = 10;
                        }
                        DataSet dsMA = g.ReturnData1("select * from LogRecordsDetails left outer join EmployeeTB on LogRecordsDetails.Employee_id= EmployeeTB.EmployeeId where DATEPART(day, Log_Date_Time)='" + kk + "' and  DATEPART(Month, Log_Date_Time)='" + ddmonth.SelectedValue + "'  and status='MA' and DATEPART(Year, Log_Date_Time)='" + year + "'  and EmployeeTB.CompanyId='" + Convert.ToInt32(lblcompaniId.Text) + "' and LogRecordsDetails.Employee_id='" + Convert.ToInt32(Session["UserId"].ToString()) + "'");
                        if (dsMA.Tables[0].Rows.Count > 0)
                        {//MA
                            gv.Rows[j].Cells[kk].Text = "MA";
                            gv.Rows[j].Cells[kk].BackColor = Color.Turquoise;
                            gv.Rows[j].Cells[kk].Width = 100;
                            gv.Rows[j].Cells[kk].Width = 10;
                        }

                        DataSet dsHolidays = g.ReturnData1("select CONVERT(varchar,Date,101) from HoliDaysMaster where datepart(month,Date)='" + ddmonth.SelectedValue + "' and datepart(Year,Date)='" + year + "' and datepart(day,Date)='" + kk + "'");
                        if (dsHolidays.Tables[0].Rows.Count > 0)
                        {//HOLIDAYS
                            gv.Rows[j].Cells[kk].Text = "H";
                            gv.Rows[j].Cells[kk].ToolTip = "Holiday";
                            gv.Rows[j].Cells[kk].BackColor = Color.Orange;
                            gv.Rows[j].Cells[kk].Width = 10;


                        }

                        DataSet dsFrm = g.ReturnData1(@"select  StartDate,EndDate  from tblLeaveApplication left outer join EmployeeTB on tblLeaveApplication.employeeID= EmployeeTB.EmployeeId where DATEPART(month, tblLeaveApplication.StartDate)='" + ddmonth.SelectedValue + "' and DATEPART(Year, tblLeaveApplication.StartDate)='" + year + "'  and DATEPART(month, tblLeaveApplication.EndDate)='" + ddmonth.SelectedValue + "' and DATEPART(Year, tblLeaveApplication.EndDate)='" + year + "'  and EmployeeTB.CompanyId='" + Convert.ToInt32(lblcompaniId.Text) + "' and tblLeaveApplication.employeeID='" + Convert.ToInt32(Session["UserId"].ToString()) + "'");
                        if (dsFrm.Tables[0].Rows.Count > 0)
                        {//LEAVES
                            DataSet dss = g.ReturnData1(@"DECLARE @Start DATETIME, @End DATETIME SELECT @Start='" + dsFrm.Tables[0].Rows[j]["StartDate"].ToString() + "' , @End = '" + dsFrm.Tables[0].Rows[j]["EndDate"].ToString() + "' ;WITH DateList AS (SELECT @Start [Date] UNION ALL SELECT [Date] +1 FROM DateList WHERE [Date] <@End) SELECT CONVERT(VARCHAR(Max), [Date],120) [mon] FROM DateList");
                            for (int h = 0; h < dss.Tables[0].Rows.Count; h++)
                            {
                                DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dss.Tables[0].Rows[h][0] + "')");
                                int days = 0;
                                days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                gv.Rows[j].Cells[days].Text = "L";
                                gv.Rows[j].Cells[days].ToolTip = "Leave";
                                gv.Rows[j].Cells[days].BackColor = Color.Yellow;
                                gv.Rows[j].Cells[days].Width = 10;


                            }
                        }
                        else if (gv.Rows[j].Cells[kk].Text != "S"
                             && gv.Rows[j].Cells[kk].Text != "WO"
                             && gv.Rows[j].Cells[kk].Text != "P"
                             && gv.Rows[j].Cells[kk].Text != "MP"
                             && gv.Rows[j].Cells[kk].Text != "MH"
                             && gv.Rows[j].Cells[kk].Text != "MA"
                             && gv.Rows[j].Cells[kk].Text != "L")
                        {
                            gv.Rows[j].Cells[kk].Text = "A";
                            gv.Rows[j].Cells[kk].ToolTip = "Absent";
                            gv.Rows[j].Cells[kk].BackColor = Color.Red;
                        }





                        #region SUNDAYS
                        DataSet dsdays = g.ReturnData1("Select Distinct Days,TrackHolidays from weeklyofftb where companyid='" + Convert.ToInt32(lblcompaniId.Text) + "' and  DATEPART(Mm,effectdate )<='" + ddmonth.SelectedValue + "' and DATEPART(YYYY,effectdate)<='" + year + "'");
                        if (dsdays.Tables[0].Rows.Count > 0)
                        {
                            for (int p = 0; p < dsdays.Tables[0].Rows.Count; p++)
                            {
                                string checkdays = dsdays.Tables[0].Rows[p]["TrackHolidays"].ToString();


                                #region 1 and 2nd
                                if (checkdays == "1 & 2")
                                {

                                    DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (1,2) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                    for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                    {
                                        DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                        int days = 0;
                                        days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                        gv.Rows[j].Cells[days].Text = "WO";
                                        gv.Rows[j].Cells[days].ToolTip = "Saturday";
                                        gv.Rows[j].Cells[days].BackColor = Color.YellowGreen;
                                        gv.Rows[j].Cells[days].Width = 10;


                                    }
                                }
                                #endregion

                                #region 3 and 4th
                                if (checkdays == "3 & 4")
                                {

                                    DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (3,4) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                    for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                    {
                                        DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                        int days = 0;
                                        days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                        gv.Rows[j].Cells[days].Text = "WO";
                                        gv.Rows[j].Cells[days].ToolTip = "Saturday";
                                        gv.Rows[j].Cells[days].BackColor = Color.YellowGreen;

                                        gv.Rows[j].Cells[days].Width = 10;


                                    }
                                }
                                #endregion

                                #region 1 and 3th
                                if (checkdays == "1 & 3")
                                {

                                    DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (1,3) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                    for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                    {
                                        DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                        int days = 0;
                                        days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                        gv.Rows[j].Cells[days].Text = "WO";
                                        gv.Rows[j].Cells[days].ToolTip = "Saturday";
                                        gv.Rows[j].Cells[days].BackColor = Color.YellowGreen;
                                        gv.Rows[j].Cells[days].Width = 10;


                                    }
                                }
                                #endregion

                                #region 2 and 4th
                                if (checkdays == "2 & 4")
                                {

                                    DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (2,4) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                    for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                    {
                                        DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                        int days = 0;
                                        days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                        gv.Rows[j].Cells[days].Text = "WO";
                                        gv.Rows[j].Cells[days].ToolTip = "Saturday";
                                        gv.Rows[j].Cells[days].BackColor = Color.YellowGreen;
                                        gv.Rows[j].Cells[days].Width = 10;


                                    }
                                }
                                #endregion

                                #region 2 and 3th
                                if (checkdays == "2 & 3")
                                {

                                    DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (2,3) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                    for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                    {
                                        DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                        int days = 0;
                                        days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                        gv.Rows[j].Cells[days].Text = "WO";
                                        gv.Rows[j].Cells[days].ToolTip = "Saturday";
                                        gv.Rows[j].Cells[days].BackColor = Color.YellowGreen;
                                        gv.Rows[j].Cells[days].Width = 10;


                                    }
                                }
                                #endregion

                                #region 1 and 4th
                                if (checkdays == "1 & 4")
                                {

                                    DataSet dsSat = g.ReturnData1(@"SET DATEFIRST 1;DECLARE @YearStart	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-01-01');DECLARE @YearEnd	DATE = CONVERT(DATE,CONVERT(CHAR(4),YEAR('" + date + "')) + '-12-31');WITH CTE_FullYear AS (SELECT TOP (366) DATEADD(dd,[number],@YearStart) dates FROM master.dbo.spt_values WHERE [type] = 'P' AND [number] >= 0 AND [number] < 367 AND DATEADD(dd,[number],@YearStart) <= @YearEnd ORDER BY number) SELECT dates FROM(SELECT 		 dates		,[Month]	= MONTH(dates),RID		= ROW_NUMBER() OVER (PARTITION BY MONTH(dates) ORDER BY dates)	FROM CTE_FullYear	WHERE DATEPART(dw,dates) = 6) tmp WHERE RID IN (1,4) and DATEPART(Month,dates) ='" + ddmonth.SelectedValue + "'; ");
                                    for (int h = 0; h < dsSat.Tables[0].Rows.Count; h++)
                                    {
                                        DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSat.Tables[0].Rows[h]["dates"] + "')");
                                        int days = 0;
                                        days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                                        gv.Rows[j].Cells[days].Text = "WO";
                                        gv.Rows[j].Cells[days].ToolTip = "Saturday";
                                        gv.Rows[j].Cells[days].BackColor = Color.YellowGreen;
                                        gv.Rows[j].Cells[days].Width = 10;


                                    }
                                }
                                #endregion
                            }

                        }
                        #region ALL SUNDAYS
                        // sun++;
                        DataSet dsSun = g.ReturnData1(@"DECLARE @date datetime
                                 SELECT @date = '" + date + "' SELECT [1st_sunday], DATENAME(weekday, [1st_sunday]) Daynames,[sunday] = DATEADD(DAY, n * 7, [1st_sunday]) FROM (SELECT [1st_sunday] = [1st_month] + 8 - DATEPART(weekday, [1st_month]) FROM (SELECT [1st_month] = DATEADD(MONTH, DATEDIFF(MONTH, 0, @date), 0)) d ) d CROSS JOIN (SELECT n = 0 UNION ALL SELECT n = 1 UNION ALL SELECT n = 2 UNION ALL SELECT n = 3 UNION ALL SELECT n = 4 ) n WHERE DATEDIFF(MONTH, @date, DATEADD(DAY, n * 7, [1st_sunday])) = 0");
                        for (int h = 0; h < dsSun.Tables[0].Rows.Count; h++)
                        {
                            DataSet dsd = g.ReturnData1("select DATEPART(Day, '" + dsSun.Tables[0].Rows[h]["sunday"] + "')");
                            int days = 0;
                            days = Convert.ToInt32(dsd.Tables[0].Rows[0][0].ToString());
                            gv.Rows[j].Cells[days].Text = "S";
                            gv.Rows[j].Cells[days].ToolTip = "Sunday";
                            gv.Rows[j].Cells[days].BackColor = Color.Orange;
                            gv.Rows[j].Cells[days].Width = 10;


                        }

                        #endregion
                        #endregion

                        if (gv.Rows[j].Cells[kk].Text != "S"
                        && gv.Rows[j].Cells[kk].Text != "WO"
                        && gv.Rows[j].Cells[kk].Text != "P"
                        && gv.Rows[j].Cells[kk].Text != "MP"
                        && gv.Rows[j].Cells[kk].Text != "MH"
                        && gv.Rows[j].Cells[kk].Text != "MA"
                        && gv.Rows[j].Cells[kk].Text != "L"
                        && dsHolidays.Tables[0].Rows.Count == 0
                        && dsP.Tables[0].Rows.Count == 0
                        && dsMP.Tables[0].Rows.Count == 0 && dsMH.Tables[0].Rows.Count == 0)
                        {
                            gv.Rows[j].Cells[kk].Text = "A";
                            gv.Rows[j].Cells[kk].BackColor = Color.Red;

                        }

                    }
                }

            }
        }
        catch (Exception ex)
        {
            g.ShowMessage(this.Page, ex.Message);
        }
        #endregion
        DispLAYCOUNT();
        #endregion
    }
    #endregion
    private void BindGrid()
    {
        var Data = (from d in EX.HoliDaysMasters
                    where d.Status == 1
                    select new { d.HolidaysID, d.HoliDaysName, d.Date, d.HolidayType, d.LeaveType }).OrderBy(d => d.Date);


        if (Data != null && Data.Count() > 0)
        {
            gvcalender.DataSource = Data;
            gvcalender.DataBind();
        }
        else
        {
            gvcalender.DataSource = null;
            gvcalender.DataBind();

        }

    }
    protected void CalHoli_SelectionChanged(object sender, EventArgs e)
    {
        BindGrid();
    }
    protected void OnApt_Cal_DayRender(object sender, DayRenderEventArgs e)
    {
        #region


        var Data = (from d in EX.HoliDaysMasters
                    where d.Status == 1
                    select new { d.HolidaysID, d.HoliDaysName, d.Date, d.HolidayType, d.LeaveType }).OrderBy(d => d.Date);


        if (Data != null && Data.Count() > 0)
        {
            foreach (var item in Data)
            {
                if (e.Day.Date == item.Date)
                {
                    //e.Cell.ToolTip = item.HoliDaysName;
                    //e.Cell.ForeColor = Color.White;
                    //e.Cell.BackColor = Color.Red;

                    e.Cell.BackColor = Color.Red;
                    e.Cell.ForeColor = Color.White;
                    e.Cell.ToolTip = item.HoliDaysName;
                    e.Cell.Controls.Clear();
                    Label link = new Label();
                    link.BackColor = Color.Red;

                    link.Style.Add("cursor", "pointer");
                    link.Text = e.Day.Date.Day.ToString();
                    link.ToolTip = item.HoliDaysName;
                    //link.CssClass = "form-control";
                    e.Cell.Controls.Add(link);

                    ///e.Cell.Text = e.Day.Date.ToString("dd");
                    ///e.Cell.ToolTip = e.Day.Date.ToString("dd/MM/yyyy");
                }
            }

        }
        #endregion


        #region
        var DataTraining = (from d in EX.TrainingTBs
                            join k in EX.EmployeeTBs on d.EmpID equals k.EmployeeId
                            where d.RHApproval == 2
                            select new { d.TraingID,d.Trainer, d.ReqDate, d.StartDate, d.EndDate, d.EndTime, d.StartTime, d.Type, d.RHApproval, d.Title, d.CourseContent, d.Description, Name = k.FName + " " + k.MName + " " + k.Lname }).OrderByDescending(d => d.ReqDate);




        if (DataTraining != null && DataTraining.Count() > 0)
        {
            foreach (var item in DataTraining)
            {
                DataSet dsDays = g.ReturnData1("DECLARE @Start DATETIME, @End DATETIME SELECT @Start='" + item.StartDate + "' , @End = '" + item.EndDate + "';WITH DateList AS (SELECT @Start [Date] UNION ALL SELECT [Date] +1 FROM DateList WHERE [Date] <@End) SELECT CONVERT(VARCHAR(Max), [Date],120) [mon] FROM DateList");
                for (int i = 0; i < dsDays.Tables[0].Rows.Count; i++)
                {
                    if (e.Day.Date == Convert.ToDateTime(dsDays.Tables[0].Rows[i][0].ToString()))
                    {
                        e.Cell.BackColor = Color.Green;
                        e.Cell.ForeColor = Color.Black;
                        e.Cell.ToolTip = item.Title + " Trainer Name:" + item.Trainer + " [" + item.StartTime + " To " + item.EndTime + "]";
                        e.Cell.Controls.Clear();
                        Label link = new Label();
                        //link.BackColor = Color.Black;

                        link.Style.Add("cursor", "pointer");
                        link.Text = e.Day.Date.Day.ToString();
                        link.ToolTip = item.Title + "Trainer Name:" + item.Trainer + " [" + item.StartTime + " " + item.EndTime + "]";
                        //link.CssClass = "form-control";
                        e.Cell.Controls.Add(link);

                        ///e.Cell.Text = e.Day.Date.ToString("dd");
                        ///e.Cell.ToolTip = e.Day.Date.ToString("dd/MM/yyyy");
                    }
                }

            }
        }
        #endregion
    }

}