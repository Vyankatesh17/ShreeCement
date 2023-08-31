using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class EmployeeShift : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;

                fillempname();

                BindAllEmployeeShiftData();

                BindShiftData();
            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }

    }
    private void BindShiftData()
    {
        DataSet dsemp = g.ReturnData1("select ST.ShiftID,CT.CompanyName,ST.Shift,ST.Intime,ST.Outtime,ST.Latemark,ST.overtime,ST.starttime from MasterShiftTB ST join CompanyInfoTB CT ON CT.CompanyId=ST.CompanyId order by ST.ShiftID desc ");
        if (dsemp.Tables[0].Rows.Count > 0)
        {
            ddlShift.DataSource = dsemp.Tables[0];
            ddlShift.DataTextField = "Shift";
            ddlShift.DataValueField = "ShiftID";
            ddlShift.DataBind();
            ddlempname.Items.Insert(0, "--Select--");
        };
    }
    private void BindAllEmployeeShiftData()
    {
        DataSet dsemp = g.ReturnData1(@"SELECT        E.FName + ' ' + E.Lname AS name, E.EmployeeId, S.Intime, S.Outtime, S.Latemark, ES.EmployeeShiftID, S.Shift
FROM            EmployeeShiftTB AS ES INNER JOIN
                         MasterShiftTB AS S ON ES.Shift = S.ShiftID LEFT OUTER JOIN
                         EmployeeTB AS E ON E.EmployeeId = ES.EmployeeId
WHERE        (E.RelivingStatus IS NULL)");

        grd_Empshift.DataSource = dsemp.Tables[0];

        grd_Empshift.DataBind();
    }
    private void fillempname()
    {
        DataSet dsemp = g.ReturnData1("select FName+' '+Lname name,EmployeeId from employeetb where RelivingStatus is null");
        if (dsemp.Tables[0].Rows.Count > 0)
        {

            ddlempname.DataSource = dsemp.Tables[0];
            ddlempname.DataTextField = "name";
            ddlempname.DataValueField = "EmployeeId";
            ddlempname.DataBind();
            ddlempname.Items.Insert(0, "--Select--");
        }
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        //string  test = txtintimehours.Text;
        // string[] intimesplittime = (test).Split(':');

        // int inhours = Convert.ToInt32(intimesplittime[0]);

        //int inminutes =  Convert.ToInt32(intimesplittime[1].Substring(0,2));

        // string[] intimesplitampm = (txtintimehours.Text).Split(' ');

        // string intimeamapm = intimesplitampm[1].ToString();
        //if(intimeamapm=="PM")
        //{
        //    inhours =inhours + 12;
        //}

        // TimeSpan tin =  inhours 
        if (btnsubmit.Text == "Save")
        {

            var dataexist = from d in HR.EmployeeShiftTBs.Where(d => d.EmployeeId == Convert.ToInt32(ddlempname.SelectedValue)) select d;

            if (dataexist.Count() == 0)
            {
                EmployeeShiftTB ES = new EmployeeShiftTB();
                ES.EmployeeId = Convert.ToInt32(ddlempname.SelectedValue);
                ES.Shift = Convert.ToInt32(ddlShift.SelectedValue);
                ES.outtime = txtouttime.Text;
                ES.Latetime = txtlatemarks.Text;

                HR.EmployeeShiftTBs.InsertOnSubmit(ES);
                HR.SubmitChanges();

                EmployeeTB employee = HR.EmployeeTBs.Where(d => d.EmployeeId == ES.EmployeeId).FirstOrDefault();
                employee.CurrentShiftId = ES.EmployeeShiftID;
                HR.SubmitChanges();

                g.ShowMessage(this.Page, "Employee Shift Added Successfully!!");
                Clear();
            }
            else
            {
                g.ShowMessage(this.Page, "Employee Shift Already Exists!!!");
            }
        }
        else
        {
            EmployeeShiftTB ES = HR.EmployeeShiftTBs.Where(d => d.EmployeeShiftID == Convert.ToInt32(lblheadid.Text)).First();
            ES.EmployeeId = Convert.ToInt32(ddlempname.SelectedValue);
            ES.Shift = Convert.ToInt32(ddlShift.SelectedValue);
            ES.outtime = txtouttime.Text;
            ES.Latetime = txtlatemarks.Text;
            HR.SubmitChanges();

            EmployeeTB employee = HR.EmployeeTBs.Where(d => d.EmployeeId == ES.EmployeeId).FirstOrDefault();
            employee.CurrentShiftId = ES.EmployeeShiftID;
            HR.SubmitChanges();

            g.ShowMessage(this.Page, "Employee Shift updated Successfully!!");
            Clear();
        }
    }

    private void Clear()
    {

        ddlempname.SelectedIndex = 0;
     
        txtouttime.Text = "06:30 PM";
        txtlatemarks.Text = "09:35 AM";
        BindAllEmployeeShiftData();
        btnsubmit.Text = "Save";
        MultiView1.ActiveViewIndex = 0;
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }
    protected void lnk_Click(object sender, EventArgs e)
    {
        LinkButton lnk = sender as LinkButton;
        lblheadid.Text = lnk.CommandArgument.ToString();
        EmployeeShiftTB MTB = HR.EmployeeShiftTBs.Where(d => d.EmployeeShiftID == Convert.ToInt32(lnk.CommandArgument.ToString())).First();

        fillempname();

        ddlempname.SelectedValue = MTB.EmployeeId.ToString();
        ddlShift.SelectedValue = MTB.Shift.Value.ToString();
      
        txtouttime.Text = MTB.outtime.ToString();
        txtlatemarks.Text = MTB.Latetime.ToString();
        MultiView1.ActiveViewIndex = 1;
        btnsubmit.Text = "Update";

    }
    protected void btncancel1_Click(object sender, EventArgs e)
    {
        Clear();
    }
}