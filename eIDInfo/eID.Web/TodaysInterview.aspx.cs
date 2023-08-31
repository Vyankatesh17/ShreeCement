using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
/// <summary>
/// Amit shinde 4 dec 2014
/// </summary>
public partial class TodaysInterview : System.Web.UI.Page
{
     HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();
     Genreal g = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
          if (Session["UserId"] != null)
        {
            if (!Page.IsPostBack)
            {
                BindInterview();

            }
        }
    }

    protected void BindInterview()
    {
        try
        {
            var interviewdata = (from dt in HR.ScheduleTBs
                                join s in HR.ScheduleDetailsTBs on dt.ScheduleId equals s.ScheduleId
                                join p in HR.SchedulePanelTBs on dt.ScheduleId equals p.ScheduleId
                                join v in HR.VancancyTBs on s.VacancyID equals v.VacancyID
                                join c in HR.CandidateTBs on s.CandidateID equals c.Candidate_ID
                                where dt.Date==DateTime.Now
                                select new { v.Title,s.ScheduleId,c.Name,s.FromTime,s.ToTime,
                                dt.Date,
                                             Interviewername = g.Getempname(dt.ScheduleId)
                                }).Distinct() ;

            //DataSet dsint = g.ReturnData1("select ST.ScheduleId,VT.Title [PositionName],CT.Name [CandidateName],SD.FromTime [ScheduleTime],convert(varchar,ST.Date,101) [ScheduleDate],'" + g.Getempname(Convert.ToInt32(g.ReturnData1("Select SS.ScheduleId from ScheduleTB SS where SS.ScheduleId=ST.ScheduleId"))) + "' from ScheduleTB ST left outer join ScheduleDetailsTB SD ON SD.ScheduleId=ST.ScheduleId left outer join VancancyTB VT ON VT.VacancyID= SD.VacancyID left outer join CandidateTB CT ON CT.Candidate_ID= sd.CandidateID where ST.Date=GETDATE() order by ST.Date desc");
            if (interviewdata.Count()>0)
            {
                grd_Interview.DataSource =interviewdata;
                grd_Interview.DataBind();
            }
            else
               {
                grd_Interview.DataSource = null;
                grd_Interview.DataBind();
            }
        
        }
        catch(Exception ex)
        {
            throw ex;
        }
    }

    protected void grd_Interview_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grd_Interview.PageIndex = e.NewPageIndex;
        BindInterview();
    }
}