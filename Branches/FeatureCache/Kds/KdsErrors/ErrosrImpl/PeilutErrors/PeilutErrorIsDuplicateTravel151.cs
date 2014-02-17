using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Errors;
using KDSCommon.Enums;
using KDSCommon.Interfaces.Managers;
using KdsLibrary;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace KdsErrors.ErrosrImpl.PeilutErrors
{
    public class PeilutErrorIsDuplicateTravel151 : PeilutErrorBase
    {
        public PeilutErrorIsDuplicateTravel151(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            DataTable dtDuplicate = new DataTable();
           
            var ovedManagaer = _container.Resolve<IOvedManager>(); 
            if (input.curPeilut.iMakatType == enMakatType.mKavShirut.GetHashCode())
            {
                var PeilutManager = ServiceLocator.Current.GetInstance<IPeilutManager>();
               
                if (PeilutManager.IsDuplicateTravle(input.iMisparIshi, input.CardDate, input.curPeilut.lMakatNesia, input.curPeilut.dFullShatYetzia, input.curPeilut.iMisparKnisa, ref dtDuplicate))
                {
                    
                    for (int i = 0; i < dtDuplicate.Rows.Count; i++)
                    {
                        //if (!CheckApprovalToEmploee((int)dtDuplicate.Rows[i]["mispar_ishi"],(DateTime)dtDuplicate.Rows[i]["taarich"],"25", oSidur.iMisparSidur, oSidur.dFullShatHatchala, input.curPeilut.iMisparKnisa, input.curPeilut.dFullShatYetzia))
                        int userId = -2;
                        if (input.UserId.HasValue)
                            userId = input.UserId.Value;
                        ovedManagaer.UpdateCardStatus((int)dtDuplicate.Rows[i]["mispar_ishi"], (DateTime)dtDuplicate.Rows[i]["taarich"], CardStatus.Error, userId);
                    }

                    AddNewError(input);
                    return false;
                }
            }
            //נסיעה כפולה בין עובדים שונים
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errDuplicateTravle; }
        }
    }
}


