using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.PeilutErrors
{
    public class PeilutErrorIsCurrentPeilutInPrevPeilut162 : PeilutErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            DateTime dCurrEndPeilut = new DateTime();
            DateTime dCurrStartPeilut = new DateTime();
            DateTime dPrevEndPeilut = new DateTime();
            DateTime dPrevStartPeilut = new DateTime();
            byte bCheck = 0;
            double dblCurrTimeInMinutes = 0;
            double dblPrevTimeInMinutes = 0;
            PeilutDM oPrevPeilut;
          
            if(input.iPeilut > 0 && input.curPeilut.iMisparKnisa==0){
                oPrevPeilut = (PeilutDM)input.curSidur.htPeilut[input.iPeilut - 1];
                //זמן תחילת פעילות לאחר זמן תחילת הפעילות הקודמת לה וזמן סיום הפעילות קודם לזמן סיום הפעילות הקודמת לה. זיהוי זמן הפעילות (זיהוי סוג פעילות לפי רוטינת זיהוי מק"ט) :עבור קו שירות, נמ"ק, , יש לפנות לקטלוג נסיעות כדי לקבל את הזמן. עבור אלמנט, במידה וזה אלמנט זמן (לפי ערך 1 במאפיין 4 בטבלת מאפייני אלמנטים), הזמן נלקח מפוזיציות 4-6 של האלמנט. בבדיקה זו אין  להתייחס לפעילות המתנה (מזהים פעילות המתנה (מסוג אלמנט) לפי מאפיין 15 בטבלת מאפייני אלמנטים).           
                if ((input.curPeilut.iMakatType == enMakatType.mKavShirut.GetHashCode()) || (input.curPeilut.iMakatType == enMakatType.mNamak.GetHashCode()))
                {
                    dCurrStartPeilut = input.curPeilut.dFullShatYetzia;
                    if (input.curPeilut.iDakotBafoal > 0)
                        dblCurrTimeInMinutes = input.curPeilut.iDakotBafoal;
                    else dblCurrTimeInMinutes = input.curPeilut.iMazanTichnun;
                    bCheck = 1;
                }
            

                //זמן תחילת פעילות לאחר זמן תחילת הפעילות הקודמת לה וזמן סיום הפעילות קודם לזמן סיום הפעילות הקודמת לה. זיהוי זמן הפעילות (זיהוי סוג פעילות לפי רוטינת זיהוי מק"ט) :עבור קו שירות, נמ"ק, , יש לפנות לקטלוג נסיעות כדי לקבל את הזמן. עבור אלמנט, במידה וזה אלמנט זמן (לפי ערך 1 במאפיין 4 בטבלת מאפייני אלמנטים), הזמן נלקח מפוזיציות 4-6 של האלמנט. בבדיקה זו אין  להתייחס לפעילות המתנה (מזהים פעילות המתנה (מסוג אלמנט) לפי מאפיין 15 בטבלת מאפייני אלמנטים).           
                if ((oPrevPeilut.iMakatType == enMakatType.mKavShirut.GetHashCode()) || (oPrevPeilut.iMakatType == enMakatType.mNamak.GetHashCode()))
                {
                    dPrevStartPeilut = oPrevPeilut.dFullShatYetzia;
                    if (oPrevPeilut.iDakotBafoal > 0)
                        dblPrevTimeInMinutes = oPrevPeilut.iDakotBafoal;
                    else dblPrevTimeInMinutes = oPrevPeilut.iMazanTichnun;
                    // dblPrevTimeInMinutes = oPrevPeilut.iMazanTichnun;
                    bCheck &= 1;
                }
              

                if (bCheck == 1)
                {
                    dCurrEndPeilut = dCurrStartPeilut.AddMinutes(dblCurrTimeInMinutes);
                    dPrevEndPeilut = dPrevStartPeilut.AddMinutes(dblPrevTimeInMinutes);
                    if ((dCurrStartPeilut >= dPrevStartPeilut) && (dCurrEndPeilut < dPrevEndPeilut))
                    {
                        AddNewError(input);
                        return false;
                    }
                }
                //פעילות נבלעת בתוך פעילות קודמת
            }
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errCurrentPeilutInPrevPeilut; }
        }
    }
}

