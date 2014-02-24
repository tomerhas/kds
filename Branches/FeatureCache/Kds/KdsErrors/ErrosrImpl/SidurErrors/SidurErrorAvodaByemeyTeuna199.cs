using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using System.Data;
using KdsLibrary;
using Microsoft.Practices.Unity;
using KDSCommon.Interfaces.Managers;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorAvodaByemeyTeuna199 : SidurErrorBase
    {
        public SidurErrorAvodaByemeyTeuna199(IUnityContainer container)
            : base(container)
        {

        }

        public override bool InternalIsCorrect(ErrorInputData input)
        {

            bool bError = false;
            DateTime dTaarichKodem;
            DataTable dtSidurim;

            var ovedManagaer = _container.Resolve<IOvedManager>(); 
         
            if (input.curSidur.bSidurMyuhad)
            {//סידור מיוחד
                if (!string.IsNullOrEmpty(input.curSidur.sHeadrutTypeKod) && input.curSidur.sHeadrutTypeKod == clGeneral.enMeafyenSidur53.enTeuna.GetHashCode().ToString()
                    && (input.curSidur.iLoLetashlum == 0 || (input.curSidur.iLoLetashlum == 1 && input.curSidur.iKodSibaLoLetashlum == 22)))
                {
                    dTaarichKodem = input.CardDate.AddDays(-1);
                    if (CheckShaaton(input.iSugYom, dTaarichKodem,input) ||
                        dTaarichKodem.DayOfWeek == DayOfWeek.Friday)
                    {
                        dtSidurim = ovedManagaer.GetOvedDetails(input.iMisparIshi, dTaarichKodem);
                        if (CheckAnozerSidurExsits(input) && CheckSidurHeadrutExsits(dtSidurim, clGeneral.enMeafyenSidur53.enTeuna.GetHashCode().ToString(), input.curSidur.iMisparSidur))
                            bError = true;
                    }

                    if (!bError)
                    {
                        if (CheckShaaton(input.iSugYom, dTaarichKodem, input) ||
                            (dTaarichKodem.DayOfWeek == DayOfWeek.Friday &&
                            (input.oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || input.oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())))
                        {
                            dTaarichKodem = dTaarichKodem.AddDays(-1);

                            dtSidurim = ovedManagaer.GetOvedDetails(input.iMisparIshi, dTaarichKodem);
                            if (CheckAnozerSidurExsits(input) && CheckSidurHeadrutExsits(dtSidurim, clGeneral.enMeafyenSidur53.enTeuna.GetHashCode().ToString(), input.curSidur.iMisparSidur))
                                bError = true;
                        }


                        if (!bError)
                        {
                            if (dTaarichKodem.DayOfWeek == DayOfWeek.Friday &&
                                (input.oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || input.oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek2.GetHashCode()))
                                dTaarichKodem = dTaarichKodem.AddDays(-1);

                            dtSidurim = ovedManagaer.GetOvedDetails(input.iMisparIshi, dTaarichKodem);
                            if (CheckAnozerSidurExsits(input) && CheckSidurHeadrutExsits(dtSidurim, clGeneral.enMeafyenSidur53.enTeuna.GetHashCode().ToString(), input.curSidur.iMisparSidur))
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
            get { return ErrorTypes.errAvodaByemeyTeuna199; }
        }
    }
}