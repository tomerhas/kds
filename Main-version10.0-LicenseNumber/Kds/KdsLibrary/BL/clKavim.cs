using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.DAL;
using System.Configuration;
using System.Web;
using System.Diagnostics;
using KDSCommon.Enums;
using KDSCommon.Helpers;
namespace KdsLibrary.BL
{
    public class clKavim
    {
        //public enum enMakatType
        //{
        //    mKavShirut = 1,
        //    mEmpty     = 2,
        //    mNamak     = 3,
        //    mVisut     = 4,
        //    mElement   = 5,
        //    mVisa      = 6
        //}

        //public int GetMakatType(long lMakat)
        //{
        //    int iMakatType = 0;
        //    long lTmpMakat = 0;

        //    lTmpMakat = long.Parse(lMakat.ToString());//.PadRight(8, char.Parse("0")));
        //    if (lTmpMakat.ToString().Substring(0, 1) == "5"  &&  (lTmpMakat >= 50000000))
        //        iMakatType = enMakatType.mVisa.GetHashCode(); //6-Visa
        //    else if ((lTmpMakat>= 100000) &&  (lTmpMakat < 50000000))
        //        iMakatType = enMakatType.mKavShirut.GetHashCode(); //1-kav sherut            
        //    else if ((lTmpMakat >= 60000000) && (lTmpMakat <= 69999999))
        //        iMakatType = enMakatType.mEmpty.GetHashCode(); //2-Empty
        //    else if ((lTmpMakat >= 80000000) && (lTmpMakat <= 99999999))
        //        iMakatType = enMakatType.mNamak.GetHashCode(); //3-Namak
        //    else if ((lTmpMakat >= 70000000) && (lTmpMakat <= 70099999))
        //       iMakatType = enMakatType.mVisut.GetHashCode(); //4-ויסות  
        //       // iMakatType = enMakatType.mElement.GetHashCode();
        //    else if ((lTmpMakat >= 70100000) && (lTmpMakat <= 79999999))
        //        iMakatType = enMakatType.mElement.GetHashCode(); //5-Element  
        //    //else if ((lTmpMakat >= 50000000) && (lTmpMakat <= 59999999))
        //    //    iMakatType = enMakatType.mVisa.GetHashCode(); //6-Visa
        //    return iMakatType;
        //}


        //public DataSet GetKavimDetailsFromTnuaDS(long lMakatNesia, DateTime dDate, out int iResult,int? reciveVisut)
        //{
        //    clTnua oTnua = new clTnua((string)ConfigurationSettings.AppSettings["KDS_TNPR_CONNECTION"]);
        //    // clDal oDal = new clDal();
        //    DataSet ds = new DataSet();

        //    try
        //    {//: מביא נתונים לקווי שירות               
        //        oTnua.AddParameter("p_date", ParameterType.ntOracleDate, DateTime.Parse(dDate.ToShortDateString()), ParameterDir.pdInput);
        //        oTnua.AddParameter("p_makat8", ParameterType.ntOracleInteger, lMakatNesia, ParameterDir.pdInput);
        //        oTnua.AddParameter("rc", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
        //        oTnua.AddParameter("KavDetailsCrs", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
        //        oTnua.AddParameter("EntryCrs", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
        //        oTnua.AddParameter("VisutimCrs", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
        //        oTnua.AddParameter("p_KnisaVisut", ParameterType.ntOracleInteger, reciveVisut, ParameterDir.pdInput );
        //        oTnua.ExecuteSP(clGeneral.cGetKavDetails, ref ds);
        //        //(קוד החזרה : 0 – תקין, 1 – שגיאה)
        //        iResult = int.Parse(oTnua.GetValParam("rc").ToString());
        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        clGeneral.LogMessage("GetKavimDetailsFromTnuaDS: " + ex.Message, EventLogEntryType.Error, clGeneral.enEventId.ProblemOfAccessToTnua.GetHashCode());
        //        throw ex;
        //    }
        //}
        //public DataTable GetKavimDetailsFromTnuaDT(long lMakatNesia, DateTime dDate, out int iResult)
        //{
        //    clTnua oTnua = new clTnua((string)ConfigurationSettings.AppSettings["KDS_TNPR_CONNECTION"]);
        //    DataTable dt = new DataTable();

