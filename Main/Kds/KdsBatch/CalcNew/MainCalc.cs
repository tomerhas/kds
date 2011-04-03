using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.DAL;
using KdsLibrary.UDT;
using KdsLibrary.BL;
using KdsLibrary;

namespace KdsBatch.CalcNew
{
    public class MainCalc
    {
        #region Definitions
        public enum TypeCalc
        { Batch = 1, OnLine = 2, Test = 3 }

        private COLL_CHISHUV_SIDUR _collChishuvSidur;
        private COLL_CHISHUV_YOMI _collChishuvYomi;
        private COLL_CHISHUV_CHODESH _collChishuvChodesh;
        #endregion

        TypeCalc _iTypeCalc;
        private int _iBakashaId;
        private string _sMaamad;
        private bool _bRitzaGorefet;
        private DateTime _dTarMe;
        private DateTime _dTarAd;

        public MainCalc()
        {
        }
        public MainCalc(int iBakashaId, DateTime dTarMe, DateTime dTarAd, string sMaamad, bool bRitzaGorefet, TypeCalc iTypeCalc)
        {
            _iBakashaId = iBakashaId;
            _dTarMe = dTarMe;
            _dTarAd = dTarAd;
            _sMaamad = sMaamad;
            _bRitzaGorefet = bRitzaGorefet;
            _iTypeCalc = iTypeCalc;
        }

        public Ovdim getOvdimLechishuv()
        {
            Ovdim listOvdim = new Ovdim();
            listOvdim.SetListOvdimLechishuv(_dTarMe, _dTarAd, _sMaamad, _bRitzaGorefet, _iBakashaId);
            return listOvdim; 
        }

        public void CalcOved(Oved oOved)
        {
            DataSet dsChishuv;
          //  CalcMonth oMonth;
            try
            {
                // oMonth = new CalcMonth(oOved);
                // dsChishuv = oMonth.CalcMonth();
             //   DataSetTurnIntoUdt(dsChishuv);
            }
            catch (Exception ex)
            {
              //  clLogBakashot.SetError(lBakashaId, iMisparIshi, "E", 0, null, "CalcOved: " + ex.Message);
                throw ex;
            }
        }

      
    }
}
