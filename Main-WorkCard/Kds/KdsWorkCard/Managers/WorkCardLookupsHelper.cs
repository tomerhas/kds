using KDSCommon.DataModels.WorkCard;
using KDSCommon.Enums;
using KDSCommon.Interfaces;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace KdsWorkCard.Managers
{
    public class WorkCardLookupsHelper
    {
        private IUnityContainer _container;
        public WorkCardLookupsHelper(IUnityContainer container)
        {
            _container = container;
        }

        public List<SelectItem> GetHashlamaList()
        {
            List<SelectItem> Hashlamot = new List<SelectItem>();
            var cache = _container.Resolve<IKDSCacheManager>();

            for (int i = 0; i < 3; i++)
            {
                var oHashlama = new SelectItem();
                oHashlama.Value = i;
                switch (i)
                {
                    case 0: oHashlama.Description = "אין השלמה";
                        break;
                    case 1: oHashlama.Description = "השלמה לשעה";
                        break;
                    case 2: oHashlama.Description = "השלמה לשעתיים";
                        break;
                }

                Hashlamot.Add(oHashlama);
            }
            return Hashlamot;
        }

        public List<SelectItem> GetTachografList()
        {
            List<SelectItem> Tachgrafs = new List<SelectItem>();

            for (int i = 0; i < 3; i++)
            {
                var oTachgraf = new SelectItem();
                oTachgraf.Value = i;
                switch (i)
                {
                    case 0: oTachgraf.Description = "אין צורך בטכוגרף";
                        break;
                    case 1: oTachgraf.Description = "חסר טכוגרף";
                        break;
                    case 2: oTachgraf.Description = "צירף טכוגרף";
                        break;
                }

                Tachgrafs.Add(oTachgraf);
            }
            return Tachgrafs;
        }

        public List<SelectItem> GetLinaList()
        {
            List<SelectItem> Linot = new List<SelectItem>();
            var cache = _container.Resolve<IKDSCacheManager>();
            DataRow[] dtLina = cache.GetCacheItem<DataTable>(CachedItems.LookUpTables).Select(string.Concat("table_name='ctb_lina'"));

            foreach (DataRow dr in dtLina)
            {

                var oLina = new SelectItem();
                oLina.Value = ParseStringToInt(dr["KOD"].ToString());
                oLina.Description = dr["TEUR"].ToString();
                Linot.Add(oLina);

            }
            return Linot;

        }

        public List<SelectItem> GetHalbashaList()
        {
            List<SelectItem> Halbashot = new List<SelectItem>();
            var cache = _container.Resolve<IKDSCacheManager>();
            DataRow[] dtHalbasha = cache.GetCacheItem<DataTable>(CachedItems.LookUpTables).Select(string.Concat("table_name='ctb_zmaney_halbasha'"));

            foreach (DataRow dr in dtHalbasha)
            {

                var oHalbasha = new SelectItem();
                oHalbasha.Value = ParseStringToInt(dr["KOD"].ToString());
                oHalbasha.Description = dr["TEUR"].ToString();
                Halbashot.Add(oHalbasha);

            }
            return Halbashot;

        }

        public List<SelectItem> GetHashlameLeYomList()
        {
            List<SelectItem> Hashlamot = new List<SelectItem>();
            var cache = _container.Resolve<IKDSCacheManager>();
            DataRow[] dtHashlama = cache.GetCacheItem<DataTable>(CachedItems.LookUpTables).Select(string.Concat("table_name='ctb_sibot_hashlama_leyom'"));

            foreach (DataRow dr in dtHashlama)
            {

                var oHashlameLeYom = new SelectItem();
                oHashlameLeYom.Value = ParseStringToInt(dr["KOD"].ToString());
                oHashlameLeYom.Description = dr["TEUR"].ToString();
                Hashlamot.Add(oHashlameLeYom);

            }
            return Hashlamot;

        }

        public List<SelectItem> GetZmanNesiaList()
        {
            List<SelectItem> Zmanim = new List<SelectItem>();
            var cache = _container.Resolve<IKDSCacheManager>();
            DataRow[] dtZmanNesia = cache.GetCacheItem<DataTable>(CachedItems.LookUpTables).Select(string.Concat("table_name='ctb_zmaney_nesiaa'"));

            foreach (DataRow dr in dtZmanNesia)
            {

                var oZman = new SelectItem();
                oZman.Value = ParseStringToInt(dr["KOD"].ToString());
                oZman.Description = dr["TEUR"].ToString();
                Zmanim.Add(oZman);

            }
            return Zmanim;

        }

        public List<SelectItem> GetPizulList()
        {
            List<SelectItem> Pizulim = new List<SelectItem>();
            var cache = _container.Resolve<IKDSCacheManager>();
            DataRow[] dtPizul = cache.GetCacheItem<DataTable>(CachedItems.LookUpTables).Select(string.Concat("table_name='ctb_pitzul_hafsaka'"));

            foreach (DataRow dr in dtPizul)
            {

                var oPizul = new SelectItem();
                oPizul.Value = ParseStringToInt(dr["KOD"].ToString());
                oPizul.Description = dr["TEUR"].ToString();
                Pizulim.Add(oPizul);

            }
            return Pizulim;

        }

        public List<SelectItem> GetHarigaList()
        {
            List<SelectItem> Harigot = new List<SelectItem>();
            var cache = _container.Resolve<IKDSCacheManager>();
            DataRow[] dtHariga = cache.GetCacheItem<DataTable>(CachedItems.LookUpTables).Select(string.Concat("table_name='ctb_divuch_hariga_meshaot'"));

            foreach (DataRow dr in dtHariga)
            {

                var oHariga = new SelectItem();
                oHariga.Value = ParseStringToInt(dr["KOD"].ToString());
                oHariga.Description = dr["TEUR"].ToString();
                Harigot.Add(oHariga);

            }
            return Harigot;

        }

        public List<SelectItem> GetSibotLedivuachList()
        {
            List<SelectItem> sibot = new List<SelectItem>();
            var cache = _container.Resolve<IKDSCacheManager>();
            DataTable dvSibotLedivuch = cache.GetCacheItem<DataTable>(CachedItems.SibotLedivuchYadani);

            foreach (DataRow dr in dvSibotLedivuch.Rows)
            {

                var siba = new SelectItem();
                siba.Value = ParseStringToInt(dr["KOD_SIBA"].ToString());
                siba.Description = dr["TEUR_SIBA"].ToString();
                sibot.Add(siba);

            }
            //string json = JsonConvert.SerializeObject(sibot);
            //   return json;
            return sibot;

        }




        private int ParseStringToInt(string sNumber)
        {
            int i = 0;
            int.TryParse(sNumber, out i);

            return i;
        }
    }
}
