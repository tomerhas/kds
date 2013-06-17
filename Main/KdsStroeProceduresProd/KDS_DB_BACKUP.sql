CREATE OR REPLACE PACKAGE          Pkg_Calculation AS
/******************************************************************************
   NAME:       PKG_CALCULATION
   PURPOSE:

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        3/24/2011             1. Created this package.
******************************************************************************/
 TYPE    CurType      IS    REF  CURSOR;
 PROCEDURE pro_insert_oved_lechishuv(p_mis_ishi IN NUMBER,p_chodesh IN DATE);
 
 PROCEDURE pro_get_ovdim_lechishuv(p_tar_me IN DATE,p_tar_ad IN DATE,
                                    p_maamad IN NUMBER, p_ritza_gorefet IN NUMBER,
                                    p_cur OUT CurType);
PROCEDURE pro_prepare_netunim_lechishuv(p_bakasha_id number,p_tar_me IN DATE,p_tar_ad IN DATE,
                                    p_maamad IN NUMBER, p_ritza_gorefet IN NUMBER, p_num_processe IN  NUMBER);
PROCEDURE pro_InsertOvdimLechishuv(p_bakasha_id number);
PROCEDURE pro_divide_packets( p_num_processe IN  NUMBER);
PROCEDURE pro_get_ovdim(p_Cur_Ovdim OUT CurType,p_num_process IN NUMBER);
PROCEDURE pro_get_michsa_yomit(p_tar_me IN  TB_MICHSA_YOMIT.me_taarich%TYPE,
							   p_tar_ad IN  TB_MICHSA_YOMIT.ad_taarich%TYPE,
							   p_cur OUT CurType);

PROCEDURE pro_get_sidurim_meyuch_rechiv(p_tar_me IN TB_SIDURIM_MEYUCHADIM_RECHIV.me_taarich%TYPE,
		  								 p_tar_ad IN TB_SIDURIM_MEYUCHADIM_RECHIV.me_taarich%TYPE,
									 p_cur OUT CurType);
									 
PROCEDURE pro_get_sug_sidur_rechiv( p_tar_me IN TB_SIDURIM_MEYUCHADIM_RECHIV.me_taarich%TYPE,
		  						 p_tar_ad IN TB_SIDURIM_MEYUCHADIM_RECHIV.me_taarich%TYPE,
		  						 p_cur OUT CurType);
								 
PROCEDURE pro_get_premyot_view( p_cur OUT CurType, p_num_process IN NUMBER); 	
PROCEDURE pro_get_premyot_nihul_tnua( p_cur OUT CurType, p_num_process IN NUMBER);
PROCEDURE pro_get_premia_yadanit(  p_Cur OUT CurType ,p_num_process IN NUMBER);

PROCEDURE pro_get_sug_yechida(  p_cur OUT CurType, p_num_process IN NUMBER);		

PROCEDURE pro_get_sugey_sidur_tnua(  p_Cur OUT CurType);	
									 
PROCEDURE pro_get_buses_details(  p_Cur OUT CurType, p_num_process IN NUMBER ,p_tar_me IN DATE,p_tar_ad IN DATE);	

PROCEDURE pro_get_yemey_avoda ( p_status_tipul  IN  TB_YAMEY_AVODA_OVDIM.status_tipul%TYPE,
													p_num_process IN NUMBER,p_cur OUT CurType,p_tar_me IN DATE,p_tar_ad IN DATE);
PROCEDURE pro_InsertYamimLeTavla(p_bakasha_id in number,p_tar_me IN DATE,p_tar_ad IN DATE, p_num_process IN NUMBER);
PROCEDURE pro_get_kavim_process(p_cur OUT CurType,p_num_process IN NUMBER,p_tar_me IN DATE,p_tar_ad IN DATE);														
PROCEDURE pro_get_kavim_details(p_cur OUT CurType);	
PROCEDURE pro_set_kavim_details_chishuv(p_tar_me IN DATE,p_tar_ad IN DATE);
PROCEDURE pro_get_pirtey_ovdim(p_cur OUT CurType, p_num_process IN NUMBER);

PROCEDURE pro_get_meafyeney_ovdim(p_brerat_Mechadal  IN NUMBER, p_num_process IN NUMBER,
                                    p_tar_me IN DATE,p_tar_ad IN DATE, p_cur OUT CurType);
									 
PROCEDURE pro_get_peiluyot_ovdim( p_Cur OUT CurType, p_num_process IN NUMBER,p_tar_me IN DATE,p_tar_ad IN DATE);	
PROCEDURE pro_get_Matzav_Ovdim(p_Cur_Matzav OUT CurType,p_num_process IN NUMBER);
PROCEDURE pro_get_ovdim_lehishuv_premiot(p_num_process IN NUMBER);				 																				 					   				 	 		  									
/********************************/

 PROCEDURE pro_get_netunim_lechishuv( p_Cur_Ovdim OUT CurType,
  p_Cur_Michsa_Yomit OUT CurType,
 p_Cur_SidurMeyuchadRechiv OUT CurType,
  p_Cur_Sug_Sidur_Rechiv OUT CurType,
 p_Cur_Premiot_View OUT CurType,
 p_Cur_Premiot_Yadaniot OUT CurType,
 p_Cur_Sug_Yechida OUT CurType, 
  p_Cur_Yemey_Avoda OUT CurType,
  p_Cur_Pirtey_Ovdim OUT CurType,
  p_Cur_Meafyeney_Ovdim OUT CurType,
  p_Cur_Peiluyot_Ovdim OUT CurType, 
 p_Cur_Mutamut OUT CurType, 
  p_Cur_Matzav  OUT CurType, 
  p_Cur_Buses_Details OUT CurType, 
  -- p_Cur_Sugey_Sidur_Tnua OUT CurType, 
 p_Cur_Kavim_Details OUT CurType, 
 p_tar_me IN DATE,p_tar_ad IN DATE,
 p_maamad IN NUMBER, p_ritza_gorefet IN NUMBER,
 p_status_tipul  IN  TB_YAMEY_AVODA_OVDIM.status_tipul%TYPE,
 p_brerat_Mechadal  IN NUMBER, p_Mis_Ishi IN NUMBER);
 
 PROCEDURE pro_get_netunim_leprocess( p_Cur_Ovdim OUT CurType ,
  p_Cur_Michsa_Yomit OUT CurType,
 p_Cur_SidurMeyuchadRechiv OUT CurType,
 p_Cur_Sug_Sidur_Rechiv OUT CurType,
p_Cur_Premiot_View OUT CurType,
 p_Cur_Premiot_Yadaniot OUT CurType,
 p_Cur_Premiot_NihulTnua OUT CurType,
 p_Cur_Sug_Yechida OUT CurType, 
 p_Cur_Yemey_Avoda OUT CurType,
 p_Cur_Pirtey_Ovdim OUT CurType,
 p_Cur_Meafyeney_Ovdim OUT CurType,
 p_Cur_Peiluyot_Ovdim OUT CurType,   
  p_Cur_Mutamut OUT CurType, 
p_Cur_Matzav  OUT CurType, 
  p_Cur_Buses_Details OUT CurType, 
 p_Cur_Kavim_Details OUT CurType, 
 p_tar_me IN DATE,p_tar_ad IN DATE, 
 p_status_tipul  IN  TB_YAMEY_AVODA_OVDIM.status_tipul%TYPE,
 p_brerat_Mechadal  IN NUMBER,
  p_Mis_Ishi IN NUMBER,
 p_num_process IN NUMBER   );
  PROCEDURE pro_ovdim_kelet_lechishuv(p_Cur_Ovdim OUT CurType);
  
   PROCEDURE pro_idkun_sug_sidur;--(  p_Cur OUT CurType);
   
PROCEDURE   pro_Ovdim_Errors(p_Cur OUT CurType);

 procedure pro_upd_yemey_avoda_bechishuv(p_tar_me IN DATE,p_tar_ad IN DATE);
   procedure pro_upd_ymy_avoda_lo_bechishuv;
END Pkg_Calculation;
/


CREATE OR REPLACE PACKAGE          Pkg_Calc_worker AS
/******************************************************************************
   NAME:       PKG_CALC
   PURPOSE:

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        05/07/2009             1. Created this package.
******************************************************************************/


 TYPE    CurType      IS    REF  CURSOR;

PROCEDURE pro_insert_oved_lechishuv(p_mis_ishi IN NUMBER,p_chodesh IN DATE); 

PROCEDURE pro_get_michsa_yomit(p_tar_me IN  TB_MICHSA_YOMIT.me_taarich%TYPE,
                               p_tar_ad IN  TB_MICHSA_YOMIT.ad_taarich%TYPE,
                               p_cur OUT CurType);
                               
PROCEDURE pro_get_sidurim_meyuch_rechiv(p_tar_me IN TB_SIDURIM_MEYUCHADIM_RECHIV.me_taarich%TYPE,
                                           p_tar_ad IN TB_SIDURIM_MEYUCHADIM_RECHIV.me_taarich%TYPE,
                                          p_cur OUT CurType);

PROCEDURE pro_get_sug_sidur_rechiv( p_tar_me IN TB_SIDURIM_MEYUCHADIM_RECHIV.me_taarich%TYPE,
                                           p_tar_ad IN TB_SIDURIM_MEYUCHADIM_RECHIV.me_taarich%TYPE,
                                                                                                    p_cur OUT CurType);
                                                                                                    
PROCEDURE pro_get_premyot_view( p_cur OUT CurType);

PROCEDURE pro_get_premia_yadanit(  p_Cur OUT CurType);
 PROCEDURE pro_get_premyot_nihul_tnua( p_cur OUT CurType);
PROCEDURE pro_get_sug_yechida( p_cur OUT CurType);

PROCEDURE pro_get_yemey_avoda ( p_status_tipul  IN  TB_YAMEY_AVODA_OVDIM.status_tipul%TYPE,  
                                                      p_cur OUT CurType,p_tar_me IN DATE,p_tar_ad IN DATE);

PROCEDURE pro_get_pirtey_ovdim(p_cur OUT CurType);     

 PROCEDURE pro_get_peiluyot_ovdim( p_Cur OUT CurType,p_tar_me IN DATE,p_tar_ad IN DATE);
                       
 PROCEDURE pro_get_Matzav_Ovdim(p_Cur_Matzav OUT CurType);
 
 PROCEDURE pro_get_buses_details( p_Cur OUT CurType,p_tar_me IN DATE,p_tar_ad IN DATE);
 
 PROCEDURE pro_get_kavim_details(p_cur OUT CurType,p_tar_me IN DATE,p_tar_ad IN DATE);
                            
 PROCEDURE pro_get_netunim_lechishuv( p_Cur_Ovdim OUT CurType ,
  p_Cur_Michsa_Yomit OUT CurType,
 p_Cur_SidurMeyuchadRechiv OUT CurType,
 p_Cur_Sug_Sidur_Rechiv OUT CurType,
p_Cur_Premiot_View OUT CurType,
 p_Cur_Premiot_Yadaniot OUT CurType,
  p_Cur_Premiot_NihulTnua OUT CurType,
 p_Cur_Sug_Yechida OUT CurType, 
 p_Cur_Yemey_Avoda OUT CurType,
 p_Cur_Pirtey_Ovdim OUT CurType,
 p_Cur_Meafyeney_Ovdim OUT CurType,
 p_Cur_Peiluyot_Ovdim OUT CurType,   
  p_Cur_Mutamut OUT CurType, 
p_Cur_Matzav  OUT CurType, 
  p_Cur_Buses_Details OUT CurType, 
 p_Cur_Kavim_Details OUT CurType, 
 p_tar_me IN DATE,p_tar_ad IN DATE, 
 p_status_tipul  IN  TB_YAMEY_AVODA_OVDIM.status_tipul%TYPE,
 p_brerat_Mechadal  IN NUMBER, 
 p_Mis_Ishi IN NUMBER,
  p_num_process IN NUMBER  );
	END Pkg_Calc_worker;
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

PROCEDURE pro_get_Find_Worker_Card_2(p_cur OUT CurType,
                                                                P_STARTDATE IN DATE,
                                                                P_ENDDATE IN DATE ,
                                                                P_Makat IN NUMBER,
                                                                P_SIDURNUMBER IN VARCHAR,
                                                                P_CARNUMBER IN VARCHAR,
                                                                P_SNIF IN VARCHAR,
                                                                P_WORKSTATION IN VARCHAR,
                                                                P_DAYTYPE IN VARCHAR ,
                                                                P_WORKERID IN VARCHAR, 
                                                                P_SECTORISUK IN VARCHAR ,
                                                                P_SNIF_MASHAR IN VARCHAR , 
                                                                P_COMPANYID IN VARCHAR  ,
                                                                P_MAMAD IN VARCHAR 
                                                                );
                                                                
  
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
                                                                 P_WorkerViewLevel IN NUMBER, 
                                                                 P_WORKERID IN VARCHAR2, 
                                                                 P_MISPAR_ISHI IN VARCHAR2 ,
                                                                 P_STARTDATE IN DATE,
                                                                 P_ENDDATE IN DATE);
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
                                                        P_MISPAR_ISHI IN VARCHAR2,
                                                        p_Period IN VARCHAR2) ;
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
                                            P_SNIF IN VARCHAR2 ,
                                            P_MAMAD IN VARCHAR2 ,
                                            P_ISUK VARCHAR2 ,
                                            P_EZOR IN VARCHAR ,
                                            P_COMPANYID  IN VARCHAR2 ,
                                            P_MisparIshi  IN VARCHAR2 ,
                                            P_KOD_YECHIDA IN VARCHAR2 ,
                                            P_SECTORISUK IN VARCHAR2 ,
                                            p_cur OUT CurType );

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
                                            p_cur out curtype );

 PROCEDURE get_query4 ( p_mispar_ishi  in OVDIM.mispar_ishi%TYPE,p_date in varchar2,  p_cur out curtype );            
procedure get_GetDayDataEggT( p_BakashaId IN TB_BAKASHOT.bakasha_id%TYPE,  
                                            --    p_Period IN VARCHAR2,
                                                p_cur out curtype ); 
                                                
procedure pro_ProfilLinesDetails( P_STARTDATE IN DATE,
                                        P_ENDDATE IN DATE , 
                                        P_Makat IN varchar2,
                                        p_cur OUT CurType)   ;                                                                                                                       
      
procedure pro_ProfilLinesSummed( P_STARTDATE IN DATE,
                                        P_ENDDATE IN DATE , 
                                        P_Makat IN varchar2,
                                        p_cur OUT CurType)    ;       
  
PROCEDURE pro_get_Bakashot_details(p_Cur OUT CurType);                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
END Pkg_Reports;
/
CREATE OR REPLACE PACKAGE BODY          Pkg_Calculation AS
/******************************************************************************
   NAME:       PKG_CALCULATION
   PURPOSE:

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        3/24/2011             1. Created this package body.
   
******************************************************************************/

  PROCEDURE pro_insert_oved_lechishuv(p_mis_ishi IN NUMBER,p_chodesh IN DATE) IS
  
  BEGIN
  --EXECUTE IMMEDIATE 'truncate table TB_TMP_OVDIM_LECHISHUV' ; 
  	    INSERT INTO TB_TMP_OVDIM_LECHISHUV (MISPAR_ISHI,taarich)
			         VALUES(p_mis_ishi,p_chodesh);
			 
		EXCEPTION
   WHEN OTHERS THEN
            RAISE;			 
  END pro_insert_oved_lechishuv;
  
  
  PROCEDURE pro_get_ovdim_lechishuv(p_tar_me IN DATE,p_tar_ad IN DATE,
                                    p_maamad IN NUMBER, p_ritza_gorefet IN NUMBER,
                                    p_cur OUT CurType) IS
			  v_me_taarich DATE;
			  v_ad_taarich DATE;
  BEGIN
      v_me_taarich:=p_tar_me;
      v_ad_taarich:=p_tar_ad;
      
   -- EXECUTE IMMEDIATE 'truncate table TB_TMP_OVDIM_LECHISHUV' ; 
    IF  p_ritza_gorefet<>1 THEN
    INSERT INTO TB_TMP_OVDIM_LECHISHUV(MISPAR_ISHI,taarich)
      (    SELECT DISTINCT MISPAR_ISHI, TO_DATE('01/' || chodesh,'dd/mm/yyyy') taarich FROM
        ( (SELECT o.MISPAR_ISHI,y.chodesh
         --  RANK() OVER(    ORDER BY  o.mispar_ishi   ASC  ) AS num 
        FROM OVDIM o,
          (SELECT mispar_ishi, chodesh FROM
             ( (SELECT o.mispar_ishi,TO_CHAR(o.taarich,'mm/yyyy') chodesh
                    FROM TB_YAMEY_AVODA_OVDIM o ,TB_MISPAR_ISHI_CHISHUV t
               WHERE o.status=1
			 AND t.mispar_ishi=o.mispar_ishi
               AND  o.taarich BETWEEN T.TAARICH AND LAST_DAY( T.TAARICH)  )
              )
           GROUP BY mispar_ishi,chodesh) y,
         (SELECT po.maamad,po.mispar_ishi
               FROM PIVOT_PIRTEY_OVDIM PO
                 WHERE  (v_me_taarich BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
              OR   v_ad_taarich  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
              OR   po.ME_TARICH>=v_me_taarich AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=  v_ad_taarich )) p
        WHERE o.mispar_ishi=p.mispar_ishi
       AND (SUBSTR(p.maamad,0,1)=p_maamad  OR p_maamad  IS NULL)
       AND o.mispar_ishi=y.mispar_ishi  ) )  );
	--   where num<=1200 );
   --    where num<=1500
   --    minus  select  distinct mispar_ishi,TO_DATE('01/06/2010' ,'dd/mm/yyyy') taarich from TB_LOG_BAKASHOT where bakasha_id =3282 ) ;
  /*   UNION
        (SELECT p.mispar_ishi,TO_CHAR(p.taarich,'mm/yyyy') chodesh
         FROM TB_PREMYOT_YADANIYOT p,
                (SELECT po.maamad,po.mispar_ishi
               FROM PIVOT_PIRTEY_OVDIM PO
                 WHERE  (v_me_taarich BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
              OR   v_ad_taarich  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
              OR   po.ME_TARICH>=v_me_taarich AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=  v_ad_taarich )) po
         WHERE p.Taarich_idkun_acharon  >
                        (SELECT Zman_Hatchala
                        FROM TB_BAKASHOT
                        WHERE Taarich_Haavara_Lesachar =
                                          (SELECT MAX(B.Taarich_Haavara_Lesachar)
                                            FROM TB_BAKASHOT B, TB_CHISHUV_CHODESH_OVDIM CC
                                           WHERE B.Bakasha_ID = CC. Bakasha_ID
                                     AND B. Huavra_Lesachar=1
                                     AND CC. Mispar_Ishi =p.mispar_ishi ))
         AND p.mispar_ishi=po.mispar_ishi 
         AND (SUBSTR(po.maamad,0,1)=p_maamad  OR p_maamad  IS NULL)
          AND p.taarich BETWEEN v_me_taarich AND v_ad_taarich)); */
  ELSE
     INSERT INTO TB_TMP_OVDIM_LECHISHUV(MISPAR_ISHI,taarich)
	   SELECT o.MISPAR_ISHI, TO_DATE('01/' || y.chodesh,'dd/mm/yyyy')
       FROM OVDIM o,
       (SELECT mispar_ishi,TO_CHAR(taarich,'mm/yyyy') chodesh
       FROM TB_YAMEY_AVODA_OVDIM
       WHERE (status=1 OR status=2)
      AND  taarich BETWEEN v_me_taarich AND v_ad_taarich
      GROUP BY mispar_ishi,TO_CHAR(taarich,'mm/yyyy') ) y,
        (SELECT po.maamad,po.mispar_ishi
              FROM PIVOT_PIRTEY_OVDIM PO
                WHERE  (v_me_taarich BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
             OR   v_ad_taarich  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
             OR   po.ME_TARICH>=v_me_taarich AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=  v_ad_taarich )) p
       WHERE o.mispar_ishi=p.mispar_ishi
      AND (SUBSTR(p.maamad,0,1)=p_maamad  OR p_maamad  IS NULL)
      AND o.mispar_ishi=y.mispar_ishi ;

  END IF;

   OPEN p_cur FOR
   SELECT * FROM TB_TMP_OVDIM_LECHISHUV;
 
EXCEPTION
   WHEN OTHERS THEN
            RAISE;

END  pro_get_ovdim_lechishuv;

PROCEDURE pro_prepare_netunim_lechishuv(p_bakasha_id number,p_tar_me IN DATE,p_tar_ad IN DATE,
                                    p_maamad IN NUMBER, p_ritza_gorefet IN NUMBER, p_num_processe IN  NUMBER) IS
              v_me_taarich DATE;
              v_ad_taarich DATE;

  BEGIN
  
    EXECUTE IMMEDIATE 'truncate table TB_MISPAR_ISHI_CHISHUV' ; 
    EXECUTE IMMEDIATE 'truncate table TB_CATALOG_CHISHUV' ; 
  --  EXECUTE IMMEDIATE 'truncate table tb_yamim_Lechishuv' ; 
      v_me_taarich:=p_tar_me;
      v_ad_taarich:=p_tar_ad;
      
