using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using KdsLibrary;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.PeilutErrors
{
    public class PeilutErrorIsElementTimeValid129 : PeilutErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
        
       
            long lMakatNesia = input.curPeilut.lMakatNesia;
            int iElementTime;
            float fSidurTime;
            fSidurTime = float.Parse((!string.IsNullOrEmpty(input.curSidur.sShatGmar) && !string.IsNullOrEmpty(input.curSidur.sShatHatchala) ? (input.curSidur.dFullShatGmar - input.curSidur.dFullShatHatchala).TotalMinutes : 0).ToString()); //clDefinitions.GetSidurTimeInMinuts(oSidur);

            //iSidurTime = int.Parse(input.curSidur.sShatGmar.Replace(":", "")) * 60 - int.Parse(input.curSidur.sShatHatchala.Replace(":", "")) * 60;           
            if ((input.curPeilut.iMakatType == enMakatType.mElement.GetHashCode()) && (input.curPeilut.sElementZviraZman != clGeneral.enSectorZviratZmanForElement.ElementZviratZman.GetHashCode().ToString()) && (input.curPeilut.sElementInMinutes == "1"))
            {
                iElementTime = int.Parse(lMakatNesia.ToString().PadLeft(8).Substring(3, 3));
                if (iElementTime > (fSidurTime))
                {
                    AddNewError(input);
                    return false;
                }

            }
            //"משך זמן האלמנט גדול ממשך זמן הסידור
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errElementTimeBiggerThanSidurTime; }
        }
    }
}

