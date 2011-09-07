using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.DAL;
using KdsLibrary.UDT;
using KdsLibrary.BL;
using KdsLibrary;


namespace KdsBatch
{
    public class clCalculation 
   {
       #region Definitions
           public enum TypeCalc
            { Batch = 1, OnLine = 2, Test=3 }

            private COLL_CHISHUV_SIDUR _collChishuvSidur;
            private COLL_CHISHUV_YOMI _collChishuvYomi;
            private COLL_CHISHUV_CHODESH _collChishuvChodesh;
       #endregion

        

       #region Methods
            //דרך תחקור וסיכום חודשי חישוב לעובד - main
            public void MainCalcOved(int iMisparIshi, long lBakashaId, DateTime dCalcMonth, int iTzuga, ref DataTable dtHeadrut, ref DataTable dtRechivimChodshiym, ref  DataTable dtRikuz1To10, ref DataTable dtRikuz11To20, ref  DataTable dtRikuz21To31, ref DataTable dtAllRikuz)
           {
               DateTime dTarMe,dTarAd;
               clUtils oUtils = new clUtils();
               try
               {
                   dTarMe = new DateTime(dCalcMonth.Year, dCalcMonth.Month, 1);
                   dTarAd = dTarMe.AddMonths(1).AddDays(-1);
                   clCalcData.DtSugeyYamimMeyuchadim = clGeneral.GetSugeyYamimMeyuchadim();
                   clCalcData.DtYamimMeyuchadim = clGeneral.GetYamimMeyuchadim();
                   clCalcData.DtMichsaYomit = GetMichsaYomitLechodesh(dTarMe, dTarAd);
                   clCalcData.DtMeafyeneySugSidur = oUtils.InitDtMeafyeneySugSidur(dTarMe, dTarAd);

                   if (clCalcData.ListParametersMonth == null)
                       clCalcData.InitListParamObject(dTarMe, dTarAd);

                   CalcOved(iMisparIshi, lBakashaId, dTarMe, dTarAd);

                   SaveChishuvTemp(iMisparIshi, dCalcMonth, iTzuga, ref  dtHeadrut, ref  dtRechivimChodshiym, ref   dtRikuz1To10, ref  dtRikuz11To20, ref   dtRikuz21To31, ref dtAllRikuz);

                   clCalcData.ListParametersMonth = null;
               }
               catch (Exception ex)
               {
                   clLogBakashot.InsertErrorToLog(lBakashaId, iMisparIshi, "E", 0, dCalcMonth, "MainCalc: " + ex.Message);
                   clCalcData.ListParametersMonth = null;
               }
           }

