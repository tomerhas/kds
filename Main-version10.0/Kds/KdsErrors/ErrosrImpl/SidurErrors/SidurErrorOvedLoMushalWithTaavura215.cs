using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KdsLibrary;
using Microsoft.Practices.Unity;
using KDSCommon.Interfaces;
using System.Data;
using Microsoft.Practices.ServiceLocation;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorOvedLoMushalWithTaavura215 : SidurErrorBase
    {

        public SidurErrorOvedLoMushalWithTaavura215(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            var berror = false;
            DataRow[] dr;
            int snif_tnua,snif_av;
            if (input.OvedDetails.iKodHevra == enEmployeeType.enEgged.GetHashCode() && input.OvedDetails.iKodHevraHashala != enEmployeeType.enEggedTaavora.GetHashCode())
            {
                if (input.curSidur.bSidurMyuhad)
                {
                    if (input.curSidur.iMisparSidur >= 99901 && input.curSidur.iMisparSidur <= 99904)
                        berror = true;
                }
                else
                {
                   var cacheManager =  ServiceLocator.Current.GetInstance<IKDSCacheManager>();
                   var SnifAvtb = cacheManager.GetCacheItem<DataTable>(CachedItems.SnifAv);
                   snif_tnua = int.Parse(input.curSidur.iMisparSidur.ToString().PadLeft(5, '0').Substring(0,2));
                   dr = SnifAvtb.Select("snif_tnua=" + snif_tnua +"and KOD_HEVRA=4895");
                   if (dr.Length > 0)
                       berror = true;
                }
            }

            if (berror)
            {
                AddNewError(input);
                return false;
            }
            else return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errOvedLoMushalWithTaavura215; }
        }
    }
}