--v_me_taarich:=to_date('01/08/2012','dd/mm/yyyy');
--v_ad_taarich:=to_date('31/10/2012','dd/mm/yyyy');
      
       IF  p_ritza_gorefet<>1 THEN
    INSERT INTO TB_MISPAR_ISHI_CHISHUV(ROW_NUM,MISPAR_ISHI,TAARICH, NUM_pack)
      (    SELECT  ROWNUM,MISPAR_ISHI, TO_DATE('01/' || chodesh,'dd/mm/yyyy') taarich,0
          FROM
      (  ( (SELECT DISTINCT  o.MISPAR_ISHI,y.chodesh
         --  RANK() OVER(    ORDER BY  o.mispar_ishi   ASC  ) AS num 
        FROM OVDIM o,
          (SELECT mispar_ishi, chodesh FROM
             ( (SELECT o.mispar_ishi,TO_CHAR(o.taarich,'mm/yyyy') chodesh
                    FROM TB_YAMEY_AVODA_OVDIM o  
       --  ,     TB_MISPAR_ISHI_CHISHUV_BAK t
               WHERE o.status=1
         AND  o.taarich BETWEEN v_me_taarich AND v_ad_taarich
                --and o.MISPAR_ISHI =44965
      -- AND T.NUM_PACK=102--
     -- AND t.mispar_ishi=o.mispar_ishi--
 --AND  o.taarich BETWEEN T.TAARICH AND LAST_DAY( T.TAARICH)  --
              ))
           GROUP BY mispar_ishi,chodesh) y,
         (SELECT po.maamad,po.mispar_ishi,PO.DIRUG,PO.DARGA
               FROM PIVOT_PIRTEY_OVDIM PO
                 WHERE  (v_me_taarich BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
              OR   v_ad_taarich  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
              OR   po.ME_TARICH>=v_me_taarich AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=  v_ad_taarich )) p
        WHERE o.mispar_ishi=p.mispar_ishi
       AND (SUBSTR(p.maamad,0,1)=p_maamad  OR p_maamad  IS NULL)
       AND O.KOD_HEVRA<>6486
       and not( p.dirug=13 and (p.darga=64 or p.darga=70 or p.darga=2 or p.darga=3))
       and not( p.dirug=12 and ((p.darga>=21 and p.darga<=30 ) or (p.darga>=62 and p.darga<=72 )))
       AND o.mispar_ishi=y.mispar_ishi
    
         ) ) 
    --   where num<=1200 );
   --    where num<=1500
   --    minus  select  distinct mispar_ishi,TO_DATE('01/06/2010' ,'dd/mm/yyyy') taarich from TB_LOG_BAKASHOT where bakasha_id =3282 ) ;
     UNION
      SELECT DISTINCT x.mispar_ishi,x.chodesh
      FROM
       (SELECT p.mispar_ishi,TO_CHAR(p.taarich,'mm/yyyy') chodesh
         FROM TB_PREMYOT_YADANIYOT p,
                   TB_YAMEY_AVODA_OVDIM o,
    --  TB_MISPAR_ISHI_CHISHUV_BAK t,
                (SELECT po.maamad,po.mispar_ishi,PO.DIRUG,PO.DARGA
               FROM PIVOT_PIRTEY_OVDIM PO
                 WHERE  (v_me_taarich BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
              OR   v_ad_taarich  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
              OR   po.ME_TARICH>=v_me_taarich AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=  v_ad_taarich )) po
         WHERE p.Taarich_idkun_acharon  >
                        (SELECT Zman_Hatchala
                        FROM TB_BAKASHOT
                        WHERE Taarich_Haavara_Lesachar =
                                          (SELECT MAX(B.Taarich_Haavara_Lesachar)
                                            FROM TB_BAKASHOT B, TB_CHISHUV_CHODESH_OVDIM CC
                                           WHERE B.Bakasha_ID = CC. Bakasha_ID
                                                AND B. Huavra_Lesachar=1
                                                AND CC. Mispar_Ishi =p.mispar_ishi
                                                AND TO_CHAR(CC.TAARICH,'mm/yyyy') =  TO_CHAR(p.TAARICH,'mm/yyyy')  ))
         AND p.mispar_ishi=po.mispar_ishi 
         AND o.mispar_ishi=po.mispar_ishi 
       AND (SUBSTR(po.maamad,0,1)=p_maamad  OR p_maamad  IS NULL)
     AND  o.taarich BETWEEN v_me_taarich AND v_ad_taarich
    AND p.taarich BETWEEN v_me_taarich AND v_ad_taarich
          AND  o.status=2
    -- AND T.NUM_PACK=102--
   --AND t.mispar_ishi=o.mispar_ishi--
   -- AND  p.taarich BETWEEN T.TAARICH AND LAST_DAY( T.TAARICH)  --
          ) x ));
  ELSE
     INSERT INTO TB_MISPAR_ISHI_CHISHUV(MISPAR_ISHI,taarich)
       SELECT o.MISPAR_ISHI, TO_DATE('01/' || y.chodesh,'dd/mm/yyyy')
       FROM OVDIM o,
       (SELECT mispar_ishi,TO_CHAR(taarich,'mm/yyyy') chodesh
       FROM TB_YAMEY_AVODA_OVDIM
       WHERE (status=1 OR status=2)
      AND  taarich BETWEEN v_me_taarich AND v_ad_taarich
      GROUP BY mispar_ishi,TO_CHAR(taarich,'mm/yyyy') ) y,
        (SELECT po.maamad,po.mispar_ishi,PO.DIRUG,PO.DARGA
              FROM PIVOT_PIRTEY_OVDIM PO
                WHERE  (v_me_taarich BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
             OR   v_ad_taarich  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
             OR   po.ME_TARICH>=v_me_taarich AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=  v_ad_taarich )) p
       WHERE o.mispar_ishi=p.mispar_ishi
      AND (SUBSTR(p.maamad,0,1)=p_maamad  OR p_maamad  IS NULL)
       AND O.KOD_HEVRA<>6486
         and not( p.dirug=13 and (p.darga=64 or p.darga=70 or p.darga=2 or p.darga=3))
       and not( p.dirug=12 and ((p.darga>=21 and p.darga<=30 ) or (p.darga>=62 and p.darga<=72 )))
      AND o.mispar_ishi=y.mispar_ishi ;
  END IF;
  Pkg_Calculation.pro_divide_packets(p_num_processe);
  
  Pkg_Calculation.pro_set_kavim_details_chishuv(v_me_taarich,v_ad_taarich);
  Pkg_Calculation.pro_upd_yemey_avoda_bechishuv(v_me_taarich,v_ad_taarich);
  Pkg_Calculation.pro_InsertYamimLeTavla(p_bakasha_id,v_me_taarich,v_ad_taarich,p_num_processe );
  Pkg_Calculation.pro_InsertOvdimLechishuv(p_bakasha_id);
EXCEPTION
   WHEN OTHERS THEN
            RAISE;

END  pro_prepare_netunim_lechishuv;

PROCEDURE pro_InsertOvdimLechishuv(p_bakasha_id number) IS
BEGIN
    INSERT INTO TB_MISPAR_ISHI_CHISHUV_HISTORY
        SELECT DISTINCT p_bakasha_id,  os.MISPAR_ISHI, os.TAARICH
            FROM   TB_MISPAR_ISHI_CHISHUV OS;      
  EXCEPTION
       WHEN OTHERS THEN
                RAISE;
END pro_InsertOvdimLechishuv;
PROCEDURE pro_divide_packets( p_num_processe IN  NUMBER) IS
   num NUMBER;
BEGIN

 SELECT COUNT(*) INTO num FROM TB_MISPAR_ISHI_CHISHUV; 
  
  UPDATE TB_MISPAR_ISHI_CHISHUV s
  SET s.num_pack = (SELECT  TRUNC(row_num*p_num_processe/num)+1 FROM dual);-- (select trunc(rownum*p_num_processe/num + 1) from TB_MISPAR_ISHI_CHISHUV);
  
  UPDATE TB_MISPAR_ISHI_CHISHUV s
  SET s.num_pack =p_num_processe
  WHERE s.num_pack=(p_num_processe+1);
  
  EXCEPTION
   WHEN OTHERS THEN
            RAISE;
END pro_divide_packets;
 
PROCEDURE pro_set_kavim_details_chishuv(p_tar_me IN DATE,p_tar_ad IN DATE) IS
    rc NUMBER;
BEGIN


    BEGIN
       INSERT INTO TMP_CATALOG_DETAILS@kds_gw_at_tnpr
                              (activity_date,makat8)
       SELECT DISTINCT   po.TAARICH ,po.MAKAT_NESIA
       FROM TB_SIDURIM_OVDIM o,TB_PEILUT_OVDIM po  , TB_MISPAR_ISHI_CHISHUV s
       WHERE o.mispar_ishi=  s.mispar_ishi
         AND O.TAARICH BETWEEN p_tar_me AND p_tar_ad
         AND  o.taarich  BETWEEN s.taarich  AND LAST_DAY(s.taarich)
         AND o.mispar_ishi = po.mispar_ishi
          AND o.taarich = po.taarich
          AND o.mispar_sidur= po.mispar_sidur
          AND o.shat_hatchala = po.shat_hatchala_sidur ;
       EXCEPTION
              WHEN DUP_VAL_ON_INDEX THEN
                   NULL;
     END;
     
    BEGIN 
     kds.kds_catalog_pack.GetKavimDetails@kds_gw_at_tnpr(rc); 
       INSERT INTO TB_CATALOG_CHISHUV( activity_date,makat8, Shilut,Description,nihul_name,mazan_tashlum,mazan_tichnun,
                                                                Km,sug_shirut_name,eilat_trip,onatiut,kisuy_tor,eshel,migun,xy_moked_tchila,xy_moked_siyum,
                                                                snif,snif_name,sug_auto ) 
                            SELECT activity_date, makat8, Shilut,Description,nihul_name,mazan_tashlum,mazan_tichnun,Km,sug_shirut_name,
                                        eilat_trip,onatiut,kisui_tor,eshel,migun,xy_moked_tchila,xy_moked_siyum,snif,snif_name,sug_auto   
                            FROM kds.TMP_CATALOG_DETAILS@kds_gw_at_tnpr;
    END;

 --COMMIT;
  EXCEPTION
       WHEN OTHERS THEN
                RAISE;
END  pro_set_kavim_details_chishuv;

PROCEDURE pro_get_ovdim(p_Cur_Ovdim OUT CurType,p_num_process IN NUMBER) IS
BEGIN
     OPEN p_Cur_Ovdim FOR
        SELECT s.MISPAR_ISHI,s.taarich
        FROM TB_MISPAR_ISHI_CHISHUV s
        WHERE S.NUM_PACK = p_num_process;
      /*   and S.MISPAR_ISHI in(   select  mispar_ishi
                                               from tb_log_bakashot 
                                                where  BAKASHA_ID = 6468
                                                AND TEUR_HODAA like  'CalcDay:    at System.Collections.Generic.Dictionary`2.get_Item(TKey key)%');*/

END pro_get_ovdim;

PROCEDURE pro_get_michsa_yomit(p_tar_me IN  TB_MICHSA_YOMIT.me_taarich%TYPE,
							   p_tar_ad IN  TB_MICHSA_YOMIT.ad_taarich%TYPE,
							   p_cur OUT CurType)
IS
BEGIN
   DBMS_APPLICATION_INFO.SET_MODULE('Pkg_Calculation.pro_get_michsa_yomit','get michsot yomiyot');
  
    OPEN p_cur FOR
	    SELECT  kod_michsa,sug_yom ,me_taarich, NVL(AD_TAARICH,TO_DATE('01/01/9999','dd/mm/yyyy')) ad_taarich,michsa,Shavoa_Avoda
        FROM TB_MICHSA_YOMIT
		WHERE me_taarich<=p_tar_ad 
		AND NVL(AD_TAARICH,TO_DATE('01/01/9999','dd/mm/yyyy'))>=p_tar_me;

		  EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;
END pro_get_michsa_yomit;

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


PROCEDURE pro_get_premyot_view( p_cur OUT CurType, p_num_process IN NUMBER)
IS
BEGIN
 DBMS_APPLICATION_INFO.SET_MODULE('PKG_UTILS.pro_get_premyot_view',' get premyot view ');
  
   OPEN p_cur FOR
        --    SELECT 0 Sug_premia,0  Sum_dakot FROM dual;
	SELECT v.Mispar_ishi,s.taarich,Sug_premia,SUM(Dakot_premia) Sum_dakot
		FROM TB_PREM v, TB_MISPAR_ISHI_CHISHUV s
		WHERE v.Mispar_ishi =s.Mispar_ishi
		 AND v.Tkufa =  TO_CHAR(s.taarich,'yyyyMM')
         AND S.NUM_PACK = p_num_process
		GROUP BY (v.Mispar_ishi,s.taarich, v.Sug_premia);
  
	EXCEPTION
		     WHEN OTHERS THEN
             OPEN p_cur FOR
               SELECT 0 mispar_ishi,SYSDATE taarich,  0 Sug_premia,0  Sum_dakot FROM dual;
              --  SELECT 0 Sug_premia,0  Sum_dakot FROM dual;
		--		 RAISE;
END pro_get_premyot_view;


PROCEDURE pro_get_premyot_nihul_tnua( p_cur OUT CurType, p_num_process IN NUMBER)
IS
BEGIN
 DBMS_APPLICATION_INFO.SET_MODULE('PKG_UTILS.pro_get_premyot_view',' get premyot view ');
  
   OPEN p_cur FOR
        --    SELECT 0 Sug_premia,0  Sum_dakot FROM dual;
    SELECT v.Mispar_ishi,s.taarich,Sug_premia,SUM(Dakot_premia) Sum_dakot
        FROM TB_PREMYOT_NIHUL_TNUA v, TB_MISPAR_ISHI_CHISHUV s
        WHERE v.Mispar_ishi =s.Mispar_ishi
         AND v.Tkufa = s.taarich
         AND S.NUM_PACK = p_num_process
        GROUP BY (v.Mispar_ishi,s.taarich, v.Sug_premia);
  
    EXCEPTION
             WHEN OTHERS THEN
             OPEN p_cur FOR
               SELECT 0 mispar_ishi,SYSDATE taarich,  0 Sug_premia,0  Sum_dakot FROM dual;
              --  SELECT 0 Sug_premia,0  Sum_dakot FROM dual;
        --         RAISE;
END pro_get_premyot_nihul_tnua;
PROCEDURE pro_get_premia_yadanit(  p_Cur OUT CurType, p_num_process IN NUMBER)
IS
BEGIN
   DBMS_APPLICATION_INFO.SET_MODULE('PKG_SUG_UTILS.pro_get_premia_yadanit',' get premia yadanit');
  
	 OPEN p_cur FOR
	 	  SELECT  s.MISPAR_ISHI,s.taarich ,DAKOT_PREMYA, Taarich_idkun_acharon,sug_premya
		  FROM TB_PREMYOT_YADANIYOT p,  TB_MISPAR_ISHI_CHISHUV s
		  WHERE p.MISPAR_ISHI = s.MISPAR_ISHI
		    AND p.TAARICH BETWEEN s.taarich AND LAST_DAY(s.taarich)  --TO_CHAR(p.TAARICH,'mm/yyyy') = TO_CHAR(s.taarich,'mm/yyyy')
            AND S.NUM_PACK = p_num_process;

	EXCEPTION
		    WHEN OTHERS THEN
				 RAISE;
END pro_get_premia_yadanit;

PROCEDURE pro_get_sug_yechida( p_cur OUT CurType, p_num_process IN NUMBER)
IS
BEGIN 
	 -- DBMS_APPLICATION_INFO.SET_MODULE('PKG_OVDIM.fun_get_sug_yechida_oved','get sug yechida leoved  ');
      OPEN p_Cur FOR 
	  	   SELECT o.mispar_ishi,y.SUG_YECHIDA,  p.me_tarich,p.ad_tarich 
		   FROM  	OVDIM o,CTB_YECHIDA y, 
		            (  SELECT   t.mispar_ishi , t.YECHIDA_IRGUNIT ,t.ME_TARICH,
					 	  			-- MAX( t.ME_TARICH) OVER (PARTITION BY t.mispar_ishi) me_tarich,
									 t.ad_tarich 
					 				-- row_number() OVER (PARTITION BY t.mispar_ishi ORDER BY me_tarich) seq
					        FROM  PIVOT_PIRTEY_OVDIM t,TB_MISPAR_ISHI_CHISHUV s
					        WHERE t.mispar_ishi= s.mispar_ishi
							    AND t.isuk IS NOT NULL 
							    AND ( s.taarich BETWEEN ME_TARICH  AND NVL(t.ad_TARICH,TO_DATE(  '01/01/9999'  ,  'dd/mm/yyyy'  ))
							         OR LAST_DAY(s.taarich) BETWEEN ME_TARICH AND NVL(t.ad_TARICH,TO_DATE(  '01/01/9999'  ,  'dd/mm/yyyy'  )) 
					                 OR t.ME_TARICH>= s.taarich AND NVL(t.ad_TARICH,TO_DATE(  '01/01/9999'  , 'dd/mm/yyyy'  ))<= LAST_DAY(s.taarich) )
                                AND S.NUM_PACK = p_num_process
							 ORDER BY mispar_ishi ) p
					  
			 WHERE o.mispar_ishi= p.mispar_ishi 
			 AND y.KOD_HEVRA=o.KOD_HEVRA
			 AND y.KOD_YECHIDA=p.YECHIDA_IRGUNIT;
   
	EXCEPTION
		    WHEN OTHERS THEN
				 RAISE;

END  pro_get_sug_yechida;

PROCEDURE pro_get_sugey_sidur_tnua(  p_Cur OUT CurType) AS
  GeneralQry VARCHAR2(3000);
  CountQry VARCHAR2(3000);
  CountRows NUMBER;
  InsertQry  VARCHAR2(3000); 
  
	 rc NUMBER ;   
BEGIN
	 DBMS_APPLICATION_INFO.SET_MODULE('PKG_SUG_SIDUR.pro_get_sugey_sidur_tnua','get sugey sidur from tnua loved letkyfa ');
 
    INSERT INTO  kds.TMP_SUG_SIDUR@KDS_GW_AT_TNPR(ACTIVE_DATE,SIDUR_AVODA)
       SELECT DISTINCT  t.TAARICH,t.MISPAR_SIDUR   FROM TB_SIDURIM_OVDIM t,TB_TMP_OVDIM_LECHISHUV o WHERE   t.MISPAR_ISHI = o.MISPAR_ISHI AND
                   t.MISPAR_SIDUR NOT LIKE '99%'  AND t.taarich BETWEEN o.taarich  AND LAST_DAY(o.taarich)  AND  ( LENGTH(t.MISPAR_SIDUR) BETWEEN 4 AND 5);
           
 
  
      Kds_Sidur_Avoda_Pack.Get_Sug_Sidur@KDS_GW_AT_TNPR(rc);
 
          INSERT INTO TMP_SIDURIM ( TAARICH,SIDUR_AVODA,SUG_SIDUR,Sug_Sidur_Name,Status) 
                                SELECT   Active_Date,Sidur_Avoda,SUG_SIDUR,Sug_Sidur_Name,Status
                            FROM   kds.TMP_SUG_SIDUR@KDS_GW_AT_TNPR ;
    
	  
	  OPEN p_Cur FOR	
	  	   SELECT TAARICH,SIDUR_AVODA mispar_sidur,SUG_SIDUR
		   FROM TMP_SIDURIM; 
		   
	EXCEPTION 
         WHEN OTHERS THEN 
               RAISE; 	   
END pro_get_sugey_sidur_tnua;

PROCEDURE pro_get_buses_details( p_Cur OUT CurType, p_num_process IN NUMBER,p_tar_me IN DATE,p_tar_ad IN DATE)
IS
 
BEGIN
 
      DBMS_APPLICATION_INFO.SET_MODULE('PKG_TNUA.pro_get_buses_details','get buses details from mashar ');
  
     OPEN p_Cur FOR
	     SELECT license_number, bus_number,Vehicle_Type
	     FROM VEHICLE_SPECIFICATIONS
 --VCL_GENERAL_VEHICLE_VIEW@kds2maale
	     WHERE bus_number IN (SELECT DISTINCT p.oto_no
		 FROM TB_PEILUT_OVDIM p,TB_MISPAR_ISHI_CHISHUV o
		 WHERE p.MISPAR_ISHI = o.MISPAR_ISHI  
          AND    p.taarich BETWEEN p_tar_me AND p_tar_ad
            AND o.NUM_PACK = p_num_process
		    AND  p.taarich BETWEEN o.taarich  AND LAST_DAY(o.taarich) );
      
EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END pro_get_buses_details;


