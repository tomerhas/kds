using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using KdsLibrary.BL;
using KdsLibrary;

namespace KdsBatch
{
    public class Ovdim
    {
        private List<Oved> _Ovdim;
        

        public List<Oved> OvdimLechishuv
        {
            get { return _Ovdim; }
        }

        public Ovdim(DateTime dTarMe, DateTime dTarAd, string sMaamad, bool bRitzaGorefet, long iBakashaId)
        {
            _Ovdim = new List<Oved>();
            SetListOvdimLechishuv(dTarMe, dTarAd, sMaamad, bRitzaGorefet, iBakashaId);
        }
        public Ovdim(long iBakashaId)
        {
            _Ovdim = new List<Oved>();
            SetListOvdimLechishuvPremia(iBakashaId);
        }

        private void SetListOvdimLechishuv(DateTime dTarMe, DateTime dTarAd, string sMaamad, bool bRitzaGorefet, long iBakashaId)
        {
            Oved ItemOved;
            DataTable dtOvdim = new DataTable();
            clCalcDal oCalcDal = new clCalcDal();
            try
            {
             /**/   dtOvdim = oCalcDal.GetOvdimLechishuv( dTarMe, dTarAd, sMaamad, bRitzaGorefet);
                for (int i = 0; i < dtOvdim.Rows.Count; i++)
                {
                    ItemOved = new Oved(int.Parse(dtOvdim.Rows[i]["mispar_ishi"].ToString()), DateTime.Parse(dtOvdim.Rows[i]["chodesh"].ToString()), dTarMe, dTarAd, iBakashaId);
                    _Ovdim.Add(ItemOved);
                }
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }

        private void SetListOvdimLechishuvPremia( long iBakashaId)
        {
            Oved ItemOved;
            DataTable dtOvdim = new DataTable();
            clCalcDal oCalcDal = new clCalcDal();
            DateTime dTarMe=new DateTime();
            DateTime dTarAd = new DateTime();
            try
            {
                dtOvdim = oCalcDal.GetPremiaCalcPopulation(ref dTarMe, ref dTarAd);
                if (dtOvdim.Rows.Count > 0)
                {
                    for (int i = 0; i < dtOvdim.Rows.Count; i++)
                    {
                        ItemOved = new Oved(int.Parse(dtOvdim.Rows[i]["mispar_ishi"].ToString()), DateTime.Parse(dtOvdim.Rows[i]["chodesh"].ToString()), dTarMe, dTarAd, iBakashaId);
                        _Ovdim.Add(ItemOved);
                    }
                }
                else _Ovdim = null;
            }
            catch (Exception ex)
            {
                throw (ex);
            }
        }
    }
}
