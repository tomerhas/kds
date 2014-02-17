using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using KDSCommon.DataModels;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.Interfaces.DAL;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.DayErrors
{
    public class DayErrorIsNesiaMeshtanaDefine150 : DayErrorBase
    {
        public DayErrorIsNesiaMeshtanaDefine150(IUnityContainer container)
            : base(container)
        {
        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            int iZmanNesiaHaloch, iZmanNesiaChazor;
            iZmanNesiaHaloch = -1;
            iZmanNesiaChazor = -1;

            if (input.oMeafyeneyOved.IsMeafyenExist(61) && int.Parse(input.oMeafyeneyOved.GetMeafyen(61).Value) > 0) // אם קיים מאפיין 61, לעובד מגיע זמן נסיעה משתנה
            {
                if (input.OvedDetails.bMercazEruaExists)
                {
                    if (IsOvedZakaiLZmanNesiaLaAvoda(input) || IsOvedZakaiLZmanNesiaLeMeAvoda(input))
                    {
                        iZmanNesiaHaloch = GetZmanNesiaMeshtana(0, 1, input);
                    }

                    if (IsOvedZakaiLZmanNesiaMeAvoda(input) || IsOvedZakaiLZmanNesiaLeMeAvoda(input))
                    {
                        iZmanNesiaChazor = GetZmanNesiaMeshtana(input.htEmployeeDetails.Count - 1, 1, input);
                    }

                    if (iZmanNesiaChazor == -1 || iZmanNesiaHaloch == -1)
                    {
                        return false;

                    }

                }
            }
            return true;
       }
        

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errNesiaMeshtanaNotDefine; }
        }

        private bool IsOvedZakaiLZmanNesiaLaAvoda(ErrorInputData input)
        {
            //לעובד מאפיין 51/61 (מאפיין זמן נסיעות) והעובד זכאי רק לזמן נסיעות לעבודה (ערך 1 בספרה הראשונה של מאפיין זמן נסיעות            
            return ((input.oMeafyeneyOved.IsMeafyenExist(61) && input.oMeafyeneyOved.GetMeafyen(61).Value.Substring(0, 1) == "1")
                   ||
                   (input.oMeafyeneyOved.IsMeafyenExist(51) && input.oMeafyeneyOved.GetMeafyen(51).Value.Substring(0, 1) == "1"));
        }

        private bool IsOvedZakaiLZmanNesiaMeAvoda(ErrorInputData input)
        {
            //לעובד מאפיין 51/61 (מאפיין זמן נסיעות) והעובד זכאי רק לזמן נסיעות מהעבודה (ערך 2 בספרה הראשונה של מאפיין זמן נסיעות            
            return ((input.oMeafyeneyOved.IsMeafyenExist(61) && input.oMeafyeneyOved.GetMeafyen(61).Value.Substring(0, 1) == "2")
                   ||
                   (input.oMeafyeneyOved.IsMeafyenExist(51) && input.oMeafyeneyOved.GetMeafyen(51).Value.Substring(0, 1) == "2"));
        }

        private bool IsOvedZakaiLZmanNesiaLeMeAvoda(ErrorInputData input)
        {
            //לעובד מאפיין 51/61 (מאפיין זמן נסיעות) והעובד זכאי רק לזמן נסיעות מהעבודה (ערך 3 בספרה הראשונה של מאפיין זמן נסיעות            
            return ((input.oMeafyeneyOved.IsMeafyenExist(61) && input.oMeafyeneyOved.GetMeafyen(61).Value.Substring(0, 1) == "3")
                   ||
                   (input.oMeafyeneyOved.IsMeafyenExist(51) && input.oMeafyeneyOved.GetMeafyen(51).Value.Substring(0, 1) == "3"));
        }

        private int GetZmanNesiaMeshtana(int iSidurIndex, int iType, ErrorInputData input)
        {

            int iZmanNesia = 0;
            int iMerkazErua = 0;
            int iMikumYaad = 0;
            // clUtils oUtils = new clUtils();

            //נשלוף את הסידור 
            SidurDM oSidur;
            try
            {
                if (input.htEmployeeDetails.Count > 0)
                {
                    oSidur = (SidurDM)input.htEmployeeDetails[iSidurIndex];

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

                    iMerkazErua = (String.IsNullOrEmpty(input.OvedDetails.sMercazErua) ? 0 : int.Parse(input.OvedDetails.sMercazErua));
                    if ((iMerkazErua > 0) && (iMikumYaad > 0))
                    {
                        var ovedManager = ServiceLocator.Current.GetInstance<IOvedDAL>();
                        iZmanNesia = ovedManager.GetZmanNesia(iMerkazErua, iMikumYaad, input.CardDate);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iZmanNesia;
        }

    }
}
