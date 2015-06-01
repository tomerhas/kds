using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorOutMichsaNotValid118 : SidurErrorBase
    {
        public SidurErrorOutMichsaNotValid118(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (((input.curSidur.sOutMichsa != "0") && (input.curSidur.sOutMichsa != "1")) || (string.IsNullOrEmpty(input.curSidur.sOutMichsa)))
            {
                AddNewError(input);
                return false;
            }
            
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errOutMichsaNotValid; }
        }
    }
}