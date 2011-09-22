//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Linq;
//using System.Text;
//using KdsLibrary.DAL;
//using System.Data;
//using KdsLibrary;
//using KdsLibrary.BL;
//using KdsLibrary.UDT;
//using System.Collections;
//using KdsLibrary.Utils;

//namespace KdsBatch
//{
//    public class clCardErrors
//    {
//        private DataTable dtDetails;
//        private DataTable dtLookUp;       
//        private DataTable dtMatzavOved;       
//        private DataTable dtSugSidur; //מכיל מאפייני סוגי סידורים 
//        //private Hashtable htEmployeeDetails;        
//        private OrderedDictionary htEmployeeDetails;
//        private DataRow drNew;       
//        private int iLastMisaprSidur;
//        private clParameters oParam;
//        private clOvedYomAvoda oOvedYomAvodaDetails;
//        private clMeafyenyOved oMeafyeneyOved;
//        //private COLL_MEAFYENEY_OVED oCollMeafyneyOved; 

        

//        private enum enErrors
//        {
//            errHrStatusNotValid = 1,
//            errSidurNotExists = 9,
//            errSidurNamlakWithoutNesiaCard = 13,
//            errSidurHourStartEndNotValid = 14,//לבדוק שינויים            
//            errHourMissing = 15,
//            errSidurimHoursNotValid = 16,
//            errPizulHafsakaValueNotValid = 20,
//            errPizulValueNotValid = 22, 
//            errShabatPizulValueNotValid = 23,
//            errPitzulSidurInShabat = 24,
//            errPitzulMuchadValueNotValid = 25,
//            errLoZakaiLeNesiot  = 26,
//            errSimunNesiaNotValid = 27,
//            errLinaValueNotValid = 30,
//            errLoZakaiLLina = 31,
//            errCharigaValueNotValid = 32,
//            errCharigaZmanHachtamatShaonNotValid = 33,
//            errZakaiLeCharigaValueNotValid = 34,  
//            errSidurExistsInShlila =35,//לקחת את הערך לפי עץ ניהולי
//            errHalbashaNotvalid = 36, 
//            errHalbashaInSidurNotValid = 37, 
//            errHamaraNotValid = 39,
//            errOutMichsaInSidurHeadrutNotValid = 40,
//            errHamaratShabatNotValid = 42,
//            errHamaratShabatNotAllowed=43,
//            errZakaiLehamaratShabat = 44,//להוסיף לסידורים רגילים
//            errHashlamaForComputerWorkerAndAccident = 45,            
//            errShbatHashlamaNotValid = 47,
//            errHashlamaForSidurNotValid = 48,
//            errHahlamatHazmanaNotValid = 49,
//            errSidurNotAllowedInShabaton = 50,
//            errTeoodatNesiaNotInVisa =52,
//            errSidurEilatNotValid = 55,
//            errYomVisaNotValid = 56,
//            errSimunVisaNotValid = 57,
//            errSidurVisaNotValid = 58,
//            //errOtoNoNotValid = 67,
//            errOtoNoExists = 68,//להוסיף לסידורים רגילים
//            errOtoNoNotExists = 69,//לבדוק באיפיון מה הכוונה מספר פעילות המתחילה ב7-
//            errKodNesiaNotExists = 81,
//            errPeilutForSidurNonValid = 84,
//            errTimeForPrepareMechineNotValid = 86,
//            errNesiaTimeNotValid = 91,
//            errShatYetizaNotExist = 92,
//            errKmNotExists = 96,
//            //errKisuyTorNotValid = 117,
//            errDuplicateShatYetiza = 103,
//            errPeilutShatYetizaNotValid = 113,
//            errOutMichsaNotValid = 118,
//            errShatPeilutSmallerThanShatHatchalaSidur = 121,
//            errShatPeilutBiggerThanShatGmarSidur = 122,
//            errSidurNetzerNotValidForOved = 124,
//            errNesiaInSidurVisaNotAllowed = 125,
//            errAtLeastOnePeilutRequired = 127,
//            errElementTimeBiggerThanSidurTime = 129,
//            errOvdaInMachalaNotAllowed = 132,
//            errDriverLessonsNumberNotValid = 136,
//            errHashlamaNotValid = 137,
//            errMisparSiduriOtoInSidurEilatNotExists = 139,  
//            errMisparSiduriOtoNotInSidurEilat = 140,
//            errNotAllowedKodsForEggedTaavora = 141,
//            errTotalHashlamotBiggerThanAllow = 142,
//            errSidurTafkidWithOutApprove = 145,
//            errNotAllowedSidurForEggedTaavora = 148,
//            errNesiaMeshtanaNotDefine  = 150,
//            errSidurAvodaNotValidForMonth = 160,
//            errOvedNotAllowedToDoSidurNahagut = 161,
//            errCurrentPeilutInPrevPeilut = 162,
//            errHashlamaLeYomAvodaNotAllowed = 163,
//            errSidurSummerNotValidForOved = 164,
//            errAvodatNahagutNotValid = 165,
//            errHmtanaTimeNotValid = 166
//        }

//        private enum errNesiaMeshtana
//        {
//            errNesiaMeshtanaNotDefineForKnisa=1,
//            errNesiaMeshtanaNotDefineForYetiza=2,
//            errNesiaMeshtanaNotDefineForAll=3,
//            enDefineAll=0
//        }

        

//        public void InsertErrorsToTbShgiot(DataTable dtErrors, DateTime dCardDate)
//        {
//            //כתיבת שגיאות ל-TB_SHGIOT
//            clDal oDal = new clDal();
//            StringBuilder sbYeshut = new StringBuilder();
//            DataSet ds = new DataSet();
//            string[] ucols = new string[2];

//            try
//            {
//                oDal.ExecuteSQL(string.Concat("DELETE TB_SHGIOT T WHERE T.MISPAR_ISHI = ", dtErrors.Rows[0]["mispar_ishi"].ToString(), " and taarich = to_date('", DateTime.Parse(dtErrors.Rows[0]["Taarich"].ToString()).ToShortDateString(), "','dd/mm/yyyy')"));

//                oDal = new clDal();

//                //ds.Tables.Add(dtErrors);
                
//                //ucols[0] = "mispar_ishi";
//                //ucols[1] = "KOD_SHGIA";
//                //oDal.InsertXML(ds.GetXml(), "TB_SHGIOT", ucols);
//                int i = 0;
//                oDal.ArrayBindCount = dtErrors.Rows.Count;
//                int[] arrMisparIshi = new int[dtErrors.Rows.Count];
//                int[] arrKodShgia = new int[dtErrors.Rows.Count];
//                string[] arrYeshutId = new string[dtErrors.Rows.Count];
//                int[] arrMisparSidur = new int[dtErrors.Rows.Count];
//                DateTime[] arrTaarich = new DateTime[dtErrors.Rows.Count];
//                DateTime[] arrShatHatchala = new DateTime[dtErrors.Rows.Count];
//                DateTime[] arrShatYetzia = new DateTime[dtErrors.Rows.Count];
//                string[] arrHeara = new string[dtErrors.Rows.Count];

//                foreach (DataRow dr in dtErrors.Rows)
//                {
//                    arrMisparIshi[i] = int.Parse(dr["mispar_ishi"].ToString());
//                    arrKodShgia[i] = int.Parse(dr["check_num"].ToString());
//                    sbYeshut.Remove(0, sbYeshut.Length);
//                    sbYeshut.Append(string.IsNullOrEmpty(dr["Taarich"].ToString()) ? dCardDate.ToShortDateString() : DateTime.Parse(dr["Taarich"].ToString()).ToShortDateString());
//                    //sbYeshut.Append(DateTime.Parse(dr["Taarich"].ToString()).ToShortDateString());
//                    sbYeshut.Append(",");
//                    sbYeshut.Append(string.IsNullOrEmpty(dr["mispar_sidur"].ToString()) ? "" : string.Concat(dr["mispar_sidur"].ToString(), ","));
//                    sbYeshut.Append(string.IsNullOrEmpty(dr["shat_hatchala"].ToString()) ? "" : string.Concat(dr["shat_hatchala"].ToString(), ","));
//                    sbYeshut.Append(string.IsNullOrEmpty(dr["shat_yetzia"].ToString()) ? "" : string.Concat(dr["shat_yetzia"].ToString(), ","));
//                    sbYeshut.Append(int.Parse(dr["check_num"].ToString()));
//                    sbYeshut.Append(",");
//                    sbYeshut.Append(i.ToString());
//                    //arrYeshutId[i]=sbYeshut.ToString().Remove(sbYeshut.ToString().Length-1,1);
//                    arrYeshutId[i] = sbYeshut.ToString();

//                    arrTaarich[i] = string.IsNullOrEmpty(dr["Taarich"].ToString()) ? dCardDate : DateTime.Parse(dr["Taarich"].ToString());
//                    arrMisparSidur[i] = (string.IsNullOrEmpty(dr["mispar_sidur"].ToString()) ? 0 : int.Parse(dr["mispar_sidur"].ToString()));
//                    arrShatHatchala[i] = (string.IsNullOrEmpty(dr["shat_hatchala"].ToString()) ? dCardDate : DateTime.Parse(string.Concat(DateTime.Parse(dr["Taarich"].ToString()).ToShortDateString(), " ", dr["shat_hatchala"].ToString())));
//                    arrShatYetzia[i] = (string.IsNullOrEmpty(dr["shat_yetzia"].ToString()) ? dCardDate : DateTime.Parse(string.Concat(DateTime.Parse(dr["Taarich"].ToString()).ToShortDateString(), " ", dr["shat_yetzia"].ToString())));
//                    arrHeara[i] = dr["error_desc"].ToString();
//                    i++;
//                }

//                oDal.AddParameter("MISPAR_ISHI", ParameterType.ntOracleInteger, arrMisparIshi, ParameterDir.pdInput);
//                oDal.AddParameter("KOD_SHGIA", ParameterType.ntOracleInteger, arrKodShgia, ParameterDir.pdInput);
//                oDal.AddParameter("YESHUT_ID ", ParameterType.ntOracleVarchar, arrYeshutId, ParameterDir.pdInput);
//                oDal.AddParameter("TAARICH ", ParameterType.ntOracleDate, arrTaarich, ParameterDir.pdInput);
//                oDal.AddParameter("MISPAR_SIDUR", ParameterType.ntOracleInteger, arrMisparSidur, ParameterDir.pdInput);
//                oDal.AddParameter("SHAT_HATCHALA", ParameterType.ntOracleDate, arrShatHatchala, ParameterDir.pdInput);
//                oDal.AddParameter("SHAT_YETZIA", ParameterType.ntOracleDate, arrShatYetzia, ParameterDir.pdInput);
//                oDal.AddParameter("HEARA", ParameterType.ntOracleVarchar, arrHeara, ParameterDir.pdInput);
//                // Set the command text on an OracleCommand object
//                oDal.ExecuteSQL("insert into TB_SHGIOT(MISPAR_ISHI,KOD_SHGIA,YESHUT_ID,TAARICH,MISPAR_SIDUR,SHAT_HATCHALA,SHAT_YETZIA,HEARA) values (:MISPAR_ISHI,:KOD_SHGIA,:YESHUT_ID,:TAARICH,:MISPAR_SIDUR,:SHAT_HATCHALA,:SHAT_YETZIA,:HEARA)");          
//               // //oDal.ExecuteSP("pro_del_tb_shgiot");
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }          
//        }
       

//        private errNesiaMeshtana GetNesiaMeshtanaTimeFromTable(int iMerkazErua, int iMikumShaonKnisa, int iMikumShaonYetzia)
//        {
//            try
//            {
//                clDal oDal = new clDal();
                
//                try
//                {//מחזיר אם קיימות הגדרות כניסה ויציאה
//                    oDal.AddParameter("p_result", ParameterType.ntOracleInteger, null, ParameterDir.pdReturnValue);
//                    oDal.AddParameter("p_merkaz_erua", ParameterType.ntOracleInteger, iMerkazErua, ParameterDir.pdInput);
//                    oDal.AddParameter("p_mikum_shaon_knisa", ParameterType.ntOracleInteger, iMikumShaonKnisa, ParameterDir.pdInput);
//                    oDal.AddParameter("p_mikum_shaon_yetiza", ParameterType.ntOracleInteger, iMikumShaonYetzia, ParameterDir.pdInput);
//                    oDal.ExecuteSP(clDefinitions.cFnIsZmanNesiaDefine);

//                    return (errNesiaMeshtana)(int.Parse(oDal.GetValParam("p_result").ToString()));
//                }
//                catch (Exception ex)
//                {
//                    throw ex;
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void SetOvedYomAvodaDetails(int iMisparIshi, DateTime dCardDate)
//        {
//           oOvedYomAvodaDetails = new clOvedYomAvoda(iMisparIshi, dCardDate);

//            //clDal oDal = new clDal();

//            //try
//            //{//מחזיר נתוני עובד: 
//            //    oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
//            //    oDal.AddParameter("p_date", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
//            //    oDal.AddParameter("p_coll_meafeney_ovdim", ParameterType.ntOracleObject, oCollMeafyneyOved, ParameterDir.pdOutput, "COLL_MEAFYENEY_OVED");

//            //    oDal.ExecuteSP(clGeneral.cProGetOvedYomAvodaUDT);

//            //    oCollMeafyneyOved = (COLL_MEAFYENEY_OVED)oDal.GetObjectParam("p_coll_meafeney_ovdim");
//            //}
//            //catch (Exception ex)
//            //{
//            //    throw ex;
//            //}
//        }

        
      

//        //private string GetOneParam(int iParamNum, DateTime dDate)
//        //{   //הפונקציה מקבלת קוד פרמטר ותאריך ומחזירה את הערך
//        //    string sParamVal="";
//        //    DataRow[] dr;

//        //    //dr = dtParameters.Select(string.Concat("kod_param=", iParamNum.ToString(), " and Convert('", dDate.ToShortDateString(), "','System.DateTime') >= me_taarich and Convert('", dDate.ToShortDateString(), "', 'System.DateTime') <= ad_taarich"));
//        //    dr = dtParameters.Select(string.Concat("kod_param=", iParamNum.ToString()));
//        //    if (dr.Length > 0)
//        //    {
//        //        sParamVal = dr[0]["erech_param"].ToString();
//        //    }
//        //    return sParamVal;
//        //}

//        //private DataTable GetKdsParametrs()
//        //{
//        //    clDal oDal = new clDal();
//        //    DataTable dt = new DataTable();

//        //    try
//        //    {   //מחזיר טבלת פרמטרים:                 
//        //        oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
//        //        oDal.ExecuteSP(clDefinitions.cProGetParameters, ref dt);

//        //        return dt;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw ex;
//        //    }
//        //}
       
//        private bool CheckEggedHourValid(string sHour)
//        {
//            string[] arr;
//            bool bError = false;
//            //מקבל מחרוזת בפורמט XX:XX ומחזיר שגיאה אם לא בין 00:00 ל31:59-
//            try
//            {
//                arr = sHour.Split(char.Parse(":"));
//                if (!((int.Parse(arr[0])) >= 0 && (int.Parse(arr[0])) <= 31))
//                {
//                    bError = true;
//                }
//                if (!((int.Parse(arr[1])) >= 0 && (int.Parse(arr[1])) <= 59))
//                {
//                    bError = true;
//                }

//                return bError;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private DataTable GetOvedMatzav(int iMisparIshi, DateTime dDate)
//        {
//            clDal oDal = new clDal();
//            DataTable dt = new DataTable(); 
//            try
//            {   //מחזיר טבלת פרמטרים:                                
//                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
//                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dDate, ParameterDir.pdInput);
//                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
//                oDal.ExecuteSP(clDefinitions.cProGetOvedMatzav,ref dt);

//                return dt;

//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private bool IsDuplicateShatYetiza(int iMisparIshi, DateTime dCardDate)
//        {
//            clDal oDal = new clDal();
//            try
//            {
//                //בודקים אם ביום עבודה לעובד מסויים קיימות פעילויות עם אותה שעת יציאה
//                //אם כן, נחזיר TRUE
//                oDal.AddParameter("p_result", ParameterType.ntOracleInteger, null, ParameterDir.pdReturnValue);
//                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
//                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
//                oDal.ExecuteSP(clDefinitions.cFnIsDuplicateShatYetiza);

//                return int.Parse(oDal.GetValParam("p_result").ToString()) > 0;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
      
//        //public void MainOvedErrors(int iMisparIshi, DateTime dCardDate)
//        //{   
//        //    //M A I N   P R O C E D U R E
//        //    //Get all oved details and calls all check function

//        //    DataTable dtErrors = new DataTable();
//        //    clDefinitions oDefinition = new clDefinitions();
//        //    DataTable dtYamimMeyuchadim;
//        //    clUtils oUtils = new clUtils();
//        //    int iSugYom;
//        //    //int iCardDay;
//        //    try
//        //    {
//        //        //Get LookUp Tables
//        //        dtLookUp = oUtils.GetLookUpTables();

//        //        //Get Parameters Table
//        //        //dtParameters = GetKdsParametrs();
//        //        dtYamimMeyuchadim=clDefinitions.GetYamimMeyuchadim();

//        //        iSugYom = clDefinitions.GetSugYom(dtYamimMeyuchadim, dCardDate);

//        //        //Set global variable with parameters
//        //        SetParameters(dCardDate, iSugYom);

//        //        //Get Meafyeny Ovdim
//        //        GetMeafyeneyOvdim(iMisparIshi, dCardDate);

//        //        //Get Meafyeney Sug Sidur
//        //        dtSugSidur = clDefinitions.GetSugeySidur();

                
//        //        //Build Error DataTable
//        //        BuildErrorDataTable(ref dtErrors);
                
//        //        //Get Oved Matzav
//        //        dtMatzavOved=GetOvedMatzav(iMisparIshi, dCardDate);

//        //       // iCardDay =clGeneral.GetCardDay(dCardDate);
//        //        //בדיקות ברמת יום עבודה
//        //        //Get yom avoda details
//        //       // dtOvedCardDetails = GetOvedYomAvodaDetails(iMisparIshi, dCardDate);
//        //        SetOvedYomAvodaDetails(iMisparIshi, dCardDate);
//        //        //if (dtOvedCardDetails.Rows.Count>0)
//        //        if (oOvedYomAvodaDetails.OvedDetailsExists)
//        //        {
//        //            //Check01
//        //            IsHrStatusValid01(dCardDate,iMisparIshi, ref dtErrors);
               
//        //            //Check30
//        //            IsSidurLina30(dCardDate,ref dtErrors);

//        //            //Check36
//        //            IsHalbashValid36(dCardDate,ref dtErrors);

//        //            //Check27
//        //            IsSimunNesiaValid27(dCardDate,ref dtErrors);
                                       
//        //            //Check103
//        //            IsDuplicateShatYetiza103(iMisparIshi, dCardDate, ref dtErrors);

//        //            //Check163
//        //            IsHashlamaLeYomAvodaValid163(iMisparIshi, dCardDate, ref dtErrors);

//        //            //Check 26
//        //            IsZakaiLeNesiot26(iMisparIshi,dCardDate, ref dtErrors);

//        //            //Check 47
//        //            IsShbatHashlamaValid47(iMisparIshi,dCardDate, ref dtErrors);

//        //            //Get Oved Details
//        //            dtDetails = oDefinition.GetOvedDetails(iMisparIshi, dCardDate);

//        //            if (dtDetails.Rows.Count > 0)
//        //            {
//        //                //Insert Oved Details to Class
//        //                htEmployeeDetails = oDefinition.InsertEmployeeDetails(dtDetails,dCardDate, ref iLastMisaprSidur);
                        
//        //                //Check132
//        //                IsOvodaInMachalaAllowed132(dCardDate, iMisparIshi, ref dtErrors);

//        //                CheckAllError(ref dtErrors, dCardDate, iMisparIshi);
//        //            }

//        //            //Write errors to tb_shgiot
//        //            if (dtErrors.Rows.Count > 0)
//        //            {
//        //                InsertErrorsToTbShgiot(dtErrors, dCardDate);
//        //            }
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw ex;
//        //    }
//        //}

      
//        private bool IsOvedMatzavExists(string sKodMatzav)
//        {
//            DataRow[] dr;
//            bool bOvedMatzavExists;
//            try
//            {
//                //נחזיר אם קיים לעובד מצב
//                // למשל מצב 1 מציין אם עובד פעיל, מצב 5 מציין אם עובד במצב של מחלה 
//                dr = dtMatzavOved.Select(string.Concat("kod_matzav=",sKodMatzav));
//                bOvedMatzavExists = dr.Length > 0;
//                return bOvedMatzavExists;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void GetMeafyeneyOvdim(int iMisparIshi, DateTime dCardDate)
//        {
//            //clOvdim oOvdim = new clOvdim();
//            oMeafyeneyOved = new clMeafyenyOved(iMisparIshi, dCardDate);                      
//        }

//        private void SetParameters(DateTime dCardDate, int iSugYom)
//        {
//            oParam = new clParameters(dCardDate, iSugYom);           
//        }

        

//        private void BuildErrorDataTable(ref DataTable dtErrors)
//        {
//            DataColumn col = new DataColumn();
//            try
//            {
//                col = new DataColumn("check_num", System.Type.GetType("System.Int32"));
//                dtErrors.Columns.Add(col);

//                col = new DataColumn("mispar_ishi", System.Type.GetType("System.Int32"));
//                dtErrors.Columns.Add(col);

//                col = new DataColumn("taarich", System.Type.GetType("System.String"));
//                dtErrors.Columns.Add(col);

//                col = new DataColumn("mispar_sidur", System.Type.GetType("System.Int32"));
//                dtErrors.Columns.Add(col);

//                col = new DataColumn("error_desc", System.Type.GetType("System.String"));
//                dtErrors.Columns.Add(col);

