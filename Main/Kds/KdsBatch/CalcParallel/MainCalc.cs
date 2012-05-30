using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.DAL;
using KdsLibrary.UDT;
using KdsLibrary.BL;
using KdsLibrary;

namespace KdsBatch
{
    public class MainCalc
    {
        clGeneral.TypeCalc _iTypeCalc;
        private long _iBakashaId;
        private string _sMaamad;
        private bool _bRitzaGorefet;
        private DateTime _dTarMe;
        private DateTime _dTarAd;
        private int _numProcess;
        private List<Oved> _Ovdim;
        public List<Oved> Ovdim
        {
            get { return _Ovdim; }
        }
        public MainCalc()
        {
        }
        public MainCalc(long iBakashaId, int numProcess)
        {
            _Ovdim = new List<Oved>();
            _iBakashaId = iBakashaId;
            _numProcess = numProcess;
            _iTypeCalc = clGeneral.TypeCalc.Premiya;
           // SetListOvdimLechishuvPremia(iBakashaId, numProcess);
        }
        public MainCalc(long iBakashaId, DateTime dTarMe, DateTime dTarAd, string sMaamad, bool bRitzaGorefet, clGeneral.TypeCalc iTypeCalc, int numProcess)
        {
            try
            {
                _iBakashaId = iBakashaId;
                _dTarMe = dTarMe;
                _dTarAd = dTarAd;
                _sMaamad = sMaamad;
                _bRitzaGorefet = bRitzaGorefet;
                _iTypeCalc = iTypeCalc;
                _numProcess = numProcess;
                _Ovdim = new List<Oved>();
                SetListOvdimLechishuv(dTarMe, dTarAd, sMaamad, bRitzaGorefet, iBakashaId, numProcess);
            }
            catch (Exception ex)
            {

                clLogBakashot.InsertErrorToLog(_iBakashaId, 0, "E", 0, _dTarMe, "MainCalc: " + ex.StackTrace + "\n message: " + ex.Message);
            }
               
        }

