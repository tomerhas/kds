using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using KDSCommon.DataModels.Shinuyim;
using KDSCommon.Enums;
using KDSCommon.Interfaces.DAL;
using KDSCommon.Interfaces.Managers;
using KDSCommon.UDT;
using KdsShinuyim.Enums;
using Microsoft.Practices.Unity;

namespace KdsShinuyim.ShinuyImpl
{
    public class ShinuyUpdateBitulZmanNesiot: ShinuyBase
    {

     
        public ShinuyUpdateBitulZmanNesiot(IUnityContainer container)
            : base(container)
        {

        }

        public override ShinuyTypes ShinuyType { get { return ShinuyTypes.ShinuyUpdateBitulZmanNesiot; } }


        public override void ExecShinuy(ShinuyInputData inputData)
        {
            string oZmanHaloch, oZmanChazor, oBitulNesiot;
            try
            {
                oZmanHaloch = inputData.oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH.ToString();
                oZmanChazor = inputData.oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR.ToString();
                oBitulNesiot = inputData.oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT.ToString();
               
                UpdateBitulZmanNesiot(inputData);

                if (oZmanHaloch != inputData.oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH.ToString())
                    InsertLogDay(inputData, oZmanHaloch, inputData.oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH.ToString(), 42, 40);
                if (oZmanChazor != inputData.oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR.ToString())
                    InsertLogDay(inputData, oZmanHaloch, inputData.oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR.ToString(), 42, 41);
                if (oBitulNesiot != inputData.oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT.ToString())
                    InsertLogDay(inputData, oZmanHaloch, inputData.oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT.ToString(), 42, 4);
            }
             catch (Exception ex)
            {
                throw new Exception("ShinuyUpdateBitulZmanNesiot: " + ex.Message);
            }
        
        }

