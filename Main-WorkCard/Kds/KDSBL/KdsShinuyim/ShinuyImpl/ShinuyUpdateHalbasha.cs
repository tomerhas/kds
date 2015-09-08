using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using KDSCommon.DataModels.Shinuyim;
using KDSCommon.Enums;
using KDSCommon.Interfaces;
using KDSCommon.Interfaces.Managers;
using KDSCommon.UDT;
using KdsShinuyim.Enums;
using Microsoft.Practices.Unity;

namespace KdsShinuyim.ShinuyImpl
{
    public class ShinuyUpdateHalbasha : ShinuyBase
    {

      
        public ShinuyUpdateHalbasha(IUnityContainer container)
            : base(container)
        {

        }

        public override ShinuyTypes ShinuyType { get { return ShinuyTypes.ShinuyUpdateHalbasha; } }


        public override void ExecShinuy(ShinuyInputData inputData)
        {
            string oHalbasha;
            try
            {
                oHalbasha = inputData.oObjYameyAvodaUpd.HALBASHA.ToString();
               
                UpdateHalbasha(inputData);

                if (oHalbasha != inputData.oObjYameyAvodaUpd.HALBASHA.ToString())
                    InsertLogDay(inputData, oHalbasha, inputData.oObjYameyAvodaUpd.HALBASHA.ToString(), 43, 7);
               
            }
            catch (Exception ex)
            {
                throw new Exception("ShinuyUpdateHalbasha: " + ex.Message);
            }
        }

