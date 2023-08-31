using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AddSalaryComponent : System.Web.UI.Page
{
    HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext();

    Genreal g = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            MultiView1.ActiveViewIndex = 0;
            BindAllcomponent();          
        }
    }

    public void BindAllcomponent()
    {

        var cptData = (from d in db.PFDynamicSettingTBs
                       select d).ToList();
        if (cptData.Count() > 0)
        {
            grd_Dept.DataSource = cptData;
            grd_Dept.DataBind();
            lblcnt.Text = cptData.Count().ToString();
        }


    }



    protected void OnClick_Edit(object sender, EventArgs e)
    {      
        ImageButton Lnk = (ImageButton)sender;
        string cptId = Lnk.CommandArgument;
        lbldeptid.Text = cptId;

        MultiView1.ActiveViewIndex = 1;
        PFDynamicSettingTB MT = db.PFDynamicSettingTBs.Where(d => d.PFid == Convert.ToInt32(cptId)).First();

        txtstartlimit.Text = MT.StartLimit.ToString();
        txtendlimit.Text = MT.EndLimit.ToString();

        if (MT.FixedValue == null || MT.FixedValue == 0)
        {
            txtvalue.Text = MT.PercentageValue.ToString();

            rb_Value_Type.SelectedValue = "1";         
        }
        else
        {
            rb_Value_Type.SelectedValue = "0";
            txtvalue.Text = Convert.ToString(MT.FixedValue);         
        }       

        btnsubmit.Text = "Update";

    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (btnsubmit.Text == "Save")
        {
            var data = from d in db.PFDynamicSettingTBs
                       where d.StartLimit <= Convert.ToDecimal(txtstartlimit.Text) && d.EndLimit >= Convert.ToDecimal(txtendlimit.Text) && d.ValueType != null
                       select d;
            if (data.Count() > 0)
            {
                modpop.Message = "P.F. Details Already Exists.!!";
                modpop.ShowPopUp();
            }
            else
            {
                PFDynamicSettingTB MTB = new PFDynamicSettingTB();
                MTB.StartLimit = Convert.ToDecimal(txtstartlimit.Text);
                MTB.EndLimit = Convert.ToDecimal(txtendlimit.Text);


                MTB.ValueType = rb_Value_Type.SelectedItem.Text;
                if (rb_Value_Type.SelectedIndex == 0)
                {
                    MTB.FixedValue = Convert.ToDecimal(txtvalue.Text);
                    MTB.PercentageValue = 0;
                }
                if (rb_Value_Type.SelectedIndex == 1)
                {
                    MTB.FixedValue = 0;
                    MTB.PercentageValue = Convert.ToDecimal(txtvalue.Text);
                }

                MTB.Status = 0;
                db.PFDynamicSettingTBs.InsertOnSubmit(MTB);
                db.SubmitChanges();
                modpop.Message = "Submitted Successfully";
                modpop.ShowPopUp();
                Clear();
            }
        }
        else
        {
             var data = from d in db.PFDynamicSettingTBs
                        where d.StartLimit <= Convert.ToDecimal(txtstartlimit.Text) && d.EndLimit >= Convert.ToDecimal(txtendlimit.Text) && d.PFid != Convert.ToInt32(lbldeptid.Text) && d.ValueType != null
                       select d;
             if (data.Count() > 0)
             {
                 modpop.Message = "P.F. Details Already Exists.!!";
                 modpop.ShowPopUp();
             }
             else
             {
                 PFDynamicSettingTB MT = db.PFDynamicSettingTBs.Where(d => d.PFid == Convert.ToInt32(lbldeptid.Text)).First();
                 MT.StartLimit = Convert.ToDecimal(txtstartlimit.Text);
                 MT.EndLimit = Convert.ToDecimal(txtendlimit.Text);

                 MT.ValueType = rb_Value_Type.SelectedItem.Text;
                 if (rb_Value_Type.SelectedIndex == 0)
                 {
                     MT.FixedValue = Convert.ToDecimal(txtvalue.Text);
                     MT.PercentageValue = 0;
                 }
                 if (rb_Value_Type.SelectedIndex == 1)
                 {
                     MT.FixedValue = 0;
                     MT.PercentageValue = Convert.ToDecimal(txtvalue.Text);
                 }
                 MT.Status = 0;
                 db.SubmitChanges();

                 modpop.Message = "Updated Successfully";
                 modpop.ShowPopUp();

                 Clear();
             }
        }
    }

    public void Clear()
    {
        txtstartlimit.Text = "0";
        txtendlimit.Text = "";
        lbldeptid.Text = "";
        txtvalue.Text = null;
        BindAllcomponent();    
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
        BindAllcomponent();
    }

    protected void txtdeptname_TextChanged(object sender, EventArgs e)
    {

    }
    protected void rb_Value_Type_SelectedIndexChanged(object sender, EventArgs e)
    {       
    }
}