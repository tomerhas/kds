//using System;
//using System.Collections.Generic;
//using System.Collections.Specialized;
//using System.Linq;
//using System.Text;
//using System.Data;
//using System.Collections;
////using System.Collections.Specialized;
//using KdsLibrary.UDT;
//using KdsLibrary.Utils;
//using KdsLibrary;
//using KdsLibrary.BL;
//using KdsWorkFlow.Approvals;
//using System.Diagnostics;

//namespace KdsBatch.InputData
//{
//    public class clInputData
//    {
//        private clOvedYomAvoda oOvedYomAvodaDetails;
//        private DataTable dtDetails;
//        private OrderedDictionary htEmployeeDetails = new OrderedDictionary();
//        OrderedDictionary htNewSidurim = new OrderedDictionary();//יכיל את מפתחות הסידורים שהוחלף להם מספר הסידור בעקבות שינוי מספר 1
//        private int iLastMisaprSidur;
//        private clParameters oParam;        
//        private clMeafyenyOved oMeafyeneyOved;
//        private DataTable dtSugSidur; //מכיל מאפייני סוגי סידורים 
//        private DataTable dtMutamut;
//        private DataTable dtSibotLedivuachYadani;
//        private DataTable dtChishuvYom;
//        private OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
//        private OBJ_SIDURIM_OVDIM oObjSidurimOvdimDel;        
//        private OBJ_SIDURIM_OVDIM oObjSidurimOvdimIns;
//        private OBJ_PEILUT_OVDIM oObjPeilutOvdimUpd;
//        private OBJ_PEILUT_OVDIM oObjPeilutOvdimDel;
//        private OBJ_YAMEY_AVODA_OVDIM oObjYameyAvodaUpd;
//        private COLL_SIDURIM_OVDIM oCollSidurimOvdimUpd; //= new COLL_SIDURIM_OVDIM();
//        private COLL_SIDURIM_OVDIM oCollSidurimOvdimIns; //= new COLL_SIDURIM_OVDIM();
//        private COLL_SIDURIM_OVDIM oCollSidurimOvdimDel;// = new COLL_SIDURIM_OVDIM();
//        private COLL_YAMEY_AVODA_OVDIM oCollYameyAvodaUpd;// = new COLL_YAMEY_AVODA_OVDIM();
//        private COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimDel; //= new COLL_OBJ_PEILUT_OVDIM();
//        private COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimUpd; //= new COLL_OBJ_PEILUT_OVDIM();
//        private COLL_OBJ_PEILUT_OVDIM oCollPeilutOvdimIns; // new COLL_OBJ_PEILUT_OVDIM();
//        private ApprovalRequest[] arrEmployeeApproval;
//        private bool bHasShaonIsurInMaxLevel;
//        private clCalculation objCalc = new clCalculation();
//        private const int SIDUR_NESIA = 99300;
//        private const int SIDUR_MATALA = 99301;
//        private const int SIDUR_HEADRUT_BETASHLUM = 99801;
//        private const string SIBA_LE_DIVUCH_YADANI_HALBASHA = "goremet_lebitul_zman_halbasha";
//        private const string SIBA_LE_DIVUCH_YADANI_NESIAA = "goremet_lebitul_zman_nesiaa";

//        public enum ApprovalCode
//        {
//            Code1 = 1,
//            Code3 = 3
//        }
//        public enum ZmanNesiotType
//        {
//            ZakaiKnisa = 1,
//            ZakaiYetiza = 2,
//            ZakaiKnisaYetiza = 3,
//            LoZakai = 4
//        }
//        public enum ZmanHalbashaType
//        {
//            ZakaiKnisa = 1,
//            ZakaiYetiza = 2,
//            ZakaiKnisaYetiza = 3,
//            LoZakai = 4,
//            CardError = 5 //  שגוי סטטוס 
//        }
//        //public void MainInputData(int iMisparIshi, DateTime dCardDate)
//        //{
//        //    //M A I N   P R O C E D U R E
//        //    //Get all oved details and calls all check function

//        //    //DataTable dtErrors = new DataTable();
//        //    clDefinitions oDefinition = new clDefinitions();
//        //    clUtils  oUtils = new clUtils();            
//        //    DataTable dtYamimMeyuchadim;
            
            
//        //    int iSugYom;

//        //    EventLog kdsLog = new EventLog();
//        //    kdsLog.Source = "KDS";
//        //    kdsLog.WriteEntry("1", EventLogEntryType.Error);

//        //    // Write an informational entry to the event log.    
           
//        //    try
//        //    {
//        //        ////Get LookUp Tables
//        //        //dtLookUp = GetLookUpTables();

//        //        //Get Parameters Table
//        //        //dtParameters = GetKdsParametrs();
//        //        dtYamimMeyuchadim = clDefinitions.GetYamimMeyuchadim();

//        //        dtSibotLedivuachYadani = oUtils.GetCtbSibotLedivuchYadani();
                
//        //        iSugYom = clDefinitions.GetSugYom(dtYamimMeyuchadim, dCardDate);

//        //        //Set global variable with parameters
//        //        oParam = new clParameters(dCardDate, iSugYom);  

//        //        //Get Meafyeny Ovdim
//        //        oMeafyeneyOved = new clMeafyenyOved(iMisparIshi, dCardDate);    

//        //        //Get Meafyeney Sug Sidur
//        //        dtSugSidur = clDefinitions.GetSugeySidur();

//        //        //Get Mutamut
//        //        dtMutamut = oUtils.GetCtbMutamut();

//        //        //Get Employee Ishurim
//        //        arrEmployeeApproval = ApprovalRequest.GetMatchingApprovalRequestsWithStatuses(iMisparIshi, dCardDate);

              
//        //        ////Build Error DataTable
//        //        //BuildErrorDataTable(ref dtErrors);

//        //        ////Get Oved Matzav
//        //        //dtMatzavOved = GetOvedMatzav(iMisparIshi, dCardDate);

//        //        //בדיקות ברמת יום עבודה
//        //        //Get yom avoda details
//        //        // dtOvedCardDetails = GetOvedYomAvodaDetails(iMisparIshi, dCardDate);
//        //        SetOvedYomAvodaDetails(iMisparIshi, dCardDate);
//        //        //if (dtOvedCardDetails.Rows.Count>0)
//        //        if (oOvedYomAvodaDetails.OvedDetailsExists)
//        //        {                   
//        //            //Get Oved Details
//        //            dtDetails = oDefinition.GetOvedDetails(iMisparIshi, dCardDate);                    
//        //            if (dtDetails.Rows.Count > 0)
//        //            {
//        //                //Insert Oved Details to Class
//        //                htEmployeeDetails = oDefinition.InsertEmployeeDetails(dtDetails,dCardDate, ref iLastMisaprSidur);                        
//        //            }                    
//        //            CheckAllData(dCardDate, iMisparIshi, iSugYom);                    
//        //        }
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw ex;
//        //    }
//        //}
//        private void SetOvedYomAvodaDetails(int iMisparIshi, DateTime dCardDate)
//        {
//            oOvedYomAvodaDetails = new clOvedYomAvoda(iMisparIshi, dCardDate);
//        }

//        private void CheckAllData(DateTime dCardDate, int iMisparIshi, int iSugYom)
//        {
//            clSidur oSidur= new clSidur();
//            clPeilut oPeilut = new clPeilut();
            
                       
//           // bool bFirstSidur = true;
//            bool bLoLetashlum = false; //כל הסידורים לא לתשלום יהיה TRUE, אחרת FALSE
//            //int iKey = 0;
//            bool bSidurNahagut = false; //מציין אם יש סידור אחד לפחות של נהגות
//            bool bSidurHeadrut = false; //מציין אם יש סידור אחד לפחות של העדרות מסוג מילואים/מחלה או תאונה            
//            bool bSidurZakaiLnesiot = false; //מציין אם  קיים סידור אחד לפחות שיש לו זכאות לזמן נסיעות
//            bool bSidurZakaiLHalbash = false; //מציין אם קיים סידור אחד לפחות שיש לו זכאות להלבשה
//            int iSidurZakaiLehalbashaIndex=-1; //האינדקס של הסידור שזכאי להלבשה
//            bool bSidurLoZakaiLHalbash = false; //מציין אם קיים סידור אחד לפחות שאין לו זכאות להלבשה
//            bool bSidurShaon = false; //קיים סידור אחד לפחות שהוא סידור שעון 
//            //bool bSidurShaonYetizaValid = false; //קיים סידור אחד לפחות שהוא סידור שעון והחתמת יציאה תקינה
//            bool bSidurShaonYadaniNotValid = false; //קיים סידור אחד לפחות שהוא סידור שעון עם החתמה ידנית של כניסה /יציאה ללא אישור
//            bool bKnisaValid = false; //קיים סידור אחד לפחות שהחתמת כניסה תקינה
//            bool bYetizaValid = false; //קיים סידור אחד לפחות שהחתמת יציאה תקינה
//            bool bKnisaNessiaValid = false; //קיים סידור אחד לפחות שהחתמת כניסה תקינה
//            bool bYetizaNessiaValid = false; //קיים סידור אחד לפחות שהחתמת יציאה תקינה
//            float fTotalSidrimTime = 0; //יכיל את כל זמן הסידורים
//            bool bFirstSidurZakaiLenesiot = false;

//            DataRow[] drSugSidur;
//            int iNewMisparMatala, iNewMisparSidur ; //עבור שינוי מספר 01
//            bool bUpdateMisparMatala;

//            oCollYameyAvodaUpd = new COLL_YAMEY_AVODA_OVDIM();
//            oCollSidurimOvdimUpd = new COLL_SIDURIM_OVDIM();
//            oCollSidurimOvdimIns = new COLL_SIDURIM_OVDIM();
//            oCollSidurimOvdimDel = new COLL_SIDURIM_OVDIM();      
//            oCollPeilutOvdimDel = new COLL_OBJ_PEILUT_OVDIM();
//            oCollPeilutOvdimUpd = new COLL_OBJ_PEILUT_OVDIM();
//            oCollPeilutOvdimIns = new COLL_OBJ_PEILUT_OVDIM();

//            try
//            {
//                EventLog kdsLog = new EventLog();
//                kdsLog.Source = "KDS";                
//                //נאתחל את אובייקט ימי עבודה לעדכון
//                oObjYameyAvodaUpd = new OBJ_YAMEY_AVODA_OVDIM();
//                InsertToYameyAvodaForUpdate(dCardDate,ref oObjYameyAvodaUpd, ref oOvedYomAvodaDetails);               
//                //נעבור על כל הסידורים                
//                for (int i = 0; i < htEmployeeDetails.Count; i++)
//                {
//                    oSidur = (clSidur)htEmployeeDetails[i];

//                    drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, dCardDate,  dtSugSidur);                  
//                    //נבדוק אם לסידור יש אישור                    
//                    bHasShaonIsurInMaxLevel = IsHasShaonIshur(ref oSidur);

//                    //נאתחל את אובייקט סידורים לעדכון
//                    oObjSidurimOvdimUpd = new OBJ_SIDURIM_OVDIM();
//                    InsertToObjSidurimOvdimForUpdate(ref oSidur, ref oObjSidurimOvdimUpd);

//                    iNewMisparMatala = 0;
//                    iNewMisparSidur  = 0;
//                    bUpdateMisparMatala = false;                    
//                    //שינוי 01
//                    FixedMisparSidur01(ref oSidur,i, ref iNewMisparMatala,ref iNewMisparSidur, ref bUpdateMisparMatala);
                        
//                    //שינוי 17
//                    //SidurNetzer17(ref oSidur);

//                    //שינוי 11
//                    SidurLoLetashlum11(drSugSidur, ref oSidur,  i);                                      

//                    //שינוי 16
//                    FixedMisparSidurToNihulTnua16(ref  oSidur);


//                    if (i > 0) //if (!bFirstSidur) אם סידור שני ומעלה
//                    {
//                        //נבצע את הבדיקות לכל הסידורים, מלבד הראשון - שינוי 08
//                        FixedSidurHours08(ref oSidur, i - 1);

//                        //שינוי 14
//                        InsertSidurRetzifut14(dCardDate, i - 1, drSugSidur, ref oSidur);
//                    }                    

//                    //שינוי 19
//                    SetHourToSidur19(ref  oSidur);

//                    //11 השדות
//                    //--------

//                    //מחוץ למכסה
//                    UpdateOutMichsa(ref oSidur);

//                    //חריגה
//                    UpdateChariga(ref oSidur);

//                    //השלמה
//                    UpdateHashlamaForSidur(ref oSidur);

//                    //אם סידור אחד לפחות לתשלום, נדליק את  הFLAG
//                    if (oObjSidurimOvdimUpd.LO_LETASHLUM == 0) { bLoLetashlum = true; }

//                    //ברגע שמצאנו סידור נהגות אחד לפחות, לא נמשיך לחפש
//                    // עבור עדכון טכוגרף
//                    if (!bSidurNahagut) { bSidurNahagut = IsSidurNahagut(drSugSidur, oSidur); }

//                    //ברגע שמצאנו סידור העדרות(מסוג מחלה/מילואים/תאונה) אחד לפחות, לא נמשיך לחפש
//                    //עבור עדכון השלמה ליום
//                    if (!bSidurHeadrut) { bSidurHeadrut = IsSidurHeadrut(oSidur); }

//                    //ברגע שמצאנו סידור אחד לפחות שזכאי לזמן נסיעות, נפסיק לחפש
//                    if (!bSidurZakaiLnesiot) 
//                    {
//                        bSidurZakaiLnesiot = IsSidurZakaiLeNesiot(drSugSidur, oSidur);
//                        bFirstSidurZakaiLenesiot = (i == 0);//bFirstSidur;
//                    }

//                    //ברגע שמצאנו סידור אחד לפחות שהוא סידור שעון עם החתמת שעון תקינה )אוטומטית/ידנית), נפסיק לחפש
//                    if (!bSidurShaon) { bSidurShaon = IsSidurShaon(oSidur); }
                                                  
//                    //ברגע שמצאנו סידור אחד לפחות שהוא סידור שעון עם החתמת שעון תקינה )אוטומטית/ידנית), נפסיק לחפש
//                    //if (!bSidurShaonYetizaValid) { bSidurShaonYetizaValid = IsSidurShaonAndYetizaValid(oSidur); }

//                    //נבדוק מצב של החתמת שעון, כניסה/יציאה ידנית, ללא אישור
//                    if (!bSidurShaonYadaniNotValid) { bSidurShaonYadaniNotValid = SidurShaonWithNoApproval(ref oSidur); }

//                    //ביטול נסיעות
//                    if (!bKnisaNessiaValid) { bKnisaNessiaValid = IsKnisaValid(ref oSidur, SIBA_LE_DIVUCH_YADANI_NESIAA); }

//                    if (!bYetizaNessiaValid) { bYetizaNessiaValid = IsYetizaValid(ref oSidur, SIBA_LE_DIVUCH_YADANI_NESIAA); }

//                    //הלבשה
//                    if (!bKnisaValid) { bKnisaValid = IsKnisaValid(ref oSidur, SIBA_LE_DIVUCH_YADANI_HALBASHA); }

//                    if (!bYetizaValid) { bYetizaValid = IsYetizaValid(ref oSidur, SIBA_LE_DIVUCH_YADANI_HALBASHA); }

//                    //אם נמצא לפחות סידור אחד שזכאי להלבשה, נפסיק לחפש
//                    if (!bSidurZakaiLHalbash)
//                    {
//                        bSidurZakaiLHalbash = IsSidurHalbasha(oSidur);
//                        //אם נמצא סידור שזכאי להלבשה, נשמור את האינדקס של הסידור
//                        if (bSidurZakaiLHalbash) { iSidurZakaiLehalbashaIndex = i; }
//                    }
                    
//                    if (!bSidurLoZakaiLHalbash) {bSidurLoZakaiLHalbash = IsNotSidurHalbasha(oSidur); }

//                    //(נסכום את זמני הסידורים (עבור השלמה ליום
//                    //רק עבור עובדים שיש להם קוד נתון 8 עם רק 2,3
//                    if ((oOvedYomAvodaDetails.sMutamut == clGeneral.enMutaam.enMutaam2.GetHashCode().ToString()) || (oOvedYomAvodaDetails.sMutamut == clGeneral.enMutaam.enMutaam3.GetHashCode().ToString()))
//                    {
//                        fTotalSidrimTime = fTotalSidrimTime + clDefinitions.GetSidurTimeInMinuts(oSidur);
//                    }

//                    //נוסיף את אובייקט סידורים לעדכון                   
//                    oCollSidurimOvdimUpd.Add(oObjSidurimOvdimUpd);
                   

//                    //נעבור על כל הפעילויות של הסידור
//                    for (int j = 0; j < ((KdsBatch.clSidur)(htEmployeeDetails[i])).htPeilut.Count; j++)
//                    {
//                        //iKey = int.Parse(dePeilutEntry.Key.ToString());
//                        oPeilut = (clPeilut)oSidur.htPeilut[j];
//                        //נאתחל את אובביקט פעילויות )עבור שינוי 1(
//                        oObjPeilutOvdimUpd = new OBJ_PEILUT_OVDIM();

//                        InsertToObjPeilutOvdimForUpdate(ref oPeilut, ref oSidur, ref oObjPeilutOvdimUpd);   

//                        //עבור שינוי 1, במידה והיה צורך לעדכן את מספר המטלה במספר הסידור הישן )מספר הסידור קיבל מספר חדש(
//                        //נעדכן את הפעילות הראשונה . במקרה כזה לא אמורה להיות יותר מפעילות אחת לסידור
//                        //נעדכן גם את הפעילויות במספר הסידור החדש
//                        if ((bUpdateMisparMatala) && (j==0))
//                        {
//                            oObjPeilutOvdimUpd.MISPAR_MATALA = iNewMisparMatala;
//                            oObjPeilutOvdimUpd.NEW_MISPAR_SIDUR = iNewMisparSidur;
//                            oObjPeilutOvdimUpd.UPDATE_OBJECT = 1;
//                        }
//                        DeleteElementRechev07(ref oSidur, ref oPeilut);                                                                                            
//                        ChangeElement06(ref oPeilut, ref oSidur, j);
                        
//                        //נוסיף את אובייקט פעילות עובדים לעדכון
//                        oCollPeilutOvdimUpd.Add(oObjPeilutOvdimUpd);
//                    }                                         
//                }
               
//                //שינויים ברמת יום עבודה 
//                NewSidurHeadrutWithPaymeny15(iMisparIshi, dCardDate, iSugYom, bLoLetashlum);
                            
//                //טכוגרף - 11 השדות
//                UpdateTachograph(bSidurNahagut);

//                //השלמה ליום - 11 השדות
//                UpdateHashlamaForYomAvoda(bSidurHeadrut,fTotalSidrimTime);

//                //ביטול זמן נסיעות - 11 השדות
//                UpdateBitulZmanNesiot(bSidurZakaiLnesiot, bKnisaNessiaValid, bYetizaNessiaValid,bSidurShaon,  bSidurShaonYadaniNotValid, bSidurNahagut, bFirstSidurZakaiLenesiot, dCardDate);
                                        
//                //שינוי מספר 3  
//                KonnutGrira03(dCardDate);

//                //שינוי מספר 5
//                //AddElementMechine5();

//                //שינוי 20
//                //UpdateZmaneyNesia20();

//                //עבור שינוי מספר 1
//                SetSidurObjects();
                
//                oCollYameyAvodaUpd.Add(oObjYameyAvodaUpd);
//                kdsLog.WriteEntry("10", EventLogEntryType.Error);
//                //נעדכן את ה- DataBase
//                clDefinitions.ShinuyKelet(oCollYameyAvodaUpd, oCollSidurimOvdimUpd, oCollSidurimOvdimIns, oCollSidurimOvdimDel, oCollPeilutOvdimUpd, oCollPeilutOvdimIns, oCollPeilutOvdimDel);

//                //DATABASE-כיוון שחישוב רכיבי השכר אמורים להתבסס על נתונים עדכניים, נבצע את שינויים שמתבססים על רכיבי שכר לאחר שעידכנו את ה- 
//                kdsLog.WriteEntry("11", EventLogEntryType.Error);
//                //Get Calc Rechivim                
//                //dtChishuvYom = objCalc.CalcDayInMonth(iMisparIshi, dCardDate);

//                //11 השדות - הלבשה                
//                UpdateHalbasha(bSidurZakaiLHalbash, bSidurLoZakaiLHalbash, bKnisaValid, bYetizaValid, iSidurZakaiLehalbashaIndex, dCardDate);

//                clDefinitions.UpdateYameyAvodaOvdim(oCollYameyAvodaUpd);
//                clDefinitions.UpdateSidurimOvdim(oCollSidurimOvdimUpd);
//                kdsLog.WriteEntry("12", EventLogEntryType.Error);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
        
//        private void UpdateZmaneyNesia20()
//        {
//            int iZmanNesia = 0;
//            try
//            {                
//                if (oMeafyeneyOved.IsMeafyenExist(51))
//                {
//                    if (!String.IsNullOrEmpty(oMeafyeneyOved.GetMeafyen(51).Value))
//                    {
//                        iZmanNesia = int.Parse(oMeafyeneyOved.GetMeafyen(51).Value.Substring(1));
//                        switch (int.Parse(oMeafyeneyOved.GetMeafyen(51).Value.Substring(0, 1)))
//                        {
//                            case 1:
//                                 oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = iZmanNesia;
//                                 break;
//                            case 2:
//                                 oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = iZmanNesia;
//                                 break;
//                            case 3:
//                                 oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = (int)(Math.Ceiling(iZmanNesia / 2.0));
//                                 oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH;
//                                break;
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void SetSidurObjects()
//        {
//            clNewSidurim oNewSidurim;
//            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
//            try
//            {                
//                //נעבור על האובייקט שמחזיק את כל הסידורים להן השתנה מספר הסידור בעקבות שינוי מספר 1                    
//                for (int i = 0; i < htNewSidurim.Count; i++)
//                {
//                    oNewSidurim = (clNewSidurim)htNewSidurim[i];
//                    //נביא את הסידור עם הנתונים העדכניים
//                    oObjSidurimOvdimUpd = GetSidurOvdimObject(oNewSidurim.SidurIndex);

//                    //נכניס סידור חדש עם כל הנתונים העדכניים ועם מספר הסידור החדש
//                    oObjSidurimOvdimIns = new OBJ_SIDURIM_OVDIM();
                                           
//                    oObjSidurimOvdimIns.MISPAR_ISHI = oObjSidurimOvdimUpd.MISPAR_ISHI;
//                    oObjSidurimOvdimIns.CHARIGA = oObjSidurimOvdimUpd.CHARIGA;
//                    oObjSidurimOvdimIns.HAMARAT_SHABAT = oObjSidurimOvdimUpd.HAMARAT_SHABAT;
//                    if (!oObjSidurimOvdimUpd.HASHLAMAIsNull)
//                    {
//                        oObjSidurimOvdimIns.HASHLAMA = oObjSidurimOvdimUpd.HASHLAMA;
//                    }
//                    oObjSidurimOvdimIns.LO_LETASHLUM = oObjSidurimOvdimUpd.LO_LETASHLUM;
//                    if (!oObjSidurimOvdimUpd.OUT_MICHSAIsNull)
//                    {
//                        oObjSidurimOvdimIns.OUT_MICHSA = oObjSidurimOvdimUpd.OUT_MICHSA;
//                    }
//                    oObjSidurimOvdimIns.PITZUL_HAFSAKA = oObjSidurimOvdimUpd.PITZUL_HAFSAKA;
//                    oObjSidurimOvdimIns.SHAT_GMAR = oObjSidurimOvdimUpd.SHAT_GMAR;
//                    oObjSidurimOvdimIns.SHAT_HATCHALA = oObjSidurimOvdimUpd.SHAT_HATCHALA;
//                    oObjSidurimOvdimIns.TAARICH = oObjSidurimOvdimUpd.TAARICH;
//                    oObjSidurimOvdimIns.VISA = oObjSidurimOvdimUpd.VISA;
//                    oObjSidurimOvdimIns.MEZAKE_NESIOT = oObjSidurimOvdimUpd.MEZAKE_NESIOT;
//                    oObjSidurimOvdimIns.MEZAKE_HALBASHA = oObjSidurimOvdimUpd.MEZAKE_HALBASHA;
//                    oObjSidurimOvdimIns.SHAT_HATCHALA_LETASHLUM = oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM;
//                    oObjSidurimOvdimIns.SHAT_GMAR_LETASHLUM = oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM;
//                    oObjSidurimOvdimIns.MISPAR_SIDUR = oNewSidurim.SidurNew;