//                col = new DataColumn("pitzul_hafsaka", System.Type.GetType("System.Int32"));
//                dtErrors.Columns.Add(col);

//                col = new DataColumn("makat_nesia", System.Type.GetType("System.Int64"));
//                dtErrors.Columns.Add(col);
                
//                col = new DataColumn("shat_yetzia", System.Type.GetType("System.String"));
//                dtErrors.Columns.Add(col);

//                col = new DataColumn("Halbasha", System.Type.GetType("System.String"));
//                dtErrors.Columns.Add(col);

//                col = new DataColumn("halbash_kod", System.Type.GetType("System.Int32"));
//                dtErrors.Columns.Add(col);
                                
//                col = new DataColumn("Kisuy_tor", System.Type.GetType("System.String"));
//                dtErrors.Columns.Add(col);

//                col = new DataColumn("Hashlama", System.Type.GetType("System.String"));
//                dtErrors.Columns.Add(col);

//                col = new DataColumn("shbaton", System.Type.GetType("System.Int32"));
//                dtErrors.Columns.Add(col);

//                col = new DataColumn("kod_matzav", System.Type.GetType("System.Int32"));
//                dtErrors.Columns.Add(col);

//                //col = new DataColumn("km_visa_lepremia", System.Type.GetType("System.String"));
//                //dtErrors.Columns.Add(col);

//                col = new DataColumn("sidur_visa_kod", System.Type.GetType("System.Int32"));
//                dtErrors.Columns.Add(col);

//                col = new DataColumn("shat_hatchala", System.Type.GetType("System.String"));
//                dtErrors.Columns.Add(col);

//                col = new DataColumn("hatchala_limit_hour", System.Type.GetType("System.String"));
//                dtErrors.Columns.Add(col);
               

//                col = new DataColumn("shat_gmar", System.Type.GetType("System.String"));
//                dtErrors.Columns.Add(col);

//                col = new DataColumn("gmar_limit_hour", System.Type.GetType("System.String"));
//                dtErrors.Columns.Add(col);

//                col = new DataColumn("hamarat_shabat", System.Type.GetType("System.String"));
//                dtErrors.Columns.Add(col);

//                col = new DataColumn("oto_no", System.Type.GetType("System.Int64"));
//                dtErrors.Columns.Add(col);

//                col = new DataColumn("nesia_time", System.Type.GetType("System.Decimal"));
//                dtErrors.Columns.Add(col);

//                col = new DataColumn("sidur_time", System.Type.GetType("System.Decimal"));
//                dtErrors.Columns.Add(col);

//                col = new DataColumn("mispar_visa", System.Type.GetType("System.Int64"));
//                dtErrors.Columns.Add(col);

//                col = new DataColumn("total_hashlamot", System.Type.GetType("System.Int32"));
//                dtErrors.Columns.Add(col);

//                col = new DataColumn("zman_maximum", System.Type.GetType("System.Int32"));
//                dtErrors.Columns.Add(col);

//                col = new DataColumn("bitul_zman_nesiot", System.Type.GetType("System.String"));
//                dtErrors.Columns.Add(col);

//                col = new DataColumn("Out_Michsa", System.Type.GetType("System.String"));
//                dtErrors.Columns.Add(col);

//                col = new DataColumn("visa", System.Type.GetType("System.String"));
//                dtErrors.Columns.Add(col); 

//                col = new DataColumn("mikum_shaon_knisa", System.Type.GetType("System.String"));
//                dtErrors.Columns.Add(col);

//                col = new DataColumn("mikum_shaon_Yetzia", System.Type.GetType("System.String"));
//                dtErrors.Columns.Add(col); 
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void InsertErrorRow(clSidur oSidur, ref DataRow drNew, string sErrorDesc, int iErrorNum)
//        {
//            drNew["check_num"] = iErrorNum;
//            drNew["mispar_ishi"] = oSidur.iMisparIshi; 
//            drNew["mispar_sidur"] = oSidur.iMisparSidur;
//            drNew["taarich"] = oSidur.sSidurDate;
//            drNew["shat_hatchala"] = (oSidur.sShatHatchala == null ? "" : oSidur.sShatHatchala.ToString());            
//            //drNew["makat_nesia"] = oSidur.//int.Parse(dr["makat_nesia"].ToString());
//            drNew["error_desc"] = sErrorDesc;
//        }
//        private void InsertPeilutErrorRow(clPeilut oPeilut, ref DataRow drNew)
//        {
//            drNew["Shat_Yetzia"] = string.IsNullOrEmpty(oPeilut.sShatYetzia) ? "" : oPeilut.sShatYetzia.ToString();
//            drNew["makat_nesia"] = oPeilut.lMakatNesia;                        
//        }
//        private string GetLookUpKods(string sTableName)
//        {
//            //The function get lookup table name and return all kods in string, separate by comma
//            string sLookUp = "";
//            DataRow[] drLookUpAll;
//            try
//            {
//                drLookUpAll = dtLookUp.Select(string.Concat("table_name='", sTableName,"'"));
//                foreach (DataRow drLookUp in drLookUpAll)
//                {
//                    sLookUp = string.Concat(sLookUp, drLookUp["Kod"].ToString(), ",");
//                }
//                sLookUp = sLookUp.Substring(0, sLookUp.Length - 1);

//                return sLookUp;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void CheckAllError(ref DataTable dtErrors, DateTime dCardDate, int iMisparIshi)
//        {
//            clSidur oSidur= new clSidur();
//            clPeilut oPeilut = new clPeilut();
//            clKavim oKavim = new clKavim();
//            //int iKey = 0;
//            int iCount = 0;                       
//            //string sPrevShatGmar = "";
//            //string sPrevShatHatchala = "";
//            //int iSidurPrevPitzulHafsaka=0;
//            DataRow drNew;
//            bool bFirstSidur = true;
//            bool bFirstPeilut = true;
//            //bool bPrevSidurEilat;                                               
//            //int iPrevLoLetashlum=0;
//            float fSidurTime = 0;
//            int iTotalHashlamotForSidur = 0;//סה"כ השלמות בכל הסידורים 
//            int iHashlama;
//            int iTotalTimePrepareMechineForSidur = 0;
//            int iTotalTimePrepareMechineForDay = 0;
//            int iTotalTimePrepareMechineForOtherMechines = 0;
//            clGeneral.enEmployeeType enEmployeeType;           
//            DataRow[] drSugSidur;
//            DateTime dPrevStartPeilut=new DateTime();
//            DateTime dPrevEndPeilut = new DateTime();
//            try
//            {
              
////                enEmployeeType = (clGeneral.enEmployeeType)int.Parse(oOvedYomAvodaDetails.sKodHevra);
//                enEmployeeType = (clGeneral.enEmployeeType)(oOvedYomAvodaDetails.iKodHevra);
       
//                //נעבור על כל הסידורים
//                //foreach (DictionaryEntry deEntry in htEmployeeDetails)
//                //{                    
//                //    iKey = int.Parse(deEntry.Key.ToString());                
//                for (int i = 0; i < htEmployeeDetails.Count; i++)
//                {
//                    oSidur = (clSidur)htEmployeeDetails[i];

//                    ////set dataset sidurim with sidur details
//                    //SetSidurDetails(dCardDate, ref oSidur, out iResult);

                    
//                    //TODO: מה קורה אם סוג סידור 0
//                    drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil,dCardDate,  dtSugSidur);

//                    //נחשב את זמן הסידור
//                    fSidurTime =clDefinitions.GetSidurTimeInMinuts(oSidur);

//                    //שגיאה 86 - סה"כ זמן הכנת מכונה לסידור
//                    iTotalTimePrepareMechineForSidur = 0;

//                    //בדיקות ברמת סידור   
//                    IsSidurExists9(ref oSidur, ref dtErrors);
//                    IsHourMissing15(ref oSidur,ref dtErrors);
//                    IsSidurChariga33(ref oSidur,ref dtErrors);
//                    IsPitzulHafsakaValid20(i,ref oSidur,ref dtErrors);
//                    IsHashlamaValid137(ref oSidur,ref dtErrors);                    
//                    IsShabatPizulValid23(ref oSidur, ref dtErrors);
//                    IsKmExists96(ref oSidur, ref dtErrors);
//                    IsOutMichsaValid118(ref oSidur, ref dtErrors);
//                    IsOutMichsaValid40(ref oSidur, ref dtErrors);
//                    IsHamaraValid42(ref oSidur, ref dtErrors);
//                    IsHamaratShabatValid39(drSugSidur,ref oSidur, ref dtErrors);
//                    IsDriverLessonsNumberValid136(drSugSidur, ref oSidur, ref dtErrors);
//                    IsZakaiLeChariga34(ref oSidur,ref dtErrors);
//                    IsHashlamaForSidurValid48(fSidurTime,ref oSidur,ref dtErrors);
//                    IsHalbashaInSidurValid37(ref oSidur, ref iCount);                   
//                    IsPitzulAndNotZakai25(ref oSidur, ref dtErrors);
//                    IsSidurVisaValid57(ref oSidur, ref dtErrors);
//                    //Invoke from error 20
//                   // IsOneSidurValid22(ref oSidur, ref dtErrors);
//                    IsVisaInSidurRagil58(ref oSidur, ref dtErrors);
//                    IsSidurHourValid14(dCardDate,ref oSidur, ref dtErrors);  
//                    IsHashlamatHazmanaValid49(fSidurTime, ref oSidur, ref dtErrors);
//                    IsYomVisaValid56(ref oSidur, ref dtErrors);                    
//                    IsZakaiLehamara44(drSugSidur,ref oSidur, ref dtErrors);
//                    IsHamaratShabatAllowed43(dCardDate, ref oSidur, ref dtErrors);
//                    IsNesiaMeshtanaDefine150(dCardDate,ref oSidur, ref dtErrors);
//                    IsOnePeilutExists127(ref oSidur, ref dtErrors);
//                    IsSidurAllowedForEggedTaavora148(enEmployeeType, ref  oSidur, ref  dtErrors);
//                    IsSidurNetzerNotValidForOved124( ref  oSidur, ref  dtErrors);
//                    IsSidurNamlakWithoutNesiaCard13(ref oSidur,ref oPeilut, ref dtErrors);
//                    IsSidurAvodaValidForTaarich160(ref oSidur, ref dtErrors);
//                    IsSidurValidInShabaton50(ref oSidur, ref dtErrors);
//                    IsSidurTafkidValid145(ref oSidur, ref dtErrors);
//                    IsCharigaValid32(ref oSidur, ref dtErrors);                    
//                    IsSidurSummerValid164(ref  oSidur, ref dtErrors);
//                    IsSidurNAhagutValid161(drSugSidur, ref oSidur, ref dtErrors);
//                    IsAvodatNahagutValid165(drSugSidur, ref oSidur, ref dtErrors);
//                    if (!(bFirstSidur))//לא נבצע את הבדיקה לסידור הראשון
//                    {
//                        IsSidurimHoursNotValid16(i, ref oSidur, ref dtErrors);
//                        IsPitzulSidurInShabatValid24(dCardDate,i, ref oSidur, ref dtErrors);
                        
//                    }
//                    else
//                    {//רק לסידור הראשון נבצע את בדיקה ,35,141
//                        IsSidurExistsInShlila35(iMisparIshi,dCardDate,ref dtErrors);
//                        IsOvedEggedTaavoraKodValid141(enEmployeeType, ref oSidur, ref dtErrors);                        
//                        //סידור יחיד ביום
//                        IsHashlamaForComputerAndAccidentValid45(ref oSidur, ref dtErrors);                        
//                    }
                    
//                    //נתונים של סידור קודם
//                    //sPrevShatGmar = oSidur.sShatGmar;
//                    //iPrevLoLetashlum = oSidur.iLoLetashlum;
//                    //iSidurPrevPitzulHafsaka = string.IsNullOrEmpty(oSidur.sPitzulHafsaka) ? 0 : int.Parse(oSidur.sPitzulHafsaka);
                    
//                    //נשמור את הנתון אם סידור אילת של הסידור הקודם - )
//                    //bPrevSidurEilat = oSidur.bSidurEilat;
                    
//                    //סה"כ השלמות בכל הסידורים, נבדוק אם אין חריגה בבדיקה 142
//                    iHashlama =string.IsNullOrEmpty(oSidur.sHashlama) ? 0 : int.Parse(oSidur.sHashlama);
//                    iTotalHashlamotForSidur = iTotalHashlamotForSidur + iHashlama;

                    
//                    //foreach (DictionaryEntry dePeilutEntry in oSidur.htPeilut)
//                    for (int j = 0; j < ((KdsBatch.clSidur)(htEmployeeDetails[i])).htPeilut.Count; j++)
//                    {
//                        //iKey = int.Parse(dePeilutEntry.Key.ToString());
//                        oPeilut = (clPeilut)oSidur.htPeilut[j];

                        
//                        //IsShatYetizaExist92(ref oSidur,ref oPeilut, ref dtErrors);
//                        IsKodNesiaExists81(ref  oSidur, ref  oPeilut, ref  dtErrors,  dCardDate);
//                        IsMisparSidurInEilatExists139(ref oPeilut, ref oSidur, ref dtErrors);
//                        IsMisparSidurEilatInRegularSidurExists140(ref oPeilut, ref oSidur, ref dtErrors);
//                        IsPeilutShatYeziaValid113(ref  oSidur, ref  oPeilut, ref  dtErrors, dCardDate);
//                        IsElementTimeValid129(fSidurTime, ref  oSidur, ref  oPeilut, ref dtErrors);
//                        IsShatPeilutNotValid121(dCardDate, ref oSidur, ref oPeilut, ref dtErrors);
//                        IsPeilutInSidurValid84(ref oSidur, ref oPeilut,ref dtErrors);
//                        IsOtoNoValid69(ref oSidur, ref oPeilut, ref dtErrors);
//                        IsZakaiLina31(ref oSidur, ref oPeilut, ref dtErrors);
//                        IsOtoNoExists68(drSugSidur, ref oSidur, ref oPeilut, ref dtErrors);
//                        IsNesiaTimeNotValid91(fSidurTime, dCardDate, ref oSidur, ref oPeilut, ref dtErrors);
//                        IsTeoodatNesiaValid52(ref oSidur, ref oPeilut, ref dtErrors);
//                        IsNesiaInSidurVisaAllowed125(ref oSidur, ref oPeilut, ref dtErrors);
//                        IsHmtanaTimeValid166(ref oSidur, ref oPeilut, ref dtErrors);
//                        if (!(bFirstPeilut))//לא נבצע את הבדיקה לפעילות הראשונה 
//                        {
//                            IsCurrentPeilutInPrevPeilut162(dPrevStartPeilut, dPrevEndPeilut,ref oSidur, ref oPeilut, ref dtErrors);
                            
//                        }
//                        SetPeilutTime(ref oPeilut, ref dPrevStartPeilut, ref dPrevEndPeilut);
//                        IsTimeForPrepareMechineValid86(ref iTotalTimePrepareMechineForSidur, ref iTotalTimePrepareMechineForDay, ref iTotalTimePrepareMechineForOtherMechines, ref oSidur, ref oPeilut, ref dtErrors);                                                                                                             
                                                       
//                        bFirstPeilut = false;
//                    }
//                    if (!bFirstSidur)
//                    {
//                        //Check55
//                        IsSidurEilatValid55(i, bFirstSidur, ref oPeilut, ref oSidur, ref dtErrors);
//                    }
//                    //sPrevShatHatchala = oSidur.sShatHatchala;
//                    bFirstSidur = false;

//                    //Check 86
//                    CheckPrepareMechineForSidurValidity86(ref oSidur, iTotalTimePrepareMechineForSidur, ref dtErrors);
//                }

//                //שגיאה 37 - נבדוק אם לכל הסידורים לא מגיע הלבשה
//                if (iCount == htEmployeeDetails.Count)
//                {
//                    drNew = dtErrors.NewRow();
//                    drNew["mispar_ishi"] =iMisparIshi; 
//                    drNew["mispar_sidur"] = 0;
//                    drNew["taarich"] = dCardDate.ToShortDateString(); 
//                    drNew["error_desc"]= "לא מגיעה הלבשה לסידורים";
//                    drNew["check_num"]=enErrors.errHalbashaInSidurNotValid.GetHashCode();
//                    dtErrors.Rows.Add(drNew);
//                }
//                //check 142
//                IsTotalHashlamotInCardValid(iTotalHashlamotForSidur, ref oSidur, ref dtErrors);
//                //Check 86
//                CheckPrepareMechineForDayValidity86(iMisparIshi, iTotalTimePrepareMechineForDay, dCardDate, ref dtErrors);
//                //Check86
//                CheckPrepareMechineOtherElementForDayValidity86(iMisparIshi, iTotalTimePrepareMechineForOtherMechines, dCardDate, ref dtErrors);
//            }
//            catch (Exception ex)
//            {
//                throw (ex);
//            }
//        }

//        private void SetPeilutTime(ref clPeilut oPeilut, ref DateTime dPrevStartPeilut, ref DateTime dPrevEndPeilut)
//        {
//            double dblTimeInMinutes=0;
//            try
//            {//נשמור את זמן הפעילות 
//                dPrevStartPeilut = oPeilut.dFullShatYetzia;
//                if (oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode())
//                {
//                    //אם אלמנט זמן
//                    //אבל לא אלמנט זמן מסוג המתנה
//                    if ((oPeilut.sElementInMinutes == "1") && (!oPeilut.bElementHamtanaExists))
//                    {
//                        dblTimeInMinutes = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));
//                    }
//                }
//                else
//                {
//                    dblTimeInMinutes = oPeilut.iMazanTichnun;
//                }
//                dPrevEndPeilut = dPrevStartPeilut.AddMinutes(dblTimeInMinutes);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void IsHourMissing15(ref clSidur oSidur,ref DataTable dtErrors)
//        {   //בדיקה ברמת סידור         
//            DataRow drNew;           
//            try
//            {
//                if (oSidur.dFullShatHatchala.Year < clGeneral.cYearNull)
//                {
//                    drNew = dtErrors.NewRow();
//                    InsertErrorRow(oSidur, ref drNew, "חסרה שעת התחלה", enErrors.errHourMissing.GetHashCode());
//                    dtErrors.Rows.Add(drNew);
//                }
//                if (string.IsNullOrEmpty(oSidur.sShatGmar))
//                {
//                    drNew = dtErrors.NewRow();
//                    InsertErrorRow(oSidur, ref drNew, "חסרה שעת גמר", enErrors.errHourMissing.GetHashCode());
//                    dtErrors.Rows.Add(drNew);
//                }              
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsSidurLina30(DateTime dCardDate, ref DataTable dtErrors)
//        {                 
//            DataRow drNew;
//            string sLookUp = "";
//            bool bError;

//            try
//            {
//                //בדיקה ברמת יום עבודה                
//                if (string.IsNullOrEmpty(oOvedYomAvodaDetails.sLina))
//                {
//                    bError = true;
//                }
//                else
//                {
//                   sLookUp = GetLookUpKods("ctb_lina");
//                   bError = (sLookUp.IndexOf(oOvedYomAvodaDetails.sLina) == -1);
//                }
                             
//                if (bError)
//                {
//                    drNew = dtErrors.NewRow();
//                    drNew["check_num"] = enErrors.errLinaValueNotValid.GetHashCode();
//                    drNew["mispar_ishi"] = oOvedYomAvodaDetails.iMisparIshi;
//                    drNew["taarich"] = dCardDate.ToShortDateString();
//                    //drNew["Lina"] = int.Parse(sLina);
//                    drNew["error_desc"] = "ערך לינה שגוי";                        
//                    dtErrors.Rows.Add(drNew);
//                }                               
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsSidurChariga33(ref clSidur oSidur, ref DataTable dtErrors)
//        {
            
