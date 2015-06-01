using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using System.Data;
using KdsLibrary;
using KDSCommon.DataModels;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.Interfaces.Managers;
using Microsoft.Practices.Unity;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.SidurErrors
{
    public class SidurErrorAvodaByemeyMachala201 : SidurErrorBase
    {
        public SidurErrorAvodaByemeyMachala201(IUnityContainer container)
            : base(container)
        {

        }

        public override bool InternalIsCorrect(ErrorInputData input)
        {
            bool bError = false;
            DataTable dtSidurim;
            DateTime dTaarichKodem;
            clDefinitions oDefinition = new clDefinitions();
          
                if (input.curSidur.bSidurMyuhad)
                {//סידור מיוחד
                    if (!string.IsNullOrEmpty(input.curSidur.sHeadrutTypeKod) && input.curSidur.iSidurLebdikatRezefMachala > 0 && input.curSidur.sHeadrutTypeKod == clGeneral.enMeafyenSidur53.enMachala.GetHashCode().ToString()
                        && (input.curSidur.iLoLetashlum == 0 || (input.curSidur.iLoLetashlum == 1 && input.curSidur.iKodSibaLoLetashlum == 22)))
                    {
                        dTaarichKodem = input.CardDate.AddDays(-1);
                        if (CheckShaaton(input.iSugYom, dTaarichKodem,input) ||
                            dTaarichKodem.DayOfWeek == DayOfWeek.Friday)
                        //  (input.oMeafyeneyOved.GetMeafyen(56) == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode() || input.oMeafyeneyOved.GetMeafyen(56) == clGeneral.enMeafyenOved56.enOved6DaysInWeek2.GetHashCode())))
                        {
                            dtSidurim = oDefinition.GetOvedDetails(input.iMisparIshi, dTaarichKodem);
                            if (CheckAnozerSidurExsits(input) && CheckMachalaExsitsYomKodem(dtSidurim))
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
                                if (CheckAnozerSidurExsits(input) && CheckMachalaExsitsYomKodem(dtSidurim))
                                    bError = true;
                            }

                            if (!bError)
                            {
                                if (dTaarichKodem.DayOfWeek == DayOfWeek.Friday &&
                                   (input.oMeafyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || input.oMeafyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode()))
                                    dTaarichKodem = dTaarichKodem.AddDays(-1);

                                dtSidurim = oDefinition.GetOvedDetails(input.iMisparIshi, dTaarichKodem);
                                if (CheckAnozerSidurExsits(input) && CheckMachalaExsitsYomKodem(dtSidurim))
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
            get { return ErrorTypes.errAvodaByemeyMachala201; }
        }

        private bool CheckMachalaExsitsYomKodem(DataTable dtSidurim)
        {
            SidurDM oSidur;
            try
            {

                foreach (DataRow dr in dtSidurim.Rows)
                {
                    var sidurManager = ServiceLocator.Current.GetInstance<ISidurManager>();
                    oSidur = sidurManager.CreateClsSidurFromEmployeeDetails(dr);
                    if (!string.IsNullOrEmpty(oSidur.sHeadrutTypeKod) && oSidur.iSidurLebdikatRezefMachala > 0 && oSidur.sHeadrutTypeKod == clGeneral.enMeafyenSidur53.enMachala.GetHashCode().ToString()
                         && (oSidur.iLoLetashlum == 0 || (oSidur.iLoLetashlum == 1 && oSidur.iKodSibaLoLetashlum == 22)))
                        return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}