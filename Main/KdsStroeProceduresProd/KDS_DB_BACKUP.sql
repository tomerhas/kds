CREATE OR REPLACE PACKAGE          PKG_ERRORS AS
TYPE    CurType      IS    REF  CURSOR;

/******************************************************************************
   NAME:       PKG_ERRORS
   PURPOSE:

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        26/04/2009             1. Created this package.
******************************************************************************/

/*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        27/04/2009      vered       1. ????? ????? ????? ?????
*/

--FUNCTION fn_find_errors(mispar_ishi in pirtey_ovdim.mispar_ishi%type, kartis_avoda_dt in date ) return integer;
--FUNCTION fn_is_hour_missing(mispar_ishi in pirtey_ovdim.mispar_ishi%type,kartis_avoda_dt in date) return integer;
--FUNCTION fn_excption_error(mispar_ishi in pirtey_ovdim.mispar_ishi%type,kartis_avoda_dt in date) return integer;


PROCEDURE pro_get_oved_sidurim_peilut(p_mispar_ishi in ovdim.mispar_ishi%type, p_date in date, p_cur out CurType);
function NahagWithSidurTafkidLeloMeafy(p_isuk in number,p_lo_tashlum in number,p_kod_siba in number) return nvarchar2 ;
function IsSidurShaonim(p_mispar_sidur in number,p_taarich in Date) return nvarchar2 ;
FUNCTION fun_hachtama_bemakom_haasaka(p_mispar_ishi in number,p_taarich Date, p_mispar_sidur in number,p_mikum_hachatama in number) return nvarchar2 ;

FUNCTION fun_get_YECHIDA_AV_LENOCH(kod_yechida  in number ) return number;
PROCEDURE pro_del_oved_errors(p_mispar_ishi in tb_shgiot.mispar_ishi%type, p_date in tb_shgiot.taarich%type);
PROCEDURE pro_get_lookup_tables(p_cur out CurType);

PROCEDURE pro_get_oved_matzav(p_mispar_ishi in matzav_ovdim.mispar_ishi%type, p_taarich in matzav_ovdim.taarich_siyum%type, p_cur out CurType) ;
PROCEDURE pro_get_oved_yom_avoda_details(p_mispar_ishi in tb_yamey_avoda_ovdim.mispar_ishi%type, p_taarich in tb_yamey_avoda_ovdim.taarich%type, p_Cur out CurType);
FUNCTION fn_is_oto_number_exists(p_oto_no in tb_peilut_ovdim.oto_no%type, p_date in date) return number;
FUNCTION fn_is_duplicate_shat_yetiza(p_mispar_ishi in ovdim.mispar_ishi%type, p_date in date) return number;
FUNCTION fn_is_zman_nesia_define(p_merkaz_erua in ctb_zman_nsiaa_mishtane.merkaz_erua%type,
                                 p_mikum_shaon_knisa in ctb_zman_nsiaa_mishtane.mikum_yaad%type,
                                 p_mikum_shaon_yetiza in ctb_zman_nsiaa_mishtane.mikum_yaad%type) return number;
PROCEDURE pro_get_sug_sidur_meafyenim(p_cur out CurType);
PROCEDURE pro_get_oved_yom_avoda_UDT(p_mispar_ishi in tb_yamey_avoda_ovdim.mispar_ishi%type, p_taarich in tb_yamey_avoda_ovdim.taarich%type,
                                             p_coll_meafeney_ovdim out coll_meafyeney_oved);
PROCEDURE pro_ins_sidurim_ovdim(p_coll_sidurim_ovdim in coll_sidurim_ovdim);
PROCEDURE pro_upd_sidurim_ovdim(p_coll_sidurim_ovdim in coll_sidurim_ovdim);
PROCEDURE pro_del_sidurim_ovdim(p_coll_sidurim_ovdim in coll_sidurim_ovdim,p_type_update in  number);
--PROCEDURE pro_upd_sidurim_ovdim(p_coll_sidurim_ovdim in COLL_TEST);
PROCEDURE pro_upd_yamey_avoda_ovdim(p_coll_yamey_avoda_ovdim in coll_yamey_avoda_ovdim);
PROCEDURE pro_upd_peilut_ovdim(p_coll_obj_peilut_ovdim in coll_obj_peilut_ovdim);
PROCEDURE pro_ins_peilut_ovdim(p_coll_obj_peilut_ovdim in coll_obj_peilut_ovdim);
PROCEDURE pro_del_peilut_ovdim(p_coll_obj_peilut_ovdim in coll_obj_peilut_ovdim);
PROCEDURE pro_ins_sidurim_ovdim_trail(p_obj_sidurim_ovdim in obj_sidurim_ovdim,p_sug_peula in trail_sidurim_ovdim.sug_peula%type);
PROCEDURE pro_upd_sidur_oved(p_obj_sidurim_ovdim in obj_sidurim_ovdim);
PROCEDURE pro_del_sidur_oved(p_obj_sidurim_ovdim in obj_sidurim_ovdim);
PROCEDURE pro_ins_yom_avoda_oved_trail(p_obj_yamey_avoda_ovdim in obj_yamey_avoda_ovdim);
PROCEDURE pro_upd_yom_avoda_oved(p_obj_yamey_avoda_ovdim in obj_yamey_avoda_ovdim);
PROCEDURE pro_upd_peilut_oved(p_obj_peilut_ovdim in obj_peilut_ovdim);
PROCEDURE pro_ins_peilut_ovdim_trail(p_obj_peilut_ovdim in obj_peilut_ovdim, p_sug_peula in trail_peilut_ovdim.sug_peula%type);
PROCEDURE pro_del_peilut_oved(p_obj_peilut_ovdim in obj_peilut_ovdim);
PROCEDURE pro_shinuy_kelet(p_coll_yamey_avoda_ovdim_upd in coll_yamey_avoda_ovdim,
                           p_coll_sidurim_ovdim_upd     in coll_sidurim_ovdim,
                           p_coll_sidurim_ovdim_ins       IN   coll_sidurim_ovdim,
                           p_coll_sidurim_ovdim_del     in coll_sidurim_ovdim,
                           p_coll_obj_peilut_ovdim_upd  in coll_obj_peilut_ovdim,
                           p_coll_obj_peilut_ovdim_ins  in coll_obj_peilut_ovdim,
                           p_coll_obj_peilut_ovdim_del  in coll_obj_peilut_ovdim);
PROCEDURE pro_oved_update_fields(p_mispar_ishi in ovdim.mispar_ishi%type, p_date in date, p_cur out CurType);
PROCEDURE pro_upd_card_status(p_mispar_ishi in tb_yamey_avoda_ovdim.mispar_ishi%type,
                              p_card_date in tb_yamey_avoda_ovdim.taarich%type,
                               p_status in tb_yamey_avoda_ovdim.status%type,
							    p_user_id in tb_yamey_avoda_ovdim.meadken_acharon%type);
PROCEDURE pro_get_ctb_shgiot(p_error_code in ctb_shgiot.kod_shgia%type, p_cur out CurType);
PROCEDURE pro_ins_approval_errors(p_obj_shgiot_meusharot in obj_shgiot_meusharot );

FUNCTION fn_is_approval_errors_exists(p_obj_shgiot_meusharot in obj_shgiot_meusharot, p_level in number) return number;
PROCEDURE pro_get_errors_for_field(p_field_name in ctb_shgiot.natun_lebdika%type, p_shgiot_leoved in number, p_cur out CurType);
PROCEDURE pro_upd_tar_ritzat_shgiot(p_mispar_ishi in tb_yamey_avoda_ovdim.mispar_ishi%type,
                              p_card_date in tb_yamey_avoda_ovdim.taarich%type,
							  p_shgiot_letzuga IN tb_yamey_avoda_ovdim.shgiot_letezuga_laoved%type);

PROCEDURE pro_is_duplicate_travel(p_mispar_ishi in tb_peilut_ovdim.mispar_ishi%type,
		 									  	 						 p_taarich  in tb_peilut_ovdim.taarich%type,
																		 p_makat_nesia in tb_peilut_ovdim.makat_nesia%type,
																		 p_shat_yetzia in tb_peilut_ovdim.shat_yetzia%type,
																		 p_mispar_knisa in tb_peilut_ovdim.mispar_knisa%type,
																		 p_Cur OUT CurType);

PROCEDURE pro_have_sidur_chofef(p_mispar_ishi in tb_sidurim_ovdim.mispar_ishi%type,
		 									  	 						 p_taarich  in tb_sidurim_ovdim.taarich%type,
																		 p_mispar_sidur in tb_sidurim_ovdim.mispar_sidur%type,
																		 p_shat_hatchala in tb_sidurim_ovdim.shat_hatchala%type,
																		 p_shat_gmar in tb_sidurim_ovdim.shat_gmar%type,
																		 p_param_chafifa in number,
																		 p_Cur OUT CurType);

FUNCTION  fn_count_shgiot_letzuga(p_arr_Kod_Shgia IN VARCHAR2) return number;

PROCEDURE get_idkuney_rashemet(p_mispar_ishi IN TB_idkun_rashemet.mispar_ishi%type,
		  										   							       p_taarich  IN TB_idkun_rashemet.taarich%type,
																				   p_Cur OUT CurType);
																				   

  PROCEDURE pro_check_have_sidur_grira(p_mispar_ishi in tb_sidurim_ovdim.mispar_ishi%type,
		 									  	 						 p_taarich  in tb_sidurim_ovdim.taarich%type,
																		 p_Cur OUT CurType) ;
	
	PROCEDURE pro_get_shgiot_no_active( p_cur out CurType);		
    procedure pro_get_all_shgiot(p_cur out CurType);
    PROCEDURE pro_get_shgiot_active(p_cur OUT CurType);
procedure pro_upd_approval_errors(p_mispar_ishi in tb_sidurim_ovdim.mispar_ishi%type,
                                  p_taarich  in tb_sidurim_ovdim.taarich%type,
                                  p_mispar_sidur in tb_sidurim_ovdim.mispar_sidur%type,
                                  p_shat_hatchala in tb_sidurim_ovdim.shat_hatchala%type,
                                  p_shat_yetzia in tb_peilut_ovdim.shat_yetzia%type,
                                  p_mispar_knisa in tb_peilut_ovdim.mispar_knisa%type,
                                  p_new_mispar_sidur in tb_sidurim_ovdim.mispar_sidur%type,
                                  p_new_shat_hatchala in tb_sidurim_ovdim.shat_hatchala%type,
                                  p_new_shat_yetzia in tb_peilut_ovdim.shat_yetzia%type);
procedure pro_upd_approval_errors(p_mispar_ishi in tb_sidurim_ovdim.mispar_ishi%type,
                                  p_taarich  in tb_sidurim_ovdim.taarich%type,
                                  p_mispar_sidur in tb_sidurim_ovdim.mispar_sidur%type,
                                  p_shat_hatchala in tb_sidurim_ovdim.shat_hatchala%type,                                  
                                  p_new_mispar_sidur in tb_sidurim_ovdim.mispar_sidur%type,
                                  p_new_shat_hatchala in tb_sidurim_ovdim.shat_hatchala%type);
                                                                    
 PROCEDURE get_approval_errors(p_mispar_ishi IN TB_idkun_rashemet.mispar_ishi%type,
		  										   							       p_taarich  IN TB_idkun_rashemet.taarich%type,
																				   p_Cur OUT CurType) ;
																				   
	  PROCEDURE pro_upd_approval_errors(p_coll_shgiot_meusharot in coll_shgiot_meusharot) ;			
     -- PROCEDURE pro_del_sidurim_ovdim(p_coll_sidurim_ovdim_del IN coll_sidurim_ovdim,  p_coll_sidurim_ovdim_ins IN  OUT coll_sidurim_ovdim,p_type_update IN  NUMBER);
	PROCEDURE pro_Delete_Errors(p_mispar_ishi IN NUMBER,p_date IN DATE);			
											                                        													 																			   
END PKG_ERRORS;
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
                                                                 P_MISPAR_ISHI IN VARCHAR2 ,
                                                                 P_STARTDATE IN DATE,
                                                                 P_ENDDATE IN DATE,
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
                                                                        p_cur OUT CurType); 
    procedure pro_cnt_ovdey_meshek_shabat(P_STARTDATE IN DATE,
                                                                        P_ENDDATE IN DATE , 
                                                                        p_cur OUT CurType)  ;    

PROCEDURE  pro_get_kod_snif_av( P_KOD_EZOR in CTB_SNIF_AV.EZOR%type,
                                p_Cur OUT CurType );  
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
procedure pro_get_tigburim_details(P_STARTDATE IN DATE,
                                    P_ENDDATE IN DATE , 
                                    p_cur OUT CurType) ;            

procedure pro_get_pirtey_ovdey_kytanot(P_STARTDATE IN DATE,
                                                            P_ENDDATE IN DATE , 
                                                            p_cur OUT CurType) ;  
PROCEDURE  pro_get_Report_Details(p_shem_doch IN  CTB_SUGEY_DOCHOT.SHEM_DOCH_BAKOD%TYPE, p_cur OUT CurType)  ;                                                                                                                                                                                                                                                                                        
END Pkg_Reports;
/


CREATE OR REPLACE PACKAGE          PKG_SIDURIM  AS

TYPE    CurType      IS    REF  CURSOR;
PROCEDURE pro_get_data(p_Kod IN VARCHAR2,p_Period in VARCHAR2, p_Cur OUT CurType);
PROCEDURE pro_get_matching_description(p_Prefix in VARCHAR2,  p_Cur OUT CurType);
PROCEDURE pro_get_matching_kod(p_Prefix in VARCHAR2,  p_Cur OUT CurType);
PROCEDURE pro_get_description_by_kod(p_Kod in VARCHAR2,p_Desc out VARCHAR2);
PROCEDURE pro_get_kod_by_description(p_Desc in CTB_SIDURIM_MEYUCHADIM.TEUR_SIDUR_MEYCHAD%type,p_Kod out CTB_SIDURIM_MEYUCHADIM.KOD_SIDUR_MEYUCHAD%type);
PROCEDURE pro_get_details_Kod(p_Cur OUT CurType );
PROCEDURE pro_get_history(p_FilterKod in varchar2 , p_Kod in VARCHAR2,p_ToDate in VARCHAR2,  p_Cur out Curtype);
PROCEDURE pro_upd_data(p_KodMeafyen number,p_MisparSidur number,p_MeTaarich date,p_AdTaarich date,p_Erech VARCHAR2 ,p_SugNatun VARCHAR2,p_comment varchar2, p_taarich_idkun_acharon DATE,p_meadken_acharon NUMBER) ;
PROCEDURE pro_ins_data(p_KodMeafyen number,p_MisparSidur number,p_MeTaarich date,p_AdTaarich date,p_Erech VARCHAR2 ,p_SugNatun VARCHAR2,p_comment varchar2, p_taarich_idkun_acharon DATE,p_meadken_acharon NUMBER) ;
PROCEDURE pro_Del_data(p_KodMeafyen number,p_MisparSidur number,p_MeTaarich date);

PROCEDURE get_sidur(p_sidur_position IN VARCHAR2,  p_mispar_ishi IN TB_SIDURIM_OVDIM.MISPAR_ISHI%type,p_taarich  IN TB_SIDURIM_OVDIM.TAARICH%type, p_Cur OUT CurType);
PROCEDURE get_sidurim_meyuchadim_all(p_tar_me IN tb_sidurim_meyuchadim.me_taarich%type,
                                                                                                p_tar_ad  IN tb_sidurim_meyuchadim.me_taarich%type,
                                                                                         p_Cur OUT CurType) ;

PROCEDURE pro_get_ctb_sidurim_meafyen(p_taarich in pivot_sidurim_meyuchadim.AD_TARICH%type, p_cur out CurType);

PROCEDURE pro_ins_upd_sidurim_ovdim(p_mispar_ishi IN tb_sidurim_ovdim.mispar_ishi%type,
                                                                                       p_mispar_sidur IN tb_sidurim_ovdim.mispar_sidur%type,
                                                                                     p_taarich IN  tb_sidurim_ovdim.taarich%type,
                                                                                  p_shat_hatchala IN tb_sidurim_ovdim.shat_hatchala%type,
                                                                                    p_shat_gmar IN tb_sidurim_ovdim.shat_gmar%type,
                                                                                p_user_upd  IN tb_sidurim_ovdim.MEADKEN_ACHARON%type);
                                                                                
PROCEDURE pro_ins_upd_sidurim_ovdim(p_mispar_ishi IN tb_sidurim_ovdim.mispar_ishi%type,
                                                            p_mispar_sidur IN tb_sidurim_ovdim.mispar_sidur%type,
                                                            p_taarich IN  tb_sidurim_ovdim.taarich%type,
                                                            p_shat_hatchala IN tb_sidurim_ovdim.shat_hatchala%type,
                                                            p_shat_gmar IN tb_sidurim_ovdim.shat_gmar%type,
                                                            p_user_upd  IN tb_sidurim_ovdim.MEADKEN_ACHARON%type,
                                                            p_mikum_shaon_knisa IN tb_sidurim_ovdim.mikum_shaon_knisa%type,
                                                            p_mikum_shaon_yetzia IN tb_sidurim_ovdim.mikum_shaon_yetzia%type,
                                                            p_course_num in tb_sidurim_ovdim.course_num%type);
PROCEDURE pro_get_sidurim_leoved(p_mispar_ishi IN tb_sidurim_ovdim.mispar_ishi%type,
                                                                               p_taarich IN  tb_sidurim_ovdim.taarich%type,
                                                                            p_cur out CurType);

PROCEDURE pro_get_meafyen_sidur_by_kod(P_Kod_Sidur in integer,
                                                                                                          P_Taarich in VARCHAR2,
                                                                                                      p_Cur out CurType) ;

 PROCEDURE pro_get_teur_whithoutlist(p_Prefix in VARCHAR2 , whithOutList in VARCHAR2,   whithOutListMisSidur in VARCHAR2  ,p_Cur OUT CurType);
 PROCEDURE pro_get_kod_whithoutlist(p_Prefix in VARCHAR2 , whithOutList in VARCHAR2,   whithOutListMisSidur in VARCHAR2  ,p_Cur OUT CurType);

 PROCEDURE pro_get_meafyen_sidur_ragil(P_Sug_Sidur in integer,
                                                                                                 P_Taarich in VARCHAR2,
                                                                                                      p_Cur out CurType);
PROCEDURE  calling_Pivot_Sidurim_M;
PROCEDURE pro_Pivot_Sidurim_Meyuchadim ;

    /************************************************************************/
PROCEDURE pro_get_yom_viza( p_Cur out CurType);
PROCEDURE pro_get_sug_hazmanat_viza( p_Cur out CurType);
PROCEDURE pro_get_kod_mivza_viza( p_Cur out CurType);
PROCEDURE pro_get_lina( p_Cur out CurType);
PROCEDURE ProGetMusach_O_Machsan(p_Taarich  in VARCHAR2, p_Cur out CurType) ;
--PROCEDURE ProGetMusach_O_Machsan(p_Taarich  in CTB_MAHSANIM.ME_TAARICH_TOKEF%type, p_Cur out CurType) ;

PROCEDURE get_tmp_sidurim_meyuchadim(p_tar_me IN tb_sidurim_meyuchadim.me_taarich%type,
                                                                                                p_tar_ad  IN tb_sidurim_meyuchadim.me_taarich%type,
                                                                                         p_Cur OUT CurType) ;
PROCEDURE get_tb_sadot_nosafim_lesidur(p_cur out CurType);
PROCEDURE pro_get_meafyeney_sidur(p_cur out CurType);
PROCEDURE pro_get_siba_lo_letashlum(p_mispar_ishi IN tb_sidurim_ovdim.mispar_ishi%type,p_date IN  tb_sidurim_ovdim.taarich%type, p_mispar_sidur IN tb_sidurim_ovdim.mispar_sidur%type ,p_shat_hatchala in varchar2,  p_teur_siba out varchar2);
                                                                               
PROCEDURE Pivot_test ;
PROCEDURE pro_get_kod_element(p_Prefix varchar2, p_kod_element in varchar2,  p_value in varchar2  ,p_Cur OUT CurType);

PROCEDURE pro_get_meafyeny_sidur_by_id(p_date in pivot_sidurim_meyuchadim.AD_TARICH%type,p_sidur_number in PIVOT_SIDURIM_MEYUCHADIM.MISPAR_SIDUR%type, p_cur out CurType);
function fun_check_meafyen_exist(p_kod_element in number,p_kod_meafyen in number) return number;
PROCEDURE pro_get_course_presence(p_date in date, p_sidur in tb_sidurim_ovdim.mispar_sidur%type,  p_course_num in tb_sidurim_ovdim.course_num%type, p_cur out CurType);
function GetSnifTnuaBySidur(p_mispar_ishi number, p_taarich date, p_mispar_sidur number, p_shat_hatchala date) return number;
END PKG_SIDURIM;
/
CREATE OR REPLACE PACKAGE BODY          Pkg_Errors AS

