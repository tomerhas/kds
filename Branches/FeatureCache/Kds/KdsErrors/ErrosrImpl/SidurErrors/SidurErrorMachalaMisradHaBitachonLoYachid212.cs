
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
    public class SidurErrorMachalaMisradHaBitachonLoYachid212 : SidurErrorBase
    {
        public SidurErrorMachalaMisradHaBitachonLoYachid212(IUnityContainer container)
            : base(container)
        {

        }

        public override bool InternalIsCorrect(ErrorInputData input)
        {
            //בדיקה ברמת סידור         
            if (input.curSidur.iMisparSidur == 99805)
            {
                foreach (SidurDM sidur in input.htEmployeeDetails.Values)
                {
                    if (input.curSidur != sidur && sidur.iLoLetashlum == 0)
                    {
                        AddNewError(input);
                        return false;
                    }

                }   
            }
           
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errMachalaMisradHaBitachonLoYachid; }
        }
    }
}