using KDSCommon.DataModels;
using KDSCommon.Interfaces.Managers;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Collections.Specialized;
using Newtonsoft.Json;

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
    public string GetEmployeePeiluyot(int misparIshi, DateTime cardDate)
    {
        cardDate = DateTime.Parse(cardDate.ToShortDateString());
        var ovedManager = ServiceLocator.Current.GetInstance<IOvedManager>();
        var ovedDetails = ovedManager.GetOvedDetails(misparIshi, cardDate);
        int emptyInt = 0;
        var htSpecialEmployeeDetails = new OrderedDictionary();
        var htFullEmployeeDetails = new OrderedDictionary();
        OrderedDictionary serviceRes = ovedManager.GetEmployeeDetails(false, ovedDetails, cardDate, misparIshi, out emptyInt, out htSpecialEmployeeDetails, out htFullEmployeeDetails);
        //In order to use the serialized json on the client side - need to replace the OrderedDictionalry with a wel formed object. Using SidurContainer
        List<SidurContainer> sidurContainerList = new List<SidurContainer>();
        foreach (var key in serviceRes.Keys)
        {
            //In order to use the serialized json on the client side - need to replace the OrderedDictionalry with a wel formed object. Using PeilutContainer
            var sidur = serviceRes[key] as SidurDM;
            sidur.PeilutList = new List<PeilutContainer>();
            foreach (var peilutKey in sidur.htPeilut.Keys)
            {
                sidur.PeilutList.Add(new PeilutContainer() { PeilutKey = peilutKey.ToString(), Peilut = sidur.htPeilut[peilutKey] as PeilutDM});
            }
            sidur.htPeilut = null;
            sidurContainerList.Add(new SidurContainer() { SidurKey = key.ToString(), Sidur = serviceRes[key] as SidurDM });
        }

        //Use the newton serializer since .net serializer cannot serialize this object
        string json = JsonConvert.SerializeObject(sidurContainerList);
        return json;

    }
}

public class SidurContainer
{
    public string SidurKey { get; set; }
    public SidurDM Sidur { get; set; }
}