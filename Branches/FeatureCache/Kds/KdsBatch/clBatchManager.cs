using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Specialized;
using KdsLibrary.BL;
using System.Collections;
using KdsLibrary.Utils;
using KdsLibrary;
using KdsWorkFlow.Approvals;
using System.Web;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.DataModels;
using KDSCommon.Interfaces;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KDSCommon.Interfaces.Managers;
using KDSCommon.Helpers;
using KDSCommon.Interfaces.DAL;
using KDSCommon.Interfaces.Errors;
using KDSCommon.UDT;
using DalOraInfra.DAL;
using ObjectCompare;

namespace KdsBatch
{
    public class clBatchManager : IDisposable
    {
        
        private clParametersDM _oParameters;
        private DataTable _dtLookUp;
        private DataTable _dtYamimMeyuchadim;
        private DataTable _dtSugeyYamimMeyuchadim;
        private MeafyenimDM _oMeafyeneyOved;
        private OvedYomAvodaDetailsDM _oOvedYomAvodaDetails;
        private DataTable _dtDetails;       
        private DataTable _dtMatzavOved;
        private DataTable _dtSugSidur;
        private OrderedDictionary _htEmployeeDetails;
        private OrderedDictionary _htFullEmployeeDetails;
        private OrderedDictionary _htSpecialEmployeeDetails;
        //private OrderedDictionary _htEmployeeDetailsWithCancled;
        private int _iLoginUserId = 0;
        private DataTable _dtErrors;
        private DataTable _dtSidurimMeyuchadim;
        private DataTable _dtMeafyeneyElements;
        private int _iSugYom;
        private int _iMisparIshi;
        private DateTime _dCardDate;
        private bool _IsExecuteInputData = false;
        private bool _IsExecuteErrors = false;
        private long? _btchRequest;
        private int _iUserId;
        private bool _bSuccsess;
        private CardStatus _CardStatus = CardStatus.Valid;
        private DataTable _dtIdkuneyRashemet;
        private DataTable _dtApprovalError;
        private DataTable _dtErrorsNotActive;
        private DateTime dTarTchilatMatzav=DateTime.MinValue;
        private bool _bHaveCount = true;

        private DataRow drNew;       
        private int iLastMisaprSidur;
        //private clParameters oParam;
        //private clOvedYomAvoda oOvedYomAvodaDetails;
        //private clMeafyenyOved oMeafyeneyOved;

        //שינויי קלט
        private DataTable dtMutamut;
        private DataTable dtSibotLedivuachYadani;
        public DataTable dtMashar;
        private OBJ_SIDURIM_OVDIM oObjSidurimOvdimDel;
        private OBJ_SIDURIM_OVDIM oObjSidurimOvdimIns;
        private OBJ_PEILUT_OVDIM oObjPeilutOvdimUpd;
        private OBJ_PEILUT_OVDIM oObjPeilutOvdimDel;
        private OBJ_YAMEY_AVODA_OVDIM oObjYameyAvodaUpd;
        private ModificationRecorder<OBJ_YAMEY_AVODA_OVDIM> oObjYameyAvodaUpdWrapper; // = new ModificationRecorder<OBJ_YAMEY_AVODA_OVDIM>(obj);

        private COLL_SIDURIM_OVDIM oCollSidurimOvdimUpd; //= new COLL_SIDURIM_OVDIM();
        private COLL_SIDURIM_OVDIM oCollSidurimOvdimIns; //= new COLL_SIDURIM_OVDIM();
        private COLL_SIDURIM_OVDIM oCollSidurimOvdimDel;// = new COLL_SIDURIM_OVDIM();
        private COLL_YAMEY_AVODA_OVDIM oCollYameyAvodaUpd;// = new COLL_YAMEY_AVODA_OVDIM();
        private COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimDel; //= new COLL_OBJ_PEILUT_OVDIM();
        private COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimUpd; //= new COLL_OBJ_PEILUT_OVDIM();
        private COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimIns; // new COLL_OBJ_PEILUT_OVDIM();
        private COLL_IDKUN_RASHEMET _oCollIdkunRashemet;
        private COLL_SHGIOT_MEUSHAROT _oCollApprovalErrors;
   
        private const int SIDUR_NESIA = 99300;
        private const int SIDUR_MATALA = 99301;
        private const int SIDUR_HEADRUT_BETASHLUM = 99801;
        private const int SIDUR_RETIZVUT99500 = 99500;
        private const int SIDUR_RETIZVUT99501 = 99501;
        private const string SIBA_LE_DIVUCH_YADANI_HALBASHA = "goremet_lebitul_zman_halbasha";
        private const string SIBA_LE_DIVUCH_YADANI_NESIAA = "goremet_lebitul_zman_nesiaa";
        OrderedDictionary htNewSidurim;// = new OrderedDictionary();//יכיל את מפתחות הסידורים שהוחלף להם מספר הסידור בעקבות שינוי מספר 1
        private bool _bHaveShgiotLetzuga = false;

        public enum ApprovalCode
        {
            Code1 = 1,
            Code3 = 3
        }

     

        public enum ZmanNesiotType
        {
            ZakaiKnisa = 1,
            ZakaiYetiza = 2,
            ZakaiKnisaYetiza = 3,
            LoZakai = 4
        }
        public enum ZmanHalbashaType
        {
            ZakaiKnisa = 1,
            ZakaiYetiza = 2,
            ZakaiKnisaYetiza = 3,
            LoZakai = 4,
            CardError = 5 //  שגוי סטטוס 
        }
         

        private enum errNesiaMeshtana
        {
            errNesiaMeshtanaNotDefineForKnisa=1,
            errNesiaMeshtanaNotDefineForYetiza=2,
            errNesiaMeshtanaNotDefineForAll=3,
            enDefineAll=0
        }

        public clBatchManager(long? btchRequest,int iMisparIshi, DateTime dCardDate)
        {
            _btchRequest = btchRequest;
            _iMisparIshi = iMisparIshi;
            _dCardDate = dCardDate;
            _iUserId = -2;
        }

        public clBatchManager( int iMisparIshi, DateTime dCardDate)
        {
            _iMisparIshi = iMisparIshi;
            _dCardDate = dCardDate;
            _iUserId = -2;
        }

        public clBatchManager(long? btchRequest)
        {
            _btchRequest = btchRequest;
            _iUserId = -2;
        }

        public clBatchManager()
        {

        }

      
        public void InitGeneralData()
        {
            int iSugYom;
            int iLastMisaprSidur=0;
            clUtils oUtils = new clUtils();
            clDefinitions oDefinition = new clDefinitions();
            try
            {
               

                //Get Parameters Table
                //dtParameters = GetKdsParametrs();
                var cache = ServiceLocator.Current.GetInstance<IKDSCacheManager>();

                dtLookUp = cache.GetCacheItem<DataTable>(CachedItems.LookUpTables);
                dtYamimMeyuchadim = cache.GetCacheItem<DataTable>(CachedItems.YamimMeyuhadim);

                _dtSugeyYamimMeyuchadim = cache.GetCacheItem<DataTable>(CachedItems.SugeyYamimMeyuchadim);

                //Get Meafyeny Ovdim
                GetMeafyeneyOvdim(_iMisparIshi, _dCardDate);

                iSugYom = DateHelper.GetSugYom(dtYamimMeyuchadim, _dCardDate, _dtSugeyYamimMeyuchadim);//, _oMeafyeneyOved.GetMeafyen(56).IntValue);

                //Set global variable with parameters
                SetParameters(_dCardDate, iSugYom);

                
                //Get Meafyeney Sug Sidur
                dtSugSidur = cache.GetCacheItem<DataTable>(CachedItems.SugeySidur);

                SetOvedYomAvodaDetails(_iMisparIshi, _dCardDate);

                if (oOvedYomAvodaDetails!=null)
                {
                    //Get Oved Details
                    dtDetails = oDefinition.GetOvedDetails(_iMisparIshi, _dCardDate);
                    if (dtDetails.Rows.Count > 0)
                    {
                        OrderedDictionary htFullSidurimDetails = new OrderedDictionary();
                        //Insert Oved Details to Class
                        htEmployeeDetails = oDefinition.InsertEmployeeDetails(false, dtDetails, _dCardDate, ref iLastMisaprSidur, out _htSpecialEmployeeDetails, ref htFullSidurimDetails);//, out  _htEmployeeDetailsWithCancled
                        htFullEmployeeDetails = htFullSidurimDetails;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
        
       
        private void SetOvedYomAvodaDetails(int iMisparIshi, DateTime dCardDate)
        {
            try{

                IOvedManager ovedManager = ServiceLocator.Current.GetInstance<IOvedManager>();
                oOvedYomAvodaDetails = ovedManager.CreateOvedDetails(iMisparIshi, dCardDate);
                   
                  _CardStatus =(CardStatus)oOvedYomAvodaDetails.iStatus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
           
        }

        public  bool CheckErevChag(int iSugYom)
        {
            if (_dtSugeyYamimMeyuchadim.Select("sug_yom=" + iSugYom).Length > 0)
            {
                return (_dtSugeyYamimMeyuchadim.Select("sug_yom=" + iSugYom)[0]["EREV_SHISHI_CHAG"].ToString() == "1") ? true : false;

            }
            else return false;
        }

        private bool CheckEggedHourValid(string sHour)
        {
            string[] arr;
            bool bError = false;
            //מקבל מחרוזת בפורמט XX:XX ומחזיר שגיאה אם לא בין 00:00 ל31:59-
            try
            {
                arr = sHour.Split(char.Parse(":"));
                if (!((int.Parse(arr[0])) >= 0 && (int.Parse(arr[0])) <= 31))
                {
                    bError = true;
                }
                if (!((int.Parse(arr[1])) >= 0 && (int.Parse(arr[1])) <= 59))
                {
                    bError = true;
                }

                return bError;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CheckHourValid(string sHour)
        {
            string[] arr;
            bool bValid = true;
            //מקבל מחרוזת בפורמט XX:XX ומחזיר שגיאה אם לא בין 00:01 ל23:59-
            try
            {
                if (sHour.Length > 0)
                {
                    arr = sHour.Split(char.Parse(":"));
                    if (!((int.Parse(arr[0])) >= 0 && (int.Parse(arr[0])) <= 23))
                    {
                        bValid = false;
                    }
                    if (!(int.Parse(arr[1]) >= 1 || (int.Parse(arr[1])==0 &&  int.Parse(arr[0]) >= 0) && int.Parse(arr[1]) <= 59))
                    {
                        bValid = false;
                    }
                }
                else
                {
                    bValid = false;
                }

                return bValid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DataTable GetOvedMatzav(int iMisparIshi, DateTime dDate)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {   //מחזיר טבלת פרמטרים:                                
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dDate, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clDefinitions.cProGetOvedMatzav, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }



        private bool CheckHaveSidurGrira(int iMisparIshi, DateTime dDateToCheck, ref DataTable dt)
        {
            clDal oDal = new clDal();
            try
            {
                //בודקים אם ישנה פעילות זהה
                //אם כן, נחזיר TRUE
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dDateToCheck, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clDefinitions.cProCheckHaveSidurGrira, ref dt);

                return dt.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        public FlowErrorResult MainOvedErrorsNew(int iMisparIshi, DateTime dCardDate)
        {
            var errorFlowManager = ServiceLocator.Current.GetInstance<IErrorFlowManager>();
            FlowErrorResult errorResults = errorFlowManager.ExecuteFlow(iMisparIshi, dCardDate, 0, 0);
            
            _dtErrors = errorResults.Errors;
            _CardStatus = errorResults.CardStatus;
            return errorResults;
         
        }


        private bool IsOvedInMatzav(string sMatzavim)
        {
            bool result = false;
            try
            {
                //return result;
                result = sMatzavim.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries)
                               .Any(matzav => IsOvedMatzavExists(matzav));
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        private bool IsOvedMatzavExists(string sKodMatzav)
        {
            DataRow[] dr;
            bool bOvedMatzavExists;

            try
            {
                //נחזיר אם קיים לעובד מצב
                // למשל מצב 1 מציין אם עובד פעיל, מצב 5 מציין אם עובד במצב של מחלה 
                if (clGeneral.IsNumeric(sKodMatzav))
                    if (int.Parse(sKodMatzav) > 0 && int.Parse(sKodMatzav) < 10)
                        sKodMatzav = "0" + sKodMatzav.Trim();

                dr = dtMatzavOved.Select(string.Concat("kod_matzav='", sKodMatzav + "'"));
                bOvedMatzavExists = dr.Length > 0;
                return bOvedMatzavExists;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       

        private void GetMeafyeneyOvdim(int iMisparIshi, DateTime dCardDate)
        {
            //clOvdim oOvdim = new clOvdim();
          try{
              var ovedManager =  ServiceLocator.Current.GetInstance<IOvedManager>();
              oMeafyeneyOved = ovedManager.CreateMeafyenyOved(iMisparIshi, dCardDate);
                //oMeafyeneyOved = new clMeafyenyOved(iMisparIshi, dCardDate);
          }
          catch (Exception ex)
          {
              throw ex;
          }
          }

        private void SetParameters(DateTime dCardDate, int iSugYom)
        {
              var cache=  ServiceLocator.Current.GetInstance<IKDSAgedQueueParameters>();
              var param=cache.GetItem(dCardDate);
              if (param != null)
                  oParam = param;
              else
              {
                  IParametersManager paramManager = ServiceLocator.Current.GetInstance<IParametersManager>();
                   oParam = paramManager.CreateClsParametrs(dCardDate, iSugYom);
                   cache.Add(oParam, dCardDate);
              }
       
        }

     
        

     
        private string GetLookUpKods(string sTableName)
        {
            //The function get lookup table name and return all kods in string, separate by comma
            string sLookUp = "";
            DataRow[] drLookUpAll;
            try
            {
                drLookUpAll = dtLookUp.Select(string.Concat("table_name='", sTableName, "'"));
                foreach (DataRow drLookUp in drLookUpAll)
                {
                    sLookUp = string.Concat(sLookUp, drLookUp["Kod"].ToString(), ",");
                }
                sLookUp = sLookUp.Substring(0, sLookUp.Length - 1);

                return sLookUp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private bool CheckBoolSidur(float fZmanSidur,int iMisparSidur)
        {
            DataSet dsSidur;
            int iResult;
            float  fZmanMapa=0;
            double fMaazanTichnun = 0;
            DateTime dShatGmarMapa, dShaHatchalaMapa;
            string sShaa;
            bool bCheckBoolSidur = false;
            try
            {
                var kavimDal = ServiceLocator.Current.GetInstance<IKavimDAL>();
                dsSidur = kavimDal.GetSidurAndPeiluyotFromTnua(iMisparSidur, _dCardDate,null, out iResult);
                if (iResult == 0)
                {
                    //שעת התחלה ושעת גמר
                    if (dsSidur.Tables[1].Rows.Count > 0)
                    {
                        sShaa = dsSidur.Tables[1].Rows[0]["SHAA"].ToString();
                        dShaHatchalaMapa = DateHelper.GetDateTimeFromStringHour(sShaa, _dCardDate);
                        for (int i = dsSidur.Tables[1].Rows.Count - 1; i >= 0; i--)
                        {
                            long lMakatNesia = long.Parse(dsSidur.Tables[1].Rows[i]["MAKAT8"].ToString());
                            sShaa = dsSidur.Tables[1].Rows[i]["SHAA"].ToString();
                            if (!string.IsNullOrEmpty(dsSidur.Tables[1].Rows[i]["MazanTichnun"].ToString()))
                                fMaazanTichnun = double.Parse(dsSidur.Tables[1].Rows[i]["MazanTichnun"].ToString());
                            dShatGmarMapa = DateHelper.GetDateTimeFromStringHour(sShaa, _dCardDate).AddMinutes(fMaazanTichnun);
                            fZmanMapa = int.Parse((dShatGmarMapa - dShaHatchalaMapa).TotalMinutes.ToString());
                    
                            //במידה והפעילות האחרונה היא אלמנט לידיעה בלבד (ערך 2 (לידיעה בלבד) במאפיין 3  (לפעולה/לידיעה בלבד), יש לקחת את הפעילות הקודמת לה.
                        
                            if ((enMakatType)(StaticBL.GetMakatType(lMakatNesia)) == enMakatType.mElement)
                            {
                                DataRow drMeafyeneyElements = dtTmpMeafyeneyElements.Select("kod_element=" + int.Parse(lMakatNesia.ToString().Substring(1, 2)))[0];
                                if (drMeafyeneyElements["element_for_yedia"].ToString() != "2")
                                {
                                    break;
                                }
                            }
                            else { break; }
                        }
                       
                    }

                    if (fZmanMapa == fZmanSidur)
                    { bCheckBoolSidur = true; }
                }
                return bCheckBoolSidur;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private PeilutDM GetLastPeilutNoElementLeyedia(SidurDM oSidur)
        {
            try{
            PeilutDM oPeilutAchrona=null;
           
            for (int i = oSidur.htPeilut.Count - 1; i >= 0; i--)
            {
                oPeilutAchrona = (PeilutDM)oSidur.htPeilut[i];
                if (oPeilutAchrona.iMakatType == enMakatType.mElement.GetHashCode())
                {
                    if (oPeilutAchrona.iElementLeYedia!=2)
                    {
                        break;
                    }
                }
                else { break; }
            }

            return oPeilutAchrona;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private bool CheckConditionsAllowSidur(ref SidurDM oSidur)
        {
            bool bError = false;
            //א. לעובד אין רישיון נהיגה באוטובוס (יודעים אם לעובד יש רישיון לפי ערכים 6, 10, 11 בקוד נתון 7 (קוד רישיון אוטובוס) בטבלת פרטי עובדים)
            bError = (!IsOvedHasDriverLicence());

            //ב. עובד הוא מותאם שאסור לו לנהוג (יודעים שעובד הוא מותאם שאסור לו לנהוג לפי ערכים 4, 5 בקוד נתון 8 (קוד עובד מותאם) בטבלת פרטי עובדים) 
            if (!(bError))
            {
                bError = IsOvedMutaam();
            }

            if (!bError)
            {
                //ד. עובד הוא בשלילה (יודעים שעובד הוא בשלילה לפי ערך 1 בקוד בנתון 21 (שלילת   רשיון) בטבלת פרטי עובדים) 
                if (_dCardDate !=  oOvedYomAvodaDetails.dTaarichMe)
                    bError = IsOvedBShlila();
            }

            if (!bError)
            {
                //. עובד הוא מותאם שמותר לו לבצע רק נסיעה ריקה (יודעים שעובד הוא מותאם שמותר לו לבצע רק נסיעה ריקה לפי ערכים 6, 7 בקוד נתון 8 (קוד עובד מותאם) בטבלת פרטי עובדים) במקרה זה יש לבדוק אם הסידור מכיל רק נסיעות ריקות, מפעילים את הרוטינה לזיהוי מקט
                bError = IsOvedMutaamForEmptyPeilut(ref oSidur);
            }
            return bError;
        }


        private bool IsOvedHasDriverLicence()
        {
            //א. לעובד אין רישיון נהיגה באוטובוס (יודעים אם לעובד יש רישיון לפי ערכים 6, 10, 11 בקוד נתון 7 (קוד רישיון אוטובוס) בטבלת פרטי עובדים)
            return (oOvedYomAvodaDetails.sRishyonAutobus == clGeneral.enRishyonAutobus.enRishyon10.GetHashCode().ToString() ||
                    oOvedYomAvodaDetails.sRishyonAutobus == clGeneral.enRishyonAutobus.enRishyon11.GetHashCode().ToString() ||
                    oOvedYomAvodaDetails.sRishyonAutobus == clGeneral.enRishyonAutobus.enRishyon6.GetHashCode().ToString());

        }

        private bool IsOvedMutaam()
        {
            //ב. עובד הוא מותאם שאסור לו לנהוג (יודעים שעובד הוא מותאם שאסור לו לנהוג לפי ערכים 4, 5 בקוד נתון 8 (קוד עובד מותאם) בטבלת פרטי עובדים)
            return (oOvedYomAvodaDetails.sMutamut == clGeneral.enMutaam.enMutaam4.GetHashCode().ToString() ||
                    oOvedYomAvodaDetails.sMutamut == clGeneral.enMutaam.enMutaam5.GetHashCode().ToString()); 
     
        }

        private bool IsOvedBShlila()
        {
            //ד. עובד הוא בשלילה (יודעים שעובד הוא בשלילה לפי ערך 1 בקוד בנתון 21 (שלילת   רשיון) בטבלת פרטי עובדים) 
            return oOvedYomAvodaDetails.sShlilatRishayon == clGeneral.enOvedBShlila.enBShlila.GetHashCode().ToString();
        }


        private bool IsOvedMutaamForEmptyPeilut(ref SidurDM oSidur)
        {
            //. עובד הוא מותאם שמותר לו לבצע רק נסיעה ריקה (יודעים שעובד הוא מותאם שמותר לו לבצע רק נסיעה ריקה לפי ערכים 6, 7 בקוד נתון 8 (קוד עובד מותאם) בטבלת פרטי עובדים) במקרה זה יש לבדוק אם הסידור מכיל רק נסיעות ריקות, מפעילים את הרוטינה לזיהוי מקט
            return ((oOvedYomAvodaDetails.sMutamut == clGeneral.enMutaam.enMutaam6.GetHashCode().ToString() ||
                   oOvedYomAvodaDetails.sMutamut == clGeneral.enMutaam.enMutaam7.GetHashCode().ToString())
                   && (oSidur.bSidurNotEmpty)); 

        }

        public clParametersDM oParam
        {
            set
            {
                _oParameters = value;
            }
            get
            {
                return _oParameters;
            }
        }
        public DataTable dtLookUp
        {
            set
            {
                _dtLookUp = value;
            }
            get
            {
                return _dtLookUp;
            }
        }

        public DataTable dtYamimMeyuchadim
        {
            set
            {
                _dtYamimMeyuchadim = value;
            }
            get
            {
                return _dtYamimMeyuchadim;
            }
        }

        public OvedYomAvodaDetailsDM oOvedYomAvodaDetails
        {
            set
            {
                _oOvedYomAvodaDetails = value;
            }
            get
            {
                return _oOvedYomAvodaDetails;
            }
        }
        public MeafyenimDM oMeafyeneyOved                               
        {
            set
            {
                _oMeafyeneyOved = value;
            }
            get
            {
                return _oMeafyeneyOved;
            }
        }
        public int iSugYom
        {
            set
            {
                _iSugYom = value;
            }
            get
            {
                return _iSugYom;
            }
        }
        public DataTable dtSugSidur
        {
            set
            {
                _dtSugSidur = value;
            }
            get
            {
                return _dtSugSidur;
            }
        }

        public DataTable dtTmpSidurimMeyuchadim
        {
            set
            {
                _dtSidurimMeyuchadim = value;
            }
            get
            {
                return _dtSidurimMeyuchadim;
            }
        }


        public DataTable dtTmpMeafyeneyElements
        {
            set
            {
                _dtMeafyeneyElements = value;
            }
            get
            {
                return _dtMeafyeneyElements;
            }
        }

        public DataTable dtErrors
        {
            set
            {
                _dtErrors = value;
            }
            get
            {
                return _dtErrors;
            }
        }

        public DataTable dtMatzavOved
        {
            set
            {
                _dtMatzavOved = value;
            }
            get
            {
                return _dtMatzavOved;
            }
        }
        public DataTable dtDetails
        {
            set
            {
                _dtDetails = value;
            }
            get
            {
                return _dtDetails;
            }
        }
        public OrderedDictionary htEmployeeDetails
        {
            set
            {
                _htEmployeeDetails = value;
            }
            get
            {
                return _htEmployeeDetails;
            }
        }

        public int iLoginUserId
        {
            set
            {
                _iLoginUserId = value;
            }
            get
            {
                return _iLoginUserId;
            }
        }
        public OrderedDictionary htFullEmployeeDetails
        {
            set
            {
                _htFullEmployeeDetails = value;
            }
            get
            {
                return _htFullEmployeeDetails;
            }
        }
      
        public CardStatus CardStatus
        {
            set { _CardStatus = value; }
            get { return _CardStatus; }
        }


        public  bool HaveShgiotLetzuga
        {
             get { return _bHaveShgiotLetzuga; }
        }

        public bool MainInputData(int iMisparIshi, DateTime dCardDate, out bool bHaveCount)
        {
            bool bSuccsess = true;

            bSuccsess=MainInputData(iMisparIshi, dCardDate);

            bHaveCount= _bHaveCount;

            return bSuccsess;
        }

        public bool MainInputData(int iMisparIshi, DateTime dCardDate)
        {
            //M A I N   P R O C E D U R E
            //Get all oved details and calls all check function
            string sCarNumbers;
            clDefinitions oDefinition = new clDefinitions();
            clUtils oUtils = new clUtils();
            OrderedDictionary htFullSidurimDetails = new OrderedDictionary();
            //Insert Oved Details to Class
            htEmployeeDetails = new OrderedDictionary();
            // Write an informational entry to the event log.    
            _bSuccsess = true;
            _iMisparIshi = iMisparIshi;
            _dCardDate = dCardDate;
            try
            {
                ////Get LookUp Tables
                //dtLookUp = GetLookUpTables();

                //Get Parameters Table
                //dtParameters = GetKdsParametrs();
                var cache = ServiceLocator.Current.GetInstance<IKDSCacheManager>();
                dtYamimMeyuchadim = cache.GetCacheItem<DataTable>(CachedItems.YamimMeyuhadim);

                _dtSugeyYamimMeyuchadim = cache.GetCacheItem<DataTable>(CachedItems.SugeyYamimMeyuchadim);

                //Get Meafyeny Ovdim
                var ovedManager = ServiceLocator.Current.GetInstance<IOvedManager>();
                oMeafyeneyOved = ovedManager.CreateMeafyenyOved(iMisparIshi, dCardDate);
                
                dtSibotLedivuachYadani = cache.GetCacheItem<DataTable>(CachedItems.SibotLedivuchYadani);

                iSugYom = DateHelper.GetSugYom(dtYamimMeyuchadim, dCardDate, _dtSugeyYamimMeyuchadim);//, _oMeafyeneyOved.GetMeafyen(56).IntValue);

                //Set global variable with parameters
                SetParameters(dCardDate, iSugYom);
               //oParam = clDefinitions.GetParamInstance(dCardDate, iSugYom);           

                //Get Meafyeney Sug Sidur
                dtSugSidur = cache.GetCacheItem<DataTable>(CachedItems.SugeySidur);

                dtMatzavOved = GetOvedMatzav(iMisparIshi, dCardDate);
                //Get Mutamut
                dtMutamut = cache.GetCacheItem<DataTable>(CachedItems.Mutamut);

                //Get Temp Sidurim Meyuchadim
                dtTmpSidurimMeyuchadim = clDefinitions.GetTmpSidurimMeyuchadim(dCardDate, dCardDate);

                //Get Temp Meafyeney Elements
                dtTmpMeafyeneyElements = clDefinitions.GetTmpMeafyeneyElements(dCardDate, dCardDate);

                //Get Employee Ishurim vered 22/05/2012
                //arrEmployeeApproval = ApprovalRequest.GetMatchingApprovalRequestsWithStatuses(iMisparIshi, dCardDate);

                //בדיקות ברמת יום עבודה
                //Get yom avoda details
                // dtOvedCardDetails = GetOvedYomAvodaDetails(iMisparIshi, dCardDate);
                SetOvedYomAvodaDetails(iMisparIshi, dCardDate);
                //if (dtOvedCardDetails.Rows.Count>0)
                if (oOvedYomAvodaDetails!=null)
                {
                    if ((oOvedYomAvodaDetails.sKodMaamd == "331") || (oOvedYomAvodaDetails.sKodMaamd == "332") || (oOvedYomAvodaDetails.sKodMaamd == "333") || (oOvedYomAvodaDetails.sKodMaamd == "334"))
                    {
                        clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, iMisparIshi, "I", 0, dCardDate, "MainInputData: " + "HR - לעובד זה חסרים נתונים ב ");
                        _bSuccsess = false;
                        _bHaveCount = false;
                    }
                    else if(oOvedYomAvodaDetails.iIsuk == 0) 
                    {
                        clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, iMisparIshi, "W", 0, dCardDate, "MainInputData: " + "HR - לעובד זה חסרים נתונים ב ");
                        _bSuccsess = false;
                    }
                    else
                    {
                        //Get Oved Details
                        dtDetails = oDefinition.GetOvedDetails(iMisparIshi, dCardDate);
                        if (dtDetails.Rows.Count > 0)
                        {
                            //OrderedDictionary htFullSidurimDetails = new OrderedDictionary();
                            //Insert Oved Details to Class
                            htEmployeeDetails = oDefinition.InsertEmployeeDetails(false, dtDetails, dCardDate, ref iLastMisaprSidur, out _htSpecialEmployeeDetails, ref htFullSidurimDetails);//, out  _htEmployeeDetailsWithCancled
                            htFullEmployeeDetails = htFullSidurimDetails;
                            sCarNumbers = clDefinitions.GetMasharCarNumbers(htEmployeeDetails);

                            if (sCarNumbers != string.Empty)
                            {
                                var kavimDal = ServiceLocator.Current.GetInstance<IKavimDAL>();
                                dtMashar = kavimDal.GetMasharData(sCarNumbers);
                            }
                        }
                        //_dtApproval = clDefinitions.GetApprovalToEmploee(iMisparIshi, dCardDate);
                        _dtIdkuneyRashemet = clDefinitions.GetIdkuneyRashemet(iMisparIshi, dCardDate);
                        _dtIdkuneyRashemet.Columns.Add("update_machine", System.Type.GetType("System.Int32"));
                   
                        _dtApprovalError = clDefinitions.GetApprovalErrors(iMisparIshi, dCardDate);

                        CheckAllData(dCardDate, iMisparIshi, iSugYom);

                        _IsExecuteInputData = true;
                    }
                }
                
               
              return  _bSuccsess;
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, iMisparIshi, "E", 0, dCardDate, "MainInputData: " + ex.Message);
                return false;
            }
            
        }
      
        private void CheckAllData(DateTime dCardDate, int iMisparIshi, int iSugYom)
        {
            SidurDM oSidur = null;
            PeilutDM oPeilut = null;


            // bool bFirstSidur = true;
            bool bLoLetashlum = false; //כל הסידורים לא לתשלום יהיה TRUE, אחרת FALSE
            //int iKey = 0;
            bool bSidurNahagut = false; //מציין אם יש סידור אחד לפחות של נהגות
            bool bSidurHeadrut = false; //מציין אם יש סידור אחד לפחות של העדרות מסוג מילואים/מחלה או תאונה            
            bool bSidurShaon = false; //קיים סידור אחד לפחות שהוא סידור שעון 
            //bool bSidurShaonYetizaValid = false; //קיים סידור אחד לפחות שהוא סידור שעון והחתמת יציאה תקינה
             bool bFirstSidurZakaiLenesiot = false;
            bool bHeadrutMachalaMiluimTeuna = false;
            DataRow[] drSugSidur;
           DateTime dShatHatchalaNew; //עבור שינוי מספר 12
           bool bFirstHayavHityazvut, bSecondHayavHityazvut, bHayavHityazvut;
            bool bUpdateShatHatchala;
            oCollYameyAvodaUpd = new COLL_YAMEY_AVODA_OVDIM();
            oCollSidurimOvdimUpd = new COLL_SIDURIM_OVDIM();
            oCollSidurimOvdimIns = new COLL_SIDURIM_OVDIM();
            oCollSidurimOvdimDel = new COLL_SIDURIM_OVDIM();
            oCollPeilutOvdimDel = new COLL_OBJ_PEILUT_OVDIM();
            oCollPeilutOvdimUpd = new COLL_OBJ_PEILUT_OVDIM();
            oCollPeilutOvdimIns = new COLL_OBJ_PEILUT_OVDIM();
            _oCollIdkunRashemet = new COLL_IDKUN_RASHEMET();
            _oCollApprovalErrors = new COLL_SHGIOT_MEUSHAROT();
            htNewSidurim = new OrderedDictionary();
            bUpdateShatHatchala = false;
            bool bIdkunRashShatHatchala = false;
            bool bIdkunRashShatGmar = false;

            int iCountPeiluyot = 0;
            int i = 0;
            int j = 0;
            int iHeadrutTypeKod = 0;
            bool bUsedMazanTichnun = false;
            SidurDM oSidurNidrashHityatvut;
            SourceObj SourceObject;
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd=null;
            try
            {                
                //נאתחל את אובייקט ימי עבודה לעדכון
                oObjYameyAvodaUpd = new OBJ_YAMEY_AVODA_OVDIM();          
                InsertToYameyAvodaForUpdate(dCardDate, ref oObjYameyAvodaUpd, ref _oOvedYomAvodaDetails);
                oObjYameyAvodaUpdWrapper = new ModificationRecorder<OBJ_YAMEY_AVODA_OVDIM>(oObjYameyAvodaUpd);
                //נעבור על כל הסידורים 
                if (htEmployeeDetails != null)
                {
                    //מחיקת סידורי רציפות
                    DeleteSidureyRetzifut();

                    //סימון סידורים לא לתשלום=0
                    for (i = 0; i < htEmployeeDetails.Count; i++)
                    {
                        oSidur = (SidurDM)htEmployeeDetails[i];
                        oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur);
                        if (!CheckIdkunRashemet("LO_LETASHLUM", oSidur.iMisparSidur, oSidur.dFullShatHatchala))
                        {
                            if (oSidur.iLoLetashlum == 1 && !(oSidur.iKodSibaLoLetashlum == 1 || oSidur.iKodSibaLoLetashlum == 11 || oSidur.iKodSibaLoLetashlum == 20 || oSidur.iKodSibaLoLetashlum == 19))
                            {
                                oSidur.iLoLetashlum = 0;
                                oSidur.iKodSibaLoLetashlum = 0;
                                oObjSidurimOvdimUpd.LO_LETASHLUM = 0;
                                oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM = 0;                              
                            }
                            if (!CheckIdkunRashemet("PITZUL_HAFSAKA", oSidur.iMisparSidur, oSidur.dFullShatHatchala))
                            {
                                oObjSidurimOvdimUpd.PITZUL_HAFSAKA = 0;
                                oSidur.sPitzulHafsaka = "0";
                            }
                       }     
                       oObjSidurimOvdimUpd.MEZAKE_NESIOT = 0;
                       oObjSidurimOvdimUpd.MEZAKE_HALBASHA = 0;
                       oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                       
                       htEmployeeDetails[i] = oSidur;
                    }

                 

                    //שינוי 01
                    for (i = 0; i < htEmployeeDetails.Count; i++)
                    {
                        oSidur = (SidurDM)htEmployeeDetails[i];
                        if (!CheckIdkunRashemet("MISPAR_SIDUR", oSidur.iMisparSidur, oSidur.dFullShatHatchala))
                        {
                            FixedMisparSidur01(ref oSidur, i);
                            
                            htEmployeeDetails[i] = oSidur;
                        }
                    }


                    //-שינוי 02
                    for (i = 0; i < htEmployeeDetails.Count; i++)
                    {
                        oSidur = (SidurDM)htEmployeeDetails[i];
                        if (!CheckIdkunRashemet("MISPAR_SIDUR", oSidur.iMisparSidur, oSidur.dFullShatHatchala))
                        {
                            oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur);

                            FixedMisparMatalatVisa02(ref oSidur, i, ref oObjSidurimOvdimUpd);
                            htEmployeeDetails[i] = oSidur;
                        }
                    }

                    //שינוי 28
                    MergerSiduryMapa28();

                    
                    //שינוי מספר 5
                    //שינוי זה צריך לעבוד לפני שינוי 4
                    AddElementMechine05_2();

                   

                    //שינוי 17
                    //SidurNetzer17(ref oSidur);

                    //שינוי 10
                    for (i = 0; i < htEmployeeDetails.Count; i++)
                    {
                        oSidur = (SidurDM)htEmployeeDetails[i];
                        if (!CheckIdkunRashemet("SHAT_GMAR", oSidur.iMisparSidur, oSidur.dFullShatHatchala))
                        {
                            oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur);

                            FixedShatGmar10(ref oSidur, i, ref  bUsedMazanTichnun, ref oObjSidurimOvdimUpd);
                            htEmployeeDetails[i] = oSidur;
                        }
                    }

                    ////// שינוי 04
                    ////for (i = 0; i < htEmployeeDetails.Count; i++)
                    ////{
                    ////    oSidur = (SidurDM)htEmployeeDetails[i];
                    ////    if (!CheckIdkunRashemet("SHAT_HATCHALA", oSidur.iMisparSidur, oSidur.dFullShatHatchala) && !CheckIdkunRashemet("SHAT_GMAR", oSidur.iMisparSidur, oSidur.dFullShatHatchala))
                    ////    {
                    ////        oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur);

                    ////        FixedShaotForSidurWithOneNesiaReeka04(i, ref oSidur, ref oObjSidurimOvdimUpd);
                    ////        htEmployeeDetails[i] = oSidur;
                    ////    }
                    ////}

                    //שינוי 11
                    for (i = 0; i < htEmployeeDetails.Count; i++)
                    {
                        oSidur = (SidurDM)htEmployeeDetails[i];
                        oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur);
                        drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, dCardDate, _dtSugSidur);
                        if (!CheckIdkunRashemet("LO_LETASHLUM", oSidur.iMisparSidur, oSidur.dFullShatHatchala))
                        {
                            SidurLoLetashlum11(drSugSidur, ref oSidur, i, ref oObjSidurimOvdimUpd);
                        }
                        htEmployeeDetails[i] = oSidur;
                    }

                    //שינוי 29
                    SiduryMapaWhithStatusNullLoLetashlum29();

                    //שינוי 6
                    //צריך לעבוד אחרי שינויים  29,11
                    SidurDM oSidurFirst = new SidurDM();
                    SidurDM oSidurSecond = new SidurDM();
                    int indexSidurFirst = 0;
                    bool flag = true;
                    
                    for (i = 0; i < htEmployeeDetails.Count; i++)
                    {
                        if (flag)
                        {
                            oSidur = (SidurDM)htEmployeeDetails[i];
                            if (oSidur.iLoLetashlum == 0)
                            {
                                oSidurFirst = oSidur;
                                indexSidurFirst = i;
                                flag = false;
                            }
                        }
                        //oObjSidurimOvdimUpd = GetUpdSidurObject(oSidurFirst);
                        //if (!CheckIdkunRashemet("PITZUL_HAFSAKA", oSidurFirst.iMisparSidur, oSidurFirst.dFullShatHatchala))
                        //{
                        //    oObjSidurimOvdimUpd.PITZUL_HAFSAKA = 0;
                        //    oSidurFirst.sPitzulHafsaka = "0";
                        //    htEmployeeDetails[indexSidurFirst] = oSidurFirst;
                        //}
                        if (!flag && i < (htEmployeeDetails.Count - 1))
                        {
                            oSidurSecond = (SidurDM)htEmployeeDetails[i + 1];
                            if (oSidurFirst.iLoLetashlum == 0 && oSidurSecond.iLoLetashlum == 0)
                            {
                                if (!CheckIdkunRashemet("PITZUL_HAFSAKA", oSidurFirst.iMisparSidur, oSidurFirst.dFullShatHatchala))
                                {
                                    oObjSidurimOvdimUpd = GetUpdSidurObject(oSidurFirst);

                                    FixedPitzulHafsaka06(ref oSidurFirst, i + 1, ref oObjSidurimOvdimUpd);
                                    htEmployeeDetails[indexSidurFirst] = oSidurFirst;
                                }
                                oSidurFirst = oSidurSecond;
                                indexSidurFirst = i + 1;
                            }
                        }
                    }
                    ////שינוי 16
                    //FixedMisparSidurToNihulTnua16(ref  oSidur, ref oObjSidurimOvdimUpd);


                    //חישוב שעת התחלה - שינוי 30
                    for (i = 0; i < htEmployeeDetails.Count; i++)
                    {
                        oSidur = (SidurDM)htEmployeeDetails[i];
                        if (!CheckIdkunRashemet("SHAT_HATCHALA", oSidur.iMisparSidur, oSidur.dFullShatHatchala))
                        {
                            oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur);

                            ChishuvShatHatchala30(ref oSidur, i, ref  bUsedMazanTichnun, ref oObjSidurimOvdimUpd);
                            htEmployeeDetails[i] = oSidur;
                        }
                    }

                    //נבצע את הבדיקות לכל הסידורים, מלבד הראשון - שינוי 08
                    FixedSidurHours08();

                   
                    //בוטל 04/07/2011
                    //-שינוי 22
                    ////for (i = 0; i < htEmployeeDetails.Count; i++)
                    ////{
                    ////    oSidur = (SidurDM)htEmployeeDetails[i];
                    ////    if (!CheckIdkunRashemet("LO_LETASHLUM", oSidur.iMisparSidur, oSidur.dFullShatHatchala) && !CheckIdkunRashemet("SHAT_GMAR", oSidur.iMisparSidur, oSidur.dFullShatHatchala))
                    ////    {
                    ////        oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur);

                    ////        FixedEggedTaavura22(ref oSidur, ref oObjSidurimOvdimUpd);
                    ////        htEmployeeDetails[i] = oSidur;
                    ////    }
                    ////}

                    //שינוי 23
                    bFirstHayavHityazvut = false;
                    bSecondHayavHityazvut = false;
                    oSidurNidrashHityatvut = null;
                    bHayavHityazvut = false;
                    //IpusSidurimMevutalimYadanit23();
                    for (i = 0; i < htEmployeeDetails.Count; i++)
                    {
                        oSidur = (SidurDM)htEmployeeDetails[i];
                        oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur);
                        if (!(CheckIdkunRashemet("KOD_SIBA_LEDIVUCH_YADANI_IN", oSidur.iMisparSidur, oSidur.dFullShatHatchala) && (oSidur.iNidreshetHitiatzvut == 1 || oSidur.iNidreshetHitiatzvut == 2)))
                        {
                            FixedItyatzvutNahag23(i, ref oSidur, ref oSidurNidrashHityatvut, ref oObjSidurimOvdimUpd, ref bFirstHayavHityazvut, ref bSecondHayavHityazvut);
                            htEmployeeDetails[i] = oSidur;
                        }
                        else
                        {
                            CheckHovatHityazvut(oSidur, ref oObjSidurimOvdimUpd, 1, ref bHayavHityazvut, true);
                            if (bHayavHityazvut)
                                if (!bFirstHayavHityazvut) 
                                    bFirstHayavHityazvut = true;
                                else if (!bSecondHayavHityazvut) 
                                    bSecondHayavHityazvut = true;
                        }
                    }

                    for (i = 0; i < htEmployeeDetails.Count; i++)
                    {
                        oSidur = (SidurDM)htEmployeeDetails[i];
                        oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur);

                        //מחוץ למכסה
                        if (!CheckIdkunRashemet("OUT_MICHSA", oSidur.iMisparSidur, oSidur.dFullShatHatchala))
                            UpdateOutMichsa(ref oSidur, ref oObjSidurimOvdimUpd);

                        //חריגה
                        if (!CheckIdkunRashemet("CHARIGA", oSidur.iMisparSidur, oSidur.dFullShatHatchala)) //!CheckApproval("2,4,5,6,10", oSidur.iMisparSidur, oSidur.dFullShatHatchala) &&
                            UpdateChariga(ref oSidur, ref oObjSidurimOvdimUpd);

                        //השלמה
                        if (!CheckIdkunRashemet("HASHLAMA", oSidur.iMisparSidur, oSidur.dFullShatHatchala))
                            UpdateHashlamaForSidur(ref oSidur,i, ref oObjSidurimOvdimUpd);

                        htEmployeeDetails[i] = oSidur;
                    }

                    for (i = 0; i < htEmployeeDetails.Count; i++)
                    {
                        oSidur = (SidurDM)htEmployeeDetails[i];
                        oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur);

                        iCountPeiluyot = ((SidurDM)(htEmployeeDetails[i])).htPeilut.Count;
                        j = 0;

                        if (iCountPeiluyot > 0)
                        {
                            do
                            {
                                oPeilut = (PeilutDM)((SidurDM)(htEmployeeDetails[i])).htPeilut[j];
                                oObjPeilutOvdimUpd = GetUpdPeilutObject(i, oPeilut, out SourceObject, oObjSidurimOvdimUpd);

                                DeleteElementRechev07(ref oSidur, ref oPeilut, ref  j, ref oObjSidurimOvdimUpd);
                                //ChangeElement06(ref oPeilut, ref oSidur, j);

                                j += 1;
                                iCountPeiluyot = ((SidurDM)(htEmployeeDetails[i])).htPeilut.Count;
                            }
                            while (j < iCountPeiluyot);
                        }
                        htEmployeeDetails[i] = oSidur;
                    }

                    //שינוי 12 
                    //שינוי זה צריך לעבוד אחרי שינוי 23.
                    for (i = 0; i < htEmployeeDetails.Count; i++)
                    {
                        oSidur = (SidurDM)htEmployeeDetails[i];
                      
                            oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur);

                            dShatHatchalaNew = oSidur.dFullShatHatchala;
                            bUpdateShatHatchala = false;
                            iCountPeiluyot = ((SidurDM)(htEmployeeDetails[i])).htPeilut.Count;
                            j = 0;

                            if (iCountPeiluyot > 0)
                            {
                                do
                                {
                                    oPeilut = (PeilutDM)((SidurDM)(htEmployeeDetails[i])).htPeilut[j];
                                    oObjPeilutOvdimUpd = GetUpdPeilutObject(i, oPeilut, out SourceObject, oObjSidurimOvdimUpd);

                                    FixedShatHatchalaLefiShatHachtamatItyatzvut12(ref j, ref oPeilut, ref oSidur, ref oObjSidurimOvdimUpd, ref oObjPeilutOvdimUpd, ref  bUpdateShatHatchala, ref dShatHatchalaNew, SourceObject);
                                 
                                    j += 1;
                                    iCountPeiluyot = ((SidurDM)(htEmployeeDetails[i])).htPeilut.Count;
                                }
                                while (j < iCountPeiluyot);
                            }

                            if (!CheckIdkunRashemet("SHAT_HATCHALA", oSidur.iMisparSidur, oSidur.dFullShatHatchala))
                            {
                                if (bUpdateShatHatchala)
                                {
                                    FixedShatHatchalaLefiShatHachtamatItyatzvut12(ref oSidur, i, dShatHatchalaNew, ref oObjSidurimOvdimUpd);
                                }
                            }
                            htEmployeeDetails[i] = oSidur;
                        
                    }


                    //שינוי 25
                    FixedShatHamtana25();

                    for (i = 0; i < htEmployeeDetails.Count; i++)
                    {
                        oSidur = (SidurDM)htEmployeeDetails[i];
                        oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur);
                        
                        bIdkunRashShatHatchala = false;
                        bIdkunRashShatGmar = false;

                        if (CheckIdkunRashemet("SHAT_HATCHALA_LETASHLUM", oSidur.iMisparSidur, oSidur.dFullShatHatchala))
                        { bIdkunRashShatHatchala = true; }
                        if (CheckIdkunRashemet("SHAT_GMAR_LETASHLUM", oSidur.iMisparSidur, oSidur.dFullShatHatchala))
                        { bIdkunRashShatGmar = true; }

                        //שינוי 19
                        SetHourToSidur19(ref  oSidur, ref oObjSidurimOvdimUpd, bIdkunRashShatHatchala, bIdkunRashShatGmar);

                        //שינוי 26
                        FixedShatHatchalaAndGmarToMafilim26(ref oSidur, ref oObjSidurimOvdimUpd, bIdkunRashShatHatchala, bIdkunRashShatGmar);

                        //שינוי 27
                        FixedShatHatchalaAndGmarSidurMapa27(ref oSidur, ref oObjSidurimOvdimUpd);

                        
                        //שינוי 21
                       // FixedShatHatchalaForSidurVisa21(ref oSidur, ref oObjSidurimOvdimUpd);
                        htEmployeeDetails[i] = oSidur;
                    }


                    //שינוי 14
                    //iCountSidurim = htEmployeeDetails.Count;
                    //if (iCountSidurim > 1)
                    //{
                    //    i = 1;
                    //    do
                    //    {
                    //        oSidur = (SidurDM)htEmployeeDetails[i];
                    //        oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur);

                    //        drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, dCardDate, _dtSugSidur);

                    //        InsertSidurRetzifut14(dCardDate, i - 1, drSugSidur, ref oSidur, ref oObjSidurimOvdimUpd);

                    //        iCountSidurim = htEmployeeDetails.Count;
                    //        i += 1;

                    //    }
                    //    while (i < iCountSidurim);
                    //}

                    for (i = 0; i < htEmployeeDetails.Count; i++)
                    {
                        oSidur = (SidurDM)htEmployeeDetails[i];

                        drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, dCardDate, _dtSugSidur);

                        //אם סידור אחד לפחות לתשלום, נדליק את  הFLAG
                        if (oSidur.iLoLetashlum == 0) { bLoLetashlum = true; }

                        //ברגע שמצאנו סידור נהגות אחד לפחות, לא נמשיך לחפש
                        // עבור עדכון טכוגרף
                        if (!bSidurNahagut) { bSidurNahagut = IsSidurNahagut(drSugSidur, oSidur); }
                                               
                        //ברגע שמצאנו סידור העדרות(מסוג מחלה/מילואים/תאונה) אחד לפחות, לא נמשיך לחפש
                        //עבור עדכון השלמה ליום
                        if (!bSidurHeadrut) { bSidurHeadrut = IsSidurHeadrut(oSidur); }
                        if (!bHeadrutMachalaMiluimTeuna) { bHeadrutMachalaMiluimTeuna = IsHeadrutMachalaMiluimTeuna(oSidur, ref iHeadrutTypeKod); }


                        //ברגע שמצאנו סידור אחד לפחות שהוא סידור שעון עם החתמת שעון תקינה )אוטומטית/ידנית), נפסיק לחפש
                        if (!bSidurShaon) { bSidurShaon = IsSidurShaon(oSidur); }

                         }
                }
                //שינויים ברמת יום עבודה 
                NewSidurHeadrutWithPaymeny15(iMisparIshi, dCardDate, iSugYom, bLoLetashlum);

                //טכוגרף - 11 השדות
                if (!CheckIdkunRashemet("TACHOGRAF"))
                   UpdateTachograph(bSidurNahagut);
                

                //השלמה ליום - 11 השדות
                if (!CheckIdkunRashemet("HASHLAMA_LEYOM"))
                     UpdateHashlamaForYomAvoda(bHeadrutMachalaMiluimTeuna, iHeadrutTypeKod);

                //ביטול זמן נסיעות - 11 השדות
                UpdateBitulZmanNesiot(bSidurShaon,  bSidurNahagut, bFirstSidurZakaiLenesiot, dCardDate);
                
                //שינוי מספר 3  
                KonnutGrira03(dCardDate);

                //שינוי 20
                //UpdateZmaneyNesia20();

                //שינוי 7
                if (!CheckIdkunRashemet("LINA"))
                       FixedLina07();

                //עבור שינויים 5,1,2,4,12
                SetSidurObjects();

                //11 השדות - הלבשה  
                UpdateHalbasha(dCardDate);

                bool hasChange = oObjYameyAvodaUpdWrapper.HasChanged();
                oCollYameyAvodaUpd.Add(oObjYameyAvodaUpd);

                //נעדכן את ה- DataBase
                clDefinitions.ShinuyKelet(oCollYameyAvodaUpd, oCollSidurimOvdimUpd, oCollSidurimOvdimIns, oCollSidurimOvdimDel, oCollPeilutOvdimUpd, oCollPeilutOvdimIns, oCollPeilutOvdimDel);

                //DATABASE-כיוון שחישוב רכיבי השכר אמורים להתבסס על נתונים עדכניים, נבצע את שינויים שמתבססים על רכיבי שכר לאחר שעידכנו את ה- 


                //////11 השדות - הלבשה  
                ////UpdateHalbasha(dCardDate);             
                ////clDefinitions.UpdateYameyAvodaOvdim(oCollYameyAvodaUpd);
                ////clDefinitions.UpdateSidurimOvdim(oCollSidurimOvdimUpd);

                if (_oCollIdkunRashemet.Count > 0)
                    clDefinitions.SaveIdkunRashemet(_oCollIdkunRashemet);

                if (_oCollApprovalErrors.Count > 0)
                    clDefinitions.UpdateAprrovalErrors(_oCollApprovalErrors);

            }
            catch (Exception ex)
            {
                _bSuccsess = false;
                throw new Exception("CheckAllData: " + iMisparIshi + " " + dCardDate.ToShortDateString() + ex.Message);
            }
        }

      
        private bool CheckIdkunRashemet(string sFieldToChange)
        {
            bool bHaveIdkun = false;
            DataRow[] drIdkunim;
            try{
            drIdkunim = _dtIdkuneyRashemet.Select("shem_db='" + sFieldToChange.ToUpper() + "'");
            if (drIdkunim.Length > 0)
                bHaveIdkun = true;

            //if (sFieldToChange.ToUpper() == "SHAT_HATCHALA")
            //    bHaveIdkun = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bHaveIdkun;
        }

        private bool CheckIdkunRashemet(string sFieldToChange, int iMisparSidur, DateTime dShatHatchala)
        {
            bool bHaveIdkun = false;
            DataRow[] drIdkunim;
            try{
            drIdkunim = _dtIdkuneyRashemet.Select("shem_db='" + sFieldToChange.ToUpper() + "' AND MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime')");
            if (drIdkunim.Length > 0)
                bHaveIdkun = true;

            //if (sFieldToChange.ToUpper() == "SHAT_HATCHALA")
            //    bHaveIdkun = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bHaveIdkun;
        }

        private void UpdateApprovalErrors(int iMisparSidur, DateTime dShatHatchala, DateTime dShatHatchalaNew)
        {
            DataRow[] drIdkunim;
            OBJ_SHGIOT_MEUSHAROT ObjShgiotMeusharot;

            try
            {
                drIdkunim = _dtApprovalError.Select("MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime')");
                for (int i = 0; i < drIdkunim.Length; i++)
                {
                    ObjShgiotMeusharot = FillApprovalErrors(drIdkunim[i]);
                    ObjShgiotMeusharot.NEW_SHAT_HATCHALA = dShatHatchalaNew;
                    drIdkunim[i]["SHAT_HATCHALA"] = dShatHatchalaNew;

                    _oCollApprovalErrors.Add(ObjShgiotMeusharot);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateApprovalErrors(int iMisparSidur, DateTime dShatHatchala, int iMisparKnisa, DateTime dShatYetzia, DateTime dShatYetziaNew)
        {
            DataRow[] drIdkunim;
            OBJ_SHGIOT_MEUSHAROT ObjShgiotMeusharot;

            try
            {
                drIdkunim = _dtApprovalError.Select("MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime') AND MISPAR_KNISA=" + iMisparKnisa + " AND SHAT_YETZIA=Convert('" + dShatYetzia.ToString() + "', 'System.DateTime') ");
                for (int i = 0; i < drIdkunim.Length; i++)
                {
                    ObjShgiotMeusharot = FillApprovalErrors(drIdkunim[i]);
                    ObjShgiotMeusharot.NEW_SHAT_YETZIA = dShatYetziaNew;
                    drIdkunim[i]["SHAT_YETZIA"] = dShatYetziaNew;

                    _oCollApprovalErrors.Add(ObjShgiotMeusharot);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private OBJ_SHGIOT_MEUSHAROT FillApprovalErrors(DataRow drIdkun)
        {
            OBJ_SHGIOT_MEUSHAROT ObjShgiotMeusharot = new OBJ_SHGIOT_MEUSHAROT();
            ObjShgiotMeusharot.MISPAR_ISHI = _iMisparIshi;
            ObjShgiotMeusharot.MISPAR_SIDUR = int.Parse(drIdkun["MISPAR_SIDUR"].ToString());
            ObjShgiotMeusharot.SHAT_HATCHALA = DateTime.Parse(drIdkun["SHAT_HATCHALA"].ToString());
            ObjShgiotMeusharot.NEW_SHAT_HATCHALA = DateTime.Parse(drIdkun["SHAT_HATCHALA"].ToString()); ;
            ObjShgiotMeusharot.TAARICH = _dCardDate;
            ObjShgiotMeusharot.SHAT_YETZIA = DateTime.Parse(drIdkun["SHAT_YETZIA"].ToString());
            ObjShgiotMeusharot.NEW_SHAT_YETZIA = DateTime.Parse(drIdkun["SHAT_YETZIA"].ToString());
            ObjShgiotMeusharot.MISPAR_KNISA = int.Parse(drIdkun["MISPAR_KNISA"].ToString());
            ObjShgiotMeusharot.KOD_SHGIA = int.Parse(drIdkun["KOD_SHGIA"].ToString());

            return ObjShgiotMeusharot;
        }
        private void UpdateIdkunRashemet(int iMisparSidur, DateTime dShatHatchala, DateTime dShatHatchalaNew)
        {
            DataRow[] drIdkunim;
            OBJ_IDKUN_RASHEMET ObjIdkunRashemet;
           
           try
            {
                drIdkunim = _dtIdkuneyRashemet.Select("MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime')");
                for (int i = 0; i < drIdkunim.Length; i++)
                {
                    ObjIdkunRashemet = FillIdkunRashemet(drIdkunim[i]);
                    ObjIdkunRashemet.NEW_SHAT_HATCHALA = dShatHatchalaNew;
                    drIdkunim[i]["SHAT_HATCHALA"] = dShatHatchalaNew;
                    
                    _oCollIdkunRashemet.Add(ObjIdkunRashemet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        private OBJ_IDKUN_RASHEMET FillIdkunRashemet(DataRow drIdkun)
        {
            OBJ_IDKUN_RASHEMET ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
            ObjIdkunRashemet.UPDATE_OBJECT = 1;
            ObjIdkunRashemet.GOREM_MEADKEN = _iUserId;
            ObjIdkunRashemet.MISPAR_ISHI = _iMisparIshi;
            ObjIdkunRashemet.MISPAR_SIDUR = int.Parse( drIdkun["MISPAR_SIDUR"].ToString());
            ObjIdkunRashemet.SHAT_HATCHALA = DateTime.Parse(drIdkun["SHAT_HATCHALA"].ToString());
            ObjIdkunRashemet.NEW_SHAT_HATCHALA = DateTime.Parse(drIdkun["SHAT_HATCHALA"].ToString()); ;
            ObjIdkunRashemet.TAARICH = _dCardDate;
            ObjIdkunRashemet.SHAT_YETZIA = DateTime.Parse(drIdkun["SHAT_YETZIA"].ToString());
            ObjIdkunRashemet.NEW_SHAT_YETZIA = DateTime.Parse(drIdkun["SHAT_YETZIA"].ToString());
            ObjIdkunRashemet.PAKAD_ID = int.Parse(drIdkun["PAKAD_ID"].ToString());
            ObjIdkunRashemet.MISPAR_KNISA = int.Parse(drIdkun["MISPAR_KNISA"].ToString());
           
            return ObjIdkunRashemet;
        }

        private void UpdateIdkunRashemet(int iMisparSidur, DateTime dShatHatchala, int iMisparKnisa, DateTime dShatYetzia, DateTime dShatYetziaNew, int flag)
        {
            DataRow[] drIdkunim;
            OBJ_IDKUN_RASHEMET ObjIdkunRashemet;

            try
            {
                //drIdkunim = _dtIdkuneyRashemet.Select("MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime') AND MISPAR_KNISA=" + iMisparKnisa + " AND SHAT_YETZIA=Convert('" + dShatYetzia.ToString() + "', 'System.DateTime')");
                if (flag > 0)
                    drIdkunim = _dtIdkuneyRashemet.Select("MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime') AND MISPAR_KNISA=" + iMisparKnisa + " AND SHAT_YETZIA=Convert('" + dShatYetzia.ToString() + "', 'System.DateTime') and update_machine=1");
                else
                    drIdkunim = _dtIdkuneyRashemet.Select("MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime') AND MISPAR_KNISA=" + iMisparKnisa + " AND SHAT_YETZIA=Convert('" + dShatYetzia.ToString() + "', 'System.DateTime') and update_machine is null");
                for (int i = 0; i < drIdkunim.Length; i++)
                {
                    ObjIdkunRashemet = FillIdkunRashemet(drIdkunim[i]);
                    ObjIdkunRashemet.NEW_SHAT_YETZIA = dShatYetziaNew;
                    drIdkunim[i]["SHAT_YETZIA"] = dShatYetziaNew;
                    if (flag == 0)
                        drIdkunim[i]["update_machine"] = "1";
                    _oCollIdkunRashemet.Add(ObjIdkunRashemet);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateIdkunRashemet(int iMisparSidur, DateTime dShatHatchala, int iMisparKnisa, DateTime dShatYetzia, DateTime dShatYetziaNew)
        {
            DataRow[] drIdkunim;
            OBJ_IDKUN_RASHEMET ObjIdkunRashemet;

            try
            {

                drIdkunim = _dtIdkuneyRashemet.Select("MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime') AND MISPAR_KNISA=" + iMisparKnisa + " AND SHAT_YETZIA=Convert('" + dShatYetzia.ToString() + "', 'System.DateTime')");
                for (int i = 0; i < drIdkunim.Length; i++)
                {
                    ObjIdkunRashemet = FillIdkunRashemet(drIdkunim[i]);
                    ObjIdkunRashemet.NEW_SHAT_YETZIA = dShatYetziaNew;
                    drIdkunim[i]["SHAT_YETZIA"] = dShatYetziaNew;
                    _oCollIdkunRashemet.Add(ObjIdkunRashemet);
                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CheckIdkunRashemet(string sFieldToChange, int iMisparSidur, DateTime dShatHatchala, int iMisparKnisa, DateTime dShatYetzia)
        {
            bool bHaveIdkun = false;
            DataRow[] drIdkunim;
            try
            {
                drIdkunim = _dtIdkuneyRashemet.Select("shem_db='" + sFieldToChange.ToUpper() + "' AND MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime') AND MISPAR_KNISA=" + iMisparKnisa + " AND SHAT_YETZIA=Convert('" + dShatYetzia.ToString() + "', 'System.DateTime') ");
                if (drIdkunim.Length > 0)
                    bHaveIdkun = true;

                //if (sFieldToChange.ToUpper() == "SHAT_HATCHALA")
                //    bHaveIdkun = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bHaveIdkun;
        }

       
        private void UpdateObjectUpdSidurim(clNewSidurim oNewSidurim)
        {
            try
            {
                for (int j = 0; j <= oCollSidurimOvdimUpd.Count - 1; j++)
                {
                    if ((oCollSidurimOvdimUpd.Value[j].MISPAR_SIDUR == oNewSidurim.SidurOld) && (oCollSidurimOvdimUpd.Value[j].SHAT_HATCHALA == oNewSidurim.ShatHatchalaOld))
                    {
                        oCollSidurimOvdimUpd.Value[j].NEW_MISPAR_SIDUR = oNewSidurim.SidurNew;
                        oCollSidurimOvdimUpd.Value[j].NEW_SHAT_HATCHALA = oNewSidurim.ShatHatchalaNew;
                        //if (oNewSidurim.ShatGmarNew != DateTime.MinValue  && oNewSidurim.ShatGmarNew != oCollSidurimOvdimUpd.Value[j].SHAT_GMAR)
                        //    oCollSidurimOvdimUpd.Value[j].SHAT_GMAR = oNewSidurim.ShatGmarNew;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private OBJ_PEILUT_OVDIM GetUpdPeilutObject(int iSidurIndex, PeilutDM oPeilut, out SourceObj SourceObject, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            OBJ_PEILUT_OVDIM oObjPeilutOvdimUpd = new OBJ_PEILUT_OVDIM();
            SourceObject = SourceObj.Update;
            try
            {
                SidurDM oSidur = (SidurDM)htEmployeeDetails[iSidurIndex];
                for (int i = 0; i <= oCollPeilutOvdimUpd.Count - 1; i++)
                {
                    if ((oCollPeilutOvdimUpd.Value[i].NEW_MISPAR_SIDUR == oSidur.iMisparSidur) && (oCollPeilutOvdimUpd.Value[i].NEW_SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
                        && (oCollPeilutOvdimUpd.Value[i].NEW_SHAT_YETZIA == oPeilut.dFullShatYetzia) && (oCollPeilutOvdimUpd.Value[i].MISPAR_KNISA == oPeilut.iMisparKnisa) && oCollPeilutOvdimUpd.Value[i].UPDATE_OBJECT == 1)
                    {
                        oObjPeilutOvdimUpd = oCollPeilutOvdimUpd.Value[i];
                        SourceObject = SourceObj.Update;
                        break;
                    }
                }
                if (oObjPeilutOvdimUpd.MISPAR_SIDUR == 0)
                {
                    for (int i = 0; i <= oCollPeilutOvdimIns.Count - 1; i++)
                    {
                        if ((oCollPeilutOvdimIns.Value[i].MISPAR_SIDUR == oSidur.iMisparSidur) && (oCollPeilutOvdimIns.Value[i].SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
                            && (oCollPeilutOvdimIns.Value[i].SHAT_YETZIA == oPeilut.dFullShatYetzia) && (oCollPeilutOvdimIns.Value[i].MISPAR_KNISA == oPeilut.iMisparKnisa))
                        {
                            oObjPeilutOvdimUpd = oCollPeilutOvdimIns.Value[i];
                            SourceObject = SourceObj.Insert;
                            break;
                        }
                    }
                   
                } 
                if (oObjPeilutOvdimUpd.MISPAR_SIDUR == 0)
                    {
                        InsertToObjPeilutOvdimForUpdate(ref oPeilut, oObjSidurimOvdimUpd, ref oObjPeilutOvdimUpd);
                        oCollPeilutOvdimUpd.Add(oObjPeilutOvdimUpd);
                        SourceObject = SourceObj.Update;
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oObjPeilutOvdimUpd;
        }

        private OBJ_PEILUT_OVDIM GetUpdPeilutObjectCancel(int iSidurIndex, PeilutDM oPeilut,  OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            OBJ_PEILUT_OVDIM oObjPeilutOvdimUpd = new OBJ_PEILUT_OVDIM();
            try
            {
                SidurDM oSidur = (SidurDM)htEmployeeDetails[iSidurIndex];
                for (int i = 0; i <= oCollPeilutOvdimUpd.Count - 1; i++)
                {
                    if ((oCollPeilutOvdimUpd.Value[i].NEW_MISPAR_SIDUR == oSidur.iMisparSidur) && (oCollPeilutOvdimUpd.Value[i].NEW_SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
                        && (oCollPeilutOvdimUpd.Value[i].NEW_SHAT_YETZIA == oPeilut.dFullShatYetzia) && (oCollPeilutOvdimUpd.Value[i].MISPAR_KNISA == oPeilut.iMisparKnisa) && oCollPeilutOvdimUpd.Value[i].UPDATE_OBJECT == 1)
                    {
                        oObjPeilutOvdimUpd = oCollPeilutOvdimUpd.Value[i];
                        break;
                    }
                }
               
                    if (oObjPeilutOvdimUpd.MISPAR_SIDUR == 0)
                    {
                        InsertToObjPeilutOvdimForUpdate(ref oPeilut, oObjSidurimOvdimUpd, ref oObjPeilutOvdimUpd);
                        oCollPeilutOvdimUpd.Add(oObjPeilutOvdimUpd);
                    }
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oObjPeilutOvdimUpd;
        }


        private OBJ_SIDURIM_OVDIM GetUpdSidurObject(SidurDM oSidur)
        {
            //מביא את הסידור לפי מפתח האינדקס
            OBJ_SIDURIM_OVDIM oObjSidurimOvdim = new OBJ_SIDURIM_OVDIM();
            try{
            for (int i = 0; i <= oCollSidurimOvdimUpd.Count - 1; i++)
            {
                if ((oCollSidurimOvdimUpd.Value[i].NEW_MISPAR_SIDUR == oSidur.iMisparSidur) && (oCollSidurimOvdimUpd.Value[i].NEW_SHAT_HATCHALA == oSidur.dFullShatHatchala) && oCollSidurimOvdimUpd.Value[i].UPDATE_OBJECT == 1)
                {
                    oObjSidurimOvdim = oCollSidurimOvdimUpd.Value[i];
                }
            }

            if (oObjSidurimOvdim.MISPAR_SIDUR == 0)
            {
                for (int i = 0; i <= oCollSidurimOvdimIns.Count - 1; i++)
                {
                    if ((oCollSidurimOvdimIns.Value[i].NEW_MISPAR_SIDUR == oSidur.iMisparSidur) && (oCollSidurimOvdimIns.Value[i].NEW_SHAT_HATCHALA == oSidur.dFullShatHatchala))
                    {
                        oObjSidurimOvdim = oCollSidurimOvdimIns.Value[i];
                    }
                }


                if (oObjSidurimOvdim.MISPAR_SIDUR == 0)
                {
                    InsertToObjSidurimOvdimForUpdate(ref oSidur, ref oObjSidurimOvdim);
                    oCollSidurimOvdimUpd.Add(oObjSidurimOvdim);
                }
            }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oObjSidurimOvdim;
        }

        private void FixedMisparMatalatVisa02(ref SidurDM oSidur, int iSidurIndex,ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            PeilutDM oPeilut;
            int iNewMisparSidur = 0;
            SourceObj SourceObject;
            bool bHaveNesiatNamak = false;
            try
            {
                bHaveNesiatNamak = oSidur.htPeilut.Values.Cast<PeilutDM>().ToList().Any(Peilut => Peilut.iMakatType == enMakatType.mNamak.GetHashCode());
                if (!bHaveNesiatNamak)
                {
                    if (oSidur.bSectorVisaExists && !oSidur.bSidurVisaKodExists)
                    {
                        clNewSidurim oNewSidurim = FindSidurOnHtNewSidurim(oSidur.iMisparSidur, oSidur.dFullShatHatchala);

                        oNewSidurim.SidurIndex = iSidurIndex;
                        oNewSidurim.SidurOld = oSidur.iMisparSidur;

                        ///ויזת צה"ל 
                        if (oSidur.iSectorVisa == 1)
                        {
                            oNewSidurim.SidurNew = 99110;
                        }
                        else
                        {//ויזת פנים 
                            oNewSidurim.SidurNew = 99101;
                        }

                        iNewMisparSidur = oNewSidurim.SidurNew;
                        oNewSidurim.ShatHatchalaNew = oSidur.dFullShatHatchala;

                        UpdateObjectUpdSidurim(oNewSidurim);

                        DataRow[] drSidurMeyuchad;
                        drSidurMeyuchad = _dtSidurimMeyuchadim.Select("mispar_sidur=" + iNewMisparSidur);
                        var sidurManager = ServiceLocator.Current.GetInstance<ISidurManager>();

                        oSidur = sidurManager.CreateClsSidurFromSidurMeyuchad(oSidur, _dCardDate, iNewMisparSidur, drSidurMeyuchad[0]);
                        oObjSidurimOvdimUpd.NEW_MISPAR_SIDUR = iNewMisparSidur;

                        if (oObjSidurimOvdimUpd.NEW_MISPAR_SIDUR == 99101)
                        {
                            oObjSidurimOvdimUpd.MIVTZA_VISA = 1;
                        }

                        if (oSidur.htPeilut.Count == 0)
                        {
                            OBJ_PEILUT_OVDIM oObjPeilutOvdimIns = new OBJ_PEILUT_OVDIM();
                            InsertToObjPeilutOvdimForInsert(ref oSidur, ref oObjPeilutOvdimIns);
                            oObjPeilutOvdimIns.MISPAR_SIDUR = iNewMisparSidur;
                            oObjPeilutOvdimIns.TAARICH = _dCardDate;
                            oObjPeilutOvdimIns.SHAT_YETZIA = oSidur.dFullShatHatchala;
                            oObjPeilutOvdimIns.SHAT_HATCHALA_SIDUR = oSidur.dFullShatHatchala;
                            oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse("50000000");
                            oObjPeilutOvdimIns.MISPAR_VISA = oNewSidurim.SidurOld;
                            oObjPeilutOvdimIns.BITUL_O_HOSAFA = 4;
                            oCollPeilutOvdimIns.Add(oObjPeilutOvdimIns);

                            PeilutDM oPeilutNew = CreatePeilut(_iMisparIshi, _dCardDate, oObjPeilutOvdimIns, dtTmpMeafyeneyElements);
                            oPeilutNew.iBitulOHosafa = 4;
                            oSidur.htPeilut.Insert(0, 1, oPeilutNew);
                        }
                        else
                        {
                            for (int j = 0; j < ((SidurDM)(htEmployeeDetails[iSidurIndex])).htPeilut.Count; j++)
                            {
                                oPeilut = (PeilutDM)((SidurDM)(htEmployeeDetails[iSidurIndex])).htPeilut[j];
                                oObjPeilutOvdimUpd = GetUpdPeilutObject(iSidurIndex, oPeilut, out SourceObject, oObjSidurimOvdimUpd);

                                //שינוי 02 - שלב שני
                                FixedMisparMatalatVisa02(ref oObjPeilutOvdimUpd, ref oPeilut, oSidur, oNewSidurim.SidurOld, SourceObject);
                                oSidur.htPeilut[j] = oPeilut;
                            }
                            //UpdatePeiluyotMevutalotYadani(iSidurIndex,oNewSidurim, oObjSidurimOvdimUpd);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 2, _iMisparIshi, _dCardDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "FixedMisparMatalatVisa02: " + ex.Message, null);
                _bSuccsess = false;
            }
        }

        private void FixedMisparMatalatVisa02(ref OBJ_PEILUT_OVDIM oObjPeilutOvdimUpd, ref PeilutDM oPeilut, SidurDM oSidur, int iOldMisparSidur, SourceObj SourceObject)
        {
            try
            {
                if (SourceObject == SourceObj.Insert)
                {
                    oObjPeilutOvdimUpd.MISPAR_SIDUR = oSidur.iMisparSidur;
                }
                else
                {
                    oObjPeilutOvdimUpd.NEW_MISPAR_SIDUR = oSidur.iMisparSidur;
                    oObjPeilutOvdimUpd.UPDATE_OBJECT = 1;

                }
                oObjPeilutOvdimUpd.MISPAR_VISA = iOldMisparSidur;
                oObjPeilutOvdimUpd.MAKAT_NESIA = long.Parse("50" + oObjPeilutOvdimUpd.MISPAR_VISA);
                    
                PeilutDM oPeilutNew = CreatePeilut(_iMisparIshi, _dCardDate, oPeilut, oObjPeilutOvdimUpd.MAKAT_NESIA, dtTmpMeafyeneyElements);
                oPeilutNew.lMisparVisa = oObjPeilutOvdimUpd.MISPAR_VISA;
                oPeilutNew.iPeilutMisparSidur = oSidur.iMisparSidur;
         //       oPeilutNew.iMakatType = enMakatType.mVisa.GetHashCode();
                oPeilut = oPeilutNew;

            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E",null, 2, _iMisparIshi, _dCardDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala,oPeilut.dFullShatYetzia, oPeilut.iMisparKnisa, "FixedMisparMatalatVisa02: " + ex.Message, null);
                _bSuccsess = false;
            }
        }

        

        private void FixedPitzulHafsaka06(ref SidurDM oSidur, int iNextSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            int iCountPitzul,iMinPaar;
            SidurDM oNextSidur = null;
            double dMinutsPitzul;
            DateTime dShatKnisatShabat;
            bool bSidurTafkid,bNextSidurTafkid;
            bool bSidurNahagut, bNextSidurNahagut;
            try
            {
               
                    oObjSidurimOvdimUpd.PITZUL_HAFSAKA = 0;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                    oSidur.sPitzulHafsaka = "0";
                    //לא ניתן פיצול במידה וקיים סידור יחיד ביום.
                    if (htEmployeeDetails.Count > 1)
                    {
                        iCountPitzul = 0;
                        if (oCollSidurimOvdimUpd.Value != null)
                        {
                            iCountPitzul = oCollSidurimOvdimUpd.Value.Cast<OBJ_SIDURIM_OVDIM>().ToList().Count(sidur => sidur.PITZUL_HAFSAKA == 1 && sidur.UPDATE_OBJECT==1);
                        }
                        // העובד זכאי למקסימום פיצולים ביום עבודה לפי פרמטר 170. 
                        if (iCountPitzul < _oParameters.iMaxPitzulLeyom && (oSidur.dFullShatGmar != DateTime.MinValue && oSidur.dFullShatHatchala != DateTime.MinValue))
                        {
                            //אין פיצול בשבתון 
                            if (!clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim,iSugYom,oSidur.dSidurDate))
                            {
                                if (iNextSidur < htEmployeeDetails.Values.Cast<SidurDM>().ToList().Count)
                                {
                                    oNextSidur = (SidurDM)htEmployeeDetails[iNextSidur];
                                    if (oNextSidur.dFullShatHatchala == DateTime.MinValue) return;
                                    dMinutsPitzul = (oNextSidur.dFullShatHatchala - oSidur.dFullShatGmar).TotalMinutes;
                                }
                                else
                                {
                                    return;
                                }

                                //אם שני הסידורים מסומנים " לתשלום" - לא מגיע פיצול.
                                if (oSidur.iLoLetashlum == 0 && oNextSidur.iLoLetashlum == 0)
                                {
                                    // אם 2 הסידורים הם סידורי מפה (לא סידורי ***99) ו-2 מספרי הסידור זהים - לא מגיע פיצול .
                                    if (!oSidur.bSidurMyuhad && !oNextSidur.bSidurMyuhad && oSidur.iMisparSidur == oNextSidur.iMisparSidur)
                                    {
                                        return;
                                    }

                                    if (oSidur.dFullShatGmar >= _oParameters.dSummerStart && oSidur.dFullShatGmar < _oParameters.dSummerEnd.AddDays(1))
                                    {
                                        iMinPaar = _oParameters.iMinHefreshSidurimLepitzulSummer;
                                    }
                                    else
                                    {
                                        iMinPaar = _oParameters.iMinHefreshSidurimLeptzulWinter;
                                    }

                                    //ביום שישי/ערב חג (לפי ה- oracle או טבלת ימים מיוחדים) יחושב פיצול רק במידה ושעת הגמר של הסידור השני היא לפני כניסת שבת. 
                                    if (oSidur.sSidurDay == enDay.Shishi.GetHashCode().ToString() || oSidur.sErevShishiChag == "1")
                                    { 
                                        dShatKnisatShabat = _oParameters.dKnisatShabat;
                                        if (oNextSidur.dFullShatHatchala > dShatKnisatShabat)
                                        {
                                            return;
                                        }
                                        else iMinPaar = _oParameters.iMinHefreshSidurimLepitzulSummer;
                                    }

                                    if (dMinutsPitzul >= iMinPaar)
                                    {
                                         DataRow[] drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, _dCardDate, _dtSugSidur);

                                         if (IsSidurNahagut(drSugSidur, oSidur) || IsSidurNihulTnua(drSugSidur, oSidur) || IsSugAvodaKupai(ref oSidur, _dCardDate))
                                            bSidurNahagut=true;
                                        else bSidurNahagut = false;
                                        drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oNextSidur.iSugSidurRagil, _dCardDate, _dtSugSidur);

                                        if (IsSidurNahagut(drSugSidur, oNextSidur) || IsSidurNihulTnua(drSugSidur, oNextSidur) || IsSugAvodaKupai(ref oSidur, _dCardDate))
                                            bNextSidurNahagut=true;
                                        else bNextSidurNahagut = false;

                                        if (oSidur.bSidurTafkid) // || IsSugAvodaKupai(ref oSidur, _dCardDate))
                                        { bSidurTafkid = true; }
                                        else { bSidurTafkid = false; }

                                        if (oNextSidur.bSidurTafkid)// || IsSugAvodaKupai(ref oNextSidur, _dCardDate))
                                        { bNextSidurTafkid = true; }
                                        else { bNextSidurTafkid = false; }

                                        if (bSidurNahagut &&  bNextSidurNahagut)
                                        {
                                            oObjSidurimOvdimUpd.PITZUL_HAFSAKA = 1;
                                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                                            oSidur.sPitzulHafsaka = "1";
                                        }
                                        else if (_oMeafyeneyOved.IsMeafyenExist(41) && (bSidurTafkid && oSidur.iZakayLepizul > 0 && bNextSidurTafkid && oNextSidur.iZakayLepizul > 0))
                                        {
                                            oObjSidurimOvdimUpd.PITZUL_HAFSAKA = 1;
                                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                                            oSidur.sPitzulHafsaka = "1";
                                        }
                                    }
                                }
                            }
                        }
                    }
             
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 6, oSidur.iMisparIshi,oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "FixedPitzulHafsaka06: " + ex.Message, null);
                _bSuccsess = false;
            }
        }

        private void FixedShatHatchalaAndGmarToMafilim26(ref SidurDM oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, bool bIdkunRashShatHatchala, bool bIdkunRashShatGmar)
        {
            DateTime dRequiredShatHatchala = DateTime.Now;
            DateTime dRequiredShatGmar = DateTime.Now;
            DateTime dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM;
            DateTime dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM;
            bool bFromMeafyenHatchala, bFromMeafyenGmar;
            try
            {
                if ( (oOvedYomAvodaDetails.iIsuk == 122 || oOvedYomAvodaDetails.iIsuk == 123 || oOvedYomAvodaDetails.iIsuk == 124 || oOvedYomAvodaDetails.iIsuk == 127)
                    && oSidur.sSugAvoda==enSugAvoda.Shaon.GetHashCode().ToString() )
                {
                    GetMeafyeneyMafilim(oSidur,oSidur.dFullShatHatchala, oSidur.dFullShatGmar, out  bFromMeafyenHatchala, out  bFromMeafyenGmar, ref dRequiredShatHatchala, ref dRequiredShatGmar);

                    dShatHatchalaLetashlumToUpd = dRequiredShatHatchala;
                    dShatGmarLetashlumToUpd = dRequiredShatGmar;
                    
                    SetShatHatchalaGmarKizuz(ref oSidur, ref oObjSidurimOvdimUpd, dRequiredShatHatchala, dRequiredShatGmar, ref dShatHatchalaLetashlumToUpd, ref dShatGmarLetashlumToUpd);
                    
                    //oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = dRequiredShatHatchala;
                    //oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = dRequiredShatGmar;
                    //oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;

                    //oSidur.dFullShatHatchalaLetashlum = oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM;
                    //oSidur.sShatHatchalaLetashlum = oSidur.dFullShatHatchalaLetashlum.ToString("HH:mm");
                    //oSidur.dFullShatGmar = oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM;
                    //oSidur.sShatGmarLetashlum = oSidur.dFullShatGmarLetashlum.ToString("HH:mm");

                    if (!bIdkunRashShatHatchala && dShatHatchalaLetashlumToUpd != oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM)
                    {
                        oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = dShatHatchalaLetashlumToUpd;
                        oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                        oSidur.dFullShatHatchalaLetashlum = oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM;
                        oSidur.sShatHatchalaLetashlum = oSidur.dFullShatHatchalaLetashlum.ToString("HH:mm");
                        if (oSidur.dFullShatHatchalaLetashlum.Year < DateHelper.cYearNull)
                            oSidur.sShatHatchalaLetashlum = "";
                    }

                    if (!bIdkunRashShatGmar && dShatGmarLetashlumToUpd != oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM)
                    {
                        oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = dShatGmarLetashlumToUpd;
                        oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                        oSidur.dFullShatGmarLetashlum = oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM;
                        oSidur.sShatGmarLetashlum = oSidur.dFullShatGmarLetashlum.ToString("HH:mm");
                    }
                   
                }
                
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 26, oSidur.iMisparIshi,oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "FixedShatHatchalaAndGmarToMafilim26: " + ex.Message, null);
                _bSuccsess = false;
            }
        }

        private void FixedShatHatchalaAndGmarSidurMapa27(ref SidurDM oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            string sug_sidur = "";
            try
            {
                DataRow[] drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, oSidur.dSidurDate, _dtSugSidur);
                if (drSugSidur.Length>0)
                    sug_sidur= drSugSidur[0]["sug_sidur"].ToString();
                
                if (!oSidur.bSidurMyuhad && sug_sidur != "69")
                {
                    oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = oSidur.dFullShatHatchala;
                    oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM =oSidur.dFullShatGmar;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;

                    oSidur.dFullShatHatchalaLetashlum = oSidur.dFullShatHatchala;
                    oSidur.sShatHatchalaLetashlum = oSidur.dFullShatHatchalaLetashlum.ToString("HH:mm");
                    oSidur.dFullShatGmarLetashlum = oSidur.dFullShatGmar;
                    oSidur.sShatGmarLetashlum = oSidur.dFullShatGmarLetashlum.ToString("HH:mm");
                    if (oSidur.dFullShatHatchalaLetashlum.Year < DateHelper.cYearNull)
                        oSidur.sShatHatchalaLetashlum = "";
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 27, oSidur.iMisparIshi,oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "FixedShatHatchalaAndGmarSidurMapa27: " + ex.Message, null);
                _bSuccsess = false;
            }
        }

        //private void SetShaotSidurKupai(int Index,ref SidurDM oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        //{
        //    //string sug_sidur = "";
        //    DataRow[] drSugSidur;
        //    SidurDM ESidur;
        //    DateTime dShatHatchala, dShatGmar;
        //    try
        //    {
        //        if (oOvedYomAvodaDetails.iKodHevra != clGeneral.enEmployeeType.enEggedTaavora.GetHashCode())
        //        {
        //            drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, oSidur.dSidurDate, _dtSugSidur);

        //            if (oSidur.sSugAvoda == enSugAvoda.Kupai.GetHashCode().ToString() || (drSugSidur.Length > 0 && drSugSidur[0]["sug_avoda"].ToString() == enSugAvoda.Kupai.GetHashCode().ToString()))
        //            {
        //                for (int i = 0; i < htEmployeeDetails.Count; i++)
        //                {
        //                    if (i != Index)
        //                    {
        //                        ESidur = (SidurDM)htEmployeeDetails[i];
        //                        if (ESidur.sSugAvoda == enSugAvoda.Shaon.GetHashCode().ToString())
        //                        {
        //                            if ((Math.Abs(oSidur.dFullShatHatchala.Subtract(ESidur.dFullShatHatchala).TotalMinutes) <= 60) &&
        //                                (Math.Abs(ESidur.dFullShatGmar.Subtract(oSidur.dFullShatGmar).TotalMinutes) <= 60))
        //                            {
        //                                dShatHatchala = oSidur.dFullShatHatchala > ESidur.dFullShatHatchala ? oSidur.dFullShatHatchala : ESidur.dFullShatHatchala;
        //                                dShatGmar = oSidur.dFullShatGmar < ESidur.dFullShatGmar?oSidur.dFullShatGmar : ESidur.dFullShatGmar;

        //                                if (dShatHatchala != oObjSidurimOvdimUpd.SHAT_HATCHALA)
        //                                {
        //                                    UpdateShatHatchala(ref oSidur, i, dShatHatchala, ref oObjSidurimOvdimUpd);
        //                                }
        //                                if (dShatGmar != oObjSidurimOvdimUpd.SHAT_GMAR)
        //                                {
        //                                    oObjSidurimOvdimUpd.SHAT_GMAR = dShatGmar;
        //                                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;

        //                                    oSidur.dFullShatGmar = oObjSidurimOvdimUpd.SHAT_GMAR;
        //                                    oSidur.sShatGmar = oSidur.dFullShatGmar.ToString("HH:mm");
        //                                }
        //                            }
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //   //     clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errFirstDayShlilatRishayon195.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurNAhagutValid161: " + ex.Message, null);
        //    }
        //}

        private void MergerSiduryMapa28()
        {
            SidurDM oSidur, oSidurPutzal;
            PeilutDM oLastPeilut, oPeilut, oFirstPeilut;
            string sMakat;
            DataSet dsSidur;
            int iResult;
            long lMakatNesia;
            string sShaa;
            bool bSidurOkev = false;
            bool bHaveSidur, bCancelHachanatMechona;
            int l, I, iCountPeiluyot, iCountSidurim;
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimDel;
            DateTime dShatYetzia;
            List<SidurDM> ListSidurim;
            try
            {
                iCountSidurim = htEmployeeDetails.Values.Count;
                I = 0;

                if (iCountSidurim > 0)
                {
                    do
                    {
                        oSidur = (SidurDM)htEmployeeDetails[I];
                        bHaveSidur = false;
                        bSidurOkev = false;
                        if (!oSidur.bSidurMyuhad)
                        {
                            //אם מזהים בטבלת סידורים עובדים TB_SIDURIM_OVDIM עבור מ.א. + תאריך את אותו מס' סידור עם שעת התחלה שונה אז בודקים האם זה סידור שפוצל.
                            oSidurPutzal = oSidur;

                            ListSidurim = htEmployeeDetails.Values.Cast<SidurDM>().ToList().FindAll(Sidur => (Sidur.dFullShatHatchala < oSidur.dFullShatHatchala)).ToList();
                            if (ListSidurim.Count > 1)
                            {
                                ListSidurim.Sort(delegate(SidurDM first, SidurDM second)
                                                    {
                                                        return first.dFullShatHatchala.CompareTo(second.dFullShatHatchala);
                                                    });
                            }
                            if (ListSidurim.Count > 0)
                            {
                                l = ListSidurim.Count - 1;
                                oSidurPutzal = (SidurDM)ListSidurim[l];

                                if (oSidurPutzal.iMisparSidur == oSidur.iMisparSidur)
                                {
                                    bHaveSidur = true;
                                }


                                if (bHaveSidur && oSidurPutzal.htPeilut.Values.Count > 0 && oSidur.htPeilut.Values.Count > 0)
                                {
                                    oLastPeilut = (PeilutDM)oSidurPutzal.htPeilut[oSidurPutzal.htPeilut.Count -1];// oSidurPutzal.htPeilut.Values.Cast<PeilutDM>().ToList().LastOrDefault(peilut => (peilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "756"));
                                    oFirstPeilut = oSidur.htPeilut.Values.Cast<PeilutDM>().ToList().FirstOrDefault(peilut => (peilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != enElementHachanatMechona.Element701.GetHashCode().ToString() && peilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != enElementHachanatMechona.Element712.GetHashCode().ToString() && peilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != enElementHachanatMechona.Element711.GetHashCode().ToString() && peilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "756"));
                                    if (oFirstPeilut != null && oLastPeilut != null)
                                    {
                                        var kavimDal = ServiceLocator.Current.GetInstance<IKavimDAL>();
                                        dsSidur = kavimDal.GetSidurAndPeiluyotFromTnua(oSidurPutzal.iMisparSidur, oSidurPutzal.dSidurDate, 1, out iResult);
                                        if (iResult == 0)
                                        {
                                            for (int i = 0; i < dsSidur.Tables[1].Rows.Count; i++)
                                            {

                                                sShaa = dsSidur.Tables[1].Rows[i]["SHAA"].ToString();
                                                lMakatNesia = long.Parse(dsSidur.Tables[1].Rows[i]["MAKAT8"].ToString());
                                                dShatYetzia = DateHelper.GetDateTimeFromStringHour(sShaa, oSidur.dFullShatHatchala);
                                                if (oLastPeilut.lMakatNesia == lMakatNesia && oLastPeilut.dFullShatYetzia == dShatYetzia && i + 1 < dsSidur.Tables[1].Rows.Count)
                                                {
                                                    lMakatNesia = long.Parse(dsSidur.Tables[1].Rows[i + 1]["MAKAT8"].ToString());
                                                    sShaa = dsSidur.Tables[1].Rows[i + 1]["SHAA"].ToString();

                                                    if ((lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == enElementHachanatMechona.Element712.GetHashCode().ToString() || lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == enElementHachanatMechona.Element711.GetHashCode().ToString())  && i + 2 < dsSidur.Tables[1].Rows.Count)
                                                    {
                                                        lMakatNesia = long.Parse(dsSidur.Tables[1].Rows[i + 2]["MAKAT8"].ToString());

                                                        sShaa = dsSidur.Tables[1].Rows[i + 2]["SHAA"].ToString();
                                                    }

                                                    dShatYetzia = DateHelper.GetDateTimeFromStringHour(sShaa, oSidur.dFullShatHatchala);
                                                    if (oFirstPeilut.lMakatNesia == lMakatNesia && oFirstPeilut.dFullShatYetzia == dShatYetzia)
                                                    {
                                                        bSidurOkev = true;
                                                        break;
                                                    }
                                                }

                                            }
                                        }
                                    }


                                    if (bSidurOkev)
                                    {
                                        oObjSidurimOvdimDel = new OBJ_SIDURIM_OVDIM();
                                        InsertToObjSidurimOvdimForDelete(ref oSidur, ref oObjSidurimOvdimDel);
                                        oCollSidurimOvdimDel.Add(oObjSidurimOvdimDel);

                                        bCancelHachanatMechona = false;

                                        iCountPeiluyot = oSidur.htPeilut.Values.Count;
                                        int j = 0;

                                        if (iCountPeiluyot > 0)
                                        {
                                            do
                                            {

                                                oPeilut = (PeilutDM)oSidur.htPeilut[j];
                                                sMakat = oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3);

                                                //לבדוק האם מס' הרכב TB_PEILUT_OVDIM.OTO_NO בפעילות האחרונה בסידור1 שונה מ- מס' הרכב בפעילות הראשונה שהועתקה מסידור2 אם כן, יש להשאיר את הכנת המכונה מק"ט 701,711,712. אחרת, אם מס' הרכב זהים יש לבטל את הכנת המכונה שהועברה מסידור2 TB_PEILUT_OVDIM. BITUL_O_HOSAFA=3.
                                                //if (j == 0)
                                                //{
                                                //    if (oLastPeilut.lOtoNo == oPeilut.lOtoNo)
                                                //    { bCancelHachanatMechona = true; }
                                                //}

                                                //if (!bCancelHachanatMechona || (bCancelHachanatMechona && sMakat != enElementHachanatMechona.Element701.GetHashCode().ToString() && sMakat != enElementHachanatMechona.Element712.GetHashCode().ToString() && sMakat != enElementHachanatMechona.Element711.GetHashCode().ToString()))
                                                //{
                                                    OBJ_PEILUT_OVDIM oObjPeilutOvdimIns = new OBJ_PEILUT_OVDIM();
                                                    InsertToObjPeilutOvdimForUpdate(ref oPeilut, oObjSidurimOvdimDel, ref oObjPeilutOvdimIns);
                                                    oObjPeilutOvdimIns.MISPAR_SIDUR = oSidurPutzal.iMisparSidur;
                                                    oObjPeilutOvdimIns.SHAT_HATCHALA_SIDUR = oSidurPutzal.dFullShatHatchala;
                                                    oObjPeilutOvdimIns.BITUL_O_HOSAFA = 4;
                                                    CopyPeilutToObj(ref oObjPeilutOvdimIns, ref oPeilut);
                                                    oCollPeilutOvdimIns.Add(oObjPeilutOvdimIns);

                                                    PeilutDM oPeilutNew = CreatePeilut(_iMisparIshi, _dCardDate, oPeilut, oPeilut.lMakatNesia, dtTmpMeafyeneyElements);
                                                    oPeilutNew.iBitulOHosafa = 4;
                                                    oPeilutNew.iPeilutMisparSidur = oSidurPutzal.iMisparSidur;
                                                    oSidurPutzal.htPeilut.Add(28 * oSidurPutzal.htPeilut.Count + 1, oPeilut);
                                                //}

                                                oObjPeilutOvdimDel = new OBJ_PEILUT_OVDIM();
                                                InsertToObjPeilutOvdimForDelete(ref oPeilut, ref oSidur, ref oObjPeilutOvdimDel);
                                                oCollPeilutOvdimDel.Add(oObjPeilutOvdimDel);
                                                oSidur.htPeilut.RemoveAt(j);

                                                iCountPeiluyot -= 1;

                                            }
                                            while (j < iCountPeiluyot);
                                        }
                                        htEmployeeDetails.RemoveAt(I);
                                        iCountSidurim -= 1;
                                        I -= 1;
                                     }

                                }
                            }
                        }
                        I += 1;
                    } while (I < iCountSidurim);
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, _iMisparIshi, "E", 28, _dCardDate, "MergerSiduryMapa28: " + ex.Message);
                _bSuccsess = false;
            }
        }

        private void SiduryMapaWhithStatusNullLoLetashlum29()
        {
            SidurDM oSidur = null;
            PeilutDM oPeilut = null;
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd = null;
            bool bHaveSidurFromMatala = false;
            bool bHaveSidurVisaFromMapa = false;
            int i = 0;
            try
            {
                if (htEmployeeDetails != null)
                    for (i = 0; i < htEmployeeDetails.Count; i++)
                    {
                        oSidur = (SidurDM)htEmployeeDetails[i];
                        bHaveSidurVisaFromMapa = false;
                        bHaveSidurFromMatala = false;
                        //סידור מפה: אינו מתחיל בספרות 99/
                        //סידור מיוחד שמקורו במטלה מהמפה (באחת הרשומות של הפעילויות בסידור0< TB_peilut_Ovdim. Mispar_matala)/
                        //סידור ויזה שהגיע מהמפה (בפעילות שהיא מסוג מק"ט 5 קיים ערך בשדה MISPAR_VISA
                        if (oSidur.bSidurMyuhad)
                        {
                            for (int j = 0; j < oSidur.htPeilut.Count; j++)
                            {
                                oPeilut = (PeilutDM)oSidur.htPeilut[j];

                                if (oPeilut.lMisparMatala > 0)
                                {
                                    bHaveSidurFromMatala = true;
                                }
                                if (oSidur.bSidurVisaKodExists || oSidur.iSectorVisa == 0 || oSidur.iSectorVisa == 1)
                                {
                                    if (oPeilut.iMakatType == enMakatType.mVisa.GetHashCode() && oPeilut.lMisparVisa > 0)
                                    {
                                        bHaveSidurVisaFromMapa = true;
                                    }
                                }
                            }
                        }

                        //&& _iLoginUserId != oSidur.iMisparIshi
                        if (((!oSidur.bSidurMyuhad && oSidur.iSugSidurRagil!=73) || (oSidur.bSidurMyuhad && bHaveSidurFromMatala) || bHaveSidurVisaFromMapa) && _oOvedYomAvodaDetails.iMeasherOMistayeg == -1)
                        {
                            if (!CheckIdkunRashemet("LO_LETASHLUM", oSidur.iMisparSidur, oSidur.dFullShatHatchala))
                            {
                                oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur);

                                oSidur.iLoLetashlum = 1;
                                oSidur.iKodSibaLoLetashlum = 16;
                                oObjSidurimOvdimUpd.LO_LETASHLUM = 1;
                                oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM = 16;
                                oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;

                                htEmployeeDetails[i] = oSidur;
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, _iMisparIshi, "E", 29, _dCardDate, "SiduryMapaLoLetashlum29: " + ex.Message);
                _bSuccsess = false;
            }
        }

        private void CopyPeilutToObj(ref OBJ_PEILUT_OVDIM oObjPeilutOvdimIns,ref PeilutDM oPeilut)
        {
            oObjPeilutOvdimIns.OTO_NO = oPeilut.lOtoNo;
            oObjPeilutOvdimIns.DAKOT_BAFOAL = oPeilut.iDakotBafoal;
            oObjPeilutOvdimIns.KISUY_TOR = oPeilut.iKisuyTor;
            if (oPeilut.iKmVisa > 0)
                oObjPeilutOvdimIns.KM_VISA = oPeilut.iKmVisa;
            oObjPeilutOvdimIns.MISPAR_SIDURI_OTO = oPeilut.lMisparSiduriOto;
            oObjPeilutOvdimIns.MISPAR_VISA = oPeilut.lMisparVisa;
            if (!string.IsNullOrEmpty(oPeilut.sSnifTnua))
                oObjPeilutOvdimIns.SNIF_TNUA = int.Parse(oPeilut.sSnifTnua);
            oObjPeilutOvdimIns.MISPAR_MATALA = oPeilut.lMisparMatala;
            oObjPeilutOvdimIns.SHILUT_NETZER = oPeilut.sShilutNetzer;
            if (oPeilut.dShatYetziaNetzer != DateTime.MinValue)
                oObjPeilutOvdimIns.SHAT_YETZIA_NETZER = oPeilut.dShatYetziaNetzer;
            if (oPeilut.dShatBhiratNesiaNetzer != DateTime.MinValue)
                oObjPeilutOvdimIns.SHAT_BHIRAT_NESIA_NETZER = oPeilut.dShatBhiratNesiaNetzer;
            oObjPeilutOvdimIns.OTO_NO_NETZER = oPeilut.lOtoNoNetzer;
            oObjPeilutOvdimIns.MISPAR_SIDUR_NETZER = oPeilut.iMisparSidurNetzer;
            oObjPeilutOvdimIns.HEARA = oPeilut.sHeara;
            oObjPeilutOvdimIns.MIKUM_BHIRAT_NESIA_NETZER = oPeilut.sMikumBhiratNesiaNetzer;
            oObjPeilutOvdimIns.KOD_SHINUY_PREMIA = oPeilut.iKodShinuyPremia;
            if (oPeilut.bImutNetzer)
                oObjPeilutOvdimIns.IMUT_NETZER = 1;
            oObjPeilutOvdimIns.MAKAT_NETZER = oPeilut.lMakatNetzer;
            oObjPeilutOvdimIns.TEUR_NESIA = oPeilut.sMakatDescription;
        }

        private void FixedShatHamtana25()
        {
            try
            {
                bool hasSidurEilat = false;
               // bool validForHamtana = false;
                SidurDM oFirstSidurEilat = null;
                SidurDM oSecondSidurEilat = null;
                PeilutDM oFirstPeilutEilat = null;
                PeilutDM oSecondPeilutEilat = null;
                PeilutDM tmpPeilut = null;

                htEmployeeDetails.Values
                                 .Cast<SidurDM>()
                                 .ToList()
                                 .ForEach
                                 (
                                    sidur =>
                                    {
                                        if (sidur.IsLongEilatTrip( out tmpPeilut, _oParameters.fOrechNesiaKtzaraEilat))
                                        {
                                            if (!hasSidurEilat)
                                            {
                                                hasSidurEilat = true;
                                                oFirstSidurEilat = sidur;
                                                oFirstPeilutEilat = tmpPeilut;
                                            }
                                            else
                                            {
                                                oSecondSidurEilat = sidur;
                                                oSecondPeilutEilat = tmpPeilut;

                                                //if (oFirstPeilutEilat.dFullShatYetzia.Date == tmpPeilut.dFullShatYetzia.Date
                                                //   || (oFirstPeilutEilat.dFullShatYetzia.Hour >= 20 && oFirstPeilutEilat.dFullShatYetzia.Date.AddDays(1) == tmpPeilut.dFullShatYetzia.Date))
                                                //{
                                                //if (tmpPeilut.iSnifAchrai != oOvedYomAvodaDetails.iSnifTnua)
                                                //    {
                                                //        validForHamtana = true;
                                                //    }
                                                //}
                                            }
                                        }
                                    }
                                 );

                if (oFirstSidurEilat != null && oSecondSidurEilat != null)
                {
                    int firstSidurIndex = htEmployeeDetails.Values.Cast<SidurDM>().ToList().FindIndex(sidur => (sidur.iMisparSidur == oFirstSidurEilat.iMisparSidur && sidur.dFullShatHatchala == oFirstSidurEilat.dFullShatHatchala));
                    int secondSidurIndex = htEmployeeDetails.Values.Cast<SidurDM>().ToList().FindIndex(sidur => (sidur.iMisparSidur == oSecondSidurEilat.iMisparSidur && sidur.dFullShatHatchala == oSecondSidurEilat.dFullShatHatchala));
                    if (firstSidurIndex + 1 == secondSidurIndex && !(_oOvedYomAvodaDetails.iSnifTnua == oSecondPeilutEilat.iSnifAchrai)) 
                    {
                        HosafatHamtana(firstSidurIndex, secondSidurIndex);
                    }
                    //else 
                    //{
                    //    HosafatHamtana(firstSidurIndex, secondSidurIndex);
                    //}

                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0,  _iMisparIshi, "E",25, _dCardDate, "FixedShatHamtana25: " + ex.Message);
                _bSuccsess = false;
            }
        }

        private void DeleteSidureyRetzifut()
        {
            SidurDM oSidur;
            int iCountSidurim,I;
            try
            {
                iCountSidurim = htEmployeeDetails.Values.Count;
                I = 0;

                if (iCountSidurim > 0)
                {
                    do
                    {
                        oSidur = (SidurDM)htEmployeeDetails[I];
                        if (oSidur.iMisparSidur == 99500 || oSidur.iMisparSidur == 99501)
                        {
                            oObjSidurimOvdimDel = new OBJ_SIDURIM_OVDIM();
                            InsertToObjSidurimOvdimForDelete(ref oSidur, ref oObjSidurimOvdimDel);
                            oObjSidurimOvdimDel.UPDATE_OBJECT = 2;
                            oCollSidurimOvdimDel.Add(oObjSidurimOvdimDel);
                            htEmployeeDetails.RemoveAt(I);
                            iCountSidurim -= 1;
                            I -= 1;
                        }
                        I += 1;
                    } while (I < iCountSidurim);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, _iMisparIshi, "E", 14, _dCardDate, "DeleteSidureyRetzifut: " + ex.Message);
                _bSuccsess = false;
            }
        }

        private void HosafatHamtana(int iIndexFrom, int iIndexTo)
        {
            int iSumElement = 0;
            int iSumElementLesidur = 0;
            SidurDM oSidur = null;
            SidurDM oSidurNext = null;
            PeilutDM oElement, oPeilut;
            double dPaar, dZmanElement;
            string sZmanElement;
            //bool bHaveIdkun = false;
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
            SourceObj SourceObject;
            int j;
            try
            {
                for (int I = iIndexFrom; I < iIndexTo; I++)
                {
                    oElement = null;
                    oSidur = (SidurDM)htEmployeeDetails[I];
                    oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur);
                    for (j = 0; j <= oSidur.htPeilut.Values.Count - 1; j++)
                    {
                        oPeilut = (PeilutDM)oSidur.htPeilut[j];
                        if (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "735")
                        {
                            oElement=oPeilut;
                            break;
                        }
               
                    }
                    iSumElementLesidur = 0;
                    if (oElement != null)
                    {
                        iSumElementLesidur = int.Parse(oElement.lMakatNesia.ToString().Substring(3, 3));
                    }

                    iSumElement += iSumElementLesidur;

                    oSidurNext = (SidurDM)htEmployeeDetails[I + 1];

                    //ב.	לוקחים את שני הסידורים הראשונים. מחשבים שעת התחלה של השני פחות שעת גמר של הראשון, אם התוצאה גדולה מ- 1 וגם סה"כ ההמתנה שניתנה עד כה קטנה מהערך בפרמטר 148ובמידה 
                    dPaar = (oSidurNext.dFullShatHatchala - oSidur.dFullShatGmar).TotalMinutes;
                    if (dPaar > 1 && iSumElement < _oParameters.iMaxZmanHamtanaEilat)
                    {
                        if (!CheckIdkunRashemet("SHAT_GMAR", oSidur.iMisparSidur, oSidur.dFullShatHatchala))
                        {
                            //במידה ובסידור הראשון מביניהם לא קיים אלמנט - מוסיפים אלמנט המתנה 724xxxxx.
                            dZmanElement = Math.Min(iSumElementLesidur + dPaar, _oParameters.iMaxZmanHamtanaEilat);

                            sZmanElement = dZmanElement.ToString().PadLeft(3, (char)48);

                            //bHaveIdkun = false;

                            if (iSumElementLesidur == 0)
                            {
                                //if (!CheckBitulRashemet(oSidur.iMisparSidur, oSidur.dFullShatHatchala, 0, oSidur.dFullShatGmar))
                                //{
                                if (!IsDuplicateShatYeziaHamtana(oSidur))
                                {
                                    OBJ_PEILUT_OVDIM oObjPeilutOvdimIns = new OBJ_PEILUT_OVDIM();
                                    InsertToObjPeilutOvdimForInsert(ref oSidur, ref oObjPeilutOvdimIns);
                                    oObjPeilutOvdimIns.MISPAR_SIDUR = oSidur.iMisparSidur;
                                    oObjPeilutOvdimIns.TAARICH = _dCardDate;
                                    oObjPeilutOvdimIns.SHAT_YETZIA = oSidur.dFullShatGmar;
                                    oObjPeilutOvdimIns.SHAT_HATCHALA_SIDUR = oSidur.dFullShatHatchala;
                                    oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse("735" + sZmanElement + "00");
                                    oObjPeilutOvdimIns.BITUL_O_HOSAFA = 4;
                                    oObjPeilutOvdimIns.MISPAR_KNISA = 0;
                                    oCollPeilutOvdimIns.Add(oObjPeilutOvdimIns);

                                    PeilutDM oPeilutNew = CreatePeilut(_iMisparIshi, _dCardDate, oObjPeilutOvdimIns, dtTmpMeafyeneyElements);
                                    oPeilutNew.iBitulOHosafa = 4;
                                    oSidur.htPeilut.Add(25 + oSidur.htPeilut.Count + 1, oPeilutNew);
                                }
                                //}
                                //else { bHaveIdkun = true; }
                            }
                            else
                            {
                                //if (!CheckIdkunRashemet("MAKAT_NESIA", oSidur.iMisparSidur, oSidur.dFullShatHatchala, oElement.iMisparKnisa, oElement.dFullShatYetzia))
                                //{
                                    oObjPeilutOvdimUpd = GetUpdPeilutObject(I, oElement, out SourceObject, oObjSidurimOvdimUpd);
                                    oElement.lMakatNesia = long.Parse(oElement.lMakatNesia.ToString().Replace(oElement.lMakatNesia.ToString().Substring(3, 3), sZmanElement));
                                    oObjPeilutOvdimUpd.MAKAT_NESIA = oElement.lMakatNesia;
                                    oObjPeilutOvdimUpd.UPDATE_OBJECT = 1;
                                    oElement = CreatePeilut(_iMisparIshi, _dCardDate, oElement, oObjPeilutOvdimUpd.MAKAT_NESIA, dtTmpMeafyeneyElements);
                                    oSidur.htPeilut[j] = oElement;
                                //}
                                //else { bHaveIdkun = true; }
                            }

                            //if (!bHaveIdkun)
                            //{
                                oSidur.dFullShatGmar = oSidur.dFullShatGmar.AddMinutes(dPaar); //dZmanElement);
                                oSidur.sShatGmar = oSidur.dFullShatGmar.ToString("HH:mm");

                           
                                oObjSidurimOvdimUpd.SHAT_GMAR = oSidur.dFullShatGmar;
                                oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                                htEmployeeDetails[I] = oSidur;
                            //}
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 25, oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "FixedShatHamtana25: " + ex.Message, null);
                _bSuccsess = false;
            }
        }

        private bool IsDuplicateShatYeziaHamtana(SidurDM oSidur)
        {
            PeilutDM oPeilut;
            DateTime dShatYezia = new DateTime();
            try
            {//בדיקה אם קיימת פעילות בשעת יציאה של פעילות ההמתנה שרוצים להוסיף
                dShatYezia = oSidur.dFullShatGmar;
                for (int i = 0; i <= oSidur.htPeilut.Values.Count - 1; i++)
                {
                    oPeilut = (PeilutDM)oSidur.htPeilut[i];
                    if (oPeilut.dFullShatYetzia == dShatYezia)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
       
        private void FixedItyatzvutNahag23(int iIndexSidur, ref SidurDM oSidur,ref SidurDM oSidurNidrashHityazvut, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, ref bool bFirstHayavHityazvut, ref bool bSecondHayavHityazvut)
        {
            bool bNidrashHityazvut=false;
            bool bHayavHityazvut=false;
            try
            {
                //. איפוס שדות לפני השינוי
                if (!oObjSidurimOvdimUpd.PTOR_MEHITIATZVUTIsNull)
                {
                    oObjSidurimOvdimUpd.PTOR_MEHITIATZVUT = 0;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                }

                if (!oObjSidurimOvdimUpd.NIDRESHET_HITIATZVUTIsNull)
                {
                    oObjSidurimOvdimUpd.NIDRESHET_HITIATZVUT = 0;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                }

                if (!oObjSidurimOvdimUpd.SHAT_HITIATZVUTIsNull)
                {
                    oObjSidurimOvdimUpd.SHAT_HITIATZVUT = DateTime.MinValue;
                    oObjSidurimOvdimUpd.SHAT_HITIATZVUTIsNull = true;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                }

                if (!oObjSidurimOvdimUpd.HACHTAMA_BEATAR_LO_TAKINIsNull)
                {
                    oObjSidurimOvdimUpd.HACHTAMA_BEATAR_LO_TAKIN = "";
                    oSidur.iHachtamaBeatarLoTakin = 0;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                }
                
                //התייצבות ראשונה
                if (!bFirstHayavHityazvut)
                { // מחפשים סידור שמצריך התייצבות )

                    bNidrashHityazvut = CheckHovatHityazvut(oSidur, ref oObjSidurimOvdimUpd,1, ref bHayavHityazvut,false);
                    if (bHayavHityazvut) bFirstHayavHityazvut = true;

                    if (bNidrashHityazvut)
                    {
                         oObjSidurimOvdimUpd.NIDRESHET_HITIATZVUT = 1;
                        oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;

                        CheckHityazvut(oSidur, ref oObjSidurimOvdimUpd);
                    }
                }
                else if (iIndexSidur > 0 && !bSecondHayavHityazvut)
                {//התייצבות שניה
                
                    // עדכון שדה התייצבות שניה - עוברים לפי השלבים הבאים:
                    //א. מחפשים סידור שמצריך התייצבות שניה  (סידור מפה) - עוברים על הסידורים, במידה ונתקלים בפער של יותר מהערך בפרמטר 91 (פער בין סידורי נהגות המחייב התייצבות חוזרת) 
                    //בין שעת הגמר של סידור (מפה או מיוחד) לבין שעת ההתחלה של סידור מפה הבא אחריו, פער זמנים כזה מחייב התייצבות נוספת בסידור שאחרי הפער.
                    SidurDM oSidurPrev = (SidurDM)htEmployeeDetails[iIndexSidur - 1];
                    if ((oSidur.dFullShatHatchala - oSidurPrev.dFullShatGmar).TotalMinutes > _oParameters.iPaarBeinSidurimMechayevHityazvut)
                    {
                        oSidurNidrashHityazvut = oSidur;
                    }

                    if (oSidurNidrashHityazvut != null)
                    {
                        bNidrashHityazvut = CheckHovatHityazvut(oSidur, ref oObjSidurimOvdimUpd, 2, ref bHayavHityazvut,false);
                        if (bNidrashHityazvut)
                        {
                            bSecondHayavHityazvut = true;

                            oObjSidurimOvdimUpd.NIDRESHET_HITIATZVUT = 2;
                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                            CheckHityazvut(oSidur, ref oObjSidurimOvdimUpd);
                            oSidurNidrashHityazvut = null;
                        }
                        else if (bHayavHityazvut) { bSecondHayavHityazvut = true; }
                    }
                }

                oSidur.iPtorMehitiatzvut = oObjSidurimOvdimUpd.PTOR_MEHITIATZVUT;
               oSidur.iNidreshetHitiatzvut = oObjSidurimOvdimUpd.NIDRESHET_HITIATZVUT;
                oSidur.dShatHitiatzvut = oObjSidurimOvdimUpd.SHAT_HITIATZVUT;
               
                if (!string.IsNullOrEmpty(oObjSidurimOvdimUpd.HACHTAMA_BEATAR_LO_TAKIN))
                {
                oSidur.iHachtamaBeatarLoTakin = int.Parse(oObjSidurimOvdimUpd.HACHTAMA_BEATAR_LO_TAKIN);
             }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 23, oSidur.iMisparIshi,oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "FixedItyatzvutNahag23: " + ex.Message, null);
                _bSuccsess = false;
            }
        }

        private bool CheckHovatHityazvut(SidurDM oSidur,ref  OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd,int iHityazvut,ref bool  bHayavHityazvut,bool bHaveIdkunRashamet)
        {
            bool bHaveMatala;
            bool  bNidrashHityazvut=false;

           try
           {
               bHayavHityazvut = false;
          // מחפשים סידור שמצריך התייצבות (סידור מפה)
          if (oSidur.bSidurMyuhad)
                {
                    bHaveMatala = false;
                    foreach (PeilutDM oPeilut in oSidur.htPeilut.Values)
                    {
                        if (oPeilut.lMisparMatala > 0)
                        {
                            bHaveMatala = true;
                            break;
                        }
                    }
                    //סידור מיוחד שמקורו במטלה. אם לסידור זה אין מאפיין 83 (חובה להחתים התייצבות):
                    //יש לעדכן 1 בשדה פטור מהתייצבות 
                    if (bHaveMatala)
                    {
                        bHayavHityazvut = true;
                        if (oSidur.sHovatHityatzvut == "" && !bHaveIdkunRashamet)
                        {
                            oObjSidurimOvdimUpd.PTOR_MEHITIATZVUT = 1;
                            oObjSidurimOvdimUpd.NIDRESHET_HITIATZVUT = iHityazvut;
                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                        }
                        else { bNidrashHityazvut = true; }
                    }
                }
                else //if (oSidur.bSidurRagilExists)
                {

                    //- עוברים על הסידורים עד שנתקלים בסידור מפה הראשון ביום. 
                    DataRow[] drSugSidur;

                    //אם לסידור זה יש מאפיין 76 (לא נדרשת החתמת התייצבות):
                    //יש לעדכן 1 בשדה פטור מהתייצבות TB_Sidurim_Ovedim.Ptor_Mehitiatzvut=1    
                    //וסיימנו את הבדיקה.  
                    bNidrashHityazvut = true;
                    bHayavHityazvut = true;
                    drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, _dCardDate, _dtSugSidur);
                    if (drSugSidur.Length > 0)
                    {
                        if (drSugSidur[0]["lo_nidreshet_hityazvut"].ToString() != "" && !bHaveIdkunRashamet)
                        {
                            oObjSidurimOvdimUpd.PTOR_MEHITIATZVUT = 1;
                            oObjSidurimOvdimUpd.NIDRESHET_HITIATZVUT = iHityazvut;
                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                            bNidrashHityazvut = false;
                        }
                    }


                }

           return bNidrashHityazvut;
        }
         catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CheckHityazvut(SidurDM oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            DateTime dShatHatchalaSidur, dShatGmarSidur;
            bool bCheckSidurNosaf, bHaveSidur;
            dShatHatchalaSidur = oSidur.dFullShatHatchala;
            dShatGmarSidur = oSidur.dFullShatGmar;
          int  iCountHachtamaLoTakin = 0;
            //לחפש סידור התייצבות (99200):
            try
            {
                bHaveSidur = false;
                bCheckSidurNosaf = true;
                if (_htSpecialEmployeeDetails.Count > 0)
                {
                              
                    foreach (SidurDM oSidurHityatvut in _htSpecialEmployeeDetails.Values)
                    {
                        if (oSidurHityatvut.iMisparSidur == 99200 && bCheckSidurNosaf)
                        {
                            if ((dShatHatchalaSidur >= oSidurHityatvut.dFullShatHatchala && (dShatHatchalaSidur - oSidurHityatvut.dFullShatHatchala).TotalMinutes <= 60) ||
                                                                 (oSidurHityatvut.dFullShatHatchala >= dShatHatchalaSidur && (oSidurHityatvut.dFullShatHatchala - dShatHatchalaSidur).TotalMinutes <= _oParameters.iBdikatIchurLesidurHityatzvut))
                            {
                                bHaveSidur = true;
                                bCheckSidurNosaf = false;
                                //if (oSidurHityatvut.iKodSibaLedivuchYadaniIn > 0)
                                //{
                                //    oObjSidurimOvdimUpd.SHAT_HITIATZVUT = oSidurHityatvut.dFullShatHatchala;
                                //    break;
                                //}
                                //else
                                //{
                                    int iPeletTnua = CheckPtorHityatzvutTnua(oSidur, oSidurHityatvut.sMikumShaonKnisa);
                                    if (iPeletTnua == 0)
                                    {
                                        oObjSidurimOvdimUpd.PTOR_MEHITIATZVUT = 1;
                                        break;
                                    }
                                    if (iPeletTnua == 1)
                                    {
                                        oObjSidurimOvdimUpd.SHAT_HITIATZVUT = oSidurHityatvut.dFullShatHatchala;
                                        break;
                                    }
                                    if (iPeletTnua == 2)
                                    {
                                        oObjSidurimOvdimUpd.SHAT_HITIATZVUT = oSidurHityatvut.dFullShatHatchala;
                                        oObjSidurimOvdimUpd.HACHTAMA_BEATAR_LO_TAKIN = "1";
                                       if(iCountHachtamaLoTakin==0)  bCheckSidurNosaf = true;
                                        iCountHachtamaLoTakin += 1;
                                    }

                                   
                    
                                //}

                            }
                        }
                    }
                }
                if (!bHaveSidur)
                {
                    if (CheckPtorHityatzvutTnua(oSidur, "") == 0)
                    {
                        oObjSidurimOvdimUpd.PTOR_MEHITIATZVUT = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int CheckPtorHityatzvutTnua(SidurDM oSidur, string sMikumShaonKnisa)
        {
            int iKodHityazvut = -1;
            int iNekudatTziyunPeilut;
            PeilutDM oPeilutRishona=null;
            try
            {
                if (oSidur.htPeilut.Count > 0)
                {
                    for (int i = 0; i < oSidur.htPeilut.Count ; i++)
                    {
                        oPeilutRishona = (PeilutDM)oSidur.htPeilut[i];
                        if (oPeilutRishona.iMakatType == enMakatType.mElement.GetHashCode() && oPeilutRishona.lMakatNesia.ToString().PadLeft(8).Substring(0, 3)!="700")
                        {
                            DataRow[] drMeafyeneyElements = dtTmpMeafyeneyElements.Select("kod_element=" + int.Parse(oPeilutRishona.lMakatNesia.ToString().PadLeft(8).Substring(1, 2)));
                            if (drMeafyeneyElements.Length > 0)
                            {
                                //עבור אלמנט עם מאפיין 36 (בדיקת התייצבות לפי הפעילות הבאה) יש לגשת לבדיקה עם הפעילות הבאה אחרי האלמנט).        
                                if (drMeafyeneyElements[0]["bdikat_nz_hityazvut_next"].ToString() == "")
                                {
                                    break;
                                }
                            }
                            else break;
                        }
                        else { break; }
                    }

                    if (oPeilutRishona != null && !string.IsNullOrEmpty(oPeilutRishona.sSnifTnua))
                    {
                        var kavimDal = ServiceLocator.Current.GetInstance<IKavimDAL>();
                        if (oPeilutRishona.iMakatType == enMakatType.mKavShirut.GetHashCode() || oPeilutRishona.iMakatType == enMakatType.mEmpty.GetHashCode() || oPeilutRishona.iMakatType == enMakatType.mNamak.GetHashCode())
                        {
                            iNekudatTziyunPeilut = oPeilutRishona.iXyMokedTchila;

                            if (sMikumShaonKnisa.Length >= 5)
                            {

                                iKodHityazvut = kavimDal.CheckHityazvutNehag(iNekudatTziyunPeilut, _oParameters.iRadyusMerchakMeshaonLehityazvut, int.Parse(oPeilutRishona.sSnifTnua), int.Parse(sMikumShaonKnisa.Substring(0, 3)), int.Parse(sMikumShaonKnisa.Substring(3, 2)));
                            }
                            else { iKodHityazvut = kavimDal.CheckHityazvutNehag(iNekudatTziyunPeilut, _oParameters.iRadyusMerchakMeshaonLehityazvut, int.Parse(oPeilutRishona.sSnifTnua), null, null); }
                        }
                        else if (oPeilutRishona.iMakatType == enMakatType.mElement.GetHashCode())
                        {
                            if (sMikumShaonKnisa.Length >= 5)
                            {
                                iKodHityazvut = kavimDal.CheckHityazvutNehag(null, _oParameters.iRadyusMerchakMeshaonLehityazvut, int.Parse(oPeilutRishona.sSnifTnua), int.Parse(sMikumShaonKnisa.Substring(0, 3)), int.Parse(sMikumShaonKnisa.Substring(3, 2)));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
           return iKodHityazvut;
        }

        private void FixedShatHatchalaLefiShatHachtamatItyatzvut12(ref SidurDM oSidur, int iSidurIndex, DateTime dShatHatchalaNew, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            OBJ_PEILUT_OVDIM oObjPeilutUpd;
            PeilutDM oPeilut;
            SourceObj SourceObject;
            try
            {
                if (dShatHatchalaNew!=oSidur.dFullShatHatchala)
                {
                   clNewSidurim oNewSidurim = FindSidurOnHtNewSidurim(oSidur.iMisparSidur, oSidur.dFullShatHatchala);

                    oNewSidurim.SidurIndex = iSidurIndex;
                    oNewSidurim.SidurNew = oSidur.iMisparSidur;
                   oNewSidurim.ShatHatchalaNew = dShatHatchalaNew;
                  
                   UpdateObjectUpdSidurim(oNewSidurim);
                    //עדכון שעת התחלה סידור של כל הפעילויות לסידור
                    for (int j = 0; j < oSidur.htPeilut.Count; j++)
                    {
                        oPeilut = (PeilutDM)oSidur.htPeilut[j];
                        if (!CheckPeilutObjectDelete(iSidurIndex, j))
                        {
                            oObjPeilutUpd = GetUpdPeilutObject(iSidurIndex, oPeilut, out SourceObject, oObjSidurimOvdimUpd);
                            if (SourceObject == SourceObj.Insert)
                            {
                                oObjPeilutUpd.SHAT_HATCHALA_SIDUR = dShatHatchalaNew;
                            }
                            else
                            {
                                oObjPeilutUpd.NEW_SHAT_HATCHALA_SIDUR = dShatHatchalaNew;
                                oObjPeilutUpd.UPDATE_OBJECT = 1;
                            }
                           
                        }
                    }
                    //UpdatePeiluyotMevutalotYadani(iSidurIndex,oNewSidurim, oObjSidurimOvdimUpd);
                    UpdateIdkunRashemet(oSidur.iMisparSidur, oSidur.dFullShatHatchala, dShatHatchalaNew);
                    UpdateApprovalErrors(oSidur.iMisparSidur, oSidur.dFullShatHatchala, dShatHatchalaNew);
                                          
                     oSidur.dFullShatHatchala = dShatHatchalaNew;
                    oSidur.sShatHatchala = dShatHatchalaNew.ToString("HH:mm");
                    oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA = dShatHatchalaNew;
                 
                 }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null,12, _iMisparIshi,  _dCardDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala,null,null, "FixedShatHatchalaLefiShatHachtamatItyatzvut12: " + ex.Message,null);
                _bSuccsess = false;
            }
        }

        private void FixedShatHatchalaLefiShatHachtamatItyatzvut12(ref int j, ref PeilutDM oPeilut, ref SidurDM oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, ref OBJ_PEILUT_OVDIM oObjPeilutOvdimUpd, ref bool bUpdateShatHatchala, ref DateTime dShatHatchalaNew,SourceObj SourceObject)
        {
             string sTempTime, sNewTempTime;
            PeilutDM oNextPeilut = null;
            int iTempTime;
            sTempTime = "";
            Double dZmanLekizuz = 0;
            int i,iCountInsPeiluyot;
            try
            {
                if (oSidur.htPeilut.Count > j+1)
                {
                    oNextPeilut = (PeilutDM)oSidur.htPeilut[j+1];
                }

                if (oObjSidurimOvdimUpd.PTOR_MEHITIATZVUT == 1) return;

                if ((oObjSidurimOvdimUpd.NIDRESHET_HITIATZVUT == 1 || oObjSidurimOvdimUpd.NIDRESHET_HITIATZVUT == 2)
                    && (oObjSidurimOvdimUpd.SHAT_HITIATZVUT > dShatHatchalaNew))
                {
                    dZmanLekizuz = (oObjSidurimOvdimUpd.SHAT_HITIATZVUT - dShatHatchalaNew).TotalMinutes;
                    //יש לקזז/לבטל פעילויות ולתקן את שעת היציאה שלהן עד אשר שעת התחלת הסידור שווה לשעת ההתייצבות:
                    if (oPeilut.dFullShatYetzia < oObjSidurimOvdimUpd.SHAT_HITIATZVUT)
                    {
                        //1. מותר לבטל/לקזז זמנים לנסיעות ריקות (לפי רוטינת זיהוי מקט). 
                        //2. מותר לבטל/לקזז זמנים לאלמנטים שיש להם מאפיין 8 (ביטול בגלל איחור לסידור). 
                        if (oPeilut.iMakatType == enMakatType.mEmpty.GetHashCode() || ((oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && oPeilut.bBitulBiglalIchurLasidurExists)))
                        {
                            bUpdateShatHatchala = true;
                            if (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && oPeilut.bBitulBiglalIchurLasidurExists)
                            {
                                sTempTime = oObjPeilutOvdimUpd.MAKAT_NESIA.ToString().Substring(3, 3);
                                iTempTime = int.Parse(sTempTime);
                            }
                            else
                            {
                                iTempTime = oPeilut.iMazanTashlum;
                            }

                            if (iTempTime <= dZmanLekizuz)
                            {
                                dShatHatchalaNew = dShatHatchalaNew.AddMinutes(iTempTime);

                                oObjPeilutOvdimDel = new OBJ_PEILUT_OVDIM();
                                InsertToObjPeilutOvdimForDelete(ref oPeilut, ref oSidur, ref oObjPeilutOvdimDel);
                                oCollPeilutOvdimDel.Add(oObjPeilutOvdimDel);
                                oSidur.htPeilut.RemoveAt(j);
                                j = j - 1;
                               
                                for (i = 0; i <= oCollPeilutOvdimUpd.Count - 1; i++)
                                {
                                    if ((oCollPeilutOvdimUpd.Value[i].NEW_MISPAR_SIDUR == oSidur.iMisparSidur) && (oCollPeilutOvdimUpd.Value[i].NEW_SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
                                        && (oCollPeilutOvdimUpd.Value[i].NEW_SHAT_YETZIA == oPeilut.dFullShatYetzia) && (oCollPeilutOvdimUpd.Value[i].MISPAR_KNISA == oPeilut.iMisparKnisa))
                                    {
                                        //oCollPeilutOvdimUpd.Value[i].UPDATE_OBJECT = 0;
                                        oCollPeilutOvdimUpd.RemoveAt(i);
                                    }
                                }
                                i = 0;
                                iCountInsPeiluyot = oCollPeilutOvdimIns.Count;
                                if (iCountInsPeiluyot > 0)
                                {
                                    do
                                    {
                                        if ((oCollPeilutOvdimIns.Value[i].MISPAR_SIDUR == oSidur.iMisparSidur) && (oCollPeilutOvdimIns.Value[i].SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
                                            && (oCollPeilutOvdimIns.Value[i].SHAT_YETZIA == oPeilut.dFullShatYetzia) && (oCollPeilutOvdimIns.Value[i].MISPAR_KNISA == oPeilut.iMisparKnisa))
                                        {
                                            oCollPeilutOvdimIns.RemoveAt(i);
                                            i -= 1;
                                        }
                                        iCountInsPeiluyot = oCollPeilutOvdimIns.Count;
                                        i += 1;
                                    } while (i < iCountInsPeiluyot);
                                }
                            }
                            else
                            {
                                //במקרה של קיזוז זמנים,  יש לעדכן את הזמן המעודכן בפוזיציות 4-6 של האלמנט.                
                                if (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && oPeilut.bBitulBiglalIchurLasidurExists)
                                {
                                    sNewTempTime = (iTempTime - dZmanLekizuz).ToString().PadLeft(3, (char)48);
                                    oObjPeilutOvdimUpd.MAKAT_NESIA = long.Parse(oObjPeilutOvdimUpd.MAKAT_NESIA.ToString().Replace(sTempTime, sNewTempTime));
                                    oPeilut = CreatePeilut(_iMisparIshi, _dCardDate, oPeilut, oObjPeilutOvdimUpd.MAKAT_NESIA, dtTmpMeafyeneyElements);
                                    oSidur.htPeilut[j] = oPeilut;
                                }
                                dShatHatchalaNew = dShatHatchalaNew.AddMinutes(dZmanLekizuz);
                                if (oNextPeilut != null)
                                {
                                    if (oNextPeilut.dFullShatYetzia == oPeilut.dFullShatYetzia.AddMinutes(dZmanLekizuz))
                                    {
                                        oObjPeilutOvdimDel = new OBJ_PEILUT_OVDIM();
                                        InsertToObjPeilutOvdimForDelete(ref oPeilut, ref oSidur, ref oObjPeilutOvdimDel);
                                        oCollPeilutOvdimDel.Add(oObjPeilutOvdimDel);
                                        oSidur.htPeilut.RemoveAt(j);
                                        j = j - 1;
                                        for (i = 0; i <= oCollPeilutOvdimUpd.Count - 1; i++)
                                        {
                                            if ((oCollPeilutOvdimUpd.Value[i].NEW_MISPAR_SIDUR == oSidur.iMisparSidur) && (oCollPeilutOvdimUpd.Value[i].NEW_SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
                                                && (oCollPeilutOvdimUpd.Value[i].NEW_SHAT_YETZIA == oPeilut.dFullShatYetzia) && (oCollPeilutOvdimUpd.Value[i].MISPAR_KNISA == oPeilut.iMisparKnisa))
                                            {
                                                //oCollPeilutOvdimUpd.Value[i].UPDATE_OBJECT = 0;
                                                oCollPeilutOvdimUpd.RemoveAt(i);
                                            }
                                        }
                                        i = 0;
                                        iCountInsPeiluyot = oCollPeilutOvdimIns.Count;
                                        if (iCountInsPeiluyot > 0)
                                        {
                                            do
                                            {
                                                if ((oCollPeilutOvdimIns.Value[i].MISPAR_SIDUR == oSidur.iMisparSidur) && (oCollPeilutOvdimIns.Value[i].SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
                                                    && (oCollPeilutOvdimIns.Value[i].SHAT_YETZIA == oPeilut.dFullShatYetzia) && (oCollPeilutOvdimIns.Value[i].MISPAR_KNISA == oPeilut.iMisparKnisa))
                                                {
                                                    oCollPeilutOvdimIns.RemoveAt(i);
                                                    i -= 1;
                                                }
                                                iCountInsPeiluyot = oCollPeilutOvdimIns.Count;
                                                i += 1;
                                            } while (i < iCountInsPeiluyot);
                                        }
                                    }
                                    else
                                    {
                                        if (SourceObject == SourceObj.Insert)
                                        {
                                            oObjPeilutOvdimUpd.SHAT_YETZIA = oPeilut.dFullShatYetzia.AddMinutes(dZmanLekizuz);
                                            oPeilut.dFullShatYetzia = oObjPeilutOvdimUpd.SHAT_YETZIA;
                                        
                                        }
                                        else
                                        {
                                            oObjPeilutOvdimUpd.NEW_SHAT_YETZIA = oPeilut.dFullShatYetzia.AddMinutes(dZmanLekizuz);
                                            oObjPeilutOvdimUpd.UPDATE_OBJECT = 1;
                                            UpdateIdkunRashemet(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutOvdimUpd.NEW_SHAT_YETZIA);
                                            UpdateApprovalErrors(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutOvdimUpd.NEW_SHAT_YETZIA);        
                                            
                                            oPeilut.dFullShatYetzia = oObjPeilutOvdimUpd.NEW_SHAT_YETZIA;
                                        
                                        }
                                        
                                        oPeilut.sShatYetzia = oPeilut.dFullShatYetzia.ToString("HH:mm");
                                    }
                                }
                                else
                                {
                                    if (SourceObject == SourceObj.Insert)
                                    {
                                        oObjPeilutOvdimUpd.SHAT_YETZIA = oPeilut.dFullShatYetzia.AddMinutes(dZmanLekizuz);
                                        oPeilut.dFullShatYetzia = oObjPeilutOvdimUpd.SHAT_YETZIA;
                                    }
                                    else
                                    {
                                        oObjPeilutOvdimUpd.NEW_SHAT_YETZIA = oPeilut.dFullShatYetzia.AddMinutes(dZmanLekizuz);
                                        oObjPeilutOvdimUpd.UPDATE_OBJECT = 1;
                                        UpdateIdkunRashemet(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutOvdimUpd.NEW_SHAT_YETZIA);
                                        UpdateApprovalErrors(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutOvdimUpd.NEW_SHAT_YETZIA);
                                        
                                        oPeilut.dFullShatYetzia = oObjPeilutOvdimUpd.NEW_SHAT_YETZIA;
                                    }
                                    
                                    oPeilut.sShatYetzia = oPeilut.dFullShatYetzia.ToString("HH:mm");
                                }
                            }
                        }
                    }
                    else if (oPeilut.dFullShatYetzia == oObjSidurimOvdimUpd.SHAT_HITIATZVUT)
                        dShatHatchalaNew = dShatHatchalaNew.AddMinutes(dZmanLekizuz);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 12, _iMisparIshi, _dCardDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.dFullShatYetzia, oPeilut.iMisparKnisa, "FixedShatHatchalaLefiShatHachtamatItyatzvut12: " + ex.Message, null);
                _bSuccsess = false;
            }
        }

        private void FixedEggedTaavura22(ref SidurDM oSidur,ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            try
            {
                if (oOvedYomAvodaDetails.iKodHevra == enEmployeeType.enEggedTaavora.GetHashCode() &&
                    oSidur.bSidurNotValidKodExists)
                {
                    oObjSidurimOvdimUpd.LO_LETASHLUM = 1;
                    oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM = 12;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;

                    oSidur.iLoLetashlum = 1;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", 22, "FixedEggedTaavura22: " + ex.Message);
                _bSuccsess = false;
            }
        }

        private void FixedShatHatchalaForSidurVisa21(ref SidurDM oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            try
            {
                if (oSidur.bSidurVisaKodExists 
                    && oSidur.sSidurVisaKod == "2"
                    && oSidur.sVisa == "3" 
                    && (oSidur.dFullShatHatchala.Hour > 7 
                        || (oSidur.dFullShatHatchala.Hour == 7 
                            && oSidur.dFullShatHatchala.Minute > 0)))
                {
                    oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = _oParameters.dShatHatchalaVisaPnimAcharon;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;

                    oSidur.dFullShatHatchalaLetashlum = oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM;
                    oSidur.sShatHatchalaLetashlum = oSidur.dFullShatHatchalaLetashlum.ToString("HH:mm");
                    if (oSidur.dFullShatHatchalaLetashlum.Year < DateHelper.cYearNull)
                        oSidur.sShatHatchalaLetashlum = "";
                   
                }
                else if (oSidur.bSidurVisaKodExists
                         && oSidur.sSidurVisaKod == "1"
                         && oSidur.sVisa == "3" 
                         && (oSidur.dFullShatHatchala.Hour > 4 
                            || (oSidur.dFullShatHatchala.Hour == 4 
                                && oSidur.dFullShatHatchala.Minute > 0)))
                {
                    oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = _oParameters.dShatHatchalaVisaTzahalAcharon;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;

                    oSidur.dFullShatHatchalaLetashlum = oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM;
                    oSidur.sShatHatchalaLetashlum = oSidur.dFullShatHatchalaLetashlum.ToString("HH:mm");
                    if (oSidur.dFullShatHatchalaLetashlum.Year < DateHelper.cYearNull)
                        oSidur.sShatHatchalaLetashlum = "";
                }
                if (oSidur.bSidurVisaKodExists)
                {
                    oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = oObjSidurimOvdimUpd.SHAT_GMAR;
                    oSidur.dFullShatGmarLetashlum = oObjSidurimOvdimUpd.SHAT_GMAR;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                }
                
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 21, oSidur.iMisparIshi,oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "FixedShatHatchalaForSidurVisa21: " + ex.Message, null);
                _bSuccsess = false;
            }
        }

        private void FixedShatGmar10(ref SidurDM oSidur, int iIndexSidur, ref  bool bUsedMazanTichnun, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            DateTime dShatGmar;
            PeilutDM oLastPeilutMashmautit=null;
            int i;
            PeilutDM oPeilut;
            bool bUsedMazanTichnunInSidur = false;
           int iMeshechPeilut=0;
            try
            {
                dShatGmar = oObjSidurimOvdimUpd.SHAT_GMAR;
                if (oSidur.iMisparSidur>1000)
                {
                //אם הסידור הוא סידור ויזה (לפי מאפיין 45 בטבלת סידורים מיוחדים), ניתן להפסיק את הבדיקה.
                    if (string.IsNullOrEmpty(oSidur.sSidurVisaKod))
                    {
                        //אם הסידור לא מכיל פעילויות, ניתן להפסיק את הבדיקה.
                        if (oSidur.htPeilut.Count > 0)
                        {
                            //פעילות משמעותית: שירות, נמ"ק, אלמנט עם מאפיין 37 (אלמנט משמעותי לצורך חישוב שעת גמר).
                            oLastPeilutMashmautit = oSidur.htPeilut.Values.Cast<PeilutDM>().ToList().LastOrDefault(Peilut => (Peilut.iMakatType == enMakatType.mKavShirut.GetHashCode() || Peilut.iMakatType == enMakatType.mNamak.GetHashCode() || (Peilut.iMakatType == enMakatType.mElement.GetHashCode() && (Peilut.iElementLeShatGmar > 0 || Peilut.iElementLeShatGmar == -1))));

                            //. קיימת פעילות משמעותית אחרונה (או יחידה):
                            if (oLastPeilutMashmautit!=null)
                            {
                                for (i = oSidur.htPeilut.Values.Count - 1; i >= 0; i--)
                                {
                                     oPeilut = (PeilutDM)oSidur.htPeilut[i];
                                    if ((oPeilut.iMakatType == enMakatType.mKavShirut.GetHashCode() && oPeilut.iMisparKnisa==0) || oPeilut.iMakatType == enMakatType.mNamak.GetHashCode() || (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && (oPeilut.iElementLeShatGmar > 0 || oPeilut.iElementLeShatGmar == -1)))
                                    {
                                        dShatGmar = oPeilut.dFullShatYetzia.AddMinutes(GetMeshechPeilut(iIndexSidur, oPeilut, oSidur, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur) + iMeshechPeilut);
                                        break;           
                                    }
                                    if ((oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && oPeilut.iErechElement == 1 && string.IsNullOrEmpty(oPeilut.sLoNitzbarLishatGmar)) || (oPeilut.iMakatType == enMakatType.mKavShirut.GetHashCode() && oPeilut.iMisparKnisa > 0) || oPeilut.iMakatType == enMakatType.mEmpty.GetHashCode()) 
                                    {
                                        iMeshechPeilut += GetMeshechPeilut(iIndexSidur, oPeilut, oSidur, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur);
                                    }
                                }
                              
                                   
                            }
                            else
                            {//לא קיימת פעילות משמעותית:

                                for (i = oSidur.htPeilut.Values.Count-1; i >=0; i--)
                                {
                                    //מחפשים פעילות מסוג ריקה או אלמנט מסוג דקות (ערך 1 (דקות) במאפיין 4  (ערך האלמנט)).
                                    oPeilut = (PeilutDM)oSidur.htPeilut[i];
                                   if ((oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && oPeilut.iErechElement == 1) || oPeilut.iMakatType == enMakatType.mEmpty.GetHashCode())
                                    {//מחשבים שעת גמר באופן הבא: שעת גמר = שעת יציאה של פעילות אחרונה שאינה משמעותית + משך הפעילות . 
                                        dShatGmar = oPeilut.dFullShatYetzia.AddMinutes(GetMeshechPeilut(iIndexSidur, oPeilut, oSidur, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur));
                                        break;
                                   }
                                }
                               
                            }
                           
                            if (dShatGmar != oObjSidurimOvdimUpd.SHAT_GMAR)
                            {
                                oObjSidurimOvdimUpd.SHAT_GMAR = dShatGmar;
                                oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;

                                oSidur.dFullShatGmar = oObjSidurimOvdimUpd.SHAT_GMAR;
                                oSidur.sShatGmar = oSidur.dFullShatGmar.ToString("HH:mm");
                            }

                            if (bUsedMazanTichnunInSidur)
                                bUsedMazanTichnun = true;
                        }
                    }
            }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int GetMeshechPeilut(int iIndexSidur, PeilutDM oPeilut, SidurDM oSidur, ref bool bUsedMazanTichnun, ref bool bUsedMazanTichnunInSidur)
        {
            int iMeshech = 0;
            DataRow[] drSugSidur;
             bool bSidurNahagutNext;

            drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, _dCardDate, _dtSugSidur);
            bool bSidurNahagut=IsSidurNahagut(drSugSidur, oSidur);
            if (htEmployeeDetails.Count > iIndexSidur + 1)
            {
                drSugSidur = clDefinitions.GetOneSugSidurMeafyen(((SidurDM)htEmployeeDetails[iIndexSidur + 1]).iSugSidurRagil, _dCardDate, _dtSugSidur);
                bSidurNahagutNext = IsSidurNahagut(drSugSidur, ((SidurDM)htEmployeeDetails[iIndexSidur + 1]));
            }
            else bSidurNahagutNext = false;

            //אם הערך בשדה 0<Dakot_bafoal אז יש לקחת את הערך משדה זה  
            if (oPeilut.iDakotBafoal > 0 || (oPeilut.iMakatType == enMakatType.mKavShirut.GetHashCode() && oPeilut.iMisparKnisa>0))
            {
                iMeshech = oPeilut.iDakotBafoal;
            }
            else
            {
                //משך פעילות מסוג אלמנט  - הערך בפוזיציות 4-6 של המק"ט. 
                if (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                {
                    iMeshech = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));
                }
               else
                { //סידור שהוא יחיד או אחרון ביום
                    //סידור אחרון צריך להיגמר לפי הגדרה לתכנון.

                    if (htEmployeeDetails.Values.Count == 1 || (htEmployeeDetails.Count - 1) == iIndexSidur || (bSidurNahagut && !bSidurNahagutNext))
                    {
                         //אלמנט ריקה  (מאפיין 23): מז"ן תשלום (גמר) - הערך שהוקלד במק"ט. מז"ן תכנון  - מז"ן תשלום * פרמטר 43.
                        if (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && !string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                            iMeshech = int.Parse(Math.Round(oPeilut.iMazanTashlum * oParam.fFactorNesiotRekot).ToString());
                         else
                            iMeshech = oPeilut.iMazanTichnun;
                    }
                    else if (bSidurNahagut && bSidurNahagutNext)
                    {
                        //סידור שאינו יחיד או אחרון ביום צריך להיגמר לפי זמן לגמר או לתכנון, בהתאם למקרה:
                        SidurDM oNextSidur = (SidurDM)htEmployeeDetails[iIndexSidur + 1];

                        //1.	אם יש פער של עד 60 דקות משעת התחלת הסידור הבא - יש לחשב לפי הגדרה גמר (תשלום) 
                        if (oNextSidur.dFullShatHatchala.Subtract(oSidur.dFullShatGmar).TotalMinutes <= 60)
                        {
                            //אלמנט ריקה  (מאפיין 23): מז"ן תשלום (גמר) - הערך שהוקלד במק"ט. מז"ן תכנון  - מז"ן תשלום * פרמטר 43.
                            if (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && !string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                               iMeshech = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));
                            else
                                iMeshech = oPeilut.iMazanTashlum;
                        }
                        else
                        {
                            if (!bUsedMazanTichnun)
                            {
                                //אלמנט ריקה  (מאפיין 23): מז"ן תשלום (גמר) - הערך שהוקלד במק"ט. מז"ן תכנון  - מז"ן תשלום * פרמטר 43.
                                if (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && !string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                                    iMeshech = int.Parse(Math.Round(oPeilut.iMazanTashlum * oParam.fFactorNesiotRekot).ToString());
                                 else
                                    //2.	אם יש פער גדול מ- 60 דקות משעת התחלה של הסידור הבא וזו הפעם הראשונה שסידור שאינו יחיד/אחרון ביום צריך להיגמר לפי הגדרה לתכנון   - יש לחשב לפי זמן לתכנון. 
                                    iMeshech = oPeilut.iMazanTichnun;
                                bUsedMazanTichnunInSidur = true;

                            }
                            else
                            {
                                //אלמנט ריקה  (מאפיין 23): מז"ן תשלום (גמר) - הערך שהוקלד במק"ט. מז"ן תכנון  - מז"ן תשלום * פרמטר 43.
                                if (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && !string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                                    iMeshech = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));
                                else
                                    //3.	אם יש פער גדול מ- 60 דקות מהסידור הבא וזו אינה הפעם הראשונה שסידור שאינו יחיד/אחרון ביום צריך להיגמר לפי זמן לתכנון - יש לחשב לפי הגדרה לגמר (תשלום)
                                    iMeshech = oPeilut.iMazanTashlum;
                            }

                        }

                    }
                }
            }
            return iMeshech;
        }

        private int GetMeshechPeilutHachnatMechona(int iIndexSidur, PeilutDM oPeilut, SidurDM oSidur, ref bool bUsedMazanTichnun, ref bool bUsedMazanTichnunInSidur)
        {
            int iMeshech = 0;
            DataRow[] drSugSidur;
            bool bSidurNahagutPrev;

            drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, _dCardDate, _dtSugSidur);
            bool bSidurNahagut = IsSidurNahagut(drSugSidur, oSidur);
            if (iIndexSidur > 0)
            {
                drSugSidur = clDefinitions.GetOneSugSidurMeafyen(((SidurDM)htEmployeeDetails[iIndexSidur - 1]).iSugSidurRagil, _dCardDate, _dtSugSidur);
                bSidurNahagutPrev = IsSidurNahagut(drSugSidur, ((SidurDM)htEmployeeDetails[iIndexSidur - 1]));
            }
            else bSidurNahagutPrev = false;

            //אם הערך בשדה 0<Dakot_bafoal אז יש לקחת את הערך משדה זה  
            if (oPeilut.iDakotBafoal > 0 || (oPeilut.iMakatType == enMakatType.mKavShirut.GetHashCode() && oPeilut.iMisparKnisa > 0))
            {
                iMeshech = oPeilut.iDakotBafoal;
            }
            else
            {
                //משך פעילות מסוג אלמנט  - הערך בפוזיציות 4-6 של המק"ט. 
                if (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                {
                    iMeshech = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));
                }
                else
                { //סידור שהוא יחיד או ראשון ביום
                    //סידור ראשון צריך להיגמר לפי הגדרה לתכנון.

                    if ((iIndexSidur == 0) || htEmployeeDetails.Values.Count == 1 || (bSidurNahagut && !bSidurNahagutPrev))
                    {
                        //אלמנט ריקה  (מאפיין 23): מז"ן תשלום (גמר) - הערך שהוקלד במק"ט. מז"ן תכנון  - מז"ן תשלום * פרמטר 43.
                        if (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && !string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                            iMeshech = int.Parse(Math.Round(oPeilut.iMazanTashlum * oParam.fFactorNesiotRekot).ToString());
                        else
                            iMeshech = oPeilut.iMazanTichnun;
                    }
                    else if (bSidurNahagut && bSidurNahagutPrev)
                    {
                        //סידור שאינו יחיד או ראשון ביום צריך להיגמר לפי זמן לגמר או לתכנון, בהתאם למקרה:
                        SidurDM oPrevSidur = (SidurDM)htEmployeeDetails[iIndexSidur - 1];

                        //1.	אם יש פער של עד 60 דקות משעת התחלת הסידור הבא - יש לחשב לפי הגדרה גמר (תשלום) 
                        if (oSidur.dFullShatHatchala.Subtract(oPrevSidur.dFullShatGmar).TotalMinutes <= 60)
                        {
                            //אלמנט ריקה  (מאפיין 23): מז"ן תשלום (גמר) - הערך שהוקלד במק"ט. מז"ן תכנון  - מז"ן תשלום * פרמטר 43.
                            if (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && !string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                                iMeshech = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));
                            else
                                iMeshech = oPeilut.iMazanTashlum;
                        }
                        else
                        {
                            if (!bUsedMazanTichnun)
                            {
                                //אלמנט ריקה  (מאפיין 23): מז"ן תשלום (גמר) - הערך שהוקלד במק"ט. מז"ן תכנון  - מז"ן תשלום * פרמטר 43.
                                if (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && !string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                                    iMeshech = int.Parse(Math.Round(oPeilut.iMazanTashlum * oParam.fFactorNesiotRekot).ToString());
                                else
                                    //2.	אם יש פער גדול מ- 60 דקות משעת התחלה של הסידור הבא וזו הפעם הראשונה שסידור שאינו יחיד/ראשון ביום צריך להיגמר לפי הגדרה לתכנון   - יש לחשב לפי זמן לתכנון. 
                                    iMeshech = oPeilut.iMazanTichnun;
                                bUsedMazanTichnunInSidur = true;

                            }
                            else
                            {
                                //אלמנט ריקה  (מאפיין 23): מז"ן תשלום (גמר) - הערך שהוקלד במק"ט. מז"ן תכנון  - מז"ן תשלום * פרמטר 43.
                                if (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && !string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                                    iMeshech = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));
                                else
                                    //3.	אם יש פער גדול מ- 60 דקות מהסידור הבא וזו אינה הפעם הראשונה שסידור שאינו יחיד/ראשון ביום צריך להיגמר לפי זמן לתכנון - יש לחשב לפי הגדרה לגמר (תשלום)
                                    iMeshech = oPeilut.iMazanTashlum;
                            }

                        }

                    }
                }
            }
            return iMeshech;
        }

        private void FixedShaotForSidurWithOneNesiaReeka04(int iCurSidurIndex, ref SidurDM oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            DateTime dShatYetzia = DateTime.MinValue;
            int iMisparKnisa = 0;
            OBJ_PEILUT_OVDIM oObjPeilutUpd;
            clNewSidurim oNewSidurim=null;
            SourceObj SourceObject;
            PeilutDM oPeilut;
            bool bHaveSidurFromMatala = false;
            bool bHavePeilutReka = false;
            bool bHaveElementHachanatMechona = false;
            bool bHaveElementLoMashmauti = false;
            int iMazanTichnun = 0,iDakot=0;
            bool bContinue = true, flag = false;
            SidurDM nextSidur, prevSidur;
            if (oSidur.htPeilut.Count <= 2)
            {
                for (int i = 0; i < oSidur.htPeilut.Count; i++)
                {
                    oPeilut = (PeilutDM)oSidur.htPeilut[i];
                    if (oPeilut.bPeilutNotRekea == false)
                    {
                        dShatYetzia = oPeilut.dFullShatYetzia;
                        iMisparKnisa = oPeilut.iMisparKnisa;
                        iMazanTichnun = oPeilut.iMazanTichnun;
                        bHavePeilutReka = true;
                    }
                    if (oPeilut.lMisparMatala > 0)
                    {
                        bHaveSidurFromMatala = true;
                    }
                    if (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == enElementHachanatMechona.Element701.GetHashCode().ToString() || oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == enElementHachanatMechona.Element712.GetHashCode().ToString() || oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == enElementHachanatMechona.Element711.GetHashCode().ToString())
                    {
                        bHaveElementHachanatMechona = true;
                    }
                    if (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && oPeilut.iElementLeShatGmar == 0)
                    {
                        bHaveElementLoMashmauti = true;
                    }
                }
                    if ((!oSidur.bSidurMyuhad || bHaveSidurFromMatala) && ((bHavePeilutReka && oSidur.htPeilut.Count == 1) || (bHavePeilutReka && (bHaveElementHachanatMechona || bHaveElementLoMashmauti))))
                    {
                    nextSidur = null; prevSidur = null;
                    if(iCurSidurIndex>0)
                        prevSidur = htEmployeeDetails[iCurSidurIndex - 1] as SidurDM;
                    if (iCurSidurIndex < (htEmployeeDetails.Count - 1))
                        nextSidur = htEmployeeDetails[iCurSidurIndex + 1] as SidurDM;
                    if ((prevSidur != null && !prevSidur.bSidurMyuhad && prevSidur.dFullShatGmar == oSidur.dFullShatHatchala) ||
                        (nextSidur != null && !nextSidur.bSidurMyuhad && oSidur.dFullShatGmar == nextSidur.dFullShatHatchala))
                        bContinue = false;
                    if (bContinue)
                    {
                        try
                        {
                            if (nextSidur != null
                                && !nextSidur.bSidurMyuhad
                                && nextSidur.dFullShatHatchala.Subtract(oSidur.dFullShatGmar).TotalMinutes >= 1)
                            {
                                oNewSidurim = FindSidurOnHtNewSidurim(oSidur.iMisparSidur, oSidur.dFullShatHatchala);

                                oNewSidurim.SidurIndex = iCurSidurIndex;
                                oNewSidurim.SidurNew = oSidur.iMisparSidur;
                                oNewSidurim.ShatHatchalaNew = oSidur.dFullShatHatchala.AddMinutes(iMazanTichnun); //.Subtract(new TimeSpan(0, iMazanTichnun, 0));

                                UpdateObjectUpdSidurim(oNewSidurim);
                                oObjSidurimOvdimUpd.SHAT_GMAR = nextSidur.dFullShatHatchala;
                                iDakot = iMazanTichnun;
                                flag = true;
                            }
                            else if (prevSidur != null
                                       && !prevSidur.bSidurMyuhad
                                       && oSidur.dFullShatHatchala.Subtract(prevSidur.dFullShatGmar).TotalMinutes >= 1)
                            {
                                oNewSidurim = FindSidurOnHtNewSidurim(oSidur.iMisparSidur, oSidur.dFullShatHatchala);

                                oNewSidurim.SidurIndex = iCurSidurIndex;
                                oNewSidurim.SidurNew = oSidur.iMisparSidur;
                                oNewSidurim.ShatHatchalaNew = prevSidur.dFullShatGmar;

                                UpdateObjectUpdSidurim(oNewSidurim);
                                oObjSidurimOvdimUpd.SHAT_GMAR = oObjSidurimOvdimUpd.SHAT_GMAR.AddMinutes(-(oObjSidurimOvdimUpd.SHAT_HATCHALA - prevSidur.dFullShatGmar).TotalMinutes);

                                iDakot = -int.Parse((oObjSidurimOvdimUpd.SHAT_HATCHALA - prevSidur.dFullShatGmar).TotalMinutes.ToString());
                                flag = true;
                            }

                            if (flag)
                            {
                                for (int j = 0; j < oSidur.htPeilut.Count; j++)
                                {
                                    oPeilut = (PeilutDM)oSidur.htPeilut[j];

                                    if (!CheckPeilutObjectDelete(iCurSidurIndex, j))
                                    {
                                        oObjPeilutUpd = GetUpdPeilutObject(iCurSidurIndex, oPeilut, out SourceObject, oObjSidurimOvdimUpd);

                                        if (SourceObject == SourceObj.Insert)
                                        {
                                            oObjPeilutUpd.SHAT_HATCHALA_SIDUR = oNewSidurim.ShatHatchalaNew;
                                        }
                                        else
                                        {
                                            oObjPeilutUpd.NEW_SHAT_HATCHALA_SIDUR = oNewSidurim.ShatHatchalaNew;
                                            oObjPeilutUpd.UPDATE_OBJECT = 1;
                                        }

                                        if (SourceObject == SourceObj.Insert)
                                        {
                                            oObjPeilutUpd.SHAT_YETZIA = oObjPeilutUpd.SHAT_YETZIA.AddMinutes(iDakot);  //oObjPeilutUpd.SHAT_YETZIA.AddMinutes((oNewSidurim.ShatHatchalaNew - oObjSidurimOvdimUpd.SHAT_HATCHALA).TotalMinutes);
                                            oPeilut.dFullShatYetzia = oObjPeilutUpd.SHAT_YETZIA;

                                        }
                                        else
                                        {
                                            oObjPeilutUpd.NEW_SHAT_YETZIA = oObjPeilutUpd.SHAT_YETZIA.AddMinutes(iDakot); //(oNewSidurim.ShatHatchalaNew - oObjSidurimOvdimUpd.SHAT_HATCHALA).TotalMinutes);
                                            UpdateIdkunRashemet(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutUpd.NEW_SHAT_YETZIA);
                                            UpdateApprovalErrors(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutUpd.NEW_SHAT_YETZIA);

                                            oPeilut.dFullShatYetzia = oObjPeilutUpd.NEW_SHAT_YETZIA;

                                        }
                                        oPeilut.sShatYetzia = oPeilut.dFullShatYetzia.ToString("HH:mm");
                                        oSidur.htPeilut[j] = oPeilut;
                                    }
                                }
                                //UpdatePeiluyotMevutalotYadani(iCurSidurIndex,oNewSidurim, oObjSidurimOvdimUpd);
                                UpdateIdkunRashemet(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oNewSidurim.ShatHatchalaNew);
                                UpdateApprovalErrors(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oNewSidurim.ShatHatchalaNew);

                                oSidur.dFullShatHatchala = oNewSidurim.ShatHatchalaNew;
                                oSidur.sShatHatchala = oSidur.dFullShatHatchala.ToString("HH:mm");
                                oSidur.dFullShatGmar = oObjSidurimOvdimUpd.SHAT_GMAR;
                                oSidur.sShatGmar = oObjSidurimOvdimUpd.SHAT_GMAR.ToString("HH:mm");
                                oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA = oSidur.dFullShatHatchala;
                            }
                        }
                        catch (Exception ex)
                        {
                            clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 4, oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, dShatYetzia, iMisparKnisa, "FixedShaotForSidurWithOneNesiaReeka04: " + ex.Message, null);
                            _bSuccsess = false;
                        }
                    }
                }
            }
        }

        private void FixedLina07()
        {
            SidurDM oSidur = new SidurDM();
            int iLina = 0;
           int iCountLina=0;
            try
            {
                
                if (htEmployeeDetails != null)
                {
                    for (int i = 0; i < htEmployeeDetails.Count; i++)
                    {
                        oSidur = (SidurDM)htEmployeeDetails[i];

                        if (oSidur.iZakaiLelina == 3) { iLina = 1; iCountLina += 1; }
                        else if (oSidur.iZakaiLelina == 4) { iLina = 2; iCountLina += 1; }
                    }
                }

                if (iCountLina==1) oObjYameyAvodaUpd.LINA = iLina;
                else oObjYameyAvodaUpd.LINA =0;

                oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", 7, "FixedLina07: " + ex.Message);
                _bSuccsess = false;
            }
        }

               
        private void UpdateZmaneyNesia20()
        {
            int iZmanNesia = 0;
            try
            {
                if (oMeafyeneyOved.IsMeafyenExist(51))
                {
                    if (!String.IsNullOrEmpty(oMeafyeneyOved.GetMeafyen(51).Value))
                    {
                        iZmanNesia = int.Parse(oMeafyeneyOved.GetMeafyen(51).Value.Substring(1));
                        switch (int.Parse(oMeafyeneyOved.GetMeafyen(51).Value.Substring(0, 1)))
                        {
                            case 1:
                                oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = iZmanNesia;
                                break;
                            case 2:
                                oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = iZmanNesia;
                                break;
                            case 3:
                                oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = (int)(Math.Ceiling(iZmanNesia / 2.0));
                                oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH;
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", 20, "UpdateZmaneyNesia20: " + ex.Message);
                _bSuccsess = false;
            }
        }

        private void SetSidurObjects()
        {
            clNewSidurim oNewSidurim;
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
            int index;
            try
            {
                //נעבור על האובייקט שמחזיק את כל הסידורים להן השתנה מספר הסידור בעקבות שינוי מספר 1                    
                for (int i = 0; i < htNewSidurim.Count; i++)
                {
                    oNewSidurim = (clNewSidurim)htNewSidurim[i];
                    //נביא את הסידור עם הנתונים העדכניים
                    oObjSidurimOvdimUpd = GetSidurOvdimObject(oNewSidurim.SidurNew, oNewSidurim.ShatHatchalaNew);
                    index = -1;
                    if (!CheckSidurNewKeyInObjectDelete(oNewSidurim, ref index))
                    {
                        if (!(oNewSidurim.SidurNew == oNewSidurim.SidurOld && oNewSidurim.ShatHatchalaNew == oNewSidurim.ShatHatchalaOld))
                        {

                            //נכניס סידור חדש עם כל הנתונים העדכניים ועם מספר הסידור החדש
                            oObjSidurimOvdimIns = new OBJ_SIDURIM_OVDIM();

                            oObjSidurimOvdimIns.MISPAR_ISHI = _iMisparIshi;
                            oObjSidurimOvdimIns.CHARIGA = oObjSidurimOvdimUpd.CHARIGA;
                            oObjSidurimOvdimIns.HAMARAT_SHABAT = oObjSidurimOvdimUpd.HAMARAT_SHABAT;
                            if (!oObjSidurimOvdimUpd.HASHLAMAIsNull)
                            {
                                oObjSidurimOvdimIns.HASHLAMA = oObjSidurimOvdimUpd.HASHLAMA;
                            }
                            oObjSidurimOvdimIns.LO_LETASHLUM = oObjSidurimOvdimUpd.LO_LETASHLUM;
                            if (!oObjSidurimOvdimUpd.OUT_MICHSAIsNull)
                            {
                                oObjSidurimOvdimIns.OUT_MICHSA = oObjSidurimOvdimUpd.OUT_MICHSA;
                            }
                            oObjSidurimOvdimIns.PITZUL_HAFSAKA = oObjSidurimOvdimUpd.PITZUL_HAFSAKA;
                            oObjSidurimOvdimIns.SHAT_GMAR = oObjSidurimOvdimUpd.SHAT_GMAR; //(oNewSidurim.ShatGmarNew != DateTime.MinValue ? oNewSidurim.ShatGmarNew : oObjSidurimOvdimUpd.SHAT_GMAR);
                            oObjSidurimOvdimIns.SHAT_HATCHALA = oNewSidurim.ShatHatchalaNew;
                            oObjSidurimOvdimIns.NEW_SHAT_HATCHALA = oNewSidurim.ShatHatchalaNew;
                            oObjSidurimOvdimIns.TAARICH = _dCardDate;
                            oObjSidurimOvdimIns.YOM_VISA = oObjSidurimOvdimUpd.YOM_VISA;
                            oObjSidurimOvdimIns.MIVTZA_VISA = oObjSidurimOvdimUpd.MIVTZA_VISA;
                            oObjSidurimOvdimIns.MEZAKE_NESIOT = oObjSidurimOvdimUpd.MEZAKE_NESIOT;
                            oObjSidurimOvdimIns.MEZAKE_HALBASHA = oObjSidurimOvdimUpd.MEZAKE_HALBASHA;
                            oObjSidurimOvdimIns.SHAT_HATCHALA_LETASHLUM = oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM;
                            oObjSidurimOvdimIns.SHAT_GMAR_LETASHLUM = oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM;
                            oObjSidurimOvdimIns.MISPAR_SIDUR = oNewSidurim.SidurNew;
                            oObjSidurimOvdimIns.NEW_MISPAR_SIDUR = oNewSidurim.SidurNew;
                            oObjSidurimOvdimIns.BITUL_O_HOSAFA = oObjSidurimOvdimUpd.BITUL_O_HOSAFA;
                            oObjSidurimOvdimIns.TAFKID_VISA = oObjSidurimOvdimUpd.TAFKID_VISA;
                            oObjSidurimOvdimIns.TOSEFET_GRIRA = oObjSidurimOvdimUpd.TOSEFET_GRIRA;
                            oObjSidurimOvdimIns.MIKUM_SHAON_KNISA = oObjSidurimOvdimUpd.MIKUM_SHAON_KNISA;
                            oObjSidurimOvdimIns.MIKUM_SHAON_YETZIA = oObjSidurimOvdimUpd.MIKUM_SHAON_YETZIA;
                            // oObjSidurimOvdimIns.KM_VISA_LEPREMIA = oObjSidurimOvdimUpd.KM_VISA_LEPREMIA;
                            oObjSidurimOvdimIns.ACHUZ_KNAS_LEPREMYAT_VISA = oObjSidurimOvdimUpd.ACHUZ_KNAS_LEPREMYAT_VISA;
                            oObjSidurimOvdimIns.ACHUZ_VIZA_BESIKUN = oObjSidurimOvdimUpd.ACHUZ_VIZA_BESIKUN;
                            //oObjSidurimOvdimIns.SUG_VISA = oObjSidurimOvdimUpd.SUG_VISA;
                            oObjSidurimOvdimIns.MISPAR_MUSACH_O_MACHSAN = oObjSidurimOvdimUpd.MISPAR_MUSACH_O_MACHSAN;
                            oObjSidurimOvdimIns.KOD_SIBA_LEDIVUCH_YADANI_IN = oObjSidurimOvdimUpd.KOD_SIBA_LEDIVUCH_YADANI_IN;
                            oObjSidurimOvdimIns.KOD_SIBA_LEDIVUCH_YADANI_OUT = oObjSidurimOvdimUpd.KOD_SIBA_LEDIVUCH_YADANI_OUT;
                            oObjSidurimOvdimIns.KOD_SIBA_LO_LETASHLUM = oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM;
                            oObjSidurimOvdimIns.SHAYAH_LEYOM_KODEM = oObjSidurimOvdimUpd.SHAYAH_LEYOM_KODEM;
                            oObjSidurimOvdimIns.MEADKEN_ACHARON = oObjSidurimOvdimUpd.MEADKEN_ACHARON;
                            oObjSidurimOvdimIns.TAARICH_IDKUN_ACHARON = oObjSidurimOvdimUpd.TAARICH_IDKUN_ACHARON;
                            oObjSidurimOvdimIns.HEARA = oObjSidurimOvdimUpd.HEARA;
                            oObjSidurimOvdimIns.MISPAR_SHIUREY_NEHIGA = oObjSidurimOvdimUpd.MISPAR_SHIUREY_NEHIGA;
                            //oObjSidurimOvdimIns.BUTAL = oObjSidurimOvdimUpd.BUTAL;
                            oObjSidurimOvdimIns.SUG_HAZMANAT_VISA = oObjSidurimOvdimUpd.SUG_HAZMANAT_VISA;
                            oObjSidurimOvdimIns.SHAT_HITIATZVUT = oObjSidurimOvdimUpd.SHAT_HITIATZVUT;

                            if (!oObjSidurimOvdimUpd.SECTOR_VISAIsNull)
                            {
                                oObjSidurimOvdimIns.SECTOR_VISA = oObjSidurimOvdimUpd.SECTOR_VISA;
                            }
                            oObjSidurimOvdimIns.NIDRESHET_HITIATZVUT = oObjSidurimOvdimUpd.NIDRESHET_HITIATZVUT;
                            oObjSidurimOvdimIns.PTOR_MEHITIATZVUT = oObjSidurimOvdimUpd.PTOR_MEHITIATZVUT;
                            oObjSidurimOvdimIns.HACHTAMA_BEATAR_LO_TAKIN = oObjSidurimOvdimUpd.HACHTAMA_BEATAR_LO_TAKIN;
                            if (oObjSidurimOvdimUpd.SUG_SIDUR != 0)
                                oObjSidurimOvdimIns.SUG_SIDUR = oObjSidurimOvdimUpd.SUG_SIDUR;

                            oCollSidurimOvdimIns.Add(oObjSidurimOvdimIns);

                            //באובייקט העדכון, נאפס את המשתנה שמציין שיש לעדכן את הרשומה
                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 0;

                            //נמחוק את הסידור השגוי    
                            oObjSidurimOvdimDel = new OBJ_SIDURIM_OVDIM();
                            oObjSidurimOvdimDel.MISPAR_ISHI = oObjSidurimOvdimUpd.MISPAR_ISHI;
                            oObjSidurimOvdimDel.SHAT_HATCHALA = oNewSidurim.ShatHatchalaOld;
                            oObjSidurimOvdimDel.NEW_SHAT_HATCHALA = oNewSidurim.ShatHatchalaNew;//DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatHatchala));
                            oObjSidurimOvdimDel.TAARICH = oObjSidurimOvdimUpd.TAARICH;
                            oObjSidurimOvdimDel.MISPAR_SIDUR = oNewSidurim.SidurOld; //מספר סידור קודם
                            oObjSidurimOvdimDel.UPDATE_OBJECT = 0;
                            oCollSidurimOvdimDel.Add(oObjSidurimOvdimDel);

                        }
                        else
                        {
                            //if(oNewSidurim.ShatGmarNew!=oNewSidurim.ShatGmarOld)
                            //    oObjSidurimOvdimUpd.SHAT_GMAR=oNewSidurim.ShatGmarNew;             
                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                        }
                    }
                    else
                    {
                        oCollSidurimOvdimDel.RemoveAt(index);
                        //נמחוק את הסידור השגוי    
                        oObjSidurimOvdimDel = new OBJ_SIDURIM_OVDIM();
                        oObjSidurimOvdimDel.MISPAR_ISHI = oObjSidurimOvdimUpd.MISPAR_ISHI;
                        oObjSidurimOvdimDel.SHAT_HATCHALA = oNewSidurim.ShatHatchalaOld;
                        oObjSidurimOvdimDel.NEW_SHAT_HATCHALA = oNewSidurim.ShatHatchalaNew;//DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatHatchala));
                        oObjSidurimOvdimDel.TAARICH = oObjSidurimOvdimUpd.TAARICH;
                        oObjSidurimOvdimDel.MISPAR_SIDUR = oNewSidurim.SidurOld; //מספר סידור קודם
                        oObjSidurimOvdimDel.UPDATE_OBJECT = 0;
                        oCollSidurimOvdimDel.Add(oObjSidurimOvdimDel);

                        oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", 1, "SetSidurObjects: " + ex.Message);
                _bSuccsess = false;
            }
        }

        private bool CheckSidurNewKeyInObjectDelete(clNewSidurim oSidurNew,ref int index)
        {
           // PeilutDM oPeilut;
           // SidurDM oSidur;
            bool bExist = false;
            try
            {
                //נמצא את הפעילות באובייקט פעילויות לעדכון
               
                for (int i = 0; i <= oCollSidurimOvdimDel.Count - 1; i++)
                {
                   // oSidur = (SidurDM)htEmployeeDetails[i];
                    if ((oCollSidurimOvdimDel.Value[i].MISPAR_SIDUR == oSidurNew.SidurNew) && (oCollSidurimOvdimDel.Value[i].SHAT_HATCHALA == oSidurNew.ShatHatchalaNew))
                    {
                        bExist = true;
                        index = i;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bExist;
        }

        private void InsertToObjSidurimOvdimForInsert(ref SidurDM oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimIns)
        {
            try
            {
                oObjSidurimOvdimIns.MISPAR_SIDUR = oSidur.iMisparSidur;
                oObjSidurimOvdimIns.MISPAR_ISHI = oSidur.iMisparIshi;
                oObjSidurimOvdimIns.CHARIGA = string.IsNullOrEmpty(oSidur.sChariga) ? 0 : int.Parse(oSidur.sChariga);
                //oObjSidurimOvdimIns.HAMARAT_SHABAT = string.IsNullOrEmpty(oSidur.sHamaratShabat) ? 0 : int.Parse(oSidur.sHamaratShabat);
                if (!String.IsNullOrEmpty(oSidur.sHashlama))
                {
                    oObjSidurimOvdimIns.HASHLAMA = int.Parse(oSidur.sHashlama);
                }
                oObjSidurimOvdimIns.SUG_HASHLAMA = oSidur.iSugHashlama;
                oObjSidurimOvdimIns.LO_LETASHLUM = oSidur.iLoLetashlum;
                if (!String.IsNullOrEmpty(oSidur.sOutMichsa))
                {
                    oObjSidurimOvdimIns.OUT_MICHSA = int.Parse(oSidur.sOutMichsa);
                }
                oObjSidurimOvdimIns.PITZUL_HAFSAKA = String.IsNullOrEmpty(oSidur.sPitzulHafsaka) ? 0 : int.Parse(oSidur.sPitzulHafsaka);
                oObjSidurimOvdimIns.SHAT_GMAR = oSidur.dFullShatGmar;//DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatGmar));
                oObjSidurimOvdimIns.SHAT_HATCHALA = oSidur.dFullShatHatchala;//DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatHatchala));
                oObjSidurimOvdimIns.TAARICH = oSidur.dSidurDate;
                oObjSidurimOvdimIns.MIVTZA_VISA = oSidur.iMivtzaVisa;
                oObjSidurimOvdimIns.TAFKID_VISA = oSidur.iTafkidVisa;
                oObjSidurimOvdimIns.MISPAR_SHIUREY_NEHIGA = oSidur.iMisparShiureyNehiga;
                oObjSidurimOvdimIns.SUG_HAZMANAT_VISA = oSidur.iSugHazmanatVisa;
                oObjSidurimOvdimIns.ACHUZ_VIZA_BESIKUN = oSidur.iAchuzVizaBesikun;
                oObjSidurimOvdimIns.ACHUZ_KNAS_LEPREMYAT_VISA = oSidur.iAchuzKnasLepremyatVisa;
                oObjSidurimOvdimIns.MISPAR_MUSACH_O_MACHSAN = oSidur.iMisparMusachOMachsan;
                if (oSidur.iSugSidurRagil != 0)
                    oObjSidurimOvdimIns.SUG_SIDUR = oSidur.iSugSidurRagil;

                oObjSidurimOvdimIns.YOM_VISA = string.IsNullOrEmpty(oSidur.sVisa) ? 0 : int.Parse(oSidur.sVisa);
                oObjSidurimOvdimIns.MEZAKE_NESIOT = oSidur.iMezakeNesiot;
                oObjSidurimOvdimIns.SHAT_HATCHALA_LETASHLUM = oSidur.dFullShatHatchalaLetashlum; //DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatHatchalaLetashlum));
                oObjSidurimOvdimIns.SHAT_GMAR_LETASHLUM = oSidur.dFullShatGmarLetashlum;//DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatGmarLetashlum));                
                oObjSidurimOvdimIns.MEADKEN_ACHARON = _iUserId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void InsertToObjSidurimOvdimForUpdate(ref SidurDM oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            try
            {
                oObjSidurimOvdimUpd.MISPAR_SIDUR = oSidur.iMisparSidur;
                oObjSidurimOvdimUpd.NEW_MISPAR_SIDUR = oSidur.iMisparSidur;
                oObjSidurimOvdimUpd.MISPAR_ISHI = oSidur.iMisparIshi;
                if (!String.IsNullOrEmpty(oSidur.sChariga))
                {
                    oObjSidurimOvdimUpd.CHARIGA = int.Parse(oSidur.sChariga);
                }

                //if (!String.IsNullOrEmpty(oSidur.sHamaratShabat))
                //{
                //    oObjSidurimOvdimUpd.HAMARAT_SHABAT = int.Parse(oSidur.sHamaratShabat);
                //}

                if (!String.IsNullOrEmpty(oSidur.sHashlama))
                {
                    oObjSidurimOvdimUpd.HASHLAMA = int.Parse(oSidur.sHashlama);
                }
                oObjSidurimOvdimUpd.SUG_HASHLAMA = oSidur.iSugHashlama;
                oObjSidurimOvdimUpd.LO_LETASHLUM = oSidur.iLoLetashlum;
                if (!String.IsNullOrEmpty(oSidur.sOutMichsa))
                {
                    oObjSidurimOvdimUpd.OUT_MICHSA = int.Parse(oSidur.sOutMichsa);
                }

                if (!String.IsNullOrEmpty(oSidur.sPitzulHafsaka))
                {
                    oObjSidurimOvdimUpd.PITZUL_HAFSAKA = int.Parse(oSidur.sPitzulHafsaka);
                }
                if (oSidur.dFullShatGmar != DateTime.MinValue)
                {
                    oObjSidurimOvdimUpd.SHAT_GMAR = oSidur.dFullShatGmar;//DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatGmar));
                }
                oObjSidurimOvdimUpd.SHAT_HATCHALA = oSidur.dFullShatHatchala;//DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatHatchala));
                oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA = oSidur.dFullShatHatchala;//DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatHatchala));
                oObjSidurimOvdimUpd.TAARICH =oSidur.dSidurDate;
                if (!String.IsNullOrEmpty(oSidur.sVisa))
                {
                    oObjSidurimOvdimUpd.YOM_VISA = int.Parse(oSidur.sVisa);
                }

                oObjSidurimOvdimUpd.MIVTZA_VISA = oSidur.iMivtzaVisa;
               
                oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = oSidur.dFullShatHatchalaLetashlum; //DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatHatchalaLetashlum));
                oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = oSidur.dFullShatGmarLetashlum;//DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatGmarLetashlum));
                oObjSidurimOvdimUpd.MEZAKE_NESIOT = oSidur.iMezakeNesiot;
                oObjSidurimOvdimUpd.MEZAKE_HALBASHA = oSidur.iMezakeHalbasha;
                oObjSidurimOvdimUpd.TOSEFET_GRIRA = oSidur.iTosefetGrira;
                if (!String.IsNullOrEmpty(oSidur.sMikumShaonKnisa))
                {
                    oObjSidurimOvdimUpd.MIKUM_SHAON_KNISA = int.Parse(oSidur.sMikumShaonKnisa);
                }

                if (!String.IsNullOrEmpty(oSidur.sMikumShaonYetzia))
                {
                    oObjSidurimOvdimUpd.MIKUM_SHAON_YETZIA = int.Parse(oSidur.sMikumShaonYetzia);
                }

                //oObjSidurimOvdimUpd.KM_VISA_LEPREMIA = oSidur.iKmVisaLepremia;                
                oObjSidurimOvdimUpd.ACHUZ_KNAS_LEPREMYAT_VISA = oSidur.iAchuzKnasLepremyatVisa;
                oObjSidurimOvdimUpd.ACHUZ_VIZA_BESIKUN = oSidur.iAchuzVizaBesikun;
                //oObjSidurimOvdimUpd.SUG_VISA = oSidur.iSugVisa;
                oObjSidurimOvdimUpd.MISPAR_MUSACH_O_MACHSAN = oSidur.iMisparMusachOMachsan;
                oObjSidurimOvdimUpd.KOD_SIBA_LEDIVUCH_YADANI_IN = oSidur.iKodSibaLedivuchYadaniIn;
                oObjSidurimOvdimUpd.KOD_SIBA_LEDIVUCH_YADANI_OUT = oSidur.iKodSibaLedivuchYadaniOut;
                oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM = oSidur.iKodSibaLoLetashlum;
                oObjSidurimOvdimUpd.SHAYAH_LEYOM_KODEM = oSidur.iShayahLeyomKodem;
                oObjSidurimOvdimUpd.MEADKEN_ACHARON = _iUserId;
                oObjSidurimOvdimUpd.TAARICH_IDKUN_ACHARON = oSidur.dTaarichIdkunAcharon;
                oObjSidurimOvdimUpd.TAFKID_VISA = oSidur.iTafkidVisa;
                
                oObjSidurimOvdimUpd.HEARA = oSidur.sHeara;
                oObjSidurimOvdimUpd.MISPAR_SHIUREY_NEHIGA = oSidur.iMisparShiureyNehiga;
                //oObjSidurimOvdimUpd.BUTAL = oSidur.iButal;
                oObjSidurimOvdimUpd.BITUL_O_HOSAFA = oSidur.iBitulOHosafa;
                oObjSidurimOvdimUpd.SUG_HAZMANAT_VISA = oSidur.iSugHazmanatVisa;
                if (oSidur.iSugSidurRagil != 0)
                 oObjSidurimOvdimUpd.SUG_SIDUR = oSidur.iSugSidurRagil;
                if (oSidur.bSectorVisaExists)
                {
                    oObjSidurimOvdimUpd.SECTOR_VISA = oSidur.iSectorVisa;
                }
                if (oSidur.dShatHitiatzvut != DateTime.MinValue)
                {
                    oObjSidurimOvdimUpd.SHAT_HITIATZVUT = oSidur.dShatHitiatzvut;
                }
                if (oSidur.iNidreshetHitiatzvut > 0)
                {
                    oObjSidurimOvdimUpd.NIDRESHET_HITIATZVUT = oSidur.iNidreshetHitiatzvut;
                }
                if (oSidur.iPtorMehitiatzvut > 0)
                {
                    oObjSidurimOvdimUpd.PTOR_MEHITIATZVUT = oSidur.iPtorMehitiatzvut;
                }
                if (oSidur.iHachtamaBeatarLoTakin > 0)
                {
                    oObjSidurimOvdimUpd.HACHTAMA_BEATAR_LO_TAKIN = oSidur.iHachtamaBeatarLoTakin.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void InsertToObjSidurimOvdimForDelete(ref SidurDM oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimDel)
        {
            try
            {
                for (int i = 0; i <= oCollSidurimOvdimUpd.Count - 1; i++)
                {
                    if ((oCollSidurimOvdimUpd.Value[i].NEW_MISPAR_SIDUR == oSidur.iMisparSidur) && (oCollSidurimOvdimUpd.Value[i].NEW_SHAT_HATCHALA == oSidur.dFullShatHatchala) && oCollSidurimOvdimUpd.Value[i].UPDATE_OBJECT == 1)
                    {
                        oCollSidurimOvdimUpd.RemoveAt(i);
                    }
                }

        
                for (int i = 0; i <= oCollSidurimOvdimIns.Count - 1; i++)
                {
                    if ((oCollSidurimOvdimIns.Value[i].MISPAR_SIDUR == oSidur.iMisparSidur) && (oCollSidurimOvdimIns.Value[i].SHAT_HATCHALA == oSidur.dFullShatHatchala))
                    {
                        oCollSidurimOvdimIns.RemoveAt(i);
                    }
                }
            
          
                oObjSidurimOvdimDel.MISPAR_SIDUR = oSidur.iMisparSidur;
                oObjSidurimOvdimDel.MISPAR_ISHI = oSidur.iMisparIshi;
                oObjSidurimOvdimDel.SHAT_HATCHALA = oSidur.dFullShatHatchala;//DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatHatchala));
                oObjSidurimOvdimDel.TAARICH = oSidur.dSidurDate;
                oObjSidurimOvdimDel.MEADKEN_ACHARON = _iUserId;
                oObjSidurimOvdimDel.BITUL_O_HOSAFA = oSidur.iBitulOHosafa;
                oObjSidurimOvdimDel.UPDATE_OBJECT = 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void InsertToObjPeilutOvdimForInsert(ref SidurDM oSidur, ref OBJ_PEILUT_OVDIM oObjPeilutOvdimIns)
        {
           try{

                oObjPeilutOvdimIns.MISPAR_ISHI = oSidur.iMisparIshi;
                oObjPeilutOvdimIns.TAARICH =oSidur.dSidurDate ;
                oObjPeilutOvdimIns.MISPAR_SIDUR = oSidur.iMisparSidur;
                oObjPeilutOvdimIns.SHAT_HATCHALA_SIDUR = oSidur.dFullShatHatchala;
                oObjPeilutOvdimIns.MISPAR_KNISA = 0;
                oObjPeilutOvdimIns.MEADKEN_ACHARON = _iUserId;
             }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void InsertToObjPeilutOvdimForUpdate(ref PeilutDM oPeilut,  OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, ref OBJ_PEILUT_OVDIM oObjPeilutOvdimUpd)
        {
            try
            {
                oObjPeilutOvdimUpd.MISPAR_SIDUR = oObjSidurimOvdimUpd.MISPAR_SIDUR;
                oObjPeilutOvdimUpd.MISPAR_ISHI = oObjSidurimOvdimUpd.MISPAR_ISHI;
                oObjPeilutOvdimUpd.SHAT_HATCHALA_SIDUR = oObjSidurimOvdimUpd.SHAT_HATCHALA;
                oObjPeilutOvdimUpd.NEW_SHAT_HATCHALA_SIDUR = oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA;
                oObjPeilutOvdimUpd.SHAT_YETZIA = oPeilut.dFullShatYetzia;
                oObjPeilutOvdimUpd.NEW_SHAT_YETZIA = oPeilut.dFullShatYetzia;
                oObjPeilutOvdimUpd.TAARICH = oObjSidurimOvdimUpd.TAARICH.Date;
                oObjPeilutOvdimUpd.MISPAR_KNISA = oPeilut.iMisparKnisa;
                oObjPeilutOvdimUpd.MISPAR_VISA = oPeilut.lMisparVisa;
                oObjPeilutOvdimUpd.MAKAT_NESIA = oPeilut.lMakatNesia;
                oObjPeilutOvdimUpd.MISPAR_MATALA  = oPeilut.lMisparMatala;
                oObjPeilutOvdimUpd.BITUL_O_HOSAFA = oPeilut.iBitulOHosafa;
                oObjPeilutOvdimUpd.NEW_MISPAR_SIDUR = oObjSidurimOvdimUpd.NEW_MISPAR_SIDUR;
                oObjPeilutOvdimUpd.KM_VISA = oPeilut.iKmVisa;
                oObjPeilutOvdimUpd.KOD_SHINUY_PREMIA = oPeilut.iKodShinuyPremia;
                oObjPeilutOvdimUpd.MISPAR_SIDURI_OTO = oPeilut.lMisparSiduriOto;
                oObjPeilutOvdimUpd.MEADKEN_ACHARON = _iUserId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void InsertToObjPeilutOvdimForDelete(ref PeilutDM oPeilut, ref SidurDM oSidur, ref OBJ_PEILUT_OVDIM oObjPeilutOvdimDel)
        {
            int iMisparSidur = oPeilut.iPeilutMisparSidur;
            DateTime dShatHatchala=oSidur.dFullShatHatchala;
            DateTime dFullShatYetzia = oPeilut.dFullShatYetzia;
            try
            {
                for (int i = 0; i <= oCollPeilutOvdimUpd.Count - 1; i++)
                {
                    if ((oCollPeilutOvdimUpd.Value[i].NEW_MISPAR_SIDUR == oSidur.iMisparSidur) && (oCollPeilutOvdimUpd.Value[i].NEW_SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
                        && (oCollPeilutOvdimUpd.Value[i].NEW_SHAT_YETZIA == oPeilut.dFullShatYetzia) && (oCollPeilutOvdimUpd.Value[i].MISPAR_KNISA == oPeilut.iMisparKnisa) && oCollPeilutOvdimUpd.Value[i].UPDATE_OBJECT == 1)
                    {
                        oObjPeilutOvdimUpd = oCollPeilutOvdimUpd.Value[i];
                        iMisparSidur = oObjPeilutOvdimUpd.MISPAR_SIDUR;
                        dShatHatchala = oObjPeilutOvdimUpd.SHAT_HATCHALA_SIDUR;
                         dFullShatYetzia=oObjPeilutOvdimUpd.SHAT_YETZIA;
                        oCollPeilutOvdimUpd.RemoveAt(i);
                        break;
                    }
                }
                if (oObjPeilutOvdimUpd == null)
                {
                    for (int i = 0; i <= oCollPeilutOvdimIns.Count - 1; i++)
                    {
                        if ((oCollPeilutOvdimIns.Value[i].MISPAR_SIDUR == oSidur.iMisparSidur) && (oCollPeilutOvdimIns.Value[i].SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
                            && (oCollPeilutOvdimIns.Value[i].SHAT_YETZIA == oPeilut.dFullShatYetzia) && (oCollPeilutOvdimIns.Value[i].MISPAR_KNISA == oPeilut.iMisparKnisa))
                        {
                            oObjPeilutOvdimUpd = oCollPeilutOvdimIns.Value[i];
                            iMisparSidur = oObjPeilutOvdimUpd.MISPAR_SIDUR;
                            dShatHatchala = oObjPeilutOvdimUpd.SHAT_HATCHALA_SIDUR;
                            dFullShatYetzia=oObjPeilutOvdimUpd.SHAT_YETZIA;
                            oCollPeilutOvdimIns.RemoveAt(i);
                            break;
                        }
                    }
                }

                oObjPeilutOvdimDel.MISPAR_ISHI = oSidur.iMisparIshi;
                oObjPeilutOvdimDel.MISPAR_SIDUR =iMisparSidur;
                oObjPeilutOvdimDel.TAARICH = oPeilut.dCardDate;
                oObjPeilutOvdimDel.SHAT_HATCHALA_SIDUR = dShatHatchala;
                oObjPeilutOvdimDel.SHAT_YETZIA = dFullShatYetzia;
                oObjPeilutOvdimDel.MISPAR_KNISA = oPeilut.iMisparKnisa;
                oObjPeilutOvdimDel.MAKAT_NESIA = oPeilut.lMakatNesia;
                oObjPeilutOvdimDel.BITUL_O_HOSAFA = oPeilut.iBitulOHosafa;
                oObjPeilutOvdimDel.MEADKEN_ACHARON = _iUserId;
             }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void InsertToYameyAvodaForUpdate(DateTime dCardDate, ref OBJ_YAMEY_AVODA_OVDIM oObjYameyAvodaUpd, ref OvedYomAvodaDetailsDM oOvedYomAvodaDetails)
        {
            try
            {
                oObjYameyAvodaUpd.MISPAR_ISHI = oOvedYomAvodaDetails.iMisparIshi;
                oObjYameyAvodaUpd.TACHOGRAF = oOvedYomAvodaDetails.sTachograf;
                if (!String.IsNullOrEmpty(oOvedYomAvodaDetails.sHalbasha))
                {
                    oObjYameyAvodaUpd.HALBASHA = int.Parse(oOvedYomAvodaDetails.sHalbasha);
                }
                if (!String.IsNullOrEmpty(oOvedYomAvodaDetails.sHashlamaLeyom))
                {
                    oObjYameyAvodaUpd.HASHLAMA_LEYOM = int.Parse(oOvedYomAvodaDetails.sHashlamaLeyom);
                }
                if (!String.IsNullOrEmpty(oOvedYomAvodaDetails.sBitulZmanNesiot))
                {
                    oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = int.Parse(oOvedYomAvodaDetails.sBitulZmanNesiot);
                }
                if (!String.IsNullOrEmpty(oOvedYomAvodaDetails.sLina))
                {
                    oObjYameyAvodaUpd.LINA = int.Parse(oOvedYomAvodaDetails.sLina);
                }
                oObjYameyAvodaUpd.TAARICH = dCardDate;
                oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = oOvedYomAvodaDetails.iZmanNesiaHaloch;
                oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = oOvedYomAvodaDetails.iZmanNesiaHazor;
                if (!String.IsNullOrEmpty(oOvedYomAvodaDetails.sHamara))
                {
                    oObjYameyAvodaUpd.HAMARAT_SHABAT = int.Parse(oOvedYomAvodaDetails.sHamara);
                }

                oObjYameyAvodaUpd.SIBAT_HASHLAMA_LEYOM = oOvedYomAvodaDetails.iSibatHashlamaLeyom;

                oObjYameyAvodaUpd.MEADKEN_ACHARON = _iUserId;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void KonnutGrira03(DateTime dCardDate)
        {
            //כוננות גרירה
            //סימון כוננות גרירה "לא לתשלום". 
            //גרירה בפועל
            //הוספת קודים לפעילות גרירה בפועל:
            //"זמן נסיעה", "השלמה
            float Minutes;
            SidurDM oSidur=null;
            bool bSidurKonnutGrira = false;
            int iSidurKonnutGrira = 0;
            SidurDM oSidurKonenutGrira =null;
             DataTable dtSidurGrira=new DataTable();
            int iTypeGrira,i;
            int iGriraNum=0;
            SidurDM oSidurGrira=null;
            OBJ_SIDURIM_OVDIM oObjSidurGriraUpd=null;
            OBJ_SIDURIM_OVDIM oObjSidurimConenutGriraUpd;
            try
            {
                if (htEmployeeDetails != null)
                {
                    //נעבור על כל הסידורים ונבדוק שיש כוננות גרירה וכוננות גרירה בפועל
                    for ( i = 0; i < htEmployeeDetails.Count; i++)
                    {
                        oSidur = (SidurDM)htEmployeeDetails[i];

                        //נבדוק אם סידור כוננות גרירה
                        if (IsKonnutGrira(ref oSidur, dCardDate))
                        {
                            bSidurKonnutGrira = true;
                            iSidurKonnutGrira = i;
                            break;
                        }
                    }

                    if ((bSidurKonnutGrira))
                    {
                        
                        for (int j = 0; j < htEmployeeDetails.Count; j++)
                        {
                            oSidurGrira = (SidurDM)htEmployeeDetails[j];

                            //אם נמצא  סידור של כוננות גרירה, נחפש סידורים של כוננות גרירה בפועל ונשמור את האינדקס שלהם במערך
                            //נבדוק אם סידור גרירה בפועל בטווח הזמן של סידור הגרירה
                            if (iSidurKonnutGrira != j)
                            {
                                if (IsActualKonnutGrira(ref oSidurGrira, iSidurKonnutGrira, out iTypeGrira))
                                {
                                    oObjSidurGriraUpd = GetUpdSidurObject(oSidurGrira);
                                    iGriraNum += 1;

                                    //אם יש סידור כוננות גרירה  וגם לפחות סידור גרירה בפועל אחד 
                                    if ((bSidurKonnutGrira) && (oObjSidurGriraUpd != null))
                                    {
                                         oSidurKonenutGrira = (SidurDM)htEmployeeDetails[iSidurKonnutGrira];
                                        if (!CheckIdkunRashemet("LO_LETASHLUM", oSidurKonenutGrira.iMisparSidur, oSidurKonenutGrira.dFullShatHatchala))
                                        {
                                            SetLoLetashlum(oSidurKonenutGrira);
                                        }

                                        if (iTypeGrira == 2)
                                        {
                                            SetShatGmarGrira(ref oObjSidurGriraUpd, oSidurKonenutGrira, oSidurGrira);
                                            if (oObjSidurGriraUpd.UPDATE_OBJECT == 1)
                                            {
                                                oSidurGrira.dFullShatGmar = oObjSidurGriraUpd.SHAT_GMAR;
                                                oSidurGrira.sShatGmar = oSidurGrira.dFullShatGmar.ToString("HH:mm");
                                                htEmployeeDetails[j] = oSidurGrira;
                                            }
                                        }
                                        if (iTypeGrira == 1)
                                        {
                                            SetBitulZmanNesiot(ref oObjSidurGriraUpd, oSidurKonenutGrira, oSidurGrira);

                                            SetZmanHashlama(ref oObjSidurGriraUpd, oSidurKonenutGrira, iGriraNum, oSidurGrira);
                                        }
                                    }
                                    
                                    break;
                                }
                            }


                            //בכל המקומות בהם מחפשים סידור גרירה בפועל בטווח הזמן של כוננות הגרירה וסידור כוננות הגרירה מתחיל לפני חצות ומסתיים אחרי חצות, 
                            if (bSidurKonnutGrira && oSidur.dFullShatHatchala < DateHelper.GetDateTimeFromStringHour("24:00", _dCardDate) && oSidur.dFullShatGmar > DateHelper.GetDateTimeFromStringHour("24:00", _dCardDate))
                            {
                                // יש לחפש סידור גרירה בתאריך כרטיס העבודה ובתאריך כרטיס העבודה +1.
                                if (CheckHaveSidurGrira(_iMisparIshi, _dCardDate.AddDays(1), ref dtSidurGrira))
                                {
                                    for (i = 0; i < dtSidurGrira.Rows.Count; i++)
                                    {
                                        var sidurManager = ServiceLocator.Current.GetInstance<ISidurManager>();
                                        oSidurGrira = sidurManager.CreateClsSidurFromSidurayGrira(dtSidurGrira.Rows[i]);

                                        if (IsActualKonnutGrira(ref oSidurGrira, iSidurKonnutGrira, out iTypeGrira))
                                        {
                                            oObjSidurGriraUpd = new OBJ_SIDURIM_OVDIM();
                                            InsertToObjSidurimOvdimForUpdate(ref oSidurGrira, ref oObjSidurGriraUpd);
                                            oCollSidurimOvdimUpd.Add(oObjSidurGriraUpd);

                                            iGriraNum += 1;

                                            //אם יש סידור כוננות גרירה  וגם לפחות סידור גרירה בפועל אחד 
                                            if ((bSidurKonnutGrira) && (oObjSidurGriraUpd != null))
                                            {
                                                 oSidurKonenutGrira = (SidurDM)htEmployeeDetails[iSidurKonnutGrira];
                                                if (!CheckIdkunRashemet("LO_LETASHLUM", oSidurKonenutGrira.iMisparSidur, oSidurKonenutGrira.dFullShatHatchala))
                                                {
                                                    SetLoLetashlum(oSidurKonenutGrira);
                                                }

                                                if (iTypeGrira == 2)
                                                    SetShatGmarGrira(ref oObjSidurGriraUpd, oSidurKonenutGrira, oSidurGrira);
                                                if (iTypeGrira == 1)
                                                {
                                                    SetBitulZmanNesiot(ref oObjSidurGriraUpd, oSidurKonenutGrira, oSidurGrira);

                                                    SetZmanHashlama(ref oObjSidurGriraUpd, oSidurKonenutGrira, iGriraNum, oSidurGrira);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }

                    }
                    //לשאול אם כוננות לא לתשלום =0
                    if ((bSidurKonnutGrira))
                    {
                        oSidurKonenutGrira = (SidurDM)htEmployeeDetails[iSidurKonnutGrira];
                        oObjSidurimConenutGriraUpd = GetSidurOvdimObject(oSidurKonenutGrira.iMisparSidur, oSidurKonenutGrira.dFullShatHatchala);
                        if (oObjSidurimConenutGriraUpd.LO_LETASHLUM == 0)
                        {
                            oObjSidurimConenutGriraUpd.UPDATE_OBJECT = 1;
                            Minutes = float.Parse((oSidurKonenutGrira.dFullShatGmar - oSidurKonenutGrira.dFullShatHatchala).TotalMinutes.ToString());
                            if (Minutes > _oParameters.iMinZmanGriraDarom)
                            {
                                if (int.Parse(oSidurKonenutGrira.iMisparSidur.ToString().Substring(0, 2)) >= 25)
                                {
                                    oObjSidurimConenutGriraUpd.SHAT_HATCHALA_LETASHLUM = oSidurKonenutGrira.dFullShatHatchala;
                                    oObjSidurimConenutGriraUpd.SHAT_GMAR_LETASHLUM = oSidurKonenutGrira.dFullShatHatchala.AddMinutes(_oParameters.iMinZmanGriraDarom);
                                }
                                else
                                {
                                    oObjSidurimConenutGriraUpd.SHAT_HATCHALA_LETASHLUM = oSidurKonenutGrira.dFullShatHatchala;
                                    oObjSidurimConenutGriraUpd.SHAT_GMAR_LETASHLUM = oSidurKonenutGrira.dFullShatHatchala.AddMinutes(_oParameters.iMinZmanGriraTzafon);
                                }
                            }
                            else if (Minutes > _oParameters.iMinZmanGriraTzafon && Minutes < _oParameters.iMinZmanGriraDarom)
                            {
                                if (int.Parse(oSidurKonenutGrira.iMisparSidur.ToString().Substring(0, 2)) >= 25)
                                {
                                    oObjSidurimConenutGriraUpd.SHAT_HATCHALA_LETASHLUM = oSidurKonenutGrira.dFullShatHatchala;
                                    oObjSidurimConenutGriraUpd.SHAT_GMAR_LETASHLUM = oSidurKonenutGrira.dFullShatGmar;
                                }
                                else
                                {
                                    oObjSidurimConenutGriraUpd.SHAT_HATCHALA_LETASHLUM = oSidurKonenutGrira.dFullShatHatchala;
                                    oObjSidurimConenutGriraUpd.SHAT_GMAR_LETASHLUM = oSidurKonenutGrira.dFullShatHatchala.AddMinutes(_oParameters.iMinZmanGriraTzafon);
                                }
                            }
                            else if (Minutes <= _oParameters.iMinZmanGriraTzafon)
                            {
                                oObjSidurimConenutGriraUpd.SHAT_HATCHALA_LETASHLUM = oSidurKonenutGrira.dFullShatHatchala;
                                oObjSidurimConenutGriraUpd.SHAT_GMAR_LETASHLUM = oSidurKonenutGrira.dFullShatGmar;
                            }
                        }
                    }                   
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, _iMisparIshi, "E", 3, dCardDate, "KonnutGrira03: " + ex.Message);
                _bSuccsess = false;
            }
        }


       

        private void AddElementMechine05_2()
        {
            SidurDM oSidur;
            int iMeshechHachanotMechona = 0;
            int iMeshechHachanotMechonaNosafot = 0;
            int iNumHachanotMechonaForSidur = 0;
            OBJ_PEILUT_OVDIM oObjPeilutUpd, oObjPeilutDel;
            PeilutDM oPeilut;
            DateTime dShatYetzia, dShatYetziaFirst;
            int iCountPeiluyot;
            int j, iIndexElement;
            int idakot, iMeshechElement;
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
            iIndexElement = -1;
            SourceObj SourceObject;
            int iIndexFirstElementMachine = 0;
            bool bHaveFirstElementMachine = false;
            int i, iCountPeiluyotIns, l;
            bool bUsedMazanTichnun = false;
            bool bUsedMazanTichnunInSidur = false;
            //הוספת אלמנט הכנת מכונה אם העובד החליף רכב ולא מופיע אלמנט זה או בתחילת יום
            try
            {
                //מחיקת כל פעילויות הכנת מכונה 
                for (i = 0; i < htEmployeeDetails.Count; i++)
                {
                    oSidur = (SidurDM)htEmployeeDetails[i];
                    j = 0;
                    iCountPeiluyot = oSidur.htPeilut.Count;
                    if (j < iCountPeiluyot)
                    {
                        do
                        {
                            oPeilut = (PeilutDM)oSidur.htPeilut[j];
                            if (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == enElementHachanatMechona.Element701.GetHashCode().ToString() || oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == enElementHachanatMechona.Element712.GetHashCode().ToString() || oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == enElementHachanatMechona.Element711.GetHashCode().ToString())
                            {
                                oObjPeilutDel = new OBJ_PEILUT_OVDIM();
                                InsertToObjPeilutOvdimForDelete(ref oPeilut, ref oSidur, ref oObjPeilutDel);
                                oObjPeilutDel.BITUL_O_HOSAFA = 3;// int.Parse(clGeneral.enBitulOHosafa.BitulAutomat.ToString());
                                oCollPeilutOvdimDel.Add(oObjPeilutDel);

                                oSidur.htPeilut.RemoveAt(j);
                                j -= 1;
                                for (l = 0; l <= oCollPeilutOvdimUpd.Count - 1; l++)
                                {
                                    if ((oCollPeilutOvdimUpd.Value[l].NEW_MISPAR_SIDUR == oSidur.iMisparSidur) && (oCollPeilutOvdimUpd.Value[l].NEW_SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
                                        && (oCollPeilutOvdimUpd.Value[l].NEW_SHAT_YETZIA == oPeilut.dFullShatYetzia) && (oCollPeilutOvdimUpd.Value[l].MISPAR_KNISA == oPeilut.iMisparKnisa))
                                    {
                                        //oCollPeilutOvdimUpd.Value[l].UPDATE_OBJECT = 0;
                                        oCollPeilutOvdimUpd.RemoveAt(l);
                                    }
                                }
                                l = 0;
                                iCountPeiluyotIns = oCollPeilutOvdimIns.Count;
                                if (l < iCountPeiluyotIns)
                                {
                                    do
                                    {
                                        if ((oCollPeilutOvdimIns.Value[l].MISPAR_SIDUR == oSidur.iMisparSidur) && (oCollPeilutOvdimIns.Value[l].SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
                                               && (oCollPeilutOvdimIns.Value[l].SHAT_YETZIA == oPeilut.dFullShatYetzia) && (oCollPeilutOvdimIns.Value[l].MISPAR_KNISA == oPeilut.iMisparKnisa))
                                        {
                                            oCollPeilutOvdimIns.RemoveAt(l);
                                            l -= 1;
                                        }
                                        l += 1;
                                        iCountPeiluyotIns = oCollPeilutOvdimIns.Count;
                                    } while (l < iCountPeiluyotIns);
                                }
                            }
                            j += 1;
                            iCountPeiluyot = oSidur.htPeilut.Count;
                        } while (j < iCountPeiluyot);
                    }

                }

                for (i = 0; i < htEmployeeDetails.Count; i++)
                {
                    oSidur = (SidurDM)htEmployeeDetails[i];
                    dShatYetzia = oSidur.dFullShatHatchala;
                    dShatYetziaFirst = oSidur.dFullShatHatchala;
                    oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur);
                    iNumHachanotMechonaForSidur = 0;
                    iIndexFirstElementMachine = 0;
                    bHaveFirstElementMachine = false;
                    //אם סידור ראשון ביום קיימים שני מקרים
                    if (iMeshechHachanotMechona == 0)
                    {
                        AddElementMachineForFirstSidur_2(ref oSidur, i, ref dShatYetziaFirst, ref iMeshechHachanotMechona, ref iNumHachanotMechonaForSidur, ref iIndexElement, ref iIndexFirstElementMachine, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur, ref oObjSidurimOvdimUpd);
                        iIndexFirstElementMachine += 1;
                        if (iIndexElement == 0) bHaveFirstElementMachine = true;
                    }

                    AddElementMachineForNextSidur_2(ref oSidur, ref dShatYetzia, i, iIndexFirstElementMachine, ref iMeshechHachanotMechona, ref iNumHachanotMechonaForSidur,ref iMeshechHachanotMechonaNosafot, ref iIndexElement, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur, ref oObjSidurimOvdimUpd);
                    if (!bHaveFirstElementMachine)
                    {
                        dShatYetziaFirst = dShatYetzia;
                    }
                    htEmployeeDetails[i] = oSidur;
                    if (bUsedMazanTichnunInSidur)
                        bUsedMazanTichnun = true;
                   // if (string.IsNullOrEmpty(oSidur.sSidurVisaKod))
                   // {
                        if (dShatYetziaFirst != oSidur.dFullShatHatchala && (!CheckIdkunRashemet("SHAT_HATCHALA", oSidur.iMisparSidur, oSidur.dFullShatHatchala)))
                        {

                            clNewSidurim oNewSidurim = FindSidurOnHtNewSidurim(oSidur.iMisparSidur, oSidur.dFullShatHatchala);

                            oNewSidurim.SidurIndex = i;
                            oNewSidurim.ShatHatchalaNew = dShatYetziaFirst;
                            oNewSidurim.SidurNew = oSidur.iMisparSidur;


                            UpdateObjectUpdSidurim(oNewSidurim);
                            for (j = 0; j < oSidur.htPeilut.Count; j++)
                            {
                                oPeilut = (PeilutDM)oSidur.htPeilut[j];

                                if (!CheckPeilutObjectDelete(i, j))
                                {
                                    oObjPeilutUpd = GetUpdPeilutObject(i, oPeilut, out SourceObject, oObjSidurimOvdimUpd);
                                    if (SourceObject == SourceObj.Insert)
                                    {
                                        oObjPeilutUpd.SHAT_HATCHALA_SIDUR = oNewSidurim.ShatHatchalaNew;
                                    }
                                    else
                                    {
                                        oObjPeilutUpd.NEW_SHAT_HATCHALA_SIDUR = oNewSidurim.ShatHatchalaNew;
                                        oObjPeilutUpd.UPDATE_OBJECT = 1;
                                    }
                                }

                            }
                            //UpdatePeiluyotMevutalotYadani(i, oNewSidurim, oObjSidurimOvdimUpd);
                            UpdateIdkunRashemet(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oNewSidurim.ShatHatchalaNew);
                            UpdateApprovalErrors(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oNewSidurim.ShatHatchalaNew);

                            oSidur.dFullShatHatchala = oNewSidurim.ShatHatchalaNew;
                            oSidur.sShatHatchala = oSidur.dFullShatHatchala.ToString("HH:mm");
                            oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA = oNewSidurim.ShatHatchalaNew;

                        }
                        else if (dShatYetziaFirst < oSidur.dFullShatHatchala && (CheckIdkunRashemet("SHAT_HATCHALA", oSidur.iMisparSidur, oSidur.dFullShatHatchala)))
                        {
                            int iMinuts;
                            for (j = 0; j < oSidur.htPeilut.Count; j++)
                            {
                                oPeilut = (PeilutDM)oSidur.htPeilut[j];

                                if (oPeilut.dFullShatYetzia == dShatYetziaFirst)
                                {
                                    iMinuts = int.Parse((oSidur.dFullShatHatchala - dShatYetziaFirst).TotalMinutes.ToString());

                                    if (int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3)) > iMinuts)
                                    {
                                        oObjPeilutUpd = GetUpdPeilutObject(i, oPeilut, out SourceObject, oObjSidurimOvdimUpd);

                                        idakot =FindDuplicatPeiluyot_2(j, oSidur.dFullShatHatchala, i, ref oSidur, ref oObjSidurimOvdimUpd);
                                        if (idakot > 0)
                                        {
                                            iMeshechElement = int.Parse(oObjPeilutUpd.MAKAT_NESIA.ToString().Substring(3, 3));
                                            if (idakot <= iMeshechElement)
                                                oObjPeilutUpd.MAKAT_NESIA = long.Parse(String.Concat("701", (iMeshechElement - idakot).ToString().PadLeft(3, (char)48), "00"));
                                            else oObjPeilutUpd.MAKAT_NESIA = long.Parse(String.Concat("701", "000", "00"));
                                            oObjPeilutUpd.SHAT_YETZIA = oSidur.dFullShatHatchala.AddMinutes(idakot);
                                        }
                                        else  oObjPeilutUpd.SHAT_YETZIA = oSidur.dFullShatHatchala;

                                        oPeilut.dFullShatYetzia = oObjPeilutUpd.SHAT_YETZIA;
                                        oPeilut.lMakatNesia = oObjPeilutUpd.MAKAT_NESIA;
                                        oPeilut.sShatYetzia = oPeilut.dFullShatYetzia.ToString("HH:mm");
                                        iMinuts = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3)) - iMinuts;
                                        oObjPeilutUpd.MAKAT_NESIA = long.Parse(string.Concat(oPeilut.lMakatNesia.ToString().Substring(0, 3), iMinuts.ToString().PadLeft(3, (char)48), oPeilut.lMakatNesia.ToString().Substring(6, 2)));
                                    }
                                    else
                                    {
                                        oSidur.htPeilut.RemoveAt(j);
                                        l = 0;
                                        iCountPeiluyotIns = oCollPeilutOvdimIns.Count;
                                        if (l < iCountPeiluyotIns)
                                        {
                                            do
                                            {
                                                if ((oCollPeilutOvdimIns.Value[l].MISPAR_SIDUR == oSidur.iMisparSidur) && (oCollPeilutOvdimIns.Value[l].SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
                                                        && (oCollPeilutOvdimIns.Value[l].SHAT_YETZIA == oPeilut.dFullShatYetzia) && (oCollPeilutOvdimIns.Value[l].MISPAR_KNISA == oPeilut.iMisparKnisa))
                                                {
                                                    oCollPeilutOvdimIns.RemoveAt(l);
                                                    l -= 1;
                                                }
                                                l += 1;
                                                iCountPeiluyotIns = oCollPeilutOvdimIns.Count;
                                            } while (l < iCountPeiluyotIns);
                                        }
                                    }
                                    break;
                                }
                            }
                        }
                    // }
                    htEmployeeDetails[i] = oSidur;

                }


            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, _iMisparIshi, "E", 5, _dCardDate, "AddElementMechine05: " + ex.Message);
                _bSuccsess = false;
            }
        }

        private void AddElementMachineForFirstSidur_2(ref SidurDM oSidur, int iIndexSidur, ref DateTime dShatYetzia, ref  int iMeshechHachanotMechona, ref int iNumHachanotMechonaForSidur, ref int iIndexElement, ref int iPeilutNesiaIndex, ref bool bUsedMazanTichnun, ref bool bUsedMazanTichnunInSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            bool bPeilutNesiaMustBusNumber = false;
            long lOtoNo = 0;

            try
            {
                //אם זה הסידור הראשון וקיימת נסיעת שירות, נמ"ק , ריקה או אלמנט שדורש הכנת רכב וגם לא קיימת פעילות הכנת מכונה
                IsSidurMustHachanatMechonaFirst_2(ref oSidur, ref bPeilutNesiaMustBusNumber, ref iPeilutNesiaIndex, ref lOtoNo);

                //אם נמצאה פעילות מסוג נסיעה או אלמנט הדורש מספר רכב וגם לא נמצאה פעילות של הכנת מכונה
                //נכניס אלמנט הכנסת מכונה. נבדיל בין שני מקרים: סידור ראשון המתחיל לפני )08:00 בבוקר וסידור ראשון שמתחיל אחרי 08:00 בבוקר
                if ((bPeilutNesiaMustBusNumber))
                {
                    //אם סידור הוא ראשון ביום, מתחיל לפני 08:00 ואין לו אלמנט הכנת מכונה מכל סוג שהוא (701, 711, 712) - להוסיף לו אלמנט הכנת מכונה ראשונה (70100000).  
                    //זמן האלמנט ייקבע לפי הערך לפרמטר 120 (זמן הכנת מכונה ראשונה) בטבלת פרמטרים חיצוניים. שעת היציאה של פעילות האלמנט תחושב באופן הבא: יש לקחת את  שעת היציאה של הפעילות העוקבת לאלמנט החדש שהוספנו ולהחסיר ממנה את זמן האלמנט שהוספנו.
                    AddElementHachanatMechine701_2(ref oSidur, iIndexSidur, ref dShatYetzia, ref iPeilutNesiaIndex, ref iMeshechHachanotMechona, ref iNumHachanotMechonaForSidur, ref iIndexElement, ref bUsedMazanTichnun, ref oObjSidurimOvdimUpd, ref bUsedMazanTichnunInSidur);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      
        private void IsSidurMustHachanatMechonaFirst_2(ref SidurDM oSidur, ref bool bPeilutNesiaMustBusNumber,
                                                   ref int iPeilutNesiaIndex,
                                                   ref long lOtoNo)
        {
            PeilutDM oPeilut;

            try
            {
                for (int j = 0; j < oSidur.htPeilut.Count; j++)
                {
                    oPeilut = (PeilutDM)oSidur.htPeilut[j];
                    var peilutManager = ServiceLocator.Current.GetInstance<IPeilutManager>();
                    if (peilutManager.IsMustBusNumber(oPeilut, oParam.iVisutMustRechevWC))
                    {
                        bPeilutNesiaMustBusNumber = true;
                        iPeilutNesiaIndex = j;
                        lOtoNo = oPeilut.lOtoNo;
                        break;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddElementHachanatMechine701_2(ref SidurDM oSidur, int iIndexSidur, ref DateTime dShatYetiza, ref int iPeilutNesiaIndex, ref  int iMeshechHachanotMechona, ref int iNumHachanotMechonaForSidur, ref int iIndexElement, ref bool bUsedMazanTichnun, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, ref bool bUsedMazanTichnunInSidur)
        {
            OBJ_PEILUT_OVDIM oObjPeilutOvdimIns = new OBJ_PEILUT_OVDIM();
            PeilutDM oPeilut;
            int idakot,iMeshechElement;
            DateTime dRefferenceDate, dShatYetziaPeilut,dShatKisuyTor;
            try
            {
                oPeilut = (PeilutDM)oSidur.htPeilut[iPeilutNesiaIndex];
                InsertToObjPeilutOvdimForInsert(ref oSidur, ref oObjPeilutOvdimIns);
               // if (!CheckHaveElementHachanatMechona_2(ref oSidur, iPeilutNesiaIndex))
              //  {
                    //אם מספר הכנות המכונה (מכל סוג שהוא) שנוספו עד כה ליום העבודה גדול שווה לערך בפרמטר 123 (מכסימום יומי להכנות מכונה) או מספר הכנות המכונה בסידור גדול שווה לערך בפרמטר 124 (מכסימום הכנות מכונה בסידור אחד)- לא מעדכנים זמן לאלמנט. 
                    //if (iNumHachanotMechona < oParam.iPrepareAllMechineTotalMaxTimeInDay || iNumHachanotMechonaForSidur < oParam.iPrepareAllMechineTotalMaxTimeForSidur)
                    oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("701", oParam.iPrepareFirstMechineMaxTime.ToString().PadLeft(3, (char)48), "00"));
                    // else oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("701", "000", "00"));

                    dRefferenceDate = DateHelper.GetDateTimeFromStringHour("08:00", oPeilut.dFullShatYetzia);
                    dShatKisuyTor = oPeilut.dFullShatYetzia.AddMinutes(-oPeilut.iKisuyTor);
                    if (oPeilut.dFullShatYetzia > dRefferenceDate && dShatKisuyTor > dRefferenceDate && (!clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, _iSugYom, _dCardDate)))
                    {
                        oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("701", oParam.iPrepareOtherMechineMaxTime.ToString().PadLeft(3, (char)48), "00"));
                        //    dShatYetziaPeilut = dShatYetziaPeilut.AddMinutes(-3);
                    }
                    oObjPeilutOvdimIns.OTO_NO = oPeilut.lOtoNo;

                    PeilutDM oPeilutNew = CreatePeilut(_iMisparIshi, _dCardDate, oObjPeilutOvdimIns, dtTmpMeafyeneyElements);

                    oObjPeilutOvdimIns.BITUL_O_HOSAFA = 4;
                    oCollPeilutOvdimIns.Add(oObjPeilutOvdimIns);
                    oPeilutNew.iBitulOHosafa = 4;
                    oSidur.htPeilut.Insert(iPeilutNesiaIndex, dShatYetiza.ToString("HH:mm:ss").Replace(":", "") + iPeilutNesiaIndex + 1, oPeilutNew);
                    iIndexElement = iPeilutNesiaIndex;
                    iPeilutNesiaIndex += 1;

                    dShatYetziaPeilut = GetShatHatchalaElementMachine_2(iIndexSidur, iPeilutNesiaIndex, ref oSidur, oPeilutNew, true, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur);

                    //dRefferenceDate = DateHelper.GetDateTimeFromStringHour("08:00", dShatYetziaPeilut);
                    //if (dShatYetziaPeilut >= dRefferenceDate && (!clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, _iSugYom, _dCardDate)))
                    //{
                    //    oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("701", "005", "00"));
                    //    dShatYetziaPeilut = dShatYetziaPeilut.AddMinutes(-3);
                    //}

                    idakot = FindDuplicatPeiluyot_2(iPeilutNesiaIndex - 1, dShatYetziaPeilut, iIndexSidur, ref oSidur, ref oObjSidurimOvdimUpd);
                    if (idakot > 0)
                    {
                        iMeshechElement = int.Parse(oObjPeilutOvdimIns.MAKAT_NESIA.ToString().Substring(3,3));
                        if (idakot <= iMeshechElement)
                            oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("701", (iMeshechElement - idakot).ToString().PadLeft(3, (char)48), "00"));
                        else oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("701", "000", "00"));
                        dShatYetziaPeilut = dShatYetziaPeilut.AddMinutes(idakot);
                    }
                    oObjPeilutOvdimIns.SHAT_YETZIA = dShatYetziaPeilut;

                    oPeilutNew.dFullShatYetzia = oObjPeilutOvdimIns.SHAT_YETZIA;
                    oPeilutNew.lMakatNesia = oObjPeilutOvdimIns.MAKAT_NESIA;
                    oPeilutNew.sShatYetzia = oPeilutNew.dFullShatYetzia.ToString("HH:mm");
                    

                    if (iIndexElement == 0) dShatYetiza = oObjPeilutOvdimIns.SHAT_YETZIA;
                    iNumHachanotMechonaForSidur += 1;
                    iMeshechHachanotMechona += int.Parse(oObjPeilutOvdimIns.MAKAT_NESIA.ToString().Substring(3, 3));

              //  }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddElementMachineForNextSidur_2(ref SidurDM oSidur, ref DateTime dShatYetzia, int iSidurIndex, int iIndexFirstElementMachine, ref  int iMeshechHachanotMechona, ref int iNumHachanotMechonaForSidur,ref int iMeshechHachanotMechonaNosafot, ref int iIndexElement, ref bool bUsedMazanTichnun, ref bool bUsedMazanTichnunInSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            PeilutDM oPeilut;
            SidurDM oLocalSidur;
            int iPeilutNesiaIndex = 0;
            long lOtoNo = 0;
            int i = 0;
            int iIndexPeilut;
            bool bHavePeilutMustRechev = false;
            int l, iCountPeiluyot;
            bool bAddElementPitzul = false;
            bool bAddElementHamtana = false;
            int j = 0;
            long lMakat;
            try
            {
                //סידור אינו הראשון ביום ויש בו פעילות "דורשת מספר רכב" ואין לפניה אלמנט הכנת מכונה מכל סוג שהוא (701, 711, 712). מחפשים פעילות אחרת שהיא "דורשת מספר רכב" ששעת היציאה שלה קטנה משעת היציאה של הפעילות אותה אנו בודקים.
                //אם הפעילות אותה אנו בודקים היא הראשונה בסידור מחפשים פעילות "דורשת מספר רכב" בסידור קודם. אם בשתי הפעילויות מספר הרכב לא זהה (ערך שאינו null או 0 ובעל 5 ספרות)  אז מוסיפים אלמנט הכנת מכונה (71100000) לפני הפעילות אותה בדקנו.

                l = iIndexFirstElementMachine;
                iCountPeiluyot = oSidur.htPeilut.Count;
                if (iCountPeiluyot > 0 && l < iCountPeiluyot)
                {
                    do
                    {
                        oPeilut = (PeilutDM)oSidur.htPeilut[l];
                        var peilutManager = ServiceLocator.Current.GetInstance<IPeilutManager>();
                        if (peilutManager.IsMustBusNumber(oPeilut, oParam.iVisutMustRechevWC))
                        {
                            iPeilutNesiaIndex = l;
                            lOtoNo = oPeilut.lOtoNo;
                            bHavePeilutMustRechev = false;

                            for (i = iSidurIndex; i >= 0; i--)
                            {
                                oLocalSidur = (SidurDM)htEmployeeDetails[i];
                                if (!bHavePeilutMustRechev && !CheckHaveElementHachanatMechona_2(ref oSidur, iPeilutNesiaIndex))
                                {
                                    if (iSidurIndex == i)
                                    { iIndexPeilut = iPeilutNesiaIndex - 1; }
                                    else
                                    { iIndexPeilut = oLocalSidur.htPeilut.Count - 1; }
                                    for (j = iIndexPeilut; j >= 0; j--)
                                    {
                                        if (!bHavePeilutMustRechev)
                                        {
                                            oPeilut = (PeilutDM)oLocalSidur.htPeilut[j];
                                            if (peilutManager.IsMustBusNumber(oPeilut, oParam.iVisutMustRechevWC))
                                            {
                                                if (oPeilut.lOtoNo != lOtoNo && oPeilut.lOtoNo > 0 && lOtoNo > 0 && oPeilut.lOtoNo.ToString().Length >= 5)
                                                {
                                                    //אם אין להן אותו מספר רכב אז מוסיפים אלמנט הכנת מכונה (71100000).
                                                    AddElementHachanatMechine711_2(ref oSidur, iSidurIndex, ref dShatYetzia, ref iPeilutNesiaIndex, ref iMeshechHachanotMechona,ref iNumHachanotMechonaForSidur,ref iMeshechHachanotMechonaNosafot,ref  iIndexElement, ref bUsedMazanTichnun, ref  bUsedMazanTichnunInSidur, ref oObjSidurimOvdimUpd);
                                                    htEmployeeDetails[iSidurIndex] = oSidur;
                                                    if (i == iSidurIndex)
                                                        l += 1;
                                                }

                                                bHavePeilutMustRechev = true;
                                                break;

                                            }
                                        }
                                    }

                                    if (!CheckHaveElementHachanatMechona_2(ref oSidur, iPeilutNesiaIndex) && !bAddElementPitzul)
                                    {
                                        if (oLocalSidur != oSidur && (oSidur.dFullShatHatchala - oLocalSidur.dFullShatGmar).TotalMinutes > _oParameters.iMinTimeBetweenSidurim
                                            && ((oSidur.dFullShatHatchala - oLocalSidur.dFullShatGmar).TotalMinutes - _oParameters.iPrepareOtherMechineMaxTime) > _oParameters.iMinTimeBetweenSidurim)
                                        {
                                            AddElementHachanatMechine711_2(ref oSidur, iSidurIndex, ref dShatYetzia, ref iPeilutNesiaIndex, ref iMeshechHachanotMechona,ref  iNumHachanotMechonaForSidur, ref iMeshechHachanotMechonaNosafot, ref  iIndexElement, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur, ref oObjSidurimOvdimUpd);
                                            htEmployeeDetails[iSidurIndex] = oSidur;
                                            bAddElementPitzul = true;
                                        }
                                    }

                                    if (!CheckHaveElementHachanatMechona_2(ref oSidur, iPeilutNesiaIndex) && !bAddElementHamtana)
                                    {
                                        if (oLocalSidur.htPeilut.Count > 0)
                                        {
                                            lMakat = ((PeilutDM)oLocalSidur.htPeilut[oLocalSidur.htPeilut.Count - 1]).lMakatNesia;
                                            if (oLocalSidur != oSidur && iSidurIndex == i + 1 && (oSidur.dFullShatHatchala - oLocalSidur.dFullShatGmar).TotalMinutes <= _oParameters.iMinTimeBetweenSidurim
                                                 && (lMakat.ToString().PadLeft(8).Substring(0, 3) == "724" || lMakat.ToString().PadLeft(8).Substring(0, 3) == "735") && int.Parse(lMakat.ToString().PadLeft(8).Substring(3, 3)) > 60)
                                            {
                                                AddElementHachanatMechine711_2(ref oSidur, iSidurIndex, ref dShatYetzia, ref iPeilutNesiaIndex, ref iMeshechHachanotMechona, ref iNumHachanotMechonaForSidur, ref iMeshechHachanotMechonaNosafot, ref  iIndexElement, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur, ref oObjSidurimOvdimUpd);
                                                htEmployeeDetails[iSidurIndex] = oSidur;
                                                bAddElementHamtana = true;
                                            }
                                        }
                                    }
                                }


                            }
                        }
                        l += 1;

                        iCountPeiluyot = oSidur.htPeilut.Count;
                    } while (l < iCountPeiluyot);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CheckHaveElementHachanatMechona_2(ref SidurDM oSidur, int iIndexPeilutMustAutoNum)
        {
            bool bHave = false;

            try
            {
                if (iIndexPeilutMustAutoNum > 0)
                {
                    PeilutDM oPeilut = (PeilutDM)oSidur.htPeilut[iIndexPeilutMustAutoNum - 1];
                    if (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == enElementHachanatMechona.Element712.GetHashCode().ToString() || oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == enElementHachanatMechona.Element711.GetHashCode().ToString())
                    { bHave = true; }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bHave;
        }

        private void AddElementHachanatMechine711_2(ref SidurDM oSidur, int iIndexSidur, ref DateTime dShatYetiza, ref int iPeilutNesiaIndex,ref int iMeshechHachanotMechona,ref int iNumHachanotMechonaForSidur,ref int iMeshechHachanotMechonaNosafot, ref int iIndexElement, ref bool bUsedMazanTichnun, ref bool bUsedMazanTichnunInSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            OBJ_PEILUT_OVDIM oObjPeilutOvdimIns = new OBJ_PEILUT_OVDIM();
            PeilutDM oPeilut;
            int idakot, iMeshechElement;
            DateTime dShatYetziaPeilut;
            try
            {
                oPeilut = (PeilutDM)oSidur.htPeilut[iPeilutNesiaIndex];
                InsertToObjPeilutOvdimForInsert(ref oSidur, ref oObjPeilutOvdimIns);

                ////if (iMeshechHachanotMechona < oParam.iPrepareAllMechineTotalMaxTimeInDay && 
                ////    iNumHachanotMechonaForSidur < oParam.iPrepareAllMechineTotalMaxTimeForSidur &&
                ////    iMeshechHachanotMechonaNosafot < oParam.iPrepareOtherMechineTotalMaxTime)
                ////{
                ////    oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("711", oParam.iPrepareOtherMechineMaxTime.ToString().PadLeft(3, (char)48), "00"));
                ////}
                ////else oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("711", "000" , "00"));

                oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("711", oParam.iPrepareOtherMechineMaxTime.ToString().PadLeft(3, (char)48), "00"));
                oObjPeilutOvdimIns.OTO_NO = oPeilut.lOtoNo;

                PeilutDM oPeilutNew = CreatePeilut(_iMisparIshi, _dCardDate, oObjPeilutOvdimIns, dtTmpMeafyeneyElements);

                oObjPeilutOvdimIns.BITUL_O_HOSAFA = 4;
                oCollPeilutOvdimIns.Add(oObjPeilutOvdimIns);
                oPeilutNew.iBitulOHosafa = 4;
                oSidur.htPeilut.Insert(iPeilutNesiaIndex, dShatYetiza.ToString("HH:mm:ss").Replace(":", "") + iPeilutNesiaIndex + 11, oPeilutNew);
                iIndexElement = iPeilutNesiaIndex;
                iPeilutNesiaIndex += 1;

                dShatYetziaPeilut = GetShatHatchalaElementMachine_2(iIndexSidur, iPeilutNesiaIndex, ref oSidur, (PeilutDM)oSidur.htPeilut[iIndexElement], false, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur);
                idakot = FindDuplicatPeiluyot_2(iPeilutNesiaIndex - 1, dShatYetziaPeilut, iIndexSidur, ref oSidur, ref oObjSidurimOvdimUpd);

                if (idakot > 0)
                {
                    iMeshechElement = int.Parse(oObjPeilutOvdimIns.MAKAT_NESIA.ToString().Substring(3, 3));
                    if (idakot <= iMeshechElement)
                        oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat(oObjPeilutOvdimIns.MAKAT_NESIA.ToString().Substring(0,3), (iMeshechElement - idakot).ToString().PadLeft(3, (char)48), "00"));
                    else oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat(oObjPeilutOvdimIns.MAKAT_NESIA.ToString().Substring(0, 3), "000", "00"));
                    dShatYetziaPeilut = dShatYetziaPeilut.AddMinutes(idakot);
                }

                oObjPeilutOvdimIns.SHAT_YETZIA = dShatYetziaPeilut;
                oPeilutNew.dFullShatYetzia = oObjPeilutOvdimIns.SHAT_YETZIA;
                oPeilutNew.lMakatNesia = oObjPeilutOvdimIns.MAKAT_NESIA;
                oPeilutNew.sShatYetzia = oPeilutNew.dFullShatYetzia.ToString("HH:mm");
                if (iIndexElement == 0) dShatYetiza = oObjPeilutOvdimIns.SHAT_YETZIA;

                iNumHachanotMechonaForSidur += 1;
                iMeshechHachanotMechona += int.Parse(oObjPeilutOvdimIns.MAKAT_NESIA.ToString().Substring(3, 3));
                iMeshechHachanotMechonaNosafot += int.Parse(oObjPeilutOvdimIns.MAKAT_NESIA.ToString().Substring(3, 3));;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DateTime GetShatHatchalaElementMachine_2(int iIndexSidur, int iPeilutNesiaIndex, ref SidurDM oSidur, PeilutDM oPeilutMachine, bool bFirstElementMachine, ref bool bUsedMazanTichnun, ref bool bUsedMazanTichnunInSidur)
        {
            PeilutDM oNextPeilut, oPeilut,oPeilutRekaFirst, oFirstPeilutMashmautit;
            DateTime dShatHatchala = DateTime.MinValue;
            int i,j, iIndexPeilutMashmautit, iMeshechPeilut, iMeshechPeilutMachine;
            DateTime dRefferenceDate;
            bool bCheck = false;
            string sSugMechona;

            try
            {
                oFirstPeilutMashmautit = null;
                oPeilutRekaFirst = null;
                iIndexPeilutMashmautit = -1;
                for (i = iPeilutNesiaIndex; i <= oSidur.htPeilut.Values.Count - 1; i++)
                {
                    oNextPeilut = (PeilutDM)oSidur.htPeilut[i];

                  //  if (oNextPeilut.iMakatType == enMakatType.mVisa.GetHashCode() || oNextPeilut.iMakatType == enMakatType.mKavShirut.GetHashCode() || oNextPeilut.iMakatType == enMakatType.mNamak.GetHashCode() || (oNextPeilut.iMakatType == enMakatType.mElement.GetHashCode() && (oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != enElementHachanatMechona.Element701.GetHashCode().ToString() && oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != enElementHachanatMechona.Element712.GetHashCode().ToString() && oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != enElementHachanatMechona.Element711.GetHashCode().ToString()) && (oNextPeilut.iElementLeShatGmar > 0 || oNextPeilut.iElementLeShatGmar == -1 || oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "700")))
                    if (isPeilutMashmautit(oNextPeilut))
                    {
                        oFirstPeilutMashmautit = oNextPeilut;
                        iIndexPeilutMashmautit = i;
                        break;
                    }
                }

                sSugMechona = oPeilutMachine.lMakatNesia.ToString().PadLeft(8).Substring(0, 3);

                //קיימת פעילות משמעותית ראשונה ):
                if (oFirstPeilutMashmautit != null)
                {
                    if (iPeilutNesiaIndex == iIndexPeilutMashmautit)
                    {
                        iMeshechPeilut = GetMeshechPeilutHachnatMechona(iIndexSidur, oPeilutMachine, oSidur, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur);
                        if (sSugMechona == "711" && iMeshechPeilut == 0)
                            iMeshechPeilut = 1;
                        dShatHatchala = oFirstPeilutMashmautit.dFullShatYetzia.AddMinutes(-(iMeshechPeilut + oFirstPeilutMashmautit.iKisuyTor));
                    }
                    else
                    {

                        dRefferenceDate = DateHelper.GetDateTimeFromStringHour("08:00", oSidur.dFullShatHatchala);
                        j = iIndexPeilutMashmautit - 1;
                        oNextPeilut = (PeilutDM)oSidur.htPeilut[j];
                        while (oNextPeilut.lMakatNesia != oPeilutMachine.lMakatNesia)
                        {
                            //if (oNextPeilut.iMakatType == enMakatType.mEmpty.GetHashCode())
                            //    oPeilutRekaFirst = oNextPeilut;
                            j -= 1;
                            oNextPeilut = (PeilutDM)oSidur.htPeilut[j];                          
                        }
                        
                        oPeilutRekaFirst = (PeilutDM)oSidur.htPeilut[j + 1];
                        if (sSugMechona == "711" || (sSugMechona == "701" && 
                                                    ((oFirstPeilutMashmautit.dFullShatYetzia <= dRefferenceDate || (oFirstPeilutMashmautit.dFullShatYetzia > dRefferenceDate && clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, _iSugYom, _dCardDate)) || (oFirstPeilutMashmautit.dFullShatYetzia > dRefferenceDate && oFirstPeilutMashmautit.dFullShatYetzia.AddMinutes(-oFirstPeilutMashmautit.iKisuyTor) <= dRefferenceDate)) && 
                                                     (GetMeshechPeilutHachnatMechona(iIndexSidur, oPeilutRekaFirst, oSidur, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur) > oParam.iMaxZmanRekaAdShmone)) 
                                                 || ((oFirstPeilutMashmautit.dFullShatYetzia > dRefferenceDate && oFirstPeilutMashmautit.dFullShatYetzia.AddMinutes(-oFirstPeilutMashmautit.iKisuyTor) > dRefferenceDate && !clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, _iSugYom, _dCardDate)) &&
                                                     (GetMeshechPeilutHachnatMechona(iIndexSidur, oPeilutRekaFirst, oSidur, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur) > oParam.iMaxZmanRekaNichleletafter8))))
                        {
                            j = iIndexPeilutMashmautit - 1; ;// -1;
                            oNextPeilut = (PeilutDM)oSidur.htPeilut[j];
                            dShatHatchala = oFirstPeilutMashmautit.dFullShatYetzia.AddMinutes(-(GetMeshechPeilutHachnatMechona(iIndexSidur, oPeilutMachine, oSidur, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur) + oFirstPeilutMashmautit.iKisuyTor));
                            
                            while (oNextPeilut.lMakatNesia != oPeilutMachine.lMakatNesia) 
                            {
                                if (isElemntLoMashmauti(oNextPeilut) || oNextPeilut.iMakatType == enMakatType.mEmpty.GetHashCode())
                                {
                                    iMeshechPeilut = (GetMeshechPeilutHachnatMechona(iIndexSidur, oNextPeilut, oSidur, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur));
                                    if (sSugMechona == "711" && iMeshechPeilut == 0)
                                        iMeshechPeilut = 1;
                                    dShatHatchala = dShatHatchala.AddMinutes(-iMeshechPeilut);
                                }
                                j -= 1;
                                oNextPeilut = (PeilutDM)oSidur.htPeilut[j];
                            }
                           
                        }
                        else
                        {
                            j = iIndexPeilutMashmautit - 1;
                            oNextPeilut = (PeilutDM)oSidur.htPeilut[j];
                            dShatHatchala = oFirstPeilutMashmautit.dFullShatYetzia.AddMinutes(-(GetMeshechPeilutHachnatMechona(iIndexSidur, oPeilutMachine, oSidur, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur) + oFirstPeilutMashmautit.iKisuyTor));

                            while (oNextPeilut != oPeilutRekaFirst)
                            {
                                if (isElemntLoMashmauti(oNextPeilut) || oNextPeilut.iMakatType == enMakatType.mEmpty.GetHashCode())
                                    dShatHatchala = dShatHatchala.AddMinutes(-(GetMeshechPeilutHachnatMechona(iIndexSidur, oNextPeilut, oSidur, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur)));

                                j -= 1;
                                oNextPeilut = (PeilutDM)oSidur.htPeilut[j];
                            }
                            //if (oPeilutRekaFirst.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "709")
                            //    dShatHatchala = dShatHatchala.AddMinutes(-int.Parse(oPeilutRekaFirst.lMakatNesia.ToString().PadLeft(8).Substring(3, 3)));            
                        }
                     }
                }
                else
                {//לא קיימת פעילות משמעותית:

                    for (i = iPeilutNesiaIndex; i <= oSidur.htPeilut.Values.Count - 1; i++)
                    {
                        //: ריקה, אלמנט ללא מאפיין 37.
                        oPeilut = (PeilutDM)oSidur.htPeilut[i];
                        //if ((oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && oPeilut.iElementLeShatGmar == 0) || oPeilut.iMakatType == enMakatType.mEmpty.GetHashCode())
                        if (isElemntLoMashmauti(oPeilut) || oPeilut.iMakatType == enMakatType.mEmpty.GetHashCode())    
                        {// יש לעדכן את שעת היציאה של הכנת המכונה לשעת יציאה של הפעילות שאינה משמעותית הראשונה פחות משך הכנת המכונה.
                            iMeshechPeilut = (GetMeshechPeilutHachnatMechona(iIndexSidur, oPeilutMachine, oSidur, ref  bUsedMazanTichnun, ref bUsedMazanTichnunInSidur));
                            if (iMeshechPeilut ==0)
                                iMeshechPeilut = 1;
                            dShatHatchala = oPeilut.dFullShatYetzia.AddMinutes(-iMeshechPeilut);
                            break;
                        }
                    }

                }
                return dShatHatchala;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int FindDuplicatPeiluyot_2(int iPeilutNesiaIndex, DateTime dShatYetzia, int iSidurIndex, ref SidurDM oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            int j;
            PeilutDM oPeilut;
            SourceObj SourceObject;
            OBJ_PEILUT_OVDIM oObjPeilutUpd;
            DateTime tmpDateShatYetzia;
            int iDakot = 0,iMoneKidum=0;

            try
            {
                for (j = 0; j < oSidur.htPeilut.Count; j++)
                {
                    if (iPeilutNesiaIndex != j)
                    {
                        oPeilut = (PeilutDM)oSidur.htPeilut[j];
                        if (oPeilut.dFullShatYetzia == dShatYetzia)
                        {
                            if (!CheckPeilutObjectDelete(iSidurIndex, j))
                            {
                                tmpDateShatYetzia = dShatYetzia;
                                while (!CheckIdkunRashemet("SHAT_YETZIA", oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia) && !isPeilutMashmautit(oPeilut) && oPeilut.dFullShatYetzia == tmpDateShatYetzia)
                                {
                                    oObjPeilutUpd = GetUpdPeilutObject(iSidurIndex, oPeilut, out SourceObject, oObjSidurimOvdimUpd);
                                    if (SourceObject == SourceObj.Insert)
                                    {
                                        oObjPeilutUpd.SHAT_YETZIA = oPeilut.dFullShatYetzia.AddMinutes(1);
                                        oPeilut.dFullShatYetzia = oObjPeilutUpd.SHAT_YETZIA;
                                    }
                                    else
                                    {
                                        oObjPeilutUpd.NEW_SHAT_YETZIA = oPeilut.dFullShatYetzia.AddMinutes(1);
                                        oObjPeilutUpd.UPDATE_OBJECT = 1;
                                        UpdateIdkunRashemet(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutUpd.NEW_SHAT_YETZIA,0);
                                        UpdateApprovalErrors(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutUpd.NEW_SHAT_YETZIA);

                                        oPeilut.dFullShatYetzia = oObjPeilutUpd.NEW_SHAT_YETZIA;
                                    }

                                    oPeilut.sShatYetzia = oPeilut.dFullShatYetzia.ToString("HH:mm");
                                    oSidur.htPeilut[j] = oPeilut;
                                    iMoneKidum++;
                                    j++;
                                    oPeilut = (PeilutDM)oSidur.htPeilut[j];
                                    tmpDateShatYetzia = tmpDateShatYetzia.AddMinutes(1);
                                }
                                if (oPeilut.dFullShatYetzia == tmpDateShatYetzia)
                                {
                                    if (CheckIdkunRashemet("SHAT_YETZIA", oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia) || isPeilutMashmautit(oPeilut))
                                    {
                                        // for (int i = j - 1; i >= 0; i--)
                                        int i = j - 1;
                                        while (iMoneKidum > 0)
                                        {
                                            //if (iPeilutNesiaIndex != i)
                                            //{
                                            oPeilut = (PeilutDM)oSidur.htPeilut[i];
                                            //if (oPeilut.dFullShatYetzia.Subtract(oPeilut.dOldFullShatYetzia).TotalMinutes == 1)
                                            //{
                                            oObjPeilutUpd = GetUpdPeilutObject(iSidurIndex, oPeilut, out SourceObject, oObjSidurimOvdimUpd);
                                            if (SourceObject == SourceObj.Insert)
                                            {
                                                oObjPeilutUpd.SHAT_YETZIA = oPeilut.dFullShatYetzia.AddMinutes(-1);
                                                oPeilut.dFullShatYetzia = oObjPeilutUpd.SHAT_YETZIA;
                                            }
                                            else
                                            {
                                                oObjPeilutUpd.NEW_SHAT_YETZIA = oPeilut.dFullShatYetzia.AddMinutes(-1);
                                                oObjPeilutUpd.UPDATE_OBJECT = 1;
                                                UpdateIdkunRashemet(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutUpd.NEW_SHAT_YETZIA,1);
                                                UpdateApprovalErrors(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutUpd.NEW_SHAT_YETZIA);

                                                oPeilut.dFullShatYetzia = oObjPeilutUpd.NEW_SHAT_YETZIA;
                                            }
                                            oPeilut.sShatYetzia = oPeilut.dFullShatYetzia.ToString("HH:mm");
                                            oSidur.htPeilut[i] = oPeilut;
                                            iMoneKidum--;
                                            i--;
                                            //}
                                            //}
                                        }
                                        iDakot += 1;
                                        dShatYetzia = dShatYetzia.AddMinutes(1);
                                    }
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }

                int iMisparSidur = oSidur.iMisparSidur;
                DateTime dShatHatchalaSidur = oSidur.dFullShatHatchala;
                return iDakot;
            }
            catch (Exception ex)
            { 
                throw ex; 
            }
        }
        //private int FindDuplicatPeiluyot_2_1(int iPeilutNesiaIndex, DateTime dShatYetzia, int iSidurIndex, ref SidurDM oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        //{
        //    int j;
        //    PeilutDM oPeilut;
        //    SourceObj SourceObject;
        //    OBJ_PEILUT_OVDIM oObjPeilutUpd;
        //    int iDakot = 0;
        //    try
        //    {

        //        for (j = 0; j < oSidur.htPeilut.Count; j++)
        //        {
        //            if (iPeilutNesiaIndex != j)
        //            {
        //                oPeilut = (PeilutDM)oSidur.htPeilut[j];
        //                if (oPeilut.dFullShatYetzia == dShatYetzia)
        //                {
        //                    if (!CheckPeilutObjectDelete(iSidurIndex, j))
        //                    {
        //                        if (!CheckIdkunRashemet("SHAT_YETZIA", oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia) && !isPeilutMashmautit(oPeilut))
        //                        {
        //                            oObjPeilutUpd = GetUpdPeilutObject(iSidurIndex, oPeilut, out SourceObject, oObjSidurimOvdimUpd);
        //                            if (SourceObject == SourceObj.Insert)
        //                            {
        //                                oObjPeilutUpd.SHAT_YETZIA = oPeilut.dFullShatYetzia.AddMinutes(1);
        //                                oPeilut.dFullShatYetzia = oObjPeilutUpd.SHAT_YETZIA;
        //                            }
        //                            else
        //                            {
        //                                oObjPeilutUpd.NEW_SHAT_YETZIA = oPeilut.dFullShatYetzia.AddMinutes(1);
        //                                oObjPeilutUpd.UPDATE_OBJECT = 1;
        //                                UpdateIdkunRashemet(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutUpd.NEW_SHAT_YETZIA,0);
        //                                UpdateApprovalErrors(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutUpd.NEW_SHAT_YETZIA);

        //                                oPeilut.dFullShatYetzia = oObjPeilutUpd.NEW_SHAT_YETZIA;
        //                            }

        //                            oPeilut.sShatYetzia = oPeilut.dFullShatYetzia.ToString("HH:mm");
        //                            oSidur.htPeilut[j] = oPeilut;
        //                            FindDuplicatPeiluyot_2(j, oPeilut.dFullShatYetzia, iSidurIndex, ref oSidur, ref oObjSidurimOvdimUpd);
        //                        }
        //                        else// if (!CheckPeilutObjectDelete(iSidurIndex, j) && !isPeilutMashmautit(oPeilut) && (CheckIdkunRashemet("SHAT_YETZIA", oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia)))
        //                        {
        //                            for (int i = j - 1; i >= 0; i--)
        //                            {
        //                                if (iPeilutNesiaIndex != i)
        //                                {
        //                                    oPeilut = (PeilutDM)oSidur.htPeilut[i];
        //                                    //if (oPeilut.dFullShatYetzia.Subtract(oPeilut.dOldFullShatYetzia).TotalMinutes == 1)
        //                                    //{
        //                                    oObjPeilutUpd = GetUpdPeilutObject(iSidurIndex, oPeilut, out SourceObject, oObjSidurimOvdimUpd);
        //                                    if (SourceObject == SourceObj.Insert)
        //                                    {
        //                                        oObjPeilutUpd.SHAT_YETZIA = oPeilut.dFullShatYetzia.AddMinutes(-1);
        //                                        oPeilut.dFullShatYetzia = oObjPeilutUpd.SHAT_YETZIA;
        //                                    }
        //                                    else
        //                                    {
        //                                        oObjPeilutUpd.NEW_SHAT_YETZIA = oPeilut.dFullShatYetzia.AddMinutes(-1);
        //                                        oObjPeilutUpd.UPDATE_OBJECT = 1;
        //                                        UpdateIdkunRashemet(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutUpd.NEW_SHAT_YETZIA,1);
        //                                        UpdateApprovalErrors(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutUpd.NEW_SHAT_YETZIA);

        //                                        oPeilut.dFullShatYetzia = oObjPeilutUpd.NEW_SHAT_YETZIA;
        //                                    }
        //                                    oPeilut.sShatYetzia = oPeilut.dFullShatYetzia.ToString("HH:mm");
        //                                    oSidur.htPeilut[j] = oPeilut;
        //                                    //}
        //                                }
        //                            }
        //                            iDakot += 1;
        //                            dShatYetzia = dShatYetzia.AddMinutes(1);   
        //                            FindDuplicatPeiluyot_2(iPeilutNesiaIndex, dShatYetzia, iSidurIndex, ref oSidur, ref oObjSidurimOvdimUpd);
        //                        }
        //                    }
        //                }
        //            }
        //        }
            
        //        int iMisparSidur = oSidur.iMisparSidur;
        //        DateTime dShatHatchalaSidur = oSidur.dFullShatHatchala;
        //        return iDakot;
        //        //oSidurWithCancled = _htEmployeeDetailsWithCancled.Values
        //        //                     .Cast<SidurDM>()
        //        //                     .ToList()
        //        //                    .Find(sidur => (sidur.iMisparSidur == iMisparSidur && sidur.dFullShatHatchala == dShatHatchalaSidur));
        //        //if (oSidurWithCancled != null)
        //        //{
        //        //    for (j = 0; j < oSidurWithCancled.htPeilut.Count; j++)
        //        //    {
        //        //        oPeilut = (PeilutDM)oSidurWithCancled.htPeilut[j];
        //        //        if (oPeilut.iBitulOHosafa == 1 && oPeilut.dFullShatYetzia == dShatYetzia)
        //        //        {
        //        //            oObjPeilutUpd = GetUpdPeilutObjectCancel(iSidurIndex, oPeilut, oObjSidurimOvdimUpd);
        //        //            oObjPeilutUpd.NEW_SHAT_YETZIA = oPeilut.dFullShatYetzia.AddMinutes(1);
        //        //            oObjPeilutUpd.UPDATE_OBJECT = 1;
        //        //            UpdateIdkunRashemet(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutUpd.NEW_SHAT_YETZIA);
        //        //            UpdateApprovalErrors(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutUpd.NEW_SHAT_YETZIA);

        //        //            oPeilut.dFullShatYetzia = oObjPeilutUpd.NEW_SHAT_YETZIA;
        //        //            oPeilut.sShatYetzia = oPeilut.dFullShatYetzia.ToString("HH:mm");
        //        //            oSidurWithCancled.htPeilut[j] = oPeilut;
        //        //        }
        //        //    }
        //        //}
        //    }
        //    catch (Exception ex)
        //    { throw ex; }
        //}

       

        private OBJ_SIDURIM_OVDIM GetSidurOvdimObject(int iMisparSidur, DateTime dShatHatchala)
        {
            //מביא את הסידור לפי מפתח האינדקס
            OBJ_SIDURIM_OVDIM oObjSidurimOvdim = new OBJ_SIDURIM_OVDIM();
          try{
            for (int i = 0; i <= oCollSidurimOvdimUpd.Count - 1; i++)
            {
                if ((oCollSidurimOvdimUpd.Value[i].NEW_MISPAR_SIDUR == iMisparSidur) && (oCollSidurimOvdimUpd.Value[i].NEW_SHAT_HATCHALA == dShatHatchala) && oCollSidurimOvdimUpd.Value[i].UPDATE_OBJECT==1)
               {
                    oObjSidurimOvdim = oCollSidurimOvdimUpd.Value[i];
                }
            }

            if (oObjSidurimOvdim.MISPAR_SIDUR == 0)
            {
                for (int i = 0; i <= oCollSidurimOvdimIns.Count - 1; i++)
                {
                    if ((oCollSidurimOvdimIns.Value[i].NEW_MISPAR_SIDUR == iMisparSidur) && (oCollSidurimOvdimIns.Value[i].NEW_SHAT_HATCHALA == dShatHatchala))
                    {
                        oObjSidurimOvdim = oCollSidurimOvdimIns.Value[i];
                    }
                }
            }
          }
          catch (Exception ex)
          {
              throw ex;
          }
            return oObjSidurimOvdim;
        }

     

        private bool CheckPeilutObjectDelete(int iSidurIndex, int iPeilutIndex)
        {
            OBJ_PEILUT_OVDIM oObjPeilutOvdim = new OBJ_PEILUT_OVDIM();
            PeilutDM oPeilut;
            SidurDM oSidur;
            bool bExist = false;
            try{
            //נמצא את הפעילות באובייקט פעילויות לעדכון
            oSidur = (SidurDM)htEmployeeDetails[iSidurIndex];
            oPeilut = (PeilutDM)oSidur.htPeilut[iPeilutIndex];
            for (int i = 0; i <= oCollPeilutOvdimDel.Count - 1; i++)
            {
                if ((oCollPeilutOvdimDel.Value[i].NEW_MISPAR_SIDUR == oSidur.iMisparSidur) && (oCollPeilutOvdimDel.Value[i].NEW_SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
                    && (oCollPeilutOvdimDel.Value[i].NEW_SHAT_YETZIA == oPeilut.dFullShatYetzia) && (oCollPeilutOvdimDel.Value[i].MISPAR_KNISA == oPeilut.iMisparKnisa))
                {
                    bExist = true;
                    break;
                }
            }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bExist;
        }

        private void SetZmanHashlama(ref OBJ_SIDURIM_OVDIM oObjSidurGriraUpd, SidurDM oSidurKonenutGrira, int iGriraNum, SidurDM oSidurGrira)
        {
            int iZmanHashlama = 0;
            int iMerchav = 0;
            float fSidurTime = 0;
            try
            {
                //אם זוהי גרירה ראשונה בפועל (זיהוי הסידור לפי הלוגיקה בסעיף סימון כוננות גרירה לא לתשלום) בתוך זמן סידור כוננות גרירה (זיהוי הסידור לפי הלוגיקה בסעיף סימון כוננות גרירה לא לתשלום) ואם סידור הכוננות הוא "מרחב צפון" (קוד הסניף שהוא 2 הספרות הראשונות של מספר הסידור קטן מ-25) וזמן הסידור (גמר - התחלה) פחות מהזמן המוגדר בפרמטר 164 (זמן גרירה מינימלי באזור צפון) אזי יש לסמן "2" בשדה "קוד השלמה". 

                if (!CheckIdkunRashemet("HASHLAMA", oSidurGrira.iMisparSidur, oSidurGrira.dFullShatHatchala))
                {
                    iMerchav = int.Parse((oSidurKonenutGrira.iMisparSidur).ToString().Substring(0, 2));

                    fSidurTime = float.Parse((oSidurGrira.dFullShatGmar - oSidurGrira.dFullShatHatchala).TotalMinutes.ToString());

                    if (iGriraNum==1) //גרירה ראשונה
                    {
                        if ((iMerchav < clGeneral.enMerchav.Tzafon.GetHashCode()) && (fSidurTime < oParam.iMinZmanGriraTzafon))
                        {
                            iZmanHashlama = 1;
                        }
                        else
                        {
                            //איזור דרום/ירושלים
                            if (((iMerchav >= clGeneral.enMerchav.Tzafon.GetHashCode()) && (iMerchav < clGeneral.enMerchav.Darom.GetHashCode())) && (fSidurTime < oParam.iMinZmanGriraDarom))
                            {
                                iZmanHashlama = 2;
                            }
                            else
                            {
                                if ((iMerchav >= clGeneral.enMerchav.Darom.GetHashCode()) && ((fSidurTime < oParam.iMinZmanGriraJrusalem)))
                                {
                                    iZmanHashlama = 2;
                                }
                            }
                        }
                        if (iZmanHashlama > 0)
                        {

                            oObjSidurGriraUpd.SUG_HASHLAMA = 1;
                            oObjSidurGriraUpd.HASHLAMA = iZmanHashlama;
                            oObjSidurGriraUpd.UPDATE_OBJECT = 1;

                        }
                    }
                    else //גרירה שניה ומעלה
                    {
                        //אם זוהי אינה גרירה ראשונה בפועל בתוך זמן הכוננות
                        //וגם זמן הסידור (גמר - התחלה) פחות מהזמן המוגדר בפרמטר 181 (זמן גרירה נוספת מינימלי בזמן כוננות גרירה) אזי יש לסמן "1" בשדה "קוד השלמה". 

                        if (fSidurTime < oParam.iGriraAddMinTime)
                        {

                            oObjSidurGriraUpd.SUG_HASHLAMA = 1;
                            oObjSidurGriraUpd.HASHLAMA = 1;
                            oObjSidurGriraUpd.UPDATE_OBJECT = 1;

                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetLoLetashlum(SidurDM oSidurKonenutGrira)
        {
            //נמצא את סידור כוננות הגרירה ונסמן לא לתשלום
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
             try{
                 oObjSidurimOvdimUpd = GetSidurOvdimObject(oSidurKonenutGrira.iMisparSidur, oSidurKonenutGrira.dFullShatHatchala);
                         
            if (oObjSidurimOvdimUpd != null)
            {
                oObjSidurimOvdimUpd.LO_LETASHLUM = 1;
                oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM = 7;
                oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                // oCollSidurimOvdimUpd.Add(oObjSidurimOvdimUpd);
            }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetShatGmarGrira(ref OBJ_SIDURIM_OVDIM oObjSidurGriraUpd, SidurDM oSidurKonenutGrira,  SidurDM oSidurGrira)
        {
            int iMerchav;
            float fTime;
            try
            {

                iMerchav = int.Parse((oSidurKonenutGrira.iMisparSidur).ToString().Substring(0, 2));

                fTime = float.Parse((oSidurGrira.dFullShatGmar-oSidurKonenutGrira.dFullShatHatchala).TotalMinutes.ToString());

                if (iMerchav < clGeneral.enMerchav.Tzafon.GetHashCode() && fTime < 60)
                {
                    oObjSidurGriraUpd.SHAT_GMAR = oSidurKonenutGrira.dFullShatHatchala.AddMinutes(60);
                    oObjSidurGriraUpd.UPDATE_OBJECT = 1;
                    
                }
                else if (iMerchav > clGeneral.enMerchav.Tzafon.GetHashCode() && fTime < 120)
                {
                    oObjSidurGriraUpd.SHAT_GMAR = oSidurKonenutGrira.dFullShatHatchala.AddMinutes(120);
                    oObjSidurGriraUpd.UPDATE_OBJECT = 1;
                 }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetBitulZmanNesiotObject(ref OBJ_SIDURIM_OVDIM oObjSidurGriraUpd,ref SidurDM oSidurKonenutGrira, int iZmanNesia)
        {
            try
            {
                oObjSidurGriraUpd.MEZAKE_NESIOT = iZmanNesia;
               oObjSidurGriraUpd.UPDATE_OBJECT = 1;
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetBitulZmanNesiot(ref OBJ_SIDURIM_OVDIM oObjSidurGriraUpd, SidurDM oSidurKonenutGrira,  SidurDM oSidurGrira)
        {
            DateTime dKonenutShatHatchala, dKonenutShatGmar;
           int iZmanNesia = 0;
            try
            {
                dKonenutShatHatchala = oSidurKonenutGrira.dFullShatHatchala;
                dKonenutShatGmar = oSidurKonenutGrira.dFullShatGmar;

                if (!String.IsNullOrEmpty(oSidurGrira.sMikumShaonKnisa) && dKonenutShatHatchala != DateTime.MinValue)
                {
                    if (int.Parse(oSidurGrira.sMikumShaonKnisa) > 0)
                    {
                        iZmanNesia = 1;
                    }
                }
                
                
                //אם כוננות גרירה בפועל מוכלת לגמרי בכוננות גרירה
                if (dKonenutShatHatchala != DateTime.MinValue && dKonenutShatGmar != DateTime.MinValue)
                {
                    if (oSidurGrira.dFullShatHatchala > dKonenutShatHatchala)
                    {
                        if ((!String.IsNullOrEmpty(oSidurGrira.sMikumShaonKnisa)) && (int.Parse(oSidurGrira.sMikumShaonKnisa) > 0) && (!String.IsNullOrEmpty(oSidurGrira.sMikumShaonYetzia)) && (int.Parse(oSidurGrira.sMikumShaonYetzia) > 0))
                        {
                            if (iZmanNesia == 1)
                                iZmanNesia = 3;
                            else
                                iZmanNesia = 2;
                        }
                    }
                }

                 if (iZmanNesia > 0)
                   SetBitulZmanNesiotObject(ref oObjSidurGriraUpd, ref oSidurKonenutGrira, iZmanNesia);
                }
           

            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsKonnutGrira(ref SidurDM oSidur, DateTime dCardDate)
        {
            DataRow[] drSugSidur;
            bool bSidurGrira = false;
            try
            {
                //נבדוק אם סידור הוא מסוג כוננות גרירה
                if (!oSidur.bSidurMyuhad)
                {//סידור רגיל
                    drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, dCardDate, _dtSugSidur);
                    if (drSugSidur.Length > 0)
                    {
                        bSidurGrira = (drSugSidur[0]["sug_Avoda"].ToString() == enSugAvoda.Grira.GetHashCode().ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bSidurGrira;
        }

        private bool IsSugAvodaKupai(ref SidurDM oSidur, DateTime dCardDate)
        {
            DataRow[] drSugSidur;
            bool bSidurKupai = false;
            try{
            //נבדוק אם סידור הוא מסוג קופאי
            if (!oSidur.bSidurMyuhad)
            {//סידור מיוחד
                bSidurKupai = (oSidur.sSugAvoda == enSugAvoda.Kupai.GetHashCode().ToString());
            }
            else
            {//סידור רגיל
                drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, dCardDate, _dtSugSidur);
                if (drSugSidur.Length > 0)
                {
                    bSidurKupai = (drSugSidur[0]["sug_Avoda"].ToString() == enSugAvoda.Kupai.GetHashCode().ToString());
                }
            }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bSidurKupai;
        }

        private bool IsActualKonnutGrira(ref SidurDM oSidur, int iSidurKonnutGrira, out int iTypeGrira)
        {
            SidurDM oSidurKonenutGrira;
            bool bSidurActualGrira = false;
            DateTime dKonenutShatHatchala, dKonenutShatGmar, dGriraShatHatchala, dGriraShatGmar;
            try
            {
                iTypeGrira = 0;
                if (oSidur.bSidurMyuhad)
                {//סידור מיוחד
                    if (oSidur.sSugAvoda == enSugAvoda.ActualGrira.GetHashCode().ToString())
                    {
                        oSidurKonenutGrira = (SidurDM)htEmployeeDetails[iSidurKonnutGrira];
                        dKonenutShatHatchala = oSidurKonenutGrira.dFullShatHatchala;
                        dKonenutShatGmar = oSidurKonenutGrira.dFullShatGmar;
                        dGriraShatHatchala = oSidur.dFullShatHatchala;
                        dGriraShatGmar = oSidur.dFullShatGmar;

                        //גרירה בפועל מוכל בתוך כוננות גרירה: 
                        //ש.התחלה גרירה בפועל >=  ש.התחלה כוננות גרירה וגם  
                        //ש. גמר גרירה בפועל <=  ש.גמר כוננות גרירה.
                        if ((dGriraShatHatchala >= dKonenutShatHatchala) && (dGriraShatGmar <= dKonenutShatGmar))
                        {
                            bSidurActualGrira = true;
                            iTypeGrira = 1;
                        }
                        // גרירה בפועל מתחילה לפני כוננות הגרירה ומסתיימת לפני סיום הכוננות    
                        //ש.התחלה גרירה בפועל <  ש.התחלה כוננות גרירה וגם  
                        //ש.גמר גרירה בפועל > ש.התחלה כוננות גרירה וגם  
                        //ש.גמר גרירה בפועל <=  ש.גמר כוננות גרירה.
                         else if ((dGriraShatHatchala < dKonenutShatHatchala) && (dGriraShatGmar > dKonenutShatHatchala) && (dGriraShatGmar <= dKonenutShatGmar))
                        {
                            bSidurActualGrira = true;
                            iTypeGrira = 2;
                        }
                        // . גרירה בפועל מתחילה אחרי כוננות הגרירה ומסתיימת אחרי סיום הכוננות  
                        //ש.התחלה גרירה בפועל >=  ש.התחלה כוננות גרירה וגם  
                        //ש.גמר גרירה בפועל >  ש.גמר כוננות גרירה. 
                        else if ((dGriraShatHatchala >= dKonenutShatHatchala) && (dGriraShatGmar > dKonenutShatGmar))
                        {
                            bSidurActualGrira = true;
                            iTypeGrira = 3;
                        }
                        //גרירה בפועל מתחילה לפני כוננות הגרירה ומסתיימת  אחרי הכוננות
                        //ש.התחלה גרירה בפועל < ש.התחלה כוננות גרירה וגם  
                        //ש.גמר סידור גרירה בפועל >  שעת גמר כוננות גרירה

                        else if ((dGriraShatHatchala < dKonenutShatHatchala) && (dGriraShatGmar > dKonenutShatGmar))
                         {
                            bSidurActualGrira = true;
                            iTypeGrira = 4;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bSidurActualGrira;
        }

        private void FixedMisparSidur01(ref SidurDM oSidur, int iSidurIndex)
        {
            //const int SIDUR_NESIA  = 99300;
            //const int SIDUR_MATALA = 99301;
            int iMisparSidur = 0;
          int iCount = 0;
            PeilutDM oPeilut;
            bool bHaveNesiatNamak=false;
            enMakatType oMakatType;
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
            SourceObj SourceObject;
            int iNewMisparMatala, iNewMisparSidur;
            bool bUpdateMisparMatala = false;
            try
            {   //בדיקה מספר 1  - תיקון מספר סידור 
                //הסדרן בכל סניף יכול להוסיף לנהג "מטלות" אשר מסומנות בספרור 00001 - 00999. זה אינו מספר סידור חוקי ולכן הופכים אותו לחוקי עפ"י קוד הפעילות אשר הסדרן רשם למטלה זו. יהיה צורך לשמור בשדה נפרד את מספר המטלה המקורי
                bHaveNesiatNamak = oSidur.htPeilut.Values.Cast<PeilutDM>().ToList().Any(Peilut => Peilut.iMakatType == enMakatType.mNamak.GetHashCode());

                if ((oSidur.iMisparSidur >= 1 && oSidur.iMisparSidur <= 999) || (oSidur.bSectorVisaExists && bHaveNesiatNamak))
                {
                    if (oSidur.iMisparSidur != 99300)
                    {
                        for (int j = 0; j < ((SidurDM)(htEmployeeDetails[iSidurIndex])).htPeilut.Count; j++)
                        {
                            //תחת מטלה יכולה להיות פעילות אחת בלבד.
                            if (iCount > 0) break;
                            oPeilut = (PeilutDM)oSidur.htPeilut[j];

                            iCount++;

                            oMakatType = (enMakatType)oPeilut.iMakatType;
                            switch (oMakatType)
                            {
                                case enMakatType.mKavShirut:
                                    //פעילות מסוג נסיעת שירות
                                    // אם הפעילות תחת המטלה  היא נסיעת שירות (לפי רוטינת זיהוי מקט), יש להכניס למספר סידור 99300
                                    iMisparSidur = SIDUR_NESIA;
                                    break;
                                case enMakatType.mEmpty:
                                    //פעילות מסוג ריקה
                                    //אם הפעילות תחת המטלה היא ריקה (לפי רוטינת זיהוי מקט), יש לסמן את הסידור 99300 
                                    iMisparSidur = SIDUR_NESIA;
                                    break;
                                case enMakatType.mNamak:
                                    //פעילות מסוג נמ"ק
                                    //אם הפעילות תחת המטלה היא נמ"ק (לפי רוטינת זיהוי מקט), יש לסמן את הסידור 99300 
                                    iMisparSidur = SIDUR_NESIA;
                                    break;
                                case enMakatType.mElement:
                                    if (oPeilut.bMisparSidurMatalotTnuaExists)
                                    {
                                        //קיים מאפיין 28
                                        //נקח את מספר הסידור ממאפיין 28
                                        iMisparSidur = oPeilut.iMisparSidurMatalotTnua;
                                    }
                                    else
                                    {
                                        //אם לא קיים מאפיין 28
                                        iMisparSidur = SIDUR_MATALA;
                                    }
                                    break;
                            }

                            if (iMisparSidur > 0)
                            {
                                oSidur.bSidurMyuhad = true;
                                clNewSidurim oNewSidurim = FindSidurOnHtNewSidurim(oSidur.iMisparSidur, oSidur.dFullShatHatchala);

                                iNewMisparSidur = iMisparSidur;
                                iNewMisparMatala = oSidur.iMisparSidur;
                                bUpdateMisparMatala = true;

                                oNewSidurim.SidurIndex = iSidurIndex;
                                oNewSidurim.SidurNew = iMisparSidur;
                                oNewSidurim.ShatHatchalaNew = oSidur.dFullShatHatchala;

                                //שינוי מקום בעקבות באג 17/05
                                oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur);
                                
                                UpdateObjectUpdSidurim(oNewSidurim);
                                DataRow[] drSidurMeyuchad;
                                drSidurMeyuchad = _dtSidurimMeyuchadim.Select("mispar_sidur=" + iNewMisparSidur);
                                var sidurManager = ServiceLocator.Current.GetInstance<ISidurManager>();
                                oSidur = sidurManager.CreateClsSidurFromSidurMeyuchad(oSidur, _dCardDate, iNewMisparSidur, drSidurMeyuchad[0]);
                                oObjSidurimOvdimUpd.NEW_MISPAR_SIDUR = iNewMisparSidur;
                                oObjSidurimOvdimUpd.HASHLAMA = 0;
                                oObjSidurimOvdimUpd.OUT_MICHSA = 0;
                                oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                                oSidur.sHashlama = "0";
                                oSidur.sOutMichsa = "0";


                                for (j = 0; j < ((SidurDM)(htEmployeeDetails[iSidurIndex])).htPeilut.Count; j++)
                                {
                                    //עבור שינוי 1, במידה והיה צורך לעדכן את מספר המטלה במספר הסידור הישן )מספר הסידור קיבל מספר חדש(
                                    //נעדכן את הפעילות הראשונה . במקרה כזה לא אמורה להיות יותר מפעילות אחת לסידור
                                    //נעדכן גם את הפעילויות במספר הסידור החדש
                                    oPeilut = (PeilutDM)((SidurDM)(htEmployeeDetails[iSidurIndex])).htPeilut[j];
                                    oObjPeilutOvdimUpd = GetUpdPeilutObject(iSidurIndex, oPeilut, out SourceObject, oObjSidurimOvdimUpd);
                                    oObjPeilutOvdimUpd.MISPAR_MATALA = iNewMisparMatala;
                                    if (SourceObject == SourceObj.Insert)
                                    {
                                        oObjPeilutOvdimUpd.MISPAR_SIDUR = iNewMisparSidur;
                                    }
                                    else
                                    {
                                        oObjPeilutOvdimUpd.NEW_MISPAR_SIDUR = iNewMisparSidur;
                                        oObjPeilutOvdimUpd.UPDATE_OBJECT = 1;
                                    }
                                    oPeilut.iPeilutMisparSidur = iNewMisparSidur;
                                    oPeilut.lMisparMatala = iNewMisparMatala;
                                }
                                //UpdatePeiluyotMevutalotYadani(iSidurIndex, oNewSidurim, oObjSidurimOvdimUpd);
                                
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 1, oSidur.iMisparIshi,oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "FixedMisparSidur01: " + ex.Message, null);
                _bSuccsess = false;
            }
        }

        private void FixedSidurHours08()
        {
            //תיקון שעות בחפיפה בין סידורים
            SidurDM oSidurPrev;
            DateTime datePrevShatGmar;
            DateTime dateCurrShatHatchala;
            SidurDM oSidur;
            OBJ_SIDURIM_OVDIM oPrevObjSidurimOvdimUpd;
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
            OBJ_PEILUT_OVDIM oObjPeilutUpd;
            PeilutDM oPeilut;
            SourceObj SourceObject;
            int i;
            try
            {
                for (i = htEmployeeDetails.Count - 1; i > 0; i--)
                {
                    oSidur = (SidurDM)htEmployeeDetails[i];
                    //נקרא את נתוני הסידור הקודם
                    oSidurPrev = (SidurDM)htEmployeeDetails[i - 1];

                    if (oSidur.iLoLetashlum == 0 && oSidurPrev.iLoLetashlum == 0)
                    {
                        oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur);
                        oPrevObjSidurimOvdimUpd = GetSidurOvdimObject(oSidurPrev.iMisparSidur, oSidurPrev.dFullShatHatchala);

                        if ((oPrevObjSidurimOvdimUpd.SHAT_GMAR != DateTime.MinValue) && (oObjSidurimOvdimUpd.SHAT_HATCHALA.Year > DateHelper.cYearNull))
                        {
                            datePrevShatGmar = oPrevObjSidurimOvdimUpd.SHAT_GMAR;
                            dateCurrShatHatchala = oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA;
                            if (dateCurrShatHatchala < datePrevShatGmar)
                            {//קיימת חפיפה בין הסידורים 
                                if (!CheckIdkunRashemet("SHAT_HATCHALA", oSidur.iMisparSidur, oSidur.dFullShatHatchala))
                                {
                                    if (oSidur.htPeilut.Values.Count > 0)
                                    {
                                        oPeilut = (PeilutDM)oSidur.htPeilut[0];
                                        if (oPeilut.dFullShatYetzia.AddMinutes(-oPeilut.iKisuyTor) > oSidur.dFullShatHatchala)
                                        {
                                            clNewSidurim oNewSidurim = FindSidurOnHtNewSidurim(oSidur.iMisparSidur, oSidur.dFullShatHatchala);

                                            oNewSidurim.SidurIndex = i;
                                            oNewSidurim.SidurNew = oSidur.iMisparSidur;
                                            oNewSidurim.ShatHatchalaNew = oSidur.dFullShatHatchala.AddMinutes(Math.Min(Math.Abs((datePrevShatGmar - dateCurrShatHatchala).TotalMinutes), (oPeilut.dFullShatYetzia.AddMinutes(-oPeilut.iKisuyTor) - oSidur.dFullShatHatchala).TotalMinutes));

                                            UpdateObjectUpdSidurim(oNewSidurim);
                                            for (int j = 0; j < oSidur.htPeilut.Count; j++)
                                            {
                                                oPeilut = (PeilutDM)oSidur.htPeilut[j];

                                                if (!CheckPeilutObjectDelete(i, j))
                                                {
                                                    oObjPeilutUpd = GetUpdPeilutObject(i, oPeilut, out SourceObject, oObjSidurimOvdimUpd);
                                                    if (SourceObject == SourceObj.Insert)
                                                    {
                                                        oObjPeilutUpd.SHAT_HATCHALA_SIDUR = oNewSidurim.ShatHatchalaNew;
                                                    }
                                                    else
                                                    {
                                                        oObjPeilutUpd.NEW_SHAT_HATCHALA_SIDUR = oNewSidurim.ShatHatchalaNew;
                                                        oObjPeilutUpd.UPDATE_OBJECT = 1;
                                                    }
                                                }

                                            }
                                            //UpdatePeiluyotMevutalotYadani(i,oNewSidurim, oObjSidurimOvdimUpd);
                                            UpdateIdkunRashemet(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oNewSidurim.ShatHatchalaNew);
                                            UpdateApprovalErrors(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oNewSidurim.ShatHatchalaNew);

                                            oSidur.dFullShatHatchala = oNewSidurim.ShatHatchalaNew;
                                            oSidur.sShatHatchala = oSidur.dFullShatHatchala.ToString("HH:mm");
                                            htEmployeeDetails[i] = oSidur;
                                            oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA = oNewSidurim.ShatHatchalaNew;

                                            dateCurrShatHatchala = oNewSidurim.ShatHatchalaNew;

                                        }
                                    }
                                }
                            }

                            if (dateCurrShatHatchala < datePrevShatGmar)
                            {
                                if (!CheckIdkunRashemet("SHAT_GMAR", oSidurPrev.iMisparSidur, oSidurPrev.dFullShatHatchala))
                                {
                                    if (oSidurPrev.htPeilut.Values.Count > 0)
                                    {
                                        oPeilut = (PeilutDM)oSidurPrev.htPeilut[oSidurPrev.htPeilut.Values.Count - 1];

                                        oPrevObjSidurimOvdimUpd.SHAT_GMAR = oSidurPrev.dFullShatGmar.AddMinutes(-(Math.Min(Math.Abs((datePrevShatGmar - dateCurrShatHatchala).TotalMinutes), Math.Abs((oSidurPrev.dFullShatGmar - oPeilut.dFullShatYetzia).TotalMinutes))));
                                        oPrevObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                                        oSidurPrev.dFullShatGmar = oPrevObjSidurimOvdimUpd.SHAT_GMAR;
                                        oSidurPrev.sShatGmar = oSidurPrev.dFullShatGmar.ToString("HH:mm");
                                        htEmployeeDetails[i - 1] = oSidurPrev;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
               clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 8,_iMisparIshi, _dCardDate,null , null, null, null, "FixedSidurHours08: " + ex.Message, null);
                _bSuccsess = false;
            }
        }

        private void SidurNetzer17(ref SidurDM oSidur,ref OBJ_SIDURIM_OVDIM  oObjSidurimOvdimUpd)
        {
            //אם סידור הוא סידור נצר (99203) (לפי ערך 11 (נ.צ.ר) במאפיין 52 (סוג עבודה) בטבלת סידורים מיוחדים)  ולעובד קיים מאפיין 64 והסידור אינו מסומן "לא לתשלום" 
            //- שותלים "חריגה" 3 (התחלה + גמר) ו"מחוץ למיכסה".

            try
            {   //אם סידור נצר
                if ((oSidur.sSugAvoda == enSugAvoda.Netzer.GetHashCode().ToString()) && (oMeafyeneyOved.IsMeafyenExist(64)) && (oSidur.iLoLetashlum == 0))
                {

                    oObjSidurimOvdimUpd.CHARIGA = 3;
                    oObjSidurimOvdimUpd.OUT_MICHSA = 1;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                    // oCollSidurimOvdimUpd.Add(oObjSidurimOvdimUpd);

                    // clDefinitions.UpdateSidurimOvdim(oCollSidurimOvdimUpd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void NewSidurHeadrutWithPaymeny15(int iMisparIshi, DateTime dCardDate, int iSugYom, bool bLoLetashlum)
        {
            DataTable dt;
            //לעובדים להם יש מאפיין אישי 63 (משפחה שכולה) , אם סוג יום = 17 (יום הזכרון) ואין להם סידור עבודה אחר באות יום (שאינו מסומן לא לתשלום) , יש לפתוח להם סידור 99801 (העדרות בתשלום יום עבודה) עם שעות מ-0400 – 2800 (כדי שדיווח אחר יצא לשגוי בחפיפה אם ידווח).            
            try
            {

                if ((oMeafyeneyOved.IsMeafyenExist(63)) && (iSugYom == enSugYom.ErevYomHatsmaut.GetHashCode()) && (!bLoLetashlum))
                {
                    if (!IsSidurExits(99801))
                    {
                        var sidurManager = ServiceLocator.Current.GetInstance<ISidurDAL>();
                        dt = sidurManager.GetMeafyeneySidurById(dCardDate, 99801);
                        oObjSidurimOvdimIns = new OBJ_SIDURIM_OVDIM();
                        //InsertToObjSidurimOvdimForInsert(ref oSidur, ref oObjSidurimOvdimIns);
                        oObjSidurimOvdimIns.TAARICH = dCardDate;
                        oObjSidurimOvdimIns.MISPAR_ISHI = iMisparIshi;
                        oObjSidurimOvdimIns.MISPAR_SIDUR = SIDUR_HEADRUT_BETASHLUM;
                        oObjSidurimOvdimIns.LO_LETASHLUM = 0;
                        oObjSidurimOvdimIns.HASHLAMA = 0;
                        oObjSidurimOvdimIns.PITZUL_HAFSAKA = 0;
                        oObjSidurimOvdimIns.CHARIGA=0;
                        oObjSidurimOvdimIns.OUT_MICHSA = 0;
                        oObjSidurimOvdimIns.SHAT_HATCHALA = (String.IsNullOrEmpty(dt.Rows[0]["shat_hatchala_muteret"].ToString())) ? oParam.dSidurStartLimitHourParam1 : DateTime.Parse(dCardDate.ToShortDateString() +" "+ DateTime.Parse(dt.Rows[0]["shat_hatchala_muteret"].ToString()).ToLongTimeString());
                        oObjSidurimOvdimIns.SHAT_GMAR = (String.IsNullOrEmpty(dt.Rows[0]["shat_gmar_muteret"].ToString())) ? oParam.dSidurLimitShatGmar : DateTime.Parse(dCardDate.ToShortDateString() + " " + DateTime.Parse(dt.Rows[0]["shat_gmar_muteret"].ToString()).ToLongTimeString());
                        oObjSidurimOvdimIns.SHAT_HATCHALA_LETASHLUM = oObjSidurimOvdimIns.SHAT_HATCHALA;
                        oObjSidurimOvdimIns.SHAT_GMAR_LETASHLUM = oObjSidurimOvdimIns.SHAT_GMAR;
                        oCollSidurimOvdimIns.Add(oObjSidurimOvdimIns);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, iMisparIshi, "E", 15, dCardDate, "NewSidurHeadrutWithPaymeny15: " + ex.Message);
                _bSuccsess = false;
            }
        }

        private bool IsSidurExits(int mispar_sidur)
        {
            SidurDM oSidur;
            for (int i = 0; i < htEmployeeDetails.Count; i++)
            {
                oSidur = (SidurDM)htEmployeeDetails[i];
                if (oSidur.iMisparSidur == mispar_sidur)
                    return true;
            }
            return false;
        }
        private void DeleteElementRechev07(ref SidurDM oSidur, ref PeilutDM oPeilut, ref int iIndexPeilut,ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            //ביטול אלמנט השאלת רכב
            //תמיד לבטל  אלמנט "השאלת רכב" (73800000). 
            //אלא אם זוהי פעילות יחידה בסידור – לא לבטל אלא לסמן את הסידור "לא לתשלום" .
            //int i, iCountInsPeiluyot;
            try
            {
                if (oPeilut.lMakatNesia.ToString().Length >= 3)
                {
                    if (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "738")
                    {
                        //אם זוהי פעילות יחידה בסידור – לא לבטל אלא לסמן את הסידור "לא לתשלום
                        if (oSidur.htPeilut.Count == 1)
                        {
                            //oObjSidurimOvdimUpd = new OBJ_SIDURIM_OVDIM();
                            //InsertToObjSidurimOvdimForUpdate(ref oSidur, ref oObjSidurimOvdimUpd);
                            oObjSidurimOvdimUpd.LO_LETASHLUM = 1;
                            oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM = 3;
                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                            oSidur.iLoLetashlum = 1;
                            // oCollSidurimOvdimUpd.Add(oObjSidurimOvdimUpd);
                        }
                        else
                        {//לבטל
                            oObjPeilutOvdimDel = new OBJ_PEILUT_OVDIM();
                            InsertToObjPeilutOvdimForDelete(ref oPeilut, ref oSidur, ref oObjPeilutOvdimDel);
                            oCollPeilutOvdimDel.Add(oObjPeilutOvdimDel);
                            oSidur.htPeilut.RemoveAt(iIndexPeilut);
                            iIndexPeilut = iIndexPeilut - 1;
                           
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 7, oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "DeleteElementRechev07: " + ex.Message, null);
                _bSuccsess = false;
            }
        }

        private void FixedMisparSidurToNihulTnua16(ref SidurDM oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            //הפיכת החתמת שעון של מנ"ס-סדרן/פקח לסידור ניהול תנועה
            //שינוי מספר סידור + שעות + קוד נסיעות

            //. עובד שקוד עיסוקו 420 (מנ"ס-סדרן) יש להפוך כל החתמת שעון שלו (סידור 99001, מזהים לפי ערך 1 (מינהל) במאפיין 54 (שעון נוכחות) בטבלת סידורים מיוחדים) לסידור 99224.
            //אם יש לו מאפיין זכאות לזמן נסיעה (51) , יש לשתול סימון "נסיעות" (1 אם החתים רק כניסה, 2 אם החתים רק יציאה, 3 אם החתים שניהם). זיהוי ההחתמה - קיים ערך בשדה "מיקום שעון" כניסה/יציאה או שאין ערך בשדה "מיקום שעון" כניסה/יציאה אך יש סיבה להחתמה ידנית ואישור  ידנית ואישור החתמה ידנית.

            //. עובד שקוד עיסוקו 422 (מנ"ס-פקח) יש להפוך כל החתמת שעון שלו (סידור 99001, מזהים לפי ערך 1 (מינהל) במאפיין 54 (שעון נוכחות) בטבלת סידורים מיוחדים) לסידור 99225.
            //אם יש לו מאפיין זכאות לזמן נסיעה (51) , יש לשתול סימון "נסיעות" (1 אם החתים רק כניסה, 2 אם החתים רק יציאה, 3 אם החתים שניהם). זיהוי ההחתמה - קיים ערך בשדה "מיקום שעון" כניסה/יציאה או שאין ערך בשדה "מיקום שעון" כניסה/יציאה אך יש סיבה להחתמה ידנית ואישור החתמה ידנית.
           
            //int iHachtamatShaon = 0;
            int iNewMisparSidur=0;
            try
            {
                if (oSidur.sShaonNochachut == enShaonNochachut.enMinhal.GetHashCode().ToString())
                {
                    if (oOvedYomAvodaDetails.iIsuk == clGeneral.enIsukOved.ManasSadran.GetHashCode())
                    {
                        iNewMisparSidur = 99224;
                    }
                    else
                    {
                        if (oOvedYomAvodaDetails.iIsuk == clGeneral.enIsukOved.ManasPakch.GetHashCode())
                        {
                            iNewMisparSidur = 99225;
                        }
                    }
                }
                if (iNewMisparSidur > 0)
                {
                    oObjSidurimOvdimUpd.NEW_MISPAR_SIDUR = iNewMisparSidur;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;

                    DataRow[] drSidurMeyuchad;
                    drSidurMeyuchad = _dtSidurimMeyuchadim.Select("mispar_sidur=" + iNewMisparSidur);
                    var sidurManager = ServiceLocator.Current.GetInstance<ISidurManager>();
                    oSidur = sidurManager.CreateClsSidurFromSidurMeyuchad(oSidur, _dCardDate, iNewMisparSidur, drSidurMeyuchad[0]);
                    oObjSidurimOvdimUpd.NEW_MISPAR_SIDUR = iNewMisparSidur;
                }
                //if (oMeafyeneyOved.IsMeafyenExist(51))
                //{
                //    if ((!String.IsNullOrEmpty(oSidur.sMikumShaonKnisa)) && (!String.IsNullOrEmpty(oSidur.sMikumShaonYetzia)))
                //    {
                //        //אם קיים גם החתמת שעון כניסה וגם יציאה - ערך 3
                //        iHachtamatShaon = clGeneral.enHachtamatShaon.KnisaAndYetzia.GetHashCode();
                //    }
                //    else
                //    {
                //        if (!String.IsNullOrEmpty(oSidur.sMikumShaonKnisa))
                //        {
                //            //קיים רק ערך כניסה - 1
                //            iHachtamatShaon = clGeneral.enHachtamatShaon.Knisa.GetHashCode();
                //        }
                //        else
                //        {
                //            if (!String.IsNullOrEmpty(oSidur.sMikumShaonYetzia))
                //            {
                //                // קיים רק ערך יציאה - 2
                //                iHachtamatShaon = clGeneral.enHachtamatShaon.Yetzia.GetHashCode();
                //            }
                //        }
                //    }
                //}
                //if (iHachtamatShaon > 0)
                //{
                //    oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = iHachtamatShaon;
                //    oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                //}
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 16, oSidur.iMisparIshi,oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "FixedMisparSidurToNihulTnua16: " + ex.Message, null);
                _bSuccsess = false;
            }
        }

       

        private void SidurLoLetashlum11(DataRow[] drSugSidur, ref SidurDM oSidur, int iSidurIndex,ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            bool bSign = false;
            int iKodSibaLoLetashlum=0;
            //בדיקה ברמת סידור
            //סימון סידור עבודה "לא לתשלום" אם עונה על תנאים מגבילים
            try
            {
                //תנאים לסימון לא לתשלום:
                //1. סידור ועד עובדים ביום שבתון - 
                //סידור ועד עובדים הוא סידור מיוחד עם ערך 10 (ועד עובדים) במאפיין 52 (סוג עבודה) בטבלת סידורים  מיוחדים. יום שבתון מזוהה לפי ערך 7 שחוזר מה- oracle או שחוזר ערך שבתון מטבלת סוגי  ימים מיוחדים.
                //2. סידור חופש על חשבון שעות נוספות  (99822) ביום שישי - מזהים את הסידור לפי ערך 9 (חופש ע"ח שעות נוספות) במאפיין 53 (סוג היעדרות) בטבלת סידורים מיוחדים. יום שישי (רק שישי, לא ערב חג) מזוהה לפי ערך 6 שחוזר מה- oracle.
                //3. סידור המכיל פעילות בודדת של אלמנט המוגדר "לידיעה בלבד"  (לאלמנט ערך 2 (ידיעה בלבד) במאפיין 3 (פעולה / ידיעה בלבד
                //4. לעובד סידור מיוחד המוגדר תפקיד (ערך 1 (תפקיד) במאפיין 3 (סקטור עבודה) בטבלת סידורים מיוחדים), והסידור בוצע ביום שבתון (יום שבתון מזוהה לפי ערך 7 שחוזר מה- oracle או שחוזר ערך שבתון מטבלת סוגי  ימים מיוחדים), ולעובד אין מאפיין עבודה בשבתון (מאפיין 07 שעת התחלה מותרת בשבתון) ולא סומן חריגה 3 (התחלה וגמר).
                //5. לעובד סידור מיוחד המוגדר תפקיד (ערך 1 (תפקיד) במאפיין 3 (סקטור עבודה) בטבלת סידורים מיוחדים), והסידור בוצע ביום שישי (רק שישי, לא ערב חג), ולעובד אין מאפיין עבודה בשישי (מאפיין 05, שעת התחלה מותרת בשישי), ולא סומן חריגה 3 (התחלה וגמר).
                //6. עובד מותאם (יש לו ערך בקוד נתון 8 בטבלת פרטי עובדים) ללא שעות נוספות  (בודקים אם למותאם אסור לעשות שעות נוספות -  ניגשים עם קוד המותאמות של העובד לטבלה CTB_Mutamut ובודקים אם יש ערך בשדה   Isur_Shaot_Nosafot)
                //אם עובד 5 ימים (מזהים עובד 5 ימים לפי ערך 51/52 במאפיין 56 בטבלת מאפייני עובדים) והסידור הוא ביום שישי (רק שישי, לא ערב חג)  או שבתון – מסמנים.
                //אם עובד 6 ימים (מזהים עובד 6 ימים לפי ערך 61/62 במאפיין 56 בטבלת מאפייני עובדים)
                //והסידור הוא בשבתון – מסמנים.
                //7. סידור שיש לו מאפיין 79 (לא לתשלום אוטומטי) (ערך 1). 
                //רלוונטי גם עבור רגילים וגם עבור מיוחדים.
                //שים לב – לאחר סימון "לא לתשלום" יש טיפול גם בשעות התחלה וגמר לתשלום.
                //מלבד זאת, קיים עדכון "לא לתשלום" כחלק מנושא גרירות".
                //וגם כחלק מקביעת "שעות לתשלום".

                //תנאי 1 
                // אין לבצע את השינוי עבור סידורים מיוחדים עם ערך 1 במאפיין 4.
                if (!(oSidur.bSidurMyuhad && oSidur.iSidurLoNibdakSofShavua == 1))
                {
                    bSign = IsOvedMatzavExists("6");
                    if (!bSign)
                    {
                        bSign = Condition1Saif11(ref oSidur);
                        if (!bSign)
                        {
                            //תנאי 2
                            bSign = Condition2Saif11(ref oSidur);
                            if (!bSign)
                            {
                                //תנאי 3
                                bSign = Condition3Saif11(ref oSidur, iSidurIndex);
                                if (!bSign)
                                {
                                    //תנאי 4
                                    bSign = Condition4Saif11(ref oSidur);
                                    if (!bSign)
                                    {
                                        //5 תנאי
                                        bSign = Condition5Saif11(ref oSidur);
                                        if (!bSign)
                                        {
                                            //תנאי 6
                                            bSign = Condition6Saif11(ref oSidur);
                                            if (!bSign)
                                            {
                                                bSign = ConditionSidurHeadrut(ref oSidur);
                                                if (!bSign)
                                                {
                                                    //אגד תעבורה 
                                                    //bSign =  Saif5EggedTaavura(drSugSidur, ref oSidur);
                                                    //if (!bSign)
                                                    //{
                                                    //תנאי 7
                                                    bSign = Condition7Saif11(drSugSidur, ref oSidur);
                                                    if (!bSign)
                                                    {
                                                        //תנאי 8
                                                        bSign = Condition8Saif11(drSugSidur, ref oSidur);
                                                        if (!bSign)
                                                        {
                                                            //תנאי 9
                                                            bSign = Condition9Saif11(drSugSidur, ref oSidur);
                                                            if (bSign)
                                                            {
                                                                iKodSibaLoLetashlum = 10;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            iKodSibaLoLetashlum = 17;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        iKodSibaLoLetashlum = 15;
                                                    }
                                                    //}
                                                    //else
                                                    //{
                                                    //    iKodSibaLoLetashlum = 12;
                                                    //} 
                                                }
                                                else
                                                {
                                                    iKodSibaLoLetashlum = 22;
                                                }
                                                
                                            }
                                            else
                                            {
                                                iKodSibaLoLetashlum = 13;
                                            }
                                        }
                                        else
                                        {
                                            iKodSibaLoLetashlum = 5;
                                        }
                                    }
                                    else
                                    {
                                        iKodSibaLoLetashlum = 4;
                                    }
                                }
                                else
                                {
                                    iKodSibaLoLetashlum = 3;
                                }
                            }
                            else
                            {
                                iKodSibaLoLetashlum = 14;
                            }

                        }
                        else
                        {
                            iKodSibaLoLetashlum = 2;
                        }
                    }
                    else
                    {
                        iKodSibaLoLetashlum = 21;
                    }

                    //if ((iKodSibaLoLetashlum == 4 || iKodSibaLoLetashlum == 5 || iKodSibaLoLetashlum == 10) && CheckApprovalStatus("211,2,4,5,511,6,10,1011", oSidur.iMisparSidur, oSidur.dFullShatHatchala) == 1)
                    //{ bSign = false; }

                    if (bSign)
                    {
                       
                        oObjSidurimOvdimUpd.LO_LETASHLUM = 1;
                        oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM = iKodSibaLoLetashlum;
                        oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                        oSidur.iLoLetashlum = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 11, oSidur.iMisparIshi,oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "SidurLoLetashlum11: " + ex.Message, null);
                _bSuccsess = false;
            }
        }
        private bool Condition1Saif11(ref SidurDM oSidur)
        {
            //תנאי ראשון לסעיף 11
            return ((oSidur.bSidurMyuhad) && (oSidur.sSugAvoda == enSugAvoda.VaadOvdim.GetHashCode().ToString()) && (clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, iSugYom, oSidur.dSidurDate)));
        }

        private bool Condition2Saif11(ref SidurDM oSidur)
        {
            //תנאי שני לסעיף 11
            return ((oSidur.iMisparSidur == 99822) && (_oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || _oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek2.GetHashCode()) && ((oSidur.sSidurDay == enDay.Shishi.GetHashCode().ToString())));
        }

        private bool Condition3Saif11(ref SidurDM oSidur, int iSidurIndex)
        {
            //תנאי שלישי לסעיף 11
            bool bElementLeyedia = false;
            PeilutDM oPeilut;
            try{
            if (oSidur.htPeilut.Count == 1)
            {
                oPeilut = (PeilutDM)oSidur.htPeilut[0];
                if ((oPeilut.iMakatType == enMakatType.mElement.GetHashCode()) && (oPeilut.iElementLeYedia == 2))
                {
                    bElementLeyedia = true;
                }
            }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bElementLeyedia;
           
        }

        private bool Condition4Saif11(ref SidurDM oSidur)
        {
            //תנאי רביעי לסעיף 11
            bool bSign = false;
            try{
               // if ((oSidur.bSidurMyuhad) && (oSidur.sSectorAvoda == enSectorAvoda.Tafkid.GetHashCode().ToString()) && (clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, iSugYom, oSidur.dSidurDate)))
                if (clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, iSugYom, oSidur.dSidurDate) &&
                    (oSidur.bSidurMyuhad && oSidur.sShaonNochachut == "1" && oSidur.sChariga == "0"))
                        //וגם לעובד אין מאפיין 7 ו- 8 ברמה האישית. 
                        if (!oMeafyeneyOved.IsMeafyenExist(7) && !oMeafyeneyOved.IsMeafyenExist(8))
                        {
                            bSign = true;
                        }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bSign;
        }

        private bool Condition5Saif11(ref SidurDM oSidur)
        {
            //תנאי חמישי לסעיף 11
            bool bSign = false;
            try{

                if ((oSidur.sSidurDay == enDay.Shishi.GetHashCode().ToString()) &&
                    (oSidur.bSidurMyuhad && oSidur.sShaonNochachut == "1" && oSidur.sChariga == "0"))
                        //וגם לעובד אין מאפיין 5 ו- 6 ברמה האישית. 
                        if (!oMeafyeneyOved.IsMeafyenExist(5) && !oMeafyeneyOved.IsMeafyenExist(6))
                        {
                            bSign = true;
                        }     
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bSign;
        }

        private bool Condition6Saif11(ref SidurDM oSidur)
        {
            //תנאי שישי לסעיף 11
            bool bSign = false;
            bool bIsurShaotNosafot;
            try{
            if (!String.IsNullOrEmpty(oOvedYomAvodaDetails.sMutamut))
            {
                GetMutamut(dtMutamut, int.Parse(oOvedYomAvodaDetails.sMutamut), out bIsurShaotNosafot);
                if (bIsurShaotNosafot)
                {
                //עובד 6 ימים 
                    if ((oMeafyeneyOved.IsMeafyenExist(56)) && ((oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved6DaysInWeek1.GetHashCode()) || (oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved6DaysInWeek2.GetHashCode())))
                    {
                        if (clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, iSugYom, oSidur.dSidurDate))
                        {
                            bSign = true;
                        }
                    }
                    //עובד 5 ימים 
                    if ((oMeafyeneyOved.IsMeafyenExist(56)) && ((oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek1.GetHashCode()) || (oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())))
                    {
                        if (clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, iSugYom, oSidur.dSidurDate) || (oSidur.sSidurDay == enDay.Shishi.GetHashCode().ToString()))
                        {
                            bSign = true;
                        }
                    }
                }
            }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bSign;
        }

        private bool Saif5EggedTaavura(DataRow[] drSugSidur, ref SidurDM oSidur)
        {
            bool bSign = false;

            if (oOvedYomAvodaDetails.iKodHevra == enEmployeeType.enEggedTaavora.GetHashCode() && oSidur.bSidurNotValidKodExists)
            {
                bSign = true;
            }
            return bSign;
        }


        private bool ConditionSidurHeadrut(ref SidurDM oSidur)
        {
            bool bLoLetashlumAutomati = false;
            if (iSugYom > 19 ||(iSugYom==10 && (oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek2.GetHashCode() )))
            {
                if (oSidur.bSidurMyuhad)
                {//סידור מיוחד
                    if (!string.IsNullOrEmpty(oSidur.sHeadrutTypeKod))
                    {
                        if ((oSidur.sHeadrutTypeKod == enMeafyenSidur53.enMachala.GetHashCode().ToString()) ||
                            (oSidur.sHeadrutTypeKod == enMeafyenSidur53.enMilueim.GetHashCode().ToString()) ||
                            (oSidur.sHeadrutTypeKod == enMeafyenSidur53.enTeuna.GetHashCode().ToString()) ||
                            (oSidur.sHeadrutTypeKod == enMeafyenSidur53.enEvel.GetHashCode().ToString()))
                        {
                            bLoLetashlumAutomati = true;
                        }
                    }
                }

            }
            //תנאי שביעי לסעיף 11
          
            return bLoLetashlumAutomati;
        }
        private bool Condition7Saif11(DataRow[] drSugSidur, ref SidurDM oSidur)
        {
            bool bLoLetashlumAutomati;

            //תנאי שביעי לסעיף 11
            if (oSidur.bSidurMyuhad)
            {
                bLoLetashlumAutomati = oSidur.bLoLetashlumAutomatiExists;
            }
            else
            {
                if (drSugSidur.Length > 0)
                {
                    //TODO: מחכה לתשובה ממירי
                    bLoLetashlumAutomati = (drSugSidur[0]["lo_letashlum_automati"].ToString() == enMeafyen79.LoLetashlumAutomat.GetHashCode().ToString());
                }
                else
                {
                    bLoLetashlumAutomati = false;
                }
            }
            return bLoLetashlumAutomati;
        }

        private bool Condition8Saif11(DataRow[] drSugSidur, ref SidurDM oSidur)
        {
            bool bLoLetashlumAutomati = false;
   
             // וגם מדובר בסידור מיוחד עם עם מאפיין לסידורים מיוחדים קוד = 54 עם ערך 1.
            if (oSidur.bSidurMyuhad && oSidur.sShaonNochachut == "1" && oSidur.sChariga == "0") // && oOvedYomAvodaDetails.iIsuk.ToString().Substring(0,1) == "5")
            {
                //וגם לעובד אין מאפיין 3 ו- 4 ברמה האישית. 
                if (!oMeafyeneyOved.IsMeafyenExist(3) && !oMeafyeneyOved.IsMeafyenExist(4))
                {
                    bLoLetashlumAutomati = true;
                }
            }

            return bLoLetashlumAutomati;
        }
 
        private bool Condition9Saif11(DataRow[] drSugSidur, ref SidurDM oSidur)
        {
            bool bLoLetashlumAutomati=true;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum;
            dShatHatchalaLetashlum=oSidur.dFullShatHatchala;
            dShatGmarLetashlum = oSidur.dFullShatGmar;
            bool bFromMeafyenHatchala, bFromMeafyenGmar;

            GetOvedShatHatchalaGmar(oSidur.dFullShatGmar, _oMeafyeneyOved, ref oSidur, ref dShatHatchalaLetashlum, ref dShatGmarLetashlum, out bFromMeafyenHatchala, out bFromMeafyenGmar);
            bLoLetashlumAutomati = CheckLoLetashlumMeafyenim(drSugSidur, oSidur, dShatHatchalaLetashlum, dShatGmarLetashlum, bFromMeafyenHatchala, bFromMeafyenGmar);

            return bLoLetashlumAutomati;
        }

        private bool CheckLoLetashlumMeafyenim(DataRow[] drSugSidur,SidurDM oSidur, DateTime dShatHatchalaLetashlum, DateTime dShatGmarLetashlum,bool bFromMeafyenHatchala, bool bFromMeafyenGmar)
        {
            bool bLoLetashlumAutomati = false;
            string sMeafyenKizuz = "";
            DateTime shaa;
            if (oSidur.bSidurMyuhad)
            {
                sMeafyenKizuz = oSidur.sKizuzAlPiHatchalaGmar;
            }
            else
            {
                if (drSugSidur.Length > 0)
                {
                    sMeafyenKizuz = drSugSidur[0]["kizuz_al_pi_hatchala_gmar"].ToString();
                }
            }

            if (!string.IsNullOrEmpty(sMeafyenKizuz) && oSidur.iLoLetashlum == 0)
            {
                if (sMeafyenKizuz == "1")
                {                   
                    if (bFromMeafyenHatchala && bFromMeafyenGmar)
                    {
                        if (((oSidur.dFullShatGmar != DateTime.MinValue && (oSidur.dFullShatGmar <= dShatHatchalaLetashlum)) || (oSidur.dFullShatHatchala != DateTime.MinValue && oSidur.dFullShatHatchala >= dShatGmarLetashlum)) && oSidur.sChariga == "0")
                        {
                            shaa = DateTime.Parse(oSidur.dFullShatHatchala.ToShortDateString() + " 18:00:00");
                            if (!oMeafyeneyOved.IsMeafyenExist(42) && oMeafyeneyOved.IsMeafyenExist(23) && oMeafyeneyOved.IsMeafyenExist(24)){
                                if ((oSidur.dFullShatHatchala.Hour >= 11 && oSidur.dFullShatHatchala.Hour <= 17 && oSidur.dFullShatGmar > shaa)
                                    && (oSidur.sShabaton != "1" && (iSugYom >= enSugYom.Chol.GetHashCode() && iSugYom < enSugYom.Shishi.GetHashCode())))
                                     bLoLetashlumAutomati = false;
                                 else bLoLetashlumAutomati = true;
                             }
                             else
                                bLoLetashlumAutomati = true;
                        }
                    }
                    else if ((bFromMeafyenHatchala && !bFromMeafyenGmar) || (!bFromMeafyenHatchala && bFromMeafyenGmar))
                    {
                        bLoLetashlumAutomati = true;
                    }
                }
            }

            return bLoLetashlumAutomati;
        }

        private void ChangeElement06(ref PeilutDM oPeilut,
                                     ref SidurDM oSidur, int iKey)
        {
            //int iLocalKey=1;
            //PeilutDM oLocalPeilut;
            long lElementNumber = 0;

            //בדיקה ברמת פעילות
            try
            {
                //איפוס אלמנט הכנת מכונה שניה אם לא הוחלף רכב
                //שינוי זמן אלמנט או החלפת קוד האלמנט
                //אם מופיע בסידור המקורי אלמנט הכנת מכונה שניה ( xxxxx712) ואם לא הוחלף רכב (כלומר - מספר הרכב באלמנט זהה למספר הרכב בפעילות הקודמת מסוג נסיעה או אלמנט הדורש מספר רכב (אלמנט דורש מספר רכב אם יש לו מאפיין 11 (חובה מספר רכב)) בטבלת מאפייני אלמנטים ) אזי יש להחליף את זמן האלמנט (תוים 4-6 במק"ט) ל-000.
                //אם הוחלף רכב - אזי יש להפוך את קוד האלמנט מ-712 (הכנת מכונה שניה) ל-711 (הכנת מכונה שניה (רשום)).
                if (oPeilut.lMakatNesia.ToString().Length >= 6)
                {
                    if (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3).Equals("712"))
                    {
                        //InsertToObjPeilutOvdimForUpdate(ref oPeilut, ref oSidur, ref oObjPeilutOvdimUpd);
                        lElementNumber = long.Parse(string.Concat("712", "000", oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(6, 2)));
                        oObjPeilutOvdimUpd.MAKAT_NESIA = lElementNumber;
                        oObjPeilutOvdimUpd.UPDATE_OBJECT = 1;

                        oPeilut = CreatePeilut(_iMisparIshi, _dCardDate, oPeilut, lElementNumber, dtTmpMeafyeneyElements);
                        ////נעבור על כל הפעילויות של הסידור
                        //for (int j = iKey - 1; j >= 0; j--)
                        //{
                        //    iLocalKey = int.Parse(dePeilutEntry.Key.ToString());
                        //    //if (j == iKey)
                        //    //{
                        //    //    continue;
                        //    //}
                        //    oLocalPeilut = (clPeilut)oSidur.htPeilut[j];
                        //    if (oLocalPeilut.IsMustBusNumber())
                        //    //if (oLocalPeilut.iMakatType == enMakatType.mEmpty.GetHashCode() ||
                        //    //    oLocalPeilut.iMakatType == enMakatType.mKavShirut.GetHashCode() ||
                        //    //    oLocalPeilut.iMakatType == enMakatType.mNamak.GetHashCode() ||
                        //    //    ((oLocalPeilut.iMakatType == enMakatType.mElement.GetHashCode()) && (oLocalPeilut.bBusNumberMustExists)))
                        //    {// נמצאה פעילות מסוג נסיעה או אלמנט הדורש מספר רכב
                        //        if (oLocalPeilut.lOtoNo == oPeilut.lOtoNo)
                        //        {//לא הוחלף רכב
                        //            lElementNumber = long.Parse(string.Concat("712", "000", oPeilut.lMakatNesia.ToString().Substring(6, 2)));
                        //        }
                        //        else
                        //        {//הוחלף רכב
                        //            lElementNumber = long.Parse(string.Concat("711", oPeilut.lMakatNesia.ToString().Substring(3)));
                        //        }
                        //    }
                        //    //iLocalKey++;
                        //}

                        //if (lElementNumber > 0)
                        //{//נעדכן את קוד האלמנט
                        //    //oObjPeilutOvdimUpd = new OBJ_PEILUT_OVDIM();
                        //    //InsertToObjPeilutOvdimForUpdate(ref oPeilut, ref oSidur, ref oObjPeilutOvdimUpd);
                        //    oObjPeilutOvdimUpd.MAKAT_NESIA = lElementNumber;
                        //    oObjPeilutOvdimUpd.UPDATE_OBJECT = 1;
                        //    //oCollPeilutOvdimUpd.Add(oObjPeilutOvdimUpd);
                        //}
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 6, oSidur.iMisparIshi,oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "ChangeElement06: " + ex.Message, null);
                _bSuccsess = false;
            }
        }

        private void GetMutamut(DataTable dtMutamut, int iKodMutamut, out bool bIsurShaotNosafot)
        {
            DataRow[] dr;
            int iIsurShaotNosafot = 0;
            //מקבל קוד מותאמות ומחזיר אם יש איסור של שעות נוספות
            try
            {
                dr = dtMutamut.Select(string.Concat("kod_mutamut=", iKodMutamut));
                if (dr.Length > 0)
                {
                    iIsurShaotNosafot = string.IsNullOrEmpty(dr[0]["isur_shaot_nosafot"].ToString()) ? 0 : int.Parse(dr[0]["isur_shaot_nosafot"].ToString());
                }
                bIsurShaotNosafot = iIsurShaotNosafot > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateOutMichsa(ref SidurDM oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            //שינוי ברמת סידור
            //עדכון שדה מחוץ למכסה
            try
            {
                //if (oOvedYomAvodaDetails.iKodHevra == clGeneral.enEmployeeType.enEggedTaavora.GetHashCode())
                //{
                //    oObjSidurimOvdimUpd.OUT_MICHSA = 0;
                //    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                //    //oObjSidurimOvdimUpd.bUpdate = true;
                //    oSidur.sOutMichsa = "0";
                //}
                //else
                //{
                    if ((oSidur.bSidurMyuhad) && (oObjSidurimOvdimUpd.LO_LETASHLUM == 0 || (oObjSidurimOvdimUpd.LO_LETASHLUM == 1 && oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM==1)))
                    {
                        if ((oSidur.sZakayMichutzLamichsa == enMeafyenSidur25.enZakaiAutomat.GetHashCode().ToString()) )
                        {   //אם סידור הוא סידור מיוחד ויש לו ערך 3 במאפיין 25 (זכאי אוטומטית "מחוץ למכסה")
                            //וגם הוא לא מאגד תעבורה.                                               
                            oObjSidurimOvdimUpd.OUT_MICHSA = 1;
                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                            oSidur.sOutMichsa = "1";
                        }
                        else
                        {
                            oObjSidurimOvdimUpd.OUT_MICHSA = 0;
                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                            oSidur.sOutMichsa = "0";
                        }
                    }
                    else
                    {
                        oObjSidurimOvdimUpd.OUT_MICHSA = 0;
                        oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                        oSidur.sOutMichsa = "0";
                    }
             //   }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 0, oSidur.iMisparIshi,oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "UpdateOutMichsa: " + ex.Message, null);
                _bSuccsess = false;
            }
        }

        private void UpdateChariga(ref SidurDM oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            //עדכון שדה חריגה ברמת סידור
            //חריגה משעת התחלה/גמר	שינוי נתוני קלט:
            //1. הסידור מזכה אוטומטית (ערך 3 במאפיין 35) 
            try
            {
                if ((oObjSidurimOvdimUpd.LO_LETASHLUM == 0 || (oObjSidurimOvdimUpd.LO_LETASHLUM == 1 && oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM == 1)) && (oSidur.sZakaiLeChariga == enMeafyenSidur35.enCharigaAutomat.GetHashCode().ToString()))
                {
                    oObjSidurimOvdimUpd.CHARIGA = 3;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                    oSidur.sChariga = "3";

                 }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 0, oSidur.iMisparIshi,oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "UpdateChariga: " + ex.Message, null);
                _bSuccsess = false;
            }
        }

        private void UpdateHashlamaForSidur(ref SidurDM oSidur,int iSidurIndex, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            //עדכון שדה השלמה ברמת סידור
            //עדכון שדה השלמה
            int iParamHashlama;
            double dZmanSidur, dZmanHashlama;
            bool bValidHashlama = false;
            DataRow[] drSugSidur;
            try
            {
                //float fSidurTime = 0;
                //0	אין השלמה	שינוי נתוני קלט:
                //1. ברירת מחדל אלא אם כן מתקיימים תנאים לעדכון ערך אחר.
                //2. עבור אגד תעבורה, תמיד לא מקבלים השלמה (מזהים אגד תעבורה לפי קוד חברה של העובד - 4895 ).
                if (oSidur.sHashlama != "0")
                {
                    oObjSidurimOvdimUpd.HASHLAMA = 0;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                    oSidur.sHashlama = "0";
                }

                if (oOvedYomAvodaDetails.iKodHevra != enEmployeeType.enEggedTaavora.GetHashCode())
                {
                    //התנאים לקבלת ערך 1 - השלמה לשעה                          
                    //כל הסידורים מסומנים ללא ערך בשדה השלמה (השלמה כלשהיא, לא משנה איזה ערך), אם קיימת -  עוצרים.
                    if (!htEmployeeDetails.Values.Cast<SidurDM>().ToList().Any(sidur => sidur.sHashlama != "0" && !string.IsNullOrEmpty(sidur.sHashlama)))
                    {
                        //מחפשים את הסידור הראשון ביום שהמשך שלו קטן מהערך בפרמטרים 101 - 103.
                        //הסידור (101 (זמן מינימום לסידור חול להשלמה, 102 - זמן מינימום לסידור שישי/ע.ח להשלמה, 103 - זמן מינימום לסידור שבת/שבתון
                        if (clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, iSugYom, oSidur.dSidurDate))
                        { iParamHashlama = oParam.iHashlamaShabat; }
                        else if ((oSidur.sErevShishiChag == "1") || (oSidur.sSidurDay == enDay.Shishi.GetHashCode().ToString()))
                        { iParamHashlama = oParam.iHashlamaShisi; }
                        else { iParamHashlama = oParam.iHashlamaYomRagil; }

                        dZmanSidur = oSidur.dFullShatGmar.Subtract(oSidur.dFullShatHatchala).TotalMinutes;
                        //אם משך הסידור או הערך בפרמטר המתאים ליום העבודה שווה או גדול משעה - עוצרים.
                        if (dZmanSidur < iParamHashlama && iParamHashlama <= 60 && dZmanSidur <= 60)
                        {
                           
                            //. לסידור מאפיין 40 (לפי מספר סידור או סוג סידור)עם ערך 2 (זכאי אוטומטי) והסידור אינו מבוטל
                            if (oSidur.bSidurMyuhad && oSidur.sHashlamaKod == "2")
                            { bValidHashlama = true; }
                            else //if (oSidur.bSidurRagilExists)
                            {
                                drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, oSidur.dSidurDate, _dtSugSidur);
                                if (drSugSidur[0]["zakay_lehashlama_avur_sidur"].ToString() == "2")
                                {
                                    bValidHashlama = true;
                                }
                            }

                            //אם הסידור אינו אחרון או בודד ביום
                            if (htEmployeeDetails.Values.Count > 1 && iSidurIndex < htEmployeeDetails.Values.Count - 1)
                            {
                                //יש לבדוק האם שעת ההתחלה של הסידור העוקב שווה לשעת הגמר של הסידור שלנו +משך ההשלמה. אם כן - עוצרים.
                                dZmanHashlama = double.Parse("60") - double.Parse(dZmanSidur.ToString());
                                if (oSidur.dFullShatGmar.AddMinutes(dZmanHashlama) > ((SidurDM)htEmployeeDetails[iSidurIndex + 1]).dFullShatHatchala)
                                {
                                    bValidHashlama = false;
                                }
                            }

                            if(bValidHashlama)
                            {
                                oObjSidurimOvdimUpd.HASHLAMA = 1;
                                oObjSidurimOvdimUpd.SUG_HASHLAMA = 1;
                                oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;

                                oSidur.sHashlama = "1";
                                oSidur.iSugHashlama = 1;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 0, oSidur.iMisparIshi,oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "UpdateHashlamaForSidur: " + ex.Message, null);
                _bSuccsess = false;
            }
        }

        private void UpdateTachograph(bool bSidurNahagut)
        {
            //עדכון ברמת יום עבודה
            PeilutDM oPeilut;
            SidurDM oSidur;
            bool bNotDegem64=false;
            int CountPeiluyot = 0;
            try
            {
                //   בדיקה האם כל הרכבים המדווחים בפעילויות באותו תאריך הם מדגם 64 
                //.מספיק שיהיה רכב אחד עם דגם שונה כדי שתנאי זה לא יעבוד
                if ((htEmployeeDetails != null) && (htEmployeeDetails.Count>0))
                {
                    for (int i = 0; i < htEmployeeDetails.Count; i++)
                    {
                        oSidur = ((SidurDM)(htEmployeeDetails[i]));
                        CountPeiluyot = CountPeiluyot + oSidur.htPeilut.Count;
                        for (int j = 0; j < oSidur.htPeilut.Count; j++)
                        {
                            oPeilut = (PeilutDM)oSidur.htPeilut[j];
                            if (dtMashar.Select("bus_number=" + oPeilut.lOtoNo + " and SUBSTRING(convert(Vehicle_Type,'System.String'),3,2)<>64").Length > 0)
                            {
                                bNotDegem64 = true;
                                break;
                            }
                        }
                    }
                }

                //יש לבדוק שלפחות אחד הרכבים המדווחים באותו תאריך אינו מדגם 64  (דגם שאינו מכיל טכוגרף).
                if (bSidurNahagut && (bNotDegem64 || CountPeiluyot==0))
                {
                    //למ.א+תאריך, אם מזהים סידור נהגות אחד לפחות (זיהוי סידור נהגות לפי ערך 5 (נהגות) במאפיין 3 (סקטור עבודה) בטבלאות סידורים מיוחדים/מאפייני סוג סידור).
                    //יש טכוגרף
                    oObjYameyAvodaUpd.TACHOGRAF = "2";
                    oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                }
                else
                {
                    ////. כל הרכבים המדווחים בפעילויות באותו תאריך הם מדגם 64 
                
                    //. למ.א+תאריך, אם לא מזהים סידור נהגות אחד לפחות (זיהוי סידור נהגות לפי ערך 5 (נהגות) במאפיין 3 (סקטור עבודה) בטבלאות סידורים מיוחדים/מאפייני סוג סידור).
                    //אין טכוגרף
                    oObjYameyAvodaUpd.TACHOGRAF = "0";
                    oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                }
            
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", 0, "UpdateTachograph: " + ex.Message);
                _bSuccsess = false;
            }
        }

        private bool IsSidurNahagut(DataRow[] drSugSidur, SidurDM oSidur)
        {
            bool bSidurNahagut = false;

            //הפונקציה תחזיר TRUE אם הסידור הוא סידור נהגות

            try
            {
                if (oSidur.bSidurMyuhad)
                {//סידור מיוחד
                    bSidurNahagut = (oSidur.sSectorAvoda == enSectorAvoda.Nahagut.GetHashCode().ToString());
                }
                else
                {//סידור רגיל
                    if (drSugSidur.Length > 0)
                    {
                        bSidurNahagut = (drSugSidur[0]["sector_avoda"].ToString() == enSectorAvoda.Nahagut.GetHashCode().ToString());
                    }
                }                
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 0, oSidur.iMisparIshi,oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurNahagut: " + ex.Message, null);
                _bSuccsess = false;
            }

            return bSidurNahagut;
        }

        private bool IsSidurNihulTnua(DataRow[] drSugSidur, SidurDM oSidur)
        {
            bool bSidurNihulTnua = false;
            bool bElementZviraZman = false;  
            //הפונקציה תחזיר TRUE אם הסידור הוא סידור נהגות

            try
            {
                if (oSidur.bSidurMyuhad)
                {//סידור מיוחד
                    bSidurNihulTnua = (oSidur.sSectorAvoda == enSectorAvoda.Nihul.GetHashCode().ToString());
                    if (!bSidurNihulTnua)
                       if (oSidur.iMisparSidur == 99301){
                    
                        PeilutDM oPeilut = null;
                        for (int i = 0; i < oSidur.htPeilut.Count; i++)
                        {
                             oPeilut = (PeilutDM)oSidur.htPeilut[i];
                             if ( !string.IsNullOrEmpty(oPeilut.sElementZviraZman))
                                 if (int.Parse(oPeilut.sElementZviraZman) == 4)
                                 {
                                     bElementZviraZman = true;
                                     break;
                                 }
                        }
                        if( bElementZviraZman)
                            bSidurNihulTnua = true;
                    }
                }
                else
                {//סידור רגיל
                    if (drSugSidur.Length > 0)
                    {
                        bSidurNihulTnua = (drSugSidur[0]["sector_avoda"].ToString() == enSectorAvoda.Nihul.GetHashCode().ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 0, oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurNihulTnua: " + ex.Message, null);
                _bSuccsess = false;
            }

            return bSidurNihulTnua;
        }

        private bool IsSidurShonim(DataRow[] drSugSidur, SidurDM oSidur)
        {
            bool bSidurZakaiLenesiot = false;

            try
            {
                if (oSidur.iLoLetashlum == 0)
                {
                    //הפונקציה תחזיר TRUE אם הסידור זכאי לזמן נסיעות
                    if (oSidur.bSidurMyuhad)
                    {//סידור מיוחד
                        bSidurZakaiLenesiot = ((oSidur.sShaonNochachut == enShaonNochachut.enMinhal.GetHashCode().ToString() || oSidur.sShaonNochachut == enShaonNochachut.enNetzer.GetHashCode().ToString() || oSidur.sShaonNochachut == enShaonNochachut.enGrira.GetHashCode().ToString()));
                    }
                    else
                    {//סידור רגיל
                        if (drSugSidur.Length > 0)
                        {
                            bSidurZakaiLenesiot = (drSugSidur[0]["shaon_nochachut"].ToString() == enShaonNochachut.enMinhal.GetHashCode().ToString() || drSugSidur[0]["shaon_nochachut"].ToString() == enShaonNochachut.enNetzer.GetHashCode().ToString() || drSugSidur[0]["shaon_nochachut"].ToString() == enShaonNochachut.enGrira.GetHashCode().ToString());
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 0, oSidur.iMisparIshi,oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurZakaiLeNesiot: " + ex.Message, null);
                _bSuccsess = false;
            }
            return bSidurZakaiLenesiot;
        }

        private bool IsSidurHalbasha(SidurDM oSidur)
        {
            bool bSidurZakaiLHalbasha = false;

            try
            {
                //TRUE הפונקציה תחזיר אם הסידור זכאי להלבשה 
                //ערך 1 במאפיין 15 זכאי לזמן הלבשה במאפייני סידורים מיוחדים, לא רלוונטי לסידורים רגילים
                if (oSidur.bSidurMyuhad)
                {//סידור מיוחד
                    bSidurZakaiLHalbasha = (oSidur.iLoLetashlum==0 && (oSidur.bHalbashKodExists) && (oSidur.sHalbashKod == clGeneral.enMeafyen15.enZakai.GetHashCode().ToString()));
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 0, oSidur.iMisparIshi,oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurHalbasha: " + ex.Message, null);
                _bSuccsess = false;
            
            }
            return bSidurZakaiLHalbasha;
        }
        private bool IsNotSidurHalbasha(SidurDM oSidur)
        {
            bool bSidurLoZakaiLHalbasha = false;

            try
            {
                //TRUE הפונקציה תחזיר אם הסידור לא זכאי להלבשה 
                //ערך 2 במאפיין 15 זכאי לזמן הלבשה במאפייני סידורים מיוחדים, לא רלוונטי לסידורים רגילים
                if (oSidur.bSidurMyuhad)
                {//סידור מיוחד
                    bSidurLoZakaiLHalbasha = ((oSidur.bHalbashKodExists) && (oSidur.sHalbashKod == clGeneral.enMeafyen15.enLoZakai.GetHashCode().ToString()));
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 0, oSidur.iMisparIshi,oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsNotSidurHalbasha: " + ex.Message, null);
                _bSuccsess = false;
            }
            return bSidurLoZakaiLHalbasha;
        }
        private void UpdateHashlamaForYomAvoda(bool bHeadrutMachalaMiluimTeuna,  int iHeadrutTypeKod)
        {
            //עדכון שדה השלמה ברמת יום עבודה
            float fTimeToCompare = 0;
            float fTotalSidrimTime=0;
            try
            {
                //oObjYameyAvodaUpd = new OBJ_YAMEY_AVODA_OVDIM();
                //InsertToYameyAvodaForUpdate(ref oObjYameyAvodaUpd, ref oOvedYomAvodaDetails);
                //ברירת מחדל
               
                oObjYameyAvodaUpd.HASHLAMA_LEYOM = 0;
                oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
               
               
                //.  יום עבודה + לפחות סידור אחד הוא מחלה/מילואים/תאונה (רק סידורים מיוחדים עם ערך 2, 3, 4 בהתאמה במאפיין של היעדרות) 
                //if (bHeadrutMachalaMiluimTeuna)
                //{
                //    oObjYameyAvodaUpd.HASHLAMA_LEYOM = 1;
                //    oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                //    if (iHeadrutTypeKod == enMeafyenSidur53.enMachala.GetHashCode())
                //    { oObjYameyAvodaUpd.SIBAT_HASHLAMA_LEYOM = 2; }
                //    else if (iHeadrutTypeKod == enMeafyenSidur53.enMilueim.GetHashCode())
                //    { oObjYameyAvodaUpd.SIBAT_HASHLAMA_LEYOM = 3; }
                //    else if (iHeadrutTypeKod == enMeafyenSidur53.enTeuna.GetHashCode())
                //    { oObjYameyAvodaUpd.SIBAT_HASHLAMA_LEYOM = 4; }
                //}
                //else
                //{
                    //עבור מותאם בנהגות (מזהים לפי ערך 2 או 3 בקוד נתון 8 (ערך מותאמות)  בטבלת פרטי עובד) שהוא מותאם לזמן קצר (מזהים מותאם לזמן קצר לפי קיום ערך בפרמטר 20 (זמן מותאמות) בטבלת פרטי עובדים) שעבד ביום (לוקחים את כל זמני הסידורים,  מחשבים גמר פחות התחלה וסוכמים) פחות מזמן המותאמות שלו (הערך בפרמטר 20) וההפרש בין זמן העבודה לזמן המותאמות קטן מהערך בפרמטר 153 (מינימום השלמה חריגה למותאם בנהגות).
                    if ((oOvedYomAvodaDetails.sMutamut == clGeneral.enMutaam.enMutaam2.GetHashCode().ToString()) || (oOvedYomAvodaDetails.sMutamut == clGeneral.enMutaam.enMutaam3.GetHashCode().ToString()))
                    {
                        SidurDM oSidur;
                        if (htEmployeeDetails != null)
                        {
                            for (int i = 0; i < htEmployeeDetails.Count; i++)
                            {
                                oSidur = (SidurDM)htEmployeeDetails[i];
                                //(נסכום את זמני הסידורים 
                                //רק עבור עובדים שיש להם קוד נתון 8 עם רק 2,3
                                if ((oOvedYomAvodaDetails.sMutamut == clGeneral.enMutaam.enMutaam2.GetHashCode().ToString()) || (oOvedYomAvodaDetails.sMutamut == clGeneral.enMutaam.enMutaam3.GetHashCode().ToString()))
                                {
                                    fTotalSidrimTime = fTotalSidrimTime + clDefinitions.GetSidurTimeInMinuts(oSidur);
                                }
                            }
                        }
                        //נקח את זמן המותאמות מפרמטר 20
                        if (fTotalSidrimTime < oOvedYomAvodaDetails.iZmanMutamut)
                        {
                            fTimeToCompare = oOvedYomAvodaDetails.iZmanMutamut - fTotalSidrimTime;
                            //מינימום השלמה חריגה למותאם בנהגות - נשווה את ההפרש לפרמטר 153
                            if (fTimeToCompare < oParam.iMinHashlamaCharigaForMutamutDriver)
                            {
                                //נשלים שעה
                                oObjYameyAvodaUpd.HASHLAMA_LEYOM = 1;
                                oObjYameyAvodaUpd.SIBAT_HASHLAMA_LEYOM = 1;
                                oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                            }
                        }
                    }
                //}
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", 0, "UpdateHashlamaForYomAvoda: " + ex.Message);
                _bSuccsess = false;
            }
        }

    
        private bool IsHeadrutMachalaMiluimTeuna(SidurDM oSidur, ref int iHeadrutTypeKod)
        {
            bool HeadrutMachalaMiluimTeuna = false;

            //הפונקציה תחזיר  אם הסידור הוא סידור העדרות מסוג מחלה/מילואים/תאונה  TRUE

            try
            {
                if (oSidur.bSidurMyuhad)
                {//סידור מיוחד
                    if (!string.IsNullOrEmpty(oSidur.sHeadrutTypeKod))
                    {
                      if ((oSidur.sHeadrutTypeKod == enMeafyenSidur53.enMachala.GetHashCode().ToString()) ||
                                                    (oSidur.sHeadrutTypeKod == enMeafyenSidur53.enMilueim.GetHashCode().ToString()) ||
                                                    (oSidur.sHeadrutTypeKod == enMeafyenSidur53.enTeuna.GetHashCode().ToString()))
                        {
                            HeadrutMachalaMiluimTeuna=true;
                            iHeadrutTypeKod = int.Parse(oSidur.sHeadrutTypeKod);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 0, oSidur.iMisparIshi,oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsHeadrutMachalaMiluimTeuna: " + ex.Message, null);
                _bSuccsess = false;
            }

            return HeadrutMachalaMiluimTeuna;
        }

        private bool IsSidurHeadrut(SidurDM oSidur)
        {
            bool bSidurHeadrut = false;
            try{
            //הפונקציה תחזיר  אם הסידור הוא סידור העדרות מסוג מחלה/מילואים/תאונה  TRUE

            try
            {
                if (oSidur.bSidurMyuhad)
                {//סידור מיוחד
                    bSidurHeadrut = !string.IsNullOrEmpty(oSidur.sHeadrutTypeKod);
                    
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 0, oSidur.iMisparIshi,oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurHeadrut: " + ex.Message, null);
                _bSuccsess = false;
            }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bSidurHeadrut;
        }

      
        private bool IsSidurShaon(SidurDM oSidur)
        {
            bool bSidurShaonKnisa = false;
            // אם יש החתמת שעון תקינה או ידנית עם אישור TRUE הפונקציה תחזיר 
            //נבדוק אם סידור שעון
            //מזהים סידור שעונים לפי ערך 1 (שעונים) במאפיין 52 (סוג עבודה) בטבלת סידורים מיוחדים

            //מזהים סידור שעונים לפי ערך 1 (שעונים) במאפיין 52 (סוג עבודה) בטבלת סידורים מיוחדים) ויש  רק החתמת כניסה בשעון . זיהוי החתמת שעון כניסה, שני מקרים שונים: 
            //א. החתמת שעון - קיום ערך בשדה שעת התחלה של הסידור הנבדק  וקיום ערך בשדה מיקום שעון כניסה.
            //ב.  דיווח ידני של שעת כניסה - קיום ערך בשדה שעת התחלה של הסידור הנבדק  וקיום סיבת דיווח ידני שמאפשרת מתן זמן הלבשה (לוקחים את קוד סיבה לדיווח ידני ובודקים בטבלת סיבות לדיווח ידני האם אין לו ערך 1 בשדה Goremet_Lebitul_Zman_Nesiaa)  וקיום אישור לדיווח החתמת שעון (קוד אישור 1 או 3 עם סטטוס אישור ברמה הכי גבוהה). 

            try
            {
                //סידור מיוחד
                bSidurShaonKnisa = ((oSidur.bSidurMyuhad) && ((oSidur.sSugAvoda == enSugAvoda.Shaon.GetHashCode().ToString())));
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 0, oSidur.iMisparIshi, oSidur.dFullShatHatchala.Date, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurShaon: " + ex.Message, null);
                _bSuccsess = false;
            }

            return bSidurShaonKnisa;
        }


     
        private bool IsKnisaValid( SidurDM oSidur, string sBitulTypeField,bool bSidurHaveNahagut)
        {
            bool bKnisaValid = false;
            bool bSidurShaonNotValid = false;
            try
            {
                if (!String.IsNullOrEmpty(oSidur.sShatHatchala))
                {  //אם הוחתם שעון וגם יש ערך במיקום כניסה - החתמת שעון ידנית 
                    if (!String.IsNullOrEmpty(oSidur.sMikumShaonKnisa) && oSidur.sMikumShaonKnisa != "0")
                    {
                        bKnisaValid = true;
                    }
                    else if ((sBitulTypeField == SIBA_LE_DIVUCH_YADANI_NESIAA && oMeafyeneyOved.IsMeafyenExist(51)) || (sBitulTypeField != SIBA_LE_DIVUCH_YADANI_NESIAA))
                    {

                        if ((!String.IsNullOrEmpty(oSidur.sShatHatchala)) && (String.IsNullOrEmpty(oSidur.sMikumShaonKnisa) || oSidur.sMikumShaonKnisa == "0"))
                        {  //אם הוחתם שעון ואין ערך במיקום כניסה - החתמת שעון ידנית                                                 
                            bSidurShaonNotValid = IsIshurYadaniValid(oSidur.iKodSibaLedivuchYadaniIn, sBitulTypeField);
                        }
                        if (bSidurShaonNotValid)
                        {
                            //קיום אישור לדיווח החתמת שעון (קוד אישור 1 או 3 עם סטטוס אישור 1 (מאושר)).
                            if (sBitulTypeField == SIBA_LE_DIVUCH_YADANI_NESIAA)
                            {
                                if (!bSidurHaveNahagut)// && CheckApprovalStatus("111,1,3,101,301", oSidur.iMisparSidur, oSidur.dFullShatHatchala) == 1)
                                    bKnisaValid = true;
                            }
                            else // if (CheckApprovalStatus("111,1,3,101,301", oSidur.iMisparSidur, oSidur.dFullShatHatchala) == 1)
                                bKnisaValid = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 0, oSidur.iMisparIshi,oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsKnisaValid: " + ex.Message, null);
                _bSuccsess = false;
            }
            return bKnisaValid;
        }

        private bool IsYetizaValid( SidurDM oSidur, string sBitulTypeField,bool bSidurHaveNahagut)
        {
            bool bYetizaValid = false;
            bool bSidurShaonNotValid = false;
            try
            {
                if (!String.IsNullOrEmpty(oSidur.sShatGmar))
                {  //אם הוחתם שעון וגם יש ערך במיקום יציאה - החתמת שעון ידנית 
                    if (!String.IsNullOrEmpty(oSidur.sMikumShaonYetzia) && oSidur.sMikumShaonYetzia!="0")
                    {
                        bYetizaValid = true;
                    }
                    else if ((sBitulTypeField==SIBA_LE_DIVUCH_YADANI_NESIAA && oMeafyeneyOved.IsMeafyenExist(51)) ||(sBitulTypeField!=SIBA_LE_DIVUCH_YADANI_NESIAA))
                    {
                       if ((!String.IsNullOrEmpty(oSidur.sShatGmar)) && (String.IsNullOrEmpty(oSidur.sMikumShaonYetzia) || oSidur.sMikumShaonYetzia == "0"))
                                {  //אם הוחתם שעון וגם אין ערך במיקום יציאה - החתמת שעון ידנית                                                 

                                    bSidurShaonNotValid = IsIshurYadaniValid(oSidur.iKodSibaLedivuchYadaniOut, sBitulTypeField);
                        }

                       if (bSidurShaonNotValid)
                       {
                           //קיום אישור לדיווח החתמת שעון (קוד אישור 1 או 3 עם סטטוס אישור 1 (מאושר)).
                           if (sBitulTypeField==SIBA_LE_DIVUCH_YADANI_NESIAA)
                            {
                                if (!bSidurHaveNahagut)// &&(CheckApprovalStatus("111,1,3,102,302", oSidur.iMisparSidur, oSidur.dFullShatHatchala) == 1))
                                        bYetizaValid = true;
                           }
                           else  //if (CheckApprovalStatus("111,1,3,102,302", oSidur.iMisparSidur, oSidur.dFullShatHatchala) == 1)
                          bYetizaValid = true;
                          
                       }   
                       
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 0, oSidur.iMisparIshi,oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsYetizaValid: " + ex.Message, null);
                _bSuccsess = false;
            }
            return bYetizaValid;
        }

        private clNewSidurim FindSidurOnHtNewSidurim(int iMisparSidur,DateTime dShatHatchala)
        {
            clNewSidurim oNewSidurim=null;
            clNewSidurim oSidur;
            for (int i = 0; i < htNewSidurim.Count; i++)
            {
                oSidur = (clNewSidurim)htNewSidurim[i];
                if (oSidur.SidurNew == iMisparSidur && oSidur.ShatHatchalaNew == dShatHatchala)
                {
                    oNewSidurim = oSidur;
                    break;
                }
            }

            if (oNewSidurim == null)
            {
                oNewSidurim = new clNewSidurim();
                oNewSidurim.ShatHatchalaOld = dShatHatchala;
                oNewSidurim.SidurOld = iMisparSidur;
                       
                htNewSidurim.Add(string.Concat(dShatHatchala.ToString("HH:mm:ss").Replace(":", ""), iMisparSidur), oNewSidurim);
           
            }
            return oNewSidurim;
        }
               

        private bool IsIshurYadaniValid(int iKodSibaLedivuckYadani, string sBitulTypeField)
        {
            bool bHasIshur = false;
            try
            {
                DataRow[] drSidurSibotLedivuchYadani;
                drSidurSibotLedivuchYadani = clDefinitions.GetOneSibotLedivuachYadani(iKodSibaLedivuckYadani, ref dtSibotLedivuachYadani);
                if (drSidurSibotLedivuchYadani.Length > 0)
                {
                    if (!System.Convert.IsDBNull(drSidurSibotLedivuchYadani[0][sBitulTypeField]))
                    {
                        if (int.Parse(drSidurSibotLedivuchYadani[0][sBitulTypeField].ToString()) != 1)
                        {
                            bHasIshur = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bHasIshur;
        }

        private void UpdateBitulZmanNesiot(bool bSidurShaon,
                                          bool bSidurNahagut,
                                           bool bFirstSidurZakaiLenesiot,
                                           DateTime dCardDate)
        {
            int iZmanNesia = 0;
            bool bSidurZakaiLnesiot = false, bSidurMezake = false; 
            DataRow[] drSugSidur;
            SidurDM oSidur;
            bool bKnisaValid = false;
            bool bYetizaValid = false;
            int iSidurZakaiLenesiaKnisa = -1;
            int iSidurZakaiLenesiaYetzia = -1;
            int iFirstMezake = -1, iLastMezake = -1;
            string sMefyen14 = "";
            //עדכון שדה ביטול זמן נסיעות ברמת יום עבודה
            try
            {
                //אם אין מאפיין נסיעות (51, 61) - נעדכן ל0- 
               
                 if (oOvedYomAvodaDetails.iKodHevra == enEmployeeType.enEggedTaavora.GetHashCode() && (!oMeafyeneyOved.IsMeafyenExist(51)) && (!oMeafyeneyOved.IsMeafyenExist(61)))
                {
                    if (!CheckIdkunRashemet("BITUL_ZMAN_NESIOT"))
                    {
                        oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = ZmanNesiotType.LoZakai.GetHashCode();
                        oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                    }
                        if (!CheckIdkunRashemet("ZMAN_NESIA_HALOCH"))
                    {
                        oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = 0;
                        oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                    }
                    if (!CheckIdkunRashemet("ZMAN_NESIA_HAZOR"))
                    {
                        oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = 0;
                        oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                    }
                }
                 else if ((!oMeafyeneyOved.IsMeafyenExist(51)) && (!oMeafyeneyOved.IsMeafyenExist(61)))
                 {
                     if (!CheckIdkunRashemet("BITUL_ZMAN_NESIOT"))
                     {
                         oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = 0;
                         oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                     }
                     if (!CheckIdkunRashemet("ZMAN_NESIA_HALOCH"))
                     {
                         oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = 0;
                         oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                     }
                     if (!CheckIdkunRashemet("ZMAN_NESIA_HAZOR"))
                     {
                         oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = 0;
                         oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                     }
                 }
                 //else if ((oMeafyeneyOved.IsMeafyenExist(61) || oMeafyeneyOved.IsMeafyenExist(51)) && !bSidurShaon)
                 //{
                 //    if (!CheckIdkunRashemet("BITUL_ZMAN_NESIOT")){
                 //        oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = ZmanNesiotType.LoZakai.GetHashCode();
                 //        oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                 //    }
                 //    if (!CheckIdkunRashemet("ZMAN_NESIA_HALOCH"))
                 //    {
                 //        oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = 0;
                 //        oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                 //    }
                 //    if (!CheckIdkunRashemet("ZMAN_NESIA_HAZOR"))
                 //    {
                 //        oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = 0;
                 //        oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                 //    }

                  //    if (oMeafyeneyOved.IsMeafyenExist(51) && CheckIdkunRashemet("BITUL_ZMAN_NESIOT"))
                 //    {
                 //        iZmanNesia = int.Parse(oMeafyeneyOved.GetMeafyen(51).Value.ToString().PadRight(3, char.Parse("0")).Substring(1));
                 //        switch (int.Parse(oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT.ToString()))
                 //        {
                 //            case 1:
                 //                if (!CheckIdkunRashemet("ZMAN_NESIA_HALOCH"))
                 //                    oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = (int)(Math.Ceiling(iZmanNesia / 2.0));
                 //                break;
                 //            case 2:
                 //                if (!CheckIdkunRashemet("ZMAN_NESIA_HAZOR"))
                 //                    oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = (int)(Math.Ceiling(iZmanNesia / 2.0));
                 //                break;
                 //            case 3:
                 //                if (!CheckIdkunRashemet("ZMAN_NESIA_HALOCH"))
                 //                    oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = (int)(Math.Ceiling(iZmanNesia / 2.0));
                 //                if (!CheckIdkunRashemet("ZMAN_NESIA_HAZOR"))
                 //                    oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = (int)(Math.Ceiling(iZmanNesia / 2.0));
                 //                break;
                 //        }
                 //    }
                 //}
                 else
                 {
                     //לעובד מאפיין 51/61 (מאפיין זמן נסיעות) והעובד זכאי רק לזמן נסיעות לעבודה (ערך 1 בספרה הראשונה של מאפיין זמן נסיעות
                     //וגם לפחות אחד הסידורים מזכה בזמן נסיעה (סידור מזכה בזמן נסיעות אם יש לו ערך 1 (זכאי) במאפיין 14 (זכאות לזמן נסיעה) בטבלת סידורים מיוחדים/מאפייני סוג סידור
                     if (!CheckIdkunRashemet("ZMAN_NESIA_HALOCH"))
                     {
                         oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = 0;
                         oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                     }
                     if (!CheckIdkunRashemet("ZMAN_NESIA_HAZOR"))
                     {
                         oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = 0;
                         oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                     }
                     //ברגע שמצאנו סידור אחד לפחות שזכאי לזמן נסיעות, נפסיק לחפש
                     if (htEmployeeDetails != null)
                     {
                         for (int i = 0; i < htEmployeeDetails.Count; i++)
                         {
                             oSidur = (SidurDM)htEmployeeDetails[i];
                             drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, dCardDate, _dtSugSidur);

                             bSidurZakaiLnesiot = IsSidurShonim(drSugSidur, oSidur);

                             sMefyen14=oSidur.sZakayLezamanNesia;
                             if (!oSidur.bSidurMyuhad && drSugSidur.Length>0) sMefyen14 = drSugSidur[0]["zakay_leaman_nesia"].ToString();

                             if (!bSidurZakaiLnesiot && sMefyen14 == "1" && oSidur.iLoLetashlum == 0)
                             {
                                 if (!(bSidurMezake))
                                 { 
                                     iFirstMezake = i;
                                     bSidurMezake = true;
                                 }
                                 iLastMezake = i;
                             }
                             else if (bSidurZakaiLnesiot && sMefyen14 == "1")
                             {
                                 if (!(bSidurMezake))
                                 { 
                                     iFirstMezake = i;
                                     bSidurMezake = true;
                                 }
                                 iLastMezake = i;

                                 if (iFirstMezake > -1 && iFirstMezake==i)
                                 {
                                     bKnisaValid = IsKnisaValid((SidurDM)htEmployeeDetails[iFirstMezake], SIBA_LE_DIVUCH_YADANI_NESIAA, bSidurNahagut);
                                     if (!bKnisaValid)
                                         iFirstMezake=-1;
                                 }
                                 if (iLastMezake > -1 && iLastMezake == i)
                                 {
                                     bYetizaValid = IsYetizaValid((SidurDM)htEmployeeDetails[iLastMezake], SIBA_LE_DIVUCH_YADANI_NESIAA, bSidurNahagut);
                                     if (!bYetizaValid)
                                         iLastMezake = -1;
                                 }
                             }

                             //if (bSidurZakaiLnesiot || oSidur.sZakayLezamanNesia == "1")
                             //{
                             //    bKnisaValid = IsKnisaValid(ref oSidur, SIBA_LE_DIVUCH_YADANI_NESIAA, bSidurNahagut);
                             //    if ((bKnisaValid && iSidurZakaiLenesiaKnisa == -1) ||(oSidur.sZakayLezamanNesia == "1" && !String.IsNullOrEmpty(oSidur.sShatHatchala) && iSidurZakaiLenesiaKnisa == -1))
                             //        iSidurZakaiLenesiaKnisa = i;
                             //    bYetizaValid = IsYetizaValid(ref oSidur, SIBA_LE_DIVUCH_YADANI_NESIAA, bSidurNahagut);
                             //    if (bYetizaValid || oSidur.sZakayLezamanNesia == "1")
                             //        iSidurZakaiLenesiaYetzia = i;
                             //    else iSidurZakaiLenesiaYetzia = -1;
                             //}

                         }


                         if (iFirstMezake == -1 && iLastMezake == -1 && !CheckIdkunRashemet("BITUL_ZMAN_NESIOT"))
                         {
                              oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = ZmanNesiotType.LoZakai.GetHashCode();
                         }
                         else
                         {
                             iSidurZakaiLenesiaKnisa = iFirstMezake;
                             iSidurZakaiLenesiaYetzia = iLastMezake;
                             //לעובד מאפיין 51/61 (מאפיין זמן נסיעות) והעובד זכאי רק לזמן נסיעות לעבודה (ערך 1 בספרה הראשונה של מאפיין זמן נסיעות
                             //עובד זכאי לנסיעות לעבודה
                             if (IsOvedZakaiLZmanNesiaLaAvoda() || IsOvedZakaiLZmanNesiaLeMeAvoda())
                             {
                                 //לפחות אחד הסידורים מזכה בזמן נסיעה (סידור מזכה בזמן נסיעות אם יש לו ערך 1 (זכאי) במאפיין 14 (זכאות לזמן נסיעה) בטבלת סידורים מיוחדים/מאפייני סוג סידור
                                 if (iSidurZakaiLenesiaKnisa > -1 || (CheckIdkunRashemet("BITUL_ZMAN_NESIOT") && oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT > 0))
                                 {
                                     if (iSidurZakaiLenesiaKnisa > -1)
                                     {
                                         if (!CheckIdkunRashemet("BITUL_ZMAN_NESIOT"))
                                             oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = ZmanNesiotType.ZakaiKnisa.GetHashCode();

                                         OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;

                                         oSidur = (SidurDM)htEmployeeDetails[iSidurZakaiLenesiaKnisa];
                                         oObjSidurimOvdimUpd = GetSidurOvdimObject(oSidur.iMisparSidur, oSidur.dFullShatHatchala);

                                         oObjSidurimOvdimUpd.MEZAKE_NESIOT = ZmanNesiotType.ZakaiKnisa.GetHashCode();
                                         oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;

                                     }
                                     //עבור מאפיין 51: 
                                     //אם שדה נסיעות התעדכן בערך 1, אז יש לעדכן את שדה זמן נסיעה הלוך בטבלת ימי עבודה עובדים בערך הזמן ממאפיין 51
                                     if (IsOvedZakaiLZmanNesiaLaAvoda() && (!CheckIdkunRashemet("ZMAN_NESIA_HALOCH")))
                                     {
                                         if ((oMeafyeneyOved.IsMeafyenExist(61)) && iSidurZakaiLenesiaKnisa > -1)
                                         {
                                             //עבור מאפיין 61:
                                             //אם שדה נסיעות התעדכן בערך 1 ויש ערך בשדה מיקום שעון כניסה בסידור הראשון ביום, יש לעדכן את שדה זמן נסיעה הלוך בערך מטבלה זמן נסיעה משתנה.                                        
                                             iZmanNesia = GetZmanNesiaMeshtana(iSidurZakaiLenesiaKnisa, 1, dCardDate);
                                             if (iZmanNesia > -1)
                                             { oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = (int)(Math.Ceiling(iZmanNesia / 2.0)); }
                                         }
                                         if (oMeafyeneyOved.IsMeafyenExist(51))
                                         {
                                             iZmanNesia = int.Parse(oMeafyeneyOved.GetMeafyen(51).Value.Substring(1));
                                             if (iZmanNesia > -1)
                                             {
                                                 oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = (int)(Math.Ceiling(iZmanNesia / 2.0));
                                             }
                                         }
                                     }
                                 }
                             }
                             //עובד זכאי לנסיעות מהעבודה
                             if (IsOvedZakaiLZmanNesiaMeAvoda() || IsOvedZakaiLZmanNesiaLeMeAvoda())
                             {
                                 if (iSidurZakaiLenesiaYetzia > -1 || (CheckIdkunRashemet("BITUL_ZMAN_NESIOT") && oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT > 0))
                                 {
                                     if (iSidurZakaiLenesiaYetzia > -1)
                                     {
                                         if (!CheckIdkunRashemet("BITUL_ZMAN_NESIOT"))
                                             oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = ZmanNesiotType.ZakaiYetiza.GetHashCode();

                                         //אם הסידור הראשון זכאי לנסיעות, נעדכן את שדה MEZAKE_NESIOT 


                                         OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;

                                         oSidur = (SidurDM)htEmployeeDetails[iSidurZakaiLenesiaYetzia];
                                         oObjSidurimOvdimUpd = GetSidurOvdimObject(oSidur.iMisparSidur, oSidur.dFullShatHatchala);

                                         oObjSidurimOvdimUpd.MEZAKE_NESIOT = ZmanNesiotType.ZakaiYetiza.GetHashCode();
                                         oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                                     }
                                     if (IsOvedZakaiLZmanNesiaMeAvoda() && (!CheckIdkunRashemet("ZMAN_NESIA_HAZOR")))
                                     {
                                         if ((oMeafyeneyOved.IsMeafyenExist(61)) && (htEmployeeDetails.Count > 0) && iSidurZakaiLenesiaYetzia > -1)
                                         {
                                             //נשלוף את הסידור האחרון
                                             iZmanNesia = GetZmanNesiaMeshtana(iSidurZakaiLenesiaYetzia, 2, dCardDate);
                                             oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = (int)(Math.Ceiling(iZmanNesia / 2.0));
                                         }
                                         if (oMeafyeneyOved.IsMeafyenExist(51))
                                         {
                                             iZmanNesia = int.Parse(oMeafyeneyOved.GetMeafyen(51).Value.ToString().PadRight(3, char.Parse("0")).Substring(1));
                                             oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = (int)(Math.Ceiling(iZmanNesia / 2.0));
                                         }
                                     }
                                 }
                             }

                             //עובד זכאי לנסיעות מהעבודה ולעבודה
                             if (IsOvedZakaiLZmanNesiaLeMeAvoda())
                             {
                                 if ((iSidurZakaiLenesiaYetzia > -1 && iSidurZakaiLenesiaKnisa > -1) || (CheckIdkunRashemet("BITUL_ZMAN_NESIOT") && oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT > 0))
                                 {
                                     if (iSidurZakaiLenesiaYetzia > -1 && iSidurZakaiLenesiaKnisa > -1)
                                     {
                                         if (!CheckIdkunRashemet("BITUL_ZMAN_NESIOT"))
                                             oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = ZmanNesiotType.ZakaiKnisaYetiza.GetHashCode();

                                         //אם ביום קיים סידור אחד בלבד  ולפי הסידור מגיע גם נסיעות כניסה וגם נסיעות יציאה - יש לעדכן את השדה "נסיעות" ברמת סידור העבודה בקוד - זכאי לנסיעות לכניסה/יציאה לעבודה.
                                         if (iSidurZakaiLenesiaYetzia > -1 && iSidurZakaiLenesiaKnisa == iSidurZakaiLenesiaYetzia)
                                         {
                                             OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
                                             oSidur = (SidurDM)htEmployeeDetails[iSidurZakaiLenesiaKnisa];
                                             oObjSidurimOvdimUpd = GetSidurOvdimObject(oSidur.iMisparSidur, oSidur.dFullShatHatchala);

                                             oObjSidurimOvdimUpd.MEZAKE_NESIOT = ZmanNesiotType.ZakaiKnisaYetiza.GetHashCode();
                                             oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                                         }
                                     }
                                 }
                                 if ((oMeafyeneyOved.IsMeafyenExist(61)) && (htEmployeeDetails.Count > 0) && (iSidurZakaiLenesiaKnisa > -1 || iSidurZakaiLenesiaYetzia > -1))
                                 {
                                     if ((oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT == 1 || oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT == 3) && iSidurZakaiLenesiaKnisa > -1 && (!CheckIdkunRashemet("ZMAN_NESIA_HALOCH")))
                                     {
                                         iZmanNesia = GetZmanNesiaMeshtana(iSidurZakaiLenesiaKnisa, 1, dCardDate);
                                         if (iZmanNesia > -1)
                                             oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = (int)(Math.Ceiling(iZmanNesia / 2.0));

                                     }
                                     if ((oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT == 2 || oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT == 3) &&  iSidurZakaiLenesiaYetzia > -1 && (!CheckIdkunRashemet("ZMAN_NESIA_HAZOR")))
                                     {
                                         iZmanNesia = GetZmanNesiaMeshtana(iSidurZakaiLenesiaYetzia, 2, dCardDate);
                                         if (iZmanNesia > -1)
                                             oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = (int)(Math.Ceiling(iZmanNesia / 2.0));
                                     }
                                 }

                                 if (oMeafyeneyOved.IsMeafyenExist(51))
                                 {
                                     iZmanNesia = int.Parse(oMeafyeneyOved.GetMeafyen(51).Value.ToString().PadRight(3, char.Parse("0")).Substring(1));
                                     // ((iSidurZakaiLenesiaKnisa > -1 || CheckIdkunRashemet("BITUL_ZMAN_NESIOT")) &&
                                     if ((oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT == 1 || oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT == 3) && (!CheckIdkunRashemet("ZMAN_NESIA_HALOCH")))
                                         oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = (int)(Math.Ceiling(iZmanNesia / 2.0));
                                     if ((oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT == 2 || oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT == 3) && (!CheckIdkunRashemet("ZMAN_NESIA_HAZOR")))
                                         oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = (int)(Math.Ceiling(iZmanNesia / 2.0));
                                 }

                             }
                         }
                         if (!CheckIdkunRashemet("BITUL_ZMAN_NESIOT") && oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT != 1 && oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT != 2 && oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT != 3)
                             oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = ZmanNesiotType.LoZakai.GetHashCode();
                     }
                 }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, _iMisparIshi, "E", 0, dCardDate, "UpdateBitulZmanNesiot: " + ex.Message);
                _bSuccsess = false;
            }
        }

        private int GetZmanNesiaMeshtana(int iSidurIndex, int iType, DateTime dCardDate)
        {
            int iZmanNesia = 0;
            int iMerkazErua = 0;
            int iMikumYaad = 0;
            clUtils oUtils = new clUtils();

            //נשלוף את הסידור 
           SidurDM oSidur;
           try
           {
               if (htEmployeeDetails.Count > 0)
               {
                   oSidur = (SidurDM)htEmployeeDetails[iSidurIndex];

                   //עבור מאפיין 61:
                   if (iType == 1) //כניסה
                   {
                       if (oSidur.sMikumShaonKnisa.Length > 0)
                       {
                           iMikumYaad = int.Parse(oSidur.sMikumShaonKnisa);
                       }
                   }
                   else //יציאה
                   {
                       if (oSidur.sMikumShaonYetzia.Length > 0)
                       {
                           iMikumYaad = int.Parse(oSidur.sMikumShaonYetzia);
                       }
                   }

                   iMerkazErua = (String.IsNullOrEmpty(oOvedYomAvodaDetails.sMercazErua) ? 0 : int.Parse(oOvedYomAvodaDetails.sMercazErua));
                   if ((iMerkazErua > 0) && (iMikumYaad > 0))
                   {
                       iZmanNesia = oUtils.GetZmanNesia(iMerkazErua, iMikumYaad, dCardDate);
                   }
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
            return iZmanNesia;
        }

        private void UpdateHalbasha( DateTime dCardDate)
        {
            //float fDakotInTafkid = 0;
            // bool bStatusChishuv=false;
            bool bSidurZakaiLHalbash = false, bSidurMezake=false;
            bool bSidurMisugShaonim = false;
            int iSidurZakaiLehalbashaKnisa = -1;
            int iSidurZakaiLehalbashaYetzia = -1;
            int iMutamut;
            bool bSidurLoZakaiLHalbash = false;
            DataRow[] drSugSidur;
            SidurDM oSidur;
            bool bHaveHalbasha = false;
            bool bKnisaValid = false;
            bool bYetizaValid=false;
            //עדכון שדה הלבשה ברמת יום עבודה
            try
            {
                //הלבשה

                if (oOvedYomAvodaDetails.iKodHevra == enEmployeeType.enEggedTaavora.GetHashCode())
                {
                    //עובד של אגד תעבורה לא זכאי אף פעם, גם אם הכרטיס שגוי וגם אם לא
                    if (!CheckIdkunRashemet("HALBASHA"))
                    {
                        oObjYameyAvodaUpd.HALBASHA = ZmanHalbashaType.LoZakai.GetHashCode();
                        oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                    }
                }
                else
                {
                    //אם אין לעובד מאפיין הלבשה, נעדכן 0-
                    if (!oMeafyeneyOved.IsMeafyenExist(44))
                    {
                        if (!CheckIdkunRashemet("HALBASHA"))
                        {
                            oObjYameyAvodaUpd.HALBASHA = 0;
                            oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                        }
                    }
                    else
                    { //קיים מאפיין 44 לעובד, זכאות להלבשה
                        if (htEmployeeDetails != null)
                        {
                            for (int i = 0; i < htEmployeeDetails.Count; i++)
                            {
                                oSidur = (SidurDM)htEmployeeDetails[i];
                                drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, dCardDate, _dtSugSidur);

                                bSidurZakaiLHalbash = IsSidurHalbasha(oSidur);
                                bSidurMisugShaonim = IsSidurShonim(drSugSidur, oSidur);

                                if (oSidur.iLoLetashlum == 0 && !bSidurMisugShaonim && oSidur.sHalbashKod == "1")
                                {
                                    if (!(bSidurMezake))
                                    {
                                        iSidurZakaiLehalbashaKnisa = i;
                                        bSidurMezake = true;
                                    }
                                   
                                    iSidurZakaiLehalbashaYetzia = i;
                                }
                                else if (oSidur.iLoLetashlum == 0 && bSidurMisugShaonim && oSidur.sHalbashKod == "1")
                                {
                                    if (!(bSidurMezake))
                                    {
                                        iSidurZakaiLehalbashaKnisa = i;
                                        bSidurMezake = true;
                                    }
                                    iSidurZakaiLehalbashaYetzia = i;

                                    if (iSidurZakaiLehalbashaKnisa > -1 && iSidurZakaiLehalbashaKnisa == i)
                                    {
                                        bKnisaValid = IsKnisaValid(oSidur, SIBA_LE_DIVUCH_YADANI_HALBASHA, false);
                                        if (!bKnisaValid)
                                            iSidurZakaiLehalbashaKnisa = -1;
                                        //if (bKnisaValid && iSidurZakaiLehalbashaKnisa == -1)
                                        //    iSidurZakaiLehalbashaKnisa = i;
                                    }
                                    if (iSidurZakaiLehalbashaYetzia > -1 && iSidurZakaiLehalbashaYetzia == i)
                                    {
                                        bYetizaValid = IsYetizaValid(oSidur, SIBA_LE_DIVUCH_YADANI_HALBASHA, false);
                                        if (!bYetizaValid)
                                            iSidurZakaiLehalbashaYetzia = -1;
                                    }
                                }
                                else if (!bSidurLoZakaiLHalbash)
                                { bSidurLoZakaiLHalbash = IsNotSidurHalbasha(oSidur); }
                                ////אם נמצא סידור שזכאי להלבשה, נשמור את האינדקס של הסידור
                                //if (bSidurZakaiLHalbash)
                                //{
                                //    bKnisaValid = IsKnisaValid(ref oSidur, SIBA_LE_DIVUCH_YADANI_HALBASHA, false);
                                //    if ((bKnisaValid && iSidurZakaiLehalbashaKnisa == -1) || (oSidur.sHalbashKod == "1" && !String.IsNullOrEmpty(oSidur.sShatHatchala) && iSidurZakaiLehalbashaKnisa == -1))
                                //        iSidurZakaiLehalbashaKnisa = i;
                                //    bYetizaValid = IsYetizaValid(ref oSidur, SIBA_LE_DIVUCH_YADANI_HALBASHA, false);
                                //    if (bYetizaValid || oSidur.sHalbashKod == "1")
                                //        iSidurZakaiLehalbashaYetzia = i;
                                //    else iSidurZakaiLehalbashaYetzia = -1;
                                //}
                                //else if (!bSidurLoZakaiLHalbash)
                                //{ bSidurLoZakaiLHalbash = IsNotSidurHalbasha(oSidur); }
                            }


                            if (iSidurZakaiLehalbashaKnisa > -1)
                            {
                                if (!CheckIdkunRashemet("HALBASHA"))
                                {
                                    oObjYameyAvodaUpd.HALBASHA = ZmanHalbashaType.ZakaiKnisa.GetHashCode();
                                    oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                                }
                                bHaveHalbasha = true;
                            }

                            if (iSidurZakaiLehalbashaYetzia > -1)
                            {
                                if (!CheckIdkunRashemet("HALBASHA"))
                                {
                                    oObjYameyAvodaUpd.HALBASHA = ZmanHalbashaType.ZakaiYetiza.GetHashCode();
                                    oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                                }
                                bHaveHalbasha = true;
                            }
                            if (iSidurZakaiLehalbashaKnisa > -1 && iSidurZakaiLehalbashaYetzia > -1)
                            {
                                if (!CheckIdkunRashemet("HALBASHA"))
                                {
                                    oObjYameyAvodaUpd.HALBASHA = ZmanHalbashaType.ZakaiKnisaYetiza.GetHashCode();
                                    oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                                }
                                bHaveHalbasha = true;
                            }

                            if (bHaveHalbasha)
                            { //עובד אשר ענה על תנאי זכאות (אחד מהערכים 1-3)

                                // 2. עובד אשר ענה על תנאי זכאות (אחד מהערכים 1-3), אולם עבד ביום  [ (לפי רכיב מחושב 4 (דקות בתפקיד) עבור יום שאינו שבת או לפי רכיב מחושב 37 (דקות שבת בתפקיד) עבור יום שבת/שבתון ] פחות  מהערך בפרמטר 0166 (כרגע 210 דקות) ולא הייתה לו השלמה (השלמה לשעות או השלמה ליום עבודה) אשר השלימה לו את יום העבודה לזמן זה .

                                //Get Calc Rechivim                
                                //dtChishuvYom = objCalc.CalcDayInMonth(_iMisparIshi, _dCardDate, _btchRequest.HasValue ? _btchRequest.Value : 0, out bStatusChishuv);

                                //if (bStatusChishuv)
                                //{
                                //fDakotInTafkid = CalcOvedDakotInYafkid(dCardDate);
                                //if (fDakotInTafkid < oParam.iMinYomAvodaForHalbasha)
                                //{
                                //    if (!CheckIdkunRashemet("HALBASHA"))
                                //    {
                                //        oObjYameyAvodaUpd.HALBASHA = 4;
                                //        oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                                //    }
                                //}
                                if (oOvedYomAvodaDetails.iKodHevra == enEmployeeType.enEggedTaavora.GetHashCode())
                                {
                                    //4. עובד מאגד תעבורה 
                                    if (!CheckIdkunRashemet("HALBASHA"))
                                    {
                                        oObjYameyAvodaUpd.HALBASHA = ZmanHalbashaType.LoZakai.GetHashCode();
                                        oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                                    }
                                }
                                else
                                {
                                    //3. עובד אשר ענה על תנאי זכאות (אחד מהערכים 1-3), אולם הוא מותאם ליום קצר (יודעים שעובד הוא מותאם ליום עבודה קצר לפי שני פרמטרים – העובד מותאם (לפי קיום ערך בפרמטר 8 (קוד עובד מותאם) בטבלת פרטי עובדים ולפי קיום ערך בפרמטר 20 (זמן מותאמות) בטבלת פרטי עובדים) וזמן המותאמות שלו (לפי ערך בפרמטר 20 (זמן מותאמות) בטבלת פרטי עובדים קטן מהערך בפרמטר 0167 (כרגע 300).
                                    if ((!CheckIdkunRashemet("HALBASHA")) && (oOvedYomAvodaDetails.bMutamutExists) && (oOvedYomAvodaDetails.iZmanMutamut < oParam.iMinDakotLemutamLeHalbasha) &&
                                        (oOvedYomAvodaDetails.iZmanMutamut > 0))
                                    {
                                        iMutamut = int.Parse(oOvedYomAvodaDetails.sMutamut);
                                        if ((iMutamut == 1 || iMutamut == 5 || iMutamut == 7))
                                        {
                                            oObjYameyAvodaUpd.HALBASHA = ZmanHalbashaType.LoZakai.GetHashCode();
                                            oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                                        }
                                    }
                                }


                                if (bSidurLoZakaiLHalbash && (!CheckIdkunRashemet("HALBASHA")))
                                {   //1.
                                    //לעובד מאפיין הלבשה (44) + ) ולפחות לסידור אחד יש מאפיין זכאי לזמן הלבשה (ערך 1 במאפיין 15 זכאי לזמן הלבשה במאפייני סידורים מיוחדים, לא רלוונטי לסידורים רגילים) ולא ענה על תנאים 0-3.
                                    oObjYameyAvodaUpd.HALBASHA = ZmanHalbashaType.LoZakai.GetHashCode();
                                    oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                                }
                                bHaveHalbasha = true;
                                //}
                                //else
                                //{
                                //    if (!CheckIdkunRashemet("HALBASHA"))
                                //    {
                                //        oObjYameyAvodaUpd.HALBASHA = ZmanHalbashaType.CardError.GetHashCode();
                                //        oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                                //    }
                                //}
                            }
                            //}
                            //אם מזכה הלבשה קיבל ערך, ולא הגענו למסקנה שהוא לא זכאי, נעדכן את שדה מזכה הלבשה
                            if (bHaveHalbasha && (oObjYameyAvodaUpd.HALBASHA != ZmanHalbashaType.LoZakai.GetHashCode()))
                            {
                                OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
                                if (iSidurZakaiLehalbashaKnisa > -1 && iSidurZakaiLehalbashaKnisa != iSidurZakaiLehalbashaYetzia)
                                {
                                    oSidur = (SidurDM)htEmployeeDetails[iSidurZakaiLehalbashaKnisa];
                                    oObjSidurimOvdimUpd = GetSidurOvdimObject(oSidur.iMisparSidur, oSidur.dFullShatHatchala);

                                    oObjSidurimOvdimUpd.MEZAKE_HALBASHA = ZmanHalbashaType.ZakaiKnisa.GetHashCode(); ;
                                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                                }

                                if (iSidurZakaiLehalbashaYetzia > -1 && iSidurZakaiLehalbashaKnisa != iSidurZakaiLehalbashaYetzia)
                                {
                                    oSidur = (SidurDM)htEmployeeDetails[iSidurZakaiLehalbashaYetzia];
                                    oObjSidurimOvdimUpd = GetSidurOvdimObject(oSidur.iMisparSidur, oSidur.dFullShatHatchala);

                                    oObjSidurimOvdimUpd.MEZAKE_HALBASHA = ZmanHalbashaType.ZakaiYetiza.GetHashCode(); ;
                                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                                }

                                if (iSidurZakaiLehalbashaYetzia > -1 && iSidurZakaiLehalbashaKnisa == iSidurZakaiLehalbashaYetzia)
                                {
                                    oSidur = (SidurDM)htEmployeeDetails[iSidurZakaiLehalbashaYetzia];
                                    oObjSidurimOvdimUpd = GetSidurOvdimObject(oSidur.iMisparSidur, oSidur.dFullShatHatchala);

                                    oObjSidurimOvdimUpd.MEZAKE_HALBASHA = ZmanHalbashaType.ZakaiKnisaYetiza.GetHashCode(); ;
                                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                                }
                            }

                            if (!bHaveHalbasha && (!CheckIdkunRashemet("HALBASHA")))
                            {
                                oObjYameyAvodaUpd.HALBASHA = ZmanHalbashaType.LoZakai.GetHashCode();
                                oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                            }
                        }
                        else
                        {
                            oObjYameyAvodaUpd.HALBASHA = ZmanHalbashaType.LoZakai.GetHashCode();
                            oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, _iMisparIshi, "E", 11, dCardDate, "UpdateHalbasha: " + ex.Message);
                _bSuccsess = false;
            }
        }

        private bool IsOvedZakaiLZmanNesiaLaAvoda()
        {
            //לעובד מאפיין 51/61 (מאפיין זמן נסיעות) והעובד זכאי רק לזמן נסיעות לעבודה (ערך 1 בספרה הראשונה של מאפיין זמן נסיעות            
            return ((oMeafyeneyOved.IsMeafyenExist(61) && oMeafyeneyOved.GetMeafyen(61).Value.Substring(0, 1) == "1")
                   ||
                   (oMeafyeneyOved.IsMeafyenExist(51) && oMeafyeneyOved.GetMeafyen(51).Value.Substring(0, 1) == "1"));
        }

        private bool IsOvedZakaiLZmanNesiaMeAvoda()
        {
            //לעובד מאפיין 51/61 (מאפיין זמן נסיעות) והעובד זכאי רק לזמן נסיעות מהעבודה (ערך 2 בספרה הראשונה של מאפיין זמן נסיעות            
            return ((oMeafyeneyOved.IsMeafyenExist(61) && oMeafyeneyOved.GetMeafyen(61).Value.Substring(0, 1) == "2" )
                   ||
                   (oMeafyeneyOved.IsMeafyenExist(51) && oMeafyeneyOved.GetMeafyen(51).Value.Substring(0, 1) == "2"));
        }
        
        private bool IsOvedZakaiLZmanNesiaLeMeAvoda()
        {
            //לעובד מאפיין 51/61 (מאפיין זמן נסיעות) והעובד זכאי רק לזמן נסיעות מהעבודה (ערך 3 בספרה הראשונה של מאפיין זמן נסיעות            
            return ((oMeafyeneyOved.IsMeafyenExist(61) && oMeafyeneyOved.GetMeafyen(61).Value.Substring(0, 1) == "3")
                   ||
                   (oMeafyeneyOved.IsMeafyenExist(51) && oMeafyeneyOved.GetMeafyen(51).Value.Substring(0, 1) == "3"));
        }

       
         /******************************/
        private void SetHourToSidur19(ref SidurDM oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, bool bIdkunRashShatHatchala, bool bIdkunRashShatGmar)
        {
            DateTime dShatHatchalaLetashlumToUpd;
            DateTime dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR;
            DateTime dShatHatchalaLetashlum = DateTime.MinValue;
            DateTime dShatGmarLetashlum = DateTime.MinValue;
            bool bFromMeafyenHatchala, bFromMeafyenGmar;//,bLoLeadken=false;
            //קביעת שעות לסידורים שזמן ההתחלה/גמר מותנה במאפיין אישי
            try
            {
                if (oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA.ToShortDateString() != DateTime.MinValue.ToShortDateString())
                    dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA;
                else if (oObjSidurimOvdimUpd.SHAT_HATCHALA.ToShortDateString() != DateTime.MinValue.ToShortDateString())
                    dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA;
                else dShatHatchalaLetashlumToUpd = DateTime.MinValue;

                //אתחול שעת התחלה וגמר לפי מאפיינים וסוגי ימים
                GetOvedShatHatchalaGmar(oObjSidurimOvdimUpd.SHAT_GMAR, oMeafyeneyOved, ref oSidur, ref dShatHatchalaLetashlum, ref dShatGmarLetashlum, out bFromMeafyenHatchala, out  bFromMeafyenGmar);
                //סידור עם מאפיין 78 - קיזוז
                SetShatHatchalaGmarKizuz(ref oSidur, ref oObjSidurimOvdimUpd, dShatHatchalaLetashlum, dShatGmarLetashlum, ref dShatHatchalaLetashlumToUpd, ref dShatGmarLetashlumToUpd);

                //כל סידור מיוחד שלא עודכנו לו שעות התחלה/גמר לתשלום באחד הסעיפים הקודמים, יש לעדכן לו:
                //שעת התחלה לתשלום לשעת התחלה סידור
                //שעת גמר לתשלום לשעת גמר סידור
                if (dShatHatchalaLetashlumToUpd == DateTime.MinValue)
                {
                    if (oSidur.dFullShatHatchala.ToShortDateString() == DateTime.MinValue.ToShortDateString())
                    {
                        dShatHatchalaLetashlumToUpd = DateTime.MinValue;
                        oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = DateTime.MinValue; 
                    }
                    else dShatHatchalaLetashlumToUpd = oSidur.dFullShatHatchala;
                }

                if (dShatGmarLetashlumToUpd == DateTime.MinValue)
                    dShatGmarLetashlumToUpd = oSidur.dFullShatGmar;

                //if (oSidur.iMenahelMusachMeadken > 0 && oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM != DateTime.MinValue && dShatHatchalaLetashlumToUpd != oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM)
                //    bLoLeadken = true;
                if (oSidur.dShatHatchalaMenahelMusach != DateTime.MinValue)
                {
                    oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = oSidur.dShatHatchalaMenahelMusach;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                    oSidur.dFullShatHatchalaLetashlum = oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM;
                    oSidur.sShatHatchalaLetashlum = oSidur.dFullShatHatchalaLetashlum.ToString("HH:mm");
                }
                else if (!bIdkunRashShatHatchala && dShatHatchalaLetashlumToUpd != oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM)// && !bLoLeadken)
                {
                    oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = dShatHatchalaLetashlumToUpd;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                    oSidur.dFullShatHatchalaLetashlum = oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM;
                    oSidur.sShatHatchalaLetashlum = oSidur.dFullShatHatchalaLetashlum.ToString("HH:mm");
                    if (oSidur.dFullShatHatchalaLetashlum.Year < DateHelper.cYearNull)
                        oSidur.sShatHatchalaLetashlum = "";
                }

                //bLoLeadken = false;
                //if (oSidur.iMenahelMusachMeadken > 0 && oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM != DateTime.MinValue && dShatGmarLetashlumToUpd != oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM)
                //    bLoLeadken = true;
                if (oSidur.dShatGmarMenahelMusach != DateTime.MinValue)
                {
                    oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = oSidur.dShatGmarMenahelMusach;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                    oSidur.dFullShatGmarLetashlum = oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM;
                    oSidur.sShatGmarLetashlum = oSidur.dFullShatGmarLetashlum.ToString("HH:mm");
                }
                else if (!bIdkunRashShatGmar && dShatGmarLetashlumToUpd != oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM)// && !bLoLeadken)
                {
                    oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = dShatGmarLetashlumToUpd;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                    oSidur.dFullShatGmarLetashlum = oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM;
                    oSidur.sShatGmarLetashlum = oSidur.dFullShatGmarLetashlum.ToString("HH:mm");
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 19, oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "SetHourToSidur19: " + ex.Message, null);
                _bSuccsess = false;
            }
        }

        private void SetShatHatchalaGmarKizuz(ref SidurDM oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, DateTime dShatHatchalaLetashlum, DateTime dShatGmarLetashlum, ref DateTime dShatHatchalaLetashlumToUpd, ref DateTime dShatGmarLetashlumToUpd)
        {
            //5. סידור מסומן במאפיין 78
            //אם הסידור מסומן במאפיין 78 (קיזוז התחלה / גמר)
            //ואינו מסומן "לא לתשלום", ישנם כמה מקרים:
            bool flag = false;
            DateTime dShatHatchalaMeafyen, dShatGmarMeafyen;
            try
            {
                if (oSidur.bKizuzAlPiHatchalaGmarExists && oObjSidurimOvdimUpd.LO_LETASHLUM == 0)//|| (oObjSidurimOvdimUpd.LO_LETASHLUM == 1 && oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM == 1)))
                {
                    //1 .אם אין סימון "קוד חריגה" (אין = null או 0) ואין סימון "מחוץ למיכסה" (אין = null או 0), שלושה מקרים:                                                                               
                    if (oObjSidurimOvdimUpd.CHARIGA == 0)
                    {
                        SetShaotHatchalaGmar_2(ref oSidur, dShatHatchalaLetashlum, dShatGmarLetashlum, ref oObjSidurimOvdimUpd, ref dShatHatchalaLetashlumToUpd, ref dShatGmarLetashlumToUpd);
                        //אם חריגה=0 וגם עובד עם מאפייני משמרת שניה
                        SetShaotLovedMishmeret2(ref oSidur, ref oObjSidurimOvdimUpd, ref dShatHatchalaLetashlumToUpd, ref dShatGmarLetashlumToUpd,ref flag);

                    }
                    else //chariga>0
                    {
                        SetShaotLovedMishmeret2(ref oSidur, ref oObjSidurimOvdimUpd, ref dShatHatchalaLetashlumToUpd, ref dShatGmarLetashlumToUpd, ref flag);
                        dShatHatchalaMeafyen = flag ? dShatHatchalaLetashlumToUpd : dShatHatchalaLetashlum;
                        dShatGmarMeafyen = flag ? dShatGmarLetashlumToUpd : dShatGmarLetashlum;
                        //3. אם יש סימון "קוד חריגה" ולא קבענו את הסידור "לא לתשלום", שלושה מקרים:                                                           
                        if ((oObjSidurimOvdimUpd.CHARIGA != 0) && (oObjSidurimOvdimUpd.LO_LETASHLUM == 0))
                        {
                            //ראשית יש לבדוק האם החריגה אושרה במנגנון האישורים, רק אם כן יש לבצע את השינוי.
                            //if (CheckApprovalStatus("2,211,4,5,511,6,10,1011", oSidur.iMisparSidur, oSidur.dFullShatHatchala) == 1)
                            //{
                            //א. אם מסומן קוד חריגה "1"  (חריגה משעת התחלה) אזי שעת התחלה לתשלום = שעת תחילת הסידור.
                            //אם שעת הגמר של הסידור גדולה משעת מאפיין הגמר המותר (המאפיין תלוי בסוג היום, ראה עמודה  שדות מעורבים): שעת גמר לתשלום = השעה המוגדרת במאפיין. 

                            if (oObjSidurimOvdimUpd.CHARIGA == 1)
                            {
                                dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA;
                                if (oObjSidurimOvdimUpd.SHAT_GMAR > dShatGmarMeafyen)
                                    dShatGmarLetashlumToUpd = dShatGmarMeafyen;
                                else
                                    dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR;
                            }
                            //ב. אם מסומן קוד חריגה "2"  (חריגה משעת גמר) אזי שעת גמר לתשלום = שעת גמר הסידור.
                            //אם שעת התחלה של הסידור קטנה משעת מאפיין התחלה המותרת (המאפיין תלוי בסוג היום, ראה עמודה  שדות מעורבים) : שעת התחלה לתשלום = השעה המוגדרת במאפיין. 
                            if (oObjSidurimOvdimUpd.CHARIGA == 2)
                            {
                                dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR;
                                if (oSidur.dFullShatHatchala < dShatHatchalaMeafyen)
                                    dShatHatchalaLetashlumToUpd = dShatHatchalaMeafyen;
                                else
                                    dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA;
                            }
                            //ג. אם מסומן קוד חריגה "3"  (חריגה משעת התחלה וגמר) אזי :
                            //שעת התחלה לתשלום = שעת תחילת הסידור.
                            //שעת גמר לתשלום = שעת גמר הסידור
                            if (oObjSidurimOvdimUpd.CHARIGA == 3)
                            {
                                dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA;
                                dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void SetShaotHatchalaGmar(ref SidurDM oSidur, DateTime dShatHatchalaLetashlum, DateTime dShatGmarLetashlum, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, ref DateTime dShatHatchalaLetashlumToUpd, ref DateTime dShatGmarLetashlumToUpd)
        //{
        //    //א. אם שעת הגמר של הסידור לא גדולה משעת מאפיין ההתחלה המותרת (המאפיין תלוי בסוג היום, ראה עמודה  שדות מעורבים): יש לסמן את הסידור "לא לתשלום"
        //    //ב. אם שעת ההתחלה של הסידור לא קטנה משעת מאפיין הגמר המותרת (המאפיין תלוי בסוג היום, ראה עמודה  שדות מעורבים) - יש לסמן את הסידור "לא לתשלום                            
        //    try
        //    {
        //        if ((oObjSidurimOvdimUpd.SHAT_GMAR != DateTime.MinValue && (oObjSidurimOvdimUpd.SHAT_GMAR <= dShatHatchalaLetashlum)) || (oObjSidurimOvdimUpd.SHAT_HATCHALA >= dShatGmarLetashlum))
        //        {
        //            //oObjSidurimOvdimUpd.LO_LETASHLUM = 1;
        //            //oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM = 10;
        //            if (dShatHatchalaLetashlum.Year > DateHelper.cYearNull)
        //            {
        //                dShatHatchalaLetashlumToUpd = dShatHatchalaLetashlum;
        //            }
        //            if (dShatGmarLetashlum.Year > DateHelper.cYearNull)
        //            {
        //                dShatGmarLetashlumToUpd = dShatGmarLetashlum;
        //            }
        //            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
        //            //oSidur.iLoLetashlum = 1;
        //        }
        //        else
        //        {
        //            //א.ב.ג
        //            if ((oObjSidurimOvdimUpd.SHAT_GMAR<= dShatHatchalaLetashlum)
        //                || (oObjSidurimOvdimUpd.SHAT_HATCHALA >= dShatGmarLetashlum)
        //                ||  (oObjSidurimOvdimUpd.SHAT_HATCHALA < dShatHatchalaLetashlum && oObjSidurimOvdimUpd.SHAT_GMAR > dShatGmarLetashlum) )
        //            {
        //                dShatHatchalaLetashlumToUpd = dShatHatchalaLetashlum;
        //                dShatGmarLetashlumToUpd = dShatGmarLetashlum;
        //            }
        //            //ד
        //            if (oObjSidurimOvdimUpd.SHAT_HATCHALA < dShatHatchalaLetashlum && oObjSidurimOvdimUpd.SHAT_GMAR <= dShatGmarLetashlum)
        //            {
        //                dShatHatchalaLetashlumToUpd = dShatHatchalaLetashlum;
        //                dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR;
        //            }
        //            //ה
        //            if (oObjSidurimOvdimUpd.SHAT_HATCHALA >= dShatHatchalaLetashlum && oObjSidurimOvdimUpd.SHAT_GMAR > dShatGmarLetashlum)
        //            {
        //                dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA;
        //                dShatGmarLetashlumToUpd = dShatGmarLetashlum;
        //            }
        //            //ו
        //            if (oObjSidurimOvdimUpd.SHAT_HATCHALA >= dShatHatchalaLetashlum && oObjSidurimOvdimUpd.SHAT_GMAR <= dShatGmarLetashlum)
        //            {
        //                dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA;
        //                dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR;
        //            }
                  
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        /*************************************/
        //private void SetHourToSidur19_2(ref SidurDM oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, bool bIdkunRashShatHatchala, bool bIdkunRashShatGmar)
        //{
        //    DateTime dShatHatchalaLetashlumToUpd; // = oObjSidurimOvdimUpd.SHAT_HATCHALA;
        //    DateTime dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR;

        //    //קביעת שעות לסידורים שזמן ההתחלה/גמר מותנה במאפיין אישי
        //    try
        //    {
        //        if (oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA != DateTime.MinValue)
        //            dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA;
        //        else dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA;


        //        //GetSidurCurrentTime(iSidurIndex, ref oSidur, ref dSidurShatHatchala, ref dSidurShatGmar);
        //        //אם הסידור הוא מסוג "היעדרות" (סידור מיוחד עם מאפיין 53 בטבלת סידורים מיוחדים) והערך במאפיין הוא 8 (היעדרות בתשלום)  או 9 (ע"ח שעות נוספות)                   
        //        SetSidurHeadrut(ref oSidur, ref dShatHatchalaLetashlumToUpd, ref dShatGmarLetashlumToUpd, ref oObjSidurimOvdimUpd);
        //        //4. סידור אינו מסומן במאפיין 78 (רק לסידורים מיוחדים):
        //        //5. סידור מסומן במאפיין 78
        //        SetSidurKizuz(ref oSidur, ref dShatHatchalaLetashlumToUpd, ref dShatGmarLetashlumToUpd, ref oObjSidurimOvdimUpd);

        //        //     SetShaotLovedMishmeret2(ref oSidur, ref oObjSidurimOvdimUpd, ref dShatHatchalaLetashlumToUpd, ref dShatGmarLetashlumToUpd);

        //        if (!bIdkunRashShatHatchala && dShatHatchalaLetashlumToUpd != oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM)
        //        {
        //            oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = dShatHatchalaLetashlumToUpd;
        //            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
        //            oSidur.dFullShatHatchalaLetashlum = oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM;
        //            oSidur.sShatHatchalaLetashlum = oSidur.dFullShatHatchalaLetashlum.ToString("HH:mm");
        //            if (oSidur.dFullShatHatchalaLetashlum.Year < DateHelper.cYearNull)
        //                oSidur.sShatHatchalaLetashlum = "";
        //        }

        //        if (!bIdkunRashShatGmar && dShatGmarLetashlumToUpd != oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM)
        //        {
        //            oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = dShatGmarLetashlumToUpd;
        //            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
        //            oSidur.dFullShatGmarLetashlum = oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM;
        //            oSidur.sShatGmarLetashlum = oSidur.dFullShatGmarLetashlum.ToString("HH:mm");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 19, oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "SetHourToSidur19: " + ex.Message, null);
        //        _bSuccsess = false;
        //    }
        //}
      
        private void SetSidurHeadrut(ref SidurDM oSidur,ref DateTime dShatHatchalaLetashlumToUpd, ref DateTime dShatGmarLetashlumToUpd,ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            DateTime dShatHatchalaLetashlum = new DateTime();
            DateTime dShatGmarLetashlum = new DateTime();
            bool bFromMeafyenHatchala, bFromMeafyenGmar;
            try{
            if (oSidur.bSidurMyuhad)
            {
                //if ((oSidur.bHeadrutTypeKodExists) && (oSidur.sHeadrutTypeKod!=enMeafyenSidur53.enHeadrutWithPayment) &&
                //    (oSidur.sHeadrutTypeKod!=enMeafyenSidur53.enHolidayForHours))
                //{
                //    oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = null;
                //    oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = null;
                //}


                /*3. סידור מסוג העדרות (מקרה 2)
                אם הסידור הוא מסוג "היעדרות" (סידור מיוחד עם מאפיין 53 בטבלת סידורים מיוחדים) והערך במאפיין הוא 8 (היעדרות בתשלום)  או 9 (ע"ח שעות נוספות)
                שעת התחלה לתשלום = שעת התחלת מאפיין (המאפיין תלוי בסוג היום, ראה הסבר בעמודה שדות מעורבים)
                שעת גמר לתשלום = שעת גמר מאפיין (המאפיין תלוי בסוג היום, ראה הסבר בעמודה שדות מעורבים).
                (אין לעדכן שעת התחלה ושעת גמר)*/

                if ((oSidur.bHeadrutTypeKodExists) && ((oSidur.sHeadrutTypeKod == enMeafyenSidur53.enHeadrutWithPayment.GetHashCode().ToString()) ||
                    (oSidur.sHeadrutTypeKod == enMeafyenSidur53.enHolidayForHours.GetHashCode().ToString())))
                {
                    GetOvedShatHatchalaGmar(oObjSidurimOvdimUpd.SHAT_GMAR, oMeafyeneyOved, ref oSidur, ref dShatHatchalaLetashlum, ref dShatGmarLetashlum, out bFromMeafyenHatchala, out bFromMeafyenGmar);
                    //אם שעת גמר המאפיין גדולה מפרמטר כללי מספר 0002  אזי שעת גמר לתשלום = פרמטר כללי 0002 (וימן 08/10/2009)
                    dShatHatchalaLetashlumToUpd = dShatHatchalaLetashlum;

                    if (dShatGmarLetashlum > oParam.dSidurLimitShatGmar)
                    {
                        dShatGmarLetashlumToUpd = oParam.dSidurLimitShatGmar;
                    }
                    else
                    {
                        dShatGmarLetashlumToUpd = dShatGmarLetashlum;
                    }
                   
                }
            }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetSidurKizuz(ref SidurDM oSidur, ref DateTime dShatHatchalaLetashlumToUpd, ref DateTime dShatGmarLetashlumToUpd, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            try
            {
                if (oSidur.bSidurMyuhad)
                {
                    if ((!oSidur.bKizuzAlPiHatchalaGmarExists) && (oObjSidurimOvdimUpd.LO_LETASHLUM == 0 || (oObjSidurimOvdimUpd.LO_LETASHLUM == 1 && oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM == 1)) && (!oSidur.bHeadrutTypeKodExists))
                    {
                        //אם הסידור אינו מסומן במאפיין 78 (קיזוז התחלה / גמר) ואינו מסומן "לא לתשלום"
                        //שעת התחלה לתשלום = שעת תחילת הסידור.
                        //שעת גמר לתשלום = שעת גמר הסידור
                        //(אין לעדכן שעת התחלה ושעת גמר)

                        //ואם הסידור אינו מסוג "היעדרות" (סידור מיוחד עם מאפיין 53 בטבלת סידורים מיוחדים) (וימן 08/10/2009)
                        if (oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA != DateTime.MinValue)
                            dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA;
                        else
                            dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA;
                        dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR;//oSidur.dFullShatGmar;
                        
                    }
                    else
                    {
                        DateTime dShatHatchalaLetashlum = DateTime.MinValue;
                        DateTime dShatGmarLetashlum = DateTime.MinValue;
                        bool bFromMeafyenHatchala, bFromMeafyenGmar;

                        GetOvedShatHatchalaGmar(oObjSidurimOvdimUpd.SHAT_GMAR, oMeafyeneyOved, ref oSidur, ref dShatHatchalaLetashlum, ref dShatGmarLetashlum, out bFromMeafyenHatchala,out  bFromMeafyenGmar);

                        SetShatHatchalaGmarKizuz(ref oSidur, ref oObjSidurimOvdimUpd, dShatHatchalaLetashlum, dShatGmarLetashlum, ref dShatHatchalaLetashlumToUpd, ref dShatGmarLetashlumToUpd);

                    
                        //כל סידור מיוחד שלא עודכנו לו שעות התחלה/גמר לתשלום באחד הסעיפים הקודמים, יש לעדכן לו:
                        //שעת התחלה לתשלום לשעת התחלה סידור
                        //שעת גמר לתשלום לשעת גמר סידור
                        if (dShatHatchalaLetashlumToUpd == DateTime.MinValue)
                        {
                            dShatHatchalaLetashlumToUpd = oSidur.dFullShatHatchala;
                        }

                        if (dShatGmarLetashlumToUpd == DateTime.MinValue)
                        {
                            dShatGmarLetashlumToUpd = oSidur.dFullShatGmar;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void SetShatHatchalaGmarKizuz_2(ref SidurDM oSidur,ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, DateTime dShatHatchalaLetashlum, DateTime dShatGmarLetashlum,ref DateTime dShatHatchalaLetashlumToUpd, ref DateTime dShatGmarLetashlumToUpd)
        //{
        //    //5. סידור מסומן במאפיין 78
        //    //אם הסידור מסומן במאפיין 78 (קיזוז התחלה / גמר)
        //    //ואינו מסומן "לא לתשלום", ישנם כמה מקרים:
        //    bool flag=false;
        //   try{
        //       if ((oSidur.bKizuzAlPiHatchalaGmarExists) && (oObjSidurimOvdimUpd.LO_LETASHLUM == 0 || (oObjSidurimOvdimUpd.LO_LETASHLUM == 1 && oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM == 1)))
        //    {
                
        //        //1 .אם אין סימון "קוד חריגה" (אין = null או 0) ואין סימון "מחוץ למיכסה" (אין = null או 0), שלושה מקרים:                                                                               
        //        if (oObjSidurimOvdimUpd.CHARIGA == 0 )
        //        {
        //            SetShaotHatchalaGmar_2(ref oSidur, dShatHatchalaLetashlum, dShatGmarLetashlum, ref oObjSidurimOvdimUpd,ref dShatHatchalaLetashlumToUpd,ref dShatGmarLetashlumToUpd);
        //            SetShaotLovedMishmeret2(ref oSidur, ref oObjSidurimOvdimUpd, ref dShatHatchalaLetashlumToUpd, ref dShatGmarLetashlumToUpd, ref flag);

        //        }
        //        else
        //        {
        //            ////5.2. אם יש סימון "מחוץ למיכסה" ולא קבענו את הסידור "לא לתשלום":
        //            ////שעת התחלה לתשלום = שעת תחילת הסידור 
        //            ////שעת גמר לתשלום = שעת גמר הסידור
        //            //if ((oObjSidurimOvdimUpd.OUT_MICHSA != 0) && (oObjSidurimOvdimUpd.LO_LETASHLUM == 0 || (oObjSidurimOvdimUpd.LO_LETASHLUM == 1 && oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM == 1)))
        //            //{
        //            //    dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA;
        //            //    dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR;
        //            // }
        //            //3. אם יש סימון "קוד חריגה" ולא קבענו את הסידור "לא לתשלום", שלושה מקרים:                                                           
        //            if ((oObjSidurimOvdimUpd.CHARIGA != 0) && (oObjSidurimOvdimUpd.LO_LETASHLUM == 0))
        //            {
        //                //ראשית יש לבדוק האם החריגה אושרה במנגנון האישורים, רק אם כן יש לבצע את השינוי.
        //                //if (CheckApprovalStatus("2,211,4,5,511,6,10,1011", oSidur.iMisparSidur, oSidur.dFullShatHatchala) == 1)
        //                //{
        //                    //א. אם מסומן קוד חריגה "1"  (חריגה משעת התחלה) אזי שעת התחלה לתשלום = שעת תחילת הסידור.
        //                    //אם שעת הגמר של הסידור גדולה משעת מאפיין הגמר המותר (המאפיין תלוי בסוג היום, ראה עמודה  שדות מעורבים): שעת גמר לתשלום = השעה המוגדרת במאפיין. 

        //                    if (oObjSidurimOvdimUpd.CHARIGA == 1)
        //                    {
        //                        dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA;
        //                        if (oObjSidurimOvdimUpd.SHAT_GMAR > dShatGmarLetashlum)
        //                        {
        //                            //if (dShatGmarLetashlum > oParam.dSidurLimitShatGmar)
        //                            //{
        //                            //   dShatGmarLetashlumToUpd = oParam.dSidurLimitShatGmar;
        //                            //}
        //                            //else
        //                            //{
        //                            dShatGmarLetashlumToUpd = dShatGmarLetashlum;
                                  
        //                            //}
        //                        }
        //                        else
        //                        {
        //                            dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR;
        //                        }

        //                   }
        //                    //ב. אם מסומן קוד חריגה "2"  (חריגה משעת גמר) אזי שעת גמר לתשלום = שעת גמר הסידור.
        //                    //אם שעת התחלה של הסידור קטנה משעת מאפיין התחלה המותרת (המאפיין תלוי בסוג היום, ראה עמודה  שדות מעורבים) : שעת התחלה לתשלום = השעה המוגדרת במאפיין. 
        //                    if (oObjSidurimOvdimUpd.CHARIGA == 2)
        //                    {
        //                        dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR;
        //                        if (oSidur.dFullShatHatchala < dShatHatchalaLetashlum)
        //                        {
        //                            dShatHatchalaLetashlumToUpd = dShatHatchalaLetashlum;
        //                        }
        //                        else
        //                        {
        //                            dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA;
        //                        }
        //                    }
        //                    //ג. אם מסומן קוד חריגה "3"  (חריגה משעת התחלה וגמר) אזי :
        //                    //שעת התחלה לתשלום = שעת תחילת הסידור.
        //                    //שעת גמר לתשלום = שעת גמר הסידור
        //                    if (oObjSidurimOvdimUpd.CHARIGA == 3)
        //                    {
        //                        dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA;
        //                        dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR;
        //                     }
        //                //}
        //                //else
        //                //{ SetShaotHatchalaGmar(ref oSidur, dShatHatchalaLetashlum, dShatGmarLetashlum, ref oObjSidurimOvdimUpd,ref dShatHatchalaLetashlumToUpd,ref dShatGmarLetashlumToUpd); }
        //            }
        //        }
                
        //    }
        //   }
        //   catch (Exception ex)
        //   {
        //       throw ex;
        //   }
        //}

        private void SetShaotLovedMishmeret2(ref SidurDM oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd,  ref DateTime dShatHatchalaLetashlumToUpd, ref DateTime dShatGmarLetashlumToUpd,ref bool bflag)
        {
            DateTime shaa24, shaa23,shaa;
            bflag = false;
          try {
                 shaa = DateTime.Parse( oObjSidurimOvdimUpd.SHAT_GMAR.ToShortDateString() + " 18:00:00");
                if (!oMeafyeneyOved.IsMeafyenExist(42) && oMeafyeneyOved.IsMeafyenExist(23) && oMeafyeneyOved.IsMeafyenExist(24))
                    if (oSidur.bKizuzAlPiHatchalaGmarExists)
                        if ((oObjSidurimOvdimUpd.SHAT_HATCHALA.Hour >= 11 && oObjSidurimOvdimUpd.SHAT_HATCHALA.Hour <= 17 && oObjSidurimOvdimUpd.SHAT_GMAR > shaa)
                             && (oSidur.sShabaton != "1" && iSugYom >= enSugYom.Chol.GetHashCode() && iSugYom < enSugYom.Shishi.GetHashCode())) //|| ((iSugYom == enSugYom.Shishi.GetHashCode()  && oObjSidurimOvdimUpd.SHAT_GMAR > shaa.AddHours(-5)))))
                            {
                                shaa23 = DateTime.Parse(oObjSidurimOvdimUpd.SHAT_HATCHALA.ToShortDateString() + " " + oMeafyeneyOved.GetMeafyen(23).Value.Substring(0, 2) + ":" + oMeafyeneyOved.GetMeafyen(23).Value.Substring(2, 2));
                                shaa24 = DateTime.Parse(oObjSidurimOvdimUpd.SHAT_GMAR.ToShortDateString()  + " " + oMeafyeneyOved.GetMeafyen(24).Value.Substring(0, 2) + ":" + oMeafyeneyOved.GetMeafyen(24).Value.Substring(2, 2));
                                dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA > shaa23 ? oObjSidurimOvdimUpd.SHAT_HATCHALA : shaa23;
                                dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR < shaa24 ? oObjSidurimOvdimUpd.SHAT_GMAR : shaa24;
                                bflag = true;
                            }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetShaotHatchalaGmar_2(ref SidurDM oSidur, DateTime dShatHatchalaLetashlum, DateTime dShatGmarLetashlum, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, ref DateTime dShatHatchalaLetashlumToUpd, ref DateTime dShatGmarLetashlumToUpd)
        {
            //א. אם שעת הגמר של הסידור לא גדולה משעת מאפיין ההתחלה המותרת (המאפיין תלוי בסוג היום, ראה עמודה  שדות מעורבים): יש לסמן את הסידור "לא לתשלום"
            //ב. אם שעת ההתחלה של הסידור לא קטנה משעת מאפיין הגמר המותרת (המאפיין תלוי בסוג היום, ראה עמודה  שדות מעורבים) - יש לסמן את הסידור "לא לתשלום                            
            try
            {
                if ((oObjSidurimOvdimUpd.SHAT_GMAR != DateTime.MinValue && (oObjSidurimOvdimUpd.SHAT_GMAR <= dShatHatchalaLetashlum)) || (oObjSidurimOvdimUpd.SHAT_HATCHALA >= dShatGmarLetashlum))
                {
                    //oObjSidurimOvdimUpd.LO_LETASHLUM = 1;
                    //oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM = 10;
                    if (dShatHatchalaLetashlum.Year > DateHelper.cYearNull)
                    {
                        dShatHatchalaLetashlumToUpd = dShatHatchalaLetashlum;
                    }
                    if (dShatGmarLetashlum.Year > DateHelper.cYearNull)
                    {
                        dShatGmarLetashlumToUpd = dShatGmarLetashlum;
                    }
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                    //oSidur.iLoLetashlum = 1;
                }
                else
                {
                    if (oObjSidurimOvdimUpd.SHAT_HATCHALA.ToShortDateString() != DateTime.MinValue.ToShortDateString() && oObjSidurimOvdimUpd.SHAT_HATCHALA < dShatHatchalaLetashlum)
                    {
                        //ג. אם שעת ההתחלה של הסידור קטנה משעת מאפיין ההתחלה המותרת (תלוי בסוג היום, ראה עמודה שדות מעורבים): שעת התחלה לתשלום = השעה המוגדרת במאפיין. 
                        dShatHatchalaLetashlumToUpd = dShatHatchalaLetashlum;
                    }
                    else if (oObjSidurimOvdimUpd.SHAT_HATCHALA.ToShortDateString() != DateTime.MinValue.ToShortDateString())
                    {   //ד. אם שעת ההתחלה של הסידור אינה קטנה משעת מאפיין ההתחלה המותרת (תלוי בסוג היום, ראה עמודה שדות מעורבים): שעת התחלה לתשלום = שעת התחלת הסידור. 
                        dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA;
                    }

                    if (oObjSidurimOvdimUpd.SHAT_GMAR > dShatGmarLetashlum)
                    {
                        //אם שעת הגמר של הסידור גדולה משעת מאפיין הגמר המותר (המאפיין תלוי בסוג היום, ראה עמודה  שדות מעורבים) : שעת גמר לתשלום = השעה המוגדרת במאפיין
                        //if (dShatGmarLetashlum > oParam.dSidurLimitShatGmar)
                        //{
                        //    dShatGmarLetashlumToUpd = oParam.dSidurLimitShatGmar;
                        //}
                        //else
                        //{
                        dShatGmarLetashlumToUpd = dShatGmarLetashlum;
                        //}

                    }
                    else
                    {
                        //ה. אם שעת הגמר של הסידור אינה גדולה משעת מאפיין הגמר המותר (המאפיין תלוי בסוג היום, ראה עמודה  שדות מעורבים) : שעת גמר לתשלום = שעת גמר הסידור
                        dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void ChishuvShatHatchala30(ref SidurDM oSidur, int iIndexSidur, ref  bool bUsedMazanTichnun, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            int i = 0, indexPeilutMechona = -1, iIndexPeilutMashmautit=-1;
            PeilutDM oPeilut, oFirstPeilutMashmautit;
            long oMakatType, otoNum;
            bool bPeilutHachanatMechona = false;
            DateTime dShatHatchala;
            bool bUsedMazanTichnunInSidur = false;
            try
            {
                if (oSidur.htPeilut.Count > 0 && oSidur.iMisparSidur > 1000 && string.IsNullOrEmpty(oSidur.sSidurVisaKod))
                {
                    while (indexPeilutMechona < 0 && i < oSidur.htPeilut.Count)
                    {
                        oPeilut = (PeilutDM)oSidur.htPeilut[i];
                        oMakatType = oPeilut.lMakatNesia;
                        if (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "701" || oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "712" || oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "711")
                            indexPeilutMechona = i;
                        i++;
                    }
                    if (indexPeilutMechona == 0)
                        bPeilutHachanatMechona = true;
                    else if (indexPeilutMechona > 0)
                    {
                        oPeilut = (PeilutDM)oSidur.htPeilut[0];
                        var peilutManager = ServiceLocator.Current.GetInstance<IPeilutManager>();
                        if (peilutManager.IsMustBusNumber(oPeilut, oParam.iVisutMustRechevWC) && !isPeilutMashmautit(oPeilut))
                        {
                            oPeilut = (PeilutDM)oSidur.htPeilut[indexPeilutMechona];
                            otoNum = oPeilut.lOtoNo;
                            i = indexPeilutMechona - 1;
                            while (i >= 0)
                            {
                                oPeilut = (PeilutDM)oSidur.htPeilut[i];
                                if (peilutManager.IsMustBusNumber(oPeilut,oParam.iVisutMustRechevWC) && oPeilut.lOtoNo != otoNum)
                                    break;
                                i--;
                            }
                            if (i== -1)
                                bPeilutHachanatMechona = true;
                        }
                    }

                    if (!bPeilutHachanatMechona)
                    {
                        //קיימת פעילות משמעותית
                        oFirstPeilutMashmautit = null;
                        for (i = 0; i <= oSidur.htPeilut.Values.Count - 1; i++)
                        {
                            oPeilut = (PeilutDM)oSidur.htPeilut[i];
                            if (isPeilutMashmautit(oPeilut)) //oPeilut.iMakatType == enMakatType.mVisa.GetHashCode() || oNextPeilut.iMakatType == enMakatType.mKavShirut.GetHashCode() || oNextPeilut.iMakatType == enMakatType.mNamak.GetHashCode() || (oNextPeilut.iMakatType == enMakatType.mElement.GetHashCode() && (oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != enElementHachanatMechona.Element701.GetHashCode().ToString() && oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != enElementHachanatMechona.Element712.GetHashCode().ToString() && oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != enElementHachanatMechona.Element711.GetHashCode().ToString()) && (oNextPeilut.iElementLeShatGmar > 0 || oNextPeilut.iElementLeShatGmar == -1 || oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "700")))
                            {
                                oFirstPeilutMashmautit = oPeilut;
                                iIndexPeilutMashmautit = i;
                                break;
                            }
                        }

                        if (oFirstPeilutMashmautit != null)
                        {
                            if (iIndexPeilutMashmautit == 0)
                            {
                                dShatHatchala = oFirstPeilutMashmautit.dFullShatYetzia.AddMinutes(-oFirstPeilutMashmautit.iKisuyTor);
                            }
                            else
                            {
                                dShatHatchala = oFirstPeilutMashmautit.dFullShatYetzia.AddMinutes(-oFirstPeilutMashmautit.iKisuyTor);

                                for (i = iIndexPeilutMashmautit-1; i >= 0; i--)
                                {
                                    oPeilut = (PeilutDM)oSidur.htPeilut[i];
                                    if (isElemntLoMashmauti(oPeilut) || oPeilut.iMakatType == enMakatType.mEmpty.GetHashCode())
                                        dShatHatchala = dShatHatchala.AddMinutes(-(GetMeshechPeilutHachnatMechona(iIndexSidur, oPeilut, oSidur, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur)));
                                }
                            }
                        }
                        else
                        {
                            oPeilut = (PeilutDM)oSidur.htPeilut[0];
                            dShatHatchala = oPeilut.dFullShatYetzia;
                        }

                        UpdateShatHatchala(ref oSidur, iIndexSidur, dShatHatchala, ref oObjSidurimOvdimUpd);
                    }
                }
                  
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, _iMisparIshi, "E", 30, _dCardDate, "ChishuvShatHatchala30: " + ex.Message);
                _bSuccsess = false;
            }
        }

        private void UpdateShatHatchala(ref SidurDM oSidur, int iSidurIndex, DateTime dShatHatchalaNew, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            OBJ_PEILUT_OVDIM oObjPeilutUpd;
            PeilutDM oPeilut;
            SourceObj SourceObject;
            try
            {
                if (dShatHatchalaNew != oSidur.dFullShatHatchala)
                {
                    clNewSidurim oNewSidurim = FindSidurOnHtNewSidurim(oSidur.iMisparSidur, oSidur.dFullShatHatchala);

                    oNewSidurim.SidurIndex = iSidurIndex;
                    oNewSidurim.SidurNew = oSidur.iMisparSidur;
                    oNewSidurim.ShatHatchalaNew = dShatHatchalaNew;

                    UpdateObjectUpdSidurim(oNewSidurim);
                    //עדכון שעת התחלה סידור של כל הפעילויות לסידור
                    for (int j = 0; j < oSidur.htPeilut.Count; j++)
                    {
                        oPeilut = (PeilutDM)oSidur.htPeilut[j];
                        if (!CheckPeilutObjectDelete(iSidurIndex, j))
                        {
                            oObjPeilutUpd = GetUpdPeilutObject(iSidurIndex, oPeilut, out SourceObject, oObjSidurimOvdimUpd);
                            if (SourceObject == SourceObj.Insert)
                            {
                                oObjPeilutUpd.SHAT_HATCHALA_SIDUR = dShatHatchalaNew;
                            }
                            else
                            {
                                oObjPeilutUpd.NEW_SHAT_HATCHALA_SIDUR = dShatHatchalaNew;
                                oObjPeilutUpd.UPDATE_OBJECT = 1;
                            }

                        }
                    }
                    //UpdatePeiluyotMevutalotYadani(iSidurIndex,oNewSidurim, oObjSidurimOvdimUpd);
                    UpdateIdkunRashemet(oSidur.iMisparSidur, oSidur.dFullShatHatchala, dShatHatchalaNew);
                    UpdateApprovalErrors(oSidur.iMisparSidur, oSidur.dFullShatHatchala, dShatHatchalaNew);

                    oSidur.dFullShatHatchala = dShatHatchalaNew;
                    oSidur.sShatHatchala = dShatHatchalaNew.ToString("HH:mm");
                    oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA = dShatHatchalaNew;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, _iMisparIshi, "E", 0, _dCardDate, "UpdateShatHatchala: " + ex.Message);
                _bSuccsess = false;
            }
        }
        private bool isPeilutMashmautit(PeilutDM oPeilut)
        {
            if (oPeilut.iMakatType == enMakatType.mVisa.GetHashCode() || oPeilut.iMakatType == enMakatType.mKavShirut.GetHashCode() || oPeilut.iMakatType == enMakatType.mNamak.GetHashCode() ||
                (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != enElementHachanatMechona.Element701.GetHashCode().ToString() && oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != enElementHachanatMechona.Element712.GetHashCode().ToString() && oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != enElementHachanatMechona.Element711.GetHashCode().ToString())
                                && (oPeilut.iElementLeShatGmar > 0 || oPeilut.iElementLeShatGmar == -1)))
                return true;
            else return false;          
        }
        private bool isElemntLoMashmauti(PeilutDM oPeilut)
        {
            if (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && oPeilut.iElementLeShatGmar == 0 &&
                oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "700")
                return true;
            else return false;
        }
        public void GetOvedShatHatchalaGmar(DateTime dSidurShatGmar, MeafyenimDM oMeafyeneyOved, ref SidurDM oSidur,
                                             ref DateTime dShatHatchalaLetashlum, ref DateTime dShatGmarLetashlum,out bool bFromMeafyenHatchala,out bool bFromMeafyenGmar)
        {
            /*  יום חול 
            שעת התחלה – מאפיין 03
            שעת גמר       - מאפיין 04
            יום שישי/ערב חג 
            שעת התחלה – מאפיין 05
            שעת גמר       - מאפיין 06
            יום שבת/שבתון 
            שעת התחלה – מאפיין 07
            שעת גמר       - מאפיין 08*/

            try
            {
                bFromMeafyenHatchala = false;
                bFromMeafyenGmar = false;
                if (oOvedYomAvodaDetails == null)
                {
                    SetOvedYomAvodaDetails(_iMisparIshi, _dCardDate);
                }
               
                    DateTime dShatHatchalaSidur = oSidur.dFullShatHatchala;

                    if (dShatHatchalaSidur == DateTime.MinValue)
                    {
                        dShatHatchalaSidur = _dCardDate;
                    }

                    if (dSidurShatGmar == DateTime.MinValue)
                    {
                        dSidurShatGmar = _dCardDate;
                    }

                    //קביעת מאפיינים מפעילים
                    if ((oOvedYomAvodaDetails.iIsuk == 122 || oOvedYomAvodaDetails.iIsuk == 123 || oOvedYomAvodaDetails.iIsuk == 124 || oOvedYomAvodaDetails.iIsuk == 127))
                    {
                        GetMeafyeneyMafilim(oSidur,dShatHatchalaSidur, dSidurShatGmar,out  bFromMeafyenHatchala, out  bFromMeafyenGmar,ref dShatHatchalaLetashlum, ref dShatGmarLetashlum);
                    }
                    else
                    {
                        //יום שבת/שבתון
                        if ((oSidur.sSidurDay == enDay.Shabat.GetHashCode().ToString()) || (oSidur.sShabaton == "1"))
                        {
                            if (oMeafyeneyOved.IsMeafyenExist(7))
                            {
                                dShatHatchalaLetashlum = DateHelper.ConvertMefyenShaotValid(dShatHatchalaSidur, oMeafyeneyOved.GetMeafyen(7).Value);
                                bFromMeafyenHatchala = true;
                            }
                            else
                            {//אם אין מאפיין, נקבע את שעת הסידור כברירת מחדל
                                dShatHatchalaLetashlum = oSidur.dFullShatHatchala;
                            }
                            if (oMeafyeneyOved.IsMeafyenExist(8))
                            {
                                dShatGmarLetashlum = DateHelper.ConvertMefyenShaotValid(dShatHatchalaSidur, oMeafyeneyOved.GetMeafyen(8).Value);
                                bFromMeafyenGmar = true;
                            }
                            else
                            {//אם אין מאפיין, נקבע את שעת הסידור כברירת מחדל
                                dShatGmarLetashlum = dSidurShatGmar;
                            }
                        }
                        else
                        {
                            //יום שישי או ערב חג
                            if ((oSidur.sSidurDay == enDay.Shishi.GetHashCode().ToString()))
                            {
                                if (oMeafyeneyOved.IsMeafyenExist(5))
                                {
                                    dShatHatchalaLetashlum = DateHelper.ConvertMefyenShaotValid(dShatHatchalaSidur, oMeafyeneyOved.GetMeafyen(5).Value);
                                    bFromMeafyenHatchala = true;
                                }
                                else
                                {//אם אין מאפיין, נקבע את שעת הסידור כברירת מחדל
                                    dShatHatchalaLetashlum = oSidur.dFullShatHatchala;
                                }
                                if (oMeafyeneyOved.IsMeafyenExist(6))
                                {
                                    dShatGmarLetashlum = DateHelper.ConvertMefyenShaotValid(dSidurShatGmar, oMeafyeneyOved.GetMeafyen(6).Value);
                                    bFromMeafyenGmar = true;
                                }
                                else
                                {//אם אין מאפיין, נקבע את שעת הסידור כברירת מחדל
                                    dShatGmarLetashlum = dSidurShatGmar;
                                }
                            }
                            else
                            {   //יום חול
                                if (oMeafyeneyOved.IsMeafyenExist(3))
                                {
                                    dShatHatchalaLetashlum = DateHelper.ConvertMefyenShaotValid(dShatHatchalaSidur, oMeafyeneyOved.GetMeafyen(3).Value);
                                    bFromMeafyenHatchala = true;
                                }
                                else
                                {//אם אין מאפיין, נקבע את שעת הסידור כברירת מחדל
                                    dShatHatchalaLetashlum = oSidur.dFullShatHatchala;
                                }

                                if (oMeafyeneyOved.IsMeafyenExist(4))
                                {
                                    dShatGmarLetashlum = DateHelper.ConvertMefyenShaotValid(dShatHatchalaSidur, oMeafyeneyOved.GetMeafyen(4).Value);
                                    bFromMeafyenGmar = true;
                                }
                                else
                                {//אם אין מאפיין, נקבע את שעת הסידור כברירת מחדל
                                    dShatGmarLetashlum = dSidurShatGmar;
                                }
                            }
                        }

                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void GetMeafyeneyMafilim(SidurDM oSidur, DateTime dShatHatchalaSidur, DateTime dSidurShatGmar, out bool bFromMeafyenHatchala, out bool bFromMeafyenGmar, ref DateTime dShatHatchalaLetashlum, ref DateTime dShatGmarLetashlum)
        {
            SidurDM firstTafkidSidur = htEmployeeDetails.Values.Cast<SidurDM>().ToList().FirstOrDefault(sidur => sidur.iMisparSidur == 99001);
            SidurDM lastTafkidSidur = htEmployeeDetails.Values.Cast<SidurDM>().ToList().LastOrDefault(sidur => sidur.iMisparSidur == 99001);
            
            bFromMeafyenHatchala = false;
            bFromMeafyenGmar = false;

            if (firstTafkidSidur != null && lastTafkidSidur != null)
            {
                if (_oMeafyeneyOved.IsMeafyenExist(3))
                {
                    dShatHatchalaLetashlum = DateHelper.ConvertMefyenShaotValid(_dCardDate, oMeafyeneyOved.GetMeafyen(3).Value);
                    bFromMeafyenHatchala = true;
                }
                if (_oMeafyeneyOved.IsMeafyenExist(4))
                {
                    dShatGmarLetashlum = DateHelper.ConvertMefyenShaotValid(_dCardDate, oMeafyeneyOved.GetMeafyen(4).Value);
                    bFromMeafyenGmar = true;
                }

                iSugYom = DateHelper.GetSugYom(_iMisparIshi, _dCardDate, _dtYamimMeyuchadim, _oOvedYomAvodaDetails.iKodSectorIsuk, _dtSugeyYamimMeyuchadim, _oMeafyeneyOved.GetMeafyen(56).IntValue);
                if (iSugYom >= enSugYom.Chol.GetHashCode() && iSugYom < enSugYom.Shishi.GetHashCode())
                {
                    int iSugMishmeret = DateHelper.GetSugMishmeret(_iMisparIshi, _dCardDate, _iSugYom, firstTafkidSidur.dFullShatHatchala, lastTafkidSidur.dFullShatGmar, _oParameters);

                    switch (iSugMishmeret)
                    {
                        case 1:// בוקר                            
                            break;

                        case 2:// צהריים
                            {
                                if (_oMeafyeneyOved.IsMeafyenExist(23))
                                {
                                    dShatHatchalaLetashlum = DateHelper.ConvertMefyenShaotValid(dShatHatchalaSidur, oMeafyeneyOved.GetMeafyen(23).Value);
                                    bFromMeafyenHatchala = true;
                                }
                                if (_oMeafyeneyOved.IsMeafyenExist(24))
                                {
                                    dShatGmarLetashlum = DateHelper.ConvertMefyenShaotValid(dSidurShatGmar, oMeafyeneyOved.GetMeafyen(24).Value);
                                    bFromMeafyenGmar = true;
                                }
                                break;
                            }


                        case 3:// לילה
                            {
                                if (_oMeafyeneyOved.IsMeafyenExist(25))
                                {
                                    dShatHatchalaLetashlum = DateHelper.ConvertMefyenShaotValid(dShatHatchalaSidur, oMeafyeneyOved.GetMeafyen(25).Value);
                                    bFromMeafyenHatchala = true;
                                }
                                if (_oMeafyeneyOved.IsMeafyenExist(26))
                                {
                                    dShatGmarLetashlum = DateHelper.ConvertMefyenShaotValid(dSidurShatGmar, oMeafyeneyOved.GetMeafyen(26).Value);
                                    bFromMeafyenGmar = true;
                                }
                                break;
                            }

                    }

                }
                else
                {
                    if (iSugYom == enSugYom.Shishi.GetHashCode() && iSugYom < enSugYom.Shabat.GetHashCode())
                    {
                        if (_oMeafyeneyOved.IsMeafyenExist(27))
                        {
                            dShatHatchalaLetashlum = DateHelper.ConvertMefyenShaotValid(dShatHatchalaSidur, oMeafyeneyOved.GetMeafyen(27).Value);
                            bFromMeafyenHatchala = true;
                        }
                        if (_oMeafyeneyOved.IsMeafyenExist(28))
                        {
                            dShatGmarLetashlum = DateHelper.ConvertMefyenShaotValid(dSidurShatGmar, oMeafyeneyOved.GetMeafyen(28).Value);
                            bFromMeafyenGmar = true;
                        }
                    }
                    else if ((iSugYom >= enSugYom.Shabat.GetHashCode() && iSugYom < enSugYom.Rishon.GetHashCode()) || oSidur.sShabaton == "1")
                    {
                        if (_oMeafyeneyOved.IsMeafyenExist(7))
                        {
                            dShatHatchalaLetashlum = DateHelper.ConvertMefyenShaotValid(dShatHatchalaSidur, oMeafyeneyOved.GetMeafyen(7).Value);
                            bFromMeafyenHatchala = true;
                        }
                        if (_oMeafyeneyOved.IsMeafyenExist(8))
                        {
                            dShatGmarLetashlum = DateHelper.ConvertMefyenShaotValid(dSidurShatGmar, oMeafyeneyOved.GetMeafyen(8).Value);
                            bFromMeafyenGmar = true;
                        }
                    }

                }
            }
        }

        private class clNewSidurim
        {
            //האובייקט יכיל את כל הסידורים שמספר הסידור לא תקין  ובעקבות שינוי 1 קיבלו מספר חדש
            int _iSidurIndex;
            int _iSidurOld;
            int _iSidurNew;
            DateTime _dShatHatchalaOld;
            DateTime _dShatHatchalaNew;
           // DateTime _dShatGmarOld;
          //  DateTime _dShatGmarNew;

            public int SidurIndex
            {
                get
                {
                    return this._iSidurIndex;
                }
                set
                {
                    this._iSidurIndex = value;
                }
            }

            public int SidurOld
            {
                get
                {
                    return this._iSidurOld;
                }
                set
                {
                    this._iSidurOld = value;
                }
            }

            public int SidurNew
            {
                get
                {
                    return this._iSidurNew;
                }
                set
                {
                    this._iSidurNew = value;
                }
            }

            public DateTime ShatHatchalaOld
            {
                get
                {
                    return this._dShatHatchalaOld;
                }
                set
                {
                    this._dShatHatchalaOld = value;
                }
            }

            public DateTime ShatHatchalaNew
            {
                get
                {
                    return this._dShatHatchalaNew;
                }
                set
                {
                    this._dShatHatchalaNew = value;
                }

            }


      }

       
        public bool IsExecuteErrors
        {
            set
            {
                _IsExecuteErrors = value;
            }
            get
            {
                return _IsExecuteErrors;
            }
        }
        public int MisparIshi
        {
            set
            {
                _iMisparIshi = value;
            }
            get
            {
                return _iMisparIshi;
            }
        }
        public DateTime CardDate
        {
            set
            {
                _dCardDate = value;
            }
            get
            {
                return _dCardDate;
            }
        }

        #region IDisposable Members

        public void Dispose()
        {
            //if (_dtApproval != null)
            //    _dtApproval.Dispose();
            if (_dtDetails != null)
                _dtDetails.Dispose();
            if (_dtErrors != null)
                _dtErrors.Dispose();
            if (_dtMatzavOved != null)
                _dtMatzavOved.Dispose();
            if (_dtMeafyeneyElements != null)
                _dtMeafyeneyElements.Dispose();
            if (_dtSidurimMeyuchadim != null)
                _dtSidurimMeyuchadim.Dispose();
            if (_dtSugeyYamimMeyuchadim != null)
                _dtSugeyYamimMeyuchadim.Dispose();
            if (_dtSugSidur != null)
                _dtSugSidur.Dispose();
            if (_dtYamimMeyuchadim != null)
                _dtYamimMeyuchadim.Dispose();
            //if (dtChishuvYom != null)
            //    dtChishuvYom.Dispose();
            if (dtMashar != null)
                dtMashar.Dispose();
            if (dtMutamut != null)
                dtMutamut.Dispose();
            if (dtSibotLedivuachYadani != null)
                dtSibotLedivuachYadani.Dispose();

        }

        #endregion

        private PeilutDM CreatePeilut(int iMisparIshi, DateTime dCardDate, PeilutDM oPeilut, long makat, DataTable dtTmpMeafyeneyElements)
        {
            var manager = ServiceLocator.Current.GetInstance<IPeilutManager>();
            return manager.CreatePeilutFromOldPeilut(iMisparIshi, dCardDate, oPeilut, makat, dtTmpMeafyeneyElements);
        }

        private PeilutDM CreatePeilut(int iMisparIshi, DateTime dCardDate, OBJ_PEILUT_OVDIM oObjPeilutOvdimIns, DataTable dtTmpMeafyeneyElements)
        {
            var manager = ServiceLocator.Current.GetInstance<IPeilutManager>();
            return manager.CreateClsPeilut(iMisparIshi, dCardDate, oObjPeilutOvdimIns, dtTmpMeafyeneyElements);
        }

        
    }
}

