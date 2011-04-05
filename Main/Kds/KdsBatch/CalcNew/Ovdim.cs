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
    }
}