PROCEDURE pro_get_oved_sidurim_peilut(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                               p_date IN DATE, p_cur OUT CurType)
IS
BEGIN
   DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'pkg_errors',action_name => 'pro_get_oved_sidurim_peilut');
	
    OPEN p_cur FOR
    SELECT * FROM
	(SELECT o.mispar_ishi, o.mispar_sidur,o.bitul_o_hosafa, o.shat_gmar, o.shat_hatchala, o.yom_visa,
           o.chariga,NVL(o.pitzul_hafsaka,0)pitzul_hafsaka,o.taarich,o.shayah_leyom_kodem,o.mispar_shiurey_nehiga,
           o.out_michsa ,o.mezake_halbasha,
		   o.mikum_shaon_knisa,o.mikum_shaon_yetzia,
           o.hashlama,o.lo_letashlum,o.mivtza_visa,o.tafkid_visa,
           o.kod_siba_ledivuch_yadani_in, o.kod_siba_ledivuch_yadani_out, o.kod_siba_lo_letashlum,
           o.shat_hatchala_letashlum, o.shat_gmar_letashlum,o.mezake_nesiot,
           o.tosefet_grira, o.achuz_knas_lepremyat_visa,o.achuz_viza_besikun,
           o.mispar_musach_o_machsan,o.sug_hazmanat_visa, o.sug_hashlama,
           o.heara, o.taarich_idkun_acharon, po.taarich_idkun_acharon taarich_idkun_acharon_peilut, o.meadken_acharon,
           o.sector_visa, o.nidreshet_hitiatzvut, o.shat_hitiatzvut, o.ptor_mehitiatzvut,o.Hachtama_Beatar_Lo_Takin,
		   po.kisuy_tor,po.makat_nesia,po.shat_yetzia,po.oto_no,po.mispar_sidur peilut_mispar_sidur,
		  po.heara heara_peilut,  po.mikum_bhirat_nesia_netzer,  po.mispar_sidur_netzer,
		  po.oto_no_netzer,po.shat_bhirat_nesia_netzer,po.shilut_netzer,
		po.shat_yetzia_netzer,po.kod_shinuy_premia,po.makat_netzer,
           po.mispar_siduri_oto,po.mispar_visa,po.mispar_knisa, po.dakot_bafoal, po.imut_netzer,
           po.bitul_o_hosafa peilut_bitul_o_hosafa,po.km_visa,  po.snif_tnua,po.mispar_matala,po.teur_nesia,
           sug_yom.erev_shishi_chag,sug_yom.shbaton, TO_CHAR(o.taarich,'d') iDay,ym.sug_yom, --mao.kod_matzav,
           v_sidurm.zakay_lezman_halbasha halbash_kod,
           v_sidurm.shat_hatchala_muteret,
           v_sidurm.shat_gmar_muteret, 
           v_sidurm.zakay_michutz_lamichsa,
           --v_sidurm.shat_hatchala_muteret hour_kod2,
           v_sidurm.sidur_asur_ledaveach_peilut no_peilot_kod,
           v_sidurm.asur_ledaveach_mispar_rechev no_oto_no,
           v_sidurm.sidur_namlak_visa sidur_visa_kod,
           v_sidurm.sector_avoda, v_sidurm.sidur_lo_nivdak_sofash,
           v_sidurm.zakay_lehashlama_avur_sidur hashlama_kod,
           v_sidurm.zakay_lechariga, v_sidurm.zakay_lehamara ,
           v_sidurm.zakay_leaman_nesia,
           v_sidurm.hova_ledaveach_peilut peilut_required_kod,
           v_sidurm.asur_ledivuach_lechevrot_bat sidur_not_valid_kod,
           --v_sidurm.sidur_netzer sidur_netzer_kod,
           v_sidurm.hukiyut_beshabaton sidur_not_in_shabton_kod,
           v_sidurm.sidur_misug_headrut headrut_type_kod,
           v_sidurm.sug_avoda,v_sidurm.hova_ledaveach_mispar_machsan,
           v_sidurm.shaon_nochachut,  v_sidurm.nitan_ledaveach_bmachala_aruc,
           v_sidurm.mispar_sidur mispar_sidur_myuhad,
           v_sidurm.sidur_kaytanot_veruey_kayiz sidur_in_summer,
           v_sidurm.lo_letashlum_automati,
		   v_sidurm.zakay_lepizul, v_sidurm.lidrosh_kod_mivtza,
           v_sidurm.rashai_ledaveach,
           v_sidurm.kizuz_al_pi_hatchala_gmar,
           v_sidurm.nitan_ledaveach_ad_taarich,
           v_sidurm.element1_hova,v_sidurm.element2_hova,v_sidurm.element3_hova,
           v_sidurm.sidur_lo_chosem,
           v_sidurm.zakay_lelina,v_sidurm.tokef_hatchala,v_sidurm.tokef_siyum,
           v_element.peula_o_yedia_bilvad element_for_yedia,
           v_element.hova_mispar_rechev bus_number_must,
           v_element.divuach_besidur_visa divuch_in_sidur_visa,
           v_element.kod_lechishuv_premia,  v_element.lo_nizbar_leshat_gmar,
           v_element.sector_zvira_zman_haelement element_zvira_zman,
           v_element.erech_element element_in_minutes ,
           v_element.mispar_sidur_matalot_tnua,v_element.divuach_besidur_meyuchad,
           v_element.bitul_biglal_ichur_lasidur,v_element.nesia_reika,
		   v_element.lehitalem_hafifa_bein_nesiot, v_element.Hamtana element_hamtana,
           v_element.hamtana_eilat,
		   v_element.Lershut ,v_sidurm.hovat_hityazvut  ,
		   v_element.erech_element,v_element.peilut_mashmautit,
           v_element.lehitalem_beitur_reyka,           
		   DECODE(o.bitul_o_hosafa,1,1,3,1,0) sidur_mevutal ,
		   DECODE(po.bitul_o_hosafa,1,1,3,1,0) peilut_mevutelet,
           sm.teur_sidur_meychad,sl.LEBDIKAT_SHGUIM,
           nvl(O.SUG_SIDUR,0) SUG_SIDUR,
           O.MENAHEL_MUSACH_MEADKEN,o.shat_hatchala_letashlum_musach, o.shat_gmar_letashlum_musach,
            pkg_errors.fun_get_YECHIDA_AV_LENOCH(to_number(substr( o.MIKUM_SHAON_KNISA,0,3) ) ) mikum_av_knisa,
             pkg_errors.fun_get_YECHIDA_AV_LENOCH(to_number(substr( o.mikum_shaon_yetzia,0,3) ) ) mikum_av_yatzia,
            decode(pkg_errors.NahagWithSidurTafkidLeloMeafy(p.isuk,o.lo_letashlum, o.KOD_SIBA_LO_LETASHLUM ),'true','true', 
                                                                                        pkg_errors.fun_hachtama_bemakom_haasaka(o.mispar_ishi,o.taarich,o.mispar_sidur,   nvl(TO_NUMBER(SUBSTR(LPAD( o.MIKUM_SHAON_KNISA    , 5, '0'),0,3)),0) ) ) knisa_tkina_err197 ,
            decode(pkg_errors.NahagWithSidurTafkidLeloMeafy(p.isuk,o.lo_letashlum, o.KOD_SIBA_LO_LETASHLUM ),'true','true', 
                                                                                        pkg_errors.fun_hachtama_bemakom_haasaka(o.mispar_ishi,o.taarich,o.mispar_sidur,  nvl(TO_NUMBER(SUBSTR(LPAD( o.MIKUM_SHAON_Yetzia    , 5, '0'),0,3)),0) )  )yatzia_tkina_err198 
           
    FROM TB_SIDURIM_OVDIM o,TB_PEILUT_OVDIM po,
         TB_YAMIM_MEYUCHADIM ym,CTB_SUGEY_YAMIM_MEYUCHADIM sug_yom,
         pivot_sidurim_meyuchadim v_sidurm,
         pivot_MEAFYENEY_ELEMENTIM v_element,
         CTB_SIDURIM_MEYUCHADIM  sm,
		 CTB_SIBOT_LOLETASHLUM sl,
           pivot_pirtey_ovdim p
    WHERE o.mispar_ishi = po.mispar_ishi(+)
          AND o.taarich = po.taarich(+)
          AND o.mispar_sidur= po.mispar_sidur(+)
          AND o.shat_hatchala = po.shat_hatchala_sidur(+)
          AND o.mispar_ishi=p_mispar_ishi
          AND o.taarich=p_date
         -- AND (((o.taarich=p_date)and (o.shayah_leyom_kodem<>1 or o.shayah_leyom_kodem is null)) or ((o.taarich=p_date+1) and (o.shayah_leyom_kodem=1)))
          AND o.taarich=ym.taarich(+)
          AND ym.sug_yom=sug_yom.sug_yom(+)
          AND v_sidurm.mispar_sidur(+)=o.mispar_sidur
          AND p_date BETWEEN v_sidurm.me_tarich(+) AND v_sidurm.ad_tarich(+)
          AND TO_NUMBER(SUBSTR(po.makat_nesia,2,2)) = v_element.kod_element(+)
          AND p_date BETWEEN v_element.me_tarich(+) AND v_element.ad_tarich(+)
          AND sm.kod_sidur_meyuchad(+) = o.mispar_sidur
		  AND o.KOD_SIBA_LO_LETASHLUM= sl.KOD_SIBA(+)
		  AND (sl.PAIL=1 OR sl.PAIL IS NULL)
           and o.mispar_ishi = p.mispar_ishi 
          and p_date between P.ME_TARICH and  NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy')) )
    ORDER BY  sidur_mevutal ,shat_hatchala,mispar_sidur  ,peilut_mevutelet,shat_yetzia,mispar_knisa ;

EXCEPTION
        WHEN OTHERS THEN
		RAISE;

END pro_get_oved_sidurim_peilut;

function NahagWithSidurTafkidLeloMeafy(p_isuk in number,p_lo_tashlum in number,p_kod_siba in number) return nvarchar2 is
begin
        if ((p_isuk between 500 and 600) and  p_lo_tashlum=1 and p_kod_siba in(4,5,17)) then
            return 'true';
       else return 'false';
       end if;
end NahagWithSidurTafkidLeloMeafy;



function IsSidurShaonim(p_mispar_sidur in number,p_taarich in Date) return nvarchar2 is
meafyen54 number;
begin
    begin
      SELECT S.SHAON_NOCHACHUT into meafyen54
      FROM pivot_sidurim_meyuchadim s
      where s.mispar_sidur= p_mispar_sidur
      and p_taarich between S.ME_TARICH and S.AD_TARICH;
     EXCEPTION
                     WHEN no_data_found THEN
                            return 'false';
      end;    
 
  if (meafyen54>0) then
   return 'true';
   else return 'false';
   end if;
end IsSidurShaonim;


FUNCTION fun_hachtama_bemakom_haasaka(p_mispar_ishi in number,p_taarich Date, p_mispar_sidur in number,p_mikum_hachatama number) return nvarchar2 is
p_mikum_yechida number;
p_mikum_av_shaon number;
p_mikum_av_yechida number;
p_meafyen_80 number;
p_mikum_av_meafyen_80 number;
p_meafyen_61 number;
p_taarich_tokef date; 
begin
        select to_date(erech_param,'dd/mm/yyyy')   into p_taarich_tokef from tb_parametrim where kod_param=272;
        if (p_taarich>=p_taarich_tokef) then
           p_meafyen_61:= nvl(pkg_ovdim.fun_get_meafyen_oved(p_mispar_ishi,61,p_taarich),0);
           if (p_meafyen_61=0 and pkg_errors.IsSidurShaonim(p_mispar_sidur,p_taarich) ='true' and p_mispar_sidur like '99%' and p_mispar_sidur<>99200 and p_mispar_sidur <>99214 and p_mikum_hachatama>0) then
                    begin
                           select nvl(P.MIKUM_YECHIDA  ,0) into p_mikum_yechida
                           from pivot_pirtey_ovdim p
                           where P.MISPAR_ISHI= p_mispar_ishi
                           and p_taarich between P.ME_TARICH and P.AD_TARICH;
                         EXCEPTION
                     WHEN no_data_found THEN
                            p_mikum_yechida:= 0;
                    end;        
                      --   DBMS_OUTPUT.PUT_LINE('111' || p_mikum_hachatama );
                    if( p_mikum_yechida=p_mikum_hachatama and p_mikum_yechida<>0 and p_mikum_hachatama<>0)then
                        return 'true';
                    end if;
                            --        DBMS_OUTPUT.PUT_LINE('222' || p_mikum_hachatama );
                    p_mikum_av_shaon:=pkg_errors.fun_get_YECHIDA_AV_LENOCH(p_mikum_hachatama);
                    p_mikum_av_yechida:=pkg_errors.fun_get_YECHIDA_AV_LENOCH(p_mikum_yechida);
                         --        DBMS_OUTPUT.PUT_LINE('333' || p_mikum_hachatama );
                    if (p_mikum_av_shaon=p_mikum_av_yechida and p_mikum_av_shaon<>0 and p_mikum_av_yechida<>0) then
                        return 'true';
                    end if;
                   --   DBMS_OUTPUT.PUT_LINE('444' || p_mikum_hachatama );
                     begin
                           select to_number(nvl(M.ERECH_ISHI  , M.ERECH_MECHDAL_PARTANY  )) into p_meafyen_80
                           from MEAFYENIM_OVDIM M
                           where M.MISPAR_ISHI = p_mispar_ishi
                               and p_taarich between M.ME_TAARICH and M.AD_TAARICH
                               and M.KOD_MEAFYEN=80;
                           --      DBMS_OUTPUT.PUT_LINE('555' || p_mikum_hachatama );
                                 if (p_meafyen_80=p_mikum_hachatama) then 
                                 return 'true';
                           
                                 end if;
                               
                                        --  DBMS_OUTPUT.PUT_LINE('666' || p_mikum_hachatama );
                               p_mikum_av_meafyen_80:=pkg_errors.fun_get_YECHIDA_AV_LENOCH(p_meafyen_80);
                               if(p_mikum_yechida<>p_mikum_hachatama and p_mikum_av_shaon= p_mikum_av_meafyen_80) then
                                  return 'true';
                               end if;
                         EXCEPTION
                     WHEN no_data_found THEN
                            return 'false';
                    end;    

                return 'false';
            end if;
            end if;
        return 'true';
end fun_hachtama_bemakom_haasaka;
FUNCTION fun_get_YECHIDA_AV_LENOCH(kod_yechida  in number ) return number is
kod number;
begin

        select nvl(Y.KOD_MIKUM_YECHIDA_AV_LENOCH,0) into kod
        from ctb_mikum_yechida y
        where Y.KOD_MIKUM_YECHIDA= kod_yechida;
        
         return kod;
       EXCEPTION
         WHEN no_data_found THEN
                return 0;
        WHEN OTHERS THEN
        RAISE; 
end fun_get_YECHIDA_AV_LENOCH;
PROCEDURE pro_del_oved_errors(p_mispar_ishi IN TB_SHGIOT.mispar_ishi%TYPE, p_date IN TB_SHGIOT.taarich%TYPE)
IS
BEGIN
    DELETE FROM TB_SHGIOT s
    WHERE s.mispar_ishi=p_mispar_ishi
          AND s.taarich= p_date;

    EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END pro_del_oved_errors;
PROCEDURE pro_get_lookup_tables(p_cur OUT CurType)
IS
BEGIN
DBMS_APPLICATION_INFO.SET_MODULE('pkg_errors.pro_get_lookup_tables','get lookup tables');
    OPEN p_cur FOR
    SELECT kod_lina kod, teur_lina teur, 'ctb_lina' table_name FROM CTB_LINA
    WHERE pail=1
    UNION ALL
    SELECT kod_divuch kod , teur_divuch teur, 'ctb_divuch_hariga_meshaot' table_name  FROM CTB_DIVUCH_HARIGA_MESHAOT
    UNION ALL
    SELECT kod_pizul_hafsaka kod,teur_pizul_hafsaka teur ,'ctb_pitzul_hafsaka' table_name FROM CTB_PITZUL_HAFSAKA
    WHERE pail=1
    UNION ALL
    SELECT kod_zman_halbasha kod, teur_zman_halbasha teur,'ctb_zmaney_halbasha' table_name FROM CTB_ZMANEY_HALBASHA
    WHERE pail=1
    UNION ALL
    SELECT kod_zman_nesiaa kod, teur_zman_nesiaa teur,'ctb_zmaney_nesiaa' table_name FROM CTB_ZMANEY_NESIAA
    WHERE pail=1
    UNION ALL
    SELECT kod_yom_visa kod, teur_yom_visa teur,'ctb_yom_visa' FROM CTB_YOM_VISA
    UNION ALL
    SELECT kod_siba kod, teur_siba teur, 'ctb_sibot_hashlama_leyom' FROM CTB_SIBOT_HASHLAMA_LEYOM
    WHERE letzuga = 1 AND pail = 1
    UNION ALL
    SELECT kod_status_kartis kod , teur_status_kartis teur, 'ctb_status_kartis' FROM CTB_STATUS_KARTIS
    WHERE pail=1;
EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END pro_get_lookup_tables;



PROCEDURE pro_get_oved_yom_avoda_details(p_mispar_ishi IN TB_YAMEY_AVODA_OVDIM.mispar_ishi%TYPE,
                                         p_taarich IN TB_YAMEY_AVODA_OVDIM.taarich%TYPE, p_Cur OUT CurType)
IS
BEGIN
  DBMS_APPLICATION_INFO.SET_MODULE('pkg_errors.pro_get_oved_yom_avoda_details','get details yom avoda leoved');
	   OPEN p_cur FOR
      SELECT ymao.mispar_ishi,ymao.lina,
            ymao.hashlama_leyom, ymao.halbasha, v_pirty_oved.snif_av,
            ymao.bitul_zman_nesiot, ymao.tachograf,ymao.hamarat_shabat Hamara,
            ymao.zman_nesia_haloch, ymao.zman_nesia_hazor,ymao.status,ymao.status_tipul,
            ymao.sibat_hashlama_leyom,ymao.measher_o_mistayeg,
            v_pirty_oved.maamad, v_pirty_oved.isuk,
            v_pirty_oved.mutaam, v_pirty_oved.mercaz_erua,
            v_pirty_oved.dakot_mutamut,v_pirty_oved.gil,
            v_pirty_oved.rishyon_autobus, v_pirty_oved.shlilat_rishayon,
            ov.kod_hevra,sug_yom.teur_yom,i.KOD_SECTOR_ISUK  ,
            sug_yom.erev_shishi_chag,sug_yom.shbaton, TO_CHAR(ymao.taarich,'d') iDay,
            NVL(status_card.teur_status_kartis,'שגוי')  teur_status_kartis,
            DECODE(s.snif_tnua,NULL,v_pirty_oved.hatzava_lekav,s.snif_tnua) snif_tnua,
            BECHISHUV_SACHAR,
              BECHISHUV_SACHAR,v_pirty_oved.me_tarich  , v_pirty_oved.ad_tarich,
              v_pirty_oved.mikum_yechida, 
              nvl(k.KOD_MIKUM_YECHIDA_AV_LENOCH   ,0) KOD_MIKUM_YECHIDA_AV_LENOCH

     FROM TB_YAMEY_AVODA_OVDIM ymao,
          pivot_pirtey_ovdim v_pirty_oved,
          OVDIM ov,
          TB_YAMIM_MEYUCHADIM ym,
          CTB_SUGEY_YAMIM_MEYUCHADIM sug_yom,
          CTB_STATUS_KARTIS status_card,
          CTB_ISUK i,
          CTB_SNIF_AV s,
          ctb_mikum_yechida k
     WHERE ymao.mispar_ishi             = p_mispar_ishi
        AND ymao.mispar_ishi            = ov.mispar_ishi
        AND ymao.taarich                = p_taarich
        AND ymao.taarich                = ym.taarich(+)
        AND ym.sug_yom                  = sug_yom.sug_yom(+)
        AND v_pirty_oved.mispar_ishi(+) = ymao.mispar_ishi
        AND p_taarich BETWEEN v_pirty_oved.me_tarich(+) AND v_pirty_oved.ad_tarich(+)
        AND status_card.kod_status_kartis(+) = ymao.status
       AND (ov.kod_hevra= i.Kod_Hevra OR  i.Kod_Hevra IS NULL)
       AND v_pirty_oved.isuk= i.Kod_Isuk(+)
       AND (s.Kod_Hevra= ov.kod_hevra OR  s.Kod_Hevra IS NULL)
       AND s.kod_snif_av(+)=v_pirty_oved.snif_av
        and v_pirty_oved.MIKUM_YECHIDA = k.KOD_MIKUM_YECHIDA;

EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END pro_get_oved_yom_avoda_details;
/*FUNCTION fn_find_errors(mispar_ishi in pirtey_ovdim.mispar_ishi%type,
                         kartis_avoda_dt in date ) return integer
IS
 result integer;
 error_code integer;
BEGIN
    --The procedure finds error in employee card
    --If error found return 0,  else return 1


    --9 : Sidur not exists

    --15: Missing start hour or end hour
       result := fn_is_hour_missing(mispar_ishi, kartis_avoda_dt);
       if (result<>0) then
            error_code:=15;
           else
            error_code:=1;
       end if;

       --33: Execption error
       result := fn_excption_error(mispar_ishi, kartis_avoda_dt);

       return result;

EXCEPTION
        WHEN OTHERS THEN
		RAISE;

END fn_find_errors;


FUNCTION fn_excption_error(mispar_ishi in pirtey_ovdim.mispar_ishi%type,kartis_avoda_dt in date) return integer
IS
    v_exeption_error number;
BEGIN
    SELECT chariga INTO v_exeption_error
    FROM tb_sidurim_ovdim mm
    WHERE mm.mispar_ishi = mispar_ishi
          AND mm.taarich = kartis_avoda_dt;

    if (v_exeption_error in (0,1,3,6,7,8)) then
     return 1;
     else
      return 0; --error
    end if;


    EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END fn_excption_error;

FUNCTION fn_is_hour_missing(mispar_ishi in pirtey_ovdim.mispar_ishi%type,
                            kartis_avoda_dt in date) return integer
IS
    v_shat_hatchala date;
    v_shat_gmar date;
    result integer;
BEGIN
    --15: Missing start hour or end hour
    --return 0:ok 1:missing start hour, 2:missing end hour, 3: missing start and end hour

    SELECT shat_hatchala,shat_gmar INTO v_shat_hatchala,v_shat_gmar
    FROM tb_sidurim_ovdim mm
    WHERE mm.mispar_ishi = mispar_ishi
          AND mm.taarich = kartis_avoda_dt;


    IF  (v_shat_hatchala=NULL) THEN
        IF (v_shat_gmar=NULL) THEN
           result:=3;
          ELSE
           result:=1;
        END IF;
     ELSE
        IF (v_shat_gmar=Null) THEN
           result:=2;
          ELSE
           result:=0;
        END IF;
    END IF;

    RETURN result;

    EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END fn_is_hour_missing;*/

PROCEDURE  pro_get_oved_matzav(p_mispar_ishi IN MATZAV_OVDIM.mispar_ishi%TYPE,
                            p_taarich IN MATZAV_OVDIM.taarich_siyum%TYPE, p_cur OUT CurType)

IS

BEGIN
	  DBMS_APPLICATION_INFO.SET_MODULE('pkg_errors.pro_get_oved_matzav','get kod matzav leoved');
    OPEN p_Cur FOR
    SELECT m.kod_matzav,M.TAARICH_HATCHALA,M.TAARICH_SIYUM
    FROM MATZAV_OVDIM m
    WHERE m.MISPAR_ISHI=p_mispar_ishi
          AND TRUNC(p_taarich) BETWEEN TRUNC(m.TAARICH_HATCHALA) AND TRUNC(m.TAARICH_SIYUM);


END  pro_get_oved_matzav;
FUNCTION fn_is_oto_number_exists(p_oto_no IN TB_PEILUT_OVDIM.oto_no%TYPE, p_date DATE) RETURN NUMBER
IS
    v_count NUMBER;
BEGIN
 -- SELECT COUNT(bus_number)  INTO v_count
 --   FROM VCL_GENERAL_VEHICLE_VIEW@kds2maale
 --   WHERE bus_number=p_oto_no;

    --Code return 0 -valid    1- error    2- not found
   SELECT VCL_LOADER.CheckBusNumber@kds2maale(p_date,p_oto_no) INTO v_count FROM dual ;
  /* select count(*) INTO v_count 
   from VEHICLE_SPECIFICATIONS v
   where V.BUS_NUMBER=p_oto_no
   and v.*/
    RETURN  v_count;

    EXCEPTION
        WHEN OTHERS THEN
        v_count:=1;
		RAISE;
END fn_is_oto_number_exists;
FUNCTION fn_is_duplicate_shat_yetiza(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE, p_date IN DATE) RETURN NUMBER
IS
    v_count NUMBER;
BEGIN
    v_count:=0;
    SELECT COUNT(shat_yetzia) INTO v_count FROM
    (SELECT COUNT(po.shat_yetzia) shat_yetzia
    FROM TB_SIDURIM_OVDIM o,TB_PEILUT_OVDIM po
    WHERE o.mispar_ishi = po.mispar_ishi
              AND o.taarich = po.taarich
              AND o.shat_hatchala = po.shat_hatchala_sidur
              AND o.mispar_sidur= po.mispar_sidur(+)
              AND o.mispar_ishi=p_mispar_ishi
              AND o.taarich=p_date
              AND ((Pkg_Tnua.fn_get_makat_type(po.makat_nesia)<>5)
                   OR ((Pkg_Tnua.fn_get_makat_type(po.makat_nesia)=5) AND
                     (NOT EXISTS (SELECT kod_element
                                  FROM TB_MEAFYENEY_ELEMENTIM o1
                                  WHERE o1.kod_element=po.makat_nesia
                                       AND o1.kod_meafyen=9
                                       AND p_date BETWEEN o1.me_taarich AND o1.ad_taarich))
                   )
              )

    GROUP BY po.shat_yetzia
    HAVING COUNT(po.shat_yetzia) > 1);

    RETURN v_count;
EXCEPTION
        WHEN NO_DATA_FOUND THEN
          RETURN 0;
        WHEN OTHERS THEN
		RAISE;

END fn_is_duplicate_shat_yetiza;

FUNCTION fn_is_zman_nesia_define(p_merkaz_erua IN CTB_ZMAN_NSIAA_MISHTANE.merkaz_erua%TYPE,
                                 p_mikum_shaon_knisa IN CTB_ZMAN_NSIAA_MISHTANE.mikum_yaad%TYPE,
                                 p_mikum_shaon_yetiza IN CTB_ZMAN_NSIAA_MISHTANE.mikum_yaad%TYPE) RETURN NUMBER
IS
 v_count_knisa NUMBER;
 v_count_yetiza NUMBER;
 v_res NUMBER;
BEGIN
    --return 0 if the two knisa and yetiza defines
    --return 3 if both, knisa and tetiza not define
    --return 1 if knisa not define
    --return 2 if yetiza not define
    IF (p_mikum_shaon_knisa=0) THEN
        v_count_knisa:=1;
    ELSE
        SELECT COUNT(merkaz_erua) INTO v_count_knisa
        FROM CTB_ZMAN_NSIAA_MISHTANE c
        WHERE c.merkaz_erua =p_merkaz_erua
              AND c.mikum_yaad=p_mikum_shaon_knisa;
    END IF;

    IF (p_mikum_shaon_yetiza=0) THEN
        v_count_yetiza:=1;
    ELSE
        SELECT COUNT(merkaz_erua) INTO v_count_yetiza
        FROM CTB_ZMAN_NSIAA_MISHTANE c
        WHERE c.merkaz_erua =p_merkaz_erua
              AND c.mikum_yaad=p_mikum_shaon_yetiza;
    END IF;

     IF (v_count_knisa=0) THEN
        IF (v_count_yetiza=0) THEN
            v_res:=3;
         ELSE
            v_res:=1;
        END IF;
       ELSE
         IF (v_count_yetiza=0) THEN
            v_res:=2;
           ELSE
            v_res:=0;
         END IF;
     END IF;

     RETURN v_res;
EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END fn_is_zman_nesia_define;
PROCEDURE pro_get_sug_sidur_meafyenim(p_cur OUT CurType)
IS
BEGIN
    OPEN p_cur FOR
    SELECT sug_sidur,sug_avoda,asur_ledaveach_mispar_rechev,
           sector_avoda,zakay_leaman_nesia,zakay_lehamara,zakay_lehashlama_avur_sidur,
           lo_letashlum_automati,kizuz_al_pi_hatchala_gmar,shaon_nochachut,
           me_tarich, ad_tarich,rashai_ledaveach,
		   zakay_lepizul, lo_nidreshet_hityazvut,S.TEUR_SIDUR_AVODA,ZAKAY_MICHUTZ_LAMICHSA
    FROM pivot_meafyeney_sug_sidur, ctb_sug_sidur s
    WHERE pivot_meafyeney_sug_sidur.sug_sidur =s.KOD_SIDUR_AVODA(+) ;

END pro_get_sug_sidur_meafyenim;

PROCEDURE pro_get_oved_yom_avoda_UDT(p_mispar_ishi IN TB_YAMEY_AVODA_OVDIM.mispar_ishi%TYPE, p_taarich IN TB_YAMEY_AVODA_OVDIM.taarich%TYPE,
                                     p_coll_meafeney_ovdim OUT coll_meafyeney_oved)
IS
BEGIN

     SELECT obj_meafyeney_oved
            (ymao.mispar_ishi,ymao.lina,ymao.hashlama_leyom ,ymao.halbasha, ymao.bitul_zman_nesiot,
            v_pirty_oved.maamad,v_pirty_oved.isuk, ov.kod_hevra)--, ymao.tachograf,


     BULK COLLECT INTO p_coll_meafeney_ovdim
     FROM TB_YAMEY_AVODA_OVDIM ymao,viw_meafyenim_ovdim v_oved_meafynim,
          pivot_pirtey_ovdim v_pirty_oved, OVDIM ov
     WHERE ymao.mispar_ishi=p_mispar_ishi
        AND ymao.mispar_ishi = ov.mispar_ishi
        AND ymao.taarich=p_taarich
        AND ymao.mispar_ishi=v_oved_meafynim.mispar_ishi(+)
        AND p_taarich > v_oved_meafynim.me_taarich(+)-- and v_oved_meafynim.ad_taarich(+)
        AND v_pirty_oved.mispar_ishi(+)=ymao.mispar_ishi
        AND p_taarich BETWEEN v_pirty_oved.me_tarich(+) AND v_pirty_oved.ad_tarich(+);

END pro_get_oved_yom_avoda_UDT;

PROCEDURE pro_ins_sidurim_ovdim(p_coll_sidurim_ovdim IN COLL_SIDURIM_OVDIM) IS
    iMinutes NUMBER;
    iCount NUMBER;
    bFound BOOLEAN;
    dShatHatchala DATE;
BEGIN
     
     
      IF (p_coll_sidurim_ovdim IS NOT NULL) THEN
          FOR i IN  1..p_coll_sidurim_ovdim.COUNT LOOP
        --  if (p_coll_sidurim_ovdim(i).update_object <> 9) then
             dShatHatchala:=p_coll_sidurim_ovdim(i).shat_hatchala;
             
             
             IF (TO_CHAR(dShatHatchala, 'YYYY')='0001') THEN
                  iMinutes := 1;
                  iCount := 0; 
                  bFound := FALSE;
                  WHILE NOT bFound LOOP
                    SELECT COUNT(mispar_sidur) INTO iCount
                    FROM TB_SIDURIM_OVDIM a 
                    WHERE  a.mispar_ishi = p_coll_sidurim_ovdim(i).mispar_ishi
                           AND a.taarich = p_coll_sidurim_ovdim(i).taarich
                           AND a.mispar_sidur = p_coll_sidurim_ovdim(i).mispar_sidur
                           AND a.shat_hatchala = dShatHatchala;
                           
                    IF (iCount > 0) THEN 
                        iMinutes := iMinutes + 1;
                        dShatHatchala := TO_DATE('01/01/0001 00:0' || iMinutes ,'dd/mm/yyyy HH24:MI') ;
                       ELSE
                           bFound:=TRUE;   
                    END IF;        
                  END LOOP;                    
              END IF;
              
              
              
              INSERT INTO TB_SIDURIM_OVDIM
                          (mispar_ishi                 ,
                           mispar_sidur                ,
                           taarich                     ,
                           shat_hatchala               ,
                           shat_gmar                   ,
                           meadken_acharon             ,
                           shat_hatchala_letashlum     ,
                           shat_gmar_letashlum         ,
                           pitzul_hafsaka              ,
                           chariga                     ,
                           tosefet_grira               ,
						   bitul_o_hosafa,
                      --     hamarat_shabat              ,
                           hashlama                    ,
                           yom_visa                    ,
                           lo_letashlum                ,
                           out_michsa                  ,
                           mikum_shaon_knisa           ,
                           mikum_shaon_yetzia          ,
                           --km_visa_lepremia          ,
                           achuz_knas_lepremyat_visa   ,
                           achuz_viza_besikun          ,
                           mispar_musach_o_machsan     ,
                           tafkid_visa                 ,
                           mivtza_visa                 ,
                           kod_siba_lo_letashlum       ,
                           kod_siba_ledivuch_yadani_in ,
                           kod_siba_ledivuch_yadani_out,
                           shayah_leyom_kodem          ,
                           taarich_idkun_acharon       ,
                           heara                       ,
                           mispar_shiurey_nehiga       ,
                           mezake_halbasha             ,
                           mezake_nesiot               ,
                           sug_hazmanat_visa           ,
                           shat_hitiatzvut,
						   sector_visa,
						   ptor_mehitiatzvut ,
						   nidreshet_hitiatzvut,
						   hachtama_beatar_lo_takin,
						   sug_hashlama,
                           sug_sidur,
                           menahel_musach_meadken
                          )
              VALUES (p_coll_sidurim_ovdim(i).mispar_ishi,p_coll_sidurim_ovdim(i).mispar_sidur,
                      p_coll_sidurim_ovdim(i).taarich, dShatHatchala,
                      p_coll_sidurim_ovdim(i).shat_gmar,-2,
                      p_coll_sidurim_ovdim(i).shat_hatchala_letashlum,p_coll_sidurim_ovdim(i).shat_gmar_letashlum,
                      p_coll_sidurim_ovdim(i).pitzul_hafsaka,p_coll_sidurim_ovdim(i).chariga,
                      p_coll_sidurim_ovdim(i).tosefet_grira,    p_coll_sidurim_ovdim(i).bitul_o_hosafa,--p_coll_sidurim_ovdim(i).hamarat_shabat,
                      p_coll_sidurim_ovdim(i).hashlama,p_coll_sidurim_ovdim(i).yom_visa,
                      p_coll_sidurim_ovdim(i).lo_letashlum,p_coll_sidurim_ovdim(i).out_michsa,
                      p_coll_sidurim_ovdim(i).mikum_shaon_knisa,p_coll_sidurim_ovdim(i).mikum_shaon_yetzia,
                      p_coll_sidurim_ovdim(i).achuz_knas_lepremyat_visa,
                      p_coll_sidurim_ovdim(i).achuz_viza_besikun,
                      p_coll_sidurim_ovdim(i).mispar_musach_o_machsan,
                      p_coll_sidurim_ovdim(i).tafkid_visa,             
                      p_coll_sidurim_ovdim(i).mivtza_visa, 
                      p_coll_sidurim_ovdim(i).kod_siba_lo_letashlum,
                      p_coll_sidurim_ovdim(i).kod_siba_ledivuch_yadani_in,p_coll_sidurim_ovdim(i).kod_siba_ledivuch_yadani_out,
                      p_coll_sidurim_ovdim(i).shayah_leyom_kodem,SYSDATE,
                      p_coll_sidurim_ovdim(i).heara,p_coll_sidurim_ovdim(i).mispar_shiurey_nehiga,
                      p_coll_sidurim_ovdim(i).mezake_halbasha,p_coll_sidurim_ovdim(i).mezake_nesiot,
                      p_coll_sidurim_ovdim(i).sug_hazmanat_visa,
                      p_coll_sidurim_ovdim(i).shat_hitiatzvut,
					  p_coll_sidurim_ovdim(i).sector_visa,
					   p_coll_sidurim_ovdim(i).ptor_mehitiatzvut ,
					  p_coll_sidurim_ovdim(i).nidreshet_hitiatzvut,
					  p_coll_sidurim_ovdim(i).hachtama_beatar_lo_takin,
					  p_coll_sidurim_ovdim(i).sug_hashlama,
                      p_coll_sidurim_ovdim(i).sug_sidur,
                      p_coll_sidurim_ovdim(i).menahel_musach_meadken);
                      
                      if ((p_coll_sidurim_ovdim(i).mispar_ishi <> p_coll_sidurim_ovdim(i).meadken_acharon) and (p_coll_sidurim_ovdim(i).meadken_acharon > 0) ) then
                          pkg_utils.pro_insert_meadken_acharon(p_coll_sidurim_ovdim(i).mispar_ishi,p_coll_sidurim_ovdim(i).taarich );
                      end if;
                 --        End if;
          END LOOP;
      
       END IF;
      EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;
END pro_ins_sidurim_ovdim;

PROCEDURE pro_upd_sidurim_ovdim(p_coll_sidurim_ovdim IN COLL_SIDURIM_OVDIM) IS
BEGIN

    IF (p_coll_sidurim_ovdim IS NOT NULL) THEN
        FOR i IN 1..p_coll_sidurim_ovdim.COUNT LOOP
            IF (p_coll_sidurim_ovdim(i).update_object=1) THEN               
                pro_ins_sidurim_ovdim_trail(p_coll_sidurim_ovdim(i),3);
                pro_upd_sidur_oved(p_coll_sidurim_ovdim(i));
            END IF;
          END LOOP;
    END IF;
      EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;
END pro_upd_sidurim_ovdim;
PROCEDURE pro_del_sidurim_ovdim(p_coll_sidurim_ovdim IN coll_sidurim_ovdim,p_type_update IN  NUMBER) IS
BEGIN
    IF (p_coll_sidurim_ovdim IS NOT NULL) THEN
        FOR i IN 1..p_coll_sidurim_ovdim.COUNT LOOP           
               IF (p_coll_sidurim_ovdim(i).update_object=p_type_update) THEN
                    pro_ins_sidurim_ovdim_trail(p_coll_sidurim_ovdim(i),2);
                    pro_del_sidur_oved(p_coll_sidurim_ovdim(i));
               END IF;           
        END LOOP;
    END IF;
      EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;
END pro_del_sidurim_ovdim;
PROCEDURE pro_upd_yamey_avoda_ovdim(p_coll_yamey_avoda_ovdim IN coll_yamey_avoda_ovdim) IS
BEGIN
      IF (p_coll_yamey_avoda_ovdim IS NOT NULL) THEN
          FOR i IN 1..p_coll_yamey_avoda_ovdim.COUNT LOOP
              IF (p_coll_yamey_avoda_ovdim(i).update_object=1) THEN
                  pro_ins_yom_avoda_oved_trail(p_coll_yamey_avoda_ovdim(i));
                  pro_upd_yom_avoda_oved(p_coll_yamey_avoda_ovdim(i));
              END IF;
          END LOOP;
      END IF;
      EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;
END pro_upd_yamey_avoda_ovdim;

PROCEDURE pro_upd_peilut_ovdim(p_coll_obj_peilut_ovdim IN coll_obj_peilut_ovdim) IS
BEGIN

     IF (p_coll_obj_peilut_ovdim IS NOT NULL) THEN
         FOR i IN 1..p_coll_obj_peilut_ovdim.COUNT LOOP
             IF (p_coll_obj_peilut_ovdim(i).update_object=1) THEN
                 pro_ins_peilut_ovdim_trail(p_coll_obj_peilut_ovdim(i),3);
                 pro_upd_peilut_oved(p_coll_obj_peilut_ovdim(i));
             END IF;

          END LOOP;
      END IF;

      EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;
END pro_upd_peilut_ovdim;

PROCEDURE pro_ins_peilut_ovdim(p_coll_obj_peilut_ovdim IN coll_obj_peilut_ovdim) IS
    iMinutes NUMBER;
    iCount NUMBER;
    bFound BOOLEAN;
    dShatYetiza DATE;
BEGIN
      IF (p_coll_obj_peilut_ovdim IS NOT NULL) THEN
          FOR i IN 1..p_coll_obj_peilut_ovdim.COUNT LOOP
          
            dShatYetiza:=p_coll_obj_peilut_ovdim(i).shat_yetzia;
            IF (TO_CHAR(dShatYetiza, 'YYYY')='0001') THEN
                  iMinutes := 1;
                  iCount := 0; 
                  bFound := FALSE;
                  WHILE NOT bFound LOOP
                    SELECT COUNT(mispar_sidur) INTO iCount
                    FROM TB_PEILUT_OVDIM a 
                    WHERE  a.mispar_ishi = p_coll_obj_peilut_ovdim(i).mispar_ishi
                           AND a.taarich = p_coll_obj_peilut_ovdim(i).taarich
                           AND a.mispar_sidur = p_coll_obj_peilut_ovdim(i).mispar_sidur
                           AND a.shat_hatchala_sidur = p_coll_obj_peilut_ovdim(i).shat_hatchala_sidur
                           AND a.mispar_knisa = p_coll_obj_peilut_ovdim(i).mispar_knisa
                           AND a.shat_yetzia = dShatYetiza;
                           
                    IF (iCount > 0) THEN 
                        iMinutes := iMinutes + 1;
                        dShatYetiza := TO_DATE('01/01/0001 00:0' || iMinutes ,'dd/mm/yyyy HH24:MI') ;
                       ELSE
                           bFound:=TRUE;   
                    END IF;        
                  END LOOP;                    
            END IF;
              
            INSERT INTO TB_PEILUT_OVDIM
                        (mispar_ishi,
                         mispar_sidur,
                         taarich,
                         shat_hatchala_sidur,
                         shat_yetzia,
                         mispar_knisa,
                         makat_nesia,
                         oto_no,
                         mispar_siduri_oto,
                         kisuy_tor,
                         bitul_o_hosafa,
                         kod_shinuy_premia,
                         snif_tnua,
                         mispar_visa,
                         imut_netzer,
                         shat_bhirat_nesia_netzer,
                         oto_no_netzer,
                         mispar_sidur_netzer,
                         shat_yetzia_netzer,
                         makat_netzer,
                         shilut_netzer,
                         mikum_bhirat_nesia_netzer,
                         mispar_matala,
						 dakot_bafoal,
                         taarich_idkun_acharon,
                         meadken_acharon,
                         heara,
						 teur_nesia)
                         --ishur_kfula)
             VALUES (p_coll_obj_peilut_ovdim(i).mispar_ishi,
                     p_coll_obj_peilut_ovdim(i).mispar_sidur,
                     p_coll_obj_peilut_ovdim(i).taarich,
                     p_coll_obj_peilut_ovdim(i).shat_hatchala_sidur,
                     dShatYetiza,--p_coll_obj_peilut_ovdim(i).shat_yetzia,
                     p_coll_obj_peilut_ovdim(i).mispar_knisa,
                     p_coll_obj_peilut_ovdim(i).makat_nesia,
                     p_coll_obj_peilut_ovdim(i).oto_no,
                     p_coll_obj_peilut_ovdim(i).mispar_siduri_oto,
                     p_coll_obj_peilut_ovdim(i).kisuy_tor,
                     p_coll_obj_peilut_ovdim(i).bitul_o_hosafa,
                     p_coll_obj_peilut_ovdim(i).kod_shinuy_premia,
                     p_coll_obj_peilut_ovdim(i).snif_tnua,
                     p_coll_obj_peilut_ovdim(i).mispar_visa,
                     p_coll_obj_peilut_ovdim(i).imut_netzer,
                     p_coll_obj_peilut_ovdim(i).shat_bhirat_nesia_netzer,
                     p_coll_obj_peilut_ovdim(i).oto_no_netzer,
                     p_coll_obj_peilut_ovdim(i).mispar_sidur_netzer,
                     p_coll_obj_peilut_ovdim(i).shat_yetzia_netzer,
                     p_coll_obj_peilut_ovdim(i).makat_netzer,
                     p_coll_obj_peilut_ovdim(i).shilut_netzer,
                     p_coll_obj_peilut_ovdim(i).mikum_bhirat_nesia_netzer,
			         p_coll_obj_peilut_ovdim(i).mispar_matala,
					 p_coll_obj_peilut_ovdim(i).dakot_bafoal,
                     SYSDATE,
                     p_coll_obj_peilut_ovdim(i).meadken_acharon,
                     p_coll_obj_peilut_ovdim(i).heara,
					 p_coll_obj_peilut_ovdim(i).teur_nesia
					  );
                    -- p_coll_obj_peilut_ovdim(i).ishur_kfula);
                      if ((p_coll_obj_peilut_ovdim(i).mispar_ishi <> p_coll_obj_peilut_ovdim(i).meadken_acharon) and (p_coll_obj_peilut_ovdim(i).meadken_acharon > 0) ) then
                          pkg_utils.pro_insert_meadken_acharon(p_coll_obj_peilut_ovdim(i).mispar_ishi,p_coll_obj_peilut_ovdim(i).taarich );
                     end if;
          END LOOP;
      END IF;
      EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;
