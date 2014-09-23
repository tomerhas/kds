using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections.Specialized;
using KDSCommon.Enums;

namespace KDSCommon.DataModels.Errors
{
    public class ErrorInputData
    {
        public ErrorInputData()
        {
            bCheckBoolSidur = false;
            bHaveSidurNahagut = false;
            dtErrors = new DataTable();
            IsSuccsess = true;
        }

        public int iMisparIshi { get; set; }
        public DateTime CardDate { get; set; }
        public int iSugYom { get; set; }
        public long? BtchRequestId { get; set; }
        public int? UserId { get; set; }
        public bool IsSuccsess { get; set; }
        public DateTime dTarTchilatMatzav = DateTime.MinValue;
        public bool bHaveSidurNahagut { get; set; }
        public bool bCheckBoolSidur { get; set; }//remove from code
       

        public DataTable SugeyYamimMeyuchadim { get; set; }
        public DataTable YamimMeyuchadim { get; set; }
        public DataTable  ErrorsNotActive { get; set; }
        public DataTable LookUp { get; set; }
        public DataTable dtMatzavOved { get; set; }
      

        public OvedYomAvodaDetailsDM OvedDetails { get; set; }
        public MeafyenimDM oMeafyeneyOved { get; set; }
        public clParametersDM oParameters { get; set; }
        public OrderedDictionary htEmployeeDetails{ get; set; }
      
        public int iLastMisaprSidur { get; set; }

        #region items fill during exec

        public DataTable dtErrors { get; set; }
        public SidurDM curSidur { get; set; }
        public PeilutDM curPeilut { get; set; }
        public int iSidur { get; set; }
        public int iPeilut { get; set; }
        public int iTotalHashlamotForSidur { get; set; }
        public int iTotalTimePrepareMechineForSidur { get; set; }
        public int iTotalTimePrepareMechineForDay { get; set; }
        public int iTotalTimePrepareMechineForOtherMechines { get; set; }
        public DataRow[] drSugSidur { get; set; }

        #endregion

    }
}
