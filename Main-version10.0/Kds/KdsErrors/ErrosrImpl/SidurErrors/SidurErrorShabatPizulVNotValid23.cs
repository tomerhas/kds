using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using Microsoft.Practices.Unity;
using KDSCommon.Helpers;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorShabatPizulVNotValid23 : SidurErrorBase
    {
        public SidurErrorShabatPizulVNotValid23(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (DateHelper.CheckShaaton(input.iSugYom, input.CardDate, input.SugeyYamimMeyuchadim) && (!string.IsNullOrEmpty(input.curSidur.sPitzulHafsaka)) && input.curSidur.sPitzulHafsaka != "0")
            {
                AddNewError(input);
                return false;
            }
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errShabatPizulValueNotValid; }
        }
    }
}