END pro_ins_peilut_ovdim;

PROCEDURE pro_del_peilut_ovdim(p_coll_obj_peilut_ovdim IN coll_obj_peilut_ovdim) IS
BEGIN
      IF (p_coll_obj_peilut_ovdim IS NOT NULL) THEN
            FOR i IN 1..p_coll_obj_peilut_ovdim.COUNT LOOP
               pro_ins_peilut_ovdim_trail(p_coll_obj_peilut_ovdim(i),2);
                pro_del_peilut_oved(p_coll_obj_peilut_ovdim(i));
           END LOOP;
      END IF;
      EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;
END pro_del_peilut_ovdim;

PROCEDURE pro_upd_sidur_oved(p_obj_sidurim_ovdim IN obj_sidurim_ovdim) IS
BEGIN
    --Insert sidurim
    UPDATE TB_SIDURIM_OVDIM
    SET lo_letashlum                 = p_obj_sidurim_ovdim.lo_letashlum,
        hashlama                     = p_obj_sidurim_ovdim.Hashlama,
    --    hamarat_shabat               = p_obj_sidurim_ovdim.hamarat_shabat,
        shat_hatchala                = p_obj_sidurim_ovdim.new_shat_hatchala,
        shat_gmar                    = p_obj_sidurim_ovdim.shat_gmar,
        mispar_ishi                  = p_obj_sidurim_ovdim.mispar_ishi,
        mispar_sidur                 = p_obj_sidurim_ovdim.new_mispar_sidur,
        chariga                      = p_obj_sidurim_ovdim.chariga,
        out_michsa                   = p_obj_sidurim_ovdim.out_michsa,
        shat_hatchala_letashlum      = p_obj_sidurim_ovdim.shat_hatchala_letashlum,
        shat_gmar_letashlum          = p_obj_sidurim_ovdim.shat_gmar_letashlum,
        mezake_nesiot                = p_obj_sidurim_ovdim.mezake_nesiot,
        mezake_halbasha              = p_obj_sidurim_ovdim.mezake_halbasha,
        taarich_idkun_acharon        = SYSDATE,
        meadken_acharon              = p_obj_sidurim_ovdim.meadken_acharon,
        kod_siba_ledivuch_yadani_out = p_obj_sidurim_ovdim.kod_siba_ledivuch_yadani_out,
        kod_siba_ledivuch_yadani_in  = p_obj_sidurim_ovdim.kod_siba_ledivuch_yadani_in,
        pitzul_hafsaka               = p_obj_sidurim_ovdim.pitzul_hafsaka,
        kod_siba_lo_letashlum        = p_obj_sidurim_ovdim.kod_siba_lo_letashlum,
        bitul_o_hosafa               = p_obj_sidurim_ovdim.bitul_o_hosafa,
        shat_hitiatzvut              = p_obj_sidurim_ovdim.shat_hitiatzvut,
		ptor_mehitiatzvut    = p_obj_sidurim_ovdim.ptor_mehitiatzvut,
		 nidreshet_hitiatzvut   = p_obj_sidurim_ovdim.nidreshet_hitiatzvut,
         hachtama_beatar_lo_takin= p_obj_sidurim_ovdim. hachtama_beatar_lo_takin,
		 sug_hashlama=p_obj_sidurim_ovdim.sug_hashlama
        -- sug_sidur =p_obj_sidurim_ovdim.sug_sidur
    WHERE mispar_ishi  = p_obj_sidurim_ovdim.mispar_ishi AND
          mispar_sidur = p_obj_sidurim_ovdim.new_mispar_sidur AND
          taarich      = TRUNC(p_obj_sidurim_ovdim.taarich) AND
          shat_hatchala= p_obj_sidurim_ovdim.new_shat_hatchala;
          
       
       if ((p_obj_sidurim_ovdim.mispar_ishi <> p_obj_sidurim_ovdim.meadken_acharon) and (p_obj_sidurim_ovdim.meadken_acharon > 0) ) then
            pkg_utils.pro_insert_meadken_acharon(p_obj_sidurim_ovdim.mispar_ishi,p_obj_sidurim_ovdim.taarich );
       end if;
EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;
END pro_upd_sidur_oved;
PROCEDURE pro_del_sidur_oved(p_obj_sidurim_ovdim IN obj_sidurim_ovdim) IS
BEGIN
      DELETE TB_SIDURIM_OVDIM
      WHERE    mispar_ishi         = p_obj_sidurim_ovdim.mispar_ishi AND
               taarich             = TRUNC(p_obj_sidurim_ovdim.taarich) AND
               mispar_sidur        = p_obj_sidurim_ovdim.mispar_sidur AND
               shat_hatchala       = p_obj_sidurim_ovdim.shat_hatchala;
               
       if ((p_obj_sidurim_ovdim.mispar_ishi <> p_obj_sidurim_ovdim.meadken_acharon) and (p_obj_sidurim_ovdim.meadken_acharon > 0) ) then
            pkg_utils.pro_insert_meadken_acharon(p_obj_sidurim_ovdim.mispar_ishi,p_obj_sidurim_ovdim.taarich );
       end if;        

EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;
END pro_del_sidur_oved;
PROCEDURE pro_ins_sidurim_ovdim_trail(p_obj_sidurim_ovdim IN obj_sidurim_ovdim,p_sug_peula IN TRAIL_SIDURIM_OVDIM.sug_peula%TYPE) IS
BEGIN
    INSERT INTO TRAIL_SIDURIM_OVDIM (mispar_ishi,mispar_sidur,taarich,
                                    shat_hatchala,shat_gmar,shat_hatchala_letashlum,shat_gmar_letashlum,
                                    pitzul_hafsaka,chariga,tosefet_grira,hashlama,yom_visa,
                                    lo_letashlum,out_michsa,mikum_shaon_knisa,mikum_shaon_yetzia,
                                    achuz_knas_lepremyat_visa,achuz_viza_besikun,mispar_musach_o_machsan,
                                    kod_siba_lo_letashlum,kod_siba_ledivuch_yadani_in,kod_siba_ledivuch_yadani_out,
                                    meadken_acharon,taarich_idkun_acharon,heara,shayah_leyom_kodem,mispar_shiurey_nehiga,
                                    mispar_ishi_trail,taarich_idkun_trail,sug_peula,mezake_halbasha,mezake_nesiot,sector_visa,shat_hitiatzvut,sug_hashlama,
									menahel_musach_meadken,sug_hazmanat_visa,tafkid_visa,mivtza_visa,nidreshet_hitiatzvut,
									ptor_mehitiatzvut,hachtama_beatar_lo_takin,hafhatat_nochechut_visa,bitul_o_hosafa   )
    SELECT mispar_ishi,mispar_sidur,taarich,
          shat_hatchala,shat_gmar,shat_hatchala_letashlum,shat_gmar_letashlum,
          pitzul_hafsaka,chariga,tosefet_grira,hashlama,yom_visa,
          lo_letashlum,out_michsa,mikum_shaon_knisa,mikum_shaon_yetzia,
          achuz_knas_lepremyat_visa,achuz_viza_besikun,mispar_musach_o_machsan,
          kod_siba_lo_letashlum,kod_siba_ledivuch_yadani_in,kod_siba_ledivuch_yadani_out,
          meadken_acharon,taarich_idkun_acharon,heara,shayah_leyom_kodem,mispar_shiurey_nehiga,
          p_obj_sidurim_ovdim.meadken_acharon ,SYSDATE,p_sug_peula, mezake_halbasha,mezake_nesiot,sector_visa,shat_hitiatzvut,sug_hashlama,
		  menahel_musach_meadken,sug_hazmanat_visa,tafkid_visa,mivtza_visa,nidreshet_hitiatzvut ,
		  ptor_mehitiatzvut,hachtama_beatar_lo_takin ,hafhatat_nochechut_visa,p_obj_sidurim_ovdim.bitul_o_hosafa
    FROM TB_SIDURIM_OVDIM
    WHERE mispar_ishi  = p_obj_sidurim_ovdim.mispar_ishi    AND
         mispar_sidur  = p_obj_sidurim_ovdim.mispar_sidur   AND
         taarich       = TRUNC(p_obj_sidurim_ovdim.taarich) AND
         shat_hatchala = p_obj_sidurim_ovdim.shat_hatchala;
         
         
        
EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;
END  pro_ins_sidurim_ovdim_trail;

PROCEDURE pro_upd_yom_avoda_oved(p_obj_yamey_avoda_ovdim IN obj_yamey_avoda_ovdim) IS
BEGIN
     UPDATE TB_YAMEY_AVODA_OVDIM
     SET tachograf             = p_obj_yamey_avoda_ovdim.tachograf,
         halbasha              = p_obj_yamey_avoda_ovdim.halbasha,
         lina                  = p_obj_yamey_avoda_ovdim.lina,
         hashlama_leyom        = p_obj_yamey_avoda_ovdim.hashlama_leyom,
         bitul_zman_nesiot     = p_obj_yamey_avoda_ovdim.bitul_zman_nesiot,
         meadken_acharon       = p_obj_yamey_avoda_ovdim.meadken_acharon,
         zman_nesia_haloch     = p_obj_yamey_avoda_ovdim.zman_nesia_haloch,
         zman_nesia_hazor      = p_obj_yamey_avoda_ovdim.zman_nesia_hazor,
         sibat_hashlama_leyom  = p_obj_yamey_avoda_ovdim.sibat_hashlama_leyom,
         hamarat_shabat        = p_obj_yamey_avoda_ovdim.hamarat_shabat,
         taarich_idkun_acharon = SYSDATE
     WHERE mispar_ishi     = p_obj_yamey_avoda_ovdim.mispar_ishi AND
           taarich         = TRUNC(p_obj_yamey_avoda_ovdim.taarich);
           
           
           
       if ((p_obj_yamey_avoda_ovdim.mispar_ishi <> p_obj_yamey_avoda_ovdim.meadken_acharon) and (p_obj_yamey_avoda_ovdim.meadken_acharon > 0) ) then
            pkg_utils.pro_insert_meadken_acharon(p_obj_yamey_avoda_ovdim.mispar_ishi,p_obj_yamey_avoda_ovdim.taarich );
       end if;   
EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;
END pro_upd_yom_avoda_oved;

PROCEDURE pro_ins_yom_avoda_oved_trail(p_obj_yamey_avoda_ovdim IN obj_yamey_avoda_ovdim) IS
BEGIN
    INSERT INTO TRAIL_YAMEY_AVODA_OVDIM(mispar_ishi,taarich,shat_hatchala,shat_siyum,
                                     tachograf,bitul_zman_nesiot,zman_nesia_haloch,
                                     zman_nesia_hazor,halbasha,lina,status,kod_histaygut_auto,
                                     measher_o_mistayeg,status_tipul,meadken_acharon,taarich_idkun_acharon,
                                     heara,hashlama_leyom,sibat_hashlama_leyom,mispar_ishi_trail,taarich_idkun_trail,
                                     sug_peula,ritzat_ishurim_acharona,shgiot_letezuga_laoved,
									 ritzat_shgiot_acharona,hamarat_shabat)
    SELECT mispar_ishi,taarich,shat_hatchala,shat_siyum,tachograf,bitul_zman_nesiot,
           zman_nesia_haloch,zman_nesia_hazor,halbasha,lina,status,kod_histaygut_auto,
           measher_o_mistayeg,status_tipul,meadken_acharon,
           taarich_idkun_acharon,heara,hashlama_leyom,sibat_hashlama_leyom,p_obj_yamey_avoda_ovdim.meadken_acharon,SYSDATE,3,
		   ritzat_ishurim_acharona,shgiot_letezuga_laoved,
									 ritzat_shgiot_acharona,hamarat_shabat
    FROM TB_YAMEY_AVODA_OVDIM
    WHERE mispar_ishi   = p_obj_yamey_avoda_ovdim.mispar_ishi AND
          taarich       = TRUNC(p_obj_yamey_avoda_ovdim.taarich);


   
EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;
END pro_ins_yom_avoda_oved_trail;


PROCEDURE pro_upd_peilut_oved(p_obj_peilut_ovdim IN obj_peilut_ovdim)
IS
BEGIN
     UPDATE TB_PEILUT_OVDIM
     SET mispar_matala           = p_obj_peilut_ovdim.mispar_matala,
         makat_nesia             = p_obj_peilut_ovdim.makat_nesia,
         mispar_sidur            = p_obj_peilut_ovdim.new_mispar_sidur,
		 shat_yetzia             = p_obj_peilut_ovdim.new_shat_yetzia,
		 shat_hatchala_sidur     = p_obj_peilut_ovdim.new_shat_hatchala_sidur,
         taarich_idkun_acharon   = SYSDATE,
         meadken_acharon         = p_obj_peilut_ovdim.meadken_acharon,
		  mispar_visa = p_obj_peilut_ovdim.mispar_visa,
		  bitul_o_hosafa=p_obj_peilut_ovdim. bitul_o_hosafa
     WHERE mispar_ishi           = p_obj_peilut_ovdim.mispar_ishi AND
           taarich               = p_obj_peilut_ovdim.taarich AND
           mispar_sidur          = p_obj_peilut_ovdim.mispar_sidur AND
           shat_hatchala_sidur   = p_obj_peilut_ovdim.shat_hatchala_sidur AND
           shat_yetzia           = p_obj_peilut_ovdim.shat_yetzia AND
           mispar_knisa          = p_obj_peilut_ovdim.mispar_knisa;

       if ((p_obj_peilut_ovdim.mispar_ishi <> p_obj_peilut_ovdim.meadken_acharon) and (p_obj_peilut_ovdim.meadken_acharon > 0) ) then
            pkg_utils.pro_insert_meadken_acharon(p_obj_peilut_ovdim.mispar_ishi,p_obj_peilut_ovdim.taarich );
       end if;   
    EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;
END pro_upd_peilut_oved;


PROCEDURE pro_ins_peilut_ovdim_trail(p_obj_peilut_ovdim IN obj_peilut_ovdim, p_sug_peula IN TRAIL_PEILUT_OVDIM.sug_peula%TYPE)
IS
BEGIN
    INSERT INTO TRAIL_PEILUT_OVDIM(mispar_ishi,taarich,mispar_sidur,
                                   shat_hatchala_sidur,shat_yetzia,mispar_knisa,
                                   makat_nesia,oto_no,mispar_siduri_oto,kisuy_tor,
                                   bitul_o_hosafa,kod_shinuy_premia,mispar_visa,imut_netzer,
                                   shat_bhirat_nesia_netzer,oto_no_netzer,mispar_sidur_netzer,
                                   shat_yetzia_netzer,makat_netzer,shilut_netzer,
                                   mikum_bhirat_nesia_netzer,mispar_matala,
                                   taarich_idkun_acharon,meadken_acharon,mispar_ishi_trail,
                                   taarich_idkun_trail,sug_peula,heara,snif_tnua,teur_nesia,dakot_bafoal ,km_visa,shat_yetzia_mekorit )
    SELECT mispar_ishi,taarich,mispar_sidur,
           shat_hatchala_sidur,shat_yetzia,mispar_knisa,
           makat_nesia,oto_no,mispar_siduri_oto,kisuy_tor,
           p_obj_peilut_ovdim.bitul_o_hosafa,kod_shinuy_premia,mispar_visa,imut_netzer,
           shat_bhirat_nesia_netzer,oto_no_netzer,mispar_sidur_netzer,
           shat_yetzia_netzer,makat_netzer,shilut_netzer,
           mikum_bhirat_nesia_netzer,mispar_matala,
           taarich_idkun_acharon,meadken_acharon,p_obj_peilut_ovdim.meadken_acharon,
		   SYSDATE, p_sug_peula,
           heara,snif_tnua,teur_nesia,dakot_bafoal ,km_visa,shat_yetzia_mekorit

    FROM TB_PEILUT_OVDIM
    WHERE mispar_ishi           = p_obj_peilut_ovdim.mispar_ishi         AND
          taarich               = TRUNC(p_obj_peilut_ovdim.taarich)      AND
          mispar_sidur          = p_obj_peilut_ovdim.mispar_sidur        AND
          shat_hatchala_sidur   = p_obj_peilut_ovdim.shat_hatchala_sidur AND
          shat_yetzia           = p_obj_peilut_ovdim.shat_yetzia         AND
          mispar_knisa          = p_obj_peilut_ovdim.mispar_knisa;


     
EXCEPTION
        --WHEN DUP_VAL_ON_INDEX THEN
        --    RETURN;
		 WHEN OTHERS THEN
		     RAISE_APPLICATION_ERROR(-20001,'An error was encountered in pro_ins_peilut_ovdim_trailm - '||SQLCODE||' -ERROR- '||SQLERRM);
END pro_ins_peilut_ovdim_trail;

PROCEDURE pro_del_peilut_oved(p_obj_peilut_ovdim IN obj_peilut_ovdim) IS
BEGIN
      DELETE TB_PEILUT_OVDIM
      WHERE    mispar_ishi         = p_obj_peilut_ovdim.mispar_ishi AND
               taarich             = TRUNC(p_obj_peilut_ovdim.taarich) AND
               mispar_sidur        = p_obj_peilut_ovdim.mispar_sidur AND
               shat_hatchala_sidur = p_obj_peilut_ovdim.shat_hatchala_sidur AND
               shat_yetzia         = p_obj_peilut_ovdim.shat_yetzia AND
               mispar_knisa        = p_obj_peilut_ovdim.mispar_knisa; 
           --    makat_nesia         = p_obj_peilut_ovdim.makat_nesia;
           
           
           
       if ((p_obj_peilut_ovdim.mispar_ishi <> p_obj_peilut_ovdim.meadken_acharon) and (p_obj_peilut_ovdim.meadken_acharon > 0) ) then
            pkg_utils.pro_insert_meadken_acharon(p_obj_peilut_ovdim.mispar_ishi,p_obj_peilut_ovdim.taarich );
       end if;   
END  pro_del_peilut_oved;
PROCEDURE pro_shinuy_kelet(p_coll_yamey_avoda_ovdim_upd IN coll_yamey_avoda_ovdim,
                           p_coll_sidurim_ovdim_upd     IN coll_sidurim_ovdim,
                           p_coll_sidurim_ovdim_ins     IN  coll_sidurim_ovdim,
                           p_coll_sidurim_ovdim_del     IN coll_sidurim_ovdim,
                           p_coll_obj_peilut_ovdim_upd  IN coll_obj_peilut_ovdim,
                           p_coll_obj_peilut_ovdim_ins  IN coll_obj_peilut_ovdim,
                           p_coll_obj_peilut_ovdim_del  IN coll_obj_peilut_ovdim) IS
BEGIN
DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'pkg_errors',action_name => 'pro_shinuy_kelet');
    --עדכון יום עבודה
    DBMS_APPLICATION_INFO.SET_ACTION( 'pro_upd_yamey_avoda_ovdim');
   BEGIN
        pro_upd_yamey_avoda_ovdim(p_coll_yamey_avoda_ovdim_upd);
   EXCEPTION
		 WHEN OTHERS THEN
              RAISE_APPLICATION_ERROR(-20001,'An error was encountered in tb_yamey_avoda_ovdim - '||SQLCODE||' -ERROR- '||SQLERRM);

   END;

