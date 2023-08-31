<%@ Page Language="C#" AutoEventWireup="true" CodeFile="preview_salary_slip.aspx.cs" Inherits="preview_salary_slip" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width" />
    <title>Full screen view- Employee Salary slip</title>
    <meta name="robots" content="index,follow" />
    <meta name="description" content="Employee Salary slip template" />
    <meta property="og:title" content="Full screen view- Employee Salary slip template" />
    <meta property="og:description" content="Employee Salary slip template" />
    <meta name="next-head-count" content="7" />
    <link rel="shortcut icon" href="/Images/logo.jpeg" />
    <style>
        .salary-slip {
            margin: 10px;
        }

            .salary-slip .empDetail {
                width: 100%;
                text-align: left;
                border: 2px solid black;
                border-collapse: collapse;
                table-layout: fixed;
            }


            .salary-slip .empDetail1 {
                width: 101%;
                text-align: left;
                border: none;
                border-collapse: collapse;
                table-layout: fixed;
            }

            .salary-slip .head {
                margin: 10px;
                margin-bottom: 50px;
                width: 100%;
            }

            .salary-slip .companyName {
                text-align: right;
                font-size: 20px;
                font-weight: bold;
            }

            .salary-slip .salaryMonth {
                text-align: center;
            }

            .salary-slip .table-border-bottom {
                border-bottom: 1px solid;
            }

            .salary-slip .table-border-right {
                border-right: 1px solid;
            }

            .salary-slip .myBackground {
                padding-top: 10px;
                text-align: left;
                border: 1px solid black;
                height: 40px;
            }

             .salary-slip .myHeadline {               
                text-align: center;
                border: 1px solid black;               
            }

            .salary-slip .myAlign {
                text-align: center;
                border-right: 1px solid black;
            }

            .salary-slip .myTotalBackground {
                padding-top: 10px;
                text-align: left;
                background-color: #ebf1de;
                border-spacing: 0px;
            }

            .salary-slip .align-4 {
                width: 25%;
                float: left;
            }

            .salary-slip .tail {
                margin-top: 35px;
            }

            .salary-slip .align-2 {
                margin-top: 25px;
                width: 50%;
                float: left;
            }

            .salary-slip .border-center {
                text-align: center;
            }

                .salary-slip .border-center th,
                .salary-slip .border-center td {
                    border: 1px solid black;
                }

            .salary-slip th,
            .salary-slip td {
                padding-left: 6px;
                font-size:15px;
            }

            @media print {
  .noPrint{
    display:none;
  }
}
    </style>

