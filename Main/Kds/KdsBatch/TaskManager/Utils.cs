using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;
using KdsLibrary.BL;
using System.Configuration;
using System.Data;
using KdsLibrary.TaskManager;
using System.IO;

namespace KdsBatch.TaskManager
{
	public class Utils
	{
		public void RunShguimOfSdrn()
		{
		   // clBatch oBatch = new clBatch();
			clUtils oUtils = new clUtils();
			long lRequestNum = 0;
			try
			{
				KdsServiceProxy.BatchServiceClient client = new KdsServiceProxy.BatchServiceClient();
				lRequestNum = clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.InputDataAndErrorsFromInputProcess, "RunShguimOfSdrn", -12);
				client.ShinuyimVeShguimBatch(lRequestNum, DateTime.Now.AddDays(-1), clGeneral.enCalcType.ShinuyimVeShguyim, clGeneral.BatchExecutionType.All);
			   // oUtils.RunSinuyimVeShguimBatch(lRequestNum, DateTime.Now.AddDays(-1), clGeneral.enCalcType.ShinuyimVeShguyim, clGeneral.BatchExecutionType.All);
			   // KdsBatch.clBatchFactory.ExecuteInputDataAndErrors(clGeneral.BatchRequestSource.ImportProcess, clGeneral.BatchExecutionType.All, DateTime.Now.AddDays(-1), lRequestNum);
				
			}
			catch (Exception ex)
			{
				throw new Exception("RunShguimOfSdrn:" + ex.Message);
			}
		}

