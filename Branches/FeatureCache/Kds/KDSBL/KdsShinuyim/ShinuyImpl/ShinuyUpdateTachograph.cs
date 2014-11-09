using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using KDSCommon.DataModels.Shinuyim;
using KdsShinuyim.Enums;
using Microsoft.Practices.Unity;

namespace KdsShinuyim.ShinuyImpl
{
    public class ShinuyUpdateTachograph: ShinuyBase
    {

        public ShinuyUpdateTachograph(IUnityContainer container)
            : base(container)
        {

        }

        public override ShinuyTypes ShinuyType { get { return ShinuyTypes.ShinuyUpdateTachograph; } }


        public override void ExecShinuy(ShinuyInputData inputData)
        {
            try
            {
                if (!CheckIdkunRashemet("TACHOGRAF", inputData))
                    UpdateTachograph(inputData);
            }
            catch (Exception ex)
            {
                throw new Exception("ShinuyUpdateTachograph: " + ex.Message);
            }
        }

        private void UpdateTachograph(ShinuyInputData inputData)
        {
            //עדכון ברמת יום עבודה
            PeilutDM oPeilut;
            SidurDM oSidur;
            bool bNotDegem64 = false;
            int CountPeiluyot = 0;
            string oVal;
            try
            {
                //   בדיקה האם כל הרכבים המדווחים בפעילויות באותו תאריך הם מדגם 64 
                //.מספיק שיהיה רכב אחד עם דגם שונה כדי שתנאי זה לא יעבוד
                if ((inputData.htEmployeeDetails != null) && (inputData.htEmployeeDetails.Count > 0))
                {
                    for (int i = 0; i < inputData.htEmployeeDetails.Count; i++)
                    {
                        oSidur = ((SidurDM)(inputData.htEmployeeDetails[i]));
                        CountPeiluyot = CountPeiluyot + oSidur.htPeilut.Count;
                        for (int j = 0; j < oSidur.htPeilut.Count; j++)
                        {
                            oPeilut = (PeilutDM)oSidur.htPeilut[j];
                            if (inputData.dtMashar != null)
                            {
                                if (inputData.dtMashar.Select("bus_number=" + oPeilut.lOtoNo + " and SUBSTRING(convert(Vehicle_Type,'System.String'),3,2)<>64").Length > 0)
                                {
                                    bNotDegem64 = true;
                                    break;
                                }
                            }
                        }
                    }
                }

                oVal = inputData.oObjYameyAvodaUpd.TACHOGRAF.ToString();
                //יש לבדוק שלפחות אחד הרכבים המדווחים באותו תאריך אינו מדגם 64  (דגם שאינו מכיל טכוגרף).
                if (inputData.bSidurNahagut && (bNotDegem64 || CountPeiluyot == 0))
                {
                    //למ.א+תאריך, אם מזהים סידור נהגות אחד לפחות (זיהוי סידור נהגות לפי ערך 5 (נהגות) במאפיין 3 (סקטור עבודה) בטבלאות סידורים מיוחדים/מאפייני סוג סידור).
                    //יש טכוגרף
                    inputData.oObjYameyAvodaUpd.TACHOGRAF = "2";
                    inputData.oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                }
                else
                {
                    //. למ.א+תאריך, אם לא מזהים סידור נהגות אחד לפחות (זיהוי סידור נהגות לפי ערך 5 (נהגות) במאפיין 3 (סקטור עבודה) בטבלאות סידורים מיוחדים/מאפייני סוג סידור).
                    //אין טכוגרף
                    inputData.oObjYameyAvodaUpd.TACHOGRAF = "0";
                    inputData.oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
                }

                if (oVal != inputData.oObjYameyAvodaUpd.TACHOGRAF)
                    InsertLogDay(inputData, oVal, inputData.oObjYameyAvodaUpd.TACHOGRAF.ToString(), 0, "TACHOGRAF");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}