//                    oObjSidurimOvdimIns.TOSEFET_GRIRA = oObjSidurimOvdimUpd.TOSEFET_GRIRA;
//                    oObjSidurimOvdimIns.MIKUM_SHAON_KNISA = oObjSidurimOvdimUpd.MIKUM_SHAON_KNISA;
//                    oObjSidurimOvdimIns.MIKUM_SHAON_YETZIA = oObjSidurimOvdimUpd.MIKUM_SHAON_YETZIA;
//                   // oObjSidurimOvdimIns.KM_VISA_LEPREMIA = oObjSidurimOvdimUpd.KM_VISA_LEPREMIA;
//                    oObjSidurimOvdimIns.ACHUZ_KNAS_LEPREMYAT_VISA = oObjSidurimOvdimUpd.ACHUZ_KNAS_LEPREMYAT_VISA;
//                    oObjSidurimOvdimIns.ACHUZ_VIZA_BESIKUN = oObjSidurimOvdimUpd.ACHUZ_VIZA_BESIKUN;
//                    //oObjSidurimOvdimIns.SUG_VISA = oObjSidurimOvdimUpd.SUG_VISA;
//                    oObjSidurimOvdimIns.MISPAR_MUSACH_O_MACHSAN = oObjSidurimOvdimUpd.MISPAR_MUSACH_O_MACHSAN;
//                    oObjSidurimOvdimIns.KOD_SIBA_LEDIVUCH_YADANI_IN = oObjSidurimOvdimUpd.KOD_SIBA_LEDIVUCH_YADANI_IN;
//                    oObjSidurimOvdimIns.KOD_SIBA_LEDIVUCH_YADANI_OUT = oObjSidurimOvdimUpd.KOD_SIBA_LEDIVUCH_YADANI_OUT;
//                    oObjSidurimOvdimIns.KOD_SIBA_LO_LETASHLUM = oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM;
//                    oObjSidurimOvdimIns.SHAYAH_LEYOM_KODEM = oObjSidurimOvdimUpd.SHAYAH_LEYOM_KODEM;
//                    oObjSidurimOvdimIns.MEADKEN_ACHARON = oObjSidurimOvdimUpd.MEADKEN_ACHARON;
//                    oObjSidurimOvdimIns.TAARICH_IDKUN_ACHARON = oObjSidurimOvdimUpd.TAARICH_IDKUN_ACHARON;
//                    oObjSidurimOvdimIns.HEARA = oObjSidurimOvdimUpd.HEARA;
//                    oObjSidurimOvdimIns.MISPAR_SHIUREY_NEHIGA = oObjSidurimOvdimUpd.MISPAR_SHIUREY_NEHIGA;
//                    //oObjSidurimOvdimIns.BUTAL = oObjSidurimOvdimUpd.BUTAL;
//                    oObjSidurimOvdimIns.SUG_HAZMANAT_VISA = oObjSidurimOvdimUpd.SUG_HAZMANAT_VISA;

                    
//                    oCollSidurimOvdimIns.Add(oObjSidurimOvdimIns);

//                    //באובייקט העדכון, נאפס את המשתנה שמציין שיש לעדכן את הרשומה
//                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 0;

//                    //נמחוק את הסידור השגוי    
//                    oObjSidurimOvdimDel = new OBJ_SIDURIM_OVDIM();
//                    oObjSidurimOvdimDel.MISPAR_ISHI = oObjSidurimOvdimUpd.MISPAR_ISHI;
//                    oObjSidurimOvdimDel.SHAT_HATCHALA = oObjSidurimOvdimUpd.SHAT_HATCHALA;//DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatHatchala));
//                    oObjSidurimOvdimDel.TAARICH = oObjSidurimOvdimUpd.TAARICH;
//                    oObjSidurimOvdimDel.MISPAR_SIDUR = oNewSidurim.SidurOld; //מספר סידור קודם
//                    oCollSidurimOvdimDel.Add(oObjSidurimOvdimDel);
//                }                    
//            }
//            catch(Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void InsertToObjSidurimOvdimForInsert(ref clSidur oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimIns)
//        {
//            try
//            {
//                oObjSidurimOvdimIns.MISPAR_SIDUR = oSidur.iMisparSidur;
//                oObjSidurimOvdimIns.MISPAR_ISHI = oSidur.iMisparIshi;
//                oObjSidurimOvdimIns.CHARIGA = string.IsNullOrEmpty(oSidur.sChariga) ? 0 : int.Parse(oSidur.sChariga);
//                oObjSidurimOvdimIns.HAMARAT_SHABAT = string.IsNullOrEmpty(oSidur.sHamaratShabat) ? 0 : int.Parse(oSidur.sHamaratShabat);
//                if (!String.IsNullOrEmpty(oSidur.sHashlama))
//                {
//                    oObjSidurimOvdimIns.HASHLAMA =  int.Parse(oSidur.sHashlama);
//                }
//                oObjSidurimOvdimIns.LO_LETASHLUM = oSidur.iLoLetashlum;
//                if (!String.IsNullOrEmpty(oSidur.sOutMichsa))
//                {
//                    oObjSidurimOvdimIns.OUT_MICHSA = int.Parse(oSidur.sOutMichsa);
//                }
//                oObjSidurimOvdimIns.PITZUL_HAFSAKA = String.IsNullOrEmpty(oSidur.sPitzulHafsaka) ? 0: int.Parse(oSidur.sPitzulHafsaka);
//                oObjSidurimOvdimIns.SHAT_GMAR = oSidur.dFullShatGmar;//DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatGmar));
//                oObjSidurimOvdimIns.SHAT_HATCHALA = oSidur.dFullShatHatchala;//DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatHatchala));
//                oObjSidurimOvdimIns.TAARICH = DateTime.Parse(DateTime.Parse(oSidur.sSidurDate).ToShortDateString());
//                oObjSidurimOvdimIns.VISA = string.IsNullOrEmpty(oSidur.sVisa) ? 0 : int.Parse(oSidur.sVisa);
//                oObjSidurimOvdimIns.MEZAKE_NESIOT = oSidur.iMezakeNesiot;
//                oObjSidurimOvdimIns.SHAT_HATCHALA_LETASHLUM = oSidur.dFullShatHatchalaLetashlum; //DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatHatchalaLetashlum));
//                oObjSidurimOvdimIns.SHAT_GMAR_LETASHLUM = oSidur.dFullShatGmarLetashlum;//DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatGmarLetashlum));                
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void InsertToObjSidurimOvdimForUpdate(ref clSidur oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd)
//        {
//            try
//            {
//                oObjSidurimOvdimUpd.MISPAR_SIDUR = oSidur.iMisparSidur;
//                oObjSidurimOvdimUpd.NEW_MISPAR_SIDUR = oSidur.iMisparSidur;
//                oObjSidurimOvdimUpd.MISPAR_ISHI = oSidur.iMisparIshi;
//                if (!String.IsNullOrEmpty(oSidur.sChariga))
//                {
//                 oObjSidurimOvdimUpd.CHARIGA =int.Parse(oSidur.sChariga);
//                }

//                if (!String.IsNullOrEmpty(oSidur.sHamaratShabat))
//                {
//                    oObjSidurimOvdimUpd.HAMARAT_SHABAT = int.Parse(oSidur.sHamaratShabat);
//                }

//                if (!String.IsNullOrEmpty(oSidur.sHashlama))
//                {
//                    oObjSidurimOvdimUpd.HASHLAMA = int.Parse(oSidur.sHashlama);
//                }

//                oObjSidurimOvdimUpd.LO_LETASHLUM = oSidur.iLoLetashlum;
//                if (!String.IsNullOrEmpty(oSidur.sOutMichsa))
//                {
//                    oObjSidurimOvdimUpd.OUT_MICHSA = int.Parse(oSidur.sOutMichsa);
//                }

//                if (!String.IsNullOrEmpty(oSidur.sPitzulHafsaka))
//                {
//                    oObjSidurimOvdimUpd.PITZUL_HAFSAKA =  int.Parse(oSidur.sPitzulHafsaka);
//                }
//                if (oSidur.dFullShatGmar != DateTime.MinValue)
//                {
//                    oObjSidurimOvdimUpd.SHAT_GMAR = oSidur.dFullShatGmar;//DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatGmar));
//                }
//                oObjSidurimOvdimUpd.SHAT_HATCHALA = oSidur.dFullShatHatchala;//DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatHatchala));
//                oObjSidurimOvdimUpd.TAARICH = DateTime.Parse(DateTime.Parse(oSidur.sSidurDate).ToShortDateString());
//                if (!String.IsNullOrEmpty(oSidur.sVisa))
//                {
//                    oObjSidurimOvdimUpd.VISA = int.Parse(oSidur.sVisa);
//                }
//                oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = oSidur.dFullShatHatchalaLetashlum; //DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatHatchalaLetashlum));
//                oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = oSidur.dFullShatGmarLetashlum;//DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatGmarLetashlum));
//                oObjSidurimOvdimUpd.MEZAKE_NESIOT = oSidur.iMezakeNesiot;
                
//                oObjSidurimOvdimUpd.TOSEFET_GRIRA = oSidur.iTosefetGrira;
//                if (!String.IsNullOrEmpty(oSidur.sMikumShaonKnisa))
//                {
//                    oObjSidurimOvdimUpd.MIKUM_SHAON_KNISA = int.Parse(oSidur.sMikumShaonKnisa);
//                }

//                if (!String.IsNullOrEmpty(oSidur.sMikumShaonYetzia))
//                {
//                    oObjSidurimOvdimUpd.MIKUM_SHAON_YETZIA = int.Parse(oSidur.sMikumShaonYetzia);
//                }

//                //oObjSidurimOvdimUpd.KM_VISA_LEPREMIA = oSidur.iKmVisaLepremia;                
//                oObjSidurimOvdimUpd.ACHUZ_KNAS_LEPREMYAT_VISA = oSidur.iAchuzKnasLepremyatVisa;
//                oObjSidurimOvdimUpd.ACHUZ_VIZA_BESIKUN = oSidur.iAchuzVizaBesikun;
//                //oObjSidurimOvdimUpd.SUG_VISA = oSidur.iSugVisa;
//                oObjSidurimOvdimUpd.MISPAR_MUSACH_O_MACHSAN = oSidur.iMisparMusachOMachsan;
//                oObjSidurimOvdimUpd.KOD_SIBA_LEDIVUCH_YADANI_IN = oSidur.iKodSibaLedivuchYadaniIn;
//                oObjSidurimOvdimUpd.KOD_SIBA_LEDIVUCH_YADANI_OUT = oSidur.iKodSibaLedivuchYadaniOut;
//                oObjSidurimOvdimUpd.KOD_SIBA_LO_LETASHLUM = oSidur.iKodSibaLoLetashlum;
//                oObjSidurimOvdimUpd.SHAYAH_LEYOM_KODEM = oSidur.iShayahLeyomKodem;
//                oObjSidurimOvdimUpd.MEADKEN_ACHARON = oSidur.lMeadkenAcharon;
//                oObjSidurimOvdimUpd.TAARICH_IDKUN_ACHARON = oSidur.dTaarichIdkunAcharon;
//                oObjSidurimOvdimUpd.HEARA = oSidur.sHeara;
//                oObjSidurimOvdimUpd.MISPAR_SHIUREY_NEHIGA = oSidur.iMisparShiureyNehiga;
//                //oObjSidurimOvdimUpd.BUTAL = oSidur.iButal;
//                oObjSidurimOvdimUpd.SUG_HAZMANAT_VISA = oSidur.iSugHazmanatVisa;
                
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void InsertToObjSidurimOvdimForDelete(ref clSidur oSidur, ref OBJ_SIDURIM_OVDIM oObjSidurimOvdimDel)
//        {
//            try
//            {
//                oObjSidurimOvdimDel.MISPAR_SIDUR = oSidur.iMisparSidur;
//                oObjSidurimOvdimDel.MISPAR_ISHI = oSidur.iMisparIshi;
//                oObjSidurimOvdimDel.SHAT_HATCHALA = oSidur.dFullShatHatchala;//DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", oSidur.sShatHatchala));
//                oObjSidurimOvdimDel.TAARICH = DateTime.Parse(DateTime.Parse(oSidur.sSidurDate).ToShortDateString());               
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void InsertToObjPeilutOvdimForInsert(ref clSidur oSidur, ref OBJ_PEILUT_OVDIM oObjPeilutOvdimIns)
//        {
//            try
//            {
//                oObjPeilutOvdimIns.MISPAR_ISHI = oSidur.iMisparIshi;
//                oObjPeilutOvdimIns.TAARICH = DateTime.Parse(DateTime.Parse(oSidur.sSidurDate).ToShortDateString());
//                oObjPeilutOvdimIns.MISPAR_SIDUR = oSidur.iMisparSidur;
//                oObjPeilutOvdimIns.SHAT_HATCHALA_SIDUR = oSidur.dFullShatHatchala;                
//                oObjPeilutOvdimIns.MISPAR_KNISA = 0;
//                oObjPeilutOvdimIns.MEADKEN_ACHARON = -2;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void InsertToObjPeilutOvdimForUpdate(ref clPeilut oPeilut, ref clSidur oSidur, ref OBJ_PEILUT_OVDIM oObjPeilutOvdimUpd)                                                     
//        {
//            try
//            {
//                oObjPeilutOvdimUpd.MISPAR_SIDUR = oSidur.iMisparSidur;
//                oObjPeilutOvdimUpd.MISPAR_ISHI = oSidur.iMisparIshi;
//                oObjPeilutOvdimUpd.SHAT_HATCHALA_SIDUR = oSidur.dFullShatHatchala;
//                oObjPeilutOvdimUpd.SHAT_YETZIA = DateTime.Parse(string.Concat(oSidur.dFullShatHatchala.ToShortDateString(), " ", oPeilut.sShatYetzia));
//                oObjPeilutOvdimUpd.TAARICH = DateTime.Parse(DateTime.Parse(oSidur.sSidurDate).ToShortDateString());
//                oObjPeilutOvdimUpd.MISPAR_KNISA = oPeilut.iMisparKnisa;
//                oObjPeilutOvdimUpd.MAKAT_NESIA = oPeilut.lMakatNesia;
//                oObjPeilutOvdimUpd.NEW_MISPAR_SIDUR = oSidur.iMisparSidur;                
//                oObjPeilutOvdimUpd.MEADKEN_ACHARON = -2;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void InsertToObjPeilutOvdimForDelete(ref clPeilut oPeilut, ref clSidur oSidur, ref OBJ_PEILUT_OVDIM oObjPeilutOvdimDel)
//        {
//            try
//            {
//                oObjPeilutOvdimDel.MISPAR_ISHI = oSidur.iMisparIshi;
//                oObjPeilutOvdimDel.MISPAR_SIDUR = oPeilut.iPeilutMisparSidur;
//                oObjPeilutOvdimDel.TAARICH = oPeilut.dCardDate;
//                oObjPeilutOvdimDel.SHAT_HATCHALA_SIDUR = oSidur.dFullShatHatchala;
//                oObjPeilutOvdimDel.SHAT_YETZIA = DateTime.Parse(string.Concat(oSidur.dFullShatHatchala.ToShortDateString(), " ", oPeilut.sShatYetzia));
//                oObjPeilutOvdimDel.MISPAR_KNISA = oPeilut.iMisparKnisa;
//                oObjPeilutOvdimDel.MAKAT_NESIA = oPeilut.lMakatNesia;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void InsertToYameyAvodaForUpdate(DateTime dCardDate, ref OBJ_YAMEY_AVODA_OVDIM oObjYameyAvodaUpd, ref clOvedYomAvoda oOvedYomAvodaDetails)
//        {
//            try
//            {
//                oObjYameyAvodaUpd.MISPAR_ISHI = oOvedYomAvodaDetails.iMisparIshi;
//                oObjYameyAvodaUpd.TACHOGRAF = oOvedYomAvodaDetails.sTachograf;
//                if (!String.IsNullOrEmpty(oOvedYomAvodaDetails.sHalbasha))
//                {
//                    oObjYameyAvodaUpd.HALBASHA =  int.Parse(oOvedYomAvodaDetails.sHalbasha);
//                }
//                if (!String.IsNullOrEmpty(oOvedYomAvodaDetails.sHashlamaLeyom))
//                {
//                    oObjYameyAvodaUpd.HASHLAMA_LEYOM = int.Parse(oOvedYomAvodaDetails.sHashlamaLeyom);
//                }
//                if (!String.IsNullOrEmpty(oOvedYomAvodaDetails.sBitulZmanNesiot))
//                {
//                    oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT =  int.Parse(oOvedYomAvodaDetails.sBitulZmanNesiot);
//                }
//                if (!String.IsNullOrEmpty(oOvedYomAvodaDetails.sLina))
//                {
//                    oObjYameyAvodaUpd.LINA = int.Parse(oOvedYomAvodaDetails.sLina);
//                }
//                oObjYameyAvodaUpd.TAARICH = dCardDate;
//                oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = oOvedYomAvodaDetails.iZmanNesiaHaloch;
//                oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = oOvedYomAvodaDetails.iZmanNesiaHazor;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void KonnutGrira03(DateTime dCardDate)
//        {
//            //כוננות גרירה
//            //סימון כוננות גרירה "לא לתשלום". 
//            //גרירה בפועל
//            //הוספת קודים לפעילות גרירה בפועל:
//            //"זמן נסיעה", "השלמה
            
//            clSidur oSidur;
//            bool bSidurKonnutGrira = false;
//            int iSidurKonnutGrira = 0;            
//            int[] arrSidurActualKonnutGrira= new int[0];
            
//            try
//            {
//                //נעבור על כל הסידורים ונבדוק שיש כוננות גרירה וכוננות גרירה בפועל
//                for (int i = 0; i < htEmployeeDetails.Count; i++)
//                {
//                    oSidur = (clSidur)htEmployeeDetails[i];
                    
//                    //נבדוק אם סידור גרירה
//                    if (IsKonnutGrira(ref oSidur, dCardDate))
//                    {
//                        bSidurKonnutGrira = true;
//                        iSidurKonnutGrira = i;
//                    }

//                    //אם נמצא  סידור של כוננות גרירה, נחפש סידורים של כוננות גרירה בפועל ונשמור את האינדקס שלהם במערך
//                    //נבדוק אם סידור גרירה בפועל בטווח הזמן של סידור הגרירה
//                    if ((bSidurKonnutGrira) && (iSidurKonnutGrira != i))
//                    {
//                        if (IsActualKonnutGrira(ref oSidur, iSidurKonnutGrira))
//                        {                           
//                            Array.Resize(ref arrSidurActualKonnutGrira, arrSidurActualKonnutGrira.Length + 1);                              
//                            arrSidurActualKonnutGrira[arrSidurActualKonnutGrira.Length-1] = i;                            
//                        }
//                    }
//                }
//                //אם יש סידור כוננות גרירה  וגם לפחות סידור גרירה בפועל אחד 
//                if ((bSidurKonnutGrira) && (arrSidurActualKonnutGrira.Length>0))
//                {
//                    SetLoLetashlum(iSidurKonnutGrira);
//                    SetBitulZmanNesiot(iSidurKonnutGrira,arrSidurActualKonnutGrira);
//                    SetZmanHashlama(iSidurKonnutGrira, arrSidurActualKonnutGrira);
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void AddElementMechine5()
//        {
//            clSidur oSidur;
//            //הוספת אלמנט הכנת מכונה אם העובד החליף רכב ולא מופיע אלמנט זה או בתחילת יום
//            try
//            {                                
//                for (int i = 0; i < htEmployeeDetails.Count; i++)
//                {
//                    oSidur = (clSidur)htEmployeeDetails[i];
//                    //אם סידור ראשון ביום קיימים שני מקרים
//                    if (i == 0)
//                    {
//                        AddElementMachineForFirstSidur(ref oSidur);
//                    }
//                    else //סידור נוסף
//                    {
//                        AddElementMachineForNextSidur(ref oSidur,  i);
//                    }                    
//                }

//                if (oCollPeilutOvdimIns.Count > 0)
//                {
//                    //Error86();
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void Error86()
//        {
//            clSidur oSidur;
//            clPeilut oPeilut;

//            try
//            {
//                for (int i = 0; i < htEmployeeDetails.Count; i++)
//                {
//                    oSidur = (clSidur)htEmployeeDetails[i];
//                    for (int j = 0; j < ((KdsBatch.clSidur)(htEmployeeDetails[i])).htPeilut.Count; j++)
//                    {                        
//                        oPeilut = (clPeilut)oSidur.htPeilut[j];
//                        CountElementHachanatMechona(ref oPeilut, ref oSidur, i,j);
//                    }                        
//                }
//            }
//            catch(Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void CountElementHachanatMechona(ref clPeilut oPeilut, ref clSidur oSidur,int iSidurIndex, int iPeilutIndex)
//        {
//            int iTotalTimePrepareMechineForDay=0;
//            int iTotalTimePrepareMechineForSidur=0;
//            int iTotalTimePrepareMechineForOtherMechines = 0;
//            int iElementType = 0;
//            int iElementTime = 0;
//            try                
//            {
//                if ((oPeilut.iMakatType == enMakatType.mElement.GetHashCode()) && (!String.IsNullOrEmpty(oPeilut.sShatYetzia)))
//                {
//                    if (oPeilut.lMakatNesia.ToString().Length >= 6) //711XXX,701XXX
//                    {
//                        iElementType = int.Parse(oPeilut.lMakatNesia.ToString().Substring(0, 3));
//                        if ((iElementType == 701) || (iElementType == 711))
//                        {
//                            iElementTime = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));
//                            if ((iElementType == 701) && (int.Parse(oPeilut.sShatYetzia.Remove(2, 1)) < 8))
//                            {
//                                //מכונה ראשונה ביום- נשווה לפרמטר 120
//                                if (iElementTime > oParam.iPrepareFirstMechineMaxTime)
//                                {
//                                    //נאפס את אלמנט המכונה
//                                    UpdateElementTime(ref oPeilut, ref oSidur, iSidurIndex, iPeilutIndex);
//                                }
//                                //צבירת זמן כל המכונות ביום ראשונה ונוספות נשווה לפרמטר 123
//                                iTotalTimePrepareMechineForDay = iTotalTimePrepareMechineForDay + iElementTime;
//                                if (iTotalTimePrepareMechineForDay > oParam.iPrepareAllMechineTotalMaxTimeInDay)
//                                {
//                                    //נאפס את אלמנט המכונה
//                                    UpdateElementTime(ref oPeilut, ref oSidur, iSidurIndex, iPeilutIndex);
//                                }
//                                //צבירת זמן כלל המכונות לסידור נשווה מול פרמטר 124
//                                iTotalTimePrepareMechineForSidur = iTotalTimePrepareMechineForSidur + iElementTime;
//                                 //נשווה את זמן כל האלמנטים של הכנת מכונה בסידור לפרמטר 124, אם עלה על המותר נעלה שגיאה
//                                if (iTotalTimePrepareMechineForSidur > oParam.iPrepareAllMechineTotalMaxTimeForSidur)
//                                {
//                                    //נאפס את אלמנט המכונה
//                                    UpdateElementTime(ref oPeilut, ref oSidur, iSidurIndex, iPeilutIndex);
//                                }
//                            }
//                            else
//                            {
//                                //מכונות נוספות נשווה לפרמטר 121
//                                if (iElementTime > oParam.iPrepareOtherMechineMaxTime)
//                                {
//                                    //נאפס את אלמנט המכונה
//                                    UpdateElementTime(ref oPeilut, ref oSidur, iSidurIndex, iPeilutIndex);
//                                }
//                                //צבירת זמן כל המכונות ביום ראשונה ונוספות נשווה לפרמטר 123
//                                iTotalTimePrepareMechineForDay = iTotalTimePrepareMechineForDay + iElementTime;
//                                if (iTotalTimePrepareMechineForDay > oParam.iPrepareAllMechineTotalMaxTimeInDay)
//                                {
//                                    //נאפס את אלמנט המכונה
//                                    UpdateElementTime(ref oPeilut, ref oSidur, iSidurIndex, iPeilutIndex);
//                                }
//                                //צבירת זמן כלל המכונות לסידור נשווה מול פרמטר 124
//                                iTotalTimePrepareMechineForSidur = iTotalTimePrepareMechineForSidur + iElementTime;
//                                //נשווה את זמן כל האלמנטים של הכנת מכונה בסידור לפרמטר 124, אם עלה על המותר נעלה שגיאה
//                                if (iTotalTimePrepareMechineForSidur > oParam.iPrepareAllMechineTotalMaxTimeForSidur)
//                                {
//                                    //נאפס את אלמנט המכונה
//                                    UpdateElementTime(ref oPeilut, ref oSidur, iSidurIndex, iPeilutIndex);
//                                }

