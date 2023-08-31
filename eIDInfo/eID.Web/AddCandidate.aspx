<%@ Page Title="Candidate Info" Language="C#" MasterPageFile="~/UserMaster.master" AutoEventWireup="true" CodeFile="AddCandidate.aspx.cs" Inherits="Recruitment_AddCandidate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script>
        function calDate(value) {
            var today = new Date();
            var nowyear = today.getFullYear();
            var nowmonth = today.getMonth();
            var nowday = today.getDate();
            var birth = new Date(value);
            var birthyear = birth.getFullYear();
            var birthmonth = birth.getMonth();
            var birthday = birth.getDate();
            var age = nowyear - birthyear;
            var age_month = nowmonth - birthmonth;
            var age_day = nowday - birthday;

            if (age_month < 0 || (age_month == 0 && age_day < 0)) {
                age = parseInt(age) - 1;
            }
            if ((age == 18 && age_month <= 0 && age_day <= 0) || age < 18) {

                alert("Invalid Date");
                txtdob.value = null;
            }

        }

    </script>

    <style type="text/css">
        .form input[type="file"] {
            z-index: 999;
            line-height: 0;
            font-size: 50px;
            position: absolute;
            opacity: 0;
            filter: alpha(opacity = 0);
            -ms-filter: "alpha(opacity=0)";
            cursor: pointer;
            _cursor: hand;
            margin: 0;
            padding: 0;
            left: 0;
        }

        .add-photo-btn {
            position: absolute;
            overflow: hidden;
            cursor: pointer;
            text-align: center;
            background-color: #8bc804;
            color: #fff;
            display: block;
            width: 120px;
            height: 31px;
            font-size: 18px;
            line-height: 35px;
            float: left;
            -moz-border-radius: 10px;
            -webkit-border-radius: 10px;
            border-radius: 10px;
        }

        input[type="text"] {
            float: left;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     
    <asp:UpdatePanel ID="updemp" runat="server">
        <Triggers>
            <asp:PostBackTrigger ControlID="chksame" />
                <asp:PostBackTrigger ControlID="chksame" />
            <asp:PostBackTrigger ControlID="btnuploadresume" />
        </Triggers>
        <ContentTemplate>    
        
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
                <asp:View ID="View1" runat="server">
                
                    
                     <div class="col-md-12">
                        <div>
                            <div>
                                <div class="form-group">
                                    <table width="100%">
                                        <tr>
                                            <td>
                                                <asp:Button ID="btnaddnew" runat="server" CssClass="btn bg-blue-active"
                                                    Text="Add New" OnClick="btnaddnew_Click" />
                                                &nbsp;
                                            </td>
                                        </tr>
                                    </table>

                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="col-md-12">
                        <div class="box box-primary">
                            <div class="box-body">
                                <div class="form-group">

                                    <table width="55%">
                                        <tr align="left">

                                            <td>Select Company :
                                                    <asp:DropDownList ID="ddlSearchCompany" runat="server" AutoPostBack="True" CssClass="form-control" Width="350">
                                                    </asp:DropDownList>

                                            </td>
                                            <td>
                                                <br />
                                                <asp:Button runat="server" ID="btnSearch" Text="Search" CssClass="btn bg-blue-active" OnClick="btnSearch_Click" />
                                                <asp:Button runat="server" ID="btnReset" Text="Reset" CssClass="btn bg-blue-active" OnClick="btnReset_Click" />
                                            </td>


                                        </tr>
                                    </table>
                                    <table width="100%" cellspacing="8px">

                                        <tr>
                                            <td align="right">
                                               <b> <asp:Label ID="Label14" runat="server" Text="No. Of Count :" Font="Bold"></asp:Label>
                                                &nbsp;
                                                  <asp:Label ID="lblcount" runat="server" Font="Bold">0</asp:Label></b>
                                                <asp:Label ID="lblcandidateID" runat="server" Visible="false"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:GridView ID="grdcandidatelist" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped" Width="99%" AllowPaging="True" OnPageIndexChanging="grdcandidatelist_PageIndexChanging">
                                                    <Columns>
                                                        <asp:TemplateField HeaderText="Sr. No." HeaderStyle-Width="50px" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:Label Text='<%#Container.DataItemIndex + 1 %>' runat="server" ID="srno" />
                                                            </ItemTemplate>
                                                            <HeaderStyle HorizontalAlign="Center" Width="50px" />
                                                        </asp:TemplateField>
                                                        <asp:BoundField DataField="Entry_Date" HeaderText="Entry Date" DataFormatString="{0:dd/MM/yyyy}" />
                                                        <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                                        <asp:BoundField DataField="Vacancy" HeaderText="Vacancy Title" />
                                                        <asp:BoundField DataField="Name" HeaderText="Candidate Name" />
                                                        <asp:BoundField DataField="CandidateId_No" HeaderText="Candidate Code" />
                                                        <asp:BoundField DataField="DOB" HeaderText="DOB" DataFormatString="{0:dd/MM/yyyy}" />
                                                        <asp:BoundField DataField="Religion" HeaderText="Religion" Visible="false" />
                                                        <asp:BoundField DataField="Nationality" HeaderText="Nationality" Visible="false" />
                                                        <asp:BoundField DataField="Contact_No" HeaderText="Contact No." />
                                                        <asp:BoundField DataField="Email_Address" HeaderText="Email Id" />


                                                        <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px">
                                                            <ItemTemplate>
                                                                <asp:ImageButton ID="ImageEditCanditList" runat="server" CommandArgument='<%# Eval("Candidate_ID") %>'
                                                                ToolTip="Edit"    Height="20px" ImageUrl="~/Images/i_edit.png" Width="20px" OnClick="ImageEditCanditList_Click" />

                                                            </ItemTemplate>
                                                            <HeaderStyle Width="70px" />
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        No Record Exist....!!!!!!!!!!!!!!
                                                    </EmptyDataTemplate>
                                                </asp:GridView>
                                            </td>
                                        </tr>




                                    </table>

                                </div>
                            </div>
                        </div>
                    </div>

                </asp:View>


                <asp:View ID="View2" runat="server">
                    <div class="row">
                        <div class="col-md-6">
                            <div class="box box-primary">
                                <div class="box-body">
                                    <div class="form-group">
                                        <fieldset>
                                            <legend>Vacancy Info</legend>
                                            <table width="100%" cellspacing="8px">
                                                <tr>
                                                    <td >
 <div class="form-group">Select Company:
                                                    <asp:Label ID="Label15" runat="server" Font-Bold="True" Font-Size="Small" ForeColor="Red" Text="*"></asp:Label>
                                                  </div>  </td>
                                                    <td>
                                                        
 <div class="form-group">
                                                        <asp:DropDownList ID="ddlComp" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="ddlComp_SelectedIndexChanged">
                                                        </asp:DropDownList>
     </div>
                                                    </td>

                                                    <td>
                                                        <asp:RequiredFieldValidator Font-Size="12pt" ID="RequiredFieldValidator12" runat="server" ControlToValidate="ddlComp" Display="Dynamic" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td >
 <div class="form-group">Vacancy :
                                                    <asp:Label ID="Label1" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                   </div> </td>
                                                    <td>
 <div class="form-group">
                                                        <asp:DropDownList ID="ddlvaccancy" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlvaccancy_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>

                                                           </div>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator Font-Size="12pt" ID="RequiredFieldValidator17" runat="server" ControlToValidate="ddlvaccancy" Display="Dynamic" ErrorMessage="*" ForeColor="Red" InitialValue="--Select--" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr id="trVcodedate" runat="server" visible="false">
                                                    <td >
                                                        <asp:Label ID="lblvCodedate" runat="server"></asp:Label>
                                                    </td>
                                                    <td >&nbsp; &nbsp;
                                                   
 <div class="form-group">Code :
                                                    <asp:Label ID="lblcode" runat="server"></asp:Label>
                                                        &nbsp; &nbsp;&nbsp; &nbsp; 
                                                   Date : 
                                                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                                                       </div>

                                                    </td>
                                                </tr>


                                                <tr>
                                                    <td >
 <div class="form-group">Name :
                                                    <asp:Label ID="Label2" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                  </div>  </td>
                                                    <td>
 <div class="form-group">
                                                        <asp:TextBox ID="txtname" runat="server" CssClass="form-control"></asp:TextBox>

                                                        <asp:FilteredTextBoxExtender ID="txtname_FilteredTextBoxExtender" runat="server"
                                                            Enabled="True" TargetControlID="txtname" ValidChars="abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ">
                                                        </asp:FilteredTextBoxExtender>
                                    </div>

                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator Font-Size="12pt" ID="RequiredFieldValidator8" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtname" ValidationGroup="S" Display="Dynamic"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td>
                                                        <div class="form-group">
                                                            DOB :
                                                        <asp:Label ID="Labeldfsgdfg14" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                        </div>
                                                    </td>
                                                    <td> <div class="form-group">
                                                        <asp:TextBox ID="txtdob" runat="server" CssClass="form-control" OnTextChanged="txtdob_TextChanged" AutoPostBack="True"></asp:TextBox>
                                                       
                                    

                                    <asp:CalendarExtender ID="dtprelivedate_CalendarExtender" runat="server" Enabled="True"
                                        TargetControlID="txtdob" Format="MM/dd/yyyy">
                                    </asp:CalendarExtender>
</div>


                                    </td>
                                                <td>
                                                    <asp:RequiredFieldValidator Font-Size="12pt" ID="RequiredFieldValidatorDate" runat="server" ControlToValidate="txtdob" Display="Dynamic" ErrorMessage="*" ForeColor="Red" ValidationGroup="S"></asp:RequiredFieldValidator>
                                    </td>
                                                </tr>



                                                <tr>
                                                    <td ><div class="form-group">Gender : </div>
                                                    </td>
                                                    <td><div class="form-group">
                                                        <asp:RadioButtonList ID="rdgender" runat="server" CssClass="form-control" RepeatDirection="Horizontal">
                                                            <asp:ListItem Selected="True">Male</asp:ListItem>
                                                            <asp:ListItem>Female</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                       </div>
                                                    </td>
                                                </tr>

                                    <tr>
                                        <td ><div class="form-group">Contact No. :
                                                    <asp:Label ID="Label4" runat="server" Text="*" ForeColor="Red"></asp:Label>

                                            <asp:Label ID="lblcompanyId" runat="server" Visible="false"></asp:Label>
                                            </div>
                                        </td>
                                        <td><div class="form-group">
                                            <asp:TextBox ID="txtcontactno" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                                Enabled="True" TargetControlID="txtcontactno" ValidChars="0123456789">
                                            </asp:FilteredTextBoxExtender> </div>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator Font-Size="12pt" ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtcontactno" ErrorMessage="*" ForeColor="Red" ValidationGroup="S"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtcontactno" Display="Dynamic" ErrorMessage="*" ForeColor="Red" ValidationExpression="\d{10}" ValidationGroup="S" Font-Size="12pt"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="form-group">
                                                Alt. Contact No. :
                                            </div>
                                        </td>
                                        <td>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtaltcontact" runat="server" CssClass="form-control" MaxLength="10"></asp:TextBox>
                                                <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
                                                    Enabled="True" TargetControlID="txtaltcontact" ValidChars="0123456789">
                                                </asp:FilteredTextBoxExtender>
                                            </div>
                                        </td>
                                        <td>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtaltcontact" Display="Dynamic" ErrorMessage="*" ForeColor="Red" ValidationExpression="\d{10}" ValidationGroup="S" Font-Size="12pt"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="form-group">
                                                Email :
                                                    <asp:Label ID="Label5" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                            </div>
                                        </td>
                                        <td>
                                            <div class="form-group">
                                                <asp:TextBox ID="txtemail" runat="server" CssClass="form-control"></asp:TextBox>
                                            </div>
                                        </td>
                                        <td>
                                            <asp:RequiredFieldValidator Font-Size="12pt" ID="RequiredFieldValidator3" runat="server" ErrorMessage="*" ForeColor="Red" ControlToValidate="txtemail" ValidationGroup="S" Display="Dynamic"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtemail"
                                                Display="Dynamic" ForeColor="Red" ErrorMessage="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                ValidationGroup="S" SetFocusOnError="True" Font-Size="12pt"></asp:RegularExpressionValidator></td>
                                    </tr>



                                    </table>
                                        </fieldset>

                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="col-md-6">
                        <div class="box box-primary">
                            <div class="box-body">
                                <div class="form-group">
                                    <fieldset>
                                        <legend>Vacancy Info / Photo</legend>
                                       

                                        

                                        <table width="90%">
                                            <tr>
                                                <td></td>
                                                <td>
                                                    <div style="margin-left: 150px">
                                                        <asp:Image ID="image1" runat="server" Height="100px" Width="150px" Visible="true" />

                                                        <br />
                                                        <br />
                                                        <label class="add-photo-btn">
                                                            Add Image<span><asp:FileUpload ID="FileUpload2" runat="server" accept="image/*" EnableTheming="True"
                                                                onchange="this.form.submit();" Font-Size="8pt" /></span>
                                                        </label>
                                                        <asp:Label ID="lblAttachPath" runat="server" Visible="False">
                                                        </asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                            <%-- <tr>
                                                <td>
                                                    <div style="float: right; margin-right: 152px;">
                                                    </div>
                                                </td>
                                            </tr>--%>
                                        </table>
   

                                        <table width="100%" cellspacing="8px">
                                            <caption>
                                                <br />
                                                <br />

                                                <tr>
                                                    <td ><div class="form-group">Candidate Code :</div></td>
                                                    <td><div class="form-group">
                                                        <asp:TextBox ID="txtcandidateId" runat="server" BorderColor="White" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                                       </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td ><div class="form-group">Marital Status :</div> </td>
                                                    <td><div class="form-group">
                                                        <asp:RadioButtonList ID="rd_maritalstatus" runat="server" CssClass="form-control" RepeatDirection="Horizontal">
                                                            <asp:ListItem Selected="True">Single</asp:ListItem>
                                                            <asp:ListItem>Married</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td ><div class="form-group">Religion : </div> </td>
                                                    <td><div class="form-group">
                                                        <asp:DropDownList ID="ddlreligion" runat="server" CssClass="form-control">
                                                            <asp:ListItem>Hinduism</asp:ListItem>
                                                            <asp:ListItem>Muslim</asp:ListItem>
                                                            <asp:ListItem>Sikhism</asp:ListItem>
                                                            <asp:ListItem>Buddhism</asp:ListItem>
                                                        </asp:DropDownList>
                                                         </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td ><div class="form-group">Nationality : </div> </td>
                                                    <td><div class="form-group">
                                                        <asp:DropDownList ID="ddlnational" runat="server" CssClass="form-control">
                                                            <asp:ListItem>Indian</asp:ListItem>
                                                            <asp:ListItem>Pakistan</asp:ListItem>
                                                            <asp:ListItem>Bangladesh</asp:ListItem>
                                                            <asp:ListItem>Nepal</asp:ListItem>
                                                            <asp:ListItem>American</asp:ListItem>
                                                        </asp:DropDownList>
                                                        </div>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td ><div class="form-group">PAN No. : 
                                                    <asp:Label ID="Label16" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                   </div> </td>
                                                    <td><div class="form-group">
                                                        <asp:TextBox ID="txtpanno" runat="server" CssClass="form-control" MaxLength="15"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
                                                            Enabled="True" TargetControlID="txtpanno" ValidChars="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890">
                                                        </asp:FilteredTextBoxExtender>
</div>

                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator Font-Size="12pt" ID="RequiredFieldValidator18" runat="server" ControlToValidate="txtpanno" ErrorMessage="*" ForeColor="Red" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td ><div class="form-group">
                                                        <asp:CheckBox ID="chkpassport" runat="server" AutoPostBack="True" OnCheckedChanged="chkpassport_CheckedChanged" />
                                                        Pass Port : </div></td>
                                                    <td><div class="form-group">
                                                        <asp:TextBox ID="txtpassport" runat="server" CssClass="form-control" ReadOnly="True"></asp:TextBox>
                                                          </div>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator Font-Size="12pt" ID="RequiredFieldValidator19" runat="server" ControlToValidate="txtpassport" Display="Dynamic" Enabled="false" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2">&nbsp; </td>
                                                </tr>
                                            </caption>
                                        </table>
                                    </fieldset>

                                </div>
                            </div>
                        </div>
                    </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="box box-primary">
                                <div class="box-body">
                                    <div class="form-group">
                                        <fieldset>
                                            <legend>Current Address</legend>
                                            <table width="100%" cellspacing="8px">
                                                <tr>
                                                    <td colspan="2">&nbsp;
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td >
 <div class="form-group">State :
                                                    <asp:Label ID="Label6" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                   </div> </td>
                                                    <td>
 <div class="form-group">
                                                        <asp:DropDownList ID="ddlCsate" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlCsate_SelectedIndexChanged"></asp:DropDownList>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator Font-Size="12pt" ID="RequiredFieldValidator15" runat="server" ControlToValidate="ddlCsate" Display="Dynamic" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>

                                                <tr>
                                                    <td >
 <div class="form-group">City :
                                                    <asp:Label ID="Label7" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                   </div> </td>
                                                    <td>
 <div class="form-group">
                                                        <asp:DropDownList ID="ddlCcity" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                        </div>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator Font-Size="12pt" ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlCcity" Display="Dynamic" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >
 <div class="form-group">Zip Code :</div>
                                                    


                                                    </td>
                                                    <td>
 <div class="form-group">
                                                        <asp:TextBox ID="txtCzipcode" runat="server" CssClass="form-control" MaxLength="6"></asp:TextBox>

                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                                            Enabled="True" TargetControlID="txtCzipcode" ValidChars="0123456789">
                                                        </asp:FilteredTextBoxExtender>

                                                       </div>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td >
 <div class="form-group">Landmark : </div>
                                                  
                                                    </td>
                                                    <td>
 <div class="form-group">
                                                        <asp:TextBox ID="txtClandmark" runat="server" CssClass="form-control"></asp:TextBox>
                                                       </div>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>

                                            </table>
                                        </fieldset>

                                    </div>
                                </div>
                            </div>
                        </div>



                        <div class="col-md-6">
                            <div class="box box-primary">
                                <div class="box-body">
                                    <div class="form-group">
                                        <fieldset>
                                            <legend>Permanent Address</legend>




                                            <table width="100%" cellspacing="8px">
                                                <tr>

                                                    <td>

                                                         <div class="form-group">
                                                        <div style="display: none">
                                                            <asp:CheckBox ID="chksame" runat="server" Text="Same" AutoPostBack="True" OnCheckedChanged="chksame_CheckedChanged" />
                                                        </div>
                                                        <asp:CheckBox ID="CheckBox1"  runat="server" AutoPostBack="True" OnCheckedChanged="chksame_CheckedChanged" Text="Same" />
</div>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >State :
                                                    <asp:Label ID="Label10" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlPstatte" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlPstatte_SelectedIndexChanged"></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator Font-Size="12pt" ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlPstatte" Display="Dynamic" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >City :
                                                    <asp:Label ID="Label11" runat="server" Text="*" ForeColor="Red"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlPcity" runat="server" CssClass="form-control"></asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator Font-Size="12pt" ID="RequiredFieldValidator9" runat="server" ControlToValidate="ddlPcity" Display="Dynamic" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >Zip Code :
                                                    <%--<asp:Label ID="Label12" runat="server" Text="*" ForeColor="Red"></asp:Label>--%>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtPzipcode" runat="server" CssClass="form-control" MaxLength="6"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
                                                            Enabled="True" TargetControlID="txtPzipcode" ValidChars="0123456789">
                                                        </asp:FilteredTextBoxExtender>

                                                        <%-- <asp:RequiredFieldValidator Font-Size="12pt" ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtPzipcode"
                                                         Display="Dynamic"  ForeColor="Red" ErrorMessage="Enter ZipCode" ValidationGroup="S"
                                                        SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>--%>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td >Landmark :
                                                    <%--<asp:Label ID="Label13" runat="server" Text="*" ForeColor="Red"></asp:Label>--%>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtPlandmark" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator Font-Size="12pt" ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtPlandmark"
                                                         Display="Dynamic"  ForeColor="Red" ErrorMessage="Enter Landmark" ValidationGroup="S"
                                                        SetFocusOnError="True">
                                                    </asp:RequiredFieldValidator>--%>
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>

                                            </table>
                                        </fieldset>

                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="row">







                        <div class="col-md-6">
                            <div class="box box-primary">
                                <div class="box-body">
                                    <div class="form-group">
                                        <fieldset>
                                            <legend>Profession</legend>
                                            <table width="100%" cellspacing="8px">
                                                <tr>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td>Year
                                                    </td>
                                                    <td>Month
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td>Total Experience :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlyear" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="0" Selected="true">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                            <asp:ListItem Value="11">11</asp:ListItem>
                                                            <asp:ListItem Value="12">12</asp:ListItem>
                                                            <asp:ListItem Value="13">13</asp:ListItem>
                                                            <asp:ListItem Value="14">14</asp:ListItem>
                                                            <asp:ListItem Value="15">15</asp:ListItem>
                                                        </asp:DropDownList>


                                                        <%-- <asp:TextBox ID="txttotalexp" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                          Enabled="True" TargetControlID="txttotalexp" ValidChars="0123456789"></asp:FilteredTextBoxExtender>--%>

                                    
                                                    </td>

                                                    <td>
                                                        <%-- <asp:DropDownList ID="ddlperiod" runat="server" CssClass="form-control">
                                       <asp:ListItem selected="true">Year</asp:ListItem>
                                       <asp:ListItem>Month</asp:ListItem>
                                   </asp:DropDownList>--%>


                                                        <asp:DropDownList ID="ddlmonth" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="0" Selected="true">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                            <asp:ListItem Value="11">11</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td>Year
                                                    </td>
                                                    <td>Month
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td>Relevant Experience :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlrelyear" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="0" Selected="true">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                            <asp:ListItem Value="11">11</asp:ListItem>
                                                            <asp:ListItem Value="12">12</asp:ListItem>
                                                            <asp:ListItem Value="13">13</asp:ListItem>
                                                            <asp:ListItem Value="14">14</asp:ListItem>
                                                            <asp:ListItem Value="15">15</asp:ListItem>

                                                        </asp:DropDownList>
                                                        <%--<asp:TextBox ID="txtrelevent" runat="server" CssClass="form-control"></asp:TextBox>
                                    <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
                                          Enabled="True" TargetControlID="txtrelevent" ValidChars="0123456789"></asp:FilteredTextBoxExtender>--%>
                                    
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlrelmont" runat="server" CssClass="form-control">
                                                            <asp:ListItem Value="0" Selected="true">0</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="3">3</asp:ListItem>
                                                            <asp:ListItem Value="4">4</asp:ListItem>
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="6">6</asp:ListItem>
                                                            <asp:ListItem Value="7">7</asp:ListItem>
                                                            <asp:ListItem Value="8">8</asp:ListItem>
                                                            <asp:ListItem Value="9">9</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                            <asp:ListItem Value="11">11</asp:ListItem>

                                                        </asp:DropDownList>

                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="top">Notice Period(In days) :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtnoticeperiod" runat="server" CssClass="form-control" MaxLength="2"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
                                                            Enabled="True" TargetControlID="txtnoticeperiod" ValidChars="0123456789">
                                                        </asp:FilteredTextBoxExtender>
                                                        <br />
                                                    </td>
                                                    <%-- <td>
                         <asp:DropDownList ID="ddlnoticePeriod" runat="server" CssClass="form-control">
                                       <asp:ListItem selected="true">Days</asp:ListItem>
                                       <asp:ListItem>Month</asp:ListItem>
                                   </asp:DropDownList>
                                </td>
                                                    --%>
                                                </tr>
                                                <tr>
                                                    <td >Skills :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtskliis" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >Upload Resume :
                                                    </td>
                                                    <td>
                                                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="form-control" />
                                                        <br />

                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnuploadresume" Text="Upload" runat="server" CssClass="btn bg-blue-active" ValidationGroup="r" OnClick="btnuploadresume_Click" />
                                                        <asp:Label ID="lblresumepath" runat="server"></asp:Label>
                                                    </td>
                                                </tr>

                                            </table>
                                        </fieldset>

                                    </div>
                                </div>
                            </div>
                        </div>



                        <div class="col-md-6">
                            <div class="box box-primary">
                                <div class="box-body">
                                    <div class="form-group">
                                        <fieldset>
                                            <legend>Experience Details</legend>
                                            <table width="100%" cellspacing="8px">
                                                <tr>
                                                    <td>Company Name
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>Designation
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtcompanyName" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="txtcompanyName_FilteredTextBoxExtender" runat="server" Enabled="True" ValidChars="abcdefghijklmnopqrstuvwxyz ABCDEFGHIJKLMNOPQRSTUVWXYZ" TargetControlID="txtcompanyName">
                                                        </asp:FilteredTextBoxExtender>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator Font-Size="12pt" ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtcompanyName" Display="Dynamic" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="X"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtdesignation" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>

                                                    <td>
                                                        <asp:RequiredFieldValidator Font-Size="12pt" ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtdesignation" Display="Dynamic" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="X"></asp:RequiredFieldValidator>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td>From Month                
                                                    </td>

                                                    <td>&nbsp;</td>

                                                    <td>Year   

                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td >
                                                        <asp:DropDownList ID="ddlFrommonth" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddlFrommonth_SelectedIndexChanged">
                                                            <asp:ListItem Value="1" Selected="True">Januray</asp:ListItem>
                                                            <asp:ListItem Value="2">Februray</asp:ListItem>
                                                            <asp:ListItem Value="3">March</asp:ListItem>
                                                            <asp:ListItem Value="4">April</asp:ListItem>
                                                            <asp:ListItem Value="5">May</asp:ListItem>
                                                            <asp:ListItem Value="6">June</asp:ListItem>
                                                            <asp:ListItem Value="7">July</asp:ListItem>
                                                            <asp:ListItem Value="8">Agust</asp:ListItem>
                                                            <asp:ListItem Value="9">September</asp:ListItem>
                                                            <asp:ListItem Value="10">October</asp:ListItem>
                                                            <asp:ListItem Value="11">November</asp:ListItem>
                                                            <asp:ListItem Value="12">December</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </td>

                                                    <td>&nbsp;</td>

                                                    <td>
                                                        <asp:TextBox ID="txtfromyear" runat="server" CssClass="form-control" MaxLength="4" AutoPostBack="True" OnTextChanged="txtfromyear_TextChanged"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
                                                            Enabled="True" TargetControlID="txtfromyear" ValidChars="0123456789">
                                                        </asp:FilteredTextBoxExtender>


                                                    </td>


                                                    <td>
                                                        <asp:RequiredFieldValidator Font-Size="12pt" ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtfromyear" Display="Dynamic" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="X"></asp:RequiredFieldValidator><br />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator4" runat="server" ControlToValidate="txtfromyear" Display="Dynamic" ErrorMessage="*" ForeColor="Red" ValidationExpression="\d{4}"></asp:RegularExpressionValidator>
                                                    </td>


                                                </tr>
                                                <tr>
                                                    <td>To Month

                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>To Year

                                                    </td>

                                                    <td>&nbsp;</td>

                                                </tr>
                                                <tr>
                                                    <td >
                                                        <asp:DropDownList ID="ddltommonth" runat="server" CssClass="form-control" AutoPostBack="True" OnSelectedIndexChanged="ddltommonth_SelectedIndexChanged">
                                                            <asp:ListItem Value="1" Selected="True">Januray</asp:ListItem>
                                                            <asp:ListItem Value="2">Februray</asp:ListItem>
                                                            <asp:ListItem Value="3">March</asp:ListItem>
                                                            <asp:ListItem Value="4">April</asp:ListItem>
                                                            <asp:ListItem Value="5">May</asp:ListItem>
                                                            <asp:ListItem Value="6">June</asp:ListItem>
                                                            <asp:ListItem Value="7">July</asp:ListItem>
                                                            <asp:ListItem Value="8">Agust</asp:ListItem>
                                                            <asp:ListItem Value="9">September</asp:ListItem>
                                                            <asp:ListItem Value="10">October</asp:ListItem>
                                                            <asp:ListItem Value="11">November</asp:ListItem>
                                                            <asp:ListItem Value="12">December</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:TextBox ID="txttoyear" runat="server" CssClass="form-control" MaxLength="4" AutoPostBack="True" OnTextChanged="txttoyear_TextChanged"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server"
                                                            Enabled="True" TargetControlID="txttoyear" ValidChars="0123456789">
                                                        </asp:FilteredTextBoxExtender>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator Font-Size="12pt" ID="RequiredFieldValidator25" runat="server" ControlToValidate="txttoyear" Display="Dynamic" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="X"></asp:RequiredFieldValidator>
                                                        <br />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ControlToValidate="txttoyear" Display="Dynamic" ErrorMessage="*" ForeColor="Red" ValidationExpression="\d{4}"></asp:RegularExpressionValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>Company  Address
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtcompanyAddress" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator Font-Size="12pt" ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtcompanyAddress" Display="Dynamic" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="X"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td >
                                                        <asp:Button ID="btnaddExperience" runat="server" Text="Add" CssClass="btn bg-blue-active" ValidationGroup="X" OnClick="btnaddExperience_Click" />
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp; 
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <asp:Panel ID="pnl" runat="server" >

                                                            <asp:GridView ID="grdexperience" runat="server" AutoGenerateColumns="false" Width="99%" CssClass="table table-bordered table-striped">
                                                                <Columns>
                                                                    <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
                                                                    <asp:BoundField DataField="Designation" HeaderText="Designation" />
                                                                    <asp:BoundField DataField="FromMonth" HeaderText="From Month" />
                                                                    <asp:BoundField DataField="FromYear" HeaderText="From Year" />

                                                                    <asp:BoundField DataField="ToMonth" HeaderText="From Month" />
                                                                    <asp:BoundField DataField="ToYear" HeaderText="From Year" />

                                                                    <asp:BoundField DataField="Address" HeaderText="Address" />

                                                                    <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px">
                                                                        <ItemTemplate>
                                                                            <asp:ImageButton ID="imgExpedit" runat="server" CommandArgument='<%# Eval("CompanyName") %>'
                                                                                Height="20px" ImageUrl="~/Images/i_edit.png" OnClick="imgExpedit_Click" />
                                                                            <asp:ImageButton ID="imgExpdelete" runat="server" CommandArgument='<%# Eval("CompanyName") %>'
                                                                                Height="20px" ImageUrl="~/Images/i_delete.png" Width="20px" OnClick="imgExpdelete_Click" />
                                                                            <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmOnFormSubmit="true"
                                                                                ConfirmText="Do you Really want to delete ..?" Enabled="True" TargetControlID="imgExpdelete">
                                                                            </asp:ConfirmButtonExtender>
                                                                        </ItemTemplate>
                                                                        <HeaderStyle Width="70px" />
                                                                    </asp:TemplateField>
                                                                </Columns>
                                                                <EmptyDataTemplate>
                                                                    No Record Exist....!!!!!!!!!!!!!!
                                                                </EmptyDataTemplate>
                                                            </asp:GridView>

                                                        </asp:Panel>


                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>

                                            </table>
                                        </fieldset>

                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                    <div class="row">
                        <div class="col-md-6">
                            <div class="box box-primary">
                                <div class="box-body">
                                    <div class="form-group">
                                        <fieldset>
                                            <legend>Other Info</legend>
                                            <table width="100%" cellspacing="8px">
                                                <tr>
                                                    <td >current CTC :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtCTC" runat="server" CssClass="form-control" MaxLength="7"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                            Enabled="True" TargetControlID="txtCTC" ValidChars="0123456789.">
                                                        </asp:FilteredTextBoxExtender>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >Expected CTC :
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtExpectedCTC" runat="server" CssClass="form-control" MaxLength="7"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                                                            Enabled="True" TargetControlID="txtExpectedCTC" ValidChars="0123456789.">
                                                        </asp:FilteredTextBoxExtender>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td >Reference By :
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlrefrenceby" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddlrefrenceby_SelectedIndexChanged" AutoPostBack="True">
                                                            <asp:ListItem Selected="True">--Select--</asp:ListItem>
                                                            <asp:ListItem>Agency</asp:ListItem>
                                                            <asp:ListItem>Employee</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr id="trrefren" runat="server" visible="false">
                                                    <td >
                                                        <asp:Label ID="lblrefnameby" runat="server"></asp:Label>
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="ddlrefNameid" runat="server" CssClass="form-control">
                                                        </asp:DropDownList>
                                                        <br />
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator Font-Size="12pt" ID="RequiredFieldValidator20" runat="server" ControlToValidate="ddlrefNameid" Display="Dynamic" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="S"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>




                                            </table>
                                        </fieldset>

                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="col-md-6">
                            <div class="box box-primary">
                                <div class="box-body">
                                    <div class="form-group">
                                        <fieldset>
                                            <legend>Education Qualification</legend>
                                            <table width="100%" cellspacing="8px">
                                                <tr>
                                                    <td>Education Name
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>Passing Year
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txteduname" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator  ID="RequiredFieldValidator16" runat="server" ControlToValidate="txteduname" Display="Dynamic" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="e" Font-Bold="False" Font-Size="12pt"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtpassingyear" runat="server" CssClass="form-control" AutoPostBack="True" OnTextChanged="txtpassingyear_TextChanged" MaxLength="4"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="txtpassingyear_FilteredTextBoxExtender" runat="server"
                                                            Enabled="True" TargetControlID="txtpassingyear" ValidChars="0123456789">
                                                        </asp:FilteredTextBoxExtender>
                                                    </td>

                                                    <td>
                                                        <asp:RequiredFieldValidator Font-Size="12pt" ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtpassingyear" Display="Dynamic" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="e"></asp:RequiredFieldValidator>
                                                        <br />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator8" runat="server" ControlToValidate="txtpassingyear" Display="Dynamic" ErrorMessage="*" ForeColor="Red" ValidationExpression="\d{4}"></asp:RegularExpressionValidator>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td>University
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>Percentage
                                                    </td>

                                                    <td>&nbsp;</td>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtuniversity" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:RequiredFieldValidator Font-Size="12pt" ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtuniversity" Display="Dynamic" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="e"></asp:RequiredFieldValidator>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtpercentage" runat="server" CssClass="form-control" MaxLength="3"></asp:TextBox>
                                                        <asp:FilteredTextBoxExtender ID="txtpercentage_FilteredTextBoxExtender" runat="server"
                                                            Enabled="True" TargetControlID="txtpercentage" ValidChars=".0123456789">
                                                        </asp:FilteredTextBoxExtender>

                                                    </td>

                                                    <td>
                                                        <asp:RequiredFieldValidator Font-Size="12pt" ID="RequiredFieldValidator26" runat="server" ControlToValidate="txtpercentage" Display="Dynamic" ErrorMessage="*" ForeColor="Red" SetFocusOnError="True" ValidationGroup="e"></asp:RequiredFieldValidator>
                                                        <br />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txtpercentage" Display="Dynamic" ErrorMessage="*" ForeColor="Red" ValidationExpression="^(100([\.][0]{1,})?$|[0-9]{2,2}([\.][0-9]{2,})?)$"></asp:RegularExpressionValidator>
                                                    </td>

                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnaddedu" runat="server" Text="Add" CssClass="btn bg-blue-active" ValidationGroup="e" OnClick="btnaddedu_Click" />
                                                    </td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp; 
                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <asp:GridView ID="grdeducation" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered table-striped" Width="99%">
                                                            <Columns>
                                                                <asp:BoundField DataField="EducationName" HeaderText="Education" />
                                                                <asp:BoundField DataField="YearOfPassing" HeaderText="Year Of Passing" />
                                                                <asp:BoundField DataField="University" HeaderText="University" />

                                                                <asp:BoundField DataField="ObtainPercent" DataFormatString="{0:0.00}" HeaderText="Percent" />
                                                                <asp:TemplateField HeaderText="Action" HeaderStyle-Width="70px">
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ID="imgedit" runat="server" CommandArgument='<%# Eval("EducationName") %>'
                                                                            Height="20px" ImageUrl="~/Images/i_edit.png" OnClick="imgedit_Click" Width="20px" />
                                                                        <asp:ImageButton ID="imgdelete" runat="server" CommandArgument='<%# Eval("EducationName") %>'
                                                                            Height="20px" ImageUrl="~/Images/i_delete.png" OnClick="imgdelete_Click" Width="20px" />
                                                                        <asp:ConfirmButtonExtender ID="ConfirmButtonExtender1" runat="server" ConfirmOnFormSubmit="true"
                                                                            ConfirmText="Do you Really want to delete ..?" Enabled="True" TargetControlID="imgdelete">
                                                                        </asp:ConfirmButtonExtender>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                            <EmptyDataTemplate>
                                                                No Record Exist....!!!!!!!!!!!!!!
                                                            </EmptyDataTemplate>
                                                        </asp:GridView>




                                                    </td>
                                                    <td>&nbsp;</td>
                                                </tr>

                                            </table>
                                        </fieldset>

                                    </div>
                                </div>
                            </div>
                        </div>


                    </div>
                    <div class="row">

                        <div class="col-md-4">
                            <div>
                                <div>
                                    <div class="form-group">

                                        <table width="100%" cellspacing="8px">
                                            <tr>
                                                <td>&nbsp;
                                                </td>
                                                <td>&nbsp;
                                                </td>
                                            </tr>
                                        </table>


                                    </div>
                                </div>
                            </div>
                        </div>

                        <div class="col-md-8">
                            <div>
                                <div>
                                    <div class="form-group">

                                        <table width="100%" cellspacing="8px">
                                            <tr>
                                                <td>
                                                    <asp:Button ID="btnsave" runat="server" CssClass="btn bg-blue-active" Text="Save" OnClick="btnsave_Click" ValidationGroup="S" />
                                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn bg-blue-active" Text="Cancel" OnClick="btnCancel_Click" />
                                                </td>
                                                <td>&nbsp;
                                                </td>
                                            </tr>
                                        </table>


                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </asp:View>

            </asp:MultiView>

        </ContentTemplate>
    </asp:UpdatePanel>




</asp:Content>

