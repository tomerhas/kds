using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary.BL;
using System.Data;
using KdsBatch.Entities;
using KdsLibrary;

namespace KdsBatch.Errors
{
    public class MainErrors
    {

        public MainErrors(DateTime taarich)
        {
            GlobalData.InitGlobalData(taarich);
        }


        public bool HafelShguim(int mispar_ishi, DateTime taarich)
        {
            EntitiesDal oDal = new EntitiesDal();
            clGeneral.enCardStatus _CardStatus;
            bool bHaveShgiotLetzuga = false;
            string sArrKodShgia;
            try
            {
                GlobalData.CardErrors.Clear();
                Day oDay = new Day(mispar_ishi, taarich, true);// new Day(int.Parse(txtId.Text), DateTime.Parse(clnFromDate.Text), true);
           //     oDay.btchRequest = 0;
                if (oDay.oOved.bOvedDetailsExists)
                {
                    try
                    {
                        foreach (Sidur oSidur in oDay.Sidurim)
                        {
                            if (oSidur.bIsSidurLeBdika)
                            {
                                foreach (Peilut oPeilut in oSidur.Peiluyot)
                                {
                                    oPeilut.Run();
                                }
                                oSidur.Run();
                            }
                        }
                        oDay.Run();
                    }
                    catch (Exception ex)
                    {
                        clLogBakashot.InsertErrorToLog(0, mispar_ishi, "E", 0, taarich, "HafelShguim: " + ex.Message);
                        oDay.bSuccsess = false;
                    }

                    oDal.DeleteErrorsFromTbShgiot(oDay.oOved.iMisparIshi, oDay.dCardDate);

                    sArrKodShgia = "";
                    oDay.RemoveShgiotMeusharotFromDt(ref sArrKodShgia);
                    if (sArrKodShgia.Length > 0)
                    {
                        sArrKodShgia = sArrKodShgia.Substring(0, sArrKodShgia.Length - 1);
                        bHaveShgiotLetzuga = oDal.CheckShgiotLetzuga(sArrKodShgia);
                    }
                    if (GlobalData.CardErrors.Count > 0)
                    {
                        oDal.InsertErrorsToTbShgiot(oDay.dCardDate);
                        _CardStatus = clGeneral.enCardStatus.Error;
                    }
                    else
                    {
                        _CardStatus = clGeneral.enCardStatus.Valid;
                    }
                    if (_CardStatus.GetHashCode() != oDay.iStatus)
                    {
                        oDal.UpdateCardStatus(oDay.oOved.iMisparIshi, oDay.dCardDate, _CardStatus, oDay.iUserId);
                    }

                    oDal.UpdateRitzatShgiotDate(oDay.oOved.iMisparIshi, oDay.dCardDate, bHaveShgiotLetzuga);
                }
                 return oDay.bSuccsess;
            }
            catch (Exception ex)
            {
                clLogBakashot.InsertErrorToLog(0, mispar_ishi, "E", 0, taarich, "HafelShguim: " + ex.Message);
                return false;
            }
        }
    }
}
