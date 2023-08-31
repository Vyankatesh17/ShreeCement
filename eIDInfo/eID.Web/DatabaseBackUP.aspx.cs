using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;


public partial class DatabaseBackUP : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {

            }
        }
        else
        {
            Response.Redirect("login.aspx");
        }
    }
    protected void Message(string msg)
    {
        string msg1 = "<script language='javascript'>window.alert('" + msg + "')</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "", msg1, false);

    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        SqlConnectionStringBuilder Builder = new SqlConnectionStringBuilder(g.DBgetConnectionString());

        string sourcepath = "D:\\";
        string DbName = Builder.InitialCatalog;

        string BackupName = DbName + DateTime.Today.Day.ToString() + DateTime.Now.ToString("MMM") + DateTime.Today.Year.ToString() + "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString() + ".bak";

        //  string BackupName = DateTime.Now.ToShortDateString() + ".bak";
        string DestPath = "\\" + BackupName;

        DataSet ds = g.ProcdureWith3Param("spDatabaseBackUp", DbName, DestPath, sourcepath);
        //Message("Backup Completed..");

        g.ShowMessage(this.Page, "Backup Completed..");

        //SqlConnection con = new SqlConnection();
        //SqlCommand sqlcmd = new SqlCommand();
        //SqlDataAdapter da = new SqlDataAdapter();
        //DataTable dt = new DataTable();

        //con.ConnectionString = g.DBgetConnectionString();

        //string backupDIR = "D:\\DataBase Backup";
        //if (!System.IO.Directory.Exists(backupDIR))
        //{
        //    System.IO.Directory.CreateDirectory(backupDIR);
        //}
        //try
        //{
        //    con.Open();
        //    sqlcmd = new SqlCommand("backup database test to disk='" + backupDIR + "\\" + DateTime.Now.ToString("ddMMyyyy_HHmmss") + ".Bak'", con);
        //    sqlcmd.ExecuteNonQuery();
        //    con.Close();
        //    // lblError.Text = "Backup database successfully";
        //}
        //catch (Exception ex)
        //{
        //    // lblError.Text = "Error Occured During DB backup process !<br>" + ex.ToString();
        //}
       // g.ShowMessage(this.Page, "Backup Completed..");
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        Server.Transfer("admin_dashboard.aspx");
    }
}