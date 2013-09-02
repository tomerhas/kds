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
        public Day oDay;
        public DataTable dtErrors = new DataTable();

        public MainErrors(DateTime taarich)
        {
            GlobalData.InitGlobalData(taarich);
        }


        public bool HafelShguim(int mispar_ishi, DateTime taarich)
        {
            EntitiesDal oDal = new EntitiesDal();
            
            bool bHaveShgiotLetzuga = false;
            string sArrKodShgia;
            try
            {
              //  GlobalData.CardErrors.Clear();
                oDay = new Day(mispar_ishi, taarich, true);// new Day(int.Parse(txtId.Text), DateTime.Parse(clnFromDate.Text), true);
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
                                    oPeilut.Run(oDay);
                                }
                                oSidur.Run(oDay);
                            }
                        }
                        oDay.Run(oDay);
                    }
                    catch (Exception ex)
                    {
                        clLogBakashot.InsertErrorToLog(0, mispar_ishi, "E", 0, taarich, "HafelShguim: " + ex.Message);
                        oDay.bSuccsess = false;
                    }

                    oDal.DeleteErrorsFromTbShgiot(oDay.oOved.iMisparIshi, oDay.dCardDate);

                    sArrKodShgia = "";
                    oDay.RemoveShgiotMeusharotFromDt(ref sArrKodShgia, oDay);
                    if (sArrKodShgia.Length > 0)
                    {
                        sArrKodShgia = sArrKodShgia.Substring(0, sArrKodShgia.Length - 1);
                        bHaveShgiotLetzuga = oDal.CheckShgiotLetzuga(sArrKodShgia);
                    }
                    if (oDay.CardErrors.Count > 0)
                    {
                        oDal.InsertErrorsToTbShgiot(oDay);
                        oDay.CardStatus = clGeneral.enCardStatus.Error;
                    }
                    else
                    {
                        oDay.CardStatus = clGeneral.enCardStatus.Valid;
                    }
                    if (oDay.CardStatus.GetHashCode() != oDay.iStatus)
                    {
                        oDal.UpdateCardStatus(oDay.oOved.iMisparIshi, oDay.dCardDate, oDay.CardStatus, oDay.iUserId);
                    }

                    oDal.UpdateRitzatShgiotDate(oDay.oOved.iMisparIshi, oDay.dCardDate, bHaveShgiotLetzuga);
                }
                oDay.IsExecuteErrors = true;
                dtErrors = oDay.CardErrors.ToDataTable();
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
