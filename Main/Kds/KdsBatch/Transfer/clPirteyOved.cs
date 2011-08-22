using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary;
namespace KdsBatch
{
    public class PirteyOved
    {
        public int iMaamad { get; set; }
        public int iMaamadRashi { get; set; }
        public int iDirug { get; set; }
        public int iDarga { get; set; }
        public int iGil { get; set; }
        public long iBakashaId { get; set; }
        public string sChodeshIbud { get; set; }

        public clEruaDataEt oDataEt { get; set; }
        public clEruaBakaraEt oBakaraEt { get; set; }
        public clErua462 oErua462 { get; set; }
        public clErua589 oErua589 { get; set; }
        public clErua413 oErua413 { get; set; }
        public clErua415 oErua415 { get; set; }
        public clErua416 oErua416 { get; set; }
        public clErua417 oErua417 { get; set; }
        public clErua418 oErua418 { get; set; }
        public clErua419 oErua419 { get; set; }
        public clErua460 oErua460 { get; set; }

        private DataRow _drPirteyOved;
        private DataTable _dtRechivim;
        public PirteyOved(int Maamad, int MaamadRashi, int Dirug, int Darga, long BakashaId, DataRow drPirteyOved, DataTable dtDetailsChishuv)
        {
            iMaamad = Maamad;
            iMaamadRashi = MaamadRashi;
            iDirug = Dirug;
            iDarga = Darga;
            iBakashaId= BakashaId;
            _drPirteyOved = drPirteyOved;
            _dtRechivim = dtDetailsChishuv;
          //  iGil =int.Parse(drPirteyOved["gil"].ToString());
            InitializeErueyOved();
        }

        private void InitializeErueyOved()
        {
            try
            {
                if (iDirug == 85 && iDarga == 30)
                {
                    oDataEt = new clEruaDataEt(iBakashaId, _drPirteyOved, _dtRechivim);
                    oBakaraEt = new clEruaBakaraEt(iBakashaId, _drPirteyOved, _dtRechivim);
                }
                else
                {
                    oErua462 = new clErua462(iBakashaId, _drPirteyOved, _dtRechivim);
                    oErua589 = new clErua589(iBakashaId, _drPirteyOved, _dtRechivim);

                    if (iDirug != 82 && iDirug != 83)
                    {
                        oErua413 = new clErua413(iBakashaId, _drPirteyOved, _dtRechivim);
                    }

                    if (iMaamad != clGeneral.enKodMaamad.Shtachim.GetHashCode())
                    {
                        oErua415 = new clErua415(iBakashaId, _drPirteyOved, _dtRechivim);
                    }
                    oErua416 = new clErua416(iBakashaId, _drPirteyOved, _dtRechivim);
                    oErua417 = new clErua417(iBakashaId, _drPirteyOved, _dtRechivim);
                    if (iMaamadRashi != clGeneral.enMaamad.Salarieds.GetHashCode())
                    {
                        if ((iDirug != 82 && iDirug != 83 && iDirug != 85) || !(iDirug == 84 && iDarga == 1) || !(iDirug == 85 && (iDarga == 80 || iDarga == 30)))
                        {
                            oErua418 = new clErua418(iBakashaId, _drPirteyOved, _dtRechivim);
                        }
                    }

                    if (iMaamadRashi == clGeneral.enMaamad.Salarieds.GetHashCode())
                    {
                        oErua419 = new clErua419(iBakashaId, _drPirteyOved, _dtRechivim);
                        oErua460 = new clErua460(iBakashaId, _drPirteyOved, _dtRechivim);

                    }
                }
            }
            catch (Exception ex)
            {
                throw(ex);
            }
        }
    }
}