</head>
<body>
    <form id="form1" runat="server">
        <div class="salary-slip">
            <table class="empDetail">  
                <tr style='background-color: #c2d69b'>
                    <td colspan="8" style="text-align:center; color:blue" class="companyName">Shri Warana Sahakari Dudh Utpadak Prakriya Sangh Ltd.,</td>                    
                </tr>
                <tr height="70px" style='background-color: #c2d69b;border: 1px solid black;'>                    
                    <td colspan='4' style="margin-right:15px;">
                        <img height="70px" width="250px" src="Images/Warana_Logo.jpeg" /></td>
                    <td colspan='4' style="color:chocolate;">Tatyasaheb Korenagar, Post Warananagar, Tal.- Panhala Dist.- Kolhapur(Maharashtra) Pin-416113. Website-www.waranamilk.com</td>
                    
                </tr>
                <tr>
                    <th>Name:
                    </th>
                    <td colspan="2">
                        <asp:Label ID="lblEmpName" runat="server"></asp:Label>
                    </td>
                    <th>Code No.:
                    </th>
                    <td>
                        <asp:Label ID="lblEmpNo" runat="server"></asp:Label>
                    </td>
                    <td></td>
                    <th>Month:
                    </th>
                    <td>
                        <asp:Label ID="lblMonthYear" runat="server"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <th>Department:
                    </th>
                    <td>
                        <asp:Label ID="lblDeptName" runat="server"></asp:Label>
                    </td>
                    <td></td>
                    <th>Designation:
                    </th>
                    <td>
                        <asp:Label ID="lblDesignation" runat="server"></asp:Label>
                    </td>
                    <td></td>
                    <th>PAN No:
                    </th>
                    <td>
                        <asp:Label ID="lblPanNo" runat="server"></asp:Label>
                    </td>
                </tr>

                <tr>
                    <th>&nbsp;
                    </th>
                    <td></td>
                    <td></td>
                    <th></th>
                    <td></td>
                    <td></td>
                    <th></th>
                    <td></td>
                </tr>

               
                <tr class="myBackground">
                    <th colspan="2">Payments
                    </th>
                    <th>Particular
                    </th>
                    <th class="table-border-right">Amount (Rs.)
                    </th>
                    <th colspan="2">Deductions
                    </th>
                    <th>Particular
                    </th>
                    <th>Amount (Rs.)
                    </th>
                </tr>
                 <tr>                    
                     <th colspan="4" class="myHeadline">Payments Rate</th>
                </tr>
                <tr>
                    <th colspan="2">Basic Salary
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblE_BasicSalary" runat="server"></asp:Label>
                    </td>
                    <th colspan="2">Provident Fund
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblD_PF" runat="server"></asp:Label>
                    </td>
                </tr>
                
                <tr>
                    <th colspan="2"> Dearness Allowance
                    </th>
                    <td></td>

                    <td class="myAlign"><asp:Label ID="lblE_DA" runat="server"></asp:Label>
                    </td>
                    <th colspan="2">ESIC
                    </th>
                    <td></td>

                    <td class="myAlign"><asp:Label ID="lblD_ESIC" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>                   
                    <th colspan="2">Special Pay
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblE_Spacial" runat="server"></asp:Label>
                    </td>                   
                    <th colspan="2">Professional Tax
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblD_PT" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                     <th colspan="2">Allowance
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblE_Other" runat="server"></asp:Label>
                    </td>
                    <th colspan="2">Servant Insurance
                    </th>
                    <td></td>

                    <td class="myAlign"><asp:Label ID="lblD_ServantInsu" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">City Allowances
                    </th>
                    <td></td>

                    <td class="myAlign"><asp:Label ID="lblE_City" runat="server"></asp:Label>
                    </td>
                   
                    <th colspan="2">Income Tax
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblD_IncomeTax" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">House Rent Allowances
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblE_HRA" runat="server"></asp:Label>
                    </td>                     
                    <th colspan="2">Postal Deposit
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblD_PostDepo" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">Chil./edu. Allowances
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblE_ChilEdu" runat="server"></asp:Label>
                    </td>
                    <th colspan="2">Education Donation
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblD_EduDon" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">Medical Allowances
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblE_Medical" runat="server"></asp:Label>
                    </td>
                    <th colspan="2">House Rent
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblD_HouseRent" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">Conveyance Allowances
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblE_Convey" runat="server"></asp:Label>
                    </td>
                    <th colspan="2">Servant Milk
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblD_ServantMik" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">Washing Allowance
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblE_Washing" runat="server"></asp:Label>
                    </td>
                    <th colspan="2">Amrut Hapta
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblD_AmritEmi" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">Attendance Allowance
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblE_Attend" runat="server"></asp:Label>
                    </td>
                    <th colspan="2">Amrut Credit
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblD_AmritCredit" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                   
                     <th colspan="2">Special Allowance 1
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblE_Spacial1" runat="server"></asp:Label>
                    </td>                     
                    <th colspan="2">Amrut Deposite
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblD_AmritThev" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                     <th colspan="2">Special Allowance 2
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblE_Spacial2" runat="server"></asp:Label>
                    </td>                 
                    <th colspan="2">Short Loan
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblD_ShortLoan" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th colspan="4" class="myHeadline"> Earning Payment</th>
                    <th colspan="2">Electricity Bill
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblD_ElectricityBill" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>                    
                   <th colspan="2">Basic Salary
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblEP_BasicSalary" runat="server"></asp:Label>
                    </td>
                    <th colspan="2">Water Bill
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblD_WaterBill" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">Dearness Allowances
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblEP_DA" runat="server"></asp:Label>
                    </td>
                    <th colspan="2">Bank Recovery
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblD_BankRecover" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <th colspan="2">Special Pay
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblEP_SpecialPay" runat="server"></asp:Label>
                    </td>
                    <th colspan="2">Ad. Tasalmat
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblD_AdTasalmat" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">Allowance
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblEP_OtherAllowance" runat="server"></asp:Label>
                    </td>
                    <th colspan="2">Worker Welfare Fund
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblD_WorkerWel" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                   <th colspan="2">City Allowances
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblEP_City" runat="server"></asp:Label>
                    </td>
                    <th colspan="2">M. L. W. Fund
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblD_LabourWel" runat="server"></asp:Label>
                    </td>
                </tr>
               
                
                <tr>
                    <th colspan="2">House Rent Allowances
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblEP_HRA" runat="server"></asp:Label>
                    </td>
                    <th colspan="2">Bank of India
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblD_BOI" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                   <th colspan="2">Chil./edu. Allowances
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblEP_Chiledu" runat="server"></asp:Label>
                    </td>
                    <th colspan="2">Warana Bank
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblD_WaranaBank" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">Medical Allowances
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblEP_Medical" runat="server"></asp:Label>
                    </td>
                    <th colspan="2">Warana Mahila
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblD_WaranaMahila" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">Conveyance Allowances
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblEP_Conveyance" runat="server"></asp:Label>
                    </td>
                    <th colspan="2">Other 1
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblD_Other1" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">Washing Allowances
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblEP_Washing" runat="server"></asp:Label>
                    </td>
                    <th colspan="2">Other 2
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblD_Other2" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">Attendance Allowances
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblEP_Attendance" runat="server"></asp:Label>
                    </td>
                    <th colspan="2">Other 3
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblD_Other3" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                   <th colspan="2">Root Car Allowances
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblEP_Rootcar" runat="server"></asp:Label>
                    </td>
                    <th colspan="2">Other 4
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblD_Other4" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <th colspan="2">Special Allowance 1
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblEP_Special1" runat="server"></asp:Label>
                    </td>
                    <th colspan="2">Recovery Payment
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblD_AddInsu" runat="server"></asp:Label>
                    </td>
                </tr>
                 <tr>
                    <th colspan="2">Special Allowance 2
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblEP_Special2" runat="server"></asp:Label>
                    </td>                    
                </tr>
                 <tr>
                    <th colspan="2">Difference Pay
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lblEP_Differencepay" runat="server"></asp:Label>
                    </td>                    
                </tr>
                 <tr>
                    <th colspan="2">Overtime
                    </th>
                    <td></td>
                    <td class="myAlign"><asp:Label ID="lbl_Overtime" runat="server"></asp:Label>
                    </td>                    
                </tr>
                <tr class="myBackground">
                    <th colspan="3">Total Earning Payments
                    </th>
                    <td class="myAlign"><asp:Label ID="lblTotalEarnings" runat="server"></asp:Label>
                    </td>
                    <th colspan="3">Total Deductions
                    </th>
                    <td class="myAlign"><asp:Label ID="lblTotalDeductions" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr height="40px">
                    <th colspan="3">Gross Salary With Company P.F. Contribution</th>                   
                    <td class="myAlign"><asp:Label ID="lblCompanypfSalarySalary" runat="server"></asp:Label></td>
                    <th colspan="3" class="table-border-bottom">Net Pay Salary
                    </th>                    
                    <td class="myAlign"><asp:Label ID="lblNetSalary" runat="server"></asp:Label>
                    </td>
                </tr>





                <tbody class="border-center">
                    <tr>
                        <th>Attend/ Absence</th>
                        <th>Present Days</th>
                        <th>W/Off</th>
                        <th>Paid Leave</th>
                        <th>Without Pay</th>
                        <th>Absent Days</th>
                        <th colspan="2">Total Payment Days</th>                        
                    </tr>

                    <tr>
                        <th>Current Month</th>
                        <td><asp:Label ID="lblPaidDays" runat="server"></asp:Label></td>
                        <td></td>
                        <td></td>
                        <td><asp:Label ID="lblUnpaidDays" runat="server"></asp:Label></td>
                        <td></td>                        
                        <td colspan="2"><asp:Label ID="lblDaysInMonth" runat="server"></asp:Label></td>
                    </tr>
                    <tr>
                        <th>Last Month / Year</th>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>                        
                        <td colspan="2"></td>
                    </tr>


                </tbody>
            </table>

        </div>

        <br />
        <button onclick="window.print();" class="btn btn-primary noPrint">
Print Salary Slip
</button>
    </form>
</body>
</html>