PROCEDURE pro_get_yemey_avoda ( p_status_tipul  IN  TB_YAMEY_AVODA_OVDIM.status_tipul%TYPE,
													p_num_process IN NUMBER,	p_cur OUT CurType,p_tar_me IN DATE,p_tar_ad IN DATE) IS
	BEGIN
	 DBMS_APPLICATION_INFO.SET_MODULE('PKG_CAOLC.pro_get_yemey_avoda_to_oved','get yemey avoda to oved');
  
		 OPEN p_cur FOR
			 	     SELECT       Y.MISPAR_ISHI, Y.TAARICH, Y.SHAT_HATCHALA, Y.SHAT_SIYUM,
                       S.MISPAR_SIDUR, NVL ( S.LO_LETASHLUM, 0 ) LO_LETASHLUM,
                       S.SHAT_HATCHALA SHAT_HATCHALA_SIDUR, S.SHAT_HATCHALA_LETASHLUM,
                       S.SHAT_GMAR_LETASHLUM, S.SHAT_GMAR SHAT_GMAR_SIDUR,
                       TO_CHAR ( S.TAARICH, 'd' ) DAY_TAARICH, Y.HAMARAT_SHABAT,
                       Y.TACHOGRAF, V_SIDURIM.SIDUR_MISUG_HEADRUT, V_SIDURIM.SECTOR_AVODA,
                       V_SIDURIM.SUG_AVODA, V_SIDURIM.SUG_SIDUR SUG_SIDUR_MEYUCHAD,
                       V_SIDURIM.SIDUR_NAMLAK_VISA,V_SIDURIM.tashlum_kavua_beshishi,
                       DECODE ( S.OUT_MICHSA, NULL, 0, S.OUT_MICHSA ) OUT_MICHSA, Y.LINA,
                       Y.HALBASHA, S.PITZUL_HAFSAKA,
                       NVL ( S.MEZAKE_HALBASHA, 0 ) MEZAKE_HALBASHA,
                       V_SIDURIM.MICHSAT_SHAOT_CHODSHIT, V_SIDURIM.MAX_DAKOT_BODED,
                       V_SIDURIM.MAX_SHAOT_BYOM_SHISHI, V_SIDURIM.MAX_SHAOT_BESHABATON,
                       NVL ( V_SIDURIM.ZAKAY_LEPIZUL, 0 ) ZAKAY_LEPIZUL,
                       V_SIDURIM.DAKOT_N_LETASHLUM_HOL,V_SIDURIM.michsat_shishi_lebaley_x,
                       NVL ( S.SUG_HASHLAMA, 0 ) SUG_HASHLAMA,
                       V_SIDURIM.SUG_SHAOT_BYOM_HOL_IF_MIGBALA,V_SIDURIM.shabaton_tashlum_kavua,
                        V_SIDURIM.sug_hashaot_beyom_shishi,V_SIDURIM.max_shaot_byom_hol,
                       V_SIDURIM.sug_hashaot_beyom_shabaton,v_sidurim.shat_gmar_auto,
                       NVL ( S.HASHLAMA, 0 ) HASHLAMA, S.BITUL_O_HOSAFA, Y.HASHLAMA_LEYOM,
                       NVL ( S.YOM_VISA, 0 ) YOM_VISA, V_SIDURIM.EIN_LESHALEM_TOS_LILA,
                       NVL ( Y.ZMAN_NESIA_HALOCH, 0 ) ZMAN_NESIA_HALOCH,
                       NVL ( Y.ZMAN_NESIA_HAZOR, 0 ) ZMAN_NESIA_HAZOR,v_sidurim.realy_veod_yom,
                       NVL(v_sidurim.zakay_lehamara,0) zakay_lehamara  ,v_sidurim.tashlum_kavua_bchol,
                       NVL ( S.HAFHATAT_NOCHECHUT_VISA, 0 ) HAFHATAT_NOCHECHUT_VISA,
                       NVL ( S.MEZAKE_NESIOT, 0 ) MEZAKE_NESIOT, Y.BITUL_ZMAN_NESIOT,
                       NVL ( S.KOD_SIBA_LEDIVUCH_YADANI_IN, 0 ) KOD_SIBA_LEDIVUCH_YADANI_IN,
                       NVL ( S.KOD_SIBA_LEDIVUCH_YADANI_OUT, 0 )
                           KOD_SIBA_LEDIVUCH_YADANI_OUT,V_SIDURIM.sidur_rak_lechevrot_banot,
                       NVL ( S.ACHUZ_KNAS_LEPREMYAT_VISA, 0 ) ACHUZ_KNAS_LEPREMYAT_VISA,
                       NVL ( S.ACHUZ_VIZA_BESIKUN, 0 ) ACHUZ_VIZA_BESIKUN,
                       S.MIKUM_SHAON_KNISA, S.MIKUM_SHAON_YETZIA,
                       V_SIDURIM.ZAKAY_LECHISHUV_RETZIFUT, NVL ( S.SUG_SIDUR, 0 ) SUG_SIDUR,
                       V_SIDURIM.matala_klalit_lelo_rechev,nvl(V_SIDURIM.zakaut_legmul_chisachon,0) zakaut_legmul_chisachon,
                       V_SIDURIM.shaon_nochachut
            FROM   TB_MISPAR_ISHI_CHISHUV OS,       
                   OVDIM O,
                   TB_YAMEY_AVODA_OVDIM Y,      
                       ( SELECT * FROM TB_SIDURIM_OVDIM s
                       WHERE   ( (s.lo_letashlum=0 OR s.lo_letashlum IS NULL)
                                   OR  (s.lo_letashlum=1 AND s.sug_sidur=69  ))
                             AND s.mispar_sidur<>99200
                            AND  s.taarich BETWEEN p_tar_me AND p_tar_ad) s,
                   PIVOT_SIDURIM_MEYUCHADIM V_SIDURIM
            WHERE  os.MISPAR_ISHI = o.MISPAR_ISHI 
                    AND    os.MISPAR_ISHI = Y.MISPAR_ISHI  
                    AND    Y.TAARICH BETWEEN os.TAARICH AND LAST_DAY ( os.TAARICH )  
                    AND    Y.TAARICH = s.TAARICH (+)
                    AND    y.MISPAR_ISHI = s.MISPAR_ISHI (+)
                    AND    V_SIDURIM.MISPAR_SIDUR(+) = S.MISPAR_SIDUR
                               AND S.TAARICH BETWEEN V_SIDURIM.ME_TARICH(+)
                                                 AND  V_SIDURIM.AD_TARICH(+)
                    AND Y.TAARICH BETWEEN p_tar_me AND p_tar_ad
                    AND OS.NUM_PACK =p_num_process 
                    AND ( Y.STATUS = 1 OR Y.STATUS = 2 )
                    AND NOT NVL ( S.BITUL_O_HOSAFA, 0 ) IN (1, 3)
                --    AND (( S.LO_LETASHLUM = 0 OR S.LO_LETASHLUM IS NULL)   OR ( S.LO_LETASHLUM = 1  AND S.SUG_SIDUR = 69 ))
                   --AND nvl(S.MISPAR_SIDUR,0) <> 99200    
               ORDER BY y.taarich ASC;
               
            --   EXECUTE IMMEDIATE 'truncate table tb_yamim_Lechishuv' ; 
         
  EXCEPTION
       WHEN OTHERS THEN
				RAISE;
END  pro_get_yemey_avoda;

PROCEDURE pro_InsertYamimLeTavla(p_bakasha_id in number,p_tar_me IN DATE,p_tar_ad IN DATE , p_num_process IN NUMBER) IS
BEGIN
    INSERT INTO TB_YAMIM_LECHISHUV
        SELECT DISTINCT  p_bakasha_id, Y.MISPAR_ISHI, Y.TAARICH, Y.STATUS
            FROM   TB_MISPAR_ISHI_CHISHUV OS,       
                   OVDIM O,
                   TB_YAMEY_AVODA_OVDIM Y,      
                       ( SELECT * FROM TB_SIDURIM_OVDIM s
                       WHERE   ( (s.lo_letashlum=0 OR s.lo_letashlum IS NULL)
                                   OR  (s.lo_letashlum=1 AND s.sug_sidur=69  ))
                             AND s.mispar_sidur<>99200
                            AND  s.taarich BETWEEN p_tar_me AND p_tar_ad) s,
                   PIVOT_SIDURIM_MEYUCHADIM V_SIDURIM
            WHERE  os.MISPAR_ISHI = o.MISPAR_ISHI 
                    AND    os.MISPAR_ISHI = Y.MISPAR_ISHI  
                    AND    Y.TAARICH BETWEEN os.TAARICH AND LAST_DAY ( os.TAARICH )  
                    AND    Y.TAARICH = s.TAARICH (+)
                    AND    y.MISPAR_ISHI = s.MISPAR_ISHI (+)
                    AND    V_SIDURIM.MISPAR_SIDUR(+) = S.MISPAR_SIDUR
                               AND S.TAARICH BETWEEN V_SIDURIM.ME_TARICH(+)
                                                 AND  V_SIDURIM.AD_TARICH(+)
                    AND Y.TAARICH BETWEEN p_tar_me AND p_tar_ad
                   -- AND OS.NUM_PACK =p_num_process 
                    AND ( Y.STATUS = 1 OR Y.STATUS = 2 )
                    AND NOT NVL ( S.BITUL_O_HOSAFA, 0 ) IN (1, 3)
                --    AND (( S.LO_LETASHLUM = 0 OR S.LO_LETASHLUM IS NULL)   OR ( S.LO_LETASHLUM = 1  AND S.SUG_SIDUR = 69 ))
                   --AND nvl(S.MISPAR_SIDUR,0) <> 99200    
               ORDER BY y.taarich ASC;

  EXCEPTION
       WHEN OTHERS THEN
                RAISE;
END pro_InsertYamimLeTavla;
PROCEDURE pro_get_kavim_process(p_cur OUT CurType,p_num_process IN NUMBER,p_tar_me IN DATE,p_tar_ad IN DATE) IS
BEGIN
    OPEN p_cur FOR
        WITH ALL_DATES AS (SELECT /*+ materialize */
                                                                                    DISTINCT TAARICH
                                                   FROM             KDSADMIN.TB_MISPAR_ISHI_CHISHUV
                                                   WHERE           NUM_PACK = p_num_process),
             ids AS ( 
            SELECT   DISTINCT   S.MISPAR_ISHI, PO.MAKAT_NESIA , PO.TAARICH
            FROM     KDSADMIN.TB_PEILUT_OVDIM PO,        
                     KDSADMIN.TB_MISPAR_ISHI_CHISHUV S
            WHERE         PO.MISPAR_ISHI = S.MISPAR_ISHI
                     AND S.TAARICH IN (SELECT    TAARICH FROM ALL_DATES)
                     AND PO.TAARICH BETWEEN S.TAARICH AND LAST_DAY ( S.TAARICH )
                     AND S.NUM_PACK =  p_num_process
                     )         
            SELECT              S.MISPAR_ISHI, C.*
            FROM    KDSADMIN.TB_CATALOG_CHISHUV C,  ids S
            WHERE  S.MAKAT_NESIA = C.MAKAT8
                                     AND S.TAARICH = C.ACTIVITY_DATE;

      --  WITH all_dates AS 
          --  (SELECT /*+ materialize */   DISTINCT taarich FROM  kdsadmin.TB_MISPAR_ISHI_CHISHUV 
         /*    WHERE  NUM_PACK = p_num_process)
            SELECT     S.MISPAR_ISHI, C.*
            FROM     kdsadmin.TB_CATALOG_CHISHUV C, 
                     kdsadmin.TB_PEILUT_OVDIM PO, 
                     kdsadmin.TB_MISPAR_ISHI_CHISHUV S
            WHERE         PO.MISPAR_ISHI = S.MISPAR_ISHI
                     AND S.TAARICH IN (SELECT taarich FROM all_dates)
                     AND PO.TAARICH BETWEEN S.TAARICH AND LAST_DAY ( S.TAARICH )
                     AND S.NUM_PACK =p_num_process
                     AND PO.MAKAT_NESIA = C.MAKAT8
                     AND PO.TAARICH = C.ACTIVITY_DATE;*/
/*
        SELECT s.mispar_ishi ,c.* 
            FROM TB_CATALOG_CHISHUV c,TB_PEILUT_OVDIM po,  TB_MISPAR_ISHI_CHISHUV s
            WHERE po.mispar_ishi=s.mispar_ishi 
            AND po.taarich  BETWEEN  p_tar_me  AND p_tar_ad
            AND    po.taarich  BETWEEN  s.taarich  AND LAST_DAY(s.taarich)
            AND S.NUM_PACK = p_num_process
            AND po.MAKAT_NESIA = c.makat8
            AND po.taarich = c.activity_date; */

    EXCEPTION
            WHEN OTHERS THEN
                 RAISE;
END pro_get_kavim_process;

PROCEDURE pro_get_kavim_details(p_cur OUT CurType) IS
    v_count  NUMBER;
    rc NUMBER;
BEGIN
   DBMS_APPLICATION_INFO.SET_MODULE('pkg_tnua.pro_get_kavim_details','get kavim details from tnua');
    SELECT COUNT(po.mispar_ishi) INTO v_count
    FROM TB_SIDURIM_OVDIM o,TB_PEILUT_OVDIM po , TB_TMP_OVDIM_LECHISHUV s
    WHERE o.mispar_ishi=  s.mispar_ishi
	     AND  o.taarich  BETWEEN    s.taarich  AND LAST_DAY(s.taarich)
		  AND o.mispar_ishi = po.mispar_ishi
          AND o.taarich = po.taarich
          AND o.mispar_sidur= po.mispar_sidur
          AND o.shat_hatchala = po.shat_hatchala_sidur ;
          --   OR (((trunc(o.taarich) = trunc(p_date_from)) and (o.shayah_leyom_kodem<>1 or o.shayah_leyom_kodem is null)) or ((o.taarich=trunc(p_date_to)+1) and (o.shayah_leyom_kodem=1))));
          
    IF (v_count>0) THEN
    BEGIN
       INSERT INTO TMP_CATALOG_DETAILS@kds_gw_at_tnpr
                              (activity_date,makat8)
       SELECT DISTINCT   po.TAARICH ,po.MAKAT_NESIA
       FROM TB_SIDURIM_OVDIM o,TB_PEILUT_OVDIM po  , TB_TMP_OVDIM_LECHISHUV s
       WHERE o.mispar_ishi=  s.mispar_ishi
	     AND  o.taarich  BETWEEN s.taarich  AND LAST_DAY(s.taarich)
		 AND o.mispar_ishi = po.mispar_ishi
          AND o.taarich = po.taarich
          AND o.mispar_sidur= po.mispar_sidur
          AND o.shat_hatchala = po.shat_hatchala_sidur ;
          
       EXCEPTION
              WHEN DUP_VAL_ON_INDEX THEN
                   NULL;
     END;
    BEGIN 
        --Get makats details
		--  INSERT INTO TMP_CATALOG_DETAILS@kds_gw_at_tnpr(activity_date,makat8) VALUES( TO_DATE('01/08/2010','dd/mm/yyyy'),7811111);
     
     kds.kds_catalog_pack.GetKavimDetails@kds_gw_at_tnpr(rc); 

	 OPEN p_cur FOR
	   SELECT s.mispar_ishi ,r.* 
			FROM TMP_CATALOG_DETAILS@kds_gw_at_tnpr r,TB_PEILUT_OVDIM po, TB_TMP_OVDIM_LECHISHUV s
			WHERE po.mispar_ishi=s.mispar_ishi 
			AND    po.taarich  BETWEEN  s.taarich  AND LAST_DAY(s.taarich)
		    AND po.MAKAT_NESIA = r.makat8
			AND po.taarich = r.activity_date; 
    END;
    END IF;
 --COMMIT;
END  pro_get_kavim_details;

PROCEDURE pro_get_pirtey_ovdim(p_cur OUT CurType, p_num_process IN NUMBER) IS
BEGIN
DBMS_APPLICATION_INFO.SET_MODULE('pkg_calc.pro_get_pirtey_ovdim','get pirtey oved for letkyfa ');
 
	 OPEN p_cur FOR
	 SELECT po.*, i.KOD_SECTOR_ISUK
	    FROM PIVOT_PIRTEY_OVDIM PO,CTB_ISUK i,OVDIM o, TB_MISPAR_ISHI_CHISHUV s
		WHERE 	po.mispar_ishi=s.MISPAR_ISHI
		AND po.mispar_ishi=o.mispar_ishi
        AND S.NUM_PACK = p_num_process
		AND   (  s.taarich  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
					  OR LAST_DAY(s.taarich) BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
					  OR   po.ME_TARICH>= s.taarich    AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<= LAST_DAY(s.taarich) )
		
		  AND i.Kod_Hevra = o.kod_hevra
		 AND  i.Kod_Isuk =po.isuk;

EXCEPTION
       WHEN OTHERS THEN
            RAISE;

END  pro_get_pirtey_ovdim;

