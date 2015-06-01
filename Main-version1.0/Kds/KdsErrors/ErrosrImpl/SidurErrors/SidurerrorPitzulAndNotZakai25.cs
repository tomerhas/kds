using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurerrorPitzulAndNotZakai25 : SidurErrorBase
    {
        public SidurerrorPitzulAndNotZakai25(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            if (!string.IsNullOrEmpty(input.curSidur.sPitzulHafsaka))
            {
                if ((input.OvedDetails.sKodHaver == "1") && (input.curSidur.sPitzulHafsaka == "2")) //קוד מעמד שמתחיל ב- 1 - חבר
                {
                    AddNewError(input);
                    return false;
                }
            }
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errPitzulMuchadValueNotValid; }
        }
    }
}