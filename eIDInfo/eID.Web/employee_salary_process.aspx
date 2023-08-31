<%@ Page Title="Employee Salary Process" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="employee_salary_process.aspx.cs" Inherits="employee_salary_process" %>

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
            $("#ContentPlaceHolder1_txtGrossSalary").change(function () { calculate(); });
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
            var basicSalary = 0, daAllow = 0, hraAllow = 0;
            var pf_deduct = 0, esi_deduct = 0, pt_deduct = 200;
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
                    basicSalary = basicTypeValue;
                }

                if (grossSalary >= 21000)
                    esi_deduct = grossSalary * 0.75 / 100;
            }
            //calculate da & hra
            if (basicSalary > 0) {
                if (isDAFixed == 1) { daAllow = basicSalary * daTypeValue / 100; }
                else { daAllow = daTypeValue; }
                if (isHRAFixed == 1) {
                    hraAllow = basicSalary * hraTypeValue / 100; // for non-metro city
                    //hraAllow = basicSalary * 50 / 100;// for metro cities
                }
                else { hraAllow = hraTypeValue; }

                var basic_da = basicSalary + daAllow;
                if (basic_da > 15000) {
                    employer_pf_contri = 15000 * 12 / 100;
                }
                else {
                    employer_pf_contri = basic_da * 12 / 100;
                }
                pf_deduct = basicSalary * 12 / 100;
            }

            // earnings
            var cityAllowance = $("#ContentPlaceHolder1_txtCityAllowance").val();
            var medicalAllowance = $("#ContentPlaceHolder1_txtMdeicalAllowance").val();
            var conveyAllowance = $("#ContentPlaceHolder1_txtConveyanceAllowance").val();
            var washingAllowance = $("#ContentPlaceHolder1_txtWashingAllowance").val();
            var attendanceAllowance = $("#ContentPlaceHolder1_txtAttendanceAllowance").val();
            var spacialAllowance = $("#ContentPlaceHolder1_txtSpacialAllowance").val();
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
            <%--<asp:PostBackTrigger ControlID="btnSave" />
            <asp:PostBackTrigger ControlID="gvPreviewProcess" />--%>
            <asp:PostBackTrigger ControlID="btnPreview" />
            <asp:PostBackTrigger ControlID="ddlDeptList" />
        </Triggers>
        <ContentTemplate>
            <asp:Panel ID="Panel1" runat="server" DefaultButton="btnPreview">
                <div class="row">
                    <div class="col-lg-12">
                        <div class="box">
                            <div class="box-header with-border">
                                <h3 class="box-title">Process Employee Salary </h3>
                                <div class="box-tools">
                                    <a href="mst_employee_list_parabhani.aspx" class="btn btn-primary btn-sm">Back to List</a>
                                </div>
                            </div>
                            <div class="box-body" style="border-bottom: 1px solid #d2d6de;">
                                <div class="row">
                                    <div class="col-lg-2 col-sm-12">
                                        <div class="form-group">
                                            <label class="control-label">Month <span class="text-red Bold">*</span></label>
                                            <asp:DropDownList ID="ddlMonth" runat="server" CssClass="form-control">
                                                <asp:ListItem Value="0">Select Month</asp:ListItem>
                                                <asp:ListItem Value="1">January</asp:ListItem>
                                                <asp:ListItem Value="2">February</asp:ListItem>
                                                <asp:ListItem Value="3">March</asp:ListItem>
                                                <asp:ListItem Value="4">April</asp:ListItem>
                                                <asp:ListItem Value="5">May</asp:ListItem>
                                                <asp:ListItem Value="6">June</asp:ListItem>
                                                <asp:ListItem Value="7">July</asp:ListItem>
                                                <asp:ListItem Value="8">August</asp:ListItem>
                                                <asp:ListItem Value="9">September</asp:ListItem>
                                                <asp:ListItem Value="10">October</asp:ListItem>
                                                <asp:ListItem Value="11">November</asp:ListItem>
                                                <asp:ListItem Value="12">December</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlMonth" ErrorMessage="This field is required" ID="RequiredFieldValidator1" runat="server" CssClass="text-red" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlMonth" ErrorMessage="This field is required" InitialValue="0" ID="RequiredFieldValidator2" runat="server" CssClass="text-red" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-2 col-sm-12">
                                        <div class="form-group">
                                            <label class="control-label">Year</label>
                                            <asp:TextBox ID="txtYear" runat="server" TextMode="Number" MaxLength="4" CssClass="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ControlToValidate="txtYear" ErrorMessage="This field is required" ID="RequiredFieldValidator3" runat="server" CssClass="text-red" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ControlToValidate="txtYear" ErrorMessage="This field is required" InitialValue="0" ID="RequiredFieldValidator4" runat="server" CssClass="text-red" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-sm-12">
                                        <div class="form-group">
                                            <label class="control-label">Department</label>
                                            <asp:DropDownList ID="ddlDeptList" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDeptList_SelectedIndexChanged"></asp:DropDownList>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlDeptList" ErrorMessage="This field is required" ID="RequiredFieldValidator5" runat="server" CssClass="text-red" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ControlToValidate="ddlDeptList" ErrorMessage="This field is required" InitialValue="0" ID="RequiredFieldValidator6" runat="server" CssClass="text-red" Display="Dynamic" ValidationGroup="A"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-sm-12 hidden">
                                        <div class="form-group">
                                            <label class="control-label">Designation</label>
                                            <asp:DropDownList ID="ddlDesigList" runat="server" CssClass="form-control" AutoPostBack="true" OnSelectedIndexChanged="ddlDesigList_SelectedIndexChanged"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-sm-12">
                                        <div class="form-group">
                                            <label class="control-label">Employee</label>
                                            <asp:DropDownList ID="ddlEmployeeList" runat="server" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-1 col-sm-12">
                                        <div class="form-group">
                                            <label class="control-label">&nbsp;</label>
                                            <asp:Button ID="btnPreview" runat="server" CssClass="btn btn-primary btn-block" Text="Preview" ValidationGroup="A" OnClick="btnPreview_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="box-body">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvPreviewProcess" runat="server" AutoGenerateColumns="false" DataKeyNames="ProcessId"
                                        CssClass="table table-bordered table-striped table-condensed table-responsive">
                                        <Columns>
                                            <asp:TemplateField HeaderText="Sr no">
                                                <ItemTemplate>
                                                    <%# Container.DataItemIndex+1  %>
                                                    <asp:Label ID="lblProcessId" runat="server" Text='<%# Eval("ProcessId")%>' Visible="false"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="EmpName" HeaderText="Employee Name" />
                                            <asp:BoundField DataField="EmployeeNo" HeaderText="Emp No" />
                                            <asp:BoundField DataField="DeptName" HeaderText="Dept Name" />
                                            <asp:BoundField DataField="DesigName" HeaderText="Desig Name" />
                                            <asp:BoundField DataField="MonthName" HeaderText="Month Name" />
                                            <asp:BoundField DataField="ProcessYear" HeaderText="Year" />
                                            <asp:BoundField DataField="TotalDays" HeaderText="Total Days" />
                                            <asp:BoundField DataField="WorkingDays" HeaderText="Work Days" />
                                            <asp:BoundField DataField="PresentDays" HeaderText="Present Days" />
                                            <asp:BoundField DataField="AbsentDays" HeaderText="Absent Days" />
                                            <asp:BoundField DataField="GrossSalary" HeaderText="Gross" />
                                            <asp:BoundField DataField="E_BasicSalary" HeaderText="Basic" />
                                            <asp:BoundField DataField="E_HRA" HeaderText="HRA" />
                                            <asp:BoundField DataField="E_DA" HeaderText="DA" />

                                            <asp:BoundField DataField="D_ProvidentFundDeduct" HeaderText="PF" />
                                            <asp:BoundField DataField="D_ESIDeduct" HeaderText="ESIC" />
                                            <asp:BoundField DataField="D_ProffessionalTaxDeduct" HeaderText="PT" />


                                            <asp:BoundField DataField="NetSalary" HeaderText="Net Payable" />
                                        </Columns>
                                    </asp:GridView>
                                </div>



                            </div>
                            <div class="box-footer">
                                <%--<asp:Button ID="btnCancel" runat="server" CssClass="btn btn-warning" Text="Cancel" />--%>
                                <asp:Button ID="btnSave" runat="server" CssClass="btn btn-primary pull-right" Text="Process Salary" ValidationGroup="B" OnClick="btnSave_Click" />
                                <asp:HiddenField ID="hfEmpId" runat="server" />
                            </div>
                            <div class="hidden">
                                <asp:Literal ID="litErrorHandle" runat="server"></asp:Literal>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:Panel>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

