using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterMenu : System.Web.UI.Page
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
                fillmenu();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }

    public void BindAllSource()
    {

        var menuData1 = from dtt in HR.MasterMenuTBs
                        select new
                        {
                            //parentid = dtt.MasterMenuTB2.ParentId,
                            menuid = dtt.MenuId,
                        
                            parentname = dtt.ParentId == null ? "NO" : dtt.MasterMenuTB1.MenuName,
                            menuname = dtt.MenuName,
                            dtt.MasterMenuTB1.PageUrl,
                            dtt.MasterMenuTB1.MenuLevel,
                            Status = dtt.Status == 0 ? "Active" : "Not Active"
                        };

        var menuData = from dtt in HR.MasterMenuTBs
                       join dt in HR.MasterMenuTBs
                       on dtt.ParentId equals dt.MenuId into g
                       from k in g.DefaultIfEmpty()
                       select new
                       {
                           parentid = dtt.ParentId,
                           menuid = dtt.MenuId,
                           parentname = k.MenuName==null ? "NO":k.MenuName,
                           menuname = dtt.MenuName,
                           dtt.PageUrl,
                           dtt.MenuLevel,
                           Status = dtt.Status == 0 ? "Active" : "Not Active"
                       };
                    if (menuData.Count() > 0)
                    {
                        grd_Menu.DataSource = menuData;
                        grd_Menu.DataBind();
                        lblcnt.Text = menuData.Count().ToString();

                    }


    }
    private void fillmenu()
    {
        var data = from dt in HR.MasterMenuTBs
                   where dt.ParentId == null
                   select dt;
        if (data != null && data.Count() > 0)
        {

            ddlParentMenu.DataSource = data;
            ddlParentMenu.DataTextField = "MenuName";
            ddlParentMenu.DataValueField = "MenuId";
            ddlParentMenu.DataBind();
            ddlParentMenu.Items.Insert(0, "--Select--");
        }
    }

    protected void OnClick_Edit(object sender, EventArgs e)
    {

        //LinkButton Lnk = (LinkButton)sender;

        ImageButton Lnk = (ImageButton)sender;
        string menuId = Lnk.CommandArgument;
        lblmenuid.Text = menuId;
        MultiView1.ActiveViewIndex = 1;
        MasterMenuTB MT = HR.MasterMenuTBs.Where(d => d.MenuId == Convert.ToInt32(menuId)).First();
        txtmenuname.Text = MT.MenuName;
        if (MT.ParentId == 0)
        {

        }
        else
        {
            if (MT.ParentId != null)
            {
               ddlParentMenu.SelectedValue = MT.ParentId.ToString();
            }
        }
        txtPageUrl.Text = MT.PageUrl;

        rd_status.SelectedIndex = Convert.ToInt32(MT.Status);
        btnsubmit.Text = "Update";

    }

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        if (btnsubmit.Text == "Save")
        {
            var ExistData = from d in HR.MasterMenuTBs
                            where d.MenuName == txtmenuname.Text
                                && d.ParentId == null
                            select d;
            if (ExistData.Count() == 0)
            {
                MasterMenuTB MTB = new MasterMenuTB();
                MTB.MenuName = txtmenuname.Text;
                if (ddlParentMenu.Items.Count > 0)
                {
                    if (ddlParentMenu.SelectedIndex == 0)
                    {
                        MTB.ParentId = null;
                    }
                    else
                    {

                        MTB.ParentId = Convert.ToInt32(ddlParentMenu.SelectedValue);
                    }
                }
                MTB.PageUrl = txtPageUrl.Text;

                MTB.Status = rd_status.SelectedIndex;
                HR.MasterMenuTBs.InsertOnSubmit(MTB);
                HR.SubmitChanges();
                g.ShowMessage(this.Page, "Submitted Successfully");
                //modpop.Message = "Submitted Successfully";
                //modpop.ShowPopUp();
                Clear();
            }
            else
            {
                g.ShowMessage(this.Page, "Allready Exist Parent Menu");
                //modpop.Message = "Allready Exist Parent Menu";
                //modpop.ShowPopUp();
            }
            }
        else
        {
            MasterMenuTB MT = HR.MasterMenuTBs.Where(d => d.MenuId == Convert.ToInt32(lblmenuid.Text)).First();
            MT.MenuName = txtmenuname.Text;
            if (ddlParentMenu.SelectedIndex == 0)
            {
                MT.ParentId = null;
            }
            else
            {
                MT.ParentId = Convert.ToInt32(ddlParentMenu.SelectedValue);
            }
            MT.PageUrl = txtPageUrl.Text;
            MT.Status = rd_status.SelectedIndex;
            HR.SubmitChanges();
            g.ShowMessage(this.Page, "Updated Successfully");
            //modpop.Message = "Submitted Successfully";
            //modpop.ShowPopUp();
            btnsubmit.Text = "Save";
            Clear();
        }
    }

    protected void grd_menu_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Menu.PageIndex = e.NewPageIndex;
        BindAllSource();
    }
    public void Clear()
    {
        clearFields();

        BindAllSource();
        fillmenu();
        MultiView1.ActiveViewIndex = 0;

    }
    private void ClearInputs(ControlCollection ctrls)
    {
        foreach (Control ctrl in ctrls)
        {
            if (ctrl is TextBox)
                ((TextBox)ctrl).Text = string.Empty;
            else if (ctrl is DropDownList)
                ((DropDownList)ctrl).ClearSelection();

            ClearInputs(ctrl.Controls);
        }
    }

    private void clearFields()
    {
        ClearInputs(Page.Controls);

        MultiView1.ActiveViewIndex = 0;
    }




    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;

    }
    protected void ddlParentMenu_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlParentMenu.SelectedIndex == 0)
        {
            txtPageUrl.Text = "#";
            txtPageUrl.Enabled = false;
        }
        else
        {
            txtPageUrl.Text = null;
            txtPageUrl.Enabled = true;
        }

    }

}