using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using KDSCommon.DataModels.Shinuyim;
using KDSCommon.Enums;
using KDSCommon.Helpers;
using KDSCommon.Interfaces;
using KDSCommon.Interfaces.Managers;
using KDSCommon.UDT;
using KdsShinuyim.DataModels;
using KdsShinuyim.Enums;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using ObjectCompare;

namespace KdsShinuyim.ShinuyImpl
{
    public abstract class ShinuyBase : IShinuy
    {
        protected IUnityContainer _container;

        protected const int SIDUR_NESIA = 99300;
        protected const int SIDUR_MATALA = 99301;
        protected const string SIBA_LE_DIVUCH_YADANI_HALBASHA = "goremet_lebitul_zman_halbasha";
        protected const string SIBA_LE_DIVUCH_YADANI_NESIAA = "goremet_lebitul_zman_nesiaa";

        //protected const int SIDUR_HEADRUT_BETASHLUM = 99801;
        //protected const int SIDUR_RETIZVUT99500 = 99500;
        //protected const int SIDUR_RETIZVUT99501 = 99501;

        public ShinuyBase(IUnityContainer container)
        {
            _container = container;
        }

        public abstract void ExecShinuy(ShinuyInputData inputData);

        public abstract ShinuyTypes ShinuyType { get; }

