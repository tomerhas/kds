using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KdsLibrary;
using Microsoft.Practices.Unity;
using KDSCommon.Helpers;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorSidurCharigaNotValid33 : SidurErrorBase
    {
        public SidurErrorSidurCharigaNotValid33(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {                      
            string sLookUp;
             bool bError = false;
            DateTime dMeafyenStartDate=DateTime.MinValue;
            DateTime dMeafyenEndDate = DateTime.MinValue;
           bool bCheckChariga = false;
        
               //בדיקה ברמת סידור
               //השגיאה רלוונטית רק עבור עובד שיש לו מאפייני עבודה מתאימים ליום העבודה:
               //יום חול - מאפיינים 3, 4, שישי/ערב חג -  מאפיינים 5, 6 שבת/שבתון -  מאפיינים 7, 8
           if (DateHelper.CheckShaaton(input.iSugYom, input.CardDate, input.SugeyYamimMeyuchadim))
               {
                   if (input.oMeafyeneyOved.IsMeafyenExist(7)  && input.oMeafyeneyOved.IsMeafyenExist(8) )
                   {
                       bCheckChariga = true;
                       //         dMeafyenStartDate = input.oMeafyeneyOved.ConvertMefyenShaotValid(_dCardDate, input.oMeafyeneyOved.sMeafyen7);
                       //         dMeafyenEndDate = input.oMeafyeneyOved.ConvertMefyenShaotValid(_dCardDate, input.oMeafyeneyOved.sMeafyen8);

                   }
               }
               else if ((input.curSidur.sErevShishiChag == "1") || (input.curSidur.sSidurDay == enDay.Shishi.GetHashCode().ToString()))
               {
                   if (input.oMeafyeneyOved.IsMeafyenExist(5) && input.oMeafyeneyOved.IsMeafyenExist(6) )
                   {
                       bCheckChariga = true;
                       //        dMeafyenStartDate = input.oMeafyeneyOved.ConvertMefyenShaotValid(_dCardDate, input.oMeafyeneyOved.sMeafyen5);
                       //        dMeafyenEndDate = input.oMeafyeneyOved.ConvertMefyenShaotValid(_dCardDate, input.oMeafyeneyOved.sMeafyen6);
                   }
               }
               else
               {
                   if (input.oMeafyeneyOved.IsMeafyenExist(3)  && input.oMeafyeneyOved.IsMeafyenExist(4) )
                   {
                       bCheckChariga = true;
                       //        dMeafyenStartDate = input.oMeafyeneyOved.ConvertMefyenShaotValid(_dCardDate, input.oMeafyeneyOved.sMeafyen3);
                       //        dMeafyenEndDate = input.oMeafyeneyOved.ConvertMefyenShaotValid(_dCardDate, input.oMeafyeneyOved.sMeafyen4);
                   }
               }

               if (!(input.curSidur.iLoLetashlum == 1 && (input.curSidur.iKodSibaLoLetashlum == 4 || input.curSidur.iKodSibaLoLetashlum == 5 || input.curSidur.iKodSibaLoLetashlum == 10)))
               {
                   if (bCheckChariga)
                   {
                       dMeafyenStartDate = input.curSidur.dFullShatHatchalaLetashlum;
                       dMeafyenEndDate = input.curSidur.dFullShatGmarLetashlum;
                       if (string.IsNullOrEmpty(input.curSidur.sChariga))
                       {
                           bError = true;
                       }
                       else
                       {
                           sLookUp = GetLookUpKods("ctb_divuch_hariga_meshaot", input);
                           //אם ערך חריגה לא נמצא בטבלה
                           if (sLookUp.IndexOf(input.curSidur.sChariga) != -1)
                           {
                               clGeneral.enCharigaValue oCharigaValue;
                               oCharigaValue = (clGeneral.enCharigaValue)int.Parse((input.curSidur.sChariga));
                               switch (oCharigaValue)
                               {
                                   case clGeneral.enCharigaValue.CharigaKnisa:
                                       //אם שעת כניסה המוגדרת לעובד פחות שעת הכניסה בפועל קטנה מפרמטר 41 המגדיר מינימום לחריגה ומדווח חריגה נעלה שגיאה
                                       if (!string.IsNullOrEmpty(input.curSidur.sShatHatchala))
                                       {

                                           if (input.curSidur.dFullShatHatchala < dMeafyenStartDate)
                                           {
                                               if ((dMeafyenStartDate - input.curSidur.dFullShatHatchala).TotalMinutes < input.oParameters.iZmanChariga)
                                               {
                                                   bError = true;
                                               }
                                           }
                                       }
                                       break;
                                   case clGeneral.enCharigaValue.CharigaYetiza:
                                       if (!string.IsNullOrEmpty(input.curSidur.sShatGmar))
                                       {
                                           if (input.curSidur.dFullShatGmar > dMeafyenEndDate)
                                           {
                                               if ((input.curSidur.dFullShatGmar - dMeafyenEndDate).TotalMinutes < input.oParameters.iZmanChariga)
                                               {
                                                   bError = true;
                                               }
                                           }
                                       }
                                       break;
                                   case clGeneral.enCharigaValue.CharigaKnisaYetiza:
                                       //אם שעת כניסה המוגדרת לעובד פחות שעת הכניסה בפועל קטנה מפרמטר 41 המגדיר מינימום לחריגה ומדווח חריגה נעלה שגיאה
                                       if (!string.IsNullOrEmpty(input.curSidur.sShatHatchala))
                                       {
                                           if (input.curSidur.dFullShatHatchala < dMeafyenStartDate)
                                           {
                                               if ((dMeafyenStartDate - input.curSidur.dFullShatHatchala).TotalMinutes < input.oParameters.iZmanChariga)
                                               {
                                                   bError = true;
                                               }
                                           }
                                       }
                                       if (!string.IsNullOrEmpty(input.curSidur.sShatGmar))
                                       {
                                           if (input.curSidur.dFullShatGmar > dMeafyenEndDate)
                                           {
                                               if ((input.curSidur.dFullShatGmar - dMeafyenEndDate).TotalMinutes < input.oParameters.iZmanChariga)
                                               {
                                                   bError = true;
                                               }
                                           }
                                        }
                                       break;
                               }
                           }
                           if (input.curSidur.bSidurMyuhad && input.curSidur.sZakaiLeChariga=="3")
                               bError=false;

                           if (bError)  // && !CheckApproval("2,211,4,5,511,6,10,1011", input.curSidur.iMisparSidur, input.curSidur.dFullShatHatchala))
                           {
                               AddNewError(input);
                               return false;
                           }
                       }
                   }
               }
               
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errCharigaZmanHachtamatShaonNotValid; }
        }
    }
}