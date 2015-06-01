using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KdsLibrary.BL;
using KDSCommon.Helpers;
using KDSCommon.DataModels;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorKmNotExists96 : SidurErrorBase
    {
        public SidurErrorKmNotExists96(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            bool isValid = true;
            
            if (input.curSidur.bSidurVisaKodExists)
            {
                input.curSidur.htPeilut.Values
                                .Cast<PeilutDM>()
                                .ToList()
                                .ForEach
                                (
                                peilut =>
                                {
                                    if ((enMakatType)StaticBL.GetMakatType(peilut.lMakatNesia) == enMakatType.mVisa 
                                        && peilut.iKmVisa <= 0)
                                    {
                                        isValid = false;
                                        input.curSidur.bSadotNosafim = true;
                                    }                                        
                                }
                                );
            }

            if (!isValid)
            {
                AddNewError(input);
                return false;
            }
            
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errKmNotExists; }
        }
    }
}