         //חישוב לאוכלוסית עובדים דרך batch
           public void MainCalc(long lBakashaId, DateTime dAdChodesh, string sMaamad, bool bRitzaGorefet, TypeCalc iTypeCalc)
           {

               DateTime dTarMe, dTarAd, dMeChodesh, dFrom;
               int iMisparIshi,i;
               DataTable dtOvdim, dtParametrim;
                int iStatus=0;
                iMisparIshi = 0;
                clUtils oUtils = new clUtils();
                try
               {
                   clLogBakashot.InsertErrorToLog(lBakashaId, "I", 0, "START");
                   clCalcData.DtSugeyYamimMeyuchadim = clGeneral.GetSugeyYamimMeyuchadim();
                   clCalcData.DtYamimMeyuchadim = clGeneral.GetYamimMeyuchadim();
                   dtParametrim = oUtils.getErechParamByKod("100", DateTime.Now.ToShortDateString());
                   dFrom = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(int.Parse(dtParametrim.Rows[0]["ERECH_PARAM"].ToString())*-1);
                   
                    clCalcData.DtParameters = oUtils.GetKdsParametrs();
                    for (dMeChodesh = dFrom; dMeChodesh <= dAdChodesh; dMeChodesh = dMeChodesh.AddMonths(1))
                   {
                       try
                       {
                               dtOvdim = GetOvdimLechishuv(dMeChodesh, sMaamad, bRitzaGorefet);
                                
                                dTarMe = dMeChodesh;
                               dTarAd = dTarMe.AddMonths(1).AddDays(-1);
                               clLogBakashot.InsertErrorToLog(lBakashaId, "I", 0,"Month:" + dMeChodesh + " Count:" + dtOvdim.Rows.Count);

                               clCalcData.DtMichsaYomit = GetMichsaYomitLechodesh(dTarMe, dTarAd);
                               clCalcData.DtMeafyeneySugSidur = oUtils.InitDtMeafyeneySugSidur(dTarMe, dTarAd);

                               if (clCalcData.ListParametersMonth == null)
                                   clCalcData.InitListParamObject(dTarMe, dTarAd);

                               for (i = 0; i <= dtOvdim.Rows.Count - 1; i++)
                               {
                                   try
                                   {
                                       iMisparIshi = int.Parse(dtOvdim.Rows[i]["mispar_ishi"].ToString());
                                       CalcOved(iMisparIshi, lBakashaId, dTarMe, dTarAd);
                                      
                                       //שמירת הנתונים רק אם התהליך התבצע ב-batch
                                       if (iTypeCalc == TypeCalc.Batch || iTypeCalc == TypeCalc.Test)
                                       {
                                           if (iTypeCalc == TypeCalc.Batch)
                                           {
                                                SaveSidurim(iMisparIshi, clCalcData.DtYemeyAvoda);
                                            }
                                           //שמירת נתוני החישוב לעובד
                                          SaveChishuv(lBakashaId, iMisparIshi);
                                       }
                                   }
                                   catch(Exception ex)
                                   {
                                       clLogBakashot.InsertErrorToLog(lBakashaId, iMisparIshi, "E", 0,dTarAd, "MainCalc: " + ex.Message);
                                   }
                               }
                               clCalcData.ListParametersMonth = null;
                           }
                        catch(Exception ex)
                       {
                           clLogBakashot.InsertErrorToLog(lBakashaId, "E", 0, "MainCalc: " + ex.Message);
                       }
                   }
                   iStatus = clGeneral.enStatusRequest.ToBeEnded.GetHashCode();

               }
               catch (Exception ex)
               {
                   iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
                   clLogBakashot.InsertErrorToLog(lBakashaId, "E", 0, "MainCalc: " + ex.Message);
                    throw ex;
               }
               finally
               {
                   clDefinitions.UpdateLogBakasha(lBakashaId,DateTime.Now,iStatus);
               }
           }
          
           public void PremiaCalc()
           {
               clBatch obatch= new clBatch();
               clUtils oUtils = new clUtils();
               long lBakashaId;
               int iMisparIshi = 0;
               int numFailed = 0;
               int numSucceed = 0;
               int seq = 0;
               seq = obatch.InsertProcessLog(98, 0, KdsLibrary.BL.RecordStatus.Wait, "PremiaCalc", 0);
               lBakashaId = clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.CalculationForPremiaPopulation, "KdsScheduler", -12);

               clCalcData.DtParameters = oUtils.GetKdsParametrs();
              
               DataTable dtPremia = GetPremiaCalcPopulation(lBakashaId);
               DateTime startMonth = DateTime.MinValue, tmpMonth, endMonth = DateTime.MinValue;
              