        private void SetListOvdimLechishuv(DateTime dTarMe, DateTime dTarAd, string sMaamad, bool bRitzaGorefet, long iBakashaId ,int numProcess)
        {
            Oved ItemOved;
            clCalcDal oCalcDal = new clCalcDal();
            GeneralData oGeneralData;
            DateTime StartTime;
            TimeSpan ts = new TimeSpan();
            try
            {
                oGeneralData = SingleGeneralData.GetInstance(dTarMe, dTarAd, sMaamad, bRitzaGorefet, 0, numProcess);
               
                for (int i = 0; i < oGeneralData.dtOvdimLechishuv.Rows.Count; i++)
                {
                    StartTime = DateTime.Now;
                    ItemOved = new Oved(int.Parse(oGeneralData.dtOvdimLechishuv.Rows[i]["mispar_ishi"].ToString()), DateTime.Parse(oGeneralData.dtOvdimLechishuv.Rows[i]["taarich"].ToString()), dTarMe, dTarAd, iBakashaId);
                    ts = DateTime.Now - StartTime;
                    _Ovdim.Add(ItemOved);
                }

                clLogBakashot.InsertErrorToLog(_iBakashaId, 0, "I", 0, dTarMe, "MainCalc Process Num" + numProcess + ". Mispar Ovdim:" + oGeneralData.dtOvdimLechishuv.Rows.Count);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private void SetListOvdimLechishuvPremia(long lBakashaId,int numProcess)
        {
            Oved ItemOved;
            DataTable dtOvdim = new DataTable();
            clCalcDal oCalcDal = new clCalcDal();
            GeneralData oGeneralData;
            try
            {

                oGeneralData = SingleGeneralData.GetInstance(DateTime.Now, DateTime.Now, "", false, -1, numProcess);
                dtOvdim = oGeneralData.dtOvdimLechishuv;
                for (int i = 0; i < dtOvdim.Rows.Count; i++)
                {
                    ItemOved = new Oved(int.Parse(dtOvdim.Rows[i]["mispar_ishi"].ToString()), DateTime.Parse(dtOvdim.Rows[i]["taarich"].ToString()), DateTime.Now, DateTime.Now, lBakashaId);
                    _Ovdim.Add(ItemOved);
                }

            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        public void CalcOved(Oved oOved)
        {

            CalcMonth oMonth;

            COLL_CHISHUV_SIDUR _collChishuvSidur = new COLL_CHISHUV_SIDUR();
            COLL_CHISHUV_YOMI _collChishuvYomi = new COLL_CHISHUV_YOMI();
            COLL_CHISHUV_CHODESH _collChishuvChodesh = new COLL_CHISHUV_CHODESH();
       
            try
            {
               oOved.SetNetunimLeOved();
               oMonth = new CalcMonth(oOved);
               oMonth.CalcMonthOved();
           
               //שמירת הנתונים רק אם התהליך התבצע ב-batch
               if (oOved.DtYemeyAvoda.Rows.Count > 0)
               {
                   if (_iTypeCalc == clGeneral.TypeCalc.Batch || _iTypeCalc == clGeneral.TypeCalc.Test || _iTypeCalc == clGeneral.TypeCalc.Premiya)
                   {
                       DataSetTurnIntoUdtChodesh(oOved._dsChishuv.Tables["CHISHUV_CHODESH"], ref _collChishuvChodesh);
                       DataSetTurnIntoUdtYom(oOved._dsChishuv.Tables["CHISHUV_YOM"], ref _collChishuvYomi);
                       DataSetTurnIntoUdtSidur(oOved._dsChishuv.Tables["CHISHUV_SIDUR"], ref _collChishuvSidur);
                       if (_iTypeCalc == clGeneral.TypeCalc.Batch || _iTypeCalc == clGeneral.TypeCalc.Premiya)
                       {
                           SaveSidurim(oOved.Mispar_ishi, oOved.DtYemeyAvoda);
                       }
                       //שמירת נתוני החישוב לעובד
                       SaveChishuv(_collChishuvChodesh, _collChishuvYomi, _collChishuvSidur);
                   }
               }
               _collChishuvChodesh = null;
               _collChishuvYomi = null;
               _collChishuvSidur = null;
               oMonth = null;
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_iBakashaId, oOved.Mispar_ishi, "E", 0, oOved.Month, "MainCalc,CalcOved: " + ex.StackTrace + "\n message: " + ex.Message);
                oOved.Dispose();
                throw(ex);
            }
        }

        public void MainCalcOved(int iMisparIshi, long lBakashaId, DateTime dCalcMonth, int iTzuga, ref DataTable dtHeadrut, ref DataTable dtRechivimChodshiym, ref  DataTable dtRikuz1To10, ref DataTable dtRikuz11To20, ref  DataTable dtRikuz21To31, ref DataTable dtAllRikuz, ref DataSet dsRikuz2)
        {
            clUtils oUtils = new clUtils();
            Oved oOved;
            DateTime StartMonth = DateTime.Parse("01/" + dCalcMonth.Month + "/" + dCalcMonth.Year);
            COLL_CHISHUV_YOMI _collChishuvYomi = new COLL_CHISHUV_YOMI();
            COLL_CHISHUV_CHODESH _collChishuvChodesh = new COLL_CHISHUV_CHODESH();
            DataSet dsAllRikuz = new DataSet();
            try
            {
              //  clLogBakashot.InsertErrorToLog(0, 75757, "E", 0, dCalcMonth, "START MainCalcOved:" + iMisparIshi);
                oOved = new Oved(iMisparIshi, StartMonth, StartMonth, dCalcMonth, lBakashaId);
               // clLogBakashot.InsertErrorToLog(0, 75757, "E", 0, dCalcMonth, "START CalcOved:" + iMisparIshi);
                CalcOved(oOved);
             //   clLogBakashot.InsertErrorToLog(0, 75757, "E", 0, dCalcMonth, "END CalcOved:" + iMisparIshi);
                DataSetTurnIntoUdtChodesh(oOved._dsChishuv.Tables["CHISHUV_CHODESH"], ref _collChishuvChodesh);
                DataSetTurnIntoUdtYom(oOved._dsChishuv.Tables["CHISHUV_YOM"], ref _collChishuvYomi);      
                SaveChishuvTemp(oOved.Mispar_ishi, dCalcMonth, iTzuga, _collChishuvChodesh,_collChishuvYomi,ref  dtHeadrut, ref  dtRechivimChodshiym, ref   dtRikuz1To10, ref  dtRikuz11To20, ref   dtRikuz21To31, ref dtAllRikuz);
                SaveChishuvTemp2(oOved.Mispar_ishi, dCalcMonth, _collChishuvChodesh, _collChishuvYomi, ref dsRikuz2);

                oOved.Dispose();
                if (SingleGeneralData.GetInstance() != null)
                    SingleGeneralData.ResetObject();
             //   clLogBakashot.InsertErrorToLog(0, 75757, "E", 0, dCalcMonth, "END MainCalcOved:" + iMisparIshi);
            }
            catch (Exception ex)
            {
                if (SingleGeneralData.GetInstance() != null)
                    SingleGeneralData.ResetObject();
                clLogBakashot.InsertErrorToLog(_iBakashaId, iMisparIshi, "E", 0, dCalcMonth, "MainCalc,MainCalcOved: " + ex.StackTrace + "\n message: " + ex.Message);
            }
        }

        public DataSet CalcDayInMonth(int iMisparIshi, DateTime dCalcDay, long lBakashaId, out bool bStatus)
        {
            bStatus = false;
            Oved oOved;
            CalcMonth oMonth;
            try
            {
                oOved = new Oved(iMisparIshi, dCalcDay, lBakashaId);
                oOved.SetNetunimLeOved();
                oMonth = new CalcMonth(oOved);
                oMonth.CalcMonthOved();
                SingleGeneralData.ResetObject();

                bStatus = true;
                return oOved._dsChishuv;
            }
            catch (Exception ex)
            {
                bStatus = false;
                clLogBakashot.InsertErrorToLog(lBakashaId, iMisparIshi, "E", 0, null, "MainCalc: " + ex.Message);
                return null;
            }
        }

        public void MainCalcTest(DateTime dTarMe, int iMisparIshi)
        {
            long lBakashaId = 1;
            clUtils oUtils = new clUtils();
            Oved oOved;
            CalcMonth oMonth;
            DateTime StartMonth = DateTime.Parse("01/" + dTarMe.Month + "/" + dTarMe.Year);
            COLL_CHISHUV_SIDUR _collChishuvSidur = new COLL_CHISHUV_SIDUR();
            COLL_CHISHUV_YOMI _collChishuvYomi = new COLL_CHISHUV_YOMI();
            COLL_CHISHUV_CHODESH _collChishuvChodesh = new COLL_CHISHUV_CHODESH();
            try
            {
                oOved = new Oved(iMisparIshi, StartMonth, StartMonth, StartMonth.AddMonths(1).AddDays(-1), lBakashaId);
                oOved.SetNetunimLeOved();
                oMonth = new CalcMonth(oOved);
                oMonth.CalcMonthOved();
                DataSetTurnIntoUdtChodesh(oOved._dsChishuv.Tables["CHISHUV_CHODESH"], ref _collChishuvChodesh);
                DataSetTurnIntoUdtYom(oOved._dsChishuv.Tables["CHISHUV_YOM"], ref _collChishuvYomi);
                DataSetTurnIntoUdtSidur(oOved._dsChishuv.Tables["CHISHUV_SIDUR"], ref _collChishuvSidur);
                       
                SaveSidurim(oOved.Mispar_ishi, oOved.DtYemeyAvoda);
                      
                SaveChishuv(_collChishuvChodesh, _collChishuvYomi, _collChishuvSidur);
                oOved.Dispose();
                SingleGeneralData.ResetObject();
            }
            catch (Exception ex)
            {
                SingleGeneralData.ResetObject();
                clLogBakashot.InsertErrorToLog(_iBakashaId, iMisparIshi, "E", 0, StartMonth, "MainCalc: " + ex.Message);
            }
        }

        private void SaveSidurim(int iMisparIshi, DataTable dtYemeyAvoda)
        {
            clTxDal oDal = new clTxDal();
            DataRow[] drChanges;
            int J;
            try
            {   //   שמירת סימון לא לתשלום
                drChanges = dtYemeyAvoda.Select("lo_letashlum=1", "", DataViewRowState.ModifiedCurrent);
                oDal.TxBegin();
                for (J = 0; J < drChanges.Length; J++)
                {
                    oDal.ClearCommand();
                    oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                    oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, drChanges[J]["taarich"], ParameterDir.pdInput);
                    oDal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger, drChanges[J]["mispar_sidur"], ParameterDir.pdInput);
                    oDal.AddParameter("p_shat_hatchala", ParameterType.ntOracleDate, drChanges[J]["shat_hatchala_sidur"], ParameterDir.pdInput);

                    oDal.ExecuteSP(clDefinitions.cProUpdateSidurimLoLetashlum);
                }
                oDal.TxCommit();
            }
            catch (Exception ex)
            {
                oDal.TxRollBack();
                throw ex;
            }
            finally
            {
                drChanges = null;
            }

        }

        private void SaveChishuv(COLL_CHISHUV_CHODESH _collChishuvChodesh,COLL_CHISHUV_YOMI _collChishuvYomi,COLL_CHISHUV_SIDUR _collChishuvSidur)
        {
            clDal oDal = new clDal();

            try
            {   //   שמירת נתוני החישוב
                if (!_collChishuvChodesh.IsNull)
                    oDal.AddParameter("p_coll_chishuv_chodesh", ParameterType.ntOracleArray, _collChishuvChodesh, ParameterDir.pdInput, "COLL_CHISHUV_CHODESH");
                if (!_collChishuvYomi.IsNull)
                    oDal.AddParameter("p_coll_chishuv_yomi", ParameterType.ntOracleArray, _collChishuvYomi, ParameterDir.pdInput, "COLL_CHISHUV_YOMI");
                if (!_collChishuvSidur.IsNull)
                    oDal.AddParameter("p_coll_chishuv_sidur", ParameterType.ntOracleArray, _collChishuvSidur, ParameterDir.pdInput, "COLL_CHISHUV_SIDUR");

                if (!_collChishuvChodesh.IsNull)
                    oDal.ExecuteSP(clDefinitions.cProInsChishuv);
                //SaveChodesh();

                //SaveYom();

                //SaveSidur();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //private void DataSetTurnIntoUdt(DataSet dsChishuv)
        //{
        //    try
        //    {
        //        int I;
        //        DataRow drChishuv;
        //        OBJ_CHISHUV_CHODESH objChsishuvChodesh;
        //        OBJ_CHISHUV_YOMI objChsishuvYomi;
        //        OBJ_CHISHUV_SIDUR objChsishuvSidur;

        //        _collChishuvChodesh = new COLL_CHISHUV_CHODESH();
        //        for (I = 0; I <= dsChishuv.Tables["CHISHUV_CHODESH"].Rows.Count - 1; I++)
        //        {
        //            drChishuv = dsChishuv.Tables["CHISHUV_CHODESH"].Rows[I];
        //            objChsishuvChodesh = new OBJ_CHISHUV_CHODESH();
        //            objChsishuvChodesh.BAKASHA_ID = long.Parse(drChishuv["BAKASHA_ID"].ToString());
        //            objChsishuvChodesh.MISPAR_ISHI = int.Parse(drChishuv["MISPAR_ISHI"].ToString());
        //            objChsishuvChodesh.TAARICH = DateTime.Parse(drChishuv["TAARICH"].ToString());
        //            objChsishuvChodesh.KOD_RECHIV = int.Parse(drChishuv["KOD_RECHIV"].ToString());
        //            objChsishuvChodesh.ERECH_RECHIV = float.Parse(drChishuv["ERECH_RECHIV"].ToString());
        //            _collChishuvChodesh.Add(objChsishuvChodesh);
        //        }

        //        _collChishuvYomi = new COLL_CHISHUV_YOMI();
        //        for (I = 0; I <= dsChishuv.Tables["CHISHUV_YOM"].Rows.Count - 1; I++)
        //        {
        //            drChishuv = dsChishuv.Tables["CHISHUV_YOM"].Rows[I];
        //            objChsishuvYomi = new OBJ_CHISHUV_YOMI();
        //            objChsishuvYomi.BAKASHA_ID = long.Parse(drChishuv["BAKASHA_ID"].ToString());
        //            objChsishuvYomi.MISPAR_ISHI = int.Parse(drChishuv["MISPAR_ISHI"].ToString());
        //            objChsishuvYomi.TAARICH = DateTime.Parse(drChishuv["TAARICH"].ToString());
        //            objChsishuvYomi.KOD_RECHIV = int.Parse(drChishuv["KOD_RECHIV"].ToString());
        //            objChsishuvYomi.ERECH_RECHIV = float.Parse(drChishuv["ERECH_RECHIV"].ToString());
        //            objChsishuvYomi.TKUFA = DateTime.Parse(drChishuv["TKUFA"].ToString());
        //            _collChishuvYomi.Add(objChsishuvYomi);
        //        }

        //        _collChishuvSidur = new COLL_CHISHUV_SIDUR();
        //        for (I = 0; I <= dsChishuv.Tables["CHISHUV_SIDUR"].Rows.Count - 1; I++)
        //        {
        //            drChishuv = dsChishuv.Tables["CHISHUV_SIDUR"].Rows[I];
        //            objChsishuvSidur = new OBJ_CHISHUV_SIDUR();
        //            objChsishuvSidur.BAKASHA_ID = long.Parse(drChishuv["BAKASHA_ID"].ToString());
        //            objChsishuvSidur.MISPAR_ISHI = int.Parse(drChishuv["MISPAR_ISHI"].ToString());
        //            objChsishuvSidur.TAARICH = DateTime.Parse(drChishuv["TAARICH"].ToString());
        //            objChsishuvSidur.KOD_RECHIV = int.Parse(drChishuv["KOD_RECHIV"].ToString());
        //            objChsishuvSidur.ERECH_RECHIV = float.Parse(drChishuv["ERECH_RECHIV"].ToString());
        //            objChsishuvSidur.MISPAR_SIDUR = int.Parse(drChishuv["MISPAR_SIDUR"].ToString());
        //            objChsishuvSidur.SHAT_HATCHALA = DateTime.Parse(drChishuv["SHAT_HATCHALA"].ToString());
        //            _collChishuvSidur.Add(objChsishuvSidur);

        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //    }
        //}


        private void DataSetTurnIntoUdtChodesh(DataTable dtChishuvChodesh,ref COLL_CHISHUV_CHODESH collChishuvChodesh )
        {
            try
            {
                int I;
                DataRow drChishuv;
                OBJ_CHISHUV_CHODESH objChsishuvChodesh;
                for (I = 0; I <= dtChishuvChodesh.Rows.Count - 1; I++)
                {
                    drChishuv = dtChishuvChodesh.Rows[I];
                    objChsishuvChodesh = new OBJ_CHISHUV_CHODESH();
                    objChsishuvChodesh.BAKASHA_ID = long.Parse(drChishuv["BAKASHA_ID"].ToString());
                    objChsishuvChodesh.MISPAR_ISHI = int.Parse(drChishuv["MISPAR_ISHI"].ToString());
                    objChsishuvChodesh.TAARICH = DateTime.Parse(drChishuv["TAARICH"].ToString());
                    objChsishuvChodesh.KOD_RECHIV = int.Parse(drChishuv["KOD_RECHIV"].ToString());
                    objChsishuvChodesh.ERECH_RECHIV = float.Parse(drChishuv["ERECH_RECHIV"].ToString());
                    collChishuvChodesh.Add(objChsishuvChodesh);
                    objChsishuvChodesh = null;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        private void DataSetTurnIntoUdtYom(DataTable dtChishuvYom, ref COLL_CHISHUV_YOMI collChishuvYomi)
        {
            try
            {
                int I;
                DataRow drChishuv;
                OBJ_CHISHUV_YOMI objChsishuvYomi;
                for (I = 0; I <= dtChishuvYom.Rows.Count - 1; I++)
                {
                    drChishuv = dtChishuvYom.Rows[I];
                    objChsishuvYomi = new OBJ_CHISHUV_YOMI();
                    objChsishuvYomi.BAKASHA_ID = long.Parse(drChishuv["BAKASHA_ID"].ToString());
                    objChsishuvYomi.MISPAR_ISHI = int.Parse(drChishuv["MISPAR_ISHI"].ToString());
                    objChsishuvYomi.TAARICH = DateTime.Parse(drChishuv["TAARICH"].ToString());
                    objChsishuvYomi.KOD_RECHIV = int.Parse(drChishuv["KOD_RECHIV"].ToString());
                    objChsishuvYomi.ERECH_RECHIV = float.Parse(drChishuv["ERECH_RECHIV"].ToString());
                    objChsishuvYomi.TKUFA = DateTime.Parse(drChishuv["TKUFA"].ToString());
                    collChishuvYomi.Add(objChsishuvYomi);
                    objChsishuvYomi = null;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        private void DataSetTurnIntoUdtSidur(DataTable dtChishuvSidur,ref COLL_CHISHUV_SIDUR collChishuvSidur)
        {
            try
            {
                int I;
                DataRow drChishuv;
                OBJ_CHISHUV_SIDUR objChsishuvSidur;
                for (I = 0; I <= dtChishuvSidur.Rows.Count - 1; I++)
                {
                    drChishuv = dtChishuvSidur.Rows[I];
                    objChsishuvSidur = new OBJ_CHISHUV_SIDUR();
                    objChsishuvSidur.BAKASHA_ID = long.Parse(drChishuv["BAKASHA_ID"].ToString());
                    objChsishuvSidur.MISPAR_ISHI = int.Parse(drChishuv["MISPAR_ISHI"].ToString());
                    objChsishuvSidur.TAARICH = DateTime.Parse(drChishuv["TAARICH"].ToString());
                    objChsishuvSidur.KOD_RECHIV = int.Parse(drChishuv["KOD_RECHIV"].ToString());
                    objChsishuvSidur.ERECH_RECHIV = float.Parse(drChishuv["ERECH_RECHIV"].ToString());
                    objChsishuvSidur.MISPAR_SIDUR = int.Parse(drChishuv["MISPAR_SIDUR"].ToString());
                    objChsishuvSidur.SHAT_HATCHALA = DateTime.Parse(drChishuv["SHAT_HATCHALA"].ToString());
                    collChishuvSidur.Add(objChsishuvSidur);
                    objChsishuvSidur = null;
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        private void SaveChishuvTemp(int iMisparIshi, DateTime dCalcMonth, int iTzuga,COLL_CHISHUV_CHODESH _collChishuvChodesh,COLL_CHISHUV_YOMI _collChishuvYomi,ref DataTable dtHeadrut, ref DataTable dtRechivimChodshiym, ref  DataTable dtRikuz1To10, ref DataTable dtRikuz11To20, ref  DataTable dtRikuz21To31, ref DataTable dtAllRikuz)
        {
            clTxDal oDal = new clTxDal();
            DataTable dt = new DataTable();
            float fErechRechiv45 = 0;
            try
            {   //   שמירת נתוני החישוב
                oDal.TxBegin();
                oDal.AddParameter("p_coll_chishuv_chodesh", ParameterType.ntOracleArray, _collChishuvChodesh, ParameterDir.pdInput, "COLL_CHISHUV_CHODESH");
                oDal.AddParameter("p_coll_chishuv_yomi", ParameterType.ntOracleArray, _collChishuvYomi, ParameterDir.pdInput, "COLL_CHISHUV_YOMI");
                if (!(_collChishuvChodesh.IsNull) && !(_collChishuvYomi.IsNull))
                    oDal.ExecuteSP(clDefinitions.cProInsChishuvTemp);

                clOvdim oOvdim = new clOvdim();
                oDal.ClearCommand();
                dtHeadrut = oOvdim.GetPirteyHeadrutTemp(ref oDal, iMisparIshi, dCalcMonth, 0);
                oDal.ClearCommand();

                dtRechivimChodshiym = oOvdim.GetRechivimChodshiyimTemp(ref oDal, iMisparIshi, dCalcMonth, 0, iTzuga);
                oDal.ClearCommand();

                fErechRechiv45 = clCalcData.GetSumErechRechiv(dtRechivimChodshiym.Compute("sum(erech_rechiv)", "kod_rechiv=" + clGeneral.enRechivim.SachGmulChisachon.GetHashCode().ToString()));
                oOvdim.GetRikuzChodshiTemp(ref oDal, iMisparIshi, dCalcMonth, 0, iTzuga, fErechRechiv45, ref  dtRikuz1To10, ref  dtRikuz11To20, ref  dtRikuz21To31, ref  dtAllRikuz);

                oDal.TxCommit();
            }
            catch (Exception ex)
            {
                oDal.TxRollBack();
                throw ex;
            }

        }

        private void SaveChishuvTemp2(int iMisparIshi, DateTime dCalcMonth,  COLL_CHISHUV_CHODESH _collChishuvChodesh, COLL_CHISHUV_YOMI _collChishuvYomi,  ref DataSet dsAllRikuz)
        {
            clTxDal oDal = new clTxDal();
          // DataSet ds = new DataSet();
            string names = "";
         //   float fErechRechiv45 = 0;
            try
            {   //   שמירת נתוני החישוב
                oDal.TxBegin();
                oDal.AddParameter("p_coll_chishuv_chodesh", ParameterType.ntOracleArray, _collChishuvChodesh, ParameterDir.pdInput, "COLL_CHISHUV_CHODESH");
                oDal.AddParameter("p_coll_chishuv_yomi", ParameterType.ntOracleArray, _collChishuvYomi, ParameterDir.pdInput, "COLL_CHISHUV_YOMI");
                if (!(_collChishuvChodesh.IsNull) && !(_collChishuvYomi.IsNull))
                    oDal.ExecuteSP(clDefinitions.cProInsChishuvTemp);

                oDal.ClearCommand();

                oDal.AddParameter("p_Cur_Rechivim_Yomi", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names = "Rechivim_Yomi";
                oDal.AddParameter("p_Cur_Rechivim_Chodshi", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Rechivim_Chodshi";
                oDal.AddParameter("p_Cur_Rechivey_Headrut", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Rechivey_Headrut";
                oDal.AddParameter("p_Cur_Num_Rechivim", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                names += ",Num_Rechivim";

                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, dCalcMonth, ParameterDir.pdInput);
                oDal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, 0, ParameterDir.pdInput);

                oDal.ExecuteSP(clGeneral.cProGetRikuzAvodaChodshiTemp, ref dsAllRikuz, names);
           
                oDal.TxCommit();
            }
            catch (Exception ex)
            {
                oDal.TxRollBack();
                throw ex;
            }

        }
        private void InsertOvedToTable(int iMis_Ishi, DateTime dTaarich)
        {
            clDal oDal = new clDal();
            string sSp = "Pkg_Calculation.pro_insert_oved_lechishuv";
            try
            {
                oDal.AddParameter("p_mis_ishi", ParameterType.ntOracleInt64, iMis_Ishi, ParameterDir.pdInput);
                oDal.AddParameter("p_chodesh", ParameterType.ntOracleDate, dTaarich, ParameterDir.pdInput);
                oDal.ExecuteSP(sSp);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //public void CalcOvedPremiya(Oved oOved)
        //{
        //    clCalcDal oCalcDal = new clCalcDal();

        //    COLL_CHISHUV_SIDUR _collChishuvSidur = new COLL_CHISHUV_SIDUR();
        //    COLL_CHISHUV_YOMI _collChishuvYomi = new COLL_CHISHUV_YOMI();
        //    COLL_CHISHUV_CHODESH _collChishuvChodesh = new COLL_CHISHUV_CHODESH();

        //    try
        //    {
        //        CalcOved(oOved);
        //        SaveSidurim(oOved.Mispar_ishi, oOved.DtYemeyAvoda);

        //        //שמירת נתוני החישוב לעובד
        //        DataSetTurnIntoUdtChodesh(oOved._dsChishuv.Tables["CHISHUV_CHODESH"], ref _collChishuvChodesh);
        //        DataSetTurnIntoUdtYom(oOved._dsChishuv.Tables["CHISHUV_YOM"], ref _collChishuvYomi);
        //        DataSetTurnIntoUdtSidur(oOved._dsChishuv.Tables["CHISHUV_SIDUR"], ref _collChishuvSidur);
        //        SaveChishuv(_collChishuvChodesh, _collChishuvYomi, _collChishuvSidur);

        //        oCalcDal.UpdatePremiaBakashaID(oOved.Mispar_ishi, oOved.iBakashaId, oOved.Month);

        //        _collChishuvChodesh = null;
        //        _collChishuvYomi = null;
        //        _collChishuvSidur = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw (ex);
        //        oOved.Dispose();
        //    }
        //}

        public void PremiaCalc()
        {
           clBatch obatch = new clBatch();
            clCalcDal oCalcDal = new clCalcDal();
            int numFailed = 0;
            int numSucceed = 0;
            int result=0;
            try
            {

                clLogBakashot.InsertErrorToLog(_iBakashaId, "I", 0, "Start PremiaCalc");
                result = oCalcDal.PrepareDataLeChishuvPremiyot(_numProcess);
                clGeneral.enBatchExecutionStatus status = clGeneral.enBatchExecutionStatus.Succeeded;
               
                if (result > 0)
                {
                    
                    try
                    {
                        SetListOvdimLechishuvPremia(_iBakashaId, _numProcess);

                        if (Ovdim != null && Ovdim.Count > 0)
                        {
                            #region not parallel
                            Ovdim.ForEach(CurrentOved =>
                            {
                                try
                                {
                                    CalcOved(CurrentOved);

                                    //   oCalcDal.UpdatePremiaBakashaID(CurrentOved.Mispar_ishi, _iBakashaId, CurrentOved.Month);

                                    numSucceed += 1;
                                    CurrentOved.Dispose();
                                }
                                catch (Exception ex)
                                {
                                    numFailed += 1;
                                    //clLogBakashot.SetError(_iBakashaId, CurrentOved.Mispar_ishi, "E",
                                    //    (int)clGeneral.enGeneralBatchType.CalculationForPremiaPopulation,
                                    //    CurrentOved.Month, ex.Message);
                                    //clLogBakashot.InsertErrorToLog();
                                    //status = clGeneral.enBatchExecutionStatus.PartialyFinished;  
                                }
                                finally
                                {
                                    CurrentOved.Dispose();
                                    CurrentOved = null;
                                }

                            });

                            oCalcDal.UpdatePremiaBakashaID(_iBakashaId);

                            #endregion
                        }
                        else if(Ovdim == null)
                        {
                            status = clGeneral.enBatchExecutionStatus.Failed;
                            KdsLibrary.clGeneral.CloseBatchRequest(_iBakashaId, status);
                            clLogBakashot.InsertErrorToLog(_iBakashaId, "E", 0, "PremiaCalc Faild Ovdim is null");
                        }
                        else if (Ovdim.Count == 0)
                        {
                            status = clGeneral.enBatchExecutionStatus.Succeeded;
                            KdsLibrary.clGeneral.CloseBatchRequest(_iBakashaId, status);
                            clLogBakashot.InsertErrorToLog(_iBakashaId, "E", 0, "PremiaCalc: No Ovdim Lechishuv");
                      
                        }
                      
                    }
                    catch (Exception ex)
                    {
                        status = clGeneral.enBatchExecutionStatus.Failed;
                        KdsLibrary.clGeneral.CloseBatchRequest(_iBakashaId, status);
                        clLogBakashot.InsertErrorToLog(_iBakashaId, "E", 0, "PremiaCalc Faild: " + ex.Message);
                        throw (ex);
                    }
                    finally
                    {
                        SingleGeneralData.ResetObject();
                    }
                }
                else status = clGeneral.enBatchExecutionStatus.Failed;

                KdsLibrary.clGeneral.CloseBatchRequest(_iBakashaId, status);
                clLogBakashot.InsertErrorToLog(_iBakashaId, "I", 0, "End PremiaCalc");
             }
            catch (Exception ex)
            {
                KdsLibrary.clGeneral.CloseBatchRequest(_iBakashaId,  clGeneral.enBatchExecutionStatus.Failed);
                clLogBakashot.InsertErrorToLog(_iBakashaId, "E", 0, "PremiaCalc Faild: " + ex.Message);
                throw(ex);
            }

            //    if (_Ovdim != null)
            //    {
            //        foreach (Oved oOved in _Ovdim)
            //        {
            //            try
            //            {
            //                oMonth = new CalcMonth(oOved);
            //                oMonth.CalcMonthOved();

            //                SaveSidurim(oOved.Mispar_ishi, oOved.DtYemeyAvoda);
            //                //שמירת נתוני החישוב לעובד
            //                SaveChishuv(oOved.iBakashaId, oOved.Mispar_ishi);

            //                oCalcDal.UpdatePremiaBakashaID(oOved.Mispar_ishi, oOved.iBakashaId, oOved.Month);
            //                numSucceed += 1;
            //            }
            //            catch (Exception ex)
            //            {
            //                numFailed += 1;
            //                clLogBakashot.SetError(lBakashaId, oOved.Mispar_ishi, "E",
            //                    (int)clGeneral.enGeneralBatchType.CalculationForPremiaPopulation,
            //                    oOved.Month, ex.Message);
            //                clLogBakashot.InsertErrorToLog();
            //                status = clGeneral.enBatchExecutionStatus.PartialyFinished;
            //            }
            //        }
            //    }
            //    else status = clGeneral.enBatchExecutionStatus.Failed;
            //    KdsLibrary.clGeneral.CloseBatchRequest(lBakashaId, status);
            //    obatch.UpdateProcessLog(seq, KdsLibrary.BL.RecordStatus.Finish, "PremiaCalc NumRowsFailed=" + numFailed + " NumRowsSucceed=" + numSucceed, 0);
            //}
            //catch (Exception ex)
            //{
            //    clLogBakashot.SetError(lBakashaId, null, "E",
            //                (int)clGeneral.enGeneralBatchType.CalculationForPremiaPopulation,
            //                DateTime.Now, ex.Message);
            //    clLogBakashot.InsertErrorToLog();
            //}
            //finally
            //{
            //    SingleGeneralData.ResetObject();
            //}
        }

        private DateTime GetEndOfMonth(DateTime date)
        {
            int year = date.Month == 12 ? date.Year + 1 : date.Year;
            int month = date.Month == 12 ? 1 : date.Month + 1;
            return new DateTime(year, month, 1).AddDays(-1);
        }

    }
}