//            DataRow drNew;            
//            int iShatHatchala, iShatGmar, iMeafyen3, iMeafyen4;
//            string sLookUp;
//            bool bError = false;
//            try
//            {
//                //בדיקה ברמת סידור
//                //חריגה (שדה ברמת סידור) הוא תקין אם מקבל את אחד הערכים 0, 1, 3, 6, 7, 8. אם מקבל ערך אחר אז זה שגוי. ערך תקין אם הוא קיים בטבלה  CTB_DIVUCH_HARIGA_MESHAOT. יתכן שיגיע נתון מהסדרן. אם ביקשו חריגה והמרווח בין החתמת שעון כניסה/יציאה לזמן כניסה/יציאה המוגדר לעובד (עפ"י מאפייני עובד 3, 4)קטן מפרמטר 41 (זמן חריגה התחלה / גמר על חשבון העובד) אז זו גם שגיאה.
//                if (string.IsNullOrEmpty(oSidur.sChariga))
//                {
//                    bError = true;
//                }
//                else
//                {
//                    sLookUp = GetLookUpKods("ctb_divuch_hariga_meshaot");
//                    //אם ערך חריגה לא נמצא בטבלה
//                    if (sLookUp.IndexOf(oSidur.sChariga) != -1)
//                    {
//                        switch  (oSidur.sChariga)
//                        {
//                            case "1":
//                                //אם שעת כניסה המוגדרת לעובד פחות שעת הכניסה בפועל קטנה מפרמטר 41 המגדיר מינימום לחריגה ומדווח חריגה נעלה שגיאה
//                                if (!string.IsNullOrEmpty(oSidur.sShatHatchala) && (oMeafyeneyOved.Meafyen3Exists))
//                                {
//                                    iShatHatchala = clDefinitions.GetTimeInMinuts(oSidur.sShatHatchala);
//                                    iMeafyen3 = clDefinitions.GetTimeInMinuts(oMeafyeneyOved.sMeafyen3);
//                                    if (iShatHatchala < iMeafyen3)
//                                    {
//                                        if ((iMeafyen3 - iShatHatchala) < oParam.iZmanChariga)
//                                        {
//                                            bError = true;
//                                        }
//                                    }
//                                }
//                                break;
//                            case "2":
//                                if (!string.IsNullOrEmpty(oSidur.sShatGmar) && (oMeafyeneyOved.Meafyen4Exists))
//                                {//כנ"ל לגבי שעת יציאה
//                                    iShatGmar = clDefinitions.GetTimeInMinuts(oSidur.sShatGmar);
//                                    iMeafyen4 = clDefinitions.GetTimeInMinuts(oMeafyeneyOved.sMeafyen4);
//                                    if (iShatGmar > iMeafyen4)
//                                    {
//                                        if ((iShatGmar - iMeafyen4) < oParam.iZmanChariga)
//                                        {
//                                            bError = true;
//                                        }
//                                    }
//                                }
//                                break;
//                            case "3":
//                                //אם שעת כניסה המוגדרת לעובד פחות שעת הכניסה בפועל קטנה מפרמטר 41 המגדיר מינימום לחריגה ומדווח חריגה נעלה שגיאה
//                                if (!string.IsNullOrEmpty(oSidur.sShatHatchala) && (oMeafyeneyOved.Meafyen3Exists))
//                                {
//                                    iShatHatchala = clDefinitions.GetTimeInMinuts(oSidur.sShatHatchala);
//                                    iMeafyen3 = clDefinitions.GetTimeInMinuts(oMeafyeneyOved.sMeafyen3);
//                                    if (iShatHatchala < iMeafyen3)
//                                    {
//                                        if ((iMeafyen3 - iShatHatchala) < oParam.iZmanChariga)
//                                        {
//                                            bError = true;
//                                        }
//                                    }
//                                }
//                                if (!string.IsNullOrEmpty(oSidur.sShatGmar) && (oMeafyeneyOved.Meafyen4Exists))
//                                {//כנ"ל לגבי שעת יציאה
//                                    iShatGmar = clDefinitions.GetTimeInMinuts(oSidur.sShatGmar);
//                                    iMeafyen4 = clDefinitions.GetTimeInMinuts(oMeafyeneyOved.sMeafyen4);
//                                    if (iShatGmar > iMeafyen4)
//                                    {
//                                        if ((iShatGmar - iMeafyen4) < oParam.iZmanChariga)
//                                        {
//                                            bError = true;
//                                        }
//                                    }
//                                }
//                                break;
//                        }                        
//                    }
//                    if (bError)
//                    {
//                        drNew = dtErrors.NewRow();
//                        InsertErrorRow(oSidur, ref drNew, "זמן החתמת שעון לא מזכה בחריגה", enErrors.errCharigaZmanHachtamatShaonNotValid.GetHashCode());
//                        dtErrors.Rows.Add(drNew);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsOneSidurValid22(int iSidur, ref clSidur oSidur,ref DataTable dtErrors)
//        {          
//            DataRow drNew;
//            try
//            {
//                //בדיקה ברמת סידור
//                //If ther is one sidur then pitizul field should be empty  
//                //אם יש סידור אחד ביום, לא ייתכן שיהיה לו ערך בשדה פיצול הפסקה. כנ"ל לגבי סידור אחרון ביום
//                if ((htEmployeeDetails.Count == 1) || (htEmployeeDetails.Count - 1 == iSidur))
//                {                    
//                    if (!string.IsNullOrEmpty(oSidur.sPitzulHafsaka))
//                    {
//                        drNew = dtErrors.NewRow();
//                        InsertErrorRow(oSidur, ref drNew, "שדה פיצול הפסקה שגוי", enErrors.errPizulValueNotValid.GetHashCode());
//                        dtErrors.Rows.Add(drNew);
//                    }
//                }
                      
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsPitzulHafsakaValid20(int iSidur,ref clSidur oSidur,ref DataTable dtErrors)
//        {           
//            DataRow drNew;
//            string sLookUp = "";
//            bool bError = false;
           
//            try
//            {
//                //בדיקה ברמת סידור
//                if (String.IsNullOrEmpty(oSidur.sPitzulHafsaka))
//                {
//                    bError = true;
//                }
//                else
//                {
//                    sLookUp = GetLookUpKods("ctb_pitzul_hafsaka");
//                    if (sLookUp.IndexOf(oSidur.sPitzulHafsaka) == -1)
//                    {
//                        bError = true;                        
//                    }                
//                }

//                if (bError)
//                {
//                    drNew = dtErrors.NewRow();
//                    InsertErrorRow(oSidur, ref drNew, "ערך  פיצול/הפסקה שגוי", enErrors.errPizulHafsakaValueNotValid.GetHashCode());
//                    dtErrors.Rows.Add(drNew);
//                }
//                else
//                {
//                    IsOneSidurValid22(iSidur, ref oSidur, ref dtErrors);
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsHalbashValid36(DateTime dCardDate, ref DataTable dtErrors)
//        {
                  
//            DataRow drNew;
//            string sLookUp = "";
//            bool bError = false;
//            try
//            {
//                //בדיקה ברמת יום עבודה
//                sLookUp = GetLookUpKods("ctb_zmaney_halbasha");
//                drNew = dtErrors.NewRow();
//                if (string.IsNullOrEmpty(oOvedYomAvodaDetails.sHalbasha))
//                {
//                    bError = true;                  
//                }
//                else
//                {
//                    if ((sLookUp.IndexOf(oOvedYomAvodaDetails.sHalbasha) == -1))
//                    {
//                        bError = true;
//                        drNew["halbasha"] = oOvedYomAvodaDetails.sHalbasha;
//                    }
//                }
//                if (bError)
//                {                    
//                    drNew["check_num"] = enErrors.errHalbashaNotvalid.GetHashCode();
//                    drNew["mispar_ishi"] = oOvedYomAvodaDetails.iMisparIshi;//int.Parse(dtOvedCardDetails.Rows[0]["mispar_ishi"].ToString());
//                    drNew["taarich"] = dCardDate.ToShortDateString();                    
//                    drNew["error_desc"] = "ערך הלבשה שגוי";
//                    dtErrors.Rows.Add(drNew);
//                }                               
//            }            
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsHashlamaValid137(ref clSidur oSidur,ref DataTable dtErrors)
//        {           
//            DataRow drNew;
//            string sLookUp = "";
//            bool bError=false;
//            try
//            {
//                //בדיקה ברמת סידור
//                if (string.IsNullOrEmpty(oSidur.sHashlama))
//                {
//                    bError = true;
//                }
//                else
//                {
//                    sLookUp = "1,2,9";
//                    if (((oSidur.bSidurMyuhad) && (oSidur.iMisparSidurMyuhad > 0)) || (!oSidur.bSidurMyuhad))
//                    {
//                        if (sLookUp.IndexOf(oSidur.sHashlama) == -1)
//                        {
//                            bError = true;                        
//                        }
//                    }
//                }
//                if (bError)
//                {
//                    drNew = dtErrors.NewRow();
//                    InsertErrorRow(oSidur, ref drNew, "ערך השלמה שגוי", enErrors.errHashlamaNotValid.GetHashCode());
//                    dtErrors.Rows.Add(drNew);
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsShatYetizaExist92(ref clSidur oSidur, ref clPeilut oPeilut,  ref DataTable dtErrors)
//        {
//            //Check if peilut Shat_yetzia  is missing
//            //DataRow[] drAll;
//            DataRow drNew;
           
//            string sShatYetzia;
//            try
//            {
//                //בדיקה ברמת פעילות                
//                sShatYetzia = oPeilut.sShatYetzia;// htEmployeeDetails["ShatYetzia"].ToString();
//                if (string.IsNullOrEmpty(sShatYetzia))
//                {
//                    drNew = dtErrors.NewRow();
//                    InsertErrorRow(oSidur, ref drNew, "ערך השלמה שגוי", enErrors.errShatYetizaNotExist.GetHashCode());
//                    InsertPeilutErrorRow(oPeilut, ref drNew);
//                    //drNew["Shat_Yetzia"] = sShatYetzia;
//                    //drNew["makat_nesia"] = oPeilut.lMakatNesia;
//                    dtErrors.Rows.Add(drNew);
//                }                
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        //private void IsKisuyTorValid117(ref DataTable dtErrors)
//        //{
           
//        //    //Check if KisuyTor field id valid          
//        //    DataRow drNew;
//        //    clSidur oSidur;           
//        //    string sKey = "";
//        //    int iKisutTor;
//        //    try
//        //    {
//        //        //בדיקה ברמת פעילות
//        //        foreach (DictionaryEntry deEntry in htEmployeeDetails)
//        //        {
//        //            sKey = deEntry.Key.ToString();
//        //            oSidur = (clSidur)htEmployeeDetails[sKey];
//        //            foreach (DictionaryEntry dePeilut in oSidur.htPeilut)
//        //            {
//        //                iKisuyTor = htEmployeeDetails["KisuyTor"].ToString();
//        //                if (iKisuyTor == 0) 
//        //                {
//        //                    drNew = dtErrors.NewRow();
//        //                    InsertErrorRow(oSidur, ref drNew, "שדה כיסוי תור שגוי", enErrors.errKisuyTorNotValid.GetHashCode());
//        //                    drNew["kisuy_tor"] = iKisuyTor;
//        //                    drNew["makat_nesia"] = htEmployeeDetails["MakatNesia"].ToString(); ;
//        //                    dtErrors.Rows.Add(drNew);
//        //                }
//        //            }
//        //        }
//        //        //foreach (DataRow dr in dtDetails.Rows)
//        //        //{
//        //        //    //Insert into dtErrors   
//        //        //    if (!(clGeneral.IsNumeric(dr["Kisuy_tor"].ToString())))
//        //        //    {
//        //        //        drNew = dtErrors.NewRow();
//        //        //        InsertErrorRow(oSidur, ref drNew, "שדה כיסוי תור שגוי", enErrors.errKisuyTorNotValid.GetHashCode());
//        //        //        //drNew["check_num"] = enErrors.errKisuyTorNotValid.GetHashCode();
//        //        //        //drNew["mispar_ishi"] = int.Parse(dr["mispar_ishi"].ToString());
//        //        //        //drNew["mispar_sidur"] = int.Parse(dr["mispar_sidur"].ToString());
//        //        //        //drNew["shat_yetzia"] = dr["shat_yetzia"].ToString();
//        //        //        //drNew["makat_nesia"] = int.Parse(dr["makat_nesia"].ToString());
//        //        //        //drNew["error_desc"] = "שדה כיסוי תור שגוי";
//        //        //        drNew["kisuy_tor"] = dr["kisuy_tor"].ToString();
//        //        //        dtErrors.Rows.Add(drNew);
//        //        //    }
//        //        //}
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw ex;
//        //    }
//        //}

//        private void IsShabatPizulValid23(ref clSidur oSidur,ref DataTable dtErrors)
//        {
           
//            DataRow drNew;
           
//            //בדיקה ברמת סידור
//            try
//            {
//                if (((oSidur.sShabaton == "1") || (oSidur.sSidurDay == clGeneral.enDay.Shabat.GetHashCode().ToString())) && (!string.IsNullOrEmpty(oSidur.sPitzulHafsaka)))
//                {
//                    drNew = dtErrors.NewRow();
//                    InsertErrorRow(oSidur, ref drNew, "ערך פיצול הפסקה ביום שבתון שגוי", enErrors.errShabatPizulValueNotValid.GetHashCode());
//                    dtErrors.Rows.Add(drNew);
//                }               
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsShbatHashlamaValid47(int iMisparIshi, DateTime dCardDate, ref DataTable dtErrors)
//        {
            
//            DataRow drNew;
//            //בודקים אם שדה השלמה ליום עבודה קיבל ערך 1 וגם יום הוא שבתון - שגיאה. שבתון יכול להיות יום שבת (חוזר מה- Oracle) או שבטבלת סוגי ימים מיוחדים הוא מוגדר כשבתון. במקרה זה רלוונטי רק ליום שמוגדר כשבתון, לא עבור ערב שבת/חג החל מכניסת שבת.
//            try
//            {
//                if (((oOvedYomAvodaDetails.sHashlamaLeyom == "1")) && ((oOvedYomAvodaDetails.sShabaton == "1") || (oOvedYomAvodaDetails.sSidurDay == clGeneral.enDay.Shabat.GetHashCode().ToString())))
//                {                   
//                    drNew = dtErrors.NewRow();
//                    drNew["check_num"] = enErrors.errShbatHashlamaNotValid.GetHashCode();
//                    drNew["mispar_ishi"] = iMisparIshi;
//                    drNew["taarich"] = dCardDate.ToShortDateString();
//                    drNew["error_desc"] = "ערך השלמה ביום שבתון שגוי";                   
                    
//                    //InsertErrorRow(oSidur, ref drNew, "ערך השלמה ביום שבתון שגוי", enErrors.errShbatHashlamaNotValid.GetHashCode());                    
//                    dtErrors.Rows.Add(drNew);
//                }              
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void IsHrStatusValid01(DateTime dCardDate, int iMisparIshi, ref DataTable dtErrors)
//        {
//            //DataRow[] drAll;
//            DataRow drNew;
       
//            try
//            {
//                //בדיקה ברמת יום עבודה
//                if (!IsOvedMatzavExists("1"))
//                {
//                    drNew = dtErrors.NewRow();
//                    drNew["mispar_ishi"] = iMisparIshi;                    
//                    drNew["check_num"] =  enErrors.errHrStatusNotValid.GetHashCode();
//                    drNew["taarich"] = dCardDate.ToShortDateString();
//                    drNew["error_desc"] = "ערך כא שגוי";
                    
//                    dtErrors.Rows.Add(drNew);
//                }              
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsHalbashaInSidurValid37(ref clSidur oSidur , ref int iCount)
//        {
            
//            string sLookUp = "";           
                       
//            try
//            {
//                //בדיקה ברמת סידור
//                if (!string.IsNullOrEmpty(oOvedYomAvodaDetails.sHalbasha))
//                {
//                    sLookUp = GetLookUpKods("ctb_zmaney_halbasha");

//                    //שדה הלבשה תקין
//                    //אם קוד הלבשה תקין אבל לסידור אין מאפיין 15, אז לא מגיע לסידור הלבשה
//                    if (((sLookUp.IndexOf(oOvedYomAvodaDetails.sHalbasha) != -1) && (!oSidur.bHalbashKodExists)) || ((sLookUp.IndexOf(oOvedYomAvodaDetails.sHalbasha) != -1) && (!oSidur.bSidurMyuhad)))
//                    {
//                        iCount++;
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsKmExists96(ref clSidur oSidur,ref DataTable dtErrors)
//        {           
//            //DataRow drNew;
           
//            //try
//            //{  //בדיקה ברמת סידור
//            //    //רק לסידור מסוג ויזה. לא הזינו ק.מ. יודעים שסידור הוא מסוג ויזה אם יש לו מאפיין ויזה (45) בטבלת סידורים מיוחדים.
//            //    if (((oSidur.bSidurMyuhad) && (oSidur.iMisparSidurMyuhad > 0)) || (!oSidur.bSidurMyuhad))
//            //    {
//            //        if ((oSidur.iKmVisaLepremia == 0) && (oSidur.bSidurVisaKodExists))
//            //        {
//            //            drNew = dtErrors.NewRow();
//            //            InsertErrorRow(oSidur, ref drNew, "חסר קמ", enErrors.errKmNotExists.GetHashCode());
//            //            dtErrors.Rows.Add(drNew);
//            //        }
//            //    }
//            //}
//            //catch (Exception ex)
//            //{
//            //    throw ex;
//            //}
//        }

//        private void IsSidurHourValid14(DateTime dCardDate, ref clSidur oSidur, ref DataTable dtErrors)
//        {         
//            DataRow drNew;            
//            DateTime dStartLimitHour;
//            DateTime dEndLimitHour;
//            DateTime dSidurStartHour;
//            DateTime dSidurEndHour;
//            //string sMisparSidur = "";           
          
//            try
//            { //בדיקה ברמת סידור
//                /*עבור כל סידור יש לבדוק האם שעת ההתחלה שלו תקינה והאם שעת הגמר שלו תקינה. נבדק אל מול פרמטרים 1 (שעת התחלה מותרת) ו- 3 (שעת גמר מינהל ונהגות) בהתאמה בטבלת פרמטרים חיצוניים.
//                א. בדיקת תקינות שעת התחלה:
//                לוקחים את שעת התחלת הסידור ומשווים אל מול הערך בפרמטר 1, אם השעה קטנה מהערך בפרמטר - שגוי.
//                ב. בדיקת תקינות שעת גמר:
//                לוקחים את התאריך של יום העבודה, מוסיפים לו אחד. את התאריך החדש שקיבלנו בצירוף השעה בפרמטר 3 משווים אל מול שעת הגמר של הסידור (מורכבת מתאריך ושעה). אם שעת הגמר של הסידור גדולה יותר - שגוי.

//                סידורים מיוחדים ייבדקו באותו אופן גם אל מול מאפיינים בטבלת מאפייני סידורים מיוחדים. אם לסידור מיוחד קיימים מאפיינים של שעת התחלה/גמר אז הם יותר "חזקים" מהערכים בטבלת פרמטרים חיצוניים. לכל סידור מיוחד יש את שעות ההגבלה שלו והן לא זהות בין סידור לסידור.*/
//                                //בדיקה ברמת סידור            
               
//                //אם סידור מיוחד 99- נבדוק  אם קיימים מאפיינים 7 ו8-
//                //אם קיימים נבדוק ששעת ההתחלה והגמר של הסידור נמצא בטווח
//               // sMisparSidur = oSidur.iMisparSidur.ToString();
//                if (oSidur.bSidurMyuhad)
//                {
//                    //שעת התחלה
//                    if (!(string.IsNullOrEmpty(oSidur.sShatHatchala)))
//                    {
//                        dSidurStartHour = oSidur.dFullShatHatchala;
//                        if (((oSidur.bShatHatchalaMuteretExists) && (!String.IsNullOrEmpty(oSidur.sShatHatchalaMuteret)))) //קיים מאפיין
//                        {
//                            dStartLimitHour = DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", DateTime.Parse(oSidur.sShatHatchalaMuteret).ToShortTimeString())); 
//                        }
//                        else
//                        {
//                            //if (String.IsNullOrEmpty(oParam.sSidurStartLimitHourParam1))
//                            //{
//                            //    dStartLimitHour = DateTime.Parse("01/01/1900 00:00");
//                            //}
//                            //else
//                            //{
//                            dStartLimitHour = oParam.dSidurStartLimitHourParam1;//DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", oParam.sSidurStartLimitHourParam1));
//                            //}
//                        }
//                        //if ((oSidur.bHourKod2Exists)) //קיים מאפיין
//                        //{
//                        //    dEndLimitHour = DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", oSidur.sHourKod2)); //int.Parse(oSidur.sHourKod2.Remove(2, 1));
//                        //}
//                        //else
//                        //{
//                        //    dEndLimitHour = DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", oParam.sSidurEndLimitHourParam3)); // oParam.sSidurEndLimitHourParam3;
//                        //}
//                        if ((dSidurStartHour < dStartLimitHour) && (dStartLimitHour.Year!=clGeneral.cYearNull)) //|| (dSidurStartHour > dEndLimitHour))
//                        {
//                            drNew = dtErrors.NewRow();
//                            InsertErrorRow(oSidur, ref drNew, "שעת ההתחלה לסידור מיוחד שגוי", enErrors.errSidurHourStartEndNotValid.GetHashCode());
//                            drNew["shat_hatchala"] = oSidur.sShatHatchala;
//                            drNew["hatchala_limit_hour"] = dStartLimitHour.ToString();
//                            //drNew["gmar_limit_hour"] = dEndLimitHour.ToString();
//                            dtErrors.Rows.Add(drNew);
//                        }
//                    }
//                    //שעת גמר
//                    if (!(string.IsNullOrEmpty(oSidur.sShatGmar)))
//                    {
//                        dSidurEndHour = oSidur.dFullShatGmar;
//                        if ((oSidur.bShatGmarMuteretExists) && (!String.IsNullOrEmpty(oSidur.sShatGmarMuteret)))//קיים מאפיין
//                        {
//                            dEndLimitHour = DateTime.Parse(string.Concat(dCardDate.AddDays(1).ToShortDateString(), " ", DateTime.Parse(oSidur.sShatGmarMuteret).ToShortTimeString())); // 
//                        }
//                        else
//                        {
//                            //if (String.IsNullOrEmpty(oParam.sSidurEndLimitHourParam3))
//                            //{
//                            //    dEndLimitHour = DateTime.Parse("01/01/1900 00:00");
//                            //}
//                            //else
//                            //{
//                            dEndLimitHour = oParam.dSidurEndLimitHourParam3.AddDays(1);//DateTime.Parse(string.Concat(dCardDate.AddDays(1).ToShortDateString(), " ", oParam.sSidurEndLimitHourParam3)); // oParam.dSidurEndLimitHourParam3;
//                           // }
//                        }
//                        //if (oSidur.bHourKod1Exists) //קיים מאפיין
//                        //{
//                        //    dStartLimitHour = DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", oSidur.sHourKod1)); ;
//                        //}
//                        //else
//                        //{
//                        //    dStartLimitHour = DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", oParam.sSidurStartLimitHourParam1)); 
//                        //}
//                        if ((dSidurEndHour > dEndLimitHour) && (dEndLimitHour.Year!=clGeneral.cYearNull)) //|| (dSidurEndHour < dStartLimitHour))
//                        {
//                            //Insert into dtErrors                    
//                            drNew = dtErrors.NewRow();
//                            InsertErrorRow(oSidur, ref drNew, "שעת סיום לסידור מיוחד שגוי", enErrors.errSidurHourStartEndNotValid.GetHashCode());
//                            drNew["shat_gmar"] = oSidur.sShatGmar;
//                            //drNew["hatchala_limit_hour"] = dStartLimitHour.ToString();
//                            drNew["gmar_limit_hour"] = dEndLimitHour.ToString();
//                            dtErrors.Rows.Add(drNew);
//                        }
//                    }
//                }
//                else
//                {//סידורים רגילים
//                    if ((!(string.IsNullOrEmpty(oSidur.sShatHatchala))) && (oParam.dSidurStartLimitHourParam1.Year!=clGeneral.cYearNull))
//                    {                       
//                        {
//                            dSidurStartHour = oSidur.dFullShatHatchala;//DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", oSidur.sShatHatchala));
//                            dStartLimitHour = oParam.dSidurStartLimitHourParam1;// DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", oParam.sSidurStartLimitHourParam1));
//                            //dEndLimitHour   = DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", oParam.sSidurEndLimitHourParam3));
//                            if ((dSidurStartHour < dStartLimitHour))// || (dSidurStartHour > dEndLimitHour))
//                            {
//                                drNew = dtErrors.NewRow();
//                                InsertErrorRow(oSidur, ref drNew, "שעת ההתחלה לסידור שגוי", enErrors.errSidurHourStartEndNotValid.GetHashCode());
//                                drNew["shat_hatchala"] = oSidur.sShatHatchala;
//                                drNew["hatchala_limit_hour"] = dStartLimitHour.ToString();
//                                dtErrors.Rows.Add(drNew);
//                            }
//                        }
//                    }
//                    if ((!(string.IsNullOrEmpty(oSidur.sShatGmar))) && ((oParam.dSidurEndLimitHourParam3.Year!=clGeneral.cYearNull)))
//                    {
//                        dSidurEndHour = oSidur.dFullShatGmar;//DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", oSidur.sShatGmar));
//                        //dStartLimitHour = DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", oParam.sSidurStartLimitHourParam1));
//                        dEndLimitHour = oParam.dSidurEndLimitHourParam3.AddDays(1);//DateTime.Parse(string.Concat(dCardDate.AddDays(1).ToShortDateString(), " ", oParam.sSidurEndLimitHourParam3));
//                        if (dSidurEndHour > dEndLimitHour) //(!((dSidurEndHour >= dStartLimitHour) && (dSidurEndHour <= dEndLimitHour)))
//                        {
//                            drNew = dtErrors.NewRow();
//                            InsertErrorRow(oSidur, ref drNew, "שעת הגמר לסידור שגוי", enErrors.errSidurHourStartEndNotValid.GetHashCode());
//                            drNew["shat_gmar"] = oSidur.sShatGmar;
//                            drNew["gmar_limit_hour"] = dEndLimitHour.ToString();
//                            dtErrors.Rows.Add(drNew);
//                        }
//                    }
//                }
//            }            
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsKodNesiaExists81(ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors,  DateTime dCardDate)
//        {           
//            try
//            {
//                //נבדוק מול התנועה אם סוג קוד נסיעה תקין
//                //בדיקה ברמת פעילות  

