using KDSCommon.DataModels;
using KDSCommon.Interfaces.Managers;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Collections.Specialized;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class wsWorkCard : System.Web.Services.WebService
{
    [WebMethod]
    public OvedYomAvodaDetailsDM GetOvedDetails(int misparIshi, DateTime cardDate)
    {
        IOvedManager ovedManager = ServiceLocator.Current.GetInstance<IOvedManager>();
        OvedYomAvodaDetailsDM ovedDetails = new OvedYomAvodaDetailsDM();
        ovedDetails.iMisparIshi = misparIshi;
        ovedDetails.sHalbasha = "Cloth";
        //OvedYomAvodaDetailsDM ovedDetails = ovedManager.CreateOvedDetails(iMisparIshi, dCardDate);
        return ovedDetails;
    }

    [WebMethod]
    public OrderedDictionary GetEmployeePeiluyot(int misparIshi, DateTime cardDate)
    {
        var ovedManager = ServiceLocator.Current.GetInstance<IOvedManager>();
        var ovedDetails = ovedManager.GetOvedDetails(misparIshi, cardDate);
        int emptyInt = 0;
        var htSpecialEmployeeDetails = new OrderedDictionary();
        var htFullEmployeeDetails = new OrderedDictionary();
        //OrderedDictionary res = ovedManager.GetEmployeeDetails(false, ovedDetails, cardDate, misparIshi, out emptyInt, out htSpecialEmployeeDetails, out htFullEmployeeDetails);
        OrderedDictionary res = new OrderedDictionary();
        var sidur = new SidurDM() { sVisa = "visa 1" };
        sidur.htPeilut.Add("1",new PeilutDM() { sSnifTnua = "snif 1" });
        sidur.htPeilut.Add("2", new PeilutDM() { sSnifTnua = "snif 2" });
        res.Add("1", sidur);
        return res;
    }
}