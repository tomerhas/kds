﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary;
using System.IO;
using System.Configuration;
using System.Web;
using KDSCommon.UDT;
using DalOraInfra.DAL;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.Interfaces.Logs;
namespace KdsBatch
{
   public class clTransferToHilan
    {
       long _lBakashaId;

       // private clEruaDataEt oDataEt;
       //private clEruaBakaraEt oBakaraEt;
       //private clErua462 oErua462;
       //private clErua589 oErua589;
       //private clErua413 oErua413;
       //private clErua415 oErua415;
       //private clErua416 oErua416;
       //private clErua417 oErua417;
       //private  clErua418 oErua418;
       //private clErua419 oErua419;
       //private clErua460 oErua460;
       private StreamWriter sFileStrCh, sFileStrS, sFileStrC, sFileStrEt, sFileStrEtBakaraReg, sFileStrEtBakaraHef;
       private StreamWriter _sFileToWrite;
       private DataTable dtEzerYomi;
       private List<PirteyOved> _PirteyOved;
       private enum enFileType
       {
           Friends = 1, 
           Salarieds = 2,
           Choze=23,
           EtBakaraReg=4,
           EtBakaraHef = 5,
           EtData=6
       }

       public void Transfer(long lBakashaId, long lRequestNumToTransfer)
       {

           int iMisparIshi, i, iMaamad, iMaamadRashi;
           DataTable dtOvdim, dtRechivim, dtPrem, dtRechivimYomi,dtChufshaRezufa;
           DataSet dsNetunim;
           int iStatus = 0;
           string bDelete = ConfigurationSettings.AppSettings["DeleteTablesAfterTransfer"];
           string sPathFile = ConfigurationSettings.AppSettings["PathFileTransfer"];
           string sChodeshIbud, sFileNameSchirim, sFileNameChaverim, sFileNameChozim, sFileNameETBTashlum, sFileNameETBakaraReg, sFileNameETBakaraHef;
           PirteyOved oPirteyOved;
           DateTime dChodesh;
           COLL_MISPAR_ISHI_SUG_CHISHUV objCollMisparIshiSugChishuv = new COLL_MISPAR_ISHI_SUG_CHISHUV();
           OBJ_MISPAR_ISHI_SUG_CHISHUV objMisparIshiSugChishuv; 
           int iDirug, iDarga;

           sFileNameChaverim = "EGD3NOCH.txt"; //"EGD3TEST.txt"; //
           sFileNameSchirim = "EGD6NOCH.txt"; //"EGD6TEST.txt"; //
           sFileNameChozim = "EGB2NOCH.txt"; //"EGB2TEST.txt"; //
           sFileNameETBTashlum = "QDIVmmyy.162";
           sFileNameETBakaraReg = "REGLmmyy.162";
           sFileNameETBakaraHef = "HEFR_Cmmyy.162";

           _lBakashaId = lBakashaId;

           try
           {
               sChodeshIbud = string.Empty;
               sFileStrS = new StreamWriter(sPathFile + sFileNameSchirim, false, Encoding.Default);
               sFileStrCh = new StreamWriter(sPathFile + sFileNameChaverim, false, Encoding.Default);
               sFileStrC = new StreamWriter(sPathFile + sFileNameChozim, false, Encoding.Default);
               

               try
               {
                   ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lBakashaId, "I", 0, "Transfer, before GetOvdimToTransfer");
                   dsNetunim = GetOvdimToTransfer(lRequestNumToTransfer);

                   dtRechivimYomi = GetRechivimYomiim(lRequestNumToTransfer);
                   dtChufshaRezufa = GetOvdimWithChufshaRezufa(lRequestNumToTransfer);
                   ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lBakashaId, "I", 0, "Transfer, after GetOvdimToTransfer");
                  
                   dtOvdim = dsNetunim.Tables[0];
                   dtRechivim = dsNetunim.Tables[1];
                   dtPrem = dsNetunim.Tables[2];

                   ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lBakashaId, "I", 0, "count:" + dtOvdim.Rows.Count);
                   _PirteyOved = new List<PirteyOved>();
                  // dtEzerYomi = new DataTable();
                   for (i = 0; i <= dtOvdim.Rows.Count - 1; i++)
                   {

                       iMisparIshi = int.Parse(dtOvdim.Rows[i]["mispar_ishi"].ToString());
                       dChodesh = DateTime.Parse(dtOvdim.Rows[i]["taarich"].ToString());

                       //try
                       //{

                       if (i == 0)
                           sChodeshIbud = dtOvdim.Rows[i]["chodesh_ibud"].ToString();
                       if(i % 100 ==0)
                           ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lBakashaId, "I", 0, "after " + i + " ovdim");
                  
