using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.DAL;
using System.Configuration;
using System.Web;
using System.Web.UI.WebControls;

namespace KdsLibrary.BL
{
    public class clUtils
    {
        private static clUtils _Instance;

        public static clUtils GetInstance()
        {
            if (_Instance == null)
                _Instance = new clUtils();
            return _Instance;
        }

        public DataTable GetMaamad(int iKodHevra)
        {
            try
            {
                DataTable dt = new DataTable();
                clDal oDal = new clDal();

                if (iKodHevra == 0)
                {
                    oDal.AddParameter("p_kod_Hevra", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }
                else
                {
                    oDal.AddParameter("p_kod_Hevra", ParameterType.ntOracleInteger, iKodHevra, ParameterDir.pdInput);
                }
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetMaamad, ref dt);
                return dt;                
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetSnifAv(int iKodEzor)
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();
            try
            {
                //dt = GetCombo(clGeneral.cProGetSnifAv, "");
                if (iKodEzor == 0)
                {
                    oDal.AddParameter("p_kod_ezor", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }
                else
                {
                    oDal.AddParameter("p_kod_ezor", ParameterType.ntOracleInteger, iKodEzor, ParameterDir.pdInput);
                }
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetSnifAv, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetEzorim()
        {
            DataTable dt;
            try
            {
                dt=GetCombo(clGeneral.cProGetEzorim, "הכל");
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetCombo(string sSpName, string sDefaultText,string sDecription,string sCode)
        {
            DataTable dtCombo;
            dtCombo= CreateCombo(sSpName, sDefaultText, sDecription, sCode);
            return dtCombo;
        }
        
        public DataTable GetCombo(string sSpName, string sDefaultText)
         {
             DataTable dtCombo;
             dtCombo = CreateCombo(sSpName, sDefaultText, "Description", "Code");
             return dtCombo;        
        }


        public DataTable CreateCombo(string sSpName, string sDefaultText, string sDecription, string sCode)
        {
            clDal oDal = new clDal();
            try
            {
                DataTable dtCombo = new DataTable();
                DataRow dr;
                string sCacheKey;

                if (ConfigurationSettings.AppSettings["UsingCache"] == "1")
                {
                    // Use Cache
                    sCacheKey = sSpName;
                    try
                    {
                        dtCombo = (DataTable)(HttpContext.Current.Cache.Get(sCacheKey));
                    }
                    catch (Exception ex)
                    {
                        dtCombo.Dispose();
                        throw ex;
                    }

                    if (dtCombo == null)
                    {
                        dtCombo = new DataTable();
                        oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                        //oDal.AddParameter((New clParameter("p_Cur", clBaseComponent.ReservedTicketsType.ntOracleRefCursor, clParameter.ParameterDir.pdOutput, Nothing));
                        oDal.ExecuteSP(sSpName, ref dtCombo);
                        HttpContext.Current.Cache.Insert(sCacheKey, dtCombo, null, DateTime.MaxValue, TimeSpan.FromMinutes(int.Parse((ConfigurationSettings.AppSettings["CacheTimeOutMinutes"]))));
                        if (sDefaultText.Length > 0)
                        {
                            dr = dtCombo.NewRow();
                            dr[sDecription] = sDefaultText;
                            dr[sCode] = -1;
                            dtCombo.Rows.InsertAt(dr, 0);
                        }
                    }
                }
                else
                {
                    oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                    //clParams.Add(New clParameter("p_Cur", clBaseComponent.ReservedTicketsType.ntOracleRefCursor, clParameter.ParameterDir.pdOutput, Nothing))
                    oDal.ExecuteSP(sSpName, ref dtCombo);
                    if ((sDefaultText.Length) > 0)
                    {
                        dr = dtCombo.NewRow();
                        dr[sDecription] = sDefaultText;
                        dr[sCode] = -1;
                        dtCombo.Rows.InsertAt(dr, 0);
                    }
                }
                if ((dtCombo.Rows.Count > 0 && (sDefaultText.Length) > 0) && (int.Parse(dtCombo.Rows[0][sCode].ToString()) != -1))
                {
                    dr = dtCombo.NewRow();
                    dr[sDecription] = sDefaultText;
                    dr[sCode] = -1;
                    dtCombo.Rows.InsertAt(dr, 0);
                }

                return dtCombo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                // GeneralFunctions.Release(oDal);}
            }
        }

        public DataTable GetHodaotToProfil(int iKodProfil,int iKodMasach)
        {
            try
            {
                DataTable dt = new DataTable();
                clDal oDal = new clDal();

                oDal.AddParameter("p_kod_masach", ParameterType.ntOracleInteger, iKodMasach, ParameterDir.pdInput);
                oDal.AddParameter("p_kod_profil", ParameterType.ntOracleInteger, iKodProfil, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetHodaotToProfil, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetLogTahalich()
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();

            try
            {
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetLogTahalichim, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetCtbElementim()
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();

            try
            {
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetCtbElementim, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
       
        
        public DataTable GetCtbMutamut()
        {
            clDal oDal = new clDal();
            DataTable dtMutamut = new DataTable();
            try
            {  
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetMutamut, ref dtMutamut);

                return dtMutamut;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetLookUpTables()
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();

            try
            {   //מחזיר טבלאות פענוח:                 
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetLookUpTables, ref dt);

                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetCtbSibotLedivuchYadani()
        {
            clDal oDal = new clDal();
            DataTable dtSibotLedivuchYadani = new DataTable();
            try
            {
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetSibotLedivuchYadani, ref dtSibotLedivuchYadani);

                return dtSibotLedivuchYadani;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int  GetStatusIshurMaxLevel(int iMisparIshi, DateTime dDate, int iKodIshur)
        {
            clDal oDal = new clDal();           
            int iKodIshurStatus = 0;
            try
            {
                //מחזיר את סטטוס קוד האישור ברמה הגבוהה ביותר
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dDate, ParameterDir.pdInput);
                oDal.AddParameter("p_kod_ishur", ParameterType.ntOracleInteger, iKodIshur, ParameterDir.pdInput);
                oDal.AddParameter("p_kod_status", ParameterType.ntOracleInteger, iKodIshurStatus, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetStatusIshurMaxLevel);
                               
                iKodIshurStatus = Int32.Parse(oDal.GetValParam("p_kod_status"));

                return iKodIshurStatus;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CheckIshurToSidur(int iMisparIshi, DateTime dDate, int iKodIshur,int iMisparSidur,DateTime dShatHatchala)
        {
            clDal oDal = new clDal();
            int iValueIshur = 0;
            try
            {
                //מחזיר את סטטוס קוד האישור ברמה הגבוהה ביותר
                oDal.AddParameter("p_return_value", ParameterType.ntOracleInteger, null, ParameterDir.pdReturnValue);
                    
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dDate, ParameterDir.pdInput);
                oDal.AddParameter("p_kod_ishur", ParameterType.ntOracleInteger, iKodIshur, ParameterDir.pdInput);
               
               oDal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger, iMisparSidur, ParameterDir.pdInput);
               oDal.AddParameter("p_shat_hatchala", ParameterType.ntOracleDate, dShatHatchala, ParameterDir.pdInput);
                
                oDal.ExecuteSP(clGeneral.cProCheckIshur);

                iValueIshur = Int32.Parse(oDal.GetValParam("p_return_value"));

                return iValueIshur;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int CheckIshur(int iMisparIshi, DateTime dDate, int iKodIshur)
        {
            clDal oDal = new clDal();
            int iValueIshur = 0;
            try
            {
                //מחזיר את סטטוס קוד האישור ברמה הגבוהה ביותר
                oDal.AddParameter("p_return_value", ParameterType.ntOracleInteger, null, ParameterDir.pdReturnValue);

                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dDate, ParameterDir.pdInput);
                oDal.AddParameter("p_kod_ishur", ParameterType.ntOracleInteger, iKodIshur, ParameterDir.pdInput);
           
                oDal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
               oDal.AddParameter("p_shat_hatchala", ParameterType.ntOracleDate, null, ParameterDir.pdInput);

               oDal.ExecuteSP(clGeneral.cProCheckIshur);

                iValueIshur = Int32.Parse(oDal.GetValParam("p_return_value"));

                return iValueIshur;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int GetZmanNesia(int iMerkazErua, int iMikumYaad, DateTime dDate)
        {
            clDal oDal = new clDal();
            int iDakot = 0;
            try
            {
                //מחזיר את זמן נסיעה
                oDal.AddParameter("p_merkaz_erua", ParameterType.ntOracleInteger, iMerkazErua, ParameterDir.pdInput);
                oDal.AddParameter("p_mikum_yaad", ParameterType.ntOracleInteger, iMikumYaad, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dDate, ParameterDir.pdInput);
                oDal.AddParameter("p_dakot", ParameterType.ntOracleInteger, iDakot, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetZmanNesia);

                iDakot = Int32.Parse(oDal.GetValParam("p_dakot"));

                return iDakot;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetTbParametrim(int iKodParam, DateTime dTarich)
        {
            //  get   Erech_param  from tb_parametrim 

            int iErech_param = 0;

            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("p_Kod_Param", ParameterType.ntOracleInteger, iKodParam, ParameterDir.pdInput);
                oDal.AddParameter("p_tarich", ParameterType.ntOracleDate, dTarich, ParameterDir.pdInput);
                oDal.AddParameter("p_Erech_param", ParameterType.ntOracleInteger, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clGeneral.cproProGetTbParametrim);

                iErech_param = Int32.Parse(oDal.GetValParam("p_Erech_param"));

                return iErech_param;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public DataTable GetPremiotDetailsForCodes(string commaSeperatedPremiotCodes)
        {
            DataTable dt = new DataTable();

            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_premya_codes", ParameterType.ntOracleVarchar, commaSeperatedPremiotCodes, ParameterDir.pdInput);
                dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(clGeneral.cProGetPremiotDetails, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }
        public  DataTable GetPremiaYadanitForOved(int mispar_ishi, DateTime selectedMonth, int sug_premya)
        {
            DataTable dt = new DataTable();

            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, mispar_ishi, ParameterDir.pdInput);
                dal.AddParameter("p_chodesh", ParameterType.ntOracleDate, selectedMonth, ParameterDir.pdInput);
                dal.AddParameter("p_sug_premya", ParameterType.ntOracleInteger, sug_premya, ParameterDir.pdInput);
                dal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(clGeneral.cProGetPremiaYadanit, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        public DataTable GetOvdimForPremiaType(string premiaType, DateTime selectedMonth)
        {
            DataTable dt = new DataTable();

            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_kod_premia", ParameterType.ntOracleInteger, premiaType, ParameterDir.pdInput);
                dal.AddParameter("p_taarich", ParameterType.ntOracleDate, selectedMonth, ParameterDir.pdInput);
                dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(clGeneral.cProGetOvdimForPremia, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dt;
        }

        public DataTable GetOvdimForPremiotType(string MisparIshi ,string premiotType, string selectedMonth)
        {
            DataTable dt = new DataTable();
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleVarchar, MisparIshi, ParameterDir.pdInput);
                dal.AddParameter("p_kod_premiot", ParameterType.ntOracleVarchar, premiotType, ParameterDir.pdInput);
                dal.AddParameter("p_Period", ParameterType.ntOracleVarchar, selectedMonth, ParameterDir.pdInput);
                dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(clGeneral.cProGetOvdimForPremiot, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

       
        public DataTable GetOvdimLeRitza(int p_mis_ritza, string maamad,string isuk,string preFix )
        {
            DataTable dt = new DataTable();
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_mis_ritza", ParameterType.ntOracleInteger, p_mis_ritza, ParameterDir.pdInput);
                if (maamad != "-1") 
                    dal.AddParameter("p_maamad", ParameterType.ntOracleVarchar, maamad, ParameterDir.pdInput);
                else
                    dal.AddParameter("p_maamad", ParameterType.ntOracleVarchar, null, ParameterDir.pdInput);
                if (isuk != "")
                    dal.AddParameter("p_isuk", ParameterType.ntOracleVarchar, isuk, ParameterDir.pdInput);
                else
                    dal.AddParameter("p_isuk", ParameterType.ntOracleVarchar, null, ParameterDir.pdInput);

                if (preFix != "")
                    dal.AddParameter("p_preFix", ParameterType.ntOracleVarchar, preFix, ParameterDir.pdInput);
                else
                    dal.AddParameter("p_preFix", ParameterType.ntOracleVarchar, null, ParameterDir.pdInput);

                dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(clGeneral.cProGetOvdimLeritza, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable InitDtPremyotYadaniyot(int iMisparIshi, DateTime dTarMonth)
        {
            clDal oDal = new clDal();
            DataTable dtPremyot = new DataTable();
            try
            {   //ידניות מחזיר פרמיות:  
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_chodesh", ParameterType.ntOracleDate, dTarMonth, ParameterDir.pdInput);
                oDal.AddParameter("p_sug_premia", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);

                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetPremiaYadanit, ref dtPremyot);

                return dtPremyot;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public DataTable getMispareiRashamot(int p_kod_natun, string Erech,DateTime TaarichCA,  string preFix)
        {
            DataTable dt = new DataTable();
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_kod_Natun", ParameterType.ntOracleInteger, p_kod_natun, ParameterDir.pdInput);

                dal.AddParameter("p_Erech", ParameterType.ntOracleVarchar, Erech, ParameterDir.pdInput);
                dal.AddParameter("p_Taarich", ParameterType.ntOracleDate, TaarichCA, ParameterDir.pdInput);
                if (preFix != "")
                    dal.AddParameter("p_preFix", ParameterType.ntOracleVarchar, preFix, ParameterDir.pdInput);
                else
                    dal.AddParameter("p_preFix", ParameterType.ntOracleVarchar, null, ParameterDir.pdInput);

                dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(clGeneral.cProGetMispareyRashamot, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public DataTable GetElementsVeMeafyenim(string taarichCA)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {

                oDal.AddParameter("p_Taarich", ParameterType.ntOracleVarchar, taarichCA, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetElementsVeMeafyenim, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetMeafyenSidurByKodSidur(int Sidur, string taarich)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {
                oDal.AddParameter("P_Kod_Sidur", ParameterType.ntOracleInteger, Sidur, ParameterDir.pdInput);
                oDal.AddParameter("P_Taarich", ParameterType.ntOracleVarchar, taarich, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetMeafyeneySidur, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetTeurElements(string prefixText)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {
                oDal.AddParameter("p_Prefix", ParameterType.ntOracleVarchar, prefixText, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetTeurElements, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int GetKodElementByTeur(string teurElemnt)
        {
                clDal oDal = new clDal();
                int kod;
                try
                {                  
                    oDal.AddParameter("p_Desc", ParameterType.ntOracleVarchar, teurElemnt, ParameterDir.pdInput,50);
                    oDal.AddParameter("p_Kod", ParameterType.ntOracleVarchar, null, ParameterDir.pdOutput,10);
                    oDal.ExecuteSP(clGeneral.cProGetKodByElementDesc);

                    if (oDal.GetValParam("p_Kod") != "null")
                    {
                        kod = Int32.Parse(oDal.GetValParam("p_Kod"));
                        return kod;
                    }
                    return -1;
                   
                }
                catch (Exception ex)
                {
                    throw ex;
                }
        }

        public DataTable getTeureySidurimWhithOutList(string preFix, string kodMeafyenList, string misSidurList)
        {
            DataTable dt = new DataTable();
            try
            {
                clDal dal = new clDal();
           
                dal.AddParameter("p_preFix", ParameterType.ntOracleVarchar, preFix, ParameterDir.pdInput);
                dal.AddParameter("whithOutList", ParameterType.ntOracleVarchar, kodMeafyenList, ParameterDir.pdInput);
                dal.AddParameter("whithOutListMisSidur", ParameterType.ntOracleVarchar, misSidurList, ParameterDir.pdInput);
             //   dal.AddParameter("p_taarich", ParameterType.ntOracleVarchar, taarich, ParameterDir.pdInput);
              
                dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(clGeneral.cProTeurSidurWhithOutList, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }


        public int getKodSidurByTeur(string teur)
        {
            clDal oDal = new clDal();
            int kod;
            try
            {
                oDal.AddParameter("p_Desc", ParameterType.ntOracleVarchar, teur, ParameterDir.pdInput, 50);
                oDal.AddParameter("p_Kod", ParameterType.ntOracleVarchar, null, ParameterDir.pdOutput, 10);
                oDal.ExecuteSP(clGeneral.cProGetKodBySidurDesc);

                if (oDal.GetValParam("p_Kod") != "null")
                {
                    kod = Int32.Parse(oDal.GetValParam("p_Kod"));
                    return kod;
                }
                return -1;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getkodSidurimWhithOutList(string preFix, string kodMeafyenList, string misSidurList)
        {
            DataTable dt = new DataTable();
            try
            {
                clDal dal = new clDal();

                dal.AddParameter("p_preFix", ParameterType.ntOracleVarchar, preFix, ParameterDir.pdInput);
                dal.AddParameter("whithOutList", ParameterType.ntOracleVarchar, kodMeafyenList, ParameterDir.pdInput);
                dal.AddParameter("whithOutListMisSidur", ParameterType.ntOracleVarchar, misSidurList, ParameterDir.pdInput);
             //   dal.AddParameter("p_taarich", ParameterType.ntOracleVarchar, taarich, ParameterDir.pdInput);


                dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                dal.ExecuteSP(clGeneral.cProKodSidurWhithOutList, ref dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }

        public string getTeurSidurByKod(int kod)
        {
            clDal oDal = new clDal();
            string teur;
            try
            {

                oDal.AddParameter("p_Kod", ParameterType.ntOracleInteger, kod, ParameterDir.pdInput);
                oDal.AddParameter("p_Desc", ParameterType.ntOracleVarchar, null, ParameterDir.pdOutput, 70);

                oDal.ExecuteSP(clGeneral.cProGetSidurDescbyKod);

                if (oDal.GetValParam("p_Desc") != "null")
                {
                    teur =oDal.GetValParam("p_Desc").ToString();
                    return teur;
                }
                return "-1";

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DataTable getErechParamByKod(string kodList, string taarich)
        {
            clDal oDal = new clDal();
            DataTable dt = new DataTable();
            try
            {
            

                oDal.AddParameter("p_KodList", ParameterType.ntOracleVarchar, kodList, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleVarchar, taarich, ParameterDir.pdInput);

                oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetErechParameterByKod, ref dt);
          
                return dt;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable InitDtMeafyeneySugSidur(DateTime dTarMe, DateTime dTarAd)
        {
            clDal oDal = new clDal();
            DataTable dtMeafyeneySugSidur = new DataTable();
            try
            {   //מחזיר מאפייני סוג סידור:  
                oDal.AddParameter("p_tar_me", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput);
                oDal.AddParameter("p_tar_ad", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetMeafyeneySugSidur, ref dtMeafyeneySugSidur);

                return dtMeafyeneySugSidur;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Literal SetFixedHeaderGrid(String sNameDivGrid)
        {
            string sStyle;
            sStyle = "div#" + sNameDivGrid + " th {   " +
                       "top: expression(document.getElementById('" + sNameDivGrid + "').scrollTop-1); " +
                       "left: expression(parentNode.parentNode.parentNode.parentNode.parentNode.scrollLeft); " +
                       "position: relative; " +
                       "}";
            Literal litCss = new Literal();
            litCss.Text = "<style type=" + '"' + "text/css" + '"' + ">" + sStyle + "</style>";

            return litCss;
        }

        public DataTable GetSadotNosafimLesidur(int sidur,string listMeafyenim)
        {
            clDal oDal = new clDal();
            DataTable dtSadotLeSidur = new DataTable();
            try
            {  
                oDal.AddParameter("p_Sidur", ParameterType.ntOracleInteger, sidur, ParameterDir.pdInput);
                oDal.AddParameter("p_List_Meafyenim", ParameterType.ntOracleVarchar, listMeafyenim, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetSadotNosafomLeSidur, ref dtSadotLeSidur);

                return dtSadotLeSidur;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetSadotNosafimLePeilut()
        {
            clDal oDal = new clDal();
            DataTable dtSadotLeSidur = new DataTable();
            try
            {  
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetSadotNosafomLePeilut, ref dtSadotLeSidur);

                return dtSadotLeSidur;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DataTable GetSadotNosafimKayamim(int mispar_ishi, int mis_sidur, DateTime taarichCA, DateTime dShatHatchala)
        {
            clDal oDal = new clDal();
            DataTable dtSadotLeSidur = new DataTable();
            try
            {
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, mispar_ishi, ParameterDir.pdInput);
                oDal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger, mis_sidur, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, taarichCA, ParameterDir.pdInput);
                oDal.AddParameter("p_shat_hatchala", ParameterType.ntOracleDate, dShatHatchala, ParameterDir.pdInput);
                oDal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetSadotNosafomKayamim, ref dtSadotLeSidur);

                return dtSadotLeSidur;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        
        public DataTable GetDataToCmbBySPname(string SPname)
        {
            clDal oDal = new clDal();
            DataTable dtNetunim = new DataTable();

            try
            {

                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(SPname, ref dtNetunim);

                return dtNetunim;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable Get_Mispar_Musach_O_Machsan(string TaarichCA)
        {
           clDal oDal = new clDal();
           DataTable dtNetunim = new DataTable();

            try
            {

                oDal.AddParameter("p_Taarich", ParameterType.ntOracleVarchar, TaarichCA, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetMusach_O_Machsan, ref dtNetunim);

                return dtNetunim;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable GetSidurimWithMeafyenim(DateTime dTaarich)
        {
            clDal oDal = new clDal();
            DataTable dtNetunim = new DataTable();

            try
            {

                oDal.AddParameter("p_Taarich", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetCtbSidurimWithMeafyen, ref dtNetunim);

                return dtNetunim;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DataTable getAllElements(string prefixText)
        {
            clDal oDal = new clDal();
            DataTable dtNetunim = new DataTable();

            try
            {

                oDal.AddParameter("p_Prefix", ParameterType.ntOracleVarchar, prefixText, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetAllElementsKod, ref dtNetunim);

                return dtNetunim;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void SetDDLToolTip(DropDownList DDL)
        {
            if (DDL.SelectedItem!=null){
                DDL.ToolTip=DDL.SelectedItem.Text;
            }
        }
        public static void BindTooltip(DropDownList lc)
        {
            for (int i = 0; i < lc.Items.Count; i++)
            {
                lc.Items[i].Attributes.Add("title", lc.Items[i].Text);              
            }
        }
        public static int GetPakadId(DataTable dtPakadim, string sShemDb)
        {
            int iPakadId=0;
            DataRow[] dr;

            dr = dtPakadim.Select("shem_db='" + sShemDb + "'");
            if (dr.Length > 0)
            {
                iPakadId = int.Parse(dr[0]["pakad_id"].ToString());
            }
            return iPakadId;
        }


        public DataTable getSidurimMeyuchadim(DateTime dTarMe, DateTime dTarAd)
        {
            clDal oDal = new clDal();
            DataTable dtSidurimMeyuchadim = new DataTable();
            try
            {  
                oDal.AddParameter("p_tar_me", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput);
                oDal.AddParameter("p_tar_ad", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(clGeneral.cProGetSidurimMeyuchadim, ref dtSidurimMeyuchadim);

                return dtSidurimMeyuchadim;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    } 

}