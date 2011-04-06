using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using KdsBatch;

namespace KdsService
{
    [ServiceContract]
    public interface IBatchService
    {
        [OperationContract]
        void ExecuteInputDataAndErrors(int requestSource,
            int execType, DateTime workDate, long btchRequest);

        //[OperationContract]
        //void CalcBatchParallel(long lRequestNum, DateTime dAdChodesh, string sMaamad,
        //    bool bRitzatTest, bool bRitzaGorefet);
        [OperationContract]
        void CalcBatch(long lRequestNum, DateTime dAdChodesh, string sMaamad, 
            bool bRitzatTest, bool bRitzaGorefet);

        [OperationContract]
        void TransferToHilan(long lRequestNum, long lRequestNumToTransfer);

        [OperationContract]
        void CreateConstantsReports(long lRequestNum, string sMonth, int iUserId);

        [OperationContract]
        void CreateHeavyReports(long lRequestNum);

        [OperationContract]
        string CreatePremiaInputFile(long btchRequest, DateTime period, int userId, 
            long processBtchNumber);

        [OperationContract]
        string RunPremiaCalculation(DateTime period, int userId, long processBtchNumber);

        [OperationContract]
        string StorePremiaCalculationOutput(long btchRequest, DateTime period, int userId,
            long processBtchNumber);
        
        [OperationContract]
        void RefreshMatzavOvdim();

        [OperationContract]
        void TahalichHrChanges(int iseq);

        [OperationContract]
        void TahalichSadran(string taarich);
      
        [OperationContract]
        void RefreshMeafyenim();

        [OperationContract]
        void RefreshPirteyOvdim();

    }
}