               clGeneral.enBatchExecutionStatus status = clGeneral.enBatchExecutionStatus.Succeeded;
               if (clCalcData.DtSugeyYamimMeyuchadim == null)
                   clCalcData.DtSugeyYamimMeyuchadim = clGeneral.GetSugeyYamimMeyuchadim();
               if (clCalcData.DtYamimMeyuchadim == null)
                   clCalcData.DtYamimMeyuchadim = clGeneral.GetYamimMeyuchadim();
               if (dtPremia != null)
               {
                  
                   clLogBakashot.InsertErrorToLog(lBakashaId, "I", 0, "Mispar Ovdim Lechishuv Premiyot:" + dtPremia.Rows.Count);
                   foreach (DataRow dr in dtPremia.Rows)
                   {
                       try
                       {
                           iMisparIshi = Convert.ToInt32(dr["mispar_ishi"]);
                           tmpMonth = Convert.ToDateTime(dr["Chodesh"]);
                           if (tmpMonth != startMonth)
                           {
                               startMonth = tmpMonth;
                               endMonth = GetEndOfMonth(startMonth);
                               if (clCalcData.ListParametersMonth == null)
                                   clCalcData.InitListParamObject(startMonth, endMonth);
                               clCalcData.DtMichsaYomit = GetMichsaYomitLechodesh(startMonth, endMonth);
                               clCalcData.DtMeafyeneySugSidur =
                                   oUtils.InitDtMeafyeneySugSidur(startMonth, endMonth);
                           }
                           CalcOved(iMisparIshi, lBakashaId, startMonth, endMonth);
                           
                           SaveSidurim(iMisparIshi, clCalcData.DtYemeyAvoda);
                         
                           //שמירת נתוני החישוב לעובד
                           SaveChishuv(lBakashaId, iMisparIshi);

                           UpdatePremiaBakashaID(iMisparIshi, lBakashaId, startMonth);
                           numSucceed += 1;
                       }
                       catch (Exception ex)
                       {
                           numFailed += 1;
                           clLogBakashot.SetError(lBakashaId, iMisparIshi, "E",
                               (int)clGeneral.enGeneralBatchType.CalculationForPremiaPopulation,
                               startMonth, ex.Message);
                           clLogBakashot.InsertErrorToLog();
                           status = clGeneral.enBatchExecutionStatus.PartialyFinished;
                       }
                   }
               }
               else status = clGeneral.enBatchExecutionStatus.Failed;
               KdsLibrary.clGeneral.CloseBatchRequest(lBakashaId, status);
               obatch.UpdateProcessLog(seq, KdsLibrary.BL.RecordStatus.Finish, "PremiaCalc NumRowsFailed=" + numFailed + " NumRowsSucceed=" + numSucceed, 0);
           }