        //    try
        //    {//: מביא נתונים לקווי שירות               
        //        oTnua.AddParameter("p_date", ParameterType.ntOracleDate, DateTime.Parse(dDate.ToShortDateString()), ParameterDir.pdInput);
        //        oTnua.AddParameter("p_makat_nesia", ParameterType.ntOracleInteger, lMakatNesia, ParameterDir.pdInput);
        //        oTnua.AddParameter("p_rc", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
        //        oTnua.AddParameter("p_Cur1", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
        //        oTnua.AddParameter("p_Cur2", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
        //        oTnua.AddParameter("VisutimCrs", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
        //        oTnua.ExecuteSP(clGeneral.cGetKavDetails, ref dt);
        //        //(קוד החזרה : 0 – תקין, 1 – שגיאה)
        //        if (dt.Rows.Count == 0)
        //        {
        //            iResult = 1;
        //        }
        //        else
        //        {
        //            iResult = int.Parse(oTnua.GetValParam("p_rc").ToString());
        //        }
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        clGeneral.LogMessage("GetKavimDetailsFromTnuaDT: " + ex.Message, EventLogEntryType.Error, clGeneral.enEventId.ProblemOfAccessToTnua.GetHashCode());
               
        //        throw ex;
        //    }
        //}
        //public DataTable GetMakatKavShirut(long lMakatNesia, DateTime dDate )
        //{
        //    clTnua oTnua = new clTnua((string)ConfigurationSettings.AppSettings["KDS_TNPR_CONNECTION"]);
        //    DataTable dt = new DataTable();
        //    //bool bValid = false;

        //    try
        //    {   //מחזיר אם מקט של נסיעת שירות תקינה- 0 תקין           
        //        oTnua.AddParameter("p_date", ParameterType.ntOracleDate, DateTime.Parse(dDate.ToShortDateString()), ParameterDir.pdInput);
        //        oTnua.AddParameter("p_makat_nesia", ParameterType.ntOracleInteger, lMakatNesia, ParameterDir.pdInput);
        //        oTnua.AddParameter("p_rc", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
        //        oTnua.AddParameter("p_Cur1", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
        //        oTnua.AddParameter("p_Cur2", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
        //        oTnua.AddParameter("VisutimCrs", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
        //        oTnua.ExecuteSP(clGeneral.cGetKavDetails, ref dt);

        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        clGeneral.LogMessage("GetMakatKavShirut: " + ex.Message, EventLogEntryType.Error, clGeneral.enEventId.ProblemOfAccessToTnua.GetHashCode());
               
        //        throw ex;
        //    }
        //}
        //public DataTable GetMakatKavNamak(long lMakatNesia, DateTime dDate)
        //{   //מחזיר אם מקט של נסיעת נמ"ק תקינה- 0 תקין
        //    clTnua oTnua = new clTnua((string)ConfigurationSettings.AppSettings["KDS_TNPR_CONNECTION"]);            
        //    DataTable dt = new DataTable();
        //    try
        //    {                 
        //        oTnua.AddParameter("p_date", ParameterType.ntOracleDate, DateTime.Parse(dDate.ToShortDateString()), ParameterDir.pdInput);
        //        oTnua.AddParameter("p_makat_nesia", ParameterType.ntOracleInteger, lMakatNesia, ParameterDir.pdInput);
        //        oTnua.AddParameter("p_rc", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
        //        oTnua.AddParameter("p_Cur1", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);               
        //        oTnua.ExecuteSP(clGeneral.cGetNamakDetails, ref dt);

        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        clGeneral.LogMessage("GetMakatKavNamak: " + ex.Message, EventLogEntryType.Error, clGeneral.enEventId.ProblemOfAccessToTnua.GetHashCode());
               
