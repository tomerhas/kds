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
using Microsoft.Practices.ServiceLocation;
using KDSCommon.Interfaces.Logs;

using System.Threading;
using KDSCommon.Enums;
using KDSCommon.Proxies;
using KDSCommon.Interfaces.Managers.BankShaot;
using KDSCommon.Interfaces.Managers;



namespace KdsBatch.TaskManager
{
	public class Utils
	{
        System.Timers.Timer _timer = null;
        private long _bakasha_id=0;
        private long _lReqestId=0;
     

        void OnTimerAwake(object sender, EventArgs e)
        {
            clRequest oRequest = new clRequest();
            try
            {

                _timer.Stop();

                if (!oRequest.CheckTahalichEnd(_bakasha_id))
                {
                    _timer.Start();
                }
                else
                {
                    _timer = null;
                    ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(_lReqestId, "I", 0, "END");
                    clDefinitions.UpdateLogBakasha(_lReqestId, DateTime.Now, clGeneral.enStatusRequest.ToBeEnded.GetHashCode());
                }
            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
            }
        }

		public void RunShguimOfSdrn()
		{
		   // clBatch oBatch = new clBatch();
			clUtils oUtils = new clUtils();
			long lRequestNum = 0;
			try
			{
				BatchProxy client = new BatchProxy();
				lRequestNum = clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.InputDataAndErrorsFromInputProcess, "RunShguimOfSdrn", -12);
				client.ShinuyimVeShguimBatch(lRequestNum, DateTime.Now.AddDays(-1), enCalcType.ShinuyimVeShguyim, BatchExecutionType.All);
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
                BatchProxy client = new BatchProxy();
                lRequestNum = clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.ShguimOfRetroSpaectSdrn, "RunShguimOfRetroSpectSdrn", -12);
                client.ShinuyimVeShguimBatch(lRequestNum, dTaarich, enCalcType.ShinuyimVeShguyim, BatchExecutionType.All);
                wait4process2end(KdsLibrary.clGeneral.enGeneralBatchType.ShguimOfRetroSpaectSdrn.GetHashCode());
            }
            catch (Exception ex)
            {
                throw new Exception("RunShguimOfRetroSpectSdrn:" + ex.Message);
            }
        }
		public void RunShguimOfPremiyotMusachim()
		{
			// clBatch oBatch = new clBatch();
		   // clUtils oUtils = new clUtils();
			long lRequestNum = 0;
			try
			{
				BatchProxy client = new BatchProxy();
                lRequestNum = clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.ShguimOfPremiyotMusachimNihulTnuaYadaniyot, "RunShguimOfPremiyotMusachim&NihulTnua&Yadaniyot", -12);
				client.ShinuyimVeShguimBatch(lRequestNum, DateTime.Now, enCalcType.ShinuyimVeSghuimPremiot, BatchExecutionType.All);
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
				BatchProxy client = new BatchProxy();
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
				BatchProxy client = new BatchProxy();
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
                        lRequestNum = clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.ShguimOfHR, "ShguimHrChanges", -12);
						dTaarich = DateTime.Now.AddDays(-1);
						oBatch.UpdateProcessLog(iSeqNum, KdsLibrary.BL.RecordStatus.Finish, "after OpenBatchRequest hr", 0);
						//'** KdsWriteProcessLog(8, 3, 1, "after OpenBatchRequest before shguyim")
						iSeqNum = oBatch.InsertProcessLog(8, 4, KdsLibrary.BL.RecordStatus.Wait, "before shguyim hr", 0);
						client.ShinuyimVeShguimBatch(lRequestNum, dTaarich, enCalcType.ShinuyimVeSghuimHR, BatchExecutionType.All);
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
                        var result = oBatchManager.MainInputDataNew(int.Parse(dtOvdim.Rows[i]["MISPAR_ISHI"].ToString()), DateTime.Parse(dtOvdim.Rows[i]["TAARICH"].ToString()));
                        bInpuDataResult = result.IsSuccess;// oBatchManager.MainInputData(int.Parse(dtOvdim.Rows[i]["MISPAR_ISHI"].ToString()), DateTime.Parse(dtOvdim.Rows[i]["TAARICH"].ToString()));
                       // bInpuDataResult = oBatchManager.MainInputData(int.Parse(dtOvdim.Rows[i]["MISPAR_ISHI"].ToString()), DateTime.Parse(dtOvdim.Rows[i]["TAARICH"].ToString()));

						if (bInpuDataResult)
						{
                            oBatchManager.MainOvedErrorsNew(int.Parse(dtOvdim.Rows[i]["MISPAR_ISHI"].ToString()), DateTime.Parse(dtOvdim.Rows[i]["TAARICH"].ToString()));
                            bInpuDataResult = result.IsSuccess; 
							//bInpuDataResult = oBatchManager.MainOvedErrors(int.Parse(dtOvdim.Rows[i]["MISPAR_ISHI"].ToString()), DateTime.Parse(dtOvdim.Rows[i]["TAARICH"].ToString()));
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
                //++clGeneral.LogError(ex);
                var excep =   ServiceLocator.Current.GetInstance<ILogBakashot>().SetError(255, 75757, "E", 9, DateTime.Now, "", ex);
                throw excep;
                //var excep = clLogBakashot.SetError(0, "E", 0, null, ex);//--
                // throw (excep);//--
				
			}
		}

        public void CreateNetuneyPremiyot()
        {
            clBatch oBatch = new clBatch();
            premyot.wsPremyot wsPremyot = new premyot.wsPremyot();
            long lRequestNum = 0;
             KdsLibrary.TaskManager.Utils oUtilsTask = new KdsLibrary.TaskManager.Utils();
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
                    ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "I", 0, ex.Message);
                    clGeneral.CloseBatchRequest(lRequestNum, clGeneral.enBatchExecutionStatus.Failed);
                }
                else clGeneral.LogError(ex);
                oUtilsTask.SendNotice(13, 10, "CreateNetuneyPremiyot Failed: " + ex.Message);
               
                throw (ex);
            }
			
        }
        private void CallbackCreateNetuneyPremiyot(object sender, KdsBatch.premyot.MeshekDataInputCompletedEventArgs e, long lRequestNum)
        {
            var logManager = ServiceLocator.Current.GetInstance<ILogBakashot>();
            logManager.InsertLog(lRequestNum, "I", 0, "יצירת נתוני פרמיות הסתיימה");
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
                        logManager.InsertLog(lRequestNum, "I", 0, "הפעולה נכשלה - בדוק רשומות בקובץ לוג");
                        clGeneral.CloseBatchRequest(lRequestNum, clGeneral.enBatchExecutionStatus.Failed);
                    }
                }
                else
                {
                    wsPremyot.Abort();
                    logManager.InsertLog(lRequestNum, "I", 0, e.Error.Message);
                    clGeneral.CloseBatchRequest(lRequestNum, clGeneral.enBatchExecutionStatus.Failed);
                }
            }
            catch (Exception ex)
            {
                wsPremyot.Abort();
                if (lRequestNum > 0)
                {
                    logManager.InsertLog(lRequestNum, "I", 0, ex.Message);
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
            KdsLibrary.TaskManager.Utils oUtilsTask = new KdsLibrary.TaskManager.Utils();
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
                    ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "I", 0, ex.Message);
					clGeneral.CloseBatchRequest(lRequestNum, clGeneral.enBatchExecutionStatus.Failed);
				}
				else clGeneral.LogError(ex);

                oUtilsTask.SendNotice(13, 11, "RunCalculationPremyot Failed: " + ex.Message);
               
				throw (ex);
			}
			
		}

		private void CallbackCalculPremyot(object sender,KdsBatch.premyot.CalcPremyotNihulTnuaCompletedEventArgs e,long lRequestNum)
		{
            var logManager = ServiceLocator.Current.GetInstance<ILogBakashot>();
            logManager.InsertLog(lRequestNum, "I", 0, "חישוב הפרמיות הסתיים");
			bool bSuccess = false;
			clBatch oBatch = new clBatch();
			premyot.wsPremyot wsPremyot = (premyot.wsPremyot)sender;
            string sStatus = "";
            try
            {
                if (null == e.Error)
                {
                     bSuccess = e.Result;
                    if (bSuccess)
                     {
                        clGeneral.CloseBatchRequest(lRequestNum, clGeneral.enBatchExecutionStatus.Succeeded);
                         sStatus = "End";
                     }
                    else
                    {
                        logManager.InsertLog(lRequestNum, "I", 0, "הפעולה נכשלה - בדוק רשומות בקובץ לוג");
                        clGeneral.CloseBatchRequest(lRequestNum, clGeneral.enBatchExecutionStatus.Failed);
                         sStatus = "Failed";
                    }
                }
                else
                {
                    sStatus = "Failed";
                    wsPremyot.Abort();
                    logManager.InsertLog(lRequestNum, "I", 0, e.Error.Message);
                    clGeneral.CloseBatchRequest(lRequestNum, clGeneral.enBatchExecutionStatus.Failed);
                }
            }
            catch (Exception ex)
            {
                sStatus = "Failed";
                wsPremyot.Abort();
                if (lRequestNum > 0)
                {
                    logManager.InsertLog(lRequestNum, "I", 0, ex.Message);
                    clGeneral.CloseBatchRequest(lRequestNum, clGeneral.enBatchExecutionStatus.Failed);
                }
                else clGeneral.LogError(ex);
                throw (ex);
            }
			finally
			{
                 ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "I", 0, sStatus);
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
            var logger = ServiceLocator.Current.GetInstance<ILogBakashot>();
            try
            {
                lRequestNum = clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.RetroSpectSDRN, "RunRetroSpectSDRN", -12);
                iStatus = clGeneral.enStatusRequest.InProcess.GetHashCode();
              //  clLogBakashot.InsertErrorToLog(lRequestNum, "I", 0, "START");

               
                Num = oUtils.GetTbParametrim(252, DateTime.Today);
                Taarich = Taarich.AddDays(-Num);

                while (Taarich <= DateTime.Now.AddDays(-2))
                {
                    sdrnDt = oTask.GetStausSdrn(Taarich.ToString("yyyyMMdd"));
                    if (sdrnDt.Rows.Count == 0)
                    {
                        lRequestNum = OpenBakashaToRetroSpectSdrn();
                        logger.InsertLog(lRequestNum, "I", 0, "START RunRetroSpectSDRN To Date: " + Taarich.ToShortDateString());
                        logger.InsertLog(lRequestNum, "I", 0, "RunSdrnWithDate TAARICH=" + Taarich.ToShortDateString());
                        oTask.RunSdrnWithDate(Taarich.ToString("yyyyMMdd"));
                        CheckStatusAgain(Taarich,"RunRetroSpectSDRN");
                    }
                    else
                    {
                        if (sdrnDt.Rows[0]["STATUS"].ToString() == "1" || sdrnDt.Rows[0]["STATUS"].ToString() == "2")
                        {
                            lRequestNum = OpenBakashaToRetroSpectSdrn();
                            logger.InsertLog(lRequestNum, "I", 0, "START RunRetroSpectSDRN To Date: " + Taarich.ToShortDateString());
                            logger.InsertLog(lRequestNum, "I", 0, "CompleteSdrn TAARICH=" + Taarich.ToShortDateString());
                            CompleteSdrn(Taarich);
                            oUtilsTask.SendNotice(4, 11, "RunRetroSpectSDRN: status sdrn=" + sdrnDt.Rows[0]["STATUS"].ToString() + " , 'RunRetrospectSdrn' + 'RefreshKnisot' run to date=" + Taarich.ToShortDateString());

                        }
                        else if (sdrnDt.Rows[0]["STATUS"].ToString() == "")
                        {
                            lRequestNum = OpenBakashaToRetroSpectSdrn();
                            ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "I", 0, "START RunRetroSpectSDRN To Date: " + Taarich.ToShortDateString());
                            ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "I", 0, "RunSdrnWithDate TAARICH=" + Taarich.ToShortDateString());
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

        private long OpenBakashaToRetroSpectSdrn()
        {
            long lRequestNum = 0;
            lRequestNum = clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.RetroSpectSDRN, "RunRetroSpectSDRN", -12);
            return lRequestNum;
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


        public void wait4process2end(int p_sug_bakasha)
        {
            clUtils oUtils = new clUtils();
            clRequest oRequest = new clRequest();
            KdsLibrary.TaskManager.Utils oUtilsTask = new KdsLibrary.TaskManager.Utils();
            var logger = ServiceLocator.Current.GetInstance<ILogBakashot>();
            try
            {
                //_timer = new System.Timers.Timer(5000);
                //_timer.Enabled = true;
                //_timer.Elapsed += OnTimerAwake;

                _lReqestId = clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.Sleep, "Sleep :" + (Enum.Parse(typeof(clGeneral.enGeneralBatchType), p_sug_bakasha.ToString())).ToString(), -12);
                _bakasha_id = oRequest.get_max_bakasha_id(p_sug_bakasha);
                if (_bakasha_id == 0)
                {
                    oUtilsTask.SendNotice(23, 1, "Sleep: bakasha not found, sug:" + p_sug_bakasha);
                    logger.InsertLog(_lReqestId, "I", 0, "Sleep: bakasha not found, sug:" + p_sug_bakasha);
                }
                else
                {
                    logger.InsertLog(_lReqestId, "I", 0, "Start Sleep wait to " + _bakasha_id);

                    while (!oRequest.CheckTahalichEnd(_bakasha_id))
                        Thread.Sleep(5000);
                }
               
                // if (!oRequest.CheckTahalichEnd(_bakasha_id))
                //{
                //    _timer.Start();
                //}
                //else
                //{
                //    _timer = null;
                    logger.InsertLog(_lReqestId, "I", 0, "END");
                    clDefinitions.UpdateLogBakasha(_lReqestId, DateTime.Now, clGeneral.enStatusRequest.ToBeEnded.GetHashCode());
                //}
                //_timer.Start();

            }
            catch (Exception ex)
            {
                logger.InsertLog(_lReqestId, "I", 0, "Sleep Faild:" + ex.Message);
                throw new Exception("Sleep:" + ex.Message);
            }
        }
	

         public void IdkunMachalotOvdim()
        {
            clBatch oBatch = new clBatch();
            long lRequestNum = 0;
            KdsLibrary.TaskManager.Utils oUtilsTask = new KdsLibrary.TaskManager.Utils();
            try
            {

                lRequestNum = clGeneral.OpenBatchRequest(clGeneral.enGeneralBatchType.IdkunMachalotOvdim, "IdkunMachalotOvdim", -12);
                if (oBatch.InsMachalotLoMeturgamot(lRequestNum))
                    oUtilsTask.SendNotice(16, 4, "machalot lo meturgamot exists to bakasha id = " + lRequestNum);
                oBatch.IdkunMachalotOvdim(lRequestNum);
                clGeneral.CloseBatchRequest(lRequestNum, clGeneral.enBatchExecutionStatus.Succeeded);
            }
            catch (Exception ex)
            {
                if (lRequestNum > 0)
                {
                    ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "I", 0, ex.Message);
                    clGeneral.CloseBatchRequest(lRequestNum, clGeneral.enBatchExecutionStatus.Failed);
                }
                else clGeneral.LogError(ex);
                
                throw (ex);
            }
			
        }


         public void ChishuvBankShaotMeshek()
         {
             string chodesh="";
             long lRequestNum = 0;
             clBatch objBatch = new clBatch();
             try
             {

                 IBankShaotManager BankManager = ServiceLocator.Current.GetInstance<IBankShaotManager>();
              //   lRequestNum = clGeneral.OpenBatchRequest(clGeneral.enGeneralBatchType.ChishuvBankShaotMeshek, "ChishuvBankShaotMeshek", -12);
                 chodesh =  DateTime.Now.Date.Month.ToString().PadLeft(2,'0') +"/"+ DateTime.Now.Date.Year;
                 lRequestNum = objBatch.InsBakashaChishuvBankShaot(clGeneral.enGeneralBatchType.ChishuvBankShaotMeshek, "ChishuvBankShaotMeshek", clGeneral.enStatusRequest.InProcess, -12, chodesh);
                // ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "I", 0, "before BankManager.ExecBankShaot");
               //  ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "I", 0, "BankManager obj=" + BankManager);
                 BankManager.ExecBankShaot(lRequestNum);
                 ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "I", 0, "after BankManager.ExecBankShaot");
                 clGeneral.CloseBatchRequest(lRequestNum, clGeneral.enBatchExecutionStatus.Succeeded);
             }
             catch (Exception ex)
             {
                 if (lRequestNum > 0)
                 {
                     ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "I", 0, ex.Message);
                     clGeneral.CloseBatchRequest(lRequestNum, clGeneral.enBatchExecutionStatus.Failed);
                 }
                 else clGeneral.LogError(ex);

                 throw (ex);
             }

         }

        public void ChishuvBankShaotMeshekByParams(int mitkan,DateTime chodesh)
        {
      
            long lRequestNum = 0;
            clBatch objBatch = new clBatch();
            try
            {

                IBankShaotManager BankManager = ServiceLocator.Current.GetInstance<IBankShaotManager>();
                //   lRequestNum = clGeneral.OpenBatchRequest(clGeneral.enGeneralBatchType.ChishuvBankShaotMeshek, "ChishuvBankShaotMeshek", -12);
                
                lRequestNum = objBatch.InsBakashaChishuvBankShaot(clGeneral.enGeneralBatchType.ChishuvBankShaotMeshek, "ChishuvBankShaotMeshek", clGeneral.enStatusRequest.InProcess, -12, chodesh.ToShortDateString());
                // ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "I", 0, "before BankManager.ExecBankShaot");
                //  ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "I", 0, "BankManager obj=" + BankManager);
                BankManager.ExecBankShaotLefiParametrim(lRequestNum, mitkan, chodesh);
                ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "I", 0, "after BankManager.ExecBankShaot");
                clGeneral.CloseBatchRequest(lRequestNum, clGeneral.enBatchExecutionStatus.Succeeded);
            }
            catch (Exception ex)
            {
                if (lRequestNum > 0)
                {
                    ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lRequestNum, "I", 0, ex.Message);
                    clGeneral.CloseBatchRequest(lRequestNum, clGeneral.enBatchExecutionStatus.Failed);
                }
                else clGeneral.LogError(ex);

                throw (ex);
            }

        }

        public void KlitatTnuotAgtan()
         {
             string FileName,InPath,SubFolder,FileNameOld;
             string[] _files,splitName;
             long lRequestNum=0;
             int status = 0;
             ILogBakashot logManager=null;
              IClockManager clockManager; 
                 
             try{


                 lRequestNum = clGeneral.OpenBatchRequest(clGeneral.enGeneralBatchType.KlitatNochechutAgtan, "KlitatTnuotAgtan", -12);
                 logManager = ServiceLocator.Current.GetInstance<ILogBakashot>();
                 logManager.InsertLog(lRequestNum, "I", 0, "Start Klitat Agtan");

                FileName =  ConfigurationSettings.AppSettings["KdsInputFileNameAgtan"];
                if(FileName.Trim()=="")
                    FileName = "A01SN_AGTAN.DAT";
                 
                 InPath  = ConfigurationSettings.AppSettings["KdsFilePath"];
                 SubFolder = ConfigurationSettings.AppSettings["KdsFileSubPath"];

                  clockManager = ServiceLocator.Current.GetInstance<IClockManager>();
                 _files = Directory.GetFiles(InPath , FileName , SearchOption.TopDirectoryOnly);
                  for (int i = 0; i < _files.Length; i++)
                  {
                      try{
                          logManager.InsertLog(lRequestNum, "I", 0, "START LoadKdsFileAgtan: " + _files[i]);
                          clockManager.LoadKdsFileAgtan(_files[i], lRequestNum);
                          logManager.InsertLog(lRequestNum, "I", 0, "END LoadKdsFileAgtan: " + _files[i]);
                          splitName = _files[i].Split("\\".ToCharArray());
                          FileName = splitName[splitName.Length - 1];
                          FileNameOld = FileName.Substring(0, FileName.Length - 4) + ".old";
                          File.Copy(_files[i], InPath+ SubFolder + FileNameOld, true);
                          
                          logManager.InsertLog(lRequestNum,"I",0, "after copy Agtan " + _files[i]);
                          File.Delete(_files[i]);
                          logManager.InsertLog(lRequestNum, "I", 0, "after delete Agtan " + _files[i]);

                      }
                      catch(Exception ex)
                      {
                          logManager.InsertLog(lRequestNum, "E", 0, "KlitatTnuotAgtan Fail, File Name: "+ _files[i] + " Err: " +ex.Message);
                      }
                  }
                 
                   status = 2;
             }
             catch(Exception ex)
             {
                 status = 3;
                 logManager.InsertLog(lRequestNum, "E", 0, "KlitatTnuotAgtan Fail: " +ex.Message);
             }
             finally
             {
                 clDefinitions.UpdateLogBakasha(lRequestNum, DateTime.Now, status);
                 logManager.InsertLog(lRequestNum, "I", 0, "End KlitatTnuotAgtan");
             }
         }



      
	}
}
