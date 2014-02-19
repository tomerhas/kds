using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary;
using KdsLibrary.DAL;
using System.IO;
using System.Configuration;
using KdsLibrary.UDT;
using System.Web;
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

           int i;//iMisparIshi, i, iMaamad, iMaamadRashi;
           DataTable dtOvdim, dtRechivim, dtPrem, dtRechivimYomi,dtChufshaRezufa,dtOvdimDorB,dtRechiveyDorB;
           DataSet dsNetunim, dsTables;
           int iStatus = 0;
           string bDelete = ConfigurationSettings.AppSettings["DeleteTablesAfterTransfer"];
           string sPathFile = ConfigurationSettings.AppSettings["PathFileTransfer"];
           string sChodeshIbud, sFileNameSchirim, sFileNameChaverim, sFileNameChozim, sFileNameETBTashlum, sFileNameETBakaraReg, sFileNameETBakaraHef;
           //PirteyOved oPirteyOved;
          // DateTime dChodesh;
           COLL_MISPAR_ISHI_SUG_CHISHUV objCollMisparIshiSugChishuv = new COLL_MISPAR_ISHI_SUG_CHISHUV();
          // OBJ_MISPAR_ISHI_SUG_CHISHUV objMisparIshiSugChishuv; 
          // int iDirug, iDarga;

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
                   clLogBakashot.InsertErrorToLog(lBakashaId, "I", 0, "Transfer, before GetOvdimToTransfer");
                   dsNetunim = GetOvdimToTransfer(lRequestNumToTransfer);

                   dtRechivimYomi = GetRechivimYomiim(lRequestNumToTransfer);
                   dtChufshaRezufa = GetOvdimWithChufshaRezufa(lRequestNumToTransfer);
                   clLogBakashot.InsertErrorToLog(lBakashaId, "I", 0, "Transfer, after GetOvdimToTransfer Before GetNetuneyDorB");

                   dtOvdim = dsNetunim.Tables[0];
                   dtRechivim = dsNetunim.Tables[1];
                   dtPrem = dsNetunim.Tables[2];

                   dsNetunim = GetNetuneyDorB(lRequestNumToTransfer);
                   
                   dtRechiveyDorB=dsNetunim.Tables[0];
                   dtOvdimDorB=dsNetunim.Tables[1];
                   clLogBakashot.InsertErrorToLog(lBakashaId, "I", 0, "Transfer, after GetNetuneyDorB");

                   dsTables = new DataSet();
                   dsTables.Tables.Add(dtRechivim.Copy());
                   dsTables.Tables.Add(dtRechiveyDorB.Copy());
                   dsTables.Tables.Add(dtPrem.Copy());
                   dsTables.Tables.Add(dtRechivimYomi.Copy());
                   dsTables.Tables.Add(dtChufshaRezufa.Copy());
                   clLogBakashot.InsertErrorToLog(lBakashaId, "I", 0, "count:" + dtOvdim.Rows.Count);
                   _PirteyOved = new List<PirteyOved>();
                  // dtEzerYomi = new DataTable();
                   for (i = 0; i <= dtOvdim.Rows.Count - 1; i++)
                   {
                       if (i % 100 == 0)
                           clLogBakashot.InsertErrorToLog(lBakashaId, "I", 0, "after " + i + " ovdim");
                      
                      AddEruaToList(lBakashaId, lRequestNumToTransfer, dtOvdim.Rows[i], dsTables, ref objCollMisparIshiSugChishuv);
                      
                      ////// iMisparIshi = int.Parse(dtOvdim.Rows[i]["mispar_ishi"].ToString());
                      ////// dChodesh = DateTime.Parse(dtOvdim.Rows[i]["taarich"].ToString());

                      ////// //try
                      ////// //{

                      ////// if (i == 0)
                      //////     sChodeshIbud = dtOvdim.Rows[i]["chodesh_ibud"].ToString();
                      ////// if(i % 100 ==0)
                      //////     clLogBakashot.InsertErrorToLog(lBakashaId, "I", 0, "after " + i + " ovdim" );
                  
                      ////// oPirteyOved = new PirteyOved(lBakashaId, lRequestNumToTransfer, dtOvdim.Rows[i]);
                      ////// oPirteyOved.sChodeshIbud = sChodeshIbud;
                      ////// //oPirteyOved.iCntYamim = GetCntYamimToOved(iMisparIshi, dtRechivimYomi, dChodesh);
                      ////// //oPirteyOved._dtChishuv = GetChishuvYomiToOved(iMisparIshi, dtRechivimYomi);
                      ////// //עובדי קייטנה 
                      ////// //לא מבצעים להם העברה לשכר

                      ////// oPirteyOved.InitializeErueyOved(dsTables);
                      //////// oPirteyOved.InitializeErueyOved(dtRechivim, dtPrem);

                      ////// _PirteyOved.Add(oPirteyOved);

                      ////// objMisparIshiSugChishuv = new OBJ_MISPAR_ISHI_SUG_CHISHUV();
                      ////// SetSugChishuvUDT(iMisparIshi, dChodesh, oPirteyOved, ref objMisparIshiSugChishuv);
                      ////// objCollMisparIshiSugChishuv.Add(objMisparIshiSugChishuv);

                       //if (i%50 ==0)
                       //   clLogBakashot.InsertErrorToLog(lBakashaId, "I", 0, "Transfer i=" + i);
                       //}
                       //catch (Exception ex)
                       //{
                       //    clLogBakashot.InsertErrorToLog(lBakashaId, iMisparIshi, "E", 0, dChodesh, "Transfer: " + ex.Message);
                       //}

                       // ClearObject();
                   }

                   for (i = 0; i <= dtOvdimDorB.Rows.Count - 1; i++)
                   {
                       if (i % 100 == 0)
                           clLogBakashot.InsertErrorToLog(lBakashaId, "I", 0, "after " + i + " ovdim");

                       AddEruaToList(lBakashaId, lRequestNumToTransfer, dtOvdimDorB.Rows[i], dsTables ,ref objCollMisparIshiSugChishuv);
                   }
                   if (sFileStrEt == null && _PirteyOved.Exists( item =>(item.iDirug == 85 && item.iDarga == 30)) )
                   {
                       sChodeshIbud = dtOvdim.Rows[0]["chodesh_ibud"].ToString();
                       sFileStrEt = new StreamWriter(sPathFile + sFileNameETBTashlum.Replace("mmyy", sChodeshIbud.Substring(0, 2) + sChodeshIbud.Substring(5, 2)), false, Encoding.Default);

                       sFileStrEtBakaraReg = new StreamWriter(sPathFile + sFileNameETBakaraReg.Replace("mmyy", sChodeshIbud.Substring(0, 2) + sChodeshIbud.Substring(5, 2)), false, Encoding.Default);
                       sFileStrEtBakaraHef = new StreamWriter(sPathFile + sFileNameETBakaraHef.Replace("mmyy", sChodeshIbud.Substring(0, 2) + sChodeshIbud.Substring(5, 2)), false, Encoding.Default);
                    }

                   clLogBakashot.InsertErrorToLog(lBakashaId, "I", 0, "Transfer, before WriteEruimToFile");
                   _PirteyOved.ForEach(item => { WriteEruimToFile(item); });
                  // WriteToFile(iMaamad, iMaamadRashi, iDirug, iDarga);
                   clLogBakashot.InsertErrorToLog(lBakashaId, "I", 0, "Transfer, after WriteEruimToFile");

                   InserIntoTableSugChishuv(objCollMisparIshiSugChishuv, lRequestNumToTransfer);
                   clLogBakashot.InsertErrorToLog(lBakashaId, "I", 0, "Transfer, after InserIntoTableSugChishuv");
                   UpdateStatusYameyAvoda(lRequestNumToTransfer);

                   clLogBakashot.InsertErrorToLog(lBakashaId, "I", 0, "Transfer, after UpdateStatusYameyAvoda");
                   if (bDelete == "true")
                       DeleteChishuvAfterTransfer(lRequestNumToTransfer);
                   //UpdateOvdimImShinuyHr(lBakashaId,lRequestNumToTransfer);
                   HttpRuntime.Cache.Remove(ConfigurationSettings.AppSettings["TakanonSizialiCachName"]);
                   iStatus = clGeneral.enStatusRequest.ToBeEnded.GetHashCode();
               }
               catch (Exception ex)
               {
                   iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
                   clLogBakashot.InsertErrorToLog(lBakashaId, "E", 0, "Transfer: " + ex.Message);
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
               clLogBakashot.InsertErrorToLog(lBakashaId, "E", 0, "Transfer: " + ex.Message);
           }
       }

       private void AddEruaToList(long lBakashaId, long lRequestNumToTransfer,  DataRow drOved, DataSet dsTables, ref COLL_MISPAR_ISHI_SUG_CHISHUV  objCollMisparIshiSugChishuv)
       {
           int iMisparIshi;
           PirteyOved oPirteyOved;
           DateTime dChodesh;
       //    COLL_MISPAR_ISHI_SUG_CHISHUV objCollMisparIshiSugChishuv = new COLL_MISPAR_ISHI_SUG_CHISHUV();
           OBJ_MISPAR_ISHI_SUG_CHISHUV objMisparIshiSugChishuv;
           //string sChodeshIbud="";
           iMisparIshi = int.Parse(drOved["mispar_ishi"].ToString());
           dChodesh = DateTime.Parse(drOved["taarich"].ToString());

           try
           {

           //if (i == 0)
           //    sChodeshIbud = drOved["chodesh_ibud"].ToString();
        
           oPirteyOved = new PirteyOved(lBakashaId, lRequestNumToTransfer, drOved);
           oPirteyOved.sChodeshIbud = drOved["chodesh_ibud"].ToString(); //sChodeshIbud;

           oPirteyOved.InitializeErueyOved(dsTables);

           _PirteyOved.Add(oPirteyOved);

           objMisparIshiSugChishuv = new OBJ_MISPAR_ISHI_SUG_CHISHUV();
           SetSugChishuvUDT(iMisparIshi, dChodesh, oPirteyOved, ref objMisparIshiSugChishuv);
           objCollMisparIshiSugChishuv.Add(objMisparIshiSugChishuv);

           }
           catch (Exception ex)
           {
               clLogBakashot.InsertErrorToLog(lBakashaId, iMisparIshi, "E", 0, dChodesh, "Transfer: " + ex.Message);
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
       ////     //   clLogBakashot.SetError(iBakashaId, iMisparIshi, "E", 0, null, "GetChishuvYomiToOved: " + ex.Message);
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
               clLogBakashot.InsertErrorToLog(_lBakashaId, mispar_ishi, "E", 0, null,"SetSugChishuvUDT: " + ex.Message);
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

       //public void Transfer(long lBakashaId, long lRequestNumToTransfer)
       // {

       //     int iMisparIshi, i, iMaamad, iMaamadRashi;
       //     DataTable dtOvdim,dtRechivim;
       //     DataSet dsNetunim;
       //     int iStatus = 0;
       //     string sPathFile = ConfigurationSettings.AppSettings["PathFileTransfer"];
       //     string sFileNameSchirim, sFileNameChaverim, sFileNameChozim, sFileNameETBTashlum, sFileNameETBakara;
       //     StreamWriter sFileStrCh, sFileStrS, sFileStrC, sFileToWrite, sFileStrEt, sFileStrEtBakara;
       //     StringBuilder sErua416, sPirteyOved, sErua415, sErua418, sErua419, sErua417, sErua589,sDataEt, sErua462, sErua413, sBakaraEt, sErua460;
       //     DateTime dChodesh;
       //    int iDirug,iDarga;

       //     sFileNameChaverim = "EGD3NOCH.txt";
       //     sFileNameSchirim = "EGD6NOCH.txt";
       //     sFileNameChozim = "EGB2NOCH.txt";
       //     sFileNameETBTashlum = "QDIVyymm.162";
       //     sFileNameETBakara = "REGLyymm.162";

       //     _lBakashaId = lBakashaId;

       //     try
       //     {
       //         sFileStrS = new StreamWriter(sPathFile + sFileNameSchirim, false, Encoding.UTF8);
       //         sFileStrCh = new StreamWriter(sPathFile + sFileNameChaverim, false, Encoding.UTF8);
       //         sFileStrC = new StreamWriter(sPathFile + sFileNameChozim, false, Encoding.UTF8);
       //         sFileStrEt = new StreamWriter(sPathFile + sFileNameETBTashlum, false, Encoding.UTF8);
       //         sFileStrEtBakara = new StreamWriter(sPathFile + sFileNameETBakara, false, Encoding.UTF8);
           

       //         try
       //         {
                  
       //             dsNetunim = GetOvdimToTransfer(lRequestNumToTransfer);

       //             dtOvdim = dsNetunim.Tables[0];
       //             dtRechivim = dsNetunim.Tables[1];

       //             clLogBakashot.InsertErrorToLog(lBakashaId, "I", 0, "count:" + dtOvdim.Rows.Count);
                 
       //             //listOfLine=new List<string>();

       //             for (i = 0; i <= dtOvdim.Rows.Count - 1; i++)
       //                 {
       //                     //clErua416 oErua416 = new clErua416(lBakashaId, dtOvdim.Rows[i]);
       //                     //listOfLine.Add(oErua416.Line);
                        
       //                     //listOfLine.Clear();
       //                       iMisparIshi = int.Parse(dtOvdim.Rows[i]["mispar_ishi"].ToString());
       //                       dChodesh = DateTime.Parse(dtOvdim.Rows[i]["taarich"].ToString());
                                 
       //                     try
       //                     {
       //                          iMaamad = int.Parse(dtOvdim.Rows[i]["maamad"].ToString());
       //                         iMaamadRashi = int.Parse(dtOvdim.Rows[i]["mifal"].ToString());
       //                         iDirug = int.Parse(dtOvdim.Rows[i]["dirug"].ToString());
       //                         iDarga = int.Parse(dtOvdim.Rows[i]["darga"].ToString());

       //                         if (iDirug == 85 && iDarga == 30)
       //                         {
       //                             sDataEt = CreateDataEt(iMisparIshi, dtOvdim.Rows[i], dChodesh, dtRechivim);
       //                             if (sDataEt.ToString().Length > 0)
       //                             {
       //                                 sFileStrEt.WriteLine(sDataEt);
       //                                 sFileStrEt.Flush();
       //                             }
       //                             sBakaraEt = CreateBakaraEt(iMisparIshi, dtOvdim.Rows[i], dChodesh, dtRechivim);
       //                             if (sBakaraEt.ToString().Length > 0)
       //                             {
       //                                 sFileStrEtBakara.WriteLine(sBakaraEt);
       //                                 sFileStrEtBakara.Flush();
       //                             }
       //                         }
       //                         else
       //                         {
       //                             if (iMaamadRashi == clGeneral.enMaamad.Friends.GetHashCode())
       //                             {
       //                                 sFileToWrite = sFileStrCh;
       //                             }
       //                             else if (iMaamad == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
       //                             {
       //                                 sFileToWrite = sFileStrC;
       //                             }
       //                             else
       //                             {
       //                                 sFileToWrite = sFileStrS;
       //                             }
       //                             sPirteyOved = GetPirteyOved(iMisparIshi, dtOvdim.Rows[i]);

       //                             if (iDirug != 82 && iDirug != 83)
       //                             {
       //                                 sErua413 = CreateErua413(iMisparIshi, dtOvdim.Rows[i], dChodesh, iMaamadRashi, dtRechivim);
       //                                 if (sErua413.ToString().Length > 0)
       //                                 {
       //                                     sFileToWrite.WriteLine(sErua413);
       //                                     sFileToWrite.Flush();
       //                                 }
       //                             }

       //                             if (iMaamad != clGeneral.enKodMaamad.Shtachim.GetHashCode())
       //                             {
       //                                 sErua415 = CreateErua415(sPirteyOved, iMisparIshi, iMaamad, iMaamadRashi, dChodesh, dtRechivim);
       //                                 if (sErua415.ToString().Length > 0)
       //                                 {
       //                                     sFileToWrite.WriteLine(sErua415);
       //                                     sFileToWrite.Flush();
       //                                 }
       //                             }
       //                             sErua416 = CreateErua416(sPirteyOved, iMisparIshi, iMaamad, iMaamadRashi, dChodesh, dtRechivim);
       //                             if (sErua416.ToString().Length > 0)
       //                             {
       //                                 sFileToWrite.WriteLine(sErua416);
       //                                 sFileToWrite.Flush();
       //                             }
       //                             sErua417 = CreateErua417(sPirteyOved, iMisparIshi, iMaamad, iMaamadRashi, dChodesh, dtRechivim);
       //                             if (sErua417.ToString().Length > 0)
       //                             {
       //                                 sFileToWrite.WriteLine(sErua417);
       //                                 sFileToWrite.Flush();
       //                             }
       //                             if (iMaamadRashi != clGeneral.enMaamad.Salarieds.GetHashCode())
       //                             {
       //                                 if ((iDirug != 82 && iDirug != 83 && iDirug != 85) || !(iDirug == 84 && iDarga == 1) || !(iDirug == 85 && (iDarga == 80 || iDarga == 30)))
       //                                 {
       //                                     sErua418 = CreateErua418(sPirteyOved, iMisparIshi, dChodesh, dtRechivim);
       //                                     if (sErua418.ToString().Length > 0)
       //                                     {
       //                                         sFileToWrite.WriteLine(sErua418);
       //                                         sFileToWrite.Flush();
       //                                     }
       //                                 }
       //                             }
       //                             if (iMaamadRashi == clGeneral.enMaamad.Salarieds.GetHashCode())
       //                             {
       //                                 sErua419 = CreateErua419(sPirteyOved, iMisparIshi, iMaamad, dChodesh, dtRechivim);
       //                                 if (sErua419.ToString().Length > 0)
       //                                 {
       //                                     sFileToWrite.WriteLine(sErua419);
       //                                     sFileToWrite.Flush();
       //                                 }
       //                                 sErua460 = CreateErua460(sPirteyOved, iMisparIshi, dChodesh, iMaamad, dtRechivim);
       //                                 if (sErua460.ToString().Length > 0)
       //                                 {
       //                                     sFileToWrite.WriteLine(sErua460);
       //                                     sFileToWrite.Flush();
       //                                 }
       //                             }
       //                             sErua462 = CreateErua462(sPirteyOved, iMisparIshi, dChodesh, dtRechivim);
       //                             if (sErua462.ToString().Length > 0)
       //                             {
       //                                 sFileToWrite.WriteLine(sErua462);
       //                                 sFileToWrite.Flush();
       //                             }
       //                             sErua589 = CreateErua589(sPirteyOved, iMisparIshi, dChodesh, int.Parse(dtOvdim.Rows[i]["mushhe"].ToString()));
       //                             if (sErua589.ToString().Length > 0)
       //                             {
       //                                 sFileToWrite.WriteLine(sErua589);
       //                                 sFileToWrite.Flush();
       //                             }
       //                         }

                               
       //                     }
       //                     catch (Exception ex)
       //                     {
       //                         clLogBakashot.InsertErrorToLog(lBakashaId, iMisparIshi, "E", 0, dChodesh, "Transfer: " + ex.Message);
       //                     }

                            
       //              }

       //             //DeleteChishuvAfterTransfer(lRequestNumToTransfer);
       //             UpdateStatusYameyAvoda(lRequestNumToTransfer);
       //             //UpdateOvdimImShinuyHr(lBakashaId,lRequestNumToTransfer);

       //             iStatus = clGeneral.enStatusRequest.ToBeEnded.GetHashCode();
       //         }
       //         catch (Exception ex)
       //         {
       //             iStatus = clGeneral.enStatusRequest.Failure.GetHashCode();
       //             clLogBakashot.InsertErrorToLog(lBakashaId, "E", 0, "Transfer: " + ex.Message);
       //         }
       //         finally
       //         {
       //             if (!(sFileStrCh == null))
       //             {
       //                 sFileStrCh.Close();
       //             }
       //             if (!(sFileStrS == null))
       //             {
       //                 sFileStrS.Close();
       //             }
       //             if (!(sFileStrC == null))
       //             {
       //                 sFileStrC.Close();
       //             }
       //             if (!(sFileStrEt== null))
       //             {
       //                 sFileStrEt.Close();
       //             }
       //             if (!(sFileStrEtBakara == null))
       //             {
       //                 sFileStrEtBakara.Close();
       //             }
       //             if (iStatus == clGeneral.enStatusRequest.ToBeEnded.GetHashCode())
       //             {
       //                 clDefinitions.UpdateLogBakasha(lBakashaId,DateTime.Now, iStatus);
       //                 clDefinitions.UpdateLogBakasha(lRequestNumToTransfer, 1, DateTime.Now);
       //             }
       //             else { clDefinitions.UpdateLogBakasha(lBakashaId, DateTime.Now, iStatus); }
       //         }
       //     }
       //     catch (Exception ex)
       //     {
       //         clDefinitions.UpdateLogBakasha(lBakashaId, DateTime.Now,  clGeneral.enStatusRequest.Failure.GetHashCode());
       //         clLogBakashot.InsertErrorToLog(lBakashaId, "E", 0, "Transfer: " + ex.Message);
       //     }
       // }

       //private string GetBlank(int iLength)
       //{
       //    string sBlank="";
       //    char sChar=Convert.ToChar(ConfigurationSettings.AppSettings["BlankCharFileHilan"]);
          
       //    sBlank = sBlank.PadRight(iLength, sChar);
           
       //    return sBlank;
       //}


       //private StringBuilder GetPirteyOved(int iMisparIshi,DataRow drRowOved)
       //{
       //    StringBuilder sPirteyOved;
       //    sPirteyOved = new StringBuilder();

       //    try
       //    {
               
       //        sPirteyOved.Append(drRowOved["mifal"].ToString().PadLeft(4));
       //        sPirteyOved.Append(drRowOved["maamad"].ToString().PadRight(2));
       //        sPirteyOved.Append("0");
       //        sPirteyOved.Append(drRowOved["mispar_ishi"].ToString().PadRight(5));
       //        sPirteyOved.Append(drRowOved["sifrat_bikoret"].ToString());
       //        sPirteyOved.Append(drRowOved["shem_mish"].ToString().PadLeft(10));
       //        sPirteyOved.Append(drRowOved["shem_prat"].ToString().PadLeft(7));
       //        sPirteyOved.Append("0000");
       //        sPirteyOved.Append("00");
       //        sPirteyOved.Append("000000000");

       //        return sPirteyOved;
       //    }
       //    catch (Exception ex)
       //    {
       //        clLogBakashot.SetError(_lBakashaId, iMisparIshi, "E", 0, null, "GetPirteyOved: " + ex.Message);
       //        return sPirteyOved;
       //    }
          
           
       //}

       //private StringBuilder CreateErua589(StringBuilder sPirteyOved, int iMisparIshi,DateTime dChodesh,int iMushee)
       //{
       //    StringBuilder sErua589=new StringBuilder();
       //    DataTable dtChishuv;
       //    DateTime dTarMe,dTarAd;
       //    float fErech;
       //    int iSugYom;
       //    try {
       //        sErua589.Append(sPirteyOved);
       //        dtChishuv=GetChishuvYomiToOved(_lBakashaId, iMisparIshi);
       //        dTarMe=DateTime.Parse("01/" + dChodesh.Month.ToString("00")+ "/" +  dChodesh.Year);
       //         dTarAd=dTarMe.AddMonths(1).AddDays(-1);
       //         do
       //         {
       //            fErech= clCalcGeneral.GetSumErechRechiv(dtChishuv.Compute("SUM(ERECH_RECHIV)","KOD_RECHIV=" + clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
       //            if (fErech > 0)
       //            {
       //                sErua589.Append("ע");
       //            }
       //            else
       //            {
       //                fErech = clCalcGeneral.GetSumErechRechiv(dtChishuv.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomChofesh.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
       //                if (fErech > 0)
       //                {
       //                    sErua589.Append("ח");
       //                }
       //                else
       //                {
       //                    fErech = clCalcGeneral.GetSumErechRechiv(dtChishuv.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomHeadrut.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
       //                    if (fErech > 0)
       //                    {
       //                        sErua589.Append("ה");
       //                    }
       //                    else
       //                    {
       //                        fErech = clCalcGeneral.GetSumErechRechiv(dtChishuv.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMiluim.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
       //                        if (fErech > 0)
       //                        {
       //                            sErua589.Append("צ");
       //                        }
       //                        else
       //                        {
       //                            fErech = clCalcGeneral.GetSumErechRechiv(dtChishuv.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachla.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
       //                            if (fErech > 0)
       //                            {
       //                                sErua589.Append("מ");
       //                            }
       //                            else
       //                            {
       //                                fErech = clCalcGeneral.GetSumErechRechiv(dtChishuv.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachalaBoded.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
       //                                if (fErech > 0)
       //                                {
       //                                    sErua589.Append("ם");
       //                                }
       //                                else
       //                                {
       //                                    fErech = clCalcGeneral.GetSumErechRechiv(dtChishuv.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachalaYeled.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
       //                                    if (fErech > 0)
       //                                    {
       //                                        sErua589.Append("י");
       //                                    }
       //                                    else
       //                                    {
       //                                        fErech = clCalcGeneral.GetSumErechRechiv(dtChishuv.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachalatHorim.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
       //                                        if (fErech > 0)
       //                                        {
       //                                            sErua589.Append("פ");
       //                                        }
       //                                        else
       //                                        {
       //                                            fErech = clCalcGeneral.GetSumErechRechiv(dtChishuv.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomMachalatBenZug.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
       //                                            if (fErech > 0)
       //                                            {
       //                                                sErua589.Append("ב");
       //                                            }
       //                                            else
       //                                            {
       //                                                fErech = clCalcGeneral.GetSumErechRechiv(dtChishuv.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomShmiratHerayon.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
       //                                                if (fErech > 0)
       //                                                {
       //                                                    sErua589.Append("ז");
       //                                                }
       //                                                else
       //                                                {
       //                                                    fErech = clCalcGeneral.GetSumErechRechiv(dtChishuv.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomTipatChalav.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
       //                                                    if (fErech > 0)
       //                                                    {
       //                                                        sErua589.Append("ט");
       //                                                    }
       //                                                    else
       //                                                    {
       //                                                        fErech = clCalcGeneral.GetSumErechRechiv(dtChishuv.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomHadracha.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
       //                                                        if (fErech > 0)
       //                                                        {
       //                                                            sErua589.Append("ק");
       //                                                        }
       //                                                        else
       //                                                        {
       //                                                            fErech = clCalcGeneral.GetSumErechRechiv(dtChishuv.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomTeuna.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
       //                                                            if (fErech > 0)
       //                                                            {
       //                                                                sErua589.Append("ת");
       //                                                            }
       //                                                            else
       //                                                            {
       //                                                                fErech = clCalcGeneral.GetSumErechRechiv(dtChishuv.Compute("SUM(ERECH_RECHIV)", "KOD_RECHIV=" + clGeneral.enRechivim.YomEvel.GetHashCode().ToString() + " and taarich=Convert('" + dTarMe.ToShortDateString() + "', 'System.DateTime')"));
       //                                                                if (fErech > 0)
       //                                                                {
       //                                                                    sErua589.Append("א");
       //                                                                }
       //                                                                else
       //                                                                {
       //                                                                    iSugYom = clDefinitions.GetSugYom(clDefinitions.GetYamimMeyuchadim(), dTarMe);
       //                                                                    if (iSugYom == 10)
       //                                                                    { sErua589.Append("ו"); }
       //                                                                    else if (iSugYom == 10)
       //                                                                    { sErua589.Append("ש"); }
       //                                                                    else if (iSugYom > 20)
       //                                                                    { sErua589.Append("ג"); }
       //                                                                    else if (iMushee>0)
       //                                                                    { sErua589.Append("ד"); }
       //                                                                }
       //                                                            }
       //                                                        }
       //                                                    }
       //                                                }
       //                                            }
       //                                        }
       //                                    }
                                           
       //                                }
       //                            }
       //                        }
       //                    }
       //                }
       //            }

       //             dTarMe = dTarMe.AddDays(1);
       //         }
       //         while (dTarMe <= dTarAd);

       //        return sErua589;
       //    }
       //    catch (Exception ex)
       //    {
       //        clLogBakashot.SetError(_lBakashaId, iMisparIshi, "E", 589, null, "CreateErua589: " + ex.Message);
       //        return sPirteyOved;
       //    }
       //}

       //private StringBuilder CreateBakaraEt(int iMisparIshi, DataRow drRowOved, DateTime dChodesh, DataTable dtRechivim)
       //{
       //    StringBuilder sBakaraEt= new StringBuilder();
       //    float fErech;
       //    try 
       //    {
       //       sBakaraEt.Append("162;");
       //       sBakaraEt.Append(dChodesh.Year.ToString().Substring(2, 2) + dChodesh.Month.ToString().PadLeft(2, char.Parse("0")) + ";");
       //       sBakaraEt.Append(iMisparIshi.ToString().PadLeft(9, char.Parse("0")) + ";");
       //       sBakaraEt.Append(drRowOved["TEUDAT_ZEHUT"].ToString().PadLeft(9, char.Parse("0")) + ";");
       //       sBakaraEt.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YemeyAvoda.GetHashCode(), dChodesh), 5, 0) + ";");
       //       sBakaraEt.Append(FormatNumberWithPoint(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.ShaotShabat100.GetHashCode(), dChodesh), 6, 2) + ";");
       //       sBakaraEt.Append(FormatNumberWithPoint(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.Shaot125Letashlum.GetHashCode(), dChodesh), 6, 2) + ";");
       //       sBakaraEt.Append(FormatNumberWithPoint(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.Shaot150Letashlum.GetHashCode(), dChodesh), 6, 2) + ";");
       //       sBakaraEt.Append(FormatNumberWithPoint(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.Shaot200Letashlum.GetHashCode(), dChodesh), 6, 2) + ";");
       //       sBakaraEt.Append("000.00;");
       //       sBakaraEt.Append("000.00;");
       //       sBakaraEt.Append("000.00;");
       //       sBakaraEt.Append("000.00;");
       //       sBakaraEt.Append(FormatNumberWithPoint(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.PremyaRegila.GetHashCode(), dChodesh), 11, 2) + ";");
       //       sBakaraEt.Append(FormatNumberWithPoint(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.DmeyNesiaLeEggedTaavura.GetHashCode(), dChodesh), 7, 2) + ";");
       //       sBakaraEt.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.SachPitzul.GetHashCode(), dChodesh), 2, 0) + ";");
       //       fErech = GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.ZmanLailaEgged.GetHashCode(), dChodesh);
       //       fErech += GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.ZmanLailaChok.GetHashCode(), dChodesh);
       //       sBakaraEt.Append(FormatNumber(fErech, 4, 0) + ";");
       //       sBakaraEt.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.EshelLeEggedTaavura.GetHashCode(), dChodesh), 2, 0) + ";");
              
       //      return sBakaraEt;
           
       //    }
       //    catch (Exception ex)
       //    {
       //        clLogBakashot.SetError(_lBakashaId, iMisparIshi, "E", 0, dChodesh, "CreateBakaraEt: " + ex.Message);
       //        return sBakaraEt;
       //    }
       //}

       //private string FormatNumber(float fErech, int iLen, int iNumDigit)
       //{
       //    double dErech;
       //    dErech = clGeneral.TrimDoubleToXDigits((double)fErech, iNumDigit);
       //    return dErech.ToString().Replace(".", "").PadLeft(iLen, char.Parse("0"));
       //}

       //private string FormatNumberWithPoint(float fErech, int iLen, int iNumDigit)
       //{
       //    double dErech;
       //    dErech = clGeneral.TrimDoubleToXDigits((double)fErech, iNumDigit);
       //    return dErech.ToString().PadLeft(iLen, char.Parse("0"));
       //}

       //private StringBuilder CreateDataEt(int iMisparIshi, DataRow drRowOved, DateTime dChodesh, DataTable dtRechivim)
       //{
       //    StringBuilder sDataEt = new StringBuilder();
       //    StringBuilder sDataEtToRechiv;
       //    float fErech;
       //    string sTehudateZehut;
       //    try
       //    {
       //        sTehudateZehut = drRowOved["TEUDAT_ZEHUT"].ToString();
       //        fErech = GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YemeyAvoda.GetHashCode(), dChodesh);
       //        if (fErech>0)
       //          {
       //           sDataEtToRechiv = CreateDataEtToRechiv(iMisparIshi,"100",fErech,0,dChodesh,sTehudateZehut);
       //           sDataEt.Append(sDataEtToRechiv);
       //           sDataEt.AppendLine("");
       //          }

       //        fErech = GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.ShaotShabat100.GetHashCode(), dChodesh);
       //        if (fErech > 0)
       //        {
       //            sDataEtToRechiv = CreateDataEtToRechiv(iMisparIshi, "001", fErech, 0, dChodesh, sTehudateZehut);
       //            sDataEt.Append(sDataEtToRechiv);
       //            sDataEt.AppendLine("");
       //        }

       //        if (drRowOved["isuk"].ToString() == "5")
       //        {
       //            fErech = GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.PremyaRegila.GetHashCode(), dChodesh);
       //            if (fErech > 0)
       //            {
       //                if (fErech > 500) { fErech = 500; }
       //                sDataEtToRechiv = CreateDataEtToRechiv(iMisparIshi, "020", 1, fErech, dChodesh, sTehudateZehut);
       //                sDataEt.Append(sDataEtToRechiv);
       //                sDataEt.AppendLine("");
       //            }


       //            fErech = GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.SachPitzul.GetHashCode(), dChodesh);
       //            if (fErech > 0)
       //            {
       //                sDataEtToRechiv = CreateDataEtToRechiv(iMisparIshi, "063",  fErech,0, dChodesh, sTehudateZehut);
       //                sDataEt.Append(sDataEtToRechiv);
       //                sDataEt.AppendLine("");
       //            }
       //        }

            
       //        fErech = GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YemeyAvoda.GetHashCode(), dChodesh);
       //         if (fErech > 0)
       //         {
       //             fErech = GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.DmeyNesiaLeEggedTaavura.GetHashCode(), dChodesh);
               
       //             sDataEtToRechiv = CreateDataEtToRechiv(iMisparIshi, "004", 1, fErech, dChodesh, sTehudateZehut);
       //             sDataEt.Append(sDataEtToRechiv);
       //             sDataEt.AppendLine("");
       //         }
              
              
       //        fErech = GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.ZmanLailaEgged.GetHashCode(), dChodesh);
       //        fErech += GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.ZmanLailaChok.GetHashCode(), dChodesh);
             
       //        if (fErech > 0)
       //        {
       //            sDataEtToRechiv = CreateDataEtToRechiv(iMisparIshi, "078", fErech, 0, dChodesh, sTehudateZehut);
       //            sDataEt.Append(sDataEtToRechiv);
       //            sDataEt.AppendLine("");
       //        }

          
       //        fErech = GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.EshelLeEggedTaavura.GetHashCode(), dChodesh);
       //        if (fErech > 0)
       //        {
       //            sDataEtToRechiv = CreateDataEtToRechiv(iMisparIshi, "079", fErech, 0, dChodesh, sTehudateZehut);
       //            sDataEt.Append(sDataEtToRechiv);
       //            sDataEt.AppendLine("");
       //        }
                  

       //        fErech = GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.Shaot125Letashlum.GetHashCode(), dChodesh);
       //        if (fErech > 0)
       //        {
       //            sDataEtToRechiv = CreateDataEtToRechiv(iMisparIshi, "007", fErech, 0, dChodesh, sTehudateZehut);
       //            sDataEt.Append(sDataEtToRechiv);
       //            sDataEt.AppendLine("");
       //        }

       //        fErech = GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.Shaot150Letashlum.GetHashCode(), dChodesh);
       //        if (fErech > 0)
       //        {
       //            sDataEtToRechiv = CreateDataEtToRechiv(iMisparIshi, "008", fErech, 0, dChodesh, sTehudateZehut);
       //            sDataEt.Append(sDataEtToRechiv);
       //            sDataEt.AppendLine("");
       //        }

       //        fErech = GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.Shaot200Letashlum.GetHashCode(), dChodesh);
       //        if (fErech > 0)
       //        {
       //            sDataEtToRechiv = CreateDataEtToRechiv(iMisparIshi, "048", fErech, 0, dChodesh, sTehudateZehut);
       //            sDataEt.Append(sDataEtToRechiv);
       //            sDataEt.AppendLine("");
       //        }
       //        return sDataEt;
           
       //    }
       //    catch (Exception ex)
       //    {
       //        clLogBakashot.SetError(_lBakashaId, iMisparIshi, "E", 0, dChodesh, "CreateDataEt: " + ex.Message);
       //        throw ex;
       //    }
       //}

       //private StringBuilder CreateDataEtToRechiv(int iMisparIshi, string sSemelNatun, float fKamut, float fMechir, DateTime dChodesh, string sTehudateZehut)
       //{
       //     string sSimanMechir,  sSimanKamut;
       //    StringBuilder sDataEt = new StringBuilder();
       //    try
       //    {
       //        if  (fKamut<0){
       //        sSimanKamut="-";}
       //        else{sSimanKamut="+";}
       //       if(fMechir<0){
       //        sSimanMechir="-";}
       //        else{sSimanMechir="+";}

       //        sDataEt.Append("162");
       //        sDataEt.Append(dChodesh.Year.ToString().Substring(2, 2) + dChodesh.Month.ToString().PadLeft(2));
       //        sDataEt.Append(iMisparIshi.ToString().PadLeft(9));
       //        sDataEt.Append(sTehudateZehut.PadLeft(9));
       //        sDataEt.Append(" ");
       //        sDataEt.Append("0");
       //        sDataEt.Append(sSemelNatun);
       //        sDataEt.Append(FormatNumber(fKamut,10,2));
       //        sDataEt.Append(sSimanKamut);
       //        sDataEt.Append(FormatNumber(fMechir,10,2));
       //        sDataEt.Append(sSimanMechir);
       //        sDataEt.Append("      ");
       //        sDataEt.Append("1");
       //        return sDataEt;

       //    }
       //    catch (Exception ex)
       //    {
       //        clLogBakashot.SetError(_lBakashaId, iMisparIshi, "E", 0, dChodesh, "CreateDataEtToRechiv: " + ex.Message);
       //        throw ex;
       //    }
       //}

       //private StringBuilder CreateErua413(int iMisparIshi, DataRow drRowOved, DateTime dChodesh, int iMaamadRashi, DataTable dtRechivim)
       //{
       //    StringBuilder sErua413 = new StringBuilder();
       //    StringBuilder sStart = new StringBuilder();
       //    StringBuilder sEnd = new StringBuilder();
          
       //    try
       //    {
       //        sStart.Append("413");
       //        sStart.Append(drRowOved["mifal"].ToString().PadLeft(4,char.Parse("0")));
       //        sStart.Append("000");
       //        sStart.Append(drRowOved["mispar_ishi"].ToString().PadRight(5, char.Parse("0")));
       //        sStart.Append(drRowOved["sifrat_bikoret"].ToString());
       //        sStart.Append(drRowOved["shem_mish"].ToString().PadLeft(10));
       //        sStart.Append(drRowOved["shem_prat"].ToString().PadLeft(7));
       //        sStart.Append("000000");
       //        sStart.Append(GetBlank(6));
              
       //        sEnd.Append(GetBlank(12));
       //        sEnd.Append(dChodesh.Month.ToString().PadLeft(2));
       //        sEnd.Append(dChodesh.Year.ToString());
       //        sEnd.AppendLine(GetBlank(37));

       //        CreateData413(ref sErua413, "224", clGeneral.enRechivim.Kizuz100.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 2);
       //        CreateData413(ref sErua413, "221", clGeneral.enRechivim.Kizuz125.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 2);
       //        CreateData413(ref sErua413, "222", clGeneral.enRechivim.Kizuz150.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 2);
       //        CreateData413(ref sErua413, "223", clGeneral.enRechivim.Kizuz200.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 2);
       //        CreateData413(ref sErua413, "321", clGeneral.enRechivim.PremiaManasim.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);


       //        if (iMaamadRashi == clGeneral.enMaamad.Friends.GetHashCode())
       //        {

       //            CreateData413(ref sErua413, "311", clGeneral.enRechivim.PremiaLariushum.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "321", clGeneral.enRechivim.PremiaMachsanKatisim.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "311", clGeneral.enRechivim.PremyatMevakrimBadrachim.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "331", clGeneral.enRechivim.PremyatMifalYetzur.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "331", clGeneral.enRechivim.PremyatNehageyTovala.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "331", clGeneral.enRechivim.PremyatNehageyTenderim.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "331", clGeneral.enRechivim.PremyatDfus.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "331", clGeneral.enRechivim.PremyatMisgarot.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "331", clGeneral.enRechivim.PremyatGifur.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "331", clGeneral.enRechivim.PremyatMusachRishonLetzyon.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "331", clGeneral.enRechivim.PremyatTechnayYetzur.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "331", clGeneral.enRechivim.PremiyatReshetBitachon.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "331", clGeneral.enRechivim.PremiyatPeirukVeshiputz.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "331", clGeneral.enRechivim.PremiaMeshek.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "331", clGeneral.enRechivim.PremiaGrira.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);

       //            CreateData413(ref sErua413, "303", clGeneral.enRechivim.PremiaMachsenaim.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);

       //        }
       //        else {

       //            CreateData413(ref sErua413, "389", clGeneral.enRechivim.PremiaLariushum.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "321", clGeneral.enRechivim.PremiaMachsanKatisim.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "389", clGeneral.enRechivim.PremyatMevakrimBadrachim.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "322", clGeneral.enRechivim.PremyatMifalYetzur.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "322", clGeneral.enRechivim.PremyatNehageyTovala.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "322", clGeneral.enRechivim.PremyatNehageyTenderim.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "322", clGeneral.enRechivim.PremyatDfus.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "322", clGeneral.enRechivim.PremyatMisgarot.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "322", clGeneral.enRechivim.PremyatGifur.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "322", clGeneral.enRechivim.PremyatMusachRishonLetzyon.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "322", clGeneral.enRechivim.PremyatTechnayYetzur.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "322", clGeneral.enRechivim.PremiyatReshetBitachon.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "322", clGeneral.enRechivim.PremiyatPeirukVeshiputz.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "322", clGeneral.enRechivim.PremiaMeshek.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7, 0);
       //            CreateData413(ref sErua413, "322", clGeneral.enRechivim.PremiaGrira.GetHashCode(), dtRechivim, iMisparIshi, dChodesh, sStart, sEnd, 7,0);

       //        }

       //        return sErua413;

       //    }
       //    catch (Exception ex)
       //    {
       //        clLogBakashot.SetError(_lBakashaId, iMisparIshi, "E", 413, dChodesh, "CreateErua413: " + ex.Message);
       //        throw ex;
       //    }
       //}

       //private void CreateData413(ref StringBuilder sErua413,string sSaifHilan, int iKodRechiv, DataTable dtRechivim, int iMisparIshi, DateTime dChodesh, StringBuilder sStart, StringBuilder sEnd, int  iLen,int iNumDigit)
       //{
       //    float fErech;
       //    fErech = GetErechRechiv(dtRechivim, iMisparIshi, iKodRechiv, dChodesh);
       //    if (fErech > 0)
       //    {
       //        sErua413.Append(sStart);
       //        sErua413.Append(sSaifHilan.PadLeft(4,char.Parse("0")));
       //        sErua413.Append(GetBlank(17));
       //        sErua413.Append(FormatNumber(fErech,iLen,iNumDigit));

       //        sErua413.Append(sEnd);
       //    }
       //}

       //private StringBuilder CreateErua415(StringBuilder sPirteyOved, int iMisparIshi, int iMaamad, int iMaamadRashi, DateTime dChodesh, DataTable dtRechivim)
       //{
       //    StringBuilder sErua415;
       //    float fErech;
       //    sErua415 = new StringBuilder();
       //    try{
       //    sErua415.Append("415");
       //    sErua415.Append(sPirteyOved);
       //    if (iMaamad == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
       //    {
       //        sErua415.Append(FormatNumber((GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), dChodesh)/60),4,1));
       //    }
       //    else { sErua415.Append(GetBlank(4)); }
       //    sErua415.Append(FormatNumber((GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.DakotRegilot.GetHashCode(), dChodesh)/60),4,1));
       //    sErua415.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.ShaotHeadrut.GetHashCode(), dChodesh),4,1));
       //    sErua415.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.Shaot100Letashlum.GetHashCode(), dChodesh),4,1));
       //    sErua415.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.Shaot125Letashlum.GetHashCode(), dChodesh),4,1));
       //    sErua415.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.Shaot150Letashlum.GetHashCode(), dChodesh),4,1));
       //    sErua415.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.Shaot200Letashlum.GetHashCode(), dChodesh), 4, 1));

       //    if (iMaamad != clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
       //    {
       //        fErech = GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.ZmanLailaEgged.GetHashCode(), dChodesh);
       //        fErech += GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.ZmanLailaChok.GetHashCode(), dChodesh);
       //        sErua415.Append(FormatNumber(fErech,4,1));
       //    }
       //    else { sErua415.Append(GetBlank(4)); }

       //    sErua415.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.ShaotShabat100.GetHashCode(), dChodesh),4,1));

       //    sErua415.Append(GetBlank(4));

       //    sErua415.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.ZmanHamaratShaotShabat.GetHashCode(), dChodesh),4,1));
       //    sErua415.Append(FormatNumber((GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.DakotNehigaChol.GetHashCode(), dChodesh)/60),4,1));
       //    sErua415.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.Shaot25.GetHashCode(), dChodesh),4,1));
       //    sErua415.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.Shaot50.GetHashCode(), dChodesh),4,1));
       //    sErua415.Append(FormatNumber((GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.DakotNosafotNahagut.GetHashCode(), dChodesh)/60),4,1));

       //    fErech = GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.SachLina.GetHashCode(), dChodesh);
       //    fErech += GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.SachLinaKfula.GetHashCode(), dChodesh)*6;
       //    sErua415.Append(FormatNumber(fErech,4,1));

       //    if (iMaamad != clGeneral.enKodMaamad.Sachir12.GetHashCode() && iMaamadRashi != clGeneral.enMaamad.Friends.GetHashCode())
       //    {
       //        sErua415.Append(FormatNumber((GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomMachalatBenZug.GetHashCode(), dChodesh) / 60),4,1));
       //      }
       //    else { sErua415.Append(GetBlank(4)); }
       //    sErua415.Append(GetBlank(4));
       //    sErua415.Append(dChodesh.Month.ToString().PadLeft(2));
       //    sErua415.Append(GetBlank(5));

       //    return sErua415;
       //    }
       //    catch (Exception ex)
       //    {
       //        clLogBakashot.SetError(_lBakashaId, iMisparIshi, "E", 415, dChodesh, "CreateErua415: " + ex.Message);
       //        throw ex;
       //    }
       //}

       //private StringBuilder CreateErua416(StringBuilder sPirteyOved,int iMisparIshi, int iMaamad,int iMaamadRashi, DateTime dChodesh, DataTable dtRechivim)
       //{
       //    StringBuilder sErua416;
       //    float fErech;
       //    sErua416 = new StringBuilder();
       //    try
       //    {
       //    sErua416.Append("416");
       //    sErua416.Append(sPirteyOved);

       //    sErua416.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YemeyAvodaLeloMeyuchadim.GetHashCode(), dChodesh),4,2));
       //    sErua416.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YemeyAvoda.GetHashCode(), dChodesh),4,2));

       //    fErech = GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomKursHasavaLekav.GetHashCode(), dChodesh);
       //   if (iMaamad == clGeneral.enKodMaamad.SachirZmani.GetHashCode() && fErech>0)
       //    {
       //        fErech += GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomHeadrut.GetHashCode(), dChodesh);

       //        sErua416.Append(FormatNumber(fErech,4,2));
       //    }
       //    else if (iMaamad == clGeneral.enKodMaamad.OvedBechoze.GetHashCode() || iMaamad == clGeneral.enKodMaamad.GimlaiBechoze.GetHashCode())
       //    {
       //        fErech = GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomHeadrut.GetHashCode(), dChodesh);
       //        fErech += GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomMachalaBoded.GetHashCode(), dChodesh);
       //        fErech += GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomMachla.GetHashCode(), dChodesh);
       //        fErech += GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomMachalaYeled.GetHashCode(), dChodesh);
       //        fErech += GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomMachalatHorim.GetHashCode(), dChodesh);
       //        fErech += GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomMachalatBenZug.GetHashCode(), dChodesh);
       //        fErech += GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomTeuna.GetHashCode(), dChodesh);

       //        sErua416.Append(FormatNumber(fErech,4,2));
       //    }
       //    else if (iMaamad == clGeneral.enKodMaamad.Shtachim.GetHashCode() || iMaamad == clGeneral.enKodMaamad.Aray.GetHashCode())
       //    {
       //        fErech = GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomHeadrut.GetHashCode(), dChodesh);
       //        fErech += GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomMachalaBoded.GetHashCode(), dChodesh);
       //        fErech += GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomMachla.GetHashCode(), dChodesh);
       //        fErech += GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomMachalaYeled.GetHashCode(), dChodesh);
       //        fErech += GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomMachalatHorim.GetHashCode(), dChodesh);
       //        fErech += GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomMachalatBenZug.GetHashCode(), dChodesh);
       //        fErech += GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomTeuna.GetHashCode(), dChodesh);
       //        fErech += GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomShmiratHerayon.GetHashCode(), dChodesh);

       //        sErua416.Append(FormatNumber(fErech,4,2));
       //    }
       //    else
       //    {
       //        sErua416.Append(GetBlank(4));
       //    }
          
       //    if(iMaamad == clGeneral.enKodMaamad.Shtachim.GetHashCode())
       //    {
       //        sErua416.Append(GetBlank(4));
       //    }
       //    else if (iMaamad == clGeneral.enKodMaamad.Sachir12.GetHashCode())
       //    {
       //        fErech = GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomChofesh.GetHashCode(), dChodesh);
       //        fErech += GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomMiluim.GetHashCode(), dChodesh);
       //        fErech += GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomMachla.GetHashCode(), dChodesh);
       //        fErech += GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomMachalaYeled.GetHashCode(), dChodesh);
       //        fErech += GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomMachalatHorim.GetHashCode(), dChodesh);
       //        fErech += GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomMachalatBenZug.GetHashCode(), dChodesh);
       //        fErech += GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomShmiratHerayon.GetHashCode(), dChodesh);

       //        sErua416.Append(FormatNumber(fErech,4,2));
       //    }
       //    else 
       //    {
       //        sErua416.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomChofesh.GetHashCode(), dChodesh),4,2));
       //    }

       //    if (iMaamad != clGeneral.enKodMaamad.Shtachim.GetHashCode())
       //    {
       //        sErua416.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.ChofeshZchut.GetHashCode(), dChodesh),4,2));
       //        sErua416.Append(GetBlank(4));
       //        sErua416.Append(GetBlank(4));
       //        if (iMaamad != clGeneral.enKodMaamad.Aray.GetHashCode() && iMaamad != clGeneral.enKodMaamad.OvedBechoze.GetHashCode() && iMaamad != clGeneral.enKodMaamad.Shtachim.GetHashCode() && iMaamad != clGeneral.enKodMaamad.Sachir12.GetHashCode())
       //        {
       //            sErua416.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomMachla.GetHashCode(), dChodesh),4,2));
       //        }
       //        else
       //        {
       //            sErua416.Append(GetBlank(4));
       //        }
       //        if (iMaamad != clGeneral.enKodMaamad.Aray.GetHashCode() && iMaamad != clGeneral.enKodMaamad.OvedBechoze.GetHashCode() && iMaamad != clGeneral.enKodMaamad.GimlaiBechoze.GetHashCode())
       //        {
       //            sErua416.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomMachalaBoded.GetHashCode(), dChodesh),4,2));
       //        }
       //        else
       //        {
       //            sErua416.Append(GetBlank(4));
       //        }
       //        if (iMaamad != clGeneral.enKodMaamad.Aray.GetHashCode() && iMaamad != clGeneral.enKodMaamad.OvedBechoze.GetHashCode() && iMaamad != clGeneral.enKodMaamad.GimlaiBechoze.GetHashCode() && iMaamad != clGeneral.enKodMaamad.Sachir12.GetHashCode())
       //        {
       //            sErua416.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomMachalaYeled.GetHashCode(), dChodesh),4,2));
       //        }
       //        else
       //        {
       //            sErua416.Append(GetBlank(4));
       //        }
       //        sErua416.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomChofesh.GetHashCode(), dChodesh),4,2));
       //        sErua416.Append(GetBlank(4));
       //    }
       //    else
       //    {
       //        sErua416.Append(GetBlank(36));
       //    }

       //    if (iMaamad != clGeneral.enKodMaamad.Sachir12.GetHashCode() && iMaamad != clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
       //    {
       //        sErua416.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomMiluim.GetHashCode(), dChodesh),4,2));
       //    }
       //    else
       //    {
       //        sErua416.Append(GetBlank(4));
       //    }
       //    if (iMaamad != clGeneral.enKodMaamad.Shtachim.GetHashCode())
       //    {
       //        sErua416.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomTipatChalav.GetHashCode(), dChodesh),4,2));
       //        sErua416.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomTeuna.GetHashCode(), dChodesh),4,2));
       //        sErua416.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomHadracha.GetHashCode(), dChodesh),4,2));
       //        if (iMaamad != clGeneral.enKodMaamad.Sachir12.GetHashCode() && iMaamadRashi != clGeneral.enMaamad.Friends.GetHashCode())
       //        {
       //            sErua416.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomEvel.GetHashCode(), dChodesh),4,2));
       //        }
       //        else
       //        {
       //            sErua416.Append(GetBlank(4));
       //        }
       //        sErua416.Append(FormatNumber((GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.DakotTamritzNahagut.GetHashCode(), dChodesh)/60),5,1));
       //    }
       //    else 
       //    {
       //        sErua416.Append(GetBlank(23));
       //    }

       //    sErua416.Append(dChodesh.Month.ToString().PadLeft(2));
       //    sErua416.Append(GetBlank(5));

       //    return sErua416;
       //    }
       //    catch (Exception ex)
       //    {
       //        clLogBakashot.SetError(_lBakashaId, iMisparIshi, "E", 416, dChodesh, "CreateErua416: " + ex.Message);
       //        throw ex;
       //    }
       //}

       //private StringBuilder CreateErua417(StringBuilder sPirteyOved, int iMisparIshi, int iMaamad, int iMaamadRashi, DateTime dChodesh, DataTable dtRechivim)
       //{
       //    StringBuilder sErua417;
       //    float fErech;
       //    sErua417 = new StringBuilder();
       //    try
       //    {
       //        sErua417.Append("417");
       //        sErua417.Append(sPirteyOved);
              
       //        fErech = GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.KamutGmulChisachon.GetHashCode(), dChodesh);
       //        //fErech += GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.KamutGmulChisachonMeyuchad.GetHashCode(), dChodesh);
       //        sErua417.Append(FormatNumber(fErech,4,1));
               
       //        sErua417.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.KamutGmulChisachonNosafot.GetHashCode(), dChodesh),4,1));
                         
       //        if (iMaamad == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
       //        {
       //        fErech = GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.DakotHeadrut.GetHashCode(), dChodesh);
       //        fErech += GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.DakotChofesh.GetHashCode(), dChodesh);
       //        sErua417.Append(FormatNumber((fErech/60),4,1));
       //        }
       //        else { sErua417.Append(GetBlank(5)); }

       //        sErua417.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.SachPitzul.GetHashCode(), dChodesh),4,1));
       //       sErua417.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.SachPitzulKaful.GetHashCode(), dChodesh),4,1));
       //       sErua417.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.SachEshelBoker.GetHashCode(), dChodesh),4,1));
       //       sErua417.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.SachEshelTzaharayim.GetHashCode(), dChodesh),4,1));
       //       sErua417.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.SachEshelErev.GetHashCode(), dChodesh),4,1));
       //       sErua417.Append(FormatNumber((GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.DakotPremiaBetochMichsa.GetHashCode(), dChodesh)/60),4,1));
              
               
       //        if (iMaamadRashi == clGeneral.enMaamad.Friends.GetHashCode())
       //        {
       //            sErua417.Append(FormatNumber((GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.DakotTosefetMeshek.GetHashCode(), dChodesh)/60),4,1));
       //        }
       //        else { sErua417.Append(GetBlank(5)); }

       //        if (iMaamad == clGeneral.enKodMaamad.ChaverSofi.GetHashCode() || iMaamad == clGeneral.enKodMaamad.Sachir12.GetHashCode() || iMaamad == clGeneral.enKodMaamad.OvedBechoze.GetHashCode() || iMaamad == clGeneral.enKodMaamad.Aray.GetHashCode() || iMaamad == clGeneral.enKodMaamad.GimlaiBechoze.GetHashCode())
       //        {
       //               sErua417.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomMachalatHorim.GetHashCode(), dChodesh),4,1));
       //       }
       //          else { sErua417.Append(GetBlank(5)); }

       //         if (iMaamadRashi == clGeneral.enMaamad.Salarieds.GetHashCode())
       //        {
       //            sErua417.Append(FormatNumber((GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.DakotTosefetMeshek.GetHashCode(), dChodesh) / 60), 4, 1));
       //        }
       //        else { sErua417.Append(GetBlank(5)); }

       //         sErua417.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.PremiaSadranim.GetHashCode(), dChodesh),4,1));
       //         sErua417.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.PremiaPakachim.GetHashCode(), dChodesh),4,1));
       //         sErua417.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.PremiaRakazim.GetHashCode(), dChodesh),4,1));
       //         sErua417.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.PremyaNamlak.GetHashCode(), dChodesh),4,1));
       //         sErua417.Append(FormatNumber((GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.DakotSikun.GetHashCode(), dChodesh)/60),4,1));
       //         sErua417.Append(FormatNumber((GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.DakotPremiaShabat.GetHashCode(), dChodesh)/60),4,1));

       //         sErua417.Append(dChodesh.Month.ToString().PadLeft(2));
       //         sErua417.Append(GetBlank(5));

       //        return sErua417;
       //    }
       //    catch (Exception ex)
       //    {
       //        clLogBakashot.SetError(_lBakashaId, iMisparIshi, "E", 417, dChodesh, "CreateErua417: " + ex.Message);
       //        throw ex;
       //    }
       //}

       //private StringBuilder CreateErua418(StringBuilder sPirteyOved,int iMisparIshi, DateTime dChodesh, DataTable dtRechivim)
       //{
       //    StringBuilder sErua418;
       //    sErua418 = new StringBuilder();
       //    try
       //    {
       //    sErua418.Append("418");
       //    sErua418.Append(sPirteyOved);
       //    sErua418.Append(GetBlank(60));
       //    sErua418.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(), dChodesh),4,2));
       //    sErua418.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), dChodesh),4,2));
       //    sErua418.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode(), dChodesh),6,2));

       //    sErua418.Append(dChodesh.Month.ToString().PadLeft(2));
       //    sErua418.Append(GetBlank(5));

       //    return sErua418;
       //    }
       //    catch (Exception ex)
       //    {
       //        clLogBakashot.SetError(_lBakashaId, iMisparIshi, "E", 418, dChodesh, "CreateErua418: " + ex.Message);
       //        throw ex;
       //    }
       //}

       //private StringBuilder CreateErua419(StringBuilder sPirteyOved, int iMisparIshi,int iMaamad, DateTime dChodesh, DataTable dtRechivim)
       //{
       //    StringBuilder sErua419;
       //    float fErech;
       //    sErua419 = new StringBuilder();
       //    try
       //    {
       //        sErua419.Append("419");
       //        sErua419.Append(sPirteyOved);
       //        sErua419.Append(GetBlank(32));
       //        if (iMaamad != clGeneral.enKodMaamad.Shtachim.GetHashCode() && iMaamad != clGeneral.enKodMaamad.Sachir12.GetHashCode())
       //        {
       //            sErua419.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomMachalatBenZug.GetHashCode(), dChodesh),4,2));
       //        }
       //        else { sErua419.Append(GetBlank(5)); }
       //         if (iMaamad == clGeneral.enKodMaamad.Shtachim.GetHashCode())
       //         {
       //         sErua419.Append(FormatNumber((GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), dChodesh)/60),4,1));
       //         }
       //          else { sErua419.Append(GetBlank(5)); }

       //         sErua419.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.PremiaMachsenaim.GetHashCode(), dChodesh),4,0));

       //         if (iMaamad == clGeneral.enKodMaamad.GimlaiBechoze.GetHashCode())
       //         {
       //             fErech = GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomMachla.GetHashCode(), dChodesh);
       //             fErech += GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.YomMachalaYeled.GetHashCode(), dChodesh);
                   
       //             sErua419.Append(FormatNumber(fErech,4,2));
       //         }
       //         else { sErua419.Append(GetBlank(5)); }

       //         sErua419.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.SachEshelBokerMevkrim.GetHashCode(), dChodesh),4,2));
       //         sErua419.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), dChodesh),4,2));
       //         sErua419.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.SachEshelErevMevkrim.GetHashCode(), dChodesh),5,2));
       //         sErua419.Append(GetBlank(5));

       //         sErua419.Append(dChodesh.Month.ToString().PadLeft(2));
       //         sErua419.Append(GetBlank(5));

       //        return sErua419;
       //    }
       //    catch (Exception ex)
       //    {
       //        clLogBakashot.SetError(_lBakashaId, iMisparIshi, "E", 419, dChodesh, "CreateErua419: " + ex.Message);
       //        throw ex;
       //    }
       //}


       //private StringBuilder CreateErua460(StringBuilder sPirteyOved, int iMisparIshi, DateTime dChodesh,int iMaamad, DataTable dtRechivim)
       //{
       //    StringBuilder sErua460 = new StringBuilder();
       //    try
       //    {
       //        sErua460.Append("460");
       //        sErua460.Append(sPirteyOved);
       //        sErua460.Append(GetBlank(8));
       //        sErua460.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.PremiaGrira.GetHashCode(), dChodesh),4,0));
       //        if (iMaamad == clGeneral.enKodMaamad.ChozeMeyuchad.GetHashCode())
       //        {
       //            sErua460.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.SachEshelTzaharayimMevakrim.GetHashCode(), dChodesh),4,2));
       //        }
       //        else
       //        {
       //            sErua460.Append(GetBlank(5));
       //        }
       //          sErua460.Append(GetBlank(57));

       //          sErua460.Append(dChodesh.Month.ToString().PadLeft(2));
       //          sErua460.Append(GetBlank(5));

       //        return sErua460;
       //    }
       //    catch (Exception ex)
       //    {
       //        clLogBakashot.SetError(_lBakashaId, iMisparIshi, "E", 460, dChodesh, "CreateErua460: " + ex.Message);
       //        throw ex;
       //    }
       //}

       //private StringBuilder CreateErua462(StringBuilder sPirteyOved, int iMisparIshi, DateTime dChodesh, DataTable dtRechivim)
       //{
       //    StringBuilder sErua462 = new StringBuilder();
       //    float fErech = 0;
       //    try
       //    {
       //        sErua462.Append("462");
       //        sErua462.Append(sPirteyOved);

       //        fErech = clCalcGeneral.GetSumErechRechiv(dtRechivim.Compute("count(MISPAR_ISHI)", "MISPAR_ISHI=" + iMisparIshi + " AND KOD_RECHIV=" + clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode() + " and taarich=Convert('" + dChodesh.ToShortDateString() + "', 'System.DateTime')"));

       //        sErua462.Append(FormatNumber(fErech,4,0));
              
       //        sErua462.Append(FormatNumber(GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.MichsaYomitMechushevet.GetHashCode(), dChodesh),4,0));
       //        sErua462.Append(FormatNumber((GetErechRechiv(dtRechivim, iMisparIshi, clGeneral.enRechivim.DakotNochehutLetashlum.GetHashCode(), dChodesh)/60),4,0));
              
       //        sErua462.Append(GetBlank(61));

       //        sErua462.Append(dChodesh.Month.ToString().PadLeft(2));
       //        sErua462.Append(GetBlank(5));

       //        return sErua462;
       //    }
       //    catch (Exception ex)
       //    {
       //        clLogBakashot.SetError(_lBakashaId, iMisparIshi, "E", 462, dChodesh, "CreateErua462: " + ex.Message);
       //        throw ex;
       //    }
       //}

       //private float GetErechRechiv(DataTable dtRechivim,int iMispar_ishi, int iKodRechiv, DateTime dChodesh)
       //{
       //    DataRow[] drRechiv;
       //    float fErech=0;

       //    drRechiv = dtRechivim.Select("MISPAR_ISHI=" + iMispar_ishi + " AND KOD_RECHIV=" + iKodRechiv + " and taarich=Convert('" + dChodesh.ToShortDateString() + "', 'System.DateTime')");

       //    if (drRechiv.Length > 0)
       //    { 
       //     fErech=float.Parse(drRechiv[0]["erech_rechiv"].ToString());
       //    }

       //    return fErech;
       //}

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
               clLogBakashot.SetError(_lBakashaId, "E", 0, "InserIntoTableSugChishuv: " + ex.Message);
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
                clLogBakashot.SetError(_lBakashaId, "E", 0, "GetOvdimToTransfer: " + ex.Message);
               
                throw ex;
            }
            finally
            {
                oDal = null;
            }
        }


        private DataSet GetNetuneyDorB(long lBakashaId)
        {
            DataSet ds = new DataSet();
            clDal oDal = new clDal();

            try
            {
                oDal.AddParameter("p_request_id", ParameterType.ntOracleInt64, lBakashaId, ParameterDir.pdInput);
                oDal.AddParameter("p_cur_rechivim", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.AddParameter("p_cur_ovdim", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);

                oDal.ExecuteSP(clDefinitions.cProGetNetuneyDorB, ref ds);
              
              return ds;
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, "E", 0, "GetOvdimToTransfer: " + ex.Message);
               
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
                clLogBakashot.SetError(_lBakashaId, 0, "E", 0, null, "GetRechivimYomiim: " + ex.Message);
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
                clLogBakashot.SetError(_lBakashaId, 0, "E", 0, null, "GetRechivimYomiim: " + ex.Message);
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
                clLogBakashot.SetError(_lBakashaId, iMisparIshi,"E", 0,null ,"GetChishuvYomiToOved: " + ex.Message);
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
                clLogBakashot.SetError(_lBakashaId, "E", 0,"DeleteChishuvAfterTransfer: " +  ex.Message);
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
                clLogBakashot.SetError(_lBakashaId, "E", 0, "UpdateStatusYameyAvoda: " + ex.Message);
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
                clLogBakashot.SetError(_lBakashaId, "E", 0, "DeleteChishuvAfterTransfer: " + ex.Message);
                throw ex;
            }
        }
    }
}
