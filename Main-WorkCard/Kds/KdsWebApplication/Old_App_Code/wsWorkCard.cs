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
using KDSCommon.DataModels.WorkCard;
using KDSCommon.Enums;
using KDSCommon.Interfaces;

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

    /// <summary>
    /// The enable session param is set to true so that when trying to save the loged in user - the session class will not be null
    /// </summary>
    /// <param name="misparIshi"></param>
    /// <param name="cardDate"></param>
    /// <returns></returns>
    [WebMethod(EnableSession = true)]
    public string GetEmployeePeiluyot(int misparIshi, DateTime cardDate)
    {
        cardDate = DateTime.Parse(cardDate.ToShortDateString());
        IWorkCardSidurManager ovedWCManager = ServiceLocator.Current.GetInstance<IWorkCardSidurManager>();
        var sidurim=ovedWCManager.GetSidurim(misparIshi, cardDate);
        //return sidurim;
        string json = JsonConvert.SerializeObject(sidurim);
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

    [WebMethod]
    public List<EmployeeIdContainer> GetOvdimToUser(string inputstring, string userId)
    {   //מביא את כל המספרים האישיים  של העובדים הכפופים
        
        clOvdim oOvdim = new clOvdim();
        List<EmployeeIdContainer> ids = new List<EmployeeIdContainer>();
        try
        {
            DataTable dt = oOvdim.GetOvdimMisparIshi(inputstring +"%",null);

              foreach (DataRow dr in dt.Rows)
              {
                
                  ids.Add(new EmployeeIdContainer(){MisparIshi = dr["mispar_ishi"].ToString()});
                  
              }
              return ids;
            //List<string> items = new List<string>(count);
            //if ((inputstring.Length > 0) && (inputstring.Substring(0, 1) != "|"))
            //{
            //    prefixText = string.Concat(prefixText, "%");
            //    if (userId.Length > 0)
            //    {
            //        dt = oOvdim.GetOvdimToUser(inputstring, int.Parse(userId));
            //    }
            //    else
            //    {
            //        dt = oOvdim.GetOvdimMisparIshi(inputstring, userId);
            //    }



            //    int i = 0;
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        if (i > count) { break; }
            //        items.Add(dr["mispar_ishi"].ToString());
            //        i++;
            //    }
            //}
            //return items.ToArray();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public EmployeeBasicDetails GetEmpoyeeById(int iMisparIshi, string sCardDate)
    {
        String sXMLResult = "";
        clOvdim oOvdim = new clOvdim();
        DataTable dt;
        DateTime dCardDate = DateTime.Parse(sCardDate);
        EmployeeBasicDetails res = new EmployeeBasicDetails();
    
        if (iMisparIshi > 0)
        {
                    
            dt = oOvdim.GetOvedDetails(iMisparIshi, dCardDate);
            if (dt.Rows.Count > 0)
            {
                res.Id = iMisparIshi;// ParseStringToInt(dt.Rows[0]["FULL_NAME"].ToString());
                res.Name = dt.Rows[0]["FULL_NAME"].ToString();
                res.UnitName = dt.Rows[0]["TEUR_SNIF_AV"].ToString();
                return res;
            }
        }
        return null;
    }

    [WebMethod]
    public List<ReasonMissingSignature> GetSibotLedivuach()
    {
        List<ReasonMissingSignature> sibot = new List<ReasonMissingSignature>();
        var cache = ServiceLocator.Current.GetInstance<IKDSCacheManager>();
        DataTable dvSibotLedivuch = cache.GetCacheItem<DataTable>(CachedItems.SibotLedivuchYadani);

        foreach (DataRow dr in dvSibotLedivuch.Rows)
        {

            var siba = new ReasonMissingSignature();
            siba.Value = ParseStringToInt(dr["KOD_SIBA"].ToString());
            siba.Description = dr["TEUR_SIBA"].ToString();
            sibot.Add(siba);

        }
        //string json = JsonConvert.SerializeObject(sibot);
     //   return json;
        return sibot;
        
    }

    [WebMethod]
    public List<Hariga> GetHariga()
    {
        List<Hariga> Harigot = new List<Hariga>();
        var cache = ServiceLocator.Current.GetInstance<IKDSCacheManager>();
        DataRow[] dtHariga = cache.GetCacheItem<DataTable>(CachedItems.LookUpTables).Select(string.Concat("table_name='ctb_divuch_hariga_meshaot'"));

        foreach (DataRow dr in dtHariga)
        {

            var oHariga = new Hariga();
            oHariga.Value = ParseStringToInt(dr["KOD"].ToString());
            oHariga.Description = dr["TEUR"].ToString();
            Harigot.Add(oHariga);

        }
        return Harigot;
        
    }

    [WebMethod]
    public List<Pizul> GetPizul()
    {
        List<Pizul> Pizulim = new List<Pizul>();
        var cache = ServiceLocator.Current.GetInstance<IKDSCacheManager>();
        DataRow[] dtPizul = cache.GetCacheItem<DataTable>(CachedItems.LookUpTables).Select(string.Concat("table_name='ctb_pitzul_hafsaka'"));

        foreach (DataRow dr in dtPizul)
        {

            var oPizul = new Pizul();
            oPizul.Value = ParseStringToInt(dr["KOD"].ToString());
            oPizul.Description = dr["TEUR"].ToString();
            Pizulim.Add(oPizul);

        }
        return Pizulim;

    }

    
    [WebMethod]
    public List<Hashlama> GetHashlama()
    {
        List<Hashlama> Hashlamot = new List<Hashlama>();
        var cache = ServiceLocator.Current.GetInstance<IKDSCacheManager>();
        DataRow[] dtHashlama = cache.GetCacheItem<DataTable>(CachedItems.LookUpTables).Select(string.Concat("table_name='ctb_sibot_hashlama_leyom'"));

        foreach (DataRow dr in dtHashlama)
        {

            var oHashlama = new Hashlama();
            oHashlama.Value = ParseStringToInt(dr["KOD"].ToString());
            oHashlama.Description = dr["TEUR"].ToString();
            Hashlamot.Add(oHashlama);

        }
        return Hashlamot;

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
