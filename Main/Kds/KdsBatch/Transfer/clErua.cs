using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary;
using System.Data;
using System.Configuration;

namespace KdsBatch
{
    public abstract class clErua
    {
        protected long _lBakashaId;
        protected int _iKodErua;
        protected int _iMisparIshi;
        protected DateTime _dMonth;
        protected int _iMaamad;
        protected int _iMaamadRashi;
        protected int _iMushhe;
        protected int _iDirug;
        protected string _sHeader;
        protected string _sFooter;
        protected List<string> _sBody;
        protected DataTable _dtDetailsChishuv;
        protected DataRow _drPirteyOved;
        private List<string> _sLine;

        public clErua(long lBakashaId, DataRow drPirteyOved, DataTable dtDetailsChishuv, int iKodErua)
        {
             _lBakashaId = lBakashaId;
             _drPirteyOved = drPirteyOved;
             _dtDetailsChishuv = dtDetailsChishuv;
             _iKodErua = iKodErua;

             _iMisparIshi = int.Parse(drPirteyOved["mispar_ishi"].ToString());
             _dMonth = DateTime.Parse(drPirteyOved["taarich"].ToString());
             _iMaamad = int.Parse(drPirteyOved["maamad"].ToString());
             _iMaamadRashi = int.Parse(drPirteyOved["maamad_rashi"].ToString());
             _iMushhe = int.Parse(drPirteyOved["mushhe"].ToString());
             _iDirug = int.Parse(drPirteyOved["dirug"].ToString());
             _sLine = new List<string>();

             SetHeader();
             SetFooter();
        }

        protected virtual void SetHeader()
        {
            StringBuilder sHeader = new StringBuilder();

            try
            {
                sHeader.Append(_iKodErua.ToString());
                sHeader.Append(_drPirteyOved["mifal"].ToString().PadLeft(4, char.Parse("0")));
                sHeader.Append(_drPirteyOved["maamad"].ToString().PadLeft(2, char.Parse("0")));
                sHeader.Append("0");
                sHeader.Append(_drPirteyOved["mispar_ishi"].ToString().PadLeft(5, char.Parse("0")));
                sHeader.Append(_drPirteyOved["sifrat_bikoret"].ToString());
                sHeader.Append(_drPirteyOved["shem_mish"].ToString().PadLeft(10).Substring(0,10));
                sHeader.Append(_drPirteyOved["shem_prat"].ToString().PadLeft(7).Substring(0,7));
                sHeader.Append("0000");
                sHeader.Append("00");
                sHeader.Append("000000000");

                _sHeader = sHeader.ToString();
                                
            }
            catch (Exception ex)
            {
                clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", 0, null, "SetHeaderRow: " + ex.Message);
            }
         }

        protected abstract List<string> SetBody();

        protected virtual void SetFooter()
        {
            StringBuilder sFooter = new StringBuilder();
             try
             {
                 sFooter.Append(_dMonth.Month.ToString().PadLeft(2, char.Parse("0")));
                 sFooter.Append(GetBlank(5));

                 _sFooter = sFooter.ToString();

             }
             catch (Exception ex)
             {
                 clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", 0, null, "SetFooterRow: " + ex.Message);
             }
        }

        protected string FormatNumber(float fErech, int iLen, int iNumDigit)
        {
            //double dErech;
            string sFormat="";
            if (iNumDigit > 0)
            {
                sFormat = "." + sFormat.PadRight(iNumDigit, char.Parse("0"));
                sFormat = sFormat.PadLeft(iLen + 1, char.Parse("0"));
            }
            else { sFormat = sFormat.PadLeft(iLen , char.Parse("0")); }

            //dErech = clGeneral.TrimDoubleToXDigits(double.Parse(fErech.ToString()), iNumDigit);
            return fErech.ToString(sFormat).Replace(".", "");
        }

        protected string FormatNumberWithPoint(float fErech, int iLen, int iNumDigit)
        {
            //double dErech;
             string sFormat = "";
             if (iNumDigit > 0)
             {
              sFormat = "." + sFormat.PadRight(iNumDigit, char.Parse("0"));
             }
            sFormat = sFormat.PadLeft(iLen, char.Parse("0"));

            //dErech = clGeneral.TrimDoubleToXDigits(double.Parse(fErech.ToString()), iNumDigit);
            return fErech.ToString(sFormat);
        }

        protected string GetBlank(int iLength)
        {
            string sBlank = "";
            char sChar = Convert.ToChar(ConfigurationSettings.AppSettings["BlankCharFileHilan"]);

            sBlank = sBlank.PadRight(iLength, sChar);

            return sBlank;
        }

        protected float GetErechRechiv(int iKodRechiv)
        {
            DataRow[] drRechiv;
            float fErech = 0;

            drRechiv = _dtDetailsChishuv.Select("MISPAR_ISHI=" + _iMisparIshi + " AND KOD_RECHIV=" + iKodRechiv + " and taarich=Convert('" + _dMonth.ToShortDateString() + "', 'System.DateTime')");

            if (drRechiv.Length > 0)
            {
                fErech = float.Parse(drRechiv[0]["erech_rechiv"].ToString());
            }

            return fErech;
        }

        protected void WriteError(string sError)
        {
        clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", _iKodErua, _dMonth, sError);
               
        }

        protected void PrepareLines()
        {
            _sBody.ForEach(delegate(string Line)
            {
                _sLine.Add(_sHeader + Line + _sFooter);
            });
         }

        public List<string> Lines
        {
            get { return _sLine; }
        }
    }
}
