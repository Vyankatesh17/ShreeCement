using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class ShiftGroupMaster : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    DataTable Dt = new DataTable();
    DataTable DtShiftlist = new DataTable();
    private void BindJqFunctions()
    {
        //jqFunctions
        ScriptManager.RegisterClientScriptBlock(this.Page, Page.GetType(), "jqFunctions", "javascript:jqFunctions();", true);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;
                fillCompany();

                BindAllShiftGroupData();               
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
        if (grd_Empshift.Rows.Count > 0)
            grd_Empshift.HeaderRow.TableSection = TableRowSection.TableHeader;
        BindJqFunctions();
    }

    private void BindAllShiftGroupData()
    {
        string query = "";
        int companyid = Convert.ToInt32(Session["CompanyID"]);
        if (Session["UserType"].ToString() == "LocationAdmin")
        {
             query = string.Format(@"SELECT ST.ShiftGroupId,ST.ShiftGroupName, ST.Remark,ST.CompanyId, CT.CompanyName FROM ShiftGroupTB AS ST INNER JOIN CompanyInfoTB AS CT ON CT.CompanyId = ST.CompanyId
            WHERE        (CT.TenantId = '{0}'And CT.CompanyId = '{1}')", Convert.ToString(Session["TenantId"]), companyid);
        }
        else
        {
             query = string.Format(@"SELECT ST.ShiftGroupId,ST.ShiftGroupName, ST.Remark,ST.CompanyId, CT.CompanyName FROM ShiftGroupTB AS ST INNER JOIN CompanyInfoTB AS CT ON CT.CompanyId = ST.CompanyId
            WHERE        (CT.TenantId = '{0}')", Convert.ToString(Session["TenantId"]));
        }
           

        DataTable dataTable = g.ReturnData(query);

        grd_Empshift.DataSource = dataTable;
        grd_Empshift.DataBind();
    }

    private void fillCompany()
    {
        ddlCompany.Items.Clear();
        List<CompanyInfoTB> data = SPCommon.DDLCompanyBind(Session["UserType"].ToString(), Session["TenantId"].ToString(), Session["CompanyID"].ToString());
        if (data != null && data.Count() > 0)
        {
            ddlCompany.DataSource = data;
            ddlCompany.DataTextField = "CompanyName";
            ddlCompany.DataValueField = "CompanyId";
            ddlCompany.DataBind();
        }
        ddlCompany.Items.Insert(0, new ListItem("--Select--", "0"));
    }

   
    protected void btnadd_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 1;
        
    }

    protected void btnAddShiftList_Click(object sender, EventArgs e)
    {
        int cnt = 0;
        if (ViewState["DtShiftlist"] == null)
        {
            DataColumn CompanyId = DtShiftlist.Columns.Add("CompanyId");
            DataColumn CompanyName = DtShiftlist.Columns.Add("CompanyName");
            DataColumn ShiftGroupName = DtShiftlist.Columns.Add("ShiftGroupName");
            DataColumn ShiftId = DtShiftlist.Columns.Add("ShiftId");
            DataColumn Shift = DtShiftlist.Columns.Add("Shift");
            DataColumn Remark = DtShiftlist.Columns.Add("Remark");            
        }
        else
        {
            DtShiftlist = (DataTable)ViewState["DtShiftlist"];
        }
        DataRow dr = DtShiftlist.NewRow();
        dr[0] = ddlCompany.SelectedValue;
        dr[1] = ddlCompany.SelectedItem;
        dr[2] = txtShiftGroup.Text; ;
        dr[3] = ddlShift.SelectedValue;
        dr[4] = ddlShift.SelectedItem;
        dr[5] = txtRemark.Text;

        if (DtShiftlist.Rows.Count > 0)
        {
            for (int f = 0; f < DtShiftlist.Rows.Count; f++)
            {
                string u2 = DtShiftlist.Rows[f][3].ToString();
                if (u2 == ddlShift.SelectedValue)
                {
                    cnt++;
                }
            }
            if (cnt > 0)
            {
                g.ShowMessage(this.Page, "Already Exist");
            }
            else
            {
                DtShiftlist.Rows.Add(dr);
                clearExpirience();
            }
        }
        else
        {
            DtShiftlist.Rows.Add(dr);
            clearExpirience();
        }
        ViewState["DtShiftlist"] = DtShiftlist;

        grdShiftList.DataSource = DtShiftlist;
        grdShiftList.DataBind();

    }

    private void clearExpirience()
    {        
        ddlShift.SelectedIndex = 0;
    }

    protected void btnCancelShiftlist_Click(object sender, EventArgs e)
    {
        ddlCompany.SelectedIndex = 0;
        txtShiftGroup.Text = null;
        ddlShift.SelectedIndex = 0;
        txtRemark.Text = null;
    }

    protected void grd_Empshift_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (grd_Empshift.Rows.Count > 0)
        {
            grd_Empshift.UseAccessibleHeader = true;            
            grd_Empshift.HeaderRow.TableSection = TableRowSection.TableHeader;
        }
    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }

    

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        DataTable dttt1 = (DataTable)ViewState["DtShiftlist"];
        if (dttt1 != null)
        {
            var shiftdata = HR.ShiftGroupTBs.Where(a => a.ShiftGroupName == txtShiftGroup.Text).FirstOrDefault();
            if (btnsubmit.Text == "Save")
            {
                ShiftGroupTB shiftgroup = new ShiftGroupTB();
                shiftgroup.ShiftGroupName = txtShiftGroup.Text;
                shiftgroup.CompanyId = Convert.ToInt32(ddlCompany.SelectedValue);
                shiftgroup.Remark = txtRemark.Text;

                HR.ShiftGroupTBs.InsertOnSubmit(shiftgroup);
                HR.SubmitChanges();

                for (int i = 0; i < dttt1.Rows.Count; i++)
                {
                    ShiftGroupDetailsTB shiftgroupdetails = new ShiftGroupDetailsTB();
                    shiftgroupdetails.ShiftGroupId = shiftgroup.ShiftGroupId;
                    shiftgroupdetails.ShiftId = Convert.ToInt32(dttt1.Rows[i]["ShiftId"].ToString());

                    HR.ShiftGroupDetailsTBs.InsertOnSubmit(shiftgroupdetails);
                    HR.SubmitChanges();
                }
                g.ShowMessage(this.Page, "Shift Group Added Successfully.");
            }
            else
            {
                for (int i = 0; i < dttt1.Rows.Count; i++)
                {
                    var shiftdetailsdata = HR.ShiftGroupDetailsTBs.Where(a => a.ShiftGroupId == shiftdata.ShiftGroupId && a.ShiftId == Convert.ToInt32(dttt1.Rows[i]["ShiftId"].ToString())).FirstOrDefault();

                    if(shiftdetailsdata == null)
                    {
                        ShiftGroupDetailsTB shiftgroupdetails = new ShiftGroupDetailsTB();
                        shiftgroupdetails.ShiftGroupId = shiftdata.ShiftGroupId;
                        shiftgroupdetails.ShiftId = Convert.ToInt32(dttt1.Rows[i]["ShiftId"].ToString());

                        HR.ShiftGroupDetailsTBs.InsertOnSubmit(shiftgroupdetails);
                        HR.SubmitChanges();
                    }                   
                }
                g.ShowMessage(this.Page, "Shift Group Updated Successfully.");
            }
        }
    }

    protected void lbtnEditShift_Click(object sender, EventArgs e)
    {
        LinkButton Lnk = (LinkButton)sender;
        string Shift = Lnk.CommandArgument;
        int Shiftid = Convert.ToInt32(Shift);

        ddlShift.SelectedValue = Shift;
        Dt = (DataTable)ViewState["DtShiftlist"];

        DataRow[] drr = Dt.Select("ShiftId=" + Shiftid + "");
        for (int i = 0; i < drr.Length; i++)
            drr[i].Delete();
        Dt.AcceptChanges();

        ViewState["DtShiftlist"] = Dt;
        grdShiftList.DataSource = Dt;
        grdShiftList.DataBind();
    }

    protected void lbtnDeleteShift_Click(object sender, EventArgs e)
    {
        LinkButton Lnk = (LinkButton)sender;
        string Shift = Lnk.CommandArgument;

        int Shiftid = Convert.ToInt32(Shift);
        var shiftgroupdata = HR.ShiftGroupTBs.Where(a => a.ShiftGroupName == txtShiftGroup.Text).FirstOrDefault();
        ShiftGroupDetailsTB shiftgroupdetails = HR.ShiftGroupDetailsTBs.Where(a => a.ShiftId == Shiftid && a.ShiftGroupId == shiftgroupdata.ShiftGroupId).FirstOrDefault();
        HR.ShiftGroupDetailsTBs.DeleteOnSubmit(shiftgroupdetails);
        HR.SubmitChanges();
        ddlShift.SelectedValue = Shift;
        Dt = (DataTable)ViewState["DtShiftlist"];

        DataRow[] drr = Dt.Select("ShiftId=" + Shiftid + "");
        for (int i = 0; i < drr.Length; i++)
            drr[i].Delete();
        Dt.AcceptChanges();

        ViewState["DtShiftlist"] = Dt;
        grdShiftList.DataSource = Dt;
        grdShiftList.DataBind();

    }


    protected void lbtnEdit_Click(object sender, EventArgs e)
    {
        LinkButton Lnk = (LinkButton)sender;
        string SGId = Lnk.CommandArgument;
        int ShiftGroupid = Convert.ToInt32(SGId);
        ShiftGroupTB SG = HR.ShiftGroupTBs.Where(a => a.ShiftGroupId == ShiftGroupid).FirstOrDefault();
        ddlCompany.SelectedValue = SG.CompanyId.ToString();
        txtShiftGroup.Text = SG.ShiftGroupName;
        txtRemark.Text = SG.Remark;

        int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
        var Shiftdata = HR.MasterShiftTBs.Where(d =>  d.CompanyId == cId && d.TenantId == Convert.ToString(Session["TenantId"])).ToList();
        if (Shiftdata != null && Shiftdata.Count > 0)
        {
            ddlShift.DataSource = Shiftdata;
            ddlShift.DataTextField = "Shift";
            ddlShift.DataValueField = "ShiftID";
            ddlShift.DataBind();
        }
        ddlShift.Items.Insert(0, new ListItem("--Select--", "0"));


        var shiftlist = HR.ShiftGroupDetailsTBs.Where(a => a.ShiftGroupId == SG.ShiftGroupId).ToList();

        foreach (var item in shiftlist)
        {
            if (ViewState["DtShiftlist"] == null)
            {
                Dt = new DataTable();
                DataColumn CompanyId = Dt.Columns.Add("CompanyId");
                DataColumn CompanyName = Dt.Columns.Add("CompanyName");
                DataColumn ShiftGroupName = Dt.Columns.Add("ShiftGroupName");
                DataColumn ShiftId = Dt.Columns.Add("ShiftId");
                DataColumn Shift = Dt.Columns.Add("Shift");
                DataColumn Remark = Dt.Columns.Add("Remark");
            }
            else
            {
                Dt = (DataTable)ViewState["DtShiftlist"];
            }
            DataTable ds1 = g.ReturnData("select  SG.CompanyId,CI.CompanyName, SG.ShiftGroupName,SGD.ShiftId,MS.Shift, SG.Remark from ShiftGroupTB As SG inner join CompanyInfoTB As CI ON CI.CompanyId = SG.CompanyId inner join ShiftGroupDetailsTB As SGD ON SGD.ShiftGroupId = SG.ShiftGroupId inner join MasterShiftTB As MS ON MS.ShiftID = SGD.ShiftId where SG.ShiftGroupId =" + ShiftGroupid+"");
            

            foreach (DataRow d in ds1.Rows)
            {
                DataRow dr = Dt.NewRow();
                dr[0] = d["CompanyId"].ToString();
                dr[1] = d["CompanyName"].ToString();
                dr[2] = d["ShiftGroupName"].ToString();
                dr[3] = d["ShiftId"].ToString();
                dr[4] = d["Shift"].ToString();
                dr[5] = d["Remark"].ToString();

                Dt.Rows.Add(dr);
            }
            
        }

        ViewState["DtShiftlist"] = Dt;
        grdShiftList.DataSource = Dt;
        grdShiftList.DataBind();

        btnsubmit.Text = "Update";
        MultiView1.ActiveViewIndex = 1;

    }



        protected void btncancel1_Click(object sender, EventArgs e)
    {
        Clear();
    }



    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {

        int cId = ddlCompany.SelectedIndex > 0 ? Convert.ToInt32(ddlCompany.SelectedValue) : 0;
        var Shiftdata = HR.MasterShiftTBs.Where(d => d.CompanyId == cId && d.TenantId == Convert.ToString(Session["TenantId"])).ToList();
        if (Shiftdata != null && Shiftdata.Count > 0)
        {
            ddlShift.DataSource = Shiftdata;
            ddlShift.DataTextField = "Shift";
            ddlShift.DataValueField = "ShiftID";
            ddlShift.DataBind();
        }
        ddlShift.Items.Insert(0, new ListItem("--Select--", "0"));
    }


    private void Clear()
    {
        ddlCompany.SelectedIndex = 0;
        ddlShift.SelectedIndex = 0;
        txtRemark = null;
        txtShiftGroup = null;
        BindAllShiftGroupData();
        btnsubmit.Text = "Save";
        MultiView1.ActiveViewIndex = 0;
    }











}