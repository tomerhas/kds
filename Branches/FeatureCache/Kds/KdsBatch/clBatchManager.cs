using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Specialized;
using KdsLibrary.BL;
using System.Collections;
using KdsLibrary.Utils;
using KdsLibrary;
using KdsWorkFlow.Approvals;
using System.Web;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.DataModels;
using KDSCommon.Interfaces;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KDSCommon.Interfaces.Managers;
using KDSCommon.Helpers;
using KDSCommon.Interfaces.DAL;
using KDSCommon.Interfaces.Errors;
using KDSCommon.UDT;
using DalOraInfra.DAL;
using ObjectCompare;
using KDSCommon.DataModels.Shinuyim;
using KDSCommon.Interfaces.Shinuyim;

namespace KdsBatch
{
    public class clBatchManager  
    {
        private long? _btchRequest;
        private int _iUserId;

        public clBatchManager(long? btchRequest):this()
        {
            _btchRequest = btchRequest;
        }

        public clBatchManager()
        {
            _iUserId = -2;
        }

        public clBatchManager(long? btchRequest, int iUserId)
        {
            _btchRequest = btchRequest;
            _iUserId = iUserId;
        }

        public FlowErrorResult MainOvedErrorsNew(int iMisparIshi, DateTime dCardDate, bool bSaveChange=true)
        {
            var errorFlowManager = ServiceLocator.Current.GetInstance<IErrorFlowManager>();
            FlowErrorResult errorResults = errorFlowManager.ExecuteFlow(iMisparIshi, dCardDate, bSaveChange,_btchRequest, _iUserId);

            return errorResults;        
        }

        public FlowShinuyResult MainInputDataNew(int iMisparIshi, DateTime dCardDate, bool bSaveChange=true)
        {
            var shinuyFlowManager = ServiceLocator.Current.GetInstance<IShinuyimFlowManager>();
            FlowShinuyResult shinuyResults = shinuyFlowManager.ExecShinuyim(iMisparIshi, dCardDate, bSaveChange,_btchRequest, _iUserId);

            return shinuyResults;
            }

                }
}

