using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.Enums;
using KDSCommon.DataModels.Errors;
using KDSCommon.DataModels;
using System.Data;
using KdsLibrary;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.Interfaces.Managers;

using System.Collections.Specialized;
using KDSCommon.Helpers;
using KDSCommon.Interfaces.DAL;
using Microsoft.Practices.Unity;


namespace KdsErrors.ErrosrImpl.SidurErrors 
{
    public class SidurErrorHighPremya153 : SidurErrorBase
    {
        public SidurErrorHighPremya153(IUnityContainer container)
            : base(container)
        {

        }
        public override bool InternalIsCorrect(ErrorInputData input)
        {
            DataSet dsSidur;
            float dSumMazanTashlum = 0;
            double dSumMazanTichnun = 0;
            int iTypeMakat;
            PeilutDM oPeilut;
            OrderedDictionary htPeilut = new OrderedDictionary();
            float fZmanSidur = 0;
            float fZmanSidurMapa = 0;
            bool isSidurValid = true;
            DateTime dShatGmarMapa, dShaHatchalaMapa;
            int iResult;
            string sShaa;
          
            var kavimDal = ServiceLocator.Current.GetInstance<IKavimDAL>();
            dsSidur = kavimDal.GetSidurAndPeiluyotFromTnua(input.curSidur.iMisparSidur, input.CardDate, null, out iResult);

            if (iResult == 0)
            {
                //שעת התחלה ושעת גמר
                if (dsSidur.Tables[1].Rows.Count > 0)
                {
                    sShaa = dsSidur.Tables[1].Rows[0]["SHAA"].ToString();
                    dShaHatchalaMapa = clGeneral.GetDateTimeFromStringHour(sShaa, input.CardDate);
                    for (int i = dsSidur.Tables[1].Rows.Count - 1; i >= 0; i--)
                    {
                        long lMakatNesia = long.Parse(dsSidur.Tables[1].Rows[i]["MAKAT8"].ToString());
                        sShaa = dsSidur.Tables[1].Rows[i]["SHAA"].ToString();
                        if (!string.IsNullOrEmpty(dsSidur.Tables[1].Rows[i]["MazanTichnun"].ToString()))
                            dSumMazanTichnun = double.Parse(dsSidur.Tables[1].Rows[i]["MazanTichnun"].ToString());
                        dShatGmarMapa = clGeneral.GetDateTimeFromStringHour(sShaa, input.CardDate).AddMinutes(dSumMazanTichnun);
                        fZmanSidurMapa = int.Parse((dShatGmarMapa - dShaHatchalaMapa).TotalMinutes.ToString());

                        //במידה והפעילות האחרונה היא אלמנט לידיעה בלבד (ערך 2 (לידיעה בלבד) במאפיין 3  (לפעולה/לידיעה בלבד), יש לקחת את הפעילות הקודמת לה.

                        if ((enMakatType)(StaticBL.GetMakatType(lMakatNesia)) == enMakatType.mElement)
                        {
                            var dtTmpMeafyeneyElements = clDefinitions.GetTmpMeafyeneyElements(input.CardDate, input.CardDate);
                            DataRow drMeafyeneyElements = dtTmpMeafyeneyElements.Select("kod_element=" + int.Parse(lMakatNesia.ToString().Substring(1, 2)))[0];
                            if (drMeafyeneyElements["element_for_yedia"].ToString() != "2")
                            {
                                break;
                            }
                        }
                        else { break; }
                    }

                }
            }
            // נתונים מהסידור בכרטיס העבודה 
            fZmanSidur = float.Parse((input.curSidur.dFullShatGmar - input.curSidur.dFullShatHatchala).TotalMinutes.ToString());
            if (fZmanSidur >= 0)
            {
                htPeilut = input.curSidur.htPeilut;
                for (int i = 0; i < input.curSidur.htPeilut.Values.Count; i++)
                {
                    oPeilut = ((PeilutDM)htPeilut[i]);
                    iTypeMakat = oPeilut.iMakatType;
                    if ((oPeilut.iMisparKnisa == 0 && iTypeMakat == enMakatType.mKavShirut.GetHashCode()) || iTypeMakat == enMakatType.mEmpty.GetHashCode() || iTypeMakat == enMakatType.mNamak.GetHashCode())
                    {
                        dSumMazanTashlum += oPeilut.iMazanTashlum;
                    }
                    else if (iTypeMakat == enMakatType.mElement.GetHashCode())
                    {
                        if (oPeilut.sElementInMinutes == "1" && oPeilut.sKodLechishuvPremia.Trim() == "1:1")
                        {
                            dSumMazanTashlum += Int32.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));
                        }
                    }
                }

                if (dSumMazanTashlum >= fZmanSidur)
                {
                    if (input.curSidur.bSidurMyuhad)
                    {
                        if (dSumMazanTashlum >= (fZmanSidur * 2))
                            isSidurValid = false;
                    }
                    else
                    {
                        if ((dSumMazanTashlum >= (fZmanSidur + 90)) || (dSumMazanTashlum >= (fZmanSidur * 2)))
                            if (((((dSumMazanTashlum - fZmanSidur) / (dSumMazanTichnun - fZmanSidurMapa)) * 100) - 100) < input.oParameters.fHighPremya)
                                isSidurValid = false;
                    }
                }

                if (!isSidurValid)
                {
                    AddNewError(input);
                    return false;
                }
                
            }
            
            return true;
        }

        public override ErrorTypes CardErrorType
        {
            get { return ErrorTypes.errHighPremya; }
        }
    }
}