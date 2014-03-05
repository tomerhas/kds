﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using KDSCommon.DataModels.Shinuyim;
using KdsShinuyim.Enums;
using Microsoft.Practices.Unity;

namespace KdsShinuyim.ShinuyImpl
{
    class ShinuyFixedLina07 : ShinuyBase
    {

        public ShinuyFixedLina07(IUnityContainer container)
            : base(container)
        {

        }

        public override ShinuyTypes ShinuyType { get { return ShinuyTypes.ShinuyFixedLina07; } }

        public override void ExecShinuy(ShinuyInputData inputData)
        {
            if (!CheckIdkunRashemet("LINA", inputData))
                FixedLina07(inputData);
        }

        private void FixedLina07(ShinuyInputData inputData)
        {
            SidurDM curSidur = new SidurDM();
            int iLina = 0;
            int iCountLina = 0;
            try
            {

                if (inputData.htEmployeeDetails != null)
                {
                    for (int i = 0; i < inputData.htEmployeeDetails.Count; i++)
                    {
                        curSidur = (SidurDM)inputData.htEmployeeDetails[i];

                        if (curSidur.iZakaiLelina == 3) { iLina = 1; iCountLina += 1; }
                        else if (curSidur.iZakaiLelina == 4) { iLina = 2; iCountLina += 1; }
                    }
                }

                if (iCountLina == 1) inputData.oObjYameyAvodaUpd.LINA = iLina;
                else inputData.oObjYameyAvodaUpd.LINA = 0;

                inputData.oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
            }
            catch (Exception ex)
            {
               // clLogBakashot.InsertErrorToLog(_btchRequest.HasValue ? _btchRequest.Value : 0, "E", 7, "FixedLina07: " + ex.Message);
                inputData.IsSuccsess = false; 
            }
        }

    }
}