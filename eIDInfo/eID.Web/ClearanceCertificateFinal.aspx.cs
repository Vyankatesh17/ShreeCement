using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class ClearanceCertificateFinal : System.Web.UI.Page
{
    HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["UserId"] != null)
        {

            if (!Page.IsPostBack)
            {
                MultiView1.ActiveViewIndex = 0;

                BindAllData();

               
            }
        }
        else
        {


        }
    }

    private void BindAllData()
    {
        DataSet dsalldata = g.ReturnData1("SELECT     ClearanceCertificateStatusID,FName + ' '+Lname 'Ename','Department Head' DH,DepartmentHeadclearnaceID,DeparmentAllStatus, ET.EmployeeID,'Accounts & Finance' AF, AccountsclearanceID, AccountsAllStatus, 'Admin' admin, AdminClearnaceId, AdminAllStatus,'IT' IT, ITClearanceId, ITAllStatus, 'HR' HR,HRClearanceId, HRAllStatus, UserID FROM         ClearanceCertificateFinalTB CCT left outer join EmployeeTB ET on ET.EmployeeId=CCT.EmployeeID ");

        if (dsalldata.Tables[0].Rows.Count > 0)
        {
            grdalldata.DataSource = dsalldata.Tables[0];
            grdalldata.DataBind();
        }
    }
    protected void lnkApproved_Click(object sender, EventArgs e)
    {
        LinkButton lnk = (sender) as LinkButton;
        string id = lnk.CommandArgument;



        string ss = "window.open('ClearanceCertificateReportviewer.aspx?id=" + id + "','mywindow','width=1000,height=700,left=200,top=1,screenX=100,screenY=100,toolbar=no,location=no,directories=no,status=yes,menubar=no,scrollbars=yes,copyhistory=yes,resizable=no')";
        string script = "<script language='javascript'>" + ss + "</script>";
        ScriptManager.RegisterStartupScript(this, this.GetType(), "PopUpWindow", script, false);

       // Response.Redirect("ClearanceCertificateReportviewer.aspx?id="+id+"");
    }
}