using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using KDSCommon.UDT;

namespace KDSCommon.DataModels.Shinuyim
{
    public class ShinuyInputData
    {
        public ShinuyInputData()
        {
            IsSuccsess = true;
            bUsedMazanTichnun = false;
            bLoLetashlum = false;
            bSidurNahagut = false;
            bFirstSidurZakaiLenesiot = false;
            bHeadrutMachalaMiluimTeuna = false;
        }
        public int iMisparIshi { get; set; }
        public DateTime CardDate { get; set; }
        public int? UserId { get; set; }
        public bool IsSuccsess { get; set; }
        public bool bUsedMazanTichnun { get; set; }
        public bool bLoLetashlum { get; set; }
        public bool bSidurNahagut { get; set; }
        public bool bFirstSidurZakaiLenesiot { get; set; }
        public bool bHeadrutMachalaMiluimTeuna { get; set; }
        public int iSugYom { get; set; }

        public OrderedDictionary htEmployeeDetails { get; set; }
        public OrderedDictionary htSpecialEmployeeDetails { get; set; }
        public DataTable IdkuneyRashemet { get; set; }
        public DataTable ApprovalError { get; set; }
        public DataTable dtTmpSidurimMeyuchadim { get; set; }
        public DataTable dtTmpMeafyeneyElements { get; set; }
        public DataTable dtMatzavOved { get; set; }
        public DataTable SugeyYamimMeyuchadim { get; set; }
        public DataTable YamimMeyuchadim { get; set; }
        public DataTable dtMashar { get; set; }
        //public DataTable dtSibotLedivuachYadani { get; set; }

        public OvedYomAvodaDetailsDM OvedDetails { get; set; }
        public clParametersDM oParam { get; set; }
        public MeafyenimDM oMeafyeneyOved { get; set; }

        /**/
        public OBJ_YAMEY_AVODA_OVDIM oObjYameyAvodaUpd { get; set; }
        public OrderedDictionary htNewSidurim { get; set; }
        public COLL_IDKUN_RASHEMET oCollIdkunRashemet { get; set; }
        public COLL_SHGIOT_MEUSHAROT oCollApprovalErrors { get; set; }
   
        
        public COLL_SIDURIM_OVDIM oCollSidurimOvdimUpd { get; set; }
        public COLL_SIDURIM_OVDIM oCollSidurimOvdimIns { get; set; }
        public COLL_SIDURIM_OVDIM oCollSidurimOvdimDel { get; set; }

        public COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimUpd { get; set; }
        public COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimIns { get; set; }
        public COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimDel { get; set; }
        
    }
}
