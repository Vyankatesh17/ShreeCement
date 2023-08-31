using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterCountry : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                bindAllCountry();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void bindAllCountry()
    {
        var countryData = (from dt in HR.CountryTBs 
                          select new
                          {
                              dt.CountryId,
                              dt.CountryName,
                              dt.CountryCode,
                            Status=dt.Status==0 ?"Active":"In Active"

                          }).OrderBy(d => d.CountryName);
        if (countryData.Count() > 0)
        {
         grd_City.DataSource = countryData;
        grd_City.DataBind();
        lblcnt.Text = countryData.Count().ToString();
        }
        else
        {
            grd_City.DataSource = null;
            grd_City.DataBind();
            lblcnt.Text = "0";
        }
       
        
    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (btnsubmit.Text=="Save")
        {
             var dte = from p in HR.CountryTBs.Where(d => d.CountryName == txtcountryname.Text && d.CountryCode == (txtcountrycode.Text)) select p;
             if (dte.Count() > 0)
             {

                 g.ShowMessage(this.Page, "Country Name Already Exist");
                 //modpop.Message = "Country Name Already Exist";
                 //modpop.ShowPopUp();

             }
             else
             {
                 CountryTB CT = new CountryTB();
                 CT.CountryName = txtcountryname.Text;
                 CT.CountryCode = txtcountrycode.Text;
                 CT.Status = Convert.ToInt32(rd_status.SelectedIndex);
                 HR.CountryTBs.InsertOnSubmit(CT);
                 HR.SubmitChanges();
                 g.ShowMessage(this.Page, "Country Details Saved Successfully");
                 //modpop.Message = "Country Detail Saved Successfully";
                 //modpop.ShowPopUp();
                 Clear();
                 bindAllCountry();
             }
        }
        else
        {
            var dte = from p in HR.CountryTBs.Where(d => d.CountryName == txtcountryname.Text && d.CountryCode == (txtcountrycode.Text) && d.CountryId == Convert.ToInt32(lblcityid.Text)) select p;
             if (dte.Count() > 0)
             {
                 updatecode();
                 
             }
             else
             {
                  var dte1 = from p in HR.CountryTBs.Where(d => d.CountryName == txtcountryname.Text && d.CountryCode == (txtcountrycode.Text) && d.CountryId != Convert.ToInt32(lblcityid.Text)) select p;
                  if (dte1.Count() > 0)
                  {
                      g.ShowMessage(this.Page, "Country Name Already Exist");

                  }
                  else
                  {
                      var dte2 = from p in HR.CountryTBs.Where(d => d.CountryCode == (txtcountrycode.Text) && d.CountryId != Convert.ToInt32(lblcityid.Text)) select p;
                      if (dte2.Count() > 0)
                      {
                          g.ShowMessage(this.Page, "Country Code Already Exist");

                      }
                      else
                      {
                          var dte3 = from p in HR.CountryTBs.Where(d => d.CountryName == txtcountryname.Text && d.CountryId != Convert.ToInt32(lblcityid.Text)) select p;
                          if (dte3.Count() > 0)
                          {
                              g.ShowMessage(this.Page, "Country Name Already Exist");

                          }
                          else
                          {
                            updatecode();
                          }
                         
                      }

                  }

                
             }
           
            
        }
       
       
    }

    private void updatecode()
    {
       // Created By Abdul rahman 
        // date : 03/12/2014
        try
        {
            CountryTB CT = HR.CountryTBs.Where(d => d.CountryId == Convert.ToInt32(lblcityid.Text)).First();
            CT.CountryName = txtcountryname.Text;
            CT.CountryCode = txtcountrycode.Text;
            CT.Status = Convert.ToInt32(rd_status.SelectedIndex);

            HR.SubmitChanges();
            g.ShowMessage(this.Page, "Country Details Updated Successfully");
            //modpop.Message = "Country Detail Updated Successfully";
            //modpop.ShowPopUp();

            btnsubmit.Text = "Save";
            Clear();
            bindAllCountry();
        }
        catch (Exception ex)
        {
            
            throw;
        }
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        txtcountrycode.Text = null;
        txtcountryname.Text = null;
        rd_status.SelectedIndex = 0;
        MultiView1.ActiveViewIndex = 0;
        btnsubmit.Text = "Save";
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }

    protected void grd_City_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_City.PageIndex = e.NewPageIndex;
        bindAllCountry();
        grd_City.DataBind();
    }

    protected void Unnamed1_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton Link = (ImageButton)sender;
        lblcityid.Text = Link.CommandArgument.ToString();
        CountryTB CTB = HR.CountryTBs.Where(d => d.CountryId == Convert.ToInt32(lblcityid.Text)).First();
        txtcountrycode.Text = CTB.CountryCode;
        txtcountryname.Text = CTB.CountryName;
        rd_status.SelectedIndex =Convert.ToInt32(CTB.Status);
        MultiView1.ActiveViewIndex = 1;
        btnsubmit.Text = "Update";
    }
}