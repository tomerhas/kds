
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using KDSCommon.Interfaces.Managers;
using Microsoft.Practices.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsErrors.ErrosrImpl.SidurErrors
{
    public class SidurErrorMichsatMachalaYeledImMugbalut210 : SidurErrorBase
    {
        private const int SIDUR_MACHALA_IM_MUGBALUT = 99819;
        public SidurErrorMichsatMachalaYeledImMugbalut210(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            DateTime taarich_me;
            float p_meshech;

            if (input.curSidur.iMisparSidur == SIDUR_MACHALA_IM_MUGBALUT)
            {
                taarich_me = DateTime.Parse("01/01/" + input.curSidur.dSidurDate.Year);

                p_meshech = _container.Resolve<ISidurManager>().GetMeshechSidur(input.curSidur.iMisparIshi, input.curSidur.iMisparSidur, taarich_me, taarich_me.AddYears(1).AddDays(-1));

                if (p_meshech >= 52)
                {
                    AddNewError(input);
                    return false;
                }
            }
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errMichsatMachalaYeledImMugbalut; }
        }
    }
}