PROCEDURE pro_get_meafyeney_ovdim(p_brerat_Mechadal  IN NUMBER, p_num_process IN NUMBER,
									p_tar_me IN DATE,p_tar_ad IN DATE, p_cur OUT CurType) IS
	BEGIN
	IF p_brerat_Mechadal =1 THEN
              OPEN p_cur FOR              
                    WITH tbIshi AS
                     (  select h.MISPAR_ISHI, h.kod_meafyen,h.ME_TAARICH,h.ad_TAARICH,h.value_erech_ishi,h.Erech_ishi, h.Erech_Mechdal_partany
                       , nvl( LEAD (h.ME_TAARICH,1)
                          OVER(partition by h.mispar_ishi,h.kod_meafyen order by h.mispar_ishi,h.kod_meafyen,h.ME_TAARICH asc),  last_day(  h.taarich)+1 ) next_hour_me
                          , nvl( LAG (h.AD_TAARICH,1)
                        OVER(partition by h.mispar_ishi,h.kod_meafyen order by h.mispar_ishi,h.kod_meafyen,h.ME_TAARICH asc) ,h.taarich-1  ) prev_hour_ad
                      from
                     (SELECT S.MISPAR_ISHI, to_char(m.kod_meafyen) kod_meafyen, S.taarich,
                                --  m.ME_TAARICH ,m.ad_TAARICH ,
                                  case when m.ME_TAARICH between  s.taarich and last_day( s.taarich ) then m.ME_TAARICH else   s.taarich  end ME_TAARICH,
                                  case when NVL( m.ad_TAARICH,TO_DATE('31/12/4712' ,'dd/mm/yyyy'))  between  s.taarich and last_day( s.taarich ) then m.ad_TAARICH else  last_day(s.taarich)  end ad_TAARICH,
                                  to_char(to_number(DECODE(m.erech_ishi,NULL,m.ERECH_MECHDAL_PARTANY,to_char(m.erech_ishi)))) value_erech_ishi,
                                  DECODE(m.erech_ishi,NULL ,m.ERECH_MECHDAL_PARTANY || ' (ב.מ.) ',m.erech_ishi || ' (אישי) '  ) Erech_ishi,
                                   DECODE(m.Erech_Mechdal_partany,NULL,'',m.Erech_Mechdal_partany ||   ' (ב.מ.) ') Erech_Mechdal_partany
                       FROM   CTB_MEAFYEN_BITZUA c,   CTB_YECHIDAT_MEAFYEN Y, PIVOT_MEAFYENIM_OVDIM m,
                                   (select distinct MISPAR_ISHI,taarich,num_pack from TB_MISPAR_ISHI_CHISHUV where num_pack=p_num_process) s
                       WHERE   ( s.taarich  BETWEEN  m.ME_TAARICH  AND   NVL(m.ad_TAARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                                      OR  last_day( s.taarich ) BETWEEN  m.ME_TAARICH  AND   NVL(m.ad_TAARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                                      OR   m.ME_TAARICH>= s.taarich  AND   NVL(m.ad_TAARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=  last_day( s.taarich ) )
                                    AND s.MISPAR_ISHI = m.MISPAR_ISHI
                                    AND c.KOD_MEAFYEN_BITZUA= m.kod_meafyen
                                    AND c.YECHIDAT_MEAFYEN = Y.KOD_YECHIDA_MEAFYEN(+)
                              --   and s.mispar_ishi=31029
                              --   and m.kod_meafyen=42
                      order by  s.mispar_ishi,m.kod_meafyen,m.ME_TAARICH ) h
                       order by  h.mispar_ishi,h.kod_meafyen,h.ME_TAARICH      )
        ,mPeriod as
                       (  
                           select * 
                           from( select s.MISPAR_ISHI, s.kod_meafyen, (s.AD_TAARICH+1) ME_TAARICH,
                                             case when last_day(s.AD_TAARICH)<=(next_hour_me-1) then  last_day(s.AD_TAARICH) else next_hour_me-1 end  AD_TAARICH,
                             -- (next_hour_me-1) AD_TAARICH, 
                                            '' Erech_Mechdal_partany,
                                             m.erech ||  ' (ב.מ. מערכת) '  Erech_ishi,
                                             to_char(m.erech) value_erech_ishi,
                                              '1' source_meafyen
                                    from  tbIshi s, brerot_mechdal_meafyenim m
                                    where (s.AD_TAARICH+1)<next_hour_me
                                         and s.kod_meafyen = m.kod_meafyen)
                             where ME_TAARICH<=AD_TAARICH     
                                       ----  and ((trunc(s.AD_TAARICH,'MM') = trunc(next_hour_me,'MM') )or (trunc(add_months(s.AD_TAARICH,1),'MM') = trunc(next_hour_me,'MM')))
                                     --      and (next_hour_me-1)<=  last_day(s.AD_TAARICH)
                                  --      and trunc(s.AD_TAARICH,'MM') =  trunc(next_hour_me,'MM')
                                     --   and s.kod_meafyen =42       
                        union 
                        
                             select *
                             from( select s.MISPAR_ISHI, s.kod_meafyen, --         (s.prev_hour_ad+1) ME_TAARICH,
                                              case when trunc(s.ME_TAARICH,'MM')>=(prev_hour_ad+1) then   trunc(s.ME_TAARICH,'MM')else s.prev_hour_ad+1 end  ME_TAARICH,
                                              (s.ME_TAARICH -1) AD_TAARICH,
                                               '' Erech_Mechdal_partany,
                                               m.erech ||  ' (ב.מ. מערכת) '  Erech_ishi,
                                               to_char(m.erech) value_erech_ishi,
                                               '1' source_meafyen
                                      from  tbIshi s, brerot_mechdal_meafyenim m
                                      where (s.prev_hour_ad+1)<s.ME_TAARICH
                                            and s.kod_meafyen = m.kod_meafyen)
                             where ME_TAARICH<=AD_TAARICH
                           ----      and ((trunc(s.ME_TAARICH,'MM') = trunc(prev_hour_ad,'MM') )or (trunc(add_months(s.ME_TAARICH,-1),'MM') = trunc(prev_hour_ad,'MM')))
                         --   and trunc(s.ME_TAARICH,'MM') =  trunc(prev_hour_ad,'MM')
                     --       and s.mispar_ishi=31029

                        union

                            select s.MISPAR_ISHI, s.kod_meafyen,s.ME_TAARICH,s.AD_TAARICH,
                            s.Erech_Mechdal_partany,
                            s.Erech_ishi,
                            s.value_erech_ishi,
                             '2' source_meafyen
                            from  tbIshi s
                      --     where  s.mispar_ishi=31029
                            ) 
          
           (select   h.MISPAR_ISHI, to_char(h.kod_meafyen) kod_meafyen, 
                        h.ME_TAARICH,h.AD_TAARICH,
                        h.Erech_Mechdal_partany,
                        h.Erech_ishi,
                        h.value_erech_ishi,
                        h.source_meafyen             
         from
         (   select mPeriod.*
              from mPeriod
           union               
            ( SELECT     OV.mispar_ishi, to_char(df.KOD_MEAFYEN) KOD_MEAFYEN,  ov.taarich me_taarich ,last_day(ov.taarich) ad_taarich,
                                            '' Erech_Mechdal_partany,
                                            df.erech ||  ' (ב.מ. מערכת) '  Erech_ishi,
                                            to_char(df.erech) value_erech_ishi,
                                             '1' source_meafyen
                            FROM   TB_MISPAR_ISHI_CHISHUV ov, BREROT_MECHDAL_MEAFYENIM df
                            where num_pack= p_num_process
                             --   and ov.mispar_ishi=31029
                            --     and df.kod_meafyen =42
                                and df.kod_meafyen not in (select   Ph.kod_meafyen 
                                                                        from  mPeriod Ph
                                                                        where Ph.mispar_ishi =  ov.mispar_ishi
                                                                        and to_char(Ph.ME_TAARICH,'mm/yyyy') =to_char( OV.taarich,'mm/yyyy') )
                                                                        )) h )
                 order by h.MISPAR_ISHI,to_char(h.ME_TAARICH,'mm/yyyy'),to_number( h.kod_meafyen) ;
	ELSE
		OPEN p_cur FOR
	   		    SELECT m.kod_meafyen,DECODE(m.Erech_Mechdal_partany,NULL,'',m.Erech_Mechdal_partany ||   ' (ב.מ.) ') Erech_Mechdal_partany,
				 	   		 c.teur_MEAFYEN_BITZUA,( SELECT  b.erech  ||   ' (ב.מ. מערכת) '
																   		FROM BREROT_MECHDAL_MEAFYENIM b
																   		WHERE TRUNC(SYSDATE) BETWEEN b.ME_TAARICH AND NVL(b.AD_TAARICH,TO_DATE('01/01/9999','dd/mm/yyyy'))
																		AND b.kod_meafyen=m.kod_meafyen
																		AND b.erech  IS NOT NULL)  Erech_Brirat_Mechdal,
				 			m.ME_TAARICH,m.ad_TAARICH,y.TEUR_YECHIDA_MEAFYEN YECHIDA,
				           DECODE(m.erech_ishi,NULL,DECODE(m.ERECH_MECHDAL_PARTANY,NULL, NULL,m.Erech_Mechdal_partany || ' (ב.מ.) '),m.erech_ishi || ' (אישי) ' ) Erech_ishi,
						    TO_NUMBER(DECODE(m.erech_ishi,NULL,DECODE(m.ERECH_MECHDAL_PARTANY,NULL, NULL,m.Erech_Mechdal_partany),m.erech_ishi ) ) value_Erech_ishi
				FROM PIVOT_MEAFYENIM_OVDIM m,CTB_MEAFYEN_BITZUA c,CTB_YECHIDAT_MEAFYEN Y ,TB_MISPAR_ISHI_CHISHUV s
				 WHERE   (s.taarich   BETWEEN  m.ME_TAARICH  AND   NVL(m.ad_TAARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
					  OR  LAST_DAY(s.taarich) BETWEEN  m.ME_TAARICH  AND   NVL(m.ad_TAARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
					  OR   m.ME_TAARICH>= s.taarich   AND   NVL(m.ad_TAARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<= LAST_DAY(s.taarich) )
                 AND  S.NUM_PACK = p_num_process
				AND   m.MISPAR_ISHI=s.mispar_ishi
				AND c.KOD_MEAFYEN_BITZUA=m.kod_meafyen
				   AND c.YECHIDAT_MEAFYEN =Y.KOD_YECHIDA_MEAFYEN(+);
	END IF;
  EXCEPTION
       WHEN OTHERS THEN
				RAISE;
END   pro_get_meafyeney_ovdim;


PROCEDURE pro_get_peiluyot_ovdim( p_Cur OUT CurType, p_num_process IN NUMBER,p_tar_me IN DATE,p_tar_ad IN DATE)
IS
BEGIN
 DBMS_APPLICATION_INFO.SET_MODULE('pkg_calc.pro_get_peiluyot_leoved','get peiluyot leoved letkufa ');
    OPEN p_cur FOR
	   SELECT p.MISPAR_ISHI,p.TAARICH,p.MISPAR_SIDUR,p.SHAT_HATCHALA_SIDUR,p.SHAT_YETZIA,
		      LPAD(TO_CHAR(p.MAKAT_NESIA),8,'0') MAKAT_NESIA,p.MISPAR_KNISA,NVL(e.sector_zvira_zman_haelement,0)sector_zvira_zman_haelement,NVL(p.km_visa,0) km_visa,
			   e.nesia,e.kod_lechishuv_premia,e.kupai,NVL(p.Kod_shinuy_premia,0) Kod_shinuy_premia ,p.Oto_no,NVL(p.Dakot_bafoal,0) Dakot_bafoal,
			  TO_NUMBER( SUBSTR(LPAD(TO_CHAR(p.MAKAT_NESIA),8,'0') ,4,3)) zmanElement,p.kisuy_tor,e.nesia_reika
	    FROM 
		 (SELECT p.*
		  FROM   TB_PEILUT_OVDIM p,TB_MISPAR_ISHI_CHISHUV s 
		  WHERE p.MISPAR_ISHI=s.MISPAR_ISHI
             AND P.TAARICH BETWEEN p_tar_me AND p_tar_ad
			  AND  p.TAARICH BETWEEN s.taarich AND LAST_DAY(s.taarich) 
              AND S.NUM_PACK= p_num_process
			  AND NOT NVL(p.Bitul_O_Hosafa,0)  IN (1,3) ) p
		  ,PIVOT_MEAFYENEY_ELEMENTIM e
        WHERE
		  TO_NUMBER(SUBSTR(p.makat_nesia,2,2)) = e.kod_element(+)
              AND p.TAARICH BETWEEN e.me_tarich(+) AND e.ad_tarich(+);

		  EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;
END pro_get_peiluyot_ovdim;

PROCEDURE pro_get_Matzav_Ovdim(p_Cur_Matzav OUT CurType,p_num_process IN NUMBER) IS
BEGIN
     OPEN p_Cur_Matzav FOR
        SELECT  s.MISPAR_ISHI,m.kod_matzav, M.TAARICH_HATCHALA TAARICH_ME, M.TAARICH_SIYUM TAARICH_AD
        FROM MATZAV_OVDIM m ,TB_MISPAR_ISHI_CHISHUV s
        WHERE  m.mispar_ishi=s.mispar_ishi
         AND S.NUM_PACK= p_num_process
        AND ( s.taarich BETWEEN M.TAARICH_HATCHALA  AND NVL(M.TAARICH_SIYUM,TO_DATE(  '01/01/9999'  ,  'dd/mm/yyyy'  ))
                                     OR LAST_DAY(s.taarich) BETWEEN M.TAARICH_HATCHALA AND NVL(M.TAARICH_SIYUM ,TO_DATE(  '01/01/9999'  ,  'dd/mm/yyyy'  )) 
                                     OR M.TAARICH_HATCHALA>= s.taarich AND NVL(M.TAARICH_SIYUM,TO_DATE(  '01/01/9999'  , 'dd/mm/yyyy'  ))<= LAST_DAY(s.taarich) ) ;
        
          EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END pro_get_Matzav_Ovdim;
PROCEDURE pro_get_ovdim_lehishuv_premiot(p_num_process IN NUMBER) IS --,p_taarich_me OUT DATE,p_taarich_ad OUT DATE) IS
p_tar_me DATE;
p_tar_ad DATE;
 BEGIN
        EXECUTE IMMEDIATE 'truncate table TB_MISPAR_ISHI_CHISHUV' ; 
        EXECUTE IMMEDIATE 'truncate table TB_CATALOG_CHISHUV' ; 
        --EXECUTE IMMEDIATE 'truncate table tb_yamim_Lechishuv' ; 
           
	    INSERT INTO TB_MISPAR_ISHI_CHISHUV(ROW_NUM,MISPAR_ISHI,TAARICH, NUM_pack)
            SELECT  ROWNUM,p.*
            FROM ( SELECT distinct  MISPAR_ISHI,  chodesh ,1
                        FROM OVDIM_LECHISHUV_PREMYOT lp  
                        WHERE LP.BAKASHA_ID IS NULL
                        ORDER BY chodesh)  p;

		
        SELECT MIN(l.chodesh) INTO p_tar_me
        FROM OVDIM_LECHISHUV_PREMYOT l
        WHERE  l.BAKASHA_ID IS NULL;
        
        SELECT MAX(LAST_DAY(l.chodesh)) INTO p_tar_ad
        FROM OVDIM_LECHISHUV_PREMYOT l
        WHERE  l.BAKASHA_ID IS NULL;
        
         Pkg_Calculation.pro_divide_packets(p_num_process);
        Pkg_Calculation.pro_set_kavim_details_chishuv(p_tar_me,p_tar_ad);
		--OPEN p_Cur FOR
  			-- SELECT * FROM TB_TMP_OVDIM_LECHISHUV;
	 EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;   
 END pro_get_ovdim_lehishuv_premiot;
 
 PROCEDURE pro_get_netunim_lechishuv( p_Cur_Ovdim OUT CurType ,
  p_Cur_Michsa_Yomit OUT CurType,
 p_Cur_SidurMeyuchadRechiv OUT CurType,
 p_Cur_Sug_Sidur_Rechiv OUT CurType,
p_Cur_Premiot_View OUT CurType,
 p_Cur_Premiot_Yadaniot OUT CurType,
 p_Cur_Sug_Yechida OUT CurType, 
 p_Cur_Yemey_Avoda OUT CurType,
 p_Cur_Pirtey_Ovdim OUT CurType,
 p_Cur_Meafyeney_Ovdim OUT CurType,
 p_Cur_Peiluyot_Ovdim OUT CurType,   
  p_Cur_Mutamut OUT CurType, 
 p_Cur_Matzav  OUT CurType, 
  p_Cur_Buses_Details OUT CurType, 
 --p_Cur_Sugey_Sidur_Tnua OUT CurType, 
 p_Cur_Kavim_Details OUT CurType, 
 p_tar_me IN DATE,p_tar_ad IN DATE, 
 p_maamad IN NUMBER, p_ritza_gorefet IN NUMBER,
 p_status_tipul  IN  TB_YAMEY_AVODA_OVDIM.status_tipul%TYPE,
 p_brerat_Mechadal  IN NUMBER, 
 p_Mis_Ishi IN NUMBER   )IS
 
  p_tarich_me   DATE;
   p_tarich_ad   DATE;
 BEGIN
    EXECUTE IMMEDIATE 'truncate table TB_TMP_OVDIM_LECHISHUV' ; 
	 --  ANALYZE  TB_TMP_OVDIM_LECHISHUV  VALIDATE STRUCTURE CASCADE;
 
 p_tarich_me :=p_tar_me;
 p_tarich_ad :=p_tar_ad;
	 IF (p_Mis_Ishi = 0) THEN 
	 	Pkg_Calculation.pro_get_ovdim_lechishuv(p_tar_me ,p_tar_ad , p_maamad , p_ritza_gorefet ,
                                    p_Cur_Ovdim); 
	 ELSE IF  p_Mis_Ishi = -1 THEN
			 -- Pkg_Calculation.pro_get_ovdim_lehishuv_premiot(p_Cur_Ovdim);
			   SELECT MIN(taarich) INTO p_tarich_me FROM TB_TMP_OVDIM_LECHISHUV;	
		 	   SELECT MAX(taarich) INTO p_tarich_ad FROM TB_TMP_OVDIM_LECHISHUV; 
		 ELSE 
				 Pkg_Calculation.pro_insert_oved_lechishuv(p_Mis_Ishi,p_tar_me);
				 OPEN p_Cur_Ovdim FOR
		   		 	  SELECT * FROM TB_TMP_OVDIM_LECHISHUV; 
		 END IF;	
	 END IF;	 	  		
 
 
	  Pkg_Calculation.pro_get_michsa_yomit(p_tarich_me , p_tarich_ad ,p_Cur_Michsa_Yomit); 
			
	 Pkg_Calculation.pro_get_sidurim_meyuch_rechiv(p_tarich_me , p_tarich_ad , p_Cur_SidurMeyuchadRechiv );					   
	 
	 Pkg_Calculation.pro_get_sug_sidur_rechiv( p_tarich_me , p_tarich_ad,p_Cur_Sug_Sidur_Rechiv);
		  							 
	-- Pkg_Calculation.pro_get_premyot_view(p_Cur_Premiot_View);
	 
	 --Pkg_Calculation.pro_get_premia_yadanit(p_Cur_Premiot_Yadaniot);
	 
	-- Pkg_Calculation.pro_get_sug_yechida( p_Cur_Sug_Yechida);
	 
	--Pkg_Calculation.pro_get_yemey_avoda (p_status_tipul,p_Cur_Yemey_Avoda);								 
	 
	-- Pkg_Calculation.pro_get_pirtey_ovdim(p_Cur_Pirtey_Ovdim);
	 
	 --Pkg_Calculation.pro_get_meafyeney_ovdim(p_brerat_Mechadal,p_Cur_Meafyeney_Ovdim);
	 
	 --Pkg_Calculation.pro_get_peiluyot_ovdim(p_Cur_Peiluyot_Ovdim);
 
      Pkg_Utils.pro_get_ctb_mutamut(p_Cur_Mutamut);  
   
  -- Pkg_Calculation.pro_get_Ovdim_ShePutru(p_Cur_Piturim);
     
   --   Pkg_Calculation.pro_get_buses_details(p_Cur_Buses_Details); 
  
   -- Pkg_Calculation.pro_get_sugey_sidur_tnua(p_Cur_Sugey_Sidur_Tnua); 
	 
	 -- Pkg_Calculation.pro_get_kavim_details(p_Cur_Kavim_Details); 
	 
 
	   EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;   
 END pro_get_netunim_lechishuv;
 
 
 PROCEDURE pro_get_netunim_leprocess( p_Cur_Ovdim OUT CurType ,
  p_Cur_Michsa_Yomit OUT CurType,
 p_Cur_SidurMeyuchadRechiv OUT CurType,
 p_Cur_Sug_Sidur_Rechiv OUT CurType,
p_Cur_Premiot_View OUT CurType,
 p_Cur_Premiot_Yadaniot OUT CurType,
 p_Cur_Premiot_NihulTnua OUT CurType,
 p_Cur_Sug_Yechida OUT CurType, 
 p_Cur_Yemey_Avoda OUT CurType,
 p_Cur_Pirtey_Ovdim OUT CurType,
 p_Cur_Meafyeney_Ovdim OUT CurType,
 p_Cur_Peiluyot_Ovdim OUT CurType,   
  p_Cur_Mutamut OUT CurType, 
p_Cur_Matzav  OUT CurType, 
  p_Cur_Buses_Details OUT CurType, 
 p_Cur_Kavim_Details OUT CurType, 
 p_tar_me IN DATE,p_tar_ad IN DATE, 
 p_status_tipul  IN  TB_YAMEY_AVODA_OVDIM.status_tipul%TYPE,
 p_brerat_Mechadal  IN NUMBER,
  p_Mis_Ishi IN NUMBER,
 p_num_process IN NUMBER   ) IS
 
  p_tarich_me   DATE;
  p_tarich_ad   DATE;
 BEGIN

  IF (p_Mis_Ishi = -1) THEN
       SELECT MIN(s.taarich)  INTO p_tarich_me  FROM TB_MISPAR_ISHI_CHISHUV s;
       SELECT MAX(LAST_DAY(s.taarich))  INTO p_tarich_ad  FROM TB_MISPAR_ISHI_CHISHUV s;
  ELSE
          p_tarich_me :=p_tar_me;
          p_tarich_ad :=p_tar_ad;
  END IF;

      --  p_tarich_me:=to_date('01/04/2012','dd/mm/yyyy');
      --  p_tarich_ad:=to_date('30/04/2012','dd/mm/yyyy');
     
      Pkg_Calculation.pro_get_ovdim(p_Cur_Ovdim,p_num_process); 
      
      Pkg_Calculation.pro_get_michsa_yomit(p_tarich_me , p_tarich_ad ,p_Cur_Michsa_Yomit);  
            
     Pkg_Calculation.pro_get_sidurim_meyuch_rechiv(p_tarich_me , p_tarich_ad , p_Cur_SidurMeyuchadRechiv );                      
     
     Pkg_Calculation.pro_get_sug_sidur_rechiv( p_tarich_me , p_tarich_ad,p_Cur_Sug_Sidur_Rechiv);
                                       
     Pkg_Calculation.pro_get_premyot_view(p_Cur_Premiot_View,p_num_process);
     
     Pkg_Calculation.pro_get_premia_yadanit(p_Cur_Premiot_Yadaniot,p_num_process); 
     
     Pkg_Calculation.pro_get_premyot_nihul_tnua(p_Cur_Premiot_NihulTnua,p_num_process); 
     
     Pkg_Calculation.pro_get_sug_yechida( p_Cur_Sug_Yechida,p_num_process); 
     
    Pkg_Calculation.pro_get_yemey_avoda (p_status_tipul,p_num_process,p_Cur_Yemey_Avoda,p_tarich_me,p_tarich_ad);                            
     
     Pkg_Calculation.pro_get_pirtey_ovdim(p_Cur_Pirtey_Ovdim,p_num_process); 
     
     Pkg_Calculation.pro_get_meafyeney_ovdim(p_brerat_Mechadal,p_num_process,p_tarich_me,p_tarich_ad,p_Cur_Meafyeney_Ovdim);
     
     Pkg_Calculation.pro_get_peiluyot_ovdim(p_Cur_Peiluyot_Ovdim,p_num_process,p_tarich_me,p_tarich_ad);
 
      Pkg_Utils.pro_get_ctb_mutamut(p_Cur_Mutamut);  
   
    Pkg_Calculation.pro_get_Matzav_Ovdim(p_Cur_Matzav,p_num_process);
     
      Pkg_Calculation.pro_get_buses_details(p_Cur_Buses_Details,p_num_process,p_tarich_me,p_tarich_ad); 

     
      Pkg_Calculation.pro_get_kavim_process(p_Cur_Kavim_Details,p_num_process,p_tarich_me,p_tarich_ad); 
     
 
       EXCEPTION
         WHEN OTHERS THEN
              RAISE;   
 END pro_get_netunim_leprocess;
 PROCEDURE pro_ovdim_kelet_lechishuv(p_Cur_Ovdim OUT CurType) IS
 BEGIN
   
   OPEN p_Cur_Ovdim FOR
         SELECT Y.MISPAR_ISHI,Y.TAARICH
         from tb_yamey_avoda_ovdim y
         where Y.TAARICH between to_date('21/06/2012','dd/mm/yyyy') and to_date('30/06/2012','dd/mm/yyyy');
         
         
         --Y.MISPAR_ISHI = p.MISPAR_ISHI and
            /*       to_char(Y.TAARICH,'mm/yyyy') = '09/2010' and 
                 --  substr(P.MAAMAD,0,1) ='2' and
                  -- Y.TAARICH between p.ME_TARICH and nvl(P.AD_TARICH,to_date('01/01/9999','dd/mm/yyyy') )
                      Y.MISPAR_ISHI  in(
                    select  mispar_ishi
                           from tb_log_bakashot 
                            where  BAKASHA_ID = 6532
                          --  AND TEUR_HODAA like 'InitMeafyenyOved: PrepareMeafyenim :An item with the same key has already been added.%'
                          AND TEUR_HODAA like 'AddRowZmanLeloHafsaka:    at System.DateTimeParse.Parse(String s, DateTimeFormatInfo dtfi, DateTimeStyles styles)%'
                      );*/
                                 
 END  pro_ovdim_kelet_lechishuv;        
 
 
 PROCEDURE pro_idkun_sug_sidur IS --(  p_Cur OUT CurType) AS

   
   GeneralQry VARCHAR2(3000);
  CountQry VARCHAR2(3000);
  CountRows NUMBER;
  InsertQry  VARCHAR2(3000); 
  
     rc NUMBER ;   
BEGIN
     DBMS_APPLICATION_INFO.SET_MODULE('PKG_SUG_SIDUR.pro_get_sugey_sidur_tnua','get sugey sidur from tnua loved letkyfa ');
 
     INSERT INTO  kds.TMP_SUG_SIDUR@KDS_GW_AT_TNPR(ACTIVE_DATE,SIDUR_AVODA)
                  SELECT DISTINCT  taarich, mispar_sidur
                    FROM TB_SIDURIM_OVDIM s
                    WHERE mispar_sidur NOT LIKE '99%'
                   AND  s.taarich BETWEEN TO_DATE('01/08/2010','dd/mm/yyyy')     AND TO_DATE('31/10/2010','dd/mm/yyyy')
                 --    and to_char(taarich,'mm/yyyy') ='08/2010'
                     AND LENGTH(MISPAR_SIDUR) BETWEEN 4 AND 5
                      AND sug_sidur  IS NULL;
              --        and s.mispar_ishi in(select m.mispar_ishi
               --                                     from tb_mispar_ishi_chishuv m); 

  
      Kds_Sidur_Avoda_Pack.Get_Sug_Sidur@KDS_GW_AT_TNPR(rc);
 
          INSERT INTO TMP_SIDURIM ( TAARICH,SIDUR_AVODA,SUG_SIDUR,Sug_Sidur_Name,Status) 
                                SELECT   Active_Date,Sidur_Avoda,SUG_SIDUR,Sug_Sidur_Name,Status
                            FROM   kds.TMP_SUG_SIDUR@KDS_GW_AT_TNPR ;
                            
 /*  update tb_sidurim_ovdim s
    set S.SUG_SIDUR = T.SUG_SIDUR
    from  tb_sidurim_ovdim s
    inner join TMP_SIDURIM t
    on S.MISPAR_SIDUR= T.SIDUR_AVODA and
         S.TAARICH = T.TAARICH; */
     
  UPDATE  TB_SIDURIM_OVDIM s
    SET   S.SUG_SIDUR = (SELECT DISTINCT  T.SUG_SIDUR 
                                    FROM TMP_SIDURIM t 
                                    WHERE S.MISPAR_SIDUR= T.SIDUR_AVODA AND
                                    S.TAARICH = T.TAARICH  )
   WHERE  s.taarich BETWEEN TO_DATE('01/08/2010','dd/mm/yyyy')     AND TO_DATE('31/10/2010','dd/mm/yyyy')
 AND    s.mispar_sidur NOT LIKE '99%'   
   AND  s.SUG_SIDUR IS  NULL 
    AND LENGTH(s.MISPAR_SIDUR) BETWEEN 4 AND 5;
 --     and s.mispar_ishi in(select m.mispar_ishi
      --                                              from tb_mispar_ishi_chishuv m);   
    
    COMMIT;      
/*
 OPEN p_Cur FOR    
             SELECT TAARICH,SIDUR_AVODA mispar_sidur,SUG_SIDUR
           FROM TMP_SIDURIM; */
/*
 update (select bonus 
           from employee_bonus b 
               inner join employees e on b.employee_id = e.employee_id 
                        where e.bonus_eligible = 'N') t 
 set t.bonus = 0*/
 END  pro_idkun_sug_sidur; 
      
 
 PROCEDURE  pro_Ovdim_Errors(p_Cur OUT CurType) IS
 BEGIN
   
  DELETE FROM TB_SHGIOT WHERE TAARICH BETWEEN TO_DATE('02/01/2011','dd/mm/yyyy') AND  TO_DATE('02/01/2011','dd/mm/yyyy');
  --delete from tb_shgiot_2 where TAARICH  between  to_date('02/01/2011','dd/mm/yyyy') and  to_date('02/01/2011','dd/mm/yyyy');
  COMMIT;
  
   OPEN p_Cur  FOR
         SELECT Y.MISPAR_ISHI,Y.TAARICH
         FROM TB_YAMEY_AVODA_OVDIM y--,pivot_pirtey_ovdim p
         WHERE Y.TAARICH  BETWEEN  TO_DATE('02/01/2011','dd/mm/yyyy') AND  TO_DATE('02/01/2011','dd/mm/yyyy')
                AND Y.STATUS=0;
 END  pro_Ovdim_Errors;    
 
 PROCEDURE pro_upd_yemey_avoda_bechishuv(p_tar_me IN DATE,p_tar_ad IN DATE) IS
 BEGIN
 
         UPDATE    TB_YAMEY_AVODA_OVDIM t
         SET t.BECHISHUV_SACHAR = 1--,
        --   t.TAARICH_IDKUN_ACHARON = sysdate
         WHERE ( t.MISPAR_ISHI   ,T.TAARICH)  IN(
                SELECT       Y.MISPAR_ISHI, Y.TAARICH
                FROM   TB_MISPAR_ISHI_CHISHUV OS,       
                            OVDIM O,
                            TB_YAMEY_AVODA_OVDIM Y,      
                           ( SELECT * FROM TB_SIDURIM_OVDIM s
                              WHERE   ( (s.lo_letashlum=0 OR s.lo_letashlum IS NULL)
                                   OR  (s.lo_letashlum=1 AND s.sug_sidur=69  ))
                                 AND s.mispar_sidur<>99200
                                AND  s.taarich BETWEEN p_tar_me AND p_tar_ad) s,
                       PIVOT_SIDURIM_MEYUCHADIM V_SIDURIM
                WHERE  os.MISPAR_ISHI = o.MISPAR_ISHI 
                        AND    os.MISPAR_ISHI = Y.MISPAR_ISHI  
                        AND    Y.TAARICH BETWEEN os.TAARICH AND LAST_DAY ( os.TAARICH )  
                        AND    Y.TAARICH = s.TAARICH (+)
                        AND    y.MISPAR_ISHI = s.MISPAR_ISHI (+)
                        AND    V_SIDURIM.MISPAR_SIDUR(+) = S.MISPAR_SIDUR
                                   AND S.TAARICH BETWEEN V_SIDURIM.ME_TARICH(+)
                                                     AND  V_SIDURIM.AD_TARICH(+)
                        AND Y.TAARICH BETWEEN p_tar_me AND p_tar_ad
                  --      AND OS.NUM_PACK =p_num_process 
                        AND ( Y.STATUS = 1 OR Y.STATUS = 2 )
                        AND NOT NVL ( S.BITUL_O_HOSAFA, 0 ) IN (1, 3));
 END  pro_upd_yemey_avoda_bechishuv;
 
  PROCEDURE pro_upd_ymy_avoda_lo_bechishuv IS
 BEGIN
 
         UPDATE    TB_YAMEY_AVODA_OVDIM t
         SET t.BECHISHUV_SACHAR = 0 --,
          --    t.TAARICH_IDKUN_ACHARON = sysdate
         WHERE t.BECHISHUV_SACHAR = 1;
         
 END  pro_upd_ymy_avoda_lo_bechishuv;
END Pkg_Calculation;
/


CREATE OR REPLACE PACKAGE BODY          Pkg_Calc_worker AS
/******************************************************************************
   NAME:       PKG_CALC
   PURPOSE:

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        05/07/2009             1. Created this package body.
******************************************************************************/

  PROCEDURE pro_insert_oved_lechishuv(p_mis_ishi IN NUMBER,p_chodesh IN DATE) IS
  
  BEGIN
          INSERT INTO TB_TMP_OVDIM_LECHISHUV (MISPAR_ISHI,TAARICH)
                     VALUES(p_mis_ishi,p_chodesh);
             
        EXCEPTION
   WHEN OTHERS THEN
            RAISE;             
  END pro_insert_oved_lechishuv; 

PROCEDURE pro_get_michsa_yomit(p_tar_me IN  TB_MICHSA_YOMIT.me_taarich%TYPE,
                               p_tar_ad IN  TB_MICHSA_YOMIT.ad_taarich%TYPE,
                               p_cur OUT CurType)
IS
BEGIN
   DBMS_APPLICATION_INFO.SET_MODULE('Pkg_Calculation.pro_get_michsa_yomit','get michsot yomiyot');
  
    OPEN p_cur FOR
        SELECT  kod_michsa,sug_yom ,me_taarich, NVL(AD_TAARICH,TO_DATE('01/01/9999','dd/mm/yyyy')) ad_taarich,michsa,Shavoa_Avoda
        FROM TB_MICHSA_YOMIT
        WHERE me_taarich<=p_tar_ad 
        AND NVL(AD_TAARICH,TO_DATE('01/01/9999','dd/mm/yyyy'))>=p_tar_me;

          EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END pro_get_michsa_yomit;

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
        
END pro_get_sidurim_meyuch_rechiv;

 
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
        
END pro_get_sug_sidur_rechiv;

PROCEDURE pro_get_premyot_view( p_cur OUT CurType)
IS
BEGIN
 DBMS_APPLICATION_INFO.SET_MODULE('PKG_UTILS.pro_get_premyot_view',' get premyot view ');
  
   OPEN p_cur FOR
--SELECT 0 mispar_ishi,sysdate taarich,  0 Sug_premia,0  Sum_dakot FROM dual;
     SELECT v.Mispar_ishi,s.taarich,Sug_premia,SUM(Dakot_premia) Sum_dakot
        FROM TB_PREM v, TB_TMP_OVDIM_LECHISHUV s
        WHERE v.Mispar_ishi =s.Mispar_ishi
         AND v.Tkufa =  TO_CHAR(s.taarich,'yyyyMM')
        GROUP BY (v.Mispar_ishi,s.taarich, v.Sug_premia);
  
    EXCEPTION
      WHEN OTHERS THEN
             OPEN p_cur FOR
                SELECT 0 mispar_ishi,sysdate taarich,  0 Sug_premia,0  Sum_dakot FROM dual;
          --  WHEN OTHERS THEN
            --     RAISE;
END pro_get_premyot_view;

PROCEDURE pro_get_premia_yadanit(  p_Cur OUT CurType)
IS
BEGIN
   DBMS_APPLICATION_INFO.SET_MODULE('PKG_SUG_UTILS.pro_get_premia_yadanit',' get premia yadanit');
  
     OPEN p_cur FOR
           SELECT  s.MISPAR_ISHI,s.taarich ,DAKOT_PREMYA, Taarich_idkun_acharon,sug_premya
          FROM TB_PREMYOT_YADANIYOT p,  TB_TMP_OVDIM_LECHISHUV s
          WHERE p.MISPAR_ISHI = s.MISPAR_ISHI
               and p.TAARICH between s.taarich and last_day(s.taarich);
           -- AND TO_CHAR(p.TAARICH,'mm/yyyy') = TO_CHAR(s.taarich,'mm/yyyy');

    EXCEPTION
            WHEN OTHERS THEN
                 RAISE;
END pro_get_premia_yadanit;


PROCEDURE pro_get_premyot_nihul_tnua( p_cur OUT CurType)
IS
BEGIN
 DBMS_APPLICATION_INFO.SET_MODULE('PKG_UTILS.pro_get_premyot_view',' get premyot view ');
  
   OPEN p_cur FOR
        SELECT v.Mispar_ishi,s.taarich,Sug_premia,SUM(Dakot_premia) Sum_dakot
        FROM TB_PREMYOT_NIHUL_TNUA v, TB_TMP_OVDIM_LECHISHUV s
        WHERE v.Mispar_ishi =s.Mispar_ishi
         AND v.Tkufa =  s.taarich
        GROUP BY (v.Mispar_ishi,s.taarich, v.Sug_premia);
    EXCEPTION
             WHEN OTHERS THEN
             OPEN p_cur FOR
               SELECT 0 mispar_ishi,SYSDATE taarich,  0 Sug_premia,0  Sum_dakot FROM dual;
              --  SELECT 0 Sug_premia,0  Sum_dakot FROM dual;
        --         RAISE;
END pro_get_premyot_nihul_tnua;
PROCEDURE pro_get_sug_yechida( p_cur OUT CurType)
IS
BEGIN 
     -- DBMS_APPLICATION_INFO.SET_MODULE('PKG_OVDIM.fun_get_sug_yechida_oved','get sug yechida leoved  ');
      OPEN p_Cur FOR 
             SELECT o.mispar_ishi,y.SUG_YECHIDA,  p.me_tarich,p.ad_tarich 
           FROM      OVDIM o,CTB_YECHIDA y, 
                    (  SELECT   t.mispar_ishi , t.YECHIDA_IRGUNIT ,t.ME_TARICH,
                                     t.ad_tarich 
                            FROM  PIVOT_PIRTEY_OVDIM t,TB_TMP_OVDIM_LECHISHUV s
                            WHERE t.mispar_ishi= s.mispar_ishi
                                AND t.isuk IS NOT NULL 
                                AND ( s.taarich BETWEEN ME_TARICH  AND NVL(t.ad_TARICH,TO_DATE(  '01/01/9999'  ,  'dd/mm/yyyy'  ))
                                     OR LAST_DAY(s.taarich) BETWEEN ME_TARICH AND NVL(t.ad_TARICH,TO_DATE(  '01/01/9999'  ,  'dd/mm/yyyy'  )) 
                                     OR t.ME_TARICH>= s.taarich AND NVL(t.ad_TARICH,TO_DATE(  '01/01/9999'  , 'dd/mm/yyyy'  ))<= LAST_DAY(s.taarich) )
                             ORDER BY mispar_ishi ) p
                      
             WHERE o.mispar_ishi= p.mispar_ishi 
             AND y.KOD_HEVRA=o.KOD_HEVRA
             AND y.KOD_YECHIDA=p.YECHIDA_IRGUNIT;
   
    EXCEPTION
            WHEN OTHERS THEN
                 RAISE;

END  pro_get_sug_yechida;

PROCEDURE pro_get_yemey_avoda ( p_status_tipul  IN  TB_YAMEY_AVODA_OVDIM.status_tipul%TYPE,  
                                                      p_cur OUT CurType,p_tar_me IN DATE,p_tar_ad IN DATE) IS
    BEGIN
     DBMS_APPLICATION_INFO.SET_MODULE('PKG_CAOLC.pro_get_yemey_avoda_to_oved','get yemey avoda to oved');
  
         OPEN p_cur FOR
                      SELECT       Y.MISPAR_ISHI, Y.TAARICH, Y.SHAT_HATCHALA, Y.SHAT_SIYUM,
                       S.MISPAR_SIDUR, NVL ( S.LO_LETASHLUM, 0 ) LO_LETASHLUM,
                       S.SHAT_HATCHALA SHAT_HATCHALA_SIDUR, S.SHAT_HATCHALA_LETASHLUM,
                       S.SHAT_GMAR_LETASHLUM, S.SHAT_GMAR SHAT_GMAR_SIDUR,
                       TO_CHAR ( S.TAARICH, 'd' ) DAY_TAARICH, Y.HAMARAT_SHABAT,
                       Y.TACHOGRAF, V_SIDURIM.SIDUR_MISUG_HEADRUT, V_SIDURIM.SECTOR_AVODA,
                      V_SIDURIM.SUG_AVODA, V_SIDURIM.SUG_SIDUR SUG_SIDUR_MEYUCHAD,
                       V_SIDURIM.SIDUR_NAMLAK_VISA,V_SIDURIM.tashlum_kavua_beshishi,
                       DECODE ( S.OUT_MICHSA, NULL, 0, S.OUT_MICHSA ) OUT_MICHSA, Y.LINA,
                       Y.HALBASHA, S.PITZUL_HAFSAKA,
                       NVL ( S.MEZAKE_HALBASHA, 0 ) MEZAKE_HALBASHA,
                       V_SIDURIM.MICHSAT_SHAOT_CHODSHIT, V_SIDURIM.MAX_DAKOT_BODED,
                       V_SIDURIM.MAX_SHAOT_BYOM_SHISHI, V_SIDURIM.MAX_SHAOT_BESHABATON,
                       NVL ( V_SIDURIM.ZAKAY_LEPIZUL, 0 ) ZAKAY_LEPIZUL,
                       V_SIDURIM.DAKOT_N_LETASHLUM_HOL,V_SIDURIM.michsat_shishi_lebaley_x,
                       NVL ( S.SUG_HASHLAMA, 0 ) SUG_HASHLAMA,v_sidurim.realy_veod_yom,
                       V_SIDURIM.SUG_SHAOT_BYOM_HOL_IF_MIGBALA,v_sidurim.tashlum_kavua_bchol,
                       V_SIDURIM.sug_hashaot_beyom_shishi,V_SIDURIM.max_shaot_byom_hol,
                       V_SIDURIM.sug_hashaot_beyom_shabaton,v_sidurim.shat_gmar_auto,
                       NVL ( S.HASHLAMA, 0 ) HASHLAMA, S.BITUL_O_HOSAFA, Y.HASHLAMA_LEYOM,
                       NVL ( S.YOM_VISA, 0 ) YOM_VISA, V_SIDURIM.EIN_LESHALEM_TOS_LILA,
                       NVL ( Y.ZMAN_NESIA_HALOCH, 0 ) ZMAN_NESIA_HALOCH,
                       NVL ( Y.ZMAN_NESIA_HAZOR, 0 ) ZMAN_NESIA_HAZOR,
                       NVL(v_sidurim.zakay_lehamara,0) zakay_lehamara  ,V_SIDURIM.shabaton_tashlum_kavua,
                       NVL ( S.HAFHATAT_NOCHECHUT_VISA, 0 ) HAFHATAT_NOCHECHUT_VISA,
                       NVL ( S.MEZAKE_NESIOT, 0 ) MEZAKE_NESIOT, Y.BITUL_ZMAN_NESIOT,
                       NVL ( S.KOD_SIBA_LEDIVUCH_YADANI_IN, 0 ) KOD_SIBA_LEDIVUCH_YADANI_IN,
                       NVL ( S.KOD_SIBA_LEDIVUCH_YADANI_OUT, 0 )
                           KOD_SIBA_LEDIVUCH_YADANI_OUT,V_SIDURIM.sidur_rak_lechevrot_banot,
                       NVL ( S.ACHUZ_KNAS_LEPREMYAT_VISA, 0 ) ACHUZ_KNAS_LEPREMYAT_VISA,
                       NVL ( S.ACHUZ_VIZA_BESIKUN, 0 ) ACHUZ_VIZA_BESIKUN,
                       S.MIKUM_SHAON_KNISA, S.MIKUM_SHAON_YETZIA,
                       V_SIDURIM.ZAKAY_LECHISHUV_RETZIFUT, NVL ( S.SUG_SIDUR, 0 ) SUG_SIDUR,
                       V_SIDURIM.matala_klalit_lelo_rechev,nvl(V_SIDURIM.zakaut_legmul_chisachon,0) zakaut_legmul_chisachon,
                        V_SIDURIM.shaon_nochachut
            FROM   TB_TMP_OVDIM_LECHISHUV OS,       
                   OVDIM O,
                   TB_YAMEY_AVODA_OVDIM Y,      
                       ( SELECT * FROM TB_SIDURIM_OVDIM s
                       WHERE   ( (s.lo_letashlum=0 OR s.lo_letashlum IS NULL)
                                   OR  (s.lo_letashlum=1 AND s.sug_sidur=69  ))
                             AND s.mispar_sidur<>99200 
                            AND  s.taarich BETWEEN   p_tar_me AND p_tar_ad) s,
                   PIVOT_SIDURIM_MEYUCHADIM V_SIDURIM
            WHERE  os.MISPAR_ISHI = o.MISPAR_ISHI 
                    AND    os.MISPAR_ISHI = Y.MISPAR_ISHI  
                   AND    Y.TAARICH BETWEEN os.TAARICH AND LAST_DAY ( os.TAARICH )  
                    AND    Y.TAARICH = s.TAARICH (+)
                    AND    y.MISPAR_ISHI = s.MISPAR_ISHI (+)
                    AND    V_SIDURIM.MISPAR_SIDUR(+) = S.MISPAR_SIDUR
                               AND S.TAARICH BETWEEN V_SIDURIM.ME_TARICH(+)
                                                 AND  V_SIDURIM.AD_TARICH(+)
                    AND Y.TAARICH BETWEEN p_tar_me AND p_tar_ad
                    AND ( Y.STATUS = 1 OR Y.STATUS = 2 )
                    AND NOT NVL ( S.BITUL_O_HOSAFA, 0 ) IN (1, 3)
            --        AND (( S.LO_LETASHLUM = 0 OR S.LO_LETASHLUM IS NULL)   OR ( S.LO_LETASHLUM = 1  AND S.SUG_SIDUR = 69 ))
              --     AND nvl(S.MISPAR_SIDUR,0) <> 99200    
               ORDER BY y.taarich ASC;
  EXCEPTION
       WHEN OTHERS THEN
                RAISE;
END  pro_get_yemey_avoda;

PROCEDURE pro_get_pirtey_ovdim(p_cur OUT CurType) IS
BEGIN
DBMS_APPLICATION_INFO.SET_MODULE('pkg_calc.pro_get_pirtey_ovdim','get pirtey oved for letkyfa ');
 
     OPEN p_cur FOR
     SELECT po.*, i.KOD_SECTOR_ISUK
        FROM PIVOT_PIRTEY_OVDIM PO,CTB_ISUK i,OVDIM o, TB_TMP_OVDIM_LECHISHUV s
        WHERE     po.mispar_ishi=s.MISPAR_ISHI
        AND po.mispar_ishi=o.mispar_ishi
        AND   (  s.taarich  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                      OR LAST_DAY(s.taarich) BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                      OR   po.ME_TARICH>= s.taarich    AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<= LAST_DAY(s.taarich) )
        
          AND i.Kod_Hevra = o.kod_hevra
         AND  i.Kod_Isuk =po.isuk;

EXCEPTION
       WHEN OTHERS THEN
            RAISE;
END  pro_get_pirtey_ovdim;


 PROCEDURE pro_get_peiluyot_ovdim( p_Cur OUT CurType,p_tar_me IN DATE,p_tar_ad IN DATE)
IS
BEGIN
 DBMS_APPLICATION_INFO.SET_MODULE('pkg_calc.pro_get_peiluyot_leoved','get peiluyot leoved letkufa ');
    OPEN p_cur FOR
       SELECT p.MISPAR_ISHI,p.TAARICH,p.MISPAR_SIDUR,p.SHAT_HATCHALA_SIDUR,p.SHAT_YETZIA,
              LPAD(TO_CHAR(p.MAKAT_NESIA),8,'0') MAKAT_NESIA,p.MISPAR_KNISA,NVL(e.sector_zvira_zman_haelement,0)sector_zvira_zman_haelement,NVL(p.km_visa,0) km_visa,
               e.nesia,e.kod_lechishuv_premia,e.kupai,NVL(p.Kod_shinuy_premia,0) Kod_shinuy_premia ,p.Oto_no,NVL(p.Dakot_bafoal,0) Dakot_bafoal,
              TO_NUMBER( SUBSTR(LPAD(TO_CHAR(p.MAKAT_NESIA),8,'0') ,4,3)) zmanElement,p.kisuy_tor,e.nesia_reika
        FROM 
         (SELECT p.*
          FROM   TB_PEILUT_OVDIM p,TB_TMP_OVDIM_LECHISHUV s 
          WHERE p.MISPAR_ISHI=s.MISPAR_ISHI
             AND P.TAARICH BETWEEN p_tar_me AND p_tar_ad
           --   AND  p.TAARICH BETWEEN s.taarich AND LAST_DAY(s.taarich) 
              AND NOT NVL(p.Bitul_O_Hosafa,0)  IN (1,3) ) p
          ,PIVOT_MEAFYENEY_ELEMENTIM e
        WHERE
          TO_NUMBER(SUBSTR(p.makat_nesia,2,2)) = e.kod_element(+)
              AND p.TAARICH BETWEEN e.me_tarich(+) AND e.ad_tarich(+);

          EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END pro_get_peiluyot_ovdim;

PROCEDURE pro_get_Matzav_Ovdim(p_Cur_Matzav OUT CurType) IS
BEGIN
     OPEN p_Cur_Matzav FOR
        SELECT  s.MISPAR_ISHI,m.Kod_Matzav,M.TAARICH_HATCHALA TAARICH_ME, M.TAARICH_SIYUM TAARICH_AD
        FROM MATZAV_OVDIM m ,TB_TMP_OVDIM_LECHISHUV s
        WHERE  m.mispar_ishi=s.mispar_ishi
          AND ( s.taarich BETWEEN M.TAARICH_HATCHALA  AND NVL(M.TAARICH_SIYUM,TO_DATE(  '01/01/9999'  ,  'dd/mm/yyyy'  ))
                                     OR LAST_DAY(s.taarich) BETWEEN M.TAARICH_HATCHALA AND NVL(M.TAARICH_SIYUM ,TO_DATE(  '01/01/9999'  ,  'dd/mm/yyyy'  )) 
                                     OR M.TAARICH_HATCHALA>= s.taarich AND NVL(M.TAARICH_SIYUM,TO_DATE(  '01/01/9999'  , 'dd/mm/yyyy'  ))<= LAST_DAY(s.taarich) ) ;
        
          EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END pro_get_Matzav_Ovdim;

PROCEDURE pro_get_buses_details( p_Cur OUT CurType,p_tar_me IN DATE,p_tar_ad IN DATE)
IS
 
BEGIN
 
      DBMS_APPLICATION_INFO.SET_MODULE('PKG_TNUA.pro_get_buses_details','get buses details from mashar ');
  
     OPEN p_Cur FOR
         SELECT license_number, bus_number,Vehicle_Type
         FROM VEHICLE_SPECIFICATIONS
 --VCL_GENERAL_VEHICLE_VIEW@kds2maale
         WHERE bus_number IN (SELECT DISTINCT p.oto_no
         FROM TB_PEILUT_OVDIM p,TB_TMP_OVDIM_LECHISHUV o
         WHERE p.MISPAR_ISHI = o.MISPAR_ISHI  
          AND    p.taarich between p_tar_me and p_tar_ad
            AND  p.taarich BETWEEN o.taarich  AND LAST_DAY(o.taarich) );
      
EXCEPTION
        WHEN OTHERS THEN
        RAISE;
END pro_get_buses_details;

PROCEDURE pro_get_kavim_details(p_cur OUT CurType,p_tar_me IN DATE,p_tar_ad IN DATE) IS
    v_count  NUMBER;
    rc NUMBER;
BEGIN
   DBMS_APPLICATION_INFO.SET_MODULE('pkg_tnua.pro_get_kavim_details','get kavim details from tnua');
    SELECT COUNT(po.mispar_ishi) INTO v_count
    FROM TB_SIDURIM_OVDIM o,TB_PEILUT_OVDIM po , TB_TMP_OVDIM_LECHISHUV s
    WHERE o.mispar_ishi=  s.mispar_ishi
     --    AND  o.taarich  BETWEEN    s.taarich  AND LAST_DAY(s.taarich)
          and   o.taarich between p_tar_me and p_tar_ad
          AND o.mispar_ishi = po.mispar_ishi
          AND o.taarich = po.taarich
          AND o.mispar_sidur= po.mispar_sidur
          AND o.shat_hatchala = po.shat_hatchala_sidur ;
          --   OR (((trunc(o.taarich) = trunc(p_date_from)) and (o.shayah_leyom_kodem<>1 or o.shayah_leyom_kodem is null)) or ((o.taarich=trunc(p_date_to)+1) and (o.shayah_leyom_kodem=1))));
          
    IF (v_count>0) THEN
    BEGIN
       INSERT INTO TMP_CATALOG_DETAILS@kds_gw_at_tnpr
                              (activity_date,makat8)
       SELECT DISTINCT   po.TAARICH ,po.MAKAT_NESIA
       FROM TB_SIDURIM_OVDIM o,TB_PEILUT_OVDIM po  , TB_TMP_OVDIM_LECHISHUV s
       WHERE o.mispar_ishi=  s.mispar_ishi
      --   AND  o.taarich  BETWEEN s.taarich  AND LAST_DAY(s.taarich)
         AND  o.taarich between p_tar_me and p_tar_ad
         AND o.mispar_ishi = po.mispar_ishi
          AND o.taarich = po.taarich
          AND o.mispar_sidur= po.mispar_sidur
          AND o.shat_hatchala = po.shat_hatchala_sidur ;
          
       EXCEPTION
              WHEN DUP_VAL_ON_INDEX THEN
                   NULL;
     END;
    BEGIN 
        --Get makats details
        --  INSERT INTO TMP_CATALOG_DETAILS@kds_gw_at_tnpr(activity_date,makat8) VALUES( TO_DATE('01/08/2010','dd/mm/yyyy'),7811111);
     
     kds.kds_catalog_pack.GetKavimDetails@kds_gw_at_tnpr(rc); 

     OPEN p_cur FOR
       SELECT s.mispar_ishi ,r.* 
            FROM TMP_CATALOG_DETAILS@kds_gw_at_tnpr r,TB_PEILUT_OVDIM po, TB_TMP_OVDIM_LECHISHUV s
            WHERE po.mispar_ishi=s.mispar_ishi 
            AND    po.taarich between  p_tar_me and p_tar_ad
            AND po.MAKAT_NESIA = r.makat8
            AND po.taarich = r.activity_date; 
    END;
    END IF;
 --COMMIT;
END  pro_get_kavim_details;
 PROCEDURE pro_get_netunim_lechishuv( p_Cur_Ovdim OUT CurType ,
  p_Cur_Michsa_Yomit OUT CurType,
 p_Cur_SidurMeyuchadRechiv OUT CurType,
 p_Cur_Sug_Sidur_Rechiv OUT CurType,
p_Cur_Premiot_View OUT CurType,
 p_Cur_Premiot_Yadaniot OUT CurType,
 p_Cur_Premiot_NihulTnua OUT CurType,
 p_Cur_Sug_Yechida OUT CurType, 
 p_Cur_Yemey_Avoda OUT CurType,
 p_Cur_Pirtey_Ovdim OUT CurType,
 p_Cur_Meafyeney_Ovdim OUT CurType,
 p_Cur_Peiluyot_Ovdim OUT CurType,   
 p_Cur_Mutamut OUT CurType, 
p_Cur_Matzav  OUT CurType, 
 p_Cur_Buses_Details OUT CurType, 
 p_Cur_Kavim_Details OUT CurType, 
 p_tar_me IN DATE,p_tar_ad IN DATE, 
 p_status_tipul  IN  TB_YAMEY_AVODA_OVDIM.status_tipul%TYPE,
 p_brerat_Mechadal  IN NUMBER, 
 p_Mis_Ishi IN NUMBER ,
 p_num_process IN NUMBER  )IS
 
  p_tarich_me   DATE;
   p_tarich_ad   DATE;
 BEGIN
    EXECUTE IMMEDIATE 'truncate table TB_TMP_OVDIM_LECHISHUV' ; 
     --  ANALYZE  TB_TMP_OVDIM_LECHISHUV  VALIDATE STRUCTURE CASCADE;
 
 p_tarich_me :=p_tar_me;
 p_tarich_ad :=p_tar_ad;

    Pkg_Calc_Worker.pro_insert_oved_lechishuv(p_Mis_Ishi,p_tar_me);
    OPEN p_Cur_Ovdim FOR
                  SELECT * FROM TB_TMP_OVDIM_LECHISHUV; 

     Pkg_Calc_Worker.pro_get_michsa_yomit(p_tarich_me , p_tarich_ad ,p_Cur_Michsa_Yomit); 
            
     Pkg_Calc_Worker.pro_get_sidurim_meyuch_rechiv(p_tarich_me , p_tarich_ad , p_Cur_SidurMeyuchadRechiv );                       
     
     Pkg_Calc_Worker.pro_get_sug_sidur_rechiv( p_tarich_me , p_tarich_ad,p_Cur_Sug_Sidur_Rechiv);
                                       
     Pkg_Calc_Worker.pro_get_premyot_view(p_Cur_Premiot_View);
     
     Pkg_Calc_Worker.pro_get_premia_yadanit(p_Cur_Premiot_Yadaniot);
     
     Pkg_Calc_Worker.pro_get_premyot_nihul_tnua(p_Cur_Premiot_NihulTnua);
     
     Pkg_Calc_Worker.pro_get_sug_yechida( p_Cur_Sug_Yechida);
     
     Pkg_Calc_Worker.pro_get_yemey_avoda (p_status_tipul,p_Cur_Yemey_Avoda,p_tarich_me , p_tarich_ad);                                 
     
     Pkg_Calc_Worker.pro_get_pirtey_ovdim(p_Cur_Pirtey_Ovdim);
     
     Pkg_Ovdim.pro_get_meafyeney_oved_all(p_Mis_Ishi,p_tarich_me ,p_tarich_ad ,  p_brerat_Mechadal,p_Cur_Meafyeney_Ovdim);
     
     Pkg_Calc_Worker.pro_get_peiluyot_ovdim(p_Cur_Peiluyot_Ovdim,p_tarich_me , p_tarich_ad); 
 
     Pkg_Utils.pro_get_ctb_mutamut(p_Cur_Mutamut);  
   
     Pkg_Calc_Worker.pro_get_Matzav_Ovdim(p_Cur_Matzav);
     
     Pkg_Calc_Worker.pro_get_buses_details(p_Cur_Buses_Details,p_tarich_me , p_tarich_ad);  
 
    Pkg_Calc_Worker.pro_get_kavim_details(p_Cur_Kavim_Details,p_tarich_me , p_tarich_ad); 
     
 
       EXCEPTION
         WHEN OTHERS THEN
              RAISE;   
 END pro_get_netunim_lechishuv;
END Pkg_Calc_worker;
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
      SELECT  0 KOD_PROFIL ,'בודד'  TEUR_PROFIL_HEB , 1 ord FROM dual 
UNION 
      SELECT KOD_PROFIL,TEUR_PROFIL_HEB , 2 ord 
        FROM CTB_PROFIL t
        WHERE LETEZUGA = 1 
        AND T.TEUR_PROFIL IN 
         (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_ProfilFilter,  ',') AS MYTABTYPE)))
        ORDER BY ord,TEUR_PROFIL_HEB
    );
        EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END  pro_get_ProfilToDisplay;

  
  
  PROCEDURE pro_get_Comment_Approval(p_cur OUT CurType) IS 
  BEGIN 
  OPEN p_cur FOR SELECT Kod_Ishur,teur_ishur  FROM(
  SELECT  -1 Kod_Ishur ,'הכל'  teur_ishur , 1 ord FROM dual 
UNION 
SELECT Kod_Ishur,
CASE WHEN LENGTH(Kod_Ishur)>=3 AND  SUBSTR(Kod_Ishur,LENGTH(Kod_Ishur) -1 ,LENGTH(Kod_Ishur))   = '11' THEN
 teur_ishur || ' , אגד תעבורה'
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
  SELECT  -1 kod_ezor ,'הכל'  teur_ezor , 1 ord FROM dual 
UNION
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
  SELECT  -1 kod_status_ishur ,'הכל'  teur_status_ishur FROM dual 
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
  (SELECT 'הכל' FullName, -1 mispar_ishi , 1 ord FROM dual
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
  SELECT Teur_Hevra,Kod_Hevra FROM 
  (SELECT 'הכל' Teur_Hevra, -1 Kod_Hevra , 1 ord FROM dual
UNION 
    SELECT Teur_Hevra , Kod_Hevra,   2 ord FROM CTB_HEVRA 
ORDER BY ord,Teur_Hevra);

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
  
  
PROCEDURE pro_get_Find_Worker_Card_2(p_cur OUT CurType, 
                                                                P_STARTDATE IN DATE,
                                                                P_ENDDATE IN DATE , 
                                                                P_Makat IN NUMBER, --1
                                                                P_SIDURNUMBER IN VARCHAR,--2
                                                                P_CARNUMBER IN VARCHAR,--3
                                                                P_SNIF IN VARCHAR,--4
                                                                P_WORKSTATION IN VARCHAR,--5
                                                                P_DAYTYPE IN VARCHAR ,--6
                                                                P_WORKERID IN VARCHAR,--7
                                                                P_SECTORISUK IN VARCHAR ,--8
                                                                P_SNIF_MASHAR IN VARCHAR , --9
                                                                P_COMPANYID IN VARCHAR ,--10
                                                                P_MAMAD IN VARCHAR --11
                                                                 ) AS
GeneralQry VARCHAR2(32767);
QryMakatDate VARCHAR2(3500);
ParamQry VARCHAR2(1000);
rc NUMBER ;
BEGIN

INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_STARTDATE:',P_STARTDATE);
INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_ENDDATE:',P_ENDDATE);
INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_Makat:',P_Makat);
INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_SIDURNUMBER:',P_SIDURNUMBER);
INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_CARNUMBER:',P_CARNUMBER);
INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_SNIF:',P_SNIF);
INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_WORKSTATION:',P_WORKSTATION);
INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_DAYTYPE:',P_DAYTYPE);
INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_WORKERID:',P_WORKERID);
INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_SECTORISUK:',P_SECTORISUK);
INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_SNIF_MASHAR:',P_SNIF_MASHAR);
INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_COMPANYID:',P_COMPANYID);
 

--QryMakatDate :=  fct_MakatDateSql(P_STARTDATE ,P_ENDDATE  ,P_Makat ,P_SIDURNUMBER ,P_WORKERID  );
--  pro_Prepare_Catalog_Details(QryMakatDate);
-- COMMIT ; sss
DBMS_OUTPUT.PUT_LINE (QryMakatDate);
GeneralQry := 'Select substr(Details.EZOR,0,1) || substr(Details.MAAMAD,0,1) || ''-'' ||  Details.MISPAR_ISHI Misparishi , activity.makat_nesia, activity.shat_yetzia, activity.mispar_knisa, 
activity.oto_no,activity.Snif_Tnua,snif.teur_snif_av,Ov.shem_mish|| '' '' ||  shem_prat full_name,So.chariga,So.hashlama,
So.out_michsa,So.meadken_acharon, so.mispar_sidur,so.shat_hatchala, so.shat_gmar,so.taarich,Dayofweek(so.taarich) Dayofweek  , 
Sidur.teur_sidur_meychad, teur_maamad_hr,isuk.teur_isuk,bus_number, license_number ,
Catalog.ACTIVITY_DATE  , Catalog.MAKAT8,Catalog.DESCRIPTION ,Catalog.SHILUT ,Catalog.MAZAN_TASHLUM,Catalog.KM,Catalog.NIHUL_NAME             
FROM  
PIVOT_PIRTEY_OVDIM  Details  ,
(SELECT po.mispar_ishi,MAX(po.ME_TARICH) me_taarich
                       FROM PIVOT_PIRTEY_OVDIM PO
                       WHERE po.isuk IS NOT NULL
                             AND (''' ||  P_STARTDATE  || '''  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE(''01/01/9999'' ,''dd/mm/yyyy''))
                               OR   ''' ||  P_ENDDATE  || '''  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE(''01/01/9999'' ,''dd/mm/yyyy''))
                                OR   po.ME_TARICH>= ''' ||  P_STARTDATE  || '''  AND   NVL(po.ad_TARICH,TO_DATE(''01/01/9999'' ,''dd/mm/yyyy''))<=  ''' ||  P_ENDDATE  || '''  )
                      GROUP BY po.mispar_ishi) RelevantDetails,
