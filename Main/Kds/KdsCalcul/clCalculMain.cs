using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Runtime.Serialization;
using KdsBatch;
using KdsLibrary;
//using System.Threading;
using KdsLibrary.BL;
using System.Threading.Tasks;
using System.Data;
namespace KdsCalcul
{
    public class clCalculMain
    {
        private long lRequestNum;
        private DateTime dFromChodesh;
        private DateTime dAdChodesh;
        private string sMaamad;
        private bool bRitzatTest;
        private bool bRitzaGorefet;
        private int iNumProcess;
        #region Properties
        public long RequestNum
        {
            get { return lRequestNum; }
            set { lRequestNum = value; }
        } 
        public DateTime AdChodesh
        {
            get { return dAdChodesh; }
            set { dAdChodesh = value; }
        } 
        public string Maamad
        {
            get { return sMaamad; }
            set { sMaamad = value; }
        } 
        public bool RitzatTest
        {
            get { return bRitzatTest; }
            set { bRitzatTest = value; }
        } 
        public bool RitzaGorefet
        {
            get { return bRitzaGorefet; }
            set { bRitzaGorefet = value; }
        }
        #endregion

        public clCalculMain(long RequestNum,DateTime dFrom, DateTime dAd, string maamd, bool RitzatTest, bool RitzaGorefet,int numProcess)
        {
            lRequestNum = RequestNum;
            dFromChodesh = dFrom;
            dAdChodesh = dAd;
            sMaamad = maamd;
            bRitzatTest = RitzatTest;
            bRitzaGorefet = RitzaGorefet;
            iNumProcess = numProcess;
        }

        public clCalculMain(long RequestNum, int numProcess)
        {
            lRequestNum = RequestNum;
            iNumProcess = numProcess;
        }
        public void RunCalcBatchProcess()
        {
           
            MainCalc oMainCalc;
            try
            {
                clLogBakashot.InsertErrorToLog(lRequestNum, "I", 0, "START PROCESS " + iNumProcess);
                oMainCalc = (bRitzatTest) ? new MainCalc(lRequestNum, dFromChodesh, dAdChodesh, sMaamad, bRitzaGorefet, clGeneral.TypeCalc.Test, iNumProcess) :
                                            new MainCalc(lRequestNum, dFromChodesh, dAdChodesh, sMaamad, bRitzaGorefet, clGeneral.TypeCalc.Batch, iNumProcess);
                if ((oMainCalc != null) && (oMainCalc.Ovdim != null) && (oMainCalc.Ovdim.Count > 0))
                {
                    #region not parallel
                    oMainCalc.Ovdim.ForEach(CurrentOved =>
                                        {
                                            oMainCalc.CalcOved(CurrentOved);
                                            CurrentOved.Dispose();
                                            CurrentOved = null;
                                        });
                    #endregion
                    //#region parallel
                    //Parallel.ForEach(oMainCalc.Ovdim, CurrentOved =>
                    //{
                    //    oMainCalc.CalcOved(CurrentOved);
                    //    CurrentOved.Dispose();
                    //    CurrentOved = null;

                    //});
                    //#endregion
                }
                clLogBakashot.InsertErrorToLog(lRequestNum, "I", 0, "END PROCESS " + iNumProcess);

            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
                clLogBakashot.InsertErrorToLog(lRequestNum, "E", 0, "RunCalcBatchProcess " + iNumProcess + ": " + ex.Message);
            }
            finally
            {
                SingleGeneralData.ResetObject();
            }
         
        }

        public void RunCalcBatchProcessPremiyot()
        {
            MainCalc oMainCalc;
            clBatch obatch = new clBatch();
            int numFailed = 0;
            int numSucceed = 0;
           //  int seq = 0;
            try
            {
                clLogBakashot.InsertErrorToLog(lRequestNum, "I", 0, "PREMIYOT START PROCESS " + iNumProcess);
                oMainCalc = new MainCalc(lRequestNum, iNumProcess);
                                           
                if ((oMainCalc != null) && (oMainCalc.Ovdim != null) && (oMainCalc.Ovdim.Count > 0))
                {
                    #region not parallel
                    oMainCalc.Ovdim.ForEach(CurrentOved =>
                    {
                        try
                        {
                            oMainCalc.CalcOvedPremiya(CurrentOved);
                            CurrentOved.Dispose();
                            CurrentOved = null;
                            numSucceed += 1;
                        }
                        catch (Exception ex)
                        {
                            numFailed += 1;
                            clLogBakashot.InsertErrorToLog(lRequestNum, CurrentOved.Mispar_ishi, "E", 0,CurrentOved.Taarich, "RunCalcBatchProcessPremiyot: " + ex.Message);
                        }
                    });
                    #endregion
                }
                clLogBakashot.InsertErrorToLog(lRequestNum, "I", 0, "PREMIYOT END PROCESS " + iNumProcess);

            }
            catch (Exception ex)
            {
                clGeneral.LogError(ex);
                clLogBakashot.InsertErrorToLog(lRequestNum, "E", 0, "RunCalcBatchProcessPremiyot " + iNumProcess + ": " + ex.Message);
            }
            finally
            {
                SingleGeneralData.ResetObject();
                clLogBakashot.InsertErrorToLog(lRequestNum, "I", 0, "PremiaCalc NumRowsFailed=" + numFailed + " NumRowsSucceed=" + numSucceed);
            }
        }

    }
}
