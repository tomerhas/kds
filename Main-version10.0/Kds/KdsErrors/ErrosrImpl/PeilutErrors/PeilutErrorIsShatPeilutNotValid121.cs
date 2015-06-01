using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using KDSCommon.Helpers;
using KdsLibrary;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.PeilutErrors
{
    public class PeilutErrorIsShatPeilutNotValid121 : PeilutErrorBase
    {
        public PeilutErrorIsShatPeilutNotValid121(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
       
           // long lMakatNesia = input.curPeilut.lMakatNesia;
            
            //if ((enMakatType)input.curPeilut.iMakatType == enMakatType.mElement
            //    && input.curPeilut.iElementLeYedia == 2 && lMakatNesia > 0)
            //{
            //    return true;
            //}

            if (!(string.IsNullOrEmpty(input.curPeilut.sShatYetzia)))
            {
                //if ((!((input.curPeilut.iMakatType == (long)enMakatType.mElement.GetHashCode()) && (input.curPeilut.iElementLeYedia == 2))) && (lMakatNesia > 0))
                //{
                    if (input.curSidur.dFullShatHatchala.Year > DateHelper.cYearNull)
                    {//בדיקה 121
                        if (input.curPeilut.dFullShatYetzia < input.curSidur.dFullShatHatchala)
                        {
                            AddNewError(input);
                            return false;
                        }
                        
                    }
                    //if (input.curSidur.dFullShatGmar != DateTime.MinValue)
                    //{//בדיקה 122
                    //    if (input.curPeilut.dFullShatYetzia > input.curSidur.dFullShatGmar)
                    //    {
                    //        drNew = dtErrors.NewRow();
                    //        InsertErrorRow(input.curSidur, ref drNew, "שעת פעילות גדולה משעת סיום הסידור", enErrors.errShatPeilutBiggerThanShatGmarSidur.GetHashCode());
                    //        InsertPeilutErrorRow(input.curPeilut, ref drNew);
                    //        dtErrors.Rows.Add(drNew);
                    //        isValid = false;
                    //    }
                    //}
              //  }
            }
          //שעת פעילות נמוכה משעת התחלת הסידור
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errShatPeilutSmallerThanShatHatchalaSidur; }
        }
    }
}

