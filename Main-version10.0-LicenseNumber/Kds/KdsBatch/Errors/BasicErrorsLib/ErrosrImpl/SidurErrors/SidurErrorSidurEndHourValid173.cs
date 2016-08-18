﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KdsLibrary;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.Interfaces;
using System.Data;




namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.SidurErrors
{
    public class SidurErrorSidurEndHourValid173 : SidurErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            DateTime dEndLimitHour, dStartLimitHour;
            DateTime dSidurEndHour;

            bool isSidurNahagut = false;
            bool isSidurNihulTnua = false;
            var cacheManager =  ServiceLocator.Current.GetInstance<IKDSCacheManager>();
            //DataTable sugSidur =  cacheManager.GetCacheItem<DataTable>(CachedItems.SugeySidur);
            //DataRow[] drSugSidur = clDefinitions.GetOneSugSidurMeafyen(input.curSidur.iSugSidurRagil, input.CardDate, sugSidur);
            isSidurNahagut = IsSidurNahagut(input.drSugSidur, input.curSidur);

            if (!isSidurNahagut) { isSidurNihulTnua = IsSidurNihulTnua(input.drSugSidur, input.curSidur); }

            dStartLimitHour =input.oParameters.dSidurStartLimitHourParam1;
            dEndLimitHour = (isSidurNahagut || isSidurNihulTnua) ? input.oParameters.dNahagutLimitShatGmar : input.oParameters.dSidurEndLimitHourParam3;

            if (input.curSidur.bSidurMyuhad && !string.IsNullOrEmpty(input.curSidur.sShaonNochachut) && (input.OvedDetails.iIsuk == 122 || input.OvedDetails.iIsuk == 123 || input.OvedDetails.iIsuk == 124 || input.OvedDetails.iIsuk == 127))
                dEndLimitHour = input.oParameters.dSidurLimitShatGmarMafilim;


            if ((input.OvedDetails.iIsuk != 122 && input.OvedDetails.iIsuk != 123 && input.OvedDetails.iIsuk != 124 && input.OvedDetails.iIsuk != 127) && input.oMeafyeneyOved.IsMeafyenExist(43))
                dEndLimitHour = input.oParameters.dSiyumLilaLeovedLoMafil;

            dSidurEndHour = input.curSidur.dFullShatGmar;
            if (input.curSidur.bSidurMyuhad)
            {
                if ((input.curSidur.bShatHatchalaMuteretExists) && (!String.IsNullOrEmpty(input.curSidur.sShatHatchalaMuteret))) //קיים מאפיין
                {
                    dStartLimitHour = GetDateTimeFromStringHour(DateTime.Parse(input.curSidur.sShatHatchalaMuteret).ToString("HH:mm"), input.CardDate);
                }

                if ((input.curSidur.bShatHatchalaMuteretExists) && (!String.IsNullOrEmpty(input.curSidur.sShatGmarMuteret))) //קיים מאפיין
                {
                    dEndLimitHour = GetDateTimeFromStringHour(DateTime.Parse(input.curSidur.sShatGmarMuteret).ToString("HH:mm"), input.CardDate.AddDays(1));
                }
            }

            if ((!string.IsNullOrEmpty(input.curSidur.sShatGmar) && dSidurEndHour < dStartLimitHour) && (dStartLimitHour.Year != clGeneral.cYearNull) ||
                (!string.IsNullOrEmpty(input.curSidur.sShatGmar) && dSidurEndHour > dEndLimitHour) && (dEndLimitHour.Year != clGeneral.cYearNull) ||
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