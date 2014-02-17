using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.BL;
using System.Diagnostics;
using KDSCommon.UDT;

namespace KdsBatch.HrWorkersChanges
{
     
    public class clManager : ClWorkerCompare
    {
        private List<COLL_OVDIM_IM_SHINUY_HR> _oArrCollOvdimImShinuyHR;
        public clManager(TableType State, ref bool flag)  : base(State, ref flag){
            try
            {
             
              //  _oCollOvdimImShinuyHR = new COLL_OVDIM_IM_SHINUY_HR();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override DataTable GetData()
        {
            clBatch obatch =  new clBatch();
            int direction;
            try
            {
                switch (_Type)
                {
                    case TableType.State:
                        _Dt = obatch.GetShinuyMatsavOvdim();
                        break;
                    case TableType.Properties:
                        _Dt = obatch.GetShinuyMeafyeneyBizua();
                        break;
                    case TableType.Details:
                        _Dt = obatch.GetShinuyPireyOved();
                        break;
                    case TableType.Defaults:
                         _Dt = obatch.GetShinuyBrerotMechdal();
                        break;
                }    
                return _Dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void SetListOfPeriodToUdt()
        {
            OBJ_OVDIM_IM_SHINUY_HR oObjOvdimImShinuyHR;
            DateTime TaarichShinuy = new DateTime();
            int j = 0;
            long size = 0;
            int indexArr = 0;
            int godel = 40000;
            try
            {
                foreach (ClPeriodOfID Period in ListPeriod)
                {
                    size = size + Period.CountOfDay;
                }
                if (size % godel != 0)
                    size = (size / godel) + 1;
                else
                    size = (size / godel);

                _oArrCollOvdimImShinuyHR = new List<COLL_OVDIM_IM_SHINUY_HR>(int.Parse(size.ToString()));
                _oArrCollOvdimImShinuyHR.Add(new COLL_OVDIM_IM_SHINUY_HR(godel));
                foreach (ClPeriodOfID Period in ListPeriod)
                {
                    
                    TaarichShinuy = Period.FromDate;
                    for (int i = 0; i < Period.CountOfDay; i++)
                    {
                        oObjOvdimImShinuyHR = new OBJ_OVDIM_IM_SHINUY_HR();
                        oObjOvdimImShinuyHR.MISPAR_ISHI = Period.IdNumber;
                        oObjOvdimImShinuyHR.TAARICH = TaarichShinuy;
                        oObjOvdimImShinuyHR.TAVLA = getNameTavla();
                        _oArrCollOvdimImShinuyHR[indexArr].Add(oObjOvdimImShinuyHR);
                        TaarichShinuy = TaarichShinuy.AddDays(1);
                        j++; 
        
                        if (j % godel == 0)
                        {
                            _oArrCollOvdimImShinuyHR[indexArr].DeleteNullElements();
                            indexArr = indexArr + 1;
                            _oArrCollOvdimImShinuyHR.Add(new COLL_OVDIM_IM_SHINUY_HR(godel));
                        }
                    } 
                }
                _oArrCollOvdimImShinuyHR[indexArr].DeleteNullElements();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertPeriod()
        {
            try
            {
                SetListOfPeriodToUdt();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SaveShinuyimHR(ref bool flag)
        {
            clBatch obatch = new clBatch();
            try
            {    
               obatch.SaveShinuyimHR(_oArrCollOvdimImShinuyHR);
            }
            catch (Exception ex)
            {
                flag = false;
                throw ex;
            }
        }

        public void TipulTableDefaults(ref bool flag)
        {
            COLL_BREROT_MECHDAL_MEAFYENIM _oCollBrerotMechdalPeriods = new COLL_BREROT_MECHDAL_MEAFYENIM();
            OBJ_BREROT_MECHDAL_MEAFYENIM oObjBrerotMechdalPeriods;// = new OBJ_OVDIM_IM_SHINUY_HR();
            clBatch obatch = new clBatch();
       
            List<ClPeriodOfID> List;
            try
            {
               
                List = ListPeriod.FindAll(delegate(ClPeriodOfID Period)
                {
                    if (Period != null) return true;
                    else return false;
                });
                
                foreach (ClPeriodOfID Period in List)
                {
                    oObjBrerotMechdalPeriods = new OBJ_BREROT_MECHDAL_MEAFYENIM();
                    oObjBrerotMechdalPeriods.KOD_MEAFYEN = Period.Code;
                    oObjBrerotMechdalPeriods.ME_TAARICH = Period.FromDate;
                    oObjBrerotMechdalPeriods.AD_TAARICH = Period.ToDate;

                    _oCollBrerotMechdalPeriods.Add(oObjBrerotMechdalPeriods);
                }
                obatch.SaveChangesDefaultsHR(_oCollBrerotMechdalPeriods);
            }
            catch (Exception ex)
            {
                flag = false;
                throw ex;
            }
        }
        public string getNameTavla()
        {
            switch (_Type)
            {
                case TableType.State:
                    return  "matzav_ovdim";
                case TableType.Properties:
                    return "meafyenim";
                case TableType.Details:
                    return "pirtey_ovdim";
            }
            return "";
        }
    }
}
