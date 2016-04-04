using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsErrors.ErrosrImpl.SidurErrors
{
    public class SidurErrorOvedLeloHeterEmptyBus216 : SidurErrorBase
    {
        public SidurErrorOvedLeloHeterEmptyBus216(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {


            if (input.curSidur.iKodHeterNehiga != 80 && input.curSidur.htPeilut.Count>0 && IsSidurWithPeiluyotOnlyWithHeterEmptyBus(input.curSidur))
            {
                AddNewError(input);
                return false;
            }

            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errOvedLeloHeterEmptyBus216; }
        }
    }
}