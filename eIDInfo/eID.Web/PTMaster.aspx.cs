using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class PTMaster : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                BindLocation();
                BindAllData();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    private void BindLocation()
    {
        //var locationdata = from dt in HR.StateTBs
        //                   where dt.Status==0
        //                   select new { dt.StateId, dt.StateName };
        //DropDownList1.DataSource = locationdata;
        //DropDownList1.DataTextField = "StateName";
        //DropDownList1.DataValueField = "StateId";
        //DropDownList1.DataBind();
        //DropDownList1.Items.Insert(0, "--Select--");
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {

       


    }

    protected void imgview_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton ImgView = (ImageButton)sender;
        lblexpenseid.Text = ImgView.CommandArgument;
        PTMasterTB PTB = HR.PTMasterTBs.Where(d => d.slabid == Convert.ToInt32(lblexpenseid.Text)).First();
        DropDownList1.SelectedValue = PTB.Location.ToString();
        txtcharge.Text = PTB.Charge.ToString();
        txtsalbfrom.Text = PTB.SlabFrom.ToString();
        txtslabTo.Text = PTB.SlabTo.ToString();
        txtjan.Text = PTB.Jan.ToString();
        txtfeb.Text = PTB.Feb.ToString();
        txtmar.Text = PTB.Mar.ToString();
        txtapr.Text = PTB.Apr.ToString();
        txtmay.Text = PTB.May.ToString();
        txtjun.Text = PTB.Jun.ToString();
        txtjul.Text = PTB.Jul.ToString();
        txtaug.Text = PTB.Aug.ToString();
        txtsep.Text = PTB.Sep.ToString();
        txtoct.Text = PTB.Oct.ToString();
        txtnov.Text = PTB.Nov.ToString();
        txtdec.Text = PTB.Dec.ToString();
        btnsubmit.Text = "Update";
        MultiView1.ActiveViewIndex = 1;

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (btnsubmit.Text == "Save")
        {
            var CurrentSlot = (from m in HR.PTMasterTBs
                               where m.Location == Convert.ToInt32(DropDownList1.SelectedValue) && m.SlabFrom == decimal.Parse(txtsalbfrom.Text)
                               && m.SlabTo == decimal.Parse(txtslabTo.Text)
                               select m).ToList();

            if (CurrentSlot.Count() == 0)
            {
                PTMasterTB PTB = new PTMasterTB();
                PTB.Location = Convert.ToInt32(DropDownList1.SelectedValue);
                PTB.SlabFrom = decimal.Parse(txtsalbfrom.Text);
                PTB.SlabTo = decimal.Parse(txtslabTo.Text);
                PTB.Charge = decimal.Parse(txtcharge.Text);

                PTB.Jan = double.Parse(txtjan.Text);
                PTB.Feb = double.Parse(txtfeb.Text);
                PTB.Mar = double.Parse(txtmar.Text);
                PTB.Apr = double.Parse(txtapr.Text);
                PTB.May = double.Parse(txtmay.Text);
                PTB.Jun = double.Parse(txtjun.Text);
                PTB.Jul = double.Parse(txtjul.Text);
                PTB.Aug = double.Parse(txtaug.Text);
                PTB.Sep = double.Parse(txtsep.Text);
                PTB.Oct = double.Parse(txtoct.Text);
                PTB.Nov = double.Parse(txtnov.Text);
                PTB.Dec = double.Parse(txtdec.Text);
                HR.PTMasterTBs.InsertOnSubmit(PTB);
                HR.SubmitChanges();


                BindAllData();

                g.ShowMessage(this.Page, "PT Details Saved successfully");
            }
            else
            {

                g.ShowMessage(this.Page, "PT Details Already Exists For This Slab");
            }
            Clear();
        }
        else
        {
             var CurrentSlot = (from m in HR.PTMasterTBs
                               where m.Location == Convert.ToInt32(DropDownList1.SelectedValue) && m.SlabFrom == decimal.Parse(txtsalbfrom.Text)
                               && m.SlabTo == decimal.Parse(txtslabTo.Text) && m.slabid == Convert.ToInt32(lblexpenseid.Text)
                               select m).ToList();

             if (CurrentSlot.Count() > 0)
             {
              updatecode();
             }
             else
             {
                 var CurrentSlot1 = (from m in HR.PTMasterTBs
                                    where m.Location == Convert.ToInt32(DropDownList1.SelectedValue) && m.SlabFrom == decimal.Parse(txtsalbfrom.Text)
                                    && m.SlabTo == decimal.Parse(txtslabTo.Text) && m.slabid != Convert.ToInt32(lblexpenseid.Text)
                                    select m).ToList();

                 if (CurrentSlot1.Count() > 0)
                 {
                     g.ShowMessage(this.Page, "PT Details Already Exists For This Slab");
                 }
                 else
                 {
                     updatecode();
                 }
             }

             Clear();
        }
       
    }

    private void updatecode()
    {
        try
        {
            PTMasterTB PTB = HR.PTMasterTBs.Where(d => d.slabid == Convert.ToInt32(lblexpenseid.Text)).First();
            PTB.Location = Convert.ToInt32(DropDownList1.SelectedValue);
            PTB.SlabFrom = decimal.Parse(txtsalbfrom.Text);
            PTB.SlabTo = decimal.Parse(txtslabTo.Text);
            PTB.Charge = decimal.Parse(txtcharge.Text);

            PTB.Jan = double.Parse(txtjan.Text);
            PTB.Feb = double.Parse(txtfeb.Text);
            PTB.Mar = double.Parse(txtmar.Text);
            PTB.Apr = double.Parse(txtapr.Text);
            PTB.May = double.Parse(txtmay.Text);
            PTB.Jun = double.Parse(txtjun.Text);
            PTB.Jul = double.Parse(txtjul.Text);
            PTB.Aug = double.Parse(txtaug.Text);
            PTB.Sep = double.Parse(txtsep.Text);
            PTB.Oct = double.Parse(txtoct.Text);
            PTB.Nov = double.Parse(txtnov.Text);
            PTB.Dec = double.Parse(txtdec.Text);

            HR.SubmitChanges();
            BindAllData();
            g.ShowMessage(this.Page, "PT Details Updated successfully");
            Clear();
        }
        catch (Exception ex)
        {
            
             g.ShowMessage(this.Page, ex.Message);
        }
    }

    private void BindAllData()
    {
        var alldata = (from dt in HR.PTMasterTBs
                      select new
                      {
                          dt.slabid,
                         // dt.StateTB.StateName,
                          dt.SlabFrom,
                          dt.SlabTo,
                          dt.Charge,
                          dt.Jan,
                          dt.Feb,
                          dt.Mar,
                          dt.Apr,
                          dt.May,
                          dt.Jun,
                          dt.Jul,
                          dt.Aug,
                          dt.Sep,
                          dt.Oct,
                          dt.Nov,
                          dt.Dec
                      }).OrderByDescending(d=>d.slabid);


        if (alldata.Count() > 0)
        {
            grd_Expense.DataSource = alldata;
            grd_Expense.DataBind();
            lblcnt.Text = alldata.Count().ToString();
        }
        else
        {
            grd_Expense.DataSource = null;
            grd_Expense.DataBind();
            lblcnt.Text = "0";
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }

    private void Clear()
    {
        DropDownList1.SelectedIndex = 0;
        txtcharge.Text = null;
        txtsalbfrom.Text = null;
        txtslabTo.Text = null;
        txtjan.Text = null;
        txtfeb.Text = null;
        txtmar.Text = null;
        txtapr.Text = null;
        txtmay.Text = null;
        txtjun.Text = null;
        txtjul.Text = null;
        txtaug.Text = null;
        txtsep.Text = null;
        txtoct.Text = null;
        txtnov.Text = null;
        txtdec.Text = null;
        btncheck.Text = "Same To All";
       
        MultiView1.ActiveViewIndex = 0;
        btnsubmit.Text = "Save";

    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
    }
    protected void grd_Expense_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Expense.PageIndex = e.NewPageIndex;
        BindAllData();
        grd_Expense.DataBind();
    }
   
    protected void btncheck_Click(object sender, EventArgs e)
    {
        txtjan.Text = txtcharge.Text;
        txtfeb.Text = txtcharge.Text;
        txtmar.Text = txtcharge.Text;
        txtapr.Text = txtcharge.Text;
        txtmay.Text = txtcharge.Text;
        txtjun.Text = txtcharge.Text;
        txtjul.Text = txtcharge.Text;
        txtaug.Text = txtcharge.Text;
        txtsep.Text = txtcharge.Text;
        txtoct.Text = txtcharge.Text;
        txtnov.Text = txtcharge.Text;
        txtdec.Text = txtcharge.Text;
    }
}