//                                if (iElementType == 711)
//                                {
//                                    //צבירת זמן כל המכונות הנוספות - נשווה בסוף מול פרמטר 122
//                                    iTotalTimePrepareMechineForOtherMechines = iTotalTimePrepareMechineForOtherMechines + iElementTime;
//                                     //נשווה את זמן כל האלמנטים של הכנת מכונה נוספות ביום לפרמטר 122, אם עלה על המותר נעלה שגיאה
//                                    if (iTotalTimePrepareMechineForOtherMechines > oParam.iPrepareOtherMechineTotalMaxTime)
//                                    {
//                                        //נאפס את אלמנט המכונה
//                                        UpdateElementTime(ref oPeilut, ref oSidur, iSidurIndex, iPeilutIndex);
//                                    }
//                                }
//                            }                           
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void UpdateElementTime(ref clPeilut oPeilut, ref clSidur oSidur, int iSidurIndex, int iPeilutIndex)
//        {
//            long lMakatNesia=0;
//            string sMakatNesia="";
//            try
//            {
//                OBJ_PEILUT_OVDIM oObjPeilutOvdimUpd;
//                oObjPeilutOvdimUpd = GetPeilutOvdimObject(iSidurIndex, iPeilutIndex);

//                if (oObjPeilutOvdimUpd == null)
//                {
//                    oObjPeilutOvdimUpd = new OBJ_PEILUT_OVDIM();
//                    InsertToObjPeilutOvdimForUpdate(ref oPeilut, ref oSidur, ref oObjPeilutOvdimUpd);
//                }
//                lMakatNesia = oPeilut.lMakatNesia;
//                sMakatNesia = lMakatNesia.ToString();
//                sMakatNesia = string.Concat(sMakatNesia.Substring(0,3) , "000" , sMakatNesia.Substring(6));
                
//                oObjPeilutOvdimUpd.MAKAT_NESIA = long.Parse(sMakatNesia);
//                oCollPeilutOvdimUpd.Add(oObjPeilutOvdimUpd);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void AddElementMachineForFirstSidur(ref clSidur oSidur)
//        {            
//            bool bElementHachanatMechonaExists=false;
//            bool bPeilutNesiaMustBusNumber = false;
//            int iPeilutNesiaIndex=0;
//            long lOtoNo = 0;
            

//            //אם זה הסידור הראשון וקיימת נסיעת שירות, נמ"ק , ריקה או אלמנט שדורש הכנת רכב וגם לא קיימת פעילות הכנת מכונה
//            IsSidurHasPeilutHachanatMechona(ref oSidur, ref bPeilutNesiaMustBusNumber, ref bElementHachanatMechonaExists, ref iPeilutNesiaIndex, ref lOtoNo);                                          
                                             
//            //אם נמצאה פעילות מסוג נסיעה או אלמנט הדורש מספר רכב וגם לא נמצאה פעילות של הכנת מכונה
//            //נכניס אלמנט הכנסת מכונה. נבדיל בין שני מקרים: סידור ראשון המתחיל לפני )08:00 בבוקר וסידור ראשון שמתחיל אחרי 08:00 בבוקר
//            if ((bPeilutNesiaMustBusNumber) && (!bElementHachanatMechonaExists))
//            {
//                //dRefferenceDate = DateTime.Parse(string.Concat(oSidur.dFullShatHatchala.ToShortDateString(), " 08:00"));
//                //if (oSidur.dFullShatHatchala < dRefferenceDate)
//                //{
//                    //אם סידור הוא ראשון ביום, מתחיל לפני 08:00 ואין לו אלמנט הכנת מכונה מכל סוג שהוא (701, 711, 712) - להוסיף לו אלמנט הכנת מכונה ראשונה (70100000).  
//                    //זמן האלמנט ייקבע לפי הערך לפרמטר 120 (זמן הכנת מכונה ראשונה) בטבלת פרמטרים חיצוניים. שעת היציאה של פעילות האלמנט תחושב באופן הבא: יש לקחת את שעת היציאה של הפעילות העוקבת לאלמנט החדש שהוספנו ולהחסיר ממנה את זמן האלמנט שהוספנו.
//                    AddElementHachanatMechine701(iPeilutNesiaIndex);
//                //}
//                //else
//                //{
//                //    //אם סידור הוא ראשון ביום, מתחיל בשעה 08:00 ומעלה ואין לו אלמנט הכנת מכונה – להוסיף אלמנט הכנת מכונה נוספת (71100000). זמן האלמנט ייקבע לפי הערך לפרמטר 121 (זמן הכנת מכונה נוספת) בטבלת פרמטרים חיצוניים. שעת היציאה של פעילות האלמנט תחושב באופן הבא: יש לקחת את שעת היציאה של הפעילות העוקבת לאלמנט החדש שהוספנו ולהחסיר ממנה את זמן האלמנט שהוספנו
//                //    AddElementHachanatMechine711(ref oSidur,iPeilutNesiaIndex);
//                //}
//            }
//        }

//        private void AddElementHachanatMechine701(int iPeilutNesiaIndex)
//        {
//            OBJ_PEILUT_OVDIM oObjPeilutOvdimIns = new OBJ_PEILUT_OVDIM();
//            clPeilut oPeilutNext;
//            clSidur oSidur;
//            DateTime  dRefferenceDate;
//            try
//            {               
//                oSidur = (clSidur)htEmployeeDetails[0];//הסידור הראשון
//                oPeilutNext = (clPeilut)oSidur.htPeilut[iPeilutNesiaIndex];
//                InsertToObjPeilutOvdimForInsert(ref oSidur, ref oObjPeilutOvdimIns);
//                dRefferenceDate = DateTime.Parse(string.Concat(oSidur.dFullShatHatchala.ToShortDateString(), " 08:00"));
//                if (oSidur.dFullShatHatchala < dRefferenceDate)
//                {
//                    oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("701", oParam.iPrepareFirstMechineMaxTime.ToString().PadLeft(3, (char)48), "00"));
//                    oObjPeilutOvdimIns.SHAT_YETZIA = oPeilutNext.dFullShatYetzia.AddMinutes(-(oParam.iPrepareFirstMechineMaxTime + oPeilutNext.iKisuyTor));
//                }
//                else
//                {
//                    oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("701", oParam.iPrepareOtherMechineMaxTime.ToString().PadLeft(3, (char)48), "00"));
//                    oObjPeilutOvdimIns.SHAT_YETZIA = oPeilutNext.dFullShatYetzia.AddMinutes(-(oParam.iPrepareOtherMechineMaxTime + oPeilutNext.iKisuyTor));
//                }
                
//                oCollPeilutOvdimIns.Add(oObjPeilutOvdimIns);
//                DeleteDoublePeilut(ref oSidur, iPeilutNesiaIndex, oObjPeilutOvdimIns.SHAT_YETZIA, oPeilutNext.iKisuyTor);   
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void AddElementMachineForNextSidur(ref clSidur oSidur, int iSidurIndex)
//        {
//            clPeilut oPeilut;
//            clSidur oLocalSidur;
//            bool bElementHachanatMechonaExists = false;
//            bool bPeilutNesiaMustBusNumber = false;
//            bool bPeilutNesiaExists = false;
//            int iPeilutNesiaIndex = 0;            
//            long lOtoNo = 0;

//            //אם קיימת נסיעת שירות, נמ"ק , ריקה או אלמנט שדורש הכנת רכב וגם לא קיימת פעילות הכנת מכונה
//            IsSidurHasPeilutHachanatMechona(ref oSidur, ref bPeilutNesiaMustBusNumber, ref bElementHachanatMechonaExists, ref iPeilutNesiaIndex, ref lOtoNo);

//            //אם נמצאה פעילות מסוג נסיעה או אלמנט הדורש מספר רכב וגם לא נמצאה פעילות של הכנת מכונה           
//            if ((bPeilutNesiaMustBusNumber) && (!bElementHachanatMechonaExists))
//            {
//             //מגיעים לסידור נוסף עם פעילות נסיעה שאין לפניה אלמנט הכנת מכונה נוספת (71100000)  הולכים לפעילות הנסיעה הקודמת (אם זו הפעילות הראשונה בסידור אז מחפשים פעילות נסיעה בסידור שקדם לסידור אותו אנו בודקים)  אם אין להן אותו מספר רכב אז מוסיפים אלמנט הכנת מכונה (71100000).
//             //זמן האלמנט ייקבע לפי הערך לפרמטר 121 (זמן הכנת מכונה נוספת) בטבלת פרמטרים חיצוניים. שעת היציאה של פעילות האלמנט תחושב באופן הבא: יש לקחת את שעת היציאה של הפעילות העוקבת לאלמנט החדש שהוספנו ולהחסיר ממנה את זמן האלמנט שהוספנו
               
//                //נעבור על כל הפעילויות בסידור מהפעילות שמצאנו שדורשת אלמנט הכנת מכונה  ועד הפעילות הראשונה
//                //נחפש את הפעילות הקודמת שדורשת מספר רכב
//                for (int j = iPeilutNesiaIndex-1; j >= 0; j--)
//                {
//                    oPeilut = (clPeilut)oSidur.htPeilut[j];
//                    if (oPeilut.IsMustBusNumber())
//                    {
//                        bPeilutNesiaExists = true;
//                        if (oPeilut.lOtoNo != lOtoNo)
//                        {
//                            //אם אין להן אותו מספר רכב אז מוסיפים אלמנט הכנת מכונה (71100000).
//                            //זמן האלמנט ייקבע לפי הערך לפרמטר 121 (זמן הכנת מכונה נוספת) בטבלת פרמטרים חיצוניים. שעת היציאה של פעילות האלמנט תחושב באופן הבא: יש לקחת את שעת היציאה של הפעילות העוקבת לאלמנט החדש שהוספנו ולהחסיר ממנה את זמן האלמנט שהוספנו
//                            AddElementHachanatMechine711(ref oSidur, iPeilutNesiaIndex);
//                            break;
//                        }
//                    }
//                }

//                if (!bPeilutNesiaExists)
//                {
//                    //אם לא מצאנו פעילות שדורשת מספר רכב בסידור הנוכחי, נבדוק בסידורים קודמים
//                    for (int i = iSidurIndex-1; i >= 0 ; i--)
//                    {
//                        oLocalSidur = (clSidur)htEmployeeDetails[i];
//                        for (int j = oLocalSidur.htPeilut.Count-1; j>=0 ; j--)
//                        {
//                            oPeilut = (clPeilut)oLocalSidur.htPeilut[j];
//                            if (oPeilut.IsMustBusNumber())
//                            {                               
//                                if (oPeilut.lOtoNo != lOtoNo)
//                                {
//                                    //אם אין להן אותו מספר רכב אז מוסיפים אלמנט הכנת מכונה (71100000).
//                                    //זמן האלמנט ייקבע לפי הערך לפרמטר 121 (זמן הכנת מכונה נוספת) בטבלת פרמטרים חיצוניים. שעת היציאה של פעילות האלמנט תחושב באופן הבא: יש לקחת את שעת היציאה של הפעילות העוקבת לאלמנט החדש שהוספנו ולהחסיר ממנה את זמן האלמנט שהוספנו
//                                    AddElementHachanatMechine711(ref oLocalSidur, iPeilutNesiaIndex);
//                                    break;
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//        }

//        private void IsSidurHasPeilutHachanatMechona(ref clSidur oSidur, ref bool bPeilutNesiaMustBusNumber, 
//                                                     ref bool bElementHachanatMechonaExists,
//                                                     ref int iPeilutNesiaIndex,
//                                                     ref long lOtoNo)                                                    
//        {
//            clPeilut oPeilut;
            
//            try
//            {
//                for (int j = 0; j < oSidur.htPeilut.Count; j++)
//                {
//                    oPeilut = (clPeilut)oSidur.htPeilut[j];
//                    if (oPeilut.IsMustBusNumber())
//                    {
//                        bPeilutNesiaMustBusNumber = true;
//                        iPeilutNesiaIndex = j;
//                        lOtoNo = oPeilut.lOtoNo;
//                        break;
//                    }
//                    if (oPeilut.lMakatNesia.ToString().Length >= 3)
//                    {
//                        if (!bElementHachanatMechonaExists)
//                        {
//                            bElementHachanatMechonaExists = oPeilut.bElementHachanatMechona;
//                        }
//                        //sMakatNesia = oPeilut.lMakatNesia.ToString().Substring(0, 3);
//                        //if ((sMakatNesia.Equals(clGeneral.enElementHachanatMechona.Element701))
//                        //    || (sMakatNesia.Equals(clGeneral.enElementHachanatMechona.Element711))
//                        //    || (sMakatNesia.Equals(clGeneral.enElementHachanatMechona.Element712)))
//                        //{
//                        //    bElementHachanatMechonaExists = true;
//                        //}
//                    }
//                }
//            }
//            catch(Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void AddElementHachanatMechine711(ref clSidur oSidur, int iPeilutNesiaIndex)
//        {
//            OBJ_PEILUT_OVDIM oObjPeilutOvdimIns = new OBJ_PEILUT_OVDIM();
//            clPeilut oPeilutNext;
            
//            try
//            {
//                //1. שעת היציאה של פעילות האלמנט תחושב באופן הבא: יש לקחת את שעת היציאה של הפעילות העוקבת לאלמנט החדש שהוספנו ולהחסיר ממנה את זמן האלמנט שהוספנו.                               2. בהמשך לסעיף 1, אם לפעילות שבגללה הוספנו אלמנט הכנת מכונה קיים ערך בשדה כיסוי תור, עבור קביעת שעת היציאה של האלמנט יש להחסיר גם את זמן כיסוי התור.                
//                oPeilutNext = (clPeilut)oSidur.htPeilut[iPeilutNesiaIndex];
//                InsertToObjPeilutOvdimForInsert(ref oSidur, ref oObjPeilutOvdimIns);
//                oObjPeilutOvdimIns.MAKAT_NESIA = long.Parse(String.Concat("711", oParam.iPrepareOtherMechineMaxTime.ToString().PadLeft(3,(char)48), "00"));
//                oObjPeilutOvdimIns.SHAT_YETZIA = oPeilutNext.dFullShatYetzia.AddMinutes(-(oParam.iPrepareOtherMechineMaxTime + oPeilutNext.iKisuyTor));
//                //לאחר שקבענו את שעת היציאה של האלמנט, אם קיימת פעילות נוספת (שהיא לא אלמנט הכנת מכונה מכל סוג שהוא) שהתחילה באותה שעת יציאה שקבענו לאלמנט, או בשעה מאוחרת יותר, והיא מסתיימת לפני שעת היציאה של כיסוי התור, והפעילות הזו היא אלמנט עם מאפיין 8 (ביטול בגלל איחור לסידור) אז יש לבטל את הפעילות הזו.
//                //נעבור על כל הפעילויות אחורה )רק של הסידור(, ונבדוק אם קיים לו מאפיין 8 וגם שעת היציאה גדולה שווה לאלמנט החדש
//                oCollPeilutOvdimIns.Add(oObjPeilutOvdimIns);
//                DeleteDoublePeilut(ref oSidur, iPeilutNesiaIndex, oObjPeilutOvdimIns.SHAT_YETZIA, oPeilutNext.iKisuyTor);   
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void DeleteDoublePeilut(ref clSidur oSidur, int iPeilutNesiaIndex, DateTime dNewPeilutShatYetiza, int iPeilutNesiaKisuyTor)
//        {
//            clPeilut oPeilut;            
//            int iZmanPeilut=0;
//            //לאחר שקבענו את שעת היציאה של האלמנט, אם קיימת פעילות נוספת (שהיא לא אלמנט הכנת מכונה מכל סוג שהוא) שהתחילה באותה שעת יציאה שקבענו לאלמנט, או בשעה מאוחרת יותר, והיא מסתיימת לפני שעת היציאה של כיסוי התור, והפעילות הזו היא אלמנט עם מאפיין 8 (ביטול בגלל איחור לסידור) אז יש לבטל את הפעילות הזו.
//            //נעבור על כל הפעילויות אחורה )רק של הסידור(, ונבדוק אם קיים לו מאפיין 8 וגם שעת היציאה גדולה שווה לאלמנט החדש
//            for (int j = iPeilutNesiaIndex; j < 0; j--)
//            {
//                oPeilut = (clPeilut)oSidur.htPeilut[j];
//                //אם פעילות מסוג אלמנט, אבל לא אלמנט הכנת מכונה
//                if ((oPeilut.iMakatType == enMakatType.mElement.GetHashCode()) && (!oPeilut.bElementHachanatMechona))
//                {
//                    iZmanPeilut = int.Parse(oPeilut.lMakatNesia.ToString().Substring(3, 3));
//                    //אם קיים מאפייו 8 לפעילות וזמן הפעילות שווה גדולה מזמן האלמנט החדש שהוספנו
//                    //והיא מסתיימת לפני שעת היציאה של כיסוי התור
//                    if ((oPeilut.bBitulBiglalIchurLasidurExists) && (oPeilut.dFullShatYetzia >= dNewPeilutShatYetiza)
//                    && (oPeilut.dFullShatYetzia.AddMinutes(iZmanPeilut) < dNewPeilutShatYetiza.AddMinutes(oParam.iPrepareOtherMechineMaxTime + iPeilutNesiaKisuyTor)))
//                    {
//                        oObjPeilutOvdimDel = new OBJ_PEILUT_OVDIM();
//                        InsertToObjPeilutOvdimForDelete(ref oPeilut, ref oSidur, ref oObjPeilutOvdimDel);
//                        oCollPeilutOvdimDel.Add(oObjPeilutOvdimDel);
//                    }
//                }
//            }         
//        }
//        private OBJ_SIDURIM_OVDIM GetSidurOvdimObject(int iSidurIndex)
//        {
//            //מביא את הסידור לפי מפתח האינדקס
//            OBJ_SIDURIM_OVDIM oObjSidurimOvdim = new OBJ_SIDURIM_OVDIM();
//            clSidur oSidur;
//            oSidur= (clSidur)htEmployeeDetails[iSidurIndex];
//            for (int i = 0; i <= oCollSidurimOvdimUpd.Count - 1; i++)
//            {
//                if ((oCollSidurimOvdimUpd.Value[i].MISPAR_SIDUR == oSidur.iMisparSidur) && (oCollSidurimOvdimUpd.Value[i].SHAT_HATCHALA.ToShortTimeString() == oSidur.sShatHatchala)
//                    && (oCollSidurimOvdimUpd.Value[i].TAARICH == DateTime.Parse(oSidur.sSidurDate)))
//                {
//                    oObjSidurimOvdim =  oCollSidurimOvdimUpd.Value[i];
//                }
//            }

//            return oObjSidurimOvdim;
//        }

//        private OBJ_PEILUT_OVDIM GetPeilutOvdimObject(int iSidurIndex, int iPeilutIndex)
//        {
//            OBJ_PEILUT_OVDIM oObjPeilutOvdim = new OBJ_PEILUT_OVDIM();
//            clPeilut oPeilut;
//            clSidur oSidur;

//            //נמצא את הפעילות באובייקט פעילויות לעדכון
//            oSidur = (clSidur)htEmployeeDetails[iSidurIndex];
//            oPeilut = (clPeilut)oSidur.htPeilut[iPeilutIndex];
//            for (int i = 0; i <= oCollPeilutOvdimUpd.Count - 1; i++)
//            {
//                if ((oCollPeilutOvdimUpd.Value[i].MISPAR_SIDUR == oSidur.iMisparSidur) && (oCollPeilutOvdimUpd.Value[i].SHAT_HATCHALA_SIDUR == oSidur.dFullShatHatchala)
//                    && (oCollPeilutOvdimUpd.Value[i].SHAT_YETZIA == oPeilut.dFullShatYetzia) && (oCollPeilutOvdimUpd.Value[i].MISPAR_KNISA==oPeilut.iMisparKnisa))
//                {                   
//                    oObjPeilutOvdim = oCollPeilutOvdimUpd.Value[i];                    
//                }               
//            }
//            return oObjPeilutOvdim;   
//        }
//        private void SetZmanHashlama(int iSidurKonnutGrira,int[] arrSidurActualKonnutGrira)
//        {
//            int iZmanHashlama = 0;
//            int iMerchav = 0;
//            float fSidurTime = 0;            
//            clSidur oSidur, oSidurKonenut;

//            //אם זוהי גרירה ראשונה בפועל (זיהוי הסידור לפי הלוגיקה בסעיף סימון כוננות גרירה לא לתשלום) בתוך זמן סידור כוננות גרירה (זיהוי הסידור לפי הלוגיקה בסעיף סימון כוננות גרירה לא לתשלום) ואם סידור הכוננות הוא "מרחב צפון" (קוד הסניף שהוא 2 הספרות הראשונות של מספר הסידור קטן מ-25) וזמן הסידור (גמר - התחלה) פחות מהזמן המוגדר בפרמטר 164 (זמן גרירה מינימלי באזור צפון) אזי יש לסמן "2" בשדה "קוד השלמה". 

//            oSidurKonenut = (clSidur)htEmployeeDetails[iSidurKonnutGrira];
//            iMerchav = int.Parse((oSidurKonenut.iMisparSidur).ToString().Substring(0, 2));
//            for (int i = 0; i < arrSidurActualKonnutGrira.Length; i++)
//            {
//                oSidur = (clSidur)htEmployeeDetails[arrSidurActualKonnutGrira[i]];
//                fSidurTime = clDefinitions.GetSidurTimeInMinuts(oSidur);

//                if (i == 0) //גרירה ראשונה
//                {                    
//                    if ((iMerchav < clGeneral.enMerchav.Tzafon.GetHashCode()) && (fSidurTime < oParam.iGriraMinTimeNorth))
//                    {
//                        iZmanHashlama = 2;
//                    }
//                    else
//                    {
//                        //איזור דרום/ירושלים
//                        if (((iMerchav >= clGeneral.enMerchav.Tzafon.GetHashCode()) && (iMerchav < clGeneral.enMerchav.Darom.GetHashCode())) && (fSidurTime < oParam.iGriraMinTimeSouth))
//                        {
//                            iZmanHashlama = 1;
//                        }
//                        //else
//                        //{
//                        //    if ((iMerchav >= clGeneral.enMerchav.Darom.GetHashCode()) && ((fSidurTime < oParam.iGriraMinTimeYerushalim)))
//                        //    {
//                        //        iZmanHashlama = 1;
//                        //    }
//                        //}
//                    }
//                    if (iZmanHashlama > 0)
//                    {
//                        OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
//                        oObjSidurimOvdimUpd = GetSidurOvdimObject(arrSidurActualKonnutGrira[i]);
//                        if (oObjSidurimOvdimUpd != null)
//                        {
//                            oObjSidurimOvdimUpd.HASHLAMA = iZmanHashlama;
//                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                        }
//                    }
//                }
//                else //גרירה שניה ומעלה
//                {
//                    //אם זוהי אינה גרירה ראשונה בפועל בתוך זמן הכוננות
//                    //וגם זמן הסידור (גמר - התחלה) פחות מהזמן המוגדר בפרמטר 181 (זמן גרירה נוספת מינימלי בזמן כוננות גרירה) אזי יש לסמן "1" בשדה "קוד השלמה". 

//                    if (fSidurTime < oParam.iGriraAddMinTime)
//                    {                                               
//                        OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
//                        oObjSidurimOvdimUpd = GetSidurOvdimObject(arrSidurActualKonnutGrira[i]);
//                        if (oObjSidurimOvdimUpd != null)
//                        {
//                            oObjSidurimOvdimUpd.HASHLAMA = 1;
//                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                        }
//                    }
//                }
//            }
//        }

