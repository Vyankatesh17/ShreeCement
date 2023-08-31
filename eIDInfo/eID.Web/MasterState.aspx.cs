using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterState : System.Web.UI.Page
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
                BindAllSource();

                BindCountry();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void BindCountry()
    {
        var data = (from d in HR.CountryTBs
                    where d.Status == 0
                    select d).Distinct();
        
        ddlcountry.DataSource = data;
        ddlcountry.DataTextField = "CountryName";
        ddlcountry.DataValueField = "CountryId";
        ddlcountry.DataBind();
        ddlcountry.Items.Insert(0, "--Select--");

 }
    public void Clear()
    {
        txtstatename.Text = null;
        lblstateid.Text = "";
     
        ddlcountry.SelectedIndex = 0;
        BindAllSource();
        MultiView1.ActiveViewIndex = 0;
        btnsubmit.Text = "Save";

    }
    public void BindAllSource()
    {
        var data = (from d in HR.StateTBs
                    select new
                    {
                        CounntryName = d.CountryTB.CountryName,
                        d.CountryId,
                        d.StateId,
                        d.StateName
                    }).Distinct();
        

        grd_State.DataSource = data;
            grd_State.DataBind();

            lblcount.Text = data.Count().ToString();


    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (btnsubmit.Text == "Save")
        {     // Changes by Abdul Rahman save & update code. date 03/12/2014
            var dt = from p in HR.StateTBs.Where(d => d.StateName == txtstatename.Text && d.CountryId == Convert.ToInt32(ddlcountry.SelectedValue)) select p;
            if (dt.Count() > 0)
            {
                g.ShowMessage(this.Page, "State Name Already Exist");
            }
            else
            {
                StateTB mtb = new StateTB();
                mtb.CountryId = Convert.ToInt32(ddlcountry.SelectedValue);
                mtb.StateName = txtstatename.Text;

                HR.StateTBs.InsertOnSubmit(mtb);
                HR.SubmitChanges();
                g.ShowMessage(this.Page, "State Details Saved Successfully");
                Clear();
            }
        }
        else
        {
            var dt = from p in HR.StateTBs.Where(d => d.StateName == txtstatename.Text && d.CountryId == Convert.ToInt32(ddlcountry.SelectedValue) && d.StateId != Convert.ToInt32(lblstateid.Text)) select p;
            if (dt.Count() > 0)
            {
                var dt1 = from p in HR.StateTBs.Where(d => d.StateName == txtstatename.Text && d.CountryId == Convert.ToInt32(ddlcountry.SelectedValue) && d.StateId == Convert.ToInt32(lblstateid.Text)) select p;
                if (dt1.Count() > 0)
                {
                    updatecode();
                }
                else
                {
                    g.ShowMessage(this.Page, "State Name Already Exist");
                }


            }
            else
            {
                updatecode();
            }
        }
    }

    private void updatecode()
    {
        try
        {
            StateTB mt = HR.StateTBs.Where(s => s.StateId == Convert.ToInt32(lblstateid.Text)).First();
            mt.CountryId = Convert.ToInt32(ddlcountry.SelectedValue);
            mt.StateName = txtstatename.Text;
           
            HR.SubmitChanges();
            g.ShowMessage(this.Page, "State Details Updated Successfully");
            //modpop.Message = "Updated Successfully";
            //modpop.ShowPopUp();
            btnsubmit.Text = "Save";
            Clear();
        }
        catch (Exception ex)
        {

            g.ShowMessage(this.Page, ex.Message);
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }
    protected void grd_Dept_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void Unnamed1_Click(object sender, ImageClickEventArgs e)
    {
       
        MultiView1.ActiveViewIndex = 1;

        ImageButton Lnk = (ImageButton)sender; // Changes by Abdul Rahman link to Image button
        string StateID = Lnk.CommandArgument;
        lblstateid.Text = StateID;

        StateTB mt = HR.StateTBs.Where(s => s.StateId == Convert.ToInt32(StateID)).First();
        txtstatename.Text = mt.StateName;
       
        ddlcountry.SelectedValue =Convert.ToString(mt.CountryId);
        
        btnsubmit.Text = "Update";
    }
    protected void txtstatename_TextChanged(object sender, EventArgs e)
    {

    }
   
    protected void ddlcountry_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void grd_State_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_State.PageIndex = e.NewPageIndex;
      //  BindCountry();
        BindAllSource();
        grd_State.DataBind();

    }
}