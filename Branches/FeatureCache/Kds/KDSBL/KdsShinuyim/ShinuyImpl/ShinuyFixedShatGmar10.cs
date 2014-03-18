using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using KDSCommon.DataModels.Shinuyim;
using KDSCommon.Enums;
using KDSCommon.UDT;
using System.Data;
using Microsoft.Practices.Unity;
using KDSCommon.Interfaces.Managers;
using KdsShinuyim.Enums;

namespace KdsShinuyim.ShinuyImpl
{
    public class ShinuyFixedShatGmar10 : ShinuyBase
    {
        public ShinuyFixedShatGmar10(IUnityContainer container)
            : base(container)
        {

        }

        public override ShinuyTypes ShinuyType { get { return ShinuyTypes.ShinuyFixedShatGmar10; } }

        public override void ExecShinuy(ShinuyInputData inputData)
        {
            try{
                for (int i = 0; i < inputData.htEmployeeDetails.Count; i++)
                {
                    SidurDM curSidur = (SidurDM)inputData.htEmployeeDetails[i];
                    if (!CheckIdkunRashemet("SHAT_GMAR", curSidur.iMisparSidur, curSidur.dFullShatHatchala, inputData))
                    {
                        FixedShatGmar10(curSidur, i, inputData);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ShinuyFixedLina07: " + ex.Message);
            }
        }

        private void FixedShatGmar10(SidurDM curSidur, int iIndexSidur, ShinuyInputData inputData)
        {
            DateTime dShatGmar;
            PeilutDM oLastPeilutMashmautit = null;
            bool bUsedMazanTichnunInSidur = false;
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd = null;

            try
            {
                oObjSidurimOvdimUpd = GetUpdSidurObject(curSidur, inputData);
                dShatGmar = oObjSidurimOvdimUpd.SHAT_GMAR;
                if (curSidur.iMisparSidur > 1000)
                {
                    //אם הסידור הוא סידור ויזה (לפי מאפיין 45 בטבלת סידורים מיוחדים), ניתן להפסיק את הבדיקה.
                    if (string.IsNullOrEmpty(curSidur.sSidurVisaKod))
                    {
                        //אם הסידור לא מכיל פעילויות, ניתן להפסיק את הבדיקה.
                        if (curSidur.htPeilut.Count > 0)
                        {
                            //פעילות משמעותית: שירות, נמ"ק, אלמנט עם מאפיין 37 (אלמנט משמעותי לצורך חישוב שעת גמר).
                            oLastPeilutMashmautit = GetLastPeilutMashmautit(curSidur);

                            //. קיימת פעילות משמעותית אחרונה (או יחידה):
                            if (oLastPeilutMashmautit != null)
                            {
                                dShatGmar = GetShatGmarWithMashmautit(curSidur, iIndexSidur, inputData, oObjSidurimOvdimUpd.SHAT_GMAR, ref bUsedMazanTichnunInSidur);
                            }
                            else
                            {//לא קיימת פעילות משמעותית:

                                dShatGmar = GetShatGmarWithOutMashmautit(curSidur, iIndexSidur, inputData, oObjSidurimOvdimUpd.SHAT_GMAR, ref bUsedMazanTichnunInSidur);
                            }

                            if (dShatGmar != oObjSidurimOvdimUpd.SHAT_GMAR)
                            {
                                oObjSidurimOvdimUpd.SHAT_GMAR = dShatGmar;
                                oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;

                                curSidur.dFullShatGmar = oObjSidurimOvdimUpd.SHAT_GMAR;
                                curSidur.sShatGmar = curSidur.dFullShatGmar.ToString("HH:mm");
                            }

                            if (bUsedMazanTichnunInSidur)
                                inputData.bUsedMazanTichnun = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DateTime GetShatGmarWithOutMashmautit(SidurDM curSidur, int iIndexSidur, ShinuyInputData inputData, DateTime ShatGmarDefault, ref bool bUsedMazanTichnunInSidur)
        {
            try
            {
                DateTime dShatGmar = ShatGmarDefault;
                for (int i = curSidur.htPeilut.Values.Count - 1; i >= 0; i--)
                {
                    //מחפשים פעילות מסוג ריקה או אלמנט מסוג דקות (ערך 1 (דקות) במאפיין 4  (ערך האלמנט)).
                    PeilutDM oPeilut = (PeilutDM)curSidur.htPeilut[i];
                    if ((oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && oPeilut.iErechElement == 1) || oPeilut.iMakatType == enMakatType.mEmpty.GetHashCode())
                    {//מחשבים שעת גמר באופן הבא: שעת גמר = שעת יציאה של פעילות אחרונה שאינה משמעותית + משך הפעילות . 
                        dShatGmar = oPeilut.dFullShatYetzia.AddMinutes(GetMeshechPeilut(iIndexSidur, oPeilut, curSidur, inputData, ref bUsedMazanTichnunInSidur));
                        break;
                    }
                }
                return dShatGmar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private DateTime GetShatGmarWithMashmautit(SidurDM curSidur, int iIndexSidur, ShinuyInputData inputData, DateTime ShatGmarDefault, ref bool bUsedMazanTichnunInSidur)
        {
            DateTime dShatGmar = ShatGmarDefault;
            int iMeshechPeilut = 0;
            try
            {
                for (int i = curSidur.htPeilut.Values.Count - 1; i >= 0; i--)
                {
                    PeilutDM oPeilut = (PeilutDM)curSidur.htPeilut[i];
                    if ((oPeilut.iMakatType == enMakatType.mKavShirut.GetHashCode() && oPeilut.iMisparKnisa == 0) || oPeilut.iMakatType == enMakatType.mNamak.GetHashCode() || (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && (oPeilut.iElementLeShatGmar > 0 || oPeilut.iElementLeShatGmar == -1)))
                    {

                        dShatGmar = oPeilut.dFullShatYetzia.AddMinutes(GetMeshechPeilut(iIndexSidur, oPeilut, curSidur, inputData, ref bUsedMazanTichnunInSidur) + iMeshechPeilut);
                        break;
                    }
                    if ((oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && oPeilut.iErechElement == 1 && string.IsNullOrEmpty(oPeilut.sLoNitzbarLishatGmar)) || (oPeilut.iMakatType == enMakatType.mKavShirut.GetHashCode() && oPeilut.iMisparKnisa > 0) || oPeilut.iMakatType == enMakatType.mEmpty.GetHashCode())
                    {
                        iMeshechPeilut += GetMeshechPeilut(iIndexSidur, oPeilut, curSidur, inputData, ref bUsedMazanTichnunInSidur);
                    }
                }
                return dShatGmar;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private  PeilutDM GetLastPeilutMashmautit(SidurDM curSidur )
        {
            return  curSidur.htPeilut.Values.Cast<PeilutDM>().ToList().LastOrDefault(Peilut => (Peilut.iMakatType == enMakatType.mKavShirut.GetHashCode() || Peilut.iMakatType == enMakatType.mNamak.GetHashCode() || (Peilut.iMakatType == enMakatType.mElement.GetHashCode() && (Peilut.iElementLeShatGmar > 0 || Peilut.iElementLeShatGmar == -1))));
        }


        private int GetMeshechPeilut(int iIndexSidur, PeilutDM oPeilut, SidurDM curSidur, ShinuyInputData inputData,  ref bool bUsedMazanTichnunInSidur)
        {
            int iMeshech = 0;
            DataRow[] drSugSidur;
            bool bSidurNahagutNext;

            try{
                var sidurManager = _container.Resolve<ISidurManager>();
                drSugSidur = sidurManager.GetOneSugSidurMeafyen(curSidur.iSugSidurRagil, inputData.CardDate);
                bool bSidurNahagut = sidurManager.IsSidurNahagut(drSugSidur, curSidur);
                if (inputData.htEmployeeDetails.Count > iIndexSidur + 1)
                {
                    drSugSidur = sidurManager.GetOneSugSidurMeafyen(((SidurDM)inputData.htEmployeeDetails[iIndexSidur + 1]).iSugSidurRagil, inputData.CardDate);
                    bSidurNahagutNext = sidurManager.IsSidurNahagut(drSugSidur, ((SidurDM)inputData.htEmployeeDetails[iIndexSidur + 1]));
                }
                else bSidurNahagutNext = false;

                //אם הערך בשדה 0<Dakot_bafoal אז יש לקחת את הערך משדה זה  
                if (oPeilut.iDakotBafoal > 0 || (oPeilut.iMakatType == enMakatType.mKavShirut.GetHashCode() && oPeilut.iMisparKnisa > 0))
                {
                    iMeshech = oPeilut.iDakotBafoal;
                }
                else
                {
                    //משך פעילות מסוג אלמנט  - הערך בפוזיציות 4-6 של המק"ט. 
                    if (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                    {
                        iMeshech = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));
                    }
                    else
                    { //סידור שהוא יחיד או אחרון ביום
                        //סידור אחרון צריך להיגמר לפי הגדרה לתכנון.

                        if (inputData.htEmployeeDetails.Values.Count == 1 || (inputData.htEmployeeDetails.Count - 1) == iIndexSidur || (bSidurNahagut && !bSidurNahagutNext))
                        {
                            //אלמנט ריקה  (מאפיין 23): מז"ן תשלום (גמר) - הערך שהוקלד במק"ט. מז"ן תכנון  - מז"ן תשלום * פרמטר 43.
                            if (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && !string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                                iMeshech = int.Parse(Math.Round(oPeilut.iMazanTashlum * inputData.oParam.fFactorNesiotRekot).ToString());
                            else
                                iMeshech = oPeilut.iMazanTichnun;
                        }
                        else if (bSidurNahagut && bSidurNahagutNext)
                        {
                            //סידור שאינו יחיד או אחרון ביום צריך להיגמר לפי זמן לגמר או לתכנון, בהתאם למקרה:
                            iMeshech = GetMeshech(iIndexSidur, oPeilut, curSidur, inputData, ref bUsedMazanTichnunInSidur);
                        }
                    }
                }
                return iMeshech;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int  GetMeshech(int iIndexSidur, PeilutDM oPeilut, SidurDM curSidur, ShinuyInputData inputData, ref bool bUsedMazanTichnunInSidur)
        {
            int iMeshech = 0;
            SidurDM oNextSidur = (SidurDM)inputData.htEmployeeDetails[iIndexSidur + 1];
            try{
                //1.	אם יש פער של עד 60 דקות משעת התחלת הסידור הבא - יש לחשב לפי הגדרה גמר (תשלום) 
                if (oNextSidur.dFullShatHatchala.Subtract(curSidur.dFullShatGmar).TotalMinutes <= 60)
                {
                    //אלמנט ריקה  (מאפיין 23): מז"ן תשלום (גמר) - הערך שהוקלד במק"ט. מז"ן תכנון  - מז"ן תשלום * פרמטר 43.
                    if (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && !string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                        iMeshech = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));
                    else
                        iMeshech = oPeilut.iMazanTashlum;
                }
                else
                {
                    if (!inputData.bUsedMazanTichnun)
                    {
                        //אלמנט ריקה  (מאפיין 23): מז"ן תשלום (גמר) - הערך שהוקלד במק"ט. מז"ן תכנון  - מז"ן תשלום * פרמטר 43.
                        if (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && !string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                            iMeshech = int.Parse(Math.Round(oPeilut.iMazanTashlum * inputData.oParam.fFactorNesiotRekot).ToString());
                        else
                            //2.	אם יש פער גדול מ- 60 דקות משעת התחלה של הסידור הבא וזו הפעם הראשונה שסידור שאינו יחיד/אחרון ביום צריך להיגמר לפי הגדרה לתכנון   - יש לחשב לפי זמן לתכנון. 
                            iMeshech = oPeilut.iMazanTichnun;
                        bUsedMazanTichnunInSidur = true;

                    }
                    else
                    {
                        //אלמנט ריקה  (מאפיין 23): מז"ן תשלום (גמר) - הערך שהוקלד במק"ט. מז"ן תכנון  - מז"ן תשלום * פרמטר 43.
                        if (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && !string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                            iMeshech = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));
                        else
                            //3.	אם יש פער גדול מ- 60 דקות מהסידור הבא וזו אינה הפעם הראשונה שסידור שאינו יחיד/אחרון ביום צריך להיגמר לפי זמן לתכנון - יש לחשב לפי הגדרה לגמר (תשלום)
                            iMeshech = oPeilut.iMazanTashlum;
                    }

                }
                return iMeshech;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}