using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using KDSCommon.DataModels;
using KDSCommon.DataModels.Shinuyim;
using KDSCommon.Interfaces.Managers;
using KdsShinuyim.Enums;
using Microsoft.Practices.Unity;

namespace KdsShinuyim.ShinuyImpl
{
    public class SetBooleanParams : ShinuyBase
    {

        public SetBooleanParams(IUnityContainer container)
            : base(container)
        {

        }

        public override ShinuyTypes ShinuyType { get { return ShinuyTypes.SetBooleanParams; } }

        public override void ExecShinuy(ShinuyInputData inputData)
        {
            DataRow[] drSugSidur;
            for (int i = 0; i < inputData.htEmployeeDetails.Count; i++)
            {
                SidurDM oSidur = (SidurDM)inputData.htEmployeeDetails[i];

                var sidurManager = _container.Resolve<ISidurManager>();
                drSugSidur = sidurManager.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, inputData.CardDate);

                //אם סידור אחד לפחות לתשלום, נדליק את  הFLAG
                if (oSidur.iLoLetashlum == 0) 
                {
                    inputData.bLoLetashlum = true;
                }

                //ברגע שמצאנו סידור נהגות אחד לפחות, לא נמשיך לחפש
                // עבור עדכון טכוגרף
                if (!inputData.bSidurNahagut)
                {
                    inputData.bSidurNahagut = sidurManager.IsSidurNahagut(drSugSidur, oSidur); 
                }

                //ברגע שמצאנו סידור העדרות(מסוג מחלה/מילואים/תאונה) אחד לפחות, לא נמשיך לחפש
                //עבור עדכון השלמה ליום
                //if (!inputData.bSidurHeadrut)
                //{
                //    bSidurHeadrut = sidurManagerIsSidurHeadrut(oSidur);
                //}
                //if (!inputData.bHeadrutMachalaMiluimTeuna)
                //{
                //    bHeadrutMachalaMiluimTeuna = IsHeadrutMachalaMiluimTeuna(oSidur, ref iHeadrutTypeKod);
                //}


                //ברגע שמצאנו סידור אחד לפחות שהוא סידור שעון עם החתמת שעון תקינה )אוטומטית/ידנית), נפסיק לחפש
                //if (!inputData.bSidurShaon) 
                //{ 
                //    bSidurShaon = IsSidurShaon(oSidur);
                //}

            }
        }
    }
}
