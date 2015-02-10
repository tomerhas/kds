using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using KDSCommon.DataModels.Shinuyim;
using KDSCommon.Enums;
using KdsShinuyim.Enums;
using Microsoft.Practices.Unity;

namespace KdsShinuyim.ShinuyImpl
{
    class ShinuyHashlamaForYomAvoda: ShinuyBase
    {
        public ShinuyHashlamaForYomAvoda(IUnityContainer container)
        : base(container)
        {

        }

        public override ShinuyTypes ShinuyType { get { return ShinuyTypes.ShinuyHashlamaForYomAvoda; } }


        public override void ExecShinuy(ShinuyInputData inputData)
        {
            try
            {
                if (!CheckIdkunRashemet("HASHLAMA_LEYOM", inputData))
                    UpdateHashlamaForYomAvoda(inputData);
            }
            catch (Exception ex)
            {
                throw new Exception("ShinuyHashlamaForYomAvoda: " + ex.Message);
            }
        }

        private void UpdateHashlamaForYomAvoda(ShinuyInputData inputData)
        {
            //עדכון שדה השלמה ברמת יום עבודה
            float fTimeToCompare = 0;
            float fTotalSidrimTime = 0;
            try
            {

                inputData.oObjYameyAvodaUpd.HASHLAMA_LEYOM = 0;
                inputData.oObjYameyAvodaUpd.UPDATE_OBJECT = 1;

                //עבור מותאם בנהגות (מזהים לפי ערך 2 או 3 בקוד נתון 8 (ערך מותאמות)  בטבלת פרטי עובד) שהוא מותאם לזמן קצר (מזהים מותאם לזמן קצר לפי קיום ערך בפרמטר 20 (זמן מותאמות) בטבלת פרטי עובדים) שעבד ביום (לוקחים את כל זמני הסידורים,  מחשבים גמר פחות התחלה וסוכמים) פחות מזמן המותאמות שלו (הערך בפרמטר 20) וההפרש בין זמן העבודה לזמן המותאמות קטן מהערך בפרמטר 153 (מינימום השלמה חריגה למותאם בנהגות).
                if ((inputData.OvedDetails.sMutamut == enMutaam.enMutaam2.GetHashCode().ToString()) || (inputData.OvedDetails.sMutamut == enMutaam.enMutaam3.GetHashCode().ToString()))
                {
                    SidurDM curSidur;
                    if (inputData.htEmployeeDetails != null)
                    {
                        for (int i = 0; i < inputData.htEmployeeDetails.Count; i++)
                        {
                            curSidur = (SidurDM)inputData.htEmployeeDetails[i];
                            //(נסכום את זמני הסידורים 
                            //רק עבור עובדים שיש להם קוד נתון 8 עם רק 2,3
                            if ((inputData.OvedDetails.sMutamut == enMutaam.enMutaam2.GetHashCode().ToString()) || (inputData.OvedDetails.sMutamut == enMutaam.enMutaam3.GetHashCode().ToString()))
                            {
                                fTotalSidrimTime = fTotalSidrimTime + GetSidurTimeInMinuts(curSidur);
                            }
                        }
                    }
                    //נקח את זמן המותאמות מפרמטר 20
                    if (fTotalSidrimTime < inputData.OvedDetails.iZmanMutamut)
                    {
                        fTimeToCompare = inputData.OvedDetails.iZmanMutamut - fTotalSidrimTime;
                        //מינימום השלמה חריגה למותאם בנהגות - נשווה את ההפרש לפרמטר 153
                        if (fTimeToCompare < inputData.oParam.iMinHashlamaCharigaForMutamutDriver)
                        {
                            //נשלים שעה
                            inputData.oObjYameyAvodaUpd.HASHLAMA_LEYOM = 1;
                            inputData.oObjYameyAvodaUpd.SIBAT_HASHLAMA_LEYOM = 1;
                            inputData.oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                            InsertLogDay(inputData, "0", inputData.oObjYameyAvodaUpd.HASHLAMA_LEYOM.ToString(), 48, 9);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static float GetSidurTimeInMinuts(SidurDM curSidur)
        {
            float fSidurTime = 0;
            try
            {   //מחזיר את זמן הסידור בדקות

                if ((!(string.IsNullOrEmpty(curSidur.sShatGmar))) && (!(string.IsNullOrEmpty(curSidur.sShatHatchala))))
                {
                    fSidurTime = (float)(curSidur.dFullShatGmar - curSidur.dFullShatHatchala).TotalMinutes;
                }
                return fSidurTime;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}