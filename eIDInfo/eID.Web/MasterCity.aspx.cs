using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterCity : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();

    Genreal g = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                BindCountry();
                BindAllSource();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void BindCountry()
    {
        var datac = (from dt in HR.CountryTBs
                    where dt.Status == 0
                    select dt).OrderBy(d => d.CountryName);
        if (datac != null && datac.Count() > 0)
        {

            ddlCountry.DataSource = datac;
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataValueField = "CountryId";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, "--Select--");
        }
       
    }

    private void fillState(int countryid)
    {
        var data = (from dt in HR.StateTBs
                   where dt.CountryId == countryid
                   select dt).OrderBy(d=>d.StateName);
        if (data != null && data.Count() > 0)
        {
            ddlstate.DataSource = data;
            ddlstate.DataTextField = "StateName";
            ddlstate.DataValueField = "StateId";
            ddlstate.DataBind();
            ddlstate.Items.Insert(0, "--Select--");
        }
       
    }

    public void Clear()
    {
        txtcityname.Text = null;
        lblcityid.Text = "";
        ddlCountry.SelectedIndex = 0;
        ddlstate.Items.Clear();

        rd_status.SelectedIndex = 0;
        BindAllSource();
        MultiView1.ActiveViewIndex = 0;
        btnsubmit.Text = "Save";

    }

    public void BindAllSource()
    {

        var CityData = (from s in HR.CityTBs
                        join d in HR.StateTBs on s.StateId equals d.StateId
                        select new { s.CityId, s.CountryTB.CountryName, s.CountryID, StateName = d.StateName, s.CityName, Status = s.Status == 0 ? "Active" : "In Active" }).OrderBy(d => d.CountryName).ThenBy(d=>d.StateName).ThenBy(d => d.CityName);

        grd_City.DataSource = CityData;
        grd_City.DataBind();
        lblcnt.Text = CityData.Count().ToString();


    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        if (btnsubmit.Text == "Save")
        {
            var dte = from p in HR.CityTBs.Where(d => d.CityName == txtcityname.Text&&d.StateId==Convert.ToInt32(ddlstate.SelectedValue)) select p;
             if (dte.Count() > 0)
             {
                 g.ShowMessage(this.Page, "City Details Already Exist");
             }
             else
             {
                 CityTB mtb = new CityTB();
                 mtb.CityName = txtcityname.Text;
                 mtb.CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
                 mtb.StateId = Convert.ToInt32(ddlstate.SelectedValue);
                 mtb.Status = rd_status.SelectedIndex;
                 HR.CityTBs.InsertOnSubmit(mtb);
                 HR.SubmitChanges();
                 g.ShowMessage(this.Page, "City Details Saved Successfully");
                 //modpop.Message = "Data Added Successfully";
                 //modpop.ShowPopUp();
                Clear();
             }
        }
        else
        {   // Code written By Abdul Rahman date: 03/12/2014
            var dt = from p in HR.CityTBs.Where(d => d.CityName == txtcityname.Text && d.CityId == Convert.ToInt32(lblcityid.Text) && d.StateId == Convert.ToInt32(ddlstate.SelectedValue)) select p;
            if (dt.Count() > 0)
            {
                updatecode();

            }
            else
            {
                var dt1 = from p in HR.CityTBs.Where(d => d.CityName == txtcityname.Text && d.CityId != Convert.ToInt32(lblcityid.Text) && d.StateId == Convert.ToInt32(ddlstate.SelectedValue)) select p;
                if (dt1.Count() > 0)
                {

                    g.ShowMessage(this.Page, "City Details Already Exist");
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
            // Code written By Abdul Rahman date: 03/12/2014
            CityTB mt = HR.CityTBs.Where(s => s.CityId == Convert.ToInt32(lblcityid.Text)).First();
            mt.CityName = txtcityname.Text;
            mt.CountryID = Convert.ToInt32(ddlCountry.SelectedValue);
        mt.StateId = Convert.ToInt32(ddlstate.SelectedValue);
            mt.Status = rd_status.SelectedIndex;
            HR.SubmitChanges();

            g.ShowMessage(this.Page, "City Details Updated Successfully");
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

    protected void Unnamed1_Click(object sender, ImageClickEventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        ImageButton Lnk = (ImageButton)sender;
        string CityID = Lnk.CommandArgument;
        lblcityid.Text = CityID;

        //int deptid = lbldeptid.Text;
        CityTB mt = HR.CityTBs.Where(s => s.CityId == Convert.ToInt32(CityID)).First();
        txtcityname.Text = mt.CityName;
        ddlCountry.SelectedValue = Convert.ToString(mt.CountryID);
        fillState(Convert.ToInt32(ddlCountry.SelectedValue));
        ddlstate.SelectedValue = mt.StateId.ToString();

        rd_status.SelectedIndex = Convert.ToInt32(mt.Status);
        btnsubmit.Text = "Update";
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlCountry.SelectedIndex > 0)
        {
            fillState(Convert.ToInt32(ddlCountry.SelectedValue));
        }
        else
        {
            ddlstate.Items.Clear();
            
        }
    }

    protected void grd_City_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_City.PageIndex = e.NewPageIndex;
        BindCountry();
        BindAllSource();

    }
}