//        private void SetLoLetashlum(int iSidurKonnutGrira)
//        {
//            //נמצא את סידור כוננות הגרירה ונסמן לא לתשלום
//           OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
//           oObjSidurimOvdimUpd = GetSidurOvdimObject(iSidurKonnutGrira);
//           if (oObjSidurimOvdimUpd != null)
//           {
//               oObjSidurimOvdimUpd.LO_LETASHLUM = 1;
//               oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//              // oCollSidurimOvdimUpd.Add(oObjSidurimOvdimUpd);
//           }
//        }
//        private void SetBitulZmanNesiotObject(int iSidurKonnutGrira, int iZmanNesia)
//        {
//            //נמצא את סידור כוננות הגרירה ונסמן לא לתשלום
//            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
//            oObjSidurimOvdimUpd = GetSidurOvdimObject(iSidurKonnutGrira);
//            if (oObjSidurimOvdimUpd != null)
//            {
//                oObjSidurimOvdimUpd.MEZAKE_NESIOT = iZmanNesia;
//                oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                //oCollSidurimOvdimUpd.Add(oObjSidurimOvdimUpd);
//            }
//        }

//        private void SetBitulZmanNesiot(int iSidurKonnutGrira, int[] arrSidurActualKonnutGrira)
//        {
//            clSidur oSidurGrira, oSidurActualGrira;            
//            DateTime dtSidurActualGriraShatHatchala,dtSidurActualGriraShatGmar, dtSidurGriraShatHatchala, dtSidurGriraShatGmar;
//            int iZmanNesia=0;

//            //סידור כוננות גרירה
//            oSidurGrira = (clSidur)htEmployeeDetails[iSidurKonnutGrira];
//            dtSidurGriraShatHatchala = oSidurGrira.dFullShatHatchala;//DateTime.Parse(String.Concat(DateTime.Parse(oSidurGrira.sSidurDate).ToShortDateString(), " ", oSidurGrira.sShatHatchala));
//            dtSidurGriraShatGmar = oSidurGrira.dFullShatGmar;//DateTime.Parse(String.Concat(DateTime.Parse(oSidurGrira.sSidurDate).ToShortDateString(), " ", oSidurGrira.sShatGmar));

//            for (int i=0; i <= arrSidurActualKonnutGrira.Length-1; i++)
//            {
//               //סידור כוננות גרירה בפועל
//               oSidurActualGrira = (clSidur)htEmployeeDetails[arrSidurActualKonnutGrira[i]];
//               dtSidurActualGriraShatHatchala = oSidurActualGrira.dFullShatHatchala;//DateTime.Parse(String.Concat(DateTime.Parse(oSidurActualGrira.sSidurDate).ToShortDateString(), " ", oSidurActualGrira.sShatHatchala));
//               dtSidurActualGriraShatGmar = oSidurActualGrira.dFullShatGmar; //DateTime.Parse(String.Concat(DateTime.Parse(oSidurActualGrira.sSidurDate).ToShortDateString(), " ", oSidurActualGrira.sShatGmar));
//               //אם כוננות גרירה בפועל מוכלת לגמרי בכוננות גרירה
//               if ((dtSidurActualGriraShatHatchala >= dtSidurGriraShatHatchala) && (dtSidurActualGriraShatGmar <= dtSidurGriraShatGmar))
//               {                  
//                   if ((!String.IsNullOrEmpty(oSidurActualGrira.sMikumShaonKnisa)) && (int.Parse(oSidurActualGrira.sMikumShaonKnisa)>0) && (!String.IsNullOrEmpty(oSidurActualGrira.sMikumShaonYetzia)) && (int.Parse(oSidurActualGrira.sMikumShaonYetzia) > 0))
//                   {
//                       iZmanNesia = 3;
//                       //oObjSidurimOvdimUpd.MEZAKE_NESIOT = iZmanNesia;
//                       SetBitulZmanNesiotObject(arrSidurActualKonnutGrira[i], iZmanNesia);
//                       //oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = iZmanNesia;
//                   }                   
//                   else
//                   {
//                       if (!String.IsNullOrEmpty(oSidurActualGrira.sMikumShaonKnisa))
//                       {
//                           if (int.Parse(oSidurActualGrira.sMikumShaonKnisa) > 0)
//                           {
//                               iZmanNesia = 1;
//                               //oObjSidurimOvdimUpd.MEZAKE_NESIOT = iZmanNesia;
//                               SetBitulZmanNesiotObject(arrSidurActualKonnutGrira[i], iZmanNesia);
//                               //oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = iZmanNesia;
//                           }
//                       }
//                       if (!String.IsNullOrEmpty(oSidurActualGrira.sMikumShaonYetzia))
//                       {
//                           if (int.Parse(oSidurActualGrira.sMikumShaonYetzia) > 0)
//                           {
//                               iZmanNesia = 2;
//                               //oObjSidurimOvdimUpd.MEZAKE_NESIOT = iZmanNesia;
//                               SetBitulZmanNesiotObject(arrSidurActualKonnutGrira[i], iZmanNesia);
//                               //oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = iZmanNesia;
//                           }
//                       }
//                   }
                  
//               }
//            }                      
//        }

//        private bool IsKonnutGrira(ref clSidur oSidur, DateTime dCardDate)
//        {
//            DataRow[] drSugSidur;
//            bool bSidurGrira = false;

//            //נבדוק אם סידור הוא מסוג כוננות גרירה
//            if (!oSidur.bSidurMyuhad)
//            //{//סידור מיוחד
//            //    bSidurGrira = (oSidur.sSugAvoda == clGeneral.enSugAvoda.ActualGrira.GetHashCode().ToString());               
//            //}
//            //else
//            {//סידור רגיל
//                drSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidur.iSugSidurRagil, dCardDate,  dtSugSidur);
//                if (drSugSidur.Length > 0)
//                {
//                    bSidurGrira = (drSugSidur[0]["sug_Avoda"].ToString() == clGeneral.enSugAvoda.Grira.GetHashCode().ToString());                    
//                }
//            }
//            return bSidurGrira;
//        }

//        private bool IsActualKonnutGrira(ref clSidur oSidur, int iSidurKonnutGrira)
//        {
//            clSidur oSidurGrira;
//            bool bSidurActualGrira = false;
//            DateTime dtSidur, dtSidurGriraShatHatchala, dtSidurGriraShatGmar;

//            if (oSidur.bSidurMyuhad)
//            {//סידור מיוחד
//                if (oSidur.sSugAvoda == clGeneral.enSugAvoda.ActualGrira.GetHashCode().ToString())
//                {
//                    oSidurGrira = (clSidur)htEmployeeDetails[iSidurKonnutGrira];
//                    dtSidurGriraShatHatchala = oSidurGrira.dFullShatHatchala; //DateTime.Parse(String.Concat(DateTime.Parse(oSidurGrira.sSidurDate).ToShortDateString(), " " , oSidurGrira.sShatHatchala));
//                    dtSidurGriraShatGmar = oSidurGrira.dFullShatGmar;//DateTime.Parse(String.Concat(DateTime.Parse(oSidurGrira.sSidurDate).ToShortDateString(), " ", oSidurGrira.sShatGmar));
//                    dtSidur = oSidurGrira.dFullShatHatchala;//DateTime.Parse(String.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " " , oSidur.sShatHatchala));
//                    //TRUE אם סידור הגרירה בפועל נמצא בטווח סידור כוננות הגרירה, נחזיר 
//                    if ((dtSidur >= dtSidurGriraShatHatchala) && (dtSidur <= dtSidurGriraShatGmar))
//                    {
//                        bSidurActualGrira = true;
//                    }
//                }
//            }

//            return bSidurActualGrira;
//        }

//        private void FixedMisparSidur01(ref clSidur oSidur, int iSidurIndex, ref int iNewMisparMatala, 
//                                        ref int iNewMisparSidur,
//                                        ref bool bUpdateMisparMatala)                                        
//        {
//            //OrderedDictionary dePeilutEntry;
//            //int iKey;
//            //const int SIDUR_NESIA  = 99300;
//            //const int SIDUR_MATALA = 99301;
//            int iMisparSidur=0;
//            int iCount=0;
//            clPeilut oPeilut;
//            try
//            {   //בדיקה מספר 1  - תיקון מספר סידור 
//                //הסדרן בכל סניף יכול להוסיף לנהג "מטלות" אשר מסומנות בספרור 00001 - 00999. זה אינו מספר סידור חוקי ולכן הופכים אותו לחוקי עפ"י קוד הפעילות אשר הסדרן רשם למטלה זו. יהיה צורך לשמור בשדה נפרד את מספר המטלה המקורי
//                bUpdateMisparMatala = false;
//                if (oSidur.iMisparSidur >= 1 && oSidur.iMisparSidur <= 999)
//                {
//                    for (int j = 0; j < ((KdsBatch.clSidur)(htEmployeeDetails[iSidurIndex])).htPeilut.Count; j++)
//                    {
//                        //תחת מטלה יכולה להיות פעילות אחת בלבד.
//                        if (iCount>0) break;
//                        oPeilut = (clPeilut)oSidur.htPeilut[j];
                                                                 
//                        iCount++;
//                        switch (oPeilut.iMakatType)
//                        {
//                           case 1://enMakatType.mKavShirut:  
//                                //פעילות מסוג נסיעת שירות
//                                // אם הפעילות תחת המטלה  היא נסיעת שירות (לפי רוטינת זיהוי מקט), יש להכניס למספר סידור 99300
//                                iMisparSidur = SIDUR_NESIA;
//                                break;
//                           case 2: //(int)enMakatType.mEmpty.GetHashCode():   
//                                //פעילות מסוג ריקה
//                                //אם הפעילות תחת המטלה היא ריקה (לפי רוטינת זיהוי מקט), יש לסמן את הסידור 99300 
//                                iMisparSidur = SIDUR_NESIA;
//                                break;
//                           case 3:// (int)enMakatType.mNamak.GetHashCode():  
//                                //פעילות מסוג נמ"ק
//                                //אם הפעילות תחת המטלה היא נמ"ק (לפי רוטינת זיהוי מקט), יש לסמן את הסידור 99300 
//                                iMisparSidur = SIDUR_NESIA;
//                                break;
//                           case 5://(int)enMakatType.mElement.GetHashCode():  
//                                if (oPeilut.bMisparSidurMatalotTnuaExists) 
//                                {
//                                   //קיים מאפיין 28
//                                   //נקח את מספר הסידור ממאפיין 28
//                                    iMisparSidur = oPeilut.iMisparSidurMatalotTnua; 
//                                }
//                                else 
//                                {
//                                    //אם לא קיים מאפיין 28
//                                    iMisparSidur = SIDUR_MATALA;  
//                                }
//                                break;
//                        }
                      
//                        if (iMisparSidur > 0)
//                        {                            
//                            ////נכניס סידור חדש
//                            //oObjSidurimOvdimIns = new OBJ_SIDURIM_OVDIM();
//                            //InsertToObjSidurimOvdimForInsert(ref oSidur, ref oObjSidurimOvdimIns);
//                            //oObjSidurimOvdimIns.MISPAR_SIDUR = iMisparSidur;
//                            //oCollSidurimOvdimIns.Add(oObjSidurimOvdimIns);

//                            ////נמחוק את הסידור השגוי
//                            //oObjSidurimOvdimDel = new OBJ_SIDURIM_OVDIM();
//                            //InsertToObjSidurimOvdimForDelete(ref oSidur, ref oObjSidurimOvdimDel);
//                            //oObjSidurimOvdimDel.MISPAR_SIDUR = oSidur.iMisparSidur;                            
//                            //oCollSidurimOvdimDel.Add(oObjSidurimOvdimDel);
//                            //oObjSidurimOvdimUpd.MISPAR_SIDUR = iMisparSidur;
//                            oSidur.bSidurMyuhad = true;
//                            clNewSidurim oNewSidurim = new clNewSidurim();

//                            iNewMisparSidur = iMisparSidur;
//                            iNewMisparMatala = oSidur.iMisparSidur;
//                            bUpdateMisparMatala = true;

//                            oNewSidurim.SidurIndex = iSidurIndex;
//                            oNewSidurim.SidurOld = oSidur.iMisparSidur;
//                            oNewSidurim.SidurNew = iMisparSidur;
//                            htNewSidurim.Add(iSidurIndex, oNewSidurim);
//                        }
//                    }                       
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void FixedSidurHours08(ref clSidur oSidur,int iPrevSidurIndex)
//        {
//            //תיקון שעות בחפיפה בין סידורים
//            //קיימים מצבים בהם קיימת חפיפה בשעות של סידורים המגיעים ממערכת הסדרן.
//            //יש לתקן חפיפה זו ע"י תיקון שעת גמר של הסידור הראשון לשעת ההתחלה של הסידור השני בתנאי ששניהם לא מסומנים לא לתשלום. אם תיווצר בעיה עם פעילויות – זה יצא לשגוי והרישום יטפל בזאת
//            clSidur oSidurPrev;
//            DateTime datePrevShatGmar;
//            DateTime dateCurrShatHatchala;
//            DateTime dateCurrShatGmar;

//            try
//            {
//                //נקרא את נתוני הסידור הקודם
//                oSidurPrev = (clSidur)htEmployeeDetails[iPrevSidurIndex];

//                if ((!oSidurPrev.bSidurMyuhad) && (!oSidur.bSidurMyuhad))
//                {
//                    OBJ_SIDURIM_OVDIM oPrevObjSidurimOvdimUpd;
//                    oPrevObjSidurimOvdimUpd = GetSidurOvdimObject(iPrevSidurIndex);

//                    if ((oPrevObjSidurimOvdimUpd.SHAT_GMAR != DateTime.MinValue) && (oObjSidurimOvdimUpd.SHAT_HATCHALA.Year > clGeneral.cYearNull))
//                    {
//                        if ((oPrevObjSidurimOvdimUpd.LO_LETASHLUM == 0) && (oObjSidurimOvdimUpd.LO_LETASHLUM == 0))
//                        {
//                            datePrevShatGmar = oPrevObjSidurimOvdimUpd.SHAT_GMAR;
//                            dateCurrShatHatchala = oObjSidurimOvdimUpd.SHAT_HATCHALA;
//                            dateCurrShatGmar = oObjSidurimOvdimUpd.SHAT_GMAR;
//                            if ((datePrevShatGmar > dateCurrShatHatchala) && (dateCurrShatGmar > datePrevShatGmar))
//                            {//קיימת חפיפה בין הסידורים                                                           
//                                oPrevObjSidurimOvdimUpd.SHAT_GMAR = oSidur.dFullShatHatchala;
//                                oPrevObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                            }
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void SidurNetzer17(ref clSidur oSidur)
//        {
//            //אם סידור הוא סידור נצר (99203) (לפי ערך 11 (נ.צ.ר) במאפיין 52 (סוג עבודה) בטבלת סידורים מיוחדים)  ולעובד קיים מאפיין 64 והסידור אינו מסומן "לא לתשלום" 
//            //- שותלים "חריגה" 3 (התחלה + גמר) ו"מחוץ למיכסה".

//            try
//            {   //אם סידור נצר
//                if ((oSidur.sSugAvoda == clGeneral.enSugAvoda.Netzer.GetHashCode().ToString()) && (oMeafyeneyOved.IsMeafyenExist(64)) && (oSidur.iLoLetashlum==0))
//                {
                    
//                    oObjSidurimOvdimUpd.CHARIGA = 3;
//                    oObjSidurimOvdimUpd.OUT_MICHSA = 1;
//                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                   // oCollSidurimOvdimUpd.Add(oObjSidurimOvdimUpd);
                    
//                   // clDefinitions.UpdateSidurimOvdim(oCollSidurimOvdimUpd);
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void NewSidurHeadrutWithPaymeny15(int iMisparIshi, DateTime dCardDate,int iSugYom, bool bLoLetashlum)                                                                                                                                                      
//        {
//            //לעובדים להם יש מאפיין אישי 63 (משפחה שכולה) , אם סוג יום = 17 (יום הזכרון) ואין להם סידור עבודה אחר באות יום (שאינו מסומן לא לתשלום) , יש לפתוח להם סידור 99801 (העדרות בתשלום יום עבודה) עם שעות מ-0400 – 2800 (כדי שדיווח אחר יצא לשגוי בחפיפה אם ידווח).            
//            try
//            {
//                if ((oMeafyeneyOved.IsMeafyenExist(63)) && (iSugYom == clGeneral.enSugYom.ErevYomHatsmaut.GetHashCode()) && (!bLoLetashlum))
//                {
//                    oObjSidurimOvdimIns = new OBJ_SIDURIM_OVDIM();
//                    //InsertToObjSidurimOvdimForInsert(ref oSidur, ref oObjSidurimOvdimIns);
//                    oObjSidurimOvdimIns.TAARICH = dCardDate;
//                    oObjSidurimOvdimIns.MISPAR_ISHI = iMisparIshi;
//                    oObjSidurimOvdimIns.MISPAR_SIDUR = SIDUR_HEADRUT_BETASHLUM;
//                    oObjSidurimOvdimIns.SHAT_HATCHALA = oParam.dSidurStartLimitHourParam1;//DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", "04:00"));
//                    oObjSidurimOvdimIns.SHAT_GMAR = oParam.dSidurLimitShatGmar;//DateTime.Parse(string.Concat(DateTime.Parse(oSidur.sSidurDate).ToShortDateString(), " ", "28:00"));

//                    oCollSidurimOvdimIns.Add(oObjSidurimOvdimIns);
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void DeleteElementRechev07(ref clSidur oSidur, ref clPeilut oPeilut)                                                                                       
//        {
//            //ביטול אלמנט השאלת רכב
//            //תמיד לבטל  אלמנט "השאלת רכב" (73800000). 
//            //אלא אם זוהי פעילות יחידה בסידור – לא לבטל אלא לסמן את הסידור "לא לתשלום" .

//            try
//            {
//                if (oPeilut.lMakatNesia.ToString().Length>=3)
//                {
//                    if (oPeilut.lMakatNesia.ToString().Substring(0, 3) == "738")
//                    {
//                        //אם זוהי פעילות יחידה בסידור – לא לבטל אלא לסמן את הסידור "לא לתשלום
//                        if (oSidur.htPeilut.Count == 1)
//                        {
//                            //oObjSidurimOvdimUpd = new OBJ_SIDURIM_OVDIM();
//                            //InsertToObjSidurimOvdimForUpdate(ref oSidur, ref oObjSidurimOvdimUpd);
//                            oObjSidurimOvdimUpd.LO_LETASHLUM = 1;
//                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                           // oCollSidurimOvdimUpd.Add(oObjSidurimOvdimUpd);
//                        }
//                        else
//                        {//לבטל
//                            oObjPeilutOvdimDel = new OBJ_PEILUT_OVDIM();
//                            InsertToObjPeilutOvdimForDelete(ref oPeilut, ref oSidur, ref oObjPeilutOvdimDel);
//                            oCollPeilutOvdimDel.Add(oObjPeilutOvdimDel);
//                        }
//                    }        
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void FixedMisparSidurToNihulTnua16(ref clSidur oSidur)                                                                                                     
                                                   
//        {
//            //הפיכת החתמת שעון של מנ"ס-סדרן/פקח לסידור ניהול תנועה
//            //שינוי מספר סידור + שעות + קוד נסיעות

//            //. עובד שקוד עיסוקו 420 (מנ"ס-סדרן) יש להפוך כל החתמת שעון שלו (סידור 99001, מזהים לפי ערך 1 (מינהל) במאפיין 54 (שעון נוכחות) בטבלת סידורים מיוחדים) לסידור 99224.
//            //אם יש לו מאפיין זכאות לזמן נסיעה (51) , יש לשתול סימון "נסיעות" (1 אם החתים רק כניסה, 2 אם החתים רק יציאה, 3 אם החתים שניהם). זיהוי ההחתמה - קיים ערך בשדה "מיקום שעון" כניסה/יציאה או שאין ערך בשדה "מיקום שעון" כניסה/יציאה אך יש סיבה להחתמה ידנית ואישור  ידנית ואישור החתמה ידנית.

//            //. עובד שקוד עיסוקו 422 (מנ"ס-פקח) יש להפוך כל החתמת שעון שלו (סידור 99001, מזהים לפי ערך 1 (מינהל) במאפיין 54 (שעון נוכחות) בטבלת סידורים מיוחדים) לסידור 99225.
//            //אם יש לו מאפיין זכאות לזמן נסיעה (51) , יש לשתול סימון "נסיעות" (1 אם החתים רק כניסה, 2 אם החתים רק יציאה, 3 אם החתים שניהם). זיהוי ההחתמה - קיים ערך בשדה "מיקום שעון" כניסה/יציאה או שאין ערך בשדה "מיקום שעון" כניסה/יציאה אך יש סיבה להחתמה ידנית ואישור החתמה ידנית.
//            int iNewMisparSidur = 0;
//            int iHachtamatShaon = 0;
//            try
//            {
//                if (oSidur.sShaonNochachut == clGeneral.enShaonNochachut.enMinhak.GetHashCode().ToString())
//                {
//                    if (oOvedYomAvodaDetails.iIsuk == clGeneral.enIsukOved.ManasSadran.GetHashCode())
//                    {
//                        iNewMisparSidur = 99224; 
//                    }
//                    else
//                    {
//                        if (oOvedYomAvodaDetails.iIsuk == clGeneral.enIsukOved.ManasPakch.GetHashCode())
//                        {
//                            iNewMisparSidur = 99225; 
//                        }
//                    }
//                }
//                if (iNewMisparSidur > 0)
//                {
//                    //oObjSidurimOvdimUpd = new OBJ_SIDURIM_OVDIM();
//                    //InsertToObjSidurimOvdimForUpdate(ref oSidur, ref oObjSidurimOvdimUpd);
//                    oObjSidurimOvdimUpd.NEW_MISPAR_SIDUR = iNewMisparSidur;
//                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                    //oObjSidurimOvdimUpd.bUpdate = true;
//                    //oCollSidurimOvdimUpd.Add(oObjSidurimOvdimUpd);                        
//                }
//                if (oMeafyeneyOved.IsMeafyenExist(51))
//                {                   
//                    if ((!String.IsNullOrEmpty(oSidur.sMikumShaonKnisa)) && (!String.IsNullOrEmpty(oSidur.sMikumShaonYetzia)))
//                    {
//                        //אם קיים גם החתמת שעון כניסה וגם יציאה - ערך 3
//                        iHachtamatShaon = clGeneral.enHachtamatShaon.KnisaAndYetzia.GetHashCode();
//                    }
//                    else
//                    {
//                        if (!String.IsNullOrEmpty(oSidur.sMikumShaonKnisa))
//                        {
//                            //קיים רק ערך כניסה - 1
//                            iHachtamatShaon = clGeneral.enHachtamatShaon.Knisa.GetHashCode();    
//                        }
//                        else
//                        {
//                            if (!String.IsNullOrEmpty(oSidur.sMikumShaonYetzia))
//                            {
//                                // קיים רק ערך יציאה - 2
//                                iHachtamatShaon = clGeneral.enHachtamatShaon.Yetzia.GetHashCode();
//                            }
//                        }
//                    }
//                }
//                if (iHachtamatShaon > 0)
//                {                                      
//                 oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = iHachtamatShaon;
//                 oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
//                }                
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void SidurLoLetashlum11(DataRow[] drSugSidur,ref clSidur oSidur, int iSidurIndex) 
                                        
                                      
//        {
//            bool bSign = false;           

