using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DalOraInfra.DAL;
using KDSCommon.Enums;
using KDSCommon.UDT;

namespace KdsErrors.DAL
{
    public class ErrorsDal
    {
        public const string cProUpdTarRitzatShgiot = "pkg_errors.pro_upd_tar_ritzat_shgiot";
        public const string cFnIsApprovalErrorExists = "PKG_ERRORS.fn_is_approval_errors_exists";
        public const string cFunCountShgiotLetzuga = "pkg_errors.fn_count_shgiot_letzuga";

        public  void DeleteErrorsFromTbShgiot(int iMisparIshi, DateTime CardDate)
        {
            clDal oDal = new clDal();
            try
            {
                oDal.ExecuteSQL(string.Concat("DELETE TB_SHGIOT T WHERE T.MISPAR_ISHI = ", iMisparIshi, " and taarich = to_date('", CardDate.Date.ToString("dd/MM/yyyy"), "','dd/mm/yyyy')"));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertErrorsToTbShgiot(DataTable dtErrors, DateTime dCardDate)
        {
            //כתיבת שגיאות ל-TB_SHGIOT
            clDal oDal = new clDal();
            StringBuilder sbYeshut = new StringBuilder();
            DataSet ds = new DataSet();
            string[] ucols = new string[2];

            try
            {

                //ds.Tables.Add(dtErrors);

                //ucols[0] = "mispar_ishi";
                //ucols[1] = "KOD_SHGIA";
                //oDal.InsertXML(ds.GetXml(), "TB_SHGIOT", ucols);
                int i = 0;
                oDal.ArrayBindCount = dtErrors.Rows.Count;
                int[] arrMisparIshi = new int[dtErrors.Rows.Count];
                int[] arrKodShgia = new int[dtErrors.Rows.Count];
                string[] arrYeshutId = new string[dtErrors.Rows.Count];
                int[] arrMisparSidur = new int[dtErrors.Rows.Count];
                DateTime[] arrTaarich = new DateTime[dtErrors.Rows.Count];
                DateTime[] arrShatHatchala = new DateTime[dtErrors.Rows.Count];
                DateTime[] arrShatYetzia = new DateTime[dtErrors.Rows.Count];
                int[] arrMisparKnisa = new int[dtErrors.Rows.Count];
                string[] arrHeara = new string[dtErrors.Rows.Count];

                foreach (DataRow dr in dtErrors.Rows)
                {
                    arrMisparIshi[i] = int.Parse(dr["mispar_ishi"].ToString());
                    arrKodShgia[i] = int.Parse(dr["check_num"].ToString());
                    sbYeshut.Remove(0, sbYeshut.Length);
                    sbYeshut.Append(string.IsNullOrEmpty(dr["Taarich"].ToString()) ? dCardDate.ToShortDateString() : DateTime.Parse(dr["Taarich"].ToString()).ToShortDateString());
                    //sbYeshut.Append(DateTime.Parse(dr["Taarich"].ToString()).ToShortDateString());
                    sbYeshut.Append(",");
                    sbYeshut.Append(string.IsNullOrEmpty(dr["mispar_sidur"].ToString()) ? "" : string.Concat(dr["mispar_sidur"].ToString(), ","));
                    sbYeshut.Append(string.IsNullOrEmpty(dr["shat_hatchala"].ToString()) ? "" : string.Concat(DateTime.Parse(dr["shat_hatchala"].ToString()).ToString("HH:mm"), ","));
                    sbYeshut.Append(string.IsNullOrEmpty(dr["shat_yetzia"].ToString()) ? "" : string.Concat(DateTime.Parse(dr["shat_yetzia"].ToString()).ToString("HH:mm"), ","));

                    if (string.IsNullOrEmpty(dr["mispar_knisa"].ToString()) || dr["mispar_knisa"].ToString() == "0")
                        sbYeshut.Append("");
                    else sbYeshut.Append(string.Concat(dr["mispar_knisa"].ToString(), ","));

                    //  sbYeshut.Append(string.IsNullOrEmpty(dr["mispar_knisa"].ToString()) ? "" : string.Concat(dr["mispar_knisa"].ToString(), ","));

                    sbYeshut.Append(int.Parse(dr["check_num"].ToString()));
                    sbYeshut.Append(",");
                    sbYeshut.Append(i.ToString());
                    //arrYeshutId[i]=sbYeshut.ToString().Remove(sbYeshut.ToString().Length-1,1);
                    arrYeshutId[i] = sbYeshut.ToString();

                    arrTaarich[i] = (string.IsNullOrEmpty(dr["Taarich"].ToString()) ? DateTime.MinValue : DateTime.Parse(dr["Taarich"].ToString()));
                    arrMisparSidur[i] = string.IsNullOrEmpty(dr["mispar_sidur"].ToString()) ? 0 : (int)dr["mispar_sidur"];
                    arrShatHatchala[i] = string.IsNullOrEmpty(dr["shat_hatchala"].ToString()) ? DateTime.MinValue : DateTime.Parse(dr["shat_hatchala"].ToString());
                    arrShatYetzia[i] = string.IsNullOrEmpty(dr["shat_yetzia"].ToString()) ? DateTime.MinValue : DateTime.Parse(dr["shat_yetzia"].ToString());
                    arrMisparKnisa[i] = string.IsNullOrEmpty(dr["mispar_knisa"].ToString()) ? 0 : (int)dr["mispar_knisa"];

                    //arrHeara[i] = dr["error_desc"].ToString();
                    i++;
                }

                oDal.AddParameter("MISPAR_ISHI", ParameterType.ntOracleInteger, arrMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("KOD_SHGIA", ParameterType.ntOracleInteger, arrKodShgia, ParameterDir.pdInput);
                oDal.AddParameter("YESHUT_ID", ParameterType.ntOracleVarchar, arrYeshutId, ParameterDir.pdInput);
                oDal.AddParameter("TAARICH", ParameterType.ntOracleDate, arrTaarich, ParameterDir.pdInput);
                oDal.AddParameter("MISPAR_SIDUR", ParameterType.ntOracleInteger, arrMisparSidur, ParameterDir.pdInput);
                oDal.AddParameter("SHAT_HATCHALA", ParameterType.ntOracleDate, arrShatHatchala, ParameterDir.pdInput);
                oDal.AddParameter("SHAT_YETZIA", ParameterType.ntOracleDate, arrShatYetzia, ParameterDir.pdInput);
                oDal.AddParameter("MISPAR_KNISA", ParameterType.ntOracleInteger, arrMisparKnisa, ParameterDir.pdInput);
                //oDal.AddParameter("HEARA", ParameterType.ntOracleVarchar, arrHeara, ParameterDir.pdInput);
                //// Set the command text on an OracleCommand object
                //oDal.ExecuteSQL("insert into TB_SHGIOT(MISPAR_ISHI,KOD_SHGIA,YESHUT_ID,TAARICH,MISPAR_SIDUR,SHAT_HATCHALA,SHAT_YETZIA,HEARA) values (:MISPAR_ISHI,:KOD_SHGIA,:YESHUT_ID,:TAARICH,:MISPAR_SIDUR,:SHAT_HATCHALA,:SHAT_YETZIA,:HEARA)");
                oDal.ExecuteSQL("insert into TB_SHGIOT(MISPAR_ISHI,KOD_SHGIA,YESHUT_ID,TAARICH,MISPAR_SIDUR,SHAT_HATCHALA,SHAT_YETZIA,MISPAR_KNISA) values (:MISPAR_ISHI,:KOD_SHGIA,:YESHUT_ID,:TAARICH,:MISPAR_SIDUR,:SHAT_HATCHALA,:SHAT_YETZIA,:MISPAR_KNISA)");

                // //oDal.ExecuteSP("pro_del_tb_shgiot");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateRitzatShgiotDate(int iMisparIshi, DateTime dCardDate, bool bShgiotLetzuga)
        {
            clDal oDal = new clDal();
            try
            {
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_date", ParameterType.ntOracleDate, dCardDate, ParameterDir.pdInput);
                if (bShgiotLetzuga)
                {
                    oDal.AddParameter("p_shgiot_letzuga", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput);
                }
                else
                {
                    oDal.AddParameter("p_shgiot_letzuga", ParameterType.ntOracleInteger, null, ParameterDir.pdInput);
                }
                oDal.ExecuteSP(cProUpdTarRitzatShgiot);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsErrorApprovalExists(ErrorLevel oErrorLevel, int iErrorNum, int iMisparIshi,
                                                 DateTime dCardDate, int iMisparSidur, DateTime dShatHatchala,
                                                 DateTime dShatYetiza, int iKnisaNum)
        {
            //Return true if approval for error exists in table, else false.
            OBJ_SHGIOT_MEUSHAROT oObjShgiotMeusharot = new OBJ_SHGIOT_MEUSHAROT();

            try
            {
                oObjShgiotMeusharot.KOD_SHGIA = iErrorNum;
                oObjShgiotMeusharot.MISPAR_ISHI = iMisparIshi;
                oObjShgiotMeusharot.TAARICH = dCardDate;

                switch (oErrorLevel)
                {
                    case ErrorLevel.LevelYomAvoda:
                        break;
                    case ErrorLevel.LevelSidur:
                        oObjShgiotMeusharot.MISPAR_SIDUR = iMisparSidur;
                        oObjShgiotMeusharot.SHAT_HATCHALA = dShatHatchala;
                        break;
                    case ErrorLevel.LevelPeilut:
                        oObjShgiotMeusharot.MISPAR_SIDUR = iMisparSidur;
                        oObjShgiotMeusharot.SHAT_HATCHALA = dShatHatchala;
                        oObjShgiotMeusharot.SHAT_YETZIA = dShatYetiza;
                        oObjShgiotMeusharot.MISPAR_KNISA = iKnisaNum;
                        break;
                    default:
                        break;
                }
                clDal _Dal = new clDal();

                _Dal.AddParameter("p_result", ParameterType.ntOracleInteger, null, ParameterDir.pdReturnValue);
                _Dal.AddParameter("p_obj_shgiot_meusharot", ParameterType.ntOracleObject, oObjShgiotMeusharot, ParameterDir.pdInput, "OBJ_SHGIOT_MEUSHAROT");
                _Dal.AddParameter("p_level", ParameterType.ntOracleInteger, oErrorLevel.GetHashCode(), ParameterDir.pdInput);
                _Dal.ExecuteSP(cFnIsApprovalErrorExists);

                return int.Parse(_Dal.GetValParam("p_result").ToString()) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool CheckShgiotLetzuga(string sArrKodShgia)
        {
            clDal oDal = new clDal();
            try
            {
                //בודקים אם ישנה פעילות זהה
                //אם כן, נחזיר TRUE
                oDal.AddParameter("p_result", ParameterType.ntOracleInteger, null, ParameterDir.pdReturnValue);
                oDal.AddParameter("p_arr_kod_shgia", ParameterType.ntOracleVarchar, sArrKodShgia, ParameterDir.pdInput, 300);

                oDal.ExecuteSP(cFunCountShgiotLetzuga);

                return int.Parse(oDal.GetValParam("p_result").ToString()) > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
