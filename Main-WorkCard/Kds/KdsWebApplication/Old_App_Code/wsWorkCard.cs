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
using KdsLibrary.Security;
using KDSCommon.Helpers;
using KDSCommon.Interfaces.DAL;

/// <summary>
/// This is the new service required for the new angular workcard screen
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class wsWorkCard : System.Web.Services.WebService
{
    public static string[] arrDays = new string[] { "א", "ב", "ג", "ד", "ה", "ו", "ש" };

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
    public string GetUserWorkCard(int misparIshi, DateTime cardDate) //GetEmployeePeiluyot
    {
        cardDate = DateTime.Parse(cardDate.ToShortDateString());
        IWorkCardSidurManager ovedWCManager = ServiceLocator.Current.GetInstance<IWorkCardSidurManager>();
        var container=ovedWCManager.GetUserWorkCard(misparIshi, cardDate);
        //return sidurim;
        string json = JsonConvert.SerializeObject(container);
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
                
                  ids.Add(new EmployeeIdContainer(){misparIshi = dr["mispar_ishi"].ToString()});
                  
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
    public List<EmployeeNameContainer> GetOvdimToUserByName(string inputstring, string userId)
    {   //מביא את כל המספרים האישיים  של העובדים הכפופים

        clOvdim oOvdim = new clOvdim();
        List<EmployeeNameContainer> names = new List<EmployeeNameContainer>();
        try
        {

            DataTable dt = oOvdim.GetOvdimByName(inputstring + "%", null);

            foreach (DataRow dr in dt.Rows)
            {

                names.Add(new EmployeeNameContainer() { employeeName = dr["Oved_Name"].ToString() });

            }
            return names;


        }
        //try
        //{
        //    prefixText = string.Concat(prefixText, "%");
        //    if (contextKey.Length > 0)
        //    {
        //        dt = oOvdim.GetOvdimToUserByName(prefixText, int.Parse(contextKey));
        //    }
        //    else
        //    {
        //        dt = oOvdim.GetOvdimByName(prefixText, contextKey);
        //    }
        //    List<string> items = new List<string>(count);

        //    int i = 0;
        //    foreach (DataRow dr in dt.Rows)
        //    {
        //        if (i > count) { break; }
        //        items.Add(dr["Oved_Name"].ToString());
        //        i++;

        //    }
        //    return items.ToArray();
        //}
        catch (Exception ex)
        {
            throw ex;
        }
    }
    

    [WebMethod]
    public EmployeeBasicDetails GetEmpoyeeById(int iMisparIshi, string sCardDate)
    {
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
                    oEmployee.MisparIshi = iMisparIshi;
                    oEmployee.DetailsDate = dCardDate.ToShortDateString();
                    oEmployee.FullName = dr["FULL_NAME"].ToString();
                    oEmployee.KodMaamad = !string.IsNullOrEmpty(dr["KOD_MAAMAD_HR"].ToString()) ? int.Parse( dr["KOD_MAAMAD_HR"].ToString()):0;
                    oEmployee.TeurMaamad = dr["TEUR_MAAMAD_HR"].ToString();
                    oEmployee.KodSnif = !string.IsNullOrEmpty(dr["KOD_SNIF_AV"].ToString()) ? int.Parse(dr["KOD_SNIF_AV"].ToString()) : 0; 
                    oEmployee.TeurSnif = dr["TEUR_SNIF_AV"].ToString();
                    oEmployee.KodEzor = !string.IsNullOrEmpty(dr["KOD_EZOR"].ToString()) ? int.Parse(dr["KOD_EZOR"].ToString()) : 0; 
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

    [WebMethod(EnableSession = true)]
    public string IsCardExists(int iMisparIshi, DateTime dWorkCard)
    {
        clWorkCard _WorkCard = new clWorkCard();
     //   DateTime dWorkCard;
        int iCardCount;
        //נבדוק שני דברים:
        //1. אם החודש שנבחר נמצא בטווח של פרמטר 100
        //2. במידה ואחד מתקיים נבדוק אם קיים כרטיס לתאריך זה
        //dWorkCard = DateTime.Parse(sWorkCard);
        var oParams = getObjectParams(dWorkCard);
        int iMaxMonthToDisplay = oParams.iMaxMonthToDisplay;

        if ((Math.Abs((DateTime.Now.Month - dWorkCard.Month) + 12 * ((DateTime.Now.Year - dWorkCard.Year))) + 1 <= iMaxMonthToDisplay))
        {
            iCardCount = _WorkCard.GetIsCardExistsInYemeyAvodaOvdim(iMisparIshi, dWorkCard);
            return iCardCount.ToString() + "|" + arrDays[dWorkCard.DayOfWeek.GetHashCode()];
        }
        else
            return "0|0";
    }

    private clParametersDM getObjectParams(DateTime dCardDate)
    {
        var cacheAge = ServiceLocator.Current.GetInstance<IKDSAgedQueueParameters>();
        var oParam = cacheAge.GetItem(dCardDate);
        if (oParam == null)
        {
            var cache = ServiceLocator.Current.GetInstance<IKDSCacheManager>();
            var dtYamimMeyuchadim = cache.GetCacheItem<DataTable>(CachedItems.YamimMeyuhadim);
            var dtSugeyYamimMeyuchadim = cache.GetCacheItem<DataTable>(CachedItems.SugeyYamimMeyuchadim);
            var iSugYom = DateHelper.GetSugYom(dtYamimMeyuchadim, dCardDate, dtSugeyYamimMeyuchadim);//, _oMeafyeneyOved.GetMeafyen(56).IntValue);

            IParametersManager paramManager = ServiceLocator.Current.GetInstance<IParametersManager>();
            oParam = paramManager.CreateClsParametrs(dCardDate, iSugYom);
            cacheAge.Add(oParam, dCardDate);
        }
        return oParam;

    }
    [WebMethod]
    public WorkCardLookupsContainer GetWorkCardLookups()
    {
        IWorkCardSidurManager ovedWCManager = ServiceLocator.Current.GetInstance<IWorkCardSidurManager>();
        var res= ovedWCManager.GetWorkCardLookups();
        string json = JsonConvert.SerializeObject(res);
        return res;
    }
    [WebMethod]
    public string GetNextErrorCardDate(int iMisparIshi, DateTime dWorkCard)
    {

        clWorkCard _WorkCard = new clWorkCard();
        return clWorkCard.GetNextErrorCard(iMisparIshi, dWorkCard).ToShortDateString();

    }

    [WebMethod]
    public string CheckOtoNo(long lOtoNo)
    {
        long lLicenseNumber = 0;

        try
        {
            if (lOtoNo > 0)
            {
                //בודק אם מספר רכב קיים בתנועה ואם כן מחזיר מספר רישוי
                var kavimDal = ServiceLocator.Current.GetInstance<IKavimDAL>();
                kavimDal.GetBusLicenseNumber(lOtoNo, ref lLicenseNumber);
            }
            return lLicenseNumber.ToString();
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    [WebMethod]
    public string GetKnisaActualMin(long lMakat, string sSidurDate, int iMisaprKnisa)
    {
        DataSet dsMakat;
        int iResult;
        var kavimdDal = ServiceLocator.Current.GetInstance<IKavimDAL>();
        dsMakat = kavimdDal.GetKavimDetailsFromTnuaDS(lMakat, DateTime.Parse(sSidurDate), out iResult, 1);
        if (dsMakat.Tables[1].Rows.Count > 0)
            return dsMakat.Tables[1].Rows[iMisaprKnisa - 1]["mazan"].ToString();
        else
            return "0";
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
