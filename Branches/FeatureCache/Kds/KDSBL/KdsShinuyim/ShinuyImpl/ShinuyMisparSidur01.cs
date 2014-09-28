using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using KDSCommon.DataModels.Shinuyim;
using KDSCommon.Enums;
using KDSCommon.Interfaces.Managers;
using KDSCommon.UDT;
using KdsShinuyim.DataModels;
using KdsShinuyim.Enums;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

namespace KdsShinuyim.ShinuyImpl
{
    public class ShinuyMisparSidur01 : ShinuyBase
    {

        public ShinuyMisparSidur01(IUnityContainer container)
            : base(container)
        {

        }
        public override ShinuyTypes ShinuyType { get { return ShinuyTypes.ShinuyMisparSidur01; } }



        public override void ExecShinuy(ShinuyInputData inputData)
        {
            try
            {
                for (int i = 0; i < inputData.htEmployeeDetails.Count; i++)
                {
                    SidurDM curSidur = (SidurDM)inputData.htEmployeeDetails[i];
                  
                    if (!CheckIdkunRashemet("MISPAR_SIDUR", curSidur.iMisparSidur, curSidur.dFullShatHatchala, inputData))
                    {
                        FixedMisparSidur01( curSidur, i, inputData);
                      //  inputData.htEmployeeDetails[i] = curSidur;
                    }
                    
                }
            }
            catch (Exception ex)
            {
                throw new Exception("ShinuyMisparSidur01: " + ex.Message);
            }
        }

