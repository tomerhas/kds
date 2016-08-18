using KDSCommon.UDT;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace KDSCommon.Interfaces.Managers
{
    public interface IClockManager
    {
        void InsertControlClockRecord(DateTime taarich, int status, string teur);
        void UpdateControlClockRecord(DateTime taarich, int status, string teur);
        string GetLastHourClock();
        void SaveShaonimMovment(long BakashaId,COLL_HARMONY_MOVMENT_ERR_MOV collHarmonyMovment);
        void InsertToCollMovment(COLL_HARMONY_MOVMENT_ERR_MOV collHarmony, DataTable dt);
        void InsertToCollErrMovment(COLL_HARMONY_MOVMENT_ERR_MOV collHarmony, DataTable dt);
        void InsertTnuotShaon(long lRequestNum);
        DataSet GetNetunimToAttend();
        void InsertControlAttendRecord(DateTime taarich, int status, string teur);
        int getLastCntrlAttend();
        void UpdateControlAttendRecord(DateTime taarich, int status, string teur);
        DataSet GetKnisaIfExists(int iMisparIshi, DateTime taarich, string inShaa, int mispar_sidur, int p24);
        void InsertKnisatShaon(int mispar_ishi, DateTime taarich, string shaa, int site_kod, int mispar_sidur, string iStm, int p24);
        DataSet GetYetziaNull(int mispar_ishi, DateTime taarich, string shaa, int mispar_sidur, int p24);
        void UpdateKnisaRecord(int mispar_ishi, DateTime taarich, string shaaK, string shaaY, int site_kod, int mispar_sidur, string iStm, int p24);
        DataSet GetYetziaIfExists(int mispar_ishi, DateTime taarich, string shaa, int mispar_sidur, int p24);
        DataSet GetKnisaNull(int mispar_ishi, DateTime taarich, string shaa, int mispar_sidur, int p24);
        void UpdateYeziaRecord(int mispar_ishi, DateTime taarich, string shaaK, string shaaY, int site_kod, int mispar_sidur, string iStm, int p24);
        void InsertYeziatShaon(int mispar_ishi, DateTime taarich, string shaa, int site_kod, int mispar_sidur, string iStm, int p24);
        void InsertHityazvutPundak(int mispar_ishi, DateTime taarich, DateTime shaa, int site_kod);
       void LoadKdsFileAgtan(string InFileName, long lRequestNum);

    }
}
