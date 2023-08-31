using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

/// <summary>
/// Summary description for clienthttp
/// </summary>
public class clienthttp
{
    private CredentialCache _credentialCache = null;
    private string strURL = string.Empty;
    private int m_iHttpTimeOut = 200000;
    //type:0-data,1-data over,2-content length
    public delegate void HttpCallback(int type, byte[] buf, int len);
    private HttpCallback m_cb;

    private CredentialCache GetCredentialCache(string sUrl, string strUserName, string strPassword)
    {
        if (_credentialCache == null)
        {
            _credentialCache = new CredentialCache();
            _credentialCache.Add(new Uri(sUrl), "Digest", new NetworkCredential(strUserName, strPassword));
            strURL = sUrl;
        }
        if (strURL != sUrl)
        {
            _credentialCache.Add(new Uri(sUrl), "Digest", new NetworkCredential(strUserName, strPassword));
            strURL = sUrl;
        }

        return _credentialCache;
    }

    public int HttpRequest(string strUserName, string strPassword, string strUrl, string strHttpMethod, string strReq, ref string strRsp)
    {
        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(strUrl);
        request.Credentials = GetCredentialCache(strUrl, strUserName, strPassword);
        request.Method = strHttpMethod;
        request.Timeout = m_iHttpTimeOut;

        if (strReq.Length > 0)
        {
            byte[] bs = Encoding.ASCII.GetBytes(strReq);

            request.ContentType = "text/json";
            request.ContentLength = bs.Length;
            request.KeepAlive = true;
            using (Stream reqStream = request.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
            }
        }

        try
        {
            WebResponse wr = request.GetResponse();
            strRsp = new StreamReader(wr.GetResponseStream()).ReadToEnd();

            wr.Close();

            return (int)HttpStatus.Http200;
        }
        catch (WebException ex)
        {
            WebResponse wenReq = (HttpWebResponse)ex.Response;
            if (wenReq != null)
            {
                strRsp = new StreamReader(wenReq.GetResponseStream()).ReadToEnd();
                wenReq.Close();
            }

            return (int)HttpStatus.HttpOther;
        }
    }

    public int HttpPostData(string strUserName, string strPassword, string strUrl, string fileKeyName, string filePath, NameValueCollection stringDict, ref string strRsp)
    {
        HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(strUrl);
        webRequest.Credentials = GetCredentialCache(strUrl, strUserName, strPassword);
        webRequest.Method = "POST";
        webRequest.Timeout = 50000;

        // boundary
        var boundary = "---------------" + DateTime.Now.Ticks.ToString("x");
        var beginBoundary = Encoding.ASCII.GetBytes("--" + boundary + "\r\n");
        var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        var endBoundary = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");

        var memStream = new MemoryStream();

        webRequest.ContentType = "multipart/form-data; boundary=" + boundary;
        // request
        var stringKeyHeader = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\"" + "\r\n\r\n{1}\r\n";
        foreach (byte[] formitembytes in from string key in stringDict.Keys
                                         select string.Format(stringKeyHeader, key, stringDict[key])
                                            into formitem
                                         select Encoding.ASCII.GetBytes(formitem))
        {
            memStream.Write(formitembytes, 0, formitembytes.Length);
        }

        // picture
        const string filePartHeader = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" + "Content-Type: image/jpeg\r\n\r\n";
        var header = string.Format(filePartHeader, fileKeyName, "1.jpg");
        //var headerbytes = Encoding.UTF8.GetBytes(header);
        var headerbytes = Encoding.ASCII.GetBytes(header);

        memStream.Write(beginBoundary, 0, beginBoundary.Length);
        memStream.Write(headerbytes, 0, headerbytes.Length);

        var buffer = new byte[1024];
        int bytesRead; // =0

        while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
        {
            memStream.Write(buffer, 0, bytesRead);
        }

        //end Boundary
        memStream.Write(endBoundary, 0, endBoundary.Length);
        webRequest.ContentLength = memStream.Length;
        var requestStream = webRequest.GetRequestStream();
        memStream.Position = 0;
        var tempBuffer = new byte[memStream.Length];
        memStream.Read(tempBuffer, 0, tempBuffer.Length);
        memStream.Close();

        requestStream.Write(tempBuffer, 0, tempBuffer.Length);
        requestStream.Close();

        try
        {
            WebResponse wr = webRequest.GetResponse();
            strRsp = new StreamReader(wr.GetResponseStream()).ReadToEnd();

            wr.Close();
            fileStream.Close();
            return (int)HttpStatus.Http200;
        }
        catch (WebException ex)
        {
            fileStream.Close();
            return (int)HttpStatus.HttpOther;
        }
    }


