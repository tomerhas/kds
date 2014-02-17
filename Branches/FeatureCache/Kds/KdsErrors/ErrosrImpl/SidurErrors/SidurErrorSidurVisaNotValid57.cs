using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorSidurVisaNotValid57 : SidurErrorBase
    {
        public SidurErrorSidurVisaNotValid57(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            string sLookUp = "";
            if (input.curSidur.bSidurVisaKodExists)
            {
                sLookUp = GetLookUpKods("CTB_YOM_VISA", input);
                int tmpVisaCode = 0;
                Int32.TryParse(input.curSidur.sVisa, out tmpVisaCode);
                if ((sLookUp.IndexOf(input.curSidur.sVisa) == -1) || (string.IsNullOrEmpty(input.curSidur.sVisa)) || tmpVisaCode == 0)
                {
                    AddNewError(input);
                    return false;
                }
            }
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errSimunVisaNotValid; }
        }
    }
}