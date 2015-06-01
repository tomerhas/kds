using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorPitzulHafsakaNotValid20 : SidurErrorBase
    {
        public SidurErrorPitzulHafsakaNotValid20(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            string sLookUp = "";
          //  bool bError = false;
            bool isValid = true;

            if (String.IsNullOrEmpty(input.curSidur.sPitzulHafsaka))
            {
                isValid = false;
            }
            else
            {
                sLookUp = GetLookUpKods("ctb_pitzul_hafsaka", input);
                if (sLookUp.IndexOf(input.curSidur.sPitzulHafsaka) == -1)
                {
                    isValid = false;
                }
            }
            if(!isValid)
            {
                AddNewError(input);
                return false;
            }
            return isValid;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errPizulHafsakaValueNotValid; }
        }
    }
}