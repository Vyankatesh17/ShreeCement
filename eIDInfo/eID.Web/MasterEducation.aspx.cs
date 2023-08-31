using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterEducation : System.Web.UI.Page
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
                BindAllDept();
           
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }

    }
  
    public void BindAllDept()
    {

        var deptData = (from d in HR.MasterEducationTBs 
                       select new { d.EducationId, d.EducationName,Status = d.Status == 0 ? "Active" : "In Active" }).OrderByDescending(d=>d.EducationId);
        if (deptData.Count() > 0)
        {
            grd_Dept.DataSource = deptData;
            grd_Dept.DataBind();
            lblcnt.Text = deptData.Count().ToString();
        }
        else
        {
            grd_Dept.DataSource = null;
            grd_Dept.DataBind();
            lblcnt.Text = "0";
        }


    }

    protected void OnClick_Edit(object sender, ImageClickEventArgs e)
    {


        ImageButton Lnk = (ImageButton)sender;
        string DeptId = Lnk.CommandArgument;
        lbldeptid.Text = DeptId;
        MultiView1.ActiveViewIndex = 1;
        MasterEducationTB MT = HR.MasterEducationTBs.Where(d => d.EducationId == Convert.ToInt32(DeptId)).First();
        txtdeptname.Text = MT.EducationName;
       
        rd_status.SelectedIndex = Convert.ToInt32(MT.Status);
        btnsubmit.Text = "Update";

    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (btnsubmit.Text == "Save")
        {
             var dte = from p in HR.MasterEducationTBs.Where(d => d.EducationName == txtdeptname.Text) select p;
             if (dte.Count() > 0)
             {

                 g.ShowMessage(this.Page, "Education Details Already Exist");
                 //modpop.Message = "Education Name Already Exist";
                 //modpop.ShowPopUp();

             }
             else
             {

                 MasterEducationTB MTB = new MasterEducationTB();
                 MTB.EducationName = txtdeptname.Text;

                 MTB.Status = rd_status.SelectedIndex;
                 HR.MasterEducationTBs.InsertOnSubmit(MTB);
                 HR.SubmitChanges();
                 g.ShowMessage(this.Page, "Education Details Saved Successfully");
                 //modpop.Message = "Submitted Successfully";
                 //modpop.ShowPopUp();

                 Clear();
             }
        }
        else
        {
            var dte = from p in HR.MasterEducationTBs.Where(d => d.EducationName == txtdeptname.Text && d.EducationId == Convert.ToInt32(lbldeptid.Text)) select p;
            if (dte.Count() > 0)
            {
                updatecode();
                g.ShowMessage(this.Page, "Education Details Already Exist");
                //modpop.Message = "Education Name Already Exist";
                //modpop.ShowPopUp();

            }
            else
            {
                var dte1 = from p in HR.MasterEducationTBs.Where(d => d.EducationName == txtdeptname.Text && d.EducationId != Convert.ToInt32(lbldeptid.Text)) select p;
                if (dte1.Count() > 0)
                {

                    g.ShowMessage(this.Page, "Education Details Already Exist");
                    //modpop.Message = "Education Name Already Exist";
                    //modpop.ShowPopUp();

                }
                else
                {
                    updatecode();
                }

            } 
            

            
        }
    }

    private void updatecode()
    {
        try
        {
            MasterEducationTB MT = HR.MasterEducationTBs.Where(d => d.EducationId == Convert.ToInt32(lbldeptid.Text)).First();

            MT.EducationName = txtdeptname.Text;
            MT.Status = rd_status.SelectedIndex;
            HR.SubmitChanges();
            g.ShowMessage(this.Page, "Education Details Updated Successfully");
            //modpop.Message = "Updated Successfully";
            //modpop.ShowPopUp();
            btnsubmit.Text = "Save";
            Clear();
        }
        catch (Exception)
        {
            
            throw;
        }
    }

    public void Clear()
    {
        txtdeptname.Text = null;
        lbldeptid.Text = "";
        rd_status.SelectedIndex = 0;
        BindAllDept();
       
        MultiView1.ActiveViewIndex = 0;
        btnsubmit.Text = "Save";

    }



    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;

    }
    protected void grd_Dept_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Dept.PageIndex = e.NewPageIndex;
        BindAllDept();
        grd_Dept.DataBind();
    }
}