        private bool CheeckMeafyenNesiot(ShinuyInputData inputData)
        {
            bool noMeafyen = false;
            try
            {
                if (inputData.OvedDetails.iKodHevra == enEmployeeType.enEggedTaavora.GetHashCode() && (!inputData.oMeafyeneyOved.IsMeafyenExist(51)) && (!inputData.oMeafyeneyOved.IsMeafyenExist(61)))
                {
                    if (!CheckIdkunRashemet("BITUL_ZMAN_NESIOT", inputData))
                    {
                        inputData.oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = ZmanNesiotType.LoZakai.GetHashCode();
                        inputData.oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                    }
                    if (!CheckIdkunRashemet("ZMAN_NESIA_HALOCH", inputData))
                    {
                        inputData.oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = 0;
                        inputData.oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                    }
                    if (!CheckIdkunRashemet("ZMAN_NESIA_HAZOR", inputData))
                    {
                        inputData.oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = 0;
                        inputData.oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                    }

                    noMeafyen = true;
                }
                else if ((!inputData.oMeafyeneyOved.IsMeafyenExist(51)) && (!inputData.oMeafyeneyOved.IsMeafyenExist(61)))
                {
                    if (!CheckIdkunRashemet("BITUL_ZMAN_NESIOT", inputData))
                    {
                        inputData.oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = 0;
                        inputData.oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                    }
                    if (!CheckIdkunRashemet("ZMAN_NESIA_HALOCH", inputData))
                    {
                        inputData.oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = 0;
                        inputData.oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                    }
                    if (!CheckIdkunRashemet("ZMAN_NESIA_HAZOR", inputData))
                    {
                        inputData.oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = 0;
                        inputData.oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                    }
                    noMeafyen = true;
                }

                return noMeafyen;
            }   
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateBitulZmanNesiot(ShinuyInputData inputData)
        {
            int iZmanNesia = 0;
            bool bSidurMezake = false;
            SidurDM cutSidur;
            int iSidurZakaiLenesiaKnisa = -1;
            int iSidurZakaiLenesiaYetzia = -1;
            int iFirstMezake = -1, iLastMezake = -1;
          
            //עדכון שדה ביטול זמן נסיעות ברמת יום עבודה
            try
            {
                //אם אין מאפיין נסיעות (51, 61) - נעדכן ל0- 

                if (!CheeckMeafyenNesiot(inputData))
                {
                    //לעובד מאפיין 51/61 (מאפיין זמן נסיעות) והעובד זכאי רק לזמן נסיעות לעבודה (ערך 1 בספרה הראשונה של מאפיין זמן נסיעות
                    //וגם לפחות אחד הסידורים מזכה בזמן נסיעה (סידור מזכה בזמן נסיעות אם יש לו ערך 1 (זכאי) במאפיין 14 (זכאות לזמן נסיעה) בטבלת סידורים מיוחדים/מאפייני סוג סידור
                    if (!CheckIdkunRashemet("ZMAN_NESIA_HALOCH", inputData))
                    {
                        inputData.oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = 0;
                        inputData.oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                    }
                    if (!CheckIdkunRashemet("ZMAN_NESIA_HAZOR", inputData))
                    {
                        inputData.oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = 0;
                        inputData.oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                    }
                    //ברגע שמצאנו סידור אחד לפחות שזכאי לזמן נסיעות, נפסיק לחפש
                    if (inputData.htEmployeeDetails != null)
                    {
                        for (int i = 0; i < inputData.htEmployeeDetails.Count; i++)
                        {
                            cutSidur = (SidurDM)inputData.htEmployeeDetails[i];
                            CheckZakautNesiotSidur(inputData, cutSidur, i, ref bSidurMezake,ref iFirstMezake, ref iLastMezake);
                        }

                        if (iFirstMezake == -1 && iLastMezake == -1 && !CheckIdkunRashemet("BITUL_ZMAN_NESIOT", inputData))
                        {
                            inputData.oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = ZmanNesiotType.LoZakai.GetHashCode();
                        }
                        else
                        {
                            iSidurZakaiLenesiaKnisa = iFirstMezake;
                            iSidurZakaiLenesiaYetzia = iLastMezake;
                            //לעובד מאפיין 51/61 (מאפיין זמן נסיעות) והעובד זכאי רק לזמן נסיעות לעבודה (ערך 1 בספרה הראשונה של מאפיין זמן נסיעות
                            //עובד זכאי לנסיעות לעבודה
                            if (IsOvedZakaiLZmanNesiaLaAvoda(inputData.oMeafyeneyOved) || IsOvedZakaiLZmanNesiaLeMeAvoda(inputData.oMeafyeneyOved))
                            {
                                SetZakautToAvoda(inputData, iSidurZakaiLenesiaKnisa);
                            }
                            //עובד זכאי לנסיעות מהעבודה
                            if (IsOvedZakaiLZmanNesiaMeAvoda(inputData.oMeafyeneyOved) || IsOvedZakaiLZmanNesiaLeMeAvoda(inputData.oMeafyeneyOved))
                            {
                                SetZakautFromAvoda(inputData, iSidurZakaiLenesiaYetzia);
                            }

                            //עובד זכאי לנסיעות מהעבודה ולעבודה
                            if (IsOvedZakaiLZmanNesiaLeMeAvoda(inputData.oMeafyeneyOved))
                            {
                                SetZakautFromAndToAvoda(inputData, iSidurZakaiLenesiaKnisa, iSidurZakaiLenesiaYetzia);
                            }
                        }
                        if (!CheckIdkunRashemet("BITUL_ZMAN_NESIOT",inputData) && inputData.oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT != 1 && inputData.oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT != 2 && inputData.oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT != 3)
                            inputData.oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = ZmanNesiotType.LoZakai.GetHashCode();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetZakautFromAndToAvoda(ShinuyInputData inputData, int iSidurZakaiLenesiaKnisa, int iSidurZakaiLenesiaYetzia)
        {
            int iZmanNesia = 0;
            SidurDM cutSidur;
            string oldVal;
            try{
                if ((iSidurZakaiLenesiaYetzia > -1 && iSidurZakaiLenesiaKnisa > -1) || (CheckIdkunRashemet("BITUL_ZMAN_NESIOT", inputData) && inputData.oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT > 0))
                {
                    if (iSidurZakaiLenesiaYetzia > -1 && iSidurZakaiLenesiaKnisa > -1)
                    {
                        if (!CheckIdkunRashemet("BITUL_ZMAN_NESIOT", inputData))
                            inputData.oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = ZmanNesiotType.ZakaiKnisaYetiza.GetHashCode();

                        //אם ביום קיים סידור אחד בלבד  ולפי הסידור מגיע גם נסיעות כניסה וגם נסיעות יציאה - יש לעדכן את השדה "נסיעות" ברמת סידור העבודה בקוד - זכאי לנסיעות לכניסה/יציאה לעבודה.
                        if (iSidurZakaiLenesiaYetzia > -1 && iSidurZakaiLenesiaKnisa == iSidurZakaiLenesiaYetzia)
                        {
                            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
                            cutSidur = (SidurDM)inputData.htEmployeeDetails[iSidurZakaiLenesiaKnisa];
                            oObjSidurimOvdimUpd = GetSidurOvdimObject(cutSidur.iMisparSidur, cutSidur.dFullShatHatchala, inputData);
                            oldVal = oObjSidurimOvdimUpd.MEZAKE_NESIOT.ToString();
                            oObjSidurimOvdimUpd.MEZAKE_NESIOT = ZmanNesiotType.ZakaiKnisaYetiza.GetHashCode();
                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                            InsertLogDay(inputData, oldVal, oObjSidurimOvdimUpd.MEZAKE_NESIOT.ToString(), 0,null, "MEZAKE_NESIOT");
                        }
                    }
                }
                if ((inputData.oMeafyeneyOved.IsMeafyenExist(61)) && (inputData.htEmployeeDetails.Count > 0) && (iSidurZakaiLenesiaKnisa > -1 || iSidurZakaiLenesiaYetzia > -1))
                {
                    if ((inputData.oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT == 1 || inputData.oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT == 3) && iSidurZakaiLenesiaKnisa > -1 && (!CheckIdkunRashemet("ZMAN_NESIA_HALOCH", inputData)))
                    {
                        iZmanNesia = GetZmanNesiaMeshtana(iSidurZakaiLenesiaKnisa, 1, inputData);
                        if (iZmanNesia > -1)
                            inputData.oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = (int)(Math.Ceiling(iZmanNesia / 2.0));

                    }
                    if ((inputData.oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT == 2 || inputData.oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT == 3) && iSidurZakaiLenesiaYetzia > -1 && (!CheckIdkunRashemet("ZMAN_NESIA_HAZOR", inputData)))
                    {
                        iZmanNesia = GetZmanNesiaMeshtana(iSidurZakaiLenesiaYetzia, 2, inputData);
                        if (iZmanNesia > -1)
                            inputData.oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = (int)(Math.Ceiling(iZmanNesia / 2.0));
                    }
                }

                if (inputData.oMeafyeneyOved.IsMeafyenExist(51))
                {
                    iZmanNesia = int.Parse(inputData.oMeafyeneyOved.GetMeafyen(51).Value.ToString().PadRight(3, char.Parse("0")).Substring(1));
                    // ((iSidurZakaiLenesiaKnisa > -1 || CheckIdkunRashemet("BITUL_ZMAN_NESIOT",inputData)) &&
                    if ((inputData.oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT == 1 || inputData.oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT == 3) && (!CheckIdkunRashemet("ZMAN_NESIA_HALOCH", inputData)))
                        inputData.oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = (int)(Math.Ceiling(iZmanNesia / 2.0));
                    if ((inputData.oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT == 2 || inputData.oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT == 3) && (!CheckIdkunRashemet("ZMAN_NESIA_HAZOR", inputData)))
                        inputData.oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = (int)(Math.Ceiling(iZmanNesia / 2.0));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetZakautFromAvoda(ShinuyInputData inputData, int iSidurZakaiLenesiaYetzia)
        {
            int iZmanNesia = 0;
            int iErechMeafyen=0;
            string oldVal;
            try{
                if (iSidurZakaiLenesiaYetzia > -1 || (CheckIdkunRashemet("BITUL_ZMAN_NESIOT", inputData) && inputData.oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT > 0))
                {
                    if (iSidurZakaiLenesiaYetzia > -1)
                    {
                        if (!CheckIdkunRashemet("BITUL_ZMAN_NESIOT", inputData))
                            inputData.oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = ZmanNesiotType.ZakaiYetiza.GetHashCode();

                        //אם הסידור הראשון זכאי לנסיעות, נעדכן את שדה MEZAKE_NESIOT 


                        OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;

                        SidurDM cutSidur = (SidurDM)inputData.htEmployeeDetails[iSidurZakaiLenesiaYetzia];
                        oObjSidurimOvdimUpd = GetSidurOvdimObject(cutSidur.iMisparSidur, cutSidur.dFullShatHatchala, inputData);

                        oldVal = oObjSidurimOvdimUpd.MEZAKE_NESIOT.ToString();
                        oObjSidurimOvdimUpd.MEZAKE_NESIOT = ZmanNesiotType.ZakaiYetiza.GetHashCode();
                        oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                        InsertLogDay(inputData, oldVal, oObjSidurimOvdimUpd.MEZAKE_NESIOT.ToString(), 42,null, "MEZAKE_NESIOT");
                    }
                    if (IsOvedZakaiLZmanNesiaMeAvoda(inputData.oMeafyeneyOved) && (!CheckIdkunRashemet("ZMAN_NESIA_HAZOR", inputData)))
                    {
                        if ((inputData.oMeafyeneyOved.IsMeafyenExist(61)) && (inputData.htEmployeeDetails.Count > 0) && iSidurZakaiLenesiaYetzia > -1)
                        {
                            //נשלוף את הסידור האחרון
                            iZmanNesia = GetZmanNesiaMeshtana(iSidurZakaiLenesiaYetzia, 2, inputData);
                            inputData.oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = (int)(Math.Ceiling(iZmanNesia / 2.0));
                        }
                        if (inputData.oMeafyeneyOved.IsMeafyenExist(51))
                        {
                            iZmanNesia = int.Parse(inputData.oMeafyeneyOved.GetMeafyen(51).Value.PadRight(3, char.Parse("0")).Substring(1));
                            iErechMeafyen = int.Parse(inputData.oMeafyeneyOved.GetMeafyen(51).Value.Substring(0, 1));

                            if (iErechMeafyen == 2)
                            {
                                inputData.oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = iZmanNesia;
                            }
                            if (iErechMeafyen == 3)
                            {
                                inputData.oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = (int)(Math.Ceiling(iZmanNesia / 2.0));
                            }

                         //   iZmanNesia = int.Parse(inputData.oMeafyeneyOved.GetMeafyen(51).Value.ToString().PadRight(3, char.Parse("0")).Substring(1));
                         //   inputData.oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = (int)(Math.Ceiling(iZmanNesia / 2.0));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SetZakautToAvoda(ShinuyInputData inputData, int iSidurZakaiLenesiaKnisa)
        {
            int iZmanNesia = 0;
            int iErechMeafyen;
            string oldVal;
            try{
            //לפחות אחד הסידורים מזכה בזמן נסיעה (סידור מזכה בזמן נסיעות אם יש לו ערך 1 (זכאי) במאפיין 14 (זכאות לזמן נסיעה) בטבלת סידורים מיוחדים/מאפייני סוג סידור
                if (iSidurZakaiLenesiaKnisa > -1 || (CheckIdkunRashemet("BITUL_ZMAN_NESIOT", inputData) && inputData.oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT > 0))
                {
                    if (iSidurZakaiLenesiaKnisa > -1)
                    {
                        if (!CheckIdkunRashemet("BITUL_ZMAN_NESIOT", inputData))
                            inputData.oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = ZmanNesiotType.ZakaiKnisa.GetHashCode();

                        OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;

                        SidurDM cutSidur = (SidurDM)inputData.htEmployeeDetails[iSidurZakaiLenesiaKnisa];
                        oObjSidurimOvdimUpd = GetSidurOvdimObject(cutSidur.iMisparSidur, cutSidur.dFullShatHatchala, inputData);

                        oldVal = oObjSidurimOvdimUpd.MEZAKE_NESIOT.ToString();
                        oObjSidurimOvdimUpd.MEZAKE_NESIOT = ZmanNesiotType.ZakaiKnisa.GetHashCode();
                        oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                        InsertLogDay(inputData, oldVal, oObjSidurimOvdimUpd.MEZAKE_NESIOT.ToString(), 42, null,"MEZAKE_NESIOT");

                    }
                    //עבור מאפיין 51: 
                    //אם שדה נסיעות התעדכן בערך 1, אז יש לעדכן את שדה זמן נסיעה הלוך בטבלת ימי עבודה עובדים בערך הזמן ממאפיין 51
                    if (IsOvedZakaiLZmanNesiaLaAvoda(inputData.oMeafyeneyOved) && (!CheckIdkunRashemet("ZMAN_NESIA_HALOCH", inputData)))
                    {
                        if ((inputData.oMeafyeneyOved.IsMeafyenExist(61)) && iSidurZakaiLenesiaKnisa > -1)
                        {
                            //עבור מאפיין 61:
                            //אם שדה נסיעות התעדכן בערך 1 ויש ערך בשדה מיקום שעון כניסה בסידור הראשון ביום, יש לעדכן את שדה זמן נסיעה הלוך בערך מטבלה זמן נסיעה משתנה.                                        
                            iZmanNesia = GetZmanNesiaMeshtana(iSidurZakaiLenesiaKnisa, 1, inputData);
                            if (iZmanNesia > -1)
                            { inputData.oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = (int)(Math.Ceiling(iZmanNesia / 2.0)); }
                        }
                        if (inputData.oMeafyeneyOved.IsMeafyenExist(51))
                        {
                            iErechMeafyen = int.Parse(inputData.oMeafyeneyOved.GetMeafyen(51).Value.Substring(0, 1));
                            iZmanNesia = int.Parse(inputData.oMeafyeneyOved.GetMeafyen(51).Value.Substring(1));
                            if (iErechMeafyen == 1)
                            {
                                inputData.oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = iZmanNesia;
                            }
                            if (iErechMeafyen == 3)
                            {
                                inputData.oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = (int)(Math.Ceiling(iZmanNesia / 2.0));
                            }

                            //iZmanNesia = int.Parse(inputData.oMeafyeneyOved.GetMeafyen(51).Value.Substring(1));
                            //if (iZmanNesia > -1)
                            //{
                            //    inputData.oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = (int)(Math.Ceiling(iZmanNesia / 2.0));
                            //}
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void CheckZakautNesiotSidur(ShinuyInputData inputData,  SidurDM curSidur,int indexSidur,ref bool bSidurMezake,ref int iFirstMezake, ref int iLastMezake)
        {
            bool bSidurZakaiLnesiot = false;
            string sMefyen14 = "";
            DataRow[] drSugSidur;
            bool bKnisaValid = false;
            bool bYetizaValid = false;
            bool bSidurRelevanti = true;
            
            try{
                var sidurManager = _container.Resolve<ISidurManager>();
                drSugSidur = sidurManager.GetOneSugSidurMeafyen(curSidur.iSugSidurRagil, inputData.CardDate);

                bSidurZakaiLnesiot = sidurManager.IsSidurShonim(drSugSidur, curSidur);

                sMefyen14 = curSidur.sZakayLezamanNesia;
                if (!curSidur.bSidurMyuhad && drSugSidur.Length > 0) sMefyen14 = drSugSidur[0]["zakay_leaman_nesia"].ToString();

                bSidurRelevanti = true;
                if (IsSidurNihulTnua(drSugSidur, curSidur))
                {
                    if (inputData.OvedDetails.iIsuk == 401 || inputData.OvedDetails.iIsuk == 402 || inputData.OvedDetails.iIsuk == 403 ||
                        inputData.OvedDetails.iIsuk == 404 || inputData.OvedDetails.iIsuk == 421 || inputData.OvedDetails.iIsuk == 422 || inputData.OvedDetails.iIsuk == 17)
                        bSidurRelevanti = true;
                    else bSidurRelevanti = false;
                }

                if (!bSidurZakaiLnesiot && sMefyen14 == "1" && curSidur.iLoLetashlum == 0 && bSidurRelevanti)
                {
                    if (!(bSidurMezake))
                    {
                        iFirstMezake = indexSidur;
                        bSidurMezake = true;
                    }
                    iLastMezake = indexSidur;
                }
                else if (bSidurZakaiLnesiot && sMefyen14 == "1")
                {
                    if (!(bSidurMezake))
                    {
                        iFirstMezake = indexSidur;
                        bSidurMezake = true;
                    }
                    iLastMezake = indexSidur;

                    if (iFirstMezake > -1 && iFirstMezake == indexSidur)
                    {
                        bKnisaValid = IsKnisaValid((SidurDM)inputData.htEmployeeDetails[iFirstMezake], SIBA_LE_DIVUCH_YADANI_NESIAA, inputData.bSidurNahagut, inputData.oMeafyeneyOved);
                        if (!bKnisaValid)
                            iFirstMezake = -1;
                    }
                    if (iLastMezake > -1 && iLastMezake == indexSidur)
                    {
                        bYetizaValid = IsYetizaValid((SidurDM)inputData.htEmployeeDetails[iLastMezake], SIBA_LE_DIVUCH_YADANI_NESIAA, inputData.bSidurNahagut, inputData.oMeafyeneyOved);
                        if (!bYetizaValid)
                            iLastMezake = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int GetZmanNesiaMeshtana(int iSidurIndex, int iType,ShinuyInputData inputData)
        {
            int iZmanNesia = 0;
            int iMerkazErua = 0;
            int iMikumYaad = 0;
          
            //נשלוף את הסידור 
            SidurDM oSidur;
            try
            {
                if (inputData.htEmployeeDetails.Count > 0)
                {
                    oSidur = (SidurDM)inputData.htEmployeeDetails[iSidurIndex];

                    //עבור מאפיין 61:
                    if (iType == 1) //כניסה
                    {
                        if (oSidur.sMikumShaonKnisa.Length > 0)
                        {
                            iMikumYaad = int.Parse(oSidur.sMikumShaonKnisa);
                        }
                    }
                    else //יציאה
                    {
                        if (oSidur.sMikumShaonYetzia.Length > 0)
                        {
                            iMikumYaad = int.Parse(oSidur.sMikumShaonYetzia);
                        }
                    }

                    iMerkazErua = (String.IsNullOrEmpty(inputData.OvedDetails.sMercazErua) ? 0 : int.Parse(inputData.OvedDetails.sMercazErua));
                    if ((iMerkazErua > 0) && (iMikumYaad > 0))
                    {                
                        iZmanNesia = _container.Resolve<IOvedDAL>().GetZmanNesia(iMerkazErua, iMikumYaad, inputData.CardDate);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iZmanNesia;
        }

        private bool IsOvedZakaiLZmanNesiaLaAvoda(MeafyenimDM oMeafyeneyOved)
        {
            //לעובד מאפיין 51/61 (מאפיין זמן נסיעות) והעובד זכאי רק לזמן נסיעות לעבודה (ערך 1 בספרה הראשונה של מאפיין זמן נסיעות            
            return ((oMeafyeneyOved.IsMeafyenExist(61) && oMeafyeneyOved.GetMeafyen(61).Value.Substring(0, 1) == "1")
                   ||
                   (oMeafyeneyOved.IsMeafyenExist(51) && oMeafyeneyOved.GetMeafyen(51).Value.Substring(0, 1) == "1"));
        }

        private bool IsOvedZakaiLZmanNesiaMeAvoda(MeafyenimDM oMeafyeneyOved)
        {
            //לעובד מאפיין 51/61 (מאפיין זמן נסיעות) והעובד זכאי רק לזמן נסיעות מהעבודה (ערך 2 בספרה הראשונה של מאפיין זמן נסיעות            
            return ((oMeafyeneyOved.IsMeafyenExist(61) && oMeafyeneyOved.GetMeafyen(61).Value.Substring(0, 1) == "2")
                   ||
                   (oMeafyeneyOved.IsMeafyenExist(51) && oMeafyeneyOved.GetMeafyen(51).Value.Substring(0, 1) == "2"));
        }

        private bool IsOvedZakaiLZmanNesiaLeMeAvoda(MeafyenimDM oMeafyeneyOved)
        {
            //לעובד מאפיין 51/61 (מאפיין זמן נסיעות) והעובד זכאי רק לזמן נסיעות מהעבודה (ערך 3 בספרה הראשונה של מאפיין זמן נסיעות            
            return ((oMeafyeneyOved.IsMeafyenExist(61) && oMeafyeneyOved.GetMeafyen(61).Value.Substring(0, 1) == "3")
                   ||
                   (oMeafyeneyOved.IsMeafyenExist(51) && oMeafyeneyOved.GetMeafyen(51).Value.Substring(0, 1) == "3"));
        }

    }
}