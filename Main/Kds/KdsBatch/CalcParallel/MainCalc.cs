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
        //#region Definitions
        ////public enum TypeCalc
        ////{ Batch = 1, OnLine = 2, Test = 3 }

        //private COLL_CHISHUV_SIDUR _collChishuvSidur;
        //private COLL_CHISHUV_YOMI _collChishuvYomi;
        //private COLL_CHISHUV_CHODESH _collChishuvChodesh;
        //#endregion

        clGeneral.TypeCalc _iTypeCalc;
        private long _iBakashaId;
        private string _sMaamad;
        private bool _bRitzaGorefet;
        private DateTime _dTarMe;
        private DateTime _dTarAd;
        private List<Oved> _Ovdim;
        public List<Oved> Ovdim
        {
            get { return _Ovdim; }
        }
        public MainCalc()
        {
        }
        //public MainCalc(long iBakashaId)
        //{
        //    //_Ovdim = new List<Oved>();
        //   // SetListOvdimLechishuvPremia(iBakashaId);

        //}
        public MainCalc(long iBakashaId, DateTime dTarMe, DateTime dTarAd, string sMaamad, bool bRitzaGorefet, clGeneral.TypeCalc iTypeCalc)
        {
            _iBakashaId = iBakashaId;
            _dTarMe = dTarMe;
            _dTarAd = dTarAd;
            _sMaamad = sMaamad;
            _bRitzaGorefet = bRitzaGorefet;
            _iTypeCalc = iTypeCalc;
            _Ovdim = new List<Oved>();
            SetListOvdimLechishuv(dTarMe, dTarAd, sMaamad, bRitzaGorefet, iBakashaId);
        }

        private void SetListOvdimLechishuv(DateTime dTarMe, DateTime dTarAd, string sMaamad, bool bRitzaGorefet, long iBakashaId)
        {
            Oved ItemOved;
            DataTable dtOvdim = new DataTable();
            clCalcDal oCalcDal = new clCalcDal();
            GeneralData oGeneralData;
            DateTime StartTime;
            TimeSpan ts = new TimeSpan();
            try
            {
                oGeneralData = SingleGeneralData.GetInstance(dTarMe, dTarAd, sMaamad, bRitzaGorefet, 0);
               
                /**/
                // dtOvdim = oCalcDal.GetOvdimLechishuv(dTarMe, dTarAd, sMaamad, bRitzaGorefet);
                dtOvdim = oGeneralData._dtOvdimLechishuv;
                clLogBakashot.InsertErrorToLog(_iBakashaId, 0, "I", 0, dTarMe, "MainCalc: After storeProcidure Before inilize Ovdim. Mispar Ovdim:" + dtOvdim.Rows.Count);
                for (int i = 0; i < dtOvdim.Rows.Count; i++)
                {


                    StartTime = DateTime.Now;
                    ItemOved = new Oved(int.Parse(dtOvdim.Rows[i]["mispar_ishi"].ToString()), DateTime.Parse(dtOvdim.Rows[i]["chodesh"].ToString()), dTarMe, dTarAd, iBakashaId);
                    ts = DateTime.Now - StartTime;
                    _Ovdim.Add(ItemOved);

                }
                clLogBakashot.InsertErrorToLog(_iBakashaId, 0, "I", 0, dTarMe, "MainCalc:After inilize Ovdim. Mispar Ovdim LeChishuv:  " + dtOvdim.Rows.Count);
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private void SetListOvdimLechishuvPremia(long lBakashaId)
        {
            Oved ItemOved;
            DataTable dtOvdim = new DataTable();
            clCalcDal oCalcDal = new clCalcDal();
            GeneralData oGeneralData;
            try
            {
                _Ovdim = new List<Oved>();
                oGeneralData = SingleGeneralData.GetInstance(DateTime.Now, DateTime.Now, "", false, -1);
                dtOvdim = oGeneralData._dtOvdimLechishuv;
                for (int i = 0; i < dtOvdim.Rows.Count; i++)
                {
                    ItemOved = new Oved(int.Parse(dtOvdim.Rows[i]["mispar_ishi"].ToString()), DateTime.Parse(dtOvdim.Rows[i]["chodesh"].ToString()), DateTime.Now, DateTime.Now, lBakashaId);
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
            COLL_CHISHUV_SIDUR _collChishuvSidur;
            COLL_CHISHUV_YOMI _collChishuvYomi;
            COLL_CHISHUV_CHODESH _collChishuvChodesh;
       
            try
            {
               oOved.SetNetunimLeOved();
                oMonth = new CalcMonth(oOved);
                // iMisparIshi = int.Parse(dtOvdim.Rows[i]["mispar_ishi"].ToString());
                oMonth.CalcMonthOved();
               // DataSetTurnIntoUdt(oOved._dsChishuv);
              
                //שמירת הנתונים רק אם התהליך התבצע ב-batch
                if (_iTypeCalc == clGeneral.TypeCalc.Batch || _iTypeCalc == clGeneral.TypeCalc.Test)
                {
                    _collChishuvChodesh = DataSetTurnIntoUdtChodesh(oOved._dsChishuv.Tables["CHISHUV_CHODESH"]);
                    _collChishuvYomi = DataSetTurnIntoUdtYom(oOved._dsChishuv.Tables["CHISHUV_YOM"]);
                    _collChishuvSidur = DataSetTurnIntoUdtSidur(oOved._dsChishuv.Tables["CHISHUV_SIDUR"]);
                    if (_iTypeCalc == clGeneral.TypeCalc.Batch)
                    {
                        SaveSidurim(oOved.Mispar_ishi, oOved.DtYemeyAvoda);
                    }
                    //שמירת נתוני החישוב לעובד
                    SaveChishuv(_collChishuvChodesh, _collChishuvYomi, _collChishuvSidur);
                }
                oOved.Dispose();
            }
            catch (Exception ex)
            {

                clLogBakashot.InsertErrorToLog(_iBakashaId, oOved.Mispar_ishi, "E", 0, oOved.Month, "MainCalc: " + ex.Message);
                oOved.Dispose();
            }
        }

        public void MainCalcOved(int iMisparIshi, long lBakashaId, DateTime dCalcMonth, int iTzuga, ref DataTable dtHeadrut, ref DataTable dtRechivimChodshiym, ref  DataTable dtRikuz1To10, ref DataTable dtRikuz11To20, ref  DataTable dtRikuz21To31, ref DataTable dtAllRikuz)
        {
            clUtils oUtils = new clUtils();
            Oved oOved;
            DateTime StartMonth = DateTime.Parse("01/" + dCalcMonth.Month + "/" + dCalcMonth.Year);
            COLL_CHISHUV_YOMI _collChishuvYomi;
            COLL_CHISHUV_CHODESH _collChishuvChodesh;
            try
            {
                //InsertOvedToTable(iMisparIshi, StartMonth);
                oOved = new Oved(iMisparIshi, StartMonth, StartMonth, dCalcMonth, lBakashaId);
                CalcOved(oOved);
                _collChishuvChodesh = DataSetTurnIntoUdtChodesh(oOved._dsChishuv.Tables["CHISHUV_CHODESH"]);
                _collChishuvYomi = DataSetTurnIntoUdtYom(oOved._dsChishuv.Tables["CHISHUV_YOM"]);      
                SaveChishuvTemp(oOved.Mispar_ishi, dCalcMonth, iTzuga, _collChishuvChodesh,_collChishuvYomi,ref  dtHeadrut, ref  dtRechivimChodshiym, ref   dtRikuz1To10, ref  dtRikuz11To20, ref   dtRikuz21To31, ref dtAllRikuz);

                oOved.Dispose();
                SingleGeneralData.ResetObject();
            }
            catch (Exception ex)
            {
                SingleGeneralData.ResetObject();
                clLogBakashot.InsertErrorToLog(_iBakashaId, iMisparIshi, "E", 0, dCalcMonth, "MainCalc: " + ex.Message);
            }
        }

        public DataSet CalcDayInMonth(int iMisparIshi, DateTime dCalcDay, long lBakashaId, out bool bStatus)
        {
            bStatus = false;
            Oved oOved;
            try
            {
                //  InsertOvedToTable(iMisparIshi, dCalcDay);
                oOved = new Oved(iMisparIshi, dCalcDay, lBakashaId);
                CalcOved(oOved);
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

        //public void MainCalcTest(DateTime dTarMe, int iMisparIshi)
        //{
        //    long lBakashaId = 0;
        //    DateTime dTarAd;
        //    int iStatus = 0;
        //    clUtils oUtils = new clUtils();
        //    Oved oOved;
        //    COLL_CHISHUV_SIDUR _collChishuvSidur;
        //    COLL_CHISHUV_YOMI _collChishuvYomi;
        //    COLL_CHISHUV_CHODESH _collChishuvChodesh;
        //    try
        //    {
        //        dTarAd = dTarMe.AddMonths(1).AddDays(-1);

        //        //   InsertOvedToTable(iMisparIshi, dTarMe);
        //        oOved = new Oved(iMisparIshi, dTarMe, dTarMe, dTarAd, lBakashaId);
        //        CalcOved(oOved);

        //        SaveSidurim(iMisparIshi, oOved.DtYemeyAvoda);
        //        //שמירת נתוני החישוב לעובד
        //        SaveChishuv(0, iMisparIshi);
        //        iStatus = clGeneral.enStatusRequest.ToBeEnded.GetHashCode();
        //    }
        //    catch (Exception ex)
        //    {
        //        iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
        //        clLogBakashot.InsertErrorToLog(lBakashaId, iMisparIshi, "E", 0, dTarMe, "MainCalcTest: " + ex.Message);
        //    }
        //    finally
        //    {
        //        clDefinitions.UpdateLogBakasha(lBakashaId, DateTime.Now, iStatus);
        //    }
        //}

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


        private COLL_CHISHUV_CHODESH DataSetTurnIntoUdtChodesh(DataTable dtChishuvChodesh)
        {
            try
            {
                int I;
                DataRow drChishuv;
                OBJ_CHISHUV_CHODESH objChsishuvChodesh;
                COLL_CHISHUV_CHODESH collChishuvChodesh = new COLL_CHISHUV_CHODESH();
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
                }

                return collChishuvChodesh;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        private COLL_CHISHUV_YOMI DataSetTurnIntoUdtYom(DataTable dtChishuvYom)
        {
            try
            {
                int I;
                DataRow drChishuv;
                OBJ_CHISHUV_YOMI objChsishuvYomi;
                COLL_CHISHUV_YOMI collChishuvYomi = new COLL_CHISHUV_YOMI();
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
                }

                return collChishuvYomi;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
        private COLL_CHISHUV_SIDUR DataSetTurnIntoUdtSidur(DataTable dtChishuvSidur)
        {
            try
            {
                int I;
                DataRow drChishuv;
                OBJ_CHISHUV_SIDUR objChsishuvSidur;
                COLL_CHISHUV_SIDUR collChishuvSidur = new COLL_CHISHUV_SIDUR();
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
                }

                return collChishuvSidur;
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


        //public void PremiaCalc()
        //{
        //    clBatch obatch = new clBatch();
        //    clCalcDal oCalcDal = new clCalcDal();
        //    List<Oved> ListOvdim = new List<Oved>();
        //    CalcMonth oMonth;
        //    long lBakashaId = 0;
        //    int numFailed = 0;
        //    int numSucceed = 0;
        //    int seq = 0;

        //    try
        //    {

        //        seq = obatch.InsertProcessLog(98, 0, KdsLibrary.BL.RecordStatus.Wait, "PremiaCalc", 0);
        //        lBakashaId = clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.CalculationForPremiaPopulation, "KdsScheduler", -12);
        //        SetListOvdimLechishuvPremia(lBakashaId);
        //        clGeneral.enBatchExecutionStatus status = clGeneral.enBatchExecutionStatus.Succeeded;

        //        if (_Ovdim != null)
        //        {
        //            foreach (Oved oOved in _Ovdim)
        //            {
        //                try
        //                {
        //                    oMonth = new CalcMonth(oOved);
        //                    oMonth.CalcMonthOved();

        //                    SaveSidurim(oOved.Mispar_ishi, oOved.DtYemeyAvoda);
        //                    //שמירת נתוני החישוב לעובד
        //                    SaveChishuv(oOved.iBakashaId, oOved.Mispar_ishi);

        //                    oCalcDal.UpdatePremiaBakashaID(oOved.Mispar_ishi, oOved.iBakashaId, oOved.Month);
        //                    numSucceed += 1;
        //                }
        //                catch (Exception ex)
        //                {
        //                    numFailed += 1;
        //                    clLogBakashot.SetError(lBakashaId, oOved.Mispar_ishi, "E",
        //                        (int)clGeneral.enGeneralBatchType.CalculationForPremiaPopulation,
        //                        oOved.Month, ex.Message);
        //                    clLogBakashot.InsertErrorToLog();
        //                    status = clGeneral.enBatchExecutionStatus.PartialyFinished;
        //                }
        //            }
        //        }
        //        else status = clGeneral.enBatchExecutionStatus.Failed;
        //        KdsLibrary.clGeneral.CloseBatchRequest(lBakashaId, status);
        //        obatch.UpdateProcessLog(seq, KdsLibrary.BL.RecordStatus.Finish, "PremiaCalc NumRowsFailed=" + numFailed + " NumRowsSucceed=" + numSucceed, 0);
        //    }
        //    catch (Exception ex)
        //    {
        //        clLogBakashot.SetError(lBakashaId, null, "E",
        //                    (int)clGeneral.enGeneralBatchType.CalculationForPremiaPopulation,
        //                    DateTime.Now, ex.Message);
        //        clLogBakashot.InsertErrorToLog();
        //    }
        //    finally
        //    {
        //        SingleGeneralData.ResetObject();
        //    }
        //}

        private DateTime GetEndOfMonth(DateTime date)
        {
            int year = date.Month == 12 ? date.Year + 1 : date.Year;
            int month = date.Month == 12 ? 1 : date.Month + 1;
            return new DateTime(year, month, 1).AddDays(-1);
            // return new DateTime(year, (date.Month + 1) % 12, 1).AddDays(-1);
        }

    }
}