                       oPirteyOved = new PirteyOved(lBakashaId, lRequestNumToTransfer, dtOvdim.Rows[i]);
                       oPirteyOved.sChodeshIbud = sChodeshIbud;
                       //oPirteyOved.iCntYamim = GetCntYamimToOved(iMisparIshi, dtRechivimYomi, dChodesh);
                       //oPirteyOved._dtChishuv = GetChishuvYomiToOved(iMisparIshi, dtRechivimYomi);
                       //עובדי קייטנה 
                       //לא מבצעים להם העברה לשכר

                       oPirteyOved.InitializeErueyOved(dtRechivim, dtPrem, dtRechivimYomi, dtChufshaRezufa);
                      // oPirteyOved.InitializeErueyOved(dtRechivim, dtPrem);

                       _PirteyOved.Add(oPirteyOved);

                       objMisparIshiSugChishuv = new OBJ_MISPAR_ISHI_SUG_CHISHUV();
                       SetSugChishuvUDT(iMisparIshi, dChodesh, oPirteyOved, ref objMisparIshiSugChishuv);
                       objCollMisparIshiSugChishuv.Add(objMisparIshiSugChishuv);

                       //if (i%50 ==0)
                       //   clLogBakashot.InsertErrorToLog(lBakashaId, "I", 0, "Transfer i=" + i);
                       //}
                       //catch (Exception ex)
                       //{
                       //    clLogBakashot.InsertErrorToLog(lBakashaId, iMisparIshi, "E", 0, dChodesh, "Transfer: " + ex.Message);
                       //}

                       // ClearObject();
                   }

