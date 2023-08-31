using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class preview_salary_slip : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal gen = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (string.IsNullOrEmpty(Request.QueryString["process_id"]))
                Response.Redirect("Salary_Process_Report.aspx");
            GetEmployeeSaalryDetails();
        }
    }
    public void GetEmployeeSaalryDetails()
    {
        try
        {
            var getQuery = string.Format(@"SELECT ES.ProcessId, ES.EmployeeId, ES.DeptId, ES.DesigId, ES.ProcessMonth, ES.ProcessYear, ES.TotalDays, ES.WorkingDays, ES.PresentDays, ES.AbsentDays, ES.PaidLeaves, ES.E_BasicSalary, ES.E_HRA, ES.E_DA, ES.E_SpecialAllowance, 
                  ES.E_CityAllowance, ES.E_MedicalAllowance, ES.E_ConveyanceAllowance, ES.E_WashingAllowance, ES.E_AttendanceAllowance, ES.E_SpacialAllowance1, ES.E_SpacialAllowance2, ES.E_OthersAllowance, ES.IsPFApplicable, 
                  ES.D_ProvidentFundDeduct, ES.IsESIApplicable, ES.D_ESIDeduct, ES.IsPTApplicable, ES.D_ProffessionalTaxDeduct, ES.D_ServantInsuranceDeduct, ES.D_IncomeTaxDeduct, ES.D_PostalDepositDeduct, ES.D_EducationDonationsDeduct, 
                  ES.D_HomeRentDeduct, ES.D_ServantMilkDeduct, ES.D_AmritEMIDeduct, ES.D_AmritCreditDeduct, ES.D_AmritThevDeduct, ES.D_ShortLoanDeduct, ES.D_ElectricityBillDeduct, ES.D_WaterBillDeduct, ES.D_WorkerWelfareDeduct, 
                  ES.D_LabourWelfareDeduct, ES.D_AdTasalmat, ES.D_BankRecovery, ES.D_BankOfIndia, ES.D_WaranaBank, ES.D_WaranaMahila, ES.D_Other1Deduct, ES.D_Other2Deduct, ES.D_Other3Deduct, ES.D_Other4Deduct, ES.D_AdInsurance, 
                  ES.TotalEarnings, ES.TotalDeductions, ES.CostToCompany, ES.GrossSalary, ES.NetSalary, ES.Stamp, ES.EmployeerPFContribution, ES.IsActive, ES.CreatedBy, ES.CreatedDate, ES.IsProcessed, E.DesgId, E.EmployeeNo, E.FName + ' ' + E.Lname AS EmpName, E.PanNo, E.MachineID,
                  D1.DeptName, D2.DesigName, DATENAME(MONTH, DATEADD(MONTH, ES.ProcessMonth, '2000-12-01')) AS MonthName
FROM     EmpSalaryProcessTB AS ES INNER JOIN
                  EmployeeTB AS E ON E.EmployeeId = ES.EmployeeId INNER JOIN
                  MasterDesgTB AS D2 ON E.DesgId = D2.DesigID INNER JOIN
                  MasterDeptTB AS D1 ON E.DeptId = D1.DeptID
WHERE  (ES.ProcessId = {0})", Request.QueryString["process_id"]);

            DataTable dataTable = gen.ReturnData(getQuery);
            lblMonthYear.Text =$"{dataTable.Rows[0]["MonthName"].ToString()}-{ dataTable.Rows[0]["ProcessYear"].ToString()}";
            lblEmpName.Text = dataTable.Rows[0]["EmpName"].ToString();
            lblEmpNo.Text = dataTable.Rows[0]["MachineID"].ToString();
            lblPanNo.Text = dataTable.Rows[0]["PanNo"].ToString();
            lblDeptName.Text = dataTable.Rows[0]["DeptName"].ToString();
            lblDesignation.Text = dataTable.Rows[0]["DesigName"].ToString();
                      
            lblEP_BasicSalary.Text = dataTable.Rows[0]["E_BasicSalary"].ToString();
            lblEP_DA.Text = dataTable.Rows[0]["E_DA"].ToString();
            lblEP_SpecialPay.Text = dataTable.Rows[0]["E_SpecialAllowance"].ToString();
            lblEP_OtherAllowance.Text = dataTable.Rows[0]["E_OthersAllowance"].ToString();
            lblEP_City.Text = dataTable.Rows[0]["E_CityAllowance"].ToString();
            lblEP_HRA.Text = dataTable.Rows[0]["E_HRA"].ToString();
            lblEP_Chiledu.Text = "0.00";
            lblEP_Medical.Text = dataTable.Rows[0]["E_MedicalAllowance"].ToString();
            lblEP_Conveyance.Text = dataTable.Rows[0]["E_ConveyanceAllowance"].ToString();
            lblEP_Washing.Text = dataTable.Rows[0]["E_WashingAllowance"].ToString();
            lblEP_Attendance.Text = dataTable.Rows[0]["E_AttendanceAllowance"].ToString();
            lblEP_Rootcar.Text = "0.00";
            lblEP_Special1.Text = dataTable.Rows[0]["E_SpacialAllowance1"].ToString();
            lblEP_Special2.Text = dataTable.Rows[0]["E_SpacialAllowance2"].ToString();
            lblEP_Differencepay.Text = "0.00";
            lbl_Overtime.Text = "0.00";




            lblD_AddInsu.Text = dataTable.Rows[0]["D_AdInsurance"].ToString();
            lblD_AdTasalmat.Text = dataTable.Rows[0]["D_AdTasalmat"].ToString();
            lblD_AmritCredit.Text = dataTable.Rows[0]["D_AmritCreditDeduct"].ToString();
            lblD_AmritEmi.Text = dataTable.Rows[0]["D_AmritEMIDeduct"].ToString();
            lblD_AmritThev.Text = dataTable.Rows[0]["D_AmritThevDeduct"].ToString();
            lblD_BankRecover.Text = dataTable.Rows[0]["D_BankRecovery"].ToString();
            lblD_BOI.Text = dataTable.Rows[0]["D_BankOfIndia"].ToString();
            lblD_EduDon.Text = dataTable.Rows[0]["D_EducationDonationsDeduct"].ToString();
            lblD_ElectricityBill.Text = dataTable.Rows[0]["D_ElectricityBillDeduct"].ToString();
            lblD_ESIC.Text = dataTable.Rows[0]["D_ESIDeduct"].ToString();
            lblD_HouseRent.Text = dataTable.Rows[0]["D_HomeRentDeduct"].ToString();
            lblD_IncomeTax.Text = dataTable.Rows[0]["D_IncomeTaxDeduct"].ToString();
            lblD_LabourWel.Text = dataTable.Rows[0]["D_LabourWelfareDeduct"].ToString();
            lblD_Other1.Text = dataTable.Rows[0]["D_Other1Deduct"].ToString();
            lblD_Other2.Text = dataTable.Rows[0]["D_Other2Deduct"].ToString();
            lblD_Other3.Text = dataTable.Rows[0]["D_Other3Deduct"].ToString();
            lblD_Other4.Text = dataTable.Rows[0]["D_Other4Deduct"].ToString();
            lblD_PF.Text = dataTable.Rows[0]["D_ProvidentFundDeduct"].ToString();
            lblD_PostDepo.Text = dataTable.Rows[0]["D_PostalDepositDeduct"].ToString();
            lblD_PT.Text = dataTable.Rows[0]["D_ProffessionalTaxDeduct"].ToString();
            lblD_ServantInsu.Text = dataTable.Rows[0]["D_ServantInsuranceDeduct"].ToString();
            lblD_ServantMik.Text = dataTable.Rows[0]["D_ServantMilkDeduct"].ToString();
            lblD_ShortLoan.Text = dataTable.Rows[0]["D_ShortLoanDeduct"].ToString();
            lblD_WaranaBank.Text = dataTable.Rows[0]["D_WaranaBank"].ToString();
            lblD_WaranaMahila.Text = dataTable.Rows[0]["D_WaranaMahila"].ToString();
            lblD_WaterBill.Text = dataTable.Rows[0]["D_WaterBillDeduct"].ToString();
            lblD_WorkerWel.Text = dataTable.Rows[0]["D_WorkerWelfareDeduct"].ToString();
            
            lblNetSalary.Text = dataTable.Rows[0]["NetSalary"].ToString();
            lblTotalDeductions.Text = dataTable.Rows[0]["TotalDeductions"].ToString();
            lblTotalEarnings.Text = dataTable.Rows[0]["TotalEarnings"].ToString();
            //lblGrossSalary.Text=dataTable.Rows[0]["GrossSalary"].ToString();

            decimal totalearning = Convert.ToDecimal(dataTable.Rows[0]["TotalEarnings"]);
            decimal companypf = Convert.ToDecimal(dataTable.Rows[0]["D_ProvidentFundDeduct"]);
            decimal totalnetpayaddpf = totalearning + companypf;

            lblCompanypfSalarySalary.Text = totalnetpayaddpf.ToString();

            //int year = Convert.ToInt32(dataTable.Rows[0]["ProcessYear"].ToString());
            //int month = Convert.ToInt32(dataTable.Rows[0]["ProcessMonth"].ToString());
            //int daysInMonth=DateTime.DaysInMonth(year, month);
            lblDaysInMonth.Text = dataTable.Rows[0]["TotalDays"].ToString();
            lblPaidDays.Text= dataTable.Rows[0]["WorkingDays"].ToString();
            lblUnpaidDays.Text= dataTable.Rows[0]["AbsentDays"].ToString();



            var empid = dataTable.Rows[0]["EmployeeId"].ToString();
            int Employeeid = Convert.ToInt32(empid);

            var EmpSettingData = HR.EmpSalarySettingsTBs.Where(a => a.EmployeeId == Employeeid && a.IsActive == true).FirstOrDefault();
            if(EmpSettingData != null)
            {            
                lblEP_BasicSalary.Text = EmpSettingData.E_BasicSalary.ToString();
                lblE_DA.Text = EmpSettingData.E_DA.ToString();
                lblE_Spacial.Text = EmpSettingData.E_SpecialAllowance.ToString();
                lblE_Other.Text = EmpSettingData.E_OthersAllowance.ToString();                
                lblE_City.Text = EmpSettingData.E_CityAllowance.ToString();
                lblE_HRA.Text = EmpSettingData.E_HRA.ToString();
                lblE_ChilEdu.Text = "0";
                lblE_Medical.Text = EmpSettingData.E_MedicalAllowance.ToString();
                lblE_Convey.Text = EmpSettingData.E_ConveyanceAllowance.ToString();
                lblE_Washing.Text = EmpSettingData.E_WashingAllowance.ToString();
                lblE_Attend.Text = EmpSettingData.E_AttendanceAllowance.ToString();
                lblE_Spacial1.Text = EmpSettingData.E_SpacialAllowance1.ToString();
                lblE_Spacial2.Text = EmpSettingData.E_SpacialAllowance2.ToString();
            }
           




        }
        catch (Exception ex) { }
    }
}