using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class EmployeeBasicDetails : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    int flao = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
       
        if (Session["UserId"] != null)
        {

            if (!IsPostBack)
            {
                ddusertype.SelectedIndex = 2;
                BindAllEmp();
                MultiView1.ActiveViewIndex = 0;
                maxleadid();
                txtbirtdate.Attributes.Add("readonly", "readonly");
                txtdateofjoining.Attributes.Add("readonly", "readonly");
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void maxleadid()
    {

        string IDHint = "100";


        var data1 = from dt in HR.EmployeeTBs
                    select dt.EmployeeId;
        if (data1.Count() > 0)
        {


            var data = (from dt in HR.EmployeeTBs
                        select dt.EmployeeId).Max();

            string Empcode = (data + 1).ToString();

            lblempCode.Text = IDHint + "" + Empcode;

        }


    }

    protected int GetMenuID(string name)
    {
        try
        {
            int id = 0;
            DataTable dtcheck = g.ReturnData("select MenuID from MasterMenuHRMSTB where Menuname='" + name + "'");
            if (dtcheck.Rows.Count > 0)
            {
                id = Convert.ToInt32(dtcheck.Rows[0][0].ToString());
            }
            return id;
        }
        catch
        {
            return 0;
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        try
        {
            if (btnsubmit.Text == "Submit")
            {
                var empno = (from d in HR.EmployeeTBs select d).Count();
                var data = from d in HR.EmployeeTBs
                           where d.EmailId == txtEmail.Text
                           //  where d.FName + ' ' + d.Lname == txtfname.Text + ' ' + txtlname.Text && d.EmailId==txtEmail.Text
                           select d;
                if (data.Count() > 0)
                {
                    g.ShowMessage(this.Page, "Employee Details Already Exist.");
                    //modpop.Message = " Employee Details Already inserted Successfully.!!";
                    //modpop.ShowPopUp();

                }
                else
                {
                    var chkExissts = (from d in HR.EmployeeTBs where d.EmployeeNo == lblempCode.Text select d).Distinct();
                    if (chkExissts.Count() > 0)
                    {
                        g.ShowMessage(this.Page, "Employee code already present..");
                    }
                    else
                    {

                        EmployeeTB em = new EmployeeTB();
                        em.EmployeeNo = lblempCode.Text;// "100" + (empno + 1);
                        em.MachineID = txtDeviceCode.Text;
                        em.Solitude = ddlsalitude.SelectedIndex;
                        em.FName = txtfname.Text;
                        em.MName = txtmname.Text;
                        em.Lname = txtlname.Text;
                        em.EmailId = em.personalEmail = txtEmail.Text;

                        //txtbirtdate.Text = date.ToString("MM/dd/yyyy");
                        if (txtbirtdate.Text == "")
                        {
                        }
                        else
                        {
                            DateTime date = Convert.ToDateTime(em.BirthDate);
                            txtbirtdate.Text = txtbirtdate.Text.Replace("12:00:00 AM", " ");
                            em.BirthDate = Convert.ToDateTime(txtbirtdate.Text);
                        }
                        if (txtdateofjoining.Text == "")
                        {
                        }
                        else
                        {
                            DateTime date1 = Convert.ToDateTime(em.DOJ);
                            //txtdateofjoining.Text = date1.ToString("MM/dd/yyyy");
                            txtdateofjoining.Text = txtdateofjoining.Text.Replace("12:00:00 AM", " ");
                            em.DOJ = Convert.ToDateTime(txtdateofjoining.Text);
                        }


                        em.Gender = RbGender.SelectedValue;
                        em.EmployeeFlag = 0;
                        //em

                        HR.EmployeeTBs.InsertOnSubmit(em);
                        HR.SubmitChanges();

                        Session["EmpID5"] = em.EmployeeId.ToString();

                        RegistrationTB rg = new RegistrationTB();
                        Session["EmpCode"] = "100" + "" + em.EmployeeId;
                        rg.Flag = 0;
                        rg.Solitude = ddlsalitude.SelectedIndex;
                        rg.FirstName = txtfname.Text;
                        rg.MiddleName = txtmname.Text;
                        rg.UserName = txtfname.Text;
                        rg.Password = "123";
                        rg.LastName = txtlname.Text;
                        rg.EmployeeId = em.EmployeeId;
                        rg.UserType = ddusertype.SelectedValue;

                        Session["flag"] = 0;
                        HR.RegistrationTBs.InsertOnSubmit(rg);
                        HR.SubmitChanges();
                        SmtpGmail();

                        if (ddusertype.SelectedIndex == 2)
                        {
                            // FOR Employee 
                            UserAssignRollHRMSTB UTB = new UserAssignRollHRMSTB();
                            UTB.MenuId = GetMenuID("Home");


                            UTB.EmployeeId = Convert.ToInt32(Session["EmpID5"]);
                            UTB.UserId = Convert.ToInt32(Session["EmpID5"]);
                            UTB.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                            HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB);
                            HR.SubmitChanges();

                            UserAssignRollHRMSTB UTB1 = new UserAssignRollHRMSTB();
                            UTB1.MenuId = GetMenuID("My Profile");

                            UTB1.EmployeeId = Convert.ToInt32(Session["EmpID5"]);
                            UTB1.UserId = Convert.ToInt32(Session["EmpID5"]);
                            UTB1.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                            HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB1);
                            HR.SubmitChanges();

                            UserAssignRollHRMSTB UTB2 = new UserAssignRollHRMSTB();
                            UTB2.MenuId = GetMenuID("Description");
                            UTB2.EmployeeId = Convert.ToInt32(Session["EmpID5"]);
                            UTB2.UserId = Convert.ToInt32(Session["EmpID5"]);
                            UTB2.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                            HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB2);
                            HR.SubmitChanges();

                            UserAssignRollHRMSTB UTB3 = new UserAssignRollHRMSTB();
                            UTB3.MenuId = GetMenuID("Job Description");
                            UTB3.EmployeeId = Convert.ToInt32(Session["EmpID5"]);
                            UTB3.UserId = Convert.ToInt32(Session["EmpID5"]);
                            UTB3.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                            HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB3);
                            HR.SubmitChanges();

                            UserAssignRollHRMSTB UTB4 = new UserAssignRollHRMSTB();
                            UTB4.MenuId = GetMenuID("KRA Details");
                            UTB4.EmployeeId = Convert.ToInt32(Session["EmpID5"]);
                            UTB4.UserId = Convert.ToInt32(Session["EmpID5"]);
                            UTB4.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                            HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB4);
                            HR.SubmitChanges();


                            UserAssignRollHRMSTB UTB5 = new UserAssignRollHRMSTB();
                            UTB5.MenuId = GetMenuID("Salary");
                            UTB5.EmployeeId = Convert.ToInt32(Session["EmpID5"]);
                            UTB5.UserId = Convert.ToInt32(Session["EmpID5"]);
                            UTB5.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                            HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB5);
                            HR.SubmitChanges();

                            UserAssignRollHRMSTB UTB6 = new UserAssignRollHRMSTB();
                            UTB6.MenuId = GetMenuID("Leave");
                            UTB6.EmployeeId = Convert.ToInt32(Session["EmpID5"]);
                            UTB6.UserId = Convert.ToInt32(Session["EmpID5"]);
                            UTB6.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                            HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB6);
                            HR.SubmitChanges();

                            UserAssignRollHRMSTB UTB7 = new UserAssignRollHRMSTB();

                            UTB7.MenuId = GetMenuID("Leave Application");


                            UTB7.EmployeeId = Convert.ToInt32(Session["EmpID5"]);
                            UTB7.UserId = Convert.ToInt32(Session["EmpID5"]);
                            UTB7.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                            HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB7);
                            HR.SubmitChanges();

                            UserAssignRollHRMSTB UTB8 = new UserAssignRollHRMSTB();
                            UTB8.MenuId = GetMenuID("Leave Details");
                            UTB8.EmployeeId = Convert.ToInt32(Session["EmpID5"]);
                            UTB8.UserId = Convert.ToInt32(Session["EmpID5"]);
                            UTB8.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                            HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB8);
                            HR.SubmitChanges();

                            UserAssignRollHRMSTB UTB9 = new UserAssignRollHRMSTB();
                            UTB9.MenuId = GetMenuID("Change Password");
                            UTB9.EmployeeId = Convert.ToInt32(Session["EmpID5"]);
                            UTB9.UserId = Convert.ToInt32(Session["EmpID5"]);
                            UTB9.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                            HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB9);
                            HR.SubmitChanges();


                            UserAssignRollHRMSTB UTB10 = new UserAssignRollHRMSTB();
                            UTB10.MenuId = GetMenuID("Salary Slip");
                            UTB10.EmployeeId = Convert.ToInt32(Session["EmpID5"]);
                            UTB10.UserId = Convert.ToInt32(Session["EmpID5"]);
                            UTB10.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                            HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB10);
                            HR.SubmitChanges();

                            UserAssignRollHRMSTB UTB11 = new UserAssignRollHRMSTB();
                            UTB11.MenuId = GetMenuID("Advance / Expense Claim");
                            UTB11.EmployeeId = Convert.ToInt32(Session["EmpID5"]);
                            UTB11.UserId = Convert.ToInt32(Session["EmpID5"]);
                            UTB11.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                            HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB11);
                            HR.SubmitChanges();


                            UserAssignRollHRMSTB UTB12 = new UserAssignRollHRMSTB();
                            UTB12.MenuId = GetMenuID("Expense Claim");
                            UTB12.EmployeeId = Convert.ToInt32(Session["EmpID5"]);
                            UTB12.UserId = Convert.ToInt32(Session["EmpID5"]);
                            UTB12.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                            HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB12);
                            HR.SubmitChanges();


                            UserAssignRollHRMSTB UTB13 = new UserAssignRollHRMSTB();
                            UTB13.MenuId = GetMenuID("Advance Form");
                            UTB13.EmployeeId = Convert.ToInt32(Session["EmpID5"]);
                            UTB13.UserId = Convert.ToInt32(Session["EmpID5"]);
                            UTB13.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                            HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB13);
                            HR.SubmitChanges();


                        }

                        //modpop.Message = " Data inserted Successfully.!!";
                        //modpop.ShowPopUp();
                        g.ShowMessage(this.Page, " Employee Register saved successfully.");
                        //g.ShowMessage(this.Page, " Data inserted Successfully");
                        Clear();
                    }
                }
            }
            else
            {
                EmployeeTB MT = HR.EmployeeTBs.Where(d => d.EmployeeId == Convert.ToInt32(lblempid.Text)).First();
                MT.Solitude = ddlsalitude.SelectedIndex;
                MT.FName = txtfname.Text;
                MT.MName = txtmname.Text;
                MT.Lname = txtlname.Text;
                MT.EmailId = txtEmail.Text;
                Session["EmpCode"] = "100" + "" + lblempid.Text;
                //MT.BirthDate = Convert.ToDateTime(txtbirtdate.Text);
                //txtbirtdate.Text = MT.BirthDate.Value.ToString("MM/dd/yyyy");
                //txtbirtdate.Text = txtbirtdate.Text.Replace("12:00:00 AM", " ");

                if (string.IsNullOrEmpty(txtbirtdate.Text))
                {
                }
                else
                {
                    DateTime date = Convert.ToDateTime(MT.BirthDate);
                    txtbirtdate.Text = txtbirtdate.Text.Replace(" 12:00:00 AM", " ");
                    MT.BirthDate = Convert.ToDateTime(txtbirtdate.Text);
                }

                if (string.IsNullOrEmpty(txtdateofjoining.Text))
                {
                }
                else
                {
                    MT.DOJ = Convert.ToDateTime(txtdateofjoining.Text);
                    //txtdateofjoining.Text = MT.DOJ.Value.ToString("MM/dd/yyyy");
                    txtdateofjoining.Text = txtdateofjoining.Text.Replace("12:00:00 AM", " ");
                }
                MT.Gender = RbGender.SelectedIndex.ToString();
                HR.SubmitChanges();


                var dataexist = (from d in HR.UserAssignRollHRMSTBs where  d.EmployeeId == MT.EmployeeId select d).ToList();
                if(dataexist.Count()== 0)
                {
                    UserAssignRollHRMSTB UTB = new UserAssignRollHRMSTB();
                    UTB.MenuId = GetMenuID("Home");

                    UTB.EmployeeId = Convert.ToInt32(lblempid.Text);
                    UTB.UserId = Convert.ToInt32(lblempid.Text);
                    UTB.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                    HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB);
                    HR.SubmitChanges();

                    UserAssignRollHRMSTB UTB1 = new UserAssignRollHRMSTB();
                    UTB1.MenuId = GetMenuID("My Profile");

                    UTB1.EmployeeId = Convert.ToInt32(lblempid.Text);
                    UTB1.UserId = Convert.ToInt32(lblempid.Text);
                    UTB1.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                    HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB1);
                    HR.SubmitChanges();

                    UserAssignRollHRMSTB UTB2 = new UserAssignRollHRMSTB();
                    UTB2.MenuId = GetMenuID("Description");
                    UTB2.EmployeeId = Convert.ToInt32(lblempid.Text);
                    UTB2.UserId = Convert.ToInt32(lblempid.Text);
                    UTB2.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                    HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB2);
                    HR.SubmitChanges();

                    UserAssignRollHRMSTB UTB3 = new UserAssignRollHRMSTB();
                    UTB3.MenuId = GetMenuID("Job Description");
                    UTB3.EmployeeId = Convert.ToInt32(lblempid.Text);
                    UTB3.UserId = Convert.ToInt32(lblempid.Text);
                    UTB3.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                    HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB3);
                    HR.SubmitChanges();

                    UserAssignRollHRMSTB UTB4 = new UserAssignRollHRMSTB();
                    UTB4.MenuId = GetMenuID("KRA Details");
                    UTB4.EmployeeId = Convert.ToInt32(lblempid.Text);
                    UTB4.UserId = Convert.ToInt32(lblempid.Text);
                    UTB4.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                    HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB4);
                    HR.SubmitChanges();


                    UserAssignRollHRMSTB UTB5 = new UserAssignRollHRMSTB();
                    UTB5.MenuId = GetMenuID("Salary");
                    UTB5.EmployeeId = Convert.ToInt32(lblempid.Text);
                    UTB5.UserId = Convert.ToInt32(lblempid.Text);
                    UTB5.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                    HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB5);
                    HR.SubmitChanges();

                    UserAssignRollHRMSTB UTB6 = new UserAssignRollHRMSTB();
                    UTB6.MenuId = GetMenuID("Leave");
                    UTB6.EmployeeId = Convert.ToInt32(lblempid.Text);
                    UTB6.UserId = Convert.ToInt32(lblempid.Text);
                    UTB6.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                    HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB6);
                    HR.SubmitChanges();

                    UserAssignRollHRMSTB UTB7 = new UserAssignRollHRMSTB();

                    UTB7.MenuId = GetMenuID("Leave Application");

                    UTB7.EmployeeId = Convert.ToInt32(lblempid.Text);
                    UTB7.UserId = Convert.ToInt32(lblempid.Text);
                    UTB7.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                    HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB7);
                    HR.SubmitChanges();

                    UserAssignRollHRMSTB UTB8 = new UserAssignRollHRMSTB();
                    UTB8.MenuId = GetMenuID("Leave Details");
                    UTB8.EmployeeId = Convert.ToInt32(lblempid.Text);
                    UTB8.UserId = Convert.ToInt32(lblempid.Text);
                    UTB8.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                    HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB8);
                    HR.SubmitChanges();

                    UserAssignRollHRMSTB UTB9 = new UserAssignRollHRMSTB();
                    UTB9.MenuId = GetMenuID("Change Password");
                    UTB9.EmployeeId = Convert.ToInt32(lblempid.Text);
                    UTB9.UserId = Convert.ToInt32(lblempid.Text);
                    UTB9.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                    HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB9);
                    HR.SubmitChanges();


                    UserAssignRollHRMSTB UTB10 = new UserAssignRollHRMSTB();
                    UTB10.MenuId = GetMenuID("Salary Slip");
                    UTB10.EmployeeId = Convert.ToInt32(lblempid.Text);
                    UTB10.UserId = Convert.ToInt32(lblempid.Text);
                    UTB10.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                    HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB10);
                    HR.SubmitChanges();

                    UserAssignRollHRMSTB UTB11 = new UserAssignRollHRMSTB();
                    UTB11.MenuId = GetMenuID("Advance / Expense Claim");
                    UTB11.EmployeeId = Convert.ToInt32(lblempid.Text);
                    UTB11.UserId = Convert.ToInt32(lblempid.Text);
                    UTB11.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                    HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB11);
                    HR.SubmitChanges();


                    UserAssignRollHRMSTB UTB12 = new UserAssignRollHRMSTB();
                    UTB12.MenuId = GetMenuID("Expense Claim");
                    UTB12.EmployeeId = Convert.ToInt32(lblempid.Text);
                    UTB12.UserId = Convert.ToInt32(lblempid.Text);
                    UTB12.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                    HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB12);
                    HR.SubmitChanges();


                    UserAssignRollHRMSTB UTB13 = new UserAssignRollHRMSTB();
                    UTB13.MenuId = GetMenuID("Advance Form");
                    UTB13.EmployeeId = Convert.ToInt32(lblempid.Text);
                    UTB13.UserId = Convert.ToInt32(lblempid.Text);
                    UTB13.Assignby = Convert.ToInt32(Session["UserId"].ToString());
                    HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB13);
                    HR.SubmitChanges();

                }


                Thread email = new Thread(delegate()
                {
                    SmtpGmail();
                });

                email.IsBackground = true;
                email.Start();

               
                g.ShowMessage(this.Page, "Code Resend Successfully. To : " + txtEmail.Text + "'");
                //modpop.Message = " Data Updated Successfully...!!";
                //modpop.ShowPopUp();


                btnsubmit.Text = "Submit";
                Clear();
            }

        }
        catch (Exception)
        {
            throw;
        }

    }
    public void BindAllEmp()
    {

        var EmpData = from d in HR.EmployeeTBs
                      join dd in HR.RegistrationTBs
                      on d.EmployeeId equals dd.EmployeeId
                      select new
                      {
                          d.EmployeeId,

                          EmpName = d.Solitude == 0 ? "Mr" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 1 ? "Ms" + " " + d.FName + " " + d.MName + " " + d.Lname : d.Solitude == 2 ? "Mrs" + " " + d.FName + " " + d.MName + " " + d.Lname : "Dr." + " " + d.FName + " " + d.MName + " " + d.Lname,

                          d.BirthDate,
                          d.DOJ,
                          dd.UserType,
                          d.EmailId,
                          emnae = d.FName + " " + d.MName + " " + d.Lname,
                          d.personalEmail

                      };

        if (ddlsearch.SelectedIndex == 1)
        {
            EmpData = EmpData.Where(d => d.emnae == txtempsearch.Text);
        }
        if (ddlsearch.SelectedIndex == 2)
        {
            EmpData = EmpData.Where(d => d.EmailId == txtemailidsearch.Text);
        }
        if (EmpData.Count() > 0)
        {
            grd_Emp.DataSource = EmpData;
            grd_Emp.DataBind();
            lblcnt.Text = EmpData.Count().ToString();
        }


    }

    public void SmtpGmail()
    {
        try
        {
            SMTPSettingsTB smtpData = HR.SMTPSettingsTBs.FirstOrDefault();

            System.Net.Mail.MailAddress fromAddress = new System.Net.Mail.MailAddress(smtpData.emailFromAddress, "HR Department");
            System.Net.Mail.MailAddress toAddress = new System.Net.Mail.MailAddress(txtEmail.Text);
            System.Net.Mail.MailAddress toAddress1 = new System.Net.Mail.MailAddress("shrikantpatil12345@gmail.com");
             string fromPassword = smtpData.emailFromPassword;           //"support@1234";
            string subject = "Registration Message";

            string link = "<a href=" + "http://web.excellenceserver.com/hrportal/&active=yes>Click here to activate your account</a>";

            //   string body = "Your Employee Code is '" + Session["EmpCode"] + "' Have Successfully Registered to activate your account please click on link " + "<a href=" + "http://web.excellenceserver.com/hrportal/Default.aspx>Click here to activate your account</a>";

            //  string body = "<b> Your Activation Code is </b>  please Follow Following Steps to register on Portal '" + Session["EmpCode"] + "' Have Successfully Registered to activate your account please click on link " + "<a href=" + "http://web.excellenceserver.com/hrportal/>Click here to activate your account</a> " + "Please Click on the link"+ "<a href="+"http://web.excellenceserver.com/hrportal/> Put Activation Code in email </a>"+"<a href="+"put the username and Password that you want toAddress create Select security question and security answer"+"click on save to Create your account "+"Once you login please fill your full profile" ;

            string body = "<p>Dear " + txtfname.Text + " </p> <p>Your Activation Code is " + Session["EmpCode"] + "  Please Follow Following Steps to register on HR Portal <br/><br/> 1. Please Click here to activate your account " + "http://webtest.yourserver.com/HR" + "<BR/> 2. Please Click On <b>Register</b> Link <br/> 3. Put Activation Code " + "" + Session["EmpCode"] + "" + " in Activation Text Box  <br/> 4. Put the username and Password that you want as your login credentials <br/> 5. Select security question and security answer  <br/> 6. Click on save to Create your account  <br/> 7. Click on Login link &amp; Login through your credentials <br/> 8. Once you login please fill your full profile</p> <br/><br/><p> Thanks HR Team </p>";



            ///  put the username and Password that you want toAddress create Select security question and security answer 

            // click on save to create create your account 

            // Once you login please fill your full profile





            System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient();
            smtp.Host = "smtp.gmail.com";
            smtp.Port = 587;
            smtp.EnableSsl = true;

            smtp.DeliveryMethod = System.Net.Mail.SmtpDeliveryMethod.Network;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(fromAddress.Address, fromPassword);

            System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(fromAddress, toAddress);
            message.To.Add(toAddress1);
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.Subject = subject;
            message.Body = body;
            smtp.Send(message);

            Page.RegisterStartupScript("Mail Delivery", "<script>alert('Mail sent Successfully. To : " + txtEmail.Text + " .');if(alert){ window.location='EmployeeBasicDetails.aspx';}</script>");
        }
        catch
        {

        }
    }
    private void Clear()
    {
        ddlsalitude.SelectedIndex = 0;
        txtfname.Text = null;
        txtlname.Text = null;
        txtmname.Text = null;
        txtdateofjoining.Text = null;
        txtbirtdate.Text = null;
        txtEmail.Text = null;
        BindAllEmp();
        maxleadid();    
        btnsubmit.Text = "Submit";
        MultiView1.ActiveViewIndex = 0;
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        // MultiView1.ActiveViewIndex = 0;
        Clear();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;

    }
    protected void OnClick_Edit(object sender, EventArgs e)
    {
        LinkButton Lnk = (LinkButton)sender;
        //ImageButton Lnk = (ImageButton)sender;
        string Empid = Lnk.CommandArgument;
        lblempid.Text = Empid;
        lblempCode.Text = "100" + "" + lblempid.Text;
        MultiView1.ActiveViewIndex = 1;
        EmployeeTB MT = HR.EmployeeTBs.Where(d => d.EmployeeId == Convert.ToInt32(Empid)).First();

        if (ddlsalitude.SelectedIndex == 0)
        {
            ddlsalitude.SelectedValue = "Mr";
        }
        else if (ddlsalitude.SelectedIndex == 1)
        {
            ddlsalitude.SelectedValue = "Ms";
        }
        else if (ddlsalitude.SelectedIndex == 2)
        {
            ddlsalitude.SelectedValue = "Miss";
        }
        if (ddlsalitude.SelectedIndex == 3)
        {
            ddlsalitude.SelectedValue = "Dr.";
        }

        //ddlsalitude.SelectedValue = Convert.ToString(MT.Solitude);
        txtfname.Text = MT.FName;
        txtmname.Text = MT.MName;
        txtlname.Text = MT.Lname;
        txtEmail.Text = MT.EmailId;
        //MT.BirthDate = Convert.ToDateTime(txtbirtdate.Text);
        if (MT.BirthDate.ToString() == "")
        {


        }
        else
        {
            txtbirtdate.Text = MT.BirthDate.ToString();
            txtbirtdate.Text = txtbirtdate.Text.Replace("12:00:00 AM", " ");
        }

        //MT.DOJ = Convert.ToDateTime(txtdateofjoining.Text);
        if (MT.DOJ.ToString() == "")
        {


        }
        else
        {
            txtdateofjoining.Text = MT.DOJ.ToString();
            txtdateofjoining.Text = txtdateofjoining.Text.Replace("12:00:00 AM", " ");
        }
        if (MT.Gender == "0")
        {
            RbGender.SelectedIndex = 0;
        }
        else
        {
            RbGender.SelectedIndex = 1;

        }

        RegistrationTB rg = HR.RegistrationTBs.Where(d => d.EmployeeId == Convert.ToInt32(Empid)).First();
        ddusertype.SelectedValue = rg.UserType;
        //txtdateofjoining.Text = (MT.DOJ).ToString();
        //txtbirtdate.Text = MT.BirthDate.ToString();


        btnsubmit.Text = "Resend Code";
    }


    protected void grd_Dept_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Emp.PageIndex = e.NewPageIndex;
        BindAllEmp();
        grd_Emp.DataBind();
    }
    protected void ddusertype_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlsalitude_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    #region Coding by Nizhat

    protected void ddlsearch_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsearch.SelectedIndex == 0)
        {
            txtemailidsearch.Visible = false;
            txtempsearch.Visible = false;
            txtempsearch.Text = "";
            txtemailidsearch.Text = "";
        }
        if (ddlsearch.SelectedIndex == 1)
        {
            txtemailidsearch.Visible = false;
            txtempsearch.Visible = true;

            txtemailidsearch.Text = "";
        }
        if (ddlsearch.SelectedIndex == 2)
        {
            txtemailidsearch.Visible = true;
            txtempsearch.Visible = false;
            txtempsearch.Text = "";

        }
    }

    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetEmployeeName(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();

        List<string> Name = (from d in HR.EmployeeTBs
                             where
                                 (d.FName + " " + d.MName + " " + d.Lname).StartsWith(prefixText)
                             select d.FName + " " + d.MName + " " + d.Lname).Distinct().ToList();
        return Name;

    }
    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetEmailID(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();

        List<string> Emailid = (from d in HR.EmployeeTBs
                                where
                                    (d.EmailId).StartsWith(prefixText)
                                select d.EmailId).Distinct().ToList();
        return Emailid;

    }
    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindAllEmp();
    }

    protected void txtbirtdate_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtbirtdate.Text != "")
            {
                DateTime Date = DateTime.Now;
                string DOBDate = Convert.ToDateTime(txtbirtdate.Text).ToString();
                DateTime dt = DateTime.Parse(txtbirtdate.Text);
                DateTime dtpr = Date.AddYears(-14);
                if (dt.Date >= DateTime.Now)
                {
                    g.ShowMessage(Page, "DOB Date should not be greater than Current  Date.");
                    txtbirtdate.Text = "";
                }
                if (dt.Date >= dtpr)
                {
                    g.ShowMessage(Page, "DOB  should be minimum 14 years .");
                    txtbirtdate.Text = "";
                }

            }
        }
        catch (Exception)
        {
            g.ShowMessage(Page, "Invalid Date");
            txtbirtdate.Text = "";
        }
    }
    protected void txtdateofjoining_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (txtdateofjoining.Text != "")
            {
                DateTime Date = DateTime.Now;
                string DOBDate = Convert.ToDateTime(txtdateofjoining.Text).ToString();
                DateTime dt = DateTime.Parse(txtdateofjoining.Text);
                DateTime dtpr = Date.AddYears(-10);
                //if (dt.Date >= DateTime.Now)
                //{
                //    g.ShowMessage(Page, "DOB Date should not be greater than Current  Date.");
                //    //modpop.Message = "DOB Date should not be greater than Current  Date.";
                //    //modpop.DataValidaction();
                //    txtdateofjoining.Text = "";
                //}
                if (dt.Date <= dtpr)
                {
                    g.ShowMessage(Page, "DOB  should be minimum 10 years .");
                    //modpop.Message = "DOB  should be minimum 18 years .";
                    //modpop.DataValidaction();
                    txtdateofjoining.Text = "";
                }

            }
        }
        catch (Exception)
        {
            g.ShowMessage(Page, "Invalid Date");
            //modpop.Message = "Invalid Date";
            //modpop.DataValidaction();
            txtdateofjoining.Text = "";
        }
    }
}