TB_PEILUT_OVDIM Activity ,OVDIM Ov,TB_SIDURIM_OVDIM so   ,CTB_SNIF_AV Snif ,CTB_SIDURIM_MEYUCHADIM Sidur  ,
        CTB_MAAMAD   Maamad ,CTB_ISUK Isuk ,TMP_CATALOG Catalog, VEHICLE_SPECIFICATIONS Mashar
WHERE 
 (Details.mispar_ishi = so.mispar_ishi )  
AND Details.mispar_ishi = RelevantDetails.mispar_ishi 
AND  Details.ME_TARICH = RelevantDetails.me_taarich
AND Details.mispar_ishi = so.mispar_ishi 
AND (so.mispar_ishi        = activity.mispar_ishi(+))  
AND  (details.mispar_ishi = Ov.mispar_ishi)  
AND ( Catalog.makat8(+) = activity.makat_nesia)
AND  (Mashar.bus_number(+) = activity.oto_no)
AND (so.mispar_sidur      = activity.mispar_sidur(+))
AND (so.shat_hatchala = activity.shat_hatchala_sidur(+)) 
AND (so.taarich           = activity.taarich(+)) 
AND (so.mispar_sidur = Sidur.kod_sidur_meyuchad(+) )    
AND  (details.maamad     = maamad.kod_maamad_hr)  
AND  (details.isuk            = Isuk.kod_isuk )
AND  (details.snif_av       = Snif.kod_snif_av )  
AND (maamad.kod_hevra = Ov.kod_hevra)
AND (maamad.kod_hevra = Snif.kod_hevra)
AND (maamad.kod_hevra = Isuk.kod_hevra)
AND  (so.taarich     BETWEEN     ''' ||  P_STARTDATE  || ''' AND ''' ||  P_ENDDATE  || ''')' ; 



