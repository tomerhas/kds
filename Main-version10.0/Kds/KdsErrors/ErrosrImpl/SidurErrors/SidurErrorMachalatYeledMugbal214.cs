using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KDSCommon.DataModels;
using Microsoft.Practices.Unity;
using KDSCommon.Interfaces.Managers;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorMachalatYeledMugbal214 : SidurErrorBase
    {
        public SidurErrorMachalatYeledMugbal214(IUnityContainer container)
            : base(container)
        {

        }

        public override bool InternalIsCorrect(ErrorInputData input)
        {
            float sum = 0;
            DateTime taarich_me;
            //בדיקה ברמת סידור         
            if (input.curSidur.iMisparSidur == 99819)
            {

                    var ovedManagaer = _container.Resolve<IOvedManager>();
                    taarich_me= DateTime.Parse("01/01/" + input.CardDate.Year.ToString());
                    sum = ovedManagaer.GetSumHouersMachala(input.iMisparIshi, 99819, taarich_me, taarich_me.AddYears(1).AddDays(-1));
                    if (sum>=104)
                    {
                        AddNewError(input);
                        return false;
                    }

                 
            }
           
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errMachalatYeledMugbal214; }
        }
    }
}