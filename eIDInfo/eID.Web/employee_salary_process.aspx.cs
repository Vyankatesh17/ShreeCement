
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class employee_salary_process : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal gen = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {

            if (!IsPostBack)
            {
                txtYear.Attributes.Add("min", DateTime.Now.AddYears(-5).Year.ToString());
                txtYear.Attributes.Add("max", DateTime.Now.Year.ToString());
                txtYear.Text = DateTime.Now.Year.ToString();
                ddlMonth.SelectedIndex =DateTime.Now.Month==1?12: DateTime.Now.Month-1;
                BindDepartmentList();
                //BindDesignationList();
                BindEmployeeList();
                //string emp_id = Request.QueryString["emp_id"];
                //hfEmpId.Value = emp_id;
                //GetEmployeeDetails();
            }
        }
        BindJqFunctions();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if(IsValid)
        {
            try 
            {
                int counter = 0;
                foreach (GridViewRow item in gvPreviewProcess.Rows)
                {
                    Label lblProcessId = (Label)item.FindControl("lblProcessId");
                    string query = $"UPDATE EmpSalaryProcessTB SET IsActive=1, IsProcessed=1 WHERE ProcessId={lblProcessId.Text}";
                    DataTable updateData = gen.ReturnData(query);
                    counter++;
                }
                gen.ShowMessage(this.Page, "Salary processed successfully..");
            }
            catch(Exception ex) 
            { 
            
            }
        }
    }
    #region Functions
    private void BindDepartmentList()
    {
        var data = (from d in HR.MasterDeptTBs
                    where d.Status == 1
                    select d).Distinct();
        if (data.Count() > 0)
        {
            ddlDeptList.DataSource = data;
            ddlDeptList.DataTextField = "DeptName";
            ddlDeptList.DataValueField = "DeptID";
            ddlDeptList.DataBind();
        }
        ddlDeptList.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    private void BindDesignationList()
    {
        int deptId = ddlDeptList.SelectedIndex > 0 ? Convert.ToInt32(ddlDeptList.SelectedValue) : 0;
        var data = (from d in HR.MasterDesgTBs
                    where d.Status == 1&&d.DeptID== deptId
                    select new {d.DesigID,d.DesigName}).Distinct();
        if (data.Count() > 0)
        {
            ddlDesigList.DataSource = data;
            ddlDesigList.DataTextField = "DesigName";
            ddlDesigList.DataValueField = "DesigID";
            ddlDesigList.DataBind();
        }
        ddlDesigList.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    private void BindEmployeeList()
    {
        int deptId = ddlDeptList.SelectedIndex > 0 ? Convert.ToInt32(ddlDeptList.SelectedValue) : 0;
        //int desigId = ddlDesigList.SelectedIndex > 0 ? Convert.ToInt32(ddlDesigList.SelectedValue) : 0;
        var data = (from d in HR.EmployeeTBs
                    where d.IsActive == true && (d.RelivingStatus == null || d.RelivingStatus == 0) && d.DeptId== deptId
                    select new {d.EmployeeId, EmpName = d.FName + ' ' + d.Lname }).Distinct();
        if (data.Count() > 0)
        {
            ddlEmployeeList.DataSource = data;
            ddlEmployeeList.DataTextField = "EmpName";
            ddlEmployeeList.DataValueField = "EmployeeId";
            ddlEmployeeList.DataBind();
        }
        ddlEmployeeList.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    private void BindJqFunctions()
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    
    #endregion

    protected void ddlDeptList_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BindDesignationList();
        BindEmployeeList();
    }

    protected void ddlDesigList_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEmployeeList();
    }

    protected void btnPreview_Click(object sender, EventArgs e)
    {
        litErrorHandle.Text = "Click on Preview Button \n";
        ProcessSalaryPreview();
    }
    private void ProcessSalaryPreview()
    {
        try
        {
            int year = Convert.ToInt32(txtYear.Text);
            litErrorHandle.Text += $"Select Year : {year} \n";
            int month = ddlMonth.SelectedIndex;
            litErrorHandle.Text += $"Select Month : {month} \n";
            var sql = $@"SELECT ES.EmpSalSettingId, ES.EmployeeId, ES.E_BasicSalary, ES.E_HRA, ES.E_DA, ES.E_SpecialAllowance, ES.E_CityAllowance, ES.E_MedicalAllowance, ES.E_ConveyanceAllowance, ES.E_WashingAllowance, ES.E_AttendanceAllowance, 
                  ES.E_SpacialAllowance1, ES.E_SpacialAllowance2, ES.E_OthersAllowance, ES.IsPFApplicable, ES.D_ProvidentFundDeduct, ES.IsESIApplicable, ES.D_ESIDeduct, ES.IsPTApplicable, ES.D_ProffessionalTaxDeduct, 
                  ES.D_ServantInsuranceDeduct, ES.D_IncomeTaxDeduct, ES.D_PostalDepositDeduct, ES.D_EducationDonationsDeduct, ES.D_HomeRentDeduct, ES.D_ServantMilkDeduct, ES.D_AmritEMIDeduct, ES.D_AmritCreditDeduct, 
                  ES.D_AmritThevDeduct, ES.D_ShortLoanDeduct, ES.D_ElectricityBillDeduct, ES.D_WaterBillDeduct, ES.D_WorkerWelfareDeduct, ES.D_LabourWelfareDeduct, ES.D_AdTasalmat, ES.D_BankRecovery, ES.D_BankOfIndia, 
                  ES.D_WaranaBank, ES.D_WaranaMahila, ES.D_Other1Deduct, ES.D_Other2Deduct, ES.D_Other3Deduct, ES.D_Other4Deduct, ES.D_AdInsurance, ES.CostToCompany, ES.GrossSalary, ES.NetSalary, ES.Stamp, 
                  ES.EmployeerPFContribution, ES.IsActive, ES.CreatedBy, ES.CreatedDate, ES.UpdatedBy, ES.UpdatedDate, E.EmployeeNo, E.FName +' '+ E.Lname AS EmpName
FROM     EmpSalarySettingsTB AS ES INNER JOIN
                  EmployeeTB AS E ON ES.EmployeeId = E.EmployeeId
WHERE  (ES.IsActive = 1) AND (E.DeptId = {ddlDeptList.SelectedValue})";
            litErrorHandle.Text += $"Generate Select Query : {sql} \n";
            if (ddlEmployeeList.SelectedIndex > 0)
            {
                sql += $" AND (E.EmployeeId = {ddlEmployeeList.SelectedValue})";
                litErrorHandle.Text += $"Append to Select Query : {sql} \n";
            }

            var empList = HR.EmployeeTBs.Where(d => d.DeptId == Convert.ToInt32(ddlDeptList.SelectedValue)).Select(s => s.EmployeeId).ToList();// select d).ToList();

            litErrorHandle.Text += $"Get Employee List Total is: {empList.Count} \n";

            if (ddlEmployeeList.SelectedIndex > 0)
            {
                int emmpId = Convert.ToInt32(ddlEmployeeList.SelectedValue);
                empList.Clear();
                empList.Add(emmpId);
            }
            var attendData = (from d in HR.AttendaceLogTBs
                              where d.AttendanceDate.Year == year && d.AttendanceDate.Month == month
                              && empList.Contains(d.EmployeeId)
                              select d).Distinct();

            litErrorHandle.Text += $"Get Attendance Data Total Records : {attendData.Count()} \n";

            DataTable data = gen.ReturnData(sql);

            litErrorHandle.Text += $"Execute Select Query Get Total Records: {data.Rows.Count} \n";
            for (int i = 0; i < data.Rows.Count; i++)
            {
                var empId = Convert.ToInt32(data.Rows[i]["EmployeeId"].ToString());
                var EmployeeNo = Convert.ToString(data.Rows[i]["EmployeeNo"].ToString());
                var EmpName = data.Rows[i]["EmpName"].ToString();
                litErrorHandle.Text += $"ROW#{i}=>Emp Name:{EmpName} & EmpNo: {EmployeeNo} & empId:{empId}\n";

                var presentDaysCount=attendData.Where(d=>d.EmployeeId==empId && d.Present==1 && (d.Status== "P"||d.Status== "SP" ||d.Status== "WO½P"||d.Status== "WOP" ||d.Status== "P(OD)")).Count();

                litErrorHandle.Text += $"ROW#{i}=>Present Count : {presentDaysCount} \n";

                var weekOffCount = attendData.Where(d => d.EmployeeId == empId && d.Status == "WO").Count();
                litErrorHandle.Text += $"ROW#{i}=>Weekoff Count : {weekOffCount} \n";
                var halfDaysCount = attendData.Where(d => d.EmployeeId == empId && (d.Status == "½P(OD)"||d.Status== "½ P")).Count();
                litErrorHandle.Text += $"ROW#{i}=>half day Count : {halfDaysCount} \n";
                var absentDaysCount = attendData.Where(d => d.EmployeeId == empId && d.Status == "A").Count();
                litErrorHandle.Text += $"ROW#{i}=>Absent Count : {absentDaysCount} \n";
                var daysInMonth= DateTime.DaysInMonth(year, month);
                var workedDays = daysInMonth - absentDaysCount;

                decimal pf_deduct = 0, esi_deduct = 0, pt_deduct = 200;
                decimal employer_pf_contri = 0;
                var EmployeerPFContribution = Convert.ToDecimal(data.Rows[i]["EmployeerPFContribution"].ToString());
                litErrorHandle.Text += $"ROW#{i}=>Employer PF : {EmployeerPFContribution} \n";
                var grossSalary = string.IsNullOrEmpty(data.Rows[i]["GrossSalary"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["GrossSalary"].ToString());
                var perDaySalary = grossSalary * daysInMonth / 100;
                litErrorHandle.Text += $"ROW#{i}=>gross salary : {grossSalary} \n";
                grossSalary = Math.Round(grossSalary);
                var NetSalary = string.IsNullOrEmpty(data.Rows[i]["NetSalary"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["NetSalary"].ToString());
                litErrorHandle.Text += $"ROW#{i}=>Net salary : {NetSalary} \n";
                NetSalary = Math.Round(NetSalary);
                var E_BasicSalary = string.IsNullOrEmpty(data.Rows[i]["E_BasicSalary"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["E_BasicSalary"].ToString());
                litErrorHandle.Text += $"ROW#{i}=>Basic salary : {E_BasicSalary} \n";
                var E_DA = string.IsNullOrEmpty(data.Rows[i]["E_DA"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["E_DA"].ToString());
                litErrorHandle.Text += $"ROW#{i}=>DA : {E_DA} \n";
                var E_HRA = string.IsNullOrEmpty(data.Rows[i]["E_HRA"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["E_HRA"].ToString());
                litErrorHandle.Text += $"ROW#{i}=>HRA : {E_HRA} \n";
                var basicPer = grossSalary / grossSalary * 100;               
                var basicSalary = E_BasicSalary * workedDays / daysInMonth;
                basicSalary = Math.Round(basicSalary);
                var daAllow = E_DA * workedDays / daysInMonth;
                daAllow = Math.Round(daAllow);
                var hraAllow = E_HRA * workedDays / daysInMonth;
                hraAllow = Math.Round(hraAllow);
                // calculate basic salary


                if (grossSalary >= 21000)
                    esi_deduct = grossSalary * Convert.ToDecimal(0.75) / 100;


                var E_CityAllowance = string.IsNullOrEmpty(data.Rows[i]["E_CityAllowance"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["E_CityAllowance"].ToString());
                var E_MedicalAllowance = string.IsNullOrEmpty(data.Rows[i]["E_MedicalAllowance"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["E_MedicalAllowance"].ToString());
                var E_ConveyanceAllowance = string.IsNullOrEmpty(data.Rows[i]["E_ConveyanceAllowance"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["E_ConveyanceAllowance"].ToString());
                var E_WashingAllowance = string.IsNullOrEmpty(data.Rows[i]["E_WashingAllowance"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["E_WashingAllowance"].ToString());
                var E_AttendanceAllowance = string.IsNullOrEmpty(data.Rows[i]["E_AttendanceAllowance"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["E_AttendanceAllowance"].ToString());
                var E_SpecialAllowance = string.IsNullOrEmpty(data.Rows[i]["E_SpecialAllowance"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["E_SpecialAllowance"].ToString());
                var E_SpacialAllowance1 = string.IsNullOrEmpty(data.Rows[i]["E_SpacialAllowance1"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["E_SpacialAllowance1"].ToString());
                var E_SpacialAllowance2 = string.IsNullOrEmpty(data.Rows[i]["E_SpacialAllowance2"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["E_SpacialAllowance2"].ToString());
                var E_OthersAllowance = string.IsNullOrEmpty(data.Rows[i]["E_OthersAllowance"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["E_OthersAllowance"].ToString());

                decimal cityAllowance = 0, medicalAllowance = 0, conveyAllowance = 0, washingAllowance = 0, attendanceAllowance = 0, spacialAllowance = 0, spacial1Allowance = 0,
                    spacial2Allowance = 0, otherAllowance = 0;
                //calculate da & hra
                if (E_SpecialAllowance > 0)
                    spacialAllowance = E_SpecialAllowance;

                if (basicSalary > 0)
                {
                    var basic_da = basicSalary + daAllow + spacialAllowance;
                    if (basic_da > 15000)
                    {
                        employer_pf_contri = 15000 * 12 / 100;
                    }
                    else
                    {
                        employer_pf_contri = basic_da * 12 / 100;
                    }
                    if (basic_da > 15000)
                    {
                        pf_deduct = 15000 * 12 / 100;
                    }
                    else
                    {
                        pf_deduct = basic_da * 12 / 100;
                    }                                         
                    pf_deduct = Math.Round(pf_deduct);
                }
                
                
                //if (E_CityAllowance > 0)
                //    cityAllowance = E_CityAllowance * workedDays / daysInMonth;
                //if(E_MedicalAllowance > 0)
                //    medicalAllowance= E_MedicalAllowance * workedDays / daysInMonth;
                //if(E_ConveyanceAllowance > 0)
                //    conveyAllowance = E_ConveyanceAllowance * workedDays / daysInMonth;
                //if(E_WashingAllowance > 0)
                //    washingAllowance = E_WashingAllowance * workedDays / daysInMonth;
                //if(E_AttendanceAllowance > 0)
                //    attendanceAllowance = E_AttendanceAllowance * workedDays / daysInMonth;
                //if(E_SpecialAllowance > 0)
                //    spacialAllowance= E_SpecialAllowance * workedDays /daysInMonth;
                //if(E_SpacialAllowance1 > 0)
                //    spacial1Allowance = E_SpacialAllowance1 * workedDays / daysInMonth;
                //if(E_SpacialAllowance2 > 0)
                //    spacial2Allowance = E_SpacialAllowance2 * workedDays / daysInMonth;
                //if(E_OthersAllowance > 0)
                //    otherAllowance = E_OthersAllowance * workedDays / daysInMonth;


                if (E_CityAllowance > 0)
                    cityAllowance = E_CityAllowance;
                if (E_MedicalAllowance > 0)
                    medicalAllowance = E_MedicalAllowance;
                if (E_ConveyanceAllowance > 0)
                    conveyAllowance = E_ConveyanceAllowance;
                if (E_WashingAllowance > 0)
                    washingAllowance = E_WashingAllowance;
                if (E_AttendanceAllowance > 0)
                    if (workedDays == daysInMonth)
                        attendanceAllowance = E_AttendanceAllowance;             
                if (E_SpacialAllowance1 > 0)
                    spacial1Allowance = E_SpacialAllowance1;
                if (E_SpacialAllowance2 > 0)
                    spacial2Allowance = E_SpacialAllowance2;
                if (E_OthersAllowance > 0)
                    otherAllowance = E_OthersAllowance;



                

                var totalEarning = (basicSalary) + (daAllow) + (hraAllow) + (cityAllowance) +
              (medicalAllowance) + (conveyAllowance) + (washingAllowance) + (attendanceAllowance) +
              (spacialAllowance) + (spacial1Allowance) + (spacial2Allowance) + (otherAllowance);
litErrorHandle.Text += $"ROW#{i}=>Total Earnig : {totalEarning} \n";
                // deductions
                //var Stamp = data.Rows[i]["Stamp"].ToString() == "0" ? false : Convert.ToBoolean(data.Rows[i]["Stamp"].ToString());
                int Stamp = Convert.ToInt32(data.Rows[i]["Stamp"].ToString());
                var isPfApplicable = string.IsNullOrEmpty(data.Rows[i]["IsPFApplicable"].ToString()) ? false : Convert.ToBoolean(data.Rows[i]["IsPFApplicable"].ToString());
                var IsESIApplicable = string.IsNullOrEmpty(data.Rows[i]["IsESIApplicable"].ToString()) ? false : Convert.ToBoolean(data.Rows[i]["IsESIApplicable"].ToString());
                var IsPTApplicable = string.IsNullOrEmpty(data.Rows[i]["IsPTApplicable"].ToString()) ? false : Convert.ToBoolean(data.Rows[i]["IsPTApplicable"].ToString());
               
                var deductServantInsurance = string.IsNullOrEmpty(data.Rows[i]["D_ServantInsuranceDeduct"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["D_ServantInsuranceDeduct"].ToString());
                var deductIncomeTax = string.IsNullOrEmpty(data.Rows[i]["D_IncomeTaxDeduct"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["D_IncomeTaxDeduct"].ToString());
                var deductPostalDeposit = string.IsNullOrEmpty(data.Rows[i]["D_PostalDepositDeduct"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["D_PostalDepositDeduct"].ToString());
                var deductEducationDonation = string.IsNullOrEmpty(data.Rows[i]["D_EducationDonationsDeduct"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["D_EducationDonationsDeduct"].ToString());
                var deductHouseRent = string.IsNullOrEmpty(data.Rows[i]["D_HomeRentDeduct"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["D_HomeRentDeduct"].ToString());
                var deductServantMilk = string.IsNullOrEmpty(data.Rows[i]["D_ServantMilkDeduct"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["D_ServantMilkDeduct"].ToString());
                var deductAmritEMI = string.IsNullOrEmpty(data.Rows[i]["D_AmritEMIDeduct"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["D_AmritEMIDeduct"].ToString());
                var deductAmritCredit = string.IsNullOrEmpty(data.Rows[i]["D_AmritCreditDeduct"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["D_AmritCreditDeduct"].ToString());
                var deductAmritThev = string.IsNullOrEmpty(data.Rows[i]["D_AmritThevDeduct"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["D_AmritThevDeduct"].ToString());
                var deductShortLoan = string.IsNullOrEmpty(data.Rows[i]["D_ShortLoanDeduct"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["D_ShortLoanDeduct"].ToString());
                var deductElectricity = string.IsNullOrEmpty(data.Rows[i]["D_ElectricityBillDeduct"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["D_ElectricityBillDeduct"].ToString());
                var deductWaterBill = string.IsNullOrEmpty(data.Rows[i]["D_WaterBillDeduct"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["D_WaterBillDeduct"].ToString());
                var deductWorkerWelfare = string.IsNullOrEmpty(data.Rows[i]["D_WorkerWelfareDeduct"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["D_WorkerWelfareDeduct"].ToString());
                var deductLabourWelfare = string.IsNullOrEmpty(data.Rows[i]["D_LabourWelfareDeduct"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["D_LabourWelfareDeduct"].ToString());
                var deductAdTasalmat = string.IsNullOrEmpty(data.Rows[i]["D_AdTasalmat"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["D_AdTasalmat"].ToString());
                var deductBankRecovery = string.IsNullOrEmpty(data.Rows[i]["D_BankRecovery"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["D_BankRecovery"].ToString());
                var deductBOI = string.IsNullOrEmpty(data.Rows[i]["D_BankOfIndia"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["D_BankOfIndia"].ToString());
                var deductWaranaBank = string.IsNullOrEmpty(data.Rows[i]["D_WaranaBank"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["D_WaranaBank"].ToString());
                var deductWaranaMahila = string.IsNullOrEmpty(data.Rows[i]["D_WaranaMahila"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["D_WaranaMahila"].ToString());
                var deductOther1 = string.IsNullOrEmpty(data.Rows[i]["D_Other1Deduct"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["D_Other1Deduct"].ToString());
                var deductOther2 = string.IsNullOrEmpty(data.Rows[i]["D_Other2Deduct"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["D_Other2Deduct"].ToString());
                var deductOther3 = string.IsNullOrEmpty(data.Rows[i]["D_Other3Deduct"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["D_Other3Deduct"].ToString());
                var deductOther4 = string.IsNullOrEmpty(data.Rows[i]["D_Other4Deduct"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["D_Other4Deduct"].ToString());
                var deductAdInsurance = string.IsNullOrEmpty(data.Rows[i]["D_AdInsurance"].ToString()) ? 0 : Convert.ToDecimal(data.Rows[i]["D_AdInsurance"].ToString());

                int degid = 0;
                if(ddlDesigList.SelectedValue == "")
                {
                    degid = 0;
                }
                else
                {
                    degid = Convert.ToInt32(ddlDesigList.SelectedValue);
                }                

              var  totalDeductions = (pf_deduct) + (pt_deduct) + (esi_deduct) + (deductServantInsurance) +
                    (deductIncomeTax) + (deductPostalDeposit) + (deductEducationDonation) + (deductHouseRent) +
                    (deductServantMilk) + (deductAmritCredit) + (deductAmritEMI) + (deductAmritThev) +
                    (deductShortLoan) + (deductElectricity) + (deductWaterBill) + (deductWorkerWelfare) +
                    (deductLabourWelfare) + (deductAdTasalmat) + (deductBankRecovery) + (deductBOI) +
                    (deductWaranaBank) + (deductWaranaMahila) + (deductOther1) + (deductOther2) +
                    (deductOther3) + (deductOther4) + (deductAdInsurance);
                litErrorHandle.Text += $"ROW#{i}=> Total Deductions : {totalDeductions} \n";
                var netSalary = totalEarning - totalDeductions;
                netSalary = Math.Round(netSalary);



                string query = $@"
UPDATE [dbo].[EmpSalaryProcessTB] SET IsActive=0 WHERE IsProcessed=1 AND ProcessMonth={month} AND ProcessYear={year} AND EmployeeId={empId};

INSERT INTO [dbo].[EmpSalaryProcessTB]
(EmployeeId, DeptId, DesigId, ProcessMonth, ProcessYear, TotalDays, WorkingDays, PresentDays, AbsentDays, E_BasicSalary, 
E_HRA, E_DA, E_SpecialAllowance, E_CityAllowance, E_MedicalAllowance, E_ConveyanceAllowance, E_WashingAllowance, E_AttendanceAllowance, 
E_SpacialAllowance1, E_SpacialAllowance2, E_OthersAllowance, IsPFApplicable, D_ProvidentFundDeduct, IsESIApplicable, D_ESIDeduct, 
IsPTApplicable, D_ProffessionalTaxDeduct, D_ServantInsuranceDeduct, D_IncomeTaxDeduct, D_PostalDepositDeduct, D_EducationDonationsDeduct, 
D_HomeRentDeduct, D_ServantMilkDeduct, D_AmritEMIDeduct, D_AmritCreditDeduct, D_AmritThevDeduct, D_ShortLoanDeduct, D_ElectricityBillDeduct, 
D_WaterBillDeduct, D_WorkerWelfareDeduct, D_LabourWelfareDeduct, D_AdTasalmat, D_BankRecovery, D_BankOfIndia, D_WaranaBank, D_WaranaMahila, 
D_Other1Deduct, D_Other2Deduct, D_Other3Deduct, D_Other4Deduct, D_AdInsurance,  GrossSalary, NetSalary, Stamp,
EmployeerPFContribution, IsActive, CreatedBy, CreatedDate, IsProcessed,TotalEarnings,TotalDeductions)
     VALUES
({empId},{ddlDeptList.SelectedValue},{degid},{month},{year},{daysInMonth},{workedDays},{presentDaysCount},{absentDaysCount},
{basicSalary},{hraAllow},{daAllow},{spacialAllowance},{cityAllowance},{medicalAllowance},{conveyAllowance},{washingAllowance},
{attendanceAllowance},{spacial1Allowance},{spacial2Allowance},{otherAllowance},'{isPfApplicable}',{pf_deduct},
'{IsESIApplicable}',{esi_deduct},'{IsPTApplicable}',{pt_deduct},{deductServantInsurance},{deductIncomeTax},{deductPostalDeposit},
{deductEducationDonation},{deductHouseRent},{deductServantMilk},{deductAmritEMI},{deductAmritCredit},{deductAmritThev},
{deductShortLoan},{deductElectricity},{deductWaterBill},{deductWorkerWelfare},{deductLabourWelfare},{deductAdTasalmat},{deductBankRecovery}
,{deductBOI},{deductWaranaBank},{deductWaranaMahila},{deductOther1},{deductOther2},{deductOther3},{deductOther4},{deductAdInsurance}
,{grossSalary},{netSalary},{Stamp},{EmployeerPFContribution},'{true}','{Session["UserId"].ToString()}',GETDATE(),'{false}',{totalEarning},{totalDeductions})


";
                litErrorHandle.Text += $"ROW#{i}=>INSERT QUERY : {query} \n";
                DataTable dt = gen.ReturnData(query);
            }

            var getQuery = string.Format(@"SELECT ES.ProcessId, ES.EmployeeId, ES.DeptId, ES.DesigId, ES.ProcessMonth, ES.ProcessYear, ES.TotalDays, ES.WorkingDays, ES.PresentDays, ES.AbsentDays, ES.PaidLeaves, ES.E_BasicSalary, ES.E_HRA, ES.E_DA, ES.E_SpecialAllowance, 
                  ES.E_CityAllowance, ES.E_MedicalAllowance, ES.E_ConveyanceAllowance, ES.E_WashingAllowance, ES.E_AttendanceAllowance, ES.E_SpacialAllowance1, ES.E_SpacialAllowance2, ES.E_OthersAllowance, ES.IsPFApplicable, 
                  ES.D_ProvidentFundDeduct, ES.IsESIApplicable, ES.D_ESIDeduct, ES.IsPTApplicable, ES.D_ProffessionalTaxDeduct, ES.D_ServantInsuranceDeduct, ES.D_IncomeTaxDeduct, ES.D_PostalDepositDeduct, ES.D_EducationDonationsDeduct, 
                  ES.D_HomeRentDeduct, ES.D_ServantMilkDeduct, ES.D_AmritEMIDeduct, ES.D_AmritCreditDeduct, ES.D_AmritThevDeduct, ES.D_ShortLoanDeduct, ES.D_ElectricityBillDeduct, ES.D_WaterBillDeduct, ES.D_WorkerWelfareDeduct, 
                  ES.D_LabourWelfareDeduct, ES.D_AdTasalmat, ES.D_BankRecovery, ES.D_BankOfIndia, ES.D_WaranaBank, ES.D_WaranaMahila, ES.D_Other1Deduct, ES.D_Other2Deduct, ES.D_Other3Deduct, ES.D_Other4Deduct, ES.D_AdInsurance, 
                  ES.CostToCompany, ES.GrossSalary, ES.NetSalary, ES.Stamp, ES.EmployeerPFContribution, ES.IsActive, ES.CreatedBy, ES.CreatedDate, ES.IsProcessed, E.DesgId, E.EmployeeNo, E.FName + ' ' + E.Lname AS EmpName, E.Gender, 
                  D1.DeptName, D2.DesigName, DATENAME(MONTH, DATEADD(MONTH, ES.ProcessMonth, '2000-12-01')) AS MonthName
FROM     EmpSalaryProcessTB AS ES INNER JOIN
                  EmployeeTB AS E ON E.EmployeeId = ES.EmployeeId INNER JOIN
                  MasterDesgTB AS D2 ON E.DesgId = D2.DesigID INNER JOIN
                  MasterDeptTB AS D1 ON E.DeptId = D1.DeptID
WHERE  (ES.IsProcessed = 0) AND (ES.IsActive = 1) AND (ES.ProcessMonth = '{0}') AND (ES.ProcessYear = '{1}')",month, year);

            if (ddlEmployeeList.SelectedIndex > 0)
            {
                getQuery += $" AND (E.EmployeeId = {ddlEmployeeList.SelectedValue})";
            }


            DataTable dataTable =gen.ReturnData(getQuery);
            gvPreviewProcess.DataSource= dataTable;
            gvPreviewProcess.DataBind();
        }
        catch (Exception ex) 
        {
            litErrorHandle.Text += $"EXCEPTION=>{ex} \n";
        }
    }
}