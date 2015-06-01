using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using System.Data;
using KdsLibrary;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.Interfaces.Managers;
using KDSCommon.DataModels;
using Microsoft.Practices.Unity;

namespace KdsBatch.Errors.BasicErrorsLib.ErrosrImpl.SidurErrors
{
    public class SidurErrorChafifaBesidurNihulTnua152 : SidurErrorBase
    {
        public SidurErrorChafifaBesidurNihulTnua152(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            bool isValid = true;
            DataTable dtChafifa = new DataTable();

            if (CheckSidurNihulTnua(input.curSidur, input.drSugSidur))
            {
                var sidurManager = ServiceLocator.Current.GetInstance<ISidurManager>();
                bool isSidurChofef = sidurManager.IsSidurChofef(input.iMisparIshi, input.CardDate, input.curSidur.iMisparSidur, input.curSidur.dFullShatHatchala, input.curSidur.dFullShatGmar, input.oParameters.iMaxChafifaBeinSidureyNihulTnua, dtChafifa);
                if (isSidurChofef)
                {
                    isValid = false;

                    int userId = -2;
                    if (input.UserId.HasValue)
                        userId = input.UserId.Value;
                    for (int i = 0; i < dtChafifa.Rows.Count; i++)
                    {
                        clDefinitions.UpdateCardStatus((int)dtChafifa.Rows[i]["mispar_ishi"], (DateTime)dtChafifa.Rows[i]["taarich"], CardStatus.Error, userId);
                    }
                }
            }
            if (!isValid)
                AddNewError(input);
             
            return isValid;
        }

        private bool CheckSidurNihulTnua(SidurDM oSidur, DataRow[] drSugSidur)
        {
            try
            {
                bool bSidurNihulTnua = false;
                //                 סידורים מסוג ניהול תנועה:
                //ניהול תנועה - (ערך 4 במאפיין 3 מאפייני סוג סידור)
                //לרשות (ערך 6 במאפיין 52 במאפייני סוג סידור)
                //קופאי (ערך 7  במאפיין 52 במאפייני סוג סידור)
                //כוננות גרירה (ערך 8 במאפיין 52 במאפייני סוג סידור)
                if (oSidur.bSidurMyuhad)
                {//סידור מיוחד
                    bSidurNihulTnua = (oSidur.sSectorAvoda == clGeneral.enSectorAvoda.Nihul.GetHashCode().ToString());
                    if (!bSidurNihulTnua)
                    {
                        bSidurNihulTnua = (oSidur.sSugAvoda == clGeneral.enSugAvoda.Lershut.GetHashCode().ToString());
                    }
                    if (!bSidurNihulTnua)
                    {
                        bSidurNihulTnua = (oSidur.sSugAvoda == clGeneral.enSugAvoda.Kupai.GetHashCode().ToString());
                    }
                    if (!bSidurNihulTnua)
                    {
                        bSidurNihulTnua = (oSidur.sSugAvoda == clGeneral.enSugAvoda.Grira.GetHashCode().ToString());
                    }
                }
                else
                {//סידור רגיל
                    if (drSugSidur.Length > 0)
                    {
                        bSidurNihulTnua = (drSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nihul.GetHashCode().ToString());
                        if (!bSidurNihulTnua)
                        {
                            bSidurNihulTnua = (drSugSidur[0]["sug_avoda"].ToString() == clGeneral.enSugAvoda.Lershut.GetHashCode().ToString());
                        }
                        if (!bSidurNihulTnua)
                        {
                            bSidurNihulTnua = (drSugSidur[0]["sug_avoda"].ToString() == clGeneral.enSugAvoda.Kupai.GetHashCode().ToString());
                        }
                        if (!bSidurNihulTnua)
                        {
                            bSidurNihulTnua = (drSugSidur[0]["sug_avoda"].ToString() == clGeneral.enSugAvoda.Grira.GetHashCode().ToString());
                        }
                    }
                }

                return bSidurNihulTnua;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errChafifaBesidurNihulTnua; }
        }
    }
}