--if (( P_MAMAD is not  null ) OR ( P_MAMAD <> '' )) THEN 
--    ParamQry := ParamQry || PREPARE_LIKE_OF_LIST('maamad.KOD_MAAMAD_HR', P_MAMAD) || ' AND '; 
--end if ; 
 
IF (( P_DAYTYPE IS NOT  NULL ) OR ( P_DAYTYPE <> '' )) THEN 
    ParamQry := ParamQry ||  '  AND dayofweek(so.taarich) =''ח''  ';
END IF ;  
IF (( P_SNIF IS NOT  NULL ) OR ( P_SNIF <> '' )) THEN 
    ParamQry := ParamQry ||  '  AND Snif.kod_snif_av in (' || P_SNIF || ') '; 
END IF ; 
IF (( P_WORKERID IS NOT  NULL ) OR ( P_WORKERID <> '' )) THEN 
    ParamQry := ParamQry || '  AND so.mispar_ishi in (' || P_WORKERID || ' ';
END IF ; 
IF (( P_Makat IS NOT  NULL ) OR ( P_Makat <> '' )) THEN 
    ParamQry := ParamQry ||  ' AND ' || Prepare_Like_Of_List('Activity.makat_nesia(+)', P_Makat,'''') ;
END IF ; 

IF (( P_SIDURNUMBER IS NOT  NULL ) OR ( P_SIDURNUMBER <> '' )) THEN 
    ParamQry := ParamQry || 'AND ' || Prepare_Like_Of_List('so.mispar_sidur', P_SIDURNUMBER,'') ;
END IF ; 
 DBMS_OUTPUT.PUT_LINE (P_SIDURNUMBER);
IF (( P_CARNUMBER IS NOT  NULL ) OR ( P_CARNUMBER <> '' )) THEN 
    ParamQry := ParamQry || ' AND activity.oto_no in (' || P_CARNUMBER || ')  ';
END IF ; 
IF (( P_WORKSTATION IS NOT  NULL ) OR ( P_WORKSTATION <> '' )) THEN 
    ParamQry := ParamQry || '  AND Activity.snif_tnua  in (' || P_WORKSTATION || ') ';
END IF ; 

/*
IF (( ParamQry IS NOT NULL  ) OR ( ParamQry <> '')) THEN 
  ParamQry := SUBSTR(ParamQry,0,LENGTH(ParamQry)-4); -- TO DELETE THE LAST 'AND ' 
  GeneralQry := GeneralQry || ' And ' || ParamQry;
END IF ;
*/
 GeneralQry := GeneralQry || ' order by Details.MISPAR_ISHI  , so.taarich  , so.shat_hatchala, activity.shat_yetzia';
DBMS_OUTPUT.PUT_LINE ( GeneralQry);
--execute immediate 'select count(*) from (' || GeneralQry || ')' into rc ;
OPEN p_cur FOR GeneralQry ;
                                                                
        EXCEPTION 
         WHEN OTHERS THEN 
             DBMS_OUTPUT.PUT_LINE (SQLERRM);
              RAISE;               
  END  pro_get_Find_Worker_Card_2;     
  
  
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
   else lst_day:='ח';
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
       Catalog.DESCRIPTION ,Catalog.SHILUT ,Catalog.MAZAN_TASHLUM,Catalog.KM,Catalog.NIHUL_NAME ,Catalog.ACTIVITY_DATE  , Catalog.MAKAT8
from (select S.mispar_ishi,S.TAARICH,Dayofweek(s.taarich) Dayofweek ,S.MISPAR_SIDUR,S.SHAT_HATCHALA,S.SHAT_GMAR,S.MEADKEN_ACHARON, S.CHARIGA,S.HASHLAMA,S.OUT_MICHSA,
                  P.SNIF_TNUA, P.MAKAT_NESIA,P.MISPAR_KNISA,P.SHAT_YETZIA,P.OTO_NO
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
                      and length(SO.MISPAR_SIDUR) between 4 and 5
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
                                                                 P_WorkerViewLevel IN NUMBER, 
                                                                 P_WORKERID IN VARCHAR2, 
                                                                 P_MISPAR_ISHI IN VARCHAR2 ,
                                                                 P_STARTDATE IN DATE,
                                                                 P_ENDDATE IN DATE
                                                         )AS  
GeneralQry VARCHAR2(32767);
QryMakatDate VARCHAR2(3000);
 ParamQry1 VARCHAR2(1000);
  ParamQry2 VARCHAR2(1000);
  ParamQry3 VARCHAR2(1000);
  x number;
BEGIN
DBMS_APPLICATION_INFO.SET_MODULE('pkg_reports.pro_get_Presence','get presence leoved for month');

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
                               UNION SELECT 0,null FROM dual )
                               
                                   select h.*,Details.full_name ,
                                         (Pkg_Ovdim.fun_get_meafyen_oved(h.mispar_ishi, 3, ''' ||  P_ENDDATE  || '''  )) START_TIME_ALLOWED ,
                                         (Pkg_Ovdim.fun_get_meafyen_oved(h.mispar_ishi, 4, ''' ||  P_ENDDATE  || ''' )) END_TIME_ALLOWED  ,
                                Details.teur_isuk ,Details.Teur_ezor,Details.teur_maamad_hr,Details.teur_snif_av 
                       
                            from    ( SELECT a.* ,  isuk.teur_isuk ,Ezor.Teur_ezor,Maamad.teur_maamad_hr,snif.teur_snif_av 
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
                                          Sidur.teur_sidur_meychad  ,So.LO_LETASHLUM, Sm.meafyen_53
                              FROM TB_YAMEY_AVODA_OVDIM y,
                                        TB_SIDURIM_OVDIM So  ,
                                        CTB_SIDURIM_MEYUCHADIM Sidur  ,
                                        CTB_MIKUM_YECHIDA MikumKnissa,
                                        CTB_MIKUM_YECHIDA MikumYetsia  ,
                                        Sm  ';
                                       
        IF (P_WorkerViewLevel = 5) THEN
          GeneralQry := GeneralQry || ',TMP_MANAGE_TREE Tree ';
        END IF ;    
 
 GeneralQry := GeneralQry || 'WHERE  (y.taarich     BETWEEN    ''' ||  P_STARTDATE  || ''' AND ''' ||  P_ENDDATE  || ''')
                                                  AND So.mispar_ishi(+) = y.Mispar_ishi 
                                                  AND So.taarich(+) = y.taarich
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
                                     null  ClockEntry,null ClockExit , null teur_sidur_meychad  , null LO_LETASHLUM, null  meafyen_53
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
                                                
 ORDER BY h.mispar_ishi,taarich,shat_hatchala, shat_gmar ';    
  -- GeneralQry := GeneralQry || ') ORDER BY mispar_ishi,taarich,shat_hatchala, shat_gmar  ';
  
DBMS_OUTPUT.PUT_LINE ( GeneralQry);
--execute immediate 'select count(*) from (' || GeneralQry || ')' into rc ;
OPEN p_cur FOR GeneralQry ;
       
           EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END  pro_get_Presence;     

       

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
   DBMS_APPLICATION_INFO.SET_MODULE('Pkg_Reports.pro_get_DriverWithoutTacograph','get drivers Without Tacograph for period');
 
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
  DBMS_APPLICATION_INFO.SET_MODULE('Pkg_Reports.pro_get_SumDriverNotSigned','get count drivers Without signature  for period');
 
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
   DBMS_APPLICATION_INFO.SET_MODULE('Pkg_Reports.pro_get_DriverNotSigned','get drivers details Without signature  for period');
 
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
   DBMS_APPLICATION_INFO.SET_MODULE('Pkg_Reports.Pro_Get_DisregardDrivers','get drivers that not send CA');
 
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
                                                        P_MISPAR_ISHI IN VARCHAR2,                                                                            
                                                        p_Period IN VARCHAR2) 
IS 
  p_ToDate DATE ; 
  p_FromDate DATE ; 
  i_bakasha number;
  p_kod number;
GeneralQry VARCHAR2(32767);
QryMakatDate VARCHAR2(3000);
ParamQry VARCHAR2(1000);
   lst_mispar_ishi VARCHAR2(200);
  sTnai VARCHAR2(200);
    p_Erech TB_PARAMETRIM.erech_param%TYPE ;
  BEGIN
  

  
   DBMS_APPLICATION_INFO.SET_MODULE('Pkg_Reports.Pro_Get_PremiotPresence','get premiot presence leoved for period by premia');
      p_FromDate := TO_DATE('01/' || p_Period,'dd/mm/yyyy'); /* period= 05/2009=>  v_MinLimitDate = 01/05/2009 */
    p_ToDate := ADD_MONTHS(p_FromDate,1) -1 ;    /* period= 05/2009=>  v_MaxLimitDate = 31/05/2009 */
 DBMS_OUTPUT.PUT_LINE ('1');
 
 IF (( P_MISPAR_ISHI IS NOT   NULL ) OR ( P_MISPAR_ISHI<>'' )) THEN 
  lst_mispar_ishi:= ' and t.mispar_ishi in (SELECT X FROM TABLE(CAST(Convert_String_To_Table(''' || P_MISPAR_ISHI  || ''',  '','') AS MYTABTYPE))) ';
END IF ; 

      SELECT SP.KOD_RACHIV_NOCHECHUT into p_kod  
      FROM CTB_SUGEY_PREMIOT Sp 
       WHERE SP.KOD_PREMIA=  p_kod_premia;

  INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'p_kod_premia:',p_kod_premia);
  INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'P_MISPAR_ISHI:',lst_mispar_ishi);
  INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'p_Period:',p_Period);
  
     GeneralQry :='   select YAO.MISPAR_ISHI,YAO.TAARICH,YAO.STATUS, Dayofweek(Yao.taarich)   Dayofweek,
                                      Yao.Hamarat_shabat,Yao.LINA,Yao.BITUL_ZMAN_NESIOT,Yao.HALBASHA,
                                      sidur.Dakot_nochechut,Decode(sidur.num,1,sidur.ERECH_YOMI,'''') ERECH_YOMI,sidur.BAKASHA_ID,sidur.MISPAR_SIDUR, sidur.shat_hatchala ,
                                      sidur.shat_gmar,  sidur.Pitzul_hafsaka,sidur.Chariga, sidur.Hashlama,sidur.Out_michsa ,
                                      Gil.Teur_kod_gil , Isuk.teur_isuk, Maamad.teur_maamad_hr, Ezor.Teur_ezor, Snif.teur_snif_av ,
                                      Ov.kod_hevra, (Ov.shem_mish|| '' '' ||  Ov.shem_prat) Full_Name ,  
                                       decode(sidur.TEUR_SIDUR_MEYCHAD,'''','''',  sidur.TEUR_SIDUR_MEYCHAD || '' '' || sidur.MISPAR_SIDUR ) TEUR_SIDUR_MEYCHAD,
                                      (Pkg_Ovdim.fun_get_meafyen_oved(Ov.mispar_ishi, 56, ''' || p_ToDate || ''')) Wording_date, 
                                       pkg_reports.fun_get_hour( trim (Pkg_Ovdim.fun_get_meafyen_oved(Ov.mispar_ishi, 3, ''' || p_ToDate || ''' ))) START_TIME_ALLOWED ,
                                       pkg_reports.fun_get_hour( trim (Pkg_Ovdim.fun_get_meafyen_oved(Ov.mispar_ishi, 4, ''' || p_ToDate || '''))) END_TIME_ALLOWED,
                                       pkg_reports.FUN_GET_ERECH_RECHIV(Ov.mispar_ishi,Yao.taarich ,sidur.Bakasha_ID,126) michsa_yomit,
                                       pr.TEUR_PREMIA
                            from
                                  TB_YAMEY_AVODA_OVDIM Yao ,
                                   OVDIM Ov ,
                                  (  select So.MISPAR_ISHI,So.TAARICH,So.MISPAR_SIDUR,So.SHAT_HATCHALA, So.shat_gmar,  So.Pitzul_hafsaka,So.Chariga, So.Hashlama,So.Out_michsa ,So.TEUR_SIDUR_MEYCHAD,
                                      Premiya.ERECH_RECHIV Dakot_nochechut,Premiya.BAKASHA_ID,Premiya.KOD_RECHIV,premiya.ERECH_YOMI,
                                      DENSE_RANK() OVER(PARTITION BY  So.mispar_ishi,So.TAARICH   ORDER BY  So.SHAT_HATCHALA  DESC  ) AS num                                        
                                     from
                                            (select S.MISPAR_ISHI,S.TAARICH,S.MISPAR_SIDUR,S.SHAT_HATCHALA, S.shat_gmar,  S.Pitzul_hafsaka,S.Chariga, S.Hashlama,S.Out_michsa ,sidurey_nochechut.TEUR_SIDUR_MEYCHAD
                                             from TB_SIDURIM_OVDIM S ,
                                                    (SELECT  Ct.TEUR_SIDUR_MEYCHAD,CT.Kod_Sidur_Meyuchad,min(sm.Me_Taarich) Me_Taarich ,max(sm.Ad_Taarich) Ad_Taarich                               
                                                     FROM  CTB_SIDURIM_MEYUCHADIM CT , TB_SIDURIM_MEYUCHADIM SM
                                                     WHERE CT.Kod_Sidur_Meyuchad=SM. Mispar_sidur 
                                                          AND  ''' || p_FromDate || ''' between sm.Me_Taarich and sm.Ad_Taarich';
                                if (  p_kod_premia =103) then 
                                       sTnai:= ' and CT.Kod_Sidur_Meyuchad=99229 ';
                                else 
                                        sTnai:= ' and  SM.Kod_Meafyen in(53,54,85) ';   
                                end if;           
                                               --           AND ( or ( and ''' || p_kod_premia || ''' =103 ))
                                      GeneralQry := GeneralQry || sTnai ||  '     group by   Ct.TEUR_SIDUR_MEYCHAD,CT.Kod_Sidur_Meyuchad ) sidurey_nochechut      
                                             where  s.taarich BETWEEN   ''' || p_FromDate || ''' AND  ''' || p_ToDate || '''
                                                and s.mispar_sidur =  sidurey_nochechut.Kod_Sidur_Meyuchad 
                                                and s.taarich BETWEEN sidurey_nochechut.Me_Taarich   AND sidurey_nochechut.Ad_Taarich
                                                and nvl(s.MISPAR_SIDUR,0) <>99200 
                                             order by s.taarich            ) So,

                                            (select cy.MISPAR_ISHI , cy.TAARICH,cy.mispar_sidur,CY.SHAT_HATCHALA, cy.ERECH_RECHIV,cy.BAKASHA_ID,cy.KOD_RECHIV,yo.ERECH_RECHIV ERECH_YOMI
                                              from TB_CHISHUV_SIDUR_OVDIM cy,TB_CHISHUV_YOMI_OVDIM yo,
                                                   (
                                                       select c.MISPAR_ISHI , max(c.BAKASHA_ID) BAKASHA_ID
                                                        from    TB_CHISHUV_CHODESH_OVDIM c 
                                                        where c.taarich BETWEEN    ''' || p_FromDate || ''' AND  ''' || p_ToDate || '''
                                                           and c.Kod_Rechiv =  ' || p_kod || '
                                                        group by (c.MISPAR_ISHI )   
                                                    ) co
                                              where cy.MISPAR_ISHI=co.MISPAR_ISHI
                                                 and cy.BAKASHA_ID= co.BAKASHA_ID
                                                 and cy.MISPAR_ISHI=yo.MISPAR_ISHI
                                                 and cy.BAKASHA_ID= yo.BAKASHA_ID
                                                 and cy.Kod_Rechiv =  yo.Kod_Rechiv 
                                                 and cy.taarich =  yo.taarich 
                                                 and yo.taarich  BETWEEN    ''' || p_FromDate || ''' AND  ''' || p_ToDate || '''
                                                 and cy.Kod_Rechiv =  ' || p_kod || '
                                                    )premiya      
                                    where 
                                                Premiya.MISPAR_ISHI(+) = So.MISPAR_ISHI
                                          and Premiya.TAARICH(+) = So.TAARICH  
                                          and Premiya.mispar_sidur(+) = So.mispar_sidur  
                                          and Premiya.SHAT_HATCHALA(+) = So.SHAT_HATCHALA  
                                     order by So.taarich       ) sidur ,

                                     ( SELECT * 
                                        FROM ( SELECT  t.mispar_ishi , MAX( t.ME_TARICH) OVER (PARTITION BY t.mispar_ishi) me_tarich, t.ad_tarich,t.SNIF_AV,t.EZOR, t.MAAMAD,t.ISUK, t.GIL,
                                                                row_number() OVER (PARTITION BY t.mispar_ishi ORDER BY me_tarich desc) seq
                                                   FROM  PIVOT_PIRTEY_OVDIM t
                                                   WHERE t.isuk IS NOT NULL 
                                                              AND ( ''' || p_FromDate || '''     BETWEEN ME_TARICH  AND NVL(t.ad_TARICH,TO_DATE(  ''01/01/9999''  ,  ''dd/mm/yyyy''  ))
                                                              OR   ''' || p_ToDate || '''     BETWEEN ME_TARICH AND NVL(t.ad_TARICH,TO_DATE(  ''01/01/9999''  ,  ''dd/mm/yyyy''  )) 
                                                              OR t.ME_TARICH>= ''' || p_FromDate || '''   AND NVL(t.ad_TARICH,TO_DATE( ''01/01/9999''   ,''dd/mm/yyyy''  ))<=   ''' || p_ToDate || '''   )';
                                        
                                         IF (( P_MISPAR_ISHI IS NOT   NULL ) OR ( P_MISPAR_ISHI<>'' )) THEN 
                                         GeneralQry := GeneralQry || lst_mispar_ishi;
                                         END IF;                     
                 GeneralQry := GeneralQry ||  ' ORDER BY mispar_ishi) a
                                       WHERE a.seq=1 )   Details ,
                                   CTB_EZOR Ezor,
                                   CTB_SNIF_AV Snif ,
                                   CTB_MAAMAD  Maamad ,
                                   CTB_ISUK  Isuk, 
                                   CTB_KOD_GIL Gil ,
                                   CTB_SUGEY_PREMIOT pr     
                            where YAO.MISPAR_ISHI in( select c.MISPAR_ISHI  
                                                                      from    TB_CHISHUV_CHODESH_OVDIM c 
                                                                      where c.taarich BETWEEN  ''' || p_FromDate || ''' AND  ''' || p_ToDate || '''
                                                                          and c.Kod_Rechiv =  ' || p_kod || '  )
                               and YAO.MISPAR_ISHI = OV.MISPAR_ISHI
                               and Yao.taarich BETWEEN  ''' || p_FromDate || ''' AND  ''' || p_ToDate || ''' ' ;
                                if (  p_kod_premia =103) then 
                                       sTnai:= ' and YAO.MISPAR_ISHI= sidur.MISPAR_ISHI
                                                     and YAO.TAARICH =sidur.TAARICH  ';
                                else 
                                        sTnai:= '  and YAO.MISPAR_ISHI= sidur.MISPAR_ISHI(+)
                                                       and YAO.TAARICH =sidur.TAARICH (+)     ';   
                                end if;           
                              -- and YAO.MISPAR_ISHI= sidur.MISPAR_ISHI(+)
                              -- and YAO.TAARICH =sidur.TAARICH (+)        
                              GeneralQry := GeneralQry || sTnai ||  '  and YAO.MISPAR_ISHI = Details.MISPAR_ISHI
                               and Details.GIL = Gil.kod_gil_hr 
                               and Ezor.kod_ezor  = Details.ezor 
                               and Ezor.kod_hevra = Ov.kod_hevra
                               and Snif.kod_snif_av  = details.snif_av  
                               and Snif.kod_hevra = Ov.kod_hevra 
                               and Isuk.kod_isuk  = details.isuk        
                               and Isuk.kod_hevra = Ov.kod_hevra   
                               and maamad.kod_maamad_hr  =  details.maamad  
                               and maamad.KOD_HEVRA =  Ov.kod_hevra
                               and pr.KOD_PREMIA   =  ' || p_kod_premia || '
                              order by YAO.MISPAR_ISHI,YAO.TAARICH,sidur.shat_hatchala,sidur.shat_gmar';
           
       DBMS_OUTPUT.PUT_LINE('GeneralQry : = ' || GeneralQry);                                                                                                                                    
        OPEN p_cur FOR   GeneralQry ;

               EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;         
 END  Pro_Get_PremiotPresence;
 
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
                   REPLACE(TB.ZMAN_HATCHALA,'-',' ') || (CASE     TB.HUAVRA_LESACHAR WHEN '1' THEN  ' -  העברה לשכר '  ELSE ''  END)  Pirtey_Riza , 2 ord , TB.ZMAN_HATCHALA ZMAN_HATCHALA
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
        SELECT 'הכל ' description  , -1 kod FROM dual
    UNION  
        SELECT 'חברים' description,1 kod  FROM dual 
    UNION 
        SELECT 'שכירים' , 2  FROM dual;
                         
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
SELECT -1 KOD_HEADRUT_CLALI , 'הכל ' TEUR_CLALI  , 1 ord FROM dual
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
SELECT -1 KOD , 'הכל ' Description  , 1 ord FROM dual
UNION  
SELECT 5 KOD , '5 ימים ' Description  , 2 ord FROM dual
UNION
SELECT 6 KOD , '6 ימים ' Description  , 3 ord FROM dual
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
                    CASE IdReport WHEN 1 THEN 'פרטי ריצה לדוח' ELSE 'פרטי חודש לדוח'  END koteret, 
                CASE strChodashim WHEN NULL THEN NULL ELSE  SUBSTR(strChodashim,0,7)   END chodesh
            FROM TB_BAKASHOT TB  
               WHERE TB.BAKASHA_ID =SUBSTR(listMispareiRizot,0,INSTR(listMispareiRizot,',')-1)  
UNION 
      
      SELECT  TB.BAKASHA_ID, TB.TEUR, TB.ZMAN_HATCHALA, TB.HUAVRA_LESACHAR ,               
                    CASE IdReport WHEN 1 THEN 'פרטי ריצה להשוואה' ELSE  'פרטי חודש שנבחר להשוואה' END koteret, 
                CASE strChodashim WHEN NULL THEN NULL ELSE  SUBSTR(strChodashim,9,7)   END chodesh
           FROM TB_BAKASHOT TB  
               WHERE TB.BAKASHA_ID =SUBSTR(listMispareiRizot,INSTR(listMispareiRizot,',')+1); 
                /*  SELECT  TB.BAKASHA_ID, TB.TEUR, TB.ZMAN_HATCHALA, TB.HUAVRA_LESACHAR ,
                   case IdReport when 1 then 
                          ( case instr(listMispareiRizot,',' || to_char(TB.BAKASHA_ID))  when 0 then ':פרטי ריצה לדוח' else ':פרטי ריצה להשוואה'  end)
                   else
                         (     case instr(listMispareiRizot,',' || to_char(TB.BAKASHA_ID))  when 0 then ':פרטי חודש לדוח' else ':פרטי חודש שנבחר להשוואה'  end) end koteret,
               case strChodashim when null then null
               else ( case instr(listMispareiRizot,',' || to_char(TB.BAKASHA_ID))  when 0 then  substr(strChodashim,0,7)  else substr(strChodashim,9,7)  end) end chodesh
            --   case  instr(strChodashim,',')   when 7 then substr(strChodashim,0,7) else substr(strChodashim,7,7) )end chodesh
        --       case instr(listMispareiRizot,',' || to_char(TB.BAKASHA_ID))  when 0 then 'פרטי ריצה לדוח:' else 'פרטי ריצה להשוואה:' end koteret
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
IF  ( trim(P_CHODESH_HARAZA) != '-1' ) THEN  
    p_FromDate := TO_DATE('01/' || P_CHODESH_HARAZA ,'dd/mm/yyyy'); /* period= 05/2009=>  v_MinLimitDate = 01/05/2009 */
     END IF ;   
 
    OPEN p_cur FOR   
        SELECT BAKASHA_ID,Pirtey_Riza FROM(
                 SELECT -1 BAKASHA_ID, '' Pirtey_Riza, 1 ord , SYSDATE  ZMAN_HATCHALA FROM dual 
            UNION
                 SELECT TB.BAKASHA_ID, TB.BAKASHA_ID || ' -  ' || TB.TEUR || ' -  ' || 
                   REPLACE(TB.ZMAN_HATCHALA,'-',' ') || (CASE     TB.HUAVRA_LESACHAR WHEN '1' THEN  ' -  העברה לשכר '  ELSE ''  END)  Pirtey_Riza , 2 ord , TB.ZMAN_HATCHALA ZMAN_HATCHALA
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
                              SELECT 'מעמד' description,1kod  FROM dual 
                         UNION 
                          SELECT 'עיסוק' , 2  FROM dual;
                         
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
                              SELECT DISTINCT s.SHEM_DOCH_BAKOD,s.TEUR_DOCH
                         FROM   CTB_SUGEY_DOCHOT s,
                                         TB_PROFIL_DOCHOT p
                         WHERE s.KOD_SUG_DOCH = p.KOD_SUG_DOCH
                                AND s.pail=1
                                AND(P_PROFIL IS NOT NULL AND p.KOD_PROFIL IN (SELECT X FROM TABLE(CAST(Convert_String_To_Table(P_PROFIL ,  ',') AS MYTABTYPE))) )
                        ORDER BY     s.TEUR_DOCH;

                         
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
    INSERT INTO TB_TMP_DATES_PIVOT(DATES_LIST,MISPAR_ISHI,KOD_RECHIV)  
    VALUES(StrList,CMispar_Ishi,CKod_Rechiv);

--DBMS_OUTPUT.PUT_LINE('Mispar_ishi:' || CMispar_Ishi ||'Kod_Rechiv:' || CKod_Rechiv ||  ',the List is: '|| SubStr( StrAllTheDateList,0,length(StrAllTheDateList)-1));
--DBMS_OUTPUT.PUT_LINE('List is: '|| StrList);
StrAllTheDateList := '';
StrList := ''; 
END LOOP;
CLOSE cursor_Groupby;

UPDATE TB_TMP_DATES_PIVOT Pivot SET
PIVOT.SUM_RECHIV = 
( 
                                        SELECT Sum_rechiv
                                        FROM 
                                        (
                                        SELECT ORIGIN.DATES_LIST, ORIGIN.MISPAR_ISHI, ORIGIN.KOD_RECHIV,sums.Sum_rechiv
                                        FROM TB_TMP_DATES_PIVOT Origin , 
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
                THEN ''מותאם'' ||
                         Ov.Matching_2 / 60 || 
                         '' שעות''
                WHEN (Ov.Matching_1 IS NOT NULL AND Ov.Matching_2 IS NULL )   
                THEN ''מותאם''         
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
                                            P_SNIF IN VARCHAR2 ,
                                            P_MAMAD IN VARCHAR2 ,
                                            P_ISUK VARCHAR2 ,
                                            P_EZOR IN VARCHAR ,
                                            P_COMPANYID  IN VARCHAR2 ,
                                            P_MisparIshi  IN VARCHAR2 ,
                                            P_KOD_YECHIDA IN VARCHAR2 ,
                                            P_SECTORISUK IN VARCHAR2 ,
                                            p_cur OUT CurType ) IS
GeneralQry VARCHAR2(32767);
QryMakatDate VARCHAR2(3000);
ParamQry VARCHAR2(1000);
BEGIN 

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
GeneralQry  := 'Select
Ov.shem_mish|| '' '' ||  Ov.shem_prat full_name ,Details.Mispar_ishi ,Gil.Teur_kod_gil    , Maamad.teur_maamad_hr  teur_maamad , Isuk.teur_isuk 
FROM OVDIM Ov , 
PIVOT_PIRTEY_OVDIM Details  ,
(SELECT po.mispar_ishi,MAX(po.ME_TARICH) me_taarich
                       FROM PIVOT_PIRTEY_OVDIM PO
                       WHERE po.isuk IS NOT NULL
                             AND (''' ||  P_STARTDATE  || '''  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE(''01/01/9999'' ,''dd/mm/yyyy''))
                               OR   ''' ||  P_ENDDATE  || '''  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE(''01/01/9999'' ,''dd/mm/yyyy''))
                                OR   po.ME_TARICH>= ''' ||  P_STARTDATE  || '''  AND   NVL(po.ad_TARICH,TO_DATE(''01/01/9999'' ,''dd/mm/yyyy''))<=  ''' ||  P_ENDDATE  || '''  )
                      GROUP BY po.mispar_ishi) RelevantDetails,

CTB_KOD_GIL Gil ,
 CTB_MAAMAD Maamad,
 CTB_ISUK Isuk ,
 CTB_SNIF_AV Snif ,
 CTB_YECHIDA Yechida
WHERE 
Ov.mispar_ishi = Details.mispar_ishi
AND Details.mispar_ishi = RelevantDetails.mispar_ishi 
AND Details.ME_TARICH = RelevantDetails.me_taarich (+)
AND Snif.KOD_HEVRA = OV.KOD_HEVRA 
AND Snif.kod_snif_av(+) = details.snif_av
AND Isuk.KOD_HEVRA = OV.KOD_HEVRA
AND MAAMAD.KOD_HEVRA = OV.KOD_HEVRA
AND maamad.kod_maamad_hr(+) =  details.maamad
AND Details.GIL         = Gil.kod_gil_hr
AND Maamad.kod_hevra =Isuk.kod_hevra 
AND YECHIDA.KOD_YECHIDA = DETAILS.YECHIDA_IRGUNIT
AND YECHIDA.KOD_HEVRA = OV.KOD_HEVRA
AND Isuk.kod_isuk(+) = details.isuk ' ;
 
IF (( P_MisparIshi IS NOT  NULL ) OR ( P_MisparIshi <> '' )) THEN 
    ParamQry := ParamQry || '  Details.mispar_ishi in (' || P_MisparIshi || ') AND ';
END IF ; 


IF (  p_ezor <> '-1' ) THEN 
    ParamQry := ParamQry || '  Details.ezor in (' || p_ezor || ') AND ';
END IF ; 

IF (P_COMPANYID <> '-1' ) THEN 
    ParamQry := ParamQry || '  OV.KOD_HEVRA in (' || P_COMPANYID || ') AND ';
END IF ;

IF (( P_MAMAD IS NOT  NULL ) OR ( P_MAMAD <> '' )) THEN 
    ParamQry := ParamQry || '  Details.MAAMAD in (' || P_MAMAD || ') AND ';
END IF ; 

IF (( P_ISUK IS NOT  NULL ) OR ( P_ISUK <> '' )) THEN 
    ParamQry := ParamQry || '  Isuk.KOD_isuk in (' || P_ISUK || ') AND ';
END IF ; 
 
IF (( P_SNIF IS NOT  NULL ) OR ( P_SNIF <> '' )) THEN 
    ParamQry := ParamQry || '  details.snif_av in (' || P_SNIF || ') AND ';
END IF ; 
 
IF (( P_KOD_YECHIDA IS NOT  NULL ) OR ( P_KOD_YECHIDA <> '' )) THEN 
    ParamQry := ParamQry || ' YECHIDA.KOD_YECHIDA in (' || P_KOD_YECHIDA || ') AND ';
END IF ; 

IF (( P_SECTORISUK IS NOT  NULL ) OR ( P_SECTORISUK <> '' )) THEN 
    ParamQry := ParamQry || ' Isuk.KOD_SECTOR_ISUK in (' || P_SECTORISUK || ') AND ';
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
            

END pro_GetMainDetailsAverage;

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
 
 OPEN p_Cur FOR         
 select  z.taarich,z.teur_yom,SUBSTR(LPAD(z.MIKUM_SHAON,5, '0'),0,3) KOD,m.TEUR_MIKUM_YECHIDA,COUNT(z.MISPAR_ISHI)num
 from(
 select  x.mispar_ishi,x.taarich ,x.SHAT_HITYAZVUT,x.MIKUM_SHAON,Dayofweek(x.TAARICH)   || '  ' || c.TEUR_YOM teur_yom,
      LAG (x.SHAT_HITYAZVUT,1)
      OVER(partition by x.mispar_ishi,x.taarich order by x.SHAT_HITYAZVUT asc) next_hour
from
  ( select h.MISPAR_ISHI  ,h.taarich,h.SHAT_HITYAZVUT ,h.MIKUM_SHAON
        from
            (SELECT T.MISPAR_ISHI  , t.taarich,t.SHAT_HITYAZVUT,t.MIKUM_SHAON
            FROM  TB_HITYAZVUT_PUNDAKIM t,ovdim v
            WHERE   T.MISPAR_ISHI = V.MISPAR_ISHI and
                          t.TAARICH BETWEEN P_STARTDATE AND P_ENDDATE  AND
                    --      to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))>5 
                          (to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))>=5 and to_char(t.SHAT_HITYAZVUT,'HH24:mi:ss')<>'05:00:00') 
                         -- T.MIKUM_SHAON='9601'
            union all
            SELECT T.MISPAR_ISHI  , (t.taarich-1) taarich,t.SHAT_HITYAZVUT,t.MIKUM_SHAON
            FROM  TB_HITYAZVUT_PUNDAKIM t,ovdim v
            WHERE   T.MISPAR_ISHI = V.MISPAR_ISHI and
                          t.TAARICH BETWEEN P_STARTDATE AND ( P_ENDDATE+1)  AND
                          ((to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))>=0 and to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))<5)  or to_char(t.SHAT_HITYAZVUT,'HH24:mi:ss')='05:00:00') 
                        --   to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))>=0 and to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))<=5 
                        --  T.MIKUM_SHAON='9601'   
                          ) h
