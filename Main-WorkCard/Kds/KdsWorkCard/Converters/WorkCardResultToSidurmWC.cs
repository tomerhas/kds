using KDSCommon.DataModels;
using KDSCommon.DataModels.Security;
using KDSCommon.DataModels.WorkCard;
using KDSCommon.Enums;
using KDSCommon.Helpers;
using KdsLibrary;
using KdsLibrary.Security;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace KdsWorkCard.Converters
{
    /// <summary>
    /// The converter class recieves objects from the original system that may or may not be possible to serialize into json
    /// This class converts the data required for a specific call into a serializable object that can be used by the UI (Angular)
    /// </summary>
    public class WorkCardResultToSidurmWC
    {
        /// <summary>
        /// Recieves a workcard result and extracts from it the Sidurim serializable object (SidurimWC)
        /// </summary>
        /// <param name="wcResult"></param>
        /// <param name="userId"></param>
        /// <param name="profiles"></param>
        /// <returns></returns>
        public SidurimWC Convert(WorkCardResult wcResult,int userId,List<UserProfile> profiles)
        {
            SidurimWC sidurimResult = new SidurimWC();
            for (int index = 0; index < wcResult.htFullEmployeeDetails.Count; index++)
            {
                var sidur = BuildSidurAndPeiluyot(wcResult, userId,index,profiles);
                if (sidur != null)
                {
                    sidurimResult.SidurimList.Add(sidur);
                }
            }
            return sidurimResult;
        }


        private SidurWC BuildSidurAndPeiluyot(WorkCardResult wcResult, int userId, int sidurIndex, List<UserProfile> profiles)
        {
            SidurDM sidurDm = wcResult.htFullEmployeeDetails[sidurIndex] as SidurDM;
            DataRow drSugSidur = GetOneSugSidurMeafyen(sidurDm.iSugSidurRagil, sidurDm.dSidurDate, wcResult.dtSugSidur);
            sidurDm.bSidurNahagut = IsSidurNahagut(sidurDm, drSugSidur);
            bool isEnableSidur = IsEnableSidur(wcResult, sidurDm, drSugSidur, userId, profiles);
            bool isSidurNihul = IsSidurNihul(sidurDm, drSugSidur);
            bool isAtLeastOneSidurInShabat = IsAtLeastOneSidurInShabat(wcResult, sidurDm, sidurDm.bSidurNahagut, isSidurNihul);
            bool isSidurActive = IsSidurActive(sidurDm);
            bool RekaUpSign;
            AttributeField oVal;

            if (sidurDm != null)
            {
                SidurWC sidur = new SidurWC();
                sidur.CollapseSrc.Value = "../../../../Images/openArrow.png";
                sidur.MisparSidur.Value = sidurDm.iMisparSidur;
                sidur.FullShatHatchala.Value = sidurDm.dFullShatHatchala;
                sidur.FullShatGmar.Value = sidurDm.dFullShatGmar;
                sidur.SibatHachtamaIn.Value = sidurDm.iKodSibaLedivuchYadaniIn;
                sidur.SibatHachtamaOut.Value = sidurDm.iKodSibaLedivuchYadaniOut;
                sidur.FullShatHatchalaLetashlum.Value = sidurDm.dFullShatHatchalaLetashlum;
                sidur.FullShatGmarLetashlum.Value = sidurDm.dFullShatGmarLetashlum;
                sidur.Hariga.Value = int.Parse(sidurDm.sChariga);
                sidur.Pizul.Value = int.Parse(sidurDm.sPitzulHafsaka);
                sidur.Hashlama.Value = sidurDm.sHashlama;
                sidur.Hamara.Value = sidurDm.sHashlamaKod;
                sidur.OutMichsa.Value = sidurDm.sOutMichsa;
                sidur.LoLetashlum.Value = sidurDm.iLoLetashlum;

                var iBitulOHosafa = sidurDm.iBitulOHosafa;
                sidur.SidurActive.Value = ((iBitulOHosafa != clGeneral.enBitulOHosafa.BitulAutomat.GetHashCode()) && (iBitulOHosafa != clGeneral.enBitulOHosafa.BitulByUser.GetHashCode()));

                RekaUpSign = false;
                for (int peilutIndex = 0; peilutIndex < sidurDm.htPeilut.Count; peilutIndex++)
                {
                    PeilutDM peilutDM = sidurDm.htPeilut[peilutIndex] as PeilutDM;
                    PeilutWC peilut = new PeilutWC();

                    peilut.KisuyTor.Value = peilutDM.iKisuyTor;
                    peilut.ShatYezia.Value = peilutDM.dFullShatYetzia;
                    peilut.Teur.Value = peilutDM.sMakatDescription;
                    peilut.Kav.Value = peilutDM.sShilut;
                    peilut.Sug.Value = peilutDM.sSugShirutName;
                    
                    peilut.Makat.Value = peilutDM.lMakatNesia;
                    peilut.Makat.Attributes.Add(new AttributeField("OldVal", peilutDM.lMakatNesia.ToString()));
                    

                    peilut.DakotTichnun.Value = peilutDM.iMazanTichnun;
                    peilut.DakotBafoal.Value = peilutDM.iDakotBafoal == -1 ? "" : peilutDM.iDakotBafoal.ToString();

                    peilut.NumOto.Value =peilutDM.lOtoNo;
                    peilut.NumOto.Attributes.Add(new AttributeField("OldVal", peilut.NumOto.Value.ToString()));
                    peilut.NumOto.Attributes.Add(new AttributeField("MustOtoNum", (IsPeilutMustOtoNumber(peilut.Makat.Value, peilutDM.bBusNumberMustExists) ? "1" : "0") )); 


                    peilut.Nezer.Value = peilutDM.bImutNetzer ? "כן" : "לא";

                    peilut.RekaDown.Value = AllowedInsertReka(peilutDM);
                    peilut.MisparKnisa.Value = peilutDM.iMisparKnisa;
                    peilut.MakatType.Value = peilutDM.iMakatType;
                   // peilut.CancelPeilut.Value = 0;
                    if (peilutIndex < 2)
                    {
                        if(peilutIndex == 0 && !IsElementHachanatMechona((enMakatType)peilutDM.iMakatType,peilutDM.lMakatNesia))
                        {
                             peilut.RekaUp.Value = AllowedInsertReka(peilutDM);
                             RekaUpSign = true;
                        }
                        else if (peilutIndex == 1 && !RekaUpSign)
                            peilut.RekaUp.Value = AllowedInsertReka(peilutDM);
                        else peilut.RekaUp.Value = false;
                        
                    }
                    else peilut.RekaUp.Value = false;

                    iBitulOHosafa = peilutDM.iBitulOHosafa;
                    peilut.PeilutActive.Value = ((iBitulOHosafa != clGeneral.enBitulOHosafa.BitulAutomat.GetHashCode()) && (iBitulOHosafa != clGeneral.enBitulOHosafa.BitulByUser.GetHashCode()));

                    sidur.PeilutList.Add(peilut);
                        
                }
                return sidur;
            }
            return null;


        }

        protected bool IsPeilutMustOtoNumber(long lMakat, bool bBusMustOtoNumber)
        {
            enMakatType _MakatType;
            _MakatType = ((enMakatType)StaticBL.GetMakatType(lMakat));
            //ויזה,פעילויות שדורשות רכב: קו שירות, נמ"ק, ריקה ואלמנט עם מאפיין 11 
            return ((_MakatType == enMakatType.mVisa) || (_MakatType == enMakatType.mKavShirut) || (_MakatType == enMakatType.mNamak) || (_MakatType == enMakatType.mEmpty) || (_MakatType == enMakatType.mVisut) || ((_MakatType == enMakatType.mElement) && (bBusMustOtoNumber)));

        }
        private bool AllowedInsertReka(PeilutDM peilutDM)
        {
            enMakatType oMakatType= (enMakatType)peilutDM.iMakatType;
            if (((oMakatType == enMakatType.mEmpty)) || (oMakatType == enMakatType.mNamak) || ((oMakatType == enMakatType.mKavShirut) && (peilutDM.iMisparKnisa == 0)))
                return true;
            else
                return false;
        }
     
        protected bool IsElementHachanatMechona(enMakatType _MakatType, long lMakat)
        {
            string sElementPrefix = "";
            bool bFound = false;
            //מחזיר אמת אם אלמנט מסוג הכנת מכונה
            if (_MakatType == enMakatType.mElement)
            {
                sElementPrefix = lMakat.ToString().Substring(0, 3);
                if ((sElementPrefix == enElementHachanatMechona.Element701.GetHashCode().ToString()) || (sElementPrefix == enElementHachanatMechona.Element711.GetHashCode().ToString()) || (sElementPrefix == enElementHachanatMechona.Element712.GetHashCode().ToString()))
                    bFound = true;
            }
            return bFound;
        }

        private bool IsSidurActive(SidurDM sidurDm)
        {
           return ((sidurDm.iBitulOHosafa != clGeneral.enBitulOHosafa.BitulAutomat.GetHashCode()) 
                                && (sidurDm.iBitulOHosafa != clGeneral.enBitulOHosafa.BitulByUser.GetHashCode()));
        }

        private bool IsAtLeastOneSidurInShabat(WorkCardResult wcResult, SidurDM sidurDm,bool isSidurNagut, bool isSidurNihul)
        {
            bool res = false;
            if (isSidurNagut || (isSidurNihul))
            {
                res = true;

                if ((sidurDm.sErevShishiChag.Equals("1")) || (sidurDm.sSidurDay.Equals(enDay.Shishi.GetHashCode().ToString())))
                {
                    if (!res)
                        res = IsSidurInShabat(wcResult,sidurDm);
                }
            }
            return res;
        }

        private bool IsEnableSidur(WorkCardResult wcResult, SidurDM oSidur, DataRow drSugSidur, int userId,List<UserProfile> profiles)
        {
            bool bEnable = true;
            bool bRashaiLedavech = false;
            bool bNewWorkCard = false;

            if (oSidur.bSidurMyuhad) //סידור מיוחד
                bRashaiLedavech = oSidur.bRashaiLedaveachExists;
            else //סידור מפה
                if (drSugSidur!= null)
                {
                    bRashaiLedavech = true;
                    bRashaiLedavech = (drSugSidur["RASHAI_LEDAVEACH"].ToString() == "1");
                }
                else //אינו סידור מיוחד ואינו סידור מפה
                    bRashaiLedavech = false;


            bNewWorkCard = DateHelper.GetDiffDays(oSidur.dSidurDate, DateTime.Now) + 1 <= wcResult.oParam.iValidDays;
            bool isMenahelBankShaot = IsUserInRole(profiles, clGeneral.enProfile.enMenahelBankMeshek);
            //אם הכרטיס הוא ללא התייחסות והמספר האישי של הגורם שנכנס שונה מהמספר האישי של הכרטיס
            //ואנחנו בטווח של 45 (פרמטר 252) יום מתאריך של היום 
            //לא נאפשר עדכון סידור אם לסידור לא קיים מאפיין 99
            if ((wcResult.oOvedYomAvodaDetails.iMeasherOMistayeg == (int)clGeneral.enMeasherOMistayeg.ValueNull) && (!bRashaiLedavech) && (userId != oSidur.iMisparIshi) && (bNewWorkCard))
                bEnable = false;
            else if (isMenahelBankShaot)
            {
                if (oSidur.bSidurMyuhad) //סידור מיוחד
                    bEnable = oSidur.bMenahelBankRashaiLedaveach;
                else
                {
                    if (drSugSidur != null)
                        bEnable = (drSugSidur["ledivuach_menahel_meshek"].ToString() == "1");
                }
            }
            return bEnable;
        }

        private bool IsSidurNahagut(SidurDM oSidur, DataRow drSugSidur)
        {
            bool bSidurNahagut = false;
            //מחזיר אם סידור נהגות
            if (oSidur.bSidurMyuhad)
                bSidurNahagut = (oSidur.sSectorAvoda == enSectorAvoda.Nahagut.GetHashCode().ToString());
            else
            {
                if(drSugSidur!=null)
                    bSidurNahagut = (drSugSidur["sector_avoda"].ToString() == enSectorAvoda.Nahagut.GetHashCode().ToString());
            }
            return bSidurNahagut;
        }

        private DataRow GetOneSugSidurMeafyen(int iSugSidur, DateTime dDate, DataTable dtSugSidur)
        {   //הפונקציה מקבלת קוד סוג סידור ותאריך ומחזירה את מאפייני סוג הסידור
             DataRow[]  dr = dtSugSidur.Select(string.Concat("sug_sidur=", iSugSidur.ToString(), " and Convert('", dDate.ToShortDateString(), "','System.DateTime') >= me_tarich and Convert('", dDate.ToShortDateString(), "', 'System.DateTime') <= ad_tarich"));
            if(dr.Count()>0)
                return dr[0];
            return null;
        }

        private bool IsSidurNihul(SidurDM oSidur, DataRow drSugSidur)
        {
            bool bSidurNihul = false;
            //מחזיר אם סידור ניהול
            if (oSidur.bSidurMyuhad)
                bSidurNihul = (oSidur.sSectorAvoda == enSectorAvoda.Nihul.GetHashCode().ToString());
            else
            {
                if (drSugSidur!=null)
                    bSidurNihul = (drSugSidur["sector_avoda"].ToString() == enSectorAvoda.Nihul.GetHashCode().ToString());
            }

            return bSidurNihul;
        }

        private bool IsSidurInShabat(WorkCardResult wcResult, SidurDM oSidur)
        {
            return oSidur.dFullShatGmar > wcResult.oParam.dKnisatShabat;
        }

        private bool IsUserInRole(List<UserProfile> profiles,clGeneral.enProfile role)
        {
            return profiles.Any(profile => profile.Role == (int)role);
        }
    }
}
