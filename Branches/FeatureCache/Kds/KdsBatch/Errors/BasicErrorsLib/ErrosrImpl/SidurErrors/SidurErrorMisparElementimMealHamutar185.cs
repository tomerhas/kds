using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using System.Data;
using KdsLibrary;
using KDSCommon.DataModels;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.SidurErrors
{
    public class SidurErrorMisparElementimMealHamutar185 : SidurErrorBase
    {
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            //בדיקה ברמת סידור         
            PeilutDM oPeilut; // = new clPeilut();
            string sMakat;
            int iNumElements730 = 0, iNumElements740 = 0, iNumElements750 = 0;
           
           // DataRow[] drSugSidur = clDefinitions.GetOneSugSidurMeafyen(input.curSidur.iSugSidurRagil, input.CardDate, _dtSugSidur);
            if (input.drSugSidur.Length > 0)
            {
                if (input.drSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nihul.GetHashCode().ToString())
                {
                    for (int j = 0; j < input.curSidur.htPeilut.Count; j++)
                    {
                        oPeilut = (PeilutDM)input.curSidur.htPeilut[j];
                        sMakat = oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3);
                        if (sMakat == "730")
                            iNumElements730 += 1;
                        if (sMakat == "740")
                            iNumElements740 += 1;
                        if (sMakat == "750")
                            iNumElements750 += 1;
                    }
                    if ((iNumElements730 > 0 && iNumElements740 > 0) || (iNumElements740 > 0 && iNumElements750 > 0) || (iNumElements730 > 0 && iNumElements750 > 0))
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
            get { return ErrorTypes.ErrMisparElementimMealHamutar; }
        }
    }
}