        private void UpdateHalbasha(ShinuyInputData inputData)
        {
            //עדכון שדה הלבשה ברמת יום עבודה
            try
            {
                //הלבשה

                if ( inputData.OvedDetails.iKodHevra == enEmployeeType.enEggedTaavora.GetHashCode())
                {
                    //עובד של אגד תעבורה לא זכאי אף פעם, גם אם הכרטיס שגוי וגם אם לא
                    if (!CheckIdkunRashemet("HALBASHA", inputData))
                    {
                        inputData.oObjYameyAvodaUpd.HALBASHA = ZmanHalbashaType.LoZakai.GetHashCode();
                        inputData.oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                    }
                }
                else
                {
                    //אם אין לעובד מאפיין הלבשה, נעדכן 0-
                    if (!inputData.oMeafyeneyOved.IsMeafyenExist(44))
                    {
                        if (!CheckIdkunRashemet("HALBASHA", inputData))
                        {
                            inputData.oObjYameyAvodaUpd.HALBASHA = 0;
                            inputData.oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                        }
                    }
                    else
                    { //קיים מאפיין 44 לעובד, זכאות להלבשה

                        SetHalbasha(inputData); 
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetHalbasha(ShinuyInputData inputData)
        {
            
            int iSidurZakaiLehalbashaKnisa = -1;
            int iSidurZakaiLehalbashaYetzia = -1;
            int iMutamut,i;
            bool bSidurLoZakaiLHalbash = false;
            bool bHaveHalbasha = false;
            bool bSidurMezake = false;
            SidurDM curSidur = null;

            try{
           
                if (inputData.htEmployeeDetails != null)
                {
                    for (i = 0; i < inputData.htEmployeeDetails.Count; i++)
                    {
                        curSidur = (SidurDM)inputData.htEmployeeDetails[i];

                        CheckZakautHalbasha(inputData, curSidur, i, ref bSidurMezake,ref iSidurZakaiLehalbashaKnisa, ref iSidurZakaiLehalbashaYetzia, ref bSidurLoZakaiLHalbash);
                    }

                    bHaveHalbasha = HaveZakaut(inputData, iSidurZakaiLehalbashaKnisa, iSidurZakaiLehalbashaYetzia);
                    if (bHaveHalbasha)
                    { //עובד אשר ענה על תנאי זכאות (אחד מהערכים 1-3)

                        // 2. עובד אשר ענה על תנאי זכאות (אחד מהערכים 1-3), אולם עבד ביום  [ (לפי רכיב מחושב 4 (דקות בתפקיד) עבור יום שאינו שבת או לפי רכיב מחושב 37 (דקות שבת בתפקיד) עבור יום שבת/שבתון ] פחות  מהערך בפרמטר 0166 (כרגע 210 דקות) ולא הייתה לו השלמה (השלמה לשעות או השלמה ליום עבודה) אשר השלימה לו את יום העבודה לזמן זה .

                        if (inputData.OvedDetails.iKodHevra == enEmployeeType.enEggedTaavora.GetHashCode())
                        {
                            //4. עובד מאגד תעבורה 
                            if (!CheckIdkunRashemet("HALBASHA", inputData))
                            {
                                inputData.oObjYameyAvodaUpd.HALBASHA = ZmanHalbashaType.LoZakai.GetHashCode();
                                inputData.oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                            }
                        }
                        else
                        {
                            //3. עובד אשר ענה על תנאי זכאות (אחד מהערכים 1-3), אולם הוא מותאם ליום קצר (יודעים שעובד הוא מותאם ליום עבודה קצר לפי שני פרמטרים – העובד מותאם (לפי קיום ערך בפרמטר 8 (קוד עובד מותאם) בטבלת פרטי עובדים ולפי קיום ערך בפרמטר 20 (זמן מותאמות) בטבלת פרטי עובדים) וזמן המותאמות שלו (לפי ערך בפרמטר 20 (זמן מותאמות) בטבלת פרטי עובדים קטן מהערך בפרמטר 0167 (כרגע 300).
                            if ((!CheckIdkunRashemet("HALBASHA", inputData)) && (inputData.OvedDetails.bMutamutExists) && (inputData.OvedDetails.iZmanMutamut < inputData.oParam.iMinDakotLemutamLeHalbasha) &&
                                (inputData.OvedDetails.iZmanMutamut > 0))
                            {
                                iMutamut = int.Parse(inputData.OvedDetails.sMutamut);
                                if ((iMutamut == 1 || iMutamut == 5 || iMutamut == 7))
                                {
                                    inputData.oObjYameyAvodaUpd.HALBASHA = ZmanHalbashaType.LoZakai.GetHashCode();
                                    inputData.oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                                }
                            }
                        }

                        if (bSidurLoZakaiLHalbash && (!CheckIdkunRashemet("HALBASHA", inputData)))
                        {   //1.
                            //לעובד מאפיין הלבשה (44) + ) ולפחות לסידור אחד יש מאפיין זכאי לזמן הלבשה (ערך 1 במאפיין 15 זכאי לזמן הלבשה במאפייני סידורים מיוחדים, לא רלוונטי לסידורים רגילים) ולא ענה על תנאים 0-3.
                            inputData.oObjYameyAvodaUpd.HALBASHA = ZmanHalbashaType.LoZakai.GetHashCode();
                            inputData.oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                        }
                        bHaveHalbasha = true;
                    }
                    //}
                    //אם מזכה הלבשה קיבל ערך, ולא הגענו למסקנה שהוא לא זכאי, נעדכן את שדה מזכה הלבשה
                    if (bHaveHalbasha && (inputData.oObjYameyAvodaUpd.HALBASHA != ZmanHalbashaType.LoZakai.GetHashCode()))
                    {
                         IdkunHalbasha(inputData, iSidurZakaiLehalbashaKnisa, iSidurZakaiLehalbashaYetzia, curSidur);
                    }

                    if (!bHaveHalbasha && (!CheckIdkunRashemet("HALBASHA", inputData)))
                    {
                        inputData.oObjYameyAvodaUpd.HALBASHA = ZmanHalbashaType.LoZakai.GetHashCode();
                        inputData.oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                    }
                }
                else
                {
                    inputData.oObjYameyAvodaUpd.HALBASHA = ZmanHalbashaType.LoZakai.GetHashCode();
                    inputData.oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void IdkunHalbasha(ShinuyInputData inputData, int iSidurZakaiLehalbashaKnisa, int iSidurZakaiLehalbashaYetzia, SidurDM curSidur)
        {
            string oHalbasa;
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
            try
            {
               
                if (iSidurZakaiLehalbashaKnisa > -1 && iSidurZakaiLehalbashaKnisa != iSidurZakaiLehalbashaYetzia)
                {
                    curSidur = (SidurDM)inputData.htEmployeeDetails[iSidurZakaiLehalbashaKnisa];
                    oObjSidurimOvdimUpd = GetSidurOvdimObject(curSidur.iMisparSidur, curSidur.dFullShatHatchala, inputData);

                    oHalbasa = oObjSidurimOvdimUpd.MEZAKE_HALBASHA.ToString();

                    oObjSidurimOvdimUpd.MEZAKE_HALBASHA = ZmanHalbashaType.ZakaiKnisa.GetHashCode(); ;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;

                    InsertLogSidur(inputData, curSidur.iMisparSidur, curSidur.dFullShatHatchala, oHalbasa, oObjSidurimOvdimUpd.MEZAKE_HALBASHA.ToString(), 43, iSidurZakaiLehalbashaKnisa, null,"MEZAKE_HALBASHA");

                }

                if (iSidurZakaiLehalbashaYetzia > -1 && iSidurZakaiLehalbashaKnisa != iSidurZakaiLehalbashaYetzia)
                {
                    curSidur = (SidurDM)inputData.htEmployeeDetails[iSidurZakaiLehalbashaYetzia];
                    oObjSidurimOvdimUpd = GetSidurOvdimObject(curSidur.iMisparSidur, curSidur.dFullShatHatchala, inputData);
                    oHalbasa = oObjSidurimOvdimUpd.MEZAKE_HALBASHA.ToString();

                    oObjSidurimOvdimUpd.MEZAKE_HALBASHA = ZmanHalbashaType.ZakaiYetiza.GetHashCode(); ;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;

                    InsertLogSidur(inputData, curSidur.iMisparSidur, curSidur.dFullShatHatchala, oHalbasa, oObjSidurimOvdimUpd.MEZAKE_HALBASHA.ToString(), 43, iSidurZakaiLehalbashaYetzia, null,"MEZAKE_HALBASHA");
                }

                if (iSidurZakaiLehalbashaYetzia > -1 && iSidurZakaiLehalbashaKnisa == iSidurZakaiLehalbashaYetzia)
                {
                    curSidur = (SidurDM)inputData.htEmployeeDetails[iSidurZakaiLehalbashaYetzia];
                    oObjSidurimOvdimUpd = GetSidurOvdimObject(curSidur.iMisparSidur, curSidur.dFullShatHatchala, inputData);
                    oHalbasa = oObjSidurimOvdimUpd.MEZAKE_HALBASHA.ToString();

                    oObjSidurimOvdimUpd.MEZAKE_HALBASHA = ZmanHalbashaType.ZakaiKnisaYetiza.GetHashCode(); ;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;

                    InsertLogSidur(inputData, curSidur.iMisparSidur, curSidur.dFullShatHatchala, oHalbasa, oObjSidurimOvdimUpd.MEZAKE_HALBASHA.ToString(), 43, iSidurZakaiLehalbashaYetzia,null, "MEZAKE_HALBASHA");
                }

                
             }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool HaveZakaut(ShinuyInputData inputData, int iSidurZakaiLehalbashaKnisa, int iSidurZakaiLehalbashaYetzia)
        {
            bool bHaveHalbasha = false;

            try
            {

                if (iSidurZakaiLehalbashaKnisa > -1)
                {
                    if (!CheckIdkunRashemet("HALBASHA", inputData))
                    {
                        inputData.oObjYameyAvodaUpd.HALBASHA = ZmanHalbashaType.ZakaiKnisa.GetHashCode();
                        inputData.oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                    }
                    bHaveHalbasha = true;
                }

                if (iSidurZakaiLehalbashaYetzia > -1)
                {
                    if (!CheckIdkunRashemet("HALBASHA", inputData))
                    {
                        inputData.oObjYameyAvodaUpd.HALBASHA = ZmanHalbashaType.ZakaiYetiza.GetHashCode();
                        inputData.oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                    }
                    bHaveHalbasha = true;
                }
                if (iSidurZakaiLehalbashaKnisa > -1 && iSidurZakaiLehalbashaYetzia > -1)
                {
                    if (!CheckIdkunRashemet("HALBASHA", inputData))
                    {
                        inputData.oObjYameyAvodaUpd.HALBASHA = ZmanHalbashaType.ZakaiKnisaYetiza.GetHashCode();
                        inputData.oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                    }
                    bHaveHalbasha = true;
                }
                return bHaveHalbasha;
            }
             
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CheckZakautHalbasha(ShinuyInputData inputData,SidurDM curSidur, int indexSidur,ref bool  bSidurMezake, ref int iSidurZakaiLehalbashaKnisa, ref int iSidurZakaiLehalbashaYetzia, ref bool bSidurLoZakaiLHalbash)
        {
            bool bSidurMisugShaonim;
            
            bool bSidurZakaiLHalbash;
            DataRow[] drSugSidur;
            bool bKnisaValid = false;
            bool bYetizaValid = false;

            try{
                var sidurManager = _container.Resolve<ISidurManager>();
                drSugSidur = sidurManager.GetOneSugSidurMeafyen(curSidur.iSugSidurRagil, inputData.CardDate);
                bSidurZakaiLHalbash = IsSidurHalbasha(curSidur);
                bSidurMisugShaonim = sidurManager.IsSidurShonim(drSugSidur, curSidur);

                if (curSidur.iLoLetashlum == 0 && !bSidurMisugShaonim && curSidur.sHalbashKod == "1")
                {
                    if (!(bSidurMezake))
                    {
                        iSidurZakaiLehalbashaKnisa = indexSidur;
                        bSidurMezake = true;
                    }

                    iSidurZakaiLehalbashaYetzia = indexSidur;
                }
                else if (curSidur.iLoLetashlum == 0 && bSidurMisugShaonim && curSidur.sHalbashKod == "1")
                {
                    if (!(bSidurMezake))
                    {
                        iSidurZakaiLehalbashaKnisa = indexSidur;
                        bSidurMezake = true;
                    }
                    iSidurZakaiLehalbashaYetzia = indexSidur;

                    if (iSidurZakaiLehalbashaKnisa > -1 && iSidurZakaiLehalbashaKnisa == indexSidur)
                    {
                        bKnisaValid = IsKnisaValid(curSidur, SIBA_LE_DIVUCH_YADANI_HALBASHA, false, inputData.oMeafyeneyOved);
                        if (!bKnisaValid)
                            iSidurZakaiLehalbashaKnisa = -1;
                    }
                    if (iSidurZakaiLehalbashaYetzia > -1 && iSidurZakaiLehalbashaYetzia == indexSidur)
                    {
                        bYetizaValid = IsYetizaValid(curSidur, SIBA_LE_DIVUCH_YADANI_HALBASHA, false, inputData.oMeafyeneyOved);
                        if (!bYetizaValid)
                            iSidurZakaiLehalbashaYetzia = -1;
                    }
                }
                else if (!bSidurLoZakaiLHalbash)
                { bSidurLoZakaiLHalbash = IsNotSidurHalbasha(curSidur); }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

       
    

     

        private bool IsNotSidurHalbasha(SidurDM curSidur)
        {
            bool bSidurLoZakaiLHalbasha = false;

            try
            {
                //TRUE הפונקציה תחזיר אם הסידור לא זכאי להלבשה 
                //ערך 2 במאפיין 15 זכאי לזמן הלבשה במאפייני סידורים מיוחדים, לא רלוונטי לסידורים רגילים
                if (curSidur.bSidurMyuhad)
                {//סידור מיוחד
                    bSidurLoZakaiLHalbasha = ((curSidur.bHalbashKodExists) && (curSidur.sHalbashKod == enMeafyen15.enLoZakai.GetHashCode().ToString()));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bSidurLoZakaiLHalbasha;
        }

        private bool IsSidurHalbasha(SidurDM curSidur)
        {
            bool bSidurZakaiLHalbasha = false;

            try
            {
                //TRUE הפונקציה תחזיר אם הסידור זכאי להלבשה 
                //ערך 1 במאפיין 15 זכאי לזמן הלבשה במאפייני סידורים מיוחדים, לא רלוונטי לסידורים רגילים
                if (curSidur.bSidurMyuhad)
                {//סידור מיוחד
                    bSidurZakaiLHalbasha = (curSidur.iLoLetashlum == 0 && (curSidur.bHalbashKodExists) && (curSidur.sHalbashKod == enMeafyen15.enZakai.GetHashCode().ToString()));
                }
            }
            catch (Exception ex)
            {
                throw ex;

            }
            return bSidurZakaiLHalbasha;
        }
    }
}
