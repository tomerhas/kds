using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using KDSCommon.Interfaces.Managers;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors
{

    public class SidurErrorTipatChalavMealMichsa205: SidurErrorBase
    {
        public SidurErrorTipatChalavMealMichsa205(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {

            DateTime taarich_me;
            float p_meshech;
            var sidurManagaer = _container.Resolve<ISidurManager>(); 

            if (input.curSidur.iMisparSidur == 99814)
            {
                taarich_me = DateTime.Parse("01/" + (DateTime.Now.AddMonths(-(input.oParameters.iMaxMonthToDisplay - 1))).ToString("MM/yyyy"));
                if (input.curSidur.dSidurDate >= taarich_me)
                {
                    p_meshech = sidurManagaer.GetMeshechSidur(input.curSidur.iMisparIshi, input.curSidur.iMisparSidur, taarich_me, DateTime.Now);
                    if (p_meshech > 40)
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
            get { return ErrorTypes.errTipatChalavMealMichsa205; }
        }
    }
}