DBMS_APPLICATION_INFO.SET_ACTION( 'pro_del_sidurim_ovdim');
--מחיקת סידורים
    BEGIN
      pro_del_sidurim_ovdim(p_coll_sidurim_ovdim_del,2);
      -- pro_del_sidurim_ovdim(p_coll_sidurim_ovdim_del,p_coll_sidurim_ovdim_ins,2);
      EXCEPTION
		 WHEN OTHERS THEN
           RAISE_APPLICATION_ERROR(-20001,'An error was encountered in delete to tb_sidurim_ovdim - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;
 DBMS_APPLICATION_INFO.SET_ACTION( 'pro_ins_sidurim_ovdim');
 --הכנסת סידורים חדשים
   BEGIN
        pro_ins_sidurim_ovdim(p_coll_sidurim_ovdim_ins);
      EXCEPTION
		 WHEN OTHERS THEN
		       RAISE_APPLICATION_ERROR(-20001,'An error was encountered in insert to tb_sidurim_ovdim - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;
DBMS_APPLICATION_INFO.SET_ACTION( 'pro_del_peilut_ovdim');
--מחיקת פעילויות
    BEGIN

        pro_del_peilut_ovdim(p_coll_obj_peilut_ovdim_del);
     EXCEPTION
		 WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20001,'An error was encountered in delete to tb_peiluyot_ovdim - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;
	DBMS_APPLICATION_INFO.SET_ACTION( 'pro_upd_peilut_ovdim');
	 --עדכון פעילויות
    BEGIN
        pro_upd_peilut_ovdim(p_coll_obj_peilut_ovdim_upd);
       EXCEPTION
		 WHEN OTHERS THEN
		      RAISE_APPLICATION_ERROR(-20001,'An error was encountered in update to tb_peiluyot_ovdim - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;
	
	 DBMS_APPLICATION_INFO.SET_ACTION( 'pro_upd_sidurim_ovdim');
    --עדכון סידורים
    BEGIN
        pro_upd_sidurim_ovdim(p_coll_sidurim_ovdim_upd);
      EXCEPTION
		 WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20001,'An error was encountered in update to tb_sidurim_ovdim - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;
    DBMS_APPLICATION_INFO.SET_ACTION( 'pro_del_sidurim_ovdim');
   --מחיקת סידורים
   BEGIN
        pro_del_sidurim_ovdim(p_coll_sidurim_ovdim_del,0);
      EXCEPTION
		 WHEN OTHERS THEN
           RAISE_APPLICATION_ERROR(-20001,'An error was encountered in delete to tb_sidurim_ovdim - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;
DBMS_APPLICATION_INFO.SET_ACTION( 'pro_ins_peilut_ovdim');
--הכנסת פעילויות
      BEGIN
        pro_ins_peilut_ovdim(p_coll_obj_peilut_ovdim_ins);
     EXCEPTION
		 WHEN OTHERS THEN
             RAISE_APPLICATION_ERROR(-20001,'An error was encountered in insert to tb_peiluyot_ovdim - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;  


END pro_shinuy_kelet;

PROCEDURE pro_oved_update_fields(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE, p_date IN DATE, p_cur OUT CurType)
IS
BEGIN
DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'pkg_errors',action_name => 'pro_oved_update_fields');
--Get maximun table/trail  last update
    OPEN p_cur FOR
    SELECT TRUNC(max_date) last_date, NVL(OVDIM.SHEM_MISH || ' ' ||  OVDIM.SHEM_PRAT ,'מערכת') full_name,MEADKEN_ACHARON
    FROM
        OVDIM,
        (
            SELECT  TAARICH_IDKUN_ACHARON ,MEADKEN_ACHARON,
                    MAX(TAARICH_IDKUN_ACHARON) OVER (ORDER BY TAARICH_IDKUN_ACHARON DESC) AS max_date
            FROM
            (
                SELECT yo.TAARICH_IDKUN_ACHARON, yo.MEADKEN_ACHARON
                FROM TB_YAMEY_AVODA_OVDIM yo
                WHERE yo.MISPAR_ISHI= p_mispar_ishi
                AND yo.TAARICH = p_date

                UNION

                SELECT TAARICH_IDKUN_ACHARON ,MEADKEN_ACHARON
                FROM (
                  SELECT so.TAARICH_IDKUN_ACHARON ,so.MEADKEN_ACHARON,
                  MAX(TAARICH_IDKUN_ACHARON) OVER (PARTITION BY mispar_ishi,TAARICH) AS rmax_date
                  FROM TB_SIDURIM_OVDIM so
                  WHERE so.MISPAR_ISHI= p_mispar_ishi
                        AND so.TAARICH = p_date)
                WHERE TAARICH_IDKUN_ACHARON = rmax_date

                UNION

                SELECT TAARICH_IDKUN_ACHARON ,MEADKEN_ACHARON
                FROM (
                  SELECT po.TAARICH_IDKUN_ACHARON ,po.MEADKEN_ACHARON,
                  MAX(TAARICH_IDKUN_ACHARON) OVER (PARTITION BY mispar_ishi,TAARICH) AS rmax_date
                  FROM TB_PEILUT_OVDIM po
                  WHERE po.MISPAR_ISHI= p_mispar_ishi
                        AND po.TAARICH = p_date)
                WHERE TAARICH_IDKUN_ACHARON = rmax_date


            )
        ) tbLastUpdate
    WHERE tbLastUpdate.meadken_acharon= OVDIM.MISPAR_ISHI(+);
        /*  union

      SELECT TAARICH_IDKUN_TRAIL ,MEADKEN_ACHARON
        FROM (
          SELECT a.TAARICH_IDKUN_TRAIL ,a.MEADKEN_ACHARON,
          MAX(TAARICH_IDKUN_TRAIL) OVER (PARTITION BY mispar_ishi,TAARICH) AS rmax_date
          FROM trail_yamey_avoda_ovdim a
          where a.MISPAR_ISHI= p_mispar_ishi
                and a.TAARICH = p_date)
        WHERE TAARICH_IDKUN_TRAIL = rmax_date


        union

        SELECT TAARICH_IDKUN_TRAIL ,MEADKEN_ACHARON
        FROM (
          SELECT a.TAARICH_IDKUN_TRAIL ,a.MEADKEN_ACHARON,
          MAX(TAARICH_IDKUN_TRAIL) OVER (PARTITION BY mispar_ishi,TAARICH) AS rmax_date
          FROM trail_sidurim_ovdim a
          where a.MISPAR_ISHI= p_mispar_ishi
                and a.TAARICH = p_date)
        WHERE TAARICH_IDKUN_TRAIL = rmax_date

        union

        SELECT FIRST_VALUE(TAARICH_IDKUN_TRAIL) OVER () as TAARICH_IDKUN_TRAIL
               ,MEADKEN_ACHARON
        FROM (
          SELECT (po.TAARICH_IDKUN_TRAIL) , po.MEADKEN_ACHARON, mispar_ishi,TAARICH,
          MAX(TAARICH_IDKUN_TRAIL) OVER () AS rmax_date
          FROM trail_peilut_ovdim po
          where po.MISPAR_ISHI= p_mispar_ishi
                and po.TAARICH = p_date)
        WHERE TAARICH_IDKUN_TRAIL = rmax_date)*/


EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;
END pro_oved_update_fields;

PROCEDURE pro_upd_card_status(p_mispar_ishi IN TB_YAMEY_AVODA_OVDIM.mispar_ishi%TYPE,
                              p_card_date IN TB_YAMEY_AVODA_OVDIM.taarich%TYPE,
                              p_status IN TB_YAMEY_AVODA_OVDIM.status%TYPE,
							  p_user_id IN TB_YAMEY_AVODA_OVDIM.meadken_acharon%TYPE  ) IS
BEGIN
    UPDATE TB_YAMEY_AVODA_OVDIM
    SET status = p_status,
        taarich_idkun_acharon = SYSDATE ,
		meadken_acharon=p_user_id
    WHERE mispar_ishi = p_mispar_ishi AND taarich = p_card_date;
    
     if ((p_mispar_ishi <> p_user_id) and (p_user_id > 0) ) then
            pkg_utils.pro_insert_meadken_acharon(p_mispar_ishi,p_card_date );
       end if;   
EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;
END  pro_upd_card_status;

PROCEDURE pro_get_ctb_shgiot(p_error_code IN CTB_SHGIOT.kod_shgia%TYPE, p_cur OUT CurType) IS
BEGIN
    OPEN p_cur FOR
    SELECT teur_shgia, kod_ishur
    FROM CTB_SHGIOT
    WHERE kod_shgia = p_error_code
          AND Pail = 1;

EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END pro_get_ctb_shgiot;

PROCEDURE pro_ins_approval_errors(p_obj_shgiot_meusharot IN obj_shgiot_meusharot ) IS
BEGIN
      -- Insert to tb_shgiot_meusharot
      INSERT INTO TB_SHGIOT_MEUSHAROT
                (mispar_ishi,
                 kod_shgia,
                 taarich,
                 mispar_sidur,
                 shat_hatchala,
                 shat_yetzia,
                 mispar_knisa,
                 gorem_measher,
                 taarich_ishur,
                 heara)

         VALUES (p_obj_shgiot_meusharot.mispar_ishi,
                 p_obj_shgiot_meusharot.kod_shgia,
                 p_obj_shgiot_meusharot.taarich,
                 p_obj_shgiot_meusharot.mispar_sidur,
                 p_obj_shgiot_meusharot.shat_hatchala,
                 p_obj_shgiot_meusharot.shat_yetzia,
                 p_obj_shgiot_meusharot.mispar_knisa,
                 p_obj_shgiot_meusharot.gorem_measher,
                 p_obj_shgiot_meusharot.taarich_ishur,
                 p_obj_shgiot_meusharot.heara);

EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END pro_ins_approval_errors;
PROCEDURE pro_upd_approval_errors(p_mispar_ishi IN TB_SIDURIM_OVDIM.mispar_ishi%TYPE,
                                      p_taarich  IN TB_SIDURIM_OVDIM.taarich%TYPE,
                                      p_mispar_sidur IN TB_SIDURIM_OVDIM.mispar_sidur%TYPE,
                                      p_shat_hatchala IN TB_SIDURIM_OVDIM.shat_hatchala%TYPE,
                                      p_shat_yetzia IN TB_PEILUT_OVDIM.shat_yetzia%TYPE,
                                      p_mispar_knisa IN TB_PEILUT_OVDIM.mispar_knisa%TYPE,
                                      p_new_mispar_sidur IN TB_SIDURIM_OVDIM.mispar_sidur%TYPE,
                                      p_new_shat_hatchala IN TB_SIDURIM_OVDIM.shat_hatchala%TYPE,
                                      p_new_shat_yetzia IN TB_PEILUT_OVDIM.shat_yetzia%TYPE) IS       
BEGIN
      -- Update  tb_shgiot_meusharot
      UPDATE TB_SHGIOT_MEUSHAROT
      SET                  
         mispar_sidur  = p_mispar_sidur,
        -- shat_hatchala = p_new_shat_hatchala,
         shat_yetzia   = p_new_shat_yetzia                  
      WHERE 
         mispar_ishi   = p_mispar_ishi AND
         taarich       = p_taarich AND         
         mispar_sidur  = p_mispar_sidur AND
         shat_hatchala = p_shat_hatchala AND
         shat_yetzia   = p_shat_yetzia AND
         mispar_knisa  = p_mispar_knisa;
       
      
EXCEPTION
         WHEN OTHERS THEN
              RAISE;                                         
END pro_upd_approval_errors; 
PROCEDURE pro_upd_approval_errors(p_mispar_ishi IN TB_SIDURIM_OVDIM.mispar_ishi%TYPE,
                                  p_taarich  IN TB_SIDURIM_OVDIM.taarich%TYPE,
                                  p_mispar_sidur IN TB_SIDURIM_OVDIM.mispar_sidur%TYPE,
                                  p_shat_hatchala IN TB_SIDURIM_OVDIM.shat_hatchala%TYPE,                                  
                                  p_new_mispar_sidur IN TB_SIDURIM_OVDIM.mispar_sidur%TYPE,
                                  p_new_shat_hatchala IN TB_SIDURIM_OVDIM.shat_hatchala%TYPE) IS
BEGIN
      -- Update  tb_shgiot_meusharot
      UPDATE TB_SHGIOT_MEUSHAROT
      SET                  
         mispar_sidur  = p_mispar_sidur,
         shat_hatchala = p_new_shat_hatchala                          
      WHERE 
         mispar_ishi   = p_mispar_ishi AND
         taarich       = p_taarich AND         
         mispar_sidur  = p_mispar_sidur AND
         shat_hatchala = p_shat_hatchala ;
        
END pro_upd_approval_errors;                                                                       
FUNCTION fn_is_approval_errors_exists(p_obj_shgiot_meusharot IN obj_shgiot_meusharot, p_level IN NUMBER) RETURN NUMBER IS
    iResult NUMBER;
BEGIN
    --Check if Error approval exists in tb_shgiot_meusharot
    --level 1- yom-avoda
    --level 2- sidur
    --level 3- peilut

    IF (p_level=1) THEN
        SELECT COUNT(kod_shgia) INTO iResult
        FROM TB_SHGIOT_MEUSHAROT S
        WHERE S.MISPAR_ISHI =  p_obj_shgiot_meusharot.mispar_ishi AND
              S.TAARICH     =  p_obj_shgiot_meusharot.taarich     AND
              S.KOD_SHGIA   =  p_obj_shgiot_meusharot.kod_shgia;
    END IF;


    IF (p_level=2) THEN
        SELECT COUNT(kod_shgia) INTO iResult
        FROM TB_SHGIOT_MEUSHAROT S
        WHERE S.MISPAR_ISHI   =  p_obj_shgiot_meusharot.mispar_ishi  AND
              S.TAARICH       =  p_obj_shgiot_meusharot.taarich      AND
              S.KOD_SHGIA     =  p_obj_shgiot_meusharot.kod_shgia    AND
              S.MISPAR_SIDUR  =  p_obj_shgiot_meusharot.mispar_sidur AND
              S.SHAT_HATCHALA =  p_obj_shgiot_meusharot.shat_hatchala;

    END IF;

     IF (p_level=3) THEN
        SELECT COUNT(kod_shgia) INTO iResult
        FROM TB_SHGIOT_MEUSHAROT S
        WHERE S.MISPAR_ISHI   =  p_obj_shgiot_meusharot.mispar_ishi   AND
              S.TAARICH       =  p_obj_shgiot_meusharot.taarich       AND
              S.KOD_SHGIA     =  p_obj_shgiot_meusharot.kod_shgia     AND
              S.MISPAR_SIDUR  =  p_obj_shgiot_meusharot.mispar_sidur  AND
              S.SHAT_HATCHALA =  p_obj_shgiot_meusharot.shat_hatchala AND
              S.SHAT_YETZIA   =  p_obj_shgiot_meusharot.shat_yetzia   AND
              S.MISPAR_KNISA  =  p_obj_shgiot_meusharot.mispar_knisa  ;

    END IF;
    RETURN iResult;
EXCEPTION
         WHEN OTHERS THEN
              iResult:=0;
              RAISE;
END  fn_is_approval_errors_exists;
PROCEDURE pro_get_errors_for_field(p_field_name IN CTB_SHGIOT.natun_lebdika%TYPE,p_shgiot_leoved IN NUMBER, p_cur OUT CurType) IS
BEGIN
    OPEN p_cur FOR
    SELECT S.KOD_SHGIA, s.TEUR_SHGIA, NVL(S.KOD_ISHUR,0) KOD_ISHUR, '' SHOW_ERROR, ('' || '|' ||  NVL(S.KOD_ISHUR,0) || '|' || S.KOD_SHGIA) ERR_KEY, NVL(S.ISHUR_RASHEMET,0) ISHUR_RASHEMET, '' USER_PROFILE
    FROM CTB_SHGIOT s
    WHERE UPPER(TRIM(S.NATUN_LEBDIKA)) = UPPER(TRIM(p_field_name))
     AND PAIL =1
     AND ((p_shgiot_leoved=1 AND S.LETEZUGA_LAOVED=1) OR (p_shgiot_leoved=0));
EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END pro_get_errors_for_field;

PROCEDURE pro_upd_tar_ritzat_shgiot(p_mispar_ishi IN TB_YAMEY_AVODA_OVDIM.mispar_ishi%TYPE,
                              p_card_date IN TB_YAMEY_AVODA_OVDIM.taarich%TYPE,
							  p_shgiot_letzuga IN TB_YAMEY_AVODA_OVDIM.shgiot_letezuga_laoved%TYPE) IS
BEGIN
    UPDATE TB_YAMEY_AVODA_OVDIM
    SET Ritzat_Shgiot_Acharona=SYSDATE,
	          shgiot_letezuga_laoved=p_shgiot_letzuga
    WHERE mispar_ishi = p_mispar_ishi
	AND taarich = p_card_date;
EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;
END  pro_upd_tar_ritzat_shgiot;

PROCEDURE pro_is_duplicate_travel(p_mispar_ishi IN TB_PEILUT_OVDIM.mispar_ishi%TYPE,
		 									  	 						 p_taarich  IN TB_PEILUT_OVDIM.taarich%TYPE,
																		 p_makat_nesia IN TB_PEILUT_OVDIM.makat_nesia%TYPE,
																		 p_shat_yetzia IN TB_PEILUT_OVDIM.shat_yetzia%TYPE,
																		 p_mispar_knisa IN TB_PEILUT_OVDIM.mispar_knisa%TYPE,
																		 p_Cur OUT CurType) IS
 BEGIN
     OPEN p_cur FOR
		   SELECT y.mispar_ishi,y.taarich
		    FROM TB_PEILUT_OVDIM p,TB_YAMEY_AVODA_OVDIM y
		    WHERE p.mispar_ishi=y.mispar_ishi
		   AND p.taarich=y.taarich
		   AND (y.measher_o_mistayeg=0 OR y.measher_o_mistayeg=1)
		   AND p.mispar_ishi<>p_mispar_ishi
		  AND p.taarich=TRUNC(p_taarich)
		  AND p.makat_nesia= p_makat_nesia
		  AND p.shat_yetzia=p_shat_yetzia
		  AND p.mispar_knisa=p_mispar_knisa;

 EXCEPTION
         WHEN OTHERS THEN
		RAISE;

END pro_is_duplicate_travel;

PROCEDURE pro_have_sidur_chofef(p_mispar_ishi IN TB_SIDURIM_OVDIM.mispar_ishi%TYPE,
		 									  	 						 p_taarich  IN TB_SIDURIM_OVDIM.taarich%TYPE,
																		 p_mispar_sidur IN TB_SIDURIM_OVDIM.mispar_sidur%TYPE,
																		 p_shat_hatchala IN TB_SIDURIM_OVDIM.shat_hatchala%TYPE,
																		 p_shat_gmar IN TB_SIDURIM_OVDIM.shat_gmar%TYPE,
																		 p_param_chafifa IN NUMBER,
																		 p_Cur OUT CurType) IS
BEGIN
DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'pkg_errors',action_name => 'pro_have_sidur_chofef');
   OPEN p_cur FOR
		   SELECT y.mispar_ishi,y.taarich
		   		FROM TB_SIDURIM_OVDIM s,TB_YAMEY_AVODA_OVDIM y
				WHERE s.mispar_ishi=y.mispar_ishi
				AND s.taarich=y.taarich
		       AND (y.measher_o_mistayeg=0 OR y.measher_o_mistayeg=1)
				AND s.mispar_ishi<>p_mispar_ishi
				AND s.taarich=TRUNC(p_taarich)
				AND s.mispar_sidur= p_mispar_sidur
				AND (p_shat_hatchala<s.shat_gmar OR p_shat_gmar>s.shat_hatchala )
				AND ((s.shat_hatchala<=p_shat_hatchala AND s.shat_gmar>=p_shat_gmar AND  (p_shat_gmar-p_shat_hatchala)*1440>p_param_chafifa)
				OR  (s.shat_gmar<p_shat_gmar AND  (s.shat_gmar-p_shat_hatchala)*1440>p_param_chafifa)
				OR  (s.shat_gmar>p_shat_gmar AND  (p_shat_gmar-s.shat_hatchala)*1440>p_param_chafifa)
				OR (s.shat_hatchala>p_shat_hatchala AND s.shat_gmar<= p_shat_gmar  AND (s.shat_gmar-s.shat_hatchala)*1440>p_param_chafifa));

 EXCEPTION
         WHEN OTHERS THEN
		RAISE;

END pro_have_sidur_chofef;

FUNCTION  fn_count_shgiot_letzuga(p_arr_Kod_Shgia IN VARCHAR2) RETURN NUMBER
IS
    v_count NUMBER;
BEGIN
    v_count:=0;
		SELECT COUNT(*) INTO v_count
		FROM CTB_SHGIOT
		WHERE LETEZUGA_LAOVED = 1
		AND kod_shgia IN(SELECT x FROM  TABLE(CAST(Convert_String_To_Table(p_arr_Kod_Shgia ,  ',') AS mytabtype)));

         RETURN v_count;
EXCEPTION
        WHEN NO_DATA_FOUND THEN
             RETURN 0;
        WHEN OTHERS THEN
		RAISE;

END fn_count_shgiot_letzuga;

PROCEDURE get_idkuney_rashemet(p_mispar_ishi IN TB_IDKUN_RASHEMET.mispar_ishi%TYPE,
		  										   							       p_taarich  IN TB_IDKUN_RASHEMET.taarich%TYPE,
																				   p_Cur OUT CurType) AS
  BEGIN
   DBMS_APPLICATION_INFO.SET_MODULE('pkg_errors.get_idkuney_rashemet','get idkuney rashemet');
	
      OPEN p_Cur FOR
         SELECT i.taarich,i.mispar_sidur,i.shat_hatchala,i.shat_yetzia,i.mispar_knisa,UPPER(m.shem_db) shem_db,i.pakad_id
		 FROM TB_IDKUN_RASHEMET i, TB_MASACH m
		 WHERE m.masach_id=19
		 AND m.sug=2
		 AND m.pakad_id=i.pakad_id
		 AND i.mispar_ishi=p_mispar_ishi
		 AND i.taarich=p_taarich;

		   EXCEPTION
        WHEN OTHERS THEN
		RAISE;
  END get_idkuney_rashemet;

  PROCEDURE pro_check_have_sidur_grira(p_mispar_ishi IN TB_SIDURIM_OVDIM.mispar_ishi%TYPE,
		 									  	 						 p_taarich  IN TB_SIDURIM_OVDIM.taarich%TYPE,
																		 p_Cur OUT CurType) IS
 BEGIN
     OPEN p_cur FOR
		  SELECT o.mispar_ishi, o.mispar_sidur,o.bitul_o_hosafa, o.shat_gmar, o.shat_hatchala, o.yom_visa,
           o.chariga,NVL(o.pitzul_hafsaka,0)pitzul_hafsaka,o.taarich,o.shayah_leyom_kodem,o.mispar_shiurey_nehiga,
           o.out_michsa ,
		   o.mikum_shaon_knisa,o.mikum_shaon_yetzia,o.mezake_halbasha,
           o.hashlama,o.lo_letashlum,
           o.kod_siba_ledivuch_yadani_in, o.kod_siba_ledivuch_yadani_out, o.kod_siba_lo_letashlum,
           o.shat_hatchala_letashlum, o.shat_gmar_letashlum,o.mezake_nesiot,
           o.tosefet_grira, o.achuz_knas_lepremyat_visa,o.achuz_viza_besikun,
           o.mispar_musach_o_machsan,o.sug_hazmanat_visa, o.sug_hashlama,
           o.heara, o.taarich_idkun_acharon, o.meadken_acharon,O.MENAHEL_MUSACH_MEADKEN,
           o.shat_gmar_letashlum_musach,O.SHAT_HATCHALA_LETASHLUM_MUSACH,
           o.sector_visa, o.nidreshet_hitiatzvut, o.shat_hitiatzvut, o.ptor_mehitiatzvut,o.Hachtama_Beatar_Lo_Takin FROM 
			TB_SIDURIM_OVDIM o,
			   pivot_sidurim_meyuchadim v_sidurm
			WHERE o.mispar_ishi=p_mispar_ishi 
			AND o.taarich=p_taarich
			AND SUBSTR(o.mispar_sidur,0,2)='99'
			AND v_sidurm.mispar_sidur(+)=o.mispar_sidur
			 AND o.taarich BETWEEN v_sidurm.me_tarich(+) AND v_sidurm.ad_tarich(+)
			 AND v_sidurm.sug_avoda=9;

 EXCEPTION
         WHEN OTHERS THEN
		RAISE;

END pro_check_have_sidur_grira;

PROCEDURE pro_get_shgiot_no_active( p_cur OUT CurType) IS
BEGIN
	 DBMS_APPLICATION_INFO.SET_MODULE('pkg_errors.pro_get_shgiot_no_active','get shgiot no active');
    OPEN p_cur FOR
    SELECT kod_shgia
    FROM CTB_SHGIOT
    WHERE  Pail = 0;

EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END pro_get_shgiot_no_active;
PROCEDURE pro_get_all_shgiot(p_cur OUT CurType) IS
BEGIN   
    OPEN p_cur FOR
    SELECT s.kod_shgia,s.teur_shgia,M.TEUR,s.letezuga_laoved
    FROM CTB_SHGIOT s,TB_MASACH m
    WHERE UPPER(s.NATUN_LEBDIKA) = UPPER(m.SHEM_DB(+));
END pro_get_all_shgiot;

PROCEDURE pro_get_shgiot_active(p_cur OUT CurType) IS
BEGIN   
    OPEN p_cur FOR
    SELECT s.kod_shgia,s.teur_shgia,M.TEUR,s.letezuga_laoved,s.RAMA
    FROM CTB_SHGIOT s,TB_MASACH m
    WHERE UPPER(s.NATUN_LEBDIKA) = UPPER(m.SHEM_DB(+))
    and nvl(s.Pail,1) <>0;
END pro_get_shgiot_active;
PROCEDURE get_approval_errors(p_mispar_ishi IN TB_IDKUN_RASHEMET.mispar_ishi%TYPE,
		  										   							       p_taarich  IN TB_IDKUN_RASHEMET.taarich%TYPE,
																				   p_Cur OUT CurType) AS
  BEGIN
      OPEN p_Cur FOR
         SELECT s.KOD_SHGIA,s.MISPAR_SIDUR,s.SHAT_HATCHALA,s.SHAT_YETZIA,s.MISPAR_KNISA
		 FROM TB_SHGIOT_MEUSHAROT S
        WHERE S.MISPAR_ISHI =  p_mispar_ishi 
		AND  S.TAARICH     = p_taarich;

		   EXCEPTION
        WHEN OTHERS THEN
		RAISE;
  END get_approval_errors;
  
  PROCEDURE pro_upd_approval_errors(p_coll_shgiot_meusharot IN coll_shgiot_meusharot) IS
BEGIN
      IF (p_coll_shgiot_meusharot IS NOT NULL) THEN
          FOR i IN 1..p_coll_shgiot_meusharot.COUNT LOOP
               IF (p_coll_shgiot_meusharot(i).NEW_SHAT_HATCHALA=p_coll_shgiot_meusharot(i).SHAT_HATCHALA) THEN                  
                  		 IF (p_coll_shgiot_meusharot(i).NEW_SHAT_YETZIA<>p_coll_shgiot_meusharot(i).SHAT_YETZIA) THEN
                      	 	pro_upd_approval_errors(p_coll_shgiot_meusharot(i).mispar_ishi,
                                                   p_coll_shgiot_meusharot(i).taarich, 
                                                   p_coll_shgiot_meusharot(i).mispar_sidur,
                                                   p_coll_shgiot_meusharot(i).shat_hatchala,
                                                   p_coll_shgiot_meusharot(i).shat_yetzia,
                                                  p_coll_shgiot_meusharot(i).mispar_knisa,                                 
                                                   p_coll_shgiot_meusharot(i).mispar_sidur,
                                                   p_coll_shgiot_meusharot(i).new_shat_hatchala,
                                                  p_coll_shgiot_meusharot(i).new_shat_yetzia);                     
                  		END IF;
                  ELSE
                   	  	 pro_upd_approval_errors(p_coll_shgiot_meusharot(i).mispar_ishi,
                                                   p_coll_shgiot_meusharot(i).taarich, 
                                                  p_coll_shgiot_meusharot(i).mispar_sidur,
                                                  p_coll_shgiot_meusharot(i).shat_hatchala,                                  
                                                 p_coll_shgiot_meusharot(i).mispar_sidur,
                                                 p_coll_shgiot_meusharot(i).new_shat_hatchala); 
                 END IF;   
          END LOOP;
      END IF;
    EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END pro_upd_approval_errors;

/*
PROCEDURE pro_del_sidurim_ovdim(p_coll_sidurim_ovdim_del IN coll_sidurim_ovdim,  p_coll_sidurim_ovdim_ins  IN  OUT coll_sidurim_ovdim,p_type_update IN  NUMBER) IS
isExists number;
iIns number;
BEGIN
    IF (p_coll_sidurim_ovdim_del IS NOT NULL) THEN
        FOR i IN 1..p_coll_sidurim_ovdim_del.COUNT LOOP           
               IF (p_coll_sidurim_ovdim_del(i).update_object=p_type_update) THEN
                      pro_ins_sidurim_ovdim_trail(p_coll_sidurim_ovdim_del(i),2);
                      pro_del_sidur_oved(p_coll_sidurim_ovdim_del(i));
               ELSE  
                    iIns:=-1;
                 IF (p_coll_sidurim_ovdim_ins IS NOT NULL) THEN
                        FOR j IN 1..p_coll_sidurim_ovdim_ins.COUNT LOOP   
                            if(p_coll_sidurim_ovdim_del(i).mispar_ishi= p_coll_sidurim_ovdim_ins(j).mispar_ishi and
                               TRUNC(p_coll_sidurim_ovdim_del(i).taarich) =TRUNC(p_coll_sidurim_ovdim_ins(j).taarich) and
                               p_coll_sidurim_ovdim_del(i).mispar_sidur = p_coll_sidurim_ovdim_ins(j).mispar_sidur  and
                                p_coll_sidurim_ovdim_del(i).shat_hatchala =  p_coll_sidurim_ovdim_ins(j).shat_hatchala) then
                                iIns:=j;
                            end if;
                                 
                        /*   BEGIN
                            SELECT count(*) into isExists
                            from tb_sidurim_ovdim s
                            where  s.mispar_ishi   =p_coll_sidurim_ovdim_ins(j).mispar_ishi AND
                                       s.taarich = TRUNC(p_coll_sidurim_ovdim_ins(j).taarich) AND
                                       s.mispar_sidur  =p_coll_sidurim_ovdim_ins(j).mispar_sidur AND
                                       s.shat_hatchala  = p_coll_sidurim_ovdim_ins(j).shat_hatchala;       
                             EXCEPTION
                                           WHEN NO_DATA_FOUND  THEN
                                             isExists:=0;
                            END;*/

                  /*      END LOOP;
                 END IF;*/
                
             
                   
          /*         if (iIns>-1) then
                     --pro_ins_sidurim_ovdim_trail(p_coll_sidurim_ovdim_del(i),2);
                     pro_upd_sidur_oved(p_coll_sidurim_ovdim_ins( iIns) );
                     p_coll_sidurim_ovdim_ins( iIns).update_object :=9;*/
                 --    p_coll_sidurim_ovdim_ins( iIns).;
         --     p_coll_sidurim_ovdim_ins.removeat(iIns);
                /* delete from p_coll_sidurim_ovdim_ins s where 
                                s.mispar_ishi= p_coll_sidurim_ovdim_del(i).mispar_ishi and
                                TRUNC(s.taarich) =TRUNC(p_coll_sidurim_ovdim_del(i).taarich) and
                                s.mispar_sidur = p_coll_sidurim_ovdim_del(i).mispar_sidur  and
                                s.shat_hatchala =  p_coll_sidurim_ovdim_del(i).shat_hatchala; */
      /*             end if;
                             
               END IF;        
        END LOOP;
    END IF;
      EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END pro_del_sidurim_ovdim;
*/

PROCEDURE pro_Delete_Errors(p_mispar_ishi IN NUMBER,p_date IN DATE) IS
BEGIN
DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'pkg_errors',action_name => 'pro_Delete_Errors');
    DELETE TB_SHGIOT T 
    WHERE T.MISPAR_ISHI =p_mispar_ishi
          and T.taarich =  p_date;

EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END pro_Delete_Errors ;


END Pkg_Errors;
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
        ORDER BY KOD_PROFIL,TEUR_PROFIL_HEB
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
                                OR   (po.ME_TARICH>= ''' ||  P_STARTDATE  || '''  AND   NVL(po.ad_TARICH,TO_DATE(''01/01/9999'' ,''dd/mm/yyyy''))<=  ''' ||  P_ENDDATE  || ''')  )
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
       Catalog.DESCRIPTION ,Catalog.SHILUT ,
       decode(IsElement,''true'',Pkg_Elements.getDakotByMakatElement(s.MAKAT_NESIA) , Catalog.MAZAN_TASHLUM) MAZAN_TASHLUM,
       decode(IsElement,''true'',TO_NUMBER( TO_CHAR(Pkg_Elements.getKmByMakatElement(s.MAKAT_NESIA),''999.99'') ) , Catalog.KM) KM,
       Catalog.NIHUL_NAME ,Catalog.ACTIVITY_DATE  , Catalog.MAKAT8
from (select S.mispar_ishi,S.TAARICH,Dayofweek(s.taarich) Dayofweek ,S.MISPAR_SIDUR,S.SHAT_HATCHALA,S.SHAT_GMAR,S.MEADKEN_ACHARON, S.CHARIGA,S.HASHLAMA,S.OUT_MICHSA,
                  P.SNIF_TNUA, P.MAKAT_NESIA,P.MISPAR_KNISA,P.SHAT_YETZIA,P.OTO_NO,
                  Pkg_Elements.IsElementZmanAnddNesia(P.MAKAT_NESIA) IsElement
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
                              SELECT DISTINCT s.SHEM_DOCH_BAKOD ,s.TEUR_DOCH  || '('|| S.KOD_SUG_DOCH ||')'  TEUR_DOCH , 
                              S.TEUR_DOCH TEUR_DOCH_2, S.RS_VERSION, r.URL_CONFIG_KEY, r.SERVICE_URL_CONFIG_KEY,r.EXTENSION 
                         FROM   CTB_SUGEY_DOCHOT s,
                                TB_PROFIL_DOCHOT p,
                                CTB_DOCHOT_RS_VERSION r
                         WHERE s.KOD_SUG_DOCH = p.KOD_SUG_DOCH
                                AND s.pail=1
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
                                            P_MisparIshi  IN VARCHAR2 ,
                                            P_EZOR IN VARCHAR ,
                                            P_COMPANYID  IN VARCHAR2 ,
                                            P_MAMAD IN VARCHAR2 ,
                                            P_MAMAD_HR IN VARCHAR2 ,
                                            P_SNIF IN VARCHAR2 ,
                                            P_ISUK VARCHAR2 ,
                                            P_KOD_YECHIDA IN VARCHAR2 ,
                                            P_SECTORISUK IN VARCHAR2 ,
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
FROM OVDIM Ov , 
PIVOT_PIRTEY_OVDIM Details  ,
(SELECT po.mispar_ishi,MAX(po.ME_TARICH) me_taarich
                       FROM PIVOT_PIRTEY_OVDIM PO
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
                      and co.mispar_ishi =Ov.mispar_ishi 
                        and CO.TAARICH BETWEEN  ''' ||  P_STARTDATE  || '''  AND ''' ||  P_ENDDATE  || ''' 
                 group by  co.mispar_ishi      
                 ) ';

IF (P_WorkerViewLevel = 5) THEN
  GeneralQry := GeneralQry || ' And   Ov.mispar_ishi in Tree.mispar_ishi  ';
END IF ;   

 
IF (( P_MisparIshi IS NOT  NULL ) OR ( P_MisparIshi <> '' )) THEN 
    ParamQry := ParamQry || ' and  Details.mispar_ishi in (' || P_MisparIshi || ')  ';
END IF ; 


IF (  p_ezor <> '-1' ) THEN 
    ParamQry := ParamQry || ' and  Details.ezor in (' || p_ezor || ')  ';
END IF ; 

IF (P_COMPANYID <> '-1' ) THEN 
    ParamQry := ParamQry || ' and  OV.KOD_HEVRA in (' || P_COMPANYID || ')  ';
END IF ;

IF ( P_MAMAD <> '-1' ) THEN 
    ParamQry := ParamQry || ' and  substr(Details.MAAMAD,0,1) = ' || P_MAMAD ;
END IF ;  

IF (( P_MAMAD_HR IS NOT  NULL ) OR ( P_MAMAD_HR <> '' )) THEN 
    ParamQry := ParamQry || ' and  Details.MAAMAD in (' || P_MAMAD_HR || ')  ';
END IF ; 

IF (( P_ISUK IS NOT  NULL ) OR ( P_ISUK <> '' )) THEN 
    ParamQry := ParamQry || ' and Isuk.KOD_isuk in (' || P_ISUK || ')  ';
END IF ; 
 
IF (( P_SNIF IS NOT  NULL ) OR ( P_SNIF <> '' )) THEN 
    ParamQry := ParamQry || ' and details.snif_av in (' || P_SNIF || ')  ';
END IF ; 
 
IF (( P_KOD_YECHIDA IS NOT  NULL ) OR ( P_KOD_YECHIDA <> '' )) THEN 
    ParamQry := ParamQry || ' and YECHIDA.KOD_YECHIDA in (' || P_KOD_YECHIDA || ')  ';
END IF ; 

IF (( P_SECTORISUK IS NOT  NULL ) OR ( P_SECTORISUK <> '' )) THEN 
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
                    FROM OVDIM Ov , 
                    PIVOT_PIRTEY_OVDIM Details  ,
                    (SELECT po.mispar_ishi,MAX(po.ME_TARICH) me_taarich
                                           FROM PIVOT_PIRTEY_OVDIM PO
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
                   
                   and (P_Ezor='-1'  or Details.Ezor in ( (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_Ezor,  ',') AS MYTABTYPE))) ))
                    and (P_COMPANYID='-1' or  OV.KOD_HEVRA in ( (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_COMPANYID,  ',') AS MYTABTYPE))) ))
                    and (P_MAMAD='-1' or substr(Details.MAAMAD,0,1)  in ( (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MAMAD,  ',') AS MYTABTYPE))) ))
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
            SELECT T.MISPAR_ISHI  ,t.taarich,t.SHAT_HITYAZVUT,t.MIKUM_SHAON
            FROM  TB_HITYAZVUT_PUNDAKIM t,ovdim v
            WHERE   T.MISPAR_ISHI = V.MISPAR_ISHI and
                          t.TAARICH BETWEEN (P_STARTDATE-1) AND P_ENDDATE  AND
                          ((to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))>=0 and to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))<5)  or to_char(t.SHAT_HITYAZVUT,'HH24:mi:ss')='05:00:00') 
                        --   to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))>=0 and to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))<=5 
                        --  T.MIKUM_SHAON='9601'   
                          ) h
