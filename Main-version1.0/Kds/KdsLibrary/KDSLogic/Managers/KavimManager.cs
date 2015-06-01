using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.Helpers;
using KDSCommon.Interfaces.DAL;
using KDSCommon.Interfaces.Managers;
using Microsoft.Practices.Unity;

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
    }
}
