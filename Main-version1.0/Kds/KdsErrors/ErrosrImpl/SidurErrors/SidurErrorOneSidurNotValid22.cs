using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorOneSidurNotValid22 : SidurErrorBase
    {
        public SidurErrorOneSidurNotValid22(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if ((input.htEmployeeDetails.Count == 1) || (input.htEmployeeDetails.Count - 1 == input.iSidur))
            {
                if (!string.IsNullOrEmpty(input.curSidur.sPitzulHafsaka) && Int32.Parse(input.curSidur.sPitzulHafsaka) > 0 && Int32.Parse(input.curSidur.sPitzulHafsaka) != 3)
                {
                    AddNewError(input);
                    return false;
                }
            }
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errPizulValueNotValid; }
        }
    }
}