//            //בדיקה ברמת סידור
//            //סימון סידור עבודה "לא לתשלום" אם עונה על תנאים מגבילים
//            try
//            {
//                //תנאים לסימון לא לתשלום:
//                //1. סידור ועד עובדים ביום שבתון - 
//                //סידור ועד עובדים הוא סידור מיוחד עם ערך 10 (ועד עובדים) במאפיין 52 (סוג עבודה) בטבלת סידורים  מיוחדים. יום שבתון מזוהה לפי ערך 7 שחוזר מה- oracle או שחוזר ערך שבתון מטבלת סוגי  ימים מיוחדים.
//                //2. סידור חופש על חשבון שעות נוספות  (99822) ביום שישי - מזהים את הסידור לפי ערך 9 (חופש ע"ח שעות נוספות) במאפיין 53 (סוג היעדרות) בטבלת סידורים מיוחדים. יום שישי (רק שישי, לא ערב חג) מזוהה לפי ערך 6 שחוזר מה- oracle.
//                //3. סידור המכיל פעילות בודדת של אלמנט המוגדר "לידיעה בלבד"  (לאלמנט ערך 2 (ידיעה בלבד) במאפיין 3 (פעולה / ידיעה בלבד
//                //4. לעובד סידור מיוחד המוגדר תפקיד (ערך 1 (תפקיד) במאפיין 3 (סקטור עבודה) בטבלת סידורים מיוחדים), והסידור בוצע ביום שבתון (יום שבתון מזוהה לפי ערך 7 שחוזר מה- oracle או שחוזר ערך שבתון מטבלת סוגי  ימים מיוחדים), ולעובד אין מאפיין עבודה בשבתון (מאפיין 07 שעת התחלה מותרת בשבתון) ולא סומן חריגה 3 (התחלה וגמר).
//                //5. לעובד סידור מיוחד המוגדר תפקיד (ערך 1 (תפקיד) במאפיין 3 (סקטור עבודה) בטבלת סידורים מיוחדים), והסידור בוצע ביום שישי (רק שישי, לא ערב חג), ולעובד אין מאפיין עבודה בשישי (מאפיין 05, שעת התחלה מותרת בשישי), ולא סומן חריגה 3 (התחלה וגמר).
//                //6. עובד מותאם (יש לו ערך בקוד נתון 8 בטבלת פרטי עובדים) ללא שעות נוספות  (בודקים אם למותאם אסור לעשות שעות נוספות -  ניגשים עם קוד המותאמות של העובד לטבלה CTB_Mutamut ובודקים אם יש ערך בשדה   Isur_Shaot_Nosafot)
//                //אם עובד 5 ימים (מזהים עובד 5 ימים לפי ערך 51/52 במאפיין 56 בטבלת מאפייני עובדים) והסידור הוא ביום שישי (רק שישי, לא ערב חג)  או שבתון – מסמנים.
//                //אם עובד 6 ימים (מזהים עובד 6 ימים לפי ערך 61/62 במאפיין 56 בטבלת מאפייני עובדים)
//                //והסידור הוא בשבתון – מסמנים.
//                //7. סידור שיש לו מאפיין 79 (לא לתשלום אוטומטי) (ערך 1). 
//                //רלוונטי גם עבור רגילים וגם עבור מיוחדים.
//                //שים לב – לאחר סימון "לא לתשלום" יש טיפול גם בשעות התחלה וגמר לתשלום.
//                //מלבד זאת, קיים עדכון "לא לתשלום" כחלק מנושא גרירות".
//                //וגם כחלק מקביעת "שעות לתשלום".
                
//                //תנאי 1                 
//                bSign = Condition1Saif11(ref oSidur);
//                if (!bSign)
//                {
//                    //תנאי 2
//                    bSign = Condition2Saif11(ref oSidur);
//                    if (!bSign)
//                    {
//                        //תנאי 3
//                        bSign = Condition3Saif11(ref oSidur, iSidurIndex);
//                        if (!bSign)
//                        {
//                            //תנאי 4
//                            bSign = Condition4Saif11(ref oSidur);
//                            if (!bSign)
//                            {
//                                //5 תנאי
//                                bSign = Condition5Saif11(ref oSidur);
//                                if (!bSign)
//                                {
//                                    //תנאי 6
//                                    bSign = Condition6Saif11(ref oSidur);
//                                    if (!bSign)
//                                    {
//                                        //תנאי 7
//                                        bSign = Condition7Saif11(drSugSidur,ref oSidur);
//                                    }
//                                }
//                            }
//                        }
//                    }
//                }

//                if (bSign)
//                {
//                    //oObjSidurimOvdimUpd = new OBJ_SIDURIM_OVDIM();
//                    //InsertToObjSidurimOvdimForUpdate(ref oSidur, ref oObjSidurimOvdimUpd);
//                    oObjSidurimOvdimUpd.LO_LETASHLUM = 1;
//                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                    //oObjSidurimOvdimUpd.bUpdate = true;
//                    //oCollSidurimOvdimUpd.Add(oObjSidurimOvdimUpd);           
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private bool Condition1Saif11(ref clSidur oSidur)
//        {
//            //תנאי ראשון לסעיף 11
//            return ((oSidur.bSidurMyuhad) && (oSidur.sSugAvoda == clGeneral.enSugAvoda.VaadOvdim.GetHashCode().ToString()) && ((oSidur.sShabaton == "1") || (oSidur.sSidurDay == clGeneral.enDay.Shabat.GetHashCode().ToString())));     
//        }

//        private bool Condition2Saif11(ref clSidur oSidur)
//        {
//            //תנאי שני לסעיף 11
//            return ((oSidur.bSidurMyuhad) && (oSidur.sHeadrutTypeKod == clGeneral.enMeafyenSidur53.enHolidayForHours.GetHashCode().ToString()) && ((oSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString())));            
//        }

//        private bool Condition3Saif11(ref clSidur oSidur, int iSidurIndex)
//        {
//            //תנאי שלישי לסעיף 11
//            int iNumPeilutForYedia = 0;
//            //int iKey;
//            clPeilut oPeilut;
                        
//            //נעבור על כל הפעילויות של הסידור
//            //foreach (DictionaryEntry dePeilutEntry in oSidur.htPeilut)
//            //{
//            for (int j = 0; j < ((KdsBatch.clSidur)(htEmployeeDetails[iSidurIndex])).htPeilut.Count; j++)
//            {
//                //iKey = int.Parse(dePeilutEntry.Key.ToString());
//                oPeilut = (clPeilut)oSidur.htPeilut[j];     
//                //iKey = int.Parse(dePeilutEntry.Key.ToString());
//                //oPeilut = (clPeilut)oSidur.htPeilut[iKey];
//                if ((oPeilut.iMakatType == enMakatType.mElement.GetHashCode()) && (oPeilut.iElementLeYedia == 2))
//                {
//                    iNumPeilutForYedia++;
//                }
//            }
//            return (iNumPeilutForYedia == 1);            
//        }

//        private bool Condition4Saif11(ref clSidur oSidur)
//        {
//            //תנאי רביעי לסעיף 11
//            bool bSign=false;

//            if ((oSidur.bSidurMyuhad) && (oSidur.sSectorAvoda == clGeneral.enSectorAvoda.Tafkid.GetHashCode().ToString()) && ((oSidur.sShabaton == "1") || (oSidur.sSidurDay == clGeneral.enDay.Shabat.GetHashCode().ToString())))
//            {
//                bSign = ((oSidur.sChariga != "3") && (!oMeafyeneyOved.Meafyen7Exists));
              
//            }
//            return bSign;
//        }

//        private bool Condition5Saif11(ref clSidur oSidur)
//        {
//            //תנאי חמישי לסעיף 11
//            bool bSign = false;

//            if ((oSidur.bSidurMyuhad) && (oSidur.sSectorAvoda == clGeneral.enSectorAvoda.Tafkid.GetHashCode().ToString()) && ((oSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString())))
//            {
//                bSign = ((oSidur.sChariga != "3") && (!oMeafyeneyOved.IsMeafyenExist(5)));                
//            }

//            return bSign;
//        }

//        private bool Condition6Saif11(ref clSidur oSidur)
//        {
//            //תנאי שישי לסעיף 11
//            bool bSign = false;
//            bool bIsurShaotNosafot;

//            if (!String.IsNullOrEmpty(oOvedYomAvodaDetails.sMutamut))
//            {                
//                GetMutamut(dtMutamut, int.Parse(oOvedYomAvodaDetails.sMutamut), out bIsurShaotNosafot);
//                if (bIsurShaotNosafot)
//                {
//                    bSign = true;
//                }
//                else
//                {//עובד 6 ימים 
//                    if ((oMeafyeneyOved.IsMeafyenExist(56)) && ((oMeafyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved6DaysInWeek1.GetHashCode()) || (oMeafyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved6DaysInWeek2.GetHashCode())))
//                    {
//                        if ((oSidur.sShabaton == "1") || (oSidur.sSidurDay == clGeneral.enDay.Shabat.GetHashCode().ToString()))
//                        {
//                            bSign = true;
//                        }
//                    }
//                    //עובד 5 ימים 
//                    if ((oMeafyeneyOved.IsMeafyenExist(56)) && ((oMeafyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek1.GetHashCode()) || (oMeafyeneyOved.GetMeafyen(56).IntValue == clGeneral.enMeafyenOved56.enOved5DaysInWeek2.GetHashCode())))
//                    {
//                        if ((oSidur.sShabaton == "1") || (oSidur.sSidurDay == clGeneral.enDay.Shabat.GetHashCode().ToString()) || (oSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()))
//                        {
//                            bSign = true;
//                        }
//                    }
//                }
//            }
//            return bSign;
//        }

//        private bool Condition7Saif11(DataRow[] drSugSidur, ref clSidur oSidur)
//        {
//            bool bLoLetashlumAutomati;

//            //תנאי שביעי לסעיף 11
//            if (oSidur.bSidurMyuhad)
//            {
//                bLoLetashlumAutomati = oSidur.bLoLetashlumAutomatiExists;
//            }
//            else
//            {
//                if (drSugSidur.Length > 0)
//                {
//                    //TODO: מחכה לתשובה ממירי
//                    bLoLetashlumAutomati = (drSugSidur[0]["lo_letashlum_automati"].ToString() == clGeneral.enMeafyen79.LoLetashlumAutomat.GetHashCode().ToString());
//                }
//                else
//                {
//                    bLoLetashlumAutomati = false;
//                }
//            }
//            return bLoLetashlumAutomati;               
//        }
//        private void ChangeElement06(ref clPeilut oPeilut,                                                                     
//                                     ref clSidur oSidur, int iKey)
//        {
//            //int iLocalKey=1;
//            //clPeilut oLocalPeilut;
//            long lElementNumber = 0;

//            //בדיקה ברמת פעילות
//            try
//            {
//                //איפוס אלמנט הכנת מכונה שניה אם לא הוחלף רכב
//                //שינוי זמן אלמנט או החלפת קוד האלמנט
//                //אם מופיע בסידור המקורי אלמנט הכנת מכונה שניה ( xxxxx712) ואם לא הוחלף רכב (כלומר - מספר הרכב באלמנט זהה למספר הרכב בפעילות הקודמת מסוג נסיעה או אלמנט הדורש מספר רכב (אלמנט דורש מספר רכב אם יש לו מאפיין 11 (חובה מספר רכב)) בטבלת מאפייני אלמנטים ) אזי יש להחליף את זמן האלמנט (תוים 4-6 במק"ט) ל-000.
//                //אם הוחלף רכב - אזי יש להפוך את קוד האלמנט מ-712 (הכנת מכונה שניה) ל-711 (הכנת מכונה שניה (רשום)).
//                if (oPeilut.lMakatNesia.ToString().Length >= 6)
//                {
//                    if (oPeilut.lMakatNesia.ToString().Substring(0, 3).Equals("712"))
//                    {
//                        //InsertToObjPeilutOvdimForUpdate(ref oPeilut, ref oSidur, ref oObjPeilutOvdimUpd);
//                        lElementNumber = long.Parse(string.Concat("712", "000", oPeilut.lMakatNesia.ToString().Substring(6, 2)));
//                        oObjPeilutOvdimUpd.MAKAT_NESIA = lElementNumber;
//                        oObjPeilutOvdimUpd.UPDATE_OBJECT = 1;

//                        ////נעבור על כל הפעילויות של הסידור
//                        //for (int j = iKey - 1; j >= 0; j--)
//                        //{
//                        //    iLocalKey = int.Parse(dePeilutEntry.Key.ToString());
//                        //    //if (j == iKey)
//                        //    //{
//                        //    //    continue;
//                        //    //}
//                        //    oLocalPeilut = (clPeilut)oSidur.htPeilut[j];
//                        //    if (oLocalPeilut.IsMustBusNumber())
//                        //    //if (oLocalPeilut.iMakatType == enMakatType.mEmpty.GetHashCode() ||
//                        //    //    oLocalPeilut.iMakatType == enMakatType.mKavShirut.GetHashCode() ||
//                        //    //    oLocalPeilut.iMakatType == enMakatType.mNamak.GetHashCode() ||
//                        //    //    ((oLocalPeilut.iMakatType == enMakatType.mElement.GetHashCode()) && (oLocalPeilut.bBusNumberMustExists)))
//                        //    {// נמצאה פעילות מסוג נסיעה או אלמנט הדורש מספר רכב
//                        //        if (oLocalPeilut.lOtoNo == oPeilut.lOtoNo)
//                        //        {//לא הוחלף רכב
//                        //            lElementNumber = long.Parse(string.Concat("712", "000", oPeilut.lMakatNesia.ToString().Substring(6, 2)));
//                        //        }
//                        //        else
//                        //        {//הוחלף רכב
//                        //            lElementNumber = long.Parse(string.Concat("711", oPeilut.lMakatNesia.ToString().Substring(3)));
//                        //        }
//                        //    }
//                        //    //iLocalKey++;
//                        //}

//                        //if (lElementNumber > 0)
//                        //{//נעדכן את קוד האלמנט
//                        //    //oObjPeilutOvdimUpd = new OBJ_PEILUT_OVDIM();
//                        //    //InsertToObjPeilutOvdimForUpdate(ref oPeilut, ref oSidur, ref oObjPeilutOvdimUpd);
//                        //    oObjPeilutOvdimUpd.MAKAT_NESIA = lElementNumber;
//                        //    oObjPeilutOvdimUpd.UPDATE_OBJECT = 1;
//                        //    //oCollPeilutOvdimUpd.Add(oObjPeilutOvdimUpd);
//                        //}
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void GetMutamut(DataTable dtMutamut, int iKodMutamut, out bool bIsurShaotNosafot)
//        {
//            DataRow[] dr;
//            int iIsurShaotNosafot=0; 
//            //מקבל קוד מותאמות ומחזיר אם יש איסור של שעות נוספות
//            try
//            {
//                dr = dtMutamut.Select(string.Concat("kod_mutamut=", iKodMutamut));
//                if (dr.Length > 0)
//                {
//                    iIsurShaotNosafot = string.IsNullOrEmpty(dr[0]["isur_shaot_nosafot"].ToString()) ? 0 : int.Parse(dr[0]["isur_shaot_nosafot"].ToString());
//                }
//                bIsurShaotNosafot = iIsurShaotNosafot > 0;
//            }              
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void UpdateOutMichsa(ref clSidur oSidur)                                     
//        {
//            //שינוי ברמת סידור
//            //עדכון שדה מחוץ למכסה
//            try
//            {
//                if (oOvedYomAvodaDetails.iKodHevra == clGeneral.enEmployeeType.enEggedTaavora.GetHashCode())
//                {
//                    oObjSidurimOvdimUpd.OUT_MICHSA = 0;
//                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                    //oObjSidurimOvdimUpd.bUpdate = true;
//                }
//                else
//                {
//                    if ((oSidur.bSidurMyuhad) && (oObjSidurimOvdimUpd.LO_LETASHLUM == 0))
//                    {
//                        //oObjSidurimOvdimUpd = new OBJ_SIDURIM_OVDIM();
//                        //InsertToObjSidurimOvdimForUpdate(ref oSidur, ref oObjSidurimOvdimUpd);
//                        if ((oSidur.sZakayMichutzLamichsa == clGeneral.enMeafyenSidur25.enZakaiAutomat.GetHashCode().ToString()) && (oOvedYomAvodaDetails.iKodHevra != clGeneral.enEmployeeType.enEggedTaavora.GetHashCode()))
//                        {   //אם סידור הוא סידור מיוחד ויש לו ערך 3 במאפיין 25 (זכאי אוטומטית "מחוץ למכסה")
//                            //וגם הוא לא מאגד תעבורה.                                               
//                            oObjSidurimOvdimUpd.OUT_MICHSA = 1;
//                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                            //oObjSidurimOvdimUpd.bUpdate = true;
//                        }
//                        else
//                        {
//                            oObjSidurimOvdimUpd.OUT_MICHSA = 0;
//                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                            //oObjSidurimOvdimUpd.bUpdate = true;
//                        }                             
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void UpdateChariga(ref clSidur oSidur)
//        {
//            //עדכון שדה חריגה ברמת סידור
//            //חריגה משעת התחלה/גמר	שינוי נתוני קלט:
//            //1. הסידור מזכה אוטומטית (ערך 3 במאפיין 35) 
//            try
//            {
//                if ((oObjSidurimOvdimUpd.LO_LETASHLUM == 0) && (oSidur.sZakaiLeChariga == clGeneral.enMeafyenSidur35.enCharigaAutomat.GetHashCode().ToString()))
//                {
//                    //oObjSidurimOvdimUpd = new OBJ_SIDURIM_OVDIM();
//                    //InsertToObjSidurimOvdimForUpdate(ref oSidur, ref oObjSidurimOvdimUpd);
//                    oObjSidurimOvdimUpd.CHARIGA = 3;
//                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                    //oObjSidurimOvdimUpd.bUpdate = true;
//                    //oCollSidurimOvdimUpd.Add(oObjSidurimOvdimUpd);  
//                }                 
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void UpdateHashlamaForSidur(ref clSidur oSidur)                                    
//        {
//            //עדכון שדה השלמה ברמת סידור
//            //עדכון שדה השלמה
//            try
//            {
//                float fSidurTime = 0;
//                //0	אין השלמה	שינוי נתוני קלט:
//                //1. ברירת מחדל אלא אם כן מתקיימים תנאים לעדכון ערך אחר.
//                //2. עבור אגד תעבורה, תמיד לא מקבלים השלמה (מזהים אגד תעבורה לפי קוד חברה של העובד - 4895 ).

//                //oObjSidurimOvdimUpd = new OBJ_SIDURIM_OVDIM();
//                //InsertToObjSidurimOvdimForUpdate(ref oSidur, ref oObjSidurimOvdimUpd);
//                oObjSidurimOvdimUpd.HASHLAMA = 0;
//                oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                //oObjSidurimOvdimUpd.bUpdate = true;
//                if (oOvedYomAvodaDetails.iKodHevra != clGeneral.enEmployeeType.enEggedTaavora.GetHashCode())
//                {                 
//                    //התנאים לקבלת ערך 1 - השלמה לשעה                          
//                    if (!oSidur.bSidurMyuhad)
//                    {//סידור רגיל
//                        //הסידור קצר במפת התנועה (פרמטר 60) - רק לרגילים, להשוות זמן תכנון (גמר - התחלה אל מול פרמטר 60). 
//                        //זמן הסידור
//                        fSidurTime = clDefinitions.GetSidurTimeInMinuts(oSidur);
//                        if (fSidurTime < oParam.iShortSidurInTnuaMap)
//                        {
//                            oObjSidurimOvdimUpd.HASHLAMA = 1;
//                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                            //oObjSidurimOvdimUpd.bUpdate = true;
//                        }
//                    }
//                }               
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void UpdateTachograph(bool bSidurNahagut)        
//        {
//            //עדכון ברמת יום עבודה
//            try
//            {
//                //oObjYameyAvodaUpd = new OBJ_YAMEY_AVODA_OVDIM();
//                //InsertToYameyAvodaForUpdate(ref oObjYameyAvodaUpd, ref oOvedYomAvodaDetails);
//                if (bSidurNahagut)
//                {
//                    //למ.א+תאריך, אם מזהים סידור נהגות אחד לפחות (זיהוי סידור נהגות לפי ערך 5 (נהגות) במאפיין 3 (סקטור עבודה) בטבלאות סידורים מיוחדים/מאפייני סוג סידור).
//                    //יש טכוגרף
//                    oObjYameyAvodaUpd.TACHOGRAF = "2";
//                    oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
//                }
//                else
//                {
//                    //. למ.א+תאריך, אם לא מזהים סידור נהגות אחד לפחות (זיהוי סידור נהגות לפי ערך 5 (נהגות) במאפיין 3 (סקטור עבודה) בטבלאות סידורים מיוחדים/מאפייני סוג סידור).
//                    //אין טכוגרף
//                    oObjYameyAvodaUpd.TACHOGRAF = "0";
//                    oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
//                }
//                //oCollYameyAvodaUpd.Add(oObjYameyAvodaUpd);
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private bool IsSidurNahagut(DataRow[] drSugSidur,clSidur oSidur)
//        {
//            bool bSidurNahagut=false;

//            //הפונקציה תחזיר TRUE אם הסידור הוא סידור נהגות

//            try
//            {
//                if (oSidur.bSidurMyuhad)
//                {//סידור מיוחד
//                    bSidurNahagut = (oSidur.sSectorAvoda == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString());                    
//                }
//                else
//                {//סידור רגיל
//                    if (drSugSidur.Length > 0)
//                    {
//                        bSidurNahagut= (drSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString());                        
//                    }
//                }

//                return bSidurNahagut;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private bool IsSidurZakaiLeNesiot(DataRow[] drSugSidur, clSidur oSidur)
//        {
//            bool bSidurZakaiLenesiot = false;

//            //הפונקציה תחזיר TRUE אם הסידור זכאי לזמן נסיעות
//            //סידור מזכה בזמן נסיעות אם יש לו ערך 1 (זכאי) במאפיין 14 (זכאות לזמן נסיעה) בטבלת סידורים מיוחדים/מאפייני סוג סידור
//            if (oSidur.bSidurMyuhad)
//            {//סידור מיוחד
//                bSidurZakaiLenesiot = ((oSidur.bZakayLezamanNesiaExists) && (oSidur.sZakayLezamanNesia == clGeneral.enMeafyen14.enZakai.GetHashCode().ToString()));                
//            }
//            else
//            {//סידור רגיל
//                if (drSugSidur.Length > 0)
//                {
//                    bSidurZakaiLenesiot = (drSugSidur[0]["Zakay_Leaman_Nesia"].ToString() == clGeneral.enMeafyen14.enZakai.GetHashCode().ToString());
//                }
//            }
//            return bSidurZakaiLenesiot;
//        }

//        private bool IsSidurHalbasha(clSidur oSidur)
//        {
//            bool bSidurZakaiLHalbasha=false;

//            //TRUE הפונקציה תחזיר אם הסידור זכאי להלבשה 
//            //ערך 1 במאפיין 15 זכאי לזמן הלבשה במאפייני סידורים מיוחדים, לא רלוונטי לסידורים רגילים
//            if (oSidur.bSidurMyuhad)
//            {//סידור מיוחד
//                bSidurZakaiLHalbasha = ((oSidur.bHalbashKodExists) && (oSidur.sHalbashKod == clGeneral.enMeafyen15.enZakai.GetHashCode().ToString()));
//            }
//            return bSidurZakaiLHalbasha;
//        }
//        private bool IsNotSidurHalbasha(clSidur oSidur)
//        {
//            bool bSidurLoZakaiLHalbasha = false;

