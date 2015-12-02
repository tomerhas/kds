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
    public List<SelectItem> GetSibotLedivuach()
    {
        List<SelectItem> sibot = new List<SelectItem>();
        var cache = ServiceLocator.Current.GetInstance<IKDSCacheManager>();
        DataTable dvSibotLedivuch = cache.GetCacheItem<DataTable>(CachedItems.SibotLedivuchYadani);

        foreach (DataRow dr in dvSibotLedivuch.Rows)
        {

            var siba = new SelectItem();
            siba.Value = ParseStringToInt(dr["KOD_SIBA"].ToString());
            siba.Description = dr["TEUR_SIBA"].ToString();
            sibot.Add(siba);

        }
        //string json = JsonConvert.SerializeObject(sibot);
     //   return json;
        return sibot;
        
    }

    [WebMethod]
    public List<SelectItem> GetHariga()
    {
        List<SelectItem> Harigot = new List<SelectItem>();
        var cache = ServiceLocator.Current.GetInstance<IKDSCacheManager>();
        DataRow[] dtHariga = cache.GetCacheItem<DataTable>(CachedItems.LookUpTables).Select(string.Concat("table_name='ctb_divuch_hariga_meshaot'"));

        foreach (DataRow dr in dtHariga)
        {

            var oHariga = new SelectItem();
            oHariga.Value = ParseStringToInt(dr["KOD"].ToString());
            oHariga.Description = dr["TEUR"].ToString();
            Harigot.Add(oHariga);

        }
        return Harigot;
        
    }

    [WebMethod]
    public List<SelectItem> GetPizul()
    {
        List<SelectItem> Pizulim = new List<SelectItem>();
        var cache = ServiceLocator.Current.GetInstance<IKDSCacheManager>();
        DataRow[] dtPizul = cache.GetCacheItem<DataTable>(CachedItems.LookUpTables).Select(string.Concat("table_name='ctb_pitzul_hafsaka'"));

        foreach (DataRow dr in dtPizul)
        {

            var oPizul = new SelectItem();
            oPizul.Value = ParseStringToInt(dr["KOD"].ToString());
            oPizul.Description = dr["TEUR"].ToString();
            Pizulim.Add(oPizul);

        }
        return Pizulim;

    }

     [WebMethod]
    public List<SelectItem> GetZmanNesia()
    {
        List<SelectItem> Zmanim = new List<SelectItem>();
        var cache = ServiceLocator.Current.GetInstance<IKDSCacheManager>();
        DataRow[] dtZmanNesia = cache.GetCacheItem<DataTable>(CachedItems.LookUpTables).Select(string.Concat("table_name='ctb_zmaney_nesiaa'"));

        foreach (DataRow dr in dtZmanNesia)
        {

            var oZman = new SelectItem();
            oZman.Value = ParseStringToInt(dr["KOD"].ToString());
            oZman.Description = dr["TEUR"].ToString();
            Zmanim.Add(oZman);

        }
        return Zmanim;

    }

    

    [WebMethod]
     public List<SelectItem> GetHashlama()
    {
        List<SelectItem> Hashlamot = new List<SelectItem>();
        var cache = ServiceLocator.Current.GetInstance<IKDSCacheManager>();

        for (int i = 0; i < 3; i++)
        {
            var oHashlama = new SelectItem();
            oHashlama.Value = i;
            switch (i) 
            {
                case 0: oHashlama.Description =  "אין השלמה";
                    break;
                case 1: oHashlama.Description =  "השלמה לשעה";
                    break;
                case 2: oHashlama.Description =  "השלמה לשעתיים";
                    break;
            }

            Hashlamot.Add(oHashlama);
        }
        return Hashlamot;
    }

    [WebMethod]
    public List<SelectItem> GetTachograf()
    {
        List<SelectItem> Tachgrafs = new List<SelectItem>();
        
        for (int i = 0; i < 3; i++)
        {
            var oTachgraf = new SelectItem();
            oTachgraf.Value = i;
            switch (i) 
            {
                case 0: oTachgraf.Description = "אין צורך בטכוגרף";
                    break;
                case 1: oTachgraf.Description = "חסר טכוגרף";
                    break;
                case 2: oTachgraf.Description = "צירף טכוגרף";
                    break;
            }

            Tachgrafs.Add(oTachgraf);
        }
        return Tachgrafs;
    }

    [WebMethod]
    public List<SelectItem> GetLina()
    {
        List<SelectItem> Linot = new List<SelectItem>();
        var cache = ServiceLocator.Current.GetInstance<IKDSCacheManager>();
        DataRow[] dtLina = cache.GetCacheItem<DataTable>(CachedItems.LookUpTables).Select(string.Concat("table_name='ctb_lina'"));

        foreach (DataRow dr in dtLina)
        {

            var oLina = new SelectItem();
            oLina.Value = ParseStringToInt(dr["KOD"].ToString());
            oLina.Description = dr["TEUR"].ToString();
            Linot.Add(oLina);

        }
        return Linot;

    }

    [WebMethod]
    public List<SelectItem> GetHalbasha()
    {
        List<SelectItem> Halbashot = new List<SelectItem>();
        var cache = ServiceLocator.Current.GetInstance<IKDSCacheManager>();
        DataRow[] dtHalbasha = cache.GetCacheItem<DataTable>(CachedItems.LookUpTables).Select(string.Concat("table_name='ctb_zmaney_halbasha'"));

        foreach (DataRow dr in dtHalbasha)
        {

            var oHalbasha = new SelectItem();
            oHalbasha.Value = ParseStringToInt(dr["KOD"].ToString());
            oHalbasha.Description = dr["TEUR"].ToString();
            Halbashot.Add(oHalbasha);

        }
        return Halbashot;

    }

    [WebMethod]
    public List<SelectItem> GetHashlameLeYom()
    {
        List<SelectItem> Hashlamot = new List<SelectItem>();
        var cache = ServiceLocator.Current.GetInstance<IKDSCacheManager>();
        DataRow[] dtHashlama = cache.GetCacheItem<DataTable>(CachedItems.LookUpTables).Select(string.Concat("table_name='ctb_sibot_hashlama_leyom'"));

        foreach (DataRow dr in dtHashlama)
        {

            var oHashlameLeYom = new SelectItem();
            oHashlameLeYom.Value = ParseStringToInt(dr["KOD"].ToString());
            oHashlameLeYom.Description = dr["TEUR"].ToString();
            Hashlamot.Add(oHashlameLeYom);

        }
        return Hashlamot;

    }
    
    [WebMethod]
    public EmplyeeDetails GetOvedAllDetails(int iMisparIshi, string sCardDate)
    {
       
        clOvdim oOvdim = new clOvdim();
        DataTable dt;
        DateTime dCardDate = DateTime.Parse(sCardDate);
        EmplyeeDetails oEmployee = new EmplyeeDetails();
        try
        {
            if (iMisparIshi > 0)
            {
                        
                dt = oOvdim.GetOvedDetails(iMisparIshi, dCardDate);
                if (dt.Rows.Count > 0)
                {
                    DataRow dr = dt.Rows[0];
                    oEmployee.FullName = dr["FULL_NAME"].ToString();
                    oEmployee.KodMaama = int.Parse( dr["KOD_MAAMAD_HR"].ToString());
                    oEmployee.TeurMaama = dr["TEUR_MAAMAD_HR"].ToString();
                    oEmployee.KodSnif = int.Parse(dr["KOD_SNIF_AV"].ToString());
                    oEmployee.TeurSnif = dr["TEUR_SNIF_AV"].ToString();
                    oEmployee.KodEzor = int.Parse(dr["KOD_EZOR"].ToString());
                    oEmployee.TeurEzor = dr["TEUR_EZOR"].ToString();
                    oEmployee.TeurIsuk = dr["TEUR_ISUK"].ToString(); 
                    oEmployee.TeurHevra = dr["TEUR_HEVRA"].ToString();   
                }
                

            }
            return oEmployee;
        }
        catch (Exception ex)
        {
            throw ex;
        }
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
