using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Specialized;
using KdsLibrary.DAL;
using KdsLibrary.BL;
using KdsLibrary.UDT;
using System.Collections;
using KdsLibrary.Utils;
using KdsLibrary;
using KdsWorkFlow.Approvals;
using System.Web;

namespace KdsBatch
{
    public class clBatchManager : IDisposable
    {
        
        private clParameters _oParameters;
        private DataTable _dtLookUp;
        private DataTable _dtYamimMeyuchadim;
        private DataTable _dtSugeyYamimMeyuchadim;
        private clMeafyenyOved _oMeafyeneyOved;
        private clOvedYomAvoda _oOvedYomAvodaDetails;
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
        private clGeneral.enCardStatus _CardStatus = clGeneral.enCardStatus.Valid;
        //private DataTable _dtApproval;
        private DataTable _dtIdkuneyRashemet;
        private DataTable _dtApprovalError;
        private DataTable _dtErrorsNotActive;
        private DateTime dTarTchilatMatzav=DateTime.MinValue;
        private bool _bHaveCount = true;
        //private DataTable dtDetails;
        //private DataTable dtLookUp;       
        //private DataTable dtMatzavOved;       
        //private DataTable dtSugSidur; //מכיל מאפייני סוגי סידורים       
        //private OrderedDictionary htEmployeeDetails;
        private DataRow drNew;       
        private int iLastMisaprSidur;
        //private clParameters oParam;
        //private clOvedYomAvoda oOvedYomAvodaDetails;
        //private clMeafyenyOved oMeafyeneyOved;

        //שינויי קלט
        private DataTable dtMutamut;
        private DataTable dtSibotLedivuachYadani;
      //  private DataSet dtChishuvYom;
        public DataTable dtMashar;
        //private OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
        private OBJ_SIDURIM_OVDIM oObjSidurimOvdimDel;
        private OBJ_SIDURIM_OVDIM oObjSidurimOvdimIns;
        private OBJ_PEILUT_OVDIM oObjPeilutOvdimUpd;
        private OBJ_PEILUT_OVDIM oObjPeilutOvdimDel;
        private OBJ_YAMEY_AVODA_OVDIM oObjYameyAvodaUpd;
        private COLL_SIDURIM_OVDIM oCollSidurimOvdimUpd; //= new COLL_SIDURIM_OVDIM();
        private COLL_SIDURIM_OVDIM oCollSidurimOvdimIns; //= new COLL_SIDURIM_OVDIM();
        private COLL_SIDURIM_OVDIM oCollSidurimOvdimDel;// = new COLL_SIDURIM_OVDIM();
        private COLL_YAMEY_AVODA_OVDIM oCollYameyAvodaUpd;// = new COLL_YAMEY_AVODA_OVDIM();
        private COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimDel; //= new COLL_OBJ_PEILUT_OVDIM();
        private COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimUpd; //= new COLL_OBJ_PEILUT_OVDIM();
        private COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimIns; // new COLL_OBJ_PEILUT_OVDIM();
        private COLL_IDKUN_RASHEMET _oCollIdkunRashemet;
        private COLL_IDKUN_RASHEMET _oCollIdkunRashemetDel;
        private COLL_SHGIOT_MEUSHAROT _oCollApprovalErrors;
   
       // private ApprovalRequest[] arrEmployeeApproval; vered 22/05/2012
       // private bool bHasShaonIsurInMaxLevel;
        private clCalculation objCalc = new clCalculation();
        private const int SIDUR_NESIA = 99300;
        private const int SIDUR_MATALA = 99301;
        private const int SIDUR_MACHALA_IM_MUGBALUT = 99819;
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

        public enum SourceObj
        {
            Insert = 1,
            Update = 2
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
         
        public enum enErrors
        {
            errHrStatusNotValid = 1,
            errSidurNotExists = 9,
            errSidurNamlakWithoutNesiaCard = 13,
            errSidurHourStartNotValid = 14,//לבדוק שינויים            
            errStartHourMissing = 15,
            errSidurimHoursNotValid = 16,
            errPizulHafsakaValueNotValid = 20,
            errPizulValueNotValid = 22, 
            errShabatPizulValueNotValid = 23,
            errPitzulSidurInShabat = 24,
            errPitzulMuchadValueNotValid = 25,
            errLoZakaiLeNesiot  = 26,
            errSimunNesiaNotValid = 27,
            errLinaValueNotValid = 30,
            errLoZakaiLLina = 31,
            errCharigaValueNotValid = 32,
            errCharigaZmanHachtamatShaonNotValid = 33,
            errZakaiLeCharigaValueNotValid = 34,  
            errSidurExistsInShlila =35,//לקחת את הערך לפי עץ ניהולי
            errHalbashaNotvalid = 36, 
            errHalbashaInSidurNotValid = 37, 
            errHamaraNotValid = 39,
            errOutMichsaInSidurHeadrutNotValid = 40,
            errHamaratShabatNotValid = 42,
            errHamaratShabatNotAllowed=43,
            errZakaiLehamaratShabat = 44,//להוסיף לסידורים רגילים
            errHashlamaForComputerWorkerAndAccident = 45,            
            errShbatHashlamaNotValid = 47,
            errHashlamaForSidurNotValid = 48,
            errHahlamatHazmanaNotValid = 49,
            errSidurNotAllowedInShabaton = 50,
            errTeoodatNesiaNotInVisa =52,
            errSidurEilatNotValid = 55,
            errYomVisaNotValid = 56,
            errSimunVisaNotValid = 57,
            errSidurVisaNotValid = 58,
            //errOtoNoNotValid = 67,
            errOtoNoExists = 68,//להוסיף לסידורים רגילים
            errOtoNoNotExists = 69,//לבדוק באיפיון מה הכוונה מספר פעילות המתחילה ב7-
            errKodNesiaNotExists = 81,
            errPeilutForSidurNonValid = 84,
            errTimeForPrepareMechineNotValid = 86,
            errHighValueKisuyTor=87,
            errNesiaTimeNotValid = 91,
            errShatYetizaNotExist = 92,
            errKmNotExists = 96,
            //errKisuyTorNotValid = 117,
            errDuplicateShatYetiza = 103,
            errMissingSugVisa=106,
            errHafifaBecauseOfHashlama = 108,
            errPeilutShatYetizaNotValid = 113,
            errOutMichsaNotValid = 118,
            errShatPeilutSmallerThanShatHatchalaSidur = 121,
            errShatPeilutBiggerThanShatGmarSidur = 122,
            errElementInSpecialSidurNotAllowed=123,
            errSidurNetzerNotValidForOved = 124,
            errNesiaInSidurVisaNotAllowed = 125,
            errAtLeastOnePeilutRequired = 127,
            errElementTimeBiggerThanSidurTime = 129,
            errOvdaInMachalaNotAllowed = 132,
            errDriverLessonsNumberNotValid = 136,
            errHashlamaNotValid = 137,
            errMisparSiduriOtoNotExists = 139,  
            errMisparSiduriOtoNotInSidurEilat = 140,
            errNotAllowedKodsForEggedTaavora = 141,
            errTotalHashlamotBiggerThanAllow = 142,
            errMissingNumStore=143,
            errSidurTafkidWithOutApprove = 145,
            errNotAllowedSidurForEggedTaavora = 148,
            errNesiaMeshtanaNotDefine  = 150,
            errDuplicateTravle=151,
            errChafifaBesidurNihulTnua = 152,
            errHighPremya = 153,
            errNegativePremya = 154,
            errMiluimAndAvoda=156,
            errSidurAvodaNotValidForMonth = 160,
            errOvedNotAllowedToDoSidurNahagut = 161,
            errCurrentPeilutInPrevPeilut = 162,
            errHashlamaLeYomAvodaNotAllowed = 163,
            errSidurSummerNotValidForOved = 164,
            errAvodatNahagutNotValid = 165,
            errHmtanaTimeNotValid = 166,
            errHafifaBetweenSidurim = 167,
            errCurrentSidurInPrevSidur = 168,
            errOvedNotExists = 169,
            errVisaNotValid = 170,
            errHasBothSidurEilatAndSidurVisa = 171,
            errOvedPeilutNotValid = 172,
            errSidurHourEndNotValid = 173,
            errEndHourMissing=174,
            errHachtamaYadanitKnisa = 175,
            errHachtamaYadanitYetzia = 176,
            errSidurGriraNotValid=177,
            errMissingKodMevatzaVisa=178,
            errHightValueDakotBefoal=179,
            IsShatHatchalaLetashlumNull = 180,
            IsShatGmarLetashlumNull = 181,
            ErrMisparElementimMealHamutar = 185,
            errMutamLoBeNahagutBizeaNahagut = 186,
            errKupaiWithNihulTnua = 187,
            errChofeshAlCheshbonShaotNosafot = 188,
            errKisuyTorLifneyHatchalatSidur =189,
            errSidurLoTakefLetaarich=190,
            errIsukNahagImSidurTafkidNoMefyen=191,
            errMatzavOvedNotValidFirstDay =192,
            errDivuachSidurLoMatimLeisuk420 = 193,
            errDivuachSidurLoMatimLeisuk422 = 194,
            errFirstDayShlilatRishayon195 = 195,
            errkupaiLeloHachtama = 196,
            errHachtamatKnisaLoBmakomHasaka197 =197,
            errHachtamatYetziaLoBmakomHasaka198 = 198,
            errAvodaByemeyTeuna199=199,
            errAvodaByemeyEvel200=200,
            errAvodaByemeyMachala201=201,
            errMachalaLeloIshur202 = 202,
            errConenutGriraMealHamutar=203,
            errSidurAsurBeyomShishiLeoved5Yamim204=204,
            errTipatChalavMealMichsa205=205,
            errOvedMutaamLeloShaotNosafot206 = 206,
            errShatHatchalaBiggerShatYetzia=207,
            errMushalETWithSidurNotAllowET = 208,
            errShatHatchalaLetashlumBiggerShatGmar = 209,
            errMichsatMachalaYeledImMugbalut = 210,
            errRechevMushbat = 211,
            errMachalaMisradHaBitachonLoYachid=212
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

        public clBatchManager(long? btchRequest, int iUserId)
        {
            _btchRequest = btchRequest;
            _iUserId = iUserId;
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
                dtLookUp = oUtils.GetLookUpTables();

                //Get Parameters Table
                //dtParameters = GetKdsParametrs();
                dtYamimMeyuchadim = clGeneral.GetYamimMeyuchadim();
                _dtSugeyYamimMeyuchadim = clGeneral.GetSugeyYamimMeyuchadim();

                //Get Meafyeny Ovdim
                GetMeafyeneyOvdim(_iMisparIshi, _dCardDate);

                iSugYom = clGeneral.GetSugYom(dtYamimMeyuchadim, _dCardDate, _dtSugeyYamimMeyuchadim);//, _oMeafyeneyOved.iMeafyen56);

                //Set global variable with parameters
                SetParameters(_dCardDate, iSugYom);

                
                //Get Meafyeney Sug Sidur
                dtSugSidur = clDefinitions.GetSugeySidur();

                SetOvedYomAvodaDetails(_iMisparIshi, _dCardDate);

                if (oOvedYomAvodaDetails.OvedDetailsExists)
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

        public void DeleteErrorsFromTbShgiot()
        {
            clDal oDal = new clDal();
            try
            {
                oDal.ExecuteSQL(string.Concat("DELETE TB_SHGIOT T WHERE T.MISPAR_ISHI = ", _iMisparIshi, " and taarich = to_date('", _dCardDate.Date.ToString("dd/MM/yyyy"), "','dd/mm/yyyy')"));
            }
            catch (Exception ex)
            {
                throw ex;
            }    
        }

        public void InsertErrorsToTbShgiot(DataTable dtErrors, DateTime dCardDate)
        {
            //כתיבת שגיאות ל-TB_SHGIOT
            clDal oDal = new clDal();
            StringBuilder sbYeshut = new StringBuilder();
            DataSet ds = new DataSet();
            string[] ucols = new string[2];

            try
            {
              
                //ds.Tables.Add(dtErrors);
                
                //ucols[0] = "mispar_ishi";
                //ucols[1] = "KOD_SHGIA";
                //oDal.InsertXML(ds.GetXml(), "TB_SHGIOT", ucols);
                int i = 0;
                oDal.ArrayBindCount = dtErrors.Rows.Count;
                int[] arrMisparIshi = new int[dtErrors.Rows.Count];
                int[] arrKodShgia = new int[dtErrors.Rows.Count];
                string[] arrYeshutId = new string[dtErrors.Rows.Count];
                int[] arrMisparSidur = new int[dtErrors.Rows.Count];
                DateTime[] arrTaarich = new DateTime[dtErrors.Rows.Count];
                DateTime[] arrShatHatchala = new DateTime[dtErrors.Rows.Count];
                DateTime[] arrShatYetzia = new DateTime[dtErrors.Rows.Count];
                int[] arrMisparKnisa = new int[dtErrors.Rows.Count];
                string[] arrHeara = new string[dtErrors.Rows.Count];

                foreach (DataRow dr in dtErrors.Rows)
                {
                    arrMisparIshi[i] = int.Parse(dr["mispar_ishi"].ToString());
                    arrKodShgia[i] = int.Parse(dr["check_num"].ToString());
                    sbYeshut.Remove(0, sbYeshut.Length);
                    sbYeshut.Append(string.IsNullOrEmpty(dr["Taarich"].ToString()) ? dCardDate.ToShortDateString() : DateTime.Parse(dr["Taarich"].ToString()).ToShortDateString());
                    //sbYeshut.Append(DateTime.Parse(dr["Taarich"].ToString()).ToShortDateString());
                    sbYeshut.Append(",");
                    sbYeshut.Append(string.IsNullOrEmpty(dr["mispar_sidur"].ToString()) ? "" : string.Concat(dr["mispar_sidur"].ToString(), ","));
                    sbYeshut.Append(string.IsNullOrEmpty(dr["shat_hatchala"].ToString()) ? "" : string.Concat(DateTime.Parse(dr["shat_hatchala"].ToString()).ToString("HH:mm"), ","));
                    sbYeshut.Append(string.IsNullOrEmpty(dr["shat_yetzia"].ToString()) ? "" : string.Concat(DateTime.Parse(dr["shat_yetzia"].ToString()).ToString("HH:mm"), ","));

                    if (string.IsNullOrEmpty(dr["mispar_knisa"].ToString()) || dr["mispar_knisa"].ToString() == "0")
                        sbYeshut.Append("");
                    else sbYeshut.Append(string.Concat(dr["mispar_knisa"].ToString(), ","));

                  //  sbYeshut.Append(string.IsNullOrEmpty(dr["mispar_knisa"].ToString()) ? "" : string.Concat(dr["mispar_knisa"].ToString(), ","));
                    
                    sbYeshut.Append(int.Parse(dr["check_num"].ToString()));
                    sbYeshut.Append(",");
                    sbYeshut.Append(i.ToString());
                    //arrYeshutId[i]=sbYeshut.ToString().Remove(sbYeshut.ToString().Length-1,1);
                    arrYeshutId[i] = sbYeshut.ToString();

                    arrTaarich[i] = (string.IsNullOrEmpty(dr["Taarich"].ToString()) ? DateTime.MinValue : DateTime.Parse(dr["Taarich"].ToString()));
                    arrMisparSidur[i] = string.IsNullOrEmpty(dr["mispar_sidur"].ToString()) ? 0 : (int)dr["mispar_sidur"];
                    arrShatHatchala[i] = string.IsNullOrEmpty(dr["shat_hatchala"].ToString()) ? DateTime.MinValue : DateTime.Parse(dr["shat_hatchala"].ToString());
                    arrShatYetzia[i] = string.IsNullOrEmpty(dr["shat_yetzia"].ToString()) ? DateTime.MinValue : DateTime.Parse(dr["shat_yetzia"].ToString());
                    arrMisparKnisa[i] = string.IsNullOrEmpty(dr["mispar_knisa"].ToString()) ? 0 : (int)dr["mispar_knisa"];
                       
                    //arrHeara[i] = dr["error_desc"].ToString();
                    i++;
                }

                oDal.AddParameter("MISPAR_ISHI", ParameterType.ntOracleInteger, arrMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("KOD_SHGIA", ParameterType.ntOracleInteger, arrKodShgia, ParameterDir.pdInput);
                oDal.AddParameter("YESHUT_ID", ParameterType.ntOracleVarchar, arrYeshutId, ParameterDir.pdInput);
                oDal.AddParameter("TAARICH", ParameterType.ntOracleDate, arrTaarich, ParameterDir.pdInput);
                oDal.AddParameter("MISPAR_SIDUR", ParameterType.ntOracleInteger, arrMisparSidur, ParameterDir.pdInput);
                oDal.AddParameter("SHAT_HATCHALA", ParameterType.ntOracleDate, arrShatHatchala, ParameterDir.pdInput);
                oDal.AddParameter("SHAT_YETZIA", ParameterType.ntOracleDate, arrShatYetzia, ParameterDir.pdInput);
                oDal.AddParameter("MISPAR_KNISA", ParameterType.ntOracleInteger, arrMisparKnisa, ParameterDir.pdInput);
                //oDal.AddParameter("HEARA", ParameterType.ntOracleVarchar, arrHeara, ParameterDir.pdInput);
                //// Set the command text on an OracleCommand object
                //oDal.ExecuteSQL("insert into TB_SHGIOT(MISPAR_ISHI,KOD_SHGIA,YESHUT_ID,TAARICH,MISPAR_SIDUR,SHAT_HATCHALA,SHAT_YETZIA,HEARA) values (:MISPAR_ISHI,:KOD_SHGIA,:YESHUT_ID,:TAARICH,:MISPAR_SIDUR,:SHAT_HATCHALA,:SHAT_YETZIA,:HEARA)");
                oDal.ExecuteSQL("insert into TB_SHGIOT(MISPAR_ISHI,KOD_SHGIA,YESHUT_ID,TAARICH,MISPAR_SIDUR,SHAT_HATCHALA,SHAT_YETZIA,MISPAR_KNISA) values (:MISPAR_ISHI,:KOD_SHGIA,:YESHUT_ID,:TAARICH,:MISPAR_SIDUR,:SHAT_HATCHALA,:SHAT_YETZIA,:MISPAR_KNISA)");          
              
                // //oDal.ExecuteSP("pro_del_tb_shgiot");
            }
            catch (Exception ex)
            {
                throw ex;
            }          
        }
       
        private void SetOvedYomAvodaDetails(int iMisparIshi, DateTime dCardDate)
        {
            try{
                  oOvedYomAvodaDetails = new clOvedYomAvoda(iMisparIshi, dCardDate);
                  _CardStatus =(clGeneral.enCardStatus)oOvedYomAvodaDetails.iStatus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //clDal oDal = new clDal();

            //try
            //{//מחזיר נתוני עובד: 
            //    oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
            //    oDal.AddParameter("p_date", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
            //    oDal.AddParameter("p_coll_meafeney_ovdim", ParameterType.ntOracleObject, oCollMeafyneyOved, ParameterDir.pdOutput, "COLL_MEAFYENEY_OVED");

            //    oDal.ExecuteSP(clGeneral.cProGetOvedYomAvodaUDT);

            //    oCollMeafyneyOved = (COLL_MEAFYENEY_OVED)oDal.GetObjectParam("p_coll_meafeney_ovdim");
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}
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
                oDal.ExecuteSP(clDefinitions.cProGetOvedMatzav,ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        private bool IsDuplicateShatYetiza(int iMisparIshi, DateTime dCardDate)
        {
            clDal oDal = new clDal();
            try
            {
                //בודקים אם ביום עבודה לעובד מסויים קיימות פעילויות עם אותה שעת יציאה
                //אם כן, נחזיר TRUE
                oDal.AddParameter("p_result", ParameterType.ntOracleInteger, null, ParameterDir.pdReturnValue);
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
                oDal.ExecuteSP(clDefinitions.cFnIsDuplicateShatYetiza);

                return int.Parse(oDal.GetValParam("p_result").ToString()) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsDuplicateTravle(int iMisparIshi, DateTime dCardDate,long lMakatNesia,DateTime dShatYetzia,int iMisparKnisa,ref DataTable dt)
        {
            clDal oDal = new clDal();
            try
            {
                //בודקים אם ישנה פעילות זהה
                //אם כן, נחזיר TRUE
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
                oDal.AddParameter("p_makat_nesia", ParameterType.ntOracleInt64, lMakatNesia, ParameterDir.pdInput);
                oDal.AddParameter("p_shat_yetzia", ParameterType.ntOracleDate, dShatYetzia, ParameterDir.pdInput);
                oDal.AddParameter("p_mispar_knisa", ParameterType.ntOracleInteger, iMisparKnisa , ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clDefinitions.cProIsDuplicateTravel, ref dt);

                return dt.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsSidurChofef(int iMisparIshi, DateTime dCardDate, int iMisparSidur, DateTime dShatHatchala, DateTime dShatGmar, int iParamChafifa,ref DataTable dt)
        {
            clDal oDal = new clDal();
            try
            {
                //בודקים אם ישנה פעילות זהה
                //אם כן, נחזיר TRUE
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
                oDal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger, iMisparSidur, ParameterDir.pdInput);
                oDal.AddParameter("p_shat_hatchala", ParameterType.ntOracleDate, dShatHatchala, ParameterDir.pdInput);
                oDal.AddParameter("p_shat_gmar", ParameterType.ntOracleDate, dShatGmar, ParameterDir.pdInput);
                oDal.AddParameter("p_param_chafifa", ParameterType.ntOracleInteger, iParamChafifa, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                
                oDal.ExecuteSP(clDefinitions.cProIsSidurChofef,ref dt);

                return dt.Rows.Count > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
      private bool CheckHaveSidurGrira(int iMisparIshi, DateTime dDateToCheck,ref DataTable dt)
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

        private bool CheckShgiotLetzuga(string sArrKodShgia)
        {
            clDal oDal = new clDal();
            try
            {
                //בודקים אם ישנה פעילות זהה
                //אם כן, נחזיר TRUE
                oDal.AddParameter("p_result", ParameterType.ntOracleInteger, null, ParameterDir.pdReturnValue);
                oDal.AddParameter("p_arr_kod_shgia", ParameterType.ntOracleVarchar, sArrKodShgia, ParameterDir.pdInput,300);
              
                oDal.ExecuteSP(clDefinitions.cFunCountShgiotLetzuga);

                return int.Parse(oDal.GetValParam("p_result").ToString()) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool MainOvedErrors(int iMisparIshi, DateTime dCardDate)
        {
            //M A I N   P R O C E D U R E
            //Get all oved details and calls all check function

            DataTable dtErrors = new DataTable();
            clDefinitions oDefinition = new clDefinitions();
            OrderedDictionary htFullSidurimDetails = new OrderedDictionary();
            //DataTable dtYamimMeyuchadim;
            clUtils oUtils = new clUtils();
            string sArrKodShgia;
            //clSidur oSidur;
            //int iSugYom;
            //int iCardDay;
            _bSuccsess = true;
            _iMisparIshi = iMisparIshi;
            _dCardDate = dCardDate;
            try
            {
                htFullEmployeeDetails = new OrderedDictionary();
                dtLookUp = oUtils.GetLookUpTables();
                _dtErrorsNotActive = clDefinitions.GetErrorsNoActive();

                //Get LookUp Tables
                if (!_IsExecuteInputData)
                { // רק אם לא רצו שינויי הקלט, נשלוף מחדש את הנתונים הכללים

                    //Get Parameters Table
                    //dtParameters = GetKdsParametrs();
                    dtYamimMeyuchadim = clGeneral.GetYamimMeyuchadim();
                    _dtSugeyYamimMeyuchadim = clGeneral.GetSugeyYamimMeyuchadim();
                    if (dtMutamut == null)
                        dtMutamut = oUtils.GetCtbMutamut();
                    _dtIdkuneyRashemet = clDefinitions.GetIdkuneyRashemet(iMisparIshi, dCardDate);
                    _dtIdkuneyRashemet.Columns.Add("update_machine", System.Type.GetType("System.Int32"));
                    //Get Meafyeny Ovdim
                    GetMeafyeneyOvdim(iMisparIshi, dCardDate);

                    iSugYom = clGeneral.GetSugYom(dtYamimMeyuchadim, dCardDate, _dtSugeyYamimMeyuchadim);//, _oMeafyeneyOved.iMeafyen56);

                    //Set global variable with parameters
                    SetParameters(dCardDate, iSugYom);

                 
                    //Get Meafyeney Sug Sidur
                    dtSugSidur = clDefinitions.GetSugeySidur();

                    //Get Temp Meafyeney Elements
                    dtTmpMeafyeneyElements = clDefinitions.GetTmpMeafyeneyElements(dCardDate, dCardDate);
                    //_dtApproval = clDefinitions.GetApprovalToEmploee(iMisparIshi, dCardDate);

                }
                SetOvedYomAvodaDetails(iMisparIshi, dCardDate);

               
                //Build Error DataTable
                try
                {
                    BuildErrorDataTable(ref dtErrors);
                }
                catch { return false; }

                //Get Oved Matzav
                if (dtMatzavOved == null)
                    dtMatzavOved = GetOvedMatzav(iMisparIshi, dCardDate);
                if (dtMatzavOved.Rows.Count > 0)
                {
                    dTarTchilatMatzav = DateTime.Parse(dtMatzavOved.Rows[0]["TAARICH_HATCHALA"].ToString());
                }
                // iCardDay =clGeneral.GetCardDay(dCardDate);
                //בדיקות ברמת יום עבודה
                //Get yom avoda details
                // dtOvedCardDetails = GetOvedYomAvodaDetails(iMisparIshi, dCardDate);
                // SetOvedYomAvodaDetails(iMisparIshi, dCardDate);
                //if (dtOvedCardDetails.Rows.Count>0)
                if (oOvedYomAvodaDetails.OvedDetailsExists)
                {
                    _oOvedYomAvodaDetails = oOvedYomAvodaDetails;

                   

                    //Check01
                   if (CheckErrorActive(1)) IsHrStatusValid01(dCardDate, iMisparIshi, ref dtErrors);

                    //Check30
                   if (CheckErrorActive(30)) IsSidurLina30(dCardDate, ref dtErrors);
                                        
                    //Check27
                   if (CheckErrorActive(27)) IsSimunNesiaValid27(dCardDate, ref dtErrors);

                    //Check163
                    //IsHashlamaLeYomAvodaValid163(iMisparIshi, dCardDate, ref dtErrors);

                    //Check 26
                    //IsZakaiLeNesiot26(iMisparIshi,dCardDate, ref dtErrors);

                    //Check 47
                   if (CheckErrorActive(47)) IsShbatHashlamaValid47(iMisparIshi, dCardDate, ref dtErrors);

                   
                    //Get Oved Details
                    dtDetails = oDefinition.GetOvedDetails(iMisparIshi, dCardDate);

                    if (dtDetails.Rows.Count > 0)
                    {
                        //Check36
                        if (CheckErrorActive(36)) IsHalbashValid36(dCardDate, ref dtErrors);

                        //Insert Oved Details to Class
                        //if (htEmployeeDetails == null)
                        //{
                            htEmployeeDetails = oDefinition.InsertEmployeeDetails(true, dtDetails, dCardDate, ref iLastMisaprSidur, out _htSpecialEmployeeDetails, ref htFullSidurimDetails);//, out  _htEmployeeDetailsWithCancled
                            htFullEmployeeDetails = htFullSidurimDetails;
                        //}

                        //מחיקת סידורי רציפות
                        //int iCountSidurim = htEmployeeDetails.Values.Count;
                        //int I = 0;

                        //if (iCountSidurim > 0)
                        //{
                        //    do
                        //    {
                        //        oSidur = (clSidur)htEmployeeDetails[I];
                        //        if (oSidur.iMisparSidur == 99500 || oSidur.iMisparSidur == 99501)
                        //        {
                        //            htEmployeeDetails.RemoveAt(I);
                        //            iCountSidurim -= 1;
                        //            I-=1;
                        //        }
                        //        I += 1;
                        //    } while (I < iCountSidurim);
                        //}

                        //Check103
                        if (CheckErrorActive(103)) IsDuplicateShatYetiza103(iMisparIshi, dCardDate, ref dtErrors);

                        //Check132
                        if (CheckErrorActive(132)) IsOvodaInMachalaAllowed132(dCardDate, iMisparIshi, ref dtErrors);

                        if (CheckErrorActive(192)) IsMatzavOvedNoValidFirstDay192(dCardDate, iMisparIshi, ref dtErrors);
                  
                        CheckAllError(ref dtErrors, dCardDate, iMisparIshi);
                    }
                    //Write errors to tb_shgiot
                    DeleteErrorsFromTbShgiot();
                    //RemoveShgiotNotActiveFromDt(ref dtErrors);
                    sArrKodShgia = "";
                    RemoveShgiotMeusharotFromDt(ref dtErrors, ref sArrKodShgia);
                    if (sArrKodShgia.Length > 0)
                    {
                        sArrKodShgia = sArrKodShgia.Substring(0, sArrKodShgia.Length - 1);
                        _bHaveShgiotLetzuga = CheckShgiotLetzuga(sArrKodShgia);
                    }
                    if (dtErrors.Rows.Count > 0)
                    {
                        InsertErrorsToTbShgiot(dtErrors, dCardDate);
                        _CardStatus = clGeneral.enCardStatus.Error;
                    }
                    else
                    {                        
                       _CardStatus = clGeneral.enCardStatus.Valid;
                    }
                    if (_CardStatus.GetHashCode() != oOvedYomAvodaDetails.iStatus)
                    {
                        clDefinitions.UpdateCardStatus(iMisparIshi, dCardDate, _CardStatus, _iUserId);
                    }

                    UpdateRitzatShgiotDate(iMisparIshi, dCardDate, _bHaveShgiotLetzuga);
                }

                _dtErrors = dtErrors;
                _IsExecuteErrors = true;
                return _bSuccsess;
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, iMisparIshi, "E", 0, dCardDate, "MainOvedErrors: " + ex.Message);
                return false;
            }
        }

        public static void UpdateRitzatShgiotDate(int iMisparIshi, DateTime dCardDate, bool bShgiotLetzuga)
        {
            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_date", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
                if (bShgiotLetzuga)
                {
                    oDal.AddParameter("p_shgiot_letzuga", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput);
                }
                else
                {
                    oDal.AddParameter("p_shgiot_letzuga", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }
                oDal.ExecuteSP(clDefinitions.cProUpdTarRitzatShgiot);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void RemoveShgiotMeusharotFromDt(ref DataTable dtErrors,ref string sArrKodShgia)
        {
            clWorkCard.ErrorLevel iErrorLevel;
            bool bMeushar;
            DataRow dr;
            int iCount;
            iCount = dtErrors.Rows.Count;
            int I = 0;
            try
            {
                sArrKodShgia = "";
                if (iCount > 0)
                {
                    do
                    {
                        bMeushar = false;
                        dr = dtErrors.Rows[I];
                        if (!(string.IsNullOrEmpty(dr["shat_yetzia"].ToString())))
                        {
                            iErrorLevel = clWorkCard.ErrorLevel.LevelPeilut;
                            bMeushar = clWorkCard.IsErrorApprovalExists(iErrorLevel, (int)dr["check_num"], (int)dr["mispar_ishi"], DateTime.Parse(dr["Taarich"].ToString()), (int)dr["mispar_sidur"], DateTime.Parse(dr["shat_hatchala"].ToString()), DateTime.Parse(dr["shat_yetzia"].ToString()), (int)dr["mispar_knisa"]);

                        }
                        else if (string.IsNullOrEmpty(dr["mispar_sidur"].ToString()))
                        {
                            iErrorLevel = clWorkCard.ErrorLevel.LevelYomAvoda;
                            bMeushar = clWorkCard.IsErrorApprovalExists(iErrorLevel, (int)dr["check_num"], (int)dr["mispar_ishi"], DateTime.Parse(dr["Taarich"].ToString()), 0, DateTime.MinValue, DateTime.MinValue, 0);

                        }
                        else
                        {
                            iErrorLevel = clWorkCard.ErrorLevel.LevelSidur;
                            bMeushar = clWorkCard.IsErrorApprovalExists(iErrorLevel, (int)dr["check_num"], (int)dr["mispar_ishi"], DateTime.Parse(dr["Taarich"].ToString()), (int)dr["mispar_sidur"], DateTime.Parse(dr["shat_hatchala"].ToString()), DateTime.MinValue, 0);
                        }


                        if (bMeushar)
                        {
                            dr.Delete();
                        }
                        else
                        {
                            sArrKodShgia += dr["check_num"].ToString() + ",";
                            I += 1; }

                        iCount = dtErrors.Rows.Count;
                    }
                    while (I < iCount);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //private void RemoveShgiotNotActiveFromDt(ref DataTable dtErrors)
        //{
        //   DataRow dr;
        //   DataRow[] drShgiaNotActive;
        //    int iCount;
        //    DataTable dtErrorsNotActive;
        //    iCount = dtErrors.Rows.Count;
        //    int I = 0;
        //    try
        //    {
        //        dtErrorsNotActive = clDefinitions.GetErrorsNoActive();
                
        //        if (iCount > 0)
        //        {
        //            do
        //            {
        //                dr = dtErrors.Rows[I];
        //                drShgiaNotActive=dtErrorsNotActive.Select("kod_shgia=" + dr["check_num"].ToString());
        //                if (drShgiaNotActive.Length>0)
        //                    dr.Delete();
        //                else
        //                  I+=1;
                            
        //                iCount = dtErrors.Rows.Count;
        //            }
        //            while (I < iCount);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        // }

        private bool CheckErrorActive(int iKodShgia)
        {
           DataRow[] drShgiaNotActive;
            
            try
            {
              
                drShgiaNotActive = _dtErrorsNotActive.Select("kod_shgia=" + iKodShgia);
                if (drShgiaNotActive.Length > 0)
                    return false;
                else
                    return true;
                   
            }
            catch (Exception ex)
            {
                throw ex;
            }

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

                dr = dtMatzavOved.Select(string.Concat("kod_matzav='",sKodMatzav+"'"));
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
            oMeafyeneyOved = new clMeafyenyOved(iMisparIshi, dCardDate);
          }
          catch (Exception ex)
          {
              throw ex;
          }
          }

        private void SetParameters(DateTime dCardDate, int iSugYom)
        {
            try{
              // oParam = clDefinitions.GetParamInstance(dCardDate, iSugYom);    
            oParam = new clParameters(dCardDate, iSugYom);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        

        private void BuildErrorDataTable(ref DataTable dtErrors)
        {
            DataColumn col = new DataColumn();
            try
            {
                col = new DataColumn("check_num", System.Type.GetType("System.Int32"));
                dtErrors.Columns.Add(col);

                col = new DataColumn("mispar_ishi", System.Type.GetType("System.Int32"));
                dtErrors.Columns.Add(col);

                col = new DataColumn("taarich", System.Type.GetType("System.DateTime"));
                dtErrors.Columns.Add(col);

                col = new DataColumn("mispar_sidur", System.Type.GetType("System.Int32"));
                dtErrors.Columns.Add(col);

                col = new DataColumn("shat_hatchala", System.Type.GetType("System.DateTime"));
                dtErrors.Columns.Add(col);

                col = new DataColumn("mispar_knisa", System.Type.GetType("System.Int32"));
                dtErrors.Columns.Add(col);

                col = new DataColumn("shat_yetzia", System.Type.GetType("System.DateTime"));
                dtErrors.Columns.Add(col);

                col = new DataColumn("sadot_nosafim", System.Type.GetType("System.Int32"));
                dtErrors.Columns.Add(col);

                col = new DataColumn("makat_nesia", System.Type.GetType("System.Int64"));
                dtErrors.Columns.Add(col);

                //col = new DataColumn("error_desc", System.Type.GetType("System.String"));
                //dtErrors.Columns.Add(col);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void InsertErrorRow(clSidur oSidur, ref DataRow drNew, string sErrorDesc, int iErrorNum)
        {
            try{
            drNew["check_num"] = iErrorNum;
            drNew["mispar_ishi"] = oSidur.iMisparIshi; 
            drNew["mispar_sidur"] = oSidur.iMisparSidur;
            drNew["taarich"] = oSidur.dSidurDate;
            drNew["shat_hatchala"] = (oSidur.sShatHatchala == null ? DateTime.MinValue : oSidur.dFullShatHatchala);
            }
            catch (Exception ex)
            {
                throw ex;
            }
                //drNew["makat_nesia"] = oSidur.//int.Parse(dr["makat_nesia"].ToString());
            //drNew["error_desc"] = sErrorDesc;
        }

        private void InsertPeilutErrorRow(clPeilut oPeilut, ref DataRow drNew)
        {
            try{
            drNew["Shat_Yetzia"] = string.IsNullOrEmpty(oPeilut.sShatYetzia) ? DateTime.MinValue : oPeilut.dFullShatYetzia;
            drNew["mispar_knisa"] = oPeilut.iMisparKnisa;
            drNew["makat_nesia"] = oPeilut.lMakatNesia;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string GetLookUpKods(string sTableName)
        {
            //The function get lookup table name and return all kods in string, separate by comma
            string sLookUp = "";
            DataRow[] drLookUpAll;
            try
            {
                drLookUpAll = dtLookUp.Select(string.Concat("table_name='", sTableName,"'"));
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


        private void CheckAllError(ref DataTable dtErrors, DateTime dCardDate, int iMisparIshi)
        {
            clSidur oSidur= new clSidur();
            clPeilut oPeilut = new clPeilut();
            clKavim oKavim = new clKavim();
            //int iKey = 0;
            //int iCount = 0;                       
            //string sPrevShatGmar = "";
            //string sPrevShatHatchala = "";
            //int iSidurPrevPitzulHafsaka=0;
            //DataRow drNew;
            bool bFirstSidur = true;
            //bool bPrevSidurEilat;                                               
            //int iPrevLoLetashlum=0;
            float fSidurTime = 0;
            int iTotalHashlamotForSidur = 0;//סה"כ השלמות בכל הסידורים 
            int iHashlama;
            bool bSidurNahagut=false;
            bool bHaveSidurNahagut = false;
            int iTotalTimePrepareMechineForSidur = 0;
            int iTotalTimePrepareMechineForDay = 0;
            int iTotalTimePrepareMechineForOtherMechines = 0;
            bool bCheckBoolSidur = false;
            clGeneral.enEmployeeType enEmployeeType;           
            DataRow[] drSugSidur;
            string sMakat;
            //DateTime dPrevStartPeilut=new DateTime();
            //DateTime dPrevEndPeilut = new DateTime();
            try
            {
              
//                enEmployeeType = (clGeneral.enEmployeeType)int.Parse(oOvedYomAvodaDetails.sKodHevra);
                enEmployeeType = (clGeneral.enEmployeeType)(oOvedYomAvodaDetails.iKodHevra);
       
                //נעבור על כל הסידורים
                //foreach (DictionaryEntry deEntry in htEmployeeDetails)
                //{                    
                //    iKey = int.Parse(deEntry.Key.ToString());   

                //HasHafifaBecauseOfHashlama108(ref dtErrors);
                if (CheckErrorActive(167)) HasHafifa167(iMisparIshi, dCardDate, ref dtErrors);

                // Check171
                if (CheckErrorActive(171)) HasBothSidurEilatAndSidurVisa171(iMisparIshi, dCardDate, ref dtErrors);

                // Check172
                if (CheckErrorActive(172)) IsOvedPeilutValid172(dCardDate, iMisparIshi, ref dtErrors);

                if (CheckErrorActive(203)) CheckNumGririotInDay203(dCardDate, iMisparIshi, ref dtErrors);

               
                
                for (int i = 0; i < htEmployeeDetails.Count; i++)
                {
                    oSidur = (clSidur)htEmployeeDetails[i];
                    ////set dataset sidurim with sidur details
                    //SetSidurDetails(dCardDate, ref oSidur, out iResult);

                    
                    //TODO: מה קורה אם סוג סידור 0
                    drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil,dCardDate,  _dtSugSidur);

                    //נחשב את זמן הסידור
                    fSidurTime = float.Parse((!string.IsNullOrEmpty(oSidur.sShatGmar) && !string.IsNullOrEmpty(oSidur.sShatHatchala) ? (oSidur.dFullShatGmar - oSidur.dFullShatHatchala).TotalMinutes : 0).ToString()); //clDefinitions.GetSidurTimeInMinuts(oSidur);

                    //שגיאה 86 - סה"כ זמן הכנת מכונה לסידור
                    iTotalTimePrepareMechineForSidur = 0;

                    //בדיקות ברמת סידור   
                    if (CheckErrorActive(9)) IsSidurExists9(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(15)) IsStartHourMissing15(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(174)) IsEndHourMissing174(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(33)) IsSidurChariga33(dCardDate, ref oSidur, ref dtErrors);
                    if (CheckErrorActive(20)) IsPitzulHafsakaValid20(i, ref oSidur, ref dtErrors);
                    if (CheckErrorActive(137)) IsHashlamaValid137(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(23)) IsShabatPizulValid23(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(96)) IsKmExists96(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(118)) IsOutMichsaValid118(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(40)) IsOutMichsaValid40(ref oSidur, ref dtErrors);
                    //IsHamaraValid42(ref oSidur, ref dtErrors);
                    //IsHamaratShabatValid39(drSugSidur,ref oSidur, ref dtErrors);
                    if (CheckErrorActive(136))  IsDriverLessonsNumberValid136(drSugSidur, ref oSidur, ref dtErrors);
                    if (CheckErrorActive(34)) IsZakaiLeChariga34(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(48)) IsHashlamaForSidurValid48(fSidurTime, ref oSidur, ref dtErrors);
                    //IsHalbashaInSidurValid37(ref oSidur, ref iCount);                   
                    if (CheckErrorActive(25)) IsPitzulAndNotZakai25(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(57)) IsSidurVisaValid57(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(22)) IsOneSidurValid22(i, ref oSidur, ref dtErrors);
                    if (CheckErrorActive(58)) IsVisaInSidurRagil58(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(14)) IsSidurStartHourValid14(dCardDate, ref oSidur, ref dtErrors);
                    if (CheckErrorActive(173)) IsSidurEndHourValid173(dCardDate, ref oSidur, ref dtErrors);
                    if (CheckErrorActive(207)) IsShatHatchalaBiggerShatYetzia207(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(209)) IsShatHatchalaLetashlumBiggerShatGmar209(ref oSidur, ref dtErrors);
                                      
                    if (CheckErrorActive(49)) IsHashlamatHazmanaValid49(fSidurTime, ref oSidur, ref dtErrors);
                    //IsYomVisaValid56(ref oSidur, ref dtErrors);                    
                    //IsZakaiLehamara44(drSugSidur,ref oSidur, ref dtErrors);
                    //IsHamaratShabatAllowed43(dCardDate, ref oSidur, ref dtErrors);
                    if (CheckErrorActive(106)) IsSidurVisaMissingSugVisa106(oSidur, ref dtErrors);
                    if (CheckErrorActive(178)) IsMissingKodMevatzeaVisa178(oSidur, ref dtErrors);
                    if (CheckErrorActive(127)) IsOnePeilutExists127(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(148)) IsSidurAllowedForEggedTaavora148(enEmployeeType, ref  oSidur, ref  dtErrors);
                    if (CheckErrorActive(124)) IsSidurNetzerNotValidForOved124(ref  oSidur, ref  dtErrors);
                    if (CheckErrorActive(160)) IsSidurAvodaValidForTaarich160(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(50)) IsSidurValidInShabaton50(ref oSidur, ref dtErrors);
                    //IsSidurTafkidValid145(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(32)) IsCharigaValid32(ref oSidur, ref dtErrors);

                    if (CheckErrorActive(164)) IsSidurSummerValid164(ref  oSidur, ref dtErrors);
                    if (CheckErrorActive(161)) IsSidurNAhagutValid161(drSugSidur, ref oSidur, ref dtErrors);
                    if (CheckErrorActive(195)) IsFirstDayShlilatRishayon195(drSugSidur, ref oSidur, ref dtErrors);
            ////        if (CheckErrorActive(196)) IsSidurKupaiLeloHachtama196(i,drSugSidur, ref oSidur, ref dtErrors);
                    //IsSidurGriraValid177(drSugSidur, ref oSidur, ref dtErrors);
                   
                    if (!(bFirstSidur))//לא נבצע את הבדיקה לסידור הראשון
                    {
                        if (CheckErrorActive(16)) IsSidurimHoursNotValid16(i, ref oSidur, ref dtErrors);
                        if (CheckErrorActive(24)) IsPitzulSidurInShabatValid24(dCardDate, i, ref oSidur, ref dtErrors);
                        
                    }
                    else
                    {//רק לסידור הראשון נבצע את בדיקה ,35,141
                        //IsSidurExistsInShlila35(iMisparIshi,dCardDate,ref dtErrors);
                        //IsOvedEggedTaavoraKodValid141(enEmployeeType, ref oSidur, ref dtErrors);                        
                       
                        //סידור יחיד ביום
                        if (CheckErrorActive(45)) IsHashlamaForComputerAndAccidentValid45(ref oSidur, ref dtErrors);                        
                    }
                    
                    //נתונים של סידור קודם
                    //sPrevShatGmar = oSidur.sShatGmar;
                    //iPrevLoLetashlum = oSidur.iLoLetashlum;
                    //iSidurPrevPitzulHafsaka = string.IsNullOrEmpty(oSidur.sPitzulHafsaka) ? 0 : int.Parse(oSidur.sPitzulHafsaka);
                    
                    //נשמור את הנתון אם סידור אילת של הסידור הקודם - )
                    //bPrevSidurEilat = oSidur.bSidurEilat;
                    
                    //סה"כ השלמות בכל הסידורים, נבדוק אם אין חריגה בבדיקה 142
                    iHashlama =string.IsNullOrEmpty(oSidur.sHashlama) ? 0 : int.Parse(oSidur.sHashlama);
                    if (iHashlama > 0)
                    {
                        iTotalHashlamotForSidur = iTotalHashlamotForSidur + 1;
                        //check 142
                        if (CheckErrorActive(142)) IsTotalHashlamotInCardValid142(iTotalHashlamotForSidur, ref oSidur, ref dtErrors);
              
                    }
                     bSidurNahagut = IsSidurNahagut(drSugSidur, oSidur);
                     if (bSidurNahagut) bHaveSidurNahagut = true;
                     if (CheckErrorActive(156)) IsSidurMiluimAndAvoda156(oSidur, ref dtErrors);
                     if (CheckErrorActive(143)) IsSidurMissingNumStore143(oSidur, ref dtErrors);
                     if (CheckErrorActive(152)) IsChafifaBesidurNihulTnua152(drSugSidur, oSidur, ref dtErrors);
                     if (CheckErrorActive(153)) IsHighPremya153(oSidur, ref dtErrors, bSidurNahagut, ref bCheckBoolSidur);
                     if (CheckErrorActive(154)) IsNegativePremya154(oSidur, ref dtErrors, bSidurNahagut, ref bCheckBoolSidur);
                     if (CheckErrorActive(186)) MutamLoBeNahagut186(oSidur,dCardDate, bSidurNahagut, ref dtErrors);
                     if (CheckErrorActive(187)) KupaiWithNihulTnua187(oSidur, dCardDate,drSugSidur, ref dtErrors);
                     if (CheckErrorActive(188)) ChofeshAlCheshbonShaotNosafot188(oSidur, dCardDate, ref dtErrors);
                     if (CheckErrorActive(208)) CheckMushalETWithSidurNotAllowET208(oSidur, dCardDate, ref dtErrors);
                    clSidur prevSidur = null;
                    if (i > 0) prevSidur = htEmployeeDetails[i - 1] as clSidur;
                    if (prevSidur != null)
                    {
                        if (CheckErrorActive(168)) IsCurrentSidurInPrevSidur168(i,ref oSidur, ref prevSidur, ref dtErrors);
                    }
                    //IsOvedExistsInWorkDay169(ref oSidur, iMisparIshi, dCardDate, ref dtErrors);

                    //IsValidSidurVisa170(ref oSidur, iMisparIshi, dCardDate, ref dtErrors);
                    if (CheckErrorActive(175)) IsHachtamaYadanitKnisaMissing175(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(176)) IsHachtamaYadanitYetziaMissing176(ref oSidur, ref dtErrors);

                    if (CheckErrorActive(180)) IsShatHatchalaLetashlumNull180(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(181)) IsShatGmarLetashlumNull180(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(190)) SidurLoTakefLetarich190(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(191)) IsukNahagImSidurTafkidNoMefyen191(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(193)) DivuachSidurLoMatimLeisuk193(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(194)) DivuachSidurLoMatimLeisuk194(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(197)) HachtamatKnisaLoBmakomHasaka197(ref oSidur,  ref dtErrors);
                    if (CheckErrorActive(198)) HachtamatYetziaLoBmakomHasaka198(ref oSidur, ref dtErrors);

                    if (CheckErrorActive(199)) AvodaByemeyTeuna199(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(200)) AvodaByemeyEvel200(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(201)) AvodaByemeyMachala201(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(202)) MachalaLeloIshurwithSidurLetashlum202(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(204)) SidurAsurBeShisiLeoved5Yamim204(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(205)) CheckMichsatTipatChalav205(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(206)) OvedMutaamLeloShaotNosafot206(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(210)) CheckMichsatMachalaYeledImMugbalut210(ref oSidur, ref dtErrors);
                    if (CheckErrorActive(212)) SidurMachalaMisradHaBitachonLoYachid212(ref oSidur, ref dtErrors);
                   
                    clPeilut oPrevPeilut = null;
                    //bool change = true;
                    int numPrev;
                    //foreach (DictionaryEntry dePeilutEntry in oSidur.htPeilut)
                    for (int j = 0; j < ((KdsBatch.clSidur)(htEmployeeDetails[i])).htPeilut.Count; j++)
                    {
                        //iKey = int.Parse(dePeilutEntry.Key.ToString());
                        oPeilut = (clPeilut)oSidur.htPeilut[j];
                        if (j==0 && oPeilut.iMisparKnisa == 0)
                            oPrevPeilut = oPeilut;
                     
                        
                        //IsShatYetizaExist92(ref oSidur,ref oPeilut, ref dtErrors);
                        if (CheckErrorActive(81)) IsKodNesiaExists81(ref  oSidur, ref  oPeilut, ref  dtErrors, dCardDate);
                        if (CheckErrorActive(139)) MisparSiduriOtoNotExists139(ref oPeilut, ref oSidur, ref dtErrors);
                        //IsMisparSidurEilatInRegularSidurExists140(ref oPeilut, ref oSidur, ref dtErrors);
                        //IsPeilutShatYeziaValid113(ref  oSidur, ref  oPeilut, ref  dtErrors, dCardDate);
                        if (CheckErrorActive(129)) IsElementTimeValid129(fSidurTime, ref  oSidur, ref  oPeilut, ref dtErrors);
                        if (CheckErrorActive(121)) IsShatPeilutNotValid121(dCardDate, ref oSidur, ref oPeilut, ref dtErrors);
                        if (CheckErrorActive(84)) IsPeilutInSidurValid84(ref oSidur, ref oPeilut, ref dtErrors);
                        if (CheckErrorActive(69)) IsOtoNoValid69(dCardDate, ref oSidur, ref oPeilut, ref dtErrors);
                        if (CheckErrorActive(211)) IsRechevMushbat211(dCardDate, ref oSidur, ref oPeilut, ref dtErrors);
                        if (CheckErrorActive(31)) IsZakaiLina31(ref oSidur, ref oPeilut, ref dtErrors);
                        if (CheckErrorActive(68)) IsOtoNoExists68(drSugSidur, ref oSidur, ref oPeilut, ref dtErrors);
                        //IsNesiaTimeNotValid91(fSidurTime, dCardDate, ref oSidur, ref oPeilut, ref dtErrors);
                        if (CheckErrorActive(52)) IsTeoodatNesiaValid52(ref oSidur, ref oPeilut, ref dtErrors);
                        if (CheckErrorActive(87)) HighValueKisuyTor87(oSidur, oPeilut, ref dtErrors);
                        if (CheckErrorActive(123)) ElementInSpecialSidurNotAllowed123(ref oSidur, ref oPeilut, ref dtErrors);
                        if (CheckErrorActive(125)) IsNesiaInSidurVisaAllowed125(ref oSidur, ref oPeilut, ref dtErrors);
                        if (CheckErrorActive(166)) IsHmtanaTimeValid166(ref oSidur, ref oPeilut, ref dtErrors);
                        if (CheckErrorActive(13)) IsSidurNamlakWithoutNesiaCard13(ref oSidur, ref oPeilut, ref dtErrors);

                        if (j > 0 && oPeilut.iMisparKnisa==0)//לא נבצע את הבדיקה לפעילות הראשונה 
                        {
                            //oPrevPeilut = (clPeilut)oSidur.htPeilut[j - 1];
                            if (CheckErrorActive(162)) IsCurrentPeilutInPrevPeilut162(ref oSidur, ref oPeilut, ref oPrevPeilut, ref dtErrors);
                            oPrevPeilut = oPeilut;
                        }
                        if (CheckErrorActive(86)) IsTimeForPrepareMechineValid86(ref iTotalTimePrepareMechineForSidur, ref iTotalTimePrepareMechineForDay, ref iTotalTimePrepareMechineForOtherMechines, ref oSidur, ref oPeilut, ref dtErrors);
                        if (CheckErrorActive(151)) IsDuplicateTravel151(ref  oSidur, ref oPeilut, ref dtErrors);
                        if (CheckErrorActive(179)) HightValueDakotBefoal179(oSidur, oPeilut, ref dtErrors);
                        if (CheckErrorActive(189)) KisuyTorLifneyHatchalatSidur189(oSidur, oPeilut, ref dtErrors);
                    }
                    if (CheckErrorActive(185)) ErrMisparElementimMealHamutar185(dCardDate, ref oSidur, ref dtErrors);

                    if (!bFirstSidur)
                    {
                        //Check55
                        if (CheckErrorActive(55)) IsSidurEilatValid55(i, dCardDate, oSidur, ref dtErrors);
                    }
                    bFirstSidur = false;

                    //Check 86
                    if (CheckErrorActive(86)) CheckPrepareMechineForSidurValidity86(ref oSidur, iTotalTimePrepareMechineForSidur, ref dtErrors);
                   
                }
                if (!CheckIdkunRashemet("BITUL_ZMAN_NESIOT"))
                {
                    if (CheckErrorActive(165)) IsAvodatNahagutValid165(iMisparIshi, dCardDate, bHaveSidurNahagut, ref dtErrors);
                }
                ////שגיאה 37 - נבדוק אם לכל הסידורים לא מגיע הלבשה
                //if (iCount == htEmployeeDetails.Count)
                //{
                //    drNew = dtErrors.NewRow();
                //    drNew["mispar_ishi"] =iMisparIshi; 
                //    drNew["mispar_sidur"] = 0;
                //    drNew["taarich"] = dCardDate.ToShortDateString(); 
                //    //drNew["error_desc"]= "לא מגיעה הלבשה לסידורים";
                //    drNew["check_num"]=enErrors.errHalbashaInSidurNotValid.GetHashCode();
                //    dtErrors.Rows.Add(drNew);
                //}
                 //Check 86
                if (CheckErrorActive(86))
                {
                    CheckPrepareMechineForDayValidity86(iMisparIshi, iTotalTimePrepareMechineForDay, dCardDate, ref dtErrors);
                    //Check86
                    CheckPrepareMechineOtherElementForDayValidity86(iMisparIshi, iTotalTimePrepareMechineForOtherMechines, dCardDate, ref dtErrors);
                }
              if (htEmployeeDetails.Count > 0)
                {
                    if (CheckErrorActive(150)) IsNesiaMeshtanaDefine150(dCardDate, ref dtErrors);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private bool IsStartHourMissing15(ref clSidur oSidur, ref DataTable dtErrors)
        {   //בדיקה ברמת סידור         
            DataRow drNew;
            bool isValid = true;
            try
            {
                if (oSidur.dFullShatHatchala.Year < clGeneral.cYearNull)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "חסרה שעת התחלה", enErrors.errStartHourMissing.GetHashCode());
                    dtErrors.Rows.Add(drNew);

                    isValid = false;
                }
                         
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errStartHourMissing.GetHashCode(), oSidur.iMisparIshi, null, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsStartHourMissing15: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsEndHourMissing174(ref clSidur oSidur, ref DataTable dtErrors)
        {   //בדיקה ברמת סידור         
            DataRow drNew;
            bool isValid = true;
            try
            {
                
                if (string.IsNullOrEmpty(oSidur.sShatGmar))
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "חסרה שעת גמר", enErrors.errEndHourMissing.GetHashCode());
                    dtErrors.Rows.Add(drNew);

                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errEndHourMissing.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsEndHourMissing174: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsHachtamaYadanitKnisaMissing175(ref clSidur oSidur, ref DataTable dtErrors)
        {   //בדיקה ברמת סידור         
            DataRow drNew;
            bool isValid = true;
            try
            {
                if (oSidur.iKodSibaLedivuchYadaniIn == 0)
                {
                    if (oSidur.bSidurMyuhad && !string.IsNullOrEmpty(oSidur.sShaonNochachut) && (string.IsNullOrEmpty(oSidur.sMikumShaonKnisa) || oSidur.sMikumShaonKnisa=="0") && CheckHourValid(oSidur.sShatHatchala))
                    {
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "חסרה סיבת אי החתמה ידנית כניסה", enErrors.errHachtamaYadanitKnisa.GetHashCode());
                        dtErrors.Rows.Add(drNew);

                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errHachtamaYadanitKnisa.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsHachtamaYadanitKnisaMissing175: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsHachtamaYadanitYetziaMissing176(ref clSidur oSidur, ref DataTable dtErrors)
        {   //בדיקה ברמת סידור         
            DataRow drNew;
            bool isValid = true;
            try
            {
                if (oSidur.iKodSibaLedivuchYadaniOut == 0)
                {
                    if (oSidur.bSidurMyuhad && !string.IsNullOrEmpty(oSidur.sShaonNochachut) && (string.IsNullOrEmpty(oSidur.sMikumShaonYetzia) || oSidur.sMikumShaonYetzia=="0") && CheckHourValid(oSidur.sShatGmar))
                    {
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "חסרה סיבת אי החתמה ידנית יציאה", enErrors.errHachtamaYadanitYetzia.GetHashCode());
                        dtErrors.Rows.Add(drNew);

                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errHachtamaYadanitYetzia.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsHachtamaYadanitYetziaMissing176: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }
        private bool IsShatHatchalaLetashlumNull180(ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור         
            DataRow drNew;
            bool isValid = true;
            try
            {
                if (oSidur.dFullShatHatchalaLetashlum == DateTime.MinValue || oSidur.dFullShatHatchalaLetashlum.Date == DateTime.MinValue.Date)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "חסרה שעת התחלה לתשלום", enErrors.IsShatHatchalaLetashlumNull.GetHashCode());
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.IsShatHatchalaLetashlumNull.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsShatHatchalaLetashlumNull180: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }
            return isValid;
        }


        private bool IsShatGmarLetashlumNull180(ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור         
            DataRow drNew;
            bool isValid = true;
            try
            {
                if (oSidur.dFullShatGmarLetashlum == DateTime.MinValue || oSidur.dFullShatGmarLetashlum.Date == DateTime.MinValue.Date)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "חסרה שעת גמר לתשלום", enErrors.IsShatGmarLetashlumNull.GetHashCode());
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.IsShatGmarLetashlumNull.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsShatGmarLetashlumNull181: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }
            return isValid;
        }

        private bool ErrMisparElementimMealHamutar185(DateTime dCardDate,ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור         
            DataRow drNew;
            bool isValid = true;
            clPeilut oPeilut; // = new clPeilut();
            string sMakat;
            int iNumElements730=0, iNumElements740=0, iNumElements750=0;
            try
            {
                DataRow[] drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, dCardDate, _dtSugSidur);
                if (drSugSidur.Length > 0)
                {
                    if (drSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nihul.GetHashCode().ToString())
                    {
                        for (int j = 0; j < oSidur.htPeilut.Count; j++)
                        {
                            oPeilut = (clPeilut)oSidur.htPeilut[j];
                            sMakat = oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3);
                            if (sMakat == "730") 
                                iNumElements730 += 1;
                             if (sMakat == "740") 
                                iNumElements740 += 1;
                             if (sMakat == "750") 
                                iNumElements750 += 1;
                        }
                        if ((iNumElements730 > 0 && iNumElements740 > 0) || (iNumElements740 > 0 && iNumElements750 > 0) || (iNumElements730 > 0 && iNumElements750 > 0))
                        {
                            drNew = dtErrors.NewRow();
                            InsertErrorRow(oSidur, ref drNew, "סידור ניהול תנועה מהמפה המכיל יותר מסוג אחד של אלמנט מסוג ניהול תנועה", enErrors.ErrMisparElementimMealHamutar.GetHashCode());
                            dtErrors.Rows.Add(drNew);
                            isValid = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.ErrMisparElementimMealHamutar.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "ErrMisparElementimMealHamutar: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }
            return isValid;
        }


        private bool SidurLoTakefLetarich190(ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור         
            DataRow drNew;
            bool isValid = true;
            try
            {
                if (oSidur.sTokefHatchala.Length > 0 && oSidur.sTokefSiyum.Length > 0 && (_dCardDate < DateTime.Parse(oSidur.sTokefHatchala) || _dCardDate > DateTime.Parse(oSidur.sTokefSiyum)))
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "סידור לא תקף לתאריך", enErrors.errSidurLoTakefLetaarich.GetHashCode());
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errSidurLoTakefLetaarich.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "SidurLoTakefLetarich190: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }
            return isValid;
        }

        private bool IsukNahagImSidurTafkidNoMefyen191(ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור         
            DataRow drNew;
            bool isValid = true;
            try
            {
                if (oOvedYomAvodaDetails.iIsuk >= 500 && oOvedYomAvodaDetails.iIsuk <= 600 && oSidur.iLoLetashlum == 1 && (oSidur.iKodSibaLoLetashlum == 4 || oSidur.iKodSibaLoLetashlum == 5 || oSidur.iKodSibaLoLetashlum == 17))
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "עיסוק נהג עם סידור תפקיד ללא מאפיינים", enErrors.errIsukNahagImSidurTafkidNoMefyen.GetHashCode());
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errIsukNahagImSidurTafkidNoMefyen.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsukNahagImSidurTafkidNoMefyen191: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }
            return isValid;
        }



        private bool IsMatzavOvedNoValidFirstDay192(DateTime dCardDate, int iMisparIshi, ref DataTable dtErrors)
        {
            bool isValid = true;
            int iCountSidurim;
            try
            {
                //בדיקה ברמת יום עבודה
                if (dTarTchilatMatzav == dCardDate)
                {
                    iCountSidurim = htEmployeeDetails.Values.Cast<clSidur>().ToList().Count(Sidur => Sidur.iNitanLedaveachBemachalaAruca == 0 && Sidur.iLoLetashlum == 0);
                    //עובד לא יכול לעבוד בכל עבודה באגד אם במחלה. מחלה זה סטטוס ב- HR. מזהים שהעובד עבד ביום מסוים אם יש לו לפחות סידור אחד ביום זה.  
                    if ((IsOvedMatzavExists("5")) && (iCountSidurim > 0))
                    {
                        drNew = dtErrors.NewRow();
                        drNew["mispar_ishi"] = iMisparIshi;
                        drNew["check_num"] = enErrors.errMatzavOvedNotValidFirstDay.GetHashCode();
                        drNew["taarich"] = dCardDate.ToShortDateString();
                       
                        dtErrors.Rows.Add(drNew);
                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, iMisparIshi, "E", enErrors.errMatzavOvedNotValidFirstDay.GetHashCode(), dCardDate, "IsMatzavOvedNoValidFirstDay192: " + ex.Message);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool DivuachSidurLoMatimLeisuk193(ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור         
            DataRow drNew;
            bool isValid = true;
            try
            {
                if (oOvedYomAvodaDetails.iIsuk == 420  && oSidur.iMisparSidur == 99001 && oSidur.iKodSibaLedivuchYadaniIn>0 &&  oSidur.iKodSibaLedivuchYadaniOut>0)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "עיסוק העובד מנס-סדרן ודווח סידור 99001. יש לדווח סידור 99224", enErrors.errDivuachSidurLoMatimLeisuk420.GetHashCode());
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errDivuachSidurLoMatimLeisuk420.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "DivuachSidurLoMatimLeisuk193: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }
            return isValid;
        }

        private bool DivuachSidurLoMatimLeisuk194(ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור         
            DataRow drNew;
            bool isValid = true;
            try
            {
                if (oOvedYomAvodaDetails.iIsuk == 422 && oSidur.iMisparSidur == 99001 && oSidur.iKodSibaLedivuchYadaniIn > 0 && oSidur.iKodSibaLedivuchYadaniOut > 0)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "עיסוק העובד מנס-פקח ודווח סידור 99001. יש לדווח סידור 99225 ", enErrors.errDivuachSidurLoMatimLeisuk422.GetHashCode());
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errDivuachSidurLoMatimLeisuk422.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "DivuachSidurLoMatimLeisuk194: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }
            return isValid;
        }


        private bool HachtamatKnisaLoBmakomHasaka197(ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור         
            bool isValid = true;
            bool bError = false;
            try
            {
                bError = !(oSidur.bIsKnisaTkina_err197);
            
                if (bError)  
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "החתמת כניסה לא במקום העסקה", enErrors.errHachtamatKnisaLoBmakomHasaka197.GetHashCode());
                    dtErrors.Rows.Add(drNew);

                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errHachtamatKnisaLoBmakomHasaka197.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "HachtamatKnisaLoBmakomHasaka197: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }
            return isValid;
        }

        private bool HachtamatYetziaLoBmakomHasaka198(ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור         
            bool isValid = true;
            bool bError = false;
            try
            {
                bError = !(oSidur.bIsYetziaTkina_err198);
              
                if (bError) 
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "החתמת יציאה לא במקום העסקה", enErrors.errHachtamatYetziaLoBmakomHasaka198.GetHashCode());
                    dtErrors.Rows.Add(drNew);

                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errHachtamatYetziaLoBmakomHasaka198.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "HachtamatKnisaLoBmakomHasaka197: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }
            return isValid;
        }


        private bool AvodaByemeyTeuna199(ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור         
            bool isValid = true;
            bool bError = false;
            DateTime dTaarichKodem;
            DataTable dtSidurim;
            clDefinitions oDefinition = new clDefinitions();
            try
            {
                if (oSidur.bSidurMyuhad)
                {//סידור מיוחד
                    if (!string.IsNullOrEmpty(oSidur.sHeadrutTypeKod) && oSidur.sHeadrutTypeKod == clGeneral.enMeafyenSidur53.enTeuna.GetHashCode().ToString()
                        && (oSidur.iLoLetashlum == 0 || (oSidur.iLoLetashlum == 1 && oSidur.iKodSibaLoLetashlum == 22)))
                    {
                        dTaarichKodem = _dCardDate.AddDays(-1);
                        if (clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, iSugYom, dTaarichKodem) ||
                            dTaarichKodem.DayOfWeek == DayOfWeek.Friday)
                        {
                            dtSidurim = oDefinition.GetOvedDetails(_iMisparIshi, dTaarichKodem);
                            if (CheckAnozerSidurExsits(oSidur) && CheckSidurHeadrutExsits(dtSidurim, clGeneral.enMeafyenSidur53.enTeuna.GetHashCode().ToString(), oSidur.iMisparSidur))
                                bError = true;
                        }

                        if (!bError)
                        {
                            if (clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, iSugYom, dTaarichKodem) ||
                                (dTaarichKodem.DayOfWeek == DayOfWeek.Friday &&
                                (oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())))
                            {
                                dTaarichKodem = dTaarichKodem.AddDays(-1);

                                dtSidurim = oDefinition.GetOvedDetails(_iMisparIshi, dTaarichKodem);
                                if (CheckAnozerSidurExsits(oSidur) && CheckSidurHeadrutExsits(dtSidurim, clGeneral.enMeafyenSidur53.enTeuna.GetHashCode().ToString(), oSidur.iMisparSidur))
                                    bError = true;
                            }


                            if (!bError)
                            {
                                if (dTaarichKodem.DayOfWeek == DayOfWeek.Friday &&
                                   (oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode()))
                                    dTaarichKodem = dTaarichKodem.AddDays(-1);

                                dtSidurim = oDefinition.GetOvedDetails(_iMisparIshi, dTaarichKodem);
                                if (CheckAnozerSidurExsits(oSidur) && CheckSidurHeadrutExsits(dtSidurim, clGeneral.enMeafyenSidur53.enTeuna.GetHashCode().ToString(), oSidur.iMisparSidur))
                                    bError = true;
                            }

                        }
                    }
                }

                if (bError)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "לא יום ראשון של תאונה - עבודה ותאונה באותו יום", enErrors.errAvodaByemeyTeuna199.GetHashCode());
                    dtErrors.Rows.Add(drNew);

                    isValid = false;
                }                
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errAvodaByemeyTeuna199.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "HachtamatKnisaLoBmakomHasaka197: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }
            return isValid;
        }

        private bool AvodaByemeyEvel200(ref clSidur oSidur, ref DataTable dtErrors)
        {
            bool isValid = true;
            bool bError = false;
            DateTime dTaarichKodem;
            DataTable dtSidurim;
            clDefinitions oDefinition = new clDefinitions();
            try
            {
                if (oSidur.bSidurMyuhad)
                {//סידור מיוחד
                    if (!string.IsNullOrEmpty(oSidur.sHeadrutTypeKod) && oSidur.sHeadrutTypeKod == clGeneral.enMeafyenSidur53.enEvel.GetHashCode().ToString()
                        && (oSidur.iLoLetashlum == 0 || (oSidur.iLoLetashlum == 1 && oSidur.iKodSibaLoLetashlum == 22)))
                    {
                        dTaarichKodem = _dCardDate.AddDays(-1);
                        if (clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, iSugYom, dTaarichKodem) ||
                            dTaarichKodem.DayOfWeek == DayOfWeek.Friday)
                        {
                            dtSidurim = oDefinition.GetOvedDetails(_iMisparIshi, dTaarichKodem);
                            if (CheckAnozerSidurExsits(oSidur) && CheckSidurHeadrutExsits(dtSidurim, clGeneral.enMeafyenSidur53.enEvel.GetHashCode().ToString(), oSidur.iMisparSidur))
                                bError = true;
                        }

                        if (!bError)
                        {
                            if (clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, iSugYom, dTaarichKodem) ||
                                (dTaarichKodem.DayOfWeek == DayOfWeek.Friday &&
                                (oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())))
                            {
                                dTaarichKodem = dTaarichKodem.AddDays(-1);

                                dtSidurim = oDefinition.GetOvedDetails(_iMisparIshi, dTaarichKodem);
                                if (CheckAnozerSidurExsits(oSidur) && CheckSidurHeadrutExsits(dtSidurim, clGeneral.enMeafyenSidur53.enEvel.GetHashCode().ToString(), oSidur.iMisparSidur))
                                    bError = true;
                            }

                            if (!bError)
                            {
                                if (dTaarichKodem.DayOfWeek == DayOfWeek.Friday &&
                                   (oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode()))
                                    dTaarichKodem = dTaarichKodem.AddDays(-1);

                                dtSidurim = oDefinition.GetOvedDetails(_iMisparIshi, dTaarichKodem);
                                if (CheckAnozerSidurExsits(oSidur) && CheckSidurHeadrutExsits(dtSidurim, clGeneral.enMeafyenSidur53.enEvel.GetHashCode().ToString(), oSidur.iMisparSidur))
                                    bError = true;
                            }

                        }
                    }
                }

                if (bError)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "לא יום ראשון של  אבל - עבודה ואבל באותו יום", enErrors.errAvodaByemeyEvel200.GetHashCode());
                    dtErrors.Rows.Add(drNew);

                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errAvodaByemeyEvel200.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "HachtamatKnisaLoBmakomHasaka197: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }
            return isValid;
        }

        private bool AvodaByemeyMachala201(ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור         
            bool isValid = true;
            bool bError = false;
            DataTable dtSidurim;
            DateTime dTaarichKodem;
            clDefinitions oDefinition = new clDefinitions();
            try
            {
                if (oSidur.bSidurMyuhad)
                {//סידור מיוחד
                    if (!string.IsNullOrEmpty(oSidur.sHeadrutTypeKod) && oSidur.iSidurLebdikatRezefMachala>0 && oSidur.sHeadrutTypeKod == clGeneral.enMeafyenSidur53.enMachala.GetHashCode().ToString()
                        && (oSidur.iLoLetashlum == 0 || (oSidur.iLoLetashlum == 1 && oSidur.iKodSibaLoLetashlum  ==22)))
                    {
                        dTaarichKodem=_dCardDate.AddDays(-1);   
                        if (clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, iSugYom, dTaarichKodem) ||
                            dTaarichKodem.DayOfWeek == DayOfWeek.Friday )
                          //  (oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode() || oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek2.GetHashCode())))
                        {
                            dtSidurim = oDefinition.GetOvedDetails(_iMisparIshi, dTaarichKodem);
                            if (CheckAnozerSidurExsits(oSidur) && CheckMachalaExsitsYomKodem(dtSidurim))
                                bError = true;
                        }

                        if (!bError)
                        {
                            if (clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, iSugYom, dTaarichKodem) ||
                                (dTaarichKodem.DayOfWeek == DayOfWeek.Friday &&
                                (oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())))
                            {
                                dTaarichKodem = dTaarichKodem.AddDays(-1);
                                dtSidurim = oDefinition.GetOvedDetails(_iMisparIshi, dTaarichKodem);
                                if (CheckAnozerSidurExsits(oSidur) && CheckMachalaExsitsYomKodem(dtSidurim))
                                    bError = true;
                            }

                            if (!bError)
                            {
                                 if (dTaarichKodem.DayOfWeek == DayOfWeek.Friday &&
                                    (oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode()))
                                     dTaarichKodem = dTaarichKodem.AddDays(-1);
                                 
                                dtSidurim = oDefinition.GetOvedDetails(_iMisparIshi, dTaarichKodem);
                                 if (CheckAnozerSidurExsits(oSidur) && CheckMachalaExsitsYomKodem(dtSidurim))
                                     bError = true;
                            }
                            
                        }
                        
                    }
                }

                if (bError)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "לא יום ראשון של  מחלת עובד - עבודה ומחלת עובד באותו יום", enErrors.errAvodaByemeyMachala201.GetHashCode());
                    dtErrors.Rows.Add(drNew);

                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errAvodaByemeyMachala201.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "HachtamatKnisaLoBmakomHasaka197: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }
            return isValid;
        }

        private bool MachalaLeloIshurwithSidurLetashlum202(ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור         
            bool isValid = true;
            bool bError = false;
            try
            {
                if (oSidur.iMisparSidur==99816)
                {
                    if (htEmployeeDetails.Count > 0)
                    {
                        foreach (clSidur oSidurElse in htEmployeeDetails.Values)
                        {
                            if (oSidur != oSidurElse && oSidurElse.iLoLetashlum == 0)
                            {
                                 bError = true;
                                 break;
                            }
                        }
                    }
                }

                if (bError)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "לא ניתן לדווח סידור מחלה ללא אישור בשילוב עם סידור נוסף ", enErrors.errMachalaLeloIshur202.GetHashCode());
                    dtErrors.Rows.Add(drNew);

                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errAvodaByemeyMachala201.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "HachtamatKnisaLoBmakomHasaka197: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }
            return isValid;
        }


        private bool SidurAsurBeShisiLeoved5Yamim204(ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור         
            bool isValid = true;
            bool bError = false;
            try
            {
                if (iSugYom == clGeneral.enSugYom.Shishi.GetHashCode())
                {
                    if (oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())
                    {
                        if (oSidur.bSidurAsurBeyomShishi)
                        {
                            bError = true;
                        }
                    }
                }

                if (bError)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "סידור אסור בשישי לעובד 5 ימים", enErrors.errSidurAsurBeyomShishiLeoved5Yamim204.GetHashCode());
                    dtErrors.Rows.Add(drNew);

                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errSidurAsurBeyomShishiLeoved5Yamim204.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "errSidurAsurBeyomShishiLeoved5Yamim204: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }
            return isValid;
        }

         
        private bool CheckMichsatTipatChalav205(ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור         
            bool isValid = true;
            bool bError = false;
            DateTime taarich_me;
            clUtils oUtils = new clUtils();
            float p_meshech;
            try
            {
                if (oSidur.iMisparSidur == 99814 )
                {
                    taarich_me = DateTime.Parse("01/" + (DateTime.Now.AddMonths(-(oParam.iMaxMonthToDisplay-1))).ToString("MM/yyyy"));
                    if (oSidur.dSidurDate >= taarich_me)
                    {
                        p_meshech = oUtils.getMeshechSidur(oSidur.iMisparIshi, oSidur.iMisparSidur, taarich_me, DateTime.Now);
                        if (p_meshech > 40)
                            bError = true;
                    }
                }

                if (bError)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "טיפת חלב - מיצוי מעל 40 שעות", enErrors.errTipatChalavMealMichsa205.GetHashCode());
                    dtErrors.Rows.Add(drNew);

                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errTipatChalavMealMichsa205.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "errTipatChalavMealMichsa205: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }
            return isValid;
        }


        private bool OvedMutaamLeloShaotNosafot206(ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור         
            bool isValid = true;
            int iMutamut;
            int iIsurShaotNosafot = 0;
            DataRow[] dr;
            try
            {
                if (oSidur.iLoLetashlum == 0 &&  oOvedYomAvodaDetails.sMutamut.Trim() != "")
                {
                    iMutamut = int.Parse(oOvedYomAvodaDetails.sMutamut);
                    if (iMutamut > 0)
                    {
                      
                        dr = dtMutamut.Select(string.Concat("kod_mutamut=", iMutamut));
                        if (dr.Length > 0)
                        {
                            iIsurShaotNosafot = string.IsNullOrEmpty(dr[0]["isur_shaot_nosafot"].ToString()) ? 0 : int.Parse(dr[0]["isur_shaot_nosafot"].ToString());
                        }

                        if (iIsurShaotNosafot > 0)
                        {
                            if ((oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode()) && 
                                (clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, iSugYom, oSidur.dSidurDate) || oSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()))
                                    isValid= false;
                              
                             if ((oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode() || oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek2.GetHashCode()) && 
                                  clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, iSugYom, oSidur.dSidurDate))
                                 isValid = false;
                        }
                    }
                }

                if (!isValid)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "עובד מותאם ללא שעות נוספות - אסורה עבודה בשישי/שבת/חג", enErrors.errOvedMutaamLeloShaotNosafot206.GetHashCode());
                    dtErrors.Rows.Add(drNew);

                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errOvedMutaamLeloShaotNosafot206.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "errOvedMutaamLeloShaotNosafot206: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }
            return isValid;
        }

        private bool CheckSidurHeadrutExsits(DataTable dtSidurim, string sug_headrut, int imispar_sidur)
        {
            clSidur oSidur;
            try
            {
                foreach (DataRow dr in dtSidurim.Rows)
                {
                    oSidur = new clSidur();
                    oSidur.AddEmployeeSidurim(dr, true);
                    if (!string.IsNullOrEmpty(oSidur.sHeadrutTypeKod) && oSidur.sHeadrutTypeKod == sug_headrut && oSidur.iMisparSidur == imispar_sidur 
                           && (oSidur.iLoLetashlum == 0 || (oSidur.iLoLetashlum == 1 && oSidur.iKodSibaLoLetashlum == 22)))
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CheckMichsatMachalaYeledImMugbalut210(ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור         
            bool isValid = true;
            bool bError = false;
            DateTime taarich_me;
            clUtils oUtils = new clUtils();
            float p_meshech;
            try
            {
                if (oSidur.iMisparSidur == SIDUR_MACHALA_IM_MUGBALUT)
                {
                    taarich_me = DateTime.Parse("01/01/" + oSidur.dSidurDate.Year);

                    p_meshech = oUtils.getMeshechSidur(oSidur.iMisparIshi, oSidur.iMisparSidur, taarich_me, taarich_me.AddYears(1).AddDays(-1));
                    if (p_meshech >= 52)
                        bError = true;

                }

                if (bError)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "מחלה: ילד עם מוגבלות רפואית ע''ח העובד - מיצוי מעל 52 שעות", enErrors.errMichsatMachalaYeledImMugbalut.GetHashCode());
                    dtErrors.Rows.Add(drNew);

                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errMichsatMachalaYeledImMugbalut.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "errMichsatMachalaYeledImMugbalut: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }
            return isValid;
        }
         
        private bool CheckAnozerSidurExsits(clSidur oSidur)
        {
            try
            {
                foreach (clSidur sidur in htEmployeeDetails.Values)
                {
                    if (oSidur.iMisparSidur != sidur.iMisparSidur && oSidur.dFullShatHatchala != sidur.dFullShatHatchala && sidur.iLoLetashlum == 0)
                       // if (!oSidur.bHeadrutTypeKodExists || (oSidur.bHeadrutTypeKodExists && oSidur.iSidurLebdikatRezefMachala > 0 && oSidur.sHeadrutTypeKod != sug_headrut))
                            return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private bool CheckMachalaExsitsYomKodem(DataTable dtSidurim)
        {
            clSidur oSidur;
            try
            {

                foreach (DataRow dr in dtSidurim.Rows)
                {
                    oSidur = new clSidur();
                    oSidur.AddEmployeeSidurim(dr, true);
                   if (!string.IsNullOrEmpty(oSidur.sHeadrutTypeKod) && oSidur.iSidurLebdikatRezefMachala > 0 && oSidur.sHeadrutTypeKod == clGeneral.enMeafyenSidur53.enMachala.GetHashCode().ToString()
                        && (oSidur.iLoLetashlum == 0 || (oSidur.iLoLetashlum == 1 && oSidur.iKodSibaLoLetashlum == 22)))
                       return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsSidurLina30(DateTime dCardDate, ref DataTable dtErrors)
        {                 
            DataRow drNew;
            string sLookUp = "";
            bool bError;
            bool isValid = true;

            try
            {
                //בדיקה ברמת יום עבודה                
                if (string.IsNullOrEmpty(oOvedYomAvodaDetails.sLina))
                {
                    bError = true;
                }
                else
                {
                   sLookUp = GetLookUpKods("ctb_lina");
                   bError = (sLookUp.IndexOf(oOvedYomAvodaDetails.sLina) == -1);
                }
                             
                if (bError)
                {
                    drNew = dtErrors.NewRow();
                    drNew["check_num"] = enErrors.errLinaValueNotValid.GetHashCode();
                    drNew["mispar_ishi"] = oOvedYomAvodaDetails.iMisparIshi;
                    drNew["taarich"] = dCardDate.ToShortDateString();
                    //drNew["Lina"] = int.Parse(sLina);
                    //drNew["error_desc"] = "ערך לינה שגוי";                        
                    dtErrors.Rows.Add(drNew);

                    isValid = false;
                }                               
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, null, "E", enErrors.errLinaValueNotValid.GetHashCode(), dCardDate, "IsSidurLina30: " + ex.Message);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsSidurChariga33(DateTime dCardDate, ref clSidur oSidur, ref DataTable dtErrors)
        {            
            DataRow drNew;                       
            string sLookUp;
             bool bError = false;
            bool isValid = true;
            DateTime dMeafyenStartDate=DateTime.MinValue;
            DateTime dMeafyenEndDate = DateTime.MinValue;
           bool bCheckChariga = false;
           try
           {
               //בדיקה ברמת סידור
               //השגיאה רלוונטית רק עבור עובד שיש לו מאפייני עבודה מתאימים ליום העבודה:
               //יום חול - מאפיינים 3, 4, שישי/ערב חג -  מאפיינים 5, 6 שבת/שבתון -  מאפיינים 7, 8
               if (clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, iSugYom, oSidur.dSidurDate))
               {
                   if (oMeafyeneyOved.Meafyen7Exists && oMeafyeneyOved.Meafyen8Exists)
                   {
                       bCheckChariga = true;
                       //         dMeafyenStartDate = oMeafyeneyOved.ConvertMefyenShaotValid(_dCardDate, oMeafyeneyOved.sMeafyen7);
                       //         dMeafyenEndDate = oMeafyeneyOved.ConvertMefyenShaotValid(_dCardDate, oMeafyeneyOved.sMeafyen8);

                   }
               }
               else if ((oSidur.sErevShishiChag == "1") || (oSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()))
               {
                   if (oMeafyeneyOved.Meafyen5Exists && oMeafyeneyOved.Meafyen6Exists)
                   {
                       bCheckChariga = true;
                       //        dMeafyenStartDate = oMeafyeneyOved.ConvertMefyenShaotValid(_dCardDate, oMeafyeneyOved.sMeafyen5);
                       //        dMeafyenEndDate = oMeafyeneyOved.ConvertMefyenShaotValid(_dCardDate, oMeafyeneyOved.sMeafyen6);
                   }
               }
               else
               {
                   if (oMeafyeneyOved.Meafyen3Exists && oMeafyeneyOved.Meafyen4Exists)
                   {
                       bCheckChariga = true;
                       //        dMeafyenStartDate = oMeafyeneyOved.ConvertMefyenShaotValid(_dCardDate, oMeafyeneyOved.sMeafyen3);
                       //        dMeafyenEndDate = oMeafyeneyOved.ConvertMefyenShaotValid(_dCardDate, oMeafyeneyOved.sMeafyen4);
                   }
               }

               if (!(oSidur.iLoLetashlum == 1 && (oSidur.iKodSibaLoLetashlum == 4 || oSidur.iKodSibaLoLetashlum == 5 || oSidur.iKodSibaLoLetashlum == 10)))
               {
                   if (bCheckChariga)
                   {
                       dMeafyenStartDate = oSidur.dFullShatHatchalaLetashlum;
                       dMeafyenEndDate = oSidur.dFullShatGmarLetashlum;
                       if (string.IsNullOrEmpty(oSidur.sChariga))
                       {
                           bError = true;
                       }
                       else
                       {
                           sLookUp = GetLookUpKods("ctb_divuch_hariga_meshaot");
                           //אם ערך חריגה לא נמצא בטבלה
                           if (sLookUp.IndexOf(oSidur.sChariga) != -1)
                           {
                               clGeneral.enCharigaValue oCharigaValue;
                               oCharigaValue = (clGeneral.enCharigaValue)int.Parse((oSidur.sChariga));
                               switch (oCharigaValue)
                               {
                                   case clGeneral.enCharigaValue.CharigaKnisa:
                                       //אם שעת כניסה המוגדרת לעובד פחות שעת הכניסה בפועל קטנה מפרמטר 41 המגדיר מינימום לחריגה ומדווח חריגה נעלה שגיאה
                                       if (!string.IsNullOrEmpty(oSidur.sShatHatchala))
                                       {

                                           if (oSidur.dFullShatHatchala < dMeafyenStartDate)
                                           {
                                               if ((dMeafyenStartDate - oSidur.dFullShatHatchala).TotalMinutes < oParam.iZmanChariga)
                                               {
                                                   bError = true;
                                               }
                                           }
                                       }
                                       break;
                                   case clGeneral.enCharigaValue.CharigaYetiza:
                                       if (!string.IsNullOrEmpty(oSidur.sShatGmar))
                                       {
                                           if (oSidur.dFullShatGmar > dMeafyenEndDate)
                                           {
                                               if ((oSidur.dFullShatGmar - dMeafyenEndDate).TotalMinutes < oParam.iZmanChariga)
                                               {
                                                   bError = true;
                                               }
                                           }
                                       }
                                       break;
                                   case clGeneral.enCharigaValue.CharigaKnisaYetiza:
                                       //אם שעת כניסה המוגדרת לעובד פחות שעת הכניסה בפועל קטנה מפרמטר 41 המגדיר מינימום לחריגה ומדווח חריגה נעלה שגיאה
                                       if (!string.IsNullOrEmpty(oSidur.sShatHatchala))
                                       {
                                           if (oSidur.dFullShatHatchala < dMeafyenStartDate)
                                           {
                                               if ((dMeafyenStartDate - oSidur.dFullShatHatchala).TotalMinutes < oParam.iZmanChariga)
                                               {
                                                   bError = true;
                                               }
                                           }
                                       }
                                       if (!string.IsNullOrEmpty(oSidur.sShatGmar))
                                       {
                                           if (oSidur.dFullShatGmar > dMeafyenEndDate)
                                           {
                                               if ((oSidur.dFullShatGmar - dMeafyenEndDate).TotalMinutes < oParam.iZmanChariga)
                                               {
                                                   bError = true;
                                               }
                                           }
                                        }
                                       break;
                               }
                           }
                           if (oSidur.bSidurMyuhad && oSidur.sZakaiLeChariga=="3")
                               bError=false;

                           if (bError)  // && !CheckApproval("2,211,4,5,511,6,10,1011", oSidur.iMisparSidur, oSidur.dFullShatHatchala))
                           {
                               drNew = dtErrors.NewRow();
                               InsertErrorRow(oSidur, ref drNew, "זמן החתמת שעון לא מזכה בחריגה", enErrors.errCharigaZmanHachtamatShaonNotValid.GetHashCode());
                               dtErrors.Rows.Add(drNew);

                               isValid = false;
                           }
                       }
                   }
               }
           }
           catch (Exception ex)
           {
               clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errCharigaZmanHachtamatShaonNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurChariga33: " + ex.Message, null);
               isValid = false;
               _bSuccsess = false;
           }

            return isValid;
        }

        private bool IsOneSidurValid22(int iSidur, ref clSidur oSidur, ref DataTable dtErrors)
        {          
            DataRow drNew;
            bool isValid = true;

            try
            {
                //בדיקה ברמת סידור
                //If ther is one sidur then pitizul field should be empty  
                //אם יש סידור אחד ביום, לא ייתכן שיהיה לו ערך בשדה פיצול הפסקה. כנ"ל לגבי סידור אחרון ביום
                if ((htEmployeeDetails.Count == 1) || (htEmployeeDetails.Count - 1 == iSidur))
                {
                    if (!string.IsNullOrEmpty(oSidur.sPitzulHafsaka) && Int32.Parse(oSidur.sPitzulHafsaka) > 0 && Int32.Parse(oSidur.sPitzulHafsaka)!=3)
                    {
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "פצול/הפסקה בסדור בודד אחרון", enErrors.errPizulValueNotValid.GetHashCode());
                        dtErrors.Rows.Add(drNew);

                        isValid = false;
                    }
                }
                      
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errPizulValueNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsOneSidurValid22: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsPitzulHafsakaValid20(int iSidur,ref clSidur oSidur,ref DataTable dtErrors)
        {           
            DataRow drNew;
            string sLookUp = "";
            bool bError = false;
            bool isValid = true;

            try
            {
                //בדיקה ברמת סידור
                if (String.IsNullOrEmpty(oSidur.sPitzulHafsaka))
                {
                    bError = true;
                }
                else
                {
                    sLookUp = GetLookUpKods("ctb_pitzul_hafsaka");
                    if (sLookUp.IndexOf(oSidur.sPitzulHafsaka) == -1)
                    {
                        bError = true;                        
                    }                
                }

                if (bError)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "ערך  פיצול/הפסקה שגוי", enErrors.errPizulHafsakaValueNotValid.GetHashCode());
                    dtErrors.Rows.Add(drNew);

                    isValid = false;
                }
                else
                {
                    isValid = IsOneSidurValid22(iSidur, ref oSidur, ref dtErrors);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errPizulHafsakaValueNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsPitzulHafsakaValid20: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsHalbashValid36(DateTime dCardDate, ref DataTable dtErrors)
        {                  
            DataRow drNew;
            string sLookUp = "";
            bool bError = false;
            bool isValid = true;

            try
            {
                //בדיקה ברמת יום עבודה
                sLookUp = GetLookUpKods("ctb_zmaney_halbasha");
                drNew = dtErrors.NewRow();
                if (string.IsNullOrEmpty(oOvedYomAvodaDetails.sHalbasha))
                {
                    bError = true;                  
                }
                else
                {
                    if ((sLookUp.IndexOf(oOvedYomAvodaDetails.sHalbasha) == -1) || oOvedYomAvodaDetails.sHalbasha == ZmanHalbashaType.CardError.GetHashCode().ToString())
                    {
                        bError = true;
                        //drNew["halbasha"] = oOvedYomAvodaDetails.sHalbasha;
                    }
                }
                if (bError)
                {                    
                    drNew["check_num"] = enErrors.errHalbashaNotvalid.GetHashCode();
                    drNew["mispar_ishi"] = oOvedYomAvodaDetails.iMisparIshi;//int.Parse(dtOvedCardDetails.Rows[0]["mispar_ishi"].ToString());
                    drNew["taarich"] = dCardDate.ToShortDateString();                    
                    //drNew["error_desc"] = "ערך הלבשה שגוי";
                    dtErrors.Rows.Add(drNew);

                    isValid = false;
                }                               
            }            
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, null, "E", enErrors.errHalbashaNotvalid.GetHashCode(), dCardDate, "IsHalbashValid36: " + ex.Message);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsHashlamaValid137(ref clSidur oSidur, ref DataTable dtErrors)
        {           
            DataRow drNew;
            string sLookUp = "";
            bool bError = false;
            bool isValid = true;

            try
            {
                //בדיקה ברמת סידור
                if (!oSidur.bHashlamaExists && string.IsNullOrEmpty(oSidur.sHashlama))
                {
                    bError = true;
                }
                else
                {
                    sLookUp = "0,1,2,9";                    
                    if (sLookUp.IndexOf(oSidur.sHashlama) == -1)
                    {
                        bError = true;                        
                    }                    
                }
                if (bError)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "ערך השלמה שגוי", enErrors.errHashlamaNotValid.GetHashCode());
                    dtErrors.Rows.Add(drNew);

                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errHashlamaNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsHashlamaValid137: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }


       private bool IsShatYetizaExist92(ref clSidur oSidur, ref clPeilut oPeilut,  ref DataTable dtErrors)
        {
            //Check if peilut Shat_yetzia  is missing
            //DataRow[] drAll;
            DataRow drNew;
            bool isValid = true;
           
            string sShatYetzia;
            try
            {
                //בדיקה ברמת פעילות                
                sShatYetzia = oPeilut.sShatYetzia;
                if (string.IsNullOrEmpty(sShatYetzia))
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "ערך השלמה שגוי", enErrors.errShatYetizaNotExist.GetHashCode());
                    InsertPeilutErrorRow(oPeilut, ref drNew);
                    //drNew["Shat_Yetzia"] = sShatYetzia;
                    //drNew["makat_nesia"] = oPeilut.lMakatNesia;
                    dtErrors.Rows.Add(drNew);

                    isValid = false;
                }                
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errShatYetizaNotExist.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsShatYetizaExist92: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        //private void IsKisuyTorValid117(ref DataTable dtErrors)
        //{
           
        //    //Check if KisuyTor field id valid          
        //    DataRow drNew;
        //    clSidur oSidur;           
        //    string sKey = "";
        //    int iKisutTor;
        //    try
        //    {
        //        //בדיקה ברמת פעילות
        //        foreach (DictionaryEntry deEntry in htEmployeeDetails)
        //        {
        //            sKey = deEntry.Key.ToString();
        //            oSidur = (clSidur)htEmployeeDetails[sKey];
        //            foreach (DictionaryEntry dePeilut in oSidur.htPeilut)
        //            {
        //                iKisuyTor = htEmployeeDetails["KisuyTor"].ToString();
        //                if (iKisuyTor == 0) 
        //                {
        //                    drNew = dtErrors.NewRow();
        //                    InsertErrorRow(oSidur, ref drNew, "שדה כיסוי תור שגוי", enErrors.errKisuyTorNotValid.GetHashCode());
        //                    drNew["kisuy_tor"] = iKisuyTor;
        //                    drNew["makat_nesia"] = htEmployeeDetails["MakatNesia"].ToString(); ;
        //                    dtErrors.Rows.Add(drNew);
        //                }
        //            }
        //        }
        //        //foreach (DataRow dr in dtDetails.Rows)
        //        //{
        //        //    //Insert into dtErrors   
        //        //    if (!(clGeneral.IsNumeric(dr["Kisuy_tor"].ToString())))
        //        //    {
        //        //        drNew = dtErrors.NewRow();
        //        //        InsertErrorRow(oSidur, ref drNew, "שדה כיסוי תור שגוי", enErrors.errKisuyTorNotValid.GetHashCode());
        //        //        //drNew["check_num"] = enErrors.errKisuyTorNotValid.GetHashCode();
        //        //        //drNew["mispar_ishi"] = int.Parse(dr["mispar_ishi"].ToString());
        //        //        //drNew["mispar_sidur"] = int.Parse(dr["mispar_sidur"].ToString());
        //        //        //drNew["shat_yetzia"] = dr["shat_yetzia"].ToString();
        //        //        //drNew["makat_nesia"] = int.Parse(dr["makat_nesia"].ToString());
        //        //        //drNew["error_desc"] = "שדה כיסוי תור שגוי";
        //        //        drNew["kisuy_tor"] = dr["kisuy_tor"].ToString();
        //        //        dtErrors.Rows.Add(drNew);
        //        //    }
        //        //}
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        private bool IsShabatPizulValid23(ref clSidur oSidur, ref DataTable dtErrors)
        {           
            DataRow drNew;
            bool isValid = true;
  
            //בדיקה ברמת סידור
            try
            {
                if (clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, iSugYom, oSidur.dSidurDate) && (!string.IsNullOrEmpty(oSidur.sPitzulHafsaka)) && oSidur.sPitzulHafsaka != "0")
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "ערך פיצול הפסקה ביום שבתון שגוי", enErrors.errShabatPizulValueNotValid.GetHashCode());
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }               
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errShabatPizulValueNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsShabatPizulValid23: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsShbatHashlamaValid47(int iMisparIshi, DateTime dCardDate, ref DataTable dtErrors)
        {            
            DataRow drNew;
            bool isValid = true;

            //בודקים אם שדה השלמה ליום עבודה קיבל ערך 1 וגם יום הוא שבתון - שגיאה. שבתון יכול להיות יום שבת (חוזר מה- Oracle) או שבטבלת סוגי ימים מיוחדים הוא מוגדר כשבתון. במקרה זה רלוונטי רק ליום שמוגדר כשבתון, לא עבור ערב שבת/חג החל מכניסת שבת.
            try
            {
                if (oOvedYomAvodaDetails.sHashlamaLeyom == "1" && clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim,iSugYom,dCardDate))
                {                   
                    drNew = dtErrors.NewRow();
                    drNew["check_num"] = enErrors.errShbatHashlamaNotValid.GetHashCode();
                    drNew["mispar_ishi"] = iMisparIshi;
                    drNew["taarich"] = dCardDate.ToShortDateString();
                    //drNew["error_desc"] = "ערך השלמה ביום שבתון שגוי";                   
                    
                    //InsertErrorRow(oSidur, ref drNew, "ערך השלמה ביום שבתון שגוי", enErrors.errShbatHashlamaNotValid.GetHashCode());                    
                    dtErrors.Rows.Add(drNew);

                    isValid = false;
                }              
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, iMisparIshi, "E", enErrors.errShbatHashlamaNotValid.GetHashCode(), dCardDate, "IsShbatHashlamaValid47: " + ex.Message);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsOvedPeilutValid172(DateTime dCardDate, int iMisparIshi, ref DataTable dtErrors)
        {
            bool isValid = true;
            
            try
            {
                //בדיקה ברמת יום עבודה
                if (IsOvedInMatzav("5,6,8") && htEmployeeDetails.Values.Cast<clSidur>().ToList().Any(sidur => !IsSidurHeadrut(sidur)))
                {
                    drNew = dtErrors.NewRow();
                    drNew["mispar_ishi"] = iMisparIshi;
                    drNew["check_num"] = enErrors.errOvedPeilutNotValid.GetHashCode();
                    drNew["taarich"] = dCardDate.ToShortDateString();
                    //drNew["error_desc"] = "עובד לא פעיל וקיימים עבורו סידורים שאינם היעדרות";

                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, iMisparIshi, "E", enErrors.errOvedPeilutNotValid.GetHashCode(), dCardDate, "IsOvedPeilutValid172: " + ex.Message);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }



        private bool CheckNumGririotInDay203(DateTime dCardDate, int iMisparIshi, ref DataTable dtErrors)
        {
            bool isValid = true;
            int iCountSidurim = 0;
            try
            {
                iCountSidurim = htEmployeeDetails.Values.Cast<clSidur>().ToList().Count(Sidur => Sidur.iSugSidurRagil == 69 && Sidur.iLoLetashlum==0);
                   
                //בדיקה ברמת יום עבודה
                if (iCountSidurim>1)
                {
                    drNew = dtErrors.NewRow();
                    drNew["mispar_ishi"] = iMisparIshi;
                    drNew["check_num"] = enErrors.errConenutGriraMealHamutar.GetHashCode();
                    drNew["taarich"] = dCardDate.ToShortDateString();
                   
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, iMisparIshi, "E", enErrors.errConenutGriraMealHamutar.GetHashCode(), dCardDate, "CheckNumGririotInDay203: " + ex.Message);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private void CheckMushalETWithSidurNotAllowET208(clSidur oSidur, DateTime dCardDate, ref DataTable dtErrors)
        {
           // int iCountSidurim = 0;
            try
            {
                if (oOvedYomAvodaDetails.iKodHevra == clGeneral.enEmployeeType.enEgged.GetHashCode() && oOvedYomAvodaDetails.iKodHevraHashala == clGeneral.enEmployeeType.enEggedTaavora.GetHashCode())
                {
                    if (oSidur.bSidurMyuhad)
                    {
                        if (oSidur.iMisparSidur == 99300 || oSidur.iMisparSidur == 99301 || oSidur.bSidurNotValidKodExists)
                        {
                            drNew = dtErrors.NewRow();
                            InsertErrorRow(oSidur, ref drNew, "", enErrors.errMushalETWithSidurNotAllowET.GetHashCode());
                            dtErrors.Rows.Add(drNew);
                
                        }
                    }
                }  
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, null, "E", enErrors.errMushalETWithSidurNotAllowET.GetHashCode(), dCardDate, "errMushalETWithSidurNotAllowET208: " + ex.Message);
                _bSuccsess = false;
            }
        }
        private bool IsHrStatusValid01(DateTime dCardDate, int iMisparIshi, ref DataTable dtErrors)
        {                      
            bool isValid = true;
            string lstStatus;
            try
            {

                //בדיקה ברמת יום עבודה
                if (dCardDate < DateTime.Parse("12/09/2013"))
                    lstStatus = "1,3,4,5,6,7,8,10,11";
                else lstStatus ="1,3,4,5,6,7,10,11";

                if (!IsOvedInMatzav(lstStatus))
                {
                    drNew = dtErrors.NewRow();
                    drNew["mispar_ishi"] = iMisparIshi;
                    drNew["check_num"] = enErrors.errHrStatusNotValid.GetHashCode();
                    drNew["taarich"] = dCardDate.ToShortDateString();
                    //drNew["error_desc"] = "ערך כא שגוי";

                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, iMisparIshi, "E", enErrors.errHrStatusNotValid.GetHashCode(), dCardDate, "IsHrStatusValid01: " + ex.Message);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsHalbashaInSidurValid37(ref clSidur oSidur, ref int iCount)
        {            
            string sLookUp = "";
            bool isValid = true;
                       
            try
            {
                //בדיקה ברמת סידור
                if (!string.IsNullOrEmpty(oOvedYomAvodaDetails.sHalbasha) && Int32.Parse(oOvedYomAvodaDetails.sHalbasha).ToString().IndexOf("1,2,3")  != -1)
                {
                    sLookUp = GetLookUpKods("ctb_zmaney_halbasha");

                    //שדה הלבשה תקין
                    //אם קוד הלבשה תקין אבל לסידור אין מאפיין 15, אז לא מגיע לסידור הלבשה
                    if (((sLookUp.IndexOf(oOvedYomAvodaDetails.sHalbasha) != -1) && (!oSidur.bHalbashKodExists)) || ((sLookUp.IndexOf(oOvedYomAvodaDetails.sHalbasha) != -1) && (!oSidur.bSidurMyuhad)))
                    {
                        iCount++;

                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errHalbashaInSidurNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsHalbashaInSidurValid37: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsKmExists96(ref clSidur oSidur, ref DataTable dtErrors)
        {           
            bool isValid = true;
            clKavim oKavim = new clKavim();

            try 
	        {
                if (oSidur.bSidurVisaKodExists)
                {
                    oSidur.htPeilut.Values
                                   .Cast<clPeilut>()
                                   .ToList()
                                   .ForEach
                                   (
                                    peilut =>
                                    {
                                        if ((clKavim.enMakatType)oKavim.GetMakatType(peilut.lMakatNesia) == clKavim.enMakatType.mVisa 
                                            && peilut.iKmVisa <= 0)
                                        {
                                            isValid = false;
                                        }                                        
                                    }
                                   );
                }

                if (!isValid)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "חסר קמ", enErrors.errKmNotExists.GetHashCode());
                    drNew["sadot_nosafim"] = 1;
                    dtErrors.Rows.Add(drNew);
                }
	        }
	        catch (Exception ex)
	        {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errKmNotExists.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsKmExists96: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
	        }  

            //try
            //{  //בדיקה ברמת סידור
            //    //רק לסידור מסוג ויזה. לא הזינו ק.מ. יודעים שסידור הוא מסוג ויזה אם יש לו מאפיין ויזה (45) בטבלת סידורים מיוחדים.
            //    if (((oSidur.bSidurMyuhad) && (oSidur.iMisparSidurMyuhad > 0)) || (!oSidur.bSidurMyuhad))
            //    {
            //        if ((oSidur.iKmVisaLepremia == 0) && (oSidur.bSidurVisaKodExists))
            //        {
            //            drNew = dtErrors.NewRow();
            //            InsertErrorRow(oSidur, ref drNew, "חסר קמ", enErrors.errKmNotExists.GetHashCode());
            //            dtErrors.Rows.Add(drNew);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            return isValid;
        }

        private bool IsSidurStartHourValid14(DateTime dCardDate, ref clSidur oSidur, ref DataTable dtErrors)
        {
            DateTime dStartLimitHour, dEndLimitHour,dEzerDate;
            DateTime dSidurStartHour;
             bool isValid = true;
             bool bSidurNahagut = false;
             bool bSidurNihulTnua = false;
            try
            { 
                //    כל הסידורים (מפה או מיוחד)
                //יש לבדוק  עבור כל סידור האם שעת ההתחלה שלו נמצאת בטווח הפרמטרים 1 (שעת התחלה מותרת - טווח תחתון)  ו- 93  (שעת התחלה מותרת -טווח עליון).

                //סידור מיוחד - במקרה של סידור מיוחד יש לבדוק האם יש לו מאפיין 7 (שעת התחלה מותרת) ו/או 8 (שעת גמר מותרת). אם לסידור קיימים מאפיינים 7 ו/או 8 הם יותר "חזקים" מהערכים בפרמטרים החיצוניים.
                //לכל סידור מיוחד יש את שעות ההגבלה שלו והן לא זהות בין סידור לסידור.                           
                //דוגמאות: א. לסידור מאפיין 7 (שעת התחלה מותרת) = 06:00 ומאפיין 8 (שעת גמר מותרת) = 20:00.
                //ניתן להקליד שעת התחלה בין 06:00 ל - 20:00 ולא בין 00:01 ל- 23:59.
                //ב. לסידור מאפיין 7 (שעת התחלה מותרת) = 06:00 ואין מאפיין 8.
                //ניתן להקליד שעת התחלה בין 06:00 ל - 23:59 (ערך בפרמטר 93).
                             
                //שעת התחלה 

                dStartLimitHour = oParam.dSidurStartLimitHourParam1;
                dEndLimitHour = oParam.dSidurEndLimitShatHatchala; 
               
                dSidurStartHour = oSidur.dFullShatHatchala;

                DataRow[] drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, dCardDate, _dtSugSidur);

                bSidurNahagut = IsSidurNahagut(drSugSidur, oSidur);
                bSidurNihulTnua=IsSidurNihulTnua(drSugSidur, oSidur);
                
                if (bSidurNahagut || bSidurNihulTnua)
                {
                    dStartLimitHour = oParam.dSidurStartLimitHourParam1;
                    dEndLimitHour = oParam.dShatHatchalaNahagutNihulTnua;
                 }

                if (oSidur.bSidurMyuhad  && (oSidur.sSugAvoda == clGeneral.enSugAvoda.ActualGrira.GetHashCode().ToString()))
                {
                    dStartLimitHour = oParam.dSidurStartLimitHourParam1;
                    dEndLimitHour = oParam.dShatHatchalaGrira;
                }
                if (oSidur.bSidurMyuhad)
                {

                    if ((oSidur.bShatHatchalaMuteretExists) && (!String.IsNullOrEmpty(oSidur.sShatHatchalaMuteret))) //קיים מאפיין
                    {
                        dStartLimitHour = clGeneral.GetDateTimeFromStringHour(DateTime.Parse(oSidur.sShatHatchalaMuteret).ToString("HH:mm"), dCardDate);
                    }

                    if ((oSidur.bShatGmarMuteretExists) && (!String.IsNullOrEmpty(oSidur.sShatGmarMuteret))) //קיים מאפיין
                    {
                        dEzerDate = DateTime.Parse(oSidur.sShatGmarMuteret); 
                        dEndLimitHour = clGeneral.GetDateTimeFromStringHour(dEzerDate.ToString("HH:mm"), getCorrectDay(dEzerDate, dCardDate));
                    }
                } 


                if ((!string.IsNullOrEmpty(oSidur.sShatHatchala) && dSidurStartHour < dStartLimitHour) && (dStartLimitHour.Year != clGeneral.cYearNull) ||
                    (!string.IsNullOrEmpty(oSidur.sShatHatchala) && dSidurStartHour > dEndLimitHour) && (dEndLimitHour.Year != clGeneral.cYearNull)) 
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "שעת ההתחלה לסידור מיוחד שגוי", enErrors.errSidurHourStartNotValid.GetHashCode());
                    //drNew["shat_hatchala"] = oSidur.sShatHatchala;
                    //drNew["hatchala_limit_hour"] = dStartLimitHour.ToString();                        
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errSidurHourStartNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurStartHourValid14: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private DateTime getCorrectDay(DateTime hour,DateTime dCardDate )
        {
            string date=hour.ToShortDateString();
            if (hour >= DateTime.Parse(date + " 00:01:00") && hour <= DateTime.Parse(date + " 07:59:00"))
            {
                return dCardDate.AddDays(1);
            }
            else return dCardDate;
        }

        private bool IsSidurEndHourValid173(DateTime dCardDate, ref clSidur oSidur, ref DataTable dtErrors)
        {
            DateTime dEndLimitHour, dStartLimitHour, dEzerDate;
            DateTime dSidurEndHour;
            bool isValid = true;
            bool bFlag = false;

            try
            { 
                //                כל הסידורים (מפה או מיוחד)
                //יש לבדוק  עבור כל סידור האם שעת הגמר שלו תקינה. הבדיקה מתבצעת אל מול פרמטרים חיצוניים המשתנים בהתאם לסוג הסידור, נהגות או מנהל. סידור נהגות (לפי ערך 3 במאפיין 5 בטבלת סידורים מיוחדים או טבלת מאפייני סוג סידור) נבדק אם הוא נמצא בטווח הפרמטרים 1 (שעת התחלה מותרת 00:01) ו- 80 (שעת גמר נהגות 07:59). סידור מנהל נבדק אם הוא נמצא בטווח הפרמטרים 1 (שעת התחלה מותרת)  ו- 3 (שעת גמר מינהל 04:00).

                //סידור מיוחד - במקרה של סידור מיוחד יש לבדוק האם יש לו מאפיין 7 (שעת התחלה מותרת) ו/או 8 (שעת גמר מותרת). אם לסידור קיימים מאפיינים 7 ו/או 8 הם יותר "חזקים" מהערכים בפרמטרים החיצוניים.
                //לכל סידור מיוחד יש את שעות ההגבלה שלו והן לא זהות בין סידור לסידור. כאשר בודקים את שעת הגמר של הסידור אל מול פרמטר שמכיל ערך בין 00:01 ל- 07:59 יש להצמיד לפרמטר את תאריך כרטיס העבודה + 1.  נשווה את שעת הגמר של הסידור (datetime) לערך בפרמטר  ותאריך כרטיס העבודה +1.                                                                            
                //דוגמאות: א. לסידור מאפיין 7 (שעת התחלה מותרת) = 10:00 ומאפיין 8 (שעת גמר מותרת) = 20:00.
                //ניתן להקליד שעת גמר בין 10:00 ל - 20:00 ולא בין 00:01 ל- 23:59.
                //ב. לסידור מאפיין 8 (שעת גמר מותרת) = 22:00 ואין מאפיין 7.
                //ניתן להקליד שעת גמר בין 00:01 ל - 20:00.
                //שעת גמר     
                bool isSidurNahagut = false;
                bool isSidurNihulTnua = false;
                DataRow[] drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, dCardDate, _dtSugSidur);
                isSidurNahagut = IsSidurNahagut(drSugSidur, oSidur);

                if (!isSidurNahagut) { isSidurNihulTnua = IsSidurNihulTnua(drSugSidur, oSidur); }

                dStartLimitHour = oParam.dSidurStartLimitHourParam1;
                dEndLimitHour = (isSidurNahagut || isSidurNihulTnua) ? oParam.dNahagutLimitShatGmar : oParam.dSidurEndLimitHourParam3;

                if (oSidur.bSidurMyuhad && !string.IsNullOrEmpty(oSidur.sShaonNochachut) && (oOvedYomAvodaDetails.iIsuk == 122 || oOvedYomAvodaDetails.iIsuk == 123 || oOvedYomAvodaDetails.iIsuk == 124 || oOvedYomAvodaDetails.iIsuk == 127))
                {
                    bFlag = true;
                    dEndLimitHour = oParam.dSidurLimitShatGmarMafilim;
                }


                if ((oOvedYomAvodaDetails.iIsuk != 122 && oOvedYomAvodaDetails.iIsuk != 123 && oOvedYomAvodaDetails.iIsuk != 124 && oOvedYomAvodaDetails.iIsuk != 127) && oMeafyeneyOved.Meafyen43Exists)
                {
                    if (!string.IsNullOrEmpty(oSidur.sShaonNochachut) )
                        bFlag = true;
                    dEndLimitHour = oParam.dSiyumLilaLeovedLoMafil;
                }

                dSidurEndHour = oSidur.dFullShatGmar;
                if (oSidur.bSidurMyuhad)
                {
                    if ((oSidur.bShatHatchalaMuteretExists) && (!String.IsNullOrEmpty(oSidur.sShatHatchalaMuteret))) //קיים מאפיין
                    {
                        dStartLimitHour = clGeneral.GetDateTimeFromStringHour(DateTime.Parse(oSidur.sShatHatchalaMuteret).ToString("HH:mm"), dCardDate);
                    }

                    if ((!bFlag) && (oSidur.bShatGmarMuteretExists) && (!String.IsNullOrEmpty(oSidur.sShatGmarMuteret))) //קיים מאפיין
                    {
                        dEzerDate = DateTime.Parse(oSidur.sShatGmarMuteret);
                        dEndLimitHour = clGeneral.GetDateTimeFromStringHour(dEzerDate.ToString("HH:mm"), getCorrectDay(dEzerDate, dCardDate));

                     //   dEndLimitHour = clGeneral.GetDateTimeFromStringHour(DateTime.Parse(oSidur.sShatGmarMuteret).ToString("HH:mm"), dCardDate.AddDays(1));
                    }
                } 
               
                if (((!string.IsNullOrEmpty(oSidur.sShatGmar) && dSidurEndHour < dStartLimitHour) && (dStartLimitHour.Year != clGeneral.cYearNull)) ||
                    ((!string.IsNullOrEmpty(oSidur.sShatGmar) && dSidurEndHour > dEndLimitHour) && (dEndLimitHour.Year != clGeneral.cYearNull) ))
                  //  ((!string.IsNullOrEmpty(oSidur.sShatGmar) && !string.IsNullOrEmpty(oSidur.sShatHatchala) && oSidur.dFullShatHatchala>=oSidur.dFullShatGmar)) )
                {
                    isValid = false;
                   
                    if (!isValid)
                    {
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "שעת סיום לסידור מיוחד שגוי", enErrors.errSidurHourEndNotValid.GetHashCode());
                        //drNew["shat_gmar"] = oSidur.sShatGmar;
                        //drNew["gmar_limit_hour"] = dEndLimitHour.ToString();
                        dtErrors.Rows.Add(drNew);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errSidurHourEndNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurEndHourValid173: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsShatHatchalaBiggerShatYetzia207(ref clSidur oSidur, ref DataTable dtErrors)
        {
            bool isValid = true;
            try
            { 
               
                if (!string.IsNullOrEmpty(oSidur.sShatGmar) && !string.IsNullOrEmpty(oSidur.sShatHatchala) && oSidur.dFullShatHatchala>=oSidur.dFullShatGmar)
                {
                    isValid = false;
                   
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "שעת התחלה גדולה משעת גמר", enErrors.errShatHatchalaBiggerShatYetzia.GetHashCode());
                    //drNew["shat_gmar"] = oSidur.sShatGmar;
                    //drNew["gmar_limit_hour"] = dEndLimitHour.ToString();
                    dtErrors.Rows.Add(drNew);
                    
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errShatHatchalaBiggerShatYetzia.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "errShatHatchalaBiggerShatYetzia207: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsShatHatchalaLetashlumBiggerShatGmar209(ref clSidur oSidur, ref DataTable dtErrors)
        {
            bool isValid = true;
            try
            {

                if (!string.IsNullOrEmpty(oSidur.sShatGmarLetashlum) && !string.IsNullOrEmpty(oSidur.sShatHatchalaLetashlum ) && oSidur.dFullShatHatchalaLetashlum >= oSidur.dFullShatGmarLetashlum)
                {
                    isValid = false;

                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "שעת התחלה  לתשלום גדולה משעת גמר לתשלום", enErrors.errShatHatchalaLetashlumBiggerShatGmar.GetHashCode());
                     dtErrors.Rows.Add(drNew);

                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errShatHatchalaLetashlumBiggerShatGmar.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsShatHatchalaLetashlumBiggerShatGmar209: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool SidurMachalaMisradHaBitachonLoYachid212(ref clSidur oSidur, ref DataTable dtErrors)
        {
            bool isValid = true;
            clSidur sidur;
            try
            {
             
                if (oSidur.iMisparSidur==99805)
                {
                    for (int i = 0; i < htEmployeeDetails.Count; i++ )
                    {
                        sidur = (clSidur)htEmployeeDetails[i];
                        if (sidur != oSidur && sidur.iLoLetashlum == 0)
                        {
                            isValid = false;

                            drNew = dtErrors.NewRow();
                            InsertErrorRow(oSidur, ref drNew, "לא ניתן לדווח סידור מחלה משרד הבטחון בשילוב עם סידור נוסף ", enErrors.errMachalaMisradHaBitachonLoYachid.GetHashCode());
                            dtErrors.Rows.Add(drNew);

                            break;
                        }
                    }
                 
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errMachalaMisradHaBitachonLoYachid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "SidurMachalaMisradHaBitachonLoYachid212: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }
        
          
        private bool IsKodNesiaExists81(ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors,  DateTime dCardDate)
        {
            bool isValid = true;

            try
            {
                //נבדוק מול התנועה אם סוג קוד נסיעה תקין
                //בדיקה ברמת פעילות  
                if(oPeilut.lMakatNesia.ToString().Length<6)
                    isValid = false;
                else if (oPeilut.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode() || oPeilut.iMakatType == clKavim.enMakatType.mNamak.GetHashCode() || oPeilut.iMakatType == clKavim.enMakatType.mEmpty.GetHashCode())
                {
                    if (oPeilut.iMakatValid != 0)              
                         isValid = false;
                 }
                else if (oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && oPeilut.lMakatNesia.ToString().Substring(0, 3) != "700" && (oPeilut.lMakatNesia.ToString().Length < 8 || oPeilut.iMakatValid != 0))
                    isValid = false;

                if (!isValid)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "קוד נסיעה לא קיים", enErrors.errKodNesiaNotExists.GetHashCode());
                    InsertPeilutErrorRow(oPeilut, ref drNew);
                    dtErrors.Rows.Add(drNew);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errKodNesiaNotExists.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.dFullShatYetzia, oPeilut.iMisparKnisa, "IsKodNesiaExists81: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsValidSidurVisa170(ref clSidur oSidur, int iMispar_Ishi, DateTime dCardDate, ref DataTable dtErrors)
        {
            bool isValid = true;

            try
            {
                if (oSidur.bSidurVisaKodExists && string.IsNullOrEmpty(oSidur.sVisa))
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "סידור ויזה ללא ערך בשדה סוג ויזה", enErrors.errVisaNotValid.GetHashCode());
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errVisaNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsValidSidurVisa170: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsOvedExistsInWorkDay169(ref clSidur oSidur, int iMispar_Ishi, DateTime dCardDate, ref DataTable dtErrors)
        {
            bool isValid = true;

            try
            {
                if (!oOvedYomAvodaDetails.OvedDetailsExists)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "לא קיים בטבלת עובדים", enErrors.errOvedNotExists.GetHashCode());
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errOvedNotExists.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsOvedExistsInWorkDay169: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool HasHafifa167(int iMispar_Ishi, DateTime dCardDate, ref DataTable dtErrors)
        {
            bool hasHafifa = false;
            string hafifaDescription = string.Empty;
            bool isValid = true;

            try
            {
                clOvdim ovdim = new clOvdim();
                DateTime shatHatchalaOfPrevDay = DateTime.MinValue;
                DateTime shatGmarOfPrevDay = DateTime.MinValue;
                DateTime shatHatchalaOfNextDay = DateTime.MinValue;
                DateTime shatGmarOfNextDay = DateTime.MinValue;
                DataTable dtSidur = ovdim.GetSidur("last", iMispar_Ishi, dCardDate.AddDays(-1));
                if(dtSidur != null && dtSidur.Rows.Count > 0)
                {
                    DateTime.TryParse(dtSidur.Rows[dtSidur.Rows.Count - 1]["shat_hatchala"].ToString(), out shatHatchalaOfPrevDay);
                    DateTime.TryParse(dtSidur.Rows[dtSidur.Rows.Count - 1]["shat_gmar"].ToString(), out shatGmarOfPrevDay);
                }
                if (htEmployeeDetails.Count > 0)
                {
                    clSidur firstSidurOfTheDay = htEmployeeDetails[0] as clSidur;
                    clSidur lastSidurOfTheDay = htEmployeeDetails[htEmployeeDetails.Count - 1] as clSidur;
                    dtSidur = ovdim.GetSidur("first", iMispar_Ishi, dCardDate.AddDays(1));
                    if (dtSidur != null && dtSidur.Rows.Count > 0)
                    {
                        DateTime.TryParse(dtSidur.Rows[0]["shat_hatchala"].ToString(), out shatHatchalaOfNextDay);
                        DateTime.TryParse(dtSidur.Rows[0]["shat_gmar"].ToString(), out shatGmarOfNextDay);
                    }

                    if (firstSidurOfTheDay.iLoLetashlum==0 && shatGmarOfPrevDay != DateTime.MinValue &&
                        shatGmarOfPrevDay.Date == firstSidurOfTheDay.dFullShatHatchala.Date &&
                        (shatGmarOfPrevDay - firstSidurOfTheDay.dFullShatHatchala) > TimeSpan.Zero)
                    {
                        hasHafifa = true;
                        hafifaDescription = "חפיפה עם יום קודם";
                    }

                    if (lastSidurOfTheDay.iLoLetashlum == 0 && shatHatchalaOfNextDay != DateTime.MinValue &&
                        lastSidurOfTheDay.dFullShatGmar != DateTime.MinValue &&
                        lastSidurOfTheDay.dFullShatGmar.Date == shatHatchalaOfNextDay.Date &&
                        (lastSidurOfTheDay.dFullShatGmar - shatHatchalaOfNextDay) > TimeSpan.Zero)
                    {
                        hasHafifa = true;
                        hafifaDescription = "חפיפה עם יום עוקב";
                    }

                    if (hasHafifa)
                    {
                        drNew = dtErrors.NewRow();
                        drNew["check_num"] = enErrors.errHafifaBetweenSidurim.GetHashCode();
                        drNew["taarich"] = dCardDate.ToShortDateString();
                        drNew["mispar_ishi"] = iMispar_Ishi;
                        //drNew["error_desc"] = string.Concat("קיימת חפיפה בין סידורים עבור ימים עוקבים", " - ", hafifaDescription);
                        dtErrors.Rows.Add(drNew);
                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, iMispar_Ishi, "E", enErrors.errHafifaBetweenSidurim.GetHashCode(), dCardDate, "HasHafifa167: " + ex.Message);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private void IsSidurMiluimAndAvoda156(clSidur oSidur, ref DataTable dtErrors)
            {
            bool isSidurValid = true;
            try{
                //ש לזהות האם באותו היום יש סידור מילואים = סידור מיוחד עבורו קיים מאפיין 53 עם ערך 3. 
                if (oSidur.sHeadrutTypeKod == "3")
                {
                    if (htEmployeeDetails.Values.Cast<clSidur>().ToList().Any(sidur => !IsSidurHeadrut(sidur) && (sidur.iLoLetashlum != 1 || sidur.iLoLetashlum==0 && sidur.iKodSibaLoLetashlum==1) && sidur.iMisparSidur != oSidur.iMisparSidur && sidur.dFullShatHatchala != oSidur.dFullShatHatchala))
                    {
                        isSidurValid = false;
                    }
                    if (!isSidurValid)
                    {
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "מילואים ועבודה באותו יום", enErrors.errMiluimAndAvoda.GetHashCode());
                        dtErrors.Rows.Add(drNew);
                    }
                    
                }
             }
             catch (Exception ex)
             {
                 clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errMiluimAndAvoda.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurMiluimAndAvoda156: " + ex.Message, null);
                 _bSuccsess = false;
             }

           
    }

        private bool IsHighPremya153(clSidur oSidur, ref DataTable dtErrors, bool bSidurNahagut, ref bool bCheckBoolSidur)
        {
            DataSet dsSidur;
            bool isValid = true;
            float dSumMazanTashlum = 0;
            double dSumMazanTichnun = 0;
            int iTypeMakat;
            clPeilut oPeilut;
            OrderedDictionary htPeilut = new OrderedDictionary();
            float fZmanSidur = 0;
            float fZmanSidurMapa = 0;
            bool isSidurValid = true;
            clKavim oKavim = new clKavim();
            DateTime dShatGmarMapa, dShaHatchalaMapa;
            int iResult;
            string sShaa;
            try
            {
                //נתונים של הסידור מהמפה
                dsSidur = oKavim.GetSidurAndPeiluyotFromTnua(oSidur.iMisparSidur, _dCardDate, null, out iResult);
                if (iResult == 0)
                {
                    //שעת התחלה ושעת גמר
                    if (dsSidur.Tables[1].Rows.Count > 0)
                    {
                        sShaa = dsSidur.Tables[1].Rows[0]["SHAA"].ToString();
                        dShaHatchalaMapa = clGeneral.GetDateTimeFromStringHour(sShaa, _dCardDate);
                        for (int i = dsSidur.Tables[1].Rows.Count - 1; i >= 0; i--)
                        {
                            long lMakatNesia = long.Parse(dsSidur.Tables[1].Rows[i]["MAKAT8"].ToString());
                            sShaa = dsSidur.Tables[1].Rows[i]["SHAA"].ToString();
                            if (!string.IsNullOrEmpty(dsSidur.Tables[1].Rows[i]["MazanTichnun"].ToString()))
                                dSumMazanTichnun = double.Parse(dsSidur.Tables[1].Rows[i]["MazanTichnun"].ToString());
                            dShatGmarMapa = clGeneral.GetDateTimeFromStringHour(sShaa, _dCardDate).AddMinutes(dSumMazanTichnun);
                            fZmanSidurMapa = int.Parse((dShatGmarMapa - dShaHatchalaMapa).TotalMinutes.ToString());

                            //במידה והפעילות האחרונה היא אלמנט לידיעה בלבד (ערך 2 (לידיעה בלבד) במאפיין 3  (לפעולה/לידיעה בלבד), יש לקחת את הפעילות הקודמת לה.

                            if ((clKavim.enMakatType)(oKavim.GetMakatType(lMakatNesia)) == clKavim.enMakatType.mElement)
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
                }
                // נתונים מהסידור בכרטיס העבודה 
                fZmanSidur = float.Parse((oSidur.dFullShatGmar - oSidur.dFullShatHatchala).TotalMinutes.ToString());
                if (fZmanSidur >= 0)
                {
                    htPeilut = oSidur.htPeilut;
                    for (int i = 0; i < oSidur.htPeilut.Values.Count; i++)
                    {
                        oPeilut = ((clPeilut)htPeilut[i]);
                        iTypeMakat = oPeilut.iMakatType;
                        if ((oPeilut.iMisparKnisa == 0 && iTypeMakat == clKavim.enMakatType.mKavShirut.GetHashCode()) || iTypeMakat == clKavim.enMakatType.mEmpty.GetHashCode() || iTypeMakat == clKavim.enMakatType.mNamak.GetHashCode())
                        {
                            dSumMazanTashlum += oPeilut.iMazanTashlum;
                        }
                        else if (iTypeMakat == clKavim.enMakatType.mElement.GetHashCode())
                        {
                            if (oPeilut.sElementInMinutes == "1" && oPeilut.sKodLechishuvPremia.Trim() == "1:1")
                            {
                                dSumMazanTashlum += Int32.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));
                            }
                        }
                    }

                    if (dSumMazanTashlum >= fZmanSidur)
                    {
                        if (oSidur.bSidurMyuhad)
                        {
                            if (dSumMazanTashlum >= (fZmanSidur * 2))
                                isSidurValid = false;
                        }
                        else
                        {
                            if ((dSumMazanTashlum >= (fZmanSidur + 90)) || (dSumMazanTashlum >= (fZmanSidur * 2)))
                                if (((((dSumMazanTashlum - fZmanSidur) / (dSumMazanTichnun - fZmanSidurMapa)) * 100) - 100) < _oParameters.fHighPremya)
                                    isSidurValid = false;
                        }
                    }


                    if (!isSidurValid)
                    {
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "פרמיה גבוהה", enErrors.errHighPremya.GetHashCode());
                        dtErrors.Rows.Add(drNew);
                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errHighPremya.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsHighPremya153: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }
        
            return isValid;
        }


        private bool IsHighPremya153_1(clSidur oSidur, ref DataTable dtErrors, bool bSidurNahagut, ref bool bCheckBoolSidur)
        {
           
            bool isSidurValid = true;
            bool isValid = true;
          float fZmanSidur;
            double dElementsHamtanaReshut;
            double dTempPremia = 0;
             clPeilut oPeilutAchrona;
             try
             {
                 if (oSidur.htPeilut.Count > 0)
                 {
                     if (bSidurNahagut && !oSidur.bSidurVisaKodExists)
                     {

                         oPeilutAchrona = GetLastPeilutNoElementLeyedia(oSidur);
                         fZmanSidur = float.Parse(((oPeilutAchrona.dFullShatYetzia.AddMinutes(oPeilutAchrona.iMazanTichnun)) - oSidur.dFullShatHatchala).TotalMinutes.ToString());

                         if (!oSidur.bSidurMyuhad)
                         {//סידורי מפה
                             bCheckBoolSidur = CheckBoolSidur(fZmanSidur, oSidur.iMisparSidur);

                             if (!bCheckBoolSidur)
                             {
                                 dTempPremia = oSidur.CalculatePremya(oSidur.htPeilut, out dElementsHamtanaReshut);

                                 if (dTempPremia >= (fZmanSidur + 90))
                                 {
                                     isSidurValid = false;
                                 }
                                 if (dTempPremia >= (fZmanSidur * 2))
                                 {
                                     isSidurValid = false;
                                 }
                             }

                         }
                         else
                         {

                             dTempPremia = oSidur.CalculatePremya(oSidur.htPeilut, out dElementsHamtanaReshut);
                             if (dTempPremia >= (fZmanSidur * 2))
                             {
                                 isSidurValid = false;
                             }
                         }



                         if (!isSidurValid)
                         {
                             drNew = dtErrors.NewRow();
                             InsertErrorRow(oSidur, ref drNew, "פרמיה גבוהה", enErrors.errHighPremya.GetHashCode());
                             dtErrors.Rows.Add(drNew);
                             isValid = false;
                         }

                     }
                 }
             }
             catch (Exception ex)
             {
                 clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errHighPremya.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsHighPremya153: " + ex.Message, null);
                 isValid = false;
                 _bSuccsess = false;
             }

            return isValid;
        }

        private bool CheckBoolSidur(float fZmanSidur,int iMisparSidur)
        {
            DataSet dsSidur;
            int iResult;
            clKavim oKavim = new clKavim();
            float  fZmanMapa=0;
            double fMaazanTichnun = 0;
            DateTime dShatGmarMapa, dShaHatchalaMapa;
            string sShaa;
            bool bCheckBoolSidur = false;
            try
            {
                dsSidur = oKavim.GetSidurAndPeiluyotFromTnua(iMisparSidur, _dCardDate,null, out iResult);
                if (iResult == 0)
                {
                    //שעת התחלה ושעת גמר
                    if (dsSidur.Tables[1].Rows.Count > 0)
                    {
                        sShaa = dsSidur.Tables[1].Rows[0]["SHAA"].ToString();
                        dShaHatchalaMapa = clGeneral.GetDateTimeFromStringHour(sShaa, _dCardDate);
                        for (int i = dsSidur.Tables[1].Rows.Count - 1; i >= 0; i--)
                        {
                            long lMakatNesia = long.Parse(dsSidur.Tables[1].Rows[i]["MAKAT8"].ToString());
                            sShaa = dsSidur.Tables[1].Rows[i]["SHAA"].ToString();
                            if (!string.IsNullOrEmpty(dsSidur.Tables[1].Rows[i]["MazanTichnun"].ToString()))
                                fMaazanTichnun = double.Parse(dsSidur.Tables[1].Rows[i]["MazanTichnun"].ToString());
                            dShatGmarMapa = clGeneral.GetDateTimeFromStringHour(sShaa, _dCardDate).AddMinutes(fMaazanTichnun);
                            fZmanMapa = int.Parse((dShatGmarMapa - dShaHatchalaMapa).TotalMinutes.ToString());
                    
                            //במידה והפעילות האחרונה היא אלמנט לידיעה בלבד (ערך 2 (לידיעה בלבד) במאפיין 3  (לפעולה/לידיעה בלבד), יש לקחת את הפעילות הקודמת לה.
                        
                            if ((clKavim.enMakatType)(oKavim.GetMakatType(lMakatNesia)) == clKavim.enMakatType.mElement)
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
        private clPeilut GetLastPeilutNoElementLeyedia(clSidur oSidur)
        {
            try{
            clPeilut oPeilutAchrona=null;
           
            for (int i = oSidur.htPeilut.Count - 1; i >= 0; i--)
            {
                oPeilutAchrona = (clPeilut)oSidur.htPeilut[i];
                if (oPeilutAchrona.iMakatType == clKavim.enMakatType.mElement.GetHashCode())
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

        private bool IsNegativePremya154(clSidur oSidur, ref DataTable dtErrors, bool bSidurNahagut, ref bool bCheckBoolSidur)
        {
        
                bool isSidurValid = true;
                bool isValid = true;
                double dTempPremia = 0;
                float fZmanSidur;
                double dElementsHamtanaReshut;
               clPeilut oPeilutAchrona;
                try
                {
                    if (oSidur.htPeilut.Count > 0)
                    {
                        if (bSidurNahagut)
                        {
                            oPeilutAchrona = GetLastPeilutNoElementLeyedia(oSidur);
                            fZmanSidur = float.Parse(((oPeilutAchrona.dFullShatYetzia.AddMinutes(oPeilutAchrona.iMazanTichnun)) - oSidur.dFullShatHatchala).TotalMinutes.ToString());

                            //אם זמן הנוכחות (שעת גמר פחות שעת התחלה של  סידור) קטן מ- 350 - לא ממשיכים בבדיקה 
                            if (fZmanSidur >= 350)
                            {
                                dTempPremia = oSidur.CalculatePremya(oSidur.htPeilut, out dElementsHamtanaReshut);

                                if (!oSidur.bSidurMyuhad)
                                {//סידורי מפה
                                   
                                    if (!bCheckBoolSidur)
                                    {
                                        if (dTempPremia < fZmanSidur)
                                        {
                                            //אם הפער בין הפרמיה היא  שווה או פחות מ- % 20 מזמן הנוכחות - לא ממשיכים בבדיקה 
                                            if (dTempPremia - fZmanSidur > ((fZmanSidur * 20) / 100))
                                            {
                                                //בודקים האם סה"כ זמן האלמנטים מסוג המתנה ולרשות קטן מהפרמיה שחושבה בא' -שגוי.
                                                if (dElementsHamtanaReshut < dTempPremia)
                                                {
                                                    isSidurValid = false;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (dTempPremia < fZmanSidur)
                                    {
                                        //הפער בין הפרמיה  היא שווה או פחות מ- % 20 מזמן הנוכחות - לא ממשיכים בבדיקה 
                                        if (dTempPremia - fZmanSidur > ((fZmanSidur * 20) / 100))
                                        {
                                            //בודקים האם סה"כ זמן האלמנטים מסוג המתנה ולרשות קטן מהפרמיה שחושבה בא' -שגוי.
                                            if (dElementsHamtanaReshut < dTempPremia)
                                            {
                                                isSidurValid = false;
                                            }
                                        }
                                    }

                                }
                                if (!isSidurValid)
                                {
                                    DataRow drNew = dtErrors.NewRow();
                                    InsertErrorRow(oSidur, ref drNew, "פרמיה שלילית", enErrors.errNegativePremya.GetHashCode());
                                    dtErrors.Rows.Add(drNew);
                                    isValid = false;
                                }
                            }
                        }
                    }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errNegativePremya.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsNegativePremya154: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private void MutamLoBeNahagut186(clSidur oSidur, DateTime dCardDate, bool bSidurNahagut, ref DataTable dtErrors)
        {
            DataRow drNew;
            int iMutamut;
            try
            {
                if (oOvedYomAvodaDetails.sMutamut.Trim() != "")
                {
                    iMutamut = int.Parse(oOvedYomAvodaDetails.sMutamut);
                    if (bSidurNahagut &&  (iMutamut ==4 || iMutamut == 5 || iMutamut ==9))
                    {
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "", enErrors.errMutamLoBeNahagutBizeaNahagut.GetHashCode());
                        dtErrors.Rows.Add(drNew);   
                    }
                }
            }           
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, null, "E", enErrors.errMutamLoBeNahagutBizeaNahagut.GetHashCode(), dCardDate, "MutamLoBeNahagut186: " + ex.Message);
                _bSuccsess = false;
            }
        }

        private void KupaiWithNihulTnua187(clSidur oSidur, DateTime dCardDate, DataRow[] drSugSidur, ref DataTable dtErrors)
        {
            DataRow drNew;
            string  sug_sidur;
            clPeilut oFirstPeilut;
            try
            {
                if (drSugSidur.Length > 0)
                {
                    sug_sidur = drSugSidur[0]["sug_sidur"].ToString();
                    if (sug_sidur == "70" || sug_sidur == "71" || sug_sidur == "72")
                    {
                        oFirstPeilut = oSidur.htPeilut.Values.Cast<clPeilut>().ToList().FirstOrDefault(peilut => (peilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "730" ||
                                                peilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "740" ||
                                                peilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "750"));

                        if (oFirstPeilut != null)
                        {
                            drNew = dtErrors.NewRow();
                            InsertErrorRow(oSidur, ref drNew, "", enErrors.errKupaiWithNihulTnua.GetHashCode());
                            dtErrors.Rows.Add(drNew);
                        }
                    }
                }
            }           
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, null, "E", enErrors.errKupaiWithNihulTnua.GetHashCode(), dCardDate, "KupaiWithNihulTnua187: " + ex.Message);
                _bSuccsess = false;
            }
        }

        private void ChofeshAlCheshbonShaotNosafot188(clSidur oSidur, DateTime dCardDate, ref DataTable dtErrors)
        {
            int iCountSidurim;
            try
            {
                if (oSidur.iMisparSidur == 99822)
                {

                    iCountSidurim = htEmployeeDetails.Values.Cast<clSidur>().ToList().Count(Sidur => ((Sidur.iMisparSidur != oSidur.iMisparSidur || Sidur.dFullShatHatchala != oSidur.dFullShatHatchala) && Sidur.iLoLetashlum == 0));
                     if (iCountSidurim > 0 || oMeafyeneyOved.iMeafyen56 != clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())
                     {
                            drNew = dtErrors.NewRow();
                            InsertErrorRow(oSidur, ref drNew, "", enErrors.errChofeshAlCheshbonShaotNosafot.GetHashCode());
                            dtErrors.Rows.Add(drNew);
                      }
                }
     
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, null, "E", enErrors.errChofeshAlCheshbonShaotNosafot.GetHashCode(), dCardDate, "ChofeshAlCheshbonShaotNosafot188: " + ex.Message);
                _bSuccsess = false;
            }
        }
       
        private bool IsSimunNesiaValid27(DateTime dCardDate, ref DataTable dtErrors)
        {            
            string sLookUp, sBitulZmanNesia;
            DataRow drNew;
            bool bError=false;
            bool isValid = true;

            try
            {
                //בדיקה ברמת כרטיס עבודה
                sLookUp = GetLookUpKods("ctb_zmaney_nesiaa");
                sBitulZmanNesia = oOvedYomAvodaDetails.sBitulZmanNesiot;
                if (!(string.IsNullOrEmpty(sBitulZmanNesia)))
                {   //נעלה שגיאה אם ערך לא קיים בטבלת פענוח
                    if (sLookUp.IndexOf(sBitulZmanNesia) == -1)
                    {
                        bError = true;
                    }
                    //else
                    //{ //אם לא קיימים מאפיינים 51 ו61-
                    //    //לא אמור להיות ערך בשדה 
                    //    if ((!oMeafyeneyOved.Meafyen51Exists) && (!oMeafyeneyOved.Meafyen61Exists))
                    //    {
                    //        bError = true;
                    //    }
                    //}
                }
                else
                {
                    bError = true;
                }
                if (bError)
                {
                    drNew = dtErrors.NewRow();
                    drNew["check_num"] = enErrors.errSimunNesiaNotValid.GetHashCode();
                    drNew["mispar_ishi"] = oOvedYomAvodaDetails.iMisparIshi;
                    drNew["taarich"] = dCardDate.ToShortDateString();
                    //drNew["error_desc"] = "ערך ביטול נסיעות שגוי";
                    //drNew["bitul_zman_nesiot"] = sBitulZmanNesia;
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }           
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, null, "E", enErrors.errSimunNesiaNotValid.GetHashCode(), dCardDate, "IsSimunNesiaValid27: " + ex.Message);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }
        private bool IsOutMichsaValid118(ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור          
            DataRow drNew;
            bool isValid = true;

            try
            {
                //אם שדה מחוץ למכסה לא מקבל ערך תקין (0 או 1) - שגוי
                if (((oSidur.sOutMichsa != "0") && (oSidur.sOutMichsa != "1")) || (string.IsNullOrEmpty(oSidur.sOutMichsa)))
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "מחוץ למכסה שגוי", enErrors.errOutMichsaNotValid.GetHashCode());
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errOutMichsaNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsOutMichsaValid118: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }
        private bool IsOutMichsaValid40(ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור          
            DataRow drNew;
            bool isValid = true;

            try
            {
                //אם שדה מחוץ למכסה תקין (0 או 1) והסידור הוא סידור העדרות - תצא שגיאה. יודעים אם סידור הוא העדרות לפי מאפיין בטבלת מאפיינים סידורים מיוחדים. רק עבור סידורים מיוחדים.
                if ( oSidur.sOutMichsa == "1" && oSidur.sSectorAvoda == clGeneral.enSectorAvoda.Headrut.GetHashCode().ToString())
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "מחוץ למכסה בסדור שאסור ", enErrors.errOutMichsaInSidurHeadrutNotValid.GetHashCode());                        
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }               
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errOutMichsaInSidurHeadrutNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsOutMichsaValid40: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsElementTimeValid129(float fSidurTime, ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors)
        {
            //בדיקה ברמת פעילות
            bool isValid = true;

            try
            {
                DataRow drNew;
                long lMakatNesia = oPeilut.lMakatNesia;
                clKavim oKavim = new clKavim();
                int iElementTime;
                /*מפעילים את הרוטינה לזיהיו סוג מקט. אם חוזר שסוג המקט הוא אלמנט יש לבדוק שהמשך שלו אינו גדול מזמן הסידור. משך האלמנט רשום בדקות בספרות 4-6 שלו.
                */
                //iSidurTime = int.Parse(oSidur.sShatGmar.Replace(":", "")) * 60 - int.Parse(oSidur.sShatHatchala.Replace(":", "")) * 60;           
                if ((oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode()) && (oPeilut.sElementZviraZman != clGeneral.enSectorZviratZmanForElement.ElementZviratZman.GetHashCode().ToString()) && (oPeilut.sElementInMinutes == "1"))
                {
                    iElementTime = int.Parse(lMakatNesia.ToString().PadLeft(8).Substring(3, 3));
                    if (iElementTime > (fSidurTime))
                    {
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "משך זמן האלמנט גדול ממשך זמן הסידור", enErrors.errElementTimeBiggerThanSidurTime.GetHashCode());
                        InsertPeilutErrorRow(oPeilut, ref drNew);
                        //drNew["Shat_Yetzia"] = oPeilut.sShatYetzia;//htEmployeeDetails["ShatYetzia"].ToString();
                        //drNew["makat_nesia"] = oPeilut.lMakatNesia;//htEmployeeDetails["MakatNesia"].ToString();
                        dtErrors.Rows.Add(drNew);
                        isValid = false;
                        //break;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errElementTimeBiggerThanSidurTime.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.dFullShatYetzia, oPeilut.iMisparKnisa, "IsElementTimeValid129: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }


        private bool IsPeilutShatYeziaValid113(ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors, DateTime dCardDate)
        {    //בדיקה ברמת פעילות
            /*שדה יציאה צריך להיות בטווח 00:00 עד 31:59. יש לבדוק את טבלת פרמטרים חיצוניים, פרמטרים 29 (שעת התחלה ראשונה מותרת לפעילות בסידור (שעת יציאה לפעילות)), - 30 (שעת התחלה אחרונה מורת לעילות בסידור (שעת יציאה לפעילות))*/
            //clSidur oSidur;
            //string sKey;
            DataRow drNew;
            //int iStartLimitHour, iEndLimitHour
            string sPeilutShatYetzia;
            DateTime dPeilutShatYetiza;
            DateTime dStartHourForPeilut;
            DateTime dEndHourForPeilut;
            bool isValid = true;

            try
            {
            ////שעת התחלה ראשונה מותרת לפעילות בסידור - מאפיין 29
            //iStartHourForPeilut = int.Parse(GetOneParam(29, dCardDate).Replace(char.Parse(":"), char.Parse("")));

            ////שעת יציאה אחרונה מותרת לפעילות - מאפיין 30
            //iEndHourForPeilut = int.Parse(GetOneParam(30, dCardDate).Replace(char.Parse(":"), char.Parse("")));

               
               if (!(string.IsNullOrEmpty(oPeilut.sShatYetzia)))
                    {
                        sPeilutShatYetzia = oPeilut.sShatYetzia;                        
                        //נבדוק אם השעה תקפה בפורמט של שעת אגד
                        if (CheckEggedHourValid(oPeilut.sShatYetzia))
                        {
                            drNew = dtErrors.NewRow();
                            InsertErrorRow(oSidur, ref drNew, "  לא בפורמט אגד - שעת יציאה שגויה", enErrors.errPeilutShatYetizaNotValid.GetHashCode());
                            InsertPeilutErrorRow(oPeilut, ref drNew);
                            //drNew["Shat_Yetzia"] = oPeilut.sShatYetzia;
                            //drNew["makat_nesia"] = oPeilut.lMakatNesia;
                            dtErrors.Rows.Add(drNew);
                            isValid = false;
                        }
                        //נבדוק אם שעה תקפה מול פרמטרים של שעת התחלה ושעת סיום
                        dPeilutShatYetiza = clGeneral.GetDateTimeFromStringHour(sPeilutShatYetzia,dCardDate);
                        if (oParam.dStartHourForPeilut.Year!=clGeneral.cYearNull)
                        {
                            dStartHourForPeilut = oParam.dStartHourForPeilut;//DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", oParam.sStartHourForPeilut));
                            if ((dPeilutShatYetiza < dStartHourForPeilut))
                            {
                                drNew = dtErrors.NewRow();
                                InsertErrorRow(oSidur, ref drNew, "שעת יציאה שגויה", enErrors.errPeilutShatYetizaNotValid.GetHashCode());
                                InsertPeilutErrorRow(oPeilut, ref drNew);
                                //drNew["Shat_Yetzia"] = oPeilut.sShatYetzia;
                                //drNew["makat_nesia"] = oPeilut.lMakatNesia;
                                dtErrors.Rows.Add(drNew);
                                isValid = false;
                            }
                        }
                        if (oParam.dEndHourForPeilut.Year!=clGeneral.cYearNull)
                        {
                            dEndHourForPeilut = oParam.dEndHourForPeilut.AddDays(1);//DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", oParam.sEndHourForPeilut)).AddDays(1);
                            if (oPeilut.dFullShatYetzia > dEndHourForPeilut)
                            {
                                drNew = dtErrors.NewRow();
                                InsertErrorRow(oSidur, ref drNew, "שעת יציאה שגויה", enErrors.errPeilutShatYetizaNotValid.GetHashCode());
                                InsertPeilutErrorRow(oPeilut, ref drNew);
                                dtErrors.Rows.Add(drNew);
                                isValid = false;
                            }
                        }
                        
                    }               
            }
            catch(Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errPeilutShatYetizaNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsPeilutShatYeziaValid113: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsHamaraValid42(ref clSidur oSidur, ref DataTable dtErrors)
        {
            bool isValid = true;

            try
            {
                //if (oSidur.sHamaratShabat.IndexOf("0,1") == -1)
                //{
                //    drNew = dtErrors.NewRow();
                //    InsertErrorRow(oSidur, ref drNew, "ערך המרה שגוי", enErrors.errHamaratShabatNotValid.GetHashCode());
                //    drNew["hamarat_shabat"] = oSidur.sHamaratShabat;
                //    dtErrors.Rows.Add(drNew);
                //    isValid = false;
                //}
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errHamaratShabatNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsHamaraValid42: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }
        private bool IsHamaratShabatValid39(DataRow[] drSugSidur, ref clSidur oSidur,ref DataTable dtErrors)
        {            
            //בדיקה ברמת סידור            
            //DataRow drNew;
            bool isValid = true;

            try
            {
                //if (!String.IsNullOrEmpty(oSidur.sHamaratShabat) && (oSidur.sHamaratShabat == "1"))
                //{       
                //    //sHamaratShabat-אם אין זכאות להמרה,לא אמור להיות ערך ב-
                //    //נבדוק רק לסידורים מיוחדים
                //    if ((oSidur.bSidurMyuhad) && (oSidur.iMisparSidurMyuhad > 0))
                //    {
                //        if (!oSidur.bZakaiLehamaraExists)
                //        {
                //            drNew = dtErrors.NewRow();
                //            InsertErrorRow(oSidur, ref drNew, "המרה אסורה לסידור", enErrors.errHamaraNotValid.GetHashCode());
                //            drNew["hamarat_shabat"] = oSidur.sHamaratShabat;
                //            dtErrors.Rows.Add(drNew);
                //            isValid = false;
                //        }
                //    }
                //    else//סידורים רגילים
                //    {
                //        if (drSugSidur.Length > 0)
                //        {   //עבור סידורים רגילים, רק בסידורי נהגות וניהול תנועה מותרת המרה.
                //            if ((drSugSidur[0]["sector_avoda"].ToString() != clGeneral.enSectorAvoda.Nihul.GetHashCode().ToString()) && (drSugSidur[0]["sector_avoda"].ToString() != clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString()))
                //            {
                //                drNew = dtErrors.NewRow();
                //                InsertErrorRow(oSidur, ref drNew, "המרה אסורה לסידור", enErrors.errHamaraNotValid.GetHashCode());
                //                drNew["hamarat_shabat"] = oSidur.sHamaratShabat;
                //                dtErrors.Rows.Add(drNew);
                //                isValid = false;
                //            }
                //        }
                //    }                  
                //}
            }          
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errHamaraNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsHamaratShabatValid39: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsPitzulAndNotZakai25(ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור           
            DataRow drNew;
            bool isValid = true;

            try
            {    //אם הערך בשדה פיצול הפסקה שווה 2 וגם מעמד שווה חבר זו שגיאה
                //נבדוק אם העובד הוא חבר
                //sKodMaamd = dtOvedCardDetails.Rows[0]["Kod_Maamd"].ToString();
                if (!string.IsNullOrEmpty(oSidur.sPitzulHafsaka))
                {
                    if ((oOvedYomAvodaDetails.sKodHaver == "1") && (oSidur.sPitzulHafsaka == "2")) //קוד מעמד שמתחיל ב- 1 - חבר
                    {
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "פצול מיוחד ולא זכאי", enErrors.errPitzulMuchadValueNotValid.GetHashCode());
                        dtErrors.Rows.Add(drNew);
                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errPitzulMuchadValueNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsPitzulAndNotZakai25: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsPeilutInSidurValid84(ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור          
            int iPeilutMisparSidur;
            DataRow drNew;
            bool isValid = true;
            bool flag = true;
            try
            {
                //סידור שאסור לדווח בו פעילויות. נבדק רק מול סידורים מיוחדים.                                   
               //
                if ((oSidur.bSidurMyuhad) && (oSidur.bNoPeilotKodExists))
                    {
                        //נבדוק אם קיימות לסידור פעילויות
                        //foreach (DictionaryEntry dePeilut in oSidur.htPeilut)
                        //{
                            iPeilutMisparSidur = oPeilut.iPeilutMisparSidur;
                            //sMakatNesia = htEmployeeDetails["MakatNesia"].ToString();
                            if (iPeilutMisparSidur > 0)
                            {
                                if (oSidur.htPeilut.Count == 1)
                                {
                                    if (oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && oPeilut.bMisparSidurMatalotTnuaExists && oPeilut.iMisparSidurMatalotTnua == iPeilutMisparSidur)
                                        flag = false;
                                }
                                if (flag)
                                {
                                    drNew = dtErrors.NewRow();
                                    InsertErrorRow(oSidur, ref drNew, "פעילות אסורה בסדור תפקיד", enErrors.errPeilutForSidurNonValid.GetHashCode());
                                    //drNew["shat_yetzia"] = oPeilut.sShatYetzia;
                                    InsertPeilutErrorRow(oPeilut, ref drNew);
                                    dtErrors.Rows.Add(drNew);
                                    isValid = false;
                                }
                            
                            }
                       // }                        
                    }
                }                
            //}
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errPeilutForSidurNonValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.dFullShatYetzia, oPeilut.iMisparKnisa, "IsPeilutInSidurValid84: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsCharigaValid32(ref clSidur oSidur, ref DataTable dtErrors)
        {
            bool bError=false;
            string sLookUp="";
            bool isValid = true;

            //בדיקה ברמת סידור
            try
            {
                //אם שדה חריגה תקין  - לבדוק תקינות לפי ערכים בטבלה CTB_DIVUCH_HARIGA_MESHAOT.    וסידור אינו זכאי לחריגה (סידור זכאי חריגה אם יש לו מאפיין 35 (זכאי לחריגה) במאפייני סידורים מיוחדים                                      ושעת גמר קטנה מ- 28
                if (string.IsNullOrEmpty(oSidur.sChariga))
                {
                    bError = true;
                } 
                else
                {
                    sLookUp = GetLookUpKods("ctb_divuch_hariga_meshaot");                
                    //אם ערך חריגה לא נמצא בטבלה
                    if (sLookUp.IndexOf(oSidur.sChariga) == -1)
                    {
                        bError = true;
                    }                        
                }
                if (bError)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "חריגה שגוי", enErrors.errCharigaValueNotValid.GetHashCode());
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errCharigaValueNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsCharigaValid32: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsZakaiLeChariga34(ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור
            //clSidur oSidur;
            string sLookUp;
            int iShatGmar;
            DataRow drNew;
            bool isValid = true;

            try
            {   //אם שדה חריגה תקין  - לבדוק תקינות לפי ערכים בטבלה CTB_DIVUCH_HARIGA_MESHAOT.    וסידור אינו זכאי לחריגה (סידור זכאי חריגה אם יש לו מאפיין 35 (זכאי לחריגה) במאפייני סידורים מיוחדים                                      ושעת גמר קטנה מ- 28                
                if (oSidur.bSidurMyuhad)
                {//סידורים מיוחדים
                    if (!string.IsNullOrEmpty(oSidur.sChariga) && Int32.Parse(oSidur.sChariga) > 0)
                    {
                        if (!(string.IsNullOrEmpty(oSidur.sShatGmar)))
                        {
                            sLookUp = GetLookUpKods("ctb_divuch_hariga_meshaot");
                            iShatGmar = int.Parse(oSidur.sShatGmar.Remove(2, 1).Substring(0, 2));
                            //אם ערך חריגה תקין, אבל אין זכאות לחריגה נעלה שגיאה
                            if (((sLookUp.IndexOf(oSidur.sChariga)) != -1) && (!oSidur.bZakaiLeCharigaExists) && (iShatGmar < 28))  //לא קיים מאפיין 35
                            {
                                drNew = dtErrors.NewRow();
                                InsertErrorRow(oSidur, ref drNew, "סידור אינו זכאי לחריגה", enErrors.errZakaiLeCharigaValueNotValid.GetHashCode());
                                dtErrors.Rows.Add(drNew);
                                isValid = false;
                            }
                        }
                    }
                }
                else
                {//סדורים רגילים
                    if (!string.IsNullOrEmpty(oSidur.sChariga) && Int32.Parse(oSidur.sChariga) > 0)
                    {
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "סידור אינו זכאי לחריגה", enErrors.errZakaiLeCharigaValueNotValid.GetHashCode());
                        dtErrors.Rows.Add(drNew);
                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errZakaiLeCharigaValueNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsZakaiLeChariga34: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsHashlamaForSidurValid48(float fSidurTime, ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור
            
            DataRow drNew;
            bool isValid = true;
          
            try
            {   //סידור מקבל קוד השלמת שעות למרות שלא צריך עפ"י הגדרת הזמן. למשל : השלמה לשעתיים למרות שהסידור גדול משעתיים. קודי ההשלמה יהיו בהתאם לקוד, לדוגמא:קוד 1 זה השלמה לשעה, קוד 2 השלמה לשעתיים וכן'                            
               
                    if (!string.IsNullOrEmpty(oSidur.sHashlama))
                    {
                        if ((int.Parse(oSidur.sHashlama)) > 0)
                        {
                            if ((!(string.IsNullOrEmpty(oSidur.sShatGmar))) && (oSidur.dFullShatHatchala.Year > clGeneral.cYearNull))
                            {
                                if (fSidurTime / 60 > int.Parse(oSidur.sHashlama))
                                {
                                    drNew = dtErrors.NewRow();
                                    InsertErrorRow(oSidur, ref drNew, "ערך השלמה לסידור שגוי", enErrors.errHashlamaForSidurNotValid.GetHashCode());
                                    //drNew["Hashlama"] = oSidur.sHashlama;
                                    dtErrors.Rows.Add(drNew);
                                    isValid = false;
                                }
                            }
                        }
                    }
                //}
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errHashlamaForSidurNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsHashlamaForSidurValid48: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }


        private bool IsShatPeilutNotValid121(DateTime dCardDate, ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors)
        {
            //בדיקה ברמת פעילות
            bool isValid = true;
            long lMakatNesia = oPeilut.lMakatNesia;
            clKavim oKavim = new clKavim();

            if ((clKavim.enMakatType)oPeilut.iMakatType == clKavim.enMakatType.mElement
                && oPeilut.iElementLeYedia == 2 && lMakatNesia > 0)
            {
                return isValid;
            }

            try
            {   //12עקב שינוי שעת התחלה של סידור יכול להיווצר מצב בו יש פעילות המתחילה לפני שעת התחלת הסידור החדשה. אין לבצע את הבדיקה אם הפעילות היא אלמנט (מתחיל ב- 7) והיא לידיעה. פעילות היא לידיעה לפי פרמטר 3 (פעולה / ידיעה בלבד) בטבלת מאפייני אלמנטים121-
                //122עקב שינוי שעת סיום של סידור יכול להיווצר מצב בו יש פעילות המתחילה אחרי שעת סיום הסידור החדשה. אין לבצע את הבדיקה אם הפעילות היא אלמנט (מתחיל ב- 7) והיא לידיעה.  פעילות היא לידיעה לפי פרמטר 3 (פעולה / ידיעה בלבד) בטבלת מאפייני אלמנטים122-
              
                    
                if (!(string.IsNullOrEmpty(oPeilut.sShatYetzia)))
                {
                    if ((!((oPeilut.iMakatType == (long)clKavim.enMakatType.mElement.GetHashCode()) && (oPeilut.iElementLeYedia == 2))) && (lMakatNesia > 0))
                    {
                        if (oSidur.dFullShatHatchala.Year > clGeneral.cYearNull)
                        {//בדיקה 121
                            if (oPeilut.dFullShatYetzia < oSidur.dFullShatHatchala)
                            {
                                drNew = dtErrors.NewRow();
                                InsertErrorRow(oSidur, ref drNew, "שעת פעילות נמוכה משעת התחלת הסידור", enErrors.errShatPeilutSmallerThanShatHatchalaSidur.GetHashCode());
                                InsertPeilutErrorRow(oPeilut, ref drNew);
                                dtErrors.Rows.Add(drNew);
                                isValid = false;
                            }
                        }
                        if (oSidur.dFullShatGmar != DateTime.MinValue)
                        {//בדיקה 122
                            if (oPeilut.dFullShatYetzia > oSidur.dFullShatGmar)
                            {
                                drNew = dtErrors.NewRow();
                                InsertErrorRow(oSidur, ref drNew, "שעת פעילות גדולה משעת סיום הסידור", enErrors.errShatPeilutBiggerThanShatGmarSidur.GetHashCode());
                                InsertPeilutErrorRow(oPeilut, ref drNew);
                                dtErrors.Rows.Add(drNew);
                                isValid = false;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errShatPeilutBiggerThanShatGmarSidur.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.dFullShatYetzia, oPeilut.iMisparKnisa, "IsShatPeilutNotValid121: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }


        private bool IsSidurimHoursNotValid16(int iSidur, ref  clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור           
            bool isValid = true;
            int i;
            DataRow drNew;
            clSidur oPrevSidur = (clSidur)htEmployeeDetails[iSidur -1];
            int iPrevLoLetashlum = oPrevSidur.iLoLetashlum;

            for (i = (iSidur-1); i >= 0; i--)
            {
                 oPrevSidur = (clSidur)htEmployeeDetails[i];

                 iPrevLoLetashlum = oPrevSidur.iLoLetashlum;
                if (iPrevLoLetashlum == 0)
                {
                    break;
                }
            }
            string sShatGmarPrev = oPrevSidur.sShatGmar;
           
            DateTime dShatHatchalaSidur = oSidur.dFullShatHatchala;
            DateTime dShatGmarPrevSidur = oPrevSidur.dFullShatGmar;
            try
            {
                if (dShatHatchalaSidur.Date != DateTime.MinValue.Date && dShatGmarPrevSidur.Date != DateTime.MinValue.Date)
                {
                    DateTime dPrevTime = new DateTime(dShatGmarPrevSidur.Year, dShatGmarPrevSidur.Month, dShatGmarPrevSidur.Day, int.Parse(dShatGmarPrevSidur.ToString("HH:mm").Substring(0, 2)), int.Parse(dShatGmarPrevSidur.ToString("HH:mm").Substring(3, 2)), 0);
                    DateTime dCurrTime = new DateTime(dShatHatchalaSidur.Year, dShatHatchalaSidur.Month, dShatHatchalaSidur.Day, int.Parse(dShatHatchalaSidur.ToString("HH:mm").Substring(0, 2)), int.Parse(dShatHatchalaSidur.ToString("HH:mm").Substring(3, 2)), 0);
                    //אם גם הסידור הקודם וגם הסידור הנוכחי הם לתשלום, נבצע את הבדיקה
                    if ((oSidur.iLoLetashlum == 0 || (oSidur.iLoLetashlum == 1 && oSidur.iKodSibaLoLetashlum == 1)) && (iPrevLoLetashlum == 0 || (oPrevSidur.iLoLetashlum == 1 && oPrevSidur.iKodSibaLoLetashlum == 1)))
                    {
                        if (dCurrTime < dPrevTime)
                        {
                            drNew = dtErrors.NewRow();
                            InsertErrorRow(oSidur, ref drNew, "קיימת חפיפה בשעות סידורים", enErrors.errSidurimHoursNotValid.GetHashCode());
                            dtErrors.Rows.Add(drNew);
                            isValid = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errSidurimHoursNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurimHoursNotValid16: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsSidurExistsInShlila35(int iMisparIshi, DateTime dCardDate, ref DataTable dtErrors)
        {           
            bool isValid = true;
            //int iHehadrutTypeKod;
            //בדיקה ברמת יום עבודה
            try
            {
                //אסור שיהיה לעובד סידור כלשהו בזמן חופשת שלילה.
                //קוד מאפיין שלילה 72

                if (IsOvedBShlila() && htEmployeeDetails.Values.Cast<clSidur>().ToList().Any(sidur => !IsSidurHeadrut(sidur)))
                //if ((oMeafyeneyOved.Meafyen72Exists) && (htEmployeeDetails.Count > 0))
                {
                    drNew = dtErrors.NewRow();
                    drNew["check_num"] = enErrors.errSidurExistsInShlila.GetHashCode();
                    drNew["mispar_ishi"] = iMisparIshi;
                    drNew["taarich"] = dCardDate.ToShortDateString();
                    //drNew["error_desc"] = "קיימים סידורים בזמן חופשת שלילה"; 
                    //InsertErrorRow( (clSidur)htEmployeeDetails[0], ref drNew, "קיימים סידורים בזמן חופשת שלילה", enErrors.errSidurExistsInShlila.GetHashCode());                   
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch(Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, iMisparIshi, "E", enErrors.errSidurExistsInShlila.GetHashCode(), dCardDate, "IsSidurExistsInShlila35: " + ex.Message);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsVisaInSidurRagil58(ref  clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור           
            DataRow drNew;
            bool isValid = true;

            try
            { //בסידור שאינו ויזה אסור שיהיה סימון בשדה קוד יום ויזה. מזהים סידור ויזה לפי  מאפיין (45) בטבלת סידורים מיוחדים. סמון ויזה יכול להגיע מהסדרן.
                if ((!oSidur.bSidurVisaKodExists) && (!String.IsNullOrEmpty(oSidur.sVisa)) && Int32.Parse(oSidur.sVisa) > 0 )
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "קיים סימון ויזה בסידור רגיל", enErrors.errSidurVisaNotValid.GetHashCode());
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errSidurVisaNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsVisaInSidurRagil58: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsSidurEilatValid55(int curSidurIndex, DateTime dCardDate, clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור           
            bool isValid = true;
            bool bCurrSidurEilat = false;
            string par = string.Empty;
            clSidur oPrevSidur = null;
            clPeilut tmpPeilut = null;
            float SachHamtana=0;
            if (oSidur.bSidurEilat && oSidur.IsLongEilatTrip(dCardDate, out tmpPeilut, _oParameters.fOrechNesiaKtzaraEilat))
            {
                bCurrSidurEilat = true;
            }

            // if the current sidur isn't SidurEilat then we shouldn't check anything
            if (!bCurrSidurEilat) isValid = false;
            else
            {
                bool bPrevSidurEilat = false;

                for (int index = 0; index < curSidurIndex; index++)
                {                    
                    oPrevSidur = (clSidur)htEmployeeDetails[index];

                    if (oPrevSidur.bSidurEilat && oPrevSidur.IsLongEilatTrip(dCardDate, out tmpPeilut, _oParameters.fOrechNesiaKtzaraEilat))
                    {
                        bPrevSidurEilat = true;
                    }

                    if (bPrevSidurEilat) break;
                }

                try
                { //צריך להיות שעה הפרש בין שני סידורי אילת. מזהים סידור אילת אם יש לו פעילות אילת. מזהים פעילות אילת לפי שדה שחוזר מהפרוצדורה GetKavDetails.
                    if (bPrevSidurEilat && bCurrSidurEilat)
                    {
                        SachHamtana = oPrevSidur.htPeilut.Values.Cast<clPeilut>().ToList().Sum(peilut =>
                        {
                            if (peilut.bElementHamtanaExists && peilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3)=="735")
                                return Int32.Parse(peilut.lMakatNesia.ToString().PadLeft(8).Substring(3, 3));
                            else return 0;
                        });



                        //foreach (clPeilut oPeilut in oPrevSidur.htPeilut.Values.Cast<clPeilut>().ToList())
                        //{
                        //    if (oPeilut.bElementHamtanaExists)
                        //    {
                        //        SachHamtana += Int32.Parse(oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(3, 3));
                        //    }
                        //}
                        if ((oSidur.dFullShatHatchala.Subtract(oPrevSidur.dFullShatGmar).TotalMinutes < 60)
                             && ((SachHamtana + oSidur.dFullShatHatchala.Subtract(oPrevSidur.dFullShatGmar).TotalMinutes) < 60))
                       //    && oPrevSidur.htPeilut.Values.Cast<clPeilut>().ToList().Any(peilut => (peilut.bElementHamtanaExists && Int32.Parse(peilut.lMakatNesia.ToString().PadLeft(8).Substring(3, 3)) + oSidur.dFullShatHatchala.Subtract(oPrevSidur.dFullShatGmar).TotalMinutes < 60))))
                        {
                           
                            drNew = dtErrors.NewRow();
                            InsertErrorRow(oSidur, ref drNew, "סידור אילת ללא הפסקה כנדרש לפני הסידור ", enErrors.errSidurEilatNotValid.GetHashCode());
                            dtErrors.Rows.Add(drNew);
                            isValid = false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errSidurEilatNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurEilatValid55: " + ex.Message, null);
                    isValid = false;
                    _bSuccsess = false;
                }
            }

            return isValid;
        }

        private bool IsOtoNoValid69(DateTime dCardDate, ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors)
        {
            clKavim oKavim = new clKavim();
            DataRow drNew;
            bool isValid = true;
            int kod;
            //בדיקה ברמת פעילות
            try
            {
                clKavim.enMakatType oMakatType = (clKavim.enMakatType)oPeilut.iMakatType;
                if (((oMakatType == clKavim.enMakatType.mKavShirut) || (oMakatType == clKavim.enMakatType.mEmpty) || (oMakatType == clKavim.enMakatType.mNamak) || (oMakatType == clKavim.enMakatType.mVisa)
                    || (oMakatType == clKavim.enMakatType.mElement && oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "700")) || ((oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode()) && oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "700"  && (oPeilut.bBusNumberMustExists) && (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "701") && (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "712") && (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "711")))
                {
                    //בודקים אם הפעילות דורשת מספר רכב ואם הוא קיים וחוקי (מול מש"ר). פעילות דורשת מספר רכב אם מרוטינת זיהוי מקט חזר פרמטר שונה מאלמנט. אם חזר מהרוטינה אלנמט יש לבדוק אם דורש מספר רכב. תהיה טבלה של מספר פעילות המתחילים ב- 7 ולכל רשומה יהיה מאפיין אם הוא דורש מספר רכב. בטבלת מאפייני אלמנטים (11 - חובה מספר רכב)
                    //בדיקת מספר רכב מול מש"ר

                    if (oPeilut.lOtoNo > 0)
                    {
                        kod = oKavim.IsBusNumberValid(oPeilut.lOtoNo, dCardDate);
                       // if (!(oKavim.IsBusNumberValid(oPeilut.lOtoNo, dCardDate)))
                        if (kod==1 || kod==2)
                        {
                            drNew = dtErrors.NewRow();
                            InsertErrorRow(oSidur, ref drNew, "מספר רכב לא תקין/חסר מספר רכב", enErrors.errOtoNoNotExists.GetHashCode());
                            InsertPeilutErrorRow(oPeilut, ref drNew);
                            //drNew["oto_no"] = oPeilut.lOtoNo;
                            //drNew["makat_nesia"] = oPeilut.lMakatNesia;
                            dtErrors.Rows.Add(drNew);
                            isValid = false;
                        }
                    }
                    else //חסר מספר רכב
                    {

                        //שגיאה 69
                        //בודקים אם הפעילות דורשת מספר רכב ואם הוא קיים וחוקי (מול מש"ר). פעילות דורשת מספר רכב אם מרוטינת זיהוי מקט חזר פרמטר שונה מאלמנט. אם חזר מהרוטינה אלנמט יש לבדוק אם דורש מספר רכב. תהיה טבלה של מספר פעילות המתחילים ב- 7 ולכל רשומה יהיה מאפיין אם הוא דורש מספר רכב. בטבלת מאפייני אלמנטים (11 - חובה מספר רכב)

                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "מספר רכב לא תקין/חסר מספר רכב", enErrors.errOtoNoNotExists.GetHashCode());
                        InsertPeilutErrorRow(oPeilut, ref drNew);
                        //drNew["oto_no"] = oPeilut.lOtoNo;
                        // drNew["makat_nesia"] = oPeilut.lMakatNesia;
                        dtErrors.Rows.Add(drNew);
                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errOtoNoNotExists.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.dFullShatYetzia, oPeilut.iMisparKnisa, "IsOtoNoValid69: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }
        private bool IsRechevMushbat211(DateTime dCardDate, ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors)
        {
            clKavim oKavim = new clKavim();
            DataRow drNew;
            bool isValid = true;
            int kod;
            //בדיקה ברמת פעילות
            try
            {
                clKavim.enMakatType oMakatType = (clKavim.enMakatType)oPeilut.iMakatType;
                if (((oMakatType == clKavim.enMakatType.mKavShirut) || (oMakatType == clKavim.enMakatType.mEmpty) || (oMakatType == clKavim.enMakatType.mNamak) || (oMakatType == clKavim.enMakatType.mVisa)
                    || (oMakatType == clKavim.enMakatType.mElement && oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "700")) || ((oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode()) && oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "700"  && (oPeilut.bBusNumberMustExists) && (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "701") && (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "712") && (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "711")))
                {
                    //בודקים אם הפעילות דורשת מספר רכב ואם הוא קיים וחוקי (מול מש"ר). פעילות דורשת מספר רכב אם מרוטינת זיהוי מקט חזר פרמטר שונה מאלמנט. אם חזר מהרוטינה אלנמט יש לבדוק אם דורש מספר רכב. תהיה טבלה של מספר פעילות המתחילים ב- 7 ולכל רשומה יהיה מאפיין אם הוא דורש מספר רכב. בטבלת מאפייני אלמנטים (11 - חובה מספר רכב)
                    //בדיקת מספר רכב מול מש"ר

                    if (oPeilut.lOtoNo > 0)
                    {
                        kod = oKavim.IsBusNumberValid(oPeilut.lOtoNo, dCardDate);
                       // if (!(oKavim.IsBusNumberValid(oPeilut.lOtoNo, dCardDate)))
                        if (kod==3)
                        {
                            drNew = dtErrors.NewRow();
                            InsertErrorRow(oSidur, ref drNew, "רכב מושבת", enErrors.errRechevMushbat.GetHashCode());
                            InsertPeilutErrorRow(oPeilut, ref drNew);
                            //drNew["oto_no"] = oPeilut.lOtoNo;
                            //drNew["makat_nesia"] = oPeilut.lMakatNesia;
                            dtErrors.Rows.Add(drNew);
                            isValid = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errRechevMushbat.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.dFullShatYetzia, oPeilut.iMisparKnisa, "IsRechevMushbat211: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }
        

      
        private bool MisparSiduriOtoNotExists139(ref clPeilut oPeilut, ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור           
            DataRow drNew;
            bool isValid = true;

            try
            {
                //בסידור המכיל נסיעת שירות עבורה קיים 71=Onatiut  - חובה שידווח מספר סידורי של רכב  - גדול או שווה ל- 1, אחרת - שגוי.
                if (oPeilut.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode() && oPeilut.iMisparKnisa==0 &&
                    oPeilut.iOnatiyut == 71 && oPeilut.lMisparSiduriOto == 0 && oPeilut.bPeilutEilat)
                {                    
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "  נסיעה ללא רכב סידורי ", enErrors.errMisparSiduriOtoNotExists.GetHashCode());
                    InsertPeilutErrorRow(oPeilut, ref drNew);
                    drNew["sadot_nosafim"] = 1;
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errMisparSiduriOtoNotExists.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.dFullShatYetzia, oPeilut.iMisparKnisa, "MisparSiduriOtoNotExists139: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsMisparSidurEilatInRegularSidurExists140(ref clPeilut oPeilut, ref  clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת פעילות           
            DataRow drNew;
            bool isValid = true;

            try
            { //בסידור המכיל נסיעות לאילת חייב שיהיה שדה המכיל את המספר הסידורי של האוטובוס. שדה זה אסור בסידור שאינו כולל נסיעת אילת וזו השגיאה. 
                if (!(oPeilut.bPeilutEilat) && (oPeilut.lMisparSiduriOto > 0))
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "רכב סידורי ללא נסיעת אילת", enErrors.errMisparSiduriOtoNotInSidurEilat.GetHashCode());
                    InsertPeilutErrorRow(oPeilut, ref drNew);
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errMisparSiduriOtoNotInSidurEilat.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.dFullShatYetzia, oPeilut.iMisparKnisa, "IsMisparSidurEilatInRegularSidurExists140: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }
        private bool IsDuplicateShatYetiza103(int iMisparIshi, DateTime dCardDate, ref DataTable dtErrors)
        {
            //בדיקה ברמת כרטיס עבודה
            //שני מקטים לאותו עובד עם אותה שעת יציאה. הבדיקה תתבצע תמיד לכל הפעילויות שאינן אלמנט (זיהוי סוג הפעילות יתבצע ע"פ "רוטינת זיהוי סוג מקט") לפעילויות שהן אלמנט, יש לבדוק בטבלת מאפיינים לאלמנטים אם אין לו מאפיין 9 ("להתעלם בבדיקת חפיפה בין נסיעות"). רק לאלמנט שאין לו את המאפיין הזה יש לעשות את הבדיקה. 
            //נבדוק שלא קיימות שתי פעילויות עם אותה שעת יציאה. הבדיקה נעשית על כל 
            //הפעילויות של כרטיס העבודה 
            bool isValid = true;

            try
            {
                Dictionary<string, clPeilut> peiluyot = new Dictionary<string, clPeilut>();
                bool shouldProcess = true;
                clKavim oKavim = new clKavim();
                DataTable dtMeafyenim = null;

                htEmployeeDetails.Values
                                 .Cast<clSidur>()
                                 .ToList()
                                 .ForEach
                                 (
                                    sidur =>
                                    {
                                        sidur.htPeilut.Values
                                                      .Cast<clPeilut>()
                                                      .ToList()
                                                      .ForEach
                                                      (
                                                        peilut =>
                                                        {
                                                            if (isValid)
                                                            {
                                                                if ((clKavim.enMakatType)peilut.iMakatType == clKavim.enMakatType.mElement)
                                                                {
                                                                    dtMeafyenim = oKavim.GetMeafyeneyElementByKod(peilut.lMakatNesia, dCardDate);
                                                                    if (dtMeafyenim.Select("KOD_MEAFYEN = 9") != null)
                                                                    {
                                                                        shouldProcess = false;
                                                                    }
                                                                    else
                                                                    {
                                                                        shouldProcess = true;
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    shouldProcess = true;
                                                                }

                                                                if (shouldProcess)
                                                                {
                                                                    if (!peiluyot.ContainsKey(peilut.dFullShatYetzia.ToString()))
                                                                    {
                                                                        peiluyot.Add(peilut.dFullShatYetzia.ToString(), peilut);
                                                                    }
                                                                    else
                                                                    {
                                                                        if (peilut.iMisparKnisa == 0 && peiluyot[peilut.dFullShatYetzia.ToString()].iMisparKnisa == 0)
                                                                        {
                                                                            isValid = false;
                                                                        }
                                                                    }                             
                                                                }
                                                            }
                                                        }
                                                      );                                                                              
                                    }
                                 );               

                if (!isValid)
                {
                    drNew = dtErrors.NewRow();
                    drNew["check_num"] = enErrors.errDuplicateShatYetiza.GetHashCode();
                    drNew["mispar_ishi"] = iMisparIshi;
                    drNew["taarich"] = dCardDate.ToShortDateString();
                    //drNew["error_desc"] = "שעת יציאה זהה בשתי פעילויות לאותו עובד";                   
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, iMisparIshi, "E", enErrors.errDuplicateShatYetiza.GetHashCode(), dCardDate, "IsDuplicateShatYetiza103: " + ex.Message);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool  IsZakaiLina31(ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors)
        {
            DataRow drNew;
            bool isValid = true;

            //בדיקה ברמת פעילות
            try
            {
                //אם  יש ערך בשדה לינה ובסידור האחרון יש פעילות מסוג אלמנט (לפי רוטינת זיהוי מקט) ולאלמנט יש מאפיין המתנה (15) - יוצא לשגיאה
                if (!string.IsNullOrEmpty(oOvedYomAvodaDetails.sLina))
                {
                    if ((oSidur.iMisparSidur == iLastMisaprSidur) && (int.Parse(oOvedYomAvodaDetails.sLina) > 0) && (oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode()) && (oPeilut.bElementHamtanaExists) && oPeilut.sHamtanaEilat == "1")
                    {
                        drNew = dtErrors.NewRow();
                        drNew["check_num"] = enErrors.errLoZakaiLLina.GetHashCode();
                        drNew["mispar_ishi"] = oOvedYomAvodaDetails.iMisparIshi;
                        drNew["taarich"] = _dCardDate.ToShortDateString();
                        //drNew["Lina"] = int.Parse(sLina);
                        //drNew["error_desc"] = "לא זכאי ללינה";                        
                        dtErrors.Rows.Add(drNew);

                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errLoZakaiLLina.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.dFullShatYetzia, oPeilut.iMisparKnisa, "IsZakaiLina31: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsOtoNoExists68(DataRow[] drSugSidur, ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors)
        {
            bool isValid = true;

            //בדיקה ברמת פעילות
            try
            {
                //אסור לדווח רכב בסידור שיש לו מאפיין 43 (אסור לדווח מספר רכב). הבדיקה רלוונטית גם לסידורים מיוחדים וגם לסידורים רגילים.                
                if (oPeilut.lOtoNo > 0)
                {
                    //TB_Sidurim_Meyuchadim נבדוק אם קיים מאפיין 43: לסידורים מיוחדים נבדוק בטבלת 
                    //לסידורים רגילים נבדוק את המאפיין מהתנועה
                    if (oSidur.bSidurMyuhad)
                    {
                        if ((oSidur.bNoOtoNoExists) && (oSidur.sNoOtoNo=="1"))
                        {
                            drNew = dtErrors.NewRow();
                            InsertErrorRow(oSidur, ref drNew, "מספר רכב בסידור תפקיד", enErrors.errOtoNoExists.GetHashCode());
                            //drNew["oto_no"] = oPeilut.lOtoNo;
                            InsertPeilutErrorRow(oPeilut, ref drNew);
                            //drNew["makat_nesia"] = oPeilut.lMakatNesia;
                            dtErrors.Rows.Add(drNew);
                            isValid = false;
                        }
                    }
                    else //סידור רגיל
                    {
                        if (drSugSidur.Length > 0)
                        {
                            if ((!String.IsNullOrEmpty(drSugSidur[0]["asur_ledaveach_mispar_rechev"].ToString())) && (drSugSidur[0]["asur_ledaveach_mispar_rechev"].ToString() =="1"))
                            {
                                drNew = dtErrors.NewRow();
                                InsertErrorRow(oSidur, ref drNew, "מספר רכב בסידור תפקיד", enErrors.errOtoNoExists.GetHashCode());
                                //drNew["oto_no"] = oPeilut.lOtoNo;
                                //drNew["makat_nesia"] = oPeilut.lMakatNesia;
                                InsertPeilutErrorRow(oPeilut, ref drNew);
                                dtErrors.Rows.Add(drNew);
                                isValid = false;
                            }
                        }
                    }
                }                
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errOtoNoExists.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsOtoNoExists68: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsNesiaTimeNotValid91(float fSidurTime, DateTime dCardDate, ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors)
        {
            int iElementTime = 0;            
            DataRow drNew;
            bool isValid = true;

            //בדיקה ברמת פעילות
            try
            {
                //ם פעילות היא אלמנט (לפי רוטינת זיהוי מקט) והאלמנט הוא נהגות (לפי ערך 1 (נהגות) במאפיין 14 (סקטור הצבירה של זמן האלמנט) בטבלת מאפייני אלמנטים) והאלמנט הוא בדקות (לפי ערך 1 (דקות) במאפיין 4 (ערך האלמנט) בטבלת מאפייני אלמנטים) אז לוקחים את זמן האלמנט (פוזיציות 4-6) מכפילים באחוזים (פרמטר 42 (פקטור נסיעות שירות בין גמר לתכנון) בטבלת פרמטרים כלליים)). אם התוצאה גדולה מזמן הסידור (גמר מינוס התחלה) אז יוצאת שגיאה.
                // אם הפעילות היא אלמנט
                //,והאלמנט הוא נהגות ובדקות
                //אז נבדוק שזמן הסידור לא חורג
                if ((oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode()) && (oPeilut.sElementZviraZman == clGeneral.enSectorZviratZmanForElement.ElementZviratZman.GetHashCode().ToString()) && (oPeilut.sElementInMinutes == "1"))
                {
                    if (oPeilut.lMakatNesia > 0)
                    {
                        //נקח את זמן האלמנט פוזיציות 4-6 באלמנט
                        iElementTime = int.Parse(oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(3, 3));
                        //נכפיל ב-פקטור נסיעות שירות בין גמר לתכנון, את הפקטור נקח מטבלת פרמטרים, פרמטר מספר 42
                        //fFactor = float.Parse(GetOneParam(42, dCardDate));
                        //אם זמן האלמנט * פקטור נסיעות הסידור גדול מזמן הסידור נעלה שגיאה
                        if (((100 + oParam.fFactor) / 100 * iElementTime) > (fSidurTime))
                        {
                            drNew = dtErrors.NewRow();
                            InsertErrorRow(oSidur, ref drNew, "זמן נסיעה חריג", enErrors.errNesiaTimeNotValid.GetHashCode());
                            //drNew["nesia_time"] = (100 + oParam.fFactor) / 100 * iElementTime;
                            //drNew["sidur_time"] = fSidurTime;
                            InsertPeilutErrorRow(oPeilut, ref drNew);
                            //drNew["makat_nesia"] = oPeilut.lMakatNesia;
                            dtErrors.Rows.Add(drNew);
                            isValid = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errNesiaTimeNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.dFullShatYetzia, oPeilut.iMisparKnisa, "IsNesiaTimeNotValid91: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsTeoodatNesiaValid52(ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors)
        {
            DataRow drNew;
            bool isValid = true;
            //בדיקה ברמת פעילות

            try
            {
                //רק לסידור ויזה עפ"י סימון בטבלת סידורים מיוחדים מותר שתהיה תעודת נסיעה (שדה מספר ויזה). יודעים שסידור מיוחד הוא ויזה לפי מאפיין (45) בטבלת סידורים מיוחדים.
                if ((!(oSidur.bSidurMyuhad && (oSidur.bSidurVisaKodExists))) && (oPeilut.lMisparVisa > 0))
                {                                       
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "תעודת נסיעה לא בסדור ויזה", enErrors.errTeoodatNesiaNotInVisa.GetHashCode());
                    //drNew["mispar_visa"] = oPeilut.lMisparVisa;
                    //drNew["makat_nesia"] = oPeilut.lMakatNesia;
                    InsertPeilutErrorRow(oPeilut, ref drNew);
                    dtErrors.Rows.Add(drNew);
                    isValid = false;                    
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errTeoodatNesiaNotInVisa.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.dFullShatYetzia, oPeilut.iMisparKnisa, "IsTeoodatNesiaValid52: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsHashlamatHazmanaValid49(float fSidurTime, ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור           
            DataRow drNew;
            bool isValid = true;
            int iZmanMinimum = 0;

            try
            {   //לסידורים להם אסור לדווח השלמה - יטופל דרך טבלת סידורים מיוחדים. ניתן לדווח השלמה אם לסידור מיוחד קיים מאפיין  40 (זכאי להשלמה עבור הסידור) בטבלת מאפיינים סידורים מיוחדים. עבור סידור שאינו מיוחד תמיד זכאי להשלמה כל עוד זמן הסידור קטן nמהערכים בפרמטרים 101-103 (זמן מינימום לסידור חול להשלמה, זמן מינימום לסידור שישי/ע.ח להשלמה, זמן מינימום לסידור שבתון להשלמה).
             if (!string.IsNullOrEmpty(oSidur.sHashlama))
             {
                if ((oSidur.bSidurMyuhad) && (oSidur.iMisparSidurMyuhad>0))
                {
                    if (oSidur.sHashlamaKod!="1")
                    {
                        if ((oSidur.bHashlamaExists) && (!String.IsNullOrEmpty(oSidur.sHashlama)))
                        {
                            if (int.Parse(oSidur.sHashlama) > 0)
                            {
                                drNew = dtErrors.NewRow();
                                InsertErrorRow(oSidur, ref drNew, "השלמת הזמנה אסורה", enErrors.errHahlamatHazmanaNotValid.GetHashCode());
                                dtErrors.Rows.Add(drNew);
                                isValid = false;
                            }
                        }
                    }
                }
                else //סידור רגיל
                {
                    if ((!oSidur.bSidurMyuhad) && (int.Parse(oSidur.sHashlama) > 0))
                    {
                        if (clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, iSugYom, oSidur.dSidurDate))
                        {
                            iZmanMinimum = oParam.iHashlamaShabat;
                        }
                        else
                        {
                            if ((oSidur.sErevShishiChag == "1") || (oSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()))
                            {
                                iZmanMinimum = oParam.iHashlamaShisi;
                            }
                            else
                            {
                                iZmanMinimum = oParam.iHashlamaYomRagil;
                            }
                        }
                        if (fSidurTime > iZmanMinimum)
                        {
                            drNew = dtErrors.NewRow();
                            InsertErrorRow(oSidur, ref drNew, "השלמת הזמנה אסורה", enErrors.errHahlamatHazmanaNotValid.GetHashCode());
                            dtErrors.Rows.Add(drNew);
                            isValid = false;
                        }
                    }
                }      
             }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errHahlamatHazmanaNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsHashlamatHazmanaValid49: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsTotalHashlamotInCardValid142(int iTotalHashlamotForSidur, ref  clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה בכרטיס עבודה
            int iZmanMaximum = 0;
            bool isValid = true;

            try
            {
                //יש מספר מקסימלי של השלמות המותר ליום, תלוי בסוג היום. בודקים את סוג היום ולפי סוג היום בודקים בטבלת פרמטרים חיצוניים מה מקסימום ההשלמות המותר ליום. 108 (מכסימום השלמות ביום חול), 109 (מכסימום השלמות בשישי/ע.ח), 110 (מכסימום השלמות בשבתון). אם בודקים יום אל מול טבלת סוגי ימים מיוחדים והוא אינו מוגדר כשבתון או ערב שבת/חג  - יום זה הוא יום חול.
                if (clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, iSugYom, oSidur.dSidurDate))
                {
                    iZmanMaximum = oParam.iHashlamaMaxShabat;
                }
                else
                {
                    if ((oSidur.sErevShishiChag == "1") || (oSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()))
                    {
                        iZmanMaximum = oParam.iHashlamaMaxShisi;
                    }
                    else
                    {
                        iZmanMaximum = oParam.iHashlamaMaxYomRagil;
                    }
                }
                if (iTotalHashlamotForSidur > iZmanMaximum)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "מספר השלמות גדול מהמותר ליום עבודה ", enErrors.errTotalHashlamotBiggerThanAllow.GetHashCode());
                    //drNew["total_hashlamot"] = iTotalHashlamotForSidur;
                    //drNew["zman_maximum"] = iZmanMaximum;
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errTotalHashlamotBiggerThanAllow.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsTotalHashlamotInCardValid142: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private void IsSidurMissingNumStore143(clSidur oSidur, ref DataTable dtErrors)
        {
            bool isSidurValid = true;
            try
            {
                if (oSidur.bSidurMyuhad)
                {//בסידור "מתגבר מחסן" (מזהים לפי מאפיין 36 (חובה לדווח מספר מחסן) בטבלת מאפייני סידורים מיוחדים) בודקים האם הוכנס מספר מחסן, אם חסר - שגוי. (כדי שניתן יהיה לצרפו למערכת חישוב פרמיות אחסנה). מספר מחסן זה לא שדה שמגיע מהסדרן. 
                    if (oSidur.bHovaMisparMachsan && oSidur.iMisparMusachOMachsan==0)
                    {
                        isSidurValid = false;
                    }

                    if (!isSidurValid)
                    {
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "חסר מספר מחסן", enErrors.errMissingNumStore.GetHashCode());
                        drNew["sadot_nosafim"] = 1;
                        dtErrors.Rows.Add(drNew);
                    }
                 }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errMissingNumStore.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurMissingNumStore143: " + ex.Message, null);
                _bSuccsess = false;
            }


        }

        private bool IsSidurVisaValid57(ref clSidur oSidur, ref DataTable dtErrors)
        {
            string sLookUp = "";
            bool isValid = true;

            //בדיקה ברמת סידור
            try
            {
                //סידור ויזה חייב סימון כלשהו בשדה "יום ויזה". יש ערכים מותרים לשדה זה. קיימת טבלת ערכים עבור יום ויזה. מזהים סידור ויזה לפי  מאפיין (45) בטבלת סידורים מיוחדים. נתון זה יגיע גם בהשלמת נתונים, לא רק בהקלדה.
                if (oSidur.bSidurVisaKodExists)
                {
                    sLookUp = GetLookUpKods("CTB_YOM_VISA");
                    int tmpVisaCode = 0;
                    Int32.TryParse(oSidur.sVisa, out tmpVisaCode);
                    if ((sLookUp.IndexOf(oSidur.sVisa) == -1) || (string.IsNullOrEmpty(oSidur.sVisa)) || tmpVisaCode == 0)
                    {
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "סדור ויזה ללא סימון", enErrors.errSimunVisaNotValid.GetHashCode());
                        //drNew["visa"] = oSidur.sVisa;
                        drNew["sadot_nosafim"] = 1;
                        dtErrors.Rows.Add(drNew);
                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errSimunVisaNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurVisaValid57: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsYomVisaValid56(ref clSidur oSidur, ref DataTable dtErrors)
        {
            string sLookUp="";
            bool isValid = true;

            //בדיקה ברמת סידור
            try
            {
                //סידור ויזה חייב סימון כלשהו בשדה "יום ויזה". יש ערכים מותרים לשדה זה. קיימת טבלת ערכים עבור יום ויזה. מזהים סידור ויזה לפי  מאפיין (45) בטבלת סידורים מיוחדים. נתון זה יגיע גם בהשלמת נתונים, לא רק בהקלדה.
                if (oSidur.bSidurVisaKodExists)
                {
                    sLookUp = GetLookUpKods("CTB_YOM_VISA");                                      
                    if ((sLookUp.IndexOf(oSidur.sVisa) == -1) || (string.IsNullOrEmpty(oSidur.sVisa)))
                    {
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "יום ויזה שגוי", enErrors.errYomVisaNotValid.GetHashCode());
                        //drNew["visa"] = oSidur.sVisa;                        
                        dtErrors.Rows.Add(drNew);
                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errYomVisaNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsYomVisaValid56: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsHashlamaForComputerAndAccidentValid45(ref clSidur oSidur, ref DataTable dtErrors)
        {
            string sHashlamaLeyom;
            bool bError = false;
            bool isValid = true;

            //בדיקה ברמת סידור
            try
            {
                // איפיון קודם, לא רלוונטי- השלמה ליום עבודה (ערך 9) מותרת בשני מקרים: מפעיל מחשב (עיסוק 122 או  123) וסידור הוא מנהל (סידור מיוחד עם ערך 1 (שעונים) במאפיין 52 (אופי עבודה)). מקרה שני הוא סידור תאונה (סידור מיוחד עם ערך 3 (תאונה) במאפיין 53 (אופי העדרות)). 
                //אם יש סימון של השלמה ליום עבודה והסידור הוא יחיד ביום והוא מסוג העדרות (לפי מאפיין 53, לא משנה מה הערך במאפיין) או שהסידור מסומן לא לתשלום (רלוונטי לכל הסידורים, מיוחדים ורגילים) - שגיאה.
                sHashlamaLeyom = oOvedYomAvodaDetails.sHashlamaLeyom;
                if (sHashlamaLeyom=="1")
                {
                    if ((htEmployeeDetails.Count == 1))
                    {
                        if (oSidur.bSidurMyuhad)
                        {
                            if ((oSidur.bHeadrutTypeKodExists) ||  (oSidur.iLoLetashlum > 0 &&  oSidur.iKodSibaLoLetashlum!=1))
                            {
                                bError = true;
                            }
                        }
                        else
                        {
                            if (oSidur.iLoLetashlum > 0 && oSidur.iKodSibaLoLetashlum!=1)
                            {
                                bError = true;
                            }
                        }
                    }
                    if (bError)
                    {
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "השלמה ליום עבודה בסידור היעדרות / סידור לא לתשלום", enErrors.errHashlamaForComputerWorkerAndAccident.GetHashCode());
                        //drNew["hashlama"] = oSidur.sHashlama;
                        dtErrors.Rows.Add(drNew);
                        isValid = false;

                        
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errHashlamaForComputerWorkerAndAccident.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsHashlamaForComputerAndAccidentValid45: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool HasBothSidurEilatAndSidurVisa171(int iMisparIshi, DateTime dCardDate, ref DataTable dtErrors)
        {
            DataRow drNew;
            bool isValid = true;
            //bool hasBoth = false;
            bool hasSidurEilat = false;
            bool hasVisa = false;
            bool isLongNesiaToEilat = false;
            clPeilut tmpPeilut = null;
            try
            {
                htEmployeeDetails.Values.Cast<clSidur>()
                                        .ToList()
                                        .ForEach(
                                            sidur =>
                                            {
                                                if (sidur.bSidurEilat)
                                                {
                                                    hasSidurEilat = true;

                                                    if (sidur.IsLongEilatTrip(dCardDate, out tmpPeilut, _oParameters.fOrechNesiaKtzaraEilat)) isLongNesiaToEilat = true;
                                                }

                                                if (sidur.bSidurVisaKodExists) hasVisa = true;
                                            }
                                        );

                if (hasSidurEilat && isLongNesiaToEilat && hasVisa)
                {
                    drNew = dtErrors.NewRow();
                    drNew["check_num"] = enErrors.errHasBothSidurEilatAndSidurVisa.GetHashCode();
                    drNew["mispar_ishi"] =iMisparIshi;
                    drNew["taarich"] = dCardDate.ToShortDateString();
                    //drNew["error_desc"] = "סידור עם נסיעת אילת ארוכה וסידור ויזה באותו יום";
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, iMisparIshi, "E", enErrors.errHasBothSidurEilatAndSidurVisa.GetHashCode(), dCardDate, "HasBothSidurEilatAndSidurVisa171: " + ex.Message);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsZakaiLehamara44(DataRow[] drSugSidur, ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור 
            string sMaamad;
            bool isValid = true;

            try
            {
                if (!string.IsNullOrEmpty(oOvedYomAvodaDetails.sKodMaamd))
                {
                    sMaamad = oOvedYomAvodaDetails.sKodMaamd.Substring(0, 2);
                    //if (!string.IsNullOrEmpty(oSidur.sHamaratShabat) && oSidur.sHamaratShabat == "1")
                    //{
                    //    //סידור מיוחד
                    //    if ((oSidur.bSidurMyuhad) && (oSidur.iMisparSidurMyuhad > 0))
                    //    {
                    //        //אם סידור נהגות וקיים לעובד ותק, אז זכאי להמרת שבת
                    //        if (!((oSidur.sSectorAvoda == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString()) && ((sMaamad == clGeneral.enMaamad.Friends.GetHashCode().ToString()) || (sMaamad == "21") || (sMaamad == "22"))))
                    //        {
                    //            drNew = dtErrors.NewRow();
                    //            InsertErrorRow(oSidur, ref drNew, "המרה לעובד שלא זכאי ", enErrors.errZakaiLehamaratShabat.GetHashCode());
                    //            //drNew["hamarat_shabat"] = oSidur.sHamaratShabat;
                    //            dtErrors.Rows.Add(drNew);
                    //            isValid = false;
                    //        }
                    //    }
                    //    else //סידור רגיל
                    //    {
                    //        if (drSugSidur.Length > 0)
                    //        {
                    //            //if (!((drSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString()) && (((sMaamad.Substring(0,1) == clGeneral.enMaamad.Friends.GetHashCode().ToString()) || (sMaamad == "21") || (sMaamad == "22")))))
                    //            if (!(((drSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString()) && ((sMaamad.Substring(0, 1) == clGeneral.enMaamad.Friends.GetHashCode().ToString()) || (sMaamad == "21") || (sMaamad == "22") ))))
                    //            {
                    //                drNew = dtErrors.NewRow();
                    //                InsertErrorRow(oSidur, ref drNew, "המרה לעובד שלא זכאי ", enErrors.errZakaiLehamaratShabat.GetHashCode());
                    //                //drNew["hamarat_shabat"] = oSidur.sHamaratShabat;
                    //                dtErrors.Rows.Add(drNew);
                    //                isValid = false;
                    //            }
                    //        }
                    //    }
                    //}
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errZakaiLehamaratShabat.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsZakaiLehamara44: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsHamaratShabatAllowed43(DateTime dDateCard, ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור 
            bool bError=false;
            DataRow drNew;
            //DateTime dShatGmar;
            bool isValid = true;

            try
            {
                //אסורה המרה ביום שהוא לא שבתון או ערב שבת/חג החל משעון שבת. שבתון/ערב שבת יכול להגיע מטבלת ימים מיוחדים או מה- Oracle. עבור ערב שבת/חג יש לבדוק אם אנחנו בתקופת קיץ/חורף לפי פרמטרים 25 (תחילת זמן קיץ אגד) ו- 26 (סיום זמן קיץ אגד). לפי התקופה יש לבדוק את שעת כניסת השבת לפי פרמטרים 6 (כניסת שבת חורף) ו- 7 (כניסת שבת קיץ). לגבי ערב שבת/חג בודקים ששעת גמר הסידור היא לפני כניסת שבת/חג.
                if (((oSidur.iMisparSidurMyuhad > 0) && (oSidur.bSidurMyuhad)) || (!(oSidur.bSidurMyuhad)))
                {
                    //if (!string.IsNullOrEmpty(oSidur.sHamaratShabat) && Int32.Parse(oSidur.sHamaratShabat) == 1) 
                    //{
                    //    if ((oSidur.sErevShishiChag == "1") || (oSidur.sShabaton == "1") || (oSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()) || (oSidur.sSidurDay == clGeneral.enDay.Shabat.GetHashCode().ToString()))
                    //    {
                    //        //אם יום שישי, נבדוק שלא נכנסה השבת
                    //        if ((oSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()) || (oSidur.sErevShishiChag == "1"))
                    //        {
                    //            if (!(string.IsNullOrEmpty(oSidur.sShatGmar)))
                    //            {
                    //            //אם שעת הגמר של הסידור גדולה משעת כניסת השבת, אז שבתון ומותרת המרה
                    //                dShatGmar = DateTime.Parse(string.Concat(dDateCard.ToShortDateString(), " ", oSidur.sShatGmar));                                
                    //                bError = (dShatGmar < oParam.dKnisatShabat); //פרמטר .7/6                                                           
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        //raise error - יש ערך המרה לא בשבת
                    //        bError = true;
                    //    }
                    //}
                    if (bError)
                    {
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "המרת שבת לא ביום שבתון ", enErrors.errHamaratShabatNotAllowed.GetHashCode());
                        //drNew["hamarat_shabat"] = oSidur.sHamaratShabat;
                        dtErrors.Rows.Add(drNew);
                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errHamaratShabatNotAllowed.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsHamaratShabatAllowed43: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsNesiaMeshtanaDefine150(DateTime dDateCard,  ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור             
            bool isValid = true;
            DataRow drNew;
            int iZmanNesiaHaloch, iZmanNesiaChazor;
            iZmanNesiaHaloch = -1;
            iZmanNesiaChazor = -1;
             
            try
            {
                //שגיאה חדשה - היום נבדק רק בחישוב !!! לעובד מגיע זמן נסיעה משתנה (מאפיין 61 במאפייני עובדים) עפ"י מקום החתמת השעון. נבנתה טבלה המכילה את זמני הנסיעה ממקום מגוריו של העובד (נקבע עפ"י מרכז הארוע - קוד נתון 10 בטבלת פרטי עובדים) אל מיקום שעון כניסה וממיקום שעון יציאה הביתה. לעיתים חסר נתון בטבלה וזאת השגיאה.  כדי לדעת מה הערך הרלוונטי לעובד יש להפעיל את "הפרוצדורה של שרי"
                if (oMeafyeneyOved.Meafyen61Exists && int.Parse(oMeafyeneyOved.sMeafyen61)>0) // אם קיים מאפיין 61, לעובד מגיע זמן נסיעה משתנה
                {
                    if (oOvedYomAvodaDetails.bMercazEruaExists)
                    {
                        if (IsOvedZakaiLZmanNesiaLaAvoda() || IsOvedZakaiLZmanNesiaLeMeAvoda() )
                        {
                             iZmanNesiaHaloch=GetZmanNesiaMeshtana(0, 1, dDateCard);
                         }

                        if (IsOvedZakaiLZmanNesiaMeAvoda() || IsOvedZakaiLZmanNesiaLeMeAvoda())
                        {
                            iZmanNesiaChazor = GetZmanNesiaMeshtana(htEmployeeDetails.Count-1, 1, dDateCard);
                         }

                        if (iZmanNesiaChazor == -1 || iZmanNesiaHaloch == -1)
                        {
                            drNew = dtErrors.NewRow();
                            drNew["mispar_ishi"] = _iMisparIshi;
                            drNew["taarich"] = dDateCard.ToShortDateString();
                            //drNew["error_desc"] = "חסרה הגדרה בטבלת זמן נסיעה משתנה ";
                            drNew["check_num"] = enErrors.errNesiaMeshtanaNotDefine.GetHashCode();
                            dtErrors.Rows.Add(drNew);
                            isValid = false;

                        }

                    }                        
                }                                    
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errNesiaMeshtanaNotDefine.GetHashCode(), _iMisparIshi, _dCardDate,null, null, null, null, "IsNesiaMeshtanaDefine150: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsOnePeilutExists127(ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור             
            DataRow drNew;
            bool isValid = true;
            
            try
            {
                //יש סידורים המחייבים לפחות פעילות אחת. אם לא קיימת - שגוי. עבור סידורים מיוחדים נבדק אל מול טבלת מאפייני סידורים מיוחדים מאפיין 13 (חובה לדווח פעילות). עבור סידור שאינו מיוחד - חובה שתהיה פעילות אחת לפחות.
               
                //אם סידור מיוחד וגם דורש לפחות פעילות אחת או סידור רגיל
                if (((oSidur.bSidurMyuhad) && (oSidur.bPeilutRequiredKodExists) && (oSidur.iMisparSidurMyuhad > 0)) || (!(oSidur.bSidurMyuhad)))
                {                    
                //אם אין פעילויות נעלה שגיאה
                    if (oSidur.htPeilut.Count == 0)
                    {
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "חובה לפחות פעילות אחת", enErrors.errAtLeastOnePeilutRequired.GetHashCode());                                                
                        dtErrors.Rows.Add(drNew);
                        isValid = false;
                    }                    
                }             
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errAtLeastOnePeilutRequired.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsOnePeilutExists127: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool ElementInSpecialSidurNotAllowed123(ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors)
        {
            DataRow drNew;
            clKavim oKavim = new clKavim();
            bool isValid = true;

            try
            {
                if (oSidur.bSidurMyuhad)
                {
                    if (oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && oPeilut.sDivuchInSidurMeyuchad == "1")
                    {
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "אלמנט אסור בסדור מיוחד", enErrors.errElementInSpecialSidurNotAllowed.GetHashCode());
                        InsertPeilutErrorRow(oPeilut, ref drNew);
                        dtErrors.Rows.Add(drNew);
                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errElementInSpecialSidurNotAllowed.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.dFullShatYetzia, oPeilut.iMisparKnisa, "ElementInSpecialSidurNotAllowed123: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsNesiaInSidurVisaAllowed125(ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors)
        {
            DataRow drNew;
            clKavim oKavim = new clKavim();
            bool isValid = true;

            //בדיקה ברמת פעילות
            try
            {
                //בסידורי ויזה מותר רק פעילויות שמתחילות ב- 5 (תעודת ויזה) או 7 (אלמנט). עבור אלמנט יש לבדוק אם הוא מותר בסידור ויזה (לפי ערך 2 (רשאי) במאפיין 12 (דיווח בסידור ויזה) בטבלת מאפייני אלמנטים). טבלת מאפייני אלמנטים תכיל מאפיינים לכל האלמנטים במערכת. מזהים סידור ויזה לפי מאפיין 45 בסידורים מיוחדים. 
                if (oSidur.bSidurVisaKodExists)
                {
                    if (!(oPeilut.iMakatType == clKavim.enMakatType.mVisa.GetHashCode() || (oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && oPeilut.sDivuchInSidurVisa == "2")))
                    {
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "נסיעה אסורה בסידור ויזה", enErrors.errNesiaInSidurVisaNotAllowed.GetHashCode());
                        InsertPeilutErrorRow(oPeilut, ref drNew);
                        dtErrors.Rows.Add(drNew);
                        isValid = false;
                    }
                }                
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errNesiaInSidurVisaNotAllowed.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.dFullShatYetzia, oPeilut.iMisparKnisa, "IsNesiaInSidurVisaAllowed125: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsOvedEggedTaavoraKodValid141(clGeneral.enEmployeeType enEmployeeType, ref clSidur oSidur, ref DataTable dtErrors)
        {
            bool isValid = true;
            DataRow drNew;

            //בדיקה ברמת כרטיס עבודה
            try
            {   //בודקים אם עובד הוא מאגד תעבורה. אם כן צריך שהשדות הבאים הבאים יקבלו את הערכים הבאים, אחרת זה שגוי: א. ביטול נסיעות = 4, ב. הלבשה = 4, ג. מחוץ למכסה = 0, ד. המרה = 0, ה.השלמה = 0. מזהים אגד תעבורה לפי קוד חברה של העובד.             
                if (enEmployeeType ==clGeneral.enEmployeeType.enEggedTaavora)
                {
                    if ((oOvedYomAvodaDetails.sBitulZmanNesiot!="4") && (!String.IsNullOrEmpty(oOvedYomAvodaDetails.sBitulZmanNesiot)))
                    {
                        drNew = dtErrors.NewRow();
                        //drNew["Bitul_Zman_Nesiot"] = oOvedYomAvodaDetails.sBitulZmanNesiot;
                        InsertErrorRow(oSidur, ref drNew, "קוד אסור לעובד אגד תעבורה", enErrors.errNotAllowedKodsForEggedTaavora.GetHashCode());
                        dtErrors.Rows.Add(drNew);
                        isValid = false;
                    }
                    if ((oOvedYomAvodaDetails.sHalbasha != "4") && (!String.IsNullOrEmpty(oOvedYomAvodaDetails.sHalbasha))) 
                    {
                        drNew = dtErrors.NewRow();
                        //drNew["Halbasha"] = oOvedYomAvodaDetails.sHalbasha;
                        InsertErrorRow(oSidur, ref drNew, "קוד אסור לעובד אגד תעבורה", enErrors.errNotAllowedKodsForEggedTaavora.GetHashCode());
                        dtErrors.Rows.Add(drNew);
                        isValid = false;
                    }
                    //if ((oSidur.sHamaratShabat != "0") && (!String.IsNullOrEmpty(oSidur.sHamaratShabat)))
                    //{
                    //    drNew = dtErrors.NewRow();
                    //    drNew["Hamarat_Shabat"] = oSidur.sHamaratShabat;
                    //    InsertErrorRow(oSidur, ref drNew, "קוד אסור לעובד אגד תעבורה", enErrors.errNotAllowedKodsForEggedTaavora.GetHashCode());
                    //    dtErrors.Rows.Add(drNew);
                    //    isValid = false;
                    //}
                    if ((oSidur.sHashlama != "0") && (!String.IsNullOrEmpty(oSidur.sHashlama)))
                    {
                        drNew = dtErrors.NewRow();
                        //drNew["Hashlama"] = oSidur.sHashlama;
                        InsertErrorRow(oSidur, ref drNew, "קוד אסור לעובד אגד תעבורה", enErrors.errNotAllowedKodsForEggedTaavora.GetHashCode());
                        dtErrors.Rows.Add(drNew);
                        isValid = false;
                    }
                    if ((oSidur.sOutMichsa!="0") && (!String.IsNullOrEmpty(oSidur.sOutMichsa)))
                    {                        
                        drNew = dtErrors.NewRow();
                        //drNew["Out_Michsa"] = oSidur.sOutMichsa;
                        InsertErrorRow(oSidur, ref drNew, "קוד אסור לעובד אגד תעבורה", enErrors.errNotAllowedKodsForEggedTaavora.GetHashCode());
                        dtErrors.Rows.Add(drNew);
                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errNotAllowedKodsForEggedTaavora.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsOvedEggedTaavoraKodValid141: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsSidurAllowedForEggedTaavora148(clGeneral.enEmployeeType enEmployeeType, ref clSidur oSidur, ref DataTable dtErrors)
        {
            bool isValid = true;

            //בדיקה ברמת סידור
            try
            {
                //לעובד בדרוג 085 (אגד תעבורה) אסור לבצע את הסידור (לפי מאפיין 31 אסור לדיווח לחברות בת בטבלת מאפייני סידורים מיוחדים). יש סימון בטבלת סידורים מיוחדים שהסידור אסור לעובדי שאינם אגד. עבור סידורים רגילים, אין לבדוק את השגיאה כי מראש הם לא ישובצו לסידורים אלו. מזהים אגד תעבורה לפי קוד חברה של העובד. 
                if (oOvedYomAvodaDetails.iDirug == 85 && oOvedYomAvodaDetails.iDarga==30)
                {
                    if ((oSidur.bSidurMyuhad) && (oSidur.bSidurNotValidKodExists))
                    {
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "סדור אסור לעובד בדרוג 85", enErrors.errNotAllowedSidurForEggedTaavora.GetHashCode());
                        dtErrors.Rows.Add(drNew);
                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errNotAllowedSidurForEggedTaavora.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurAllowedForEggedTaavora148: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsSidurNetzerNotValidForOved124(ref clSidur oSidur, ref DataTable dtErrors)
        {
            bool isValid = true;

            //בדיקה ברמת סידור
            try
            {
                //עובדים במרכז נ.צ.ר רשאים לעבוד שם רק לאחר שעברו הכשרה. בסיומה מדווחים להם מאפיין 64. מזהים סידור נ.צ.ר לפי מאפיין 52 ערך 11(סידור נצר) בטבלת סידורים מיוחדים.
                if ((oSidur.bSidurMyuhad) && (oSidur.sSugAvoda == clGeneral.enSugAvoda.Netzer.GetHashCode().ToString()) && (!oMeafyeneyOved.Meafyen64Exists))
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "לעובד אסור סידור נ.צ.ר", enErrors.errSidurNetzerNotValidForOved.GetHashCode());
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errSidurNetzerNotValidForOved.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurNetzerNotValidForOved124: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsSidurNamlakWithoutNesiaCard13(ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors)
        {
            bool isValid = true;

            //בדיקה ברמת פעילות
            try
            {
                //אם הסידור הוא נמל"ק (לפי מאפיין 45 (ויזה) במאפייני סידורים מיוחדים), יש לבדוק שיש לסידור תעודת נסיעה (ערך בשדה מספר ויזה)..
                if ((oSidur.bSidurMyuhad) && (oSidur.bSidurVisaKodExists) && (oSidur.iMisparSidurMyuhad>0))
                {
                    if ((oPeilut.lMisparVisa == 0) && (oPeilut.lMakatNesia > 0) && oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 1) == "5")  //אין תעודת נסיעה
                    {
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "סדור נמלק ללא תעודת נסיעה", enErrors.errSidurNamlakWithoutNesiaCard.GetHashCode());
                        InsertPeilutErrorRow(oPeilut, ref drNew);
                        drNew["sadot_nosafim"] = 1;
                        dtErrors.Rows.Add(drNew);
                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errSidurNamlakWithoutNesiaCard.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurNamlakWithoutNesiaCard13: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsSidurValidInShabaton50(ref clSidur oSidur, ref DataTable dtErrors)
        {
            bool isValid = true;

            //בדיקה ברמת סידור
            try
            {
                //יש לבדוק שהסידור המיוחד מותר בשבתון (לפי מאפיין 9 (אסור בשבתון) בטבלת מאפייני סידורים מיוחדים) ושלעובד מותר לעבוד בשבתון (יש לו מאפיין 7 (מאפיין זה אומר זמן התחלה מותר בשבת (שבתון בלבד לא כולל ערב)). שבתון יכול להיות יום שבת (חוזר מה- Oracle) או שבטבלת סוגי ימים מיוחדים הוא מוגדר כשבתון. במקרה זה השבת נחשבת החל משעות שבת (ערב שבת/חג). בדיקה זו לא רלוונטית עבור סידור לא מיוחד משום שבמקור הוא תוכנן לשבתון ואם זה תוכנן זה תקין. השגיאה מתייחסת לסידור ולא לאם לעובד מותר לעבוד בשבתון ולכן לא מעניין מצב זו הסידור מותר בשבתון ולעובד אין מאפיין 7. השגיאה לא רלוונטית לערב שבת/חג.
               //לא להתייחס למאפיין 7
                if (oSidur.bSidurMyuhad) //וסידור מיוחד
                {   //אם שבתון וקיים מאפיין 9, כלומר סידור אזור בשבתון, אז נעלה שגיאה
                    if (clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim,iSugYom,oSidur.dSidurDate) && oSidur.bSidurNotInShabtonKodExists )
                    {//היום הוא יום שבתון ולסידור יש מאפיין אסור בשבתון, לכן נעלה שגיאה
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "סדור אסור בשבתון ", enErrors.errSidurNotAllowedInShabaton.GetHashCode());
                        dtErrors.Rows.Add(drNew);
                        isValid = false;
                    }
                }               
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errSidurNotAllowedInShabaton.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurValidInShabaton50: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsPitzulSidurInShabatValid24(DateTime dCardDate, int iSidur,  ref clSidur oSidur, ref DataTable dtErrors)
        {
            bool isValid = true;

            //בדיקה ברמת סידור           
            //אם יש סימון של פיצול ביום העבודה והיום הוא ערב שבת/חג ושעת התחלה של החלק השני של הפיצול גדול משעת כניסת שבת - זוהי שגיאה. יש לבדוק אם אנחנו בתקופת קיץ/חורף לפי פרמטרים 25 (תחילת זמן קיץ אגד) ו- 26 (סיום זמן קיץ אגד). לפי התקופה יש לבדוק את שעת כניסת השבת לפי פרמטרים 6 (כניסת שבת חורף) ו- 7 (כניסת שבת קיץ). אם עבור יום מסוים חוזר מהאם ביום שהוא ערב שבת/חג יש סידור אחד שמסתיים לפני כניסת שבת ויש לו סימון בשדה פיצול והסידור העוקב אחריו התחיל אחרי כניסת השבת  - זו שגיאה. (מצב תקין הוא שהסידור העוקב התחיל לפני כניסת שבת וגלש/לא גלש לשבת). יש לבדוק אם אנחנו בתקופת קיץ/חורף לפי פרמטרים 48 (תחילת זמן קיץ אגד) ו- 49 (סיום זמן קיץ אגד). לפי התקופה יש לבדוק את שעת כניסת השבת לפי פרמטרים 6 (כניסת שבת חורף) ו- 7 (כניסת שבת קיץ). אם עבור יום מסוים חוזר מה- Oracle שזה שבת ובטבלת ימים מיוחדים חוזר משהו אחר (לא שבתון) אז מה שחוזר מה- Oracle הוא "חזק" יותר. השגיאה לא רלוונטית לערב חג שנופל ביום שבת משום כי השבתון גובר על ערב שבתון.
            DateTime dSidurPrevShatGmar;
            DateTime dSidurShatHatchala;
            clSidur oPrevSidur = (clSidur)htEmployeeDetails[iSidur-1];
            string sSidurPrevShatGmar = oPrevSidur.sShatGmar;
            int iSidurPrevPitzulHafsaka = string.IsNullOrEmpty(oPrevSidur.sPitzulHafsaka) ? 0 : int.Parse(oPrevSidur.sPitzulHafsaka);

            try
            {//אם הגענו לרוטינה, סימן שצויין פיצול בסידור הקודם
                if (!(string.IsNullOrEmpty(oSidur.sShatHatchala)))
                {
                    //אם  יום שישי או ערב חג אבל  לא בשבתון
                    if ((((oSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()) || ((oSidur.sErevShishiChag == "1") && (oSidur.sSidurDay != clGeneral.enDay.Shabat.GetHashCode().ToString())))) && (iSidurPrevPitzulHafsaka > 0))
                    {
                        //נקרא את שעת כניסת השבת                   
                        //אם ביום שהוא ערב שבת/חג יש סידור אחד שמסתיים לפני כניסת שבת ויש לו סימון בשדה פיצול והסידור העוקב אחריו התחיל אחרי כניסת השבת  - זו שגיאה. (מצב תקין הוא שהסידור העוקב התחיל לפני כניסת שבת וגלש/לא גלש לשבת). 
                        //if (((int.Parse(sSidurPrevShatGmar.Remove(2, 1)) > iShabatStart)) || (int.Parse(oSidur.sShatHatchala.Remove(2, 1)) <= iShabatStart))
                        dSidurPrevShatGmar =clGeneral.GetDateTimeFromStringHour(sSidurPrevShatGmar,dCardDate);
                        dSidurShatHatchala = clGeneral.GetDateTimeFromStringHour(oSidur.sShatHatchala,dCardDate);
                        if ((dSidurPrevShatGmar <= oParam.dKnisatShabat) && (dSidurShatHatchala > oParam.dKnisatShabat))
                        {
                            //נציג את הסידור השני כשגוי
                            drNew = dtErrors.NewRow();
                            InsertErrorRow(oSidur, ref drNew, " עבודה בערב שבת/חג לאחר פצול", enErrors.errPitzulSidurInShabat.GetHashCode());
                            dtErrors.Rows.Add(drNew);
                            isValid = false;
                        }
                        if ((dSidurPrevShatGmar > oParam.dKnisatShabat) && (dSidurShatHatchala > oParam.dKnisatShabat))
                        {
                            //נציג את שני הסידורים כשגויים
                            drNew = dtErrors.NewRow();
                            InsertErrorRow(oPrevSidur, ref drNew, " עבודה בערב שבת/חג לאחר פצול", enErrors.errPitzulSidurInShabat.GetHashCode());
                            dtErrors.Rows.Add(drNew);
                            drNew = dtErrors.NewRow();
                            InsertErrorRow(oSidur, ref drNew, " עבודה בערב שבת/חג לאחר פצול", enErrors.errPitzulSidurInShabat.GetHashCode());
                            dtErrors.Rows.Add(drNew);

                            isValid = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errPitzulSidurInShabat.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsPitzulSidurInShabatValid24: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsSidurExists9(ref clSidur oSidur, ref DataTable dtErrors)
        {
            bool isValid = true;

            //בדיקה ברמת סידור 
            try
            {//מספר הסידור לא נמצא במפה (טבלת סידורים) לתאריך הנדרש או בטבלת סידורים מיוחדים. 
                //if (oSidur.bSidurMyuhad)
                //{
                if ((oSidur.bSidurMyuhad && oSidur.iMisparSidurMyuhad == 0) ||  oSidur.iMisparSidur.ToString().Length < 4)
                    {
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "סידור לא קיים", enErrors.errSidurNotExists.GetHashCode());
                        dtErrors.Rows.Add(drNew);
                        isValid = false;
                    }
                //}
                //else
                //{
                //    if (!oSidur.bSidurRagilExists)
                //    {
                //        drNew = dtErrors.NewRow();
                //        InsertErrorRow(oSidur, ref drNew, "סידור לא קיים", enErrors.errSidurNotExists.GetHashCode());
                //        dtErrors.Rows.Add(drNew);
                //        isValid = false;
                //    }
                //}

            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errSidurNotExists.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurExists9: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsZakaiLeNesiot26(int iMisparIshi, DateTime dCardDate, ref DataTable dtErrors)
        {
            bool isValid = true;

            //בדיקה ברמת עובד
            try
            {
                //אם יש ערך תקין בשדה ביטול נסיעות ולעובד אין מאפיין 51 (זמן נסיעה קבוע) או 61 (זמן נסיעה משתנה) - זו שגיאה.                
                if ((!(string.IsNullOrEmpty(oOvedYomAvodaDetails.sBitulZmanNesiot))) && Int32.Parse(oOvedYomAvodaDetails.sBitulZmanNesiot) > 0 && (!oMeafyeneyOved.Meafyen51Exists) && (!oMeafyeneyOved.Meafyen61Exists))
                {
                    drNew = dtErrors.NewRow();
                    drNew["check_num"] = enErrors.errLoZakaiLeNesiot.GetHashCode();
                    drNew["mispar_ishi"] =iMisparIshi;
                    drNew["taarich"] = dCardDate.ToShortDateString();
                    //drNew["error_desc"] = "לא זכאי לנסיעות";                    
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, iMisparIshi, "E", enErrors.errLoZakaiLeNesiot.GetHashCode(), dCardDate, "IsZakaiLeNesiot26: " + ex.Message);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsOvodaInMachalaAllowed132(DateTime dCardDate, int iMisparIshi,ref DataTable dtErrors)
        {
            bool isValid = true;
            int iCountSidurim;
            //בדיקה ברמת עובד
            try
            {
                if (dTarTchilatMatzav !=dCardDate)
                {
                    iCountSidurim = htEmployeeDetails.Values.Cast<clSidur>().ToList().Count(Sidur => Sidur.iNitanLedaveachBemachalaAruca == 0 && Sidur.iLoLetashlum==0);
                    //עובד לא יכול לעבוד בכל עבודה באגד אם במחלה. מחלה זה סטטוס ב- HR. מזהים שהעובד עבד ביום מסוים אם יש לו לפחות סידור אחד ביום זה.  
                    if ((IsOvedMatzavExists("5")) && (iCountSidurim > 0))
                    {
                        drNew = dtErrors.NewRow();
                        drNew["check_num"] = enErrors.errOvdaInMachalaNotAllowed.GetHashCode();
                        drNew["mispar_ishi"] = iMisparIshi;
                        drNew["taarich"] = dCardDate.ToShortDateString();
                        //drNew["error_desc"] = "עבודה אסורה במחלה ארוכה";
                        dtErrors.Rows.Add(drNew);
                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, iMisparIshi, "E", enErrors.errOvdaInMachalaNotAllowed.GetHashCode(), dCardDate, "IsOvodaInMachalaAllowed132: " + ex.Message);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsSidurTafkidValid145(ref clSidur oSidur, ref DataTable dtErrors)
        {
            bool isValid = true;

            if (oSidur.iMisparSidur == 99501) return isValid;

            //בדיקה ברמת סידור
            try
            {//אסור לדווח סידור תפקיד אם אין לעובד מאפייני שעת התחלה / גמר (לפי מאפיינים 3/4 במאפייני עובדים). רלוונטי רק לסידורים מיוחדים, לא לרגילים.
                if ((oSidur.bSidurMyuhad) && (oSidur.sSectorAvoda == clGeneral.enSectorAvoda.Tafkid.GetHashCode().ToString()) && ((!oMeafyeneyOved.Meafyen3Exists) || (!oMeafyeneyOved.Meafyen4Exists)))
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "סידור תפקיד ללא מאפיין התחלה/גמר", enErrors.errSidurTafkidWithOutApprove.GetHashCode());
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errSidurTafkidWithOutApprove.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurTafkidValid145: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsDriverLessonsNumberValid136(DataRow[] drSugSidur, ref clSidur oSidur, ref DataTable dtErrors)
        {//בדיקה ברמת סידור
            bool isValid = true;
            int iCountShiureyNehiga = 0;
            try
            {
                if (!oSidur.bSidurMyuhad)
                {//סידורים רגילים
                 if (drSugSidur.Length > 0)
                    {   //עבור סידורים רגילים, נבדוק במאפייני סידורים אם סוג סידור נהגות.
                        if ((drSugSidur[0]["sug_avoda"].ToString() == clGeneral.enSugAvoda.Nahagut.GetHashCode().ToString()))
                        {
                            iCountShiureyNehiga = oSidur.htPeilut.Values.Cast<clPeilut>().ToList().Count(Peilut => Peilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "842" && Peilut.lMakatNesia.ToString().PadLeft(8).Substring(5, 3) == "044");

                            if (iCountShiureyNehiga==0)
                            {
                                isValid = false;
                            }
                        }
                    }
                }

                if (!isValid)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "שיעור נהיגה לא בסידור הוראת נהיגה", enErrors.errDriverLessonsNumberNotValid.GetHashCode());
                    dtErrors.Rows.Add(drNew);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errDriverLessonsNumberNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsDriverLessonsNumberValid136: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsSidurAvodaValidForTaarich160(ref clSidur oSidur, ref DataTable dtErrors)
        {//בדיקה ברמת סידור
            bool bError = false;
            DateTime dSidurDate;
            bool isValid = true;

            try
            {//קיימים סידורי עבודה מיוחדים הפעילים רק מספר חודשים בשנה - סידורי קייטנות וארועי קייץ, סידורים אלו מזוהים לפי קיום ערך עבור מאפיין 73 (סוג עבודה בארועי קיץ) בטבלת מאפייני סידורים מיוחדים.התקופה החוקית לסידורים אלה נבדקת אל מול פרמטרים בטבלת פרמטרים חיצוניים (44-47).  (44 - אירועי קיץ - תאריך חוקי לתחילת סידורי ניהול ושיווק, 45 - אירועי קיץ - תאריך חוקי לתחילת סידור תפעול , 46 - אירועי קיץ - תאריך חוקי לסיום סידור שיווק, 47 - אירועי קיץ - תאריך חוקי לסיום סידור). השגיאה רלוונטית רק לסידורים מיוחדים.
                if (oSidur.bSidurMyuhad)
                {//קיים מאפיין 73 
                    if (oSidur.bSidurInSummerExists)
                    {
                        dSidurDate = oSidur.dSidurDate;
                        if ((oSidur.sSidurInSummer == "1") || (oSidur.sSidurInSummer == "2"))
                        {
                            if (oParam.dStartNihulVShivik.Year!=clGeneral.cYearNull)
                            {
                                bError = (dSidurDate < oParam.dStartNihulVShivik);
                            }

                            if (!bError)
                            {
                                if (oParam.dEndNihulVShivik.Year != clGeneral.cYearNull)
                                {
                                    bError = (dSidurDate > oParam.dEndNihulVShivik);
                                }
                            }
                        }
                        else
                        {
                            bError = (((dSidurDate < oParam.dStartTiful) && (oParam.dStartTiful.Year!=clGeneral.cYearNull)) || ((dSidurDate > oParam.dEndTiful) && (oParam.dEndTiful.Year!=clGeneral.cYearNull)));
                        }
                        if (bError)
                        {
                            drNew = dtErrors.NewRow();
                            InsertErrorRow(oSidur, ref drNew, "סידור עבודה לא חוקי לחודש זה", enErrors.errSidurAvodaNotValidForMonth.GetHashCode());
                            dtErrors.Rows.Add(drNew);
                            isValid = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errSidurAvodaNotValidForMonth.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurAvodaValidForTaarich160: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsCurrentSidurInPrevSidur168(int iSidur,ref clSidur oSidur, ref clSidur oPrevSidur, ref DataTable dtErrors)
        {//בדיקה ברמת סידור
            
            bool isError = false;
            bool isValid = true;
            int i;
            DateTime dShatHatchalaSidur = oSidur.dFullShatHatchala;
            DateTime dShatGmarSidur = oSidur.dFullShatGmar;
            DateTime dShatHatchalaPrevSidur = oPrevSidur.dFullShatHatchala;
            DateTime dShatGmarPrevSidur = oPrevSidur.dFullShatGmar;
            try
            {
                for (i = (iSidur - 1); i >= 0; i--)
                {
                    oPrevSidur = (clSidur)htEmployeeDetails[i];
                     dShatHatchalaPrevSidur = oPrevSidur.dFullShatHatchala;
                     dShatGmarPrevSidur = oPrevSidur.dFullShatGmar;
                    if (oPrevSidur.iLoLetashlum == 0)
                    {
                        break;
                    }
                }

                if (dShatHatchalaSidur.Date == DateTime.MinValue.Date)
                {
                    dShatHatchalaSidur = oSidur.dFullShatGmar;
                }
                if (dShatGmarSidur.Date == DateTime.MinValue.Date)
                {
                    dShatGmarSidur = oSidur.dFullShatHatchala;
                }
                if (dShatHatchalaPrevSidur.Date == DateTime.MinValue.Date)
                {
                    dShatHatchalaPrevSidur = oPrevSidur.dFullShatGmar;
                }
                if (dShatGmarPrevSidur.Date == DateTime.MinValue.Date)
                {
                    dShatGmarPrevSidur = oPrevSidur.dFullShatHatchala;
                }

               

                if ((oSidur.iLoLetashlum == 0 || (oSidur.iLoLetashlum == 1 && oSidur.iKodSibaLoLetashlum == 1)) && (oPrevSidur.iLoLetashlum==0  || (oPrevSidur.iLoLetashlum == 1 && oPrevSidur.iKodSibaLoLetashlum == 1)))
                {
                 
                    if (dShatHatchalaSidur > dShatHatchalaPrevSidur &&
                        dShatGmarSidur < dShatGmarPrevSidur)
                    {
                        isError = true;
                    }
                }
                
                if (isError)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "סידור נבלעת בתוך סידור קודם ", enErrors.errCurrentSidurInPrevSidur.GetHashCode());
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errCurrentSidurInPrevSidur.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsCurrentSidurInPrevSidur168: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsCurrentPeilutInPrevPeilut162(ref clSidur oSidur, ref clPeilut oPeilut, ref clPeilut oPrevPeilut, ref DataTable dtErrors)
        {//בדיקה ברמת פעילות
            DateTime dCurrEndPeilut = new DateTime();
            DateTime dCurrStartPeilut = new DateTime();
            DateTime dPrevEndPeilut = new DateTime();
            DateTime dPrevStartPeilut = new DateTime();
            byte bCheck = 0;
            double dblCurrTimeInMinutes = 0;
            double dblPrevTimeInMinutes = 0;
            bool isValid = true;

            try
            {
                //זמן תחילת פעילות לאחר זמן תחילת הפעילות הקודמת לה וזמן סיום הפעילות קודם לזמן סיום הפעילות הקודמת לה. זיהוי זמן הפעילות (זיהוי סוג פעילות לפי רוטינת זיהוי מק"ט) :עבור קו שירות, נמ"ק, , יש לפנות לקטלוג נסיעות כדי לקבל את הזמן. עבור אלמנט, במידה וזה אלמנט זמן (לפי ערך 1 במאפיין 4 בטבלת מאפייני אלמנטים), הזמן נלקח מפוזיציות 4-6 של האלמנט. בבדיקה זו אין  להתייחס לפעילות המתנה (מזהים פעילות המתנה (מסוג אלמנט) לפי מאפיין 15 בטבלת מאפייני אלמנטים).           
                if ((oPeilut.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode()) || (oPeilut.iMakatType == clKavim.enMakatType.mNamak.GetHashCode()))
                {
                    dCurrStartPeilut = oPeilut.dFullShatYetzia;
                    if (oPeilut.iDakotBafoal>0)
                        dblCurrTimeInMinutes = oPeilut.iDakotBafoal;
                    else dblCurrTimeInMinutes = oPeilut.iMazanTichnun;
                    bCheck = 1;
                }
                //else
                //{
                //    dCurrStartPeilut = oPeilut.dFullShatYetzia;
                //    if (oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode())
                //    {
                //        //אם אלמנט זמן
                //        //אבל לא אלמנט זמן מסוג המתנה
                //        if (!oPeilut.bElementIgnoreHafifaBetweenNesiotExists)
                //        {
                //            dblCurrTimeInMinutes = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));
                //            bCheck = 1;
                //        }
                //        else
                //        {
                //            bCheck &= 0;
                //        }
                //    }
                //    else
                //    {
                //        bCheck &= 0;
                //    }
                //}

                //זמן תחילת פעילות לאחר זמן תחילת הפעילות הקודמת לה וזמן סיום הפעילות קודם לזמן סיום הפעילות הקודמת לה. זיהוי זמן הפעילות (זיהוי סוג פעילות לפי רוטינת זיהוי מק"ט) :עבור קו שירות, נמ"ק, , יש לפנות לקטלוג נסיעות כדי לקבל את הזמן. עבור אלמנט, במידה וזה אלמנט זמן (לפי ערך 1 במאפיין 4 בטבלת מאפייני אלמנטים), הזמן נלקח מפוזיציות 4-6 של האלמנט. בבדיקה זו אין  להתייחס לפעילות המתנה (מזהים פעילות המתנה (מסוג אלמנט) לפי מאפיין 15 בטבלת מאפייני אלמנטים).           
                if ((oPrevPeilut.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode()) || (oPrevPeilut.iMakatType == clKavim.enMakatType.mNamak.GetHashCode()))
                {
                    dPrevStartPeilut = oPrevPeilut.dFullShatYetzia;
                    if (oPrevPeilut.iDakotBafoal > 0)
                        dblPrevTimeInMinutes = oPrevPeilut.iDakotBafoal;
                    else dblPrevTimeInMinutes = oPrevPeilut.iMazanTichnun;
                   // dblPrevTimeInMinutes = oPrevPeilut.iMazanTichnun;
                    bCheck &= 1;
                }
                //else
                //{
                //    dPrevStartPeilut = oPrevPeilut.dFullShatYetzia;
                //    if (oPrevPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode())
                //    {
                //        //אם אלמנט זמן
                //        //אבל לא אלמנט זמן מסוג המתנה
                //        if (!oPeilut.bElementIgnoreHafifaBetweenNesiotExists)
                //        {
                //            dblPrevTimeInMinutes = int.Parse(oPrevPeilut.lMakatNesia.ToString().Substring(3, 3));
                //            bCheck &= 1;
                //        }
                //        else
                //        {
                //            bCheck &= 0;
                //        }
                //    }
                //    else
                //    {
                //        bCheck &= 0;
                //    }
                //}

                if (bCheck == 1)
                {
                    dCurrEndPeilut = dCurrStartPeilut.AddMinutes(dblCurrTimeInMinutes);
                    dPrevEndPeilut = dPrevStartPeilut.AddMinutes(dblPrevTimeInMinutes);
                    if ((dCurrStartPeilut >= dPrevStartPeilut) && (dCurrEndPeilut < dPrevEndPeilut))
                    {
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "פעילות נבלעת בתוך פעילות קודמת ", enErrors.errCurrentPeilutInPrevPeilut.GetHashCode());
                        InsertPeilutErrorRow(oPeilut, ref drNew);
                        dtErrors.Rows.Add(drNew);
                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errCurrentPeilutInPrevPeilut.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.dFullShatYetzia, oPeilut.iMisparKnisa, "IsCurrentPeilutInPrevPeilut162: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsHashlamaLeYomAvodaValid163(int iMisparIshi, DateTime dCardDate,  ref DataTable dtErrors)
        {//בדיקה ברמת יום עבודה
            bool isValid = true;

            try
            {
                //השלמה ליום עבודה אסורה ביום שבתון (שבתון יכול להיות יום שבת (חוזר מה- Oracle) או שבטבלת סוגי ימים מיוחדים הוא מוגדר כשבתון) לעובד ללא מאפיין 07 (שעת התחלה מותרת בשבתון) או ביום שישי (אם חוזר 6 מה- Oracle) לעובד ללא מאפיין 05.
                if (!string.IsNullOrEmpty(oOvedYomAvodaDetails.sHashlamaLeyom))
                {    
                    if ((((oOvedYomAvodaDetails.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()) || (oOvedYomAvodaDetails.sErevShishiChag=="1"))  && (!oMeafyeneyOved.Meafyen5Exists))
                        || (clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim,iSugYom,dCardDate) && (!oMeafyeneyOved.Meafyen7Exists)))
                    {                       
                        drNew = dtErrors.NewRow();
                        drNew["check_num"] = enErrors.errHashlamaLeYomAvodaNotAllowed.GetHashCode();
                        drNew["mispar_ishi"] = iMisparIshi;
                        drNew["taarich"] = dCardDate.ToShortDateString();
                        //drNew["error_desc"] = "השלמה ליום עבודה אסורה";                   
                    
                        //InsertErrorRow(oSidur, ref drNew, "השלמה ליום עבודה אסורה ", enErrors.errHashlamaLeYomAvodaNotAllowed.GetHashCode());                        
                        dtErrors.Rows.Add(drNew);
                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, iMisparIshi, "E", enErrors.errHashlamaLeYomAvodaNotAllowed.GetHashCode(), dCardDate, "IsHashlamaLeYomAvodaValid163: " + ex.Message);
                isValid = false;
                _bSuccsess = false;
            }


            return isValid;
        }

        private void IsDuplicateTravel151(ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors)
        {
            DataTable dtDuplicate=new DataTable();
            try {
                //if (!CheckApproval("2511,25", oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia))
                //{
                    if (oPeilut.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode())
                    {
                        if (IsDuplicateTravle(_iMisparIshi,_dCardDate,oPeilut.lMakatNesia,oPeilut.dFullShatYetzia,oPeilut.iMisparKnisa,ref dtDuplicate))
                        {
                            drNew = dtErrors.NewRow();
                            InsertErrorRow(oSidur, ref drNew, "נסיעה כפולה בין עובדים שונים", enErrors.errDuplicateTravle.GetHashCode());
                            InsertPeilutErrorRow(oPeilut, ref drNew);
                            dtErrors.Rows.Add(drNew);

                            for (int i = 0; i < dtDuplicate.Rows.Count; i++)
                            {
                                //if (!CheckApprovalToEmploee((int)dtDuplicate.Rows[i]["mispar_ishi"],(DateTime)dtDuplicate.Rows[i]["taarich"],"25", oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia))
                                      clDefinitions.UpdateCardStatus((int)dtDuplicate.Rows[i]["mispar_ishi"], (DateTime)dtDuplicate.Rows[i]["taarich"], clGeneral.enCardStatus.Error, _iUserId);
                                
                            }
                        }
                    }
                //}
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errDuplicateTravle.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsDuplicateTravel151: " + ex.Message, null);
                _bSuccsess = false;
            }
        }

        private void IsChafifaBesidurNihulTnua152(DataRow[] drSugSidur, clSidur oSidur, ref DataTable dtErrors)
        {
            DataTable dtChafifa = new DataTable();
            try
            {
                //if (!CheckApproval("2711,27", oSidur.iMisparSidur, oSidur.dFullShatHatchala))
                //{
                    if (CheckSidurNihulTnua(oSidur, drSugSidur))
                    {
                        if (IsSidurChofef(_iMisparIshi, _dCardDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, oSidur.dFullShatGmar, _oParameters.iMaxChafifaBeinSidureyNihulTnua, ref dtChafifa))
                        {
                            drNew = dtErrors.NewRow();
                            InsertErrorRow(oSidur, ref drNew, "חפיפה בסידור ניהול תנועה", enErrors.errChafifaBesidurNihulTnua.GetHashCode());
                            dtErrors.Rows.Add(drNew);

                            for (int i = 0; i < dtChafifa.Rows.Count; i++)
                            {
                                //if (!CheckApprovalToEmploee((int)dtChafifa.Rows[i]["mispar_ishi"], (DateTime)dtChafifa.Rows[i]["taarich"], "27", oSidur.iMisparSidur, oSidur.dFullShatHatchala))
                                     clDefinitions.UpdateCardStatus((int)dtChafifa.Rows[i]["mispar_ishi"], (DateTime)dtChafifa.Rows[i]["taarich"], clGeneral.enCardStatus.Error, _iUserId);
                            }
                        }
                    }
                //}
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errChafifaBesidurNihulTnua.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsChafifaBesidurNihulTnua152: " + ex.Message, null);
                _bSuccsess = false;
            }
        }

        private bool CheckSidurNihulTnua(clSidur oSidur,DataRow[] drSugSidur)
        {
            try
            {
                bool bSidurNihulTnua = false;
                //                 סידורים מסוג ניהול תנועה:
                //ניהול תנועה - (ערך 4 במאפיין 3 מאפייני סוג סידור)
                //לרשות (ערך 6 במאפיין 52 במאפייני סוג סידור)
                //קופאי (ערך 7  במאפיין 52 במאפייני סוג סידור)
                //כוננות גרירה (ערך 8 במאפיין 52 במאפייני סוג סידור)
                if (oSidur.bSidurMyuhad)
                {//סידור מיוחד
                    bSidurNihulTnua = (oSidur.sSectorAvoda == clGeneral.enSectorAvoda.Nihul.GetHashCode().ToString());
                    if (!bSidurNihulTnua)
                    {
                        bSidurNihulTnua = (oSidur.sSugAvoda == clGeneral.enSugAvoda.Lershut.GetHashCode().ToString());
                    }
                    if (!bSidurNihulTnua)
                    {
                        bSidurNihulTnua = (oSidur.sSugAvoda == clGeneral.enSugAvoda.Kupai.GetHashCode().ToString());
                    }
                    if (!bSidurNihulTnua)
                    {
                        bSidurNihulTnua = (oSidur.sSugAvoda == clGeneral.enSugAvoda.Grira.GetHashCode().ToString());
                    }
                }
                else
                {//סידור רגיל
                    if (drSugSidur.Length > 0)
                    {
                        bSidurNihulTnua = (drSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nihul.GetHashCode().ToString());
                        if (!bSidurNihulTnua)
                        {
                            bSidurNihulTnua = (drSugSidur[0]["sug_avoda"].ToString() == clGeneral.enSugAvoda.Lershut.GetHashCode().ToString());
                        }
                        if (!bSidurNihulTnua)
                        {
                            bSidurNihulTnua = (drSugSidur[0]["sug_avoda"].ToString() == clGeneral.enSugAvoda.Kupai.GetHashCode().ToString());
                        }
                        if (!bSidurNihulTnua)
                        {
                            bSidurNihulTnua = (drSugSidur[0]["sug_avoda"].ToString() == clGeneral.enSugAvoda.Grira.GetHashCode().ToString());
                        }
                     }
                }  

                return bSidurNihulTnua;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool IsSidurSummerValid164(ref clSidur oSidur, ref DataTable dtErrors)
        {
            bool isValid = true;

            //בדיקה ברמת סידור
            try
            {
                if (oSidur.bSidurMyuhad)
                {
                    if (oSidur.bSidurInSummerExists && (oSidur.sSidurInSummer != "1" && oSidur.sSidurInSummer != "2" && oSidur.sSidurInSummer != "3" && oSidur.sSidurInSummer != "4"))
                    {
                        //סידור של ארועי קיץ חייב להיות לעובד אשר הוגדר עובד 6 ימים (מזהים לפי ערך 61, 62) במאפיין 56 במאפייני עובדים. סידור של ארועי קיץ = סידור מיוחד עם מאפיין 73
                        if (((!oMeafyeneyOved.Meafyen56Exists)) || ((oMeafyeneyOved.Meafyen56Exists) && (oMeafyeneyOved.iMeafyen56 != clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode()) && (oMeafyeneyOved.iMeafyen56 != clGeneral.enMeafyenOved56.enOved6DaysInWeek2.GetHashCode())))
                        {
                            drNew = dtErrors.NewRow();
                            InsertErrorRow(oSidur, ref drNew, "סידור של ארועי קיץ לעובד 5 ימים", enErrors.errSidurSummerNotValidForOved.GetHashCode());
                            dtErrors.Rows.Add(drNew);
                            isValid = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errSidurSummerNotValidForOved.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurSummerValid164: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsSidurNAhagutValid161(DataRow[] drSugSidur,ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור
            bool bError = false;
            bool isValid = true;

            try
            {
                //לעובד אסור לבצע סידור נהיגה (עבור סידורים מיוחדים, מזהים סידור נהגות לפי ערך 5 במאפיין  3 בטבלת סידורים מיוחדים. עבור סידורים רגילים מזהים סידור נהגות לפי ערך 5 ב מאפיין 3 בטבלת מאפייני סידורים) במקרים הבאים: א. לעובד אין רישיון נהיגה באוטובוס (יודעים אם לעובד יש רישיון לפי ערכים 6, 10, 11 בקוד נתון 7 (קוד רישיון אוטובוס) בטבלת פרטי עובדים) ב. עובד הוא מותאם שאסור לו לנהוג (יודעים שעובד הוא מותאם שאסור לו לנהוג לפי ערכים 4, 5 בקוד נתון 8 (קוד עובד מותאם) בטבלת פרטי עובדים) ג. עובד הוא מותאם שמותר לו לבצע רק נסיעה ריקה (יודעים שעובד הוא מותאם שמותר לו לבצע רק נסיעה ריקה לפי ערכים 6, 7 בקוד נתון 8 (קוד עובד מותאם) בטבלת פרטי עובדים) במקרה זה יש לבדוק אם הסידור מכיל רק נסיעות ריקות, מפעילים את הרוטינה לזיהוי מקט ד. עובד הוא בשלילה (יודעים שעובד הוא בשלילה לפי ערך 1 בקוד בנתון 21 (שלילת   רשיון) בטבלת פרטי עובדים) 
                if (oSidur.bSidurMyuhad)
                {
                    if (oSidur.sSugAvoda != clGeneral.enSugAvoda.ActualGrira.GetHashCode().ToString())
                    {
                        if (oSidur.sSectorAvoda == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString())
                        {
                            bError = CheckConditionsAllowSidur(ref oSidur);
                        }
                    }
                }
                else
                {//סידור רגיל
                    if (drSugSidur.Length > 0)
                    {
                         if ( drSugSidur[0]["sug_Avoda"].ToString() != clGeneral.enSugAvoda.Grira.GetHashCode().ToString() && drSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString())
                        {
                            bError = CheckConditionsAllowSidur(ref oSidur);
                        }
                    }
                }
                if (bError)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "לעובד אסור לבצע סידור נהיגה", enErrors.errOvedNotAllowedToDoSidurNahagut.GetHashCode());
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errOvedNotAllowedToDoSidurNahagut.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurNAhagutValid161: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsFirstDayShlilatRishayon195(DataRow[] drSugSidur,ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור
            bool bError = false;
            bool isValid = true;

            try
            {
                //ד. עובד הוא בשלילה (יודעים שעובד הוא בשלילה לפי ערך 1 בקוד בנתון 21 (שלילת   רשיון) בטבלת פרטי עובדים) 
                if (oSidur.bSidurMyuhad)
                {
                    if (oSidur.sSugAvoda != clGeneral.enSugAvoda.ActualGrira.GetHashCode().ToString())
                    {
                        if (oSidur.sSectorAvoda == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString())
                        {
                            if (_dCardDate == oOvedYomAvodaDetails.dTaarichMe)
                                bError = IsOvedBShlila();
                        }
                    }
                }
                else
                {//סידור רגיל
                    if (drSugSidur.Length > 0)
                    {
                        if (drSugSidur[0]["sug_Avoda"].ToString() != clGeneral.enSugAvoda.Grira.GetHashCode().ToString() && drSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString())
                        {
                            if (_dCardDate == oOvedYomAvodaDetails.dTaarichMe)
                                bError = IsOvedBShlila();
                        }
                    }
                }
                

                if (bError)
                {
                    drNew = dtErrors.NewRow();
                    drNew["check_num"] = enErrors.errFirstDayShlilatRishayon195.GetHashCode();
                    drNew["mispar_ishi"] = oSidur.iMisparIshi;
                    drNew["taarich"] =oSidur.dSidurDate.ToShortDateString();
                    //drNew["error_desc"] = "סידור עם נסיעת אילת ארוכה וסידור ויזה באותו יום";
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errFirstDayShlilatRishayon195.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurNAhagutValid161: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsSidurKupaiLeloHachtama196(int iSidur,DataRow[] drSugSidur,ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור
            bool bError = false;
            bool isValid = true;
            clSidur ESidur;
            try
            {
                if (oOvedYomAvodaDetails.iKodHevra != clGeneral.enEmployeeType.enEggedTaavora.GetHashCode() )
                {
                    if (oSidur.sSugAvoda == clGeneral.enSugAvoda.Kupai.GetHashCode().ToString() || (drSugSidur.Length > 0 && drSugSidur[0]["sug_avoda"].ToString() == clGeneral.enSugAvoda.Kupai.GetHashCode().ToString()))
                    {
                        bError = true;
                        for (int i = 0; i < htEmployeeDetails.Count; i++)
                        {
                            if (i != iSidur)
                            {
                                ESidur = (clSidur)htEmployeeDetails[i];
                                if (ESidur.sSugAvoda == clGeneral.enSugAvoda.Shaon.GetHashCode().ToString())
                                {
                                    if ((Math.Abs(oSidur.dFullShatHatchala.Subtract(ESidur.dFullShatHatchala).TotalMinutes) <= 60) &&
                                        (Math.Abs(ESidur.dFullShatGmar.Subtract(oSidur.dFullShatGmar).TotalMinutes) <= 60))
                                        bError = false;
                                }
                            }
                        }
                    }

                    if (bError)
                    {
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "", enErrors.errkupaiLeloHachtama.GetHashCode());
                        dtErrors.Rows.Add(drNew);
                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errFirstDayShlilatRishayon195.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurNAhagutValid161: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }
        private bool IsSidurGriraValid177(DataRow[] drSugSidur, ref clSidur oSidur, ref DataTable dtErrors)
        {
            //בדיקה ברמת סידור
            bool bError = false;
            bool isValid = true;

            try
            {
                //לעובד אסור לבצע סידור נהיגה (עבור סידורים מיוחדים, מזהים סידור נהגות לפי ערך 5 במאפיין  3 בטבלת סידורים מיוחדים. עבור סידורים רגילים מזהים סידור נהגות לפי ערך 5 ב מאפיין 3 בטבלת מאפייני סידורים) במקרים הבאים: א. לעובד אין רישיון נהיגה באוטובוס (יודעים אם לעובד יש רישיון לפי ערכים 6, 10, 11 בקוד נתון 7 (קוד רישיון אוטובוס) בטבלת פרטי עובדים) ב. עובד הוא מותאם שאסור לו לנהוג (יודעים שעובד הוא מותאם שאסור לו לנהוג לפי ערכים 4, 5 בקוד נתון 8 (קוד עובד מותאם) בטבלת פרטי עובדים) ג. עובד הוא מותאם שמותר לו לבצע רק נסיעה ריקה (יודעים שעובד הוא מותאם שמותר לו לבצע רק נסיעה ריקה לפי ערכים 6, 7 בקוד נתון 8 (קוד עובד מותאם) בטבלת פרטי עובדים) במקרה זה יש לבדוק אם הסידור מכיל רק נסיעות ריקות, מפעילים את הרוטינה לזיהוי מקט ד. עובד הוא בשלילה (יודעים שעובד הוא בשלילה לפי ערך 1 בקוד בנתון 21 (שלילת   רשיון) בטבלת פרטי עובדים) 
                if (oSidur.bSidurMyuhad)
                {
                    if (oSidur.sSugAvoda == clGeneral.enSugAvoda.ActualGrira.GetHashCode().ToString())
                    {

                        bError = CheckConditionsAllowSidur(ref oSidur);
                    }
                }
                else
                {//סידור רגיל
                    if (drSugSidur.Length > 0)
                    {
                        if (drSugSidur[0]["sug_Avoda"].ToString() == clGeneral.enSugAvoda.Grira.GetHashCode().ToString() )
                        {
                            bError = CheckConditionsAllowSidur(ref oSidur);
                        }
                    }
                } 
            
                if (bError)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "לעובד אסור לבצע סידור גרירה בפועל", enErrors.errSidurGriraNotValid.GetHashCode());
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errSidurGriraNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurGriraValid177: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool CheckConditionsAllowSidur(ref clSidur oSidur)
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


        private bool IsOvedMutaamForEmptyPeilut(ref clSidur oSidur)
        {
            //. עובד הוא מותאם שמותר לו לבצע רק נסיעה ריקה (יודעים שעובד הוא מותאם שמותר לו לבצע רק נסיעה ריקה לפי ערכים 6, 7 בקוד נתון 8 (קוד עובד מותאם) בטבלת פרטי עובדים) במקרה זה יש לבדוק אם הסידור מכיל רק נסיעות ריקות, מפעילים את הרוטינה לזיהוי מקט
            return ((oOvedYomAvodaDetails.sMutamut == clGeneral.enMutaam.enMutaam6.GetHashCode().ToString() ||
                   oOvedYomAvodaDetails.sMutamut == clGeneral.enMutaam.enMutaam7.GetHashCode().ToString())
                   && (oSidur.bSidurNotEmpty)); 

        }

        private bool IsHmtanaTimeValid166(ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors)
        {
            //בדיקה ברמת פעילות
            bool isValid = true;
            int iTimeInMinutes=0;
            clPeilut tmpPeilut;
            bool bCurrSidurEilat = false;
            bool bhaveHamtana = false;
            int iParamHamtana = 0;
            try
            {
                //נסיעת אילת ארוכה – פעילות שעונה על שני התנאים: א.שדה EilatTrip=1  ב. הערך בשדה Km הוא מעל הערך בפרמטר 149.
                if (oSidur.bSidurEilat && oSidur.IsLongEilatTrip(oSidur.dFullShatHatchala, out tmpPeilut, _oParameters.fOrechNesiaKtzaraEilat))
                {
                    bCurrSidurEilat = true;
                }
                if(oPeilut.lMakatNesia.ToString().Length>5)
                     iTimeInMinutes = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));
                    
                //1. אם בסידור אליו משויכת פעילות ההמתנה קיימת נסיעת אילת ארוכה ומשך ההמתנה (הערך בפוזיציות 4-6) הוא מעל הערך בפרמטר 148 - שגוי.
                //2. אם בסידור אליו משויכת פעילות ההמתנה לא קיימת נסיעת אילת ארוכה ומשך ההמתנה (הערך בפוזיציות 4-6) הוא מעל הערך בפרמטר 161 - שגוי.
                if (bCurrSidurEilat && oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && oPeilut.bElementHamtanaExists)
                {
                    iParamHamtana = oParam.iMaxZmanHamtanaEilat;
                    bhaveHamtana = true;
                }
                else if (!bCurrSidurEilat && oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && oPeilut.bElementHamtanaExists)
                {
                    iParamHamtana = oParam.iMaximumHmtanaTime;
                    bhaveHamtana = true;
                }

                if (bhaveHamtana && iTimeInMinutes > iParamHamtana)
                    {
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "עיכוב ארוך מעל המותר ", enErrors.errHmtanaTimeNotValid.GetHashCode());
                        InsertPeilutErrorRow(oPeilut, ref drNew);
                        dtErrors.Rows.Add(drNew);
                        isValid = false;
                    }
                
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errHmtanaTimeNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.dFullShatYetzia, oPeilut.iMisparKnisa, "IsHmtanaTimeValid166: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private void IsSidurVisaMissingSugVisa106(clSidur oSidur, ref DataTable dtErrors)
        {
            bool isSidurValid = true;
            try
            {
                if (oSidur.bSidurMyuhad)
                {//בסידור ויזה (לפי מאפיין 45 בטבלת סידורים מיוחדים עם ערך 2 ) חובה לדווח סוג ויזה. אם שדה ריק - שגוי. 
                    if (oSidur.sSidurVisaKod=="2" && oSidur.iSugHazmanatVisa == 0)
                    {
                        isSidurValid = false;
                    }

                    if (!isSidurValid)
                    {
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "סדור ויזה מחייב סוג ויזה", enErrors.errMissingSugVisa.GetHashCode());
                        drNew["sadot_nosafim"] = 1;
                        dtErrors.Rows.Add(drNew);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errMissingSugVisa.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurVisaMissingSugVisa106: " + ex.Message, null);
                _bSuccsess = false;
            }


        }

        private void IsMissingKodMevatzeaVisa178(clSidur oSidur, ref DataTable dtErrors)
        {
            bool isSidurValid = true;
            try
            {
                if (oSidur.bSidurMyuhad)
                {//בסידור ויזה (לפי מאפיין 45 בטבלת סידורים מיוחדים עם ערך 2 ) חובה לדווח קוד מבצע ויזה. אם שדה ריק - שגוי. 
                    if (oSidur.sSidurVisaKod == "2" && oSidur.iMivtzaVisa == 0 && oSidur.sLidroshKodMivtza!="")
                    {
                        isSidurValid = false;
                    }

                    if (!isSidurValid)
                    {
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "ויזה פנים מחייב קוד מבצע ויזה", enErrors.errMissingKodMevatzaVisa.GetHashCode());
                        drNew["sadot_nosafim"] = 1;
                        dtErrors.Rows.Add(drNew);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errMissingKodMevatzaVisa.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsMissingKodMevatzeaVisa178: " + ex.Message, null);
                _bSuccsess = false;
            }
         }
        
        private void HightValueDakotBefoal179(clSidur oSidur,clPeilut oPeilut, ref DataTable dtErrors)
        {
            bool isValid = true;
            try
            {
                if ((oPeilut.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode() && oPeilut.iMisparKnisa == 0) || oPeilut.iMakatType == clKavim.enMakatType.mNamak.GetHashCode() || oPeilut.iMakatType == clKavim.enMakatType.mEmpty.GetHashCode())
                {
                    if (oPeilut.iDakotBafoal > oPeilut.iMazanTashlum)
                        isValid = false;

                }

                if (oPeilut.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode() && oPeilut.iMisparKnisa > 0)
                {
                    if (oPeilut.iDakotBafoal > oParam.iMaxMinutsForKnisot)
                        isValid = false;
                }

                if (!isValid)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "ערך דקות בפועל גבוה מזמן לגמר או מהזמן המותר לכניסה ", enErrors.errHightValueDakotBefoal.GetHashCode());
                    InsertPeilutErrorRow(oPeilut, ref drNew);
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errHightValueDakotBefoal.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.dFullShatYetzia, oPeilut.iMisparKnisa, "HightValueDakotBefoal179: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

        }


        private void KisuyTorLifneyHatchalatSidur189(clSidur oSidur, clPeilut oPeilut, ref DataTable dtErrors)
        {
            DateTime dShatKisuyTor;
            try
            {
                if (oPeilut.iKisuyTor > 0)
                {
                    dShatKisuyTor = oPeilut.dFullShatYetzia.AddMinutes(-oPeilut.iKisuyTor);
                    if (dShatKisuyTor < oSidur.dFullShatHatchala)
                    {
                        drNew = dtErrors.NewRow();
                        InsertErrorRow(oSidur, ref drNew, "כיסוי תור לפני תחילת סידור", enErrors.errKisuyTorLifneyHatchalatSidur.GetHashCode());
                        InsertPeilutErrorRow(oPeilut, ref drNew);
                        dtErrors.Rows.Add(drNew);
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errKisuyTorLifneyHatchalatSidur.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.dFullShatYetzia, oPeilut.iMisparKnisa, "KisuyTorLifneyHatchalatSidur189: " + ex.Message, null);
                _bSuccsess = false;
            }

        }

        private bool HasHafifaBecauseOfHashlama108(ref DataTable dtErrors)
        {
            bool hasHafifa = false;
            bool isValid = true;

            try
            {
                int index = 1;
                clSidur curSidur = null;
                clSidur prevSidur = null;
                int errorCount = dtErrors.Rows.Count;
                for (; index < htEmployeeDetails.Count; index++)
                {
                    prevSidur = (clSidur)htEmployeeDetails[index - 1];
                    curSidur = (clSidur)htEmployeeDetails[index];
                    errorCount = dtErrors.Rows.Count;
                    IsCurrentSidurInPrevSidur168(index,ref curSidur, ref prevSidur, ref dtErrors);
                    if (prevSidur.bHashlamaExists && dtErrors.Rows.Count != errorCount)
                    {
                        hasHafifa = true;
                        break;
                    }
                }

                if (hasHafifa)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow((clSidur)htEmployeeDetails[index], ref drNew, "סדורים חופפים בשעות בהשלמה", enErrors.errHafifaBecauseOfHashlama.GetHashCode());
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", enErrors.errHafifaBecauseOfHashlama.GetHashCode(), "HasHafifaBecauseOfHashlama108: " + ex.Message);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool IsAvodatNahagutValid165(int iMisparIshi,DateTime dCardDate,bool bHaveSidurNahagut, ref DataTable dtErrors)
        {
            bool bError = false;
            bool isValid = true;

            //בדיקה ברמת סידור
            try
            {
                //לעובד מאפיין זמן נסיעות  (51, 61) ועשה סידור נהגות.
                if (oMeafyeneyOved.Meafyen51Exists) //|| (oMeafyeneyOved.Meafyen61Exists))
                {
                    if (bHaveSidurNahagut)
                       bError = true;
                       
                    if (bError)
                    {
                        drNew = dtErrors.NewRow();
                        drNew["mispar_ishi"] = iMisparIshi;
                        drNew["taarich"] = dCardDate.ToShortDateString();
                        //drNew["error_desc"] = "עובד עם מאפיין זמן נסיעה וביצע עבודת נהיגה ";
                        drNew["check_num"] = enErrors.errAvodatNahagutNotValid.GetHashCode();
                        dtErrors.Rows.Add(drNew);
                        isValid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errAvodatNahagutNotValid.GetHashCode(), iMisparIshi, dCardDate,null,null,null,null, "IsAvodatNahagutValid165: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }        

        private bool IsTimeForPrepareMechineValid86(ref int iTotalTimePrepareMechineForSidur,
                                                    ref int iTotalTimePrepareMechineForDay, 
                                                    ref int iTotalTimePrepareMechineForOtherMechines,
                                                    ref clSidur oSidur,
                                                    ref clPeilut oPeilut, 
                                                    ref DataTable dtErrors)
        {
            bool isValid = true;
            int iElementTime = 0;
            int iElementType = 0;
            bool bError = false; 
          
            //בדיקה ברמת פעילות            
            try
            {
                //זמן הכנת מכונה באלמנט הוא מוגבל בזמן הזמן משתנה אם זו הכנת מכונה ראשונה (אלמנט 701xxx00) ביום או נוספת (711xxx00). זיהוי הכנה ראשונה/נוספת ביום - אם הסידור בו מדווחת הכנת מכונה התחיל עד 8 בבוקר (לא כולל) זוהי הכנת מכונה ראשונה. כל הכנת מכונה נוספת/מאוחרת משעה 8 בבוקר (כולל) נחשבת להכנת מכונה נוספת. זמן תקין להכנת מכונה  ראשונה הוא עד הערך בפרמטר 120 (זמן הכנת מכונה ראשונה), זמן תקין להכנת מכונה נוספת הוא עד הערך בפרמטר 121 (זמן הכנת מכונה נוספת). ביום עבודה יש מקסימום זמן לסה"כ הכנות מכונה נוספות, זמן תקין לפי פרמטר 122 (מכסימום יומי להכנות מכונה נוספות דקות). ביום עבודה יש מקסימום זמן לסה"כ הכנות מכונה (ראשונה ונוספות), זמן תקין לפי פרמטר 123 (מכסימום יומי להכנות מכונה  דקות).      מקסימום הכנות מכונה מותר בסידור         יש מקסימום למספר הכנות מכונה מותרות בסידור, נבדק לפי פרמטר 124 (מכסימום הכנות מכונה בסידור אחד), לא משנה מה הסוג שלהן.      
                if ((oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode()) && (!String.IsNullOrEmpty(oPeilut.sShatYetzia)))
                {
                    iElementType = int.Parse(oPeilut.lMakatNesia.ToString().Substring(0,3));                    
                    if ((iElementType == 701) || (iElementType == 711))
                    {
                        iElementTime = int.Parse(oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(3, 3));
                        if ((iElementType == 701) && oPeilut.dFullShatYetzia < oPeilut.dFullShatYetzia.Date.AddHours(8) || clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim,iSugYom,oSidur.dSidurDate))
                        {
                            //מכונה ראשונה ביום- נשווה לפרמטר 120
                            bError = (iElementTime > oParam.iPrepareFirstMechineMaxTime);
                            //צבירת זמן כל המכונות ביום ראשונה ונוספות נשווה לפרמטר 123
                            iTotalTimePrepareMechineForDay = iTotalTimePrepareMechineForDay + iElementTime;
                            //צבירת זמן כלל המכונות לסידור נשווה מול פרמטר 124
                            //iTotalTimePrepareMechineForSidur = iTotalTimePrepareMechineForSidur + iElementTime;
                            iTotalTimePrepareMechineForSidur = iTotalTimePrepareMechineForSidur + 1 ;
                        }
                        else if ((iElementType == 701) && oPeilut.dFullShatYetzia >= oPeilut.dFullShatYetzia.Date.AddHours(8) && !clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim,iSugYom,oSidur.dSidurDate))
                        {
                             //מכונות נוספות נשווה לפרמטר 121
                             bError = (iElementTime > oParam.iPrepareOtherMechineMaxTime);
                             //צבירת זמן כל המכונות ביום ראשונה ונוספות נשווה לפרמטר 123
                             iTotalTimePrepareMechineForDay = iTotalTimePrepareMechineForDay + iElementTime;
                             //צבירת זמן כלל המכונות לסידור נשווה מול פרמטר 124
                             //iTotalTimePrepareMechineForSidur = iTotalTimePrepareMechineForSidur + iElementTime;
                             iTotalTimePrepareMechineForSidur = iTotalTimePrepareMechineForSidur + 1;

                             if (iElementType == 711)
                             {
                                 //צבירת זמן כל המכונות הנוספות - נשווה בסוף מול פרמטר 122
                                 iTotalTimePrepareMechineForOtherMechines = iTotalTimePrepareMechineForOtherMechines + iElementTime;
                             }
                        }

                        if (bError)
                        {
                            drNew = dtErrors.NewRow();
                            InsertErrorRow(oSidur, ref drNew, "הכנת מכונה מעל המותר ", enErrors.errTimeForPrepareMechineNotValid.GetHashCode());
                            InsertPeilutErrorRow(oPeilut, ref drNew);
                            dtErrors.Rows.Add(drNew);
                            isValid = false;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errTimeForPrepareMechineNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.dFullShatYetzia, oPeilut.iMisparKnisa, "IsTimeForPrepareMechineValid86: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool CheckPrepareMechineForSidurValidity86(ref clSidur oSidur, int iTotalTimePrepareMechineForSidur, ref DataTable dtErrors)
        {
            bool isValid = true;

            try
            {
                //נשווה את זמן כל האלמנטים של הכנת מכונה בסידור לפרמטר 124, אם עלה על המותר נעלה שגיאה
                if (iTotalTimePrepareMechineForSidur > oParam.iPrepareAllMechineTotalMaxTimeForSidur)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "הכנת מכונה מעל המותר ", enErrors.errTimeForPrepareMechineNotValid.GetHashCode());
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errTimeForPrepareMechineNotValid.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "CheckPrepareMechineForSidurValidity86: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool CheckPrepareMechineForDayValidity86(int iMisparIshi, int iTotalTimePrepareMechineForDay, DateTime dCardDate, ref DataTable dtErrors)
        {
            bool isValid = true;

            try
            {
                //נשווה את זמן כל האלמנטים של הכנת מכונה ביום לפרמטר 123, אם עלה על המותר נעלה שגיאה
                if (iTotalTimePrepareMechineForDay > oParam.iPrepareAllMechineTotalMaxTimeInDay)
                {
                    drNew = dtErrors.NewRow();
                    drNew["mispar_ishi"] = iMisparIshi;
                    drNew["taarich"] = dCardDate.ToShortDateString();
                    //drNew["error_desc"] = "הכנת מכונה מעל המותר ";
                    drNew["check_num"] = enErrors.errTimeForPrepareMechineNotValid.GetHashCode();
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, iMisparIshi, "E", enErrors.errTimeForPrepareMechineNotValid.GetHashCode(), dCardDate, "CheckPrepareMechineForDayValidity86: " + ex.Message);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private bool CheckPrepareMechineOtherElementForDayValidity86(int iMisparIshi, int iTotalTimePrepareMechineForOtherMechines, DateTime dCardDate, ref DataTable dtErrors)
        {
            bool isValid = true;

            try
            {
                //נשווה את זמן כל האלמנטים של הכנת מכונה נוספות ביום לפרמטר 122, אם עלה על המותר נעלה שגיאה
                if (iTotalTimePrepareMechineForOtherMechines > oParam.iPrepareOtherMechineTotalMaxTime)
                {
                    drNew = dtErrors.NewRow();
                    drNew["mispar_ishi"] = iMisparIshi;
                    drNew["taarich"] = dCardDate.ToShortDateString();
                    //drNew["error_desc"] = "הכנת מכונה מעל המותר ";
                    drNew["check_num"] = enErrors.errTimeForPrepareMechineNotValid.GetHashCode();
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, iMisparIshi, "E", enErrors.errTimeForPrepareMechineNotValid.GetHashCode(), dCardDate, "CheckPrepareMechineOtherElementForDayValidity86: " + ex.Message);
                isValid = false;
                _bSuccsess = false;
            }

            return isValid;
        }

        private void HighValueKisuyTor87(clSidur oSidur, clPeilut oPeilut, ref DataTable dtErrors)
        {
            bool isValid = true;
            try
            {
                if ((oPeilut.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode() && oPeilut.iMisparKnisa == 0) || oPeilut.iMakatType == clKavim.enMakatType.mNamak.GetHashCode() )
                {
                    if (oPeilut.iKisuyTor > oPeilut.iKisuyTorMap)
                        isValid = false;

                }

                
                if (!isValid)
                {
                    drNew = dtErrors.NewRow();
                    InsertErrorRow(oSidur, ref drNew, "כסוי תור מעל המותר", enErrors.errHighValueKisuyTor.GetHashCode());
                    InsertPeilutErrorRow(oPeilut, ref drNew);
                    dtErrors.Rows.Add(drNew);
                    isValid = false;
                }

            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errHighValueKisuyTor.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.dFullShatYetzia, oPeilut.iMisparKnisa, "HighValueKisuyTor87: " + ex.Message, null);
                isValid = false;
                _bSuccsess = false;
            }

        }
        public clParameters oParam
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

        public clOvedYomAvoda oOvedYomAvodaDetails
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
        public clMeafyenyOved oMeafyeneyOved                               
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
        //public OrderedDictionary htEmployeeDetailsWithCancled
        //{
        //    set
        //    {
        //        _htEmployeeDetailsWithCancled = value;
        //    }
        //    get
        //    {
        //        return _htEmployeeDetailsWithCancled;
        //    }
        //}
      
        public clGeneral.enCardStatus CardStatus
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
                dtYamimMeyuchadim = clGeneral.GetYamimMeyuchadim();
                _dtSugeyYamimMeyuchadim = clGeneral.GetSugeyYamimMeyuchadim();

                //Get Meafyeny Ovdim
                oMeafyeneyOved = new clMeafyenyOved(iMisparIshi, dCardDate);

                dtSibotLedivuachYadani = oUtils.GetCtbSibotLedivuchYadani();

                iSugYom = clGeneral.GetSugYom(dtYamimMeyuchadim, dCardDate, _dtSugeyYamimMeyuchadim);//, _oMeafyeneyOved.iMeafyen56);

                //Set global variable with parameters
                oParam = new clParameters(dCardDate, iSugYom);
               //oParam = clDefinitions.GetParamInstance(dCardDate, iSugYom);           

                //Get Meafyeney Sug Sidur
                dtSugSidur = clDefinitions.GetSugeySidur();

                dtMatzavOved = GetOvedMatzav(iMisparIshi, dCardDate);
                //Get Mutamut
                dtMutamut = oUtils.GetCtbMutamut();

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
                if (oOvedYomAvodaDetails.OvedDetailsExists)
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
                                clKavim oKavim = new clKavim();
                                dtMashar = oKavim.GetMasharData(sCarNumbers);
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
        //private void SetOvedYomAvodaDetails(int iMisparIshi, DateTime dCardDate)
        //{
        //    oOvedYomAvodaDetails = new clOvedYomAvoda(iMisparIshi, dCardDate);
        //}

        private void CheckAllData(DateTime dCardDate, int iMisparIshi, int iSugYom)
        {
            clSidur oSidur = new clSidur();
            clPeilut oPeilut = new clPeilut();


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
            _oCollIdkunRashemetDel = new COLL_IDKUN_RASHEMET();
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
            clSidur oSidurNidrashHityatvut;
            SourceObj SourceObject;
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd=null;
            try
            {                
                //נאתחל את אובייקט ימי עבודה לעדכון
                oObjYameyAvodaUpd = new OBJ_YAMEY_AVODA_OVDIM();
                InsertToYameyAvodaForUpdate(dCardDate, ref oObjYameyAvodaUpd, ref _oOvedYomAvodaDetails);
                //נעבור על כל הסידורים 
                if (htEmployeeDetails != null)
                {
                    //מחיקת סידורי רציפות
                    DeleteSidureyRetzifut();

                    //סימון סידורים לא לתשלום=0
                    for (i = 0; i < htEmployeeDetails.Count; i++)
                    {
                        oSidur = (clSidur)htEmployeeDetails[i];
                        oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur);
                        if (!CheckIdkunRashemet("LO_LETASHLUM", oSidur.iMisparSidur, oSidur.dFullShatHatchala))
                        {
                            if (oSidur.iLoLetashlum == 1 && !(oSidur.iKodSibaLoLetashlum == 1 || oSidur.iKodSibaLoLetashlum == 11 || oSidur.iKodSibaLoLetashlum == 20 || oSidur.iKodSibaLoLetashlum == 19 || oSidur.iKodSibaLoLetashlum == 23))
                            {
                                oSidur.iLoLetashlum = 0;
                                oSidur.iKodSibaLoLetashlum = 0;
                                oObjSidurimOvdimUpd.LO_LETASHLUM = 0;
                                oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM = 0;                              
                            }
                          
                       }
                      if (oSidur.iLoLetashlum == 1 || (oSidur.iLoLetashlum == 0 && !CheckIdkunRashemet("PITZUL_HAFSAKA", oSidur.iMisparSidur, oSidur.dFullShatHatchala)))
                      {
                         oObjSidurimOvdimUpd.PITZUL_HAFSAKA = 0;
                         oSidur.sPitzulHafsaka = "0";
                         if (CheckIdkunRashemet("PITZUL_HAFSAKA", oSidur.iMisparSidur, oSidur.dFullShatHatchala))
                             DeleteIdkunRashemet("PITZUL_HAFSAKA", oSidur.iMisparSidur, oSidur.dFullShatHatchala);
                      }
                     
                       oObjSidurimOvdimUpd.MEZAKE_NESIOT = 0;
                       oObjSidurimOvdimUpd.MEZAKE_HALBASHA = 0;
                       oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                       
                       htEmployeeDetails[i] = oSidur;
                    }

                 

                    //שינוי 01
                    for (i = 0; i < htEmployeeDetails.Count; i++)
                    {
                        oSidur = (clSidur)htEmployeeDetails[i];
                        if (!CheckIdkunRashemet("MISPAR_SIDUR", oSidur.iMisparSidur, oSidur.dFullShatHatchala))
                        {
                            FixedMisparSidur01(ref oSidur, i);
                            
                            htEmployeeDetails[i] = oSidur;
                        }
                    }


                    //-שינוי 02
                    for (i = 0; i < htEmployeeDetails.Count; i++)
                    {
                        oSidur = (clSidur)htEmployeeDetails[i];
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
                        oSidur = (clSidur)htEmployeeDetails[i];
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
                    ////    oSidur = (clSidur)htEmployeeDetails[i];
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
                        oSidur = (clSidur)htEmployeeDetails[i];
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
                    clSidur oSidurFirst = new clSidur();
                    clSidur oSidurSecond = new clSidur();
                    int indexSidurFirst = 0;
                    bool flag = true;
                    
                    for (i = 0; i < htEmployeeDetails.Count; i++)
                    {
                        if (flag)
                        {
                            oSidur = (clSidur)htEmployeeDetails[i];
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
                            oSidurSecond = (clSidur)htEmployeeDetails[i + 1];
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
                        bUsedMazanTichnun = false;
                        oSidur = (clSidur)htEmployeeDetails[i];
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
                    ////    oSidur = (clSidur)htEmployeeDetails[i];
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
                        oSidur = (clSidur)htEmployeeDetails[i];
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
                        oSidur = (clSidur)htEmployeeDetails[i];
                        oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur);

                        //מחוץ למכסה
                        if (!CheckIdkunRashemet("OUT_MICHSA", oSidur.iMisparSidur, oSidur.dFullShatHatchala))
                            UpdateOutMichsa(ref oSidur, ref oObjSidurimOvdimUpd);

                        //חריגה
                        if (!CheckIdkunRashemet("CHARIGA", oSidur.iMisparSidur, oSidur.dFullShatHatchala)) //!CheckApproval("2,4,5,6,10", oSidur.iMisparSidur, oSidur.dFullShatHatchala) &&
                            UpdateChariga(ref oSidur, ref oObjSidurimOvdimUpd);

                        //השלמה
                        if (!CheckIdkunRashemet("HASHLAMA", oSidur.iMisparSidur, oSidur.dFullShatHatchala))
                            UpdateHashlamaForSidur(ref oSidur, i, ref oObjSidurimOvdimUpd);

                        htEmployeeDetails[i] = oSidur;
                    } 

                    for (i = 0; i < htEmployeeDetails.Count; i++)
                    {
                        oSidur = (clSidur)htEmployeeDetails[i];
                        oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur);

                        iCountPeiluyot = ((clSidur)(htEmployeeDetails[i])).htPeilut.Count;
                        j = 0;

                        if (iCountPeiluyot > 0)
                        {
                            do
                            {
                                oPeilut = (clPeilut)((clSidur)(htEmployeeDetails[i])).htPeilut[j];
                                oObjPeilutOvdimUpd = GetUpdPeilutObject(i, oPeilut, out SourceObject, oObjSidurimOvdimUpd);

                                DeleteElementRechev07(ref oSidur, ref oPeilut, ref  j, ref oObjSidurimOvdimUpd);
                                //ChangeElement06(ref oPeilut, ref oSidur, j);

                                j += 1;
                                iCountPeiluyot = ((clSidur)(htEmployeeDetails[i])).htPeilut.Count;
                            }
                            while (j < iCountPeiluyot);
                        }
                        htEmployeeDetails[i] = oSidur;
                    }

                    //שינוי 12 
                    //שינוי זה צריך לעבוד אחרי שינוי 23.
                    for (i = 0; i < htEmployeeDetails.Count; i++)
                    {
                        oSidur = (clSidur)htEmployeeDetails[i];
                      
                            oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur);

                            dShatHatchalaNew = oSidur.dFullShatHatchala;
                            bUpdateShatHatchala = false;
                            iCountPeiluyot = ((clSidur)(htEmployeeDetails[i])).htPeilut.Count;
                            j = 0;

                            if (iCountPeiluyot > 0)
                            {
                                do
                                {
                                    oPeilut = (clPeilut)((clSidur)(htEmployeeDetails[i])).htPeilut[j];
                                    oObjPeilutOvdimUpd = GetUpdPeilutObject(i, oPeilut, out SourceObject, oObjSidurimOvdimUpd);

                                    FixedShatHatchalaLefiShatHachtamatItyatzvut12(ref j, ref oPeilut, ref oSidur, ref oObjSidurimOvdimUpd, ref oObjPeilutOvdimUpd, ref  bUpdateShatHatchala, ref dShatHatchalaNew, SourceObject);
                                 
                                    j += 1;
                                    iCountPeiluyot = ((clSidur)(htEmployeeDetails[i])).htPeilut.Count;
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
                        oSidur = (clSidur)htEmployeeDetails[i];
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
                    //        oSidur = (clSidur)htEmployeeDetails[i];
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
                        oSidur = (clSidur)htEmployeeDetails[i];

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

                if (_oCollIdkunRashemetDel.Count > 0)
                    clDefinitions.DeleteIdkunRashemet(_oCollIdkunRashemetDel);

                if (_oCollApprovalErrors.Count > 0)
                    clDefinitions.UpdateAprrovalErrors(_oCollApprovalErrors);

            }
            catch (Exception ex)
            {
                _bSuccsess = false;
                throw new Exception("CheckAllData: " + iMisparIshi + " " + dCardDate.ToShortDateString() + ex.Message);
            }
        }

        //private bool CheckApproval(string sKodIshur)
        //{
        //    bool bHaveIshur=false;
        //    DataRow[] drApproval;
        //    try{
        //   drApproval= _dtApproval.Select("KOD_ISHUR IN(" + sKodIshur + ")");
        //   if (drApproval.Length > 0)
        //       bHaveIshur = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return bHaveIshur;
        //}

        //private bool CheckApproval(string sKodIshur,int iMisparSidur,DateTime dShatHatchala)
        //{
        //    bool bHaveIshur = false;
        //    DataRow[] drApproval;
        //    try{
        //    drApproval = _dtApproval.Select("KOD_ISHUR IN(" + sKodIshur + ") AND MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime')");
        //    if (drApproval.Length > 0)
        //        bHaveIshur = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return bHaveIshur;
        //}

        //private bool CheckApproval(string sKodIshur, int iMisparSidur, DateTime dShatHatchala,int iMisparKnisa,DateTime dShatYetzia)
        //{
        //    bool bHaveIshur = false;
        //    DataRow[] drApproval;
        //    try{
        //    drApproval = _dtApproval.Select("KOD_ISHUR IN(" + sKodIshur + ") AND MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime') AND MISPAR_KNISA=" + iMisparKnisa + " AND SHAT_YETZIA=Convert('" + dShatYetzia.ToString() + "', 'System.DateTime') ");
        //    if (drApproval.Length > 0)
        //        bHaveIshur = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return bHaveIshur;
        //}

        //private bool CheckApprovalToEmploee(int iMisparIshi, DateTime dTaarich, string sKodIshur, int iMisparSidur, DateTime dShatHatchala)
        //{
        //    bool bHaveIshur = false;
        //    DataRow[] drApproval;
        //    //DataTable dtApproval;
        //    try{
        //    //dtApproval = clDefinitions.GetApprovalToEmploee(iMisparIshi, dTaarich);
       
        //    drApproval = _dtApproval.Select("KOD_ISHUR IN(" + sKodIshur + ") AND MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime')");
        //    if (drApproval.Length > 0)
        //        bHaveIshur = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return bHaveIshur;
        //}

        //private bool CheckApprovalToEmploee(int iMisparIshi,DateTime dTaarich, string sKodIshur, int iMisparSidur, DateTime dShatHatchala,int iMisparKnisa,DateTime dShatYetzia)
        //{
        //    bool bHaveIshur = false;
        //    DataRow[] drApproval;
        //    //DataTable dtApproval;
        //    try{
        //    //dtApproval = clDefinitions.GetApprovalToEmploee(iMisparIshi, dTaarich);
       
        //    drApproval = _dtApproval.Select("KOD_ISHUR IN(" + sKodIshur + ") AND MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime') AND MISPAR_KNISA=" + iMisparKnisa + " AND SHAT_YETZIA=Convert('" + dShatYetzia.ToString() + "', 'System.DateTime') ");
        //    if (drApproval.Length > 0)
        //        bHaveIshur = true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return bHaveIshur;
        //}

        //private int CheckApprovalStatus(string sKodIshur, int iMisparSidur, DateTime dShatHatchala)
        //{
        //    int iStatus=0;
        //    DataRow[] drApproval;
        //    try{
        //    drApproval = _dtApproval.Select("KOD_ISHUR IN(" + sKodIshur + ") AND MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime')", "RAMA DESC");
        //    if (drApproval.Length > 0)
        //    {
        //        iStatus = int.Parse(drApproval[0]["kod_status_ishur"].ToString());
        //    }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return iStatus;
        //}

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

        private void DeleteIdkunRashemet(string sFieldToChange, int iMisparSidur, DateTime dShatHatchala)
        {
            //bool bHaveIdkun = false;
            DataRow dr;
            OBJ_IDKUN_RASHEMET ObjIdkunRashemet;
            try
            {
                for (int i = 0; i < _dtIdkuneyRashemet.Rows.Count; i++)
                {
                    dr = _dtIdkuneyRashemet.Rows[i];

                    if (dr["shem_db"].ToString() ==  sFieldToChange.ToUpper() && int.Parse(dr["MISPAR_SIDUR"].ToString()) == iMisparSidur && dr["shat_hatchala"].ToString() == dShatHatchala.ToString())
                    {
                        ObjIdkunRashemet = FillIdkunRashemet(dr);
                        _oCollIdkunRashemetDel.Add(ObjIdkunRashemet);
                        _dtIdkuneyRashemet.Rows.RemoveAt(i);
                    }
                    
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

        //private bool CheckBitulRashemet(int iMisparSidur, DateTime dShatHatchala, int iMisparKnisa, DateTime dShatYetzia)
        //{
        //    bool bHaveBitul = false;
        //     clSidur oSidur;
        //     clPeilut oPeilut;
        //    try{
        //     oSidur = _htEmployeeDetailsWithCancled.Values
        //                          .Cast<clSidur>()
        //                          .ToList()
        //                         .Find(sidur => (sidur.iMisparSidur == iMisparSidur && sidur.dFullShatHatchala == dShatHatchala));

        //     if (oSidur != null)
        //     {
        //         oPeilut = oSidur.htPeilut.Values
        //                             .Cast<clPeilut>()
        //                             .ToList()
        //                            .Find(peilut => (peilut.iMisparKnisa == iMisparKnisa && peilut.dFullShatYetzia == dShatYetzia && (peilut.iBitulOHosafa == 1 || peilut.iBitulOHosafa == 3)));
        //         if (oPeilut != null)
        //         {
        //             bHaveBitul = true;
        //         }
        //     }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return bHaveBitul;
        //}

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

        private OBJ_PEILUT_OVDIM GetUpdPeilutObject(int iSidurIndex, clPeilut oPeilut, out SourceObj SourceObject, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            OBJ_PEILUT_OVDIM oObjPeilutOvdimUpd = new OBJ_PEILUT_OVDIM();
            SourceObject = SourceObj.Update;
            try
            {
                clSidur oSidur = (clSidur)htEmployeeDetails[iSidurIndex];
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
                    if (oObjPeilutOvdimUpd.MISPAR_SIDUR == 0)
                    {
                        InsertToObjPeilutOvdimForUpdate(ref oPeilut, oObjSidurimOvdimUpd, ref oObjPeilutOvdimUpd);
                        oCollPeilutOvdimUpd.Add(oObjPeilutOvdimUpd);
                        SourceObject = SourceObj.Update;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oObjPeilutOvdimUpd;
        }

        private OBJ_PEILUT_OVDIM GetUpdPeilutObjectCancel(int iSidurIndex, clPeilut oPeilut,  OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            OBJ_PEILUT_OVDIM oObjPeilutOvdimUpd = new OBJ_PEILUT_OVDIM();
            try
            {
                clSidur oSidur = (clSidur)htEmployeeDetails[iSidurIndex];
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


        private OBJ_SIDURIM_OVDIM GetUpdSidurObject(clSidur oSidur)
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

        private void FixedMisparMatalatVisa02(ref clSidur oSidur, int iSidurIndex,ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            clPeilut oPeilut;
            int iNewMisparSidur = 0;
            SourceObj SourceObject;
            bool bHaveNesiatNamak = false;
            try
            {
                bHaveNesiatNamak = oSidur.htPeilut.Values.Cast<clPeilut>().ToList().Any(Peilut => Peilut.iMakatType == clKavim.enMakatType.mNamak.GetHashCode());
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
                        oSidur = new clSidur(oSidur, _dCardDate, iNewMisparSidur, drSidurMeyuchad[0]);
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
                            oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse("50" + oNewSidurim.SidurOld);  //long.Parse("50000000");
                            oObjPeilutOvdimIns.MISPAR_VISA = oNewSidurim.SidurOld;
                            oObjPeilutOvdimIns.BITUL_O_HOSAFA = 4;
                            oCollPeilutOvdimIns.Add(oObjPeilutOvdimIns);

                            clPeilut oPeilutNew = new clPeilut(_iMisparIshi, _dCardDate, oObjPeilutOvdimIns, dtTmpMeafyeneyElements);
                            oPeilutNew.iBitulOHosafa = 4;
                            oSidur.htPeilut.Insert(0, 1, oPeilutNew);
                        }
                        else
                        {
                            for (int j = 0; j < ((clSidur)(htEmployeeDetails[iSidurIndex])).htPeilut.Count; j++)
                            {
                                oPeilut = (clPeilut)((clSidur)(htEmployeeDetails[iSidurIndex])).htPeilut[j];
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

        private void FixedMisparMatalatVisa02(ref OBJ_PEILUT_OVDIM oObjPeilutOvdimUpd, ref clPeilut oPeilut, clSidur oSidur, int iOldMisparSidur, SourceObj SourceObject)
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
                    
                clPeilut oPeilutNew = new clPeilut(_iMisparIshi, _dCardDate, oPeilut, oObjPeilutOvdimUpd.MAKAT_NESIA, dtTmpMeafyeneyElements);
                oPeilutNew.lMisparVisa = oObjPeilutOvdimUpd.MISPAR_VISA;
                oPeilutNew.iPeilutMisparSidur = oSidur.iMisparSidur;
         //       oPeilutNew.iMakatType = clKavim.enMakatType.mVisa.GetHashCode();
                oPeilut = oPeilutNew;

            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E",null, 2, _iMisparIshi, _dCardDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala,oPeilut.dFullShatYetzia, oPeilut.iMisparKnisa, "FixedMisparMatalatVisa02: " + ex.Message, null);
                _bSuccsess = false;
            }
        }

        private void FixedPitzulHafsaka06(ref clSidur oSidur, int iNextSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            int iCountPitzul,iMinPaar;
            clSidur oNextSidur = null;
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
                                if (iNextSidur < htEmployeeDetails.Values.Cast<clSidur>().ToList().Count)
                                {
                                    oNextSidur = (clSidur)htEmployeeDetails[iNextSidur];
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
                                    if (oSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString() || oSidur.sErevShishiChag == "1")
                                    { 
                                        dShatKnisatShabat = _oParameters.dKnisatShabat;
                                        if (oNextSidur.dFullShatHatchala < dShatKnisatShabat)
                                            iMinPaar = _oParameters.iMinHefreshSidurimLepitzulSummer;
                                        else return;
                                      
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
                                        else if (_oMeafyeneyOved.Meafyen41Exists && (bSidurTafkid && oSidur.iZakayLepizul > 0 && bNextSidurTafkid && oNextSidur.iZakayLepizul > 0))
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

        private void FixedShatHatchalaAndGmarToMafilim26(ref clSidur oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, bool bIdkunRashShatHatchala, bool bIdkunRashShatGmar)
        {
            DateTime dRequiredShatHatchala = DateTime.MinValue; 
            DateTime dRequiredShatGmar = DateTime.MinValue; 
            DateTime dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM;
            DateTime dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM;
            bool bFromMeafyenHatchala, bFromMeafyenGmar;
            try
            {
                if ( (oOvedYomAvodaDetails.iIsuk == 122 || oOvedYomAvodaDetails.iIsuk == 123 || oOvedYomAvodaDetails.iIsuk == 124 || oOvedYomAvodaDetails.iIsuk == 127)
                    && oSidur.sSugAvoda==clGeneral.enSugAvoda.Shaon.GetHashCode().ToString() )
                {
                    if (oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA.ToShortDateString() != DateTime.MinValue.ToShortDateString())
                        dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA;
                    else if (oObjSidurimOvdimUpd.SHAT_HATCHALA.ToShortDateString() != DateTime.MinValue.ToShortDateString())
                        dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA;
                    else dShatHatchalaLetashlumToUpd = DateTime.MinValue;

                    GetMeafyeneyMafilim(oSidur,oSidur.dFullShatHatchala, oSidur.dFullShatGmar, out  bFromMeafyenHatchala, out  bFromMeafyenGmar, ref dRequiredShatHatchala, ref dRequiredShatGmar);

                   // dShatHatchalaLetashlumToUpd = dRequiredShatHatchala;
                   // dShatGmarLetashlumToUpd = dRequiredShatGmar;
                    
                    SetShatHatchalaGmarKizuz(ref oSidur, ref oObjSidurimOvdimUpd, dRequiredShatHatchala, dRequiredShatGmar, ref dShatHatchalaLetashlumToUpd, ref dShatGmarLetashlumToUpd);

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
                        if (oSidur.dFullShatHatchalaLetashlum.Year < clGeneral.cYearNull)
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

        private void FixedShatHatchalaAndGmarSidurMapa27(ref clSidur oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
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
                    if (oSidur.dFullShatHatchalaLetashlum.Year < clGeneral.cYearNull)
                        oSidur.sShatHatchalaLetashlum = "";
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 27, oSidur.iMisparIshi,oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "FixedShatHatchalaAndGmarSidurMapa27: " + ex.Message, null);
                _bSuccsess = false;
            }
        }

        private void SetShaotSidurKupai(int Index,ref clSidur oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            //string sug_sidur = "";
            DataRow[] drSugSidur;
            clSidur ESidur;
            DateTime dShatHatchala, dShatGmar;
            try
            {
                if (oOvedYomAvodaDetails.iKodHevra != clGeneral.enEmployeeType.enEggedTaavora.GetHashCode())
                {
                    drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, oSidur.dSidurDate, _dtSugSidur);

                    if (oSidur.sSugAvoda == clGeneral.enSugAvoda.Kupai.GetHashCode().ToString() || (drSugSidur.Length > 0 && drSugSidur[0]["sug_avoda"].ToString() == clGeneral.enSugAvoda.Kupai.GetHashCode().ToString()))
                    {
                        for (int i = 0; i < htEmployeeDetails.Count; i++)
                        {
                            if (i != Index)
                            {
                                ESidur = (clSidur)htEmployeeDetails[i];
                                if (ESidur.sSugAvoda == clGeneral.enSugAvoda.Shaon.GetHashCode().ToString())
                                {
                                    if ((Math.Abs(oSidur.dFullShatHatchala.Subtract(ESidur.dFullShatHatchala).TotalMinutes) <= 60) &&
                                        (Math.Abs(ESidur.dFullShatGmar.Subtract(oSidur.dFullShatGmar).TotalMinutes) <= 60))
                                    {
                                        dShatHatchala = oSidur.dFullShatHatchala > ESidur.dFullShatHatchala ? oSidur.dFullShatHatchala : ESidur.dFullShatHatchala;
                                        dShatGmar = oSidur.dFullShatGmar < ESidur.dFullShatGmar?oSidur.dFullShatGmar : ESidur.dFullShatGmar;

                                        if (dShatHatchala != oObjSidurimOvdimUpd.SHAT_HATCHALA)
                                        {
                                            UpdateShatHatchala(ref oSidur, i, dShatHatchala, ref oObjSidurimOvdimUpd);
                                        }
                                        if (dShatGmar != oObjSidurimOvdimUpd.SHAT_GMAR)
                                        {
                                            oObjSidurimOvdimUpd.SHAT_GMAR = dShatGmar;
                                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;

                                            oSidur.dFullShatGmar = oObjSidurimOvdimUpd.SHAT_GMAR;
                                            oSidur.sShatGmar = oSidur.dFullShatGmar.ToString("HH:mm");
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
           //     clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, enErrors.errFirstDayShlilatRishayon195.GetHashCode(), oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurNAhagutValid161: " + ex.Message, null);
            }
        }

        private void MergerSiduryMapa28()
        {
            clSidur oSidur, oSidurPutzal;
            clPeilut oLastPeilut, oPeilut, oFirstPeilut;
            string sMakat;
            clKavim oKavim = new clKavim();
            DataSet dsSidur;
            int iResult;
            long lMakatNesia;
            string sShaa;
            bool bSidurOkev = false;
            bool bHaveSidur, bCancelHachanatMechona;
            int l, I, iCountPeiluyot, iCountSidurim;
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimDel;
            DateTime dShatYetzia;
            List<clSidur> ListSidurim;
            try
            {
                iCountSidurim = htEmployeeDetails.Values.Count;
                I = 0;

                if (iCountSidurim > 0)
                {
                    do
                    {
                        oSidur = (clSidur)htEmployeeDetails[I];
                        bHaveSidur = false;
                        bSidurOkev = false;
                        if (!oSidur.bSidurMyuhad)
                        {
                            //אם מזהים בטבלת סידורים עובדים TB_SIDURIM_OVDIM עבור מ.א. + תאריך את אותו מס' סידור עם שעת התחלה שונה אז בודקים האם זה סידור שפוצל.
                            oSidurPutzal = oSidur;

                            ListSidurim = htEmployeeDetails.Values.Cast<clSidur>().ToList().FindAll(Sidur => (Sidur.dFullShatHatchala < oSidur.dFullShatHatchala)).ToList();
                            if (ListSidurim.Count > 1)
                            {
                                ListSidurim.Sort(delegate(clSidur first, clSidur second)
                                                    {
                                                        return first.dFullShatHatchala.CompareTo(second.dFullShatHatchala);
                                                    });
                            }
                            if (ListSidurim.Count > 0)
                            {
                                l = ListSidurim.Count - 1;
                                oSidurPutzal = (clSidur)ListSidurim[l];

                                if (oSidurPutzal.iMisparSidur == oSidur.iMisparSidur)
                                {
                                    bHaveSidur = true;
                                }


                                if (bHaveSidur && oSidurPutzal.htPeilut.Values.Count > 0 && oSidur.htPeilut.Values.Count > 0)
                                {
                                    oLastPeilut = (clPeilut)oSidurPutzal.htPeilut[oSidurPutzal.htPeilut.Count -1];// oSidurPutzal.htPeilut.Values.Cast<clPeilut>().ToList().LastOrDefault(peilut => (peilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "756"));
                                    oFirstPeilut = oSidur.htPeilut.Values.Cast<clPeilut>().ToList().FirstOrDefault(peilut => (peilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != KdsLibrary.clGeneral.enElementHachanatMechona.Element701.GetHashCode().ToString() && peilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != KdsLibrary.clGeneral.enElementHachanatMechona.Element712.GetHashCode().ToString() && peilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != KdsLibrary.clGeneral.enElementHachanatMechona.Element711.GetHashCode().ToString() && peilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "756"));
                                    if (oFirstPeilut != null && oLastPeilut != null)
                                    {
                                        dsSidur = oKavim.GetSidurAndPeiluyotFromTnua(oSidurPutzal.iMisparSidur, oSidurPutzal.dSidurDate, 1, out iResult);
                                        if (iResult == 0)
                                        {
                                            for (int i = 0; i < dsSidur.Tables[1].Rows.Count; i++)
                                            {

                                                sShaa = dsSidur.Tables[1].Rows[i]["SHAA"].ToString();
                                                lMakatNesia = long.Parse(dsSidur.Tables[1].Rows[i]["MAKAT8"].ToString());
                                                dShatYetzia = clGeneral.GetDateTimeFromStringHour(sShaa, oSidur.dFullShatHatchala);
                                                if (oLastPeilut.lMakatNesia == lMakatNesia && oLastPeilut.dFullShatYetzia == dShatYetzia && i + 1 < dsSidur.Tables[1].Rows.Count)
                                                {
                                                    lMakatNesia = long.Parse(dsSidur.Tables[1].Rows[i + 1]["MAKAT8"].ToString());
                                                    sShaa = dsSidur.Tables[1].Rows[i + 1]["SHAA"].ToString();

                                                    if ((lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == KdsLibrary.clGeneral.enElementHachanatMechona.Element712.GetHashCode().ToString() || lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == KdsLibrary.clGeneral.enElementHachanatMechona.Element711.GetHashCode().ToString())  && i + 2 < dsSidur.Tables[1].Rows.Count)
                                                    {
                                                        lMakatNesia = long.Parse(dsSidur.Tables[1].Rows[i + 2]["MAKAT8"].ToString());

                                                        sShaa = dsSidur.Tables[1].Rows[i + 2]["SHAA"].ToString();
                                                    }

                                                    dShatYetzia = clGeneral.GetDateTimeFromStringHour(sShaa, oSidur.dFullShatHatchala);
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

                                                oPeilut = (clPeilut)oSidur.htPeilut[j];
                                                sMakat = oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3);

                                                //לבדוק האם מס' הרכב TB_PEILUT_OVDIM.OTO_NO בפעילות האחרונה בסידור1 שונה מ- מס' הרכב בפעילות הראשונה שהועתקה מסידור2 אם כן, יש להשאיר את הכנת המכונה מק"ט 701,711,712. אחרת, אם מס' הרכב זהים יש לבטל את הכנת המכונה שהועברה מסידור2 TB_PEILUT_OVDIM. BITUL_O_HOSAFA=3.
                                                //if (j == 0)
                                                //{
                                                //    if (oLastPeilut.lOtoNo == oPeilut.lOtoNo)
                                                //    { bCancelHachanatMechona = true; }
                                                //}

                                                //if (!bCancelHachanatMechona || (bCancelHachanatMechona && sMakat != KdsLibrary.clGeneral.enElementHachanatMechona.Element701.GetHashCode().ToString() && sMakat != KdsLibrary.clGeneral.enElementHachanatMechona.Element712.GetHashCode().ToString() && sMakat != KdsLibrary.clGeneral.enElementHachanatMechona.Element711.GetHashCode().ToString()))
                                                //{
                                                    OBJ_PEILUT_OVDIM oObjPeilutOvdimIns = new OBJ_PEILUT_OVDIM();
                                                    InsertToObjPeilutOvdimForUpdate(ref oPeilut, oObjSidurimOvdimDel, ref oObjPeilutOvdimIns);
                                                    oObjPeilutOvdimIns.MISPAR_SIDUR = oSidurPutzal.iMisparSidur;
                                                    oObjPeilutOvdimIns.SHAT_HATCHALA_SIDUR = oSidurPutzal.dFullShatHatchala;
                                                    oObjPeilutOvdimIns.BITUL_O_HOSAFA = 4;
                                                    CopyPeilutToObj(ref oObjPeilutOvdimIns, ref oPeilut);
                                                    oCollPeilutOvdimIns.Add(oObjPeilutOvdimIns);

                                                    clPeilut oPeilutNew = new clPeilut(_iMisparIshi, _dCardDate, oPeilut, oPeilut.lMakatNesia, dtTmpMeafyeneyElements);
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
            clSidur oSidur = new clSidur();
            clPeilut oPeilut = new clPeilut();
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd = null;
            bool bHaveSidurFromMatala = false;
            bool bHaveSidurVisaFromMapa = false;
            int i = 0;
            try
            {
                if (htEmployeeDetails != null)
                    for (i = 0; i < htEmployeeDetails.Count; i++)
                    {
                        oSidur = (clSidur)htEmployeeDetails[i];
                        bHaveSidurVisaFromMapa = false;
                        bHaveSidurFromMatala = false;
                        //סידור מפה: אינו מתחיל בספרות 99/
                        //סידור מיוחד שמקורו במטלה מהמפה (באחת הרשומות של הפעילויות בסידור0< TB_peilut_Ovdim. Mispar_matala)/
                        //סידור ויזה שהגיע מהמפה (בפעילות שהיא מסוג מק"ט 5 קיים ערך בשדה MISPAR_VISA
                        if (oSidur.bSidurMyuhad)
                        {
                            for (int j = 0; j < oSidur.htPeilut.Count; j++)
                            {
                                oPeilut = (clPeilut)oSidur.htPeilut[j];

                                if (oPeilut.lMisparMatala > 0)
                                {
                                    bHaveSidurFromMatala = true;
                                }
                                if (oSidur.bSidurVisaKodExists || oSidur.iSectorVisa == 0 || oSidur.iSectorVisa == 1)
                                {
                                    if (oPeilut.iMakatType == clKavim.enMakatType.mVisa.GetHashCode() && oPeilut.lMisparVisa > 0)
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

        private void CopyPeilutToObj(ref OBJ_PEILUT_OVDIM oObjPeilutOvdimIns,ref clPeilut oPeilut)
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
                clSidur oFirstSidurEilat = null;
                clSidur oSecondSidurEilat = null; 
                clPeilut oFirstPeilutEilat = null;
                clPeilut oSecondPeilutEilat = null;
                clPeilut tmpPeilut = null;

                htEmployeeDetails.Values
                                 .Cast<clSidur>()
                                 .ToList()
                                 .ForEach
                                 (
                                    sidur =>
                                    {
                                        if (sidur.IsLongEilatTrip(_dCardDate, out tmpPeilut, _oParameters.fOrechNesiaKtzaraEilat))
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
                    int firstSidurIndex = htEmployeeDetails.Values.Cast<clSidur>().ToList().FindIndex(sidur => (sidur.iMisparSidur == oFirstSidurEilat.iMisparSidur && sidur.dFullShatHatchala == oFirstSidurEilat.dFullShatHatchala));
                    int secondSidurIndex = htEmployeeDetails.Values.Cast<clSidur>().ToList().FindIndex(sidur => (sidur.iMisparSidur == oSecondSidurEilat.iMisparSidur && sidur.dFullShatHatchala == oSecondSidurEilat.dFullShatHatchala));
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
            clSidur oSidur;
            int iCountSidurim,I;
            try
            {
                iCountSidurim = htEmployeeDetails.Values.Count;
                I = 0;

                if (iCountSidurim > 0)
                {
                    do
                    {
                        oSidur = (clSidur)htEmployeeDetails[I];
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
            clSidur oSidur = null;
            clSidur oSidurNext = null;
            clPeilut oElement, oPeilut;
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
                    oSidur = (clSidur)htEmployeeDetails[I];
                    oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur);
                    for (j = 0; j <= oSidur.htPeilut.Values.Count - 1; j++)
                    {
                        oPeilut = (clPeilut)oSidur.htPeilut[j];
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

                    oSidurNext = (clSidur)htEmployeeDetails[I + 1];

                    //ב.	לוקחים את שני הסידורים הראשונים. מחשבים שעת התחלה של השני פחות שעת גמר של הראשון, אם התוצאה גדולה מ- 1 וגם סה"כ ההמתנה שניתנה עד כה קטנה מהערך בפרמטר 148ובמידה 
                    dPaar = (oSidurNext.dFullShatHatchala - oSidur.dFullShatGmar).TotalMinutes;
                    if (dPaar >= 1 && iSumElement < _oParameters.iMaxZmanHamtanaEilat)
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

                                    clPeilut oPeilutNew = new clPeilut(_iMisparIshi, _dCardDate, oObjPeilutOvdimIns, dtTmpMeafyeneyElements);
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
                                    oElement = new clPeilut(_iMisparIshi, _dCardDate, oElement, oObjPeilutOvdimUpd.MAKAT_NESIA, dtTmpMeafyeneyElements);
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

        private bool IsDuplicateShatYeziaHamtana(clSidur oSidur)
        {
            clPeilut oPeilut;
            DateTime dShatYezia = new DateTime();
            try
            {//בדיקה אם קיימת פעילות בשעת יציאה של פעילות ההמתנה שרוצים להוסיף
                dShatYezia = oSidur.dFullShatGmar;
                for (int i = 0; i <= oSidur.htPeilut.Values.Count - 1; i++)
                {
                    oPeilut = (clPeilut)oSidur.htPeilut[i];
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
        //private void IpusSidurimMevutalimYadanit23()
        //{
        //    clSidur oSidur;
        //    OBJ_SIDURIM_OVDIM oObjSidurUpd;


        //    for (int j = 0; j < _htEmployeeDetailsWithCancled.Values.Count; j++)
        //    {
        //        oSidur = (clSidur)_htEmployeeDetailsWithCancled[j];
        //        if (oSidur.iBitulOHosafa == 1)
        //        {
        //            oObjSidurUpd = new OBJ_SIDURIM_OVDIM();
        //            InsertToObjSidurimOvdimForUpdate(ref oSidur, ref oObjSidurUpd);
        //            oCollSidurimOvdimUpd.Add(oObjSidurUpd);

        //            if (!oObjSidurUpd.PTOR_MEHITIATZVUTIsNull)
        //            {
        //                oObjSidurUpd.PTOR_MEHITIATZVUT = 0;
        //                oObjSidurUpd.UPDATE_OBJECT = 1;
        //            }

        //            if (!oObjSidurUpd.NIDRESHET_HITIATZVUTIsNull)
        //            {
        //                oObjSidurUpd.NIDRESHET_HITIATZVUT = 0;
        //                oObjSidurUpd.UPDATE_OBJECT = 1;
        //            }

        //            if (!oObjSidurUpd.SHAT_HITIATZVUTIsNull)
        //            {
        //                oObjSidurUpd.SHAT_HITIATZVUT = DateTime.MinValue;
        //                oObjSidurUpd.SHAT_HITIATZVUTIsNull = true;
        //                oObjSidurUpd.UPDATE_OBJECT = 1;
        //            }

        //            if (!oObjSidurUpd.HACHTAMA_BEATAR_LO_TAKINIsNull)
        //            {
        //                oObjSidurUpd.HACHTAMA_BEATAR_LO_TAKIN = "";
        //                oObjSidurUpd.UPDATE_OBJECT = 1;
        //            }

        //        }
        //    }
        //}

        private void FixedItyatzvutNahag23(int iIndexSidur, ref clSidur oSidur,ref clSidur oSidurNidrashHityazvut, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, ref bool bFirstHayavHityazvut, ref bool bSecondHayavHityazvut)
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
                    clSidur oSidurPrev = (clSidur)htEmployeeDetails[iIndexSidur - 1];
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

        private bool CheckHovatHityazvut(clSidur oSidur,ref  OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd,int iHityazvut,ref bool  bHayavHityazvut,bool bHaveIdkunRashamet)
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
                    foreach (clPeilut oPeilut in oSidur.htPeilut.Values)
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

        private void CheckHityazvut(clSidur oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
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
                              
                    foreach (clSidur oSidurHityatvut in _htSpecialEmployeeDetails.Values)
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

        private int CheckPtorHityatzvutTnua(clSidur oSidur, string sMikumShaonKnisa)
        {
            clKavim oKavim = new clKavim();
            int iKodHityazvut = -1;
            int iNekudatTziyunPeilut;
            clPeilut oPeilutRishona=null;
            try
            {
                if (oSidur.htPeilut.Count > 0)
                {
                    for (int i = 0; i < oSidur.htPeilut.Count ; i++)
                    {
                        oPeilutRishona = (clPeilut)oSidur.htPeilut[i];
                        if (oPeilutRishona.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && oPeilutRishona.lMakatNesia.ToString().PadLeft(8).Substring(0, 3)!="700")
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

                        if (oPeilutRishona.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode() || oPeilutRishona.iMakatType == clKavim.enMakatType.mEmpty.GetHashCode() || oPeilutRishona.iMakatType == clKavim.enMakatType.mNamak.GetHashCode())
                        {
                            iNekudatTziyunPeilut = oPeilutRishona.iXyMokedTchila;

                            if (sMikumShaonKnisa.Length >= 5)
                            {

                                iKodHityazvut = oKavim.CheckHityazvutNehag(iNekudatTziyunPeilut, _oParameters.iRadyusMerchakMeshaonLehityazvut, int.Parse(oPeilutRishona.sSnifTnua), int.Parse(sMikumShaonKnisa.Substring(0, 3)), int.Parse(sMikumShaonKnisa.Substring(3, 2)));
                            }
                            else { iKodHityazvut = oKavim.CheckHityazvutNehag(iNekudatTziyunPeilut, _oParameters.iRadyusMerchakMeshaonLehityazvut, int.Parse(oPeilutRishona.sSnifTnua), null, null); }
                        }
                        else if (oPeilutRishona.iMakatType == clKavim.enMakatType.mElement.GetHashCode())
                        {
                            if (sMikumShaonKnisa.Length >= 5)
                            {
                                iKodHityazvut = oKavim.CheckHityazvutNehag(null, _oParameters.iRadyusMerchakMeshaonLehityazvut, int.Parse(oPeilutRishona.sSnifTnua), int.Parse(sMikumShaonKnisa.Substring(0, 3)), int.Parse(sMikumShaonKnisa.Substring(3, 2)));
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

        private void FixedShatHatchalaLefiShatHachtamatItyatzvut12(ref clSidur oSidur, int iSidurIndex, DateTime dShatHatchalaNew, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            OBJ_PEILUT_OVDIM oObjPeilutUpd;
            clPeilut oPeilut;
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
                        oPeilut = (clPeilut)oSidur.htPeilut[j];
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

        private void FixedShatHatchalaLefiShatHachtamatItyatzvut12(ref int j, ref clPeilut oPeilut, ref clSidur oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, ref OBJ_PEILUT_OVDIM oObjPeilutOvdimUpd, ref bool bUpdateShatHatchala, ref DateTime dShatHatchalaNew,SourceObj SourceObject)
        {
             string sTempTime, sNewTempTime;
            clPeilut oNextPeilut = null;
            int iTempTime;
            sTempTime = "";
            Double dZmanLekizuz = 0;
            int i,iCountInsPeiluyot;
            try
            {
                if (oSidur.htPeilut.Count > j+1)
                {
                    oNextPeilut = (clPeilut)oSidur.htPeilut[j+1];
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
                        if (oPeilut.iMakatType == clKavim.enMakatType.mEmpty.GetHashCode() || ((oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && oPeilut.bBitulBiglalIchurLasidurExists)))
                        {
                            bUpdateShatHatchala = true;
                            if (oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && oPeilut.bBitulBiglalIchurLasidurExists)
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
                                if (oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && oPeilut.bBitulBiglalIchurLasidurExists)
                                {
                                    sNewTempTime = (iTempTime - dZmanLekizuz).ToString().PadLeft(3, (char)48);
                                    oObjPeilutOvdimUpd.MAKAT_NESIA = long.Parse(oObjPeilutOvdimUpd.MAKAT_NESIA.ToString().Replace(sTempTime, sNewTempTime));
                                    oPeilut = new clPeilut(_iMisparIshi, _dCardDate, oPeilut, oObjPeilutOvdimUpd.MAKAT_NESIA, dtTmpMeafyeneyElements);
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

        private void FixedEggedTaavura22(ref clSidur oSidur,ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            try
            {
                if (oOvedYomAvodaDetails.iKodHevra == clGeneral.enEmployeeType.enEggedTaavora.GetHashCode() &&
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

        private void FixedShatHatchalaForSidurVisa21(ref clSidur oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
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
                    if (oSidur.dFullShatHatchalaLetashlum.Year < clGeneral.cYearNull)
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
                    if (oSidur.dFullShatHatchalaLetashlum.Year < clGeneral.cYearNull)
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

        private void FixedShatGmar10(ref clSidur oSidur, int iIndexSidur, ref  bool bUsedMazanTichnun, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            DateTime dShatGmar;
            clPeilut oLastPeilutMashmautit=null;
            int i;
            clPeilut oPeilut;
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
                            oLastPeilutMashmautit = oSidur.htPeilut.Values.Cast<clPeilut>().ToList().LastOrDefault(Peilut => (Peilut.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode() || Peilut.iMakatType == clKavim.enMakatType.mNamak.GetHashCode() || (Peilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && (Peilut.iElementLeShatGmar > 0 || Peilut.iElementLeShatGmar == -1))));

                            //. קיימת פעילות משמעותית אחרונה (או יחידה):
                            if (oLastPeilutMashmautit!=null)
                            {
                                for (i = oSidur.htPeilut.Values.Count - 1; i >= 0; i--)
                                {
                                     oPeilut = (clPeilut)oSidur.htPeilut[i];
                                    if ((oPeilut.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode() && oPeilut.iMisparKnisa==0) || oPeilut.iMakatType == clKavim.enMakatType.mNamak.GetHashCode() || (oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && (oPeilut.iElementLeShatGmar > 0 || oPeilut.iElementLeShatGmar == -1)))
                                    {
                                        dShatGmar = oPeilut.dFullShatYetzia.AddMinutes(GetMeshechPeilut(iIndexSidur, oPeilut, oSidur, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur) + iMeshechPeilut);
                                        break;           
                                    }
                                    if ((oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && oPeilut.iErechElement == 1 && string.IsNullOrEmpty(oPeilut.sLoNitzbarLishatGmar)) || (oPeilut.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode() && oPeilut.iMisparKnisa > 0) || oPeilut.iMakatType == clKavim.enMakatType.mEmpty.GetHashCode()) 
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
                                    oPeilut = (clPeilut)oSidur.htPeilut[i];
                                   if ((oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && oPeilut.iErechElement == 1) || oPeilut.iMakatType == clKavim.enMakatType.mEmpty.GetHashCode())
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

        private int GetMeshechPeilut(int iIndexSidur, clPeilut oPeilut, clSidur oSidur, ref bool bUsedMazanTichnun, ref bool bUsedMazanTichnunInSidur)
        {
            int iMeshech = 0;
            DataRow[] drSugSidur;
             bool bSidurNahagutNext;

            drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, _dCardDate, _dtSugSidur);
            bool bSidurNahagut=IsSidurNahagut(drSugSidur, oSidur);
            if (htEmployeeDetails.Count > iIndexSidur + 1)
            {
                drSugSidur = clDefinitions.GetOneSugSidurMeafyen(((clSidur)htEmployeeDetails[iIndexSidur + 1]).iSugSidurRagil, _dCardDate, _dtSugSidur);
                bSidurNahagutNext = IsSidurNahagut(drSugSidur, ((clSidur)htEmployeeDetails[iIndexSidur + 1]));
            }
            else bSidurNahagutNext = false;

            //אם הערך בשדה 0<Dakot_bafoal אז יש לקחת את הערך משדה זה  
            if (oPeilut.iDakotBafoal > 0 || (oPeilut.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode() && oPeilut.iMisparKnisa>0))
            {
                iMeshech = oPeilut.iDakotBafoal;
            }
            else
            {
                //משך פעילות מסוג אלמנט  - הערך בפוזיציות 4-6 של המק"ט. 
                if (oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                {
                    iMeshech = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));
                }
               else
                { //סידור שהוא יחיד או אחרון ביום
                    //סידור אחרון צריך להיגמר לפי הגדרה לתכנון.

                    if (htEmployeeDetails.Values.Count == 1 || (htEmployeeDetails.Count - 1) == iIndexSidur || (bSidurNahagut && !bSidurNahagutNext))
                    {
                         //אלמנט ריקה  (מאפיין 23): מז"ן תשלום (גמר) - הערך שהוקלד במק"ט. מז"ן תכנון  - מז"ן תשלום * פרמטר 43.
                        if (oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && !string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                            iMeshech = int.Parse(Math.Round(oPeilut.iMazanTashlum * oParam.fFactorNesiotRekot).ToString());
                         else
                            iMeshech = oPeilut.iMazanTichnun;
                    }
                    else if (bSidurNahagut && bSidurNahagutNext)
                    {
                        //סידור שאינו יחיד או אחרון ביום צריך להיגמר לפי זמן לגמר או לתכנון, בהתאם למקרה:
                        clSidur oNextSidur = (clSidur)htEmployeeDetails[iIndexSidur + 1];

                        //1.	אם יש פער של עד 60 דקות משעת התחלת הסידור הבא - יש לחשב לפי הגדרה גמר (תשלום) 
                        if (oNextSidur.dFullShatHatchala.Subtract(oSidur.dFullShatGmar).TotalMinutes <= 60)
                        {
                            //אלמנט ריקה  (מאפיין 23): מז"ן תשלום (גמר) - הערך שהוקלד במק"ט. מז"ן תכנון  - מז"ן תשלום * פרמטר 43.
                            if (oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && !string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                               iMeshech = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));
                            else
                                iMeshech = oPeilut.iMazanTashlum;
                        }
                        else
                        {
                            if (!bUsedMazanTichnun)
                            {
                                //אלמנט ריקה  (מאפיין 23): מז"ן תשלום (גמר) - הערך שהוקלד במק"ט. מז"ן תכנון  - מז"ן תשלום * פרמטר 43.
                                if (oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && !string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                                    iMeshech = int.Parse(Math.Round(oPeilut.iMazanTashlum * oParam.fFactorNesiotRekot).ToString());
                                 else
                                    //2.	אם יש פער גדול מ- 60 דקות משעת התחלה של הסידור הבא וזו הפעם הראשונה שסידור שאינו יחיד/אחרון ביום צריך להיגמר לפי הגדרה לתכנון   - יש לחשב לפי זמן לתכנון. 
                                    iMeshech = oPeilut.iMazanTichnun;
                                bUsedMazanTichnunInSidur = true;

                            }
                            else
                            {
                                //אלמנט ריקה  (מאפיין 23): מז"ן תשלום (גמר) - הערך שהוקלד במק"ט. מז"ן תכנון  - מז"ן תשלום * פרמטר 43.
                                if (oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && !string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
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

        private int GetMeshechPeilutHachnatMechona(int iIndexSidur, clPeilut oPeilut, clSidur oSidur, ref bool bUsedMazanTichnun, ref bool bUsedMazanTichnunInSidur)
        {
            int iMeshech = 0;
            DataRow[] drSugSidur;
            bool bSidurNahagutPrev;

            drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, _dCardDate, _dtSugSidur);
            bool bSidurNahagut = IsSidurNahagut(drSugSidur, oSidur);
            if (iIndexSidur > 0)
            {
                drSugSidur = clDefinitions.GetOneSugSidurMeafyen(((clSidur)htEmployeeDetails[iIndexSidur - 1]).iSugSidurRagil, _dCardDate, _dtSugSidur);
                bSidurNahagutPrev = IsSidurNahagut(drSugSidur, ((clSidur)htEmployeeDetails[iIndexSidur - 1]));
            }
            else bSidurNahagutPrev = false;

            //אם הערך בשדה 0<Dakot_bafoal אז יש לקחת את הערך משדה זה  
            if (oPeilut.iDakotBafoal > 0 || (oPeilut.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode() && oPeilut.iMisparKnisa > 0))
            {
                iMeshech = oPeilut.iDakotBafoal;
            }
            else
            {
                //משך פעילות מסוג אלמנט  - הערך בפוזיציות 4-6 של המק"ט. 
                if (oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                {
                    iMeshech = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));
                }
                else
                { //סידור שהוא יחיד או ראשון ביום
                    //סידור ראשון צריך להיגמר לפי הגדרה לתכנון.

                    if ((iIndexSidur == 0) || htEmployeeDetails.Values.Count == 1 || (bSidurNahagut && !bSidurNahagutPrev))
                    {
                        //אלמנט ריקה  (מאפיין 23): מז"ן תשלום (גמר) - הערך שהוקלד במק"ט. מז"ן תכנון  - מז"ן תשלום * פרמטר 43.
                        if (oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && !string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                            iMeshech = int.Parse(Math.Round(oPeilut.iMazanTashlum * oParam.fFactorNesiotRekot).ToString());
                        else
                            iMeshech = oPeilut.iMazanTichnun;
                    }
                    else if (bSidurNahagut && bSidurNahagutPrev)
                    {
                        //סידור שאינו יחיד או ראשון ביום צריך להיגמר לפי זמן לגמר או לתכנון, בהתאם למקרה:
                        clSidur oPrevSidur = (clSidur)htEmployeeDetails[iIndexSidur - 1];

                        //1.	אם יש פער של עד 60 דקות משעת התחלת הסידור הבא - יש לחשב לפי הגדרה גמר (תשלום) 
                        if (oSidur.dFullShatHatchala.Subtract(oPrevSidur.dFullShatGmar).TotalMinutes <= 60)
                        {
                            //אלמנט ריקה  (מאפיין 23): מז"ן תשלום (גמר) - הערך שהוקלד במק"ט. מז"ן תכנון  - מז"ן תשלום * פרמטר 43.
                            if (oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && !string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                                iMeshech = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));
                            else
                                iMeshech = oPeilut.iMazanTashlum;
                        }
                        else
                        {
                            if (!bUsedMazanTichnun)
                            {
                                //אלמנט ריקה  (מאפיין 23): מז"ן תשלום (גמר) - הערך שהוקלד במק"ט. מז"ן תכנון  - מז"ן תשלום * פרמטר 43.
                                if (oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && !string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                                    iMeshech = int.Parse(Math.Round(oPeilut.iMazanTashlum * oParam.fFactorNesiotRekot).ToString());
                                else
                                    //2.	אם יש פער גדול מ- 60 דקות משעת התחלה של הסידור הבא וזו הפעם הראשונה שסידור שאינו יחיד/ראשון ביום צריך להיגמר לפי הגדרה לתכנון   - יש לחשב לפי זמן לתכנון. 
                                    iMeshech = oPeilut.iMazanTichnun;
                                bUsedMazanTichnunInSidur = true;

                            }
                            else
                            {
                                //אלמנט ריקה  (מאפיין 23): מז"ן תשלום (גמר) - הערך שהוקלד במק"ט. מז"ן תכנון  - מז"ן תשלום * פרמטר 43.
                                if (oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && !string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
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

        private void FixedShaotForSidurWithOneNesiaReeka04(int iCurSidurIndex, ref clSidur oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            DateTime dShatYetzia = DateTime.MinValue;
            int iMisparKnisa = 0;
            OBJ_PEILUT_OVDIM oObjPeilutUpd;
            clNewSidurim oNewSidurim=null;
            SourceObj SourceObject;
            clPeilut oPeilut;
            bool bHaveSidurFromMatala = false;
            bool bHavePeilutReka = false;
            bool bHaveElementHachanatMechona = false;
            bool bHaveElementLoMashmauti = false;
            int iMazanTichnun = 0,iDakot=0;
            bool bContinue = true, flag = false;
            clSidur nextSidur, prevSidur;
            if (oSidur.htPeilut.Count <= 2)
            {
                for (int i = 0; i < oSidur.htPeilut.Count; i++)
                {
                    oPeilut = (clPeilut)oSidur.htPeilut[i];
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
                    if (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == KdsLibrary.clGeneral.enElementHachanatMechona.Element701.GetHashCode().ToString() || oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == KdsLibrary.clGeneral.enElementHachanatMechona.Element712.GetHashCode().ToString() || oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == KdsLibrary.clGeneral.enElementHachanatMechona.Element711.GetHashCode().ToString())
                    {
                        bHaveElementHachanatMechona = true;
                    }
                    if (oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && oPeilut.iElementLeShatGmar == 0)
                    {
                        bHaveElementLoMashmauti = true;
                    }
                }
                    if ((!oSidur.bSidurMyuhad || bHaveSidurFromMatala) && ((bHavePeilutReka && oSidur.htPeilut.Count == 1) || (bHavePeilutReka && (bHaveElementHachanatMechona || bHaveElementLoMashmauti))))
                    {
                    nextSidur = null; prevSidur = null;
                    if(iCurSidurIndex>0)
                        prevSidur = htEmployeeDetails[iCurSidurIndex - 1] as clSidur;
                    if (iCurSidurIndex < (htEmployeeDetails.Count - 1))
                        nextSidur = htEmployeeDetails[iCurSidurIndex + 1] as clSidur;
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
                                    oPeilut = (clPeilut)oSidur.htPeilut[j];

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
            clSidur oSidur = new clSidur();
            int iLina = 0;
           int iCountLina=0;
            try
            {
                
                if (htEmployeeDetails != null)
                {
                    for (int i = 0; i < htEmployeeDetails.Count; i++)
                    {
                        oSidur = (clSidur)htEmployeeDetails[i];

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

        //private void UpdatePeiluyotMevutalotYadani(int iCurSidurIndex,clNewSidurim oNewSidurim, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        //{
        //    clPeilut oPeilut;
        //    clSidur oSidurWithCancled;
        //    OBJ_PEILUT_OVDIM oObjPeilutUpd;
          
        //    oSidurWithCancled = _htEmployeeDetailsWithCancled.Values
        //                         .Cast<clSidur>()
        //                         .ToList()
        //                        .Find(sidur => (sidur.iMisparSidur == oNewSidurim.SidurOld && sidur.dFullShatHatchala == oNewSidurim.ShatHatchalaOld));
        //    if (oSidurWithCancled != null)
        //    {
        //        for (int j = 0; j < oSidurWithCancled.htPeilut.Count; j++)
        //        {
        //            oPeilut = (clPeilut)oSidurWithCancled.htPeilut[j];
        //            if (oPeilut.iBitulOHosafa == 1)
        //            {
        //                oObjPeilutUpd = GetUpdPeilutObjectCancel(iCurSidurIndex, oPeilut, oObjSidurimOvdimUpd);
        //                oObjPeilutUpd.NEW_SHAT_HATCHALA_SIDUR = oNewSidurim.ShatHatchalaNew;
        //                oObjPeilutUpd.UPDATE_OBJECT = 1;
        //              }
        //        }
        //    }
        //}
               
        private void UpdateZmaneyNesia20()
        {
            int iZmanNesia = 0;
            try
            {
                if (oMeafyeneyOved.Meafyen51Exists)
                {
                    if (!String.IsNullOrEmpty(oMeafyeneyOved.sMeafyen51))
                    {
                        iZmanNesia = int.Parse(oMeafyeneyOved.sMeafyen51.Substring(1));
                        switch (int.Parse(oMeafyeneyOved.sMeafyen51.Substring(0, 1)))
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
           // clPeilut oPeilut;
           // clSidur oSidur;
            bool bExist = false;
            try
            {
                //נמצא את הפעילות באובייקט פעילויות לעדכון
               
                for (int i = 0; i <= oCollSidurimOvdimDel.Count - 1; i++)
                {
                   // oSidur = (clSidur)htEmployeeDetails[i];
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

        private void InsertToObjSidurimOvdimForInsert(ref clSidur oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimIns)
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
        private void InsertToObjSidurimOvdimForUpdate(ref clSidur oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
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
        private void InsertToObjSidurimOvdimForDelete(ref clSidur oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimDel)
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
        private void InsertToObjPeilutOvdimForInsert(ref clSidur oSidur, ref OBJ_PEILUT_OVDIM oObjPeilutOvdimIns)
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
        private void InsertToObjPeilutOvdimForUpdate(ref clPeilut oPeilut,  OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, ref OBJ_PEILUT_OVDIM oObjPeilutOvdimUpd)
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
        private void InsertToObjPeilutOvdimForDelete(ref clPeilut oPeilut, ref clSidur oSidur, ref OBJ_PEILUT_OVDIM oObjPeilutOvdimDel)
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
        private void InsertToYameyAvodaForUpdate(DateTime dCardDate, ref OBJ_YAMEY_AVODA_OVDIM oObjYameyAvodaUpd, ref clOvedYomAvoda oOvedYomAvodaDetails)
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
            clSidur oSidur=null;
            bool bSidurKonnutGrira = false;
            int iSidurKonnutGrira = 0;
            clSidur oSidurKonenutGrira =null;
             DataTable dtSidurGrira=new DataTable();
            int iTypeGrira,i;
            int iGriraNum=0;
            int iNumSidur2 = 0;
            clSidur oSidurGrira=null;
            OBJ_SIDURIM_OVDIM oObjSidurGriraUpd=null;
            OBJ_SIDURIM_OVDIM oObjSidurimConenutGriraUpd;
            try
            {
                if (htEmployeeDetails != null)
                {
                    //נעבור על כל הסידורים ונבדוק שיש כוננות גרירה וכוננות גרירה בפועל
                    for ( i = 0; i < htEmployeeDetails.Count; i++)
                    {
                        oSidur = (clSidur)htEmployeeDetails[i];

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
                            oSidurGrira = (clSidur)htEmployeeDetails[j];

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
                                         oSidurKonenutGrira = (clSidur)htEmployeeDetails[iSidurKonnutGrira];
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
                                           // SetBitulZmanNesiot(ref oObjSidurGriraUpd, oSidurKonenutGrira, oSidurGrira);

                                            SetZmanHashlama(ref oObjSidurGriraUpd, oSidurKonenutGrira, iGriraNum, oSidurGrira);
                                        }
                                    }
                                    
                                    break;
                                }
                            }


                            //בכל המקומות בהם מחפשים סידור גרירה בפועל בטווח הזמן של כוננות הגרירה וסידור כוננות הגרירה מתחיל לפני חצות ומסתיים אחרי חצות, 
                            if (bSidurKonnutGrira && oSidur.dFullShatHatchala < clGeneral.GetDateTimeFromStringHour("24:00", _dCardDate) && oSidur.dFullShatGmar > clGeneral.GetDateTimeFromStringHour("24:00", _dCardDate))
                            {
                                // יש לחפש סידור גרירה בתאריך כרטיס העבודה ובתאריך כרטיס העבודה +1.
                                if (CheckHaveSidurGrira(_iMisparIshi, _dCardDate.AddDays(1), ref dtSidurGrira))
                                {
                                    for (i = 0; i < dtSidurGrira.Rows.Count; i++)
                                    {
                                        oSidurGrira = new clSidur(dtSidurGrira.Rows[i]);

                                        if (IsActualKonnutGrira(ref oSidurGrira, iSidurKonnutGrira, out iTypeGrira))
                                        {
                                            oObjSidurGriraUpd = new OBJ_SIDURIM_OVDIM();
                                            InsertToObjSidurimOvdimForUpdate(ref oSidurGrira, ref oObjSidurGriraUpd);
                                            oCollSidurimOvdimUpd.Add(oObjSidurGriraUpd);

                                            iGriraNum += 1;

                                            //אם יש סידור כוננות גרירה  וגם לפחות סידור גרירה בפועל אחד 
                                            if ((bSidurKonnutGrira) && (oObjSidurGriraUpd != null))
                                            {
                                                 oSidurKonenutGrira = (clSidur)htEmployeeDetails[iSidurKonnutGrira];
                                                if (!CheckIdkunRashemet("LO_LETASHLUM", oSidurKonenutGrira.iMisparSidur, oSidurKonenutGrira.dFullShatHatchala))
                                                {
                                                    SetLoLetashlum(oSidurKonenutGrira);
                                                }

                                                if (iTypeGrira == 2)
                                                    SetShatGmarGrira(ref oObjSidurGriraUpd, oSidurKonenutGrira, oSidurGrira);
                                                if (iTypeGrira == 1)
                                                {
                                                    //SetBitulZmanNesiot(ref oObjSidurGriraUpd, oSidurKonenutGrira, oSidurGrira);

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
                        oSidurKonenutGrira = (clSidur)htEmployeeDetails[iSidurKonnutGrira];
                        oObjSidurimConenutGriraUpd = GetSidurOvdimObject(oSidurKonenutGrira.iMisparSidur, oSidurKonenutGrira.dFullShatHatchala);
                        if (oObjSidurimConenutGriraUpd.LO_LETASHLUM == 0)
                        {
                            oObjSidurimConenutGriraUpd.UPDATE_OBJECT = 1;
                            Minutes = float.Parse((oSidurKonenutGrira.dFullShatGmar - oSidurKonenutGrira.dFullShatHatchala).TotalMinutes.ToString());
                            iNumSidur2 = int.Parse(oSidurKonenutGrira.iMisparSidur.ToString().PadLeft(5, '0').Substring(0, 2));
                            if (Minutes > _oParameters.iMinZmanGriraDarom)
                            {
                                if (iNumSidur2 >= 25 || iNumSidur2 == 4 || (iNumSidur2 == 22 && 
                                    (oSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString() || oSidur.sErevShishiChag == "1" || oSidur.sSidurDay == clGeneral.enDay.Shabat.GetHashCode().ToString() || oSidur.sShabaton == "1")))
                                {
                                    oObjSidurimConenutGriraUpd.SHAT_HATCHALA_LETASHLUM = oSidurKonenutGrira.dFullShatHatchala;
                                    oObjSidurimConenutGriraUpd.SHAT_GMAR_LETASHLUM = oSidurKonenutGrira.dFullShatHatchala.AddMinutes(_oParameters.iMinZmanGriraDarom);
                                }
                                else if (iNumSidur2 < 25 && (iNumSidur2 == 1 || iNumSidur2 == 22) && (oSidur.sSidurDay != clGeneral.enDay.Shabat.GetHashCode().ToString() &&
                                                oSidur.sSidurDay != clGeneral.enDay.Shishi.GetHashCode().ToString() &&
                                                !oSidur.sErevShishiChag.Equals("1") && !oSidur.sShabaton.Equals("1")))
                                {
                                    oObjSidurimConenutGriraUpd.SHAT_HATCHALA_LETASHLUM = oSidurKonenutGrira.dFullShatHatchala;
                                    oObjSidurimConenutGriraUpd.SHAT_GMAR_LETASHLUM = oSidurKonenutGrira.dFullShatHatchala.AddMinutes(_oParameters.iMinZmanGriraTzafon);
                                }
                            }
                            else if (Minutes > _oParameters.iMinZmanGriraTzafon && Minutes <= _oParameters.iMinZmanGriraDarom)
                            {
                                if (iNumSidur2 >= 25 || iNumSidur2 == 4 || (iNumSidur2 == 22 &&
                                     (oSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString() || oSidur.sErevShishiChag == "1" || oSidur.sSidurDay == clGeneral.enDay.Shabat.GetHashCode().ToString() || oSidur.sShabaton == "1")))
                                {
                                    oObjSidurimConenutGriraUpd.SHAT_HATCHALA_LETASHLUM = oSidurKonenutGrira.dFullShatHatchala;
                                    oObjSidurimConenutGriraUpd.SHAT_GMAR_LETASHLUM = oSidurKonenutGrira.dFullShatGmar;
                                }
                                else if (iNumSidur2 < 25 && (iNumSidur2 == 1 || iNumSidur2 == 22) && (oSidur.sSidurDay != clGeneral.enDay.Shabat.GetHashCode().ToString() &&
                                                oSidur.sSidurDay != clGeneral.enDay.Shishi.GetHashCode().ToString() &&
                                                !oSidur.sErevShishiChag.Equals("1") && !oSidur.sShabaton.Equals("1")))
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


        //private void AddElementMechine05()
        //{
        //    clSidur oSidur;
        //    int iNumHachanotMechona = 0;
        //    int iNumHachanotMechonaForSidur = 0;
        //    OBJ_PEILUT_OVDIM oObjPeilutUpd, oObjPeilutDel;
        //    clPeilut oPeilut;
        //    DateTime dShatYetzia, dShatYetziaFirst;
        //    int iCountPeiluyot;
        //    int j, iIndexElement;
        //    OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
        //    iIndexElement = -1;
        //    SourceObj SourceObject;
        //    int iIndexFirstElementMachine = 0;
        //    bool bHaveFirstElementMachine = false;
        //    int i, iCountPeiluyotIns, l;
        //    bool bUsedMazanTichnun = false;
        //    bool bUsedMazanTichnunInSidur = false;
        //    //הוספת אלמנט הכנת מכונה אם העובד החליף רכב ולא מופיע אלמנט זה או בתחילת יום
        //    try
        //    {
        //        //מחיקת כל פעילויות הכנת מכונה 
        //        for (i = 0; i < htEmployeeDetails.Count; i++)
        //        {
        //            oSidur = (clSidur)htEmployeeDetails[i];
        //            j = 0;
        //            iCountPeiluyot = oSidur.htPeilut.Count;
        //            if (j < iCountPeiluyot)
        //            {
        //                do
        //                {
        //                    oPeilut = (clPeilut)oSidur.htPeilut[j];
        //                    if (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == KdsLibrary.clGeneral.enElementHachanatMechona.Element701.GetHashCode().ToString() || oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == KdsLibrary.clGeneral.enElementHachanatMechona.Element712.GetHashCode().ToString() || oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == KdsLibrary.clGeneral.enElementHachanatMechona.Element711.GetHashCode().ToString())
        //                    {
        //                        oObjPeilutDel = new OBJ_PEILUT_OVDIM();
        //                        InsertToObjPeilutOvdimForDelete(ref oPeilut, ref oSidur, ref oObjPeilutDel);
        //                        oObjPeilutDel.BITUL_O_HOSAFA = 3;// int.Parse(clGeneral.enBitulOHosafa.BitulAutomat.ToString());
        //                        oCollPeilutOvdimDel.Add(oObjPeilutDel);

        //                        oSidur.htPeilut.RemoveAt(j);
        //                        j -= 1;
        //                        for (l = 0; l <= oCollPeilutOvdimUpd.Count - 1; l++)
        //                        {
        //                            if ((oCollPeilutOvdimUpd.Value[l].NEW_MISPAR_SIDUR == oSidur.iMisparSidur) && (oCollPeilutOvdimUpd.Value[l].NEW_SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
        //                                && (oCollPeilutOvdimUpd.Value[l].NEW_SHAT_YETZIA == oPeilut.dFullShatYetzia) && (oCollPeilutOvdimUpd.Value[l].MISPAR_KNISA == oPeilut.iMisparKnisa))
        //                            {
        //                                //oCollPeilutOvdimUpd.Value[l].UPDATE_OBJECT = 0;
        //                                oCollPeilutOvdimUpd.RemoveAt(l);
        //                            }
        //                        }
        //                        l = 0;
        //                        iCountPeiluyotIns = oCollPeilutOvdimIns.Count;
        //                        if (l < iCountPeiluyotIns)
        //                        {
        //                            do
        //                            {
        //                                if ((oCollPeilutOvdimIns.Value[l].MISPAR_SIDUR == oSidur.iMisparSidur) && (oCollPeilutOvdimIns.Value[l].SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
        //                                       && (oCollPeilutOvdimIns.Value[l].SHAT_YETZIA == oPeilut.dFullShatYetzia) && (oCollPeilutOvdimIns.Value[l].MISPAR_KNISA == oPeilut.iMisparKnisa))
        //                                {
        //                                    oCollPeilutOvdimIns.RemoveAt(l);
        //                                    l -= 1;
        //                                }
        //                                l += 1;
        //                                iCountPeiluyotIns = oCollPeilutOvdimIns.Count;
        //                            } while (l < iCountPeiluyotIns);
        //                        }
        //                    }
        //                    j += 1;
        //                    iCountPeiluyot = oSidur.htPeilut.Count;
        //                } while (j < iCountPeiluyot);
        //            }

        //        }

        //        for (i = 0; i < htEmployeeDetails.Count; i++)
        //        {
        //            oSidur = (clSidur)htEmployeeDetails[i];
        //            dShatYetzia = oSidur.dFullShatHatchala;
        //            dShatYetziaFirst = oSidur.dFullShatHatchala;
        //            oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur);
        //            iNumHachanotMechonaForSidur = 0;
        //            iIndexFirstElementMachine = 0;
        //            bHaveFirstElementMachine = false;
        //            //אם סידור ראשון ביום קיימים שני מקרים
        //            if (iNumHachanotMechona == 0)
        //            {
        //                AddElementMachineForFirstSidur(ref oSidur, i, ref dShatYetziaFirst, ref iNumHachanotMechona, ref iNumHachanotMechonaForSidur, ref iIndexElement, ref iIndexFirstElementMachine, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur, ref oObjSidurimOvdimUpd);
        //                iIndexFirstElementMachine += 1;
        //                if (iIndexElement == 0) bHaveFirstElementMachine = true;
        //            }

        //            AddElementMachineForNextSidur(ref oSidur, ref dShatYetzia, i, iIndexFirstElementMachine, ref iNumHachanotMechona, ref iNumHachanotMechonaForSidur, ref iIndexElement, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur, ref oObjSidurimOvdimUpd);
        //            if (!bHaveFirstElementMachine)
        //            {
        //                dShatYetziaFirst = dShatYetzia;
        //            }
        //            htEmployeeDetails[i] = oSidur;
        //            if (bUsedMazanTichnunInSidur)
        //                bUsedMazanTichnun = true;
        //            if (dShatYetziaFirst != oSidur.dFullShatHatchala && (!CheckIdkunRashemet("SHAT_HATCHALA", oSidur.iMisparSidur, oSidur.dFullShatHatchala)))
        //            {

        //                clNewSidurim oNewSidurim = FindSidurOnHtNewSidurim(oSidur.iMisparSidur, oSidur.dFullShatHatchala);

        //                oNewSidurim.SidurIndex = i;
        //                oNewSidurim.ShatHatchalaNew = dShatYetziaFirst;
        //                oNewSidurim.SidurNew = oSidur.iMisparSidur;


        //                UpdateObjectUpdSidurim(oNewSidurim);
        //                for (j = 0; j < oSidur.htPeilut.Count; j++)
        //                {
        //                    oPeilut = (clPeilut)oSidur.htPeilut[j];

        //                    if (!CheckPeilutObjectDelete(i, j))
        //                    {
        //                        oObjPeilutUpd = GetUpdPeilutObject(i, oPeilut, out SourceObject, oObjSidurimOvdimUpd);
        //                        if (SourceObject == SourceObj.Insert)
        //                        {
        //                            oObjPeilutUpd.SHAT_HATCHALA_SIDUR = oNewSidurim.ShatHatchalaNew;
        //                        }
        //                        else
        //                        {
        //                            oObjPeilutUpd.NEW_SHAT_HATCHALA_SIDUR = oNewSidurim.ShatHatchalaNew;
        //                            oObjPeilutUpd.UPDATE_OBJECT = 1;
        //                        }
        //                    }

        //                }
        //                //UpdatePeiluyotMevutalotYadani(i, oNewSidurim, oObjSidurimOvdimUpd);
        //                UpdateIdkunRashemet(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oNewSidurim.ShatHatchalaNew);
        //                UpdateApprovalErrors(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oNewSidurim.ShatHatchalaNew);

        //                oSidur.dFullShatHatchala = oNewSidurim.ShatHatchalaNew;
        //                oSidur.sShatHatchala = oSidur.dFullShatHatchala.ToString("HH:mm");
        //                oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA = oNewSidurim.ShatHatchalaNew;

        //            }
        //            else if (dShatYetziaFirst < oSidur.dFullShatHatchala && (CheckIdkunRashemet("SHAT_HATCHALA", oSidur.iMisparSidur, oSidur.dFullShatHatchala)))
        //            {
        //                int iMinuts;
        //                for (j = 0; j < oSidur.htPeilut.Count; j++)
        //                {
        //                    oPeilut = (clPeilut)oSidur.htPeilut[j];

        //                    if (oPeilut.dFullShatYetzia == dShatYetziaFirst)
        //                    {
        //                        iMinuts = int.Parse((oSidur.dFullShatHatchala - dShatYetziaFirst).TotalMinutes.ToString());

        //                        if (int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3)) > iMinuts)
        //                        {
        //                            oObjPeilutUpd = GetUpdPeilutObject(i, oPeilut, out SourceObject, oObjSidurimOvdimUpd);

        //                            FindDuplicatPeiluyot(j, oSidur.dFullShatHatchala, i, ref oSidur, ref oObjSidurimOvdimUpd);
        //                            oObjPeilutUpd.SHAT_YETZIA = oSidur.dFullShatHatchala;

        //                            oPeilut.dFullShatYetzia = oObjPeilutUpd.SHAT_YETZIA;
        //                            oPeilut.sShatYetzia = oPeilut.dFullShatYetzia.ToString("HH:mm");
        //                            iMinuts = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3)) - iMinuts;
        //                            oObjPeilutUpd.MAKAT_NESIA = long.Parse(string.Concat(oPeilut.lMakatNesia.ToString().Substring(0, 3), iMinuts.ToString().PadLeft(3, (char)48), oPeilut.lMakatNesia.ToString().Substring(6, 2)));
        //                        }
        //                        else
        //                        {
        //                            oSidur.htPeilut.RemoveAt(j);
        //                            l = 0;
        //                            iCountPeiluyotIns = oCollPeilutOvdimIns.Count;
        //                            if (l < iCountPeiluyotIns)
        //                            {
        //                                do
        //                                {
        //                                    if ((oCollPeilutOvdimIns.Value[l].MISPAR_SIDUR == oSidur.iMisparSidur) && (oCollPeilutOvdimIns.Value[l].SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
        //                                           && (oCollPeilutOvdimIns.Value[l].SHAT_YETZIA == oPeilut.dFullShatYetzia) && (oCollPeilutOvdimIns.Value[l].MISPAR_KNISA == oPeilut.iMisparKnisa))
        //                                    {
        //                                        oCollPeilutOvdimIns.RemoveAt(l);
        //                                        l -= 1;
        //                                    }
        //                                    l += 1;
        //                                    iCountPeiluyotIns = oCollPeilutOvdimIns.Count;
        //                                } while (l < iCountPeiluyotIns);
        //                            }
        //                        }
        //                        break;
        //                    }
        //                }
        //            }
        //            htEmployeeDetails[i] = oSidur;

        //        }


        //    }
        //    catch (Exception ex)
        //    {
        //        clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, _iMisparIshi, "E", 5, _dCardDate, "AddElementMechine05: " + ex.Message);
        //        _bSuccsess = false;
        //    }
        //}

        //private void FindDuplicatPeiluyot(int iPeilutNesiaIndex, DateTime dShatYetzia, int iSidurIndex, ref clSidur oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        //{
        //    int j;
        //    clPeilut oPeilut;
        //    SourceObj SourceObject;
        //    OBJ_PEILUT_OVDIM oObjPeilutUpd;

        //    try
        //    {

        //        for (j = 0; j < oSidur.htPeilut.Count; j++)
        //        {
        //            if (iPeilutNesiaIndex != j)
        //            {
        //                oPeilut = (clPeilut)oSidur.htPeilut[j];
        //                if (oPeilut.dFullShatYetzia == dShatYetzia)
        //                {
        //                    if (!CheckPeilutObjectDelete(iSidurIndex, j))
        //                    {
        //                        oObjPeilutUpd = GetUpdPeilutObject(iSidurIndex, oPeilut, out SourceObject, oObjSidurimOvdimUpd);
        //                        if (SourceObject == SourceObj.Insert)
        //                        {
        //                            oObjPeilutUpd.SHAT_YETZIA = oPeilut.dFullShatYetzia.AddMinutes(1);
        //                            oPeilut.dFullShatYetzia = oObjPeilutUpd.SHAT_YETZIA;
        //                        }
        //                        else
        //                        {
        //                            oObjPeilutUpd.NEW_SHAT_YETZIA = oPeilut.dFullShatYetzia.AddMinutes(1);
        //                            oObjPeilutUpd.UPDATE_OBJECT = 1;
        //                            UpdateIdkunRashemet(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutUpd.NEW_SHAT_YETZIA,1);
        //                            UpdateApprovalErrors(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutUpd.NEW_SHAT_YETZIA);

        //                            oPeilut.dFullShatYetzia = oObjPeilutUpd.NEW_SHAT_YETZIA;
        //                        }

        //                        oPeilut.sShatYetzia = oPeilut.dFullShatYetzia.ToString("HH:mm");
        //                        oSidur.htPeilut[j] = oPeilut;
        //                        FindDuplicatPeiluyot(j, oPeilut.dFullShatYetzia, iSidurIndex, ref oSidur, ref oObjSidurimOvdimUpd);
        //                    }
        //                }
        //            }
        //        }

        //        int iMisparSidur = oSidur.iMisparSidur;
        //        DateTime dShatHatchalaSidur = oSidur.dFullShatHatchala;

        //        //oSidurWithCancled = _htEmployeeDetailsWithCancled.Values
        //        //                     .Cast<clSidur>()
        //        //                     .ToList()
        //        //                    .Find(sidur => (sidur.iMisparSidur == iMisparSidur && sidur.dFullShatHatchala == dShatHatchalaSidur));
        //        //if (oSidurWithCancled != null)
        //        //{
        //        //    for (j = 0; j < oSidurWithCancled.htPeilut.Count; j++)
        //        //    {
        //        //        oPeilut = (clPeilut)oSidurWithCancled.htPeilut[j];
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


        //private DateTime GetShatHatchalaElementMachine(int iIndexSidur, int iPeilutNesiaIndex, ref clSidur oSidur, clPeilut oPeilutMachine, bool bFirstElementMachine, ref bool bUsedMazanTichnun, ref bool bUsedMazanTichnunInSidur)
        //{
        //    clPeilut oNextPeilut, oPeilut, oFirstPeilutMashmautit;
        //    DateTime dShatHatchala = DateTime.MinValue;
        //    int i, iIndexPeilutMashmautit, iMeshechPeilut, iMeshechPeilutMachine;
        //    DateTime dRefferenceDate;
        //    bool bCheck = false;
        //    try
        //    {
        //        oFirstPeilutMashmautit = null;
        //        iIndexPeilutMashmautit = -1;
        //        for (i = iPeilutNesiaIndex; i <= oSidur.htPeilut.Values.Count - 1; i++)
        //        {
        //            oNextPeilut = (clPeilut)oSidur.htPeilut[i];
        //            if (oNextPeilut.iMakatType == clKavim.enMakatType.mVisa.GetHashCode() || oNextPeilut.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode() || oNextPeilut.iMakatType == clKavim.enMakatType.mNamak.GetHashCode() || (oNextPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && (oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != KdsLibrary.clGeneral.enElementHachanatMechona.Element701.GetHashCode().ToString() && oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != KdsLibrary.clGeneral.enElementHachanatMechona.Element712.GetHashCode().ToString() && oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != KdsLibrary.clGeneral.enElementHachanatMechona.Element711.GetHashCode().ToString()) && (oNextPeilut.iElementLeShatGmar > 0 || oNextPeilut.iElementLeShatGmar == -1 || oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "700")))
        //            {
        //                oFirstPeilutMashmautit = oNextPeilut;
        //                iIndexPeilutMashmautit = i;
        //                break;
        //            }
        //        }

        //        //קיימת פעילות משמעותית ראשונה ):
        //        if (oFirstPeilutMashmautit != null)
        //        {
        //            if (iPeilutNesiaIndex == iIndexPeilutMashmautit)
        //            {
        //                dShatHatchala = oFirstPeilutMashmautit.dFullShatYetzia.AddMinutes(-(GetMeshechPeilutHachnatMechona(iIndexSidur, oPeilutMachine, oSidur, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur) + oFirstPeilutMashmautit.iKisuyTor));
        //            }
        //            else
        //            {
        //                oNextPeilut = (clPeilut)oSidur.htPeilut[iPeilutNesiaIndex];
        //                iMeshechPeilut = GetMeshechPeilutHachnatMechona(iIndexSidur, oNextPeilut, oSidur, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur);
        //                iMeshechPeilutMachine = GetMeshechPeilutHachnatMechona(iIndexSidur, oPeilutMachine, oSidur, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur);

        //                dRefferenceDate = clGeneral.GetDateTimeFromStringHour("08:00", oSidur.dFullShatHatchala);
        //                if (oSidur.dFullShatHatchala < dRefferenceDate || (clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, _iSugYom, _dCardDate)))
        //                    bCheck = true;

        //                if (bCheck && iMeshechPeilut <= 6 && iMeshechPeilut < iMeshechPeilutMachine && bFirstElementMachine)
        //                {
        //                    dShatHatchala = oFirstPeilutMashmautit.dFullShatYetzia.AddMinutes(-(iMeshechPeilutMachine + oFirstPeilutMashmautit.iKisuyTor));
        //                }
        //                else
        //                {
        //                    dShatHatchala = oFirstPeilutMashmautit.dFullShatYetzia.AddMinutes(-(iMeshechPeilutMachine + iMeshechPeilut + oFirstPeilutMashmautit.iKisuyTor));
        //                }
        //            }
        //        }
        //        else
        //        {//לא קיימת פעילות משמעותית:

        //            for (i = iPeilutNesiaIndex; i <= oSidur.htPeilut.Values.Count - 1; i++)
        //            {
        //                //: ריקה, אלמנט ללא מאפיין 37.
        //                oPeilut = (clPeilut)oSidur.htPeilut[i];
        //                if ((oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && oPeilut.iElementLeShatGmar == 0) || oPeilut.iMakatType == clKavim.enMakatType.mEmpty.GetHashCode())
        //                {// יש לעדכן את שעת היציאה של הכנת המכונה לשעת יציאה של הפעילות שאינה משמעותית הראשונה פחות משך הכנת המכונה.
        //                    dShatHatchala = oPeilut.dFullShatYetzia.AddMinutes(-(GetMeshechPeilutHachnatMechona(iIndexSidur, oPeilutMachine, oSidur, ref  bUsedMazanTichnun, ref bUsedMazanTichnunInSidur)));
        //                    break;
        //                }
        //            }

        //        }
        //        return dShatHatchala;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private void AddElementMachineForFirstSidur(ref clSidur oSidur, int iIndexSidur, ref DateTime dShatYetzia, ref  int iNumHachanotMechona, ref int iNumHachanotMechonaForSidur, ref int iIndexElement, ref int iPeilutNesiaIndex, ref bool bUsedMazanTichnun, ref bool bUsedMazanTichnunInSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        //{
        //    bool bPeilutNesiaMustBusNumber = false;
        //    long lOtoNo = 0;

        //    try
        //    {
        //        //אם זה הסידור הראשון וקיימת נסיעת שירות, נמ"ק , ריקה או אלמנט שדורש הכנת רכב וגם לא קיימת פעילות הכנת מכונה
        //        IsSidurMustHachanatMechonaFirst(ref oSidur, ref bPeilutNesiaMustBusNumber, ref iPeilutNesiaIndex, ref lOtoNo);

        //        //אם נמצאה פעילות מסוג נסיעה או אלמנט הדורש מספר רכב וגם לא נמצאה פעילות של הכנת מכונה
        //        //נכניס אלמנט הכנסת מכונה. נבדיל בין שני מקרים: סידור ראשון המתחיל לפני )08:00 בבוקר וסידור ראשון שמתחיל אחרי 08:00 בבוקר
        //        if ((bPeilutNesiaMustBusNumber))
        //        {
        //            //אם סידור הוא ראשון ביום, מתחיל לפני 08:00 ואין לו אלמנט הכנת מכונה מכל סוג שהוא (701, 711, 712) - להוסיף לו אלמנט הכנת מכונה ראשונה (70100000).  
        //            //זמן האלמנט ייקבע לפי הערך לפרמטר 120 (זמן הכנת מכונה ראשונה) בטבלת פרמטרים חיצוניים. שעת היציאה של פעילות האלמנט תחושב באופן הבא: יש לקחת את  שעת היציאה של הפעילות העוקבת לאלמנט החדש שהוספנו ולהחסיר ממנה את זמן האלמנט שהוספנו.
        //            AddElementHachanatMechine701(ref oSidur, iIndexSidur, ref dShatYetzia, ref iPeilutNesiaIndex, ref iNumHachanotMechona, ref iNumHachanotMechonaForSidur, ref iIndexElement, ref bUsedMazanTichnun, ref oObjSidurimOvdimUpd, ref bUsedMazanTichnunInSidur);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private void AddElementHachanatMechine701(ref clSidur oSidur, int iIndexSidur, ref DateTime dShatYetiza, ref int iPeilutNesiaIndex, ref  int iNumHachanotMechona, ref int iNumHachanotMechonaForSidur, ref int iIndexElement, ref bool bUsedMazanTichnun, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, ref bool bUsedMazanTichnunInSidur)
        //{
        //    OBJ_PEILUT_OVDIM oObjPeilutOvdimIns = new OBJ_PEILUT_OVDIM();
        //    clPeilut oPeilut;
        //    DateTime dRefferenceDate, dShatYetziaPeilut;
        //    try
        //    {
        //        oPeilut = (clPeilut)oSidur.htPeilut[iPeilutNesiaIndex];
        //        InsertToObjPeilutOvdimForInsert(ref oSidur, ref oObjPeilutOvdimIns);
        //        if (!CheckHaveElementHachanatMechona(ref oSidur, iPeilutNesiaIndex))
        //        {
        //            //אם מספר הכנות המכונה (מכל סוג שהוא) שנוספו עד כה ליום העבודה גדול שווה לערך בפרמטר 123 (מכסימום יומי להכנות מכונה) או מספר הכנות המכונה בסידור גדול שווה לערך בפרמטר 124 (מכסימום הכנות מכונה בסידור אחד)- לא מעדכנים זמן לאלמנט. 
        //            if (iNumHachanotMechona < oParam.iPrepareAllMechineTotalMaxTimeInDay || iNumHachanotMechonaForSidur < oParam.iPrepareAllMechineTotalMaxTimeForSidur)
        //                oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("701", oParam.iPrepareFirstMechineMaxTime.ToString().PadLeft(3, (char)48), "00"));
        //            else oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("701", "000", "00"));

        //            ////dRefferenceDate = clGeneral.GetDateTimeFromStringHour("08:00", oPeilut.dFullShatYetzia);
        //            ////if (oPeilut.dFullShatYetzia >= dRefferenceDate && (!clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, _iSugYom, _dCardDate)))
        //            ////{
        //            ////    oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("701", "005", "00"));
        //            //////    dShatYetziaPeilut = dShatYetziaPeilut.AddMinutes(-3);
        //            ////}
        //            oObjPeilutOvdimIns.OTO_NO = oPeilut.lOtoNo;

        //            clPeilut oPeilutNew = new clPeilut(_iMisparIshi, _dCardDate, oObjPeilutOvdimIns, dtTmpMeafyeneyElements);

        //            oObjPeilutOvdimIns.BITUL_O_HOSAFA = 4;
        //            oCollPeilutOvdimIns.Add(oObjPeilutOvdimIns);
        //            oPeilutNew.iBitulOHosafa = 4;
        //            oSidur.htPeilut.Insert(iPeilutNesiaIndex, dShatYetiza.ToString("HH:mm:ss").Replace(":", "") + iPeilutNesiaIndex + 1, oPeilutNew);
        //            iIndexElement = iPeilutNesiaIndex;
        //            iPeilutNesiaIndex += 1;

        //            dShatYetziaPeilut = GetShatHatchalaElementMachine(iIndexSidur, iPeilutNesiaIndex, ref oSidur, oPeilutNew, true, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur);

        //            dRefferenceDate = clGeneral.GetDateTimeFromStringHour("08:00", dShatYetziaPeilut);
        //            if (dShatYetziaPeilut >= dRefferenceDate && (!clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, _iSugYom, _dCardDate)))
        //            {
        //                oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("701", "005", "00"));
        //                dShatYetziaPeilut = dShatYetziaPeilut.AddMinutes(-3);
        //            }

        //            FindDuplicatPeiluyot(iPeilutNesiaIndex - 1, dShatYetziaPeilut, iIndexSidur, ref oSidur, ref oObjSidurimOvdimUpd);

        //            oObjPeilutOvdimIns.SHAT_YETZIA = dShatYetziaPeilut;

        //            oPeilutNew.dFullShatYetzia = oObjPeilutOvdimIns.SHAT_YETZIA;
        //            oPeilutNew.sShatYetzia = oPeilutNew.dFullShatYetzia.ToString("HH:mm");

        //            if (iIndexElement == 0) dShatYetiza = oObjPeilutOvdimIns.SHAT_YETZIA;
        //            iNumHachanotMechonaForSidur += 1;
        //            iNumHachanotMechona += 1;

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private bool CheckHaveElementHachanatMechona(ref clSidur oSidur, int iIndexPeilutMustAutoNum)
        //{
        //    bool bHave = false;

        //    try
        //    {
        //        if (iIndexPeilutMustAutoNum > 0)
        //        {
        //            clPeilut oPeilut = (clPeilut)oSidur.htPeilut[iIndexPeilutMustAutoNum - 1];
        //            if (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == KdsLibrary.clGeneral.enElementHachanatMechona.Element712.GetHashCode().ToString() || oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == KdsLibrary.clGeneral.enElementHachanatMechona.Element711.GetHashCode().ToString())
        //            { bHave = true; }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return bHave;
        //}

        //private void AddElementMachineForNextSidur(ref clSidur oSidur, ref DateTime dShatYetzia, int iSidurIndex, int iIndexFirstElementMachine, ref  int iNumHachanotMechona, ref int iNumHachanotMechonaForSidur, ref int iIndexElement, ref bool bUsedMazanTichnun, ref bool bUsedMazanTichnunInSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        //{
        //    clPeilut oPeilut;
        //    clSidur oLocalSidur;
        //    int iPeilutNesiaIndex = 0;
        //    long lOtoNo = 0;
        //    int i = 0;
        //    int iIndexPeilut;
        //    bool bHavePeilutMustRechev = false;
        //    int l, iCountPeiluyot;
        //    bool bAddElementPitzul = false;
        //    bool bAddElementHamtana = true;
        //    int j = 0;
        //    long lMakat;
        //    try
        //    {
        //        //סידור אינו הראשון ביום ויש בו פעילות "דורשת מספר רכב" ואין לפניה אלמנט הכנת מכונה מכל סוג שהוא (701, 711, 712). מחפשים פעילות אחרת שהיא "דורשת מספר רכב" ששעת היציאה שלה קטנה משעת היציאה של הפעילות אותה אנו בודקים.
        //        //אם הפעילות אותה אנו בודקים היא הראשונה בסידור מחפשים פעילות "דורשת מספר רכב" בסידור קודם. אם בשתי הפעילויות מספר הרכב לא זהה (ערך שאינו null או 0 ובעל 5 ספרות)  אז מוסיפים אלמנט הכנת מכונה (71100000) לפני הפעילות אותה בדקנו.

        //        l = iIndexFirstElementMachine;
        //        iCountPeiluyot = oSidur.htPeilut.Count;
        //        if (iCountPeiluyot > 0 && l < iCountPeiluyot)
        //        {
        //            do
        //            {
        //                oPeilut = (clPeilut)oSidur.htPeilut[l];

        //                if (oPeilut.IsMustBusNumber(oParam.iVisutMustRechevWC))
        //                {
        //                    iPeilutNesiaIndex = l;
        //                    lOtoNo = oPeilut.lOtoNo;
        //                    bHavePeilutMustRechev = false;

        //                    for (i = iSidurIndex; i >= 0; i--)
        //                    {
        //                        oLocalSidur = (clSidur)htEmployeeDetails[i];
        //                        if (!bHavePeilutMustRechev && !CheckHaveElementHachanatMechona(ref oSidur, iPeilutNesiaIndex))
        //                        {
        //                            if (iSidurIndex == i)
        //                            { iIndexPeilut = iPeilutNesiaIndex - 1; }
        //                            else
        //                            { iIndexPeilut = oLocalSidur.htPeilut.Count - 1; }
        //                            for (j = iIndexPeilut; j >= 0; j--)
        //                            {
        //                                if (!bHavePeilutMustRechev)
        //                                {
        //                                    oPeilut = (clPeilut)oLocalSidur.htPeilut[j];

        //                                    if (oPeilut.IsMustBusNumber(oParam.iVisutMustRechevWC))
        //                                    {
        //                                        if (oPeilut.lOtoNo != lOtoNo && oPeilut.lOtoNo > 0 && lOtoNo > 0 && oPeilut.lOtoNo.ToString().Length >= 5)
        //                                        {
        //                                            //אם אין להן אותו מספר רכב אז מוסיפים אלמנט הכנת מכונה (71100000).
        //                                            AddElementHachanatMechine711(ref oSidur, iSidurIndex, ref dShatYetzia, ref iPeilutNesiaIndex, iNumHachanotMechona, iNumHachanotMechonaForSidur, ref  iIndexElement, ref bUsedMazanTichnun, ref  bUsedMazanTichnunInSidur, ref oObjSidurimOvdimUpd);
        //                                            htEmployeeDetails[iSidurIndex] = oSidur;
        //                                            if (i == iSidurIndex)
        //                                                l += 1;
        //                                        }

        //                                        bHavePeilutMustRechev = true;
        //                                        break;

        //                                    }
        //                                }
        //                            }

        //                            if (!CheckHaveElementHachanatMechona(ref oSidur, iPeilutNesiaIndex) && !bAddElementPitzul)
        //                            {
        //                                if (oLocalSidur != oSidur && (oSidur.dFullShatHatchala - oLocalSidur.dFullShatGmar).TotalMinutes > _oParameters.iMinTimeBetweenSidurim)
        //                                {
        //                                    AddElementHachanatMechine711(ref oSidur, iSidurIndex, ref dShatYetzia, ref iPeilutNesiaIndex, iNumHachanotMechona, iNumHachanotMechonaForSidur, ref  iIndexElement, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur, ref oObjSidurimOvdimUpd);
        //                                    htEmployeeDetails[iSidurIndex] = oSidur;
        //                                    bAddElementPitzul = true;
        //                                }
        //                            }

        //                            if (!CheckHaveElementHachanatMechona(ref oSidur, iPeilutNesiaIndex) && !bAddElementHamtana)
        //                            {
        //                                lMakat = ((clPeilut)oLocalSidur.htPeilut[oLocalSidur.htPeilut.Count - 1]).lMakatNesia;
        //                                if (oLocalSidur != oSidur && iSidurIndex == i + 1 && lMakat.ToString().PadLeft(8).Substring(0, 3) == "724" && int.Parse(lMakat.ToString().PadLeft(8).Substring(3, 3)) > 60)
        //                                {
        //                                    AddElementHachanatMechine711(ref oSidur, iSidurIndex, ref dShatYetzia, ref iPeilutNesiaIndex, iNumHachanotMechona, iNumHachanotMechonaForSidur, ref  iIndexElement, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur, ref oObjSidurimOvdimUpd);
        //                                    htEmployeeDetails[iSidurIndex] = oSidur;
        //                                    bAddElementHamtana = true;
        //                                }
        //                            }
        //                        }


        //                    }
        //                }
        //                l += 1;

        //                iCountPeiluyot = oSidur.htPeilut.Count;
        //            } while (l < iCountPeiluyot);
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //private void IsSidurMustHachanatMechonaFirst(ref clSidur oSidur, ref bool bPeilutNesiaMustBusNumber,
        //                                             ref int iPeilutNesiaIndex,
        //                                             ref long lOtoNo)
        //{
        //    clPeilut oPeilut;

        //    try
        //    {
        //        for (int j = 0; j < oSidur.htPeilut.Count; j++)
        //        {
        //            oPeilut = (clPeilut)oSidur.htPeilut[j];

        //            if (oPeilut.IsMustBusNumber(oParam.iVisutMustRechevWC))
        //            {
        //                bPeilutNesiaMustBusNumber = true;
        //                iPeilutNesiaIndex = j;
        //                lOtoNo = oPeilut.lOtoNo;
        //                break;
        //            }

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}



        //private void AddElementHachanatMechine711(ref clSidur oSidur, int iIndexSidur, ref DateTime dShatYetiza, ref int iPeilutNesiaIndex, int iNumHachanotMechona, int iNumHachanotMechonaForSidur, ref int iIndexElement, ref bool bUsedMazanTichnun, ref bool bUsedMazanTichnunInSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        //{
        //    OBJ_PEILUT_OVDIM oObjPeilutOvdimIns = new OBJ_PEILUT_OVDIM();
        //    clPeilut oPeilut;
        //    DateTime dShatYetziaPeilut;
        //    try
        //    {
        //        oPeilut = (clPeilut)oSidur.htPeilut[iPeilutNesiaIndex];
        //        InsertToObjPeilutOvdimForInsert(ref oSidur, ref oObjPeilutOvdimIns);

        //        //אם מספר הכנות המכונה (מכל סוג שהוא) שנוספו עד כה ליום העבודה גדול שווה לערך בפרמטר 123 (מכסימום יומי להכנות מכונה) או מספר הכנות המכונה בסידור גדול שווה לערך בפרמטר 124 (מכסימום הכנות מכונה בסידור אחד)- לא מעדכנים זמן לאלמנט. 
        //        if (iNumHachanotMechona < oParam.iPrepareAllMechineTotalMaxTimeInDay || iNumHachanotMechonaForSidur < oParam.iPrepareAllMechineTotalMaxTimeForSidur)
        //        {
        //            oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("711", oParam.iPrepareOtherMechineMaxTime.ToString().PadLeft(3, (char)48), "00"));
        //        }
        //        oObjPeilutOvdimIns.OTO_NO = oPeilut.lOtoNo;

        //        clPeilut oPeilutNew = new clPeilut(_iMisparIshi, _dCardDate, oObjPeilutOvdimIns, dtTmpMeafyeneyElements);

        //        oObjPeilutOvdimIns.BITUL_O_HOSAFA = 4;
        //        oCollPeilutOvdimIns.Add(oObjPeilutOvdimIns);
        //        oPeilutNew.iBitulOHosafa = 4;
        //        oSidur.htPeilut.Insert(iPeilutNesiaIndex, dShatYetiza.ToString("HH:mm:ss").Replace(":", "") + iPeilutNesiaIndex + 11, oPeilutNew);
        //        iIndexElement = iPeilutNesiaIndex;
        //        iPeilutNesiaIndex += 1;

        //        dShatYetziaPeilut = GetShatHatchalaElementMachine(iIndexSidur, iPeilutNesiaIndex, ref oSidur, (clPeilut)oSidur.htPeilut[iIndexElement], false, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur);
        //        FindDuplicatPeiluyot(iPeilutNesiaIndex - 1, dShatYetziaPeilut, iIndexSidur, ref oSidur, ref oObjSidurimOvdimUpd);

        //        oObjPeilutOvdimIns.SHAT_YETZIA = dShatYetziaPeilut;
        //        oPeilutNew.dFullShatYetzia = oObjPeilutOvdimIns.SHAT_YETZIA;
        //        oPeilutNew.sShatYetzia = oPeilutNew.dFullShatYetzia.ToString("HH:mm");
        //        if (iIndexElement == 0) dShatYetiza = oObjPeilutOvdimIns.SHAT_YETZIA;

        //        iNumHachanotMechonaForSidur += 1;
        //        iNumHachanotMechona += 1;

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}


        private void AddElementMechine05_2()
        {
            clSidur oSidur;
            int iMeshechHachanotMechona = 0;
            int iMeshechHachanotMechonaNosafot = 0;
            int iNumHachanotMechonaForSidur = 0;
            OBJ_PEILUT_OVDIM oObjPeilutUpd, oObjPeilutDel;
            clPeilut oPeilut;
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
                    oSidur = (clSidur)htEmployeeDetails[i];
                    j = 0;
                    iCountPeiluyot = oSidur.htPeilut.Count;
                    if (j < iCountPeiluyot)
                    {
                        do
                        {
                            oPeilut = (clPeilut)oSidur.htPeilut[j];
                            if (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == KdsLibrary.clGeneral.enElementHachanatMechona.Element701.GetHashCode().ToString() || oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == KdsLibrary.clGeneral.enElementHachanatMechona.Element712.GetHashCode().ToString() || oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == KdsLibrary.clGeneral.enElementHachanatMechona.Element711.GetHashCode().ToString())
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
                    oSidur = (clSidur)htEmployeeDetails[i];
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
                                oPeilut = (clPeilut)oSidur.htPeilut[j];

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
                                oPeilut = (clPeilut)oSidur.htPeilut[j];

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

        private void AddElementMachineForFirstSidur_2(ref clSidur oSidur, int iIndexSidur, ref DateTime dShatYetzia, ref  int iMeshechHachanotMechona, ref int iNumHachanotMechonaForSidur, ref int iIndexElement, ref int iPeilutNesiaIndex, ref bool bUsedMazanTichnun, ref bool bUsedMazanTichnunInSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
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

      
        private void IsSidurMustHachanatMechonaFirst_2(ref clSidur oSidur, ref bool bPeilutNesiaMustBusNumber,
                                                   ref int iPeilutNesiaIndex,
                                                   ref long lOtoNo)
        {
            clPeilut oPeilut;

            try
            {
                for (int j = 0; j < oSidur.htPeilut.Count; j++)
                {
                    oPeilut = (clPeilut)oSidur.htPeilut[j];

                    if (oPeilut.IsMustBusNumber(oParam.iVisutMustRechevWC))
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

        private void AddElementHachanatMechine701_2(ref clSidur oSidur, int iIndexSidur, ref DateTime dShatYetiza, ref int iPeilutNesiaIndex, ref  int iMeshechHachanotMechona, ref int iNumHachanotMechonaForSidur, ref int iIndexElement, ref bool bUsedMazanTichnun, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, ref bool bUsedMazanTichnunInSidur)
        {
            OBJ_PEILUT_OVDIM oObjPeilutOvdimIns = new OBJ_PEILUT_OVDIM();
            clPeilut oPeilut;
            int idakot,iMeshechElement;
            DateTime dRefferenceDate, dShatYetziaPeilut,dShatKisuyTor;
            try
            {
                oPeilut = (clPeilut)oSidur.htPeilut[iPeilutNesiaIndex];
                InsertToObjPeilutOvdimForInsert(ref oSidur, ref oObjPeilutOvdimIns);
               // if (!CheckHaveElementHachanatMechona_2(ref oSidur, iPeilutNesiaIndex))
              //  {
                    //אם מספר הכנות המכונה (מכל סוג שהוא) שנוספו עד כה ליום העבודה גדול שווה לערך בפרמטר 123 (מכסימום יומי להכנות מכונה) או מספר הכנות המכונה בסידור גדול שווה לערך בפרמטר 124 (מכסימום הכנות מכונה בסידור אחד)- לא מעדכנים זמן לאלמנט. 
                    //if (iNumHachanotMechona < oParam.iPrepareAllMechineTotalMaxTimeInDay || iNumHachanotMechonaForSidur < oParam.iPrepareAllMechineTotalMaxTimeForSidur)
                    oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("701", oParam.iPrepareFirstMechineMaxTime.ToString().PadLeft(3, (char)48), "00"));
                    // else oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("701", "000", "00"));

                    dRefferenceDate = clGeneral.GetDateTimeFromStringHour("08:00", oPeilut.dFullShatYetzia);
                    dShatKisuyTor = oPeilut.dFullShatYetzia.AddMinutes(-oPeilut.iKisuyTor);
                    if (oPeilut.dFullShatYetzia > dRefferenceDate && dShatKisuyTor > dRefferenceDate && (!clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, _iSugYom, _dCardDate)))
                    {
                        oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("701", oParam.iPrepareOtherMechineMaxTime.ToString().PadLeft(3, (char)48), "00"));
                        //    dShatYetziaPeilut = dShatYetziaPeilut.AddMinutes(-3);
                    }
                    oObjPeilutOvdimIns.OTO_NO = oPeilut.lOtoNo;

                    clPeilut oPeilutNew = new clPeilut(_iMisparIshi, _dCardDate, oObjPeilutOvdimIns, dtTmpMeafyeneyElements);

                    oObjPeilutOvdimIns.BITUL_O_HOSAFA = 4;
                    oCollPeilutOvdimIns.Add(oObjPeilutOvdimIns);
                    oPeilutNew.iBitulOHosafa = 4;
                    oSidur.htPeilut.Insert(iPeilutNesiaIndex, dShatYetiza.ToString("HH:mm:ss").Replace(":", "") + iPeilutNesiaIndex + 1, oPeilutNew);
                    iIndexElement = iPeilutNesiaIndex;
                    iPeilutNesiaIndex += 1;

                    dShatYetziaPeilut = GetShatHatchalaElementMachine_2(iIndexSidur, iPeilutNesiaIndex, ref oSidur, oPeilutNew, true, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur);

                    //dRefferenceDate = clGeneral.GetDateTimeFromStringHour("08:00", dShatYetziaPeilut);
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

        private void AddElementMachineForNextSidur_2(ref clSidur oSidur, ref DateTime dShatYetzia, int iSidurIndex, int iIndexFirstElementMachine, ref  int iMeshechHachanotMechona, ref int iNumHachanotMechonaForSidur,ref int iMeshechHachanotMechonaNosafot, ref int iIndexElement, ref bool bUsedMazanTichnun, ref bool bUsedMazanTichnunInSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            clPeilut oPeilut;
            clSidur oLocalSidur;
            DateTime dShatYetziaPeilut;
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
                        oPeilut = (clPeilut)oSidur.htPeilut[l];

                        if (oPeilut.IsMustBusNumber(oParam.iVisutMustRechevWC))
                        {
                            iPeilutNesiaIndex = l;
                            lOtoNo = oPeilut.lOtoNo;
                            bHavePeilutMustRechev = false;
                            dShatYetziaPeilut = oPeilut.dFullShatYetzia;
                            for (i = iSidurIndex; i >= 0; i--)
                            {
                                oLocalSidur = (clSidur)htEmployeeDetails[i];
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
                                            oPeilut = (clPeilut)oLocalSidur.htPeilut[j];

                                            if (oPeilut.IsMustBusNumber(oParam.iVisutMustRechevWC))
                                            {
                                                if (oPeilut.lOtoNo != lOtoNo && oPeilut.lOtoNo > 0 && lOtoNo > 0 && oPeilut.lOtoNo.ToString().Length >= 5)
                                                {
                                                    //אם אין להן אותו מספר רכב אז מוסיפים אלמנט הכנת מכונה (71100000).
                                                    AddElementHachanatMechine711_2(ref oSidur, iSidurIndex, ref dShatYetzia, ref iPeilutNesiaIndex, ref iMeshechHachanotMechona, ref iNumHachanotMechonaForSidur, ref iMeshechHachanotMechonaNosafot, ref  iIndexElement, ref bUsedMazanTichnun, ref  bUsedMazanTichnunInSidur, ref oObjSidurimOvdimUpd);
                                                    htEmployeeDetails[iSidurIndex] = oSidur;
                                                    if (i == iSidurIndex)
                                                        l += 1;
                                                }
                                                else if (oLocalSidur != oSidur && l == 0 && isPeilutMashmautit((clPeilut)oSidur.htPeilut[l]) && oPeilut.lOtoNo == lOtoNo && (dShatYetziaPeilut - oLocalSidur.dFullShatGmar).TotalMinutes > _oParameters.iMinTimeBetweenSidurim
                                                    && ((dShatYetziaPeilut - oLocalSidur.dFullShatGmar).TotalMinutes - _oParameters.iPrepareOtherMechineMaxTime) > _oParameters.iMinTimeBetweenSidurim)
                                                { 
                                                    AddElementHachanatMechine711_2(ref oSidur, iSidurIndex, ref dShatYetzia, ref iPeilutNesiaIndex, ref iMeshechHachanotMechona, ref iNumHachanotMechonaForSidur, ref iMeshechHachanotMechonaNosafot, ref  iIndexElement, ref bUsedMazanTichnun, ref  bUsedMazanTichnunInSidur, ref oObjSidurimOvdimUpd);
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

                                            lMakat = ((clPeilut)oLocalSidur.htPeilut[oLocalSidur.htPeilut.Count - 1]).lMakatNesia;
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

        private bool CheckHaveElementHachanatMechona_2(ref clSidur oSidur, int iIndexPeilutMustAutoNum)
        {
            bool bHave = false;

            try
            {
                if (iIndexPeilutMustAutoNum > 0)
                {
                    clPeilut oPeilut = (clPeilut)oSidur.htPeilut[iIndexPeilutMustAutoNum - 1];
                    if (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == KdsLibrary.clGeneral.enElementHachanatMechona.Element712.GetHashCode().ToString() || oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == KdsLibrary.clGeneral.enElementHachanatMechona.Element711.GetHashCode().ToString())
                    { bHave = true; }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bHave;
        }

        private void AddElementHachanatMechine711_2(ref clSidur oSidur, int iIndexSidur, ref DateTime dShatYetiza, ref int iPeilutNesiaIndex,ref int iMeshechHachanotMechona,ref int iNumHachanotMechonaForSidur,ref int iMeshechHachanotMechonaNosafot, ref int iIndexElement, ref bool bUsedMazanTichnun, ref bool bUsedMazanTichnunInSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            OBJ_PEILUT_OVDIM oObjPeilutOvdimIns = new OBJ_PEILUT_OVDIM();
            clPeilut oPeilut;
            int idakot, iMeshechElement;
            DateTime dShatYetziaPeilut;
            try
            {
                oPeilut = (clPeilut)oSidur.htPeilut[iPeilutNesiaIndex];
                InsertToObjPeilutOvdimForInsert(ref oSidur, ref oObjPeilutOvdimIns);

                if (oSidur.dSidurDate >= _oParameters.dTaarichmichsatHachanatMechona)
                {
                    if (iMeshechHachanotMechona < oParam.iPrepareAllMechineTotalMaxTimeInDay &&
                        iNumHachanotMechonaForSidur < oParam.iPrepareAllMechineTotalMaxTimeForSidur &&
                        iMeshechHachanotMechonaNosafot < oParam.iPrepareOtherMechineTotalMaxTime)
                    {
                        oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("711", oParam.iPrepareOtherMechineMaxTime.ToString().PadLeft(3, (char)48), "00"));
                    }
                    else oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("711", "000", "00"));
                }
                else   
                    oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("711", oParam.iPrepareOtherMechineMaxTime.ToString().PadLeft(3, (char)48), "00"));
               
                oObjPeilutOvdimIns.OTO_NO = oPeilut.lOtoNo;

                clPeilut oPeilutNew = new clPeilut(_iMisparIshi, _dCardDate, oObjPeilutOvdimIns, dtTmpMeafyeneyElements);

                oObjPeilutOvdimIns.BITUL_O_HOSAFA = 4;
                oCollPeilutOvdimIns.Add(oObjPeilutOvdimIns);
                oPeilutNew.iBitulOHosafa = 4;
                oSidur.htPeilut.Insert(iPeilutNesiaIndex, dShatYetiza.ToString("HH:mm:ss").Replace(":", "") + iPeilutNesiaIndex + 11, oPeilutNew);
                iIndexElement = iPeilutNesiaIndex;
                iPeilutNesiaIndex += 1;

                dShatYetziaPeilut = GetShatHatchalaElementMachine_2(iIndexSidur, iPeilutNesiaIndex, ref oSidur, (clPeilut)oSidur.htPeilut[iIndexElement], false, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur);
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

        private DateTime GetShatHatchalaElementMachine_2(int iIndexSidur, int iPeilutNesiaIndex, ref clSidur oSidur, clPeilut oPeilutMachine, bool bFirstElementMachine, ref bool bUsedMazanTichnun, ref bool bUsedMazanTichnunInSidur)
        {
            clPeilut oNextPeilut, oPeilut,oPeilutRekaFirst, oFirstPeilutMashmautit;
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
                    oNextPeilut = (clPeilut)oSidur.htPeilut[i];

                  //  if (oNextPeilut.iMakatType == clKavim.enMakatType.mVisa.GetHashCode() || oNextPeilut.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode() || oNextPeilut.iMakatType == clKavim.enMakatType.mNamak.GetHashCode() || (oNextPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && (oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != KdsLibrary.clGeneral.enElementHachanatMechona.Element701.GetHashCode().ToString() && oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != KdsLibrary.clGeneral.enElementHachanatMechona.Element712.GetHashCode().ToString() && oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != KdsLibrary.clGeneral.enElementHachanatMechona.Element711.GetHashCode().ToString()) && (oNextPeilut.iElementLeShatGmar > 0 || oNextPeilut.iElementLeShatGmar == -1 || oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "700")))
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

                        dRefferenceDate = clGeneral.GetDateTimeFromStringHour("08:00", oSidur.dFullShatHatchala);
                        j = iIndexPeilutMashmautit - 1;
                        oNextPeilut = (clPeilut)oSidur.htPeilut[j];
                        while (oNextPeilut.lMakatNesia != oPeilutMachine.lMakatNesia)
                        {
                            //if (oNextPeilut.iMakatType == clKavim.enMakatType.mEmpty.GetHashCode())
                            //    oPeilutRekaFirst = oNextPeilut;
                            j -= 1;
                            oNextPeilut = (clPeilut)oSidur.htPeilut[j];                          
                        }
                        
                        oPeilutRekaFirst = (clPeilut)oSidur.htPeilut[j + 1];
                        if (sSugMechona == "711" || (sSugMechona == "701" && 
                                                    ((oFirstPeilutMashmautit.dFullShatYetzia <= dRefferenceDate || (oFirstPeilutMashmautit.dFullShatYetzia > dRefferenceDate && clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, _iSugYom, _dCardDate)) || (oFirstPeilutMashmautit.dFullShatYetzia > dRefferenceDate && oFirstPeilutMashmautit.dFullShatYetzia.AddMinutes(-oFirstPeilutMashmautit.iKisuyTor) <= dRefferenceDate)) && 
                                                     (GetMeshechPeilutHachnatMechona(iIndexSidur, oPeilutRekaFirst, oSidur, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur) > oParam.iMaxZmanRekaAdShmone)) 
                                                 || ((oFirstPeilutMashmautit.dFullShatYetzia > dRefferenceDate && oFirstPeilutMashmautit.dFullShatYetzia.AddMinutes(-oFirstPeilutMashmautit.iKisuyTor) > dRefferenceDate && !clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, _iSugYom, _dCardDate)) &&
                                                     (GetMeshechPeilutHachnatMechona(iIndexSidur, oPeilutRekaFirst, oSidur, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur) > oParam.iMaxZmanRekaNichleletafter8))))
                        {
                            j = iIndexPeilutMashmautit - 1; ;// -1;
                            oNextPeilut = (clPeilut)oSidur.htPeilut[j];
                            dShatHatchala = oFirstPeilutMashmautit.dFullShatYetzia.AddMinutes(-(GetMeshechPeilutHachnatMechona(iIndexSidur, oPeilutMachine, oSidur, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur) + oFirstPeilutMashmautit.iKisuyTor));
                            
                            while (oNextPeilut.lMakatNesia != oPeilutMachine.lMakatNesia) 
                            {
                                if (isElemntLoMashmauti(oNextPeilut) || oNextPeilut.iMakatType == clKavim.enMakatType.mEmpty.GetHashCode())
                                {
                                    iMeshechPeilut = (GetMeshechPeilutHachnatMechona(iIndexSidur, oNextPeilut, oSidur, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur));
                                    if (sSugMechona == "711" && iMeshechPeilut == 0)
                                        iMeshechPeilut = 1;
                                    dShatHatchala = dShatHatchala.AddMinutes(-iMeshechPeilut);
                                }
                                j -= 1;
                                oNextPeilut = (clPeilut)oSidur.htPeilut[j];
                            }
                           
                        }
                        else
                        {
                            j = iIndexPeilutMashmautit - 1;
                            oNextPeilut = (clPeilut)oSidur.htPeilut[j];
                            dShatHatchala = oFirstPeilutMashmautit.dFullShatYetzia.AddMinutes(-(GetMeshechPeilutHachnatMechona(iIndexSidur, oPeilutMachine, oSidur, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur) + oFirstPeilutMashmautit.iKisuyTor));

                            while (oNextPeilut != oPeilutRekaFirst)
                            {
                                if (isElemntLoMashmauti(oNextPeilut) || oNextPeilut.iMakatType == clKavim.enMakatType.mEmpty.GetHashCode())
                                    dShatHatchala = dShatHatchala.AddMinutes(-(GetMeshechPeilutHachnatMechona(iIndexSidur, oNextPeilut, oSidur, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur)));

                                j -= 1;
                                oNextPeilut = (clPeilut)oSidur.htPeilut[j];
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
                        oPeilut = (clPeilut)oSidur.htPeilut[i];
                        //if ((oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && oPeilut.iElementLeShatGmar == 0) || oPeilut.iMakatType == clKavim.enMakatType.mEmpty.GetHashCode())
                        if (isElemntLoMashmauti(oPeilut) || oPeilut.iMakatType == clKavim.enMakatType.mEmpty.GetHashCode())    
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

        private int FindDuplicatPeiluyot_2(int iPeilutNesiaIndex, DateTime dShatYetzia, int iSidurIndex, ref clSidur oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            int j;
            clPeilut oPeilut;
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
                        oPeilut = (clPeilut)oSidur.htPeilut[j];
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
                                    oPeilut = (clPeilut)oSidur.htPeilut[j];
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
                                            oPeilut = (clPeilut)oSidur.htPeilut[i];
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
        private int FindDuplicatPeiluyot_2_1(int iPeilutNesiaIndex, DateTime dShatYetzia, int iSidurIndex, ref clSidur oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            int j;
            clPeilut oPeilut;
            SourceObj SourceObject;
            OBJ_PEILUT_OVDIM oObjPeilutUpd;
            int iDakot = 0;
            try
            {

                for (j = 0; j < oSidur.htPeilut.Count; j++)
                {
                    if (iPeilutNesiaIndex != j)
                    {
                        oPeilut = (clPeilut)oSidur.htPeilut[j];
                        if (oPeilut.dFullShatYetzia == dShatYetzia)
                        {
                            if (!CheckPeilutObjectDelete(iSidurIndex, j))
                            {
                                if (!CheckIdkunRashemet("SHAT_YETZIA", oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia) && !isPeilutMashmautit(oPeilut))
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
                                    FindDuplicatPeiluyot_2(j, oPeilut.dFullShatYetzia, iSidurIndex, ref oSidur, ref oObjSidurimOvdimUpd);
                                }
                                else// if (!CheckPeilutObjectDelete(iSidurIndex, j) && !isPeilutMashmautit(oPeilut) && (CheckIdkunRashemet("SHAT_YETZIA", oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia)))
                                {
                                    for (int i = j - 1; i >= 0; i--)
                                    {
                                        if (iPeilutNesiaIndex != i)
                                        {
                                            oPeilut = (clPeilut)oSidur.htPeilut[i];
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
                                            oSidur.htPeilut[j] = oPeilut;
                                            //}
                                        }
                                    }
                                    iDakot += 1;
                                    dShatYetzia = dShatYetzia.AddMinutes(1);   
                                    FindDuplicatPeiluyot_2(iPeilutNesiaIndex, dShatYetzia, iSidurIndex, ref oSidur, ref oObjSidurimOvdimUpd);
                                }
                            }
                        }
                    }
                }
            
                int iMisparSidur = oSidur.iMisparSidur;
                DateTime dShatHatchalaSidur = oSidur.dFullShatHatchala;
                return iDakot;
                //oSidurWithCancled = _htEmployeeDetailsWithCancled.Values
                //                     .Cast<clSidur>()
                //                     .ToList()
                //                    .Find(sidur => (sidur.iMisparSidur == iMisparSidur && sidur.dFullShatHatchala == dShatHatchalaSidur));
                //if (oSidurWithCancled != null)
                //{
                //    for (j = 0; j < oSidurWithCancled.htPeilut.Count; j++)
                //    {
                //        oPeilut = (clPeilut)oSidurWithCancled.htPeilut[j];
                //        if (oPeilut.iBitulOHosafa == 1 && oPeilut.dFullShatYetzia == dShatYetzia)
                //        {
                //            oObjPeilutUpd = GetUpdPeilutObjectCancel(iSidurIndex, oPeilut, oObjSidurimOvdimUpd);
                //            oObjPeilutUpd.NEW_SHAT_YETZIA = oPeilut.dFullShatYetzia.AddMinutes(1);
                //            oObjPeilutUpd.UPDATE_OBJECT = 1;
                //            UpdateIdkunRashemet(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutUpd.NEW_SHAT_YETZIA);
                //            UpdateApprovalErrors(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutUpd.NEW_SHAT_YETZIA);

                //            oPeilut.dFullShatYetzia = oObjPeilutUpd.NEW_SHAT_YETZIA;
                //            oPeilut.sShatYetzia = oPeilut.dFullShatYetzia.ToString("HH:mm");
                //            oSidurWithCancled.htPeilut[j] = oPeilut;
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            { throw ex; }
        }

        //private void ChangeShatYetzitPeilut(ref clSidur oSidur,ref clPeilut oPeilut,ref OBJ_PEILUT_OVDIM oObjPeilutUpd)
        //{
        //    oObjPeilutUpd.NEW_SHAT_YETZIA = oPeilut.dFullShatYetzia.AddMinutes(-1);
        //    oObjPeilutUpd.UPDATE_OBJECT = 1;
        //    UpdateIdkunRashemet(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutUpd.NEW_SHAT_YETZIA);
        //    UpdateApprovalErrors(oSidur.iMisparSidur, oSidur.dFullShatHatchala, oPeilut.iMisparKnisa, oPeilut.dFullShatYetzia, oObjPeilutUpd.NEW_SHAT_YETZIA);

        //    oPeilut.dFullShatYetzia = oObjPeilutUpd.NEW_SHAT_YETZIA;
        //    oPeilut.sShatYetzia = oPeilut.dFullShatYetzia.ToString("HH:mm");
        //    oSidur.htPeilut[j] = oPeilut;
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
            clPeilut oPeilut;
            clSidur oSidur;
            bool bExist = false;
            try{
            //נמצא את הפעילות באובייקט פעילויות לעדכון
            oSidur = (clSidur)htEmployeeDetails[iSidurIndex];
            oPeilut = (clPeilut)oSidur.htPeilut[iPeilutIndex];
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

        private void SetZmanHashlama(ref OBJ_SIDURIM_OVDIM oObjSidurGriraUpd, clSidur oSidurKonenutGrira, int iGriraNum, clSidur oSidurGrira)
        {
            int iZmanHashlama = 0;
            int iMerchav = 0;
            float fSidurTime = 0;
            try
            {
                //אם זוהי גרירה ראשונה בפועל (זיהוי הסידור לפי הלוגיקה בסעיף סימון כוננות גרירה לא לתשלום) בתוך זמן סידור כוננות גרירה (זיהוי הסידור לפי הלוגיקה בסעיף סימון כוננות גרירה לא לתשלום) ואם סידור הכוננות הוא "מרחב צפון" (קוד הסניף שהוא 2 הספרות הראשונות של מספר הסידור קטן מ-25) וזמן הסידור (גמר - התחלה) פחות מהזמן המוגדר בפרמטר 164 (זמן גרירה מינימלי באזור צפון) אזי יש לסמן "2" בשדה "קוד השלמה". 

                if (!CheckIdkunRashemet("HASHLAMA", oSidurGrira.iMisparSidur, oSidurGrira.dFullShatHatchala))
                {
                    iMerchav = int.Parse((oSidurKonenutGrira.iMisparSidur).ToString().PadLeft(5, '0').Substring(0, 2));

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

        private void SetLoLetashlum(clSidur oSidurKonenutGrira)
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

        private void SetShatGmarGrira(ref OBJ_SIDURIM_OVDIM oObjSidurGriraUpd, clSidur oSidurKonenutGrira,  clSidur oSidurGrira)
        {
            int iMerchav;
            float fTime;
            try
            {

                iMerchav = int.Parse((oSidurKonenutGrira.iMisparSidur).ToString().PadLeft(5,'0').Substring(0, 2));

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

        private void SetBitulZmanNesiotObject(ref OBJ_SIDURIM_OVDIM oObjSidurGriraUpd,ref clSidur oSidurKonenutGrira, int iZmanNesia)
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

        private void SetBitulZmanNesiot(ref OBJ_SIDURIM_OVDIM oObjSidurGriraUpd, clSidur oSidurKonenutGrira,  clSidur oSidurGrira)
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

        private bool IsKonnutGrira(ref clSidur oSidur, DateTime dCardDate)
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
                        bSidurGrira = (drSugSidur[0]["sug_Avoda"].ToString() == clGeneral.enSugAvoda.Grira.GetHashCode().ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bSidurGrira;
        }

        private bool IsSugAvodaKupai(ref clSidur oSidur, DateTime dCardDate)
        {
            DataRow[] drSugSidur;
            bool bSidurKupai = false;
            try{
            //נבדוק אם סידור הוא מסוג קופאי
            if (!oSidur.bSidurMyuhad)
            {//סידור מיוחד
                bSidurKupai = (oSidur.sSugAvoda == clGeneral.enSugAvoda.Kupai.GetHashCode().ToString());
            }
            else
            {//סידור רגיל
                drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, dCardDate, _dtSugSidur);
                if (drSugSidur.Length > 0)
                {
                    bSidurKupai = (drSugSidur[0]["sug_Avoda"].ToString() == clGeneral.enSugAvoda.Kupai.GetHashCode().ToString());
                }
            }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bSidurKupai;
        }

        private bool IsActualKonnutGrira(ref clSidur oSidur, int iSidurKonnutGrira, out int iTypeGrira)
        {
            clSidur oSidurKonenutGrira;
            bool bSidurActualGrira = false;
            DateTime dKonenutShatHatchala, dKonenutShatGmar, dGriraShatHatchala, dGriraShatGmar;
            try
            {
                iTypeGrira = 0;
                if (oSidur.bSidurMyuhad)
                {//סידור מיוחד
                    if (oSidur.sSugAvoda == clGeneral.enSugAvoda.ActualGrira.GetHashCode().ToString() && oSidur.iLoLetashlum == 0)
                    {
                        oSidurKonenutGrira = (clSidur)htEmployeeDetails[iSidurKonnutGrira];
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
                        else if ((dGriraShatHatchala >= dKonenutShatHatchala) && (dGriraShatHatchala < dKonenutShatGmar )&& (dGriraShatGmar > dKonenutShatGmar))
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

        private void FixedMisparSidur01(ref clSidur oSidur, int iSidurIndex)
        {
            //const int SIDUR_NESIA  = 99300;
            //const int SIDUR_MATALA = 99301;
            int iMisparSidur = 0;
            int iCount = 0;
            clPeilut oPeilut;
            bool bHaveNesiatNamak=false;
            clKavim.enMakatType oMakatType;
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
            SourceObj SourceObject;
            int iNewMisparMatala, iNewMisparSidur;
            bool bUpdateMisparMatala = false;
            try
            {   //בדיקה מספר 1  - תיקון מספר סידור 
                //הסדרן בכל סניף יכול להוסיף לנהג "מטלות" אשר מסומנות בספרור 00001 - 00999. זה אינו מספר סידור חוקי ולכן הופכים אותו לחוקי עפ"י קוד הפעילות אשר הסדרן רשם למטלה זו. יהיה צורך לשמור בשדה נפרד את מספר המטלה המקורי
                bHaveNesiatNamak = oSidur.htPeilut.Values.Cast<clPeilut>().ToList().Any(Peilut => Peilut.iMakatType == clKavim.enMakatType.mNamak.GetHashCode());

                if ((oSidur.iMisparSidur >= 1 && oSidur.iMisparSidur <= 999) || (oSidur.bSectorVisaExists && bHaveNesiatNamak))
                {
                    if (oSidur.iMisparSidur != 99300)
                    {
                        for (int j = 0; j < ((KdsBatch.clSidur)(htEmployeeDetails[iSidurIndex])).htPeilut.Count; j++)
                        {
                            //תחת מטלה יכולה להיות פעילות אחת בלבד.
                            if (iCount > 0) break;
                            oPeilut = (clPeilut)oSidur.htPeilut[j];

                            iCount++;

                            oMakatType = (clKavim.enMakatType)oPeilut.iMakatType;
                            switch (oMakatType)
                            {
                                case clKavim.enMakatType.mKavShirut:
                                    //פעילות מסוג נסיעת שירות
                                    // אם הפעילות תחת המטלה  היא נסיעת שירות (לפי רוטינת זיהוי מקט), יש להכניס למספר סידור 99300
                                    iMisparSidur = SIDUR_NESIA;
                                    break;
                                case clKavim.enMakatType.mEmpty:
                                    //פעילות מסוג ריקה
                                    //אם הפעילות תחת המטלה היא ריקה (לפי רוטינת זיהוי מקט), יש לסמן את הסידור 99300 
                                    iMisparSidur = SIDUR_NESIA;
                                    break;
                                case clKavim.enMakatType.mNamak:
                                    //פעילות מסוג נמ"ק
                                    //אם הפעילות תחת המטלה היא נמ"ק (לפי רוטינת זיהוי מקט), יש לסמן את הסידור 99300 
                                    iMisparSidur = SIDUR_NESIA;
                                    break;
                                case clKavim.enMakatType.mElement:
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

                                oSidur = new clSidur(oSidur, _dCardDate, iNewMisparSidur, drSidurMeyuchad[0]);
                                oObjSidurimOvdimUpd.NEW_MISPAR_SIDUR = iNewMisparSidur;
                                oObjSidurimOvdimUpd.HASHLAMA = 0;
                                oObjSidurimOvdimUpd.OUT_MICHSA = 0;
                                oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                                oSidur.sHashlama = "0";
                                oSidur.sOutMichsa = "0";


                                for (j = 0; j < ((clSidur)(htEmployeeDetails[iSidurIndex])).htPeilut.Count; j++)
                                {
                                    //עבור שינוי 1, במידה והיה צורך לעדכן את מספר המטלה במספר הסידור הישן )מספר הסידור קיבל מספר חדש(
                                    //נעדכן את הפעילות הראשונה . במקרה כזה לא אמורה להיות יותר מפעילות אחת לסידור
                                    //נעדכן גם את הפעילויות במספר הסידור החדש
                                    oPeilut = (clPeilut)((clSidur)(htEmployeeDetails[iSidurIndex])).htPeilut[j];
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
            clSidur oSidurPrev;
            DateTime datePrevShatGmar;
            DateTime dateCurrShatHatchala;
            clSidur oSidur;
            OBJ_SIDURIM_OVDIM oPrevObjSidurimOvdimUpd;
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
            OBJ_PEILUT_OVDIM oObjPeilutUpd;
            clPeilut oPeilut;
            SourceObj SourceObject;
            int i;
            try
            {
                for (i = htEmployeeDetails.Count - 1; i > 0; i--)
                {
                    oSidur = (clSidur)htEmployeeDetails[i];
                    //נקרא את נתוני הסידור הקודם
                    oSidurPrev = (clSidur)htEmployeeDetails[i - 1];

                    if (oSidur.iLoLetashlum == 0 && oSidurPrev.iLoLetashlum == 0 &&
                        (!oSidur.bSidurMyuhad || (oSidur.bSidurMyuhad && IsMatala(oSidur))) && 
                         (!oSidurPrev.bSidurMyuhad || (oSidurPrev.bSidurMyuhad && IsMatala(oSidurPrev))) )
                    {
                        oObjSidurimOvdimUpd = GetUpdSidurObject(oSidur);
                        oPrevObjSidurimOvdimUpd = GetSidurOvdimObject(oSidurPrev.iMisparSidur, oSidurPrev.dFullShatHatchala);

                        if ((oPrevObjSidurimOvdimUpd.SHAT_GMAR != DateTime.MinValue) && (oObjSidurimOvdimUpd.SHAT_HATCHALA.Year > clGeneral.cYearNull))
                        {
                            datePrevShatGmar = oPrevObjSidurimOvdimUpd.SHAT_GMAR;
                            dateCurrShatHatchala = oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA;
                            if (dateCurrShatHatchala < datePrevShatGmar)
                            {//קיימת חפיפה בין הסידורים 
                                if (!CheckIdkunRashemet("SHAT_HATCHALA", oSidur.iMisparSidur, oSidur.dFullShatHatchala))
                                {
                                    if (oSidur.htPeilut.Values.Count > 0)
                                    {
                                        oPeilut = (clPeilut)oSidur.htPeilut[0];
                                        if (oPeilut.dFullShatYetzia.AddMinutes(-oPeilut.iKisuyTor) > oSidur.dFullShatHatchala)
                                        {
                                            clNewSidurim oNewSidurim = FindSidurOnHtNewSidurim(oSidur.iMisparSidur, oSidur.dFullShatHatchala);

                                            oNewSidurim.SidurIndex = i;
                                            oNewSidurim.SidurNew = oSidur.iMisparSidur;
                                            oNewSidurim.ShatHatchalaNew = oSidur.dFullShatHatchala.AddMinutes(Math.Min(Math.Abs((datePrevShatGmar - dateCurrShatHatchala).TotalMinutes), (oPeilut.dFullShatYetzia.AddMinutes(-oPeilut.iKisuyTor) - oSidur.dFullShatHatchala).TotalMinutes));

                                            UpdateObjectUpdSidurim(oNewSidurim);
                                            for (int j = 0; j < oSidur.htPeilut.Count; j++)
                                            {
                                                oPeilut = (clPeilut)oSidur.htPeilut[j];

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
                                        oPeilut = (clPeilut)oSidurPrev.htPeilut[oSidurPrev.htPeilut.Values.Count - 1];

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

        private bool IsMatala(clSidur oSidur)
        {
            clPeilut oPeilut;
            if (oSidur.bSidurMyuhad)
            {
                for (int j = 0; j < oSidur.htPeilut.Count; j++)
                {
                    oPeilut = (clPeilut)oSidur.htPeilut[j];

                    if (oPeilut.lMisparMatala > 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private void SidurNetzer17(ref clSidur oSidur,ref OBJ_SIDURIM_OVDIM  oObjSidurimOvdimUpd)
        {
            //אם סידור הוא סידור נצר (99203) (לפי ערך 11 (נ.צ.ר) במאפיין 52 (סוג עבודה) בטבלת סידורים מיוחדים)  ולעובד קיים מאפיין 64 והסידור אינו מסומן "לא לתשלום" 
            //- שותלים "חריגה" 3 (התחלה + גמר) ו"מחוץ למיכסה".

            try
            {   //אם סידור נצר
                if ((oSidur.sSugAvoda == clGeneral.enSugAvoda.Netzer.GetHashCode().ToString()) && (oMeafyeneyOved.Meafyen64Exists) && (oSidur.iLoLetashlum == 0))
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

                if ((oMeafyeneyOved.Meafyen63Exists) && (iSugYom == clGeneral.enSugYom.ErevYomHatsmaut.GetHashCode()) && (!bLoLetashlum))
                {
                    if (!IsSidurExits(99801))
                    {
                        dt = clWorkCard.GetMeafyeneySidurById(dCardDate, 99801);
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
            clSidur oSidur;
            for (int i = 0; i < htEmployeeDetails.Count; i++)
            {
                oSidur = (clSidur)htEmployeeDetails[i];
                if (oSidur.iMisparSidur == mispar_sidur)
                    return true;
            }
            return false;
        }
        private void DeleteElementRechev07(ref clSidur oSidur, ref clPeilut oPeilut, ref int iIndexPeilut,ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
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
                        if (oSidur.htPeilut.Count > 1)
                        {
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

        private void FixedMisparSidurToNihulTnua16(ref clSidur oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
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
                if (oSidur.sShaonNochachut == clGeneral.enShaonNochachut.enMinhal.GetHashCode().ToString())
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
                    oSidur = new clSidur(oSidur, _dCardDate, iNewMisparSidur, drSidurMeyuchad[0]);
                    oObjSidurimOvdimUpd.NEW_MISPAR_SIDUR = iNewMisparSidur;
                }
                //if (oMeafyeneyOved.Meafyen51Exists)
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

       

        private void SidurLoLetashlum11(DataRow[] drSugSidur, ref clSidur oSidur, int iSidurIndex,ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
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
                        //bSign = Condition1Saif11(ref oSidur);
                        //if (!bSign)
                        //{
                            //תנאי 2
                            //bSign = Condition2Saif11(ref oSidur);
                            //if (!bSign)
                            //{
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
                                            //bSign = Condition6Saif11(ref oSidur);
                                            //if (!bSign)
                                            //{
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
                                                
                                            //}
                                            //else
                                            //{
                                            //    iKodSibaLoLetashlum = 13;
                                            //}
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
                            //}
                            //else
                            //{
                            //    iKodSibaLoLetashlum = 14;
                            //}

                        //}
                        //else
                        //{
                        //    iKodSibaLoLetashlum = 2;
                        //}
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
        private bool Condition1Saif11(ref clSidur oSidur)
        {
            //תנאי ראשון לסעיף 11
            return ((oSidur.bSidurMyuhad) && (oSidur.sSugAvoda == clGeneral.enSugAvoda.VaadOvdim.GetHashCode().ToString()) && (clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, iSugYom, oSidur.dSidurDate)));
        }

        private bool Condition2Saif11(ref clSidur oSidur)
        {
            //תנאי שני לסעיף 11
            return ((oSidur.iMisparSidur == 99822) && (_oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || _oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode()) && ((oSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString())));
        }

        private bool Condition3Saif11(ref clSidur oSidur, int iSidurIndex)
        {
            //תנאי שלישי לסעיף 11
            bool bElementLeyedia = false;
            clPeilut oPeilut;
            try{
            if (oSidur.htPeilut.Count == 1)
            {
                oPeilut = (clPeilut)oSidur.htPeilut[0];
                if ((oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode()) && (oPeilut.iElementLeYedia == 2))
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

        private bool Condition4Saif11(ref clSidur oSidur)
        {
            //תנאי רביעי לסעיף 11
            bool bSign = false;
            try{
               // if ((oSidur.bSidurMyuhad) && (oSidur.sSectorAvoda == clGeneral.enSectorAvoda.Tafkid.GetHashCode().ToString()) && (clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, iSugYom, oSidur.dSidurDate)))
                if (clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, iSugYom, oSidur.dSidurDate) &&
                    (oSidur.bSidurMyuhad && oSidur.sShaonNochachut == "1" && oSidur.sChariga == "0"))
                        //וגם לעובד אין מאפיין 7 ו- 8 ברמה האישית. 
                        if (!oMeafyeneyOved.Meafyen7Exists && !oMeafyeneyOved.Meafyen8Exists)
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

        private bool Condition5Saif11(ref clSidur oSidur)
        {
            //תנאי חמישי לסעיף 11
            bool bSign = false;
            try{

                if ((oSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()) &&
                    (oSidur.bSidurMyuhad && oSidur.sShaonNochachut == "1" && oSidur.sChariga == "0"))
                        //וגם לעובד אין מאפיין 5 ו- 6 ברמה האישית. 
                        if (!oMeafyeneyOved.Meafyen5Exists && !oMeafyeneyOved.Meafyen6Exists)
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

        private bool Condition6Saif11(ref clSidur oSidur)
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
                    if ((oMeafyeneyOved.Meafyen56Exists) && ((oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode()) || (oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved6DaysInWeek2.GetHashCode())))
                    {
                        if (clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, iSugYom, oSidur.dSidurDate))
                        {
                            bSign = true;
                        }
                    }
                    //עובד 5 ימים 
                    if ((oMeafyeneyOved.Meafyen56Exists) && ((oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode()) || (oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())))
                    {
                        if (clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, iSugYom, oSidur.dSidurDate) || (oSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()))
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

        private bool Saif5EggedTaavura(DataRow[] drSugSidur, ref clSidur oSidur)
        {
            bool bSign = false;

            if (oOvedYomAvodaDetails.iKodHevra == clGeneral.enEmployeeType.enEggedTaavora.GetHashCode() && oSidur.bSidurNotValidKodExists)
            {
                bSign = true;
            }
            return bSign;
        }


        private bool ConditionSidurHeadrut(ref clSidur oSidur)
        {
            bool bLoLetashlumAutomati = false;
            if (!(oSidur.iLoLetashlum == 1 && oSidur.iKodSibaLoLetashlum==23) &&
                (iSugYom > 19 ||(iSugYom==10 && (oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || oMeafyeneyOved.iMeafyen56 == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode() )) ) )
            {
                if (oSidur.bSidurMyuhad)
                {//סידור מיוחד
                    if (!string.IsNullOrEmpty(oSidur.sHeadrutTypeKod))
                    {
                        if ((oSidur.sHeadrutTypeKod == clGeneral.enMeafyenSidur53.enMachala.GetHashCode().ToString()) ||
                            (oSidur.sHeadrutTypeKod == clGeneral.enMeafyenSidur53.enMilueim.GetHashCode().ToString()) ||
                            (oSidur.sHeadrutTypeKod == clGeneral.enMeafyenSidur53.enTeuna.GetHashCode().ToString()) ||
                            (oSidur.sHeadrutTypeKod == clGeneral.enMeafyenSidur53.enEvel.GetHashCode().ToString()))
                        {
                            bLoLetashlumAutomati = true;
                        }
                    }
                }

            }
            //תנאי שביעי לסעיף 11
          
            return bLoLetashlumAutomati;
        }
        private bool Condition7Saif11(DataRow[] drSugSidur, ref clSidur oSidur)
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
                    bLoLetashlumAutomati = (drSugSidur[0]["lo_letashlum_automati"].ToString() == clGeneral.enMeafyen79.LoLetashlumAutomat.GetHashCode().ToString());
                }
                else
                {
                    bLoLetashlumAutomati = false;
                }
            }
            return bLoLetashlumAutomati;
        }

        private bool Condition8Saif11(DataRow[] drSugSidur, ref clSidur oSidur)
        {
            bool bLoLetashlumAutomati = false;
   
             // וגם מדובר בסידור מיוחד עם עם מאפיין לסידורים מיוחדים קוד = 54 עם ערך 1.
            if (oSidur.bSidurMyuhad && oSidur.sShaonNochachut == "1" && oSidur.sChariga == "0") // && oOvedYomAvodaDetails.iIsuk.ToString().Substring(0,1) == "5")
            {
                //וגם לעובד אין מאפיין 3 ו- 4 ברמה האישית. 
                if (!oMeafyeneyOved.Meafyen3Exists && !oMeafyeneyOved.Meafyen4Exists)
                {
                    bLoLetashlumAutomati = true;
                }
            }

            return bLoLetashlumAutomati;
        }
 
        private bool Condition9Saif11(DataRow[] drSugSidur, ref clSidur oSidur)
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

        private bool CheckLoLetashlumMeafyenim(DataRow[] drSugSidur,clSidur oSidur, DateTime dShatHatchalaLetashlum, DateTime dShatGmarLetashlum,bool bFromMeafyenHatchala, bool bFromMeafyenGmar)
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
                            if (!oMeafyeneyOved.Meafyen42Exists && oMeafyeneyOved.Meafyen23Exists && oMeafyeneyOved.Meafyen24Exists){
                                if ((oSidur.dFullShatHatchala.Hour >= 11 && oSidur.dFullShatHatchala.Hour <= 17 && oSidur.dFullShatGmar > shaa)
                                    && (oSidur.sShabaton != "1" && (iSugYom >= clGeneral.enSugYom.Chol.GetHashCode() && iSugYom < clGeneral.enSugYom.Shishi.GetHashCode())))
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

        private void ChangeElement06(ref clPeilut oPeilut,
                                     ref clSidur oSidur, int iKey)
        {
            //int iLocalKey=1;
            //clPeilut oLocalPeilut;
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

                        oPeilut = new clPeilut(_iMisparIshi, _dCardDate, oPeilut, lElementNumber, dtTmpMeafyeneyElements);
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
                        //    //if (oLocalPeilut.iMakatType == clKavim.enMakatType.mEmpty.GetHashCode() ||
                        //    //    oLocalPeilut.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode() ||
                        //    //    oLocalPeilut.iMakatType == clKavim.enMakatType.mNamak.GetHashCode() ||
                        //    //    ((oLocalPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode()) && (oLocalPeilut.bBusNumberMustExists)))
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

        private void UpdateOutMichsa(ref clSidur oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            DataRow[] drSugSidur;
            //שינוי ברמת סידור
            //עדכון שדה מחוץ למכסה
            try
            {
                if (oOvedYomAvodaDetails.iKodHevra == clGeneral.enEmployeeType.enEggedTaavora.GetHashCode())
                {
                    oObjSidurimOvdimUpd.OUT_MICHSA = 0;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                    //oObjSidurimOvdimUpd.bUpdate = true;
                    oSidur.sOutMichsa = "0";
                }
                else
                {
                    if (oObjSidurimOvdimUpd.LO_LETASHLUM == 0 || (oObjSidurimOvdimUpd.LO_LETASHLUM == 1 && oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM==1))
                    {
                        if (!oSidur.bSidurMyuhad)
                        {
                            drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, oSidur.dSidurDate, _dtSugSidur);
                            if (drSugSidur.Length>0)
                            {
                                oSidur.sZakayMichutzLamichsa = drSugSidur[0]["zakay_michutz_lamichsa"].ToString();
                            }
                        }

                        if ((oSidur.sZakayMichutzLamichsa == clGeneral.enMeafyenSidur25.enZakaiAutomat.GetHashCode().ToString()))
                        {   //אם סידור הוא סידור מיוחד/מפה ויש לו ערך 3 במאפיין 25 (זכאי אוטומטית "מחוץ למכסה")
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
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 0, oSidur.iMisparIshi,oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "UpdateOutMichsa: " + ex.Message, null);
                _bSuccsess = false;
            }
        }

        private void UpdateChariga(ref clSidur oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            //עדכון שדה חריגה ברמת סידור
            //חריגה משעת התחלה/גמר	שינוי נתוני קלט:
            //1. הסידור מזכה אוטומטית (ערך 3 במאפיין 35) 
            try
            {
                if ((oObjSidurimOvdimUpd.LO_LETASHLUM == 0 || (oObjSidurimOvdimUpd.LO_LETASHLUM == 1 && oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM == 1)) && (oSidur.sZakaiLeChariga == clGeneral.enMeafyenSidur35.enCharigaAutomat.GetHashCode().ToString()))
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

        private void UpdateHashlamaForSidur(ref clSidur oSidur,int iSidurIndex, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
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

                //if (oOvedYomAvodaDetails.iKodHevra != clGeneral.enEmployeeType.enEggedTaavora.GetHashCode())
                //{
                //    //התנאים לקבלת ערך 1 - השלמה לשעה                          
                //    //כל הסידורים מסומנים ללא ערך בשדה השלמה (השלמה כלשהיא, לא משנה איזה ערך), אם קיימת -  עוצרים.
                //    if (!htEmployeeDetails.Values.Cast<clSidur>().ToList().Any(sidur => sidur.sHashlama != "0" && !string.IsNullOrEmpty(sidur.sHashlama)))
                //    {
                //        //מחפשים את הסידור הראשון ביום שהמשך שלו קטן מהערך בפרמטרים 101 - 103.
                //        //הסידור (101 (זמן מינימום לסידור חול להשלמה, 102 - זמן מינימום לסידור שישי/ע.ח להשלמה, 103 - זמן מינימום לסידור שבת/שבתון
                //        if (clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, iSugYom, oSidur.dSidurDate))
                //        { iParamHashlama = oParam.iHashlamaShabat; }
                //        else if ((oSidur.sErevShishiChag == "1") || (oSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()))
                //        { iParamHashlama = oParam.iHashlamaShisi; }
                //        else { iParamHashlama = oParam.iHashlamaYomRagil; }

                //        dZmanSidur = oSidur.dFullShatGmar.Subtract(oSidur.dFullShatHatchala).TotalMinutes;
                //        //אם משך הסידור או הערך בפרמטר המתאים ליום העבודה שווה או גדול משעה - עוצרים.
                //        if (dZmanSidur < iParamHashlama && iParamHashlama <= 60 && dZmanSidur <= 60)
                //        {
                           
                //            //. לסידור מאפיין 40 (לפי מספר סידור או סוג סידור)עם ערך 2 (זכאי אוטומטי) והסידור אינו מבוטל
                //            if (oSidur.bSidurMyuhad && oSidur.sHashlamaKod == "2")
                //            { bValidHashlama = true; }
                //            else //if (oSidur.bSidurRagilExists)
                //            {
                //                drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, oSidur.dSidurDate, _dtSugSidur);
                //                if (drSugSidur[0]["zakay_lehashlama_avur_sidur"].ToString() == "2")
                //                {
                //                    bValidHashlama = true;
                //                }
                //            }

                //            //אם הסידור אינו אחרון או בודד ביום
                //            if (htEmployeeDetails.Values.Count > 1 && iSidurIndex < htEmployeeDetails.Values.Count - 1)
                //            {
                //                //יש לבדוק האם שעת ההתחלה של הסידור העוקב שווה לשעת הגמר של הסידור שלנו +משך ההשלמה. אם כן - עוצרים.
                //                dZmanHashlama = double.Parse("60") - double.Parse(dZmanSidur.ToString());
                //                if (oSidur.dFullShatGmar.AddMinutes(dZmanHashlama) > ((clSidur)htEmployeeDetails[iSidurIndex + 1]).dFullShatHatchala)
                //                {
                //                    bValidHashlama = false;
                //                }
                //            }

                //            if(bValidHashlama)
                //            {
                //                oObjSidurimOvdimUpd.HASHLAMA = 1;
                //                oObjSidurimOvdimUpd.SUG_HASHLAMA = 1;
                //                oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;

                //                oSidur.sHashlama = "1";
                //                oSidur.iSugHashlama = 1;
                //            }
                //        }
                //    }
                //}
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
            clPeilut oPeilut;
            clSidur oSidur;
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
                        oSidur = ((clSidur)(htEmployeeDetails[i]));
                        CountPeiluyot = CountPeiluyot + oSidur.htPeilut.Count;
                        for (int j = 0; j < oSidur.htPeilut.Count; j++)
                        {
                            oPeilut = (clPeilut)oSidur.htPeilut[j];
                            if (dtMashar != null)
                            {
                                if (dtMashar.Select("bus_number=" + oPeilut.lOtoNo + " and SUBSTRING(convert(Vehicle_Type,'System.String'),3,2)<>64").Length > 0)
                                {
                                    bNotDegem64 = true;
                                    break;
                                }
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

        private bool IsSidurNahagut(DataRow[] drSugSidur, clSidur oSidur)
        {
            bool bSidurNahagut = false;

            //הפונקציה תחזיר TRUE אם הסידור הוא סידור נהגות

            try
            {
                if (oSidur.bSidurMyuhad)
                {//סידור מיוחד
                    bSidurNahagut = (oSidur.sSectorAvoda == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString());
                }
                else
                {//סידור רגיל
                    if (drSugSidur.Length > 0)
                    {
                        bSidurNahagut = (drSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString());
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

        private bool IsSidurNihulTnua(DataRow[] drSugSidur, clSidur oSidur)
        {
            bool bSidurNihulTnua = false;
            bool bElementZviraZman = false;  
            //הפונקציה תחזיר TRUE אם הסידור הוא סידור נהגות

            try
            {
                if (oSidur.bSidurMyuhad)
                {//סידור מיוחד
                    bSidurNihulTnua = (oSidur.sSectorAvoda == clGeneral.enSectorAvoda.Nihul.GetHashCode().ToString());
                    if (!bSidurNihulTnua)
                        if (oSidur.iMisparSidur == 99301)
                        { // oSidur.bMatalaKlalitLeloRechev
                    
                        clPeilut oPeilut = null;
                        for (int i = 0; i < oSidur.htPeilut.Count; i++)
                        {
                             oPeilut = (clPeilut)oSidur.htPeilut[i];
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
                        bSidurNihulTnua = (drSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nihul.GetHashCode().ToString());
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

        private bool IsSidurShonim(DataRow[] drSugSidur, clSidur oSidur)
        {
            bool bSidurZakaiLenesiot = false;

            try
            {
                if (oSidur.iLoLetashlum == 0)
                {
                    //הפונקציה תחזיר TRUE אם הסידור זכאי לזמן נסיעות
                    if (oSidur.bSidurMyuhad)
                    {//סידור מיוחד
                        bSidurZakaiLenesiot = ((oSidur.sShaonNochachut == clGeneral.enShaonNochachut.enMinhal.GetHashCode().ToString() || oSidur.sShaonNochachut == clGeneral.enShaonNochachut.enNetzer.GetHashCode().ToString() || oSidur.sShaonNochachut == clGeneral.enShaonNochachut.enGrira.GetHashCode().ToString()));
                    }
                    else
                    {//סידור רגיל
                        if (drSugSidur.Length > 0)
                        {
                            bSidurZakaiLenesiot = (drSugSidur[0]["shaon_nochachut"].ToString() == clGeneral.enShaonNochachut.enMinhal.GetHashCode().ToString() || drSugSidur[0]["shaon_nochachut"].ToString() == clGeneral.enShaonNochachut.enNetzer.GetHashCode().ToString() || drSugSidur[0]["shaon_nochachut"].ToString() == clGeneral.enShaonNochachut.enGrira.GetHashCode().ToString());
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

        private bool IsSidurHalbasha(clSidur oSidur)
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
        private bool IsNotSidurHalbasha(clSidur oSidur)
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
                //    if (iHeadrutTypeKod == clGeneral.enMeafyenSidur53.enMachala.GetHashCode())
                //    { oObjYameyAvodaUpd.SIBAT_HASHLAMA_LEYOM = 2; }
                //    else if (iHeadrutTypeKod == clGeneral.enMeafyenSidur53.enMilueim.GetHashCode())
                //    { oObjYameyAvodaUpd.SIBAT_HASHLAMA_LEYOM = 3; }
                //    else if (iHeadrutTypeKod == clGeneral.enMeafyenSidur53.enTeuna.GetHashCode())
                //    { oObjYameyAvodaUpd.SIBAT_HASHLAMA_LEYOM = 4; }
                //}
                //else
                //{
                    //עבור מותאם בנהגות (מזהים לפי ערך 2 או 3 בקוד נתון 8 (ערך מותאמות)  בטבלת פרטי עובד) שהוא מותאם לזמן קצר (מזהים מותאם לזמן קצר לפי קיום ערך בפרמטר 20 (זמן מותאמות) בטבלת פרטי עובדים) שעבד ביום (לוקחים את כל זמני הסידורים,  מחשבים גמר פחות התחלה וסוכמים) פחות מזמן המותאמות שלו (הערך בפרמטר 20) וההפרש בין זמן העבודה לזמן המותאמות קטן מהערך בפרמטר 153 (מינימום השלמה חריגה למותאם בנהגות).
                    if ((oOvedYomAvodaDetails.sMutamut == clGeneral.enMutaam.enMutaam2.GetHashCode().ToString()) || (oOvedYomAvodaDetails.sMutamut == clGeneral.enMutaam.enMutaam3.GetHashCode().ToString()))
                    {
                        clSidur oSidur;
                        if (htEmployeeDetails != null)
                        {
                            for (int i = 0; i < htEmployeeDetails.Count; i++)
                            {
                                oSidur = (clSidur)htEmployeeDetails[i];
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

        private bool IsHeadrutMachalaMiluimTeuna(clSidur oSidur, ref int iHeadrutTypeKod)
        {
            bool HeadrutMachalaMiluimTeuna = false;

            //הפונקציה תחזיר  אם הסידור הוא סידור העדרות מסוג מחלה/מילואים/תאונה  TRUE

            try
            {
                if (oSidur.bSidurMyuhad)
                {//סידור מיוחד
                    if (!string.IsNullOrEmpty(oSidur.sHeadrutTypeKod))
                    {
                      if ((oSidur.sHeadrutTypeKod == clGeneral.enMeafyenSidur53.enMachala.GetHashCode().ToString()) ||
                                                    (oSidur.sHeadrutTypeKod == clGeneral.enMeafyenSidur53.enMilueim.GetHashCode().ToString()) ||
                                                    (oSidur.sHeadrutTypeKod == clGeneral.enMeafyenSidur53.enTeuna.GetHashCode().ToString()))
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

        private bool IsSidurHeadrut(clSidur oSidur)
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

      
        private bool IsSidurShaon(clSidur oSidur)
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
                bSidurShaonKnisa = ((oSidur.bSidurMyuhad) && ((oSidur.sSugAvoda == clGeneral.enSugAvoda.Shaon.GetHashCode().ToString())));
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 0, oSidur.iMisparIshi, oSidur.dFullShatHatchala.Date, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "IsSidurShaon: " + ex.Message, null);
                _bSuccsess = false;
            }

            return bSidurShaonKnisa;
        }


        //private bool IsSidurShaonAndYetizaValid(clSidur oSidur)
        //{
        //    bool bSidurShaonYetiza = false;


        //    // אם יש החתמת שעון תקינה או ידנית עם אישור TRUE הפונקציה תחזיר 
        //    //נבדוק אם סידור שעון
        //    //מזהים סידור שעונים לפי ערך 1 (שעונים) במאפיין 52 (סוג עבודה) בטבלת סידורים מיוחדים

        //   //. זיהוי החתמת שעון יציאה, שני מקרים שונים: 
        //   //א. החתמת שעון - קיום ערך בשדה שעת גמר של הסידור הנבדק  וקיום ערך בשדה מיקום שעון יציאה.
        //   //ב.  דיווח ידני של שעת יציאה - קיום ערך בשדה שעת גמר של הסידור הנבדק  וקיום סיבת דיווח ידני שמאפשרת מתן זמן הלבשה (לוקחים את קוד סיבה לדיווח ידני ובודקים בטבלת סיבות לדיווח ידני האם אין לו ערך 1 בשדה  Goremet_Lebitul_Zman_Nesiaa)  וקיום אישור לדיווח החתמת שעון (קוד אישור 1 או 3 עם סטטוס אישור ברמה הכי גבוהה). 

        //    try
        //    {
        //        if (oSidur.bSidurMyuhad)
        //        {//סידור מיוחד
        //            if ((oSidur.sSugAvoda == clGeneral.enSugAvoda.Shaon.GetHashCode().ToString()))
        //            {
        //                bSidurShaonYetiza = IsYetizaValid(ref oSidur, SIBA_LE_DIVUCH_YADANI_NESIAA);
        //            }
        //        }
        //        return bSidurShaonYetiza;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        private bool IsKnisaValid( clSidur oSidur, string sBitulTypeField,bool bSidurHaveNahagut)
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
                    else if ((sBitulTypeField == SIBA_LE_DIVUCH_YADANI_NESIAA && oMeafyeneyOved.Meafyen51Exists) || (sBitulTypeField != SIBA_LE_DIVUCH_YADANI_NESIAA))
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

        private bool IsYetizaValid( clSidur oSidur, string sBitulTypeField,bool bSidurHaveNahagut)
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
                    else if ((sBitulTypeField==SIBA_LE_DIVUCH_YADANI_NESIAA && oMeafyeneyOved.Meafyen51Exists) ||(sBitulTypeField!=SIBA_LE_DIVUCH_YADANI_NESIAA))
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
            clSidur oSidur;
            int iErechMeafyen = 0;
            bool bKnisaValid = false;
            bool bYetizaValid = false;
            int iSidurZakaiLenesiaKnisa = -1;
            int iSidurZakaiLenesiaYetzia = -1;
            int iFirstMezake = -1, iLastMezake = -1;
            string sMefyen14 = "";
            bool bSidurRelevanti = true;
            //עדכון שדה ביטול זמן נסיעות ברמת יום עבודה
            try
            {
                //אם אין מאפיין נסיעות (51, 61) - נעדכן ל0- 
               
                 if (oOvedYomAvodaDetails.iKodHevra == clGeneral.enEmployeeType.enEggedTaavora.GetHashCode() && (!oMeafyeneyOved.Meafyen51Exists) && (!oMeafyeneyOved.Meafyen61Exists))
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
                 else if ((!oMeafyeneyOved.Meafyen51Exists) && (!oMeafyeneyOved.Meafyen61Exists))
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
                 //else if ((oMeafyeneyOved.Meafyen61Exists || oMeafyeneyOved.Meafyen51Exists) && !bSidurShaon)
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

                  //    if (oMeafyeneyOved.Meafyen51Exists && CheckIdkunRashemet("BITUL_ZMAN_NESIOT"))
                 //    {
                 //        iZmanNesia = int.Parse(oMeafyeneyOved.sMeafyen51.ToString().PadRight(3, char.Parse("0")).Substring(1));
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
                             oSidur = (clSidur)htEmployeeDetails[i];
                             drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, dCardDate, _dtSugSidur);

                             bSidurZakaiLnesiot = IsSidurShonim(drSugSidur, oSidur);

                             sMefyen14=oSidur.sZakayLezamanNesia;
                             if (!oSidur.bSidurMyuhad && drSugSidur.Length>0) sMefyen14 = drSugSidur[0]["zakay_leaman_nesia"].ToString();

                             bSidurRelevanti = true;
                             if(IsSidurNihulTnua(drSugSidur, oSidur))
                             {
                                 if(oOvedYomAvodaDetails.iIsuk==401 || oOvedYomAvodaDetails.iIsuk==402 ||  oOvedYomAvodaDetails.iIsuk==403 ||
                                     oOvedYomAvodaDetails.iIsuk == 404 || oOvedYomAvodaDetails.iIsuk == 421 || oOvedYomAvodaDetails.iIsuk == 422 || oOvedYomAvodaDetails.iIsuk == 17)
                                      bSidurRelevanti = true;
                                 else bSidurRelevanti = false;
                             }
                             if (!bSidurZakaiLnesiot && sMefyen14 == "1" && oSidur.iLoLetashlum == 0 && bSidurRelevanti)
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
                                     bKnisaValid = IsKnisaValid((clSidur)htEmployeeDetails[iFirstMezake], SIBA_LE_DIVUCH_YADANI_NESIAA, bSidurNahagut);
                                     if (!bKnisaValid)
                                         iFirstMezake=-1;
                                 }
                                 if (iLastMezake > -1 && iLastMezake == i)
                                 {
                                     bYetizaValid = IsYetizaValid((clSidur)htEmployeeDetails[iLastMezake], SIBA_LE_DIVUCH_YADANI_NESIAA, bSidurNahagut);
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

                                         oSidur = (clSidur)htEmployeeDetails[iSidurZakaiLenesiaKnisa];
                                         oObjSidurimOvdimUpd = GetSidurOvdimObject(oSidur.iMisparSidur, oSidur.dFullShatHatchala);

                                         oObjSidurimOvdimUpd.MEZAKE_NESIOT = ZmanNesiotType.ZakaiKnisa.GetHashCode();
                                         oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;

                                     }
                                     //עבור מאפיין 51: 
                                     //אם שדה נסיעות התעדכן בערך 1, אז יש לעדכן את שדה זמן נסיעה הלוך בטבלת ימי עבודה עובדים בערך הזמן ממאפיין 51
                                     if (IsOvedZakaiLZmanNesiaLaAvoda() && (!CheckIdkunRashemet("ZMAN_NESIA_HALOCH")))
                                     {
                                         if ((oMeafyeneyOved.Meafyen61Exists) && iSidurZakaiLenesiaKnisa > -1)
                                         {
                                             //עבור מאפיין 61:
                                             //אם שדה נסיעות התעדכן בערך 1 ויש ערך בשדה מיקום שעון כניסה בסידור הראשון ביום, יש לעדכן את שדה זמן נסיעה הלוך בערך מטבלה זמן נסיעה משתנה.                                        
                                             iZmanNesia = GetZmanNesiaMeshtana(iSidurZakaiLenesiaKnisa, 1, dCardDate);
                                             if (iZmanNesia > -1)
                                             { oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = (int)(Math.Ceiling(iZmanNesia / 2.0)); }
                                         }
                                         if (oMeafyeneyOved.Meafyen51Exists)
                                         {
                                             iErechMeafyen = int.Parse(oMeafyeneyOved.sMeafyen51.ToString().Substring(0, 1));
                                             iZmanNesia = int.Parse(oMeafyeneyOved.sMeafyen51.Substring(1));
                                             if (iErechMeafyen == 1)
                                             {
                                                 oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = iZmanNesia;
                                             }
                                             if (iErechMeafyen == 3)
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

                                         oSidur = (clSidur)htEmployeeDetails[iSidurZakaiLenesiaYetzia];
                                         oObjSidurimOvdimUpd = GetSidurOvdimObject(oSidur.iMisparSidur, oSidur.dFullShatHatchala);

                                         oObjSidurimOvdimUpd.MEZAKE_NESIOT = ZmanNesiotType.ZakaiYetiza.GetHashCode();
                                         oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                                     }
                                     if (IsOvedZakaiLZmanNesiaMeAvoda() && (!CheckIdkunRashemet("ZMAN_NESIA_HAZOR")))
                                     {
                                         if ((oMeafyeneyOved.Meafyen61Exists) && (htEmployeeDetails.Count > 0) && iSidurZakaiLenesiaYetzia > -1)
                                         {
                                             //נשלוף את הסידור האחרון
                                             iZmanNesia = GetZmanNesiaMeshtana(iSidurZakaiLenesiaYetzia, 2, dCardDate);
                                             oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = (int)(Math.Ceiling(iZmanNesia / 2.0));
                                         }
                                         if (oMeafyeneyOved.Meafyen51Exists)
                                         {
                                             iZmanNesia = int.Parse(oMeafyeneyOved.sMeafyen51.ToString().PadRight(3, char.Parse("0")).Substring(1));
                                             iErechMeafyen = int.Parse(oMeafyeneyOved.sMeafyen51.ToString().Substring(0, 1));

                                             if (iErechMeafyen == 2)
                                             {
                                                 oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = iZmanNesia ;
                                             }
                                             if (iErechMeafyen == 3)
                                             {
                                                 oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = (int)(Math.Ceiling(iZmanNesia / 2.0));
                                             }
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
                                             oSidur = (clSidur)htEmployeeDetails[iSidurZakaiLenesiaKnisa];
                                             oObjSidurimOvdimUpd = GetSidurOvdimObject(oSidur.iMisparSidur, oSidur.dFullShatHatchala);

                                             oObjSidurimOvdimUpd.MEZAKE_NESIOT = ZmanNesiotType.ZakaiKnisaYetiza.GetHashCode();
                                             oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                                         }
                                     }
                                 }
                                 if ((oMeafyeneyOved.Meafyen61Exists) && (htEmployeeDetails.Count > 0) && (iSidurZakaiLenesiaKnisa > -1 || iSidurZakaiLenesiaYetzia > -1))
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

                                 if (oMeafyeneyOved.Meafyen51Exists)
                                 {
                                     iZmanNesia = int.Parse(oMeafyeneyOved.sMeafyen51.ToString().PadRight(3, char.Parse("0")).Substring(1));
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
           clSidur oSidur;
           try
           {
               if (htEmployeeDetails.Count > 0)
               {
                   oSidur = (clSidur)htEmployeeDetails[iSidurIndex];

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
            clSidur oSidur;
            bool bHaveHalbasha = false;
            bool bKnisaValid = false;
            bool bYetizaValid=false;
            //עדכון שדה הלבשה ברמת יום עבודה
            try
            {
                //הלבשה

                if (oOvedYomAvodaDetails.iKodHevra == clGeneral.enEmployeeType.enEggedTaavora.GetHashCode() )
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
                    if (!oMeafyeneyOved.Meafyen44Exists)
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
                                oSidur = (clSidur)htEmployeeDetails[i];
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
                                if (oOvedYomAvodaDetails.iKodHevra == clGeneral.enEmployeeType.enEggedTaavora.GetHashCode())
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
                                    oSidur = (clSidur)htEmployeeDetails[iSidurZakaiLehalbashaKnisa];
                                    oObjSidurimOvdimUpd = GetSidurOvdimObject(oSidur.iMisparSidur, oSidur.dFullShatHatchala);

                                    oObjSidurimOvdimUpd.MEZAKE_HALBASHA = ZmanHalbashaType.ZakaiKnisa.GetHashCode(); ;
                                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                                }

                                if (iSidurZakaiLehalbashaYetzia > -1 && iSidurZakaiLehalbashaKnisa != iSidurZakaiLehalbashaYetzia)
                                {
                                    oSidur = (clSidur)htEmployeeDetails[iSidurZakaiLehalbashaYetzia];
                                    oObjSidurimOvdimUpd = GetSidurOvdimObject(oSidur.iMisparSidur, oSidur.dFullShatHatchala);

                                    oObjSidurimOvdimUpd.MEZAKE_HALBASHA = ZmanHalbashaType.ZakaiYetiza.GetHashCode(); ;
                                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                                }

                                if (iSidurZakaiLehalbashaYetzia > -1 && iSidurZakaiLehalbashaKnisa == iSidurZakaiLehalbashaYetzia)
                                {
                                    oSidur = (clSidur)htEmployeeDetails[iSidurZakaiLehalbashaYetzia];
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
            return ((oMeafyeneyOved.Meafyen61Exists && oMeafyeneyOved.sMeafyen61.Substring(0, 1) == "1")
                   ||
                   (oMeafyeneyOved.Meafyen51Exists && oMeafyeneyOved.sMeafyen51.Substring(0, 1) == "1"));
        }

        private bool IsOvedZakaiLZmanNesiaMeAvoda()
        {
            //לעובד מאפיין 51/61 (מאפיין זמן נסיעות) והעובד זכאי רק לזמן נסיעות מהעבודה (ערך 2 בספרה הראשונה של מאפיין זמן נסיעות            
            return ((oMeafyeneyOved.Meafyen61Exists && oMeafyeneyOved.sMeafyen61.Substring(0, 1) == "2" )
                   ||
                   (oMeafyeneyOved.Meafyen51Exists && oMeafyeneyOved.sMeafyen51.Substring(0, 1) == "2"));
        }
        
        private bool IsOvedZakaiLZmanNesiaLeMeAvoda()
        {
            //לעובד מאפיין 51/61 (מאפיין זמן נסיעות) והעובד זכאי רק לזמן נסיעות מהעבודה (ערך 3 בספרה הראשונה של מאפיין זמן נסיעות            
            return ((oMeafyeneyOved.Meafyen61Exists && oMeafyeneyOved.sMeafyen61.Substring(0, 1) == "3")
                   ||
                   (oMeafyeneyOved.Meafyen51Exists && oMeafyeneyOved.sMeafyen51.Substring(0, 1) == "3"));
        }

        //private float CalcOvedDakotInYafkid(DateTime dCardDate)
        //{
        //    //DataRow[] dr;
        //    float fDakotInTafkid = 0;
        //    object oErechRechiv;
        //    try{
        //    //if (oOvedYomAvodaDetails.sShabaton == clGeneral.enDay.Shabat.GetHashCode().ToString())
        //    //{
        //    //    dr = dtChishuvYom.Tables["CHISHUV_YOM"].Select("kod_rechiv=" + clGeneral.enRechivim.DakotTafkidShabat.GetHashCode().ToString());
        //    //}
        //    //else
        //    //{
        //    //    dr = dtChishuvYom.Tables["CHISHUV_YOM"].Select("kod_rechiv=" + clGeneral.enRechivim.DakotTafkidChol.GetHashCode().ToString());
        //    //}
        //    //if (dr.Length > 0)
        //    //{
        //    //    iDakotInTafkid = System.Convert.IsDBNull(dr[0]["erech_rechiv"].ToString()) ? 0 : int.Parse(dr[0]["erech_rechiv"].ToString());
        //    //}

        //    //ערך רכיבים 4+37+193
        //    oErechRechiv = dtChishuvYom.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidChol.GetHashCode().ToString());
        //    fDakotInTafkid = oErechRechiv.Equals(System.DBNull.Value) ? 0 : float.Parse(oErechRechiv.ToString());

        //    oErechRechiv = dtChishuvYom.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.DakotTafkidShabat.GetHashCode().ToString());
        //    fDakotInTafkid += oErechRechiv.Equals(System.DBNull.Value) ? 0 : float.Parse(oErechRechiv.ToString());

        //    oErechRechiv = dtChishuvYom.Tables["CHISHUV_YOM"].Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.SachDakotTafkidShishi.GetHashCode().ToString());
        //    fDakotInTafkid += oErechRechiv.Equals(System.DBNull.Value) ? 0 : float.Parse(oErechRechiv.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //    return fDakotInTafkid;
        //}

        //private void InsertSidurRetzifut14(DateTime dCardDate, int iCurrSidurIndex, DataRow[] drSugSidur,
        //                                   ref clSidur oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        //{
        //    clSidur oSidurPrev;
        //    clSidur oSidurNew;
        //   float fDiffTimeBetweenSidurim = 0;
        //   clPeilut oElement;
        //    try
        //    {
        //        //הוספת סידור - שינוי ברמת סידור
        //        //בין 2 סידורים מגיעה לנהג רציפות עפ"י אלגוריתם המורכב מיום בשבוע , סוג הסידור , מרווח בין סידורים ועוד.
        //        //במקרים בהם יש פער זמנים בין שני סידורי עבודה המזכה את העובד בתוספת זמן עבודה עבור פער זה (ומאחד למעשה את 2 הסידורים), יש לפתוח סידור רציפות. ישנם שני סוגים של סידורי רציפות: רציפות נהגות  ורציפות שאינה נהגות. פתיחת סידור רציפות נהגות, 99500:
        //        //מזהים פער זמנים בין  שני סידורי נהגות (מזהים סידור נהגות לפי ערך 5 בטבלת סידורים מיוחדים/מאפייני סוג סידור) הגדול מהערך בפרמטר 0104 (פער הזמן).
        //        //פתיחת סידור רציפות שאינה נהגות, 99501:
        //        //מזהים בין  שני סידורים שלפחות אחד מהם אינו נהגות  פער זמנים הגדול מהערך בפרמטר 0104 (פער הזמן).
        //        //את שני הסידורים פותחים עם השעות הבאות:
        //        //א. שעת התחלה סידור רציפות - שעת גמר של הסידור הקודם לסידור הרציפות + דקה).
        //        //ב.  שעת גמר סידור רציפות - שעת התחלה של הסידור העוקב לרציפות פחות דקה).


        //        oSidurPrev = (clSidur)htEmployeeDetails[iCurrSidurIndex];
        //       DataRow[] drPrevSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidurPrev.iSugSidurRagil, dCardDate,  _dtSugSidur);
        //        bool bSidurNahagut = false;
        //        bool bSidurPrevNahagut = false;
        //        bool bSidurTafkidDoreshRetzifut = false;

        //        // אם בסידור הראשון יש אלמנט המתנה 724xxx00 , עוצרים.
        //        oElement = oSidurPrev.htPeilut.Values.Cast<clPeilut>().ToList().FirstOrDefault(oPeilut => (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "724"));
        //        if (oElement==null)
        //        {
        //            if (oSidur.dFullShatHatchala > oSidurPrev.dFullShatGmar)
        //            { //אם שני סידורי נהגות וגם פער הזמנים בין הסידורים גדול מפרמטר 104, נכניס סידור רציפות

        //                if (oSidur.bSidurMyuhad && oSidur.bSidurNahagut)
        //                {
        //                    bSidurNahagut = true;

        //                }
        //                else if (drSugSidur.Length > 0)
        //                {
        //                    if (drSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString())
        //                    {
        //                        bSidurNahagut = true;
        //                    }
        //                }


        //                if (oSidurPrev.bSidurMyuhad && oSidurPrev.bSidurNahagut)
        //                {
        //                    bSidurPrevNahagut = true;
        //                }
        //                else if (drPrevSugSidur.Length > 0)
        //                {
        //                    if (drPrevSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString())
        //                    {
        //                        bSidurPrevNahagut = true;

        //                    }
        //                }

        //                //מזהים פער זמנים בין סידור נהגות (מזהים סידור נהגות לפי ערך 5 בטבלת סידורים מיוחדים/מאפייני סוג סידור) לסידור תפקיד שמזכה ברציפות (לפי מאפיין 26 עם ערך 1). 
        //                if ((!bSidurPrevNahagut && bSidurNahagut) || (bSidurPrevNahagut && !bSidurNahagut))
        //                {
        //                    if (!bSidurPrevNahagut)
        //                    {
        //                        if (oSidurPrev.bSidurMyuhad && oSidurPrev.bSidurTafkid && oSidurPrev.iZakayLepizul == 1)
        //                        {
        //                            bSidurTafkidDoreshRetzifut = true;
        //                        }
        //                        else if (drPrevSugSidur.Length > 0)
        //                        {
        //                            if (drPrevSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Tafkid.GetHashCode().ToString() && drPrevSugSidur[0]["zakay_lepizul"].ToString() == "1")
        //                            {
        //                                bSidurTafkidDoreshRetzifut = true;
        //                            }
        //                        }
        //                    }
        //                    else if (!bSidurNahagut)
        //                    {
        //                        if (oSidur.bSidurMyuhad && oSidur.bSidurTafkid && oSidur.iZakayLepizul == 1)
        //                        {
        //                            bSidurTafkidDoreshRetzifut = true;
        //                        }
        //                        else if (drSugSidur.Length > 0)
        //                        {
        //                            if (drSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Tafkid.GetHashCode().ToString() && drSugSidur[0]["zakay_lepizul"].ToString() == "1")
        //                            {
        //                                bSidurTafkidDoreshRetzifut = true;
        //                            }
        //                        }
        //                    }
        //                }


        //                //אם לא היה מצב של שני סידורי נהגות ברציפות, נחשב את הפרש הזמן בין שני הסידורים 
        //                //ונשווה מול פרמטר 104
        //                fDiffTimeBetweenSidurim = clDefinitions.GetTimeBetweenTwoSidurimInMinuts(oSidurPrev, oSidur);
        //                if (fDiffTimeBetweenSidurim >= 1 && fDiffTimeBetweenSidurim <= oParam.iMinTimeBetweenSidurim)
        //                {
        //                    if ((bSidurNahagut && bSidurPrevNahagut) || (bSidurTafkidDoreshRetzifut && (bSidurNahagut || bSidurPrevNahagut)))
        //                    {

        //                        oObjSidurimOvdimIns = new OBJ_SIDURIM_OVDIM();
        //                        oObjSidurimOvdimIns.MISPAR_ISHI = _iMisparIshi;
        //                        oObjSidurimOvdimIns.TAARICH = _dCardDate;
        //                        oObjSidurimOvdimIns.MISPAR_SIDUR = SIDUR_RETIZVUT99500;
        //                        oObjSidurimOvdimIns.SHAT_HATCHALA = clGeneral.GetDateTimeFromStringHour(oSidurPrev.sShatGmar, oSidurPrev.dFullShatGmar);
        //                        oObjSidurimOvdimIns.SHAT_GMAR = clGeneral.GetDateTimeFromStringHour(oSidur.sShatHatchala, oSidur.dFullShatHatchala);
        //                        oObjSidurimOvdimIns.SHAT_HATCHALA_LETASHLUM = oObjSidurimOvdimIns.SHAT_HATCHALA;
        //                        oObjSidurimOvdimIns.SHAT_GMAR_LETASHLUM = oObjSidurimOvdimIns.SHAT_GMAR;
        //                        oObjSidurimOvdimIns.OUT_MICHSA = 0;
        //                        oObjSidurimOvdimIns.HASHLAMA = 0;
        //                        oObjSidurimOvdimIns.CHARIGA = 0;
        //                        oObjSidurimOvdimIns.BITUL_O_HOSAFA = 4;
        //                        oCollSidurimOvdimIns.Add(oObjSidurimOvdimIns);

        //                        DataRow[] drSidurMeyuchad;
        //                        drSidurMeyuchad = _dtSidurimMeyuchadim.Select("mispar_sidur=" + oObjSidurimOvdimIns.MISPAR_SIDUR);
        //                        oSidurNew = new clSidur(_iMisparIshi, _dCardDate, oObjSidurimOvdimIns.MISPAR_SIDUR, drSidurMeyuchad[0]);
        //                        oSidurNew.dFullShatHatchala = oObjSidurimOvdimIns.SHAT_HATCHALA;
        //                        oSidurNew.sShatHatchala = oSidurNew.dFullShatHatchala.ToString("HH:mm");
        //                        oSidurNew.dFullShatGmar = oObjSidurimOvdimIns.SHAT_GMAR;
        //                        oSidurNew.sShatGmar = oSidurNew.dFullShatGmar.ToString("HH:mm");
        //                        oSidurNew.sHashlama = "0";
        //                        oSidurNew.sChariga = "0";
        //                        oSidurNew.sOutMichsa = "0";
        //                        oSidurNew.iBitulOHosafa = 4;
        //                        htEmployeeDetails.Insert(iCurrSidurIndex + 1, long.Parse(string.Concat(oObjSidurimOvdimIns.SHAT_HATCHALA.ToString("ddMM"),oObjSidurimOvdimIns.SHAT_HATCHALA.ToString("HH:mm").Replace(":", ""), oObjSidurimOvdimIns.MISPAR_SIDUR)), oSidurNew);

        //                    }
        //                    //אין לפתוח רציפות אם הסידור שאינו נהגות הינו סידור היעדרות = ערך כלשהו במאפיין 53 בטבלת מאפייני סידורים מיוחדים
        //                    //else if ((!bSidurNahagut && oSidur.sHeadrutTypeKod == "" && bSidurPrevNahagut) || (!bSidurPrevNahagut && oSidurPrev.sHeadrutTypeKod == "" && bSidurNahagut) ||
        //                    //    !bSidurNahagut && oSidur.sHeadrutTypeKod == "" && !bSidurPrevNahagut && oSidurPrev.sHeadrutTypeKod == "")
        //                    //{
        //                    //    oObjSidurimOvdimIns = new OBJ_SIDURIM_OVDIM();
        //                    //    oObjSidurimOvdimIns.MISPAR_ISHI = _iMisparIshi;
        //                    //    oObjSidurimOvdimIns.TAARICH = _dCardDate;
        //                    //    oObjSidurimOvdimIns.MISPAR_SIDUR = SIDUR_RETIZVUT99501;
        //                    //    oObjSidurimOvdimIns.SHAT_HATCHALA = clGeneral.GetDateTimeFromStringHour(oSidurPrev.sShatGmar,dCardDate).AddMinutes(1);
        //                    //    oObjSidurimOvdimIns.SHAT_GMAR =clGeneral.GetDateTimeFromStringHour(oSidur.sShatHatchala,dCardDate).AddMinutes(-1);
        //                    //    oObjSidurimOvdimIns.OUT_MICHSA = 0;
        //                    //    oObjSidurimOvdimIns.HASHLAMA = 0;
        //                    //    oObjSidurimOvdimIns.CHARIGA = 0;
        //                    //    oObjSidurimOvdimIns.BITUL_O_HOSAFA = 4;
        //                    //    oCollSidurimOvdimIns.Add(oObjSidurimOvdimIns);

        //                    //    DataRow[] drSidurMeyuchad;
        //                    //    drSidurMeyuchad = _dtSidurimMeyuchadim.Select("mispar_sidur=" + oObjSidurimOvdimIns.MISPAR_SIDUR);
        //                    //    oSidurNew = new clSidur(_iMisparIshi, _dCardDate, oObjSidurimOvdimIns.MISPAR_SIDUR, drSidurMeyuchad[0]);
        //                    //    oSidurNew.dFullShatHatchala = oObjSidurimOvdimIns.SHAT_HATCHALA;
        //                    //    oSidurNew.sShatHatchala = oSidurNew.dFullShatHatchala.ToLongTimeString();
        //                    //    oSidurNew.dFullShatGmar = oObjSidurimOvdimIns.SHAT_GMAR;
        //                    //    oSidurNew.sShatGmar = oSidurNew.dFullShatGmar.ToString("HH:mm");
        //                    //    oSidurNew.sHashlama = "0";
        //                    //    oSidurNew.sChariga = "0";
        //                    //    oSidurNew.sOutMichsa = "0";
        //                    //    oSidurNew.iBitulOHosafa = 4;
        //                    //    htEmployeeDetails.Insert(iCurrSidurIndex + 1, long.Parse(string.Concat(oObjSidurimOvdimIns.SHAT_HATCHALA.ToString("HH:mm").Replace(":", ""), oObjSidurimOvdimIns.MISPAR_SIDUR)), oSidurNew);
        //                    //}

        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 14, oSidur.iMisparIshi,oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "InsertSidurRetzifut14: " + ex.Message, null);
        //        _bSuccsess = false;
        //    }
        //}

         /******************************/
        private void SetHourToSidur19(ref clSidur oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, bool bIdkunRashShatHatchala, bool bIdkunRashShatGmar)
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
                    if (oSidur.dFullShatHatchalaLetashlum.Year < clGeneral.cYearNull)
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

        private void SetShatHatchalaGmarKizuz(ref clSidur oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, DateTime dShatHatchalaLetashlum, DateTime dShatGmarLetashlum, ref DateTime dShatHatchalaLetashlumToUpd, ref DateTime dShatGmarLetashlumToUpd)
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

        private void SetShaotHatchalaGmar(ref clSidur oSidur, DateTime dShatHatchalaLetashlum, DateTime dShatGmarLetashlum, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, ref DateTime dShatHatchalaLetashlumToUpd, ref DateTime dShatGmarLetashlumToUpd)
        {
            //א. אם שעת הגמר של הסידור לא גדולה משעת מאפיין ההתחלה המותרת (המאפיין תלוי בסוג היום, ראה עמודה  שדות מעורבים): יש לסמן את הסידור "לא לתשלום"
            //ב. אם שעת ההתחלה של הסידור לא קטנה משעת מאפיין הגמר המותרת (המאפיין תלוי בסוג היום, ראה עמודה  שדות מעורבים) - יש לסמן את הסידור "לא לתשלום                            
            try
            {
                if ((oObjSidurimOvdimUpd.SHAT_GMAR != DateTime.MinValue && (oObjSidurimOvdimUpd.SHAT_GMAR <= dShatHatchalaLetashlum)) || (oObjSidurimOvdimUpd.SHAT_HATCHALA >= dShatGmarLetashlum))
                {
                    //oObjSidurimOvdimUpd.LO_LETASHLUM = 1;
                    //oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM = 10;
                    if (dShatHatchalaLetashlum.Year > clGeneral.cYearNull)
                    {
                        dShatHatchalaLetashlumToUpd = dShatHatchalaLetashlum;
                    }
                    if (dShatGmarLetashlum.Year > clGeneral.cYearNull)
                    {
                        dShatGmarLetashlumToUpd = dShatGmarLetashlum;
                    }
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                    //oSidur.iLoLetashlum = 1;
                }
                else
                {
                    //א.ב.ג
                    if ((oObjSidurimOvdimUpd.SHAT_GMAR<= dShatHatchalaLetashlum)
                        || (oObjSidurimOvdimUpd.SHAT_HATCHALA >= dShatGmarLetashlum)
                        ||  (oObjSidurimOvdimUpd.SHAT_HATCHALA < dShatHatchalaLetashlum && oObjSidurimOvdimUpd.SHAT_GMAR > dShatGmarLetashlum) )
                    {
                        dShatHatchalaLetashlumToUpd = dShatHatchalaLetashlum;
                        dShatGmarLetashlumToUpd = dShatGmarLetashlum;
                    }
                    //ד
                    if (oObjSidurimOvdimUpd.SHAT_HATCHALA < dShatHatchalaLetashlum && oObjSidurimOvdimUpd.SHAT_GMAR <= dShatGmarLetashlum)
                    {
                        dShatHatchalaLetashlumToUpd = dShatHatchalaLetashlum;
                        dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR;
                    }
                    //ה
                    if (oObjSidurimOvdimUpd.SHAT_HATCHALA >= dShatHatchalaLetashlum && oObjSidurimOvdimUpd.SHAT_GMAR > dShatGmarLetashlum)
                    {
                        dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA;
                        dShatGmarLetashlumToUpd = dShatGmarLetashlum;
                    }
                    //ו
                    if (oObjSidurimOvdimUpd.SHAT_HATCHALA >= dShatHatchalaLetashlum && oObjSidurimOvdimUpd.SHAT_GMAR <= dShatGmarLetashlum)
                    {
                        dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA;
                        dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR;
                    }
                  
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /*************************************/
        private void SetHourToSidur19_2(ref clSidur oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, bool bIdkunRashShatHatchala, bool bIdkunRashShatGmar)
        {
            DateTime dShatHatchalaLetashlumToUpd; // = oObjSidurimOvdimUpd.SHAT_HATCHALA;
            DateTime dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR;

            //קביעת שעות לסידורים שזמן ההתחלה/גמר מותנה במאפיין אישי
            try
            {
                if (oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA != DateTime.MinValue)
                    dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA;
                else dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA;


                //GetSidurCurrentTime(iSidurIndex, ref oSidur, ref dSidurShatHatchala, ref dSidurShatGmar);
                //אם הסידור הוא מסוג "היעדרות" (סידור מיוחד עם מאפיין 53 בטבלת סידורים מיוחדים) והערך במאפיין הוא 8 (היעדרות בתשלום)  או 9 (ע"ח שעות נוספות)                   
                SetSidurHeadrut(ref oSidur, ref dShatHatchalaLetashlumToUpd, ref dShatGmarLetashlumToUpd, ref oObjSidurimOvdimUpd);
                //4. סידור אינו מסומן במאפיין 78 (רק לסידורים מיוחדים):
                //5. סידור מסומן במאפיין 78
                SetSidurKizuz(ref oSidur, ref dShatHatchalaLetashlumToUpd, ref dShatGmarLetashlumToUpd, ref oObjSidurimOvdimUpd);

                //     SetShaotLovedMishmeret2(ref oSidur, ref oObjSidurimOvdimUpd, ref dShatHatchalaLetashlumToUpd, ref dShatGmarLetashlumToUpd);

                if (!bIdkunRashShatHatchala && dShatHatchalaLetashlumToUpd != oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM)
                {
                    oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = dShatHatchalaLetashlumToUpd;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                    oSidur.dFullShatHatchalaLetashlum = oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM;
                    oSidur.sShatHatchalaLetashlum = oSidur.dFullShatHatchalaLetashlum.ToString("HH:mm");
                    if (oSidur.dFullShatHatchalaLetashlum.Year < clGeneral.cYearNull)
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
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 19, oSidur.iMisparIshi, oSidur.dSidurDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "SetHourToSidur19: " + ex.Message, null);
                _bSuccsess = false;
            }
        }
      
        private void SetSidurHeadrut(ref clSidur oSidur,ref DateTime dShatHatchalaLetashlumToUpd, ref DateTime dShatGmarLetashlumToUpd,ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            DateTime dShatHatchalaLetashlum = new DateTime();
            DateTime dShatGmarLetashlum = new DateTime();
            bool bFromMeafyenHatchala, bFromMeafyenGmar;
            try{
            if (oSidur.bSidurMyuhad)
            {
                //if ((oSidur.bHeadrutTypeKodExists) && (oSidur.sHeadrutTypeKod!=clGeneral.enMeafyenSidur53.enHeadrutWithPayment) &&
                //    (oSidur.sHeadrutTypeKod!=clGeneral.enMeafyenSidur53.enHolidayForHours))
                //{
                //    oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = null;
                //    oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = null;
                //}


                /*3. סידור מסוג העדרות (מקרה 2)
                אם הסידור הוא מסוג "היעדרות" (סידור מיוחד עם מאפיין 53 בטבלת סידורים מיוחדים) והערך במאפיין הוא 8 (היעדרות בתשלום)  או 9 (ע"ח שעות נוספות)
                שעת התחלה לתשלום = שעת התחלת מאפיין (המאפיין תלוי בסוג היום, ראה הסבר בעמודה שדות מעורבים)
                שעת גמר לתשלום = שעת גמר מאפיין (המאפיין תלוי בסוג היום, ראה הסבר בעמודה שדות מעורבים).
                (אין לעדכן שעת התחלה ושעת גמר)*/

                if ((oSidur.bHeadrutTypeKodExists) && ((oSidur.sHeadrutTypeKod == clGeneral.enMeafyenSidur53.enHeadrutWithPayment.GetHashCode().ToString()) ||
                    (oSidur.sHeadrutTypeKod == clGeneral.enMeafyenSidur53.enHolidayForHours.GetHashCode().ToString())))
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

        private void SetSidurKizuz(ref clSidur oSidur, ref DateTime dShatHatchalaLetashlumToUpd, ref DateTime dShatGmarLetashlumToUpd, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
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

        private void SetShatHatchalaGmarKizuz_2(ref clSidur oSidur,ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, DateTime dShatHatchalaLetashlum, DateTime dShatGmarLetashlum,ref DateTime dShatHatchalaLetashlumToUpd, ref DateTime dShatGmarLetashlumToUpd)
        {
            //5. סידור מסומן במאפיין 78
            //אם הסידור מסומן במאפיין 78 (קיזוז התחלה / גמר)
            //ואינו מסומן "לא לתשלום", ישנם כמה מקרים:
            bool flag=false;
           try{
               if ((oSidur.bKizuzAlPiHatchalaGmarExists) && (oObjSidurimOvdimUpd.LO_LETASHLUM == 0 || (oObjSidurimOvdimUpd.LO_LETASHLUM == 1 && oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM == 1)))
            {
                
                //1 .אם אין סימון "קוד חריגה" (אין = null או 0) ואין סימון "מחוץ למיכסה" (אין = null או 0), שלושה מקרים:                                                                               
                if (oObjSidurimOvdimUpd.CHARIGA == 0 )
                {
                    SetShaotHatchalaGmar_2(ref oSidur, dShatHatchalaLetashlum, dShatGmarLetashlum, ref oObjSidurimOvdimUpd,ref dShatHatchalaLetashlumToUpd,ref dShatGmarLetashlumToUpd);
                    SetShaotLovedMishmeret2(ref oSidur, ref oObjSidurimOvdimUpd, ref dShatHatchalaLetashlumToUpd, ref dShatGmarLetashlumToUpd, ref flag);

                }
                else
                {
                    ////5.2. אם יש סימון "מחוץ למיכסה" ולא קבענו את הסידור "לא לתשלום":
                    ////שעת התחלה לתשלום = שעת תחילת הסידור 
                    ////שעת גמר לתשלום = שעת גמר הסידור
                    //if ((oObjSidurimOvdimUpd.OUT_MICHSA != 0) && (oObjSidurimOvdimUpd.LO_LETASHLUM == 0 || (oObjSidurimOvdimUpd.LO_LETASHLUM == 1 && oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM == 1)))
                    //{
                    //    dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA;
                    //    dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR;
                    // }
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
                                if (oObjSidurimOvdimUpd.SHAT_GMAR > dShatGmarLetashlum)
                                {
                                    //if (dShatGmarLetashlum > oParam.dSidurLimitShatGmar)
                                    //{
                                    //   dShatGmarLetashlumToUpd = oParam.dSidurLimitShatGmar;
                                    //}
                                    //else
                                    //{
                                    dShatGmarLetashlumToUpd = dShatGmarLetashlum;
                                  
                                    //}
                                }
                                else
                                {
                                    dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR;
                                }

                           }
                            //ב. אם מסומן קוד חריגה "2"  (חריגה משעת גמר) אזי שעת גמר לתשלום = שעת גמר הסידור.
                            //אם שעת התחלה של הסידור קטנה משעת מאפיין התחלה המותרת (המאפיין תלוי בסוג היום, ראה עמודה  שדות מעורבים) : שעת התחלה לתשלום = השעה המוגדרת במאפיין. 
                            if (oObjSidurimOvdimUpd.CHARIGA == 2)
                            {
                                dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR;
                                if (oSidur.dFullShatHatchala < dShatHatchalaLetashlum)
                                {
                                    dShatHatchalaLetashlumToUpd = dShatHatchalaLetashlum;
                                }
                                else
                                {
                                    dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA;
                                }
                            }
                            //ג. אם מסומן קוד חריגה "3"  (חריגה משעת התחלה וגמר) אזי :
                            //שעת התחלה לתשלום = שעת תחילת הסידור.
                            //שעת גמר לתשלום = שעת גמר הסידור
                            if (oObjSidurimOvdimUpd.CHARIGA == 3)
                            {
                                dShatHatchalaLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_HATCHALA;
                                dShatGmarLetashlumToUpd = oObjSidurimOvdimUpd.SHAT_GMAR;
                             }
                        //}
                        //else
                        //{ SetShaotHatchalaGmar(ref oSidur, dShatHatchalaLetashlum, dShatGmarLetashlum, ref oObjSidurimOvdimUpd,ref dShatHatchalaLetashlumToUpd,ref dShatGmarLetashlumToUpd); }
                    }
                }
                
            }
           }
           catch (Exception ex)
           {
               throw ex;
           }
        }

        private void SetShaotLovedMishmeret2(ref clSidur oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd,  ref DateTime dShatHatchalaLetashlumToUpd, ref DateTime dShatGmarLetashlumToUpd,ref bool bflag)
        {
            DateTime shaa24, shaa23,shaa;
            bflag = false;
          try {
                 shaa = DateTime.Parse( oObjSidurimOvdimUpd.SHAT_GMAR.ToShortDateString() + " 18:00:00");
                if (!oMeafyeneyOved.Meafyen42Exists && oMeafyeneyOved.Meafyen23Exists && oMeafyeneyOved.Meafyen24Exists)
                    if (oSidur.bKizuzAlPiHatchalaGmarExists)
                        if ((oObjSidurimOvdimUpd.SHAT_HATCHALA.Hour >= 11 && oObjSidurimOvdimUpd.SHAT_HATCHALA.Hour <= 17 && oObjSidurimOvdimUpd.SHAT_GMAR > shaa)
                             && (oSidur.sShabaton != "1" && iSugYom >= clGeneral.enSugYom.Chol.GetHashCode() && iSugYom < clGeneral.enSugYom.Shishi.GetHashCode())) //|| ((iSugYom == clGeneral.enSugYom.Shishi.GetHashCode()  && oObjSidurimOvdimUpd.SHAT_GMAR > shaa.AddHours(-5)))))
                            {
                                shaa23 = DateTime.Parse(oObjSidurimOvdimUpd.SHAT_HATCHALA.ToShortDateString() + " " + oMeafyeneyOved.sMeafyen23.Substring(0, 2) + ":" + oMeafyeneyOved.sMeafyen23.Substring(2, 2));
                                shaa24 = DateTime.Parse(oObjSidurimOvdimUpd.SHAT_GMAR.ToShortDateString()  + " " + oMeafyeneyOved.sMeafyen24.Substring(0, 2) + ":" + oMeafyeneyOved.sMeafyen24.Substring(2, 2));
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
       
        private void SetShaotHatchalaGmar_2(ref clSidur oSidur, DateTime dShatHatchalaLetashlum, DateTime dShatGmarLetashlum, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd,ref DateTime dShatHatchalaLetashlumToUpd, ref DateTime dShatGmarLetashlumToUpd)
        {
            //א. אם שעת הגמר של הסידור לא גדולה משעת מאפיין ההתחלה המותרת (המאפיין תלוי בסוג היום, ראה עמודה  שדות מעורבים): יש לסמן את הסידור "לא לתשלום"
            //ב. אם שעת ההתחלה של הסידור לא קטנה משעת מאפיין הגמר המותרת (המאפיין תלוי בסוג היום, ראה עמודה  שדות מעורבים) - יש לסמן את הסידור "לא לתשלום                            
           try{
            if ((oObjSidurimOvdimUpd.SHAT_GMAR != DateTime.MinValue && (oObjSidurimOvdimUpd.SHAT_GMAR <= dShatHatchalaLetashlum)) || (oObjSidurimOvdimUpd.SHAT_HATCHALA >= dShatGmarLetashlum))
            {
                //oObjSidurimOvdimUpd.LO_LETASHLUM = 1;
                //oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM = 10;
                if (dShatHatchalaLetashlum.Year > clGeneral.cYearNull)
                {
                    dShatHatchalaLetashlumToUpd = dShatHatchalaLetashlum;
                }
                if (dShatGmarLetashlum.Year > clGeneral.cYearNull)
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
                    dShatHatchalaLetashlumToUpd= oObjSidurimOvdimUpd.SHAT_HATCHALA;
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
                    dShatGmarLetashlumToUpd= oObjSidurimOvdimUpd.SHAT_GMAR;
                }
            }
            }
           catch (Exception ex)
           {
               throw ex;
           }     
        }

        private void ChishuvShatHatchala30(ref clSidur oSidur, int iIndexSidur, ref  bool bUsedMazanTichnun, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            int i = 0, indexPeilutMechona = -1, iIndexPeilutMashmautit=-1;
            clPeilut oPeilut, oFirstPeilutMashmautit;
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
                        oPeilut = (clPeilut)oSidur.htPeilut[i];
                        oMakatType = oPeilut.lMakatNesia;
                        if (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "701" || oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "712" || oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "711")
                            indexPeilutMechona = i;
                        i++;
                    }
                    if (indexPeilutMechona == 0)
                        bPeilutHachanatMechona = true;
                    else if (indexPeilutMechona > 0)
                    {
                        oPeilut = (clPeilut)oSidur.htPeilut[0];
                        if (oPeilut.IsMustBusNumber(oParam.iVisutMustRechevWC) && !isPeilutMashmautit(oPeilut))
                        {
                            oPeilut = (clPeilut)oSidur.htPeilut[indexPeilutMechona];
                            otoNum = oPeilut.lOtoNo;
                            i = indexPeilutMechona - 1;
                            while (i >= 0)
                            {
                                oPeilut = (clPeilut)oSidur.htPeilut[i];
                                if (oPeilut.IsMustBusNumber(oParam.iVisutMustRechevWC) && oPeilut.lOtoNo != otoNum)
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
                            oPeilut = (clPeilut)oSidur.htPeilut[i];
                            if (isPeilutMashmautit(oPeilut)) //oPeilut.iMakatType == clKavim.enMakatType.mVisa.GetHashCode() || oNextPeilut.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode() || oNextPeilut.iMakatType == clKavim.enMakatType.mNamak.GetHashCode() || (oNextPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && (oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != KdsLibrary.clGeneral.enElementHachanatMechona.Element701.GetHashCode().ToString() && oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != KdsLibrary.clGeneral.enElementHachanatMechona.Element712.GetHashCode().ToString() && oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != KdsLibrary.clGeneral.enElementHachanatMechona.Element711.GetHashCode().ToString()) && (oNextPeilut.iElementLeShatGmar > 0 || oNextPeilut.iElementLeShatGmar == -1 || oNextPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "700")))
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
                                    oPeilut = (clPeilut)oSidur.htPeilut[i];
                                    if (isElemntLoMashmauti(oPeilut) || oPeilut.iMakatType == clKavim.enMakatType.mEmpty.GetHashCode())
                                        dShatHatchala = dShatHatchala.AddMinutes(-(GetMeshechPeilutHachnatMechona(iIndexSidur, oPeilut, oSidur, ref bUsedMazanTichnun, ref bUsedMazanTichnunInSidur)));
                                    if (bUsedMazanTichnunInSidur)
                                        bUsedMazanTichnun = true;     
                                }
                            }
                        }
                        else
                        {
                            oPeilut = (clPeilut)oSidur.htPeilut[0];
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

        private void UpdateShatHatchala(ref clSidur oSidur, int iSidurIndex, DateTime dShatHatchalaNew, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            OBJ_PEILUT_OVDIM oObjPeilutUpd;
            clPeilut oPeilut;
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
                        oPeilut = (clPeilut)oSidur.htPeilut[j];
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
        private bool isPeilutMashmautit(clPeilut oPeilut)
        {
            if (oPeilut.iMakatType == clKavim.enMakatType.mVisa.GetHashCode() || oPeilut.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode() || oPeilut.iMakatType == clKavim.enMakatType.mNamak.GetHashCode() ||
                (oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != KdsLibrary.clGeneral.enElementHachanatMechona.Element701.GetHashCode().ToString() && oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != KdsLibrary.clGeneral.enElementHachanatMechona.Element712.GetHashCode().ToString() && oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != KdsLibrary.clGeneral.enElementHachanatMechona.Element711.GetHashCode().ToString())
                                && (oPeilut.iElementLeShatGmar > 0 || oPeilut.iElementLeShatGmar == -1)))
                return true;
            else return false;          
        }
        private bool isElemntLoMashmauti(clPeilut oPeilut)
        {
            if (oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode() && oPeilut.iElementLeShatGmar == 0 &&
                oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "700")
                return true;
            else return false;
        }
        public void GetOvedShatHatchalaGmar(DateTime dSidurShatGmar, clMeafyenyOved oMeafyeneyOved, ref clSidur oSidur,
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
                        if ((oSidur.sSidurDay == clGeneral.enDay.Shabat.GetHashCode().ToString()) || (oSidur.sShabaton == "1"))
                        {
                            if (oMeafyeneyOved.Meafyen7Exists)
                            {
                                dShatHatchalaLetashlum = oMeafyeneyOved.ConvertMefyenShaotValid(dShatHatchalaSidur, oMeafyeneyOved.sMeafyen7);
                                bFromMeafyenHatchala = true;
                            }
                            else
                            {//אם אין מאפיין, נקבע את שעת הסידור כברירת מחדל
                                dShatHatchalaLetashlum = oSidur.dFullShatHatchala;
                            }
                            if (oMeafyeneyOved.Meafyen8Exists)
                            {
                                dShatGmarLetashlum = oMeafyeneyOved.ConvertMefyenShaotValid(dShatHatchalaSidur, oMeafyeneyOved.sMeafyen8);
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
                            if ((oSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()))
                            {
                                if (oMeafyeneyOved.Meafyen5Exists)
                                {
                                    dShatHatchalaLetashlum = oMeafyeneyOved.ConvertMefyenShaotValid(dShatHatchalaSidur, oMeafyeneyOved.sMeafyen5);
                                    bFromMeafyenHatchala = true;
                                }
                                else
                                {//אם אין מאפיין, נקבע את שעת הסידור כברירת מחדל
                                    dShatHatchalaLetashlum = oSidur.dFullShatHatchala;
                                }
                                if (oMeafyeneyOved.Meafyen6Exists)
                                {
                                    dShatGmarLetashlum = oMeafyeneyOved.ConvertMefyenShaotValid(dSidurShatGmar, oMeafyeneyOved.sMeafyen6);
                                    bFromMeafyenGmar = true;
                                }
                                else
                                {//אם אין מאפיין, נקבע את שעת הסידור כברירת מחדל
                                    dShatGmarLetashlum = dSidurShatGmar;
                                }
                            }
                            else
                            {   //יום חול
                                if (oMeafyeneyOved.Meafyen3Exists)
                                {
                                    dShatHatchalaLetashlum = oMeafyeneyOved.ConvertMefyenShaotValid(dShatHatchalaSidur, oMeafyeneyOved.sMeafyen3);
                                    bFromMeafyenHatchala = true;
                                }
                                else
                                {//אם אין מאפיין, נקבע את שעת הסידור כברירת מחדל
                                    dShatHatchalaLetashlum = oSidur.dFullShatHatchala;
                                }

                                if (oMeafyeneyOved.Meafyen4Exists)
                                {
                                    dShatGmarLetashlum = oMeafyeneyOved.ConvertMefyenShaotValid(dShatHatchalaSidur, oMeafyeneyOved.sMeafyen4);
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

        private void GetMeafyeneyMafilim(clSidur oSidur, DateTime dShatHatchalaSidur, DateTime dSidurShatGmar, out bool bFromMeafyenHatchala, out bool bFromMeafyenGmar, ref DateTime dShatHatchalaLetashlum, ref DateTime dShatGmarLetashlum)
        {
            clSidur firstTafkidSidur = htEmployeeDetails.Values.Cast<clSidur>().ToList().FirstOrDefault(sidur => sidur.iMisparSidur == 99001);
            clSidur lastTafkidSidur = htEmployeeDetails.Values.Cast<clSidur>().ToList().LastOrDefault(sidur => sidur.iMisparSidur == 99001);
            
            bFromMeafyenHatchala = false;
            bFromMeafyenGmar = false;

            if (firstTafkidSidur != null && lastTafkidSidur != null)
            {
                iSugYom = clGeneral.GetSugYom(_iMisparIshi, _dCardDate, _dtYamimMeyuchadim, _oOvedYomAvodaDetails.iKodSectorIsuk, _dtSugeyYamimMeyuchadim, _oMeafyeneyOved.iMeafyen56);
            

                if ((iSugYom >= clGeneral.enSugYom.Chol.GetHashCode() && iSugYom < clGeneral.enSugYom.Shabat.GetHashCode()) &&
                    iSugYom != clGeneral.enSugYom.Shishi.GetHashCode())   
                {
                    int iSugMishmeret = clDefinitions.GetSugMishmeret(_iMisparIshi, _dCardDate, _iSugYom, firstTafkidSidur.dFullShatHatchala, lastTafkidSidur.dFullShatGmar, _oParameters);

                    switch (iSugMishmeret)
                    {
                        case 1:
                                if (_oMeafyeneyOved.Meafyen3Exists)
                                {
                                    dShatHatchalaLetashlum = oMeafyeneyOved.ConvertMefyenShaotValid(_dCardDate, oMeafyeneyOved.sMeafyen3);
                                    bFromMeafyenHatchala = true;
                                }
                                if (_oMeafyeneyOved.Meafyen4Exists)
                                {
                                    dShatGmarLetashlum = oMeafyeneyOved.ConvertMefyenShaotValid(_dCardDate, oMeafyeneyOved.sMeafyen4);
                                    bFromMeafyenGmar = true;
                                }
                              
                            break;

                        case 2:// צהריים
                            {
                                if (_oMeafyeneyOved.Meafyen23Exists)
                                {
                                    dShatHatchalaLetashlum = oMeafyeneyOved.ConvertMefyenShaotValid(dShatHatchalaSidur, oMeafyeneyOved.sMeafyen23);
                                    bFromMeafyenHatchala = true;
                                }
                                if (_oMeafyeneyOved.Meafyen24Exists)
                                {
                                    dShatGmarLetashlum = oMeafyeneyOved.ConvertMefyenShaotValid(dSidurShatGmar, oMeafyeneyOved.sMeafyen24);
                                    bFromMeafyenGmar = true;
                                }
                                break;
                            }


                        case 3:// לילה
                            {
                                if (_oMeafyeneyOved.Meafyen25Exists)
                                {
                                    dShatHatchalaLetashlum = oMeafyeneyOved.ConvertMefyenShaotValid(dShatHatchalaSidur, oMeafyeneyOved.sMeafyen25);
                                    bFromMeafyenHatchala = true;
                                }
                                if (_oMeafyeneyOved.Meafyen26Exists)
                                {
                                    dShatGmarLetashlum = oMeafyeneyOved.ConvertMefyenShaotValid(dSidurShatGmar, oMeafyeneyOved.sMeafyen26);
                                    bFromMeafyenGmar = true;
                                }
                                break;
                            }

                    }

                }
                else
                {
                    if (iSugYom == clGeneral.enSugYom.Shishi.GetHashCode())// && iSugYom < clGeneral.enSugYom.Shabat.GetHashCode())
                    {
                        if (_oMeafyeneyOved.Meafyen5Exists)
                        {
                            dShatHatchalaLetashlum = oMeafyeneyOved.ConvertMefyenShaotValid(dShatHatchalaSidur, oMeafyeneyOved.sMeafyen5);
                            bFromMeafyenHatchala = true;
                        }
                        if (_oMeafyeneyOved.Meafyen6Exists)
                        {
                            dShatGmarLetashlum = oMeafyeneyOved.ConvertMefyenShaotValid(dSidurShatGmar, oMeafyeneyOved.sMeafyen6);
                            bFromMeafyenGmar = true;
                        }
                    }
                    else if ((iSugYom >= clGeneral.enSugYom.Shabat.GetHashCode() && iSugYom < clGeneral.enSugYom.Rishon.GetHashCode()) || oSidur.sShabaton == "1")
                    {
                        if (_oMeafyeneyOved.Meafyen7Exists)
                        {
                            dShatHatchalaLetashlum = oMeafyeneyOved.ConvertMefyenShaotValid(dShatHatchalaSidur, oMeafyeneyOved.sMeafyen7);
                            bFromMeafyenHatchala = true;
                        }
                        if (_oMeafyeneyOved.Meafyen8Exists)
                        {
                            dShatGmarLetashlum = oMeafyeneyOved.ConvertMefyenShaotValid(dSidurShatGmar, oMeafyeneyOved.sMeafyen8);
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

            //public DateTime ShatGmarOld
            //{
            //    get
            //    {
            //        return this._dShatGmarOld;
            //    }
            //    set
            //    {
            //        this._dShatGmarOld = value;
            //    }
            //}

        //    public DateTime ShatGmarNew
        //    {
        //        get
        //        {
        //            return this._dShatGmarNew;
        //        }
        //        set
        //        {
        //            this._dShatGmarNew = value;
        //        }
        //    }


      }

        //public bool IsExecuteInputData
        //{
        //    set
        //    {
        //        _IsExecuteInputData = value;
        //    }
        //    get
        //    {
        //        return _IsExecuteInputData;
        //    }
        //}

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
    }
}

