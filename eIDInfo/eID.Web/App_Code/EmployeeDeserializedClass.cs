using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EmployeeDeserializedClass
/// </summary>
public class EmployeeDeserializedClass
{
    public class Valid
    {
        public bool enable { get; set; }
        public DateTime beginTime { get; set; }
        public DateTime endTime { get; set; }
        public string timeType { get; set; }
    }

    public class RightPlan
    {
        public int doorNo { get; set; }
        public string planTemplateNo { get; set; }
    }

    public class UserInfo
    {
        public string employeeNo { get; set; }
        public string name { get; set; }
        public string userType { get; set; }
        public bool closeDelayEnabled { get; set; }
        public Valid Valid { get; set; }
        public string belongGroup { get; set; }
        public string password { get; set; }
        public string doorRight { get; set; }
        public List<RightPlan> RightPlan { get; set; }
        public int maxOpenDoorTime { get; set; }
        public int openDoorTime { get; set; }
        public int roomNumber { get; set; }
        public int floorNumber { get; set; }
        public bool localUIRight { get; set; }
        public string gender { get; set; }
        public int numOfCard { get; set; }
        public int numOfFP { get; set; }
        public int numOfFace { get; set; }
    }

    public class UserInfoSearch
    {
        public string searchID { get; set; }
        public string responseStatusStrg { get; set; }
        public int numOfMatches { get; set; }
        public int totalMatches { get; set; }
        public List<UserInfo> UserInfo { get; set; }
    }
    public class EmployeeDeserializedRoot
    {
        public UserInfoSearch UserInfoSearch { get; set; }
    }

    public EmployeeDeserializedClass()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}