where  h.TAARICH BETWEEN P_STARTDATE AND P_ENDDATE
 
order by h.SHAT_HITYAZVUT ) x ,
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
--INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'p_BakashaId:',p_BakashaId);
--INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'p_ReportName:',p_ReportName);
--INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'p_MisparIshi:',p_MisparIshi);
--INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'p_Extension:',p_Extension);
--INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'p_DestinationFolder:',p_DestinationFolder);
--INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'p_SendToMail:',p_SendToMail);

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

  
 PROCEDURE  pro_get_Definition_Reports(p_BakashaId IN  TB_BAKASHOT.bakasha_id%TYPE, p_cur OUT CurType) AS 
BEGIN 
     OPEN p_cur FOR          
                 SELECT DISTINCT t.KOD_SUG_DOCH KOD, c.SHEM_DOCH_BAKOD NAME,c.TEUR_DOCH, c.TVACH_TAARICHIM,T.BAKASHA_ID,T.EXTENSION_TYPE
                 ,T.MISPAR_ISHI
               FROM TB_DOCHOT_MISPAR_ISHI t,
                                CTB_SUGEY_DOCHOT c
                WHERE t.PAIL=1 AND C.PAIL =1 
                                            AND t.KOD_SUG_DOCH = c.KOD_SUG_DOCH
                                       AND T.BAKASHA_ID = p_BakashaId ;
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
             S.TEUR_SNIF_AV, REPLACE(REPLACE(y.TEUR_MIKUM_YECHIDA,'-פונדק','' ),'מחלף','מח.') TEUR_MIKUM_YECHIDA, H.mispar_ishi,
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
                    g.F5,g.NOT_ON_TIME,g.PUBLIC_COMPLAINTS 
                
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
                               VEHICLE_SPECIFICATIONS v
                   WHERE s.MISPAR_ISHI = P_MISPAR_ISHI
                        AND s.TAARICH = trunc(P_TAARICH)  --to_date('25/10/2012','dd/mm/yyyy')  -- 
                        AND s.MISPAR_ISHI = p.MISPAR_ISHI
                        AND s.TAARICH = p.TAARICH
                        AND s.MISPAR_SIDUR =p.MISPAR_SIDUR
                        AND s.SHAT_HATCHALA = p.SHAT_HATCHALA_SIDUR
                        AND P.OTO_NO = V.BUS_NUMBER
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
              pkg_reports.fun_get_list_oto(P_TAARICH,P_MISPAR_ISHI,5, s.MISPAR_SIDUR) S_LIST
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
           SELECT -1 KOD_SNIF_TNUAA,'הכל' teur FROM dual 
          UNION 
           SELECT KOD_SNIF_TNUAA ,teur_snif_tnuaa ||' (' ||KOD_SNIF_TNUAA||')' teur
          FROM CTB_SNIFEY_TNUAA s 
          WHERE s.KOD_SNIF_TNUAA between p_from and p_to
          ORDER BY KOD_SNIF_TNUAA;
     
