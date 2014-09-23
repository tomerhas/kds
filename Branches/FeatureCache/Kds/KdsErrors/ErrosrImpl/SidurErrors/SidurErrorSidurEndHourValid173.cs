using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KdsLibrary;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.Interfaces;
using System.Data;
using Microsoft.Practices.Unity;
using KDSCommon.Helpers;
using KDSCommon.Interfaces.Managers;




namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorSidurEndHourValid173 : SidurErrorBase
    {
        public SidurErrorSidurEndHourValid173(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            DateTime dEndLimitHour, dStartLimitHour;
            DateTime dSidurEndHour, dEzerDate;
            bool bFlag = false;

            bool isSidurNahagut = false;
            bool isSidurNihulTnua = false;
            var cacheManager =  ServiceLocator.Current.GetInstance<IKDSCacheManager>();
            //DataTable sugSidur =  cacheManager.GetCacheItem<DataTable>(CachedItems.SugeySidur);
            //DataRow[] drSugSidur = clDefinitions.GetOneSugSidurMeafyen(input.curSidur.iSugSidurRagil, input.CardDate, sugSidur);
            isSidurNahagut = _container.Resolve<ISidurManager>().IsSidurNahagut(input.drSugSidur, input.curSidur);

            if (!isSidurNahagut) { isSidurNihulTnua = IsSidurNihulTnua(input.drSugSidur, input.curSidur); }

            dStartLimitHour =input.oParameters.dSidurStartLimitHourParam1;
            dEndLimitHour = (isSidurNahagut || isSidurNihulTnua) ? input.oParameters.dNahagutLimitShatGmar : input.oParameters.dSidurEndLimitHourParam3;

            if (input.curSidur.bSidurMyuhad && !string.IsNullOrEmpty(input.curSidur.sShaonNochachut) && (input.OvedDetails.iIsuk == 122 || input.OvedDetails.iIsuk == 123 || input.OvedDetails.iIsuk == 124 || input.OvedDetails.iIsuk == 127))
            {
                bFlag = true;
                dEndLimitHour = input.oParameters.dSidurLimitShatGmarMafilim;
            }


            if ((input.OvedDetails.iIsuk != 122 && input.OvedDetails.iIsuk != 123 && input.OvedDetails.iIsuk != 124 && input.OvedDetails.iIsuk != 127) && input.oMeafyeneyOved.IsMeafyenExist(43))
            {
                if (!string.IsNullOrEmpty(input.curSidur.sShaonNochachut))
                    bFlag = true;
                dEndLimitHour = input.oParameters.dSiyumLilaLeovedLoMafil;
            }

            dSidurEndHour = input.curSidur.dFullShatGmar;
            if (input.curSidur.bSidurMyuhad)
            {
                if ((input.curSidur.bShatHatchalaMuteretExists) && (!String.IsNullOrEmpty(input.curSidur.sShatHatchalaMuteret))) //קיים מאפיין
                {
                    dStartLimitHour = clGeneral.GetDateTimeFromStringHour(DateTime.Parse(input.curSidur.sShatHatchalaMuteret).ToString("HH:mm"), input.CardDate);
                }

                if ((!bFlag) && (input.curSidur.bShatGmarMuteretExists) && (!String.IsNullOrEmpty(input.curSidur.sShatGmarMuteret))) //קיים מאפיין
                {
                    dEzerDate = DateTime.Parse(input.curSidur.sShatGmarMuteret);
                    dEndLimitHour = clGeneral.GetDateTimeFromStringHour(dEzerDate.ToString("HH:mm"), DateHelper.getCorrectDay(dEzerDate, input.CardDate));

                }
            }

            if ((!string.IsNullOrEmpty(input.curSidur.sShatGmar) && dSidurEndHour < dStartLimitHour) && (dStartLimitHour.Year != DateHelper.cYearNull) ||
                (!string.IsNullOrEmpty(input.curSidur.sShatGmar) && dSidurEndHour > dEndLimitHour) && (dEndLimitHour.Year != DateHelper.cYearNull) ||
                (!string.IsNullOrEmpty(input.curSidur.sShatGmar) && !string.IsNullOrEmpty(input.curSidur.sShatHatchala) && input.curSidur.dFullShatHatchala >= input.curSidur.dFullShatGmar))
            {
                AddNewError(input);
                return false;
            }
            
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errSidurHourEndNotValid; }
        }
    }
}