//            //TRUE הפונקציה תחזיר אם הסידור לא זכאי להלבשה 
//            //ערך 2 במאפיין 15 זכאי לזמן הלבשה במאפייני סידורים מיוחדים, לא רלוונטי לסידורים רגילים
//            if (oSidur.bSidurMyuhad)
//            {//סידור מיוחד
//                bSidurLoZakaiLHalbasha = ((oSidur.bHalbashKodExists) && (oSidur.sHalbashKod == clGeneral.enMeafyen15.enLoZakai.GetHashCode().ToString()));
//            }
//            return bSidurLoZakaiLHalbasha;
//        }
//        private void UpdateHashlamaForYomAvoda(bool bSidurHeadrut, float fTotalSidrimTime)
//        {
//            //עדכון שדה השלמה ברמת יום עבודה
//            float fTimeToCompare=0;
//            try
//            {
//                //oObjYameyAvodaUpd = new OBJ_YAMEY_AVODA_OVDIM();
//                //InsertToYameyAvodaForUpdate(ref oObjYameyAvodaUpd, ref oOvedYomAvodaDetails);
//                //ברירת מחדל
//                oObjYameyAvodaUpd.HASHLAMA_LEYOM = 0;
//                oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
//                //.  יום עבודה + לפחות סידור אחד הוא מחלה/מילואים/תאונה (רק סידורים מיוחדים עם ערך 2, 3, 4 בהתאמה במאפיין של היעדרות) 
//                if (bSidurHeadrut)
//                {
//                    oObjYameyAvodaUpd.HASHLAMA_LEYOM = 1;
//                    oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
//                }
//                else
//                {
//                 //עבור מותאם בנהגות (מזהים לפי ערך 2 או 3 בקוד נתון 8 (ערך מותאמות)  בטבלת פרטי עובד) שהוא מותאם לזמן קצר (מזהים מותאם לזמן קצר לפי קיום ערך בפרמטר 20 (זמן מותאמות) בטבלת פרטי עובדים) שעבד ביום (לוקחים את כל זמני הסידורים,  מחשבים גמר פחות התחלה וסוכמים) פחות מזמן המותאמות שלו (הערך בפרמטר 20) וההפרש בין זמן העבודה לזמן המותאמות קטן מהערך בפרמטר 153 (מינימום השלמה חריגה למותאם בנהגות).
//                    if ((oOvedYomAvodaDetails.sMutamut == clGeneral.enMutaam.enMutaam2.GetHashCode().ToString()) || (oOvedYomAvodaDetails.sMutamut == clGeneral.enMutaam.enMutaam3.GetHashCode().ToString()))
//                    {
//                        //נקח את זמן המותאמות מפרמטר 20
//                        if (fTotalSidrimTime < oOvedYomAvodaDetails.iZmanMutamut)
//                        {
//                            fTimeToCompare = oOvedYomAvodaDetails.iZmanMutamut - fTotalSidrimTime;
//                            //מינימום השלמה חריגה למותאם בנהגות - נשווה את ההפרש לפרמטר 153
//                            if (fTimeToCompare < oParam.iMinHashlamaCharigaForMutamutDriver) 
//                            {
//                                //נשלים שעה
//                                oObjYameyAvodaUpd.HASHLAMA_LEYOM = 1;
//                                oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
//                            }
//                        }                       
//                    }
//                }                           
//            }
//            catch(Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private bool IsSidurHeadrut(clSidur oSidur)
//        {
//            bool bSidurHeadrut = false;

//            //הפונקציה תחזיר  אם הסידור הוא סידור העדרות מסוג מחלה/מילואים/תאונה  TRUE

//            try
//            {
//                if (oSidur.bSidurMyuhad)
//                {//סידור מיוחד
//                    bSidurHeadrut = ((oSidur.sHeadrutTypeKod == clGeneral.enMeafyenSidur53.enMachala.GetHashCode().ToString())
//                         || (oSidur.sHeadrutTypeKod == clGeneral.enMeafyenSidur53.enMilueim.GetHashCode().ToString())
//                         || (oSidur.sHeadrutTypeKod== clGeneral.enMeafyenSidur53.enTeuna.GetHashCode().ToString()));
//                }


//                return bSidurHeadrut;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private bool IsHasShaonIshur(ref clSidur oSidur)
//        {
//            //נבדוק אם לסידור יש אישור 1 או 3
//            bool bIshur=true;
//            for (int i = 0; i < arrEmployeeApproval.Length; i++)
//            {
//                if (( 
//                    (arrEmployeeApproval[i].WorkCard.SidurNumber == oSidur.iMisparSidur)
//                    && (arrEmployeeApproval[i].WorkCard.SidurStart == oSidur.dFullShatHatchala))                    
//                && (((arrEmployeeApproval[i].Approval.Code == ApprovalCode.Code1.GetHashCode()) ||
//                    (arrEmployeeApproval[i].Approval.Code == ApprovalCode.Code3.GetHashCode()))))
//                {
//                    if (arrEmployeeApproval[i].State == ApprovalRequestState.Approved)
//                    {
//                        bIshur = true;
//                    }
//                    else
//                    {
//                        bIshur = false;
//                    }
//                    break;
//                }
//            }
//            return bIshur;
//        }
//        private bool IsSidurShaon(clSidur oSidur)
//        {
//            bool bSidurShaonKnisa = false;            
//            // אם יש החתמת שעון תקינה או ידנית עם אישור TRUE הפונקציה תחזיר 
//            //נבדוק אם סידור שעון
//            //מזהים סידור שעונים לפי ערך 1 (שעונים) במאפיין 52 (סוג עבודה) בטבלת סידורים מיוחדים

//            //מזהים סידור שעונים לפי ערך 1 (שעונים) במאפיין 52 (סוג עבודה) בטבלת סידורים מיוחדים) ויש  רק החתמת כניסה בשעון . זיהוי החתמת שעון כניסה, שני מקרים שונים: 
//            //א. החתמת שעון - קיום ערך בשדה שעת התחלה של הסידור הנבדק  וקיום ערך בשדה מיקום שעון כניסה.
//            //ב.  דיווח ידני של שעת כניסה - קיום ערך בשדה שעת התחלה של הסידור הנבדק  וקיום סיבת דיווח ידני שמאפשרת מתן זמן הלבשה (לוקחים את קוד סיבה לדיווח ידני ובודקים בטבלת סיבות לדיווח ידני האם אין לו ערך 1 בשדה Goremet_Lebitul_Zman_Nesiaa)  וקיום אישור לדיווח החתמת שעון (קוד אישור 1 או 3 עם סטטוס אישור ברמה הכי גבוהה). 

//            try
//            {               
//               //סידור מיוחד
//                bSidurShaonKnisa = ((oSidur.bSidurMyuhad) && ((oSidur.sSugAvoda == clGeneral.enSugAvoda.Shaon.GetHashCode().ToString())));              
//                return bSidurShaonKnisa;
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }


//        //private bool IsSidurShaonAndYetizaValid(clSidur oSidur)
//        //{
//        //    bool bSidurShaonYetiza = false;
            

//        //    // אם יש החתמת שעון תקינה או ידנית עם אישור TRUE הפונקציה תחזיר 
//        //    //נבדוק אם סידור שעון
//        //    //מזהים סידור שעונים לפי ערך 1 (שעונים) במאפיין 52 (סוג עבודה) בטבלת סידורים מיוחדים

//        //   //. זיהוי החתמת שעון יציאה, שני מקרים שונים: 
//        //   //א. החתמת שעון - קיום ערך בשדה שעת גמר של הסידור הנבדק  וקיום ערך בשדה מיקום שעון יציאה.
//        //   //ב.  דיווח ידני של שעת יציאה - קיום ערך בשדה שעת גמר של הסידור הנבדק  וקיום סיבת דיווח ידני שמאפשרת מתן זמן הלבשה (לוקחים את קוד סיבה לדיווח ידני ובודקים בטבלת סיבות לדיווח ידני האם אין לו ערך 1 בשדה  Goremet_Lebitul_Zman_Nesiaa)  וקיום אישור לדיווח החתמת שעון (קוד אישור 1 או 3 עם סטטוס אישור ברמה הכי גבוהה). 

//        //    try
//        //    {
//        //        if (oSidur.bSidurMyuhad)
//        //        {//סידור מיוחד
//        //            if ((oSidur.sSugAvoda == clGeneral.enSugAvoda.Shaon.GetHashCode().ToString()))
//        //            {
//        //                bSidurShaonYetiza = IsYetizaValid(ref oSidur, SIBA_LE_DIVUCH_YADANI_NESIAA);
//        //            }
//        //        }
//        //        return bSidurShaonYetiza;
//        //    }
//        //    catch (Exception ex)
//        //    {
//        //        throw ex;
//        //    }
//        //}

//        private bool IsKnisaValid(ref clSidur oSidur, string sBitulTypeField)
//        {
//            bool bKnisaValid = false;
//            if (!String.IsNullOrEmpty(oSidur.sShatHatchala))
//            {  //אם הוחתם שעון וגם יש ערך במיקום כניסה - החתמת שעון ידנית 
//                if (!String.IsNullOrEmpty(oSidur.sMikumShaonKnisa))
//                {
//                    bKnisaValid = true;
//                }
//                else
//                {
//                    //החתמה ידנית, נבדוק שיש דיווח תקף להחתמה ידנית
//                    if (bHasShaonIsurInMaxLevel)
//                    {
//                        bKnisaValid = IsIshurYadaniValid(oSidur.iKodSibaLedivuchYadaniIn, sBitulTypeField);
//                    }
//                    else
//                    {
//                        bKnisaValid = false;
//                    }                    
//                    //drSidurSibotLedivuchYadani = clDefinitions.GetOneSibotLedivuachYadani(oSidur.iKodSibaLedivuchYadaniIn, ref dtSibotLedivuachYadani);
//                    //if (drSidurSibotLedivuchYadani.Length > 0)
//                    //{
//                    //    if (!System.Convert.IsDBNull(drSidurSibotLedivuchYadani[0]["Goremet_Lebitul_Zman_Nesiaa"]))
//                    //    {
//                    //        if (int.Parse(drSidurSibotLedivuchYadani[0]["Goremet_Lebitul_Zman_Nesiaa"].ToString()) != 1)
//                    //        {
//                    //            //נבדוק קיום אישור לדיווח החתמת שעון (קוד אישור 1 או 3 עם סטטוס אישור ברמה הכי גבוהה).
//                    //            //נעבור על מערך האישורים לעובד, שאותחל בהתחלה
//                    //            bKnisaValid = bHasShaonIsurInMaxLevel;
//                    //        }
//                    //    }
//                    //}
//                    //else
//                    //{
//                    //    bKnisaValid = false;
//                    //}
//                }
//            }
//            return bKnisaValid;
//        }

//        private bool IsYetizaValid(ref clSidur oSidur, string sBitulTypeField)
//        {
//            bool bYetizaValid = false;
//            if (!String.IsNullOrEmpty(oSidur.sShatGmar))
//            {  //אם הוחתם שעון וגם יש ערך במיקום יציאה - החתמת שעון ידנית 
//                if (!String.IsNullOrEmpty(oSidur.sMikumShaonYetzia))
//                {
//                    bYetizaValid = true;
//                }
//                else
//                {
//                    //החתמה ידנית, נבדוק שיש דיווח תקף להחתמה ידנית
//                    if (bHasShaonIsurInMaxLevel)
//                    {
//                        bYetizaValid = IsIshurYadaniValid(oSidur.iKodSibaLedivuchYadaniOut, sBitulTypeField);
//                    }
//                    else
//                    {
//                        bYetizaValid = false;
//                    }
                     
//                    //drSidurSibotLedivuchYadani = clDefinitions.GetOneSibotLedivuachYadani(oSidur.iKodSibaLedivuchYadaniOut, ref dtSibotLedivuachYadani);
//                    //if (drSidurSibotLedivuchYadani.Length > 0)
//                    //{
//                    //    if (!System.Convert.IsDBNull(drSidurSibotLedivuchYadani[0]["Goremet_Lebitul_Zman_Nesiaa"]))
//                    //    {
//                    //        if (int.Parse(drSidurSibotLedivuchYadani[0]["Goremet_Lebitul_Zman_Nesiaa"].ToString()) != 1)
//                    //        {
//                    //            //נבדוק קיום אישור לדיווח החתמת שעון (קוד אישור 1 או 3 עם סטטוס אישור ברמה הכי גבוהה).
//                    //            //נעבור על מערך האישורים לעובד, שאותחל בהתחלה
//                    //            bYetizaValid = bHasShaonIsurInMaxLevel;
//                    //        }
//                    //    }
//                    //}
//                    //else
//                    //{
//                    //    bYetizaValid = false;
//                    //}
//                }
//            }
//            return bYetizaValid;
//        }
//        private bool SidurShaonWithNoApproval(ref clSidur oSidur)
//        {
//            DataRow[] drSidurSibotLedivuchYadani;
//            bool bSidurShaonNotValid = false;
//            //נבדוק מצב של החתמת שעון, כניסה/יציאה ידנית, ללא אישור
//            try
//            {
//                if (oSidur.bSidurMyuhad)
//                {//סידור מיוחד
//                    if ((oSidur.sSugAvoda == clGeneral.enSugAvoda.Shaon.GetHashCode().ToString()))
//                    {
//                        if ((!String.IsNullOrEmpty(oSidur.sShatGmar)) && (String.IsNullOrEmpty(oSidur.sMikumShaonYetzia)))
//                        {  //אם הוחתם שעון וגם אין ערך במיקום יציאה - החתמת שעון ידנית                                                 
//                           //אם אין אישור להחתמה הידנית, נחזירTRUE   
//                            drSidurSibotLedivuchYadani = clDefinitions.GetOneSibotLedivuachYadani(oSidur.iKodSibaLedivuchYadaniOut, ref dtSibotLedivuachYadani);
//                            if (drSidurSibotLedivuchYadani.Length > 0)
//                            {
//                                if (!System.Convert.IsDBNull(drSidurSibotLedivuchYadani[0][SIBA_LE_DIVUCH_YADANI_NESIAA]))
//                                {
//                                    bSidurShaonNotValid = int.Parse(drSidurSibotLedivuchYadani[0][SIBA_LE_DIVUCH_YADANI_NESIAA].ToString()) == 1;
//                                }
//                            }                                                       
//                        }

//                        if ((!String.IsNullOrEmpty(oSidur.sShatHatchala)) && (String.IsNullOrEmpty(oSidur.sMikumShaonKnisa)))
//                        {  //אם הוחתם שעון ואין ערך במיקום כניסה - החתמת שעון ידנית                                                 
//                            //אם אין אישור להחתמה הידנית, נחזירTRUE   
//                            drSidurSibotLedivuchYadani = clDefinitions.GetOneSibotLedivuachYadani(oSidur.iKodSibaLedivuchYadaniIn, ref dtSibotLedivuachYadani);
//                            if (drSidurSibotLedivuchYadani.Length > 0)
//                            {
//                                if (!System.Convert.IsDBNull(drSidurSibotLedivuchYadani[0][SIBA_LE_DIVUCH_YADANI_NESIAA]))
//                                {
//                                    bSidurShaonNotValid = int.Parse(drSidurSibotLedivuchYadani[0][SIBA_LE_DIVUCH_YADANI_NESIAA].ToString()) == 1;
//                                }
//                            }
//                        }
//                    }
//                }
//                return bSidurShaonNotValid;
//            }
//            catch(Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private bool IsIshurYadaniValid(int iKodSibaLedivuckYadani, string sBitulTypeField)
//        {
//            bool bHasIshur = false;

//            DataRow[] drSidurSibotLedivuchYadani;
//            drSidurSibotLedivuchYadani = clDefinitions.GetOneSibotLedivuachYadani(iKodSibaLedivuckYadani, ref dtSibotLedivuachYadani);
//            if (drSidurSibotLedivuchYadani.Length > 0)
//            {
//                if (!System.Convert.IsDBNull(drSidurSibotLedivuchYadani[0][sBitulTypeField]))
//                {
//                    if (int.Parse(drSidurSibotLedivuchYadani[0][sBitulTypeField].ToString()) != 1)
//                    {
//                        bHasIshur = true;
//                    }
//                }
//            }
//            return bHasIshur;
//        }
//        private void UpdateBitulZmanNesiot(bool bSidurZakaiLnesiot,
//                                           bool bKnisaNessiaValid,
//                                           bool bYetizaNessiaValid,
//                                           bool bSidurShaon,                                            
//                                           bool bSidurShaonYadaniNotValid,
//                                           bool bSidurNahagut, 
//                                           bool bFirstSidurZakaiLenesiot,
//                                           DateTime dCardDate)                                                                                     
//        {
//            int iZmanNesia = 0;                        

//            //עדכון שדה ביטול זמן נסיעות ברמת יום עבודה
//            try
//            {               
//                //אם אין מאפיין נסיעות (51, 61) - נעדכן ל0- 
//                if ((!oMeafyeneyOved.IsMeafyenExist(51)) && (!oMeafyeneyOved.IsMeafyenExist(61)))
//                {
//                    oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = 0;
//                    oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
//                }
//                else
//                {
//                    //לעובד מאפיין 51/61 (מאפיין זמן נסיעות) והעובד זכאי רק לזמן נסיעות לעבודה (ערך 1 בספרה הראשונה של מאפיין זמן נסיעות
//                    //וגם לפחות אחד הסידורים מזכה בזמן נסיעה (סידור מזכה בזמן נסיעות אם יש לו ערך 1 (זכאי) במאפיין 14 (זכאות לזמן נסיעה) בטבלת סידורים מיוחדים/מאפייני סוג סידור
                  
//                    //לעובד מאפיין 51/61 (מאפיין זמן נסיעות) והעובד זכאי רק לזמן נסיעות לעבודה (ערך 1 בספרה הראשונה של מאפיין זמן נסיעות
//                    //עובד זכאי לנסיעות לעבודה
//                    if (IsOvedZakaiLZmanNesiaLaAvoda())
//                    {
//                        //לפחות אחד הסידורים מזכה בזמן נסיעה (סידור מזכה בזמן נסיעות אם יש לו ערך 1 (זכאי) במאפיין 14 (זכאות לזמן נסיעה) בטבלת סידורים מיוחדים/מאפייני סוג סידור
//                        if ((bSidurZakaiLnesiot) || ((bSidurShaon) && (bKnisaNessiaValid) && (!bSidurNahagut)))
//                        {
//                            oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = ZmanNesiotType.ZakaiKnisa.GetHashCode();
//                            oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
//                            if (bFirstSidurZakaiLenesiot)
//                            {
//                                // אם יש סידורים
//                                //אם הסידור הראשון זכאי לנסיעות, נעדכן את שדה MEZAKE_NESIOT 
//                                //בסידור הראשון ל -1                                   
//                                if (htEmployeeDetails.Count > 0)
//                                {
//                                    OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
//                                    oObjSidurimOvdimUpd = GetSidurOvdimObject(0);
//                                    oObjSidurimOvdimUpd.MEZAKE_NESIOT = ZmanNesiotType.ZakaiKnisa.GetHashCode(); 
//                                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;                                   
//                                }
//                            }
//                            //עבור מאפיין 51: 
//                            //אם שדה נסיעות התעדכן בערך 1, אז יש לעדכן את שדה זמן נסיעה הלוך בטבלת ימי עבודה עובדים בערך הזמן ממאפיין 51
//                            if ((oMeafyeneyOved.IsMeafyenExist(61)))
//                            {
//                                //עבור מאפיין 61:
//                                //אם שדה נסיעות התעדכן בערך 1 ויש ערך בשדה מיקום שעון כניסה בסידור הראשון ביום, יש לעדכן את שדה זמן נסיעה הלוך בערך מטבלה זמן נסיעה משתנה.                                        
//                                iZmanNesia = GetZmanNesiaMeshtana(0, 1, dCardDate);
//                                oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = iZmanNesia;
//                            }
//                            if (oMeafyeneyOved.IsMeafyenExist(51))
//                            {
//                                iZmanNesia = int.Parse(oMeafyeneyOved.GetMeafyen(51).Value.Substring(1));
//                                oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = iZmanNesia;
//                            }                            
//                        }
//                    }

//                    //עובד זכאי לנסיעות מהעבודה
//                    if (IsOvedZakaiLZmanNesiaMeAvoda())
//                    {
//                        if ((bSidurZakaiLnesiot) || ((bSidurShaon) && (bYetizaNessiaValid) && (!bSidurNahagut)))
//                        {
//                            oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = ZmanNesiotType.ZakaiYetiza.GetHashCode();
//                            oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
//                            //אם הסידור הראשון זכאי לנסיעות, נעדכן את שדה MEZAKE_NESIOT 
//                            //בסידור הראשון ל -2
//                            if (bFirstSidurZakaiLenesiot)
//                            {
//                                OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
//                                oObjSidurimOvdimUpd = GetSidurOvdimObject(0);
//                                oObjSidurimOvdimUpd.MEZAKE_NESIOT = ZmanNesiotType.ZakaiYetiza.GetHashCode();
//                                oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;                                                                         
//                            }
                            
//                            if ((oMeafyeneyOved.IsMeafyenExist(61))  && (htEmployeeDetails.Count>0))
//                            {
//                                //נשלוף את הסידור האחרון
//                                iZmanNesia = GetZmanNesiaMeshtana(htEmployeeDetails.Count - 1, 2, dCardDate);                                     
//                                oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = iZmanNesia;                                
//                            }
//                            if (oMeafyeneyOved.IsMeafyenExist(51))
//                            {
//                                iZmanNesia = int.Parse(oMeafyeneyOved.GetMeafyen(51).Value.Substring(1));
//                                oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = iZmanNesia;
//                            }
//                        }
//                    }

//                    //עובד זכאי לנסיעות מהעבודה ולעבודה
//                    if (IsOvedZakaiLZmanNesiaLeMeAvoda())
//                    {
//                        if ((bSidurZakaiLnesiot) || (((bSidurShaon) && (bKnisaNessiaValid) && (bYetizaNessiaValid)) && (!bSidurNahagut)))
//                        {
//                            oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = ZmanNesiotType.ZakaiKnisaYetiza.GetHashCode();
//                            oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
//                            //אם ביום קיים סידור אחד בלבד  ולפי הסידור מגיע גם נסיעות כניסה וגם נסיעות יציאה - יש לעדכן את השדה "נסיעות" ברמת סידור העבודה בקוד - זכאי לנסיעות לכניסה/יציאה לעבודה.
//                            //TB_Sidurim_Ovedim.Mezake_ nesiot =3.
//                            if ((htEmployeeDetails.Count == 1))
//                            {
//                                oObjSidurimOvdimUpd.MEZAKE_NESIOT = ZmanNesiotType.ZakaiKnisaYetiza.GetHashCode();
//                                oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;                                   
//                            }
//                            if ((oMeafyeneyOved.IsMeafyenExist(61)) && (htEmployeeDetails.Count>0))
//                            {
//                                oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = GetZmanNesiaMeshtana(0, 1, dCardDate);
//                                oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = GetZmanNesiaMeshtana(htEmployeeDetails.Count - 1, 2, dCardDate); 
//                            }
//                            if (oMeafyeneyOved.IsMeafyenExist(51))
//                            {
//                                iZmanNesia = int.Parse(oMeafyeneyOved.GetMeafyen(51).Value.Substring(1));
//                                oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH = (int)(Math.Ceiling(iZmanNesia / 2.0));
//                                oObjYameyAvodaUpd.ZMAN_NESIA_HAZOR = oObjYameyAvodaUpd.ZMAN_NESIA_HALOCH;
//                            }
//                        }
//                    }

//                    if (((bSidurShaonYadaniNotValid) || (!bHasShaonIsurInMaxLevel)) && ((bSidurShaon) && ((!bKnisaNessiaValid) || (!bYetizaNessiaValid))))
//                    {
//                        oObjYameyAvodaUpd.BITUL_ZMAN_NESIOT = ZmanNesiotType.LoZakai.GetHashCode(); ;
//                        oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
//                    }               
//                }              
//            }
//            catch(Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private int GetZmanNesiaMeshtana(int iSidurIndex, int iType, DateTime dCardDate)
//        {
//            int iZmanNesia = 0;
//            int iMerkazErua = 0;
//            int iMikumYaad = 0;
//            clUtils oUtils = new clUtils();

//            //נשלוף את הסידור 
//            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
//            oObjSidurimOvdimUpd = GetSidurOvdimObject(iSidurIndex);

//            //עבור מאפיין 61:
//            if (iType == 1) //כניסה
//            {
//                if (oObjSidurimOvdimUpd.MIKUM_SHAON_KNISA > 0)
//                {
//                    iMikumYaad = oObjSidurimOvdimUpd.MIKUM_SHAON_KNISA;
//                }
//            }
//            else //יציאה
//            {
//                if (oObjSidurimOvdimUpd.MIKUM_SHAON_YETZIA > 0)
//                {
//                    iMikumYaad = oObjSidurimOvdimUpd.MIKUM_SHAON_YETZIA;
//                }
//            }
           
//            iMerkazErua = (String.IsNullOrEmpty(oOvedYomAvodaDetails.sMercazErua) ? 0 : int.Parse(oOvedYomAvodaDetails.sMercazErua));
//            if ((iMerkazErua > 0) && (iMikumYaad>0))
//            {
//                iZmanNesia = oUtils.GetZmanNesia(iMerkazErua, iMikumYaad, dCardDate);                    
//            }
           