where  h.TAARICH BETWEEN (P_STARTDATE-1) AND P_ENDDATE
 
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
                      (select    z.TAARICH ,0 Kod_Rechiv, z.SNIF_AV, z.EZOR, 'מספר עובדים בסניף לחודש' TEUR_RECHIV,'' TEUR_SUG_ERECH,0 MIYUN_MEMUZAIM,
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
        trim(TO_CHAR(zo.R131 ,'9999')) R131
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
                 SUM(   CASE kod_rechiv WHEN 219 THEN Erech_Rechiv ELSE NULL END) R219     
      from(
                  select ch.*
                  from TB_CHISHUV_YOMI_OVDIM Ch,p
                  where Ch.Taarich BETWEEN  p_FromDate  AND p_ToDate
                     AND Ch.Bakasha_ID = p_BakashaId
                     and CH.KOD_RECHIV in(1,5,18,32,49,60,66,67,76,77,78,96,108,126,131,219)
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
                                    ( case when x.maamad='1' then 'חברים' else case when  x.maamad='2' then 'שכירים' else 'חברים ושכירים' end end) || ' - ' ||   
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
           TO_NUMBER( TO_CHAR(  (S.SHAT_GMAR_LETASHLUM - S.SHAT_HATCHALA_LETASHLUM)*1440,'999.99') ) dakot_nocechut,
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
       p_hearat_chufsha:='חופשה מאושרת ';
     end if;
         
    select   Dayofweek(taarich) into t_yom from dual;
        DBMS_OUTPUT.PUT_LINE(taarich);
     if (cnt_sidurim_beyom=0 and (t_yom not in ('ו','ש','ח','ע') )) then
         if (kod_headrut=1 ) then
            return p_hearat_chufsha;
        end if;
        return 'אין דיווח';
     end if;

    if( mispar_sidur is not null and (shat_hatchala is null or shat_gmar  is null)  and mispar_sidur<>99200) then
      p_heara_ezer:= 'חסר דיווח';
    end if;

   if( mispar_sidur is not null and kod_headrut=1 and is_headrut=0) then
       p_heara_ezer:= 'ס.ע';
     end if;
     
     if(is_headrut>0 and kod_headrut=1 ) then
       p_heara_ezer:=  'סידור היעדרות';
     end if;
  
    if (p_heara_ezer is null and cnt_others_sidurim >0 and cnt_sidurim_beyom=cnt_others_sidurim) then 
       p_heara_ezer:=   'ס.ע אחר';
     end if;
   
    if (p_hearat_chufsha is null and lo_letashlum =1) then
        p_heara_ezer:='לא לתשלום';
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
                                                       p_cur OUT CurType)   as 
  --P_STARTDATE date;
 -- P_ENDDATE date;
  begin
  DBMS_APPLICATION_INFO.SET_MODULE(module_name => 'Pkg_Reports',action_name => 'pro_get_mushalim_details');
--P_STARTDATE:=sysdate;
--P_ENDDATE:=sysdate;
  open p_cur for
with p as
    (  select p.mispar_ishi,p.me_tarich,p.ad_tarich,P.SNIF_AV,P.EZOR,P.DIRUG,P.MAAMAD,P.GIL,P.ISUK
                from pivot_pirtey_ovdim p,ctb_snif_av s,-- ovdim o,
                     (SELECT po.mispar_ishi,MAX(po.ME_TARICH) me_taarich
                                           FROM PIVOT_PIRTEY_OVDIM PO
                                           WHERE po.isuk IS NOT NULL
                                                 AND (P_STARTDATE BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                                                   OR P_ENDDATE BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                                                    OR (  po.ME_TARICH>=P_STARTDATE  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=P_ENDDATE))
                                          GROUP BY po.mispar_ishi) RelevantDetails 
                where   p.mispar_ishi= RelevantDetails.mispar_ishi
                     and   P.ME_TARICH = RelevantDetails.me_taarich  
                  --   and  p.mispar_ishi=  o.mispar_ishi
                  --   and o.kod_hevra=580
                     and p.SNIF_AV=S.KOD_SNIF_AV
                     and S.KOD_HEVRA=4895
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
                            AND b.Huavra_Lesachar=1
                               and CO.TAARICH between P_STARTDATE and P_ENDDATE
                          and  co.mispar_ishi=p.mispar_ishi
                     ) a
        WHERE a.th1=a.th2 
        AND a.mispar_ishi=P.mispar_ishi
    GROUP BY  a.mispar_ishi, a.Taarich, a.bakasha_id
      )--,
,zman_egged_tavura as
 (
    select c.mispar_ishi, C.BAKASHA_ID,trunc(c.taarich,'MM') taarich,
            -- sum(nvl(c.erech_rechiv,0)) time_egTav,
            -- sum( erech_rechiv) rechiv_1s,
            sum( case when  snif_tnua in(38,45,46,67,98 ) then erech_rechiv else 0 end ) time_egTav
    from        
      (  select c.mispar_ishi, C.BAKASHA_ID,    c.taarich,erech_rechiv,
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
         and C.KOD_RECHIV=18 ) c
        group by  c.mispar_ishi, C.BAKASHA_ID,trunc(c.taarich,'MM') 
 )      
      

select h.mispar_ishi,ov.SHEM_MISH ||  ' ' || OV.SHEM_PRAT shem, m.TEUR_MAAMAD_HR maamad,G.TEUR_KOD_GIL gil, 
         e.kod_ezor,E.TEUR_EZOR, S.TEUR_SNIF_AV,I.KOD_ISUK,  I.TEUR_ISUK, h.dirug, to_char( h.taarich,'mm/yyyy') taarich,
         h.rechiv_109 yemey_nochechut,
         round((h.time_egTav/60),2) zman_bafoal_egd_tavura,
         round((rechiv_1/60),2) zman_avoda,
         round((h.rechiv_18/60),2)   zman_avoda_bafoal,
         round(  ((rechiv_2+rechiv_3+rechiv_4)/60),2) shaot_regilot, 
         round((h.rechiv_19+h.rechiv_20+h.rechiv_21+ rechiv_131)/60,2)  shaot_nosafot,
    --     to_number(to_char(( ((rechiv_19+rechiv_20+rechiv_21)/60) +rechiv_100) ,'999.99'))shaot_nosafot,
        -- case when OV.KOD_HEVRA=4895 then round(((h.rechiv_19+h.rechiv_20+h.rechiv_21+ h.rechiv_100)/60),2) else
         --                                                        round(((h.rechiv_19+h.rechiv_20+h.rechiv_21)/60+ h.rechiv_100),2) end shaot_nosafot,
         round((h.rechiv_4 /60),2)  shaot_regilot_tafkid,
         round((h.rechiv_21 /60),2) shaot_nosafot_tafkid,
         round((h.rechiv_2 /60),2) shaot_regilot_nahagut,
         round((h.rechiv_19  /60),2)  shaot_nosafot_nahagut,
         round((h.rechiv_3 /60),2)  shaot_regilot_nihul_tnua,
         round((h.rechiv_20 /60),2)  shaot_nosafot_nihul_tnua,
         h.rechiv_96 zman_rezifut,
         round((h.rechiv_131 /60),2)  shaot_100_bafoal,
        case when OV.KOD_HEVRA=4895 then  round((h.rechiv_100 /60),2) else   round(h.rechiv_100,2) end  shaot_100_letashlum,
         round((h.rechiv_76 /60),2) shaot_125,
         round((h.rechiv_101 /60),2) shaot_125_letashlum,
         round((h.rechiv_77 /60),2) shaot_150,
         round((h.rechiv_102 /60),2) shaot_150_letashlum,
         round((h.rechiv_78 /60),2)  shaot_200,
         round((h.rechiv_103 /60),2) shaot_200_letashlum,
         round((h.rechiv_55 /60),2)  shaot_layla,
         h.rechiv_49 pizul, h.rechiv_75 yemey_nochechut_7,  
         h.rechiv_91 shaot_25, --  to_number(to_char(  ( h.rechiv_91 /60),'999.99'))  shaot_25,
         h.rechiv_92  shaot_50 -- to_number(to_char(  ( h.rechiv_92 /60),'999.99')) shaot_50
from ovdim ov,
        ctb_maamad m,
        ctb_kod_gil g,
        ctb_isuk i,
        ctb_snif_av s ,
        ctb_ezor e,
     (
 select z2.*, P.SNIF_AV,P.EZOR,P.DIRUG,P.MAAMAD,P.GIL,P.ISUK, egt.time_egTav--,egt.rechiv_1s
 from p,zman_egged_tavura egt,
     ( select  z.mispar_ishi,z.taarich, z.bakasha_id,
      sum(  case when z.kod_rechiv=1 then z.erech_rechiv else 0 end) rechiv_1 ,
                sum(  case when z.kod_rechiv=18 then z.erech_rechiv else 0 end) rechiv_18 ,
                sum(  case when z.kod_rechiv=2 then z.erech_rechiv else 0 end) rechiv_2 ,
                sum(  case  when z.kod_rechiv=3 then z.erech_rechiv else 0 end) rechiv_3,
                sum(   case when z.kod_rechiv=4 then z.erech_rechiv else 0 end) rechiv_4,
                sum( case when z.kod_rechiv=19 then z.erech_rechiv else 0 end) rechiv_19,
                 sum( case when z.kod_rechiv=20 then z.erech_rechiv else 0 end) rechiv_20,
                 sum( case when z.kod_rechiv=21 then z.erech_rechiv else 0 end) rechiv_21,
                  sum( case when z.kod_rechiv=49 then z.erech_rechiv else 0 end) rechiv_49,
                   sum( case when z.kod_rechiv=55 then z.erech_rechiv else 0 end) rechiv_55,
                   sum( case when z.kod_rechiv=76 then z.erech_rechiv else 0 end) rechiv_76,
                   sum( case when z.kod_rechiv=77 then z.erech_rechiv else 0 end) rechiv_77,
                   sum( case when z.kod_rechiv=78 then z.erech_rechiv else 0 end) rechiv_78,
                   sum( case when z.kod_rechiv=75 then z.erech_rechiv else 0 end) rechiv_75,
                   sum( case when z.kod_rechiv=91 then z.erech_rechiv else 0 end) rechiv_91,
                   sum( case when z.kod_rechiv=92 then z.erech_rechiv else 0 end) rechiv_92,
                   sum( case when z.kod_rechiv=96 then z.erech_rechiv else 0 end) rechiv_96,
                   sum( case when z.kod_rechiv=100 then z.erech_rechiv else 0 end) rechiv_100,
                   sum( case when z.kod_rechiv=101 then z.erech_rechiv else 0 end) rechiv_101,
                   sum( case when z.kod_rechiv=102 then z.erech_rechiv else 0 end) rechiv_102,
                   sum( case when z.kod_rechiv=103 then z.erech_rechiv else 0 end) rechiv_103,
                    sum( case when z.kod_rechiv=109 then z.erech_rechiv else 0 end) rechiv_109,
                  sum( case when z.kod_rechiv=151 then z.erech_rechiv else 0 end) rechiv_151,
                   sum( case when z.kod_rechiv=252 then z.erech_rechiv else 0 end) rechiv_252,
                   sum( case when z.kod_rechiv=250 then z.erech_rechiv else 0 end) rechiv_250,
                   sum( case when z.kod_rechiv=131 then z.erech_rechiv else 0 end) rechiv_131
                  -- sum( case when (z.kod_rechiv=1 and z.snif_tnua in(85,84,83,82,61 )) then z.erech_rechiv else 0 end ) zman_egd_tavura
                   
                   
                   
             from(
    
      select  c.mispar_ishi,c.taarich,c.bakasha_id, c.kod_rechiv,c.erech_rechiv--,c.mispar_sidur,substr(lpad(c.mispar_sidur,5,'0'),1,2) snif_tnua
      from  TB_CHISHUV_chodesh_OVDIM c,bakasha b
     where c.mispar_ishi=b.mispar_ishi
        and c.taarich=b.taarich
         and c.bakasha_id=b.bakasha_id
         and c.kod_rechiv in(1,75,18,2,3,4,252,151,250,100,21,19,20,96,131,76,77,78,55,49,91,92,101,102,103,109) 
     ) z

        group by z.mispar_ishi,z.taarich, z.bakasha_id) Z2
        where       p.mispar_ishi=z2.mispar_ishi
        -- and  (z2.Taarich BETWEEN  P.ME_TARICH  AND   NVL(P.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
          --           OR  last_day(z2.Taarich)  BETWEEN  P.ME_TARICH  AND   NVL(P.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
            --        OR  ( p.ME_TARICH>=z2.Taarich AND   NVL(P.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<= last_day(z2.Taarich)) )
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
                   --          and P.SNIF_AV in (82594 ,85407 ,88898 )
                             ),
           shabaton as 
               (select y.taarich ,Y.SUG_YOM,s.EREV_SHISHI_CHAG    erev_chag  
                from TB_YAMIM_MEYUCHADIM y, CTB_SUGEY_YAMIM_MEYUCHADIM s
                where Y.SUG_YOM=     S.SUG_YOM
                  and S.Shbaton=1
                  and s.pail=1
                  and Y.TAARICH  between  P_STARTDATE and P_ENDDATE
                  and Y.SUG_YOM not in( 27,29)  )  ,
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
        and trim(O.LEUM)='י'

          and S.MISPAR_ISHI = P.MISPAR_ISHI
          and S.TAARICH between P.ME_TARICH and P.AD_TARICH
          AND p.ezor       =  E.KOD_EZOR
          AND e.kod_hevra = o.kod_hevra
          
          ) s
          group by  S.TAARICH,s.KOD_EZOR,s.TEUR_EZOR)
         group by  TAARICH,YOM
         order by TAARICH ,YOM;
 
 
 end pro_cnt_ovdey_meshek_shabat;          

PROCEDURE  pro_get_kod_snif_av( P_KOD_EZOR in CTB_SNIF_AV.EZOR%type,
                                p_Cur OUT CurType )  IS
                                                     
BEGIN

open p_cur for
   SELECT a.Kod_Snif_Av KOD_SNIF_AV, a.Teur_Snif_Av  || ' (' ||  a.Kod_Snif_Av || ') '  || c.teur_hevra || ' ('  ||  b.kod_hevra || ')'    teur_snif_av
         FROM  CTB_SNIF_AV a, CTB_EZOR b, CTB_HEVRA c
         WHERE a.ezor=b.kod_ezor
                AND    (P_KOD_EZOR = -1 or a.EZOR =  P_KOD_EZOR) 
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
       ),
    details as(
     select  de.TAARICH, de.MISPAR_ISHI, de.id_rashemet ,  de.name_rashemet , de.snif_av, de.TEUR_SNIF_AV, de.Ezor,
             OvdimCount, friend,   salaried,  measher, 
           MISTAYEG, nofill, 
            case when  MISPAR_ISHI <>  nvl(lag(MISPAR_ISHI,1) over (partition by  MISPAR_ISHI,id_rashemet,SNIF_AV, EZOR order by  MISPAR_ISHI, id_rashemet, EZOR, SNIF_AV),0)
                 then tickets 
                 else 0 end tickets
  from(
    select distinct IR.MISPAR_ISHI, PO.SNIF_AV, S.TEUR_SNIF_AV,  O.SHEM_MISH ||' '||O.SHEM_PRAT name_rashemet , 
    IR.GOREM_MEADKEN id_rashemet, Po.EZOR, trunc(IR.TAARICH_IDKUN,'mm') TAARICH,ir.TAARICH TAARICH_card,
      (count(distinct IR.MISPAR_ISHI ) over (PARTITION BY IR.GOREM_MEADKEN, Po.EZOR,PO.SNIF_AV  )) OvdimCount,
       count(distinct ir.TAARICH) over (partition by  IR.MISPAR_ISHI,IR.GOREM_MEADKEN,PO.SNIF_AV, Po.EZOR) tickets,
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
                     ) po on IR.MISPAR_ISHI = Po.MISPAR_ISHI  and ir.TAARICH between Po.ME_TARICH and Po.AD_TARICH
          left join por on IR.GOREM_MEADKEN = Por.MISPAR_ISHI  and IR.TAARICH_IDKUN between Por.ME_TARICH and Por.AD_TARICH
         left join tb_yamey_avoda_ovdim y on IR.MISPAR_ISHI = Y.MISPAR_ISHI and IR.TAARICH = Y.TAARICH 
         left join ctb_snif_av s on PO.SNIF_AV = S.KOD_SNIF_AV 
    where (P_MIS_RASHEMET is null or IR.GOREM_MEADKEN  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MIS_RASHEMET,  ',') AS MYTABTYPE))))
            and (p_snif is null or PO.SNIF_AV in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_snif,  ',') AS MYTABTYPE))))
            and (p_ezor = -1 or PO.EZOR=p_ezor)
          and IR.TAARICH_IDKUN between P_STARTDATE and  P_ENDDATE
          and por.ISUK=133 
     order by IR.MISPAR_ISHI, IR.GOREM_MEADKEN, Po.EZOR, PO.SNIF_AV
       )de
     )
    select d.TAARICH, d.id_rashemet ,  d.name_rashemet , d.snif_av, d.TEUR_SNIF_AV, d.Ezor,
            max(d.OvdimCount) OvdimCount,sum(friend) friend,  sum(salaried) salaried, sum(measher) measher, 
           sum(MISTAYEG) MISTAYEG, sum(nofill) nofill,
              sum(tickets) tickets, 
             count(*) update_tickets,
            v_rechiv_109 rechiv_109 
    from details d 
    group by d.TAARICH, d.id_rashemet ,d.name_rashemet , d.Ezor, d.snif_av, d.TEUR_SNIF_AV;
   -- order by    d.id_rashemet ,  d.name_rashemet ,d.Ezor,  d.snif_av, d.TEUR_SNIF_AV;

else
    open p_cur for
       with por as
       (SELECT po.mispar_ishi,po.me_tarich,po.ad_tarich,Po.SNIF_AV,Po.EZOR,Po.YECHIDA_IRGUNIT,Po.ISUK, Po.MAAMAD
       FROM PIVOT_PIRTEY_OVDIM PO
       WHERE po.isuk=133
             AND (P_STARTDATE  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
               OR P_ENDDATE  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                OR   (po.ME_TARICH>=P_STARTDATE   AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=P_ENDDATE  ))
       ),
    details as(
      select  de.TAARICH, de.MISPAR_ISHI, de.id_rashemet ,  de.name_rashemet , de.snif_av, de.TEUR_SNIF_AV, de.Ezor,
             OvdimCount, friend,   salaried,  measher, 
           MISTAYEG, nofill, 
            case when  MISPAR_ISHI <>  nvl(lag(MISPAR_ISHI,1) over (partition by  MISPAR_ISHI,id_rashemet,SNIF_AV, EZOR order by  MISPAR_ISHI, id_rashemet, EZOR, SNIF_AV),0)
                 then tickets 
                 else 0 end tickets
  from(
    select distinct IR.MISPAR_ISHI, PO.SNIF_AV, S.TEUR_SNIF_AV,  O.SHEM_MISH ||' '||O.SHEM_PRAT name_rashemet , 
           IR.GOREM_MEADKEN id_rashemet, Po.EZOR, trunc(IR.TAARICH_IDKUN,'mm') TAARICH, ir.TAARICH TAARICH_card,
           (count(distinct IR.MISPAR_ISHI ) over (PARTITION BY IR.GOREM_MEADKEN, Po.EZOR,PO.SNIF_AV  )) OvdimCount,
              count(distinct ir.TAARICH) over (partition by  IR.MISPAR_ISHI,IR.GOREM_MEADKEN,PO.SNIF_AV, Po.EZOR) tickets,
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
                     ) po on IR.MISPAR_ISHI = Po.MISPAR_ISHI  and ir.TAARICH between Po.ME_TARICH and Po.AD_TARICH
          left join por on IR.GOREM_MEADKEN = Por.MISPAR_ISHI  and IR.TAARICH_IDKUN between Por.ME_TARICH and Por.AD_TARICH 
         left join tb_yamey_avoda_ovdim y on IR.MISPAR_ISHI = Y.MISPAR_ISHI and IR.TAARICH = Y.TAARICH 
         left join ctb_snif_av s on PO.SNIF_AV = S.KOD_SNIF_AV 
    where (P_MIS_RASHEMET is null or IR.GOREM_MEADKEN  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MIS_RASHEMET,  ',') AS MYTABTYPE))))
            and (p_snif is null or PO.SNIF_AV in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_snif,  ',') AS MYTABTYPE))))
            and (p_ezor = -1 or PO.EZOR=p_ezor)
          and IR.TAARICH_IDKUN between P_STARTDATE and  P_ENDDATE
          and por.ISUK=133 
           order by IR.MISPAR_ISHI, IR.GOREM_MEADKEN, Po.EZOR, PO.SNIF_AV
       )de
    )
    select d.TAARICH, d.id_rashemet ,  d.name_rashemet , d.snif_av, d.TEUR_SNIF_AV, d.Ezor, 
           (select count(*) from (  select  p.mispar_ishi,p.me_tarich,p.ad_tarich,P.SNIF_AV,P.EZOR,P.YECHIDA_IRGUNIT,P.ISUK, P.MAAMAD
                        from pivot_pirtey_ovdim p
                        where p.isuk IS NOT NULL
                     ) po where PO.SNIF_AV = d.snif_av) SnifOvdimCount, 
           max(d.OvdimCount) OvdimCount, v_rechiv_109 rechiv_109, 
           sum(friend) friend,  sum(salaried) salaried, sum(measher) measher, 
           sum(MISTAYEG) MISTAYEG, sum(nofill) nofill, 
            sum(tickets) tickets,    count(*) update_tickets
    from details d 
    group by d.TAARICH, d.Ezor, d.snif_av, d.TEUR_SNIF_AV, d.id_rashemet ,d.name_rashemet;
    --order by d.Ezor, d.snif_av, d.TEUR_SNIF_AV,  d.id_rashemet ,  d.name_rashemet;
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
    select distinct PO.SNIF_AV, S.TEUR_SNIF_AV,  O.SHEM_MISH ||' '||O.SHEM_PRAT name_rashemet , IR.MISPAR_ISHI,
    IR.GOREM_MEADKEN id_rashemet, Po.EZOR, ir.TAARICH TAARICH_card,
      (count(distinct IR.MISPAR_ISHI ) over (PARTITION BY trunc(IR.TAARICH_IDKUN,'mm'), IR.GOREM_MEADKEN, Po.EZOR,PO.SNIF_AV  )) OvdimCount,
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
                     ) po on IR.MISPAR_ISHI = Po.MISPAR_ISHI  and ir.TAARICH between Po.ME_TARICH and Po.AD_TARICH
          left join por on IR.GOREM_MEADKEN = Por.MISPAR_ISHI  and IR.TAARICH_IDKUN between Por.ME_TARICH and Por.AD_TARICH
         left join tb_yamey_avoda_ovdim y on IR.MISPAR_ISHI = Y.MISPAR_ISHI and IR.TAARICH = Y.TAARICH 
         left join ctb_snif_av s on PO.SNIF_AV = S.KOD_SNIF_AV 
    where (P_MIS_RASHEMET is null or IR.GOREM_MEADKEN  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MIS_RASHEMET,  ',') AS MYTABTYPE))))
            and (p_snif is null or PO.SNIF_AV in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_snif,  ',') AS MYTABTYPE))))
            and (p_ezor = -1 or PO.EZOR=p_ezor)   
          and IR.TAARICH_IDKUN between P_STARTDATE and  P_ENDDATE
          and por.ISUK=133;
  

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
  
    select distinct PO.SNIF_AV, S.TEUR_SNIF_AV,  O.SHEM_MISH ||' '||O.SHEM_PRAT name_rashemet , IR.MISPAR_ISHI,
           IR.GOREM_MEADKEN id_rashemet, Po.EZOR, ir.TAARICH TAARICH_card,
           (count(distinct IR.MISPAR_ISHI ) over (PARTITION BY trunc(IR.TAARICH_IDKUN,'mm'), IR.GOREM_MEADKEN, Po.EZOR,PO.SNIF_AV  )) OvdimCount,
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
                     ) po on IR.MISPAR_ISHI = Po.MISPAR_ISHI  and ir.TAARICH between Po.ME_TARICH and Po.AD_TARICH
          left join por on IR.GOREM_MEADKEN = Por.MISPAR_ISHI  and IR.TAARICH_IDKUN between Por.ME_TARICH and Por.AD_TARICH
         left join tb_yamey_avoda_ovdim y on IR.MISPAR_ISHI = Y.MISPAR_ISHI and IR.TAARICH = Y.TAARICH 
         left join ctb_snif_av s on PO.SNIF_AV = S.KOD_SNIF_AV 
    where (P_MIS_RASHEMET is null or IR.GOREM_MEADKEN  in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( P_MIS_RASHEMET,  ',') AS MYTABTYPE))))
            and (p_snif is null or PO.SNIF_AV in (SELECT X FROM TABLE(CAST(Convert_String_To_Table( p_snif,  ',') AS MYTABTYPE))))
            and (p_ezor = -1 or PO.EZOR=p_ezor)
          and IR.TAARICH_IDKUN between P_STARTDATE and  P_ENDDATE
          and por.ISUK=133;
  
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
          select   sum(case when c.kod_rechiv=109 then c.erech_rechiv else 0 end) rechiv_109
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
           ( SELECT  co.mispar_ishi, co.Taarich, co.bakasha_id,b.TAARICH_HAAVARA_LESACHAR th1,
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
             from(select  c.mispar_ishi,c.taarich,c.bakasha_id,C.MISPAR_SIDUR,C.SHAT_HATCHALA, c.kod_rechiv,c.erech_rechiv--,c.mispar_sidur,substr(lpad(c.mispar_sidur,5,'0'),1,2) snif_tnua
                  from  TB_CHISHUV_sidur_OVDIM c,bakasha b
                  where c.mispar_ishi=b.mispar_ishi
                        and c.taarich between b.taarich and last_day(b.taarich) 
                         and c.bakasha_id=b.bakasha_id
                         and c.kod_rechiv in(1,5)
                   ) z
             group by z.mispar_ishi,z.taarich, z.bakasha_id, 
                      z.MISPAR_SIDUR,z.SHAT_HATCHALA)
  select distinct PO.MISPAR_ISHI, O.SHEM_MISH || ' '|| O.SHEM_PRAT full_name, P.MAAMAD, P.GIL , P.EZOR, P.SNIF_AV, 
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
            