           private DateTime GetEndOfMonth(DateTime date)
           {
               int year = date.Month == 12 ? date.Year + 1 : date.Year;
               int month = date.Month == 12 ? 1 : date.Month+1;
               return new DateTime(year, month, 1).AddDays(-1);
             // return new DateTime(year, (date.Month + 1) % 12, 1).AddDays(-1);
           }
           private void UpdatePremiaBakashaID(int iMisparIshi, long lBakashaId, DateTime startMonth)
           {
              
               clDal dal = new clDal();
               dal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, lBakashaId,
                   ParameterDir.pdInput);
               dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi,
                   ParameterDir.pdInput);
               dal.AddParameter("p_chodesh", ParameterType.ntOracleDate, startMonth,
                   ParameterDir.pdInput);
               dal.ExecuteSP(clDefinitions.cProUpdateChishuvPremia);
              
           }

           private DataTable GetPremiaCalcPopulation(long lBakashaId)
           {
               DataTable dt = new DataTable();
               clDal dal = new clDal();
               try
               {
                   dal.AddParameter("p_cur", ParameterType.ntOracleRefCursor, null,
                       ParameterDir.pdOutput);
                   dal.ExecuteSP(clDefinitions.cProGetPremiaOvdimLechishuv, ref dt);
               }
               catch (Exception ex)
               {
                   clLogBakashot.SetError(lBakashaId, null, "E",
                               (int)clGeneral.enGeneralBatchType.CalculationForPremiaPopulation,
                               DateTime.Now, ex.Message);
                   clLogBakashot.InsertErrorToLog();
                   dt = null;
               }
               return dt;
                   
           }


           public void MainCalcTest(DateTime dTarMe, int iMisparIshi)
           {
               long lBakashaId = 0;
               DateTime dTarAd;
              int iStatus = 0;
              clUtils oUtils = new clUtils();
               try
               {
                   clCalcData.DtSugeyYamimMeyuchadim = clGeneral.GetSugeyYamimMeyuchadim();
                   clCalcData.DtYamimMeyuchadim = clGeneral.GetYamimMeyuchadim();
                 

                   dTarAd = dTarMe.AddMonths(1).AddDays(-1);
                   clCalcData.DtParameters = oUtils.GetKdsParametrs();
                   if (clCalcData.ListParametersMonth == null)
                       clCalcData.InitListParamObject(dTarMe, dTarAd);

                   clCalcData.DtMichsaYomit = GetMichsaYomitLechodesh(dTarMe, dTarAd);
                   clCalcData.DtMeafyeneySugSidur = oUtils.InitDtMeafyeneySugSidur(dTarMe, dTarAd);

                    CalcOved(iMisparIshi, lBakashaId, dTarMe, dTarAd);

                    SaveSidurim(iMisparIshi, clCalcData.DtYemeyAvoda);
                       //שמירת נתוני החישוב לעובד
                    SaveChishuv(0, iMisparIshi);
                      iStatus = clGeneral.enStatusRequest.ToBeEnded.GetHashCode();
                     
               }
               catch (Exception ex)
               {
                   iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
                   clLogBakashot.InsertErrorToLog(lBakashaId, iMisparIshi, "E", 0, dTarMe, "MainCalcTest: " + ex.Message);
               }
               finally
               {
                   clCalcData.ListParametersMonth = null;
                   clDefinitions.UpdateLogBakasha(lBakashaId, DateTime.Now, iStatus);
               }
           }

           private void CalcOved(int iMisparIshi, long lBakashaId, DateTime dTarMe, DateTime dTarAd)
           {
               clCalcMonth oMonth;
               DataSet dsChishuv;
               clUtils oUtils = new clUtils();
              // clKavim oKavim = new clKavim();
               try
               {
                   clCalcData.ResetDataSet();
                 //  clCalcData.DtSugeySidur = oUtils.InitDtSugeySidur(dTarMe, dTarAd, iMisparIshi);
                   clCalcData.DtBusNumbers = null;
                   clCalcData.DtPeiluyotOved = oUtils.GetPeiluyLeovedForMonth( iMisparIshi,dTarMe, dTarAd);
                   oMonth = new clCalcMonth(iMisparIshi, lBakashaId);

                   dsChishuv = oMonth.CalcMonth(iMisparIshi, dTarMe, dTarAd);
                   
                   DataSetTurnIntoUdt(dsChishuv);
                   

               }
               catch (Exception ex)
               {
                   clLogBakashot.SetError(lBakashaId, iMisparIshi, "E", 0,null, "CalcOved: " + ex.Message);
                   throw ex;
               }
           }

           public DataSet CalcDayInMonth(int iMisparIshi, DateTime dCalcDay, long lBakashaId,out bool bStatus)
           {

               clCalcMonth oMonth;
               DateTime dTarMe, dTarAd;
               DataSet dsChishuv=null;
               clUtils oUtils = new clUtils();
               clKavim oKavim = new clKavim();
               bStatus = false;
               try
               {
                   dTarMe = dCalcDay;
                   dTarAd = dCalcDay;
                   clCalcData.ResetDataSet();

                   clCalcData.DtSugeyYamimMeyuchadim = clGeneral.GetSugeyYamimMeyuchadim();
                   clCalcData.DtYamimMeyuchadim = clGeneral.GetYamimMeyuchadim();
                   clCalcData.DtMichsaYomit = GetMichsaYomitLechodesh(dTarMe, dTarAd);
                   clCalcData.DtMeafyeneySugSidur = oUtils.InitDtMeafyeneySugSidur(dTarMe, dTarAd);
                  // clCalcData.DtSugeySidur = oUtils.InitDtSugeySidur(dTarMe, dTarAd, iMisparIshi);
                   clCalcData.DtPeiluyotOved = oUtils.GetPeiluyLeovedForMonth(iMisparIshi, dTarMe, dTarAd);
                   clCalcData.DtBusNumbers = null;
                   clCalcData.DtParameters = oUtils.GetKdsParametrs();
                   if (clCalcData.ListParametersMonth == null)
                       clCalcData.InitListParamObject(dTarMe, dTarAd);
                   oMonth = new clCalcMonth(iMisparIshi, lBakashaId);
                  
                   dsChishuv= oMonth.CalcMonth(iMisparIshi, dTarMe, dTarAd);

                   clCalcData.ListParametersMonth = null;
                   bStatus = true;
                 }
               catch (Exception ex)
               {
                   bStatus = false;
                   clLogBakashot.InsertErrorToLog(lBakashaId, iMisparIshi, "E", 0, null, "MainCalc: " + ex.Message);
                }

               return dsChishuv;
             }

           private void DataSetTurnIntoUdt(DataSet dsChishuv)
           {
               try
               {
                   int I;
                   DataRow drChishuv;
                   OBJ_CHISHUV_CHODESH objChsishuvChodesh;
                   OBJ_CHISHUV_YOMI objChsishuvYomi;
                   OBJ_CHISHUV_SIDUR objChsishuvSidur;

                   _collChishuvChodesh = new COLL_CHISHUV_CHODESH();
                   for (I = 0; I <= dsChishuv.Tables["CHISHUV_CHODESH"].Rows.Count - 1; I++)
                   {
                       drChishuv = dsChishuv.Tables["CHISHUV_CHODESH"].Rows[I];
                       objChsishuvChodesh = new OBJ_CHISHUV_CHODESH();
                       objChsishuvChodesh.BAKASHA_ID = long.Parse(drChishuv["BAKASHA_ID"].ToString());
                       objChsishuvChodesh.MISPAR_ISHI = int.Parse(drChishuv["MISPAR_ISHI"].ToString());
                       objChsishuvChodesh.TAARICH = DateTime.Parse(drChishuv["TAARICH"].ToString());
                       objChsishuvChodesh.KOD_RECHIV = int.Parse(drChishuv["KOD_RECHIV"].ToString());
                       objChsishuvChodesh.ERECH_RECHIV = float.Parse(drChishuv["ERECH_RECHIV"].ToString());
                       _collChishuvChodesh.Add(objChsishuvChodesh);
                   }
                  
                   _collChishuvYomi = new COLL_CHISHUV_YOMI();
                   for (I = 0; I <= dsChishuv.Tables["CHISHUV_YOM"].Rows.Count - 1; I++)
                   {
                       drChishuv = dsChishuv.Tables["CHISHUV_YOM"].Rows[I];
                       objChsishuvYomi = new OBJ_CHISHUV_YOMI();
                       objChsishuvYomi.BAKASHA_ID = long.Parse(drChishuv["BAKASHA_ID"].ToString());
                       objChsishuvYomi.MISPAR_ISHI = int.Parse(drChishuv["MISPAR_ISHI"].ToString());
                       objChsishuvYomi.TAARICH = DateTime.Parse(drChishuv["TAARICH"].ToString());
                       objChsishuvYomi.KOD_RECHIV = int.Parse(drChishuv["KOD_RECHIV"].ToString());
                       objChsishuvYomi.ERECH_RECHIV = float.Parse(drChishuv["ERECH_RECHIV"].ToString());
                       objChsishuvYomi.TKUFA = DateTime.Parse(drChishuv["TKUFA"].ToString());
                       _collChishuvYomi.Add(objChsishuvYomi);
                   }

                   _collChishuvSidur = new COLL_CHISHUV_SIDUR();
                   for (I = 0; I <= dsChishuv.Tables["CHISHUV_SIDUR"].Rows.Count - 1; I++)
                   {
                       drChishuv = dsChishuv.Tables["CHISHUV_SIDUR"].Rows[I];
                       objChsishuvSidur = new OBJ_CHISHUV_SIDUR();
                       objChsishuvSidur.BAKASHA_ID = long.Parse(drChishuv["BAKASHA_ID"].ToString());
                       objChsishuvSidur.MISPAR_ISHI = int.Parse(drChishuv["MISPAR_ISHI"].ToString());
                       objChsishuvSidur.TAARICH = DateTime.Parse(drChishuv["TAARICH"].ToString());
                       objChsishuvSidur.KOD_RECHIV = int.Parse(drChishuv["KOD_RECHIV"].ToString());
                       objChsishuvSidur.ERECH_RECHIV = float.Parse(drChishuv["ERECH_RECHIV"].ToString());
                       objChsishuvSidur.MISPAR_SIDUR = int.Parse(drChishuv["MISPAR_SIDUR"].ToString());
                       objChsishuvSidur.SHAT_HATCHALA = DateTime.Parse(drChishuv["SHAT_HATCHALA"].ToString());
                       _collChishuvSidur.Add(objChsishuvSidur);

                   }
               }
               catch (Exception ex)
               {
                   throw (ex);
               }
           }
                
           private void SaveSidurim(int iMisparIshi,DataTable dtYemeyAvoda)
           {
               clTxDal oDal = new clTxDal();
               DataRow[] drChanges;
               int J; 
               try
               {   //   שמירת סימון לא לתשלום
                   drChanges = dtYemeyAvoda.Select("lo_letashlum=1", "", DataViewRowState.ModifiedCurrent);
                   oDal.TxBegin();   
                   for (J = 0; J < drChanges.Length ; J++)
                   {
                       oDal.ClearCommand();
                       oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                       oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, drChanges[J]["taarich"], ParameterDir.pdInput);
                       oDal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger, drChanges[J]["mispar_sidur"], ParameterDir.pdInput);
                       oDal.AddParameter("p_shat_hatchala", ParameterType.ntOracleDate, drChanges[J]["shat_hatchala_sidur"], ParameterDir.pdInput);

                       oDal.ExecuteSP(clDefinitions.cProUpdateSidurimLoLetashlum);
                   }
                   oDal.TxCommit();
               }
               catch (Exception ex)
               {
                   oDal.TxRollBack();
                   throw ex;
               }
           }

           private void SaveChishuv(long lbakasha,int imispar)
           {
               clDal oDal = new clDal();

               try
               {   //   שמירת נתוני החישוב
                   oDal.AddParameter("p_coll_chishuv_chodesh", ParameterType.ntOracleArray, _collChishuvChodesh, ParameterDir.pdInput, "COLL_CHISHUV_CHODESH");
                   oDal.AddParameter("p_coll_chishuv_yomi", ParameterType.ntOracleArray, _collChishuvYomi, ParameterDir.pdInput, "COLL_CHISHUV_YOMI");
                   oDal.AddParameter("p_coll_chishuv_sidur", ParameterType.ntOracleArray, _collChishuvSidur, ParameterDir.pdInput, "COLL_CHISHUV_SIDUR");

                   oDal.ExecuteSP(clDefinitions.cProInsChishuv);
                   //SaveChodesh();
                                         
                   //SaveYom();
                                         
                   //SaveSidur();
                }
               catch (Exception ex)
               {
                   throw ex;
               }
           }

           private void SaveChodesh()
           {
               clDal oDal = new clDal();

               try
               {   //   שמירת נתוני החישוב
                   oDal.AddParameter("p_coll_chishuv_chodesh", ParameterType.ntOracleArray, _collChishuvChodesh, ParameterDir.pdInput, "COLL_CHISHUV_CHODESH");

                   oDal.ExecuteSP("pkg_calc.pro_ins_chishuv_codesh_ovdim");

               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }

           private void SaveYom()
           {
               clDal oDal = new clDal();

               try
               {   //   שמירת נתוני החישוב
                    oDal.AddParameter("p_coll_chishuv_yomi", ParameterType.ntOracleArray, _collChishuvYomi, ParameterDir.pdInput, "COLL_CHISHUV_YOMI");

                    oDal.ExecuteSP("pkg_calc.pro_ins_chishuv_yomi_ovdim");

               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }

           private void SaveSidur()
           {
               clDal oDal = new clDal();

               try
               {   //   שמירת נתוני החישוב
                   oDal.AddParameter("p_coll_chishuv_sidur", ParameterType.ntOracleArray, _collChishuvSidur, ParameterDir.pdInput, "COLL_CHISHUV_SIDUR");

                   oDal.ExecuteSP("pkg_calc.pro_ins_chishuv_sidur_ovdim");

               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }

           private void SaveChishuvTemp(int iMisparIshi, DateTime dCalcMonth,int iTzuga,ref DataTable dtHeadrut ,ref DataTable dtRechivimChodshiym,ref  DataTable dtRikuz1To10, ref DataTable dtRikuz11To20, ref  DataTable dtRikuz21To31, ref DataTable dtAllRikuz)
           {
               clTxDal oDal = new clTxDal();
               DataTable dt = new DataTable();
               float fErechRechiv45 = 0;
               try
               {   //   שמירת נתוני החישוב
                   oDal.TxBegin();
                   oDal.AddParameter("p_coll_chishuv_chodesh", ParameterType.ntOracleArray, _collChishuvChodesh, ParameterDir.pdInput, "COLL_CHISHUV_CHODESH");
                   oDal.AddParameter("p_coll_chishuv_yomi", ParameterType.ntOracleArray, _collChishuvYomi, ParameterDir.pdInput, "COLL_CHISHUV_YOMI");
                   oDal.ExecuteSP(clDefinitions.cProInsChishuvTemp);

                   clOvdim oOvdim = new clOvdim();
                   oDal.ClearCommand();
                   dtHeadrut = oOvdim.GetPirteyHeadrutTemp(ref oDal, iMisparIshi, dCalcMonth, 0);
                   oDal.ClearCommand();

                   dtRechivimChodshiym = oOvdim.GetRechivimChodshiyimTemp(ref oDal, iMisparIshi, dCalcMonth, 0, iTzuga);
                   oDal.ClearCommand();

                   fErechRechiv45=clCalcData.GetSumErechRechiv(dtRechivimChodshiym.Compute("sum(erech_rechiv)", "kod_rechiv=" + clGeneral.enRechivim.SachGmulChisachon.GetHashCode().ToString()));
                   oOvdim.GetRikuzChodshiTemp(ref oDal, iMisparIshi, dCalcMonth, 0, iTzuga,fErechRechiv45, ref  dtRikuz1To10, ref  dtRikuz11To20, ref  dtRikuz21To31, ref  dtAllRikuz);

                   oDal.TxCommit();
               }
               catch (Exception ex)
               {
                   oDal.TxRollBack();
                   throw ex;
               }
             
           }

           private DataTable GetMichsaYomitLechodesh(DateTime dTarMe, DateTime dTarAd)
           {
               DataTable dt = new DataTable();
               clDal oDal = new clDal();

               try
               {
                   oDal.AddParameter("p_tar_me", ParameterType.ntOracleDate, dTarMe, ParameterDir.pdInput);
                   oDal.AddParameter("p_tar_ad", ParameterType.ntOracleDate, dTarAd, ParameterDir.pdInput);
                   oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                   oDal.ExecuteSP(clDefinitions.cProGetMichsaYomit, ref dt);
                   return dt;
               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }

         
           private DataTable GetOvdimLechishuv(DateTime dChodesh,string sMaamad,bool bRitzaGorefet)
           {
               DataTable dt = new DataTable();
               clDal oDal = new clDal();

               try
               {
                   oDal.AddParameter("p_codesh", ParameterType.ntOracleDate, dChodesh, ParameterDir.pdInput);
                   if (sMaamad.IndexOf(",") > 0)
                   {
                       oDal.AddParameter("p_maamad", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                   }
                   else
                   {
                       oDal.AddParameter("p_maamad", ParameterType.ntOracleInteger, sMaamad, ParameterDir.pdInput);
                   }
                   oDal.AddParameter("p_ritza_gorefet", ParameterType.ntOracleInteger, bRitzaGorefet.GetHashCode(), ParameterDir.pdInput);
                   oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                   oDal.ExecuteSP(clDefinitions.cProGetOvdimLechishuv, ref dt);
                   return dt;
               }
               catch (Exception ex)
               {
                   throw ex;
               }
           }

         
       #endregion

          
    }
}
