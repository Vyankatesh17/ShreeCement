using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ClosedXML.Excel;

public partial class calculate_attendance : System.Web.UI.Page
{
    Genreal gen = new Genreal();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!string.IsNullOrEmpty(Convert.ToString(Session["UserId"])))
        {
            if (!IsPostBack)
            {
                BindCompanyList();
                BindBranchList();
                BindDepartmentList();

                //txtFromDate.Attributes.Add("readonly", "readonly");
                //txtToDate.Attributes.Add("readonly", "readonly");
            }
        }
    }

    protected void ddlCompany_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindBranchList();
        BindDepartmentList();
    }

    protected void ddlBranch_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDepartmentList();
    }



    protected void btnCalculate_Click(object sender, EventArgs e)
    {
        try
        {
            using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
            {
                db.CommandTimeout = 15 * 60;
                litMsg.Text = "";
                bool empFlag = true;
                int empId = 0; string machineId;
                string mobileintime = ""; string mobileouttime = "";
                string calculationmode = "ShiftGroup";
                string Punchdirection = "SingleDirection";
                string weeklyoffmode = "CompanyWise";

                var sysSet = db.SystemSettingsTBs.Where(d => d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.TenantId == Convert.ToString(Session["TenantId"])).FirstOrDefault();

                string CLA = "";
                int allowedLM = 0;
                if (sysSet != null)
                {
                    CLA = sysSet.ConsiderLatemark.HasValue ? sysSet.ConsiderLatemark.Value.ToString() : "H";
                    allowedLM = sysSet.LatemarkAllowed.HasValue ? sysSet.LatemarkAllowed.Value : 0;
                    if(sysSet.AttandanceCalculation != null)
                    {
                        calculationmode = sysSet.AttandanceCalculation;
                    }
                    if (sysSet.WeeklyOff_Calculation != null)
                    {
                        weeklyoffmode = sysSet.WeeklyOff_Calculation;
                    }
                    if (sysSet.Punch_Direction != null)
                    {
                        Punchdirection = sysSet.Punch_Direction;
                    }
                }

                DateTime fromday = Genreal.GetDate(txtFromDate.Text);
                DateTime thru = Genreal.GetDate(txtToDate.Text);

                var empData = (from d in db.EmployeeTBs
                               where d.IsActive == true && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.RelivingStatus == 0
                               select d
                                 ).Distinct();
                if (ddlDepartment.SelectedIndex > 0)
                {
                    empData = empData.Where(d => d.DeptId == Convert.ToInt32(ddlDepartment.SelectedValue)).Distinct();
                }

                if (!string.IsNullOrEmpty(txtEmpCode.Text))
                {
                    empData = empData.Where(d => d.EmployeeNo == txtEmpCode.Text).Distinct();
                    empFlag = true;
                }
                else { empFlag = false; }
                var holidayData = (from d in db.HoliDaysMasters where d.TenantId == Convert.ToString(Session["TenantId"]) && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.Date >= fromday.Date && d.Date <= thru.Date select d).ToList();
                var companyshiftdata = db.MasterShiftTBs.Where(a => a.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && a.IsDefault == true).FirstOrDefault();
                foreach (var item in empData)
                {
                    int lateorEarlyCount = 0;
                    List<AttendaceLogTB> allchkExists = db.AttendaceLogTBs.Where(d => d.EmployeeId == item.EmployeeId && d.AttendanceDate >= fromday && d.AttendanceDate <= thru).ToList();
                    if (allchkExists.Count > 0)
                    {
                        var dt = new DateTime(thru.Year, thru.Month, thru.Day, 23, 59, 59);
                        List<DeviceLogsTB> alldevicelogdata = new List<DeviceLogsTB>();
                        alldevicelogdata = db.DeviceLogsTBs.Where(a => a.EmpID == item.EmployeeId && a.Calculationflag != 0 && a.AttendDate.Value >= fromday && a.AttendDate.Value <= dt).ToList();
                        if (alldevicelogdata.Count > 0)
                        {
                            foreach (var devicelogdata in alldevicelogdata)
                            {
                                devicelogdata.Calculationflag = 0;
                                db.SubmitChanges();
                            }
                        }
                    }

                    for (var day = fromday.Date; day.Date <= thru.Date; day = day.AddDays(1))
                    {
                        string dayName = day.Date.ToString("dddd");                     

                        var leaveslistData = (from d in db.LeaveApplicationDetailsTBs
                                              join la in db.LeaveApplicationsTBs on d.LeaveApplicationId equals la.LeaveApplicationId
                                              join l in db.masterLeavesTBs on la.LeaveTypeId equals l.LeaveID
                                              where la.HrStatus == "Approve" && la.IsLossofPay == 0 && d.Status == true && d.LeaveDate == day.Date
                                              select new { la.EmployeeId, la.LeaveReason, l.LeaveName, la.LeaveTypeId, d.LeaveApplicationId, d.DayWorkStatus }).ToList();
                        //int lateMC = 0;
                        Console.WriteLine(day.ToShortDateString());
                        bool holday = false, isWO = false, isOD = false, isLeave = false, isMobile = false, isWFH = false;
                        string workDayStat = "";
                        TimeSpan workHours = new TimeSpan();
                        TimeSpan wHOUR = new TimeSpan();
                        double earlyBy = 0, lateBy = 0;
                        var dt = day.Date; // time is zero by default
                        string dayofweek = day.DayOfWeek.ToString();
                        //double lBY = 0, eBY = 0;
                        TimeSpan WoODtime = new TimeSpan();

                        AttendaceLogTB attendace = new AttendaceLogTB();
                        var chkExists = (from d in db.AttendaceLogTBs where d.EmployeeId == item.EmployeeId && d.AttendanceDate.Day == day.Day && d.AttendanceDate.Month == day.Month && d.AttendanceDate.Year == day.Year select d).Distinct();

                        if (chkExists.Count() > 0)
                        {
                            //List<DeviceLogsTB> alldevicelogdata = new List<DeviceLogsTB>();
                            attendace = db.AttendaceLogTBs.Where(d => d.LogId == chkExists.FirstOrDefault().LogId).FirstOrDefault();                           
                            
                        }

                        attendace.ShiftId = companyshiftdata.ShiftID;

                        #region Default Values
                        attendace.AttendanceDate = day;
                        attendace.Absent = 1;
                        attendace.EmployeeId = item.EmployeeId;
                        attendace.CompanyId = item.CompanyId.Value;
                        attendace.TenantId = Convert.ToString(Session["TenantId"]);
                        #endregion

                        #region Get Week Offs                    

                        if (chkWeekOff.Checked == true)
                        {
                            var weekCompanyData = new WeeklyOffTB();
                            var weekEmployeeData = new EmployeeWeeklyOffTB();
                            if (weeklyoffmode == "CompanyWise")
                            {
                                weekCompanyData = (from d in db.WeeklyOffTBs where d.CompanyID == Convert.ToInt32(ddlCompany.SelectedValue) && d.TenantId == Convert.ToString(Session["TenantId"]) && d.Days == dayName select d).FirstOrDefault();
                            }
                            else
                            {
                                weekEmployeeData = (from d in db.EmployeeWeeklyOffTBs where d.EmployeeNo == item.EmployeeNo && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.TenantId == Convert.ToString(Session["TenantId"]) && d.Days == dayName select d).FirstOrDefault();
                            }


                            DataTable dtChkWeekOff = new DataTable();
                            if (weekCompanyData != null)
                            {
                                if (weekCompanyData.TrackHolidays == "All")
                                {
                                    isWO = true;
                                    attendace.WeeklyOff = 1;
                                    attendace.Status = "WO";
                                    attendace.Remarks = "Weekly Off";
                                }
                                else if (weekCompanyData.TrackHolidays == "1 & 3")
                                {
                                    dtChkWeekOff = gen.ReturnData(string.Format("exec dbo.GetWeekOff {0},{1},'{2}'", "1", "3", dayName));
                                    if (dtChkWeekOff.Rows.Count > 0)
                                    {
                                        isWO = true;
                                        attendace.WeeklyOff = 1;
                                        attendace.Status = "WO";
                                        attendace.Remarks = "Weekly Off";
                                    }
                                }
                                else if (weekCompanyData.TrackHolidays == "2 & 4")
                                {
                                    dtChkWeekOff = gen.ReturnData(string.Format("exec dbo.GetWeekOff {0},{1},'{2}'", "2", "4", dayName));
                                    if (dtChkWeekOff.Rows.Count > 0)
                                    {
                                        isWO = true;
                                        attendace.WeeklyOff = 1;
                                        attendace.Status = "WO";
                                        attendace.Remarks = "Weekly Off";
                                    }
                                }
                            }

                            if (weekEmployeeData != null)
                            {
                                if(weekEmployeeData.WeeklyOffId > 0)
                                {
                                    isWO = true;
                                    attendace.WeeklyOff = 1;
                                    attendace.Status = "WO";
                                    attendace.Remarks = "Weekly Off";
                                }                               
                            }


                            var rosterdata = (from d in db.BeforeSalaryRosterDetails(day.Day, day.Month, day.Year, item.EmployeeId) select d).ToList();
                            if (rosterdata.Count() > 0)
                            {
                                attendace.WeeklyOff = 1;
                                attendace.Status = "WO";
                                attendace.Remarks = "Weekly Off";
                                isWO = true;
                            }
                            else
                            {

                            }
                        }
                        #endregion

                        #region Get Leave Details
                        if (chkLeaves.Checked == true)
                        {
                            var leavesData = leaveslistData.Where(a => a.EmployeeId == item.EmployeeId).FirstOrDefault();

                            if (leavesData != null)
                            {
                                attendace.IsOnLeave = 1;
                                attendace.LeaveRemarks = leavesData.LeaveReason;
                                attendace.LeaveType = leavesData.LeaveName;
                                attendace.LeaveTypeId = leavesData.LeaveTypeId;
                                attendace.Status = "L";
                                attendace.Remarks = "Leave of type" + leavesData.LeaveName;
                                workDayStat = leavesData.DayWorkStatus;
                                isLeave = true;
                            }
                        }
                        #endregion

                        #region Get OD Data
                        if (chkOD.Checked)
                        {
                            var odData = (from d in db.OutDoorEntryTBs
                                          join o in db.OutDoorEntryDetailsTBs on d.OddId equals o.OutDoorEntry_Id
                                          where d.EmployeeId == item.EmployeeId && d.HrStatus == "Approve" && o.OD_Date == day.Date
                                          select d).DefaultIfEmpty().FirstOrDefault();

                            if (odData != null)
                            {


                                attendace.InTime = odData.FromTime.Value.ToString();//.HolidaysID;
                                attendace.OutTime = odData.ToTime.Value.ToString();
                                WoODtime = odData.ToTime.Value.Subtract(odData.FromTime.Value);
                                attendace.Duration = WoODtime.TotalHours;
                                attendace.LateBy = 0;

                                if (WoODtime.TotalHours > 4)
                                {
                                    attendace.Status = "P(OD)";
                                    attendace.Remarks = "On Duty";
                                    attendace.Duration = WoODtime.TotalHours;
                                }
                                else
                                {
                                    attendace.Status = "½P(OD)";
                                    attendace.Remarks = "On Duty Half Day";
                                    attendace.Duration = WoODtime.TotalHours;
                                }
                                isOD = true;
                            }
                        }
                        #endregion

                        #region Get WFH Data
                        if (chkWFH.Checked)
                        {
                            var WFHdata = (from d in db.WorkFromHomeTBs where d.EmployeeId == item.EmployeeId && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) && d.TenantId == Convert.ToString(Session["TenantId"]) && d.HR_Status == "Approve" && d.FromDate == day.Date select d).DefaultIfEmpty().FirstOrDefault();
                            if (WFHdata != null)
                            {
                                attendace.InTime = WFHdata.InTime.Value.ToString();
                                attendace.OutTime = WFHdata.OutTime.Value.ToString();
                                attendace.Status = "WFH";
                                attendace.Remarks = "WFH Entry";
                                isWFH = true;
                            }
                        }
                        #endregion

                        #region Get Holidays
                        if (chkHoliday.Checked == true)
                        {
                            var hdata = holidayData.Where(a => a.Date == day.Date).FirstOrDefault();
                            if (hdata != null)
                            {
                                attendace.Holiday = hdata.HolidaysID;
                                attendace.Status = "H";
                                attendace.Remarks = "Holiday";
                                holday = true;
                            }
                        }
                        #endregion                    

                        #region Get App Attendance
                        //if (chkMobileApp.Checked == true)
                        //{
                        //    var appData = appListData.Where(a => a.EmpId == item.EmployeeId).Distinct();
                        //    if (appData.Count() > 0)
                        //    {
                        //        var shift_start = appData.Where(d => d.PunchType == "shift_start").FirstOrDefault();
                        //        var shift_end = appData.Where(d => d.PunchType == "shift_end").FirstOrDefault();

                        //        mobileintime = shift_start == null ? shift_end.PunchTime.ToString() :shift_start.PunchTime.Value.ToString();
                        //        mobileouttime = shift_end == null ? shift_start.PunchTime.Value.ToString() : shift_end.PunchTime.ToString();

                        //        attendace.InTime = shift_start == null ? shift_end.PunchTime.ToString() : shift_start.PunchTime.Value.ToString();
                        //        attendace.OutTime = shift_end == null ? shift_start.PunchTime.Value.ToString() : shift_end.PunchTime.ToString();
                        //        attendace.Status = "P";
                        //        attendace.Remarks = "Mobile Punched";
                        //        isMobile = true;
                        //    }
                        //}
                        #endregion

                        #region Get Manual Attendance Data                     
                        //var manAttLog = (from d in db.ManualAttendanceTBs where d.EmpId == item.EmployeeId && d.AttendanceDate.Value.Date == day.Date select d).FirstOrDefault();
                        #endregion
                        
                        #region Get Employee Data
                        if (isOD == true)
                        {
                            if (isOD == true && isWO == true)
                            {
                                attendace.Status = WoODtime.TotalHours > 7.5 ? "WOP(OD)" : "WO½P(OD)";
                                attendace.Remarks = WoODtime.TotalHours > 7.5 ? "OD on Weekly Off" : "Half Day OD on Weekly Off";
                            }

                            if (isOD == true && holday == true)
                            {
                                attendace.Status = WoODtime.TotalHours > 7.5 ? "HP(OD)" : "H½P(OD)";
                                attendace.Remarks = WoODtime.TotalHours > 7.5 ? "OD on Holiday" : "Half Day OD on Holiday";
                            }
                            if (isOD == true && holday == true && isWO == true)
                            {
                                attendace.Status = WoODtime.TotalHours > 7.5 ? "WOHP(OD)" : "WOH½P(OD)";
                                attendace.Remarks = WoODtime.TotalHours > 7.5 ? "OD on Holiday on Weekly Off" : "Half Day OD on Holiday on Weekly Off";
                            }
                        }

                        if (holday == true && isWO == true)
                        {
                            attendace.Status = "WOH";
                            attendace.Remarks = "Holiday on Weekly Off";
                        }

                        //var deviceLogFirst = (from d in db.DeviceLogsTBs
                        //                      where d.ADate.Value.Date == day.Date && d.EmpID == item.EmployeeId && d.Calculationflag == 0
                        //                      orderby d.ATime
                        //                      select new
                        //                      {
                        //                          d.AttendDate,
                        //                          d.EmpID,
                        //                          d.LogId,
                        //                          d.ADate,
                        //                          d.ATime,
                        //                          d.DeviceAccountId,
                        //                          d.PunchStatus
                        //                      }).FirstOrDefault();

                        DeviceLogsTB deviceLogFirst = new DeviceLogsTB();

                        if(Punchdirection == "SingleDirection")
                        {
                            deviceLogFirst = db.DeviceLogsTBs.Where(a => a.EmpID == item.EmployeeId && a.Calculationflag == 0 && a.ADate == day.Date).OrderBy(a => a.ATime).FirstOrDefault();


                           
                        }
                        else if(Punchdirection == "BiDirection")
                        {
                            deviceLogFirst = db.DeviceLogsTBs.Where(a => a.EmpID == item.EmployeeId && a.Calculationflag == 0 && a.ADate == day.Date && a.PunchStatus == "In").OrderBy(a => a.ATime).FirstOrDefault();
                          
                        }

                        
                        if (isOD == false && deviceLogFirst != null)
                        {
                            if (chkExists.Count() > 0)
                            {
                                attendace.Present = 1;
                                attendace.Remarks = "Punched";
                            }

                            attendace.Status = isWO == true ? "WO" : holday == true ? "H" : isOD == true ? "OD" : isWFH == true ? "WFH" : "P";
                            var shiftgroupdetails = db.ShiftGroupDetailsTBs.Where(a => a.ShiftGroupId == item.ShiftGroupId).ToList();
                            var shiftflag = 0;

                            TimeSpan Intimedata = deviceLogFirst.ATime.Value;                           
                            attendace.InTime = Intimedata.ToString();
                            attendace.InDeviceId = deviceLogFirst.DeviceAccountId.ToString();
                            

                            if (calculationmode == "RoasterGroup")
                            {
                                #region Roaster Data

                                string query = string.Format(@"SELECT        RD.RosterId, RD.Day, RD.Type, RM.Year, RM.MonthID, MS.Shift, MS.Intime, MS.Outtime, MS.PunchStart, MS.PunchEnd, MS.LateMarkStart, MS.LateMarkEnd, MS.EarlyMarkStart, MS.EarlyMarkEnd, MS.Shifthours,MS.ShiftID,MS.OTStartAfter
FROM            RosterDetailsTB AS RD INNER JOIN
                         RosterMasterTB AS RM ON RD.RosterId = RM.RosterId INNER JOIN
                          MasterShiftTB AS MS ON RD.ShiftId = MS.ShiftID
WHERE        (RM.EmpID = {0}) AND (CONVERT(date, RD.Date) = CONVERT(date, '{1}')) ORDER BY RD.RosterDetailID DESC", item.EmployeeId, day.ToString("MM/dd/yyyy"));
                                litMsg.Text += string.Format(@"query for employee : {0} and date is : {1} and query is : {2}", item.EmployeeId, day.ToString("MM/dd/yyyy"), query);
                                DataTable dataTable = gen.ReturnData(query);


                                if (dataTable.Rows.Count > 0)
                                {
                                    try
                                    {
                                        attendace.ShiftId = Convert.ToInt32(Convert.ToString(dataTable.Rows[0]["ShiftID"]));
                                        TimeSpan inTime = TimeSpan.Parse(Convert.ToString(dataTable.Rows[0]["Intime"]));
                                        TimeSpan outTime = TimeSpan.Parse(Convert.ToString(dataTable.Rows[0]["Outtime"]));
                                        TimeSpan lateStart = TimeSpan.Parse(Convert.ToString(dataTable.Rows[0]["LateMarkStart"]));
                                        TimeSpan lateEnd = TimeSpan.Parse(Convert.ToString(dataTable.Rows[0]["LateMarkEnd"]));
                                        int pstartMinutes = Convert.ToInt32(Convert.ToString(dataTable.Rows[0]["PunchStart"]));
                                        int pendMinutes = Convert.ToInt32(Convert.ToString(dataTable.Rows[0]["PunchEnd"]));
                                        TimeSpan shiftHours = TimeSpan.Parse(Convert.ToString(dataTable.Rows[0]["Shifthours"]));
                                        TimeSpan OTStartAfter = TimeSpan.Parse(Convert.ToString(dataTable.Rows[0]["OTStartAfter"]));

                                        TimeSpan earlyStart = TimeSpan.Parse(Convert.ToString(dataTable.Rows[0]["EarlyMarkStart"]));
                                        TimeSpan earlyEnd = TimeSpan.Parse(Convert.ToString(dataTable.Rows[0]["EarlyMarkEnd"]));
                                        TimeSpan cPunchStart = inTime.Subtract(TimeSpan.FromMinutes(pstartMinutes));
                                        TimeSpan cPunchEnd = outTime.Add(TimeSpan.FromMinutes(pendMinutes));

                                        var fullStartDt = dt + cPunchStart; // 2010-06-26 01:16:50
                                        var fullEndDt = dt + cPunchEnd; // 2010-06-26 01:16:50

                                        if (chkExists.Count() > 0)
                                        {
                                            List<DeviceLogsTB> alldevicelogdata = new List<DeviceLogsTB>();
                                            alldevicelogdata = db.DeviceLogsTBs.Where(a => a.EmpID == item.EmployeeId && a.Calculationflag != 0 && a.AttendDate.Value >= fullStartDt && a.AttendDate.Value <= fullEndDt).ToList();

                                            if (alldevicelogdata.Count > 0)
                                            {
                                                foreach (var devicelogdata in alldevicelogdata)
                                                {
                                                    devicelogdata.Calculationflag = 0;
                                                    db.SubmitChanges();
                                                }
                                            }
                                        }

                                        //var deviceLogFirst = (from d in db.DeviceLogsTBs                                                              
                                        //                      where d.ADate.Value.Date == day.Date && d.AttendDate.Value >= fullStartDt && d.AttendDate.Value <= fullEndDt
                                        //                      && d.EmpID == item.EmployeeId && d.Calculationflag == 0
                                        //                      select new
                                        //                      {
                                        //                          d.AttendDate,
                                        //                          d.EmpID,
                                        //                          d.LogId,
                                        //                          d.ADate,
                                        //                          d.ATime,
                                        //                          d.DeviceAccountId,
                                        //                          d.PunchStatus                                                                  
                                        //                      }).FirstOrDefault();


                                        var deviceLogEnd = (from d in db.DeviceLogsTBs
                                                            join d1 in db.DevicesTBs on d.DeviceAccountId equals Convert.ToInt32(d1.DeviceAccountId)
                                                            where d.ADate.Value.Date == day.Date && d.AttendDate.Value >= fullStartDt && d.AttendDate.Value <= fullEndDt
                                                            && d.EmpID == item.EmployeeId && d.Calculationflag == 0
                                                            select new
                                                            {
                                                                d.AttendDate,
                                                                d.EmpID,
                                                                d.LogId,
                                                                d.ADate,
                                                                d.ATime,
                                                                d1.DeviceAccountId,
                                                                d.PunchStatus,
                                                                d1.DeviceSerialNo
                                                            }).OrderByDescending(d => d.AttendDate).FirstOrDefault();

                                        //TimeSpan Intimedata = deviceLogFirst.ATime.Value;
                                        TimeSpan Outtimedata = deviceLogEnd.ATime.Value;

                                        //if (deviceLogFirst != null)
                                        //{
                                        //    attendace.InTime = Intimedata.ToString();
                                        //    attendace.InDeviceId = deviceLogFirst.DeviceAccountId.ToString();
                                        //}
                                        if (deviceLogEnd != null)
                                        {
                                            attendace.OutTime = Outtimedata.ToString();
                                            attendace.OutDeviceId = deviceLogEnd.DeviceAccountId;
                                        }
                                        if (deviceLogFirst.LogId == deviceLogEnd.LogId)
                                        {
                                            if (item.Single_Punch_Present == 1)
                                            {
                                                attendace.OutTime = outTime.ToString();
                                                Outtimedata = outTime;
                                            }
                                        }
                                        if (chkExists.Count() > 0)
                                        {
                                            attendace.Present = 1;
                                            attendace.Remarks = "Punched";
                                        }

                                        if (inTime > outTime || Intimedata > Outtimedata)
                                        {
                                            TimeSpan totalt = new TimeSpan(24, 00, 00);
                                            TimeSpan calculatetotal = totalt.Subtract(deviceLogFirst.ATime.Value);
                                            workHours = calculatetotal.Add(Outtimedata);
                                        }
                                        else
                                        {
                                            workHours = Outtimedata.Subtract(deviceLogFirst.ATime.Value);
                                        }
                                        attendace.Duration = workHours.TotalHours;
                                        wHOUR = workHours;

                                        if (deviceLogFirst != null)
                                        {
                                            if (Outtimedata > Intimedata)
                                            {
                                                if (Intimedata.Subtract(lateEnd).TotalMinutes > 0)
                                                {
                                                    attendace.Status = "½ P";
                                                    attendace.Remarks = "In Punch After : " + lateEnd;
                                                }
                                                else if (wHOUR < shiftHours)
                                                {
                                                    attendace.Status = "½ P";
                                                    attendace.Remarks = "Minimum hours not completed.";
                                                }

                                                else if (Outtimedata > OTStartAfter)
                                                {
                                                    if (Outtimedata != Intimedata)
                                                    {
                                                        if (Outtimedata > outTime)
                                                        {
                                                            if (Outtimedata > Intimedata)
                                                            {
                                                                if (wHOUR > shiftHours)
                                                                {
                                                                    TimeSpan ts = deviceLogEnd.ATime.Value.Subtract(outTime);
                                                                    attendace.OverTime = Convert.ToInt32(ts.TotalMinutes);
                                                                }
                                                            }
                                                        }
                                                        else if (inTime > outTime || Intimedata > Outtimedata)
                                                        {
                                                            if (wHOUR > shiftHours)
                                                            {
                                                                TimeSpan totalt = new TimeSpan(24, 00, 00);
                                                                TimeSpan calculatetotal = totalt.Subtract(Outtimedata);
                                                                TimeSpan tt = outTime.Subtract(calculatetotal);

                                                                attendace.OverTime = Convert.ToInt32(tt.TotalMinutes);
                                                            }
                                                        }
                                                    }
                                                }
                                            }

                                            if (deviceLogFirst.PunchStatus == "MOB" || deviceLogEnd.PunchStatus == "MOB")
                                            {
                                                attendace.Remarks = "Mobile Punched";
                                            }



                                            if (deviceLogEnd == null && deviceLogFirst == null)
                                            {
                                                attendace.Status = holday == true ? "H" : "A";
                                                attendace.Absent = 1;
                                            }

                                            if (lateorEarlyCount == 3)
                                            {
                                                if (isWO == true || holday == true)
                                                {
                                                    attendace.Status = isWO == true ? "WO½P" : "H½P";
                                                    attendace.Remarks = isWO == true ? "Present on week-offs" : "Present on holdiay";
                                                }
                                                else
                                                {
                                                    attendace.Status = "½ P";
                                                    attendace.Remarks = "3 Late Mark or Early Mark.";
                                                }
                                                lateorEarlyCount = 0;
                                            }

                                            if (item.Skip_Latemark != 1)
                                            {
                                                if (deviceLogFirst != null)
                                                {
                                                    if (Outtimedata != Intimedata)
                                                    {
                                                        if (Intimedata <= lateStart)
                                                        {
                                                            lateBy = Intimedata.Subtract(lateStart).TotalMinutes;
                                                            lateorEarlyCount++;
                                                        }
                                                        else if (Intimedata <= lateStart)
                                                        {
                                                            TimeSpan totalt = new TimeSpan(24, 00, 00);
                                                            TimeSpan calculatetotal = totalt.Subtract(inTime);
                                                            lateBy = Intimedata.Add(calculatetotal).TotalMinutes;
                                                            lateorEarlyCount++;
                                                        }
                                                    }
                                                    attendace.LateBy = lateBy > 0 ? (int)lateBy : 0;
                                                }
                                            }

                                            if (deviceLogEnd != null)
                                            {
                                                if (Outtimedata != Intimedata)
                                                {
                                                    if (Outtimedata <= earlyStart)
                                                    {
                                                        earlyBy = outTime.Subtract(Outtimedata).TotalMinutes;
                                                        lateorEarlyCount++;
                                                    }
                                                    else if (Outtimedata >= earlyStart)
                                                    {
                                                        TimeSpan totalt = new TimeSpan(24, 00, 00);
                                                        TimeSpan calculatetotal = totalt.Subtract(Outtimedata);
                                                        earlyBy = outTime.Add(calculatetotal).TotalMinutes;
                                                        lateorEarlyCount++;
                                                    }
                                                }
                                                attendace.EarlyBy = earlyBy > 0 ? (int)earlyBy : 0;
                                            }

                                            if (deviceLogFirst != null && deviceLogEnd != null)
                                            {
                                                var setalldevicelogdata = db.DeviceLogsTBs.Where(a => a.AttendDate >= deviceLogFirst.AttendDate && a.AttendDate <= deviceLogEnd.AttendDate && a.EmpID == item.EmployeeId).ToList();
                                                foreach (var devicelogdata in setalldevicelogdata)
                                                {
                                                    devicelogdata.Calculationflag = 1;
                                                    db.SubmitChanges();
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex) { }
                                }
                                #endregion
                            }
                            else if (calculationmode == "ShiftGroup")
                            {
                                #region Shift Group Agains Data
                                try
                                {
                                    foreach (var datails in shiftgroupdetails)
                                    {
                                        if (shiftflag == 0)
                                        {
                                            var shiftdata = db.MasterShiftTBs.Where(a => a.ShiftID == datails.ShiftId && a.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)).FirstOrDefault();
                                            if (shiftdata != null)
                                            {
                                                int pstartMinutes = Convert.ToInt32(shiftdata.PunchStart);
                                                int pendMinutes = Convert.ToInt32(shiftdata.PunchEnd);
                                                TimeSpan inTime = TimeSpan.Parse(shiftdata.Intime);
                                                TimeSpan outTime = TimeSpan.Parse(shiftdata.Outtime);
                                                TimeSpan cPunchStart = inTime.Subtract(TimeSpan.FromMinutes(pstartMinutes));
                                                TimeSpan cPunchEnd = outTime.Add(TimeSpan.FromMinutes(pendMinutes));
                                                var fullStartDt = dt + cPunchStart; // 2010-06-26 01:16:50                                           
                                                var fullEndDt = dt + cPunchEnd; // 2010-06-26 01:16:50

                                                TimeSpan ShiftintimeAddStartminute = inTime.Add(TimeSpan.FromMinutes(pstartMinutes));
                                                DateTime ShiftInEnd = dt + ShiftintimeAddStartminute;

                                                if ((deviceLogFirst.ATime >= ShiftintimeAddStartminute || deviceLogFirst.ATime <= ShiftintimeAddStartminute) && shiftdata.IsDefault == true)
                                                {
                                                    TimeSpan shifminimumttime = TimeSpan.Parse(shiftdata.Shifthours.ToString());
                                                    ShiftInEnd = dt + inTime.Add(shifminimumttime);
                                                }
                                                                                                
                                               

                                                TimeSpan cPunchfirst = outTime.Subtract(TimeSpan.FromMinutes(pstartMinutes));
                                                var firstdevicelasttime = dt + cPunchfirst;
                                                var workingday = dt;
                                                if (inTime >= outTime)
                                                {
                                                    DateTime firstday = dt;
                                                    workingday = firstday.AddDays(1);
                                                    fullEndDt = workingday + cPunchEnd;
                                                    firstdevicelasttime = workingday + cPunchfirst;
                                                }

                                                DeviceLogsTB deviceLogEnd = new DeviceLogsTB();
                                                if (deviceLogEnd.LogId == 0)
                                                {
                                                    attendace.Status = "SP";
                                                }
                                                
                                                    
                                                
                                                if (deviceLogFirst.AttendDate >= fullStartDt && deviceLogFirst.AttendDate <= ShiftInEnd && deviceLogFirst.AttendDate <= firstdevicelasttime)
                                                {
                                                    attendace.ShiftId = shiftdata.ShiftID;
                                                    TimeSpan lateStart = TimeSpan.Parse(shiftdata.LateMarkStart.ToString());
                                                    TimeSpan lateEnd = TimeSpan.Parse(shiftdata.LateMarkEnd.ToString());
                                                    TimeSpan shiftHours = TimeSpan.Parse("07:00");
                                                    if (shiftdata.Shifthours != null)
                                                    {
                                                        shiftHours = TimeSpan.Parse(shiftdata.Shifthours.ToString());
                                                    }
                                                    TimeSpan OTStartAfter = TimeSpan.Parse("00:00");
                                                    if (shiftdata.OTStartAfter != null)
                                                    {
                                                        OTStartAfter = TimeSpan.Parse(shiftdata.OTStartAfter.ToString());
                                                    }
                                                    TimeSpan earlyStart = TimeSpan.Parse("00:00");
                                                    if (shiftdata.EarlyMarkStart != null)
                                                    {
                                                        earlyStart = TimeSpan.Parse(shiftdata.EarlyMarkStart.ToString());
                                                    }
                                                    TimeSpan earlyEnd = TimeSpan.Parse("00:00");
                                                    if (shiftdata.EarlyMarkEnd != null)
                                                    {
                                                        earlyEnd = TimeSpan.Parse(shiftdata.EarlyMarkEnd.ToString());
                                                    }                                                    
                                                    
                                                    if (Punchdirection == "SingleDirection")
                                                    {
                                                        deviceLogEnd = db.DeviceLogsTBs.Where(a => a.EmpID == item.EmployeeId && a.Calculationflag == 0 && a.AttendDate >= fullStartDt && a.AttendDate <= fullEndDt).OrderByDescending(a => a.AttendDate).FirstOrDefault();
                                                    }
                                                    else if (Punchdirection == "BiDirection")
                                                    {
                                                        deviceLogEnd = db.DeviceLogsTBs.Where(a => a.EmpID == item.EmployeeId && a.Calculationflag == 0 && a.AttendDate >= fullStartDt && a.AttendDate <= fullEndDt && a.PunchStatus == "Out").OrderByDescending(a => a.AttendDate).FirstOrDefault();
                                                    }
                                                    if(deviceLogEnd != null)
                                                    {
                                                        attendace.Status = isWO == true ? "WO" : holday == true ? "H" : isOD == true ? "OD" : isWFH == true ? "WFH" : "P";
                                                    }

                                                    attendace.OutTime = null;
                                                    attendace.OutDeviceId = null;
                                                    attendace.LateBy = null;
                                                    attendace.EarlyBy = null;
                                                    attendace.Duration = null;
                                                    if (deviceLogEnd != null && deviceLogEnd.LogId != deviceLogFirst.LogId)
                                                    {
                                                        TimeSpan Outtimedata = deviceLogEnd.ATime.Value;
                                                        TimeSpan totalt = new TimeSpan(24, 00, 00);
                                                        if (deviceLogEnd != null)
                                                        {
                                                            attendace.OutTime = Outtimedata.ToString();
                                                            attendace.OutDeviceId = deviceLogEnd.DeviceAccountId.ToString();
                                                        }

                                                        if (deviceLogFirst.LogId == deviceLogEnd.LogId)
                                                        {
                                                            if (item.Single_Punch_Present == 1)
                                                            {
                                                                attendace.OutTime = outTime.ToString();
                                                                Outtimedata = outTime;
                                                            }
                                                        }

                                                        if (deviceLogFirst.PunchStatus == "MOB" || deviceLogEnd.PunchStatus == "MOB")
                                                        {
                                                            attendace.Remarks = "Mobile Punched";
                                                        }

                                                        if (inTime > outTime || Intimedata > Outtimedata)
                                                        {

                                                            TimeSpan calculatetotal = totalt.Subtract(Intimedata);
                                                            workHours = calculatetotal.Add(Outtimedata);
                                                        }
                                                        else
                                                        {
                                                            workHours = Outtimedata.Subtract(Intimedata);
                                                        }
                                                        attendace.Duration = workHours.TotalHours;
                                                        wHOUR = workHours;

                                                        if (Outtimedata != Intimedata)
                                                        {
                                                            if (inTime < outTime || Intimedata < Outtimedata)
                                                            {
                                                                if (deviceLogFirst.ATime.Value.Subtract(lateEnd).TotalMinutes > 0)
                                                                {
                                                                    attendace.Status = "½ P";
                                                                    attendace.Remarks = "In Punch After : " + lateEnd;
                                                                }
                                                                else if (wHOUR < shiftHours)
                                                                {
                                                                    if (dayofweek == "Saturday" && shiftdata.ShiftID == 1)
                                                                    {
                                                                        attendace.Status = "P";
                                                                        attendace.Remarks = "Saturday Entry.";
                                                                    }
                                                                    else
                                                                    {
                                                                        attendace.Status = "½ P";
                                                                        attendace.Remarks = "Minimum hours not completed.";
                                                                    }
                                                                }
                                                            }
                                                            if (Outtimedata > OTStartAfter)
                                                            {
                                                                if (Outtimedata > outTime)
                                                                {
                                                                    if (wHOUR > shiftHours)
                                                                    {
                                                                        TimeSpan ts = deviceLogEnd.ATime.Value.Subtract(outTime);
                                                                        attendace.OverTime = Convert.ToInt32(ts.TotalMinutes);
                                                                    }
                                                                }
                                                                else if (inTime > outTime || Intimedata > Outtimedata)
                                                                {
                                                                    if (wHOUR > shiftHours)
                                                                    {
                                                                        TimeSpan calculatetotal = totalt.Subtract(Outtimedata);
                                                                        TimeSpan tt = outTime.Subtract(calculatetotal);
                                                                        //TimeSpan ts = deviceLogEnd.ATime.Value.Subtract(outTime);
                                                                        attendace.OverTime = Convert.ToInt32(tt.TotalMinutes);
                                                                    }
                                                                }
                                                            }

                                                            if (lateorEarlyCount == 3)
                                                            {
                                                                attendace.Status = "½ P";
                                                                attendace.Remarks = "3 Late Mark or Early Mark.";
                                                                lateorEarlyCount = 0;
                                                            }

                                                            if (item.Skip_Latemark != 1)
                                                            {
                                                                if (Intimedata >= lateStart)
                                                                {
                                                                    lateBy = Intimedata.Subtract(inTime).TotalMinutes;
                                                                    lateorEarlyCount++;
                                                                }
                                                                if (inTime > outTime)
                                                                {
                                                                    if (Intimedata >= lateStart)
                                                                    {
                                                                        lateBy = Intimedata.Subtract(inTime).TotalMinutes;
                                                                        lateorEarlyCount++;
                                                                    }
                                                                    else if (Intimedata <= lateStart && Intimedata < outTime)
                                                                    {
                                                                        TimeSpan calculatetotal = totalt.Subtract(inTime);
                                                                        lateBy = Intimedata.Add(calculatetotal).TotalMinutes;
                                                                        lateorEarlyCount++;
                                                                    }
                                                                }
                                                                attendace.LateBy = lateBy > 0 ? (int)lateBy : 0;
                                                            }

                                                            if (Outtimedata <= earlyStart && Outtimedata >= inTime)
                                                            {
                                                                earlyBy = outTime.Subtract(Outtimedata).TotalMinutes;
                                                                lateorEarlyCount++;
                                                            }
                                                            if (inTime > outTime)
                                                            {
                                                                if (Outtimedata <= earlyStart)
                                                                {
                                                                    earlyBy = outTime.Subtract(Outtimedata).TotalMinutes;
                                                                    lateorEarlyCount++;
                                                                }
                                                                else if (Outtimedata >= earlyStart && Outtimedata > inTime)
                                                                {
                                                                    TimeSpan calculatetotal = totalt.Subtract(Outtimedata);
                                                                    earlyBy = outTime.Add(calculatetotal).TotalMinutes;
                                                                    lateorEarlyCount++;
                                                                }
                                                            }
                                                            attendace.EarlyBy = earlyBy > 0 ? (int)earlyBy : 0;
                                                        }

                                                    }
                                                    if (deviceLogFirst != null && deviceLogEnd != null)
                                                    {
                                                        var setalldevicelogdata = db.DeviceLogsTBs.Where(a => a.AttendDate >= deviceLogFirst.AttendDate && a.AttendDate <= deviceLogEnd.AttendDate && a.EmpID == item.EmployeeId).ToList();
                                                        foreach (var devicelogdata in setalldevicelogdata)
                                                        {
                                                            devicelogdata.Calculationflag = 1;
                                                            db.SubmitChanges();
                                                        }                                                        
                                                    }

                                                    if(deviceLogFirst != null || deviceLogEnd != null)
                                                    {
                                                        if (deviceLogFirst.LogId == deviceLogEnd.LogId)
                                                        {
                                                            attendace.Status = "SP";
                                                        }
                                                    }

                                                    
                                                    

                                                    shiftflag = 1;
                                                }                                                
                                            }
                                        }
                                    }

                                }
                                catch (Exception ex) { }
                                #endregion
                            }
                        }

                        #endregion

                        if (attendace.InTime != null && attendace.OutTime != null)
                        { 
                            if (isWO == true || holday == true)
                            {
                                attendace.Status = workHours.TotalHours > 7.5 ? isWO == true ? "WOP" : "HP" : isWO == true ? "WO½P" : "H½P";
                                attendace.Remarks = workHours.TotalHours > 7.5 ? isWO == true ? "Present on week-off" : "Present on holdiay" : isWO == true ? "Half Day on Weekly Off" : "Half Day on Holiday";
                            }

                            if (isWO == true && holday == true)
                            {
                                attendace.Status = workHours.TotalHours > 7.5 ? "WOHP" : "WOH½P";
                                attendace.Remarks = workHours.TotalHours > 7.5 ? "Present on Holiday on Weekly Off" : "Half Day on Holiday on Weekly Off";
                            }
                        }

                        

                        if (attendace.InTime == null && attendace.OutTime == null && isWO == false && holday == false && isLeave == false && isOD == false && isWFH == false)
                        {
                            attendace.Status = "A";
                        }

                        if(attendace.OverTime != null)
                        {
                            if(sysSet.OT_Calculation == "OTInDays")
                            {
                                TimeSpan halfdayothours = (TimeSpan)sysSet.Half_Day_OT_Hours;
                                int halfdaytotalminutes = halfdayothours.Hours * 60 + halfdayothours.Minutes;

                                TimeSpan fulldayothours = (TimeSpan)sysSet.Full_Day_OT_Hours;
                                int fulldaytotalminutes = fulldayothours.Hours * 60 + fulldayothours.Minutes;

                                if(attendace.OverTime >= halfdaytotalminutes && attendace.OverTime < fulldaytotalminutes)
                                {
                                    attendace.Status = "P½P";
                                }
                                else if (attendace.OverTime >= fulldaytotalminutes)
                                {
                                    attendace.Status = "PP";
                                }
                            }
                        }

                        if (chkExists.Count() == 0)
                        {
                            db.AttendaceLogTBs.InsertOnSubmit(attendace);
                        }
                        db.SubmitChanges();
                    }
                }
                if (Convert.ToInt32(ddlCompany.SelectedValue) == 81)
                {
                    AttendanceExportToExcel();
                }
            }
        }
        catch (Exception ex) { Console.WriteLine(ex.Message); litMsg.Text = ex.Message; }
        gen.ShowMessage(this.Page, string.Format(@"Attendance calculated for {0}-{1}", txtFromDate.Text, txtToDate.Text));
    }
    private void BindCompanyList()
    {
        try
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

            ddlCompany.SelectedIndex = 1;
            BindDepartmentList();
        }
        catch (Exception ex) { }
    }
    private void BindBranchList()
    {
        ddlBranch.Items.Clear();
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.BranchTBs where d.Status == 1 && d.Companyid == Convert.ToInt32(ddlCompany.SelectedValue) select new { d.Branchname, d.BranchId }).Distinct();

            if (data.Count() > 0)
            {
                ddlBranch.DataSource = data;
                ddlBranch.DataTextField = "BranchName";
                ddlBranch.DataValueField = "BranchId";
                ddlBranch.DataBind();
            }
            ddlBranch.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }
    private void BindDepartmentList()
    {
        ddlDepartment.Items.Clear();
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            var data = (from d in db.MasterDeptTBs where d.Status == 1 && d.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue) select new { d.DeptName, d.DeptID }).Distinct();

            if (data.Count() > 0)
            {
                ddlDepartment.DataSource = data;
                ddlDepartment.DataTextField = "DeptName";
                ddlDepartment.DataValueField = "DeptID";
                ddlDepartment.DataBind();
            }
            ddlDepartment.Items.Insert(0, new ListItem("--Select--", "0"));
        }
    }


    protected void AttendanceExportToExcel()
    {
        using (HrPortalDtaClassDataContext db = new HrPortalDtaClassDataContext())
        {
            DateTime from = Genreal.GetDate(txtFromDate.Text);
            DateTime thru = Genreal.GetDate(txtToDate.Text);

            for (var day = from.Date; day.Date <= thru.Date; day = day.AddDays(1))
            {
                DataTable dt = new DataTable("Employee Attendance");
                dt.Columns.AddRange(new DataColumn[6] {
                                            new DataColumn("Ecode"),
                                            new DataColumn("Attendance Date"),
                                            new DataColumn("In Date"),
                                            new DataColumn("In Time"),
                                            new DataColumn("Out Date"),
                                            new DataColumn("Out Time")});

                var attendancelogdata = db.AttendaceLogTBs.Where(a => a.AttendanceDate == day.Date && a.CompanyId == Convert.ToInt32(ddlCompany.SelectedValue)).Distinct();

                string adate = "";

                foreach (var item in attendancelogdata)
                {
                    adate = item.AttendanceDate.Day + "-" + item.AttendanceDate.ToString("MM") + "-" + item.AttendanceDate.Year;

                    var empdata = db.EmployeeTBs.Where(a => a.EmployeeId == item.EmployeeId).FirstOrDefault();

                    dt.Rows.Add(empdata.EmployeeNo, adate, adate, item.InTime, adate, item.OutTime);
                }


                string csv = string.Empty;

                foreach (DataColumn column in dt.Columns)
                {
                    //Add the Header row for CSV file.
                    csv += column.ColumnName + ',';
                }

                //Add new line.
                csv += "\r\n";

                foreach (DataRow row in dt.Rows)
                {
                    foreach (DataColumn column in dt.Columns)
                    {
                        //Add the Data rows.
                        csv += row[column.ColumnName].ToString().Replace(",", ";") + ',';
                    }

                    //Add new line.
                    csv += "\r\n";
                }

                Response.Clear();
                Response.Buffer = true;
                string fileName = adate;
                Response.AddHeader("content-disposition", "attachment;filename=" + fileName + ".csv");
                Response.Charset = "";
                Response.ContentType = "application/text";
                Response.Output.Write(csv);
                Response.Flush();
                Response.End();


                //using (XLWorkbook wb = new XLWorkbook())
                //{
                //    IXLWorksheet sheet = wb.Worksheets.Add(dt);

                //    Response.Clear();
                //    Response.Buffer = true;
                //    string fileName = adate + "- Attendance.csv";
                //    Response.ContentType = "application/x-msexcel";
                //    Response.AddHeader("content-Disposition", "attachment;filename=" + fileName + "");

                //    Response.Charset = "";


                //    using (MemoryStream MyMemoryStream = new MemoryStream())
                //    {
                //        wb.SaveAs(MyMemoryStream);
                //        MyMemoryStream.WriteTo(Response.OutputStream);
                //        Response.Flush();                        
                //        Response.End();

                //    }
                //}
            }
        }
    }


    //private void AttendanceExportToExcel()
    //{
    //    try
    //    {
    //        SqlDataAdapter sda = new SqlDataAdapter();
    //        try
    //        {
    //            cmd.Connection = con;
    //            con.Open();
    //            sda.SelectCommand = cmd;

    //            DataTable dt = new DataTable();
    //            sda.Fill(dt);

    //            if (dt.Rows.Count > 0)
    //            {
    //                string path = Server.MapPath("exportedfiles\\");

    //                if (!Directory.Exists(path))   // CHECK IF THE FOLDER EXISTS. IF NOT, CREATE A NEW FOLDER.
    //                {
    //                    Directory.CreateDirectory(path);
    //                }

    //                File.Delete(path + "EmployeeDetails.xlsx"); // DELETE THE FILE BEFORE CREATING A NEW ONE.

    //                // ADD A WORKBOOK USING THE EXCEL APPLICATION.
    //                Excel.Application xlAppToExport = new Excel.Application();
    //                xlAppToExport.Workbooks.Add("");

    //                // ADD A WORKSHEET.
    //                Excel.Worksheet xlWorkSheetToExport = default(Excel.Worksheet);
    //                xlWorkSheetToExport = (Excel.Worksheet)xlAppToExport.Sheets["Sheet1"];

    //                // ROW ID FROM WHERE THE DATA STARTS SHOWING.
    //                int iRowCnt = 4;

    //                // SHOW THE HEADER.
    //                xlWorkSheetToExport.Cells[1, 1] = "Employee Details";

    //                Excel.Range range = xlWorkSheetToExport.Cells[1, 1] as Excel.Range;
    //                range.EntireRow.Font.Name = "Calibri";
    //                range.EntireRow.Font.Bold = true;
    //                range.EntireRow.Font.Size = 20;

    //                xlWorkSheetToExport.Range["A1:D1"].MergeCells = true;       // MERGE CELLS OF THE HEADER.

    //                // SHOW COLUMNS ON THE TOP.
    //                xlWorkSheetToExport.Cells[iRowCnt - 1, 1] = "Employee Name";
    //                xlWorkSheetToExport.Cells[iRowCnt - 1, 2] = "Mobile No.";
    //                xlWorkSheetToExport.Cells[iRowCnt - 1, 3] = "PresentAddress";
    //                xlWorkSheetToExport.Cells[iRowCnt - 1, 4] = "Email Address";

    //                int i;
    //                for (i = 0; i <= dt.Rows.Count - 1; i++)
    //                {
    //                    xlWorkSheetToExport.Cells[iRowCnt, 1] = dt.Rows[i].Field<string>("EmpName");
    //                    xlWorkSheetToExport.Cells[iRowCnt, 2] = dt.Rows[i].Field<string>("Mobile");
    //                    xlWorkSheetToExport.Cells[iRowCnt, 3] = dt.Rows[i].Field<string>("PresentAddress");
    //                    xlWorkSheetToExport.Cells[iRowCnt, 4] = dt.Rows[i].Field<string>("Email");

    //                    iRowCnt = iRowCnt + 1;
    //                }

    //                // FINALLY, FORMAT THE EXCEL SHEET USING EXCEL'S AUTOFORMAT FUNCTION.
    //                Excel.Range range1 = xlAppToExport.ActiveCell.Worksheet.Cells[4, 1] as Excel.Range;
    //                range1.AutoFormat(ExcelAutoFormat.xlRangeAutoFormatList3);

    //                // SAVE THE FILE IN A FOLDER.
    //                xlWorkSheetToExport.SaveAs(path + "EmployeeDetails.xlsx");

    //                // CLEAR.
    //                xlAppToExport.Workbooks.Close();
    //                xlAppToExport.Quit();
    //                xlAppToExport = null;
    //                xlWorkSheetToExport = null;

    //                lblConfirm.Text = "Data Exported Successfully";
    //                lblConfirm.Attributes.Add("style", "color:green; font: normal 14px Verdana;");
    //                btView.Attributes.Add("style", "display:block");
    //                btDownLoadFile.Attributes.Add("style", "display:block");
    //            }
    //        }
    //    }
    //    catch (Exception ex) { }
    //}
}