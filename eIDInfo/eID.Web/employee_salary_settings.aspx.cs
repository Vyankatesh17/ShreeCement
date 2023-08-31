using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class employee_salary_settings : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal gen = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {

            if (!IsPostBack)
            {
                string emp_id = Request.QueryString["emp_id"];
                hfEmpId.Value = emp_id;
                GetEmployeeDetails();
                GetEmployeeOldSettings();

                //txtGrossSalary.Attributes.Add("")
            }
        }
        BindJqFunctions();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if(IsValid)
        {
            try {
                CalculateSalary();

             
            }
            catch(Exception ex) 
            { 
            
            }
        }
    }
    #region Functions
    private void GetEmployeeDetails()
    {
        var data =(from d in  HR.EmployeeTBs where  d.EmployeeId == Convert.ToInt32(hfEmpId.Value)
                   join d1 in HR.MasterDeptTBs on d.DeptId equals d1.DeptID
                   select new { d.EmployeeNo,d.FName,d.MName,d.Lname,d.PanNo,d.PFAccountNo,d.ContactNo,
        d.DOJ,d.Email,d.ESICAccountNo,d.Grade1,d.Grade_Sequence_No,d.Gender,d.WorkLocation,d1.DeptName}).FirstOrDefault();
        if (data != null)
        {
            string fullName= $"{data.FName} {data.MName} {data.Lname}".Trim();
            lblEmpName.Text = $"{fullName} ({data.EmployeeNo})";
            lblDeptName.Text = data.DeptName;
            lblEmpGrade.Text = data.Grade1;
            lblPanNo.Text = data.PanNo;
            lblJoiningDate.Text = data.DOJ.Value.ToString("dd MMM yyyy");
            lblEmail.Text = data.Email;
            lblMobileNo.Text = data.ContactNo;
            lblGender.Text = data.Gender;
        }
    }
    private void GetEmployeeOldSettings()
    {
        string query = $"SELECT * FROM EmpSalarySettingsTB WHERE IsActive=1 AND EmployeeId={hfEmpId.Value}";
        DataTable dataTable = gen.ReturnData(query);
        if (dataTable.Rows.Count > 0)
        {
            lblMessage.Text = "Old Salary Settings Details";
            lblAttendanceAllow.Text = dataTable.Rows[0]["E_AttendanceAllowance"].ToString();
            lblBasic.Text= dataTable.Rows[0]["E_BasicSalary"].ToString();
            lblCityAllow.Text = dataTable.Rows[0]["E_CityAllowance"].ToString();
            lblConveyAllow.Text = dataTable.Rows[0]["E_ConveyanceAllowance"].ToString();
            lblDA.Text = dataTable.Rows[0]["E_DA"].ToString();
            lblHRA.Text = dataTable.Rows[0]["E_HRA"].ToString();
            lblMedicalAllow.Text = dataTable.Rows[0]["E_MedicalAllowance"].ToString();
            lblOtherAllow.Text = dataTable.Rows[0]["E_OthersAllowance"].ToString();
            lblSpacialAllow.Text = dataTable.Rows[0]["E_SpecialAllowance"].ToString();
            lblSpacialAllow1.Text = dataTable.Rows[0]["E_SpacialAllowance1"].ToString();
            lblSpacialAllow2.Text = dataTable.Rows[0]["E_SpacialAllowance2"].ToString();
            lblWashingAllow.Text = dataTable.Rows[0]["E_WashingAllowance"].ToString();


            lblDedAdInsu.Text = dataTable.Rows[0]["D_AdInsurance"].ToString();
            lblDedAdTasalmat.Text = dataTable.Rows[0]["D_AdTasalmat"].ToString();
            lblDedAmritCredit.Text = dataTable.Rows[0]["D_AmritCreditDeduct"].ToString();
            lblDedAmritEMI.Text = dataTable.Rows[0]["D_AmritEMIDeduct"].ToString();
            lblDedAmritThev.Text = dataTable.Rows[0]["D_AmritThevDeduct"].ToString();
            lblDedBankRecov.Text = dataTable.Rows[0]["D_BankRecovery"].ToString();
            lblDedEduDon.Text = dataTable.Rows[0]["D_EducationDonationsDeduct"].ToString();
            lblDedEleBill.Text = dataTable.Rows[0]["D_ElectricityBillDeduct"].ToString();
            lblDedESIC.Text = dataTable.Rows[0]["D_ESIDeduct"].ToString();
            lblDedHouseRent.Text = dataTable.Rows[0]["D_HomeRentDeduct"].ToString();
            lblDedIncomeTax.Text = dataTable.Rows[0]["D_IncomeTaxDeduct"].ToString();
            lblDedLabourWel.Text = dataTable.Rows[0]["D_LabourWelfareDeduct"].ToString();
            lblDedOther1.Text = dataTable.Rows[0]["D_Other1Deduct"].ToString();
            lblDedOther2.Text = dataTable.Rows[0]["D_Other2Deduct"].ToString();
            lblDedOther3.Text = dataTable.Rows[0]["D_Other3Deduct"].ToString();
            lblDedOther4.Text = dataTable.Rows[0]["D_Other4Deduct"].ToString();
            lblDedPF.Text = dataTable.Rows[0]["D_ProvidentFundDeduct"].ToString();
            lblDedPostDepo.Text = dataTable.Rows[0]["D_PostalDepositDeduct"].ToString();
            lblDedPT.Text = dataTable.Rows[0]["D_ProffessionalTaxDeduct"].ToString();
            lblDedServantInsu.Text = dataTable.Rows[0]["D_ServantInsuranceDeduct"].ToString();
            lblDedServMilk.Text = dataTable.Rows[0]["D_ServantMilkDeduct"].ToString();
            lblDedShortLoan.Text = dataTable.Rows[0]["D_ShortLoanDeduct"].ToString();
            lblDedWaranaBOI.Text = dataTable.Rows[0]["D_BankOfIndia"].ToString();
            lblDedWaranaMahila.Text = dataTable.Rows[0]["D_WaranaMahila"].ToString();
            lblDedWaterBill.Text = dataTable.Rows[0]["D_WaterBillDeduct"].ToString();
            lblDedWorkerWel.Text = dataTable.Rows[0]["D_WorkerWelfareDeduct"].ToString();
            lblDeWaranaBank.Text = dataTable.Rows[0]["D_WaranaBank"].ToString();
            //lblTotalDeduct.Text = dataTable.Rows[0][""].ToString();
            //lblTotalEarn.Text = dataTable.Rows[0][""].ToString();
            lblNetPay.Text = dataTable.Rows[0]["NetSalary"].ToString();
        }
        else
        {
            lblMessage.Text = "Salary setting is not completed for selected employee.. Click on Set New Setting button for salary setting..";
        }
       
    }
    private void BindJqFunctions()
    {
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    private void CalculateSalary()
    {
        var grossSalary = string.IsNullOrEmpty(txtGrossSalary.Text) ? 0 : Convert.ToDecimal(txtGrossSalary.Text);
        var costToCompany = decimal.MaxValue;
        var netSalary = decimal.MaxValue;
        var totalEarning = decimal.MaxValue;
        var totalDeductions = decimal.MaxValue;
        var isBasicFixed = ddlBasicSalaryType.Text;
        var basicTypeValue = string.IsNullOrEmpty(txtBasicSalaryType.Text) ? 0 : Convert.ToDecimal(txtBasicSalaryType.Text);
        var isDAFixed = ddlDAType.Text;
        var daTypeValue = string.IsNullOrEmpty(txtDAValue.Text) ? 0 : Convert.ToDecimal(txtDAValue.Text);
        var isHRAFixed = ddlHRAType.Text;
        var hraTypeValue = string.IsNullOrEmpty(txHRAValue.Text) ? 0 : Convert.ToDecimal(txHRAValue.Text);
        var spacialAllowance = string.IsNullOrEmpty(txtSpacialAllowance.Text) ? 0 : Convert.ToDecimal(txtSpacialAllowance.Text);
        decimal basicSalary = 0, daAllow = 0, hraAllow = 0, SpecialPay = 0;
        decimal pf_deduct = 0, esi_deduct = 0, pt_deduct = 200;
        decimal employer_pf_contri = 0;
        if (grossSalary == 0)
            grossSalary = 0;
        // calculate basic salary
        if (grossSalary > 0)
        {
            basicTypeValue = basicTypeValue > 0 ? basicTypeValue : 0;
            if (ddlBasicSalaryType.SelectedIndex == 0)
            {
                basicSalary = grossSalary * basicTypeValue / 100;
            }
            else
            {
                basicSalary = basicTypeValue;
            }

            if (grossSalary >= 21000)
                esi_deduct = grossSalary * Convert.ToDecimal(0.75) / 100;
        }
        //calculate da & hra

        if (basicSalary > 0)
        {
            if (ddlDAType.SelectedIndex == 0) { daAllow = grossSalary * daTypeValue / 100; }
            else { daAllow = daTypeValue; }
            if (ddlHRAType.SelectedIndex == 0)
            {
                var basic_da_Total = basicSalary + daAllow;               
                    hraAllow = basic_da_Total * hraTypeValue / 100;
                                                           //hraAllow = basicSalary * 50 / 100;// for metro cities
            }
            else { hraAllow = hraTypeValue; }
            hraAllow = Math.Round(hraAllow);

            if(spacialAllowance > 0)
            {
                SpecialPay = spacialAllowance;
            }



            var basic_da = basicSalary + daAllow + SpecialPay;
            if (basic_da > 15000)
            {
                employer_pf_contri = 15000 * 12 / 100;
            }
            else
            {
                employer_pf_contri = basic_da * 12 / 100;
            }
            pf_deduct = basic_da * 12 / 100;
            pf_deduct = Math.Round(pf_deduct);
        }

        // earnings
        var cityAllowance = string.IsNullOrEmpty(txtCityAllowance.Text) ? 0 : Convert.ToDecimal(txtCityAllowance.Text);
        var medicalAllowance = string.IsNullOrEmpty(txtMdeicalAllowance.Text) ? 0 : Convert.ToDecimal(txtMdeicalAllowance.Text);
        var conveyAllowance = string.IsNullOrEmpty(txtConveyanceAllowance.Text) ? 0 : Convert.ToDecimal(txtConveyanceAllowance.Text);
        var washingAllowance = string.IsNullOrEmpty(txtWashingAllowance.Text) ? 0 : Convert.ToDecimal(txtWashingAllowance.Text);
        var attendanceAllowance = string.IsNullOrEmpty(txtAttendanceAllowance.Text) ? 0 : Convert.ToDecimal(txtAttendanceAllowance.Text);
       
        var spacial1Allowance = string.IsNullOrEmpty(txtSpacialAllow1.Text) ? 0 : Convert.ToDecimal(txtSpacialAllow1.Text);
        var spacial2Allowance = string.IsNullOrEmpty(txtSpacialAllow2.Text) ? 0 : Convert.ToDecimal(txtSpacialAllow2.Text);
        var otherAllowance = string.IsNullOrEmpty(txtOtherAllowance.Text) ? 0 : Convert.ToDecimal(txtOtherAllowance.Text);

        totalEarning = (basicSalary) + (daAllow) + (hraAllow) + (cityAllowance) +
            (medicalAllowance) + (conveyAllowance) + (washingAllowance) + (attendanceAllowance) +
            (spacialAllowance) + (spacial1Allowance) + (spacial2Allowance) + (otherAllowance);
        // deductions
        var deductServantInsurance = string.IsNullOrEmpty(txtServantInsurance.Text) ? 0 : Convert.ToDecimal(txtServantInsurance.Text);
        var deductIncomeTax = string.IsNullOrEmpty(txtIncomeTax.Text) ? 0 : Convert.ToDecimal(txtIncomeTax.Text);
        var deductPostalDeposit = string.IsNullOrEmpty(txtPostalDeposit.Text) ? 0 : Convert.ToDecimal(txtPostalDeposit.Text);
        var deductEducationDonation = string.IsNullOrEmpty(txtEducationDonation.Text) ? 0 : Convert.ToDecimal(txtEducationDonation.Text);
        var deductHouseRent = string.IsNullOrEmpty(txtHouseRent.Text) ? 0 : Convert.ToDecimal(txtHouseRent.Text);
        var deductServantMilk = string.IsNullOrEmpty(txtServantMilk.Text) ? 0 : Convert.ToDecimal(txtServantMilk.Text);
        var deductAmritEMI = string.IsNullOrEmpty(txtAmritEMI.Text) ? 0 : Convert.ToDecimal(txtAmritEMI.Text);
        var deductAmritCredit = string.IsNullOrEmpty(txtAmritCredit.Text) ? 0 : Convert.ToDecimal(txtAmritCredit.Text);
        var deductAmritThev = string.IsNullOrEmpty(txtAmritThev.Text) ? 0 : Convert.ToDecimal(txtAmritThev.Text);
        var deductShortLoan = string.IsNullOrEmpty(txtShortLoan.Text) ? 0 : Convert.ToDecimal(txtShortLoan.Text);
        var deductElectricity = string.IsNullOrEmpty(txtElectricity.Text) ? 0 : Convert.ToDecimal(txtElectricity.Text);
        var deductWaterBill = string.IsNullOrEmpty(txtWaterBill.Text) ? 0 : Convert.ToDecimal(txtWaterBill.Text);
        var deductWorkerWelfare = string.IsNullOrEmpty(txtWorkerWelfare.Text) ? 0 : Convert.ToDecimal(txtWorkerWelfare.Text);
        var deductLabourWelfare = string.IsNullOrEmpty(txtLabourWelfare.Text) ? 0 : Convert.ToDecimal(txtLabourWelfare.Text);
        var deductAdTasalmat = string.IsNullOrEmpty(txtAdTasalmat.Text) ? 0 : Convert.ToDecimal(txtAdTasalmat.Text);
        var deductBankRecovery = string.IsNullOrEmpty(txtBankRecovery.Text) ? 0 : Convert.ToDecimal(txtBankRecovery.Text);
        var deductBOI = string.IsNullOrEmpty(txtBOI.Text) ? 0 : Convert.ToDecimal(txtBOI.Text);
        var deductWaranaBank = string.IsNullOrEmpty(txtWaranaBank.Text) ? 0 : Convert.ToDecimal(txtWaranaBank.Text);
        var deductWaranaMahila = string.IsNullOrEmpty(txtWaranaMahila.Text) ? 0 : Convert.ToDecimal(txtWaranaMahila.Text);
        var deductOther1 = string.IsNullOrEmpty(txtOther1.Text) ? 0 : Convert.ToDecimal(txtOther1.Text);
        var deductOther2 = string.IsNullOrEmpty(txtOther2.Text) ? 0 : Convert.ToDecimal(txtOther2.Text);
        var deductOther3 = string.IsNullOrEmpty(txtOther3.Text) ? 0 : Convert.ToDecimal(txtOther3.Text);
        var deductOther4 = string.IsNullOrEmpty(txtOther4.Text) ? 0 : Convert.ToDecimal(txtOther4.Text);
        var deductAdInsurance = string.IsNullOrEmpty(txtAdInsurance.Text) ? 0 : Convert.ToDecimal(txtAdInsurance.Text);



        totalDeductions = (pf_deduct) + (pt_deduct) + (esi_deduct) + (deductServantInsurance) +
            (deductIncomeTax) + (deductPostalDeposit) + (deductEducationDonation) + (deductHouseRent) +
            (deductServantMilk) + (deductAmritCredit) + (deductAmritEMI) + (deductAmritThev) +
            (deductShortLoan) + (deductElectricity) + (deductWaterBill) + (deductWorkerWelfare) +
            (deductLabourWelfare) + (deductAdTasalmat) + (deductBankRecovery) + (deductBOI) +
            (deductWaranaBank) + (deductWaranaMahila) + (deductOther1) + (deductOther2) +
            (deductOther3) + (deductOther4) + (deductAdInsurance);

        netSalary = totalEarning - totalDeductions;

        txtBasicSalary.Text = basicSalary.ToString();
        txtDearnessAllowance.Text = daAllow.ToString();
        txtHRA.Text = hraAllow.ToString();
        txtPF.Text = pf_deduct.ToString();
        txtESIC.Text = esi_deduct.ToString();
        txtProffessionalTax.Text = pt_deduct.ToString();

        costToCompany = totalEarning + pf_deduct + pt_deduct + esi_deduct;

        lblTotalEarning.Text = totalEarning.ToString();
        lblTotalDeductions.Text = totalDeductions.ToString();

        txtCTC.Text = costToCompany.ToString();
        txtNetPay.Text = netSalary.ToString();

        var IsPFApplicable = pf_deduct > 0 ? true : false;
        var IsESIApplicable = esi_deduct > 0 ? true : false;
        var IsPTApplicable = pt_deduct > 0 ? true : false;
        var CreatedBy = Session["UserId"];
        var query = $@"INSERT INTO dbo.EmpSalarySettingsTB (EmployeeId,E_BasicSalary,E_HRA,E_DA,E_SpecialAllowance,E_CityAllowance
,E_MedicalAllowance,E_ConveyanceAllowance,E_WashingAllowance,E_AttendanceAllowance,E_SpacialAllowance1,E_SpacialAllowance2,E_OthersAllowance
,IsPFApplicable,D_ProvidentFundDeduct,IsESIApplicable,D_ESIDeduct,IsPTApplicable,D_ProffessionalTaxDeduct,D_ServantInsuranceDeduct
,D_IncomeTaxDeduct,D_PostalDepositDeduct,D_EducationDonationsDeduct,D_HomeRentDeduct,D_ServantMilkDeduct,D_AmritEMIDeduct
,D_AmritCreditDeduct,D_AmritThevDeduct,D_ShortLoanDeduct,D_ElectricityBillDeduct,D_WaterBillDeduct,D_WorkerWelfareDeduct,D_LabourWelfareDeduct
,D_AdTasalmat,D_BankRecovery,D_BankOfIndia,D_WaranaBank,D_WaranaMahila,D_Other1Deduct,D_Other2Deduct,D_Other3Deduct,D_Other4Deduct
,D_AdInsurance,CostToCompany,GrossSalary,NetSalary,IsActive,CreatedBy,CreatedDate,UpdatedBy,UpdatedDate,Stamp,EmployeerPFContribution)
     VALUES ('{hfEmpId.Value}','{basicSalary}','{hraAllow}','{daAllow}','{spacialAllowance}','{cityAllowance}','{medicalAllowance}'
,'{conveyAllowance}','{washingAllowance}','{attendanceAllowance}','{spacial1Allowance}','{spacial2Allowance}'
,'{otherAllowance}','{IsPFApplicable}','{pf_deduct}','{IsESIApplicable}','{esi_deduct}','{IsPTApplicable}'
,'{pt_deduct}','{deductServantInsurance}','{deductIncomeTax}','{deductPostalDeposit}','{deductEducationDonation}'
,'{deductHouseRent}','{deductServantMilk}','{deductAmritEMI}','{deductAmritCredit}','{deductAmritThev}','{deductShortLoan}'
,'{deductElectricity}','{deductWaterBill}','{deductWorkerWelfare}','{deductLabourWelfare}','{deductAdTasalmat}','{deductBankRecovery}'
,'{deductBOI}','{deductWaranaBank}','{deductWaranaMahila}','{deductOther1}','{deductOther2}','{deductOther3}','{deductOther4}'
,'{deductAdInsurance}','{costToCompany}','{grossSalary}','{netSalary}','{true}','{CreatedBy}',GETDATE(),'{CreatedBy}',GETDATE(),'{ddlStamp.SelectedIndex}','{employer_pf_contri}');

SELECT SCOPE_IDENTITY() AS [LastInserted];";

        DataTable dt = gen.ReturnData(query);
        if(dt.Rows.Count > 0)
        {
            var last_record = string.IsNullOrEmpty(dt.Rows[0]["LastInserted"].ToString()) ? 0 : Convert.ToInt32(dt.Rows[0]["LastInserted"].ToString());
            if (last_record > 0)
            {
                string upQuery = $"UPDATE dbo.EmpSalarySettingsTB SET IsActive=0 WHERE EmployeeId='{hfEmpId.Value}' AND EmpSalSettingId<>'{last_record}'";
                DataTable dtUp= gen.ReturnData(upQuery);
                gen.ShowMessageRedirect(this.Page, $"Salary settings successfully applied for {lblEmpName.Text}", "mst_employee_list_parabhani.aspx");
            }
        }
    }
    #endregion

    protected void btnAddNew_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }
}