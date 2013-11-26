using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary.DAL;
using System.Data;
using KdsLibrary.Utils;
using KdsLibrary;
using KdsBatch.Errors;
using KdsLibrary.BL;
using KDSCommon.DataModels;
using Microsoft.Practices.ServiceLocation;
using KDSCommon.Interfaces;

namespace KdsBatch.Entities
{
    public class Day : BasicErrors
    {
       
        public string sHalbasha = "";
        public string sHamara = "";
        public string sBitulZmanNesiot;
        public string sTachograf = "";
        public int iZmanNesiaHaloch;
        public int iZmanNesiaHazor;
        public int iStatus;
        public int iStatusTipul;
        public string sStatusCardDesc;
        public string sDayTypeDesc; //תאור סוג יום
        public int iMeasherOMistayeg; //מאשר או מסתייג, אם התקבל ערך NULL ייכנס -1
        public string sHashlamaLeyom = "";
        public int iSibatHashlamaLeyom;
        public string sLina = "";     
        public string sSidurDay;
        public string sShabaton;
        public string sErevShishiChag;
        public string sRishyonAutobus;
        public string sShlilatRishayon;
        public DateTime dCardDate;
        public int iSugYom;
        public int iTotalHashlamotForSidur;
        public int iTotalTimePrepareMechineForDay=0;
        public int iTotalTimePrepareMechineForOtherMechines = 0;
        
        public int iUserId;
        public bool bSuccsess = true;
        public long btchRequest;
        public bool IsExecuteErrors = false;

        public clParametersDM oParameters;
        public List<Sidur> Sidurim;
    //    public DataTable dtTmpMeafyeneyElements;
        public Oved oOved;
        public List<CardError> CardErrors;
        public clGeneral.enCardStatus CardStatus;
        