//                if (oPeilut.iMakatValid != 0) //שגיאה1-, -0תקין                
//                {
//                    drNew = dtErrors.NewRow();
//                    InsertErrorRow(oSidur, ref drNew, "קוד נסיעה לא קיים", enErrors.errKodNesiaNotExists.GetHashCode());
//                    InsertPeilutErrorRow(oPeilut, ref drNew);                    
//                    dtErrors.Rows.Add(drNew);
//                }
//            }
//            catch(Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsSimunNesiaValid27(DateTime dCardDate,ref DataTable dtErrors)
//        {            
//            string sLookUp, sBitulZmanNesia;
//            DataRow drNew;
//            bool bError=false;

//            try
//            {
//                //בדיקה ברמת כרטיס עבודה
//                sLookUp = GetLookUpKods("ctb_zmaney_nesiaa");
//                sBitulZmanNesia = oOvedYomAvodaDetails.sBitulZmanNesiot;
//                if (!(string.IsNullOrEmpty(sBitulZmanNesia)))
//                {   //נעלה שגיאה אם ערך לא קיים בטבלת פענוח
//                    if (sLookUp.IndexOf(sBitulZmanNesia) == -1)
//                    {
//                        bError = true;
//                    }
//                    else
//                    { //אם לא קיימים מאפיינים 51 ו61-
//                        //לא אמור להיות ערך בשדה 
//                        if ((!oMeafyeneyOved.Meafyen51Exists) && (!oMeafyeneyOved.Meafyen61Exists))
//                        {
//                            bError = true;
//                        }
//                    }
//                }
//                else
//                {
//                    bError = true;
//                }
//                if (bError)
//                {
//                    drNew = dtErrors.NewRow();
//                    drNew["check_num"] = enErrors.errSimunNesiaNotValid.GetHashCode();
//                    drNew["mispar_ishi"] = oOvedYomAvodaDetails.iMisparIshi;
//                    drNew["error_desc"] = "ערך ביטול נסיעות שגוי";
//                    drNew["bitul_zman_nesiot"] = sBitulZmanNesia;
//                    dtErrors.Rows.Add(drNew);
//                }
//            }           
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void IsOutMichsaValid118(ref clSidur oSidur, ref DataTable dtErrors)
//        {
//            //בדיקה ברמת סידור          
//            DataRow drNew;
//            try
//            {
//                //אם שדה מחוץ למכסה לא מקבל ערך תקין (0 או 1) - שגוי
//                if (((oSidur.sOutMichsa != "0") && (oSidur.sOutMichsa != "1")) || (string.IsNullOrEmpty(oSidur.sOutMichsa)))
//                {
//                    drNew = dtErrors.NewRow();
//                    InsertErrorRow(oSidur, ref drNew, "מחוץ למכסה שגוי", enErrors.errOutMichsaNotValid.GetHashCode());
//                    dtErrors.Rows.Add(drNew);
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void IsOutMichsaValid40(ref clSidur oSidur,ref DataTable dtErrors)
//        {
//            //בדיקה ברמת סידור          
//            DataRow drNew;
//            try
//            {
//                //אם שדה מחוץ למכסה תקין (0 או 1) והסידור הוא סידור העדרות - תצא שגיאה. יודעים אם סידור הוא העדרות לפי מאפיין בטבלת מאפיינים סידורים מיוחדים. רק עבור סידורים מיוחדים.
//                if (((oSidur.sOutMichsa == "0") || (oSidur.sOutMichsa == "1")) && (oSidur.sSectorAvoda == clGeneral.enSectorAvoda.Headrut.GetHashCode().ToString()))
//                {
//                    drNew = dtErrors.NewRow();
//                    InsertErrorRow(oSidur, ref drNew, "מחוץ למכסה בסדור שאסור ", enErrors.errOutMichsaInSidurHeadrutNotValid.GetHashCode());                        
//                    dtErrors.Rows.Add(drNew);
//                }               
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsElementTimeValid129(float fSidurTime, ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors)
//        {
//            //בדיקה ברמת פעילות
           
//            DataRow drNew;
//            long lMakatNesia=oPeilut.lMakatNesia;            
//            clKavim oKavim=new clKavim();
//            int iElementTime;
//            /*מפעילים את הרוטינה לזיהיו סוג מקט. אם חוזר שסוג המקט הוא אלמנט יש לבדוק שהמשך שלו אינו גדול מזמן הסידור. משך האלמנט רשום בדקות בספרות 4-6 שלו.
//            */            
//                //iSidurTime = int.Parse(oSidur.sShatGmar.Replace(":", "")) * 60 - int.Parse(oSidur.sShatHatchala.Replace(":", "")) * 60;           
//            if ((oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode()) && (oPeilut.sElementZviraZman != clGeneral.enSectorZviratZmanForElement.ElementZviratZman.GetHashCode().ToString()) && (oPeilut.sElementInMinutes == "1"))
//                {
//                    iElementTime = int.Parse(lMakatNesia.ToString().Substring(3, 3));
//                    if (iElementTime >(fSidurTime))
//                    {
//                        drNew = dtErrors.NewRow();
//                        InsertErrorRow(oSidur, ref drNew, "משך זמן האלמנט גדול ממשך זמן הסידור", enErrors.errElementTimeBiggerThanSidurTime.GetHashCode());
//                        InsertPeilutErrorRow(oPeilut, ref drNew);
//                        //drNew["Shat_Yetzia"] = oPeilut.sShatYetzia;//htEmployeeDetails["ShatYetzia"].ToString();
//                        //drNew["makat_nesia"] = oPeilut.lMakatNesia;//htEmployeeDetails["MakatNesia"].ToString();
//                        dtErrors.Rows.Add(drNew);
//                        //break;
//                    }
//                }              
//        }


//        private void IsPeilutShatYeziaValid113(ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors, DateTime dCardDate)
//        {    //בדיקה ברמת פעילות
//            /*שדה יציאה צריך להיות בטווח 00:00 עד 31:59. יש לבדוק את טבלת פרמטרים חיצוניים, פרמטרים 29 (שעת התחלה ראשונה מותרת לפעילות בסידור (שעת יציאה לפעילות)), - 30 (שעת התחלה אחרונה מורת לעילות בסידור (שעת יציאה לפעילות))*/
//            //clSidur oSidur;
//            //string sKey;
//            DataRow drNew;
//            //int iStartLimitHour, iEndLimitHour
//            string sPeilutShatYetzia;
//            DateTime dPeilutShatYetiza;
//            DateTime dStartHourForPeilut;
//            DateTime dEndHourForPeilut;
//            try
//            {
//            ////שעת התחלה ראשונה מותרת לפעילות בסידור - מאפיין 29
//            //iStartHourForPeilut = int.Parse(GetOneParam(29, dCardDate).Replace(char.Parse(":"), char.Parse("")));

//            ////שעת יציאה אחרונה מותרת לפעילות - מאפיין 30
//            //iEndHourForPeilut = int.Parse(GetOneParam(30, dCardDate).Replace(char.Parse(":"), char.Parse("")));

               
//               if (!(string.IsNullOrEmpty(oPeilut.sShatYetzia)))
//                    {
//                        sPeilutShatYetzia = oPeilut.sShatYetzia;                        
//                        //נבדוק אם השעה תקפה בפורמט של שעת אגד
//                        if (CheckEggedHourValid(oPeilut.sShatYetzia))
//                        {
//                            drNew = dtErrors.NewRow();
//                            InsertErrorRow(oSidur, ref drNew, "  לא בפורמט אגד - שעת יציאה שגויה", enErrors.errPeilutShatYetizaNotValid.GetHashCode());
//                            InsertPeilutErrorRow(oPeilut, ref drNew);
//                            //drNew["Shat_Yetzia"] = oPeilut.sShatYetzia;
//                            //drNew["makat_nesia"] = oPeilut.lMakatNesia;
//                            dtErrors.Rows.Add(drNew);
//                        }
//                        //נבדוק אם שעה תקפה מול פרמטרים של שעת התחלה ושעת סיום
//                        dPeilutShatYetiza = DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", sPeilutShatYetzia));
//                        if (oParam.dStartHourForPeilut.Year!=clGeneral.cYearNull)
//                        {
//                            dStartHourForPeilut = oParam.dStartHourForPeilut;//DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", oParam.sStartHourForPeilut));
//                            if ((dPeilutShatYetiza < dStartHourForPeilut))
//                            {
//                                drNew = dtErrors.NewRow();
//                                InsertErrorRow(oSidur, ref drNew, "שעת יציאה שגויה", enErrors.errPeilutShatYetizaNotValid.GetHashCode());
//                                InsertPeilutErrorRow(oPeilut, ref drNew);
//                                //drNew["Shat_Yetzia"] = oPeilut.sShatYetzia;
//                                //drNew["makat_nesia"] = oPeilut.lMakatNesia;
//                                dtErrors.Rows.Add(drNew);
//                            }
//                        }
//                        if (oParam.dEndHourForPeilut.Year!=clGeneral.cYearNull)
//                        {
//                            dEndHourForPeilut = oParam.dEndHourForPeilut.AddDays(1);//DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", oParam.sEndHourForPeilut)).AddDays(1);
//                            if (oPeilut.dFullShatYetzia > dEndHourForPeilut)
//                            {
//                                drNew = dtErrors.NewRow();
//                                InsertErrorRow(oSidur, ref drNew, "שעת יציאה שגויה", enErrors.errPeilutShatYetizaNotValid.GetHashCode());
//                                InsertPeilutErrorRow(oPeilut, ref drNew);
//                                dtErrors.Rows.Add(drNew);
//                            }
//                        }
                        
//                    }               
//            }
//            catch(Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void IsHamaraValid42(ref clSidur oSidur, ref DataTable dtErrors)
//        {
//            try
//            {
//                if (!((oSidur.sHamaratShabat == "0") || (oSidur.sHamaratShabat == "1")))
//                {
//                    drNew = dtErrors.NewRow();
//                    InsertErrorRow(oSidur, ref drNew, "ערך המרה שגוי", enErrors.errHamaratShabatNotValid.GetHashCode());
//                    drNew["hamarat_shabat"] = oSidur.sHamaratShabat;
//                    dtErrors.Rows.Add(drNew);
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void IsHamaratShabatValid39(DataRow[] drSugSidur,ref clSidur oSidur,ref DataTable dtErrors)
//        {            
//            //בדיקה ברמת סידור            
//            DataRow drNew;
//            try
//            {
//                if (!String.IsNullOrEmpty(oSidur.sHamaratShabat))
//                {
//                    if (((oSidur.sHamaratShabat == "0") || (oSidur.sHamaratShabat == "1")))
//                    {
//                        if (((oSidur.bSidurMyuhad) && (oSidur.iMisparSidurMyuhad > 0)) || (!oSidur.bSidurMyuhad))
//                        {
//                            //sHamaratShabat-אם אין זכאות להמרה,לא אמור להיות ערך ב-
//                            //נבדוק רק לסידורים מיוחדים
//                            if ((oSidur.bSidurMyuhad) && (oSidur.iMisparSidurMyuhad > 0))
//                            {
//                                if ((!oSidur.bZakaiLehamaraExists) && (oSidur.sHamaratShabat != string.Empty))
//                                {
//                                    drNew = dtErrors.NewRow();
//                                    InsertErrorRow(oSidur, ref drNew, "המרה אסורה לסידור", enErrors.errHamaraNotValid.GetHashCode());
//                                    drNew["hamarat_shabat"] = oSidur.sHamaratShabat;
//                                    dtErrors.Rows.Add(drNew);
//                                }
//                            }
//                            else//סידורים רגילים
//                            {
//                                if (drSugSidur.Length > 0)
//                                {   //עבור סידורים רגילים, רק בסידורי נהגות וניהול תנועה מותרת המרה.
//                                    if ((drSugSidur[0]["sector_avoda"].ToString() != clGeneral.enSectorAvoda.Nihul.GetHashCode().ToString()) && (drSugSidur[0]["sector_avoda"].ToString() != clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString()) && (oSidur.sHamaratShabat != string.Empty))
//                                    {
//                                        drNew = dtErrors.NewRow();
//                                        InsertErrorRow(oSidur, ref drNew, "המרה אסורה לסידור", enErrors.errHamaraNotValid.GetHashCode());
//                                        drNew["hamarat_shabat"] = oSidur.sHamaratShabat;
//                                        dtErrors.Rows.Add(drNew);
//                                    }
//                                }
//                            }
//                        }
//                    }
//                }
//            }          
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsPitzulAndNotZakai25(ref clSidur oSidur,ref DataTable dtErrors)
//        {
//            //בדיקה ברמת סידור           
//            DataRow drNew;           
//            try
//            {    //אם הערך בשדה פיצול הפסקה שווה 2 וגם מעמד שווה חבר זו שגיאה
//                //נבדוק אם העובד הוא חבר
//                //sKodMaamd = dtOvedCardDetails.Rows[0]["Kod_Maamd"].ToString();
//                if (!string.IsNullOrEmpty(oSidur.sPitzulHafsaka))
//                {
//                    if ((oOvedYomAvodaDetails.sKodHaver == "1") && (oSidur.sPitzulHafsaka == "2")) //קוד מעמד שמתחיל ב- 1 - חבר
//                    {
//                        drNew = dtErrors.NewRow();
//                        InsertErrorRow(oSidur, ref drNew, "פצול מיוחד ולא זכאי", enErrors.errPitzulMuchadValueNotValid.GetHashCode());
//                        dtErrors.Rows.Add(drNew);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsPeilutInSidurValid84(ref clSidur oSidur,ref clPeilut oPeilut, ref DataTable dtErrors)
//        {
//            //בדיקה ברמת סידור          
//            int iPeilutMisparSidur;
//            DataRow drNew;
            
//            try
//            {
//                //סידור שאסור לדווח בו פעילויות. נבדק רק מול סידורים מיוחדים.                                   
//               //
//                if ((oSidur.bSidurMyuhad) && (oSidur.bNoPeilotKodExists))
//                    {
//                        //נבדוק אם קיימות לסידור פעילויות
//                        //foreach (DictionaryEntry dePeilut in oSidur.htPeilut)
//                        //{
//                            iPeilutMisparSidur = oPeilut.iPeilutMisparSidur;
//                            //sMakatNesia = htEmployeeDetails["MakatNesia"].ToString();
//                            if (iPeilutMisparSidur > 0)
//                            {
//                                drNew = dtErrors.NewRow();
//                                InsertErrorRow(oSidur, ref drNew, "פעילות אסורה בסדור תפקיד", enErrors.errPeilutForSidurNonValid.GetHashCode());
//                                //drNew["shat_yetzia"] = oPeilut.sShatYetzia;
//                                InsertPeilutErrorRow(oPeilut, ref drNew);
//                                dtErrors.Rows.Add(drNew);
                               
//                            }
//                       // }                        
//                    }
//                }                
//            //}
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsCharigaValid32(ref clSidur oSidur, ref DataTable dtErrors)
//        {
//            bool bError=false;
//            string sLookUp="";
//            //בדיקה ברמת סידור
//            try
//            {
//                //אם שדה חריגה תקין  - לבדוק תקינות לפי ערכים בטבלה CTB_DIVUCH_HARIGA_MESHAOT.    וסידור אינו זכאי לחריגה (סידור זכאי חריגה אם יש לו מאפיין 35 (זכאי לחריגה) במאפייני סידורים מיוחדים                                      ושעת גמר קטנה מ- 28
//                if (string.IsNullOrEmpty(oSidur.sChariga))
//                {
//                    bError=true;
//                } 
//                else
//                {
//                    sLookUp = GetLookUpKods("ctb_divuch_hariga_meshaot");                
//                    //אם ערך חריגה לא נמצא בטבלה
//                    if (sLookUp.IndexOf(oSidur.sChariga) == -1)
//                    {
//                        bError = true;
//                    }                        
//                }
//                if (bError)
//                {
//                    drNew = dtErrors.NewRow();
//                    InsertErrorRow(oSidur, ref drNew, "חריגה שגוי", enErrors.errCharigaValueNotValid.GetHashCode());
//                    dtErrors.Rows.Add(drNew);
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsZakaiLeChariga34(ref clSidur oSidur,ref DataTable dtErrors)
//        {
//            //בדיקה ברמת סידור
//            //clSidur oSidur;
//            string sLookUp;
//            int iShatGmar;
//            DataRow drNew;

//            try
//            {   //אם שדה חריגה תקין  - לבדוק תקינות לפי ערכים בטבלה CTB_DIVUCH_HARIGA_MESHAOT.    וסידור אינו זכאי לחריגה (סידור זכאי חריגה אם יש לו מאפיין 35 (זכאי לחריגה) במאפייני סידורים מיוחדים                                      ושעת גמר קטנה מ- 28                
//                if (oSidur.bSidurMyuhad)
//                {//סידורים מיוחדים
//                    if (!string.IsNullOrEmpty(oSidur.sChariga))
//                    {
//                        if (!(string.IsNullOrEmpty(oSidur.sShatGmar)))
//                        {
//                            sLookUp = GetLookUpKods("ctb_divuch_hariga_meshaot");
//                            iShatGmar = int.Parse(oSidur.sShatGmar.Remove(2, 1).Substring(0, 2));
//                            //אם ערך חריגה תקין, אבל אין זכאות לחריגה נעלה שגיאה
//                            if ((sLookUp.IndexOf(oSidur.sChariga) != -1) && (!oSidur.bZakaiLeCharigaExists) && (iShatGmar < 28))  //לא קיים מאפיין 35
//                            {
//                                drNew = dtErrors.NewRow();
//                                InsertErrorRow(oSidur, ref drNew, "סידור אינו זכאי לחריגה", enErrors.errZakaiLeCharigaValueNotValid.GetHashCode());
//                                dtErrors.Rows.Add(drNew);
//                            }
//                        }
//                    }
//                }
//                else
//                {//סדורים רגילים
//                    if (!string.IsNullOrEmpty(oSidur.sChariga))
//                    {
//                        drNew = dtErrors.NewRow();
//                        InsertErrorRow(oSidur, ref drNew, "סידור אינו זכאי לחריגה", enErrors.errZakaiLeCharigaValueNotValid.GetHashCode());
//                        dtErrors.Rows.Add(drNew);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsHashlamaForSidurValid48(float fSidurTime,ref clSidur oSidur,ref DataTable dtErrors)
//        {
//            //בדיקה ברמת סידור
            
//            DataRow drNew;
          
//            try
//            {   //סידור מקבל קוד השלמת שעות למרות שלא צריך עפ"י הגדרת הזמן. למשל : השלמה לשעתיים למרות שהסידור גדול משעתיים. קודי ההשלמה יהיו בהתאם לקוד, לדוגמא:קוד 1 זה השלמה לשעה, קוד 2 השלמה לשעתיים וכן'                            
               
