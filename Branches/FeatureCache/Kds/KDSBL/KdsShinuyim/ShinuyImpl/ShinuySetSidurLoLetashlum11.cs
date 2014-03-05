using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using KDSCommon.DataModels.Shinuyim;
using KDSCommon.Enums;
using KDSCommon.Helpers;
using KDSCommon.Interfaces;
using KDSCommon.Interfaces.Managers;
using KdsShinuyim.Enums;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace KdsShinuyim.ShinuyImpl
{
    class ShinuySetSidurLoLetashlum11 : ShinuyBase
    {

        public ShinuySetSidurLoLetashlum11(IUnityContainer container)
            : base(container)
        {

        }

        public override ShinuyTypes ShinuyType { get { return ShinuyTypes.ShinuySetSidurLoLetashlum11; } }


        public override void ExecShinuy(ShinuyInputData inputData)
        {
            for (int i = 0; i < inputData.htEmployeeDetails.Count; i++)
            {
                SidurDM curSidur = (SidurDM)inputData.htEmployeeDetails[i];
                if (!CheckIdkunRashemet("LO_LETASHLUM", curSidur.iMisparSidur, curSidur.dFullShatHatchala, inputData))
                {
                    SidurLoLetashlum11(curSidur,i, inputData);
                }
            }
        }


        private void SidurLoLetashlum11(SidurDM curSidur, int iSidurIndex, ShinuyInputData inputData)
        {
            bool bSign = false;
            int iKodSibaLoLetashlum = 0;
            DataRow[] drSugSidur;
            //בדיקה ברמת סידור
            //סימון סידור עבודה "לא לתשלום" אם עונה על תנאים מגבילים
            try
            {
                var oObjSidurimOvdimUpd = GetUpdSidurObject(curSidur, inputData);
                drSugSidur = _container.Resolve<ISidurManager>().GetOneSugSidurMeafyen(curSidur.iSugSidurRagil, inputData.CardDate);
                
                if (!(curSidur.bSidurMyuhad && curSidur.iSidurLoNibdakSofShavua == 1))
                {
                    bSign = _container.Resolve<IOvedManager>().IsOvedMatzavExists("6", inputData.dtMatzavOved);
                    if (!bSign)
                    {
                        bSign = Condition1Saif11(curSidur, inputData);
                        if (!bSign)
                        {
                            //תנאי 2
                            bSign = Condition2Saif11(curSidur, inputData.oMeafyeneyOved);
                            if (!bSign)
                            {
                                //תנאי 3
                                bSign = Condition3Saif11(curSidur);
                                if (!bSign)
                                {
                                    //תנאי 4
                                    bSign = Condition4Saif11(curSidur, inputData);
                                    if (!bSign)
                                    {
                                        //5 תנאי
                                        bSign = Condition5Saif11(curSidur, inputData.oMeafyeneyOved);
                                        if (!bSign)
                                        {
                                            //תנאי 6
                                            bSign = Condition6Saif11(curSidur, inputData);
                                            if (!bSign)
                                            {
                                                bSign = ConditionSidurHeadrut(curSidur,  inputData);
                                                if (!bSign)
                                                {
                                                    bSign = Condition7Saif11(drSugSidur, curSidur);
                                                    if (!bSign)
                                                    {
                                                        //תנאי 8
                                                        bSign = Condition8Saif11(drSugSidur, curSidur, inputData);
                                                        if (!bSign)
                                                        {
                                                            //תנאי 9
                                                            bSign = Condition9Saif11(drSugSidur, curSidur, inputData);
                                                            if (bSign)
                                                            {
                                                                iKodSibaLoLetashlum = 10;
                                                            }
                                                        }
                                                        else
                                                        {
                                                            iKodSibaLoLetashlum = 17;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        iKodSibaLoLetashlum = 15;
                                                    }
                                                }
                                                else
                                                {
                                                    iKodSibaLoLetashlum = 22;
                                                }

                                            }
                                            else
                                            {
                                                iKodSibaLoLetashlum = 13;
                                            }
                                        }
                                        else
                                        {
                                            iKodSibaLoLetashlum = 5;
                                        }
                                    }
                                    else
                                    {
                                        iKodSibaLoLetashlum = 4;
                                    }
                                }
                                else
                                {
                                    iKodSibaLoLetashlum = 3;
                                }
                            }
                            else
                            {
                                iKodSibaLoLetashlum = 14;
                            }

                        }
                        else
                        {
                            iKodSibaLoLetashlum = 2;
                        }
                    }
                    else
                    {
                        iKodSibaLoLetashlum = 21;
                    }

                  
                    if (bSign)
                    {

                        oObjSidurimOvdimUpd.LO_LETASHLUM = 1;
                        oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM = iKodSibaLoLetashlum;
                        oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                        curSidur.iLoLetashlum = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                //clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 11, curSidur.iMisparIshi, curSidur.dSidurDate, curSidur.iMisparSidur, curSidur.dFullShatHatchala, null, null, "SidurLoLetashlum11: " + ex.Message, null);
                inputData.IsSuccsess = false;
            }
        }
        private bool Condition1Saif11(SidurDM curSidur, ShinuyInputData inputData)
        {
            //תנאי ראשון לסעיף 11
            return ((curSidur.bSidurMyuhad) && (curSidur.sSugAvoda == enSugAvoda.VaadOvdim.GetHashCode().ToString()) && (DateHelper.CheckShaaton(inputData.iSugYom, curSidur.dSidurDate, inputData.SugeyYamimMeyuchadim)));
        }

        private bool Condition2Saif11(SidurDM curSidur, MeafyenimDM oMeafyeneyOved)
        {
            //תנאי שני לסעיף 11
            return ((curSidur.iMisparSidur == 99822) && (oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek2.GetHashCode()) && ((curSidur.sSidurDay == enDay.Shishi.GetHashCode().ToString())));
        }

        private bool Condition3Saif11(SidurDM curSidur)
        {
            //תנאי שלישי לסעיף 11
            bool bElementLeyedia = false;
            PeilutDM oPeilut;
            try
            {
                if (curSidur.htPeilut.Count == 1)
                {
                    oPeilut = (PeilutDM)curSidur.htPeilut[0];
                    if ((oPeilut.iMakatType == enMakatType.mElement.GetHashCode()) && (oPeilut.iElementLeYedia == 2))
                    {
                        bElementLeyedia = true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bElementLeyedia;

        }

        private bool Condition4Saif11(SidurDM curSidur, ShinuyInputData inputData)
        {
            //תנאי רביעי לסעיף 11
            bool bSign = false;
            try
            {
                // if ((curSidur.bSidurMyuhad) && (curSidur.sSectorAvoda == enSectorAvoda.Tafkid.GetHashCode().ToString()) && (clDefinitions.CheckShaaton(_dtSugeyYamimMeyuchadim, iSugYom, curSidur.dSidurDate)))
                if (DateHelper.CheckShaaton(inputData.iSugYom, curSidur.dSidurDate, inputData.SugeyYamimMeyuchadim) &&
                    (curSidur.bSidurMyuhad && curSidur.sShaonNochachut == "1" && curSidur.sChariga == "0"))
                    //וגם לעובד אין מאפיין 7 ו- 8 ברמה האישית. 
                    if (!inputData.oMeafyeneyOved.IsMeafyenExist(7) && !inputData.oMeafyeneyOved.IsMeafyenExist(8))
                    {
                        bSign = true;
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bSign;
        }

        private bool Condition5Saif11(SidurDM curSidur, MeafyenimDM oMeafyeneyOved)
        {
            //תנאי חמישי לסעיף 11
            bool bSign = false;
            try
            {

                if ((curSidur.sSidurDay == enDay.Shishi.GetHashCode().ToString()) &&
                    (curSidur.bSidurMyuhad && curSidur.sShaonNochachut == "1" && curSidur.sChariga == "0"))
                    //וגם לעובד אין מאפיין 5 ו- 6 ברמה האישית. 
                    if (!oMeafyeneyOved.IsMeafyenExist(5) && !oMeafyeneyOved.IsMeafyenExist(6))
                    {
                        bSign = true;
                    }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bSign;
        }

        private bool Condition6Saif11(SidurDM curSidur, ShinuyInputData inputData)
        {
            //תנאי שישי לסעיף 11
            bool bSign = false;
            bool bIsurShaotNosafot;
            try
            {
                if (!String.IsNullOrEmpty(inputData.OvedDetails.sMutamut))
                {
                    var cache = ServiceLocator.Current.GetInstance<IKDSCacheManager>();
                    GetMutamut(cache.GetCacheItem<DataTable>(CachedItems.Mutamut), int.Parse(inputData.OvedDetails.sMutamut), out bIsurShaotNosafot);

                    if (bIsurShaotNosafot)
                    {
                        //עובד 6 ימים 
                        if ((inputData.oMeafyeneyOved.IsMeafyenExist(56)) && ((inputData.oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved6DaysInWeek1.GetHashCode()) || (inputData.oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved6DaysInWeek2.GetHashCode())))
                        {
                            if (DateHelper.CheckShaaton(inputData.iSugYom, curSidur.dSidurDate, inputData.SugeyYamimMeyuchadim))
                            {
                                bSign = true;
                            }
                        }
                        //עובד 5 ימים 
                        if ((inputData.oMeafyeneyOved.IsMeafyenExist(56)) && ((inputData.oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek1.GetHashCode()) || (inputData.oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())))
                        {
                            if (DateHelper.CheckShaaton(inputData.iSugYom, curSidur.dSidurDate, inputData.SugeyYamimMeyuchadim) || (curSidur.sSidurDay == enDay.Shishi.GetHashCode().ToString()))
                            {
                                bSign = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bSign;
        }

        private bool ConditionSidurHeadrut(SidurDM curSidur, ShinuyInputData inputData)
        {
            bool bLoLetashlumAutomati = false;
            if (inputData.iSugYom > 19 || (inputData.iSugYom == 10 && (inputData.oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek1.GetHashCode() || inputData.oMeafyeneyOved.GetMeafyen(56).IntValue == enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())))
            {
                if (curSidur.bSidurMyuhad)
                {//סידור מיוחד
                    if (!string.IsNullOrEmpty(curSidur.sHeadrutTypeKod))
                    {
                        if ((curSidur.sHeadrutTypeKod == enMeafyenSidur53.enMachala.GetHashCode().ToString()) ||
                            (curSidur.sHeadrutTypeKod == enMeafyenSidur53.enMilueim.GetHashCode().ToString()) ||
                            (curSidur.sHeadrutTypeKod == enMeafyenSidur53.enTeuna.GetHashCode().ToString()) ||
                            (curSidur.sHeadrutTypeKod == enMeafyenSidur53.enEvel.GetHashCode().ToString()))
                        {
                            bLoLetashlumAutomati = true;
                        }
                    }
                }

            }
            //תנאי שביעי לסעיף 11

            return bLoLetashlumAutomati;
        }
       
        private bool Condition7Saif11(DataRow[] drSugSidur, SidurDM curSidur)
        {
            bool bLoLetashlumAutomati;

            //תנאי שביעי לסעיף 11
            if (curSidur.bSidurMyuhad)
            {
                bLoLetashlumAutomati = curSidur.bLoLetashlumAutomatiExists;
            }
            else
            {
                if (drSugSidur.Length > 0)
                {
                    //TODO: מחכה לתשובה ממירי
                    bLoLetashlumAutomati = (drSugSidur[0]["lo_letashlum_automati"].ToString() == enMeafyen79.LoLetashlumAutomat.GetHashCode().ToString());
                }
                else
                {
                    bLoLetashlumAutomati = false;
                }
            }
            return bLoLetashlumAutomati;
        }

        private bool Condition8Saif11(DataRow[] drSugSidur, SidurDM curSidur, ShinuyInputData inputData)
        {
            bool bLoLetashlumAutomati = false;

            // וגם מדובר בסידור מיוחד עם עם מאפיין לסידורים מיוחדים קוד = 54 עם ערך 1.
            if (curSidur.bSidurMyuhad && curSidur.sShaonNochachut == "1" && curSidur.sChariga == "0") // && oOvedYomAvodaDetails.iIsuk.ToString().Substring(0,1) == "5")
            {
                //וגם לעובד אין מאפיין 3 ו- 4 ברמה האישית. 
                if (!inputData.oMeafyeneyOved.IsMeafyenExist(3) && !inputData.oMeafyeneyOved.IsMeafyenExist(4))
                {
                    bLoLetashlumAutomati = true;
                }
            }

            return bLoLetashlumAutomati;
        }

        private bool Condition9Saif11(DataRow[] drSugSidur, SidurDM curSidur, ShinuyInputData inputData)
        {
            bool bLoLetashlumAutomati = true;
            DateTime dShatHatchalaLetashlum, dShatGmarLetashlum;
            dShatHatchalaLetashlum = curSidur.dFullShatHatchala;
            dShatGmarLetashlum = curSidur.dFullShatGmar;
            bool bFromMeafyenHatchala, bFromMeafyenGmar;

            //??
            GetOvedShatHatchalaGmar(curSidur.dFullShatGmar, inputData.oMeafyeneyOved, curSidur,inputData, ref dShatHatchalaLetashlum, ref dShatGmarLetashlum, out bFromMeafyenHatchala, out bFromMeafyenGmar);
            bLoLetashlumAutomati = CheckLoLetashlumMeafyenim(drSugSidur, curSidur, inputData, dShatHatchalaLetashlum, dShatGmarLetashlum, bFromMeafyenHatchala, bFromMeafyenGmar);

            return bLoLetashlumAutomati;
        }

        private bool CheckLoLetashlumMeafyenim(DataRow[] drSugSidur, SidurDM oSidur, ShinuyInputData inputData,DateTime dShatHatchalaLetashlum, DateTime dShatGmarLetashlum, bool bFromMeafyenHatchala, bool bFromMeafyenGmar)
        {
            bool bLoLetashlumAutomati = false;
            string sMeafyenKizuz = "";
            DateTime shaa;
            if (oSidur.bSidurMyuhad)
            {
                sMeafyenKizuz = oSidur.sKizuzAlPiHatchalaGmar;
            }
            else
            {
                if (drSugSidur.Length > 0)
                {
                    sMeafyenKizuz = drSugSidur[0]["kizuz_al_pi_hatchala_gmar"].ToString();
                }
            }

            if (!string.IsNullOrEmpty(sMeafyenKizuz) && oSidur.iLoLetashlum == 0)
            {
                if (sMeafyenKizuz == "1")
                {
                    if (bFromMeafyenHatchala && bFromMeafyenGmar)
                    {
                        if (((oSidur.dFullShatGmar != DateTime.MinValue && (oSidur.dFullShatGmar <= dShatHatchalaLetashlum)) || (oSidur.dFullShatHatchala != DateTime.MinValue && oSidur.dFullShatHatchala >= dShatGmarLetashlum)) && oSidur.sChariga == "0")
                        {
                            shaa = DateTime.Parse(oSidur.dFullShatHatchala.ToShortDateString() + " 18:00:00");
                            if (!inputData.oMeafyeneyOved.IsMeafyenExist(42) && inputData.oMeafyeneyOved.IsMeafyenExist(23) && inputData.oMeafyeneyOved.IsMeafyenExist(24))
                            {
                                if ((oSidur.dFullShatHatchala.Hour >= 11 && oSidur.dFullShatHatchala.Hour <= 17 && oSidur.dFullShatGmar > shaa)
                                    && (oSidur.sShabaton != "1" && (inputData.iSugYom >= enSugYom.Chol.GetHashCode() && inputData.iSugYom < enSugYom.Shishi.GetHashCode())))
                                    bLoLetashlumAutomati = false;
                                else bLoLetashlumAutomati = true;
                            }
                            else
                                bLoLetashlumAutomati = true;
                        }
                    }
                    else if ((bFromMeafyenHatchala && !bFromMeafyenGmar) || (!bFromMeafyenHatchala && bFromMeafyenGmar))
                    {
                        bLoLetashlumAutomati = true;
                    }
                }
            }

            return bLoLetashlumAutomati;
        }

    }
}