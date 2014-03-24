using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;
using KDSCommon.UDT;
using ObjectCompare;

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
          //  bHeadrutMachalaMiluimTeuna = false;
            bHaveCount = true;

            oCollSidurimOvdimIns = new COLL_SIDURIM_OVDIM();
            oCollSidurimOvdimDel = new COLL_SIDURIM_OVDIM();
            oCollPeilutOvdimDel = new COLL_OBJ_PEILUT_OVDIM();
            oCollPeilutOvdimIns = new COLL_OBJ_PEILUT_OVDIM();
            oCollIdkunRashemet = new COLL_IDKUN_RASHEMET();
            oCollApprovalErrors = new COLL_SHGIOT_MEUSHAROT();
            htNewSidurim = new OrderedDictionary();
            htEmployeeDetails = new OrderedDictionary();
           
            oCollYameyAvodaUpdRecorder = new ModificationRecorderCollection<OBJ_YAMEY_AVODA_OVDIM>();
            oCollSidurimOvdimUpdRecorder = new ModificationRecorderCollection<OBJ_SIDURIM_OVDIM>();
            oCollPeilutOvdimUpdRecorder = new ModificationRecorderCollection<OBJ_PEILUT_OVDIM>();
        }
        public int iMisparIshi { get; set; }
        public DateTime CardDate { get; set; }
        public int? UserId { get; set; }
        public bool IsSuccsess { get; set; }
        public bool bUsedMazanTichnun { get; set; }
        public bool bLoLetashlum { get; set; }
        public bool bSidurNahagut { get; set; }
        public bool bFirstSidurZakaiLenesiot { get; set; }
      //  public bool bHeadrutMachalaMiluimTeuna { get; set; }
        public int iSugYom { get; set; }
        public long? BtchRequestId { get; set; }
        public bool bHaveCount { get; set; } 

        public OrderedDictionary htEmployeeDetails { get; set; }
        public OrderedDictionary htSpecialEmployeeDetails { get; set; }
        public OrderedDictionary htFullEmployeeDetails { get; set; }
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

        public ModificationRecorderCollection<OBJ_YAMEY_AVODA_OVDIM> oCollYameyAvodaUpdRecorder { get; set; }
        //public COLL_YAMEY_AVODA_OVDIM oCollYameyAvodaUpd;
        public OrderedDictionary htNewSidurim { get; set; }
        public COLL_IDKUN_RASHEMET oCollIdkunRashemet { get; set; }
        public COLL_SHGIOT_MEUSHAROT oCollApprovalErrors { get; set; }
     
        public ModificationRecorderCollection<OBJ_SIDURIM_OVDIM> oCollSidurimOvdimUpdRecorder { get; set; }
       // public COLL_SIDURIM_OVDIM oCollSidurimOvdimUpd { get; set; }
        public COLL_SIDURIM_OVDIM oCollSidurimOvdimIns { get; set; }
        public COLL_SIDURIM_OVDIM oCollSidurimOvdimDel { get; set; }

        public ModificationRecorderCollection<OBJ_PEILUT_OVDIM> oCollPeilutOvdimUpdRecorder { get; set; }
    //    public COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimUpd { get; set; }
        public COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimIns { get; set; }
        public COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimDel { get; set; }
        
    }
}