//                    if (!string.IsNullOrEmpty(oSidur.sHashlama))
//                    {
//                        if ((int.Parse(oSidur.sHashlama)) > 0)
//                        {
//                            if ((!(string.IsNullOrEmpty(oSidur.sShatGmar))) && (oSidur.dFullShatHatchala.Year > clGeneral.cYearNull))
//                            {
//                                if (fSidurTime / 60 > int.Parse(oSidur.sHashlama))
//                                {
//                                    drNew = dtErrors.NewRow();
//                                    InsertErrorRow(oSidur, ref drNew, "ערך השלמה לסידור שגוי", enErrors.errHashlamaForSidurNotValid.GetHashCode());
//                                    drNew["Hashlama"] = oSidur.sHashlama;
//                                    dtErrors.Rows.Add(drNew);
//                                }
//                            }
//                        }
//                    }
//                //}
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }


//        private void IsShatPeilutNotValid121(DateTime dCardDate, ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors)
//        {
//            //בדיקה ברמת פעילות
//            //clSidur oSidur;
//            //string sKey;
//            DateTime dSidurShatGmar = new DateTime(); 
//            DateTime dSidurShatHatchala = new DateTime();
//            DateTime dShatYetziaPeilut;
//            long lMakatNesia = oPeilut.lMakatNesia;
//            DataRow drNew;
//            clKavim oKavim = new clKavim();

//            try
//            {   //12עקב שינוי שעת התחלה של סידור יכול להיווצר מצב בו יש פעילות המתחילה לפני שעת התחלת הסידור החדשה. אין לבצע את הבדיקה אם הפעילות היא אלמנט (מתחיל ב- 7) והיא לידיעה. פעילות היא לידיעה לפי פרמטר 3 (פעולה / ידיעה בלבד) בטבלת מאפייני אלמנטים121-
//                //122עקב שינוי שעת סיום של סידור יכול להיווצר מצב בו יש פעילות המתחילה אחרי שעת סיום הסידור החדשה. אין לבצע את הבדיקה אם הפעילות היא אלמנט (מתחיל ב- 7) והיא לידיעה.  פעילות היא לידיעה לפי פרמטר 3 (פעולה / ידיעה בלבד) בטבלת מאפייני אלמנטים122-

//                if (oSidur.dFullShatHatchala.Year > clGeneral.cYearNull)
//                    {
//                        dSidurShatHatchala = DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", oSidur.sShatHatchala));
//                    }
//                    if (!(string.IsNullOrEmpty(oSidur.sShatGmar)))
//                    {
//                        dSidurShatGmar = DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", oSidur.sShatGmar)); 
//                    }
                    
//                    if (!(string.IsNullOrEmpty(oPeilut.sShatYetzia)))
//                    {
//                        dShatYetziaPeilut = DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", oPeilut.sShatYetzia)); 

//                        //אם אלמנט ולידיעה לא נבצע את הבדיקה
                      
//                        if ((!((oPeilut.iMakatType == (long)clKavim.enMakatType.mElement.GetHashCode()) && (oPeilut.iElementLeYedia == 2))) && (lMakatNesia > 0))
//                        {
//                            if (oSidur.dFullShatHatchala.Year > clGeneral.cYearNull)
//                            {//בדיקה 121
//                                if (dShatYetziaPeilut < dSidurShatHatchala)
//                                {
//                                    drNew = dtErrors.NewRow();
//                                    InsertErrorRow(oSidur, ref drNew, "שעת פעילות נמוכה משעת התחלת הסידור", enErrors.errShatPeilutSmallerThanShatHatchalaSidur.GetHashCode());
//                                    //drNew["shat_yetzia"] = iShatYetziaPeilut;
//                                    InsertPeilutErrorRow(oPeilut, ref drNew);
//                                    dtErrors.Rows.Add(drNew);
//                                }
//                            }
//                            if (!(string.IsNullOrEmpty(oSidur.sShatGmar)))
//                            {//בדיקה 122
//                                if (dShatYetziaPeilut > dSidurShatGmar)
//                                {
//                                    drNew = dtErrors.NewRow();
//                                    InsertErrorRow(oSidur, ref drNew, "שעת פעילות גדולה משעת סיום הסידור", enErrors.errShatPeilutBiggerThanShatGmarSidur.GetHashCode());
//                                    //drNew["shat_yetzia"] = iShatYetziaPeilut;
//                                    InsertPeilutErrorRow(oPeilut, ref drNew);
//                                    dtErrors.Rows.Add(drNew);
//                                }
//                            }
//                        }
//                    }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }


//        private void IsSidurimHoursNotValid16(int iSidur,ref  clSidur oSidur, ref DataTable dtErrors)
//        {
//            //בדיקה ברמת סידור           
//            DataRow drNew;
//            clSidur oPrevSidur = (clSidur)htEmployeeDetails[iSidur - 1];
//            string sShatGmarPrev = oPrevSidur.sShatGmar;
//            int iPrevLoLetashlum = oPrevSidur.iLoLetashlum;
//            try
//            {
//                if ((!(string.IsNullOrEmpty(sShatGmarPrev))) && (!string.IsNullOrEmpty(oSidur.sShatHatchala)))
//                {
//                    DateTime dPrevTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(sShatGmarPrev.Substring(0, 2)), int.Parse(sShatGmarPrev.Substring(3, 2)),0);
//                    DateTime dCurrTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(oSidur.sShatHatchala.Substring(0, 2)), int.Parse(oSidur.sShatHatchala.Substring(3, 2)),0);
//                    //אם גם הסידור הקודם וגם הסידור הנוכחי הם לתשלום, נבצע את הבדיקה
//                    if ((oSidur.iLoLetashlum == 0) && (iPrevLoLetashlum ==0))
//                    {
//                        if (dCurrTime < dPrevTime)
//                        {
//                            drNew = dtErrors.NewRow();
//                            InsertErrorRow(oSidur, ref drNew, "קיימת חפיפה בשעות סידורים", enErrors.errSidurimHoursNotValid.GetHashCode());
//                            dtErrors.Rows.Add(drNew);
//                        }
//                    }
//                }
//            }            
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsSidurExistsInShlila35(int iMisparIshi,DateTime dCardDate, ref DataTable dtErrors)
//        {           
//            DataRow drNew;
//            //בדיקה ברמת יום עבודה
//            try
//            {
//                //אסור שיהיה לעובד סידור כלשהו בזמן חופשת שלילה.
//                //קוד מאפיין שלילה 72
//                if ((oMeafyeneyOved.Meafyen72Exists) && (htEmployeeDetails.Count > 0))
//                {
//                    drNew = dtErrors.NewRow();
//                    drNew["check_num"] = enErrors.errSidurExistsInShlila.GetHashCode();
//                    drNew["mispar_ishi"] = iMisparIshi;
//                    drNew["taarich"] = dCardDate.ToShortDateString();
//                    drNew["error_desc"] = "קיימים סידורים בזמן חופשת שלילה"; 
//                    //InsertErrorRow( (clSidur)htEmployeeDetails[0], ref drNew, "קיימים סידורים בזמן חופשת שלילה", enErrors.errSidurExistsInShlila.GetHashCode());                   
//                    dtErrors.Rows.Add(drNew);
//                }
//            }
//            catch(Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void IsVisaInSidurRagil58(ref  clSidur oSidur, ref DataTable dtErrors)
//        {
//            //בדיקה ברמת סידור           
//            DataRow drNew;

//            try
//            { //בסידור שאינו ויזה אסור שיהיה סימון בשדה קוד יום ויזה. מזהים סידור ויזה לפי  מאפיין (45) בטבלת סידורים מיוחדים. סמון ויזה יכול להגיע מהסדרן.
//                if ((!oSidur.bSidurVisaKodExists) && (!String.IsNullOrEmpty(oSidur.sVisa)))
//                {
//                    drNew = dtErrors.NewRow();
//                    InsertErrorRow(oSidur, ref drNew, "קיים סימון ויזה בסידור רגיל", enErrors.errSidurVisaNotValid.GetHashCode());
//                    dtErrors.Rows.Add(drNew);
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void IsSidurEilatValid55(int iSidur, bool bFirstSidur,ref clPeilut oPeilut, ref  clSidur oSidur, ref DataTable dtErrors)
//        {
//            //בדיקה ברמת סידור           
//            DataRow drNew;
//            clSidur oPrevSidur = (clSidur)htEmployeeDetails[iSidur - 1];
//            string sPrevShatHatchala = oPrevSidur.sShatHatchala;
//            bool bPrevSidurEilat = oPrevSidur.bSidurEilat;

//            try
//            { //צריך להיות שעה הפרש בין שני סידורי אילת. מזהים סידור אילת אם יש לו פעילות אילת. מזהים פעילות אילת לפי שדה שחוזר מהפרוצדורה GetKavDetails.
//                if ((oSidur.bSidurEilat) && (bPrevSidurEilat) && (!(bFirstSidur)) && (oSidur.dFullShatHatchala.Year > clGeneral.cYearNull) && (!(string.IsNullOrEmpty(sPrevShatHatchala))))
//                {
//                    DateTime dPrevTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(sPrevShatHatchala.Substring(0, 2)), int.Parse(sPrevShatHatchala.Substring(3, 2)), 0);
//                    DateTime dCurrTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, int.Parse(oSidur.sShatHatchala.Substring(0, 2)), int.Parse(oSidur.sShatHatchala.Substring(3, 2)), 0);
//                    //ההפרש בין שני סידורי אילת קטן משעה
//                    if (dPrevTime.AddHours(1)<dCurrTime)
//                    {
//                        drNew = dtErrors.NewRow();
//                        InsertErrorRow(oSidur, ref drNew, "סידור אילת ללא הפסקה כנדרש לפני הסידור ", enErrors.errSidurEilatNotValid.GetHashCode());
//                        InsertPeilutErrorRow(oPeilut, ref drNew);
//                        dtErrors.Rows.Add(drNew);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsOtoNoValid69(ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors)
//        {
//            clKavim oKavim = new clKavim();
//            DataRow drNew;

//            //בדיקה ברמת פעילות
//            try
//            {
//                //בודקים אם הפעילות דורשת מספר רכב ואם הוא קיים וחוקי (מול מש"ר). פעילות דורשת מספר רכב אם מרוטינת זיהוי מקט חזר פרמטר שונה מאלמנט. אם חזר מהרוטינה אלנמט יש לבדוק אם דורש מספר רכב. תהיה טבלה של מספר פעילות המתחילים ב- 7 ולכל רשומה יהיה מאפיין אם הוא דורש מספר רכב. בטבלת מאפייני אלמנטים (11 - חובה מספר רכב)
//                //בדיקת מספר רכב מול מש"ר
//                if (oPeilut.lOtoNo > 0)
//                {
//                    if (!(oKavim.IsBusNumberValid(oPeilut.lOtoNo)))
//                    {
//                        drNew = dtErrors.NewRow();
//                        InsertErrorRow(oSidur, ref drNew, "מספר רכב לא תקין/חסר מספר רכב", enErrors.errOtoNoNotExists.GetHashCode());
//                        InsertPeilutErrorRow(oPeilut, ref drNew);
//                        drNew["oto_no"] = oPeilut.lOtoNo;
//                        //drNew["makat_nesia"] = oPeilut.lMakatNesia;
//                        dtErrors.Rows.Add(drNew);
//                    }
//                }
//                else //חסר מספר רכב
//                {
//                    //שגיאה 69
//                    //בודקים אם הפעילות דורשת מספר רכב ואם הוא קיים וחוקי (מול מש"ר). פעילות דורשת מספר רכב אם מרוטינת זיהוי מקט חזר פרמטר שונה מאלמנט. אם חזר מהרוטינה אלנמט יש לבדוק אם דורש מספר רכב. תהיה טבלה של מספר פעילות המתחילים ב- 7 ולכל רשומה יהיה מאפיין אם הוא דורש מספר רכב. בטבלת מאפייני אלמנטים (11 - חובה מספר רכב)
//                    if ((!(oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode())) || ((oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode()) && (oPeilut.bBusNumberMustExists)))
//                    {
//                        drNew = dtErrors.NewRow();
//                        InsertErrorRow(oSidur, ref drNew, "מספר רכב לא תקין/חסר מספר רכב", enErrors.errOtoNoNotExists.GetHashCode());
//                        InsertPeilutErrorRow(oPeilut, ref drNew);
//                        drNew["oto_no"] = oPeilut.lOtoNo;
//                        // drNew["makat_nesia"] = oPeilut.lMakatNesia;
//                        dtErrors.Rows.Add(drNew);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void IsMisparSidurInEilatExists139(ref clPeilut oPeilut,  ref  clSidur oSidur, ref DataTable dtErrors)
//        {
//            //בדיקה ברמת סידור           
//            DataRow drNew;

//            try
//            { //בסידור המכיל נסיעות לאילת חייב שיהיה שדה המכיל את המספר הסידורי של האוטובוס, אם הוא חסר זו שגיאה.
//                if ((oPeilut.bPeilutEilat) && (oPeilut.lMisparSiduriOto == 0))
//                {                    
//                    drNew = dtErrors.NewRow();
//                    InsertErrorRow(oSidur, ref drNew, "נסיעת אילת ללא רכב סדורי", enErrors.errMisparSiduriOtoInSidurEilatNotExists.GetHashCode());
//                    InsertPeilutErrorRow(oPeilut, ref drNew);
//                    dtErrors.Rows.Add(drNew);
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void IsMisparSidurEilatInRegularSidurExists140(ref clPeilut oPeilut, ref  clSidur oSidur, ref DataTable dtErrors)
//        {
//            //בדיקה ברמת פעילות           
//            DataRow drNew;

//            try
//            { //בסידור המכיל נסיעות לאילת חייב שיהיה שדה המכיל את המספר הסידורי של האוטובוס. שדה זה אסור בסידור שאינו כולל נסיעת אילת וזו השגיאה. 
//                if (!(oPeilut.bPeilutEilat) && (oPeilut.lMisparSiduriOto > 0))
//                {
//                    drNew = dtErrors.NewRow();
//                    InsertErrorRow(oSidur, ref drNew, "רכב סידורי ללא נסיעת אילת", enErrors.errMisparSiduriOtoNotInSidurEilat.GetHashCode());
//                    InsertPeilutErrorRow(oPeilut, ref drNew);
//                    dtErrors.Rows.Add(drNew);
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void IsDuplicateShatYetiza103(int iMisparIshi, DateTime dCardDate,ref DataTable dtErrors)
//        {
//            //בדיקה ברמת כרטיס עבודה
//            //שני מקטים לאותו עובד עם אותה שעת יציאה. הבדיקה תתבצע תמיד לכל הפעילויות שאינן אלמנט (זיהוי סוג הפעילות יתבצע ע"פ "רוטינת זיהוי סוג מקט") לפעילויות שהן אלמנט, יש לבדוק בטבלת מאפיינים לאלמנטים אם אין לו מאפיין 9 ("להתעלם בבדיקת חפיפה בין נסיעות"). רק לאלמנט שאין לו את המאפיין הזה יש לעשות את הבדיקה. 
//            //נבדוק שלא קיימות שתי פעילויות עם אותה שעת יציאה. הבדיקה נעשית על כל 
//            //הפעילויות של כרטיס העבודה 
//            DataRow drNew;
//            try
//            {
//                if (IsDuplicateShatYetiza(iMisparIshi, dCardDate))
//                {
//                    drNew = dtErrors.NewRow();
//                    drNew["check_num"] = enErrors.errDuplicateShatYetiza.GetHashCode();
//                    drNew["mispar_ishi"] = iMisparIshi;
//                    drNew["taarich"] = dCardDate.ToShortDateString();
//                    drNew["error_desc"] = "שעת יציאה זהה בשתי פעילויות לאותו עובד";                   
//                    dtErrors.Rows.Add(drNew);
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void IsZakaiLina31(ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors)
//        {
//            DataRow drNew;
//            //בדיקה ברמת פעילות
//            try
//            {
//                //אם  יש ערך בשדה לינה ובסידור האחרון יש פעילות מסוג אלמנט (לפי רוטינת זיהוי מקט) ולאלמנט יש מאפיין המתנה (15) - יוצא לשגיאה
//                if (!string.IsNullOrEmpty(oOvedYomAvodaDetails.sLina))
//                {
//                    if ((oSidur.iMisparSidur == iLastMisaprSidur) && (int.Parse(oOvedYomAvodaDetails.sLina) > 0) && (oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode()) && (oPeilut.bElementHamtanaExists ))
//                    {
//                        drNew = dtErrors.NewRow();
//                        InsertErrorRow(oSidur, ref drNew, "לא זכאי ללינה", enErrors.errLoZakaiLLina.GetHashCode());
//                        InsertPeilutErrorRow(oPeilut, ref drNew);
//                        //drNew["makat_nesia"] = oPeilut.lMakatNesia;
//                        dtErrors.Rows.Add(drNew);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void IsOtoNoExists68(DataRow[] drSugSidur,  ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors)
//        {
//            //בדיקה ברמת פעילות
//            try
//            {
//                //אסור לדווח רכב בסידור שיש לו מאפיין 43 (אסור לדווח מספר רכב). הבדיקה רלוונטית גם לסידורים מיוחדים וגם לסידורים רגילים.                
//                if (oPeilut.lOtoNo > 0)
//                {
//                    //TB_Sidurim_Meyuchadim נבדוק אם קיים מאפיין 43: לסידורים מיוחדים נבדוק בטבלת 
//                    //לסידורים רגילים נבדוק את המאפיין מהתנועה
//                    if (oSidur.bSidurMyuhad)
//                    {
//                        if ((oSidur.bNoOtoNoExists) && (oSidur.sNoOtoNo=="1"))
//                        {
//                            drNew = dtErrors.NewRow();
//                            InsertErrorRow(oSidur, ref drNew, "מספר רכב בסידור תפקיד", enErrors.errOtoNoExists.GetHashCode());
//                            drNew["oto_no"] = oPeilut.lOtoNo;
//                            InsertPeilutErrorRow(oPeilut, ref drNew);
//                            //drNew["makat_nesia"] = oPeilut.lMakatNesia;
//                            dtErrors.Rows.Add(drNew);
//                        }
//                    }
//                    else //סידור רגיל
//                    {
//                        if (drSugSidur.Length > 0)
//                        {
//                            if ((!String.IsNullOrEmpty(drSugSidur[0]["asur_ledaveach_mispar_rechev"].ToString())) && (drSugSidur[0]["asur_ledaveach_mispar_rechev"].ToString() =="1"))
//                            {
//                                drNew = dtErrors.NewRow();
//                                InsertErrorRow(oSidur, ref drNew, "מספר רכב בסידור תפקיד", enErrors.errOtoNoExists.GetHashCode());
//                                drNew["oto_no"] = oPeilut.lOtoNo;
//                                //drNew["makat_nesia"] = oPeilut.lMakatNesia;
//                                InsertPeilutErrorRow(oPeilut, ref drNew);
//                                dtErrors.Rows.Add(drNew);
//                            }
//                        }
//                    }
//                }                
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsNesiaTimeNotValid91(float fSidurTime, DateTime dCardDate, ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors)
//        {
//            int iElementTime = 0;            
//            DataRow drNew;

//            //בדיקה ברמת פעילות
//            try
//            {
//                //ם פעילות היא אלמנט (לפי רוטינת זיהוי מקט) והאלמנט הוא נהגות (לפי ערך 1 (נהגות) במאפיין 14 (סקטור הצבירה של זמן האלמנט) בטבלת מאפייני אלמנטים) והאלמנט הוא בדקות (לפי ערך 1 (דקות) במאפיין 4 (ערך האלמנט) בטבלת מאפייני אלמנטים) אז לוקחים את זמן האלמנט (פוזיציות 4-6) מכפילים באחוזים (פרמטר 42 (פקטור נסיעות שירות בין גמר לתכנון) בטבלת פרמטרים כלליים)). אם התוצאה גדולה מזמן הסידור (גמר מינוס התחלה) אז יוצאת שגיאה.
//                // אם הפעילות היא אלמנט
//                //,והאלמנט הוא נהגות ובדקות
//                //אז נבדוק שזמן הסידור לא חורג
//                if ((oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode()) && (oPeilut.sElementZviraZman == clGeneral.enSectorZviratZmanForElement.ElementZviratZman.GetHashCode().ToString()) && (oPeilut.sElementInMinutes == "1"))
//                {
//                    if (oPeilut.lMakatNesia > 0)
//                    {
//                        //נקח את זמן האלמנט פוזיציות 4-6 באלמנט
//                        iElementTime = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));
//                        //נכפיל ב-פקטור נסיעות שירות בין גמר לתכנון, את הפקטור נקח מטבלת פרמטרים, פרמטר מספר 42
//                        //fFactor = float.Parse(GetOneParam(42, dCardDate));
//                        //אם זמן האלמנט * פקטור נסיעות הסידור גדול מזמן הסידור נעלה שגיאה
//                        if ((oParam.fFactor * iElementTime) > (fSidurTime))
//                        {
//                            drNew = dtErrors.NewRow();
//                            InsertErrorRow(oSidur, ref drNew, "זמן נסיעה חריג", enErrors.errNesiaTimeNotValid.GetHashCode());
//                            drNew["nesia_time"] = oParam.fFactor * iElementTime;
//                            drNew["sidur_time"] = fSidurTime;
//                            InsertPeilutErrorRow(oPeilut, ref drNew);
//                            //drNew["makat_nesia"] = oPeilut.lMakatNesia;
//                            dtErrors.Rows.Add(drNew);
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsTeoodatNesiaValid52(ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors)
//        {
//            DataRow drNew;
//            //בדיקה ברמת פעילות

