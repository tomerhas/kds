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
using KdsLibrary.BL;


namespace KdsBatch
{
    public class clTkinutMakatim
    {
        public void CheckTkinutMakatim(DateTime dTaarich)
        {
            StreamWriter oFileMakat=null;
            DataTable dtMakatim = new DataTable();
            DataTable dtKavim = new DataTable();
            clKavim oKavim = new clKavim();
            clBatch oBatch = new clBatch();
            int numFaild = 0, numSucceeded = 0, iMakatValid;
            clKavim.enMakatType oMakatType;
            long lMakatNesia,lRequestNum = 0;
            DateTime dCardDate;
            string Line;
            string sPathFile = ConfigurationSettings.AppSettings["PathFileReportsTemp"]+ "Tkinut_Makatim.txt";
            try
            {
                lRequestNum = clGeneral.OpenBatchRequest(KdsLibrary.clGeneral.enGeneralBatchType.InputDataAndErrorsFromUI, "CheckTkinutMakatim", -12);
              //  lRequestNum = oBatch.InsertProcessLog(77, 0, KdsLibrary.BL.RecordStatus.Wait, "start RunShguimLechishuv", 0);
              
                 dtMakatim = oKavim.GetMakatimLeTkinut(dTaarich);
                if (!File.Exists(sPathFile))
                    File.Create(sPathFile);
                oFileMakat = new StreamWriter(sPathFile , false, Encoding.Default);
             
                for (int i = 0; i < dtMakatim.Rows.Count; i++)
                {
                    try
                    {
                        lMakatNesia = long.Parse(dtMakatim.Rows[i]["MAKAT_NESIA"].ToString());
                        dCardDate = DateTime.Parse(dtMakatim.Rows[i]["TAARICH"].ToString());
                        oMakatType = (clKavim.enMakatType)oKavim.GetMakatType(lMakatNesia);
                        switch (oMakatType)
                        {
                            case clKavim.enMakatType.mKavShirut:
                                 dtKavim = oKavim.GetKavimDetailsFromTnuaDT(lMakatNesia, dCardDate, out iMakatValid);    
                                break;
                            case clKavim.enMakatType.mEmpty:
                                 dtKavim = oKavim.GetMakatRekaDetailsFromTnua(lMakatNesia, dCardDate, out iMakatValid);         
                                break;
                            case clKavim.enMakatType.mNamak:
                                 dtKavim = oKavim.GetMakatNamakDetailsFromTnua(lMakatNesia, dCardDate, out iMakatValid);
                                break;
                            default: iMakatValid = 1;
                                break;
                         }
                        if (iMakatValid == 1) //שגוי
                        {
                            Line = lMakatNesia + " ,  " + dCardDate.ToShortDateString();
                            oFileMakat.WriteLine(Line);
                            oFileMakat.Flush();
                        }
                        numSucceeded += 1;      
                    }
                    catch (Exception ex)
                    {
                        numFaild += 1;
                    }
                }
                if (!(oFileMakat == null))
                {
                    oFileMakat.Close();
                }
                clLogBakashot.InsertErrorToLog(lRequestNum, 0, "I", 0, null, "end CheckTkinutMakatim: Total Makats=" + dtMakatim.Rows.Count + "; numFaild=" + numFaild + ";  numSucceeded=" + numSucceeded);
                clDefinitions.UpdateLogBakasha(lRequestNum, DateTime.Now, KdsLibrary.BL.RecordStatus.Finish.GetHashCode());
               // oBatch.UpdateProcessLog(int.Parse(lRequestNum.ToString()), KdsLibrary.BL.RecordStatus.Finish, "end CheckTkinutMakatim: Total Makats=" + dtMakatim.Rows.Count + "; numFaild=" + numFaild + ";  numSucceeded=" + numSucceeded, 0);
            }
            catch (Exception ex)
            {
                if (!(oFileMakat == null))
                {
                    oFileMakat.Close();
                }
                clLogBakashot.InsertErrorToLog(lRequestNum, 0, "I", 0, null, "CheckTkinutMakatim:" + ex.Message);
                clDefinitions.UpdateLogBakasha(lRequestNum, DateTime.Now, KdsLibrary.BL.RecordStatus.Faild.GetHashCode());
            } 
        }
    }
}
