using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using KDSCommon.Interfaces.Service;

namespace KDSCommon.Proxies
{
    public class BatchProxy : ClientBase<IBatchService>, IBatchService
    {
        public void ExecuteInputDataAndErrors(int requestSource, int execType, DateTime workDate, long btchRequest)
        {
            Channel.ExecuteInputDataAndErrors(requestSource, execType, workDate, btchRequest);
        }

        public void CalcBatchParallel(long lRequestNum, DateTime dAdChodesh, string sMaamad, bool bRitzatTest, bool bRitzaGorefet)
        {
            Channel.CalcBatchParallel( lRequestNum,  dAdChodesh,  sMaamad,  bRitzatTest,  bRitzaGorefet);
        }

        public void ShinuyimVeShguimBatch(long lRequestNum, DateTime dTaarich, KDSCommon.Enums.enCalcType TypeShguyim, KDSCommon.Enums.BatchExecutionType ExecutionTypeShguim)
        {
            Channel.ShinuyimVeShguimBatch( lRequestNum,  dTaarich,  TypeShguyim, ExecutionTypeShguim);
        }

        public void CalcBatchPremiyot(long lRequestNum)
        {
            Channel.CalcBatchPremiyot( lRequestNum);
        }

        public void InsetRecordsToHistory(long lRequestNum)
        {
            Channel.InsetRecordsToHistory( lRequestNum);
        }

        public void TransferToHilan(long lRequestNum, long lRequestNumToTransfer)
        {
            Channel.TransferToHilan( lRequestNum,  lRequestNumToTransfer);
        }

        public void BdikatChufshaRezifa(long lRequestNum, int iUserId)
        {
            Channel.BdikatChufshaRezifa( lRequestNum,  iUserId);
        }

        public void YeziratRikuzim(long lRequestNum, long iRequestIdForRikuzim)
        {
            Channel.YeziratRikuzim( lRequestNum,  iRequestIdForRikuzim);
        }

        public void TransferTekenNehagim(long lRequestNum, long iRequestIdForTransfer)
        {
            Channel.TransferTekenNehagim( lRequestNum,  iRequestIdForTransfer);
        }

        public void ShlichatRikuzimMail(long lRequestNum, long iRequestIdForRikuzim)
        {
            Channel.ShlichatRikuzimMail( lRequestNum,  iRequestIdForRikuzim);
        }

        public void CreateConstantsReports(long lRequestNum, string sMonth, int iUserId)
        {
            Channel.CreateConstantsReports( lRequestNum,  sMonth,  iUserId);
        }

        public void CreateHeavyReports(long lRequestNum)
        {
            Channel.CreateHeavyReports( lRequestNum);
        }

        public string CreatePremiaInputFile(long btchRequest, DateTime period, int userId, long processBtchNumber)
        {
            return Channel.CreatePremiaInputFile( btchRequest,  period,  userId,  processBtchNumber);
        }

        public string RunPremiaCalculation(DateTime period, int userId, long processBtchNumber)
        {
            return Channel.RunPremiaCalculation( period,  userId,  processBtchNumber);
        }

        public string StorePremiaCalculationOutput(long btchRequest, DateTime period, int userId, long processBtchNumber)
        {
            return Channel.StorePremiaCalculationOutput(btchRequest, period, userId, processBtchNumber);
        }

        public void RefreshMatzavOvdim()
        {
            Channel.RefreshMatzavOvdim();
        }

        public void TahalichHrChanges(int iseq)
        {
            Channel.TahalichHrChanges( iseq);
        }

        public void TahalichSadran(string taarich)
        {
            Channel.TahalichSadran( taarich);
        }

        public void RefreshMeafyenim()
        {
            Channel.RefreshMeafyenim();
        }

        public void RefreshPirteyOvdim()
        {
            Channel.RefreshPirteyOvdim();
        }

        public void TkinutMakatimBatch(DateTime dTaarich)
        {
            Channel.TkinutMakatimBatch( dTaarich);
        }

        public void BdikatYemeyMachala(long lRequestNum, int iUserId)
        {
            Channel.BdikatYemeyMachala( lRequestNum,  iUserId);
        }
    }
}
