using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KdsLibrary;
using KDSCommon.DataModels;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorDriverLessonsNumberValid136: SidurErrorBase
    {
        public SidurErrorDriverLessonsNumberValid136(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            int iCountShiureyNehiga = 0;

            if (!input.curSidur.bSidurMyuhad)
            {//סידורים רגילים
                if (input.drSugSidur.Length > 0)
                {   //עבור סידורים רגילים, נבדוק במאפייני סידורים אם סוג סידור נהגות.
                    if ((input.drSugSidur[0]["sug_avoda"].ToString() == enSugAvoda.Nahagut.GetHashCode().ToString()))
                    {
                        iCountShiureyNehiga = input.curSidur.htPeilut.Values.Cast<PeilutDM>().ToList().Count(Peilut => Peilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "842" && Peilut.lMakatNesia.ToString().PadLeft(8).Substring(5, 3) == "044");

                        if (iCountShiureyNehiga == 0)
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
            get { return ErrorTypes.errDriverLessonsNumberNotValid; }
        }
    }
}
