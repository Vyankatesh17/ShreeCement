using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class EmpCalendar : System.Web.UI.Page
{
    // Created By Abdul rahman.. Employee Calendar Show on training Shedule
    HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {
            if (!IsPostBack)
            {

                
            }
            var empcal = from n in hr.TrainingSheduleTBs
                         where n.Shedule_Status == 0 && n.User_ID == Convert.ToInt32(Session["UserId"])
                         select new
                         {
                             EventDay = n.Training_Date.ToString().Replace("12:00:00 AM", " "),
                             n.Training_Topic,

                             TrainingTime = n.AM_PM.ToString() == "1" ? n.Time + "AM" : n.Time + "PM",
                             DeptName = n.MasterDeptTB.DeptName,
                             CopmanyName = n.CompanyInfoTB.CompanyName,
                             n.Training_Shedule_ID
                         };
            cal1.DataSource = (DataTable)ToDataTable(hr, empcal);
        }
        else
        {
            Response.Redirect("~/Default.aspx");
        }

    }

    public DataTable ToDataTable(System.Data.Linq.DataContext ctx, object query)
    {
        if (query == null)
        {
            throw new ArgumentNullException("query");
        }
        IDbCommand cmd = ctx.GetCommand((IQueryable)query);
        System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter();
        adapter.SelectCommand = (System.Data.SqlClient.SqlCommand)cmd;
        DataTable dt = new DataTable("dataTbl");
        try
        {
            cmd.Connection.Open();
            adapter.FillSchema(dt, SchemaType.Source);
            adapter.Fill(dt);
        }
        finally
        {
            cmd.Connection.Close();
        }
        return dt;
    }
}