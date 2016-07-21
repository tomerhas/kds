using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using KDSCommon.Interfaces.DAL;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.PeilutErrors
{
    public class PeilutErrorIsOtoNoValid69 : PeilutErrorBase
    {
        public PeilutErrorIsOtoNoValid69(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            int kod=0;
            enMakatType oMakatType = (enMakatType)input.curPeilut.iMakatType;
            if (((oMakatType == enMakatType.mKavShirut) || (oMakatType == enMakatType.mEmpty) || (oMakatType == enMakatType.mNamak) || (oMakatType == enMakatType.mVisa)
                || (oMakatType == enMakatType.mElement && input.curPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) == "700")) || ((input.curPeilut.iMakatType == enMakatType.mElement.GetHashCode()) && input.curPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "700" && (input.curPeilut.bBusNumberMustExists) && (input.curPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "701") && (input.curPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "712") && (input.curPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "711")))
            {
                //בודקים אם הפעילות דורשת מספר רכב ואם הוא קיים וחוקי (מול מש"ר). פעילות דורשת מספר רכב אם מרוטינת זיהוי מקט חזר פרמטר שונה מאלמנט. אם חזר מהרוטינה אלנמט יש לבדוק אם דורש מספר רכב. תהיה טבלה של מספר פעילות המתחילים ב- 7 ולכל רשומה יהיה מאפיין אם הוא דורש מספר רכב. בטבלת מאפייני אלמנטים (11 - חובה מספר רכב)
                //בדיקת מספר רכב מול מש"ר

                if ((input.CardDate < input.oParameters.dParam319 && input.curPeilut.lOtoNo > 0) || (input.CardDate >= input.oParameters.dParam319 && input.curPeilut.lLicenseNumber > 0))
                {

                    if (input.CardDate < input.oParameters.dParam319)
                        kod = IsBusNumberValid(input.curPeilut.lOtoNo, input.CardDate);
                    if (input.CardDate >= input.oParameters.dParam319)
                        kod = IsLicenseNumberValid(input.curPeilut.lLicenseNumber, input.CardDate);

                    if (kod==1 || kod==2)
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

   

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errOtoNoNotExists; }
        }
    }
}

