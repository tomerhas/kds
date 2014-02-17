using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KdsLibrary;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.SidurErrors
{
    public class SidurErrorSidurAvodaValidForTaarich160 : SidurErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            DateTime dSidurDate;
            bool bError = false;
            if (input.curSidur.bSidurMyuhad)
            {//קיים מאפיין 73 
                if (input.curSidur.bSidurInSummerExists)
                {
                    dSidurDate = input.curSidur.dSidurDate;
                    if ((input.curSidur.sSidurInSummer == "1") || (input.curSidur.sSidurInSummer == "2"))
                    {
                        if (input.oParameters.dStartNihulVShivik.Year != clGeneral.cYearNull)
                        {
                            bError = (dSidurDate < input.oParameters.dStartNihulVShivik);
                        }

                        if (!bError)
                        {
                            if (input.oParameters.dEndNihulVShivik.Year != clGeneral.cYearNull)
                            {
                                bError = (dSidurDate > input.oParameters.dEndNihulVShivik);
                            }
                        }
                    }
                    else
                    {
                        bError = (((dSidurDate < input.oParameters.dStartTiful) && (input.oParameters.dStartTiful.Year != clGeneral.cYearNull)) || ((dSidurDate > input.oParameters.dEndTiful) && (input.oParameters.dEndTiful.Year != clGeneral.cYearNull)));
                    }

                    if (bError)
                    {
                        AddNewError(input);
                        return false;
                    }
                }
            }
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errSidurAvodaNotValidForMonth; }
        }
    }
}