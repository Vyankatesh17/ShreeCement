<%@ Page Title="Performance Evaluation" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="ViewEmployeePerformance.aspx.cs" Inherits="ViewEmployeePerformance" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <section class="content-header">
        <h3>
        Performance Evaluation
    </section>

    <section class="content">
        <!-- left column -->
        <div class="col-md-12">
            <div class="box box-primary">
                <table cellpadding="3" cellspacing="3" width="50%">
                    <tr>
                        <td>



                            <asp:Panel ID="pasn1" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td valign="top">Employee Name:
                                                                            <asp:Label ID="Label25" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="ddlEmp" runat="server" AutoPostBack="True" CssClass="form-control" Enabled="False">
                                            </asp:DropDownList>
                                            <asp:ListSearchExtender ID="ddlEmp_ListSearchExtender2" runat="server"
                                                Enabled="True" TargetControlID="ddlEmp">
                                            </asp:ListSearchExtender>
                                            <asp:CompareValidator ID="cmp3" runat="server" ControlToValidate="ddlEmp" Display="Static" ForeColor="Red"
                                                ErrorMessage="*" Operator="NotEqual" ValidationGroup="a" ValueToCompare="--Select--"></asp:CompareValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlEmp"
                                                Display="Static" ForeColor="Red" ErrorMessage="*" ValidationGroup="a" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                        </td>
                                        <%-- <td id="tdttext" runat="server" visible="false">
                                                            <asp:TextBox ID="txtEmpName" runat="server" OnTextChanged="txtEmpName_TextChanged" CssClass="form-control"></asp:TextBox>
                                                            <asp:FilteredTextBoxExtender ID="txtEmpName_FilteredTextBoxExtender" runat="server"
                                                                Enabled="True" TargetControlID="txtEmpName" ValidChars="abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                                            </asp:FilteredTextBoxExtender>
                                                            <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" DelimiterCharacters=""
                                                                Enabled="True" ServicePath="" TargetControlID="txtEmpName" ServiceMethod="GetCompletionList"
                                                                UseContextKey="True">
                                                            </asp:AutoCompleteExtender>
                                                        </td>--%>
                                    </tr>
                                    <tr>
                                        <td valign="top">Date Of Evaluation:
                                                 <asp:Label ID="Label4" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtdateeval" runat="server" CssClass="form-control" BackColor="White" Enabled="False"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtdateeval">
                                            </asp:CalendarExtender>
                                            <asp:RequiredFieldValidator ID="req3" runat="server" ControlToValidate="txtdateeval"
                                                Display="Static" ForeColor="Red" ErrorMessage="*" ValidationGroup="a" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Employee Position:
                                                                            <asp:Label ID="Label1" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEmpPosition" runat="server" CssClass="form-control" BackColor="White" ReadOnly="True" Enabled="False"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" Enabled="True"
                                                TargetControlID="txtEmpPosition" ValidChars="abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                            </asp:FilteredTextBoxExtender>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEmpPosition"
                                                Display="Static" ForeColor="Red" ErrorMessage="*" ValidationGroup="a" SetFocusOnError="True"></asp:RequiredFieldValidator>

                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="top">Evaluator Name:
                                                                            <asp:Label ID="Label2" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        </td>
                                        <td valign="top">
                                            <asp:DropDownList ID="ddlevaluater" runat="server" AutoPostBack="True" CssClass="form-control" Enabled="False">
                                            </asp:DropDownList>
                                            <asp:ListSearchExtender ID="ListSearchExtender1" runat="server"
                                                Enabled="True" TargetControlID="ddlevaluater">
                                            </asp:ListSearchExtender>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="ddlevaluater" Display="Static" ForeColor="Red"
                                                ErrorMessage="*" Operator="NotEqual" ValidationGroup="a" ValueToCompare="--Select--"></asp:CompareValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlevaluater"
                                                Display="Static" ForeColor="Red" ErrorMessage="*" ValidationGroup="a" SetFocusOnError="True"></asp:RequiredFieldValidator>
                                            <%--  <asp:RequiredFieldValidator ID="RequiredFieldValidator33" runat="server" ControlToValidate="ddlevaluater"
                                                                Display="Static" ForeColor="Red" ErrorMessage="*" ValidationGroup="a" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>

                                            <%-- <asp:TextBox ID="txtEvaluatorName" runat="server" CssClass="form-control"></asp:TextBox>
                                                           <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server" Enabled="True"
                                                                TargetControlID="txtEvaluatorName" ValidChars="abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                                            </asp:FilteredTextBoxExtender>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEvaluatorName"
                                                                Display="Static" ForeColor="Red" ErrorMessage="*" ValidationGroup="a" SetFocusOnError="True"></asp:RequiredFieldValidator>--%>

                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>

                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </section>

    <section class="content-header">
        <h3>
        Types Of Evaluation
    </section>
    <section class="content">
        <!-- left column -->
        <div class="col-md-12">
            <div class="box box-primary">
                <table cellpadding="3" cellspacing="3" width="100%">
                    <tr>
                        <td>



                            <asp:Panel ID="Panel2" runat="server">
                                <table width="100%">
                                    <tr>
                                        <td class="style1">
                                            <asp:RadioButtonList ID="rdbDays" runat="server" RepeatDirection="Horizontal" Width="950px">
                                                <asp:ListItem Selected="True">60 Days</asp:ListItem>
                                                <asp:ListItem>6 Month</asp:ListItem>
                                                <asp:ListItem>Year</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                </table>
                <table cellpadding="2" cellspacing="2" width="90%" style="padding-right: 20px">
                    <tr>
                        <td>
                            <fieldset>
                                <legend>Performance Rating Definitions</legend>
                                <asp:Panel ID="Panel3" runat="server">
                                    <table cellpadding="2" cellspacing="2" width="100%">
                                        <tr>
                                            <td>The following ratings must be used to ensure commonality of language and consistency
                                                                            on overall ratings:
                                                                            <br />
                                                <br />
                                                (There should be supporting comments to justify ratings of “Outstanding” “Below
                                                                            Expectations, and “Unsatisfactory”)
                                                                            <table style="border: 1px solid black;" width="100%">
                                                                                <tr>
                                                                                    <td>
                                                                                        <br />
                                                                                        Outstanding
                                                                                    </td>
                                                                                    <td>
                                                                                        <br />
                                                                                        Performance is consistently superior
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <br />
                                                                                        Exceeds Expectations
                                                                                    </td>
                                                                                    <td>
                                                                                        <br />
                                                                                        Performance is routinely above job requirements
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <br />
                                                                                        Meets Expectations
                                                                                    </td>
                                                                                    <td>
                                                                                        <br />
                                                                                        Performance is regularly competent and dependable
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <br />
                                                                                        Below Expectations
                                                                                    </td>
                                                                                    <td>
                                                                                        <br />
                                                                                        Performance fails to meet job requirements on a frequent basis
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td>
                                                                                        <br />
                                                                                        Unsatisfactory
                                                                                    </td>
                                                                                    <td>
                                                                                        <br />
                                                                                        Performance is consistently unacceptable
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </fieldset>
                        </td>
                    </tr>
                </table>
                <table cellpadding="2" cellspacing="2" width="90%">
                    <tr>
                        <td>
                            <fieldset>
                                <legend>Performance Factors</legend>
                                <asp:Panel ID="Panel1" runat="server">
                                    <table width="100%" style="border: 1px solid black;">
                                        <tr>
                                            <td>1) Knowledge of Work -
                                                                            <br />
                                                Consider employee's skill level,<br />
                                                knowledge and understanding of all phases of the job
                                                                            <br />
                                                and
                                                                            <br />
                                                those requiring improved skills and/or experience.<br />
                                                <br />
                                                <asp:RadioButtonList ID="RadioButtonList1" runat="server" Width="700px" RepeatDirection="Horizontal" Enabled="False">
                                                    <asp:ListItem Selected="True" Value="5">Outstanding</asp:ListItem>
                                                    <asp:ListItem Value="4">Exceeds Expectations</asp:ListItem>
                                                    <asp:ListItem Value="3">Meets Expectations</asp:ListItem>
                                                    <asp:ListItem Value="2">Below Expectations</asp:ListItem>
                                                    <asp:ListItem Value="1">Unsatisfactory</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                                <table width="100%" style="border: 1px solid black;">
                                    <tr>
                                        <td>2) Customer Responsiveness -
                                                                        <br />
                                            <br />
                                            Measures responsiveness and courtesy in dealing with internal staff,<br />
                                            external customers and vendors; employee projects a courteous manner.<br />
                                            <br />
                                            <asp:RadioButtonList ID="RadioButtonList2" Width="700px" runat="server" RepeatDirection="Horizontal" Enabled="False">
                                                <asp:ListItem Selected="True" Value="5">Outstanding</asp:ListItem>
                                                <asp:ListItem Value="4">Exceeds Expectations</asp:ListItem>
                                                <asp:ListItem Value="3">Meets Expectations</asp:ListItem>
                                                <asp:ListItem Value="2">Below Expectations</asp:ListItem>
                                                <asp:ListItem Value="1">Unsatisfactory</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" style="border: 1px solid black;">
                                    <tr>
                                        <td>3) QUALITY OF WORK:<br />
                                            <br />
                                            Performs work accurately, completely and precisely. Meets deadlines.
                                                                        <br />
                                            <br />
                                            <asp:RadioButtonList ID="RadioButtonList3" Width="700px" runat="server" RepeatDirection="Horizontal" Enabled="False">
                                                <asp:ListItem Selected="True" Value="5">Outstanding</asp:ListItem>
                                                <asp:ListItem Value="4">Exceeds Expectations</asp:ListItem>
                                                <asp:ListItem Value="3">Meets Expectations</asp:ListItem>
                                                <asp:ListItem Value="2">Below Expectations</asp:ListItem>
                                                <asp:ListItem Value="1">Unsatisfactory</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" style="border: 1px solid black;">
                                    <tr>
                                        <td>4) Job Knowledge -<br />
                                            <br />
                                            Measures effectiveness in keeping knowledgeable of methods,
                                                                        <br />
                                            techniques and skills required in own job and related functions;<br />
                                            remaining current on new developments affecting SPSU and its work activities.
                                                                        <br />
                                            <br />
                                            <asp:RadioButtonList ID="RadioButtonList4" Width="700px" runat="server" RepeatDirection="Horizontal" Enabled="False">
                                                <asp:ListItem Selected="True" Value="5">Outstanding</asp:ListItem>
                                                <asp:ListItem Value="4">Exceeds Expectations</asp:ListItem>
                                                <asp:ListItem Value="3">Meets Expectations</asp:ListItem>
                                                <asp:ListItem Value="2">Below Expectations</asp:ListItem>
                                                <asp:ListItem Value="1">Unsatisfactory</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" style="border: 1px solid black;">
                                    <tr>
                                        <td>5) ATTENDANCE:<br />
                                            <br />
                                            Attends work each day unless on approved leave, such as vacation leave, sick leave,
                                                                        jury duty, etc.
                                                                        <br />
                                            <br />
                                            <asp:RadioButtonList ID="RadioButtonList5" Width="700px" runat="server" RepeatDirection="Horizontal" Enabled="False">
                                                <asp:ListItem Selected="True" Value="5">Outstanding</asp:ListItem>
                                                <asp:ListItem Value="4">Exceeds Expectations</asp:ListItem>
                                                <asp:ListItem Value="3">Meets Expectations</asp:ListItem>
                                                <asp:ListItem Value="2">Below Expectations</asp:ListItem>
                                                <asp:ListItem Value="1">Unsatisfactory</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" style="border: 1px solid black;">
                                    <tr>
                                        <td>6) WRITTEN AND ORAL COMMUNICATION:<br />
                                            <br />
                                            Writes and speaks clearly, accurately and concisely using correct vocabulary, spelling
                                                                        and grammar.
                                                                        <br />
                                            <br />
                                            <asp:RadioButtonList ID="RadioButtonList6" Width="700px" runat="server" RepeatDirection="Horizontal" Enabled="False">
                                                <asp:ListItem Selected="True" Value="5">Outstanding</asp:ListItem>
                                                <asp:ListItem Value="4">Exceeds Expectations</asp:ListItem>
                                                <asp:ListItem Value="3">Meets Expectations</asp:ListItem>
                                                <asp:ListItem Value="2">Below Expectations</asp:ListItem>
                                                <asp:ListItem Value="1">Unsatisfactory</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" style="border: 1px solid black;">
                                    <tr>
                                        <td>7) JUDGMENT/DECISION MAKING ABILITY:<br />
                                            <br />
                                            Makes the right, correct decisions in a timely manner. Recognizes the distinction
                                                                        between authority and responsibility.
                                                                        <br />
                                            <br />
                                            <asp:RadioButtonList ID="RadioButtonList7" Width="700px" runat="server" RepeatDirection="Horizontal" Enabled="False">
                                                <asp:ListItem Selected="True" Value="5">Outstanding</asp:ListItem>
                                                <asp:ListItem Value="4">Exceeds Expectations</asp:ListItem>
                                                <asp:ListItem Value="3">Meets Expectations</asp:ListItem>
                                                <asp:ListItem Value="2">Below Expectations</asp:ListItem>
                                                <asp:ListItem Value="1">Unsatisfactory</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" style="border: 1px solid black;">
                                    <tr>
                                        <td>8) INDUSTRIOUSNESS:<br />
                                            <br />
                                            Displays initiative. Is willing and able to work hard
                                                                        <br />
                                            <br />
                                            <asp:RadioButtonList ID="RadioButtonList8" Width="700px" runat="server" RepeatDirection="Horizontal" Enabled="False">
                                                <asp:ListItem Selected="True" Value="5">Outstanding</asp:ListItem>
                                                <asp:ListItem Value="4">Exceeds Expectations</asp:ListItem>
                                                <asp:ListItem Value="3">Meets Expectations</asp:ListItem>
                                                <asp:ListItem Value="2">Below Expectations</asp:ListItem>
                                                <asp:ListItem Value="1">Unsatisfactory</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" style="border: 1px solid black;">
                                    <tr>
                                        <td>9) COOPERATION WITH CO-WORKERS:<br />
                                            <br />
                                            Works as a team member. Relates well with co-workers and assists them when asked.
                                                                        <br />
                                            <br />
                                            <asp:RadioButtonList ID="RadioButtonList9" Width="700px" runat="server" RepeatDirection="Horizontal" Enabled="False">
                                                <asp:ListItem Selected="True" Value="5">Outstanding</asp:ListItem>
                                                <asp:ListItem Value="4">Exceeds Expectations</asp:ListItem>
                                                <asp:ListItem Value="3">Meets Expectations</asp:ListItem>
                                                <asp:ListItem Value="2">Below Expectations</asp:ListItem>
                                                <asp:ListItem Value="1">Unsatisfactory</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" style="border: 1px solid black;">
                                    <tr>
                                        <td>10) PUNCTUALITY:
                                                                        <br />
                                            <br />
                                            Arrives on time at the start of the workday. Does not abuse breaks or lunch periods
                                                                        by leaving early and/or returning late.
                                                                        <br />
                                            <br />
                                            <asp:RadioButtonList ID="RadioButtonList10" Width="700px" runat="server" RepeatDirection="Horizontal" Enabled="False">
                                                <asp:ListItem Selected="True" Value="5">Outstanding</asp:ListItem>
                                                <asp:ListItem Value="4">Exceeds Expectations</asp:ListItem>
                                                <asp:ListItem Value="3">Meets Expectations</asp:ListItem>
                                                <asp:ListItem Value="2">Below Expectations</asp:ListItem>
                                                <asp:ListItem Value="1">Unsatisfactory</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" style="border: 1px solid black;">
                                    <tr>
                                        <td>11) QUANTITY OF WORK:
                                                                        <br />
                                            <br />
                                            Performs a satisfactory volume of work during a given period of time.
                                                                        <br />
                                            <br />
                                            <asp:RadioButtonList ID="RadioButtonList11" Width="700px" runat="server" RepeatDirection="Horizontal" Enabled="False">
                                                <asp:ListItem Selected="True" Value="5">Outstanding</asp:ListItem>
                                                <asp:ListItem Value="4">Exceeds Expectations</asp:ListItem>
                                                <asp:ListItem Value="3">Meets Expectations</asp:ListItem>
                                                <asp:ListItem Value="2">Below Expectations</asp:ListItem>
                                                <asp:ListItem Value="1">Unsatisfactory</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                                <table width="100%" style="border: 1px solid black;">
                                    <tr>
                                        <td>12) COOPERATION WITH SUPERVISOR:
                                                                        <br />
                                            <br />
                                            Works with supervisor in carrying out tasks and following instructions.
                                                                        <br />
                                            <br />
                                            <asp:RadioButtonList ID="RadioButtonList12" Width="700px" runat="server" RepeatDirection="Horizontal" Enabled="False">
                                                <asp:ListItem Selected="True" Value="5">Outstanding</asp:ListItem>
                                                <asp:ListItem Value="4">Exceeds Expectations</asp:ListItem>
                                                <asp:ListItem Value="3">Meets Expectations</asp:ListItem>
                                                <asp:ListItem Value="2">Below Expectations</asp:ListItem>
                                                <asp:ListItem Value="1">Unsatisfactory</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
                <table cellpadding="2" cellspacing="2" width="90%">
                    <tr>
                        <td>
                            <fieldset>
                                <legend>Comments Relative to Performance Evaluation</legend>
                                <table cellpadding="2" cellspacing="2" width="100%" style="border: 1px solid black;">
                                    <tr>
                                        <td>Supervisor Comments Relative to Performance Evaluation:<br />
                                            (Attach additional pages if required.) General Comments: &nbsp; &nbsp; &nbsp;
                                                                        <asp:TextBox ID="txtsupervisorcomment" runat="server" TextMode="MultiLine" Height="100px" BackColor="White" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                                <table cellpadding="2" cellspacing="2" width="100%" style="border: 1px solid black;">
                                    <tr>
                                        <td>Employee Comments Relative to Performance Evaluation:<br />
                                            (Attach additional pages if required.) General Comments: &nbsp; &nbsp; &nbsp;
                                                                        <asp:TextBox ID="txtEmployeeComent" runat="server" TextMode="MultiLine" Height="100px" BackColor="White" Enabled="False"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </fieldset>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="panel4" runat="server" Width="90%">
                    &nbsp;&nbsp;
                                                <table width="100%" style="border: 1px solid black">
                                                    <tr>
                                                        <td>I have been given the opportunity to review the performance appraisal. I have discussed
                                                            this evaluation with my supervisor. I know that I can receive a copy of this appraisal
                                                            if so desired.
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">

                                                            <table width="100%">
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:Label ID="Label12" Text="  Employee’s Name and Signature  :" runat="server"
                                                                            CssClass="countStylelable" />
                                                                        <asp:Label ID="Label26" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                    </td>
                                                                    <td valign="top">
                                                                        <asp:Label ID="lblenpnameSing" runat="server"></asp:Label>
                                                                        <%-- <asp:DropDownList ID="ddEmployee" runat="server" Visible="false" AutoPostBack="True" CssClass="form-control"
                                                                            Width="60%" CausesValidation="True" ValidationGroup="a">
                                                                        </asp:DropDownList>
                                                                       
                                                                        <asp:CompareValidator ID="c1" runat="server" ControlToValidate="ddEmployee"
                                                                            ErrorMessage="Select EmpName" Operator="NotEqual"
                                                                            ValidationGroup="a" ValueToCompare="--Select--" Font-Size="9pt"
                                                                            ForeColor="Red"></asp:CompareValidator>--%>

                                                                    </td>
                                                                    <td valign="top">&nbsp;<asp:Label ID="Label3" Text="  Date  :" runat="server" CssClass="countStylelable" />
                                                                        &nbsp;<asp:Label ID="Label28" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                    </td>
                                                                    <td valign="top">
                                                                        <asp:TextBox ID="Txtdate" runat="server" Width="60%" CssClass="form-control" BackColor="White" Enabled="False"></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="Txtdate">
                                                                        </asp:CalendarExtender>


                                                                        <asp:RequiredFieldValidator ID="r3" runat="server" ControlToValidate="Txtdate"
                                                                            Display="Static" ForeColor="Red" ErrorMessage="*" ValidationGroup="a"
                                                                            SetFocusOnError="True" Font-Size="9pt"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td valign="top">
                                                                        <asp:Label ID="Label5" Text="  Supervisor’s Name and Signature  :" runat="server"
                                                                            CssClass="countStylelable" />
                                                                        &nbsp;<asp:Label ID="Label27" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                                                    </td>
                                                                    <td valign="top">
                                                                        <asp:TextBox ID="TextBox4" runat="server" Width="60%" CssClass="form-control" BackColor="White" Enabled="False"></asp:TextBox>
                                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server" Enabled="True"
                                                                            TargetControlID="TextBox4" ValidChars="abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                                                        </asp:FilteredTextBoxExtender>

                                                                        <asp:RequiredFieldValidator ID="r1" runat="server" ControlToValidate="TextBox4"
                                                                            Display="Static" ForeColor="Red" ErrorMessage="*" ValidationGroup="a"
                                                                            SetFocusOnError="True" Font-Size="9pt"></asp:RequiredFieldValidator>
                                                                    </td>
                                                                    <td valign="top">&nbsp;<asp:Label ID="Label6" Text="  Date  :" runat="server" CssClass="countStylelable" />
                                                                        &nbsp;<asp:Label ID="Label29" runat="server" ForeColor="Red" Text="*"></asp:Label>

                                                                    </td>
                                                                    <td>
                                                                        <asp:TextBox ID="txtdate1" runat="server" Width="60%" CssClass="form-control" BackColor="White" Enabled="False"></asp:TextBox>
                                                                        <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtdate1">
                                                                        </asp:CalendarExtender>

                                                                        <asp:RequiredFieldValidator ID="r4" runat="server" ControlToValidate="txtdate1"
                                                                            Display="Static" ForeColor="Red" ErrorMessage="*" ValidationGroup="a"
                                                                            SetFocusOnError="True" Font-Size="9pt"></asp:RequiredFieldValidator>

                                                                        <br />

                                                                        <asp:CompareValidator ID="CompareValidator3" runat="server"
                                                                            ControlToValidate="txtdate1" ErrorMessage="Please enter a Valid Date"
                                                                            Font-Size="9pt" ForeColor="Red" Operator="DataTypeCheck" Type="Date"
                                                                            ValidationGroup="a" />

                                                                    </td>

                                                                </tr>
                                                            </table>


                                                        </td>



                                                </table>
                </asp:Panel>

                <asp:UpdatePanel ID="updddda" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="panel5" runat="server" Width="90%" Height="30%">
                            <table width="100%" style="border: 1px solid black;">
                                <tr>
                                    <td colspan="4">Department Head or other Manager’s Signature Date (As the rater’s supervisor,
                                                            <br />
                                        my signature does not acknowledge agreement or disagreement with this rating.
                                                            <br />
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 350px">
                                        <asp:Label ID="Label7" Text="Department Head or other Manager’s Name and Signature  :"
                                            runat="server" CssClass="countStylelable" Width="60%" />

                                        &nbsp;<asp:Label ID="Label30" runat="server" ForeColor="Red" Text="*"></asp:Label>
                                        &nbsp;&nbsp; </td>
                                    <td>
                                        <asp:TextBox ID="Text5" runat="server" Width="60%" CssClass="form-control" BackColor="White" Enabled="False"></asp:TextBox>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server" Enabled="True"
                                            TargetControlID="Text5" ValidChars="abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                        </asp:FilteredTextBoxExtender>

                                        <asp:RequiredFieldValidator ID="r2" runat="server" ControlToValidate="Text5"
                                            Display="Static" ForeColor="Red" ErrorMessage="*" ValidationGroup="a"
                                            SetFocusOnError="True" Font-Size="9pt"></asp:RequiredFieldValidator>

                                    </td>
                                    <td valign="top">&nbsp;<asp:Label ID="Label8" Text="  Date  :" runat="server" CssClass="countStylelable" />
                                        &nbsp;<asp:Label ID="Label31" runat="server" ForeColor="Red" Text="*"></asp:Label>

                                    </td>
                                    <td valign="top">
                                        <asp:TextBox ID="txtdate2" runat="server" Width="60%" BackColor="White" CssClass="form-control" Enabled="False"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtdate2" PopupPosition="TopLeft">
                                        </asp:CalendarExtender>
                                        <asp:RequiredFieldValidator ID="r5" runat="server" ControlToValidate="txtdate2"
                                            Display="Static" ForeColor="Red" ErrorMessage="*" ValidationGroup="a"
                                            SetFocusOnError="True" Font-Size="9pt"></asp:RequiredFieldValidator>

                                        <asp:CompareValidator ID="CompareValidator2" runat="server"
                                            ControlToValidate="txtdate2" ErrorMessage="Please enter a Valid Date"
                                            Font-Size="9pt" ForeColor="Red" Operator="DataTypeCheck" Type="Date"
                                            ValidationGroup="a" />

                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>

                    </ContentTemplate>
                </asp:UpdatePanel>
                <asp:Panel ID="panel6" runat="server">
                    <table width="100%">
                        <tr>

                            <td align="center">

                                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn bg-blue-active"
                                    Height="35px" OnClick="btnBack_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </div>
    </section>




</asp:Content>

