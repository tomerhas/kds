﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using System.Data;
using KdsLibrary;
using KDSCommon.Interfaces;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using KDSCommon.Helpers;
using KDSCommon.Interfaces.Managers;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorSidurStartHourValid14 : SidurErrorBase
    {
        public SidurErrorSidurStartHourValid14(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            DateTime dStartLimitHour, dEndLimitHour, dEzerDate;
            DateTime dSidurStartHour;
            bool bSidurNahagut = false;
            bool bSidurNihulTnua = false;
        
            dStartLimitHour = input.oParameters.dSidurStartLimitHourParam1;
            dEndLimitHour = input.oParameters.dSidurEndLimitShatHatchala;

            dSidurStartHour =  input.curSidur.dFullShatHatchala;
          //  var cacheManager = ServiceLocator.Current.GetInstance<IKDSCacheManager>();
           // DataTable sugSidur = cacheManager.GetCacheItem<DataTable>(CachedItems.SugeySidur);

           // DataRow[] drSugSidur = clDefinitions.GetOneSugSidurMeafyen(input.curSidur.iSugSidurRagil, input.CardDate, sugSidur);

            bSidurNahagut = _container.Resolve<ISidurManager>().IsSidurNahagut(input.drSugSidur, input.curSidur);
            bSidurNihulTnua = IsSidurNihulTnua(input.drSugSidur, input.curSidur);

            if (bSidurNahagut || bSidurNihulTnua)
            {
                dStartLimitHour = input.oParameters.dSidurStartLimitHourParam1;
                dEndLimitHour = input.oParameters.dShatHatchalaNahagutNihulTnua;
            }

            if (input.curSidur.bSidurMyuhad && (input.curSidur.sSugAvoda == enSugAvoda.ActualGrira.GetHashCode().ToString()))
            {
                dStartLimitHour = input.oParameters.dSidurStartLimitHourParam1;
                dEndLimitHour = input.oParameters.dShatHatchalaGrira;
            }

            if (input.curSidur.bSidurMyuhad)
            {
                if ((input.curSidur.bShatHatchalaMuteretExists) && (!String.IsNullOrEmpty(input.curSidur.sShatHatchalaMuteret))) //קיים מאפיין
                {
                    dStartLimitHour = clGeneral.GetDateTimeFromStringHour(DateTime.Parse(input.curSidur.sShatHatchalaMuteret).ToString("HH:mm"), input.CardDate);
                }

                if ((input.curSidur.bShatGmarMuteretExists) && (!String.IsNullOrEmpty(input.curSidur.sShatGmarMuteret))) //קיים מאפיין
                {
                    dEzerDate = DateTime.Parse(input.curSidur.sShatGmarMuteret);
                    dEndLimitHour = clGeneral.GetDateTimeFromStringHour(dEzerDate.ToString("HH:mm"), DateHelper.getCorrectDay(dEzerDate, input.CardDate));
                }
            }


            if ((!string.IsNullOrEmpty(input.curSidur.sShatHatchala) && dSidurStartHour < dStartLimitHour) && (dStartLimitHour.Year != DateHelper.cYearNull) ||
                (!string.IsNullOrEmpty(input.curSidur.sShatHatchala) && dSidurStartHour > dEndLimitHour) && (dEndLimitHour.Year != DateHelper.cYearNull))
            {
                AddNewError(input);
                return false;
            }
            
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errSidurHourStartNotValid; }
        }
    }
}