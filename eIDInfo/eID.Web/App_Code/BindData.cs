using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using AjaxControlToolkit;

/// <summary>
/// Summary description for BindData
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
// [System.Web.Script.Services.ScriptService]
public class BindData : System.Web.Services.WebService {

    public BindData () {

        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod]
    public string HelloWorld() {
        return "Hello World";
    }


     [WebMethod]
    public  CascadingDropDownNameValue[] GetCountry(string knownCategoryValues, string category)
    {
        string CountryID = category;
        HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();

        var Data = from d in HR.CountryTBs select d;
        //create list and add items in it by looping through dataset table
        List<CascadingDropDownNameValue> CountrDetails = new List<CascadingDropDownNameValue>();
        foreach (var item in Data)
        {


            string StateId = item.CountryId.ToString();
            string StateName = item.CountryName;
            CountrDetails.Add(new CascadingDropDownNameValue(StateName, StateId));
        }
        return CountrDetails.ToArray();

    }



     [WebMethod]
    public  CascadingDropDownNameValue[] GetCity(string knownCategoryValues, string category)
    {
        int StartIndex = knownCategoryValues.IndexOf(':');
        int EndIndex = knownCategoryValues.IndexOf(';') - 1;
        string StateId = knownCategoryValues.Substring(StartIndex + 1, EndIndex - StartIndex);
        HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();

        var Data = from d in HR.CityTBs where d.StateId.ToString() == StateId select d;
        //create list and add items in it by looping through dataset table
        List<CascadingDropDownNameValue> StateDetails = new List<CascadingDropDownNameValue>();
        foreach (var item in Data)
        {


            string CityId = item.CityId.ToString();
            string CityName = item.CityName;
            StateDetails.Add(new CascadingDropDownNameValue(CityName, CityId));
        }
        return StateDetails.ToArray();
    }



    [WebMethod]
    public  AjaxControlToolkit.CascadingDropDownNameValue[] GetState(string knownCategoryValues, string category)
    {
        int StartIndex = knownCategoryValues.IndexOf(':');
        int EndIndex = knownCategoryValues.IndexOf(';') - 1;
        string CountryID = knownCategoryValues.Substring(StartIndex + 1, EndIndex - StartIndex);
        HrPortalDtaClassDataContext HR = new HrPortalDtaClassDataContext();

        var Data = from d in HR.StateTBs where d.CountryId.ToString() == CountryID select d;
        //create list and add items in it by looping through dataset table
        List<CascadingDropDownNameValue> StateDetails = new List<CascadingDropDownNameValue>();
        foreach (var item in Data)
        {


            string StateId = item.StateId.ToString();
            string StateName = item.StateName;
            StateDetails.Add(new CascadingDropDownNameValue(StateName, StateId));
        }
        return StateDetails.ToArray();
    }
    
}
