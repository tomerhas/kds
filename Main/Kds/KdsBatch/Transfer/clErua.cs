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
        protected int _iGil;
        protected string _sHeader;
        protected string _sFooter;
        protected List<string> _sBody;
        protected DataTable _dtDetailsChishuv;
        protected DataRow _drPirteyOved;
        private List<string> _sLine;
        public bool bKayamEfreshBErua=false;
        
        protected bool _sadeLeloErech;

        public clErua(long lBakashaId, DataRow drPirteyOved, DataTable dtDetailsChishuv, int iKodErua)
        {
             _sadeLeloErech = ConfigurationSettings.AppSettings["SadeLeloErech"] =="true" ? true :false;

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
             if (drPirteyOved["gil"].ToString() != "")
                 _iGil = int.Parse(drPirteyOved["gil"].ToString());
             else _iGil = -1;
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
                sHeader.Append("00"); //-maamad  --sHeader.Append(_drPirteyOved["maamad"].ToString().PadLeft(2, char.Parse("0")));
                sHeader.Append("0");
                sHeader.Append(_drPirteyOved["mispar_ishi"].ToString().PadLeft(5, char.Parse("0")));
                sHeader.Append(_drPirteyOved["sifrat_bikoret"].ToString());
                sHeader.Append(_drPirteyOved["shem_mish"].ToString().PadLeft(10).Substring(0,10));
                sHeader.Append(_drPirteyOved["shem_prat"].ToString().PadLeft(7).Substring(0,7));
                sHeader.Append("0001");
                sHeader.Append("00");
                sHeader.Append("000000000");
                //switch (_iKodErua)
                //{
                //    case 413:
                //            sHeader.Append("000000");
                //            sHeader.Append(GetBlank(6));
                //        break;
                //    case 589:
                //            sHeader.Append("00000");
                //            sHeader.Append(GetBlank(2));
                //            sHeader.Append(GetBlank(1));
                //            sHeader.Append("011");
                //            sHeader.Append("01");
                //        break;
                //    default: 
                //            sHeader.Append("0001");
                //            sHeader.Append("00");
                //            sHeader.Append("000000000");
                //        break;
                //}
            

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
                 if (_sadeLeloErech && ((_iKodErua >= 415 && _iKodErua <= 419) || _iKodErua == 460 || _iKodErua == 462))
                     sFooter.Append(GetBlank(2));
                 else 
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
            int iSfarot,iLastDigit;
            string sErech, sErechTmp;

            if (fErech.ToString().IndexOf("E") > -1) fErech = 0;

            if (fErech.ToString().IndexOf(".") > -1)
            {
                sErechTmp = fErech.ToString().Substring(0, fErech.ToString().IndexOf("."));
                sErechTmp = sErechTmp.Replace("-", "");
                iSfarot = sErechTmp.Length;
                if (iNumDigit==0) //dakot
                    fErech =float.Parse(fErech.ToString().Substring(0, fErech.ToString().IndexOf(".")));
            }
            else
                iSfarot = fErech.ToString().Replace("-", "").Length;
            if (iSfarot > (iLen - iNumDigit))
                throw new Exception("num digits of value above the permitted.wrong value=" + fErech);

          
            if (iNumDigit > 0)
            {
                sFormat = "." + sFormat.PadRight(iNumDigit, char.Parse("0"));
                sFormat = sFormat.PadLeft(iLen + 1, char.Parse("0"));
            }
            else { sFormat = sFormat.PadLeft(iLen , char.Parse("0")); }

            //dErech = clGeneral.TrimDoubleToXDigits(double.Parse(fErech.ToString()), iNumDigit);
            if (float.Parse(fErech.ToString(sFormat)) == 0 && _iKodErua != 162)
                sErech = fErech.ToString(sFormat).Replace(".", "").Replace("0", " ");
            else sErech = fErech.ToString(sFormat).Replace(".", "");

            if (_iKodErua != 162 && _iKodErua != 462 && _iKodErua != 589 && sErech.Trim() != "" && fErech < 0)
            {
                iLastDigit = int.Parse(sErech.Substring(sErech.Length - 1, 1));
                sErech = sErech.Substring(1, sErech.Length - 2);
                sErech += GetSimanEfresh(iLastDigit);
            }
          
            return sErech;
        }

        protected string GetSimanEfresh(int iLastDigit)
        {
            switch (iLastDigit)
            {
                case 0: return "}"; 
                case 1: return "J"; 
                case 2: return "K"; 
                case 3: return "L"; 
                case 4: return "M"; 
                case 5: return "N"; 
                case 6: return "O"; 
                case 7: return "P"; 
                case 8: return "Q"; 
                case 9: return "R"; 
            }
            return "";
        }

        protected bool IsEmptyErua(string sErua)
        {
            if (sErua.Replace("0", "").Replace(" ", "") == "")
                return true;
            else return false;
        }
        protected string FormatNumberWithPoint(float fErech, int iLen, int iNumDigit)
        {
            //double dErech;
             string sFormat = "";
             int iSfarot;
             if (fErech.ToString().IndexOf(".") > -1)
             {
                 iSfarot = fErech.ToString().Substring(0, fErech.ToString().IndexOf(".")).Length;
                 if (iNumDigit == 0) //dakot
                     fErech = float.Parse(fErech.ToString().Substring(0, fErech.ToString().IndexOf(".")));
             }
             else
                 iSfarot = fErech.ToString().Length;

             if (iSfarot > (iLen - (iNumDigit+1)))
                 throw new Exception("num digits of value above the permitted.wrong value=" + fErech);

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
            return GetErechRechiv(iKodRechiv, "erech_rechiv");
        }

        protected float GetErechRechivPremiya(int iKodRechiv, DataTable dtPrem)
        {
            DataRow[] dr;
            float erech = 0;
            if (dtPrem.Rows.Count>0)
            {
                dr = dtPrem.Select("MISPAR_ISHI=" + _iMisparIshi + " AND KOD_RECHIV=" + iKodRechiv + " AND TAARICH=Convert('" + _dMonth.ToShortDateString() + "', 'System.DateTime')");
                if (dr.Length>0)
                    erech = float.Parse(dr[0]["ERECH_RECHIV"].ToString());
            }
            return erech;
        }

        protected float GetErechRechiv(int iKodRechiv,string col)
        {
            DataRow[] drRechiv;
            float fErech = 0;

            drRechiv = _dtDetailsChishuv.Select("MISPAR_ISHI=" + _iMisparIshi + " AND KOD_RECHIV=" + iKodRechiv + " and taarich=Convert('" + _dMonth.ToShortDateString() + "', 'System.DateTime')");

            if (drRechiv.Length > 0)
            {
                fErech = float.Parse(drRechiv[0][col].ToString());

                if ( fErech != 0 && col =="erech_rechiv" && (!bKayamEfreshBErua &&  _iKodErua != 162 && _iKodErua != 462 && _iKodErua != 589 ))
                {
                    CheckHefresh(drRechiv[0]);
                }
                else if (!bKayamEfreshBErua && ( _iKodErua == 462 || _iKodErua == 589))
                {
                    drRechiv = _dtDetailsChishuv.Select("MISPAR_ISHI=" + _iMisparIshi + " AND KOD_RECHIV=126 and taarich=Convert('" + _dMonth.ToShortDateString() + "', 'System.DateTime')");

                    if (drRechiv[0]["bakasha_id_2"] != null)
                        if (drRechiv[0]["bakasha_id_2"].ToString() != "")
                            bKayamEfreshBErua = true;
                }
            }
            return fErech;
        }

        private void CheckHefresh(DataRow drRechiv)
        {
            string  bakasha2="";
            float Hefresh;

            if (drRechiv["bakasha_id_2"] != null)
                bakasha2 = drRechiv["bakasha_id_2"].ToString();
            Hefresh = float.Parse(drRechiv["erech_rechiv"].ToString());

            if (bakasha2 != "" && Hefresh != 0)
                bKayamEfreshBErua = true;
        }

        protected bool KayemetRitzatHefresh()
        {
            DataRow[] drRechiv;
            drRechiv = _dtDetailsChishuv.Select("MISPAR_ISHI=" + _iMisparIshi + " and bakasha_id_2 is not null and taarich=Convert('" + _dMonth.ToShortDateString() + "', 'System.DateTime')");
            if (drRechiv.Length > 0)
                return true;
            else return false;
        }

        protected void WriteError(string sError)
        {
        clLogBakashot.SetError(_lBakashaId, _iMisparIshi, "E", _iKodErua, _dMonth, sError);
        clLogBakashot.InsertErrorToLog();
        }

        protected void PrepareLines()
        {
            int iLastDigit;
            string sErua="";
            if (bKayamEfreshBErua && _iKodErua != 413)
            {
                iLastDigit = int.Parse(_sFooter.Substring(_sFooter.Trim().Length - 1, 1));
                _sFooter = _sFooter.Replace(iLastDigit.ToString(),GetSimanEfresh(iLastDigit));
            }
            _sBody.ForEach(delegate(string Line)
            {
                if (_iKodErua == 413)
                    sErua = Line;
                else sErua = _sHeader + Line + _sFooter;
                //_sLine.Add(_sHeader + Line + _sFooter);
                _sLine.Add(sErua);
            });
         }

        public List<string> Lines
        {
            get { return _sLine; }
        }
    }
}