//            try
//            {
//                //רק לסידור ויזה עפ"י סימון בטבלת סידורים מיוחדים מותר שתהיה תעודת נסיעה (שדה מספר ויזה). יודעים שסידור מיוחד הוא ויזה לפי מאפיין (45) בטבלת סידורים מיוחדים.
//                if ((!(oSidur.bSidurMyuhad && (oSidur.bSidurVisaKodExists))) && (oPeilut.lMisparVisa > 0))
//                {                                       
//                    drNew = dtErrors.NewRow();
//                    InsertErrorRow(oSidur, ref drNew, "תעודת נסיעה לא בסדור ויזה", enErrors.errTeoodatNesiaNotInVisa.GetHashCode());
//                    drNew["mispar_visa"] = oPeilut.lMisparVisa;
//                    //drNew["makat_nesia"] = oPeilut.lMakatNesia;
//                    InsertPeilutErrorRow(oPeilut, ref drNew);
//                    dtErrors.Rows.Add(drNew);                                         
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsHashlamatHazmanaValid49(float fSidurTime, ref  clSidur oSidur, ref DataTable dtErrors)
//        {
//            //בדיקה ברמת סידור           
//            DataRow drNew;
//            int iZmanMinimum = 0;
//            try
//            {   //לסידורים להם אסור לדווח השלמה - יטופל דרך טבלת סידורים מיוחדים. ניתן לדווח השלמה אם לסידור מיוחד קיים מאפיין  40 (זכאי להשלמה עבור הסידור) בטבלת מאפיינים סידורים מיוחדים. עבור סידור שאינו מיוחד תמיד זכאי להשלמה כל עוד זמן הסידור קטן nמהערכים בפרמטרים 101-103 (זמן מינימום לסידור חול להשלמה, זמן מינימום לסידור שישי/ע.ח להשלמה, זמן מינימום לסידור שבתון להשלמה).
//             if (!string.IsNullOrEmpty(oSidur.sHashlama))
//             {
//                if ((oSidur.bSidurMyuhad) && (oSidur.iMisparSidurMyuhad>0))
//                {
//                    if ((oSidur.bHashlamaExists) && (!String.IsNullOrEmpty(oSidur.sHashlama)))
//                    {
//                        if (int.Parse(oSidur.sHashlama) > 0)
//                        {
//                            drNew = dtErrors.NewRow();
//                            InsertErrorRow(oSidur, ref drNew, "השלמת הזמנה אסורה", enErrors.errHahlamatHazmanaNotValid.GetHashCode());
//                            dtErrors.Rows.Add(drNew);
//                        }
//                    }
//                }
//                else //סידור רגיל
//                {
//                    if ((!oSidur.bSidurMyuhad) && (int.Parse(oSidur.sHashlama) > 0))
//                    {
//                        if ((oSidur.sShabaton == "1") || (oSidur.sSidurDay == clGeneral.enDay.Shabat.GetHashCode().ToString()))
//                        {
//                            iZmanMinimum = oParam.iHashlamaShabat;
//                        }
//                        else
//                        {
//                            if ((oSidur.sErevShishiChag == "1") || (oSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()))
//                            {
//                                iZmanMinimum = oParam.iHashlamaShisi;
//                            }
//                            else
//                            {
//                                iZmanMinimum = oParam.iHashlamaYomRagil;
//                            }
//                        }
//                        if (fSidurTime > iZmanMinimum)
//                        {
//                            drNew = dtErrors.NewRow();
//                            InsertErrorRow(oSidur, ref drNew, "השלמת הזמנה אסורה", enErrors.errHahlamatHazmanaNotValid.GetHashCode());
//                            dtErrors.Rows.Add(drNew);
//                        }
//                    }
//                }      
//             }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsTotalHashlamotInCardValid(int iTotalHashlamotForSidur, ref  clSidur oSidur, ref DataTable dtErrors)
//        {
//            //בדיקה בכרטיס עבודה
//            int iZmanMaximum = 0;
//            try
//            {
//                //יש מספר מקסימלי של השלמות המותר ליום, תלוי בסוג היום. בודקים את סוג היום ולפי סוג היום בודקים בטבלת פרמטרים חיצוניים מה מקסימום ההשלמות המותר ליום. 108 (מכסימום השלמות ביום חול), 109 (מכסימום השלמות בשישי/ע.ח), 110 (מכסימום השלמות בשבתון). אם בודקים יום אל מול טבלת סוגי ימים מיוחדים והוא אינו מוגדר כשבתון או ערב שבת/חג  - יום זה הוא יום חול.
//                if ((oSidur.sShabaton == "1") || (oSidur.sSidurDay == clGeneral.enDay.Shabat.GetHashCode().ToString()))
//                {
//                    iZmanMaximum = oParam.iHashlamaMaxShabat;
//                }
//                else
//                {
//                    if ((oSidur.sErevShishiChag == "1") || (oSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()))
//                    {
//                        iZmanMaximum = oParam.iHashlamaMaxShisi;
//                    }
//                    else
//                    {
//                        iZmanMaximum = oParam.iHashlamaMaxYomRagil;
//                    }
//                }
//                if (iTotalHashlamotForSidur > iZmanMaximum)
//                {
//                    drNew = dtErrors.NewRow();
//                    InsertErrorRow(oSidur, ref drNew, "מספר השלמות גדול מהמותר ליום עבודה ", enErrors.errTotalHashlamotBiggerThanAllow.GetHashCode());
//                    drNew["total_hashlamot"] = iTotalHashlamotForSidur;
//                    drNew["zman_maximum"] = iZmanMaximum;
//                    dtErrors.Rows.Add(drNew);
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsSidurVisaValid57(ref  clSidur oSidur, ref DataTable dtErrors)
//        {
//            string sLookUp = "";
//            //בדיקה ברמת סידור
//            try
//            {
//                //סידור ויזה חייב סימון כלשהו בשדה "יום ויזה". יש ערכים מותרים לשדה זה. קיימת טבלת ערכים עבור יום ויזה. מזהים סידור ויזה לפי  מאפיין (45) בטבלת סידורים מיוחדים. נתון זה יגיע גם בהשלמת נתונים, לא רק בהקלדה.
//                if (oSidur.bSidurVisaKodExists)
//                {
//                    sLookUp = GetLookUpKods("CTB_YOM_VISA");
//                    if ((sLookUp.IndexOf(oSidur.sVisa) == -1) || (string.IsNullOrEmpty(oSidur.sVisa)))
//                    {
//                        drNew = dtErrors.NewRow();
//                        InsertErrorRow(oSidur, ref drNew, "סדור ויזה ללא סימון", enErrors.errSimunVisaNotValid.GetHashCode());
//                        drNew["visa"] = oSidur.sVisa;
//                        dtErrors.Rows.Add(drNew);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void IsYomVisaValid56(ref  clSidur oSidur, ref DataTable dtErrors)
//        {
//            string sLookUp="";
//            //בדיקה ברמת סידור
//            try
//            {
//                //סידור ויזה חייב סימון כלשהו בשדה "יום ויזה". יש ערכים מותרים לשדה זה. קיימת טבלת ערכים עבור יום ויזה. מזהים סידור ויזה לפי  מאפיין (45) בטבלת סידורים מיוחדים. נתון זה יגיע גם בהשלמת נתונים, לא רק בהקלדה.
//                if (oSidur.bSidurVisaKodExists)
//                {
//                    sLookUp = GetLookUpKods("CTB_YOM_VISA");
                                      
//                    if ((sLookUp.IndexOf(oSidur.sVisa) == -1) || (string.IsNullOrEmpty(oSidur.sVisa)))
//                    {
//                        drNew = dtErrors.NewRow();
//                        InsertErrorRow(oSidur, ref drNew, "יום ויזה שגוי", enErrors.errYomVisaNotValid.GetHashCode());
//                        drNew["visa"] = oSidur.sVisa;                        
//                        dtErrors.Rows.Add(drNew);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void IsHashlamaForComputerAndAccidentValid45(ref  clSidur oSidur, ref DataTable dtErrors)
//        {
//            string sHashlamaLeyom;
//            bool bError = false;

//            //בדיקה ברמת סידור
//            try
//            {
//                // איפיון קודם, לא רלוונטי- השלמה ליום עבודה (ערך 9) מותרת בשני מקרים: מפעיל מחשב (עיסוק 122 או  123) וסידור הוא מנהל (סידור מיוחד עם ערך 1 (שעונים) במאפיין 52 (אופי עבודה)). מקרה שני הוא סידור תאונה (סידור מיוחד עם ערך 3 (תאונה) במאפיין 53 (אופי העדרות)). 
//                //אם יש סימון של השלמה ליום עבודה והסידור הוא יחיד ביום והוא מסוג העדרות (לפי מאפיין 53, לא משנה מה הערך במאפיין) או שהסידור מסומן לא לתשלום (רלוונטי לכל הסידורים, מיוחדים ורגילים) - שגיאה.
//                sHashlamaLeyom = oOvedYomAvodaDetails.sHashlamaLeyom;
//                if (!string.IsNullOrEmpty(sHashlamaLeyom))
//                {
//                    if ((htEmployeeDetails.Count == 1))
//                    {
//                        if (oSidur.bSidurMyuhad)
//                        {
//                            if ((oSidur.bHeadrutTypeKodExists) ||  (oSidur.iLoLetashlum > 0))
//                            {
//                                bError = true;
//                            }
//                        }
//                        else
//                        {
//                            if (oSidur.iLoLetashlum > 0)
//                            {
//                                bError = true;
//                            }
//                        }
//                    }
//                    if (bError)
//                    {
//                        drNew = dtErrors.NewRow();
//                        InsertErrorRow(oSidur, ref drNew, "השלמה ליום עבודה בסידור היעדרות / סידור לא לתשלום", enErrors.errHashlamaForComputerWorkerAndAccident.GetHashCode());
//                        drNew["hashlama"] = oSidur.sHashlama;
//                        dtErrors.Rows.Add(drNew);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsZakaiLehamara44(DataRow[] drSugSidur, ref  clSidur oSidur, ref DataTable dtErrors)
//        {
//            //בדיקה ברמת סידור 
//            string sMaamad;
//            try
//            {
//                if (!string.IsNullOrEmpty(oOvedYomAvodaDetails.sKodMaamd))
//                {
//                    sMaamad = oOvedYomAvodaDetails.sKodMaamd.Substring(0, 2);
//                    if (oSidur.sHamaratShabat != null)
//                    {
//                        //סידור מיוחד
//                        if ((oSidur.bSidurMyuhad) && (oSidur.iMisparSidurMyuhad > 0))
//                        {
//                            //אם סידור נהגות וקיים לעובד ותק, אז זכאי להמרת שבת
//                            if (!((oSidur.sSectorAvoda == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString()) && ((sMaamad == clGeneral.enMaamad.Friends.GetHashCode().ToString()) || (sMaamad == "21") || (sMaamad == "22"))))
//                            {
//                                drNew = dtErrors.NewRow();
//                                InsertErrorRow(oSidur, ref drNew, "המרה לעובד שלא זכאי ", enErrors.errZakaiLehamaratShabat.GetHashCode());
//                                drNew["hamarat_shabat"] = oSidur.sHamaratShabat;
//                                dtErrors.Rows.Add(drNew);
//                            }
//                        }
//                        else //סידור רגיל
//                        {
//                            if (drSugSidur.Length > 0)
//                            {
//                                //if (!((drSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString()) && (((sMaamad.Substring(0,1) == clGeneral.enMaamad.Friends.GetHashCode().ToString()) || (sMaamad == "21") || (sMaamad == "22")))))
//                                if (!(((drSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString()) && ((sMaamad.Substring(0, 1) == clGeneral.enMaamad.Friends.GetHashCode().ToString()) || (sMaamad == "21") || (sMaamad == "22") ))))
//                                {
//                                    drNew = dtErrors.NewRow();
//                                    InsertErrorRow(oSidur, ref drNew, "המרה לעובד שלא זכאי ", enErrors.errZakaiLehamaratShabat.GetHashCode());
//                                    drNew["hamarat_shabat"] = oSidur.sHamaratShabat;
//                                    dtErrors.Rows.Add(drNew);
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsHamaratShabatAllowed43(DateTime dDateCard, ref  clSidur oSidur, ref DataTable dtErrors)
//        {
//            //בדיקה ברמת סידור 
//            bool bError=false;
//            DataRow drNew;
//            DateTime dShatGmar;
//            try
//            {
//                //אסורה המרה ביום שהוא לא שבתון או ערב שבת/חג החל משעון שבת. שבתון/ערב שבת יכול להגיע מטבלת ימים מיוחדים או מה- Oracle. עבור ערב שבת/חג יש לבדוק אם אנחנו בתקופת קיץ/חורף לפי פרמטרים 25 (תחילת זמן קיץ אגד) ו- 26 (סיום זמן קיץ אגד). לפי התקופה יש לבדוק את שעת כניסת השבת לפי פרמטרים 6 (כניסת שבת חורף) ו- 7 (כניסת שבת קיץ). לגבי ערב שבת/חג בודקים ששעת גמר הסידור היא לפני כניסת שבת/חג.
//                if (((oSidur.iMisparSidurMyuhad > 0) && (oSidur.bSidurMyuhad)) || (!(oSidur.bSidurMyuhad)))
//                {
//                    if (!string.IsNullOrEmpty(oSidur.sHamaratShabat)) 
//                    {
//                        if ((oSidur.sErevShishiChag == "1") || (oSidur.sShabaton == "1") || (oSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()) || (oSidur.sSidurDay == clGeneral.enDay.Shabat.GetHashCode().ToString()))
//                        {
//                            //אם יום שישי, נבדוק שלא נכנסה השבת
//                            if ((oSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()) || (oSidur.sErevShishiChag == "1"))
//                            {
//                                if (!(string.IsNullOrEmpty(oSidur.sShatGmar)))
//                                {
//                                //אם שעת הגמר של הסידור גדולה משעת כניסת השבת, אז שבתון ומותרת המרה
//                                    dShatGmar = DateTime.Parse(string.Concat(dDateCard.ToShortDateString(), " ", oSidur.sShatGmar));                                
//                                    bError = (dShatGmar < oParam.dKnisatShabat); //פרמטר .7/6                                                           
//                                }
//                            }
//                        }
//                        else
//                        {
//                            //raise error - יש ערך המרה לא בשבת
//                            bError = true;
//                        }
//                    }
//                    if (bError)
//                    {
//                        drNew = dtErrors.NewRow();
//                        InsertErrorRow(oSidur, ref drNew, "המרת שבת לא ביום שבתון ", enErrors.errHamaratShabatNotAllowed.GetHashCode());
//                        drNew["hamarat_shabat"] = oSidur.sHamaratShabat;
//                        dtErrors.Rows.Add(drNew);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }


//        private void IsNesiaMeshtanaDefine150(DateTime dDateCard, ref  clSidur oSidur, ref DataTable dtErrors)
//        {
//            //בדיקה ברמת סידור             
//            DataRow drNew;
//            //DataRow[] drMeafyn;
//            int iMerkazErua=0;
//            int iMikumShaonYetzia, iMikumShaonKnisa;
//            errNesiaMeshtana enRes;
//            try
//            {
//                //שגיאה חדשה - היום נבדק רק בחישוב !!! לעובד מגיע זמן נסיעה משתנה (מאפיין 61 במאפייני עובדים) עפ"י מקום החתמת השעון. נבנתה טבלה המכילה את זמני הנסיעה ממקום מגוריו של העובד (נקבע עפ"י מרכז הארוע - קוד נתון 10 בטבלת פרטי עובדים) אל מיקום שעון כניסה וממיקום שעון יציאה הביתה. לעיתים חסר נתון בטבלה וזאת השגיאה.  כדי לדעת מה הערך הרלוונטי לעובד יש להפעיל את "הפרוצדורה של שרי"
//                if (oMeafyeneyOved.Meafyen61Exists) // אם קיים מאפיין 61, לעובד מגיע זמן נסיעה משתנה
//                {
//                    if (oOvedYomAvodaDetails.bMercazEruaExists)
//                    {
//                        //נקרא את מיקום האירוע של העובד
//                        iMerkazErua = string.IsNullOrEmpty(oOvedYomAvodaDetails.sMercazErua) ? 0 : int.Parse(oOvedYomAvodaDetails.sMercazErua);
//                        iMikumShaonKnisa = string.IsNullOrEmpty(oSidur.sMikumShaonKnisa) ? 0 : int.Parse(oSidur.sMikumShaonKnisa);
//                        iMikumShaonYetzia = string.IsNullOrEmpty(oSidur.sMikumShaonYetzia) ? 0 : int.Parse(oSidur.sMikumShaonYetzia);

//                        enRes = GetNesiaMeshtanaTimeFromTable(iMerkazErua, iMikumShaonKnisa, iMikumShaonYetzia);
//                        if (enRes != errNesiaMeshtana.enDefineAll)
//                        {
//                            drNew = dtErrors.NewRow();
//                            InsertErrorRow(oSidur, ref drNew, "חסרה הגדרה בטבלת זמן נסיעה משתנה ", enErrors.errNesiaMeshtanaNotDefine.GetHashCode());                           
//                            drNew["mikum_shaon_knisa"] = oSidur.sMikumShaonKnisa;
//                            drNew["mikum_shaon_Yetzia"] = oSidur.sMikumShaonYetzia;                           
//                            dtErrors.Rows.Add(drNew);
//                        }
//                    }                        
//                }                                    
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void IsOnePeilutExists127(ref clSidur oSidur, ref DataTable dtErrors)
//        {
//            //בדיקה ברמת סידור             
//            DataRow drNew;            
            
//            try
//            {
//                //יש סידורים המחייבים לפחות פעילות אחת. אם לא קיימת - שגוי. עבור סידורים מיוחדים נבדק אל מול טבלת מאפייני סידורים מיוחדים מאפיין 13 (חובה לדווח פעילות). עבור סידור שאינו מיוחד - חובה שתהיה פעילות אחת לפחות.
               
//                //אם סידור מיוחד וגם דורש לפחות פעילות אחת או סידור רגיל
//                if (((oSidur.bSidurMyuhad) && (oSidur.bPeilutRequiredKodExists) && (oSidur.iMisparSidurMyuhad > 0)) || (!(oSidur.bSidurMyuhad)))
//                {                    
//                //אם אין פעילויות נעלה שגיאה
//                    if (oSidur.htPeilut.Count == 0)
//                    {
//                        drNew = dtErrors.NewRow();
//                        InsertErrorRow(oSidur, ref drNew, "חובה לפחות פעילות אחת", enErrors.errAtLeastOnePeilutRequired.GetHashCode());                                                
//                        dtErrors.Rows.Add(drNew);
//                    }                    
//                }             
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void IsNesiaInSidurVisaAllowed125(ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors)
//        {
//            DataRow drNew;
//            int iMakatType;
//            clKavim oKavim = new clKavim();

//            //בדיקה ברמת פעילות
//            try
//            {
//                //בסידורי ויזה מותר רק פעילויות שמתחילות ב- 5 (תעודת ויזה) או 7 (אלמנט). עבור אלמנט יש לבדוק אם הוא מותר בסידור ויזה (לפי ערך 1 (רשאי) במאפיין 12 (דיווח בסידור ויזה) בטבלת מאפייני אלמנטים). טבלת מאפייני אלמנטים תכיל מאפיינים לכל האלמנטים במערכת. מזהים סידור ויזה לפי מאפיין 45 בסידורים מיוחדים. 
//                if (oSidur.bSidurVisaKodExists)
//                {
//                    iMakatType = oKavim.GetMakatType(oPeilut.lMakatNesia);
//                    if (!((iMakatType == clKavim.enMakatType.mVisut.GetHashCode()) || ((iMakatType == clKavim.enMakatType.mElement.GetHashCode()) && (oPeilut.sDivuchInSidurVisa == "1"))))
//                    {
//                        drNew = dtErrors.NewRow();
//                        InsertErrorRow(oSidur, ref drNew, "נסיעה אסורה בסידור ויזה", enErrors.errNesiaInSidurVisaNotAllowed.GetHashCode());
//                        InsertPeilutErrorRow(oPeilut, ref drNew);
//                        dtErrors.Rows.Add(drNew);
//                    }
//                }                
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void IsOvedEggedTaavoraKodValid141(clGeneral.enEmployeeType enEmployeeType, ref clSidur oSidur, ref DataTable dtErrors)
//        {
           
