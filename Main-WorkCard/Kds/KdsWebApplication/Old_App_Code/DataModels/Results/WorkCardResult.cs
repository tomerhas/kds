//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Data;
//using System.Linq;
//using System.Web;
//using KDSCommon.DataModels;
//using KDSCommon.Enums;

///// <summary>
///// Summary description for WorkCardResult
///// </summary>
//public class WorkCardResult
//{
//    public WorkCardResult()
//    {
//        CardStatus = KDSCommon.Enums.CardStatus.Valid;
//    }
//    public  bool Succeeded { get; set; }
//    public OrderedDictionary htEmployeeDetails { get; set; }
//    public OrderedDictionary htFullEmployeeDetails { get; set; }
//    public clParametersDM oParam { get; set; }
//    public DataTable dtLookUp { get; set; }
//    public DataTable dtSugSidur { get; set; }
//    public OvedYomAvodaDetailsDM oOvedYomAvodaDetails { get; set; }
//    public MeafyenimDM oMeafyeneyOved { get; set; }
//    public DataTable dtDetails { get; set; }
//    public DataTable dtErrors { get; set; }
//    public CardStatus CardStatus { get; set; }
//    public DataTable dtMashar { get; set; }

//}