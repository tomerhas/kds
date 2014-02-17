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
    public int iMakorNetunim { get; set; }
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
        private int _iCntYamim;
        private DataTable _dtRechivim;
        //public DataTable _dtChishuv;
        private DataTable _dtRechiveyPrem;
        public PirteyOved(  long BakashaId,long lRequestNumToTransfer, DataRow drPirteyOved)
        {
            try
            {
                iBakashaId = BakashaId;

                iMaamad = int.Parse(drPirteyOved["maamad"].ToString());
                iMaamadRashi = int.Parse(drPirteyOved["maamad_rashi"].ToString());
                iDirug = int.Parse(drPirteyOved["dirug"].ToString());
                iDarga = int.Parse(drPirteyOved["darga"].ToString());
                dChodeshChishuv = DateTime.Parse(drPirteyOved["taarich"].ToString());
                iMakorNetunim = int.Parse(drPirteyOved["makor"].ToString());

                _drPirteyOved = drPirteyOved;

                iBakashaIdRizatChishuv = lRequestNumToTransfer;
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(iBakashaId, int.Parse(drPirteyOved["mispar_ishi"].ToString()), "E", 0, null, "PirteyOved ctor: " + ex.Message);
                throw (ex);
            }
        }

        public void InitializeErueyOved(DataSet dsTables)
        //public void InitializeErueyOved(DataTable dtDetailsChishuv, DataTable dtPrem)
        {
            _dtRechivim =dsTables.Tables[0];
            _dtRechiveyPrem = dsTables.Tables[2];
            try
            {
                //_dtChishuv = GetChishuvYomiToOved(int.Parse(_drPirteyOved["mispar_ishi"].ToString()), dtRechivimYomiim);
                if (iDirug == 85 && iDarga == 30)
                {
                    if (sChodeshIbud == DateTime.Parse(_drPirteyOved["taarich"].ToString()).ToString("MM/yyyy"))
                        oDataEt = new clEruaDataEt(iBakashaId, _drPirteyOved, dsTables);

                    oBakaraEt = new clEruaBakaraEt(iBakashaId, _drPirteyOved, dsTables, sChodeshIbud);
                }
                else
                {
                    _iCntYamim = GetCntYamimToOved(int.Parse(_drPirteyOved["mispar_ishi"].ToString()), dsTables.Tables[3], DateTime.Parse(_drPirteyOved["taarich"].ToString()));
                    oErua462 = new clErua462(iBakashaId, _drPirteyOved, dsTables, _iCntYamim);
                    //oErua462 = new clErua462(iBakashaId, _drPirteyOved, _dtRechivim);

                 //   if (iMakorNetunim != 2) //לא הגיע מרכיבי פרמיה בלבד
                 //       oErua589 = new clErua589(iBakashaId, _drPirteyOved, _dtRechivim, _dtChishuv);
                    oErua413 = new clErua413(iBakashaId, _drPirteyOved, dsTables);

                    oErua415 = new clErua415(iBakashaId, _drPirteyOved, dsTables);

                    oErua416 = new clErua416(iBakashaId, _drPirteyOved, dsTables);
                    oErua417 = new clErua417(iBakashaId, _drPirteyOved, dsTables);
                    oErua460 = new clErua460(iBakashaId, _drPirteyOved, dsTables);

                    if (iMaamadRashi != clGeneral.enMaamad.Salarieds.GetHashCode())
                    {
                        oErua418 = new clErua418(iBakashaId, _drPirteyOved, dsTables);
                    }

                    if (iMaamadRashi == clGeneral.enMaamad.Salarieds.GetHashCode())
                    {
                        oErua419 = new clErua419(iBakashaId, _drPirteyOved, dsTables);
                    }
                }
            }
            catch (Exception ex)
            {
                throw(ex);
            }
        }

        private int GetCntYamimToOved(int iMisparIshi, DataTable dtRechivimYomiim, DateTime dChodesh)
        {
            int num = 0;

            try
            {
                if (dtRechivimYomiim.Select("mispar_ishi= " + iMisparIshi + " and chodesh=Convert('" + dChodesh.ToShortDateString() + "', 'System.DateTime')").Length > 0)
                    num = int.Parse(dtRechivimYomiim.Select("mispar_ishi= " + iMisparIshi + " and chodesh=Convert('" + dChodesh.ToShortDateString() + "', 'System.DateTime')")[0]["yamim"].ToString());
                return num;
            }
            catch (Exception ex)
            {
                //   clLogBakashot.SetError(iBakashaId, iMisparIshi, "E", 0, null, "GetChishuvYomiToOved: " + ex.Message);
                throw ex;
            }
        }
        private DataTable GetChishuvYomiToOved(int iMisparIshi, DataTable dtRechivimYomiim)
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();
            DataRow[] rows;
            try
            {

                rows = dtRechivimYomiim.Select("mispar_ishi= " + iMisparIshi);
                if (rows.Length > 0)
                {
                    dt = rows.CopyToDataTable();
                }
                else
                {
                    dt = dtRechivimYomiim.Clone();
                }

                //oDal.AddParameter("p_request_id", ParameterType.ntOracleInt64, iBakashaIdRizatChishuv, ParameterDir.pdInput);
                //oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                //oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dChodeshChishuv, ParameterDir.pdInput);
                //oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                //oDal.ExecuteSP(clDefinitions.cProGetChishuvYomiToOved, ref  dt);

                return dt;
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(iBakashaId, iMisparIshi, "E", 0, null, "GetChishuvYomiToOved: " + ex.Message);
                throw ex;
            }
        }
      /*  private DataTable GetChishuvYomiToOved(int iMisparIshi, DataTable dtRechivimYomiim)
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();
            DataRow[] rows;
            try
            {

                rows = dtRechivimYomiim.Select("mispar_ishi= " + iMisparIshi );
                if (rows.Length > 0)
                {
                    dt = rows.CopyToDataTable();
                }
                else
                {
                    dt = dtRechivimYomiim.Clone();
                }

                //oDal.AddParameter("p_request_id", ParameterType.ntOracleInt64, iBakashaIdRizatChishuv, ParameterDir.pdInput);
                //oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                //oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dChodeshChishuv, ParameterDir.pdInput);
                //oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                //oDal.ExecuteSP(clDefinitions.cProGetChishuvYomiToOved, ref  dt);

                return dt;
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(iBakashaId, iMisparIshi, "E", 0, null, "GetChishuvYomiToOved: " + ex.Message);
                throw ex;
            }
        }*/
    }
}