//            DataRow drNew;
//            //בדיקה ברמת כרטיס עבודה
//            try
//            {   //בודקים אם עובד הוא מאגד תעבורה. אם כן צריך שהשדות הבאים הבאים יקבלו את הערכים הבאים, אחרת זה שגוי: א. ביטול נסיעות = 4, ב. הלבשה = 4, ג. מחוץ למכסה = 0, ד. המרה = 0, ה.השלמה = 0. מזהים אגד תעבורה לפי קוד חברה של העובד.             
//                if (enEmployeeType ==clGeneral.enEmployeeType.enEggedTaavora)
//                {
//                    if ((oOvedYomAvodaDetails.sBitulZmanNesiot!="4") && (!String.IsNullOrEmpty(oOvedYomAvodaDetails.sBitulZmanNesiot)))
//                    {
//                        drNew = dtErrors.NewRow();
//                        drNew["Bitul_Zman_Nesiot"] = oOvedYomAvodaDetails.sBitulZmanNesiot;
//                        InsertErrorRow(oSidur, ref drNew, "קוד אסור לעובד אגד תעבורה", enErrors.errNotAllowedKodsForEggedTaavora.GetHashCode());
//                        dtErrors.Rows.Add(drNew);
//                    }
//                    if ((oOvedYomAvodaDetails.sHalbasha != "4") && (!String.IsNullOrEmpty(oOvedYomAvodaDetails.sHalbasha))) 
//                    {
//                        drNew = dtErrors.NewRow();
//                        drNew["Halbasha"] = oOvedYomAvodaDetails.sHalbasha;
//                        InsertErrorRow(oSidur, ref drNew, "קוד אסור לעובד אגד תעבורה", enErrors.errNotAllowedKodsForEggedTaavora.GetHashCode());
//                        dtErrors.Rows.Add(drNew);
//                    }
//                    if ((oSidur.sHamaratShabat != "0") && (!String.IsNullOrEmpty(oSidur.sHamaratShabat)))
//                    {
//                        drNew = dtErrors.NewRow();
//                        drNew["Hamarat_Shabat"] = oSidur.sHamaratShabat;
//                        InsertErrorRow(oSidur, ref drNew, "קוד אסור לעובד אגד תעבורה", enErrors.errNotAllowedKodsForEggedTaavora.GetHashCode());
//                        dtErrors.Rows.Add(drNew);
//                    }
//                    if ((oSidur.sHashlama != "0") && (!String.IsNullOrEmpty(oSidur.sHashlama)))
//                    {
//                        drNew = dtErrors.NewRow();
//                        drNew["Hashlama"] = oSidur.sHashlama;
//                        InsertErrorRow(oSidur, ref drNew, "קוד אסור לעובד אגד תעבורה", enErrors.errNotAllowedKodsForEggedTaavora.GetHashCode());
//                        dtErrors.Rows.Add(drNew);
//                    }
//                    if ((oSidur.sOutMichsa!="0") && (!String.IsNullOrEmpty(oSidur.sOutMichsa)))
//                    {                        
//                        drNew = dtErrors.NewRow();
//                        drNew["Out_Michsa"] = oSidur.sOutMichsa;
//                        InsertErrorRow(oSidur, ref drNew, "קוד אסור לעובד אגד תעבורה", enErrors.errNotAllowedKodsForEggedTaavora.GetHashCode());
//                        dtErrors.Rows.Add(drNew);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsSidurAllowedForEggedTaavora148(clGeneral.enEmployeeType enEmployeeType,ref clSidur oSidur, ref DataTable dtErrors)
//        {//בדיקה ברמת סידור
//            try
//            {
//                //לעובד בדרוג 085 (אגד תעבורה) אסור לבצע את הסידור (לפי מאפיין 31 אסור לדיווח לחברות בת בטבלת מאפייני סידורים מיוחדים). יש סימון בטבלת סידורים מיוחדים שהסידור אסור לעובדי שאינם אגד. עבור סידורים רגילים, אין לבדוק את השגיאה כי מראש הם לא ישובצו לסידורים אלו. מזהים אגד תעבורה לפי קוד חברה של העובד. 
//                if (enEmployeeType ==clGeneral.enEmployeeType.enEggedTaavora)
//                {
//                    if ((oSidur.bSidurMyuhad) && (oSidur.bSidurNotValidKodExists))
//                    {
//                        drNew = dtErrors.NewRow();
//                        InsertErrorRow(oSidur, ref drNew, "סדור אסור לעובד בדרוג 85", enErrors.errNotAllowedSidurForEggedTaavora.GetHashCode());
//                        dtErrors.Rows.Add(drNew);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void IsSidurNetzerNotValidForOved124(ref clSidur oSidur, ref DataTable dtErrors)
//        {//בדיקה ברמת סידור
//            try
//            {
//                //עובדים במרכז נ.צ.ר רשאים לעבוד שם רק לאחר שעברו הכשרה. בסיומה מדווחים להם מאפיין 64. מזהים סידור נ.צ.ר לפי מאפיין 52 ערך 11(סידור נצר) בטבלת סידורים מיוחדים.
//                if ((oSidur.bSidurMyuhad) && (oSidur.sSugAvoda == clGeneral.enSugAvoda.Netzer.GetHashCode().ToString()) && (!oMeafyeneyOved.Meafyen64Exists))
//                {
//                    drNew = dtErrors.NewRow();
//                    InsertErrorRow(oSidur, ref drNew, "לעובד אסור סידור נ.צ.ר", enErrors.errSidurNetzerNotValidForOved.GetHashCode());
//                    dtErrors.Rows.Add(drNew);
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsSidurNamlakWithoutNesiaCard13(ref clSidur oSidur,ref clPeilut oPeilut, ref DataTable dtErrors)
//        {//בדיקה ברמת פעילות
//            try
//            {
//                //אם הסידור הוא נמל"ק (לפי מאפיין 45 (ויזה) במאפייני סידורים מיוחדים), יש לבדוק שיש לסידור תעודת נסיעה (ערך בשדה מספר ויזה)..
//                if ((oSidur.bSidurMyuhad) && (oSidur.bSidurVisaKodExists) && (oSidur.iMisparSidurMyuhad>0))
//                {
//                    if ((oPeilut.lMisparVisa == 0) && (oPeilut.lMakatNesia>0))  //אין תעודת נסיעה
//                    {
//                        drNew = dtErrors.NewRow();
//                        InsertErrorRow(oSidur, ref drNew, "סדור נמלק ללא תעודת נסיעה", enErrors.errSidurNamlakWithoutNesiaCard.GetHashCode());
//                        InsertPeilutErrorRow(oPeilut, ref drNew);
//                        dtErrors.Rows.Add(drNew);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsSidurValidInShabaton50(ref clSidur oSidur,  ref DataTable dtErrors)
//        {   //בדיקה ברמת סידור
//            try
//            {
//                //יש לבדוק שהסידור המיוחד מותר בשבתון (לפי מאפיין 9 (אסור בשבתון) בטבלת מאפייני סידורים מיוחדים) ושלעובד מותר לעבוד בשבתון (יש לו מאפיין 7 (מאפיין זה אומר זמן התחלה מותר בשבת (שבתון בלבד לא כולל ערב)). שבתון יכול להיות יום שבת (חוזר מה- Oracle) או שבטבלת סוגי ימים מיוחדים הוא מוגדר כשבתון. במקרה זה השבת נחשבת החל משעות שבת (ערב שבת/חג). בדיקה זו לא רלוונטית עבור סידור לא מיוחד משום שבמקור הוא תוכנן לשבתון ואם זה תוכנן זה תקין. השגיאה מתייחסת לסידור ולא לאם לעובד מותר לעבוד בשבתון ולכן לא מעניין מצב זו הסידור מותר בשבתון ולעובד אין מאפיין 7. השגיאה לא רלוונטית לערב שבת/חג.
//               //לא להתייחס למאפיין 7
//                if (oSidur.bSidurMyuhad) //וסידור מיוחד
//                {   //אם שבתון וקיים מאפיין 9, כלומר סידור אזור בשבתון, אז נעלה שגיאה
//                    if (((oSidur.sShabaton == "1") || (oSidur.sSidurDay == clGeneral.enDay.Shabat.GetHashCode().ToString())) && (oSidur.bSidurNotInShabtonKodExists ))
//                    {//היום הוא יום שבתון ולסידור יש מאפיין אסור בשבתון, לכן נעלה שגיאה
//                        drNew = dtErrors.NewRow();
//                        InsertErrorRow(oSidur, ref drNew, "סדור אסור בשבתון ", enErrors.errSidurNotAllowedInShabaton.GetHashCode());
//                        dtErrors.Rows.Add(drNew);
//                    }
//                }               
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsPitzulSidurInShabatValid24(DateTime dCardDate, int iSidur,  ref clSidur oSidur, ref DataTable dtErrors)
//        {   //בדיקה ברמת סידור           
//            //אם יש סימון של פיצול ביום העבודה והיום הוא ערב שבת/חג ושעת התחלה של החלק השני של הפיצול גדול משעת כניסת שבת - זוהי שגיאה. יש לבדוק אם אנחנו בתקופת קיץ/חורף לפי פרמטרים 25 (תחילת זמן קיץ אגד) ו- 26 (סיום זמן קיץ אגד). לפי התקופה יש לבדוק את שעת כניסת השבת לפי פרמטרים 6 (כניסת שבת חורף) ו- 7 (כניסת שבת קיץ). אם עבור יום מסוים חוזר מהאם ביום שהוא ערב שבת/חג יש סידור אחד שמסתיים לפני כניסת שבת ויש לו סימון בשדה פיצול והסידור העוקב אחריו התחיל אחרי כניסת השבת  - זו שגיאה. (מצב תקין הוא שהסידור העוקב התחיל לפני כניסת שבת וגלש/לא גלש לשבת). יש לבדוק אם אנחנו בתקופת קיץ/חורף לפי פרמטרים 48 (תחילת זמן קיץ אגד) ו- 49 (סיום זמן קיץ אגד). לפי התקופה יש לבדוק את שעת כניסת השבת לפי פרמטרים 6 (כניסת שבת חורף) ו- 7 (כניסת שבת קיץ). אם עבור יום מסוים חוזר מה- Oracle שזה שבת ובטבלת ימים מיוחדים חוזר משהו אחר (לא שבתון) אז מה שחוזר מה- Oracle הוא "חזק" יותר. השגיאה לא רלוונטית לערב חג שנופל ביום שבת משום כי השבתון גובר על ערב שבתון.
//            DateTime dSidurPrevShatGmar;
//            DateTime dSidurShatHatchala;
//            clSidur oPrevSidur = (clSidur)htEmployeeDetails[iSidur-1];
//            string sSidurPrevShatGmar = oPrevSidur.sShatGmar;
//            int iSidurPrevPitzulHafsaka = string.IsNullOrEmpty(oPrevSidur.sPitzulHafsaka) ? 0 : int.Parse(oPrevSidur.sPitzulHafsaka);

//            try
//            {//אם הגענו לרוטינה, סימן שצויין פיצול בסידור הקודם
//                if (!(string.IsNullOrEmpty(oSidur.sShatHatchala)))
//                {
//                    //אם  יום שישי או ערב חג אבל  לא בשבתון
//                    if ((((oSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()) || ((oSidur.sErevShishiChag == "1") && (oSidur.sSidurDay != clGeneral.enDay.Shabat.GetHashCode().ToString())))) && (iSidurPrevPitzulHafsaka > 0))
//                    {
//                        //נקרא את שעת כניסת השבת                   
//                        //אם ביום שהוא ערב שבת/חג יש סידור אחד שמסתיים לפני כניסת שבת ויש לו סימון בשדה פיצול והסידור העוקב אחריו התחיל אחרי כניסת השבת  - זו שגיאה. (מצב תקין הוא שהסידור העוקב התחיל לפני כניסת שבת וגלש/לא גלש לשבת). 
//                        //if (((int.Parse(sSidurPrevShatGmar.Remove(2, 1)) > iShabatStart)) || (int.Parse(oSidur.sShatHatchala.Remove(2, 1)) <= iShabatStart))
//                        dSidurPrevShatGmar = DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", sSidurPrevShatGmar));
//                        dSidurShatHatchala = DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", oSidur.sShatHatchala));
//                        if ((dSidurPrevShatGmar <= oParam.dKnisatShabat) && (dSidurShatHatchala > oParam.dKnisatShabat))
//                        {
//                            //נציג את הסידור השני כשגוי
//                            drNew = dtErrors.NewRow();
//                            InsertErrorRow(oSidur, ref drNew, " עבודה בערב שבת/חג לאחר פצול", enErrors.errPitzulSidurInShabat.GetHashCode());
//                            dtErrors.Rows.Add(drNew);
//                        }
//                        if ((dSidurPrevShatGmar > oParam.dKnisatShabat) && (dSidurShatHatchala > oParam.dKnisatShabat))
//                        {
//                            //נציג את שני הסידורים כשגויים
//                            drNew = dtErrors.NewRow();
//                            InsertErrorRow(oPrevSidur, ref drNew, " עבודה בערב שבת/חג לאחר פצול", enErrors.errPitzulSidurInShabat.GetHashCode());
//                            dtErrors.Rows.Add(drNew);
//                            drNew = dtErrors.NewRow();
//                            InsertErrorRow(oSidur, ref drNew, " עבודה בערב שבת/חג לאחר פצול", enErrors.errPitzulSidurInShabat.GetHashCode());
//                            dtErrors.Rows.Add(drNew);
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void IsSidurExists9(ref clSidur oSidur, ref DataTable dtErrors)
//        {
//            //בדיקה ברמת סידור 
//            try
//            {//מספר הסידור לא נמצא במפה (טבלת סידורים) לתאריך הנדרש או בטבלת סידורים מיוחדים. 
//                if (oSidur.bSidurMyuhad)
//                {
//                    if (oSidur.iMisparSidurMyuhad == 0)
//                    {
//                        drNew = dtErrors.NewRow();
//                        InsertErrorRow(oSidur, ref drNew, "סידור לא קיים", enErrors.errSidurNotExists.GetHashCode());
//                        dtErrors.Rows.Add(drNew);
//                    }
//                }
//                else
//                {
//                    if (!oSidur.bSidurRagilExists)
//                    {
//                        drNew = dtErrors.NewRow();
//                        InsertErrorRow(oSidur, ref drNew, "סידור לא קיים", enErrors.errSidurNotExists.GetHashCode());
//                        dtErrors.Rows.Add(drNew);
//                    }
//                }

//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsZakaiLeNesiot26(int iMisparIshi,DateTime dCardDate, ref DataTable dtErrors)
//        {           
//            //בדיקה ברמת עובד
//            try
//            {
//                //אם יש ערך תקין בשדה ביטול נסיעות ולעובד אין מאפיין 51 (זמן נסיעה קבוע) או 61 (זמן נסיעה משתנה) - זו שגיאה.                
//                if ((!(string.IsNullOrEmpty(oOvedYomAvodaDetails.sBitulZmanNesiot))) && (!oMeafyeneyOved.Meafyen51Exists) && (!oMeafyeneyOved.Meafyen61Exists))
//                {
//                    drNew = dtErrors.NewRow();
//                    drNew["check_num"] = enErrors.errLoZakaiLeNesiot.GetHashCode();
//                    drNew["mispar_ishi"] =iMisparIshi;
//                    drNew["taarich"] = dCardDate.ToShortDateString();
//                    drNew["error_desc"] = "לא זכאי לנסיעות";                    
//                    dtErrors.Rows.Add(drNew);
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsOvodaInMachalaAllowed132(DateTime dCardDate, int iMisparIshi,ref DataTable dtErrors)
//        {
           
//            //בדיקה ברמת עובד
//            try
//            {
//                //עובד לא יכול לעבוד בכל עבודה באגד אם במחלה. מחלה זה סטטוס ב- HR. מזהים שהעובד עבד ביום מסוים אם יש לו לפחות סידור אחד ביום זה.  
//                if ((IsOvedMatzavExists("5")) && (htEmployeeDetails.Count>0))
//                {
//                    drNew = dtErrors.NewRow();
//                    drNew["check_num"] = enErrors.errOvdaInMachalaNotAllowed.GetHashCode();
//                    drNew["mispar_ishi"] = iMisparIshi;
//                    drNew["taarich"] = dCardDate.ToShortDateString();
//                    drNew["error_desc"] = "עבודה אסורה במחלה ארוכה";
//                    dtErrors.Rows.Add(drNew);
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void IsSidurTafkidValid145(ref clSidur oSidur,ref DataTable dtErrors)
//        {
//            //בדיקה ברמת סידור
//            try
//            {//אסור לדווח סידור תפקיד אם אין לעובד מאפייני שעת התחלה / גמר (לפי מאפיינים 3/4 במאפייני עובדים). רלוונטי רק לסידורים מיוחדים, לא לרגילים.
//                if ((oSidur.bSidurMyuhad) && (oSidur.sSectorAvoda == clGeneral.enSectorAvoda.Tafkid.GetHashCode().ToString()) && ((!oMeafyeneyOved.Meafyen3Exists) || (!oMeafyeneyOved.Meafyen4Exists)))
//                {
//                    drNew = dtErrors.NewRow();
//                    InsertErrorRow(oSidur, ref drNew, "סידור תפקיד ללא מאפיין התחלה/גמר", enErrors.errSidurTafkidWithOutApprove.GetHashCode());
//                    dtErrors.Rows.Add(drNew);
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsDriverLessonsNumberValid136(DataRow[] drSugSidur, ref clSidur oSidur, ref DataTable dtErrors)
//        {//בדיקה ברמת סידור
//            bool bError = false;
//            try
//            {//מורה לנהיגה צריך לדווח את מספר השיעורים שלימד. מזהים סידור של הוראת לנהיגה לפי ערך 5 (הוראת נהיגה) במאפיין 52 (סוג עבודה) בטבלת מאפייני סידורים מיוחדים/מאפייני סוג סידור. מכסימום שעורי נהיגה למורה בסידור יחיד לפי פרמטר 141 (מכסימום שעורי נהיגה למורה בסידור יחיד) בטבלת פרמטרים. שיעורי נהיגה מדווחים לשדה ברמת סידור. 
//                //if (oSidur.bSidurMyuhad)
//                //{//סידורים מיוחדים
//                //    if (oSidur.sSugAvoda != "-1")
//                //    {//מזהים סידור של הוראת לנהיגה לפי ערך 5 (הוראת נהיגה) במאפיין 52 (סוג עבודה
//                //        if (oSidur.sSugAvoda == clGeneral.enSugAvoda.Nahagut.GetHashCode().ToString())
//                //        {
//                //            if ((oSidur.iMisparShiureyNehiga == 0) || (oSidur.iMisparShiureyNehiga > oParam.iMaxDriverLessons))
//                //            {
//                //                bError = true;
//                //            }
//                //        }
//                //    }
//                //}
//                //else
//                if (!oSidur.bSidurMyuhad)
//                {//סידורים רגילים
//                 if (drSugSidur.Length > 0)
//                    {   //עבור סידורים רגילים, נבדוק במאפייני סידורים אם סוג סידור נהגות.
//                        if ((drSugSidur[0]["sug_avoda"].ToString() == clGeneral.enSugAvoda.Nahagut.GetHashCode().ToString()))
//                        {
//                            if ((oSidur.iMisparShiureyNehiga == 0) || (oSidur.iMisparShiureyNehiga > oParam.iMaxDriverLessons))
//                            {
//                                bError = true;
//                            }
//                        }
//                    }
//                }

//                if (bError)
//                {
//                    drNew = dtErrors.NewRow();
//                    InsertErrorRow(oSidur, ref drNew, "שיעור נהיגה לא בסידור הוראת נהיגה", enErrors.errDriverLessonsNumberNotValid.GetHashCode());
//                    dtErrors.Rows.Add(drNew);
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsSidurAvodaValidForTaarich160(ref clSidur oSidur, ref DataTable dtErrors)
//        {//בדיקה ברמת סידור
//            bool bError = false;
//            DateTime dSidurDate;
//            try
//            {//קיימים סידורי עבודה מיוחדים הפעילים רק מספר חודשים בשנה - סידורי קייטנות וארועי קייץ, סידורים אלו מזוהים לפי קיום ערך עבור מאפיין 73 (סוג עבודה בארועי קיץ) בטבלת מאפייני סידורים מיוחדים.התקופה החוקית לסידורים אלה נבדקת אל מול פרמטרים בטבלת פרמטרים חיצוניים (44-47).  (44 - אירועי קיץ - תאריך חוקי לתחילת סידורי ניהול ושיווק, 45 - אירועי קיץ - תאריך חוקי לתחילת סידור תפעול , 46 - אירועי קיץ - תאריך חוקי לסיום סידור שיווק, 47 - אירועי קיץ - תאריך חוקי לסיום סידור). השגיאה רלוונטית רק לסידורים מיוחדים.
//                if (oSidur.bSidurMyuhad)
//                {//קיים מאפיין 73 
//                    if (oSidur.bSidurInSummerExists)
//                    {
//                        dSidurDate = DateTime.Parse(oSidur.sSidurDate);
//                        if ((oSidur.sSidurInSummer == "1") || (oSidur.sSidurInSummer == "2"))
//                        {
//                            if (oParam.dStartNihulVShivik.Year!=clGeneral.cYearNull)
//                            {
//                                bError = (dSidurDate < oParam.dStartNihulVShivik);
//                            }

//                            if (!bError)
//                            {
//                                if (oParam.dEndNihulVShivik.Year != clGeneral.cYearNull)
//                                {
//                                    bError = (dSidurDate > oParam.dEndNihulVShivik);
//                                }
//                            }
//                        }
//                        else
//                        {
//                            bError = (((dSidurDate < oParam.dStartTiful) && (oParam.dStartTiful.Year!=clGeneral.cYearNull)) || ((dSidurDate > oParam.dEndTiful) && (oParam.dEndTiful.Year!=clGeneral.cYearNull)));
//                        }
//                        if (bError)
//                        {
//                            drNew = dtErrors.NewRow();
//                            InsertErrorRow(oSidur, ref drNew, "סידור עבודה לא חוקי לחודש זה", enErrors.errSidurAvodaNotValidForMonth.GetHashCode());
//                            dtErrors.Rows.Add(drNew);
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void IsCurrentPeilutInPrevPeilut162(DateTime dPrevStartPeilut, DateTime dPrevEndPeilut,ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors)
//        {//בדיקה ברמת פעילות
//            DateTime dCurrEndPeilut=new DateTime();
//            DateTime dCurrStartPeilut=new DateTime();
//            bool bCheck = false;
//            double dblTimeInMinutes=0;
//            try
//            {
//                //זמן תחילת פעילות לאחר זמן תחילת הפעילות הקודמת לה וזמן סיום הפעילות קודם לזמן סיום הפעילות הקודמת לה. זיהוי זמן הפעילות (זיהוי סוג פעילות לפי רוטינת זיהוי מק"ט) :עבור קו שירות, נמ"ק, ריקה, יש לפנות לקטלוג נסיעות כדי לקבל את הזמן. עבור אלמנט, במידה וזה אלמנט זמן (לפי ערך 1 במאפיין 4 בטבלת מאפייני אלמנטים), הזמן נלקח מפוזיציות 4-6 של האלמנט. בבדיקה זו אין  להתייחס לפעילות המתנה (מזהים פעילות המתנה (מסוג אלמנט) לפי מאפיין 15 בטבלת מאפייני אלמנטים).           
//                if ((oPeilut.iMakatType == clKavim.enMakatType.mKavShirut.GetHashCode()) || (oPeilut.iMakatType == clKavim.enMakatType.mEmpty.GetHashCode()) || (oPeilut.iMakatType == clKavim.enMakatType.mNamak.GetHashCode()))
//                {
//                    dCurrStartPeilut = oPeilut.dFullShatYetzia;
//                    dblTimeInMinutes = oPeilut.iMazanTichnun;
//                    bCheck = true;
//                }
//                else
//                {
//                    dCurrStartPeilut = oPeilut.dFullShatYetzia;
//                    if (oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode())
//                    {
//                        //אם אלמנט זמן
//                        //אבל לא אלמנט זמן מסוג המתנה
//                        if ((oPeilut.sElementInMinutes == "1") && (!oPeilut.bElementHamtanaExists ))
//                        {
//                            dblTimeInMinutes = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));
//                            bCheck = true;
//                        }
//                    }                  
//                }

