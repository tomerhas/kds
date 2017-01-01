CREATE OR REPLACE PACKAGE          Pkg_Attendance AS
TYPE    CurType      IS    REF  CURSOR;
/******************************************************************************
   NAME:       Pkg_Attendance
   PURPOSE:

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        05/07/2015             1. Created this package.
******************************************************************************/

    PROCEDURE pro_new_recHarmony( pMISPAR_ISHI varchar, pTAARICH varchar, pShaa varchar, pMispar_shaon varchar,pMISPAR_ISHI_chk varchar, 
                            pSTATUS_data varchar, pstatus_ans varchar, pclock_inner_num varchar,  psite_kod varchar, pclock_num_in_site varchar,                            
                            pclock_name varchar, prec_time_stmp varchar, paction_kod varchar, ptbl_num varchar); 
                            
    function fun_get_number_code(ACTION_KOD in VARCHAR2,TBL_NUM  in VARCHAR2 ) return number;
    PROCEDURE  pro_new_recHarmony_err(   pErr_kod in number,    pErr_filename  in varchar,  pErr_pref in varchar ,pErr_line in varchar , pErr_suff in varchar , pLineErrCnt in number );
    PROCEDURE  Pro_diffHarmony (p_Cur OUT CurType) ;
    PROCEDURE  Ins_TahalichHarmony(   pfilename  in varchar  ) ;
    PROCEDURE pro_ins_control_clock( p_taarich in date,  p_status  in number,  p_teur in varchar );
    PROCEDURE pro_upd_control_clock( p_taarich in date,  p_status  in number,  p_teur in varchar ) ;
    function pro_get_last_hour_clock return varchar;
    PROCEDURE pro_ins_shaonim_movment(p_bakasha number,p_coll_Harmony_movment IN coll_Harmony_movment_Err_Mov );
    function pro_get_last_attend_cntl return number;
    procedure pro_Insert_Tnuot_Shaon(p_bakasha in number);
    PROCEDURE  pro_fetch_AttendHarmony (p_Cur OUT CurType) ;
    PROCEDURE  pro_new_control_Attend( pTAARICH in DATE,  pSTATUS  in number,  pteur in varchar );
    PROCEDURE  pro_Upd_control_Attend( pTAARICH in DATE,  pSTATUS  in number,  pteur in varchar ) ; 
    PROCEDURE  pro_check_Yetzia (pMISPAR_ISHI in NUMBER, pTAARICH in DATE, pShaa in varchar, pmispar_sidur in NUMBER,p24  in NUMBER, p_Cur OUT CurType) ;
    PROCEDURE  pro_GetOutNull(pMISPAR_ISHI in number, pTAARICH in date,pKnisaHH NUMBER, pKnisaMM NUMBER,  pmispar_sidur in number,p24  in NUMBER, p_cur OUT CurType);
    PROCEDURE  pro_UpdKnisa(pMISPAR_ISHI in number, pTAARICH in date,pKnisaHH NUMBER, pKnisaMM NUMBER ,pMIKUM number,pYetziaHH NUMBER, pYetziaMM NUMBER, pmispar_sidur in number,pStm in varchar,p24  in NUMBER) ;
    PROCEDURE pro_GetInNull(pMISPAR_ISHI in number, pTAARICH in date,pYetziaHH NUMBER, pYetziaMM NUMBER,  pmispar_sidur in number,p24  in number,p_cur OUT CurType);
    PROCEDURE  pro_ins_Yetzia (pMISPAR_ISHI in number, pTAARICH in DATE, pShaa in varchar, pMikum in number, pmispar_sidur in number,p24  in number,pStm in varchar) ;
    PROCEDURE  pro_UpdYetzia(pMISPAR_ISHI in number, pTAARICH in DATE,pKnisaHH NUMBER, pKnisaMM NUMBER ,pMIKUM number,pYetziaHH NUMBER, pYetziaMM NUMBER, pmispar_sidur in number,p24  in number,pStm in varchar) ;
    PROCEDURE  pro_check_In (pMISPAR_ISHI in number, pTAARICH in DATE, pShaa in varchar,pmispar_sidur in number,p24  in number, p_Cur OUT CurType) ;
    PROCEDURE  pro_ins_In (pMISPAR_ISHI in number, pTAARICH in DATE, pShaa in varchar, pMikum in number,  pmispar_sidur in number, pStm in varchar,p24  in number) ;
PROCEDURE pro_ins_hityazvut_pundakim(p_mispar_ishi in number,p_taarich in date,p_shaa in date,p_mikum in number);
   
END Pkg_Attendance;
/

CREATE OR REPLACE PACKAGE          Pkg_Reports AS
/******************************************************************************
   NAME:       PKG_REPORTS
   PURPOSE:

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        27/05/2009             1. Created this package.
******************************************************************************/
 TYPE    CurType      IS    REF  CURSOR;

PROCEDURE pro_get_ishurim_lekartis(p_mispar_ishi IN TB_ISHURIM.MISPAR_ISHI%TYPE,
                                                                                   p_taarich IN TB_ISHURIM.TAARICH%TYPE,
                                                                               p_cur OUT CurType) ;
PROCEDURE pro_get_ProfilToDisplay(p_ProfilFilter IN VARCHAR2 , p_cur OUT CurType);
PROCEDURE pro_get_Comment_Approval(p_Cur OUT CurType);
  PROCEDURE pro_get_Regions(p_cur OUT CurType);
PROCEDURE pro_get_Status_Approval(p_cur OUT CurType);
PROCEDURE pro_get_Factors_Confirm(p_cur OUT CurType);
PROCEDURE pro_get_CompanyId ( p_cur OUT CurType ) ;

PROCEDURE pro_get_Ishurim_Rashemet (p_cur OUT Curtype ,
                                                                 P_WorkerViewLevel IN NUMBER, 
                                                                 P_WORKERID IN VARCHAR2, 
                                                                 P_MISPAR_ISHI IN VARCHAR2,
                                                                 P_STARTDATE IN DATE,
                                                                 P_ENDDATE IN DATE ,
                                                                 P_KOD_ISHUR TB_ISHURIM.Kod_Status_Ishur%TYPE ,
                                                                 P_FactorConfirm IN OVDIM.mispar_ishi%TYPE,
                                                                 P_STATUS IN TB_ISHURIM.Kod_Status_Ishur%TYPE ) ;

PROCEDURE pro_get_Ishurim_RashemetOld (p_cur OUT Curtype ,
                                                                 P_MISPAR_ISHI IN OVDIM.mispar_ishi%TYPE,
                                                                 P_STARTDATE IN DATE,
                                                                 P_ENDDATE IN DATE ,
                                                                 P_KOD_ISHUR TB_ISHURIM.Kod_Status_Ishur%TYPE ,
                                                                 P_FactorConfirm IN OVDIM.mispar_ishi%TYPE,
                                                                 P_STATUS IN TB_ISHURIM.Kod_Status_Ishur%TYPE ) ;
PROCEDURE pro_get_SidurimOvdim(p_FromDate IN DATE , p_ToDate IN DATE, p_prefix IN VARCHAR2 , p_cur OUT CurType);

PROCEDURE pro_get_WorkStation(p_prefix IN VARCHAR2 , p_cur OUT CurType);

PROCEDURE pro_get_LicenseNumber(p_prefix IN VARCHAR2 , p_cur OUT CurType);

PROCEDURE pro_get_Id_of_Yamey_Avoda(p_FromDate IN DATE , p_ToDate IN DATE,p_Prefix IN VARCHAR2, p_cur OUT CurType) ;
PROCEDURE pro_get_MakatOfActivity(p_FromDate IN DATE , p_ToDate IN DATE, p_prefix IN VARCHAR2 , p_cur OUT CurType);
PROCEDURE pro_get_Id_of_Yamey_Avoda(p_FromDate IN DATE , p_ToDate IN DATE ,p_CompanyId IN VARCHAR2,p_Prefix IN VARCHAR2, p_cur OUT CurType);

PROCEDURE pro_get_Find_Worker_Card_2( p_cur OUT CurType, 
                                                                P_STARTDATE IN DATE,
                                                                P_ENDDATE IN DATE , 
                                                                P_SIDURNUMBER IN VARCHAR2,
                                                                P_Makat IN VARCHAR2,
                                                                P_MISPARVISA IN VARCHAR2, 
                                                                P_CARNUMBER IN VARCHAR2,
                                                                P_RISHUYCAR   IN  VARCHAR2,
                                                                P_WORKERID  IN  VARCHAR2,
                                                                P_EZOR  IN VARCHAR2,                                                               
                                                                P_SNIF IN VARCHAR2,
                                                                P_MAMAD IN VARCHAR2
                                                                 );
 procedure pro_prepare_netuney_tnua_itur( P_STARTDATE IN DATE, P_ENDDATE IN DATE ,   P_SIDURNUMBER IN VARCHAR2,  p_lst_makat IN VARCHAR2, P_WORKERID  IN  VARCHAR );                                                                
 function  fun_get_teur_nesia(p_makat number,p_mispar_knisa number,p_teur_tnua nvarchar2 )return nvarchar2;
  PROCEDURE pro_get_Find_Worker_Card(p_cur OUT CurType, 
                                                                P_STARTDATE IN DATE,
                                                                P_ENDDATE IN DATE , 
                                                                P_Makat  IN  VARCHAR2, 
                                                                P_SIDURNUMBER  IN  VARCHAR2,
                                                                P_CARNUMBER  IN  VARCHAR2,
                                                                P_SNIF  IN  VARCHAR2,
                                                                P_WORKSTATION  IN  VARCHAR2,
                                                                P_DAYTYPE  IN  VARCHAR2 ,
                                                                P_WORKERID  IN  VARCHAR2,
                                                                P_RISHUYCAR   IN  VARCHAR2,
                                                                   P_SECTORISUK IN VARCHAR2 ,
                                                                P_SNIF_MASHAR IN VARCHAR2 , 
                                                                P_COMPANYID IN VARCHAR2 ,
                                                                P_MAMAD IN VARCHAR2 ,
                                                                   P_EZOR IN VARCHAR2 ,
                                                                P_ISUK IN VARCHAR2 
                                                                 ) ;                                                             
    FUNCTION fct_MakatDateSql    (  P_STARTDATE IN DATE,
                                                P_ENDDATE IN DATE ,
                                                P_Makat IN VARCHAR,
                                                P_SIDURNUMBER IN VARCHAR,
                                                P_WORKERID IN VARCHAR ) RETURN VARCHAR ;
  PROCEDURE pro_Prepare_Catalog_Details ( GeneralQry IN VARCHAR )    ;



PROCEDURE pro_get_Presence (p_cur OUT Curtype ,
                                                                 P_MISPAR_ISHI IN VARCHAR2 ,
                                                                P_STARTDATE IN DATE,
                                                                 P_ENDDATE IN DATE,
                                                                P_SNIF IN VARCHAR2,
                                                                  P_WorkerViewLevel IN NUMBER, 
                                                   P_WORKERID IN VARCHAR2
                                                                 );
                                                                 
PROCEDURE pro_get_Presence_all (p_cur OUT Curtype ,
                                         P_STARTDATE IN DATE,
                                        P_ENDDATE IN DATE,
                                         P_MISPAR_ISHI IN VARCHAR2 ,
                                        P_SNIF IN VARCHAR2,
                                          P_WorkerViewLevel IN NUMBER, 
                           P_WORKERID IN VARCHAR2
                                                                 );
PROCEDURE pro_get_DriverWithoutTacograph  (p_cur OUT CurType ,
                                                                            p_ezor IN VARCHAR2,
                                                                            p_Period IN VARCHAR2);

  PROCEDURE Pro_Get_MaxTimeNoTacograph(p_cur OUT CurType ,
                                                                            p_Period IN VARCHAR2);

  PROCEDURE pro_get_DriverNotSigned  (p_cur OUT CurType ,
                                                                            p_ezor IN VARCHAR2,
                                                                            p_Period IN VARCHAR2) ;

PROCEDURE pro_get_SumDriverNotSigned  (p_cur OUT CurType ,
                                                                            p_ezor IN VARCHAR2,
                                                                            p_Period IN VARCHAR2);
 FUNCTION get_First_Snif_Relevant(p_start IN DATE,p_end IN DATE,p_ezor IN VARCHAR2) RETURN VARCHAR2;

 

PROCEDURE Pro_Get_DisregardDrivers(p_Cur OUT CurType ,
                                     P_DISREGARDTYPE IN NUMBER,
                                     p_Period IN VARCHAR2);

   PROCEDURE Pro_Get_PremiotPresence(p_Cur OUT CurType ,
                                                        p_kod_premia IN MEAFYENIM_OVDIM.KOD_MEAFYEN%TYPE,
                                                        p_Period IN VARCHAR2,
                                                        p_ezor VARCHAR2,
                                                        P_MISPAR_ISHI IN VARCHAR2 ) ;
PROCEDURE Pro_PremiotPresence_tenderim(p_Cur OUT CurType ,
                                                        p_kod_premia IN MEAFYENIM_OVDIM.KOD_MEAFYEN%TYPE,  
                                                         p_Period IN VARCHAR2,
                                                           p_ezor VARCHAR2,
                                                        P_MISPAR_ISHI IN VARCHAR2  ) ;                                                        
   FUNCTION fun_get_hour( shaa varchar2) return VARCHAR2;                                                              
 FUNCTION FUN_GET_ERECH_RECHIV(p_mispar_ishi number, p_taarich DATE,p_bakasha_id number, p_kod_rchiv number) return VARCHAR2;                                       
 PROCEDURE pro_get_rizot_chishuv_succeed(  P_CHODESH_HARAZA  IN VARCHAR2,
                                                                                           p_Cur OUT CurType)  ;
    PROCEDURE  pro_get_maamadot(p_Cur OUT CurType );

PROCEDURE pro_get_AbsenceType( p_Cur OUT CurType );
PROCEDURE pro_get_WorkDayList( p_Cur OUT CurType );

PROCEDURE  pro_get_pirtey_rizot(listMispareiRizot IN VARCHAR2,
                                                                                                      IdReport IN NUMBER,
                                                                      strChodashim IN VARCHAR2,
                                                                   p_Cur OUT CurType );

PROCEDURE  pro_getNetuneyHashvaatRizot( P_CHODESH IN VARCHAR2,
                                                                                                              P_MIS_RITZA IN INTEGER,
                                                                                                          P_MIS_RITZA_LEHASVAA IN INTEGER,
                                                                                                      P_MAMAD IN VARCHAR2,
                                                                                                      P_ISUK IN VARCHAR2,
                                                                                                      P_MISPAR_ISHI IN VARCHAR2,
                                                                                           p_Cur OUT CurType ) ;


PROCEDURE pro_get_rizot_chishuv_lehodesh(  P_CHODESH_HARAZA  IN VARCHAR2,
                                                                                           p_Cur OUT CurType) ;

PROCEDURE  pro_get_ramot_chishuv_letezuga(p_Cur OUT CurType );

PROCEDURE  pro_GetHashvaatChodsheyRizot( P_CHODESH IN VARCHAR2,
                                                                                                           P_MIS_RITZA IN INTEGER,
                                                                                                       P_CHODESH_LEHASHVAA IN  VARCHAR2,
                                                                                                   P_MIS_RITZA_LEHASVAA IN INTEGER,
                                                                                                   P_RAMA_LETEZUGA IN VARCHAR2,
                                                                                                    p_Cur OUT CurType );

PROCEDURE  pro_get_reports_list(P_PROFIL  IN VARCHAR2,
                                                                             p_Cur OUT CurType );

    PROCEDURE  pro_GetNetuneyIdkuneyRashemet( P_MIS_RASHEMET IN INTEGER,
                                                                                                           P_TAARICH_CA IN VARCHAR2,  -- CA=Cartis Avoda
                                                                                                       P_SHAA IN VARCHAR2,
                                                                                                    p_Cur OUT CurType ) ;

PROCEDURE PREPARE_DAYLIST_OF_RPT( CurrentMonth VARCHAR2 );
PROCEDURE pro_Get_AbsentWorkers(P_PERIOD IN  VARCHAR2 ,
                                                   P_EZOR IN NUMBER ,
                                                   P_ABSENCE_TYPE IN NUMBER ,
                                                   P_WORKDAYS IN NUMBER,
                                                   P_MAMAD IN NUMBER ,
                                                   p_cur OUT CurType ) ;

PROCEDURE pro_GetMainDetailsAverage (P_STARTDATE IN DATE, 
                                            P_ENDDATE IN DATE ,
                                            P_MisparIshi  IN VARCHAR2 ,
                                            P_EZOR IN VARCHAR ,
                                            P_COMPANYID  IN VARCHAR2 ,
                                            P_MAMAD IN VARCHAR2 ,
                                            P_MAMAD_HR IN VARCHAR2 ,
                                            P_SNIF IN VARCHAR2 ,
                                            P_ISUK VARCHAR2 ,
                                            P_KOD_YECHIDA IN VARCHAR2 ,
                                            P_SECTORISUK IN VARCHAR2 ,
                                             P_rechiv IN VARCHAR2 ,
                                            P_WORKERID in number,
                                            P_WorkerViewLevel IN NUMBER, 
                                            p_cur OUT CurType ) ;
PROCEDURE pro_GetAverageExl (P_STARTDATE IN DATE, 
                                            P_ENDDATE IN DATE ,
                                            P_MisparIshi  IN VARCHAR2 ,
                                            P_EZOR IN VARCHAR ,
                                            P_COMPANYID  IN VARCHAR2 ,
                                            P_MAMAD IN VARCHAR2 ,
                                            P_MAMAD_HR IN VARCHAR2 ,
                                            P_SNIF IN VARCHAR2 ,
                                            P_ISUK VARCHAR2 ,
                                            P_KOD_YECHIDA IN VARCHAR2 ,
                                            P_SECTORISUK IN VARCHAR2 ,
                                            P_RECHIV IN VARCHAR2 ,
                                            P_WORKERID in number,
                                            P_WorkerViewLevel IN NUMBER, 
                                            p_cur OUT CurType ) ;
PROCEDURE pro_GetRechivValueAverage (P_STARTDATE IN DATE,
                                            P_ENDDATE IN DATE ,
                                            P_MisparIshi  IN NUMBER,
                                            p_cur OUT CurType ) ;
FUNCTION fct_GetSumMonthWithRechiv (P_STARTDATE IN DATE, 
                                            P_ENDDATE IN DATE ,
                                            P_MisparIshi  IN VARCHAR2 ) RETURN  NUMBER;
PROCEDURE pro_GetDescriptionComponents (P_STARTDATE  IN VARCHAR2,
                                          P_ENDDATE IN VARCHAR2,
                                             P_COMPANYID  IN VARCHAR2,
                                            p_cur OUT CurType ) ;

PROCEDURE  pro_GetPundakimLeHitchashbenut( P_STARTDATE IN TB_HITYAZVUT_PUNDAKIM.TAARICH%TYPE,
                                                            P_ENDDATE IN TB_HITYAZVUT_PUNDAKIM.TAARICH%TYPE,
                                                     p_Cur OUT CurType ) ;
PROCEDURE pro_get_Id_of_Ovdim(p_FromDate IN DATE , p_ToDate IN DATE ,
                                                                        p_preFix IN VARCHAR2,  p_cur OUT CurType) ;
PROCEDURE pro_ins_Heavy_Reports (p_BakashaId IN TB_BAKASHOT.bakasha_id%TYPE, 
                                                   p_ReportName IN CTB_SUGEY_DOCHOT.SHEM_DOCH_BAKOD%TYPE,
                                                   p_ReportParams IN    COLL_Report_Param,
                                                   p_MisparIshi IN NUMBER ,
                                                   p_Extension IN TB_DOCHOT_MISPAR_ISHI.EXTENSION_TYPE%TYPE ,
                                                   p_DestinationFolder IN TB_DOCHOT_MISPAR_ISHI.SHEM_TIKIYA%TYPE,
                                                   p_SendToMail IN NUMBER);

PROCEDURE pro_get_ReportDetails ( p_kodReport IN CTB_SUGEY_DOCHOT.KOD_SUG_DOCH%TYPE, p_cur OUT CurType); 
PROCEDURE  pro_get_Definition_Reports(p_BakashaId IN  TB_BAKASHOT.bakasha_id%TYPE, p_cur OUT CurType) ;
PROCEDURE pro_get_Details_Reports      (p_BakashaId IN  TB_BAKASHOT.bakasha_id%TYPE, p_cur OUT CurType);
PROCEDURE pro_get_Destinations_Reports(p_BakashaId IN  TB_BAKASHOT.bakasha_id%TYPE,p_cur OUT CurType);

/*PROCEDURE pro_Get_TlunotPundakim(P_STARTDATE in TB_PEILUT_OVDIM.TAARICH%TYPE,
                                                       P_ENDDATE in TB_PEILUT_OVDIM.TAARICH%TYPE,
                                                P_MISPAR_ISHI in VARCHAR2,
                                                pCur out Curtype ) ;            */
  PROCEDURE Pro_Get_TlunotPundakim(p_Cur OUT Curtype ,
                                                                                              P_STARTDATE IN VARCHAR2,
                                                                                             P_ENDDATE IN VARCHAR2,
                                                                                    P_MISPAR_ISHI IN VARCHAR2,
                                                                                    P_SUG_REPORT NUMBER ) ;

PROCEDURE pro_Prepare_Netunim_Tnua(P_STARTDATE IN VARCHAR2,
                                                                                 P_ENDDATE IN VARCHAR2,
                                                                      P_MISPAR_ISHI IN VARCHAR2);

PROCEDURE pro_getPirteyOvedForWorkCard(p_Cur OUT Curtype,
                                                                                              P_TAARICH IN DATE,
                                                                                                P_MISPAR_ISHI IN NUMBER) ;
function pro_get_snif_giyus_lekav(p_kod_hevra in number,HATZAVA_LEKAV in number ) return VARCHAR2;                                                                                                
  PROCEDURE pro_get_list_Rishuy_rechev    (   p_Cur OUT Curtype,
                                                                    P_TAARICH IN DATE,                                                             
                                                               P_MISPAR_ISHI IN NUMBER,
                                                                p_type in number);
 function fun_get_list_oto( P_TAARICH IN DATE,
                                      P_MISPAR_ISHI IN NUMBER,
                                       p_type in number,
                                        p_mispar_sidur IN NUMBER) return varchar;
PROCEDURE pro_getSidurimVePeiluyotForWC(p_Cur OUT Curtype,
                                                                                                    P_TAARICH IN DATE,
                                                                                                P_MISPAR_ISHI IN NUMBER) ;
FUNCTION IsEilatTrip(km NUMBER,taarich DATE, eilatTrip NUMBER) RETURN NUMBER ;
FUNCTION bdok_shibuz_mechona(P_TAARICH IN DATE, P_MISPAR_ISHI IN NUMBER) RETURN NUMBER ;
PROCEDURE pro_getPrepareReports(P_MISPAR_ISHI IN NUMBER,
                                                                                          P_STATUS_LIST IN VARCHAR2,
                                                                                         p_Cur OUT Curtype ) ;

PROCEDURE pro_updatePrepareReports(P_MISPAR_ISHI IN NUMBER ) ;    
PROCEDURE pro_getSidureyVisaForWC(p_Cur OUT Curtype,
                                                                                  P_TAARICH IN DATE,
                                                                                       P_MISPAR_ISHI IN NUMBER) ;        
    
PROCEDURE pro_get_HeavyReportsToDelete(p_Cur OUT Curtype) ;            
PROCEDURE pro_ChafifotBesidureyNihulTnua(P_STARTDATE IN  TB_SIDURIM_OVDIM.taarich%TYPE ,P_ENDDATE IN TB_SIDURIM_OVDIM.taarich%TYPE,
                                                                                                            P_EZOR IN NUMBER, P_SNIF IN NUMBER,
                                                                                                            p_Cur OUT Curtype);            
FUNCTION fun_get_snifey_tnua_lezor(p_ezor IN NUMBER,p_snif IN NUMBER ,p_from number,p_to number) RETURN VARCHAR;                                                                                               
PROCEDURE    pro_GetSnifeyTnuaByEzor(p_ezor IN NUMBER,p_Cur OUT Curtype);
PROCEDURE proGetSnifeyTnuaByEzor(p_ezor IN VARCHAR,p_snif_av in VARCHAR,p_Cur OUT Curtype);          
PROCEDURE pro_get_Nesiot_kfulot(P_STARTDATE IN  DATE ,P_ENDDATE IN DATE,
                                                                                                            P_EZOR IN NUMBER, P_SNIF IN NUMBER,
                                                                                                            p_Cur OUT Curtype);        
FUNCTION get_snif_mashar(p_mispar_ishi IN NUMBER, p_taarich IN DATE,p_oto_no IN NUMBER) RETURN VARCHAR ;            
procedure pro_getPirteyOved(p_isuk IN NUMBER,p_tachana IN NUMBER, p_Cur OUT Curtype);
 PROCEDURE  pro_GetIdkuneyRashemetMasach4( P_MISPAR_ISHI IN VARCHAR2 ,
                                                                        P_PERIOD IN VARCHAR2,
                                                                        p_Cur OUT CurType );   
FUNCTION pro_get_oved_full_name(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE) RETURN VARCHAR;                                                                        
PROCEDURE  pro_get_pirtey_premiya( P_KOD_PREMIYA IN NUMBER, 
                                                         p_Cur OUT CurType ) ;     
                                                         
  PROCEDURE pro_get_rechivim_to_average (p_start_date in date, 
                                            p_end_date in date ,
                                            p_mispar_ishi  in number,
                                             p_rechiv in nvarchar2,
                                            p_cur out curtype );

PROCEDURE pro_average_snif_ezor (p_startdate in date, 
                                          p_enddate in date ,
                                          p_ezor  in varchar2,
                                          p_snif  in varchar2,
                                          P_MAMAD_HR IN VARCHAR2 ,
                                          P_ISUK in VARCHAR2 ,
                                           P_WORKERVIEWLEVEL IN NUMBER,
                                          P_WORKERID in number,
                                          p_cur out curtype ) ;
 PROCEDURE pro_average_ovdim_at_snif (p_startdate in date, 
                                          p_enddate in date ,
                                          p_ezor  in varchar2,
                                          p_snif  in varchar2,
                                          P_WORKERVIEWLEVEL IN NUMBER, 
                                          P_WORKERID in number,
                                          p_cur out curtype );                                         
 PROCEDURE get_query4 ( p_mispar_ishi  in OVDIM.mispar_ishi%TYPE,p_date in varchar2,  p_cur out curtype );            
procedure get_GetDayDataEggT( p_BakashaId IN TB_BAKASHOT.bakasha_id%TYPE,  
                                            --    p_Period IN VARCHAR2,
                                                p_cur out curtype ); 
                                                
procedure pro_ProfilLinesDetails( P_STARTDATE IN DATE,
                                        P_ENDDATE IN DATE , 
                                        P_Makat IN varchar2,
                                        p_cur OUT CurType)   ;                                                                                                                       
function fn_get_makat_type(makat_nesia in number) return number;     
procedure pro_ProfilLinesSummed( P_STARTDATE IN DATE,
                                        P_ENDDATE IN DATE , 
                                        P_Makat IN varchar2,
                                        p_cur OUT CurType)    ;       
  
PROCEDURE pro_get_Bakashot_details(p_Cur OUT CurType);
    procedure pro_ovdey_meshek_shishi_shabat(P_STARTDATE IN DATE,
                                                                        P_ENDDATE IN DATE , 
                                                                        P_Ezor IN varchar2,
                                                                        p_cur OUT CurType)  ;                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
function get_heara_doch_nochechut(mispar_ishi in number,
                                                    taarich in date,
                                                    mispar_sidur in number,
                                                    lo_letashlum in number,
                                                    shat_hatchala in date,
                                                    shat_gmar in date,
                                                    kod_headrut in number,
                                                        cnt_sidurim_beyom number,
                                                        cnt_others_sidurim number,
                                                    is_headrut in number) return nvarchar2;
  procedure pro_get_mushalim_details(P_STARTDATE IN DATE,
                                                                      P_ENDDATE IN DATE , 
                                                                      P_COMPANYID IN NUMBER,
                                                                        p_cur OUT CurType); 
    procedure pro_cnt_ovdey_meshek_shabat(P_STARTDATE IN DATE,
                                                                        P_ENDDATE IN DATE , 
                                                                        p_cur OUT CurType)  ;    

PROCEDURE  pro_get_kod_snif_av( P_KOD_EZOR in nvarchar2,
                                p_Cur OUT CurType );  
 PROCEDURE  pro_get_kod_snif_av( p_Cur OUT CurType );
PROCEDURE  pro_get_updated_tickets( P_STARTDATE in date,
                                    P_ENDDATE in date,
                                    P_MIS_RASHEMET in varchar2,
                                    p_snif in varchar2,
                                    p_ezor in number,
                                    P_tezuga in number,
                                p_Cur OUT CurType ) ;
PROCEDURE  pro_get_updated_tickets_tomer( P_STARTDATE in date,
                                    P_ENDDATE in date,
                                    P_MIS_RASHEMET in varchar2,
                                    p_snif in varchar2,
                                    p_ezor in number,
                                    P_TEZUGA in number,
                                p_Cur OUT CurType ) ;                                
function  func_get_rash_actual_work_days( P_STARTDATE in date,
                                    P_ENDDATE in date,
                                    P_MIS_RASHEMET in varchar2,
                                    P_TEZUGA in number)   return nvarchar2;
--procedure pro_get_tigburim_details(P_STARTDATE IN DATE,
  --                                  P_ENDDATE IN DATE , 
 --                                   p_cur OUT CurType) ;            

procedure pro_get_pirtey_ovdey_kytanot(P_STARTDATE IN DATE,
                                                            P_ENDDATE IN DATE , 
                                                            p_cur OUT CurType) ;  
PROCEDURE  pro_get_Report_Details(p_kod_doch IN  CTB_SUGEY_DOCHOT.KOD_SUG_DOCH%TYPE, p_cur OUT CurType)  ;  
PROCEDURE pro_get_yechida ( p_cur OUT CurType);       
  procedure pro_rpt_sidur_vaad_ovdim(P_STARTDATE IN DATE,
                                                            P_ENDDATE IN DATE , 
                                                            p_cur OUT CurType) ;    
procedure pro_get_ovdim_peilim(P_STARTDATE IN DATE,
                                P_ENDDATE IN DATE , 
                                p_cur OUT CurType)  ;     
                                                            
procedure pro_get_ovdey_egged_metagberim(P_STARTDATE IN DATE, P_ENDDATE IN DATE ,  p_cur OUT CurType);  
procedure pro_get_rechev_egged_metagber(P_STARTDATE IN DATE, P_ENDDATE IN DATE ,  p_cur OUT CurType);  
 --procedure pro_get_tigburim(  p_cur OUT CurType) ;
 procedure pro_get_cnt_not_signed(p_cur OUT CurType , 
                                 p_Period IN VARCHAR2,
                                 p_ezor IN VARCHAR2,
                                 p_snif IN VARCHAR2,
                                 p_snif_tnua IN VARCHAR2,
                                 p_siba IN VARCHAR2,
                                 p_mispar_ishi IN VARCHAR2 );   
function fun_get_snif_tnua_leoved(p_snif_av in number,p_hazava_lekav in number, p_kod_hevra in number) return number;                                  
 procedure pro_get_ovdim_not_signed(p_cur OUT CurType , 
                                 p_Period IN VARCHAR2,
                                 p_ezor IN VARCHAR2,
                                 p_snif IN VARCHAR2,
                                 p_snif_tnua IN VARCHAR2,
                                 p_siba IN VARCHAR2,
                                 p_mispar_ishi IN VARCHAR2 );                                                                                                                                                                                                                                                                                               
END Pkg_Reports;
/

CREATE OR REPLACE PACKAGE          Pkg_Utils AS
/******************************************************************************
   NAME:       PKG_UTILS
   PURPOSE:

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        26/04/2009             1. Created this package.
******************************************************************************/

/*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        27/04/2009      sari       1. ????? ????? ????? ?????
*/
TYPE	CurType	  IS	REF  CURSOR;



PROCEDURE MoveRecordsToHistory;

PROCEDURE pro_get_ezorim(p_Cur OUT CurType);

PROCEDURE pro_get_snif_av(p_kod_ezor IN CTB_EZOR.kod_ezor%TYPE, p_cur OUT CurType);

PROCEDURE pro_get_profil(p_Cur OUT CurType) ;

PROCEDURE pro_get_harshaot_to_profil(p_kod_profil IN  TB_HARSHAOT_MASACHIM.KOD_PROFIL%TYPE, p_Cur OUT CurType);

PROCEDURE pro_get_maamad(p_kod_hevra IN CTB_MAAMAD.kod_hevra%TYPE, p_cur OUT CurType);

PROCEDURE pro_get_hodaot_to_profil(p_kod_masach  IN TB_HODAOT.MASACH_ID%TYPE ,
		  									   		   		                  p_kod_profil IN  TB_HARSHAOT_MASACHIM.KOD_PROFIL%TYPE ,
		  										  	  							   p_Cur OUT CurType) ;

PROCEDURE pro_get_error_ovdim(p_kod_snif IN CTB_SNIF_AV.kod_snif_av%TYPE, p_kod_maamad IN CTB_MAAMAD.kod_maamad_hr%TYPE,
                              p_from_date IN DATE, p_to_date IN DATE,
                              p_cur OUT CurType);

PROCEDURE pro_get_log_tahalich(p_Cur OUT CurType) ;

PROCEDURE pro_get_etz_nihuly_by_user(p_prefix IN VARCHAR2, p_mispar_ishi IN NUMBER, p_cur OUT CurType) ;
PROCEDURE pro_ins_Manage_Tree(p_mispar_ishi IN NUMBER );

PROCEDURE pro_get_etz_nihuly_by_name(p_prefix IN VARCHAR2, p_mispar_ishi IN NUMBER, p_cur OUT CurType);

PROCEDURE pro_get_meafyeney_bitua(p_Cur OUT CurType) ;

PROCEDURE pro_get_kod_natun(p_Cur OUT CurType) ;
PROCEDURE pro_get_parameters_table(p_cur OUT CurType);
PROCEDURE pro_get_ctb_elementim(p_cur OUT CurType);
PROCEDURE pro_get_sugey_yamim_meyuchadim(p_cur OUT CurType);
PROCEDURE pro_get_yamim_meyuchadim(p_cur OUT CurType);
PROCEDURE pro_get_sidurim_meyuch_rechiv(p_tar_me IN TB_SIDURIM_MEYUCHADIM_RECHIV.me_taarich%TYPE,
		  								 p_tar_ad IN TB_SIDURIM_MEYUCHADIM_RECHIV.me_taarich%TYPE,
		  									   	  											 p_cur OUT CurType);
PROCEDURE pro_get_sug_sidur_rechiv(p_tar_me IN TB_SIDURIM_MEYUCHADIM_RECHIV.me_taarich%TYPE,
		  								 p_tar_ad IN TB_SIDURIM_MEYUCHADIM_RECHIV.me_taarich%TYPE,
		  									   	  		 p_cur OUT CurType);
PROCEDURE pro_get_ctb_mutamut(p_cur OUT CurType);
PROCEDURE pro_get_sibot_ledivuch_yadani(p_cur OUT CurType);
PROCEDURE pro_get_status_ishur_max_level(p_mispar_ishi IN TB_ISHURIM.mispar_ishi%TYPE,
                                         p_taarich IN TB_ISHURIM.taarich%TYPE,
                                         p_kod_ishur IN TB_ISHURIM.kod_ishur%TYPE,
                                         p_kod_status OUT TB_ISHURIM.kod_status_ishur%TYPE);
FUNCTION  pro_check_ishur(p_mispar_ishi IN TB_ISHURIM.mispar_ishi%TYPE,
                                         p_taarich IN TB_ISHURIM.taarich%TYPE,
                                         p_kod_ishur IN TB_ISHURIM.kod_ishur%TYPE,
                                       p_mispar_sidur IN TB_ISHURIM.mispar_sidur%TYPE DEFAULT  NULL,
									    p_shat_hatchala IN TB_ISHURIM.Shat_Hatchala%TYPE DEFAULT  NULL) RETURN  NUMBER;

										PROCEDURE pro_get_premia_yadanit(p_mispar_ishi IN TB_PREMYOT_YADANIYOT.MISPAR_ISHI%TYPE, p_chodesh IN TB_PREMYOT_YADANIYOT.TAARICH%TYPE, p_sug_premya IN TB_PREMYOT_YADANIYOT.SUG_PREMYA%TYPE, p_Cur OUT CurType);

PROCEDURE pro_get_ovdim_for_premia(p_kod_premia IN MEAFYENIM_OVDIM.KOD_MEAFYEN%TYPE,p_taarich IN MEAFYENIM_OVDIM.ME_TAARICH%TYPE, p_cur OUT CurType);

PROCEDURE pro_get_ovdim_for_premiot(p_mispar_ishi IN VARCHAR2,p_kod_premia IN MEAFYENIM_OVDIM.KOD_MEAFYEN%TYPE,p_Period IN VARCHAR2, p_cur OUT CurType);

PROCEDURE pro_get_premyot_details(p_premya_codes VARCHAR2, p_cur OUT CurType);

PROCEDURE pro_get_premyot_view(p_mispar_ishi IN PREMYOT_VW.mispar_ishi%TYPE,
                                         p_tkufa IN PREMYOT_VW.tkufa%TYPE,
                                         p_cur OUT CurType);

PROCEDURE pro_get_zman_nesia(p_merkaz_erua IN CTB_ZMAN_NSIAA_MISHTANE.merkaz_erua%TYPE,
                             p_mikum_yaad  IN CTB_ZMAN_NSIAA_MISHTANE.mikum_yaad%TYPE,
                             p_taarich     IN CTB_ZMAN_NSIAA_MISHTANE.me_taarich%TYPE,
                             p_dakot OUT   CTB_ZMAN_NSIAA_MISHTANE.dakot%TYPE);

PROCEDURE Pro_Get_Value_From_Parametrim( p_Kod_Param IN  TB_PARAMETRIM.Kod_Param%TYPE,
                                                                               p_Period IN VARCHAR2 ,
                                                                              p_Erech_Param OUT  TB_PARAMETRIM.ERECH_PARAM%TYPE)       ;

PROCEDURE Pro_Get_Value_From_Parametrim (p_kod_param IN TB_PARAMETRIM.KOD_PARAM%TYPE,
                                            p_taarich IN DATE ,
                                            p_ERECH_PARAM    OUT INTEGER)  ;

PROCEDURE Pro_Get_Previous_Months_List(p_FromDate IN DATE, NumOfPreviousMonth NUMBER ,DisplayAll NUMBER,   p_cur OUT CurType);

  PROCEDURE  pro_get_ovdim_leRitza (p_mis_ritza IN INTEGER  ,
  			 					   				   			  	                p_maamad IN VARCHAR2,
																			    p_isuk IN VARCHAR2,
																				p_preFix IN VARCHAR2,
																				p_cur OUT CurType);

  FUNCTION fun_GET_Rechiv_Value(p_MisparIshi IN TB_CHISHUV_CHODESH_OVDIM.Mispar_ishi%TYPE,
                                                        p_Kod_Rechiv IN TB_CHISHUV_CHODESH_OVDIM.Kod_Rechiv%TYPE,
                                                        p_StartDate IN DATE,
                                                        p_EndDate IN DATE,
                                                        p_Bakasha_ID IN  TB_CHISHUV_CHODESH_OVDIM.Bakasha_ID%TYPE
                                                        ) RETURN NUMBER ;

	 PROCEDURE get_sadot_nosafim_lesidur(p_Sidur IN INTEGER,
 		  									   		   	  		 		        p_List_Meafyenim IN VARCHAR2,
																				p_cur OUT CurType) ;
	PROCEDURE get_sadot_nosafim_lePeilut(p_cur OUT CurType);
  PROCEDURE get_sadot_nosafim_kayamim(p_mispar_ishi IN  INTEGER,
														                                p_mispar_sidur IN  INTEGER,
																					    p_taarich IN TB_SIDURIM_OVDIM.TAARICH%TYPE,
																						p_shat_hatchala IN TB_SIDURIM_OVDIM.shat_hatchala%TYPE,
																						p_cur OUT CurType) ;
																						
	PROCEDURE pro_insert_barkod_Tachograf(p_mispar_ishi IN  INTEGER,p_taarich IN TB_TACHOGRAF_LE_KARTIS.TAARICH%TYPE,p_Barkod IN  NUMBER);		
    PROCEDURE fun_get_barkod_Tachograf(p_mispar_ishi IN  INTEGER,p_taarich IN TB_TACHOGRAF_LE_KARTIS.TAARICH%TYPE, p_cur OUT CurType  ) ;
	PROCEDURE pro_get_tavlaot_to_refresh(p_cur OUT CurType);	
	PROCEDURE pro_get_snif_tnua_by_kod(p_kod_snif IN NUMBER,p_cur OUT CurType);		
    PROCEDURE pro_insert_meadken_acharon(p_mispar_ishi IN tb_meadken_acharon.mispar_ishi%type,p_taarich tb_meadken_acharon.taarich%type, p_gorem_meadken in tb_meadken_acharon.GOREM_MEADKEN%type);	
      PROCEDURE pro_get_ovdim_by_bakasha(p_bakasha_id IN NUMBER,p_cur OUT CurType);
 FUNCTION fun_get_kod_tachanat_bizua(p_mispar_ishi IN  TB_PEILUT_OVDIM.mispar_ishi%TYPE,
                                     p_mispar_sidur IN TB_PEILUT_OVDIM.mispar_sidur%TYPE,
                                     p_taarich IN  TB_PEILUT_OVDIM.taarich%TYPE,
                                     p_shat_hatchala IN  TB_PEILUT_OVDIM.shat_hatchala_sidur%TYPE) RETURN number;
                                                
function fun_get_yechida(p_mispar_ishi in ovdim.mispar_ishi%type) return number;

function fun_get_manager_employees(p_prefix in varchar2,p_yechida in pivot_pirtey_ovdim.yechida_irgunit %type) return  tree_table pipelined;
function fun_get_isuk_harshaot(p_prefix in varchar2,p_mispar_ishi in pivot_pirtey_ovdim.mispar_ishi%type)  return  tree_table pipelined;      

--function fun_get_ez_nihuli_yechida(p_mispar_ishi in pivot_pirtey_ovdim.mispar_ishi%type) return  tb_harshaot_idkun.kod_yechida_nihuli%type;
function fun_get_manager_emp_by_maamad(p_prefix in varchar2,p_mispar_ishi in ovdim.mispar_ishi%type ) return  tree_table pipelined;
function fun_get_manager_emp_by_name(p_prefix in varchar2,p_mispar_ishi in pivot_pirtey_ovdim.mispar_ishi%type) return  tree_table pipelined;
function fun_get_isuk_harshaot_by_name(p_prefix in varchar2,p_mispar_ishi in pivot_pirtey_ovdim.mispar_ishi%type)  return  tree_table pipelined;
function fun_get_mngr_emp_by_mamad_name(p_prefix in varchar2,p_mispar_ishi in ovdim.mispar_ishi%type ) return  tree_table pipelined;
PROCEDURE pro_get_michsat_sidur_meafyen(p_cur OUT CurType, p_tarich_me DATE,p_tarich_ad DATE); 
function CheckShabatonOrShishi(p_mispar_ishi number,p_taarich date,p_meafyen56 number) return nvarchar2 ;
procedure pro_get_status_card(p_cur OUT CurType);
procedure pro_get_status_kartis_ctb(p_cur OUT CurType);
procedure pro_get_pundakim_tb(p_cur OUT CurType);
procedure pro_get_snif_av_ctb(p_cur OUT CurType);
 procedure pro_get_breaks_details(p_cur OUT CurType);
procedure pro_yechida_musach_machsan_ctb(p_cur OUT CurType);
procedure pro_get_michsa_yomit_tb(p_cur OUT CurType);
END Pkg_utils;
/
CREATE OR REPLACE PACKAGE BODY          Pkg_Attendance AS
/******************************************************************************
   NAME:       Pkg_Attendance
   PURPOSE:

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        05/07/2015             1. Created this package body.
******************************************************************************/


PROCEDURE pro_new_recHarmony(   pMISPAR_ISHI varchar, pTAARICH varchar, pShaa varchar, pMispar_shaon varchar,pMISPAR_ISHI_chk varchar, 
                            pSTATUS_data varchar, pstatus_ans varchar, pclock_inner_num varchar,  psite_kod varchar, pclock_num_in_site varchar,                            
                            pclock_name varchar, prec_time_stmp varchar, paction_kod varchar, ptbl_num varchar ) IS
     shaa date;  
   shaas date;
     v_mispar_siduri number;  
     code varchar(3);
     sqlm varchar(150);  
     p_shaon number;             
           BEGIN
          
       Begin                     
          --psite_kod=180 ,pclock_num_in_site=01
    shaa:= to_date(pTAARICH || ' ' || substr( lpad(pShaa,4,'0'),0,2) ||':'|| substr( lpad(pShaa,4,'0'),3,2)|| ':00','dd/mm/yyyy HH24:mi:ss');  
    code:= Pkg_Attendance.fun_get_number_code(paction_kod,ptbl_num);
    shaas:=sysdate;
    
 
  select  code into p_shaon
    from tb_clocks
    where code_shaon=to_number(psite_kod || lpad(pclock_num_in_site,2,'0'));
   
    
    INSERT INTO  MOVMENT@DG4HARMONY("Emp_no","Date","Time","Code","Rec_Enter","Badge","ClockID","From_dll","Dup_rec" ,"Chang_rec","Good_rec", "L_Present", "Hand","ZIgnore","export_p")
                VALUES(pMISPAR_ISHI,  to_date(pTAARICH,'dd/mm/yyyy')  ,shaa,code,shaas,'0000'||pMISPAR_ISHI ||'0000',p_shaon,0,0,0,0,0,0,0,0); 
                  commit;  
 Exception
   WHEN OTHERS THEN
   sqlm:=SQLERRM;
  --sqlm:='';
    rollback;
        SELECT log_seq.NEXTVAL INTO v_mispar_siduri FROM dual;  
        INSERT INTO TB_LOG_BAKASHOT(MISPAR_SIDURI,BAKASHA_ID,TAARICH_IDKUN_ACHARON,SUG_HODAA,KOD_TAHALICH,KOD_YESHUT,
         MISPAR_ISHI,TAARICH,MISPAR_SIDUR,SHAT_HATCHALA_SIDUR,SHAT_YETZIA,MISPAR_KNISA,KOD_HODAA,TEUR_HODAA)
   VALUES (v_mispar_siduri,66,sysdate,'E',0,0,pMISPAR_ISHI,  to_date(pTAARICH,'dd/mm/yyyy'),null,null,
           shaa,null,null,'harmony err' || sqlm);
         
        commit;  
 End ;
              
   INSERT INTO TB_attendance_Harmony ( MISPAR_ISHI ,  TAARICH   ,  Shaa , Mispar_shaon,  MISPAR_ISHI_chk ,
   STATUS_data   , Status_ans  , clock_inner_num , site_kod , clock_num_in_site  ,
 clock_name , rec_time_stmp  , action_kod , tbl_num ,  MEADKEN_ACHARON  )
             VALUES  ( pMISPAR_ISHI  , to_date(pTAARICH,'ddmmyyyy')  , pShaa  , pMispar_shaon  ,pMISPAR_ISHI_chk  ,
                       pSTATUS_data , pstatus_ans  , pclock_inner_num  , psite_kod  ,  pclock_num_in_site  ,                        
                       pclock_name  , prec_time_stmp  ,   paction_kod  , ptbl_num  ,  -11);
    COMMIT;
        
  Exception
   WHEN OTHERS THEN
   BEGIN
        ROLLBACK;
        RAISE;
    END;
  END pro_new_recHarmony; 
  
  function fun_get_number_code(ACTION_KOD in VARCHAR2,TBL_NUM  in VARCHAR2 ) return number is
  begin
       if(ACTION_KOD ='A') then
            return '100';
       else if (ACTION_KOD ='B') then
                return '200';
         else if ((ACTION_KOD ='C' and TBL_NUM='440') or(ACTION_KOD ='D' and TBL_NUM='440') or (ACTION_KOD ='E' and TBL_NUM='440') ) then
                return '440';
               else if ((ACTION_KOD ='D' and TBL_NUM='460') or(ACTION_KOD ='E' and TBL_NUM='460') or (ACTION_KOD ='F' and TBL_NUM='460') ) then
                        return '460';
                     else if (ACTION_KOD ='G' or (ACTION_KOD ='D' and TBL_NUM<>'460'  and TBL_NUM<>'440') ) then
                            return '700';
                            else  return '0';
                          end if;
                   end if;
                end if;
            end if;
       end if;
  end fun_get_number_code;
  
  PROCEDURE pro_new_recHarmony_err(   pErr_kod in number,    pErr_filename  in varchar,  pErr_pref in varchar ,pErr_line in varchar , pErr_suff in varchar , pLineErrCnt in number ) IS
           BEGIN
           INSERT INTO TB_attendance_Harmony_err ( Err_kod ,  TAARICH_err , Err_filename  ,  Err_pref  ,Err_line  , Err_suff  , LineErrCnt   )
             VALUES  ( pErr_kod ,  sysdate , pErr_filename  ,  pErr_pref  ,pErr_line  , pErr_suff , pLineErrCnt  );
             COMMIT;
                            Exception
   WHEN OTHERS THEN
   BEGIN
   ROLLBACK;
        RAISE;
        END;
  END pro_new_recHarmony_err; 
        
PROCEDURE Pro_diffHarmony (p_Cur OUT CurType) AS
  BEGIN
   OPEN p_Cur FOR
  select o.mispar_ishi,o.taarich,substr(lpad(o.mikum_shaon_knisa,5,0),1,3),o.shat_hatchala,'in'
from tb_sidurim_ovdim o 
where o.mispar_sidur=99001
 and substr(lpad(o.mikum_shaon_knisa,5,0),1,3) in (106,224)
 and o.mikum_shaon_knisa+o.mikum_shaon_yetzia>0
 and o.taarich >   to_date('16/06/2015','dd/mm/yyyy') 
and not exists (select * from tb_attendance_harmony h
where h.mispar_ishi =o.mispar_ishi
 and h.taarich >   to_date('16/06/2015','dd/mm/yyyy') 
and o.mispar_ishi=h.mispar_ishi
and o.taarich=h.taarich
and o.shat_hatchala=h.taarich+ (substr(lpad(h.shaa,4,0),1,2))/24 + (substr(lpad(h.shaa,4,0),3,2))/(24*60)  
and h.action_kod='A' 
and substr(lpad(o.mikum_shaon_knisa,5,0),1,3)=site_kod)
union all
select o.mispar_ishi,o.taarich,substr(lpad(o.mikum_shaon_yetzia,5,0),1,3),o.shat_gmar,'out'
from tb_sidurim_ovdim o 
where o.mispar_sidur=99001
 and substr(lpad(o.mikum_shaon_yetzia,5,0),1,3) in (106,224)
 and o.mikum_shaon_knisa+o.mikum_shaon_yetzia>0
 and o.taarich >   to_date('16/06/2015','dd/mm/yyyy')  
and not exists (select * from tb_attendance_harmony h
where h.mispar_ishi =o.mispar_ishi
 and h.taarich >   to_date('16/06/2015','dd/mm/yyyy')  
and o.mispar_ishi=h.mispar_ishi
and o.taarich=h.taarich
and o.shat_gmar=h.taarich+ (substr(lpad(h.shaa,4,0),1,2))/24 + (substr(lpad(h.shaa,4,0),3,2))/(24*60)  
and h.action_kod='B' 
and substr(lpad(o.mikum_shaon_yetzia,5,0),1,3)=site_kod) 
union all
   select o.mispar_ishi,o.taarich,substr(lpad(o.mikum_shaon_knisa,5,0),1,3),o.shat_hatchala,'inG'
from tb_sidurim_ovdim o 
where o.mispar_sidur=99220
 and substr(lpad(o.mikum_shaon_knisa,5,0),1,3) in (106,224)
 and o.mikum_shaon_knisa+o.mikum_shaon_yetzia>0
 and o.taarich >   to_date('16/06/2015','dd/mm/yyyy')  
and not exists (select * from tb_attendance_harmony h
where h.mispar_ishi =o.mispar_ishi
 and h.taarich >   to_date('16/06/2015','dd/mm/yyyy')  
and o.mispar_ishi=h.mispar_ishi
and o.taarich=h.taarich
and o.shat_hatchala=h.taarich+ (substr(lpad(h.shaa,4,0),1,2))/24 + (substr(lpad(h.shaa,4,0),3,2))/(24*60)  
and h.action_kod IN ('D','C','E')
and H.TBL_NUM='440' 
and substr(lpad(o.mikum_shaon_knisa,5,0),1,3)=site_kod)
union all
select o.mispar_ishi,o.taarich,substr(lpad(o.mikum_shaon_yetzia,5,0),1,3),o.shat_gmar,'outG'
from tb_sidurim_ovdim o 
where o.mispar_sidur=99220
 and substr(lpad(o.mikum_shaon_yetzia,5,0),1,3) in (106,224)
 and o.mikum_shaon_knisa+o.mikum_shaon_yetzia>0
 and o.taarich >   to_date('16/06/2015','dd/mm/yyyy')  
and not exists (select * from tb_attendance_harmony h
where h.mispar_ishi =o.mispar_ishi
 and h.taarich >   to_date('16/06/2015','dd/mm/yyyy')  
and o.mispar_ishi=h.mispar_ishi
and o.taarich=h.taarich
and o.shat_gmar=h.taarich+ (substr(lpad(h.shaa,4,0),1,2))/24 + (substr(lpad(h.shaa,4,0),3,2))/(24*60)  
and h.action_kod IN ('D','F','E')
and H.TBL_NUM='460' 
and substr(lpad(o.mikum_shaon_yetzia,5,0),1,3)=site_kod) 
-- shayah leyom kodem
and not exists (select * from tb_attendance_harmony h
where h.mispar_ishi =o.mispar_ishi
 and h.taarich >   to_date('16/06/2015','dd/mm/yyyy')  
and o.mispar_ishi=h.mispar_ishi
and o.taarich=h.taarich-1
and o.shat_gmar=h.taarich+ (substr(lpad(h.shaa,4,0),1,2))/24 + (substr(lpad(h.shaa,4,0),3,2))/(24*60)  
and h.action_kod IN ('D','F','E')
and H.TBL_NUM='460' 
and substr(lpad(o.mikum_shaon_yetzia,5,0),1,3)=site_kod) 
union all
-- drive is tested against knisa only since it has no yetzia
select o.mispar_ishi,o.taarich,substr(lpad(o.mikum_shaon_knisa,5,0),1,3),o.shat_hatchala,'drive'
from tb_sidurim_ovdim o 
where o.mispar_sidur=99200
 and substr(lpad(o.mikum_shaon_knisa,5,0),1,3) in (106,224)
-- and o.mikum_shaon_knisa+o.mikum_shaon_yetzia>0
 and o.taarich >   to_date('16/06/2015','dd/mm/yyyy')  
and not exists (select * from tb_attendance_harmony h
where h.mispar_ishi =o.mispar_ishi
 and h.taarich >   to_date('16/06/2015','dd/mm/yyyy')  
and o.taarich=h.taarich
and o.shat_hatchala=h.taarich+ (substr(lpad(h.shaa,4,0),1,2))/24 + (substr(lpad(h.shaa,4,0),3,2))/(24*60)  
and ((h.action_kod ='D' and H.TBL_NUM<>'460' and H.TBL_NUM<>'440' )    or h.action_kod='G')
and substr(lpad(o.mikum_shaon_knisa,5,0),1,3)=site_kod)
union all
select o.mispar_ishi,o.taarich,substr(lpad(o.mikum_shaon_knisa,5,0),1,3),o.shat_hatchala,'drive'
from tb_sidurim_ovdim o 
where o.mispar_sidur=99200
 and substr(lpad(o.mikum_shaon_knisa,5,0),1,3) =149
 and o.taarich >   to_date('16/06/2015','dd/mm/yyyy')  
and not exists (select * from tb_attendance_harmony h
where h.mispar_ishi =o.mispar_ishi
 and h.taarich >   to_date('16/06/2015','dd/mm/yyyy')  
and o.taarich=h.taarich
and o.shat_hatchala=h.taarich+ (substr(lpad(h.shaa,4,0),1,2))/24 + (substr(lpad(h.shaa,4,0),3,2))/(24*60)  
and ((h.action_kod ='D' and H.TBL_NUM<>'460' and H.TBL_NUM<>'440' )    or h.action_kod='G')
and substr(lpad(o.mikum_shaon_knisa,5,0),1,3)=site_kod)
union all
  select h.mispar_ishi,h.taarich,to_char(h.site_kod),
  h.taarich+ (substr(lpad(h.shaa,4,0),1,2))/24 + (substr(lpad(h.shaa,4,0),3,2))/(24*60) hatchala,'Attin'
from tb_attendance_harmony h   
where   h.action_kod='A'  
 and h.taarich >   to_date('16/06/2015','dd/mm/yyyy') 
and not exists (select * from  tb_sidurim_ovdim o
where h.mispar_ishi =o.mispar_ishi
and o.mispar_ishi=h.mispar_ishi
and o.taarich=h.taarich
and o.shat_hatchala=h.taarich+ (substr(lpad(h.shaa,4,0),1,2))/24 + (substr(lpad(h.shaa,4,0),3,2))/(24*60)  
and o.mispar_sidur=99001
  and substr(lpad(o.mikum_shaon_knisa,5,0),1,3) in (106,224)
-- and o.mikum_shaon_knisa+o.mikum_shaon_yetzia>0
 and o.taarich >   to_date('16/06/2015','dd/mm/yyyy') 
and substr(lpad(o.mikum_shaon_knisa,5,0),1,3)=site_kod)
union all
 select     h.mispar_ishi,h.taarich,to_char(h.site_kod),
  h.taarich+ (substr(lpad(h.shaa,4,0),1,2))/24 + (substr(lpad(h.shaa,4,0),3,2))/(24*60) gmar,'Attout'
from tb_attendance_harmony h
where   h.taarich >   to_date('16/06/2015','dd/mm/yyyy')  
and h.action_kod='B' 
and not exists (select * from  tb_sidurim_ovdim o
where o.mispar_sidur=99001
 and substr(lpad(o.mikum_shaon_yetzia,5,0),1,3) in (106,224)
 and o.mikum_shaon_knisa+o.mikum_shaon_yetzia>0
 and o.taarich >   to_date('16/06/2015','dd/mm/yyyy')  
 and h.mispar_ishi =o.mispar_ishi
and o.taarich=h.taarich
and o.shat_gmar=h.taarich+ (substr(lpad(h.shaa,4,0),1,2))/24 + (substr(lpad(h.shaa,4,0),3,2))/(24*60)  
and substr(lpad(o.mikum_shaon_yetzia,5,0),1,3)=site_kod) 
union all
  select h.mispar_ishi,h.taarich,to_char(h.site_kod),
  h.taarich+ (substr(lpad(h.shaa,4,0),1,2))/24 + (substr(lpad(h.shaa,4,0),3,2))/(24*60) hatchala,'AttinG'
from tb_attendance_harmony h  
where  h.taarich >   to_date('16/06/2015','dd/mm/yyyy') 
and   h.action_kod IN ('D','C','E')
and H.TBL_NUM='440'   
and not exists (select * from  tb_sidurim_ovdim o 
where o.mispar_sidur=99220
 and substr(lpad(o.mikum_shaon_knisa,5,0),1,3) in (106,224)
 and o.taarich >   to_date('16/06/2015','dd/mm/yyyy')
 and h.mispar_ishi =o.mispar_ishi  
and o.taarich=h.taarich
and o.shat_hatchala=h.taarich+ (substr(lpad(h.shaa,4,0),1,2))/24 + (substr(lpad(h.shaa,4,0),3,2))/(24*60)   
and substr(lpad(o.mikum_shaon_knisa,5,0),1,3)=site_kod)
union all
select     h.mispar_ishi,h.taarich,to_char(h.site_kod),
  h.taarich+ (substr(lpad(h.shaa,4,0),1,2))/24 + (substr(lpad(h.shaa,4,0),3,2))/(24*60) gmar,'AttoutG'
from tb_attendance_harmony h
where   h.taarich >   to_date('16/06/2015','dd/mm/yyyy') 
and h.action_kod IN ('D','F','E')
and H.TBL_NUM='460'  
and not exists (select * from  tb_sidurim_ovdim o 
where   o.mispar_sidur=99220
 and substr(lpad(o.mikum_shaon_yetzia,5,0),1,3) in (106,224)
 and o.taarich >   to_date('16/06/2015','dd/mm/yyyy')  
and h.mispar_ishi =o.mispar_ishi
and o.taarich=h.taarich
and o.shat_gmar=h.taarich+ (substr(lpad(h.shaa,4,0),1,2))/24 + (substr(lpad(h.shaa,4,0),3,2))/(24*60)  
and substr(lpad(o.mikum_shaon_yetzia,5,0),1,3)=site_kod) 
-- shayah leyom kodem
and not exists (select * from tb_sidurim_ovdim o 
where h.mispar_ishi =o.mispar_ishi
and o.taarich=h.taarich-1
and o.shat_gmar=h.taarich+ (substr(lpad(h.shaa,4,0),1,2))/24 + (substr(lpad(h.shaa,4,0),3,2))/(24*60)  
and substr(lpad(o.mikum_shaon_yetzia,5,0),1,3)=site_kod) 
union all
select     h.mispar_ishi,h.taarich,to_char(h.site_kod),
  h.taarich+ (substr(lpad(h.shaa,4,0),1,2))/24 + (substr(lpad(h.shaa,4,0),3,2))/(24*60) gmar,'Attdrive'
from tb_attendance_harmony h
 where    h.taarich >   to_date('16/06/2015','dd/mm/yyyy')  
  and h.site_kod<>149
and ((h.action_kod ='D' and H.TBL_NUM<>'460' and H.TBL_NUM<>'440' )    or h.action_kod='G')
and not exists (select *  from tb_sidurim_ovdim o 
where h.mispar_ishi =o.mispar_ishi
and o.taarich=h.taarich
and o.shat_hatchala=h.taarich+ (substr(lpad(h.shaa,4,0),1,2))/24 + (substr(lpad(h.shaa,4,0),3,2))/(24*60)  
and o.mispar_sidur=99200
 and substr(lpad(o.mikum_shaon_knisa,5,0),1,3) in (106,224)
 and o.taarich >   to_date('16/06/2015','dd/mm/yyyy')  
and substr(lpad(o.mikum_shaon_knisa,5,0),1,3)=site_kod)
union all
select     h.mispar_ishi,h.taarich,to_char(h.site_kod),
  h.taarich+ (substr(lpad(h.shaa,4,0),1,2))/24 + (substr(lpad(h.shaa,4,0),3,2))/(24*60) gmar,'Attdrive'
from tb_attendance_harmony h
 where    h.taarich >   to_date('16/06/2015','dd/mm/yyyy')  
  and h.site_kod=149
and ((h.action_kod ='D' and H.TBL_NUM<>'460' and H.TBL_NUM<>'440' )    or h.action_kod='G')
and not exists (select *  from tb_sidurim_ovdim o 
where h.mispar_ishi =o.mispar_ishi
and o.taarich=h.taarich
and o.shat_hatchala=h.taarich+ (substr(lpad(h.shaa,4,0),1,2))/24 + (substr(lpad(h.shaa,4,0),3,2))/(24*60)  
and o.mispar_sidur=99200
 and substr(lpad(o.mikum_shaon_knisa,5,0),1,3)=149
 and o.taarich >   to_date('16/06/2015','dd/mm/yyyy')  
and substr(lpad(o.mikum_shaon_knisa,5,0),1,3)=site_kod);

     EXCEPTION
         WHEN OTHERS THEN
              RAISE;

end Pro_diffHarmony;

PROCEDURE Ins_TahalichHarmony(   pfilename  in varchar  ) IS
           BEGIN   
           INSERT INTO TB_LOG_TAHALICH
    VALUES (2,4,0,SYSDATE,'','','','Harmony '||pfilename);
  EXCEPTION
         WHEN OTHERS THEN
              RAISE;

end Ins_TahalichHarmony;
 
PROCEDURE pro_ins_control_clock( p_taarich in date,  p_status  in number,  p_teur in varchar ) IS
           BEGIN
 insert into TB_CONTROL_CLOCK( START_DT , STATUS  , TAARICH_IDKUN_ACHARON , TEUR_TECH )
        values (p_taarich, p_status,sysdate,p_teur );
commit;
     EXCEPTION
         WHEN OTHERS THEN
              RAISE;

end pro_ins_control_clock;

PROCEDURE pro_upd_control_clock( p_taarich in date,  p_status  in number,  p_teur in varchar )  IS
           BEGIN
           update TB_CONTROL_CLOCK
           set teur_tech=p_teur,
               status=p_status
            where start_dt=p_taarich;
commit;
     EXCEPTION
         WHEN OTHERS THEN
              RAISE;
end pro_upd_control_clock;       

function pro_get_last_hour_clock return varchar is
p_hour varchar(50);
begin
    
      select  to_char(  max(START_DT),'dd/mm/yyyy HH24:mi:ss' )  into p_hour
      from TB_CONTROL_CLOCK
      where status=2;   
 
   return p_hour;
   EXCEPTION
    WHEN no_data_found THEN
        return to_char(sysdate);
    WHEN OTHERS THEN
      raise;

end pro_get_last_hour_clock;
PROCEDURE pro_ins_shaonim_movment(p_bakasha number,p_coll_Harmony_movment IN coll_Harmony_movment_Err_Mov ) IS
  v_mispar_siduri number;  
     sqlm varchar(150);   
BEGIN
NULL;
  /*
 IF (p_coll_Harmony_movment IS NOT NULL) THEN
        FOR i IN 1..p_coll_Harmony_movment.COUNT LOOP
        
            begin
                  insert into TB_HARMONY_MOVMENT(EMP_NO,TAARICH, HOUR,CODE,OPER,STATION,STATION_W ,MACHINE,MACHINE_W, ACTION,XLEVEL,ALTERNATIVE,GOOD,BED,
                                                         EXCEP,EXECUTED,MARKED,FROM_DLL ,DUP_REC ,CHANG_REC,GOOD_REC, L_PRESENT, 
                                                         BADGE,AUTHORIZED,PARAM1,PARAM2,PARAM3,PARAM4,PARAM5,PARAM6,LANCH,  HAND ,ZIGNORE ,CLOCKID,EXPORT_P,
                                                         MEAL_SUPP,MEAL_QUANT,REC_ENTER,   ABSCODE,ABSTIME,CAR_BADGE,CAR_NO, BADGE_TYPE,
                                                         DATETRANSF,CODE_ERR,DESCR,ERR_MOVID,meadken_acharon,MAKOR)
                  
                  values(
                        
                            p_coll_Harmony_movment(i).EMP_NO ,
                            p_coll_Harmony_movment(i).TAARICH ,
                            p_coll_Harmony_movment(i).HOUR ,
                            p_coll_Harmony_movment(i).CODE ,
                            p_coll_Harmony_movment(i).OPER ,
                            p_coll_Harmony_movment(i).STATION,
                            p_coll_Harmony_movment(i).STATION_W ,
                            p_coll_Harmony_movment(i).MACHINE ,
                            p_coll_Harmony_movment(i).MACHINE_W,
                            p_coll_Harmony_movment(i).ACTION,
                            p_coll_Harmony_movment(i).XLEVEL,
                            p_coll_Harmony_movment(i).ALTERNATIVE,
                            p_coll_Harmony_movment(i).GOOD,
                            p_coll_Harmony_movment(i).BED ,
                            p_coll_Harmony_movment(i).EXCEP,
                            p_coll_Harmony_movment(i).EXECUTED ,
                            p_coll_Harmony_movment(i).MARKED ,
                            p_coll_Harmony_movment(i).FROM_DLL ,
                            p_coll_Harmony_movment(i).DUP_REC ,
                            p_coll_Harmony_movment(i).CHANG_REC,
                            p_coll_Harmony_movment(i).GOOD_REC,
                            p_coll_Harmony_movment(i).L_PRESENT,
                            p_coll_Harmony_movment(i).BADGE ,
                            p_coll_Harmony_movment(i).AUTHORIZED ,
                            p_coll_Harmony_movment(i).PARAM1 ,
                            p_coll_Harmony_movment(i).PARAM2,
                            p_coll_Harmony_movment(i).PARAM3,
                            p_coll_Harmony_movment(i).PARAM4,
                            p_coll_Harmony_movment(i).PARAM5,
                            p_coll_Harmony_movment(i).PARAM6 ,
                            p_coll_Harmony_movment(i).LANCH ,
                            p_coll_Harmony_movment(i).HAND ,
                            p_coll_Harmony_movment(i).ZIGNORE,
                            p_coll_Harmony_movment(i).CLOCKID ,  
                            p_coll_Harmony_movment(i).EXPORT_P  ,            
                            p_coll_Harmony_movment(i).MEAL_SUPP ,
                            p_coll_Harmony_movment(i).MEAL_QUANT ,
                            p_coll_Harmony_movment(i).REC_ENTER,
                            p_coll_Harmony_movment(i).ABSCODE ,
                            p_coll_Harmony_movment(i).ABSTIME ,
                            p_coll_Harmony_movment(i).CAR_BADGE ,
                            p_coll_Harmony_movment(i).CAR_NO ,
                            p_coll_Harmony_movment(i).BADGE_TYPE ,
                            p_coll_Harmony_movment(i).DATETRANSF,
                            p_coll_Harmony_movment(i).CODE_ERR,
                            p_coll_Harmony_movment(i).DESCR,
                            p_coll_Harmony_movment(i).ERR_MOVID,
                            -14,
                            p_coll_Harmony_movment(i).MAKOR
                  );    
            commit;
            
            Exception
               WHEN OTHERS THEN
               sqlm:=SQLERRM;
                rollback;
                    SELECT log_seq.NEXTVAL INTO v_mispar_siduri FROM dual;  
                    INSERT INTO TB_LOG_BAKASHOT(MISPAR_SIDURI,BAKASHA_ID,TAARICH_IDKUN_ACHARON,SUG_HODAA,KOD_TAHALICH,KOD_YESHUT,
                     MISPAR_ISHI,TAARICH,MISPAR_SIDUR,SHAT_HATCHALA_SIDUR,SHAT_YETZIA,MISPAR_KNISA,KOD_HODAA,TEUR_HODAA)
               VALUES (v_mispar_siduri,p_bakasha,sysdate,'E',0,0,p_coll_Harmony_movment(i).EMP_NO,   p_coll_Harmony_movment(i).TAARICH ,null,null,
                        p_coll_Harmony_movment(i).HOUR ,null,null,'insert harmony err: ' || sqlm);
                     
                    commit;  
           end;
           
        END LOOP;
    END IF;
*/
         
       
EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END  pro_ins_shaonim_movment;    
  


function pro_get_last_attend_cntl return number is
 p_status number;
BEGIN
        
 SELECT status into p_status
 FROM (SELECT * FROM TB_control_Attend order by TAARICH_IDKUN_ACHARON desc) 
 WHERE ROWNUM=1  ;
     
     return p_status;
commit;
     EXCEPTION
         WHEN OTHERS THEN
              RAISE;

end pro_get_last_attend_cntl;

procedure pro_Insert_Tnuot_Shaon(p_bakasha in number) as
 taarich date;
 p_count number;
 v_mispar_siduri number;
begin
        select max(insert_date) into taarich from TB_HARMONY_MOVMENT;
        IF (TAARICH IS NULL) THEN
            select MIN(insert_date) into taarich from MOVEMENT;
        END IF; 
        
        SELECT log_seq.NEXTVAL INTO v_mispar_siduri FROM dual;  
        INSERT INTO TB_LOG_BAKASHOT(MISPAR_SIDURI,BAKASHA_ID,TAARICH_IDKUN_ACHARON,SUG_HODAA,TEUR_HODAA)
              VALUES (v_mispar_siduri,p_bakasha,sysdate,'I','max_taarich tb_harmony_movment: ' || to_char(taarich,'dd/mm/yyyy HH24:mi:ss') );
               
        select count(*) into p_count
        from MOVEMENT H
        where H.insert_date >  taarich and  H.insert_date<=sysdate;  --(sysdate -  1/1440); 
        
        SELECT log_seq.NEXTVAL INTO v_mispar_siduri FROM dual;  
        INSERT INTO TB_LOG_BAKASHOT(MISPAR_SIDURI,BAKASHA_ID,TAARICH_IDKUN_ACHARON,SUG_HODAA,TEUR_HODAA)
              VALUES (v_mispar_siduri,p_bakasha,sysdate,'I','Num rows to insert tb_harmony_movment: ' ||p_count );
           
       insert into TB_HARMONY_MOVMENT
        select H.*,-14,SYSDATE
        from MOVEMENT H
        where H.insert_date > taarich and  H.insert_date<=sysdate; 
end pro_Insert_Tnuot_Shaon;
PROCEDURE  pro_fetch_AttendHarmony (p_Cur OUT CurType) AS
  BEGIN
   OPEN p_Cur FOR  
       select  h.Emp_no MISPAR_ISHI,h.HR_DATE TAARICH,h.HR_TIME Shaa,h.Code,
             nvl(T.MISPAR_SIDUR,0) MISPAR_SIDUR, T.SUG_PEULA, P.ISUK,
            case when T.MISPAR_SIDUR=99200 and  T.SUG_PEULA =1  then 1
                 when T.MISPAR_SIDUR=99001 and  T.SUG_PEULA =1  then 2 
                 when T.MISPAR_SIDUR=99001 and  T.SUG_PEULA =2  then 3 
                 when T.MISPAR_SIDUR=99220 and  T.SUG_PEULA =1  then 4 
                 when T.MISPAR_SIDUR=99220 and  T.SUG_PEULA =2  then 5 
                 when T.MISPAR_SIDUR=99214 and  T.SUG_PEULA =1  then 6 
                 else 0 end SugRec,  ca.ControllerGrp code_shaon,--c.code_shaon,
                 pkg_ovdim.fun_get_meafyen_oved( h.Emp_no,43,h.HR_DATE ) meafyen43 
        from TB_HARMONY_MOVMENT h ,CTB_TNUAT_SHAON_SIDUR t,pivot_pirtey_ovdim p, TLA_CONTROLLERS ca --tb_clocks c
        where
              h.Emp_no=P.MISPAR_ISHI
          and h.HR_DATE between P.ME_TARICH and P.AD_TARICH
      and h.insert_date>=(select nvl(max(start_dt),sysdate ) from TB_control_Attend where STATUS=2)
     --   and h.HR_DATE = to_date('12/01/2016','dd/mm/yyyy')
          and  h.Code=T.Code(+)
      --    and  h.Emp_no=48732 
          and h.clockid= ca.Code(+) --c.code(+)
          --and  T.MISPAR_SIDUR<>99200
         Order by h.Emp_no,h.HR_DATE ,h.HR_TIME;
 
  /* select  h.EMP_NO  MISPAR_ISHI ,  h.TAARICH   , h.hour Shaa , h.code     
 from TB_HARMONY_MOVMENT h
where  rec_enter>(select nvl(max(start_dt),sysdate ) from TB_control_Attend where STATUS=2);*/
/***select   MISPAR_ISHI ,   to_char(TAARICH,'dd/mm/yyyy') TAARICH  ,  Shaa , Mispar_shaon,  MISPAR_ISHI_chk ,moed,
   STATUS_data   , Status_ans  , clock_inner_num , site_kod , clock_num_in_site  ,
 clock_name , rec_time_stmp  , action_kod , tbl_num ,  taarich_spare,spare1,spare2,
 MEADKEN_ACHARON ,taarich_klita
 from TB_attendance_Harmony 
where   MISPAR_ISHI=74798
 and taarich BETWEEN to_date('24/09/2015','dd/mm/yyyy') AND
to_date('25/09/2015','dd/mm/yyyy')
-- AND TBL_NUM>400
--taarich_klita>(select nvl(max(start_dt),sysdate ) from TB_control_Attend where STATUS=2)
 ORDER BY taarich_klita; -- MISPAR_ISHI, TAARICH,TO_NUMBER(NVL(Shaa,0));***/
 -- AND TBL_NUM=200;

---- taarich_klita>(select nvl(max(start_dt),sysdate ) from TB_control_Attend where STATUS=2);



/*select  "Emp_no" MISPAR_ISHI,"Date" TAARICH,"Time" Shaa,"Code",
        T.MISPAR_SIDUR, T.SUG_PEULA,
        case when T.MISPAR_SIDUR=99200 and  T.SUG_PEULA =1  then 1
             when T.MISPAR_SIDUR=99001 and  T.SUG_PEULA =1  then 2 
             when T.MISPAR_SIDUR=99001 and  T.SUG_PEULA =2  then 3 
             when T.MISPAR_SIDUR=99220 and  T.SUG_PEULA =1  then 4 
             when T.MISPAR_SIDUR=99220 and  T.SUG_PEULA =2  then 5  end SugRec
from MOVMENT@DG4HARMONY ,CTB_TNUAT_SHAON_SIDUR t
where "Date">to_date('06/09/2015','dd/mm/yyyy')
 and "Code"=T.KOD*/
     EXCEPTION
         WHEN OTHERS THEN
              RAISE;

end pro_fetch_AttendHarmony;

PROCEDURE pro_new_control_Attend( pTAARICH in DATE,  pSTATUS  in number,  pteur in varchar ) IS
           BEGIN
 insert into TB_control_Attend
( START_DT , STATUS  , TAARICH_IDKUN_ACHARON , TEUR_TECH )
values (pTAARICH, pSTATUS,sysdate,pteur );

     EXCEPTION
         WHEN OTHERS THEN
              RAISE;

end pro_new_control_Attend;

PROCEDURE pro_Upd_control_Attend( pTAARICH in DATE,  pSTATUS  in number,  pteur in varchar ) IS
           BEGIN
           update TB_control_Attend
set teur_tech=pteur,
    status=pSTATUS,
    TAARICH_IDKUN_ACHARON=SYSDATE
where start_dt=pTAARICH;--to_date(pTAARICH,'yyyymmddhh24miss');

     EXCEPTION
         WHEN OTHERS THEN
              RAISE;

end pro_Upd_control_Attend;
 


PROCEDURE  pro_check_Yetzia (pMISPAR_ISHI in NUMBER, pTAARICH in DATE, pShaa in varchar, pmispar_sidur in NUMBER,p24  in NUMBER, p_Cur OUT CurType) AS
  BEGIN
   OPEN p_Cur FOR
select  count(*) 
 from TB_Sidurim_ovdim o  --TB_Sidurim_attend
where  o.mispar_ishi=pMISPAR_ISHI 
and o.taarich=pTAARICH -p24 --to_date(pTAARICH,'dd/mm/yyyy') - p24 
and o.shat_gmar=pTAARICH + (substr(lpad(pShaa,4,0),1,2))/24 + (substr(lpad(pShaa,4,0),3,2))/(24*60)   --to_date(pTAARICH,'dd/mm/yyyy')+ (substr(lpad(pShaa,4,0),1,2))/24 + (substr(lpad(pShaa,4,0),3,2))/(24*60)   
and o.mispar_sidur=pmispar_sidur
--p24: shayah leyom kodem
;

     EXCEPTION
         WHEN OTHERS THEN
              RAISE;

end pro_check_Yetzia;

   PROCEDURE  pro_GetOutNull(pMISPAR_ISHI in number, pTAARICH in date,pKnisaHH NUMBER, pKnisaMM NUMBER,  pmispar_sidur in number,p24  in NUMBER, p_cur OUT CurType) IS
BEGIN

    OPEN p_Cur FOR   
    select to_char(shat_gmar,'yyyymmddhh24mi') gmar 
    from  TB_Sidurim_ovdim-- TB_Sidurim_attend
        WHERE mispar_ishi=pMISPAR_ISHI
        AND taarich=pTAARICH  -p24   --TO_DATE(pTAARICH,'dd/mm/yyyy')   
        and mispar_sidur=pmispar_sidur
        and ( shat_hatchala is null or trunc(shat_hatchala)=to_date('01/01/0001','dd/mm/yyyy')  ) ---???
        and pTAARICH + pKnisaHH/24 + pKnisaMM/1440 < shat_gmar -- to_date(pTAARICH,'dd/mm/yyyy') + pKnisaHH/24 + pKnisaMM/1440 < shat_gmar
        order by shat_gmar  ;
    EXCEPTION
   WHEN OTHERS THEN
        RAISE;
 END pro_GetOutNull; 
 

    PROCEDURE  pro_UpdKnisa(pMISPAR_ISHI in number, pTAARICH in date,pKnisaHH NUMBER, pKnisaMM NUMBER ,pMIKUM number,pYetziaHH NUMBER, pYetziaMM NUMBER, pmispar_sidur in number,pStm in varchar,p24  in NUMBER) IS
BEGIN
 update TB_Sidurim_ovdim--  TB_Sidurim_attend
set shat_hatchala = pTAARICH +  pKnisaHH/24 + pKnisaMM/1440,
    mikum_shaon_knisa =pMIKUM -- DECODE(Trim(pMIKUM) , '' ,NULL, '0000',NULL,trim(pMIKUM) ),
        WHERE  mispar_ishi=pMISPAR_ISHI
        
        and taarich=pTAARICH - p24 --to_date(pTAARICH,'dd/mm/yyyy') 
        and mispar_sidur=pmispar_sidur 
        and (shat_hatchala is null  or trunc(shat_hatchala)=to_date('01/01/0001','dd/mm/yyyy')  ) ---???
        and TO_char(shat_gmar,'hh24:mi') = to_char(pTAARICH  + pYetziaHH/24 + pYetziaMM/1440 ,'hh24:mi')   --to_char(to_date(pTAARICH,'dd/mm/yyyy')  + pYetziaHH/24 + pYetziaMM/1440 ,'hh24:mi')   
        and  pTAARICH +  pKnisaHH/24 + pKnisaMM/1440 < shat_gmar ;-- to_date(pTAARICH,'dd/mm/yyyy') +  pKnisaHH/24 + pKnisaMM/1440 < shat_gmar ;
        
        /* update TB_Sidurim_ovdim--  TB_Sidurim_attend
set shat_hatchala = to_date(pTAARICH,'dd/mm/yyyy') +  pKnisaHH/24 + pKnisaMM/1440,
    mikum_shaon_knisa =pMIKUM, -- DECODE(Trim(pMIKUM) , '' ,NULL, '0000',NULL,trim(pMIKUM) ),
    stm_knisa = pStm
        WHERE  mispar_ishi=pMISPAR_ISHI
        and taarich=pTAARICH - p24 --to_date(pTAARICH,'dd/mm/yyyy') 
        and mispar_sidur=pmispar_sidur 
        and shat_hatchala is null
        and TO_char(shat_gmar,'hh24:mi') = to_char(pTAARICH  + pYetziaHH/24 + pYetziaMM/1440 ,'hh24:mi')   --to_char(to_date(pTAARICH,'dd/mm/yyyy')  + pYetziaHH/24 + pYetziaMM/1440 ,'hh24:mi')   
        and  pTAARICH +  pKnisaHH/24 + pKnisaMM/1440 < shat_gmar ;-- to_date(pTAARICH,'dd/mm/yyyy') +  pKnisaHH/24 + pKnisaMM/1440 < shat_gmar ;
   */
    EXCEPTION
   WHEN OTHERS THEN
        RAISE;
 END pro_UpdKnisa; 
    PROCEDURE  pro_GetInNull(pMISPAR_ISHI in number, pTAARICH in date,pYetziaHH NUMBER, pYetziaMM NUMBER,  pmispar_sidur in number,p24  in number,p_cur OUT CurType)is -- pro_GetInNull(pMISPAR_ISHI in number, pTAARICH in date,pYetziaHH NUMBER, pYetziaMM NUMBER,  pmispar_sidur in number,p24  in number,p_cur OUT CurType) IS
BEGIN

    OPEN p_Cur FOR   
    select to_char(shat_hatchala,'yyyymmddhh24mi') knisa 
    from TB_Sidurim_ovdim--  TB_Sidurim_attend
        WHERE mispar_ishi=pMISPAR_ISHI
        AND taarich=pTAARICH -p24 --TO_DATE(pTAARICH,'dd/mm/yyyy') -  p24  
        and mispar_sidur=pmispar_sidur  
        and shat_gmar is null
        and shat_hatchala <    pTAARICH  + pYetziaHH/24 + pYetziaMM/1440   --to_date(pTAARICH,'dd/mm/yyyy') + pYetziaHH/24 + pYetziaMM/1440  
        order by shat_hatchala  ;
    EXCEPTION
   WHEN OTHERS THEN
        RAISE;
 END pro_GetInNull; 
 PROCEDURE  pro_ins_Yetzia (pMISPAR_ISHI in number, pTAARICH in date, pShaa in varchar, pMikum in number, pmispar_sidur in number,p24  in number,pStm in varchar) IS
 c_cunt number;
 v_shat_hatchala date;
  BEGIN
          
    select count(*) into c_cunt
    from TB_Sidurim_ovdim
    where mispar_ishi=pMISPAR_ISHI
    and taarich= pTAARICH - p24 
    and trunc(shat_hatchala) = to_date('01/01/0001','dd/mm/yyyy');
 
   if(c_cunt =0) then
        v_shat_hatchala:= to_date('01/01/0001','dd/mm/yyyy');      
   else
        v_shat_hatchala:=to_date('01/01/0001 00:' ||  (c_cunt) || ':00','dd/mm/yyyy HH24:mi:ss');
   end if;
   
    INSERT INTO  TB_Sidurim_ovdim   (  MISPAR_ISHI , MISPAR_sidur , TAARICH  ,Shat_hatchala ,shat_gmar,Mikum_shaon_yetzia   ,  MEADKEN_ACHARON , TAARICH_IDKUN_ACHARON )
        VALUES  ( pMISPAR_ISHI ,pmispar_sidur  , pTAARICH - p24   ,  v_shat_hatchala,pTAARICH+ (substr(lpad(pShaa,4,0),1,2))/24 + (substr(lpad(pShaa,4,0),3,2))/(24*60),pMikum,-11,sysdate);
      
         /*
     INSERT INTO  TB_Sidurim_ovdim   (  MISPAR_ISHI , MISPAR_sidur ,       TAARICH  ,  --TB_Sidurim_attend
      Shat_hatchala , shat_gmar,   
          Mikum_shaon_yetzia ,   stm_yetzia ,   status_rec ,  MEADKEN_ACHARON , TAARICH_IDKUN_ACHARON )
         VALUES  ( pMISPAR_ISHI ,pmispar_sidur  ,         pTAARICH - p24   , 
         null,     pTAARICH+ (substr(lpad(pShaa,4,0),1,2))/24 + (substr(lpad(pShaa,4,0),3,2))/(24*60),
         pMikum,pStm,0,-11,sysdate);     */
      EXCEPTION
         WHEN OTHERS THEN
              RAISE;

end pro_ins_Yetzia;
    PROCEDURE  pro_UpdYetzia(pMISPAR_ISHI in number, pTAARICH in date,pKnisaHH NUMBER, pKnisaMM NUMBER ,pMIKUM number,pYetziaHH NUMBER, pYetziaMM NUMBER, pmispar_sidur in number,p24  in number,pStm in varchar) IS
BEGIN
 update TB_Sidurim_ovdim--  TB_Sidurim_attend
set shat_gmar = pTAARICH  +  pYetziaHH/24 + pYetziaMM/1440, --to_date(pTAARICH,'dd/mm/yyyy') +  pYetziaHH/24 + pYetziaMM/1440,
    Mikum_shaon_yetzia =pMIKUM -- DECODE(Trim(pMIKUM) , '' ,NULL, '00000',NULL,trim(pMIKUM) ),
 --   stm_yetzia = pStm
        WHERE  mispar_ishi=pMISPAR_ISHI
        and taarich=pTAARICH - p24   --to_date(pTAARICH,'dd/mm/yyyy') - p24  
        and mispar_sidur=pmispar_sidur  
        and shat_gmar is null
        and TO_char(shat_hatchala,'hh24:mi') =to_char(pTAARICH  + pKnisaHH/24 + pKnisaMM/1440 ,'hh24:mi')    --to_char(to_date(pTAARICH,'dd/mm/yyyy')  + pKnisaHH/24 + pKnisaMM/1440 ,'hh24:mi')   
        and shat_hatchala < pTAARICH +  pYetziaHH/24 + pYetziaMM/1440   ;--to_date(pTAARICH,'dd/mm/yyyy') +  pYetziaHH/24 + pYetziaMM/1440   ;
        /*
        update TB_Sidurim_ovdim--  TB_Sidurim_attend
set shat_gmar = pTAARICH  +  pYetziaHH/24 + pYetziaMM/1440, --to_date(pTAARICH,'dd/mm/yyyy') +  pYetziaHH/24 + pYetziaMM/1440,
    Mikum_shaon_yetzia =pMIKUM, -- DECODE(Trim(pMIKUM) , '' ,NULL, '00000',NULL,trim(pMIKUM) ),
    stm_yetzia = pStm
        WHERE  mispar_ishi=pMISPAR_ISHI
        and taarich=pTAARICH - p24   --to_date(pTAARICH,'dd/mm/yyyy') - p24  
        and mispar_sidur=pmispar_sidur  
        and shat_gmar is null
        and TO_char(shat_hatchala,'hh24:mi') =to_char(pTAARICH  + pKnisaHH/24 + pKnisaMM/1440 ,'hh24:mi')    --to_char(to_date(pTAARICH,'dd/mm/yyyy')  + pKnisaHH/24 + pKnisaMM/1440 ,'hh24:mi')   
        and shat_hatchala < pTAARICH +  pYetziaHH/24 + pYetziaMM/1440   ;--to_date(pTAARICH,'dd/mm/yyyy') +  pYetziaHH/24 + pYetziaMM/1440   ;*/
    EXCEPTION
   WHEN OTHERS THEN
        RAISE;
 END pro_UpdYetzia; 


PROCEDURE  pro_check_In (pMISPAR_ISHI in number, pTAARICH in date, pShaa in varchar,pmispar_sidur in number,p24  in number, p_Cur OUT CurType) AS
  BEGIN
   OPEN p_Cur FOR
select  count(*)  from TB_Sidurim_ovdim o --TB_Sidurim_attend o  
where  o.mispar_ishi=pMISPAR_ISHI 
and o.taarich=pTAARICH -p24 --to_date(pTAARICH,'dd/mm/yyyy')
and o.shat_hatchala=pTAARICH + (substr(lpad(pShaa,4,0),1,2))/24 + (substr(lpad(pShaa,4,0),3,2))/(24*60) --to_date(pTAARICH,'dd/mm/yyyy')+ (substr(lpad(pShaa,4,0),1,2))/24 + (substr(lpad(pShaa,4,0),3,2))/(24*60)  
and o.mispar_sidur=pmispar_sidur;

     EXCEPTION
         WHEN OTHERS THEN
              RAISE;

end pro_check_In;

PROCEDURE  pro_ins_In (pMISPAR_ISHI in number, pTAARICH in date, pShaa in varchar, pMikum in number,  pmispar_sidur in number, pStm in varchar,p24  in number) IS
           BEGIN
            INSERT INTO  TB_Sidurim_ovdim   (  MISPAR_ISHI , MISPAR_sidur , TAARICH  , Shat_hatchala ,  --TB_Sidurim_attend
       Mikum_shaon_knisa ,   MEADKEN_ACHARON , TAARICH_IDKUN_ACHARON )
         VALUES  ( pMISPAR_ISHI ,pmispar_sidur ,pTAARICH - p24   , 
        pTAARICH+ (substr(lpad(pShaa,4,0),1,2))/24 + (substr(lpad(pShaa,4,0),3,2))/(24*60),
         pMikum,-11,sysdate);
         
     /*  INSERT INTO  TB_Sidurim_ovdim   (  MISPAR_ISHI , MISPAR_sidur , TAARICH  , Shat_hatchala ,  --TB_Sidurim_attend
       Mikum_shaon_knisa ,   stm_knisa ,   status_rec ,  MEADKEN_ACHARON , TAARICH_IDKUN_ACHARON )
         VALUES  ( pMISPAR_ISHI ,pmispar_sidur ,pTAARICH - p24   , 
        pTAARICH+ (substr(lpad(pShaa,4,0),1,2))/24 + (substr(lpad(pShaa,4,0),3,2))/(24*60),
         pMikum,pStm,0,-11,sysdate);*/
         
      EXCEPTION
         WHEN OTHERS THEN
              RAISE;

end pro_ins_In;


PROCEDURE pro_ins_hityazvut_pundakim(p_mispar_ishi in number,p_taarich in date,p_shaa in date,p_mikum in number)  is
p_count number;
begin
        select count(*) into p_count
        from tb_hityazvut_pundakim p
        where P.MISPAR_ISHI=p_mispar_ishi
           and P.TAARICH=p_taarich
           and P.SHAT_HITYAZVUT= p_shaa
           and P.MIKUM_SHAON =p_mikum;

        if(p_count =0) then
            insert into tb_hityazvut_pundakim(MISPAR_ISHI,TAARICH,SHAT_HITYAZVUT,MIKUM_SHAON,meadken_acharon,taarich_idkun_acharon)
            values (p_mispar_ishi  ,p_taarich  ,p_shaa  ,p_mikum,-11,sysdate);
        end if;
        
          EXCEPTION
         WHEN OTHERS THEN
              RAISE;
end pro_ins_hityazvut_pundakim;
END Pkg_Attendance;
/

CREATE OR REPLACE PACKAGE BODY          Pkg_Reports AS
/******************************************************************************
   NAME:       PKG_REPORTS
   PURPOSE:

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        27/05/2009             1. Created this package body.
******************************************************************************/ 
                                                         
 
      PROCEDURE pro_get_ishurim_lekartis(p_mispar_ishi IN TB_ISHURIM.MISPAR_ISHI%TYPE,
                                                                                   p_taarich IN TB_ISHURIM.TAARICH%TYPE,
                                                                               p_cur OUT CurType)  IS
 BEGIN
 IF TO_NUMBER(TO_CHAR(p_taarich,'DD'))  >1 THEN
        OPEN p_cur FOR
           SELECT  TB.Kod_Ishur ,CTB. Teur_Ishur ,  Si.TEUR_STATUS_ISHUR , Ov.SHEM_MISH || ' ' ||  Ov.SHEM_PRAT Gorem_Meacher ,TB.TAARICH_IDKUN_ACHARON
            FROM TB_ISHURIM TB,  CTB_ISHURIM CTB, CTB_STATUS_ISHURIM Si , OVDIM Ov 
            WHERE TB.Kod_Ishur = CTB. Kod_Ishur
--            and  TB. Kod_Status_Ishur=0
            AND TB. Mispar_Ishi=p_mispar_ishi
            AND TB.Taarich=p_taarich
            AND TB.KOD_STATUS_ISHUR = Si.KOD_STATUS_ISHUR
            AND Ov.MIspar_ishi (+)= TB.GOREM_MEASHER_RASHSI;
ELSE
    OPEN p_cur FOR
                    SELECT  TB.Kod_Ishur ,CTB.Teur_Ishur,  Si.TEUR_STATUS_ISHUR , Ov.SHEM_MISH || ' ' ||  Ov.SHEM_PRAT Gorem_Meacher ,TB.TAARICH_IDKUN_ACHARON
                FROM TB_ISHURIM TB,  CTB_ISHURIM CTB, CTB_STATUS_ISHURIM Si , OVDIM Ov 
                WHERE TB.Kod_Ishur = CTB. Kod_Ishur
  --              and  TB. Kod_Status_Ishur=0
                AND TB. Mispar_Ishi=p_mispar_ishi
                AND TB.Taarich=p_taarich
            AND TB.KOD_STATUS_ISHUR = Si.KOD_STATUS_ISHUR
            AND Ov.MIspar_ishi(+) = TB.GOREM_MEASHER_RASHSI
            UNION ALL 
                SELECT TB.Kod_Ishur ,CTB. Teur_Ishur,  Si.TEUR_STATUS_ISHUR , Ov.SHEM_MISH || ' ' ||  Ov.SHEM_PRAT Gorem_Meacher ,TB.TAARICH_IDKUN_ACHARON
                FROM TB_ISHURIM TB,  CTB_ISHURIM CTB, CTB_STATUS_ISHURIM Si , OVDIM Ov 
                WHERE TB.Kod_Ishur = CTB. Kod_Ishur
                AND TB.Kod_Ishur  IN (34,35)
    --            and  TB. Kod_Status_Ishur=0
                AND TB. Mispar_Ishi=p_mispar_ishi
                AND TO_CHAR(TB.Taarich,'MM') = TO_CHAR(ADD_MONTHS(p_taarich,-1),'MM')
            AND TB.KOD_STATUS_ISHUR = Si.KOD_STATUS_ISHUR
            AND Ov.MIspar_ishi (+) = TB.GOREM_MEASHER_RASHSI ;
END IF;

      EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END  pro_get_ishurim_lekartis;
  
  

PROCEDURE pro_get_ProfilToDisplay(p_ProfilFilter IN VARCHAR2 , p_cur OUT CurType) IS  
  BEGIN 
  
--INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'p_ProfilFilter',p_ProfilFilter);
  
  OPEN p_cur FOR SELECT KOD_PROFIL,TEUR_PROFIL_HEB  FROM(
      SELECT  0 KOD_PROFIL ,'����'  TEUR_PROFIL_HEB , 1 ord FROM dual 
UNION 
      SELECT KOD_PROFIL,TEUR_PROFIL_HEB , 2 ord 
        FROM CTB_PROFIL t
        WHERE LETEZUGA = 1 
        AND lower( T.TEUR_PROFIL) IN 
         (SELECT X FROM TABLE(CAST(Convert_String_To_Table( lower(p_ProfilFilter),  ',') AS MYTABTYPE)))
        ORDER BY KOD_PROFIL,TEUR_PROFIL_HEB
    );
        EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END  pro_get_ProfilToDisplay;

  
  
  PROCEDURE pro_get_Comment_Approval(p_cur OUT CurType) IS 
  BEGIN 
  OPEN p_cur FOR SELECT Kod_Ishur,teur_ishur  FROM(
  SELECT  -1 Kod_Ishur ,'���'  teur_ishur , 1 ord FROM dual 
UNION 
SELECT Kod_Ishur,
CASE WHEN LENGTH(Kod_Ishur)>=3 AND  SUBSTR(Kod_Ishur,LENGTH(Kod_Ishur) -1 ,LENGTH(Kod_Ishur))   = '11' THEN
 teur_ishur || ' , ��� ������'
 ELSE 
 teur_ishur 
 END teur_ishur , 2 ord FROM CTB_ISHURIM 
WHERE Pail = 1  ORDER BY ord,teur_ishur);
        EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END  pro_get_Comment_Approval;
  
  
  
 PROCEDURE pro_get_Regions(p_cur OUT CurType) IS 
  BEGIN 
  OPEN p_cur FOR SELECT kod_ezor,teur_ezor  FROM(
 -- SELECT  -1 kod_ezor ,'���'  teur_ezor , 1 ord FROM dual 
--UNION
SELECT DISTINCT kod_ezor,teur_ezor ,2 ord FROM CTB_EZOR 
 ORDER BY ord,teur_ezor);
        EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END  pro_get_Regions;
  
  
  
  
  
  PROCEDURE pro_get_Status_Approval(p_cur OUT CurType) IS 
  BEGIN 
--set transaction read only ;
  OPEN p_cur FOR 
-- select LICENSE_NUMBER kod_status_ishur,1 teur_status_ishur FROM VCL_GENERAL_VEHICLE_VIEW@kds2maale ORDER BY LICENSE_NUMBER;
  SELECT  -1 kod_status_ishur ,'���'  teur_status_ishur FROM dual 
UNION 
 SELECT kod_status_ishur, teur_status_ishur FROM CTB_STATUS_ISHURIM ;

        EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END  pro_get_Status_Approval;
  
  PROCEDURE pro_get_Factors_Confirm(p_cur OUT CurType)   IS
 BEGIN
  OPEN p_cur FOR 
  SELECT FullName,mispar_ishi FROM 
  (SELECT '���' FullName, -1 mispar_ishi , 1 ord FROM dual
UNION 
  SELECT DISTINCT o.SHEM_MISH || ' ' || O. SHEM_PRAT FullName, o.mispar_ishi,2 ord
FROM TB_ISHURIM TB,  OVDIM O WHERE TB. Gorem_Measher_Rashsi = O. Mispar_Ishi 
ORDER BY ord,FullName);

        EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END  pro_get_Factors_Confirm;
  
  PROCEDURE pro_get_CompanyId ( p_cur OUT CurType ) IS 
  BEGIN 
  
  OPEN p_cur FOR 
/*  SELECT Teur_Hevra,Kod_Hevra FROM 
  (SELECT '���' Teur_Hevra, -1 Kod_Hevra , 1 ord FROM dual
UNION */
    SELECT Teur_Hevra , Kod_Hevra,   2 ord FROM CTB_HEVRA 
ORDER BY ord,Teur_Hevra;

        EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END  pro_get_CompanyId;


  
  PROCEDURE pro_get_Ishurim_RashemetOld (p_cur OUT Curtype ,
                                                                 P_MISPAR_ISHI IN OVDIM.mispar_ishi%TYPE, 
                                                                 P_STARTDATE IN DATE,
                                                                 P_ENDDATE IN DATE ,
                                                                 P_KOD_ISHUR TB_ISHURIM.Kod_Status_Ishur%TYPE ,
                                                                 P_FactorConfirm IN OVDIM.mispar_ishi%TYPE,
                                                                 P_STATUS IN TB_ISHURIM.Kod_Status_Ishur%TYPE ) IS
                                                                 strGeneral VARCHAR2(10000);
                                                                 strFilter VARCHAR2(500);
BEGIN 



strGeneral := 'Select TB.Mispar_Ishi ,' ||
            'O. SHEM_MISH || '' '' ||  O. SHEM_PRAT FULL_NAME,' ||
            'TB.Taarich,CTB.Teur_Ishur,' ||
            'OvGorem. SHEM_MISH || '' '' ||  OvGorem.SHEM_PRAT Factor_Confirm,'||
            'S.Teur_Status_Ishur ,TB.TAARICH_IDKUN_ACHARON ' ||
            'From TB_Ishurim TB,CTB_Ishurim CTB,'  ||
            'Ovdim O, Ovdim OvGorem,CTB_Status_Ishurim S ' ||
            'Where TB.Kod_Ishur = CTB.Kod_Ishur ' ||
            'and TB.Mispar_Ishi  = O.Mispar_Ishi ' || 
            'and TB.Gorem_Measher_Rashsi  = OvGorem.Mispar_Ishi ' ||
            'and TB.Kod_Status_Ishur=S.Kod_Status_Ishur ' ;
strFilter := '' ;
IF ( P_MISPAR_ISHI IS NOT NULL ) THEN
    strFilter := ' and TB.Mispar_Ishi = ' || P_MISPAR_ISHI;
END IF ; 
IF ( P_KOD_ISHUR <> -1) THEN
    strFilter := strFilter || ' and TB.Kod_Ishur = ' || P_KOD_ISHUR;
END IF ; 
IF ( P_FactorConfirm  <> -1 ) THEN
    strFilter := strFilter || ' and TB.Gorem_Measher_Rashsi = ' || P_FactorConfirm;
END IF ; 
IF ( P_STATUS <> -1  ) THEN
    strFilter := strFilter || ' and TB.Kod_Status_Ishur = ' || P_STATUS;
END IF ; 
IF (( P_STARTDATE IS NOT NULL  ) OR ( P_STARTDATE <> '' )) THEN
    strFilter := strFilter || ' and TB.Taarich >= ''' ||  P_STARTDATE  || '''';  --trunc
END IF ; 
IF (( P_ENDDATE IS NOT  NULL ) OR ( P_ENDDATE <> '' )) THEN 
    strFilter := strFilter || ' and TB.Taarich <= ''' ||  P_ENDDATE  || '''';  --trunc
END IF ; 

OPEN p_cur FOR strGeneral || strFilter || ' ORDER BY Mispar_Ishi desc  , taarich desc ,CTB.Kod_Ishur asc ' ; 
           SELECT REPLACE(strGeneral,'Where',CHR(13) ||'Where' ) INTO strGeneral FROM dual; 
           SELECT REPLACE(strGeneral,'From',CHR(13) ||'From' ) INTO strGeneral FROM dual; 
           SELECT REPLACE(strFilter,'and',CHR(13) ||'and' ) INTO strFilter FROM dual; 
           DBMS_OUTPUT.PUT_LINE('sql = ' || strGeneral || strFilter);
     
        EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END  pro_get_Ishurim_RashemetOld;     

  
    PROCEDURE pro_get_Ishurim_Rashemet (p_cur OUT Curtype ,
                                                                 P_WorkerViewLevel IN NUMBER, 
                                                                 P_WORKERID IN VARCHAR2, 
                                                                 P_MISPAR_ISHI IN VARCHAR2, 
                                                                 P_STARTDATE IN DATE,
                                                                 P_ENDDATE IN DATE ,
                                                                 P_KOD_ISHUR TB_ISHURIM.Kod_Status_Ishur%TYPE ,
                                                                 P_FactorConfirm IN OVDIM.mispar_ishi%TYPE,
                                                                 P_STATUS IN TB_ISHURIM.Kod_Status_Ishur%TYPE ) IS
                                                                 strGeneral VARCHAR2(10000);
                                                                 strFilter VARCHAR2(500);
BEGIN 

IF (P_WorkerViewLevel = 5) THEN  
        EXECUTE IMMEDIATE 'truncate table TMP_MANAGE_TREE' ; 
        COMMIT ; 
        Pkg_Utils.pro_ins_Manage_Tree(TO_NUMBER(P_WORKERID,999999999));
END IF ;

 /*INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_WorkerViewLevel:',P_WorkerViewLevel);
 INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_WORKERID:',P_WORKERID);
 INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_MISPAR_ISHI:',P_MISPAR_ISHI);
 INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_STARTDATE:',P_STARTDATE);
 INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_ENDDATE:',P_ENDDATE);
 INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_KOD_ISHUR:',P_KOD_ISHUR);
 INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_FactorConfirm:',P_FactorConfirm);
 INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_STATUS:',P_STATUS);
*/
strGeneral := 'Select TB.Mispar_Ishi ,
            O.SHEM_MISH || '' '' ||  O.SHEM_PRAT FULL_NAME,
            TB.Taarich,CTB.Teur_Ishur,
            OvGorem. SHEM_MISH || '' '' ||  OvGorem.SHEM_PRAT Factor_Confirm,
            S.Teur_Status_Ishur ,TB.TAARICH_IDKUN_ACHARON 
            FROM TB_ISHURIM TB,CTB_ISHURIM CTB,
            OVDIM O, OVDIM OvGorem,CTB_STATUS_ISHURIM S ' ;
            IF (P_WorkerViewLevel = 5) THEN
              strGeneral := strGeneral || ',TMP_MANAGE_TREE Tree ';
            END IF ;    
              strGeneral := strGeneral || 'where
             TB.Kod_Ishur = CTB.Kod_Ishur 
            AND TB.Mispar_Ishi  = O.Mispar_Ishi  
            AND TB.Gorem_Measher_Rashsi  =  OvGorem.Mispar_Ishi (+) 
            AND TB.Kod_Status_Ishur=S.Kod_Status_Ishur ' ;
            
strFilter := '' ;
IF ( P_KOD_ISHUR <> -1) THEN
    strFilter := strFilter || ' and TB.Kod_Ishur = ' || P_KOD_ISHUR;
END IF ; 
IF ( P_FactorConfirm  <> -1 ) THEN
    strFilter := strFilter || ' and TB.Gorem_Measher_Rashsi = ' || P_FactorConfirm;
END IF ; 
IF ( P_STATUS <> -1  ) THEN
    strFilter := strFilter || ' and TB.Kod_Status_Ishur = ' || P_STATUS;
END IF ; 
IF (( P_STARTDATE IS NOT NULL  ) OR ( P_STARTDATE <> '' )) THEN
    strFilter := strFilter || ' and TB.Taarich >= ''' || P_STARTDATE  || '''';  --trunc
END IF ; 
IF (( P_ENDDATE IS NOT  NULL ) OR ( P_ENDDATE <> '' )) THEN 
    strFilter := strFilter || ' and TB.Taarich <= ''' || P_ENDDATE  || '''';  --trunc
END IF ; 


IF (
       (
            (NVL(P_MISPAR_ISHI,-1) <> NVL(P_WORKERID,-1))  
            OR  
            (P_WorkerViewLevel =0)
        )
    AND ( (P_MISPAR_ISHI <> '' ) OR   (P_MISPAR_ISHI IS NOT NULL ))
  )THEN 
        strFilter := strFilter || ' and TB.mispar_ishi in (' || P_MISPAR_ISHI || ') ';
END IF ; 


IF (P_WorkerViewLevel = 5) THEN
  strFilter := strFilter || 'And  TB.mispar_ishi in Tree.mispar_ishi ';
END IF ;    


IF (( strFilter IS NOT NULL  ) OR ( strFilter <> '')) THEN
  strGeneral := strGeneral || strFilter;
END IF ;

DBMS_OUTPUT.PUT_LINE(strGeneral );
OPEN p_cur FOR strGeneral || ' ORDER BY Mispar_Ishi desc  , taarich desc ,CTB.Kod_Ishur asc ' ; 
     
        EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END  pro_get_Ishurim_Rashemet;     
       



PROCEDURE pro_get_MakatOfActivity(p_FromDate IN DATE , p_ToDate IN DATE, p_prefix IN VARCHAR2 , p_cur OUT CurType) AS 
BEGIN 

OPEN p_cur FOR 
SELECT DISTINCT Makat_nesia 
FROM TB_PEILUT_OVDIM Activity
WHERE ACTIVITY.TAARICH BETWEEN  p_FromDate AND  p_todate 
AND ACTIVITY.MAKAT_NESIA LIKE  p_prefix  ||  '%' ; 
        EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END  pro_get_MakatOfActivity;     


PROCEDURE pro_get_SidurimOvdim(p_FromDate IN DATE , p_ToDate IN DATE, p_prefix IN VARCHAR2 , p_cur OUT CurType) AS 
BEGIN 

OPEN p_cur FOR 
SELECT DISTINCT Mispar_sidur  
FROM TB_SIDURIM_OVDIM So
WHERE SO.TAARICH BETWEEN  p_FromDate AND  p_todate 
AND SO.MISPAR_SIDUR  LIKE   p_prefix ||'%'   
ORDER BY 1 ; 
        EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END  pro_get_SidurimOvdim; 
  

PROCEDURE pro_get_WorkStation(p_prefix IN VARCHAR2 , p_cur OUT CurType) AS 
BEGIN 

OPEN p_cur FOR 
SELECT DISTINCT ST.KOD_SNIF_TNUAA FROM 
CTB_SNIFEY_TNUAA st 
WHERE ST.KOD_SNIF_TNUAA  LIKE   p_prefix  ||  '%' ; 
        EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END  pro_get_WorkStation; 
    


PROCEDURE pro_get_LicenseNumber(p_prefix IN VARCHAR2 , p_cur OUT CurType) AS 
BEGIN 

OPEN p_cur FOR 
SELECT LICENSE_NUMBER FROM VEHICLE_SPECIFICATIONS V --VCL_GENERAL_VEHICLE_VIEW@kds2maale V 
WHERE v.LICENSE_NUMBER LIKE  P_PREFIX  ||  '%'; 
        EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END  pro_get_LicenseNumber; 
    

PROCEDURE pro_get_Id_of_Yamey_Avoda(p_FromDate IN DATE , p_ToDate IN DATE,p_Prefix IN VARCHAR2, p_cur OUT CurType) AS 
BEGIN 
IF (p_Prefix <> ''  ) THEN 
OPEN p_cur FOR 
--select 1 mispar_ishi from dual ;
  SELECT DISTINCT mispar_ishi 
  FROM  TB_YAMEY_AVODA_OVDIM O
  WHERE O.TAARICH BETWEEN  p_FromDate AND  p_todate 
  AND O.MISPAR_ISHI LIKE   p_prefix  ||  '%' ; 
  ELSE 
OPEN p_cur FOR 
--select 1 mispar_ishi from dual ;
  SELECT DISTINCT mispar_ishi 
  FROM  TB_YAMEY_AVODA_OVDIM O
  WHERE O.TAARICH BETWEEN  p_FromDate AND p_todate 
  AND O.MISPAR_ISHI LIKE p_prefix  || '%' ; 
END IF ;  
  
        EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END  pro_get_Id_of_Yamey_Avoda;     

PROCEDURE pro_get_Id_of_Yamey_Avoda(p_FromDate IN DATE , p_ToDate IN DATE ,p_CompanyId IN VARCHAR2,p_Prefix IN VARCHAR2, p_cur OUT CurType) AS 
BEGIN 
  --  open p_cur for 
    --select p_CompanyId mispar_ishi ,  p_CompanyId FullName from dual ;

IF (p_Prefix <> ''  ) THEN 
    OPEN p_cur FOR 
           SELECT    O.mispar_ishi ,  FullName
            FROM ( SELECT DISTINCT  mispar_ishi 
                      FROM TB_YAMEY_AVODA_OVDIM
                      WHERE TAARICH BETWEEN  p_FromDate AND  p_todate 
                        AND (mispar_ishi LIKE  p_prefix  ||  '%'  )
                    )  O ,
                    (SELECT mispar_ishi, (mispar_ishi ||  ' (' || SHEM_MISH || ' ' || SHEM_PRAT || ')')  FullName  FROM OVDIM 
                    WHERE ((p_CompanyId =  '-1') OR  ( KOD_HEVRA = p_CompanyId))
                    ) Ov
            WHERE  OV.MISPAR_ISHI = O.MISPAR_ISHI(+) ;
ELSE 
    OPEN p_cur FOR 
           SELECT    O.mispar_ishi ,  FullName
            FROM ( SELECT DISTINCT  mispar_ishi 
                      FROM TB_YAMEY_AVODA_OVDIM
                      WHERE TAARICH BETWEEN  p_FromDate AND  p_todate  
                    )  O ,
                    (SELECT mispar_ishi, (mispar_ishi ||  ' (' || SHEM_MISH || ' ' || SHEM_PRAT || ')')  FullName  FROM OVDIM 
                    WHERE ((p_CompanyId =  '-1') OR  ( KOD_HEVRA = p_CompanyId))
                    ) Ov
            WHERE  OV.MISPAR_ISHI = O.MISPAR_ISHI(+) ;
END IF ;               

        EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END  pro_get_Id_of_Yamey_Avoda;     
  
 
 
   PROCEDURE pro_get_Find_Worker_Card_2( p_cur OUT CurType, 
                                                                P_STARTDATE IN DATE,
                                                                P_ENDDATE IN DATE , 
                                                                P_SIDURNUMBER IN VARCHAR2,
                                                                P_Makat IN VARCHAR2,
                                                                P_MISPARVISA IN VARCHAR2, 
                                                                P_CARNUMBER IN VARCHAR2,
                                                                P_RISHUYCAR   IN  VARCHAR2,
                                                                P_WORKERID  IN  VARCHAR2,
                                                                P_EZOR  IN VARCHAR2,                                                               
                                                                P_SNIF IN VARCHAR2,
                                                                P_MAMAD IN VARCHAR2
                                                                 ) AS     
       p_lst_makat   VARCHAR2(300);   
       p_lst_rishuy   VARCHAR2(300);                                          
begin
    
 

 pro_prepare_netuney_tnua_itur( P_STARTDATE , P_ENDDATE  ,   P_SIDURNUMBER ,  p_lst_makat , P_WORKERID  );
  
   if (P_Makat is null) then
     p_lst_makat:='-1';
     else p_lst_makat:=P_Makat;
  end if;
  
  if (P_RISHUYCAR is null) then
     p_lst_rishuy:='-1';
     else p_lst_rishuy:=P_RISHUYCAR;
  end if;
  
    OPEN p_cur FOR    
          with makatTB as(
                    (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_lst_makat,  ',') AS MYTABTYPE))) 
           ),
           oto as(
                select p.VEHICLE_ID, P.LICENSE_NUMBER,P.EVENT_DATE me_taarich, P.AD_TAARICH
                from PIVOT_VEHICLE_AUDIT p  --7530169
                where (p_lst_rishuy='-1' or exists (select 1
                                                    from  (select X FROM TABLE(CAST(Convert_String_To_Table(p_lst_rishuy,  ',') AS MYTABTYPE))) z
                                                    where z.x= P.LICENSE_NUMBER))
           )
          , 
           sidur as(
                 select h.taarich,h.yom,h.MISPAR_ISHI , h.MISPAR_SIDUR, h.SHAT_HATCHALA,h.SHAT_HATCHALA_CHR  ,
                        h.SHAT_GMAR,h.LO_LETASHLUM, h.SUG_SIDUR, h.MAKAT_NESIA, h.SHAT_YETZIA, h.SHAT_YETZIA_CHR ,
                        h.mispar_knisa,h.OTO_NO, h.MISPAR_VISA, h.LICENSE_NUMBER , h.shilut, h.DESCRIPTION,
                        case  when makat_type=5 then round(pkg_elements.getKmByMakatElement(h.makat_nesia,'true'),2)  
                              when ((makat_type in (1,2,3)) and h.mispar_knisa=0)  then h.km 
                              when makat_type=6 then h.KM_VISA end km,
                        case  when makat_type=5 then pkg_elements.getDakotByMakatElement(h.makat_nesia,'false')
                              when ((makat_type in (1,2,3)) and h.mispar_knisa=0) then h.MAZAN_TICHNUN end  mazan_tichnun
                              
                from    
                    (select s.taarich,DAYOFWEEK( s.taarich) yom,S.MISPAR_ISHI , S.MISPAR_SIDUR, S.SHAT_HATCHALA,to_char( S.SHAT_HATCHALA,'dd/mm/yyyy HH24:mi') SHAT_HATCHALA_CHR  ,
                              to_char( S.SHAT_GMAR,'dd/mm/yyyy HH24:mi') SHAT_GMAR,S.LO_LETASHLUM, S.SUG_SIDUR,
                              P.MAKAT_NESIA, P.SHAT_YETZIA, to_char( P.SHAT_YETZIA,'dd/mm/yyyy HH24:mi') SHAT_YETZIA_CHR , p.mispar_knisa,P.OTO_NO, P.MISPAR_VISA, V.LICENSE_NUMBER , c.shilut,--c.DESCRIPTION
                          TO_CHAR(  nvl( pkg_reports.fun_get_teur_nesia(P.MAKAT_NESIA,p.mispar_knisa, C.DESCRIPTION), P.TEUR_NESIA)) DESCRIPTION,
                          Pkg_Tnua.fn_get_makat_type(p.makat_nesia) makat_type,P.KM_VISA, C.KM, C.MAZAN_TICHNUN
                        --  case when makat_type=5 then pkg_elements.getKmByMakatElement(s.makat_nesia) else s.km end km
                         --  C.DESCRIPTION
                         
                    from tb_sidurim_ovdim s,tb_peilut_ovdim p,PIVOT_VEHICLE_AUDIT v, tmp_catalog c  , makatTB
              
                    where
                              s.taarich between P_STARTDATE and P_ENDDATE
                        and S.MISPAR_ISHI = P.MISPAR_ISHI(+)
                        and S.TAARICH = P.TAARICH(+)
                        and S.MISPAR_SIDUR=P.MISPAR_SIDUR(+)
                        and S.SHAT_HATCHALA=P.SHAT_HATCHALA_SIDUR(+)
                        and P.OTO_NO = V.VEHICLE_ID(+)
                        and p.taarich between V.EVENT_DATE(+) and v.AD_TAARICH(+)
                        and p.makat_nesia= C.MAKAT8(+)
                        and p.taarich = C.ACTIVITY_DATE(+)
                        and (P_SIDURNUMBER is null  or  S.MISPAR_SIDUR in   (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_SIDURNUMBER,  ',') AS MYTABTYPE)))   )
                       and (P_Makat is null or LPAD(p.MAKAT_NESIA,8,'0') like  makatTB.x || '%' )  
                        and (P_MISPARVISA is null or P.MISPAR_VISA in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MISPARVISA,  ',') AS MYTABTYPE)))   )
                        and (P_CARNUMBER is null or P.OTO_NO in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_CARNUMBER,  ',') AS MYTABTYPE)))   )
                       
                        and (p_lst_rishuy ='-1' or EXISTS (select 1
                                                            from oto o 
                                                            where V.LICENSE_NUMBER = o.LICENSE_NUMBER
                                                                and p.taarich between  o.me_taarich and o.AD_TAARICH   )  
                              )  
                      --  and (P_RISHUYCAR is null  or V.LICENSE_NUMBER  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_RISHUYCAR,  ',') AS MYTABTYPE)))   )
                        and (P_WORKERID is null or S.MISPAR_ISHI in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_WORKERID,  ',') AS MYTABTYPE)))   )
                        ) h  
                )
        , sidurH as(
                select s.taarich,DAYOFWEEK( s.taarich) yom,S.MISPAR_ISHI , S.MISPAR_SIDUR, S.SHAT_HATCHALA,to_char( S.SHAT_HATCHALA,'dd/mm/yyyy HH24:mi') SHAT_HATCHALA_CHR,
                           to_char( S.SHAT_GMAR,'dd/mm/yyyy HH24:mi')  SHAT_GMAR_CHR,S.LO_LETASHLUM, S.SUG_SIDUR,
                          P.MAKAT_NESIA, P.SHAT_YETZIA, to_char( P.SHAT_YETZIA,'dd/mm/yyyy HH24:mi') SHAT_YETZIA_CHR,p.mispar_knisa,P.OTO_NO, P.MISPAR_VISA,  V.LICENSE_NUMBER ,
                          CH.KAV_RASHI shilut,P.TEUR_NESIA DESCRIPTION, 
                             case  when (length(p.MAKAT_NESIA)=8 and substr(p.MAKAT_NESIA,1,1)='5') then p.km_visa else CH.KLM  end  KM,
                              CH.HAGDARAT_TICHNUN mazan_tichnun
                from history_sidurim_ovdim s,history_peilut_ovdim p, PIVOT_VEHICLE_AUDIT v, MF_HIST_CATALOG_NESIOT ch,makatTB
          
                where
                          s.taarich between P_STARTDATE and P_ENDDATE
                    and S.MISPAR_ISHI = P.MISPAR_ISHI(+)
                    and S.TAARICH = P.TAARICH(+)
                    and S.MISPAR_SIDUR=P.MISPAR_SIDUR(+)
                    and S.SHAT_HATCHALA=P.SHAT_HATCHALA_SIDUR(+)
                    and P.OTO_NO = V.VEHICLE_ID(+)
                    and p.taarich between V.EVENT_DATE(+) and v.AD_TAARICH(+)
                    and (P_SIDURNUMBER is null  or  S.MISPAR_SIDUR in   (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_SIDURNUMBER,  ',') AS MYTABTYPE)))   )
                    and (P_Makat is null or LPAD(p.MAKAT_NESIA,8,'0') like  makatTB.x || '%' )  
                    and (P_MISPARVISA is null or P.MISPAR_VISA in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MISPARVISA,  ',') AS MYTABTYPE)))   )
                    and (P_CARNUMBER is null or P.OTO_NO in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_CARNUMBER,  ',') AS MYTABTYPE)))   )
                    and (P_RISHUYCAR is null or EXISTS (select 1
                                                        from oto o 
                                                        where V.LICENSE_NUMBER = o.LICENSE_NUMBER
                                                          and p.taarich between  o.me_taarich and o.AD_TAARICH   )  
                              )  
                   -- and (P_RISHUYCAR is null  or V.LICENSE_NUMBER  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_RISHUYCAR,  ',') AS MYTABTYPE)))   )
                    and (P_WORKERID is null or S.MISPAR_ISHI in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_WORKERID,  ',') AS MYTABTYPE)))   )
                    and P.MAKAT_NESIA= CH.MAKAT_BITZUA(+)
                    and P.TAARICH between to_date( ch.taarich_from(+),'yyyy/mm/dd') and to_date( ch.taarich_to(+),'yyyy/mm/dd')
                )
         select *
         from(
                 select s.*,O.SHEM_PRAT || ' ' || O.SHEM_MISH shem,E.TEUR_EZOR,M.TEUR_MAAMAD_HR, F.TEUR_SNIF_AV,
                         case when  LENGTH(s.MISPAR_SIDUR)=5 AND substr(s.MISPAR_SIDUR,0,2)='99' then (select SM.TEUR_SIDUR_MEYCHAD from ctb_sidurim_meyuchadim sm where s.MISPAR_SIDUR = SM.KOD_SIDUR_MEYUCHAD )
                                 else (select SS.TEUR_SIDUR_AVODA from ctb_sug_sidur ss where s.SUG_SIDUR = SS.KOD_SIDUR_AVODA ) end teur_sidur
                 from sidur s,  ovdim_history o,pivot_pirtey_ovdim_hist po, ctb_ezor e  ,ctb_maamad m,ctb_snif_av f      
                  where  s.mispar_ishi= o.mispar_ishi
                      and s.mispar_ishi= po.mispar_ishi
                      and s.taarich between PO.ME_TARICH and PO.AD_TARICH
                      and po.ezor= E.KOD_EZOR
                      and E.KOD_HEVRA=O.KOD_HEVRA
                      and po.maamad= M.KOD_MAAMAD_HR
                      and m.KOD_HEVRA=O.KOD_HEVRA
                      and po.snif_av= f.kod_snif_av
                      and f.KOD_HEVRA=O.KOD_HEVRA
                      and (P_EZOR is null or  Po.EZOR  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_EZOR,  ',') AS MYTABTYPE))) )
                      and (P_SNIF is null or Po.SNIF_AV  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_SNIF,  ',') AS MYTABTYPE))) )
                      and (P_MAMAD is null or Po.MAAMAD  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MAMAD,  ',') AS MYTABTYPE))) )
              
            union all
            
              select s.*,O.SHEM_PRAT || ' ' || O.SHEM_MISH shem,E.TEUR_EZOR,M.TEUR_MAAMAD_HR, F.TEUR_SNIF_AV, 
                         case when  LENGTH(s.MISPAR_SIDUR)=4 AND substr(s.MISPAR_SIDUR,0,2)='85' then (select SM.TEUR_SIDUR_MEYCHAD from ctb_sidurim_meyuchadim sm where s.MISPAR_SIDUR = SM.KOD_SIDUR_MEYUCHAD_yashan )
                                else (select SS.TEUR_SIDUR_AVODA from ctb_sug_sidur ss where s.SUG_SIDUR = SS.KOD_SIDUR_AVODA ) end teur_sidur
                    from sidurH s,  ovdim_history o,pivot_pirtey_ovdim_hist po, ctb_ezor e  ,ctb_maamad m,ctb_snif_av f      
                  where  s.mispar_ishi= o.mispar_ishi
                      and s.mispar_ishi= po.mispar_ishi
                      and s.taarich between PO.ME_TARICH and PO.AD_TARICH
                      and po.ezor= E.KOD_EZOR
                      and E.KOD_HEVRA=O.KOD_HEVRA
                      and po.maamad= M.KOD_MAAMAD_HR
                      and m.KOD_HEVRA=O.KOD_HEVRA
                      and po.snif_av= f.kod_snif_av
                      and f.KOD_HEVRA=O.KOD_HEVRA
                      and (P_EZOR is null or  Po.EZOR  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_EZOR,  ',') AS MYTABTYPE))) )
                      and (P_SNIF is null or Po.SNIF_AV  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_SNIF,  ',') AS MYTABTYPE))) )
                      and (P_MAMAD is null or Po.MAAMAD  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MAMAD,  ',') AS MYTABTYPE))) )
              )
  order by mispar_ishi,taarich,shat_hatchala, SHAT_YETZIA,mispar_knisa;
        
  
   EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;           
end   pro_get_Find_Worker_Card_2;                 



procedure pro_prepare_netuney_tnua_itur( P_STARTDATE IN DATE, P_ENDDATE IN DATE ,   P_SIDURNUMBER IN VARCHAR2,  p_lst_makat IN VARCHAR2, P_WORKERID  IN  VARCHAR ) is  
       rc NUMBER ;
        makat_s varchar2(2000);
        InsertQry varchar2(3500);
begin
    EXECUTE IMMEDIATE  'truncate table tmp_Catalog' ;      
if(p_lst_makat<>'' and p_lst_makat is not null ) then
       SELECT PREPARE_LIKE_OF_MKAT8('p.makat_nesia',p_lst_makat,'%') into makat_s FROM DUAL;
end if;
      InsertQry:=' INSERT INTO  kds.TMP_CATALOG_DETAILS@KDS_GW_AT_TNPR(makat8,activity_date)
                            Select  distinct p.makat_nesia,p.TAARICH  
                            FROM TB_PEILUT_OVDIM p 
                            where  p.MISPAR_SIDUR not like ''99%''
                                AND p.taarich BETWEEN   ''' || P_STARTDATE  || ''' and    ''' ||P_ENDDATE ||''' ';
           if (P_SIDURNUMBER  is not null ) then
            InsertQry:= InsertQry || ' and p.MISPAR_SIDUR in  (' ||  P_SIDURNUMBER ||')';
           end if;
           if(p_lst_makat  is not null  ) then
                InsertQry:= InsertQry || ' and ' || makat_s;
           end if;
            if (P_WORKERID  is not null ) then
                InsertQry:= InsertQry || ' and p.MISPAR_ISHI in  (' ||  P_WORKERID ||')';
           end if;                    
                             
        --   DBMS_OUTPUT.PUT_LINE( p_lst_makat );       
        --  DBMS_OUTPUT.PUT_LINE(InsertQry);      
        EXECUTE IMMEDIATE InsertQry ;
        
    kds_catalog_pack.GetKavimDetails@KDS_GW_AT_TNPR(rc);
     INSERT INTO TMP_CATALOG( activity_date,makat8, Shilut,Description,nihul_name,mazan_tashlum,mazan_tichnun,Km,sug_shirut_name,eilat_trip,onatiut,kisuy_tor,eshel,migun,xy_moked_tchila,xy_moked_siyum,snif,snif_name,sug_auto ) 
    SELECT activity_date, makat8, Shilut,Description,nihul_name,mazan_tashlum,mazan_tichnun,Km,sug_shirut_name,eilat_trip,onatiut,kisui_tor,eshel,migun,xy_moked_tchila,xy_moked_siyum,snif,snif_name,sug_auto   FROM kds.TMP_CATALOG_DETAILS@KDS_GW_AT_TNPR;
  COMMIT ; 
                                      
end pro_prepare_netuney_tnua_itur;
  
  /*
  PROCEDURE pro_get_Find_Worker_Card_2( p_cur OUT CurType, 
                                                                P_STARTDATE IN DATE,
                                                                P_ENDDATE IN DATE , 
                                                                P_SIDURNUMBER IN VARCHAR2,
                                                                P_Makat IN VARCHAR2,
                                                                P_MISPARVISA IN VARCHAR2, 
                                                                P_CARNUMBER IN VARCHAR2,
                                                                P_RISHUYCAR   IN  VARCHAR2,
                                                                P_WORKERID  IN  VARCHAR2,
                                                                P_EZOR  IN VARCHAR2,                                                               
                                                                P_SNIF IN VARCHAR2,
                                                                P_MAMAD IN VARCHAR2
                                                                 ) AS
                                                                 
      GeneralQry VARCHAR2(32767);
      QryMakatDate VARCHAR2(3500);
       rc NUMBER ;        
       p_lst_makat   VARCHAR2(300);       
       ParamQry       VARCHAR2(300);                                          
begin
    
   if (P_Makat is null) then
     p_lst_makat:='-1';
     else p_lst_makat:=P_Makat;
  end if;

    QryMakatDate:='
                    with makatTB as(
                     (SELECT X FROM TABLE(CAST(Convert_String_To_Table( ''' || p_lst_makat ||  ''',  '','') AS MYTABTYPE))) 
                    )
                    Select  distinct p.makat_nesia,p.TAARICH  
                    FROM TB_PEILUT_OVDIM p
                       where  p.MISPAR_SIDUR not like ''99%''
                      AND p.taarich BETWEEN  ''' || P_STARTDATE  || ''' AND ''' ||  P_ENDDATE  || '''';

IF (( P_WORKERID IS NOT  NULL ) ) THEN 
    ParamQry := ParamQry || ' AND  p.mispar_ishi in (' || P_WORKERID || ')  ';
END IF ;  
IF (( P_Makat IS NOT  NULL )) THEN 
    ParamQry := ParamQry || ' AND LPAD(p.MAKAT_NESIA,8,''0'') like  makatTB.x || ''%'' ) ';-- ||  Prepare_Like_Of_List('p.makat_nesia', P_Makat,'''')  ;
END IF ; 
IF (( P_SIDURNUMBER IS NOT  NULL )) THEN 
    ParamQry := ParamQry ||  ' AND  p.mispar_sidur in (' || P_SIDURNUMBER || ') ';
END IF ; 

IF (( ParamQry IS NOT NULL  ) OR ( ParamQry <> '')) THEN 
 GeneralQry := GeneralQry  || ParamQry;
END IF ;

   --   DBMS_OUTPUT.PUT_LINE(QryMakatDate);
-- QryMakatDate :=  fct_MakatDateSql(P_STARTDATE ,P_ENDDATE  ,P_Makat ,P_SIDURNUMBER ,P_WORKERID  );
   pro_Prepare_Catalog_Details(QryMakatDate);
  COMMIT ; 
   

  
    OPEN p_cur FOR     
            with makatTB as(
                     (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_lst_makat,  ',') AS MYTABTYPE))) 
            )
           , sidur as(
                select s.taarich,DAYOFWEEK( s.taarich) yom,S.MISPAR_ISHI , S.MISPAR_SIDUR, S.SHAT_HATCHALA,to_char( S.SHAT_HATCHALA,'dd/mm/yyyy HH24:mi') SHAT_HATCHALA_CHR  ,
                          to_char( S.SHAT_GMAR,'dd/mm/yyyy HH24:mi') SHAT_GMAR,S.LO_LETASHLUM, S.SUG_SIDUR,
                          P.MAKAT_NESIA, P.SHAT_YETZIA, to_char( P.SHAT_YETZIA,'dd/mm/yyyy HH24:mi') SHAT_YETZIA_CHR , p.mispar_knisa,P.OTO_NO, P.MISPAR_VISA, V.LICENSE_NUMBER , c.shilut,--c.DESCRIPTION
                      TO_CHAR(  nvl( pkg_reports.fun_get_teur_nesia(P.MAKAT_NESIA,p.mispar_knisa, C.DESCRIPTION), P.TEUR_NESIA)) DESCRIPTION
                     --  C.DESCRIPTION
                     
                from tb_sidurim_ovdim s,tb_peilut_ovdim p,VEHICLE_SPECIFICATIONS v, makatTB, tmp_catalog c 
          
                where
                          s.taarich between P_STARTDATE and P_ENDDATE
                    and S.MISPAR_ISHI = P.MISPAR_ISHI(+)
                    and S.TAARICH = P.TAARICH(+)
                    and S.MISPAR_SIDUR=P.MISPAR_SIDUR(+)
                    and S.SHAT_HATCHALA=P.SHAT_HATCHALA_SIDUR(+)
                    and P.OTO_NO = V.BUS_NUMBER(+)
                    and p.makat_nesia= C.MAKAT8(+)
                    and p.taarich = C.ACTIVITY_DATE(+)
                    and (P_SIDURNUMBER is null  or  S.MISPAR_SIDUR in   (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_SIDURNUMBER,  ',') AS MYTABTYPE)))   )
                    and (P_Makat is null or LPAD(p.MAKAT_NESIA,8,'0') like  makatTB.x || '%' )  
                    and (P_MISPARVISA is null or P.MISPAR_VISA in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MISPARVISA,  ',') AS MYTABTYPE)))   )
                    and (P_CARNUMBER is null or P.OTO_NO in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_CARNUMBER,  ',') AS MYTABTYPE)))   )
                    and (P_RISHUYCAR is null  or V.LICENSE_NUMBER  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_RISHUYCAR,  ',') AS MYTABTYPE)))   )
                    and (P_WORKERID is null or S.MISPAR_ISHI in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_WORKERID,  ',') AS MYTABTYPE)))   )  
                )
        , sidurH as(
                select s.taarich,DAYOFWEEK( s.taarich) yom,S.MISPAR_ISHI , S.MISPAR_SIDUR, S.SHAT_HATCHALA,to_char( S.SHAT_HATCHALA,'dd/mm/yyyy HH24:mi') SHAT_HATCHALA_CHR,
                           to_char( S.SHAT_GMAR,'dd/mm/yyyy HH24:mi')  SHAT_GMAR_CHR,S.LO_LETASHLUM, S.SUG_SIDUR,
                          P.MAKAT_NESIA, P.SHAT_YETZIA, to_char( P.SHAT_YETZIA,'dd/mm/yyyy HH24:mi') SHAT_YETZIA_CHR,p.mispar_knisa,P.OTO_NO, P.MISPAR_VISA,  V.LICENSE_NUMBER ,p.shilut,P.TEUR_NESIA DESCRIPTION
                from history_sidurim_ovdim s,history_peilut_ovdim p,VEHICLE_SPECIFICATIONS v, makatTB
          
                where
                          s.taarich between P_STARTDATE and P_ENDDATE
                    and S.MISPAR_ISHI = P.MISPAR_ISHI(+)
                    and S.TAARICH = P.TAARICH(+)
                    and S.MISPAR_SIDUR=P.MISPAR_SIDUR(+)
                    and S.SHAT_HATCHALA=P.SHAT_HATCHALA_SIDUR(+)
                    and P.OTO_NO = V.BUS_NUMBER(+)
                    and (P_SIDURNUMBER is null  or  S.MISPAR_SIDUR in   (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_SIDURNUMBER,  ',') AS MYTABTYPE)))   )
                    and (P_Makat is null or LPAD(p.MAKAT_NESIA,8,'0') like  makatTB.x || '%' )  
                    and (P_MISPARVISA is null or P.MISPAR_VISA in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MISPARVISA,  ',') AS MYTABTYPE)))   )
                    and (P_CARNUMBER is null or P.OTO_NO in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_CARNUMBER,  ',') AS MYTABTYPE)))   )
                    and (P_RISHUYCAR is null  or V.LICENSE_NUMBER  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_RISHUYCAR,  ',') AS MYTABTYPE)))   )
                    and (P_WORKERID is null or S.MISPAR_ISHI in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_WORKERID,  ',') AS MYTABTYPE)))   )
                
                )
         select *
         from(
                 select s.*,O.SHEM_PRAT || ' ' || O.SHEM_MISH shem,E.TEUR_EZOR,M.TEUR_MAAMAD_HR, F.TEUR_SNIF_AV,
                         case when  LENGTH(s.MISPAR_SIDUR)=5 AND substr(s.MISPAR_SIDUR,0,2)='99' then (select SM.TEUR_SIDUR_MEYCHAD from ctb_sidurim_meyuchadim sm where s.MISPAR_SIDUR = SM.KOD_SIDUR_MEYUCHAD )
                                 else (select SS.TEUR_SIDUR_AVODA from ctb_sug_sidur ss where s.SUG_SIDUR = SS.KOD_SIDUR_AVODA ) end teur_sidur
                 from sidur s,  ovdim o,pivot_pirtey_ovdim_hist po, ctb_ezor e  ,ctb_maamad m,ctb_snif_av f      
                  where  s.mispar_ishi= o.mispar_ishi
                      and s.mispar_ishi= po.mispar_ishi
                      and s.taarich between PO.ME_TARICH and PO.AD_TARICH
                      and po.ezor= E.KOD_EZOR
                      and E.KOD_HEVRA=O.KOD_HEVRA
                      and po.maamad= M.KOD_MAAMAD_HR
                      and m.KOD_HEVRA=O.KOD_HEVRA
                      and po.snif_av= f.kod_snif_av
                      and f.KOD_HEVRA=O.KOD_HEVRA
                      and (P_EZOR is null or  Po.EZOR  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_EZOR,  ',') AS MYTABTYPE))) )
                      and (P_SNIF is null or Po.SNIF_AV  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_SNIF,  ',') AS MYTABTYPE))) )
                      and (P_MAMAD is null or Po.MAAMAD  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MAMAD,  ',') AS MYTABTYPE))) )
              
            union all
            
              select s.*,O.SHEM_PRAT || ' ' || O.SHEM_MISH shem,E.TEUR_EZOR,M.TEUR_MAAMAD_HR, F.TEUR_SNIF_AV, 
                         case when  LENGTH(s.MISPAR_SIDUR)=4 AND substr(s.MISPAR_SIDUR,0,2)='85' then (select SM.TEUR_SIDUR_MEYCHAD from ctb_sidurim_meyuchadim sm where s.MISPAR_SIDUR = SM.KOD_SIDUR_MEYUCHAD_yashan )
                                else (select SS.TEUR_SIDUR_AVODA from ctb_sug_sidur ss where s.SUG_SIDUR = SS.KOD_SIDUR_AVODA ) end teur_sidur
                    from sidurH s,  ovdim_history o,pivot_pirtey_ovdim_hist po, ctb_ezor e  ,ctb_maamad m,ctb_snif_av f      
                  where  s.mispar_ishi= o.mispar_ishi
                      and s.mispar_ishi= po.mispar_ishi
                      and s.taarich between PO.ME_TARICH and PO.AD_TARICH
                      and po.ezor= E.KOD_EZOR
                      and E.KOD_HEVRA=O.KOD_HEVRA
                      and po.maamad= M.KOD_MAAMAD_HR
                      and m.KOD_HEVRA=O.KOD_HEVRA
                      and po.snif_av= f.kod_snif_av
                      and f.KOD_HEVRA=O.KOD_HEVRA
                      and (P_EZOR is null or  Po.EZOR  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_EZOR,  ',') AS MYTABTYPE))) )
                      and (P_SNIF is null or Po.SNIF_AV  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_SNIF,  ',') AS MYTABTYPE))) )
                      and (P_MAMAD is null or Po.MAAMAD  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MAMAD,  ',') AS MYTABTYPE))) )
              )
  order by mispar_ishi,taarich,shat_hatchala, SHAT_YETZIA,mispar_knisa;
        
  
   EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;           
end   pro_get_Find_Worker_Card_2;       

PROCEDURE pro_get_Find_Worker_Card_2( p_cur OUT CurType, 
                                                                P_STARTDATE IN DATE,
                                                                P_ENDDATE IN DATE , 
                                                                P_SIDURNUMBER IN VARCHAR2,
                                                                P_Makat IN VARCHAR2,
                                                                P_MISPARVISA IN VARCHAR2, 
                                                                P_CARNUMBER IN VARCHAR2,
                                                                P_RISHUYCAR   IN  VARCHAR2,
                                                                P_WORKERID  IN  VARCHAR2,
                                                                P_EZOR  IN VARCHAR2,                                                               
                                                                P_SNIF IN VARCHAR2,
                                                                P_MAMAD IN VARCHAR2
                                                                 ) AS
                                                                 
      GeneralQry VARCHAR2(32767);
      QryMakatDate VARCHAR2(3500);
       rc NUMBER ;        
       p_lst_makat   VARCHAR2(300);        
       ParamQry       VARCHAR2(300);                                              
begin
    
 if (P_Makat is null) then
     p_lst_makat:='-1';
     else p_lst_makat:=P_Makat;
  end if;

    QryMakatDate:='
                    with makatTB as(
                     (SELECT X FROM TABLE(CAST(Convert_String_To_Table( ''' || p_lst_makat ||  ''',  '','') AS MYTABTYPE))) 
                    )
                    Select  distinct p.makat_nesia,p.TAARICH  
                    FROM TB_PEILUT_OVDIM p
                       where  p.MISPAR_SIDUR not like ''99%''
                      AND p.taarich BETWEEN  ''' || P_STARTDATE  || ''' AND ''' ||  P_ENDDATE  || '''';

        IF (( P_WORKERID IS NOT  NULL ) ) THEN 
            ParamQry := ParamQry || ' AND  p.mispar_ishi in (' || P_WORKERID || ')  ';
        END IF ;  
        IF (( P_Makat IS NOT  NULL )) THEN 
            ParamQry := ParamQry || ' AND LPAD(p.MAKAT_NESIA,8,''0'') like  makatTB.x || ''%'' ) ';-- ||  Prepare_Like_Of_List('p.makat_nesia', P_Makat,'''')  ;
        END IF ; 
        IF (( P_SIDURNUMBER IS NOT  NULL )) THEN 
            ParamQry := ParamQry ||  ' AND  p.mispar_sidur in (' || P_SIDURNUMBER || ') ';
        END IF ; 

        IF (( ParamQry IS NOT NULL  ) OR ( ParamQry <> '')) THEN 
         GeneralQry := GeneralQry  || ParamQry;
        END IF ;

  --QryMakatDate :=  fct_MakatDateSql(P_STARTDATE ,P_ENDDATE  ,P_Makat ,P_SIDURNUMBER ,P_WORKERID  );
   pro_Prepare_Catalog_Details(QryMakatDate);
  COMMIT ; 
   
   if (P_Makat is null) then
     p_lst_makat:='-1';
     else p_lst_makat:=P_Makat;
  end if;
  
    OPEN p_cur FOR     
            with makatTB as(
                     (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_lst_makat,  ',') AS MYTABTYPE))) 
            )
           , sidur as(
                select s.taarich,DAYOFWEEK( s.taarich) yom,S.MISPAR_ISHI , S.MISPAR_SIDUR, S.SHAT_HATCHALA,to_char( S.SHAT_HATCHALA,'dd/mm/yyyy HH24:mi') SHAT_HATCHALA_CHR  ,
                          to_char( S.SHAT_GMAR,'dd/mm/yyyy HH24:mi') SHAT_GMAR,S.LO_LETASHLUM, S.SUG_SIDUR,
                          P.MAKAT_NESIA, P.SHAT_YETZIA, to_char( P.SHAT_YETZIA,'dd/mm/yyyy HH24:mi') SHAT_YETZIA_CHR , p.mispar_knisa,P.OTO_NO, P.MISPAR_VISA, V.LICENSE_NUMBER , c.shilut,--c.DESCRIPTION
                      TO_CHAR(  nvl( pkg_reports.fun_get_teur_nesia(P.MAKAT_NESIA,p.mispar_knisa, C.DESCRIPTION), P.TEUR_NESIA)) DESCRIPTION
                     --  C.DESCRIPTION
                     
                from tb_sidurim_ovdim s,tb_peilut_ovdim p,VEHICLE_SPECIFICATIONS v, makatTB, tmp_catalog c 
          
                where
                          s.taarich between P_STARTDATE and P_ENDDATE
                    and S.MISPAR_ISHI = P.MISPAR_ISHI(+)
                    and S.TAARICH = P.TAARICH(+)
                    and S.MISPAR_SIDUR=P.MISPAR_SIDUR(+)
                    and S.SHAT_HATCHALA=P.SHAT_HATCHALA_SIDUR(+)
                    and P.OTO_NO = V.BUS_NUMBER(+)
                    and p.makat_nesia= C.MAKAT8(+)
                    and p.taarich = C.ACTIVITY_DATE(+)
                    and (P_SIDURNUMBER is null  or  S.MISPAR_SIDUR in   (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_SIDURNUMBER,  ',') AS MYTABTYPE)))   )
                    and (P_Makat is null or LPAD(p.MAKAT_NESIA,8,'0') like  makatTB.x || '%' )  
                    and (P_MISPARVISA is null or P.MISPAR_VISA in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MISPARVISA,  ',') AS MYTABTYPE)))   )
                    and (P_CARNUMBER is null or P.OTO_NO in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_CARNUMBER,  ',') AS MYTABTYPE)))   )
                    and (P_RISHUYCAR is null  or V.LICENSE_NUMBER  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_RISHUYCAR,  ',') AS MYTABTYPE)))   )
                    and (P_WORKERID is null or S.MISPAR_ISHI in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_WORKERID,  ',') AS MYTABTYPE)))   )  
                )
        , sidurH as(
                select s.taarich,DAYOFWEEK( s.taarich) yom,S.MISPAR_ISHI , S.MISPAR_SIDUR, S.SHAT_HATCHALA,to_char( S.SHAT_HATCHALA,'dd/mm/yyyy HH24:mi') SHAT_HATCHALA_CHR,
                           to_char( S.SHAT_GMAR,'dd/mm/yyyy HH24:mi')  SHAT_GMAR_CHR,S.LO_LETASHLUM, S.SUG_SIDUR,
                          P.MAKAT_NESIA, P.SHAT_YETZIA, to_char( P.SHAT_YETZIA,'dd/mm/yyyy HH24:mi') SHAT_YETZIA_CHR,p.mispar_knisa,P.OTO_NO, P.MISPAR_VISA,  V.LICENSE_NUMBER ,p.shilut,P.TEUR_NESIA DESCRIPTION
                from history_sidurim_ovdim s,history_peilut_ovdim p,VEHICLE_SPECIFICATIONS v, makatTB
          
                where
                          s.taarich between P_STARTDATE and P_ENDDATE
                    and S.MISPAR_ISHI = P.MISPAR_ISHI(+)
                    and S.TAARICH = P.TAARICH(+)
                    and S.MISPAR_SIDUR=P.MISPAR_SIDUR(+)
                    and S.SHAT_HATCHALA=P.SHAT_HATCHALA_SIDUR(+)
                    and P.OTO_NO = V.BUS_NUMBER(+)
                    and (P_SIDURNUMBER is null  or  S.MISPAR_SIDUR in   (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_SIDURNUMBER,  ',') AS MYTABTYPE)))   )
                    and (P_Makat is null or LPAD(p.MAKAT_NESIA,8,'0') like  makatTB.x || '%' )  
                    and (P_MISPARVISA is null or P.MISPAR_VISA in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MISPARVISA,  ',') AS MYTABTYPE)))   )
                    and (P_CARNUMBER is null or P.OTO_NO in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_CARNUMBER,  ',') AS MYTABTYPE)))   )
                    and (P_RISHUYCAR is null  or V.LICENSE_NUMBER  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_RISHUYCAR,  ',') AS MYTABTYPE)))   )
                    and (P_WORKERID is null or S.MISPAR_ISHI in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_WORKERID,  ',') AS MYTABTYPE)))   )
                
                )
         select *
         from(
                 select s.*,O.SHEM_PRAT || ' ' || O.SHEM_MISH shem,E.TEUR_EZOR,M.TEUR_MAAMAD_HR, F.TEUR_SNIF_AV,
                         case when  LENGTH(s.MISPAR_SIDUR)=5 AND substr(s.MISPAR_SIDUR,0,2)='99' then (select SM.TEUR_SIDUR_MEYCHAD from ctb_sidurim_meyuchadim sm where s.MISPAR_SIDUR = SM.KOD_SIDUR_MEYUCHAD )
                                 else (select SS.TEUR_SIDUR_AVODA from ctb_sug_sidur ss where s.SUG_SIDUR = SS.KOD_SIDUR_AVODA ) end teur_sidur
                 from sidur s,  ovdim o,pivot_pirtey_ovdim_hist po, ctb_ezor e  ,ctb_maamad m,ctb_snif_av f      
                  where  s.mispar_ishi= o.mispar_ishi
                      and s.mispar_ishi= po.mispar_ishi
                      and s.taarich between PO.ME_TARICH and PO.AD_TARICH
                      and po.ezor= E.KOD_EZOR
                      and E.KOD_HEVRA=O.KOD_HEVRA
                      and po.maamad= M.KOD_MAAMAD_HR
                      and m.KOD_HEVRA=O.KOD_HEVRA
                      and po.snif_av= f.kod_snif_av
                      and f.KOD_HEVRA=O.KOD_HEVRA
                      and (P_EZOR is null or  Po.EZOR  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_EZOR,  ',') AS MYTABTYPE))) )
                      and (P_SNIF is null or Po.SNIF_AV  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_SNIF,  ',') AS MYTABTYPE))) )
                      and (P_MAMAD is null or Po.MAAMAD  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MAMAD,  ',') AS MYTABTYPE))) )
              
            union all
            
              select s.*,O.SHEM_PRAT || ' ' || O.SHEM_MISH shem,E.TEUR_EZOR,M.TEUR_MAAMAD_HR, F.TEUR_SNIF_AV, 
                         case when  LENGTH(s.MISPAR_SIDUR)=4 AND substr(s.MISPAR_SIDUR,0,2)='85' then (select SM.TEUR_SIDUR_MEYCHAD from ctb_sidurim_meyuchadim sm where s.MISPAR_SIDUR = SM.KOD_SIDUR_MEYUCHAD_yashan )
                                else (select SS.TEUR_SIDUR_AVODA from ctb_sug_sidur ss where s.SUG_SIDUR = SS.KOD_SIDUR_AVODA ) end teur_sidur
                    from sidurH s,  ovdim_history o,pivot_pirtey_ovdim_hist po, ctb_ezor e  ,ctb_maamad m,ctb_snif_av f      
                  where  s.mispar_ishi= o.mispar_ishi
                      and s.mispar_ishi= po.mispar_ishi
                      and s.taarich between PO.ME_TARICH and PO.AD_TARICH
                      and po.ezor= E.KOD_EZOR
                      and E.KOD_HEVRA=O.KOD_HEVRA
                      and po.maamad= M.KOD_MAAMAD_HR
                      and m.KOD_HEVRA=O.KOD_HEVRA
                      and po.snif_av= f.kod_snif_av
                      and f.KOD_HEVRA=O.KOD_HEVRA
                      and (P_EZOR is null or  Po.EZOR  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_EZOR,  ',') AS MYTABTYPE))) )
                      and (P_SNIF is null or Po.SNIF_AV  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_SNIF,  ',') AS MYTABTYPE))) )
                      and (P_MAMAD is null or Po.MAAMAD  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MAMAD,  ',') AS MYTABTYPE))) )
              )
  order by mispar_ishi,taarich,shat_hatchala, SHAT_YETZIA,mispar_knisa;
        
  
   EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;           
end   pro_get_Find_Worker_Card_2;                 
              */                                                   
function fun_get_teur_nesia(p_makat number,p_mispar_knisa number,p_teur_tnua nvarchar2 )return nvarchar2 is
 p_teur nvarchar2(100);
begin 
       p_teur:=null;
        if (p_teur_tnua is not null and p_mispar_knisa=0) then
            p_teur:=p_teur_tnua;
        end if;
            
        if (length(p_makat)=8 and substr(p_makat,0,3) <> '700' and substr(p_makat,0,1) = '7') then
            select E.TEUR_ELEMENT into p_teur
            from ctb_elementim e
            where E.KOD_ELEMENT = to_number(substr(p_makat,2,2));
        end if;
        
           if (length(p_makat)=8 and substr(p_makat,0,3) = '700') then
            select t.TEUR_NEKUDAT_TIFUL  into p_teur
            from CTB_NKUDUT_TIFAUL t
            where  to_number(substr(t.kod_nekudat_tiful,2,3))  = to_number(substr(p_makat,4,3))
               and  t.kod_nekudat_tiful<4000;
        end if;
        return p_teur;
end fun_get_teur_nesia;
  /***********************************/
  PROCEDURE pro_get_Find_Worker_Card(p_cur OUT CurType, 
                                                                P_STARTDATE IN DATE,
                                                                P_ENDDATE IN DATE , 
                                                                P_Makat  IN  VARCHAR2, 
                                                                P_SIDURNUMBER  IN  VARCHAR2,
                                                                P_CARNUMBER  IN  VARCHAR2,
                                                                P_SNIF  IN  VARCHAR2,
                                                                P_WORKSTATION  IN  VARCHAR2,
                                                                P_DAYTYPE  IN  VARCHAR2 ,
                                                                P_WORKERID  IN  VARCHAR2,
                                                                P_RISHUYCAR   IN  VARCHAR2,
                                                                P_SECTORISUK IN VARCHAR2 ,
                                                                P_SNIF_MASHAR IN VARCHAR2 , 
                                                                P_COMPANYID IN VARCHAR2 ,
                                                                P_MAMAD IN VARCHAR2 ,
                                                                P_EZOR IN VARCHAR2 ,
                                                                P_ISUK IN VARCHAR2 
                                                                 ) AS
      lst_sidur_num VARCHAR2(200);
      lst_makat VARCHAR2(200);
      lst_car_num VARCHAR2(200);
      lst_snif VARCHAR2(200);
      lst_mispar_ishi VARCHAR2(200);
      lst_station VARCHAR2(200);
      lst_rishuy VARCHAR2(200);
      lst_day VARCHAR2(20);
      lst_maamad VARCHAR2(20);
      lst_snif_mashar VARCHAR2(200);
      lst_company  VARCHAR2(20);
      lst_ezor  VARCHAR2(20);
      lst_isuk  VARCHAR2(20);
      lst_sector_isuk  VARCHAR2(20);
      GeneralQry VARCHAR2(32767);
      QryMakatDate VARCHAR2(3500);
      rc NUMBER ;                                       
BEGIN
 DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_get_Find_Worker_Card');
 IF (( P_Makat IS   NULL ) or ( P_Makat = '' )) THEN 
    lst_makat:='-1';
   else lst_makat:=P_Makat;
END IF ;
 IF (( P_SIDURNUMBER IS NULL) or  (P_SIDURNUMBER= '') ) THEN 
   lst_sidur_num:='-1';
   else   lst_sidur_num:=P_SIDURNUMBER;
END IF ;
 IF (( P_CARNUMBER IS   NULL ) or ( P_CARNUMBER  =  '' )) THEN 
    lst_car_num:='-1';
  else lst_car_num:=P_CARNUMBER;
END IF ;
 IF (( P_SNIF IS  NULL ) or ( P_SNIF = '' )) THEN 
      lst_snif:='-1';
 else   lst_snif:=P_SNIF;
END IF ;
 IF (( P_WORKSTATION IS  NULL ) or ( P_WORKSTATION = '' )) THEN 
     lst_station:='-1';
    else lst_station:=P_WORKSTATION;
END IF ;
 IF (( P_DAYTYPE IS  NULL ) or ( P_DAYTYPE = '' )) THEN 
    lst_day:='-1';
   else lst_day:='�';
END IF ;
 IF (( P_WORKERID IS NULL) or  (P_WORKERID= '') ) THEN 
   lst_mispar_ishi:='-1';
   else   lst_mispar_ishi:=P_WORKERID;
END IF ;
 IF (( P_RISHUYCAR IS  NULL ) or ( P_RISHUYCAR = '' )) THEN 
    lst_rishuy:='-1';
  else lst_rishuy:=P_RISHUYCAR;
END IF ;
 IF (( P_COMPANYID IS  NULL ) or ( P_COMPANYID = '' )) THEN 
    lst_company:='-1';
  else lst_company:=P_COMPANYID;
END IF ;
 IF (( P_SECTORISUK IS  NULL ) or ( P_SECTORISUK = '' )) THEN 
    lst_sector_isuk:='-1';
  else lst_sector_isuk:=P_SECTORISUK;
END IF ;
 IF (( P_SNIF_MASHAR IS  NULL ) or ( P_SNIF_MASHAR = '' )) THEN 
    lst_snif_mashar:='-1';
  else lst_snif_mashar:=P_SNIF_MASHAR;
END IF ;
 IF (( P_MAMAD IS  NULL ) or ( P_MAMAD = '' )) THEN 
    lst_maamad:='-1';
  else lst_maamad:=P_MAMAD;
END IF ;
 IF (( P_EZOR IS  NULL ) or ( P_EZOR = '' )) THEN 
    lst_ezor:='-1';
  else lst_ezor:=P_EZOR;
END IF ;
 IF (( P_ISUK IS  NULL ) or ( P_ISUK = '' )) THEN 
    lst_isuk:='-1';
  else lst_isuk:=P_ISUK;
END IF ;
DBMS_OUTPUT.PUT_LINE ( lst_mispar_ishi);
  INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'xxx:', Prepare_Like_Of_List('s.MISPAR_SIDUR', lst_sidur_num,NULL) );
QryMakatDate :=  fct_MakatDateSql(P_STARTDATE ,P_ENDDATE  ,P_Makat ,P_SIDURNUMBER ,P_WORKERID  );
 pro_Prepare_Catalog_Details(QryMakatDate);
 COMMIT ; 
 GeneralQry := 'select   substr(Details.EZOR,0,1) || substr(Details.MAAMAD,0,1) || ''-'' ||  s.MISPAR_ISHI Misparishi,o.shem_mish|| '' '' ||  o.shem_prat full_name,
         S.TAARICH,S.MISPAR_SIDUR,S.SHAT_HATCHALA,S.SHAT_GMAR,
         case when  to_number(trim(S.meadken_acharon)) < 0 then null when to_number(trim(S.meadken_acharon)) > 0 then S.meadken_acharon  end MEADKEN_ACHARON,
         S.CHARIGA,S.HASHLAMA,S.OUT_MICHSA,
        M.TEUR_SIDUR_MEYCHAD,EZOR.TEUR_EZOR,MAAMAD.TEUR_MAAMAD_HR,SNIF.TEUR_SNIF_AV,ISUK.TEUR_ISUK,s.SNIF_TNUA,
       s.MAKAT_NESIA,s.MISPAR_KNISA,s.SHAT_YETZIA,s.OTO_NO,MASHAR.LICENSE_NUMBER,s.Dayofweek,MASHAR.bus_number,
       Catalog.DESCRIPTION ,Catalog.SHILUT ,
       decode(IsElement,''true'',Pkg_Elements.getDakotByMakatElement(s.MAKAT_NESIA) , Catalog.MAZAN_TASHLUM) MAZAN_TASHLUM,
       decode(IsElement,''true'',TO_NUMBER( TO_CHAR(Pkg_Elements.getKmByMakatElement(s.MAKAT_NESIA),''999.99'') ) , Catalog.KM) KM,
       Catalog.NIHUL_NAME ,Catalog.ACTIVITY_DATE  , Catalog.MAKAT8
from (select S.mispar_ishi,S.TAARICH,Dayofweek(s.taarich) Dayofweek ,S.MISPAR_SIDUR,S.SHAT_HATCHALA,S.SHAT_GMAR,S.MEADKEN_ACHARON, S.CHARIGA,S.HASHLAMA,S.OUT_MICHSA,
                  P.SNIF_TNUA, P.MAKAT_NESIA,P.MISPAR_KNISA,P.SHAT_YETZIA,P.OTO_NO,
                  Pkg_Elements.IsElementZmanAnddNesia(P.MAKAT_NESIA,''true'') IsElement
         from  tb_sidurim_ovdim s,TB_PEILUT_OVDIM p
         where    S.MISPAR_ISHI = P.MISPAR_ISHI(+)
                and S.TAARICH = P.TAARICH(+)
                and S.SHAT_HATCHALA = P.SHAT_HATCHALA_SIDUR(+)
                and S.MISPAR_SIDUR = P.MISPAR_SIDUR(+) 
                and  s.taarich     BETWEEN   ''' ||  P_STARTDATE  || '''   AND  ''' ||  P_ENDDATE  || '''
               and (''' || lst_mispar_ishi || '''=''-1'' or s.MISPAR_ISHI in(' ||lst_mispar_ishi || ')) 
                and (''' || lst_sidur_num || '''=''-1'' or  ' || Prepare_Like_Of_List('s.MISPAR_SIDUR', lst_sidur_num,NULL) ||')
              and (''' || lst_station|| '''=''-1'' or  p.SNIF_TNUA in (' || lst_station || '))
              and (''' || lst_car_num || '''=''-1'' or  p.oto_no in (' || lst_car_num || '))       
       ) s ,
        Ovdim o,
        ctb_sidurim_meyuchadim m,
       CTB_SNIF_AV Snif ,
       CTB_MAAMAD   Maamad ,
       CTB_ISUK Isuk ,
       Ctb_Ezor Ezor,
       TMP_CATALOG Catalog,   
         VEHICLE_SPECIFICATIONS Mashar,
     ( SELECT * 
        FROM ( SELECT   t.mispar_ishi , 
                                               MAX( t.ME_TARICH) OVER (PARTITION BY t.mispar_ishi) me_tarich,
                                               t.ad_tarich,T.SNIF_AV,t.EZOR,t.MAAMAD,t.ISUK, 
                                               row_number() OVER (PARTITION BY t.mispar_ishi ORDER BY me_tarich desc) seq
                            FROM  PIVOT_PIRTEY_OVDIM t
                            WHERE t.isuk IS NOT NULL 
                                AND (''' ||  P_STARTDATE  || '''  BETWEEN ME_TARICH  AND NVL(t.ad_TARICH,TO_DATE( ''01/01/9999'' , ''dd/mm/yyyy'' ))
                                     OR  ''' ||  P_ENDDATE  || '''  BETWEEN ME_TARICH AND NVL(t.ad_TARICH,TO_DATE( ''01/01/9999'' , ''dd/mm/yyyy'' )) 
                                     OR t.ME_TARICH>=''' ||  P_STARTDATE  || '''  AND NVL(t.ad_TARICH,TO_DATE( ''01/01/9999'' ,''dd/mm/yyyy'' ))<=  ''' ||  P_ENDDATE  || ''')
                               and (''' ||lst_mispar_ishi || '''=''-1'' or t.MISPAR_ISHI in(' ||lst_mispar_ishi || '))
                               and (''' ||lst_ezor ||'''=''-1'' or t.EZOR  in ('|| lst_ezor||' ))
                                and (''' ||lst_isuk ||'''=''-1'' or t.ISUK  in ('|| lst_isuk||' ))
                                and (''' || lst_maamad  || '''=''-1'' or   ' || Prepare_Like_Of_List('t.MAAMAD', lst_maamad,NULL) ||')        
                                and (''' || lst_snif|| '''=''-1'' or  t.SNIF_AV in (' || lst_snif || '))
                                 ) a
        WHERE a.seq=1 ) Details
  where s.MISPAR_ISHI = O.MISPAR_ISHI
   and M.KOD_SIDUR_MEYUCHAD(+) =  S.MISPAR_SIDUR
   and Details.mispar_ishi = s.MISPAR_ISHI
   and  SNIF.KOD_SNIF_AV = Details.SNIF_AV
   and SNIF.KOD_HEVRA =O.KOD_HEVRA
   and MAAMAD.KOD_MAAMAD_HR = Details.MAAMAD
   and MAAMAD.KOD_HEVRA =O.KOD_HEVRA
   and ISUK.KOD_ISUK = Details.ISUK
   and ISUK.KOD_HEVRA=O.KOD_HEVRA
   and EZOR.KOD_EZOR = Details.EZOR
   and EZOR.KOD_HEVRA=O.KOD_HEVRA
   and MASHAR.BUS_NUMBER(+) = s.OTO_NO
   and Catalog.makat8(+) = s.makat_nesia
   and CATALOG.ACTIVITY_DATE(+) =s.TAARICH
  and (''' ||lst_day || '''=''-1'' or s.Dayofweek=''' || lst_day || ''')
  and (''' ||lst_rishuy ||'''=''-1'' or MASHAR.LICENSE_NUMBER in ('|| lst_rishuy||' ))
   and  (''' ||lst_makat || '''=''-1'' or  ' || Prepare_Like_Of_List('s.MAKAT_NESIA', lst_makat,'%') ||')
   and (''' ||lst_sector_isuk ||'''=''-1'' or ISUK.KOD_SECTOR_ISUK  in ('|| lst_sector_isuk||' ))
  and (''' ||lst_company ||'''=''-1'' or o.KOD_HEVRA in ('|| lst_company||' )) ';

 GeneralQry := GeneralQry || ' order by s.MISPAR_ISHI  , s.taarich  , s.shat_hatchala, s.shat_yetzia';
             DBMS_OUTPUT.PUT_LINE (GeneralQry);


OPEN p_cur FOR GeneralQry ;
                                                           
        EXCEPTION 
         WHEN OTHERS THEN 
             DBMS_OUTPUT.PUT_LINE (SQLERRM);
              RAISE;               
  END  pro_get_Find_Worker_Card;  
  
  /********************************/
    FUNCTION fct_MakatDateSql    (  P_STARTDATE IN DATE,
                                                P_ENDDATE IN DATE , 
                                                P_Makat IN VARCHAR,
                                                P_SIDURNUMBER IN VARCHAR,
                                                P_WORKERID IN VARCHAR ) RETURN VARCHAR AS
GeneralQry VARCHAR2(3000);
ParamQry VARCHAR2(1000);
BEGIN 

GeneralQry:= 'Select   distinct activity.makat_nesia,ACTIVITY.TAARICH  
                    FROM TB_PEILUT_OVDIM Activity, TB_SIDURIM_OVDIM so 
                    WHERE  (so.mispar_ishi          = activity.mispar_ishi)  
                      AND (so.mispar_sidur  = activity.mispar_sidur)
                      AND (so.shat_hatchala = activity.shat_hatchala_sidur) 
                      AND (so.taarich           = activity.taarich)  
                       and SO.MISPAR_SIDUR not like ''99%''
                      AND so.taarich BETWEEN  ''' || P_STARTDATE  || ''' AND ''' ||  P_ENDDATE  || '''';
 
IF (( P_WORKERID IS NOT  NULL ) OR ( P_WORKERID <> '' )) THEN 
    ParamQry := ParamQry || '  so.mispar_ishi in (' || P_WORKERID || ') AND ';
END IF ;  
IF (( P_Makat IS NOT  NULL ) OR ( P_Makat <> '' )) THEN 
    ParamQry := ParamQry ||  Prepare_Like_Of_List('Activity.makat_nesia(+)', P_Makat,'''')  || ' AND ';
END IF ; 
IF (( P_SIDURNUMBER IS NOT  NULL ) OR ( P_SIDURNUMBER <> '' )) THEN 
    ParamQry := ParamQry ||  Prepare_Like_Of_List('Activity.mispar_sidur(+)', P_SIDURNUMBER,'''')  || ' AND ';
END IF ; 
IF (( ParamQry IS NOT NULL  ) OR ( ParamQry <> '')) THEN 
  ParamQry := SUBSTR(ParamQry,0,LENGTH(ParamQry)-4); -- TO DELETE THE LAST 'AND ' 
  GeneralQry := GeneralQry || 'And ' || ParamQry;
END IF ;
 
RETURN GeneralQry ; 

EXCEPTION 
WHEN OTHERS THEN 
  RAISE;               
  END  fct_MakatDateSql;   
    
  PROCEDURE pro_Prepare_Catalog_Details ( GeneralQry IN VARCHAR ) AS
CountQry VARCHAR2(3000); 
InsertQry VARCHAR2(3000); 
rc NUMBER ;                                                                
CountRows NUMBER ;
BEGIN 
DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_Prepare_Catalog_Details');
DBMS_OUTPUT.PUT_LINE (GeneralQry);
EXECUTE IMMEDIATE  'truncate table tmp_Catalog' ;     
CountQry := 'Select  nvl(count(*),0)  from (' || GeneralQry || ')'  ;  
EXECUTE IMMEDIATE CountQry INTO CountRows  ; 
DBMS_OUTPUT.PUT_LINE ('CountRows'  || CountRows);
IF (CountRows > 0 ) THEN 
 -- DBMS_OUTPUT.PUT_LINE (CountRows);
    InsertQry := 'INSERT INTO  kds.TMP_CATALOG_DETAILS@KDS_GW_AT_TNPR(makat8,activity_date) ' || GeneralQry  ;
    -- DBMS_OUTPUT.PUT_LINE (InsertQry);                            
    EXECUTE IMMEDIATE InsertQry ;
    --DBMS_OUTPUT.PUT_LINE ('6:'  || to_char(sysdate,'HH24:mi:ss'));
    -- Get the others details requiered from kds_gw_at_tnpr
    kds_catalog_pack.GetKavimDetails@KDS_GW_AT_TNPR(rc);
    --DBMS_OUTPUT.PUT_LINE ('7:'  || to_char(sysdate,'HH24:mi:ss'));
    -- copy the data from kds_gw_at_tnpr to local tmp_Catalog ( which trunkated on preserve - at the end of the session )
    INSERT INTO TMP_CATALOG( activity_date,makat8, Shilut,Description,nihul_name,mazan_tashlum,mazan_tichnun,Km,sug_shirut_name,eilat_trip,onatiut,kisuy_tor,eshel,migun,xy_moked_tchila,xy_moked_siyum,snif,snif_name,sug_auto ) 
    SELECT activity_date, makat8, Shilut,Description,nihul_name,mazan_tashlum,mazan_tichnun,Km,sug_shirut_name,eilat_trip,onatiut,kisui_tor,eshel,migun,xy_moked_tchila,xy_moked_siyum,snif,snif_name,sug_auto   FROM kds.TMP_CATALOG_DETAILS@KDS_GW_AT_TNPR;
  COMMIT ; 
 END IF ; 

EXCEPTION 
WHEN OTHERS THEN 
rollback;
  RAISE;               
  END  pro_Prepare_Catalog_Details;   
    
   PROCEDURE pro_get_Presence (p_cur OUT Curtype ,
                                                  P_MISPAR_ISHI IN VARCHAR2 ,
                                                   P_STARTDATE IN DATE,
                                                   P_ENDDATE IN DATE,
                                                    P_SNIF IN VARCHAR2,
                                                   P_WorkerViewLevel IN NUMBER, 
                                                   P_WORKERID IN VARCHAR2
                                                         )AS  
GeneralQry VARCHAR2(32767);
QryMakatDate VARCHAR2(3000);
 ParamQry1 VARCHAR2(1000);
  ParamQry2 VARCHAR2(1000);
  ParamQry3 VARCHAR2(1000);
--   P_STARTDATE DATE;
 -- P_ENDDATE  DATE;
  x number;
BEGIN
DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_get_Presence');

   -- P_STARTDATE:=sysdate;
    --P_ENDDATE:=sysdate;
x:=0;
--TMP_MANAGE_TREE
/*INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_WorkerViewLevel:',P_WorkerViewLevel);
 INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_WORKERID:',P_WORKERID);
 INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_MISPAR_ISHI:',P_MISPAR_ISHI);
 INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_STARTDATE:',P_STARTDATE);
 INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_ENDDATE:',P_ENDDATE);
*/
IF (P_WorkerViewLevel = 5) THEN  
        Pkg_Utils.pro_ins_Manage_Tree(TO_NUMBER(P_WORKERID,999999999));
        select count(*) into x from TMP_MANAGE_TREE;
        DBMS_OUTPUT.PUT_LINE ('TMP_MANAGE_TREE=' || x);
END IF ;

IF ((P_MISPAR_ISHI <> '' ) OR   (P_MISPAR_ISHI IS NOT NULL )) THEN
        ParamQry1 :=  ' AND s.mispar_ishi in (' || P_MISPAR_ISHI || ') '; 
        ParamQry2 :=  ' AND t.mispar_ishi in (' || P_MISPAR_ISHI || ') '; 
         ParamQry3 :=  ' AND y.mispar_ishi in (' || P_MISPAR_ISHI || ') '; 
ELSE
        ParamQry1 :=  ' '; 
        ParamQry2 := ' '; 
         ParamQry3 := ' '; 
END IF;


GeneralQry:= '       WITH sm AS 
                               (   select z.mispar_sidur ,z.meafyen_53
                                                            from
                                                                (select  h.mispar_sidur,
                                                                          max(case  to_number(h.KOD_MEAFYEN)  when 5 then ''5'' else null end ) meafyen_5,
                                                                          max(case  to_number(h.KOD_MEAFYEN)  when 53 then ''53'' else null end ) meafyen_53,
                                                                          max(case  to_number(h.KOD_MEAFYEN)  when 54 then ''54'' else null end ) meafyen_54
                                                                from(          
                                                                SELECT  a.mispar_sidur, a.KOD_MEAFYEN
                                                                 FROM TB_SIDURIM_MEYUCHADIM a
                                                                 WHERE  a.KOD_MEAFYEN IN (5,53,54 ) ) h
                                                                 group by h.mispar_sidur) z
                                                            where   z.meafyen_5 is not null and(   meafyen_53 is not null or meafyen_54 is not null)
                               UNION SELECT 0,null FROM dual ),
                                 headrut as(
                                select M.MISPAR_ISHI, M.TAARICH_HATCHALA, M.TAARICH_SIYUM,   T.KOD_HEADRUT 
                                from ovdim y,   ctb_status t, MATZAV_OVDIM m 
                                where  y.mispar_ishi=M.MISPAR_ISHI
                                     and    t.KOD_STATUS= m.KOD_MATZAV
                                         and y.KOD_HEVRA = T.KOD_HEVRA    
                                                      AND (  ''' || P_STARTDATE  || '''  BETWEEN M.TAARICH_HATCHALA  AND NVL(M.TAARICH_SIYUM,TO_DATE(  ''01/01/9999''  ,  ''dd/mm/yyyy'' ))
                                                             OR  ''' || P_ENDDATE  || '''   BETWEEN M.TAARICH_HATCHALA AND NVL(M.TAARICH_SIYUM ,TO_DATE(  ''01/01/9999''  ,  ''dd/mm/yyyy''  )) 
                                                             OR M.TAARICH_HATCHALA>= ''' || P_STARTDATE  || '''  AND NVL(M.TAARICH_SIYUM,TO_DATE(  ''01/01/9999''  , ''dd/mm/yyyy''  ))<= ''' || P_ENDDATE  || '''  ) ';
                                  GeneralQry := GeneralQry || ParamQry3; 
                                  GeneralQry := GeneralQry || '  
                                )          ,
                                
                     cnt_sidurim_beyom as  
                   ( select  s.mispar_ishi,s.taarich ,count(*) num
                        from tb_sidurim_ovdim s
                        where     s.taarich between ''' ||  P_STARTDATE  || '''  and ''' ||  P_ENDDATE  || ''' ' ;
                    GeneralQry := GeneralQry || ParamQry1; 
                  GeneralQry := GeneralQry || '         group by mispar_ishi,taarich  ),
                   
              cnt_others_sidurim  as  
                      ( select  s.mispar_ishi,s.taarich ,count(*) num
                        from tb_sidurim_ovdim s
                        where     s.taarich between ''' ||  P_STARTDATE  || '''  and ''' ||  P_ENDDATE  || ''' ' ;
             GeneralQry := GeneralQry || ParamQry1; 
              GeneralQry := GeneralQry || '     and s.mispar_sidur not in(select mispar_sidur from sm)
                        and s.mispar_sidur<>99200
                        group by mispar_ishi,taarich  )
                               
                                   select h.*,Details.full_name ,Details.kod_hevra,
                                         (Pkg_Ovdim.fun_get_meafyen_oved(h.mispar_ishi, 3, ''' ||  P_ENDDATE  || '''  )) START_TIME_ALLOWED ,
                                         (Pkg_Ovdim.fun_get_meafyen_oved(h.mispar_ishi, 4, ''' ||  P_ENDDATE  || ''' )) END_TIME_ALLOWED  ,
                                Details.teur_isuk ,Details.Teur_ezor,Details.teur_maamad_hr,Details.teur_snif_av ,headrut.KOD_HEADRUT,
                               pkg_reports.get_heara_doch_nochechut(h.mispar_ishi,h.taarich,h.mispar_sidur,h.lo_letashlum,h.shat_hatchala, h.shat_gmar ,headrut.KOD_HEADRUT,
                                                                                        nvl(cnt_sidurim_beyom.num,0), nvl(cnt_others_sidurim.num,0),
                                                                                        nvl( h.meafyen_53,0)) heara
                       
                            from   cnt_sidurim_beyom, cnt_others_sidurim,headrut, ( SELECT a.* ,  isuk.teur_isuk ,Ezor.Teur_ezor,Maamad.teur_maamad_hr,snif.teur_snif_av 
                                    FROM 
                                             CTB_EZOR Ezor ,
                                             CTB_MAAMAD Maamad, 
                                             CTB_ISUK Isuk,
                                             CTB_SNIF_AV Snif,   
                                             ( SELECT   t.mispar_ishi ,  min( t.ME_TARICH) OVER (PARTITION BY t.mispar_ishi) me_tarich,
                                                                                                 ad_tarich,  t.SNIF_AV,t.EZOR,MAAMAD,t.ISUK, ov.KOD_HEVRA,Ov.shem_mish|| '' '' ||  Ov.shem_prat full_name,
                                                                                                 row_number() OVER (PARTITION BY t.mispar_ishi ORDER BY me_tarich desc) seq
                                                                                    FROM  PIVOT_PIRTEY_OVDIM t,OVDIM Ov  
                                                                                    WHERE t.isuk IS NOT NULL 
                                                                                        AND ( ''' || P_STARTDATE  || ''' BETWEEN ME_TARICH  AND NVL(t.ad_TARICH,TO_DATE( ''01/01/9999'' , ''dd/mm/yyyy'' ))
                                                                                             OR ''' || P_ENDDATE  || '''  BETWEEN ME_TARICH AND NVL(t.ad_TARICH,TO_DATE( ''01/01/9999'' , ''dd/mm/yyyy'' )) 
                                                                                             OR t.ME_TARICH>= ''' || P_STARTDATE  || ''' AND NVL(t.ad_TARICH,TO_DATE( ''01/01/9999'' ,''dd/mm/yyyy'' ))<= ''' || P_ENDDATE  || ''' ) 
                                                                                             and     ov.MISPAR_ISHI = t.MISPAR_ISHI ';
                                                     IF ((P_SNIF <> '' ) OR   (P_SNIF IS NOT NULL )) THEN
                                                        GeneralQry := GeneralQry || ' AND T.SNIF_AV IN(  ' || P_SNIF  || ')';
                                                    end if;                                                                
                                                            GeneralQry := GeneralQry || ParamQry2; 
                                                            GeneralQry := GeneralQry || '  ORDER BY mispar_ishi) a
                                                                        WHERE a.seq=1 
                                                   AND Ezor.kod_ezor(+)  = a.ezor
                                      AND EZOR.KOD_HEVRA(+) = a.KOD_HEVRA
                                      AND Isuk.kod_isuk(+) = a.isuk
                                      AND Isuk.KOD_HEVRA(+) = a.KOD_HEVRA
                                      AND Snif.kod_snif_av(+) = a.snif_av
                                      AND Snif.KOD_HEVRA(+) = a.KOD_HEVRA
                                     AND maamad.kod_maamad_hr(+) =  a.maamad
                                 and MAAMAD.KOD_HEVRA(+) = a.KOD_HEVRA   
                                                         
                                                                        ) Details,
                      ( SELECT y.MISPAR_ISHI,y.TAARICH,Dayofweek(y.taarich) Dayofweek,
                                          So.chariga,So.hashlama,So.out_michsa, NVL(so.mispar_sidur,'''') mispar_sidur,so.shat_hatchala, so.shat_gmar  ,
                                          MikumKnissa.Teur_Mikum_Yechida ClockEntry, MikumYetsia.Teur_Mikum_Yechida ClockExit, 
                                          Sidur.teur_sidur_meychad  ,So.LO_LETASHLUM, Sm.meafyen_53,
                                           decode(pkg_errors.NahagWithSidurTafkidLeloMeafy(p.isuk,so.lo_letashlum, so.KOD_SIBA_LO_LETASHLUM ),''true'',''true'', 
                                                                            pkg_errors.fun_hachtama_bemakom_haasaka(so.mispar_ishi,so.taarich,so.mispar_sidur,nvl(TO_NUMBER(SUBSTR(LPAD(so.mikum_shaon_knisa    , 5, ''0''),0,3)),0)) ) knisa_tkina_err197 ,
                                           decode(pkg_errors.NahagWithSidurTafkidLeloMeafy(p.isuk,so.lo_letashlum, so.KOD_SIBA_LO_LETASHLUM ),''true'',''true'', 
                                                                            pkg_errors.fun_hachtama_bemakom_haasaka(so.mispar_ishi,so.taarich,so.mispar_sidur,nvl(TO_NUMBER(SUBSTR(LPAD(so.mikum_shaon_yetzia    , 5, ''0''),0,3)),0)) ) yatzia_tkina_err198 
                                            
                              FROM TB_YAMEY_AVODA_OVDIM y,
                                        TB_SIDURIM_OVDIM So  ,
                                        CTB_SIDURIM_MEYUCHADIM Sidur  ,
                                        CTB_MIKUM_YECHIDA MikumKnissa,
                                        CTB_MIKUM_YECHIDA MikumYetsia  ,
                                        pivot_pirtey_ovdim p,
                                        Sm  ';
                                       
        IF (P_WorkerViewLevel = 5) THEN
          GeneralQry := GeneralQry || ',TMP_MANAGE_TREE Tree ';
        END IF ;    
 
 GeneralQry := GeneralQry || 'WHERE  (y.taarich     BETWEEN    ''' ||  P_STARTDATE  || ''' AND ''' ||  P_ENDDATE  || ''')
                                                  AND So.mispar_ishi(+) = y.Mispar_ishi 
                                                  AND So.taarich(+) = y.taarich
                                                  and y.mispar_ishi = p.mispar_ishi 
                                                  and  y.taarich between P.ME_TARICH and  NVL(p.ad_TARICH,TO_DATE(''01/01/9999'' ,''dd/mm/yyyy'')) 
                                                  AND Sidur.kod_sidur_meyuchad(+) = So.mispar_sidur
                                                  AND NVL(SO.MISPAR_SIDUR,0)   =SM.MISPAR_SIDUR  
                                                  AND MikumKnissa.kod_mikum_yechida (+) =  TO_NUMBER(SUBSTR(LPAD(so.mikum_shaon_knisa    , 5, ''0''),0,3))
                                                  AND MikumYetsia.kod_mikum_yechida (+) = TO_NUMBER(SUBSTR(LPAD(so.MIKUM_SHAON_YETZIA   , 5, ''0''),0,3))  ';
                  GeneralQry := GeneralQry || ParamQry3;                                   

IF (P_WorkerViewLevel = 5) THEN
  GeneralQry := GeneralQry || 'And   y.mispar_ishi in Tree.mispar_ishi ';
END IF ;    


  --GeneralQry := GeneralQry ||  ParamQry2; -- || ' ORDER BY y.mispar_ishi,y.taarich,so.shat_hatchala, so.shat_gmar  ';
  
  
GeneralQry := GeneralQry ||  ' union  
                            select f.MISPAR_ISHI,f.TAARICH,Dayofweek(f.taarich) Dayofweek,null chariga,null hashlama,null out_michsa,null mispar_sidur,null shat_hatchala,null shat_gmar  ,
                                     null  ClockEntry,null ClockExit , null teur_sidur_meychad  , null LO_LETASHLUM, null  meafyen_53,null knisa_tkina, null yatzia_tkina
                            from  TB_SIDURIM_OVDIM s,
                                    (select H.mispar_ishi, H.taarich,
                                       MAX(CASE WHEN  (H.num2=-1)   THEN H.num    ELSE 0 END)    num ,
                                       MAX(CASE WHEN  (H.num=-1)   THEN H.num2    ELSE 0 END)    num2
                                     from
                                       ( select s.mispar_ishi, s.taarich,count(S.MISPAR_SIDUR) num, -1 num2
                                                  from  TB_SIDURIM_OVDIM s,sm
                                                  where s.mispar_sidur= sm.mispar_sidur     
                                                    AND (s.taarich     BETWEEN    ''' || P_STARTDATE  || '''  AND   ''' ||  P_ENDDATE  || ''' )';
                                  GeneralQry := GeneralQry || ParamQry1;
                                  GeneralQry := GeneralQry || ' group by     s.mispar_ishi, s.taarich
                                         union        
                                                  select s.mispar_ishi, s.taarich,-1 num,count(S.MISPAR_SIDUR) num2
                                                  from  TB_SIDURIM_OVDIM s
                                                  where (s.taarich     BETWEEN    ''' || P_STARTDATE  || ''' AND   ''' ||  P_ENDDATE  || ''')';
                                   GeneralQry := GeneralQry || ParamQry1;
                                   GeneralQry := GeneralQry || ' group by     s.mispar_ishi, s.taarich) H
                                          group by    H.mispar_ishi, H.taarich ) f ' ;
        IF (P_WorkerViewLevel = 5) THEN
          GeneralQry := GeneralQry || ',TMP_MANAGE_TREE Tree ';
        END IF ;    
 GeneralQry := GeneralQry || ' where S.MISPAR_ISHI = f.MISPAR_ISHI
                                                   and S.TAARICH = f.TAARICH 
                                                   and (nvl(f.num,0)=0 and f.num2>0)  ';
                                               --    and( (f.num=1 and (s.mispar_sidur=99200 )) or (nvl(f.num,0)=0 and f.num2>0) )   ';
           IF (P_WorkerViewLevel = 5) THEN
          GeneralQry := GeneralQry || 'And   s.mispar_ishi in Tree.mispar_ishi ';
        END IF ; 
        GeneralQry := GeneralQry || ' ) h
                                           
        where   Details.MISPAR_ISHI =  h.MISPAR_ISHI
                  AND (h.taarich BETWEEN Details.me_tarich AND Details.ad_tarich)
                    and       h.MISPAR_ISHI= headrut.MISPAR_ISHI         
                and   (h.taarich BETWEEN headrut.TAARICH_HATCHALA AND headrut.TAARICH_SIYUM)     
                   and       h.MISPAR_ISHI= cnt_sidurim_beyom.MISPAR_ISHI(+)         
                and   h.taarich= cnt_sidurim_beyom.taarich(+)    
                    and       h.MISPAR_ISHI= cnt_others_sidurim.MISPAR_ISHI(+)         
                and   h.taarich= cnt_others_sidurim.taarich(+)        
                                                
 ORDER BY h.mispar_ishi,h.taarich,shat_hatchala, shat_gmar ';    
  -- GeneralQry := GeneralQry || ') ORDER BY mispar_ishi,taarich,shat_hatchala, shat_gmar  ';
  
DBMS_OUTPUT.PUT_LINE ( GeneralQry);
--execute immediate 'select count(*) from (' || GeneralQry || ')' into rc ;
OPEN p_cur FOR GeneralQry ;
       
           EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END  pro_get_Presence;     

       
  PROCEDURE pro_get_Presence_all (p_cur OUT Curtype ,
                                                   P_STARTDATE IN DATE,
                                                   P_ENDDATE IN DATE,
                                                      P_MISPAR_ISHI IN VARCHAR2 ,
                                                    P_SNIF IN VARCHAR2,
                                                   P_WorkerViewLevel IN NUMBER, 
                                                   P_WORKERID IN VARCHAR2
                                                         )AS  
GeneralQry VARCHAR2(32767);
QryMakatDate VARCHAR2(3000);
 ParamQry1 VARCHAR2(1000);
  ParamQry2 VARCHAR2(1000);
  ParamQry3 VARCHAR2(1000);
  -- P_STARTDATE DATE;
  --P_ENDDATE  DATE;
  x number;
BEGIN
DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_get_Presence_et');

   -- P_STARTDATE:=to_date('01/01/2015','dd/mm/yyyy');--sysdate;
  --  P_ENDDATE:=last_day(to_date('01/01/2015','dd/mm/yyyy'));
x:=0;
--TMP_MANAGE_TREE
/*INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_WorkerViewLevel:',P_WorkerViewLevel);
 INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_WORKERID:',P_WORKERID);
 INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_MISPAR_ISHI:',P_MISPAR_ISHI);
 INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_STARTDATE:',P_STARTDATE);
 INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_ENDDATE:',P_ENDDATE);
*/
IF (P_WorkerViewLevel = 5) THEN  
        Pkg_Utils.pro_ins_Manage_Tree(TO_NUMBER(P_WORKERID,999999999));
        select count(*) into x from TMP_MANAGE_TREE;
        DBMS_OUTPUT.PUT_LINE ('TMP_MANAGE_TREE=' || x);
END IF ;
    
 

OPEN p_cur FOR
    with p as
        (SELECT a.* ,  isuk.teur_isuk ,Ezor.Teur_ezor,Maamad.teur_maamad_hr,snif.teur_snif_av 
               FROM   CTB_EZOR Ezor ,
                             CTB_MAAMAD Maamad, 
                             CTB_ISUK Isuk,
                             CTB_SNIF_AV Snif,  
                             ( SELECT   t.mispar_ishi ,  min( t.ME_TARICH) OVER (PARTITION BY t.mispar_ishi) me_tarich,
                                                                                 ad_tarich,  t.SNIF_AV,t.EZOR,MAAMAD,t.ISUK, ov.KOD_HEVRA,Ov.shem_mish|| ' ' ||  Ov.shem_prat full_name,
                                                                                 row_number() OVER (PARTITION BY t.mispar_ishi ORDER BY me_tarich desc) seq
                                                                    FROM  PIVOT_PIRTEY_OVDIM t,OVDIM Ov  
                                                                    WHERE   t.isuk IS NOT NULL 
                                                                        AND ( P_STARTDATE BETWEEN ME_TARICH  AND NVL(t.ad_TARICH,TO_DATE( '01/01/9999' , 'dd/mm/yyyy' ))
                                                                             OR P_ENDDATE  BETWEEN ME_TARICH AND NVL(t.ad_TARICH,TO_DATE( '01/01/9999' , 'dd/mm/yyyy' )) 
                                                                             OR t.ME_TARICH>= P_STARTDATE AND NVL(t.ad_TARICH,TO_DATE( '01/01/9999' ,'dd/mm/yyyy' ))<= P_ENDDATE ) 
                                                                             and     ov.MISPAR_ISHI = t.MISPAR_ISHI  
                                                                              and ((P_MISPAR_ISHI is null) OR   t.mispar_ishi IN (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MISPAR_ISHI,  ',') AS MYTABTYPE))) )         
                                                                               ORDER BY mispar_ishi) a
                                                        WHERE a.seq=1 
                                   AND Ezor.kod_ezor(+)  = a.ezor
                      AND EZOR.KOD_HEVRA(+) = a.KOD_HEVRA
                      AND Isuk.kod_isuk(+) = a.isuk
                      AND Isuk.KOD_HEVRA(+) = a.KOD_HEVRA
                      AND Snif.kod_snif_av(+) = a.snif_av
                      AND Snif.KOD_HEVRA(+) = a.KOD_HEVRA
                     AND maamad.kod_maamad_hr(+) =  a.maamad
                     and ((nvl(P_WorkerViewLevel,0) <>5 and a.mispar_ishi not in(select * from TMP_MANAGE_TREE) ) or (P_WorkerViewLevel=5 and  a.mispar_ishi in (select * from TMP_MANAGE_TREE))   ) 
                    and ((P_SNIF is null) OR   a.snif_av IN (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_SNIF,  ',') AS MYTABTYPE))) )         
                 and MAAMAD.KOD_HEVRA(+) = a.KOD_HEVRA  )
    ,sh AS 
       (          
                SELECT distinct  a.mispar_sidur --, a.KOD_MEAFYEN
                 FROM TB_SIDURIM_MEYUCHADIM a
                 WHERE  a.KOD_MEAFYEN =53
                   AND (  P_STARTDATE BETWEEN a.ME_TAARICH  AND a.AD_TAARICH
                        OR  P_ENDDATE  BETWEEN a.ME_TAARICH AND a.AD_TAARICH 
                        OR (a.ME_TAARICH>=P_STARTDATE  AND a.AD_TAARICH <= P_ENDDATE  ))
     )
  ,sidur as(
      SELECT y.MISPAR_ISHI,y.TAARICH,Dayofweek(y.taarich) Dayofweek,
          So.chariga,So.hashlama,So.out_michsa, NVL(so.mispar_sidur,'') mispar_sidur,so.shat_hatchala, so.shat_gmar  ,
          MikumKnissa.Teur_Mikum_Yechida ClockEntry, MikumYetsia.Teur_Mikum_Yechida ClockExit, 
          Sidur.teur_sidur_meychad  ,So.LO_LETASHLUM, sh.mispar_sidur meafyen_53,
           decode(pkg_errors.NahagWithSidurTafkidLeloMeafy(p.isuk,so.lo_letashlum, so.KOD_SIBA_LO_LETASHLUM ),'true','true', 
                                            pkg_errors.fun_hachtama_bemakom_haasaka(so.mispar_ishi,so.taarich,so.mispar_sidur,nvl(TO_NUMBER(SUBSTR(LPAD(so.mikum_shaon_knisa    , 5, '0'),0,3)),0)) ) knisa_tkina_err197 ,
           decode(pkg_errors.NahagWithSidurTafkidLeloMeafy(p.isuk,so.lo_letashlum, so.KOD_SIBA_LO_LETASHLUM ),'true','true', 
                                            pkg_errors.fun_hachtama_bemakom_haasaka(so.mispar_ishi,so.taarich,so.mispar_sidur,nvl(TO_NUMBER(SUBSTR(LPAD(so.mikum_shaon_yetzia    , 5, '0'),0,3)),0)) ) yatzia_tkina_err198 
                                                
                                  FROM p, TB_YAMEY_AVODA_OVDIM y,
                                            TB_SIDURIM_OVDIM So  ,
                                            CTB_SIDURIM_MEYUCHADIM Sidur  ,
                                            CTB_MIKUM_YECHIDA MikumKnissa,
                                            CTB_MIKUM_YECHIDA MikumYetsia,  
                                            sh
                                    WHERE     p.mispar_ishi = y.mispar_ishi
                                              and  y.taarich between P.ME_TARICH and  NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))  
                                             and (y.taarich     BETWEEN    P_STARTDATE AND P_ENDDATE)
                                              AND y.Mispar_ishi = So.mispar_ishi(+) 
                                              AND y.taarich = So.taarich(+)                                         
                                              AND So.mispar_sidur =Sidur.kod_sidur_meyuchad(+) 
                                              AND NVL(SO.MISPAR_SIDUR,0)   =sh.MISPAR_SIDUR(+)  
                                              AND MikumKnissa.kod_mikum_yechida (+) =  TO_NUMBER(SUBSTR(LPAD(so.mikum_shaon_knisa    , 5, '0'),0,3))
                                              AND MikumYetsia.kod_mikum_yechida (+) = TO_NUMBER(SUBSTR(LPAD(so.MIKUM_SHAON_YETZIA   , 5, '0'),0,3))   
                                             )                          
        ,headrut as(
            select M.MISPAR_ISHI, M.TAARICH_HATCHALA, M.TAARICH_SIYUM,   T.KOD_HEADRUT 
            from p,   ctb_status t, MATZAV_OVDIM m 
            where  p.mispar_ishi=M.MISPAR_ISHI
                 and    t.KOD_STATUS= m.KOD_MATZAV
                     and p.KOD_HEVRA = T.KOD_HEVRA    
                                  AND (  P_STARTDATE  BETWEEN M.TAARICH_HATCHALA  AND NVL(M.TAARICH_SIYUM,TO_DATE(  '01/01/9999'  ,  'dd/mm/yyyy' ))
                                         OR  P_ENDDATE   BETWEEN M.TAARICH_HATCHALA AND NVL(M.TAARICH_SIYUM ,TO_DATE(  '01/01/9999'  ,  'dd/mm/yyyy'  )) 
                                         OR M.TAARICH_HATCHALA>=P_STARTDATE  AND NVL(M.TAARICH_SIYUM,TO_DATE(  '01/01/9999'  , 'dd/mm/yyyy'  ))<= P_ENDDATE  )  
          ) 
    ,  cnt_sidurim_beyom as  (
                select  s.mispar_ishi,s.taarich ,count(*) num
                    from p, tb_sidurim_ovdim s
                    where  p.mispar_ishi=s.mispar_ishi
                     and   s.taarich between P_STARTDATE  and P_ENDDATE
                 group by s.mispar_ishi,s.taarich)                                   
      select sidur.*,p.full_name ,p.kod_hevra,
             (Pkg_Ovdim.fun_get_meafyen_oved(sidur.mispar_ishi, 3, P_ENDDATE  )) START_TIME_ALLOWED ,
             (Pkg_Ovdim.fun_get_meafyen_oved(sidur.mispar_ishi, 4, P_ENDDATE )) END_TIME_ALLOWED  ,
              p.teur_isuk ,p.Teur_ezor,p.teur_maamad_hr,p.teur_snif_av ,headrut.KOD_HEADRUT,
              pkg_reports.get_heara_doch_nochechut(sidur.mispar_ishi,sidur.taarich,sidur.mispar_sidur,sidur.lo_letashlum,sidur.shat_hatchala, sidur.shat_gmar ,headrut.KOD_HEADRUT, nvl(cnt_sidurim_beyom.num,0), 0,0) heara
                           
      from sidur,p,  cnt_sidurim_beyom,headrut
                                     
      where   p.MISPAR_ISHI =  sidur.MISPAR_ISHI
          AND (sidur.taarich BETWEEN p.me_tarich AND p.ad_tarich)
          and  sidur.MISPAR_ISHI= headrut.MISPAR_ISHI         
          and  (sidur.taarich BETWEEN headrut.TAARICH_HATCHALA AND headrut.TAARICH_SIYUM)     
          and   sidur.MISPAR_ISHI= cnt_sidurim_beyom.MISPAR_ISHI(+)         
          and   sidur.taarich= cnt_sidurim_beyom.taarich(+)           
                                                
 ORDER BY sidur.mispar_ishi,sidur.taarich,shat_hatchala, shat_gmar;
       
           EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END  pro_get_Presence_all;    
PROCEDURE pro_get_DriverWithoutTacograph  (p_cur OUT CurType , 
                                                                            p_ezor IN VARCHAR2,
                                                                            p_Period IN VARCHAR2) IS 
    p_ToDate DATE ; 
    p_FromDate DATE ; 
    p_Erech TB_PARAMETRIM.erech_param%TYPE ;
    GeneralQry VARCHAR2(32767);
    QryMakatDate VARCHAR2(3000);
    ParamQry VARCHAR2(1000);
BEGIN
   DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_get_DriverWithoutTacograph');
 
    p_FromDate :=  TO_DATE('01/' || p_Period,'dd/mm/yyyy'); /* period= 05/2009=>  v_MinLimitDate = 01/05/2009 */
    p_ToDate := ADD_MONTHS(p_FromDate,1) -1 ;    /* period= 05/2009=>  v_MaxLimitDate = 31/05/2009 */
    Pkg_Utils.Pro_Get_Value_From_Parametrim(193,p_period,p_Erech);

    QryMakatDate :=  fct_MakatDateSql(p_FromDate ,p_ToDate  ,NULL ,NULL ,NULL  );
     pro_Prepare_Catalog_Details(QryMakatDate);
  --  COMMIT ; 
    GeneralQry :=  ' SELECT   Yao.mispar_ishi , Yao.taarich , Ov.shem_mish|| '' '' || Ov.shem_prat  Full_Name ,
                             Ezor.Teur_ezor, Snif.teur_snif_av ,
                             So.mispar_sidur , So.shat_hatchala , So.shat_gmar, NVL( So.shat_gmar ,So.shat_hatchala) shat_gmar_diff,
                             Details.sum_km,Details.oto_no,
                             Mashar.vehicle_type_dscr ,Mashar.manufacture_year  
                     FROM  (SELECT mispar_ishi, taarich
                                FROM TB_YAMEY_AVODA_OVDIM
                                WHERE Tachograf = 1 AND 
                                Mispar_ishi IN (SELECT Mispar_ishi  
                                                    FROM TB_YAMEY_AVODA_OVDIM 
                                                    WHERE Tachograf = 1
                                                    AND taarich BETWEEN  ''' || p_FromDate || '''  AND  ''' || p_ToDate || '''
                                                    GROUP BY Mispar_ishi 
                                                    HAVING COUNT(Mispar_ishi) >= 0 
                                                    )
                                 AND taarich BETWEEN ''' || p_FromDate || '''  AND  ''' || p_ToDate || ''') Yao ,
                             OVDIM Ov ,
                             TB_SIDURIM_OVDIM So ,
                             CTB_SNIF_AV Snif ,
                             CTB_EZOR  Ezor,
                             ( SELECT * 
                               FROM ( SELECT   t.mispar_ishi , 
                                                MAX( t.ME_TARICH) OVER (PARTITION BY t.mispar_ishi) me_tarich,
                                             t.ad_tarich,t.SNIF_AV,t.EZOR,  
                                              row_number() OVER (PARTITION BY t.mispar_ishi ORDER BY me_tarich desc) seq
                                    FROM  PIVOT_PIRTEY_OVDIM t
                                    WHERE t.isuk IS NOT NULL 
                                        AND ( ''' || p_FromDate  || ''' BETWEEN ME_TARICH  AND NVL(t.ad_TARICH,TO_DATE( ''01/01/9999'' , ''dd/mm/yyyy'' ))
                                             OR ''' || p_ToDate  || '''  BETWEEN ME_TARICH AND NVL(t.ad_TARICH,TO_DATE( ''01/01/9999'' , ''dd/mm/yyyy'' )) 
                                             OR t.ME_TARICH>= ''' || p_FromDate  || ''' AND NVL(t.ad_TARICH,TO_DATE( ''01/01/9999'' ,''dd/mm/yyyy'' ))<= ''' || p_ToDate || ''' )
                                     ORDER BY mispar_ishi) a
                              WHERE a.seq=1 ) RelevantDetails ,
                            ( SELECT   S.TAARICH, S.MISPAR_ISHI, A.OTO_NO   , S.MISPAR_SIDUR, S.SHAT_HATCHALA,  S.SHAT_GMAR,  A.SUM_km
                              FROM  TB_SIDURIM_OVDIM S,
                                    ( SELECT   A.TAARICH, A.MISPAR_ISHI,A.MISPAR_SIDUR ,A.SHAT_HATCHALA_SIDUR,
                                      A.SHAT_YETZIA peilut_SHAT_YETZIA,  
                                       max( A.OTO_NO) OVER (PARTITION BY A.TAARICH, A.MISPAR_ISHI,A.MISPAR_SIDUR,A.SHAT_HATCHALA_SIDUR) oto_no,
                                      MIN(A.SHAT_YETZIA) OVER (PARTITION BY A.TAARICH, A.MISPAR_ISHI,A.MISPAR_SIDUR,A.SHAT_HATCHALA_SIDUR) min_peilut_SHAT_YETZIA 
                                      , SUM(c.km) OVER (PARTITION BY A.TAARICH, A.MISPAR_ISHI,A.MISPAR_SIDUR, A.SHAT_HATCHALA_SIDUR  )SUM_km
                                       FROM  TB_PEILUT_OVDIM A ,TMP_CATALOG c
                                     WHERE        A.TAARICH BETWEEN ''' || p_FromDate || ''' AND ''' || p_ToDate || '''
                                              AND A.MISPAR_KNISA = 0
                                            --  AND A.MISPAR_ISHI IN( 76133)
                                                AND A.TAARICH=c.ACTIVITY_DATE(+) 
                                                  AND A.MAKAT_NESIA = c.MAKAT8(+) 
                                               --   AND nvl(c.km,0) <> 0
                                              )A
                              WHERE   S.TAARICH  BETWEEN ''' || p_FromDate || ''' AND ''' || p_ToDate || '''
                                        --   AND S.MISPAR_ISHI IN( 76133  )  
                                     AND A.MISPAR_ISHI(+) = S.MISPAR_ISHI
                                     AND A.TAARICH(+) = S.TAARICH   
                                     AND A.MISPAR_SIDUR(+)=S.MISPAR_SIDUR
                                     AND A.SHAT_HATCHALA_SIDUR(+) =S.SHAT_HATCHALA
                                     AND A.peilut_SHAT_YETZIA(+) = A.min_peilut_SHAT_YETZIA(+)
                                     AND ((S.MISPAR_SIDUR in (  select SM.MISPAR_SIDUR
                                                                                from tb_sidurim_meyuchadim sm
                                                                                where SM.KOD_MEAFYEN=3 and
                                                                                          SM.ERECH=5)) or S.SUG_SIDUR in(select  M.SUG_SIDUR
                                                                                                                                            from TB_MEAFYENEY_SUG_SIDUR m
                                                                                                                                            where M.KOD_MEAFYEN=3 and M.ERECH=5 ))
                                     )  Details, 
                           VEHICLE_SPECIFICATIONS Mashar  
                    WHERE Ov.mispar_ishi = Yao.mispar_ishi 
                      AND RelevantDetails.mispar_ishi = Yao.mispar_ishi 
                      AND nvl(Details.sum_km,0)>0
                      AND Yao.taarich BETWEEN RelevantDetails.me_tarich AND RelevantDetails.ad_tarich 
                      AND RelevantDetails.EZOR= Ezor.kod_ezor
                      AND Ov.kod_hevra = Ezor.kod_hevra
                      AND RelevantDetails.SNIF_AV= Snif.kod_snif_av
                      AND Ov.kod_hevra = Snif.kod_hevra
                      AND So.mispar_ishi = Yao.mispar_ishi
                      AND So.taarich = Yao.Taarich
                      AND So.mispar_ishi = Details.mispar_ishi
                      AND So.taarich  = Details.taarich 
                      AND So.mispar_sidur = Details.mispar_sidur
                      AND So.SHAT_HATCHALA = Details.SHAT_HATCHALA
                      AND  Mashar.bus_number(+) = Details.oto_no ';

IF (( p_ezor IS NOT  NULL ) OR ( p_ezor <> '' )) THEN 
 
    GeneralQry := GeneralQry || ' And RelevantDetails.ezor in (' || p_ezor || ')' ;
END IF ; 
  GeneralQry := GeneralQry || ' order by   Ezor.Teur_ezor, Snif.teur_snif_av , Yao.mispar_ishi , So.shat_hatchala';

    OPEN p_cur FOR  GeneralQry ;
    
           EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END  pro_get_DriverWithoutTacograph;     

  PROCEDURE Pro_Get_MaxTimeNoTacograph(p_cur OUT CurType , 
                                                                            p_Period IN VARCHAR2) IS 
  p_Erech TB_PARAMETRIM.erech_param%TYPE ;
  BEGIN
   Pkg_Utils.Pro_Get_Value_From_Parametrim(193,p_period,p_Erech);
    OPEN p_cur FOR SELECT  p_erech ERECH_PARAM FROM dual ; 
           EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END  Pro_Get_MaxTimeNoTacograph;           

PROCEDURE pro_get_SumDriverNotSigned  (p_cur OUT CurType , 
                                                                            p_ezor IN VARCHAR2,
                                                                            p_Period IN VARCHAR2) IS 
    p_ToDate DATE ; 
    p_FromDate DATE ;  
    p_snif VARCHAR2(50);
   -- Pezor VARCHAR2(10);
    GeneralQry VARCHAR2(32767);
  BEGIN
     DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_get_SumDriverNotSigned');
   INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_EZOR:',p_ezor);
   INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_ENDDATE:',p_Period);
      p_FromDate := TO_DATE('01/' || p_Period,'dd/mm/yyyy'); /* period= 05/2009=>  v_MinLimitDate = 01/05/2009 */
      p_ToDate := ADD_MONTHS(p_FromDate,1) -1 ;    /* period= 05/2009=>  v_MaxLimitDate = 31/05/2009 */
    
      p_snif := Pkg_Reports.get_First_Snif_Relevant(p_FromDate,p_ToDate,p_ezor);
        
      DBMS_OUTPUT.PUT_LINE (p_snif);    
    
        GeneralQry:= ' select h.Ezor,h.teur_snif_av,h.taarich,h.Cnt
                                from(
                                     SELECT   Snif.Ezor,  Snif.teur_snif_av  ,So.taarich ,SUM (CASE WHEN So.mispar_ishi = 0 THEN 0 ELSE 1 END) Cnt
                                       FROM
                                        ( SELECT  taarich,mispar_ishi FROM TB_SIDURIM_OVDIM
                                         WHERE Nidreshet_hitiatzvut > 0 
                                                AND taarich BETWEEN '''  ||  P_FromDate  || '''   AND '''  ||  P_ToDate  || '''                               
                                                AND ( Shat_hitiatzvut IS NULL AND (Ptor_Mehitiatzvut IS NULL OR Ptor_Mehitiatzvut = 0)
                                                OR( Shat_hitiatzvut IS NOT NULL AND Kod_SIBA_LEDIVUCH_YADANI_in IN ( SELECT Kod_Siba FROM CTB_SIBOT_LEDIVUCH_YADANI WHERE Mutzdak = 0)))
                                       ) So,
                                       
                                        ( SELECT * 
                                           FROM ( SELECT   t.mispar_ishi , 
                                                         MAX( t.ME_TARICH) OVER (PARTITION BY t.mispar_ishi) me_tarich,
                                                         t.ad_tarich,t.SNIF_AV,t.EZOR,  
                                                         row_number() OVER (PARTITION BY t.mispar_ishi ORDER BY me_tarich desc) seq
                                                FROM  PIVOT_PIRTEY_OVDIM t
                                                WHERE t.isuk IS NOT NULL 
                                                    AND ( ''' || p_FromDate  || ''' BETWEEN ME_TARICH  AND NVL(t.ad_TARICH,TO_DATE( ''01/01/9999'' , ''dd/mm/yyyy'' ))
                                                         OR ''' || p_ToDate  || '''  BETWEEN ME_TARICH AND NVL(t.ad_TARICH,TO_DATE( ''01/01/9999'' , ''dd/mm/yyyy'' )) 
                                                         OR t.ME_TARICH>= ''' || p_FromDate  || ''' AND NVL(t.ad_TARICH,TO_DATE( ''01/01/9999'' ,''dd/mm/yyyy'' ))<= ''' || p_ToDate || ''' )
                                                 ORDER BY mispar_ishi) a
                                          WHERE a.seq=1 ) RelevantDetails  ,
                                                  CTB_SNIF_AV Snif   ,
                                                CTB_SNIFEY_TNUAA Stnua
                                    WHERE     So.mispar_ishi=       RelevantDetails.mispar_ishi(+)
                                      AND So.taarich BETWEEN  RelevantDetails.me_tarich(+) AND RelevantDetails.ad_tarich(+)
                                      AND Snif.kod_snif_av   = RelevantDetails.snif_av 
                                      AND ( Snif.SNIF_TNUA = Stnua.KOD_SNIF_TNUAA)
                                      AND Snif.Ezor=RelevantDetails.EZOR ';
                 
        IF (( p_ezor IS NOT  NULL ) OR ( p_ezor <> '' )) THEN 
            GeneralQry:= GeneralQry || ' And RelevantDetails.EZOR IN( ' || p_ezor ||')';
       END IF ;             
                       
            GeneralQry:= GeneralQry ||    ' GROUP BY CUBE(Snif.Ezor,Snif.teur_snif_av,So.taarich )
                      HAVING GROUPING (Snif.Ezor)=0  and GROUPING (Snif.teur_snif_av) = 0 AND GROUPING (So.taarich)  = 0   
              UNION
                    SELECT   S.EZOR , ''' || p_snif || '''  teur_snif_av ,TO_DATE(x,''dd/mm/yyyy'') Taarich ,NULL  
                        FROM TABLE(CAST(Convert_String_To_Table(String_Dates_Of_Period(''' || p_Period || '''),'','') AS mytabtype)) ,
                         CTB_SNIF_AV s
                          where S.TEUR_SNIF_AV= ''' || p_snif || ''' ) h
                        order by h.EZOR, h.teur_snif_av  ,h.taarich ';
                
    DBMS_OUTPUT.PUT_LINE (GeneralQry);       
    OPEN p_cur FOR     GeneralQry;              
    
           EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END  pro_get_SumDriverNotSigned;           
  
 FUNCTION get_First_Snif_Relevant(p_start IN DATE,p_end IN DATE,p_ezor IN VARCHAR2) RETURN VARCHAR2
 IS
         p_snif VARCHAR2(50);
        GeneralQry VARCHAR2(32767);
 BEGIN
    BEGIN
       DBMS_APPLICATION_INFO.SET_MODULE('Pkg_Reports.get_First_Snif_Relevant','get first snif');
 
          GeneralQry:=  'select h.teur_snif_av from( SELECT   Snif.teur_snif_av   
         FROM 
            ( SELECT  taarich,mispar_ishi FROM TB_SIDURIM_OVDIM
                 WHERE Nidreshet_hitiatzvut > 0 
                         AND taarich BETWEEN ''' || p_start || '''   AND ''' || p_end || '''                             
                        AND ( Shat_hitiatzvut IS NULL AND (Ptor_Mehitiatzvut IS NULL OR Ptor_Mehitiatzvut = 0)
                        OR( Shat_hitiatzvut IS NOT NULL AND Kod_SIBA_LEDIVUCH_YADANI_in IN ( SELECT Kod_Siba FROM CTB_SIBOT_LEDIVUCH_YADANI WHERE Mutzdak = 0))) 
                        )So
            ,
            ( SELECT * 
              FROM ( SELECT   t.mispar_ishi , 
                            MAX( t.ME_TARICH) OVER (PARTITION BY t.mispar_ishi) me_tarich,
                         t.ad_tarich,t.SNIF_AV,t.EZOR,  
                          row_number() OVER (PARTITION BY t.mispar_ishi ORDER BY me_tarich desc) seq
                    FROM  PIVOT_PIRTEY_OVDIM t
                    WHERE t.isuk IS NOT NULL 
                        AND ( ''' || p_start  || ''' BETWEEN ME_TARICH  AND NVL(t.ad_TARICH,TO_DATE( ''01/01/9999'' , ''dd/mm/yyyy'' ))
                             OR ''' || p_end  || '''  BETWEEN ME_TARICH AND NVL(t.ad_TARICH,TO_DATE( ''01/01/9999'' , ''dd/mm/yyyy'' )) 
                             OR t.ME_TARICH>= ''' || p_start  || ''' AND NVL(t.ad_TARICH,TO_DATE( ''01/01/9999'' ,''dd/mm/yyyy'' ))<= ''' || p_end || ''' )
                     ORDER BY mispar_ishi) a
              WHERE a.seq=1 ) RelevantDetails  , 
                           CTB_SNIF_AV Snif ,
                           CTB_SNIFEY_TNUAA Stnua  
         WHERE So.mispar_ishi = RelevantDetails.mispar_ishi
          AND So.taarich BETWEEN RelevantDetails.me_tarich AND RelevantDetails.ad_tarich
          AND Snif.kod_snif_av =RelevantDetails.SNIF_AV
          AND ( Snif.SNIF_TNUA = Stnua.KOD_SNIF_TNUAA)
          AND Snif.Ezor=RelevantDetails.EZOR ';
          
             IF (( p_ezor IS NOT  NULL ) OR ( p_ezor <> '' )) THEN 
                GeneralQry:= GeneralQry || ' And RelevantDetails.EZOR IN(' || p_ezor ||')';
           END IF ; 
          GeneralQry:= GeneralQry || '  order by    Snif.teur_snif_av  ) h where rownum=1';
EXECUTE IMMEDIATE GeneralQry INTO p_snif  ; 

        EXCEPTION
         WHEN NO_DATA_FOUND  THEN
             p_snif:=NULL; 
     END;     
      
      RETURN p_snif; 
 END get_First_Snif_Relevant;
  
  
        
  

  PROCEDURE pro_get_DriverNotSigned  (p_cur OUT CurType , 
                                                                            p_ezor IN VARCHAR2,
                                                                            p_Period IN VARCHAR2) IS 
    p_ToDate DATE ; 
    p_FromDate DATE ; 
    p_Erech TB_PARAMETRIM.erech_param%TYPE ;
    GeneralQry VARCHAR2(32767);
    ParamQry VARCHAR2(1000);        
  BEGIN
   DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_get_DriverNotSigned');
 
--INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'p_ezor:',p_ezor);
--INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'p_Period:',p_Period);
      p_FromDate := TO_DATE('01/' || p_Period,'dd/mm/yyyy'); /* period= 05/2009=>  v_MinLimitDate = 01/05/2009 */
    p_ToDate := ADD_MONTHS(p_FromDate,1) -1 ;    /* period= 05/2009=>  v_MaxLimitDate = 31/05/2009 */
    Pkg_Utils.Pro_Get_Value_From_Parametrim(147,p_period,p_Erech);
    GeneralQry := ' SELECT  so.taarich, SUBSTR(Details.EZOR,0,1) || SUBSTR(Details.MAAMAD,0,1) || ''-'' ||  so.MISPAR_ISHI MISPAR_ISHI ,so.MISPAR_ISHI  MIS_ISHI, 
                                         Ezor.Teur_ezor,Details.Ezor,Ov.shem_mish || '' '' || Ov.shem_prat full_name,Snif.teur_snif_av,mispar_sidur1,so.StartTime1,so.EndTime1,
                                         mispar_sidur2,so.StartTime2,so.EndTime2
                           FROM
                                   (select   sidurim.MISPAR_ISHI,sidurim.taarich   ,
                                       MAX(CASE WHEN  sidurim.nidreshet_hitiatzvut =1 THEN sidurim.mispar_sidur    ELSE null END)    mispar_sidur1,
                                      MAX(CASE WHEN  sidurim.nidreshet_hitiatzvut =1 THEN sidurim.shat_hatchala    ELSE null END)    StartTime1,
                                      MAX(CASE WHEN  sidurim.nidreshet_hitiatzvut =1 THEN sidurim.shat_gmar    ELSE null END)    EndTime1,
                                      MAX(CASE WHEN  sidurim.nidreshet_hitiatzvut =2 THEN sidurim.shat_hatchala    ELSE null END)    StartTime2,
                                      MAX(CASE WHEN  sidurim.nidreshet_hitiatzvut =2 THEN sidurim.shat_gmar    ELSE null END)    EndTime2 ,
                                      MAX(CASE WHEN  sidurim.nidreshet_hitiatzvut =2 THEN sidurim.mispar_sidur    ELSE null END)    mispar_sidur2
                                    from                             
                                       (SELECT A.taarich,A.mispar_ishi,A.mispar_sidur,A.nidreshet_hitiatzvut,A.SHAT_GMAR,A.SHAT_HATCHALA  
                                        FROM( SELECT s.taarich,s.mispar_ishi,s.mispar_sidur,s.nidreshet_hitiatzvut,s.SHAT_GMAR,s.SHAT_HATCHALA,
                                                     COUNT(s.mispar_ishi)  OVER (PARTITION BY  s.mispar_ishi) num  
                                              FROM TB_SIDURIM_OVDIM s, TB_YAMEY_AVODA_OVDIM y
                                  WHERE   s.mispar_ishi = y.mispar_ishi
                                             and s.taarich = y.taarich
                                             and y.status =1
                                              and s.lo_letashlum =0
                                            AND  s.taarich BETWEEN  '''  ||  P_FromDate  || '''  AND  '''  ||  P_ToDate  || '''             
                                              AND s.Nidreshet_hitiatzvut > 0 
                                              AND ( (s.Shat_hitiatzvut IS NULL AND (s.Ptor_Mehitiatzvut IS NULL OR s.Ptor_Mehitiatzvut = 0))
                                                    OR  (s.Shat_hitiatzvut IS NOT NULL AND s.Kod_SIBA_LEDIVUCH_YADANI_in IN (SELECT Kod_Siba FROM CTB_SIBOT_LEDIVUCH_YADANI WHERE Mutzdak = 0)))
                                               )A 
                                           WHERE num>  '''  || p_Erech  || ''' ) sidurim
                                 --where   sidurim.mispar_ishi=46456                               
                                 group by  sidurim.MISPAR_ISHI,sidurim.taarich      
                                 order by  sidurim.MISPAR_ISHI,  sidurim.taarich ) so,
                     ( SELECT * 
                              FROM ( SELECT   t.mispar_ishi , 
                                            MAX( t.ME_TARICH) OVER (PARTITION BY t.mispar_ishi) me_tarich,
                                         t.ad_tarich,t.SNIF_AV,t.EZOR,t.MAAMAD,
                                          row_number() OVER (PARTITION BY t.mispar_ishi ORDER BY me_tarich desc) seq
                                    FROM  PIVOT_PIRTEY_OVDIM t
                                    WHERE t.isuk IS NOT NULL 
                                        AND (  '''  ||  P_FromDate  || '''  BETWEEN ME_TARICH  AND NVL(t.ad_TARICH,TO_DATE( ''01/01/9999'' , ''dd/mm/yyyy'' ))
                                             OR  '''  ||  P_ToDate  || '''            BETWEEN ME_TARICH AND NVL(t.ad_TARICH,TO_DATE(''01/01/9999'' , ''dd/mm/yyyy'' )) 
                                             OR t.ME_TARICH>= '''  ||  P_FromDate  || '''  AND NVL(t.ad_TARICH,TO_DATE( ''01/01/9999'' ,''dd/mm/yyyy'' ))<= '''  ||  P_ToDate  || '''  )
                                     ORDER BY mispar_ishi) a
                              WHERE a.seq=1 )   Details ,   
                          CTB_SNIF_AV Snif ,
                          OVDIM  Ov,
                          CTB_EZOR Ezor             
            WHERE 
                            so.mispar_ishi= Details.mispar_ishi
                         --AND sidurim.taarich BETWEEN Details.me_tarich AND Details.ad_tarich
                        AND  so.mispar_ishi= ov.mispar_ishi
                        AND Details.SNIF_AV=Snif.kod_snif_av
                        AND Snif.kod_hevra = ov.kod_hevra
                        AND Details.Ezor=Ezor.kod_ezor
                        AND Ezor.kod_hevra = ov.kod_hevra
                        AND SNIF.KOD_HEVRA <> 4895';
                        
        IF (( P_EZOR IS NOT  NULL ) OR ( P_EZOR <> '' )) THEN 
            GeneralQry := GeneralQry || ' And EZOR.kod_ezor in (' || P_EZOR || ') ';
        END IF ;       

   --GeneralQry := GeneralQry || ' order by  Ezor.kod_ezor, Snif.teur_snif_av,so.MISPAR_ISHI,  so.taarich ' ;
 

DBMS_OUTPUT.PUT_LINE ( GeneralQry);
--execute immediate 'select count(*) from (' || GeneralQry || ')' into rc ;
OPEN p_cur FOR GeneralQry ;      
      
           EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END  pro_get_DriverNotSigned;          


  
 PROCEDURE Pro_Get_DisregardDrivers(p_Cur OUT CurType ,
                                     P_DISREGARDTYPE IN NUMBER,                                                                              
                                     p_Period IN VARCHAR2) 
IS 
    p_ToDate DATE ; 
    p_FromDate DATE ; 
    --sector VARCHAR2(100);
    GeneralQry VARCHAR2(32767);
  BEGIN
      DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'Pro_Get_DisregardDrivers');
 
        p_FromDate := TO_DATE('01/' || p_Period,'dd/mm/yyyy'); /* period= 05/2009=>  v_MinLimitDate = 01/05/2009 */
        p_ToDate := ADD_MONTHS(p_FromDate,1) -1 ;    /* period= 05/2009=>  v_MaxLimitDate = 31/05/2009 */
     
  OPEN p_cur FOR
     SELECT 
         Ov.shem_mish || '   ' || Ov.shem_prat full_name , 
         Yao.mispar_ishi , Yao.taarich , Dayofweek(Yao.taarich) Dayofweek ,decode(YAO.STATUS,null,-1,YAO.STATUS) STATUS,
        Activity.mispar_sidur , Activity.shat_hatchala , Activity.shat_gmar , Activity.oto_no,Activity.SECTOR_VISA,Activity.mispar_visa,
         Ezor.Teur_ezor,  Ezor.kod_ezor,
         Snif.teur_snif_av
        FROM 
         TB_YAMEY_AVODA_OVDIM Yao ,
         OVDIM Ov ,  
         ( SELECT * 
            FROM ( SELECT   t.mispar_ishi , 
                        MAX( t.ME_TARICH) OVER (PARTITION BY t.mispar_ishi) me_tarich,
                     t.ad_tarich,t.SNIF_AV,t.EZOR, 
                      row_number() OVER (PARTITION BY t.mispar_ishi ORDER BY me_tarich desc) seq
                FROM  PIVOT_PIRTEY_OVDIM t
                WHERE t.isuk IS NOT NULL 
                    AND (   P_FromDate    BETWEEN ME_TARICH  AND NVL(t.ad_TARICH,TO_DATE(  '01/01/9999'  ,  'dd/mm/yyyy'  ))
                         OR   P_ToDate     BETWEEN ME_TARICH AND NVL(t.ad_TARICH,TO_DATE(  '01/01/9999'  ,  'dd/mm/yyyy'  )) 
                         OR t.ME_TARICH>=  P_FromDate   AND NVL(t.ad_TARICH,TO_DATE(  '01/01/9999'   ,'dd/mm/yyyy'  ))<=  P_ToDate   )
                 ORDER BY mispar_ishi) a
            WHERE a.seq=1 )   RelevantDetails , 
         ( SELECT   S.TAARICH, S.MISPAR_ISHI, A.OTO_NO   , S.MISPAR_SIDUR, S.SHAT_HATCHALA,
                 S.SHAT_GMAR, NVL ( S.SECTOR_VISA, -1 ) SECTOR_VISA ,A.mispar_visa, A.mispar_matala
             FROM      TB_SIDURIM_OVDIM S,
                        ( SELECT   A.TAARICH, A.MISPAR_ISHI,A.MISPAR_SIDUR, A.OTO_NO ,A.SHAT_HATCHALA_SIDUR,
                          A.SHAT_YETZIA peilut_SHAT_YETZIA, 
                          MIN(A.SHAT_YETZIA) OVER (PARTITION BY A.TAARICH, A.MISPAR_ISHI,A.MISPAR_SIDUR,A.SHAT_HATCHALA_SIDUR) min_peilut_SHAT_YETZIA,
                          Max(A.mispar_visa)   OVER (PARTITION BY A.TAARICH, A.MISPAR_ISHI,A.MISPAR_SIDUR,A.SHAT_HATCHALA_SIDUR) mispar_visa,
                           Max(A.mispar_matala )   OVER (PARTITION BY A.TAARICH, A.MISPAR_ISHI,A.MISPAR_SIDUR,A.SHAT_HATCHALA_SIDUR) mispar_matala
                           FROM  TB_PEILUT_OVDIM A
                         WHERE        A.TAARICH BETWEEN  P_FromDate  AND P_ToDate 
                                  AND A.MISPAR_KNISA = 0
                                -- AND A.MISPAR_ISHI IN( 205,206,42690 )
                                  )A
             WHERE     
                    S.TAARICH  BETWEEN  P_FromDate  AND P_ToDate 
                --  AND S.MISPAR_ISHI IN( 205,206,42690  ) AND S.MISPAR_ISHI IN( 205,206,42690)
                   AND A.MISPAR_ISHI(+) = S.MISPAR_ISHI
                  AND A.TAARICH(+) = S.TAARICH   
                  AND A.MISPAR_SIDUR(+)=S.MISPAR_SIDUR
                  AND A.SHAT_HATCHALA_SIDUR(+) =S.SHAT_HATCHALA
                  AND A.peilut_SHAT_YETZIA(+) = A.min_peilut_SHAT_YETZIA(+)
               AND( ( P_DISREGARDTYPE<0 and(S.MISPAR_SIDUR not like '99%' or  S.MISPAR_SIDUR<1000 or  A.mispar_matala>0  )) 
                        or P_DISREGARDTYPE>=0)
                   )Activity,
              CTB_EZOR Ezor,
             CTB_SNIF_AV Snif 
                                            
        WHERE  Yao.taarich   BETWEEN  P_FromDate  AND P_ToDate 
           AND Yao.mispar_ishi=Ov.mispar_ishi 
           AND Yao.taarich = Activity.Taarich 
           AND Yao.mispar_ishi = Activity.mispar_ishi 
           AND Yao.Measher_O_Mistayeg IS NULL
            AND RelevantDetails.mispar_ishi = Ov.mispar_ishi
           AND RelevantDetails.snif_av = Snif.kod_snif_av(+) 
           AND Ov.kod_hevra = Snif.kod_hevra
           AND RelevantDetails.Ezor  =Ezor.kod_ezor(+)
           AND Ov.kod_hevra = Ezor.kod_hevra 
           --AND Yao.mispar_ishi IN( 205,206,42690 )
           AND Activity.sector_visa>=P_DISREGARDTYPE;
    -- ORDER BY Snif.teur_snif_av ,Yao.Mispar_ishi  ,Activity.shat_hatchala ; 
                    
DBMS_OUTPUT.PUT_LINE ( GeneralQry);
 
               EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END  Pro_Get_DisregardDrivers; 
 


 PROCEDURE Pro_Get_PremiotPresence(p_Cur OUT CurType ,
                                                        p_kod_premia IN MEAFYENIM_OVDIM.KOD_MEAFYEN%TYPE,
                                                        p_Period IN VARCHAR2,
                                                        p_ezor VARCHAR2,
                                                        P_MISPAR_ISHI IN VARCHAR2 ) 
IS 
 p_ToDate DATE ; 
  p_FromDate DATE ; 
BEGIN
      DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'Pro_Get_PremiotPresence');
      p_FromDate := TO_DATE('01/' || p_Period,'dd/mm/yyyy'); /* period= 05/2009=>  v_MinLimitDate = 01/05/2009 */
      p_ToDate := ADD_MONTHS(p_FromDate,1) -1 ;    /* period= 05/2009=>  v_MaxLimitDate = 31/05/2009 */

INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'p_ezorBBB',p_ezor);
  
    OPEN p_cur FOR  
            with k as
                    (SELECT SP.KOD_RACHIV_NOCHECHUT kod 
                      FROM CTB_SUGEY_PREMIOT Sp 
                       WHERE SP.KOD_PREMIA =  p_kod_premia)
             ,ov as
                 ( select DISTINCT MISPAR_ISHI, p_FromDate chodesh,kod_meafyen
                    from meafyenim_ovdim t
                    where t.kod_meafyen=p_kod_premia
                  -- and t.MISPAR_ISHI in (76105)
                     AND ((P_MISPAR_ISHI  IS NULL) OR  t.mispar_ishi IN (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MISPAR_ISHI,  ',') AS MYTABTYPE))) )           
                         AND NVL(ERECH_ishi,ERECH_MECHDAL_PARTANY)>0
                            and  (p_FromDate    BETWEEN ME_TAARICH  AND NVL(t.ad_TAARICH,TO_DATE(  '01/01/9999'  ,  'dd/mm/yyyy'  ))
                                  OR  p_ToDate   BETWEEN ME_TAARICH AND NVL(t.ad_TAARICH,TO_DATE(  '01/01/9999'  ,  'dd/mm/yyyy'  )) 
                                  OR (t.ME_TAARICH>=p_FromDate   AND NVL(t.ad_TAARICH,TO_DATE( '01/01/9999'   ,'dd/mm/yyyy'  ))<= p_ToDate)  )
                                  ),
             Details as
                 ( SELECT * 
                    FROM ( SELECT  t.mispar_ishi , MAX( t.ME_TARICH) OVER (PARTITION BY t.mispar_ishi) me_tarich, t.ad_tarich,t.SNIF_AV,t.EZOR, t.MAAMAD,t.ISUK, t.GIL,
                                         (o.shem_mish|| ' ' ||  o.shem_prat) Full_Name, O.KOD_HEVRA,  row_number() OVER (PARTITION BY t.mispar_ishi ORDER BY me_tarich desc) seq
                               FROM  PIVOT_PIRTEY_OVDIM t,ov,ovdim o
                               WHERE t.isuk IS NOT NULL 
                                    and  (p_FromDate  BETWEEN ME_TARICH  AND NVL(t.ad_TARICH,TO_DATE(  '01/01/9999'  ,  'dd/mm/yyyy'  ))
                                  OR  p_ToDate   BETWEEN ME_TARICH AND NVL(t.ad_TARICH,TO_DATE(  '01/01/9999'  ,  'dd/mm/yyyy'  )) 
                                  OR (t.ME_TARICH>=p_FromDate   AND NVL(t.ad_TARICH,TO_DATE( '01/01/9999'   ,'dd/mm/yyyy'  ))<= p_ToDate)  )
                              and  t.mispar_ishi=ov.mispar_ishi
                              and  t.mispar_ishi=o.mispar_ishi)a
                     WHERE a.seq=1)     
            ,b as                   
                  (SELECT  co.mispar_ishi, co.Taarich, max(co.bakasha_id) bakasha_id
                             FROM TB_CHISHUV_CHODESH_OVDIM co,TB_BAKASHOT b,ov,k
                             WHERE   B.BAKASHA_ID = CO.BAKASHA_ID
                                 --   and CO.MISPAR_ISHI=76105
                                    and CO.TAARICH = ov.chodesh 
                                    and CO.MISPAR_ISHI=ov.MISPAR_ISHI
                                  and CO.KOD_RECHIV = k.kod
                      group by    co.mispar_ishi,co.Taarich          )
   ,     Sidur as
( select So.MISPAR_ISHI,So.TAARICH,So.MISPAR_SIDUR,So.SHAT_HATCHALA, So.shat_gmar,  So.Pitzul_hafsaka,So.Chariga, So.Hashlama,So.Out_michsa ,So.TEUR_SIDUR_MEYCHAD,
                                      Prem.ERECH_RECHIV Dakot_nochechut,Prem.BAKASHA_ID,Prem.KOD_RECHIV,prem.ERECH_YOMI,Prem.num
  from                                
(         select S.MISPAR_ISHI,S.TAARICH,S.MISPAR_SIDUR,S.SHAT_HATCHALA, S.shat_gmar,  S.Pitzul_hafsaka,S.Chariga, S.Hashlama,S.Out_michsa ,sidurey_nochechut.TEUR_SIDUR_MEYCHAD
           from TB_SIDURIM_OVDIM S ,ov,
                    (SELECT  Ct.TEUR_SIDUR_MEYCHAD,CT.Kod_Sidur_Meyuchad,min(sm.Me_Taarich) Me_Taarich ,max(sm.Ad_Taarich) Ad_Taarich                               
                     FROM  CTB_SIDURIM_MEYUCHADIM CT , TB_SIDURIM_MEYUCHADIM SM
                     WHERE CT.Kod_Sidur_Meyuchad=SM. Mispar_sidur 
                          AND p_FromDate   between sm.Me_Taarich and sm.Ad_Taarich
                                  and  SM.Kod_Meafyen in(53,54,85)  
                     group by   Ct.TEUR_SIDUR_MEYCHAD,CT.Kod_Sidur_Meyuchad ) sidurey_nochechut      
             where  s.taarich BETWEEN  p_FromDate and p_ToDate
                and s.mispar_sidur =  sidurey_nochechut.Kod_Sidur_Meyuchad 
                and s.taarich BETWEEN sidurey_nochechut.Me_Taarich   AND sidurey_nochechut.Ad_Taarich
                and nvl(s.MISPAR_SIDUR,0) <>99200 
                and  s.mispar_ishi = ov.mispar_ishi
    ) So  ,    

(select  cs.*,  CY.ERECH_RECHIV ERECH_YOMI,
 DENSE_RANK() OVER(PARTITION BY  cs.mispar_ishi,cs.TAARICH   ORDER BY  cs.SHAT_HATCHALA  DESC  ) AS num       
from  TB_CHISHUV_SIDUR_OVDIM cs,TB_CHISHUV_yomi_OVDIM cy , b,k
where b.mispar_ishi= cs.mispar_ishi
    and b.taarich=trunc(cs.taarich,'MM')
    and b.bakasha_id= cs.bakasha_id
    and k.kod= CS.KOD_RECHIV
    and cs.mispar_ishi= cy.mispar_ishi
    and cs.taarich=cy.taarich
    and cs.bakasha_id= cy.bakasha_id
    and cs.KOD_RECHIV= Cy.KOD_RECHIV ) Prem

where       Prem.MISPAR_ISHI = So.MISPAR_ISHI
         and Prem.TAARICH = So.TAARICH  
         and Prem.mispar_sidur = So.mispar_sidur  
         and Prem.SHAT_HATCHALA = So.SHAT_HATCHALA  
)

select YAO.MISPAR_ISHI,YAO.TAARICH,YAO.STATUS, Dayofweek(Yao.taarich)   Dayofweek,
          Yao.Hamarat_shabat,Yao.LINA,Yao.BITUL_ZMAN_NESIOT,Yao.HALBASHA,
          sidur.Dakot_nochechut,Decode(sidur.num,1,sidur.ERECH_YOMI,'') ERECH_YOMI,b.BAKASHA_ID,sidur.MISPAR_SIDUR, sidur.shat_hatchala ,
          sidur.shat_gmar,  sidur.Pitzul_hafsaka,sidur.Chariga, sidur.Hashlama,sidur.Out_michsa ,
          Gil.Teur_kod_gil , Isuk.teur_isuk, Maamad.teur_maamad_hr, Ezor.Teur_ezor, Snif.teur_snif_av ,
          Details.kod_hevra, Details.Full_Name ,  
           decode(sidur.TEUR_SIDUR_MEYCHAD,'','',  sidur.TEUR_SIDUR_MEYCHAD || ' ' || sidur.MISPAR_SIDUR ) TEUR_SIDUR_MEYCHAD,
          (Pkg_Ovdim.fun_get_meafyen_oved(Ov.mispar_ishi, 56, p_ToDate)) Wording_date, 
           pkg_reports.fun_get_hour( trim (Pkg_Ovdim.fun_get_meafyen_oved(Ov.mispar_ishi, 3,p_ToDate ))) START_TIME_ALLOWED ,
           pkg_reports.fun_get_hour( trim (Pkg_Ovdim.fun_get_meafyen_oved(Ov.mispar_ishi, 4, p_ToDate))) END_TIME_ALLOWED,
           pkg_reports.FUN_GET_ERECH_RECHIV(Ov.mispar_ishi,Yao.taarich ,b.Bakasha_ID,126) michsa_yomit,
           pr.TEUR_PREMIA
from
           TB_YAMEY_AVODA_OVDIM Yao ,
           Sidur, ov, Details,b,
           CTB_EZOR Ezor,
           CTB_SNIF_AV Snif ,
           CTB_MAAMAD  Maamad ,
           CTB_ISUK  Isuk, 
           CTB_KOD_GIL Gil ,
           CTB_SUGEY_PREMIOT pr   
 where
              YAO.MISPAR_ISHI = OV.MISPAR_ISHI
               and Yao.taarich BETWEEN  p_FromDate  and p_ToDate
               and YAO.MISPAR_ISHI =  sidur.MISPAR_ISHI (+) 
               and YAO.TAARICH =sidur.TAARICH (+)    
               and yao.mispar_ishi=b.mispar_ishi
               and trunc(yao.taarich,'MM')=b.taarich
               and YAO.MISPAR_ISHI = Details.MISPAR_ISHI
               and Details.GIL = Gil.kod_gil_hr 
               and Ezor.kod_ezor  = Details.ezor 
               and Ezor.kod_hevra = Details.kod_hevra
              -- and ( P_Ezor is null or Details.EZOR in (P_Ezor))
               AND ((P_Ezor  IS NULL) OR Details.EZOR IN (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_Ezor,  ',') AS MYTABTYPE))) )      
               and Snif.kod_snif_av  = details.snif_av  
               and Snif.kod_hevra = Details.kod_hevra 
               and Isuk.kod_isuk  = details.isuk        
               and Isuk.kod_hevra = Details.kod_hevra   
               and maamad.kod_maamad_hr  =  details.maamad  
               and maamad.KOD_HEVRA =  Details.kod_hevra
               and pr.KOD_PREMIA   =  p_kod_premia
 order by YAO.MISPAR_ISHI,YAO.TAARICH,sidur.shat_hatchala,sidur.shat_gmar  ;        

               EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;         
end   Pro_Get_PremiotPresence;

PROCEDURE Pro_PremiotPresence_tenderim(p_Cur OUT CurType ,
                                                        p_kod_premia IN MEAFYENIM_OVDIM.KOD_MEAFYEN%TYPE,  
                                                         p_Period IN VARCHAR2,
                                                        p_ezor VARCHAR2,
                                                        P_MISPAR_ISHI IN VARCHAR2 ) 
IS 
 p_ToDate DATE ; 
  p_FromDate DATE ; 
BEGIN
      DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'Pro_PremiotPresence_tenderim');
      p_FromDate := TO_DATE('01/' || p_Period,'dd/mm/yyyy'); /* period= 05/2009=>  v_MinLimitDate = 01/05/2009 */
      p_ToDate := ADD_MONTHS(p_FromDate,1) -1 ;    /* period= 05/2009=>  v_MaxLimitDate = 31/05/2009 */

    OPEN p_cur FOR  
        with  Details as
         ( SELECT * 
            FROM ( SELECT  t.mispar_ishi , MAX( t.ME_TARICH) OVER (PARTITION BY t.mispar_ishi) me_tarich, t.ad_tarich,t.SNIF_AV,t.EZOR, t.MAAMAD,t.ISUK, t.GIL,
                                 (o.shem_mish|| ' ' ||  o.shem_prat) Full_Name, O.KOD_HEVRA,  row_number() OVER (PARTITION BY t.mispar_ishi ORDER BY me_tarich desc) seq
                       FROM  PIVOT_PIRTEY_OVDIM t,ovdim o
                       WHERE t.isuk IS NOT NULL 
                            and  (p_FromDate   BETWEEN ME_TARICH  AND NVL(t.ad_TARICH,TO_DATE(  '01/01/9999'  ,  'dd/mm/yyyy'  ))
                          OR p_ToDate   BETWEEN ME_TARICH AND NVL(t.ad_TARICH,TO_DATE(  '01/01/9999'  ,  'dd/mm/yyyy'  )) 
                          OR (t.ME_TARICH>=p_FromDate   AND NVL(t.ad_TARICH,TO_DATE( '01/01/9999'   ,'dd/mm/yyyy'  ))<= p_ToDate)  )
                     AND ((P_MISPAR_ISHI  IS NULL) OR  t.mispar_ishi IN (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MISPAR_ISHI,  ',') AS MYTABTYPE))) )           
                      and  t.mispar_ishi=o.mispar_ishi)a
             WHERE a.seq=1)     ,
            Sidur as
                    (select s.MISPAR_ISHI,S.TAARICH,S.MISPAR_SIDUR,S.SHAT_HATCHALA, S.shat_gmar,  S.Pitzul_hafsaka,S.Chariga, S.Hashlama,S.Out_michsa ,s.TEUR_SIDUR_MEYCHAD,
                             s.nochechut Dakot_nochechut, sum(s.nochechut) over(partition by s.MISPAR_ISHI,S.TAARICH) erech_yomi,
                              DENSE_RANK() OVER(PARTITION BY  s.mispar_ishi,s.TAARICH   ORDER BY  s.SHAT_HATCHALA  DESC  ) AS num       
                    from (  select s.* , CT.TEUR_SIDUR_MEYCHAD ,
                        TO_NUMBER( TO_CHAR(  (shat_gmar-shat_hatchala)*1440,'999.99') ) nochechut
                 --    to_number( (shat_gmar-shat_hatchala)*1440) nochechut
                         from tb_sidurim_ovdim s,CTB_SIDURIM_MEYUCHADIM CT
                         where s.taarich between p_FromDate and p_ToDate
                         and S.MISPAR_SIDUR=99229
                         and S.LO_LETASHLUM=0
                         and S.MISPAR_SIDUR=CT.KOD_SIDUR_MEYUCHAD
                         AND ((P_MISPAR_ISHI  IS NULL) OR  s.MISPAR_ISHI IN (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MISPAR_ISHI,  ',') AS MYTABTYPE))) )           
                    union   
                         select s.*, '' TEUR_SIDUR_MEYCHAD ,to_number(  substr(P.MAKAT_NESIA,4,3))  nochechut
                             from  tb_sidurim_ovdim s,tb_peilut_ovdim p
                             where  s.mispar_ishi= P.MISPAR_ISHI
                              and s.taarich= P.TAARICH
                              and s.mispar_sidur=p.mispar_sidur
                               and S.MISPAR_SIDUR<>99229
                              and S.SHAT_HATCHALA= P.SHAT_HATCHALA_SIDUR
                              and p.makat_nesia like '737%00'
                              and length(P.MAKAT_NESIA)=8
                              and S.LO_LETASHLUM=0
                              and p.taarich between  p_FromDate and p_ToDate
                              AND ((P_MISPAR_ISHI  IS NULL) OR  s.MISPAR_ISHI IN (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MISPAR_ISHI,  ',') AS MYTABTYPE))) )           
                        ) s
                    )
            ,ov_tender as
            (select  mispar_ishi, trunc(taarich,'MM') taarich  from Sidur group by mispar_ishi, trunc(taarich,'MM'))
       ,b as                   
                  (SELECT  co.mispar_ishi, co.Taarich, max(co.bakasha_id) bakasha_id
                             FROM TB_CHISHUV_CHODESH_OVDIM co,TB_BAKASHOT b,ov_tender
                             WHERE   B.BAKASHA_ID = CO.BAKASHA_ID
                                   and CO.TAARICH = ov_tender.taarich 
                                and CO.MISPAR_ISHI=ov_tender.MISPAR_ISHI
                      group by    co.mispar_ishi,co.Taarich          )         
select YAO.MISPAR_ISHI,YAO.TAARICH,YAO.STATUS, b.Bakasha_ID,Dayofweek(Yao.taarich)   Dayofweek,
          Yao.Hamarat_shabat,Yao.LINA,Yao.BITUL_ZMAN_NESIOT,Yao.HALBASHA,
          sidur.Dakot_nochechut,Decode(sidur.num,1,sidur.ERECH_YOMI,'') ERECH_YOMI,sidur.MISPAR_SIDUR, sidur.shat_hatchala ,
          sidur.shat_gmar,  sidur.Pitzul_hafsaka,sidur.Chariga, sidur.Hashlama,sidur.Out_michsa ,
          Gil.Teur_kod_gil , Isuk.teur_isuk, Maamad.teur_maamad_hr, Ezor.Teur_ezor, Snif.teur_snif_av ,
          Details.kod_hevra, Details.Full_Name ,  
           decode(sidur.TEUR_SIDUR_MEYCHAD,'','',  sidur.TEUR_SIDUR_MEYCHAD || ' ' || sidur.MISPAR_SIDUR ) TEUR_SIDUR_MEYCHAD,
          (Pkg_Ovdim.fun_get_meafyen_oved(Yao.mispar_ishi, 56,p_ToDate)) Wording_date, 
           pkg_reports.fun_get_hour( trim (Pkg_Ovdim.fun_get_meafyen_oved(Yao.mispar_ishi, 3,p_ToDate ))) START_TIME_ALLOWED ,
           pkg_reports.fun_get_hour( trim (Pkg_Ovdim.fun_get_meafyen_oved(Yao.mispar_ishi, 4,p_ToDate))) END_TIME_ALLOWED,
          pkg_reports.FUN_GET_ERECH_RECHIV(Yao.mispar_ishi,Yao.taarich ,b.Bakasha_ID,126) michsa_yomit,
           pr.TEUR_PREMIA
from
           TB_YAMEY_AVODA_OVDIM Yao ,
           Sidur,  Details,ov_tender,b,
           CTB_EZOR Ezor,
           CTB_SNIF_AV Snif ,
           CTB_MAAMAD  Maamad ,
           CTB_ISUK  Isuk, 
           CTB_KOD_GIL Gil ,
           CTB_SUGEY_PREMIOT pr   
 where
              yao.mispar_ishi=ov_tender.mispar_ishi
              and yao.mispar_ishi=b.mispar_ishi(+)
               and trunc(yao.taarich,'MM')=b.taarich(+)
               and   Yao.taarich BETWEEN   p_FromDate and p_ToDate
               and YAO.MISPAR_ISHI =  sidur.MISPAR_ISHI (+)
               and YAO.TAARICH =sidur.TAARICH (+)    
               and YAO.MISPAR_ISHI = Details.MISPAR_ISHI
               and Details.GIL = Gil.kod_gil_hr 
               and Ezor.kod_ezor  = Details.ezor 
               and Ezor.kod_hevra = Details.kod_hevra
              AND ((P_Ezor  IS NULL) OR Details.EZOR IN (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_Ezor,  ',') AS MYTABTYPE))) )      
               and Snif.kod_snif_av  = details.snif_av  
               and Snif.kod_hevra = Details.kod_hevra 
               and Isuk.kod_isuk  = details.isuk        
               and Isuk.kod_hevra = Details.kod_hevra   
               and maamad.kod_maamad_hr  =  details.maamad  
               and maamad.KOD_HEVRA =  Details.kod_hevra
               and pr.KOD_PREMIA   =  p_kod_premia
 order by YAO.MISPAR_ISHI,YAO.TAARICH,sidur.shat_hatchala,sidur.shat_gmar;         

               EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;         
end   Pro_PremiotPresence_tenderim;
 
 /************************************/
 FUNCTION fun_get_hour( shaa varchar2) return VARCHAR2 is
 
 ihour number;
 new_hour varchar2(5);
 begin
    if(shaa is null or shaa ='') then
        return '';
    else 
        new_hour:= lpad(shaa,4,'0');
        ihour:= to_number(substr(new_hour,0,2));
        if (ihour>23) then
         --   ihour:=ihour-24;
          new_hour:='23:59';
        else
            new_hour:=lpad(ihour,2,'0') || ':' || substr(new_hour,3,2);
        end if;    
      
        return new_hour;
    end if;
 end fun_get_hour;
 /***************************************************************/
 FUNCTION FUN_GET_ERECH_RECHIV(p_mispar_ishi number, p_taarich DATE,p_bakasha_id number, p_kod_rchiv number) return VARCHAR2 is
 erech VARCHAR2(20);
 BEGIN
        select to_char(y.Erech_rechiv) into erech
        from TB_CHISHUV_YOMI_OVDIM y
        where y.mispar_ishi=p_mispar_ishi
            and y.taarich=p_taarich
            and y.bakasha_id=p_bakasha_id
            and y.kod_rechiv=p_kod_rchiv;
      return   erech;
      
      EXCEPTION
           WHEN NO_DATA_FOUND  THEN
                  return '';
 END FUN_GET_ERECH_RECHIV;
 
 /*********************/
 PROCEDURE pro_get_rizot_chishuv_succeed(  P_CHODESH_HARAZA  IN VARCHAR2,
                                                                                           p_Cur OUT CurType)  IS

  p_ToDate DATE ; 
  p_FromDate DATE ; 
  --  p_Erech TB_Parametrim.erech_param%TYPE ;
  BEGIN
IF  ( trim(P_CHODESH_HARAZA) != '-1' ) THEN  
      p_FromDate := TO_DATE('01/' || P_CHODESH_HARAZA ,'dd/mm/yyyy'); /* period= 05/2009=>  v_MinLimitDate = 01/05/2009 */
     p_ToDate := ADD_MONTHS(p_FromDate,1) -1 ;
     END IF ;   
 
    OPEN p_cur FOR   
        SELECT BAKASHA_ID,Pirtey_Riza FROM(
                 SELECT -1 BAKASHA_ID, '' Pirtey_Riza, 1 ord , SYSDATE  ZMAN_HATCHALA FROM dual 
            UNION
                 SELECT TB.BAKASHA_ID, TB.BAKASHA_ID || ' -  ' || TB.TEUR || ' -  ' || 
                   REPLACE(TB.ZMAN_HATCHALA,'-',' ') || (CASE     TB.HUAVRA_LESACHAR WHEN '1' THEN  ' -  ����� ���� '  ELSE ''  END)  Pirtey_Riza , 2 ord , TB.ZMAN_HATCHALA ZMAN_HATCHALA
                   FROM  TB_BAKASHOT TB  
                   WHERE  TB.STATUS=2 AND
                   TB.SUG_BAKASHA=1 AND
                   (  (trim(P_CHODESH_HARAZA) != '-1'  AND  (TB.ZMAN_HATCHALA   BETWEEN p_FromDate AND p_ToDate) ) OR trim(P_CHODESH_HARAZA) = '-1')
                   ORDER BY ord , ZMAN_HATCHALA);


     EXCEPTION 
                WHEN OTHERS THEN 
                       RAISE;         
 END  pro_get_rizot_chishuv_succeed;
  /***************************************************************/
PROCEDURE  pro_get_maamadot(p_Cur OUT CurType )  IS
BEGIN

    OPEN p_Cur FOR 
      
        SELECT '�����' description,1 kod  FROM dual 
    UNION 
        SELECT '������' , 2  FROM dual;
                         
 EXCEPTION 
                WHEN OTHERS THEN 
                       RAISE;         
END   pro_get_maamadot;
 /***************************************************************/
PROCEDURE pro_get_AbsenceType( p_Cur OUT CurType ) IS 
BEGIN 
OPEN p_Cur FOR
SELECT KOD_HEADRUT_CLALI, TEUR_CLALI FROM 
(
SELECT -1 KOD_HEADRUT_CLALI , '��� ' TEUR_CLALI  , 1 ord FROM dual
UNION  
SELECT  DISTINCT CTB.KOD_HEADRUT_CLALI, CTB.TEUR_CLALI, 2 ord FROM
    CTB_SUGEY_HEADRUYUT Ctb   WHERE CTB.KOD_HEADRUT_CLALI IS NOT NULL 
    ORDER BY ord, TEUR_CLALI
    ) ; 

    
EXCEPTION 
                WHEN OTHERS THEN 
                       RAISE;         
END   pro_get_AbsenceType;

 /***************************************************************/
PROCEDURE pro_get_WorkDayList( p_Cur OUT CurType ) IS 
BEGIN 
OPEN p_Cur FOR
SELECT KOD,Description FROM 
(
SELECT -1 KOD , '��� ' Description  , 1 ord FROM dual
UNION  
SELECT 5 KOD , '5 ���� ' Description  , 2 ord FROM dual
UNION
SELECT 6 KOD , '6 ���� ' Description  , 3 ord FROM dual
    ORDER BY ord, Description
 ) ;
EXCEPTION 
                WHEN OTHERS THEN 
                       RAISE;         
END   pro_get_WorkDayList;
 /***************************************************************/
PROCEDURE  pro_get_pirtey_rizot(listMispareiRizot IN VARCHAR2,
                                                                              IdReport IN NUMBER,
                                                                      strChodashim IN VARCHAR2,
                                                                   p_Cur OUT CurType )  IS
BEGIN

          OPEN p_Cur FOR 
                 SELECT  TB.BAKASHA_ID, TB.TEUR, TB.ZMAN_HATCHALA, TB.HUAVRA_LESACHAR ,               
                    CASE IdReport WHEN 1 THEN '���� ���� ����' ELSE '���� ���� ����'  END koteret, 
                CASE strChodashim WHEN NULL THEN NULL ELSE  SUBSTR(strChodashim,0,7)   END chodesh
            FROM TB_BAKASHOT TB  
               WHERE TB.BAKASHA_ID =SUBSTR(listMispareiRizot,0,INSTR(listMispareiRizot,',')-1)  
UNION 
      
      SELECT  TB.BAKASHA_ID, TB.TEUR, TB.ZMAN_HATCHALA, TB.HUAVRA_LESACHAR ,               
                    CASE IdReport WHEN 1 THEN '���� ���� �������' ELSE  '���� ���� ����� �������' END koteret, 
                CASE strChodashim WHEN NULL THEN NULL ELSE  SUBSTR(strChodashim,9,7)   END chodesh
           FROM TB_BAKASHOT TB  
               WHERE TB.BAKASHA_ID =SUBSTR(listMispareiRizot,INSTR(listMispareiRizot,',')+1); 
                /*  SELECT  TB.BAKASHA_ID, TB.TEUR, TB.ZMAN_HATCHALA, TB.HUAVRA_LESACHAR ,
                   case IdReport when 1 then 
                          ( case instr(listMispareiRizot,',' || to_char(TB.BAKASHA_ID))  when 0 then ':���� ���� ����' else ':���� ���� �������'  end)
                   else
                         (     case instr(listMispareiRizot,',' || to_char(TB.BAKASHA_ID))  when 0 then ':���� ���� ����' else ':���� ���� ����� �������'  end) end koteret,
               case strChodashim when null then null
               else ( case instr(listMispareiRizot,',' || to_char(TB.BAKASHA_ID))  when 0 then  substr(strChodashim,0,7)  else substr(strChodashim,9,7)  end) end chodesh
            --   case  instr(strChodashim,',')   when 7 then substr(strChodashim,0,7) else substr(strChodashim,7,7) )end chodesh
        --       case instr(listMispareiRizot,',' || to_char(TB.BAKASHA_ID))  when 0 then '���� ���� ����:' else '���� ���� �������:' end koteret
               FROM TB_BAKASHOT TB  
               WHERE TB.BAKASHA_ID in (select x from table(cast(convert_string_to_table(listMispareiRizot ,  ',') as mytabtype)));
               */      
END pro_get_pirtey_rizot;
 /***************************************************************/
PROCEDURE  pro_getNetuneyHashvaatRizot( P_CHODESH IN VARCHAR2,
                                                                                                              P_MIS_RITZA IN INTEGER,
                                                                                                          P_MIS_RITZA_LEHASVAA IN INTEGER, 
                                                                                                      P_MAMAD IN VARCHAR2,
                                                                                                      P_ISUK IN VARCHAR2,
                                                                                                      P_MISPAR_ISHI IN VARCHAR2,
                                                                                           p_Cur OUT CurType )  IS
                                                                   
BEGIN
      DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_getNetuneyHashvaatRizot');
                OPEN p_Cur FOR     
                    SELECT H.TAARICH,H.MISPAR_ISHI, H.KOD_RECHIV,H.XX,H.YY,H.HEFRESH,
                                      OV.SHEM_MISH || ' ' ||  OV.SHEM_PRAT  FULL_NAME ,
                                       I. TEUR_ISUK ISUK,M.TEUR_MAAMAD_HR MAAMAD, E.TEUR_EZOR EZOR, S.TEUR_SNIF_AV SNIF,R.TEUR_RECHIV
                    FROM
                                       PIVOT_PIRTEY_OVDIM T ,
                                        OVDIM OV,
                                        CTB_MAAMAD M,
                                        CTB_ISUK I,
                                        CTB_EZOR E,
                                         CTB_SNIF_AV S,
                                         CTB_RECHIVIM R,
                                            (SELECT DECODE(A.TAARICH,NULL,B.TAARICH,A.TAARICH) TAARICH,                 
                                                                DECODE(A.MISPAR_ISHI,NULL,B.MISPAR_ISHI,A.MISPAR_ISHI) MISPAR_ISHI , 
                                                               DECODE(A.KOD_RECHIV,NULL,B.KOD_RECHIV,A.KOD_RECHIV) KOD_RECHIV,
                                                               NVL(A.ERECH_RECHIV,0) XX,NVL(B.ERECH_RECHIV,0) YY,
                                                                (NVL(A.ERECH_RECHIV,0)-NVL(B.ERECH_RECHIV,0) ) HEFRESH
                                              FROM
                                                        (SELECT A.MISPAR_ISHI,A.KOD_RECHIV,A.ERECH_RECHIV,A.TAARICH
                                                        FROM  TB_CHISHUV_CHODESH_OVDIM A
                                                        WHERE A.BAKASHA_ID = P_MIS_RITZA
                                                            AND (TO_CHAR(A.TAARICH,'mm/yyyy')=P_CHODESH OR P_CHODESH = '-1')
                                                            AND ((P_MISPAR_ISHI  IS NULL) OR A.MISPAR_ISHI IN (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MISPAR_ISHI,  ',') AS MYTABTYPE))) )) A
                                            FULL JOIN
                                                    (SELECT B.MISPAR_ISHI, B.KOD_RECHIV,B.ERECH_RECHIV,B.TAARICH
                                                    FROM  TB_CHISHUV_CHODESH_OVDIM B
                                                    WHERE B.BAKASHA_ID =P_MIS_RITZA_LEHASVAA
                                                        AND (TO_CHAR(B.TAARICH,'mm/yyyy')=P_CHODESH OR P_CHODESH = '-1')
                                                        AND ( ( P_MISPAR_ISHI  IS NULL) OR B.MISPAR_ISHI IN (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MISPAR_ISHI ,  ',') AS MYTABTYPE)))  )) B
                                            ON B.MISPAR_ISHI=A.MISPAR_ISHI
                                                            AND  B.KOD_RECHIV=A.KOD_RECHIV
                                                            AND B.TAARICH=A.TAARICH)     H
                    WHERE    
                                         H.MISPAR_ISHI = OV.MISPAR_ISHI (+)
                                        AND H.MISPAR_ISHI = T.MISPAR_ISHI
                                        AND T.MAAMAD =   M.KOD_MAAMAD_HR 
                                         AND OV.KOD_HEVRA=M.KOD_HEVRA
                                        AND T.ISUK = I.KOD_ISUK 
                                        AND OV.KOD_HEVRA=I.KOD_HEVRA
                                        AND T.EZOR = E.KOD_EZOR
                                        AND OV.KOD_HEVRA=E.KOD_HEVRA
                                        AND T.SNIF_AV = S.KOD_SNIF_AV
                                        AND OV.KOD_HEVRA=S.KOD_HEVRA
                                        AND H.KOD_RECHIV = R.KOD_RECHIV 
                                        AND (ADD_MONTHS(H.TAARICH,1) -1) BETWEEN T.ME_TARICH AND T.AD_TARICH
                                        AND (  (P_ISUK IS NULL) OR T.ISUK IN (SELECT X FROM TABLE(CAST(Convert_String_To_Table(P_ISUK ,  ',') AS MYTABTYPE)))    )
                                         AND ( (P_MAMAD = '-1') OR SUBSTR(T.MAAMAD,0,1) IN (SELECT X FROM TABLE(CAST(Convert_String_To_Table(P_MAMAD ,  ',') AS MYTABTYPE)))     )
                        
                        ORDER BY H.MISPAR_ISHI,H.TAARICH,R.TEUR_RECHIV;
                            
                        EXCEPTION 
                WHEN OTHERS THEN 
                       RAISE;                                                                                     
END pro_getNetuneyHashvaatRizot;                                                                                            

 /***************************************************************/
PROCEDURE pro_get_rizot_chishuv_lehodesh(  P_CHODESH_HARAZA  IN VARCHAR2,
                                                                                           p_Cur OUT CurType) IS
                                                                                                                                                    
       p_FromDate DATE ; 

  BEGIN
       DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_get_rizot_chishuv_lehodesh');
IF  ( trim(P_CHODESH_HARAZA) != '-1' ) THEN  
    p_FromDate := TO_DATE('01/' || P_CHODESH_HARAZA ,'dd/mm/yyyy'); /* period= 05/2009=>  v_MinLimitDate = 01/05/2009 */
     END IF ;   
 
    OPEN p_cur FOR   
        SELECT BAKASHA_ID,Pirtey_Riza FROM(
                 SELECT -1 BAKASHA_ID, '' Pirtey_Riza, 1 ord , SYSDATE  ZMAN_HATCHALA FROM dual 
            UNION
                 SELECT TB.BAKASHA_ID, TB.BAKASHA_ID || ' -  ' || TB.TEUR || ' -  ' || 
                   REPLACE(TB.ZMAN_HATCHALA,'-',' ') || (CASE     TB.HUAVRA_LESACHAR WHEN '1' THEN  ' -  ����� ���� '  ELSE ''  END)  Pirtey_Riza , 2 ord , TB.ZMAN_HATCHALA ZMAN_HATCHALA
                   FROM  TB_BAKASHOT TB ,
                                      TB_BAKASHOT_PARAMS P
                   WHERE  TB.STATUS=2 AND
                                          TB.SUG_BAKASHA= 1 AND
                                           TB.BAKASHA_ID = P.BAKASHA_ID AND
                                          P.ERECH = P_CHODESH_HARAZA and
                          --        (ADD_MONTHS( TO_DATE('01/' || P.ERECH, 'dd/mm/yyyy') ,1) -1    >= p_FromDate)  AND
                                      P.PARAM_ID =2
        --           (  (trim(P_CHODESH_HARAZA) != '-1'  and  (TB.ZMAN_HATCHALA   BETWEEN p_FromDate And p_ToDate) ) or trim(P_CHODESH_HARAZA) = '-1')
                   ORDER BY ord , ZMAN_HATCHALA DESC);


     EXCEPTION 
                WHEN OTHERS THEN 
                       RAISE;    
      
END  pro_get_rizot_chishuv_lehodesh;

 /***************************************************************/
PROCEDURE  pro_get_ramot_chishuv_letezuga(p_Cur OUT CurType )  IS
BEGIN

               OPEN p_Cur FOR 
                              SELECT '����' description,1kod  FROM dual 
                         UNION 
                          SELECT '�����' , 2  FROM dual;
                         
 EXCEPTION 
                WHEN OTHERS THEN 
                       RAISE;         
END   pro_get_ramot_chishuv_letezuga;

 /***************************************************************/
PROCEDURE  pro_GetHashvaatChodsheyRizot( P_CHODESH IN VARCHAR2,
                                                                                                           P_MIS_RITZA IN INTEGER,
                                                                                                       P_CHODESH_LEHASHVAA IN VARCHAR2,
                                                                                                   P_MIS_RITZA_LEHASVAA IN INTEGER,
                                                                                                   P_RAMA_LETEZUGA IN VARCHAR2,
                                                                                                    p_Cur OUT CurType )  IS
                                                                   
BEGIN
       DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_GetHashvaatChodsheyRizot');
                OPEN p_Cur FOR     
                        SELECT    H.*    ,R.TEUR_RECHIV, I. TEUR_ISUK ,SUBSTR(maamad,0,1) MAMAD
                        FROM
                                               CTB_ISUK I,
                                            CTB_RECHIVIM R,
                                            OVDIM O,
                                            (     SELECT   DECODE(A.MISPAR_ISHI,NULL,B.MISPAR_ISHI,A.MISPAR_ISHI) MISPAR_ISHI    ,            
                                                                      NVL(A.ERECH_RECHIV,0)  EA,  NVL(B.ERECH_RECHIV,0) EB,
                                                                      DECODE(A.KOD_RECHIV,NULL,B.KOD_RECHIV,A.KOD_RECHIV)KOD,
                                                                      NVL(CA,0) CA ,  NVL(CB,0) CB,
                                                                      NVL(A.MAAMAD, B.MAAMAD) maamad,  
                                                                      DECODE(A.ISUK,NULL,B.ISUK,A.ISUK) ISUK                                                              
                                                                                                                       
                                                     FROM
                                                                (        SELECT A.MISPAR_ISHI ,  A.ERECH_RECHIV , A.KOD_RECHIV,T.MAAMAD,T.ISUK, COUNT(A.ERECH_RECHIV) CA
                                                                        FROM  TB_CHISHUV_CHODESH_OVDIM A,
                                                                                                                  PIVOT_PIRTEY_OVDIM T                         
                                                                        WHERE A.BAKASHA_ID =  P_MIS_RITZA    AND
                                                                                         A.TAARICH=TO_DATE('01/' || P_CHODESH,'dd/mm/yyyy')        AND
                                                                                         A.MISPAR_ISHI =T.MISPAR_ISHI(+)     AND 
                                                                                        (ADD_MONTHS(A.TAARICH,1) -1) BETWEEN T.ME_TARICH(+) AND T.AD_TARICH(+) 
                                                                                        GROUP BY     A.MISPAR_ISHI ,  A.ERECH_RECHIV , A.KOD_RECHIV,T.MAAMAD,T.ISUK   ) A
                                                                
                                                                FULL JOIN                        
                                                                                        
                                                                        ( SELECT   B.MISPAR_ISHI ,B.ERECH_RECHIV , B.KOD_RECHIV,T.MAAMAD,T.ISUK, COUNT(B.ERECH_RECHIV) CB
                                                                         FROM  TB_CHISHUV_CHODESH_OVDIM B,
                                                                                                                  PIVOT_PIRTEY_OVDIM T                         
                                                                        WHERE B.BAKASHA_ID =P_MIS_RITZA_LEHASVAA AND
                                                                                         B.TAARICH=TO_DATE('01/' || P_CHODESH_LEHASHVAA,'dd/mm/yyyy')        AND
                                                                                         B.MISPAR_ISHI =T.MISPAR_ISHI(+)     AND 
                                                                                        (ADD_MONTHS(B.TAARICH,1) -1) BETWEEN T.ME_TARICH(+) AND T.AD_TARICH(+)    
                                                                                        GROUP BY     B.MISPAR_ISHI ,B.ERECH_RECHIV , B.KOD_RECHIV,T.MAAMAD,T.ISUK ) B
                                                                                        
                                                                ON         
                                                                                   A.MISPAR_ISHI=B.MISPAR_ISHI
                                                                    AND   A.KOD_RECHIV=B.KOD_RECHIV
                                                                    AND A.ISUK =B.ISUK
                                                                    AND A.MAAMAD= B.MAAMAD 
                                            )H
WHERE
                  H.KOD = R.KOD_RECHIV  
    AND  H.ISUK = I.KOD_ISUK
    AND I.KOD_HEVRA = O.KOD_HEVRA
    AND H.MISPAR_ISHI = O.MISPAR_ISHI
    
        ORDER BY  R.TEUR_RECHIV;

                                                                                           
                                                                                             
                         
            EXCEPTION 
                WHEN OTHERS THEN 
                       RAISE;                                                                  
END  pro_GetHashvaatChodsheyRizot;
 /***************************************************************/
PROCEDURE  pro_get_reports_list(P_PROFIL  IN VARCHAR2,
                                                                             p_Cur OUT CurType )  IS
BEGIN


               OPEN p_Cur FOR 
                              SELECT DISTINCT S.KOD_SUG_DOCH, s.SHEM_DOCH_BAKOD ,s.TEUR_DOCH  || '('|| S.KOD_SUG_DOCH ||')'  TEUR_DOCH , 
                              S.TEUR_DOCH TEUR_DOCH_2, S.RS_VERSION, r.URL_CONFIG_KEY, r.SERVICE_URL_CONFIG_KEY,r.EXTENSION 
                         FROM   CTB_SUGEY_DOCHOT s,
                                TB_PROFIL_DOCHOT p,
                                CTB_DOCHOT_RS_VERSION r
                         WHERE s.KOD_SUG_DOCH = p.KOD_SUG_DOCH
                                AND s.pail=1
                                and P.PAIL=1
                                AND(P_PROFIL IS NOT NULL AND p.KOD_PROFIL IN (SELECT X FROM TABLE(CAST(Convert_String_To_Table(P_PROFIL ,  ',') AS MYTABTYPE))) )
                                and S.RS_VERSION = r.RS_VERSION
                        ORDER BY     TEUR_DOCH;

                         
 EXCEPTION 
                WHEN OTHERS THEN 
                       RAISE;         
END   pro_get_reports_list;

 /***************************************************************/
PROCEDURE  pro_GetNetuneyIdkuneyRashemet( P_MIS_RASHEMET IN INTEGER,
                                                                                                      P_TAARICH_CA IN VARCHAR2,  -- CA=Cartis Avoda
                                                                                                       P_SHAA IN VARCHAR2,
                                                                                                    p_Cur OUT CurType )  IS
            TaarichIdkun DATE;                                                       
BEGIN
       DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_GetNetuneyIdkuneyRashemet');
            TaarichIdkun := TO_DATE(P_TAARICH_CA || ' ' || P_SHAA || ':00' ,'dd/mm/yyyy HH24:mi:ss'); 
             
            OPEN p_Cur FOR     
                  SELECT    Y. MISPAR_ISHI,Y.MISPAR_SIDUR, TO_CHAR( Y.TAARICH_IDKUN,'dd/mm/yyyy HH24:mi:ss') TAARICH_IDKUN,TO_CHAR(Y.TAARICH,'dd/mm/yyyy') TAARICH
                  FROM TB_IDKUN_RASHEMET  Y
                  WHERE Y.GOREM_MEADKEN=TO_NUMBER(P_MIS_RASHEMET) AND
                        Y.TAARICH_IDKUN >= TaarichIdkun and  Y.TAARICH_IDKUN < (TO_DATE(P_TAARICH_CA,'dd/mm/yyyy')+1)
                GROUP BY Y.TAARICH_IDKUN ,Y.TAARICH,Y.MISPAR_ISHI ,Y.MISPAR_SIDUR  
                ORDER BY Y.TAARICH_IDKUN;
                --  ORDER BY Y.TAARICH_IDKUN ,Y.TAARICH,MISPAR_ISHI ,Y.MISPAR_SIDUR;                                        
                  
         EXCEPTION 
                WHEN OTHERS THEN 
                       RAISE;                     
            

END pro_GetNetuneyIdkuneyRashemet;

PROCEDURE PREPARE_DAYLIST_OF_RPT( CurrentMonth VARCHAR2 )  IS 

  CurrentDay VARCHAR2(2) ;
  PreviousDay VARCHAR2(2);
 
  StrAllTheDateList VARCHAR(300);
  StrList VARCHAR(300);
  CoutiniousList BOOLEAN ;
  LastBakasha number ;
    p_FromDate date ;
    p_ToDate date ; 

  CDate TB_CHISHUV_YOMI_OVDIM.Taarich%TYPE; 
  CMispar_Ishi TB_CHISHUV_YOMI_OVDIM.Mispar_ishi%TYPE; 
  CKod_Rechiv TB_CHISHUV_YOMI_OVDIM.kod_rechiv%TYPE;
  CURSOR cursor_GroupBy  IS  
                                    SELECT  DayCalc.Mispar_ishi, DayCalc.kod_rechiv 
                                    FROM TB_CHISHUV_YOMI_OVDIM DayCalc 
                                    WHERE TAARICH BETWEEN p_FromDate and p_ToDate  
                                            AND DayCalc.BAKASHA_ID =LastBakasha
                                            AND DAYCALC.KOD_RECHIV IN (SELECT R.KOD_RECHIV
                                    FROM  CTB_RECHIVIM r
                                    WHERE R.KOD_HEADRUT_CLALI IS NOT NULL
                                    AND R.KOD_RECHIV NOT IN (248,249)) 
                                    GROUP BY DayCalc.Mispar_ishi, DayCalc.kod_rechiv;
 CURSOR cursor_date (p_MisparIshi  TB_CHISHUV_YOMI_OVDIM.Mispar_ishi%TYPE ,p_Kod_Rechiv TB_CHISHUV_YOMI_OVDIM.kod_rechiv%TYPE,p_Month VARCHAR )  IS
                                     SELECT Taarich  
                                    FROM TB_CHISHUV_YOMI_OVDIM DayCalc 
                                    WHERE DAYCALC.MISPAR_ISHI = p_MisparIshi
                                    AND kod_rechiv = p_Kod_Rechiv
                                    AND TAARICH BETWEEN p_FromDate and p_ToDate 
                                    AND DayCalc.BAKASHA_ID = LastBakasha                      
                                    ORDER BY TO_CHAR(Taarich,'DD');                                    
                                    

BEGIN


DBMS_OUTPUT.PUT_LINE('CurrentMonth:' || CurrentMonth );
    p_FromDate :=  TO_DATE('01/' || CurrentMonth,'dd/mm/yyyy'); /* period= 05/2009=>  v_MinLimitDate = 01/05/2009 */
DBMS_OUTPUT.PUT_LINE('p_FromDate:' || p_FromDate ||'p_ToDate:'  || p_ToDate);
    p_ToDate := ADD_MONTHS(p_FromDate,1) -1 ;    /* period= 05/2009=>  v_MaxLimitDate = 31/05/2009 */
DBMS_OUTPUT.PUT_LINE('p_FromDate:' || p_FromDate ||'p_ToDate:'  || p_ToDate);

  --CurrentMonth :=  '11/2009'; 
     SELECT   MAX(YOMI.BAKASHA_ID) into LastBakasha
    FROM  TB_CHISHUV_YOMI_OVDIM Yomi
    WHERE TAARICH BETWEEN p_FromDate and p_ToDate ;


OPEN cursor_GroupBy ;

LOOP 
    FETCH cursor_GroupBy INTO CMispar_ishi,CKod_Rechiv ;
        EXIT WHEN cursor_GroupBy%NOTFOUND;
    PreviousDay := '' ;
    CoutiniousList := FALSE ;     
    OPEN cursor_date (CMispar_ishi,CKod_Rechiv,CurrentMonth ) ;
    LOOP 
        FETCH cursor_date INTO CDate;
        CurrentDay := TO_CHAR(CDate,'DD');
        EXIT WHEN cursor_date%NOTFOUND;
            IF (NVL(TO_NUMBER(PreviousDay),0) <> TO_NUMBER(CurrentDay) ) THEN
                StrAllTheDateList := StrAllTheDateList || CurrentDay   || ',';
                IF (TO_NUMBER(PreviousDay) + 1 = TO_NUMBER (CurrentDay) ) THEN 
                PreviousDay := CurrentDay ;
                    IF (CoutiniousList = FALSE) THEN
                        StrList := StrList || '-' || CurrentDay ; 
                        CoutiniousList := TRUE;
                    ELSE StrList := SUBSTR(StrList ,0,LENGTH(StrList) - 2 ) || CurrentDay ;  
                    END IF ;      
                ELSE CoutiniousList := FALSE;
                END IF ;
                IF(CoutiniousList = FALSE) THEN
                    IF (LENGTH(StrList) > 0 ) THEN 
                        StrList := StrList   ||  ','  || CurrentDay ;
                    ELSE StrList :=  CurrentDay ;
                    END IF ;
                END IF ;
            END IF ;
        PreviousDay := TO_CHAR(CDate,'DD');
    END LOOP;
    CLOSE cursor_date;
    INSERT INTO TMP_DATES_PIVOT(DATES_LIST,MISPAR_ISHI,KOD_RECHIV)  
    VALUES(StrList,CMispar_Ishi,CKod_Rechiv);

--DBMS_OUTPUT.PUT_LINE('Mispar_ishi:' || CMispar_Ishi ||'Kod_Rechiv:' || CKod_Rechiv ||  ',the List is: '|| SubStr( StrAllTheDateList,0,length(StrAllTheDateList)-1));
--DBMS_OUTPUT.PUT_LINE('List is: '|| StrList);
StrAllTheDateList := '';
StrList := ''; 
END LOOP;
CLOSE cursor_Groupby;

UPDATE TMP_DATES_PIVOT Pivot SET
PIVOT.SUM_RECHIV = 
( 
                                        SELECT Sum_rechiv
                                        FROM 
                                        (
                                        SELECT ORIGIN.DATES_LIST, ORIGIN.MISPAR_ISHI, ORIGIN.KOD_RECHIV,sums.Sum_rechiv
                                        FROM TMP_DATES_PIVOT Origin , 
                                           (
                                           SELECT mispar_ishi,DAILY.KOD_RECHIV,  SUM(DAILY.ERECH_RECHIV) Sum_rechiv 
                                           FROM TB_CHISHUV_YOMI_OVDIM Daily
                                           WHERE  TO_CHAR(Taarich,'MM/YYYY') = CurrentMonth
                                                    AND Daily.BAKASHA_ID = (
                                                                                                    SELECT   MAX(YOMI.BAKASHA_ID) LastBakasha
                                                                                                    FROM  TB_CHISHUV_YOMI_OVDIM Yomi
                                                                                                    WHERE TO_CHAR(Taarich,'MM/YYYY') = CurrentMonth
                                                                                                )
                                           GROUP BY CUBE (DAILY.KOD_RECHIV, mispar_ishi )
                                           HAVING GROUPING (DAILY.KOD_RECHIV) = 0 AND GROUPING (mispar_ishi)  = 0
                                           )
                                            Sums
                                        WHERE Sums.mispar_ishi =    ORIGIN.MISPAR_ISHI
                                           AND Sums.KOD_RECHIV =    ORIGIN.KOD_RECHIV
                                           ) SubSums 
                                    WHERE PIVOT.KOD_RECHIV = SubSums.kod_rechiv
                                    AND PIVOT.MISPAR_ISHI =  SubSums.mispar_ishi                                        
                                    AND PIVOT.DATES_LIST =  SubSums.DATES_LIST                                        
) ;




END PREPARE_DAYLIST_OF_RPT ;




PROCEDURE pro_Get_AbsentWorkers(P_PERIOD IN  VARCHAR2 ,
                                                   P_EZOR IN NUMBER ,
                                                   P_ABSENCE_TYPE IN NUMBER , 
                                                   P_WORKDAYS IN NUMBER,
                                                   P_MAMAD IN NUMBER ,
                                                     p_cur OUT CurType ) IS
  p_ToDate DATE ; 
  p_FromDate DATE ; 
GeneralQry VARCHAR2(32767);
ParamQry VARCHAR2(1000);
    p_Erech TB_PARAMETRIM.erech_param%TYPE ;
  BEGIN
  DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_Get_AbsentWorkers');
  EXECUTE IMMEDIATE 'truncate table TB_TMP_DATES_PIVOT ';
      p_FromDate := TO_DATE('01/' || p_Period,'dd/mm/yyyy'); /* period= 05/2009=>  v_MinLimitDate = 01/05/2009 */
    p_ToDate := ADD_MONTHS(p_FromDate,1) -1 ;    /* period= 05/2009=>  v_MaxLimitDate = 31/05/2009 */
     PREPARE_DAYLIST_OF_RPT(P_PERIOD) ; 
     INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_PERIOD:',P_PERIOD);
    INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_EZOR:',P_EZOR);
     INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_ABSENCE_TYPE:',P_ABSENCE_TYPE);
     INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_WORKDAYS:',P_WORKDAYS);
     INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_MAMAD:',P_MAMAD);
    
  GeneralQry :=    '
     SELECT  SUBSTR(Details.EZOR,0,1) || SUBSTR(Details.MAAMAD,0,1) || ''-'' ||  Details.MISPAR_ISHI Mispar_ishi_Full,
                 DETAILS.SNIF_AV ,
                Ov.Full_name  ,
                YAMIMLIST.DATES_LIST ,YAMIMLIST.SUM_RECHIV ,YAMIMLIST.KOD_RECHIV ,ABSENCE.TEUR_CLALI ,
                teur_maamad_hr  Maamad ,  
                CASE WHEN Ov.Wording_date  IN (51,52) THEN 5 
                        WHEN Ov.Wording_date  IN (61,62) THEN 6 
                        END WorkDay,
                CASE WHEN (Ov.Matching_1 IS NOT NULL AND Ov.Matching_2 IS NOT NULL )   
                THEN ''�����'' ||
                         Ov.Matching_2 / 60 || 
                         '' ����''
                WHEN (Ov.Matching_1 IS NOT NULL AND Ov.Matching_2 IS NULL )   
                THEN ''�����''         
                ELSE '''' 
                END Matching , 
                Gil.Teur_kod_gil  
 FROM 
 ( 
            SELECT kod_hevra, mispar_ishi , shem_mish|| ''  '' ||  shem_prat full_name, 
            (Pkg_Ovdim.fun_get_meafyen_oved(mispar_ishi, 56,''' ||  P_ToDate  || ''' )) Wording_date, 
            (Pkg_Ovdim.fun_get_meafyen_oved(mispar_ishi, 8,''' || P_ToDate  || ''' )) Matching_1,
            (Pkg_Ovdim.fun_get_meafyen_oved(mispar_ishi, 20,''' ||  P_ToDate  || ''' )) Matching_2
            FROM OVDIM
            ) Ov,
PIVOT_PIRTEY_OVDIM Details  ,
CTB_RECHIVIM Component , 
TB_TMP_DATES_PIVOT YAMIMLIST , 
CTB_MAAMAD maamad , 
( SELECT DISTINCT   H.KOD_HEADRUT_CLALI, H.TEUR_CLALI FROM CTB_SUGEY_HEADRUYUT H WHERE H.KOD_HEADRUT_CLALI IS NOT NULL  ) ABSENCE ,
(SELECT po.mispar_ishi,MAX(po.ME_TARICH) me_taarich
                       FROM PIVOT_PIRTEY_OVDIM PO
                       WHERE po.isuk IS NOT NULL
                       AND 
                       (Me_Tarich <=''' ||  P_ToDate  || ''' ) AND (Ad_Tarich >=''' ||  P_FromDate  || ''' OR Ad_Tarich IS NULL)
                      GROUP BY po.mispar_ishi) RelevantDetails,
CTB_KOD_GIL Gil 
WHERE 
Details.mispar_ishi = RelevantDetails.mispar_ishi 
AND  Details.ME_TARICH = RelevantDetails.me_taarich
AND Ov.mispar_ishi = RelevantDetails.mispar_ishi
AND YAMIMLIST.MISPAR_ISHI = Details.mispar_ishi
AND YAMIMLIST.KOD_RECHIV = COMPONENT.KOD_RECHIV
AND COMPONENT.KOD_HEADRUT_CLALI = ABSENCE.KOD_HEADRUT_CLALI
AND MAAMAD.KOD_HEVRA = OV.KOD_HEVRA
AND MAAMAD.KOD_MAAMAD_HR = DETAILS.MAAMAD
AND Details.GIL         = Gil.kod_gil_hr  ' ;

IF ( P_MAMAD <> '-1' ) THEN 
    ParamQry := ParamQry || ' substr(Details.MAAMAD,0,1) like ''' || P_MAMAD || ''' AND ';
END IF ; 

IF (( P_EZOR IS NOT  NULL ) OR ( P_EZOR <> '' )) THEN 
    ParamQry := ParamQry || ' Details.EZOR in (' || P_EZOR || ') AND ';
END IF ; 


IF ( P_ABSENCE_TYPE <> '-1' ) THEN 
    ParamQry := ParamQry || ' COMPONENT.KOD_HEADRUT_CLALI in (' || P_ABSENCE_TYPE || ') AND ';
END IF ; 

IF ( P_WORKDAYS <> '-1' ) THEN 
    ParamQry := ParamQry || 'substr(Ov.Wording_date,0,1) like ''' || P_WORKDAYS || ''' AND ';
END IF ; 



IF (( ParamQry IS NOT NULL  ) OR ( ParamQry <> '')) THEN
  ParamQry := SUBSTR(ParamQry,0,LENGTH(ParamQry)-4); -- TO DELETE THE LAST 'AND '
  GeneralQry := GeneralQry || 'And ' || ParamQry;
END IF ;
DBMS_OUTPUT.PUT_LINE ( GeneralQry);

--execute immediate 'select count(*) from (' || GeneralQry || ')' into rc ;
OPEN p_cur FOR GeneralQry ;

         EXCEPTION 
                WHEN OTHERS THEN 
                       RAISE;                     
            

END pro_Get_AbsentWorkers;


PROCEDURE pro_GetMainDetailsAverage (P_STARTDATE IN DATE, 
                                            P_ENDDATE IN DATE ,
                                            P_MisparIshi  IN VARCHAR2 ,
                                            P_EZOR IN VARCHAR ,
                                            P_COMPANYID  IN VARCHAR2 ,
                                            P_MAMAD IN VARCHAR2 ,
                                            P_MAMAD_HR IN VARCHAR2 ,
                                            P_SNIF IN VARCHAR2 ,
                                            P_ISUK VARCHAR2 ,
                                            P_KOD_YECHIDA IN VARCHAR2 ,
                                            P_SECTORISUK IN VARCHAR2 ,
                                            P_rechiv IN VARCHAR2 ,
                                            P_WORKERID in number,
                                            P_WorkerViewLevel IN NUMBER, 
                                            p_cur OUT CurType ) IS
GeneralQry VARCHAR2(32767);
QryMakatDate VARCHAR2(3000);
ParamQry VARCHAR2(1000);
BEGIN 
  DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_GetMainDetailsAverage');
/*
INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_STARTDATE:',P_STARTDATE);
INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_ENDDATE:',P_ENDDATE);
INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_SNIF:',P_SNIF);
INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_MAMAD:',P_MAMAD);
INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_ISUK:',P_ISUK);
INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_EZOR:',P_EZOR);
INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_COMPANYID:',P_COMPANYID);
INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_MisparIshi:',P_MisparIshi);
INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_KOD_YECHIDA:',P_KOD_YECHIDA);
INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_SECTORISUK:',P_SECTORISUK);
*/

    
IF (P_WorkerViewLevel = 5) THEN  
        Pkg_Utils.pro_ins_Manage_Tree(TO_NUMBER(P_WORKERID,999999999));
END IF ;
    
GeneralQry  := 'Select
Ov.shem_mish|| '' '' ||  Ov.shem_prat full_name ,Details.Mispar_ishi ,Gil.Teur_kod_gil , e.teur_ezor, Snif.TEUR_SNIF_AV  , Maamad.teur_maamad_hr  teur_maamad , Isuk.teur_isuk ,h.TEUR_HEVRA   ,Yechida.TEUR_YECHIDA  
FROM OVDIM_HISTORY Ov , 
PIVOT_PIRTEY_OVDIM_HIST Details  ,
(SELECT po.mispar_ishi,MAX(po.ME_TARICH) me_taarich
                       FROM PIVOT_PIRTEY_OVDIM_HIST PO
                       WHERE po.isuk IS NOT NULL
                             AND (''' ||  P_STARTDATE  || '''  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE(''01/01/9999'' ,''dd/mm/yyyy''))
                               OR   ''' ||  P_ENDDATE  || '''  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE(''01/01/9999'' ,''dd/mm/yyyy''))
                                OR   (po.ME_TARICH>= ''' ||  P_STARTDATE  || '''  AND   NVL(po.ad_TARICH,TO_DATE(''01/01/9999'' ,''dd/mm/yyyy''))<=  ''' ||  P_ENDDATE  || ''' ) )
                      GROUP BY po.mispar_ishi) RelevantDetails,

CTB_KOD_GIL Gil ,
ctb_ezor e,
 CTB_MAAMAD Maamad,
 CTB_ISUK Isuk ,
 CTB_SNIF_AV Snif ,
 ctb_hevra h,
 CTB_YECHIDA Yechida';
                                       
        IF (P_WorkerViewLevel = 5) THEN
          GeneralQry := GeneralQry || ',TMP_MANAGE_TREE Tree ';
        END IF ;    
 
 GeneralQry := GeneralQry || ' WHERE 
Ov.mispar_ishi = Details.mispar_ishi
AND Details.mispar_ishi = RelevantDetails.mispar_ishi 
AND Details.ME_TARICH = RelevantDetails.me_taarich (+)
AND Snif.KOD_HEVRA = OV.KOD_HEVRA 
AND Snif.kod_snif_av(+) = details.snif_av
AND Isuk.KOD_HEVRA = OV.KOD_HEVRA
AND MAAMAD.KOD_HEVRA = OV.KOD_HEVRA
AND maamad.kod_maamad_hr(+) =  details.maamad
AND e.KOD_HEVRA = OV.KOD_HEVRA
AND e.kod_ezor(+) =  details.ezor
AND Details.GIL         = Gil.kod_gil_hr
AND Maamad.kod_hevra =Isuk.kod_hevra 
AND YECHIDA.KOD_YECHIDA = DETAILS.YECHIDA_IRGUNIT
AND YECHIDA.KOD_HEVRA = OV.KOD_HEVRA
AND h.KOD_HEVRA = OV.KOD_HEVRA
AND Isuk.kod_isuk(+) = details.isuk 
and exists(
 SELECT  co.mispar_ishi 
                 FROM TB_CHISHUV_CHODESH_OVDIM co,TB_BAKASHOT b
                 WHERE   B.BAKASHA_ID = CO.BAKASHA_ID
                        AND b.Huavra_Lesachar=1
                      and co.mispar_ishi =Ov.mispar_ishi ';
 
IF ( P_Rechiv IS NOT  NULL ) THEN 
    GeneralQry := GeneralQry || ' and  co.kod_rechiv in (' || P_rechiv || ')  ';
END IF ;      
                      
  GeneralQry := GeneralQry || 'and CO.TAARICH BETWEEN  ''' ||  P_STARTDATE  || '''  AND ''' ||  P_ENDDATE  || ''' 
                 group by  co.mispar_ishi      
                 ) ';

IF (P_WorkerViewLevel = 5) THEN
  GeneralQry := GeneralQry || ' And   Ov.mispar_ishi in Tree.mispar_ishi  ';
END IF ;   

 
IF ( P_MisparIshi IS NOT NULL  ) THEN 
    ParamQry := ParamQry || ' and  Details.mispar_ishi in (' || P_MisparIshi || ')  ';
END IF ; 


IF (  p_ezor IS NOT  NULL  ) THEN 
    ParamQry := ParamQry || ' and  Details.ezor in (' || p_ezor || ')  ';
END IF ; 

IF (P_COMPANYID IS NOT  NULL  ) THEN 
    ParamQry := ParamQry || ' and  OV.KOD_HEVRA in (' || P_COMPANYID || ')  ';
END IF ;

IF (  P_MAMAD IS NOT  NULL    ) THEN 
    ParamQry := ParamQry || ' and  substr(Details.MAAMAD,0,1) = ' || P_MAMAD ;
END IF ;  

IF ( P_MAMAD_HR IS NOT  NULL  ) THEN 
    ParamQry := ParamQry || ' and  Details.MAAMAD in (' || P_MAMAD_HR || ')  ';
END IF ; 

IF (P_ISUK IS NOT  NULL  ) THEN 
    ParamQry := ParamQry || ' and Isuk.KOD_isuk in (' || P_ISUK || ')  ';
END IF ; 
 
IF ( P_SNIF IS NOT  NULL ) THEN 
    ParamQry := ParamQry || ' and details.snif_av in (' || P_SNIF || ')  ';
END IF ; 
 
IF ( P_KOD_YECHIDA IS NOT  NULL  ) THEN 
    ParamQry := ParamQry || ' and YECHIDA.KOD_YECHIDA in (' || P_KOD_YECHIDA || ')  ';
END IF ; 

IF ( P_SECTORISUK IS NOT  NULL ) THEN 
    ParamQry := ParamQry || ' and Isuk.KOD_SECTOR_ISUK in (' || P_SECTORISUK || ')  ';
END IF ; 
 
GeneralQry := GeneralQry || ParamQry;
/*IF (( ParamQry IS NOT NULL  ) OR ( ParamQry <> '')) THEN
  ParamQry := SUBSTR(ParamQry,0,LENGTH(ParamQry)-4); -- TO DELETE THE LAST 'AND '
  GeneralQry := GeneralQry || 'And ' || ParamQry;
END IF ;*/

DBMS_OUTPUT.PUT_LINE ( GeneralQry);
--execute immediate 'select count(*) from (' || GeneralQry || ')' into rc ;
OPEN p_cur FOR GeneralQry ;
 
    EXCEPTION 
    WHEN OTHERS THEN 
    RAISE;                     
            

END pro_GetMainDetailsAverage;


PROCEDURE pro_GetAverageExl (P_STARTDATE IN DATE, 
                                            P_ENDDATE IN DATE ,
                                            P_MisparIshi  IN VARCHAR2 ,
                                            P_EZOR IN VARCHAR ,
                                            P_COMPANYID  IN VARCHAR2 ,
                                            P_MAMAD IN VARCHAR2 ,
                                            P_MAMAD_HR IN VARCHAR2 ,
                                            P_SNIF IN VARCHAR2 ,
                                            P_ISUK VARCHAR2 ,
                                            P_KOD_YECHIDA IN VARCHAR2 ,
                                            P_SECTORISUK IN VARCHAR2 ,
                                            P_RECHIV IN VARCHAR2 ,
                                            P_WORKERID in number,
                                            P_WorkerViewLevel IN NUMBER, 
                                            p_cur OUT CurType ) IS
begin

IF (P_WorkerViewLevel = 5) THEN  
        Pkg_Utils.pro_ins_Manage_Tree(TO_NUMBER(P_WORKERID,999999999));
END IF ;

        open p_cur for
           with ovd as(
                    Select Ov.shem_mish|| ' ' ||  Ov.shem_prat full_name ,Details.Mispar_ishi ,Gil.Teur_kod_gil , e.teur_ezor, Snif.TEUR_SNIF_AV    , Maamad.teur_maamad_hr  teur_maamad , Isuk.teur_isuk ,h.TEUR_HEVRA   ,Yechida.TEUR_YECHIDA  ,
                              b.Taarich, b.bakasha_id, b.cnt_month num
                              
                    FROM OVDIM_HISTORY Ov , 
                    PIVOT_PIRTEY_OVDIM_HIST Details  ,
                    (SELECT po.mispar_ishi,MAX(po.ME_TARICH) me_taarich
                                           FROM PIVOT_PIRTEY_OVDIM_HIST PO
                                           WHERE po.isuk IS NOT NULL
                                                 AND (P_STARTDATE  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                                                   OR P_ENDDATE   BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                                                    OR   (po.ME_TARICH>= P_STARTDATE   AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<= P_ENDDATE  ) )
                                          GROUP BY po.mispar_ishi) RelevantDetails,

                    CTB_KOD_GIL Gil ,
                    ctb_ezor e,
                     CTB_MAAMAD Maamad,
                     CTB_ISUK Isuk ,
                     CTB_SNIF_AV Snif ,
                     ctb_hevra h,
                     CTB_YECHIDA Yechida, 
                   --  TMP_MANAGE_TREE Tree,
                     
                        (SELECT   a.mispar_ishi, a.Taarich, a.bakasha_id,
                                        count(a.Taarich) over (PARTITION BY a.mispar_ishi) cnt_month
                              FROM( SELECT  co.mispar_ishi, co.Taarich, co.bakasha_id,b.TAARICH_HAAVARA_LESACHAR th1,
                                                  MAX(B.TAARICH_HAAVARA_LESACHAR) OVER (PARTITION BY co.mispar_ishi,co.Taarich )  th2
                                     FROM TB_CHISHUV_CHODESH_OVDIM co,TB_BAKASHOT b
                                     WHERE   B.BAKASHA_ID = CO.BAKASHA_ID
                                            AND B.HUAVRA_LESACHAR=1
                                            and (P_RECHIV is null or co.kod_rechiv in ( (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_RECHIV,  ',') AS MYTABTYPE))) ))     
                                             and (P_MisparIshi is null or co.mispar_ishi in ( (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MisparIshi,  ',') AS MYTABTYPE))) ))
                                            and CO.TAARICH BETWEEN P_STARTDATE AND P_ENDDATE --in(to_date('01/09/2012','dd/mm/yyyy') ,  to_date('01/10/2012','dd/mm/yyyy') , to_date('01/02/2013','dd/mm/yyyy'),  to_date('01/03/2013','dd/mm/yyyy'),  to_date('01/04/2013','dd/mm/yyyy'))
                                     ) a
                                WHERE a.th1=a.th2
                                 group by   a.mispar_ishi, a.Taarich, a.bakasha_id )  b
                                                           

                     WHERE 
                    Ov.mispar_ishi = Details.mispar_ishi
                    AND Details.mispar_ishi = RelevantDetails.mispar_ishi 
                    AND Details.ME_TARICH = RelevantDetails.me_taarich (+)
                    AND Snif.KOD_HEVRA = OV.KOD_HEVRA 
                    AND Snif.kod_snif_av(+) = details.snif_av
                    AND Isuk.KOD_HEVRA = OV.KOD_HEVRA
                    AND MAAMAD.KOD_HEVRA = OV.KOD_HEVRA
                    AND maamad.kod_maamad_hr(+) =  details.maamad
                     AND e.KOD_HEVRA = OV.KOD_HEVRA
                    AND e.kod_ezor(+) =  details.ezor
                    AND Details.GIL         = Gil.kod_gil_hr
                    AND Maamad.kod_hevra =Isuk.kod_hevra 
                    AND YECHIDA.KOD_YECHIDA = DETAILS.YECHIDA_IRGUNIT
                    AND YECHIDA.KOD_HEVRA = OV.KOD_HEVRA
                    AND h.KOD_HEVRA = OV.KOD_HEVRA
                    AND Isuk.kod_isuk(+) = details.isuk 
                    and Ov.mispar_ishi =b.mispar_ishi 
                   
                   and ((P_Ezor is null)   or Details.Ezor in ( (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_Ezor,  ',') AS MYTABTYPE))) ))
                    and ((P_COMPANYID is null) or  OV.KOD_HEVRA in ( (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_COMPANYID,  ',') AS MYTABTYPE))) ))
                    and ((P_MAMAD is null) or substr(Details.MAAMAD,0,1)  in ( (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MAMAD,  ',') AS MYTABTYPE))) ))
                    and (P_MAMAD_HR is null or Details.MAAMAD  in ( (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MAMAD_HR,  ',') AS MYTABTYPE))) ))
                    and (P_SNIF is null or details.snif_av  in ( (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_SNIF,  ',') AS MYTABTYPE))) ))
                    and (P_ISUK is null or  Isuk.KOD_isuk  in ( (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_ISUK,  ',') AS MYTABTYPE))) ))
                    and (P_KOD_YECHIDA is null or  YECHIDA.KOD_YECHIDA  in ( (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_KOD_YECHIDA,  ',') AS MYTABTYPE))) ))
                    and (P_SECTORISUK is null or  Isuk.KOD_SECTOR_ISUK  in ( (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_SECTORISUK,  ',') AS MYTABTYPE))) ))
               and (P_WorkerViewLevel <> 5 or (P_WorkerViewLevel = 5 and Ov.mispar_ishi in ( select * from TMP_MANAGE_TREE)  )) 
                  ) 
            
               SELECT d.*, Ch.Kod_Rechiv ,E.TEUR_SUG_ERECH,
                        R.TEUR_RECHIV,
                  case when nvl(r.SUG_ERECH_LETZUGA_CHODSHI,0)=1 and nvl(R.SUG_ERECH,0)=2  then  
                      round( (CH.ERECH_RECHIV/60),2) else  
                      case when  (nvl(r.SUG_ERECH_LETZUGA_CHODSHI,0)=2 or nvl(R.SUG_ERECH,0)=2) then   round(CH.ERECH_RECHIV,1) else round(CH.ERECH_RECHIV,2) end   
                     end    ERECH_RECHIV,   
                           nvl(  R.MIYUN_MEMUZAIM,999) MIYUN_MEMUZAIM
                        FROM TB_CHISHUV_CHODESH_OVDIM Ch,ctb_rechivim r, ovd d, ctb_sugey_erech_chishuv e
                        WHERE 
                             ch.mispar_ishi= d.mispar_ishi
                            and (P_RECHIV is null or ch.kod_rechiv in ( (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_RECHIV,  ',') AS MYTABTYPE))) )) 
                            and ch.bakasha_id= d.bakasha_id
                            and ch.taarich=d.taarich
                            and CH.KOD_RECHIV=R.KOD_RECHIV
                            and Ch.Taarich  BETWEEN   P_STARTDATE  AND P_ENDDATE 
                            and R.SUG_ERECH_LETZUGA_CHODSHI  =E.KOD_SUG_ERECH(+);
             
      
   
end pro_GetAverageExl;                                        
                                            
PROCEDURE pro_GetRechivValueAverage (P_STARTDATE IN DATE, 
                                         P_ENDDATE IN DATE ,
                                            P_MisparIshi  IN NUMBER,
                                            p_cur OUT CurType ) IS
BEGIN 
OPEN p_Cur FOR 
SELECT 
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 1 THEN Erech_Rechiv/60 ELSE NULL END),'999999.99')) Nohehout,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 105 THEN Erech_Rechiv ELSE NULL END),'999999.99')) Nossafot,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 100 THEN Erech_Rechiv ELSE NULL END),'999999.99')) Shaot100  ,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 101 THEN Erech_Rechiv ELSE NULL END),'999999.99')) Shaot125,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 102 THEN Erech_Rechiv ELSE NULL END),'999999.99')) Shaot150,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 192 THEN Erech_Rechiv ELSE NULL END),'999999.99')) ShaotTafkid,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 37 THEN Erech_Rechiv/60 ELSE NULL END),'999999.99')) TafkidShabess,
trim(TO_CHAR(SUM(CASE WHEN kod_rechiv IN(21,37,193)   THEN Erech_Rechiv/60 ELSE NULL END),'999999.99')) NosafotTafkid,
trim(TO_CHAR(SUM(CASE WHEN kod_rechiv IN(161,147)   THEN Erech_Rechiv/60 ELSE NULL END),'999999.99')) KizuzTafkid,
trim(TO_CHAR(SUM(CASE WHEN kod_rechiv IN(133,134)   THEN Erech_Rechiv ELSE NULL END),'999999.99')) SumPremia,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 2 THEN Erech_Rechiv/60 ELSE NULL END),'999999.99')) DriveWeek,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 189 THEN Erech_Rechiv/60 ELSE NULL END),'999999.99')) Drive6,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 35 THEN Erech_Rechiv/60 ELSE NULL END),'999999.99')) DriveShabess,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 60 THEN Erech_Rechiv ELSE NULL END),'999999.99')) HillDays,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 62 THEN Erech_Rechiv ELSE NULL END),'999999.99')) MiluimDays,
trim(TO_CHAR(SUM(CASE WHEN kod_rechiv IN(66,67)   THEN Erech_Rechiv ELSE NULL END),'999999.99')) HofeshEadrut,
trim(TO_CHAR(SUM(CASE WHEN kod_rechiv IN(56,60,61,62,64,65,66,67,68,69,70,71)   THEN Erech_Rechiv ELSE NULL END),'999999.99')) SumEadrut,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 57 THEN Erech_Rechiv ELSE NULL END),'999999.99')) Adraha,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 75 THEN Erech_Rechiv ELSE NULL END),'999999.99')) NohehoutDay,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 109 THEN Erech_Rechiv ELSE NULL END),'999999.99')) Workday,
fct_GetSumMonthWithRechiv( P_STARTDATE ,P_ENDDATE  ,P_MisparIshi  ) Months,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 194 THEN Erech_Rechiv ELSE NULL END),'999999.99')) Nohah1_5,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 126 THEN Erech_Rechiv ELSE NULL END),'999999.99')) MonthlyQuota,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 82 THEN Erech_Rechiv/60 ELSE NULL END),'999999.99')) Nosafot1_5,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 200 THEN Erech_Rechiv ELSE NULL END),'999999.99'))AlM1_5,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 195 THEN Erech_Rechiv ELSE NULL END),'999999.99')) HoursDay_6,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 196 THEN Erech_Rechiv ELSE NULL END),'999999.99')) HoursShabess,
trim(TO_CHAR(SUM(CASE WHEN kod_rechiv IN(54,55)   THEN Erech_Rechiv ELSE NULL END),'999999.99')) HoursNight,
trim(TO_CHAR(SUM(CASE WHEN kod_rechiv IN(133,134)   THEN Erech_Rechiv ELSE NULL END),'999999.99')) PremiaNaagut,
trim(TO_CHAR(SUM(CASE WHEN kod_rechiv IN(49,50)   THEN Erech_Rechiv ELSE NULL END),'999999.99')) Pitsul,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 57 THEN Erech_Rechiv ELSE NULL END),'999999.99')) HadrahaDays,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 67 THEN Erech_Rechiv ELSE NULL END),'999999.99')) HofeshDays,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 64 THEN Erech_Rechiv ELSE NULL END),'999999.99')) AccidentDays,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 66 THEN Erech_Rechiv ELSE NULL END),'999999.99')) HeadrutDays,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 190 THEN Erech_Rechiv/60 ELSE NULL END),'999999.99')) HoursTnua,
trim(TO_CHAR(SUM(CASE WHEN kod_rechiv IN(20,36,191)   THEN Erech_Rechiv ELSE NULL END),'999999.99')) NosafotTnua,
trim(TO_CHAR(SUM(CASE WHEN kod_rechiv IN(160,163)   THEN Erech_Rechiv ELSE NULL END),'999999.99')) KizuzTnua,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 188 THEN Erech_Rechiv ELSE NULL END),'999999.99')) HourNaagut,
trim(TO_CHAR(SUM(CASE WHEN kod_rechiv IN(19,35,189)   THEN Erech_Rechiv ELSE NULL END),'999999.99')) NosafotNahagut,
0 KizuzNaagut,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 85 THEN Erech_Rechiv ELSE NULL END),'999999.99')) ZmanRetsef,
trim(TO_CHAR(SUM(CASE kod_rechiv WHEN 22 THEN Erech_Rechiv ELSE NULL END),'999999.99')) GmulSum
FROM 
(         
SELECT Kod_Rechiv ,Erech_Rechiv 
FROM (
        SELECT  Ch.Kod_Rechiv ,   AVG(Ch.Erech_Rechiv) Erech_Rechiv     
        FROM TB_CHISHUV_CHODESH_OVDIM Ch
        WHERE Ch.Mispar_Ishi = P_MisparIshi
       -- ((P_MisparIshi IS NOT NULL ) AND (  Ch.Mispar_Ishi  IN (SELECT x FROM TABLE(CAST(Convert_String_To_Table(P_MisparIshi ,  ',') AS mytabtype)))))         
        AND (Ch.Taarich BETWEEN P_STARTDATE  AND P_ENDDATE)
        AND Ch.Bakasha_ID IN(
                                        SELECT B.BAKASHA_ID
                                        FROM  TB_BAKASHOT B
                                        WHERE B.TAARICH_HAAVARA_LESACHAR IN 
                                                                                                    (
                                                                                                        SELECT   MAX(Taarich_Haavara_Lesachar) MaxDate 
                                                                                                        FROM TB_BAKASHOT B
                                                                                                        WHERE Bakasha_ID IN
                                                                                                                                      (
                                                                                                                                       SELECT  /*+  full(_CHISHUV_CHODESH_OVDIM  ) */ 
                                                                                                                                        DISTINCT Bakasha_ID  
                                                                                                                                      FROM TB_CHISHUV_CHODESH_OVDIM  
                                                                                                                                      WHERE
                                                                                                                                      Taarich  BETWEEN P_STARTDATE  AND P_ENDDATE
                                                                                                                                      )
                                                                                                         AND Taarich_Haavara_Lesachar IS NOT NULL
                                                                                                         AND B.HUAVRA_LESACHAR =    1
                                                                                                         AND Bakasha_ID IS NOT NULL 
                                                                                                         GROUP BY TO_CHAR(Taarich_Haavara_Lesachar,'MM/YYYY')
                                                                                                    ) 
                                        )
        GROUP BY Ch.Kod_Rechiv) 
);
 
     EXCEPTION 
    WHEN OTHERS THEN 
    RAISE;                     
END        pro_GetRechivValueAverage ; 



FUNCTION fct_GetSumMonthWithRechiv (P_STARTDATE IN DATE, 
                                            P_ENDDATE IN DATE ,
                                            P_MisparIshi  IN VARCHAR2 ) RETURN  NUMBER  IS 
CountMonth NUMBER  ;                                            
BEGIN 
SELECT COUNT(Cnt) INTO CountMonth
FROM (
        SELECT  COUNT(Ch.Kod_Rechiv) CNT 
        FROM TB_CHISHUV_CHODESH_OVDIM Ch
        WHERE 
        ((P_MisparIshi IS NOT NULL ) AND (  Ch.Mispar_Ishi  IN (SELECT x FROM TABLE(CAST(Convert_String_To_Table(P_MisparIshi ,  ',') AS mytabtype)))))         
        AND (Ch.Taarich BETWEEN P_STARTDATE  AND P_ENDDATE)
        AND Ch.Bakasha_ID IN(
                                        SELECT B.BAKASHA_ID
                                        FROM  TB_BAKASHOT B
                                        WHERE B.TAARICH_HAAVARA_LESACHAR IN 
                                                                                                    (
                                                                                                        SELECT   MAX(Taarich_Haavara_Lesachar) MaxDate 
                                                                                                        FROM TB_BAKASHOT B
                                                                                                        WHERE Bakasha_ID IN
                                                                                                                                      (
                                                                                                                                      SELECT  DISTINCT Bakasha_ID  
                                                                                                                                      FROM TB_CHISHUV_CHODESH_OVDIM  
                                                                                                                                      WHERE
                                                                                                                                      Taarich  BETWEEN P_STARTDATE  AND P_ENDDATE
                                                                                                                                      )
                                                                                                         AND Taarich_Haavara_Lesachar IS NOT NULL
                                                                                                         AND B.HUAVRA_LESACHAR =    1
                                                                                                         AND Bakasha_ID IS NOT NULL 
                                                                                                         GROUP BY TO_CHAR(Taarich_Haavara_Lesachar,'MM/YYYY')
                                                                                                    ) 
                                        )
        GROUP BY TO_CHAR(Ch.Taarich,'MM/YYYY')       
 
);

RETURN CountMonth;
 
     EXCEPTION 
    WHEN OTHERS THEN
    RETURN 0 ; 
END        fct_GetSumMonthWithRechiv ; 


 
PROCEDURE pro_GetDescriptionComponents (P_STARTDATE IN VARCHAR2,
                                           P_ENDDATE IN VARCHAR2,
                                            P_COMPANYID  IN VARCHAR2,
                                            p_cur OUT CurType ) IS
     FromDate VARCHAR2(10);             
     ToDate VARCHAR2(10);   
     dateTo DATE;                                                                                       
BEGIN
        IF ( LENGTH( P_STARTDATE) = 7) THEN
               FromDate := '01/'  || P_STARTDATE;
             dateTo := LAST_DAY(TO_DATE( '01/'  || P_ENDDATE,'dd/mm/yyyy'));
             ToDate  := TO_CHAR(dateTo,'dd') ||  '/' || P_ENDDATE;
         ELSE
             FromDate := P_STARTDATE;
             ToDate  :=  P_ENDDATE;     
          END IF;

            OPEN p_Cur FOR 
                SELECT  Ch.MISPAR_ISHI ,Ch.taarich, Ch.Kod_Rechiv , Ch.Erech_Rechiv,
                  OV.SHEM_MISH || ' ' ||  OV.SHEM_PRAT  FULL_NAME  ,
                I.TEUR_ISUK,E.TEUR_EZOR,M.TEUR_MAAMAD_HR,S.TEUR_SNIF_AV,
                R.TEUR_RECHIV
                
                FROM TB_CHISHUV_CHODESH_OVDIM Ch,
                           PIVOT_PIRTEY_OVDIM T,
                           CTB_ISUK I,
                           CTB_EZOR E,
                           CTB_MAAMAD M,
                           CTB_SNIF_AV S,
                           CTB_RECHIVIM R, 
                            OVDIM OV,    
                            
                           (SELECT  o.mispar_ishi,o.taarich,t.Bakasha_ID,MAX( t.Taarich_Haavara_Lesachar) Taarich_Haavara_Lesachar
                                    from TB_CHISHUV_CHODESH_OVDIM  o,TB_BAKASHOT t
                                    where O.BAKASHA_ID = t.BAKASHA_ID
                                     and  t.HUAVRA_LESACHAR=1
                                     and  o.Taarich between  TO_DATE(FromDate,'dd/mm/yyyy')  AND TO_DATE(ToDate ,'dd/mm/yyyy')
                                    group by o.mispar_ishi,o.taarich,t.Bakasha_ID    ) h
                        
    WHERE Ch.Bakasha_ID=H.Bakasha_ID    AND
                       Ch.mispar_ishi = H.mispar_ishi    AND                                                                        
                     (Ch.Taarich BETWEEN    TO_DATE(FromDate,'dd/mm/yyyy')  AND TO_DATE(ToDate ,'dd/mm/yyyy')  )   AND                 
                      OV.MISPAR_ISHI = Ch.MISPAR_ISHI AND
                      Ch.MISPAR_ISHI =T.MISPAR_ISHI(+) AND
                      (ADD_MONTHS(Ch.TAARICH,1) -1) BETWEEN T.ME_TARICH(+) AND T.AD_TARICH(+)    AND
                       T .ISUK =I.KOD_ISUK AND
                       OV.KOD_HEVRA=I.KOD_HEVRA AND
                       T.EZOR =E.KOD_EZOR AND
                       OV.KOD_HEVRA=E.KOD_HEVRA AND
                       T.MAAMAD = M.KOD_MAAMAD_HR AND
                       OV.KOD_HEVRA=M.KOD_HEVRA   AND
                         T.SNIF_AV = S. KOD_SNIF_AV AND
                        OV.KOD_HEVRA=S.KOD_HEVRA   AND    
                        Ch.KOD_RECHIV = R.KOD_RECHIV  AND     
                        (P_COMPANYID='-1' OR OV.KOD_HEVRA IN (SELECT x FROM TABLE(CAST(Convert_String_To_Table(P_COMPANYID ,  ',') AS mytabtype))));
                                                                            
 EXCEPTION 
    WHEN OTHERS THEN 
    RAISE;                     
END   pro_GetDescriptionComponents;

PROCEDURE  pro_GetPundakimLeHitchashbenut( P_STARTDATE IN TB_HITYAZVUT_PUNDAKIM.TAARICH%TYPE,
                                                                                                P_ENDDATE IN TB_HITYAZVUT_PUNDAKIM.TAARICH%TYPE,
                                                                                     p_Cur OUT CurType ) IS
BEGIN
   DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_GetPundakimLeHitchashbenut');
 OPEN p_Cur FOR         
 select  z.taarich,z.teur_yom,SUBSTR(LPAD(z.MIKUM_SHAON,5, '0'),0,3) KOD,m.TEUR_MIKUM_YECHIDA,COUNT(z.MISPAR_ISHI)num
 from(
 select  x.mispar_ishi,x.taarich ,x.SHAT_HITYAZVUT,x.MIKUM_SHAON,Dayofweek(x.TAARICH)   || '  ' || c.TEUR_YOM teur_yom,
      LAG (x.SHAT_HITYAZVUT,1)
      OVER(partition by x.mispar_ishi,x.taarich order by x.SHAT_HITYAZVUT asc) next_hour
from
  (
   /*SELECT T.MISPAR_ISHI  , t.taarich,t.SHAT_HITYAZVUT,t.MIKUM_SHAON
            FROM  TB_HITYAZVUT_PUNDAKIM t,ovdim v
            WHERE   T.MISPAR_ISHI = V.MISPAR_ISHI and
                          t.TAARICH BETWEEN P_STARTDATE AND P_ENDDATE  
                     -- T.MIKUM_SHAON='9601'
                       
         order by t.SHAT_HITYAZVUT */
  select h.MISPAR_ISHI  ,h.taarich,h.SHAT_HITYAZVUT ,h.MIKUM_SHAON
        from
            (SELECT T.MISPAR_ISHI  , trunc(t.SHAT_HITYAZVUT)  taarich ,
            --case when t.taarich+1 = trunc(t.SHAT_HITYAZVUT) then trunc(t.SHAT_HITYAZVUT) else   t.taarich end  taarich,
            t.SHAT_HITYAZVUT,t.MIKUM_SHAON
            FROM  TB_HITYAZVUT_PUNDAKIM t,ovdim v
            WHERE   T.MISPAR_ISHI = V.MISPAR_ISHI and
                          t.TAARICH BETWEEN P_STARTDATE AND P_ENDDATE  AND
                    --      to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))>5 
                          (to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))>=5 and to_char(t.SHAT_HITYAZVUT,'HH24:mi:ss')<>'05:00:00') 
                         -- T.MIKUM_SHAON='9601'
            union all
            SELECT T.MISPAR_ISHI  , t.taarich,t.SHAT_HITYAZVUT,t.MIKUM_SHAON
            FROM  TB_HITYAZVUT_PUNDAKIM t,ovdim v
            WHERE   T.MISPAR_ISHI = V.MISPAR_ISHI and
                          t.TAARICH BETWEEN P_STARTDATE AND P_ENDDATE  AND
                          ((to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))>=0 and to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))<5)  or to_char(t.SHAT_HITYAZVUT,'HH24:mi:ss')='05:00:00') 
                        --   to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))>=0 and to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))<=5 
                        --  T.MIKUM_SHAON='9601'   
                          ) h
--where  h.TAARICH BETWEEN (P_STARTDATE-1) AND P_ENDDATE
 
order by h.SHAT_HITYAZVUT
  /* select h.MISPAR_ISHI  ,h.taarich,h.SHAT_HITYAZVUT ,h.MIKUM_SHAON
        from
            (SELECT T.MISPAR_ISHI  , t.taarich,t.SHAT_HITYAZVUT,t.MIKUM_SHAON
            FROM  TB_HITYAZVUT_PUNDAKIM t,ovdim v
            WHERE   T.MISPAR_ISHI = V.MISPAR_ISHI and
                          t.TAARICH BETWEEN P_STARTDATE AND P_ENDDATE  AND
                    --      to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))>5 
                          (to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))>=5 and to_char(t.SHAT_HITYAZVUT,'HH24:mi:ss')<>'05:00:00') 
                         -- T.MIKUM_SHAON='9601'
            union all
            SELECT T.MISPAR_ISHI  , t.taarich,t.SHAT_HITYAZVUT,t.MIKUM_SHAON
            FROM  TB_HITYAZVUT_PUNDAKIM t,ovdim v
            WHERE   T.MISPAR_ISHI = V.MISPAR_ISHI and
                          t.TAARICH BETWEEN (P_STARTDATE-1) AND P_ENDDATE  AND
                          ((to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))>=0 and to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))<5)  or to_char(t.SHAT_HITYAZVUT,'HH24:mi:ss')='05:00:00') 
                        --   to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))>=0 and to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))<=5 
                        --  T.MIKUM_SHAON='9601'   
                          ) h
where  h.TAARICH BETWEEN (P_STARTDATE-1) AND P_ENDDATE
 
order by h.SHAT_HITYAZVUT */) x ,
 TB_YAMIM_MEYUCHADIM b, 
 CTB_SUGEY_YAMIM_MEYUCHADIM c
 where     x.taarich = b.taarich(+)
       AND  b.sug_yom=c.sug_yom(+)   )z,
CTB_MIKUM_YECHIDA m

where  ((z.SHAT_HITYAZVUT- nvl(z.next_hour, TO_DATE('01/01/0001' ,'dd/mm/yyyy')))*24*60)>60 
       AND   SUBSTR(LPAD(z.MIKUM_SHAON,5, '0'),0,3)=LPAD(m.KOD_MIKUM_YECHIDA,3, '0')   
      
GROUP BY m.TEUR_MIKUM_YECHIDA,z.taarich,z. teur_yom,z.MIKUM_SHAON
ORDER BY m.TEUR_MIKUM_YECHIDA,z.taarich,z. teur_yom,z.MIKUM_SHAON;                

 EXCEPTION 
    WHEN OTHERS THEN 
    RAISE;                     
END     pro_GetPundakimLeHitchashbenut;        

PROCEDURE pro_get_Id_of_Ovdim( p_FromDate IN DATE , p_ToDate IN DATE ,
                                                                        p_preFix IN VARCHAR2,  p_cur OUT CurType) AS 
BEGIN 

OPEN p_cur FOR 
             SELECT DISTINCT Ov.MISPAR_ISHI,  
                CASE WHEN (p_prefix IS NULL) THEN  
                               Ov.shem_mish || ' ' ||  Ov.shem_prat || '(' || Ov.MISPAR_ISHI || ')'   
                      ELSE '' 
                      END full_name 
          FROM  OVDIM Ov --,
                            -- TB_HITYAZVUT_PUNDAKIM h
          WHERE 
          --    Ov.MISPAR_ISHI = h.MISPAR_ISHI and
           --   h.TAARICH between p_FromDate and p_ToDate and
             ( p_preFix IS NULL OR Ov.MISPAR_ISHI LIKE p_preFix ||  '%'  )
           ORDER BY Ov.MISPAR_ISHI;            
  
        EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END  pro_get_Id_of_Ovdim;   
  
PROCEDURE pro_ins_Heavy_Reports (p_BakashaId IN TB_BAKASHOT.bakasha_id%TYPE, 
                                                   p_ReportName IN CTB_SUGEY_DOCHOT.SHEM_DOCH_BAKOD%TYPE,
                                                   p_ReportParams IN    COLL_Report_Param,
                                                   p_MisparIshi IN NUMBER ,
                                                   p_Extension IN TB_DOCHOT_MISPAR_ISHI.EXTENSION_TYPE%TYPE ,
                                                   p_DestinationFolder IN TB_DOCHOT_MISPAR_ISHI.SHEM_TIKIYA%TYPE,
                                                   p_SendToMail IN NUMBER) AS 
sEmail VARCHAR2(20); 
iKodReport INTEGER ;
BEGIN 

IF (p_SendToMail = 1) THEN
    SELECT   O.EMAIL INTO   sEmail FROM OVDIM O 
    WHERE O.MISPAR_ISHI = p_MisparIshi ;
END IF ;     

SELECT  S.KOD_SUG_DOCH  INTO iKodReport 
FROM CTB_SUGEY_DOCHOT s 
WHERE S.SHEM_DOCH_BAKOD  =  p_ReportName  AND ROWNUM  =1 
ORDER BY S.KOD_SUG_DOCH ASC ;

   IF (p_ReportParams IS NOT NULL) THEN
                 FOR i IN 1..p_ReportParams.COUNT LOOP
                           --check if exist in table
                BEGIN
                    INSERT INTO TB_PARAMETRIM_LEDOCHOT 
                    (BAKASHA_ID,KOD_SUG_DOCH,PAIL,SHEM_PARAM_BADOCH,ERECH)
                    VALUES (p_BakashaId ,iKodReport ,1,p_ReportParams(i).NAME  ,p_ReportParams(i).VALUE);                          
                END;
             END LOOP;

             INSERT INTO TB_DOCHOT_MISPAR_ISHI p
             (BAKASHA_ID,EXTENSION_TYPE,KOD_SUG_DOCH,MISPAR_ISHI,PAIL,SHEM_TIKIYA,Destination_Type )
             VALUES (p_BakashaId,p_Extension,iKodReport,p_MisparIshi,1,p_DestinationFolder, 
             (CASE WHEN (p_SendToMail = 1) THEN 1 ELSE 0 END )
             );
     END IF;
 
--select '53435' into sEmail from dual ;


                         
                                                  

 EXCEPTION
         WHEN OTHERS THEN
          RAISE;
             
  END         pro_ins_Heavy_Reports;

PROCEDURE pro_get_ReportDetails ( p_kodReport IN CTB_SUGEY_DOCHOT.KOD_SUG_DOCH%TYPE, p_cur OUT CurType) AS 
BEGIN 
     OPEN p_cur FOR          
                 SELECT C.KOD_SUG_DOCH, C.TEUR_DOCH
               FROM CTB_SUGEY_DOCHOT c
                WHERE c.KOD_SUG_DOCH = p_kodReport ;
 EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END         pro_get_ReportDetails;             

PROCEDURE pro_get_yechida ( p_cur OUT CurType) AS 
BEGIN 
     OPEN p_cur FOR          
         Select distinct Kod_Yechida, Teur_Yechida 
         From CTB_Yechida 
         WHERE Kod_Yechida>=0 order by Teur_Yechida;
         --91495
 EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END         pro_get_yechida;     
  
 PROCEDURE  pro_get_Definition_Reports(p_BakashaId IN  TB_BAKASHOT.bakasha_id%TYPE, p_cur OUT CurType) AS 
BEGIN 
     OPEN p_cur FOR          
                 SELECT DISTINCT t.KOD_SUG_DOCH KOD, c.SHEM_DOCH_BAKOD NAME,c.TEUR_DOCH, c.TVACH_TAARICHIM,T.BAKASHA_ID,T.EXTENSION_TYPE
                 ,T.MISPAR_ISHI, C.RS_VERSION, r.URL_CONFIG_KEY, r.SERVICE_URL_CONFIG_KEY
               FROM TB_DOCHOT_MISPAR_ISHI t,
                                CTB_SUGEY_DOCHOT c,
                                CTB_DOCHOT_RS_VERSION r
                WHERE t.PAIL=1 AND C.PAIL =1 
                                            AND t.KOD_SUG_DOCH = c.KOD_SUG_DOCH
                                       AND T.BAKASHA_ID = p_BakashaId 
                                       and C.RS_VERSION = r.RS_VERSION ;
                                  --     and t.MISPAR_ISHI = 75757; 
 EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END         pro_get_Definition_Reports;             
  
  PROCEDURE pro_get_Details_Reports      (p_BakashaId IN  TB_BAKASHOT.bakasha_id%TYPE, p_cur OUT CurType) AS 
BEGIN 
IF (p_BakashaId = 0) THEN 
     OPEN p_cur FOR          
                    SELECT p.KOD_SUG_DOCH,p.SHEM_PARAM_BADOCH,p.ERECH,p.TAARICH_IDKUN_ACHARON , T.MISPAR_ISHI
                    FROM TB_DOCHOT_MISPAR_ISHI t,
                    TB_PARAMETRIM_LEDOCHOT p
                    WHERE t.PAIL=1
                    AND p.PAIL=1
                    AND t.KOD_SUG_DOCH = p.KOD_SUG_DOCH
                    AND T.BAKASHA_ID = p.BAKASHA_ID
                    AND T.BAKASHA_ID = p_BakashaId 
                UNION 
                    SELECT t.KOD_SUG_DOCH, 
                    'P_EZOR' AS SHEM_PARAM_BADOCH, 
                    Pkg_Ovdim.FUNC_GET_ERECH_BY_KOD_NATUN (T.MISPAR_ISHI, 3,SYSDATE)    AS  ERECH ,
                    SYSDATE AS TAARICH_IDKUN_ACHARON , T.MISPAR_ISHI
                    FROM TB_DOCHOT_MISPAR_ISHI t
                    WHERE t.PAIL=1 AND T.BAKASHA_ID = 0 AND t.KOD_SUG_DOCH IN(104,500,700);--,809,810,811);
ELSE 
     OPEN p_cur FOR          
                    SELECT p.KOD_SUG_DOCH,p.SHEM_PARAM_BADOCH,p.ERECH,p.TAARICH_IDKUN_ACHARON
                    FROM TB_DOCHOT_MISPAR_ISHI t,
                    TB_PARAMETRIM_LEDOCHOT p
                    WHERE t.PAIL=1
                    AND p.PAIL=1
                    AND t.KOD_SUG_DOCH = p.KOD_SUG_DOCH
                    AND T.BAKASHA_ID = p.BAKASHA_ID
                    AND T.BAKASHA_ID = p_BakashaId ; 
END IF ;                                 


 EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END pro_get_Details_Reports;
  
 PROCEDURE pro_get_Destinations_Reports(p_BakashaId IN  TB_BAKASHOT.bakasha_id%TYPE,p_cur OUT CurType) AS 
BEGIN 
     OPEN p_cur FOR          
                   SELECT DISTINCT  t.KOD_SUG_DOCH, t.SHEM_TIKIYA, CASE WHEN T.Destination_Type =1 THEN   o.EMAIL ELSE NULL END EMAIL , T.MISPAR_ISHI
                 FROM TB_DOCHOT_MISPAR_ISHI t,
                              OVDIM o
                 WHERE t.PAIL=1
                          AND t.MISPAR_ISHI = o.MISPAR_ISHI
                               AND T.BAKASHA_ID = p_BakashaId
                      --       and t.MISPAR_ISHI = 75757
                ORDER BY  t.KOD_SUG_DOCH;
            
 EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END pro_get_Destinations_Reports;
  
  
  
  PROCEDURE Pro_Get_TlunotPundakim(p_Cur OUT Curtype,
                                                    P_STARTDATE IN VARCHAR2,
                                                       P_ENDDATE IN VARCHAR2,
                                                P_MISPAR_ISHI IN VARCHAR2,
                                                P_SUG_REPORT NUMBER )  IS
mis NUMBER;
BEGIN 
 DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'Pro_Get_TlunotPundakim');
IF (P_SUG_REPORT = 1) THEN
   mis := 0;
ELSE mis :=  -1;
END IF;
DBMS_OUTPUT.PUT_LINE (P_SUG_REPORT);
DBMS_OUTPUT.PUT_LINE ('pro_Prepare_Netunim_Tnua:'  || TO_CHAR(SYSDATE,'HH24:mi:ss'));
Pkg_Reports.pro_Prepare_Netunim_Tnua(P_STARTDATE,P_ENDDATE,P_MISPAR_ISHI);
DBMS_OUTPUT.PUT_LINE ('pro_Prepare_Netunim_Tnua:'  || TO_CHAR(SYSDATE,'HH24:mi:ss'));
OPEN p_Cur   FOR
 SELECT pundak.SHEM_MISH , pundak.SHEM_PRAT  , pundak.TEUR_EZOR ,pundak.KOD_EZOR,pundak.TEUR_MAAMAD_HR , pundak.KOD_MAAMAD,
          pundak.TEUR_SNIF_AV, pundak.mispar_ishi, pundak.taarich, pundak.makat, 
              pundak.SHAT_YETZIA  ,pundak.yom ,  pundak.MISPAR_SIDUR,  pundak.OTO_NO,   pundak.SHILUT , pundak.TEUR_NESIA , 
                MAX(CASE WHEN  ((num_pundak=1 AND pundak.makat IS NOT NULL) OR (pundak.num=1 AND pundak.makat IS  NULL))   THEN pundak.TEUR_MIKUM_YECHIDA    ELSE '' END)    pundak1,
                MAX(CASE WHEN  ((num_pundak=2 AND pundak.makat IS NOT NULL)OR (pundak.num=2 AND pundak.makat IS NULL))  THEN pundak.TEUR_MIKUM_YECHIDA    ELSE '' END)    pundak2,
                MAX(CASE WHEN  ((num_pundak=3 AND pundak.makat IS NOT NULL)OR (pundak.num=3 AND pundak.makat IS  NULL))  THEN pundak.TEUR_MIKUM_YECHIDA    ELSE '' END)    pundak3 ,
                MAX(CASE WHEN  ((num_pundak=4 AND pundak.makat IS NOT NULL)OR (pundak.num=4 AND pundak.makat IS  NULL))  THEN pundak.TEUR_MIKUM_YECHIDA    ELSE '' END)    pundak4 ,
                 MAX(CASE WHEN ((num_pundak=1 AND pundak.makat IS NOT NULL)OR (pundak.num=1 AND pundak.makat IS  NULL))THEN pundak.SHAT_HITYAZVUT    ELSE '' END)    shaa1,
                MAX(CASE WHEN ((num_pundak=2 AND pundak.makat IS NOT NULL)OR (pundak.num=2 AND pundak.makat IS  NULL))THEN pundak.SHAT_HITYAZVUT    ELSE '' END)    shaa2,
                MAX(CASE WHEN ((num_pundak=3 AND pundak.makat IS NOT NULL)OR (pundak.num=3 AND pundak.makat IS  NULL))THEN pundak.SHAT_HITYAZVUT    ELSE '' END)    shaa3,
                MAX(CASE WHEN ((num_pundak=4 AND pundak.makat IS NOT NULL)OR (pundak.num=4 AND pundak.makat IS  NULL))THEN pundak.SHAT_HITYAZVUT    ELSE '' END)    shaa4,
                MAX(pundak.avera) avera
 FROM(
 SELECT    OV.SHEM_MISH , OV.SHEM_PRAT  , E.TEUR_EZOR ,E.KOD_EZOR ,M.TEUR_MAAMAD_HR ,SUBSTR(M.KOD_MAAMAD_HR,2)KOD_MAAMAD,
             S.TEUR_SNIF_AV, REPLACE(REPLACE(y.TEUR_MIKUM_YECHIDA,'-�����','' ),'����','��.') TEUR_MIKUM_YECHIDA, H.mispar_ishi,
               H.taarich, H.makat,H.mikum_pundak,H.mikum_shaon,
              H.SHAT_YETZIA  ,H.yom ,  H.MISPAR_SIDUR,  H.OTO_NO, H.SHAT_HITYAZVUT, H.SHILUT , H.TEUR_NESIA ,H.avera,
              DENSE_RANK() OVER(PARTITION BY  H.mispar_ishi,H.taarich,H.SHAT_YETZIA   ORDER BY  H.mikum_pundak  ASC  ) AS num_pundak,
              DENSE_RANK() OVER(PARTITION BY  H.mispar_ishi,H.taarich,H.SHAT_YETZIA   ORDER BY  H.SHAT_HITYAZVUT  ASC  ) AS num 
                
 FROM
         CTB_MIKUM_YECHIDA y,
         PIVOT_PIRTEY_OVDIM T,
         CTB_EZOR E,
         CTB_MAAMAD M,
         CTB_SNIF_AV S,
         OVDIM OV ,      
  
  
   (  SELECT DECODE(x.mispar_ishi,NULL,y.mispar_ishi,x.mispar_ishi)mispar_ishi,
                     DECODE(x.taarich,NULL,y.taarich,x.taarich) taarich,
                       x.makat_nesia   makat,x.mikum_pundak,y.mikum_shaon,
                       x.SHAT_YETZIA  ,NVL( x.yom ,y.yom) yom ,  x.MISPAR_SIDUR,  x.OTO_NO,
                      NVL(TO_CHAR(y.SHAT_HITYAZVUT,'hh24:mi'),'') SHAT_HITYAZVUT, x.SHILUT ,
                       x.TEUR_NESIA ,
                    (CASE 
                       WHEN x.mikum_pundak IS NOT NULL AND y.mikum_shaon IS NULL AND SUBSTR(x.MAKAT_NESIA,6,1) NOT  IN ('6','7','8') THEN 1
                       WHEN x.mikum_pundak IS NOT NULL AND y.mikum_shaon IS NULL AND SUBSTR(x.MAKAT_NESIA,6,1)  IN ('6','7','8') THEN 4 
                       WHEN  x.mikum_pundak IS NULL AND y.mikum_shaon IS NOT NULL AND x.k IS NOT NULL THEN 2
                       WHEN  x.mikum_pundak IS NULL AND y.mikum_shaon IS NOT NULL AND x.k IS NULL THEN 3 
                     ELSE 0
                     END) avera
FROM                     
(SELECT DISTINCT  p.MISPAR_ISHI, p.MAKAT_NESIA, p.TAARICH, v.MIKUM_PUNDAK,
                       p.SHAT_YETZIA, k.SHILUT,k.TEUR_NESIA,
                      Dayofweek(p.TAARICH)  Yom,p.OTO_NO,p.MISPAR_SIDUR,k.MAKAT k,g.MAZAN_TICHNUN
        FROM TB_PEILUT_OVDIM p,
             CTB_MAKATIM_LEPUNDAKIM k,
             TMP_VISUTIM c,
             TB_PUNDAKIM_VISUTIM v ,
              TMP_CATALOG g
        WHERE SUBSTR( LPAD(p.MAKAT_NESIA,8, '0'),0,5) = LPAD(k.MAKAT ,5, '0')  
            AND p.TAARICH BETWEEN k.ME_TAARICH AND k.AD_TAARICH
            AND p.TAARICH  BETWEEN TO_DATE(P_STARTDATE,'dd/mm/yyyy') AND  TO_DATE(P_ENDDATE,'dd/mm/yyyy') 
            AND p.MAKAT_NESIA=c.MAKAT8  
            AND v.MIS_VISUT = SUBSTR(c.NIHUL,2)
            AND  p.TAARICH BETWEEN v.ME_TAARICH AND v.AD_TAARICH
            AND  p.TAARICH=c.START_DATE
               AND  p.TAARICH=g.ACTIVITY_DATE(+)        
           AND p.MAKAT_NESIA = g.MAKAT8(+)  
           AND   (P_MISPAR_ISHI IS NULL OR p.MISPAR_ISHI IN (SELECT x FROM TABLE(CAST(Convert_String_To_Table(P_MISPAR_ISHI ,  ',') AS mytabtype))))
        --   AND p.MISPAR_ISHI = 19942
    ) x      
     FULL JOIN      
    (
            select DISTINCT h.MISPAR_ISHI,h.TAARICH,h.SHAT_HITYAZVUT,Dayofweek(h.TAARICH)  Yom,  SUBSTR(LPAD(h.MIKUM_SHAON ,5, '0'),0,3)     MIKUM_SHAON
            from(
                    select   t.MISPAR_ISHI,t.TAARICH,t.SHAT_HITYAZVUT,t.MIKUM_SHAON 
                    FROM  TB_HITYAZVUT_PUNDAKIM t,ovdim v
                    WHERE   T.MISPAR_ISHI = V.MISPAR_ISHI and
                                  t.TAARICH  BETWEEN TO_DATE(P_STARTDATE,'dd/mm/yyyy') AND  TO_DATE(P_ENDDATE,'dd/mm/yyyy')  and
                                  to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))>5 
                                --  and  T.MISPAR_ISHI = 19818
                             -- T.MIKUM_SHAON='9601'
                 union all
                    select DISTINCT  t.MISPAR_ISHI,(t.TAARICH-1) TAARICH,t.SHAT_HITYAZVUT, t.MIKUM_SHAON 
                    FROM  TB_HITYAZVUT_PUNDAKIM t,ovdim v
                    WHERE   T.MISPAR_ISHI = V.MISPAR_ISHI and
                                  t.TAARICH BETWEEN TO_DATE(P_STARTDATE,'dd/mm/yyyy') AND  (TO_DATE(P_ENDDATE,'dd/mm/yyyy')+1)  and
                                   to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))>=0 and to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))<=5 
                               --        and  T.MISPAR_ISHI = 19818
                                        ) h
                 where   h.TAARICH BETWEEN TO_DATE(P_STARTDATE,'dd/mm/yyyy') AND  TO_DATE(P_ENDDATE,'dd/mm/yyyy') 
     ) y
 
    ON x.MISPAR_ISHI=y.MISPAR_ISHI
             AND x.TAARICH = y.TAARICH
              AND x.MIKUM_PUNDAK = y.MIKUM_SHAON
               AND y.SHAT_HITYAZVUT>= x.SHAT_YETZIA   AND y.SHAT_HITYAZVUT<= (x.SHAT_YETZIA +( x.MAZAN_TICHNUN/1440))
               )H 
               
    WHERE ( y.KOD_MIKUM_YECHIDA = h.mikum_pundak  OR y.KOD_MIKUM_YECHIDA = h.MIKUM_SHAON)
        AND OV.MISPAR_ISHI = H.MISPAR_ISHI
        AND OV.MISPAR_ISHI = T.MISPAR_ISHI
        AND  H.TAARICH BETWEEN T.ME_TARICH AND T.AD_TARICH  
        AND  T.EZOR =E.KOD_EZOR 
        AND OV.KOD_HEVRA=E.KOD_HEVRA 
        AND  T.MAAMAD = M.KOD_MAAMAD_HR 
        AND  OV.KOD_HEVRA=M.KOD_HEVRA  
        AND  T.SNIF_AV = S. KOD_SNIF_AV 
        AND  OV.KOD_HEVRA=S.KOD_HEVRA
        AND h.avera >mis) pundak
 
GROUP BY pundak.SHEM_MISH , pundak.SHEM_PRAT  , pundak.TEUR_EZOR, pundak.KOD_EZOR, pundak.TEUR_MAAMAD_HR , pundak.KOD_MAAMAD,
           pundak.TEUR_SNIF_AV, pundak.mispar_ishi, pundak.taarich, pundak.makat, 
              pundak.SHAT_YETZIA  ,pundak.yom ,  pundak.MISPAR_SIDUR,  pundak.OTO_NO,   pundak.SHILUT  , pundak.TEUR_NESIA --,pundak.avera 
 ORDER BY pundak.TAARICH ,pundak.MISPAR_ISHI, pundak.SHAT_YETZIA; 
 
DBMS_OUTPUT.PUT_LINE ('sql:'  || TO_CHAR(SYSDATE,'HH24:mi:ss'));
END   Pro_Get_TlunotPundakim;

PROCEDURE pro_Prepare_Netunim_Tnua(P_STARTDATE IN VARCHAR2,
                                                                                 P_ENDDATE IN VARCHAR2,
                                                                                 P_MISPAR_ISHI IN VARCHAR2) IS
rc NUMBER ;    
CountQry VARCHAR2(3000);
CountRows NUMBER;
GeneralQry VARCHAR2(3000);
InsertQry  VARCHAR2(3000);                                                                 
BEGIN
DBMS_OUTPUT.PUT_LINE ('1:'  || TO_CHAR(SYSDATE,'HH24:mi:ss'));
        EXECUTE IMMEDIATE  'truncate table tmp_Visutim' ;  
      /*  GeneralQry:= 'select distinct  p.TAARICH,p.MAKAT_NESIA,1 from TB_PEILUT_OVDIM p  ,TB_HITYAZVUT_PUNDAKIM t  where  p.TAARICH= t.TAARICH and t.MISPAR_ISHI=p.MISPAR_ISHI
                                                  and   (p.taarich between to_date( '''    || P_STARTDATE || ''',''dd/MM/yyyy'') and to_date(''' || P_ENDDATE || ''',''dd/MM/yyyy''))';
    */
     GeneralQry:= 'select distinct  p.TAARICH,p.MAKAT_NESIA,1 from TB_PEILUT_OVDIM p  ,CTB_MAKATIM_LEPUNDAKIM c
                                         WHERE  SUBSTR( LPAD(p.MAKAT_NESIA,8, ''0''),0,5) = c.MAKAT AND c.ME_TAARICH<=p.TAARICH AND c.AD_TAARICH>=p.TAARICH AND
                                       (p.taarich BETWEEN TO_DATE( '''    || P_STARTDATE || ''',''dd/MM/yyyy'') AND TO_DATE(''' || P_ENDDATE || ''',''dd/MM/yyyy''))';
        if (P_MISPAR_ISHI is not null) then
               GeneralQry:= GeneralQry || ' and p.mispar_ishi in (' ||   P_MISPAR_ISHI || ')';
        end if;                            
        CountQry := 'Select  nvl(count(*),0)  from (' || GeneralQry || ')'  ;  
        EXECUTE IMMEDIATE CountQry INTO CountRows  ; 
        DBMS_OUTPUT.PUT_LINE ('2:'  || TO_CHAR(SYSDATE,'HH24:mi:ss'));
    --    DBMS_OUTPUT.PUT_LINE (CountRows);
        IF (CountRows > 0 ) THEN                             
                 InsertQry := 'INSERT INTO  tmp_visutim@KDS_GW_AT_TNPR(start_date,makat8,SIDURI) ' || GeneralQry  ;                      
                 EXECUTE IMMEDIATE InsertQry ;
                 GetVisut4Kav@KDS_GW_AT_TNPR(rc);
                 INSERT INTO TMP_VISUTIM ( START_DATE,MAKAT8, SIDURI,NIHUL,  NIHUL_NAME,STATUS) 
                                 SELECT  START_DATE,MAKAT8, SIDURI,NIHUL,  NIHUL_NAME,STATUS  
                                 FROM TMP_VISUTIM@KDS_GW_AT_TNPR ;
                 COMMIT ;        
        END IF;       
        DBMS_OUTPUT.PUT_LINE ('3:'  || TO_CHAR(SYSDATE,'HH24:mi:ss'));
     GeneralQry:='';
        GeneralQry:= ' select  distinct  p.MAKAT_NESIA, p.TAARICH  from TB_PEILUT_OVDIM p  where 
                                    (p.taarich BETWEEN TO_DATE( '''    || P_STARTDATE || ''',''dd/MM/yyyy'') AND TO_DATE(''' || P_ENDDATE || ''',''dd/MM/yyyy''))';
                                    
          if (P_MISPAR_ISHI is not null) then
               GeneralQry:= GeneralQry || ' and p.mispar_ishi in (' ||   P_MISPAR_ISHI ||  ')';
            end if;                       
        Pkg_Reports.pro_Prepare_Catalog_Details(GeneralQry);
        DBMS_OUTPUT.PUT_LINE ('4:'  || TO_CHAR(SYSDATE,'HH24:mi:ss'));
     EXCEPTION 
         WHEN OTHERS THEN 
               RAISE;             
END pro_Prepare_Netunim_Tnua;
/****************************************************/

PROCEDURE pro_getPirteyOvedForWorkCard(p_Cur OUT Curtype,
                                                                             P_TAARICH IN DATE,
                                                                                                P_MISPAR_ISHI IN NUMBER) AS            
    --  P_TAARICH        date;                                                                        
BEGIN
 DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_getPirteyOvedForWorkCard');
 --P_TAARICH  := to_date('01/12/2012','dd/mm/yyyy');
OPEN p_Cur   FOR
        SELECT o.shem_prat  || ' ' ||  o.shem_mish  full_name,Dayofweek(P_TAARICH) yom,
                     t.MAAMAD kod_maamad,  m.TEUR_MAAMAD_HR maamad, 
                     s.TEUR_SNIF_AV snif, y.TACHOGRAF kod_tachograf,
                    z.TEUR_ZMAN_NESIAA,l.TEUR_LINA,
                    y.HAMARAT_SHABAT,h.TEUR_ZMAN_HALBASHA,
                    so1.NIDRESHET_HITIATZVUT HITIATZVUT_A, NVL(TO_CHAR(so1.SHAT_HITIATZVUT,'hh24:mi'),'')  SHAT_HITIATZVUT_A,
                    so1.HACHTAMA_BEATAR_LO_TAKIN HACHTAMA_A,so1.PTOR_MEHITIATZVUT PTOR_A,
                    b.TEUR_SIBA SIBA_A,t.DIRUG,t.DARGA,
                    so2.NIDRESHET_HITIATZVUT HITIATZVUT_B,NVL(TO_CHAR(so2.SHAT_HITIATZVUT,'hh24:mi'),'')  SHAT_HITIATZVUT_B,
                    so2.HACHTAMA_BEATAR_LO_TAKIN HACHTAMA_B,so2.PTOR_MEHITIATZVUT PTOR_B,
                    b2.TEUR_SIBA SIBA_B, y.MEASHER_O_MISTAYEG
                    ,  Pkg_Reports.bdok_shibuz_mechona( P_TAARICH,P_MISPAR_ISHI) SHIBUZ_MECHONA,
                    g.F5,g.NOT_ON_TIME,g.PUBLIC_COMPLAINTS ,
                     pro_get_snif_giyus_lekav(o.kod_hevra,t.HATZAVA_LEKAV)  snif_giyus_kav
                
                 --   pkg_ovdim.func_is_card_last_updated(P_MISPAR_ISHI,P_TAARICH) gorem_metapel
        FROM OVDIM o,      
                  PIVOT_PIRTEY_OVDIM t,
                  CTB_MAAMAD m,
                  CTB_SNIF_AV s,
                  TB_YAMEY_AVODA_OVDIM y,
                  CTB_ZMANEY_NESIAA z,
                  CTB_LINA l,
                  CTB_ZMANEY_HALBASHA h,
                  TB_DWH_DRIVER_GRADES g,
                   TB_SIDURIM_OVDIM so1,
                 CTB_SIBOT_LEDIVUCH_YADANI b,
                 TB_SIDURIM_OVDIM so2,
                  CTB_SIBOT_LEDIVUCH_YADANI b2
                 
        WHERE o.MISPAR_ISHI = P_MISPAR_ISHI
        AND o.MISPAR_ISHI = t.MISPAR_ISHI
        AND  P_TAARICH  BETWEEN t.ME_TARICH AND t.AD_TARICH
        AND t.MAAMAD = m.KOD_MAAMAD_HR
        AND o.KOD_HEVRA = m.KOD_HEVRA
        AND  t.SNIF_AV= s.KOD_SNIF_AV
        AND o.KOD_HEVRA = s.KOD_HEVRA
        AND o.MISPAR_ISHI = y.MISPAR_ISHI
        AND y.TAARICH =  P_TAARICH 
        AND y.BITUL_ZMAN_NESIOT = z.KOD_ZMAN_NESIAA
        AND y.LINA =  l.KOD_LINA
        AND y.HALBASHA = h.KOD_ZMAN_HALBASHA
        AND y.MISPAR_ISHI = g.DRIVER_ID (+)       
        AND P_TAARICH between g.DATE_REPORT(+)   and last_day(g.DATE_REPORT(+))
        AND so1.MISPAR_ISHI(+) = o.MISPAR_ISHI
        AND so1.TAARICH(+) = P_TAARICH 
        AND so1.NIDRESHET_HITIATZVUT(+) = 1 
        AND (so1.bitul_o_hosafa NOT IN(1,3) OR so1.bitul_o_hosafa IS NULL)
        AND so1.KOD_SIBA_LEDIVUCH_YADANI_IN = b.KOD_SIBA(+)
        AND so2.MISPAR_ISHI(+) = o.MISPAR_ISHI
        AND so2.TAARICH(+) = P_TAARICH 
        AND so2.NIDRESHET_HITIATZVUT(+) = 2
        AND (so2.bitul_o_hosafa NOT IN(1,3) OR so2.bitul_o_hosafa IS NULL)
        AND so2.KOD_SIBA_LEDIVUCH_YADANI_IN = b2.KOD_SIBA(+);

     EXCEPTION 
         WHEN OTHERS THEN 
               RAISE;             
END pro_getPirteyOvedForWorkCard;

function pro_get_snif_giyus_lekav(p_kod_hevra in number,HATZAVA_LEKAV in number) return VARCHAR2 is
  snif_kav VARCHAR2(50);
begin
        select s.teur_snif_av into snif_kav
         from  ctb_snif_av s
         where s.kod_SNIF_AV= HATZAVA_LEKAV
         and s.kod_hevra =p_kod_hevra;

   return  snif_kav;  
   
   EXCEPTION 
     WHEN no_data_found THEN
     return '';
        WHEN OTHERS THEN 
            RAISE;    
end pro_get_snif_giyus_lekav;
 PROCEDURE pro_get_list_Rishuy_rechev    (   p_Cur OUT Curtype,
                                                                  P_TAARICH IN DATE,                                                             
                                                               P_MISPAR_ISHI IN NUMBER,
                                                               p_type in number) is
                                               
 begin
    
     open p_Cur for
     select fun_get_list_oto( P_TAARICH   , P_MISPAR_ISHI, p_type,null) s_list from dual;

EXCEPTION
      WHEN OTHERS THEN
        RAISE;
 end  pro_get_list_Rishuy_rechev; 
 
 function fun_get_list_oto( P_TAARICH IN DATE,
                                      P_MISPAR_ISHI IN NUMBER,
                                       p_type in number,
                                       p_mispar_sidur IN NUMBER) return varchar as
          v_list VARCHAR2(1000);
         CURSOR v_Cur IS
           --SELECT distinct P.SHAT_HATCHALA_SIDUR, p.OTO_NO, V.LICENSE_NUMBER
           select z.shaa, z.OTO_NO, z.LICENSE_NUMBER
            from( SELECT distinct p.OTO_NO, V.LICENSE_NUMBER ,
                        min(P.SHAT_HATCHALA_SIDUR) OVER(partition by   p.OTO_NO, V.LICENSE_NUMBER order by P.SHAT_YETZIA asc) shaa
                    FROM TB_SIDURIM_OVDIM s,
                              TB_PEILUT_OVDIM p  ,
                            PIVOT_VEHICLE_AUDIT v
                   WHERE s.MISPAR_ISHI = P_MISPAR_ISHI
                        AND s.TAARICH = trunc(P_TAARICH)  --to_date('25/10/2012','dd/mm/yyyy')  -- 
                        AND s.MISPAR_ISHI = p.MISPAR_ISHI
                        AND s.TAARICH = p.TAARICH
                        AND s.MISPAR_SIDUR =p.MISPAR_SIDUR
                        AND s.SHAT_HATCHALA = p.SHAT_HATCHALA_SIDUR
                        AND P.OTO_NO = v.VEHICLE_ID
                        and p.taarich between v.EVENT_DATE and v.ad_taarich
                        AND((p_type=1   and S.MISPAR_SIDUR not in(select mispar_sidur from TB_SIDURIM_MEYUCHADIM
                                                                                where kod_meafyen =45) )or(p_type=5 and P.MISPAR_SIDUR=p_mispar_sidur )) )z
                order by z.shaa desc  ;       
         --   order by P.SHAT_HATCHALA_SIDUR desc; 
                
                v_rec v_Cur%ROWTYPE;                                                   
 begin
    
    OPEN  v_Cur;
          LOOP
                FETCH  v_Cur INTO v_rec;
                EXIT WHEN v_Cur%NOTFOUND;
                  begin
                        v_list:= v_list ||  ' ; ' || v_rec.LICENSE_NUMBER || '-' || v_rec.OTO_NO   ;
                  end;
          END LOOP;
       CLOSE v_Cur;
       
       return v_list;
       --open p_Cur for
       -- select  v_list from dual;
EXCEPTION
      WHEN OTHERS THEN
        RAISE;
 end  fun_get_list_oto;                                   
PROCEDURE pro_getSidurimVePeiluyotForWC(p_Cur OUT Curtype,
                                                                                                P_TAARICH IN DATE,
                                                                                                P_MISPAR_ISHI IN NUMBER) AS
    GeneralQry VARCHAR2(3000);             
  --  P_TAARICH date;                                                                                                                                                                           
BEGIN
 DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_getSidurimVePeiluyotForWC');
--P_TAARICH:= to_date('01/12/2010','dd/mm/yyyy');
GeneralQry:='';
GeneralQry:='select * from(select  distinct  p.MAKAT_NESIA,  p.TAARICH from TB_PEILUT_OVDIM p  where p.MISPAR_ISHI ='    || P_MISPAR_ISHI || ' and   p.taarich  =   ''' ||  P_TAARICH  || ''')';
Pkg_Reports.pro_Prepare_Catalog_Details(GeneralQry);

OPEN p_Cur   FOR
select A.*, nvl(A.num_row, B.x ) num 
from 
 (    SELECT s.MISPAR_SIDUR, TO_CHAR( s.SHAT_HATCHALA,'hh24:mi')  SHAT_HATCHALA,s.SHAT_HATCHALA SHAT_HATCHALA_FULL,
                     TO_CHAR( s.SHAT_GMAR,'hh24:mi')  SHAT_GMAR,
                     TO_CHAR( p.SHAT_YETZIA,'hh24:mi')  SHAT_YETZIA,p.SHAT_YETZIA SHAT_YETZIA_FULL,
                     p.MAKAT_NESIA, p.OTO_NO,p.mispar_knisa,
                     TO_CHAR( p.SHAT_YETZIA -( NVL(p.KISUY_TOR,0)/1440)  ,'hh24:mi') KISUY_TOR,
                      c.SHILUT, DECODE(p.mispar_knisa,NULL,trim(c.DESCRIPTION),0,trim(c.DESCRIPTION),trim(p.teur_nesia)) TEUR_NESIA  ,c.SUG_SHIRUT_NAME ,c.ONATIUT,
                      p.MISPAR_MATALA,p.MISPAR_SIDURI_OTO,
                      c.EILAT_TRIP  isEilatTrip,trim(p.teur_nesia) teur,p.DAKOT_BAFOAL,   
                       case  when substr(p.MAKAT_NESIA,0,3)='700'  then pkg_elements.fun_get_teur_nekudat_tiful( SUBSTR(p.MAKAT_NESIA,4,3)) else null end   teur_nekudat_tiful,
                      row_number() OVER ( ORDER BY s.SHAT_HATCHALA,p.SHAT_YETZIA,p.mispar_knisa asc ) num_row,
                      case  when (p.MAKAT_NESIA >= 70000000 and  p.MAKAT_NESIA < 80000000) then pkg_elements.fun_get_description_by_kod( to_number(SUBSTR( p.MAKAT_NESIA,2,2))) else null end  TEUR_ELEMENT,
                      case  when (p.MAKAT_NESIA >= 70000000 and  p.MAKAT_NESIA < 80000000) then pkg_sidurim.fun_check_meafyen_exist( to_number(SUBSTR( p.MAKAT_NESIA,2,2)),19) else null end  MEAFYEN_NOSEA,
                      case  when (p.MAKAT_NESIA >= 70000000 and  p.MAKAT_NESIA < 80000000) then pkg_sidurim.fun_check_meafyen_exist( to_number(SUBSTR( p.MAKAT_NESIA,2,2)),11) else null end  meafyen_11

        FROM TB_SIDURIM_OVDIM s,
                  TB_PEILUT_OVDIM p  ,
                 TMP_CATALOG c
        WHERE s.MISPAR_ISHI = P_MISPAR_ISHI
         --    and s.MISPAR_SIDUR =49150
              AND s.TAARICH =  P_TAARICH
              AND s.MISPAR_ISHI = p.MISPAR_ISHI(+)
              AND s.TAARICH = p.TAARICH(+)
              AND s.MISPAR_SIDUR =p.MISPAR_SIDUR(+)
              AND s.SHAT_HATCHALA = p.SHAT_HATCHALA_SIDUR(+)
              AND  p.TAARICH=c.ACTIVITY_DATE(+)
              AND p.MAKAT_NESIA = c.MAKAT8(+)
                AND (p.BITUL_O_HOSAFA <>1 OR p.BITUL_O_HOSAFA IS NULL)
              AND s.MISPAR_SIDUR NOT IN (SELECT MISPAR_SIDUR FROM TB_SIDURIM_MEYUCHADIM WHERE KOD_MEAFYEN = 45)
              AND s.MISPAR_SIDUR NOT IN(99500,99501,99200)
              AND (s.BITUL_O_HOSAFA  NOT IN(1,3) OR s.BITUL_O_HOSAFA IS NULL)
           --   AND e.KOD_MEAFYEN(+) =19
                ) A 
     full join  ( TABLE(CAST(Convert_String_To_Table('1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23,24,25,26,27,28,29,30,31,32'  ,  ',') AS mytabtype)) ) B         
     on   A.num_row = B.x  
 ORDER BY nvl(A.num_row, B.x ) ASC;
     
--DBMS_OUTPUT.PUT_LINE (p_Cur);
 EXCEPTION 
         WHEN OTHERS THEN 
               RAISE;             
END       pro_getSidurimVePeiluyotForWC;

FUNCTION IsEilatTrip(km NUMBER,taarich DATE,eilatTrip NUMBER) RETURN NUMBER AS
erech NUMBER;
BEGIN
     SELECT  NVL(p.ERECH_PARAM,0)
     INTO erech
     FROM TB_PARAMETRIM p
     WHERE p.KOD_PARAM = 149
     AND taarich BETWEEN p.ME_TAARICH AND p.AD_TAARICH;

     IF (km>erech  AND eilatTrip=1) THEN
         RETURN 1;
        ELSE
        RETURN 0;
    END IF;    
 EXCEPTION 
         WHEN OTHERS THEN 
               RAISE;             
END    IsEilatTrip;

FUNCTION bdok_shibuz_mechona(P_TAARICH IN DATE, P_MISPAR_ISHI IN NUMBER) RETURN NUMBER AS

CURSOR p_cur IS
        SELECT p.MAKAT_NESIA,p.OTO_NO
        FROM  TB_SIDURIM_OVDIM s,TB_PEILUT_OVDIM p  
        WHERE s.MISPAR_ISHI = P_MISPAR_ISHI
              AND s.TAARICH =   P_TAARICH 
              AND s.MISPAR_ISHI = p.MISPAR_ISHI(+)
              AND s.TAARICH = p.TAARICH(+)
              AND s.MISPAR_SIDUR =p.MISPAR_SIDUR(+)
              AND s.SHAT_HATCHALA = p.SHAT_HATCHALA_SIDUR(+)
              AND s.MISPAR_SIDUR NOT IN (SELECT MISPAR_SIDUR FROM TB_SIDURIM_MEYUCHADIM WHERE KOD_MEAFYEN = 45)
              AND s.MISPAR_SIDUR NOT IN(99500,99501)
              AND s.BITUL_O_HOSAFA <>1;
    v_rec  p_cur%ROWTYPE;         
    flag VARCHAR(5); 
    kod NUMBER(3); 
BEGIN
         flag:='true';
            FOR v_rec  IN   p_cur   LOOP
                 kod:=-1;
                IF (v_rec.MAKAT_NESIA IS NOT NULL AND SUBSTR(v_rec.MAKAT_NESIA,1,1) ='7') THEN
                    BEGIN
                               SELECT e.KOD_MEAFYEN
                                        INTO kod
                                        FROM  TB_MEAFYENEY_ELEMENTIM e
                                        WHERE e.KOD_ELEMENT = TO_NUMBER( SUBSTR(v_rec.MAKAT_NESIA,2,2) )
                                                  AND e.KOD_MEAFYEN =11;
                              EXCEPTION
                                               WHEN NO_DATA_FOUND  THEN
                                                   kod:=-1;
                         END;
                         IF ( kod <>-1 AND v_rec.OTO_NO IS NULL) THEN
                                flag:='false';
                         END IF; 
                    ELSE
                         IF (v_rec.MAKAT_NESIA IS NOT NULL AND v_rec.OTO_NO IS NULL) THEN
                                flag:='false';
                         END IF; 
                    END IF;
       END LOOP;
    --DBMS_OUTPUT.PUT_LINE (flag);   
       IF (flag ='true') THEN
             RETURN 1;
        ELSE RETURN 0;
    END IF;
          
 EXCEPTION 
         WHEN OTHERS THEN 
               RAISE;             
END    bdok_shibuz_mechona;

PROCEDURE pro_getPrepareReports(P_MISPAR_ISHI IN NUMBER,
                                                                                       P_STATUS_LIST IN VARCHAR2,
                                                                                         p_Cur OUT Curtype ) AS

BEGIN
OPEN p_Cur   FOR  
     SELECT B.Status, B.Bakasha_ID, B.Sug_Bakasha, B.Teur, B.Zman_Hatchala, S.TEUR_STATUS_BAKASHA,
                          SB.TEUR_SUG_BAKASHA,B.Zman_Siyum, O.SHEM_PRAT || '  ' ||O. SHEM_MISH USER_NAME
                      ,D.KOD_SUG_DOCH, D.SHEM_TIKIYA,D.EXTENSION_TYPE
                        FROM TB_BAKASHOT B, 
                                       OVDIM O ,
                                       CTB_STATUS_BAKASHA S,
                                      CTB_SUG_BAKASHA SB,
                                      TB_DOCHOT_MISPAR_ISHI D
                        WHERE B.Mishtamesh_ID=P_MISPAR_ISHI --O.Mispar_Ishi (+)
                        AND B.Mishtamesh_ID=O.Mispar_Ishi 
                        AND B.STATUS=S.KOD_STATUS_BAKASHA
                        AND B.SUG_BAKASHA=SB.KOD_SUG_BAKASHA
                        AND B.BAKASHA_ID = D.BAKASHA_ID
                        AND D.PAIL =1
                        AND B.STATUS IN (SELECT x FROM TABLE(CAST(Convert_String_To_Table(P_STATUS_LIST ,  ',') AS mytabtype)))
ORDER BY B.ZMAN_HATCHALA DESC;

 EXCEPTION 
         WHEN OTHERS THEN 
               RAISE;             
END pro_getPrepareReports;


PROCEDURE pro_updatePrepareReports(P_MISPAR_ISHI IN NUMBER ) AS

BEGIN
        UPDATE     TB_BAKASHOT B
        SET     B.STATUS = 2
        WHERE B.BAKASHA_ID IN(SELECT B.Bakasha_ID
                                                                FROM TB_BAKASHOT B, TB_DOCHOT_MISPAR_ISHI D
                                                                WHERE B.Mishtamesh_ID=P_MISPAR_ISHI 
                                                                AND B.BAKASHA_ID = D.BAKASHA_ID
                                                                AND B.STATUS = 4
                                                                AND D.PAIL =1);
 EXCEPTION 
         WHEN OTHERS THEN 
               RAISE;             
END pro_updatePrepareReports;


PROCEDURE pro_getSidureyVisaForWC(p_Cur OUT Curtype,
                                                                                    P_TAARICH IN DATE,
                                                                                       P_MISPAR_ISHI IN NUMBER) AS
                      --         P_TAARICH date;                                                        
BEGIN
 DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_getSidureyVisaForWC');
OPEN p_Cur   FOR                                                                                                    
        SELECT DISTINCT h.*,po.OTO_NO
FROM(
SELECT s.MISPAR_SIDUR, TO_CHAR( s.SHAT_HATCHALA,'hh24:mi')  SHAT_HATCHALA,
             TO_CHAR( s.SHAT_GMAR,'hh24:mi')  SHAT_GMAR ,s.SECTOR_VISA,  s.YOM_VISA ,
              y.TEUR_YOM_VISA, p.MISPAR_VISA,p.KM_VISA,  TO_CHAR( p.SHAT_YETZIA,'hh24:mi')   SHAT_YETZIA,
              o.shem_prat  || ' ' ||  o.shem_mish  full_name,Dayofweek( P_TAARICH ) yom,
              t.MAAMAD kod_maamad,  m.TEUR_MAAMAD_HR maamad, 
              f.TEUR_SNIF_AV snif, ya.TACHOGRAF kod_tachograf,
              z.TEUR_ZMAN_NESIAA,l.TEUR_LINA,
              ya.HAMARAT_SHABAT, --,x.TEUR_ZMAN_HALBASHA,
              ya.MEASHER_O_MISTAYEG,
              s.SHAT_HATCHALA SHAT_SIDUR, s.TAARICH,s.MISPAR_ISHI ,
              pkg_reports.fun_get_list_oto(P_TAARICH,P_MISPAR_ISHI,5, s.MISPAR_SIDUR) S_LIST,
                pro_get_snif_giyus_lekav(o.kod_hevra,t.HATZAVA_LEKAV) snif_giyus_kav
        --      pkg_ovdim.func_is_card_last_updated(P_MISPAR_ISHI,P_TAARICH) gorem_metapel

FROM TB_SIDURIM_OVDIM s,
          TB_PEILUT_OVDIM p  ,
          CTB_YOM_VISA y,
          OVDIM o,      
          PIVOT_PIRTEY_OVDIM t,
           CTB_MAAMAD m,
           CTB_SNIF_AV f,
           TB_YAMEY_AVODA_OVDIM ya,
           CTB_ZMANEY_NESIAA z,
           CTB_LINA l  --,
        --   CTB_ZMANEY_HALBASHA x
 WHERE s.MISPAR_ISHI =   P_MISPAR_ISHI
              AND s.TAARICH = P_TAARICH 
             AND s.YOM_VISA = y.KOD_YOM_VISA(+)
              AND s.MISPAR_ISHI = p.MISPAR_ISHI(+)
              AND s.TAARICH = p.TAARICH(+)
              AND s.MISPAR_SIDUR =p.MISPAR_SIDUR(+)
              AND s.BITUL_O_HOSAFA  NOT IN(1,3)
              AND s.SHAT_HATCHALA = p.SHAT_HATCHALA_SIDUR(+)
              AND s.MISPAR_SIDUR  IN (SELECT MISPAR_SIDUR FROM TB_SIDURIM_MEYUCHADIM WHERE KOD_MEAFYEN = 45)
              AND SUBSTR(p.MAKAT_NESIA,0,1) ='5'
              AND  o.MISPAR_ISHI =   P_MISPAR_ISHI
              AND o.MISPAR_ISHI = t.MISPAR_ISHI
              AND  P_TAARICH  BETWEEN t.ME_TARICH AND t.AD_TARICH
              AND t.MAAMAD = m.KOD_MAAMAD_HR
              AND o.KOD_HEVRA = m.KOD_HEVRA
              AND t.SNIF_AV= f.KOD_SNIF_AV
              AND o.KOD_HEVRA = f.KOD_HEVRA
              AND o.MISPAR_ISHI = ya.MISPAR_ISHI
              AND ya.TAARICH =   P_TAARICH 
              AND ya.BITUL_ZMAN_NESIOT = z.KOD_ZMAN_NESIAA
             --  and ya.HALBASHA = x.KOD_ZMAN_HALBASHA
              AND ya.LINA =  l.KOD_LINA   )h,
               TB_PEILUT_OVDIM po
WHERE h.MISPAR_ISHI =po.MISPAR_ISHI AND
               h.MISPAR_SIDUR = po.MISPAR_SIDUR AND
               h.TAARICH = po.TAARICH AND
             h.SHAT_SIDUR = po.SHAT_HATCHALA_SIDUR 
        
ORDER BY h.MISPAR_SIDUR,h.SHAT_SIDUR;
END pro_getSidureyVisaForWC;        

PROCEDURE pro_get_HeavyReportsToDelete(p_Cur OUT Curtype) AS

DaysNum NUMBER;
BEGIN

         SELECT p.ERECH_PARAM
        INTO DaysNum
        FROM TB_PARAMETRIM p
        WHERE p.KOD_PARAM = 94;

        OPEN p_Cur   FOR            
                 SELECT D.Bakasha_ID || (CASE    D.EXTENSION_TYPE  WHEN  1  THEN '.pdf'  ELSE  '.exl' END)  ReportName
                   FROM  TB_BAKASHOT B, TB_DOCHOT_MISPAR_ISHI D
                WHERE  B.BAKASHA_ID = D.BAKASHA_ID
                       AND D.PAIL =1 
                       AND B.STATUS=2
                       AND TRUNC((B.ZMAN_SIYUM + DaysNum)) = TRUNC(SYSDATE);
                                                                                              
    UPDATE TB_DOCHOT_MISPAR_ISHI d
        SET d.PAIL = 0
        WHERE d.BAKASHA_ID IN( SELECT B.Bakasha_ID
                                                                              FROM  TB_BAKASHOT B, TB_DOCHOT_MISPAR_ISHI D
                                                                WHERE  B.BAKASHA_ID = D.BAKASHA_ID
                                                                       AND D.PAIL =1
                                                                       AND B.STATUS=2
                                                                       AND TRUNC((B.ZMAN_SIYUM + DaysNum)) = TRUNC(SYSDATE));

     EXCEPTION 
         WHEN OTHERS THEN 
               RAISE; 
END pro_get_HeavyReportsToDelete;


PROCEDURE pro_ChafifotBesidureyNihulTnua(P_STARTDATE IN  TB_SIDURIM_OVDIM.taarich%TYPE ,P_ENDDATE IN TB_SIDURIM_OVDIM.taarich%TYPE,
                                                                                                            P_EZOR IN NUMBER, P_SNIF IN NUMBER,
                                                                                                            p_Cur OUT Curtype) AS
          Param154 NUMBER;
          snifim VARCHAR(100); 
           p_from number;
          p_to number;
BEGIN
     DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_ChafifotBesidureyNihulTnua');
          SELECT ERECH_PARAM INTO Param154
           FROM TB_PARAMETRIM
           WHERE KOD_PARAM =154;
  -- DBMS_OUTPUT.PUT_LINE (Param154);
    p_from:=1;
    select max(s.KOD_SNIF_TNUAA) into  p_to from CTB_SNIFEY_TNUAA s;
     if (p_ezor = 1) then
                p_from:=25;
                p_to :=84;
         else if (p_ezor =2) then
                p_to :=24;    
             else if (p_ezor =3) then
                p_from:=85;
             end if;
         end if;
    end if;

        snifim:= Pkg_Reports.fun_get_snifey_tnua_lezor(P_EZOR,P_SNIF,p_from,p_to);
           DBMS_OUTPUT.PUT_LINE (snifim);
 OPEN p_Cur   FOR
              WITH Tb AS (
                            SELECT     T.TAARICH, T.MISPAR_SIDUR, T.MISPAR_ISHI,T.SHAT_HATCHALA, T.SHAT_GMAR,y.MEASHER_O_MISTAYEG, T.SUG_SIDUR
                            FROM     TB_SIDURIM_OVDIM T, TB_YAMEY_AVODA_OVDIM y,TB_MEAFYENEY_SUG_SIDUR s
                            WHERE     ( T.TAARICH BETWEEN  P_STARTDATE AND P_ENDDATE )
                                     AND T.TAARICH = y.TAARICH
                                     AND T.MISPAR_ISHI = y.MISPAR_ISHI
                                     AND T.taarich BETWEEN s.ME_TAARICH AND NVL ( s.AD_TAARICH,  SYSDATE )
                                     AND s.SUG_SIDUR IS NOT NULL
                                     AND T.sug_sidur = s.SUG_SIDUR
                                     AND ( ( s.KOD_MEAFYEN = 3 AND s.ERECH = 4 ) OR ( s.KOD_MEAFYEN = 52 AND s.ERECH IN (6, 7, 8) ) )
                                     AND y.MEASHER_O_MISTAYEG IS NOT NULL
                                     AND y.MEASHER_O_MISTAYEG IN (0, 1)
                                     AND (snifim ='-1'  OR  SUBSTR(t.MISPAR_SIDUR,0,2) IN (SELECT x FROM TABLE(CAST(Convert_String_To_Table(snifim ,  ',') AS mytabtype))))       
                                     AND T.SHAT_GMAR IS NOT NULL  
                                     ),
                 tb2 AS (        
                            SELECT  DISTINCT t1.TAARICH, t1.MISPAR_SIDUR, t1.MISPAR_ISHI, t1.SHAT_HATCHALA, t1.SHAT_GMAR, t1.SUG_SIDUR
                            FROM tb T LEFT OUTER JOIN tb t1
                                ON      T.MISPAR_SIDUR = t1.MISPAR_SIDUR
                                   AND T.TAARICH = t1.TAARICH
                                   AND ( T.shat_hatchala < t1.shat_gmar OR T.shat_gmar > t1.shat_hatchala )
                                   AND ((t1.shat_hatchala < T.shat_hatchala AND t1.shat_gmar > T.shat_gmar AND (T.shat_gmar - T.shat_hatchala ) *1440 > Param154 ) 
                                      OR (t1.shat_gmar < T.shat_gmar AND ( t1.shat_gmar - T.shat_hatchala ) * 1440 > Param154 )
                                     OR (t1.shat_gmar > T.shat_gmar AND ( T.shat_gmar - t1.shat_hatchala ) * 1440 > Param154 )
                                     OR (t1.shat_hatchala > T.shat_hatchala AND t1.shat_gmar < T.shat_gmar AND ( t1.shat_gmar - t1.shat_hatchala ) * 1440 > Param154 ) )
                         )      
                          SELECT       h2.*,
                                            DENSE_RANK() OVER(PARTITION BY h2.snif_miyun   ORDER BY rownum  ASC  ) AS num       
                          FROM (  SELECT   h.TAARICH, h.MISPAR_SIDUR, h.MISPAR_ISHI,
                                                      SUBSTR ( TO_CHAR ( h.SHAT_HATCHALA, 'dd/mm/yyyy HH24:mi:ss' ), 12, 5 ) SHAT_HATCHALA,
                                                      h.SHAT_HATCHALA full_SHAT_HATCHALA,
                                                      SUBSTR ( TO_CHAR ( h.SHAT_GMAR, 'dd/mm/yyyy HH24:mi:ss' ), 12, 5 ) SHAT_GMAR, o.SHEM_PRAT || ' ' || o.SHEM_MISH full_name,
                                                      s.TEUR_SNIF_AV, R.TEUR_SIDUR_AVODA,S.EZOR,
                                                      Dayofweek ( h.taarich ) || '  ' || c.TEUR_YOM teur_yom,
                                                      first_value (s.TEUR_SNIF_AV) OVER(PARTITION BY   h.TAARICH, h.MISPAR_SIDUR   ORDER BY  h.SHAT_HATCHALA  asc  ) AS snif_miyun,
                                                      first_value (s.EZOR) OVER(PARTITION BY   h.TAARICH, h.MISPAR_SIDUR   ORDER BY  h.SHAT_HATCHALA  asc  ) AS ezor_miyun
                                        FROM   TB_YAMIM_MEYUCHADIM b, CTB_SUGEY_YAMIM_MEYUCHADIM c, OVDIM o, PIVOT_PIRTEY_OVDIM P, CTB_SNIF_AV s, CTB_SUG_SIDUR r, tb2 h    
                                        WHERE       h.taarich = b.taarich(+)
                                                   AND b.sug_yom = c.sug_yom(+)
                                                   AND h.mispar_ishi = o.mispar_ishi
                                                   AND h.MISPAR_ISHI = P.MISPAR_ISHI
                                                   AND P.SNIF_AV = s.KOD_SNIF_AV
                                                   AND o.KOD_HEVRA = s.KOD_HEVRA
                                                   AND h.taarich BETWEEN P.ME_TARICH AND P.AD_TARICH
                                                   AND s.EZOR = P.EZOR
                                                   AND h.sug_sidur = r.KOD_SIDUR_AVODA               
                                                   ORDER BY   h.TAARICH, h.MISPAR_SIDUR, h.SHAT_HATCHALA ) h2
                                              order by  h2.ezor_miyun,h2.snif_miyun,num;
END pro_ChafifotBesidureyNihulTnua;        

FUNCTION fun_get_snifey_tnua_lezor(p_ezor IN NUMBER,p_snif IN NUMBER,p_from number,p_to number ) RETURN VARCHAR AS
   CURSOR p_cur(p_ezor CTB_SNIF_AV.ezor%TYPE ) IS
         SELECT KOD_SNIF_TNUAA SNIF_TNUA
          FROM CTB_SNIFEY_TNUAA s 
          WHERE s.KOD_SNIF_TNUAA between p_from and p_to
          ORDER BY KOD_SNIF_TNUAA;   
               
          snifim VARCHAR(100); 
          v_rec  p_cur%ROWTYPE;
BEGIN
     IF (p_snif=-1 AND p_ezor=-1) THEN
             RETURN '-1';
         ELSE IF(p_snif<>-1) THEN
               RETURN TO_CHAR(p_snif) ;
         ELSE 
                        FOR v_rec  IN   p_cur(p_ezor) LOOP
                          IF (v_rec.SNIF_TNUA IS NOT NULL) THEN
                              snifim:= snifim || TO_CHAR(v_rec.SNIF_TNUA) || ',';
                         END IF;
                     END LOOP;
             END IF;      
    END IF;  
    snifim:=SUBSTR(snifim,0,LENGTH(snifim)-1);
    RETURN snifim;
END fun_get_snifey_tnua_lezor;

PROCEDURE    pro_GetSnifeyTnuaByEzor(p_ezor IN NUMBER,p_Cur OUT Curtype) AS
    p_from number;
    p_to number;
BEGIN
    p_from:=1;
    select max(s.KOD_SNIF_TNUAA) into  p_to from CTB_SNIFEY_TNUAA s;
     if (p_ezor = 1) then
                p_from:=25;
                p_to :=84;
         else if (p_ezor =2) then
                p_to :=24;    
             else if (p_ezor =3) then
                p_from:=85;
             end if;
         end if;
     end if;                    
     OPEN p_Cur FOR
           SELECT -1 KOD_SNIF_TNUAA,'���' teur FROM dual 
          UNION 
           SELECT KOD_SNIF_TNUAA ,teur_snif_tnuaa ||' (' ||KOD_SNIF_TNUAA||')' teur
          FROM CTB_SNIFEY_TNUAA s 
          WHERE s.KOD_SNIF_TNUAA between p_from and p_to
          ORDER BY KOD_SNIF_TNUAA;
     
END   pro_GetSnifeyTnuaByEzor;

PROCEDURE    proGetSnifeyTnuaByEzor(p_ezor IN VARCHAR,p_snif_av in VARCHAR,p_Cur OUT Curtype) AS
BEGIN             
     OPEN p_Cur FOR
          SELECT  s.KOD_SNIF_TNUAA ,s.teur_snif_tnuaa ||' (' ||s.KOD_SNIF_TNUAA||')' teur
          FROM ctb_snif_av cs,  CTB_SNIFEY_TNUAA s 
          WHERE (p_ezor is null or cs.ezor in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_ezor,  ',') AS MYTABTYPE))) )
             and (p_snif_av is null or cs.kod_snif_av in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_snif_av,  ',') AS MYTABTYPE))) ) and cs.SNIF_TNUA   = s.KOD_SNIF_TNUAA
          group by s.KOD_SNIF_TNUAA ,s.teur_snif_tnuaa
          ORDER BY s.KOD_SNIF_TNUAA;
     
END   proGetSnifeyTnuaByEzor;
PROCEDURE pro_get_Nesiot_kfulot(P_STARTDATE IN DATE ,P_ENDDATE IN DATE,
                                                                                                            P_EZOR IN NUMBER, P_SNIF IN NUMBER,
                                                                                                            p_Cur OUT Curtype)        AS
          toD VARCHAR2(15);
          fromD VARCHAR2(15);
          GeneralQry VARCHAR2(3000);
           snifim VARCHAR(70);
               p_from number;
    p_to number;
BEGIN
DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_get_Nesiot_kfulot');
      fromD:= TO_CHAR(P_STARTDATE,'dd/mm/yyyy');
        toD:=TO_CHAR(P_ENDDATE,'dd/mm/yyyy');
 

      GeneralQry:= ' select  distinct  p.MAKAT_NESIA, p.TAARICH  from TB_PEILUT_OVDIM p  where  p.MISPAR_SIDUR not like ''99%''  and
                                    (p.taarich BETWEEN TO_DATE( '''    || fromD || ''',''dd/MM/yyyy'') AND TO_DATE(''' || toD || ''',''dd/MM/yyyy''))  AND  ( LENGTH(p.MISPAR_SIDUR) BETWEEN 4 AND 5)';
    
             
     
    --DBMS_OUTPUT.PUT_LINE (GeneralQry);
    Pkg_Reports.pro_Prepare_Catalog_Details(GeneralQry);
  --  DBMS_OUTPUT.PUT_LINE ('after' || ' ' || sysdate);
  --commit;
  p_from:=1;
    select max(s.KOD_SNIF_TNUAA) into  p_to from CTB_SNIFEY_TNUAA s;
     if (p_ezor = 1) then
                p_from:=25;
                p_to :=84;
         else if (p_ezor =2) then
                p_to :=24;    
             else if (p_ezor =3) then
                p_from:=85;
             end if;
         end if;
    end if;
          snifim:= Pkg_Reports.fun_get_snifey_tnua_lezor(P_EZOR,P_SNIF,p_from,p_to);
   
    --DBMS_OUTPUT.PUT_LINE (snifim);
    --DBMS_OUTPUT.PUT_LINE (pkg_reports.get_snif_mashar(72704,to_date('15/10/2009','dd/MM/yyyy'),21741));
OPEN p_Cur   FOR        
         SELECT h.*  ,SUBSTR(TO_CHAR( h.SHAT_YETZIA  ,'dd/mm/yyyy HH24:mi:ss') ,12,5) SHAT_YEZIA_S,
                     Dayofweek(h.taarich) || '  ' || c.TEUR_YOM teur_yom,SA.EZOR KOD_EZOR_MIYUN,SA.TEUR_SNIF_AV TEUR_SNIF_AV_MIYUN,
                     EZ.TEUR_EZOR TEUR_EZOR_MIYUN
         FROM TB_YAMIM_MEYUCHADIM b, CTB_SUGEY_YAMIM_MEYUCHADIM c,CTB_SNIF_AV sa,CTB_EZOR ez,
                    ( SELECT   t1.TAARICH, lpad(t1.MAKAT_NESIA,8,'0') MAKAT_NESIA ,t1.SHAT_YETZIA,t1.mispar_ishi,  o.SHEM_PRAT || ' ' || o.SHEM_MISH full_name,
                                     t1.MISPAR_SIDUR,  K.DESCRIPTION teur_nesia, nvl(k.SHILUT,t1.SHILUT_NETZER) SHILUT, S.TEUR_SNIF_AV,
                                     MAX(S.KOD_SNIF_AV) OVER (PARTITION BY t1.TAARICH ,t1.makat_nesia,t1.SHAT_YETZIA ) KOD_SNIF_AV_MIYUN  
                       FROM  PIVOT_PIRTEY_OVDIM p,CTB_SNIF_AV s,TMP_CATALOG k,OVDIM o,CTB_EZOR e,
                                    (SELECT  p1.TAARICH,p1.MAKAT_NESIA,p1.SHAT_YETZIA  ,p1.mispar_ishi,p1.MISPAR_SIDUR,p1.MISPAR_KNISA,P1.SHILUT_NETZER,
                                             COUNT(*) OVER (PARTITION BY p1.TAARICH,p1.MAKAT_NESIA,p1.SHAT_YETZIA  ,p1.MISPAR_KNISA) num
                                    FROM TB_PEILUT_OVDIM p1,TB_YAMEY_AVODA_OVDIM y
                                    WHERE  p1.TAARICH  BETWEEN P_STARTDATE AND P_ENDDATE
                                         AND p1.MISPAR_ISHI = y.MISPAR_ISHI
                                         AND p1.TAARICH = y.TAARICH
                                         AND Y.STATUS<>0
                                          AND p1.MISPAR_KNISA=0
                                          AND length(P1.MISPAR_SIDUR) between 4 and 5
                                          AND( p1.MAKAT_NESIA >= 100000 and   p1.MAKAT_NESIA <=50000000)
                                         AND y.MEASHER_O_MISTAYEG IN (0,1) 
                                         AND ( snifim ='-1' OR (  (SUBSTR(p1.MISPAR_SIDUR,0,2)<>'99' AND  SUBSTR(p1.MISPAR_SIDUR,0,2) IN (SELECT x FROM TABLE(CAST(Convert_String_To_Table(snifim ,  ',') AS mytabtype))) ) OR
                                                 (SUBSTR(p1.MISPAR_SIDUR,0,2)='99'    AND  Pkg_Reports.get_snif_mashar(p1.mispar_ishi, p1.TAARICH,p1.oto_no)    IN (SELECT x FROM TABLE(CAST(Convert_String_To_Table(snifim ,  ',') AS mytabtype)))     )) )
                                  ) t1
                    WHERE   t1.num>1 and
                                  t1.MISPAR_ISHI =p.MISPAR_ISHI  AND
                                  t1.MISPAR_ISHI =o.MISPAR_ISHI AND
                                  t1.taarich BETWEEN p.ME_TARICH AND p.AD_TARICH AND 
                                  p.SNIF_AV = s. KOD_SNIF_AV AND
                                  o.KOD_HEVRA=s.KOD_HEVRA AND
                                  k.ACTIVITY_DATE(+)= t1.taarich  AND
                                  k.makat8(+)=t1.MAKAT_NESIA AND 
                                   o.KOD_HEVRA=e.KOD_HEVRA AND
                                    p.ezor=E.KOD_EZOR and
                                  s.EZOR = p.EZOR   ) h
         WHERE  h.taarich = b.taarich(+) AND
                      b.sug_yom=c.sug_yom(+) AND
                      h.KOD_SNIF_AV_MIYUN =sa.KOD_SNIF_AV and
                      SA.EZOR = EZ.KOD_EZOR and
                      SA.KOD_HEVRA = ez.KOD_HEVRA
ORDER BY h.TAARICH,h.MAKAT_NESIA,h.SHAT_YETZIA  ,h.mispar_ishi,h.MISPAR_SIDUR;
END        pro_get_Nesiot_kfulot;                                                                               
    
FUNCTION get_snif_mashar(p_mispar_ishi IN NUMBER, p_taarich IN DATE,p_oto_no IN NUMBER) RETURN VARCHAR AS
          snif VARCHAR(2); 
          branch NUMBER;
BEGIN
     BEGIN
             SELECT DISTINCT s.SNIF_TNUA INTO snif
                FROM CTB_SNIF_AV s,OVDIM o,PIVOT_PIRTEY_OVDIM p
                 WHERE o.MISPAR_ISHI=p_mispar_ishi
                      AND p.MISPAR_ISHI=o.MISPAR_ISHI
                      AND p_taarich BETWEEN p.ME_TARICH AND p.AD_TARICH  
                      AND p.SNIF_AV= s.KOD_SNIF_AV
                      AND o.KOD_HEVRA =s.KOD_HEVRA;
                    --  and s.KOD_SNIF_AV = 88989;
     EXCEPTION
                               WHEN NO_DATA_FOUND  THEN
                                 snif:=0;
    END;
    IF (snif =0) THEN
                --branch:=0;
                 BEGIN
                                   SELECT g.branch2 INTO branch
                                  FROM VEHICLE_SPECIFICATIONS g --VCL_GENERAL_VEHICLE_VIEW@kds2maale  g
                                  WHERE g.bus_number=p_oto_no;
                 EXCEPTION
                                           WHEN NO_DATA_FOUND  THEN
                                             branch:=0;
                  END;
                IF ( branch <> 0 AND  branch IS NOT NULL) THEN
                        IF (branch BETWEEN 1010 AND 3999) THEN
                                 snif:=SUBSTR(TO_CHAR(branch),2,2);
                     ELSE
                                  snif:='43';
                     END IF;
                END IF;
      END IF;
    RETURN snif;
END get_snif_mashar;
    

procedure pro_getPirteyOved(p_isuk IN NUMBER,p_tachana IN NUMBER ,p_Cur OUT Curtype) AS
begin

    OPEN p_Cur   FOR    
        SELECT OV.MISPAR_ISHI,Ov.shem_mish|| ' ' ||  Ov.shem_prat full_name
        FROM OVDIM Ov  ,  PIVOT_PIRTEY_OVDIM t
        WHERE OV.MISPAR_ISHI = T.MISPAR_ISHI
                and sysdate BETWEEN t.ME_TARICH  AND NVL(t.ad_TARICH,TO_DATE( '01/01/9999' , 'dd/mm/yyyy'))
                and  t.isuk= p_isuk --23
                and T.YECHIDA_IRGUNIT =p_tachana;-- 63321;
            
end   pro_getPirteyOved;         

 PROCEDURE  pro_GetIdkuneyRashemetMasach4( P_MISPAR_ISHI IN VARCHAR2 ,
                                                                        P_PERIOD IN VARCHAR2,
                                                                        p_Cur OUT CurType )  IS                                                    
BEGIN
DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_GetIdkuneyRashemetMasach4');
            OPEN p_Cur FOR  
            select distinct * from(
                select S.MISPAR_ISHI,S.TAARICH,S.MISPAR_SIDUR, S.SHAT_HATCHALA, S.SHAT_GMAR,  Dayofweek(Y.taarich) YOM ,
                          Y.STATUS,Y.HAMARAT_SHABAT, Y.HALBASHA,Y.LINA,Y.BITUL_ZMAN_NESIOT,Y.MEASHER_O_MISTAYEG,Y.TACHOGRAF,
                          S.PITZUL_HAFSAKA,S.SECTOR_VISA,S.OUT_MICHSA,S.CHARIGA,S.HASHLAMA, -- R.GOREM_MEADKEN    ,   
                           pkg_reports.pro_get_oved_full_name(R.GOREM_MEADKEN ) GOREM_MEADKEN,
                          to_char(R.TAARICH_IDKUN,'dd/mm/yyyy HH24:mi') TAARICH_IDKUN,R.TAARICH_IDKUN TAARICH_IDKUN_DATE,
                         MAX(R.TAARICH_IDKUN) OVER (PARTITION BY S.MISPAR_ISHI,S.TAARICH,S.MISPAR_SIDUR, S.SHAT_HATCHALA)   MAX_TAARICH              
                from TB_Idkun_Rashemet r,
                         TB_Yamey_Avoda_Ovdim y,
                         TB_Sidurim_Ovdim s
                where  Y.MISPAR_ISHI = S.MISPAR_ISHI
                         and Y.TAARICH = S.TAARICH  
                         and S.MISPAR_ISHI = R.MISPAR_ISHI(+)
                         and S.TAARICH = R.TAARICH(+)
                         and S.MISPAR_SIDUR = R.MISPAR_SIDUR(+)
                         and ((S.MISPAR_SIDUR in (  select SM.MISPAR_SIDUR
                                                                   from tb_sidurim_meyuchadim sm
                                                                   where SM.KOD_MEAFYEN=3 and SM.ERECH=5)) or S.SUG_SIDUR in(select  M.SUG_SIDUR
                                                                                                                                            from TB_MEAFYENEY_SUG_SIDUR m
                                                                                                                                            where M.KOD_MEAFYEN=3 and M.ERECH=5 ))
                         and S.SHAT_HATCHALA =R.SHAT_HATCHALA(+)
                         and to_char(Y.TAARICH,'mm/yyyy') = P_PERIOD 
                         and  Y.MISPAR_ISHI =P_MISPAR_ISHI     ) h
    where    (h.TAARICH_IDKUN_DATE = h.MAX_TAARICH or h.TAARICH_IDKUN_DATE is null)  
   order by h.TAARICH,h.SHAT_HATCHALA,h.TAARICH_IDKUN    ;             
                    
         EXCEPTION 
                WHEN OTHERS THEN 
                       RAISE;                     
END pro_GetIdkuneyRashemetMasach4;
            
FUNCTION pro_get_oved_full_name(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE) RETURN VARCHAR
IS
   p_full_name VARCHAR2(15);
    BEGIN
        SELECT o.SHEM_MISH || ' ' || o.SHEM_PRAT FullName INTO p_full_name
        FROM OVDIM o
        WHERE o.mispar_ishi=p_mispar_ishi;
          return p_full_name;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            p_full_name:='';

    return p_full_name;
END  pro_get_oved_full_name;  
PROCEDURE  pro_get_pirtey_premiya( P_KOD_PREMIYA IN NUMBER, 
                                                         p_Cur OUT CurType )  IS                                                    
BEGIN  
  
   OPEN p_Cur FOR 
        select * 
        from CTB_SUGEY_PREMIOT s
        where S.KOD_PREMIA=P_KOD_PREMIYA;
    
   EXCEPTION 
                WHEN OTHERS THEN 
                       RAISE;           
END    pro_get_pirtey_premiya;    

PROCEDURE pro_get_rechivim_to_average (p_start_date in date, 
                                            p_end_date in date ,
                                            p_mispar_ishi  in number,
                                            p_rechiv in nvarchar2,
                                            p_cur out curtype ) IS
      
     --p_start_date date;
   --      p_end_date date;
BEGIN 
 --  p_start_date:= to_date('01/01/2012','dd/mm/yyyy') ;
   --      p_end_date:=                       to_date('01/01/2012','dd/mm/yyyy') ;
 OPEN p_Cur FOR 
     with b as
    (
        (SELECT   a.mispar_ishi, a.Taarich, a.bakasha_id,
                    count(a.Taarich) over (PARTITION BY a.mispar_ishi) cnt_month
          FROM( SELECT  co.mispar_ishi, co.Taarich, co.bakasha_id,b.TAARICH_HAAVARA_LESACHAR th1,
                              MAX(B.TAARICH_HAAVARA_LESACHAR) OVER (PARTITION BY co.mispar_ishi,co.Taarich )  th2
                 FROM TB_CHISHUV_CHODESH_OVDIM co,TB_BAKASHOT b
                 WHERE   B.BAKASHA_ID = CO.BAKASHA_ID
                        AND B.HUAVRA_LESACHAR=1
                       and (P_RECHIV is null or co.kod_rechiv in ( (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_RECHIV,  ',') AS MYTABTYPE))) ))     
                      and co.mispar_ishi =p_mispar_ishi
                        and CO.TAARICH BETWEEN  p_start_date AND p_end_date--in(to_date('01/09/2012','dd/mm/yyyy') ,  to_date('01/10/2012','dd/mm/yyyy') , to_date('01/02/2013','dd/mm/yyyy'),  to_date('01/03/2013','dd/mm/yyyy'),  to_date('01/04/2013','dd/mm/yyyy'))
                 ) a
            WHERE a.th1=a.th2
             group by   a.mispar_ishi, a.Taarich, a.bakasha_id ) 
    )
    SELECT  CH.TAARICH ,b.cnt_month num  , Ch.Kod_Rechiv ,E.TEUR_SUG_ERECH,
            R.TEUR_RECHIV,
        case when nvl(r.SUG_ERECH_LETZUGA_CHODSHI,0)=1 and nvl(R.SUG_ERECH,0)=2  then  
                      round( (CH.ERECH_RECHIV/60),2) else  
                      case when  (nvl(r.SUG_ERECH_LETZUGA_CHODSHI,0)=2 or nvl(R.SUG_ERECH,0)=2) then   to_number(round(CH.ERECH_RECHIV,1) ) else round(CH.ERECH_RECHIV,2) end   
         end    ERECH_RECHIV,   
               nvl(  R.MIYUN_MEMUZAIM,999) MIYUN_MEMUZAIM
            FROM TB_CHISHUV_CHODESH_OVDIM Ch,ctb_rechivim r, b, ctb_sugey_erech_chishuv e
            WHERE 
            Ch.Mispar_Ishi = p_mispar_ishi
            and CH.KOD_RECHIV=R.KOD_RECHIV
            AND Ch.Taarich  BETWEEN  p_start_date AND p_end_date
           and ch.mispar_ishi= b.mispar_ishi
           and (P_RECHIV is null or ch.kod_rechiv in ( (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_RECHIV,  ',') AS MYTABTYPE))) ))     
           and ch.bakasha_id= b.bakasha_id
           and ch.taarich=b.taarich
           and R.SUG_ERECH_LETZUGA_CHODSHI  =E.KOD_SUG_ERECH(+);

                          
EXCEPTION 
    WHEN OTHERS THEN 
        RAISE;                     
END   pro_get_rechivim_to_average ;


PROCEDURE pro_average_snif_ezor (p_startdate in date, 
                                          p_enddate in date ,
                                          p_ezor  in varchar2,
                                          p_snif  in varchar2,
                                          P_MAMAD_HR IN VARCHAR2 ,
                                          P_ISUK in VARCHAR2 ,
                                          P_WORKERVIEWLEVEL IN NUMBER, 
                                          P_WORKERID in number,
                                          p_cur out curtype )  IS

--  p_start_date date;
   --   p_end_date date;
BEGIN 
DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_average_snif_ezor');
 -- p_start_date:= to_date('01/01/2012','dd/mm/yyyy') ;
  --p_end_date:=                       to_date('01/01/2012','dd/mm/yyyy') ;
    IF (P_WorkerViewLevel = 5) THEN  
            Pkg_Utils.pro_ins_Manage_Tree(TO_NUMBER(P_WORKERID,999999999));
    END IF ;
 OPEN p_Cur FOR 
      with b as
    (
        (SELECT   a.mispar_ishi, a.Taarich, a.bakasha_id
          FROM( SELECT  co.mispar_ishi, co.Taarich, co.bakasha_id,b.TAARICH_HAAVARA_LESACHAR th1,
                              MAX(B.TAARICH_HAAVARA_LESACHAR) OVER (PARTITION BY co.mispar_ishi,co.Taarich )  th2
                 FROM TB_CHISHUV_CHODESH_OVDIM co,TB_BAKASHOT b
                 WHERE   B.BAKASHA_ID = CO.BAKASHA_ID
                        AND B.HUAVRA_LESACHAR=1
                        and CO.TAARICH BETWEEN  p_startdate AND p_enddate
                 ) a
            WHERE a.th1=a.th2
             group by   a.mispar_ishi, a.Taarich, a.bakasha_id ) 
    ),
    rechivim as
       (
          SELECT ch.mispar_ishi,   CH.TAARICH , Ch.Kod_Rechiv ,CH.ERECH_RECHIV , P.SNIF_AV, P.EZOR,   e.TEUR_EZOR, S.TEUR_SNIF_AV
                FROM TB_CHISHUV_CHODESH_OVDIM Ch, b, pivot_pirtey_ovdim p, ctb_ezor e,  ctb_snif_av s,    ovdim o 
                WHERE 
                            ch.mispar_ishi= b.mispar_ishi
                       and ch.bakasha_id= b.bakasha_id
                       and ch.taarich=b.taarich
                       and Ch.Taarich  BETWEEN   p_startdate AND p_enddate
                       and ch.mispar_ishi= p.mispar_ishi
                       and P.ISUK is not null
                       and last_day(Ch.Taarich) BETWEEN  p.ME_TARICH  AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                       and p.mispar_ishi= o.mispar_ishi
                       and p.ezor= e.KOD_EZOR     
                       and O.KOD_HEVRA=E.KOD_HEVRA
                       and p.snif_av= S.KOD_SNIF_AV      
                       and O.KOD_HEVRA=s.KOD_HEVRA
                      and (p_snif is null or P.SNIF_AV in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_snif,  ',') AS MYTABTYPE))))
                      and ((p_ezor='-1')  or P.EZOR in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_ezor,  ',') AS MYTABTYPE))))
                      and (P_MAMAD_HR is null or P.maamad in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MAMAD_HR,  ',') AS MYTABTYPE))))
                      and (p_isuk is null or P.isuk in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_isuk,  ',') AS MYTABTYPE))))
                      and (P_WorkerViewLevel<>5 or ch.mispar_ishi in (select * from TMP_MANAGE_TREE) )  
        )
        SELECT *
     from(
             select  zo.TAARICH , zo.Kod_Rechiv, zo.SNIF_AV, zo.EZOR,
                          r.TEUR_RECHIV,e.TEUR_SUG_ERECH,nvl(r.MIYUN_MEMUZAIM,999) MIYUN_MEMUZAIM,
                          zo.TEUR_EZOR, zo.TEUR_SNIF_AV,
                          case when nvl(r.SUG_ERECH_LETZUGA_CHODSHI,0)=1 and nvl(r.SUG_ERECH,0)=2  then     round( (zo.ERECH_RECHIV/60),2) else zo.ERECH_RECHIV end     ERECH_RECHIV , 
                         CASE WHEN p_snif IS NOT NULL THEN  count(DISTINCT zo.TAARICH) over (PARTITION BY  zo.SNIF_AV) ELSE
                                                                                   count(DISTINCT zo.TAARICH) over (PARTITION BY  zo.ezor)  END NUM
               from ctb_rechivim r,  ctb_sugey_erech_chishuv e  ,      
                      (select    z.TAARICH , z.Kod_Rechiv, z.SNIF_AV, z.EZOR, z.TEUR_EZOR, z.TEUR_SNIF_AV,count(z.mispar_ishi) cnt_ovdim,sum(z.ERECH_RECHIV ) ERECH_RECHIV
                       from rechivim z
                       group by   z.TAARICH , z.Kod_Rechiv, z.SNIF_AV, z.EZOR, z.TEUR_EZOR, z.TEUR_SNIF_AV  ) zo
                      where   zo.KOD_RECHIV=R.KOD_RECHIV  
                            and r.SUG_ERECH_LETZUGA_CHODSHI  =E.KOD_SUG_ERECH(+)
               union
                      (select    z.TAARICH ,0 Kod_Rechiv, z.SNIF_AV, z.EZOR, '���� ������ ����� �����' TEUR_RECHIV,'' TEUR_SUG_ERECH,0 MIYUN_MEMUZAIM,
                                 z.TEUR_EZOR, z.TEUR_SNIF_AV,count(z.mispar_ishi ) ERECH_RECHIV,
                                 CASE WHEN z.SNIF_AV IS NOT NULL THEN  count(DISTINCT z.TAARICH) over (PARTITION BY  z.SNIF_AV) ELSE
                                                                                   count(DISTINCT z.TAARICH) over (PARTITION BY  z.ezor)  END NUM
                       from rechivim z
                       group by    z.TAARICH , z.SNIF_AV, z.EZOR, z.TEUR_EZOR, z.TEUR_SNIF_AV  ))
                       order by        MIYUN_MEMUZAIM;
                    

EXCEPTION 
    WHEN OTHERS THEN 
        RAISE;                     
END   pro_average_snif_ezor ;

PROCEDURE pro_average_ovdim_at_snif (p_startdate in date, 
                                          p_enddate in date ,
                                          p_ezor  in varchar2,
                                          p_snif  in varchar2,
                                          P_WORKERVIEWLEVEL IN NUMBER, 
                                          P_WORKERID in number,
                                          p_cur out curtype )  IS
      
     --p_start_date date;
   --      p_end_date date;
BEGIN 
DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_average_ovdim_at_snif');
 --  p_start_date:= to_date('01/01/2012','dd/mm/yyyy') ;
   --      p_end_date:=                       to_date('01/01/2012','dd/mm/yyyy') ;
     IF (P_WorkerViewLevel = 5) THEN  
            Pkg_Utils.pro_ins_Manage_Tree(TO_NUMBER(P_WORKERID,999999999));
    END IF ; 
    
 OPEN p_Cur FOR 
       with b as
    (
        (SELECT   a.mispar_ishi, a.Taarich, a.bakasha_id,
                    count(a.Taarich) over (PARTITION BY a.mispar_ishi) cnt_month
          FROM( SELECT  co.mispar_ishi, co.Taarich, co.bakasha_id,b.TAARICH_HAAVARA_LESACHAR th1,
                              MAX(B.TAARICH_HAAVARA_LESACHAR) OVER (PARTITION BY co.mispar_ishi,co.Taarich )  th2
                 FROM TB_CHISHUV_CHODESH_OVDIM co,TB_BAKASHOT b
                 WHERE   B.BAKASHA_ID = CO.BAKASHA_ID
                        AND B.HUAVRA_LESACHAR=1
                        and CO.TAARICH BETWEEN  p_startdate AND p_enddate       
                 ) a
            WHERE a.th1=a.th2
             group by   a.mispar_ishi, a.Taarich, a.bakasha_id ) 
    ),
    cc as (select count(distinct taarich) num from b),
     p as
    (  select p.mispar_ishi,P.SNIF_AV,P.ISUK,p.gil,P.EZOR, P.MAAMAD
        from pivot_pirtey_ovdim p,
                     (SELECT po.mispar_ishi,MAX(po.ME_TARICH) me_taarich
                                           FROM PIVOT_PIRTEY_OVDIM PO
                                           WHERE po.isuk IS NOT NULL
                                                 AND (p_startdate BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                                                   OR  p_enddate BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                                                    OR   po.ME_TARICH>=p_startdate AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=p_enddate)
                                          GROUP BY po.mispar_ishi) RelevantDetails 
                where   p.mispar_ishi= RelevantDetails.mispar_ishi
                     and   P.ME_TARICH = RelevantDetails.me_taarich  
                    and (p_snif is null or P.SNIF_AV in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_snif,  ',') AS MYTABTYPE))))
                    and ((p_ezor='-1')  or P.EZOR in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_ezor,  ',') AS MYTABTYPE))))
        )
  
  select   rr.mispar_ishi, O.SHEM_MISH || ' ' || O.SHEM_PRAT oved_name,  cc.num  , rr.Kod_Rechiv ,rr.TEUR_SUG_ERECH, rr.TEUR_RECHIV,
             rr.ERECH_RECHIV, rr.MIYUN_MEMUZAIM ,S.TEUR_SNIF_AV, G.TEUR_KOD_GIL, I.TEUR_ISUK,P.EZOR,M.TEUR_MAAMAD_HR
  from   cc,ovdim o,p,ctb_snif_av s,ctb_kod_gil g, ctb_isuk i,ctb_maamad m,
         (  select z.mispar_ishi  , z.Kod_Rechiv ,E.TEUR_SUG_ERECH,R.TEUR_RECHIV, 
                    case when nvl(r.SUG_ERECH_LETZUGA_CHODSHI,0)=1 and nvl(R.SUG_ERECH,0)=2  then     round( (z.ERECH_RECHIV/60),2) else z.ERECH_RECHIV  end     ERECH_RECHIV,   
                     nvl(  R.MIYUN_MEMUZAIM,999) MIYUN_MEMUZAIM
           from   ctb_rechivim r,  ctb_sugey_erech_chishuv e,
                  ( SELECT ch.mispar_ishi  , Ch.Kod_Rechiv, sum(CH.ERECH_RECHIV) ERECH_RECHIV
                    FROM TB_CHISHUV_CHODESH_OVDIM Ch, b
                    WHERE   ch.mispar_ishi= b.mispar_ishi
                                   and ch.bakasha_id= b.bakasha_id
                                   and ch.taarich=b.taarich
                                   and Ch.Taarich  BETWEEN  p_startdate  AND p_enddate 
                    group by  ch.mispar_ishi  , Ch.Kod_Rechiv) z
            where  z.KOD_RECHIV=R.KOD_RECHIV
                    and R.SUG_ERECH_LETZUGA_CHODSHI  =E.KOD_SUG_ERECH(+) ) rr
    where    o.mispar_ishi= rr.mispar_ishi 
            and rr.mispar_ishi =p.mispar_ishi
            and p.snif_av= S.KOD_SNIF_AV      
            and O.KOD_HEVRA=s.KOD_HEVRA
            and p.gil= G.KOD_GIL_HR
            and p.isuk= I.KOD_ISUK
            and O.KOD_HEVRA=i.KOD_HEVRA
              and p.maamad=M.KOD_MAAMAD_HR
            and O.KOD_HEVRA=m.KOD_HEVRA
            and (P_WorkerViewLevel<>5 or rr.mispar_ishi in (select * from TMP_MANAGE_TREE) )  ;
                          
EXCEPTION 
    WHEN OTHERS THEN 
        RAISE;                     
END   pro_average_ovdim_at_snif ;

PROCEDURE pro_get_SomeRechivimToAverage (p_start_date in date, 
                                            p_end_date in date ,
                                            p_mispar_ishi  in number,
                                            p_Rechivims varchar2,
                                            p_cur out curtype ) IS
     str_dates VARCHAR2(500);    
     v_tar_me DATE;                                       
BEGIN 
    v_tar_me:=p_start_date;
   LOOP
        BEGIN
        EXIT WHEN  v_tar_me>p_end_date;
                str_dates:=str_dates || TO_CHAR(v_tar_me,'dd/mm/yyyy') || ',';
                 v_tar_me:= add_months(v_tar_me,1);
        END;
     END LOOP;

     str_dates:=SUBSTR(str_dates,0,LENGTH(str_dates)-1);
OPEN p_Cur FOR 
      select TO_DATE(tmp2.x,'dd/mm/yyyy')  TAARICH,a.Kod_Rechiv ,a.TEUR_RECHIV,a.ERECH_RECHIV    from 
     ( SELECT CH.TAARICH  ,Ch.Kod_Rechiv ,R.TEUR_RECHIV,CH.ERECH_RECHIV      
        FROM TB_CHISHUV_CHODESH_OVDIM Ch,ctb_rechivim r
        WHERE (( p_mispar_ishi IS NULL ) OR (Ch.Mispar_Ishi = p_mispar_ishi))
        and CH.KOD_RECHIV=R.KOD_RECHIV
     --   and  R.LETZUGA_BESIKUM_CHODSHI is not  null
        AND Ch.Taarich  BETWEEN p_start_date  AND p_end_date
        AND Ch.Bakasha_ID IN(SELECT B.BAKASHA_ID
                                        FROM  TB_BAKASHOT B
                                        WHERE B.TAARICH_HAAVARA_LESACHAR IN 
                                                                                                    ( SELECT /*+  full(_CHISHUV_CHODESH_OVDIM  ) */   MAX(Taarich_Haavara_Lesachar) MaxDate 
                                                                                                        FROM TB_BAKASHOT B, TB_CHISHUV_CHODESH_OVDIM   cc
                                                                                                        WHERE b.Bakasha_ID=cc. Bakasha_ID  
                                                                                                         and   cc.Taarich  BETWEEN p_start_date AND p_end_date
                                                                                                        and cc.Mispar_Ishi =p_mispar_ishi
                                                                                                       AND Taarich_Haavara_Lesachar IS NOT NULL
                                                                                                         AND B.SUG_BAKASHA=1
                                                                                                       -- AND B.STATUS =   2
                                                                                                     --AND B.HUAVRA_LESACHAR=1
                                                                                                     group by cc.Taarich
                                                                                                    )   ) ) a,
                    TABLE(CAST(Convert_String_To_Table(str_dates ,  ',') AS mytabtype)) tmp2                                                                                     
             where TO_DATE(tmp2.x,'dd/mm/yyyy')=a.Taarich(+)  ;
                          
EXCEPTION 
    WHEN OTHERS THEN 
        RAISE;                     
END   pro_get_SomeRechivimToAverage ;



PROCEDURE get_query4 ( p_mispar_ishi  in OVDIM.mispar_ishi%TYPE,p_date in varchar2,  p_cur out curtype ) is
begin
DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'get_query4');
open p_cur for
select o.mispar_ishi, o.mispar_sidur,to_char(o.taarich,'dd/mm/yyyy') taarich,DayOfWeek(o.taarich) Day_Of_Week , to_char(o.shat_gmar,'hh24:mi') shat_gmar , to_char(o.shat_hatchala,'hh24:mi') shat_hatchala , nvl(o.out_michsa,0) out_michsa,
         t_in.teur_siba hachtama_in, t_out.teur_siba hachtama_out,
         t_hashlama.teur_siba hashlama, decode(o.lo_letashlum,1,'1-' || T_LETASHLUM.TEUR_SIBA,'') loletashlum,
         t_pitzul.teur_pizul_hafsaka pitzul,t_chariga.teur_divuch hariga, o1.shem_prat || '  ' || o1.shem_mish full_name
from tb_sidurim_ovdim o , ctb_sibot_ledivuch_yadani t_in, ctb_sibot_ledivuch_yadani t_out,
        ctb_sibot_loletashlum t_letashlum,ctb_sibot_hashlama_leyom t_hashlama,ctb_pitzul_hafsaka t_pitzul,
        ctb_divuch_hariga_meshaot t_chariga,
        ovdim o1
where 
        o.mispar_ishi = o1.mispar_ishi(+)
        and o.mispar_ishi = p_mispar_ishi and to_date(to_char(o.taarich,'mm/yyyy'),'mm/yyyy') = to_date(p_date,'mm/yyyy')
        and o.kod_siba_ledivuch_yadani_in = t_in.kod_siba(+)
        and o.kod_siba_ledivuch_yadani_in = t_out.kod_siba(+)
        and o.kod_siba_lo_letashlum = t_letashlum.kod_siba(+)
        and o.hashlama=t_hashlama.kod_siba(+)
        and o.pitzul_hafsaka = t_pitzul.kod_pizul_hafsaka(+)
        and o.chariga = t_chariga.kod_divuch(+)
        and o.mispar_sidur<>99200
        ORDER BY o.taarich ASC,o.shat_hatchala;
end  get_query4;
 
procedure get_GetDayDataEggT( p_BakashaId IN TB_BAKASHOT.bakasha_id%TYPE,  
                                           --     p_Period IN VARCHAR2,
                                                p_cur out curtype ) is

    p_ToDate DATE ; 
    p_FromDate DATE ; 
BEGIN 
DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'get_GetDayDataEggT');
DBMS_OUTPUT.put_line('111');
    select min(taarich)    into p_FromDate
    from tb_chishuv_chodesh_ovdim
    where bakasha_id= p_BakashaId;
     DBMS_OUTPUT.put_line(p_FromDate);
      select  last_day(max(taarich))  into   p_ToDate
    from tb_chishuv_chodesh_ovdim
    where bakasha_id= p_BakashaId;
 --   p_FromDate :=  TO_DATE('01/' || p_Period,'dd/mm/yyyy'); /* period= 05/2009=>  v_MinLimitDate = 01/05/2009 */
 --   p_ToDate := ADD_MONTHS(p_FromDate,1) -1 ;    /* period= 05/2009=>  v_MaxLimitDate = 31/05/2009 */

DBMS_OUTPUT.put_line(p_ToDate);
OPEN p_cur for 
     with p as
    (  
       select p.mispar_ishi,p.me_tarich,p.ad_tarich
       from PIVOT_PIRTEY_OVDIM p, ctb_snif_av s
       WHERE 
                 (p_FromDate BETWEEN  p.ME_TARICH  AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                   OR p_ToDate BETWEEN  p.ME_TARICH  AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                    OR (  p.ME_TARICH>=p_FromDate  AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=p_ToDate))
            and p.SNIF_AV=S.KOD_SNIF_AV
            and S.KOD_HEVRA=4895
     )
     
,so as
   (
        select soInternal.MISPAR_ISHI , soInternal.TAARICH,min(soInternal.SHAT_HATCHALA) SHAT_HATCHALA , max(soInternal.SHAT_GMAR) SHAT_GMAR 
        from tb_sidurim_ovdim soInternal,p
        where
                soInternal.MISPAR_ISHI =P.MISPAR_ISHI
                 and  soInternal.Taarich between p.me_tarich and p.ad_tarich
                 and soInternal.Taarich BETWEEN  p_FromDate AND p_ToDate
                 and SOINTERNAL.MISPAR_SIDUR not in(99200,99214)
        group by soInternal.mispar_ishi,soInternal.TAARICH
      ) 

select   zo.MISPAR_ISHI,
        TO_CHAR(zo.taarich ,'yyyymmDD') StartDate ,
        trim(TO_CHAR(zo.R126 ,'9999')) R126,
        so.SHAT_HATCHALA , 
        so.SHAT_GMAR, 
        TRIM(TO_NUMBER(zo.R1) -(NVL(zo.R108,0)*60))  R1_108,
        trim(TO_CHAR(zo.R18 ,'9999')) R18,
        trim(TO_CHAR(zo.R32 ,'9999')) R32,
        trim(TO_CHAR(zo.R76 ,'9999')) R76 ,
        trim(TO_CHAR(zo.R77 ,'9999')) R77 ,
        trim(TO_CHAR(zo.R78 ,'9999')) R78 ,
        trim(TO_CHAR(zo.R67 ,'0.000')) R67 ,
        trim(TO_CHAR(zo.R219*60 ,'9999')) R219 ,
        trim(TO_CHAR(zo.R66 ,'0.000')) R66 ,
        trim(TO_CHAR(   (( nvl(zo.R219,0) +nvl(zo.r5,0))    *60) ,'9999')) R219_5 ,
        trim(TO_CHAR(zo.R60 ,'0.000'))   R60, 
        trim(TO_CHAR(zo.R96 ,'9999')) R96,
        trim(TO_CHAR(zo.R49 ,'9')) R49,
        trim(TO_CHAR(zo.R131 ,'9999')) R131,
         trim(TO_CHAR(zo.R295 ,'9999')) R295,
          trim(TO_CHAR(zo.R298 ,'9999')) R298
from so,
        (  SELECT   z.MISPAR_ISHI, z.TAARICH,
                  SUM(  CASE kod_rechiv WHEN 1 THEN Erech_Rechiv ELSE null END ) R1,
                   SUM(  CASE kod_rechiv WHEN 5 THEN Erech_Rechiv ELSE null END) R5,
                  SUM(   CASE kod_rechiv WHEN 18 THEN Erech_Rechiv ELSE null END) R18,
                  SUM(   CASE kod_rechiv WHEN 32 THEN Erech_Rechiv ELSE null END) R32,
                 SUM(    CASE kod_rechiv WHEN 49 THEN Erech_Rechiv ELSE null END) R49,
                 SUM(    CASE kod_rechiv WHEN 60 THEN Erech_Rechiv ELSE null END) R60,
                  SUM(   CASE kod_rechiv WHEN 66 THEN Erech_Rechiv ELSE null END) R66,
                 SUM(    CASE kod_rechiv WHEN 67 THEN Erech_Rechiv ELSE null END) R67,
                 SUM(    CASE kod_rechiv WHEN 76 THEN Erech_Rechiv ELSE null END) R76,
                  SUM(   CASE kod_rechiv WHEN 77 THEN Erech_Rechiv ELSE null END) R77,
                 SUM(    CASE kod_rechiv WHEN 78 THEN Erech_Rechiv ELSE null END) R78,
                 SUM(    CASE kod_rechiv WHEN 96 THEN Erech_Rechiv ELSE null END) R96,
                SUM(     CASE kod_rechiv WHEN 108 THEN Erech_Rechiv ELSE null END) R108,
                SUM(     CASE kod_rechiv WHEN 126 THEN Erech_Rechiv ELSE null END) R126,
                SUM(     CASE kod_rechiv WHEN 131 THEN Erech_Rechiv ELSE null END )R131,      
                SUM(   CASE kod_rechiv WHEN 219 THEN Erech_Rechiv ELSE NULL END) R219 ,
                SUM(   CASE kod_rechiv WHEN 295 THEN Erech_Rechiv ELSE NULL END) R295,    
                SUM(   CASE kod_rechiv WHEN 298 THEN Erech_Rechiv ELSE NULL END) R298        
      from(
                  select ch.*
                  from TB_CHISHUV_YOMI_OVDIM Ch,p
                  where Ch.Taarich BETWEEN  p_FromDate  AND p_ToDate
                     AND Ch.Bakasha_ID = p_BakashaId
                     and CH.KOD_RECHIV in(1,5,18,32,49,60,66,67,76,77,78,96,108,126,131,219,295,298)
                     and CH.MISPAR_ISHI=P.MISPAR_ISHI
                       and ch.Taarich between p.me_tarich and p.ad_tarich
                 ) z
         GROUP BY   z.MISPAR_ISHI, z.TAARICH                 
     ) zo
where   zo.mispar_ishi= so.mispar_ishi
    and zo.taarich = so.taarich
order by      zo.mispar_ishi, zo.taarich ;
/*select 
        O.MISPAR_ISHI,
        TO_CHAR(co.taarich ,'yyyymmDD') StartDate ,
        trim(TO_CHAR(co.R126 ,'99999')) R126,
        so.SHAT_HATCHALA , 
        so.SHAT_GMAR, 
        TRIM(TO_NUMBER(co.R1) -(NVL(co.R108,0)*60))  R1_108,
        trim(TO_CHAR(co.R18 ,'99999')) R18,
        trim(TO_CHAR(co.R32 ,'99999')) R32,
        trim(TO_CHAR(co.R76 ,'9999')) R76 ,
        trim(TO_CHAR(co.R77 ,'9999')) R77 ,
        trim(TO_CHAR(co.R78 ,'9999')) R78 ,
        trim(TO_CHAR(co.R67 ,'99.999')) R67 ,
        trim(TO_CHAR(co.R219/60 ,'9999')) R219 ,
        trim(TO_CHAR(co.R66 ,'99.999')) R66 ,
        trim(TO_CHAR(   (( co.R219 +co.r5)    /60) ,'9999')) R219_5 ,
        trim(TO_CHAR(co.R60 ,'9.999'))   R60, 
        trim(TO_CHAR(co.R96 ,'9999')) R96,
        trim(TO_CHAR(co.R49 ,'9')) R49,
        trim(TO_CHAR(co.R131 ,'99999')) R131
from
PIVOT_PIRTEY_OVDIM  Po  ,
(
        select MISPAR_ISHI , TAARICH,min(SHAT_HATCHALA) SHAT_HATCHALA , max(SHAT_GMAR) SHAT_GMAR 
        from tb_sidurim_ovdim soInternal
        where
                soInternal.Taarich BETWEEN p_FromDate  AND p_ToDate 
                  and SOINTERNAL.MISPAR_SIDUR not in(99200,99214)
--        soInternal.TAARICH   between to_date('01/07/2012','dd/MM/yyyy') and to_date('30/07/2012','dd/MM/yyyy') 
        group by soInternal.mispar_ishi,soInternal.TAARICH
 ) so ,
(SELECT po.mispar_ishi,MAX(po.ME_TARICH) me_taarich
                       FROM PIVOT_PIRTEY_OVDIM PO
                       WHERE po.isuk IS NOT NULL
                             AND( (p_FromDate) BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                               OR p_ToDate  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                               OR  ( po.ME_TARICH>= p_FromDate  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<= p_ToDate  ))
    --                         AND (to_date('01/07/2012','dd/MM/yyyy') BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
      --                        OR  to_date('30/07/2012','dd/MM/yyyy')  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
        --                      OR   po.ME_TARICH>= to_date('01/07/2012','dd/MM/yyyy')  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=  to_date('30/07/2012','dd/MM/yyyy') )
                                 
                      GROUP BY po.mispar_ishi) RelevantDetails,
        ovdim o, 
        ctb_snif_av cs,
        CTB_ISUK Isuk,
 (
SELECT   cco.MISPAR_ISHI,cco.TAARICH,
        sum(cco.R1) R1,          sum(cco.R5) R5,            sum(cco.R18) R18,         sum(cco.R32) R32,        sum(cco.R49) R49,        
       sum(cco.R60) R60,        sum(cco.R66) R66,        sum(cco.R67) R67,        sum(cco.R76) R76,        sum(cco.R77) R77,        
       sum(cco.R78) R78,        sum(cco.R96) R96,        sum(cco.R108) R108,        sum(cco.R126) R126,        sum(cco.R131) R131,      sum(cco.R219) R219
        FROM     ( SELECT   CH.MISPAR_ISHI, CH.TAARICH,
                    CASE kod_rechiv WHEN 1 THEN Erech_Rechiv ELSE NULL END R1,
                    CASE kod_rechiv WHEN 5 THEN Erech_Rechiv ELSE NULL END R5,
                    CASE kod_rechiv WHEN 18 THEN Erech_Rechiv ELSE NULL END R18,
                    CASE kod_rechiv WHEN 32 THEN Erech_Rechiv ELSE NULL END R32,
                    CASE kod_rechiv WHEN 49 THEN Erech_Rechiv ELSE NULL END R49,
                    CASE kod_rechiv WHEN 60 THEN Erech_Rechiv ELSE NULL END R60,
                    CASE kod_rechiv WHEN 66 THEN Erech_Rechiv ELSE NULL END R66,
                    CASE kod_rechiv WHEN 67 THEN Erech_Rechiv ELSE NULL END R67,
                    CASE kod_rechiv WHEN 76 THEN Erech_Rechiv ELSE NULL END R76,
                    CASE kod_rechiv WHEN 77 THEN Erech_Rechiv ELSE NULL END R77,
                    CASE kod_rechiv WHEN 78 THEN Erech_Rechiv ELSE NULL END R78,
                    CASE kod_rechiv WHEN 96 THEN Erech_Rechiv ELSE NULL END R96,
                    CASE kod_rechiv WHEN 108 THEN Erech_Rechiv ELSE NULL END R108,
                    CASE kod_rechiv WHEN 126 THEN Erech_Rechiv ELSE NULL END R126,
                    CASE kod_rechiv WHEN 131 THEN Erech_Rechiv ELSE NULL END R131,
                    
                    CASE kod_rechiv WHEN 219 THEN Erech_Rechiv ELSE NULL END R219
                    FROM TB_CHISHUV_YOMI_OVDIM Ch
                    WHERE
          --          CH.TAARICH between to_date('01/07/2012','dd/MM/yyyy') and to_date('30/07/2012','dd/MM/yyyy') 
                    Ch.Taarich BETWEEN p_FromDate  AND p_ToDate 
                    --AND Ch.Bakasha_ID = 8184
                    AND Ch.Bakasha_ID = p_BakashaId
                    ) cco
 group by cco.MISPAR_ISHI,cco.TAARICH
) co
where 
        co.TAARICH = so.TAARICH and 
        Po.mispar_ishi = o.mispar_ishi   and 
        Po.mispar_ishi = So.mispar_ishi   and 
        Po.mispar_ishi = RelevantDetails.mispar_ishi and  
        Po.ME_TARICH = RelevantDetails.me_taarich and 
        CS.EZOR = PO.EZOR and 
        CS.KOD_HEVRA = O.KOD_HEVRA and
        CS.KOD_HEVRA = 4895 AND 
        CS.KOD_SNIF_AV = PO.SNIF_AV and 
        PO.MISPAR_ISHI = O.MISPAR_ISHI and 
        CO.MISPAR_ISHI = PO.MISPAR_ISHI and 
        ISUK.KOD_HEVRA = CS.KOD_HEVRA and 
        ISUK.KOD_ISUK = PO.ISUK ;*/

end get_GetDayDataEggT ;

procedure pro_ProfilLinesDetails( P_STARTDATE IN DATE,
                                        P_ENDDATE IN DATE , 
                                        P_Makat IN varchar2,
                                        p_cur OUT CurType)   as 
    GeneralQry VARCHAR2(30000);    
 --   listLikeMakats  VARCHAR2(1000);       
BEGIN
DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_ProfilLinesDetails');
                    
GeneralQry:='Select   distinct p.makat_nesia,p.TAARICH 
                     FROM TB_PEILUT_OVDIM p
                     where  P.TAARICH between   ''' || P_STARTDATE || ''' AND  ''' || P_ENDDATE || '''  ';
           

     DBMS_OUTPUT.PUT_LINE(GeneralQry );     
Pkg_Reports.pro_Prepare_Catalog_Details(GeneralQry);

                    
GeneralQry := '
                select makat_nesia ,taarich,description,MAZAN_TICHNUN,KISUY_TOR,km,SNIF,count(makat_nesia) SumOfMakat,
                        sum(EMP_BEF_SHERUT) EMP_BEF_SHERUT, 
                        sum(EMP_BEF_NAMAK) EMP_BEF_NAMAK, 
                        sum(EMP_BEF_EMP) EMP_BEF_EMP, 
                        sum(EMP_BEF_OTHER) EMP_BEF_OTHER,
                        sum(EMP_AFT_SHERUT) EMP_AFT_SHERUT, 
                        sum(EMP_AFT_NAMAK) EMP_AFT_NAMAK, 
                        sum(EMP_AFT_EMP) EMP_AFT_EMP, 
                        sum(EMP_AFT_OTHER) EMP_AFT_OTHER,
                        sum(T_EMP_BEF_SHERUT) T_EMP_BEF_SHERUT, 
                        sum(T_EMP_BEF_NAMAK) T_EMP_BEF_NAMAK, 
                        sum(T_EMP_BEF_EMP) T_EMP_BEF_EMP, 
                        sum(T_EMP_BEF_OTHER) T_EMP_BEF_OTHER,
                        sum(T_EMP_AFT_SHERUT) T_EMP_AFT_SHERUT, 
                        sum(T_EMP_AFT_NAMAK) T_EMP_AFT_NAMAK, 
                        sum(T_EMP_AFT_EMP) T_EMP_AFT_EMP, 
                        sum(T_EMP_AFT_OTHER) T_EMP_AFT_OTHER,
                        sum(KM_EMP_BEF_SHERUT) KM_EMP_BEF_SHERUT, 
                        sum(KM_EMP_BEF_NAMAK) KM_EMP_BEF_NAMAK, 
                        sum(KM_EMP_BEF_EMP) KM_EMP_BEF_EMP, 
                        sum(KM_EMP_BEF_OTHER) KM_EMP_BEF_OTHER,
                        sum(KM_EMP_AFT_SHERUT) KM_EMP_AFT_SHERUT, 
                        sum(KM_EMP_AFT_NAMAK) KM_EMP_AFT_NAMAK, 
                        sum(KM_EMP_AFT_EMP) KM_EMP_AFT_EMP, 
                        sum(KM_EMP_AFT_OTHER) KM_EMP_AFT_OTHER
                FROM    (
                      SELECT makat_nesia , taarich,
                            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 1 THEN 1 ELSE 0 END EMP_BEF_SHERUT, 
                            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 3 THEN 1 ELSE 0 END EMP_BEF_NAMAK, 
                            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 2 THEN 1 ELSE 0 END EMP_BEF_EMP, 
                            CASE WHEN  PREV_SHERUT =2 AND NVL(PREV_SHERUT2,0) NOT IN (1,2,3) THEN 1 ELSE 0 END EMP_BEF_OTHER,
                            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 1 THEN 1 ELSE 0 END EMP_AFT_SHERUT, 
                            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 3 THEN 1 ELSE 0 END EMP_AFT_NAMAK, 
                            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 2 THEN 1 ELSE 0 END EMP_AFT_EMP, 
                            CASE WHEN  NEXT_SHERUT =2 AND NVL(NEXT_SHERUT2,0) NOT IN (1,2,3) THEN 1 ELSE 0 END EMP_AFT_OTHER,
                            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 1 THEN PREV_TIME ELSE 0 END T_EMP_BEF_SHERUT, 
                            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 3 THEN PREV_TIME ELSE 0 END T_EMP_BEF_NAMAK, 
                            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 2 THEN PREV_TIME ELSE 0 END T_EMP_BEF_EMP, 
                            CASE WHEN  PREV_SHERUT =2 AND NVL(PREV_SHERUT2,0) NOT IN (1,2,3) THEN PREV_TIME ELSE 0 END T_EMP_BEF_OTHER,
                            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 1 THEN NEXT_TIME ELSE 0 END T_EMP_AFT_SHERUT, 
                            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 3 THEN NEXT_TIME ELSE 0 END T_EMP_AFT_NAMAK, 
                            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 2 THEN NEXT_TIME ELSE 0 END T_EMP_AFT_EMP, 
                            CASE WHEN  NEXT_SHERUT =2 AND NVL(NEXT_SHERUT2,0)  NOT IN (1,2,3) THEN NEXT_TIME ELSE 0 END T_EMP_AFT_OTHER,
                            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 1 THEN PREV_KM ELSE 0 END KM_EMP_BEF_SHERUT, 
                            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 3 THEN PREV_KM ELSE 0 END KM_EMP_BEF_NAMAK, 
                            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 2 THEN PREV_KM ELSE 0 END KM_EMP_BEF_EMP, 
                            CASE WHEN  PREV_SHERUT =2 AND NVL(PREV_SHERUT2,0) NOT IN (1,2,3) THEN PREV_KM ELSE 0 END KM_EMP_BEF_OTHER,
                            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 1 THEN NEXT_TIME ELSE 0 END KM_EMP_AFT_SHERUT, 
                            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 3 THEN NEXT_TIME ELSE 0 END KM_EMP_AFT_NAMAK, 
                            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 2 THEN NEXT_TIME ELSE 0 END KM_EMP_AFT_EMP, 
                            CASE WHEN  NEXT_SHERUT =2 AND NVL(NEXT_SHERUT2,0)  NOT IN (1,2,3) THEN NEXT_TIME ELSE 0 END KM_EMP_AFT_OTHER,
                             first_value(description) over(partition by makat_nesia ) description,
                             first_value(MAZAN_TICHNUN) over(partition by makat_nesia ) MAZAN_TICHNUN,
                            first_value(KISUY_TOR) over(partition by makat_nesia ) KISUY_TOR,
                             first_value(km) over(partition by makat_nesia ) km,
                             first_value(SNIF) over(partition by makat_nesia ) SNIF
                     FROM ( SELECT mispar_ishi,makat_nesia,taarich, Sherut_nb,
                                            LAG (Sherut_nb ,1 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) PREV_SHERUT,
                                            LAG (Sherut_nb ,2 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) PREV_SHERUT2,
                                            LEAD (Sherut_nb ,1 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) NEXT_SHERUT,
                                            LEAD (Sherut_nb ,2 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) NEXT_SHERUT2,
                                            LAG (MAZAN_TICHNUN ,1 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) PREV_TIME,
                                            LEAD (MAZAN_TICHNUN ,1 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) NEXT_TIME,
                                            LAG (KM ,1 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) PREV_KM,
                                            LEAD (KM ,1 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) NEXT_KM,
                                            km , KISUY_TOR , SNIF , MAZAN_TICHNUN, description, SHAT_YETZIA 
                                            FROM (  
                                               select P.mispar_ishi,P.taarich, P.makat_nesia,P.MISPAR_sidur, P.SHAT_YETZIA,p.SNIF_TNUA SNIF , 
                                                        c.km ,  c.KISUY_TOR ,c.MAZAN_TICHNUN , c.description  , Pkg_Reports.fn_get_makat_type(p.makat_nesia) sherut_nb   
                                                from TB_PEILUT_OVDIM P,TMP_CATALOG c, 
                                                       (select  distinct  P.mispar_ishi,P.taarich,P.MISPAR_sidur, P.SHAT_HATCHALA_SIDUR
                                                        FROM   TB_PEILUT_OVDIM P
                                                         WHERE   P.TAARICH between   ''' || P_STARTDATE || ''' AND  ''' || P_ENDDATE || '''  
                                                               and nvl( P.MISPAR_KNISA,0) =0
                                                               and p.makat_nesia>0
                                                               and (Pkg_Tnua.fn_get_makat_type(p.makat_nesia) in(1,3) or
                                                               (substr( LPAD(p.makat_nesia,8, ''0''),0,1) =7 and  
                                                                            substr(LPAD(p.makat_nesia,8, ''0'') ,2,2) in (select LPAD(KOD_ELEMENT,2, ''0'')     from
                                                                                                                                         TB_MEAFYENEY_ELEMENTIM where KOD_MEAFYEN = 4 AND ERECH in(''1'')) ) )';
                                      
                     IF (( P_Makat IS NOT  NULL ) OR ( P_Makat <> '' )) THEN 
                        GeneralQry := GeneralQry || ' AND ' || PREPARE_LIKE_OF_MKAT8('p.makat_nesia', P_Makat,'%') ;
                    END IF ; 
                                                 
                        GeneralQry := GeneralQry ||    ' )po 
                    where  P.mispar_ishi = po.mispar_ishi
                    and P.taarich = po.taarich     
                    and P.MISPAR_sidur=po.MISPAR_sidur
                    and P.shat_hatchala_sidur=po.shat_hatchala_sidur
                    and nvl( P.MISPAR_KNISA,0) =0
                    and p.makat_nesia>0
                    and ( substr( LPAD(p.makat_nesia,8, ''0''),0,1) <>7 or ( substr( LPAD(p.makat_nesia,8, ''0''),0,1) =7 and  
                                                                                                         substr(LPAD(p.makat_nesia,8, ''0'') ,2,2) in (select LPAD(KOD_ELEMENT,2, ''0'')     from
                                                                                                                                                                       TB_MEAFYENEY_ELEMENTIM where KOD_MEAFYEN = 4 AND ERECH IN( ''1'')) ))
                    and  c.ACTIVITY_DATE  (+) = p.taarich
                    and c.MAKAT8  (+) = p.MAKAT_NESIA
              order by  P.mispar_ishi,P.taarich,   P.SHAT_YETZIA 
              
              )
               order by  mispar_ishi,taarich,   SHAT_YETZIA 
               )
                where Sherut_nb<>2
             )';
             
            IF (( P_Makat IS NOT  NULL ) OR ( P_Makat <> '' )) THEN 
                    GeneralQry := GeneralQry || ' WHERE  ' || PREPARE_LIKE_OF_MKAT8('makat_nesia', P_Makat,'%') ;
            END IF ; 
      
     GeneralQry := GeneralQry ||  'GROUP BY makat_nesia ,taarich,description,MAZAN_TICHNUN,KISUY_TOR,km,SNIF';
 
       
DBMS_OUTPUT.PUT_LINE(GeneralQry );
        
OPEN p_cur   FOR GeneralQry;


end pro_ProfilLinesDetails;

function fn_get_makat_type(makat_nesia number)  return number as
  makat_type number;
  kod_element number;
begin
    makat_type:= Pkg_Tnua.fn_get_makat_type(makat_nesia);
    kod_element:=0;
    if (makat_type=5) then
       BEGIN
           select kod_element into kod_element
           from tb_meafyeney_elementim
           where kod_meafyen =23
              and kod_element = substr( LPAD(makat_nesia,8, '0'),2,2);
            if (   kod_element >0) then
                makat_type:=2;
            end if;
         EXCEPTION
         WHEN NO_DATA_FOUND  THEN
             return makat_type;
         END;         
    end if;
    return makat_type;
end fn_get_makat_type;            
procedure pro_ProfilLinesSummed( P_STARTDATE IN DATE,
                                        P_ENDDATE IN DATE , 
                                        P_Makat IN varchar2,
                                        p_cur OUT CurType)   as 
    GeneralQry VARCHAR2(30000);             
BEGIN
DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_ProfilLinesSummed');
GeneralQry:='Select   distinct p.makat_nesia,p.TAARICH 
                     FROM TB_PEILUT_OVDIM p
                     where  P.TAARICH between   ''' || P_STARTDATE || ''' AND  ''' || P_ENDDATE || '''  ';
   
DBMS_OUTPUT.PUT_LINE(GeneralQry );
Pkg_Reports.pro_Prepare_Catalog_Details(GeneralQry);

GeneralQry := 'select makat_nesia ,SNIF_TNUA Snif_Bizua,description,MAZAN_TICHNUN,KISUY_TOR,to_number(TO_CHAR(km,''9999999.99'')) km,SNIF,count(makat_nesia) SumOfMakat,
                sum(EMP_BEF_SHERUT) EMP_BEF_SHERUT, 
                sum(EMP_BEF_NAMAK) EMP_BEF_NAMAK, 
                sum(EMP_BEF_EMP) EMP_BEF_EMP, 
                 sum(EMP_BEF_OTHER) EMP_BEF_OTHER,
                sum(EMP_AFT_SHERUT) EMP_AFT_SHERUT, 
                sum(EMP_AFT_NAMAK) EMP_AFT_NAMAK, 
                sum(EMP_AFT_EMP) EMP_AFT_EMP, 
                sum(EMP_AFT_OTHER) EMP_AFT_OTHER,
                sum(T_EMP_BEF_SHERUT)T_EMP_BEF_SHERUT, 
                sum(T_EMP_BEF_NAMAK) T_EMP_BEF_NAMAK, 
                sum(T_EMP_BEF_EMP) T_EMP_BEF_EMP, 
                sum(T_EMP_BEF_OTHER) T_EMP_BEF_OTHER,
                sum(T_EMP_AFT_SHERUT) T_EMP_AFT_SHERUT, 
                sum(T_EMP_AFT_NAMAK) T_EMP_AFT_NAMAK, 
                sum(T_EMP_AFT_EMP) T_EMP_AFT_EMP, 
                sum(T_EMP_AFT_OTHER) T_EMP_AFT_OTHER,
                to_number(TO_CHAR(sum(KM_EMP_BEF_SHERUT),''9999999.99'')) KM_EMP_BEF_SHERUT, 
                to_number(TO_CHAR(sum(KM_EMP_BEF_NAMAK),''9999999.99'')) KM_EMP_BEF_NAMAK, 
                to_number(TO_CHAR(sum(KM_EMP_BEF_EMP),''9999999.99'')) KM_EMP_BEF_EMP, 
                to_number(TO_CHAR(sum(KM_EMP_BEF_OTHER),''9999999.99'')) KM_EMP_BEF_OTHER,
                to_number(TO_CHAR(sum(KM_EMP_AFT_SHERUT),''9999999.99'')) KM_EMP_AFT_SHERUT, 
               to_number(TO_CHAR( sum(KM_EMP_AFT_NAMAK),''9999999.99'')) KM_EMP_AFT_NAMAK, 
                to_number(TO_CHAR(sum(KM_EMP_AFT_EMP),''9999999.99'')) KM_EMP_AFT_EMP, 
                to_number(TO_CHAR(sum(KM_EMP_AFT_OTHER),''99999999.99'')) KM_EMP_AFT_OTHER
               
        FROM 
        (
             SELECT makat_nesia , taarich,SNIF_TNUA,
                            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 1 THEN 1 ELSE 0 END EMP_BEF_SHERUT, 
                            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 3 THEN 1 ELSE 0 END EMP_BEF_NAMAK, 
                            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 2 THEN 1 ELSE 0 END EMP_BEF_EMP, 
                            CASE WHEN  PREV_SHERUT =2 AND NVL(PREV_SHERUT2,0) NOT IN (1,2,3) THEN 1 ELSE 0 END EMP_BEF_OTHER,
                            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 1 THEN 1 ELSE 0 END EMP_AFT_SHERUT, 
                            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 3 THEN 1 ELSE 0 END EMP_AFT_NAMAK, 
                            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 2 THEN 1 ELSE 0 END EMP_AFT_EMP, 
                            CASE WHEN  NEXT_SHERUT =2 AND NVL(NEXT_SHERUT2,0) NOT IN (1,2,3) THEN 1 ELSE 0 END EMP_AFT_OTHER,
                            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 1 THEN PREV_TIME ELSE 0 END T_EMP_BEF_SHERUT, 
                            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 3 THEN PREV_TIME ELSE 0 END T_EMP_BEF_NAMAK, 
                            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 2 THEN PREV_TIME ELSE 0 END T_EMP_BEF_EMP, 
                            CASE WHEN  PREV_SHERUT =2 AND NVL(PREV_SHERUT2,0) NOT IN (1,2,3) THEN PREV_TIME ELSE 0 END T_EMP_BEF_OTHER,
                            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 1 THEN NEXT_TIME ELSE 0 END T_EMP_AFT_SHERUT, 
                            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 3 THEN NEXT_TIME ELSE 0 END T_EMP_AFT_NAMAK, 
                            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 2 THEN NEXT_TIME ELSE 0 END T_EMP_AFT_EMP, 
                            CASE WHEN  NEXT_SHERUT =2 AND NVL(NEXT_SHERUT2,0)  NOT IN (1,2,3) THEN NEXT_TIME ELSE 0 END T_EMP_AFT_OTHER,
                            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 1 THEN PREV_KM 
                            ELSE 0 END KM_EMP_BEF_SHERUT, 
                            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 3 THEN PREV_KM ELSE 0 END KM_EMP_BEF_NAMAK, 
                            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 2 THEN PREV_KM ELSE 0 END KM_EMP_BEF_EMP, 
                            CASE WHEN  PREV_SHERUT =2 AND NVL(PREV_SHERUT2,0) NOT IN (1,2,3) THEN PREV_KM ELSE 0 END KM_EMP_BEF_OTHER,
                            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 1 THEN NEXT_TIME ELSE 0 END KM_EMP_AFT_SHERUT, 
                            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 3 THEN NEXT_TIME ELSE 0 END KM_EMP_AFT_NAMAK, 
                            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 2 THEN NEXT_TIME ELSE 0 END KM_EMP_AFT_EMP, 
                            CASE WHEN  NEXT_SHERUT =2 AND NVL(NEXT_SHERUT2,0)  NOT IN (1,2,3) THEN NEXT_TIME ELSE 0 END KM_EMP_AFT_OTHER,
                             first_value(description) over(partition by makat_nesia ) description,
                             first_value(MAZAN_TICHNUN) over(partition by makat_nesia ) MAZAN_TICHNUN,
                            first_value(KISUY_TOR) over(partition by makat_nesia ) KISUY_TOR,
                             first_value(km) over(partition by makat_nesia ) km,
                             first_value(SNIF) over(partition by makat_nesia ) SNIF
                             
            FROM 
                (
                  SELECT mispar_ishi,makat_nesia,taarich, Sherut_nb,
                                            LAG (Sherut_nb ,1 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) PREV_SHERUT,
                                            LAG (Sherut_nb ,2 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) PREV_SHERUT2,
                                            LEAD (Sherut_nb ,1 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) NEXT_SHERUT,
                                            LEAD (Sherut_nb ,2 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) NEXT_SHERUT2,
                                            LAG (MAZAN_TICHNUN ,1 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) PREV_TIME,
                                            LEAD (MAZAN_TICHNUN ,1 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) NEXT_TIME,
                                            LAG (KM ,1 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) PREV_KM,
                                            LEAD (KM ,1 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) NEXT_KM,
                                            km , KISUY_TOR , SNIF , MAZAN_TICHNUN, description, SHAT_YETZIA , 
                                            NVL(SNIF_TNUA,PKG_SIDURIM.GETSNIFTNUABYSIDUR(mispar_ishi, taarich,MISPAR_sidur,SHAT_HATCHALA_SIDUR  )) SNIF_TNUA
                                            FROM (  
                                               select P.mispar_ishi,P.taarich, P.makat_nesia,P.MISPAR_sidur, P.SHAT_YETZIA,P.SNIF_TNUA, c.SNIF , P.SHAT_HATCHALA_SIDUR,
                                                        c.km ,  c.KISUY_TOR ,c.MAZAN_TICHNUN , c.description  , Pkg_Reports.fn_get_makat_type(p.makat_nesia) sherut_nb   
                                                from TB_PEILUT_OVDIM P,TMP_CATALOG c, 
                                                       (select  distinct  P.mispar_ishi,P.taarich,P.MISPAR_sidur, P.SHAT_HATCHALA_SIDUR
                                                        FROM   TB_PEILUT_OVDIM P
                                                         WHERE   P.TAARICH between  ''' || P_STARTDATE || ''' AND  ''' || P_ENDDATE || '''  
                                                               and nvl( P.MISPAR_KNISA,0) =0
                                                               and p.makat_nesia>0
                                                               and (Pkg_Tnua.fn_get_makat_type(p.makat_nesia) in(1,3) or
                                                               (substr( LPAD(p.makat_nesia,8, ''0''),0,1) =7 and  
                                                                            substr(LPAD(p.makat_nesia,8, ''0'') ,2,2) in (select LPAD(KOD_ELEMENT,2, ''0'')     from
                                                                                                                                         TB_MEAFYENEY_ELEMENTIM where KOD_MEAFYEN = 4 AND ERECH in(''1'')) ) )';
                                      
                     IF (( P_Makat IS NOT  NULL ) OR ( P_Makat <> '' )) THEN 
                        GeneralQry := GeneralQry || ' AND ' || PREPARE_LIKE_OF_MKAT8('p.makat_nesia', P_Makat,'%') ;
                    END IF ; 
                                                 
                        GeneralQry := GeneralQry ||    ' )po 
                                where  P.mispar_ishi = po.mispar_ishi
                                and P.taarich = po.taarich     
                                and P.MISPAR_sidur=po.MISPAR_sidur
                                and P.shat_hatchala_sidur=po.shat_hatchala_sidur
                                and nvl( P.MISPAR_KNISA,0) =0
                               and p.makat_nesia>0
                               and ( substr( LPAD(p.makat_nesia,8, ''0''),0,1) <>7 or ( substr( LPAD(p.makat_nesia,8, ''0''),0,1) =7 and  
                                                                                                         substr(LPAD(p.makat_nesia,8, ''0'') ,2,2) in (select LPAD(KOD_ELEMENT,2, ''0'')     from
                                                                                                                                                                       TB_MEAFYENEY_ELEMENTIM where KOD_MEAFYEN = 4 AND ERECH IN( ''1'')) ))
                               and  c.ACTIVITY_DATE  (+) = p.taarich
                                and c.MAKAT8  (+) = p.MAKAT_NESIA
                          order by  P.mispar_ishi,P.taarich,   P.SHAT_YETZIA 
                          
                          )
                           order by  mispar_ishi,taarich,   SHAT_YETZIA 
                           )
                             where Sherut_nb<>2
             )';
             
            IF (( P_Makat IS NOT  NULL ) OR ( P_Makat <> '' )) THEN 
                    GeneralQry := GeneralQry || ' WHERE  ' || PREPARE_LIKE_OF_MKAT8('makat_nesia', P_Makat,'%') ;
            END IF ; 
      
           GeneralQry := GeneralQry ||  '   GROUP BY makat_nesia ,SNIF_TNUA,description,MAZAN_TICHNUN,KISUY_TOR,km,SNIF 
                                                          order by makat_nesia ,SNIF_TNUA';

DBMS_OUTPUT.PUT_LINE(GeneralQry );
        
OPEN p_Cur   FOR GeneralQry;

end pro_ProfilLinesSummed;



 

PROCEDURE pro_get_Bakashot_details(p_Cur OUT CurType)
  IS
     BEGIN
     OPEN p_Cur FOR
             select  x.bakasha_id, (   x.bakasha_id  || ' - ' ||
                                    ( case when x.maamad='1' then '�����' else case when  x.maamad='2' then '������' else '����� �������' end end) || ' - ' ||   
                                    x.chodesh
                            ) teur
            from(
                    select b.bakasha_id,
                            (select  p.erech from tb_bakashot_params p where p.PARAM_ID=1 and b.bakasha_id= p.bakasha_id ) maamad,
                             (select  p.erech from tb_bakashot_params p where p.PARAM_ID=2 and b.bakasha_id= p.bakasha_id) chodesh
                    from tb_bakashot b 
                    where sug_bakasha=1
                         and HUAVRA_LESACHAR=1
        ) x
        order by   x.bakasha_id desc;

     EXCEPTION
        WHEN OTHERS THEN
        RAISE;
  END pro_get_Bakashot_details;
           
    procedure pro_ovdey_meshek_shishi_shabat(P_STARTDATE IN DATE,
                                                                        P_ENDDATE IN DATE , 
                                                                        P_Ezor IN varchar2,
                                                                        p_cur OUT CurType)   as 
  
  begin
DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_ovdey_meshek_shishi_shabat');
  open p_cur for
with p as
    (  select p.mispar_ishi,p.me_tarich,p.ad_tarich,P.SNIF_AV,P.EZOR,P.YECHIDA_IRGUNIT,P.ISUK
        from pivot_pirtey_ovdim p
        where p.isuk IS NOT NULL
             AND (P_STARTDATE BETWEEN  p.ME_TARICH  AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
               OR P_ENDDATE BETWEEN  p.ME_TARICH  AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                OR  ( p.ME_TARICH>=P_STARTDATE AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=P_ENDDATE))
                     and ( substr(P.ISUK ,0,1) in (6,7)) 
                         and length (trim(P.ISUK))=3
           --          and P.SNIF_AV in (82594 ,85407 ,88898 )
                     ),
   shabaton as 
   (select y.taarich 
    from TB_YAMIM_MEYUCHADIM y, CTB_SUGEY_YAMIM_MEYUCHADIM s
    where Y.SUG_YOM=     S.SUG_YOM
      and S.Shbaton=1
      and s.pail=1  )  
      
select S.TAARICH, s.mispar_ishi, O.SHEM_MISH || ' ' || O.SHEM_PRAT shem, SNIF.TEUR_SNIF_AV, I.TEUR_ISUK,
        (Pkg_Ovdim.fun_get_meafyen_oved(s.mispar_ishi, 3, P_ENDDATE )) START_TIME_ALLOWED ,
        (Pkg_Ovdim.fun_get_meafyen_oved(s.mispar_ishi, 4, P_ENDDATE  )) END_TIME_ALLOWED  ,   
           TO_NUMBER( TO_CHAR(  (S.SHAT_GMAR_LETASHLUM - S.SHAT_HATCHALA_LETASHLUM)*1440,'99999.99') ) dakot_nocechut,
         S.MISPAR_SIDUR,M.TEUR_SIDUR_MEYCHAD,
          to_char(S.SHAT_HATCHALA ,'HH24:mi')  SHAT_HATCHALA,
          to_char(S.SHAT_GMAR ,'HH24:mi')  SHAT_GMAR,
         to_char(S.SHAT_HATCHALA_LETASHLUM_musach ,'HH24:mi') SHAT_HATCHALA_LETASHLUM,
          to_char(S.SHAT_GMAR_LETASHLUM_musach ,'HH24:mi') SHAT_GMAR_LETASHLUM,
         S.CHARIGA,S.LO_LETASHLUM
--snif.teur_snif_av ,  S.SHAT_HATCHALA,S.SHAT_GMAR,s.SHAT_HATCHALA_LETASHLUM_MUSACH,s.SHAT_GMAR_LETASHLUM_MUSACH,  s.taarich,  S.MISPAR_SIDUR ,E.TEUR_EZOR
from ovdim o,tb_sidurim_ovdim s,p,CTB_SNIF_AV Snif,ctb_ezor e,ctb_isuk i,CTB_SIDURIM_MEYUCHADIM m

where o.mispar_ishi=s.mispar_ishi
  and  s.taarich between P_STARTDATE and P_ENDDATE
  and S.MISPAR_SIDUR=99001
  and O.KOD_HEVRA=580
  and  ( TO_CHAR(TRUNC(s.taarich),'d') in('6','7') 
            or exists (select * from shabaton sh where sh.taarich = s.taarich)
            )
  and S.MISPAR_ISHI = P.MISPAR_ISHI
  and S.TAARICH between P.ME_TARICH and P.AD_TARICH
  AND p.snif_av       = Snif.kod_snif_av 
  AND Snif.kod_hevra = o.kod_hevra
  AND p.ezor       =  E.KOD_EZOR
   AND ( P_Ezor is null or p.EZOR in    (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_EZOR,  ',') AS MYTABTYPE))))
  AND e.kod_hevra = o.kod_hevra
  --and Y.KOD_YECHIDA= p.YECHIDA_IRGUNIT
  --and Y.KOD_HEVRA= o.kod_hevra
  and I.KOD_ISUK = p.ISUK
  and i.KOD_HEVRA= o.kod_hevra 
  and S.MISPAR_SIDUR= m.KOD_SIDUR_MEYUCHAD
  order by SNIF.TEUR_SNIF_AV, s.taarich, s.mispar_ishi, S.SHAT_HATCHALA;

 end pro_ovdey_meshek_shishi_shabat;        
 
function get_heara_doch_nochechut(mispar_ishi in number,
                                                    taarich in date,
                                                    mispar_sidur in number,
                                                    lo_letashlum in number,
                                                    shat_hatchala in date,
                                                    shat_gmar in date,
                                                    kod_headrut in number,
                                                    cnt_sidurim_beyom number,
                                                    cnt_others_sidurim number,
                                                    is_headrut in number) return nvarchar2 as
       p_heara   VARCHAR2(50);       
       p_heara_ezer   VARCHAR2(50); 
        p_hearat_chufsha   VARCHAR2(50);      
       t_yom         VARCHAR2(2);
begin
     p_heara:='';
     p_hearat_chufsha:='';
     if(kod_headrut=1 )  then
       p_hearat_chufsha:='����� ������ ';
     end if;
         
    select   Dayofweek(taarich) into t_yom from dual;
        DBMS_OUTPUT.PUT_LINE(taarich);
     if (cnt_sidurim_beyom=0 and (t_yom not in ('�','�','�','�') )) then
         if (kod_headrut=1 ) then
            return p_hearat_chufsha;
        end if;
        return '��� �����';
     end if;

    if( mispar_sidur is not null and (shat_hatchala is null or shat_hatchala=to_date('01/01/0001','dd/mm/yyyy') or shat_gmar  is null)  and mispar_sidur<>99200) then
      p_heara_ezer:= '��� �����';
    end if;
 

   if( mispar_sidur is not null and kod_headrut=1 and is_headrut=0) then
       p_heara_ezer:= '�.�';
     end if;
     
     if(is_headrut>0 and kod_headrut=1 ) then
       p_heara_ezer:=  '����� �������';
     end if;
  
    if (p_heara_ezer is null and cnt_others_sidurim >0 and cnt_sidurim_beyom=cnt_others_sidurim) then 
       p_heara_ezer:=   '�.� ���';
     end if;
   
    if (p_hearat_chufsha is null and lo_letashlum =1) then
        p_heara_ezer:='�� ������';
    end if;
    
   if(kod_headrut=1 and  p_heara_ezer is not null)  then
        p_heara:=  p_hearat_chufsha || '+' || p_heara_ezer;
    else 
          p_heara:= p_heara_ezer;
   end if;
      
   if (p_heara is null and kod_headrut=1 ) then
        return p_hearat_chufsha;
   end if;

    return p_heara;
end  get_heara_doch_nochechut;              

   procedure pro_get_mushalim_details(P_STARTDATE IN DATE,
                                                         P_ENDDATE IN DATE , 
                                                         P_COMPANYID IN NUMBER,
                                                       p_cur OUT CurType)   as 
  --P_STARTDATE date;
 -- P_ENDDATE date;
  begin
  DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_get_mushalim_details');
--P_STARTDATE:=sysdate;
--P_ENDDATE:=sysdate;
  open p_cur for
with p as
    (  select p.mispar_ishi,p.me_tarich,p.ad_tarich,P.SNIF_AV,P.EZOR,P.DIRUG,P.MAAMAD,P.GIL,P.ISUK, O.KOD_HEVRA
        from pivot_pirtey_ovdim p,ctb_snif_av s, ovdim o
        where  
                 (P_STARTDATE BETWEEN  p.ME_TARICH  AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                  OR P_ENDDATE BETWEEN  p.ME_TARICH  AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                  OR (  p.ME_TARICH>=P_STARTDATE  AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=P_ENDDATE))
             and  p.mispar_ishi=  o.mispar_ishi
             and o.kod_hevra=580
             and p.SNIF_AV=S.KOD_SNIF_AV   
             and ((s.KOD_HEVRA=4895 and P_COMPANYID=4895) or ( P_COMPANYID=6486  and( s.KOD_HEVRA=6486 or  p.SNIF_AV=41509 ) ))
                     
                  --    and  p.mispar_ishi=49747
                     )
, bakasha as
   (
        SELECT    a.mispar_ishi, a.Taarich, a.bakasha_id 
          FROM P,
           ( SELECT  co.mispar_ishi, co.Taarich, co.bakasha_id,b.TAARICH_HAAVARA_LESACHAR th1,
                                  MAX(B.TAARICH_HAAVARA_LESACHAR) OVER (PARTITION BY co.mispar_ishi,co.Taarich )  th2
                     FROM TB_CHISHUV_CHODESH_OVDIM co,TB_BAKASHOT b,p
                     WHERE   B.BAKASHA_ID = CO.BAKASHA_ID
                            and b.Huavra_Lesachar=1
                               and CO.TAARICH between P_STARTDATE and P_ENDDATE
                               and  co.mispar_ishi=p.mispar_ishi
                               and (CO.TAARICH BETWEEN  P.ME_TARICH  AND   NVL(P.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                                                   OR LAST_DAY(CO.TAARICH) BETWEEN  P.ME_TARICH  AND   NVL(P.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                                                    OR (  P.ME_TARICH>=CO.TAARICH  AND   NVL(P.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=LAST_DAY(CO.TAARICH)))
                     ) a
        WHERE a.th1=a.th2 
        AND a.mispar_ishi=P.mispar_ishi
    GROUP BY  a.mispar_ishi, a.Taarich, a.bakasha_id
      ),
 snif_egd_tavura as    
    (select snif_tnua
     from ctb_snif_av
     where  kod_hevra=4895
     and snif_tnua is not null)
,zman_egged_tavura as
 (
    select c.mispar_ishi, C.BAKASHA_ID,trunc(c.taarich,'MM') taarich,
            sum( case when  ((c.snif is not null )or  (mispar_sidur in(99901,99902,99903, 99904 ))  )then erech_rechiv else 0 end ) time_egTav,
              sum( case when  mispar_sidur in(99905,99906,99907  ) then erech_rechiv else 0 end ) time_egHeseim
    from  
    (select c.*,b.snif_tnua snif
    from (  select c.mispar_ishi, C.BAKASHA_ID,  c. mispar_sidur,  c.taarich,erech_rechiv,
                  (CASE
                            WHEN (SUBSTR (c.mispar_sidur, 0, 2) <> '99' AND c.mispar_sidur > 999)
                            THEN
                               TO_NUMBER (SUBSTR (LPAD (c.mispar_sidur, 5), 0, 2))
                            WHEN SUBSTR (c.mispar_sidur, 0, 2) = '99'
                            THEN
                               (SELECT v.snif_tnua
                                  FROM VIW_SNIF_TNUA_FROM_TNUA v
                                 WHERE     v.mispar_ishi = c.mispar_ishi
                                       AND v.taarich = c.taarich
                                       AND v.mispar_sidur = c.mispar_sidur
                                       AND v.SHAT_HATCHALA = c.SHAT_HATCHALA)
                            ELSE
                               NULL
                         END) snif_tnua
        from tb_chishuv_sidur_ovdim c, bakasha b
        where  c.mispar_ishi = b.mispar_ishi
             and trunc(c.taarich,'MM') =b.taarich
             and c.bakasha_id= b.bakasha_id
             and C.KOD_RECHIV=18 ) c,snif_egd_tavura  b
        where c.snif_tnua=b.snif_tnua(+)  ) c
        group by  c.mispar_ishi, C.BAKASHA_ID,trunc(c.taarich,'MM') 
 )  
      

select h.mispar_ishi,ov.SHEM_MISH ||  ' ' || OV.SHEM_PRAT shem, m.TEUR_MAAMAD_HR maamad,G.TEUR_KOD_GIL gil, 
         e.kod_ezor,E.TEUR_EZOR, S.TEUR_SNIF_AV,I.KOD_ISUK,  I.TEUR_ISUK,  cH.TEUR_HEVRA,
          to_char( h.taarich,'mm/yyyy') taarich,
         h.rechiv_109 yemey_nochechut,
         case when  P_COMPANYID=4895 then round((h.time_egTav/60),2) else round((h.time_egHeseim/60),2) end zman_hevrat_bat,
         round((h.rechiv_4 /60),2)  shaot_regilot_tafkid,
         round((h.rechiv_2 /60),2) shaot_regilot_nahagut,
         round((h.rechiv_3 /60),2)  shaot_regilot_nihul_tnua,
         h.rechiv_96 zman_rezifut,
         h.rechiv_49 pizul, h.rechiv_75 yemey_nochechut_7
from ovdim ov,
        ctb_maamad m,
        ctb_kod_gil g,
        ctb_isuk i,
        ctb_snif_av s ,
        ctb_ezor e,
        ctb_hevra ch,
     (
 select z2.*, P.SNIF_AV,P.EZOR,P.DIRUG,P.MAAMAD,P.GIL,P.ISUK, egt.time_egTav,egt.time_egHeseim--,egt.rechiv_1s
 from p,zman_egged_tavura egt,
     ( select  z.mispar_ishi,z.taarich, z.bakasha_id,
               sum(  case when z.kod_rechiv=2 then z.erech_rechiv else 0 end) rechiv_2 ,
               sum(  case  when z.kod_rechiv=3 then z.erech_rechiv else 0 end) rechiv_3,
               sum(  case when z.kod_rechiv=4 then z.erech_rechiv else 0 end) rechiv_4,
               sum( case when z.kod_rechiv=49 then z.erech_rechiv else 0 end) rechiv_49,
               sum( case when z.kod_rechiv=75 then z.erech_rechiv else 0 end) rechiv_75,
               sum( case when z.kod_rechiv=96 then z.erech_rechiv else 0 end) rechiv_96,
               sum( case when z.kod_rechiv=109 then z.erech_rechiv else 0 end) rechiv_109    
           from(
    
                  select  c.mispar_ishi,c.taarich,c.bakasha_id, c.kod_rechiv,c.erech_rechiv
                  from  TB_CHISHUV_chodesh_OVDIM c,bakasha b
                 where c.mispar_ishi=b.mispar_ishi
                    and c.taarich=b.taarich
                     and c.bakasha_id=b.bakasha_id
                     and c.kod_rechiv in(1,75,2,3,4,96,49,109) 
      --   and c.kod_rechiv in(1,75,18,2,3,4,252,151,250,100,21,19,20,96,131,76,77,78,55,49,91,92,101,102,103,109) 
                 ) z

        group by z.mispar_ishi,z.taarich, z.bakasha_id) Z2
        where   p.mispar_ishi=z2.mispar_ishi
         and last_day(z2.taarich) between P.ME_TARICH and P.AD_TARICH
         and z2.mispar_ishi= egt.mispar_ishi(+)
         and z2.taarich=egt.taarich(+)
         and z2.bakasha_id=egt.bakasha_id(+)           
     ) h
where  ov.mispar_ishi=h.mispar_ishi
     and ov.kod_hevra= m.kod_hevra
     and h.MAAMAD = m.KOD_MAAMAD_HR
     and h.gil =  G.KOD_GIL_HR
     and ov.kod_hevra= s.kod_hevra
     and h.SNIF_AV= S.KOD_SNIF_AV 
     and ov.kod_hevra= i.kod_hevra
     and h.ISUK = I.KOD_ISUK
     and ov.kod_hevra= e.kod_hevra
     and h.ezor=e.kod_ezor
      and  ov.kod_hevra=ch.kod_hevra
    order by h.taarich, h.mispar_ishi;
 end pro_get_mushalim_details;  
     procedure pro_cnt_ovdey_meshek_shabat(P_STARTDATE IN DATE,
                                                                        P_ENDDATE IN DATE , 
                                                                        p_cur OUT CurType)   as 
  
  begin
  DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_cnt_ovdey_meshek_shabat');
  open p_cur for
        with p as
            (  select p.mispar_ishi,p.me_tarich,p.ad_tarich,P.SNIF_AV,P.EZOR,P.YECHIDA_IRGUNIT,P.ISUK
                    
                                                   FROM PIVOT_PIRTEY_OVDIM P
                                                   WHERE
                                                          (P_STARTDATE  BETWEEN  P.ME_TARICH  AND   NVL(P.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                                                           OR P_ENDDATE  BETWEEN  P.ME_TARICH  AND   NVL(P.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                                                            OR   (P.ME_TARICH>=P_STARTDATE  AND   NVL(P.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=P_ENDDATE ))                                 
                         AND     ( substr(P.ISUK ,0,1) in (6,7)) 
                         and length (trim(P.ISUK))=3
                   --          and P.SNIF_AV in (82594 ,85407 ,88898 )
                             ),
           shabaton as 
               (select y.taarich ,Y.SUG_YOM,s.EREV_SHISHI_CHAG    erev_chag  
                from TB_YAMIM_MEYUCHADIM y, CTB_SUGEY_YAMIM_MEYUCHADIM s
                where Y.SUG_YOM=     S.SUG_YOM
                  and S.Shbaton=1
                  and s.pail=1
                  and Y.TAARICH  between  P_STARTDATE and P_ENDDATE
                  and Y.SUG_YOM not in( 27,29)  
                  
                   union 
                   select to_date('01/01/2100','dd/mm/yyyy') taarich ,null SUG_YOM,null    erev_chag
                   from dual )  ,
         params as(     
                  select  max(case when z.kod_param=48 then to_number(substr(z.erech_param,4,2)) else null end) tchilat_kaiz_month,
                            max(case when z.kod_param=49 then to_number(substr(z.erech_param,4,2)) else null end ) siyum_kaiz_month,
                            max(case when z.kod_param=270 then  z.erech_param  else null end ) hour_y_kaiz,
                            max(case when z.kod_param=269 then  z.erech_param  else null end ) hour_y_choref
                        from
                        (
                        select *
                        from tb_parametrim p
                        where kod_param in(48,49,270,269) 
                        ) z    )
              
        select     TAARICH,YOM,
           sum( case when KOD_EZOR=1 then cnt else null end) darom,
           sum( case when KOD_EZOR=2 then cnt else null end )zafon,
            sum(case when KOD_EZOR=3 then cnt else null end )jerusalem
          from(
        select  S.TAARICH, FullTeurDay( S.TAARICH) YOM,s.KOD_EZOR, s.TEUR_EZOR,
                 COUNT(S.MISPAR_ISHI) cnt
        from(       
         SELECT distinct S.mispar_ishi,s.taarich , E.KOD_EZOR, E.TEUR_EZOR
        from ovdim o,tb_sidurim_ovdim s,p,ctb_ezor e,  shabaton sh, params r
        where o.mispar_ishi=s.mispar_ishi
          and  s.taarich between  P_STARTDATE  and P_ENDDATE
          and S.MISPAR_SIDUR in( 99001 ,99220)
          and O.KOD_HEVRA=580
          and ( ( ( (s.taarich = sh.taarich and not (sh.sug_yom between 51 and 60) )  or 
                     (s.taarich not  in (select taarich from shabaton ) and  TO_CHAR(TRUNC(s.taarich),'d') ='7'  ) )
                   and ( ( to_number(to_char(s.taarich,'mm'))  between r.tchilat_kaiz_month and r.siyum_kaiz_month and S.SHAT_HATCHALA <> to_date('01/01/0001','dd/mm/yyyy') and S.SHAT_HATCHALA<  to_date( to_char(s.taarich,'dd/mm/yyyy') || ' ' || r.hour_y_kaiz,'dd/mm/yyyy HH24:ss'  )  )
                      or  (  to_number(to_char(s.taarich,'mm')) not between r.tchilat_kaiz_month and r.siyum_kaiz_month and S.SHAT_HATCHALA <> to_date('01/01/0001','dd/mm/yyyy') and S.SHAT_HATCHALA< to_date( to_char(s.taarich,'dd/mm/yyyy') || ' ' || r.hour_y_choref,'dd/mm/yyyy HH24:ss'  ) )) )
                or ( s.taarich = sh.taarich and ( sh.sug_yom between 51 and 60 or sh.erev_chag=1 )  )      
          )
        and trim(O.LEUM)='�'

          and S.MISPAR_ISHI = P.MISPAR_ISHI
          and S.TAARICH between P.ME_TARICH and P.AD_TARICH
          AND p.ezor       =  E.KOD_EZOR
          AND e.kod_hevra = o.kod_hevra
          
          ) s
          group by  S.TAARICH,s.KOD_EZOR,s.TEUR_EZOR)
         group by  TAARICH,YOM
         order by TAARICH ,YOM;
 
 
 end pro_cnt_ovdey_meshek_shabat;          

PROCEDURE  pro_get_kod_snif_av( P_KOD_EZOR in nvarchar2,
                                p_Cur OUT CurType )  IS
                                                     
BEGIN

open p_cur for
   SELECT a.Kod_Snif_Av KOD_SNIF_AV, a.Teur_Snif_Av  || ' (' ||  a.Kod_Snif_Av || ') '  || c.teur_hevra || ' ('  ||  b.kod_hevra || ')'    teur_snif_av
         FROM  CTB_SNIF_AV a, CTB_EZOR b, CTB_HEVRA c
         WHERE a.ezor=b.kod_ezor
                AND (P_KOD_EZOR='-1'  or a.EZOR in  (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_KOD_EZOR,  ',') AS MYTABTYPE))))--   (P_KOD_EZOR = -1 or a.EZOR =  P_KOD_EZOR) 
                AND a.kod_hevra = c.kod_hevra
                AND a.ezor=b.kod_ezor
                AND c.KOD_HEVRA=b.kod_hevra
         ORDER BY a.Teur_Snif_Av ASC ;
/*
select KOD_SNIF_AV,teur_snif_av 
from ctb_snif_av
where (P_KOD_EZOR = -1 or EZOR =  P_KOD_EZOR) 
ORDER BY teur_snif_av;
*/

 EXCEPTION 
        WHEN OTHERS THEN 
               RAISE;                     
            

END pro_get_kod_snif_av;

PROCEDURE  pro_get_kod_snif_av( p_Cur OUT CurType )  IS
                                                     
BEGIN

open p_cur for
   SELECT a.Kod_Snif_Av KOD_SNIF_AV, a.Teur_Snif_Av  || ' (' ||  a.Kod_Snif_Av || ') '  || c.teur_hevra || ' ('  ||  b.kod_hevra || ')'    teur_snif_av
         FROM  CTB_SNIF_AV a, CTB_EZOR b, CTB_HEVRA c
         WHERE a.ezor=b.kod_ezor
               -- AND (P_KOD_EZOR='-1'  or a.EZOR in  (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_KOD_EZOR,  ',') AS MYTABTYPE))))--   (P_KOD_EZOR = -1 or a.EZOR =  P_KOD_EZOR) 
                AND a.kod_hevra = c.kod_hevra
                AND a.ezor=b.kod_ezor
                AND c.KOD_HEVRA=b.kod_hevra
         ORDER BY a.Teur_Snif_Av ASC ;
/*
select KOD_SNIF_AV,teur_snif_av 
from ctb_snif_av
where (P_KOD_EZOR = -1 or EZOR =  P_KOD_EZOR) 
ORDER BY teur_snif_av;
*/

 EXCEPTION 
        WHEN OTHERS THEN 
               RAISE;                     
            

END pro_get_kod_snif_av;

PROCEDURE  pro_get_updated_tickets( P_STARTDATE in date,
                                    P_ENDDATE in date,
                                    P_MIS_RASHEMET in varchar2,
                                    p_snif in varchar2,
                                    p_ezor in number,
                                    P_TEZUGA in number,
                                p_Cur OUT CurType )  IS
 v_rechiv_109 number;                                                    
BEGIN 
  DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_get_updated_tickets');
v_rechiv_109:=0;

if (P_TEZUGA=1) then
select pkg_reports.func_get_rash_actual_work_days( P_STARTDATE, P_ENDDATE,  P_MIS_RASHEMET,  P_TEZUGA)  
into v_rechiv_109 from dual;

open p_cur for
    with por as
       (SELECT po.mispar_ishi,po.me_tarich,po.ad_tarich,Po.SNIF_AV,Po.EZOR,Po.YECHIDA_IRGUNIT,Po.ISUK, Po.MAAMAD
       FROM PIVOT_PIRTEY_OVDIM PO
       WHERE po.isuk=133
             AND (P_STARTDATE  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
               OR P_ENDDATE  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                OR   (po.ME_TARICH>=P_STARTDATE   AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=P_ENDDATE  ))
       )
       ,
    details as(
    /* select  de.TAARICH, de.MISPAR_ISHI, de.id_rashemet ,  de.name_rashemet , de.snif_av, de.TEUR_SNIF_AV, de.TAARICH_card,
             OvdimCount, friend,   salaried,  measher, 
           MISTAYEG, nofill, 
            case when  MISPAR_ISHI <>  nvl(lag(MISPAR_ISHI,1) over (partition by  MISPAR_ISHI,id_rashemet,SNIF_AV order by  MISPAR_ISHI, id_rashemet,  SNIF_AV),0)
                 then tickets 
                 else 0 end tickets
  from(*/
    select distinct rank() over (partition by IR.GOREM_MEADKEN,PO.SNIF_AV  order by  IR.MISPAR_ISHI, ir.TAARICH) row_num, IR.MISPAR_ISHI, PO.SNIF_AV, S.TEUR_SNIF_AV,  O.SHEM_MISH ||' '||O.SHEM_PRAT name_rashemet , 
    IR.GOREM_MEADKEN id_rashemet, trunc(IR.TAARICH_IDKUN,'mm') TAARICH,ir.TAARICH TAARICH_card,
      (count(distinct IR.MISPAR_ISHI ) over (PARTITION BY IR.GOREM_MEADKEN,PO.SNIF_AV  )) OvdimCount,
       count(distinct ir.TAARICH) over (partition by  IR.MISPAR_ISHI,IR.GOREM_MEADKEN,PO.SNIF_AV) tickets,
           (case when  substr(po.maamad,0,1)=1 then 1 else 0 end) friend,
           (case when  substr(po.maamad,0,1)=2 then 1 else 0 end) salaried,
           (case when  Y.MEASHER_O_MISTAYEG=1 then 1 else 0 end) measher,
           (case when  Y.MEASHER_O_MISTAYEG=0 then 1 else 0 end) MISTAYEG,
           (case when  Y.MEASHER_O_MISTAYEG is null then 1 else 0 end) nofill                 
    from tb_idkun_rashemet ir
         left join ovdim o on IR.GOREM_MEADKEN = O.MISPAR_ISHI
         left join (  select  p.mispar_ishi,p.me_tarich,p.ad_tarich,P.SNIF_AV,P.EZOR,P.YECHIDA_IRGUNIT,P.ISUK, P.MAAMAD
                        from pivot_pirtey_ovdim p
                        where p.isuk IS NOT NULL
                           AND (P_STARTDATE  BETWEEN  p.ME_TARICH  AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
               OR P_ENDDATE  BETWEEN  p.ME_TARICH  AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                OR   (p.ME_TARICH>=P_STARTDATE   AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=P_ENDDATE  ))
                     ) po on IR.MISPAR_ISHI = Po.MISPAR_ISHI  and ir.TAARICH between Po.ME_TARICH and Po.AD_TARICH
          left join por on IR.GOREM_MEADKEN = Por.MISPAR_ISHI  and IR.TAARICH_IDKUN between Por.ME_TARICH and Por.AD_TARICH
         left join tb_yamey_avoda_ovdim y on IR.MISPAR_ISHI = Y.MISPAR_ISHI and IR.TAARICH = Y.TAARICH 
         inner join  ctb_snif_av s on PO.SNIF_AV = S.KOD_SNIF_AV
    where (P_MIS_RASHEMET is null or IR.GOREM_MEADKEN  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MIS_RASHEMET,  ',') AS MYTABTYPE))))
            and (p_snif is null or PO.SNIF_AV in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_snif,  ',') AS MYTABTYPE))))
            and (p_ezor = -1 or PO.EZOR=p_ezor)
          and IR.TAARICH_IDKUN between P_STARTDATE and  P_ENDDATE
          and por.ISUK=133 
    -- order by IR.MISPAR_ISHI, IR.GOREM_MEADKEN,  PO.SNIF_AV
   --    )de
     ),
     details2 as (
             select d.id_rashemet ,  d.name_rashemet , d.snif_av, d.TEUR_SNIF_AV,
                    max(d.OvdimCount) OvdimCount,sum(friend) friend,  sum(salaried) salaried, sum(measher) measher, 
                    sum(MISTAYEG) MISTAYEG, sum(nofill) nofill,
                    count(*) tickets
             from       ( select distinct d.MISPAR_ISHI, d.TAARICH_card, d.id_rashemet ,  d.name_rashemet , d.snif_av, d.TEUR_SNIF_AV,
                                  d.OvdimCount,d.friend, d.salaried, d.measher,  d.MISTAYEG, d.nofill, d.tickets
                          from details d 
                        ) d
         group by d.id_rashemet ,d.name_rashemet ,  d.snif_av, d.TEUR_SNIF_AV
     )
    select d.TAARICH, d.id_rashemet ,  d.name_rashemet , d.snif_av, d.TEUR_SNIF_AV,
            d2.OvdimCount, d2.friend,  d2.salaried, d2.measher,  d2.MISTAYEG, d2.nofill,
            d2.tickets,  count(*) update_tickets,
            v_rechiv_109 rechiv_109 
    from details d 
    left join details2 d2 on d.id_rashemet = d2.id_rashemet and d.snif_av=d2.snif_av and row_num=1
    group by d.TAARICH, d.id_rashemet ,d.name_rashemet ,  d.snif_av, d.TEUR_SNIF_AV, d2.OvdimCount, d2.friend,  d2.salaried, d2.measher,  d2.MISTAYEG, d2.nofill,d2.tickets;


 

   -- order by    d.id_rashemet ,  d.name_rashemet ,d.Ezor,  d.snif_av, d.TEUR_SNIF_AV;

else
    open p_cur for
       with por as
       (SELECT po.mispar_ishi,po.me_tarich,po.ad_tarich,Po.SNIF_AV,Po.YECHIDA_IRGUNIT,Po.ISUK, Po.MAAMAD
       FROM PIVOT_PIRTEY_OVDIM PO
       WHERE po.isuk=133
             AND (P_STARTDATE  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
               OR P_ENDDATE  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                OR   (po.ME_TARICH>=P_STARTDATE   AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=P_ENDDATE  ))
       ),
   details as(
     
    select distinct rank() over (partition by IR.GOREM_MEADKEN,PO.SNIF_AV  order by  IR.MISPAR_ISHI, ir.TAARICH) row_num,IR.MISPAR_ISHI, PO.SNIF_AV, S.TEUR_SNIF_AV,  O.SHEM_MISH ||' '||O.SHEM_PRAT name_rashemet , 
           IR.GOREM_MEADKEN id_rashemet,  trunc(IR.TAARICH_IDKUN,'mm') TAARICH, ir.TAARICH TAARICH_card,
           (count(distinct IR.MISPAR_ISHI ) over (PARTITION BY IR.GOREM_MEADKEN, PO.SNIF_AV  )) OvdimCount,
              count(distinct ir.TAARICH) over (partition by  IR.MISPAR_ISHI,IR.GOREM_MEADKEN,PO.SNIF_AV) tickets,
          -- (count(*) over  (PARTITION BY po.SNIF_AV )) SnifOvdimCount,
           (case when  substr(po.maamad,0,1)=1 then 1 else 0 end) friend,
           (case when  substr(po.maamad,0,1)=2 then 1 else 0 end) salaried,
           (case when  Y.MEASHER_O_MISTAYEG=1 then 1 else 0 end) measher,
           (case when  Y.MEASHER_O_MISTAYEG=0 then 1 else 0 end) MISTAYEG,
           (case when  Y.MEASHER_O_MISTAYEG is null then 1 else 0 end) nofill                 
    from tb_idkun_rashemet ir
         left join ovdim o on IR.GOREM_MEADKEN = O.MISPAR_ISHI
          left join (  select  p.mispar_ishi,p.me_tarich,p.ad_tarich,P.SNIF_AV,P.EZOR,P.YECHIDA_IRGUNIT,P.ISUK, P.MAAMAD
                        from pivot_pirtey_ovdim p
                        where p.isuk IS NOT NULL
                         AND (P_STARTDATE  BETWEEN  p.ME_TARICH  AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
               OR P_ENDDATE  BETWEEN  p.ME_TARICH  AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                OR   (p.ME_TARICH>=P_STARTDATE   AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=P_ENDDATE  ))
                     ) po on IR.MISPAR_ISHI = Po.MISPAR_ISHI  and ir.TAARICH between Po.ME_TARICH and Po.AD_TARICH
                     
          left join por on IR.GOREM_MEADKEN = Por.MISPAR_ISHI  and IR.TAARICH_IDKUN between Por.ME_TARICH and Por.AD_TARICH 
         left join tb_yamey_avoda_ovdim y on IR.MISPAR_ISHI = Y.MISPAR_ISHI and IR.TAARICH = Y.TAARICH 
         inner join ctb_snif_av s on PO.SNIF_AV = S.KOD_SNIF_AV 
    where (P_MIS_RASHEMET is null or IR.GOREM_MEADKEN  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MIS_RASHEMET,  ',') AS MYTABTYPE))))
            and (p_snif is null or PO.SNIF_AV in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_snif,  ',') AS MYTABTYPE))))
            and (p_ezor = -1 or PO.EZOR=p_ezor)
          and IR.TAARICH_IDKUN between P_STARTDATE and  P_ENDDATE
          and por.ISUK=133 
          -- order by IR.MISPAR_ISHI, IR.GOREM_MEADKEN,  PO.SNIF_AV
      
    ),
      details2 as (
             select d.id_rashemet ,  d.name_rashemet , d.snif_av, d.TEUR_SNIF_AV,
                    max(d.OvdimCount) OvdimCount,sum(friend) friend,  sum(salaried) salaried, sum(measher) measher, 
                    sum(MISTAYEG) MISTAYEG, sum(nofill) nofill, 
                    
                      (select count(*) from (  select distinct  p.mispar_ishi, p.SNIF_AV
                        from pivot_pirtey_ovdim p
                        where p.isuk IS NOT NULL 
                             AND (P_STARTDATE  BETWEEN  p.ME_TARICH  AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                                   OR P_ENDDATE  BETWEEN  p.ME_TARICH  AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                                    OR   (p.ME_TARICH>=P_STARTDATE   AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=P_ENDDATE  ))
                            
                     ) ppo where ppo.SNIF_AV = d.snif_av ) SnifOvdimCount, 
                    
                    count(*) tickets
             from       ( select distinct d.MISPAR_ISHI, d.TAARICH_card, d.id_rashemet ,  d.name_rashemet , d.snif_av, d.TEUR_SNIF_AV,
                                  d.OvdimCount,d.friend, d.salaried, d.measher,  d.MISTAYEG, d.nofill, d.tickets
                          from details d 
                        ) d
         group by  d.snif_av, d.TEUR_SNIF_AV , d.id_rashemet ,d.name_rashemet 
     )
    select d.TAARICH, d.id_rashemet ,  d.name_rashemet , d.snif_av, d.TEUR_SNIF_AV,d2.SnifOvdimCount,
            d2.OvdimCount, d2.friend,  d2.salaried, d2.measher,  d2.MISTAYEG, d2.nofill,
            d2.tickets,  count(*) update_tickets,
            v_rechiv_109 rechiv_109 
    from details d 
    left join details2 d2 on d.id_rashemet = d2.id_rashemet and d.snif_av=d2.snif_av and row_num=1
    group by d.TAARICH,  d.snif_av, d.TEUR_SNIF_AV, d.id_rashemet ,d.name_rashemet, d2.OvdimCount, d2.SnifOvdimCount, d2.friend,  d2.salaried, d2.measher,  d2.MISTAYEG, d2.nofill,d2.tickets;

   
end if;


 EXCEPTION 
        WHEN OTHERS THEN 
               RAISE;                     
            

END pro_get_updated_tickets;

PROCEDURE  pro_get_updated_tickets_tomer( P_STARTDATE in date,
                                    P_ENDDATE in date,
                                    P_MIS_RASHEMET in varchar2,
                                    p_snif in varchar2,
                                    p_ezor in number,
                                    P_TEZUGA in number,
                                p_Cur OUT CurType )  IS
 v_rechiv_109 number;                                                    
BEGIN 

v_rechiv_109:=0;

if (P_TEZUGA=1) then

select pkg_reports.func_get_rash_actual_work_days( P_STARTDATE, P_ENDDATE,  P_MIS_RASHEMET,  P_TEZUGA) 
into v_rechiv_109 from dual;

open p_cur for
    with por as
       (SELECT po.mispar_ishi,po.me_tarich,po.ad_tarich,Po.SNIF_AV,Po.EZOR,Po.YECHIDA_IRGUNIT,Po.ISUK, Po.MAAMAD
       FROM PIVOT_PIRTEY_OVDIM PO
       WHERE po.isuk=133
             AND (P_STARTDATE  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
               OR P_ENDDATE  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                OR   (po.ME_TARICH>=P_STARTDATE   AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=P_ENDDATE  ))
       )

     select  de.TAARICH,de.TAARICH_CARD, de.MISPAR_ISHI, de.id_rashemet ,  de.name_rashemet , de.snif_av, de.TEUR_SNIF_AV,
             OvdimCount, friend,   salaried,  measher, 
           MISTAYEG, nofill, 
            case when  MISPAR_ISHI <>  nvl(lag(MISPAR_ISHI,1) over (partition by  MISPAR_ISHI,id_rashemet,SNIF_AV order by  MISPAR_ISHI, id_rashemet,  SNIF_AV),0)
                 then tickets 
                 else 0 end tickets
  from(
    select distinct IR.MISPAR_ISHI, PO.SNIF_AV, S.TEUR_SNIF_AV,  O.SHEM_MISH ||' '||O.SHEM_PRAT name_rashemet , 
    IR.GOREM_MEADKEN id_rashemet, trunc(IR.TAARICH_IDKUN,'mm') TAARICH,ir.TAARICH TAARICH_card,
      (count(distinct IR.MISPAR_ISHI ) over (PARTITION BY IR.GOREM_MEADKEN, PO.SNIF_AV  )) OvdimCount,
       count(distinct ir.TAARICH) over (partition by  IR.MISPAR_ISHI,IR.GOREM_MEADKEN,PO.SNIF_AV) tickets,
           (case when  substr(po.maamad,0,1)=1 then 1 else 0 end) friend,
           (case when  substr(po.maamad,0,1)=2 then 1 else 0 end) salaried,
           (case when  Y.MEASHER_O_MISTAYEG=1 then 1 else 0 end) measher,
           (case when  Y.MEASHER_O_MISTAYEG=0 then 1 else 0 end) MISTAYEG,
           (case when  Y.MEASHER_O_MISTAYEG is null then 1 else 0 end) nofill                 
    from tb_idkun_rashemet ir
         left join ovdim o on IR.GOREM_MEADKEN = O.MISPAR_ISHI
         left join (  select  p.mispar_ishi,p.me_tarich,p.ad_tarich,P.SNIF_AV,P.EZOR,P.YECHIDA_IRGUNIT,P.ISUK, P.MAAMAD
                        from pivot_pirtey_ovdim p
                        where p.isuk IS NOT NULL
                           AND (P_STARTDATE  BETWEEN  p.ME_TARICH  AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
               OR P_ENDDATE  BETWEEN  p.ME_TARICH  AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                OR   (p.ME_TARICH>=P_STARTDATE   AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=P_ENDDATE  ))
                     ) po on IR.MISPAR_ISHI = Po.MISPAR_ISHI  and ir.TAARICH between Po.ME_TARICH and Po.AD_TARICH
          left join por on IR.GOREM_MEADKEN = Por.MISPAR_ISHI  and IR.TAARICH_IDKUN between Por.ME_TARICH and Por.AD_TARICH
         left join tb_yamey_avoda_ovdim y on IR.MISPAR_ISHI = Y.MISPAR_ISHI and IR.TAARICH = Y.TAARICH 
         inner join ctb_snif_av s on PO.SNIF_AV = S.KOD_SNIF_AV 
    where (P_MIS_RASHEMET is null or IR.GOREM_MEADKEN  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MIS_RASHEMET,  ',') AS MYTABTYPE))))
            and (p_snif is null or PO.SNIF_AV in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_snif,  ',') AS MYTABTYPE))))
            and (p_ezor = -1 or PO.EZOR=p_ezor)
          and IR.TAARICH_IDKUN between P_STARTDATE and  P_ENDDATE
          and por.ISUK=133 
     order by IR.MISPAR_ISHI, IR.GOREM_MEADKEN, PO.SNIF_AV
       )de;
     
else
    open p_cur for
         with por as
       (SELECT po.mispar_ishi,po.me_tarich,po.ad_tarich,Po.SNIF_AV,Po.EZOR,Po.YECHIDA_IRGUNIT,Po.ISUK, Po.MAAMAD
       FROM PIVOT_PIRTEY_OVDIM PO
       WHERE po.isuk=133
             AND (P_STARTDATE  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
               OR P_ENDDATE  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                OR   (po.ME_TARICH>=P_STARTDATE   AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=P_ENDDATE  ))
       )
      select  de.TAARICH,de.TAARICH_CARD, de.MISPAR_ISHI, de.id_rashemet ,  de.name_rashemet , de.snif_av, de.TEUR_SNIF_AV
             OvdimCount, friend,   salaried,  measher, 
           MISTAYEG, nofill, 
            case when  MISPAR_ISHI <>  nvl(lag(MISPAR_ISHI,1) over (partition by  MISPAR_ISHI,id_rashemet,SNIF_AV order by  MISPAR_ISHI, id_rashemet, SNIF_AV),0)
                 then tickets 
                 else 0 end tickets
  from(
    select distinct IR.MISPAR_ISHI, PO.SNIF_AV, S.TEUR_SNIF_AV,  O.SHEM_MISH ||' '||O.SHEM_PRAT name_rashemet , 
           IR.GOREM_MEADKEN id_rashemet,  trunc(IR.TAARICH_IDKUN,'mm') TAARICH, ir.TAARICH TAARICH_card,
           (count(distinct IR.MISPAR_ISHI ) over (PARTITION BY IR.GOREM_MEADKEN, PO.SNIF_AV  )) OvdimCount,
              count(distinct ir.TAARICH) over (partition by  IR.MISPAR_ISHI,IR.GOREM_MEADKEN,PO.SNIF_AV) tickets,
          -- (count(*) over  (PARTITION BY po.SNIF_AV )) SnifOvdimCount,
           (case when  substr(po.maamad,0,1)=1 then 1 else 0 end) friend,
           (case when  substr(po.maamad,0,1)=2 then 1 else 0 end) salaried,
           (case when  Y.MEASHER_O_MISTAYEG=1 then 1 else 0 end) measher,
           (case when  Y.MEASHER_O_MISTAYEG=0 then 1 else 0 end) MISTAYEG,
           (case when  Y.MEASHER_O_MISTAYEG is null then 1 else 0 end) nofill                 
    from tb_idkun_rashemet ir
         left join ovdim o on IR.GOREM_MEADKEN = O.MISPAR_ISHI
          left join (  select  p.mispar_ishi,p.me_tarich,p.ad_tarich,P.SNIF_AV,P.EZOR,P.YECHIDA_IRGUNIT,P.ISUK, P.MAAMAD
                        from pivot_pirtey_ovdim p
                        where p.isuk IS NOT NULL
                         AND (P_STARTDATE  BETWEEN  p.ME_TARICH  AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
               OR P_ENDDATE  BETWEEN  p.ME_TARICH  AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                OR   (p.ME_TARICH>=P_STARTDATE   AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=P_ENDDATE  ))
                     ) po on IR.MISPAR_ISHI = Po.MISPAR_ISHI  and ir.TAARICH between Po.ME_TARICH and Po.AD_TARICH
                     
          left join por on IR.GOREM_MEADKEN = Por.MISPAR_ISHI  and IR.TAARICH_IDKUN between Por.ME_TARICH and Por.AD_TARICH 
         left join tb_yamey_avoda_ovdim y on IR.MISPAR_ISHI = Y.MISPAR_ISHI and IR.TAARICH = Y.TAARICH 
         inner join ctb_snif_av s on PO.SNIF_AV = S.KOD_SNIF_AV 
    where (P_MIS_RASHEMET is null or IR.GOREM_MEADKEN  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MIS_RASHEMET,  ',') AS MYTABTYPE))))
            and (p_snif is null or PO.SNIF_AV in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_snif,  ',') AS MYTABTYPE))))
            and (p_ezor = -1 or PO.EZOR=p_ezor)
          and IR.TAARICH_IDKUN between P_STARTDATE and  P_ENDDATE
          and por.ISUK=133 
           order by IR.MISPAR_ISHI, IR.GOREM_MEADKEN,  PO.SNIF_AV
       )de;
  
end if;


 EXCEPTION 
        WHEN OTHERS THEN 
               RAISE;                     
            

END pro_get_updated_tickets_tomer;

function  func_get_rash_actual_work_days( P_STARTDATE in date,
                                    P_ENDDATE in date,
                                    P_MIS_RASHEMET in varchar2,
                                    P_TEZUGA in number)   return nvarchar2 is

rechiv_109 number;                                                    
BEGIN 

rechiv_109:=0;

if (P_TEZUGA=1) then
  with bakasha as
   (
        SELECT    a.mispar_ishi, a.Taarich, a.bakasha_id 
          FROM ( SELECT co.mispar_ishi, co.Taarich, co.bakasha_id,b.TAARICH_HAAVARA_LESACHAR th1,
                                  MAX(B.TAARICH_HAAVARA_LESACHAR) OVER (PARTITION BY co.mispar_ishi,co.Taarich )  th2
                     FROM TB_CHISHUV_CHODESH_OVDIM co,TB_BAKASHOT b
                     WHERE   B.BAKASHA_ID = CO.BAKASHA_ID
                            AND b.Huavra_Lesachar=1
                               and CO.TAARICH between P_STARTDATE and P_ENDDATE
                          and  co.mispar_ishi in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MIS_RASHEMET,  ',') AS MYTABTYPE))) and CO.KOD_RECHIV=109
                    ) a
        WHERE a.th1=a.th2 
    GROUP BY  a.mispar_ishi, a.Taarich, a.bakasha_id
     )
          select   nvl(sum(case when c.kod_rechiv=109 then c.erech_rechiv else 0 end) ,0) rechiv_109
          into rechiv_109
          from  TB_CHISHUV_YOMI_OVDIM c,bakasha b
          where c.mispar_ishi=b.mispar_ishi
                and c.taarich between b.taarich and last_day(b.taarich) 
                 and c.bakasha_id=b.bakasha_id
                 and c.kod_rechiv =109
                 and C.TAARICH between P_STARTDATE and P_ENDDATE;
         -- group by c.mispar_ishi,b.taarich, c.bakasha_id;
end if;

return rechiv_109;

 EXCEPTION 
        WHEN OTHERS THEN 
               RAISE;                     
            

END func_get_rash_actual_work_days;


/*

 procedure pro_get_tigburim_details(P_STARTDATE IN DATE,
                                    P_ENDDATE IN DATE , 
                                    p_cur OUT CurType)   as 
   QryMakatDate VARCHAR2(3500);
  begin
    DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_get_tigburim_details');
 --QryMakatDate :=  fct_MakatDateSql(P_STARTDATE ,P_ENDDATE  ,null ,null ,null  );
 QryMakatDate:= 'with p as
                      (  select p.mispar_ishi,p.me_tarich,p.ad_tarich,P.SNIF_AV,P.EZOR,P.YECHIDA_IRGUNIT,P.ISUK, P.MAAMAD, P.GIL, P.MIKUM_YECHIDA
                         from pivot_pirtey_ovdim p,
                         (SELECT po.mispar_ishi,MAX(po.ME_TARICH) me_taarich
                                           FROM PIVOT_PIRTEY_OVDIM PO
                                           WHERE po.isuk IS NOT NULL
                                                 AND ('''||P_STARTDATE ||''' BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE(''01/01/9999'' ,''dd/mm/yyyy''))
                                                   OR '''||P_ENDDATE||'''  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE(''01/01/9999'' ,''dd/mm/yyyy''))
                                                    OR   (po.ME_TARICH>='''||P_STARTDATE ||'''  AND   NVL(po.ad_TARICH,TO_DATE(''01/01/9999'' ,''dd/mm/yyyy''))<='''||P_ENDDATE||'''  ))
                                          GROUP BY po.mispar_ishi) RelevantDetails 
                      where   p.mispar_ishi= RelevantDetails.mispar_ishi
                                 and   P.ME_TARICH = RelevantDetails.me_taarich  )
                        
                        Select   distinct activity.makat_nesia,ACTIVITY.TAARICH  
                        FROM TB_PEILUT_OVDIM Activity
                        left join TB_SIDURIM_OVDIM so on  (so.mispar_ishi          = activity.mispar_ishi)  
                                                                  AND (so.mispar_sidur  = activity.mispar_sidur)
                                                                  AND (so.shat_hatchala = activity.shat_hatchala_sidur) 
                                                                  AND (so.taarich           = activity.taarich)  
                       left join VEHICLE_SPECIFICATIONS v on Activity.OTO_NO = V.BUS_NUMBER
                        inner join p on Activity.MISPAR_ISHI = P.MISPAR_ISHI  and Activity.TAARICH between P.ME_TARICH and P.AD_TARICH
                        WHERE  SO.MISPAR_SIDUR not like ''99%''
                              and length(SO.MISPAR_SIDUR) between 4 and 5
                              AND so.taarich BETWEEN '''|| P_STARTDATE||''' AND '''||P_ENDDATE||
                             ''' and ((substr(to_char(Activity.MISPAR_SIDUR),0,2) in (''38'',''53'',''67'',''45'',''46'',''98''))
                                          or (P.SNIF_AV = 4 ) or (substr(to_char(V.BRANCH2),3,5) in  (''38'',''53'',''67'',''45'',''46'',''98'')))';

 
 pro_Prepare_Catalog_Details(QryMakatDate);

  open p_cur for
 with p as
      (  select p.mispar_ishi,p.me_tarich,p.ad_tarich,P.SNIF_AV,P.EZOR,P.YECHIDA_IRGUNIT,P.ISUK, P.MAAMAD, P.GIL, P.MIKUM_YECHIDA
      from pivot_pirtey_ovdim p,
         (SELECT po.mispar_ishi,MAX(po.ME_TARICH) me_taarich
                               FROM PIVOT_PIRTEY_OVDIM PO
                               WHERE po.isuk IS NOT NULL
                                     AND (P_STARTDATE  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                                       OR P_ENDDATE  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                                        OR   (po.ME_TARICH>=P_STARTDATE   AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=P_ENDDATE  ))
                              GROUP BY po.mispar_ishi) RelevantDetails 
          where   p.mispar_ishi= RelevantDetails.mispar_ishi
            and   P.ME_TARICH = RelevantDetails.me_taarich  )
    ,bakasha as
   (
        SELECT    a.mispar_ishi, a.Taarich, a.bakasha_id 
          FROM P,
           ( SELECT   co.mispar_ishi, co.Taarich, co.bakasha_id,b.TAARICH_HAAVARA_LESACHAR th1,
                                  MAX(B.TAARICH_HAAVARA_LESACHAR) OVER (PARTITION BY co.mispar_ishi,co.Taarich )  th2
                     FROM TB_CHISHUV_CHODESH_OVDIM co,TB_BAKASHOT b,p
                     WHERE   B.BAKASHA_ID = CO.BAKASHA_ID
                            AND b.Huavra_Lesachar=1
                               and CO.TAARICH between P_STARTDATE and P_ENDDATE
                          and  co.mispar_ishi=p.mispar_ishi
                     ) a
        WHERE a.th1=a.th2 
        AND a.mispar_ishi=P.mispar_ishi
    GROUP BY  a.mispar_ishi, a.Taarich, a.bakasha_id
      )
      , cs as
      (select  z.mispar_ishi,z.taarich, z.bakasha_id,z.MISPAR_SIDUR,z.SHAT_HATCHALA,
                      sum(case when z.kod_rechiv=1 then z.erech_rechiv else 0 end) rechiv_1,
                      sum(case when z.kod_rechiv=5 then z.erech_rechiv else 0 end) rechiv_5                
             from(select c.mispar_ishi,c.taarich,c.bakasha_id,C.MISPAR_SIDUR,C.SHAT_HATCHALA, c.kod_rechiv,c.erech_rechiv--,c.mispar_sidur,substr(lpad(c.mispar_sidur,5,'0'),1,2) snif_tnua
                  from  TB_CHISHUV_sidur_OVDIM c,bakasha b
                  where c.mispar_ishi=b.mispar_ishi
                        and c.taarich between b.taarich and last_day(b.taarich) 
                         and c.bakasha_id=b.bakasha_id
                         and c.kod_rechiv in(1,5)
                   ) z
             group by z.mispar_ishi,z.taarich, z.bakasha_id, 
                      z.MISPAR_SIDUR,z.SHAT_HATCHALA)
  select  distinct PO.MISPAR_ISHI, O.SHEM_MISH || ' '|| O.SHEM_PRAT full_name, P.MAAMAD, P.GIL , P.EZOR, P.SNIF_AV, 
         P.ISUK, I.TEUR_ISUK, PO.TAARICH, Dayofweek(PO.TAARICH) Dayofweek, PO.MISPAR_SIDUR , SO.SHAT_HATCHALA, SO.SHAT_GMAR,
         CS.rechiv_1 attendance_minutes, CS.rechiv_5 absence_minutes , C.KM as km, SO.CHARIGA, SO.HASHLAMA,
          case when substr(PO.MISPAR_SIDUR,0,2)='99' then (select to_char(SM.KOD_SIDUR_MEYUCHAD_YASHAN) from ctb_sidurim_meyuchadim sm where SO.MISPAR_SIDUR = SM.KOD_SIDUR_MEYUCHAD )
              else (select SS.TEUR_SIDUR_AVODA from ctb_sug_sidur ss where SO.SUG_SIDUR = SS.KOD_SIDUR_AVODA ) end name_sidur, 
          P.MIKUM_YECHIDA, PO.SNIF_TNUA, PO.OTO_NO, V.LICENSE_NUMBER,
         substr(V.BRANCH2, 3,4) BRANCH2, 
        PKG_SIDURIM.GETSNIFTNUABYSIDUR(PO.MISPAR_ISHI, Po.TAARICH , po.MISPAR_SIDUR, po.SHAT_HATCHALA_SIDUR) snif_metugbar,
        po.mispar_visa, SV.TEUR_SECTOR_VISA as sug_visa     
  from tb_peilut_ovdim po
  inner join p on PO.MISPAR_ISHI = P.MISPAR_ISHI  and po.TAARICH between P.ME_TARICH and P.AD_TARICH
  left join VEHICLE_SPECIFICATIONS v on PO.OTO_NO = V.BUS_NUMBER 
  inner join Ovdim o on PO.MISPAR_ISHI = O.MISPAR_ISHI
  left join ctb_isuk i on P.ISUK=I.KOD_ISUK and O.KOD_HEVRA=I.KOD_HEVRA
  inner join tb_sidurim_ovdim so on PO.MISPAR_SIDUR = SO.MISPAR_SIDUR and PO.MISPAR_ISHI = SO.MISPAR_ISHI and PO.TAARICH = SO.TAARICH and PO.SHAT_HATCHALA_SIDUR = SO.SHAT_HATCHALA
  inner join cs on po.mispar_ishi = cs.mispar_ishi and po.taarich = cs.taarich  
                                                            and PO.MISPAR_SIDUR=CS.MISPAR_SIDUR and PO.SHAT_HATCHALA_SIDUR= CS.SHAT_HATCHALA
  left join CTB_SECTOR_VISA sv on SO.SECTOR_VISA = SV.KOD_SECTOR_VISA
  left join TMP_CATALOG c on PO.MAKAT_NESIA = C.MAKAT8 and PO.TAARICH = C.ACTIVITY_DATE
  where  Po.TAARICH between P_STARTDATE AND  P_ENDDATE
         and ((substr(to_char(Po.MISPAR_SIDUR),0,2) in ('38','53','67','45','46','98'))
               or (P.SNIF_AV = 4 )  or (substr(to_char(V.BRANCH2),3,5) in  ('38','53','67','45','46','98')))    ;



 EXCEPTION 
        WHEN OTHERS THEN 
               RAISE;                     
            

END pro_get_tigburim_details;*/


procedure pro_get_pirtey_ovdey_kytanot(P_STARTDATE IN DATE,
                                                            P_ENDDATE IN DATE , 
                                                            p_cur OUT CurType)   as 
begin
 DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_get_pirtey_ovdey_kytanot');
     open p_cur for
          with p as
            (  select p.mispar_ishi,p.me_tarich,p.ad_tarich,P.SNIF_AV,P.EZOR,P.DIRUG,P.DARGA,P.YECHIDA_IRGUNIT, P.TCHILAT_AVODA
                FROM PIVOT_PIRTEY_OVDIM P
                 WHERE (P_STARTDATE  BETWEEN  P.ME_TARICH  AND   NVL(P.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                           OR P_ENDDATE  BETWEEN  P.ME_TARICH  AND   NVL(P.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                            OR   (P.ME_TARICH>=P_STARTDATE  AND   NVL(P.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=P_ENDDATE ))                                 
                       and (P.ISUK=161 or  (P.ISUK BETWEEN 870 AND 893))
                             )
         
            SELECT  O.MISPAR_ISHI,O.MISPAR_ISHI || O.SIFRAT_BIKORET_MI MISPAR_OVED, O.SHEM_PRAT , O.SHEM_MISH  ,
                        O.TEUDAT_ZEHUT,P.TCHILAT_AVODA,  O.KTOVET, O.SHEM_AV,  P.DIRUG|| LPAD(P.DARGA,2,'0') DARGA,O.TAARICH_LEDA,YI.TEUR_YECHIDA,
                        case when (months_between (TO_DATE('01/07/2013','DD/MM/YYYY'),O.TAARICH_LEDA)/12) >18 then 8 else 7 end micsa,
                       y.TAARICH,  to_char(S.SHAT_HATCHALA ,'HH24:mi')  SHAT_HATCHALA,
                         to_char(S.SHAT_GMAR ,'HH24:mi')  SHAT_GMAR,
                         dayofweek(y.TAARICH)
            FROM OVDIM O , tb_yamey_avoda_ovdim y,
                      TB_SIDURIM_OVDIM S, P, CTB_YECHIDA YI
            WHERE y.MISPAR_ISHI=s.MISPAR_ISHI(+)
                 and  y.taarich = S.TAARICH(+)
                 and O.MISPAR_ISHI=y.MISPAR_ISHI
                 and  y.MISPAR_ISHI=P.MISPAR_ISHI
                 and y.TAARICH BETWEEN p.me_tarich AND p.ad_tarich
                 and y.TAARICH between  P_STARTDATE AND P_ENDDATE
                 and YI.KOD_YECHIDA = P.YECHIDA_IRGUNIT
            ORDER BY   P.YECHIDA_IRGUNIT , O.MISPAR_ISHI,y.TAARICH, S.SHAT_HATCHALA;
end pro_get_pirtey_ovdey_kytanot;


 PROCEDURE  pro_get_Report_Details(p_kod_doch IN  CTB_SUGEY_DOCHOT.KOD_SUG_DOCH%TYPE, p_cur OUT CurType) AS 
BEGIN 
     OPEN p_cur FOR          
                 SELECT DISTINCT C.KOD_SUG_DOCH ,C.TEUR_DOCH as PageHeader, c.TEUR_DOCH, C.RS_VERSION as RS_VERSION, R.EXTENSION, r.URL_CONFIG_KEY, r.SERVICE_URL_CONFIG_KEY
               FROM   CTB_SUGEY_DOCHOT c inner join  CTB_DOCHOT_RS_VERSION r on  C.RS_VERSION = r.RS_VERSION and 
                C.KOD_SUG_DOCH = p_kod_doch --p_shem_doch
                order by C.KOD_SUG_DOCH asc;
 EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END         pro_get_Report_Details;  
  
  
  procedure pro_rpt_sidur_vaad_ovdim(P_STARTDATE IN DATE,
                                                            P_ENDDATE IN DATE , 
                                                            p_cur OUT CurType)   as 
begin

OPEN p_cur FOR          
  with p as
(
     SELECT   t.mispar_ishi ,T.me_tarich,T.ad_tarich, T.SNIF_AV,t.EZOR,t.MAAMAD,t.ISUK, T.GIL
                                  -- row_number() OVER (PARTITION BY t.mispar_ishi ORDER BY me_tarich desc) seq
                     FROM  PIVOT_PIRTEY_OVDIM t
                     WHERE t.isuk IS NOT NULL 
                     and (P_STARTDATE  BETWEEN  t.ME_TARICH  AND   NVL(t.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                           OR P_ENDDATE  BETWEEN  t.ME_TARICH  AND   NVL(t.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                            OR   (t.ME_TARICH>=P_STARTDATE  AND   NVL(t.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=P_ENDDATE ))  
                   --  and  t.mispar_ishi=201
           
)

select O.MISPAR_ISHI, O.SHEM_MISH || ' '|| O.SHEM_PRAT shem,S.MISPAR_SIDUR,
         P.GIL,P.EZOR,E.TEUR_EZOR,G.TEUR_KOD_GIL, M.TEUR_MAAMAD_HR,
         S.TAARICH, to_char(S.SHAT_HATCHALA_LETASHLUM,'HH24:mi') SHAT_HATCHALA_LETASHLUM,to_char(S.SHAT_GMAR_LETASHLUM,'HH24:mi') SHAT_GMAR_LETASHLUM,
        round(to_number((S.SHAT_GMAR_LETASHLUM-S.SHAT_HATCHALA_LETASHLUM) *1440)) DAKOT,
             (case when p.gil=0 then 516 else
               case when  p.gil=1 then 444 else 480 end end) michsa
from ovdim o,tb_sidurim_ovdim s,p, ctb_ezor e, ctb_kod_gil g, ctb_maamad m
where o.mispar_ishi=S.MISPAR_ISHI 
    and s.taarich between P_STARTDATE and  P_ENDDATE
    and S.MISPAR_SIDUR=99008
    and S.LO_LETASHLUM=0
    and S.MISPAR_ISHI=p.mispar_ishi
     and s.taarich between  P.me_tarich AND P.ad_tarich
    and P.EZOR= E.KOD_EZOR
    and O.KOD_HEVRA=E.KOD_HEVRA
     and P.MAAMAD= M.KOD_MAAMAD_HR
    and O.KOD_HEVRA=m.KOD_HEVRA
    and P.gil= G.KOD_GIL_HR;
 EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;          
END    pro_rpt_sidur_vaad_ovdim;

  procedure pro_get_ovdim_peilim(P_STARTDATE IN DATE,
                                                            P_ENDDATE IN DATE , 
                                                            p_cur OUT CurType)   as 
begin

OPEN p_cur FOR          
  with pail as
        (select distinct mispar_ishi
        from matzav_ovdim m
        where kod_matzav<'38'
          and   (P_STARTDATE BETWEEN  M.TAARICH_HATCHALA  AND   NVL(M.TAARICH_SIYUM ,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                      OR P_ENDDATE  BETWEEN  m.TAARICH_HATCHALA  AND   NVL(m.TAARICH_SIYUM,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                         OR   m.TAARICH_HATCHALA>=P_STARTDATE AND   NVL(m.TAARICH_SIYUM,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=P_ENDDATE) 
        ),
        pratim as(
            select *
            from(
                select P.MISPAR_ISHI,P.ISUK,P.MAAMAD,P.SNIF_AV,P.EZOR,
                        row_number() OVER (PARTITION BY p.mispar_ishi ORDER BY p.me_tarich desc) seq
                from pivot_pirtey_ovdim p, pail m
                where P.MISPAR_ISHI=m.mispar_ishi
                    and   (P_STARTDATE BETWEEN  p.ME_TARICH  AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                      OR P_ENDDATE  BETWEEN  p.ME_TARICH  AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                         OR   p.ME_TARICH>=P_STARTDATE AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=P_ENDDATE)
                )
            where seq=1
        )
    select p.mispar_ishi,O.SHEM_MISH, O.SHEM_PRAT,EZOR.TEUR_EZOR,MAAMAD.TEUR_MAAMAD_HR,SNIF.TEUR_SNIF_AV,p.isuk, ISUK.TEUR_ISUK
    from pratim p, Ovdim o,  CTB_SNIF_AV Snif ,CTB_MAAMAD   Maamad , CTB_ISUK Isuk ,Ctb_Ezor Ezor
    where p.MISPAR_ISHI = O.MISPAR_ISHI
       and SNIF.KOD_SNIF_AV = p.SNIF_AV
       and SNIF.KOD_HEVRA =O.KOD_HEVRA
       and MAAMAD.KOD_MAAMAD_HR = p.MAAMAD
       and MAAMAD.KOD_HEVRA =O.KOD_HEVRA
       and ISUK.KOD_ISUK = p.ISUK
       and ISUK.KOD_HEVRA=O.KOD_HEVRA
       and EZOR.KOD_EZOR = p.EZOR
       and EZOR.KOD_HEVRA=O.KOD_HEVRA;
      
 end pro_get_ovdim_peilim;
  
procedure pro_get_ovdey_egged_metagberim(P_STARTDATE IN DATE, P_ENDDATE IN DATE ,  p_cur OUT CurType)   as 
--procedure pro_get_tigburim(  p_cur OUT CurType)   as 
 GeneralQry VARCHAR2(32767);
 rc NUMBER ; 
 --P_STARTDATE DATE;
  --P_ENDDATE  DATE;
begin
--P_STARTDATE:=  to_date('30/04/2015','dd/mm/yyyy');
 --P_ENDDATE:= to_date('30/04/2015','dd/mm/yyyy');
       -- GeneralQry:= ' select  distinct  p.MAKAT_NESIA, p.TAARICH  from TB_PEILUT_OVDIM p  where 
       --                (p.taarich BETWEEN TO_DATE( '''    || P_STARTDATE || ''',''dd/MM/yyyy'') AND TO_DATE(''' || P_ENDDATE || ''',''dd/MM/yyyy''))';                      
       -- Pkg_Reports.pro_Prepare_Catalog_Details(GeneralQry);
       EXECUTE IMMEDIATE  'truncate table tmp_Catalog' ;   
        INSERT INTO  kds.TMP_CATALOG_DETAILS@KDS_GW_AT_TNPR(makat8,activity_date)
        select  distinct  p.MAKAT_NESIA, p.TAARICH  from TB_PEILUT_OVDIM p  where 
                       p.taarich BETWEEN P_STARTDATE AND P_ENDDATE;
        kds_catalog_pack.GetKavimDetails@KDS_GW_AT_TNPR(rc);
         INSERT INTO TMP_CATALOG( activity_date,makat8, Shilut,Description,nihul_name,nihul,mazan_tashlum,mazan_tichnun,Km,sug_shirut_name,eilat_trip,onatiut,kisuy_tor,eshel,migun,xy_moked_tchila,xy_moked_siyum,snif,snif_name,sug_auto ) 
            SELECT activity_date, makat8, Shilut,Description,nihul_name,nihul,mazan_tashlum,mazan_tichnun,Km,sug_shirut_name,eilat_trip,onatiut,kisui_tor,eshel,migun,xy_moked_tchila,xy_moked_siyum,snif,snif_name,sug_auto   FROM kds.TMP_CATALOG_DETAILS@KDS_GW_AT_TNPR;
          COMMIT ; 
  
    OPEN p_cur FOR          
     with snifEg as(
        select s.*-- kod_snif_av
        from ctb_snif_av s
        where kod_hevra =580
        and not exists(select 1
                      from ctb_snif_av s1
                      where kod_hevra =4895
                       and s.kod_snif_av=s1.kod_snif_av) ),
      pivot as (  
        SELECT * 
        FROM (  SELECT   t.mispar_ishi , MAX( t.ME_TARICH) OVER (PARTITION BY t.mispar_ishi) me_tarich,
                    t.ad_tarich,t.SNIF_AV,t.EZOR,t.isuk,
                    row_number() OVER (PARTITION BY t.mispar_ishi ORDER BY me_tarich desc) seq
               FROM  pivot_pirtey_ovdim_hist t  --PIVOT_PIRTEY_OVDIM t
               WHERE t.isuk IS NOT NULL 
                 AND (P_STARTDATE    BETWEEN ME_TARICH  AND NVL(t.ad_TARICH,TO_DATE(  '01/01/9999'  ,  'dd/mm/yyyy'  ))
                        OR   P_ENDDATE  BETWEEN ME_TARICH AND NVL(t.ad_TARICH,TO_DATE(  '01/01/9999'  ,  'dd/mm/yyyy'  )) 
                         OR (t.ME_TARICH>= P_STARTDATE AND NVL(t.ad_TARICH,TO_DATE(  '01/01/9999'   ,'dd/mm/yyyy'  ))<=  P_ENDDATE)
                        )   
                 ORDER BY mispar_ishi) a
        WHERE a.seq=1 )    ,            
        ovEg as(
            select o.*,  p.ezor,p.snif_av,p.isuk
            from ovdim_history o ,pivot  p, snifEg s
            where O.KOD_HEVRA=580
               and O.MISPAR_ISHI=p.mispar_ishi
               and P.SNIF_AV = s.kod_snif_av
        )
       ,sidurim as(
             select Pkg_Tnua.fn_get_makat_type(p.makat_nesia) makat_type, 
                   o.mispar_ishi,o.shem_mish || ' ' ||  o.shem_prat fullname, o.kod_hevra,o.ezor,o.snif_av,o.isuk,
                   s.taarich,s.mispar_sidur,P.SHAT_HATCHALA_SIDUR, S.SHAT_GMAR,
                   S.LO_LETASHLUM,P.MAKAT_NESIA,P.MISPAR_KNISA, P.SHAT_YETZIA,P.OTO_NO,nvl(p.snif_tnua,c.snif) snif_tnua, c.km,
                   c.snif snif_catalog, c.nihul,P.KM_VISA
               
             from  ovEg o,tb_sidurim_ovdim s ,tb_peilut_ovdim p,  tmp_catalog c 
             where o.mispar_ishi=s.mispar_ishi
             and s.taarich between P_STARTDATE and P_ENDDATE 
             and s.mispar_ishi=p.mispar_ishi
             and s.taarich= p.taarich
             AND P.MISPAR_KNISA=0
             and s.mispar_sidur=P.MISPAR_SIDUr
             and S.SHAT_HATCHALA = P.SHAT_HATCHALA_SIDUR 
             and p.taarich= C.ACTIVITY_DATE(+)
             and P.MAKAT_NESIA = C.MAKAT8(+) 
             ),
    egedToTavura as(
        select distinct s.mispar_ishi,s.taarich,s.mispar_sidur,s.shat_hatchala_sidur
        from sidurim s,ctb_snif_av st
        where substr(lpad(s.mispar_sidur,5,'0'),0,2) = St.SNIF_TNUA
         and substr(lpad(s.mispar_sidur,5,'0'),0,2) <>'99'
         and St.KOD_HEVRA=4895

        union

        select distinct s.mispar_ishi,s.taarich,s.mispar_sidur,s.shat_hatchala_sidur
        from sidurim s,snifEg se, ctb_snif_av st
        where substr(lpad(s.mispar_sidur,5,'0'),0,2) = se.SNIF_TNUA
         and substr(lpad(s.mispar_sidur,5,'0'),0,2) <>'99'
         and s.makat_type in(1,3)
         and s.snif_tnua = ST.SNIF_TNUA
         and St.KOD_HEVRA=4895

        union 

        select distinct s.mispar_ishi,s.taarich,s.mispar_sidur,s.shat_hatchala_sidur
        from sidurim s
        where s.mispar_sidur between 99901 and 99904


        union 

        select distinct s.mispar_ishi,s.taarich,s.mispar_sidur,s.shat_hatchala_sidur
        from sidurim s, ctb_snif_av st
        where s.mispar_sidur not between 99901 and 99904
            and substr(lpad(s.mispar_sidur,5,'0'),0,2) ='99'
            and s.makat_type in(1,3)
            and s.snif_tnua = ST.SNIF_TNUA
            and St.KOD_HEVRA=4895
            
        union

        select distinct  s.mispar_ishi,s.taarich,s.mispar_sidur,s.shat_hatchala_sidur
        from sidurim s, ctb_snif_av st,PIVOT_VEHICLE_AUDIT v
        where s.mispar_sidur not between 99901 and 99904
            and substr(lpad(s.mispar_sidur,5,'0'),0,2) ='99'
            and s.makat_type in(2,5)
            and s.oto_no= V.VEHICLE_ID
            and to_number(substr(lpad(V.BRANCH2,4,'0'),2,2))=st.SNIF_TNUA
            and  s.taarich between  v.event_date and v.ad_taarich
            and St.KOD_HEVRA=4895 )
 /*,
 a as
(select ST.kod_SNIF_TNUAA  KOD_SNIF,ST.TEUR_SNIF_TNUAA snif_shayachut--, NH.TEUR_SNIF_TNUAA nekudat_tiful
from ctb_snifey_tnuaa st,tmp_catalog c
where ST.KOD_SNIF_TNUAA= c.snif
group by ST.kod_SNIF_TNUAA,ST.TEUR_SNIF_TNUAA), 
b as
(select ST.kod_SNIF_TNUAA  KOD_SNIF,ST.TEUR_SNIF_TNUAA  nekudat_tiful
from ctb_snifey_tnuaa st,tmp_catalog c
where ST.KOD_SNIF_TNUAA= c.nihul
group by ST.kod_SNIF_TNUAA,ST.TEUR_SNIF_TNUAA) ,

pirtey_snif as(
select a.kod_snif, a.snif_shayachut, b.nekudat_tiful
from a join b
on a.kod_snif= b.kod_snif(+)
union 
select b.kod_snif, a.snif_shayachut, b.nekudat_tiful
from a join b
on b.kod_snif= a.kod_snif(+)  )
  */    
     --  ���� ��� �����
    select s.mispar_ishi, s.fullname, H.TEUR_HEVRA, z.TEUR_EZOR,s.isuk,  SA.TEUR_SNIF_AV,I.KOD_ISUK, I.TEUR_ISUK,
            s.taarich  , dayofweek(s.taarich) day ,s.mispar_sidur,
            case when substr(lpad(s.mispar_sidur,5,'0'),0,2) <>'99' then ST.TEUR_SNIF_TNUAA else null end snif_tnua_sidur,
           s.SHAT_HATCHALA_SIDUR, S.SHAT_GMAR,
           S.LO_LETASHLUM,s.MAKAT_NESIA,s.SHAT_YETZIA,s.MISPAR_KNISA, 
            case when makat_type=5 then pkg_elements.getKmByMakatElement(s.makat_nesia,'true') 
                 when makat_type in(1,2,3) then  s.km else s.km_visa end km, 
             s.snif_catalog snif_shayachut_peilut,substr(s.nihul,2,2) snif_tiful_peilut,
            s.OTO_NO, v.LICENSE_NUMBER,v.OWNERSHIP,v.branch1,substr(v.branch1,2,2) snif_shayachut_rechev,v.branch2,substr(v.branch2,2,2) snif_tiful_rechev,
            1 type

           
from sidurim s, egedToTavura e, ctb_hevra h,ctb_snif_av sa, ctb_isuk i, ctb_ezor z ,ctb_snifey_tnuaa st, PIVOT_VEHICLE_AUDIT v
where    s.mispar_ishi=e.mispar_ishi
     and s.taarich= e.taarich
     and s.mispar_sidur=e.MISPAR_SIDUR
     and S.SHAT_HATCHALA_SIDUR = e.SHAT_HATCHALA_SIDUR     
     and s.kod_hevra= h.kod_hevra
     and s.kod_hevra= sa.kod_hevra
     and s.SNIF_AV=sa.kod_snif_av
     and s.kod_hevra= i.kod_hevra
     and s.isuk=i.kod_isuk
     and s.kod_hevra= z.kod_hevra
     and s.ezor=z.kod_ezor
     and substr(lpad(s.mispar_sidur,5,'0'),0,2) = ST.KOD_SNIF_TNUAA(+)
     and s.OTO_NO = V.VEHICLE_ID(+)
     and s.taarich between v.event_date(+) and v.ad_taarich(+)
     order by s.mispar_ishi, s.taarich, S.SHAT_HATCHALA_SIDUR, s.SHAT_YETZIA;
/*union 
-- ��� ��� �����
 select o.mispar_ishi, o.shem_mish || ' ' ||  o.shem_prat fullname,  H.TEUR_HEVRA,z.TEUR_EZOR,t.isuk,
        SA.TEUR_SNIF_AV,I.KOD_ISUK, I.TEUR_ISUK, p.taarich  , dayofweek(p.taarich) day,
        p.mispar_sidur, case when substr(lpad(p.mispar_sidur,5,'0'),0,2) <>'99' then ST.TEUR_SNIF_TNUAA else null end snif_tnua_sidur,
        p.SHAT_HATCHALA_SIDUR, p.SHAT_GMAR, p.LO_LETASHLUM,p.MAKAT_NESIA,p.SHAT_YETZIA,p.MISPAR_KNISA, 
        case when p.makat_type=5 then pkg_elements.getKmByMakatElement(p.makat_nesia,'true') else p.km end km, 
        p.snif_catalog snif_shayachut_peilut,p.nihul snif_tiful_peilut,
        p.OTO_NO, v.LICENSE_NUMBER,v.OWNERSHIP,v.branch1,substr(v.branch1,2,2) snif_shayachut_rechev,v.branch2,substr(v.branch2,2,2) snif_tiful_rechev,
        3 type
from SidurimRechevEgged p, RechvEgedMetagber r,pivot t,ovdim o,
     ctb_hevra h,ctb_snif_av sa, ctb_isuk i, PIVOT_VEHICLE_AUDIT v,ctb_snifey_tnuaa st, ctb_ezor z
where  r.mispar_ishi=p.mispar_ishi
  and r.taarich= p.taarich
  and r.mispar_sidur=p.MISPAR_SIDUR
  and r.SHAT_HATCHALA_SIDUR = p.SHAT_HATCHALA_SIDUR
  and r.mispar_ishi= o.mispar_ishi 
  and o.mispar_ishi= t.mispar_ishi
  and o.kod_hevra= h.kod_hevra
  and o.kod_hevra= sa.kod_hevra
  and t.SNIF_AV=sa.kod_snif_av
  and o.kod_hevra= i.kod_hevra
  and t.isuk=i.kod_isuk
  and o.kod_hevra= z.kod_hevra
  and t.ezor=z.kod_ezor
  and p.OTO_NO = V.VEHICLE_ID(+)
  and p.taarich between v.event_date(+) and v.ad_taarich(+)
  and substr(lpad(r.mispar_sidur,5,'0'),0,2) = ST.KOD_SNIF_TNUAA(+);*/

end pro_get_ovdey_egged_metagberim;


procedure pro_get_rechev_egged_metagber(P_STARTDATE IN DATE, P_ENDDATE IN DATE ,  p_cur OUT CurType)   as 
--procedure pro_get_tigburim(  p_cur OUT CurType)   as 
 GeneralQry VARCHAR2(32767);
 rc NUMBER ; 
 --P_STARTDATE DATE;
  --P_ENDDATE  DATE;
begin
--P_STARTDATE:=  to_date('30/04/2015','dd/mm/yyyy');
 --P_ENDDATE:= to_date('30/04/2015','dd/mm/yyyy');
       -- GeneralQry:= ' select  distinct  p.MAKAT_NESIA, p.TAARICH  from TB_PEILUT_OVDIM p  where 
       --                (p.taarich BETWEEN TO_DATE( '''    || P_STARTDATE || ''',''dd/MM/yyyy'') AND TO_DATE(''' || P_ENDDATE || ''',''dd/MM/yyyy''))';                      
       -- Pkg_Reports.pro_Prepare_Catalog_Details(GeneralQry);
       EXECUTE IMMEDIATE  'truncate table tmp_Catalog' ;   
        INSERT INTO  kds.TMP_CATALOG_DETAILS@KDS_GW_AT_TNPR(makat8,activity_date)
        select  distinct  p.MAKAT_NESIA, p.TAARICH  from TB_PEILUT_OVDIM p  where 
                       p.taarich BETWEEN P_STARTDATE AND P_ENDDATE;
        kds_catalog_pack.GetKavimDetails@KDS_GW_AT_TNPR(rc);
         INSERT INTO TMP_CATALOG( activity_date,makat8, Shilut,Description,nihul_name,nihul,mazan_tashlum,mazan_tichnun,Km,sug_shirut_name,eilat_trip,onatiut,kisuy_tor,eshel,migun,xy_moked_tchila,xy_moked_siyum,snif,snif_name,sug_auto ) 
            SELECT activity_date, makat8, Shilut,Description,nihul_name,nihul,mazan_tashlum,mazan_tichnun,Km,sug_shirut_name,eilat_trip,onatiut,kisui_tor,eshel,migun,xy_moked_tchila,xy_moked_siyum,snif,snif_name,sug_auto   FROM kds.TMP_CATALOG_DETAILS@KDS_GW_AT_TNPR;
          COMMIT ; 
  
    OPEN p_cur FOR          
     with snifEg as(
        select s.*-- kod_snif_av
        from ctb_snif_av s
        where kod_hevra =580
        and not exists(select 1
                      from ctb_snif_av s1
                      where kod_hevra =4895
                       and s.kod_snif_av=s1.kod_snif_av) ),
      pivot as (  
        SELECT * 
        FROM (  SELECT   t.mispar_ishi , MAX( t.ME_TARICH) OVER (PARTITION BY t.mispar_ishi) me_tarich,
                    t.ad_tarich,t.SNIF_AV,t.EZOR,t.isuk,
                    row_number() OVER (PARTITION BY t.mispar_ishi ORDER BY me_tarich desc) seq
               FROM  pivot_pirtey_ovdim_hist t
               WHERE t.isuk IS NOT NULL 
                 AND (P_STARTDATE    BETWEEN ME_TARICH  AND NVL(t.ad_TARICH,TO_DATE(  '01/01/9999'  ,  'dd/mm/yyyy'  ))
                        OR   P_ENDDATE  BETWEEN ME_TARICH AND NVL(t.ad_TARICH,TO_DATE(  '01/01/9999'  ,  'dd/mm/yyyy'  )) 
                         OR (t.ME_TARICH>= P_STARTDATE AND NVL(t.ad_TARICH,TO_DATE(  '01/01/9999'   ,'dd/mm/yyyy'  ))<=  P_ENDDATE)
                        )   
                 ORDER BY mispar_ishi) a
        WHERE a.seq=1 )    ,            
         SidurimRechevEgged as(
          
          select Pkg_Tnua.fn_get_makat_type(p.makat_nesia) makat_type,
                  s.mispar_ishi,s.taarich,s.mispar_sidur,P.SHAT_HATCHALA_SIDUR, S.SHAT_GMAR,
                  S.LO_LETASHLUM,P.MAKAT_NESIA,P.MISPAR_KNISA, P.SHAT_YETZIA,nvl(p.snif_tnua,c.snif) snif_tnua,
                  p.oto_no,c.km, c.snif snif_catalog, c.nihul,p.km_visa
           from  tmp_catalog c,tb_peilut_ovdim p,
                (select  s.mispar_ishi,s.taarich,s.mispar_sidur,S.LO_LETASHLUM,S.SHAT_HATCHALA,S.SHAT_GMAR
                from  tb_sidurim_ovdim s, tb_peilut_ovdim p, PIVOT_VEHICLE_AUDIT v,snifEg se
                where s.taarich between  P_STARTDATE and P_ENDDATE  
                  and s.mispar_ishi=p.mispar_ishi
                  and s.taarich= p.taarich
                  and s.mispar_sidur=P.MISPAR_SIDUR
                  and S.SHAT_HATCHALA = P.SHAT_HATCHALA_SIDUR  
                  and p.oto_no= V.VEHICLE_ID
                  and to_number(substr(lpad(V.BRANCH2,4,'0'),2,2))=se.SNIF_TNUA
                  and  s.taarich between  v.event_date and v.ad_taarich
                  group by s.mispar_ishi,s.taarich,s.mispar_sidur,S.LO_LETASHLUM,S.SHAT_HATCHALA,S.SHAT_GMAR
                  ) s
            where  s.mispar_ishi=p.mispar_ishi
             and s.taarich= p.taarich
             and s.mispar_sidur=P.MISPAR_SIDUR
             AND P.MISPAR_KNISA=0
             and S.SHAT_HATCHALA = P.SHAT_HATCHALA_SIDUR  
             and p.taarich= C.ACTIVITY_DATE(+)
             and P.MAKAT_NESIA = C.MAKAT8(+)   
          
        )
        ,RechvEgedMetagber as
        (
            select s.mispar_ishi,s.taarich,s.mispar_sidur,s.shat_hatchala_sidur,  S.SHAT_GMAR,s.lo_letashlum
            from SidurimRechevEgged s,ctb_snif_av st
            where substr(lpad(s.mispar_sidur,5,'0'),0,2) = St.SNIF_TNUA
             and substr(lpad(s.mispar_sidur,5,'0'),0,2) <>'99'
             and St.KOD_HEVRA=4895

            union

            select s.mispar_ishi,s.taarich,s.mispar_sidur,s.shat_hatchala_sidur, S.SHAT_GMAR,s.lo_letashlum
            from SidurimRechevEgged s,snifEg se, ctb_snif_av st
            where se.SNIF_TNUA is not null
            and  substr(lpad(s.mispar_sidur,5,'0'),0,2) =se.SNIF_TNUA
             and substr(lpad(s.mispar_sidur,5,'0'),0,2) <>'99'
             and s.makat_type in(1,3)
             and s.snif_tnua = ST.SNIF_TNUA
             and St.KOD_HEVRA=4895

            union 

            select s.mispar_ishi,s.taarich,s.mispar_sidur,s.shat_hatchala_sidur, S.SHAT_GMAR,s.lo_letashlum
            from SidurimRechevEgged s
            where s.mispar_sidur between 99901 and 99904


            union 

            select s.mispar_ishi,s.taarich,s.mispar_sidur,s.shat_hatchala_sidur, S.SHAT_GMAR,s.lo_letashlum
            from SidurimRechevEgged s, ctb_snif_av st
            where s.mispar_sidur not between 99901 and 99904
                and substr(lpad(s.mispar_sidur,5,'0'),0,2) ='99'
                and s.makat_type in(1,3)
                and s.snif_tnua = ST.SNIF_TNUA
                and St.KOD_HEVRA=4895
        )/*,
 a as
(select ST.kod_SNIF_TNUAA  KOD_SNIF,ST.TEUR_SNIF_TNUAA snif_shayachut--, NH.TEUR_SNIF_TNUAA nekudat_tiful
from ctb_snifey_tnuaa st,tmp_catalog c
where ST.KOD_SNIF_TNUAA= c.snif
group by ST.kod_SNIF_TNUAA,ST.TEUR_SNIF_TNUAA), 
b as
(select ST.kod_SNIF_TNUAA  KOD_SNIF,ST.TEUR_SNIF_TNUAA  nekudat_tiful
from ctb_snifey_tnuaa st,tmp_catalog c
where ST.KOD_SNIF_TNUAA= c.nihul
group by ST.kod_SNIF_TNUAA,ST.TEUR_SNIF_TNUAA) ,

pirtey_snif as(
select a.kod_snif, a.snif_shayachut, b.nekudat_tiful
from a join b
on a.kod_snif= b.kod_snif(+)
union 
select b.kod_snif, a.snif_shayachut, b.nekudat_tiful
from a join b
on b.kod_snif= a.kod_snif(+)  )
  */    
-- ��� ��� �����
         select o.mispar_ishi, o.shem_mish || ' ' ||  o.shem_prat fullname,  H.TEUR_HEVRA,z.TEUR_EZOR,t.isuk,
                SA.TEUR_SNIF_AV,I.KOD_ISUK, I.TEUR_ISUK, p.taarich  , dayofweek(p.taarich) day,
                p.mispar_sidur, case when substr(lpad(p.mispar_sidur,5,'0'),0,2) <>'99' then ST.TEUR_SNIF_TNUAA else null end snif_tnua_sidur,
                p.SHAT_HATCHALA_SIDUR, p.SHAT_GMAR, p.LO_LETASHLUM,p.MAKAT_NESIA,p.SHAT_YETZIA,p.MISPAR_KNISA, 
                case when p.makat_type=5 then pkg_elements.getKmByMakatElement(p.makat_nesia,'true') 
                 when p.makat_type in(1,2,3) then  p.km else p.km_visa end km, 
                p.snif_catalog snif_shayachut_peilut,substr(p.nihul,2,2) snif_tiful_peilut,
                p.OTO_NO, v.LICENSE_NUMBER,v.OWNERSHIP,v.branch1,substr(v.branch1,2,2) snif_shayachut_rechev,v.branch2,substr(v.branch2,2,2) snif_tiful_rechev,
                3 type
        from SidurimRechevEgged p, RechvEgedMetagber r,pivot t,ovdim_history o,
             ctb_hevra h,ctb_snif_av sa, ctb_isuk i, PIVOT_VEHICLE_AUDIT v,ctb_snifey_tnuaa st, ctb_ezor z
        where  r.mispar_ishi=p.mispar_ishi
          and r.taarich= p.taarich
          and r.mispar_sidur=p.MISPAR_SIDUR
          and r.SHAT_HATCHALA_SIDUR = p.SHAT_HATCHALA_SIDUR
          and r.mispar_ishi= o.mispar_ishi 
          and o.mispar_ishi= t.mispar_ishi
          and o.kod_hevra= h.kod_hevra
          and o.kod_hevra= sa.kod_hevra
          and t.SNIF_AV=sa.kod_snif_av
          and o.kod_hevra= i.kod_hevra
          and t.isuk=i.kod_isuk
          and o.kod_hevra= z.kod_hevra
          and t.ezor=z.kod_ezor
          and p.OTO_NO = V.VEHICLE_ID(+)
          and p.taarich between v.event_date(+) and v.ad_taarich(+)
          and substr(lpad(r.mispar_sidur,5,'0'),0,2) = ST.KOD_SNIF_TNUAA(+)
        order by p.mispar_ishi, p.taarich, p.SHAT_HATCHALA_SIDUR, p.SHAT_YETZIA;

end pro_get_rechev_egged_metagber;






procedure pro_get_cnt_not_signed(p_cur OUT CurType , 
                                 p_Period IN VARCHAR2,
                                 p_ezor IN VARCHAR2,
                                 p_snif IN VARCHAR2,
                                 p_snif_tnua IN VARCHAR2,
                                 p_siba IN VARCHAR2,
                                 p_mispar_ishi IN VARCHAR2 ) IS 
    p_ToDate DATE ; 
    p_FromDate DATE ;  
   -- p_snif VARCHAR2(50);
    GeneralQry VARCHAR2(32767);
  BEGIN
    
      p_FromDate := TO_DATE('01/' || p_Period,'dd/mm/yyyy'); /* period= 05/2009=>  v_MinLimitDate = 01/05/2009 */
      p_ToDate := ADD_MONTHS(p_FromDate,1) -1 ;    /* period= 05/2009=>  v_MaxLimitDate = 31/05/2009 */
    
     -- p_snif := Pkg_Reports.get_First_Snif_Relevant(p_FromDate,p_ToDate,p_ezor);
        
     -- DBMS_OUTPUT.PUT_LINE (p_snif);    
   OPEN p_cur FOR  
       with st as
        (
            select S.KOD_SNIF_AV
            from ctb_snif_av s 
            where( p_snif_tnua is null or S.SNIF_TNUA in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_snif_tnua,  ',') AS MYTABTYPE)))) 
        
        ) ,p as
        (
            SELECT *--,
                 --  pkg_reports.fun_get_snif_tnua_leoved( 
            FROM ( SELECT   t.mispar_ishi , 
                    --    MAX( t.ME_TARICH) OVER (PARTITION BY t.mispar_ishi) me_tarich,
                     t.ad_tarich,t.SNIF_AV,t.EZOR,t.MAAMAD,T.HATZAVA_LEKAV,s.snif_tnua,
                      row_number() OVER (PARTITION BY t.mispar_ishi ORDER BY me_tarich desc) seq
                FROM  PIVOT_PIRTEY_OVDIM t,ctb_snif_av s
                WHERE t.isuk IS NOT NULL 
             --   and t.mispar_ishi= 201
                    and t.ezor=S.EZOR(+)
                    and T.HATZAVA_LEKAV=S.KOD_SNIF_AV(+)
                    AND ( p_FromDate  BETWEEN ME_TARICH  AND NVL(t.ad_TARICH,TO_DATE( '01/01/9999' , 'dd/mm/yyyy' ))
                         OR  p_ToDate           BETWEEN ME_TARICH AND NVL(t.ad_TARICH,TO_DATE('01/01/9999' , 'dd/mm/yyyy' )) 
                         OR t.ME_TARICH>= p_FromDate  AND NVL(t.ad_TARICH,TO_DATE( '01/01/9999' ,'dd/mm/yyyy' ))<=  p_ToDate )
                 ORDER BY mispar_ishi) a
              WHERE a.seq=1
                and (p_ezor is null or a.ezor in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_ezor,  ',') AS MYTABTYPE))))
                and (p_snif is null or a.snif_av in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_snif,  ',') AS MYTABTYPE))))
                and  (p_mispar_ishi is null or a.mispar_ishi in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_mispar_ishi,  ',') AS MYTABTYPE))))
                and( p_snif_tnua is null or ( a.snif_tnua is null and  exists (select 1 from st where a.snif_av= st.KOD_SNIF_AV )
                        or exists (select 1 from st where a.HATZAVA_LEKAV= st.KOD_SNIF_AV ) ) )
                  
        ), so as(
          select h.teur_ezor , S.TEUR_SNIF_TNUAA ,h.snif_tnua ,h.taarich,  count(h.mispar_ishi)cnt
            from CTB_SNIFEY_TNUAA s,(
              SELECT distinct s.mispar_ishi,s.taarich, e.teur_ezor ,
               pkg_reports.fun_get_snif_tnua_leoved(p.snif_av,p.HATZAVA_LEKAV, o.kod_hevra) snif_tnua  --, s.Shat_hitiatzvut,s.Ptor_Mehitiatzvut,s.Kod_SIBA_LEDIVUCH_YADANI_in
              FROM TB_SIDURIM_OVDIM s, TB_YAMEY_AVODA_OVDIM y,p, ctb_snif_av st,ovdim o,ctb_ezor e
              WHERE  p.mispar_ishi  =  s.mispar_ishi 
                 and y.taarich BETWEEN  p_FromDate  AND  p_ToDate
                 and y.mispar_ishi = s.mispar_ishi
                 and y.taarich = s.taarich
                -- and y.status =1
                 and s.lo_letashlum =0            
                 and s.Nidreshet_hitiatzvut > 0 
                   and ( (s.Shat_hitiatzvut IS NULL and ( s.Ptor_Mehitiatzvut IS NULL OR s.Ptor_Mehitiatzvut = 0) and
                            ((nvl(s.Kod_SIBA_LEDIVUCH_YADANI_in ,0)=0) or ( nvl(s.Kod_SIBA_LEDIVUCH_YADANI_in,0) <> 0 and (p_siba is null or 
                                                                           s.Kod_SIBA_LEDIVUCH_YADANI_in in  (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_siba,  ',') AS MYTABTYPE))) ) ) ))
                     or 
                        S.HACHTAMA_BEATAR_LO_TAKIN is not null)
                 and p.snif_av =  ST.KOD_SNIF_AV   
                 and o.mispar_ishi=p.mispar_ishi
                 and o.kod_hevra=ST.KOD_HEVRA 
                 and p.ezor=e.kod_ezor
                 and o.kod_hevra=e.KOD_HEVRA 
               ) h
               where h.SNIF_TNUA>0
               and h.snif_tnua= S.KOD_SNIF_TNUAA
            group by h.teur_ezor ,s.TEUR_SNIF_TNUAA,h.snif_tnua,h.taarich)

        ,t as
        ( select z.teur_ezor, z.TEUR_SNIF_TNUAA, z.SNIF_TNUA
          from(
              select f.teur_ezor, f.TEUR_SNIF_TNUAA, f.SNIF_TNUA,
                     rank() over(partition by f.teur_ezor order by  f.teur_ezor, f.TEUR_SNIF_TNUAA )seq
              from so f) z
          where  z.seq=1
          group by z.teur_ezor, z.TEUR_SNIF_TNUAA, z.SNIF_TNUA)
             
         SELECT t.teur_ezor, t.TEUR_SNIF_TNUAA, t.SNIF_TNUA,  TO_DATE(d.x,'dd/mm/yyyy') Taarich, 0  cnt 
         FROM (TABLE(CAST(Convert_String_To_Table(String_Dates_Of_Period(p_Period),',') AS mytabtype))) d, t
                --    ( select so.SNIF_TNUA,so.TEUR_SNIF_TNUAA,so.teur_ezor from so where rownum=1 ) f
         where not exists(select 1 from so where so.teur_ezor=t.teur_ezor and so.SNIF_TNUA = t.SNIF_TNUA and so.Taarich= TO_DATE(d.x,'dd/mm/yyyy'))

     union
         
        select * from so;
            
 
           EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
end pro_get_cnt_not_signed;

function fun_get_snif_tnua_leoved(p_snif_av in number,p_hazava_lekav in number, p_kod_hevra in number) return number is
  p_snif_tnua number;
--  teur_snif varchar(50);
begin
        p_snif_tnua:=0;
         if(p_hazava_lekav is not null) then
            select nvl(s.snif_tnua,0) into p_snif_tnua
                 from  ctb_snif_av s
                 where s.kod_SNIF_AV= p_hazava_lekav
                 and s.kod_hevra =p_kod_hevra;
        end if;
        
          if(p_snif_tnua =0 ) then
             select nvl(s.snif_tnua,0) into p_snif_tnua
             from  ctb_snif_av s
             where s.kod_SNIF_AV= p_snif_av
             and s.kod_hevra =p_kod_hevra;
        end if;
       
   return  p_snif_tnua;  
   
   EXCEPTION 
    -- WHEN no_data_found THEN
     --return '';
        WHEN OTHERS THEN 
            RAISE;    
end fun_get_snif_tnua_leoved;
 

    
procedure pro_get_ovdim_not_signed(p_cur OUT CurType , 
                                 p_Period IN VARCHAR2,
                                 p_ezor IN VARCHAR2,
                                 p_snif IN VARCHAR2,
                                 p_snif_tnua IN VARCHAR2,
                                 p_siba IN VARCHAR2,
                                 p_mispar_ishi IN VARCHAR2 ) IS 
    p_ToDate DATE ; 
    p_FromDate DATE ;  
    p_Erech TB_PARAMETRIM.erech_param%TYPE ;
BEGIN
  
      p_FromDate := TO_DATE('01/' || p_Period,'dd/mm/yyyy'); /* period= 05/2009=>  v_MinLimitDate = 01/05/2009 */
      p_ToDate := ADD_MONTHS(p_FromDate,1) -1 ;    /* period= 05/2009=>  v_MaxLimitDate = 31/05/2009 */
     Pkg_Utils.Pro_Get_Value_From_Parametrim(147,p_period,p_Erech);
    
     -- DBMS_OUTPUT.PUT_LINE (p_snif);    
   OPEN p_cur FOR  
       with st as
        (
            select S.KOD_SNIF_AV
            from ctb_snif_av s 
            where( p_snif_tnua is null or S.SNIF_TNUA in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_snif_tnua,  ',') AS MYTABTYPE)))) 
        
        ) ,p as
        (
            SELECT * 
            FROM ( SELECT   t.mispar_ishi , 
                    --    MAX( t.ME_TARICH) OVER (PARTITION BY t.mispar_ishi) me_tarich,
                     t.ad_tarich,t.SNIF_AV,t.EZOR,t.MAAMAD,T.HATZAVA_LEKAV,S.SNIF_TNUA,
                      row_number() OVER (PARTITION BY t.mispar_ishi ORDER BY me_tarich desc) seq
                FROM  PIVOT_PIRTEY_OVDIM t,ctb_snif_av s
                WHERE t.isuk IS NOT NULL 
                   and t.ezor=S.EZOR(+)
                    and T.HATZAVA_LEKAV=S.KOD_SNIF_AV(+)
             --   and t.mispar_ishi= 201
                    AND ( p_FromDate  BETWEEN ME_TARICH  AND NVL(t.ad_TARICH,TO_DATE( '01/01/9999' , 'dd/mm/yyyy' ))
                         OR  p_ToDate  BETWEEN ME_TARICH AND NVL(t.ad_TARICH,TO_DATE('01/01/9999' , 'dd/mm/yyyy' )) 
                         OR t.ME_TARICH>= p_FromDate  AND NVL(t.ad_TARICH,TO_DATE( '01/01/9999' ,'dd/mm/yyyy' ))<=  p_ToDate )
                 ORDER BY mispar_ishi) a
              WHERE a.seq=1
                and (p_ezor is null or a.ezor in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_ezor,  ',') AS MYTABTYPE))))
                and (p_snif is null or a.snif_av in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_snif,  ',') AS MYTABTYPE))))
                and  (p_mispar_ishi is null or a.mispar_ishi in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_mispar_ishi,  ',') AS MYTABTYPE))))
                and( p_snif_tnua is null OR ( a.snif_tnua is null and  exists (select 1 from st where a.snif_av= st.KOD_SNIF_AV )
                        or (  exists (select 1 from st where a.HATZAVA_LEKAV= st.KOD_SNIF_AV )) ) )
                  
        ),
      SidureyHityazvut as
         (
            select s.mispar_ishi,s.taarich,s.shat_hatchala, Y.TEUR_MIKUM_YECHIDA
            from tb_sidurim_ovdim s,p,ctb_mikum_yechida y
            where s.mispar_sidur=99200
                and s.mispar_ishi= p.mispar_ishi
                and s.taarich BETWEEN p_FromDate and p_ToDate
                and TO_NUMBER(SUBSTR(LPAD( s.MIKUM_SHAON_KNISA    , 5, '0'),0,3)) = Y.KOD_MIKUM_YECHIDA
         )
     
   select s.*, s1.teur_snif_av  , s2.teur_snif_av snif_giyus, ST.TEUR_SNIF_TNUAA,
           e.teur_ezor,h.shat_hatchala shat_hityazvut, h.TEUR_MIKUM_YECHIDA,
             
         case when (mispar_sidur1 is not null) then rank() over(partition by s.MISPAR_ISHI,s.taarich order by h.shat_hatchala) else 0 end seq1,
         case when (mispar_sidur2 is not null) then rank() over(partition by s.MISPAR_ISHI,s.taarich order by h.shat_hatchala) else 0 end seq2
   from   ctb_snif_av s1, ctb_snif_av s2,ctb_ezor e,SidureyHityazvut h,CTB_SNIFEY_TNUAA st,
        (select so.MISPAR_ISHI,so.taarich  ,so.snif_av,so.ezor, so.HATZAVA_LEKAV,so.full_name,so.kod_hevra,so.snif_tnua,
              MAX(CASE WHEN  so.nidreshet_hitiatzvut =1 THEN so.mispar_sidur    ELSE null END)    mispar_sidur1,
              MAX(CASE WHEN  so.nidreshet_hitiatzvut =1 THEN so.shat_hatchala    ELSE null END)    StartTime1,
              MAX(CASE WHEN  so.nidreshet_hitiatzvut =1 THEN so.shat_gmar    ELSE null END)    EndTime1,
              MAX(CASE WHEN  so.nidreshet_hitiatzvut =1 THEN so.kod_siba    ELSE null END)    KodSiba1,
              MAX(CASE WHEN  so.nidreshet_hitiatzvut =2 THEN so.shat_hatchala    ELSE null END)    StartTime2,
              MAX(CASE WHEN  so.nidreshet_hitiatzvut =2 THEN so.shat_gmar    ELSE null END)    EndTime2 ,
              MAX(CASE WHEN  so.nidreshet_hitiatzvut =2 THEN so.mispar_sidur    ELSE null END)    mispar_sidur2,
              MAX(CASE WHEN  so.nidreshet_hitiatzvut =2 THEN so.kod_siba    ELSE null END)    KodSiba2
       from (
             SELECT A.taarich,A.mispar_ishi,o.shem_prat || ' ' ||  o.shem_mish full_name,A.mispar_sidur,A.nidreshet_hitiatzvut,A.SHAT_GMAR,A.SHAT_HATCHALA  ,
                    A.kod_siba,A.snif_av,A.ezor, A.HATZAVA_LEKAV,o.kod_hevra,
                  pkg_reports.fun_get_snif_tnua_leoved(A.snif_av,A.HATZAVA_LEKAV, o.kod_hevra) snif_tnua
             
             from ovdim o,(     
                  SELECT s.mispar_ishi,s.taarich, p.snif_av,p.ezor, p.HATZAVA_LEKAV,
                         s.mispar_sidur,s.nidreshet_hitiatzvut,s.SHAT_GMAR,s.SHAT_HATCHALA,
                         s.KOD_SIBA_LEDIVUCH_YADANI_IN   kod_siba,
                          COUNT(s.mispar_ishi)  OVER (PARTITION BY  s.mispar_ishi) num  
                  FROM TB_SIDURIM_OVDIM s, TB_YAMEY_AVODA_OVDIM y,p
                  WHERE  p.mispar_ishi  =  s.mispar_ishi 
                     and y.taarich BETWEEN p_FromDate  AND p_ToDate
                     and y.mispar_ishi = s.mispar_ishi
                     and y.taarich = s.taarich
                   --  and y.status =1
                     and s.lo_letashlum =0            
                     and s.Nidreshet_hitiatzvut > 0 
                     and ( (s.Shat_hitiatzvut IS NULL and ( s.Ptor_Mehitiatzvut IS NULL OR s.Ptor_Mehitiatzvut = 0) and
                            ((nvl(s.Kod_SIBA_LEDIVUCH_YADANI_in ,0)=0) or ( nvl(s.Kod_SIBA_LEDIVUCH_YADANI_in,0) <> 0 and (p_siba is null or 
                                                                           s.Kod_SIBA_LEDIVUCH_YADANI_in in  (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_siba,  ',') AS MYTABTYPE))) ) ) ))
                     or 
                        S.HACHTAMA_BEATAR_LO_TAKIN is not null)
                    -- and ( (s.Shat_hitiatzvut IS NULL AND (s.Ptor_Mehitiatzvut IS NULL OR s.Ptor_Mehitiatzvut = 0))
                    --     OR(s.Shat_hitiatzvut IS NOT NULL and S.HACHTAMA_BEATAR_LO_TAKIN is not null AND 
                     --    ( p_siba is null or s.Kod_SIBA_LEDIVUCH_YADANI_in  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_siba,  ',') AS MYTABTYPE)))  )   ))
                   ) A
               where a.num>p_Erech
               and o.mispar_ishi= A.mispar_ishi ) so
        group by  so.MISPAR_ISHI,so.taarich ,so.snif_av,so.ezor ,so.HATZAVA_LEKAV, so.full_name,so.kod_hevra,so.snif_tnua ) s
    where  s1.KOD_SNIF_AV= s.snif_av
      and s1.kod_hevra=s.kod_hevra
      and s2.KOD_SNIF_AV(+)= s.HATZAVA_LEKAV
      and s2.kod_hevra(+)=s.kod_hevra
      and s.ezor= e.kod_ezor
      and e.kod_hevra=s.kod_hevra
      and s.mispar_ishi = h.mispar_ishi(+)
      and s.taarich = h.taarich(+)
      and s.SNIF_TNUA>0
       and s.snif_tnua= St.KOD_SNIF_TNUAA;
        
 
       EXCEPTION 
         WHEN OTHERS THEN 
           RAISE;          
end pro_get_ovdim_not_signed;
END Pkg_Reports;
/

CREATE OR REPLACE PACKAGE BODY          Pkg_Utils AS
/******************************************************************************
   NAME:       PKG_UTILS
   PURPOSE:

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        26/04/2009             1. Created this package.
******************************************************************************/

PROCEDURE MoveRecordsToHistory 
IS
     param NUMBER;
	fromDate DATE;
	toDate DATE;
	 err_code NUMBER;
	  err_msg VARCHAR2(100);
  BEGIN
DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Utils',action_name => 'MoveRecordsToHistory');
  SELECT SM.ERECH_PARAM INTO param
  FROM TB_PARAMETRIM SM
  WHERE SM.KOD_PARAM =100   AND
	   SYSDATE BETWEEN SM.ME_TAARICH AND SM.AD_TAARICH;
	   
  SELECT ADD_MONTHS(TRUNC(SYSDATE, 'MM'),param*-1) INTO fromDate FROM dual;
   SELECT  LAST_DAY(fromDate) INTO toDate FROM dual;


		INSERT  INTO HISTORY_YAMEY_AVODA_OVDIM
        -- 2013/08/20 (MISPAR_ISHI,TAARICH,SHAT_HATCHALA,SHAT_SIYUM,TACHOGRAF,BITUL_ZMAN_NESIOT,ZMAN_NESIA_HALOCH,ZMAN_NESIA_HAZOR,HALBASHA,LINA,HASHLAMA_LEYOM,SIBAT_HASHLAMA_LEYOM,STATUS,KOD_HISTAYGUT_AUTO,MEASHER_O_MISTAYEG,STATUS_TIPUL,MEADKEN_ACHARON,TAARICH_IDKUN_ACHARON,HEARA,HAMARAT_SHABAT)
		--  2013/08/20 (
         SELECT *
        -- 2013/08/20 MISPAR_ISHI,TAARICH,SHAT_HATCHALA,SHAT_SIYUM,TACHOGRAF,BITUL_ZMAN_NESIOT,ZMAN_NESIA_HALOCH,ZMAN_NESIA_HAZOR,HALBASHA,LINA,HASHLAMA_LEYOM,SIBAT_HASHLAMA_LEYOM,STATUS,KOD_HISTAYGUT_AUTO,MEASHER_O_MISTAYEG,STATUS_TIPUL,MEADKEN_ACHARON,TAARICH_IDKUN_ACHARON,HEARA,HAMARAT_SHABAT
		  FROM TB_YAMEY_AVODA_OVDIM
		  WHERE Taarich BETWEEN fromDate AND toDate
        -- 2013/08/20  )
        ;
  
  
		INSERT INTO HISTORY_SIDURIM_OVDIM
        -- 2013/08/20(MISPAR_ISHI,MISPAR_SIDUR,TAARICH,SHAT_HATCHALA,SHAT_GMAR,SHAT_HATCHALA_LETASHLUM,SHAT_GMAR_LETASHLUM,PITZUL_HAFSAKA,CHARIGA,TOSEFET_GRIRA,HASHLAMA,YOM_VISA,LO_LETASHLUM,OUT_MICHSA,MIKUM_SHAON_KNISA,MIKUM_SHAON_YETZIA,BITUL_O_HOSAFA,ACHUZ_KNAS_LEPREMYAT_VISA,ACHUZ_VIZA_BESIKUN,tafkid_visa,MISPAR_MUSACH_O_MACHSAN,KOD_SIBA_LO_LETASHLUM,KOD_SIBA_LEDIVUCH_YADANI_IN,KOD_SIBA_LEDIVUCH_YADANI_OUT,SHAYAH_LEYOM_KODEM,MEADKEN_ACHARON,TAARICH_IDKUN_ACHARON,HEARA,MISPAR_SHIUREY_NEHIGA,MEZAKE_HALBASHA,MEZAKE_NESIOT,MIVTZA_VISA,SUG_HAZMANAT_VISA,NIDRESHET_HITIATZVUT,SHAT_HITIATZVUT,PTOR_MEHITIATZVUT,MENAHEL_MUSACH_MEADKEN)
		-- 2013/08/20 (
         SELECT *
         -- 2013/08/20MISPAR_ISHI,MISPAR_SIDUR,TAARICH,SHAT_HATCHALA,SHAT_GMAR,SHAT_HATCHALA_LETASHLUM,SHAT_GMAR_LETASHLUM,PITZUL_HAFSAKA,CHARIGA,TOSEFET_GRIRA,HASHLAMA,YOM_VISA,LO_LETASHLUM,OUT_MICHSA,MIKUM_SHAON_KNISA,MIKUM_SHAON_YETZIA,BITUL_O_HOSAFA,ACHUZ_KNAS_LEPREMYAT_VISA,ACHUZ_VIZA_BESIKUN,tafkid_visa,MISPAR_MUSACH_O_MACHSAN,KOD_SIBA_LO_LETASHLUM,KOD_SIBA_LEDIVUCH_YADANI_IN,KOD_SIBA_LEDIVUCH_YADANI_OUT,SHAYAH_LEYOM_KODEM,MEADKEN_ACHARON,TAARICH_IDKUN_ACHARON,HEARA,MISPAR_SHIUREY_NEHIGA,MEZAKE_HALBASHA,MEZAKE_NESIOT,MIVTZA_VISA,SUG_HAZMANAT_VISA,NIDRESHET_HITIATZVUT,SHAT_HITIATZVUT,PTOR_MEHITIATZVUT,MENAHEL_MUSACH_MEADKEN
		  FROM TB_SIDURIM_OVDIM
		  WHERE Taarich BETWEEN fromDate AND toDate
         -- 2013/08/20 )
          ;
  
		   INSERT INTO HISTORY_PEILUT_OVDIM
           -- 2013/08/20(MISPAR_ISHI,TAARICH,MISPAR_SIDUR,SHAT_HATCHALA_SIDUR,SHAT_YETZIA,MISPAR_KNISA,MAKAT_NESIA,OTO_NO,MISPAR_SIDURI_OTO,KISUY_TOR,BITUL_O_HOSAFA,KOD_SHINUY_PREMIA,SNIF_TNUA,MISPAR_VISA,IMUT_NETZER,SHAT_BHIRAT_NESIA_NETZER,OTO_NO_NETZER,MISPAR_SIDUR_NETZER,SHAT_YETZIA_NETZER,MAKAT_NETZER,SHILUT_NETZER,MIKUM_BHIRAT_NESIA_NETZER,MISPAR_MATALA,TAARICH_IDKUN_ACHARON,MEADKEN_ACHARON,HEARA,DAKOT_BAFOAL,KM_VISA)
		 -- 2013/08/20(
         -- 20140324: due to new shilut,  write all fields!!
         (MISPAR_ISHI   ,  TAARICH      ,  MISPAR_sidur  ,  Shat_hatchala_sidur ,  Shat_yetzia     ,  Mispar_knisa    ,  
Makat_nesia     ,  Oto_no    ,  Mispar_siduri_oto  ,  Kisuy_tor    ,  Bitul_O_Hosafa,  Kod_shinuy_premia ,  
Snif_tnua     ,  Mispar_visa    ,  Imut_netzer    ,  Shat_Bhirat_Nesia_Netzer ,  Oto_No_Netzer   ,  
Mispar_Sidur_Netzer ,  Shat_yetzia_Netzer   ,  Makat_Netzer    ,  Shilut_Netzer   ,  Mispar_matala  ,  
Dakot_bafoal  ,  Km_visa   ,  TAARICH_IDKUN_ACHARON ,  MEADKEN_ACHARON    ,  Mikum_Bhirat_Nesia_Netzer ,  
heara     ,  Teur_Nesia ,  Shat_yetzia_Mekorit  )--  ,   Shilut
         SELECT MISPAR_ISHI   ,  TAARICH      ,  MISPAR_sidur  ,  Shat_hatchala_sidur ,  Shat_yetzia     ,  Mispar_knisa    ,  
Makat_nesia     ,  Oto_no    ,  Mispar_siduri_oto  ,  Kisuy_tor    ,  Bitul_O_Hosafa,  Kod_shinuy_premia ,  
Snif_tnua     ,  Mispar_visa    ,  Imut_netzer    ,  Shat_Bhirat_Nesia_Netzer ,  Oto_No_Netzer   ,  
Mispar_Sidur_Netzer ,  Shat_yetzia_Netzer   ,  Makat_Netzer    ,  Shilut_Netzer   ,  Mispar_matala  ,  
Dakot_bafoal  ,  Km_visa   ,  TAARICH_IDKUN_ACHARON ,  MEADKEN_ACHARON    ,  Mikum_Bhirat_Nesia_Netzer ,  
heara     ,  Teur_Nesia ,  Shat_yetzia_Mekorit  --*
         -- 2013/08/20MISPAR_ISHI,TAARICH,MISPAR_SIDUR,SHAT_HATCHALA_SIDUR,SHAT_YETZIA,MISPAR_KNISA,MAKAT_NESIA,OTO_NO,MISPAR_SIDURI_OTO,KISUY_TOR,BITUL_O_HOSAFA,KOD_SHINUY_PREMIA,SNIF_TNUA,MISPAR_VISA,IMUT_NETZER,SHAT_BHIRAT_NESIA_NETZER,OTO_NO_NETZER,MISPAR_SIDUR_NETZER,SHAT_YETZIA_NETZER,MAKAT_NETZER,SHILUT_NETZER,MIKUM_BHIRAT_NESIA_NETZER,MISPAR_MATALA,TAARICH_IDKUN_ACHARON,MEADKEN_ACHARON,HEARA,DAKOT_BAFOAL,KM_VISA
		  FROM TB_PEILUT_OVDIM
		  WHERE Taarich BETWEEN fromDate AND toDate
          -- 2013/08/20)
          ;
  

		  DELETE  FROM TB_PEILUT_OVDIM
		  WHERE Taarich BETWEEN fromDate AND toDate;
		 
		  DELETE FROM TB_SIDURIM_OVDIM
		  WHERE Taarich BETWEEN fromDate AND toDate;
 
		  DELETE FROM TB_YAMEY_AVODA_OVDIM
		  WHERE Taarich BETWEEN fromDate AND toDate;
	  -- delete trails
	 	  DELETE  FROM TRAIL_PEILUT_OVDIM
    	  WHERE Taarich BETWEEN fromDate AND toDate;
		 
		  DELETE FROM TRAIL_SIDURIM_OVDIM
          WHERE Taarich BETWEEN fromDate AND toDate;
 
		  DELETE FROM TRAIL_YAMEY_AVODA_OVDIM
          WHERE Taarich BETWEEN fromDate AND toDate;
          
          DELETE FROM TB_MEADKEN_ACHARON
          WHERE Taarich BETWEEN fromDate AND toDate;
          
          DELETE FROM TB_SHGIOT_MEUSHAROT
          WHERE Taarich BETWEEN fromDate AND toDate;
	--	   commit;	  
		EXCEPTION
		        WHEN OTHERS THEN
				RAISE;
			--	BEGIN
					-- ROLLBACK;
						-- err_msg := substr(SQLERRM, 1, 200);
					  --   PKG_BATCH.pro_ins_log_tahalich(99,0,3,  err_msg);
					     ---commit;	  
				-- END;
 END MoveRecordsToHistory;

/*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        27/04/2009      vered       1. ????? ????? ????? ?????
*/
PROCEDURE pro_get_ezorim(p_Cur OUT CurType)
IS
	 BEGIN
     OPEN p_Cur FOR
       SELECT code,description
       FROM (
             SELECT  DISTINCT aa.kod_ezor code, kod_ezor || ' - ' ||  aa.teur_ezor description,aa.teur_ezor
             FROM CTB_EZOR aa)
       ORDER BY teur_ezor;

	 EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END pro_get_ezorim;


PROCEDURE pro_get_snif_av(p_kod_ezor IN CTB_EZOR.kod_ezor%TYPE,p_cur OUT CurType)
IS
    BEGIN
     OPEN p_Cur FOR
         SELECT a.Kod_Snif_Av code, a.Teur_Snif_Av  || ' (' ||  a.Kod_Snif_Av || ') '  || c.teur_hevra || ' ('  ||  b.kod_hevra || ')'    Description
         FROM  CTB_SNIF_AV a, CTB_EZOR b, CTB_HEVRA c
         WHERE a.ezor=b.kod_ezor
               AND (a.ezor=p_kod_ezor OR p_kod_ezor IS NULL)
               AND a.kod_hevra = c.kod_hevra
               AND a.ezor=b.kod_ezor
               AND c.KOD_HEVRA=b.kod_hevra
         ORDER BY a.Teur_Snif_Av ASC ;

	 EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END pro_get_snif_av;

/*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0       03/05/2009      sari       1. ����� ����� ��������
*/
PROCEDURE pro_get_profil(p_Cur OUT CurType)
IS
	 BEGIN
     OPEN p_Cur FOR
      	  SELECT kod_profil,teur_profil FROM CTB_PROFIL;

	 EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END pro_get_profil;


/*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0       03/05/2009      sari       1. ����� �����  ������ �������
*/
PROCEDURE pro_get_harshaot_to_profil(p_kod_profil IN  TB_HARSHAOT_MASACHIM.KOD_PROFIL%TYPE ,
		  										  	  							   p_Cur OUT CurType)
IS
	 BEGIN
     OPEN p_Cur FOR
      	  SELECT  m.sug,m.shem,h.kod_harshaa, m.masach_id
			FROM  TB_MASACH m, TB_HARSHAOT_MASACHIM h
			WHERE m.masach_id=h.masach_id
			AND m.pakad_id=h.pakad_id
			AND h.kod_profil=p_kod_profil;

	 EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END pro_get_harshaot_to_profil;

PROCEDURE pro_get_maamad(p_kod_hevra IN CTB_MAAMAD.kod_hevra%TYPE, p_cur OUT CurType) IS
BEGIN
     OPEN p_Cur FOR
          SELECT m.kod_hevra || '-' || m.kod_maamad_hr code, m.teur_maamad_hr  || ' (' || m.kod_maamad_hr || ') '   || c.teur_hevra || ' (' || c.kod_hevra || ')'  Description
          FROM  CTB_MAAMAD m, CTB_HEVRA c
          WHERE (m.kod_hevra=p_kod_hevra OR p_kod_hevra IS NULL)
                AND m.kod_hevra= c.kod_hevra
          ORDER BY m.teur_maamad_hr;
         /*SELECT distinct a.kod_maamad_hr code, a.teur_maamad_hr  || ' ' || a.kod_maamad_hr || ' '   || c.teur_hevra || ' ' || c.kod_hevra  Description
         FROM  ctb_maamad a,pirtey_ovdim o , ctb_hevra c
         WHERE c.kod_hevra = a.kod_hevra
               and exists (select distinct m.mispar_ishi
                           from pirtey_ovdim  m
                           where m.kod_natun=4
                                 and m.erech=p_kod_snif and m.mispar_ishi=o.mispar_ishi)
         AND o.kod_natun =13
         AND o.erech =a.kod_maamad_hr

         ORDER BY a.kod_maamad_hr;*/


	 EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END pro_get_maamad;

/*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0       0405/2009      sari       1. ����� �����  ������ �������
*/
PROCEDURE pro_get_hodaot_to_profil(p_kod_masach  IN TB_HODAOT.MASACH_ID%TYPE ,
		  									   		   		                  p_kod_profil IN  TB_HARSHAOT_MASACHIM.KOD_PROFIL%TYPE ,
		  										  	  							   p_Cur OUT CurType)
IS
	 BEGIN
     OPEN p_Cur FOR
      	 SELECT h. Melel_Hodaa,h.Kod_Hodaa
		FROM TB_HODAOT h,  TB_HODAOT_LEPROFIL hp
		WHERE h.Kod_Hodaa= hp.Kod_Hodaa
		AND h.MASACH_ID = p_kod_masach
		AND hp.Kod_Profil = p_kod_profil
		AND h. Me_Taarich <= TRUNC(SYSDATE)
		AND h. Ad_Taarich >= TRUNC(SYSDATE);


	 EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END pro_get_hodaot_to_profil;

PROCEDURE pro_get_error_ovdim(p_kod_snif IN CTB_SNIF_AV.kod_snif_av%TYPE, p_kod_maamad IN CTB_MAAMAD.kod_maamad_hr%TYPE,
                              p_from_date IN DATE, p_to_date IN DATE,
                              p_cur OUT CurType)
IS
     BEGIN
     OPEN p_Cur FOR
      	 SELECT DISTINCT m1.mispar_ishi
         FROM PIRTEY_OVDIM  m1, PIRTEY_OVDIM m2,TB_YAMEY_AVODA_OVDIM l

         WHERE m1.kod_natun=4
               AND m1.erech=p_kod_snif
               AND m2.KOD_NATUN=13
               AND m2.erech= p_kod_maamad

               AND m1.mispar_ishi=m2.mispar_ishi
               AND m1.mispar_ishi=l.mispar_ishi
               AND l.taarich BETWEEN p_from_date AND p_to_date;


	 EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END pro_get_error_ovdim;


 /*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0       0505/2009      sari       1. ����� ����� ��� �������
*/
PROCEDURE pro_get_log_tahalich(p_Cur OUT CurType)
IS
	 BEGIN
     DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Utils',action_name => 'pro_get_log_tahalich');
 	 	  OPEN p_Cur FOR
      	  	   SELECT k.Teur_Tahalich,p.Teur_Peilut_Be_Tahalich,
				l.Taarich,t.Teur_Takala,l.Teur_Tech
				FROM CTB_TAHALICH_KLITA k, CTB_PEILUT_BETAHALICH p, TB_LOG_TAHALICH l, CTB_TAKALOT t
				WHERE l. Status=3
				AND l.Kod_Tahalich = k.Kod_Tahalich
				AND k.Kod_Tahalich = p.Kod_Tahalich(+)
				AND p.Kod_Peilut_Be_Tahalich = l.Kod_Peilut_Tahalich(+)
				AND l.Kod_Takala = t.Kod_Takala(+)
				AND ROWNUM=0
				ORDER BY l.Taarich DESC;

	 EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END pro_get_log_tahalich;

 /*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0       02/06/2009      sari       1. ����� ������ ������ �����
*/
PROCEDURE pro_get_etz_nihuly_by_user(p_prefix IN VARCHAR2, p_mispar_ishi IN NUMBER, p_cur OUT CurType) IS
    p_yechida varchar2(6);
    p_cnt number;
BEGIN
 DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Utils',action_name => 'pro_get_etz_nihuly_by_user');
             
             /*   BEGIN
                    SELECT  ERECH into p_yechida
                    FROM PIRTEY_OVDIM
                    WHERE MISPAR_ISHI=p_mispar_ishi  
                         AND KOD_NATUN=1
                         AND TRUNC(SYSDATE)>= ME_TAARICH AND TRUNC(SYSDATE)<=AD_TAARICH ;
                 EXCEPTION
                               WHEN NO_DATA_FOUND  THEN
                                 p_yechida:=0;
                END;*/
   
    /*        SELECT  count(*) into p_cnt
            FROM KDSADMIN.EZ_NIHULY E
                CONNECT BY     E.YECHIDAT_ABA = PRIOR E.YECHIDA_MEKORIT
                START WITH     ( E.YECHIDAT_ABA = p_yechida      OR E.YECHIDA_MEKORIT = p_yechida);

   if  (p_cnt <=1) then
    OPEN p_Cur FOR
        SELECT  DISTINCT e.yechidat_aba,e.yechida_mekorit,
                        o.shem_mish || ' ' ||  o.shem_prat  || ' (' || o.mispar_ishi || ')' Oved_Name,o.mispar_ishi
           FROM kdsadmin.EZ_NIHULY E, kdsadmin.OVDIM O
           WHERE E.YECHIDA_MEKORIT=p_yechida
            and O.MISPAR_ISHI = p_mispar_ishi
         ORDER BY mispar_ishi;
   ELSE */
  

    -- OPEN p_Cur FOR
     --       WITH NIHULY AS (SELECT
      --                               EN.YECHIDAT_ABA, EN.YECHIDA_MEKORIT
       --                     FROM         KDSADMIN.EZ_NIHULY EN
        --                    CONNECT BY     EN.YECHIDAT_ABA = PRIOR EN.YECHIDA_MEKORIT
         --                   START WITH     EN.yechida_mekorit = p_yechida ) --    97709
        --   SELECT   /*+ use_nl (e po o) */
         --             E.YECHIDAT_ABA, E.YECHIDA_MEKORIT,
          --             O.SHEM_MISH || ' ' || O.SHEM_PRAT || ' (' || O.MISPAR_ISHI || ')'
           --                OVED_NAME, O.MISPAR_ISHI
          --  FROM       KDSADMIN.PIRTEY_OVDIM PO, NIHULY E, KDSADMIN.OVDIM O
           -- WHERE           TRUNC ( SYSDATE ) >= PO.ME_TAARICH
            --           AND TRUNC ( SYSDATE ) <= PO.AD_TAARICH
             --          AND PO.KOD_NATUN = 1
              --         AND PO.ERECH = E.YECHIDA_MEKORIT
               --        AND O.MISPAR_ISHI = PO.MISPAR_ISHI
                --       AND po.MISPAR_ISHI LIKE p_prefix
        --     ORDER BY   mispar_ishi;
        
        
   p_yechida:= fun_get_yechida(p_mispar_ishi);       
   open p_cur for     --������
  -- select  distinct yechidat_aba,yechida_mekorit, oved_name,mispar_ishi                       
    select  distinct oved_name,mispar_ishi 
    from (                          
            select * from table(fun_get_manager_employees(p_prefix,p_yechida))
            union       
            select * from table(fun_get_isuk_harshaot(p_prefix,p_mispar_ishi))
            union          
            select * from table(fun_get_manager_emp_by_maamad(p_prefix, p_mispar_ishi)))
     order by mispar_ishi;
        
   --  fun_get_ez_nihuli_mispar_ishi
    /*    (select ' ' YECHIDAT_ABA,  ' ' YECHIDA_MEKORIT , O.SHEM_MISH || ' ' ||  O.SHEM_PRAT OVED_NAME, O.Mispar_ishi
        from 
        pivot_pirtey_ovdim po, ovdim o 
        where o.mispar_ishi = po.mispar_ishi
        and sysdate between PO.ME_TARICH and PO.AD_TARICH
        and  po.MISPAR_ISHI LIKE p_prefix
        and exists (
                        select h.kod_darga, h.kod_ezor 
                        from tb_harshaot_idkun h 
                        where exists  (select PO.ISUK, PO.YECHIDA_IRGUNIT
                                         ++    from pivot_pirtey_ovdim po
                                             where po.mispar_ishi=p_mispar_ishi
                                                       and  sysdate between PO.ME_TARICH and PO.AD_TARICH
                                                       and h.kod_isuk = PO.isuk and h.kod_yechida = po.YECHIDA_IRGUNIT)
                    
                        and H.KOD_DARGA = PO.DARGA and H.KOD_EZOR = po.EZOR)
                                               
         ORDER BY   mispar_ishi); */


             /* SELECT  DISTINCT e.yechidat_aba,e.yechida_mekorit,
                            o.shem_mish || ' ' ||  o.shem_prat  || ' (' || o.mispar_ishi || ')' Oved_Name,o.mispar_ishi
               FROM kdsadmin.PIRTEY_OVDIM po, kdsadmin.EZ_NIHULY E, kdsadmin.OVDIM O
               WHERE TRUNC(SYSDATE)>= po.ME_TAARICH AND TRUNC(SYSDATE)<= po.AD_TAARICH 
                    and po.KOD_NATUN=1
                    and po.ERECH=E.YECHIDA_MEKORIT
                    and O.MISPAR_ISHI= po.MISPAR_ISHI AND po.MISPAR_ISHI LIKE p_prefix
                 CONNECT BY e.YECHIDAT_ABA = PRIOR e.YECHIDA_MEKORIT
                 START WITH (e.YECHIDAT_ABA =p_yechida  OR e.YECHIDA_MEKORIT = p_yechida)
             ORDER BY mispar_ishi;*/
      --   END IF;
    
    /*SELECT DISTINCT a.yechidat_aba,a.yechida_mekorit,o.shem_mish || ' ' ||  o.shem_prat  || ' (' || a.mispar_ishi || ')' Oved_Name,a.mispar_ishi
				FROM
				     OVDIM o,
									(SELECT * FROM
									       EZ_NIHULY e,
										  (SELECT Mispar_Ishi,erech FROM PIRTEY_OVDIM
										   WHERE  TRUNC(SYSDATE)>= Me_taarich
										  AND TRUNC(SYSDATE)<= Ad_taarich
										  AND  Kod_Natun=1) p
									WHERE p.erech=e.yechida_mekorit) a
						WHERE o.mispar_ishi=a.mispar_ishi
						AND   o.mispar_ishi LIKE p_prefix
						CONNECT BY a.YECHIDAT_ABA  = PRIOR a.yechida_mekorit
						START WITH (a.YECHIDAT_ABA  =(SELECT Erech
																							FROM PIRTEY_OVDIM
																							WHERE Mispar_Ishi=p_mispar_ishi
																							AND TRUNC(SYSDATE)>= Me_taarich
																							AND TRUNC(SYSDATE)<= Ad_taarich
																							AND  Kod_Natun=1)
														OR a.yechida_mekorit   =(SELECT Erech
																							FROM PIRTEY_OVDIM
																							WHERE Mispar_Ishi=p_mispar_ishi
																							AND TRUNC(SYSDATE)>= Me_taarich
																							AND TRUNC(SYSDATE)<= Ad_taarich
																							AND  Kod_Natun=1))
                                   ORDER BY mispar_ishi;                                                         

					/*union all
						  select  e.yechidat_aba,e.yechida_mekorit,o.shem_mish || ' ' ||  o.shem_prat  || ' (' || o.mispar_ishi || ')' Oved_Name,o.mispar_ishi
							from
							     ovdim o, Pirtey_Ovdim p,ez_nihuly e
						      where  trunc(sysdate)>= Me_taarich
									  and trunc(sysdate)<= p.Ad_taarich
									  and  p.Kod_Natun=1
									  and p.Mispar_Ishi=p_mispar_ishi
									and p.erech=e.yechida_mekorit
									and o.mispar_ishi=p.mispar_ishi
									and   o.mispar_ishi like p_prefix		*/
				


	 EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END pro_get_etz_nihuly_by_user;

PROCEDURE pro_ins_Manage_Tree(p_mispar_ishi IN NUMBER ) IS 
  p_yechida varchar2(6);
BEGIN 
   EXECUTE IMMEDIATE 'truncate table TMP_MANAGE_TREE' ; 
 p_yechida:= fun_get_yechida(p_mispar_ishi);    
INSERT INTO TMP_MANAGE_TREE 
  select  distinct mispar_ishi
    from (                          
             select * from table(fun_get_manager_employees('%',p_yechida))
            union       
            select * from table(fun_get_isuk_harshaot('%',p_mispar_ishi))
            union          
            select * from table(fun_get_manager_emp_by_maamad('%', p_mispar_ishi))
            );
   -- order by oved_name;
       /*   SELECT DISTINCT a.mispar_ishi
                FROM
                                    (SELECT * FROM
                                           EZ_NIHULY e,
                                          (SELECT Mispar_Ishi,erech FROM PIRTEY_OVDIM
                                           WHERE  TRUNC(SYSDATE)>= Me_taarich
                                          AND TRUNC(SYSDATE)<= Ad_taarich
                                          AND  Kod_Natun=1) p
                                    WHERE p.erech=e.yechida_mekorit) a
                        CONNECT BY a.YECHIDAT_ABA  = PRIOR a.yechida_mekorit
                        START WITH (a.YECHIDAT_ABA  =(SELECT Erech
                                                                                            FROM PIRTEY_OVDIM
                                                                                            WHERE Mispar_Ishi=p_mispar_ishi
                                                                                            AND TRUNC(SYSDATE)>= Me_taarich
                                                                                            AND TRUNC(SYSDATE)<= Ad_taarich
                                                                                            AND  Kod_Natun=1)
                                                        OR a.yechida_mekorit   =(SELECT Erech
                                                                                            FROM PIRTEY_OVDIM
                                                                                            WHERE Mispar_Ishi=p_mispar_ishi
                                                                                            AND TRUNC(SYSDATE)>= Me_taarich
                                                                                            AND TRUNC(SYSDATE)<= Ad_taarich
                                                                                            AND  Kod_Natun=1)) ;

 
*/
END pro_ins_Manage_Tree; 

/*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0       02/06/2009      sari       1. ����� ������ ������ �����
*/
PROCEDURE pro_get_etz_nihuly_by_name(p_prefix IN VARCHAR2, p_mispar_ishi IN NUMBER, p_cur OUT CurType) IS
BEGIN
DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Utils',action_name => 'pro_get_etz_nihuly_by_name');
   OPEN p_Cur FOR    
   -- select  distinct yechidat_aba,yechida_mekorit, oved_name,mispar_ishi                       
    select  distinct  oved_name,mispar_ishi
    from (                          
            select * from table(fun_get_manager_emp_by_name(p_prefix,p_mispar_ishi))
            union       
            select * from table(fun_get_isuk_harshaot_by_name(p_prefix,p_mispar_ishi))
            union          
            select * from table(fun_get_mngr_emp_by_mamad_name(p_prefix, p_mispar_ishi)))
     order by oved_name;
        /*  SELECT DISTINCT a.yechidat_aba,a.yechida_mekorit,o.shem_mish || ' ' ||  o.shem_prat  || ' (' || o.mispar_ishi || ')' Oved_Name,a.mispar_ishi
				FROM
				     OVDIM o,
									(SELECT * FROM
									       EZ_NIHULY e,
										  (SELECT Mispar_Ishi,erech FROM PIRTEY_OVDIM
										   WHERE  TRUNC(SYSDATE)>= Me_taarich
										  AND TRUNC(SYSDATE)<= Ad_taarich
										  AND  Kod_Natun=1) p
									WHERE p.erech=e.yechida_mekorit) a
						WHERE o.mispar_ishi=a.mispar_ishi
						AND   o.shem_mish || ' ' ||  o.shem_prat LIKE p_prefix
						CONNECT BY a.YECHIDAT_ABA  = PRIOR a.yechida_mekorit
						START WITH (a.YECHIDAT_ABA  =(SELECT Erech
																							FROM PIRTEY_OVDIM
																							WHERE Mispar_Ishi=p_mispar_ishi
																							AND TRUNC(SYSDATE)>= Me_taarich
																							AND TRUNC(SYSDATE)<= Ad_taarich
																							AND  Kod_Natun=1)
														OR a.yechida_mekorit   =(SELECT Erech
																							FROM PIRTEY_OVDIM
																							WHERE Mispar_Ishi=p_mispar_ishi
																							AND TRUNC(SYSDATE)>= Me_taarich
																							AND TRUNC(SYSDATE)<= Ad_taarich
																							AND  Kod_Natun=1))
*/
					/*union all
						  select  e.yechidat_aba,e.yechida_mekorit,o.shem_mish || ' ' ||  o.shem_prat  || ' (' || o.mispar_ishi || ')' Oved_Name,o.mispar_ishi
							from
							     ovdim o, Pirtey_Ovdim p,ez_nihuly e
						      where  trunc(sysdate)>= Me_taarich
									  and trunc(sysdate)<= p.Ad_taarich
									  and  p.Kod_Natun=1
									  and p.Mispar_Ishi=p_mispar_ishi
									and p.erech=e.yechida_mekorit
									and o.mispar_ishi=p.mispar_ishi
									and   o.shem_mish || ' ' ||  o.shem_prat like p_prefix						*/
/*				ORDER BY mispar_ishi;*/


	 EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END pro_get_etz_nihuly_by_name;

/*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0       0405/2009      sari       1. ����� ��� ����� ������� �����
*/
PROCEDURE pro_get_meafyeney_bitua(p_Cur OUT CurType)
IS
	 BEGIN
     OPEN p_Cur FOR
      	 SELECT TO_CHAR(m.KOD_MEAFYEN_BITZUA) KOD_MEAFYEN_BITZUA,m.TEUR_MEAFYEN_BITZUA
		   FROM CTB_MEAFYEN_BITZUA  m
		   ORDER BY m.KOD_MEAFYEN_BITZUA ;


	 EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END pro_get_meafyeney_bitua;


/*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0       0405/2009      sari       1. ����� ���� ����
*/
PROCEDURE pro_get_kod_natun(p_Cur OUT CurType)
IS
	 BEGIN
     OPEN p_Cur FOR
      	 SELECT TO_CHAR(n.KOD_NATUN) KOD_NATUN ,n.TEUR_NATUN
		 FROM CTB_NATUN_HR  n
		 ORDER BY n.KOD_NATUN;


	 EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END pro_get_kod_natun;
PROCEDURE pro_get_parameters_table(p_cur OUT CurType)
IS
BEGIN
	 DBMS_APPLICATION_INFO.SET_MODULE('PKG_UTILS.pro_get_parameters_table',' get parameters table ');
     OPEN p_cur FOR
     SELECT kod_param,me_taarich,ad_taarich,erech_param
     FROM TB_PARAMETRIM;

EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END pro_get_parameters_table;
PROCEDURE pro_get_ctb_elementim(p_cur OUT CurType)
IS
BEGIN
    OPEN p_cur FOR
    SELECT * FROM CTB_ELEMENTIM;

END pro_get_ctb_elementim;
/*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0       27/07/2009      sari       1. ������ ���� ���� �������
*/
PROCEDURE pro_get_sugey_yamim_meyuchadim(p_cur OUT CurType)
IS
BEGIN
   DBMS_APPLICATION_INFO.SET_MODULE('Pkg_Utils.pro_get_sugey_yamim_meyuchadim','get  sugey yamim meyuchadim');
  
    OPEN p_cur FOR
	    SELECT sy.SUG_YOM,sy.TEUR_YOM,sy.YOM_AVODA,sy.SHBATON,sy.EREV_SHISHI_CHAG,sy.SHISHI_MUHLAF
		 FROM CTB_SUGEY_YAMIM_MEYUCHADIM sy;

END pro_get_sugey_yamim_meyuchadim;

/*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0       28/07/2009      sari       1. ������  ���� �������
*/
PROCEDURE pro_get_yamim_meyuchadim(p_cur OUT CurType)
IS
BEGIN
  DBMS_APPLICATION_INFO.SET_MODULE('Pkg_Utils.pro_get_yamim_meyuchadim','get yamim meyuchadim');
  
    OPEN p_cur FOR
	    SELECT m.SUG_YOM,m.taarich,m.Sug_Yom_Muchlaf_Minhal ,m.Sug_Yom_Muchlaf_Meshek , 
		m.Sug_Yom_Muchlaf_Tnua ,m.Sug_Yom_Muchlaf_Nehagut
		 FROM TB_YAMIM_MEYUCHADIM m ;

END pro_get_yamim_meyuchadim;

/*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0       27/07/2009      sari       1. ����� ������� �������  ����� ��� �����
*/
PROCEDURE pro_get_sidurim_meyuch_rechiv(p_tar_me IN TB_SIDURIM_MEYUCHADIM_RECHIV.me_taarich%TYPE,
		  								 p_tar_ad IN TB_SIDURIM_MEYUCHADIM_RECHIV.me_taarich%TYPE,
		  									   	  											 p_cur OUT CurType)
IS
BEGIN
DBMS_APPLICATION_INFO.SET_MODULE('PKG_UTILS.pro_get_sidurim_meyuch_rechiv','get kod rechiv for sidurim meyuchadim');
    OPEN p_cur FOR
	    SELECT sm.mispar_sidur,sm.me_taarich,NVL(sm.ad_taarich,TO_DATE('01/01/9999' ,'dd/mm/yyyy')) ad_taarich,sm.kod_rechiv
		 FROM TB_SIDURIM_MEYUCHADIM_RECHIV sm
		 WHERE  (  p_tar_me  BETWEEN  sm.me_taarich AND   NVL(sm.ad_taarich,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
					  OR p_tar_ad  BETWEEN  sm.me_taarich  AND   NVL(sm.ad_taarich,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
					  OR   sm.me_taarich>=p_tar_me  AND   NVL(sm.ad_taarich,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=  p_tar_ad  );
		
		-- WHERE p_date BETWEEN sm.me_taarich AND NVL(sm.ad_taarich,TO_DATE('01/01/9999','dd/mm/yyyy'));

END pro_get_sidurim_meyuch_rechiv;

/*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0       27/07/2009      sari       1. ����� ��� ����� ����� ��� �����
*/
PROCEDURE pro_get_sug_sidur_rechiv( p_tar_me IN TB_SIDURIM_MEYUCHADIM_RECHIV.me_taarich%TYPE,
		  								 p_tar_ad IN TB_SIDURIM_MEYUCHADIM_RECHIV.me_taarich%TYPE,
		  									   	  											 p_cur OUT CurType)
IS
BEGIN
	 DBMS_APPLICATION_INFO.SET_MODULE('PKG_UTILS.pro_get_sug_sidur_rechiv','get kod rechiv for sidurim regilim');
    OPEN p_cur FOR
	    SELECT sr.KOD_RECHIV,sr.ME_TAARICH, NVL(sr.ad_taarich,TO_DATE('01/01/9999' ,'dd/mm/yyyy')) AD_TAARICH,sr.SUG_SIDUR
		 FROM TB_SUG_SIDUR_RECHIV sr
		  WHERE  (  p_tar_me  BETWEEN  sr.me_taarich AND   NVL(sr.ad_taarich,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
					  OR p_tar_ad  BETWEEN  sr.me_taarich  AND   NVL(sr.ad_taarich,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
					  OR   sr.me_taarich>=p_tar_me  AND   NVL(sr.ad_taarich,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=  p_tar_ad  );
		
		 --WHERE p_date BETWEEN sr.me_taarich AND NVL(sr.ad_taarich,TO_DATE('01/01/9999','dd/mm/yyyy'));

END pro_get_sug_sidur_rechiv;

/*   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0       03/08/2009      sari       1. ����� ���� �������*/
PROCEDURE pro_get_ctb_mutamut(p_cur OUT CurType)
IS
BEGIN
    OPEN p_cur FOR
    SELECT m.kod_mutamut,m.teur_mutamut,m.mezake_gmul, m.isur_shaot_nosafot
    FROM CTB_MUTAMUT m;

END pro_get_ctb_mutamut;


PROCEDURE pro_get_sibot_ledivuch_yadani(p_cur OUT CurType)
IS
BEGIN
DBMS_APPLICATION_INFO.SET_MODULE('pkg_utils.pro_get_sibot_ledivuch_yadani','get sibot ledivuch yadani');

    OPEN p_cur FOR
    SELECT m.goremet_lebitul_zman_halbasha,m.goremet_lebitul_zman_nesiaa,
           m.kod_siba, m.pail, m.teur_siba
    FROM CTB_SIBOT_LEDIVUCH_YADANI m
    WHERE Pail=1
    ORDER BY m.kod_siba;
END pro_get_sibot_ledivuch_yadani;


PROCEDURE pro_get_status_ishur_max_level(p_mispar_ishi IN TB_ISHURIM.mispar_ishi%TYPE,
                                         p_taarich IN TB_ISHURIM.taarich%TYPE,
                                         p_kod_ishur IN TB_ISHURIM.kod_ishur%TYPE,
                                         p_kod_status OUT TB_ISHURIM.kod_status_ishur%TYPE)
IS
BEGIN
    SELECT kod_status_ishur INTO p_kod_status
    FROM TB_ISHURIM T
    WHERE t.mispar_ishi   = p_mispar_ishi
          AND t.taarich   =  p_taarich 
          AND t.kod_ishur = p_kod_ishur
          AND t.rama = (SELECT MAX(rama) rama
                        FROM TB_ISHURIM I
                        WHERE i.mispar_ishi = t.mispar_ishi
                              AND i.taarich = t.taarich
                              AND i.kod_ishur = t.kod_ishur);
END pro_get_status_ishur_max_level;


FUNCTION  pro_check_ishur(p_mispar_ishi IN TB_ISHURIM.mispar_ishi%TYPE,
                                         p_taarich IN TB_ISHURIM.taarich%TYPE,
                                         p_kod_ishur IN TB_ISHURIM.kod_ishur%TYPE,
                                       p_mispar_sidur IN TB_ISHURIM.mispar_sidur%TYPE DEFAULT  NULL,
									    p_shat_hatchala IN TB_ISHURIM.Shat_Hatchala%TYPE DEFAULT  NULL) RETURN  NUMBER
	IS
	v_ishur NUMBER;
BEGIN
	 BEGIN
		 SELECT 1 INTO v_ishur
			FROM TB_ISHURIM
			WHERE Mispar_Ishi = p_mispar_ishi
			AND Kod_Ishur =  p_kod_ishur
			AND Taarich =  p_taarich
			AND (Mispar_Sidur =   p_mispar_sidur OR   p_mispar_sidur IS NULL)
			AND (Shat_Hatchala= p_shat_hatchala OR  p_shat_hatchala IS NULL)
			AND ROWNUM=1;

			EXCEPTION
		 WHEN NO_DATA_FOUND THEN
					RETURN 1;
	       WHEN OTHERS THEN
				  RAISE;
     END;

	BEGIN
	   SELECT 1 INTO v_ishur
		FROM TB_ISHURIM
        WHERE Mispar_Ishi =  p_mispar_ishi
        AND Kod_Ishur =  p_kod_ishur
       AND Taarich = p_taarich
        AND (Mispar_Sidur =   p_mispar_sidur OR   p_mispar_sidur IS NULL)
		AND (Shat_Hatchala= p_shat_hatchala OR  p_shat_hatchala IS NULL)
        AND Kod_Status_Ishur = 1
        AND Rama = (SELECT MAX(rama)
      			                    FROM TB_ISHURIM
								    WHERE Mispar_Ishi =  p_mispar_ishi
								     AND Kod_Ishur =  p_kod_ishur
								    AND Taarich = p_taarich
								    AND (Mispar_Sidur =   p_mispar_sidur OR   p_mispar_sidur IS NULL)
							        AND (Shat_Hatchala= p_shat_hatchala OR  p_shat_hatchala IS NULL));


		RETURN v_ishur;

	EXCEPTION
		 WHEN NO_DATA_FOUND THEN
					 RETURN 0;
	       WHEN OTHERS THEN
				 RAISE;
  END;

END pro_check_ishur;

PROCEDURE pro_get_ovdim_for_premia(p_kod_premia IN MEAFYENIM_OVDIM.KOD_MEAFYEN%TYPE,p_taarich IN MEAFYENIM_OVDIM.ME_TAARICH%TYPE, p_cur OUT CurType)
IS
BEGIN
DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Utils',action_name => 'pro_get_ovdim_for_premia');
if (p_kod_premia <> 103 and (p_kod_premia not between 112 and 119)) then
--<> 112 and p_kod_premia <> 113 and p_kod_premia <> 114 and
--p_kod_premia <> 115 and p_kod_premia <> 116 and p_kod_premia <> 117 and p_kod_premia <> 118 and  p_kod_premia <> 119 ) then
	 OPEN p_cur FOR
           SELECT *
              FROM
              (SELECT  m.MISPAR_ISHI,
                                        DECODE(m.kod_meafyen,NULL,b.kod_meafyen,m.kod_meafyen) kod_meafyen,
                                          TO_NUMBER(DECODE(m.erech_ishi,NULL,DECODE(m.ERECH_MECHDAL_PARTANY,NULL, b.erech ,m.ERECH_MECHDAL_PARTANY ),m.erech_ishi))  value_erech_ishi
                                        FROM PIVOT_MEAFYENIM_OVDIM m,BREROT_MECHDAL_MEAFYENIM b,
                                           (SELECT m.mispar_ishi,MAX(m.ME_TAARICH) me_taarich
                                               FROM PIVOT_MEAFYENIM_OVDIM m
                                               WHERE (p_taarich   BETWEEN  m.ME_TAARICH  AND   NVL(m.ad_TAARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                                                       OR last_day(p_taarich)  BETWEEN  m.ME_TAARICH  AND   NVL(m.ad_TAARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                                                        OR   (m.ME_TAARICH>=p_taarich   AND   NVL(m.ad_TAARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=last_day(p_taarich )  ))
                                             AND m.kod_meafyen=p_kod_premia
                                             GROUP BY m.mispar_ishi) x 
                            WHERE    m.mispar_ishi= x.mispar_ishi
                                    and   m.ME_TaARICH = x.me_taarich  
                              AND p_taarich BETWEEN b.ME_TAARICH AND NVL(b.AD_TAARICH,TO_DATE('01/01/9999','dd/mm/yyyy'))
                               AND m.kod_meafyen=b.KOD_MEAFYEN(+)
                               AND m.kod_meafyen=p_kod_premia
                ) t
                WHERE t.value_erech_ishi = 1
                ORDER BY t.MISPAR_ISHI ASC;
     /*SELECT *
              FROM
              (SELECT  m.MISPAR_ISHI,
                                        DECODE(m.kod_meafyen,NULL,b.kod_meafyen,m.kod_meafyen) kod_meafyen,
                                          TO_NUMBER(DECODE(m.erech_ishi,NULL,DECODE(m.ERECH_MECHDAL_PARTANY,NULL, b.erech ,m.ERECH_MECHDAL_PARTANY ),m.erech_ishi))  value_erech_ishi
                                        FROM PIVOT_MEAFYENIM_OVDIM m,BREROT_MECHDAL_MEAFYENIM b
                            WHERE  p_taarich  BETWEEN m.ME_TAARICH(+) AND NVL(m.AD_TAARICH(+),TO_DATE('01/01/9999','dd/mm/yyyy'))
                              AND  p_taarich  BETWEEN b.ME_TAARICH AND NVL(b.AD_TAARICH,TO_DATE('01/01/9999','dd/mm/yyyy'))
                               AND m.kod_meafyen=b.KOD_MEAFYEN(+)
                               AND m.kod_meafyen=p_kod_premia
                ) t
                WHERE t.value_erech_ishi = 1
                ORDER BY t.MISPAR_ISHI ASC;*/
else
OPEN p_cur FOR
     SELECT p.MISPAR_ISHI, p_kod_premia
     FROM TB_PREMYOT_YADANIYOT p
          WHERE  p.SUG_PREMYA = p_kod_premia  
            AND  p.TAARICH between p_taarich and last_day(p_taarich); -- TO_CHAR(TAARICH,'mm/yyyy') = TO_CHAR(p_chodesh,'mm/yyyy');

            
end if;
	 EXCEPTION
		    WHEN OTHERS THEN
				 RAISE;

END pro_get_ovdim_for_premia;

PROCEDURE pro_get_ovdim_for_premiot(p_mispar_ishi IN VARCHAR2, p_kod_premia IN MEAFYENIM_OVDIM.KOD_MEAFYEN%TYPE,p_Period IN VARCHAR2, p_cur OUT CurType)
 IS
  p_ToDate DATE ;
  p_FromDate DATE ;
  BEGIN
  DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Utils',action_name => 'pro_get_ovdim_for_premiot');
      p_FromDate := TO_DATE('01/' || p_Period,'dd/mm/yyyy'); /* period= 05/2009=>  v_MinLimitDate = 01/05/2009 */
    p_ToDate := ADD_MONTHS(p_FromDate,1) -1 ;    /* period= 05/2009=>  v_MaxLimitDate = 31/05/2009 */

     OPEN p_cur FOR
SELECT *
  FROM
  (SELECT  m.MISPAR_ISHI,
                              DECODE(m.kod_meafyen,NULL,b.kod_meafyen,m.kod_meafyen) kod_meafyen,
                              TO_NUMBER(DECODE(m.erech_ishi,NULL,DECODE(m.ERECH_MECHDAL_PARTANY,NULL, b.erech ,m.ERECH_MECHDAL_PARTANY ),m.erech_ishi))  value_erech_ishi
                            FROM PIVOT_MEAFYENIM_OVDIM m,BREROT_MECHDAL_MEAFYENIM b
                WHERE (m.Me_Taarich <=  p_ToDate ) AND (m.Ad_Taarich >= p_FromDate OR m.Ad_Taarich IS NULL )
                   AND m.kod_meafyen=b.KOD_MEAFYEN(+)
                   AND m.kod_meafyen=p_kod_premia
    ) t
    WHERE t.value_erech_ishi = 1
    AND t.mispar_ishi LIKE  p_mispar_ishi || '%' 
     group by MISPAR_ISHI,kod_meafyen,value_erech_ishi
    ORDER BY t.MISPAR_ISHI ASC;

     EXCEPTION
            WHEN OTHERS THEN
                 RAISE;

END pro_get_ovdim_for_premiot;


PROCEDURE pro_get_premyot_details(p_premya_codes VARCHAR2, p_cur OUT CurType)
IS
BEGIN
--INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'p_premya_codes:',p_premya_codes);
	 OPEN p_cur FOR
	 	  SELECT Kod_Premia, Teur_Premia,TALUI_BEMAFYEN_ISHI
		  FROM CTB_SUGEY_PREMIOT
		  WHERE  p_premya_codes IS NOT NULL 
               AND LENGTH(p_premya_codes) > 0 
               AND   Kod_Premia IN  (SELECT x FROM TABLE(CAST(Convert_String_To_Table(p_premya_codes ,  ',') AS mytabtype)))
               AND pail=1;

	EXCEPTION
		    WHEN OTHERS THEN
				 RAISE;

END pro_get_premyot_details;

PROCEDURE pro_get_premyot_view(p_mispar_ishi IN PREMYOT_VW.mispar_ishi%TYPE,
                                         p_tkufa IN PREMYOT_VW.tkufa%TYPE,
                                         p_cur OUT CurType)
IS
BEGIN
 DBMS_APPLICATION_INFO.SET_MODULE('PKG_UTILS.pro_get_premyot_view',' get premyot view ');
  
   OPEN p_cur FOR
  -- SELECT 0 Sug_premia,0  Sum_dakot FROM dual;
	 SELECT Sug_premia,SUM(Dakot_premia) Sum_dakot
		FROM PREMYOT_VW
		WHERE Mispar_ishi = p_mispar_ishi
		AND Tkufa = p_tkufa
		GROUP BY (Mispar_ishi,Tkufa, Sug_premia);

	EXCEPTION
		    WHEN OTHERS THEN
				 RAISE;
END pro_get_premyot_view;

PROCEDURE pro_get_premia_yadanit(p_mispar_ishi IN TB_PREMYOT_YADANIYOT.MISPAR_ISHI%TYPE, p_chodesh IN TB_PREMYOT_YADANIYOT.TAARICH%TYPE, p_sug_premya IN TB_PREMYOT_YADANIYOT.SUG_PREMYA%TYPE, p_Cur OUT CurType)
IS
    p_taarich date;
BEGIN
   DBMS_APPLICATION_INFO.SET_MODULE('PKG_SUG_UTILS.pro_get_premia_yadanit',' get premia yadanit');
     p_taarich:= to_date('01/' || to_char(p_chodesh,'mm/yyyy'),'dd/mm/yyyy');
	 OPEN p_cur FOR
	 	  SELECT DAKOT_PREMYA, Taarich_idkun_acharon,sug_premya
		  FROM TB_PREMYOT_YADANIYOT
		  WHERE MISPAR_ISHI = p_mispar_ishi
		    AND (SUG_PREMYA = p_sug_premya OR p_sug_premya IS NULL)
			AND  TAARICH between p_taarich and last_day(p_taarich); -- TO_CHAR(TAARICH,'mm/yyyy') = TO_CHAR(p_chodesh,'mm/yyyy');

	EXCEPTION
		    WHEN OTHERS THEN
				 RAISE;
END pro_get_premia_yadanit;

PROCEDURE pro_get_zman_nesia(p_merkaz_erua IN  CTB_ZMAN_NSIAA_MISHTANE.merkaz_erua%TYPE,
                             p_mikum_yaad  IN  CTB_ZMAN_NSIAA_MISHTANE.mikum_yaad%TYPE,
                             p_taarich     IN CTB_ZMAN_NSIAA_MISHTANE.me_taarich%TYPE,
                             p_dakot       OUT CTB_ZMAN_NSIAA_MISHTANE.dakot%TYPE)
IS
    v_dakot NUMBER;
	v_kod_mikum_yechida NUMBER;
	 v_kod_merkaz_erua NUMBER;
BEGIN
    v_dakot:= 0;
	SELECT    KOD_MIKUM_YECHIDA_EZORI  INTO  v_kod_mikum_yechida FROM CTB_MIKUM_YECHIDA
	WHERE KOD_MIKUM_YECHIDA=SUBSTR(p_mikum_yaad,0,3);

	SELECT  KOD_MERKAZ_EROA_EZORI    INTO  v_kod_merkaz_erua FROM CTB_MERKAZ_EROA
	WHERE KOD_MERKAZ_EROA=p_merkaz_erua;

    SELECT dakot INTO v_dakot
    FROM CTB_ZMAN_NSIAA_MISHTANE c
    WHERE c.merkaz_erua =  v_kod_merkaz_erua AND
          c.mikum_yaad  = v_kod_mikum_yechida  AND
           p_taarich  BETWEEN c.me_taarich AND c.ad_taarich;


    p_dakot := v_dakot;

EXCEPTION
        WHEN NO_DATA_FOUND THEN
		     p_dakot := -1;
        WHEN OTHERS THEN
             RAISE;
END pro_get_zman_nesia;

  PROCEDURE Pro_Get_Value_From_Parametrim( p_Kod_Param IN  TB_PARAMETRIM.Kod_Param%TYPE,
                                                                              p_Period IN VARCHAR2 ,
                                                                              p_Erech_Param OUT  TB_PARAMETRIM.ERECH_PARAM%TYPE) IS
  p_ToDate DATE ;
  p_FromDate DATE ;
  BEGIN
  	p_FromDate := TO_DATE('01/' || p_Period,'dd/mm/yyyy'); /* period= 05/2009=>  v_MinLimitDate = 01/05/2009 */
    p_ToDate := ADD_MONTHS(p_FromDate,1) -1 ;    /* period= 05/2009=>  v_MaxLimitDate = 31/05/2009 */
    SELECT  erech_param INTO p_Erech_Param
    FROM TB_PARAMETRIM
    WHERE Kod_Param = p_Kod_Param
    AND (Me_Taarich <= p_ToDate) AND (Ad_Taarich >= p_FromDate OR Ad_Taarich IS NULL ) ;
           EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;
  END  Pro_Get_Value_From_Parametrim;

PROCEDURE Pro_Get_Value_From_Parametrim (p_kod_param IN TB_PARAMETRIM.KOD_PARAM%TYPE,
                                                                                p_taarich IN DATE ,
                                                                                p_ERECH_PARAM    OUT INTEGER)  IS
BEGIN
    SELECT ERECH_PARAM  INTO p_ERECH_PARAM
            FROM TB_PARAMETRIM
                WHERE KOD_PARAM =  p_kod_param
                    AND  ME_TAARICH   <= p_taarich
                    AND  AD_TAARICH   >= p_taarich ;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
         p_ERECH_PARAM := 0;
END Pro_Get_Value_From_Parametrim ;


PROCEDURE Pro_Get_Previous_Months_List(p_FromDate IN DATE, NumOfPreviousMonth NUMBER ,DisplayAll NUMBER,   p_cur OUT CurType) IS
BEGIN
OPEN p_Cur FOR
  SELECT x FROM TABLE(CAST(Convert_String_To_Table(
   KDSADMIN.Previous_Months_List ( p_FromDate, NumOfPreviousMonth, DisplayAll ) ,  ',') AS mytabtype));

   END Pro_Get_Previous_Months_List;




  PROCEDURE  pro_get_ovdim_leRitza (p_mis_ritza IN INTEGER  ,
  			 					   				   			  	                p_maamad IN VARCHAR2,
																			    p_isuk IN VARCHAR2,
																				p_preFix IN VARCHAR2,
																				p_cur OUT CurType) IS
  BEGIN
      OPEN p_cur FOR
	--  select 75290, '����' from dual ;
  SELECT DISTINCT Ov.MISPAR_ISHI,
	  		 		  CASE WHEN (p_prefix IS NULL) THEN
 					  	    Ov.shem_mish || ' ' ||  Ov.shem_prat || '(' || Ov.MISPAR_ISHI || ')'
					  ELSE ''
					  END full_name
		   FROM TB_CHISHUV_CHODESH_OVDIM C,
		   				PIVOT_PIRTEY_OVDIM T ,
						OVDIM Ov
		   WHERE
		   		 			  c.MISPAR_ISHI = Ov.MISPAR_ISHI AND
		   		 			  C.BAKASHA_ID =	p_mis_ritza AND
		   		 			 C.MISPAR_ISHI = T.MISPAR_ISHI AND
							--(  p_maamad is null OR  ( (substr( T.MAAMAD,0,1)  IN (SELECT X FROM TABLE(CAST(CONVERT_STRING_TO_TABLE(p_maamad ,  ',') AS MYTABTYPE))) )   AND      ( (add_months(C.TAARICH,1) -1) BETWEEN T.ME_TARICH and T.AD_TARICH)    )     ) AND
						--	(  p_isuk is null OR  ( (T.ISUK  IN (SELECT X FROM TABLE(CAST(CONVERT_STRING_TO_TABLE(p_isuk ,  ',') AS MYTABTYPE))) )   AND      ( (add_months(C.TAARICH,1) -1) BETWEEN T.ME_TARICH and T.AD_TARICH)    )     ) AND

						(ADD_MONTHS(C.TAARICH,1) -1) BETWEEN T.ME_TARICH(+) AND T.AD_TARICH(+)
						AND ((SUBSTR( T.MAAMAD,0,1)  IN (SELECT X FROM TABLE(CAST(Convert_String_To_Table(p_maamad ,  ',') AS MYTABTYPE)))) OR p_maamad IS NULL) AND
				    	 (T.ISUK IN (SELECT X FROM TABLE(CAST(Convert_String_To_Table(p_isuk ,  ',') AS MYTABTYPE)))  OR p_isuk IS NULL) AND
							( p_preFix IS NULL OR C.MISPAR_ISHI LIKE p_preFix ||  '%'  )
		   ORDER BY Ov.MISPAR_ISHI;

	   EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;
  END pro_get_ovdim_leRitza;

  FUNCTION fun_GET_Rechiv_Value ( p_MisparIshi IN TB_CHISHUV_CHODESH_OVDIM.Mispar_ishi%TYPE,
                                                        p_Kod_Rechiv IN TB_CHISHUV_CHODESH_OVDIM.Kod_Rechiv%TYPE,
                                                        p_StartDate IN DATE,
                                                        p_EndDate IN DATE,
                                                        p_Bakasha_ID IN  TB_CHISHUV_CHODESH_OVDIM.Bakasha_ID%TYPE
                                                        ) RETURN NUMBER AS
ValueComponent NUMBER ;
BEGIN
SELECT NVL( Ch.Erech_Rechiv,0)  INTO ValueComponent
FROM TB_CHISHUV_CHODESH_OVDIM Ch
WHERE Ch.Kod_Rechiv=p_Kod_Rechiv
AND Ch. Mispar_Ishi= p_MisparIshi
AND Ch.Taarich BETWEEN p_StartDate AND p_EndDate
AND Ch.Bakasha_ID=p_Bakasha_ID;

    RETURN ValueComponent  ;

       EXCEPTION
            WHEN NO_DATA_FOUND THEN
                      ValueComponent := 0 ;
                          RETURN ValueComponent  ;
            WHEN OTHERS THEN
                          RAISE;
END fun_GET_Rechiv_Value;


 PROCEDURE get_sadot_nosafim_lesidur(p_Sidur IN INTEGER,
 		  									   		   	  		 		        p_List_Meafyenim IN VARCHAR2,
																				p_cur OUT CurType) IS
 BEGIN
 		  OPEN p_cur FOR
					 SELECT s.KOD_MEAFYEN,s.ERECH, c.*
					 FROM TB_SADOT_NOSAFIM_LESIDUR s,CTB_SADOT_NOSAFIM_LESIDUR c
					 WHERE s.KOD_SADE_IN_MASACH = c.KOD_SADE_IN_MASACH AND
					 	   			  (s.MISPAR_SIDUR=p_Sidur  OR s.KOD_MEAFYEN IN(  SELECT x FROM TABLE(CAST(Convert_String_To_Table(p_List_Meafyenim ,  ',') AS mytabtype)) )    ) AND
									  c.RAMA = 1;



       EXCEPTION
         WHEN OTHERS THEN
              RAISE;
  END get_sadot_nosafim_lesidur;

  PROCEDURE get_sadot_nosafim_lePeilut(p_cur OUT CurType) IS
 BEGIN
 		  OPEN p_cur FOR
					 SELECT c.*
					 FROM CTB_SADOT_NOSAFIM_LESIDUR c
					 WHERE  c.RAMA = 2;

       EXCEPTION
         WHEN OTHERS THEN
              RAISE;
  END get_sadot_nosafim_lePeilut;

  PROCEDURE get_sadot_nosafim_kayamim(p_mispar_ishi IN  INTEGER,
														                                p_mispar_sidur IN  INTEGER,
																					    p_taarich IN TB_SIDURIM_OVDIM.TAARICH%TYPE,
																						p_shat_hatchala IN TB_SIDURIM_OVDIM.shat_hatchala%TYPE,
																						p_cur OUT CurType) IS
	 BEGIN
       DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Utils',action_name => 'get_sadot_nosafim_kayamim');
	 			 OPEN p_cur FOR
						  SELECT s.Mispar_Musach_O_Machsan,s.Yom_Visa,s.Achuz_Knas_LePremyat_Visa ,
							              s.Achuz_Viza_Besikun,s.Sug_Hazmanat_Visa,s.Mispar_shiurey_nehiga,
										  s.Tafkid_Visa,s.Mivtza_Visa,y.Lina
							FROM TB_SIDURIM_OVDIM s ,
										  TB_YAMEY_AVODA_OVDIM y
							WHERE s.MISPAR_ISHI =p_mispar_ishi  AND
								  			   s.MISPAR_SIDUR =p_mispar_sidur AND
								  			   s.TAARICH =	  p_taarich  AND
											   s.MISPAR_ISHI = y.MISPAR_ISHI AND
											   s.SHAT_HATCHALA = p_shat_hatchala AND
							                   s.TAARICH = y.TAARICH ;
											          EXCEPTION
         WHEN OTHERS THEN
              RAISE;
  END get_sadot_nosafim_kayamim;
  
  
  PROCEDURE pro_insert_barkod_Tachograf(p_mispar_ishi IN  INTEGER,
  													  	  		  	  			 			p_taarich IN TB_TACHOGRAF_LE_KARTIS.TAARICH%TYPE,
																							p_Barkod IN NUMBER	)IS
	 BEGIN
	 			
				INSERT INTO TB_TACHOGRAF_LE_KARTIS(MISPAR_ISHI,TAARICH,MISPAR,BARKOD)
					   																 VALUES(p_mispar_ishi,p_taarich ,1,p_Barkod);
		 EXCEPTION
         WHEN OTHERS THEN
              RAISE;
  END pro_insert_barkod_Tachograf;
		
				
 PROCEDURE fun_get_barkod_Tachograf(p_mispar_ishi IN  INTEGER,
			  										 p_taarich IN TB_TACHOGRAF_LE_KARTIS.TAARICH%TYPE,
													 p_cur OUT CurType  ) AS

BEGIN
	 		  OPEN p_Cur FOR
	 		   	SELECT DISTINCT t.Barkod 
				FROM TB_TACHOGRAF_LE_KARTIS t 
				WHERE t.MISPAR_ISHI =p_mispar_ishi
					  AND t.TAARICH =p_taarich;		   			
    				  
       EXCEPTION
            WHEN OTHERS THEN
                          RAISE;
END fun_get_barkod_Tachograf;
  
  
  
  	PROCEDURE pro_get_tavlaot_to_refresh(p_cur OUT CurType)
	IS
	 BEGIN
	 			OPEN p_cur FOR
					    SELECT trim(SUBSTR(p.teur_peilut_be_tahalich,9)) NAME, p.KOD_PEILUT_BE_TAHALICH kod
						FROM CTB_PEILUT_BETAHALICH p
						WHERE kod_tahalich=3
						AND SUBSTR(teur_peilut_be_tahalich,1,7) = ('Refresh' )	
						ORDER BY	p.KOD_PEILUT_BE_TAHALICH ASC;								
		 EXCEPTION
         WHEN OTHERS THEN
              RAISE;
  END pro_get_tavlaot_to_refresh;
					
	PROCEDURE pro_get_snif_tnua_by_kod(p_kod_snif IN NUMBER,p_cur OUT CurType)
	IS
	 BEGIN
	 			OPEN p_cur FOR
                   SELECT DISTINCT s.KOD_SNIF_TNUAA,s.teur_snif_tnuaa TEUR_SNIF
                       FROM ctb_snifey_tnuaa s
                       WHERE s.KOD_SNIF_TNUAA= p_kod_snif;
				   /*    SELECT DISTINCT s.KOD_SNIF_AV,s.TEUR_SNIF_AV TEUR_SNIF
					   FROM CTB_SNIF_AV s
					   WHERE s.SNIF_TNUA LIKE p_kod_snif || '%';*/
		 EXCEPTION
         WHEN OTHERS THEN
              RAISE;
  END pro_get_snif_tnua_by_kod;		
  
 PROCEDURE pro_insert_meadken_acharon(p_mispar_ishi IN tb_meadken_acharon.mispar_ishi%type,p_taarich tb_meadken_acharon.taarich%type, p_gorem_meadken in tb_meadken_acharon.GOREM_MEADKEN%type)
    IS
    icount number;
     BEGIN
        
        BEGIN
            select count(*) into icount
            from tb_meadken_acharon t
            where T.MISPAR_ISHI = p_mispar_ishi
               and T.TAARICH = p_taarich;
        
        EXCEPTION
         WHEN NO_DATA_FOUND THEN
                icount:=0;
        END;
       
          if (icount = 0) then
                insert into tb_meadken_acharon(MISPAR_ISHI,TAARICH,TAARICH_IDKUN_ACHARON,GOREM_MEADKEN )
                VALUES (p_mispar_ishi,p_taarich, sysdate,p_gorem_meadken);
          ELSE
                UPDATE tb_meadken_acharon
                SET  TAARICH_IDKUN_ACHARON=  sysdate,
                        gorem_meadken=p_gorem_meadken
                WHERE   MISPAR_ISHI = p_mispar_ishi
                      and  TAARICH = p_taarich;
          end if;
           
         EXCEPTION
         WHEN OTHERS THEN
              RAISE;
  END pro_insert_meadken_acharon;    
  
                          
    PROCEDURE pro_get_ovdim_by_bakasha(p_bakasha_id IN NUMBER,p_cur OUT CurType)
    IS
     BEGIN
                 OPEN p_cur FOR
                   SELECT DISTINCT C.MISPAR_ISHI,C.TAARICH
                       FROM tb_chishuv_chodesh_ovdim c
                       WHERE C.BAKASHA_ID = p_bakasha_id;

         EXCEPTION
         WHEN OTHERS THEN
              RAISE;
  END pro_get_ovdim_by_bakasha;        
  	
  
FUNCTION fun_get_kod_tachanat_bizua(p_mispar_ishi IN  TB_PEILUT_OVDIM.mispar_ishi%TYPE,
                                     p_mispar_sidur IN TB_PEILUT_OVDIM.mispar_sidur%TYPE,
                                     p_taarich IN  TB_PEILUT_OVDIM.taarich%TYPE,
                                     p_shat_hatchala IN  TB_PEILUT_OVDIM.shat_hatchala_sidur%TYPE) RETURN number AS


     v_snif_tnua NUMBER;
     BEGIN
      if   (SUBSTR (p_mispar_sidur, 0, 2) <> '99' AND p_mispar_sidur > 999) then
            v_snif_tnua:=TO_NUMBER (SUBSTR (LPAD (p_mispar_sidur, 5), 0, 2));
      else if (SUBSTR (p_mispar_sidur, 0, 2) = '99') then
                    begin
                          SELECT v.snif_tnua into v_snif_tnua
                          FROM VIW_SNIF_TNUA_FROM_TNUA v
                         WHERE     v.mispar_ishi =p_mispar_ishi
                               AND v.taarich = p_taarich
                               AND v.mispar_sidur = p_mispar_sidur
                               AND v.SHAT_HATCHALA =p_shat_hatchala;
                               
                               EXCEPTION
                                    WHEN no_data_found THEN
                                      v_snif_tnua:=null;  
                     end;
               else    v_snif_tnua:=null;  
             end if;
      end if;         
      return    v_snif_tnua;
         EXCEPTION
         WHEN OTHERS THEN
              RAISE;
  END fun_get_kod_tachanat_bizua;  
  function fun_get_yechida(p_mispar_ishi in ovdim.mispar_ishi%type) return number as
   p_yechida number;
  BEGIN
        BEGIN
                    SELECT  ERECH into p_yechida
                    FROM PIRTEY_OVDIM
                    WHERE MISPAR_ISHI=p_mispar_ishi  
                         AND KOD_NATUN=1
                         AND TRUNC(SYSDATE)>= ME_TAARICH AND TRUNC(SYSDATE)<=AD_TAARICH ;
                 EXCEPTION
                               WHEN NO_DATA_FOUND  THEN
                                 p_yechida:=0;
                END;
    return p_yechida;                
   EXCEPTION        
         WHEN OTHERS THEN
              RAISE;
  end fun_get_yechida;
  
  
  
  function fun_get_manager_employees(p_prefix in varchar2,p_yechida in pivot_pirtey_ovdim.yechida_irgunit %type) return tree_table pipelined as
   --rc sys_refcursor;
    --  tbl tree_table;
begin
       DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Utils',action_name => 'fun_get_manager_employees');
     for i in (with nihuly as (select
                                      en.yechidat_aba, en.yechida_mekorit
                                      from         kdsadmin.ez_nihuly en
                                      connect by   en.yechidat_aba = prior en.yechida_mekorit
                                      start with     en.yechida_mekorit = p_yechida ) --    97709
                 select   /*+ use_nl (e po o) */
                           --  e.yechidat_aba , e.yechida_mekorit ,
                             o.shem_mish || ' ' || o.shem_prat || ' (' || o.mispar_ishi || ')'
                             oved_name, o.mispar_ishi 
                 from     kdsadmin.pirtey_ovdim po, nihuly e, kdsadmin.ovdim o
                 where   trunc ( sysdate ) >= po.me_taarich
                            and trunc ( sysdate ) <= po.ad_taarich
                            and po.kod_natun = 1
                            and po.erech = e.yechida_mekorit
                            and o.mispar_ishi = po.mispar_ishi
                            and po.mispar_ishi like p_prefix
     ) loop
  --   pipe row(tree_record(i.yechidat_aba,i.yechida_mekorit,i.oved_name,i.mispar_ishi));
     pipe row(tree_record(i.oved_name,i.mispar_ishi));
     end loop;
 return ;    
      
   exception        
         when others then
              raise;
  end fun_get_manager_employees;
  
  function fun_get_isuk_harshaot(p_prefix in varchar2,p_mispar_ishi in pivot_pirtey_ovdim.mispar_ishi%type) return  tree_table pipelined as
  
   begin
       DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Utils',action_name => 'fun_get_isuk_harshaot');
       -- for i in (select 0 yechidat_aba, 0 yechida_mekorit , o.shem_mish || ' ' || o.shem_prat || ' (' || o.mispar_ishi || ')'  oved_name, o.mispar_ishi
        for i in (select o.shem_mish || ' ' || o.shem_prat || ' (' || o.mispar_ishi || ')'  oved_name, o.mispar_ishi
        from 
            pivot_pirtey_ovdim po, ovdim o 
         where o.mispar_ishi = po.mispar_ishi
                   and sysdate between po.me_tarich and po.ad_tarich
                   and  po.mispar_ishi like p_prefix
                   and exists (
                         select h.kod_maamad, h.kod_ezor 
                         from tb_harshaot_idkun h 
                         where exists  (select po.isuk, po.yechida_irgunit
                                              from pivot_pirtey_ovdim po
                                              where po.mispar_ishi=p_mispar_ishi
                                                        and  sysdate between po.me_tarich and po.ad_tarich
                                                        and h.kod_isuk = po.isuk and h.kod_yechida = po.yechida_irgunit)
                         
                         and  (case when h.kod_ezor = 0 then  po.ezor  else h.kod_ezor end) = po.ezor 
                         and  (po.maamad in  (select  hm.kod_maamad_hr
                                                        from ctb_harshaot_maamad hm 
                                                        where h.kod_maamad= hm.kod_maamad
                                                        ) or h.kod_maamad=0)
                          and h.kod_isuk_nihuli is null and h.kod_yechida_nihuli is null  )                                                                    
                                                        
                                                                                      
          order by   mispar_ishi
       ) loop
    --  pipe row(tree_record(i.yechidat_aba,i.yechida_mekorit,i.oved_name,i.mispar_ishi));
      pipe row(tree_record(i.oved_name,i.mispar_ishi));
      end loop; 
          return;
  exception        
         when others then
              raise;
  end fun_get_isuk_harshaot;
  
  /*function fun_get_ez_nihuli_yechida(p_mispar_ishi in pivot_pirtey_ovdim.mispar_ishi%type) return tb_harshaot_idkun.kod_yechida_nihuli%type as
     
  v_yechida tb_harshaot_idkun.kod_yechida_nihuli%type;
  begin     
            select distinct(h.kod_yechida_nihuli) into v_yechida
            from tb_harshaot_idkun h 
            where exists  (select po.isuk, po.yechida_irgunit
                                 from pivot_pirtey_ovdim po
                                 where po.mispar_ishi=p_mispar_ishi
                                           and  sysdate between po.me_tarich and po.ad_tarich
                                           and h.kod_isuk = po.isuk and h.kod_yechida = po.yechida_irgunit);                                                     
          
      return v_yechida;
  exception        
         when others then
              raise;                      
  end fun_get_ez_nihuli_yechida;*/
function fun_get_manager_emp_by_maamad(p_prefix in varchar2,  p_mispar_ishi in ovdim.mispar_ishi%type) return  tree_table pipelined
as
 begin  
 DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Utils',action_name => 'fun_get_manager_emp_by_maamad');   
                for i in (with nihuly as (select
                                      en.yechidat_aba, en.yechida_mekorit
                                      from         kdsadmin.ez_nihuly en
                                      connect by   en.yechidat_aba = prior en.yechida_mekorit
                                      start with     en.yechida_mekorit in ( select distinct(h.kod_yechida_nihuli)
                                                                                           from tb_harshaot_idkun h 
                                                                                             where exists  (select po.isuk, po.yechida_irgunit
                                                                                                                     from pivot_pirtey_ovdim po
                                                                                                                     where po.mispar_ishi=p_mispar_ishi
                                                                                                                               and  sysdate between po.me_tarich and po.ad_tarich
                                                                                                                               and h.kod_isuk = po.isuk and h.kod_yechida = po.yechida_irgunit) ) ) --    97709
                 select   /*+ use_nl (e po o) */
                            -- e.yechidat_aba , e.yechida_mekorit ,
                             o.shem_mish || ' ' || o.shem_prat || ' (' || o.mispar_ishi || ')'
                             oved_name, o.mispar_ishi 
                 from     kdsadmin.pirtey_ovdim po, nihuly e, kdsadmin.ovdim o, pivot_pirtey_ovdim ppo1
                 where   trunc ( sysdate ) >= po.me_taarich
                            and trunc ( sysdate ) <= po.ad_taarich
                            and po.kod_natun = 1
                            and po.erech = e.yechida_mekorit
                            and o.mispar_ishi = ppo1.mispar_ishi
                            and sysdate  between ppo1.me_tarich and ppo1.ad_tarich                            
                            and o.mispar_ishi = po.mispar_ishi                                                        
                            and po.mispar_ishi like p_prefix
                            and exists (select h.kod_maamad, h.kod_ezor
                                            from tb_harshaot_idkun h
                                            where exists  (select po2.isuk, po2.yechida_irgunit
                                                                 from pivot_pirtey_ovdim po2
                                                                 where po2.mispar_ishi=p_mispar_ishi
                                                                               and  sysdate between po2.me_tarich and po2.ad_tarich
                                                                               and h.kod_isuk = po2.isuk and h.kod_yechida = po2.yechida_irgunit)
                                                                               
                                             and  (case when h.kod_ezor = 0 or   h.kod_ezor = 5 then  ppo1.ezor  else h.kod_ezor end) = ppo1.ezor
                                             and  (ppo1.maamad in  (select  hm.kod_maamad_hr
                                                                                from ctb_harshaot_maamad hm 
                                                                                 where h.kod_maamad= hm.kod_maamad
                                                                                ) or h.kod_maamad=5  or h.kod_maamad=0)                              
                                            )                                                                                                       
                       
     ) loop
     -- pipe row(tree_record(i.yechidat_aba,i.yechida_mekorit,i.oved_name,i.mispar_ishi));
      pipe row(tree_record(i.oved_name,i.mispar_ishi));
       end loop; 
          return;
   exception        
         when others then
              raise;          
end fun_get_manager_emp_by_maamad;
function fun_get_manager_emp_by_name(p_prefix in varchar2,p_mispar_ishi in pivot_pirtey_ovdim.mispar_ishi%type) return  tree_table pipelined
as
begin
 DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Utils',action_name => 'fun_get_manager_emp_by_name');   
     --for i in (select distinct a.yechidat_aba,a.yechida_mekorit,o.shem_mish || ' ' ||  o.shem_prat  || ' (' || o.mispar_ishi || ')' oved_name,a.mispar_ishi
     for i in (select distinct o.shem_mish || ' ' ||  o.shem_prat  || ' (' || o.mispar_ishi || ')' oved_name,a.mispar_ishi
                from
                     ovdim o,
                                    (select * from
                                           ez_nihuly e,
                                          (select mispar_ishi,erech from pirtey_ovdim
                                           where  trunc(sysdate)>= me_taarich
                                          and trunc(sysdate)<= ad_taarich
                                          and  kod_natun=1) p
                                           where p.erech=e.yechida_mekorit) a
                                           where o.mispar_ishi=a.mispar_ishi
                                           and   o.shem_mish || ' ' ||  o.shem_prat like p_prefix
                                           connect by a.yechidat_aba  = prior a.yechida_mekorit
                                           start with (a.yechidat_aba  =(select erech
                                                                                       from pirtey_ovdim
                                                                                       where mispar_ishi=p_mispar_ishi
                                                                                        and trunc(sysdate)>= me_taarich
                                                                                        and trunc(sysdate)<= ad_taarich
                                                                                        and  kod_natun=1)
                                                        or a.yechida_mekorit   =(select erech
                                                                                            from pirtey_ovdim
                                                                                            where mispar_ishi=p_mispar_ishi
                                                                                            and trunc(sysdate)>= me_taarich
                                                                                            and trunc(sysdate)<= ad_taarich
                                                                                            and  kod_natun=1))
            --    order by mispar_ishi
     ) loop
      --pipe row(tree_record(i.yechidat_aba,i.yechida_mekorit,i.oved_name,i.mispar_ishi));
        pipe row(tree_record(i.oved_name,i.mispar_ishi));
     end loop;
 return ;    
end fun_get_manager_emp_by_name;
function fun_get_isuk_harshaot_by_name(p_prefix in varchar2,p_mispar_ishi in pivot_pirtey_ovdim.mispar_ishi%type) return  tree_table pipelined
as
begin
 DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Utils',action_name => 'fun_get_isuk_harshaot_by_name');   
       --for i in (select 0 yechidat_aba, 0 yechida_mekorit ,o.shem_mish || ' ' || o.shem_prat || ' (' || o.mispar_ishi || ')' oved_name, o.mispar_ishi
       for i in (select o.shem_mish || ' ' || o.shem_prat || ' (' || o.mispar_ishi || ')' oved_name, o.mispar_ishi
        from 
            pivot_pirtey_ovdim po, ovdim o 
         where o.mispar_ishi = po.mispar_ishi
                   and sysdate between po.me_tarich and po.ad_tarich
                   and o.shem_mish || ' ' ||  o.shem_prat like p_prefix
                    
                   and exists (
                         select h.kod_maamad, h.kod_ezor 
                         from tb_harshaot_idkun h 
                         where exists  (select po.isuk, po.yechida_irgunit
                                              from pivot_pirtey_ovdim po
                                              where po.mispar_ishi=p_mispar_ishi
                                                        and  sysdate between po.me_tarich and po.ad_tarich
                                                        and h.kod_isuk = po.isuk and h.kod_yechida = po.yechida_irgunit)
                         
                         and  (case when h.kod_ezor = 0 then  po.ezor  else h.kod_ezor end) = po.ezor 
                         and  (po.maamad in  (select  hm.kod_maamad_hr
                                                        from ctb_harshaot_maamad hm 
                                                        where h.kod_maamad= hm.kod_maamad
                                                        ) or h.kod_maamad=0)
                           and h.kod_isuk_nihuli is null and h.kod_yechida_nihuli is null)                                                        
                                                                       
        --  order by   mispar_ishi
       ) loop
      --pipe row(tree_record(i.yechidat_aba,i.yechida_mekorit,i.oved_name,i.mispar_ishi));
      pipe row(tree_record(i.oved_name,i.mispar_ishi));
      end loop; 
          return;
  exception        
         when others then
              raise;
end fun_get_isuk_harshaot_by_name;
function fun_get_mngr_emp_by_mamad_name(p_prefix in varchar2,p_mispar_ishi in ovdim.mispar_ishi%type ) return  tree_table pipelined
as
begin
DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Utils',action_name => 'fun_get_mngr_emp_by_mamad_name');   
      for i in (with nihuly as (select
                                      en.yechidat_aba, en.yechida_mekorit
                                      from         kdsadmin.ez_nihuly en
                                      connect by   en.yechidat_aba = prior en.yechida_mekorit
                                      start with     en.yechida_mekorit in ( select distinct(h.kod_yechida_nihuli)
                                                                                           from tb_harshaot_idkun h 
                                                                                             where exists  (select po.isuk, po.yechida_irgunit
                                                                                                                     from pivot_pirtey_ovdim po
                                                                                                                     where po.mispar_ishi=p_mispar_ishi
                                                                                                                               and  sysdate between po.me_tarich and po.ad_tarich
                                                                                                                               and h.kod_isuk = po.isuk and h.kod_yechida = po.yechida_irgunit) ) ) --    97709
                 select   /*+ use_nl (e po o) */
                           --  e.yechidat_aba , e.yechida_mekorit ,
                             o.shem_mish || ' ' || o.shem_prat || ' (' || o.mispar_ishi || ')'
                             oved_name, o.mispar_ishi 
                 from     kdsadmin.pirtey_ovdim po, nihuly e, kdsadmin.ovdim o, pivot_pirtey_ovdim ppo1
                 where   trunc ( sysdate ) >= po.me_taarich
                            and trunc ( sysdate ) <= po.ad_taarich
                            and po.kod_natun = 1
                            and po.erech = e.yechida_mekorit
                            and o.mispar_ishi = ppo1.mispar_ishi
                            and sysdate  between ppo1.me_tarich and ppo1.ad_tarich                            
                            and o.mispar_ishi = po.mispar_ishi                                                        
                           and o.shem_mish || ' ' ||  o.shem_prat like p_prefix
                            and exists (select h.kod_maamad, h.kod_ezor
                                            from tb_harshaot_idkun h
                                            where exists  (select po2.isuk, po2.yechida_irgunit
                                                                 from pivot_pirtey_ovdim po2
                                                                 where po2.mispar_ishi=p_mispar_ishi
                                                                               and  sysdate between po2.me_tarich and po2.ad_tarich
                                                                               and h.kod_isuk = po2.isuk and h.kod_yechida = po2.yechida_irgunit)
                                                                               
                                             and  (case when h.kod_ezor = 0 or   h.kod_ezor = 5 then  ppo1.ezor  else h.kod_ezor end) = ppo1.ezor
                                             and  (ppo1.maamad in  (select  hm.kod_maamad_hr
                                                                              from ctb_harshaot_maamad hm 
                                                                              where h.kod_maamad= hm.kod_maamad
                                                                             ) or h.kod_maamad=5 or h.kod_maamad=0)                              
                                            )                                                                                                       
                       
     ) loop
      --pipe row(tree_record(i.yechidat_aba,i.yechida_mekorit,i.oved_name,i.mispar_ishi));
      pipe row(tree_record(i.oved_name,i.mispar_ishi));
       end loop; 
          return;
   exception        
         when others then
              raise;          
end fun_get_mngr_emp_by_mamad_name;


PROCEDURE pro_get_michsat_sidur_meafyen(p_cur OUT CurType, p_tarich_me DATE,p_tarich_ad DATE)
IS
BEGIN
    OPEN p_cur FOR
    SELECT t.kod_meafyen,t.mispar_sidur, t.me_taarich,t.ad_taarich
    FROM  TB_MICHSA_CHODSHIT_LEMEAFYEN t
    where (p_tarich_me  BETWEEN  t.ME_TAARICH  AND   NVL(t.ad_TAARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
               OR p_tarich_ad  BETWEEN  t.ME_TAARICH  AND   NVL(t.ad_TAARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                OR   (t.ME_TAARICH>=p_tarich_me  AND   NVL(t.ad_TAARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=p_tarich_ad )) ;    

END pro_get_michsat_sidur_meafyen;

function CheckShabatonOrShishi(p_mispar_ishi number,p_taarich date,p_meafyen56 number) return nvarchar2 as

  iCount NUMBER ;
begin

  if (  TO_CHAR(TRUNC(p_taarich),'d')  = '7') then
    return 'true';
  end if;
  SELECT COUNT(TRUNC(p_taarich)) INTO iCount  FROM TB_YAMIM_MEYUCHADIM WHERE taarich = TRUNC(p_taarich); 
  
  IF  (iCount> 0  ) THEN 
        SELECT COUNT(taarich)   INTO   iCount
        FROM TB_YAMIM_MEYUCHADIM Ym 
        INNER JOIN CTB_SUGEY_YAMIM_MEYUCHADIM Ctb ON  ym.sug_yom = ctb.sug_yom 
        WHERE CTB.SHBATON = 1
           and  ym.taarich = TRUNC(p_taarich);
        
         IF  (iCount> 0  ) THEN
           return 'true';
         end if;
  END IF;  
  
  if (  TO_CHAR(TRUNC(p_taarich),'d')  = '6') then
        if(p_meafyen56 =51 or p_meafyen56 =52) then
         return 'true';
        end if;
  end if;
  return 'false';
end CheckShabatonOrShishi;

procedure pro_get_status_card(p_cur OUT CurType) is
begin
  OPEN p_cur FOR
    select M.KOD_MEASHER_O_MISTAYEG kod, M.TEUR_MEASHER_O_MISTAYEG teur
    from  CTB_MEASHER_O_MISTAYEG m
    where M.PAIL='1'
    order by  M.TEUR_MEASHER_O_MISTAYEG;
end pro_get_status_card;

procedure pro_get_status_kartis_ctb(p_cur OUT CurType) is
begin
    OPEN p_cur FOR
        select o.kod_measher_o_mistayeg, o.teur_measher_o_mistayeg
        from CTB_MEASHER_O_MISTAYEG o
        order by o.kod_measher_o_mistayeg;
end pro_get_status_kartis_ctb;

procedure pro_get_pundakim_tb(p_cur OUT CurType) is
begin
 OPEN p_cur FOR
        select v.*
        from tb_pundakim_visutim v;
       
end pro_get_pundakim_tb;

procedure pro_get_snif_av_ctb(p_cur OUT CurType) is
begin
 OPEN p_cur FOR
        select v.*
        from ctb_snif_av v;
       
end pro_get_snif_av_ctb;

 procedure pro_get_breaks_details(p_cur OUT CurType) IS
BEGIN
     OPEN p_cur FOR
        SELECT  *
        FROM TB_BREAK;
                                  
          EXCEPTION
         WHEN OTHERS THEN
              RAISE;
 
 end;
procedure pro_yechida_musach_machsan_ctb(p_cur OUT CurType) is
begin
 OPEN p_cur FOR
        select y.*
        from CTB_YECHIDA_MUSACH_MACHSAN y;
       
end pro_yechida_musach_machsan_ctb;

procedure pro_get_michsa_yomit_tb(p_cur OUT CurType) is
begin
 OPEN p_cur FOR
        select m.*
        from  TB_Michsa_yomit m;
       
end pro_get_michsa_yomit_tb;

END Pkg_utils;
/