        public void RunShguimOfRetroSpectSdrn(DateTime dTaarich)
        {
            // clBatch oBatch = new clBatch();
            clUtils oUtils = new clUtils();
            long lRequestNum = 0;
            try
            {
                KdsServiceProxy.BatchServiceClient client = new KdsServiceProxy.BatchServiceClient();
                lRequestNum = clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.InputDataAndErrorsFromInputProcess, "RunShguimOfRetroSpectSdrn", -12);
                client.ShinuyimVeShguimBatch(lRequestNum, dTaarich, clGeneral.enCalcType.ShinuyimVeShguyim, clGeneral.BatchExecutionType.All);
               
            }
            catch (Exception ex)
            {
                throw new Exception("RunShguimOfSdrn:" + ex.Message);
            }
        }
		public void RunShguimOfPremiyotMusachim()
		{
			// clBatch oBatch = new clBatch();
		   // clUtils oUtils = new clUtils();
			long lRequestNum = 0;
			try
			{
				KdsServiceProxy.BatchServiceClient client = new KdsServiceProxy.BatchServiceClient();
                lRequestNum = clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.InputDataAndErrorsFromInputProcess, "RunShguimOfPremiyotMusachim&NihulTnua&Yadaniyot", -12);
				client.ShinuyimVeShguimBatch(lRequestNum, DateTime.Now, clGeneral.enCalcType.ShinuyimVeSghuimPremiot, clGeneral.BatchExecutionType.All);
			   // oUtils.RunSinuyimVeShguimBatch(lRequestNum, DateTime.Now, clGeneral.enCalcType.ShinuyimVeSghuimPremiot, clGeneral.BatchExecutionType.All);
				// KdsBatch.clBatchFactory.ExecuteInputDataAndErrors(clGeneral.BatchRequestSource.ImportProcess, clGeneral.BatchExecutionType.All, DateTime.Now.AddDays(-1), lRequestNum);

			}
			catch (Exception ex)
			{
                throw new Exception("RunShguimOfPremiyotMusachim&NihulTnua&Yadaniyot:" + ex.Message);
			}
		}
		public void RunCalcPremiyotMusachim()
		{
			MainCalc oCalc;// = new MainCalc(
			clBatch oBatch = new clBatch();
			long lRequestNum = 0;
			try
			{
				KdsServiceProxy.BatchServiceClient client = new KdsServiceProxy.BatchServiceClient();
                lRequestNum = clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.CalculationForPremiaPopulation, "RunCalcPremiyotMusachim&NihulTnua&Yadaniyot", -12);
				client.CalcBatchPremiyot(lRequestNum);
			  
				//lRequestNum = clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.CalculationForPremiaPopulation, "RunCalcPremiyotMusachim", -12);
				//oCalc = new MainCalc(lRequestNum, 1);
				//oCalc.PremiaCalc();//KdsBatch.clBatchFactory.ExecuteInputDataAndErrors(KdsBatch.BatchRequestSource.ImportProcess, KdsBatch.BatchExecutionType.All, DateTime.Now.AddDays(-1), lRequestNum);
			}
			catch (Exception ex)
			{
				throw new Exception("RunCalcPremiyotMusachim&NihulTnua&Yadaniyot:" + ex.Message);
			}
		}
		public void RunIshurimOfSdrn()
		{
			try
			{
				string Environment = ConfigurationSettings.AppSettings["Environment"];
				KdsWorkFlow.Approvals.ApprovalFactory.ApprovalsEndOfDayProcess(DateTime.Now.AddDays(-1), (Environment == "Production"));
			}
			catch (Exception ex)
			{
				throw new Exception("RunIshurimOfSdrn :" + ex.Message);
			}
		}
		public void ShguimHrChanges()
		{
			int iSeqThreadHr, iSeqNum, num;
			DateTime dTaarich;
			clBatch oBatch = new clBatch();
			clUtils oUtils = new clUtils();
			try
			{
				KdsServiceProxy.BatchServiceClient client = new KdsServiceProxy.BatchServiceClient();
				iSeqThreadHr = oBatch.InsertProcessLog(8, 3, KdsLibrary.BL.RecordStatus.Wait, "start RunThreadHrChainges", 0);
				//(0,0)=no record at all ->run
				if (!oBatch.CheckViewEmpty("NEW_MATZAV_OVDIM") && !oBatch.CheckViewEmpty("NEW_PIRTEY_OVDIM") &&
					!oBatch.CheckViewEmpty("NEW_MEAFYENIM_OVDIM") && !oBatch.CheckViewEmpty("NEW_BREROT_MECHDAL_MEAFYENIM"))
				{
					//num = oBatch.GetNumChangesHrToShguim();
					//if ((num < 50000))
					//{
						long lRequestNum = 0;
						iSeqNum = oBatch.InsertProcessLog(8, 4, KdsLibrary.BL.RecordStatus.Wait, "before OpenBatchRequest hr", 0);
						//'**KdsWriteProcessLog(8, 3, 1, "before OpenBatchRequest")
						lRequestNum = clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.InputDataAndErrorsFromInputProcess, "ShguimHrChanges", -12);
						dTaarich = DateTime.Now.AddDays(-1);
						oBatch.UpdateProcessLog(iSeqNum, KdsLibrary.BL.RecordStatus.Finish, "after OpenBatchRequest hr", 0);
						//'** KdsWriteProcessLog(8, 3, 1, "after OpenBatchRequest before shguyim")
						iSeqNum = oBatch.InsertProcessLog(8, 4, KdsLibrary.BL.RecordStatus.Wait, "before shguyim hr", 0);
						client.ShinuyimVeShguimBatch(lRequestNum, dTaarich, clGeneral.enCalcType.ShinuyimVeSghuimHR, clGeneral.BatchExecutionType.All);
						//oUtils.RunSinuyimVeShguimBatch(lRequestNum, dTaarich, clGeneral.enCalcType.ShinuyimVeSghuimHR, clGeneral.BatchExecutionType.All);
						// KdsBatch.clBatchFactory.ExecuteInputDataAndErrors(clGeneral.BatchRequestSource.ImportProcessForChangesInHR, clGeneral.BatchExecutionType.All, dTaarich, lRequestNum);
						oBatch.UpdateProcessLog(iSeqNum, KdsLibrary.BL.RecordStatus.Finish, "after shguyim from hr", 0);
						oBatch.UpdateProcessLog(iSeqThreadHr, KdsLibrary.BL.RecordStatus.Finish, "end RunThreadHrChainges", 0);
						//'**KdsWriteProcessLog(8, 3, 2, "after shguyim from hr")
					//}
					//else
					//{
					//    oBatch.UpdateProcessLog(iSeqThreadHr, KdsLibrary.BL.RecordStatus.PartialFinish, "ThreadHrChainges did not run.a lot of mispar_ishi: " + num.ToString(), 0);
					//    //'**  KdsWriteProcessLog(8, 3, 4, "ThreadHrChainges did not run.a lot of mispar_ishi: " & num.ToString())
					//    throw new Exception("ThreadHrChainges didn't run because a lot of records (" + num.ToString() + " workers)");
					//}
				}
				else
				{
					oBatch.UpdateProcessLog(iSeqThreadHr, KdsLibrary.BL.RecordStatus.PartialFinish, "ThreadHrChainges did not run: view Empty", 0);
				}
			}
			catch
			{
				throw;
			}
		}

		public void RunShguimLechishuv()
		{
			clBatchManager oBatchManager = new clBatchManager();
			DataTable dtOvdim = new DataTable();
			clCalcDal oCalcDal = new clCalcDal();
			clBatch oBatch = new clBatch();
			int lRequestNum = 0;
			int numFaild=0;
			int numSucceeded = 0;
			bool bInpuDataResult;
			try
			{
				lRequestNum = oBatch.InsertProcessLog(77, 0, KdsLibrary.BL.RecordStatus.Wait, "start RunShguimLechishuv", 0);
				dtOvdim = oCalcDal.GetOvdimLeRizatShguim();
				for (int i = 0; i <dtOvdim.Rows.Count; i++)
				{
					try
					{
						bInpuDataResult = true;
						bInpuDataResult = oBatchManager.MainInputData(int.Parse(dtOvdim.Rows[i]["MISPAR_ISHI"].ToString()), DateTime.Parse(dtOvdim.Rows[i]["TAARICH"].ToString()));

						if (bInpuDataResult)
						{
							bInpuDataResult = oBatchManager.MainOvedErrors(int.Parse(dtOvdim.Rows[i]["MISPAR_ISHI"].ToString()), DateTime.Parse(dtOvdim.Rows[i]["TAARICH"].ToString()));
							numSucceeded += 1;
						}
						else
						{
							numFaild += 1;
						}
					}
					catch (Exception ex)
					{
						numFaild += 1;
					}
				}
				oBatch.UpdateProcessLog(lRequestNum, KdsLibrary.BL.RecordStatus.Finish, "end RunShguimLechishuv: Total Rows=" + dtOvdim.Rows.Count + "; numFaild=" + numFaild + ";  numSucceeded=" + numSucceeded, 0);
			}
			catch (Exception ex)
			{
				throw new Exception("RunShguimLechishuv:" + ex.Message);
			}
			finally {
			   oBatchManager = null;
			   dtOvdim =null;
			   oCalcDal =null;
			   oBatch =null;
			}
		}

		public void RefreshKnisot(DateTime p_date)
		{
			clTkinutMakatim objMakat = new clTkinutMakatim();
			KdsLibrary.TaskManager.Utils oUtilsTask = new KdsLibrary.TaskManager.Utils();
			try
			{
				if (p_date.ToShortDateString() != "01/01/2000")
				{
					if (p_date.ToShortDateString() != DateTime.MinValue.ToShortDateString())
					{
						oUtilsTask.SendNotice(15, 1, "Update Knisot For Taarich=" + p_date.ToShortDateString());
						objMakat.RunRefreshKnisot(p_date);
					}
					else
						objMakat.RunRefreshKnisot(DateTime.Now.AddDays(-1).Date);
				}
			}
			catch (Exception ex)
			{
				clGeneral.LogError(ex);
			}
		}

        public void CreateNetuneyPremiyot()
        {
            clBatch oBatch = new clBatch();
            premyot.wsPremyot wsPremyot = new premyot.wsPremyot();
            long lRequestNum = 0;
            try
            {

                lRequestNum = clGeneral.OpenBatchRequest(clGeneral.enGeneralBatchType.LoadNetuneyMeshekForPremyot, "RunLoadNetuneyMeshekForPremyot", -12);
                wsPremyot.UseDefaultCredentials = false;
                wsPremyot.Credentials = new System.Net.NetworkCredential(ConfigurationSettings.AppSettings["RSUserName"], ConfigurationSettings.AppSettings["RSPassword"], ConfigurationSettings.AppSettings["RSDomain"]);

                wsPremyot.MeshekDataInputCompleted += (sender, args) =>  CallbackCreateNetuneyPremiyot(sender, args, lRequestNum);

                wsPremyot.MeshekDataInputAsync();

            }
            catch (Exception ex)
            {
                if (lRequestNum > 0)
                {
                    clLogBakashot.InsertErrorToLog(lRequestNum, "I", 0, ex.Message);
                    clGeneral.CloseBatchRequest(lRequestNum, clGeneral.enBatchExecutionStatus.Failed);
                }
                else clGeneral.LogError(ex);
                throw (ex);
            }
			
        }
        private void CallbackCreateNetuneyPremiyot(object sender, KdsBatch.premyot.MeshekDataInputCompletedEventArgs e, long lRequestNum)
        {
            clLogBakashot.InsertErrorToLog(lRequestNum, "I", 0, "יצירת נתוני פרמיות הסתיימה");
            bool bSuccess = false;
            clBatch oBatch = new clBatch();
            premyot.wsPremyot wsPremyot = (premyot.wsPremyot)sender;

            try
            {
                if (null == e.Error)
                {
                    bSuccess = e.Result;
                    if (bSuccess)
                        clGeneral.CloseBatchRequest(lRequestNum, clGeneral.enBatchExecutionStatus.Succeeded);
                    else
                    {
                        clLogBakashot.InsertErrorToLog(lRequestNum, "I", 0, "הפעולה נכשלה - בדוק רשומות בקובץ לוג");
                        clGeneral.CloseBatchRequest(lRequestNum, clGeneral.enBatchExecutionStatus.Failed);
                    }
                }
                else
                {
                    wsPremyot.Abort();
                    clLogBakashot.InsertErrorToLog(lRequestNum, "I", 0, e.Error.Message);
                    clGeneral.CloseBatchRequest(lRequestNum, clGeneral.enBatchExecutionStatus.Failed);
                }
            }
            catch (Exception ex)
            {
                wsPremyot.Abort();
                if (lRequestNum > 0)
                {
                    clLogBakashot.InsertErrorToLog(lRequestNum, "I", 0, ex.Message);
                    clGeneral.CloseBatchRequest(lRequestNum, clGeneral.enBatchExecutionStatus.Failed);
                }
                else clGeneral.LogError(ex);
                throw (ex);
            }
            finally
            {
                wsPremyot.Dispose();
            }
        }

		public void RunCalculationPremyot()
		{
			clBatch oBatch = new clBatch();
			premyot.wsPremyot wsPremyot = new premyot.wsPremyot();
			long lRequestNum = 0;
			try
			{
				
				lRequestNum = clGeneral.OpenBatchRequest(clGeneral.enGeneralBatchType.ExecutePremiaCalculationMacro, "RunCalcPremyotNihulTnua", -12);
				wsPremyot.UseDefaultCredentials = false;
				wsPremyot.Credentials = new System.Net.NetworkCredential(ConfigurationSettings.AppSettings["RSUserName"], ConfigurationSettings.AppSettings["RSPassword"], ConfigurationSettings.AppSettings["RSDomain"]);
				
				 //bSuccess = wsPremyot.CalcPremyotNihulTnua();
			  
				 wsPremyot.CalcPremyotNihulTnuaCompleted += (sender, args) => CallbackCalculPremyot(sender, args, lRequestNum);
   
				wsPremyot.CalcPremyotNihulTnuaAsync();
	
			 }
			catch (Exception ex)
			{
				if (lRequestNum > 0)
				{
					clLogBakashot.InsertErrorToLog(lRequestNum, "I", 0, ex.Message);
					clGeneral.CloseBatchRequest(lRequestNum, clGeneral.enBatchExecutionStatus.Failed);
				}
				else clGeneral.LogError(ex);
				throw (ex);
			}
			
		}

		private void CallbackCalculPremyot(object sender,KdsBatch.premyot.CalcPremyotNihulTnuaCompletedEventArgs e,long lRequestNum)
		{
            clLogBakashot.InsertErrorToLog(lRequestNum, "I", 0, "חישוב הפרמיות הסתיים");
			bool bSuccess = false;
			clBatch oBatch = new clBatch();
			premyot.wsPremyot wsPremyot = (premyot.wsPremyot)sender;

            try
            {
                if (null == e.Error)
                {
                     bSuccess = e.Result;
                    if (bSuccess)
                        clGeneral.CloseBatchRequest(lRequestNum, clGeneral.enBatchExecutionStatus.Succeeded);
                    else
                    {
                        clLogBakashot.InsertErrorToLog(lRequestNum, "I", 0, "הפעולה נכשלה - בדוק רשומות בקובץ לוג");
                        clGeneral.CloseBatchRequest(lRequestNum, clGeneral.enBatchExecutionStatus.Failed);
                    }
                }
                else
                {
                    wsPremyot.Abort();
                    clLogBakashot.InsertErrorToLog(lRequestNum, "I", 0, e.Error.Message);
                    clGeneral.CloseBatchRequest(lRequestNum, clGeneral.enBatchExecutionStatus.Failed);
                }
            }
            catch (Exception ex)
            {
                wsPremyot.Abort();
                if (lRequestNum > 0)
                {
                    clLogBakashot.InsertErrorToLog(lRequestNum, "I", 0, ex.Message);
                    clGeneral.CloseBatchRequest(lRequestNum, clGeneral.enBatchExecutionStatus.Failed);
                }
                else clGeneral.LogError(ex);
                throw (ex);
            }
			finally
			{
				wsPremyot.Dispose();
			}
		}

		public void RunRetroSpectSDRN()
		{
            int Num = 45, iStatus = 0;
			DataTable sdrnDt = new DataTable();
            long lRequestNum = 0;
			clUtils oUtils = new clUtils();
			DateTime Taarich = DateTime.Now;
			clTaskManager oTask = new clTaskManager();
			KdsLibrary.TaskManager.Utils oUtilsTask = new KdsLibrary.TaskManager.Utils();
            try
            {
                lRequestNum = clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.RetroSpectSDRN, "RunRetroSpectSDRN", -12);
               // iStatus = clGeneral.enStatusRequest.InProcess.GetHashCode();
                Num = oUtils.GetTbParametrim(252, DateTime.Today);
                Taarich = Taarich.AddDays(-Num);

                while (Taarich <= DateTime.Now.AddDays(-2))
                {
                    sdrnDt = oTask.GetStausSdrn(Taarich.ToString("yyyyMMdd"));
                    if (sdrnDt.Rows.Count == 0)
                    {
                        oTask.RunSdrnWithDate(Taarich.ToString("yyyyMMdd"));
                        CheckStatusAgain(Taarich,"RunRetroSpectSDRN");
                    }
                    else
                    {
                        if (sdrnDt.Rows[0]["STATUS"].ToString() == "1" || sdrnDt.Rows[0]["STATUS"].ToString() == "2")
                        {
                            CompleteSdrn(Taarich);
                            oUtilsTask.SendNotice(4, 11, "RunRetroSpectSDRN: status sdrn=" + sdrnDt.Rows[0]["STATUS"].ToString() + " , 'RunRetrospectSdrn' + 'RefreshKnisot' run to date=" + Taarich.ToShortDateString());
                        }
                        else if (sdrnDt.Rows[0]["STATUS"].ToString() == "")
                        {
                            oTask.RunSdrnWithDate(Taarich.ToString("yyyyMMdd"));
                            CheckStatusAgain(Taarich,"RunRetroSpectSDRN");
                          //  oUtilsTask.SendNotice(4, 11, "RunRetroSpectSDRN: status sdrn=null , 'RunSdrnWithDate' run to date=" + Taarich.ToShortDateString());
                        }
                    }
                    Taarich = Taarich.AddDays(1);
                }
                iStatus = clGeneral.enStatusRequest.ToBeEnded.GetHashCode();
            }
            catch (Exception ex)
            {
                iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
                clGeneral.LogError(ex);
            }
            finally
            {
                clDefinitions.UpdateLogBakasha(lRequestNum, DateTime.Now, iStatus);
            }
		}

        private void CompleteSdrn(DateTime Taarich)
        {
            clTaskManager oTask = new clTaskManager();
            try
            {
                oTask.RunRetrospectSdrn(Taarich.ToString("yyyyMMdd"));
                RefreshKnisot(Taarich);
                RunShguimOfRetroSpectSdrn(Taarich);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void CheckStatusAgain(DateTime Taarich, string teur)
        {
            DataTable sdrnDt = new DataTable();
            KdsLibrary.TaskManager.Utils oUtilsTask = new KdsLibrary.TaskManager.Utils();
            clTaskManager oTask = new clTaskManager();
            try
            {
                sdrnDt = oTask.GetStausSdrn(Taarich.ToString("yyyyMMdd"));
                if (sdrnDt.Rows.Count == 0)
                {
                    oUtilsTask.SendNotice(4, 11, teur + ": status sdrn rows=0 after 'RunSdrnWithDate' to date=" + Taarich.ToShortDateString());
                }
                else if (sdrnDt.Rows[0]["STATUS"].ToString() == "")
                {
                    oUtilsTask.SendNotice(4, 11, teur + ": status sdrn is null after 'RunSdrnWithDate' to date=" + Taarich.ToShortDateString());

                }
                else if (sdrnDt.Rows[0]["STATUS"].ToString() == "1" || sdrnDt.Rows[0]["STATUS"].ToString() == "2")
                {
                    CompleteSdrn(Taarich);
                    oUtilsTask.SendNotice(4, 11, teur + ": status sdrn=" + sdrnDt.Rows[0]["STATUS"].ToString() + " ,'RunSdrnWithDate' + 'CompleteSdrn' run to date=" + Taarich.ToShortDateString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void RunBakaratSDRN()
        {
           
            DataTable LogDt = new DataTable();
             DataTable sdrnDt = new DataTable(); 
            int  numLog21;
            DateTime moedTchilaSdrn, moedTchilaBakara;
            clUtils oUtils = new clUtils();
            DateTime Taarich = DateTime.Now.AddDays(-1);
            clTaskManager oTask = new clTaskManager();
            KdsLibrary.TaskManager.Utils oUtilsTask = new KdsLibrary.TaskManager.Utils();
            try
            {
                LogDt = oTask.GetLogKvuzotByKod(4, 1, Taarich.AddDays(1));
                if (LogDt.Rows.Count > 0)
                {
                    moedTchilaSdrn = DateTime.Parse(LogDt.Rows[0]["moed_tchila"].ToString());
                    LogDt = oTask.GetLogKvuzotByKod(21, 2, Taarich.AddDays(1));
                    if (LogDt.Rows.Count > 0)
                    {
                        moedTchilaBakara = DateTime.Parse(LogDt.Rows[0]["moed_tchila"].ToString());
                        numLog21 = LogDt.Rows.Count;

                        if (numLog21 > 1 && moedTchilaBakara.Subtract(moedTchilaSdrn).TotalMinutes >= 60)
                        {
                            sdrnDt = oTask.GetStausSdrn(Taarich.ToString("yyyyMMdd"));
                            if (sdrnDt.Rows.Count == 0 || sdrnDt.Rows[0]["STATUS"].ToString() == "")
                            {
                                oTask.RunSdrnWithDate(Taarich.ToString("yyyyMMdd"));
                                CheckStatusAgain(Taarich, "RunBakaratSDRN");
                            }
                            else if (sdrnDt.Rows[0]["STATUS"].ToString() == "1" || sdrnDt.Rows[0]["STATUS"].ToString() == "2")
                            {
                                CheckStatusAgain(Taarich, "RunBakaratSDRN");
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
	}
}