END pro_get_tigburim_details;

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


PROCEDURE  pro_get_Report_Details(p_shem_doch IN  CTB_SUGEY_DOCHOT.SHEM_DOCH_BAKOD%TYPE, p_cur OUT CurType) AS 
BEGIN 
     OPEN p_cur FOR          
                 SELECT DISTINCT C.TEUR_DOCH as PageHeader, c.TEUR_DOCH, C.RS_VERSION as RS_VERSION, R.EXTENSION, r.URL_CONFIG_KEY, r.SERVICE_URL_CONFIG_KEY
               FROM   CTB_SUGEY_DOCHOT c inner join  CTB_DOCHOT_RS_VERSION r on  C.RS_VERSION = r.RS_VERSION and 
                C.SHEM_DOCH_BAKOD = p_shem_doch;
 EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END         pro_get_Report_Details;  
END Pkg_Reports;
/


CREATE OR REPLACE PACKAGE BODY          Pkg_Sidurim AS

  PROCEDURE pro_get_data(p_Kod IN VARCHAR2,p_Period IN VARCHAR2,p_Cur OUT CurType) IS
  v_MaxLimitDate DATE ;
  v_MinLimitDate DATE ;
  BEGIN
      v_MinLimitDate := TO_DATE('01/' || p_Period,'dd/mm/yyyy'); /* period= 05/2009=>  v_MinLimitDate = 01/05/2009 */
    v_MaxLimitDate := ADD_MONTHS(v_MinLimitDate,1) -1 ;    /* period= 05/2009=>  v_MaxLimitDate = 31/05/2009 */


          OPEN p_Cur FOR
          SELECT tb.MISPAR_SIDUR, ctb.Kod_Meafyen,(ctb.Kod_Meafyen||'-'|| ctb.shem_meafyen || '(' || ctb.SUG_NATUN || ')')DetailsMeafyen ,tb.Me_Taarich,tb.Ad_Taarich,Erech, ctb.sug_natun,tb.taarich_idkun_acharon LastUpdate,TB.HEARA
          FROM TB_SIDURIM_MEYUCHADIM tb      INNER JOIN CTB_MEAFYENEY_SIDURIM ctb
          ON ctb.kod_meafyen= tb.kod_meafyen
          WHERE ((tb.Me_Taarich <=  v_MaxLimitDate ) AND (tb.Ad_Taarich >= v_MinLimitDate OR tb.Ad_Taarich IS NULL ))
          AND tb.MISPAR_SIDUR = p_Kod AND ctb.PAIL = 1
          ORDER BY   ctb.Kod_Meafyen ;
    EXCEPTION
        WHEN OTHERS THEN
            RAISE;
  END pro_get_data;

  PROCEDURE pro_get_matching_description(p_Prefix IN VARCHAR2,  p_Cur OUT CurType) AS
  BEGIN
  OPEN p_Cur FOR
      SELECT TEUR_SIDUR_MEYCHAD FROM CTB_SIDURIM_MEYUCHADIM SM
      WHERE SM.TEUR_SIDUR_MEYCHAD   LIKE  p_Prefix || '%' AND pail =1
      ORDER BY TEUR_SIDUR_MEYCHAD ASC;
    NULL;
  END pro_get_matching_description;


  PROCEDURE pro_get_matching_kod(p_Prefix IN VARCHAR2,  p_Cur OUT CurType) AS
  BEGIN
  OPEN p_Cur FOR
      SELECT KOD_SIDUR_MEYUCHAD
      FROM CTB_SIDURIM_MEYUCHADIM SM
      WHERE SM.KOD_SIDUR_MEYUCHAD   LIKE  p_Prefix || '%' AND pail =1
      ORDER BY KOD_SIDUR_MEYUCHAD ASC ;
    END pro_get_matching_kod;

PROCEDURE pro_get_history(p_FilterKod IN VARCHAR2, p_Kod IN VARCHAR2,p_ToDate IN VARCHAR2, p_Cur OUT Curtype) IS
  v_tar_me DATE ;
  BEGIN
      v_tar_me:=TO_DATE(p_ToDate,'dd/mm/yyyy');
  OPEN p_Cur FOR
      SELECT SM.ME_TAARICH,SM.ad_taarich,SM.erech
      FROM TB_SIDURIM_MEYUCHADIM SM
      WHERE SM.kod_meafyen =p_Kod AND SM.AD_TAARICH< v_tar_me AND MISPAR_SIDUR = p_filterkod
      ORDER BY SM.ME_TAARICH ASC ;

END pro_get_history;

PROCEDURE pro_get_description_by_kod(p_Kod IN VARCHAR2,p_Desc OUT VARCHAR2) IS
  BEGIN

        SELECT  CASE WHEN ( pail = 1)  THEN SM.teur_sidur_meychad
                        ELSE '-1' END  INTO p_Desc
        FROM CTB_SIDURIM_MEYUCHADIM SM
        WHERE SM.KOD_SIDUR_MEYUCHAD = p_Kod ;
    EXCEPTION
       WHEN NO_DATA_FOUND THEN
       p_Desc := '';
    END pro_get_description_by_kod;

  PROCEDURE pro_get_kod_by_description(p_Desc IN CTB_SIDURIM_MEYUCHADIM.TEUR_SIDUR_MEYCHAD%TYPE  ,p_Kod OUT CTB_SIDURIM_MEYUCHADIM.KOD_SIDUR_MEYUCHAD%TYPE) AS
  BEGIN
          SELECT  CASE WHEN ( pail = 1)  THEN SM.KOD_SIDUR_MEYUCHAD
                        ELSE -1 END  INTO p_Kod
        FROM CTB_SIDURIM_MEYUCHADIM SM
        WHERE SM.teur_sidur_meychad = p_Desc ;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
        p_Kod := '';
    END pro_get_kod_by_description;

PROCEDURE pro_get_details_Kod(p_Cur OUT CurType ) AS
BEGIN
  OPEN p_Cur FOR
      SELECT  SM.KOD_MEAFYEN Kod_Meafyen , ('-' || SM.KOD_MEAFYEN  || SM.SHEM_MEAFYEN || '(' || sm.SUG_NATUN || ')' ) DetailsMeafyen
      FROM CTB_MEAFYENEY_SIDURIM SM
      WHERE SM.PAIL = 1 AND (SIDUR_MEYUCHAD_OR_SUG_SIDUR = 0 OR SIDUR_MEYUCHAD_OR_SUG_SIDUR = 2)
      ORDER BY Kod_Meafyen ASC ;
END pro_get_details_Kod;

  PROCEDURE pro_upd_data(p_KodMeafyen NUMBER,p_MisparSidur NUMBER,p_MeTaarich DATE,p_AdTaarich DATE,p_Erech VARCHAR2 ,p_SugNatun VARCHAR2,p_comment VARCHAR2, p_taarich_idkun_acharon DATE,p_meadken_acharon NUMBER) AS
  BEGIN
    UPDATE TB_SIDURIM_MEYUCHADIM SM
    SET SM.ERECH = p_Erech ,
        SM.AD_TAARICH = p_AdTaarich ,
          SM.TAARICH_IDKUN_ACHARON = p_taarich_idkun_acharon,
          SM.MEADKEN_ACHARON = p_meadken_acharon,
          SM.HEARA = p_comment
    WHERE MISPAR_SIDUR = p_MisparSidur AND
    ME_TAARICH = p_MeTaarich AND
    KOD_MEAFYEN = p_KodMeafyen;
  END pro_upd_data;

PROCEDURE pro_ins_data(p_KodMeafyen NUMBER,p_MisparSidur NUMBER,p_MeTaarich DATE,p_AdTaarich DATE,p_Erech VARCHAR2 ,p_SugNatun VARCHAR2,p_comment VARCHAR2, p_taarich_idkun_acharon DATE,p_meadken_acharon NUMBER) AS
  BEGIN
  INSERT INTO TB_SIDURIM_MEYUCHADIM
    (ad_taarich, erech, kod_meafyen, me_taarich, meadken_acharon, mispar_sidur,heara)
    VALUES (p_adtaarich, p_erech, p_kodmeafyen, p_metaarich, p_meadken_acharon, p_misparsidur,p_comment);
  END pro_ins_data;

      PROCEDURE pro_Del_data(p_KodMeafyen NUMBER,p_MisparSidur NUMBER,p_MeTaarich DATE)  AS
  BEGIN
      DELETE TB_SIDURIM_MEYUCHADIM
    WHERE MISPAR_SIDUR = p_MisparSidur AND
    ME_TAARICH = p_MeTaarich AND
    KOD_MEAFYEN = p_KodMeafyen;

  END pro_Del_data;



    /*   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0       06/07/2009      sari       1. פונקציה המחזירה סידורים מיוחדים*/
PROCEDURE get_sidurim_meyuchadim_all(p_tar_me IN TB_SIDURIM_MEYUCHADIM.me_taarich%TYPE,
                                                                                                p_tar_ad  IN TB_SIDURIM_MEYUCHADIM.me_taarich%TYPE,
                                                                                         p_Cur OUT CurType) AS
BEGIN
     OPEN p_cur FOR
           SELECT tb.MISPAR_SIDUR,tb.Kod_Meafyen,tb.Me_Taarich,NVL(tb.Ad_Taarich,TO_DATE('01/01/9999','dd/mm/yyyy')) ad_taarich,Erech
        FROM TB_SIDURIM_MEYUCHADIM tb
        WHERE  tb.Me_Taarich <=p_tar_me
        AND NVL(tb.Ad_Taarich,TO_DATE('01/01/9999','dd/mm/yyyy'))  >=p_tar_ad;

EXCEPTION
       WHEN OTHERS THEN
                RAISE;
  END get_sidurim_meyuchadim_all;

PROCEDURE get_sidur(p_sidur_position IN VARCHAR2,  p_mispar_ishi IN TB_SIDURIM_OVDIM.MISPAR_ISHI%TYPE,p_taarich  IN TB_SIDURIM_OVDIM.TAARICH%TYPE, p_Cur OUT CurType)
AS
shat_hatchala DATE;
BEGIN
DBMS_APPLICATION_INFO.SET_MODULE('pkg_sidurim.get_sidur','get pirtey sidur leoved letaarich');
           IF p_sidur_position is not NULL THEN
                           IF p_sidur_position = 'first' THEN

                              OPEN p_cur FOR
                                    with sh as
                                        (SELECT MIN(tbs.SHAT_HATCHALA)  SHAT_HATCHALA
                                         FROM TB_SIDURIM_OVDIM tbs
                                         WHERE tbs.TAARICH = p_taarich
                                              AND tbs.mispar_sidur<>99200
                                              AND tbs.MISPAR_ISHI = p_mispar_ishi
                                              AND tbs.lo_letashlum=0)
                                  SELECT tb.*
                                  FROM TB_SIDURIM_OVDIM tb,sh
                                  WHERE  tb.TAARICH = p_taarich
                                        AND tb.mispar_sidur<>99200
                                        AND tb.MISPAR_ISHI = p_mispar_ishi
                                        AND tb.SHAT_HATCHALA =sh.SHAT_HATCHALA
                                        AND tb.lo_letashlum=0;

                       ELSIF p_sidur_position = 'last' THEN

                                 OPEN p_cur FOR
                                    with sh as
                                        (SELECT MAX(tbs.SHAT_HATCHALA)  SHAT_HATCHALA
                                         FROM TB_SIDURIM_OVDIM tbs
                                         WHERE tbs.TAARICH = p_taarich
                                              AND tbs.mispar_sidur<>99200
                                              AND tbs.MISPAR_ISHI = p_mispar_ishi
                                              AND tbs.lo_letashlum=0)
                                  SELECT tb.*
                                  FROM TB_SIDURIM_OVDIM tb,sh
                                  WHERE  tb.TAARICH = p_taarich
                                        AND tb.mispar_sidur<>99200
                                        AND tb.MISPAR_ISHI = p_mispar_ishi
                                        AND tb.SHAT_HATCHALA =sh.SHAT_HATCHALA
                                        AND tb.lo_letashlum=0;
                     END IF;

          ELSE

                         OPEN p_cur FOR
                         SELECT *
                       FROM TB_SIDURIM_OVDIM tb
                       WHERE  tb.Taarich = TO_DATE(TO_CHAR(p_taarich, 'dd/mm/yyyy'), 'dd/mm/yyyy')
                       AND tb.mispar_sidur<>99200
                                  AND tb.MISPAR_ISHI = p_mispar_ishi
                                  AND tb.lo_letashlum=0;
                            --      AND (tb.BITUL_O_HOSAFA=2 OR   tb.BITUL_O_HOSAFA=4);
          END IF;

EXCEPTION
       WHEN OTHERS THEN
                RAISE;
  END get_sidur;

  PROCEDURE pro_get_ctb_sidurim_meafyen(p_taarich IN PIVOT_SIDURIM_MEYUCHADIM.AD_TARICH%TYPE,  p_cur OUT CurType)
IS
BEGIN
     OPEN p_cur FOR
     SELECT  c.kod_sidur_meyuchad,c.teur_sidur_meychad ,s.sidur_misug_headrut,s.shat_hatchala_muteret,s.shat_gmar_muteret,
    s.nitan_ledaveach_ad_taarich,s.headrut_hova_ledaveach_shaot,s.rashai_ledaveach 
        FROM PIVOT_SIDURIM_MEYUCHADIM s ,CTB_SIDURIM_MEYUCHADIM c
        WHERE s.mispar_sidur=c.kod_sidur_meyuchad
        AND  s.Me_Tarich <=p_taarich
        AND C.PAIL='1'
        AND NVL(s.AD_TARICH,TO_DATE('01/01/9999','dd/mm/yyyy'))  >=p_taarich
        ORDER BY c.teur_sidur_meychad ;

EXCEPTION
        WHEN OTHERS THEN
        RAISE;
END pro_get_ctb_sidurim_meafyen;

  PROCEDURE pro_get_sidurim_leoved(p_mispar_ishi IN TB_SIDURIM_OVDIM.mispar_ishi%TYPE,
                                                                                   p_taarich IN  TB_SIDURIM_OVDIM.taarich%TYPE,
                                                                                p_cur OUT CurType) AS
  BEGIN
        OPEN p_cur  FOR
                     SELECT mispar_sidur,shat_hatchala,shat_gmar,NVL(kod_siba_lo_letashlum,0)kod_siba_lo_letashlum,NVL(Lo_letashlum,0) Lo_letashlum,NVL(Bitul_O_Hosafa,0) Bitul_O_Hosafa FROM TB_SIDURIM_OVDIM
                    WHERE mispar_ishi=p_mispar_ishi
                    AND taarich =p_taarich;

        EXCEPTION
        WHEN OTHERS THEN
        RAISE;
    END pro_get_sidurim_leoved;

PROCEDURE pro_ins_upd_sidurim_ovdim(p_mispar_ishi IN TB_SIDURIM_OVDIM.mispar_ishi%TYPE,
                                                                                       p_mispar_sidur IN TB_SIDURIM_OVDIM.mispar_sidur%TYPE,
                                                                                     p_taarich IN  TB_SIDURIM_OVDIM.taarich%TYPE,
                                                                                  p_shat_hatchala IN TB_SIDURIM_OVDIM.shat_hatchala%TYPE,
                                                                                    p_shat_gmar IN TB_SIDURIM_OVDIM.shat_gmar%TYPE,
                                                                                p_user_upd  IN TB_SIDURIM_OVDIM.MEADKEN_ACHARON%TYPE)       AS
             v_count NUMBER;
    BEGIN
          v_count:=0;
           SELECT COUNT(*)  INTO v_count
             FROM      TB_SIDURIM_OVDIM
             WHERE mispar_ishi=p_mispar_ishi
            AND taarich =TRUNC(p_taarich)
            AND mispar_sidur=p_mispar_sidur
            AND shat_hatchala=p_shat_hatchala;

             IF (v_count=0) THEN
                       INSERT INTO TB_SIDURIM_OVDIM
                       (mispar_ishi,mispar_sidur,taarich,shat_hatchala,shat_gmar,meadken_acharon,Taarich_idkun_acharon)
                       VALUES(p_mispar_ishi,p_mispar_sidur,TRUNC(p_taarich),p_shat_hatchala,p_shat_gmar ,p_user_upd,SYSDATE);
                       
                   
            ELSE
                     INSERT INTO TRAIL_SIDURIM_OVDIM (mispar_ishi,mispar_sidur,taarich,
                                            shat_hatchala,shat_gmar,shat_hatchala_letashlum,shat_gmar_letashlum,
                                            pitzul_hafsaka,chariga,tosefet_grira,hashlama,yom_visa,
                                            lo_letashlum,out_michsa,mikum_shaon_knisa,mikum_shaon_yetzia,
                                            achuz_knas_lepremyat_visa,achuz_viza_besikun,mispar_musach_o_machsan,
                                            kod_siba_lo_letashlum,kod_siba_ledivuch_yadani_in,kod_siba_ledivuch_yadani_out,
                                            meadken_acharon,taarich_idkun_acharon,heara,shayah_leyom_kodem,mispar_shiurey_nehiga,
                                            mispar_ishi_trail,taarich_idkun_trail,sug_peula,mezake_halbasha,mezake_nesiot)
                                        SELECT mispar_ishi,mispar_sidur,taarich,
                                              shat_hatchala,shat_gmar,shat_hatchala_letashlum,shat_gmar_letashlum,
                                              pitzul_hafsaka,chariga,tosefet_grira,hashlama,yom_visa,
                                              lo_letashlum,out_michsa,mikum_shaon_knisa,mikum_shaon_yetzia,
                                              achuz_knas_lepremyat_visa,achuz_viza_besikun,mispar_musach_o_machsan,
                                              kod_siba_lo_letashlum,kod_siba_ledivuch_yadani_in,kod_siba_ledivuch_yadani_out,
                                              meadken_acharon,taarich_idkun_acharon,heara,shayah_leyom_kodem,mispar_shiurey_nehiga,
                                              p_user_upd,SYSDATE,3, mezake_halbasha,mezake_nesiot
                                        FROM TB_SIDURIM_OVDIM
                                        WHERE mispar_ishi  =p_mispar_ishi    AND
                                             mispar_sidur  = p_mispar_sidur  AND
                                             taarich       = TRUNC(p_taarich) AND
                                             shat_hatchala = p_shat_hatchala;


                            UPDATE  TB_SIDURIM_OVDIM
                            SET shat_hatchala=p_shat_hatchala,
                            shat_gmar=p_shat_gmar,
                            meadken_acharon=p_user_upd,
                            Taarich_idkun_acharon=SYSDATE
                            WHERE mispar_ishi=p_mispar_ishi
                            AND taarich =TRUNC(p_taarich)
                            AND mispar_sidur=p_mispar_sidur
                            AND shat_hatchala=p_shat_hatchala;
                            
               
            END IF;
            IF ((p_user_upd<> p_mispar_ishi) AND (p_user_upd>0)) THEN
                    Pkg_Utils.pro_insert_meadken_acharon(p_mispar_ishi,p_taarich);
            END IF; 
        
            EXCEPTION
        WHEN OTHERS THEN
        RAISE;
    END pro_ins_upd_sidurim_ovdim;


PROCEDURE pro_ins_upd_sidurim_ovdim(p_mispar_ishi IN tb_sidurim_ovdim.mispar_ishi%type,
                                                            p_mispar_sidur IN tb_sidurim_ovdim.mispar_sidur%type,
                                                            p_taarich IN  tb_sidurim_ovdim.taarich%type,
                                                            p_shat_hatchala IN tb_sidurim_ovdim.shat_hatchala%type,
                                                            p_shat_gmar IN tb_sidurim_ovdim.shat_gmar%type,
                                                            p_user_upd  IN tb_sidurim_ovdim.MEADKEN_ACHARON%type,
                                                            p_mikum_shaon_knisa IN tb_sidurim_ovdim.mikum_shaon_knisa%type,
                                                            p_mikum_shaon_yetzia IN tb_sidurim_ovdim.mikum_shaon_yetzia%type,
                                                            p_course_num in tb_sidurim_ovdim.course_num%type) IS

     v_count NUMBER;
    BEGIN
          v_count:=0;
           SELECT COUNT(*)  INTO v_count
             FROM      TB_SIDURIM_OVDIM
             WHERE mispar_ishi=p_mispar_ishi
            AND taarich =TRUNC(p_taarich)
            AND mispar_sidur=p_mispar_sidur
            AND shat_hatchala=p_shat_hatchala;

             IF (v_count=0) THEN
                       INSERT INTO TB_SIDURIM_OVDIM
                       (mispar_ishi,mispar_sidur,taarich,shat_hatchala,shat_gmar,meadken_acharon,Taarich_idkun_acharon,mikum_shaon_knisa,mikum_shaon_yetzia,course_num)
                       VALUES(p_mispar_ishi,p_mispar_sidur,TRUNC(p_taarich),p_shat_hatchala,p_shat_gmar ,p_user_upd,SYSDATE,p_mikum_shaon_knisa,p_mikum_shaon_yetzia,p_course_num);
                       
                   
            ELSE
                     INSERT INTO TRAIL_SIDURIM_OVDIM (mispar_ishi,mispar_sidur,taarich,
                                            shat_hatchala,shat_gmar,shat_hatchala_letashlum,shat_gmar_letashlum,
                                            pitzul_hafsaka,chariga,tosefet_grira,hashlama,yom_visa,
                                            lo_letashlum,out_michsa,mikum_shaon_knisa,mikum_shaon_yetzia,
                                            achuz_knas_lepremyat_visa,achuz_viza_besikun,mispar_musach_o_machsan,
                                            kod_siba_lo_letashlum,kod_siba_ledivuch_yadani_in,kod_siba_ledivuch_yadani_out,
                                            meadken_acharon,taarich_idkun_acharon,heara,shayah_leyom_kodem,mispar_shiurey_nehiga,
                                            mispar_ishi_trail,taarich_idkun_trail,sug_peula,mezake_halbasha,mezake_nesiot)
                                        SELECT mispar_ishi,mispar_sidur,taarich,
                                              shat_hatchala,shat_gmar,shat_hatchala_letashlum,shat_gmar_letashlum,
                                              pitzul_hafsaka,chariga,tosefet_grira,hashlama,yom_visa,
                                              lo_letashlum,out_michsa,mikum_shaon_knisa,mikum_shaon_yetzia,
                                              achuz_knas_lepremyat_visa,achuz_viza_besikun,mispar_musach_o_machsan,
                                              kod_siba_lo_letashlum,kod_siba_ledivuch_yadani_in,kod_siba_ledivuch_yadani_out,
                                              meadken_acharon,taarich_idkun_acharon,heara,shayah_leyom_kodem,mispar_shiurey_nehiga,
                                              p_user_upd,SYSDATE,3, mezake_halbasha,mezake_nesiot
                                        FROM TB_SIDURIM_OVDIM
                                        WHERE mispar_ishi  =p_mispar_ishi    AND
                                             mispar_sidur  = p_mispar_sidur  AND
                                             taarich       = TRUNC(p_taarich) AND
                                             shat_hatchala = p_shat_hatchala;


                            UPDATE  TB_SIDURIM_OVDIM
                            SET shat_hatchala=p_shat_hatchala,
                            shat_gmar=p_shat_gmar,
                            meadken_acharon=p_user_upd,
                            Taarich_idkun_acharon=SYSDATE,
                            mikum_shaon_yetzia = p_mikum_shaon_yetzia,
                            course_num = p_course_num
                            WHERE mispar_ishi=p_mispar_ishi
                            AND taarich =TRUNC(p_taarich)
                            AND mispar_sidur=p_mispar_sidur
                            AND shat_hatchala=p_shat_hatchala;
                            
               
            END IF;
            IF ((p_user_upd<> p_mispar_ishi) AND (p_user_upd>0)) THEN
                    Pkg_Utils.pro_insert_meadken_acharon(p_mispar_ishi,p_taarich);
            END IF; 
        
            EXCEPTION
        WHEN OTHERS THEN
        RAISE;                                                            
END pro_ins_upd_sidurim_ovdim;                                                            
PROCEDURE pro_get_meafyen_sidur_by_kod(P_Kod_Sidur IN INTEGER,
                                                                                                          P_Taarich IN VARCHAR2,
                                                                                                      p_Cur OUT CurType) IS
              v_tar DATE ;
BEGIN
 v_tar:=TO_DATE(P_Taarich,'dd/mm/yyyy');
      OPEN p_Cur FOR
            SELECT  S.KOD_MEAFYEN,S.ERECH
            FROM TB_SIDURIM_MEYUCHADIM S
            WHERE
                             S.MISPAR_SIDUR = P_Kod_Sidur AND
                            v_tar BETWEEN S.ME_TAARICH AND S.AD_TAARICH ;
EXCEPTION
    WHEN OTHERS THEN
        RAISE;
END pro_get_meafyen_sidur_by_kod;

 PROCEDURE pro_get_teur_whithoutlist(p_Prefix IN VARCHAR2 , whithOutList IN VARCHAR2 ,
                                                                     whithOutListMisSidur IN VARCHAR2 ,
                                                                                     p_Cur OUT CurType) AS
  BEGIN
  OPEN p_Cur FOR
                   SELECT DISTINCT(SM.TEUR_SIDUR_MEYCHAD)
              FROM CTB_SIDURIM_MEYUCHADIM SM
              WHERE   SM.TEUR_SIDUR_MEYCHAD   LIKE  p_Prefix || '%'  AND
                                    SM.PAIL=1 AND
                                SM.KOD_SIDUR_MEYUCHAD NOT IN( SELECT x FROM TABLE(CAST(Convert_String_To_Table(  whithOutListMisSidur,  ',') AS mytabtype)) )  AND
                                --SM.KOD_SIDUR_MEYUCHAD NOT  IN() AND
                                    SM.KOD_SIDUR_MEYUCHAD NOT  IN(
                                                                            SELECT DISTINCT( t.MISPAR_SIDUR)
                                                                   FROM TB_SIDURIM_MEYUCHADIM t
                                                                   WHERE t.KOD_MEAFYEN IN( SELECT x FROM TABLE(CAST(Convert_String_To_Table(whithOutList ,  ',') AS mytabtype)) )
                                                                   )
      ORDER BY SM.TEUR_SIDUR_MEYCHAD ASC;

   EXCEPTION
    WHEN OTHERS THEN
        RAISE;
 END pro_get_teur_whithoutlist;

