using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Recruitment_SpecialityMaster : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal(); 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"]!=null)
        {
            if (!IsPostBack)
            {
                bindgrid();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void bindgrid()
    {
        DataTable dtSpeciality = g.ReturnData("Select SpecialityId,SpecialityName from SpecialityMasterTB");
        if (dtSpeciality.Rows.Count>0)
        {
            grdSpeciality.DataSource = dtSpeciality;
            grdSpeciality.DataBind();
            lblCount.Text = dtSpeciality.Rows.Count.ToString();
        }
        else
        {
            grdSpeciality.DataSource = null;
            grdSpeciality.DataBind();
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }
    protected void imgedit_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton imgbtn = (ImageButton)sender;
        lblspicalId.Text = imgbtn.CommandArgument;
        var spacialdata = from d in HR.SpecialityMasterTBs
                          where d.SpecialityId==Convert.ToInt32(lblspicalId.Text)
                          select new { d.SpecialityName};
        foreach (var item in spacialdata)
        {
            txtspeciality.Text = item.SpecialityName;
        }
        MultiView1.ActiveViewIndex = 1;
        btnsave.Text = "Update";
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
        txtspeciality.Text = "";
        btnsave.Text = "Save";
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        if (btnsave.Text == "Save")
        {
            var existdata = from d in HR.SpecialityMasterTBs where d.SpecialityName == txtspeciality.Text select d;
            if (existdata.Count() > 0)
            {
                g.ShowMessage(this.Page, "Speciality Name already Exist");
            }
            else
            {
                SpecialityMasterTB SP = new SpecialityMasterTB();
                SP.SpecialityName = txtspeciality.Text;
                HR.SpecialityMasterTBs.InsertOnSubmit(SP);
                HR.SubmitChanges();
                g.ShowMessage(this.Page, "Speciality Details Saved Successfully");
                bindgrid();
                MultiView1.ActiveViewIndex = 0;
                txtspeciality.Text = "";
                btnsave.Text = "Save";
            }
        }
        else
        {
            var existdata = from d in HR.SpecialityMasterTBs where d.SpecialityName == txtspeciality.Text && d.SpecialityId != Convert.ToInt32(lblspicalId.Text) select d;
            if (existdata.Count() > 0)
            {
                g.ShowMessage(this.Page, "Speciality Name already Exist");
            }
            else
            {
                SpecialityMasterTB SP = HR.SpecialityMasterTBs.Where(d => d.SpecialityId == Convert.ToInt32(lblspicalId.Text)).First();
                SP.SpecialityName = txtspeciality.Text;
                HR.SubmitChanges();
                g.ShowMessage(this.Page, "Speciality Details Updated Successfully");
                bindgrid();
                MultiView1.ActiveViewIndex = 0;
                txtspeciality.Text = "";
                btnsave.Text = "Save";
            }
        }
       
    }
    protected void grdSpeciality_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSpeciality.PageIndex = e.NewPageIndex;
        bindgrid();

    }
}