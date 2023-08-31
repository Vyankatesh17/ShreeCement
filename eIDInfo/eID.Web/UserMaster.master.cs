using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class UserMaster : System.Web.UI.MasterPage
{
    HrPortalDtaClassDataContext HDC = new HrPortalDtaClassDataContext();
    protected void checkExpiry()
    {
        try
        {
            DateTime currentDate = gen.GetCurrentDateTime();

            string exDate = "";
            var exData = HDC.LockSettingsTBs.Where(d => d.TenantId == Convert.ToString(Session["TenantId"])).FirstOrDefault();
            if (exData != null)
            {
                exDate = SPPasswordHasher.Decrypt(exData.Expiry);
                DateTime expiryDate = Convert.ToDateTime(exDate);

                if (currentDate.Subtract(expiryDate).TotalDays > 0)
                {
                    gen.ShowMessageRedirect(this.Page, "License expired..", "login.aspx");
                }
            }
            else
            {
                Response.Redirect("lock_settings.aspx");
            }

        }
        catch (Exception ex) { }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {
                EmployeeTB CTB = HDC.EmployeeTBs.Where(d =>d.Email==Session["Email"].ToString()).FirstOrDefault();
                SystemUsersTB SUTB = HDC.SystemUsersTBs.Where(d => d.Email == Session["Email"].ToString()).FirstOrDefault();
                if (CTB != null && SUTB.UserRole =="User")
                {
                    //lblloginname.Text = Convert.ToString(CTB.FName + " " + CTB.Lname);
                    //lblloginname1.Text = "Welcome   " + Convert.ToString(CTB.FName + " " + CTB.Lname);
                    lblloginname.Text = Session["DisplayName"].ToString();
                    lblloginname1.Text = "Welcome   " + Session["DisplayName"].ToString();
                    // lblnote.Text = "Welcome " + Convert.ToString(CTB.FName + " " + CTB.Lname);
                    // BindMenu();
                    BindNavbarMenu();

                    string title=lblTitle.Text=lblActivePage.Text = Page.Page.Title;
                }
                else if (Session["UserType"].ToString().Equals("Admin"))
                {
                    lblloginname.Text = "Administrator";
                    lblloginname1.Text = "Welcome " + Session["TenantName"] + " Admin";
                    // lblnote.Text = "Welcome " + Convert.ToString(CTB.FName + " " + CTB.Lname);
                    // BindMenu();
                    BindNavbarMenu();

                    string title = lblTitle.Text = lblActivePage.Text = Page.Page.Title;
                }
                else if (Session["UserType"].ToString().Equals("SuperAdmin"))
                {
                    lblloginname.Text = "Super Admin";
                    lblloginname1.Text = "Welcome   Super Admin" ;
                    // lblnote.Text = "Welcome " + Convert.ToString(CTB.FName + " " + CTB.Lname);
                    // BindMenu();
                    BindNavbarMenu();

                    string title = lblTitle.Text = lblActivePage.Text = Page.Page.Title;
                }
                else if (Session["UserType"].ToString().Equals("LocationAdmin"))
                {
                    lblloginname.Text = "Location Admin";
                    lblloginname1.Text = "Welcome   Location Admin";
                    // lblnote.Text = "Welcome " + Convert.ToString(CTB.FName + " " + CTB.Lname);
                    // BindMenu();
                    BindNavbarMenu();

                    string title = lblTitle.Text = lblActivePage.Text = Page.Page.Title;


                }
                else if (Session["UserType"].ToString().Equals("Administrator"))
                {
                    lblloginname.Text = "Administrator";
                    lblloginname1.Text = "Welcome   Administrator";
                    // lblnote.Text = "Welcome " + Convert.ToString(CTB.FName + " " + CTB.Lname);
                    // BindMenu();
                    //BindNavbarMenu();

                    string title = lblTitle.Text = lblActivePage.Text = Page.Page.Title;


                }
                else
                {
                    Response.Redirect("Login.aspx");
                }
            }
            checkExpiry();
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
        
    }

    //private void BindMenu()
    //{
    //    var Data = from d in HDC.MasterMenuTBs
    //               join k in HDC.UserAssignRollTBs on d.MenuId equals k.MenuId
    //               orderby d.MenuLevel
    //               where d.ParentId == null && k.EmployeeId == Convert.ToInt32(Session["EmpId"]) && d.Status == 0
    //               select new { d.MenuId, d.MenuName, d.PageUrl };
    //    litMenu.Text = "<ul class='sidebar-menu'>";
    //    foreach (var item in Data)
    //    {

    //        if(item.PageUrl=="#")
    //        {
    //            litMenu.Text = litMenu.Text + "<li class='treeview'><a href='" + item.PageUrl + "'><i class='fa fa-bar-chart-o'></i><span>" + item.MenuName + "</span><i class='fa fa-angle-left pull-right'></i></a>   <ul class='treeview-menu'>";
    //        }
    //        else
    //        {
    //            litMenu.Text = litMenu.Text + "<li ><a href='" + item.PageUrl + "'><i class='fa fa-bar-chart-o'></i><span>" + item.MenuName + "</span><i class='fa fa-angle-left pull-right'></i></a>   <ul class='treeview-menu'>";
    //        }


    //        var Subdata = from d in HDC.MasterMenuTBs
    //                      join k in HDC.UserAssignRollTBs
    //                      on d.MenuId equals k.MenuId
    //                      where d.ParentId == item.MenuId && k.EmployeeId == Convert.ToInt32(Session["EmpId"]) && d.Status == 0
    //                      select new { d.MenuId, d.MenuName, d.PageUrl };

    //        foreach (var item1 in Subdata)
    //        {
    //            string pname = item1.PageUrl;

    //            litMenu.Text = litMenu.Text + "<li><a href='" + pname + "'><i class='fa fa-angle-double-right'></i> " + item1.MenuName + "</a></li>";
    //        }
    //        litMenu.Text = litMenu.Text + "</ul></li>";

    //    }
    //    litMenu.Text = litMenu.Text + "</ul>";
    //}




    private void BindMenu()
    {
        var Data = (from d in HDC.MasterMenuHRMSTBs
                    join k in HDC.UserAssignRollHRMSTBs on d.MenuId equals k.MenuId
                    orderby d.MenuLevel
                    where d.ParentId == null && k.EmployeeId == Convert.ToInt32(Session["EmpId"]) && d.Status == 0
                    select new { d.MenuId, d.MenuName, d.PageUrl, d.MenuLevel }).OrderBy(d => d.MenuLevel);
        litMenu.Text = "<ul class='sidebar-menu'>";
        foreach (var item in Data)
        {

            if (item.PageUrl == "#")
            {
                litMenu.Text = litMenu.Text + "<li class='treeview'><a href='" + item.PageUrl + "'><i class='fa fa-bar-chart-o'></i><span>" + item.MenuName + "</span><i class='fa fa-angle-left pull-right'></i></a>   <ul class='treeview-menu'>";
            }
            else
            {
                litMenu.Text = litMenu.Text + "<li ><a href='" + item.PageUrl + "'><i class='fa fa-bar-chart-o'></i><span>" + item.MenuName + "</span><i class='fa fa-angle-left pull-right'></i></a>   <ul class='treeview-menu'>";
            }


            var Subdata = (from d in HDC.MasterMenuHRMSTBs
                          join k in HDC.UserAssignRollHRMSTBs
                          on d.MenuId equals k.MenuId
                          orderby d.MenuLevel
                          where d.ParentId == item.MenuId && k.EmployeeId == Convert.ToInt32(Session["EmpId"]) && d.Status == 0
                          select new { d.MenuId, d.MenuName, d.PageUrl ,d.MenuLevel}).OrderBy(d=>d.MenuLevel);

            foreach (var item1 in Subdata)
            {

                if (item1.PageUrl == "#")
                {
                    litMenu.Text = litMenu.Text + "<li class='treeview'><a href='" + item1.PageUrl + "'><i class='fa fa-angle-double-right'></i><span>" + item1.MenuName + "</span><i class='fa fa-angle-left pull-right'></i></a>   <ul class='treeview-menu'>";
                }
                else
                {
                    litMenu.Text = litMenu.Text + "<li ><a href='" + item1.PageUrl + "'><i class='fa fa-angle-double-right'></i><span>" + item1.MenuName + "</span><i class='fa fa-angle-left pull-right'></i></a>   <ul class='treeview-menu'>";
                }


                var Subdata1 = from d in HDC.MasterMenuHRMSTBs
                               join k in HDC.UserAssignRollHRMSTBs
                               on d.MenuId equals k.MenuId
                               where d.ParentId == item1.MenuId && k.EmployeeId == Convert.ToInt32(Session["EmpId"]) && d.Status == 0
                               select new { d.MenuId, d.MenuName, d.PageUrl };
                if (Subdata1.Count() > 0)
                {
                    foreach (var item2 in Subdata1)
                    {

                        string pname = item2.PageUrl;

                        litMenu.Text = litMenu.Text + "<li><a href='" + pname + "'><i class='fa fa-angle-double-right'></i> " + item2.MenuName + "</a></li>";
                    }

                }
                else
                {
                    string pname = item1.PageUrl;

                    litMenu.Text = litMenu.Text + "<li><a href='" + pname + "'><i class='fa fa-angle-double-right'></i> " + item1.MenuName + "</a></li>";
                }
                litMenu.Text = litMenu.Text + "</ul></li>";
            }
            litMenu.Text = litMenu.Text + "</ul></li>";

        }
        litMenu.Text = litMenu.Text + "</ul>";
    }


    Genreal gen = new Genreal();
    private void BindNavbarMenu()
    {
        string query =string.Format(@"SELECT DISTINCT d.MenuId, d.MenuName, d.PageUrl, d.MenuLevel, d.ParentId
FROM            MasterMenuHRMSTB AS d LEFT OUTER JOIN
                         UserAssignRollHRMSTB AS u ON d.MenuId = u.MenuId
WHERE        (d.ParentId IS NULL) AND (d.Status = 0)");
        if (Session["UserType"].ToString().Equals("User"))
        {
            query +=string.Format( " AND (u.EmployeeId={0})", Convert.ToInt32(Session["EmpId"]));
        }
        query += " ORDER BY d.MenuLevel";

        DataTable dtMain = gen.ReturnData(query);
        for (int i = 0; i < dtMain.Rows.Count; i++)
        {
            string pId = dtMain.Rows[i]["MenuId"].ToString();
            string subQuery = string.Format(@"SELECT DISTINCT d.MenuId, d.MenuName, d.PageUrl, d.MenuLevel, d.ParentId
FROM            MasterMenuHRMSTB AS d LEFT JOIN
                         UserAssignRollHRMSTB AS u ON d.MenuId = u.MenuId
WHERE        (d.ParentId = {0}) AND (d.Status = 0)", pId);
            if (Session["UserType"].ToString().Equals("User"))
            {
                subQuery += string.Format(" AND (u.EmployeeId={0})", Convert.ToInt32(Session["EmpId"]));
            }

            if (!Session["UserType"].ToString().Equals("Administrator"))
            {
                subQuery += string.Format(" AND (d.MenuName != 'Company_Settings')");
            }

            if (Session["UserType"].ToString().Equals("LocationAdmin") && !Session["EmpId"].ToString().Equals("0"))
            {
                subQuery += string.Format(" AND (u.EmployeeId={0})", Convert.ToInt32(Session["EmpId"]));
            }

            subQuery += " ORDER BY d.MenuLevel";

            DataTable dtSub = gen.ReturnData(subQuery);

            string fa = dtMain.Rows[i]["MenuName"].ToString() == "Master" ? "fa fa-edit" : dtMain.Rows[i]["MenuName"].ToString() == "Analysis" ? "fa fa-bar-chart" : dtMain.Rows[i]["MenuName"].ToString() == "HR" ? "fa fa-male" : dtMain.Rows[i]["MenuName"].ToString() == "System Control" ? "fa fa-cogs" : "fa fa-circle-o";

            if (dtSub.Rows.Count > 0)
            {
                //<i class='fa fa-angle-left pull-right'></i>
                litMenu.Text = litMenu.Text + "<li class='treeview'><a href='#' style='color:#fff;'>" + dtMain.Rows[i]["MenuName"].ToString() + "<span class='pull-right-container'><i class='fa fa-angle-left pull-right'></i></span></a>   <ul class='treeview-menu bg-logo'>";
                //Literal1.Text = Literal1.Text + "<li class='treeview'><a href='#'><i class='" + fa + "'></i><span>" + dtMain.Rows[i]["Category_Name"].ToString() + "</span><span class='pull-right-container'><i class='fa fa-angle-left pull-right'></i></span></a>   <ul class='treeview-menu'>";
                for (int j = 0; j < dtSub.Rows.Count; j++)
                {
                    //litMenu.Text = litMenu.Text + "<li><a href='../" + dtSub.Rows[j]["Category_URL"].ToString() + "'>" + dtSub.Rows[j]["Category_Name"].ToString() + "</a></li>";
                    //Literal1.Text = Literal1.Text + "<li><a href='../" + dtSub.Rows[j]["Category_URL"].ToString() + "'><i class='fa fa-circle-o'></i>" + dtSub.Rows[j]["Category_Name"].ToString() + "</a></li>";
                    litMenu.Text = litMenu.Text + "<li><a href='" + dtSub.Rows[j]["PageUrl"].ToString() + "' style='color:#fff;'>" + dtSub.Rows[j]["MenuName"].ToString() + "</a></li>";
                    //Literal1.Text = Literal1.Text + "<li><a href='" + dtSub.Rows[j]["Category_URL"].ToString() + "'><i class='fa fa-circle-o'></i>" + dtSub.Rows[j]["Category_Name"].ToString() + "</a></li>";
                }
                litMenu.Text = litMenu.Text + "</ul></li>";
                //Literal1.Text = Literal1.Text + "</ul></li>";
            }
            else
            {
                if(dtMain.Rows[i]["PageUrl"].ToString()!="#")
                litMenu.Text = litMenu.Text + "<li><a href='" + dtMain.Rows[i]["PageUrl"].ToString() + "' style='color:#fff;'>" + dtMain.Rows[i]["MenuName"].ToString() + "</a></li>";
                //Literal1.Text = Literal1.Text + "<li><a href='../" + dtMain.Rows[i]["Category_URL"].ToString() + "'><i class='fa fa-circle-o'><i>" + dtMain.Rows[i]["Category_Name"].ToString() + "</a></li>";

            }
        }
    }

   
    protected void btnlogout_Click(object sender, EventArgs e)
    {
        Session["UserId"] = null;

        Response.Redirect("Login.aspx");
    }
    protected void btnProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("EmployeeDetails.aspx");
    }
}
