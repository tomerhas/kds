using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels.Shinuyim;
using KDSCommon.UDT;
using KdsShinuyim.DataModels;
using KdsShinuyim.Enums;
using Microsoft.Practices.Unity;

namespace KdsShinuyim.ShinuyImpl
{
    public class  SetSidurObjects : ShinuyBase
    {

        public SetSidurObjects(IUnityContainer container)
            : base(container)
        {

        }

        public override ShinuyTypes ShinuyType { get { return ShinuyTypes.SetSidurObjects; } }

        public override void ExecShinuy(ShinuyInputData inputData)
        {
            try
            {
                SetObjectsFromNewSidurim(inputData);
            }
            catch (Exception ex)
            {
                throw new Exception("SetSidurObjects: " + ex.Message);
            }
        }

        private void SetObjectsFromNewSidurim(ShinuyInputData inputData)
        {
            NewSidur oNewSidurim;
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
            int index;
            try
            {
                //נעבור על האובייקט שמחזיק את כל הסידורים להן השתנה מספר הסידור בעקבות שינוי מספר 1                    
                for (int i = 0; i < inputData.htNewSidurim.Count; i++)
                {
                    oNewSidurim = (NewSidur)inputData.htNewSidurim[i];
                    //נביא את הסידור עם הנתונים העדכניים
                    oObjSidurimOvdimUpd = GetSidurOvdimObject(oNewSidurim.SidurNew, oNewSidurim.ShatHatchalaNew,inputData);
                    index = -1;
                    if (!CheckSidurNewKeyInObjectDelete(oNewSidurim, inputData.oCollSidurimOvdimDel ,ref index))
                    {
                        if (!(oNewSidurim.SidurNew == oNewSidurim.SidurOld && oNewSidurim.ShatHatchalaNew == oNewSidurim.ShatHatchalaOld))
                        {

                            //נכניס סידור חדש עם כל הנתונים העדכניים ועם מספר הסידור החדש
                            AddNewSidurToCollection(inputData, oNewSidurim, oObjSidurimOvdimUpd);

                            //באובייקט העדכון, נאפס את המשתנה שמציין שיש לעדכן את הרשומה
                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 0;

                            //נמחוק את הסידור השגוי    
                            DeleteSidurShaguyFromCollection(inputData, oNewSidurim, oObjSidurimOvdimUpd);

                        }
                        else
                        {
                            //if(oNewSidurim.ShatGmarNew!=oNewSidurim.ShatGmarOld)
                            //    oObjSidurimOvdimUpd.SHAT_GMAR=oNewSidurim.ShatGmarNew;             
                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                        }
                    }
                    else
                    {
                        inputData.oCollSidurimOvdimDel.RemoveAt(index);
                        //נמחוק את הסידור השגוי    
                        DeleteSidurShaguyFromCollection(inputData, oNewSidurim, oObjSidurimOvdimUpd);
                       
                        oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        private void DeleteSidurShaguyFromCollection(ShinuyInputData inputData, NewSidur oNewSidurim, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimDel = new OBJ_SIDURIM_OVDIM();
            try
            {
                oObjSidurimOvdimDel.MISPAR_ISHI = oObjSidurimOvdimUpd.MISPAR_ISHI;
                oObjSidurimOvdimDel.SHAT_HATCHALA = oNewSidurim.ShatHatchalaOld;
                oObjSidurimOvdimDel.NEW_SHAT_HATCHALA = oNewSidurim.ShatHatchalaNew;//DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatHatchala));
                oObjSidurimOvdimDel.TAARICH = oObjSidurimOvdimUpd.TAARICH;
                oObjSidurimOvdimDel.MISPAR_SIDUR = oNewSidurim.SidurOld; //מספר סידור קודם
                oObjSidurimOvdimDel.UPDATE_OBJECT = 0;

                inputData.oCollSidurimOvdimDel.Add(oObjSidurimOvdimDel);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void AddNewSidurToCollection(ShinuyInputData inputData, NewSidur oNewSidurim, OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
        {
            OBJ_SIDURIM_OVDIM oObjSidurimOvdimIns= new OBJ_SIDURIM_OVDIM();

            try{
                oObjSidurimOvdimIns.MISPAR_ISHI = inputData.iMisparIshi;
                oObjSidurimOvdimIns.CHARIGA = oObjSidurimOvdimUpd.CHARIGA;
                oObjSidurimOvdimIns.HAMARAT_SHABAT = oObjSidurimOvdimUpd.HAMARAT_SHABAT;
                if (!oObjSidurimOvdimUpd.HASHLAMAIsNull)
                {
                    oObjSidurimOvdimIns.HASHLAMA = oObjSidurimOvdimUpd.HASHLAMA;
                }
                oObjSidurimOvdimIns.LO_LETASHLUM = oObjSidurimOvdimUpd.LO_LETASHLUM;
                if (!oObjSidurimOvdimUpd.OUT_MICHSAIsNull)
                {
                    oObjSidurimOvdimIns.OUT_MICHSA = oObjSidurimOvdimUpd.OUT_MICHSA;
                }
                oObjSidurimOvdimIns.PITZUL_HAFSAKA = oObjSidurimOvdimUpd.PITZUL_HAFSAKA;
                oObjSidurimOvdimIns.SHAT_GMAR = oObjSidurimOvdimUpd.SHAT_GMAR; //(oNewSidurim.ShatGmarNew != DateTime.MinValue ? oNewSidurim.ShatGmarNew : oObjSidurimOvdimUpd.SHAT_GMAR);
                oObjSidurimOvdimIns.SHAT_HATCHALA = oNewSidurim.ShatHatchalaNew;
                oObjSidurimOvdimIns.NEW_SHAT_HATCHALA = oNewSidurim.ShatHatchalaNew;
                oObjSidurimOvdimIns.TAARICH = inputData.CardDate;
                oObjSidurimOvdimIns.YOM_VISA = oObjSidurimOvdimUpd.YOM_VISA;
                oObjSidurimOvdimIns.MIVTZA_VISA = oObjSidurimOvdimUpd.MIVTZA_VISA;
                oObjSidurimOvdimIns.MEZAKE_NESIOT = oObjSidurimOvdimUpd.MEZAKE_NESIOT;
                oObjSidurimOvdimIns.MEZAKE_HALBASHA = oObjSidurimOvdimUpd.MEZAKE_HALBASHA;
                oObjSidurimOvdimIns.SHAT_HATCHALA_LETASHLUM = oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM;
                oObjSidurimOvdimIns.SHAT_GMAR_LETASHLUM = oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM;
                oObjSidurimOvdimIns.MISPAR_SIDUR = oNewSidurim.SidurNew;
                oObjSidurimOvdimIns.NEW_MISPAR_SIDUR = oNewSidurim.SidurNew;
                oObjSidurimOvdimIns.BITUL_O_HOSAFA = oObjSidurimOvdimUpd.BITUL_O_HOSAFA;
                oObjSidurimOvdimIns.TAFKID_VISA = oObjSidurimOvdimUpd.TAFKID_VISA;
                oObjSidurimOvdimIns.TOSEFET_GRIRA = oObjSidurimOvdimUpd.TOSEFET_GRIRA;
                oObjSidurimOvdimIns.MIKUM_SHAON_KNISA = oObjSidurimOvdimUpd.MIKUM_SHAON_KNISA;
                oObjSidurimOvdimIns.MIKUM_SHAON_YETZIA = oObjSidurimOvdimUpd.MIKUM_SHAON_YETZIA;
                // oObjSidurimOvdimIns.KM_VISA_LEPREMIA = oObjSidurimOvdimUpd.KM_VISA_LEPREMIA;
                oObjSidurimOvdimIns.ACHUZ_KNAS_LEPREMYAT_VISA = oObjSidurimOvdimUpd.ACHUZ_KNAS_LEPREMYAT_VISA;
                oObjSidurimOvdimIns.ACHUZ_VIZA_BESIKUN = oObjSidurimOvdimUpd.ACHUZ_VIZA_BESIKUN;
                //oObjSidurimOvdimIns.SUG_VISA = oObjSidurimOvdimUpd.SUG_VISA;
                oObjSidurimOvdimIns.MISPAR_MUSACH_O_MACHSAN = oObjSidurimOvdimUpd.MISPAR_MUSACH_O_MACHSAN;
                oObjSidurimOvdimIns.KOD_SIBA_LEDIVUCH_YADANI_IN = oObjSidurimOvdimUpd.KOD_SIBA_LEDIVUCH_YADANI_IN;
                oObjSidurimOvdimIns.KOD_SIBA_LEDIVUCH_YADANI_OUT = oObjSidurimOvdimUpd.KOD_SIBA_LEDIVUCH_YADANI_OUT;
                oObjSidurimOvdimIns.KOD_SIBA_LO_LETASHLUM = oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM;
                oObjSidurimOvdimIns.SHAYAH_LEYOM_KODEM = oObjSidurimOvdimUpd.SHAYAH_LEYOM_KODEM;
                oObjSidurimOvdimIns.MEADKEN_ACHARON = oObjSidurimOvdimUpd.MEADKEN_ACHARON;
                oObjSidurimOvdimIns.TAARICH_IDKUN_ACHARON = oObjSidurimOvdimUpd.TAARICH_IDKUN_ACHARON;
                oObjSidurimOvdimIns.HEARA = oObjSidurimOvdimUpd.HEARA;
                oObjSidurimOvdimIns.MISPAR_SHIUREY_NEHIGA = oObjSidurimOvdimUpd.MISPAR_SHIUREY_NEHIGA;
                //oObjSidurimOvdimIns.BUTAL = oObjSidurimOvdimUpd.BUTAL;
                oObjSidurimOvdimIns.SUG_HAZMANAT_VISA = oObjSidurimOvdimUpd.SUG_HAZMANAT_VISA;
                oObjSidurimOvdimIns.SHAT_HITIATZVUT = oObjSidurimOvdimUpd.SHAT_HITIATZVUT;

                if (!oObjSidurimOvdimUpd.SECTOR_VISAIsNull)
                {
                    oObjSidurimOvdimIns.SECTOR_VISA = oObjSidurimOvdimUpd.SECTOR_VISA;
                }
                oObjSidurimOvdimIns.NIDRESHET_HITIATZVUT = oObjSidurimOvdimUpd.NIDRESHET_HITIATZVUT;
                oObjSidurimOvdimIns.PTOR_MEHITIATZVUT = oObjSidurimOvdimUpd.PTOR_MEHITIATZVUT;
                oObjSidurimOvdimIns.HACHTAMA_BEATAR_LO_TAKIN = oObjSidurimOvdimUpd.HACHTAMA_BEATAR_LO_TAKIN;
                if (oObjSidurimOvdimUpd.SUG_SIDUR != 0)
                    oObjSidurimOvdimIns.SUG_SIDUR = oObjSidurimOvdimUpd.SUG_SIDUR;

                inputData.oCollSidurimOvdimIns.Add(oObjSidurimOvdimIns);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private bool CheckSidurNewKeyInObjectDelete(NewSidur oSidurNew, COLL_SIDURIM_OVDIM oCollSidurimOvdimDel, ref int index)
        {
            // PeilutDM oPeilut;
            // SidurDM oSidur;
            bool bExist = false;
            try
            {
                //נמצא את הפעילות באובייקט פעילויות לעדכון

                for (int i = 0; i <= oCollSidurimOvdimDel.Count - 1; i++)
                {
                    // oSidur = (SidurDM)htEmployeeDetails[i];
                    if ((oCollSidurimOvdimDel.Value[i].MISPAR_SIDUR == oSidurNew.SidurNew) && (oCollSidurimOvdimDel.Value[i].SHAT_HATCHALA == oSidurNew.ShatHatchalaNew))
                    {
                        bExist = true;
                        index = i;
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


    }
}