        private void FixedMisparSidur01( SidurDM curSidur, int iSidurIndex, ShinuyInputData inputData)
        {
           
             
            bool bHaveNesiatNamak = false;

           // bool bUpdateMisparMatala = false;
            try
            {   //בדיקה מספר 1  - תיקון מספר סידור 
                //הסדרן בכל סניף יכול להוסיף לנהג "מטלות" אשר מסומנות בספרור 00001 - 00999. זה אינו מספר סידור חוקי ולכן הופכים אותו לחוקי עפ"י קוד הפעילות אשר הסדרן רשם למטלה זו. יהיה צורך לשמור בשדה נפרד את מספר המטלה המקורי
                bHaveNesiatNamak = ContainsPeilutNamak(curSidur);

                if ((curSidur.iMisparSidur >= 1 && curSidur.iMisparSidur <= 999) || (curSidur.bSectorVisaExists && bHaveNesiatNamak))
                {

                    if (curSidur.iMisparSidur == 99300)
                    {
                        return;
                    }

                    if (((SidurDM)(inputData.htEmployeeDetails[iSidurIndex])).htPeilut.Count > 0)
                    {
                        PeilutDM oPeilut = (PeilutDM)((SidurDM)(inputData.htEmployeeDetails[iSidurIndex])).htPeilut[0];
                        UpdateMisparSidur( curSidur, oPeilut, iSidurIndex, inputData);
                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void UpdateMisparSidur(SidurDM curSidur,PeilutDM oPeilut, int iSidurIndex, ShinuyInputData inputData)
        {
            int iNewMisparMatala, iNewMisparSidur;
            int iMisparSidur = GetMisparSidur(oPeilut);
            OBJ_PEILUT_OVDIM oObjPeilutUpd;
            try{
                if (iMisparSidur > 0)
                {
                  //  inputData.LogCollection.AddShinuy(this, curSidur);

                    curSidur.bSidurMyuhad = true;
                    NewSidur oNewSidurim = FindSidurOnHtNewSidurim(curSidur.iMisparSidur, curSidur.dFullShatHatchala, inputData.htNewSidurim);

                    iNewMisparSidur = iMisparSidur;
                    iNewMisparMatala = curSidur.iMisparSidur;
                    //    bUpdateMisparMatala = true;

                    oNewSidurim.SidurIndex = iSidurIndex;
                    oNewSidurim.SidurNew = iMisparSidur;
                    oNewSidurim.ShatHatchalaNew = curSidur.dFullShatHatchala;

                    //שינוי מקום בעקבות באג 17/05
                    var oObjSidurimOvdimUpd = GetUpdSidurObject(curSidur, inputData);

                    UpdateObjectUpdSidurim(oNewSidurim, inputData.oCollSidurimOvdimUpdRecorder);
                    DataRow[] drSidurMeyuchad;

                    drSidurMeyuchad = inputData.dtTmpSidurimMeyuchadim.Select("mispar_sidur=" + iNewMisparSidur);
                    var sidurManager = ServiceLocator.Current.GetInstance<ISidurManager>();
                  //  var temp = sidurManager.CreateClsSidurFromSidurMeyuchad(curSidur, inputData.CardDate, iNewMisparSidur, drSidurMeyuchad[0]);
                 
                    sidurManager.UpdateClsSidurFromSidurMeyuchad(curSidur, inputData.CardDate, iNewMisparSidur, drSidurMeyuchad[0]);
                    
                    curSidur.sHashlama = "0";
                    curSidur.sOutMichsa = "0";

                    oObjSidurimOvdimUpd.NEW_MISPAR_SIDUR = iNewMisparSidur;
                    oObjSidurimOvdimUpd.HASHLAMA = 0;
                    oObjSidurimOvdimUpd.OUT_MICHSA = 0;
                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
               

                    for (int j = 0; j < ((SidurDM)(inputData.htEmployeeDetails[iSidurIndex])).htPeilut.Count; j++)
                    {
                        
                        //עבור שינוי 1, במידה והיה צורך לעדכן את מספר המטלה במספר הסידור הישן )מספר הסידור קיבל מספר חדש(
                        //נעדכן את הפעילות הראשונה . במקרה כזה לא אמורה להיות יותר מפעילות אחת לסידור
                        //נעדכן גם את הפעילויות במספר הסידור החדש
                        SourceObj SourceObject;
                        oPeilut = (PeilutDM)((SidurDM)(inputData.htEmployeeDetails[iSidurIndex])).htPeilut[j];
                        oObjPeilutUpd = GetUpdPeilutObject(iSidurIndex, oPeilut, inputData, oObjSidurimOvdimUpd, out SourceObject);

                        oObjPeilutUpd.MISPAR_MATALA = iNewMisparMatala;
                        if (SourceObject == SourceObj.Insert)
                        {
                            oObjPeilutUpd.MISPAR_SIDUR = iNewMisparSidur;
                        }
                        else
                        {
                            oObjPeilutUpd.NEW_MISPAR_SIDUR = iNewMisparSidur;
                            oObjPeilutUpd.UPDATE_OBJECT = 1;
                        }
                        oPeilut.iPeilutMisparSidur = iNewMisparSidur;
                        oPeilut.lMisparMatala = iNewMisparMatala;
                    }
                    //UpdatePeiluyotMevutalotYadani(iSidurIndex, oNewSidurim, oObjSidurimOvdimUpd);
                   
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private int GetMisparSidur(PeilutDM oPeilut)
        {
            int iMisparSidur = 0;
            enMakatType oMakatType;
            oMakatType = (enMakatType)oPeilut.iMakatType;
            switch (oMakatType)
            {
                case enMakatType.mKavShirut:
                    //פעילות מסוג נסיעת שירות
                    // אם הפעילות תחת המטלה  היא נסיעת שירות (לפי רוטינת זיהוי מקט), יש להכניס למספר סידור 99300
                    iMisparSidur = SIDUR_NESIA;
                    break;
                case enMakatType.mEmpty:
                    //פעילות מסוג ריקה
                    //אם הפעילות תחת המטלה היא ריקה (לפי רוטינת זיהוי מקט), יש לסמן את הסידור 99300 
                    iMisparSidur = SIDUR_NESIA;
                    break;
                case enMakatType.mNamak:
                    //פעילות מסוג נמ"ק
                    //אם הפעילות תחת המטלה היא נמ"ק (לפי רוטינת זיהוי מקט), יש לסמן את הסידור 99300 
                    iMisparSidur = SIDUR_NESIA;
                    break;
                case enMakatType.mElement:
                    if (oPeilut.bMisparSidurMatalotTnuaExists)
                    {
                        //קיים מאפיין 28
                        //נקח את מספר הסידור ממאפיין 28
                        iMisparSidur = oPeilut.iMisparSidurMatalotTnua;
                    }
                    else
                    {
                        //אם לא קיים מאפיין 28
                        iMisparSidur = SIDUR_MATALA;
                    }
                    break;
            }
            return iMisparSidur;
        }

      
    }
}