                   if (sFileStrEt == null && _PirteyOved.Exists( item =>(item.iDirug == 85 && item.iDarga == 30)) )
                   {
                       sFileStrEt = new StreamWriter(sPathFile + sFileNameETBTashlum.Replace("mmyy", sChodeshIbud.Substring(0, 2) + sChodeshIbud.Substring(5, 2)), false, Encoding.Default);

                       sFileStrEtBakaraReg = new StreamWriter(sPathFile + sFileNameETBakaraReg.Replace("mmyy", sChodeshIbud.Substring(0, 2) + sChodeshIbud.Substring(5, 2)), false, Encoding.Default);
                       sFileStrEtBakaraHef = new StreamWriter(sPathFile + sFileNameETBakaraHef.Replace("mmyy", sChodeshIbud.Substring(0, 2) + sChodeshIbud.Substring(5, 2)), false, Encoding.Default);
                    }
                   ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lBakashaId, "I", 0, "Transfer, before WriteEruimToFile");
                   _PirteyOved.ForEach(item => { WriteEruimToFile(item); });
                  // WriteToFile(iMaamad, iMaamadRashi, iDirug, iDarga);
                   ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lBakashaId, "I", 0, "Transfer, after WriteEruimToFile");
                   
                   UpdateStatusYameyAvoda(lRequestNumToTransfer);
                   InserIntoTableSugChishuv(objCollMisparIshiSugChishuv, lRequestNumToTransfer);

                   if (bDelete == "true")
                       DeleteChishuvAfterTransfer(lRequestNumToTransfer);
                   //UpdateOvdimImShinuyHr(lBakashaId,lRequestNumToTransfer);
                   HttpRuntime.Cache.Remove(ConfigurationSettings.AppSettings["TakanonSizialiCachName"]);
                   iStatus = clGeneral.enStatusRequest.ToBeEnded.GetHashCode();
               }
               catch (Exception ex)
               {
                   iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
                   ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lBakashaId, "E", 0, "Transfer: " + ex.Message);
               }
               finally
               {
                   CloseFile();
                   if (iStatus == clGeneral.enStatusRequest.ToBeEnded.GetHashCode())
                   {
                       clDefinitions.UpdateLogBakasha(lBakashaId, DateTime.Now, iStatus);
                       clDefinitions.UpdateLogBakasha(lRequestNumToTransfer, 1, DateTime.Now);
                   }
                   else { clDefinitions.UpdateLogBakasha(lBakashaId, DateTime.Now, iStatus); }
               }
           }
           catch (Exception ex)
           {
               clDefinitions.UpdateLogBakasha(lBakashaId, DateTime.Now, clGeneral.enStatusRequest.Failure.GetHashCode());
               ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(lBakashaId, "E", 0, "Transfer: " + ex.Message);
           }
       }
     

       ////private DataTable GetChishuvYomiToOved(int iMisparIshi, DataTable dtRechivimYomiim)
       ////{
       ////    clDal oDal = new clDal();
       ////    DataRow[] rows;
       ////    try
       ////    {
       ////        rows = dtRechivimYomiim.Select("mispar_ishi= " + iMisparIshi);
       ////        if (rows.Length > 0)
       ////        {
       ////            dtEzerYomi = rows.CopyToDataTable();
       ////        }
       ////        else
       ////        {
       ////            dtEzerYomi = dtRechivimYomiim.Clone();
       ////        }
       ////        return dtEzerYomi;
       ////    }
       ////    catch (Exception ex)
       ////    {
       ////     //   //clLogBakashot.SetError(iBakashaId, iMisparIshi, "E", 0, null, "GetChishuvYomiToOved: " + ex.Message);
       ////        throw ex;
       ////    }
       ////}

       private void SetSugChishuvUDT(int mispar_ishi,DateTime dTaarich, PirteyOved oPirteyOved, ref OBJ_MISPAR_ISHI_SUG_CHISHUV objMisparIshiSugChishuv)
       {
           try
           {
               objMisparIshiSugChishuv.MISPAR_ISHI =mispar_ishi;
               objMisparIshiSugChishuv.TAARICH = dTaarich;
               objMisparIshiSugChishuv.BAKASHA_ID = oPirteyOved.iBakashaIdRizatChishuv;
               objMisparIshiSugChishuv.SUG_CHISHUV = 0;

               if (oPirteyOved.iDirug != 85 && oPirteyOved.iDarga != 30)
               {
                   if ((oPirteyOved.oErua413 != null && oPirteyOved.oErua413.bKayamEfreshBErua) ||
                       (oPirteyOved.oErua415 != null && oPirteyOved.oErua415.bKayamEfreshBErua) ||
                       (oPirteyOved.oErua416 != null && oPirteyOved.oErua416.bKayamEfreshBErua) || 
                       (oPirteyOved.oErua417 != null && oPirteyOved.oErua417.bKayamEfreshBErua) ||
                       (oPirteyOved.oErua418 != null && oPirteyOved.oErua418.bKayamEfreshBErua) || 
                       (oPirteyOved.oErua419 != null && oPirteyOved.oErua419.bKayamEfreshBErua) ||
                       (oPirteyOved.oErua589 != null && oPirteyOved.oErua589.bKayamEfreshBErua) )
                        objMisparIshiSugChishuv.SUG_CHISHUV = 1;
               }
           }
           catch (Exception ex)
           {
               ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(_lBakashaId, "E",0,  "SetSugChishuvUDT: " + ex.Message, mispar_ishi,null);
               throw (ex);
          }
       }
       private void CloseFile()
       {
           if (!(sFileStrCh == null))
           {
               sFileStrCh.Close();
           }
           if (!(sFileStrS == null))
           {
               sFileStrS.Close();
           }
           if (!(sFileStrC == null))
           {
               sFileStrC.Close();
           }
           if (!(sFileStrEt == null))
           {
               sFileStrEt.Close();
           }
           if (!(sFileStrEtBakaraReg == null))
           {
               sFileStrEtBakaraReg.Close();
           }
           if (!(sFileStrEtBakaraHef == null))
           {
               sFileStrEtBakaraHef.Close();
           }
       }

       //private void ClearObject()
       //{
       //    oErua413 = null;
       //    oErua415 = null;
       //    oErua416 = null;
       //    oErua417 = null;
       //    oErua418 = null;
       //    oErua419 = null;
       //    oErua589 = null;
       //    oErua460 = null;
       //    oErua462 = null;
       //    oDataEt = null;
       //    oBakaraEt = null;
       //}

       private void WriteEruimToFile(PirteyOved oOved)
       {

           if (oOved.iDirug == 85 && oOved.iDarga == 30)
           {
               if (oOved.oBakaraEt.TypeFile == clEruaBakaraEt.enTypeFile.Ragil.GetHashCode())
                    WriteEruaToFile(sFileStrEtBakaraReg, oOved.oBakaraEt);
               else
                    WriteEruaToFile(sFileStrEtBakaraHef, oOved.oBakaraEt);

               WriteEruaToFile(sFileStrEt, oOved.oDataEt);
           }
          else
           {
               _sFileToWrite = GetFileToWrite(oOved.iMaamad, oOved.iMaamadRashi, oOved.iDirug, oOved.iDarga);

               WriteEruaToFile(_sFileToWrite, oOved.oErua413);
               WriteEruaToFile(_sFileToWrite, oOved.oErua415);
               WriteEruaToFile(_sFileToWrite, oOved.oErua416);
               WriteEruaToFile(_sFileToWrite, oOved.oErua417);
               WriteEruaToFile(_sFileToWrite, oOved.oErua418);
               WriteEruaToFile(_sFileToWrite, oOved.oErua419);
               WriteEruaToFile(_sFileToWrite, oOved.oErua460);
               WriteEruaToFile(_sFileToWrite, oOved.oErua462);
               WriteEruaToFile(_sFileToWrite, oOved.oErua589);
           }
       }
       private void WriteEruaToFile(StreamWriter oFile, clErua oErua)
       {
           if (oErua != null)
           {
               oErua.Lines.ForEach(delegate(string Line)
               {
                   oFile.WriteLine(Line);
                   oFile.Flush();
               });
           }
       }


       //private void WriteToFile(int iMaamad, int iMaamadRashi, int iDirug, int iDarga)
       //{

       //    if (iDirug == 85 && iDarga == 30)
       //    {
       //        _sFileToWrite = GetFileToWrite(enFileType.EtBakara.GetHashCode(), iMaamadRashi, iDirug, iDarga);

       //        WriteErua(oBakaraEt);

       //        _sFileToWrite = GetFileToWrite(enFileType.EtData.GetHashCode(), iMaamadRashi, iDirug, iDarga);

       //        WriteErua(oDataEt);
       //    }
       //    else
       //    {
       //        _sFileToWrite = GetFileToWrite(iMaamad, iMaamadRashi, iDirug, iDarga);

       //        WriteErua(oErua413);
       //        WriteErua(oErua415);
       //        WriteErua(oErua416);
       //        WriteErua(oErua417);
       //        WriteErua(oErua418);
       //        WriteErua(oErua419);
       //        WriteErua(oErua460);
       //        WriteErua(oErua462);
       //        WriteErua(oErua589);
       //    }
       //}

       private void WriteErua(clErua oErua)
       {
           if (oErua != null)
           {
               oErua.Lines.ForEach(delegate(string Line)
               {
                   _sFileToWrite.WriteLine(Line);
                   _sFileToWrite.Flush();
               });
           }
       }

     
       private StreamWriter GetFileToWrite(int iFileType, int iMaamadRashi, int iDirug, int iDarga)
       {
           StreamWriter sFileToWrite;
           if (iFileType == enFileType.EtBakaraReg.GetHashCode())
           {
               sFileToWrite = sFileStrEtBakaraReg;
           }
           else if (iFileType == enFileType.EtBakaraHef.GetHashCode())
           {
               sFileToWrite = sFileStrEtBakaraHef;
           }
           else if (iFileType == enFileType.EtData.GetHashCode())
           {
               sFileToWrite = sFileStrEt;
           }
           else
           {
               if (iMaamadRashi == clGeneral.enMaamad.Friends.GetHashCode())
               {
                   sFileToWrite = sFileStrCh;
               }
               else if (iFileType == enFileType.Choze.GetHashCode())
               {
                   sFileToWrite = sFileStrC;
               }
               else
               {
                   sFileToWrite = sFileStrS;
               }
           }

           return sFileToWrite;
       }

 

       private void InserIntoTableSugChishuv(COLL_MISPAR_ISHI_SUG_CHISHUV objCollMisparIshiSugChishuv, long lRequestNumToTransfer)
       {
           DataSet ds = new DataSet();
           clDal oDal = new clDal();

           try
           {
               if (!objCollMisparIshiSugChishuv.IsNull)
               {
                   oDal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, lRequestNumToTransfer, ParameterDir.pdInput);
                   oDal.AddParameter("p_coll_chishuv_sug_sidur", ParameterType.ntOracleArray, objCollMisparIshiSugChishuv, ParameterDir.pdInput, "COLL_MISPAR_ISHI_SUG_CHISHUV");
                   oDal.ExecuteSP(clDefinitions.cProInsertMisparIshiSugChishuv);
               }
           }
           catch (Exception ex)
           {
               ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(_lBakashaId, "E", 0, "InserIntoTableSugChishuv: " + ex.Message);
               throw ex;
           }
       }

        private DataSet GetOvdimToTransfer(long lBakashaId)
        {
            DataSet ds = new DataSet();
            clDal oDal = new clDal();

            try
            {
                oDal.AddParameter("p_request_id", ParameterType.ntOracleInt64, lBakashaId, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur_list", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.AddParameter("p_cur_prem", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clDefinitions.cProGetOvdimToTransfer, ref ds);
              
              return ds;
            }
            catch (Exception ex)
            {
                ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(_lBakashaId, "E", 0, "GetOvdimToTransfer: " + ex.Message);
               
                throw ex;
            }
            finally
            {
                oDal = null;
            }
        }

        private DataTable GetRechivimYomiim(long lBakashaId)
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();

            try
            {
                oDal.AddParameter("p_request_id", ParameterType.ntOracleInt64, lBakashaId, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
          

                oDal.ExecuteSP(clDefinitions.cProGetRechivimChishuvYomi, ref  dt);

                return dt;
            }
            catch (Exception ex)
            {
                ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(_lBakashaId,  "E", 0,  "GetRechivimYomiim: " + ex.Message);
               
                throw ex;
            }
            finally
            {
                oDal = null;
            }
        }

   
        private DataTable GetOvdimWithChufshaRezufa(long lBakashaId)
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();

            try
            {
                oDal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, lBakashaId, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);


                oDal.ExecuteSP(clDefinitions.cProGetOvdimChufshaRezifa, ref  dt);

                return dt;
            }
            catch (Exception ex)
            {
                ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(_lBakashaId,  "E",0,   "GetRechivimYomiim: " + ex.Message);
                throw ex;
            }
            finally
            {
                oDal = null;
            }
        }
        private DataTable GetChishuvYomiToOved(long lBakashaId,int iMisparIshi)
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();

            try
            {
                oDal.AddParameter("p_request_id", ParameterType.ntOracleInt64, lBakashaId, ParameterDir.pdInput);
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, iMisparIshi, ParameterDir.pdInput);
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clDefinitions.cProGetChishuvYomiToOved,ref  dt);

                return dt;
            }
            catch (Exception ex)
            {
                ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(_lBakashaId, "E", 0, "GetChishuvYomiToOved: " + ex.Message, iMisparIshi, null);
                throw ex;
            }
        }

        private void DeleteChishuvAfterTransfer(long lBakashaId)
        {
            clDal oDal = new clDal();

            try
            {
                oDal.AddParameter("p_request_id", ParameterType.ntOracleInt64, lBakashaId, ParameterDir.pdInput);
                
                oDal.ExecuteSP(clDefinitions.cProDelChishuvAfterTransfer);
            }
            catch (Exception ex)
            {
                ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(_lBakashaId, "E", 0, "DeleteChishuvAfterTransfer: " + ex.Message);
                throw ex;
            }
        }

        private void UpdateStatusYameyAvoda(long lBakashaId)
        {
            clDal oDal = new clDal();

            try
            {
                oDal.AddParameter("p_request_id", ParameterType.ntOracleInt64, lBakashaId, ParameterDir.pdInput);

                oDal.ExecuteSP(clDefinitions.cProUpdStatusYameyAvoda);
            }
            catch (Exception ex)
            {
                ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(_lBakashaId, "E", 0, "UpdateStatusYameyAvoda: " + ex.Message);
                throw ex;
            }

        }

        private void UpdateOvdimImShinuyHr(long lBakashaId,long lBakashaHuavraLesachar)
        {

            clTnua oDal = new clTnua((string)ConfigurationSettings.AppSettings["KDS_APPS_CONNECTION"]);
         

            try
            {
                oDal.AddParameter("p_bakasha_riza_chishuv", ParameterType.ntOracleInt64, lBakashaId, ParameterDir.pdInput);
                oDal.AddParameter("p_bakasha_riza_lasachar", ParameterType.ntOracleInt64, lBakashaHuavraLesachar, ParameterDir.pdInput);

                oDal.ExecuteSP(clDefinitions.cProUpdOvdimImShinuyHr);
            }
            catch (Exception ex)
            {
                ServiceLocator.Current.GetInstance<ILogBakashot>().InsertLog(_lBakashaId, "E", 0, "DeleteChishuvAfterTransfer: " + ex.Message);
                throw ex;
            }
        }
    }
}
