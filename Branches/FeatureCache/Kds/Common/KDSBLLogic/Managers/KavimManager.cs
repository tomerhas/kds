using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using KDSCommon.Enums;
using KDSCommon.Helpers;
using KDSCommon.Interfaces.DAL;
using KDSCommon.Interfaces.Managers;
using Microsoft.Practices.Unity;
using System.Web;
using System.Configuration;

namespace KdsLibrary.KDSLogic.Managers
{
    public class KavimManager : IKavimManager
    {
        private IUnityContainer _container;
        public KavimManager(IUnityContainer container)
        {
            _container = container;
        }
        public DataTable GetMakatDetails(long lNewMakat, DateTime dDate)
        {
            var kavimDal = _container.Resolve<IKavimDAL>();
            DataTable dt = new DataTable();
            enMakatType oMakatType;
            try
            {
                oMakatType = (enMakatType)StaticBL.GetMakatType(lNewMakat);
                switch (oMakatType)
                {
                    case enMakatType.mKavShirut:
                        dt = kavimDal.GetMakatKavShirut(lNewMakat, dDate);
                        break;
                    case enMakatType.mNamak:
                        dt = kavimDal.GetMakatKavNamak(lNewMakat, dDate);// ? "1" : "0";
                        break;
                    case enMakatType.mEmpty:
                        dt = kavimDal.GetMakatKavReka(lNewMakat, dDate);// ? "1" : "0";
                        break;
                    case enMakatType.mElement:
                        //אלמנט ייבדק מוך טבלת CTB_ELEMENTIM
                        // sResult = IsElementValid(lNewMakat, dDate) ? "1" : "0";
                        dt = kavimDal.GetElementDetails(lNewMakat);
                        break;
                    case enMakatType.mVisut:
                        dt = kavimDal.GetVisutDetails(lNewMakat);
                        break;
                    default:
                        break;
                }
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool IsElementValid(long lMakatNesia, DateTime dDate)
        {
            bool bValid = true;
            DataTable dt;
            DataRow[] dr;
            long lElementValue = 0;
            try
            {
                var kavimDal = _container.Resolve<IKavimDAL>();
                //Get peilut meafyeney Elementim
                dt = kavimDal.GetMeafyeneyElementByKod(lMakatNesia, dDate);
                if (dt.Rows.Count > 0)
                {
                    lElementValue = ((lMakatNesia % 100000) / 100);
                    dr = dt.Select("kod_meafyen=" + 6);
                    if (dr.Length > 0)
                    {
                        if (!String.IsNullOrEmpty(dr[0]["erech"].ToString()))
                        {
                            if (lElementValue < long.Parse(dr[0]["erech"].ToString()))
                            {
                                bValid = false;
                            }
                        }
                    }
                    dr = dt.Select("kod_meafyen=" + 7);
                    if (dr.Length > 0)
                    {
                        if (!String.IsNullOrEmpty(dr[0]["erech"].ToString()))
                        {
                            if (lElementValue > long.Parse(dr[0]["erech"].ToString()))
                            {
                                bValid = false;
                            }
                        }
                    }
                }
                else
                {
                    bValid = false;
                }
                return bValid;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public  string GetMasharCarNumbers(OrderedDictionary htEmployeeDetails)
        {
            string sCarNumbers = "";
            PeilutDM oPeilut;
            SidurDM oSidur;

            //נשרשר את כל מספרי הרכב, כדי לפנות למש"ר עם פחות נתונים
            for (int i = 0; i < htEmployeeDetails.Count; i++)
            {
                oSidur = (SidurDM)htEmployeeDetails[i];
                for (int j = 0; j < oSidur.htPeilut.Count; j++)
                {
                    oPeilut = (PeilutDM)oSidur.htPeilut[j];
                    sCarNumbers += oPeilut.lOtoNo.ToString() + ",";
                }
            }

            if (sCarNumbers.Length > 0)
            {
                sCarNumbers = sCarNumbers.Substring(0, sCarNumbers.Length - 1);
            }
            return sCarNumbers;
        }

        public DataTable GetKatalogKavim(int iMisparIshi, DateTime dFromDate, DateTime dToDate)
        {
            DataTable _Peiluyot;
            string sCacheKey = iMisparIshi + dFromDate.ToShortDateString();

            try
            {
                _Peiluyot = (DataTable)HttpRuntime.Cache.Get(sCacheKey);
            }
            catch (Exception ex)
            {
                _Peiluyot = null;
            }

            if (_Peiluyot == null)
            {
                var kavimDal = _container.Resolve<IKavimDAL>();
                _Peiluyot = kavimDal.GetKatalogKavim(iMisparIshi, dFromDate, dToDate);
                HttpRuntime.Cache.Insert(sCacheKey, _Peiluyot, null, DateTime.MaxValue, TimeSpan.FromMinutes(int.Parse((ConfigurationManager.AppSettings["PeilyutCacheTimeOutMinutes"]))));
            }

            return _Peiluyot;
        }
    }
}
