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
            try
            {
                Ovdim listOvdim = new Ovdim();
                listOvdim.SetListOvdimLechishuv(_dTarMe, _dTarAd, _sMaamad, _bRitzaGorefet, _iBakashaId);
                return listOvdim;
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_iBakashaId, 0, "E", 0, _dTarMe, "getOvdimLechishuv: " + ex.Message);
                throw ex;
            }
        }

         
        public void CalcOved(Oved oOved)
        {
          
            CalcMonth oMonth;
            try
            {
                oMonth = new CalcMonth(oOved);
               // iMisparIshi = int.Parse(dtOvdim.Rows[i]["mispar_ishi"].ToString());
                oMonth.CalcMonthOved();
                DataSetTurnIntoUdt(oOved._dsChishuv);
                //שמירת הנתונים רק אם התהליך התבצע ב-batch
                if (_iTypeCalc == TypeCalc.Batch || _iTypeCalc == TypeCalc.Test)
                {
                    if (_iTypeCalc == TypeCalc.Batch)
                    {
                        SaveSidurim(oOved.Mispar_ishi, clCalcData.DtYemeyAvoda);
                    }
                    //שמירת נתוני החישוב לעובד
                    SaveChishuv(_iBakashaId, oOved.Mispar_ishi);
                }
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_iBakashaId, oOved.Mispar_ishi, "E", 0, oOved.Month, "MainCalc: " + ex.Message);
            }
        }

        public void MainCalcOved(int iMisparIshi, long lBakashaId, DateTime dCalcMonth, int iTzuga, ref DataTable dtHeadrut, ref DataTable dtRechivimChodshiym, ref  DataTable dtRikuz1To10, ref DataTable dtRikuz11To20, ref  DataTable dtRikuz21To31, ref DataTable dtAllRikuz)
        {
            clUtils oUtils = new clUtils();
            Oved oOved;
            DateTime StartMonth = DateTime.Parse("01/" + dCalcMonth.Month + "/" + dCalcMonth.Year);
            try
            {
                InsertOvedToTable(iMisparIshi, StartMonth);
                oOved = new Oved(iMisparIshi, StartMonth, StartMonth, dCalcMonth.AddMonths(1).AddDays(-1));
                CalcOved(oOved);

                SaveChishuvTemp(oOved.Mispar_ishi, dCalcMonth, iTzuga, ref  dtHeadrut, ref  dtRechivimChodshiym, ref   dtRikuz1To10, ref  dtRikuz11To20, ref   dtRikuz21To31, ref dtAllRikuz);

                SingleGeneralData.ResetObject();
            //    clCalcData.ListParametersMonth = null;
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(_iBakashaId, iMisparIshi, "E", 0, dCalcMonth, "MainCalc: " + ex.Message);
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
        }

        private void SaveChishuv(long lbakasha, int imispar)
        {
            clDal oDal = new clDal();

            try
            {   //   שמירת נתוני החישוב
                oDal.AddParameter("p_coll_chishuv_chodesh", ParameterType.ntOracleArray, _collChishuvChodesh, ParameterDir.pdInput, "COLL_CHISHUV_CHODESH");
                oDal.AddParameter("p_coll_chishuv_yomi", ParameterType.ntOracleArray, _collChishuvYomi, ParameterDir.pdInput, "COLL_CHISHUV_YOMI");
                oDal.AddParameter("p_coll_chishuv_sidur", ParameterType.ntOracleArray, _collChishuvSidur, ParameterDir.pdInput, "COLL_CHISHUV_SIDUR");

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

        private void DataSetTurnIntoUdt(DataSet dsChishuv)
        {
            try
            {
                int I;
                DataRow drChishuv;
                OBJ_CHISHUV_CHODESH objChsishuvChodesh;
                OBJ_CHISHUV_YOMI objChsishuvYomi;
                OBJ_CHISHUV_SIDUR objChsishuvSidur;

                _collChishuvChodesh = new COLL_CHISHUV_CHODESH();
                for (I = 0; I <= dsChishuv.Tables["CHISHUV_CHODESH"].Rows.Count - 1; I++)
                {
                    drChishuv = dsChishuv.Tables["CHISHUV_CHODESH"].Rows[I];
                    objChsishuvChodesh = new OBJ_CHISHUV_CHODESH();
                    objChsishuvChodesh.BAKASHA_ID = long.Parse(drChishuv["BAKASHA_ID"].ToString());
                    objChsishuvChodesh.MISPAR_ISHI = int.Parse(drChishuv["MISPAR_ISHI"].ToString());
                    objChsishuvChodesh.TAARICH = DateTime.Parse(drChishuv["TAARICH"].ToString());
                    objChsishuvChodesh.KOD_RECHIV = int.Parse(drChishuv["KOD_RECHIV"].ToString());
                    objChsishuvChodesh.ERECH_RECHIV = float.Parse(drChishuv["ERECH_RECHIV"].ToString());
                    _collChishuvChodesh.Add(objChsishuvChodesh);
                }

                _collChishuvYomi = new COLL_CHISHUV_YOMI();
                for (I = 0; I <= dsChishuv.Tables["CHISHUV_YOM"].Rows.Count - 1; I++)
                {
                    drChishuv = dsChishuv.Tables["CHISHUV_YOM"].Rows[I];
                    objChsishuvYomi = new OBJ_CHISHUV_YOMI();
                    objChsishuvYomi.BAKASHA_ID = long.Parse(drChishuv["BAKASHA_ID"].ToString());
                    objChsishuvYomi.MISPAR_ISHI = int.Parse(drChishuv["MISPAR_ISHI"].ToString());
                    objChsishuvYomi.TAARICH = DateTime.Parse(drChishuv["TAARICH"].ToString());
                    objChsishuvYomi.KOD_RECHIV = int.Parse(drChishuv["KOD_RECHIV"].ToString());
                    objChsishuvYomi.ERECH_RECHIV = float.Parse(drChishuv["ERECH_RECHIV"].ToString());
                    objChsishuvYomi.TKUFA = DateTime.Parse(drChishuv["TKUFA"].ToString());
                    _collChishuvYomi.Add(objChsishuvYomi);
                }

                _collChishuvSidur = new COLL_CHISHUV_SIDUR();
                for (I = 0; I <= dsChishuv.Tables["CHISHUV_SIDUR"].Rows.Count - 1; I++)
                {
                    drChishuv = dsChishuv.Tables["CHISHUV_SIDUR"].Rows[I];
                    objChsishuvSidur = new OBJ_CHISHUV_SIDUR();
                    objChsishuvSidur.BAKASHA_ID = long.Parse(drChishuv["BAKASHA_ID"].ToString());
                    objChsishuvSidur.MISPAR_ISHI = int.Parse(drChishuv["MISPAR_ISHI"].ToString());
                    objChsishuvSidur.TAARICH = DateTime.Parse(drChishuv["TAARICH"].ToString());
                    objChsishuvSidur.KOD_RECHIV = int.Parse(drChishuv["KOD_RECHIV"].ToString());
                    objChsishuvSidur.ERECH_RECHIV = float.Parse(drChishuv["ERECH_RECHIV"].ToString());
                    objChsishuvSidur.MISPAR_SIDUR = int.Parse(drChishuv["MISPAR_SIDUR"].ToString());
                    objChsishuvSidur.SHAT_HATCHALA = DateTime.Parse(drChishuv["SHAT_HATCHALA"].ToString());
                    _collChishuvSidur.Add(objChsishuvSidur);

                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private void SaveChishuvTemp(int iMisparIshi, DateTime dCalcMonth, int iTzuga, ref DataTable dtHeadrut, ref DataTable dtRechivimChodshiym, ref  DataTable dtRikuz1To10, ref DataTable dtRikuz11To20, ref  DataTable dtRikuz21To31, ref DataTable dtAllRikuz)
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
    }
}