        //        throw ex;
        //    }
        //}
        //public DataTable GetMakatKavReka(long lMakatNesia, DateTime dDate)
        //{//מחזיר אם מקט של נסיעה ריקה תקינה- 0 תקין
        //    clTnua oTnua = new clTnua((string)ConfigurationSettings.AppSettings["KDS_TNPR_CONNECTION"]);            
        //    DataTable dt = new DataTable();
        //    try
        //    {         
        //        oTnua.AddParameter("p_date", ParameterType.ntOracleDate, DateTime.Parse(dDate.ToShortDateString()), ParameterDir.pdInput);
        //        oTnua.AddParameter("p_makat_nesia", ParameterType.ntOracleInteger, lMakatNesia, ParameterDir.pdInput);
        //        oTnua.AddParameter("p_rc", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
        //        oTnua.AddParameter("p_Cur1", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
        //        oTnua.ExecuteSP(clGeneral.cGetRekaDetails, ref dt);

        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        clGeneral.LogMessage("GetMakatKavReka: " + ex.Message, EventLogEntryType.Error, clGeneral.enEventId.ProblemOfAccessToTnua.GetHashCode());
               
        //        throw ex;
        //    }
        //}
        //public bool IsElementValid(long lMakatNesia, DateTime dDate)
        //{           
        //    bool bValid = true;
        //    DataTable dt;
        //    DataRow[] dr;
        //    long lElementValue = 0;
        //    try
        //    {
        //        //Get peilut meafyeney Elementim
        //        dt = GetMeafyeneyElementByKod(lMakatNesia, dDate);
        //        if (dt.Rows.Count > 0)
        //        {
        //            lElementValue = ((lMakatNesia % 100000) / 100);
        //            dr = dt.Select("kod_meafyen=" + 6);
        //            if (dr.Length > 0)
        //            {
        //                if (!String.IsNullOrEmpty(dr[0]["erech"].ToString()))
        //                {
        //                    if (lElementValue < long.Parse(dr[0]["erech"].ToString()))
        //                    {
        //                        bValid = false;
        //                    }
        //                }
        //            }
        //            dr = dt.Select("kod_meafyen=" + 7);
        //            if (dr.Length > 0)
        //            {
        //                if (!String.IsNullOrEmpty(dr[0]["erech"].ToString()))
        //                {
        //                    if (lElementValue > long.Parse(dr[0]["erech"].ToString()))
        //                    {
        //                        bValid = false;
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            bValid = false;
        //        }
        //        return bValid;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        ////public DataTable GetMeafyeneyElementByKod(long lMakatNesia, DateTime dDate)
        //{
        //    DataTable dt = new DataTable();
        //    clDal oDal = new clDal();            
        //    long lKodElement;
            
        //    try
        //    {
        //        //Get peilut meafyeney Elementim
        //        lKodElement = long.Parse(lMakatNesia.ToString().PadRight(8, (char)48));
        //        lKodElement = (lKodElement % 10000000);
        //        lKodElement = (lKodElement / 100000);

        //        if (lKodElement ==0)
        //        {
        //            oDal.AddParameter("p_kod_element", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
        //        }
        //        else
        //        {
        //            oDal.AddParameter("p_kod_element", ParameterType.ntOracleInteger, lKodElement, ParameterDir.pdInput);
        //        }
        //        oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, DateTime.Parse(dDate.ToShortDateString()), ParameterDir.pdInput);
        //        oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