    public int DownloadFile(string strUserName, string strPassword, string strUrl, string strHttpMethod, string localFilename)
    {
        HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(strUrl);
        request.Credentials = GetCredentialCache(strUrl, strUserName, strPassword);
        request.Method = strHttpMethod;
        request.Timeout = m_iHttpTimeOut;


        Stream remoteStream = null;
        Stream localStream = null;
        WebResponse response = null;
        int bytesProcessed = 0;
        try
        {
            if (request != null)
            {
                // Send the request to the server and retrieve the
                // WebResponse object 
                response = request.GetResponse();
                if (response != null)
                {
                    // Once the WebResponse object has been retrieved,
                    // get the stream object associated with the response's data
                    remoteStream = response.GetResponseStream();

                    // Create the local file

                    localStream = File.Create(localFilename);

                    // Allocate a 1k buffer
                    byte[] buffer = new byte[1024];
                    int bytesRead;

                    // Simple do/while loop to read from stream until
                    // no bytes are returned
                    do
                    {
                        // Read data (up to 1k) from the stream
                        bytesRead = remoteStream.Read(buffer, 0, buffer.Length);

                        // Write the data to the local file
                        localStream.Write(buffer, 0, bytesRead);

                        // Increment total bytes processed
                        bytesProcessed += bytesRead;
                    } while (bytesRead > 0);
                }
            }
            
            return (int)HttpStatus.Http200;
        }
        catch (WebException ex)
        {
            WebResponse wenReq = (HttpWebResponse)ex.Response;
            if (wenReq != null)
            {
                wenReq.Close();
            }

            return (int)HttpStatus.HttpOther;
        }
        finally
        {
            // Close the response and streams objects here 
            // to make sure they're closed even if an exception
            // is thrown at some point
            if (response != null) response.Close();
            if (remoteStream != null) remoteStream.Close();
            if (localStream != null) localStream.Close();
        }
    }

    public HttpWebRequest CreateSOAPWebRequest(string headerdata)
    {
        var url = ConfigurationManager.AppSettings["SOAPURL"];

        string soapurl = url + headerdata;
        //Making Web Request  
        HttpWebRequest Req = (HttpWebRequest)WebRequest.Create(soapurl);
        //SOAPAction  
        Req.Headers.Add(@"SOAPAction:http://tempuri.org/" + headerdata);
        //Content_type  
        Req.ContentType = "text/xml;charset=\"utf-8\"";
        Req.Accept = "text/xml";
        //HTTP method  
        Req.Method = "POST";
        //return HttpWebRequest  
        return Req;
    }






}
public enum HttpStatus
{
    Http200 = 0,
    HttpOther,
    HttpTimeOut
}

public class DeviceFingerprintRoot
{
    public fingerprintcapture CaptureFingerPrint { get; set; }
}
public class fingerprintcapture
{
    public string fingerData { get; set; }
    public int fingerNo { get; set; }
    public int fingerPrintQuality { get; set; }
}

public class Devicesettimeresult
{
    public int errorCode { get; set; }
    public string errorMsg { get; set; }
    public int statusCode { get; set; }
    public string statusString { get; set; }
    public string subStatusCode { get; set; }
}

public class DeviceSearchRoot
{
    public DeviceSearchResult SearchResult { get; set; }
}
public class DeviceSearchResult
{
    public List<MatchList> MatchList { get; set; }
    public int numOfMatches { get; set; }
    public int totalMatches { get; set; }
}
public class MatchList
{
    public MatchlistDevice Device { get; set; }
}
public class MatchlistDevice
{
    public EhomeParams EhomeParams { get; set; }
    public bool activeStatus { get; set; }
    public string devIndex { get; set; }
    public string devMode { get; set; }
    public string devName { get; set; }
    public string devStatus { get; set; }
    public string devType { get; set; }
    public string protocolType { get; set; }
    public int videoChannelNum { get; set; }
}
public class EhomeParams
{
    public string EhomeID { get; set; }
}
public class EventSearchRoot
{
    public EventSearchResult AcsEventSearchResult { get; set; }
}
public class EventSearchResult
{
    public string numOfMatches { get; set; }
    public string totalMatches { get; set; }
    public string responseStatusStrg { get; set; }
    public List<EventInfo> MatchList { get; set; }
}
public class EventInfo
{
    public string major;
    public string minor;
    public string time;
    public DateTime eventdate;
    public string Atime;
    public string ADate;
    public string employeeNoString;
    public string cardNo;
    public string cardReaderNo;
    public string doorNo;
    public string currentVerifyMode;
    public string serialNo;
    public string type;
    public string mask;
    public string name;
    public string userType;
}