PROCEDURE pro_get_kod_whithoutlist(p_Prefix IN VARCHAR2 , whithOutList IN VARCHAR2 ,
                                                                                               whithOutListMisSidur IN VARCHAR2 ,
                                                                                               p_Cur OUT CurType) AS
  BEGIN
  OPEN p_Cur FOR
                SELECT DISTINCT(SM.KOD_SIDUR_MEYUCHAD)
              FROM CTB_SIDURIM_MEYUCHADIM SM
              WHERE   SM.KOD_SIDUR_MEYUCHAD   LIKE  p_Prefix || '%'  AND
                                      SM.PAIL=1 AND
                                  SM.KOD_SIDUR_MEYUCHAD NOT IN( SELECT x FROM TABLE(CAST(Convert_String_To_Table(  whithOutListMisSidur,  ',') AS mytabtype)) )  AND
                            --      SM.KOD_SIDUR_MEYUCHAD not in(whithOutListMisSidur) AND
                                        SM.KOD_SIDUR_MEYUCHAD NOT  IN(
                                                                            SELECT DISTINCT( t.MISPAR_SIDUR)
                                                                   FROM TB_SIDURIM_MEYUCHADIM t
                                                                   WHERE t.KOD_MEAFYEN IN( SELECT x FROM TABLE(CAST(Convert_String_To_Table(whithOutList ,  ',') AS mytabtype)) ))
                    ORDER BY     SM.KOD_SIDUR_MEYUCHAD;

   EXCEPTION
    WHEN OTHERS THEN
        RAISE;
 END pro_get_kod_whithoutlist;

 PROCEDURE pro_get_meafyen_sidur_ragil(P_Sug_Sidur IN INTEGER,
                                                                                                 P_Taarich IN VARCHAR2,
                                                                                                      p_Cur OUT CurType) IS
              v_tar DATE ;
BEGIN
 v_tar:=TO_DATE(P_Taarich,'dd/mm/yyyy');
      OPEN p_Cur FOR
            SELECT  M.KOD_MEAFYEN,M.ERECH
            FROM  TB_MEAFYENEY_SUG_SIDUR M
            WHERE
                            M.SUG_SIDUR = P_Sug_Sidur AND
                            v_tar BETWEEN M.ME_TAARICH AND M.AD_TAARICH ;
EXCEPTION
    WHEN OTHERS THEN
        RAISE;
END pro_get_meafyen_sidur_ragil;

PROCEDURE calling_Pivot_Sidurim_M IS
 --    v_idkun_tb     date;
  --   v_idkun_tmp   date;
BEGIN
--select max(nvl(m.taarich_idkun_acharon,sysdate)) idkun_tb,
--max(nvl(t.taarich_idkun_acharon,sysdate)) idkun_tmp
--into v_idkun_tb, v_idkun_tmp
--from tb_sidurim_meyuchadim m, tmp_sidurim_meyuchadim t;

--if (v_idkun_tb > v_idkun_tmp) then
DELETE FROM   PIVOT_SIDURIM_MEYUCHADIM;
 Pkg_Sidurim.pro_Pivot_Sidurim_Meyuchadim;
--end if;
COMMIT;
EXCEPTION
        WHEN OTHERS THEN
            RAISE;
END calling_Pivot_Sidurim_M;

PROCEDURE pro_Pivot_Sidurim_Meyuchadim IS
CURSOR Sidurim_tmp IS
  SELECT  DISTINCT  mispar_sidur
FROM   TB_SIDURIM_MEYUCHADIM
 -- where   mispar_sidur <99813 --between 63473 and 77690
;

CURSOR Me_tarich_tmp(par_sidur NUMBER) IS
SELECT DISTINCT me_tarich FROM
(SELECT DISTINCT p1.me_taarich  me_tarich
FROM   TB_SIDURIM_MEYUCHADIM  p1
    WHERE  p1.mispar_sidur=par_sidur
    UNION ALL
SELECT DISTINCT   p2.ad_taarich+1 me_tarich
--distinct  nvl(p2.ad_taarich,to_date('31/12/2100','dd/mm/yyyy'))+1 me_tarich
    FROM   TB_SIDURIM_MEYUCHADIM p2
    WHERE  p2.mispar_sidur=par_sidur
    AND p2.ad_taarich<TO_DATE('31/12/4712','dd/mm/yyyy')
    AND   p2.ad_taarich IS NOT NULL
  -- -- and p2.ad_taarich<to_date('31/12/2100','dd/mm/yyyy')
   -- and decode(p2.ad_taarich,to_date('31/12/4712','dd/mm/yyyy'),null, to_date('31/12/2100','dd/mm/yyyy'),null,   p2.ad_taarich) is not null
    --  and not exists( select * from tb_sidurim_meyuchadim p3 where  p3.mispar_sidur=par_sidur
       --                                       and   p2.ad_taarich+1=p3.me_taarich
    --                             and  p2.mispar_sidur= p3.mispar_sidur);
    )
    ORDER BY me_tarich;


CURSOR Me_Ad_tmp(par_sidur NUMBER) IS
SELECT me_tarich,NVL(ad_tarich,TO_DATE('31/12/4712','dd/mm/yyyy')) ad_tarich
FROM    PIVOT_SIDURIM_MEYUCHADIM
WHERE mispar_sidur=par_sidur;

CURSOR sidurim_meyuchadim_tmp(par_sidur NUMBER,p_Dt_from DATE, p_Dt_to DATE) IS
SELECT
  mispar_sidur,
  teur, Sug_sidur     ,sector_avoda     ,sug_premia, mofia_bdoch_nochachut_minhal_u, headrut_hova_ledaveach_shaot,
  shat_hatchala_muteret,
  shat_gmar_muteret,
  hukiyut_beshabaton ,hokiyut_beshishi ,
  sug_hashaot_beyom_shishi,     sug_hashaot_beyom_shabaton, hova_ledaveach_peilut,
  zakay_leaman_nesia,zakay_lezman_halbasha,  max_shaot_byom_hol     ,
  sug_shaot_byom_hol_if_migbala,     max_shaot_byom_shishi     ,max_shaot_beshabaton     ,
  asur_2_peiluyot_meoto_sug,hashlama_lemin_shaot_imahu,
  mutar_lebitzua_rak_lemeafyen_x, mutar_lebitzua_rak_lerishyon,avar_hadracha_meyuchedet    ,
  zakay_michutz_lamichsa,  zakay_lepizul      ,
  nitan_ledaveach_ad_taarich,
  kod_tipul_meyuchad_Baklita,     kod_tipul_meyuchad_Bashguim,     kod_tipul_meyuchad_Bachishuv,
  asur_ledivuach_lechevrot_bat    , zakay_lehamara         ,kod_headrut         ,zakay_lelina     ,
  zakay_lechariga,
  hova_ledaveach_mispar_machsan,hova_ledaveach_mispar_musach,
  hova_yom_visa_namlak ,
  asur_divuach_simun_tachograf,     zakay_lehashlama_avur_sidur ,zakay_lechishuv_retzifut     ,
  metagber_benihul_tnua,asur_ledaveach_mispar_rechev,
  zarich_ledaveach_km,sidur_namlak_visa,sidur_asur_ledaveach_peilut,
  avodat_musach    ,zakaut_lepizul_nehagut    ,zakaut_legmul_chisachon,
  sug_avoda,sidur_misug_headrut, shaon_nochachut,ein_leshalem_tos_lila,max_dakot_boded,
   dakot_n_letashlum_hol,sidur_kaytanot_veruey_kayiz,sidur_lo_nivdak_sofash,
  lo_letashlum_automati,kizuz_al_pi_hatchala_gmar,Michsat_shaot_chodshit,
       zakaut_lenosafot    ,   lo_pogea_bemichsat_nosafot,   rechiv_lezvira_yomit,
   rechiv_lezvira_chodshit,   max_dakot_nosafot_bechodesh,   zakaut_tamritz_nosafot_minhal,
     michsat_shishi_lebaley_x,  shabaton_tashlum_kavua,       max_yemey_avoda_bechodesh,
     kod_ishur_WF    ,       sidur_mishtatel_bechafifa,  tashlum_kavua_beshishi,       lo_zakay_legmul_beshishi,
      lo_zakay_legmul_beshabaton,           sug_shaot_nosafot    ,  musachey_mishmeret2,
      asur_leshanot_zman_peilut,   lo_nidreshet_hityazvut,   zakaut_lerezifut_benahagut,
      zakaut_letamriz_nos_nehagut,       tokef_hatchala    ,       tokef_siyum        ,   hovat_hityazvut    ,   
      lidrosh_kod_mivtza ,mofia_bedoch_noch_prem    ,avodat_meshek,
      tashlum_kavua_bchol    ,shat_hatchala_auto,shat_gmar_auto, sidur_lo_chosem,realy_veod_yom,sidur_rak_lechevrot_banot ,
      element1_hova    ,      element2_hova    ,   element3_hova    ,   bdikat_hityazvut_letachanat_s,     
      nitan_ledaveach_bmachala_aruc, nizbar_yomit_lehalbasha_nesio,rashai_ledaveach,heara_letiud,
      matala_klalit_lelo_rechev
FROM (
  SELECT
    mispar_sidur,
    MAX(CASE WHEN kod_meafyen=1 THEN NVL(erech,'-1')   ELSE '' END)    teur,
    MAX(CASE WHEN  kod_meafyen=2 THEN NVL(erech,'-1')   ELSE '' END)   Sug_sidur     ,
    MAX(CASE WHEN  kod_meafyen=3 THEN NVL(erech,'-1')   ELSE '' END)   sector_avoda,
    MAX(CASE WHEN  kod_meafyen=72 THEN NVL(erech,'-1')   ELSE '' END)   sug_premia,
    MAX(CASE WHEN  kod_meafyen=5 THEN NVL(erech,'-1')   ELSE '' END)   mofia_bdoch_nochachut_minhal_u,
    MAX(CASE WHEN  kod_meafyen=6 THEN NVL(erech,'-1')   ELSE '' END)   headrut_hova_ledaveach_shaot,
    MAX(CASE WHEN  kod_meafyen=7 THEN erech  ELSE  NULL  END)  shat_hatchala_muteret,
    MAX(CASE WHEN  kod_meafyen=8 THEN erech  ELSE  NULL END)  shat_gmar_muteret,
    MAX(CASE WHEN  kod_meafyen=9 THEN NVL(erech,'-1')   ELSE '' END)   hukiyut_beshabaton,
    MAX(CASE WHEN  kod_meafyen=10 THEN NVL(erech,'-1')   ELSE '' END) hokiyut_beshishi,
    MAX(CASE WHEN  kod_meafyen=11 THEN NVL(erech,'-1')   ELSE '' END) sug_hashaot_beyom_shishi,
    MAX(CASE WHEN  kod_meafyen=12 THEN NVL(erech,'-1')   ELSE '' END) sug_hashaot_beyom_shabaton,
    MAX(CASE WHEN  kod_meafyen=13 THEN NVL(erech,'-1')   ELSE '' END) hova_ledaveach_peilut,
    MAX(CASE WHEN  kod_meafyen=14 THEN NVL(erech,'-1')   ELSE '' END) zakay_leaman_nesia     ,
    MAX(CASE WHEN  kod_meafyen=15  THEN NVL(erech,'-1')   ELSE '' END) zakay_lezman_halbasha,
    MAX(CASE WHEN  kod_meafyen=16 THEN NVL(erech,'-1')   ELSE '' END)  max_shaot_byom_hol     ,
    MAX(CASE WHEN  kod_meafyen=17 THEN NVL(erech,'-1')   ELSE '' END)  sug_shaot_byom_hol_if_migbala,
    MAX(CASE WHEN  kod_meafyen=18 THEN NVL(erech,'-1')   ELSE '' END)  max_shaot_byom_shishi     ,
    MAX(CASE WHEN  kod_meafyen=19 THEN NVL(erech,'-1')   ELSE '' END)  max_shaot_beshabaton     ,
    MAX(CASE WHEN  kod_meafyen=20 THEN NVL(erech,'-1')   ELSE '' END)  asur_2_peiluyot_meoto_sug,
    MAX(CASE WHEN  kod_meafyen=21 THEN NVL(erech,'-1')   ELSE '' END)  hashlama_lemin_shaot_imahu,
    MAX(CASE WHEN  kod_meafyen=22 THEN NVL(erech,'-1')   ELSE '' END)  mutar_lebitzua_rak_lemeafyen_x,
    MAX(CASE WHEN  kod_meafyen=23 THEN NVL(erech,'-1')   ELSE '' END)  mutar_lebitzua_rak_lerishyon,
    MAX(CASE WHEN  kod_meafyen=24 THEN NVL(erech,'-1')   ELSE '' END)  avar_hadracha_meyuchedet    ,
    MAX(CASE WHEN  kod_meafyen=25 THEN NVL(erech,'-1')   ELSE '' END)  zakay_michutz_lamichsa,
    MAX(CASE WHEN  kod_meafyen=26 THEN NVL(erech,'-1')   ELSE '' END)  zakay_lepizul      ,
    MAX(CASE WHEN  kod_meafyen=27 THEN NVL(erech,'-1')   ELSE '' END)  nitan_ledaveach_ad_taarich,
    MAX(CASE WHEN  kod_meafyen=28 THEN NVL(erech,'-1')   ELSE '' END)  kod_tipul_meyuchad_Baklita,
    MAX(CASE WHEN  kod_meafyen=29 THEN NVL(erech,'-1')   ELSE '' END)  kod_tipul_meyuchad_Bashguim,
    MAX(CASE WHEN  kod_meafyen=30 THEN NVL(erech,'-1')   ELSE '' END)  kod_tipul_meyuchad_Bachishuv,
    MAX(CASE WHEN  kod_meafyen=31 THEN NVL(erech,'-1')   ELSE '' END)  asur_ledivuach_lechevrot_bat    ,
    MAX(CASE WHEN  kod_meafyen=32 THEN NVL(erech,'-1')   ELSE '' END)  zakay_lehamara         ,
    MAX(CASE WHEN  kod_meafyen=33 THEN NVL(erech,'-1')   ELSE '' END)  kod_headrut         ,
    MAX(CASE WHEN  kod_meafyen=34 THEN NVL(erech,'-1')   ELSE '' END)  zakay_lelina     ,
    MAX(CASE WHEN  kod_meafyen=35 THEN NVL(erech,'-1')   ELSE '' END)  zakay_lechariga,
    MAX(CASE WHEN  kod_meafyen=36 THEN NVL(erech,'-1')   ELSE '' END)  hova_ledaveach_mispar_machsan,
    MAX(CASE WHEN  kod_meafyen=37 THEN NVL(erech,'-1')   ELSE '' END)  hova_ledaveach_mispar_musach,
    MAX(CASE WHEN  kod_meafyen=38 THEN NVL(erech,'-1')   ELSE '' END)  hova_yom_visa_namlak,
    MAX(CASE WHEN  kod_meafyen=39 THEN NVL(erech,'-1')   ELSE '' END)  asur_divuach_simun_tachograf,
    MAX(CASE WHEN  kod_meafyen=40 THEN NVL(erech,'-1')   ELSE '' END)  zakay_lehashlama_avur_sidur ,
    MAX(CASE WHEN  kod_meafyen=41 THEN NVL(erech,'-1')   ELSE '' END)  zakay_lechishuv_retzifut,
    MAX(CASE WHEN  kod_meafyen=42 THEN NVL(erech,'-1')   ELSE '' END)  metagber_benihul_tnua,
    MAX(CASE WHEN  kod_meafyen=43 THEN NVL(erech,'-1')   ELSE '' END)  asur_ledaveach_mispar_rechev,
    MAX(CASE WHEN  kod_meafyen=44 THEN NVL(erech,'-1')   ELSE '' END)  zarich_ledaveach_km,
    MAX(CASE WHEN  kod_meafyen=45 THEN NVL(erech,'-1')   ELSE '' END)  sidur_namlak_visa,
    MAX(CASE WHEN  kod_meafyen=46 THEN NVL(erech,'-1')   ELSE '' END)  sidur_asur_ledaveach_peilut,
    MAX(CASE WHEN  kod_meafyen=47 THEN NVL(erech,'-1')   ELSE '' END)  avodat_musach    ,
    MAX(CASE WHEN  kod_meafyen=48 THEN NVL(erech,'-1')   ELSE '' END)  zakaut_lepizul_nehagut    ,
    MAX(CASE WHEN  kod_meafyen=49 THEN NVL(erech,'-1')   ELSE '' END)  zakaut_legmul_chisachon,
    MAX(CASE WHEN  kod_meafyen=50 THEN NVL(erech,'-1')   ELSE '' END)      zakaut_lenosafot    ,
    MAX(CASE WHEN  kod_meafyen=51 THEN NVL(erech,'-1')   ELSE '' END)      lo_pogea_bemichsat_nosafot,
    MAX(CASE WHEN  kod_meafyen=52 THEN NVL(erech,'-1')   ELSE '' END)  sug_avoda,
    MAX(CASE WHEN  kod_meafyen=53 THEN NVL(erech,'-1')   ELSE '' END)  sidur_misug_headrut,
    MAX(CASE WHEN  kod_meafyen=54 THEN NVL(erech,'-1')   ELSE '' END)  shaon_nochachut,
    MAX(CASE WHEN  kod_meafyen=55 THEN NVL(erech,'-1')   ELSE '' END)       rechiv_lezvira_yomit,
    MAX(CASE WHEN  kod_meafyen=56 THEN NVL(erech,'-1')   ELSE '' END)       rechiv_lezvira_chodshit,
    MAX(CASE WHEN  kod_meafyen=57 THEN NVL(erech,'-1')   ELSE '' END)  Michsat_shaot_chodshit,
    MAX(CASE WHEN  kod_meafyen=58 THEN NVL(erech,'-1')   ELSE '' END)       max_dakot_nosafot_bechodesh,
    MAX(CASE WHEN  kod_meafyen=59 THEN NVL(erech,'-1')   ELSE '' END)       zakaut_tamritz_nosafot_minhal,
    MAX(CASE WHEN  kod_meafyen=60 THEN NVL(erech,'-1')   ELSE '' END)  max_dakot_boded,
    MAX(CASE WHEN  kod_meafyen=61 THEN NVL(erech,'-1')   ELSE '' END)  ein_leshalem_tos_lila,
    MAX(CASE WHEN  kod_meafyen=62 THEN NVL(erech,'-1')   ELSE '' END)  dakot_n_letashlum_hol,
    MAX(CASE WHEN  kod_meafyen=63 THEN NVL(erech,'-1')   ELSE '' END)       michsat_shishi_lebaley_x,
    MAX(CASE WHEN  kod_meafyen=64 THEN NVL(erech,'-1')   ELSE '' END)       shabaton_tashlum_kavua,
    MAX(CASE WHEN  kod_meafyen=65 THEN NVL(erech,'-1')   ELSE '' END)         max_yemey_avoda_bechodesh,
    MAX(CASE WHEN  kod_meafyen=66 THEN NVL(erech,'-1')   ELSE '' END)         kod_ishur_WF    ,
    MAX(CASE WHEN  kod_meafyen=67 THEN NVL(erech,'-1')   ELSE '' END)         sidur_mishtatel_bechafifa,
    MAX(CASE WHEN  kod_meafyen=68 THEN NVL(erech,'-1')   ELSE '' END)         tashlum_kavua_beshishi,
    MAX(CASE WHEN  kod_meafyen=69 THEN NVL(erech,'-1')   ELSE '' END)         lo_zakay_legmul_beshishi,
    MAX(CASE WHEN  kod_meafyen=70 THEN NVL(erech,'-1')   ELSE '' END)         lo_zakay_legmul_beshabaton,
    MAX(CASE WHEN  kod_meafyen=71 THEN NVL(erech,'-1')   ELSE '' END)         sug_shaot_nosafot    ,
    MAX(CASE WHEN  kod_meafyen=4 THEN NVL(erech,'-1')   ELSE '' END)        sidur_lo_nivdak_sofash,
    MAX(CASE WHEN  kod_meafyen=73 THEN NVL(erech,'-1')   ELSE '' END)      sidur_kaytanot_veruey_kayiz,
    MAX(CASE WHEN  kod_meafyen=74 THEN NVL(erech,'-1')   ELSE '' END)         musachey_mishmeret2,
    MAX(CASE WHEN  kod_meafyen=75 THEN NVL(erech,'-1')   ELSE '' END)       asur_leshanot_zman_peilut,
    MAX(CASE WHEN  kod_meafyen=76 THEN NVL(erech,'-1')   ELSE '' END)       lo_nidreshet_hityazvut,
    MAX(CASE WHEN  kod_meafyen=77 THEN NVL(erech,'-1')   ELSE '' END)       zakaut_lerezifut_benahagut,
    MAX(CASE WHEN  kod_meafyen=78 THEN NVL(erech,'-1')   ELSE '' END)      kizuz_al_pi_hatchala_gmar,
    MAX(CASE WHEN  kod_meafyen=79 THEN NVL(erech,'-1')   ELSE '' END)      lo_letashlum_automati,
    MAX(CASE WHEN  kod_meafyen=80 THEN NVL(erech,'-1')   ELSE '' END)       zakaut_letamriz_nos_nehagut,
    MAX(CASE WHEN  kod_meafyen=81 THEN erech  ELSE  NULL  END)   tokef_hatchala     ,
    MAX(CASE WHEN  kod_meafyen=82 THEN erech  ELSE  NULL  END)   tokef_siyum     ,
    MAX(CASE WHEN  kod_meafyen=83 THEN NVL(erech,'-1')   ELSE '' END)       hovat_hityazvut    ,
    MAX(CASE WHEN  kod_meafyen=84 THEN NVL(erech,'-1')   ELSE '' END)       lidrosh_kod_mivtza,
    MAX(CASE WHEN  kod_meafyen=85 THEN NVL(erech,'-1')   ELSE '' END)      mofia_bedoch_noch_prem    ,
    MAX(CASE WHEN  kod_meafyen=86 THEN NVL(erech,'-1')   ELSE '' END)      avodat_meshek,
    MAX(CASE WHEN  kod_meafyen=87 THEN NVL(erech,'-1')   ELSE '' END)        tashlum_kavua_bchol    ,
    MAX(CASE WHEN  kod_meafyen=88 THEN NVL(erech,'-1')   ELSE '' END)        shat_hatchala_auto,
    MAX(CASE WHEN  kod_meafyen=89 THEN NVL(erech,'-1')   ELSE '' END)      shat_gmar_auto,
    MAX(CASE WHEN  kod_meafyen=90 THEN NVL(erech,'-1')   ELSE '' END)      sidur_lo_chosem,
    MAX(CASE WHEN  kod_meafyen=91 THEN NVL(erech,'-1')   ELSE '' END)      realy_veod_yom,
    MAX(CASE WHEN  kod_meafyen=92 THEN NVL(erech,'-1')   ELSE '' END)      sidur_rak_lechevrot_banot,
    MAX(CASE WHEN  kod_meafyen=93 THEN NVL(erech,'-1')   ELSE '' END)       element1_hova    ,
    MAX(CASE WHEN  kod_meafyen=94 THEN NVL(erech,'-1')   ELSE '' END)       element2_hova    ,
    MAX(CASE WHEN  kod_meafyen=95 THEN NVL(erech,'-1')   ELSE '' END)       element3_hova    ,
    MAX(CASE WHEN  kod_meafyen=96 THEN NVL(erech,'-1')   ELSE '' END)       bdikat_hityazvut_letachanat_s,
    MAX(CASE WHEN  kod_meafyen=97 THEN NVL(erech,'-1')   ELSE '' END)       nitan_ledaveach_bmachala_aruc,
    MAX(CASE WHEN  kod_meafyen=98 THEN NVL(erech,'-1')   ELSE '' END)       nizbar_yomit_lehalbasha_nesio,
    MAX(CASE WHEN  kod_meafyen=99 THEN NVL(erech,'-1')   ELSE '' END)       rashai_ledaveach,
    MAX(CASE WHEN  kod_meafyen=100 THEN NVL(erech,'-1')   ELSE '' END)       heara_letiud,
    MAX(CASE WHEN  kod_meafyen=101 THEN NVL(erech,'-1')   ELSE '' END)      matala_klalit_lelo_rechev

  FROM
    TB_SIDURIM_MEYUCHADIM
    WHERE mispar_sidur =par_sidur
            AND ((p_Dt_from <= me_taarich  AND  p_Dt_to >= me_taarich  AND p_Dt_to <= NVL(ad_taarich,TO_DATE('31/12/4712','dd/mm/yyyy')))
     OR  (p_Dt_from <= me_taarich  AND p_Dt_to >= NVL(ad_taarich,TO_DATE('31/12/4712','dd/mm/yyyy')))
     OR (p_Dt_from >= me_taarich  AND  p_Dt_from <= NVL(ad_taarich,TO_DATE('31/12/4712','dd/mm/yyyy')) AND p_Dt_to >= NVL(ad_taarich,TO_DATE('31/12/4712','dd/mm/yyyy')))
     OR (p_Dt_from >= me_taarich   AND  p_Dt_to <= NVL(ad_taarich,TO_DATE('31/12/4712','dd/mm/yyyy'))))
--    and ((to_date(p_Dt_from,'dd/mm/yyyy') <= me_taarich  and to_date(p_Dt_to,'dd/mm/yyyy') >= me_taarich  and to_date(p_Dt_to,'dd/mm/yyyy') <= nvl(ad_taarich,to_date('31/12/4712','dd/mm/yyyy')))
--     or  (to_date(p_Dt_from,'dd/mm/yyyy') <= me_taarich  and to_date(p_Dt_to,'dd/mm/yyyy') >= nvl(ad_taarich,to_date('31/12/4712','dd/mm/yyyy')))
 --    or (to_date(p_Dt_from,'dd/mm/yyyy') >= me_taarich  and to_date(p_Dt_from,'dd/mm/yyyy') <= nvl(ad_taarich,to_date('31/12/4712','dd/mm/yyyy')) and to_date(p_Dt_to,'dd/mm/yyyy') >= nvl(ad_taarich,to_date('31/12/4712','dd/mm/yyyy')))
--     or (to_date(p_Dt_from,'dd/mm/yyyy') >= me_taarich   and to_date(p_Dt_to,'dd/mm/yyyy') <= nvl(ad_taarich,to_date('31/12/4712','dd/mm/yyyy'))))
     GROUP BY
   mispar_sidur);

BEGIN

FOR  Sidurim_tmp_rec IN  Sidurim_tmp LOOP

