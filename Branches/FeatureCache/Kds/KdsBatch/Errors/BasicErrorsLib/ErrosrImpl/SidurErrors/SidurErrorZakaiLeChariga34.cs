using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.SidurErrors
{
    public class SidurErrorZakaiLeChariga34 : SidurErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            string sLookUp;
            int iShatGmar;
          
            //אם שדה חריגה תקין  - לבדוק תקינות לפי ערכים בטבלה CTB_DIVUCH_HARIGA_MESHAOT.    וסידור אינו זכאי לחריגה (סידור זכאי חריגה אם יש לו מאפיין 35 (זכאי לחריגה) במאפייני סידורים מיוחדים                                      ושעת גמר קטנה מ- 28                
            if (input.curSidur.bSidurMyuhad)
            {//סידורים מיוחדים
                if (!string.IsNullOrEmpty(input.curSidur.sChariga) && Int32.Parse(input.curSidur.sChariga) > 0)
                {
                    if (!(string.IsNullOrEmpty(input.curSidur.sShatGmar)))
                    {
                        sLookUp = GetLookUpKods("ctb_divuch_hariga_meshaot", input);
                        iShatGmar = int.Parse(input.curSidur.sShatGmar.Remove(2, 1).Substring(0, 2));
                        //אם ערך חריגה תקין, אבל אין זכאות לחריגה נעלה שגיאה
                        if (((sLookUp.IndexOf(input.curSidur.sChariga)) != -1) && (!input.curSidur.bZakaiLeCharigaExists) && (iShatGmar < 28))  //לא קיים מאפיין 35
                        {
                            AddNewError(input);
                            return false;
                        }
                    }
                }
            }
            else
            {//סדורים רגילים
                if (!string.IsNullOrEmpty(input.curSidur.sChariga) && Int32.Parse(input.curSidur.sChariga) > 0)
                {
                    AddNewError(input);
                    return false;
                }
            }

            return true;
        }


        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errZakaiLeCharigaValueNotValid; }
        }
    }
}