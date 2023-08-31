using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Data.SqlClient;

public partial class ImportData : System.Web.UI.Page
{
    HrPortalDtaClassDataContext hr = new HrPortalDtaClassDataContext();
    Genreal g = new Genreal();
    DataManager DM = new DataManager();
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

    private int SaveStatus(DataSet data)
    {

        LogRecordsDetail lg = new LogRecordsDetail();

        HiddenField1.Value = Convert.ToString(lg.Log_id);
        DateTime months;
        for (int i = 0; i < data.Tables[0].Rows.Count; i++)
        {
            int data1 = Convert.ToInt32(HiddenField1.Value);
            string id = data.Tables[0].Rows[i][1].ToString();
            months = DateTime.Parse(data.Tables[0].Rows[i][3].ToString());

            string m1 = months.Month.ToString();

            var logdata = from d in hr.LogRecordsDetails
                          where d.Employee_id == int.Parse(id)
                          && d.Log_Date_Time.Value.Month == int.Parse(m1)
                          select new
                          {
                              d.Employee_id,
                              d.Emp_Name,
                              d.Log_Date_Time
                          };

            if (logdata.Count() > 0)
            {

                if (months != logdata.First().Log_Date_Time)
                {
                    for (int k = 0; k < data.Tables[0].Rows.Count; k++)
                    {
                        try
                        {
                            int j = DM.InsertUpdateDeleteRecord("Insert into LogRecordsDetails([Log_Date_Time],[Employee_id],[Emp_Name]) values('" + data.Tables[0].Rows[k]["Log_Date_Time"].ToString() + "', '" + data.Tables[0].Rows[k]["Employee_id"].ToString() + "','" + data.Tables[0].Rows[k]["Emp_Name"].ToString() + "')");

                        }
                        catch
                        {
                            return 0;
                        }


                    }
                }
                else
                {

                    g.ShowMessage(this.Page, " Data already exist.");
                    break;
                }
               
            }

            else
            {
                for (int k = 0; k < data.Tables[0].Rows.Count; k++)
                {
                    try
                    {
                        int j = DM.InsertUpdateDeleteRecord("Insert into LogRecordsDetails([Log_Date_Time],[Employee_id],[Emp_Name]) values('" + data.Tables[0].Rows[k]["Log_Date_Time"].ToString() + "', '" + data.Tables[0].Rows[k]["Employee_id"].ToString() + "','" + data.Tables[0].Rows[k]["Emp_Name"].ToString() + "')");

                    }
                    catch
                    {
                        return 0;
                    }
                }
                break;
            }
        }
        
        return 1;
    }
    protected void btnemport_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            var fileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
            // store the file inside ~/App_Data/uploads folder
            FileUpload1.SaveAs(Path.Combine(Server.MapPath("EXFILE/" + fileName)));

            Session["ad"] = fileName;
            //FileUpload1.SaveAs(Server.MapPath("EXFILE/" + fileName));
            ////Create a connection string to access the Excel file using the ACE provider.
            ////This is for Excel 2007. 2003 uses an older driver.
            var connectionString = string.Format("Provider=Microsoft.ACE.OLEDB.12.0; Data Source=" + Path.Combine(Server.MapPath("EXFILE/"), fileName) + ";Extended Properties=Excel 12.0;");

            //Fill the dataset with information from the BV and REV worksheet

            var adapterbvandrev = new OleDbDataAdapter("SELECT * FROM [Sheet1$] ", connectionString);
            //    if(adapterbvandrev.e)
            var dsbvandrev = new DataSet();

            adapterbvandrev.Fill(dsbvandrev);


            int status = SaveStatus(dsbvandrev);

            if (status == 1)
            {
                g.ShowMessage(this.Page, " Data imported and saved successfully.");

                //lblmsg.Text = "Data imported and saved successfully.";
            }
            else
            {
                g.ShowMessage(this.Page, " Some Error Found");

                //lblmsg.Text = "Some Error Found";
            }


        }
    }
    protected void cancel_Click(object sender, EventArgs e)
    {

    }
}