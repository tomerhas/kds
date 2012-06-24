using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary;
using KdsLibrary.DAL;
namespace KdsBatch
{
    public class PirteyOved
    {
        public int iMaamad { get; set; }
        public int iMaamadRashi { get; set; }
        public int iDirug { get; set; }
        public int iDarga { get; set; }
        public int iGil { get; set; }
        public long iBakashaId { get; set; }
        public long iBakashaIdRizatChishuv { get; set; }
        public DateTime dChodeshChishuv { get; set; }
        public string sChodeshIbud { get; set; }

        public clEruaDataEt oDataEt { get; set; }
        public clEruaBakaraEt oBakaraEt { get; set; }
        public clErua462 oErua462 { get; set; }
        public clErua589 oErua589 { get; set; }
        public clErua413 oErua413 { get; set; }
        public clErua415 oErua415 { get; set; }
        public clErua416 oErua416 { get; set; }
        public clErua417 oErua417 { get; set; }
        public clErua418 oErua418 { get; set; }
        public clErua419 oErua419 { get; set; }
        public clErua460 oErua460 { get; set; }

        private DataRow _drPirteyOved;
        private DataTable _dtRechivim;
        private DataTable _dtChishuv;
        private DataTable _dtRechiveyPrem;
        public PirteyOved(int Maamad, int MaamadRashi, int Dirug, int Darga, long BakashaId,long lRequestNumToTransfer, DataRow drPirteyOved)
        {
            iMaamad = Maamad;
            iMaamadRashi = MaamadRashi;
            iDirug = Dirug;
            iDarga = Darga;
            iBakashaId= BakashaId;
            dChodeshChishuv = DateTime.Parse(drPirteyOved["taarich"].ToString());
            _drPirteyOved = drPirteyOved;
           
            iBakashaIdRizatChishuv = lRequestNumToTransfer;
          //  iGil =int.Parse(drPirteyOved["gil"].ToString());
         //   InitializeErueyOved();
        }

        public void InitializeErueyOved(DataTable dtDetailsChishuv,DataTable dtPrem)
        {
            _dtRechivim = dtDetailsChishuv;
            _dtRechiveyPrem = dtPrem;
            try
            {
                _dtChishuv = GetChishuvYomiToOved(int.Parse(_drPirteyOved["mispar_ishi"].ToString()));
                if (iDirug == 85 && iDarga == 30)
                {
                    oDataEt = new clEruaDataEt(iBakashaId, _drPirteyOved, _dtRechivim, _dtChishuv);
                    oBakaraEt = new clEruaBakaraEt(iBakashaId, _drPirteyOved, _dtRechivim);
                }
                else
                {
                    oErua462 = new clErua462(iBakashaId, _drPirteyOved, _dtRechivim, _dtChishuv);
                    oErua589 = new clErua589(iBakashaId, _drPirteyOved, _dtRechivim, _dtChishuv);
                    oErua413 = new clErua413(iBakashaId, _drPirteyOved, _dtRechivim);
                
                    oErua415 = new clErua415(iBakashaId, _drPirteyOved, _dtRechivim);
                    
                    oErua416 = new clErua416(iBakashaId, _drPirteyOved, _dtRechivim);
                    oErua417 = new clErua417(iBakashaId, _drPirteyOved, _dtRechivim, _dtRechiveyPrem);
                    oErua460 = new clErua460(iBakashaId, _drPirteyOved, _dtRechivim, _dtRechiveyPrem);

                    if (iMaamadRashi != clGeneral.enMaamad.Salarieds.GetHashCode())
                    {
                        oErua418 = new clErua418(iBakashaId, _drPirteyOved, _dtRechivim);
                    }

                    if (iMaamadRashi == clGeneral.enMaamad.Salarieds.GetHashCode())
                    {
                        oErua419 = new clErua419(iBakashaId, _drPirteyOved, _dtRechivim, _dtRechiveyPrem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw(ex);
            }
        }

        private DataTable GetChishuvYomiToOved(int iMisparIshi)
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();

            try
            {
                oDal.AddParameter("p_request_id", ParameterType.ntOracleInt64, iBakashaIdRizatChishuv, ParameterDir.pdInput);
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dChodeshChishuv, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clDefinitions.cProGetChishuvYomiToOved, ref  dt);

                return dt;
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(iBakashaId, iMisparIshi, "E", 0, null, "GetChishuvYomiToOved: " + ex.Message);
                throw ex;
            }
        }
    }
}
