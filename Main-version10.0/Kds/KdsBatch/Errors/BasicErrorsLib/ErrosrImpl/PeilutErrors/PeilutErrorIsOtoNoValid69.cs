using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using KDSCommon.Interfaces.DAL;
using Microsoft.Practices.ServiceLocation;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.PeilutErrors
{
    public class PeilutErrorIsOtoNoValid69 : PeilutErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            bool isValid = true;

            enMakatType oMakatType = (enMakatType)input.curPeilut.iMakatType;
            if (((oMakatType == enMakatType.mKavShirut) || (oMakatType == enMakatType.mEmpty) || (oMakatType == enMakatType.mNamak) || (oMakatType == enMakatType.mVisa)
                || (oMakatType == enMakatType.mElement && input.curPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "700")) || ((input.curPeilut.iMakatType == enMakatType.mElement.GetHashCode()) && input.curPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "700" && (input.curPeilut.bBusNumberMustExists) && (input.curPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "701") && (input.curPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "712") && (input.curPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "711")))
            {
                //בודקים אם הפעילות דורשת מספר רכב ואם הוא קיים וחוקי (מול מש"ר). פעילות דורשת מספר רכב אם מרוטינת זיהוי מקט חזר פרמטר שונה מאלמנט. אם חזר מהרוטינה אלנמט יש לבדוק אם דורש מספר רכב. תהיה טבלה של מספר פעילות המתחילים ב- 7 ולכל רשומה יהיה מאפיין אם הוא דורש מספר רכב. בטבלת מאפייני אלמנטים (11 - חובה מספר רכב)
                //בדיקת מספר רכב מול מש"ר

                if (input.curPeilut.lOtoNo > 0)
                {
                    if (!(IsBusNumberValid(input.curPeilut.lOtoNo, input.CardDate)))
                    {
                        AddNewError(input);
                        return false;
                    }
                }
                else //חסר מספר רכב
                {

                    //שגיאה 69
                    //בודקים אם הפעילות דורשת מספר רכב ואם הוא קיים וחוקי (מול מש"ר). פעילות דורשת מספר רכב אם מרוטינת זיהוי מקט חזר פרמטר שונה מאלמנט. אם חזר מהרוטינה אלנמט יש לבדוק אם דורש מספר רכב. תהיה טבלה של מספר פעילות המתחילים ב- 7 ולכל רשומה יהיה מאפיין אם הוא דורש מספר רכב. בטבלת מאפייני אלמנטים (11 - חובה מספר רכב)
                    
                        AddNewError(input);
                        return false;
                    
                }
            }
            //מספר רכב לא תקין/חסר מספר רכב
            return true;
        }

        private bool IsBusNumberValid(long otoNumber, DateTime cardDate)
        {
            string sCacheKey = otoNumber + cardDate.ToShortDateString();
            if (HttpRuntime.Cache.Get(sCacheKey) == null || HttpRuntime.Cache.Get(sCacheKey).ToString() == "")
            {
                var kavimDal = ServiceLocator.Current.GetInstance<IKavimDAL>();
                var result = kavimDal.IsBusNumberValid(otoNumber, cardDate);

                HttpRuntime.Cache.Insert(sCacheKey, result, null, DateTime.MaxValue, TimeSpan.FromMinutes(1440));
                return result == 0;
            }
            else
            {
                return HttpRuntime.Cache.Get(sCacheKey).ToString().Trim() == "0";
            }
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errOtoNoNotExists; }
        }
    }
}

