using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class AssignRoll : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["UserId"] = "12";
        if (Session["UserId"] != null)
        {

            if (!IsPostBack)
            {
                BindCheckToMenu();
                fillcompany();
                BindAllEmployee();
            }
        }
        else
        {
            Response.Redirect("Login.aspx");
        }
    }
    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindAllEmployee();
    }
    private void fillcompany()
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
    public void BindCheckToMenu()
    {
        var data = from d in HR.MasterMenuHRMSTBs
                   where d.ParentId == null && d.Status == 0
                   select new
                   {
                       d.MenuId,
                       d.MenuName
                   };
        foreach (var item in data)
        {
            TreeNode tn = new TreeNode();
            tn.Value = item.MenuId.ToString();
            tn.Text = item.MenuName;

            TreeNode td = new TreeNode();
            var ChildData = from d in HR.MasterMenuHRMSTBs
                            where d.ParentId == item.MenuId && d.Status == 0
                            select new
                            {
                                d.MenuId,
                                d.MenuName
                            };
            foreach (var itemData in ChildData)
            {
                TreeNode ts = new TreeNode();
                ts.Value = itemData.MenuId.ToString();
                ts.Text = itemData.MenuName;
                tn.ChildNodes.Add(ts);
                // for subchild
                var subchildata = from dt in HR.MasterMenuHRMSTBs
                                  where dt.ParentId == itemData.MenuId && dt.Status == 0
                                  select new { dt.MenuId, dt.MenuName };
                if (subchildata.Count() > 0)
                {
                    foreach (var itemer in subchildata)
                    {
                        TreeNode tsw = new TreeNode();
                        tsw.Value = itemer.MenuId.ToString();
                        tsw.Text = itemer.MenuName;
                        ts.ChildNodes.Add(tsw);
                    }
                }
            }
            TreeView1.Nodes.Add(tn);
        }
    }

    public void BindAllEmployee()
    {
        ddemployee.Items.Clear();
        var data = (from d in HR.EmployeeTBs
                            where d.IsActive==true&& (d.RelivingStatus == 0 || d.RelivingStatus == null) && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)
                            && d.TenantId==Convert.ToString(Session["TenantId"])
                            select new
                            {
                                d.EmployeeId,
                                EmpName = d.FName + ' ' + d.Lname.Replace('~', ' ')
                            }).Distinct();
        if (data != null && data.Count() > 0)
        {
            ddemployee.DataSource = data;
            ddemployee.DataTextField = "EmpName";
            ddemployee.DataValueField = "EmployeeId";
            ddemployee.DataBind();
        }
        ddemployee.Items.Insert(0, new ListItem("--Select--", "0"));
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        var ExistData = from d in HR.UserAssignRollHRMSTBs where d.EmployeeId == Convert.ToInt32(ddemployee.SelectedValue) select d;
        if (ExistData.Count() > 0)
        {
            HR.UserAssignRollHRMSTBs.DeleteAllOnSubmit(ExistData);
            HR.SubmitChanges();
        }
        TraverseTreeView(TreeView1);
        g.ShowMessage(this.Page, "Role Assign Successfully");

        //modpop.Message = "Roll Assign Successfully";
        //modpop.ShowPopUp();

        Clear();
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Clear();
    }
    protected void ddemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        int empId = ddemployee.SelectedIndex > 0 ? Convert.ToInt32(ddemployee.SelectedValue) : 0;

        var data = (from d in HR.EmployeeTBs
                    join u in HR.SystemUsersTBs on d.Email equals u.Email
                    where d.IsActive==true&& (d.RelivingStatus==0||d.RelivingStatus==null)&&d.IsActive==true&& d.EmployeeId == empId && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)
                    select new { d.EmployeeId, d.MachineID, d.EmployeeNo, u.UserRole, u.Email, u.Password }).FirstOrDefault();
        if (data != null)
        {
            lblstatus.Text = "Active";
            Label1.Text = data.UserRole;
            HdSource0.Value = data.EmployeeId.ToString();
            UserInformation.Visible = true;
            refreshtreeview(TreeView1);
            TraverseTreeViewch(TreeView1);
            btnsubmit.Text = "Update";
        }
        
    }

    private void TraverseTreeView(TreeView tview)
    {

        TreeNode temp = new TreeNode();

        string[] a = new string[200];
        int j = 0;
        for (int k = 0; k < tview.Nodes.Count; k++)
        {
            temp = tview.Nodes[k];
            if (temp.Checked)
            {
                string str = temp.Text;
                string val = temp.Value;
                a[j] = val;
                j++;

                for (int i = 0; i < temp.ChildNodes.Count; i++)
                {
                    if (temp.ChildNodes[i].Checked)
                    {
                        string child = temp.ChildNodes[i].Text;
                        string childval = temp.ChildNodes[i].Value;
                        a[j] = childval;
                        j++;
                    }

                    // Code for subchild
                    TreeNode temp12 = new TreeNode();
                    temp12 = temp.ChildNodes[i];
                    for (int b = 0; b < temp12.ChildNodes.Count; b++)
                    {
                        if (temp12.ChildNodes[b].Checked)
                        {
                            string child12 = temp12.ChildNodes[b].Text;
                            string childval12 = temp12.ChildNodes[b].Value;
                            a[j] = childval12;
                            j++;
                        }

                    }
                }
            }
        }
        for (int k = 0; k < j; k++)
        {
            UserAssignRollHRMSTB UTB = new UserAssignRollHRMSTB();
            UTB.MenuId = Convert.ToInt32(a[k]);
            UTB.EmployeeId = Convert.ToInt32(ddemployee.SelectedValue);
            UTB.UserId = Convert.ToInt32(HdSource0.Value);
            UTB.Assignby = Convert.ToInt32(Session["EmpId"].ToString());
            HR.UserAssignRollHRMSTBs.InsertOnSubmit(UTB);
            HR.SubmitChanges();

        }

    }

    private void refreshtreeview(TreeView tview)
    {
        for (int k = 0; k < tview.Nodes.Count; k++)
        {
            TreeNode temp = new TreeNode();
            temp = tview.Nodes[k];
            temp.Checked = false;
            for (int i = 0; i < temp.ChildNodes.Count; i++)
            {
                temp.ChildNodes[i].Checked = false;
                // for subchild
                TreeNode temp12 = new TreeNode();
                temp12 = temp.ChildNodes[i];
                for (int j = 0; j < temp12.ChildNodes.Count; j++)
                {
                    temp12.ChildNodes[j].Checked = false;
                }
            }

        }
    }
    private void TraverseTreeViewch(TreeView tview)
    {

        for (int k = 0; k < tview.Nodes.Count; k++)
        {
            TreeNode temp = new TreeNode();
            temp = tview.Nodes[k];
            if (getval(Convert.ToInt32(temp.Value)))
            {

                temp.Checked = true;
                for (int i = 0; i < temp.ChildNodes.Count; i++)
                {

                    if (getval(Convert.ToInt32(temp.ChildNodes[i].Value)))
                    {
                        temp.ChildNodes[i].Checked = true;
                        temp.CollapseAll();
                    }
                    else
                    {
                        temp.ChildNodes[i].Checked = false;
                    }
                    // Code for subchild
                    TreeNode temp12 = new TreeNode();
                    temp12 = temp.ChildNodes[i];
                    for (int j = 0; j < temp12.ChildNodes.Count; j++)
                    {
                        if (getval(Convert.ToInt32(temp12.ChildNodes[j].Value)))
                        {
                            temp12.ChildNodes[j].Checked = true;
                            temp12.CollapseAll();
                        }
                        else
                        {
                            temp12.ChildNodes[j].Checked = false;
                        }
                    }

                }
            }
            else
            {
                temp.Checked = false;
            }
        }
    }

    public Boolean getval(int val)
    {

        var data = from d in HR.UserAssignRollHRMSTBs
                   where d.MenuId == val & d.EmployeeId == Convert.ToInt32(ddemployee.SelectedValue)
                   select d;
        if (data.Count() == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void Clear()
    {
        UserInformation.Visible = false;
        refreshtreeview(TreeView1); ;
        ddemployee.SelectedIndex = 0;
        btnsubmit.Text = "Submit";

    }
}