END   pro_GetSnifeyTnuaByEzor;

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
END   pro_get_rechivim_to_average ;



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
select 
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
--        soInternal.TAARICH   between to_date('01/07/2012','dd/MM/yyyy') and to_date('30/07/2012','dd/MM/yyyy') 
        group by soInternal.mispar_ishi,soInternal.TAARICH
 ) so ,
(SELECT po.mispar_ishi,MAX(po.ME_TARICH) me_taarich
                       FROM PIVOT_PIRTEY_OVDIM PO
                       WHERE po.isuk IS NOT NULL
                             AND (p_FromDate) BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                               OR p_ToDate  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                               OR   po.ME_TARICH>= p_FromDate  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<= p_ToDate  
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
        ISUK.KOD_ISUK = PO.ISUK ;

end get_GetDayDataEggT ;


procedure pro_ProfilLinesDetails( P_STARTDATE IN DATE,
                                        P_ENDDATE IN DATE , 
                                        P_Makat IN varchar2,
                                        p_cur OUT CurType)   as 
    GeneralQry VARCHAR2(30000);             
BEGIN
GeneralQry:='Select   distinct p.makat_nesia,p.TAARICH  FROM TB_PEILUT_OVDIM p, 
            (select KOD_ELEMENT    from  TB_MEAFYENEY_ELEMENTIM where KOD_MEAFYEN = 4 AND ERECH = ''1'') me
            WHERE substr(MAKAT_NESIA,2,2) = me.KOD_ELEMENT
            and              p.taarich between''' || P_STARTDATE || ''' and ''' || P_ENDDATE || '''';

--Pkg_Reports.pro_Prepare_Catalog_Details(GeneralQry);

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
        FROM 
        (
            SELECT makat_nesia , taarich,
            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 1 THEN 1 ELSE 0 END EMP_BEF_SHERUT, 
            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 3 THEN 1 ELSE 0 END EMP_BEF_NAMAK, 
            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 2 THEN 1 ELSE 0 END EMP_BEF_EMP, 
            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 NOT IN (1,2,3) THEN 1 ELSE 0 END EMP_BEF_OTHER,
            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 1 THEN 1 ELSE 0 END EMP_AFT_SHERUT, 
            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 3 THEN 1 ELSE 0 END EMP_AFT_NAMAK, 
            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 2 THEN 1 ELSE 0 END EMP_AFT_EMP, 
            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 NOT IN (1,2,3) THEN 1 ELSE 0 END EMP_AFT_OTHER,
            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 1 THEN PREV_TIME ELSE 0 END T_EMP_BEF_SHERUT, 
            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 3 THEN PREV_TIME ELSE 0 END T_EMP_BEF_NAMAK, 
            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 2 THEN PREV_TIME ELSE 0 END T_EMP_BEF_EMP, 
            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 NOT IN (1,2,3) THEN PREV_TIME ELSE 0 END T_EMP_BEF_OTHER,
            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 1 THEN NEXT_TIME ELSE 0 END T_EMP_AFT_SHERUT, 
            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 3 THEN NEXT_TIME ELSE 0 END T_EMP_AFT_NAMAK, 
            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 2 THEN NEXT_TIME ELSE 0 END T_EMP_AFT_EMP, 
            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 NOT IN (1,2,3) THEN NEXT_TIME ELSE 0 END T_EMP_AFT_OTHER,
            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 1 THEN PREV_KM ELSE 0 END KM_EMP_BEF_SHERUT, 
            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 3 THEN PREV_KM ELSE 0 END KM_EMP_BEF_NAMAK, 
            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 2 THEN PREV_KM ELSE 0 END KM_EMP_BEF_EMP, 
            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 NOT IN (1,2,3) THEN PREV_KM ELSE 0 END KM_EMP_BEF_OTHER,
            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 1 THEN NEXT_TIME ELSE 0 END KM_EMP_AFT_SHERUT, 
            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 3 THEN NEXT_TIME ELSE 0 END KM_EMP_AFT_NAMAK, 
            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 2 THEN NEXT_TIME ELSE 0 END KM_EMP_AFT_EMP, 
            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 NOT IN (1,2,3) THEN NEXT_TIME ELSE 0 END KM_EMP_AFT_OTHER,
             first_value(description) over(partition by makat_nesia ) description,
             first_value(MAZAN_TICHNUN) over(partition by makat_nesia ) MAZAN_TICHNUN,
            first_value(KISUY_TOR) over(partition by makat_nesia ) KISUY_TOR,
             first_value(km) over(partition by makat_nesia ) km,
             first_value(SNIF) over(partition by makat_nesia ) SNIF
            FROM 
                (
                    SELECT makat_nesia,taarich, Sherut_nb,
                    LAG (Sherut_nb ,1 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) PREV_SHERUT,
                    LAG (Sherut_nb ,2 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) PREV_SHERUT2,
                    LEAD (Sherut_nb ,1 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) NEXT_SHERUT,
                    LEAD (Sherut_nb ,2 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) NEXT_SHERUT2,
                    LAG (MAZAN_TICHNUN ,1 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) PREV_TIME,
                    LEAD (MAZAN_TICHNUN ,1 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) NEXT_TIME,
                    LAG (KM ,1 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) PREV_KM,
                    LEAD (KM ,1 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) NEXT_KM,
                    km , KISUY_TOR , SNIF , MAZAN_TICHNUN, description
                    FROM 
                        ( 
                        Select  activity.makat_nesia,activity.taarich,activity.mispar_ishi,ACTIVITY.MISPAR_sidur,
                                Pkg_Tnua.fn_get_makat_type(activity.makat_nesia) sherut_nb,
                                ACTIVITY.SHAT_YETZIA,
                                c.km , 
                                c.KISUY_TOR ,
                                ACTIVITY.SNIF_TNUA SNIF , 
                                c.MAZAN_TICHNUN       ,
                                c.description     
                            FROM    TB_PEILUT_OVDIM Activity , 
                            TB_SIDURIM_OVDIM so ,
                            TMP_CATALOG c,
                            (select KOD_ELEMENT    from
                            TB_MEAFYENEY_ELEMENTIM where KOD_MEAFYEN = 4 AND ERECH = ''1'') me -- get elements which have  minutes
                            WHERE  (so.mispar_ishi          = activity.mispar_ishi)  
                            AND (so.mispar_sidur  = activity.mispar_sidur)
                            AND (so.shat_hatchala = activity.shat_hatchala_sidur) 
                            AND (so.taarich           = activity.taarich)  
                            and Activity.taarich BETWEEN  ''' || P_STARTDATE || ''' AND  ''' || P_ENDDATE || '''  
                            and so.taarich BETWEEN  ''' || P_STARTDATE || ''' AND  ''' || P_ENDDATE || '''  
                            AND substr(MAKAT_NESIA,2,2) = me.KOD_ELEMENT
                            and         c.ACTIVITY_DATE  (+) = ACTIVITY.taarich
                            and c.MAKAT8  (+) = ACTIVITY.MAKAT_NESIA ';
                            
                            
IF (( P_Makat IS NOT  NULL ) OR ( P_Makat <> '' )) THEN 
    GeneralQry := GeneralQry || ' AND ' || Prepare_Like_Of_List('Activity.makat_nesia', P_Makat,'%') ;
END IF ; 


GeneralQry := GeneralQry ||  ')
                                                order by makat_nesia
                                        )    
                                    WHERE Sherut_nb<>2
                                )
                                GROUP BY makat_nesia ,taarich,description,MAZAN_TICHNUN,KISUY_TOR,km,SNIF ';

DBMS_OUTPUT.PUT_LINE(GeneralQry );
        
OPEN p_cur   FOR GeneralQry;


end pro_ProfilLinesDetails;
           
procedure pro_ProfilLinesSummed( P_STARTDATE IN DATE,
                                        P_ENDDATE IN DATE , 
                                        P_Makat IN varchar2,
                                        p_cur OUT CurType)   as 
    GeneralQry VARCHAR2(30000);             
BEGIN
GeneralQry:='Select   distinct p.makat_nesia,p.TAARICH  FROM TB_PEILUT_OVDIM p, 
            (select KOD_ELEMENT    from  TB_MEAFYENEY_ELEMENTIM where KOD_MEAFYEN = 4 AND ERECH = ''1'') me
            WHERE substr(MAKAT_NESIA,2,2) = me.KOD_ELEMENT
            and              p.taarich between''' || P_STARTDATE || ''' and ''' || P_ENDDATE || '''';



Pkg_Reports.pro_Prepare_Catalog_Details(GeneralQry);


GeneralQry := '
        select makat_nesia ,description,MAZAN_TICHNUN,KISUY_TOR,km,SNIF,count(makat_nesia) SumOfMakat,
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
        FROM 
        (
            SELECT makat_nesia , taarich,
            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 1 THEN 1 ELSE 0 END EMP_BEF_SHERUT, 
            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 3 THEN 1 ELSE 0 END EMP_BEF_NAMAK, 
            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 2 THEN 1 ELSE 0 END EMP_BEF_EMP, 
            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 NOT IN (1,2,3) THEN 1 ELSE 0 END EMP_BEF_OTHER,
            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 1 THEN 1 ELSE 0 END EMP_AFT_SHERUT, 
            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 3 THEN 1 ELSE 0 END EMP_AFT_NAMAK, 
            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 2 THEN 1 ELSE 0 END EMP_AFT_EMP, 
            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 NOT IN (1,2,3) THEN 1 ELSE 0 END EMP_AFT_OTHER,
            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 1 THEN PREV_TIME ELSE 0 END T_EMP_BEF_SHERUT, 
            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 3 THEN PREV_TIME ELSE 0 END T_EMP_BEF_NAMAK, 
            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 2 THEN PREV_TIME ELSE 0 END T_EMP_BEF_EMP, 
            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 NOT IN (1,2,3) THEN PREV_TIME ELSE 0 END T_EMP_BEF_OTHER,
            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 1 THEN NEXT_TIME ELSE 0 END T_EMP_AFT_SHERUT, 
            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 3 THEN NEXT_TIME ELSE 0 END T_EMP_AFT_NAMAK, 
            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 2 THEN NEXT_TIME ELSE 0 END T_EMP_AFT_EMP, 
            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 NOT IN (1,2,3) THEN NEXT_TIME ELSE 0 END T_EMP_AFT_OTHER,
            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 1 THEN PREV_KM ELSE 0 END KM_EMP_BEF_SHERUT, 
            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 3 THEN PREV_KM ELSE 0 END KM_EMP_BEF_NAMAK, 
            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 = 2 THEN PREV_KM ELSE 0 END KM_EMP_BEF_EMP, 
            CASE WHEN  PREV_SHERUT =2 AND PREV_SHERUT2 NOT IN (1,2,3) THEN PREV_KM ELSE 0 END KM_EMP_BEF_OTHER,
            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 1 THEN NEXT_TIME ELSE 0 END KM_EMP_AFT_SHERUT, 
            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 3 THEN NEXT_TIME ELSE 0 END KM_EMP_AFT_NAMAK, 
            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 = 2 THEN NEXT_TIME ELSE 0 END KM_EMP_AFT_EMP, 
            CASE WHEN  NEXT_SHERUT =2 AND NEXT_SHERUT2 NOT IN (1,2,3) THEN NEXT_TIME ELSE 0 END KM_EMP_AFT_OTHER,
             first_value(description) over(partition by makat_nesia ) description,
             first_value(MAZAN_TICHNUN) over(partition by makat_nesia ) MAZAN_TICHNUN,
            first_value(KISUY_TOR) over(partition by makat_nesia ) KISUY_TOR,
             first_value(km) over(partition by makat_nesia ) km,
             first_value(SNIF) over(partition by makat_nesia ) SNIF
            FROM 
                (
                    SELECT makat_nesia,taarich, Sherut_nb,
                    LAG (Sherut_nb ,1 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) PREV_SHERUT,
                    LAG (Sherut_nb ,2 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) PREV_SHERUT2,
                    LEAD (Sherut_nb ,1 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) NEXT_SHERUT,
                    LEAD (Sherut_nb ,2 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) NEXT_SHERUT2,
                    LAG (MAZAN_TICHNUN ,1 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) PREV_TIME,
                    LEAD (MAZAN_TICHNUN ,1 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) NEXT_TIME,
                    LAG (KM ,1 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) PREV_KM,
                    LEAD (KM ,1 ) OVER (PARTITION BY MISPAR_ISHI,taarich ORDER BY SHAT_YETZIA ) NEXT_KM,
                    km , KISUY_TOR , SNIF , MAZAN_TICHNUN, description
                    FROM 
                        ( 
                        Select  activity.makat_nesia,activity.taarich,activity.mispar_ishi,ACTIVITY.MISPAR_sidur,
                                Pkg_Tnua.fn_get_makat_type(activity.makat_nesia) sherut_nb,
                                ACTIVITY.SHAT_YETZIA,
                                c.km , 
                                c.KISUY_TOR ,
                                ACTIVITY.SNIF_TNUA SNIF , 
                                c.MAZAN_TICHNUN       ,
                                c.description     
                            FROM    TB_PEILUT_OVDIM Activity , 
                            TB_SIDURIM_OVDIM so ,
                            TMP_CATALOG c,
                            (select KOD_ELEMENT    from
                            TB_MEAFYENEY_ELEMENTIM where KOD_MEAFYEN = 4 AND ERECH = ''1'') me -- get elements which have  minutes
                            WHERE  (so.mispar_ishi          = activity.mispar_ishi)  
                            AND (so.mispar_sidur  = activity.mispar_sidur)
                            AND (so.shat_hatchala = activity.shat_hatchala_sidur) 
                            AND (so.taarich           = activity.taarich)  
                            and Activity.taarich BETWEEN  ''' || P_STARTDATE || ''' AND  ''' || P_ENDDATE || '''  
                            and so.taarich BETWEEN  ''' || P_STARTDATE || ''' AND  ''' || P_ENDDATE || '''  
                            AND substr(MAKAT_NESIA,2,2) = me.KOD_ELEMENT
                            and         c.ACTIVITY_DATE  (+) = ACTIVITY.taarich
                            and c.MAKAT8  (+) = ACTIVITY.MAKAT_NESIA ';
                            
                            
IF (( P_Makat IS NOT  NULL ) OR ( P_Makat <> '' )) THEN 
    GeneralQry := GeneralQry || ' AND ' || Prepare_Like_Of_List('Activity.makat_nesia', P_Makat,'%') ;
END IF ; 


GeneralQry := GeneralQry ||  ')
                                                order by makat_nesia
                                        )    
                                    WHERE Sherut_nb<>2
                                )
                                GROUP BY makat_nesia ,description,MAZAN_TICHNUN,KISUY_TOR,km,SNIF ';


       
DBMS_OUTPUT.PUT_LINE(GeneralQry );
        
OPEN p_Cur   FOR GeneralQry;

end pro_ProfilLinesSummed;



   
PROCEDURE pro_get_Bakashot_details(p_Cur OUT CurType)
  IS
     BEGIN
     OPEN p_Cur FOR
             select  x.bakasha_id, (   x.bakasha_id  || ' - ' ||
                                    ( case when x.maamad='1' then 'חברים' else case when  x.maamad='2' then 'שכירים' else 'חברים ושכירים' end end) || ' - ' ||   
                                    x.chodesh
                            ) teur
            from(
                    select b.bakasha_id,
                            (select  p.erech from tb_bakashot_params p where p.PARAM_ID=1 and b.bakasha_id= p.bakasha_id ) maamad,
                             (select  p.erech from tb_bakashot_params p where p.PARAM_ID=2 and b.bakasha_id= p.bakasha_id) chodesh
                    from tb_bakashot b 
                    where sug_bakasha=1
        ) x
         order by   x.bakasha_id desc;

     EXCEPTION
        WHEN OTHERS THEN
        RAISE;
  END pro_get_Bakashot_details;
                                
END Pkg_Reports;
/