//            return iZmanNesia;
//        }
//        private void UpdateHalbasha(bool bSidurZakaiLHalbash, bool bSidurLoZakaiLHalbash,
//                                    bool bKnisaValid,
//                                    bool bYetizaValid,
//                                    int iSidurZakaiLehalbashaIndex,                                                                      
//                                    DateTime dCardDate)
//        {
//            int iDakotInTafkid=0;
//            int iMezakeHalbasha = 0;
//            //עדכון שדה הלבשה ברמת יום עבודה
//            try
//            {
//                //oObjYameyAvodaUpd = new OBJ_YAMEY_AVODA_OVDIM();
//                //InsertToYameyAvodaForUpdate(ref oObjYameyAvodaUpd, ref oOvedYomAvodaDetails);
//                if (oOvedYomAvodaDetails.iKodHevra == clGeneral.enEmployeeType.enEggedTaavora.GetHashCode())
//                {
//                    //עובד של אגד תעבורה לא זכאי אף פעם, גם אם הכרטיס שגוי וגם אם לא
//                    oObjYameyAvodaUpd.HALBASHA = ZmanHalbashaType.LoZakai.GetHashCode();
//                    oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
//                }
//                else
//                {
//                    //אם אין לעובד מאפיין הלבשה, נעדכן 0-
//                    if (!oMeafyeneyOved.IsMeafyenExist(44))
//                    {
//                        oObjYameyAvodaUpd.HALBASHA = 0;
//                        oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
//                    }
//                    else
//                    { //קיים מאפיין 44 לעובד, זכאות להלבשה
//                        /*1. לעובד מאפיין הלבשה (44) ולפחות לסידור אחד יש מאפיין זכאי לזמן הלבשה (ערך 1 במאפיין 15 זכאי לזמן הלבשה במאפייני סידורים מיוחדים, לא רלוונטי לסידורים רגילים) ויש  רק החתמת כניסה בשעון (לא מתקיימת במקביל לוגיקה המעדכנת ערך 2/3 בשדה זה).  זיהוי החתמת שעון כניסה, שני מקרים שונים: 
//                           א. החתמת שעון - קיום ערך בשדה שעת התחלה של הסידור הנבדק  וקיום ערך שאינו null בשדה מיקום שעון כניסה.
//                           ב.  דיווח ידני של שעת כניסה - קיום ערך בשדה שעת התחלה של הסידור הנבדק  וקיום סיבת דיווח ידני שמאפשרת מתן זמן הלבשה (לוקחים את קוד סיבה לדיווח ידני ובודקים בטבלת סיבות לדיווח ידני האם אין לו ערך 1 בשדה Goremet_Lebitul_Zman_Halbash)  וקיום אישור לדיווח החתמת שעון (קוד אישור 1 או 3 עם סטטוס אישור ברמה הכי גבוהה). 

//                           עדכון שדה TB_Sidurim_Ovedim.Mezake_Halbasha:
//                           אם זהו הסידור הראשון ביום העבודה עבורו זיהינו כי לעובד מגיע זמן הלבשה – יש לעדכן את השדה "הלבשה" ברמת סידור העבודה בקוד – זכאי להלבשה
//                            לכניסה לעבודה.
//                           TB_Sidurim_Ovedim.Mezake_Halbasha=1*/
//                        if (oOvedYomAvodaDetails.iStatus == enCardStatus.Error.GetHashCode())
//                        {
//                            oObjYameyAvodaUpd.HALBASHA = ZmanHalbashaType.CardError.GetHashCode();
//                            oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
//                        }
//                        else{//כרטיס תקין
//                            if ((bSidurZakaiLHalbash) && (bKnisaValid) && (!bYetizaValid))
//                            {
//                                oObjYameyAvodaUpd.HALBASHA = ZmanHalbashaType.ZakaiKnisa.GetHashCode();
//                                oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
//                                //אם הסידור הראשון זכאי להלבשה, נעדכן את שדה MEZAKE_Halbasha 
//                                //בסידור הראשון ל -1
//                                if (iSidurZakaiLehalbashaIndex == 0)
//                                {
//                                    //OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
//                                    //oObjSidurimOvdimUpd = GetSidurOvdimObject(0);
//                                    //oObjSidurimOvdimUpd.MEZAKE_HALBASHA = ZmanHalbashaType.ZakaiKnisa.GetHashCode();
//                                    //oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                                    iMezakeHalbasha = ZmanHalbashaType.ZakaiKnisa.GetHashCode();
//                                }
//                            }

//                            /*שינוי נתוני קלט:
//                            1. לעובד מאפיין הלבשה (44) ולפחות לסידור אחד יש מאפיין זכאי לזמן הלבשה (ערך 1 במאפיין 15 זכאי לזמן הלבשה במאפייני סידורים מיוחדים, לא רלוונטי לסידורים רגילים) ויש  רק החתמת יציאה בשעון (לא מתקיימת במקביל לוגיקה המעדכנת ערך 1/3 בשדה זה).  זיהוי החתמת שעון יציאה, שני מקרים שונים: 
//                            א. החתמת שעון - קיום ערך בשדה שעת גמר של הסידור הנבדק וקיום ערך שאינו null בשדה מיקום שעון יציאה.
//                            ב.  דיווח ידני של שעת יציאה - קיום ערך בשדה שעת גמר שלה סידור הנבדק  וקיום סיבת דיווח ידני שמאפשרת מתן זמן הלבשה (לוקחים את קוד סיבה לדיווח ידני ובודקים בטבלת סיבות לדיווח ידני האם אין לו ערך 1 בשדה Goremet_Lebitul_Zman_Halbash)  וקיום אישור לדיווח החתמת שעון (קוד אישור 1 או 3 עם סטטוס אישור ברמה הכי גבוהה).

//                            עדכון שדה TB_Sidurim_Ovedim.Mezake_Halbasha:
//                            אם זהו הסידור האחרון ביום העבודה עבורו זיהינו כי לעובד מגיע זמן הלבשה - יש לעדכן את השדה "הלבשה" ברמת סידור העבודה בקוד – זכאי להלבשה ליציאה לעבודה.
//                            TB_Sidurim_Ovedim.Mezake_Halbasha=2*/

//                            if ((bSidurZakaiLHalbash) && (bYetizaValid) && (!bKnisaValid))
//                            {
//                                oObjYameyAvodaUpd.HALBASHA = ZmanHalbashaType.ZakaiYetiza.GetHashCode();
//                                oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
//                                //אם הסידור הראשון זכאי להלבשה, נעדכן את שדה MEZAKE_Halbasha 
//                                //בסידור הראשון ל -2
//                                if ((iSidurZakaiLehalbashaIndex == htEmployeeDetails.Count - 1) && (htEmployeeDetails.Count > 0))
//                                {
//                                    //OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
//                                    //oObjSidurimOvdimUpd = GetSidurOvdimObject(iSidurZakaiLehalbashaIndex);
//                                    //oObjSidurimOvdimUpd.MEZAKE_HALBASHA = ZmanHalbashaType.ZakaiYetiza.GetHashCode();
//                                    //oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                                    iMezakeHalbasha = ZmanHalbashaType.ZakaiYetiza.GetHashCode();
//                                }
//                            }

//                            /*שינוי נתוני קלט:
//                            1. לעובד מאפיין הלבשה (44)+ ) ולפחות לסידור אחד יש מאפיין זכאי לזמן הלבשה (ערך 1 במאפיין 15 זכאי לזמן הלבשה במאפייני סידורים מיוחדים, לא רלוונטי לסידורים רגילים) ויש החתמת כניסה ויציאה בשעון  - מתקיימות במקביל הלוגיקות עבור עדכון ערך 1 ועבור עדכון ערך 2.

//                            עדכון שדה TB_Sidurim_Ovedim.Mezake_Halbasha:
//                            אם ביום קיים סידור אחד בלבד  ולפי הסידור מגיע גם הלבשה כניסה וגם הלבשה יציאה - יש לעדכן את השדה "הלבשה" ברמת סידור העבודה בקוד - זכאי להלבשה לכניסה/יציאה לעבודה.
//                            TB_Sidurim_Ovedim.Mezake_Halbasha=3.*/
//                            if ((bSidurZakaiLHalbash) && (bYetizaValid) && (bKnisaValid))
//                            {
//                                oObjYameyAvodaUpd.HALBASHA = ZmanHalbashaType.ZakaiKnisaYetiza.GetHashCode();
//                                oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
//                                if ((htEmployeeDetails.Count == 1) && (bKnisaValid) && (bYetizaValid))
//                                {
//                                    //OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
//                                    //oObjSidurimOvdimUpd = GetSidurOvdimObject(0);
//                                    //oObjSidurimOvdimUpd.MEZAKE_HALBASHA = ZmanHalbashaType.ZakaiKnisaYetiza.GetHashCode();
//                                    //oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                                    iMezakeHalbasha = ZmanHalbashaType.ZakaiKnisaYetiza.GetHashCode(); 
//                                }
//                            }
//                            if ((bSidurZakaiLHalbash) && ((bKnisaValid) || (bYetizaValid)))
//                            { //עובד אשר ענה על תנאי זכאות (אחד מהערכים 1-3)

//                                // 2. עובד אשר ענה על תנאי זכאות (אחד מהערכים 1-3), אולם עבד ביום  [ (לפי רכיב מחושב 4 (דקות בתפקיד) עבור יום שאינו שבת או לפי רכיב מחושב 37 (דקות שבת בתפקיד) עבור יום שבת/שבתון ] פחות  מהערך בפרמטר 0166 (כרגע 210 דקות) ולא הייתה לו השלמה (השלמה לשעות או השלמה ליום עבודה) אשר השלימה לו את יום העבודה לזמן זה .
//                                //iDakotInTafkid = CalcOvedDakotInYafkid(dCardDate);
//                                //if (iDakotInTafkid < oParam.iMinYomAvodaForHalbasha)
//                                //{
//                                //    oObjYameyAvodaUpd.HALBASHA = 4;
//                                //    oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
//                                //}
//                                if (oOvedYomAvodaDetails.iKodHevra == clGeneral.enEmployeeType.enEggedTaavora.GetHashCode())
//                                {
//                                    //4. עובד מאגד תעבורה 
//                                    oObjYameyAvodaUpd.HALBASHA = ZmanHalbashaType.LoZakai.GetHashCode();
//                                    oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
//                                }
//                                else
//                                {
//                                    //3. עובד אשר ענה על תנאי זכאות (אחד מהערכים 1-3), אולם הוא מותאם ליום קצר (יודעים שעובד הוא מותאם ליום עבודה קצר לפי שני פרמטרים – העובד מותאם (לפי קיום ערך בפרמטר 8 (קוד עובד מותאם) בטבלת פרטי עובדים ולפי קיום ערך בפרמטר 20 (זמן מותאמות) בטבלת פרטי עובדים) וזמן המותאמות שלו (לפי ערך בפרמטר 20 (זמן מותאמות) בטבלת פרטי עובדים קטן מהערך בפרמטר 0167 (כרגע 300).
//                                    if ((oOvedYomAvodaDetails.bMutamutExists) && (oOvedYomAvodaDetails.iZmanMutamut < oParam.iMinDakotLemutamLeHalbasha) && (oOvedYomAvodaDetails.iZmanMutamut > 0))
//                                    {
//                                        oObjYameyAvodaUpd.HALBASHA = ZmanHalbashaType.LoZakai.GetHashCode();
//                                        oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
//                                    }
//                                }
//                            }

//                            if (bSidurLoZakaiLHalbash)
//                            {   //1.
//                                //לעובד מאפיין הלבשה (44) + ) ולפחות לסידור אחד יש מאפיין זכאי לזמן הלבשה (ערך 1 במאפיין 15 זכאי לזמן הלבשה במאפייני סידורים מיוחדים, לא רלוונטי לסידורים רגילים) ולא ענה על תנאים 0-3.
//                                oObjYameyAvodaUpd.HALBASHA = ZmanHalbashaType.LoZakai.GetHashCode();
//                                oObjYameyAvodaUpd.UPDATE_OBJECT = 1;
//                            }
//                        }

//                        //אם מזכה הלבשה קיבל ערך, ולא הגענו למסקנה שהוא לא זכאי, נעדכן את שדה מזכה הלבשה
//                        if ((iMezakeHalbasha > 0) && (oObjYameyAvodaUpd.HALBASHA != ZmanHalbashaType.LoZakai.GetHashCode()))
//                        {
//                            OBJ_SIDURIM_OVDIM oObjSidurimOvdimUpd;
//                            if (iMezakeHalbasha == 2)
//                            {
//                                oObjSidurimOvdimUpd = GetSidurOvdimObject(iSidurZakaiLehalbashaIndex);
//                            }
//                            else
//                            {
//                                oObjSidurimOvdimUpd = GetSidurOvdimObject(0);
//                            }
                                                        
//                            oObjSidurimOvdimUpd.MEZAKE_HALBASHA = iMezakeHalbasha;
//                            oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;                            
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private bool IsOvedZakaiLZmanNesiaLaAvoda()
//        {
//            //לעובד מאפיין 51/61 (מאפיין זמן נסיעות) והעובד זכאי רק לזמן נסיעות לעבודה (ערך 1 בספרה הראשונה של מאפיין זמן נסיעות            
//            return (((((oMeafyeneyOved.IsMeafyenExist(61)) && (oMeafyeneyOved.GetMeafyen(61).Value.Substring(0, 1) == "1"))
//                   ||
//                   ((oMeafyeneyOved.IsMeafyenExist(51)) && (oMeafyeneyOved.GetMeafyen(51).Value.Substring(0, 1) == "1")))));                                 
//        }

//        private bool IsOvedZakaiLZmanNesiaMeAvoda()
//        {
//            //לעובד מאפיין 51/61 (מאפיין זמן נסיעות) והעובד זכאי רק לזמן נסיעות מהעבודה (ערך 2 בספרה הראשונה של מאפיין זמן נסיעות            
//            return (((((oMeafyeneyOved.IsMeafyenExist(61)) && (oMeafyeneyOved.GetMeafyen(61).Value.Substring(0, 1) == "2"))
//                   ||
//                   ((oMeafyeneyOved.IsMeafyenExist(51)) && (oMeafyeneyOved.GetMeafyen(51).Value.Substring(0, 1) == "2")))));
//        }

//        private bool IsOvedZakaiLZmanNesiaLeMeAvoda()
//        {
//            //לעובד מאפיין 51/61 (מאפיין זמן נסיעות) והעובד זכאי רק לזמן נסיעות מהעבודה (ערך 3 בספרה הראשונה של מאפיין זמן נסיעות            
//            return (((((oMeafyeneyOved.IsMeafyenExist(61)) && (oMeafyeneyOved.GetMeafyen(61).Value.Substring(0, 1) == "3"))
//                   ||
//                   ((oMeafyeneyOved.IsMeafyenExist(51)) && (oMeafyeneyOved.GetMeafyen(51).Value.Substring(0, 1) == "3")))));
//        }

//        private int CalcOvedDakotInYafkid(DateTime dCardDate)
//        {
//            DataRow[] dr;
//            int iDakotInTafkid = 0;

//            if (oOvedYomAvodaDetails.sShabaton==clGeneral.enDay.Shabat.GetHashCode().ToString())
//            {
//                dr = dtChishuvYom.Select("kod_rechiv=37");
//            }
//            else
//            {
//                dr = dtChishuvYom.Select("kod_rechiv=4");
//            }
//            if (dr.Length > 0)
//            {
//                iDakotInTafkid = System.Convert.IsDBNull(dr[0]["erech_rechiv"].ToString()) ? 0 : int.Parse(dr[0]["erech_rechiv"].ToString());
//            }
 
//            return iDakotInTafkid;
//        }

//        private void InsertSidurRetzifut14(DateTime dCardDate, int iCurrSidurIndex,DataRow[] drSugSidur,
//                                           ref clSidur oSidur)
//        {
//            clSidur oSidurPrev;
//            OBJ_SIDURIM_OVDIM oObjPrevSidurimOvdimUpd;
//            float fDiffTimeBetweenSidurim = 0;
//            try
//            {
//                //הוספת סידור - שינוי ברמת סידור
//                //בין 2 סידורים מגיעה לנהג רציפות עפ"י אלגוריתם המורכב מיום בשבוע , סוג הסידור , מרווח בין סידורים ועוד.
//                //במקרים בהם יש פער זמנים בין שני סידורי עבודה המזכה את העובד בתוספת זמן עבודה עבור פער זה (ומאחד למעשה את 2 הסידורים), יש לפתוח סידור רציפות. ישנם שני סוגים של סידורי רציפות: רציפות נהגות  ורציפות שאינה נהגות. פתיחת סידור רציפות נהגות, 99500:
//                //מזהים פער זמנים בין  שני סידורי נהגות (מזהים סידור נהגות לפי ערך 5 בטבלת סידורים מיוחדים/מאפייני סוג סידור) הגדול מהערך בפרמטר 0104 (פער הזמן).
//                //פתיחת סידור רציפות שאינה נהגות, 99501:
//                //מזהים בין  שני סידורים שלפחות אחד מהם אינו נהגות  פער זמנים הגדול מהערך בפרמטר 0104 (פער הזמן).
//                //את שני הסידורים פותחים עם השעות הבאות:
//                //א. שעת התחלה סידור רציפות - שעת גמר של הסידור הקודם לסידור הרציפות + דקה).
//                //ב.  שעת גמר סידור רציפות - שעת התחלה של הסידור העוקב לרציפות פחות דקה).


//                oSidurPrev = (clSidur)htEmployeeDetails[iCurrSidurIndex];
//                oObjPrevSidurimOvdimUpd = GetSidurOvdimObject(iCurrSidurIndex);
//                DataRow[] drPrevSugSidur = clDefinitions.GetOneSugSidurMeafyen(oSidurPrev.iSugSidurRagil, dCardDate,  dtSugSidur);
//                bool bSidureyNahagut = false;

//                if (oObjSidurimOvdimUpd.SHAT_HATCHALA > oObjPrevSidurimOvdimUpd.SHAT_GMAR)
//                {
//                    if (oSidur.bSidurMyuhad)
//                    {
//                        //אם שני סידורי נהגות וגם פער הזמנים בין הסידורים גדול מפרמטר 104, נכניס סידור רציפות
//                        if ((oSidur.sSectorAvoda == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString())
//                            && (oSidurPrev.sSectorAvoda == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString()))
//                        {
//                            //נחשב את הפרש הזמן בין הסידורים
//                            fDiffTimeBetweenSidurim = clDefinitions.GetTimeBetweenTwoSidurimInMinuts(oSidurPrev, oSidur);
//                            if (fDiffTimeBetweenSidurim > oParam.iMinTimeBetweenSidurim)
//                            {
//                                oObjSidurimOvdimIns = new OBJ_SIDURIM_OVDIM();
//                                InsertToObjSidurimOvdimForInsert(ref oSidur, ref oObjSidurimOvdimIns);
//                                oObjSidurimOvdimIns.MISPAR_SIDUR = 99500;
//                                oObjSidurimOvdimIns.SHAT_HATCHALA = DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", oSidurPrev.sShatGmar)).AddMinutes(1);
//                                oObjSidurimOvdimIns.SHAT_GMAR = DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", oSidur.sShatHatchala)).AddMinutes(-1);
//                                oCollSidurimOvdimIns.Add(oObjSidurimOvdimIns);
//                            }
//                        }
//                        else
//                        {
//                            //לפחות אחד מהסידורים אינו נהגות
//                            fDiffTimeBetweenSidurim = clDefinitions.GetTimeBetweenTwoSidurimInMinuts(oSidurPrev, oSidur);
//                            if (fDiffTimeBetweenSidurim > oParam.iMinTimeBetweenSidurim)
//                            {
//                                oObjSidurimOvdimIns = new OBJ_SIDURIM_OVDIM();
//                                InsertToObjSidurimOvdimForInsert(ref oSidur, ref oObjSidurimOvdimIns);
//                                oObjSidurimOvdimIns.MISPAR_SIDUR = 99501;
//                                oObjSidurimOvdimIns.SHAT_HATCHALA = DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", oSidurPrev.sShatGmar)).AddMinutes(1);
//                                oObjSidurimOvdimIns.SHAT_GMAR = DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", oSidur.sShatHatchala)).AddMinutes(-1);
//                                oCollSidurimOvdimIns.Add(oObjSidurimOvdimIns);
//                            }
//                        }
//                    }
//                    else
//                    {   //סידור רגיל
//                        //אם שני סידורי נהגות וגם פער הזמנים בין הסידורים גדול מפרמטר 104, נכניס סידור רציפות
//                        if (drSugSidur.Length > 0)
//                        {
//                            if (drSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString())
//                            {
//                                if (drPrevSugSidur.Length > 0)
//                                {
//                                    if (drPrevSugSidur[0]["sector_avoda"].ToString() == clGeneral.enSectorAvoda.Nahagut.GetHashCode().ToString())
//                                    {
//                                        fDiffTimeBetweenSidurim = clDefinitions.GetTimeBetweenTwoSidurimInMinuts(oSidurPrev, oSidur);
//                                        if (fDiffTimeBetweenSidurim > oParam.iMinTimeBetweenSidurim)
//                                        {
//                                            oObjSidurimOvdimIns = new OBJ_SIDURIM_OVDIM();
//                                            InsertToObjSidurimOvdimForInsert(ref oSidur, ref oObjSidurimOvdimIns);
//                                            oObjSidurimOvdimIns.MISPAR_SIDUR = 99500;
//                                            oObjSidurimOvdimIns.SHAT_HATCHALA = DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", oSidurPrev.sShatGmar)).AddMinutes(1);
//                                            oObjSidurimOvdimIns.SHAT_GMAR = DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", oSidur.sShatHatchala)).AddMinutes(-1);
//                                            oCollSidurimOvdimIns.Add(oObjSidurimOvdimIns);
//                                            bSidureyNahagut = true;
//                                        }
//                                    }
//                                }
//                            }
//                        }
//                        if (!bSidureyNahagut)
//                        {//אם לא היה מצב של שני סידורי נהגות ברציפות, נחשב את הפרש הזמן בין שני הסידורים 
//                            //ונשווה מול פרמטר 104
//                            fDiffTimeBetweenSidurim = clDefinitions.GetTimeBetweenTwoSidurimInMinuts(oSidurPrev, oSidur);
//                            if (fDiffTimeBetweenSidurim > oParam.iMinTimeBetweenSidurim)
//                            {
//                                oObjSidurimOvdimIns = new OBJ_SIDURIM_OVDIM();
//                                InsertToObjSidurimOvdimForInsert(ref oSidur, ref oObjSidurimOvdimIns);
//                                oObjSidurimOvdimIns.MISPAR_SIDUR = 99501;
//                                oObjSidurimOvdimIns.SHAT_HATCHALA = DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", oSidurPrev.sShatGmar)).AddMinutes(1);
//                                oObjSidurimOvdimIns.SHAT_GMAR = DateTime.Parse(string.Concat(dCardDate.ToShortDateString(), " ", oSidur.sShatHatchala)).AddMinutes(-1);
//                                oCollSidurimOvdimIns.Add(oObjSidurimOvdimIns);
//                            }
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

//        private void GetSidurCurrentTime(int iSidurIndex, ref clSidur oSidur,ref DateTime dSidurShatHatchala, ref DateTime dSidurShatGmar)
//        {
//            OBJ_SIDURIM_OVDIM oObj;
//            //DB-אם שונתה שעת הגמר/ההתחלה של הסידור נקח מהאובייקט העדכני,אחרת מה
//            try
//            {
//                oObj = GetSidurOvdimObject(iSidurIndex);

//                dSidurShatHatchala = oObj.SHAT_HATCHALA;
//                dSidurShatGmar = oObj.SHAT_GMAR;
                
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private void SetHourToSidur19(ref clSidur oSidur)
//        {
//            //DateTime dSidurShatHatchala, dSidurShatGmar;

//            //קביעת שעות לסידורים שזמן ההתחלה/גמר מותנה במאפיין אישי
//            try
//            {
//                //GetSidurCurrentTime(iSidurIndex, ref oSidur, ref dSidurShatHatchala, ref dSidurShatGmar);
//                //אם הסידור הוא מסוג "היעדרות" (סידור מיוחד עם מאפיין 53 בטבלת סידורים מיוחדים) והערך במאפיין הוא 8 (היעדרות בתשלום)  או 9 (ע"ח שעות נוספות)                   
//                SetSidurHeadrut(ref oSidur);
//                //4. סידור אינו מסומן במאפיין 78 (רק לסידורים מיוחדים):
//                //5. סידור מסומן במאפיין 78
//                SetSidurKizuz(ref oSidur);               
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }

       
//        private void SetSidurHeadrut(ref clSidur oSidur)
//        {          
//            DateTime dShatHatchalaLetashlum = new DateTime();
//            DateTime dShatGmarLetashlum = new DateTime();

