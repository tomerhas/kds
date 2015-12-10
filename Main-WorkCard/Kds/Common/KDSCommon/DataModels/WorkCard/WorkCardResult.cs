using KDSCommon.Enums;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Linq;
using System.Text;

namespace KDSCommon.DataModels.WorkCard
{
    /// <summary>
    /// Contains data of a users work card for a misparishi and date
    /// This is the original WorkCardResult as defined in the system prior to angular version
    /// </summary>
    public class WorkCardResult
    {
        public WorkCardResult()
        {
            CardStatus = KDSCommon.Enums.CardStatus.Valid;
        }
        public bool Succeeded { get; set; }
        public OrderedDictionary htEmployeeDetails { get; set; }
        public OrderedDictionary htFullEmployeeDetails { get; set; }
        public clParametersDM oParam { get; set; }
        public DataTable dtLookUp { get; set; }
        public DataTable dtSugSidur { get; set; }
        public OvedYomAvodaDetailsDM oOvedYomAvodaDetails { get; set; }
        public MeafyenimDM oMeafyeneyOved { get; set; }
        public DataTable dtDetails { get; set; }
        public DataTable dtErrors { get; set; }
        public CardStatus CardStatus { get; set; }
        public DataTable dtMashar { get; set; }
    }
}
