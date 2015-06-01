using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Diagnostics;
using KDSCommon.Interfaces.DAL;
using System.Configuration;
using DalOraInfra.DAL;
using KDSCommon.Enums;
using Microsoft.Practices.Unity;
using KDSCommon.Interfaces.Logs;
namespace KdsLibrary.KDSLogic.DAL
{
    public class KavimDAL : IKavimDAL
    {
        public const string cGetKavDetails = "kds_catalog_pack.GetKavDetails";
        public const string cGetRekaDetails = "kds_catalog_pack.GetRekaDetails";
        public const string cGetNamakDetails = "kds_catalog_pack.GetNamakDetails";
        public const string cGetRekaDetailsByXY = "kds_catalog_pack.GetRekaDetailsByXY";
        public const string cProGetDataByKodElement = "PKG_ELEMENTS.pro_get_data_by_kod_element";
        public const string cProGetAllElementsKod = "PKG_ELEMENTS.pro_get_all_elements_kod";
        public const string cProGetElementDetails = "PKG_ELEMENTS.pro_get_element_details";
        public const string cProGetVisutDetails = "PKG_ELEMENTS.pro_get_visut_details";
        public const string cGetKatalogKavim = "pkg_tnua.pro_get_kavim_details";
        public const string cProGetMasharBusLicenseNum = "pkg_tnua.pro_get_mashar_bus_license_num";
        public const string cProGetMasharData = "PKG_TNUA.pro_get_mashar_data";
        public const string cProGetBusesDetails = "PKG_TNUA.pro_get_buses_details";
        public const string cFnIsOtoNumberExists = "pkg_errors.fn_is_oto_number_exists";
        public const string cGetSidurDetails = "KDS_SIDUR_AVODA_PACK.GetSidurDetails";
        public const string cCheckHityatzvutNehag = "kds.KdsVerifyDriverCheckIn";
        public const string cProGetMakatimLeTkinut = "PKG_BATCH.pro_Get_Makatim_LeTkinut";
        private IUnityContainer _container;

        public KavimDAL(IUnityContainer container)
        {
            _container = container;
        }
        public DataTable GetKatalogKavim(int iMisparIshi, DateTime dFromDate, DateTime dToDate)
        {
            clTxDal _Dal = new clTxDal();
            DataTable dt = new DataTable();
            //הפרוצדורה מחזירה את כל נתוני הפעילויות מהתנועה, למספר אישי ולטווח תאריכים נתון
            try
            {
                _Dal.TxBegin();
                _Dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                _Dal.AddParameter("p_date_from", ParameterType.ntOracleDate, dFromDate, ParameterDir.pdInput, 100);
                _Dal.AddParameter("p_date_to", ParameterType.ntOracleDate, dToDate, ParameterDir.pdInput, 100);
                _Dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                _Dal.ExecuteSP(cGetKatalogKavim, ref dt);
                _Dal.TxCommit();
                return dt;
            }
            catch (Exception ex)
            {

                _container.Resolve<ILogger>().LogMessage("GetKatalogKavim: " + ex.Message, EventLogEntryType.Error, enEventId.ProblemOfAccessToTnua.GetHashCode());

                _Dal.TxRollBack();
                throw ex;
            }
        }


