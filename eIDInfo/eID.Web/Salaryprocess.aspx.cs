using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading;
using System.Data;

public partial class Salaryprocess : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    DataTable Dt = new DataTable();
    DataSet ds = new DataSet();
    DataTable Dtsaldet = new DataTable();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                Dt = new DataTable();
                MultiView1.ActiveViewIndex = 0;
                fillcompany();
                fillYear();
                BindAllEmp();
                ddlMonths.SelectedIndex = -1;
                ddlMonths.Items.FindByValue(System.DateTime.Now.Month.ToString()).Selected = true;
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }

    }
    public void BindAllEmp()
    {
        try
        {

            //#region Company Wise
            //if (ddlCompany.SelectedIndex > 0 && ddldepartment.SelectedIndex == 0)
            //{
            //    var d1 = from m in HR.EmployeeSalarySettingsTBs
            //             where m.Salatrydate.Value.Year <=
            //             Convert.ToInt32(ddlYears.SelectedValue)

            //             && m.Salatrydate.Value.Month <= Convert.ToInt32(ddlMonths.SelectedValue)
            //             group m by m.Empid into gg
            //             select new
            //             {
            //                 a = gg.Max(p => p.EmpSalaryid),
            //                 Category = gg.ToList()
            //             };
            //    List<String> l1 = new List<string>();
            //    Dt = new DataTable();
            //    DataColumn fname = Dt.Columns.Add("Name");
            //    DataColumn empid = Dt.Columns.Add("EmployeeId");
            //    DataColumn grosssal = Dt.Columns.Add("GrossSalary");
            //    DataColumn deduction = Dt.Columns.Add("deductions");
            //    DataColumn Earning = Dt.Columns.Add("Earning");
            //    DataColumn netpay = Dt.Columns.Add("netpay");
            //    foreach (var item in d1)
            //    {
            //        DataRow dr = Dt.NewRow();
            //        var EmpData = (from d in HR.EmployeeTBs
            //                       join n in HR.EmployeeSalarySettingsTBs on d.EmployeeId equals n.Empid
            //                       where n.EmpSalaryid == int.Parse(item.a.ToString())
            //                       //&&  n.Salatrydate == Convert.ToDateTime(g.GetCurrentDateTime().Year.ToString())
            //                       //&& n.Salatrydate == Convert.ToDateTime(g.GetCurrentDateTime().Month.ToString()) 

            //                       select new
            //                       {
            //                           Name = d.FName + " " + d.MName + " " + d.Lname,
            //                           fname = d.FName,
            //                           lname = d.Lname,
            //                           empid11 = d.EmployeeId,
            //                           GrossSalary = n.GrossSalary,
            //                           //Deduction = n.deductions,
            //                           Deduction = AddDeductions(d.EmployeeId, n.deductions, n.GrossSalary),
            //                           Earning = AddEarning(d.EmployeeId),
            //                           //netpay1 = n.netpay
            //                           netpay = Addnetpay(d.EmployeeId, n.netpay, n.deductions, n.GrossSalary),
            //                          n.Currency
            //                       });
            //        if (EmpData.Count() > 0)
            //        {
            //            dr[0] = EmpData.First().Name;
            //            dr[1] = EmpData.First().empid11;
            //            dr[2] = EmpData.First().GrossSalary;
            //            dr[3] = EmpData.First().Deduction;
            //            dr[4] = EmpData.First().Earning;
            //            dr[5] = EmpData.First().netpay;

            //            Dt.Rows.Add(dr);

            //        }
            //    }
            //    if (Dt.Rows.Count > 0)
            //    {
            //        grd_Emp.DataSource = Dt;
            //        grd_Emp.DataBind();
            //        lblcnt.Text = Dt.Rows.Count.ToString();
            //    }
            //    else
            //    {
            //        grd_Emp.DataSource = null;
            //        grd_Emp.DataBind();
            //        lblcnt.Text = "0";
            //    }
            //}
            //#endregion
            //#region Company & Department wise
            //if (ddlCompany.SelectedIndex > 0 && ddldepartment.SelectedIndex > 0 && ddldesignation.SelectedIndex == 0)
            //{
            //    var d1 = from m in HR.EmployeeSalarySettingsTBs
            //             where m.Salatrydate.Value.Year ==
            //             Convert.ToInt32(ddlYears.SelectedValue)
            //             && m.Salatrydate.Value.Month == Convert.ToInt32(ddlMonths.SelectedValue)
            //             group m by m.Empid into gg

            //             select new
            //             {
            //                 a = gg.Max(p => p.EmpSalaryid),
            //                 Category = gg.ToList()
            //             };
            //    List<String> l1 = new List<string>();
            //    Dt = new DataTable();
            //    DataColumn fname = Dt.Columns.Add("Name");
            //    DataColumn empid = Dt.Columns.Add("EmployeeId");
            //    DataColumn grosssal = Dt.Columns.Add("GrossSalary");
            //    DataColumn deduction = Dt.Columns.Add("deductions");
            //    DataColumn Earning = Dt.Columns.Add("Earning");
            //    DataColumn netpay = Dt.Columns.Add("netpay");
            //    foreach (var item in d1)
            //    {
            //        DataRow dr = Dt.NewRow();
            //        var EmpData = (from d in HR.EmployeeTBs
            //                       join n in HR.EmployeeSalarySettingsTBs on d.EmployeeId equals n.Empid
            //                       where d.CompanyId == int.Parse(ddlCompany.SelectedValue)
            //                       && d.DeptId == Convert.ToInt32(ddldepartment.SelectedValue)
            //                       && n.EmpSalaryid == int.Parse(item.a.ToString())
            //                       //    && n.Salatrydate == Convert.ToDateTime(g.GetCurrentDateTime().Year.ToString())
            //                       //&& n.Salatrydate == Convert.ToDateTime(g.GetCurrentDateTime().Month.ToString()) 

            //                       select new
            //                       {
            //                           Name = d.FName + " " + d.MName + " " + d.Lname,
            //                           fname = d.FName,
            //                           lname = d.Lname,
            //                           empid11 = d.EmployeeId,
            //                           GrossSalary = n.GrossSalary,
            //                           //Deduction = n.deductions,
            //                           Deduction = AddDeductions(d.EmployeeId, n.deductions, n.GrossSalary),
            //                           Earning = AddEarning(d.EmployeeId),
            //                           //netpay1 = n.netpay
            //                           netpay = Addnetpay(d.EmployeeId, n.netpay, n.deductions, n.GrossSalary)

            //                       });


            //        if (EmpData.Count() > 0)
            //        {
            //            dr[0] = EmpData.First().Name;
            //            dr[1] = EmpData.First().empid11;
            //            dr[2] = EmpData.First().GrossSalary;
            //            dr[3] = EmpData.First().Deduction;
            //            dr[4] = EmpData.First().Earning;
            //            dr[5] = EmpData.First().netpay;

            //            Dt.Rows.Add(dr);




            //        }
            //    }
            //    if (Dt.Rows.Count > 0)
            //    {
            //        grd_Emp.DataSource = Dt;
            //        grd_Emp.DataBind();
            //        lblcnt.Text = Dt.Rows.Count.ToString();
            //    }
            //    else
            //    {
            //        grd_Emp.DataSource = null;
            //        grd_Emp.DataBind();
            //        lblcnt.Text = "0";
            //    }

            //}
            //#endregion
            //#region Company Department & designation
            //if (ddlCompany.SelectedIndex > 0 && ddldepartment.SelectedIndex > 0 && ddldesignation.SelectedIndex > 0)
            //{
            //    var d1 = from m in HR.EmployeeSalarySettingsTBs
            //             where m.Salatrydate.Value.Year ==
            //             Convert.ToInt32(ddlYears.SelectedValue)
            //             && m.Salatrydate.Value.Month == Convert.ToInt32(ddlMonths.SelectedValue)
            //             group m by m.Empid into gg
            //             select new
            //             {
            //                 a = gg.Max(p => p.EmpSalaryid),
            //                 Category = gg.ToList()
            //             };
            //    List<String> l1 = new List<string>();
            //    Dt = new DataTable();
            //    DataColumn fname = Dt.Columns.Add("Name");
            //    DataColumn empid = Dt.Columns.Add("EmployeeId");
            //    DataColumn grosssal = Dt.Columns.Add("GrossSalary");
            //    DataColumn deduction = Dt.Columns.Add("deductions");
            //    DataColumn Earning = Dt.Columns.Add("Earning");
            //    DataColumn netpay = Dt.Columns.Add("netpay");
            //    foreach (var item in d1)
            //    {
            //        DataRow dr = Dt.NewRow();
            //        var EmpData = (from d in HR.EmployeeTBs
            //                       join n in HR.EmployeeSalarySettingsTBs on d.EmployeeId equals n.Empid
            //                       where d.CompanyId == int.Parse(ddlCompany.SelectedValue) && d.DeptId == Convert.ToInt32(ddldepartment.SelectedValue)
            //                       && d.DesgId == Convert.ToInt32(ddldesignation.SelectedValue)
            //                       && n.EmpSalaryid == int.Parse(item.a.ToString())
            //                       //  && n.Salatrydate == Convert.ToDateTime(g.GetCurrentDateTime().Year.ToString())
            //                       //&& n.Salatrydate == Convert.ToDateTime(g.GetCurrentDateTime().Month.ToString()) 

            //                       select new
            //                       {
            //                           Name = d.FName + " " + d.MName + " " + d.Lname,
            //                           fname = d.FName,
            //                           lname = d.Lname,
            //                           empid11 = d.EmployeeId,
            //                           GrossSalary = n.GrossSalary,
            //                           //Deduction = n.deductions,
            //                           Deduction = AddDeductions(d.EmployeeId, n.deductions, n.GrossSalary),
            //                           Earning = AddEarning(d.EmployeeId),
            //                           //netpay1 = n.netpay
            //                           netpay = Addnetpay(d.EmployeeId, n.netpay, n.deductions, n.GrossSalary)
            //                       });
            //        if (EmpData.Count() > 0)
            //        {
            //            dr[0] = EmpData.First().Name;
            //            dr[1] = EmpData.First().empid11;
            //            dr[2] = EmpData.First().GrossSalary;
            //            dr[3] = EmpData.First().Deduction;
            //            dr[4] = EmpData.First().Earning;
            //            dr[5] = EmpData.First().netpay;

            //            Dt.Rows.Add(dr);
            //            DataInsertData(EmpData.First().empid11, Convert.ToInt32(ddlMonths.SelectedValue), Convert.ToInt32(ddlYears.SelectedItem.Text));
            //        }
            //    }
            //    if (Dt.Rows.Count > 0)
            //    {
            //        grd_Emp.DataSource = Dt;
            //        grd_Emp.DataBind();
            //        lblcnt.Text = Dt.Rows.Count.ToString();
            //    }
            //    else
            //    {
            //        grd_Emp.DataSource = null;
            //        grd_Emp.DataBind();
            //        lblcnt.Text = "0";
            //    }

            //}
            //#endregion

            try
            {

                if (ddlCompany.SelectedIndex > 0)
                {
                    var d1 = from m in HR.EmployeeSalarySettingsTBs
                             group m by m.Empid into gg
                             select new
                             {
                                 a = gg.Max(p => p.EmpSalaryid),
                                 Category = gg.ToList()
                             };
                    List<String> l1 = new List<string>();
                    Dt = new DataTable();
                    DataColumn fname = Dt.Columns.Add("Name");
                    DataColumn empid = Dt.Columns.Add("EmployeeId");
                    DataColumn grosssal = Dt.Columns.Add("GrossSalary");
                    DataColumn deduction = Dt.Columns.Add("deductions");
                    DataColumn Earning = Dt.Columns.Add("Earning");
                    DataColumn netpay = Dt.Columns.Add("netpay");
                    DataColumn vis = Dt.Columns.Add("vis");
                    foreach (var item in d1)
                    {
                        
                        DataRow dr = Dt.NewRow();
                        var EmpData = (from d in HR.EmployeeTBs
                                       join n in HR.EmployeeSalarySettingsTBs on d.EmployeeId equals n.Empid
                                       join b in HR.BeforeSalProcessTBs on d.EmployeeId equals b.empid
                                       where d.CompanyId == int.Parse(ddlCompany.SelectedValue)
                                           // && d.RelivingDate == null 
                                       && b.month == Convert.ToString(ddlMonths.SelectedIndex) && b.year == ddlYears.SelectedItem.Text
                                           //  &&  n.Salatrydate.Value.Year.Equals(Convert.ToInt32(ddlyear.SelectedItem.Text.ToString()))
                                       && n.EmpSalaryid == int.Parse(item.a.ToString())
                                       select new
                                       {
                                           Name = d.FName + " " + d.MName + " " + d.Lname,
                                           fname = d.FName,
                                           lname = d.Lname,
                                           empid11 = d.EmployeeId,
                                           GrossSalary = n.GrossSalary,

                                           d.CompanyId,
                                           d.DeptId,
                                           d.DesgId,
                                           n.Salatrydate,
                                           Deduction = AddDeductions(d.EmployeeId, n.deductions, n.GrossSalary, Convert.ToDateTime(d.DOJ)),
                                           Earning = AddEarning(d.EmployeeId),
                                           netpay = Addnetpay(d.EmployeeId, n.netpay, n.deductions, n.GrossSalary, Convert.ToDateTime(d.DOJ), Convert.ToString(d.RelivingDate)),
                                           month = b.month,
                                           year = b.year
                                           ,
                                           vis = (from x in HR.SalaryProcessDetailsTBs where x.EmployeeId == d.EmployeeId && x.MonthId == Convert.ToInt32(ddlMonths.SelectedValue) && x.YearId == Convert.ToInt32(ddlYears.SelectedValue) select x.EmpSalaryProcessid).FirstOrDefault()
                                       }).ToList();

                        //var CurrentSlot = (from m in HR.BeforeSalProcessTBs
                        //        where m.empid == int.Parse(EmpData.First().empid11.ToString()) && m.month == ddlMonths.SelectedIndex.ToString() && m.year == ddlYears.SelectedItem.Text.ToString()
                        //        select m).ToList();



                        if (ddlCompany.SelectedIndex > 0)
                        {
                            EmpData = EmpData.Where(d => d.CompanyId == Convert.ToInt32(ddlCompany.SelectedItem.Value)).ToList();
                        }
                        if (ddldepartment.SelectedIndex > 0)
                        {
                            EmpData = EmpData.Where(d => d.DeptId == Convert.ToInt32(ddldepartment.SelectedValue.ToString())).ToList();
                        }
                        if (ddldesignation.SelectedIndex > 0)
                        {
                            EmpData = EmpData.Where(d => d.DesgId == Convert.ToInt32(ddldesignation.SelectedValue.ToString())).ToList();
                        }
                        if (ddlMonths.SelectedIndex > 0)
                        {

                            EmpData = EmpData.Where(d => Convert.ToInt32(d.month) <= Convert.ToInt32(ddlMonths.SelectedIndex)).ToList();
                        }
                        if (ddlYears.SelectedIndex > 0)
                        {
                            //   int year = EmpData.First().Salatrydate.Value.Year;
                            EmpData = EmpData.Where(d => Convert.ToInt32(d.year) <= Convert.ToInt32(ddlYears.SelectedItem.Text)).ToList();
                        }

                        if (EmpData.Count() > 0)// && CurrentSlot.Count() > 0)
                        {
                            dr[0] = EmpData.First().Name;
                            dr[1] = EmpData.First().empid11;
                            dr[2] = EmpData.First().GrossSalary;
                            dr[3] = EmpData.First().Deduction;
                            dr[4] = EmpData.First().Earning;
                            dr[5] = EmpData.First().netpay;
                            dr[6] = EmpData.First().vis;
                            Dt.Rows.Add(dr);

                        }


                    }



                    if (Dt.Rows.Count > 0)
                    {
                        grd_Emp.DataSource = Dt;
                        grd_Emp.DataBind();
                        lblcnt.Text = Dt.Rows.Count.ToString();
                    }
                    else
                    {
                        grd_Emp.DataSource = null;
                        grd_Emp.DataBind();
                        lblcnt.Text = "0";
                    }

                }
            }
            catch
            {

            }

        }
        catch
        {

        }

    }



    //Add Function By Shankar on 18-02-2015

    public void DataInsertData(int EmpId, int Month, int Year)
    {
        try
        {
            var Exist = (from d in HR.SalaryProcessDetailsTBs
                         where d.Componentid == "OT" && d.MonthId == Month
                         && d.YearId == Year && d.EmployeeId == EmpId
                         select d.Salaryid).Count();
            if (Exist == 0)
            {
                //var Earnings = (from d in HR.Sp_GetOverTimeData(EmpId, Month, Year) select d).ToList();
                SalaryProcessDetailsTB STD = new SalaryProcessDetailsTB();
                STD.ComponentType = "Earning";
                //STD.amount = Convert.ToString(Math.Round(Convert.ToDecimal(Earnings.FirstOrDefault().Earning)));
                STD.Componentid = "OT";
                STD.MonthId = Month;
                STD.YearId = Year;
               
                STD.EmployeeId = EmpId;               
                STD.SalaryDate = Convert.ToDateTime("1/" + Month + "/" + Year);
                HR.SalaryProcessDetailsTBs.InsertOnSubmit(STD);
                HR.SubmitChanges();

                var verifyExist = (from d in HR.BeforeSalProcessTBs.Where(d => d.empid == EmpId && d.month == Month.ToString() && d.year == Year.ToString()) select d).ToList();

                //if (verifyExist.Count() > 0)
                //{
                //    BeforeSalProcessTB BSP = HR.BeforeSalProcessTBs.Where(d => d.empid == EmpId && d.month == Month.ToString() && d.year == Year.ToString()).FirstOrDefault();
                //    BSP.OTHours = Convert.ToString(Earnings.FirstOrDefault().Minutes);
                //    BSP.OTRate = Convert.ToDecimal(Math.Round(Convert.ToDecimal(Earnings.FirstOrDefault().amount)));
                //    HR.SubmitChanges();
                //}
            }
            else
            {
                if (Exist > 0)
                {
                    //var Earnings = (from d in HR.Sp_GetOverTimeData(EmpId, Month, Year) select d).ToList();
                    SalaryProcessDetailsTB STD = HR.SalaryProcessDetailsTBs.Where(d => d.EmpSalaryProcessid == null && d.ComponentType == "Earning" && d.Componentid == "OT" && d.MonthId == Month && d.YearId == Year && d.EmployeeId == EmpId).First();
                    STD.ComponentType = "Earning";
                    //STD.amount = Convert.ToString(Math.Round(Convert.ToDecimal(Earnings.FirstOrDefault().Earning)));
                    STD.Componentid = "OT";
                    STD.MonthId = Month;
                    STD.YearId = Year;
                    
                    STD.EmployeeId = EmpId;
                    STD.SalaryDate = Convert.ToDateTime("1/" + Month + "/" + Year);
                    HR.SubmitChanges();

                    var verifyExist = (from d in HR.BeforeSalProcessTBs.Where(d => d.empid == EmpId && d.month == Month.ToString() && d.year == Year.ToString()) select d).ToList();

                    //if (verifyExist.Count() > 0)
                    //{
                    //    BeforeSalProcessTB BSP = HR.BeforeSalProcessTBs.Where(d => d.empid == EmpId && d.month == Month.ToString() && d.year == Year.ToString()).FirstOrDefault();
                    //    BSP.OTHours = Convert.ToString(Earnings.FirstOrDefault().Minutes);
                    //    BSP.OTRate = Convert.ToDecimal(Math.Round(Convert.ToDecimal(Earnings.FirstOrDefault().amount)));
                    //    HR.SubmitChanges();
                    //}
                }
            }

            // for Latemark calculation
            var LatemarkCount = (from d in HR.SalaryProcessDetailsTBs
                                 where d.Componentid == "Late" && d.MonthId == Month
                                 && d.YearId == Year && d.EmployeeId == EmpId
                                 select d.Salaryid).Count();
            //var lateMarkAmount = from d in HR.CalculateLateMarksSP_salaryprocess(EmpId, Month, Year) select d;
            if (LatemarkCount == 0)
            {

                SalaryProcessDetailsTB STD = new SalaryProcessDetailsTB();
                STD.ComponentType = "Deduction";
                //STD.amount = Convert.ToString(Math.Round(Convert.ToDecimal(lateMarkAmount.FirstOrDefault().Amount)));
                STD.Componentid = "Late";
                STD.MonthId = Month;
                STD.YearId = Year;
              
                STD.EmployeeId = EmpId;               
                STD.SalaryDate = Convert.ToDateTime("1/" + Month + "/" + Year);
                HR.SalaryProcessDetailsTBs.InsertOnSubmit(STD);
                HR.SubmitChanges();
            }
            else
            {
                if (LatemarkCount > 0)
                {

                    SalaryProcessDetailsTB STD = HR.SalaryProcessDetailsTBs.Where(d => d.EmpSalaryProcessid == null && d.ComponentType == "Deduction" && d.Componentid == "Late" && d.MonthId == Month && d.YearId == Year && d.EmployeeId == EmpId).First();
                    STD.ComponentType = "Deduction";
                    //STD.amount = Convert.ToString(Math.Round(Convert.ToDecimal(lateMarkAmount.FirstOrDefault().Amount)));
                    STD.Componentid = "Late";
                    STD.MonthId = Month;
                    STD.YearId = Year;                   
                    STD.EmployeeId = EmpId;
                    STD.SalaryDate = Convert.ToDateTime("1/" + Month + "/" + Year);
                    HR.SubmitChanges();
                }
            }
        }
        catch
        {

        }


    }
    private void fillcompany()
    {
        //var data = from dt in HR.CompanyInfoTBs
        //           where dt.Status == 0
        //           select dt;
        //if (data != null && data.Count() > 0)
        //{

        //    ddlCompany.DataSource = data;
        //    ddlCompany.DataTextField = "CompanyName";
        //    ddlCompany.DataValueField = "CompanyId";
        //    ddlCompany.DataBind();
        //    ddlCompany.Items.Insert(0, "--Select--");
        //}

        try
        {
            ddlCompany.Items.Clear();
            List<CompanyInfoTB> data = SPCommon.DDLCompanyBind(Session["UserType"].ToString(), Session["TenantId"].ToString(), Session["CompanyID"].ToString());
            if (data != null && data.Count() > 0)
            {

                ddlCompany.DataSource = data;
                ddlCompany.DataTextField = "CompanyName";
                ddlCompany.DataValueField = "CompanyId";
                ddlCompany.DataBind();
            }
            ddlCompany.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    private void fillYear()
    {
        DataTable Dtyears = new DataTable();
        DataColumn years = new DataColumn("years");
        Dtyears.Columns.Add(years);

        int i = int.Parse(DateTime.Now.AddYears(-10).Date.Year.ToString());
        for (int j = 0; j <= 11; j++)
        {
            DataRow DR = Dtyears.NewRow();
            DR[0] = i.ToString();
            Dtyears.Rows.Add(DR);
            i++;

        }

        ddlYears.DataSource = Dtyears;
        ddlYears.DataTextField = "years";
        ddlYears.DataValueField = "years";
        ddlYears.DataBind();
        ddlYears.Items.FindByText(g.GetCurrentDateTime().Year.ToString()).Selected = true;

    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedIndex == 0)
        {
            ddldepartment.Items.Clear();
            ddldesignation.Items.Clear();
        }
        if (ddlCompany.SelectedIndex > 0)
        {
            filldepartment();
            filldesignation();
        }


    }

    private void filldesignation()
    {
        try
        {
            DataTable dtfilldesig = g.ReturnData("Select   0 DesgId, 'ALL' DesigName  Union all Select distinct EmployeeTB.DesgId, ds.DesigName from EmployeeTB  left outer join MasterDesgTB ds on ds.DesigID=EmployeeTB.DesgId  where EmployeeTB.DeptId='" + Convert.ToInt32(ddldepartment.SelectedValue) + "'");
            ddldesignation.DataSource = dtfilldesig;
            ddldesignation.DataTextField = "DesigName";
            ddldesignation.DataValueField = "DesgId";
            ddldesignation.DataBind();

        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }

    private void filldepartment()
    {
        try
        {
            DataTable dtfinddept = g.ReturnData("Select   0 DeptId, 'ALL' DeptName  Union all Select distinct EmployeeTB.DeptId, dp.DeptName from EmployeeTB left outer join MasterDeptTB dp on dp.DeptID=EmployeeTB.DeptId where EmployeeTB.CompanyId='" + Convert.ToInt32(ddlCompany.SelectedValue) + "'");
            //if (dtfinddept.Rows.Count > 0)
            //{
            ddldepartment.DataSource = dtfinddept;
            ddldepartment.DataTextField = "DeptName";
            ddldepartment.DataValueField = "DeptId";
            ddldepartment.DataBind();

            //}
            //else
            //{

            //}

        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }
    protected void chkall_CheckedChanged(object sender, EventArgs e)
    {

        int count = grd_Emp.Rows.Count;
        int i = 0;

        if (chkall.Checked == true)
        {
            for (i = 0; i < count; i++)
            {
                CheckBox Check = (CheckBox)grd_Emp.Rows[i].FindControl("chkemp");
                Check.Checked = true;

            }
        }
        else
        {
            for (i = 0; i < count; i++)
            {
                CheckBox Check = (CheckBox)grd_Emp.Rows[i].FindControl("chkemp");
                Check.Checked = false;

            }
        }

    }
    private string GetNumberOfSundays(Int32 Month, Int32 Year)
    {

        DateTime StartDate = Convert.ToDateTime(Month.ToString() + "/01/" + Year.ToString());

        Int32 iDays = DateTime.DaysInMonth(Year, Month);

        DateTime EndDate = StartDate.AddDays(iDays - 1);

        Int32 Count = 0;

        Int32 SundayCount = 0;

        while (StartDate.DayOfWeek != DayOfWeek.Sunday)
        {

            StartDate = StartDate.AddDays(1);

        }

        SundayCount = 1;

        StartDate = StartDate.AddDays(7);

        while (StartDate <= EndDate)
        {
            SundayCount += 1; StartDate = StartDate.AddDays(7);

        }

        return SundayCount.ToString();

    }
    protected void BtnProcess_Click(object sender, EventArgs e)
    {
        try
        {
            //Check Before Salary Attendance TB 
            int count = grd_Emp.Rows.Count;
            int i = 0;
            if (count > 0)
            {
                #region


                for (i = 0; i < count; i++)
                {
                    CheckBox Check = (CheckBox)grd_Emp.Rows[i].FindControl("chkemp");
                    if (Check.Checked == true)
                    {
                        Label lblid = (Label)grd_Emp.Rows[i].FindControl("lblempid");
                        int year = int.Parse(ddlYears.SelectedItem.ToString());

                        int month = ddlMonths.SelectedIndex;
                        int days = System.DateTime.DaysInMonth(year, month);
                        //int month = ddlMonths.SelectedIndex + 1;

                        var CurrentSlot = (from m in HR.BeforeSalProcessTBs
                                           where m.empid == Convert.ToInt32(lblid.Text) && m.month == month.ToString() && m.year == year.ToString()
                                           select m).ToList();

                        if (CurrentSlot.Count() > 0)
                        {
                            var saldata = (from m in HR.SalaryProcessTBs
                                           where m.EmployeeID == Convert.ToInt32(lblid.Text) && m.Month == month.ToString() && m.Year == year.ToString()
                                           select m).ToList();

                            var EmpData = (from d in HR.EmployeeTBs
                                           join n in HR.EmployeeSalarySettingsTBs on d.EmployeeId equals n.Empid

                                           where n.Empid == int.Parse(lblid.Text)
                                           select new
                                           {
                                               Name = d.FName + " " + d.MName + " " + d.Lname,
                                               d.FName,
                                               d.Lname,
                                               d.EmployeeId,
                                               n.EmpSalaryid,
                                               d.PFAccountNo,
                                               d.ESICAccountNo,
                                               d.Salarytype,
                                               d.BankName,
                                               d.SalaryAccountNo,
                                               // GrossSalary = n.GrossSalary,
                                               //Deduction = n.deductions,
                                               //netpay1 = n.netpay,
                                               d.DOJ,
                                               d.RelivingDate,
                                               _ReleviengDate = d.RelivingDate.ToString(),
                                               //n.Currency,
                                               //n.IsTaxable,
                                               GrossSalary = n.GrossSalary,
                                               Deduction = Math.Round(AddDeductions(d.EmployeeId, n.deductions, n.GrossSalary, Convert.ToDateTime(d.DOJ))),
                                               Earning = Math.Round(AddEarning(d.EmployeeId)),
                                               netpay = Math.Round(Addnetpay(d.EmployeeId, n.netpay, n.deductions, n.GrossSalary, Convert.ToDateTime(d.DOJ), Convert.ToString(d.RelivingDate)))

                                           }).OrderByDescending(x => x.EmpSalaryid);


                            if (saldata.Count() == 0)
                            {


                                SalaryProcessTB salprocess = new SalaryProcessTB();
                                salprocess.EmployeeID = EmpData.First().EmployeeId;
                                salprocess.SalSettingID = (from m in HR.EmployeeSalarySettingsTBs
                                                           where m.Empid == int.Parse(lblid.Text)
                                                           select m.EmpSalaryid).Max();
                                salprocess.Month = month.ToString();
                                salprocess.Year = year.ToString();
                                salprocess.WorkingDays = days;

                                // salprocess.WorkingDays = 31;
                                salprocess.PFAccountNo = EmpData.First().PFAccountNo;
                                salprocess.ESICAccountNo = EmpData.First().ESICAccountNo;
                                salprocess.SalaryMode = EmpData.First().Salarytype;
                                salprocess.BankName = EmpData.First().BankName;
                                salprocess.SalaryAccountNo = EmpData.First().SalaryAccountNo;
                                //salprocess.Currency = EmpData.First().Currency;

                                BeforeSalProcessTB BSP = HR.BeforeSalProcessTBs.Where(d => d.empid == EmpData.First().EmployeeId && d.month == month.ToString() && d.year == year.ToString()).FirstOrDefault();

                                // n changes 19 May
                                salprocess.Netpaybledays = Convert.ToString(Convert.ToDecimal(BSP.presentdays) + Convert.ToDecimal(BSP.SundayHolidays) + Convert.ToDecimal(BSP.ApprovedLeaves));
                                // n changes 05 jan                               
                                string date1 = ddlMonths.SelectedValue + '/' + 1 + '/' + year;
                                decimal Leavdeductionamt = 0;
                                if (EmpData.First()._ReleviengDate != null)
                                {
                                    if ((Convert.ToDateTime(EmpData.First().DOJ) >= Convert.ToDateTime(date1)) || (Convert.ToDateTime(EmpData.First().RelivingDate) >= Convert.ToDateTime(Convert.ToString(date1))))
                                    {
                                        Leavdeductionamt = 0;
                                    }
                                }
                                else
                                {
                                    if ((Convert.ToDateTime(EmpData.First().DOJ) >= Convert.ToDateTime(date1)))
                                    {
                                        Leavdeductionamt = 0;
                                    }
                                    else
                                    {

                                        Leavdeductionamt = (decimal.Parse(EmpData.First().GrossSalary) / days) * Convert.ToDecimal(BSP.absentdays);
                                    }
                                }
                                //Gross Salary
                                decimal GrossAmount = Convert.ToDecimal(EmpData.First().GrossSalary);///holiday add *suhas sir
                                //Net Pay Amount

                                decimal netAmount = GrossAmount - EmpData.First().Deduction;///holiday add *suhas sir
                                Decimal Empdata1 = Convert.ToDecimal(netAmount);
                                int emp1 = Convert.ToInt32(Empdata1);

                                salprocess.GrossSalary = Convert.ToDecimal(EmpData.First().GrossSalary).ToString();

                                salprocess.NetSlary = EmpData.First().netpay.ToString();
                                //if (EmpData.First().IsTaxable == true)
                                //{

                                //    if (Convert.ToDecimal(salprocess.GrossSalary) >= 70000)
                                //    {
                                //        int year1 = Convert.ToInt32(ddlYears.SelectedValue);
                                //        string date = ddlMonths.SelectedValue + '/' + 1 + '/' + year1;
                                //        //  int month1 = Convert.ToInt32(ddlMonths.SelectedValue);

                                //        //For DOJ BY Arvind Chopade
                                //        if ((EmpData.First().DOJ >= Convert.ToDateTime(date)) || (EmpData.First().RelivingDate >= Convert.ToDateTime(date)))
                                //        {
                                //            int p = EmpData.First().EmployeeId;
                                //            DataSet CalBasic = g.ReturnData1("select amount from SalaryDetail left outer join SalaryComponentTB SC on SC.Componentid=SalaryDetail.Componentid where employeeid='" + p + "' and SC.Componentid=1");
                                //            DataTable DTBasic = CalBasic.Tables[0];
                                //            DataSet Cal = g.ReturnData1("select monthDays,WorkingDays,absentdays from BeforeSalProcessTB where empid='" + p + "' and year='" + ddlYears.SelectedValue + "' and month='" + ddlMonths.SelectedValue + "'");
                                //            DataTable dt = Cal.Tables[0];
                                //            DataSet CalAllowancesamt = g.ReturnData1("select amount from SalaryDetail left outer join SalaryComponentTB SC on SC.Componentid=SalaryDetail.Componentid where employeeid='" + p + "' and SC.Componentid=2");
                                //            DataTable dtAllowances = CalAllowancesamt.Tables[0];
                                //            if (dt.Rows.Count > 0)
                                //            {
                                //                decimal ActualPay = (Convert.ToDecimal(DTBasic.Rows[0]["amount"].ToString()) / Convert.ToDecimal(dt.Rows[0]["monthDays"].ToString())) * (Convert.ToDecimal(dt.Rows[0]["WorkingDays"].ToString()) - Convert.ToDecimal(dt.Rows[0]["absentdays"].ToString()));
                                //                decimal NetSalary = ActualPay + Convert.ToDecimal(dtAllowances.Rows[0]["amount"].ToString());
                                //                decimal Tax = (NetSalary * Convert.ToDecimal(1.5)) / 100;
                                //                decimal NetAllowances = Convert.ToDecimal(dtAllowances.Rows[0]["amount"].ToString()) - Tax;

                                //                decimal salary = ActualPay + NetAllowances;
                                //                // total = Math.Round(salary + Convert.ToDecimal(exp)) - Convert.ToDecimal(deduction);
                                //                //salprocess.TaxAmount = Tax;
                                //                salprocess.NetSlary = Convert.ToString(Convert.ToDecimal(salary));// - Convert.ToDecimal(Tax));
                                //                //salprocess.NetsalaryAmount = Convert.ToDecimal(NetSalary);
                                //            }
                                //        }
                                //        else
                                //        {
                                //            //salprocess.NetsalaryAmount = Convert.ToDecimal(EmpData.First().netpay);
                                //            //salprocess.TaxAmount = ((Convert.ToDecimal(EmpData.First().netpay) * Convert.ToDecimal(1.5)) / 100);
                                //            //salprocess.NetSlary = (Convert.ToDecimal(salprocess.NetsalaryAmount) - Convert.ToDecimal(salprocess.TaxAmount)).ToString();
                                //        }
                                //    }
                                //    else
                                //    {
                                //        //salprocess.TaxAmount = (0);
                                //        //salprocess.NetSlary = (Convert.ToDecimal(salprocess.NetsalaryAmount)).ToString();
                                //    }
                                //}
                                //else
                                //{
                                //    //salprocess.TaxAmount = (0);
                                //    //salprocess.NetSlary = (Convert.ToDecimal(salprocess.NetsalaryAmount)).ToString();
                                //}

                                HR.SalaryProcessTBs.InsertOnSubmit(salprocess);
                                HR.SubmitChanges();


                                var sdData = (from d in HR.SalaryDetails
                                              join
                                                  ct in HR.SalaryComponentTBs on d.Componentid equals ct.Componentid
                                              where ct.ComponentName.Contains("Other")
                                                  && d.EmpSalarysettingid == salprocess.SalSettingID && d.EmployeeId == salprocess.EmployeeID
                                              select new
                                              {
                                                  d.Salaryid,
                                                  ct.FixedValue
                                              }).FirstOrDefault();


                                Decimal LDeduction = Convert.ToDecimal(Leavdeductionamt);


                                SalaryDetail SD = HR.SalaryDetails.Where(d => d.Salaryid == sdData.Salaryid).First();
                                SD.amount = Convert.ToString(LDeduction);
                                HR.SubmitChanges();

                                // DataSet dscheckexist = g.ReturnData1("select * from SalaryProcessDetailsTB where convert(varchar,salarydate,101)= '01/" + Convert.ToInt32(ddlMonths.SelectedIndex) + "/" + Convert.ToInt32(ddlYears.SelectedItem.Text) + "' and EmployeeId='" + EmpData.First().EmployeeId + "'");
                                //if (dscheckexist.Tables[0].Rows.Count <= 0)
                                //{
                                DataSet dssalprocessdetails = g.ReturnData1("insert into SalaryProcessDetailsTB( EmpSalaryProcessid, EmployeeId, SalaryDate, Componentid, ComponentType, amount,MonthId,YearId) SELECT      '" + salprocess.SalProcessId + "', EmployeeId, '01/" + Convert.ToInt32(ddlMonths.SelectedIndex) + "/" + Convert.ToInt32(ddlYears.SelectedItem.Text) + "', ComponentName,SalaryDetail.ComponentType, amount," + Convert.ToInt32(ddlMonths.SelectedIndex) + "," + Convert.ToInt32(ddlYears.SelectedItem.Text) + " FROM         SalaryDetail left outer join SalaryComponentTB SC on SC.Componentid=SalaryDetail.Componentid  where EmployeeId='" + EmpData.First().EmployeeId + "'");
                                // }

                                DataSet dsupdate = g.ReturnData1("update  EmployeeAdvanceSlotTB set Status='Paid' where monthid='" + Convert.ToInt32(ddlMonths.SelectedIndex) + "' and year='" + Convert.ToInt32(ddlYears.SelectedItem.Text) + "' and employeeid='" + EmpData.First().EmployeeId + "'"); //  for loan - here we update status='Paid'

                                string updatequery = "update SalaryProcessDetailsTB set EmpSalaryProcessid ='" + salprocess.SalProcessId + "' where EmployeeId='" + EmpData.First().EmployeeId + "' and convert(varchar,SalaryDate,103)= convert(date,'" + Convert.ToDateTime((Convert.ToInt32(ddlMonths.SelectedIndex)) + "/01/" + Convert.ToInt32(ddlYears.SelectedItem.Text)) + "',101)";

                                DataSet dsupdateprevdata = g.ReturnData1(updatequery);

                                modpop.Message = "Salary process updated successfully";
                                modpop.ShowPopUp();



                            }
                            else
                            {
                                modpop.Message = "Salary already Process on Month" + " " + ddlMonths.SelectedItem.Text;
                                modpop.ShowPopUp();

                            }

                        }
                        else
                        {
                            modpop.Message = " First Process Attendance...!!";
                            modpop.ShowPopUp();

                        }
                    }
                }
                #endregion
            }
            else
            {
                g.ShowMessage(this.Page, "No Record Exists..");
            }
        }
        catch
        {

        }
    }

    protected void grd_Emp_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Emp.PageIndex = e.NewPageIndex;
        BindAllEmp();
    }
    protected void btnsearch_Click(object sender, EventArgs e)
    {
        if (ddlCompany.SelectedItem.Text != "--Select--")
        {
            BindAllEmp();
        }
        else
        {
            g.ShowMessage(this.Page, "Please Select Company.");
            grd_Emp.DataSource = null;
            grd_Emp.DataBind();
            ddlCompany.Focus();
        }



    }

    private decimal Addnetpay(int p, string netpay, string ded, string grosssal, DateTime DOJ, string RelevingDate)
    {
        decimal exp = 0, deduction = 0, total = 0;
        try { 
        int year = Convert.ToInt32(ddlYears.SelectedValue);
        string date = ddlMonths.SelectedValue + '/' + 1 + '/' + year;
        int month = Convert.ToInt32(ddlMonths.SelectedValue);


        //DataSet dsallearning = g.ReturnData1("select isnull(sum(CONVERT(decimal,amount,12)),0) Amount from SalaryProcessDetailsTB where EmployeeId='" + p + "' and Monthid='" + (Convert.ToInt16(ddlMonths.SelectedIndex)) + "' and Yearid='" + ddlYears.SelectedItem.Text + "'and ComponentType='Earning' and Componentid not in (select ComponentName from SalaryComponentTB)");
        DataSet dsallearning = g.ReturnData1(@"select isnull(sum(CONVERT(decimal,amount,12)),0) Amount from SalaryProcessDetailsTB sp
left join SalaryComponentTB sc on sp.Componentid=ComponentName where EmployeeId='" + p + "' and Monthid='" + (Convert.ToInt16(ddlMonths.SelectedIndex)) + "' and Yearid='" + ddlYears.SelectedItem.Text + "'and sp.ComponentType='Earning' --and Componentid not in (select ComponentName from SalaryComponentTB)");
        if (dsallearning.Tables[0].Rows.Count > 0)
        {
            exp = exp + Convert.ToDecimal(dsallearning.Tables[0].Rows[0][0].ToString());

        }

        //DataSet dsalldeduction = g.ReturnData1("select isnull(sum(CONVERT(decimal,amount,12)),0) Amount from SalaryProcessDetailsTB where EmployeeId='" + p + "' and Monthid='" + (Convert.ToInt16(ddlMonths.SelectedIndex)) + "' and Yearid='" + ddlYears.SelectedItem.Text + "' and ComponentType='Deduction' and Componentid not in (select ComponentName from SalaryComponentTB where ComponentName!='Other')");
        DataSet dsalldeduction = g.ReturnData1(@"select isnull(sum(CONVERT(decimal,amount,12)),0) Amount from SalaryProcessDetailsTB sp
left join SalaryComponentTB sc on sp.Componentid=ComponentName and ComponentName!='Other' where EmployeeId='" + p + "' and Monthid='" + (Convert.ToInt16(ddlMonths.SelectedIndex)) + "' and Yearid='" + ddlYears.SelectedItem.Text + "' and sp.ComponentType='Deduction' --and Componentid not in (select ComponentName from SalaryComponentTB where ComponentName!='Other')");
        if (dsalldeduction.Tables[0].Rows.Count > 0)
        {
            deduction = deduction + Convert.ToDecimal(dsalldeduction.Tables[0].Rows[0][0].ToString());
        }


        if ((DOJ >= Convert.ToDateTime(date)) || (Convert.ToDateTime(RelevingDate) >= Convert.ToDateTime(Convert.ToString(date))))
        {
            deduction = 0;

            DataSet CalBasic = g.ReturnData1("select amount from SalaryDetail left outer join SalaryComponentTB SC on SC.Componentid=SalaryDetail.Componentid where employeeid='" + p + "' and SC.Componentid=1");
            DataTable DTBasic = CalBasic.Tables[0];
            DataSet Cal = g.ReturnData1("select monthDays,WorkingDays,absentdays from BeforeSalProcessTB where empid='" + p + "' and year='" + ddlYears.SelectedValue + "' and month='" + ddlMonths.SelectedValue + "'");
            DataTable dt = Cal.Tables[0];
            decimal ActualWorkingdays = Convert.ToDecimal(dt.Rows[0]["WorkingDays"].ToString()) - Convert.ToDecimal(dt.Rows[0]["absentdays"].ToString());
            DataSet CalAllowancesamt = g.ReturnData1("select amount from SalaryDetail left outer join SalaryComponentTB SC on SC.Componentid=SalaryDetail.Componentid where employeeid='" + p + "' and SC.Componentid=2");
            DataTable dtAllowances = CalAllowancesamt.Tables[0];
            if (dt.Rows.Count > 0)
            {
                decimal ActualPay = (Convert.ToDecimal(DTBasic.Rows[0]["amount"].ToString()) / Convert.ToDecimal(dt.Rows[0]["monthDays"].ToString())) * ActualWorkingdays;
                decimal NetSalary = ActualPay + Convert.ToDecimal(dtAllowances.Rows[0]["amount"].ToString());
                decimal Tax = Math.Round((NetSalary * Convert.ToDecimal(1.5)) / 100);
                decimal NetAllowances = Convert.ToDecimal(dtAllowances.Rows[0]["amount"].ToString()) - Tax;

                decimal salary = ActualPay + NetAllowances;
                total = Math.Round(salary + Convert.ToDecimal(exp)) - Convert.ToDecimal(deduction);

            }


        }
        else
        {
            
            var vis = (from x in HR.SalaryProcessDetailsTBs where x.EmployeeId == p && x.MonthId == Convert.ToInt32(ddlMonths.SelectedValue) && x.YearId == Convert.ToInt32(ddlYears.SelectedValue) select x.EmpSalaryProcessid).FirstOrDefault();
            if (vis == null)
            {
                decimal Leavdeductionamt = LeaveDeductionMethod(p, grosssal);

                //Net Pay Amount
                deduction = deduction + Convert.ToDecimal(Leavdeductionamt) + Convert.ToDecimal(ded);
                
            }
            total = (Convert.ToDecimal(grosssal)) - Convert.ToDecimal(deduction);
        }
        }
        catch (Exception ex) { }

        return total;
    }

    private decimal AddEarning(int p)
    {
        try
        {
            decimal de = 0;

            DataInsertData(p, Convert.ToInt32(ddlMonths.SelectedValue), Convert.ToInt32(ddlYears.SelectedItem.Text));

            //DataSet dsallearning = g.ReturnData1("select isnull(sum(CONVERT(decimal,amount,12)),0) Amount from SalaryProcessDetailsTB where EmployeeId='" + p + "' and Monthid='" + (Convert.ToInt16(ddlMonths.SelectedIndex)) + "' and Yearid='" + ddlYears.SelectedItem.Text + "'and ComponentType='Earning' and Componentid not in (select ComponentName from SalaryComponentTB)");
//            DataSet dsallearning = g.ReturnData1(@"select isnull(sum(CONVERT(decimal,amount,12)),0) Amount from SalaryProcessDetailsTB sp
//left join SalaryComponentTB sc on sp.Componentid=ComponentName where EmployeeId='" + p + "' and Monthid='" + (Convert.ToInt16(ddlMonths.SelectedIndex)) + "' and Yearid='" + ddlYears.SelectedItem.Text + "'and sp.ComponentType='Earning' --and Componentid not in (select ComponentName from SalaryComponentTB)");
            DataSet dsallearning = g.ReturnData1(@"select top 1 grosssalary from EmployeeSalarySettingsTB where empid="+p+" order by empsalaryid desc");
            if (dsallearning.Tables[0].Rows.Count > 0)
            {
                de = de + Convert.ToDecimal(dsallearning.Tables[0].Rows[0][0].ToString());
            }


            return de;
        }
        catch
        {
            return 0;
        }
    }

    private decimal AddDeductions(int p, string p_2, string grosssal, DateTime DOJ)
    {
        if(p==48)
        { }
        decimal de = 0;
        try
        {
            DateTime datevalue = (Convert.ToDateTime(DOJ.ToString()));

            String dy = datevalue.Day.ToString();
            String mn = datevalue.Month.ToString();
            String yy = datevalue.Year.ToString();

            int m = datevalue.Month;
            int y = datevalue.Year;
            DateTime dtDOJ = new DateTime(y, m, 1);
            DateTime dtCurJo = new DateTime(Convert.ToInt32(ddlYears.SelectedValue), Convert.ToInt32(ddlMonths.SelectedValue), 1);
            if (dtDOJ != dtCurJo)
            //if (ddlMonths.SelectedValue != mn && ddlYears.SelectedValue != yy)
            {
                // string date = (ddlMonths.SelectedValue + "/" + 1 + "/" + ddlYears.SelectedValue);

                DataSet dsadvance = g.ReturnData1("select convert(decimal(18,2), isnull(sum(MonthPay),0)) Amount from EmployeeAdvanceSlotTB where EmployeeId='" + p + "' and Monthid='" + (Convert.ToInt16(ddlMonths.SelectedIndex)) + "' and Year='" + ddlYears.SelectedItem.Text + "'");
                if (dsadvance.Tables[0].Rows.Count > 0)
                {
                    de = Convert.ToDecimal(dsadvance.Tables[0].Rows[0][0].ToString());

                    var Exist = (from d in HR.SalaryProcessDetailsTBs
                                 where d.Componentid == "Loan" && d.MonthId == ddlMonths.SelectedIndex
                                 && d.YearId == Convert.ToInt32(ddlYears.SelectedItem.Text) && d.EmployeeId == p
                                 select d.Salaryid).Count();

                    if (Exist == 0 && de != 0)
                    {
                        DataSet dsinsert = g.ReturnData1("insert into SalaryProcessDetailsTB( EmployeeId, SalaryDate, Componentid, ComponentType, amount,MonthId,YearId)   values( '" + p + "','01/" + Convert.ToInt32(ddlMonths.SelectedIndex) + "/" + Convert.ToInt32(ddlYears.SelectedItem.Text) + "','Loan','Deduction','" + de + "','" + ddlMonths.SelectedIndex + "','" + ddlYears.SelectedItem.Text + "')");
                    }
                    de = 0;

                }
                DataInsertData(p, Convert.ToInt32(ddlMonths.SelectedValue), Convert.ToInt32(ddlYears.SelectedItem.Text));
                DataSet dsalldeduction = g.ReturnData1("select isnull(sum(CONVERT(decimal,amount,12)),0) Amount from SalaryProcessDetailsTB where EmployeeId='" + p + "' and Monthid='" + (Convert.ToInt16(ddlMonths.SelectedIndex)) + "' and Yearid='" + ddlYears.SelectedItem.Text + "' and ComponentType='Deduction' and Componentid not in (select ComponentName from SalaryComponentTB where ComponentName!='Other')");
                if (dsalldeduction.Tables[0].Rows.Count > 0)
                {
                    de = de + Convert.ToDecimal(dsalldeduction.Tables[0].Rows[0][0].ToString());

                }

                de = de + Convert.ToDecimal(p_2);


                var vis = (from x in HR.SalaryProcessDetailsTBs where x.EmployeeId == p && x.MonthId == Convert.ToInt32(ddlMonths.SelectedValue) && x.YearId == Convert.ToInt32(ddlYears.SelectedValue) select x.EmpSalaryProcessid).FirstOrDefault();
                if (vis == null)
                {
                    decimal Leavdeductionamt = LeaveDeductionMethod(p, grosssal);
                    //Net Pay Amount

                    de = de + Convert.ToDecimal(Leavdeductionamt);
                }
            }
        }
        catch (Exception ex) { }
        return Math.Round(de);
    }


    private decimal LeaveDeductionMethod(int p, string grosssal)
    {
        try
        {
            int month = ddlMonths.SelectedIndex;
            int year = int.Parse(ddlYears.SelectedItem.ToString());
            int days = System.DateTime.DaysInMonth(year, month); //Change by narendra 25-03-2015

            //  int days = 31;// change by shrikant on 3rd jan 2015//Change by Shankar 23-03-2015
            var CurrentSlot = (from m in HR.BeforeSalProcessTBs
                               where m.empid == Convert.ToInt32(p) && m.month == month.ToString() && m.year == year.ToString()
                               select m).ToList();
            if (CurrentSlot.Count() > 0)
            {

                decimal Leavdeductionamt = (decimal.Parse(grosssal) / days) * Convert.ToDecimal(CurrentSlot.First().absentdays);
                //Gross Salary
                decimal GrossAmount = decimal.Parse(grosssal) - Leavdeductionamt;
                return Math.Round(Leavdeductionamt);
            }
            else
            {
                return 0;
            }

        }
        catch
        {
            return 0;
        }

    }
    protected void ddlMonths_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlYears_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Edit_Click(object sender, EventArgs e)
    {
        mod1.Show();
        LinkButton lnk = (sender) as LinkButton;
        string id = lnk.CommandArgument.ToString();
        DataSet dsalldeduction = g.ReturnData1("select Componentid,ComponentType, convert(decimal(18,2), Amount) Amount from SalaryProcessDetailsTB where EmployeeId='" + id + "' and Monthid='" + (Convert.ToInt16(ddlMonths.SelectedIndex)) + "' and Yearid='" + ddlYears.SelectedItem.Text + "' and ComponentType='Deduction'");
        grddeduction.DataSource = dsalldeduction.Tables[0];
        grddeduction.DataBind();
        DataSet dsallearning = g.ReturnData1("select Componentid,ComponentType,convert(decimal(18,2), Amount) Amount from SalaryProcessDetailsTB where EmployeeId='" + id + "' and Monthid='" + (Convert.ToInt16(ddlMonths.SelectedIndex)) + "' and Yearid='" + ddlYears.SelectedItem.Text + "' and ComponentType='Earning'");
        grdearning.DataSource = dsallearning.Tables[0];
        grdearning.DataBind();
    }
    protected void ddldepartment_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddldepartment.SelectedIndex == 0)
        {
            filldesignation();
        }
        if (ddlCompany.SelectedIndex > 0)
        {
            filldesignation();
        }
    }
}