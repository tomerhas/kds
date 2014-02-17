using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KdsLibrary;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorSidurNetzerNotValidForOved124 : SidurErrorBase
    {
        public SidurErrorSidurNetzerNotValidForOved124(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if ((input.curSidur.bSidurMyuhad) && (input.curSidur.sSugAvoda == clGeneral.enSugAvoda.Netzer.GetHashCode().ToString()) && (!input.oMeafyeneyOved.IsMeafyenExist(64)))
            {
                AddNewError(input);
                return false;
            }
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errSidurNetzerNotValidForOved; }
        }
    }
}