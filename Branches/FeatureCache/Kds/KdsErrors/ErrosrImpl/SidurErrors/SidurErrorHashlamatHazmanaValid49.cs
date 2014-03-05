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
    public class SidurErrorHashlamatHazmanaValid49 : SidurErrorBase
    {
        public SidurErrorHashlamatHazmanaValid49(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            int iZmanMinimum = 0;
            float fSidurTime = float.Parse((!string.IsNullOrEmpty(input.curSidur.sShatGmar) && !string.IsNullOrEmpty(input.curSidur.sShatHatchala) ? (input.curSidur.dFullShatGmar - input.curSidur.dFullShatHatchala).TotalMinutes : 0).ToString()); //clDefinitions.GetSidurTimeInMinuts(oSidur);

            //לסידורים להם אסור לדווח השלמה - יטופל דרך טבלת סידורים מיוחדים. ניתן לדווח השלמה אם לסידור מיוחד קיים מאפיין  40 (זכאי להשלמה עבור הסידור) בטבלת מאפיינים סידורים מיוחדים. עבור סידור שאינו מיוחד תמיד זכאי להשלמה כל עוד זמן הסידור קטן nמהערכים בפרמטרים 101-103 (זמן מינימום לסידור חול להשלמה, זמן מינימום לסידור שישי/ע.ח להשלמה, זמן מינימום לסידור שבתון להשלמה).
            if (!string.IsNullOrEmpty(input.curSidur.sHashlama))
            {
                if ((input.curSidur.bSidurMyuhad) && (input.curSidur.iMisparSidurMyuhad > 0))
                {
                    if (input.curSidur.sHashlamaKod != "1")
                    {
                        if ((input.curSidur.bHashlamaExists) && (!String.IsNullOrEmpty(input.curSidur.sHashlama)))
                        {
                            if (int.Parse(input.curSidur.sHashlama) > 0)
                            {
                                AddNewError(input);
                                return false;
                            }
                        }
                    }
                }
                else //סידור רגיל
                {
                    if ((!input.curSidur.bSidurMyuhad) && (int.Parse(input.curSidur.sHashlama) > 0))
                    {
                        if (DateHelper.CheckShaaton(input.iSugYom, input.CardDate, input.SugeyYamimMeyuchadim))
                        {
                            iZmanMinimum = input.oParameters.iHashlamaShabat;
                        }
                        else
                        {
                            if ((input.curSidur.sErevShishiChag == "1") || (input.curSidur.sSidurDay == enDay.Shishi.GetHashCode().ToString()))
                            {
                                iZmanMinimum = input.oParameters.iHashlamaShisi;
                            }
                            else
                            {
                                iZmanMinimum = input.oParameters.iHashlamaYomRagil;
                            }
                        }
                        if (fSidurTime > iZmanMinimum)
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
            get { return ErrorTypes.errHahlamatHazmanaNotValid; }
        }
    }
}