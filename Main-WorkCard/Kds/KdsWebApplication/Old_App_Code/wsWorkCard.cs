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
using KdsWebApplication.ViewModels.WorkCard;
using KdsLibrary.BL;
using System.Data;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class wsWorkCard : System.Web.Services.WebService
{
    [WebMethod]
    public OvedYomAvodaDetailsDM GetOvedDetails(int misparIshi, DateTime cardDate)
    {
        cardDate = DateTime.Parse(cardDate.ToShortDateString());
        IOvedManager ovedManager = ServiceLocator.Current.GetInstance<IOvedManager>();
        OvedYomAvodaDetailsDM ovedDetails = new OvedYomAvodaDetailsDM();
        ovedDetails.iMisparIshi = misparIshi;
        ovedDetails.sHalbasha = "Cloth";
        OvedYomAvodaDetailsDM ovedDetails2 = ovedManager.CreateOvedDetails(misparIshi, cardDate);
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

    [WebMethod]
    public List<UserWCUpdateInfo> GetLastUpdate(int misparIshi, DateTime cardDate)
    {
         clOvdim oOvdim = new clOvdim();
         DataTable dtUpdtes = oOvdim.GetLastUpdator(misparIshi, cardDate);
         List<UserWCUpdateInfo> updates = new List<UserWCUpdateInfo>();
        // UserWCUpdateInfo rowUpdate;
         foreach (DataRow row in dtUpdtes.Rows)
         {
             var userUpdate = new UserWCUpdateInfo();
             userUpdate.UpdateDate = ParseStringToDate(row[0].ToString());
             userUpdate.MisparIshi = ParseStringToInt(row[1].ToString());
             userUpdate.NameUpdater = row[2].ToString();

             updates.Add(userUpdate);
         }
         return updates;
    }

    private DateTime ParseStringToDate(string sDate)
    {
        DateTime d = DateTime.Now;
        DateTime.TryParse(sDate, out d);

        return d;
    }

    private int ParseStringToInt(string sNumber)
    {
        int i=0;
        int.TryParse(sNumber, out i);

        return i;
    }
}