        public Day() : base(OriginError.Day)  { }
        public Day(int iMisparIshi, DateTime dDate, bool bInsertToShguim) : base(OriginError.Day)
        {
            iUserId = -2;
            dCardDate = dDate;
            oOved = new Oved(iMisparIshi, dDate);
            if (oOved.dtOvedDetails.Rows.Count > 0)
            {
                CardErrors = new List<CardError>();
                SetPirteyYom();
                InitSidurim(bInsertToShguim);
                InitializeErrors();
            }
        }

     
        private void SetPirteyYom()
        {
            EntitiesDal oDal = new EntitiesDal();
          //  DataTable oOved.dtOvedDetails;
            try
            {
                //dtTmpMeafyeneyElements = oDal.GetTmpMeafyeneyElements(dCardDate, dCardDate);
              //  oOved.dtOvedDetails = oDal.GetOvedYomAvodaDetails(oOved.iMisparIshi, dCardDate);
                //נתונים כללים               
                //נוציא את שדה הלבשה ברמת יום עבודה
                if (oOved.dtOvedDetails.Rows[0]["halbasha"] != null)
                {
                    sHalbasha = oOved.dtOvedDetails.Rows[0]["halbasha"].ToString();
                }
                if (oOved.dtOvedDetails.Rows[0]["Hamara"] != null)
                {
                    sHamara = oOved.dtOvedDetails.Rows[0]["Hamara"].ToString();
                }

                sBitulZmanNesiot = oOved.dtOvedDetails.Rows[0]["bitul_zman_nesiot"].ToString();
                sTachograf = oOved.dtOvedDetails.Rows[0]["Tachograf"].ToString();
                sLina = oOved.dtOvedDetails.Rows[0]["lina"].ToString();
                sHashlamaLeyom = oOved.dtOvedDetails.Rows[0]["Hashlama_Leyom"].ToString();
                iSibatHashlamaLeyom = System.Convert.IsDBNull(oOved.dtOvedDetails.Rows[0]["sibat_hashlama_leyom"]) ? 0 : int.Parse(oOved.dtOvedDetails.Rows[0]["sibat_hashlama_leyom"].ToString());
                sSidurDay = oOved.dtOvedDetails.Rows[0]["iDay"].ToString();
                sShabaton = oOved.dtOvedDetails.Rows[0]["shbaton"].ToString();
                sErevShishiChag = oOved.dtOvedDetails.Rows[0]["erev_shishi_chag"].ToString();
                sRishyonAutobus = oOved.dtOvedDetails.Rows[0]["rishyon_autobus"].ToString().Trim();
                sShlilatRishayon = oOved.dtOvedDetails.Rows[0]["shlilat_rishayon"].ToString();
                iZmanNesiaHaloch = System.Convert.IsDBNull(oOved.dtOvedDetails.Rows[0]["zman_nesia_haloch"]) ? 0 : int.Parse(oOved.dtOvedDetails.Rows[0]["zman_nesia_haloch"].ToString());
                iZmanNesiaHazor = System.Convert.IsDBNull(oOved.dtOvedDetails.Rows[0]["zman_nesia_hazor"]) ? 0 : int.Parse(oOved.dtOvedDetails.Rows[0]["zman_nesia_hazor"].ToString());
                iStatus = System.Convert.IsDBNull(oOved.dtOvedDetails.Rows[0]["Status"]) ? -1 : int.Parse(oOved.dtOvedDetails.Rows[0]["Status"].ToString());
                iStatusTipul = System.Convert.IsDBNull(oOved.dtOvedDetails.Rows[0]["status_tipul"]) ? 0 : int.Parse(oOved.dtOvedDetails.Rows[0]["status_tipul"].ToString());
                sStatusCardDesc = oOved.dtOvedDetails.Rows[0]["teur_status_kartis"].ToString();
                sDayTypeDesc = oOved.dtOvedDetails.Rows[0]["teur_yom"].ToString();
                iMeasherOMistayeg = String.IsNullOrEmpty(oOved.dtOvedDetails.Rows[0]["measher_o_mistayeg"].ToString()) ? -1 : int.Parse(oOved.dtOvedDetails.Rows[0]["measher_o_mistayeg"].ToString());
                iSugYom = clGeneral.GetSugYom(GlobalData.dtYamimMeyuchadim, dCardDate, GlobalData.dtSugeyYamimMeyuchadim);
                IParametersManager paramManager = ServiceLocator.Current.GetInstance<IParametersManager>();
                oParameters = paramManager.CreateClsParametrs(dCardDate, iSugYom);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void InitSidurim(bool bInsertToShguim)
        {
            Sidurim = new List<Sidur>();
            Sidur item;// = new Sidur();
            EntitiesDal oDal = new EntitiesDal();
            DateTime dcurShatHatchala, dPreShatHatchala;
            int curMisparSidur, prevMisparSidur;
            int i = 0;
            if (oOved.bOvedDetailsExists)
            {
                prevMisparSidur = 0;
                dPreShatHatchala = DateTime.MinValue;
                foreach (DataRow dr in oOved.dtSidurimVePeiluyot.Rows)
                {
                    curMisparSidur = int.Parse(dr["Mispar_Sidur"].ToString());
                    dcurShatHatchala = DateTime.Parse(dr["shat_hatchala"].ToString());
                    if ((curMisparSidur != prevMisparSidur) || (curMisparSidur == prevMisparSidur && dcurShatHatchala != dPreShatHatchala))
                    {
                        if (!GlobalData.SpecialSidurim.Contains(curMisparSidur))
                        {
                            item = new Sidur(dr,this);
                            item.iMispar_Siduri = i;
                       
                            if (!bInsertToShguim || (bInsertToShguim && (item.iLoLetashlum == 0 || (item.iLoLetashlum == 1 && item.iLebdikaShguim == 1))))
                                item.bIsSidurLeBdika = true;
                            Sidurim.Add(item);
                            i++;
                        }
                      
                    }
                    prevMisparSidur = curMisparSidur;
                    dPreShatHatchala = dcurShatHatchala;
                }
            }
        }

        public int GetZmanNesiaMeshtana(int iSidurIndex, int iType, DateTime dCardDate)
        {
            int iZmanNesia = 0;
            int iMerkazErua = 0;
            int iMikumYaad = 0;
            clUtils oUtils = new clUtils();

            //נשלוף את הסידור 
            Sidur oSidur;
            try
            {
                if (Sidurim.Count > 0)
                {
                    oSidur = Sidurim[iSidurIndex];

                    //עבור מאפיין 61:
                    if (iType == 1) //כניסה
                    {
                        if (oSidur.sMikumShaonKnisa.Length > 0)
                        {
                            iMikumYaad = int.Parse(oSidur.sMikumShaonKnisa);
                        }
                    }
                    else //יציאה
                    {
                        if (oSidur.sMikumShaonYetzia.Length > 0)
                        {
                            iMikumYaad = int.Parse(oSidur.sMikumShaonYetzia);
                        }
                    }

                    iMerkazErua = (String.IsNullOrEmpty(oOved.sMercazErua) ? 0 : int.Parse(oOved.sMercazErua));
                    if ((iMerkazErua > 0) && (iMikumYaad > 0))
                    {
                        iZmanNesia = oUtils.GetZmanNesia(iMerkazErua, iMikumYaad, dCardDate);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iZmanNesia;
        }

        public Sidur getLastSidurLebdika()
        {
            for (int i = Sidurim.Count - 1; i>=0; i--)
            {
                if (Sidurim[i].bIsSidurLeBdika)
                    return Sidurim[i];
            }
            return null;
        }
        //public run

        public Sidur getPrevSidurLeTashlum(int index)
        {

            try
            {
                for (int i = (index-1); i >= 0; i--)
                {
                    if (Sidurim[i].bIsSidurLeBdika)
                        return Sidurim[i];
                }
                return null;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
