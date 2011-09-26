using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KdsLibrary.DAL;
using System.Data;
namespace KdsBatch.Entities
{
    public class Day
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
        List<Sidur> Sidurim;
        Oved oOved;

        public Day(){}
        public Day(int iMisparIshi, DateTime dDate)
        {
            dCardDate = dDate;
            oOved = new Oved(iMisparIshi, dDate);
            if (oOved.dtOvedDetails.Rows.Count > 0)
            {
                SetMeafyneyOved();
                InitSidurim();
            }
        }

     
        private void SetMeafyneyOved()
        {
            EntitiesDal oDal = new EntitiesDal();
            DataTable dtOvedCardDetails;
            try
            {

                dtOvedCardDetails = oDal.GetOvedYomAvodaDetails(oOved.iMisparIshi, dCardDate);
                //נתונים כללים               
                //נוציא את שדה הלבשה ברמת יום עבודה
                if (dtOvedCardDetails.Rows[0]["halbasha"] != null)
                {
                    sHalbasha = dtOvedCardDetails.Rows[0]["halbasha"].ToString();
                }
                if (dtOvedCardDetails.Rows[0]["Hamara"] != null)
                {
                    sHamara = dtOvedCardDetails.Rows[0]["Hamara"].ToString();
                }

                sBitulZmanNesiot = dtOvedCardDetails.Rows[0]["bitul_zman_nesiot"].ToString();
                sTachograf = dtOvedCardDetails.Rows[0]["Tachograf"].ToString();
                sLina = dtOvedCardDetails.Rows[0]["lina"].ToString();
                sHashlamaLeyom = dtOvedCardDetails.Rows[0]["Hashlama_Leyom"].ToString();
                iSibatHashlamaLeyom = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["sibat_hashlama_leyom"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["sibat_hashlama_leyom"].ToString());
                sSidurDay = dtOvedCardDetails.Rows[0]["iDay"].ToString();
                sShabaton = dtOvedCardDetails.Rows[0]["shbaton"].ToString();
                sErevShishiChag = dtOvedCardDetails.Rows[0]["erev_shishi_chag"].ToString();
                sRishyonAutobus = dtOvedCardDetails.Rows[0]["rishyon_autobus"].ToString().Trim();
                sShlilatRishayon = dtOvedCardDetails.Rows[0]["shlilat_rishayon"].ToString();
                iZmanNesiaHaloch = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["zman_nesia_haloch"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["zman_nesia_haloch"].ToString());
                iZmanNesiaHazor = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["zman_nesia_hazor"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["zman_nesia_hazor"].ToString());
                iStatus = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["Status"]) ? -1 : int.Parse(dtOvedCardDetails.Rows[0]["Status"].ToString());
                iStatusTipul = System.Convert.IsDBNull(dtOvedCardDetails.Rows[0]["status_tipul"]) ? 0 : int.Parse(dtOvedCardDetails.Rows[0]["status_tipul"].ToString());
                sStatusCardDesc = dtOvedCardDetails.Rows[0]["teur_status_kartis"].ToString();
                sDayTypeDesc = dtOvedCardDetails.Rows[0]["teur_yom"].ToString();
                iMeasherOMistayeg = String.IsNullOrEmpty(dtOvedCardDetails.Rows[0]["measher_o_mistayeg"].ToString()) ? -1 : int.Parse(dtOvedCardDetails.Rows[0]["measher_o_mistayeg"].ToString()); 
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
           

        void InitSidurim()
        {
            Sidurim = new List<Sidur>();
            Sidur item;// = new Sidur();
            EntitiesDal oDal = new EntitiesDal();
            DataTable dtDetails;
            if (oOved.OvedDetailsExists)
            {
                if (oOved.sKodMaamd == "331" || oOved.sKodMaamd == "332" || oOved.sKodMaamd == "333" || oOved.sKodMaamd == "334")
                {
                  //  clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, iMisparIshi, "I", 0, dCardDate, "MainInputData: " + "HR - לעובד זה חסרים נתונים ב ");
                  //  _bSuccsess = false;
                   // _bHaveCount = false;
                }
                else if (oOved.iIsuk == 0)
                {
                   // clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, iMisparIshi, "W", 0, dCardDate, "MainInputData: " + "HR - לעובד זה חסרים נתונים ב ");
                   // _bSuccsess = false;
                }
                else
                {
                    dtDetails = oDal.GetSidurimLeOved(oOved.iMisparIshi, dCardDate);
                    foreach (DataRow dr in dtDetails.Rows)
                    {
                        item = new Sidur(dr);
                        Sidurim.Add(item);
                    }
                    //if (dtDetails.Rows.Count > 0)
                    //{

                    //    //htEmployeeDetails = oDefinition.InsertEmployeeDetails(false, dtDetails, dCardDate, ref iLastMisaprSidur, out _htSpecialEmployeeDetails, ref htFullSidurimDetails);//, out  _htEmployeeDetailsWithCancled
                    //    //htFullEmployeeDetails = htFullSidurimDetails;
                    //    //sCarNumbers = clDefinitions.GetMasharCarNumbers(htEmployeeDetails);

                    //    //if (sCarNumbers != string.Empty)
                    //    //{
                    //    //    clKavim oKavim = new clKavim();
                    //    //    dtMashar = oKavim.GetMasharData(sCarNumbers);
                    //    //}
                    //}
                    //_dtApproval = clDefinitions.GetApprovalToEmploee(iMisparIshi, dCardDate);
                   // _dtIdkuneyRashemet = clDefinitions.GetIdkuneyRashemet(iMisparIshi, dCardDate);
                   // _dtApprovalError = clDefinitions.GetApprovalErrors(iMisparIshi, dCardDate);

                   // CheckAllData(dCardDate, iMisparIshi, iSugYom);

                    //_IsExecuteInputData = true;
                }
            }
        }
    }
}
