﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.239
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KdsBatch.KdsServiceProxy {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="KdsServiceProxy.IBatchService")]
    public interface IBatchService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBatchService/ExecuteInputDataAndErrors", ReplyAction="http://tempuri.org/IBatchService/ExecuteInputDataAndErrorsResponse")]
        void ExecuteInputDataAndErrors(int requestSource, int execType, System.DateTime workDate, long btchRequest);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBatchService/CalcBatchParallel", ReplyAction="http://tempuri.org/IBatchService/CalcBatchParallelResponse")]
        void CalcBatchParallel(long lRequestNum, System.DateTime dAdChodesh, string sMaamad, bool bRitzatTest, bool bRitzaGorefet);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBatchService/ShinuyimVeShguimBatch", ReplyAction="http://tempuri.org/IBatchService/ShinuyimVeShguimBatchResponse")]
        void ShinuyimVeShguimBatch(long lRequestNum, System.DateTime dTaarich, KdsLibrary.clGeneral.enCalcType TypeShguyim, KdsLibrary.clGeneral.BatchExecutionType ExecutionTypeShguim);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBatchService/CalcBatchPremiyot", ReplyAction="http://tempuri.org/IBatchService/CalcBatchPremiyotResponse")]
        void CalcBatchPremiyot(long lRequestNum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBatchService/CalcBatch", ReplyAction="http://tempuri.org/IBatchService/CalcBatchResponse")]
        void CalcBatch(long lRequestNum, System.DateTime dAdChodesh, string sMaamad, bool bRitzatTest, bool bRitzaGorefet);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBatchService/TransferToHilan", ReplyAction="http://tempuri.org/IBatchService/TransferToHilanResponse")]
        void TransferToHilan(long lRequestNum, long lRequestNumToTransfer);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBatchService/YeziratRikuzim", ReplyAction="http://tempuri.org/IBatchService/YeziratRikuzimResponse")]
        void YeziratRikuzim(long lRequestNum, long iRequestIdForRikuzim);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBatchService/CreateConstantsReports", ReplyAction="http://tempuri.org/IBatchService/CreateConstantsReportsResponse")]
        void CreateConstantsReports(long lRequestNum, string sMonth, int iUserId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBatchService/CreateHeavyReports", ReplyAction="http://tempuri.org/IBatchService/CreateHeavyReportsResponse")]
        void CreateHeavyReports(long lRequestNum);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBatchService/CreatePremiaInputFile", ReplyAction="http://tempuri.org/IBatchService/CreatePremiaInputFileResponse")]
        string CreatePremiaInputFile(long btchRequest, System.DateTime period, int userId, long processBtchNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBatchService/RunPremiaCalculation", ReplyAction="http://tempuri.org/IBatchService/RunPremiaCalculationResponse")]
        string RunPremiaCalculation(System.DateTime period, int userId, long processBtchNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBatchService/StorePremiaCalculationOutput", ReplyAction="http://tempuri.org/IBatchService/StorePremiaCalculationOutputResponse")]
        string StorePremiaCalculationOutput(long btchRequest, System.DateTime period, int userId, long processBtchNumber);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBatchService/RefreshMatzavOvdim", ReplyAction="http://tempuri.org/IBatchService/RefreshMatzavOvdimResponse")]
        void RefreshMatzavOvdim();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBatchService/TahalichHrChanges", ReplyAction="http://tempuri.org/IBatchService/TahalichHrChangesResponse")]
        void TahalichHrChanges(int iseq);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBatchService/TahalichSadran", ReplyAction="http://tempuri.org/IBatchService/TahalichSadranResponse")]
        void TahalichSadran(string taarich);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBatchService/RefreshMeafyenim", ReplyAction="http://tempuri.org/IBatchService/RefreshMeafyenimResponse")]
        void RefreshMeafyenim();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IBatchService/RefreshPirteyOvdim", ReplyAction="http://tempuri.org/IBatchService/RefreshPirteyOvdimResponse")]
        void RefreshPirteyOvdim();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IBatchServiceChannel : KdsBatch.KdsServiceProxy.IBatchService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class BatchServiceClient : System.ServiceModel.ClientBase<KdsBatch.KdsServiceProxy.IBatchService>, KdsBatch.KdsServiceProxy.IBatchService {
        
        public BatchServiceClient() {
        }
        
        public BatchServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public BatchServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BatchServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public BatchServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void ExecuteInputDataAndErrors(int requestSource, int execType, System.DateTime workDate, long btchRequest) {
            base.Channel.ExecuteInputDataAndErrors(requestSource, execType, workDate, btchRequest);
        }
        
        public void CalcBatchParallel(long lRequestNum, System.DateTime dAdChodesh, string sMaamad, bool bRitzatTest, bool bRitzaGorefet) {
            base.Channel.CalcBatchParallel(lRequestNum, dAdChodesh, sMaamad, bRitzatTest, bRitzaGorefet);
        }
        
        public void ShinuyimVeShguimBatch(long lRequestNum, System.DateTime dTaarich, KdsLibrary.clGeneral.enCalcType TypeShguyim, KdsLibrary.clGeneral.BatchExecutionType ExecutionTypeShguim) {
            base.Channel.ShinuyimVeShguimBatch(lRequestNum, dTaarich, TypeShguyim, ExecutionTypeShguim);
        }
        
        public void CalcBatchPremiyot(long lRequestNum) {
            base.Channel.CalcBatchPremiyot(lRequestNum);
        }
        
        public void CalcBatch(long lRequestNum, System.DateTime dAdChodesh, string sMaamad, bool bRitzatTest, bool bRitzaGorefet) {
            base.Channel.CalcBatch(lRequestNum, dAdChodesh, sMaamad, bRitzatTest, bRitzaGorefet);
        }
        
        public void TransferToHilan(long lRequestNum, long lRequestNumToTransfer) {
            base.Channel.TransferToHilan(lRequestNum, lRequestNumToTransfer);
        }
        
        public void YeziratRikuzim(long lRequestNum, long iRequestIdForRikuzim) {
            base.Channel.YeziratRikuzim(lRequestNum, iRequestIdForRikuzim);
        }
        
        public void CreateConstantsReports(long lRequestNum, string sMonth, int iUserId) {
            base.Channel.CreateConstantsReports(lRequestNum, sMonth, iUserId);
        }
        
        public void CreateHeavyReports(long lRequestNum) {
            base.Channel.CreateHeavyReports(lRequestNum);
        }
        
        public string CreatePremiaInputFile(long btchRequest, System.DateTime period, int userId, long processBtchNumber) {
            return base.Channel.CreatePremiaInputFile(btchRequest, period, userId, processBtchNumber);
        }
        
        public string RunPremiaCalculation(System.DateTime period, int userId, long processBtchNumber) {
            return base.Channel.RunPremiaCalculation(period, userId, processBtchNumber);
        }
        
        public string StorePremiaCalculationOutput(long btchRequest, System.DateTime period, int userId, long processBtchNumber) {
            return base.Channel.StorePremiaCalculationOutput(btchRequest, period, userId, processBtchNumber);
        }
        
        public void RefreshMatzavOvdim() {
            base.Channel.RefreshMatzavOvdim();
        }
        
        public void TahalichHrChanges(int iseq) {
            base.Channel.TahalichHrChanges(iseq);
        }
        
        public void TahalichSadran(string taarich) {
            base.Channel.TahalichSadran(taarich);
        }
        
        public void RefreshMeafyenim() {
            base.Channel.RefreshMeafyenim();
        }
        
        public void RefreshPirteyOvdim() {
            base.Channel.RefreshPirteyOvdim();
        }
    }
}
