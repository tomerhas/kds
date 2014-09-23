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
using KdsShinuyim.ShinuyImpl;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;

public class ShinuyMisparSidurVisa02 : ShinuyBase
{

    public ShinuyMisparSidurVisa02(IUnityContainer container)
        : base(container)
    {

    }

    public override ShinuyTypes ShinuyType { get { return ShinuyTypes.ShinuyMisparSidurVisa02; } }


    public override void ExecShinuy(ShinuyInputData inputData)
    {
        try{
            for (int i = 0; i < inputData.htEmployeeDetails.Count; i++)
            {
                SidurDM curSidur = (SidurDM)inputData.htEmployeeDetails[i];
                if (!CheckIdkunRashemet("MISPAR_SIDUR", curSidur.iMisparSidur, curSidur.dFullShatHatchala,inputData))
                {
                        FixedMisparMatalatVisa02(curSidur, i, inputData);
                     
                }
            }
        }
        catch (Exception ex)
        {
            throw new Exception("ShinuyMisparSidurVisa02: " + ex.Message);
        }
    }

    private void FixedMisparMatalatVisa02(SidurDM curSidur, int iSidurIndex, ShinuyInputData inputData)
    {
        //PeilutDM oPeilut;
        //int iNewMisparSidur = 0;
        //SourceObj SourceObject;
        bool bHaveNesiatNamak = false;
        try
        {
            bHaveNesiatNamak = ContainsPeilutNamak(curSidur);
         
            if (!bHaveNesiatNamak)
            {
                if (curSidur.bSectorVisaExists && !curSidur.bSidurVisaKodExists)
                {
                    CreateSidurVisa(curSidur,inputData, iSidurIndex);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private void CreateSidurVisa( SidurDM curSidur,ShinuyInputData inputData, int iSidurIndex)
    {
        try{
            var oObjSidurimOvdimUpd = GetUpdSidurObject(curSidur, inputData);

            NewSidur oNewSidurim = FindSidurOnHtNewSidurim(curSidur.iMisparSidur, curSidur.dFullShatHatchala, inputData.htNewSidurim);

            oNewSidurim.SidurIndex = iSidurIndex;
            oNewSidurim.SidurOld = curSidur.iMisparSidur;

            ///ויזת צה"ל 
            if (curSidur.iSectorVisa == 1)
            {
                oNewSidurim.SidurNew = 99110;
            }
            else
            {//ויזת פנים 
                oNewSidurim.SidurNew = 99101;
            }

           // iNewMisparSidur = oNewSidurim.SidurNew;
            oNewSidurim.ShatHatchalaNew = curSidur.dFullShatHatchala;

            UpdateObjectUpdSidurim(oNewSidurim, inputData.oCollSidurimOvdimUpdRecorder);

            DataRow[] drSidurMeyuchad;
            drSidurMeyuchad = inputData.dtTmpSidurimMeyuchadim.Select("mispar_sidur=" + oNewSidurim.SidurNew);
            var sidurManager = ServiceLocator.Current.GetInstance<ISidurManager>();

           sidurManager.UpdateClsSidurFromSidurMeyuchad(curSidur, inputData.CardDate, oNewSidurim.SidurNew, drSidurMeyuchad[0]);
            oObjSidurimOvdimUpd.NEW_MISPAR_SIDUR = oNewSidurim.SidurNew;

            if (oObjSidurimOvdimUpd.NEW_MISPAR_SIDUR == 99101)
            {
                oObjSidurimOvdimUpd.MIVTZA_VISA = 1;
            }

            if (curSidur.htPeilut.Count == 0)
            {
                CreatePeilutVisa(curSidur,inputData, oNewSidurim);
            }
            else
            {
                for (int j = 0; j < ((SidurDM)(inputData.htEmployeeDetails[iSidurIndex])).htPeilut.Count; j++)
                {
                    SourceObj SourceObject;
                    PeilutDM oPeilut = (PeilutDM)((SidurDM)(inputData.htEmployeeDetails[iSidurIndex])).htPeilut[j];
                  //  oObjPeilutOvdimUpd = GetUpdPeilutObject(iSidurIndex, oPeilut, out SourceObject, oObjSidurimOvdimUpd);
                    OBJ_PEILUT_OVDIM oObjPeilutOvdimUpd = GetUpdPeilutObject(iSidurIndex, oPeilut, inputData, oObjSidurimOvdimUpd, out SourceObject);


                    //שינוי 02 - שלב שני
                    FixedMisparMatalatVisa02(oObjPeilutOvdimUpd, ref oPeilut, curSidur, oNewSidurim.SidurOld, SourceObject, inputData);
                  //?  oSidur.htPeilut[j] = oPeilut;
                }
                //UpdatePeiluyotMevutalotYadani(iSidurIndex,oNewSidurim, oObjSidurimOvdimUpd);
            }
        }
        catch (Exception ex)
        {
            //  clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", null, 2, _iMisparIshi, _dCardDate, oSidur.iMisparSidur, oSidur.dFullShatHatchala, null, null, "FixedMisparMatalatVisa02: " + ex.Message, null);
            throw ex;
        }
    }

    private void CreatePeilutVisa(SidurDM curSidur, ShinuyInputData inputData, NewSidur oNewSidurim)
    {
        try
        {
            OBJ_PEILUT_OVDIM oObjPeilutOvdimIns = new OBJ_PEILUT_OVDIM();
            InsertToObjPeilutOvdimForInsert(curSidur, oObjPeilutOvdimIns, inputData.UserId);
            oObjPeilutOvdimIns.MISPAR_SIDUR = oNewSidurim.SidurNew;
            oObjPeilutOvdimIns.TAARICH = inputData.CardDate;
            oObjPeilutOvdimIns.SHAT_YETZIA = curSidur.dFullShatHatchala;
            oObjPeilutOvdimIns.SHAT_HATCHALA_SIDUR = curSidur.dFullShatHatchala;
            oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse("50000000");
            oObjPeilutOvdimIns.MISPAR_VISA = oNewSidurim.SidurOld;
            oObjPeilutOvdimIns.BITUL_O_HOSAFA = 4;
            inputData.oCollPeilutOvdimIns.Add(oObjPeilutOvdimIns);

            PeilutDM oPeilutNew = CreatePeilut(inputData.iMisparIshi, inputData.CardDate, oObjPeilutOvdimIns, inputData.dtTmpMeafyeneyElements);
            oPeilutNew.iBitulOHosafa = 4;
            curSidur.htPeilut.Insert(0, 1, oPeilutNew);
        }
        catch (Exception ex)
        {
           throw ex;
        }
    }
    //? Fמות גדולה של פרמטרים
    private void FixedMisparMatalatVisa02( OBJ_PEILUT_OVDIM oObjPeilutOvdimUpd,ref PeilutDM oPeilut, SidurDM oSidur, int iOldMisparSidur, SourceObj SourceObject,ShinuyInputData inputData)
    {
        try
        {
            if (SourceObject == SourceObj.Insert)
            {
                oObjPeilutOvdimUpd.MISPAR_SIDUR = oSidur.iMisparSidur;
            }
            else
            {
                oObjPeilutOvdimUpd.NEW_MISPAR_SIDUR = oSidur.iMisparSidur;
                oObjPeilutOvdimUpd.UPDATE_OBJECT = 1;

            }
            oObjPeilutOvdimUpd.MISPAR_VISA = iOldMisparSidur;
            oObjPeilutOvdimUpd.MAKAT_NESIA = long.Parse("50" + oObjPeilutOvdimUpd.MISPAR_VISA);

         //   PeilutDM oPeilutNew = CreatePeilut(inputData.iMisparIshi, inputData.CardDate, oPeilut, oObjPeilutOvdimUpd.MAKAT_NESIA, inputData.dtTmpMeafyeneyElements);
           
            UpdatePeilut(inputData.iMisparIshi, inputData.CardDate, oPeilut, oObjPeilutOvdimUpd.MAKAT_NESIA, inputData.dtTmpMeafyeneyElements);

            oPeilut.lMisparVisa = oObjPeilutOvdimUpd.MISPAR_VISA;
            oPeilut.iPeilutMisparSidur = oSidur.iMisparSidur;
            //       oPeilutNew.iMakatType = enMakatType.mVisa.GetHashCode();
           // oPeilut = oPeilutNew; //?

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

        
}