        public DataSet GetKavimDetailsFromTnuaDS(long lMakatNesia, DateTime dDate, out int iResult, int? reciveVisut)
        {
            clTnua oTnua = new clTnua((string)ConfigurationSettings.AppSettings["KDS_TNPR_CONNECTION"]);
            // clDal oDal = new clDal();
            DataSet ds = new DataSet();

            try
            {//: מביא נתונים לקווי שירות               
                oTnua.AddParameter("p_date", ParameterType.ntOracleDate, DateTime.Parse(dDate.ToShortDateString()), ParameterDir.pdInput);
                oTnua.AddParameter("p_makat8", ParameterType.ntOracleInteger, lMakatNesia, ParameterDir.pdInput);
                oTnua.AddParameter("rc", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
                oTnua.AddParameter("KavDetailsCrs", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oTnua.AddParameter("EntryCrs", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oTnua.AddParameter("VisutimCrs", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oTnua.AddParameter("p_KnisaVisut", ParameterType.ntOracleInteger, reciveVisut, ParameterDir.pdInput);
                oTnua.ExecuteSP(cGetKavDetails, ref ds);
                //(קוד החזרה : 0 – תקין, 1 – שגיאה)
                iResult = int.Parse(oTnua.GetValParam("rc").ToString());
                return ds;
            }
            catch (Exception ex)
            {
                _container.Resolve<ILogger>().LogMessage("GetKavimDetailsFromTnuaDS: " + ex.Message, EventLogEntryType.Error, enEventId.ProblemOfAccessToTnua.GetHashCode());
                throw ex;
            }
        }

        public DataTable GetKavimDetailsFromTnuaDT(long lMakatNesia, DateTime dDate, out int iResult)
        {
            clTnua oTnua = new clTnua((string)ConfigurationSettings.AppSettings["KDS_TNPR_CONNECTION"]);
            DataTable dt = new DataTable();

            try
            {//: מביא נתונים לקווי שירות               
                oTnua.AddParameter("p_date", ParameterType.ntOracleDate, DateTime.Parse(dDate.ToShortDateString()), ParameterDir.pdInput);
                oTnua.AddParameter("p_makat_nesia", ParameterType.ntOracleInteger, lMakatNesia, ParameterDir.pdInput);
                oTnua.AddParameter("p_rc", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
                oTnua.AddParameter("p_Cur1", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oTnua.AddParameter("p_Cur2", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oTnua.AddParameter("VisutimCrs", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oTnua.ExecuteSP(cGetKavDetails, ref dt);
                //(קוד החזרה : 0 – תקין, 1 – שגיאה)
                if (dt.Rows.Count == 0)
                {
                    iResult = 1;
                }
                else
                {
                    iResult = int.Parse(oTnua.GetValParam("p_rc").ToString());
                }
                return dt;
            }
            catch (Exception ex)
            {
                _container.Resolve<ILogger>().LogMessage("GetKavimDetailsFromTnuaDT: " + ex.Message, EventLogEntryType.Error, enEventId.ProblemOfAccessToTnua.GetHashCode());

                throw ex;
            }
        }

        public DataTable GetMakatKavShirut(long lMakatNesia, DateTime dDate)
        {
            clTnua oTnua = new clTnua((string)ConfigurationSettings.AppSettings["KDS_TNPR_CONNECTION"]);
            DataTable dt = new DataTable();
            //bool bValid = false;

            try
            {   //מחזיר אם מקט של נסיעת שירות תקינה- 0 תקין           
                oTnua.AddParameter("p_date", ParameterType.ntOracleDate, DateTime.Parse(dDate.ToShortDateString()), ParameterDir.pdInput);
                oTnua.AddParameter("p_makat_nesia", ParameterType.ntOracleInteger, lMakatNesia, ParameterDir.pdInput);
                oTnua.AddParameter("p_rc", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
                oTnua.AddParameter("p_Cur1", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oTnua.AddParameter("p_Cur2", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oTnua.AddParameter("VisutimCrs", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oTnua.ExecuteSP(cGetKavDetails, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                _container.Resolve<ILogger>().LogMessage("GetMakatKavShirut: " + ex.Message, EventLogEntryType.Error, enEventId.ProblemOfAccessToTnua.GetHashCode());

                throw ex;
            }
        }

        public DataTable GetMakatKavNamak(long lMakatNesia, DateTime dDate)
        {
            //מחזיר אם מקט של נסיעת נמ"ק תקינה- 0 תקין
            clTnua oTnua = new clTnua((string)ConfigurationSettings.AppSettings["KDS_TNPR_CONNECTION"]);
            DataTable dt = new DataTable();
            try
            {
                oTnua.AddParameter("p_date", ParameterType.ntOracleDate, DateTime.Parse(dDate.ToShortDateString()), ParameterDir.pdInput);
                oTnua.AddParameter("p_makat_nesia", ParameterType.ntOracleInteger, lMakatNesia, ParameterDir.pdInput);
                oTnua.AddParameter("p_rc", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
                oTnua.AddParameter("p_Cur1", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oTnua.ExecuteSP(cGetNamakDetails, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                _container.Resolve<ILogger>().LogMessage("GetMakatKavNamak: " + ex.Message, EventLogEntryType.Error, enEventId.ProblemOfAccessToTnua.GetHashCode());

                throw ex;
            }
        }

        public DataTable GetMakatKavReka(long lMakatNesia, DateTime dDate)
        {
            //מחזיר אם מקט של נסיעה ריקה תקינה- 0 תקין
            clTnua oTnua = new clTnua((string)ConfigurationSettings.AppSettings["KDS_TNPR_CONNECTION"]);
            DataTable dt = new DataTable();
            try
            {
                oTnua.AddParameter("p_date", ParameterType.ntOracleDate, DateTime.Parse(dDate.ToShortDateString()), ParameterDir.pdInput);
                oTnua.AddParameter("p_makat_nesia", ParameterType.ntOracleInteger, lMakatNesia, ParameterDir.pdInput);
                oTnua.AddParameter("p_rc", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
                oTnua.AddParameter("p_Cur1", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oTnua.ExecuteSP(cGetRekaDetails, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                _container.Resolve<ILogger>().LogMessage("GetMakatKavReka: " + ex.Message, EventLogEntryType.Error, enEventId.ProblemOfAccessToTnua.GetHashCode());

                throw ex;
            }
        }

        public DataTable GetMeafyeneyElementByKod(long lMakatNesia, DateTime dDate)
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();
            long lKodElement;

            try
            {
                //Get peilut meafyeney Elementim
                lKodElement = long.Parse(lMakatNesia.ToString().PadRight(8, (char)48));
                lKodElement = (lKodElement % 10000000);
                lKodElement = (lKodElement / 100000);

                if (lKodElement == 0)
                {
                    oDal.AddParameter("p_kod_element", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }
                else
                {
                    oDal.AddParameter("p_kod_element", ParameterType.ntOracleInteger, lKodElement, ParameterDir.pdInput);
                }
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, DateTime.Parse(dDate.ToShortDateString()), ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(cProGetDataByKodElement, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //----------------------------------------------
        public DataTable GetElementDetails(long lNewMakat)
        {
            clDal _Dal = new clDal();
            DataTable dt = new DataTable();
            try
            {
                _Dal.AddParameter("p_kod_element", ParameterType.ntOracleLong, int.Parse(lNewMakat.ToString().Substring(1, 2)), ParameterDir.pdInput);
                _Dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                _Dal.ExecuteSP(cProGetElementDetails, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetVisutDetails(long lNewMakat)
        {
            clDal _Dal = new clDal();
            DataTable dt = new DataTable();
            try
            {
                _Dal.AddParameter("p_kod_visut", ParameterType.ntOracleLong, int.Parse(lNewMakat.ToString().Substring(3, 3)), ParameterDir.pdInput);
                _Dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                _Dal.ExecuteSP(cProGetVisutDetails, ref dt);
                return dt;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public DataTable GetMakatRekaDetailsFromTnua(long lMakatNesia, DateTime dDate, out int iResult)
        {
            clTnua oTnua = new clTnua((string)ConfigurationSettings.AppSettings["KDS_TNPR_CONNECTION"]);
            DataTable dt = new DataTable();

            try
            {//מביא נתונים למקטים מסוג קו ריק                
                oTnua.AddParameter("p_date", ParameterType.ntOracleDate, DateTime.Parse(dDate.ToShortDateString()), ParameterDir.pdInput, 100);
                oTnua.AddParameter("p_makat_nesia", ParameterType.ntOracleInteger, lMakatNesia, ParameterDir.pdInput);
                oTnua.AddParameter("p_rc", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
                oTnua.AddParameter("p_Cur1", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oTnua.ExecuteSP(cGetRekaDetails, ref dt);
                //(קוד החזרה : 0 – תקין, 1 – שגיאה)
                if (dt.Rows.Count == 0)
                {
                    iResult = 1;
                }
                else
                {
                    iResult = int.Parse(oTnua.GetValParam("p_rc").ToString());
                }
                return dt;
            }
            catch (Exception ex)
            {
                _container.Resolve<ILogger>().LogMessage("GetMakatRekaDetailsFromTnua: " + ex.Message, EventLogEntryType.Error, enEventId.ProblemOfAccessToTnua.GetHashCode());

                throw ex;
            }
        }


        public DataTable GetMakatNamakDetailsFromTnua(long lMakatNesia, DateTime dDate, out int iResult)
        {
            clTnua oTnua = new clTnua((string)ConfigurationSettings.AppSettings["KDS_TNPR_CONNECTION"]);
            DataTable dt = new DataTable();

            try
            {//מביא נתונים למקטים מסוג נמ"ק                
                oTnua.AddParameter("p_date", ParameterType.ntOracleDate, DateTime.Parse(dDate.ToShortDateString()), ParameterDir.pdInput);
                oTnua.AddParameter("p_makat_nesia", ParameterType.ntOracleInteger, lMakatNesia, ParameterDir.pdInput);
                oTnua.AddParameter("p_rc", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
                oTnua.AddParameter("p_Cur1", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oTnua.ExecuteSP(cGetNamakDetails, ref dt);
                //(קוד החזרה : 0 – תקין, 1 – שגיאה)]
                if (dt.Rows.Count == 0)
                {
                    iResult = 1;
                }
                else
                {
                    iResult = int.Parse(oTnua.GetValParam("p_rc").ToString());
                }
                return dt;
            }
            catch (Exception ex)
            {
                _container.Resolve<ILogger>().LogMessage("GetMakatNamakDetailsFromTnua: " + ex.Message, EventLogEntryType.Error, enEventId.ProblemOfAccessToTnua.GetHashCode());

                throw ex;
            }
        }

        public int IsBusNumberValid(long lOtoNo, DateTime dCardDate)
        {
            clDal oDal = new clDal();

            try
            {   //בודק מול מש"ר אם מספר רכב תקין:   
                //0-תקין 
                //1- שגיאה
                //2- לא אותר
                oDal.AddParameter("p_result", ParameterType.ntOracleInteger, null, ParameterDir.pdReturnValue);
                oDal.AddParameter("p_oto_no", ParameterType.ntOracleLong, lOtoNo, ParameterDir.pdInput);
                oDal.AddParameter("p_date", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
                oDal.ExecuteSP(cFnIsOtoNumberExists);
                var result = oDal.GetValParam("p_result").ToString();
                return int.Parse(result);

            }
            catch (Exception ex)
            {
                _container.Resolve<ILogger>().LogMessage("IsBusNumberValid: " + ex.Message, EventLogEntryType.Error, enEventId.ProblemOfAccessToTnua.GetHashCode());

                throw ex;
            }
        }

        public void GetBusLicenseNumber(long lOtoNo, ref long lLicenseNumber)
        {
            clDal oDal = new clDal();

            try
            {   //בדיקה אם קיים מספר רכב במש"ר ואם כן מחזיר מספר רישוי:                                 
                oDal.AddParameter("p_oto_no", ParameterType.ntOracleLong, lOtoNo, ParameterDir.pdInput);
                oDal.AddParameter("p_license_no", ParameterType.ntOracleLong, lLicenseNumber, ParameterDir.pdOutput, 200);

                oDal.ExecuteSP(cProGetMasharBusLicenseNum);

                if (string.IsNullOrEmpty(oDal.GetValParam("p_license_no")))
                {
                    lLicenseNumber = 0;
                }
                else
                {
                    lLicenseNumber = long.Parse(oDal.GetValParam("p_license_no").ToString());
                }
            }
            catch (Exception ex)
            {
                _container.Resolve<ILogger>().LogMessage("GetBusLicenseNumber: " + ex.Message, EventLogEntryType.Error, enEventId.ProblemOfAccessToTnua.GetHashCode());

                throw ex;
            }
        }

        public DataTable GetSidurDetailsFromTnua(int iMisparSidur, DateTime dDate, out int iResult)
        {
            clTnua oTnua = new clTnua((string)ConfigurationSettings.AppSettings["KDS_TNPR_CONNECTION"]);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            try
            {//: מביא נתונים לסידור               
                oTnua.AddParameter("p_date", ParameterType.ntOracleVarchar, dDate.ToShortDateString(), ParameterDir.pdInput, 100);
                oTnua.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger, iMisparSidur, ParameterDir.pdInput);
                oTnua.AddParameter("p_Cur1", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oTnua.AddParameter("p_Cur2", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oTnua.AddParameter("p_rc", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
                oTnua.AddParameter("p_KnisaVisut", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);

                oTnua.ExecuteSP(cGetSidurDetails, ref dt);
                //(קוד החזרה : 0 – תקין, 1 – שגיאה)
                if (dt.Rows.Count == 0)
                {
                    iResult = 1;
                }
                else
                {
                    iResult = int.Parse(oTnua.GetValParam("p_rc").ToString());
                }
                return dt;
            }
            catch (Exception ex)
            {
                _container.Resolve<ILogger>().LogMessage("GetSidurDetailsFromTnua: " + ex.Message, EventLogEntryType.Error, enEventId.ProblemOfAccessToTnua.GetHashCode());

                throw ex;
            }
        }
        public DataSet GetSidurAndPeiluyotFromTnua(int iMisparSidur, DateTime dDate, int? iKnisaVisut, out int iResult)
        {
            // int ?iKnisaVisut,    
            DataSet ds = new DataSet();
            clTxDal oTxDal = new clTxDal((string)ConfigurationSettings.AppSettings["KDS_TNPR_CONNECTION"]);

            try
            {//: מביא נתונים לסידור      
                oTxDal.TxBegin();
                oTxDal.AddParameter("p_date", ParameterType.ntOracleVarchar, dDate.ToShortDateString(), ParameterDir.pdInput, 100);
                oTxDal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger, iMisparSidur, ParameterDir.pdInput);
                oTxDal.AddParameter("p_Cur1", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oTxDal.AddParameter("p_Cur2", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oTxDal.AddParameter("p_rc", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
                oTxDal.AddParameter("p_KnisaVisut", ParameterType.ntOracleInteger, iKnisaVisut, ParameterDir.pdInput);

                oTxDal.ExecuteSP(cGetSidurDetails, ref ds);
                //(קוד החזרה : 0 – תקין, 1 – שגיאה)
                if (ds.Tables[0].Rows.Count == 0)
                {
                    iResult = 1;
                }
                else
                {
                    iResult = int.Parse(oTxDal.GetValParam("p_rc").ToString());
                }
                oTxDal.TxCommit();
                return ds;
            }
            catch (Exception ex)
            {
                _container.Resolve<ILogger>().LogMessage("GetSidurAndPeiluyotFromTnua: " + ex.Message, EventLogEntryType.Error, enEventId.ProblemOfAccessToTnua.GetHashCode());

                oTxDal.TxRollBack();
                throw ex;
            }
        }

        public DataTable GetMasharData(string sCarsNumbers)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {   //מביא את נתוני מש"ר: 
                oDal.AddParameter("p_cars_number", ParameterType.ntOracleVarchar, sCarsNumbers, ParameterDir.pdInput, 500);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(cProGetMasharData, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                _container.Resolve<ILogger>().LogMessage("GetMasharData: " + ex.Message, EventLogEntryType.Error, enEventId.ProblemOfAccessToTnua.GetHashCode());

                throw ex;
            }
        }



        public DataTable GetRekaDetailsByXY(DateTime dDate, long lMokedStart, long lMokedEnd, out int iResult)
        {
            clTnua _Tnua = new clTnua((string)ConfigurationSettings.AppSettings["KDS_TNPR_CONNECTION"]);
            DataTable dt = new DataTable();
            //הפרוצדורה מחזירה את כל נתוני הפעילויות מהתנועה, למספר אישי ולטווח תאריכים נתון
            try
            {
                _Tnua.AddParameter("p_date", ParameterType.ntOracleDate, dDate, ParameterDir.pdInput);
                _Tnua.AddParameter("p_mokedTchila", ParameterType.ntOracleLong, lMokedStart, ParameterDir.pdInput, 100);
                _Tnua.AddParameter("p_mokedsiyum", ParameterType.ntOracleLong, lMokedEnd, ParameterDir.pdInput, 100);
                _Tnua.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                _Tnua.AddParameter("p_rc", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);
                _Tnua.ExecuteSP(cGetRekaDetailsByXY, ref dt);

                //(קוד החזרה : 0 – תקין, 1 – שגיאה)
                iResult = int.Parse(_Tnua.GetValParam("p_rc").ToString());
                return dt;
            }
            catch (Exception ex)
            {
                _container.Resolve<ILogger>().LogMessage("GetRekaDetailsByXY: " + ex.Message, EventLogEntryType.Error, enEventId.ProblemOfAccessToTnua.GetHashCode());

                throw ex;
            }
        }

        public int CheckHityazvutNehag(int? iNekudatTziyunTnua, int iRadyus, int iSnif, int? iMikumShaon, int? iMisparSiduriShaon)
        {
            clTnua oTnua = new clTnua((string)ConfigurationSettings.AppSettings["KDS_TNPR_CONNECTION"]);

            try
            {
                //מחזיר קוד התייצבות נהג
                //0	פטור מהחתמה
                //1	החתמה תקינה
                //2	החתמה לא תקינה 
                //3	חסרה החתמה 
                //4	מיקום שעון לא קיים בטבלה 
                //9	תקלה  

                oTnua.AddParameter("p_Kod_hityazvut", ParameterType.ntOracleInteger, null, ParameterDir.pdReturnValue);
                oTnua.AddParameter("p_radius", ParameterType.ntOracleInteger, iRadyus, ParameterDir.pdInput);
                oTnua.AddParameter("p_branch_id", ParameterType.ntOracleInteger, iSnif, ParameterDir.pdInput);

                if (iMikumShaon.HasValue)
                {
                    oTnua.AddParameter("p_clock_location", ParameterType.ntOracleInteger, iMikumShaon, ParameterDir.pdInput);
                }
                else
                {
                    oTnua.AddParameter("p_clock_location", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }

                if (iMisparSiduriShaon.HasValue)
                {
                    oTnua.AddParameter("p_clock_number", ParameterType.ntOracleInteger, iMisparSiduriShaon, ParameterDir.pdInput);
                }
                else
                {
                    oTnua.AddParameter("p_clock_number", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }

                if (iNekudatTziyunTnua.HasValue)
                {
                    oTnua.AddParameter("p_xy", ParameterType.ntOracleInteger, iNekudatTziyunTnua, ParameterDir.pdInput);
                }
                else
                {
                    oTnua.AddParameter("p_xy", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }

                oTnua.ExecuteSP(cCheckHityatzvutNehag);

                return int.Parse(oTnua.GetValParam("p_Kod_hityazvut").ToString());
            }
            catch (Exception ex)
            {
                _container.Resolve<ILogger>().LogMessage("CheckHityazvutNehag: " + ex.Message, EventLogEntryType.Error, enEventId.ProblemOfAccessToTnua.GetHashCode());

                throw ex;
            }
        }

        public DataTable GetMakatimLeTkinut(DateTime dTaarich)
        {
            DataTable dt = new DataTable();
            clDal dal = new clDal();
            try
            {
                dal.AddParameter("p_date", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
                dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(cProGetMakatimLeTkinut, ref dt);
            }
            catch (Exception ex)
            {
                //dt = null;

                throw (ex);
            }
            return dt;
        }
        public DataTable GetBusesDetailsLeOvedForMonth(DateTime dTarMe, DateTime dTarAd, int iMispar_ishi)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {   //מביא את נתוני מש"ר: 
                oDal.AddParameter("p_tar_me", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput);
                oDal.AddParameter("p_tar_ad", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput);
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMispar_ishi, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(cProGetBusesDetails, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                _container.Resolve<ILogger>().LogMessage("GetBusesDetailsLeOvedForMonth: " + ex.Message, EventLogEntryType.Error, enEventId.ProblemOfAccessToTnua.GetHashCode());

                throw ex;
            }
        }

    }
}
