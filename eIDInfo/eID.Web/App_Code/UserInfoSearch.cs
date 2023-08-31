using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for UserInfoSearch
/// </summary>
// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
public class Valid
{
    public DateTime beginTime { get; set; }
    public bool enable { get; set; }
    public DateTime endTime { get; set; }
    public string timeType { get; set; }
}

public class UserInfo
{
    public Valid Valid { get; set; }
    public string employeeNo { get; set; }
    public string name { get; set; }
    public string userType { get; set; }
}

public class UserInfoSearch
{
    public List<UserInfo> UserInfo { get; set; }
    public int numOfMatches { get; set; }
    public string responseStatusStrg { get; set; }
    public string searchID { get; set; }
    public int totalMatches { get; set; }
}

public class RootUser
{
    public UserInfoSearch UserInfoSearch { get; set; }
}

public class UserInfoCount
{
    public int userNumber { get; set; }
}

