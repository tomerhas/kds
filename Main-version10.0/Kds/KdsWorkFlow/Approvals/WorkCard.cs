using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KdsWorkFlow.Approvals
{
    /// <summary>
    /// Represents a Work Card of Employee
    /// </summary>
    public class WorkCard
    {
        #region Fields
        private DateTime _workDate;
        private int _sidurNumber;
        private DateTime _sidurStart;
        private DateTime _activityStart;
        private int _activityNumber; 
        #endregion

        #region Properties
        public DateTime WorkDate
        {
            get { return _workDate; }
            set { _workDate = value; }
        }

        public int SidurNumber
        {
            get { return _sidurNumber; }
            set { _sidurNumber = value; }
        }

        public DateTime SidurStart
        {
            get { return _sidurStart; }
            set { _sidurStart = value; }
        }

        public DateTime ActivityStart
        {
            get { return _activityStart; }
            set { _activityStart = value; }
        }

        public int ActivityNumber
        {
            get { return _activityNumber; }
            set { _activityNumber = value; }
        } 
        #endregion
        private enum ErrorLevelItemNum
        {
            errYomAvoda = 3,
            errSidur = 5,
            errPeilut = 7
        }
        private const int ERR_ISHUR_CODE = 1;
       // private const int ERR_DESCRIPTION = 1;
        private const int ERR_NUM = 2;
        private const int ERR_SIDUR_NUMBER = 3;
        private const int ERR_SIDUR_START = 4;
        private const int ERR_ACTIVITIY_START = 5;
        private const int ERR_ACTIVITIY_NUMBER = 6;
        public bool IsEqual(WorkCard card)
        {
            if (card == null) return false;
            else return _workDate == card.WorkDate &&
                _sidurStart == card.SidurStart &&
                _activityStart == card.ActivityStart &&
                _activityNumber == card.ActivityNumber;

        }
        public bool HasApproval(ApprovalRequest[] arrEmployeeApproval, int iSidurNumber, DateTime dSidurStart, 
                                DateTime dActivityStart, long lActivityNumber,
                                int iApprovalKey, ref string sApprovalDescription,  ref bool bEnableApprove)
        {
            //חיפוש אישור ברמת פעילות
            bool bApproval = false;
            try
            {
                for (int i = 0; i < arrEmployeeApproval.Length; i++)
                {                    
                    if ((arrEmployeeApproval[i].WorkCard.SidurNumber == iSidurNumber)
                        && (arrEmployeeApproval[i].WorkCard.SidurStart == dSidurStart)
                        && (arrEmployeeApproval[i].WorkCard.ActivityStart == dActivityStart)
                        //&& ((arrEmployeeApproval[i].WorkCard.ActivityNumber == lActivityNumber) || (lActivityNumber==0))
                        && (arrEmployeeApproval[i].Approval.Code == iApprovalKey))                       
                      {
                        bApproval = (arrEmployeeApproval[i].State == ApprovalRequestState.Pending); //אישור בטיפול     
                        if (bApproval)
                        {
                            sApprovalDescription = arrEmployeeApproval[i].Approval.Description;
                            bEnableApprove = (arrEmployeeApproval[i].Approval.Level == 1); //במידה והאישור ברמה 1 וסטטוס אישור בטיפול נאפשר את השדה
                            break;
                        }                       
                      }
                }

                return bApproval;
            }
            catch (Exception ex)
            {
               throw ex;
            }           
        }
        public bool HasApproval(ApprovalRequest[] arrEmployeeApproval, int iSidurNumber, DateTime dSidurStart,                               
                               int iApprovalKey, ref string sApprovalDescription,ref bool bEnableApprove)
        {
            //חיפוש אישור ברמת סידור
            bool bApproval = false;
           
            try
            {
               // bEnableApprove=false;
                for (int i = 0; i < arrEmployeeApproval.Length; i++)
                {
                    if ((arrEmployeeApproval[i].WorkCard.SidurNumber == iSidurNumber)
                        && (arrEmployeeApproval[i].WorkCard.SidurStart == dSidurStart)                        
                        && (arrEmployeeApproval[i].Approval.Code == iApprovalKey))
                    {
                        bApproval = (arrEmployeeApproval[i].State == ApprovalRequestState.Pending); //אישור בטיפול                        
                        if (bApproval)
                        {
                            sApprovalDescription = arrEmployeeApproval[i].Approval.Description;
                            bEnableApprove = (arrEmployeeApproval[i].Approval.Level==1); //במידה והאישור ברמה 1 וסטטוס אישור בטיפול נאפשר את השדה
                            break;
                        }
                       
                    }
                }

                return bApproval;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool HasApproval(ApprovalRequest[] arrEmployeeApproval,
                               int iApprovalKey, ref string sApprovalDescription, ref bool bEnableApprove)
        {
            //חיפוש אישור ברמת יום עבודה
            bool bApproval = false;
           
            try
            {
                for (int i = 0; i < arrEmployeeApproval.Length; i++)
                {
                    if (arrEmployeeApproval[i].Approval.Code == iApprovalKey)
                    {
                        bApproval = (arrEmployeeApproval[i].State == ApprovalRequestState.Pending); //אישור בטיפול    
                        if (bApproval)
                        {
                            sApprovalDescription = arrEmployeeApproval[i].Approval.Description;
                            bEnableApprove = (arrEmployeeApproval[i].Approval.Level == 1); //במידה והאישור ברמה 1 וסטטוס אישור בטיפול נאפשר את השדה
                            break;
                        }                       
                    }
                }

                return bApproval;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int ApproveError(int iMisparIshi, DateTime dDateCard, string sErrorKey, string sGoremMeasher)
        {
            int iResult = 0;
            //string[] arrResult = hErrKey.Value.Split((char.Parse("|")));
            string[] arrResult = sErrorKey.Split((char.Parse("|")));
            WorkCard workCard = new WorkCard();
            ErrorLevelItemNum oErrLevel;

            try
            {
                workCard.WorkDate = dDateCard;
                
                oErrLevel = (ErrorLevelItemNum)arrResult.Length;
                switch (oErrLevel)
                {
                    case ErrorLevelItemNum.errYomAvoda: //שגיאה ברמת יום עבודה
                        workCard.SidurNumber = 0;
                        workCard.SidurStart = DateTime.MinValue;
                        workCard.ActivityNumber = 0;
                        workCard.ActivityStart = DateTime.MinValue;
                        break;
                    case ErrorLevelItemNum.errSidur:// שגיאה ברמת סידור
                        workCard.SidurNumber = int.Parse(arrResult[ERR_SIDUR_NUMBER]);
                        workCard.SidurStart = DateTime.Parse(arrResult[ERR_SIDUR_START]);
                        workCard.ActivityNumber = 0;
                        workCard.ActivityStart = DateTime.MinValue;
                        break;
                    case ErrorLevelItemNum.errPeilut: //שגיאה ברמת פעילות
                        workCard.SidurNumber = int.Parse(arrResult[ERR_SIDUR_NUMBER]);
                        workCard.SidurStart = DateTime.Parse(arrResult[ERR_SIDUR_START]);
                        workCard.ActivityStart = DateTime.Parse(arrResult[ERR_ACTIVITIY_START]);
                        workCard.ActivityNumber = int.Parse(arrResult[ERR_ACTIVITIY_NUMBER]);                       
                        break;
                }

                var request = ApprovalRequest.CreateApprovalRequest(iMisparIshi.ToString(), int.Parse(arrResult[ERR_ISHUR_CODE]),
                    workCard, RequestValues.Empty, true);

                request.ProcessRequest();
                iResult = 1;
                return iResult;
            }
            catch(Exception ex)
            {
                throw ex;
            }
            //    request.State==ApprovalRequestState.
        }
    }
}
