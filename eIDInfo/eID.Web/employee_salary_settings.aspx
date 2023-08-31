<%@ Page Title="Employee Salary Setting" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="employee_salary_settings.aspx.cs" Inherits="employee_salary_settings" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        .row.equal-cols {
            display: -webkit-flex;
            display: -ms-flexbox;
            display: flex;
            -webkit-flex-wrap: wrap;
            -ms-flex-wrap: wrap;
            flex-wrap: wrap;
        }

            .row.equal-cols:before,
            .row.equal-cols:after {
                display: block;
            }

            .row.equal-cols > [class*='col-'] {
                display: -webkit-flex;
                display: -ms-flexbox;
                display: flex;
                -webkit-flex-direction: column;
                -ms-flex-direction: column;
                flex-direction: column;
            }

                .row.equal-cols > [class*='col-'] > * {
                    -webkit-flex: 1 1 auto;
                    -ms-flex: 1 1 auto;
                    flex: 1 1 auto;
                }
    </style>

   <script>
       $(function () {
           //$("input[type='number'].autoCal").change(function () { calculate(); });
           $("#ContentPlaceHolder1_txtGrossSalary").change(function () { calculate(); });
           $('<%=txtGrossSalary.ClientID%>').on("change", function () {
                alert($(this).val());
            });
            $("#ContentPlaceHolder1_txtBasicSalaryType").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtCityAllowance").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtMdeicalAllowance").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtConveyanceAllowance").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtWashingAllowance").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtAttendanceAllowance").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtSpacialAllowance").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtSpacialAllow1").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtSpacialAllow2").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtOtherAllowance").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtServantInsurance").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtIncomeTax").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtPostalDeposit").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtEducationDonation").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtHouseRent").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtServantMilk").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtAmritEMI").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtAmritCredit").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtAmritThev").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtShortLoan").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtElectricity").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtWaterBill").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtWorkerWelfare").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtLabourWelfare").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtAdTasalmat").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtBankRecovery").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtBOI").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtWaranaBank").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtWaranaMahila").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtOther1").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtOther2").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtOther3").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtOther4").change(function () { calculate(); });
            $("#ContentPlaceHolder1_txtAdInsurance").change(function () { calculate(); });
        });
       function calculate() {
           var grossSalary = $("#ContentPlaceHolder1_txtGrossSalary").val();
           var costToCompany = $("#ContentPlaceHolder1_txtCTC").val();
           var netSalary = $("#ContentPlaceHolder1_txtNetPay").val();
           var totalEarning = $("#ContentPlaceHolder1_lblTotalEarning").val();
           var totalDeductions = $("#ContentPlaceHolder1_lblTotalDeductions").val();
           var isBasicFixed = $("#ContentPlaceHolder1_ddlBasicSalaryType").val();
           var basicTypeValue = $("#ContentPlaceHolder1_txtBasicSalaryType").val();
           var isDAFixed = $("#ContentPlaceHolder1_ddlDAType").val();
           var daTypeValue = $("#ContentPlaceHolder1_txtDAValue").val();
           var isHRAFixed = $("#ContentPlaceHolder1_ddlHRAType").val();
           var hraTypeValue = $("#ContentPlaceHolder1_txHRAValue").val();
           var spacialAllowance = $("#ContentPlaceHolder1_txtSpacialAllowance").val();
           var basicSalary = 0, daAllow = 0, hraAllow = 0, Specialpay = 0;
           var pf_deduct = 0, esi_deduct = 0, pt_deduct = 200;
           console.log('grossSalary=>' + grossSalary);
           var employer_pf_contri = 0;
           if (grossSalary.length == 0)
               grossSalary = 0;
           // calculate basic salary
           if (grossSalary > 0) {
               basicTypeValue = basicTypeValue.length > 0 ? basicTypeValue : 0;
               if (isBasicFixed == 1) {
                   basicSalary = grossSalary * basicTypeValue / 100;
               }
               else {
                   basicSalary = parseInt(basicTypeValue);
               }

               if (grossSalary >= 21000)
                   esi_deduct = grossSalary * 0.75 / 100;
           }
           //calculate da & hra
           if (basicSalary > 0) {
               if (isDAFixed == 1) { daAllow = grossSalary * daTypeValue / 100; }
               else { daAllow = parseInt(daTypeValue); }
               if (isHRAFixed == 1) {
                   var basic_daTotal = basicSalary + daAllow;
                   hraAllow = basic_daTotal * hraTypeValue / 100; // for non-metro city
                   //hraAllow = basicSalary * 50 / 100;// for metro cities
               }
               else { hraAllow = parseInt(hraTypeValue); }

               if (spacialAllowance > 0) {
                   Specialpay = parseInt(spacialAllowance);
               }

               


               var basic_da = basicSalary + daAllow + Specialpay;
               if (basic_da > 15000) {
                   employer_pf_contri = 15000 * 12 / 100;                   
               }
               else {
                   employer_pf_contri = basic_da * 12 / 100;
                   
               }
               if (basic_da > 15000) {
                   pf_deduct = 15000 * 12 / 100;
               }
               else {
                   pf_deduct = basic_da * 12 / 100;
               }
           }

           // earnings
           var cityAllowance = $("#ContentPlaceHolder1_txtCityAllowance").val();
           var medicalAllowance = $("#ContentPlaceHolder1_txtMdeicalAllowance").val();
           var conveyAllowance = $("#ContentPlaceHolder1_txtConveyanceAllowance").val();
           var washingAllowance = $("#ContentPlaceHolder1_txtWashingAllowance").val();
           var attendanceAllowance = $("#ContentPlaceHolder1_txtAttendanceAllowance").val();
           
           var spacial1Allowance = $("#ContentPlaceHolder1_txtSpacialAllow1").val();
           var spacial2Allowance = $("#ContentPlaceHolder1_txtSpacialAllow2").val();
           var otherAllowance = $("#ContentPlaceHolder1_txtOtherAllowance").val();
           cityAllowance = cityAllowance.length > 0 ? cityAllowance : 0;
           medicalAllowance = medicalAllowance.length > 0 ? medicalAllowance : 0;
           conveyAllowance = conveyAllowance.length > 0 ? conveyAllowance : 0;
           washingAllowance = washingAllowance.length > 0 ? washingAllowance : 0;
           attendanceAllowance = attendanceAllowance.length > 0 ? attendanceAllowance : 0;
           spacialAllowance = spacialAllowance.length > 0 ? spacialAllowance : 0;
           spacial1Allowance = spacial1Allowance.length > 0 ? spacial1Allowance : 0;
           spacial2Allowance = spacial2Allowance.length > 0 ? spacial2Allowance : 0;
           otherAllowance = otherAllowance.length > 0 ? otherAllowance : 0;

           totalEarning = parseFloat(basicSalary) + parseFloat(daAllow) + parseFloat(hraAllow) + parseFloat(cityAllowance) +
               parseFloat(medicalAllowance) + parseFloat(conveyAllowance) + parseFloat(washingAllowance) + parseFloat(attendanceAllowance) +
               parseFloat(spacialAllowance) + parseFloat(spacial1Allowance) + parseFloat(spacial2Allowance) + parseFloat(otherAllowance);
           // deductions
           var deductServantInsurance = $("#ContentPlaceHolder1_txtServantInsurance").val();
           var deductIncomeTax = $("#ContentPlaceHolder1_txtIncomeTax").val();
           var deductPostalDeposit = $("#ContentPlaceHolder1_txtPostalDeposit").val();
           var deductEducationDonation = $("#ContentPlaceHolder1_txtEducationDonation").val();
           var deductHouseRent = $("#ContentPlaceHolder1_txtHouseRent").val();
           var deductServantMilk = $("#ContentPlaceHolder1_txtServantMilk").val();
           var deductAmritEMI = $("#ContentPlaceHolder1_txtAmritEMI").val();
           var deductAmritCredit = $("#ContentPlaceHolder1_txtAmritCredit").val();
           var deductAmritThev = $("#ContentPlaceHolder1_txtAmritThev").val();
           var deductShortLoan = $("#ContentPlaceHolder1_txtShortLoan").val();
           var deductElectricity = $("#ContentPlaceHolder1_txtElectricity").val();
           var deductWaterBill = $("#ContentPlaceHolder1_txtWaterBill").val();
           var deductWorkerWelfare = $("#ContentPlaceHolder1_txtWorkerWelfare").val();
           var deductLabourWelfare = $("#ContentPlaceHolder1_txtLabourWelfare").val();
           var deductAdTasalmat = $("#ContentPlaceHolder1_txtAdTasalmat").val();
           var deductBankRecovery = $("#ContentPlaceHolder1_txtBankRecovery").val();
           var deductBOI = $("#ContentPlaceHolder1_txtBOI").val();
           var deductWaranaBank = $("#ContentPlaceHolder1_txtWaranaBank").val();
           var deductWaranaMahila = $("#ContentPlaceHolder1_txtWaranaMahila").val();
           var deductOther1 = $("#ContentPlaceHolder1_txtOther1").val();
           var deductOther2 = $("#ContentPlaceHolder1_txtOther2").val();
           var deductOther3 = $("#ContentPlaceHolder1_txtOther3").val();
           var deductOther4 = $("#ContentPlaceHolder1_txtOther4").val();
           var deductAdInsurance = $("#ContentPlaceHolder1_txtAdInsurance").val();

           deductServantInsurance = deductServantInsurance.length > 0 ? deductServantInsurance : 0;
           deductIncomeTax = deductIncomeTax.length > 0 ? deductIncomeTax : 0;
           deductPostalDeposit = deductPostalDeposit.length > 0 ? deductPostalDeposit : 0;
           deductEducationDonation = deductEducationDonation.length > 0 ? deductEducationDonation : 0;
           deductHouseRent = deductHouseRent.length > 0 ? deductHouseRent : 0;
           deductServantMilk = deductServantMilk.length > 0 ? deductServantMilk : 0;
           deductAmritCredit = deductAmritCredit.length > 0 ? deductAmritCredit : 0;
           deductAmritEMI = deductAmritEMI.length > 0 ? deductAmritEMI : 0;
           deductAmritThev = deductAmritThev.length > 0 ? deductAmritThev : 0;
           deductShortLoan = deductShortLoan.length > 0 ? deductShortLoan : 0;
           deductElectricity = deductElectricity.length > 0 ? deductElectricity : 0;
           deductWaterBill = deductWaterBill.length > 0 ? deductWaterBill : 0;
           deductWorkerWelfare = deductWorkerWelfare.length > 0 ? deductWorkerWelfare : 0;
           deductLabourWelfare = deductLabourWelfare.length > 0 ? deductLabourWelfare : 0;
           deductAdTasalmat = deductAdTasalmat.length > 0 ? deductAdTasalmat : 0;
           deductBankRecovery = deductBankRecovery.length > 0 ? deductBankRecovery : 0;
           deductBOI = deductBOI.length > 0 ? deductBOI : 0;
           deductWaranaBank = deductWaranaBank.length > 0 ? deductWaranaBank : 0;
           deductWaranaMahila = deductWaranaMahila.length > 0 ? deductWaranaMahila : 0;
           deductOther1 = deductOther1.length > 0 ? deductOther1 : 0;
           deductOther2 = deductOther2.length > 0 ? deductOther2 : 0;
           deductOther3 = deductOther3.length > 0 ? deductOther3 : 0;
           deductOther4 = deductOther4.length > 0 ? deductOther4 : 0;
           deductAdInsurance = deductAdInsurance.length > 0 ? deductAdInsurance : 0;


           totalDeductions = parseFloat(pf_deduct) + parseFloat(pt_deduct) + parseFloat(esi_deduct) + parseFloat(deductServantInsurance) +
               parseFloat(deductIncomeTax) + parseFloat(deductPostalDeposit) + parseFloat(deductEducationDonation) + parseFloat(deductHouseRent) +
               parseFloat(deductServantMilk) + parseFloat(deductAmritCredit) + parseFloat(deductAmritEMI) + parseFloat(deductAmritThev) +
               parseFloat(deductShortLoan) + parseFloat(deductElectricity) + parseFloat(deductWaterBill) + parseFloat(deductWorkerWelfare) +
               parseFloat(deductLabourWelfare) + parseFloat(deductAdTasalmat) + parseFloat(deductBankRecovery) + parseFloat(deductBOI) +
               parseFloat(deductWaranaBank) + parseFloat(deductWaranaMahila) + parseFloat(deductOther1) + parseFloat(deductOther2) +
               parseFloat(deductOther3) + parseFloat(deductOther4) + parseFloat(deductAdInsurance);

           netSalary = totalEarning - totalDeductions;

           $("#ContentPlaceHolder1_txtBasicSalary").val(basicSalary);
           $("#ContentPlaceHolder1_txtDearnessAllowance").val(daAllow);
           $("#ContentPlaceHolder1_txtHRA").val(hraAllow);
           $("#ContentPlaceHolder1_txtPF").val(pf_deduct);
           $("#ContentPlaceHolder1_txtESIC").val(esi_deduct);
           $("#ContentPlaceHolder1_txtProffessionalTax").val(pt_deduct);

           costToCompany = totalEarning + pf_deduct + pt_deduct + esi_deduct;

           $("#ContentPlaceHolder1_lblTotalEarning").text(totalEarning);
           $("#ContentPlaceHolder1_lblTotalDeductions").text(totalDeductions);

           $("#ContentPlaceHolder1_txtCTC").val(costToCompany);
           $("#ContentPlaceHolder1_txtNetPay").val(netSalary);

       }
       function jqFunctions() {

       }
   </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="btnSave" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSave">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="box">
                            <div class="box-header with-border">
                                <h3 class="box-title">Set Salary Setting for :
                            <asp:Label ID="lblEmpName" runat="server" CssClass="text-primary"></asp:Label></h3>
                                <div class="box-tools">
                                    <a href="mst_employee_list_parabhani.aspx" class="btn btn-primary btn-sm">Back to List</a>
                                </div>
                            </div>
                            <div class="box-body" style="border-bottom: 1px solid #d2d6de;">
                                <div class="row form-horizontal">
                                    <div class="col-lg-3 col-sm-12">
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Grade</label>
                                            <div class="col-lg-7">
                                                <asp:TextBox ID="lblEmpGrade" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-sm-12">
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Department</label>
                                            <div class="col-lg-7">
                                                <asp:TextBox ID="lblDeptName" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-sm-12">
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Aadhar No</label>
                                            <div class="col-lg-7">
                                                <asp:TextBox ID="lblAadharNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-sm-12">
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">PAN</label>
                                            <div class="col-lg-7">
                                                <asp:TextBox ID="lblPanNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-sm-12">
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Mobile No</label>
                                            <div class="col-lg-7">
                                                <asp:TextBox ID="lblMobileNo" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-sm-12">
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Email</label>
                                            <div class="col-lg-7">
                                                <asp:TextBox ID="lblEmail" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-sm-12">
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">DOJ</label>
                                            <div class="col-lg-7">
                                                <asp:TextBox ID="lblJoiningDate" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-sm-12">
                                        <div class="form-group">
                                            <label class="control-label col-sm-4">Gender</label>
                                            <div class="col-lg-7">
                                                <asp:TextBox ID="lblGender" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box-body">
                                <asp:MultiView ActiveViewIndex="0" ID="MultiView1" runat="server">
                                    <asp:View runat="server" ID="View1">
                                        <div class="box-header with-border">
                                            <h3 class="box-title">
                                                <asp:Label ID="lblMessage" runat="server" class="text-danger"></asp:Label></h3>
                                             <div class="box-tools">
                                   <asp:Button ID="btnAddNew" runat="server" CssClass="btn btn-danger" Text="Set New Setting" OnClick="btnAddNew_Click" />
                             
                                </div>
                                        </div>
                                          <div class="row" style="display: flex;">
                                            <div class="col-lg-4" style="display: flex;">
                                                <div class="box box-solid">
                                                    <div class="box-header with-border">
                                                        <h3 class="box-title text-success">Earnings</h3>
                                                    </div>
                                                    <div class="box-body">
                                                        <div class="row">
                                                            <div class="col-lg-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Basic Salary </label>
                                                                    <asp:Label ID="lblBasic" runat="server" CssClass="form-control" Enabled="false"></asp:Label>
                                                                         </div>
                                                            </div>
                                                            <div class="col-lg-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">DA (Dearness Allowance) </label>
                                                                    <asp:Label ID="lblDA" runat="server" CssClass="form-control" TextMode="Number" Enabled="false"></asp:Label>
                                                                  
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">HRA </label>
                                                                    <asp:Label ID="lblHRA" runat="server" CssClass="form-control" TextMode="Number" Enabled="false"></asp:Label>
                                                                   
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Spacial Pay</label>
                                                                    <asp:Label ID="lblSpacialAllow" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">City Allowance</label>
                                                                    <asp:Label ID="lblCityAllow" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Medical Allowance</label>
                                                                    <asp:Label ID="lblMedicalAllow" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Conveyance Allowance</label>
                                                                    <asp:Label ID="lblConveyAllow" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Washing Allowance</label>
                                                                    <asp:Label ID="lblWashingAllow" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Attendance Allowance</label>
                                                                    <asp:Label ID="lblAttendanceAllow" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            
                                                            <div class="col-lg-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Spacial Allowance 1</label>
                                                                    <asp:Label ID="lblSpacialAllow1" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Spacial Allowance 2</label>
                                                                    <asp:Label ID="lblSpacialAllow2" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Other Allowance</label>
                                                                    <asp:Label ID="lblOtherAllow" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-8">
                                                <div class="box box-solid">
                                                    <div class="box-header with-border">
                                                        <h3 class="box-title text-danger">Deductions</h3>
                                                    </div>
                                                    <div class="box-body">
                                                        <div class="row">
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Provident Fund </label>
                                                                    <asp:Label ID="lblDedPF" runat="server" CssClass="form-control" TextMode="Number" Enabled="false"></asp:Label>
                                                                  
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">ESIC </label>
                                                                    <asp:Label ID="lblDedESIC" runat="server" CssClass="form-control" TextMode="Number" Enabled="false"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Professional Tax </label>
                                                                    <asp:Label ID="lblDedPT" runat="server" Text="200" CssClass="form-control" TextMode="Number" Enabled="false"></asp:Label>
                                                                   
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Servant Insurance</label>
                                                                    <asp:Label ID="lblDedServantInsu" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Income Tax</label>
                                                                    <asp:Label ID="lblDedIncomeTax" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Postal Deposit</label>
                                                                    <asp:Label ID="lblDedPostDepo" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Education Donation</label>
                                                                    <asp:Label ID="lblDedEduDon" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">House Rent</label>
                                                                    <asp:Label ID="lblDedHouseRent" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Servant Milk</label>
                                                                    <asp:Label ID="lblDedServMilk" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Amrut EMI</label>
                                                                    <asp:Label ID="lblDedAmritEMI" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Amrut Credit</label>
                                                                    <asp:Label ID="lblDedAmritCredit" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Amrut Thev</label>
                                                                    <asp:Label ID="lblDedAmritThev" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Short Loan</label>
                                                                    <asp:Label ID="lblDedShortLoan" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Electricity Bill</label>
                                                                    <asp:Label ID="lblDedEleBill" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Water Bill</label>
                                                                    <asp:Label ID="lblDedWaterBill" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Worker Welfare</label>
                                                                    <asp:Label ID="lblDedWorkerWel" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Labour Welfare</label>
                                                                    <asp:Label ID="lblDedLabourWel" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Ad. Tasalmat</label>
                                                                    <asp:Label ID="lblDedAdTasalmat" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Bank Recovery</label>
                                                                    <asp:Label ID="lblDedBankRecov" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Bank of India</label>
                                                                    <asp:Label ID="lblDedWaranaBOI" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Warana Bank</label>
                                                                    <asp:Label ID="lblDeWaranaBank" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Warana Mahila</label>
                                                                    <asp:Label ID="lblDedWaranaMahila" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Other 1</label>
                                                                    <asp:Label ID="lblDedOther1" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Other 2</label>
                                                                    <asp:Label ID="lblDedOther2" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Other 3</label>
                                                                    <asp:Label ID="lblDedOther3" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Other 4</label>
                                                                    <asp:Label ID="lblDedOther4" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Ad. Insurance</label>
                                                                    <asp:Label ID="lblDedAdInsu" runat="server" CssClass="form-control" TextMode="Number"></asp:Label>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <%--<div class="row">
                                            <div class="col-lg-4">
                                                <div class="box-body bg-success">
                                                    <h5>Total Earnings :
                                                <asp:Label ID="lblTotalEarn" runat="server" CssClass="text-bold"></asp:Label></h5>
                                                </div>
                                            </div>
                                            <div class="col-lg-8">
                                                <div class="box-body bg-danger">
                                                    <h5>Total Deductions :
                                                <asp:Label ID="lblTotalDeduct" runat="server" CssClass="text-bold"></asp:Label></h5>
                                                </div>
                                            </div>
                                        </div>--%>
                                        <hr />
                                        <div class="row form-horizontal">
                                            <div class="col-lg-4">
                                                <div class="form-group hidden">
                                                    <label class="control-label text-right col-lg-4">CTC  :</label>
                                                    <div class="col-lg-8">
                                                        <asp:Label ID="lblCTCPay" runat="server" CssClass="form-control" Enabled="false"></asp:Label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4"></div>
                                            <div class="col-lg-4">
                                                <div class="form-group">
                                                    <label class="control-label text-right col-lg-4">Net Pay  :</label>
                                                    <div class="col-lg-8">
                                                        <asp:Label ID="lblNetPay" runat="server" CssClass="form-control" Enabled="false"></asp:Label>
                                                     
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                             </asp:View>
                                    <asp:View runat="server" ID="View2">
                                        <div class="row">
                                            <div class="col-lg-4 col-sm-12">
                                                <div class="form-group">
                                                    <label class="control-label">Gross Salary <span class="text-red text-bold">*</span></label>
                                                    <asp:TextBox ID="txtGrossSalary" onblur="calculate();" runat="server" CssClass="form-control autoCal" TextMode="Number" ></asp:TextBox>
                                                   <%-- <asp:RequiredFieldValidator ControlToValidate="txtGrossSalary" ID="RequiredFieldValidator1" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator ControlToValidate="txtGrossSalary" InitialValue="0" ID="RequiredFieldValidator2" runat="server" CssClass="text-red" ErrorMessage="Amount should be greater than 0" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                --%></div>
                                            </div>
                                            <div class="col-lg-4 col-sm-12">
                                                <div class="form-group">
                                                    <label class="control-label">Stamp <span class="text-red text-bold">*</span></label>
                                                    <asp:DropDownList ID="ddlStamp" runat="server" CssClass="form-control">
                                                        <asp:ListItem Value="1" Selected="True">Yes</asp:ListItem>
                                                        <asp:ListItem Value="0">No</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <%--<div class="col-lg-3 col-sm-12">
                                <div class="form-group">
                                    <label class="control-label">Net Salary <span class="text-red text-bold">*</span></label>
                                </div>
                            </div>--%>
                                        </div>
                                        <div class="row" style="display: flex;">
                                            <div class="col-lg-4" style="display: flex;">
                                                <div class="box box-solid box-success">
                                                    <div class="box-header with-border">
                                                        <h3 class="box-title">Earnings</h3>
                                                    </div>
                                                    <div class="box-body">
                                                        <div class="row">
                                                            <div class="col-lg-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Basic Salary Type<span class="text-red text-bold">*</span></label>
                                                                    <div class="input-group">
                                                                        <div class="input-group-btn">
                                                                            <asp:DropDownList ID="ddlBasicSalaryType" runat="server" CssClass="form-control" Width="75px">
                                                                                <asp:ListItem Selected="True" Value="1">In %</asp:ListItem>
                                                                                <asp:ListItem Value="2">Fixed</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <asp:TextBox ID="txtBasicSalaryType" onblur="calculate();" runat="server" Text="50" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Basic Salary <span class="text-red text-bold">*</span></label>
                                                                    <asp:TextBox ID="txtBasicSalary" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                                    <%-- <asp:RequiredFieldValidator ControlToValidate="txtBasicSalary" ID="RequiredFieldValidator3" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                            <asp:RequiredFieldValidator ControlToValidate="txtBasicSalary" InitialValue="0" ID="RequiredFieldValidator4" runat="server" CssClass="text-red" ErrorMessage="Amount should be greater than 0" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>--%>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">DA Type<span class="text-red text-bold">*</span></label>
                                                                    <div class="input-group">
                                                                        <div class="input-group-btn">
                                                                            <asp:DropDownList ID="ddlDAType" runat="server" CssClass="form-control" Width="75px">
                                                                                <asp:ListItem Selected="True" Value="1">In %</asp:ListItem>
                                                                                <asp:ListItem Value="2">Fixed</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <asp:TextBox ID="txtDAValue" onblur="calculate();" runat="server" Text="10" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">DA (Dearness Allowance) <span class="text-red text-bold">*</span></label>
                                                                    <asp:TextBox ID="txtDearnessAllowance" runat="server" CssClass="form-control" TextMode="Number" Enabled="false"></asp:TextBox>
                                                                    <%--<asp:RequiredFieldValidator ControlToValidate="txtDearnessAllowance" ID="RequiredFieldValidator5" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                            <asp:RequiredFieldValidator ControlToValidate="txtDearnessAllowance" InitialValue="0" ID="RequiredFieldValidator6" runat="server" CssClass="text-red" ErrorMessage="Amount should be greater than 0" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                                    --%>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">HRA Type<span class="text-red text-bold">*</span></label>
                                                                    <div class="input-group">
                                                                        <div class="input-group-btn">
                                                                            <asp:DropDownList ID="ddlHRAType" runat="server" CssClass="form-control" Width="75px">
                                                                                <asp:ListItem Selected="True" Value="1">In %</asp:ListItem>
                                                                                <asp:ListItem Value="2">Fixed</asp:ListItem>
                                                                            </asp:DropDownList>
                                                                        </div>
                                                                        <asp:TextBox ID="txHRAValue" onblur="calculate();" runat="server" Text="40" TextMode="Number" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">HRA <span class="text-red text-bold">*</span></label>
                                                                    <asp:TextBox ID="txtHRA" runat="server" CssClass="form-control" TextMode="Number" Enabled="false"></asp:TextBox>
                                                                    <%-- <asp:RequiredFieldValidator ControlToValidate="txtHRA" ID="RequiredFieldValidator7" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                            <asp:RequiredFieldValidator ControlToValidate="txtHRA" InitialValue="0" ID="RequiredFieldValidator8" runat="server" CssClass="text-red" ErrorMessage="Amount should be greater than 0" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                                    --%>
                                                                </div>
                                                            </div>
                                                             <div class="col-lg-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Spacial Pay</label>
                                                                    <asp:TextBox ID="txtSpacialAllowance" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Attendance Allowance</label>
                                                                    <asp:TextBox ID="txtAttendanceAllowance" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">City Allowance</label>
                                                                    <asp:TextBox ID="txtCityAllowance" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Medical Allowance</label>
                                                                    <asp:TextBox ID="txtMdeicalAllowance" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Conveyance Allowance</label>
                                                                    <asp:TextBox ID="txtConveyanceAllowance" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Washing Allowance</label>
                                                                    <asp:TextBox ID="txtWashingAllowance" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            
                                                           
                                                            <div class="col-lg-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Spacial Allowance 1</label>
                                                                    <asp:TextBox ID="txtSpacialAllow1" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Spacial Allowance 2</label>
                                                                    <asp:TextBox ID="txtSpacialAllow2" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Other Allowance</label>
                                                                    <asp:TextBox ID="txtOtherAllowance" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="col-lg-8">
                                                <div class="box box-solid box-danger">
                                                    <div class="box-header with-border">
                                                        <h3 class="box-title">Deductions</h3>
                                                    </div>
                                                    <div class="box-body">
                                                        <div class="row">
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Provident Fund <span class="text-red text-bold">*</span></label>
                                                                    <asp:TextBox ID="txtPF" runat="server" CssClass="form-control" TextMode="Number" Enabled="false"></asp:TextBox>
                                                                    <%--<asp:RequiredFieldValidator ControlToValidate="txtPF" ID="RequiredFieldValidator9" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                            <asp:RequiredFieldValidator ControlToValidate="txtPF" InitialValue="0" ID="RequiredFieldValidator10" runat="server" CssClass="text-red" ErrorMessage="Amount should be greater than 0" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                                    --%>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">ESIC <span class="text-red text-bold">*</span></label>
                                                                    <asp:TextBox ID="txtESIC" runat="server" CssClass="form-control" TextMode="Number" Enabled="false"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Professional Tax <span class="text-red text-bold">*</span></label>
                                                                    <asp:TextBox ID="txtProffessionalTax" runat="server" Text="200" CssClass="form-control" TextMode="Number" Enabled="false"></asp:TextBox>
                                                                    <%--  <asp:RequiredFieldValidator ControlToValidate="txtProffessionalTax" ID="RequiredFieldValidator11" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                            <asp:RequiredFieldValidator ControlToValidate="txtProffessionalTax" InitialValue="0" ID="RequiredFieldValidator12" runat="server" CssClass="text-red" ErrorMessage="Amount should be greater than 0" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                                    --%>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Servant Insurance</label>
                                                                    <asp:TextBox ID="txtServantInsurance" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Income Tax</label>
                                                                    <asp:TextBox ID="txtIncomeTax" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Postal Deposit</label>
                                                                    <asp:TextBox ID="txtPostalDeposit" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Education Donation</label>
                                                                    <asp:TextBox ID="txtEducationDonation" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">House Rent</label>
                                                                    <asp:TextBox ID="txtHouseRent" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Servant Milk</label>
                                                                    <asp:TextBox ID="txtServantMilk" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Amrit EMI</label>
                                                                    <asp:TextBox ID="txtAmritEMI" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Amrit Credit</label>
                                                                    <asp:TextBox ID="txtAmritCredit" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Amrit Thev</label>
                                                                    <asp:TextBox ID="txtAmritThev" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Short Loan</label>
                                                                    <asp:TextBox ID="txtShortLoan" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Electricity Bill</label>
                                                                    <asp:TextBox ID="txtElectricity" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Water Bill</label>
                                                                    <asp:TextBox ID="txtWaterBill" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Worker Welfare</label>
                                                                    <asp:TextBox ID="txtWorkerWelfare" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Labour Welfare</label>
                                                                    <asp:TextBox ID="txtLabourWelfare" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Ad. Tasalmat</label>
                                                                    <asp:TextBox ID="txtAdTasalmat" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Bank Recovery</label>
                                                                    <asp:TextBox ID="txtBankRecovery" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Bank of India</label>
                                                                    <asp:TextBox ID="txtBOI" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Warana Bank</label>
                                                                    <asp:TextBox ID="txtWaranaBank" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Warana Mahila</label>
                                                                    <asp:TextBox ID="txtWaranaMahila" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Other 1</label>
                                                                    <asp:TextBox ID="txtOther1" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Other 2</label>
                                                                    <asp:TextBox ID="txtOther2" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Other 3</label>
                                                                    <asp:TextBox ID="txtOther3" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Other 4</label>
                                                                    <asp:TextBox ID="txtOther4" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-4 col-sm-12">
                                                                <div class="form-group">
                                                                    <label class="control-label">Ad. Insurance</label>
                                                                    <asp:TextBox ID="txtAdInsurance" onblur="calculate();" runat="server" CssClass="form-control" TextMode="Number"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-4">
                                                <div class="box-body bg-success">
                                                    <h5>Total Earnings :
                                                <asp:Label ID="lblTotalEarning" runat="server" CssClass="text-bold"></asp:Label></h5>
                                                </div>
                                            </div>
                                            <div class="col-lg-8">
                                                <div class="box-body bg-danger">
                                                    <h5>Total Deductions :
                                                <asp:Label ID="lblTotalDeductions" runat="server" CssClass="text-bold"></asp:Label></h5>
                                                </div>
                                            </div>
                                        </div>
                                        <hr />
                                        <div class="row form-horizontal">
                                            <div class="col-lg-4">
                                                <div class="form-group hidden">
                                                    <label class="control-label text-right col-lg-4">CTC  <span class="text-red Bold">*</span>  :</label>
                                                    <div class="col-lg-8">
                                                        <asp:TextBox ID="txtCTC" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-4"></div>
                                            <div class="col-lg-4">
                                                <div class="form-group">
                                                    <label class="control-label text-right col-lg-4">Net Pay  <span class="text-red Bold">*</span> :</label>
                                                    <div class="col-lg-8">
                                                        <asp:TextBox ID="txtNetPay" runat="server" CssClass="form-control" Enabled="false"></asp:TextBox>
                                                        <%--  <asp:RequiredFieldValidator ControlToValidate="txtNetPay" ID="RequiredFieldValidator13" runat="server" CssClass="text-red" ErrorMessage="This field is required" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                <asp:RequiredFieldValidator ControlToValidate="txtNetPay" InitialValue="0" ID="RequiredFieldValidator14" runat="server" CssClass="text-red" ErrorMessage="Amount should be greater than 0" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                                        --%>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="box-footer">
                                            <%--<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" />--%>
                                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary pull-right" Text="Save" ValidationGroup="A" OnClick="btnSave_Click" />
                                            <asp:HiddenField ID="hfEmpId" runat="server" />
                                        </div>
                                    </asp:View>
                                </asp:MultiView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