FOR  Me_tarich_tmp_rec IN  Me_tarich_tmp(Sidurim_tmp_rec.mispar_sidur) LOOP

 INSERT INTO PIVOT_SIDURIM_MEYUCHADIM
 (mispar_sidur,me_tarich,ad_tarich)
 VALUES (Sidurim_tmp_rec.mispar_sidur,Me_tarich_tmp_rec.me_tarich,
-- ( select distinct least((select   nvl(min(ad_taarich),to_date('31/12/2100','dd/mm/yyyy'))
 ( SELECT DISTINCT LEAST((SELECT   NVL(MIN(p1.ad_taarich),TO_DATE('31/12/4712','dd/mm/yyyy'))
FROM   TB_SIDURIM_MEYUCHADIM p1
    WHERE  mispar_sidur=Sidurim_tmp_rec.mispar_sidur
    AND p1.ad_taarich>=Me_tarich_tmp_rec.me_tarich)
    ,
    (SELECT NVL(MIN(p2.me_taarich) - 1,TO_DATE('31/12/4712','dd/mm/yyyy'))
FROM   TB_SIDURIM_MEYUCHADIM p2
    WHERE  mispar_sidur=Sidurim_tmp_rec.mispar_sidur
         AND p2.me_taarich>(SELECT MIN(p3.me_taarich) FROM TB_SIDURIM_MEYUCHADIM p3 WHERE mispar_sidur=Sidurim_tmp_rec.mispar_sidur)
 AND  p2.me_taarich>Me_tarich_tmp_rec.me_tarich )
     ,
     TO_DATE('31/12/4712','dd/mm/yyyy')) ad_tarich
FROM   TB_SIDURIM_MEYUCHADIM p4
    WHERE  mispar_sidur=Sidurim_tmp_rec.mispar_sidur)
 );

END LOOP;

 DELETE  FROM  PIVOT_SIDURIM_MEYUCHADIM
 WHERE mispar_sidur=Sidurim_tmp_rec.mispar_sidur
 AND ad_tarich<TO_DATE('01/01/2008','dd/mm/yyyy');


 FOR  Me_Ad_tmp_rec IN  Me_Ad_tmp(Sidurim_tmp_rec.mispar_sidur) LOOP

 FOR  sidurim_meyuchadim_tmp_rec IN  sidurim_meyuchadim_tmp(Sidurim_tmp_rec.mispar_sidur,
 Me_Ad_tmp_rec.me_tarich,Me_Ad_tmp_rec.ad_tarich)
 LOOP
 UPDATE  PIVOT_SIDURIM_MEYUCHADIM
 SET  teur=sidurim_meyuchadim_tmp_rec.teur,
       sug_premia=sidurim_meyuchadim_tmp_rec.sug_premia,
     shat_hatchala_muteret= TO_DATE(sidurim_meyuchadim_tmp_rec.shat_hatchala_muteret,'hh24:mi'),
      shat_gmar_muteret= TO_DATE(sidurim_meyuchadim_tmp_rec.shat_gmar_muteret,'hh24:mi'),
       hova_ledaveach_peilut=sidurim_meyuchadim_tmp_rec.hova_ledaveach_peilut,
       zakay_lezman_halbasha=sidurim_meyuchadim_tmp_rec.zakay_lezman_halbasha,
       asur_2_peiluyot_meoto_sug=sidurim_meyuchadim_tmp_rec.asur_2_peiluyot_meoto_sug,
       mutar_lebitzua_rak_lmeafyen_x    =sidurim_meyuchadim_tmp_rec.mutar_lebitzua_rak_lemeafyen_x,
      mutar_lebitzua_rak_lerishyon=sidurim_meyuchadim_tmp_rec.mutar_lebitzua_rak_lerishyon,
      zakay_michutz_lamichsa=sidurim_meyuchadim_tmp_rec.zakay_michutz_lamichsa,
      nitan_ledaveach_ad_taarich=sidurim_meyuchadim_tmp_rec.nitan_ledaveach_ad_taarich,
      zakay_lechariga=sidurim_meyuchadim_tmp_rec.zakay_lechariga,
      hova_ledaveach_mispar_machsan=sidurim_meyuchadim_tmp_rec.hova_ledaveach_mispar_machsan,
      hova_ledaveach_mispar_musach=sidurim_meyuchadim_tmp_rec.hova_ledaveach_mispar_musach,
      hova_yom_visa_namlak=sidurim_meyuchadim_tmp_rec.hova_yom_visa_namlak,
      Sug_sidur    =sidurim_meyuchadim_tmp_rec.sug_sidur,
      sector_avoda    =sidurim_meyuchadim_tmp_rec.sector_avoda,
      headrut_hova_ledaveach_shaot=sidurim_meyuchadim_tmp_rec.headrut_hova_ledaveach_shaot,
      mofia_bdoch_nochachut_minhal_u=sidurim_meyuchadim_tmp_rec.mofia_bdoch_nochachut_minhal_u,
      hukiyut_beshabaton=sidurim_meyuchadim_tmp_rec.hukiyut_beshabaton,
      hokiyut_beshishi=sidurim_meyuchadim_tmp_rec.hokiyut_beshishi,
      sug_hashaot_beyom_shishi    =sidurim_meyuchadim_tmp_rec.sug_hashaot_beyom_shishi    ,
      sug_hashaot_beyom_shabaton =sidurim_meyuchadim_tmp_rec.sug_hashaot_beyom_shabaton ,
      zakay_leaman_nesia    =sidurim_meyuchadim_tmp_rec.zakay_leaman_nesia,
      max_shaot_byom_hol    =sidurim_meyuchadim_tmp_rec.max_shaot_byom_hol,
      sug_shaot_byom_hol_if_migbala    =sidurim_meyuchadim_tmp_rec.sug_shaot_byom_hol_if_migbala    ,
      max_shaot_byom_shishi    =sidurim_meyuchadim_tmp_rec.max_shaot_byom_shishi    ,
      max_shaot_beshabaton    =sidurim_meyuchadim_tmp_rec.max_shaot_beshabaton    ,
      hashlama_lemin_shaot_imahu    =sidurim_meyuchadim_tmp_rec.hashlama_lemin_shaot_imahu,
      avar_hadracha_meyuchedet    =sidurim_meyuchadim_tmp_rec.avar_hadracha_meyuchedet    ,
      zakay_lepizul     =sidurim_meyuchadim_tmp_rec.zakay_lepizul ,
      kod_tipul_meyuchad_Baklita    =sidurim_meyuchadim_tmp_rec.kod_tipul_meyuchad_Baklita,
      kod_tipul_meyuchad_Bashguim    =sidurim_meyuchadim_tmp_rec.kod_tipul_meyuchad_Bashguim    ,
      kod_tipul_meyuchad_Bachishuv    =sidurim_meyuchadim_tmp_rec.kod_tipul_meyuchad_Bachishuv,
      asur_ledivuach_lechevrot_bat    =sidurim_meyuchadim_tmp_rec.asur_ledivuach_lechevrot_bat,
      zakay_lehamara        =sidurim_meyuchadim_tmp_rec.zakay_lehamara,
      kod_headrut        =sidurim_meyuchadim_tmp_rec.kod_headrut    ,
      zakay_lelina    =sidurim_meyuchadim_tmp_rec.zakay_lelina,
      asur_divuach_simun_tachograf    =sidurim_meyuchadim_tmp_rec.asur_divuach_simun_tachograf,
      zakay_lehashlama_avur_sidur=sidurim_meyuchadim_tmp_rec.zakay_lehashlama_avur_sidur,
      zakay_lechishuv_retzifut    =sidurim_meyuchadim_tmp_rec.zakay_lechishuv_retzifut,
      metagber_benihul_tnua=sidurim_meyuchadim_tmp_rec.metagber_benihul_tnua,
      zarich_ledaveach_km=sidurim_meyuchadim_tmp_rec.zarich_ledaveach_km,
      asur_ledaveach_mispar_rechev =sidurim_meyuchadim_tmp_rec.asur_ledaveach_mispar_rechev,
      sidur_namlak_visa  =sidurim_meyuchadim_tmp_rec.sidur_namlak_visa,
      sidur_asur_ledaveach_peilut=sidurim_meyuchadim_tmp_rec.sidur_asur_ledaveach_peilut,
      avodat_musach    =sidurim_meyuchadim_tmp_rec.avodat_musach,
      zakaut_lepizul_nehagut    =sidurim_meyuchadim_tmp_rec.zakaut_lepizul_nehagut,
      zakaut_legmul_chisachon=sidurim_meyuchadim_tmp_rec.zakaut_legmul_chisachon,
      sug_avoda  =sidurim_meyuchadim_tmp_rec.sug_avoda,
      sidur_misug_headrut =sidurim_meyuchadim_tmp_rec.sidur_misug_headrut,
       shaon_nochachut  =sidurim_meyuchadim_tmp_rec.shaon_nochachut,
      ein_leshalem_tos_lila=sidurim_meyuchadim_tmp_rec.ein_leshalem_tos_lila,
      max_dakot_boded= sidurim_meyuchadim_tmp_rec.max_dakot_boded,
       dakot_n_letashlum_hol= sidurim_meyuchadim_tmp_rec. dakot_n_letashlum_hol,
      sidur_kaytanot_veruey_kayiz=sidurim_meyuchadim_tmp_rec.sidur_kaytanot_veruey_kayiz,
      sidur_lo_nivdak_sofash=sidurim_meyuchadim_tmp_rec.sidur_lo_nivdak_sofash,
      kizuz_al_pi_hatchala_gmar=sidurim_meyuchadim_tmp_rec.kizuz_al_pi_hatchala_gmar,
      lo_letashlum_automati=sidurim_meyuchadim_tmp_rec.lo_letashlum_automati,
      Michsat_shaot_chodshit=sidurim_meyuchadim_tmp_rec.Michsat_shaot_chodshit,
      zakaut_lenosafot    =sidurim_meyuchadim_tmp_rec.zakaut_lenosafot,
      lo_pogea_bemichsat_nosafot    =sidurim_meyuchadim_tmp_rec.lo_pogea_bemichsat_nosafot,
       rechiv_lezvira_yomit    =sidurim_meyuchadim_tmp_rec.rechiv_lezvira_yomit,
       rechiv_lezvira_chodshit    =sidurim_meyuchadim_tmp_rec.rechiv_lezvira_chodshit    ,
       max_dakot_nosafot_bechodesh    =sidurim_meyuchadim_tmp_rec.max_dakot_nosafot_bechodesh,
       zakaut_tamritz_nosafot_minhal=sidurim_meyuchadim_tmp_rec.zakaut_tamritz_nosafot_minhal,
       michsat_shishi_lebaley_x=sidurim_meyuchadim_tmp_rec.michsat_shishi_lebaley_x,
       shabaton_tashlum_kavua    =sidurim_meyuchadim_tmp_rec.shabaton_tashlum_kavua,
         max_yemey_avoda_bechodesh    =sidurim_meyuchadim_tmp_rec.max_yemey_avoda_bechodesh,
         kod_ishur_WF=sidurim_meyuchadim_tmp_rec. kod_ishur_WF,
         sidur_mishtatel_bechafifa=sidurim_meyuchadim_tmp_rec.sidur_mishtatel_bechafifa,
         tashlum_kavua_beshishi    =sidurim_meyuchadim_tmp_rec. tashlum_kavua_beshishi,
         lo_zakay_legmul_beshishi=sidurim_meyuchadim_tmp_rec.lo_zakay_legmul_beshishi,
         lo_zakay_legmul_beshabaton    =sidurim_meyuchadim_tmp_rec. lo_zakay_legmul_beshabaton    ,
         sug_shaot_nosafot    =sidurim_meyuchadim_tmp_rec.sug_shaot_nosafot,
         musachey_mishmeret2    =sidurim_meyuchadim_tmp_rec.musachey_mishmeret2,
       asur_leshanot_zman_peilut    =sidurim_meyuchadim_tmp_rec. asur_leshanot_zman_peilut    ,
       lo_nidreshet_hityazvut    =sidurim_meyuchadim_tmp_rec.lo_nidreshet_hityazvut    ,
       zakaut_lerezifut_benahagut=sidurim_meyuchadim_tmp_rec.zakaut_lerezifut_benahagut,
       zakaut_letamriz_nos_nehagut=sidurim_meyuchadim_tmp_rec.zakaut_letamriz_nos_nehagut,
       hovat_hityazvut    =sidurim_meyuchadim_tmp_rec.hovat_hityazvut    ,
       lidrosh_kod_mivtza    =sidurim_meyuchadim_tmp_rec.lidrosh_kod_mivtza,
       mofia_bedoch_noch_prem       =sidurim_meyuchadim_tmp_rec.mofia_bedoch_noch_prem    ,
       avodat_meshek    =sidurim_meyuchadim_tmp_rec.avodat_meshek,
       tashlum_kavua_bchol    =sidurim_meyuchadim_tmp_rec.tashlum_kavua_bchol,
       shat_hatchala_auto  =sidurim_meyuchadim_tmp_rec.shat_hatchala_auto,
       shat_gmar_auto   =sidurim_meyuchadim_tmp_rec.shat_gmar_auto ,
       sidur_lo_chosem = sidurim_meyuchadim_tmp_rec.sidur_lo_chosem,
       realy_veod_yom  = sidurim_meyuchadim_tmp_rec.realy_veod_yom,
       sidur_rak_lechevrot_banot  =sidurim_meyuchadim_tmp_rec.sidur_rak_lechevrot_banot ,
       element1_hova    =sidurim_meyuchadim_tmp_rec.element1_hova,
       element2_hova    =sidurim_meyuchadim_tmp_rec.element2_hova,
       element3_hova    =sidurim_meyuchadim_tmp_rec.element3_hova    ,
       bdikat_hityazvut_letachanat_s=sidurim_meyuchadim_tmp_rec.bdikat_hityazvut_letachanat_s,
       nitan_ledaveach_bmachala_aruc=  sidurim_meyuchadim_tmp_rec.nitan_ledaveach_bmachala_aruc,
       nizbar_yomit_lehalbasha_nesio     =  sidurim_meyuchadim_tmp_rec.nizbar_yomit_lehalbasha_nesio,
       tokef_hatchala= TO_DATE(sidurim_meyuchadim_tmp_rec.tokef_hatchala,'dd/mm/yyyy'),
       tokef_siyum    = TO_DATE(sidurim_meyuchadim_tmp_rec.tokef_siyum,'dd/mm/yyyy'),
       rashai_ledaveach =  sidurim_meyuchadim_tmp_rec.rashai_ledaveach,
       heara_letiud        =sidurim_meyuchadim_tmp_rec.heara_letiud ,
       matala_klalit_lelo_rechev =sidurim_meyuchadim_tmp_rec.matala_klalit_lelo_rechev
 WHERE mispar_sidur=Sidurim_tmp_rec.mispar_sidur
 AND me_tarich=Me_Ad_tmp_rec.me_tarich
 AND ad_tarich=Me_Ad_tmp_rec.ad_tarich;
END LOOP;
END LOOP;
END LOOP;
END pro_Pivot_Sidurim_Meyuchadim;

/**************************************************************/

PROCEDURE pro_get_yom_viza( p_Cur OUT CurType) IS
BEGIN

      OPEN p_Cur FOR
       SELECT  Y.KOD_YOM_VISA code, Y.TEUR_YOM_VISA || ' (' || Y.KOD_YOM_VISA  ||') ' description
            FROM  CTB_YOM_VISA Y
            WHERE Y.PAIL='1'
            ORDER BY Y.KOD_YOM_VISA DESC;

EXCEPTION
    WHEN OTHERS THEN
        RAISE;
END pro_get_yom_viza;

PROCEDURE    pro_get_sug_hazmanat_viza( p_Cur OUT CurType) IS
BEGIN

      OPEN p_Cur FOR
       SELECT S.KOD_HAZMANA   code, S.TEUR_HAZMANA ||  '('  || S.KOD_HAZMANA || ')'  description
            FROM  CTB_SUG_HAZMANA_VISA S
            WHERE S.PAIL='1'
            ORDER BY S.TEUR_HAZMANA;

EXCEPTION
    WHEN OTHERS THEN
        RAISE;
END pro_get_sug_hazmanat_viza;

PROCEDURE    pro_get_kod_mivza_viza( p_Cur OUT CurType) IS
BEGIN

      OPEN p_Cur FOR
       SELECT M.KOD_MIVTZA_VISA  code,M.TEUR_MIVTZA_VISA  description
            FROM  CTB_MIVTZA_VISA M
            WHERE M.PAIL='1'
            ORDER BY M.TEUR_MIVTZA_VISA;


EXCEPTION
    WHEN OTHERS THEN
        RAISE;
END pro_get_kod_mivza_viza;

PROCEDURE    pro_get_lina( p_Cur OUT CurType) IS
BEGIN

      OPEN p_Cur FOR
         SELECT L.KOD_LINA  code,L.TEUR_LINA  description
            FROM  CTB_LINA L
            WHERE L.PAIL='1'
            ORDER BY L.TEUR_LINA;

EXCEPTION
    WHEN OTHERS THEN
        RAISE;
END pro_get_lina;

PROCEDURE ProGetMusach_O_Machsan(p_Taarich  IN VARCHAR2, p_Cur OUT CurType) IS
BEGIN

      OPEN p_Cur FOR
         SELECT DISTINCT M.MAHSAN_AHSANA code, M.MAHSAN_AHSANA  description
            FROM  CTB_MAHSANIM M
            WHERE TO_DATE(p_Taarich,'dd/mm/yyyy') BETWEEN m.ME_TAARICH_TOKEF AND NVL(M.AD_TAARICH_TOKEF,TO_DATE('01/01/4000','dd/mm/yyyy'))
            ORDER BY M.MAHSAN_AHSANA;

EXCEPTION
    WHEN OTHERS THEN
        RAISE;
END ProGetMusach_O_Machsan;

PROCEDURE get_tmp_sidurim_meyuchadim(p_tar_me IN TB_SIDURIM_MEYUCHADIM.me_taarich%TYPE,
                                                                                                p_tar_ad  IN TB_SIDURIM_MEYUCHADIM.me_taarich%TYPE,
                                                                                         p_Cur OUT CurType) AS
BEGIN
DBMS_APPLICATION_INFO.SET_MODULE('pkg_sidurim.get_tmp_sidurim_meyuchadim','get sidurim meyuchadim');


     OPEN p_cur FOR
           SELECT ts.mispar_sidur, ts.zakay_lezman_halbasha halbash_kod,
           ts.shat_hatchala_muteret,
          ts.shat_gmar_muteret,
           ts.zakay_michutz_lamichsa,
           ts.sidur_asur_ledaveach_peilut no_peilot_kod,
           ts.asur_ledaveach_mispar_rechev no_oto_no,
           ts.sidur_namlak_visa sidur_visa_kod,
         ts.sector_avoda,
         ts.zakay_lehashlama_avur_sidur hashlama_kod,
       ts.zakay_lechariga, ts.zakay_lehamara ,
      ts.zakay_leaman_nesia,
         ts.hova_ledaveach_peilut peilut_required_kod,
           ts.asur_ledivuach_lechevrot_bat sidur_not_valid_kod,
             ts.hukiyut_beshabaton sidur_not_in_shabton_kod,
          ts.sidur_misug_headrut headrut_type_kod,
         ts.sug_avoda,
          ts.shaon_nochachut,
         ts.mispar_sidur mispar_sidur_myuhad,
          ts.sidur_kaytanot_veruey_kayiz sidur_in_summer,
           ts.lo_letashlum_automati,
           ts.zakay_lepizul,
          ts.kizuz_al_pi_hatchala_gmar,
          ts.hovat_hityazvut,
          ts.hova_ledaveach_mispar_machsan,
          ts.sidur_lo_nivdak_sofash,
          ts.nitan_ledaveach_bmachala_aruc,
          ts.lidrosh_kod_mivtza,
          ts.zakay_lelina,ts.tokef_hatchala,ts.tokef_siyum
        FROM PIVOT_SIDURIM_MEYUCHADIM ts
        WHERE  ts.Me_Tarich <=p_tar_me
        AND NVL(ts.AD_TARICH ,TO_DATE('01/01/9999','dd/mm/yyyy'))  >=p_tar_ad;

EXCEPTION
       WHEN OTHERS THEN
                RAISE;
  END get_tmp_sidurim_meyuchadim;
PROCEDURE get_tb_sadot_nosafim_lesidur(p_cur OUT CurType) IS
BEGIN
 DBMS_APPLICATION_INFO.SET_MODULE('pkg_sidurim.get_tb_sadot_nosafim_lesidur','get sadot nosafim lesidur');
    OPEN p_Cur FOR
    SELECT * FROM TB_SADOT_NOSAFIM_LESIDUR;
END  get_tb_sadot_nosafim_lesidur;
PROCEDURE pro_get_meafyeney_sidur(p_cur OUT CurType) IS
BEGIN
DBMS_APPLICATION_INFO.SET_MODULE('pkg_sidurim.pro_get_meafyeney_sidur','get meafyeney sidurim meyuchadim + meafyeney sug sidur');
 
  OPEN p_cur FOR  
  SELECT DISTINCT mispar_sidur sidur_key, kod_meafyen,erech FROM  TB_SIDURIM_MEYUCHADIM a, CTB_SIDURIM_MEYUCHADIM b WHERE b.pail = 1 AND A.MISPAR_SIDUR = B.KOD_SIDUR_MEYUCHAD
  UNION ALL
  SELECT DISTINCT sug_sidur sidur_key, kod_meafyen,erech FROM TB_MEAFYENEY_SUG_SIDUR;


END pro_get_meafyeney_sidur;


PROCEDURE Pivot_test IS
CURSOR Sidurim_tmp IS
  SELECT  DISTINCT  mispar_sidur
FROM   TB_SIDURIM_MEYUCHADIM
 --where   mispar_sidur between 63473 and 77690
;

CURSOR Me_tarich_tmp(par_sidur NUMBER) IS
SELECT DISTINCT me_tarich FROM
(SELECT DISTINCT p1.me_taarich  me_tarich
FROM   TB_SIDURIM_MEYUCHADIM  p1
    WHERE  p1.mispar_sidur=par_sidur
    UNION ALL
SELECT DISTINCT   p2.ad_taarich+1 me_tarich
--distinct  nvl(p2.ad_taarich,to_date('31/12/2100','dd/mm/yyyy'))+1 me_tarich
    FROM   TB_SIDURIM_MEYUCHADIM p2
    WHERE  p2.mispar_sidur=par_sidur
  -- and p2.ad_taarich<to_date('31/12/2100','dd/mm/yyyy')
   AND DECODE(p2.ad_taarich,TO_DATE('31/12/4712','dd/mm/yyyy'),NULL, TO_DATE('31/12/2100','dd/mm/yyyy'),NULL,   p2.ad_taarich) IS NOT NULL
    --  and not exists( select * from tb_sidurim_meyuchadim p3 where  p3.mispar_sidur=par_sidur
       --                                       and   p2.ad_taarich+1=p3.me_taarich
    --                             and  p2.mispar_sidur= p3.mispar_sidur);
    )
    ORDER BY me_tarich;



BEGIN

FOR  Sidurim_tmp_rec IN  Sidurim_tmp LOOP

FOR  Me_tarich_tmp_rec IN  Me_tarich_tmp(Sidurim_tmp_rec.mispar_sidur) LOOP

 INSERT INTO PIVOT_SIDURIM_MEYUCHADIM
 (mispar_sidur,me_tarich,ad_tarich)
 VALUES (Sidurim_tmp_rec.mispar_sidur,Me_tarich_tmp_rec.me_tarich,
 ( SELECT DISTINCT LEAST((SELECT   NVL(MIN(ad_taarich),TO_DATE('31/12/2100','dd/mm/yyyy'))
FROM   TB_SIDURIM_MEYUCHADIM
    WHERE  mispar_sidur=Sidurim_tmp_rec.mispar_sidur
    AND ad_taarich>=TO_DATE(Me_tarich_tmp_rec.me_tarich,'dd/mm/yyyy'))
    ,
    (SELECT NVL(MIN(me_taarich) - 1,TO_DATE('31/12/2100','dd/mm/yyyy'))
FROM   TB_SIDURIM_MEYUCHADIM
    WHERE  mispar_sidur=Sidurim_tmp_rec.mispar_sidur
         AND me_taarich>(SELECT MIN(me_taarich) FROM TB_SIDURIM_MEYUCHADIM WHERE mispar_sidur=Sidurim_tmp_rec.mispar_sidur)
 AND  me_taarich>TO_DATE(Me_tarich_tmp_rec.me_tarich,'dd/mm/yyyy') )
     ,
     TO_DATE('31/12/2100','dd/mm/yyyy')) ad_tarich
FROM   TB_SIDURIM_MEYUCHADIM
    WHERE  mispar_sidur=Sidurim_tmp_rec.mispar_sidur)
 );

END LOOP;

 DELETE  FROM  PIVOT_SIDURIM_MEYUCHADIM
 WHERE mispar_sidur=Sidurim_tmp_rec.mispar_sidur
 AND ad_tarich<TO_DATE('01/01/2008','dd/mm/yyyy');


END LOOP;

END Pivot_test;


PROCEDURE pro_get_siba_lo_letashlum(p_mispar_ishi IN TB_SIDURIM_OVDIM.mispar_ishi%TYPE,p_date IN  TB_SIDURIM_OVDIM.taarich%TYPE, p_mispar_sidur IN TB_SIDURIM_OVDIM.mispar_sidur%TYPE,p_shat_hatchala IN VARCHAR2, p_teur_siba OUT VARCHAR2)
IS
    v_teur_siba CTB_SIBOT_LOLETASHLUM.teur_siba%TYPE;
BEGIN
    v_teur_siba:='';
    SELECT A.TEUR_SIBA INTO v_teur_siba
    FROM CTB_SIBOT_LOLETASHLUM a, TB_SIDURIM_OVDIM b
    WHERE a.kod_siba = b.Kod_Siba_Lo_Letashlum
    AND B.MISPAR_ISHI = p_mispar_ishi
    AND  B.TAARICH  = p_date
    AND B.MISPAR_SIDUR = p_mispar_sidur
    AND B.shat_hatchala = TO_DATE(p_shat_hatchala,'dd/mm/yyyy HH24:MI:SS');

    p_teur_siba := v_teur_siba;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        p_teur_siba := '';  
END pro_get_siba_lo_letashlum;

PROCEDURE pro_get_kod_element(p_Prefix VARCHAR2,p_kod_element IN VARCHAR2,  p_value IN VARCHAR2  ,p_Cur OUT CurType)
IS
        
BEGIN
    OPEN p_Cur FOR
    SELECT DISTINCT(SM.KOD_SIDUR_MEYUCHAD)
    FROM CTB_SIDURIM_MEYUCHADIM SM
    WHERE SM.KOD_SIDUR_MEYUCHAD  LIKE  p_Prefix || '%' AND   
          SM.PAIL=1 AND
         ( (SM.KOD_SIDUR_MEYUCHAD  IN(SELECT DISTINCT(t.MISPAR_SIDUR)
                                    FROM TB_SIDURIM_MEYUCHADIM t
                                    WHERE t.KOD_MEAFYEN IN(SELECT x FROM TABLE(CAST(Convert_String_To_Table(p_kod_element ,  ',') AS mytabtype)))
                                    AND t.erech=p_value)
                                    )
        OR (p_kod_element IS NULL) )                                   
                                                         
     ORDER BY SM.KOD_SIDUR_MEYUCHAD;
END pro_get_kod_element;
PROCEDURE pro_get_meafyeny_sidur_by_id(p_date IN PIVOT_SIDURIM_MEYUCHADIM.AD_TARICH%TYPE,p_sidur_number IN PIVOT_SIDURIM_MEYUCHADIM.MISPAR_SIDUR%TYPE, p_cur OUT CurType) IS
BEGIN
    OPEN p_Cur FOR
    SELECT * FROM PIVOT_SIDURIM_MEYUCHADIM m
    WHERE m.mispar_sidur = p_sidur_number
          AND p_date BETWEEN m.me_tarich(+) AND m.ad_tarich(+); 
          
END pro_get_meafyeny_sidur_by_id;

FUNCTION fun_check_meafyen_exist(p_kod_element IN NUMBER,p_kod_meafyen IN NUMBER) RETURN NUMBER
IS
  p_kod NUMBER;
BEGIN

    SELECT e.KOD_MEAFYEN INTO p_kod
    FROM TB_MEAFYENEY_ELEMENTIM e
    WHERE E.KOD_ELEMENT = p_kod_element
          AND E.KOD_MEAFYEN = p_kod_meafyen;
 RETURN p_kod;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
        p_kod := NULL; 
        
        RETURN p_kod;
END fun_check_meafyen_exist;
PROCEDURE pro_get_course_presence(p_date in date,p_sidur in tb_sidurim_ovdim.mispar_sidur%type, p_course_num in tb_sidurim_ovdim.course_num%type, p_cur out CurType) is
BEGIN
    OPEN p_cur FOR

  SELECT  T.MISPAR_ISHI, O.SHEM_MISH || ' ' || O.SHEM_PRAT EmpName, 
               T.TAARICH,TEUR_YECHIDA, to_char(T.SHAT_HATCHALA,'HH24:MI') SHAT_HATCHALA, to_char(T.SHAT_GMAR,'HH24:MI') SHAT_GMAR 
               ,YECHIDA_IRGUNIT,   P.EZOR || SubStr(P.MAAMAD,0,1) EmpType
     FROM tb_sidurim_ovdim t, ovdim o,PIVOT_PIRTEY_OVDIM p, CTB_YECHIDA y 
     WHERE T.MISPAR_SIDUR = p_sidur and t.taarich between trunc(p_date) and trunc(p_date)
     AND O.MISPAR_ISHI = T.MISPAR_ISHI and P.MISPAR_ISHI = O.MISPAR_ISHI and trunc(p_date) between P.ME_TARICH and P.AD_TARICH
     AND P.MISPAR_ISHI = T.MISPAR_ISHI
     AND P.YECHIDA_IRGUNIT= Y.KOD_YECHIDA(+)
     AND Y.KOD_HEVRA=O.KOD_HEVRA
     AND T.COURSE_NUM=p_course_num
     ORDER BY YECHIDA_IRGUNIT;
     
      
/*SELECT  T.MISPAR_ISHI, O.SHEM_MISH || ' ' || O.SHEM_PRAT EmpName, 
                t.TAARICH,TEUR_YECHIDA,to_char(T.SHAT_HATCHALA,'HH:MM') SHAT_HATCHALA, to_char(T.SHAT_GMAR,'HH:MM') SHAT_GMAR ,
                YECHIDA_IRGUNIT,   P.EZOR || SubStr(P.MAAMAD,0,1) EmpType
     FROM tb_sidurim_ovdim t, ovdim o,PIVOT_PIRTEY_OVDIM p, CTB_YECHIDA y 
     WHERE T.MISPAR_SIDUR = p_sidur and t.taarich between trunc(sysdate-86) and trunc(sysdate)
     AND O.MISPAR_ISHI = T.MISPAR_ISHI and P.MISPAR_ISHI = O.MISPAR_ISHI and trunc(sysdate) between P.ME_TARICH and P.AD_TARICH
     AND P.MISPAR_ISHI = T.MISPAR_ISHI
     AND P.YECHIDA_IRGUNIT= Y.KOD_YECHIDA(+)
     order by YECHIDA_IRGUNIT;
   --  AND T.COURSE_NUM=p_course_num;*/
 
END pro_get_course_presence;

function GetSnifTnuaBySidur(p_mispar_ishi number, p_taarich date, p_mispar_sidur number, p_shat_hatchala date) return number IS
   p_snif number;
BEGIN
        p_snif:=0;
        IF (SUBSTR (p_mispar_sidur, 0, 2) <> '99' AND p_mispar_sidur > 999)  THEN
                 p_snif:= TO_NUMBER (SUBSTR (LPAD (p_mispar_sidur, 5), 0, 2));
        else   if(SUBSTR (p_mispar_sidur, 0, 2) = '99') THEN
                BEGIN
                       SELECT v.snif_tnua into p_snif
                        FROM VIW_SNIF_TNUA_FROM_TNUA v
                         WHERE     v.mispar_ishi =p_mispar_ishi
                               AND v.taarich =p_taarich
                               AND v.mispar_sidur = p_mispar_sidur
                               AND v.SHAT_HATCHALA =p_shat_hatchala;
                              
                    EXCEPTION
                          WHEN NO_DATA_FOUND THEN
                            p_snif:=0;
                    END;
            end if;      
         end if;
                           
        RETURN  p_snif;
        
        EXCEPTION 
             WHEN OTHERS THEN
                RAISE;      
end GetSnifTnuaBySidur;                        
END Pkg_Sidurim;
/