        protected bool CheckIdkunRashemet(string sFieldToChange, int iMisparSidur, DateTime dShatHatchala, ShinuyInputData inputData)
        {
            bool bHaveIdkun = false;
            DataRow[] drIdkunim;
            try
            {
                drIdkunim = inputData.IdkuneyRashemet.Select("shem_db='" + sFieldToChange.ToUpper() + "' AND MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime')");
                if (drIdkunim.Length > 0)
                    bHaveIdkun = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bHaveIdkun;
        }
        protected bool CheckIdkunRashemet(string sFieldToChange, ShinuyInputData inputData)
        {
            bool bHaveIdkun = false;
            DataRow[] drIdkunim;
            try
            {
                drIdkunim = inputData.IdkuneyRashemet.Select("shem_db='" + sFieldToChange.ToUpper() + "'");
                if (drIdkunim.Length > 0)
                    bHaveIdkun = true;

                //if (sFieldToChange.ToUpper() == "SHAT_HATCHALA")
                //    bHaveIdkun = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bHaveIdkun;
        }
        protected bool CheckIdkunRashemet(string sFieldToChange, int iMisparSidur, DateTime dShatHatchala, int iMisparKnisa, DateTime dShatYetzia, ShinuyInputData inputData)
        {
            bool bHaveIdkun = false;
            DataRow[] drIdkunim;
            try
            {
                drIdkunim = inputData.IdkuneyRashemet.Select("shem_db='" + sFieldToChange.ToUpper() + "' AND MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime') AND MISPAR_KNISA=" + iMisparKnisa + " AND SHAT_YETZIA=Convert('" + dShatYetzia.ToString() + "', 'System.DateTime') ");
                if (drIdkunim.Length > 0)
                    bHaveIdkun = true;

                //if (sFieldToChange.ToUpper() == "SHAT_HATCHALA")
                //    bHaveIdkun = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bHaveIdkun;
        }

        protected NewSidur FindSidurOnHtNewSidurim(int iMisparSidur, DateTime dShatHatchala, OrderedDictionary htNewSidurim)
        {
            NewSidur oNewSidurim = null;
            NewSidur oSidur;
            for (int i = 0; i < htNewSidurim.Count; i++)
            {
                oSidur = (NewSidur)htNewSidurim[i];
                if (oSidur.SidurNew == iMisparSidur && oSidur.ShatHatchalaNew == dShatHatchala)
                {
                    oNewSidurim = oSidur;
                    break;
                }
            }

            if (oNewSidurim == null)
            {
                oNewSidurim = new NewSidur();
                oNewSidurim.ShatHatchalaOld = dShatHatchala;
                oNewSidurim.SidurOld = iMisparSidur;

                htNewSidurim.Add(string.Concat(dShatHatchala.ToString("HH:mm:ss").Replace(":", ""), iMisparSidur), oNewSidurim);

            }
            return oNewSidurim;
        }

        protected bool ContainsPeilutNamak(SidurDM curSidur)
        {
            return curSidur.htPeilut.Values.Cast<PeilutDM>().ToList().Any(Peilut => Peilut.iMakatType == enMakatType.mNamak.GetHashCode());
        }

        protected OBJ_SIDURIM_OVDIM GetUpdSidurObject(SidurDM oSidur, ShinuyInputData inputData)
        {
            //מביא את הסידור לפי מפתח האינדקס
            OBJ_SIDURIM_OVDIM oObjSidurimOvdim = null;
            try
            {
                var sidurOvdimContainer = inputData.oCollSidurimOvdimUpdRecorder.SingleOrDefault(x =>
                {
                    if (x.ContainedItem.NEW_MISPAR_SIDUR == oSidur.iMisparSidur
                        && x.ContainedItem.NEW_SHAT_HATCHALA == oSidur.dFullShatHatchala
                        && x.ContainedItem.UPDATE_OBJECT == 1)
                    {
                        return true;
                    }
                    return false;
                });
                if (sidurOvdimContainer != null)
                {
                    return sidurOvdimContainer.ContainedItem;
                }

                //Search for sidur ovdim in oCollSidurimOvdimIns collection
                for (int i = 0; i <= inputData.oCollSidurimOvdimIns.Count - 1; i++)
                {
                    if ((inputData.oCollSidurimOvdimIns.Value[i].NEW_MISPAR_SIDUR == oSidur.iMisparSidur) && (inputData.oCollSidurimOvdimIns.Value[i].NEW_SHAT_HATCHALA == oSidur.dFullShatHatchala))
                    {
                        oObjSidurimOvdim = inputData.oCollSidurimOvdimIns.Value[i];
                    }
                }


                if (oObjSidurimOvdim==null)
                {
                    oObjSidurimOvdim = InsertToObjSidurimOvdimForUpdate(oSidur, inputData.UserId);
                    ModificationRecorder<OBJ_SIDURIM_OVDIM> recorder = new ModificationRecorder<OBJ_SIDURIM_OVDIM>(oObjSidurimOvdim, true);
                    inputData.oCollSidurimOvdimUpdRecorder.Add(recorder);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oObjSidurimOvdim;
        }
        //TODO modify the name from Insert to Create
        protected OBJ_SIDURIM_OVDIM InsertToObjSidurimOvdimForUpdate(SidurDM oSidur,  int? UserId)
        {
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd = new OBJ_SIDURIM_OVDIM();
            try
            {
                oObjSidurimOvdimUpd.MISPAR_SIDUR = oSidur.iMisparSidur;
                oObjSidurimOvdimUpd.NEW_MISPAR_SIDUR = oSidur.iMisparSidur;
                oObjSidurimOvdimUpd.MISPAR_ISHI = oSidur.iMisparIshi;
                if (!String.IsNullOrEmpty(oSidur.sChariga))
                {
                    oObjSidurimOvdimUpd.CHARIGA = int.Parse(oSidur.sChariga);
                }

                if (!String.IsNullOrEmpty(oSidur.sHashlama))
                {
                    oObjSidurimOvdimUpd.HASHLAMA = int.Parse(oSidur.sHashlama);
                }
                oObjSidurimOvdimUpd.SUG_HASHLAMA = oSidur.iSugHashlama;
                oObjSidurimOvdimUpd.LO_LETASHLUM = oSidur.iLoLetashlum;
                if (!String.IsNullOrEmpty(oSidur.sOutMichsa))
                {
                    oObjSidurimOvdimUpd.OUT_MICHSA = int.Parse(oSidur.sOutMichsa);
                }

                if (!String.IsNullOrEmpty(oSidur.sPitzulHafsaka))
                {
                    oObjSidurimOvdimUpd.PITZUL_HAFSAKA = int.Parse(oSidur.sPitzulHafsaka);
                }
                if (oSidur.dFullShatGmar != DateTime.MinValue)
                {
                    oObjSidurimOvdimUpd.SHAT_GMAR = oSidur.dFullShatGmar;//DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatGmar));
                }
                oObjSidurimOvdimUpd.SHAT_HATCHALA = oSidur.dFullShatHatchala;//DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatHatchala));
                oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA = oSidur.dFullShatHatchala;//DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatHatchala));
                oObjSidurimOvdimUpd.TAARICH = oSidur.dSidurDate;
                if (!String.IsNullOrEmpty(oSidur.sVisa))
                {
                    oObjSidurimOvdimUpd.YOM_VISA = int.Parse(oSidur.sVisa);
                }

                oObjSidurimOvdimUpd.MIVTZA_VISA = oSidur.iMivtzaVisa;

                oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = oSidur.dFullShatHatchalaLetashlum; //DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatHatchalaLetashlum));
                oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = oSidur.dFullShatGmarLetashlum;//DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatGmarLetashlum));
                oObjSidurimOvdimUpd.MEZAKE_NESIOT = oSidur.iMezakeNesiot;
                oObjSidurimOvdimUpd.MEZAKE_HALBASHA = oSidur.iMezakeHalbasha;
                oObjSidurimOvdimUpd.TOSEFET_GRIRA = oSidur.iTosefetGrira;
                if (!String.IsNullOrEmpty(oSidur.sMikumShaonKnisa))
                {
                    oObjSidurimOvdimUpd.MIKUM_SHAON_KNISA = int.Parse(oSidur.sMikumShaonKnisa);
                }

                if (!String.IsNullOrEmpty(oSidur.sMikumShaonYetzia))
                {
                    oObjSidurimOvdimUpd.MIKUM_SHAON_YETZIA = int.Parse(oSidur.sMikumShaonYetzia);
                }

                //oObjSidurimOvdimUpd.KM_VISA_LEPREMIA = oSidur.iKmVisaLepremia;                
                oObjSidurimOvdimUpd.ACHUZ_KNAS_LEPREMYAT_VISA = oSidur.iAchuzKnasLepremyatVisa;
                oObjSidurimOvdimUpd.ACHUZ_VIZA_BESIKUN = oSidur.iAchuzVizaBesikun;
                //oObjSidurimOvdimUpd.SUG_VISA = oSidur.iSugVisa;
                oObjSidurimOvdimUpd.MISPAR_MUSACH_O_MACHSAN = oSidur.iMisparMusachOMachsan;
                oObjSidurimOvdimUpd.KOD_SIBA_LEDIVUCH_YADANI_IN = oSidur.iKodSibaLedivuchYadaniIn;
                oObjSidurimOvdimUpd.KOD_SIBA_LEDIVUCH_YADANI_OUT = oSidur.iKodSibaLedivuchYadaniOut;
                oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM = oSidur.iKodSibaLoLetashlum;
                oObjSidurimOvdimUpd.SHAYAH_LEYOM_KODEM = oSidur.iShayahLeyomKodem;
                if (UserId.HasValue)
                    oObjSidurimOvdimUpd.MEADKEN_ACHARON = UserId.Value;
                oObjSidurimOvdimUpd.TAARICH_IDKUN_ACHARON = oSidur.dTaarichIdkunAcharon;
                oObjSidurimOvdimUpd.TAFKID_VISA = oSidur.iTafkidVisa;

                oObjSidurimOvdimUpd.HEARA = oSidur.sHeara;
                oObjSidurimOvdimUpd.MISPAR_SHIUREY_NEHIGA = oSidur.iMisparShiureyNehiga;
                //oObjSidurimOvdimUpd.BUTAL = oSidur.iButal;
                oObjSidurimOvdimUpd.BITUL_O_HOSAFA = oSidur.iBitulOHosafa;
                oObjSidurimOvdimUpd.SUG_HAZMANAT_VISA = oSidur.iSugHazmanatVisa;
                if (oSidur.iSugSidurRagil != 0)
                    oObjSidurimOvdimUpd.SUG_SIDUR = oSidur.iSugSidurRagil;
                if (oSidur.bSectorVisaExists)
                {
                    oObjSidurimOvdimUpd.SECTOR_VISA = oSidur.iSectorVisa;
                }
                if (oSidur.dShatHitiatzvut != DateTime.MinValue)
                {
                    oObjSidurimOvdimUpd.SHAT_HITIATZVUT = oSidur.dShatHitiatzvut;
                }
                if (oSidur.iNidreshetHitiatzvut > 0)
                {
                    oObjSidurimOvdimUpd.NIDRESHET_HITIATZVUT = oSidur.iNidreshetHitiatzvut;
                }
                if (oSidur.iPtorMehitiatzvut > 0)
                {
                    oObjSidurimOvdimUpd.PTOR_MEHITIATZVUT = oSidur.iPtorMehitiatzvut;
                }
                if (oSidur.iHachtamaBeatarLoTakin > 0)
                {
                    oObjSidurimOvdimUpd.HACHTAMA_BEATAR_LO_TAKIN = oSidur.iHachtamaBeatarLoTakin.ToString();
                }
                return oObjSidurimOvdimUpd;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void UpdateObjectUpdSidurim(NewSidur oNewSidurim, ModificationRecorderCollection<OBJ_SIDURIM_OVDIM> oCollSidurimOvdimRecorderUpd)
        {
            try
            {

                for (int j = 0; j <= oCollSidurimOvdimRecorderUpd.Count - 1; j++)
                {
                    if ((oCollSidurimOvdimRecorderUpd[j].ContainedItem.MISPAR_SIDUR == oNewSidurim.SidurOld) && (oCollSidurimOvdimRecorderUpd[j].ContainedItem.SHAT_HATCHALA == oNewSidurim.ShatHatchalaOld))
                    {
                        oCollSidurimOvdimRecorderUpd[j].ContainedItem.NEW_MISPAR_SIDUR = oNewSidurim.SidurNew;
                        oCollSidurimOvdimRecorderUpd[j].ContainedItem.NEW_SHAT_HATCHALA = oNewSidurim.ShatHatchalaNew;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected bool TryGetPeilutFromUpdObject(SidurDM oSidur, PeilutDM oPeilut, ModificationRecorderCollection<OBJ_PEILUT_OVDIM> oCollPeilutOvdimUpdRecorder, out OBJ_PEILUT_OVDIM oObjPeilutOvdimUpd)
        {
            oObjPeilutOvdimUpd = null; // new OBJ_PEILUT_OVDIM();
            for (int i = 0; i <= oCollPeilutOvdimUpdRecorder.Count - 1; i++)
            {
                if ((oCollPeilutOvdimUpdRecorder[i].ContainedItem.NEW_MISPAR_SIDUR == oSidur.iMisparSidur) && (oCollPeilutOvdimUpdRecorder[i].ContainedItem.NEW_SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
                    && (oCollPeilutOvdimUpdRecorder[i].ContainedItem.NEW_SHAT_YETZIA == oPeilut.dFullShatYetzia) && (oCollPeilutOvdimUpdRecorder[i].ContainedItem.MISPAR_KNISA == oPeilut.iMisparKnisa) && oCollPeilutOvdimUpdRecorder[i].ContainedItem.UPDATE_OBJECT == 1)
                {
                    oObjPeilutOvdimUpd = oCollPeilutOvdimUpdRecorder[i].ContainedItem;
                    return true;
                }
            }
            return false;
        }

        protected bool TryGetPeilutFromInsertObject(SidurDM oSidur, PeilutDM oPeilut, COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimIns, out OBJ_PEILUT_OVDIM oObjPeilutOvdimIns)
        {
            oObjPeilutOvdimIns = null; // new OBJ_PEILUT_OVDIM();

            for (int i = 0; i <= oCollPeilutOvdimIns.Count - 1; i++)
            {
                if ((oCollPeilutOvdimIns.Value[i].MISPAR_SIDUR == oSidur.iMisparSidur) && (oCollPeilutOvdimIns.Value[i].SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
                    && (oCollPeilutOvdimIns.Value[i].SHAT_YETZIA == oPeilut.dFullShatYetzia) && (oCollPeilutOvdimIns.Value[i].MISPAR_KNISA == oPeilut.iMisparKnisa))
                {
                    oObjPeilutOvdimIns = oCollPeilutOvdimIns.Value[i];
                    return true;
                }
            }
            return false;
        }

        protected OBJ_PEILUT_OVDIM GetUpdPeilutObject(int iSidurIndex, PeilutDM oPeilut, ShinuyInputData inputData, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd, out SourceObj SourceObject)
        {
            OBJ_PEILUT_OVDIM oObjPeilutOvdimUpd = null;
            SourceObject = SourceObj.Update;
            try
            {
                SidurDM oSidur = (SidurDM)inputData.htEmployeeDetails[iSidurIndex];
                if (TryGetPeilutFromUpdObject(oSidur, oPeilut, inputData.oCollPeilutOvdimUpdRecorder, out oObjPeilutOvdimUpd))
                {
                    SourceObject = SourceObj.Update;
                    return oObjPeilutOvdimUpd;
                }

                if (TryGetPeilutFromInsertObject(oSidur, oPeilut, inputData.oCollPeilutOvdimIns, out oObjPeilutOvdimUpd))
                {
                    SourceObject = SourceObj.Insert;
                    return oObjPeilutOvdimUpd;
                }
                
                oObjPeilutOvdimUpd = new OBJ_PEILUT_OVDIM();
                InsertToObjPeilutOvdimForUpdate(oPeilut, oObjSidurimOvdimUpd,oObjPeilutOvdimUpd, inputData.UserId,false);

                ModificationRecorder<OBJ_PEILUT_OVDIM> recorder = new ModificationRecorder<OBJ_PEILUT_OVDIM>(oObjPeilutOvdimUpd, true);
                inputData.oCollPeilutOvdimUpdRecorder.Add(recorder);
                SourceObject = SourceObj.Update;

                InsertToObjPeilutOvdimForUpdate(oPeilut, oObjSidurimOvdimUpd,oObjPeilutOvdimUpd, inputData.UserId, true);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oObjPeilutOvdimUpd;
        }

        protected void InsertToObjPeilutOvdimForUpdate(PeilutDM oPeilut, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd,OBJ_PEILUT_OVDIM oObjPeilutOvdimUpd, int? userid, bool insertNewFields = true)
        {
           // OBJ_PEILUT_OVDIM oObjPeilutOvdimUpd = new OBJ_PEILUT_OVDIM();
            try
            {
                oObjPeilutOvdimUpd.MISPAR_SIDUR = oObjSidurimOvdimUpd.MISPAR_SIDUR;
                oObjPeilutOvdimUpd.MISPAR_ISHI = oObjSidurimOvdimUpd.MISPAR_ISHI;
                oObjPeilutOvdimUpd.SHAT_HATCHALA_SIDUR = oObjSidurimOvdimUpd.SHAT_HATCHALA;       
                oObjPeilutOvdimUpd.SHAT_YETZIA = oPeilut.dFullShatYetzia;
                oObjPeilutOvdimUpd.NEW_SHAT_YETZIA = oPeilut.dFullShatYetzia;
                oObjPeilutOvdimUpd.TAARICH = oObjSidurimOvdimUpd.TAARICH.Date;
                oObjPeilutOvdimUpd.MISPAR_KNISA = oPeilut.iMisparKnisa;
                oObjPeilutOvdimUpd.MISPAR_VISA = oPeilut.lMisparVisa;
                oObjPeilutOvdimUpd.MAKAT_NESIA = oPeilut.lMakatNesia;
                oObjPeilutOvdimUpd.MISPAR_MATALA = oPeilut.lMisparMatala;
                oObjPeilutOvdimUpd.BITUL_O_HOSAFA = oPeilut.iBitulOHosafa;
                oObjPeilutOvdimUpd.KM_VISA = oPeilut.iKmVisa;
                oObjPeilutOvdimUpd.KOD_SHINUY_PREMIA = oPeilut.iKodShinuyPremia;
                oObjPeilutOvdimUpd.MISPAR_SIDURI_OTO = oPeilut.lMisparSiduriOto;
                if (userid.HasValue)
                    oObjPeilutOvdimUpd.MEADKEN_ACHARON = userid.Value;

                if (insertNewFields)
                {
                    oObjPeilutOvdimUpd.NEW_SHAT_HATCHALA_SIDUR = oObjSidurimOvdimUpd.NEW_SHAT_HATCHALA;
                    oObjPeilutOvdimUpd.NEW_MISPAR_SIDUR = oObjSidurimOvdimUpd.NEW_MISPAR_SIDUR;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

           // return oObjPeilutOvdimUpd;
        }

        protected void InsertToObjPeilutOvdimForInsert(SidurDM oSidur, OBJ_PEILUT_OVDIM oObjPeilutOvdimIns, int? userid)
        {
            try
            {

                oObjPeilutOvdimIns.MISPAR_ISHI = oSidur.iMisparIshi;
                oObjPeilutOvdimIns.TAARICH = oSidur.dSidurDate;
                oObjPeilutOvdimIns.MISPAR_SIDUR = oSidur.iMisparSidur;
                oObjPeilutOvdimIns.SHAT_HATCHALA_SIDUR = oSidur.dFullShatHatchala;
                oObjPeilutOvdimIns.MISPAR_KNISA = 0;
                if (userid.HasValue)
                    oObjPeilutOvdimIns.MEADKEN_ACHARON = userid.Value;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected OBJ_SIDURIM_OVDIM InsertToObjSidurimOvdimForDelete(SidurDM oSidur, ShinuyInputData inputData)
        {
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimDel = new OBJ_SIDURIM_OVDIM();

            try
            {

                for (int i = 0; i <= inputData.oCollSidurimOvdimUpdRecorder.Count - 1; i++)
                {
                    if ((inputData.oCollSidurimOvdimUpdRecorder[i].ContainedItem.NEW_MISPAR_SIDUR == oSidur.iMisparSidur) && (inputData.oCollSidurimOvdimUpdRecorder[i].ContainedItem.NEW_SHAT_HATCHALA == oSidur.dFullShatHatchala) && inputData.oCollSidurimOvdimUpdRecorder[i].ContainedItem.UPDATE_OBJECT == 1)
                    {
                        inputData.oCollSidurimOvdimUpdRecorder.RemoveAt(i);
                    }
                }


                for (int i = 0; i <= inputData.oCollSidurimOvdimIns.Count - 1; i++)
                {
                    if ((inputData.oCollSidurimOvdimIns.Value[i].MISPAR_SIDUR == oSidur.iMisparSidur) && (inputData.oCollSidurimOvdimIns.Value[i].SHAT_HATCHALA == oSidur.dFullShatHatchala))
                    {
                        inputData.oCollSidurimOvdimIns.RemoveAt(i);
                    }
                }


                oObjSidurimOvdimDel.MISPAR_SIDUR = oSidur.iMisparSidur;
                oObjSidurimOvdimDel.MISPAR_ISHI = oSidur.iMisparIshi;
                oObjSidurimOvdimDel.SHAT_HATCHALA = oSidur.dFullShatHatchala;//DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatHatchala));
                oObjSidurimOvdimDel.TAARICH = oSidur.dSidurDate;
                if (inputData.UserId.HasValue)
                    oObjSidurimOvdimDel.MEADKEN_ACHARON = inputData.UserId.Value;
                oObjSidurimOvdimDel.BITUL_O_HOSAFA = oSidur.iBitulOHosafa;
                oObjSidurimOvdimDel.UPDATE_OBJECT = 0;

                return oObjSidurimOvdimDel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected OBJ_PEILUT_OVDIM InsertToObjPeilutOvdimForDelete(PeilutDM oPeilut, SidurDM oSidur, ShinuyInputData inputData)
        {
            OBJ_PEILUT_OVDIM oObjPeilutOvdimDel = new OBJ_PEILUT_OVDIM();
            OBJ_PEILUT_OVDIM oObjPeilutOvdimUpd = null;
            int iMisparSidur = oPeilut.iPeilutMisparSidur;
            DateTime dShatHatchala = oSidur.dFullShatHatchala;
            DateTime dFullShatYetzia = oPeilut.dFullShatYetzia;
            try
            {
                for (int i = 0; i <= inputData.oCollPeilutOvdimUpdRecorder.Count - 1; i++)
                {
                    if ((inputData.oCollPeilutOvdimUpdRecorder[i].ContainedItem.NEW_MISPAR_SIDUR == oSidur.iMisparSidur) && (inputData.oCollPeilutOvdimUpdRecorder[i].ContainedItem.NEW_SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
                        && (inputData.oCollPeilutOvdimUpdRecorder[i].ContainedItem.NEW_SHAT_YETZIA == oPeilut.dFullShatYetzia) && (inputData.oCollPeilutOvdimUpdRecorder[i].ContainedItem.MISPAR_KNISA == oPeilut.iMisparKnisa) && inputData.oCollPeilutOvdimUpdRecorder[i].ContainedItem.UPDATE_OBJECT == 1)
                    {
                        oObjPeilutOvdimUpd = inputData.oCollPeilutOvdimUpdRecorder[i].ContainedItem;
                        iMisparSidur = oObjPeilutOvdimUpd.MISPAR_SIDUR;
                        dShatHatchala = oObjPeilutOvdimUpd.SHAT_HATCHALA_SIDUR;
                        dFullShatYetzia = oObjPeilutOvdimUpd.SHAT_YETZIA;
                        inputData.oCollPeilutOvdimUpdRecorder.RemoveAt(i);
                        break;
                    }
                }
                if (oObjPeilutOvdimUpd == null)
                {
                    for (int i = 0; i <= inputData.oCollPeilutOvdimIns.Count - 1; i++)
                    {
                        if ((inputData.oCollPeilutOvdimIns.Value[i].MISPAR_SIDUR == oSidur.iMisparSidur) && (inputData.oCollPeilutOvdimIns.Value[i].SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
                            && (inputData.oCollPeilutOvdimIns.Value[i].SHAT_YETZIA == oPeilut.dFullShatYetzia) && (inputData.oCollPeilutOvdimIns.Value[i].MISPAR_KNISA == oPeilut.iMisparKnisa))
                        {
                            oObjPeilutOvdimUpd = inputData.oCollPeilutOvdimIns.Value[i];
                            iMisparSidur = oObjPeilutOvdimUpd.MISPAR_SIDUR;
                            dShatHatchala = oObjPeilutOvdimUpd.SHAT_HATCHALA_SIDUR;
                            dFullShatYetzia = oObjPeilutOvdimUpd.SHAT_YETZIA;
                            inputData.oCollPeilutOvdimIns.RemoveAt(i);
                            break;
                        }
                    }
                }

                oObjPeilutOvdimDel.MISPAR_ISHI = oSidur.iMisparIshi;
                oObjPeilutOvdimDel.MISPAR_SIDUR = iMisparSidur;
                oObjPeilutOvdimDel.TAARICH = oPeilut.dCardDate;
                oObjPeilutOvdimDel.SHAT_HATCHALA_SIDUR = dShatHatchala;
                oObjPeilutOvdimDel.SHAT_YETZIA = dFullShatYetzia;
                oObjPeilutOvdimDel.MISPAR_KNISA = oPeilut.iMisparKnisa;
                oObjPeilutOvdimDel.MAKAT_NESIA = oPeilut.lMakatNesia;
                oObjPeilutOvdimDel.BITUL_O_HOSAFA = oPeilut.iBitulOHosafa;
                if (inputData.UserId.HasValue)
                    oObjPeilutOvdimDel.MEADKEN_ACHARON = inputData.UserId.Value;

                return oObjPeilutOvdimDel;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void CopyPeilutToObj(OBJ_PEILUT_OVDIM oObjPeilutOvdimIns, PeilutDM oPeilut)
        {
            oObjPeilutOvdimIns.OTO_NO = oPeilut.lOtoNo;
            oObjPeilutOvdimIns.DAKOT_BAFOAL = oPeilut.iDakotBafoal;
            oObjPeilutOvdimIns.KISUY_TOR = oPeilut.iKisuyTor;
            if (oPeilut.iKmVisa > 0)
                oObjPeilutOvdimIns.KM_VISA = oPeilut.iKmVisa;
            oObjPeilutOvdimIns.MISPAR_SIDURI_OTO = oPeilut.lMisparSiduriOto;
            oObjPeilutOvdimIns.MISPAR_VISA = oPeilut.lMisparVisa;
            if (!string.IsNullOrEmpty(oPeilut.sSnifTnua))
                oObjPeilutOvdimIns.SNIF_TNUA = int.Parse(oPeilut.sSnifTnua);
            oObjPeilutOvdimIns.MISPAR_MATALA = oPeilut.lMisparMatala;
            oObjPeilutOvdimIns.SHILUT_NETZER = oPeilut.sShilutNetzer;
            if (oPeilut.dShatYetziaNetzer != DateTime.MinValue)
                oObjPeilutOvdimIns.SHAT_YETZIA_NETZER = oPeilut.dShatYetziaNetzer;
            if (oPeilut.dShatBhiratNesiaNetzer != DateTime.MinValue)
                oObjPeilutOvdimIns.SHAT_BHIRAT_NESIA_NETZER = oPeilut.dShatBhiratNesiaNetzer;
            oObjPeilutOvdimIns.OTO_NO_NETZER = oPeilut.lOtoNoNetzer;
            oObjPeilutOvdimIns.MISPAR_SIDUR_NETZER = oPeilut.iMisparSidurNetzer;
            oObjPeilutOvdimIns.HEARA = oPeilut.sHeara;
            oObjPeilutOvdimIns.MIKUM_BHIRAT_NESIA_NETZER = oPeilut.sMikumBhiratNesiaNetzer;
            oObjPeilutOvdimIns.KOD_SHINUY_PREMIA = oPeilut.iKodShinuyPremia;
            if (oPeilut.bImutNetzer)
                oObjPeilutOvdimIns.IMUT_NETZER = 1;
            oObjPeilutOvdimIns.MAKAT_NETZER = oPeilut.lMakatNetzer;
            oObjPeilutOvdimIns.TEUR_NESIA = oPeilut.sMakatDescription;
        }

        protected PeilutDM CreatePeilut(int iMisparIshi, DateTime dCardDate, PeilutDM oPeilut, long makat, DataTable dtTmpMeafyeneyElements)
        {
            var manager = ServiceLocator.Current.GetInstance<IPeilutManager>();
            return manager.CreatePeilutFromOldPeilut(iMisparIshi, dCardDate, oPeilut, makat, dtTmpMeafyeneyElements);
        }

        protected PeilutDM CreatePeilut(int iMisparIshi, DateTime dCardDate, OBJ_PEILUT_OVDIM oObjPeilutOvdimIns, DataTable dtTmpMeafyeneyElements)
        {
            var manager = ServiceLocator.Current.GetInstance<IPeilutManager>();
            return manager.CreateClsPeilut(iMisparIshi, dCardDate, oObjPeilutOvdimIns, dtTmpMeafyeneyElements);
        }

        //GetMutamut
        protected bool HaveIsurShaotNosafotLeMutam(int iKodMutamut)
        {
            DataRow[] dr;
            int iIsurShaotNosafot = 0;
            //מקבל קוד מותאמות ומחזיר אם יש איסור של שעות נוספות
            try
            {
                dr = _container.Resolve<IKDSCacheManager>().GetCacheItem<DataTable>(CachedItems.Mutamut).Select(string.Concat("kod_mutamut=", iKodMutamut));
                if (dr.Length > 0)
                {
                    iIsurShaotNosafot = string.IsNullOrEmpty(dr[0]["isur_shaot_nosafot"].ToString()) ? 0 : int.Parse(dr[0]["isur_shaot_nosafot"].ToString());
                }
                return iIsurShaotNosafot > 0;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        protected void GetOvedShatHatchalaGmar(DateTime dSidurShatGmar, MeafyenimDM oMeafyeneyOved, SidurDM curSidur, ShinuyInputData inputData,
                                         ref DateTime dShatHatchalaLetashlum, ref DateTime dShatGmarLetashlum, out bool bFromMeafyenHatchala, out bool bFromMeafyenGmar)
        {
            /*  יום חול 
            שעת התחלה – מאפיין 03
            שעת גמר       - מאפיין 04
            יום שישי/ערב חג 
            שעת התחלה – מאפיין 05
            שעת גמר       - מאפיין 06
            יום שבת/שבתון 
            שעת התחלה – מאפיין 07
            שעת גמר       - מאפיין 08*/

            try
            {
                bFromMeafyenHatchala = false;
                bFromMeafyenGmar = false;
                //??
                //if (inputData.OvedDetails == null)
                //{
                //    //??
                //    SetOvedYomAvodaDetails(inputData.iMisparIshi, inputData.CardDate);
                //}

                DateTime dShatHatchalaSidur = curSidur.dFullShatHatchala;

                if (dShatHatchalaSidur == DateTime.MinValue)
                {
                    dShatHatchalaSidur = inputData.CardDate;
                }

                if (dSidurShatGmar == DateTime.MinValue)
                {
                    dSidurShatGmar = inputData.CardDate;
                }


                //קביעת מאפיינים מפעילים
                if ((inputData.OvedDetails.iIsuk == 122 || inputData.OvedDetails.iIsuk == 123 || inputData.OvedDetails.iIsuk == 124 || inputData.OvedDetails.iIsuk == 127))
                {
                    GetMeafyeneyMafilim(curSidur.sShabaton, dShatHatchalaSidur, dSidurShatGmar, inputData, out  bFromMeafyenHatchala, out  bFromMeafyenGmar, ref dShatHatchalaLetashlum, ref dShatGmarLetashlum);
                }
                else
                {
                    //יום שבת/שבתון
                    if ((curSidur.sSidurDay == enDay.Shabat.GetHashCode().ToString()) || (curSidur.sShabaton == "1"))
                    {
                        if (oMeafyeneyOved.IsMeafyenExist(7))
                        {
                            dShatHatchalaLetashlum = DateHelper.ConvertMefyenShaotValid(dShatHatchalaSidur, oMeafyeneyOved.GetMeafyen(7).Value);
                            bFromMeafyenHatchala = true;
                        }
                        else
                        {//אם אין מאפיין, נקבע את שעת הסידור כברירת מחדל
                            dShatHatchalaLetashlum = curSidur.dFullShatHatchala;
                        }
                        if (oMeafyeneyOved.IsMeafyenExist(8))
                        {
                            dShatGmarLetashlum = DateHelper.ConvertMefyenShaotValid(dShatHatchalaSidur, oMeafyeneyOved.GetMeafyen(8).Value);
                            bFromMeafyenGmar = true;
                        }
                        else
                        {//אם אין מאפיין, נקבע את שעת הסידור כברירת מחדל
                            dShatGmarLetashlum = dSidurShatGmar;
                        }
                    }
                    else
                    {
                        //יום שישי או ערב חג
                        if ((curSidur.sSidurDay == enDay.Shishi.GetHashCode().ToString()))
                        {
                            if (oMeafyeneyOved.IsMeafyenExist(5))
                            {
                                dShatHatchalaLetashlum = DateHelper.ConvertMefyenShaotValid(dShatHatchalaSidur, oMeafyeneyOved.GetMeafyen(5).Value);
                                bFromMeafyenHatchala = true;
                            }
                            else
                            {//אם אין מאפיין, נקבע את שעת הסידור כברירת מחדל
                                dShatHatchalaLetashlum = curSidur.dFullShatHatchala;
                            }
                            if (oMeafyeneyOved.IsMeafyenExist(6))
                            {
                                dShatGmarLetashlum = DateHelper.ConvertMefyenShaotValid(dSidurShatGmar, oMeafyeneyOved.GetMeafyen(6).Value);
                                bFromMeafyenGmar = true;
                            }
                            else
                            {//אם אין מאפיין, נקבע את שעת הסידור כברירת מחדל
                                dShatGmarLetashlum = dSidurShatGmar;
                            }
                        }
                        else
                        {   //יום חול
                            if (oMeafyeneyOved.IsMeafyenExist(3))
                            {
                                dShatHatchalaLetashlum = DateHelper.ConvertMefyenShaotValid(dShatHatchalaSidur, oMeafyeneyOved.GetMeafyen(3).Value);
                                bFromMeafyenHatchala = true;
                            }
                            else
                            {//אם אין מאפיין, נקבע את שעת הסידור כברירת מחדל
                                dShatHatchalaLetashlum = curSidur.dFullShatHatchala;
                            }

                            if (oMeafyeneyOved.IsMeafyenExist(4))
                            {
                                dShatGmarLetashlum = DateHelper.ConvertMefyenShaotValid(dShatHatchalaSidur, oMeafyeneyOved.GetMeafyen(4).Value);
                                bFromMeafyenGmar = true;
                            }
                            else
                            {//אם אין מאפיין, נקבע את שעת הסידור כברירת מחדל
                                dShatGmarLetashlum = dSidurShatGmar;
                            }
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetMeafyeneyMafilim(string sShabaton, DateTime dShatHatchalaSidur, DateTime dSidurShatGmar, ShinuyInputData inputData, out bool bFromMeafyenHatchala, out bool bFromMeafyenGmar, ref DateTime dShatHatchalaLetashlum, ref DateTime dShatGmarLetashlum)
        {
            SidurDM firstTafkidSidur = inputData.htEmployeeDetails.Values.Cast<SidurDM>().ToList().FirstOrDefault(sidur => sidur.iMisparSidur == 99001);
            SidurDM lastTafkidSidur = inputData.htEmployeeDetails.Values.Cast<SidurDM>().ToList().LastOrDefault(sidur => sidur.iMisparSidur == 99001);

            bFromMeafyenHatchala = false;
            bFromMeafyenGmar = false;

            if (firstTafkidSidur != null && lastTafkidSidur != null)
            {
                if (inputData.oMeafyeneyOved.IsMeafyenExist(3))
                {
                    dShatHatchalaLetashlum = DateHelper.ConvertMefyenShaotValid(inputData.CardDate, inputData.oMeafyeneyOved.GetMeafyen(3).Value);
                    bFromMeafyenHatchala = true;
                }
                if (inputData.oMeafyeneyOved.IsMeafyenExist(4))
                {
                    dShatGmarLetashlum = DateHelper.ConvertMefyenShaotValid(inputData.CardDate, inputData.oMeafyeneyOved.GetMeafyen(4).Value);
                    bFromMeafyenGmar = true;
                }

                int iSugYom = DateHelper.GetSugYom(inputData.iMisparIshi, inputData.CardDate, inputData.YamimMeyuchadim, inputData.OvedDetails.iKodSectorIsuk, inputData.SugeyYamimMeyuchadim, inputData.oMeafyeneyOved.GetMeafyen(56).IntValue);
                if (iSugYom >= enSugYom.Chol.GetHashCode() && iSugYom < enSugYom.Shishi.GetHashCode())
                {
                    int iSugMishmeret = DateHelper.GetSugMishmeret(inputData.iMisparIshi, inputData.CardDate, inputData.iSugYom, firstTafkidSidur.dFullShatHatchala, lastTafkidSidur.dFullShatGmar, inputData.oParam);
                    SetShaotYomCholMafilim(dShatHatchalaSidur, dSidurShatGmar, inputData, iSugMishmeret, ref bFromMeafyenHatchala, ref bFromMeafyenGmar, ref dShatHatchalaLetashlum, ref dShatGmarLetashlum);
                }
                else
                {
                    SetShaotShishiShabatMafilim(sShabaton, dShatHatchalaSidur, dSidurShatGmar, inputData.oMeafyeneyOved, ref bFromMeafyenHatchala, ref bFromMeafyenGmar, ref dShatHatchalaLetashlum, ref dShatGmarLetashlum, iSugYom);
                }
            }
        }

        private void SetShaotShishiShabatMafilim(string sShabaton, DateTime dShatHatchalaSidur, DateTime dSidurShatGmar, MeafyenimDM oMeafyeneyOved, ref bool bFromMeafyenHatchala, ref bool bFromMeafyenGmar, ref DateTime dShatHatchalaLetashlum, ref DateTime dShatGmarLetashlum, int iSugYom)
        {
            if (iSugYom == enSugYom.Shishi.GetHashCode() && iSugYom < enSugYom.Shabat.GetHashCode())
            {
                if (oMeafyeneyOved.IsMeafyenExist(27))
                {
                    dShatHatchalaLetashlum = DateHelper.ConvertMefyenShaotValid(dShatHatchalaSidur, oMeafyeneyOved.GetMeafyen(27).Value);
                    bFromMeafyenHatchala = true;
                }
                if (oMeafyeneyOved.IsMeafyenExist(28))
                {
                    dShatGmarLetashlum = DateHelper.ConvertMefyenShaotValid(dSidurShatGmar, oMeafyeneyOved.GetMeafyen(28).Value);
                    bFromMeafyenGmar = true;
                }
            }
            else if ((iSugYom >= enSugYom.Shabat.GetHashCode() && iSugYom < enSugYom.Rishon.GetHashCode()) || sShabaton == "1")
            {
                if (oMeafyeneyOved.IsMeafyenExist(7))
                {
                    dShatHatchalaLetashlum = DateHelper.ConvertMefyenShaotValid(dShatHatchalaSidur, oMeafyeneyOved.GetMeafyen(7).Value);
                    bFromMeafyenHatchala = true;
                }
                if (oMeafyeneyOved.IsMeafyenExist(8))
                {
                    dShatGmarLetashlum = DateHelper.ConvertMefyenShaotValid(dSidurShatGmar, oMeafyeneyOved.GetMeafyen(8).Value);
                    bFromMeafyenGmar = true;
                }
            }
        }

        private void SetShaotYomCholMafilim(DateTime dShatHatchalaSidur, DateTime dSidurShatGmar, ShinuyInputData inputData, int iSugMishmeret, ref bool bFromMeafyenHatchala, ref bool bFromMeafyenGmar, ref DateTime dShatHatchalaLetashlum, ref DateTime dShatGmarLetashlum)
        {

            switch (iSugMishmeret)
            {
                case 1:// בוקר                            
                    break;

                case 2:// צהריים
                    {
                        if (inputData.oMeafyeneyOved.IsMeafyenExist(23))
                        {
                            dShatHatchalaLetashlum = DateHelper.ConvertMefyenShaotValid(dShatHatchalaSidur, inputData.oMeafyeneyOved.GetMeafyen(23).Value);
                            bFromMeafyenHatchala = true;
                        }
                        if (inputData.oMeafyeneyOved.IsMeafyenExist(24))
                        {
                            dShatGmarLetashlum = DateHelper.ConvertMefyenShaotValid(dSidurShatGmar, inputData.oMeafyeneyOved.GetMeafyen(24).Value);
                            bFromMeafyenGmar = true;
                        }
                        break;
                    }


                case 3:// לילה
                    {
                        if (inputData.oMeafyeneyOved.IsMeafyenExist(25))
                        {
                            dShatHatchalaLetashlum = DateHelper.ConvertMefyenShaotValid(dShatHatchalaSidur, inputData.oMeafyeneyOved.GetMeafyen(25).Value);
                            bFromMeafyenHatchala = true;
                        }
                        if (inputData.oMeafyeneyOved.IsMeafyenExist(26))
                        {
                            dShatGmarLetashlum = DateHelper.ConvertMefyenShaotValid(dSidurShatGmar, inputData.oMeafyeneyOved.GetMeafyen(26).Value);
                            bFromMeafyenGmar = true;
                        }
                        break;
                    }

            }
        }
        protected bool isPeilutMashmautit(PeilutDM oPeilut)
        {
            if (oPeilut.iMakatType == enMakatType.mVisa.GetHashCode() || oPeilut.iMakatType == enMakatType.mKavShirut.GetHashCode() || oPeilut.iMakatType == enMakatType.mNamak.GetHashCode() ||
                (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && (oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != enElementHachanatMechona.Element701.GetHashCode().ToString() && oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != enElementHachanatMechona.Element712.GetHashCode().ToString() && oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != enElementHachanatMechona.Element711.GetHashCode().ToString())
                                && (oPeilut.iElementLeShatGmar > 0 || oPeilut.iElementLeShatGmar == -1)))
                return true;
            else return false;
        }

        protected bool isElemntLoMashmauti(PeilutDM oPeilut)
        {
            if (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && oPeilut.iElementLeShatGmar == 0 &&
                oPeilut.lMakatNesia.ToString().PadLeft(8).Substring(0, 3) != "700")
                return true;
            else return false;
        }

        protected int GetMeshechPeilutHachnatMechona(int iIndexSidur, PeilutDM oPeilut, SidurDM curSidur, ShinuyInputData inputData, ref bool bUsedMazanTichnunInSidur)
        {
            int iMeshech = 0;
            DataRow[] drSugSidur;
            bool bSidurNahagutPrev;

            try
            {
                var sidurManager = _container.Resolve<ISidurManager>();
                drSugSidur = sidurManager.GetOneSugSidurMeafyen(curSidur.iSugSidurRagil, inputData.CardDate);
                bool bSidurNahagut = sidurManager.IsSidurNahagut(drSugSidur, curSidur);
                if (iIndexSidur > 0)
                {
                    drSugSidur = sidurManager.GetOneSugSidurMeafyen(((SidurDM)inputData.htEmployeeDetails[iIndexSidur - 1]).iSugSidurRagil, inputData.CardDate);
                    bSidurNahagutPrev = sidurManager.IsSidurNahagut(drSugSidur, ((SidurDM)inputData.htEmployeeDetails[iIndexSidur - 1]));
                }
                else bSidurNahagutPrev = false;

                //אם הערך בשדה 0<Dakot_bafoal אז יש לקחת את הערך משדה זה  
                if (oPeilut.iDakotBafoal > 0 || (oPeilut.iMakatType == enMakatType.mKavShirut.GetHashCode() && oPeilut.iMisparKnisa > 0))
                {
                    iMeshech = oPeilut.iDakotBafoal;
                }
                else
                {
                    //משך פעילות מסוג אלמנט  - הערך בפוזיציות 4-6 של המק"ט. 
                    if (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                    {
                        iMeshech = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));
                    }
                    else
                    { //סידור שהוא יחיד או ראשון ביום
                        //סידור ראשון צריך להיגמר לפי הגדרה לתכנון.

                        if ((iIndexSidur == 0) || inputData.htEmployeeDetails.Values.Count == 1 || (bSidurNahagut && !bSidurNahagutPrev))
                        {
                            //אלמנט ריקה  (מאפיין 23): מז"ן תשלום (גמר) - הערך שהוקלד במק"ט. מז"ן תכנון  - מז"ן תשלום * פרמטר 43.
                            if (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && !string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                                iMeshech = int.Parse(Math.Round(oPeilut.iMazanTashlum * inputData.oParam.fFactorNesiotRekot).ToString());
                            else
                                iMeshech = oPeilut.iMazanTichnun;
                        }
                        else if (bSidurNahagut && bSidurNahagutPrev)
                        {
                            iMeshech = GetMeshech(iIndexSidur, oPeilut, curSidur, inputData, ref bUsedMazanTichnunInSidur);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return iMeshech;
        }

        private int GetMeshech(int iIndexSidur, PeilutDM oPeilut, SidurDM curSidur, ShinuyInputData inputData, ref bool bUsedMazanTichnunInSidur)
        {
            int iMeshech = 0;
            SidurDM oPrevSidur = (SidurDM)inputData.htEmployeeDetails[iIndexSidur - 1];
            try
            {
                //סידור שאינו יחיד או ראשון ביום צריך להיגמר לפי זמן לגמר או לתכנון, בהתאם למקרה:
                //1.	אם יש פער של עד 60 דקות משעת התחלת הסידור הבא - יש לחשב לפי הגדרה גמר (תשלום) 
                if (curSidur.dFullShatHatchala.Subtract(oPrevSidur.dFullShatGmar).TotalMinutes <= 60)
                {
                    //אלמנט ריקה  (מאפיין 23): מז"ן תשלום (גמר) - הערך שהוקלד במק"ט. מז"ן תכנון  - מז"ן תשלום * פרמטר 43.
                    if (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && !string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                        iMeshech = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));
                    else
                        iMeshech = oPeilut.iMazanTashlum;
                }
                else
                {
                    if (!inputData.bUsedMazanTichnun)
                    {
                        //אלמנט ריקה  (מאפיין 23): מז"ן תשלום (גמר) - הערך שהוקלד במק"ט. מז"ן תכנון  - מז"ן תשלום * פרמטר 43.
                        if (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && !string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                            iMeshech = int.Parse(Math.Round(oPeilut.iMazanTashlum * inputData.oParam.fFactorNesiotRekot).ToString());
                        else
                            //2.	אם יש פער גדול מ- 60 דקות משעת התחלה של הסידור הבא וזו הפעם הראשונה שסידור שאינו יחיד/ראשון ביום צריך להיגמר לפי הגדרה לתכנון   - יש לחשב לפי זמן לתכנון. 
                            iMeshech = oPeilut.iMazanTichnun;
                        bUsedMazanTichnunInSidur = true;

                    }
                    else
                    {
                        //אלמנט ריקה  (מאפיין 23): מז"ן תשלום (גמר) - הערך שהוקלד במק"ט. מז"ן תכנון  - מז"ן תשלום * פרמטר 43.
                        if (oPeilut.iMakatType == enMakatType.mElement.GetHashCode() && !string.IsNullOrEmpty(oPeilut.sElementNesiaReka))
                            iMeshech = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));
                        else
                            //3.	אם יש פער גדול מ- 60 דקות מהסידור הבא וזו אינה הפעם הראשונה שסידור שאינו יחיד/ראשון ביום צריך להיגמר לפי זמן לתכנון - יש לחשב לפי הגדרה לגמר (תשלום)
                            iMeshech = oPeilut.iMazanTashlum;
                    }

                }
                return iMeshech;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected bool CheckPeilutObjectDelete(int iSidurIndex, int iPeilutIndex, ShinuyInputData inputData)
        {
            OBJ_PEILUT_OVDIM oObjPeilutOvdim = new OBJ_PEILUT_OVDIM();
            PeilutDM oPeilut;
            SidurDM oSidur;
            bool bExist = false;
            try
            {
                //נמצא את הפעילות באובייקט פעילויות לעדכון
                oSidur = (SidurDM)inputData.htEmployeeDetails[iSidurIndex];
                oPeilut = (PeilutDM)oSidur.htPeilut[iPeilutIndex];
                for (int i = 0; i <= inputData.oCollPeilutOvdimDel.Count - 1; i++)
                {
                    if ((inputData.oCollPeilutOvdimDel.Value[i].NEW_MISPAR_SIDUR == oSidur.iMisparSidur) && (inputData.oCollPeilutOvdimDel.Value[i].NEW_SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
                        && (inputData.oCollPeilutOvdimDel.Value[i].NEW_SHAT_YETZIA == oPeilut.dFullShatYetzia) && (inputData.oCollPeilutOvdimDel.Value[i].MISPAR_KNISA == oPeilut.iMisparKnisa))
                    {
                        bExist = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bExist;
        }
        protected void UpdateIdkunRashemet(int iMisparSidur, DateTime dShatHatchala, int iMisparKnisa, DateTime dShatYetzia, DateTime dShatYetziaNew, int flag, ShinuyInputData inputData)
        {
            DataRow[] drIdkunim;
            OBJ_IDKUN_RASHEMET ObjIdkunRashemet;

            try
            {
                //drIdkunim = _dtIdkuneyRashemet.Select("MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime') AND MISPAR_KNISA=" + iMisparKnisa + " AND SHAT_YETZIA=Convert('" + dShatYetzia.ToString() + "', 'System.DateTime')");
                if (flag > 0)
                    drIdkunim = inputData.IdkuneyRashemet.Select("MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime') AND MISPAR_KNISA=" + iMisparKnisa + " AND SHAT_YETZIA=Convert('" + dShatYetzia.ToString() + "', 'System.DateTime') and update_machine=1");
                else
                    drIdkunim = inputData.IdkuneyRashemet.Select("MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime') AND MISPAR_KNISA=" + iMisparKnisa + " AND SHAT_YETZIA=Convert('" + dShatYetzia.ToString() + "', 'System.DateTime') and update_machine is null");
                for (int i = 0; i < drIdkunim.Length; i++)
                {
                    ObjIdkunRashemet = FillIdkunRashemet(drIdkunim[i], inputData);
                    ObjIdkunRashemet.NEW_SHAT_YETZIA = dShatYetziaNew;
                    drIdkunim[i]["SHAT_YETZIA"] = dShatYetziaNew;
                    if (flag == 0)
                        drIdkunim[i]["update_machine"] = "1";
                    inputData.oCollIdkunRashemet.Add(ObjIdkunRashemet);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void UpdateIdkunRashemet(int iMisparSidur, DateTime dShatHatchala, int iMisparKnisa, DateTime dShatYetzia, DateTime dShatYetziaNew, ShinuyInputData inputData)
        {
            DataRow[] drIdkunim;
            OBJ_IDKUN_RASHEMET ObjIdkunRashemet;

            try
            {

                drIdkunim = inputData.IdkuneyRashemet.Select("MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime') AND MISPAR_KNISA=" + iMisparKnisa + " AND SHAT_YETZIA=Convert('" + dShatYetzia.ToString() + "', 'System.DateTime')");
                for (int i = 0; i < drIdkunim.Length; i++)
                {
                    ObjIdkunRashemet = FillIdkunRashemet(drIdkunim[i], inputData);
                    ObjIdkunRashemet.NEW_SHAT_YETZIA = dShatYetziaNew;
                    drIdkunim[i]["SHAT_YETZIA"] = dShatYetziaNew;
                    inputData.oCollIdkunRashemet.Add(ObjIdkunRashemet);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void UpdateIdkunRashemet(int iMisparSidur, DateTime dShatHatchala, DateTime dShatHatchalaNew, ShinuyInputData inputData)
        {
            DataRow[] drIdkunim;
            OBJ_IDKUN_RASHEMET ObjIdkunRashemet;

            try
            {
                drIdkunim = inputData.IdkuneyRashemet.Select("MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime')");
                for (int i = 0; i < drIdkunim.Length; i++)
                {
                    ObjIdkunRashemet = FillIdkunRashemet(drIdkunim[i], inputData);
                    ObjIdkunRashemet.NEW_SHAT_HATCHALA = dShatHatchalaNew;
                    drIdkunim[i]["SHAT_HATCHALA"] = dShatHatchalaNew;

                    inputData.oCollIdkunRashemet.Add(ObjIdkunRashemet);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private OBJ_IDKUN_RASHEMET FillIdkunRashemet(DataRow drIdkun, ShinuyInputData inputData)
        {
            OBJ_IDKUN_RASHEMET ObjIdkunRashemet = new OBJ_IDKUN_RASHEMET();
            ObjIdkunRashemet.UPDATE_OBJECT = 1;
            if (inputData.UserId.HasValue)
                ObjIdkunRashemet.GOREM_MEADKEN = inputData.UserId.Value;
            ObjIdkunRashemet.MISPAR_ISHI = inputData.iMisparIshi;
            ObjIdkunRashemet.MISPAR_SIDUR = int.Parse(drIdkun["MISPAR_SIDUR"].ToString());
            ObjIdkunRashemet.SHAT_HATCHALA = DateTime.Parse(drIdkun["SHAT_HATCHALA"].ToString());
            ObjIdkunRashemet.NEW_SHAT_HATCHALA = DateTime.Parse(drIdkun["SHAT_HATCHALA"].ToString()); ;
            ObjIdkunRashemet.TAARICH = inputData.CardDate;
            ObjIdkunRashemet.SHAT_YETZIA = DateTime.Parse(drIdkun["SHAT_YETZIA"].ToString());
            ObjIdkunRashemet.NEW_SHAT_YETZIA = DateTime.Parse(drIdkun["SHAT_YETZIA"].ToString());
            ObjIdkunRashemet.PAKAD_ID = int.Parse(drIdkun["PAKAD_ID"].ToString());
            ObjIdkunRashemet.MISPAR_KNISA = int.Parse(drIdkun["MISPAR_KNISA"].ToString());

            return ObjIdkunRashemet;
        }

        protected void UpdateApprovalErrors(int iMisparSidur, DateTime dShatHatchala, DateTime dShatHatchalaNew, ShinuyInputData inputData)
        {
            DataRow[] drIdkunim;
            OBJ_SHGIOT_MEUSHAROT ObjShgiotMeusharot;

            try
            {
                drIdkunim = inputData.ApprovalError.Select("MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime')");
                for (int i = 0; i < drIdkunim.Length; i++)
                {
                    ObjShgiotMeusharot = FillApprovalErrors(drIdkunim[i], inputData);
                    ObjShgiotMeusharot.NEW_SHAT_HATCHALA = dShatHatchalaNew;
                    drIdkunim[i]["SHAT_HATCHALA"] = dShatHatchalaNew;

                    inputData.oCollApprovalErrors.Add(ObjShgiotMeusharot);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private OBJ_SHGIOT_MEUSHAROT FillApprovalErrors(DataRow drIdkun, ShinuyInputData inputData)
        {
            OBJ_SHGIOT_MEUSHAROT ObjShgiotMeusharot = new OBJ_SHGIOT_MEUSHAROT();
            ObjShgiotMeusharot.MISPAR_ISHI = inputData.iMisparIshi;
            ObjShgiotMeusharot.MISPAR_SIDUR = int.Parse(drIdkun["MISPAR_SIDUR"].ToString());
            ObjShgiotMeusharot.SHAT_HATCHALA = DateTime.Parse(drIdkun["SHAT_HATCHALA"].ToString());
            ObjShgiotMeusharot.NEW_SHAT_HATCHALA = DateTime.Parse(drIdkun["SHAT_HATCHALA"].ToString()); ;
            ObjShgiotMeusharot.TAARICH = inputData.CardDate;
            ObjShgiotMeusharot.SHAT_YETZIA = DateTime.Parse(drIdkun["SHAT_YETZIA"].ToString());
            ObjShgiotMeusharot.NEW_SHAT_YETZIA = DateTime.Parse(drIdkun["SHAT_YETZIA"].ToString());
            ObjShgiotMeusharot.MISPAR_KNISA = int.Parse(drIdkun["MISPAR_KNISA"].ToString());
            ObjShgiotMeusharot.KOD_SHGIA = int.Parse(drIdkun["KOD_SHGIA"].ToString());

            return ObjShgiotMeusharot;
        }

        protected void UpdateApprovalErrors(int iMisparSidur, DateTime dShatHatchala, int iMisparKnisa, DateTime dShatYetzia, DateTime dShatYetziaNew, ShinuyInputData inputData)
        {
            DataRow[] drIdkunim;
            OBJ_SHGIOT_MEUSHAROT ObjShgiotMeusharot;

            try
            {
                drIdkunim = inputData.ApprovalError.Select("MISPAR_SIDUR=" + iMisparSidur + " AND SHAT_HATCHALA=Convert('" + dShatHatchala.ToString() + "', 'System.DateTime') AND MISPAR_KNISA=" + iMisparKnisa + " AND SHAT_YETZIA=Convert('" + dShatYetzia.ToString() + "', 'System.DateTime') ");
                for (int i = 0; i < drIdkunim.Length; i++)
                {
                    ObjShgiotMeusharot = FillApprovalErrors(drIdkunim[i], inputData);
                    ObjShgiotMeusharot.NEW_SHAT_YETZIA = dShatYetziaNew;
                    drIdkunim[i]["SHAT_YETZIA"] = dShatYetziaNew;

                    inputData.oCollApprovalErrors.Add(ObjShgiotMeusharot);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected OBJ_SIDURIM_OVDIM GetSidurOvdimObject(int iMisparSidur, DateTime dShatHatchala, ShinuyInputData inputData)
        {
            //מביא את הסידור לפי מפתח האינדקס
            OBJ_SIDURIM_OVDIM oObjSidurimOvdim = new OBJ_SIDURIM_OVDIM();
            try
            {
                for (int i = 0; i <= inputData.oCollSidurimOvdimUpdRecorder.Count - 1; i++)
                {
                    if ((inputData.oCollSidurimOvdimUpdRecorder[i].ContainedItem.NEW_MISPAR_SIDUR == iMisparSidur) && (inputData.oCollSidurimOvdimUpdRecorder[i].ContainedItem.NEW_SHAT_HATCHALA == dShatHatchala) && inputData.oCollSidurimOvdimUpdRecorder[i].ContainedItem.UPDATE_OBJECT == 1)
                    {
                        oObjSidurimOvdim = inputData.oCollSidurimOvdimUpdRecorder[i].ContainedItem;
                    }
                }

                if (oObjSidurimOvdim.MISPAR_SIDUR == 0)
                {
                    for (int i = 0; i <= inputData.oCollSidurimOvdimIns.Count - 1; i++)
                    {
                        if ((inputData.oCollSidurimOvdimIns.Value[i].NEW_MISPAR_SIDUR == iMisparSidur) && (inputData.oCollSidurimOvdimIns.Value[i].NEW_SHAT_HATCHALA == dShatHatchala))
                        {
                            oObjSidurimOvdim = inputData.oCollSidurimOvdimIns.Value[i];
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oObjSidurimOvdim;
        }


        protected bool IsKnisaValid(SidurDM curSidur, string sBitulTypeField, bool bSidurHaveNahagut, MeafyenimDM oMeafyeneyOved)
        {
            bool bKnisaValid = false;
            bool bSidurShaonNotValid = false;
            try
            {
                if (!String.IsNullOrEmpty(curSidur.sShatHatchala))
                {  //אם הוחתם שעון וגם יש ערך במיקום כניסה - החתמת שעון ידנית 
                    if (!String.IsNullOrEmpty(curSidur.sMikumShaonKnisa) && curSidur.sMikumShaonKnisa != "0")
                    {
                        bKnisaValid = true;
                    }
                    else if ((sBitulTypeField == SIBA_LE_DIVUCH_YADANI_NESIAA && oMeafyeneyOved.IsMeafyenExist(51)) || (sBitulTypeField != SIBA_LE_DIVUCH_YADANI_NESIAA))
                    {

                        if ((!String.IsNullOrEmpty(curSidur.sShatHatchala)) && (String.IsNullOrEmpty(curSidur.sMikumShaonKnisa) || curSidur.sMikumShaonKnisa == "0"))
                        {  //אם הוחתם שעון ואין ערך במיקום כניסה - החתמת שעון ידנית                                                 
                            bSidurShaonNotValid = IsIshurYadaniValid(curSidur.iKodSibaLedivuchYadaniIn, sBitulTypeField);
                        }
                        if (bSidurShaonNotValid)
                        {
                            //קיום אישור לדיווח החתמת שעון (קוד אישור 1 או 3 עם סטטוס אישור 1 (מאושר)).
                            if (sBitulTypeField == SIBA_LE_DIVUCH_YADANI_NESIAA)
                            {
                                if (!bSidurHaveNahagut)// && CheckApprovalStatus("111,1,3,101,301", curSidur.iMisparSidur, curSidur.dFullShatHatchala) == 1)
                                    bKnisaValid = true;
                            }
                            else // if (CheckApprovalStatus("111,1,3,101,301", curSidur.iMisparSidur, curSidur.dFullShatHatchala) == 1)
                                bKnisaValid = true;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bKnisaValid;
        }

        protected bool IsYetizaValid(SidurDM curSidur, string sBitulTypeField, bool bSidurHaveNahagut, MeafyenimDM oMeafyeneyOved)
        {
            bool bYetizaValid = false;
            bool bSidurShaonNotValid = false;
            try
            {
                if (!String.IsNullOrEmpty(curSidur.sShatGmar))
                {  //אם הוחתם שעון וגם יש ערך במיקום יציאה - החתמת שעון ידנית 
                    if (!String.IsNullOrEmpty(curSidur.sMikumShaonYetzia) && curSidur.sMikumShaonYetzia != "0")
                    {
                        bYetizaValid = true;
                    }
                    else if ((sBitulTypeField == SIBA_LE_DIVUCH_YADANI_NESIAA && oMeafyeneyOved.IsMeafyenExist(51)) || (sBitulTypeField != SIBA_LE_DIVUCH_YADANI_NESIAA))
                    {
                        if ((!String.IsNullOrEmpty(curSidur.sShatGmar)) && (String.IsNullOrEmpty(curSidur.sMikumShaonYetzia) || curSidur.sMikumShaonYetzia == "0"))
                        {  //אם הוחתם שעון וגם אין ערך במיקום יציאה - החתמת שעון ידנית                                                 

                            bSidurShaonNotValid = IsIshurYadaniValid(curSidur.iKodSibaLedivuchYadaniOut, sBitulTypeField);
                        }

                        if (bSidurShaonNotValid)
                        {
                            //קיום אישור לדיווח החתמת שעון (קוד אישור 1 או 3 עם סטטוס אישור 1 (מאושר)).
                            if (sBitulTypeField == SIBA_LE_DIVUCH_YADANI_NESIAA)
                            {
                                if (!bSidurHaveNahagut)// &&(CheckApprovalStatus("111,1,3,102,302", curSidur.iMisparSidur, curSidur.dFullShatHatchala) == 1))
                                    bYetizaValid = true;
                            }
                            else  //if (CheckApprovalStatus("111,1,3,102,302", curSidur.iMisparSidur, curSidur.dFullShatHatchala) == 1)
                                bYetizaValid = true;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                //  clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 0, curSidur.iMisparIshi, curSidur.dSidurDate, curSidur.iMisparSidur, curSidur.dFullShatHatchala, null, null, "IsYetizaValid: " + ex.Message, null);
                throw ex;
            }
            return bYetizaValid;
        }

        private bool IsIshurYadaniValid(int iKodSibaLedivuckYadani, string sBitulTypeField)
        {
            bool bHasIshur = false;
            try
            {
                DataRow[] drSidurSibotLedivuchYadani;
                drSidurSibotLedivuchYadani = GetOneSibotLedivuachYadani(iKodSibaLedivuckYadani, _container.Resolve<IKDSCacheManager>().GetCacheItem<DataTable>(CachedItems.SibotLedivuchYadani));
                if (drSidurSibotLedivuchYadani.Length > 0)
                {
                    if (!System.Convert.IsDBNull(drSidurSibotLedivuchYadani[0][sBitulTypeField]))
                    {
                        if (int.Parse(drSidurSibotLedivuchYadani[0][sBitulTypeField].ToString()) != 1)
                        {
                            bHasIshur = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return bHasIshur;
        }

        private DataRow[] GetOneSibotLedivuachYadani(int iKodSiba, DataTable drSidurSibotLedivuchYadani)
        {   //הפונקציה מקבלת קוד סיבה ומחזירה גורמים לביטול  

            DataRow[] dr;

            dr = drSidurSibotLedivuchYadani.Select(string.Concat("kod_siba=", iKodSiba.ToString()));

            return dr;
        }
    }
}