//                if (bCheck)
//                {
//                    dCurrEndPeilut = dCurrStartPeilut.AddMinutes(dblTimeInMinutes);
//                    if ((dCurrStartPeilut >= dPrevStartPeilut) && (dCurrEndPeilut <= dPrevEndPeilut))
//                    {
//                        drNew = dtErrors.NewRow();
//                        InsertErrorRow(oSidur, ref drNew, "פעילות נבלעת בתוך פעילות קודמת ", enErrors.errCurrentPeilutInPrevPeilut.GetHashCode());
//                        InsertPeilutErrorRow(oPeilut, ref drNew);
//                        dtErrors.Rows.Add(drNew);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsHashlamaLeYomAvodaValid163(int iMisparIshi, DateTime dCardDate,  ref DataTable dtErrors)
//        {//בדיקה ברמת יום עבודה
           
//            try
//            {
//                //השלמה ליום עבודה אסורה ביום שבתון (שבתון יכול להיות יום שבת (חוזר מה- Oracle) או שבטבלת סוגי ימים מיוחדים הוא מוגדר כשבתון) לעובד ללא מאפיין 07 (שעת התחלה מותרת בשבתון) או ביום שישי (אם חוזר 6 מה- Oracle) לעובד ללא מאפיין 05.
//                if (!string.IsNullOrEmpty(oOvedYomAvodaDetails.sHashlamaLeyom))
//                {    
//                    if ((((oOvedYomAvodaDetails.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()) || (oOvedYomAvodaDetails.sErevShishiChag=="1"))  && (!oMeafyeneyOved.Meafyen5Exists))
//                        || ((((oOvedYomAvodaDetails.sSidurDay == clGeneral.enDay.Shabat.GetHashCode().ToString()) || (oOvedYomAvodaDetails.sShabaton=="1")) && (!oMeafyeneyOved.Meafyen7Exists))))
//                    {                       
//                        drNew = dtErrors.NewRow();
//                        drNew["check_num"] = enErrors.errHashlamaLeYomAvodaNotAllowed.GetHashCode();
//                        drNew["mispar_ishi"] = iMisparIshi;
//                        drNew["taarich"] = dCardDate.ToShortDateString();
//                        drNew["error_desc"] = "השלמה ליום עבודה אסורה";                   
                    
//                        //InsertErrorRow(oSidur, ref drNew, "השלמה ליום עבודה אסורה ", enErrors.errHashlamaLeYomAvodaNotAllowed.GetHashCode());                        
//                        dtErrors.Rows.Add(drNew);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsSidurSummerValid164(ref clSidur oSidur, ref DataTable dtErrors)
//        {
//            //בדיקה ברמת סידור
//            try
//            {
//                //סידור של ארועי קיץ חייב להיות לעובד אשר הוגדר עובד 6 ימים (מזהים לפי ערך 61, 62) במאפיין 56 במאפייני עובדים. סידור של ארועי קיץ = סידור מיוחד עם מאפיין 73
//                if (((oSidur.bSidurInSummerExists) && (!oMeafyeneyOved.Meafyen56Exists)) || ((oMeafyeneyOved.Meafyen56Exists) && (oMeafyeneyOved.iMeafyen56 != clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode()) && (oMeafyeneyOved.iMeafyen56 != clGeneral.enMeafyenOved56.enOved6DaysInWeek2.GetHashCode())))
//                {
//                    drNew = dtErrors.NewRow();
//                    InsertErrorRow(oSidur, ref drNew, "סידור של ארועי קיץ לעובד 5 ימים", enErrors.errSidurSummerNotValidForOved.GetHashCode());
//                    dtErrors.Rows.Add(drNew);
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsSidurNAhagutValid161(DataRow[] drSugSidur,ref clSidur oSidur, ref DataTable dtErrors)
//        {
//            //בדיקה ברמת סידור
//            bool bError = false;
//            try
//            {
//                //לעובד אסור לבצע סידור נהיגה (עבור סידורים מיוחדים, מזהים סידור נהגות לפי ערך 5 במאפיין  3 בטבלת סידורים מיוחדים. עבור סידורים רגילים מזהים סידור נהגות לפי ערך 5 ב מאפיין 3 בטבלת מאפייני סידורים) במקרים הבאים: א. לעובד אין רישיון נהיגה באוטובוס (יודעים אם לעובד יש רישיון לפי ערכים 6, 10, 11 בקוד נתון 7 (קוד רישיון אוטובוס) בטבלת פרטי עובדים) ב. עובד הוא מותאם שאסור לו לנהוג (יודעים שעובד הוא מותאם שאסור לו לנהוג לפי ערכים 4, 5 בקוד נתון 8 (קוד עובד מותאם) בטבלת פרטי עובדים) ג. עובד הוא מותאם שמותר לו לבצע רק נסיעה ריקה (יודעים שעובד הוא מותאם שמותר לו לבצע רק נסיעה ריקה לפי ערכים 6, 7 בקוד נתון 8 (קוד עובד מותאם) בטבלת פרטי עובדים) במקרה זה יש לבדוק אם הסידור מכיל רק נסיעות ריקות, מפעילים את הרוטינה לזיהוי מקט ד. עובד הוא בשלילה (יודעים שעובד הוא בשלילה לפי ערך 1 בקוד בנתון 21 (שלילת   רשיון) בטבלת פרטי עובדים) 
//                if (oSidur.bSidurMyuhad) 
//                {
//                    if (oSidur.sSectorAvoda == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString())
//                    {
//                        //א. לעובד אין רישיון נהיגה באוטובוס (יודעים אם לעובד יש רישיון לפי ערכים 6, 10, 11 בקוד נתון 7 (קוד רישיון אוטובוס) בטבלת פרטי עובדים)
//                        bError = (!IsOvedHasDriverLicence());

//                        //ב. עובד הוא מותאם שאסור לו לנהוג (יודעים שעובד הוא מותאם שאסור לו לנהוג לפי ערכים 4, 5 בקוד נתון 8 (קוד עובד מותאם) בטבלת פרטי עובדים) 
//                        if (!(bError))
//                        {
//                            bError = IsOvedMutaam();
//                        }

//                        if (!bError)
//                        {
//                            //ד. עובד הוא בשלילה (יודעים שעובד הוא בשלילה לפי ערך 1 בקוד בנתון 21 (שלילת   רשיון) בטבלת פרטי עובדים) 
//                            bError = IsOvedBShlila();
//                        }
//                    }
//                }
//                else
//                {//סידור רגיל
//                    if (drSugSidur.Length > 0)
//                    {
//                        if (drSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString())
//                        {
//                            //א. לעובד אין רישיון נהיגה באוטובוס (יודעים אם לעובד יש רישיון לפי ערכים 6, 10, 11 בקוד נתון 7 (קוד רישיון אוטובוס) בטבלת פרטי עובדים)
//                            bError = (!IsOvedHasDriverLicence());

//                            //ב. עובד הוא מותאם שאסור לו לנהוג (יודעים שעובד הוא מותאם שאסור לו לנהוג לפי ערכים 4, 5 בקוד נתון 8 (קוד עובד מותאם) בטבלת פרטי עובדים) 
//                            if (!(bError))
//                            {
//                                bError = IsOvedMutaam();
//                            }

//                            if (!bError)
//                            {
//                                //ד. עובד הוא בשלילה (יודעים שעובד הוא בשלילה לפי ערך 1 בקוד בנתון 21 (שלילת   רשיון) בטבלת פרטי עובדים) 
//                                bError = IsOvedBShlila();
//                            }
//                            if (!bError)
//                            {
//                                //. עובד הוא מותאם שמותר לו לבצע רק נסיעה ריקה (יודעים שעובד הוא מותאם שמותר לו לבצע רק נסיעה ריקה לפי ערכים 6, 7 בקוד נתון 8 (קוד עובד מותאם) בטבלת פרטי עובדים) במקרה זה יש לבדוק אם הסידור מכיל רק נסיעות ריקות, מפעילים את הרוטינה לזיהוי מקט
//                                bError = (!IsOvedMutaamForEmptyPeilut(ref oSidur));
//                            }
//                        }
//                    }
//                }
//                if (bError)
//                {
//                    drNew = dtErrors.NewRow();
//                    InsertErrorRow(oSidur, ref drNew, "לעובד אסור לבצע סידור נהיגה", enErrors.errOvedNotAllowedToDoSidurNahagut.GetHashCode());
//                    dtErrors.Rows.Add(drNew);
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private bool IsOvedHasDriverLicence()
//        {
//            //א. לעובד אין רישיון נהיגה באוטובוס (יודעים אם לעובד יש רישיון לפי ערכים 6, 10, 11 בקוד נתון 7 (קוד רישיון אוטובוס) בטבלת פרטי עובדים)
//            return (oOvedYomAvodaDetails.sRishyonAutobus == clGeneral.enRishyonAutobus.enRishyon10.GetHashCode().ToString() ||
//                    oOvedYomAvodaDetails.sRishyonAutobus == clGeneral.enRishyonAutobus.enRishyon11.GetHashCode().ToString() ||
//                    oOvedYomAvodaDetails.sRishyonAutobus == clGeneral.enRishyonAutobus.enRishyon6.GetHashCode().ToString());

//        }

//        private bool IsOvedMutaam()
//        {
//            //ב. עובד הוא מותאם שאסור לו לנהוג (יודעים שעובד הוא מותאם שאסור לו לנהוג לפי ערכים 4, 5 בקוד נתון 8 (קוד עובד מותאם) בטבלת פרטי עובדים)
//            return (oOvedYomAvodaDetails.sMutamut == clGeneral.enMutaam.enMutaam4.GetHashCode().ToString() ||
//                    oOvedYomAvodaDetails.sMutamut == clGeneral.enMutaam.enMutaam5.GetHashCode().ToString()); 
     
//        }

//        private bool IsOvedBShlila()
//        {
//            //ד. עובד הוא בשלילה (יודעים שעובד הוא בשלילה לפי ערך 1 בקוד בנתון 21 (שלילת   רשיון) בטבלת פרטי עובדים) 
//            return oOvedYomAvodaDetails.sShlilatRishayon == clGeneral.enOvedBShlila.enBShlila.GetHashCode().ToString();
//        }


//        private bool IsOvedMutaamForEmptyPeilut(ref clSidur oSidur)
//        {
//            //. עובד הוא מותאם שמותר לו לבצע רק נסיעה ריקה (יודעים שעובד הוא מותאם שמותר לו לבצע רק נסיעה ריקה לפי ערכים 6, 7 בקוד נתון 8 (קוד עובד מותאם) בטבלת פרטי עובדים) במקרה זה יש לבדוק אם הסידור מכיל רק נסיעות ריקות, מפעילים את הרוטינה לזיהוי מקט
//            return ((oOvedYomAvodaDetails.sMutamut == clGeneral.enMutaam.enMutaam6.GetHashCode().ToString() ||
//                   oOvedYomAvodaDetails.sMutamut == clGeneral.enMutaam.enMutaam7.GetHashCode().ToString())
//                   && (oSidur.bSidurNotEmpty)); 

//        }

//        private void IsHmtanaTimeValid166(ref clSidur oSidur, ref clPeilut oPeilut, ref DataTable dtErrors)
//        {
//            //בדיקה ברמת פעילות
//            int iTimeInMinutes=0;
//            try
//            {
//                //אלמנט 724 , המתנה , אסור מעל x דקות (פרמטר 161 מכסימום זמן המתנה ידני ללא אישור מיוחד). מעבר לכך יש להעביר לסבב אישורים (28). יש מקרים בהם יש הודעה כללית של התנועה על עיכובים ביום ובאזור מסויים ולכן אין צורך להעביר כל כרטיס לאישור אלא הרשמת מאשרת את השגיאה.
//                if ((oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode())
//                    && (oPeilut.bElementHamtanaExists))
//                {
//                    iTimeInMinutes = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));

//                    if (iTimeInMinutes > oParam.iMaximumHmtanaTime)
//                    {
//                        drNew = dtErrors.NewRow();
//                        InsertErrorRow(oSidur, ref drNew, "עיכוב ארוך מעל המותר ", enErrors.errHmtanaTimeNotValid.GetHashCode());
//                        InsertPeilutErrorRow(oPeilut, ref drNew);
//                        dtErrors.Rows.Add(drNew);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsAvodatNahagutValid165(DataRow[] drSugSidur, ref clSidur oSidur, ref DataTable dtErrors)
//        {
//            bool bError = false;
//            //בדיקה ברמת סידור
//            try
//            {
//                //לעובד מאפיין זמן נסיעות  (51, 61) ועשה סידור נהגות.
//                if ((oMeafyeneyOved.Meafyen51Exists) || (oMeafyeneyOved.Meafyen61Exists))
//                {
//                    if (oSidur.bSidurMyuhad)
//                    {//סידור מיוחד
//                        if ((oSidur.bSectorAvodaExists) && (oSidur.sSectorAvoda == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString()))
//                        {
//                            bError = true;
//                        }
//                    }
//                    else
//                    {//סידור רגיל
//                        if (drSugSidur.Length > 0)
//                        {                            
//                            if (drSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString()) 
//                            {
//                                bError = true;
//                            }
//                        }
//                    }
//                    if (bError)
//                    {
//                        drNew = dtErrors.NewRow();
//                        InsertErrorRow(oSidur, ref drNew, "עובד עם מאפיין זמן נסיעה וביצע עבודת נהיגה ", enErrors.errAvodatNahagutNotValid.GetHashCode());
//                        dtErrors.Rows.Add(drNew);
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void IsTimeForPrepareMechineValid86(ref int iTotalTimePrepareMechineForSidur,
//                                                    ref int iTotalTimePrepareMechineForDay, 
//                                                    ref int iTotalTimePrepareMechineForOtherMechines,
//                                                    ref clSidur oSidur,
//                                                    ref clPeilut oPeilut, 
//                                                    ref DataTable dtErrors)
//        {
//            int iElementTime = 0;
//            int iElementType = 0;
//            bool bError = false;

//            //בדיקה ברמת פעילות            
//            try
//            {
//                //זמן הכנת מכונה באלמנט הוא מוגבל בזמן הזמן משתנה אם זו הכנת מכונה ראשונה (אלמנט 701xxx00) ביום או נוספת (711xxx00). זיהוי הכנה ראשונה/נוספת ביום - אם הסידור בו מדווחת הכנת מכונה התחיל עד 8 בבוקר (לא כולל) זוהי הכנת מכונה ראשונה. כל הכנת מכונה נוספת/מאוחרת משעה 8 בבוקר (כולל) נחשבת להכנת מכונה נוספת. זמן תקין להכנת מכונה  ראשונה הוא עד הערך בפרמטר 120 (זמן הכנת מכונה ראשונה), זמן תקין להכנת מכונה נוספת הוא עד הערך בפרמטר 121 (זמן הכנת מכונה נוספת). ביום עבודה יש מקסימום זמן לסה"כ הכנות מכונה נוספות, זמן תקין לפי פרמטר 122 (מכסימום יומי להכנות מכונה נוספות דקות). ביום עבודה יש מקסימום זמן לסה"כ הכנות מכונה (ראשונה ונוספות), זמן תקין לפי פרמטר 123 (מכסימום יומי להכנות מכונה  דקות).      מקסימום הכנות מכונה מותר בסידור         יש מקסימום למספר הכנות מכונה מותרות בסידור, נבדק לפי פרמטר 124 (מכסימום הכנות מכונה בסידור אחד), לא משנה מה הסוג שלהן.      
//                if ((oPeilut.iMakatType == clKavim.enMakatType.mElement.GetHashCode()) && (!String.IsNullOrEmpty(oPeilut.sShatYetzia)))
//                {
//                    iElementType = int.Parse(oPeilut.lMakatNesia.ToString().Substring(0,3));                    
//                    if ((iElementType == 701) || (iElementType == 711))
//                    {
//                        iElementTime = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));
//                        if ((iElementType == 701) && (int.Parse(oPeilut.sShatYetzia.Remove(2, 1)) < 8))
//                        {
//                            //מכונה ראשונה ביום- נשווה לפרמטר 120
//                            bError = (iElementTime > oParam.iPrepareFirstMechineMaxTime);
//                            //צבירת זמן כל המכונות ביום ראשונה ונוספות נשווה לפרמטר 123
//                            iTotalTimePrepareMechineForDay = iTotalTimePrepareMechineForDay + iElementTime;
//                            //צבירת זמן כלל המכונות לסידור נשווה מול פרמטר 124
//                            iTotalTimePrepareMechineForSidur = iTotalTimePrepareMechineForSidur + iElementTime;
//                        }
//                        else
//                        {
//                             //מכונות נוספות נשווה לפרמטר 121
//                             bError = (iElementTime > oParam.iPrepareOtherMechineMaxTime);
//                             //צבירת זמן כל המכונות ביום ראשונה ונוספות נשווה לפרמטר 123
//                             iTotalTimePrepareMechineForDay = iTotalTimePrepareMechineForDay + iElementTime;
//                             //צבירת זמן כלל המכונות לסידור נשווה מול פרמטר 124
//                             iTotalTimePrepareMechineForSidur = iTotalTimePrepareMechineForSidur + iElementTime;

//                             if (iElementType == 711)
//                             {
//                                 //צבירת זמן כל המכונות הנוספות - נשווה בסוף מול פרמטר 122
//                                 iTotalTimePrepareMechineForOtherMechines = iTotalTimePrepareMechineForOtherMechines + iElementTime;
//                             }
//                        }
//                        if (bError)
//                        {
//                            drNew = dtErrors.NewRow();
//                            InsertErrorRow(oSidur, ref drNew, "הכנת מכונה מעל המותר ", enErrors.errTimeForPrepareMechineNotValid.GetHashCode());
//                            InsertPeilutErrorRow(oPeilut, ref drNew);
//                            dtErrors.Rows.Add(drNew);
//                        }
//                    }
//                }
//            }
//            catch(Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void CheckPrepareMechineForSidurValidity86(ref clSidur oSidur, int iTotalTimePrepareMechineForSidur, ref DataTable dtErrors)
//        {
//            //נשווה את זמן כל האלמנטים של הכנת מכונה בסידור לפרמטר 124, אם עלה על המותר נעלה שגיאה
//            if (iTotalTimePrepareMechineForSidur > oParam.iPrepareAllMechineTotalMaxTimeForSidur)
//            {
//                drNew = dtErrors.NewRow();
//                InsertErrorRow(oSidur, ref drNew, "הכנת מכונה מעל המותר ", enErrors.errTimeForPrepareMechineNotValid.GetHashCode());                
//                dtErrors.Rows.Add(drNew);
//            }
//        }

//        private void CheckPrepareMechineForDayValidity86(int iMisparIshi, int iTotalTimePrepareMechineForDay, DateTime dCardDate, ref DataTable dtErrors)
//        {
//            //נשווה את זמן כל האלמנטים של הכנת מכונה ביום לפרמטר 123, אם עלה על המותר נעלה שגיאה
//            if (iTotalTimePrepareMechineForDay > oParam.iPrepareAllMechineTotalMaxTimeInDay)
//            {
//                drNew = dtErrors.NewRow();
//                drNew["mispar_ishi"] = iMisparIshi;
//                drNew["mispar_sidur"] = 0;
//                drNew["taarich"] = dCardDate.ToShortDateString();
//                drNew["error_desc"] = "הכנת מכונה מעל המותר ";
//                drNew["check_num"] = enErrors.errTimeForPrepareMechineNotValid.GetHashCode();
//                dtErrors.Rows.Add(drNew);
//            }
//        }

//        private void CheckPrepareMechineOtherElementForDayValidity86(int iMisparIshi, int iTotalTimePrepareMechineForOtherMechines, DateTime dCardDate, ref DataTable dtErrors)
//        {
//            //נשווה את זמן כל האלמנטים של הכנת מכונה נוספות ביום לפרמטר 122, אם עלה על המותר נעלה שגיאה
//            if (iTotalTimePrepareMechineForOtherMechines > oParam.iPrepareOtherMechineTotalMaxTime)
//            {
//                drNew = dtErrors.NewRow();
//                drNew["mispar_ishi"] = iMisparIshi;
//                drNew["mispar_sidur"] = 0;
//                drNew["taarich"] = dCardDate.ToShortDateString();
//                drNew["error_desc"] = "הכנת מכונה מעל המותר ";
//                drNew["check_num"] = enErrors.errTimeForPrepareMechineNotValid.GetHashCode();
//                dtErrors.Rows.Add(drNew);
//            }
//        }
//    }
//}
