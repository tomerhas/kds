using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KDSCommon.DataModels;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorCurrentSidurInPrevSidur168 : SidurErrorBase
    {
        public SidurErrorCurrentSidurInPrevSidur168(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            bool isError = false;
            SidurDM oPrevSidur=null;
            DateTime dShatHatchalaSidur = input.curSidur.dFullShatHatchala;
            DateTime dShatGmarSidur = input.curSidur.dFullShatGmar;

            if (input.iSidur > 0) oPrevSidur = input.htEmployeeDetails[input.iSidur - 1] as SidurDM;

            if (oPrevSidur != null)
            {

                DateTime dShatHatchalaPrevSidur = oPrevSidur.dFullShatHatchala;
                DateTime dShatGmarPrevSidur = oPrevSidur.dFullShatGmar;

                for (int i = (input.iSidur - 1); i >= 0; i--)
                {
                    oPrevSidur = (SidurDM)input.htEmployeeDetails[i];
                    dShatHatchalaPrevSidur = oPrevSidur.dFullShatHatchala;
                    dShatGmarPrevSidur = oPrevSidur.dFullShatGmar;
                    if (oPrevSidur.iLoLetashlum == 0)
                    {
                        break;
                    }
                }

                if (dShatHatchalaSidur.Date == DateTime.MinValue.Date)
                {
                    dShatHatchalaSidur = input.curSidur.dFullShatGmar;
                }
                if (dShatGmarSidur.Date == DateTime.MinValue.Date)
                {
                    dShatGmarSidur = input.curSidur.dFullShatHatchala;
                }
                if (dShatHatchalaPrevSidur.Date == DateTime.MinValue.Date)
                {
                    dShatHatchalaPrevSidur = oPrevSidur.dFullShatGmar;
                }
                if (dShatGmarPrevSidur.Date == DateTime.MinValue.Date)
                {
                    dShatGmarPrevSidur = oPrevSidur.dFullShatHatchala;
                }

                if ((input.curSidur.iLoLetashlum == 0 || (input.curSidur.iLoLetashlum == 1 && input.curSidur.iKodSibaLoLetashlum == 1)) && (oPrevSidur.iLoLetashlum == 0 || (oPrevSidur.iLoLetashlum == 1 && oPrevSidur.iKodSibaLoLetashlum == 1)))
                {

                    if (dShatHatchalaSidur > dShatHatchalaPrevSidur &&
                        dShatGmarSidur < dShatGmarPrevSidur)
                    {
                        isError = true;
                    }
                }

                if (isError)
                {
                    AddNewError(input);
                    return false;
                }
            }
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errCurrentSidurInPrevSidur; }
        }
    }
}