        //        oDal.ExecuteSP(clGeneral.cProGetDataByKodElement,ref dt);

        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }        
        //}
        //public DataTable GetElementDetails(long lNewMakat)
        //{
        //    clDal _Dal = new clDal();
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        _Dal.AddParameter("p_kod_element", ParameterType.ntOracleLong, int.Parse(lNewMakat.ToString().Substring(1,2)), ParameterDir.pdInput);
        //        _Dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
        //        _Dal.ExecuteSP(clGeneral.cProGetElementDetails, ref dt);
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }            
        //}
        //public DataTable GetVisutDetails(long lNewMakat)
        //{
        //    clDal _Dal = new clDal();
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        _Dal.AddParameter("p_kod_visut", ParameterType.ntOracleLong, int.Parse(lNewMakat.ToString().Substring(3,3)), ParameterDir.pdInput);
        //        _Dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
        //        _Dal.ExecuteSP(clGeneral.cProGetVisutDetails, ref dt);
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
                
        //        throw ex;
        //    }            
        //}
        
        //public DataTable GetMakatDetails(long lNewMakat, DateTime dDate)
        //{            
        //    DataTable dt = new DataTable();
        //    enMakatType oMakatType;
        //    try
        //    {
        //        oMakatType = (enMakatType)StaticBL.GetMakatType(lNewMakat);
        //        switch (oMakatType)
        //        {
        //            case enMakatType.mKavShirut:
        //                dt = GetMakatKavShirut(lNewMakat, dDate);                        
        //                break;
        //            case enMakatType.mNamak:
        //                dt = GetMakatKavNamak(lNewMakat, dDate);// ? "1" : "0";
        //                break;
        //            case enMakatType.mEmpty:
        //                dt = GetMakatKavReka(lNewMakat, dDate);// ? "1" : "0";
        //                break;
        //            case enMakatType.mElement:
        //                //אלמנט ייבדק מוך טבלת CTB_ELEMENTIM
        //               // sResult = IsElementValid(lNewMakat, dDate) ? "1" : "0";
        //                dt = GetElementDetails(lNewMakat);
        //                break;
        //            case enMakatType.mVisut:
        //                dt = GetVisutDetails(lNewMakat);
        //                break;
        //            default:                        
        //                break;
        //        }
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public string CheckMakatValidity(long lNewMakat, DateTime dDate)
        //{
        //    string sResult="";

        //    enMakatType oMakatType;
        //    try
        //    {
        //        oMakatType = (enMakatType)StaticBL.GetMakatType(lNewMakat);
        //        switch (oMakatType)
        //        {
        //            case enMakatType.mKavShirut:
        //                //sResult = IsMakatKavShirutValid(lNewMakat, dDate) ? "1" : "0";
        //                break;
        //            case enMakatType.mNamak:
        //               // sResult = IsMakatKavNamakValid(lNewMakat, dDate) ? "1" : "0";                       
        //                break;
        //            case enMakatType.mEmpty:
        //               // sResult = IsMakatKavRekaValid(lNewMakat, dDate) ? "1" : "0";                        
        //                break;
        //            case enMakatType.mElement:
        //                //אלמנט ייבדק מוך טבלת CTB_ELEMENTIM
        //              //  sResult = IsElementValid(lNewMakat, dDate) ? "1" : "0";                       
        //                break;
        //            case enMakatType.mVisut:
        //                sResult = "1";
        //                break;
        //            default:
        //                sResult = "0";
        //                break;
        //        }
        //        return sResult;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        //public DataTable GetMakatRekaDetailsFromTnua(long lMakatNesia, DateTime dDate, out int iResult)
        //{
        //    clTnua oTnua = new clTnua((string)ConfigurationSettings.AppSettings["KDS_TNPR_CONNECTION"]);
        //    DataTable dt = new DataTable();

        //    try
        //    {//מביא נתונים למקטים מסוג קו ריק                
        //        oTnua.AddParameter("p_date", ParameterType.ntOracleDate, DateTime.Parse(dDate.ToShortDateString()), ParameterDir.pdInput, 100);
        //        oTnua.AddParameter("p_makat_nesia", ParameterType.ntOracleInteger, lMakatNesia, ParameterDir.pdInput);
        //        oTnua.AddParameter("p_rc", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
        //        oTnua.AddParameter("p_Cur1", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

        //        oTnua.ExecuteSP(clGeneral.cGetRekaDetails, ref dt);
        //        //(קוד החזרה : 0 – תקין, 1 – שגיאה)
        //        if (dt.Rows.Count == 0)
        //        {
        //            iResult = 1;
        //        }
        //        else
        //        {
        //            iResult = int.Parse(oTnua.GetValParam("p_rc").ToString());
        //        }
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        clGeneral.LogMessage("GetMakatRekaDetailsFromTnua: " + ex.Message, EventLogEntryType.Error, clGeneral.enEventId.ProblemOfAccessToTnua.GetHashCode());
               
        //        throw ex;
        //    }
        //}


        //public DataTable GetMakatNamakDetailsFromTnua(long lMakatNesia, DateTime dDate, out int iResult)
        //{
        //    clTnua oTnua = new clTnua((string)ConfigurationSettings.AppSettings["KDS_TNPR_CONNECTION"]);
        //    DataTable dt = new DataTable();

        //    try
        //    {//מביא נתונים למקטים מסוג נמ"ק                
        //        oTnua.AddParameter("p_date", ParameterType.ntOracleDate, DateTime.Parse(dDate.ToShortDateString()), ParameterDir.pdInput);
        //        oTnua.AddParameter("p_makat_nesia", ParameterType.ntOracleInteger, lMakatNesia, ParameterDir.pdInput);
        //        oTnua.AddParameter("p_rc", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
        //        oTnua.AddParameter("p_Cur1", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

        //        oTnua.ExecuteSP(clGeneral.cGetNamakDetails, ref dt);
        //        //(קוד החזרה : 0 – תקין, 1 – שגיאה)]
        //        if (dt.Rows.Count == 0)
        //        {
        //            iResult = 1;
        //        }
        //        else
        //        {
        //            iResult = int.Parse(oTnua.GetValParam("p_rc").ToString());
        //        }
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        clGeneral.LogMessage("GetMakatNamakDetailsFromTnua: " + ex.Message, EventLogEntryType.Error, clGeneral.enEventId.ProblemOfAccessToTnua.GetHashCode());
               
        //        throw ex;
        //    }
        //}

        //public bool IsBusNumberValid(long lOtoNo, DateTime dCardDate)
        //{
        //    clDal oDal = new clDal();           
        //    string sCacheKey = lOtoNo + dCardDate.ToShortDateString();
        //    try
        //    {   //בודק מול מש"ר אם מספר רכב תקין:   
        //        //0-תקין 
        //        //1- שגיאה
        //        //2- לא אותר

        //        if (HttpRuntime.Cache.Get(sCacheKey) == null || HttpRuntime.Cache.Get(sCacheKey).ToString()=="")
        //        {
        //            oDal.AddParameter("p_result", ParameterType.ntOracleInteger, null, ParameterDir.pdReturnValue);
        //            oDal.AddParameter("p_oto_no", ParameterType.ntOracleLong, lOtoNo, ParameterDir.pdInput);
        //            oDal.AddParameter("p_date", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
        //            oDal.ExecuteSP(clGeneral.cFnIsOtoNumberExists);
        //            HttpRuntime.Cache.Insert(sCacheKey, int.Parse(oDal.GetValParam("p_result").ToString()), null, DateTime.MaxValue, TimeSpan.FromMinutes(1440));
        //            return int.Parse(oDal.GetValParam("p_result").ToString()) == 0;
        //        }
        //        else return  HttpRuntime.Cache.Get(sCacheKey).ToString().Trim() == "0";                 
        //        //    return  (int)HttpRuntime.Cache.Get(sCacheKey) == 0;                                  
        //    }
        //    catch (Exception ex)
        //    {
        //        clGeneral.LogMessage("IsBusNumberValid: " + ex.Message, EventLogEntryType.Error, clGeneral.enEventId.ProblemOfAccessToTnua.GetHashCode());
               
        //        throw ex;
        //    }
        //}
        //public void GetBusLicenseNumber(long lOtoNo, ref long lLicenseNumber)
        //{
        //    clDal oDal = new clDal();

        //    try
        //    {   //בדיקה אם קיים מספר רכב במש"ר ואם כן מחזיר מספר רישוי:                                 
        //        oDal.AddParameter("p_oto_no", ParameterType.ntOracleLong, lOtoNo, ParameterDir.pdInput);
        //        oDal.AddParameter("p_license_no", ParameterType.ntOracleLong, lLicenseNumber, ParameterDir.pdOutput,200);

        //        oDal.ExecuteSP(clGeneral.cProGetMasharBusLicenseNum);

        //        if (string.IsNullOrEmpty(oDal.GetValParam("p_license_no")))
        //        {
        //            lLicenseNumber = 0;
        //        }
        //        else
        //        {
        //            lLicenseNumber = long.Parse(oDal.GetValParam("p_license_no").ToString());
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clGeneral.LogMessage("GetBusLicenseNumber: " + ex.Message, EventLogEntryType.Error, clGeneral.enEventId.ProblemOfAccessToTnua.GetHashCode());
               
        //        throw ex;
        //    }
        //}

        //public DataTable GetSidurDetailsFromTnua(int iMisparSidur, DateTime dDate, out int iResult)
        //{
        //    clTnua oTnua = new clTnua((string)ConfigurationSettings.AppSettings["KDS_TNPR_CONNECTION"]);
        //    DataSet ds = new DataSet();
        //    DataTable dt = new DataTable();
        //    try
        //    {//: מביא נתונים לסידור               
        //        oTnua.AddParameter("p_date", ParameterType.ntOracleVarchar, dDate.ToShortDateString(), ParameterDir.pdInput,100);
        //        oTnua.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger, iMisparSidur, ParameterDir.pdInput);                
        //        oTnua.AddParameter("p_Cur1", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
        //        oTnua.AddParameter("p_Cur2", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
        //        oTnua.AddParameter("p_rc", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
        //        oTnua.AddParameter("p_KnisaVisut", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);

        //        oTnua.ExecuteSP(clGeneral.cGetSidurDetails, ref dt);
        //        //(קוד החזרה : 0 – תקין, 1 – שגיאה)
        //        if (dt.Rows.Count == 0)
        //        {
        //            iResult = 1;
        //        }
        //        else
        //        {
        //            iResult = int.Parse(oTnua.GetValParam("p_rc").ToString());
        //        }
        //        return dt; 
        //    }
        //    catch (Exception ex)
        //    {
        //        clGeneral.LogMessage("GetSidurDetailsFromTnua: " + ex.Message, EventLogEntryType.Error, clGeneral.enEventId.ProblemOfAccessToTnua.GetHashCode());
               
        //        throw ex;
        //    }
        //}
        //public DataSet GetSidurAndPeiluyotFromTnua(int iMisparSidur, DateTime dDate,int? iKnisaVisut, out int iResult)
        // {    
        //   // int ?iKnisaVisut,    
        //    DataSet ds = new DataSet();
        //    clTxDal oTxDal = new clTxDal((string)ConfigurationSettings.AppSettings["KDS_TNPR_CONNECTION"]);
           
        //    try
        //    {//: מביא נתונים לסידור      
        //        oTxDal.TxBegin();
        //        oTxDal.AddParameter("p_date", ParameterType.ntOracleVarchar, dDate.ToShortDateString(), ParameterDir.pdInput, 100);
        //        oTxDal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger, iMisparSidur, ParameterDir.pdInput);
        //        oTxDal.AddParameter("p_Cur1", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
        //        oTxDal.AddParameter("p_Cur2", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
        //        oTxDal.AddParameter("p_rc", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
        //        oTxDal.AddParameter("p_KnisaVisut", ParameterType.ntOracleInteger, iKnisaVisut, ParameterDir.pdInput);

        //        oTxDal.ExecuteSP(clGeneral.cGetSidurDetails, ref ds);
        //        //(קוד החזרה : 0 – תקין, 1 – שגיאה)
        //        if (ds.Tables[0].Rows.Count == 0)
        //        {
        //            iResult = 1;
        //        }
        //        else
        //        {
        //            iResult = int.Parse(oTxDal.GetValParam("p_rc").ToString());
        //        }
        //        oTxDal.TxCommit();
        //        return ds;
        //    }
        //    catch (Exception ex)
        //    {
        //        clGeneral.LogMessage("GetSidurAndPeiluyotFromTnua: " + ex.Message, EventLogEntryType.Error, clGeneral.enEventId.ProblemOfAccessToTnua.GetHashCode());
               
        //        oTxDal.TxRollBack();
        //        throw ex;
        //    }
        //}

        //public DataTable GetMasharData(string sCarsNumbers)
        //{
        //    clDal oDal = new clDal();
        //    DataTable dt = new DataTable();

        //    try
        //    {   //מביא את נתוני מש"ר: 
        //        oDal.AddParameter("p_cars_number", ParameterType.ntOracleVarchar, sCarsNumbers, ParameterDir.pdInput, 500);
        //        oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
        //        oDal.ExecuteSP(clGeneral.cProGetMasharData, ref dt);
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        clGeneral.LogMessage("GetMasharData: " + ex.Message, EventLogEntryType.Error, clGeneral.enEventId.ProblemOfAccessToTnua.GetHashCode());
               
        //        throw ex;
        //    }            
        //}

        //public DataTable GetKatalogKavim(int iMisparIshi, DateTime dFromDate, DateTime dToDate)
        //{
        //    clTxDal _Dal = new clTxDal();
        //    DataTable dt = new DataTable();
        //    //הפרוצדורה מחזירה את כל נתוני הפעילויות מהתנועה, למספר אישי ולטווח תאריכים נתון
        //    try
        //    {
        //        _Dal.TxBegin();
        //        _Dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
        //        _Dal.AddParameter("p_date_from", ParameterType.ntOracleDate, dFromDate, ParameterDir.pdInput, 100);
        //        _Dal.AddParameter("p_date_to", ParameterType.ntOracleDate, dToDate, ParameterDir.pdInput, 100);
        //        _Dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
        //        _Dal.ExecuteSP(clGeneral.cGetKatalogKavim, ref dt);
        //        _Dal.TxCommit();
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        clGeneral.LogMessage("GetKatalogKavim: " + ex.Message, EventLogEntryType.Error, clGeneral.enEventId.ProblemOfAccessToTnua.GetHashCode());
               
        //        _Dal.TxRollBack();
        //        throw ex;
        //    }
        //}

        //public DataTable GetRekaDetailsByXY(DateTime dDate, long lMokedStart, long lMokedEnd, out int iResult)
        //{
        //    clTnua _Tnua = new clTnua((string)ConfigurationSettings.AppSettings["KDS_TNPR_CONNECTION"]);
        //    DataTable dt = new DataTable();
        //    //הפרוצדורה מחזירה את כל נתוני הפעילויות מהתנועה, למספר אישי ולטווח תאריכים נתון
        //    try
        //    {
        //        _Tnua.AddParameter("p_date", ParameterType.ntOracleDate, dDate, ParameterDir.pdInput);
        //        _Tnua.AddParameter("p_mokedTchila", ParameterType.ntOracleLong, lMokedStart, ParameterDir.pdInput, 100);
        //        _Tnua.AddParameter("p_mokedsiyum", ParameterType.ntOracleLong, lMokedEnd, ParameterDir.pdInput, 100);
        //        _Tnua.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
        //        _Tnua.AddParameter("p_rc", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
        //        _Tnua.ExecuteSP(clGeneral.cGetRekaDetailsByXY, ref dt);

        //        //(קוד החזרה : 0 – תקין, 1 – שגיאה)
        //        iResult = int.Parse(_Tnua.GetValParam("p_rc").ToString());
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        clGeneral.LogMessage("GetRekaDetailsByXY: " + ex.Message, EventLogEntryType.Error, clGeneral.enEventId.ProblemOfAccessToTnua.GetHashCode());
               
        //        throw ex;
        //    }
        //}

        //public int CheckHityazvutNehag(int? iNekudatTziyunTnua, int iRadyus,int iSnif, int? iMikumShaon, int? iMisparSiduriShaon)
        //{
        //    clTnua oTnua = new clTnua((string)ConfigurationSettings.AppSettings["KDS_TNPR_CONNECTION"]);
           
        //    try
        //   {
        //        //מחזיר קוד התייצבות נהג
        //        //0	פטור מהחתמה
        //        //1	החתמה תקינה
        //        //2	החתמה לא תקינה 
        //        //3	חסרה החתמה 
        //        //4	מיקום שעון לא קיים בטבלה 
        //        //9	תקלה  

        //       oTnua.AddParameter("p_Kod_hityazvut", ParameterType.ntOracleInteger, null, ParameterDir.pdReturnValue);
        //       oTnua.AddParameter("p_radius", ParameterType.ntOracleInteger, iRadyus, ParameterDir.pdInput);
        //       oTnua.AddParameter("p_branch_id", ParameterType.ntOracleInteger, iSnif, ParameterDir.pdInput);
                
        //        if (iMikumShaon.HasValue)
        //        {
        //            oTnua.AddParameter("p_clock_location", ParameterType.ntOracleInteger, iMikumShaon, ParameterDir.pdInput);
        //        }
        //        else
        //        {
        //            oTnua.AddParameter("p_clock_location", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
        //        }

        //        if (iMisparSiduriShaon.HasValue)
        //        {
        //            oTnua.AddParameter("p_clock_number", ParameterType.ntOracleInteger, iMisparSiduriShaon, ParameterDir.pdInput);
        //        }
        //        else
        //        {
        //            oTnua.AddParameter("p_clock_number", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
        //        }

        //        if (iNekudatTziyunTnua.HasValue)
        //        {
        //            oTnua.AddParameter("p_xy", ParameterType.ntOracleInteger, iNekudatTziyunTnua, ParameterDir.pdInput);
        //        }
        //        else
        //        {
        //            oTnua.AddParameter("p_xy", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
        //        }

        //        oTnua.ExecuteSP(clGeneral.cCheckHityatzvutNehag);

        //        return int.Parse(oTnua.GetValParam("p_Kod_hityazvut").ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        clGeneral.LogMessage("CheckHityazvutNehag: " + ex.Message, EventLogEntryType.Error, clGeneral.enEventId.ProblemOfAccessToTnua.GetHashCode());
               
        //        throw ex;
        //    }
        //}

        //public DataTable GetMakatimLeTkinut(DateTime dTaarich)
        //{
        //    DataTable dt = new DataTable();
        //    clDal dal = new clDal();
        //    try
        //    {
        //        dal.AddParameter("p_date", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
        //        dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
        //        dal.ExecuteSP(clGeneral.cProGetMakatimLeTkinut, ref dt);
        //    }
        //    catch (Exception ex)
        //    {
        //        //dt = null;
               
        //        throw (ex);
        //    }
        //    return dt;
        //}
        //public DataTable GetBusesDetailsLeOvedForMonth(DateTime dTarMe, DateTime dTarAd, int iMispar_ishi)
        //{
        //    clDal oDal = new clDal();
        //    DataTable dt = new DataTable();

        //    try
        //    {   //מביא את נתוני מש"ר: 
        //        oDal.AddParameter("p_tar_me", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput);
        //        oDal.AddParameter("p_tar_ad", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput);
        //        oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMispar_ishi, ParameterDir.pdInput);
        //        oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
        //        oDal.ExecuteSP(clGeneral.cProGetBusesDetails, ref dt);
        //        return dt;
        //    }
        //    catch (Exception ex)
        //    {
        //        clGeneral.LogMessage("GetBusesDetailsLeOvedForMonth: " + ex.Message, EventLogEntryType.Error, clGeneral.enEventId.ProblemOfAccessToTnua.GetHashCode());
               
        //        throw ex;
        //    }
        //}
    }
}
