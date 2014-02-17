using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KdsLibrary;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.SidurErrors
{
    public class SidurErrorOutMichsaNotValid40 : SidurErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (input.curSidur.sOutMichsa == "1" && input.curSidur.sSectorAvoda == clGeneral.enSectorAvoda.Headrut.GetHashCode().ToString())
            {
                AddNewError(input);
                return false;
            }
                        
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errOutMichsaInSidurHeadrutNotValid; }
        }
    }
}