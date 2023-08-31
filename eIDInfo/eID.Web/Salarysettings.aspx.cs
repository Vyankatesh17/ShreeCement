using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Salarysettings : System.Web.UI.Page
{
    HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    DataTable Dt = new DataTable();
    int basicamnt, hraamount;
    static decimal EarningSum = 0, DeductionSum = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            txtDOJ.Attributes.Add("readonly", "readonly");
           // txtsalarydate.Attributes.Add("readonly", "readonly");
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }


    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetEmployeeList(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
        List<string> Empname = (from d in hr.EmployeeTBs
                                        .Where(r => (r.FName + " " + r.MName + " " + r.Lname).Contains(prefixText) && r.RelivingStatus == null && r.DeptId != null)
                                select d.FName + " " + d.Lname).Distinct().ToList();
        return Empname;
    }


    [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
    public static List<string> GetEmployeeIDList(string prefixText, int count, string contextKey)
    {
        HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
        List<string> Empname = (from d in hr.EmployeeTBs
                                        .Where(r => ("100" + r.EmployeeId).Contains(prefixText) && r.RelivingStatus == null && r.DeptId != null)
                                select "100" + d.EmployeeId).Distinct().ToList();
        return Empname;
    }


    protected void txtempid_TextChanged(object sender, EventArgs e)
    {
        try
        {
            string EmpId = "";
            string str = txtempid.Text.Substring(0, 4);
            if (str == "ESPL")
            {
                #region

                var GetEmployeeId = from e1 in db.EmployeeTBs
                                    where e1.EmployeeId == Convert.ToInt32(txtempid.Text.Replace("ESPL0", ""))
                                    select new
                                    {
                                        e1.EmployeeId,
                                    };
                if (GetEmployeeId.Count() > 0)
                {
                    foreach (var item in GetEmployeeId)
                    {
                        EmpId = Convert.ToString(item.EmployeeId);
                        Session["EmpIDSalID"] = EmpId;
                    }                 

                }
                else
                {
                    txtempid.Text = string.Empty;
                }
                #endregion
            }
            else if (str == "EISP")
            {
                #region
                var GetEmployeeId = from e1 in db.EmployeeTBs
                                    where e1.EmployeeId == Convert.ToInt32(txtempid.Text.Replace("100", ""))
                                    select new
                                    {
                                        e1.EmployeeId,
                                    };
                if (GetEmployeeId.Count() > 0)
                {
                    foreach (var item in GetEmployeeId)
                    {
                        EmpId = Convert.ToString(item.EmployeeId);
                        Session["EmpIDSalID"] = EmpId;
                    }                

                }
                else
                {
                    txtempid.Text = string.Empty;
                }
                #endregion
            }

            string a = txtempid.Text;



            if (txtempid.Text != null)
            {

                var EmpData = (from d in db.EmployeeTBs
                               join n in db.MasterDeptTBs on d.DeptId equals n.DeptID
                               where d.EmployeeId == int.Parse(EmpId) 
                               select new
                               {
                                   Name = d.FName + " " + d.MName + " " + d.Lname,
                                   d.FName,
                                   d.Lname,
                                   d.MachineID,
                                   DOJ1 = d.DOJ,
                                   d.CurState,
                                   d.BankName,
                                   d.Salarytype,
                                   d.SalaryAccountNo,
                                   d.ESICAccountNo,
                                   d.PFAccountNo,
                                   d.PanNo,
                                   n.DeptName,
                                   d.NetSalary
                               }).First();
                txtmachinid.Text = EmpData.MachineID;
                txtname.Text = EmpData.Name;

              //  txtDOJ.Text = EmpData.DOJ1.Value.ToString("MM/dd/yyyy");
                txtDOJ.Text = EmpData.DOJ1.Value.ToString();
                txtdept.Text = EmpData.DeptName;
                txtPan1.Text = EmpData.PanNo;

                txtpfaccount.Text = EmpData.PFAccountNo;
                txtaccount.Text = EmpData.SalaryAccountNo;
                ddlsalarytype.Text = EmpData.Salarytype;
                txtbankname.Text = EmpData.BankName;
                txtesicaccnumber.Text = EmpData.ESICAccountNo;
                if (EmpData.NetSalary != null)
                    txtsalary.Text = EmpData.NetSalary;
                else
                    txtsalary.Text =string.Empty;
             
                txtcompanyname.Text = (from m in db.EmployeeTBs
                                       join n in db.CompanyInfoTBs on m.CompanyId equals n.CompanyId
                                       where m.EmployeeId == int.Parse(EmpId)
                                       select n.CompanyName).First();
                txtdesg.Text = (from m in db.EmployeeTBs
                                join n in db.MasterDesgTBs on m.DesgId equals n.DesigID
                                where m.EmployeeId == int.Parse(EmpId)
                                select n.DesigName).First();

                //new code
                DataColumn Component = Dt.Columns.Add("Component");
                DataColumn percentageValue = Dt.Columns.Add("percentageValue");
                DataColumn ComponentType = Dt.Columns.Add("ComponentType");
                DataColumn amount = Dt.Columns.Add("amount");

                DataSet dscheck = g.ReturnData1("Select top 1 convert(varchar, Salatrydate,101) date  from EmployeeSalarySettingsTB where empid=" + EmpId + " order by empsalaryid");
                if (dscheck.Tables[0].Rows.Count > 0)
                {
                    //txtsalary.Text = dscheck.Tables[0].Rows[0]["GrossSalary"].ToString();
                    g.ShowMessage(this.Page, "Salary Setting is already done on " + dscheck.Tables[0].Rows[0][0].ToString() + " this date");

                    DataSet dsretriveamt = g.ReturnData1(@"select SC.Componentid,SC.ComponentName,PercentageValue,FixedValue,SC.ComponentType,amount from SalaryComponentTB SC cross join
EmployeeSalarySettingsTB ES left outer join SalaryDetail SD on SD.EmpSalarysettingid=ES.EmpSalaryid
and SD.Componentid=SC.componentid where  employeeid ='" + EmpId + "' and  convert(date,ES.salatrydate,101) =convert(date,'" + dscheck.Tables[0].Rows[0][0].ToString() + "',101)");

                    for (int i = 0; i < dsretriveamt.Tables[0].Rows.Count; i++)
                    {

                        DataRow dr = Dt.NewRow();
                        dr[0] = dsretriveamt.Tables[0].Rows[i]["ComponentName"].ToString();

                        // Change 
                        decimal pfamount = 0;
                        if (dsretriveamt.Tables[0].Rows[i]["ComponentName"].ToString() == "P.F.")
                        {
                            DataTable dspfsetting = g.ReturnData("select pfid,Startlimit,endlimit,valuetype,percentagevalue,fixedvalue from PFDynamicSettingTB where '" + txtsalary.Text + "' between startlimit and endlimit");
                            if (dspfsetting.Rows.Count > 0)
                            {
                                string valutype = dspfsetting.Rows[0]["valuetype"].ToString();
                                if (valutype == "Fixed")
                                {
                                    pfamount = Convert.ToDecimal(dspfsetting.Rows[0]["fixedvalue"].ToString());
                                    dr[1] = dspfsetting.Rows[0]["fixedvalue"].ToString();
                                }
                                else
                                {
                                    pfamount = (Convert.ToDecimal(txtsalary.Text) * Convert.ToDecimal(dspfsetting.Rows[0]["percentagevalue"].ToString())) / 100;
                                    dr[1] = dspfsetting.Rows[0]["percentagevalue"].ToString();
                                }
                            }
                            else
                            {
                                pfamount = 0;
                            }

                        }
                        else
                        {
                            if (string.IsNullOrEmpty(dsretriveamt.Tables[0].Rows[i]["PercentageValue"].ToString()) || Convert.ToDecimal(dsretriveamt.Tables[0].Rows[i]["PercentageValue"].ToString()) == 0)
                            {
                                dr[1] = dsretriveamt.Tables[0].Rows[i]["FixedValue"].ToString();
                            }
                            else
                            {
                                dr[1] = dsretriveamt.Tables[0].Rows[i]["PercentageValue"].ToString();
                            }
                            pfamount = Convert.ToDecimal(dsretriveamt.Tables[0].Rows[i]["amount"].ToString());
                        }


                        dr[2] = dsretriveamt.Tables[0].Rows[i]["ComponentType"].ToString();
                        dr[3] = pfamount;
                        Dt.Rows.Add(dr);


                        string cmptype = dsretriveamt.Tables[0].Rows[i]["ComponentType"].ToString();
                        string amt = dsretriveamt.Tables[0].Rows[i]["amount"].ToString();
                        //  TextBox txtamt = (TextBox)grd_salary.Rows[i].FindControl("txtamount");
                        if (cmptype == "Earning")
                        {
                            earnings = earnings + (decimal.Parse(amt.ToString()));
                        }
                        else
                        {
                            deductions = deductions + (decimal.Parse(amt.ToString()));
                        }
                    }


                    txtdeductions.Text = deductions.ToString();
                    txtnetpay.Text = (earnings - deductions).ToString();
                    //txtgross.Text = txtsalary.Text.ToString();
                    txtgross.Text = earnings.ToString();
                    txtsalarydate.Text = dscheck.Tables[0].Rows[0][0].ToString();

                }
                else
                {


                    var component1 = from m in db.SalaryComponentTBs
                                     select m;
                    foreach (var item in component1)
                    {
                        DataRow dr = Dt.NewRow();
                        dr[0] = item.ComponentName;


                        if (item.ComponentName == "P.F.")
                        {
                            DataTable dspfsetting = g.ReturnData("select pfid,Startlimit,endlimit,valuetype,percentagevalue,fixedvalue from PFDynamicSettingTB where '" + txtsalary.Text + "' between startlimit and endlimit");
                            if (dspfsetting.Rows.Count > 0)
                            {
                                string valutype = dspfsetting.Rows[0]["valuetype"].ToString();
                                if (valutype == "Fixed")
                                {

                                    dr[1] = dspfsetting.Rows[0]["fixedvalue"].ToString();
                                }
                                else
                                {
                                    dr[1] = dspfsetting.Rows[0]["percentagevalue"].ToString();
                                }
                            }
                            else
                            {
                                dr[1] = 0;
                            }

                        }
                        else
                        {
                            if (item.PercentageValue == null || item.PercentageValue == "0")
                            {
                                dr[1] = item.FixedValue;
                            }
                            else
                            {
                                dr[1] = item.PercentageValue;
                            }

                        }

                        dr[2] = item.ComponentType;

                        dr[3] = 0;
                        Dt.Rows.Add(dr);
                    }
                }


                ViewState["DT"] = Dt;

                grd_salary.DataSource = Dt;
                grd_salary.DataBind();
                //}
                //else
                //{
                //    grd_salary.DataSource = null;
                //    grd_salary.DataBind();
                //    g.ShowMessage(this.Page, "Please Enter Correct Employee Id  ..");
                //}
            }
        }
        catch
        {

        }
    }
    decimal earnings, deductions;

    decimal finalvalue = 0;

    protected void btnAmount_Click(object sender, EventArgs e)
    {
        BindCalculateComponentAmount();
    }

    private void BindCalculateComponentAmount()
    {
        try
        {
            #region Previous Logic

            //for (int ii = 0; ii < grd_salary.Rows.Count; ii++)
            //{

            //    string a = grd_salary.Rows[ii].Cells[0].Text;
            //    string b = grd_salary.Rows[ii].Cells[1].Text;

            //    if (a == "Basic")
            //    {
            //        TextBox txt = (TextBox)grd_salary.Rows[ii].FindControl("txtamount");
            //        basicamnt = (int.Parse(b) * int.Parse(txtsalary.Text)) / 100;
            //        txt.Text = basicamnt.ToString();

            //    }
            //    else if (a == "H.R.A.")
            //    {
            //        hraamount = ((int.Parse(b) * basicamnt) / 100);
            //        TextBox txt = (TextBox)grd_salary.Rows[ii].FindControl("txtamount");
            //        txt.Text = hraamount.ToString();
            //    }
            //    else if (a == "T.A")
            //    {
            //        TextBox txt = (TextBox)grd_salary.Rows[ii].FindControl("txtamount");
            //        txt.Text = "0";
            //    }
            //    else if (a == "P.F.")
            //    {
            //        TextBox txt = (TextBox)grd_salary.Rows[ii].FindControl("txtamount");
            //        if (txt.Text != "780" && txt.Text != "")
            //        {
            //        }
            //        else
            //        {
            //            //txt.Text = 780;
            //            txt.Text = b;
            //        }
            //    }
            //    else if (a == "Medical Allownce")
            //    {

            //        TextBox txt = (TextBox)grd_salary.Rows[ii].FindControl("txtamount");
            //        if (txt.Text != "1250" && txt.Text != "")
            //        {
            //        }
            //        else
            //        {
            //            //txt.Text = "1250";
            //            txt.Text = b;
            //        }
            //    }
            //    else if (a == "Conveyance Allowance")
            //    {
            //        TextBox txt = (TextBox)grd_salary.Rows[ii].FindControl("txtamount");
            //        if (txt.Text != "800" && txt.Text != "")
            //        {
            //        }
            //        else
            //           // txt.Text = "800";
            //        txt.Text = b;
            //    }
            //    else if (a == "Special Allowance")
            //    {

            //        TextBox txt = (TextBox)grd_salary.Rows[ii].FindControl("txtamount");
            //        txt.Text = (int.Parse(txtsalary.Text) - basicamnt - hraamount - 800 - 1250).ToString();
            //    }
            //    else if (a == "Professional Tax")
            //    {
            //        var pt = (from m in db.PTMasterTBs

            //                  select m);
            //        TextBox txt = (TextBox)grd_salary.Rows[ii].FindControl("txtamount");
            //        DataTable dt = g.ReturnData("select " + DateTime.Now.Date.ToString("MMM") + " from PTMasterTB where '" + txtsalary.Text + "'>=slabfrom and '" + txtsalary.Text + "'<=slabto");
            //        txt.Text = dt.Rows[0][0].ToString();
            //    }
            //    else
            //    {
            //        TextBox txt = (TextBox)grd_salary.Rows[ii].FindControl("txtamount");
            //        if (txt.Text != "0" && txt.Text != "")
            //        {
            //        }
            //        else
            //        {
            //            txt.Text = "0";
            //        }
            //    }
            //}
            //for (int ii = 0; ii < grd_salary.Rows.Count; ii++)
            //{
            //    string a = grd_salary.Rows[ii].Cells[2].Text;
            //    TextBox txtamt = (TextBox)grd_salary.Rows[ii].FindControl("txtamount");
            //    if (a == "Earning")
            //    {
            //        earnings = earnings + (decimal.Parse(txtamt.Text));
            //    }
            //    else
            //    {
            //        deductions = deductions + (decimal.Parse(txtamt.Text));
            //    }
            //}
            //txtgross.Text = earnings.ToString();
            //txtdeductions.Text = deductions.ToString();
            //txtnetpay.Text = (earnings - deductions).ToString();


            #endregion
            #region Code ..............

            if (txtsalary.Text == null || txtsalary.Text == "")
            {
                g.ShowMessage(this.Page, "Please enter your Salary  ..");
            }
            else
            {
                decimal ded = 0, sum = 0, totalsal = 0;

                for (int ii = 0; ii < grd_salary.Rows.Count; ii++)
                {
                    string type = grd_salary.Rows[ii].Cells[0].Text;//ValueType
                    string component = grd_salary.Rows[ii].Cells[0].Text;//Component Name
                    string calon = grd_salary.Rows[ii].Cells[1].Text;//PercentageComponentType

                    string value = "0";

                    decimal sal = Convert.ToDecimal(txtsalary.Text);
                    decimal per = 0, fixedvalue = 0, comp = 0, total = 0;

                    if (Convert.ToString(txtsalary.Text) == null || Convert.ToString(txtsalary.Text) == "0" || Convert.ToString(txtsalary.Text) == "")
                    {
                        g.ShowMessage(this.Page, "Please Enter Correct Salary .."); break;
                    }
                    else
                    {
                        DataTable dsvalue = g.ReturnData("SELECT     Componentid, ComponentType, ComponentName, ValueType, isnull(PercentageValue,0) per, PercenComponentType,isnull(FixedValue,0) fx, Status FROM         SalaryComponentTB where ComponentName like '" + component + "%'");
                        TextBox txt = (TextBox)grd_salary.Rows[ii].FindControl("txtamount");

                        if (dsvalue.Rows[0]["ComponentName"].ToString() != "Special Allowance" && dsvalue.Rows[0]["ComponentName"].ToString() != "Professional Tax" && dsvalue.Rows[0]["ComponentName"].ToString() != "P.F.")
                        {
                            if (Convert.ToDecimal(dsvalue.Rows[0]["per"].ToString()) != 0)
                            {
                                per = Convert.ToDecimal(dsvalue.Rows[0]["per"].ToString());

                                if (!string.IsNullOrEmpty(dsvalue.Rows[0]["PercenComponentType"].ToString()))
                                {
                                    DataTable dscheck = g.ReturnData("SELECT Componentid, ComponentType, ComponentName, ValueType, isnull(PercentageValue,0) per, PercenComponentType,isnull(FixedValue,0) fx, Status FROM         SalaryComponentTB where ComponentName like '" + dsvalue.Rows[0]["PercenComponentType"] + "%'");
                                    if (dscheck.Rows.Count > 0)
                                    {
                                        comp = (per * ((Convert.ToDecimal(dscheck.Rows[0]["per"].ToString()) * Convert.ToDecimal(sal)) / 100) / 100);
                                        txt.Text = comp.ToString();
                                    }
                                    else
                                    {
                                        comp = (per * sal) / 100;
                                        txt.Text = comp.ToString();
                                    }
                                }
                            }
                            else
                            {
                                fixedvalue = Convert.ToDecimal(dsvalue.Rows[0]["fx"].ToString());
                                if (txt.Text == "0.00" || txt.Text == "")
                                    txt.Text = fixedvalue.ToString();
                                
                            }

                            if (dsvalue.Rows[0]["ComponentType"].ToString() == "Earning")
                            {
                                sum = sum + Convert.ToDecimal(txt.Text);
                                Session["Summ"] = sum;
                            }
                        }
                        else if (component == "P.F.")
                        {
                            DataTable dspfsetting = g.ReturnData("select pfid,Startlimit,endlimit,valuetype,percentagevalue,fixedvalue from PFDynamicSettingTB where '" + txtsalary.Text + "' between startlimit and endlimit");
                            if (dspfsetting.Rows.Count > 0)
                            {
                                string valutype = dspfsetting.Rows[0]["valuetype"].ToString();
                                if (valutype == "Fixed")
                                {
                                    txt.Text = dspfsetting.Rows[0]["fixedvalue"].ToString();
                                }
                                else
                                {
                                    txt.Text = ((Convert.ToDecimal(txtsalary.Text) * Convert.ToDecimal(dspfsetting.Rows[0]["percentagevalue"].ToString())) / 100).ToString();

                                }
                            }
                            else
                            {
                                txt.Text = "0";
                            }
                        }
                        else
                        {
                            if (component == "Special Allowance")
                            {
                                txt.Text = (Convert.ToInt32(txtsalary.Text) - Convert.ToInt32(Session["Summ"])).ToString();
                            }
                            if (component == "Professional Tax")
                            {
                                var pt = (from m in db.PTMasterTBs
                                          select m);
                                //  TextBox txt = (TextBox)grd_salary.Rows[ii].FindControl("txtamount");
                                DataTable dt = g.ReturnData("select " + DateTime.Now.Date.ToString("MMM") + " from PTMasterTB where '" + txtsalary.Text + "'>=slabfrom and '" + txtsalary.Text + "'<=slabto");
                                if (dt.Rows.Count > 0)
                                {
                                    txt.Text = dt.Rows[0][0].ToString();
                                }

                                //txt.Text = (Convert.ToInt32(txtsalary.Text) - Convert.ToInt32(Session["Summ"])).ToString();
                            }
                        }

                    }



                    //txtdeductions.Text = ded.ToString();
                    ////  txtnetpay.Text = (Convert.ToDecimal(sum) - Convert.ToDecimal(ded)).ToString();
                    //txt.Text = (Convert.ToDecimal(sum) - Convert.ToDecimal(ded)).ToString();

                    //txtgross.Text = txtsalary.Text.ToString();

                    #region

                    //    if (type == "Percentage")
                    //    {
                    //        value = grd_salary.Rows[ii].Cells[1].Text;//Percentage amount
                    //        //finalvalue=
                    //    }
                    //    else
                    //    {
                    //        value = grd_salary.Rows[ii].Cells[1].Text;//Amount

                    //    }
                    //    string a = grd_salary.Rows[ii].Cells[0].Text;
                    //    string b = grd_salary.Rows[ii].Cells[1].Text;


                    //    if (a == "Basic")
                    //    {
                    //        TextBox txt = (TextBox)grd_salary.Rows[ii].FindControl("txtamount");
                    //        basicamnt = (int.Parse(b) * int.Parse(txtsalary.Text)) / 100;
                    //        txt.Text = basicamnt.ToString();

                    //    }
                    //    else if (a == "H.R.A.")
                    //    {
                    //        hraamount = ((int.Parse(b) * basicamnt) / 100);
                    //        TextBox txt = (TextBox)grd_salary.Rows[ii].FindControl("txtamount");
                    //        txt.Text = hraamount.ToString();
                    //    }
                    //    else if (a == "T.A")
                    //    {
                    //        TextBox txt = (TextBox)grd_salary.Rows[ii].FindControl("txtamount");
                    //        txt.Text = "0";
                    //    }
                    //    else if (a == "P.F.")
                    //    {
                    //        TextBox txt = (TextBox)grd_salary.Rows[ii].FindControl("txtamount");
                    //        if (txt.Text != "780" && txt.Text != "")
                    //        {
                    //        }
                    //        else
                    //        {
                    //            txt.Text = "780";
                    //        }
                    //    }
                    //    else if (a == "Medical Allowance")
                    //    {

                    //        TextBox txt = (TextBox)grd_salary.Rows[ii].FindControl("txtamount");
                    //        if (txt.Text != "1250" && txt.Text != "")
                    //        {
                    //        }
                    //        else
                    //        {
                    //            txt.Text = "1250";
                    //        }
                    //    }
                    //    else if (a == "Conveyance Allowance")
                    //    {
                    //        TextBox txt = (TextBox)grd_salary.Rows[ii].FindControl("txtamount");
                    //        if (txt.Text != "800" && txt.Text != "")
                    //        {
                    //        }
                    //        else
                    //            txt.Text = "800";
                    //    }
                    //    else if (a == "Special Allowance")
                    //    {

                    //        TextBox txt = (TextBox)grd_salary.Rows[ii].FindControl("txtamount");
                    //        txt.Text = (int.Parse(txtsalary.Text) - basicamnt - hraamount - 800 - 1250).ToString();
                    //    }
                    //    else if (a == "Professional Tax")
                    //    {

                    //        var pt = (from m in db.PTMasterTBs

                    //                  select m);
                    //        TextBox txt = (TextBox)grd_salary.Rows[ii].FindControl("txtamount");
                    //        DataTable dt = g.ReturnData("select " + DateTime.Now.Date.ToString("MMM") + " from PTMasterTB where '" + txtsalary.Text + "'>=slabfrom and '" + txtsalary.Text + "'<=slabto");
                    //        txt.Text = dt.Rows[0][0].ToString();

                    //    }
                    //    else
                    //    {
                    //        TextBox txt = (TextBox)grd_salary.Rows[ii].FindControl("txtamount");
                    //        if (txt.Text != "0" && txt.Text != "")
                    //        {
                    //        }
                    //        else
                    //            txt.Text = "0";
                    //    }


                    //}
                    #endregion

                    //  txtnetpay.Text = (Convert.ToDecimal(sum) - Convert.ToDecimal(ded)).ToString();

                    //for (int t = 0; t < grd_salary.Rows.Count; t++)
                    //{
                    //    string a = grd_salary.Rows[t].Cells[2].Text;
                    //    TextBox txtamt = (TextBox)grd_salary.Rows[t].FindControl("txtamount");
                    //    if (a == "Earning")
                    //    {
                    //        earnings = earnings + (decimal.Parse(txtamt.Text));
                    //    }
                    //    else
                    //    {
                    //        deductions = deductions + (decimal.Parse(txtamt.Text));
                    //    }
                    //    txtamt.Text = Convert.ToString(Convert.ToDecimal(txtsalary.Text) - (earnings - deductions) - deductions);
                    //}
                    //txtgross.Text = earnings.ToString();
                    //txtdeductions.Text = deductions.ToString();
                    //txtnetpay.Text = (earnings - deductions).ToString();

            #endregion
                }
                for (int t = 0; t < grd_salary.Rows.Count; t++)
                {
                    if (Convert.ToString(txtsalary.Text) == null || Convert.ToString(txtsalary.Text) == "0" || Convert.ToString(txtsalary.Text) == "")
                    {
                        g.ShowMessage(this.Page, "Please Enter Correct Salary .."); break;
                    }
                    else
                    {
                        string a = grd_salary.Rows[t].Cells[2].Text;
                        TextBox txtamt = (TextBox)grd_salary.Rows[t].FindControl("txtamount");
                        if (a == "Earning")
                        {
                            earnings = earnings + (decimal.Parse(txtamt.Text));
                        }
                        else
                        {
                            deductions = deductions + (decimal.Parse(txtamt.Text));
                        }
                        //txtamt.Text = Convert.ToString(Convert.ToDecimal(txtsalary.Text) - (earnings - deductions) - deductions);
                    }
                }

                if (earnings  > Convert.ToDecimal(txtsalary.Text))
                {
                    g.ShowMessage(this.Page, "Varify Gross Salary Amount");
                    txtdeductions.Text = null;
                    txtnetpay.Text = null;
                    txtgross.Text = null;
                }
                else
                {

                    txtdeductions.Text = deductions.ToString();
                    txtnetpay.Text = (earnings-deductions).ToString();
                    txtgross.Text = earnings.ToString();
                }
            }
        }
        catch
        {

        }
    }
    public void Clear()
    {
        txtaccount.Text = null;
        txtempid.Text = null;
        txtmachinid.Text = null;
        txtname.Text = null;
        txtgross.Text = null;
        txtesicaccnumber.Text = null;
        txtdesg.Text = null;
        txtDOJ.Text = null;
        txtdeductions.Text = null;
        txtnetpay.Text = null;
        txtsalarydate.Text = null;
        txtdept.Text = null;
        txtcompanyname.Text = null;
        txtsalary.Text = null;
        txtpfaccount.Text = null;
        txtPan1.Text = null;
        ddlsalarytype.SelectedIndex = 0;
        txtbankname.Text = null;
        Session["EmpIDSalID"] = null;
        Session["Summ"] = "";
        for (int i = 0; i < grd_salary.Rows.Count; i++)
        {
            TextBox t = (TextBox)grd_salary.Rows[i].FindControl("txtamount");
            t.Text = "";
        }

    }

    protected void BtnSave_Click(object sender, EventArgs e)
    {
        if (BtnSave.Text == "Save")
        {
            if (txtempid.Text != null)
            {
                InsertData();
            }
        }
    }

    private void InsertData()
    {
        try
        {         // var empid = txtempid.Text.Remove(4) ;
            string empid = Session["EmpIDSalID"].ToString();
            string salaryDate = txtsalarydate.Text;

            EmployeeTB EMP = db.EmployeeTBs.Where(d => d.EmployeeId == Convert.ToInt32(empid)).First();
            EMP.MachineID = txtmachinid.Text;
            EMP.NetSalary = txtsalary.Text;
            db.SubmitChanges();

            //Employee Salary
            EmployeeSalarySettingsTB Empsal = new EmployeeSalarySettingsTB();
            Empsal.Empid = int.Parse(empid);
            Empsal.Salatrydate = DateTime.Parse(salaryDate.ToString());
            Empsal.GrossSalary = txtgross.Text;
            Empsal.deductions = txtdeductions.Text;
            Empsal.netpay = txtnetpay.Text;
            db.EmployeeSalarySettingsTBs.InsertOnSubmit(Empsal);
            db.SubmitChanges();

            DataSet dscheck = g.ReturnData1("delete from SalaryDetail where EmployeeId=" + empid + "");

            for (int ii = 0; ii < grd_salary.Rows.Count; ii++)
            {
                string a = grd_salary.Rows[ii].Cells[0].Text;
                var componentid = (from m in db.SalaryComponentTBs
                                   where m.ComponentName == a
                                   select m.Componentid).First();

                string ComponentType = grd_salary.Rows[ii].Cells[2].Text;
                TextBox txt = (TextBox)grd_salary.Rows[ii].FindControl("txtamount");
                string Amount = txt.Text;


                //Salary Details
                SalaryDetail SD = new SalaryDetail();
                SD.EmpSalarysettingid = Empsal.EmpSalaryid;
                SD.Componentid = int.Parse(componentid.ToString());
                SD.EmployeeId = int.Parse(empid);
                SD.ComponentType = ComponentType;
                SD.amount = Amount;
                SD.SalaryDate = DateTime.Parse(salaryDate.ToString());
                db.SalaryDetails.InsertOnSubmit(SD);
                db.SubmitChanges();

            }

            Clear();
            g.ShowMessage(this.Page, "Add Data Successfully");
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
    protected void txtamount_TextChanged(object sender, EventArgs e)
    {
        decimal earn = 0, deduct = 0, TotalGross = 0, TotalNet = 0;

        for (int i = 0; i < grd_salary.Rows.Count; i++)
        {
            string Comptype = grd_salary.Rows[i].Cells[2].Text;//ValueType
            string amt = grd_salary.Rows[i].Cells[3].Text;//Component Name
            string calon = grd_salary.Rows[i].Cells[1].Text;//PercentageComponentType
            string ComponentName = grd_salary.Rows[i].Cells[0].Text;//PercentageComponentType
            TextBox txt = (TextBox)grd_salary.Rows[i].FindControl("txtamount");
            if (Comptype == "Earning")
            {
                #region

                DataTable dsvalue = g.ReturnData("SELECT     Componentid, ComponentType, ComponentName, ValueType, isnull(PercentageValue,0) per, PercenComponentType,isnull(FixedValue,0) fx, Status FROM         SalaryComponentTB where ComponentName like '" + ComponentName + "%'");
                if (Convert.ToDecimal(dsvalue.Rows[0]["per"].ToString()) == 0)

                { earn = Convert.ToDecimal(earn) + Convert.ToDecimal(txt.Text); }
                else
                {
                   decimal per = Convert.ToDecimal(dsvalue.Rows[0]["per"].ToString());
                    txt.Text = (per *  Convert.ToDecimal(txtsalary.Text)/100).ToString();
                    earn = Convert.ToDecimal(earn) + Convert.ToDecimal(txt.Text);
                }

                #endregion
            }
            else
            {
                #region
                deduct = Convert.ToDecimal(deduct) + Convert.ToDecimal(txt.Text);
                #endregion
            }
        }
        //txtdeductions.Text = deduct.ToString();
        //txtnetpay.Text = (earn).ToString();
        //txtgross.Text = txtsalary.Text.ToString();
        if ( earn > Convert.ToDecimal(txtsalary.Text))
        {
            g.ShowMessage(this.Page, "Varify Gross Salary Amount");
            txtdeductions.Text = null;
            txtnetpay.Text = null;
            txtgross.Text = null;
        }
        else
        {

            txtdeductions.Text = deduct.ToString();
            txtgross.Text = earn.ToString();
            txtnetpay.Text = (earn-deduct).ToString();
        }
    
       
        //txtnetpay.Text = deduct.ToString();
        //txtgross.Text = earn.ToString();
    }
    protected void txtname_TextChanged(object sender, EventArgs e)
    {
        try
        {
            if (!string.IsNullOrEmpty(txtname.Text))
            {
                string EmpId = "";
                var GetEmployeeId = from e1 in db.EmployeeTBs
                                    where e1.FName + " " + e1.Lname == txtname.Text
                                    select new
                                    {
                                        e1.EmployeeId,
                                    };
                if (GetEmployeeId.Count() > 0)
                {
                    foreach (var item in GetEmployeeId)
                    {
                        txtempid.Text = "100" + Convert.ToString(item.EmployeeId);
                        EmpId = Convert.ToString(item.EmployeeId);
                        Session["EmpIDSalID"] = EmpId;
                    }


                    if (txtempid.Text != null)
                    {

                        var EmpData = (from d in db.EmployeeTBs
                                       join n in db.MasterDeptTBs on d.DeptId equals n.DeptID
                                       where d.EmployeeId == int.Parse(EmpId) && d.DeptId != null 
                                       select new
                                       {
                                           Name = d.FName + " " + d.MName + " " + d.Lname,
                                           d.FName,
                                           d.Lname,
                                           d.MachineID,
                                           DOJ1 = d.DOJ,
                                           d.CurState,
                                           d.BankName,
                                           d.Salarytype,
                                           d.SalaryAccountNo,
                                           d.ESICAccountNo,
                                           d.PFAccountNo,
                                           d.PanNo,
                                           n.DeptName,
                                           d.NetSalary
                                       }).First();
                        txtmachinid.Text = EmpData.MachineID;
                        txtname.Text = EmpData.Name;
                        txtDOJ.Text = EmpData.DOJ1.Value.ToString("MM/dd/yyyy");
                        txtdept.Text = EmpData.DeptName;
                        txtPan1.Text = EmpData.PanNo;
                      //  txtsalary.Text = EmpData;
                        txtpfaccount.Text = EmpData.PFAccountNo;
                        txtaccount.Text = EmpData.SalaryAccountNo;
                        ddlsalarytype.Text = EmpData.Salarytype;
                        txtbankname.Text = EmpData.BankName;
                        txtesicaccnumber.Text = EmpData.ESICAccountNo;

                        if (EmpData.NetSalary != null)
                            txtsalary.Text = EmpData.NetSalary;
                        else
                            txtsalary.Text = string.Empty;
                        txtcompanyname.Text = (from m in db.EmployeeTBs
                                               join n in db.CompanyInfoTBs on m.CompanyId equals n.CompanyId
                                               where m.EmployeeId == int.Parse(EmpId)
                                               select n.CompanyName).First();
                        txtdesg.Text = (from m in db.EmployeeTBs
                                        join n in db.MasterDesgTBs on m. DesgId equals n.DesigID
                                        where m.EmployeeId == int.Parse(EmpId)
                                        select n.DesigName).First();

                        //new code
                        DataColumn Component = Dt.Columns.Add("Component");
                        DataColumn percentageValue = Dt.Columns.Add("percentageValue");
                        DataColumn ComponentType = Dt.Columns.Add("ComponentType");
                        DataColumn amount = Dt.Columns.Add("amount");

                        DataSet dscheck = g.ReturnData1("Select top 1 convert(varchar, Salatrydate,101) date,GrossSalary,convert(float,deductions)+convert(float,netpay) as gross   from EmployeeSalarySettingsTB where empid=" + EmpId + " order by empsalaryid");
                        if (dscheck.Tables[0].Rows.Count > 0)
                        {
                            //txtsalary.Text = dscheck.Tables[0].Rows[0]["GrossSalary"].ToString();
                            txtsalary.Text = dscheck.Tables[0].Rows[0]["GrossSalary"].ToString();
                          //  txtgross.Text = dscheck.Tables[0].Rows[0]["GrossSalary"].ToString();//code added on 14th sep 2015

                            g.ShowMessage(this.Page, "Salary Setting is already done on " + dscheck.Tables[0].Rows[0][0].ToString() + " this date");

//                            DataSet dsretriveamt = g.ReturnData1(@"select SC.Componentid,SC.ComponentName,PercentageValue,FixedValue,SC.ComponentType,amount from SalaryComponentTB SC cross join
//EmployeeSalarySettingsTB ES left outer join SalaryDetail SD on SD.EmpSalarysettingid=ES.EmpSalaryid
//and SD.Componentid=SC.componentid where  employeeid ='" + EmpId + "' and  convert(date,ES.salatrydate,101) =convert(date,'" + dscheck.Tables[0].Rows[0][0].ToString() + "',101)");
                            DataSet dsretriveamt = g.ReturnData1(@"SELECT        Componentid, ComponentName, PercentageValue, FixedValue, ComponentType, ISNULL
                             ((SELECT        CASE WHEN ComponentName = 'Other' THEN 0 ELSE CONVERT(decimal(18, 2), isnull(amount, 0)) END AS amount
                                 FROM            EmployeeSalarySettingsTB AS ES LEFT OUTER JOIN
                                                          SalaryDetail AS SD ON SD.EmpSalarysettingid = ES.EmpSalaryid
                                 WHERE        (SD.Componentid = SC.Componentid) AND (SD.EmployeeId = '"+EmpId+"') AND (CONVERT(date, ES.Salatrydate, 101) = CONVERT(date, '"+dscheck.Tables[0].Rows[0][0].ToString()+"', 101))), 0) AS amount FROM            SalaryComponentTB AS SC");
                            for (int i = 0; i < dsretriveamt.Tables[0].Rows.Count; i++)
                            {

                                DataRow dr = Dt.NewRow();
                                dr[0] = dsretriveamt.Tables[0].Rows[i]["ComponentName"].ToString();

                                // Change 
                                decimal pfamount = 0;
                                if (dsretriveamt.Tables[0].Rows[i]["ComponentName"].ToString() == "P.F.")
                                {
                                    DataTable dspfsetting = g.ReturnData("select pfid,Startlimit,endlimit,valuetype,percentagevalue,fixedvalue from PFDynamicSettingTB where '" + txtsalary.Text + "' between startlimit and endlimit");
                                    if(dspfsetting.Rows.Count > 0)
                                    {
                                        string valutype = dspfsetting.Rows[0]["valuetype"].ToString();
                                        if(valutype == "Fixed")
                                        {
                                            pfamount = Convert.ToDecimal(dspfsetting.Rows[0]["fixedvalue"].ToString());
                                            dr[1] = dspfsetting.Rows[0]["fixedvalue"].ToString();
                                        }
                                        else
                                        {
                                            pfamount = (Convert.ToDecimal(txtsalary.Text) * Convert.ToDecimal(dspfsetting.Rows[0]["percentagevalue"].ToString())) / 100;
                                            dr[1] = dspfsetting.Rows[0]["percentagevalue"].ToString();
                                        }
                                    }
                                    else
                                    {
                                        pfamount = 0;
                                    }

                                }                                
                                else
                                {
                                    if (string.IsNullOrEmpty(dsretriveamt.Tables[0].Rows[i]["PercentageValue"].ToString()) || Convert.ToDecimal(dsretriveamt.Tables[0].Rows[i]["PercentageValue"].ToString()) == 0)
                                    {
                                        dr[1] = dsretriveamt.Tables[0].Rows[i]["FixedValue"].ToString();
                                    }
                                    else
                                    {
                                        dr[1] = dsretriveamt.Tables[0].Rows[i]["PercentageValue"].ToString();
                                    }
                                    pfamount = Convert.ToDecimal(dsretriveamt.Tables[0].Rows[i]["amount"].ToString());
                                }

                               
                                dr[2] = dsretriveamt.Tables[0].Rows[i]["ComponentType"].ToString();
                                dr[3] = pfamount;
                                Dt.Rows.Add(dr);


                                string a = dsretriveamt.Tables[0].Rows[i]["ComponentType"].ToString();
                                string amt = dsretriveamt.Tables[0].Rows[i]["amount"].ToString();
                              //  TextBox txtamt = (TextBox)grd_salary.Rows[i].FindControl("txtamount");
                                if (a == "Earning")
                                {
                                    earnings = earnings + (decimal.Parse(amt.ToString()));
                                }
                                else
                                {
                                    deductions = deductions + (decimal.Parse(amt.ToString()));
                                }
                            }
                            

                            txtdeductions.Text = deductions.ToString();
                            txtnetpay.Text = (earnings - deductions).ToString();
                            //txtgross.Text = txtsalary.Text.ToString();
                            txtgross.Text = earnings.ToString();
                            txtsalarydate.Text = dscheck.Tables[0].Rows[0][0].ToString();

                        }


                        else
                        {
                            var component1 = from m in db.SalaryComponentTBs
                                             select m;
                            foreach (var item in component1)
                            {
                                DataRow dr = Dt.NewRow();
                                dr[0] = item.ComponentName;

                                // Change 

                                if (item.ComponentName == "P.F.")
                                {
                                    DataTable dspfsetting = g.ReturnData("select pfid,Startlimit,endlimit,valuetype,percentagevalue,fixedvalue from PFDynamicSettingTB where '" + txtsalary.Text + "' between startlimit and endlimit");
                                    if (dspfsetting.Rows.Count > 0)
                                    {
                                        string valutype = dspfsetting.Rows[0]["valuetype"].ToString();
                                        if (valutype == "Fixed")                                        {
                                          
                                            dr[1] = dspfsetting.Rows[0]["fixedvalue"].ToString();
                                        }
                                        else
                                        {                                            
                                            dr[1] = dspfsetting.Rows[0]["percentagevalue"].ToString();
                                        }
                                    }
                                    else
                                    {
                                        dr[1] = 0;
                                    }

                                }
                                else
                                {
                                    if (item.PercentageValue == null || item.PercentageValue == "0")
                                    {
                                        dr[1] = item.FixedValue;
                                    }
                                    else
                                    {
                                        dr[1] = item.PercentageValue;
                                    }
                                    
                                }
                              
                                dr[2] = item.ComponentType;

                                dr[3] = 0;
                                Dt.Rows.Add(dr);
                            }
                            txtdeductions.Text = "0";
                            txtnetpay.Text = "0";
                            txtgross.Text = txtsalary.Text.ToString();
                        }

                        ViewState["DT"] = Dt;

                        grd_salary.DataSource = Dt;
                        grd_salary.DataBind();
                        //}
                        //else
                        //{
                        //    grd_salary.DataSource = null;
                        //    grd_salary.DataBind();
                        //    g.ShowMessage(this.Page, "Please Enter Correct Employee Id  ..");
                        //}
                    }

                }
                else
                {
                    txtempid.Text = string.Empty;
                }
            }
        }
        catch (Exception exc)
        {
            g.ShowMessage(Page, "Please Contact Admin.\n '" + exc + "'");
        }
    }
}