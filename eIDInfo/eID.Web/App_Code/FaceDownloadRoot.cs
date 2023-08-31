using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

/// <summary>
/// Summary description for FaceDownloadRoot
/// </summary>
public class FaceDownloadRoot
{
    public FaceDownloadRoot()
    {
        //
        // TODO: Add constructor logic here
        //
    }
}

// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
public class FaceInfo
{
    public string employeeNo { get; set; }
    public string faceURL { get; set; }
}

public class FaceInfoSearch
{
    public List<FaceInfo> FaceInfo { get; set; }
    public int numOfMatches { get; set; }
    public string responseStatusStrg { get; set; }
    public string searchID { get; set; }
    public int totalMatches { get; set; }
}

public class FaceDownRoot
{
    public FaceInfoSearch FaceInfoSearch { get; set; }
}

public class Fingerinfo
{
    public int cardReaderNo { get; set; }
    public string fingerData { get; set; }
    public int fingerPrintID { get; set; }
    public string fingerType { get; set; }
    public List<string> leaderFP { get; set; }
}

public class FingerInfoList
{
    public List<Fingerinfo> FingerPrintList { get; set; }   
    public string searchID { get; set; }
    public string status { get; set; }   
}

public class FingerRoot
{
    public FingerInfoList FingerPrintInfo { get; set; }
}


public class Cardinfo
{
    public string cardNo { get; set; }
    public string cardType { get; set; }
    public string employeeNo { get; set; }    
}


public class CardInfoList
{
    public List<Cardinfo> CardInfo { get; set; }
    public int numOfMatche { get; set; }
    public string responseStatusStrg { get; set; }
    public string searchID { get; set; }
    public int totalMatches { get; set; }
}

public class CardrRoot
{
    public CardInfoList CardInfoSearch { get; set; }
}

public class passwordRoot
{
    public passwordInfoList UserInfoSearch { get; set; }
}

public class passwordInfoList
{      
    public string searchID { get; set; }
    public string responseStatusStrg { get; set; }
    public int numOfMatche { get; set; }
    public int totalMatches { get; set; }
    public List<passwordinfo> UserInfo { get; set; }
}



public class passwordinfo
{
    public string employeeNo { get; set; }
    public string name { get; set; }
    public string userType { get; set; }
    public bool closeDelayEnabled { get; set; }
    public List<Validinfo> Valid { get; set; }
    public string belongGroup { get; set; }
    public string password { get; set; }
    public string doorRight { get; set; }
    public List<door> RightPlan { get; set; }

    public int maxOpenDoorTime { get; set; }
    public int openDoorTime { get; set; }
    public int roomNumber { get; set; }
    public int floorNumber { get; set; }

    public bool localUIRight { get; set; }
    public string gender { get; set; }
    public int numOfCard { get; set; }
    public int numOfFP { get; set; }
    public int numOfFace { get; set; }
    public List<personinfo> PersonInfoExtends { get; set; }
}

public class Validinfo
{
    public bool enable { get; set; }
    public string beginTime { get; set; }
    public string endTime { get; set; }
    public string timeType { get; set; }   
}


public class door
{
    public int doorNo { get; set; }
    public string planTemplateNo { get; set; }   
}



public class personinfo
{
    public string value { get; set; }   
}


[XmlRoot(ElementName = "CaptureFaceData")]
public class CaptureFaceData
{
    [XmlElement]
    public string faceDataUrl;
    [XmlElement]
    public int captureProgress;
    [XmlElement]
    public string isCurRequestOver;    
}