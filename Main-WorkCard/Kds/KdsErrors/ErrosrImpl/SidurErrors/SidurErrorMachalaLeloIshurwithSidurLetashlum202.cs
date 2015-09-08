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
    public class SidurErrorMachalaLeloIshurwithSidurLetashlum202 : SidurErrorBase
    {
        public SidurErrorMachalaLeloIshurwithSidurLetashlum202(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            //בדיקה ברמת סידור         
           
            if (input.curSidur.iMisparSidur == 99816)
            {
                if (input.htEmployeeDetails.Count > 0)
                {
                    foreach (SidurDM oSidurElse in input.htEmployeeDetails.Values)
                    {
                        if (input.curSidur != oSidurElse && oSidurElse.iLoLetashlum == 0)
                        {
                            AddNewError(input);
                            return false;
                        }
                            
                    }
                }
            }

            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errMachalaLeloIshur202; }
        }
    }
}