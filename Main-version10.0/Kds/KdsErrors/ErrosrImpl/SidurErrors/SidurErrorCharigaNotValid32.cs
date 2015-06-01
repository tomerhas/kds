using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorCharigaNotValid32 : SidurErrorBase
    {
        public SidurErrorCharigaNotValid32(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            bool bError = false;
            string sLookUp = "";
            //אם שדה חריגה תקין  - לבדוק תקינות לפי ערכים בטבלה CTB_DIVUCH_HARIGA_MESHAOT.    וסידור אינו זכאי לחריגה (סידור זכאי חריגה אם יש לו מאפיין 35 (זכאי לחריגה) במאפייני סידורים מיוחדים                                      ושעת גמר קטנה מ- 28
            if (string.IsNullOrEmpty(input.curSidur.sChariga))
            {
                bError = true;
            }
            else
            {
                sLookUp = GetLookUpKods("ctb_divuch_hariga_meshaot", input);
                //אם ערך חריגה לא נמצא בטבלה
                if (sLookUp.IndexOf(input.curSidur.sChariga) == -1)
                {
                    bError = true;
                }
            }
            if (bError)
            {
                AddNewError(input);
                return false;
            }
            
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errCharigaValueNotValid; }
        }
    }
}