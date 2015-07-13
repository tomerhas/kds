﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using System.Data;
using KdsLibrary;
using Microsoft.Practices.Unity;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.SidurErrors
{
    public class SidurErrorAvodaByemeyEvel200 : SidurErrorBase
    {
        public SidurErrorAvodaByemeyEvel200(IUnityContainer container)
            : base(container)
        {

        }

        public override bool InternalIsCorrect(ErrorInputData input)
        {
            bool isValid = true;
            bool bError = false;
            DateTime dTaarichKodem;
            DataTable dtSidurim;
            clDefinitions oDefinition = new clDefinitions();
          
            if (input.curSidur.bSidurMyuhad)
            {//סידור מיוחד
                if (!string.IsNullOrEmpty(input.curSidur.sHeadrutTypeKod) && input.curSidur.sHeadrutTypeKod == clGeneral.enMeafyenSidur53.enEvel.GetHashCode().ToString()
                    && (input.curSidur.iLoLetashlum == 0 || (input.curSidur.iLoLetashlum == 1 && input.curSidur.iKodSibaLoLetashlum == 22)))
                {
                    dTaarichKodem = input.CardDate.AddDays(-1);
                    if (CheckShaaton(input.iSugYom, dTaarichKodem, input) ||
                        dTaarichKodem.DayOfWeek == DayOfWeek.Friday)
                    {
                        dtSidurim = oDefinition.GetOvedDetails(input.iMisparIshi, dTaarichKodem);
                        if (CheckAnozerSidurExsits(input) && CheckSidurHeadrutExsits(dtSidurim, clGeneral.enMeafyenSidur53.enEvel.GetHashCode().ToString(), input.curSidur.iMisparSidur))
                            bError = true;
                    }

                    if (!bError)
                    {
                        if (CheckShaaton(input.iSugYom, dTaarichKodem, input) ||
                            (dTaarichKodem.DayOfWeek == DayOfWeek.Friday &&
                            (input.oMeafyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || input.oMeafyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())))
                        {
                            dTaarichKodem = dTaarichKodem.AddDays(-1);

                            dtSidurim = oDefinition.GetOvedDetails(input.iMisparIshi, dTaarichKodem);
                            if (CheckAnozerSidurExsits(input) && CheckSidurHeadrutExsits(dtSidurim, clGeneral.enMeafyenSidur53.enEvel.GetHashCode().ToString(), input.curSidur.iMisparSidur))
                                bError = true;
                        }

                        if (!bError)
                        {
                            if (dTaarichKodem.DayOfWeek == DayOfWeek.Friday &&
                                (input.oMeafyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || input.oMeafyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode()))
                                dTaarichKodem = dTaarichKodem.AddDays(-1);

                            dtSidurim = oDefinition.GetOvedDetails(input.iMisparIshi, dTaarichKodem);
                            if (CheckAnozerSidurExsits(input) && CheckSidurHeadrutExsits(dtSidurim, clGeneral.enMeafyenSidur53.enEvel.GetHashCode().ToString(), input.curSidur.iMisparSidur))
                                bError = true;
                        }

                    }
                }
            }

            if (bError)
            {
                AddNewError(input);
                return false;
            }
           
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errAvodaByemeyEvel200; }
        }
    }
}