//            if (oSidur.bSidurMyuhad)
//            {
//                //if ((oSidur.bHeadrutTypeKodExists) && (oSidur.sHeadrutTypeKod!=clGeneral.enMeafyenSidur53.enHeadrutWithPayment) &&
//                //    (oSidur.sHeadrutTypeKod!=clGeneral.enMeafyenSidur53.enHolidayForHours))
//                //{
//                //    oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = null;
//                //    oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = null;
//                //}


//                /*3. סידור מסוג העדרות (מקרה 2)
//                אם הסידור הוא מסוג "היעדרות" (סידור מיוחד עם מאפיין 53 בטבלת סידורים מיוחדים) והערך במאפיין הוא 8 (היעדרות בתשלום)  או 9 (ע"ח שעות נוספות)
//                שעת התחלה לתשלום = שעת התחלת מאפיין (המאפיין תלוי בסוג היום, ראה הסבר בעמודה שדות מעורבים)
//                שעת גמר לתשלום = שעת גמר מאפיין (המאפיין תלוי בסוג היום, ראה הסבר בעמודה שדות מעורבים).
//                (אין לעדכן שעת התחלה ושעת גמר)*/

//                if ((oSidur.bHeadrutTypeKodExists) && ((oSidur.sHeadrutTypeKod == clGeneral.enMeafyenSidur53.enHeadrutWithPayment.GetHashCode().ToString()) ||
//                    (oSidur.sHeadrutTypeKod == clGeneral.enMeafyenSidur53.enHolidayForHours.GetHashCode().ToString())))
//                {                    
//                    GetOvedShatHatchalaGmar(oMeafyeneyOved, ref oSidur, ref dShatHatchalaLetashlum, ref dShatGmarLetashlum);
//                    //אם שעת גמר המאפיין גדולה מפרמטר כללי מספר 0002  אזי שעת גמר לתשלום = פרמטר כללי 0002 (וימן 08/10/2009)
//                    oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = dShatHatchalaLetashlum;
//                    if (dShatGmarLetashlum > oParam.dSidurLimitShatGmar)
//                    {
//                        oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = oParam.dSidurLimitShatGmar;
//                    }
//                    else
//                    {
//                        oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = dShatGmarLetashlum;                        
//                    }
//                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                }
//            }
//        }

//        private void SetSidurKizuz(ref clSidur oSidur)
//        {           
//            if (oSidur.bSidurMyuhad)
//            {
//                if ((!oSidur.bKizuzAlPiHatchalaGmarExists) && (oObjSidurimOvdimUpd.LO_LETASHLUM == 0) && (!oSidur.bHeadrutTypeKodExists))
//                {                    
//                    //אם הסידור אינו מסומן במאפיין 78 (קיזוז התחלה / גמר) ואינו מסומן "לא לתשלום"
//                    //שעת התחלה לתשלום = שעת תחילת הסידור.
//                    //שעת גמר לתשלום = שעת גמר הסידור
//                    //(אין לעדכן שעת התחלה ושעת גמר)

//                    //ואם הסידור אינו מסוג "היעדרות" (סידור מיוחד עם מאפיין 53 בטבלת סידורים מיוחדים) (וימן 08/10/2009)
//                    oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = oObjSidurimOvdimUpd.SHAT_HATCHALA;
//                    oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = oObjSidurimOvdimUpd.SHAT_GMAR;//oSidur.dFullShatGmar;
//                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                }
//                else
//                {
//                    DateTime dShatHatchalaLetashlum = new DateTime();
//                    DateTime dShatGmarLetashlum = new DateTime();
//                    //5. סידור מסומן במאפיין 78
//                    //אם הסידור מסומן במאפיין 78 (קיזוז התחלה / גמר)
//                    //ואינו מסומן "לא לתשלום", ישנם כמה מקרים:
//                    if ((oSidur.bKizuzAlPiHatchalaGmarExists) && (oObjSidurimOvdimUpd.LO_LETASHLUM == 0))
//                    {                       
//                        GetOvedShatHatchalaGmar(oMeafyeneyOved, ref oSidur, ref dShatHatchalaLetashlum, ref dShatGmarLetashlum);

//                        //1 .אם אין סימון "קוד חריגה" (אין = null או 0) ואין סימון "מחוץ למיכסה" (אין = null או 0), שלושה מקרים:                                                                               
//                        if (((oObjSidurimOvdimUpd.CHARIGA == 0) && (oObjSidurimOvdimUpd.OUT_MICHSA==0)))                        
//                        {
//                            //א. אם שעת הגמר של הסידור לא גדולה משעת מאפיין ההתחלה המותרת (המאפיין תלוי בסוג היום, ראה עמודה  שדות מעורבים): יש לסמן את הסידור "לא לתשלום"
//                            //ב. אם שעת ההתחלה של הסידור לא קטנה משעת מאפיין הגמר המותרת (המאפיין תלוי בסוג היום, ראה עמודה  שדות מעורבים) - יש לסמן את הסידור "לא לתשלום                            
//                            if ((oObjSidurimOvdimUpd.SHAT_GMAR <= dShatHatchalaLetashlum) || (oObjSidurimOvdimUpd.SHAT_HATCHALA >= dShatGmarLetashlum))
//                            {
//                                oObjSidurimOvdimUpd.LO_LETASHLUM = 1;
//                                oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                            }
//                            if (oObjSidurimOvdimUpd.SHAT_HATCHALA < dShatHatchalaLetashlum)
//                            {
//                                //ג. אם שעת ההתחלה של הסידור קטנה משעת מאפיין ההתחלה המותרת (תלוי בסוג היום, ראה עמודה שדות מעורבים): שעת התחלה לתשלום = השעה המוגדרת במאפיין. 
//                                oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = dShatHatchalaLetashlum;
//                                oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                            }
//                            else
//                            {   //ד. אם שעת ההתחלה של הסידור אינה קטנה משעת מאפיין ההתחלה המותרת (תלוי בסוג היום, ראה עמודה שדות מעורבים): שעת התחלה לתשלום = שעת התחלת הסידור. 
//                                oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = oObjSidurimOvdimUpd.SHAT_HATCHALA;
//                                oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                            }
//                            if (oObjSidurimOvdimUpd.SHAT_GMAR > dShatGmarLetashlum)
//                            {
//                                //אם שעת הגמר של הסידור גדולה משעת מאפיין הגמר המותר (המאפיין תלוי בסוג היום, ראה עמודה  שדות מעורבים) : שעת גמר לתשלום = השעה המוגדרת במאפיין
//                                //if (dShatGmarLetashlum > oParam.dSidurLimitShatGmar)
//                                //{
//                                //    oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = oParam.dSidurLimitShatGmar;
//                                //    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                                //}
//                                //else
//                                //{
//                                    oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = dShatGmarLetashlum;
//                                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                                //}
                                
//                            }
//                            else
//                            {
//                                //ה. אם שעת הגמר של הסידור אינה גדולה משעת מאפיין הגמר המותר (המאפיין תלוי בסוג היום, ראה עמודה  שדות מעורבים) : שעת גמר לתשלום = שעת גמר הסידור
//                                oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = oObjSidurimOvdimUpd.SHAT_GMAR;
//                                oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                            }
//                        }
//                        else
//                        {
//                            //5.2. אם יש סימון "מחוץ למיכסה" ולא קבענו את הסידור "לא לתשלום":
//                            //שעת התחלה לתשלום = שעת תחילת הסידור 
//                            //שעת גמר לתשלום = שעת גמר הסידור
//                            if ((oObjSidurimOvdimUpd.OUT_MICHSA!=0)  && (oObjSidurimOvdimUpd.LO_LETASHLUM == 0))
//                            {
//                                oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = oObjSidurimOvdimUpd.SHAT_HATCHALA;
//                                oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = oObjSidurimOvdimUpd.SHAT_GMAR;
//                                oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                            }
//                            //3. אם יש סימון "קוד חריגה" ולא קבענו את הסידור "לא לתשלום", שלושה מקרים:                                                           
//                            if ((oObjSidurimOvdimUpd.CHARIGA != 0) && (oObjSidurimOvdimUpd.LO_LETASHLUM == 0))
//                            {
//                                //א. אם מסומן קוד חריגה "1"  (חריגה משעת התחלה) אזי שעת התחלה לתשלום = שעת תחילת הסידור.
//                                //אם שעת הגמר של הסידור גדולה משעת מאפיין הגמר המותר (המאפיין תלוי בסוג היום, ראה עמודה  שדות מעורבים): שעת גמר לתשלום = השעה המוגדרת במאפיין. 
//                                if (oObjSidurimOvdimUpd.CHARIGA == 1)
//                                {
//                                    oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = oObjSidurimOvdimUpd.SHAT_HATCHALA;
//                                    if (oObjSidurimOvdimUpd.SHAT_GMAR > dShatGmarLetashlum)
//                                    {
//                                        //if (dShatGmarLetashlum > oParam.dSidurLimitShatGmar)
//                                        //{
//                                        //    oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = oParam.dSidurLimitShatGmar;
//                                        //    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                                        //}
//                                        //else
//                                        //{
//                                        oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = dShatGmarLetashlum;
//                                        oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                                        //}
//                                    }
//                                    else
//                                    {
//                                        oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = oObjSidurimOvdimUpd.SHAT_GMAR;
//                                    }
//                                }  
//                                //ב. אם מסומן קוד חריגה "2"  (חריגה משעת גמר) אזי שעת גמר לתשלום = שעת גמר הסידור.
//                                //אם שעת התחלה של הסידור קטנה משעת מאפיין התחלה המותרת (המאפיין תלוי בסוג היום, ראה עמודה  שדות מעורבים) : שעת התחלה לתשלום = השעה המוגדרת במאפיין. 
//                                if (oObjSidurimOvdimUpd.CHARIGA == 2)
//                                {
//                                    oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = oObjSidurimOvdimUpd.SHAT_GMAR;
//                                    if (oSidur.dFullShatHatchala < dShatHatchalaLetashlum)
//                                    {
//                                        oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = dShatHatchalaLetashlum;
//                                    }
//                                    else
//                                    {
//                                        oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = oObjSidurimOvdimUpd.SHAT_HATCHALA;
//                                    }
//                                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                                }
//                                //ג. אם מסומן קוד חריגה "3"  (חריגה משעת התחלה וגמר) אזי :
//                                //שעת התחלה לתשלום = שעת תחילת הסידור.
//                                //שעת גמר לתשלום = שעת גמר הסידור
//                                if (oObjSidurimOvdimUpd.CHARIGA == 3)
//                                {
//                                    oObjSidurimOvdimUpd.SHAT_HATCHALA_LETASHLUM = oObjSidurimOvdimUpd.SHAT_HATCHALA;
//                                    oObjSidurimOvdimUpd.SHAT_GMAR_LETASHLUM = oObjSidurimOvdimUpd.SHAT_GMAR;
//                                    oObjSidurimOvdimUpd.UPDATE_OBJECT = 1;
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//        }
//        private void GetOvedShatHatchalaGmar(clMeafyenyOved oMeafyeneyOved,ref clSidur oSidur, 
//                                             ref DateTime dShatHatchalaLetashlum, ref DateTime dShatGmarLetashlum)
//        {
//            /*  יום חול 
//            שעת התחלה – מאפיין 03
//            שעת גמר       - מאפיין 04
//            יום שישי/ערב חג 
//            שעת התחלה – מאפיין 05
//            שעת גמר       - מאפיין 06
//            יום שבת/שבתון 
//            שעת התחלה – מאפיין 07
//            שעת גמר       - מאפיין 08*/

//            try
//            {
//                string sShaa, sMinutes;
//                int iPos, iShaa;

//                //יום שבת/שבתון
//                if ((oSidur.sSidurDay == clGeneral.enDay.Shabat.GetHashCode().ToString()) || (oSidur.sShabaton == "1"))
//                {
//                    if (oMeafyeneyOved.Meafyen7Exists)
//                    {
//                        if (oMeafyeneyOved.sMeafyen7 == "1")
//                        {
//                            dShatHatchalaLetashlum = DateTime.Parse(string.Concat(oSidur.dFullShatHatchala.ToShortDateString(), " 00:01"));
//                        }
//                        else
//                        {
//                            if (oMeafyeneyOved.sMeafyen7.Length > 2)
//                            {
//                                iPos = oMeafyeneyOved.sMeafyen7.Length - 2;
//                                sMinutes = oMeafyeneyOved.sMeafyen7.Substring(iPos, 2);
//                                sShaa = oMeafyeneyOved.sMeafyen7.Substring(0, iPos);
//                                sShaa = string.Concat(sShaa, ":", sMinutes);                               
//                                dShatHatchalaLetashlum = DateTime.Parse(string.Concat(oSidur.dFullShatHatchala.ToShortDateString(), " ", sShaa));
//                            }
//                        }
//                    }
//                    else
//                    {//אם אין מאפיין, נקבע את שעת הסידור כברירת מחדל
//                        dShatHatchalaLetashlum = oSidur.dFullShatHatchala;
//                    }
//                    if (oMeafyeneyOved.IsMeafyenExist(8))
//                    {
//                        if (oMeafyeneyOved.GetMeafyen(8).Value == "1")
//                        {
//                            dShatGmarLetashlum = DateTime.Parse(String.Concat(oObjSidurimOvdimUpd.SHAT_GMAR.ToShortDateString(), " 00:01"));
//                        }
//                        else
//                        {
//                            if (oMeafyeneyOved.GetMeafyen(8).Value.Length > 2)
//                            {
//                                iPos = oMeafyeneyOved.GetMeafyen(8).Value.Length - 2;
//                                sMinutes = oMeafyeneyOved.GetMeafyen(8).Value.Substring(iPos, 2);
//                                sShaa = oMeafyeneyOved.GetMeafyen(8).Value.Substring(0, iPos);
//                                iShaa = int.Parse(sShaa);
//                                if ((iShaa >= 24) && (iShaa <= 32))
//                                {
//                                    iShaa = iShaa - 24;
//                                    sShaa = iShaa.ToString().PadLeft(2, (char)48);
//                                    sShaa = string.Concat(sShaa, ":", sMinutes);
//                                    if (DateTime.Parse(oSidur.sSidurDate).ToShortDateString().Equals((oObjSidurimOvdimUpd.SHAT_GMAR.ToShortDateString())))
//                                    {
//                                        dShatGmarLetashlum = DateTime.Parse(string.Concat(oObjSidurimOvdimUpd.SHAT_GMAR.ToShortDateString(), " ", sShaa)).AddDays(1);
//                                    }
//                                    else
//                                    {
//                                        dShatGmarLetashlum = DateTime.Parse(string.Concat(oObjSidurimOvdimUpd.SHAT_GMAR.ToShortDateString(), " ", sShaa));
//                                    }                                    
//                                }
//                                else
//                                {
//                                    sShaa = string.Concat(sShaa, ":", sMinutes);
//                                    dShatGmarLetashlum = DateTime.Parse(String.Concat(oObjSidurimOvdimUpd.SHAT_GMAR.ToShortDateString(), " ", sShaa));
//                                }
//                            }
//                        }
//                    }                   
//                    else
//                    {//אם אין מאפיין, נקבע את שעת הסידור כברירת מחדל
//                        dShatGmarLetashlum = oObjSidurimOvdimUpd.SHAT_GMAR;
//                    }                   
//                }
//                else
//                {
//                    //יום שישי או ערב חג
//                    if ((oSidur.sSidurDay == clGeneral.enDay.Shishi.GetHashCode().ToString()) || (oSidur.sErevShishiChag == "1"))
//                    {
//                        if (oMeafyeneyOved.IsMeafyenExist(5))
//                        {
//                            if (oMeafyeneyOved.GetMeafyen(5).Value == "1")
//                            {
//                                dShatHatchalaLetashlum = DateTime.Parse(string.Concat(oSidur.dFullShatHatchala.ToShortDateString(), " 00:01"));
//                            }
//                            else
//                            {
//                                if (oMeafyeneyOved.GetMeafyen(5).Value.Length > 2)
//                                {
//                                    iPos = oMeafyeneyOved.GetMeafyen(5).Value.Length - 2;
//                                    sMinutes = oMeafyeneyOved.GetMeafyen(5).Value.Substring(iPos, 2);
//                                    sShaa = oMeafyeneyOved.GetMeafyen(5).Value.Substring(0, iPos);
//                                    sShaa = string.Concat(sShaa, ":", sMinutes);
//                                    dShatHatchalaLetashlum = DateTime.Parse(string.Concat(oSidur.dFullShatHatchala.ToShortDateString(), " ", sShaa));
//                                }
//                            }
//                        }
//                        else
//                        {//אם אין מאפיין, נקבע את שעת הסידור כברירת מחדל
//                            dShatHatchalaLetashlum = oSidur.dFullShatHatchala;
//                        }
//                        if (oMeafyeneyOved.IsMeafyenExist(6))
//                        {
//                            if (oMeafyeneyOved.GetMeafyen(6).Value == "1")
//                            {
//                                dShatGmarLetashlum = DateTime.Parse(string.Concat(oObjSidurimOvdimUpd.SHAT_GMAR.ToShortDateString(), " 00:01"));
//                            }
//                            else
//                            {
//                                if (oMeafyeneyOved.GetMeafyen(6).Value.Length > 2)
//                                {
//                                    iPos = oMeafyeneyOved.GetMeafyen(6).Value.Length - 2;
//                                    sMinutes = oMeafyeneyOved.GetMeafyen(6).Value.Substring(iPos, 2);
//                                    sShaa = oMeafyeneyOved.GetMeafyen(6).Value.Substring(0, iPos);
//                                    iShaa = int.Parse(sShaa);
//                                    if ((iShaa >= 24) && (iShaa <= 32))
//                                    {
//                                        iShaa = iShaa - 24;
//                                        sShaa = iShaa.ToString().PadLeft(2, (char)48);
//                                        sShaa = string.Concat(sShaa, ":", sMinutes);
//                                        if (DateTime.Parse(oSidur.sSidurDate).ToShortDateString().Equals(oObjSidurimOvdimUpd.SHAT_GMAR.ToShortDateString()))
//                                        {
//                                            dShatGmarLetashlum = DateTime.Parse(string.Concat(oObjSidurimOvdimUpd.SHAT_GMAR.ToShortDateString(), " ", sShaa)).AddDays(1);
//                                        }
//                                        else
//                                        {
//                                            dShatGmarLetashlum = DateTime.Parse(string.Concat(oObjSidurimOvdimUpd.SHAT_GMAR.ToShortDateString(), " ", sShaa));
//                                        }                                        
//                                    }
//                                    else
//                                    {
//                                        sShaa = string.Concat(sShaa, ":", sMinutes);
//                                        dShatGmarLetashlum = DateTime.Parse(string.Concat(oObjSidurimOvdimUpd.SHAT_GMAR.ToShortDateString(), " ", sShaa));
//                                    }
//                                }
//                            }
//                        }
//                        else
//                        {//אם אין מאפיין, נקבע את שעת הסידור כברירת מחדל
//                            dShatGmarLetashlum = oObjSidurimOvdimUpd.SHAT_GMAR;
//                        }  
//                    }
//                    else
//                    {   //יום חול
//                        if (oMeafyeneyOved.IsMeafyenExist(3))
//                        {
//                            if (oMeafyeneyOved.GetMeafyen(3).Value == "1")
//                            {
//                                dShatHatchalaLetashlum = DateTime.Parse(string.Concat(oSidur.dFullShatHatchala.ToShortDateString(), " 00:01"));
//                            }
//                            else
//                            {
//                                if (oMeafyeneyOved.GetMeafyen(3).Value.Length > 2)
//                                {
//                                    iPos = oMeafyeneyOved.GetMeafyen(3).Value.Length - 2;
//                                    sMinutes = oMeafyeneyOved.GetMeafyen(3).Value.Substring(iPos, 2);
//                                    sShaa = oMeafyeneyOved.GetMeafyen(3).Value.Substring(0, iPos);
//                                    //iShaa = int.Parse(sShaa);
//                                    //if ((iShaa >= 24) && (iShaa <= 28))
//                                    //{
//                                    //    iShaa = iShaa - 24;
//                                    //    sShaa = iShaa.ToString().PadLeft(2, (char)48);
//                                    //    sShaa = string.Concat(sShaa, ":", sMinutes);
//                                    //    dShatHatchalaLetashlum = DateTime.Parse(string.Concat(oSidur.dFullShatHatchala.ToShortDateString(), " ", sShaa)).AddDays(1);
//                                    //}
//                                    //else
//                                    //{
//                                    sShaa = string.Concat(sShaa, ":", sMinutes);
//                                    dShatHatchalaLetashlum = DateTime.Parse(string.Concat(oSidur.dFullShatHatchala.ToShortDateString(), " ", sShaa));
//                                    // }
//                                }
//                            }
//                        }
//                        else
//                        {//אם אין מאפיין, נקבע את שעת הסידור כברירת מחדל
//                            dShatHatchalaLetashlum = oSidur.dFullShatHatchala;
//                        }  

//                        if (oMeafyeneyOved.IsMeafyenExist(4))
//                        {
//                            if (oMeafyeneyOved.GetMeafyen(4).Value == "1")
//                            {
//                                dShatGmarLetashlum = DateTime.Parse(string.Concat(oObjSidurimOvdimUpd.SHAT_GMAR.ToShortDateString(), " 00:01"));
//                            }
//                            else
//                            {
//                                if (oMeafyeneyOved.GetMeafyen(4).Value.Length > 2)
//                                {
//                                    iPos = oMeafyeneyOved.GetMeafyen(4).Value.Length - 2;
//                                    sMinutes = oMeafyeneyOved.GetMeafyen(4).Value.Substring(iPos, 2);
//                                    sShaa = oMeafyeneyOved.GetMeafyen(4).Value.Substring(0, iPos);
//                                    iShaa = int.Parse(sShaa);
//                                    if ((iShaa >= 24) && (iShaa <= 32))
//                                    {
//                                        iShaa = iShaa - 24;
//                                        sShaa = iShaa.ToString().PadLeft(2, (char)48);
//                                        sShaa = string.Concat(sShaa, ":", sMinutes);
//                                        if (DateTime.Parse(oSidur.sSidurDate).ToShortDateString().Equals(oObjSidurimOvdimUpd.SHAT_GMAR.ToShortDateString()))
//                                        {
//                                            dShatGmarLetashlum = DateTime.Parse(string.Concat(oObjSidurimOvdimUpd.SHAT_GMAR.ToShortDateString(), " ", sShaa)).AddDays(1);
//                                        }
//                                        else
//                                        {
//                                            dShatGmarLetashlum = DateTime.Parse(string.Concat(oObjSidurimOvdimUpd.SHAT_GMAR.ToShortDateString(), " ", sShaa));
//                                        }
//                                    }
//                                    else
//                                    {
//                                        sShaa = string.Concat(sShaa, ":", sMinutes);
//                                        dShatGmarLetashlum = DateTime.Parse(string.Concat(oObjSidurimOvdimUpd.SHAT_GMAR.ToShortDateString(), " ", sShaa));
//                                    }
//                                }
//                            }
//                        }
//                        else
//                        {//אם אין מאפיין, נקבע את שעת הסידור כברירת מחדל
//                            dShatGmarLetashlum = oObjSidurimOvdimUpd.SHAT_GMAR;
//                        }  
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                throw ex;
//            }
//        }
//        private class clNewSidurim
//        {
//            //האובייקט יכיל את כל הסידורים שמספר הסידור לא תקין  ובעקבות שינוי 1 קיבלו מספר חדש
//            int _iSidurIndex;
//            int _iSidurOld;
//            int _iSidurNew;
//            public int SidurIndex
//            {
//                get
//                {
//                    return this._iSidurIndex;
//                }
//                set
//                {
//                    this._iSidurIndex = value;
//                }
//            }
//            public int SidurOld
//            {
//                get
//                {
//                    return this._iSidurOld;
//                }
//                set
//                {
//                    this._iSidurOld = value;
//                }
//            }
//            public int SidurNew
//            {
//                get
//                {
//                    return this._iSidurNew;
//                }
//                set
//                {
//                    this._iSidurNew = value;
//                }
//            }
//        }
//    }
//}
