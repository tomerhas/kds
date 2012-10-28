CREATE OR REPLACE PACKAGE          Pkg_Batch AS
TYPE    CurType      IS    REF  CURSOR;
/******************************************************************************
   NAME:       PKG_BATCH
   PURPOSE:

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        26/04/2009             1. Created this package.
******************************************************************************/

PROCEDURE pro_ins_log_bakasha(p_bakasha_id  IN TB_LOG_BAKASHOT.bakasha_id%TYPE,
                   p_taarich_idkun  IN TB_LOG_BAKASHOT.taarich_idkun_acharon%TYPE,
                 p_sug_hodaa  IN TB_LOG_BAKASHOT.sug_hodaa%TYPE,
                 p_kod_tahalich IN TB_LOG_BAKASHOT.kod_tahalich%TYPE,
                 p_kod_yeshut IN TB_LOG_BAKASHOT.kod_yeshut%TYPE,
              p_mispar_ishi IN TB_LOG_BAKASHOT.mispar_ishi%TYPE,
              p_taarich IN TB_LOG_BAKASHOT.taarich%TYPE,
              p_mispar_sidur IN TB_LOG_BAKASHOT.mispar_sidur%TYPE,
              p_shat_hatchala_sidur IN TB_LOG_BAKASHOT.shat_hatchala_sidur%TYPE,
              p_shat_yetzia IN TB_LOG_BAKASHOT.shat_yetzia%TYPE,
              p_mispar_knisa IN TB_LOG_BAKASHOT.mispar_knisa%TYPE,
              p_teur_hodaa IN TB_LOG_BAKASHOT.teur_hodaa%TYPE,
              p_kod_hodaa IN TB_LOG_BAKASHOT.kod_hodaa%TYPE,
              p_mispar_siduri OUT TB_LOG_BAKASHOT.mispar_siduri%TYPE);

PROCEDURE pro_ins_bakasha(p_sug_bakasha  IN TB_BAKASHOT.sug_bakasha%TYPE,
                   p_teur  IN TB_BAKASHOT.teur%TYPE,
                 p_status   IN TB_BAKASHOT.status%TYPE,
                 p_user_id IN TB_BAKASHOT.mishtamesh_id%TYPE,
              p_bakasha_id OUT TB_BAKASHOT.bakasha_id%TYPE);

  PROCEDURE pro_upd_bakasha(p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                   p_status   IN TB_BAKASHOT.status%TYPE,
              p_huavra_lesachar IN  TB_BAKASHOT.huavra_lesachar%TYPE,
              p_zman_siyum   IN  TB_BAKASHOT.zman_siyum%TYPE,
              p_tar_haavara_lesachar   IN  TB_BAKASHOT.taarich_haavara_lesachar%TYPE) ;

 PROCEDURE  pro_upd_bakasha_all_fields(p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                                              p_zman_siyum   IN  TB_BAKASHOT.zman_siyum%TYPE,
                                                              p_status   IN TB_BAKASHOT.status%TYPE,
                                                              p_huavra_lesachar IN  TB_BAKASHOT.huavra_lesachar%TYPE,
                                                              p_tar_haavara_lesachar   IN  TB_BAKASHOT.taarich_haavara_lesachar%TYPE,
                                                              p_ishur_hilan   IN TB_BAKASHOT.ISHUR_HILAN%TYPE );
 PROCEDURE pro_ins_bakasha_param(p_bakasha_id  IN TB_BAKASHOT_PARAMS.bakasha_id%TYPE,
                   p_param_id  IN  TB_BAKASHOT_PARAMS.param_id%TYPE,
                 p_erech   IN  TB_BAKASHOT_PARAMS.erech%TYPE);

 PROCEDURE pro_get_pirtey_ritzot(p_taarich_me IN TB_BAKASHOT.zman_hatchala%TYPE,
             p_taarich_ad IN TB_BAKASHOT.zman_hatchala%TYPE,
            p_get_all NUMBER,
            p_cur OUT CurType);
FUNCTION fun_get_status_sachar(p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE) RETURN NUMBER;
FUNCTION fun_get_status_bakasha(p_sug_bakasha NUMBER,p_Param_id NUMBER,p_erech VARCHAR) RETURN NUMBER;
FUNCTION fun_get_rizot_zehot_lesachar(p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE) RETURN VARCHAR;
  PROCEDURE pro_get_ovdim_lechishuv(p_chodesh IN DATE,
             p_maamad IN NUMBER,
            p_ritza_gorefet IN NUMBER,
            p_cur OUT CurType) ;

  PROCEDURE pro_get_ovdim_to_transfer(p_request_id IN  TB_BAKASHOT.bakasha_id%TYPE,
                 p_cur_list OUT CurType,
            p_cur OUT CurType,
            p_cur_prem OUT CurType);

   PROCEDURE pro_get_chishuv_yomi(p_request_id IN  TB_BAKASHOT.bakasha_id%TYPE,
                   p_mispar_ishi IN  TB_CHISHUV_YOMI_OVDIM.MISPAR_ISHI%TYPE,
                    p_taarich IN TB_CHISHUV_YOMI_OVDIM.TAARICH%TYPE,
              p_cur OUT CurType);

    ----------------
PROCEDURE pro_del_chishuv_after_transfer(p_request_id IN  TB_BAKASHOT.bakasha_id%TYPE);
----------------
PROCEDURE pro_upd_status_yamey_avoda(p_request_id IN  TB_BAKASHOT.bakasha_id%TYPE); 
PROCEDURE pro_ins_yamey_avoda_ovdim ;
     PROCEDURE pro_ins_yamey_avoda_ovdim(pDt VARCHAR);
        PROCEDURE pro_get_premiot_ovdim(pYM VARCHAR,p_Cur OUT CurType);
     PROCEDURE pro_upd_yamey_avoda_ovdim(pDt VARCHAR);
 --         PROCEDURE pro_upd_yamey_avoda_1oved(pDt varchar,pIshi varchar);
      PROCEDURE pro_get_my_attendance(pIshi VARCHAR,pDt VARCHAR,p_Cur OUT CurType);
      PROCEDURE pro_pivot(p_ishi VARCHAR,p_Dt_from VARCHAR,p_Dt_to VARCHAR,p_Cur OUT CurType);
           PROCEDURE pro_pivot_1Day(p_ishi VARCHAR,p_Dt VARCHAR,p_Cur OUT CurType);
     PROCEDURE pro_ins_log_tahalich(p_KodTahalich  NUMBER  ,p_KodPeilut NUMBER ,  p_KodStatus  NUMBER ,  p_TeurTech VARCHAR  ) ;
      PROCEDURE pro_ins_log_tahalich_takala(p_KodTahalich  NUMBER  ,p_KodPeilut NUMBER,  p_KodStatus  NUMBER ,  p_TeurTech VARCHAR  ,p_KodTakala NUMBER) ;
	  PROCEDURE pro_GetListDs(pDt VARCHAR, pIshi VARCHAR ,psidur VARCHAR,p_cur OUT CurType) ;
         PROCEDURE pro_GetRowDt(pDt VARCHAR, p_cur OUT CurType) ;
      PROCEDURE pro_GetRowDtLong(pDt VARCHAR, p_cur OUT CurType);
        PROCEDURE pro_GetRowDtVeryLong(pDt VARCHAR, p_cur OUT CurType) ;
        PROCEDURE pro_GetRowDtVeryLong2(pDt VARCHAR, phatchala VARCHAR,pIshi VARCHAR ,psidur VARCHAR,p_cur OUT CurType)  ;
        PROCEDURE pro_GetRowDtVeryLongPundakim2(pDt VARCHAR, phatchala VARCHAR,pIshi VARCHAR ,p_cur OUT CurType)  ; 
	   PROCEDURE pro_RefreshMv(shem_mvew VARCHAR)  ;
         PROCEDURE InsIntoTrailKnisa(pDt VARCHAR, pDt_N_KNISA VARCHAR,SRV_D_ISHI NUMBER,calc_D_new_sidur NUMBER,P24 NUMBER)  ;
     PROCEDURE InsIntoTrailYetzia(pDt VARCHAR, pDt_N_YETZIA VARCHAR,SRV_D_ISHI NUMBER,calc_D_new_sidur NUMBER,P24 NUMBER)  ;
    PROCEDURE pro_get_yamei_avoda_meshek( p_date DATE, p_bakasha_id NUMBER, p_Cur OUT CurType);
      PROCEDURE pro_get_all_yamei_avoda(  p_Cur OUT CurType);
   PROCEDURE pro_new_rec(  SRV_D_ISHI VARCHAR,  SRV_D_TAARICH VARCHAR,  calc_D_new_sidur VARCHAR,
      SRV_D_KNISA_X VARCHAR,  SRV_D_MIKUM_KNISA VARCHAR,  SRV_D_SIBAT_DIVUACH_KNISA VARCHAR,
      SRV_D_YETZIA_X VARCHAR,  SRV_D_MIKUM_YETZIA VARCHAR,  SRV_D_SIBAT_DIVUACH_YETZIA VARCHAR,
      SRV_D_ISHI_MEADKEN VARCHAR,
   SRV_D_KOD_BITUL_ZMAN_NESIA_X VARCHAR,
      SRV_D_KOD_CHARIGA_X VARCHAR,  SRV_D_KOD_HALBASHA_X VARCHAR,  SRV_D_KOD_HAZMANA_X VARCHAR,
      TAARICH_knisa_p24 NUMBER,  TAARICH_yetzia_p24 NUMBER,  DatEfes VARCHAR,
      TAARICH_knisa_letashlum_p24 NUMBER,  SRV_D_KNISA_letashlum_X VARCHAR,
      TAARICH_yetzia_letashlum_p24 NUMBER,  SRV_D_YETZIA_letashlum_X VARCHAR) ;
   PROCEDURE pro_get_yamei_avoda_shinui_hr(p_date DATE, p_bakasha_id NUMBER, p_Cur OUT CurType);
       PROCEDURE pro_measher_o_mistayeg(  SRV_D_ISHI VARCHAR,  SRV_D_TAARICH VARCHAR,  TAARICH_knisa_p24 NUMBER );
          PROCEDURE pro_lo_letashlum(  SRV_D_ISHI VARCHAR,  SRV_D_TAARICH VARCHAR,  TAARICH_knisa_p24 NUMBER ) ;
       PROCEDURE pro_upd_out_blank(  SRV_D_ISHI VARCHAR,  SRV_D_TAARICH VARCHAR,  calc_D_new_sidur VARCHAR,
      SRV_D_KNISA_X VARCHAR,  SRV_D_MIKUM_KNISA VARCHAR,  SRV_D_SIBAT_DIVUACH_KNISA VARCHAR,
      SRV_D_YETZIA_X VARCHAR,  SRV_D_MIKUM_YETZIA VARCHAR,  SRV_D_SIBAT_DIVUACH_YETZIA VARCHAR,
      SRV_D_ISHI_MEADKEN VARCHAR,
   SRV_D_KOD_BITUL_ZMAN_NESIA_X VARCHAR,
      SRV_D_KOD_CHARIGA_X VARCHAR,  SRV_D_KOD_HALBASHA_X VARCHAR,  SRV_D_KOD_HAZMANA_X VARCHAR,
      TAARICH_knisa_p24 NUMBER , TAARICH_yetzia_p24 NUMBER,  DatEfes VARCHAR,
      TAARICH_knisa_letashlum_p24 NUMBER,  SRV_D_KNISA_letashlum_X VARCHAR,
      TAARICH_yetzia_letashlum_p24 NUMBER,  SRV_D_YETZIA_letashlum_X VARCHAR) ;
   PROCEDURE pro_upd_in_blank(  SRV_D_ISHI VARCHAR,  SRV_D_TAARICH VARCHAR,  calc_D_new_sidur VARCHAR,
      SRV_D_KNISA_X VARCHAR,  SRV_D_MIKUM_KNISA VARCHAR,  SRV_D_SIBAT_DIVUACH_KNISA VARCHAR,
      SRV_D_YETZIA_X VARCHAR,  SRV_D_MIKUM_YETZIA VARCHAR,  SRV_D_SIBAT_DIVUACH_YETZIA VARCHAR,
      SRV_D_ISHI_MEADKEN VARCHAR,
   SRV_D_KOD_BITUL_ZMAN_NESIA_X VARCHAR,
      SRV_D_KOD_CHARIGA_X VARCHAR,  SRV_D_KOD_HALBASHA_X VARCHAR,  SRV_D_KOD_HAZMANA_X VARCHAR,
      TAARICH_knisa_p24 NUMBER , TAARICH_yetzia_p24 NUMBER,  DatEfes VARCHAR,
      TAARICH_knisa_letashlum_p24 NUMBER,  SRV_D_KNISA_letashlum_X VARCHAR,
      TAARICH_yetzia_letashlum_p24 NUMBER,  SRV_D_YETZIA_letashlum_X VARCHAR) ;
     PROCEDURE pro_upd_in_out_letashlum(  SRV_D_ISHI VARCHAR,  SRV_D_TAARICH VARCHAR,  calc_D_new_sidur VARCHAR,
      SRV_D_KNISA_X VARCHAR,
   --SRV_D_MIKUM_KNISA varchar,  SRV_D_SIBAT_DIVUACH_KNISA varchar,
      SRV_D_YETZIA_X VARCHAR,
   --SRV_D_MIKUM_YETZIA varchar,  SRV_D_SIBAT_DIVUACH_YETZIA varchar,
      SRV_D_ISHI_MEADKEN VARCHAR,
   SRV_D_KOD_BITUL_ZMAN_NESIA_X VARCHAR,
      --SRV_D_KOD_CHARIGA_X varchar,
   SRV_D_KOD_HALBASHA_X VARCHAR,
   --SRV_D_KOD_HAZMANA_X varchar,
      TAARICH_knisa_p24 NUMBER , TAARICH_yetzia_p24 NUMBER,  DatEfes VARCHAR,
      TAARICH_knisa_letashlum_p24 NUMBER,  SRV_D_KNISA_letashlum_X VARCHAR,
      TAARICH_yetzia_letashlum_p24 NUMBER,  SRV_D_YETZIA_letashlum_X VARCHAR) ;
    PROCEDURE  pro_ins_yamey_avoda_1oved(SRV_D_ISHI NUMBER,  SRV_D_TAARICH VARCHAR) ;
  PROCEDURE pro_new_rec_pundakim(  SRV_D_ISHI VARCHAR,  SRV_D_TAARICH VARCHAR,
                                     SRV_D_KNISA_X VARCHAR,  SRV_D_MIKUM_KNISA VARCHAR,   TAARICH_knisa_p24 NUMBER) ;
  PROCEDURE pro_GetListDsPundakim(pDt VARCHAR, pIshi VARCHAR ,p_cur OUT CurType) ;
 	 PROCEDURE pro_insert_debug_maatefet(p_mispar_ishi NUMBER, p_taarich DATE, p_taarich_ritza DATE,
                                    p_bakasha_id NUMBER, p_sug_bakasha NUMBER, 
                                    p_comments TEST_MAATEFET.comments%TYPE DEFAULT NULL);
	 PROCEDURE pro_IfSdrnManas(pDt VARCHAR,pIshi VARCHAR ,p_cur OUT CurType) ;
PROCEDURE pro_get_shinuy_matsav_ovdim(p_Cur OUT CurType);
PROCEDURE pro_get_Shinuy_meafyeney_bizua(p_Cur OUT CurType) ;
PROCEDURE pro_get_shinuy_pirey_oved( p_Cur OUT CurType);
PROCEDURE pro_get_shinuy_brerot_mechdal( p_Cur OUT CurType);
PROCEDURE pro_ins_ovdim_im_shinuy_hr(p_coll_obj_ovdim_im_shinuy_hr IN coll_ovdim_im_shinuy_hr);
PROCEDURE pro_ins_defaults_hr(p_coll_obj_brerot_mechdal_hr IN coll_brerot_mechdal_meafyenim);

PROCEDURE inset_oved_im_shinuy_hr(p_mispar_ishi IN NUMBER,
		  											   	   	   			  		 p_taarich IN DATE,p_tavla IN VARCHAR)	;
PROCEDURE update_oved_im_shinuy_hr(p_mispar_ishi IN NUMBER,
		  											   	   	   			  		 p_taarich IN DATE,
																				 p_tavla IN VARCHAR );

PROCEDURE MoveNewMatzavOvdimToOld ;
PROCEDURE MoveNewPirteyOvedToOld ;
PROCEDURE MoveNewMeafyenimOvdimToOld ;
PROCEDURE MoveNewBrerotMechdalToOld ;																			 
PROCEDURE pro_get_ovdim4rerunsdrn( pDt VARCHAR,p_cur OUT CurType) ;
 PROCEDURE pro_sof_meafyenim( pDt VARCHAR,p_Cur OUT CurType);
PROCEDURE pro_get_premia_input(p_taarich DATE, p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE,p_Cur OUT CurType);
PROCEDURE pro_update_calc_premia(p_taarich DATE, p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE, p_mispar_ishi NUMBER,
            p_kod_rechiv NUMBER, p_erech_rechiv NUMBER);
PROCEDURE pro_get_premia_bakashot(p_taarich DATE,p_Cur OUT CurType);
 PROCEDURE pro_if_start(p_Cur OUT CurType);
  PROCEDURE pro_if_GalreadyRun(p_Cur OUT CurType) ;
PROCEDURE pro_get_ovdim_lehishuv_premiot(p_Cur OUT CurType);
/*procedure pro_update_chishuv_premia(p_bakasha_id tb_bakashot.bakasha_id%type,
            p_mispar_ishi OVDIM_LECHISHUV_PREMYOT.MISPAR_ISHI%type,
            p_chodesh ovdim_lechishuv_premyot.chodesh%type);*/
PROCEDURE pro_update_chishuv_premia(p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE);

FUNCTION fun_get_num_changes_to_shguim RETURN NUMBER;
FUNCTION pro_ins_log_tahalich_rec(p_KodTahalich  NUMBER  ,p_KodPeilut NUMBER,  
		  										  		  			   			p_KodStatus  NUMBER ,  p_TeurTech VARCHAR ,p_KodTakala NUMBER ) RETURN NUMBER ;
	 PROCEDURE pro_upd_log_tahalich_rec(p_seqTahalich NUMBER ,p_KodStatus NUMBER,  p_TeurTech VARCHAR ,p_KodTakala NUMBER ) ;	
 PROCEDURE pro_delete_log_tahalich_rcds;		
 PROCEDURE  pro_upd_yamimOfSdrn   ;				
  PROCEDURE pro_get_meafyenim_gap(p_num_process NUMBER,   p_cur OUT CurType);
   PROCEDURE pro_get_meafyenim_manygaps(p_num_process NUMBER,   p_cur OUT CurType) ;

	 PROCEDURE Pro_get_pirtey_ovdim_leRikuzim(p_bakasha_id NUMBER, p_cur OUT CurType);
       PROCEDURE Pro_get_Email_Ovdim_LeRikuzim(p_bakasha_id NUMBER, p_cur OUT CurType);
       
   FUNCTION  pro_get_sug_chishuv(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                                    p_taarich IN DATE,
                                                    p_bakasha_id OUT TB_BAKASHOT.bakasha_id%TYPE ) RETURN NUMBER;  
                                                    
PROCEDURE pro_ins_misparishi_sug_chishuv(p_bakasha_id NUMBER,p_coll_chishuv_sug_sidur IN COLL_MISPAR_ISHI_SUG_CHISHUV);		
PROCEDURE Prepare_yamei_avoda_meshek(p_date IN DATE, p_type IN NUMBER,p_num_process IN NUMBER, p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE) ;		
PROCEDURE Prepare_yamei_avoda_shinui_hr(p_type IN NUMBER,p_num_process IN NUMBER, p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE);
PROCEDURE pro_divide_packets( p_num_process IN  NUMBER,p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE );	
PROCEDURE pro_get_netunim_for_process( p_num_process IN  NUMBER ,  p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE, p_cur OUT CurType);	
PROCEDURE pro_delete_tb_shguim_batch(	p_num_process IN  NUMBER ,  p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE);		
PROCEDURE Prepare_premiot_shguim_batch(p_type IN NUMBER,p_num_process IN NUMBER, p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE);
PROCEDURE Pro_Save_Rikuz_Pdf( p_BakashatId TB_RIKUZ_PDF.bakasha_id%TYPE,p_coll_rikuz_pdf IN COLL_RIKUZ_PDF);
PROCEDURE Pro_Get_Rikuz_Pdf(p_mispar_ishi IN NUMBER,p_taarich IN DATE,p_BakashatId IN NUMBER, p_cur OUT CurType); --p_rikuz OUT BLOB);

FUNCTION pro_check_view_empty(p_TableName VARCHAR2) RETURN NUMBER;
PROCEDURE DeleteBakashotYeziratRikuzim(p_BakashatId TB_BAKASHOT.bakasha_id%TYPE);
    PROCEDURE pro_Get_Makatim_LeTkinut(p_date IN DATE, p_cur OUT CurType); 
	 PROCEDURE pro_retrospect_yamey_avoda;
   
END Pkg_Batch;
/


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
PROCEDURE pro_prepare_netunim_lechishuv(p_tar_me IN DATE,p_tar_ad IN DATE,
                                    p_maamad IN NUMBER, p_ritza_gorefet IN NUMBER, p_num_processe IN  NUMBER);
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
PROCEDURE pro_get_premia_yadanit(  p_Cur OUT CurType ,p_num_process IN NUMBER);

PROCEDURE pro_get_sug_yechida(  p_cur OUT CurType, p_num_process IN NUMBER);		

PROCEDURE pro_get_sugey_sidur_tnua(  p_Cur OUT CurType);	
									 
PROCEDURE pro_get_buses_details(  p_Cur OUT CurType, p_num_process IN NUMBER ,p_tar_me IN DATE,p_tar_ad IN DATE);	

PROCEDURE pro_get_yemey_avoda ( p_status_tipul  IN  TB_YAMEY_AVODA_OVDIM.status_tipul%TYPE,
													p_num_process IN NUMBER,p_cur OUT CurType,p_tar_me IN DATE,p_tar_ad IN DATE);
PROCEDURE pro_InsertYamimLeTavla(p_tar_me IN DATE,p_tar_ad IN DATE, p_num_process IN NUMBER);
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


CREATE OR REPLACE PACKAGE          PKG_OVDIM AS
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
TYPE    CurType      IS    REF  CURSOR;
PROCEDURE pro_get_error_ovdim(p_kod_hevra in ctb_hevra.kod_hevra%type, p_kod_ezor in ctb_ezor.kod_ezor%type, p_kod_snif in ctb_snif_av.kod_snif_av%type, p_kod_maamad in ctb_maamad.kod_maamad_hr%type,
                              p_from_date in date, p_to_date in date,
                              p_cur OUT CurType);

PROCEDURE pro_get_oved_full_name(p_mispar_ishi in ovdim.mispar_ishi%type, p_full_name out ovdim.shem_mish%type);
PROCEDURE pro_get_ovedim_mispar_ishi(p_prefix in varchar2, p_misparim_range in varchar2, p_cur out CurType);
PROCEDURE pro_get_Active_Workers(p_prefix in varchar2, p_FromDate in date,p_ToDate in date , p_cur out CurType);
PROCEDURE pro_get_oved_errors_cards(p_mispar_ishi in ovdim.mispar_ishi%type,
		  										  	 						 		  p_from in tb_yamey_avoda_ovdim.taarich%type,
																					  p_to  in tb_yamey_avoda_ovdim.taarich%type,
																					  p_cur out CurType);
PROCEDURE pro_get_oved_details(p_mispar_ishi in ovdim.mispar_ishi%type,p_from_taarich in date, p_cur out CurType );
PROCEDURE pro_get_ovedim_by_name(p_prefix in varchar2, p_misparim_range in varchar2, p_cur out CurType);
PROCEDURE pro_get_oved_mispar_ishi(p_name in varchar2, p_mispar_ishi out ovdim.mispar_ishi%type);

PROCEDURE pro_get_pirtey_oved(p_mispar_ishi in ovdim.mispar_ishi%type, p_taarich IN DATE,p_cur out CurType);
function   pro_get_meafyen_leoved_by_ez(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE, p_kod_meafyen in number,p_taarich IN DATE) return varchar2;
PROCEDURE pro_get_sug_chishuv(p_mispar_ishi in ovdim.mispar_ishi%type,
    		 				  p_taarich in date,
							  p_bakasha_id OUT tb_bakashot.bakasha_id%type,
							  p_sug_chishuv OUT number,
							  p_taarich_chishuv OUT DATE);

PROCEDURE pro_get_headruyot(p_cur out CurType);

PROCEDURE pro_get_pirtey_headrut(p_mispar_ishi in ovdim.mispar_ishi%type,
    						     p_taarich in date,
								 p_bakasha_id IN tb_bakashot.bakasha_id%type,
								 p_cur out CurType);

PROCEDURE pro_get_pirtey_headrut_tmp(p_mispar_ishi in ovdim.mispar_ishi%type,
    						     p_taarich in date,
								 p_bakasha_id IN tb_bakashot.bakasha_id%type,
								 p_cur out CurType);

PROCEDURE pro_get_rechivim_codshiyim(p_mispar_ishi in ovdim.mispar_ishi%type,
  								 	 p_taarich in date,
									 p_bakasha_id IN tb_bakashot.bakasha_id%type,
									 p_tzuga IN CTB_Rechivim.Letzuga_Besikum_Chodshi%type,
									 p_cur out CurType);
PROCEDURE pro_get_rechivim_codshiyim_tmp(p_mispar_ishi in ovdim.mispar_ishi%type,
  								 	 p_taarich in date,
									 p_bakasha_id IN tb_bakashot.bakasha_id%type,
									 p_tzuga IN CTB_Rechivim.Letzuga_Besikum_Chodshi%type,
									 p_cur out CurType);

PROCEDURE pro_get_rikuz_chodshi(p_mispar_ishi in ovdim.mispar_ishi%type,
							  	p_taarich in date,
     	 				        p_bakasha_id IN tb_bakashot.bakasha_id%type,
								p_tzuga IN CTB_Rechivim.Letzuga_Besikum_Chodshi%type,
								p_cur out CurType);

PROCEDURE pro_get_rikuz_chodshi_tmp(p_mispar_ishi in ovdim.mispar_ishi%type,
							  	p_taarich in date,
     	 				        p_bakasha_id IN tb_bakashot.bakasha_id%type,
								p_tzuga IN CTB_Rechivim.Letzuga_Besikum_Chodshi%type,
								p_cur out CurType);

PROCEDURE pro_get_rec_pirtey_oved(p_mispar_ishi in ovdim.mispar_ishi%type,
       							  p_taarich in date,
	    						  p_cur out CurType);

PROCEDURE pro_upd_premia_details(p_kod_premia in TRAIL_PREMYOT_YADANIYOT.SUG_PREMYA%type,
		  									  	 								p_mispar_ishi in TRAIL_PREMYOT_YADANIYOT.MISPAR_ISHI_TRAIL%type,
																				p_taarich in TRAIL_PREMYOT_YADANIYOT.TAARICH_IDKUN_TRAIL%type,
																				p_dakot_premia in TRAIL_PREMYOT_YADANIYOT.DAKOT_PREMYA%type,
																				p_mispar_ishi_of_meadken in TRAIL_PREMYOT_YADANIYOT.MISPAR_ISHI_TRAIL%type);

function fun_get_BakashaId(p_misparIshi in OVDIM.MISPAR_ISHI%type ,
                                         p_taarich in TB_CHISHUV_CHODESH_OVDIM.TAARICH%type )
                                         return TB_BAKASHOT.BAKASHA_ID%type;

FUNCTION fun_get_meafyen_oved(p_mispar_ishi in meafyenim_ovdim.mispar_ishi%type,
						 	  p_kod_meafyen  in meafyenim_ovdim.kod_meafyen%type,
							  p_taarich  in meafyenim_ovdim.me_taarich%type) return  meafyenim_ovdim.erech_ishi%type;

PROCEDURE pro_get_oved_snif_unit(p_mispar_ishi in ovdim.mispar_ishi%type, p_teur_snif out ctb_snif_av.teur_snif_av%type, p_teur_unit out ctb_yechida.teur_yechida%type);

PROCEDURE pro_get_oved_cards(p_mispar_ishi in ovdim.mispar_ishi%type,p_month in varchar2,p_cur out CurType);
FUNCTION fun_get_status_lekartis(p_SHGIOT_LETEZUGA_LAOVED number,p_mispar_ishi number,p_taarich DATE) return varchar;
FUNCTION fun_get_kartis_lelo_peilut(p_mispar_ishi number,p_taarich DATE) return varchar;
PROCEDURE pro_get_oved_cards_in_tipul(p_mispar_ishi in ovdim.mispar_ishi%type,p_cur out CurType);


PROCEDURE pro_get_meafyeney_oved(p_mispar_ishi in meafyenim_ovdim.mispar_ishi%type,
		  									   	        p_taarich in meafyenim_ovdim.me_taarich%type,
														p_cur out CurType);
														
PROCEDURE pro_get_meafyeney_oved_all(p_mispar_ishi in meafyenim_ovdim.mispar_ishi%type,
		  									   	        p_me_taarich in meafyenim_ovdim.me_taarich%type,
														 p_ad_taarich in meafyenim_ovdim.me_taarich%type,
														p_brerat_Mechadal  in number,
														p_cur out CurType) ;
														
PROCEDURE pro_get_historiat_meafyen(p_mispar_ishi in meafyenim_ovdim.mispar_ishi%type,
														p_from_taarich in meafyenim_ovdim.me_taarich%type,
														p_Code_meafyen  in meafyenim_ovdim.KOD_MEAFYEN%type,
														p_cur out CurType) ;

PROCEDURE pro_get_month_year_to_oved(p_mispar_ishi in TB_Yamey_Avoda_Ovdim.mispar_ishi%type,
														p_cur out CurType);

PROCEDURE pro_get_status_oved(p_mispar_ishi in Matzav_Ovdim.mispar_ishi%type,
														p_cur out CurType);

 PROCEDURE pro_get_pirtey_oved_all(p_mispar_ishi in ovdim.mispar_ishi%type,
 		   										 						    p_taarich in date,
 		   									 							p_cur out CurType) ;
	PROCEDURE pro_get_historiat_natun(p_mispar_ishi in Pirtey_Ovdim.mispar_ishi%type,
														p_from_taarich in Pirtey_Ovdim.me_taarich%type,
														p_Code_natun  in Pirtey_Ovdim.KOD_NATUN%type,
														p_cur out CurType);

	 PROCEDURE pro_get_ritzot_chishuv(p_mispar_ishi in ovdim.mispar_ishi%type,
								 				p_taarich in date,
												p_cur out CurType);

 PROCEDURE pro_get_shaot_meal_michsa(p_mispar_ishi in ovdim.mispar_ishi%type,
 		   										 						  p_taarich in date,
																		  p_bakasha in tb_Chishuv_chodesh_Ovdim.Bakasha_ID%type,
 		   									 							p_cur out CurType) ;

  PROCEDURE pro_ins_bakasha_mechutz_lemich(p_mispar_ishi  in tb_bakashot_michutz_lamichsa.mispar_ishi%type,
																			  p_taarich  in tb_bakashot_michutz_lamichsa.taarich%type,
																			  p_shaot  in tb_bakashot_michutz_lamichsa.mevukash%type,
																			  p_siba  in tb_bakashot_michutz_lamichsa.sibat_habakasha%type,
																			  p_user_id   in tb_bakashot_michutz_lamichsa.mispar_ishi%type);



procedure pro_get_Hour_Approval(p_TypeDemand INTEGER,
                                                             p_mispar_ishi in ovdim.mispar_ishi% TYPE,
                                                             p_Month VARCHAR2,
                                                             p_StatusIsuk integer,
                                                             p_Filter in VARCHAR2,
                                                             p_cur out CurType);

procedure pro_getRelevantMonthOfApproval(p_mispar_ishi in ovdim.mispar_ishi% TYPE,
                                                             p_cur out CurType);
procedure pro_upd_Hour_Aproval(p_Bakasha_ID  in number ,
                                                    p_kod_status_ishur in number ,  p_MISPAR_ISHI IN  TB_ISHURIM.MISPAR_ISHI% type, 
                                                    p_KOD_ISHUR IN  TB_ISHURIM.kod_ishur% type,
                                                    p_TAARICH IN  TB_ISHURIM.TAARICH%type, 
                                                    p_MISPAR_SIDUR IN  TB_ISHURIM.MISPAR_SIDUR%type, 
                                                    p_SHAT_HATCHALA IN  TB_ISHURIM.SHAT_HATCHALA%type, 
                                                    p_SHAT_YETZIA IN  TB_ISHURIM.SHAT_YETZIA%type, 
                                                    p_MISPAR_KNISA IN  TB_ISHURIM.MISPAR_KNISA%type, 
                                                    p_RAMA IN  TB_ISHURIM.RAMA%type, 
                                                    p_ERECH_MEUSHAR IN  TB_ISHURIM.ERECH_MEUSHAR%type, 
                                                    p_ERECH_MEVUKASH IN TB_ISHURIM.ERECH_MEVUKASH%type ,
                                                    P_SIBA IN TB_ISHURIM.SIBA%type ,
                                                    P_MAINFACTOR NUMBER, 
                                                    P_SECONDARYFACTOR NUMBER ,
                                                    p_Result out INTEGER) ;
  
PROCEDURE pro_get_SharedMonthly_Quota(p_mispar_ishi in ovdim.mispar_ishi% TYPE,p_Period in VARCHAR2,p_Quota OUT TB_MICHSA_AGAPIT .MICHSA_AGAPIT%type,p_SharedQuota out INTEGER) ;

PROCEDURE pro_get_Status_Isuk(p_mispar_ishi in ovdim.mispar_ishi% TYPE,p_Form in INTEGER, p_Month VARCHAR2,p_Result OUT INTEGER);


PROCEDURE pro_get_tmp_pirtey_oved(p_mispar_ishi in ovdim.mispar_ishi%type, p_taarich in date,
 		   									 							p_cur out CurType) ;

FUNCTION fun_get_sug_yechida_oved(p_mispar_ishi in ovdim.mispar_ishi%type,
														p_taarich  in pivot_Pirtey_Ovdim.me_tarich%type) return ctb_yechida.SUG_YECHIDA%type;

PROCEDURE pro_get_merkaz_erua_ByKod(p_mispar_ishi in pirtey_ovdim.mispar_ishi%type,
                                   p_kod_natun in pirtey_ovdim.kod_natun%type,
								      p_taarich in pirtey_ovdim.ME_TAARICH%type,
                                   P_erech out pirtey_ovdim.erech%type);

PROCEDURE pro_get_mikum_shaon_in_out(p_mispar_ishi in TB_SIDURIM_OVDIM.mispar_ishi%type,
                                   p_taarich in date ,
                                   p_mikum_shaon_knisa  out integer   ,
                                   p_mikum_shaon_yetzia out integer) ;


PROCEDURE pro_get_zman_nesiaa_mistane (p_merkaz_erua in CTB_ZMAN_NSIAA_MISHTANE.MERKAZ_ERUA%type,
                                       p_mikum_yaad  in CTB_ZMAN_NSIAA_MISHTANE.MIKUM_YAAD%type,
                                       p_taarich in date ,
                                       p_dakot   out integer);

PROCEDURE pro_get_zman_nesiaa_ovdim (p_mispar_ishi in TB_YAMEY_AVODA_OVDIM.mispar_ishi%type,
                                     p_taarich in date ,
                                     p_zman_nesia_haloch  out integer   ,
                                     p_zman_nesia_hazor   out integer)  ;

PROCEDURE pro_upd_zman_nesiaa (p_mispar_ishi in TB_YAMEY_AVODA_OVDIM.mispar_ishi%type,
                                     p_taarich in date ,
                                     p_zman_nesia_haloch  in integer   ,
                                     p_zman_nesia_hazor   in integer,      
                                     p_meadken_acharon IN  number) ;

PROCEDURE pro_get_last_updator (p_mispar_ishi in TB_YAMEY_AVODA_OVDIM.mispar_ishi%type,
                                p_taarich in date ,
                                p_cur out CurType) ;


PROCEDURE pro_get_RECHIVIM(   p_cur OUT CurType) ;

PROCEDURE pro_get_months_huavar_lesachar(p_mispar_ishi in TB_Yamey_Avoda_Ovdim.mispar_ishi%type,
														p_cur out CurType);

PROCEDURE pro_save_employee_card(p_coll_yamey_avoda_ovdim in coll_yamey_avoda_ovdim,
                                 p_coll_sidurim_ovdim_upd in coll_sidurim_ovdim,
                                 p_coll_obj_peilut_ovdim  in coll_obj_peilut_ovdim,
                                 p_coll_sidurim_ovdim_ins in coll_sidurim_ovdim,
                                 p_coll_sidurim_ovdim_del in coll_sidurim_ovdim,
                                 p_coll_peilut_ovdim_del  in coll_obj_peilut_ovdim,
                                 p_coll_idkun_rashemet in coll_idkun_rashemet,
                                 p_coll_peilut_ovdim_ins  in coll_obj_peilut_ovdim );

PROCEDURE pro_upd_peilut_ovdim(p_coll_obj_peilut_ovdim in coll_obj_peilut_ovdim);
PROCEDURE pro_upd_peilut_oved(p_obj_peilut_ovdim in obj_peilut_ovdim);
PROCEDURE pro_ins_peilut_ovdim_trail(p_obj_peilut_ovdim in obj_peilut_ovdim, p_sug_peula in trail_peilut_ovdim.sug_peula%type);
PROCEDURE pro_upd_sidurim_ovdim(p_coll_sidurim_ovdim in COLL_SIDURIM_OVDIM);
PROCEDURE pro_upd_sidur_oved(p_obj_sidurim_ovdim in obj_sidurim_ovdim);
PROCEDURE pro_upd_tb_sidur_oved(p_obj_sidurim_ovdim in obj_sidurim_ovdim);
PROCEDURE pro_ins_sidurim_ovdim_trail(p_obj_sidurim_ovdim in obj_sidurim_ovdim,p_sug_peula in trail_sidurim_ovdim.sug_peula%type);

PROCEDURE pro_ins_yemey_avoda_leoved(p_mispar_ishi in tb_yamey_avoda_ovdim.mispar_ishi%type,
																	p_taarich in tb_yamey_avoda_ovdim.taarich%type,
																	p_measher_mistayeg in tb_yamey_avoda_ovdim.measher_o_mistayeg%type,
																	p_status in tb_yamey_avoda_ovdim.status_tipul%type,
																	p_meadken in tb_yamey_avoda_ovdim.meadken_acharon%type);

PROCEDURE pro_get_yemey_avoda(p_mispar_ishi in  tb_yamey_avoda_ovdim.mispar_ishi%type,
														p_taarich_me  in  tb_yamey_avoda_ovdim.TAARICH%type,
														p_taarich_ad  in  tb_yamey_avoda_ovdim.TAARICH%type,
                                                        p_cur out CurType) ;

PROCEDURE  pro_get_MisparIshiByKodVeErech (p_kod_Natun in integer  ,
  			 					   				   			  	                p_Erech in varchar2,
																				p_Taarich in date,
																				p_preFix in varchar2,
																				p_cur out CurType) ;

PROCEDURE pro_get_peiluyot_le_oved(p_mispar_ishi in  integer,
		  										 	 		 		 		  	  p_taarich in  TB_PEILUT_OVDIM.TAARICH%type,
																					  p_mispar_sidur in  integer,
																					p_shat_hatchala in  TB_PEILUT_OVDIM.SHAT_HATCHALA_SIDUR%type,
																					p_cur out CurType) ;
PROCEDURE pro_upd_yamey_avoda_ovdim(p_coll_yamey_avoda_ovdim in coll_yamey_avoda_ovdim);
PROCEDURE pro_upd_sadot_nosafim(p_coll_yamey_avoda_ovdim in coll_yamey_avoda_ovdim,
		  												 	p_coll_sidurim_ovdim in coll_sidurim_ovdim,
															p_coll_obj_peilut_ovdim in coll_obj_peilut_ovdim) ;
PROCEDURE pro_upd_yom_avoda_oved(p_obj_yamey_avoda_ovdim in obj_yamey_avoda_ovdim);
PROCEDURE pro_ins_idkuney_rashemet(p_coll_idkun_rashemet in coll_idkun_rashemet);
PROCEDURE pro_ins_idkun_rashemet(p_obj_idkun_rashemet in obj_idkun_rashemet);
PROCEDURE pro_upd_idkun_rashemet(p_obj_idkun_rashemet in obj_idkun_rashemet);
PROCEDURE pro_upd_idkun_rashemet_peilut(p_obj_idkun_rashemet in obj_idkun_rashemet);
PROCEDURE pro_get_pirtey_hitkashrut_oved(p_mispar_ishi in ovdim.mispar_ishi%type,
 		   									 							p_cur out CurType) ;
PROCEDURE pro_get_idkuney_rashemet(p_mispar_ishi in ovdim.mispar_ishi%type,p_taarich in tb_yamey_avoda_ovdim.taarich%type,p_cur out CurType);
PROCEDURE pro_get_meadken_acharon(p_mispar_ishi in ovdim.mispar_ishi%type,p_date in tb_yamey_avoda_ovdim.taarich%type,p_cur out CurType);
PROCEDURE pro_update_measher_o_mistayeg(p_mispar_ishi in ovdim.mispar_ishi%type,p_date in tb_yamey_avoda_ovdim.taarich%type, p_status in tb_yamey_avoda_ovdim.measher_o_mistayeg%type);
PROCEDURE pro_get_oved_details_betkufa(p_mispar_ishi in ovdim.mispar_ishi%type,
		  										p_start_date in date,p_end_date  in date, p_cur out CurType );
PROCEDURE	pro_get_ovdim_to_period_ByCode(p_code in  number,p_start_date in date,
												  	  					  	 	  		  			  p_end_date  in date, p_cur out CurType );
PROCEDURE pro_save_measher_O_mistayeg(p_mispar_ishi in ovdim.mispar_ishi%type,p_date in tb_yamey_avoda_ovdim.taarich%type,p_status number);
PROCEDURE pro_get_pakad_id(p_masach_id in tb_masach.masach_id%type,p_cur out CurType );
FUNCTION fun_is_card_exists_yemey_avoda(p_mispar_ishi in  tb_yamey_avoda_ovdim.mispar_ishi%type,
                                        p_taarich  in  tb_yamey_avoda_ovdim.TAARICH%type) return number;
 
FUNCTION fun_check_peilut_exist(p_mispar_sidur in tb_peilut_ovdim.mispar_sidur%type,
		 									   	  					  p_mispar_ishi in  tb_peilut_ovdim.mispar_ishi%type,
		 									  						  p_shat_hatchala in  tb_peilut_ovdim.shat_hatchala_sidur%type,
											 						  p_shat_yezia in  tb_peilut_ovdim.shat_yetzia%type ) return number;     
 FUNCTION	fun_check_sidur_exist(p_mispar_sidur in tb_sidurim_ovdim.mispar_sidur%type,
 												 		               p_mispar_ishi in  tb_sidurim_ovdim.mispar_ishi%type,
		 									                           p_shat_hatchala in  tb_sidurim_ovdim.shat_hatchala%type ) return number;   
																	   
PROCEDURE pro_del_knisot_peilut( p_mispar_ishi in  tb_peilut_ovdim.mispar_ishi%type,
		 									 p_mispar_sidur in tb_peilut_ovdim.mispar_sidur%type,
											 p_taarich in  tb_peilut_ovdim.taarich%type,
		 									   p_shat_hatchala in  tb_peilut_ovdim.shat_hatchala_sidur%type,
											 p_shat_yezia in  tb_peilut_ovdim.shat_yetzia%type,
											 p_makat_nesia in  tb_peilut_ovdim.makat_nesia%type,
                                             p_meadken_acharon IN  TB_PEILUT_OVDIM.meadken_acharon%TYPE);
	
	           
PROCEDURE pro_del_hachanot_mechona( p_mispar_ishi in  tb_peilut_ovdim.mispar_ishi%type,
		 									 p_mispar_sidur in tb_peilut_ovdim.mispar_sidur%type,
											 p_taarich in  tb_peilut_ovdim.taarich%type,
		 		 						     p_shat_hatchala in  tb_peilut_ovdim.shat_hatchala_sidur%type);										 																                                                       

function func_get_mispar_tachana(p_mispar_ishi in  tb_peilut_ovdim.mispar_ishi%type,
                                     p_mispar_sidur in tb_peilut_ovdim.mispar_sidur%type,
                                     p_taarich in  tb_peilut_ovdim.taarich%type,
                                     p_shat_hatchala in  tb_peilut_ovdim.shat_hatchala_sidur%type) return varchar2;

PROCEDURE pro_get_arachim_by_misIshi( p_mispar_ishi in  pirtey_ovdim.mispar_ishi%type,
																				        p_taarich in pirtey_ovdim.me_taarich%type,
																	                    p_cur out CurType);					

function func_get_erech_by_kod_natun(p_mispar_ishi in  pirtey_ovdim.mispar_ishi%type,
										                                     p_kod_natun in pirtey_ovdim.kod_natun%type,
										                                     p_taarich in  pirtey_ovdim.me_taarich%type  ) return varchar2;			
PROCEDURE pro_get_pirtey_oved_letkufot( p_mispar_ishi in  pivot_Pirtey_Ovdim.mispar_ishi%type,
																				        p_start in pivot_Pirtey_Ovdim.me_tarich%type,p_end in pivot_Pirtey_Ovdim.ad_tarich%type,
																	                    p_cur out CurType) ;			
PROCEDURE pro_get_meafyen_oved_letkufot( p_mispar_ishi in  pivot_meafyenim_ovdim.mispar_ishi%type,
																				        p_start in pivot_meafyenim_ovdim.me_taarich%type,p_end in pivot_meafyenim_ovdim.ad_taarich%type,
                                                                                     p_cur out CurType);
PROCEDURE pro_upd_shgiot_meusharot(p_coll_sidurim_ovdim in coll_sidurim_ovdim,
                                   p_coll_peilut_ovdim  in coll_obj_peilut_ovdim);

PROCEDURE pro_del_knisot_without_av(p_mispar_ishi in tb_peilut_ovdim.mispar_ishi%type, p_taarich in tb_peilut_ovdim.taarich%type,p_meadken_acharon IN TB_PEILUT_OVDIM.meadken_acharon%TYPE);
PROCEDURE pro_del_idkuney_rashemet_sidur(p_coll_sidurim_ovdim in coll_sidurim_ovdim, p_type_update in number);
PROCEDURE pro_del_idkuney_rashemet_peilt(p_coll_peiluyot_ovdim in coll_obj_peilut_ovdim);
FUNCTION func_get_next_err_card(p_mispar_ishi in tb_yamey_avoda_ovdim.mispar_ishi%type, p_date in tb_yamey_avoda_ovdim.taarich%type ) return varchar2;
PROCEDURE pro_get_rikuzey_avoda_leoved(p_mispar_ishi IN MATZAV_OVDIM.mispar_ishi%TYPE,p_taarich IN DATE,
                                                        p_cur OUT CurType);      
FUNCTION func_is_card_last_updated(p_mispar_ishi in tb_peilut_ovdim.mispar_ishi%type, p_taarich in tb_peilut_ovdim.taarich%type) return number;

 PROCEDURE get_ovdim_by_Rikuzim(p_preFix IN VARCHAR2,  p_cur OUT CurType);      
 
 FUNCTION fun_get_count_no_val_letashlum(p_tar_me IN DATE,p_tar_ad IN DATE,
                                                                    p_maamad IN NUMBER) return number;
   
PROCEDURE  pro_get_workcad_no_val_letash(p_tar_me IN DATE,p_tar_ad IN DATE,
                                                                    p_maamad IN NUMBER,  p_cur OUT CurType);                                                                                                              																 																	 
END PKG_OVDIM;
/
CREATE OR REPLACE PACKAGE BODY          Pkg_Batch AS
/******************************************************************************
   NAME:       PKG_BATCH
   PURPOSE:

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        26/04/2009             1. Created this package body.
******************************************************************************/

PROCEDURE pro_ins_log_bakasha(p_bakasha_id  IN TB_LOG_BAKASHOT.bakasha_id%TYPE,
                   p_taarich_idkun  IN TB_LOG_BAKASHOT.taarich_idkun_acharon%TYPE,
                 p_sug_hodaa  IN TB_LOG_BAKASHOT.sug_hodaa%TYPE,
                 p_kod_tahalich IN TB_LOG_BAKASHOT.kod_tahalich%TYPE,
                 p_kod_yeshut IN TB_LOG_BAKASHOT.kod_yeshut%TYPE,
              p_mispar_ishi IN TB_LOG_BAKASHOT.mispar_ishi%TYPE,
              p_taarich IN TB_LOG_BAKASHOT.taarich%TYPE,
              p_mispar_sidur IN TB_LOG_BAKASHOT.mispar_sidur%TYPE,
              p_shat_hatchala_sidur IN TB_LOG_BAKASHOT.shat_hatchala_sidur%TYPE,
              p_shat_yetzia IN TB_LOG_BAKASHOT.shat_yetzia%TYPE,
              p_mispar_knisa IN TB_LOG_BAKASHOT.mispar_knisa%TYPE,
              p_teur_hodaa IN TB_LOG_BAKASHOT.teur_hodaa%TYPE,
              p_kod_hodaa IN TB_LOG_BAKASHOT.kod_hodaa%TYPE,
              p_mispar_siduri OUT TB_LOG_BAKASHOT.mispar_siduri%TYPE)
 IS
   v_mispar_siduri TB_LOG_BAKASHOT.mispar_siduri%TYPE;
  BEGIN

      SELECT log_seq.NEXTVAL INTO v_mispar_siduri FROM dual;
   p_mispar_siduri:=v_mispar_siduri;

        INSERT INTO TB_LOG_BAKASHOT
                      (MISPAR_SIDURI,BAKASHA_ID,TAARICH_IDKUN_ACHARON,SUG_HODAA,KOD_TAHALICH,KOD_YESHUT,
         MISPAR_ISHI,TAARICH,MISPAR_SIDUR,SHAT_HATCHALA_SIDUR,SHAT_YETZIA,MISPAR_KNISA,KOD_HODAA,TEUR_HODAA)
   VALUES (v_mispar_siduri,p_bakasha_id,p_taarich_idkun,p_sug_hodaa,p_kod_tahalich,p_kod_yeshut,p_mispar_ishi, p_taarich,p_mispar_sidur,p_shat_hatchala_sidur,
           p_shat_yetzia,p_mispar_knisa,p_kod_hodaa,p_teur_hodaa);

      EXCEPTION
   WHEN OTHERS THEN
        RAISE;
  END pro_ins_log_bakasha;

/*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        27/04/2009      sari       1. הוספת רשומה לטבלת בקשות
*/
  PROCEDURE pro_ins_bakasha(p_sug_bakasha  IN TB_BAKASHOT.sug_bakasha%TYPE,
                   p_teur  IN TB_BAKASHOT.teur%TYPE,
                 p_status   IN TB_BAKASHOT.status%TYPE,
                 p_user_id IN TB_BAKASHOT.mishtamesh_id%TYPE,
              p_bakasha_id OUT TB_BAKASHOT.bakasha_id%TYPE) IS
     v_bakasha_id TB_BAKASHOT.bakasha_id%TYPE;
  BEGIN
        SELECT request_seq.NEXTVAL INTO v_bakasha_id FROM dual;
   p_bakasha_id:=v_bakasha_id;

        INSERT INTO TB_BAKASHOT
                      (BAKASHA_ID,SUG_BAKASHA,TEUR,ZMAN_HATCHALA,ZMAN_SIYUM,STATUS,MISHTAMESH_ID,
         HUAVRA_LESACHAR,TAARICH_HAAVARA_LESACHAR  )
   VALUES (v_bakasha_id,p_sug_bakasha,p_teur,SYSDATE,NULL,p_status, p_user_id,NULL,NULL);

      EXCEPTION
   WHEN OTHERS THEN
        RAISE;
  END pro_ins_bakasha;

  PROCEDURE pro_upd_bakasha(p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                   p_status   IN TB_BAKASHOT.status%TYPE,
              p_huavra_lesachar IN  TB_BAKASHOT.huavra_lesachar%TYPE,
              p_zman_siyum   IN  TB_BAKASHOT.zman_siyum%TYPE,
              p_tar_haavara_lesachar   IN  TB_BAKASHOT.taarich_haavara_lesachar%TYPE) IS
  BEGIN
        UPDATE  TB_BAKASHOT
           SET ZMAN_SIYUM=NVL(p_zman_siyum,ZMAN_SIYUM),
               STATUS=NVL(p_status,STATUS),
         HUAVRA_LESACHAR=NVL(p_huavra_lesachar,HUAVRA_LESACHAR),
         TAARICH_HAAVARA_LESACHAR=NVL(p_tar_haavara_lesachar,TAARICH_HAAVARA_LESACHAR)
   WHERE BAKASHA_ID=p_bakasha_id;

      EXCEPTION
   WHEN OTHERS THEN
        RAISE;
  END pro_upd_bakasha;


  PROCEDURE pro_upd_bakasha_all_fields(p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                                              p_zman_siyum   IN  TB_BAKASHOT.zman_siyum%TYPE,
                                                              p_status   IN TB_BAKASHOT.status%TYPE,
                                                              p_huavra_lesachar IN  TB_BAKASHOT.huavra_lesachar%TYPE,
                                                              p_tar_haavara_lesachar   IN  TB_BAKASHOT.taarich_haavara_lesachar%TYPE,
                                                              p_ishur_hilan   IN TB_BAKASHOT.ISHUR_HILAN%TYPE ) IS
  BEGIN
  
           UPDATE  TB_BAKASHOT
           SET ZMAN_SIYUM=NVL(p_zman_siyum,ZMAN_SIYUM),
                  STATUS=NVL(p_status,STATUS),
                  HUAVRA_LESACHAR=NVL(p_huavra_lesachar,HUAVRA_LESACHAR),
                  TAARICH_HAAVARA_LESACHAR=NVL(p_tar_haavara_lesachar,TAARICH_HAAVARA_LESACHAR),
                  ISHUR_HILAN=NVL(p_ishur_hilan,   ISHUR_HILAN)
   WHERE BAKASHA_ID=p_bakasha_id;

      EXCEPTION
   WHEN OTHERS THEN
        RAISE;
  END pro_upd_bakasha_all_fields;

/*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        27/04/2009      sari       1. הוספת   רשומה לטבלת פרמטר בקשות
*/
 PROCEDURE pro_ins_bakasha_param(p_bakasha_id  IN TB_BAKASHOT_PARAMS.bakasha_id%TYPE,
                   p_param_id  IN  TB_BAKASHOT_PARAMS.param_id%TYPE,
                 p_erech   IN  TB_BAKASHOT_PARAMS.erech%TYPE) IS
  BEGIN
        INSERT INTO TB_BAKASHOT_PARAMS
                      (BAKASHA_ID,PARAM_ID,ERECH)
   VALUES (p_bakasha_id,p_param_id,p_erech);

      EXCEPTION
   WHEN OTHERS THEN
        RAISE;
  END  pro_ins_bakasha_param;


  /*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        02/07/2009     sari      1. פונקציה המחזירה  ריצות חישוב לפי תאריכים
*/
 PROCEDURE pro_get_pirtey_ritzot(p_taarich_me IN TB_BAKASHOT.zman_hatchala%TYPE,
             p_taarich_ad IN TB_BAKASHOT.zman_hatchala%TYPE,
            p_get_all NUMBER,
            p_cur OUT CurType) IS
  v_first_bakasha NUMBER;
BEGIN
    IF (p_get_all =1) THEN
    OPEN p_cur FOR
         SELECT B.Zman_Hatchala , B.Bakasha_ID, B.Teur,
             (SELECT  DECODE(bp.erech,NULL,'',1,'חברים',2,'שכירים','חברים ושכירים') FROM TB_BAKASHOT_PARAMS bp WHERE bp.bakasha_id=b.bakasha_id AND  bp.param_id=1)   auchlusia,
               (SELECT  bp.erech FROM TB_BAKASHOT_PARAMS bp WHERE bp.bakasha_id=b.bakasha_id AND bp.param_id=2) tkufa,
               TO_DATE('01/' ||  (SELECT  bp.erech FROM TB_BAKASHOT_PARAMS bp WHERE bp.bakasha_id=b.bakasha_id AND bp.param_id=2) ,'dd/mm/yyyy') tkufa_date,
          (SELECT  DECODE(bp.erech,1,'כן','לא') FROM TB_BAKASHOT_PARAMS bp WHERE bp.bakasha_id=b.bakasha_id AND bp.param_id=3) ritza_gorfet,
          B.HUAVRA_LESACHAR,B.ISHUR_HILAN,
          Pkg_Batch.fun_get_status_sachar( B.Bakasha_ID) status_haavara_lesachar,
          Pkg_Batch.fun_get_status_bakasha(13,1,B.Bakasha_ID) status_yezirat_rikuzim,
          Pkg_Batch.fun_get_rizot_zehot_lesachar( B.Bakasha_ID) rizot_zehot      
       FROM TB_BAKASHOT B,(SELECT Erech,Bakasha_ID
                   FROM TB_BAKASHOT_PARAMS
                   WHERE  Param_ID = 5) P
       WHERE B.Sug_Bakasha=1
        AND TRUNC(B.Zman_Hatchala) BETWEEN TRUNC(p_taarich_me) AND TRUNC(p_taarich_ad)
        AND b.bakasha_id=p.bakasha_id
        AND p.erech='0'
        AND b.status=2
       ORDER BY B.Zman_Hatchala DESC ;
  ELSE
     OPEN p_cur FOR
             SELECT B.Zman_Hatchala , B.Bakasha_ID, B.Teur,
                (SELECT  DECODE(bp.erech,NULL,'',1,'חברים',2,'שכירים','חברים ושכירים') FROM TB_BAKASHOT_PARAMS bp WHERE bp.bakasha_id=b.bakasha_id AND  bp.param_id=1)   auchlusia,
             (SELECT  bp.erech FROM TB_BAKASHOT_PARAMS bp WHERE bp.bakasha_id=b.bakasha_id AND bp.param_id=2) tkufa,
              TO_DATE('01/' ||  (SELECT  bp.erech FROM TB_BAKASHOT_PARAMS bp WHERE bp.bakasha_id=b.bakasha_id AND bp.param_id=2),'dd/mm/yyyy' ) tkufa_date,
             (SELECT  DECODE(bp.erech,1,'כן','לא') FROM TB_BAKASHOT_PARAMS bp WHERE bp.bakasha_id=b.bakasha_id AND bp.param_id=3) ritza_gorfet,
             B.HUAVRA_LESACHAR,B.ISHUR_HILAN,
             Pkg_Batch.fun_get_status_sachar( B.Bakasha_ID) status_haavara_lesachar,
             Pkg_Batch.fun_get_status_bakasha(13,1,B.Bakasha_ID) status_yezirat_rikuzim,
             Pkg_Batch.fun_get_rizot_zehot_lesachar( B.Bakasha_ID) rizot_zehot
        FROM TB_BAKASHOT B,(SELECT Erech,Bakasha_ID
                   FROM TB_BAKASHOT_PARAMS
                   WHERE  Param_ID = 5) P
        WHERE B.Sug_Bakasha=1
        AND TRUNC(B.Zman_Hatchala) BETWEEN TRUNC(p_taarich_me) AND TRUNC(p_taarich_ad)
        AND (B.Huavra_Lesachar=0 OR B.Huavra_Lesachar IS NULL)
        AND b.bakasha_id=p.bakasha_id
        AND p.erech='0'
        AND b.status=2
        ORDER BY B.Zman_Hatchala DESC ;

  END IF;

EXCEPTION
   WHEN OTHERS THEN
            RAISE;

END  pro_get_pirtey_ritzot;


FUNCTION fun_get_status_sachar(p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE) RETURN NUMBER
IS 
    p_cur  CurType;
    p_status NUMBER;
    
BEGIN
   p_status:=0;   
   BEGIN
   
       SELECT T.STATUS INTO  p_status
       FROM   TB_BAKASHOT t 
       WHERE   T.BAKASHA_ID IN(
                               SELECT    MAX(T.BAKASHA_ID) bakasha_id
                               FROM  TB_BAKASHOT t , TB_BAKASHOT_PARAMS p
                               WHERE T.BAKASHA_ID = P.BAKASHA_ID
                                  AND  T.SUG_BAKASHA =3
                                  AND P.PARAM_ID = 1
                                  AND P.ERECH = TO_CHAR(p_bakasha_id));     
   EXCEPTION
       WHEN NO_DATA_FOUND  THEN
         p_status:=0;
   END;
  
  RETURN p_status;
END fun_get_status_sachar;

FUNCTION fun_get_status_bakasha(p_sug_bakasha NUMBER,p_Param_id NUMBER,p_erech VARCHAR) RETURN NUMBER
IS 
    p_cur  CurType;
    p_status NUMBER;
    
BEGIN
   p_status:=0;   
   BEGIN
   
        SELECT  t.status INTO p_status
        FROM  TB_BAKASHOT t , TB_BAKASHOT_PARAMS p
        WHERE T.BAKASHA_ID = P.BAKASHA_ID
              AND  T.SUG_BAKASHA =p_sug_bakasha
              AND P.PARAM_ID =p_Param_id
              AND P.ERECH = p_erech;     
   EXCEPTION
       WHEN NO_DATA_FOUND  THEN
         p_status:=0;
   END;
  
  RETURN p_status;
END fun_get_status_bakasha;


FUNCTION fun_get_rizot_zehot_lesachar(p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE) RETURN VARCHAR
IS
CURSOR p_cur(p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE) IS
        SELECT h2.BAKASHA_ID, h2.PARAM_ID_1,h2.PARAM_ID_2
        FROM (SELECT  H.BAKASHA_ID,
                            MAX(CASE WHEN  (H.Param_ID=1)   THEN H.ERECH  END)    PARAM_ID_1 ,
                            MAX(CASE WHEN  (H.Param_ID=2)   THEN H.ERECH  END)    PARAM_ID_2
                 FROM(  SELECT B.BAKASHA_ID,B.ERECH,B.PARAM_ID
                           FROM TB_BAKASHOT_PARAMS b,TB_BAKASHOT t
                           WHERE   T.HUAVRA_LESACHAR=1
                                  AND T.SUG_BAKASHA = 1
                                  AND B.BAKASHA_ID=T.BAKASHA_ID
                                  AND b.bakasha_id <>p_bakasha_id
                                  AND B.PARAM_ID IN(1,2) ) H
                 GROUP BY  H.BAKASHA_ID )  h2
         WHERE   h2.Param_ID_1=( SELECT  bp.erech FROM TB_BAKASHOT_PARAMS bp WHERE bp.bakasha_id=p_bakasha_id AND bp.param_id=1)
              AND h2.Param_ID_2=( SELECT  bp.erech FROM TB_BAKASHOT_PARAMS bp WHERE bp.bakasha_id=p_bakasha_id AND bp.param_id=2);     
lst_bakasha_id VARCHAR(1000);        
v_rec  p_cur%ROWTYPE;
BEGIN
        lst_bakasha_id:='';
             FOR v_rec  IN   p_cur(p_bakasha_id)
              LOOP
                      lst_bakasha_id:= lst_bakasha_id || ';' || v_rec.bakasha_id;
              END LOOP;         
              
      RETURN lst_bakasha_id;            
END fun_get_rizot_zehot_lesachar;

PROCEDURE pro_get_ovdim_lechishuv(p_chodesh IN DATE,
             p_maamad IN NUMBER,
            p_ritza_gorefet IN NUMBER,
            p_cur OUT CurType) IS
  v_me_taarich DATE;
  v_ad_taarich DATE;
BEGIN
  v_me_taarich:=p_chodesh;
  v_ad_taarich:=ADD_MONTHS(v_me_taarich,1)-1;

    IF  p_ritza_gorefet<>1 THEN
    OPEN p_cur FOR
          SELECT MISPAR_ISHI FROM
        ( (SELECT o.MISPAR_ISHI
        FROM OVDIM o,
          (SELECT mispar_ishi, chodesh FROM
             ( (SELECT o.mispar_ishi,TO_CHAR(o.taarich,'mm/yyyy') chodesh
                    FROM TB_YAMEY_AVODA_OVDIM o,TB_MISPAR_ISHI_CHISHUV t
               WHERE o.status=1
			    AND t.mispar_ishi=o.mispar_ishi
               AND TO_CHAR(o.taarich,'mm/yyyy')=TO_CHAR(p_chodesh,'mm/yyyy'))
          /*  union all
            (select mispar_ishi,to_char(chodesh,'mm/yyyy') chodesh
              from Ovdim_Im_Shinuy_HR
              where Bakasha_ID  is null)*/)
           GROUP BY mispar_ishi,chodesh) y,
         (SELECT po.maamad,po.mispar_ishi
               FROM PIVOT_PIRTEY_OVDIM PO
                 WHERE  (v_me_taarich BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
              OR   v_ad_taarich  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
              OR   po.ME_TARICH>=v_me_taarich AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=  v_ad_taarich )) p
        WHERE o.mispar_ishi=p.mispar_ishi
       AND (SUBSTR(p.maamad,0,1)=p_maamad  OR p_maamad  IS NULL)
       AND o.mispar_ishi=y.mispar_ishi)
       UNION
        (SELECT p.mispar_ishi
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
          AND TO_CHAR(p.taarich,'mm/yyyy')=TO_CHAR(p_chodesh,'mm/yyyy')));
  ELSE
     OPEN p_cur FOR
         SELECT o.MISPAR_ISHI
       FROM OVDIM o,
       (SELECT mispar_ishi,TO_CHAR(taarich,'mm/yyyy') chodesh
       FROM TB_YAMEY_AVODA_OVDIM
       WHERE (status=1 OR status=2)
      AND TO_CHAR(taarich,'mm/yyyy')=TO_CHAR(p_chodesh,'mm/yyyy')
      GROUP BY mispar_ishi,TO_CHAR(taarich,'mm/yyyy') ) y,
        (SELECT po.maamad,po.mispar_ishi
              FROM PIVOT_PIRTEY_OVDIM PO
                WHERE  (v_me_taarich BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
             OR   v_ad_taarich  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
             OR   po.ME_TARICH>=v_me_taarich AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=  v_ad_taarich )) p
       WHERE o.mispar_ishi=p.mispar_ishi
      AND (SUBSTR(p.maamad,0,1)=p_maamad  OR p_maamad  IS NULL)
      AND o.mispar_ishi=y.mispar_ishi;

  END IF;

EXCEPTION
   WHEN OTHERS THEN
            RAISE;

END  pro_get_ovdim_lechishuv;

PROCEDURE pro_get_ovdim_to_transfer(p_request_id IN  TB_BAKASHOT.bakasha_id%TYPE,
                p_cur_list OUT CurType,
            p_cur OUT CurType,
            p_cur_prem OUT CurType ) IS
         bakasha_id_prem NUMBER;   
         bakasha_id_nihul_prem NUMBER;   
         taarich_prem DATE;
          taarich_cur DATE;     
         p_maamad varchar(5);
 BEGIN
 
  SELECT  TO_DATE('01/' || x.erech,'dd/mm/yyyy') INTO taarich_cur 
  FROM TB_BAKASHOT_PARAMS x
  WHERE x.bakasha_id=p_request_id AND x.param_id=2;
 
 IF ( TO_DATE('01/' || TO_CHAR(ADD_MONTHS(SYSDATE,-1),'mm/yyyy'),'dd/mm/yyyy') = taarich_cur) THEN
      taarich_prem:= ADD_MONTHS(taarich_cur,-1);
       SELECT MAX(b.bakasha_id) INTO bakasha_id_prem
       FROM TB_BAKASHOT b,TB_CHISHUV_CHODESH_OVDIM c
       WHERE  B.BAKASHA_ID = c.BAKASHA_ID
            AND B.SUG_BAKASHA=12
            AND C.TAARICH = taarich_prem;
            
        SELECT MAX(b.bakasha_id) INTO bakasha_id_nihul_prem
        FROM TB_CHISHUV_CHODESH_OVDIM c,TB_BAKASHOT b
        WHERE B.BAKASHA_ID = c.BAKASHA_ID
            AND c.kod_rechiv IN(114,117,116,118)
            AND c.taarich=taarich_prem;
            
        select p.erech into p_maamad
        from tb_bakashot_params p
        where p.bakasha_id=p_request_id
        and p.param_id=1;

 ELSE
        bakasha_id_prem:= NULL;
        bakasha_id_nihul_prem:=NULL;
        taarich_prem:=NULL;
        p_maamad:=null;
 END IF;
 
     OPEN p_cur_list FOR
     WITH cc AS (
                 SELECT     DISTINCT TAARICH, MISPAR_ISHI
                 FROM     TB_CHISHUV_CHODESH_OVDIM C
                 WHERE     BAKASHA_ID = p_request_id ),
             mp AS ( SELECT     ERECH
                   FROM      TB_BAKASHOT_PARAMS
                   WHERE     BAKASHA_ID =p_request_id
                             AND PARAM_ID = 2 )
     SELECT * FROM(
			     SELECT c.taarich,c.mispar_ishi ,DECODE(SUBSTR(p.maamad,0,1),1,'0013', DECODE(SUBSTR(p.maamad,2,2),'23','2626',  '0026')) mifal,
                 SUBSTR(p.maamad,2,2) maamad,p.gil,SUBSTR(p.maamad,0,1) maamad_rashi, c.chodesh_ibud,
						o.SIFRAT_BIKORET_MI sifrat_bikoret,o.SHEM_MISH,o.SHEM_PRAT,p.dirug,p.darga,o.TEUDAT_ZEHUT,p.Isuk,
                      Pkg_Ovdim.fun_get_meafyen_oved(c.mispar_ishi,53,c.taarich) meafyen53,
                      Pkg_Ovdim.fun_get_meafyen_oved(c.mispar_ishi,83,c.taarich) meafyen83,
			                DECODE((SELECT  1 FROM MATZAV_OVDIM m
									            WHERE m.mispar_ishi=c.mispar_ishi
									           AND c.TAARICH BETWEEN m.taarich_hatchala AND m.taarich_siyum
									          AND  m.Kod_matzav='33'),NULL,0,1) mushhe, 1 makor
			     FROM      ( SELECT cc.TAARICH, cc.MISPAR_ISHI, mp.ERECH CHODESH_IBUD
                        FROM cc, mp )
                     /* (SELECT  DISTINCT taarich,mispar_ishi,
				 (SELECT erech FROM TB_BAKASHOT_PARAMS 
							WHERE bakasha_id=p_request_id
							AND param_id=2) chodesh_ibud
					              FROM TB_CHISHUV_CHODESH_OVDIM c
					            WHERE Bakasha_ID=p_request_id) */ c,
			           			OVDIM o,
                                 PIVOT_PIRTEY_OVDIM p
						    /*    (SELECT po.me_tarich,po.ad_tarich ,po.mispar_ishi,po.maamad,po.dirug,po. darga,po.Isuk,po.gil
						       FROM PIVOT_PIRTEY_OVDIM PO)p*/
			   WHERE  c.mispar_ishi=p.mispar_ishi
             -- and c.mispar_ishi =46629
			   AND o.mispar_ishi=c.mispar_ishi
                AND p.ME_TARICH=(SELECT MAX(po.me_tarich)
													          FROM   PIVOT_PIRTEY_OVDIM po
													          WHERE (c.taarich BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
													          OR   (ADD_MONTHS(c.taarich,1)-1)  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
													          OR   po.ME_TARICH>=c.taarich AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=    (ADD_MONTHS(c.taarich,1)-1))
													  AND po.ISUK IS NOT NULL
													   AND po.mispar_ishi=c.mispar_ishi)
		--	   ORDER BY p.mispar_ishi asc,c.taarich desc
          UNION       
               SELECT DISTINCT c.taarich, c.mispar_ishi,
                         DECODE(SUBSTR(p.maamad,0,1),1,'0013', DECODE(SUBSTR(p.maamad,2,2),'23','2626',  '0026')) mifal,
                         SUBSTR(p.maamad,2,2) maamad,p.gil,SUBSTR(p.maamad,0,1) maamad_rashi ,mp.ERECH CHODESH_IBUD,
                          o.SIFRAT_BIKORET_MI sifrat_bikoret,o.SHEM_MISH,o.SHEM_PRAT, p.dirug,p.darga,o.TEUDAT_ZEHUT,p.Isuk,
                          Pkg_Ovdim.fun_get_meafyen_oved(c.mispar_ishi,53,c.taarich) meafyen53,
                          Pkg_Ovdim.fun_get_meafyen_oved(c.mispar_ishi,83,c.taarich) meafyen83 ,
                            DECODE((SELECT  1 FROM MATZAV_OVDIM m
                                                WHERE m.mispar_ishi=c.mispar_ishi
                                               AND c.TAARICH BETWEEN m.taarich_hatchala AND m.taarich_siyum
                                              AND  m.Kod_matzav='33'),NULL,0,1) mushhe, 2 makor
               FROM TB_CHISHUV_CHODESH_OVDIM c,OVDIM o,
                         PIVOT_PIRTEY_OVDIM p,
                         mp
                /* (SELECT po.me_tarich,po.ad_tarich ,po.mispar_ishi,po.maamad,po.dirug,po. darga,po.Isuk,po.gil
                               FROM PIVOT_PIRTEY_OVDIM PO)p*/
               WHERE (  (c.Bakasha_ID=bakasha_id_prem  AND c.KOD_RECHIV IN(115,113,112) )
                           OR   (c.Bakasha_ID=bakasha_id_nihul_prem  AND c.KOD_RECHIV IN(114,117,116,118) ) )
                   AND c.taarich= taarich_prem
                   AND c.mispar_ishi=o.mispar_ishi
                   AND c.mispar_ishi=p.mispar_ishi
                   and trim(p_maamad)<>'1'
                   AND  SUBSTR(p.maamad,0,1) = 2
              --     and c.mispar_ishi =46629
                   AND c.taarich BETWEEN p.me_tarich AND p.ad_tarich
                 AND c.mispar_ishi NOT IN(   select cc.mispar_ishi from cc
                  /* SELECT x.mispar_ishi --,c.taarich,c.BAKASHA_ID,c.kod_rechiv,c.erech_rechiv
                        FROM TB_CHISHUV_CHODESH_OVDIM x
                        WHERE x.Bakasha_ID=p_request_id */ )  )     
     ORDER BY mispar_ishi ASC,taarich DESC;          

   OPEN p_cur FOR
   with cc as ( SELECT DISTINCT   a.mispar_ishi, a.Taarich, a.bakasha_id,a.th2 TAARICH_HAAVARA_LESACHAR
                                        FROM( SELECT  co.mispar_ishi, co.Taarich, co.bakasha_id,b.TAARICH_HAAVARA_LESACHAR th1,
                                                              MAX(B.TAARICH_HAAVARA_LESACHAR) OVER (PARTITION BY co.mispar_ishi,co.Taarich )  th2
                                                 FROM TB_CHISHUV_CHODESH_OVDIM co,TB_BAKASHOT b
                                                 WHERE co.Bakasha_ID<>p_request_id
                                                    AND co.Bakasha_ID<p_request_id
                                                       AND B.BAKASHA_ID = CO.BAKASHA_ID
                                                       AND b.Huavra_Lesachar=1
                                           --      and co.mispar_ishi=28466
                                                  ) a
                                          WHERE a.th1=a.th2)     ,
          mp AS ( SELECT     to_date('01/' || ERECH,'dd/mm/yyyy') chodesh_ibud
                   FROM      TB_BAKASHOT_PARAMS
                   WHERE     BAKASHA_ID =p_request_id
                             AND PARAM_ID = 2 )                               
   SELECT c.mispar_ishi, c.taarich,c.kod_rechiv,c.b1 bakasha_id_1,c.b2 bakasha_id_2,c.erech_rechiv_a,c.erech_rechiv_b,c.erech_rechiv,chodesh_ibud
    FROM
            ( SELECT  NVL(a.taarich,b.taarich) taarich,NVL(a.mispar_ishi,b.mispar_ishi)mispar_ishi,a.BAKASHA_ID b1,NVL(a.erech_rechiv,0) erech_rechiv_a,
                NVL(b.BAKASHA_ID,bakasha_id_last) b2, NVL(b.erech_rechiv,0) erech_rechiv_b,NVL(a.kod_rechiv,b.kod_rechiv) kod_rechiv,
                NVL(a.erech_rechiv,0)-NVL(b.erech_rechiv,0) erech_rechiv,nvl(a.chodesh_ibud,b.chodesh_ibud) chodesh_ibud
               FROM
                       (
                        SELECT  c1.mispar_ishi,c1.taarich,c1.BAKASHA_ID,c1.kod_rechiv,c1.erech_rechiv,c2.bakasha_id bakasha_id_last,mp.chodesh_ibud
                        FROM    (SELECT c.mispar_ishi,c.taarich,c.BAKASHA_ID,c.kod_rechiv,c.erech_rechiv
                                     FROM TB_CHISHUV_CHODESH_OVDIM c
                                     WHERE c.Bakasha_ID=p_request_id
                                  --     and c.mispar_ishi=28466
                                     ORDER BY c.mispar_ishi,c.kod_rechiv) c1,   
                                     mp,                
                                   ( select  cc.mispar_ishi, cc.Taarich, cc.bakasha_id,cc.TAARICH_HAAVARA_LESACHAR from cc
                                   /*SELECT DISTINCT   a.mispar_ishi, a.Taarich, a.bakasha_id,a.th2 TAARICH_HAAVARA_LESACHAR
                                                FROM( SELECT  co.mispar_ishi, co.Taarich, co.bakasha_id,b.TAARICH_HAAVARA_LESACHAR th1,
                                                                      MAX(B.TAARICH_HAAVARA_LESACHAR) OVER (PARTITION BY co.mispar_ishi,co.Taarich )  th2
                                                         FROM TB_CHISHUV_CHODESH_OVDIM co,TB_BAKASHOT b
                                                         WHERE co.Bakasha_ID<>p_request_id
                                                               AND co.Bakasha_ID<p_request_id
                                                               AND B.BAKASHA_ID = CO.BAKASHA_ID
                                                               AND b.Huavra_Lesachar=1
                                                      --          and co.mispar_ishi=28466
                                                        -- and co.mispar_ishi=202
                                                          ) a
                                                  WHERE a.th1=a.th2*/
                                                ) c2
                          WHERE   c1.mispar_ishi= c2.mispar_ishi(+)
                                AND c1.taarich = c2.taarich(+)                    
                         )a
                    FULL JOIN
                        (SELECT c1.mispar_ishi,c1.bakasha_id,c1.Taarich,c1.kod_rechiv,c1.erech_rechiv,mp.chodesh_ibud
                          FROM TB_CHISHUV_CHODESH_OVDIM c1 ,
                                    TB_BAKASHOT b,
                                    (SELECT  DISTINCT taarich,mispar_ishi
                                      FROM TB_CHISHUV_CHODESH_OVDIM c
                                      WHERE Bakasha_ID=p_request_id)c2  ,
                                      mp,
                                     ( select  cc.mispar_ishi, cc.Taarich, cc.bakasha_id,cc.TAARICH_HAAVARA_LESACHAR from cc
                                      /*SELECT DISTINCT   a.mispar_ishi, a.Taarich, a.bakasha_id,a.th2 TAARICH_HAAVARA_LESACHAR
                                        FROM( SELECT  co.mispar_ishi, co.Taarich, co.bakasha_id,b.TAARICH_HAAVARA_LESACHAR th1,
                                                              MAX(B.TAARICH_HAAVARA_LESACHAR) OVER (PARTITION BY co.mispar_ishi,co.Taarich )  th2
                                                 FROM TB_CHISHUV_CHODESH_OVDIM co,TB_BAKASHOT b
                                                 WHERE co.Bakasha_ID<>p_request_id
                                                    AND co.Bakasha_ID<p_request_id
                                                       AND B.BAKASHA_ID = CO.BAKASHA_ID
                                                       AND b.Huavra_Lesachar=1
                                           --      and co.mispar_ishi=28466
                                                  ) a
                                          WHERE a.th1=a.th2*/
                                        ) x
                          WHERE c1.Bakasha_ID<>p_request_id
                               AND c1.taarich=c2.taarich
                               AND c1.mispar_ishi=c2.mispar_ishi
                               AND b.Huavra_Lesachar=1
                               AND b.bakasha_id=c1.bakasha_id
                               AND c1.BAKASHA_ID=x.BAKASHA_ID
                               AND b.Taarich_Haavara_Lesachar = x.Taarich_Haavara_Lesachar
                               AND c1.taarich=x.taarich
                               AND c1.mispar_ishi=x.mispar_ishi
                         ORDER BY c1.mispar_ishi,c1.taarich,c1.kod_rechiv ) b
                   ON       a.mispar_ishi=b.mispar_ishi
                       AND a.taarich=b.taarich
                       AND  a.kod_rechiv  = b.kod_rechiv) c 
      ORDER BY c.mispar_ishi,c.taarich,c.kod_rechiv;    

    OPEN p_cur_prem FOR
        SELECT C.MISPAR_ISHI,C.TAARICH,C.KOD_RECHIV,C.ERECH_RECHIV                         
        FROM TB_CHISHUV_CHODESH_OVDIM c 
        WHERE C.BAKASHA_ID=bakasha_id_prem
          AND C.TAARICH =  taarich_prem
          AND c.KOD_RECHIV IN(115,113,112)
    UNION
        SELECT C.MISPAR_ISHI,C.TAARICH,C.KOD_RECHIV,C.ERECH_RECHIV                         
        FROM TB_CHISHUV_CHODESH_OVDIM c 
         WHERE C.BAKASHA_ID=bakasha_id_nihul_prem
             AND C.TAARICH =  taarich_prem
             AND c.KOD_RECHIV IN(114,117,116,118);
EXCEPTION
   WHEN OTHERS THEN
            RAISE;

END  pro_get_ovdim_to_transfer;

PROCEDURE pro_get_chishuv_yomi(p_request_id IN  TB_BAKASHOT.bakasha_id%TYPE,
                   p_mispar_ishi IN  TB_CHISHUV_YOMI_OVDIM.MISPAR_ISHI%TYPE,
                   p_taarich IN TB_CHISHUV_YOMI_OVDIM.TAARICH%TYPE,
              p_cur OUT CurType) IS
 BEGIN
 OPEN p_cur  FOR
   SELECT   taarich,kod_rechiv,erech_rechiv
   FROM TB_CHISHUV_YOMI_OVDIM c
   WHERE mispar_ishi =p_mispar_ishi
   AND taarich BETWEEN p_taarich AND LAST_DAY(p_taarich)
  AND bakasha_id=p_request_id;

EXCEPTION
   WHEN OTHERS THEN
            RAISE;

END  pro_get_chishuv_yomi;

----------------
PROCEDURE pro_del_chishuv_after_transfer(p_request_id IN  TB_BAKASHOT.bakasha_id%TYPE) IS
 CURSOR v_cur(v_request_id TB_BAKASHOT.bakasha_id%TYPE) IS
  SELECT b.bakasha_id
     FROM
     (SELECT bakasha_id,MAX(CASE WHEN param_id=1 THEN erech ELSE NULL END) param1 ,
     MAX(CASE WHEN param_id=2 THEN erech ELSE NULL END) param2
      FROM TB_BAKASHOT_PARAMS
      WHERE param_id IN(1,2)
      GROUP BY bakasha_id )p,
      TB_BAKASHOT b
    WHERE  b.HUAVRA_LESACHAR IS NULL
    AND b.bakasha_id=p.bakasha_id
     AND b.bakasha_id<>v_request_id
    AND (p.param1=(SELECT p.erech FROM TB_BAKASHOT_PARAMS p,TB_BAKASHOT b
                    WHERE b.bakasha_id=v_request_id
                    AND b.bakasha_id=p.bakasha_id
                    AND param_id=1)
    AND p.param2=(SELECT p.erech FROM TB_BAKASHOT_PARAMS p,TB_BAKASHOT b
                  WHERE b.bakasha_id=v_request_id
                  AND b.bakasha_id=p.bakasha_id
                  AND param_id=2));
 --icount number;
  v_from_date date; 
 v_to_date date;
 v_bakashot varchar2(1000 char);
  BEGIN
  --icount:=0;
  v_bakashot:='';
   
   FOR   v_cur_rec IN  v_cur(p_request_id)
   LOOP
   if (length(v_bakashot)>0) then v_bakashot:=v_bakashot || ','; end if;
   
   v_bakashot:=v_bakashot || v_cur_rec.bakasha_id;
   /*
  DELETE FROM TB_CHISHUV_SIDUR_OVDIM
    WHERE bakasha_id=v_cur_rec.bakasha_id;

    DELETE FROM TB_CHISHUV_YOMI_OVDIM
    WHERE bakasha_id=v_cur_rec.bakasha_id;

    DELETE FROM TB_CHISHUV_CHODESH_OVDIM
    WHERE bakasha_id=v_cur_rec.bakasha_id;
    
    DELETE FROM TB_BAKASHOT_PARAMS
    WHERE bakasha_id=v_cur_rec.bakasha_id;
    
    DELETE FROM TB_BAKASHOT
    WHERE bakasha_id=v_cur_rec.bakasha_id;*/
    END LOOP;
 
   select  last_day(to_date('01/' || erech,'dd/mm/yyyy')) ,
            add_months(to_date('01/' || erech,'dd/mm/yyyy'),-11) into v_from_date,v_to_date
     FROM TB_BAKASHOT_PARAMS
    WHERE bakasha_id=p_request_id
    and param_id=2;
              
    DELETE FROM TB_CHISHUV_SIDUR_OVDIM c
    WHERE c.taarich between v_from_date  and v_to_date
     and C.BAKASHA_ID in( SELECT X FROM TABLE(CAST(Convert_String_To_Table(v_bakashot,  ',') AS MYTABTYPE)));
    
     delete  FROM TB_CHISHUV_YOMI_OVDIM c
    WHERE c.taarich between v_from_date  and v_to_date
       and C.BAKASHA_ID in( SELECT X FROM TABLE(CAST(Convert_String_To_Table(v_bakashot,  ',') AS MYTABTYPE)));
                    
      delete  FROM TB_CHISHUV_CHODESH_OVDIM c
    WHERE c.taarich between v_from_date  and v_to_date
       and C.BAKASHA_ID in( SELECT X FROM TABLE(CAST(Convert_String_To_Table(v_bakashot,  ',') AS MYTABTYPE)));
       
       DELETE FROM TB_BAKASHOT_PARAMS
      where BAKASHA_ID in( SELECT X FROM TABLE(CAST(Convert_String_To_Table(v_bakashot,  ',') AS MYTABTYPE)));
    
    DELETE FROM TB_BAKASHOT
     where BAKASHA_ID in( SELECT X FROM TABLE(CAST(Convert_String_To_Table(v_bakashot,  ',') AS MYTABTYPE)));
                                                       
EXCEPTION
     WHEN NO_DATA_FOUND THEN
        NULL;
   WHEN OTHERS THEN
            RAISE;


END   pro_del_chishuv_after_transfer;

----------------
PROCEDURE pro_upd_status_yamey_avoda(p_request_id IN  TB_BAKASHOT.bakasha_id%TYPE) IS
 CURSOR v_cur(v_request_id TB_BAKASHOT.bakasha_id%TYPE) IS
         WITH bakasha_list AS  (
             SELECT DISTINCT ch.mispar_ishi, ch.taarich , b.ZMAN_HATCHALA
             FROM  kdsadmin.TB_CHISHUV_CHODESH_OVDIM ch ,
                   kdsadmin.TB_BAKASHOT b
             WHERE b.bakasha_id= p_request_id
                 AND b.bakasha_id= ch.bakasha_id)
             SELECT  o.mispar_ishi, o.taarich 
             FROM kdsadmin.TB_YAMEY_AVODA_OVDIM o ,
                 bakasha_list bl
             WHERE o.taarich BETWEEN  bl.taarich AND LAST_DAY (bl.taarich)
                 AND  o.mispar_ishi = bl.mispar_ishi  
                 AND o.Status<>0
                 AND O.TAARICH_IDKUN_ACHARON <=bl.ZMAN_HATCHALA 
                 AND (  EXISTS 
                                    (SELECT 1 FROM kdsadmin.TB_CHISHUV_YOMI_OVDIM C
                                    WHERE c.taarich =o.taarich
                                      AND c.mispar_ishi = o.mispar_ishi
                                     AND bakasha_id= p_request_id)       
             
                         OR          
                           NOT EXISTS (SELECT 1 FROM kdsadmin.TB_SIDURIM_OVDIM S
                                                WHERE s.taarich = o.taarich
                                                     AND s.mispar_ishi = o.mispar_ishi
                                                     AND s.mispar_sidur<>99200)       
                        );
             
             

BEGIN
     FOR   v_cur_rec IN  v_cur(p_request_id)
   LOOP
   UPDATE TB_YAMEY_AVODA_OVDIM
   SET status=2,
          TAARICH_IDKUN_ACHARON = SYSDATE
   WHERE mispar_ishi=v_cur_rec.mispar_ishi
   AND taarich=v_cur_rec.taarich;
  END LOOP;

    Pkg_Calculation.pro_upd_ymy_avoda_lo_bechishuv;
EXCEPTION
     WHEN OTHERS THEN
            RAISE;
END pro_upd_status_yamey_avoda;
--------------


   PROCEDURE pro_ins_yamey_avoda_ovdim IS
    idNumber NUMBER;
err_str VARCHAR(1000);
		
CURSOR  stam6 IS
SELECT teur_tech
 FROM TB_LOG_TAHALICH
WHERE   taarich>SYSDATE-0.01
AND kod_tahalich=7 
AND kod_peilut_tahalich=1;
--and seq between 30 and 44;

 BEGIN
 idNumber:=0;
err_str:='';

   pro_ins_yamey_avoda_ovdim(TO_CHAR(SYSDATE,'yyyymmdd'));
   
 FOR  stam6_rec IN  stam6  LOOP
BEGIN
err_str:=err_str ||trim(stam6_rec.teur_tech);
END;
END LOOP;
COMMIT;

SELECT COUNT(*)
 INTO idNumber
 FROM TB_LOG_TAHALICH
WHERE   taarich>SYSDATE-0.01  
AND kod_tahalich=7
AND kod_peilut_tahalich=1;
--and seq between 30 and 44;

  	IF (idNumber >0) THEN
    RAISE_APPLICATION_ERROR(-20006, SUBSTR(err_str,1,100), TRUE);
 END IF;
 
  EXCEPTION
     WHEN NO_DATA_FOUND  THEN
        						 idNumber:=0;
   WHEN OTHERS THEN
        RAISE;
  
END pro_ins_yamey_avoda_ovdim;  
 
 PROCEDURE pro_ins_yamey_avoda_ovdim(pDt VARCHAR) IS
 CURSOR Yamim IS
  SELECT   DISTINCT mispar_ishi,TO_DATE(pDt,'yyyymmdd') dt--,0,SYSDATE,-11
FROM NEW_MATZAV_OVDIM  o
WHERE   kod_matzav IN ('01','03','04','05','06','07','08')
   AND TO_DATE(pDt,'yyyymmdd') BETWEEN taarich_hatchala AND NVL(taarich_siyum,SYSDATE+1)
   AND NOT EXISTS (SELECT * FROM  TB_YAMEY_AVODA_OVDIM yao
  WHERE yao.taarich=TO_DATE(pDt,'yyyymmdd')
  AND yao.MISPAR_ISHI=o.mispar_ishi);

   BEGIN
FOR  Yamim_rec IN  Yamim LOOP
BEGIN
   INSERT INTO TB_YAMEY_AVODA_OVDIM
   (mispar_ishi,taarich,lina,taarich_idkun_acharon,meadken_acharon)
VALUES (Yamim_rec.mispar_ishi,Yamim_rec.dt,  0,SYSDATE,-11 );
--   SELECT mispar_ishi,TO_DATE(pDt,'yyyymmdd'),0,SYSDATE,-11
--  FROM  NEW_MATZAV_OVDIM  o
--   WHERE   kod_matzav IN ('01','03','04','05','06','07','08')
--   AND TO_DATE(pDt,'yyyymmdd') BETWEEN taarich_hatchala AND NVL(taarich_siyum,SYSDATE+1)
--   AND NOT EXISTS (SELECT * FROM  TB_YAMEY_AVODA_OVDIM yao
--  WHERE yao.taarich=TO_DATE(pDt,'yyyymmdd')
--  AND yao.MISPAR_ISHI=o.mispar_ishi);
EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,1) '||to_char(Yamim_rec.driver_id)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
 	INSERT INTO TB_LOG_TAHALICH
	VALUES (7,1,1,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Yamim_rec.mispar_ishi) ||' '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
END;
END LOOP;
COMMIT;
       
END pro_ins_yamey_avoda_ovdim;


 PROCEDURE pro_get_premiot_ovdim(pYM VARCHAR,p_Cur OUT CurType) IS
  BEGIN
  OPEN p_Cur FOR
-- SELECT baa_premia_empl Mispar_ishi,baa_premia_year_month Tkufa,baa_premia_garagge Snif_premia,baa_sug_premia Sug_premia,baa_premia_minutes Dakot_premia
-- FROM  baam_premia_4_kds@KDS2baam
--WHERE baa_premia_year_month=pYM
--UNION ALL
--SELECT * FROM  wr_premia_4_kds@KDS2wr1
-- WHERE wr_premia_year_month=pYM;
-- 5/2/2012: materialized view tb_prem is the above union only on current open -1 tkufot
SELECT   Mispar_ishi,  Tkufa,  Snif_premia,  Sug_premia,  Dakot_premia
FROM  TB_PREM
WHERE Tkufa=pYM;
END pro_get_premiot_ovdim;


   PROCEDURE pro_upd_yamey_avoda_ovdim(pDt VARCHAR) IS

   BEGIN
   
   BEGIN
          UPDATE TB_YAMEY_AVODA_OVDIM  yao
      SET --measher_o_mistayeg=2,
          tachograf=2
      WHERE  EXISTS (SELECT * FROM TB_SIDURIM_OVDIM so
           WHERE meadken_acharon=-12
        AND so.mispar_ishi=yao.mispar_ishi
        AND  so.taarich=yao.taarich
        AND mispar_sidur<>99200)
     AND yao.taarich= TO_DATE(pDt,'yyyymmdd');
       EXCEPTION
   WHEN OTHERS THEN
   INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,56,SYSDATE,'',10,'',SUBSTR('update_yamim '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
	END;
COMMIT; 

BEGIN
         UPDATE TB_YAMEY_AVODA_OVDIM  yao
      SET --measher_o_mistayeg=2,
          tachograf=2
      WHERE  EXISTS (SELECT * FROM TB_SIDURIM_OVDIM so
           WHERE meadken_acharon=-12
        AND so.mispar_ishi=yao.mispar_ishi
        AND  so.taarich=yao.taarich
        AND mispar_sidur<>99200)
     AND yao.taarich= TO_DATE(pDt,'yyyymmdd')+1;
	        EXCEPTION
   WHEN OTHERS THEN
   INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,57,SYSDATE,'',10,'',SUBSTR('update_yamim '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
	END;
COMMIT; 


-- 20120712 no upd of measher_o_mistayeg
-- 20120712 BEGIN
-- 20120712-- 2010/06/29 measher_o_mistayeg
-- 20120712   UPDATE  TB_YAMEY_AVODA_OVDIM yao
-- 20120712 SET   measher_o_mistayeg =1
-- 20120712WHERE yao.taarich =  TO_DATE(pDt,'yyyymmdd')
-- 20120712AND measher_o_mistayeg IS NULL
-- 20120712AND NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM so
-- 20120712           WHERE so.meadken_acharon=-12
-- 20120712        AND so.mispar_ishi=yao.mispar_ishi
-- 20120712        AND  so.taarich=yao.taarich
-- 20120712      AND so.taarich =  TO_DATE(pDt,'yyyymmdd')
-- 20120712        AND so.mispar_sidur<>99200);
-- 20120712       EXCEPTION
-- 20120712   WHEN OTHERS THEN
-- 20120712   INSERT INTO TB_LOG_TAHALICH
-- 20120712	VALUES (15,1,58,SYSDATE,'',10,'',SUBSTR('update_yamim '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
-- 20120712	END;
-- 20120712COMMIT; 

  --t2010/06/29 update measher_o_mistayeg=null,   for workers with any -12
 --     update  tb_yamey_avoda_ovdim yao
--set   measher_o_mistayeg =null
--where yao.taarich =  to_date(pDt,'yyyymmdd')
--and measher_o_mistayeg =1
--and exists (select * from tb_sidurim_ovdim so
  --         where meadken_acharon=-12
    --    and so.mispar_ishi=yao.mispar_ishi
      --  and  so.taarich=yao.taarich
     -- and so.taarich =  to_date(pDt,'yyyymmdd')
     --   and mispar_sidur<>99200);

 --           update  tb_yamey_avoda_ovdim  ty
--                   set ty.shat_hatchala         =
--                      (select min(so1.shat_hatchala)
--                       from  tb_sidurim_ovdim so1
--                       where
--                       so1.mispar_ishi          = ty.mispar_ishi and
--                       so1.taarich              = ty.taarich and
--                       so1.mispar_sidur         <>99200),
--                       ty.shat_siyum            =
--                      (select  max(so2.shat_gmar)
--                       from  tb_sidurim_ovdim so2
--                       where
--                       so2.mispar_ishi          = ty.mispar_ishi and
--                       so2.taarich              = ty.taarich and
--                       so2.mispar_sidur         <>99200)--,
--            --           ty.taarich_idkun_acharon = sysdate
--                WHERE
--                  ty.taarich       = to_date(pDt,'yyyymmdd');

--            update  tb_yamey_avoda_ovdim  ty
--                   set ty.shat_hatchala         =
--                      (select min(so1.shat_hatchala)
--                       from  tb_sidurim_ovdim so1
--                       where
--                       so1.mispar_ishi          = ty.mispar_ishi and
--                       so1.taarich              = ty.taarich and
--                       so1.mispar_sidur         <>99200),
--                       ty.shat_siyum            =
--                      (select  max(so2.shat_gmar)
--                       from  tb_sidurim_ovdim so2
--                       where
--                       so2.mispar_ishi          = ty.mispar_ishi and
--                       so2.taarich              = ty.taarich and
--                       so2.mispar_sidur         <>99200)--,
--             --          ty.taarich_idkun_acharon = sysdate
--                WHERE
--                  ty.taarich       = to_date(pDt,'yyyymmdd')+1;

--    EXCEPTION
--   WHEN OTHERS THEN
--        RAISE;
      END pro_upd_yamey_avoda_ovdim;


--   PROCEDURE pro_upd_yamey_avoda_1oved(pDt varchar,pIshi varchar) IS
--   BEGIN
 --        update  tb_yamey_avoda_ovdim  ty
 --                  set ty.shat_hatchala         =
--                      (select min(so1.shat_hatchala)
--                       from  tb_sidurim_ovdim so1
--                       where
--                       so1.mispar_ishi          =pIshi  and
--                       so1.taarich              = ty.taarich and
--                       so1.mispar_sidur         <>99200),
--                       ty.shat_siyum            =
--                      (select  max(so2.shat_gmar)
--                       from  tb_sidurim_ovdim so2
--                       where
--                       so2.mispar_ishi          = pIshi and
--                       so2.taarich              = ty.taarich and
--                       so2.mispar_sidur         <>99200),
--                       ty.taarich_idkun_acharon = sysdate
--                WHERE
--                  ty.taarich       = to_date(pDt,'yyyymmdd')
--        and ty.mispar_ishi=pIshi     ;
--          EXCEPTION
--   WHEN OTHERS THEN
--        RAISE;
--      END pro_upd_yamey_avoda_1oved;




 PROCEDURE pro_get_my_attendance(pIshi VARCHAR,pDt VARCHAR,p_Cur OUT CurType) IS
  BEGIN
  OPEN p_Cur FOR
SELECT  taarich,mikum_shaon_knisa,mikum_shaon_yetzia,
DECODE(TO_CHAR(shat_hatchala,'dd/mm/yyyy'),TO_CHAR(taarich,'dd/mm/yyyy'),' ',TO_CHAR(shat_hatchala,'dd/mm/yyyy'))||' '||
TO_CHAR(shat_hatchala,'hh24:mi') shat_hatchala,
DECODE(TO_CHAR(shat_gmar,'dd/mm/yyyy'),TO_CHAR(taarich,'dd/mm/yyyy'),' ',TO_CHAR(shat_gmar,'dd/mm/yyyy'))||' '||
TO_CHAR(shat_gmar,'hh24:mi') shat_gmar,
CAST((shat_gmar - shat_hatchala)*24 AS NUMBER(10,2)) zman
 FROM   TB_SIDURIM_OVDIM
   WHERE  mispar_ishi=pIshi
   AND taarich = TO_DATE(pDt,'yyyymmdd')
   AND mispar_sidur=99001
   AND shat_hatchala>TO_DATE('28/02/1954','dd/mm/yyyy')
   AND shat_gmar IS NOT NULL
   UNION ALL
      SELECT  taarich,mikum_shaon_knisa,mikum_shaon_yetzia,
DECODE(TO_CHAR(shat_hatchala,'dd/mm/yyyy'),TO_CHAR(taarich,'dd/mm/yyyy'),' ',TO_CHAR(shat_hatchala,'dd/mm/yyyy'))||' '||
TO_CHAR(shat_hatchala,'hh24:mi') shat_hatchala,
-- ' ' shat_gmar,0 zman
TO_CHAR(SYSDATE,'hh24:mi') shat_gmar,
CAST((SYSDATE - shat_hatchala)*24 AS NUMBER(10,2)) zman
 FROM   TB_SIDURIM_OVDIM
   WHERE  mispar_ishi=pIshi
   AND taarich = TO_DATE(pDt,'yyyymmdd')
   AND shat_hatchala>TO_DATE('28/02/1954','dd/mm/yyyy')
   AND  shat_gmar  IS NULL
   AND taarich=TRUNC(SYSDATE)
   AND mispar_sidur=99001
   UNION ALL
      SELECT  taarich,mikum_shaon_knisa,mikum_shaon_yetzia,
'01/01/0001'  shat_hatchala,
DECODE(TO_CHAR(shat_gmar,'dd/mm/yyyy'),TO_CHAR(taarich,'dd/mm/yyyy'),' ',TO_CHAR(shat_gmar,'dd/mm/yyyy'))||' '||
TO_CHAR(shat_gmar,'hh24:mi') shat_gmar, 0  zman
 FROM   TB_SIDURIM_OVDIM
   WHERE  mispar_ishi=pIshi
   AND taarich = TO_DATE(pDt,'yyyymmdd')
   AND mispar_sidur=99001
   AND TRUNC(shat_hatchala)=TO_DATE('01/01/0001','dd/mm/yyyy')
   AND shat_gmar IS NOT NULL
   UNION ALL
         SELECT  taarich,mikum_shaon_knisa,mikum_shaon_yetzia,
'01/01/0001'  shat_hatchala,
DECODE(TO_CHAR(shat_gmar,'dd/mm/yyyy'),TO_CHAR(taarich,'dd/mm/yyyy'),' ',TO_CHAR(shat_gmar,'dd/mm/yyyy'))||' '||
TO_CHAR(shat_gmar,'hh24:mi') shat_gmar, 0  zman
 FROM   TB_SIDURIM_OVDIM
 WHERE  mispar_ishi=pIshi
   AND taarich = TO_DATE(pDt,'yyyymmdd')
  AND mispar_sidur=99001
   AND TRUNC(shat_hatchala)=TO_DATE('01/01/0001','dd/mm/yyyy')
   AND shat_gmar IS NULL
   UNION ALL
      SELECT  taarich,mikum_shaon_knisa,mikum_shaon_yetzia,
DECODE(TO_CHAR(shat_hatchala,'dd/mm/yyyy'),TO_CHAR(taarich,'dd/mm/yyyy'),' ',TO_CHAR(shat_hatchala,'dd/mm/yyyy'))||' '||
TO_CHAR(shat_hatchala,'hh24:mi') shat_hatchala,' ' shat_gmar,0 zman
 FROM   TB_SIDURIM_OVDIM
   WHERE  mispar_ishi=pIshi
   AND taarich = TO_DATE(pDt,'yyyymmdd')
   AND shat_hatchala>TO_DATE('28/02/1954','dd/mm/yyyy')
   AND  shat_gmar  IS NULL
   AND taarich<TRUNC(SYSDATE)
   AND mispar_sidur=99001
   UNION ALL
   SELECT SYSDATE ,COUNT(*),COUNT(*)*8.9,NULL,NULL,SUM(CAST((NVL(shat_gmar,SYSDATE) - shat_hatchala)*24 AS NUMBER(10,2)))
    FROM   TB_SIDURIM_OVDIM
   WHERE  mispar_ishi=pIshi
   AND taarich = TO_DATE(pDt,'yyyymmdd')
   AND mispar_sidur=99001
   AND shat_hatchala>TO_DATE('28/02/1954','dd/mm/yyyy')
   --and shat_gmar is not null;
   ;
   END pro_get_my_attendance;

PROCEDURE pro_pivot(p_ishi VARCHAR,p_Dt_from VARCHAR,p_Dt_to VARCHAR,p_Cur OUT CurType) IS
  BEGIN
  OPEN p_Cur FOR
SELECT
  mispar_ishi,
  yechida_irgunit, taarich_hatzava, ezor,snif_av,mikum_yechida,  isuk, rishyon_autobus,mutaam,
    mutaam_bitachon,mercaz_erua,dirug,darga,maamad,gil,  tachanat_sachar,sug_misra,
 keren_revacha, tchilat_avoda
FROM (
  SELECT
    mispar_ishi,
    MAX(CASE WHEN kod_natun=1 THEN erech ELSE NULL END) yechida_irgunit,
    MAX(CASE WHEN  kod_natun=2 THEN erech ELSE NULL END) taarich_hatzava,
    MAX(CASE WHEN  kod_natun=3 THEN erech ELSE NULL END)  ezor,
    MAX(CASE WHEN kod_natun=4 THEN  NVL(erech,'kuku')   ELSE NULL END) snif_av,
    MAX(CASE WHEN  kod_natun=5 THEN erech ELSE NULL END) mikum_yechida,
    MAX(CASE WHEN  kod_natun=6  THEN NVL(erech,'kuku') ELSE NULL END)  isuk,
    MAX(CASE WHEN kod_natun=7 THEN erech ELSE NULL END) rishyon_autobus,
    MAX(CASE WHEN  kod_natun=8 THEN erech ELSE NULL END) mutaam,
    MAX(CASE WHEN  kod_natun=9 THEN erech ELSE NULL END)  mutaam_bitachon,
    MAX(CASE WHEN kod_natun=10 THEN erech ELSE NULL END) mercaz_erua,
    MAX(CASE WHEN  kod_natun=11 THEN erech ELSE NULL END) dirug,
    MAX(CASE WHEN  kod_natun=12 THEN erech ELSE NULL END) darga,
    MAX(CASE WHEN kod_natun=13 THEN erech ELSE NULL END) maamad,
    MAX(CASE WHEN  kod_natun=14 THEN erech ELSE NULL END) gil,
    MAX(CASE WHEN  kod_natun=15 THEN erech ELSE NULL END)  tachanat_sachar,
    MAX(CASE WHEN kod_natun=17 THEN erech ELSE NULL END) sug_misra,
    MAX(CASE WHEN  kod_natun=18 THEN erech ELSE NULL END) keren_revacha,
    MAX(CASE WHEN  kod_natun=19 THEN erech ELSE NULL END) tchilat_avoda
  FROM
    PIRTEY_OVDIM
 WHERE  mispar_ishi=p_ishi
  AND ((TO_DATE(p_Dt_from,'dd/mm/yyyy') <= me_taarich  AND TO_DATE(p_Dt_to,'dd/mm/yyyy') >= me_taarich  AND TO_DATE(p_Dt_to,'dd/mm/yyyy') <= NVL(ad_taarich,TO_DATE('31/12/4712','dd/mm/yyyy')))
  OR  (TO_DATE(p_Dt_from,'dd/mm/yyyy') <= me_taarich  AND TO_DATE(p_Dt_to,'dd/mm/yyyy') >= NVL(ad_taarich,TO_DATE('31/12/4712','dd/mm/yyyy')))
     OR (TO_DATE(p_Dt_from,'dd/mm/yyyy') >= me_taarich  AND TO_DATE(p_Dt_from,'dd/mm/yyyy') <= NVL(ad_taarich,TO_DATE('31/12/4712','dd/mm/yyyy')) AND TO_DATE(p_Dt_to,'dd/mm/yyyy') >= NVL(ad_taarich,TO_DATE('31/12/4712','dd/mm/yyyy')))
  OR (TO_DATE(p_Dt_from,'dd/mm/yyyy') >= me_taarich   AND TO_DATE(p_Dt_to,'dd/mm/yyyy') <= NVL(ad_taarich,TO_DATE('31/12/4712','dd/mm/yyyy'))))
  GROUP BY
   mispar_ishi);
END pro_pivot;

PROCEDURE pro_pivot_1Day(p_ishi VARCHAR,p_Dt VARCHAR,p_Cur OUT CurType) IS
  BEGIN
  OPEN p_Cur FOR
SELECT
  mispar_ishi,
  yechida_irgunit, taarich_hatzava, ezor,snif_av,mikum_yechida,  isuk, rishyon_autobus,mutaam,
    mutaam_bitachon,mercaz_erua,dirug,darga,maamad,gil,  tachanat_sachar,sug_misra,
 keren_revacha, tchilat_avoda
FROM (
  SELECT
    mispar_ishi,
    MAX(CASE WHEN kod_natun=1 THEN erech ELSE NULL END) yechida_irgunit,
    MAX(CASE WHEN  kod_natun=2 THEN erech ELSE NULL END) taarich_hatzava,
    MAX(CASE WHEN  kod_natun=3 THEN erech ELSE NULL END)  ezor,
    MAX(CASE WHEN kod_natun=4 THEN  NVL(erech,'kuku')   ELSE NULL END) snif_av,
    MAX(CASE WHEN  kod_natun=5 THEN erech ELSE NULL END) mikum_yechida,
    MAX(CASE WHEN  kod_natun=6  THEN NVL(erech,'kuku') ELSE NULL END)  isuk,
    MAX(CASE WHEN kod_natun=7 THEN erech ELSE NULL END) rishyon_autobus,
    MAX(CASE WHEN  kod_natun=8 THEN erech ELSE NULL END) mutaam,
    MAX(CASE WHEN  kod_natun=9 THEN erech ELSE NULL END)  mutaam_bitachon,
    MAX(CASE WHEN kod_natun=10 THEN erech ELSE NULL END) mercaz_erua,
    MAX(CASE WHEN  kod_natun=11 THEN erech ELSE NULL END) dirug,
    MAX(CASE WHEN  kod_natun=12 THEN erech ELSE NULL END) darga,
    MAX(CASE WHEN kod_natun=13 THEN erech ELSE NULL END) maamad,
    MAX(CASE WHEN  kod_natun=14 THEN erech ELSE NULL END) gil,
    MAX(CASE WHEN  kod_natun=15 THEN erech ELSE NULL END)  tachanat_sachar,
    MAX(CASE WHEN kod_natun=17 THEN erech ELSE NULL END) sug_misra,
    MAX(CASE WHEN  kod_natun=18 THEN erech ELSE NULL END) keren_revacha,
    MAX(CASE WHEN  kod_natun=19 THEN erech ELSE NULL END) tchilat_avoda
  FROM
    PIRTEY_OVDIM
 WHERE  mispar_ishi=p_ishi
 AND TO_DATE(p_Dt ,'dd/mm/yyyy') BETWEEN me_taarich AND ad_taarich
  GROUP BY
   mispar_ishi);
END pro_pivot_1Day;

PROCEDURE pro_ins_log_tahalich(p_KodTahalich  NUMBER  ,p_KodPeilut NUMBER,  p_KodStatus  NUMBER ,  p_TeurTech VARCHAR  ) IS
  BEGIN

        INSERT INTO  TB_LOG_TAHALICH
   VALUES (p_KodTahalich,p_KodPeilut,0,SYSDATE,p_KodStatus,NULL,NULL,p_TeurTech);

      EXCEPTION
   WHEN OTHERS THEN
        RAISE;
  END pro_ins_log_tahalich;

PROCEDURE pro_ins_log_tahalich_takala(p_KodTahalich  NUMBER  ,p_KodPeilut NUMBER,  p_KodStatus  NUMBER ,  p_TeurTech VARCHAR  ,p_KodTakala NUMBER) IS
  BEGIN

        INSERT INTO  TB_LOG_TAHALICH
   VALUES (p_KodTahalich,p_KodPeilut,0,SYSDATE,p_KodStatus,p_KodTakala,NULL,p_TeurTech);

      EXCEPTION
   WHEN OTHERS THEN
        RAISE;
  END pro_ins_log_tahalich_takala;



  PROCEDURE pro_GetListDs(pDt VARCHAR, pIshi VARCHAR ,psidur VARCHAR,p_cur OUT CurType)  IS
     BEGIN
 OPEN p_cur FOR
   SELECT TO_CHAR(shat_hatchala,'yyyymmddhh24mi') shat_hatchala,
 TO_CHAR(shat_gmar,'yyyymmddhh24mi') shat_gmar,
 TO_CHAR(shat_hatchala_letashlum,'yyyymmddhh24mi') shat_hatchala_letashlum,
 TO_CHAR(shat_gmar_letashlum,'yyyymmddhh24mi') shat_gmar_letashlum
  FROM TB_SIDURIM_OVDIM
  WHERE mispar_ishi=pIshi
  AND taarich= TO_DATE(pDt,'yyyymmdd')
  AND mispar_sidur=psidur
  ORDER BY shat_hatchala;
    EXCEPTION
   WHEN OTHERS THEN
        RAISE;
 END pro_GetListDs;


  PROCEDURE pro_GetRowDt(pDt VARCHAR, p_cur OUT CurType)  IS
     BEGIN
 OPEN p_cur FOR
 SELECT
               TO_CHAR(TO_DATE(pDt ,'yyyymmdd')+1,'yyyymmdd')  thenextday
      FROM   dual;
        EXCEPTION
   WHEN OTHERS THEN
        RAISE;
END pro_GetRowDt;

 PROCEDURE pro_GetRowDtLong(pDt VARCHAR, p_cur OUT CurType)  IS
     BEGIN
 OPEN p_cur FOR
 SELECT
           TO_CHAR(TO_DATE(pDt ,'yyyymmddhh24mi')+(1/1440),'yyyymmddhh24mi')  thenextefes
       FROM   dual;
       EXCEPTION
   WHEN OTHERS THEN
        RAISE;
END pro_GetRowDtLong;

   PROCEDURE pro_GetRowDtVeryLong(pDt VARCHAR, p_cur OUT CurType)  IS
     BEGIN
 OPEN p_cur FOR
 SELECT
           TO_CHAR(TO_DATE(pDt ,'yyyymmddhh24mi')+(1/86400),'yyyymmddhh24miss') thenextdt
       FROM   dual;
       EXCEPTION
   WHEN OTHERS THEN
        RAISE;
END pro_GetRowDtVeryLong;


  PROCEDURE pro_GetRowDtVeryLong2(pDt VARCHAR, phatchala VARCHAR,pIshi VARCHAR ,psidur VARCHAR,p_cur OUT CurType)  IS
     BEGIN
 OPEN p_cur FOR
 SELECT
      TO_CHAR( MAX(shat_hatchala)  +(1/86400),'yyyymmddhh24miss') thenextdt
 FROM TB_SIDURIM_OVDIM
 WHERE mispar_ishi=pIshi
 AND taarich=TO_DATE(pDt,'yyyymmdd')
 AND mispar_sidur=psidur
AND      TO_CHAR(shat_hatchala ,'yyyymmddhh24mi')=     TO_CHAR(TO_DATE(phatchala ,'yyyymmddhh24mi') ,'yyyymmddhh24mi');
      EXCEPTION
   WHEN OTHERS THEN
        RAISE;
END pro_GetRowDtVeryLong2;


  PROCEDURE pro_GetRowDtVeryLongPundakim2(pDt VARCHAR, phatchala VARCHAR,pIshi VARCHAR ,p_cur OUT CurType)  IS
     BEGIN
 OPEN p_cur FOR
 SELECT
      TO_CHAR( MAX(shat_hityazvut)  +(1/86400),'yyyymmddhh24miss') thenextdt
  FROM TB_HITYAZVUT_PUNDAKIM
  WHERE mispar_ishi=pIshi
 AND taarich=TO_DATE(pDt,'yyyymmdd')
  AND      TO_CHAR(shat_hityazvut ,'yyyymmddhh24mi')=     TO_CHAR(TO_DATE(phatchala ,'yyyymmddhh24mi') ,'yyyymmddhh24mi');
     EXCEPTION
   WHEN OTHERS THEN
        RAISE;
END pro_GetRowDtVeryLongPundakim2;


  PROCEDURE pro_RefreshMv(shem_mvew VARCHAR)  IS
  BEGIN
 dbms_mview.REFRESH(shem_mvew,'c');

EXCEPTION
   WHEN OTHERS THEN
        RAISE;
 END pro_RefreshMv;



 PROCEDURE InsIntoTrailKnisa(pDt VARCHAR, pDt_N_KNISA VARCHAR,SRV_D_ISHI NUMBER,calc_D_new_sidur NUMBER,P24 NUMBER)  IS
     BEGIN
    INSERT INTO  TRAIL_SIDURIM_OVDIM
            (MISPAR_ISHI,MISPAR_sidur,TAARICH,Shat_hatchala,Shat_gmar,
            Shat_hatchala_letashlum,  Shat_gmar_letashlum,Pitzul_hafsaka,
            Chariga,Tosefet_Grira,Hashlama,yom_Visa,
            Lo_letashlum,Out_michsa,Mikum_shaon_knisa,
            Mikum_shaon_yetzia,Achuz_Knas_LePremyat_Visa,
            Achuz_Viza_Besikun,Mispar_Musach_O_Machsan,
            Kod_siba_lo_letashlum,Kod_siba_ledivuch_yadani_in,
            Kod_siba_ledivuch_yadani_out,Menahel_Musach_Meadken,Shayah_LeYom_Kodem ,Mispar_shiurey_nehiga,
            Mezake_Halbasha ,Mezake_nesiot,MEADKEN_ACHARON,TAARICH_IDKUN_ACHARON,
             tafkid_visa ,mivtza_visa ,Sug_Hazmanat_Visa,
             Bitul_O_Hosafa, Nidreshet_hitiatzvut ,Shat_hitiatzvut,  Ptor_Mehitiatzvut,
        Hachtama_Beatar_Lo_Takin  ,Hafhatat_Nochechut_Visa ,   Sector_Visa ,
       heara ,mispar_ishi_trail,taarich_idkun_trail,sug_peula)
            SELECT MISPAR_ISHI,MISPAR_sidur,TAARICH,Shat_hatchala,Shat_gmar,
            Shat_hatchala_letashlum,  Shat_gmar_letashlum,Pitzul_hafsaka,
            Chariga,Tosefet_Grira, Hashlama,yom_Visa,
            Lo_letashlum,Out_michsa,Mikum_shaon_knisa,
            Mikum_shaon_yetzia,Achuz_Knas_LePremyat_Visa,
            Achuz_Viza_Besikun,Mispar_Musach_O_Machsan,
            Kod_siba_lo_letashlum,Kod_siba_ledivuch_yadani_in,
            Kod_siba_ledivuch_yadani_out,Menahel_Musach_Meadken,Shayah_LeYom_Kodem ,Mispar_shiurey_nehiga,            Mezake_Halbasha ,Mezake_nesiot,MEADKEN_ACHARON,TAARICH_IDKUN_ACHARON,
             tafkid_visa ,mivtza_visa ,Sug_Hazmanat_Visa,
             Bitul_O_Hosafa, Nidreshet_hitiatzvut ,Shat_hitiatzvut,  Ptor_Mehitiatzvut,
        Hachtama_Beatar_Lo_Takin  ,Hafhatat_Nochechut_Visa ,   Sector_Visa ,
             heara,-11,SYSDATE ,3
            FROM TB_SIDURIM_OVDIM
             WHERE mispar_ishi=SRV_D_ISHI
             AND taarich=TO_DATE(pDt,'yyyymmdd') + P24
             AND mispar_sidur=  calc_D_new_sidur
             AND shat_hatchala = TO_DATE(pDt_N_KNISA,'yyyymmddhh24mi') +  P24  ;
      EXCEPTION
   WHEN OTHERS THEN
        RAISE;
             END  InsIntoTrailKnisa;

    PROCEDURE InsIntoTrailYetzia(pDt VARCHAR, pDt_N_YETZIA VARCHAR,SRV_D_ISHI NUMBER,calc_D_new_sidur NUMBER,P24 NUMBER)  IS
     BEGIN
    INSERT INTO  TRAIL_SIDURIM_OVDIM
            (MISPAR_ISHI,MISPAR_sidur,TAARICH,Shat_hatchala,Shat_gmar,
            Shat_hatchala_letashlum,  Shat_gmar_letashlum,Pitzul_hafsaka,
            Chariga,Tosefet_Grira,Hashlama,yom_Visa,
            Lo_letashlum,Out_michsa,Mikum_shaon_knisa,
            Mikum_shaon_yetzia,Achuz_Knas_LePremyat_Visa,
            Achuz_Viza_Besikun,Mispar_Musach_O_Machsan,
            Kod_siba_lo_letashlum,Kod_siba_ledivuch_yadani_in,
            Kod_siba_ledivuch_yadani_out,Shayah_LeYom_Kodem ,Mispar_shiurey_nehiga,
            Mezake_Halbasha ,Mezake_nesiot,MEADKEN_ACHARON,TAARICH_IDKUN_ACHARON,
             tafkid_visa ,mivtza_visa ,Sug_Hazmanat_Visa,
             Bitul_O_Hosafa, Nidreshet_hitiatzvut ,Shat_hitiatzvut,  Ptor_Mehitiatzvut,
        Hachtama_Beatar_Lo_Takin  ,Hafhatat_Nochechut_Visa ,   Sector_Visa ,
            heara ,mispar_ishi_trail,taarich_idkun_trail,sug_peula)
            SELECT MISPAR_ISHI,MISPAR_sidur,TAARICH,Shat_hatchala,Shat_gmar,
            Shat_hatchala_letashlum,  Shat_gmar_letashlum,Pitzul_hafsaka,
            Chariga,Tosefet_Grira,Hashlama,yom_Visa,
            Lo_letashlum,Out_michsa,Mikum_shaon_knisa,
            Mikum_shaon_yetzia,Achuz_Knas_LePremyat_Visa,
            Achuz_Viza_Besikun,Mispar_Musach_O_Machsan,
            Kod_siba_lo_letashlum,Kod_siba_ledivuch_yadani_in,
            Kod_siba_ledivuch_yadani_out,Shayah_LeYom_Kodem ,Mispar_shiurey_nehiga,
            Mezake_Halbasha ,Mezake_nesiot,MEADKEN_ACHARON,TAARICH_IDKUN_ACHARON,
             tafkid_visa ,mivtza_visa ,Sug_Hazmanat_Visa,
             Bitul_O_Hosafa, Nidreshet_hitiatzvut ,Shat_hitiatzvut,  Ptor_Mehitiatzvut,
        Hachtama_Beatar_Lo_Takin  ,Hafhatat_Nochechut_Visa ,   Sector_Visa ,
             heara,-11,SYSDATE ,3
            FROM TB_SIDURIM_OVDIM
             WHERE mispar_ishi=SRV_D_ISHI
             AND taarich=TO_DATE(pDt,'yyyymmdd')
             AND mispar_sidur=  calc_D_new_sidur
    AND TO_CHAR(shat_hatchala,'yyyymmdd')='00010101'
            AND shat_gmar=TO_DATE(pDt_N_YETZIA,'yyyymmddhh24mi') +P24 ;
       EXCEPTION
   WHEN OTHERS THEN
        RAISE;
            END  InsIntoTrailYetzia;


 PROCEDURE pro_get_yamei_avoda_meshek(p_date DATE,p_bakasha_id NUMBER, p_Cur OUT CurType) IS
  BEGIN
      OPEN p_cur  FOR
      /* alef */
        SELECT DISTINCT  ya.mispar_ishi,ya.taarich
        FROM TB_YAMEY_AVODA_OVDIM ya,OVDIM o
        WHERE  o.mispar_ishi=ya.mispar_ishi
        AND  EXISTS
        (SELECT mispar_ishi
          FROM TB_SIDURIM_OVDIM so
          WHERE so.mispar_ishi=ya.mispar_ishi
          AND so.taarich=ya.taarich AND so.meadken_acharon=-11
          AND so.taarich_idkun_acharon>NVL(ya.ritzat_shgiot_acharona,so.taarich_idkun_acharon-1)
          )
       AND ya.measher_o_mistayeg IS NOT NULL
       AND NVL(ya.status,-1)<>0
          /* bet */
            UNION SELECT DISTINCT   ya.mispar_ishi,ya.taarich
            FROM TB_YAMEY_AVODA_OVDIM ya,OVDIM o
            WHERE ya.taarich=TRUNC(p_date)
               AND  o.mispar_ishi=ya.mispar_ishi
               AND NOT EXISTS(SELECT 1 FROM TB_SIDURIM_OVDIM so WHERE so.mispar_ishi=ya.mispar_ishi
                                        AND so.taarich=ya.taarich AND so.meadken_acharon=-12)
              AND EXISTS(SELECT 1 FROM TB_SIDURIM_OVDIM so WHERE so.mispar_ishi=ya.mispar_ishi
                                        AND so.taarich=ya.taarich AND so.meadken_acharon<>-12)
            /* gimel */
			/* 20101006:
        union select h.Mispar_Ishi, h.Taarich 
        from Ovdim_Im_Shinuy_HR h, TB_YAMEY_AVODA_OVDIM y,
		  CTB_Isuk i,   pivot_pirtey_ovdim v_pirty_oved,ovdim ov
        where h.Mispar_Ishi=y.Mispar_Ishi
          and h.Taarich=Y.Taarich
          and nvl(y. RITZAT_SHGIOT_ACHARONA,h.Taarich_Idkun_HR-1)<h.Taarich_Idkun_HR
          and (y.STATUS<>0 or y.STATUS is null)
          and y. Measher_O_Mistayeg Is not Null
		    and h.mispar_ishi            = ov.mispar_ishi
		    and v_pirty_oved.mispar_ishi(+) = h.mispar_ishi
        and h.taarich  between v_pirty_oved.me_tarich(+) and v_pirty_oved.ad_tarich(+)
         and i.kod_hevra = ov.kod_hevra
		 and  i.kod_isuk =v_pirty_oved.isuk
		 20101006*/
        /*dalet*/
        UNION SELECT  DISTINCT mispar_ishi, taarich
      FROM
       (SELECT DISTINCT t.mispar_ishi, t.kod_ishur,t.taarich, t.mispar_sidur, t.shat_hatchala,
             t.shat_yetzia, t.mispar_knisa  ,t.rama,t.kod_status_ishur,t.taarich_idkun_acharon,
              MAX(t.rama) KEEP (DENSE_RANK LAST ORDER BY t.rama )
                OVER (PARTITION BY t.mispar_ishi,t.taarich,t.kod_ishur ) max_f4
              FROM TB_ISHURIM t, TB_YAMEY_AVODA_OVDIM ya,OVDIM o
              WHERE  t.taarich=ya.taarich
              AND t.mispar_ishi=ya.mispar_ishi
              AND  o.mispar_ishi=ya.mispar_ishi
              AND t.taarich_idkun_acharon>NVL(ya.ritzat_shgiot_acharona,t.taarich_idkun_acharon-1)
            )
           WHERE max_f4=rama
           AND kod_status_ishur IN (1,2)
       
       /*heih*/    
        UNION SELECT DISTINCT  ya.mispar_ishi,ya.taarich
        FROM TB_YAMEY_AVODA_OVDIM ya
        WHERE YA.RITZAT_SHGIOT_ACHARONA=TO_DATE('01/01/0001','dd/mm/yyyy');
 END pro_get_yamei_avoda_meshek;

   PROCEDURE pro_get_all_yamei_avoda(  p_Cur OUT CurType) IS
    BEGIN
      OPEN p_cur FOR
         SELECT ya.mispar_ishi,ya.taarich
        FROM TB_YAMEY_AVODA_OVDIM ya,OVDIM o
        WHERE ya.taarich> TO_DATE('28/02/2010','dd/mm/yyyy') AND ya.taarich<TO_DATE('02/03/2010','dd/mm/yyyy')
  AND o.mispar_ishi=ya.mispar_ishi
  ORDER BY o.mispar_ishi;
     --   and status is null;
        --where ya.taarich<=trunc(sysdate).;
  END pro_get_all_yamei_avoda;

  PROCEDURE pro_get_yamei_avoda_shinui_hr(p_date DATE, p_bakasha_id NUMBER, p_Cur OUT CurType) IS
  BEGIN 
    OPEN p_cur FOR 
        /* gimel */
        SELECT h.Mispar_Ishi, h.Taarich 
        FROM OVDIM_IM_SHINUY_HR h, TB_YAMEY_AVODA_OVDIM y,
          CTB_ISUK i,   PIVOT_PIRTEY_OVDIM v_pirty_oved,OVDIM ov
        WHERE h.Mispar_Ishi=y.Mispar_Ishi
          AND h.Taarich=Y.Taarich
          AND NVL(y. RITZAT_SHGIOT_ACHARONA,h.Taarich_Idkun_HR-1)<h.Taarich_Idkun_HR
          AND (y.STATUS<>0 OR y.STATUS IS NULL)
          AND y. Measher_O_Mistayeg IS NOT NULL
            AND h.mispar_ishi            = ov.mispar_ishi
            AND v_pirty_oved.mispar_ishi(+) = h.mispar_ishi
        AND h.taarich  BETWEEN v_pirty_oved.me_tarich(+) AND v_pirty_oved.ad_tarich(+)
         AND i.kod_hevra = ov.kod_hevra
         AND  i.kod_isuk =v_pirty_oved.isuk;
  END pro_get_yamei_avoda_shinui_hr;

       PROCEDURE pro_new_rec(  SRV_D_ISHI VARCHAR,  SRV_D_TAARICH VARCHAR,  calc_D_new_sidur VARCHAR,
      SRV_D_KNISA_X VARCHAR,  SRV_D_MIKUM_KNISA VARCHAR,  SRV_D_SIBAT_DIVUACH_KNISA VARCHAR,
      SRV_D_YETZIA_X VARCHAR,  SRV_D_MIKUM_YETZIA VARCHAR,  SRV_D_SIBAT_DIVUACH_YETZIA VARCHAR,
      SRV_D_ISHI_MEADKEN VARCHAR,
   SRV_D_KOD_BITUL_ZMAN_NESIA_X VARCHAR,
      SRV_D_KOD_CHARIGA_X VARCHAR,  SRV_D_KOD_HALBASHA_X VARCHAR,  SRV_D_KOD_HAZMANA_X VARCHAR,
      TAARICH_knisa_p24 NUMBER , TAARICH_yetzia_p24 NUMBER,  DatEfes VARCHAR,
      TAARICH_knisa_letashlum_p24 NUMBER,  SRV_D_KNISA_letashlum_X VARCHAR,
      TAARICH_yetzia_letashlum_p24 NUMBER,  SRV_D_YETZIA_letashlum_X VARCHAR) IS
    BEGIN
 IF LENGTH(SRV_D_KNISA_X) = 4 THEN
          INSERT INTO TB_SIDURIM_OVDIM ( mispar_ishi  ,   taarich ,  mispar_sidur   , shat_hatchala   ,mikum_shaon_knisa  ,
             Kod_siba_ledivuch_yadani_in,  shat_gmar ,  mikum_shaon_yetzia   ,Kod_siba_ledivuch_yadani_out,
             shat_hatchala_letashlum , shat_gmar_letashlum , chariga    ,hashlama  , lo_letashlum  ,mezake_nesiot,
            mezake_halbasha, Shayah_LeYom_Kodem, Menahel_Musach_Meadken,meadken_acharon)
             VALUES  ( SRV_D_ISHI ,
            TO_DATE(SRV_D_TAARICH,'yyyymmdd') +   TAARICH_knisa_p24,
            calc_D_new_sidur ,
             TO_DATE(SRV_D_KNISA_X ,'yyyymmddhh24mi') +  TAARICH_knisa_p24 ,
            DECODE(Trim(SRV_D_MIKUM_KNISA) , '' ,NULL, '00000',NULL,trim(SRV_D_MIKUM_KNISA) ),
            SRV_D_SIBAT_DIVUACH_KNISA ,
   TO_DATE(SRV_D_YETZIA_X ,'yyyymmddhh24mi') +  TAARICH_yetzia_p24,
            DECODE (Trim(SRV_D_MIKUM_YETZIA) , '',NULL, '00000',NULL, trim(SRV_D_MIKUM_YETZIA)),
            DECODE (Trim(SRV_D_SIBAT_DIVUACH_YETZIA) , '',NULL, '00',NULL, '0',NULL,trim(SRV_D_SIBAT_DIVUACH_YETZIA)),
            TO_DATE(SRV_D_KNISA_letashlum_X ,'yyyymmddhh24mi') +  TAARICH_knisa_letashlum_p24,
            TO_DATE(SRV_D_YETZIA_letashlum_X ,'yyyymmddhh24mi') +  TAARICH_yetzia_letashlum_p24,
            DECODE( SRV_D_KOD_CHARIGA_X , '' ,  0,SRV_D_KOD_CHARIGA_X ),
            DECODE( SRV_D_KOD_HAZMANA_X  , '0' , NULL,'' ,NULL, '7',NULL, SRV_D_KOD_HAZMANA_X ),
            DECODE( SRV_D_KOD_HAZMANA_X , '7' ,1,NULL),
            DECODE ( SRV_D_KOD_BITUL_ZMAN_NESIA_X , '',NULL, '0',NULL, SRV_D_KOD_BITUL_ZMAN_NESIA_X),
    DECODE ( SRV_D_KOD_HALBASHA_X , '', NULL, '0',NULL,  SRV_D_KOD_HALBASHA_X),
            DECODE( TAARICH_knisa_p24 , 1 ,1,NULL),
            DECODE( SRV_D_ISHI_MEADKEN  , '0' ,'', '00000' ,'', 0,'',SRV_D_ISHI_MEADKEN ),
      -11);
   ELSE
          INSERT INTO TB_SIDURIM_OVDIM ( mispar_ishi  ,   taarich ,  mispar_sidur   , shat_hatchala   ,mikum_shaon_knisa  ,
             Kod_siba_ledivuch_yadani_in,  shat_gmar ,  mikum_shaon_yetzia   ,Kod_siba_ledivuch_yadani_out,
             shat_hatchala_letashlum , shat_gmar_letashlum , chariga    ,hashlama  , lo_letashlum  ,mezake_nesiot,
            mezake_halbasha, Shayah_LeYom_Kodem, Menahel_Musach_Meadken,meadken_acharon)
             VALUES  ( SRV_D_ISHI ,
            TO_DATE(SRV_D_TAARICH,'yyyymmdd') +   TAARICH_knisa_p24,
            calc_D_new_sidur ,
             TO_DATE(SRV_D_KNISA_X ,'yyyymmddhh24miss') +  TAARICH_knisa_p24 ,
            DECODE(Trim(SRV_D_MIKUM_KNISA) , '' ,NULL, '00000',NULL,trim(SRV_D_MIKUM_KNISA) ),
            SRV_D_SIBAT_DIVUACH_KNISA ,
   TO_DATE(SRV_D_YETZIA_X ,'yyyymmddhh24mi') +  TAARICH_yetzia_p24,
            DECODE (Trim(SRV_D_MIKUM_YETZIA) , '',NULL, '00000',NULL, trim(SRV_D_MIKUM_YETZIA)),
            DECODE (Trim(SRV_D_SIBAT_DIVUACH_YETZIA) , '',NULL, '00',NULL, '0',NULL,trim(SRV_D_SIBAT_DIVUACH_YETZIA)),
            TO_DATE(SRV_D_KNISA_letashlum_X ,'yyyymmddhh24mi') +  TAARICH_knisa_letashlum_p24,
            TO_DATE(SRV_D_YETZIA_letashlum_X ,'yyyymmddhh24mi') +  TAARICH_yetzia_letashlum_p24,
            DECODE( SRV_D_KOD_CHARIGA_X , '' ,  0,SRV_D_KOD_CHARIGA_X ),
            DECODE( SRV_D_KOD_HAZMANA_X  , '0' , NULL,'' ,NULL, '7',NULL, SRV_D_KOD_HAZMANA_X ),
            DECODE( SRV_D_KOD_HAZMANA_X , '7' ,1,NULL),
            DECODE ( SRV_D_KOD_BITUL_ZMAN_NESIA_X , '',NULL, '0',NULL, SRV_D_KOD_BITUL_ZMAN_NESIA_X),
    DECODE ( SRV_D_KOD_HALBASHA_X , '', NULL, '0',NULL,  SRV_D_KOD_HALBASHA_X),
            DECODE( TAARICH_knisa_p24 , 1 ,1,NULL),
            DECODE( SRV_D_ISHI_MEADKEN  , '0' ,'', '00000' ,'', 0,'',SRV_D_ISHI_MEADKEN ),
      -11);
   END IF;
    EXCEPTION
   WHEN OTHERS THEN
        RAISE;
  END pro_new_rec;

       PROCEDURE pro_measher_o_mistayeg(  SRV_D_ISHI VARCHAR,  SRV_D_TAARICH VARCHAR,  TAARICH_knisa_p24 NUMBER ) IS
    BEGIN
              UPDATE TB_YAMEY_AVODA_OVDIM
                 SET measher_o_mistayeg = 1
                          WHERE mispar_ishi= SRV_D_ISHI
                       AND taarich =   TO_DATE(SRV_D_TAARICH,'yyyymmdd') +   TAARICH_knisa_p24;
    EXCEPTION
   WHEN OTHERS THEN
        RAISE;
     END pro_measher_o_mistayeg;

            PROCEDURE pro_lo_letashlum(  SRV_D_ISHI VARCHAR,  SRV_D_TAARICH VARCHAR,  TAARICH_knisa_p24 NUMBER ) IS
    BEGIN
              UPDATE TB_SIDURIM_OVDIM
                 SET  lo_letashlum = 1 ,KOD_SIBA_LO_LETASHLUM=3
                          WHERE mispar_ishi= SRV_D_ISHI
                       AND taarich =   TO_DATE(SRV_D_TAARICH,'yyyymmdd') +   TAARICH_knisa_p24
        AND mispar_sidur=99200;
    EXCEPTION
   WHEN OTHERS THEN
        RAISE;
     END pro_lo_letashlum;

     PROCEDURE pro_upd_out_blank(  SRV_D_ISHI VARCHAR,  SRV_D_TAARICH VARCHAR,  calc_D_new_sidur VARCHAR,
      SRV_D_KNISA_X VARCHAR,  SRV_D_MIKUM_KNISA VARCHAR,  SRV_D_SIBAT_DIVUACH_KNISA VARCHAR,
      SRV_D_YETZIA_X VARCHAR,  SRV_D_MIKUM_YETZIA VARCHAR,  SRV_D_SIBAT_DIVUACH_YETZIA VARCHAR,
      SRV_D_ISHI_MEADKEN VARCHAR,
   SRV_D_KOD_BITUL_ZMAN_NESIA_X VARCHAR,
      SRV_D_KOD_CHARIGA_X VARCHAR,  SRV_D_KOD_HALBASHA_X VARCHAR,  SRV_D_KOD_HAZMANA_X VARCHAR,
      TAARICH_knisa_p24 NUMBER , TAARICH_yetzia_p24 NUMBER,  DatEfes VARCHAR,
      TAARICH_knisa_letashlum_p24 NUMBER,  SRV_D_KNISA_letashlum_X VARCHAR,
      TAARICH_yetzia_letashlum_p24 NUMBER,  SRV_D_YETZIA_letashlum_X VARCHAR) IS
    BEGIN
             UPDATE TB_SIDURIM_OVDIM
             SET shat_gmar=  TO_DATE(SRV_D_YETZIA_X ,'yyyymmddhh24mi') +  TAARICH_yetzia_p24,
     mikum_shaon_yetzia=DECODE(Trim(SRV_D_MIKUM_YETZIA) , '', mikum_shaon_yetzia,'00000',mikum_shaon_yetzia,trim(SRV_D_MIKUM_YETZIA)),
      Kod_siba_ledivuch_yadani_out =trim(SRV_D_SIBAT_DIVUACH_YETZIA),
      Menahel_Musach_Meadken=DECODE(Trim(SRV_D_ISHI_MEADKEN) ,'',Menahel_Musach_Meadken,'0',Menahel_Musach_Meadken,'00000',Menahel_Musach_Meadken,trim(SRV_D_ISHI_MEADKEN)),
     shat_hatchala_letashlum=TO_DATE(SRV_D_KNISA_letashlum_X ,'yyyymmddhh24mi') +  TAARICH_knisa_letashlum_p24,
              shat_gmar_letashlum=TO_DATE(SRV_D_YETZIA_letashlum_X ,'yyyymmddhh24mi') +  TAARICH_yetzia_letashlum_p24,
      chariga=DECODE(Trim(SRV_D_KOD_CHARIGA_X) ,'',chariga,'0',chariga,Trim(SRV_D_KOD_CHARIGA_X)),
              hashlama=DECODE(trim(SRV_D_KOD_HAZMANA_X ),'',hashlama,'0',hashlama,'7',hashlama,trim(SRV_D_KOD_HAZMANA_X)),
     lo_letashlum=DECODE(trim(SRV_D_KOD_HAZMANA_X),'7',1,lo_letashlum),
     mezake_nesiot=DECODE(Trim(SRV_D_KOD_BITUL_ZMAN_NESIA_X),'',mezake_nesiot,'0',mezake_nesiot,Trim(SRV_D_KOD_BITUL_ZMAN_NESIA_X)),
             mezake_halbasha=DECODE(Trim(SRV_D_KOD_HALBASHA_X) ,'',mezake_halbasha,'0',mezake_halbasha,Trim(SRV_D_KOD_HALBASHA_X)),
             taarich_idkun_acharon = SYSDATE
             WHERE mispar_ishi= SRV_D_ISHI
             AND taarich =   TO_DATE(SRV_D_TAARICH,'yyyymmdd') +   TAARICH_knisa_p24
             AND mispar_sidur= calc_D_new_sidur
             AND shat_hatchala =    TO_DATE(SRV_D_KNISA_X ,'yyyymmddhh24mi') +  TAARICH_knisa_p24;
  EXCEPTION
   WHEN OTHERS THEN
        RAISE;
     END pro_upd_out_blank;

         PROCEDURE pro_upd_in_blank(  SRV_D_ISHI VARCHAR,  SRV_D_TAARICH VARCHAR,  calc_D_new_sidur VARCHAR,
      SRV_D_KNISA_X VARCHAR,  SRV_D_MIKUM_KNISA VARCHAR,  SRV_D_SIBAT_DIVUACH_KNISA VARCHAR,
      SRV_D_YETZIA_X VARCHAR,  SRV_D_MIKUM_YETZIA VARCHAR,  SRV_D_SIBAT_DIVUACH_YETZIA VARCHAR,
      SRV_D_ISHI_MEADKEN VARCHAR,
   SRV_D_KOD_BITUL_ZMAN_NESIA_X VARCHAR,
      SRV_D_KOD_CHARIGA_X VARCHAR,  SRV_D_KOD_HALBASHA_X VARCHAR,  SRV_D_KOD_HAZMANA_X VARCHAR,
      TAARICH_knisa_p24 NUMBER , TAARICH_yetzia_p24 NUMBER,  DatEfes VARCHAR,
      TAARICH_knisa_letashlum_p24 NUMBER,  SRV_D_KNISA_letashlum_X VARCHAR,
      TAARICH_yetzia_letashlum_p24 NUMBER,  SRV_D_YETZIA_letashlum_X VARCHAR) IS
    BEGIN
             UPDATE TB_SIDURIM_OVDIM s1
               SET shat_hatchala=TO_DATE(SRV_D_KNISA_X ,'yyyymmddhh24mi') +  TAARICH_knisa_p24 ,
          mikum_shaon_knisa=DECODE(Trim(SRV_D_MIKUM_KNISA) , '', mikum_shaon_knisa,'00000',mikum_shaon_knisa,trim(SRV_D_MIKUM_KNISA)),
         Kod_siba_ledivuch_yadani_in=trim(SRV_D_SIBAT_DIVUACH_KNISA),
         --meadken_acharon=decode(Trim(SRV_D_ISHI_MEADKEN) ,'',meadken_acharon,'0',meadken_acharon,'00000',meadken_acharon,trim(SRV_D_ISHI_MEADKEN)),
      Menahel_Musach_Meadken=DECODE(Trim(SRV_D_ISHI_MEADKEN) ,'',Menahel_Musach_Meadken,'0',Menahel_Musach_Meadken,'00000',Menahel_Musach_Meadken,trim(SRV_D_ISHI_MEADKEN)),
          shat_hatchala_letashlum=TO_DATE(SRV_D_KNISA_letashlum_X ,'yyyymmddhh24mi') +  TAARICH_knisa_letashlum_p24,
                 shat_gmar_letashlum=TO_DATE(SRV_D_YETZIA_letashlum_X ,'yyyymmddhh24mi') +  TAARICH_yetzia_letashlum_p24,
         chariga=DECODE(Trim(SRV_D_KOD_CHARIGA_X) ,'',chariga,'0',chariga,Trim(SRV_D_KOD_CHARIGA_X)),
                 hashlama=DECODE(trim(SRV_D_KOD_HAZMANA_X ),'',hashlama,'0',hashlama,'7',hashlama,trim(SRV_D_KOD_HAZMANA_X)),
        lo_letashlum=DECODE(trim(SRV_D_KOD_HAZMANA_X),'7',1,lo_letashlum),
        mezake_nesiot=DECODE(Trim(SRV_D_KOD_BITUL_ZMAN_NESIA_X),'',mezake_nesiot,'0',mezake_nesiot,Trim(SRV_D_KOD_BITUL_ZMAN_NESIA_X)),
                mezake_halbasha=DECODE(Trim(SRV_D_KOD_HALBASHA_X) ,'',mezake_halbasha,'0',mezake_halbasha,Trim(SRV_D_KOD_HALBASHA_X)),
                taarich_idkun_acharon = SYSDATE
                WHERE mispar_ishi= SRV_D_ISHI
                 AND taarich =   TO_DATE(SRV_D_TAARICH,'yyyymmdd') +   TAARICH_knisa_p24
                AND mispar_sidur= calc_D_new_sidur
        AND TO_CHAR(shat_hatchala,'yyyymmdd')='00010101'
                AND shat_gmar=TO_DATE(SRV_D_YETZIA_X ,'yyyymmddhh24mi') + TAARICH_yetzia_p24
         AND NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s2
      WHERE  s2.mispar_ishi= SRV_D_ISHI
         AND s2.taarich =   TO_DATE(SRV_D_TAARICH,'yyyymmdd') +   TAARICH_knisa_p24
         AND s2.mispar_sidur= calc_D_new_sidur
           AND s2.shat_hatchala=TO_DATE(SRV_D_KNISA_X ,'yyyymmddhh24mi') +  TAARICH_knisa_p24 )  ;
  EXCEPTION
   WHEN OTHERS THEN
        RAISE;
     END pro_upd_in_blank;

   PROCEDURE pro_upd_in_out_letashlum(  SRV_D_ISHI VARCHAR,  SRV_D_TAARICH VARCHAR,  calc_D_new_sidur VARCHAR,
      SRV_D_KNISA_X VARCHAR,
   --SRV_D_MIKUM_KNISA varchar,  SRV_D_SIBAT_DIVUACH_KNISA varchar,
      SRV_D_YETZIA_X VARCHAR,
   --SRV_D_MIKUM_YETZIA varchar,  SRV_D_SIBAT_DIVUACH_YETZIA varchar,
      SRV_D_ISHI_MEADKEN VARCHAR,
   SRV_D_KOD_BITUL_ZMAN_NESIA_X VARCHAR,
      --SRV_D_KOD_CHARIGA_X varchar,
   SRV_D_KOD_HALBASHA_X VARCHAR,
   --SRV_D_KOD_HAZMANA_X varchar,
      TAARICH_knisa_p24 NUMBER , TAARICH_yetzia_p24 NUMBER,  DatEfes VARCHAR,
      TAARICH_knisa_letashlum_p24 NUMBER,  SRV_D_KNISA_letashlum_X VARCHAR,
      TAARICH_yetzia_letashlum_p24 NUMBER,  SRV_D_YETZIA_letashlum_X VARCHAR) IS
    BEGIN
               UPDATE TB_SIDURIM_OVDIM
       SET  shat_hatchala_letashlum=
    DECODE(SRV_D_KNISA_letashlum_X,NULL,shat_hatchala_letashlum,TO_DATE(SRV_D_KNISA_letashlum_X ,'yyyymmddhh24mi')  +TAARICH_knisa_letashlum_p24),
    --to_date(SRV_D_KNISA_letashlum_X ,'yyyymmddhh24mi') +  TAARICH_knisa_letashlum_p24,
              shat_gmar_letashlum=
    DECODE(SRV_D_yetzia_letashlum_X,NULL,shat_gmar_letashlum,TO_DATE(SRV_D_yetzia_letashlum_X ,'yyyymmddhh24mi')  +TAARICH_yetzia_letashlum_p24),
     --to_date(SRV_D_YETZIA_letashlum_X ,'yyyymmddhh24mi') +  TAARICH_yetzia_letashlum_p24,
     mezake_nesiot=DECODE(Trim(SRV_D_KOD_BITUL_ZMAN_NESIA_X),'',mezake_nesiot,'0',mezake_nesiot,Trim(SRV_D_KOD_BITUL_ZMAN_NESIA_X)),
              mezake_halbasha=DECODE(Trim(SRV_D_KOD_HALBASHA_X) ,'',mezake_halbasha,'0',mezake_halbasha,Trim(SRV_D_KOD_HALBASHA_X)),
          --meadken_acharon=decode(Trim(SRV_D_ISHI_MEADKEN) ,'',meadken_acharon,'0',meadken_acharon,'00000',meadken_acharon,trim(SRV_D_ISHI_MEADKEN)),
      Menahel_Musach_Meadken=DECODE(Trim(SRV_D_ISHI_MEADKEN) ,'',Menahel_Musach_Meadken,'0',Menahel_Musach_Meadken,'00000',Menahel_Musach_Meadken,trim(SRV_D_ISHI_MEADKEN)),
     taarich_idkun_acharon = SYSDATE
   WHERE mispar_ishi= SRV_D_ISHI
           AND taarich =   TO_DATE(SRV_D_TAARICH,'yyyymmdd') +   TAARICH_knisa_p24
        AND mispar_sidur= calc_D_new_sidur
           AND shat_hatchala =    TO_DATE(SRV_D_KNISA_X ,'yyyymmddhh24mi') +  TAARICH_knisa_p24
          AND shat_gmar=TO_DATE(SRV_D_YETZIA_X ,'yyyymmddhh24mi') + TAARICH_yetzia_p24;
   EXCEPTION
   WHEN OTHERS THEN
        RAISE;
             END pro_upd_in_out_letashlum;


            PROCEDURE pro_ins_yamey_avoda_1oved(SRV_D_ISHI NUMBER,  SRV_D_TAARICH VARCHAR) IS
   BEGIN
                     INSERT INTO TB_YAMEY_AVODA_OVDIM ( mispar_ishi,taarich  )
                     VALUES  (  SRV_D_ISHI ,
                            TO_DATE(SRV_D_TAARICH ,'yyyymmdd')  );
     EXCEPTION
   WHEN OTHERS THEN
        RAISE;
     END pro_ins_yamey_avoda_1oved;

   PROCEDURE pro_new_rec_pundakim(  SRV_D_ISHI VARCHAR,  SRV_D_TAARICH VARCHAR,
                                     SRV_D_KNISA_X VARCHAR,  SRV_D_MIKUM_KNISA VARCHAR,   TAARICH_knisa_p24 NUMBER)  IS
   BEGIN
 IF LENGTH(SRV_D_KNISA_X) = 4 THEN
          INSERT INTO TB_HITYAZVUT_PUNDAKIM ( mispar_ishi  ,   taarich ,  shat_hityazvut   ,mikum_shaon  ,meadken_acharon)
             VALUES  ( SRV_D_ISHI ,
            TO_DATE(SRV_D_TAARICH,'yyyymmdd') +   TAARICH_knisa_p24,
             TO_DATE(SRV_D_KNISA_X ,'yyyymmddhh24mi') +  TAARICH_knisa_p24 ,
            DECODE(Trim(SRV_D_MIKUM_KNISA) , '' ,'0', '00000','0',trim(SRV_D_MIKUM_KNISA) ),
      -11);
   ELSE
          INSERT INTO TB_HITYAZVUT_PUNDAKIM ( mispar_ishi  ,   taarich ,  shat_hityazvut   ,mikum_shaon  ,meadken_acharon)
             VALUES  ( SRV_D_ISHI ,
            TO_DATE(SRV_D_TAARICH,'yyyymmdd') +   TAARICH_knisa_p24,
             TO_DATE(SRV_D_KNISA_X ,'yyyymmddhh24miss') +  TAARICH_knisa_p24 ,
            DECODE(Trim(SRV_D_MIKUM_KNISA) , '' ,'0', '00000','0',trim(SRV_D_MIKUM_KNISA) ),
      -11);
   END IF;
              EXCEPTION
   WHEN OTHERS THEN
        RAISE;
     END pro_new_rec_pundakim;

   PROCEDURE pro_GetListDsPundakim(pDt VARCHAR, pIshi VARCHAR ,p_cur OUT CurType)    IS
     BEGIN
 OPEN p_cur FOR
   SELECT TO_CHAR(shat_hityazvut,'yyyymmddhh24mi') shat_hityazvut
  FROM  TB_HITYAZVUT_PUNDAKIM
  WHERE mispar_ishi=pIshi
  AND taarich= TO_DATE(pDt,'yyyymmdd')
   ORDER BY shat_hityazvut;
         EXCEPTION
   WHEN OTHERS THEN
        RAISE;
     END pro_GetListDsPundakim ;

	 PROCEDURE pro_insert_debug_maatefet(p_mispar_ishi NUMBER, p_taarich DATE, p_taarich_ritza DATE,
                                    p_bakasha_id NUMBER, p_sug_bakasha NUMBER,
                                    p_comments TEST_MAATEFET.comments%TYPE DEFAULT NULL)					 IS
        BEGIN
          INSERT INTO TEST_MAATEFET(mispar_ishi, taarich, taarich_ritza, bakasha_id, sug_bakasha,comments)
          VALUES(p_mispar_ishi , p_taarich , p_taarich_ritza ,
                                    p_bakasha_id , p_sug_bakasha, p_comments);

        END pro_insert_debug_maatefet;


PROCEDURE pro_IfSdrnManas(pDt VARCHAR,pIshi VARCHAR ,p_cur OUT CurType)  IS
     BEGIN
 OPEN p_cur FOR
   SELECT erech FROM PIRTEY_OVDIM
WHERE Kod_Natun=6
AND TRUNC(Erech) IN ('0420', '0422','420','422',420, 422)
AND  TO_DATE(pDt,'yyyymmdd') BETWEEN me_taarich AND ad_taarich
AND mispar_ishi=pIshi ;
 EXCEPTION
   WHEN OTHERS THEN
        RAISE;
END pro_IfSdrnManas;

PROCEDURE pro_get_shinuy_matsav_ovdim( p_Cur OUT CurType) IS
 BEGIN


 		       OPEN p_Cur FOR
		 --select * from(
			   		SELECT   a.mispar_ishi, a.taarich_hatchala,
							   		DECODE(a.taarich_siyum,NULL,TRUNC(SYSDATE-1),DECODE( INSTR((SYSDATE-1)-a.taarich_siyum,'-'),1,TRUNC(SYSDATE-1), a.taarich_siyum)) date_a,
								--	decode( instr((sysdate-1)-b.taarich_siyum,'-'),1,trunc(sysdate-1), b.taarich_siyum) date_b,
								DECODE( INSTR((SYSDATE-1)-b.taarich_siyum,'-'),1,TRUNC(SYSDATE-1),DECODE(b.taarich_siyum,NULL,DECODE(b.kod_matzav,NULL,NULL,TRUNC(SYSDATE-1)),b.taarich_siyum) )date_b,
									a.kod_matzav erech_a,b.kod_matzav erech_b,0 kod
					 FROM
									(SELECT * FROM MATZAV_OVDIM MINUS SELECT * FROM NEW_MATZAV_OVDIM) a,
									NEW_MATZAV_OVDIM b,
									OVDIM o
						WHERE a.mispar_ishi=b.mispar_ishi(+)
						     AND a.mispar_ishi=o.mispar_ishi
							  AND a.taarich_hatchala=b.taarich_hatchala(+)
							  AND a.taarich_hatchala <= TRUNC(SYSDATE) 
							--  and a.mispar_ishi=47906
		UNION
			   		SELECT  a.mispar_ishi, a.taarich_hatchala,
							   		DECODE(a.taarich_siyum,NULL,TRUNC(SYSDATE-1),DECODE( INSTR((SYSDATE-1)-a.taarich_siyum,'-'),1,TRUNC(SYSDATE-1), a.taarich_siyum)) date_a,
									DECODE( INSTR((SYSDATE-1)-b.taarich_siyum,'-'),1,TRUNC(SYSDATE-1),DECODE(b.taarich_siyum,NULL,DECODE(b.kod_matzav,NULL,NULL,TRUNC(SYSDATE-1)),b.taarich_siyum) )date_b,
								--	decode( instr((sysdate-1)-b.taarich_siyum,'-'),1,trunc(sysdate-1), b.taarich_siyum) date_b,
									a.kod_matzav erech_a,b.kod_matzav erech_b,0 kod
					 FROM
									(SELECT * FROM NEW_MATZAV_OVDIM MINUS SELECT * FROM MATZAV_OVDIM) a,
									MATZAV_OVDIM b,
									OVDIM o
						WHERE a.mispar_ishi=b.mispar_ishi(+)
						  	  AND a.mispar_ishi=o.mispar_ishi
							  AND a.taarich_hatchala=b.taarich_hatchala(+)
							  AND a.taarich_hatchala <= TRUNC(SYSDATE);
						--	  and a.mispar_ishi=47906;
				--	  	)
				--	where mispar_ishi =763;
				--	where RowNum<6;


  EXCEPTION
   WHEN OTHERS THEN
        RAISE;
END pro_get_shinuy_matsav_ovdim;

PROCEDURE pro_get_Shinuy_meafyeney_bizua( p_Cur OUT CurType) IS
 BEGIN


 		      OPEN p_Cur FOR
        --select * from(
                              SELECT  a.mispar_ishi, a.me_taarich taarich_hatchala,
                                   DECODE(a.ad_taarich,NULL,TRUNC(SYSDATE-1),DECODE( INSTR((SYSDATE-1)-a.ad_taarich,'-'),1,TRUNC(SYSDATE-1), a.ad_taarich)) date_a,
                    --            decode( instr((sysdate-1)-b.ad_taarich,'-'),1,trunc(sysdate-1), b.ad_taarich) date_b,
                                DECODE( INSTR((SYSDATE-1)-b.ad_taarich,'-'),1,TRUNC(SYSDATE-1),DECODE(b.ad_taarich,NULL,DECODE(b.erech_ishi ,NULL,NULL,TRUNC(SYSDATE-1)),b.ad_taarich) )date_b,
                                a.erech_ishi erech_a,b.erech_ishi erech_b ,0 kod
                     FROM
                                (SELECT * FROM MEAFYENIM_OVDIM MINUS SELECT * FROM NEW_MEAFYENIM_OVDIM) a,
                                NEW_MEAFYENIM_OVDIM b,
                                OVDIM o
                     WHERE a.mispar_ishi=b.mispar_ishi(+)
                               AND a.mispar_ishi=o.mispar_ishi
                            AND a.me_taarich=b.me_taarich(+)
                            AND a.kod_meafyen = b.kod_meafyen(+)
                            AND a.me_taarich <= TRUNC(SYSDATE)
                            AND a.kod_meafyen IN(1,2,3,4,5,6,7,8,11,13,14,23,24,25,26,27,28,30,32,41,42,43,44,45,47,50,51,53,54,56,60,61,63,64,74,83,84,85,91)
                   --    and  a.mispar_ishi=67761
    UNION
                           SELECT  a.mispar_ishi, a.me_taarich taarich_hatchala,
                                   DECODE(a.ad_taarich,NULL,TRUNC(SYSDATE-1),DECODE( INSTR((SYSDATE-1)-a.ad_taarich,'-'),1,TRUNC(SYSDATE-1), a.ad_taarich)) date_a,
                        --        decode( instr((sysdate-1)-b.ad_taarich,'-'),1,trunc(sysdate-1), b.ad_taarich) date_b,
                        DECODE( INSTR((SYSDATE-1)-b.ad_taarich,'-'),1,TRUNC(SYSDATE-1),DECODE(b.ad_taarich,NULL,DECODE(b.erech_ishi ,NULL,NULL,TRUNC(SYSDATE-1)),b.ad_taarich) )date_b,
                                a.erech_ishi erech_a,b.erech_ishi erech_b ,0 kod
                     FROM
                                (SELECT * FROM NEW_MEAFYENIM_OVDIM MINUS SELECT * FROM MEAFYENIM_OVDIM) a,
                                MEAFYENIM_OVDIM b,
                                OVDIM o
                     WHERE a.mispar_ishi=b.mispar_ishi(+)
                            AND a.mispar_ishi=o.mispar_ishi
                            AND a.me_taarich=b.me_taarich(+)
                            AND a.kod_meafyen = b.kod_meafyen(+)
                            AND a.me_taarich<= TRUNC(SYSDATE)
                            AND a.kod_meafyen IN(1,2,3,4,5,6,7,8,11,13,14,23,24,25,26,27,28,30,32,41,42,43,44,45,47,50,51,53,54,56,60,61,63,64,74,83,84,85,91);
                    --and  a.mispar_ishi=67761;
                --    )
            --    where mispar_ishi=224;


  EXCEPTION
   WHEN OTHERS THEN
        RAISE;
END pro_get_Shinuy_meafyeney_bizua;

PROCEDURE pro_get_shinuy_pirey_oved(p_Cur OUT CurType) IS
 BEGIN


 		       OPEN p_Cur FOR
			   		  SELECT   a.mispar_ishi, a.me_taarich taarich_hatchala,
								 DECODE(a.ad_taarich,NULL,TRUNC(SYSDATE-1),DECODE( INSTR((SYSDATE-1)-a.ad_taarich,'-'),1,TRUNC(SYSDATE-1), a.ad_taarich)) date_a,
							--	decode( instr((sysdate-1)-b.ad_taarich,'-'),1,trunc(sysdate-1), b.ad_taarich) date_b,
							DECODE( INSTR((SYSDATE-1)-b.ad_taarich,'-'),1,TRUNC(SYSDATE-1),DECODE(b.ad_taarich,NULL,DECODE(b.erech ,NULL,NULL,TRUNC(SYSDATE-1)),b.ad_taarich) )date_b,
						   		a.erech erech_a,b.erech  erech_b,0 kod
					 FROM
							(SELECT * FROM PIRTEY_OVDIM MINUS SELECT * FROM NEW_PIRTEY_OVDIM) a,
							NEW_PIRTEY_OVDIM b,
							OVDIM o
					WHERE a.mispar_ishi=b.mispar_ishi(+)
					 	   AND a.mispar_ishi=o.mispar_ishi
							AND a.me_taarich=b.me_taarich(+)
							AND a.kod_natun  = b.kod_natun (+)
							AND a.me_taarich <= TRUNC(SYSDATE)
                            AND a.kod_natun IN(6,7,8,10,13,20,21)
						--	and a.mispar_ishi=21470
		UNION
					 SELECT   a.mispar_ishi, a.me_taarich taarich_hatchala,
								 DECODE(a.ad_taarich,NULL,TRUNC(SYSDATE-1),DECODE( INSTR((SYSDATE-1)-a.ad_taarich,'-'),1,TRUNC(SYSDATE-1), a.ad_taarich)) date_a,
							--	decode( instr((sysdate-1)-b.ad_taarich,'-'),1,trunc(sysdate-1), b.ad_taarich) date_b,
							DECODE( INSTR((SYSDATE-1)-b.ad_taarich,'-'),1,TRUNC(SYSDATE-1),DECODE(b.ad_taarich,NULL,DECODE(b.erech ,NULL,NULL,TRUNC(SYSDATE-1)),b.ad_taarich) )date_b,
						   		a.erech erech_a,b.erech  erech_b,0 kod
					 FROM
							(SELECT * FROM NEW_PIRTEY_OVDIM MINUS SELECT * FROM PIRTEY_OVDIM) a,
							PIRTEY_OVDIM b,
							OVDIM o
					WHERE a.mispar_ishi=b.mispar_ishi(+)
					  	    AND a.mispar_ishi=o.mispar_ishi
							AND a.me_taarich=b.me_taarich(+)
							AND a.kod_natun  = b.kod_natun (+)
							AND a.me_taarich <= TRUNC(SYSDATE)
                             AND a.kod_natun IN(6,7,8,10,13,20,21);
						--	and a.mispar_ishi=21470;


  EXCEPTION
   WHEN OTHERS THEN
        RAISE;
END pro_get_shinuy_pirey_oved;

PROCEDURE pro_get_shinuy_brerot_mechdal(p_Cur OUT CurType) IS
 BEGIN


 		       OPEN p_Cur FOR
			   		SELECT a.kod_meafyen  kod, a.me_taarich taarich_hatchala,
											 DECODE(a.ad_taarich,NULL,TRUNC(SYSDATE-1),DECODE( INSTR((SYSDATE-1)-a.ad_taarich,'-'),1,TRUNC(SYSDATE-1), a.ad_taarich)) date_a,
											-- decode( instr((sysdate-1)-b.ad_taarich,'-'),1,trunc(sysdate-1), b.ad_taarich) date_b,
											DECODE( INSTR((SYSDATE-1)-b.ad_taarich,'-'),1,TRUNC(SYSDATE-1),DECODE(b.ad_taarich,NULL,DECODE(b.erech ,NULL,NULL,TRUNC(SYSDATE-1)),b.ad_taarich) )date_b,
						   		             a.erech erech_a,b.erech  erech_b,0 mispar_ishi
								FROM
										    (SELECT A.KOD_MEAFYEN,A.ME_TAARICH,A.AD_TAARICH,A.ERECH  FROM NEW_BREROT_MECHDAL_MEAFYENIM a 
                                                MINUS SELECT B.KOD_MEAFYEN,B.ME_TAARICH,B.AD_TAARICH,B.ERECH FROM BREROT_MECHDAL_MEAFYENIM b) a,
											BREROT_MECHDAL_MEAFYENIM b
								WHERE a.kod_meafyen=b.kod_meafyen(+)
									   AND a.me_taarich=b.me_taarich(+)
									   AND a.me_taarich <= TRUNC(SYSDATE)
									 --  and a.kod_meafyen=5
				UNION
					  	 	   SELECT a.kod_meafyen  kod, a.me_taarich taarich_hatchala,
											 DECODE(a.ad_taarich,NULL,TRUNC(SYSDATE-1),DECODE( INSTR((SYSDATE-1)-a.ad_taarich,'-'),1,TRUNC(SYSDATE-1), a.ad_taarich)) date_a,
									--		 decode( instr((sysdate-1)-b.ad_taarich,'-'),1,trunc(sysdate-1), b.ad_taarich) date_b,
									DECODE( INSTR((SYSDATE-1)-b.ad_taarich,'-'),1,TRUNC(SYSDATE-1),DECODE(b.ad_taarich,NULL,DECODE(b.erech ,NULL,NULL,TRUNC(SYSDATE-1)),b.ad_taarich) )date_b,
						   		             a.erech erech_a,b.erech  erech_b,0 mispar_ishi
								FROM
										   (SELECT B.KOD_MEAFYEN,B.ME_TAARICH,B.AD_TAARICH,B.ERECH FROM BREROT_MECHDAL_MEAFYENIM b
                                              MINUS SELECT A.KOD_MEAFYEN,A.ME_TAARICH,A.AD_TAARICH,A.ERECH FROM NEW_BREROT_MECHDAL_MEAFYENIM a) a,
											NEW_BREROT_MECHDAL_MEAFYENIM b
								WHERE a.kod_meafyen=b.kod_meafyen(+)
									   AND a.me_taarich=b.me_taarich(+)
									   AND a.me_taarich <= TRUNC(SYSDATE);
								--	   and a.kod_meafyen=5;


  EXCEPTION
   WHEN OTHERS THEN
        RAISE;
END pro_get_shinuy_brerot_mechdal;

PROCEDURE pro_ins_ovdim_im_shinuy_hr(p_coll_obj_ovdim_im_shinuy_hr IN coll_ovdim_im_shinuy_hr) IS
idNumber NUMBER;
 BEGIN
    IF (p_coll_obj_ovdim_im_shinuy_hr IS NOT NULL) THEN
                 FOR i IN 1..p_coll_obj_ovdim_im_shinuy_hr.COUNT LOOP
				   	    --check if exist in table
				BEGIN

				SELECT NVL(o.mispar_ishi,0)
						INTO idNumber
						FROM OVDIM_IM_SHINUY_HR o
						WHERE o.mispar_ishi = p_coll_obj_ovdim_im_shinuy_hr(i).mispar_ishi
							  AND o. taarich =   TRUNC(p_coll_obj_ovdim_im_shinuy_hr(i).taarich);
			     EXCEPTION
  				 		    WHEN NO_DATA_FOUND  THEN
        						 idNumber:=0;
				END;
						--if no then insert else update
				IF (idNumber =0) THEN
				   			 	Pkg_Batch.inset_oved_im_shinuy_hr( p_coll_obj_ovdim_im_shinuy_hr(i).mispar_ishi,
																	 TRUNC( p_coll_obj_ovdim_im_shinuy_hr(i).taarich),p_coll_obj_ovdim_im_shinuy_hr(i).tavla);
		       ELSE
								Pkg_Batch.update_oved_im_shinuy_hr	( p_coll_obj_ovdim_im_shinuy_hr(i).mispar_ishi,
																	 TRUNC( p_coll_obj_ovdim_im_shinuy_hr(i).taarich),p_coll_obj_ovdim_im_shinuy_hr(i).tavla);

				 END IF;
			 END LOOP;
	 END IF;
	--select 4 into idNumber from dual;
  EXCEPTION
   WHEN OTHERS THEN
        RAISE;
END pro_ins_ovdim_im_shinuy_hr;

PROCEDURE pro_ins_defaults_hr(p_coll_obj_brerot_mechdal_hr IN coll_brerot_mechdal_meafyenim) IS
CURSOR p_cur(p_kod MEAFYENIM_OVDIM.KOD_MEAFYEN%TYPE ,
	   			   				 p_me_taarich TB_YAMEY_AVODA_OVDIM.TAARICH%TYPE,
								 p_ad_taarich  TB_YAMEY_AVODA_OVDIM.TAARICH%TYPE) IS
	    SELECT DISTINCT y.MISPAR_ISHI,y.TAARICH
					 FROM TB_YAMEY_AVODA_OVDIM y,
					           MEAFYENIM_OVDIM m
					 WHERE y.MISPAR_ISHI = m.MISPAR_ISHI
					 	   		AND  m.KOD_MEAFYEN = p_kod
								AND y.TAARICH BETWEEN  p_me_taarich   AND  p_ad_taarich ;
								--and  y.MISPAR_ISHI=3620;

idNumber NUMBER;
v_rec  p_cur%ROWTYPE;
BEGIN
 IF (p_coll_obj_brerot_mechdal_hr  IS NOT NULL) THEN
       FOR i IN 1..p_coll_obj_brerot_mechdal_hr.COUNT LOOP
			FOR v_rec  IN   p_cur(p_coll_obj_brerot_mechdal_hr(i).kod_meafyen ,
			 						   p_coll_obj_brerot_mechdal_hr(i). me_taarich,
									   p_coll_obj_brerot_mechdal_hr(i). ad_taarich )
			  LOOP
				  	 	BEGIN
							SELECT o.mispar_ishi
								INTO idNumber
								FROM OVDIM_IM_SHINUY_HR o
								 WHERE o.mispar_ishi = v_rec.mispar_ishi
							         AND o. taarich =   TRUNC(v_rec.taarich);
						   EXCEPTION
	  				 		    WHEN NO_DATA_FOUND  THEN
	        						 idNumber:=0;
							END;
								IF (idNumber =0) THEN
				   			 	   			    Pkg_Batch.inset_oved_im_shinuy_hr( v_rec.mispar_ishi, TRUNC(v_rec.taarich),'brerot_mechdal');
						       ELSE
											Pkg_Batch.update_oved_im_shinuy_hr	( v_rec.mispar_ishi, TRUNC(v_rec.taarich),'brerot_mechdal');
								 END IF;
			  END LOOP;
       END LOOP;
END IF;
 EXCEPTION
   WHEN OTHERS THEN
        RAISE;
END pro_ins_defaults_hr;


PROCEDURE inset_oved_im_shinuy_hr(p_mispar_ishi IN NUMBER,
		  											   	   	   			  		 p_taarich IN DATE,
																				 p_tavla IN VARCHAR ) IS
BEGIN
	 	     INSERT INTO OVDIM_IM_SHINUY_HR(
						   						   			   							   	 		 mispar_ishi,
						   									   	  						   	 		 taarich,
																									 taarich_idkun_hr,
																									 tavla )
																			   VALUES (
																									  p_mispar_ishi,
																									 p_taarich,
																									   SYSDATE,
																									   p_tavla);
END inset_oved_im_shinuy_hr;

PROCEDURE update_oved_im_shinuy_hr(p_mispar_ishi IN NUMBER,
		  											   	   	   			  		 p_taarich IN DATE,
																				 p_tavla IN VARCHAR )  IS
BEGIN
	 	  UPDATE  OVDIM_IM_SHINUY_HR o
		  SET o. taarich_idkun_hr=SYSDATE,
		         tavla =p_tavla
		  WHERE 	o.mispar_ishi = p_mispar_ishi
	  				   AND o. taarich =   p_taarich;
END update_oved_im_shinuy_hr;

PROCEDURE MoveNewMatzavOvdimToOld  IS
BEGIN
	 EXECUTE IMMEDIATE  'truncate table Matzav_Ovdim' ;    
	 EXECUTE IMMEDIATE ' insert into Matzav_Ovdim select * from new_Matzav_Ovdim';
EXCEPTION
   WHEN OTHERS THEN
   		ROLLBACK;
        RAISE;
END MoveNewMatzavOvdimToOld;

PROCEDURE MoveNewPirteyOvedToOld  IS
BEGIN
	  EXECUTE IMMEDIATE  'truncate table pirtey_ovdim' ;    
	 EXECUTE IMMEDIATE 'insert into pirtey_ovdim select * from new_pirtey_ovdim';
EXCEPTION
   WHEN OTHERS THEN
   		ROLLBACK;
        RAISE;
END MoveNewPirteyOvedToOld;

PROCEDURE MoveNewMeafyenimOvdimToOld  IS
BEGIN
	 EXECUTE IMMEDIATE  'truncate table meafyenim_ovdim' ;    
	 EXECUTE IMMEDIATE 'insert into meafyenim_ovdim select * from new_meafyenim_ovdim';
EXCEPTION
   WHEN OTHERS THEN
   		ROLLBACK;
        RAISE;
END MoveNewMeafyenimOvdimToOld;

PROCEDURE MoveNewBrerotMechdalToOld  IS
BEGIN
	-- EXECUTE IMMEDIATE  'truncate table Brerot_Mechdal_Meafyenim' ;
	 DELETE FROM     BREROT_MECHDAL_MEAFYENIM ;
	 EXECUTE IMMEDIATE 'insert into Brerot_Mechdal_Meafyenim select * from new_Brerot_Mechdal_Meafyenim';
EXCEPTION
   WHEN OTHERS THEN
   		ROLLBACK;
        RAISE;
END  MoveNewBrerotMechdalToOld;


PROCEDURE pro_get_ovdim4rerunsdrn( pDt VARCHAR,p_Cur OUT CurType) IS
  BEGIN
  OPEN p_Cur FOR
SELECT y.mispar_ishi
  FROM TB_YAMEY_AVODA_OVDIM y
WHERE  y.taarich=TO_DATE(pDt,'yyyymmdd')
 AND measher_o_mistayeg IS NULL
 UNION ALL
 SELECT y.mispar_ishi
  FROM TB_YAMEY_AVODA_OVDIM y
WHERE  y.taarich=TO_DATE(pDt,'yyyymmdd')
 AND measher_o_mistayeg =1
AND  NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s
  	 	 				  	   WHERE  s.taarich=TO_DATE(pDt,'yyyymmdd')
							   AND s.taarich=y.taarich
							   AND s.mispar_ishi=y.mispar_ishi
						   AND (s.mispar_sidur<99000 OR s.mispar_sidur>99999));
						   EXCEPTION
   WHEN OTHERS THEN
        RAISE;
END  pro_get_ovdim4rerunsdrn;

  PROCEDURE pro_sof_meafyenim( pDt VARCHAR,p_Cur OUT CurType)  IS
     BEGIN
 OPEN p_Cur FOR
SELECT  COUNT(*) ct
FROM   TB_LOG_TAHALICH
WHERE TRUNC(taarich)  = TO_DATE(pDt,'yyyymmdd')
  AND kod_tahalich=3
  AND kod_peilut_tahalich =33
  AND status=2;
						   EXCEPTION
   WHEN OTHERS THEN
        RAISE;
END  pro_sof_meafyenim;

PROCEDURE pro_get_premia_input(p_taarich DATE, p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE,p_Cur OUT CurType) IS
     --   p_tar DATE;
BEGIN
      --  p_tar:= TO_DATE('01/' || TO_CHAR(p_taarich,'mm/yyyy'));
    OPEN p_Cur FOR
       SELECT cs.MISPAR_ISHI, CS.MISPAR_SIDUR,cs.TAARICH, cs.KOD_RECHIV ,cs.ERECH_RECHIV,
           SP.KOD_PREMIA,PO.EZOR, PO.MAAMAD, PO.MUTAAM, PO.GIL, PO.ISUK,O.SHEM_MISH,O.SHEM_PRAT,
           Pkg_Ovdim.fun_get_meafyen_oved(cs.MISPAR_ISHI,60,p_taarich) meafeyn_60,
           Pkg_Ovdim.fun_get_meafyen_oved(cs.MISPAR_ISHI,74,p_taarich) meafeyn_74,
           Pkg_Ovdim.func_get_mispar_tachana(cs.MISPAR_ISHI,CS.MISPAR_SIDUR,cs.TAARICH,cs.SHAT_HATCHALA) mispar_tachana
        FROM TB_CHISHUV_SIDUR_OVDIM cs,
                 CTB_SUGEY_PREMIOT sp ,
                 PIVOT_PIRTEY_OVDIM po,
                 OVDIM o 
        WHERE   cs.BAKASHA_ID=p_bakasha_id
        AND cs.taarich BETWEEN p_taarich AND LAST_DAY(p_taarich) -- to_char(cs.taarich,'mm/yyyy')=to_char(p_taarich,'mm/yyyy')
        AND  cs.KOD_RECHIV IN (256,257,258,259,260)      
  --    AND  cs.MISPAR_ISHI= 78387
        AND cs.KOD_RECHIV=SP.KOD_RACHIV_NOCHECHUT(+)
        AND cs.MISPAR_ISHI=O.MISPAR_ISHI
        AND cs.MISPAR_ISHI=PO.MISPAR_ISHI
        AND p_taarich BETWEEN PO.ME_TARICH AND NVL(PO.AD_TARICH,p_taarich+1)
        ORDER BY MISPAR_ISHI,KOD_PREMIA,mispar_tachana,taarich;
 
END pro_get_premia_input;
PROCEDURE pro_update_calc_premia(p_taarich DATE, p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE, p_mispar_ishi NUMBER,
            p_kod_rechiv NUMBER, p_erech_rechiv NUMBER) AS
 v_count NUMBER;
BEGIN
    SELECT COUNT(*) INTO v_count
    FROM TB_CHISHUV_CHODESH_OVDIM t 
    WHERE  T.KOD_RECHIV = p_kod_rechiv
        AND T.MISPAR_ISHI = p_mispar_ishi
        AND T.TAARICH = p_taarich
        AND T.BAKASHA_ID = p_bakasha_id;
    
    IF v_count=0 THEN
        INSERT INTO TB_CHISHUV_CHODESH_OVDIM(MISPAR_ISHI,TAARICH,BAKASHA_ID,KOD_RECHIV,ERECH_RECHIV )
        VALUES(p_mispar_ishi,p_taarich,p_bakasha_id,p_kod_rechiv,p_erech_rechiv);
    ELSE
        UPDATE TB_CHISHUV_CHODESH_OVDIM t 
            SET T.ERECH_RECHIV=p_erech_rechiv
        WHERE  T.KOD_RECHIV = p_kod_rechiv
            AND T.MISPAR_ISHI = p_mispar_ishi
            AND T.TAARICH = p_taarich
            AND T.BAKASHA_ID = p_bakasha_id;
    END IF;
    
END pro_update_calc_premia;
PROCEDURE pro_get_premia_bakashot(p_taarich DATE,p_Cur OUT CurType) IS
BEGIN
    OPEN p_Cur FOR
    SELECT TB.Bakasha_ID,TB.Bakasha_ID || ' '||  TB.Teur || ' ' || TO_CHAR(TB.Zman_Hatchala,'dd.mm.yyyy') Teur_Bakasha
        FROM TB_BAKASHOT TB,
        TB_BAKASHOT_PARAMS TC 
        WHERE TB.Sug_Bakasha=1
        AND TB.Status IN (2,4)
        AND TB.Bakasha_ID=TC.Bakasha_ID
        AND TC.PARAM_ID = 2
        AND TO_DATE('01/' || TC.ERECH,'dd/mm/yyyy') >= p_taarich
        ORDER BY TB.Zman_Hatchala;

END pro_get_premia_bakashot;
PROCEDURE pro_if_start(p_Cur OUT CurType) IS
  BEGIN
  OPEN p_Cur FOR
SELECT  COUNT(*) ct
FROM TB_LOG_TAHALICH
WHERE taarich>=TRUNC(SYSDATE)
 AND kod_tahalich=1
AND kod_peilut_tahalich=1
AND trim(teur_tech)='KdsSchedulerProc'  ;
   END pro_if_start;
   
 PROCEDURE pro_if_GalreadyRun(p_Cur OUT CurType) IS
  BEGIN
  OPEN p_Cur FOR
 SELECT   NVL(MAX(status),0) stat2
 --nvl(min( status),0) stat1,
FROM TB_LOG_TAHALICH   
WHERE taarich>=TRUNC(SYSDATE)
AND kod_tahalich=8
AND kod_peilut_tahalich=3;
--and ( status=1  or  status=2);
   END pro_if_GalreadyRun;

PROCEDURE pro_get_ovdim_lehishuv_premiot(p_Cur OUT CurType) IS
 BEGIN
    OPEN p_Cur FOR
    SELECT mispar_ishi,chodesh
    FROM OVDIM_LECHISHUV_PREMYOT lp
    WHERE LP.BAKASHA_ID IS NULL
    ORDER BY chodesh;
 END pro_get_ovdim_lehishuv_premiot; 
 
 /*PROCEDURE pro_update_chishuv_premia(p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE,
            p_mispar_ishi OVDIM_LECHISHUV_PREMYOT.MISPAR_ISHI%TYPE,
            p_chodesh OVDIM_LECHISHUV_PREMYOT.chodesh%TYPE) IS
  BEGIN
    UPDATE OVDIM_LECHISHUV_PREMYOT lp
    SET LP.BAKASHA_ID=p_bakasha_id
    WHERE LP.MISPAR_ISHI=p_mispar_ishi 
        AND LP.CHODESH=p_chodesh; 
  END pro_update_chishuv_premia;*/
  
PROCEDURE pro_update_chishuv_premia(p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE) IS
   CURSOR p_cur( p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE) IS
        SELECT DISTINCT c.MISPAR_ISHI,c.TAARICH
        FROM TB_CHISHUV_CHODESH_OVDIM c
        WHERE C.BAKASHA_ID = p_bakasha_id;
v_rec  p_cur%ROWTYPE;
BEGIN
            FOR v_rec  IN   p_cur(p_bakasha_id)
            LOOP
              
                   UPDATE OVDIM_LECHISHUV_PREMYOT lp
                   SET LP.BAKASHA_ID=p_bakasha_id
                   WHERE LP.MISPAR_ISHI=  v_rec.mispar_ishi
                        AND LP.CHODESH=v_rec.taarich; 
  
            END LOOP;
     EXCEPTION
         WHEN OTHERS THEN
              RAISE;
  END pro_update_chishuv_premia;
 
  FUNCTION fun_get_num_changes_to_shguim RETURN NUMBER AS
num NUMBER ;
BEGIN

  SELECT COUNT(*) INTO num
        FROM OVDIM_IM_SHINUY_HR h, TB_YAMEY_AVODA_OVDIM y,
          CTB_ISUK i,   PIVOT_PIRTEY_OVDIM v_pirty_oved,OVDIM ov
        WHERE h.Mispar_Ishi=y.Mispar_Ishi
          AND h.Taarich=Y.Taarich
          AND NVL(y. RITZAT_SHGIOT_ACHARONA,h.Taarich_Idkun_HR-1)<h.Taarich_Idkun_HR
          AND (y.STATUS<>0 OR y.STATUS IS NULL)
          AND y. Measher_O_Mistayeg IS NOT NULL
            AND h.mispar_ishi            = ov.mispar_ishi
            AND v_pirty_oved.mispar_ishi(+) = h.mispar_ishi
        AND h.taarich  BETWEEN v_pirty_oved.me_tarich(+) AND v_pirty_oved.ad_tarich(+)
         AND i.kod_hevra = ov.kod_hevra
         AND  i.kod_isuk =v_pirty_oved.isuk;

    RETURN num  ;

       EXCEPTION
            WHEN NO_DATA_FOUND THEN
                      num := 0 ;
                          RETURN num  ;
            WHEN OTHERS THEN
                          RAISE;
END fun_get_num_changes_to_shguim;


FUNCTION pro_ins_log_tahalich_rec(p_KodTahalich  NUMBER  ,p_KodPeilut NUMBER,  
		  										  		  			   			p_KodStatus  NUMBER ,  p_TeurTech VARCHAR ,p_KodTakala NUMBER ) RETURN NUMBER AS
p_mispar_siduri TB_LOG_TAHALICH.seq%TYPE;
  BEGIN

      SELECT seq_tahalich.NEXTVAL INTO p_mispar_siduri FROM dual;

        INSERT INTO  TB_LOG_TAHALICH
   		VALUES (p_KodTahalich,p_KodPeilut,p_mispar_siduri,SYSDATE,p_KodStatus,p_KodTakala,NULL,p_TeurTech);

		RETURN p_mispar_siduri ;
      EXCEPTION
		   WHEN OTHERS THEN
		   	     p_mispar_siduri  := 0 ;
		         RETURN p_mispar_siduri  ;
		        RAISE;
  END pro_ins_log_tahalich_rec;
  
  PROCEDURE pro_upd_log_tahalich_rec(p_seqTahalich NUMBER ,p_KodStatus NUMBER,  p_TeurTech VARCHAR ,p_KodTakala NUMBER ) AS
  BEGIN

      UPDATE  TB_LOG_TAHALICH t
	  SET t.STATUS=p_KodStatus,
	  	     t.TEUR_TECH=p_TeurTech,
			 t.KOD_TAKALA = p_KodTakala,
			 t.TAARICH_SGIRA = SYSDATE
	  WHERE t.SEQ=p_seqTahalich;

     
      EXCEPTION
		   WHEN OTHERS THEN
		        RAISE;
  END pro_upd_log_tahalich_rec;
  
  PROCEDURE pro_delete_log_tahalich_rcds AS
  BEGIN
  	   			 DELETE FROM TB_LOG_TAHALICH 
				 WHERE TRUNC(taarich) < ADD_MONTHS(SYSDATE,-2);
      EXCEPTION
		   WHEN OTHERS THEN
		        RAISE;
  END pro_delete_log_tahalich_rcds;
  
  PROCEDURE pro_upd_yamimOfSdrn    IS
  idNumber NUMBER;
err_str VARCHAR(1000);
		
CURSOR  stam5 IS
SELECT teur_tech
 FROM TB_LOG_TAHALICH
WHERE   taarich>SYSDATE-0.01
AND (kod_tahalich=4 OR kod_tahalich=15)
AND kod_peilut_tahalich=1;
--and seq between 30 and 44;

 BEGIN
 idNumber:=0;
err_str:='';

   pro_upd_yamey_avoda_ovdim(TO_CHAR(SYSDATE-1,'yyyymmdd'));

FOR  stam5_rec IN  stam5  LOOP
BEGIN
err_str:=err_str ||trim(stam5_rec.teur_tech);
END;
END LOOP;
COMMIT;

SELECT COUNT(*)
 INTO idNumber
 FROM TB_LOG_TAHALICH
WHERE   taarich>SYSDATE-0.01  
AND (kod_tahalich=4 OR kod_tahalich=15)
AND kod_peilut_tahalich=1;
--and seq between 30 and 44;

  	IF (idNumber >0) THEN
    RAISE_APPLICATION_ERROR(-20005, 'duplicate key in sdrn', TRUE);
 END IF;
 
  EXCEPTION
     WHEN NO_DATA_FOUND  THEN
        						 idNumber:=0;
   WHEN OTHERS THEN
        RAISE;
  
END pro_upd_yamimOfSdrn; 


 PROCEDURE pro_get_meafyenim_gap(p_num_process NUMBER,   p_cur OUT CurType) IS
  
BEGIN
     OPEN p_cur FOR	
	 --stepa : add  to an existing kod "the rest" of the month
	 SELECT DISTINCT   p1.mispar_ishi oved,p1.kod_meafyen kodm,--p1.me_taarich ,p1.ad_taarich,
	p1.ad_taarich+1 miss_me, NVL(LEAST(p3.me_taarich-1,LAST_DAY(s.taarich)),LAST_DAY(s.taarich)) miss_ad  ,b.erech
FROM   MEAFYENIM_OVDIM  p1,MEAFYENIM_OVDIM  p3,BREROT_MECHDAL_MEAFYENIM b,TB_MISPAR_ISHI_CHISHUV s
	WHERE   p1.me_taarich BETWEEN s.taarich AND  LAST_DAY(s.taarich)
			AND p1.ad_taarich+1 BETWEEN s.taarich AND  LAST_DAY(s.taarich)
			AND p3.mispar_ishi(+)=p1.mispar_ishi
		    AND p3.kod_meafyen(+)=p1.kod_meafyen
	        AND p3.me_taarich(+) >   p1.ad_taarich+1
			AND b.kod_meafyen = p1.kod_meafyen
			AND    S.NUM_PACK =p_num_process
						 AND s.mispar_ishi  = p1.mispar_ishi
	--  	    and ((p1.ad_taarich+1 between b.me_taarich and nvl(b.ad_taarich,sysdate+200)) or
	--					( nvl(least(p3.me_taarich-1,last_day(s.taarich)),last_day(s.taarich))  between b.me_taarich and nvl(b.ad_taarich,sysdate+200)))
		AND NOT EXISTS ( SELECT *  FROM   MEAFYENIM_OVDIM  p2
						   	 		   	  WHERE   p2.mispar_ishi=p1.mispar_ishi
										  		  AND p2.kod_meafyen=p1.kod_meafyen
												  AND p2.me_taarich = p1.ad_taarich+1)
	   AND NOT EXISTS (   SELECT p8.mispar_ishi,p8.kod_meafyen,COUNT(*)
					   	   		  		 FROM   MEAFYENIM_OVDIM  p8
										   WHERE  ((me_taarich<= s.taarich AND ad_taarich<=LAST_DAY(s.taarich) AND ad_taarich>= s.taarich ) OR
										     	  		(me_taarich>= s.taarich  AND ad_taarich<=LAST_DAY(s.taarich)) OR
														(me_taarich>= s.taarich  AND ad_taarich>=LAST_DAY(s.taarich) AND me_taarich<= LAST_DAY(s.taarich) ) OR
														(me_taarich<= s.taarich  AND ad_taarich>=LAST_DAY(s.taarich)))
										AND p8.mispar_ishi=p1.mispar_ishi
										AND p8.kod_meafyen=p1.kod_meafyen
										GROUP BY p8.kod_meafyen,p8.mispar_ishi
										HAVING COUNT(*)>1)
  --stepb : add  to an existing kod "the beginning" of the month
		UNION ALL
		SELECT DISTINCT   p4.mispar_ishi oved,p4.kod_meafyen kodm,--p4.me_taarich ,p4.ad_taarich,
	 NVL(GREATEST(p6.ad_taarich+1,s.taarich),s.taarich)  miss_me,
	 p4.me_taarich-1  miss_ad,b.erech
FROM   MEAFYENIM_OVDIM  p4,MEAFYENIM_OVDIM  p6,BREROT_MECHDAL_MEAFYENIM b,TB_MISPAR_ISHI_CHISHUV s
	WHERE  p4.me_taarich BETWEEN s.taarich AND  LAST_DAY(s.taarich)
		AND		 p4.me_taarich-1 BETWEEN s.taarich AND  LAST_DAY(s.taarich)
		 AND p6.mispar_ishi(+)=p4.mispar_ishi
		     AND p6.kod_meafyen(+)=p4.kod_meafyen
	        AND p6.ad_taarich(+) <=   p4.me_taarich-1
				AND b.kod_meafyen = p4.kod_meafyen
				AND    S.NUM_PACK =p_num_process
							 AND s.mispar_ishi  = p4.mispar_ishi
			AND NOT EXISTS ( SELECT *  FROM   MEAFYENIM_OVDIM  p5
						   	 		   	  WHERE   p5.mispar_ishi=p4.mispar_ishi
										  		  AND p5.kod_meafyen=p4.kod_meafyen
												  AND p5.ad_taarich = p4.me_taarich-1) 
		 AND  NOT EXISTS (   SELECT p8.mispar_ishi,p8.kod_meafyen,COUNT(*)
					   	   		  		 FROM   MEAFYENIM_OVDIM  p8
										   WHERE  ((me_taarich<= s.taarich AND ad_taarich<=LAST_DAY(s.taarich) AND ad_taarich>= s.taarich ) OR
										     	  		(me_taarich>= s.taarich  AND ad_taarich<=LAST_DAY(s.taarich)) OR
														(me_taarich>= s.taarich  AND ad_taarich>=LAST_DAY(s.taarich) AND me_taarich<= LAST_DAY(s.taarich) ) OR
														(me_taarich<= s.taarich  AND ad_taarich>=LAST_DAY(s.taarich)))
										AND p8.mispar_ishi=p4.mispar_ishi
										AND p8.kod_meafyen=p4.kod_meafyen
										GROUP BY p8.kod_meafyen,p8.mispar_ishi
										HAVING COUNT(*)>1)
   --stepc :  	add to a whole month where the kod does not exist
   UNION ALL
   SELECT DISTINCT s.mispar_ishi oved ,kod_meafyen_bitzua kodm , s.taarich miss_me , LAST_DAY(s.taarich) miss_ad  ,b.erech
  FROM CTB_MEAFYEN_BITZUA, BREROT_MECHDAL_MEAFYENIM b,TB_MISPAR_ISHI_CHISHUV s
WHERE     S.NUM_PACK =p_num_process
AND NOT EXISTS (SELECT * FROM   MEAFYENIM_OVDIM  p1
  WHERE p1.kod_meafyen=kod_meafyen_bitzua
  AND ((me_taarich<= s.taarich  AND ad_taarich<=LAST_DAY(s.taarich) AND ad_taarich>= s.taarich ) OR
  	  		(me_taarich>= s.taarich  AND ad_taarich<=LAST_DAY(s.taarich)) OR
			(me_taarich>= s.taarich  AND ad_taarich>=LAST_DAY(s.taarich) AND me_taarich<= LAST_DAY(s.taarich) ) OR
			(me_taarich<= s.taarich  AND ad_taarich>=LAST_DAY(s.taarich))))
AND b.kod_meafyen = kod_meafyen_bitzua
   --todo  stepd :  	pro_get_meafyenim_manygaps use cursors where there is more than one gap 
  ORDER BY oved,kodm  ;
      EXCEPTION
		   WHEN OTHERS THEN
		        RAISE;
  END pro_get_meafyenim_gap;
  
 PROCEDURE pro_get_meafyenim_manygaps(p_num_process NUMBER,   p_cur OUT CurType) IS
   v_dt DATE;
  max_dt DATE;
   
 CURSOR many_gaps IS
 SELECT DISTINCT    oved,  kodm,  miss_me,  miss_ad  
FROM (SELECT DISTINCT   p1.mispar_ishi oved,p1.kod_meafyen kodm,
	s.taarich  miss_me, LAST_DAY(s.taarich) miss_ad 
FROM   MEAFYENIM_OVDIM  p1,MEAFYENIM_OVDIM  p3,BREROT_MECHDAL_MEAFYENIM b,TB_MISPAR_ISHI_CHISHUV s
	WHERE   p1.me_taarich BETWEEN s.taarich AND  LAST_DAY(s.taarich)
			AND p1.ad_taarich+1 BETWEEN s.taarich AND  LAST_DAY(s.taarich)
			AND p3.mispar_ishi(+)=p1.mispar_ishi
		    AND p3.kod_meafyen(+)=p1.kod_meafyen
	        AND p3.me_taarich(+) >   p1.ad_taarich+1
			AND b.kod_meafyen = p1.kod_meafyen
			AND    S.NUM_PACK =p_num_process
				 AND s.mispar_ishi  = p1.mispar_ishi
		AND NOT EXISTS ( SELECT *  FROM   MEAFYENIM_OVDIM  p2
						   	 		   	  WHERE   p2.mispar_ishi=p1.mispar_ishi
										  		  AND p2.kod_meafyen=p1.kod_meafyen
												  AND p2.me_taarich = p1.ad_taarich+1)
AND  EXISTS (   SELECT p8.mispar_ishi,p8.kod_meafyen,COUNT(*)
					   	   		  		 FROM   MEAFYENIM_OVDIM  p8
										   WHERE  ((me_taarich<= s.taarich AND ad_taarich<=LAST_DAY(s.taarich) AND ad_taarich>= s.taarich ) OR
										     	  		(me_taarich>= s.taarich  AND ad_taarich<=LAST_DAY(s.taarich)) OR
														(me_taarich>= s.taarich  AND ad_taarich>=LAST_DAY(s.taarich) AND me_taarich<= LAST_DAY(s.taarich) ) OR
														(me_taarich<= s.taarich  AND ad_taarich>=LAST_DAY(s.taarich)))
										AND p8.mispar_ishi=p1.mispar_ishi
										AND p8.kod_meafyen=p1.kod_meafyen
										GROUP BY p8.kod_meafyen,p8.mispar_ishi
										HAVING COUNT(*)>1)
 --stepb : add  to an existing kod "the beginning" of the month
		UNION ALL
		SELECT DISTINCT   p4.mispar_ishi oved,p4.kod_meafyen kodm,
	s.taarich  miss_me, LAST_DAY(s.taarich) miss_ad 
FROM   MEAFYENIM_OVDIM  p4,MEAFYENIM_OVDIM  p6,BREROT_MECHDAL_MEAFYENIM b,TB_MISPAR_ISHI_CHISHUV s
	WHERE  p4.me_taarich BETWEEN s.taarich AND  LAST_DAY(s.taarich)
		AND		 p4.me_taarich-1 BETWEEN s.taarich AND  LAST_DAY(s.taarich)
		 AND p6.mispar_ishi(+)=p4.mispar_ishi
		     AND p6.kod_meafyen(+)=p4.kod_meafyen
	        AND p6.ad_taarich(+) <=   p4.me_taarich-1
				AND b.kod_meafyen = p4.kod_meafyen
				AND    S.NUM_PACK =p_num_process
							 AND s.mispar_ishi  = p4.mispar_ishi
			AND NOT EXISTS ( SELECT *  FROM   MEAFYENIM_OVDIM  p5
						   	 		   	  WHERE   p5.mispar_ishi=p4.mispar_ishi
										  		  AND p5.kod_meafyen=p4.kod_meafyen
												  AND p5.ad_taarich = p4.me_taarich-1) 
		 AND  EXISTS (   SELECT p8.mispar_ishi,p8.kod_meafyen,COUNT(*)
					   	   		  		 FROM   MEAFYENIM_OVDIM  p8
										   WHERE  ((me_taarich<= s.taarich AND ad_taarich<=LAST_DAY(s.taarich) AND ad_taarich>= s.taarich ) OR
										     	  		(me_taarich>= s.taarich  AND ad_taarich<=LAST_DAY(s.taarich)) OR
														(me_taarich>= s.taarich  AND ad_taarich>=LAST_DAY(s.taarich) AND me_taarich<= LAST_DAY(s.taarich) ) OR
														(me_taarich<= s.taarich  AND ad_taarich>=LAST_DAY(s.taarich)))
										AND p8.mispar_ishi=p4.mispar_ishi
										AND p8.kod_meafyen=p4.kod_meafyen
										GROUP BY p8.kod_meafyen,p8.mispar_ishi
										HAVING COUNT(*)>1))
  ORDER BY oved,kodm;
  
  CURSOR meafyeney_oved(par_ishi NUMBER,p_meafyen NUMBER) IS
  SELECT p9.mispar_ishi,p9.kod_meafyen,p9.me_taarich,p9.ad_taarich
FROM   MEAFYENIM_OVDIM p9  ,TB_MISPAR_ISHI_CHISHUV s
	WHERE    p9.mispar_ishi=par_ishi 
		     AND  p9.kod_meafyen=p_meafyen
			 AND    S.NUM_PACK =p_num_process
				 AND s.mispar_ishi  = p9.mispar_ishi
			 AND ((p9.me_taarich<= s.taarich  AND p9.ad_taarich<=LAST_DAY(s.taarich) AND p9.ad_taarich>= s.taarich ) OR
  	  		(p9.me_taarich>= s.taarich  AND p9.ad_taarich<=LAST_DAY(s.taarich)) OR
			(p9.me_taarich>= s.taarich  AND p9.ad_taarich>=LAST_DAY(s.taarich) AND p9.me_taarich<= LAST_DAY(s.taarich) ) OR
			(p9.me_taarich<= s.taarich  AND p9.ad_taarich>=LAST_DAY(s.taarich)));

  
BEGIN
   --todo  stepd :   there is more than one gap 
FOR  many_gaps_rec IN  many_gaps  LOOP
 v_dt:=many_gaps_rec.miss_me;
 max_dt:=many_gaps_rec.miss_ad;

FOR  meafyeney_oved_rec IN  meafyeney_oved(many_gaps_rec.oved,many_gaps_rec.kodm) LOOP
	 IF (meafyeney_oved_rec.me_taarich > v_dt) THEN
	-- insert into tb_log_tahalich values (40,meafyeney_oved_rec.kod_meafyen,meafyeney_oved_rec.mispar_ishi,v_dt,0,0,least(max_dt,meafyeney_oved_rec.me_taarich-1),'');
	INSERT INTO TMP_MEAFYENIM_GAP 
		   VALUES (meafyeney_oved_rec.mispar_ishi,meafyeney_oved_rec.kod_meafyen,v_dt,LEAST(max_dt,meafyeney_oved_rec.me_taarich-1));
	 END IF;
	 v_dt:=meafyeney_oved_rec.ad_taarich+1;
END LOOP;
	IF (v_dt <= max_dt) THEN
	-- insert into tb_log_tahalich values (40,many_gaps_rec.kodm,many_gaps_rec.oved,v_dt,0,0,max_dt,'');
	INSERT INTO TMP_MEAFYENIM_GAP 
		VALUES (many_gaps_rec.oved,many_gaps_rec.kodm,v_dt,max_dt);
 	 END IF;

END LOOP;

 OPEN p_cur FOR	
 	  SELECT MISPAR_ISHI   ,  Kod_meafyen	,  Me_taarich  ,  ad_taarich   
	  FROM TMP_MEAFYENIM_GAP;

--beware to commit; not to lose the data!!

      EXCEPTION
		   WHEN OTHERS THEN
		        RAISE;
  END pro_get_meafyenim_manygaps;
  
  PROCEDURE Pro_get_pirtey_ovdim_leRikuzim(p_bakasha_id NUMBER, p_cur OUT CurType) IS
  p_date DATE;
  BEGIN
  
        SELECT TO_DATE('01/' ||p.erech ,'dd/mm/yyyy') INTO p_date
        FROM TB_BAKASHOT_PARAMS p, TB_BAKASHOT t
        WHERE t.bakasha_id = p_bakasha_id
            AND t.sug_bakasha=1
            AND  t.bakasha_id =  p.bakasha_id  
            AND p.param_id=2;
     OPEN p_cur FOR   
        SELECT DISTINCT S.MISPAR_ISHI ,S.TAARICH ,
                 NVL(p.erech,r.erech)  WorkDay, 
                B.ZMAN_HATCHALA,c.SUG_CHISHUV, o.kod_hevra,
                 ppo.ezor,ppo.maamad    
        FROM TB_CHISHUV_CHODESH_OVDIM s ,TB_BAKASHOT b ,
                BREROT_MECHDAL_MEAFYENIM r,TB_MISPAR_ISHI_SUG_CHISHUV c,
                ovdim o,
                 (select p.mispar_ishi,p.ezor,p.MAAMAD from
                    PIVOT_PIRTEY_OVDIM p,
                       (SELECT Mispar_Ishi,MAX( po.ME_TARICH) me_taarich
                        FROM PIVOT_PIRTEY_OVDIM PO
                         WHERE     (p_date   BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                         OR  TRUNC(LAST_DAY(p_date)) BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                         OR   po.ME_TARICH>=p_date  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<= TRUNC(LAST_DAY(p_date)))
                         group by po.Mispar_Ishi) ppo
                     where  ppo.Mispar_Ishi=p.mispar_ishi
                    AND  ppo.ME_TAARICH= p.ME_TARICH) ppo,
                (SELECT * FROM(
                SELECT p.mispar_ishi,P.ME_TAARICH, NVL(P.ERECH_ISHI,P.ERECH_MECHDAL_PARTANY) erech  ,
                 MAX(p.ME_TAARICH) OVER (PARTITION BY p.mispar_ishi) max_me_tarich
                 FROM   PIVOT_MEAFYENIM_OVDIM p
                 WHERE      
                       ((p.me_taarich<=p_date    AND p.ad_taarich<= LAST_DAY(p_date)  AND p.ad_taarich>=  p_date   ) OR
                         (p.me_taarich>=  p_date   AND p.ad_taarich<= LAST_DAY( p_date )  ) OR
                         (p.me_taarich>=p_date   AND p.ad_taarich>=  LAST_DAY(p_date)  AND p.me_taarich<=  LAST_DAY( p_date )   ) OR
                         (p.me_taarich<= p_date   AND p.ad_taarich>= LAST_DAY( p_date )  ))  
                     AND p.KOD_MEAFYEN =56 ) po
                     WHERE po.ME_TAARICH = po.max_me_tarich
                  ) p
        WHERE S.BAKASHA_ID = p_bakasha_id
            AND S.BAKASHA_ID = B.BAKASHA_ID
                  AND S.MISPAR_ISHI = o.MISPAR_ISHI(+)
               AND S.MISPAR_ISHI = ppo.MISPAR_ISHI(+)
            AND S.MISPAR_ISHI = P.MISPAR_ISHI(+)
            AND S.MISPAR_ISHI = c.MISPAR_ISHI(+)
            AND s.BAKASHA_ID = c.BAKASHA_ID(+)
            AND S.TAARICH = c.TAARICH(+)
            AND r.KOD_MEAFYEN =56;
             
       /* SELECT DISTINCT S.MISPAR_ISHI ,S.TAARICH ,
                 NVL(NVL(P.ERECH_ISHI,P.ERECH_MECHDAL_PARTANY),r.erech)  WorkDay, 
                 B.ZMAN_HATCHALA,c.SUG_CHISHUV        
        FROM TB_CHISHUV_CHODESH_OVDIM s ,TB_BAKASHOT b ,PIVOT_MEAFYENIM_OVDIM p,
                BREROT_MECHDAL_MEAFYENIM r,TB_MISPAR_ISHI_SUG_CHISHUV c
        WHERE S.BAKASHA_ID = p_bakasha_id
            AND S.BAKASHA_ID = B.BAKASHA_ID
            AND S.MISPAR_ISHI = P.MISPAR_ISHI
            AND s.TAARICH BETWEEN P.ME_TAARICH AND P.AD_TAARICH
            AND S.MISPAR_ISHI = c.MISPAR_ISHI(+)
            AND s.BAKASHA_ID = c.BAKASHA_ID(+)
            AND S.TAARICH = c.TAARICH(+)
            AND p.kod_meafyen = r.kod_meafyen
            AND P.KOD_MEAFYEN =56;*/
        
  END Pro_get_pirtey_ovdim_leRikuzim;
  
  PROCEDURE Pro_get_Email_Ovdim_LeRikuzim(p_bakasha_id NUMBER, p_cur OUT CurType) IS
  BEGIN
  
     OPEN p_cur FOR    
      SELECT p.MISPAR_ISHI ,p.TAARICH , o.EMAIL  ,P.RIKUZ_PDF
        FROM OVDIM o,TB_RIKUZ_PDF p
        WHERE p.MISPAR_ISHI = O.MISPAR_ISHI
            AND p.BAKASHA_ID = p_bakasha_id
            AND o.EMAIL IS NOT NULL;
     
     /*   select distinct S.MISPAR_ISHI ,S.TAARICH ,o.EMAIL  ,c.SUG_CHISHUV
        from tb_chishuv_chodesh_ovdim s  ,   OVDIM o,tb_mispar_ishi_sug_chishuv c
        where S.MISPAR_ISHI = O.MISPAR_ISHI
            and S.BAKASHA_ID = p_bakasha_id
            and S.MISPAR_ISHI = c.MISPAR_ISHI
            and s.BAKASHA_ID = c.BAKASHA_ID
            and S.TAARICH = c.TAARICH;*/
        
  END Pro_get_Email_Ovdim_LeRikuzim;
  
  
  FUNCTION  pro_get_sug_chishuv(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                                  p_taarich IN DATE,
                                                  p_bakasha_id OUT TB_BAKASHOT.bakasha_id%TYPE ) RETURN NUMBER IS
  v_count_bakashot NUMBER;
  p_sug_chishuv NUMBER;
  p_tar DATE;
BEGIN
        p_tar:= TO_DATE('01/' || TO_CHAR(p_taarich,'mm/yyyy'));

        SELECT COUNT(b.BAKASHA_ID) INTO  v_count_bakashot
        FROM TB_BAKASHOT B
        WHERE B.Huavra_Lesachar=1
        AND EXISTS (SELECT bakasha_id FROM TB_CHISHUV_CHODESH_OVDIM  C
                                      WHERE    C.Bakasha_ID=B.Bakasha_ID
                                AND C.Mispar_Ishi =p_mispar_ishi
                                AND C.Taarich BETWEEN p_tar AND LAST_DAY(p_tar));--TO_CHAR(C.Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy'));

   IF v_count_bakashot=1 THEN
            p_sug_chishuv:=0;
   ELSIF v_count_bakashot>1 THEN
            p_sug_chishuv:=1;
     ELSE
            p_sug_chishuv:=2;
   END IF;
   
   RETURN p_sug_chishuv;
EXCEPTION
    WHEN NO_DATA_FOUND THEN
          p_sug_chishuv:=2;
          RETURN p_sug_chishuv;
     WHEN OTHERS THEN
            RAISE;
 
END  pro_get_sug_chishuv;


PROCEDURE pro_ins_misparishi_sug_chishuv(p_bakasha_id NUMBER,p_coll_chishuv_sug_sidur IN COLL_MISPAR_ISHI_SUG_CHISHUV) IS

BEGIN
     DELETE FROM TB_MISPAR_ISHI_SUG_CHISHUV WHERE  bakasha_id=p_bakasha_id;
      IF (p_coll_chishuv_sug_sidur IS NOT NULL) THEN
          FOR i IN 1..p_coll_chishuv_sug_sidur.COUNT LOOP
            INSERT INTO TB_MISPAR_ISHI_SUG_CHISHUV
                        (mispar_ishi,
                         bakasha_id,
                         taarich,
                         sug_chishuv
                        )
             VALUES (p_coll_chishuv_sug_sidur(i).mispar_ishi,
                     p_coll_chishuv_sug_sidur(i).bakasha_id,
                     p_coll_chishuv_sug_sidur(i).taarich,
                     p_coll_chishuv_sug_sidur(i).sug_chishuv
                      );
          END LOOP;
      END IF;
      
      EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END pro_ins_misparishi_sug_chishuv;


/****************************************************************************************/
/********************* shguim batch  ********************************/

PROCEDURE Prepare_yamei_avoda_meshek(p_date IN DATE, p_type IN NUMBER,p_num_process IN NUMBER, p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE) IS
  BEGIN
     INSERT INTO TB_MISPAR_ISHI_SHGUIM_BATCH(ROW_NUM,MISPAR_ISHI,TAARICH, NUM_pack,TYPE,bakasha_id)
      /* alef */
      SELECT  ROWNUM,MISPAR_ISHI, taarich,0,p_type ,p_bakasha_id
      FROM(           SELECT DISTINCT  ya.mispar_ishi,ya.taarich
                FROM TB_YAMEY_AVODA_OVDIM ya,OVDIM o
                WHERE  o.mispar_ishi=ya.mispar_ishi
                     AND  EXISTS  (SELECT mispar_ishi
                                          FROM TB_SIDURIM_OVDIM so
                                          WHERE so.mispar_ishi=ya.mispar_ishi
                                          AND so.taarich=ya.taarich AND so.meadken_acharon=-11
                                          AND so.taarich_idkun_acharon>NVL(ya.ritzat_shgiot_acharona,so.taarich_idkun_acharon-1)  )
                   --   AND ya.measher_o_mistayeg IS NOT NULL
               --       AND NVL(ya.status,-1)<>0
                  /* bet */
     UNION  SELECT DISTINCT   ya.mispar_ishi,ya.taarich
                 FROM TB_YAMEY_AVODA_OVDIM ya,OVDIM o
                 WHERE ya.taarich=TRUNC(p_date)
                     AND  o.mispar_ishi=ya.mispar_ishi
                     AND NOT EXISTS(SELECT 1 FROM TB_SIDURIM_OVDIM so WHERE so.mispar_ishi=ya.mispar_ishi
                                                                                                               AND so.taarich=ya.taarich AND so.meadken_acharon=-12)
                    /* AND EXISTS(SELECT 1 FROM TB_SIDURIM_OVDIM so WHERE so.mispar_ishi=ya.mispar_ishi
                                                                                                               AND so.taarich=ya.taarich AND so.meadken_acharon<>-12)*/
      UNION  SELECT  DISTINCT mispar_ishi, taarich
                  FROM
                   (SELECT DISTINCT t.mispar_ishi, t.kod_ishur,t.taarich, t.mispar_sidur, t.shat_hatchala,
                         t.shat_yetzia, t.mispar_knisa  ,t.rama,t.kod_status_ishur,t.taarich_idkun_acharon,
                          MAX(t.rama) KEEP (DENSE_RANK LAST ORDER BY t.rama )
                            OVER (PARTITION BY t.mispar_ishi,t.taarich,t.kod_ishur ) max_f4
                          FROM TB_ISHURIM t, TB_YAMEY_AVODA_OVDIM ya,OVDIM o
                          WHERE  t.taarich=ya.taarich
                          AND t.mispar_ishi=ya.mispar_ishi
                          AND  o.mispar_ishi=ya.mispar_ishi
                          AND t.taarich_idkun_acharon>NVL(ya.ritzat_shgiot_acharona,t.taarich_idkun_acharon-1) )
                    WHERE max_f4=rama
                         AND kod_status_ishur IN (1,2)
           /*heih*/    
       UNION SELECT DISTINCT  ya.mispar_ishi,ya.taarich
                 FROM TB_YAMEY_AVODA_OVDIM ya
                 WHERE YA.RITZAT_SHGIOT_ACHARONA=TO_DATE('01/01/0001','dd/mm/yyyy'));
      
       Pkg_Batch.pro_divide_packets(p_num_process,p_bakasha_id); 
       
       INSERT INTO TEST_MAATEFET(mispar_ishi, taarich, taarich_ritza, bakasha_id, sug_bakasha,comments)
         SELECT   M.MISPAR_ISHI,M.TAARICH,SYSDATE,p_bakasha_id,0,'shguim of sdrn'
         FROM TB_MISPAR_ISHI_SHGUIM_BATCH m
         WHERE m.bakasha_id = p_bakasha_id;
 END Prepare_yamei_avoda_meshek;
 
  PROCEDURE Prepare_yamei_avoda_shinui_hr( p_type IN NUMBER,p_num_process IN NUMBER, p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE) IS
  BEGIN 
      INSERT INTO TB_MISPAR_ISHI_SHGUIM_BATCH(ROW_NUM,MISPAR_ISHI,TAARICH, NUM_pack,TYPE,bakasha_id)
        SELECT ROWNUM, h.Mispar_Ishi, h.Taarich ,0,p_type,p_bakasha_id
        FROM OVDIM_IM_SHINUY_HR h, TB_YAMEY_AVODA_OVDIM y,
          CTB_ISUK i,   PIVOT_PIRTEY_OVDIM v_pirty_oved,OVDIM ov
        WHERE h.Mispar_Ishi=y.Mispar_Ishi
          AND h.Taarich=Y.Taarich
          AND NVL(y. RITZAT_SHGIOT_ACHARONA,h.Taarich_Idkun_HR-1)<h.Taarich_Idkun_HR
          AND (y.STATUS<>0 OR y.STATUS IS NULL)
       --   AND y. Measher_O_Mistayeg IS NOT NULL
            AND h.mispar_ishi            = ov.mispar_ishi
            AND v_pirty_oved.mispar_ishi(+) = h.mispar_ishi
        AND h.taarich  BETWEEN v_pirty_oved.me_tarich(+) AND v_pirty_oved.ad_tarich(+)
         AND i.kod_hevra = ov.kod_hevra
         AND  i.kod_isuk =v_pirty_oved.isuk;
         
          Pkg_Batch.pro_divide_packets(p_num_process,p_bakasha_id);  
          
            INSERT INTO TEST_MAATEFET(mispar_ishi, taarich, taarich_ritza, bakasha_id, sug_bakasha,comments)
         SELECT   M.MISPAR_ISHI,M.TAARICH,SYSDATE,p_bakasha_id,0,'shguim of hr'
         FROM TB_MISPAR_ISHI_SHGUIM_BATCH m
         WHERE m.bakasha_id = p_bakasha_id;
  END Prepare_yamei_avoda_shinui_hr;
  
 PROCEDURE pro_divide_packets( p_num_process IN  NUMBER, p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE ) IS
   num NUMBER;
BEGIN

 SELECT COUNT(*) INTO num FROM TB_MISPAR_ISHI_SHGUIM_BATCH WHERE bakasha_id =p_bakasha_id;
  
  UPDATE TB_MISPAR_ISHI_SHGUIM_BATCH s
  SET s.num_pack = (SELECT  TRUNC(row_num*p_num_process/num)+1 FROM dual)-- (select trunc(rownum*p_num_processe/num + 1) from TB_MISPAR_ISHI_CHISHUV);
   WHERE    s.bakasha_id =p_bakasha_id;
      
  UPDATE TB_MISPAR_ISHI_SHGUIM_BATCH s
  SET s.num_pack =p_num_process
  WHERE s.num_pack=(p_num_process+1)
  AND s.bakasha_id =p_bakasha_id;
  
  EXCEPTION
   WHEN OTHERS THEN
            RAISE;
END pro_divide_packets;

PROCEDURE pro_get_netunim_for_process( p_num_process IN  NUMBER , p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE, p_Cur OUT CurType) IS
  BEGIN
  
     OPEN p_Cur FOR    
        SELECT s.mispar_ishi,s.taarich,S.BAKASHA_ID,S.NUM_PACK
         FROM TB_MISPAR_ISHI_SHGUIM_BATCH s
         WHERE   s.num_pack =p_num_process 
               AND s.bakasha_id = p_bakasha_id;
  
  EXCEPTION
   WHEN OTHERS THEN
            RAISE;
END pro_get_netunim_for_process;

PROCEDURE pro_delete_tb_shguim_batch(    p_num_process IN  NUMBER ,  p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE) IS
BEGIN

        DELETE  TB_MISPAR_ISHI_SHGUIM_BATCH s
        WHERE   s.num_pack =p_num_process 
               AND s.bakasha_id = p_bakasha_id;
              
         EXCEPTION
   WHEN OTHERS THEN
            RAISE; 
END pro_delete_tb_shguim_batch;   

PROCEDURE Prepare_premiot_shguim_batch(p_type IN NUMBER,p_num_process IN NUMBER, p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE) IS
CURSOR p_cur IS
      SELECT Mispar_Ishi, chodesh
           FROM OVDIM_LECHISHUV_PREMYOT lp  
            WHERE LP.BAKASHA_ID IS NULL
      --      and LP.MISPAR_ISHI = 75148
            ORDER BY chodesh;
 v_rec  p_cur%ROWTYPE;  
 tar_me DATE;    
 tar_ad DATE;  
 row_num NUMBER;   
 BEGIN
            row_num:=1;
             FOR v_rec  IN   p_cur
              LOOP
                      BEGIN
                           tar_me:=v_rec.chodesh;
                           tar_ad:=ADD_MONTHS(tar_me,1)-1;
                           IF (tar_ad>SYSDATE) THEN
                                tar_ad:= SYSDATE;
                           END IF;
                            
                           WHILE TRUNC(tar_me)<=TRUNC(tar_ad)
                            LOOP
                                 INSERT INTO TB_MISPAR_ISHI_SHGUIM_BATCH(ROW_NUM,MISPAR_ISHI,TAARICH, NUM_pack,TYPE,bakasha_id)
                                 VALUES (row_num,v_rec.MISPAR_ISHI,tar_me,0,p_type,p_bakasha_id);
                                 row_num:=row_num+1;
                                 tar_me:= tar_me+1;
                            END LOOP;
                      END;
              END LOOP;
   /*    insert into TB_MISPAR_ISHI_SHGUIM_BATCH(ROW_NUM,MISPAR_ISHI,TAARICH, NUM_pack,TYPE,bakasha_id)
        SELECT rownum, Mispar_Ishi, chodesh ,0,p_type,p_bakasha_id
           FROM OVDIM_LECHISHUV_PREMYOT lp  
            WHERE LP.BAKASHA_ID IS NULL
            ORDER BY chodesh;*/

         Pkg_Batch.pro_divide_packets(p_num_process,p_bakasha_id);

      INSERT INTO TEST_MAATEFET(mispar_ishi, taarich, taarich_ritza, bakasha_id, sug_bakasha,comments)
             SELECT   M.MISPAR_ISHI,M.TAARICH,SYSDATE,p_bakasha_id,0,'shguim of premiot'
             FROM TB_MISPAR_ISHI_SHGUIM_BATCH m
             WHERE m.bakasha_id = p_bakasha_id;
     EXCEPTION
         WHEN OTHERS THEN
              RAISE;   
 END Prepare_premiot_shguim_batch;   
 
 
PROCEDURE Pro_Save_Rikuz_Pdf(p_BakashatId TB_RIKUZ_PDF.bakasha_id%TYPE,p_coll_rikuz_pdf IN COLL_RIKUZ_PDF) IS
BEGIN  
     DELETE FROM TB_RIKUZ_PDF p
      WHERE P.BAKASHA_ID = p_BakashatId;

      IF (p_coll_rikuz_pdf IS NOT NULL) THEN
          FOR i IN 1..p_coll_rikuz_pdf.COUNT LOOP
          IF (p_coll_rikuz_pdf(i).rikuz_pdf IS NOT NULL) THEN
            INSERT INTO TB_RIKUZ_PDF
                        (mispar_ishi,
                         bakasha_id,
                         taarich,
                         sug_chishuv,
                         rikuz_pdf
                        )
             VALUES (p_coll_rikuz_pdf(i).mispar_ishi,
                     p_coll_rikuz_pdf(i).bakasha_id,
                     p_coll_rikuz_pdf(i).taarich,
                    DECODE(p_coll_rikuz_pdf(i).sug_chishuv,-1,NULL,p_coll_rikuz_pdf(i).sug_chishuv),
                    p_coll_rikuz_pdf(i).rikuz_pdf
                      );
                      END IF;
          END LOOP;
      END IF;
      
      EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END Pro_Save_Rikuz_Pdf;

PROCEDURE Pro_Get_Rikuz_Pdf(p_mispar_ishi IN NUMBER,p_taarich IN DATE,p_BakashatId IN NUMBER , p_cur OUT CurType) IS --p_rikuz OUT BLOB) IS

BEGIN  
 OPEN p_cur FOR    
    SELECT * -- into p_rikuz 
    FROM TB_RIKUZ_PDF r
    WHERE r.mispar_ishi = p_mispar_ishi
        AND r.bakasha_id = p_BakashatId
      AND TRUNC(r.taarich) = TRUNC(p_taarich);

      EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END Pro_Get_Rikuz_Pdf;




FUNCTION pro_check_view_empty(p_TableName VARCHAR2) RETURN NUMBER AS
num NUMBER ;
BEGIN

INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'p_TableName:',p_TableName);

  IF (p_TableName = 'NEW_MATZAV_OVDIM') THEN
      SELECT COUNT(*) INTO num FROM NEW_MATZAV_OVDIM;
   ELSE IF (p_TableName = 'NEW_PIRTEY_OVDIM') THEN
              SELECT COUNT(*) INTO num FROM NEW_PIRTEY_OVDIM;
         ELSE IF (p_TableName = 'NEW_MEAFYENIM_OVDIM') THEN
                    SELECT COUNT(*) INTO num FROM NEW_MEAFYENIM_OVDIM;
                ELSE IF (p_TableName = 'NEW_BREROT_MECHDAL_MEAFYENIM') THEN
                      SELECT COUNT(*) INTO num FROM NEW_BREROT_MECHDAL_MEAFYENIM;
                END IF;
         END IF;
   END IF;
  END IF;     
              
  RETURN num  ;

   /*    EXCEPTION
            WHEN NO_DATA_FOUND THEN
                      num := 0 ;
                          RETURN num  ;
            WHEN OTHERS THEN
                          RAISE;*/
END pro_check_view_empty;

PROCEDURE DeleteBakashotYeziratRikuzim(p_BakashatId TB_BAKASHOT.bakasha_id%TYPE) IS
    CURSOR v_cur(p_BakashatId TB_BAKASHOT.bakasha_id%TYPE) IS
       SELECT P.BAKASHA_ID     
       FROM  TB_BAKASHOT t , TB_BAKASHOT_PARAMS p
       WHERE T.BAKASHA_ID = P.BAKASHA_ID
              AND  T.SUG_BAKASHA =13
              AND P.PARAM_ID =1
              AND P.ERECH = TO_CHAR(p_BakashatId);
  BEGIN
      FOR   v_cur_rec IN  v_cur(p_BakashatId)
   LOOP
            DELETE FROM TB_BAKASHOT_PARAMS
            WHERE bakasha_id=v_cur_rec.bakasha_id;
            
            DELETE FROM TB_BAKASHOT
            WHERE bakasha_id=v_cur_rec.bakasha_id;
  END LOOP;
EXCEPTION
     WHEN NO_DATA_FOUND THEN
        NULL;
   WHEN OTHERS THEN
            RAISE;

END DeleteBakashotYeziratRikuzim;


PROCEDURE pro_Get_Makatim_LeTkinut(p_date IN DATE, p_cur OUT CurType) IS
BEGIN

    OPEN p_Cur FOR
        SELECT DISTINCT P.MAKAT_NESIA,P.TAARICH
        FROM TB_PEILUT_OVDIM p
        WHERE  P.TAARICH BETWEEN p_date AND LAST_DAY(p_date) 
      --  P.TAARICH =  to_date('01/04/2011','dd/mm/yyyy') 
        -- and  substr(P.MAKAT_NESIA,1,1)='1' 
        AND P.MAKAT_NESIA>0
        AND NOT((SUBSTR(P.MAKAT_NESIA,1,1)='7' OR SUBSTR(P.MAKAT_NESIA,1,1)='5' ) AND LENGTH(P.MAKAT_NESIA)=8);
        
        
        -- P.TAARICH between p_date and last_day(p_date);
        
END pro_Get_Makatim_LeTkinut;


PROCEDURE pro_retrospect_yamey_avoda  IS
 
-- CURSOR Yamim_r IS
--  SELECT DISTINCT taarich dt 
--FROM  TB_YAMEY_AVODA_OVDIM 
--WHERE    taarich BETWEEN GREATEST(TO_DATE('01/07/2012','dd/mm/yyyy'),SYSDATE-(SELECT erech_param FROM TB_PARAMETRIM WHERE kod_param=262)) AND SYSDATE-1;

--FOR  Yamim_r_rec IN  Yamim_r LOOP
--   INSERT INTO TB_YAMEY_AVODA_OVDIM
--   (mispar_ishi,taarich,lina,measher_o_mistayeg ,taarich_idkun_acharon,meadken_acharon)
--   SELECT mispar_ishi,Yamim_r_rec.dt,0,1,SYSDATE,-11
--  FROM  NEW_MATZAV_OVDIM  o
--   WHERE   kod_matzav IN ('01','03','04','05','06','07','08')
--   AND Yamim_r_rec.dt BETWEEN taarich_hatchala AND NVL(taarich_siyum,Yamim_r_rec.dt+1)
--   AND NOT EXISTS (SELECT * FROM  TB_YAMEY_AVODA_OVDIM yao
--  WHERE yao.taarich=Yamim_r_rec.dt
--  AND yao.MISPAR_ISHI=o.mispar_ishi);
 
 strt_dt date;
 Miss_oved_Dt DATE;  
 
 CURSOR Yamim_r  IS
  select ndt ,mispar_ishi
from (
SELECT  trunc(strt_dt + rownum -1) ndt
   from all_objects 
  where rownum <= (sysdate-1)-strt_dt+1),
  new_matzav_ovdim o
 WHERE   kod_matzav IN ('01','03','04','05','06','07','08')
   AND ndt BETWEEN taarich_hatchala AND NVL(taarich_siyum,ndt+1)
   AND NOT EXISTS (SELECT * FROM  TB_YAMEY_AVODA_OVDIM yao
  WHERE yao.taarich=ndt
  AND yao.MISPAR_ISHI=o.mispar_ishi)
  order by mispar_ishi,ndt;

 
   BEGIN

select  trunc(GREATEST(TO_DATE('01/07/2012','dd/mm/yyyy'),SYSDATE-(SELECT erech_param FROM TB_PARAMETRIM WHERE kod_param=262)))
into strt_dt 
from dual;

FOR  Yamim_r_rec IN  Yamim_r LOOP
BEGIN

 select  greatest( taarich_hatchala, to_date('01/07/2012','dd/mm/yyyy'),trunc(sysdate)-365)
  into Miss_oved_Dt
  from     NEW_MATZAV_OVDIM  o
   WHERE   kod_matzav IN ('01','03','04','05','06','07','08')
   and Yamim_r_rec.ndt  between taarich_hatchala and taarich_siyum
 and   mispar_ishi=Yamim_r_rec.mispar_ishi;
 
    INSERT INTO TB_YAMEY_AVODA_OVDIM
   (mispar_ishi,taarich,lina,measher_o_mistayeg ,taarich_idkun_acharon,meadken_acharon)
   SELECT Yamim_r_rec.mispar_ishi,kkdt,0,1,SYSDATE,-11
from (
SELECT trunc( Miss_oved_Dt + rownum -1) kkdt
   from all_objects 
  where rownum <= (sysdate-1)-Miss_oved_Dt+1),
  new_matzav_ovdim o
 WHERE   kod_matzav IN ('01','03','04','05','06','07','08')
 and mispar_ishi=Yamim_r_rec.mispar_ishi
   AND kkdt BETWEEN taarich_hatchala AND NVL(taarich_siyum,kkdt+1)
   AND NOT EXISTS (SELECT * FROM  TB_YAMEY_AVODA_OVDIM yao
  WHERE yao.taarich=kkdt
  AND yao.MISPAR_ISHI=o.mispar_ishi);
  commit;

EXCEPTION
   WHEN OTHERS THEN
     INSERT INTO TB_LOG_TAHALICH
    VALUES (7,12,1,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Yamim_r_rec.ndt)||' '||to_char(Yamim_r_rec.mispar_ishi) ||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
END;

END LOOP;
COMMIT;
       
END pro_retrospect_yamey_avoda;


END Pkg_Batch;
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

PROCEDURE pro_prepare_netunim_lechishuv(p_tar_me IN DATE,p_tar_ad IN DATE,
                                    p_maamad IN NUMBER, p_ritza_gorefet IN NUMBER, p_num_processe IN  NUMBER) IS
              v_me_taarich DATE;
              v_ad_taarich DATE;

  BEGIN
  
    EXECUTE IMMEDIATE 'truncate table TB_MISPAR_ISHI_CHISHUV' ; 
    EXECUTE IMMEDIATE 'truncate table TB_CATALOG_CHISHUV' ; 
    EXECUTE IMMEDIATE 'truncate table tb_yamim_Lechishuv' ; 
      v_me_taarich:=p_tar_me;
      v_ad_taarich:=p_tar_ad;
      
  v_me_taarich:=to_date('01/03/2012','dd/mm/yyyy');
 v_ad_taarich:=to_date('30/06/2012','dd/mm/yyyy');
      
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
         -- AND T.NUM_PACK=5--
         --    AND t.mispar_ishi=o.mispar_ishi--
       --    AND  o.taarich BETWEEN T.TAARICH AND LAST_DAY( T.TAARICH)  --
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
          --   TB_MISPAR_ISHI_CHISHUV_BAK t, --
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
   --AND T.NUM_PACK=5--
   --AND t.mispar_ishi=o.mispar_ishi--
    --AND  p.taarich BETWEEN T.TAARICH AND LAST_DAY( T.TAARICH)  --
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
  pro_InsertYamimLeTavla(v_me_taarich,v_ad_taarich,p_num_processe );
EXCEPTION
   WHEN OTHERS THEN
            RAISE;

END  pro_prepare_netunim_lechishuv;

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
                       V_SIDURIM.matala_klalit_lelo_rechev,nvl(V_SIDURIM.zakaut_legmul_chisachon,0) zakaut_legmul_chisachon
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

PROCEDURE pro_InsertYamimLeTavla(p_tar_me IN DATE,p_tar_ad IN DATE , p_num_process IN NUMBER) IS
BEGIN
    INSERT INTO TB_YAMIM_LECHISHUV
        SELECT DISTINCT   Y.MISPAR_ISHI, Y.TAARICH, Y.STATUS
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
                         --   and s.mispar_ishi=75933
              order by  s.mispar_ishi,m.kod_meafyen,m.ME_TAARICH ) h
               order by  h.mispar_ishi,h.kod_meafyen,h.ME_TAARICH      )
   
             select h.MISPAR_ISHI, to_char(h.kod_meafyen) kod_meafyen, 
                      h.Erech_Mechdal_partany,
                      h.ME_TAARICH,h.AD_TAARICH,
                      h.Erech_ishi,
                      h.value_erech_ishi,
                      h.source_meafyen
             from(
                    select s.MISPAR_ISHI, s.kod_meafyen, (s.AD_TAARICH+1) ME_TAARICH, (next_hour_me-1) AD_TAARICH, 
                    '' Erech_Mechdal_partany,
                     m.erech ||  ' (ב.מ. מערכת) '  Erech_ishi,
                       to_char(m.erech) value_erech_ishi,
                            '1' source_meafyen
                    from  tbIshi s, brerot_mechdal_meafyenim m
                    where (s.AD_TAARICH+1)<next_hour_me
                    and s.kod_meafyen = m.kod_meafyen
                
                union 

                    select s.MISPAR_ISHI, s.kod_meafyen, (s.prev_hour_ad+1) ME_TAARICH, (s.ME_TAARICH -1) AD_TAARICH,
                    '' Erech_Mechdal_partany,
                     m.erech ||  ' (ב.מ. מערכת) '  Erech_ishi,
                      to_char(m.erech) value_erech_ishi,
                      '1' source_meafyen
                    from  tbIshi s, brerot_mechdal_meafyenim m
                    where (s.prev_hour_ad+1)<s.ME_TAARICH
                    and s.kod_meafyen = m.kod_meafyen

                union

                    select s.MISPAR_ISHI, s.kod_meafyen,s.ME_TAARICH,s.AD_TAARICH,
                    s.Erech_Mechdal_partany,
                    s.Erech_ishi,
                    s.value_erech_ishi,
                     '2' source_meafyen
                    from  tbIshi s
                   -- where  s.mispar_ishi=75933

                union

                    SELECT     OV.mispar_ishi, to_char(df.KOD_MEAFYEN) KOD_MEAFYEN,  ov.taarich me_taarich ,last_day(ov.taarich) ad_taarich,
                                    '' Erech_Mechdal_partany,
                                    df.erech ||  ' (ב.מ. מערכת) '  Erech_ishi,
                                    to_char(df.erech) value_erech_ishi,
                                     '1' source_meafyen
                    FROM   TB_MISPAR_ISHI_CHISHUV ov, BREROT_MECHDAL_MEAFYENIM df
                    where num_pack= p_num_process
                       -- and ov.mispar_ishi=75933
                        and df.kod_meafyen not in (select   sh.kod_meafyen 
                                                                from  tbIshi sh
                                                                where sh.mispar_ishi =  ov.mispar_ishi
                                                                and to_char(sh.ME_TAARICH,'mm/yyyy') =to_char( OV.taarich,'mm/yyyy') )
                                                          /*    and ((sh.me_taarich<=OV.taarich     and sh.ad_taarich<= last_day(OV.taarich)  and sh.ad_taarich>=  OV.taarich   ) or
                                                                       (sh.me_taarich>=  OV.taarich  and sh.ad_taarich<= last_day(OV.taarich)  ) or
                                                                        (sh.me_taarich>=OV.taarich   and sh.ad_taarich>=  last_day(OV.taarich)  and sh.me_taarich<=  last_day(OV.taarich)   ) or
                                                                      (sh.me_taarich<= OV.taarich  and sh.ad_taarich>= last_day(OV.taarich)  ))   ) */                                                                            
                 ) h
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
         SELECT  ROWNUM,MISPAR_ISHI,  chodesh ,1
            FROM OVDIM_LECHISHUV_PREMYOT lp  
            WHERE LP.BAKASHA_ID IS NULL
            ORDER BY chodesh;
		
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

    --  p_tarich_me:=to_date('01/06/2012','dd/mm/yyyy');
    -- p_tarich_ad:=to_date('30/06/2012','dd/mm/yyyy');
     
      Pkg_Calculation.pro_get_ovdim(p_Cur_Ovdim,p_num_process); 
      
      Pkg_Calculation.pro_get_michsa_yomit(p_tarich_me , p_tarich_ad ,p_Cur_Michsa_Yomit);  
            
     Pkg_Calculation.pro_get_sidurim_meyuch_rechiv(p_tarich_me , p_tarich_ad , p_Cur_SidurMeyuchadRechiv );                      
     
     Pkg_Calculation.pro_get_sug_sidur_rechiv( p_tarich_me , p_tarich_ad,p_Cur_Sug_Sidur_Rechiv);
                                       
     Pkg_Calculation.pro_get_premyot_view(p_Cur_Premiot_View,p_num_process);
     
     Pkg_Calculation.pro_get_premia_yadanit(p_Cur_Premiot_Yadaniot,p_num_process); 
     
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


CREATE OR REPLACE PACKAGE BODY          Pkg_Ovdim AS
/******************************************************************************
   NAME:       PKG_OVDIM
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

PROCEDURE pro_get_error_ovdim(p_kod_hevra IN CTB_HEVRA.kod_hevra%TYPE,p_kod_ezor IN CTB_EZOR.kod_ezor%TYPE,
                              p_kod_snif IN CTB_SNIF_AV.kod_snif_av%TYPE, p_kod_maamad IN CTB_MAAMAD.kod_maamad_hr%TYPE,
                              p_from_date IN DATE, p_to_date IN DATE,
                              p_cur OUT CurType) IS
BEGIN

     OPEN p_cur FOR
	 	  SELECT DISTINCT  v.mispar_ishi , o.shem_mish || ' ' ||  o.shem_prat full_name ,TO_CHAR(v.mispar_ishi ) mispar_ishi_char,
					    v.SNIF_AV ,v.ME_TARICH , v.AD_TARICH ,ez.teur_ezor, s.teur_snif_av,  ma.teur_maamad_hr,
					   DECODE(l.status_key ,1,'+','') status_key,
			           DECODE(l.status_tipul_key ,2,'+','') status_tipul_key
          FROM  PIVOT_PIRTEY_OVDIM v,
	   	  	         (SELECT po.mispar_ishi,MAX(po.ME_TARICH) me_taarich
					   FROM PIVOT_PIRTEY_OVDIM PO
					   WHERE po.isuk IS NOT NULL                        
						     AND (TRUNC(p_from_date) BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
					 		  OR    TRUNC(p_to_date) BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
					  		  OR   po.ME_TARICH>= TRUNC(p_from_date) AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=  TRUNC(p_to_date) )
					  GROUP BY po.mispar_ishi) p,
					 (SELECT DISTINCT NVL(x.mispar_ishi, y.mispar_ishi  ) mispar_ishi ,x.status_key, y.status_tipul_key
					 FROM
							 (SELECT  mispar_ishi, 1 status_key  FROM TB_YAMEY_AVODA_OVDIM
							   WHERE taarich BETWEEN  TRUNC(p_from_date)  AND  TRUNC(p_to_date)
							       AND status=0
								--   AND measher_o_mistayeg IS NOT NULL
								  --  AND measher_o_mistayeg <>2
							  GROUP BY mispar_ishi) x
						 FULL JOIN
						 ( SELECT mispar_ishi ,2 status_tipul_key  FROM TB_ISHURIM
							   WHERE taarich BETWEEN   TRUNC(p_from_date) AND TRUNC(p_to_date)
							        AND NVL(KOD_STATUS_ISHUR ,0) =0
							 GROUP BY mispar_ishi) y

							ON x.mispar_ishi=y.mispar_ishi
							   )l,
	                OVDIM o,
	                CTB_EZOR ez,
	                CTB_SNIF_AV s,
	                CTB_MAAMAD ma
       WHERE  v.mispar_ishi = p.mispar_ishi
	       AND  v.me_tarich = p.me_taarich
	       AND  v.mispar_ishi = o.mispar_ishi
		   AND  v.mispar_ishi = l.mispar_ishi
            and o.kod_hevra = s.kod_hevra
		  -- AND  ((p_kod_hevra IS NULL) OR (o.kod_hevra = p_kod_hevra))
           AND  ((p_kod_snif IS NULL )OR (v.SNIF_AV =p_kod_snif) OR (p_kod_snif=101 and S.TEUR_SNIF_AV like '%מוסך%' ) 
                                                                                             OR  (p_kod_snif=100 and S.TEUR_SNIF_AV like '%אגף%'))
           AND  v.SNIF_AV = s.kod_snif_av(+)
	       AND  (v.snif_av IS NULL OR (p_kod_hevra IS NULL) OR o.kod_hevra = s.kod_hevra)
	       AND  (  ((p_kod_maamad  IS NULL) OR (v.MAAMAD =p_kod_maamad ))
		   				OR ((p_kod_maamad IN (1,2)) AND (v.MAAMAD LIKE p_kod_maamad ||'%' ))		)
	       AND  v.MAAMAD = ma.kod_maamad_hr(+)
	       AND  (v.maamad IS NULL OR (p_kod_hevra IS NULL) OR ma.kod_hevra  = o.kod_hevra)
	       AND  v.ezor = ez.kod_ezor (+)
	       AND  (v.ezor IS NULL OR (p_kod_hevra IS NULL)  OR  o.kod_hevra = ez.kod_hevra )
	       AND  ((p_kod_ezor IS NULL) OR (v.ezor =p_kod_ezor))
	 ORDER BY mispar_ishi;

	 EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END pro_get_error_ovdim;

PROCEDURE pro_get_oved_full_name(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE, p_full_name OUT OVDIM.shem_mish%TYPE)
IS
    BEGIN
        SELECT o.SHEM_MISH || ' ' || o.SHEM_PRAT FullName INTO p_full_name
        FROM OVDIM o
        WHERE o.mispar_ishi=p_mispar_ishi;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
            p_full_name:='';
        WHEN OTHERS THEN
            RAISE;
END  pro_get_oved_full_name;

PROCEDURE pro_get_ovedim_mispar_ishi(p_prefix IN VARCHAR2, p_misparim_range IN VARCHAR2, p_cur OUT CurType)
IS
    BEGIN
       --open p_cur for  select 75290 mispar_ishi ,'75290 ( כהן שרה בדיקה)' FullName from dual ;
       IF ((LENGTH(p_misparim_range)>0) AND p_misparim_range IS NOT NULL ) THEN
            OPEN p_cur FOR
            SELECT  o.mispar_ishi ,o.mispar_ishi || ' (' || o.shem_mish || ' ' || o.shem_prat   || ')'  FullName
            FROM OVDIM o
            WHERE o.mispar_ishi IN
                 (SELECT x FROM TABLE(CAST(Convert_String_To_Table(p_misparim_range ,  ',') AS mytabtype)))
                 AND o.mispar_ishi LIKE p_prefix;
        ELSE
            OPEN p_cur FOR
            SELECT o.mispar_ishi ,o.mispar_ishi || ' (' || o.shem_mish || ' ' || o.shem_prat   || ')'  FullName
            FROM OVDIM o
            WHERE o.mispar_ishi LIKE p_prefix
            ORDER BY o.mispar_ishi;
        END IF;
   EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END  pro_get_ovedim_mispar_ishi;

PROCEDURE pro_get_Active_Workers(p_prefix IN VARCHAR2, p_FromDate IN DATE,p_ToDate IN DATE , p_cur OUT CurType) IS
BEGIN
OPEN p_cur FOR
SELECT DISTINCT o.mispar_ishi ,o.mispar_ishi || ' (' || o.shem_mish || ' ' || o.shem_prat   || ')'  FullName
            FROM OVDIM o INNER JOIN MATZAV_OVDIM Mo ON mo.mispar_ishi= o.mispar_ishi
WHERE mo.Kod_matzav IN ('01','03','04','05','06','08','09' )
AND
(
    (p_ToDate BETWEEN mo.Taarich_hatchala AND mo.Taarich_siyum )
    OR
    (p_FromDate BETWEEN mo.Taarich_hatchala AND mo.Taarich_siyum )
    OR
    (( mo.Taarich_hatchala <= p_FromDate )    AND    ( p_ToDate <= mo.Taarich_siyum))
)
AND mo.mispar_ishi LIKE  p_prefix || '%'
ORDER BY TO_CHAR(o.mispar_ishi) ASC ;

   EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END  pro_get_Active_Workers;


PROCEDURE pro_get_oved_errors_cards(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
		  										  	 						 		  p_from IN TB_YAMEY_AVODA_OVDIM.taarich%TYPE,
																					  p_to  IN TB_YAMEY_AVODA_OVDIM.taarich%TYPE,
																					  p_cur OUT CurType)
IS
    BEGIN
         OPEN p_cur FOR
        SELECT DISTINCT aa.mispar_ishi, aa.Taarich, aa.Status, aa.measher_o_mistayeg,aa.status_tipul,
                TO_CHAR(aa.taarich,'d') DAY,
                DECODE(aa.status,0,'+','') status_key,aa.status status_card,
                DECODE(aa.measher_o_mistayeg,0,DECODE(aa.status_tipul,2,'','+'),'') measher_o_mistayeg_key,
				k.KOD_STATUS_ISHUR,
				DECODE(k.KOD_STATUS_ISHUR,0,'+','') status_tipul_key  ,
                TO_CHAR(aa.Taarich,'dd/mm/yyyy') || ' ' || (CASE TO_CHAR(aa.taarich,'d') WHEN '1' THEN 'א'
                                             WHEN '2' THEN 'ב'
                                             WHEN '3' THEN 'ג'
                                             WHEN '4' THEN 'ד'
                                             WHEN '5' THEN 'ה'
                                             WHEN '6' THEN 'ו'
                                             WHEN '7' THEN 'ש' END)  || ' ' || c.teur_yom teur_yom

         FROM TB_YAMEY_AVODA_OVDIM aa ,TB_ISHURIM k
		        , TB_YAMIM_MEYUCHADIM b, CTB_SUGEY_YAMIM_MEYUCHADIM c
         WHERE aa.mispar_ishi = p_mispar_ishi --25225 -- 7951 --25225 --12147  --
		      AND aa.taarich BETWEEN  p_from AND  p_to
		 	   AND aa.mispar_ishi  = k.MISPAR_ISHI(+)
              AND aa.taarich = b.taarich(+)
			   AND aa.taarich = k.taarich(+)
               AND b.sug_yom=c.sug_yom(+)
			   AND (aa.status=0 OR k.KOD_STATUS_ISHUR=0);
    EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END pro_get_oved_errors_cards;
/*
PROCEDURE pro_get_oved_errors_cards(p_mispar_ishi in ovdim.mispar_ishi%type, p_cur out CurType)
IS
    BEGIN
         open p_cur for
         select aa.mispar_ishi, aa.Taarich, aa.Status, aa.measher_o_mistayeg,aa.status_tipul,
                to_char(aa.taarich,'d') Day,
                decode(aa.status,0,'+','') status_key,
                decode(aa.measher_o_mistayeg,0,decode(aa.status_tipul,2,'','+'),'') measher_o_mistayeg_key,
                decode(aa.status_Tipul ,1,'+','') status_Tipul_key,
                to_char(aa.Taarich,'dd/mm/yyyy') || ' ' || (case to_char(aa.taarich,'d') when '1' then 'א'
                                             when '2' then 'ב'
                                             when '3' then 'ג'
                                             when '4' then 'ד'
                                             when '5' then 'ה'
                                             when '6' then 'ו'
                                             when '7' then 'ש' end)  || ' ' || c.teur_yom teur_yom

         from tb_yamey_avoda_ovdim aa,tb_yamim_meyuchadim b, ctb_sugey_yamim_meyuchadim c
         where aa.mispar_ishi =p_mispar_ishi
               and aa.taarich = b.taarich(+)
               and b.sug_yom=c.sug_yom(+)
               and (aa.status = 0 or aa.status_tipul=1 or (aa.measher_o_mistayeg=0 and (aa.status_tipul<>2 or aa.status_tipul is null)));
    EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END pro_get_oved_errors_cards;
*/
PROCEDURE pro_get_oved_details(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,p_from_taarich IN DATE,p_cur OUT CurType )
IS
BEGIN
DBMS_APPLICATION_INFO.SET_MODULE('pkg_ovdim.pro_get_oved_details','get pirtey oved letaarich');
	

        OPEN p_Cur FOR
        SELECT o.shem_mish || ' ' || o.shem_prat full_name,
               s.teur_snif_av,s.kod_snif_av, ma.teur_maamad_hr,
               ma.kod_maamad_hr, ez.kod_ezor, ez.teur_ezor,
               ck.teur_isuk, ch.teur_hevra
        FROM  OVDIM o,CTB_SNIF_AV s,CTB_MAAMAD ma,CTB_EZOR ez, PIVOT_PIRTEY_OVDIM  po,
              CTB_ISUK ck, CTB_HEVRA ch
        WHERE o.mispar_ishi= p_mispar_ishi AND
              po.mispar_ishi= o.mispar_ishi AND
              s.kod_hevra = ez.kod_hevra AND
              s.kod_snif_av = po.snif_av AND
              ma.kod_maamad_hr = po.maamad AND
              ez.kod_ezor = po.ezor AND
              ck.KOD_HEVRA = o.KOD_HEVRA AND
              ck.KOD_ISUK = po.ISUK AND
              ch.KOD_HEVRA =o.KOD_HEVRA AND
              p_from_taarich BETWEEN po.me_tarich AND po.ad_tarich;

      /*  select o.shem_mish || ' ' || o.shem_prat full_name,
           s.teur_snif_av,s.kod_snif_av, ma.teur_maamad_hr,
           ma.kod_maamad_hr, ez.kod_ezor, ez.teur_ezor
        from  ovdim o,ctb_snif_av s,ctb_maamad ma,ctb_ezor ez
        where o.mispar_ishi= p_mispar_ishi and
              s.kod_hevra = ez.kod_hevra and
              s.kod_snif_av =(select kod_snif
                              from  viw_get_oved_maamad_snif vS
                              where vS.mispar_ishi=o.mispar_ishi and
                                    vS.ad_taarich = (select max(v2.ad_taarich)
                                                     from  viw_get_oved_maamad_snif  v2
                                                     where v2.mispar_ishi=vS.mispar_ishi
                                                           and v2.me_TAARICH<=p_last_taarich))

               and ma.kod_maamad_hr =(select kod_maamad
                                      from  viw_get_oved_maamad_snif vM
                                      where vM.mispar_ishi=o.mispar_ishi
                                           and vM.ad_taarich = (select max(v3.ad_taarich)
                                                                from  viw_get_oved_maamad_snif v3
                                                                where v3.mispar_ishi=vM.mispar_ishi
                                                                      and v3.me_TAARICH <= p_last_taarich))

            and ez.kod_ezor = s.ezor;*/

	 EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END pro_get_oved_details;

PROCEDURE pro_get_ovedim_by_name(p_prefix IN VARCHAR2, p_misparim_range IN VARCHAR2, p_cur OUT CurType)
IS
BEGIN
 IF ((LENGTH(p_misparim_range)>0) AND p_misparim_range IS NOT NULL ) THEN
    OPEN p_cur FOR
    SELECT o.shem_mish || ' ' ||  o.shem_prat  || ' (' || o.mispar_ishi || ')' Oved_Name
    FROM OVDIM o
    WHERE o.mispar_ishi IN
         (SELECT x FROM TABLE(CAST(Convert_String_To_Table(p_misparim_range ,  ',') AS mytabtype)))
         AND o.shem_mish || ' ' ||  o.shem_prat LIKE p_prefix
    ORDER BY o.shem_mish,o.shem_prat;
  ELSE
    OPEN p_cur FOR
    SELECT o.shem_mish || ' ' ||  o.shem_prat  || ' (' || o.mispar_ishi || ')' Oved_Name
    FROM OVDIM o
    WHERE o.shem_mish || ' ' ||  o.shem_prat LIKE p_prefix
    ORDER BY o.shem_mish,o.shem_prat;
  END IF;
END pro_get_ovedim_by_name;

PROCEDURE pro_get_oved_mispar_ishi(p_name IN VARCHAR2, p_mispar_ishi OUT OVDIM.mispar_ishi%TYPE)
IS
BEGIN
    SELECT o.mispar_ishi INTO p_mispar_ishi
    FROM OVDIM o
    WHERE
          o.shem_mish || ' ' || o.shem_prat = p_name
		  AND ROWNUM=1;
EXCEPTION
        WHEN NO_DATA_FOUND THEN
            p_mispar_ishi:='';
        WHEN OTHERS THEN
            RAISE;

END  pro_get_oved_mispar_ishi;

   /*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        12/05/2009     sari      1. מביא פרטי עובד
*/
 PROCEDURE pro_get_pirtey_oved(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE, p_taarich IN DATE,
 		   									 							p_cur OUT CurType) IS
	v_date_from DATE;
 --   p_taarich date;
BEGIN
--p_taarich:= to_date('01/08/2010','dd/mm/yyyy');
	 v_date_from:=TO_DATE('01/' || TO_CHAR(p_taarich,'mm/yyyy'),'dd/mm/yyyy');
             
   -- INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'p_taarich:',p_taarich);
    OPEN p_cur FOR
	      SELECT o.mispar_ishi,o.shem_MISH,o.shem_prat, TO_CHAR(p_taarich,'mm/yyyy') month_year,o. Kod_Hevra,
          substr(p.maamad,2,2) kod_maamad,p.DIRUG,p.darga,substr(p.maamad,1,1) maamad_rashi,
        (SELECT teur_maamad_hr FROM CTB_MAAMAD WHERE kod_maamad_hr=p.maamad AND kod_hevra=o. Kod_Hevra) maamad,
		 (SELECT teur_ezor FROM CTB_EZOR WHERE kod_ezor=p.ezor AND kod_hevra=o. Kod_Hevra) ezor,
		  (SELECT teur_snif_av FROM CTB_SNIF_AV WHERE kod_snif_av=p.snif_av AND kod_hevra=o. Kod_Hevra) snif_av,
		    (SELECT teur_snif_av FROM CTB_SNIF_AV WHERE kod_snif_av=p.tachanat_sachar AND kod_hevra=o. Kod_Hevra) tachanat_sachar,
			  (SELECT teur_isuk FROM CTB_ISUK WHERE kod_isuk=p.isuk AND kod_hevra=o. Kod_Hevra) isuk,
			    (SELECT teur_kod_gil FROM  CTB_KOD_GIL  WHERE kod_gil_hr=p.gil) gil, p.gil kod_gil,p.tchilat_avoda, p.tachanat_sachar kod_tachana,
               pkg_ovdim.pro_get_meafyen_leoved_by_ez(p_mispar_ishi , 83 ,v_date_from) meafyen83,
                pkg_ovdim.pro_get_meafyen_leoved_by_ez(p_mispar_ishi , 56 ,v_date_from) meafyen56
		 FROM  OVDIM o ,
					  (SELECT MAX( po.ME_TARICH) me_taarich
					   FROM PIVOT_PIRTEY_OVDIM PO
					     WHERE 	mispar_ishi=p_mispar_ishi
					  AND (v_date_from BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
					  OR  TRUNC(p_taarich) BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
					  OR   po.ME_TARICH>=v_date_from AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<= TRUNC(p_taarich))) po,
						PIVOT_PIRTEY_OVDIM p
			WHERE p.mispar_ishi=p_mispar_ishi
			AND  p.Mispar_Ishi=o.mispar_ishi
			AND  po.ME_TAARICH= p.ME_TARICH;

EXCEPTION
       WHEN OTHERS THEN
            RAISE;

END  pro_get_pirtey_oved;

function   pro_get_meafyen_leoved_by_ez(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE, p_kod_meafyen in number,p_taarich IN DATE) return varchar2 is
erech varchar2(100);
begin

        begin
            select nvl(x.erech_ishi,x.ERECH_MECHDAL_PARTANY) into erech
           from(select p.*,  RANK() OVER(PARTITION BY  p.mispar_ishi,p.KOD_MEAFYEN   ORDER BY p.ad_taarich  DESC  ) AS num           
                    from pivot_meafyenim_ovdim p 
                    where p.mispar_ishi=p_mispar_ishi
                        and p.KOD_MEAFYEN=p_kod_meafyen
                        and     ((p.me_taarich<=p_taarich AND p.ad_taarich<= LAST_DAY( p_taarich)  AND p.ad_taarich>=  p_taarich  ) OR
                                             (p.me_taarich>=  p_taarich    AND p.ad_taarich<= LAST_DAY(p_taarich  )  ) OR
                                             (p.me_taarich>= p_taarich    AND p.ad_taarich>=  LAST_DAY(p_taarich )  AND p.me_taarich<=  LAST_DAY(p_taarich )   ) OR
                                             (p.me_taarich<= p_taarich    AND p.ad_taarich>= LAST_DAY( p_taarich )  )) ) x
            where x.num =1;                                 
         EXCEPTION
           WHEN NO_DATA_FOUND  THEN
              erech:=null;
       END;
           
            if ( erech = null) then
                    select b.erech into erech
                    from  brerot_mechdal_meafyenim  b
                    where  b.KOD_MEAFYEN=p_kod_meafyen;
            end if;       
          return  erech; 
end  pro_get_meafyen_leoved_by_ez;
   
 /*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        13/05/2009     sari      1. פונקציה המחזירה את סוג חישוב  ואת מספר ריצת החישוב עובד
*/
 PROCEDURE pro_get_sug_chishuv(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
								 				p_taarich IN DATE,
											p_bakasha_id OUT TB_BAKASHOT.bakasha_id%TYPE,
											p_sug_chishuv OUT NUMBER,
											p_taarich_chishuv OUT DATE) IS
  v_count_bakashot NUMBER;
BEGIN
 	 	SELECT COUNT(b.BAKASHA_ID) INTO  v_count_bakashot
		FROM TB_BAKASHOT B
		WHERE B.Huavra_Lesachar=1
        AND EXISTS (SELECT bakasha_id FROM TB_CHISHUV_CHODESH_OVDIM  C
				   	   			WHERE    C.Bakasha_ID=B.Bakasha_ID
								AND C.Mispar_Ishi =p_mispar_ishi
								AND TO_CHAR(C.Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy'));

		IF v_count_bakashot=1 THEN
		    p_sug_chishuv:=0;

			SELECT B.Bakasha_ID INTO p_bakasha_id
			FROM TB_BAKASHOT B
		   WHERE B.Huavra_Lesachar=1
           AND EXISTS (SELECT bakasha_id FROM TB_CHISHUV_CHODESH_OVDIM  C
				   	   			WHERE    C.Bakasha_ID=B.Bakasha_ID
								AND C.Mispar_Ishi =p_mispar_ishi
								AND TO_CHAR(C.Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy'));

	 ELSIF v_count_bakashot>1 THEN
	 	     p_sug_chishuv:=1;

			 SELECT Bakasha_ID INTO p_bakasha_id
			 FROM TB_BAKASHOT
			 WHERE  Taarich_Haavara_Lesachar =  (SELECT MAX(Taarich_Haavara_Lesachar)
         	 	   							   		   								 FROM TB_BAKASHOT
        																		WHERE Bakasha_ID IN  (SELECT Bakasha_ID
																									                            FROM TB_CHISHUV_CHODESH_OVDIM
																									                           WHERE Mispar_Ishi =p_mispar_ishi
																																AND TO_CHAR(Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy') )
																				AND Huavra_Lesachar=1);

   ELSE
   		p_sug_chishuv:=2;
	END IF;


	SELECT TRUNC(Zman_Hatchala) INTO p_taarich_chishuv
		FROM TB_BAKASHOT
		WHERE Bakasha_ID= p_bakasha_id;

EXCEPTION
	WHEN NO_DATA_FOUND THEN
		  p_sug_chishuv:=2;
		  p_bakasha_id :=0;
		  p_taarich_chishuv:=SYSDATE;
     WHEN OTHERS THEN
            RAISE;

END  pro_get_sug_chishuv;


PROCEDURE pro_get_headruyot(p_cur OUT CurType) IS
BEGIN
	 OPEN  p_cur FOR
	 	   SELECT TB_SIDURIM_MEYUCHADIM.MISPAR_SIDUR,  CTB_SIDURIM_MEYUCHADIM.TEUR_SIDUR_MEYCHAD, TB_SIDURIM_MEYUCHADIM.KOD_MEAFYEN, TB_SIDURIM_MEYUCHADIM.ERECH
		   FROM TB_SIDURIM_MEYUCHADIM
		   INNER JOIN CTB_SIDURIM_MEYUCHADIM
		   		 ON CTB_SIDURIM_MEYUCHADIM.KOD_SIDUR_MEYUCHAD = TB_SIDURIM_MEYUCHADIM.MISPAR_SIDUR
		   WHERE TB_SIDURIM_MEYUCHADIM.KOD_MEAFYEN IN (53,6,7,8);

EXCEPTION
	 WHEN OTHERS THEN
            RAISE;
 END  pro_get_headruyot;

/*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        17/05/2009     sari      1. פונקציה המחזירה את ימי החופשה וההעידרות של העובד
*/
 PROCEDURE pro_get_pirtey_headrut(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
 		   										p_taarich IN DATE,
								 			p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
											 p_cur OUT CurType) IS
BEGIN
 	 	OPEN  p_cur FOR
			  SELECT  Mispar_Ishi,Taarich,
						SUM(CASE Kod_Rechiv WHEN 66 THEN Erech_Rechiv ELSE NULL END)   AS  YEMEY_HEADRUT,
			             SUM(CASE Kod_Rechiv  WHEN 62 THEN Erech_Rechiv  ELSE NULL END) AS YEMEY_MILUIM ,
						 SUM(CASE Kod_Rechiv WHEN 64  THEN Erech_Rechiv ELSE NULL END)   AS  YEMEY_TEUNA,
			             SUM(CASE Kod_Rechiv  WHEN 67 THEN Erech_Rechiv  ELSE NULL END) AS CHOFESH_CHOVA  ,
						 SUM(CASE Kod_Rechiv WHEN 8 THEN Erech_Rechiv ELSE NULL END)   AS CHOFESH_ZCHUT ,
			             SUM(CASE Kod_Rechiv  WHEN 60 THEN Erech_Rechiv  ELSE NULL END) AS YEMEY_MACHALA,
						  SUM(CASE Kod_Rechiv  WHEN 61 THEN Erech_Rechiv  ELSE NULL END)  AS YOM_MACHALA_BODED,
			             SUM(CASE Kod_Rechiv  WHEN 56 THEN Erech_Rechiv  ELSE NULL END) AS YEMEY_EVEL  ,
						 SUM(CASE Kod_Rechiv WHEN 69 THEN Erech_Rechiv ELSE NULL END)   AS MACHALAT_BEN_ZUG ,
			             SUM(CASE Kod_Rechiv  WHEN 71 THEN Erech_Rechiv  ELSE NULL END) AS MACHALAT_YELED,
						  SUM(CASE Kod_Rechiv  WHEN 70 THEN Erech_Rechiv  ELSE NULL END)  AS MACHALAT_HORE,
						  SUM(CASE Kod_Rechiv WHEN 68 THEN Erech_Rechiv ELSE NULL END)   AS  TIPAT_CHALAV,
			             SUM(CASE Kod_Rechiv  WHEN 65 THEN Erech_Rechiv  ELSE NULL END) AS HERAYON ,
						 NULL HEADRUT_SCHIRIM
			FROM TB_CHISHUV_CHODESH_OVDIM
			WHERE Mispar_Ishi=p_mispar_ishi
			AND bakasha_id= p_bakasha_id
			AND TO_CHAR(Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy')
			AND Kod_Rechiv IN (56,69,71,65,70,68,66,62,64,67,8,60,61)
			GROUP BY Mispar_Ishi,Taarich;

EXCEPTION
	 WHEN OTHERS THEN
            RAISE;
 END  pro_get_pirtey_headrut;

  PROCEDURE pro_get_pirtey_headrut_tmp(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
 		   										p_taarich IN DATE,
								 			p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
											 p_cur OUT CurType) IS
BEGIN
  	 DBMS_APPLICATION_INFO.SET_MODULE('PKG_OVDIM.pro_get_pirtey_headrut_tmp','get pirtey headrut');
  
 	 	OPEN  p_cur FOR
			  SELECT  Mispar_Ishi,Taarich,
						SUM(CASE Kod_Rechiv WHEN 66 THEN Erech_Rechiv ELSE NULL END)   AS  YEMEY_HEADRUT,
			             SUM(CASE Kod_Rechiv  WHEN 62 THEN Erech_Rechiv  ELSE NULL END) AS YEMEY_MILUIM ,
						 SUM(CASE Kod_Rechiv WHEN 64  THEN Erech_Rechiv ELSE NULL END)   AS  YEMEY_TEUNA,
			             SUM(CASE Kod_Rechiv  WHEN 67 THEN Erech_Rechiv  ELSE NULL END) AS CHOFESH_CHOVA  ,
						 SUM(CASE Kod_Rechiv WHEN 8 THEN Erech_Rechiv ELSE NULL END)   AS CHOFESH_ZCHUT ,
			             SUM(CASE Kod_Rechiv  WHEN 60 THEN Erech_Rechiv  ELSE NULL END) AS YEMEY_MACHALA,
						  SUM(CASE Kod_Rechiv  WHEN 61 THEN Erech_Rechiv  ELSE NULL END)  AS YOM_MACHALA_BODED,
			             SUM(CASE Kod_Rechiv  WHEN 56 THEN Erech_Rechiv  ELSE NULL END) AS YEMEY_EVEL  ,
						 SUM(CASE Kod_Rechiv WHEN 69 THEN Erech_Rechiv ELSE NULL END)   AS MACHALAT_BEN_ZUG ,
			             SUM(CASE Kod_Rechiv  WHEN 71 THEN Erech_Rechiv  ELSE NULL END) AS MACHALAT_YELED,
						  SUM(CASE Kod_Rechiv  WHEN 70 THEN Erech_Rechiv  ELSE NULL END)  AS MACHALAT_HORE,
						  SUM(CASE Kod_Rechiv WHEN 68 THEN Erech_Rechiv ELSE NULL END)   AS  TIPAT_CHALAV,
			             SUM(CASE Kod_Rechiv  WHEN 65 THEN Erech_Rechiv  ELSE NULL END) AS HERAYON ,
						 NULL HEADRUT_SCHIRIM
			FROM TB_TMP_CHISHUV_CHODESH_OVDIM
			WHERE Mispar_Ishi=p_mispar_ishi
			AND bakasha_id= p_bakasha_id
			AND TO_CHAR(Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy')
			AND Kod_Rechiv IN (56,69,71,65,70,68,66,62,64,67,8,60,61)
			GROUP BY Mispar_Ishi,Taarich;

EXCEPTION
	 WHEN OTHERS THEN
            RAISE;
 END  pro_get_pirtey_headrut_tmp;

  /*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        17/05/2009     sari      1. פונקציה המחזירה רכיבים חודשיים
*/
  PROCEDURE pro_get_rechivim_codshiyim(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
  													 								  		p_taarich IN DATE,
																			 				p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
																							p_tzuga IN CTB_RECHIVIM.Letzuga_Besikum_Chodshi%TYPE,
																						 p_cur OUT CurType) IS
      v_tar_me DATE;
BEGIN
        v_tar_me:=TO_DATE('01/' || TO_CHAR(p_taarich,'mm/yyyy'),'dd/mm/yyyy');
 	 	OPEN  p_cur FOR
			  SELECT R.Kod_Rechiv,R.Teur_Rechiv, Ch.Erech_Rechiv, R.MIYUN_BESIKUM_CHODSHI,R.Letzuga_Besikum_Chodshi,r.LETZUGA_BETIHKUR_RASHEMET
			FROM CTB_RECHIVIM R, TB_CHISHUV_CHODESH_OVDIM Ch
			WHERE R.Kod_Rechiv=Ch.Kod_Rechiv
			AND DECODE(p_tzuga,1,R.Letzuga_Besikum_Chodshi,r.LETZUGA_BETIHKUR_RASHEMET)=2
			AND Ch.Mispar_Ishi=p_mispar_ishi
			AND ch.bakasha_id=p_bakasha_id
			AND ch.taarich between v_tar_me and last_day(v_tar_me) --AND TO_CHAR(ch.Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy')
			AND Ch.Erech_Rechiv IS NOT NULL
			ORDER BY R.MIYUN_BESIKUM_CHODSHI ASC;

EXCEPTION
	 WHEN OTHERS THEN
            RAISE;

END  pro_get_rechivim_codshiyim;

 PROCEDURE pro_get_rechivim_codshiyim_tmp(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
  													 								  		p_taarich IN DATE,
																			 				p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
																							p_tzuga IN CTB_RECHIVIM.Letzuga_Besikum_Chodshi%TYPE,
																						 p_cur OUT CurType) IS
    v_tar_me DATE;
BEGIN
	 DBMS_APPLICATION_INFO.SET_MODULE('pkg_ovdim.pro_get_rechivim_codshiyim_tmp','get rechivim codshiyim ');
       v_tar_me:=TO_DATE('01/' || TO_CHAR(p_taarich,'mm/yyyy'),'dd/mm/yyyy');

 	 	OPEN  p_cur FOR
			  SELECT R.Kod_Rechiv,R.Teur_Rechiv, Ch.Erech_Rechiv, R.MIYUN_BESIKUM_CHODSHI,R.Letzuga_Besikum_Chodshi,r.LETZUGA_BETIHKUR_RASHEMET
			FROM CTB_RECHIVIM R, TB_TMP_CHISHUV_CHODESH_OVDIM Ch
			WHERE R.Kod_Rechiv=Ch.Kod_Rechiv
			AND DECODE(p_tzuga,1,R.Letzuga_Besikum_Chodshi,r.LETZUGA_BETIHKUR_RASHEMET)=2
			AND Ch.Mispar_Ishi=p_mispar_ishi
			AND ch.bakasha_id=p_bakasha_id
			AND ch.taarich between v_tar_me and last_day(v_tar_me) --AND TO_CHAR(ch.Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy')
			AND Ch.Erech_Rechiv IS NOT NULL
			ORDER BY R.MIYUN_BESIKUM_CHODSHI ASC;

EXCEPTION
	 WHEN OTHERS THEN
            RAISE;

END  pro_get_rechivim_codshiyim_tmp;

 /*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        18/05/2009     sari      1. פונקציה המחזירה ריכוז עבודה חודשי
*/
  PROCEDURE pro_get_rikuz_chodshi(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
  													 								  		p_taarich IN DATE,
																			 				p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
																							p_tzuga IN CTB_RECHIVIM.Letzuga_Besikum_Chodshi%TYPE,
																						 p_cur OUT CurType) IS
			v_tar_me DATE;
			v_tar_ad DATE;
			str_dates VARCHAR2(500);
	BEGIN
		  BEGIN
			         --נשרשר למחרוזת את כל התאריכים בחודש
					 v_tar_me:=TO_DATE('01/' || TO_CHAR(p_taarich,'mm/yyyy'),'dd/mm/yyyy');

			         LOOP
						BEGIN
						EXIT WHEN  v_tar_me>p_taarich;
								str_dates:=str_dates || TO_CHAR(v_tar_me,'dd/mm/yyyy') || ',';
								 v_tar_me:= v_tar_me+1;
						END;
					 END LOOP;

					 str_dates:=SUBSTR(str_dates,0,LENGTH(str_dates)-1);
			END;

     v_tar_me:=TO_DATE('01/' || TO_CHAR(p_taarich,'mm/yyyy'),'dd/mm/yyyy');
 	 OPEN  p_cur FOR
		SELECT * FROM
		((SELECT  DECODE(tmp1.MIYUN_BESIKUM_CHODSHI,NULL,800,tmp1.MIYUN_BESIKUM_CHODSHI) MIYUN_BESIKUM_CHODSHI
				,tmp1.Teur_Rechiv, ' ' heara,DECODE(tmp1.Erech_Rechiv,NULL,0,tmp1.Erech_Rechiv) erech_rechiv, TO_DATE(tmp2.x,'dd/mm/yyyy') Taarich,
							 TO_NUMBER(TO_CHAR(DECODE(tmp1.Kod_Rechiv,NULL,0,tmp1.Kod_Rechiv))) Kod_Rechiv,
							 TO_NUMBER(TO_CHAR(TO_DATE(tmp2.x,'dd/mm/yyyy') ,'DD')) day_taarich ,
							 CASE  kod_rechiv
								     WHEN 76 THEN (SELECT y.erech_rechiv FROM TB_CHISHUV_CHODESH_OVDIM y
											WHERE y. Mispar_Ishi=p_mispar_ishi
											AND y.taarich between v_tar_me and last_day(v_tar_me) -- TO_CHAR(Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy')
											AND y.Erech_Rechiv IS NOT NULL
											AND y.Bakasha_ID=p_bakasha_id
											AND y.kod_rechiv=119)
								 WHEN 77 THEN (SELECT y.erech_rechiv FROM TB_CHISHUV_CHODESH_OVDIM y
											WHERE y. Mispar_Ishi=p_mispar_ishi
											AND y.taarich between v_tar_me and last_day(v_tar_me) --	AND  TO_CHAR(Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy')
											AND y.Erech_Rechiv IS NOT NULL
											AND y.Bakasha_ID=p_bakasha_id
											AND y.kod_rechiv=120)
									 WHEN 78 THEN (SELECT y.erech_rechiv FROM TB_CHISHUV_CHODESH_OVDIM y
											WHERE y. Mispar_Ishi=p_mispar_ishi
											AND y.taarich between v_tar_me and last_day(v_tar_me) --	AND  TO_CHAR(Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy')
											AND y.Erech_Rechiv IS NOT NULL
											AND y.Bakasha_ID=p_bakasha_id
											AND y.kod_rechiv=121 )
									 WHEN 1 THEN (SELECT y.erech_rechiv FROM TB_CHISHUV_CHODESH_OVDIM y
											WHERE y. Mispar_Ishi=p_mispar_ishi
											AND y.taarich between v_tar_me and last_day(v_tar_me) --	AND  TO_CHAR(Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy')
											AND y.Erech_Rechiv IS NOT NULL
											AND y.Bakasha_ID=p_bakasha_id
											AND y.kod_rechiv=108 )
											ELSE NULL END  AS kizuz_meal_michsa
			  FROM
				(SELECT  R.MIYUN_BESIKUM_CHODSHI,R.Teur_Rechiv, ' ' heara,C.Erech_Rechiv erech_rechiv,
						  c.Taarich,TO_NUMBER(TO_CHAR(R.Kod_Rechiv)) Kod_Rechiv,TO_NUMBER(TO_CHAR(c.Taarich,'DD')) day_taarich
							
						FROM CTB_RECHIVIM R, TB_CHISHUV_YOMI_OVDIM C
						WHERE R.Kod_Rechiv=C.Kod_Rechiv
						AND DECODE(p_tzuga,1,R.Letzuga_Besikum_Chodshi,r.LETZUGA_BETIHKUR_RASHEMET)=1
						AND C. Mispar_Ishi=p_mispar_ishi
						AND c.taarich between v_tar_me and last_day(v_tar_me) --AND TO_CHAR(c.Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy')
						AND C.Erech_Rechiv IS NOT NULL
						AND C.Bakasha_ID=p_bakasha_id) tmp1,
			 			 TABLE(CAST(Convert_String_To_Table(str_dates ,  ',') AS mytabtype)) tmp2
			WHERE  TO_DATE(tmp2.x,'dd/mm/yyyy')=tmp1.Taarich(+))
		UNION ALL
			   (SELECT 900 MIYUN_BESIKUM_CHODSHI, 'הערה' TEUR_RECHIV,'מושעה' heara,0 erech_rechiv,
	   		    Y.TAARICH,TO_NUMBER(TO_CHAR(Y.KOD_RECHIV)) Kod_Rechiv,TO_NUMBER(TO_CHAR(y.Taarich,'DD'))  day_taarich,
	   			NULL kizuz_meal_michsa
	          FROM MATZAV_OVDIM p, TB_CHISHUV_YOMI_OVDIM y
				WHERE  p.Mispar_Ishi=p_mispar_ishi
			   AND y.taarich between v_tar_me and last_day(v_tar_me) --AND TO_CHAR(y.Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy')
					AND y.TAARICH BETWEEN p.taarich_hatchala AND p.taarich_siyum
				AND  p.Kod_matzav='33')
		UNION ALL
	   (SELECT 900 MIYUN_BESIKUM_CHODSHI, 'הערה' TEUR_RECHIV,MAX(H.HEARA) heara,0 erech_rechiv,
	   		    Y.TAARICH,-1 Kod_Rechiv,TO_NUMBER(TO_CHAR(y.Taarich,'DD'))  day_taarich,
	   			NULL kizuz_meal_michsa
	   FROM (SELECT   Mispar_Ishi,Taarich,SYS_CONNECT_BY_PATH (kod_rechiv,'.') KOD_RECHIV
       		            FROM ( SELECT C. Mispar_Ishi,  C.Kod_Rechiv, c.Taarich,ROW_NUMBER () OVER (PARTITION BY c.Taarich ORDER BY c.kod_rechiv ASC) RN
								          FROM TB_CHISHUV_YOMI_OVDIM C,CTB_RECHIVIM r
										  WHERE  C.Bakasha_ID=p_bakasha_id
										  AND C. Mispar_Ishi=p_mispar_ishi
										  AND c.erech_rechiv>0
										  AND r.yesh_heara=1
										  AND r.kod_rechiv=c.kod_rechiv
										  AND c.taarich between v_tar_me and last_day(v_tar_me)) --AND TO_CHAR(c.Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy') )
						    CONNECT BY Taarich = PRIOR Taarich AND RN = PRIOR RN + 1
						    START WITH RN = 1
						      ORDER BY Taarich) Y,
	 						 CTB_HEAROT_RECHIVIM H,
							  (select  o.taarich,p.erech  
                             from (select p_mispar_ishi   mispar_ishi ,TO_DATE(x,'dd/mm/yyyy') taarich 
                                      from ( SELECT X from    TABLE(CAST(Convert_String_To_Table(String_Dates_Of_Period(to_char( p_taarich ,'mm/yyyy')),',') AS mytabtype)))  )o,
                                      PIRTEY_OVDIM p  
                             where o.mispar_ISHI=  p.mispar_ISHI(+)
                               and  o.taarich between   p.Me_taarich(+) and p.Ad_taarich(+)  
                                and P.KOD_natun(+) =9 ) m
             WHERE H.KOD_RECHIV=SUBSTR(Y.KOD_RECHIV,2,LENGTH(Y.KOD_RECHIV)-1)
           AND ((H.MUTAM_BITACHON=1 AND M.ERECH IS NOT NULL) OR M.ERECH IS NULL)
           and  y.taarich =  m.taarich
		   GROUP BY Y.TAARICH)
		    ORDER BY MIYUN_BESIKUM_CHODSHI,Kod_Rechiv,Taarich ASC);

EXCEPTION
	 WHEN OTHERS THEN
            RAISE;
END  pro_get_rikuz_chodshi;

PROCEDURE pro_get_rikuz_chodshi_tmp(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
  													 								  		p_taarich IN DATE,
																			 				p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
																							p_tzuga IN CTB_RECHIVIM.Letzuga_Besikum_Chodshi%TYPE,
																						 p_cur OUT CurType) IS
			v_tar_me DATE;
			v_tar_ad DATE;
			str_dates VARCHAR2(500);
	BEGIN
	DBMS_APPLICATION_INFO.SET_MODULE('pkg_ovdim.pro_get_rikuz_chodshi_tmp','get rikuz chodshi');
		  BEGIN
			         --נשרשר למחרוזת את כל התאריכים בחודש
					 v_tar_me:=TO_DATE('01/' || TO_CHAR(p_taarich,'mm/yyyy'),'dd/mm/yyyy');

			         LOOP
						BEGIN
						EXIT WHEN  v_tar_me>p_taarich;
								str_dates:=str_dates || TO_CHAR(v_tar_me,'dd/mm/yyyy') || ',';
								 v_tar_me:= v_tar_me+1;
						END;
					 END LOOP;

					 str_dates:=SUBSTR(str_dates,0,LENGTH(str_dates)-1);
			END;

    v_tar_me:=TO_DATE('01/' || TO_CHAR(p_taarich,'mm/yyyy'),'dd/mm/yyyy');

 	 OPEN  p_cur FOR
		SELECT * FROM
		((SELECT DECODE(tmp1.MIYUN_BESIKUM_CHODSHI,NULL,800,tmp1.MIYUN_BESIKUM_CHODSHI) MIYUN_BESIKUM_CHODSHI
				,tmp1.Teur_Rechiv, ' ' heara,DECODE(tmp1.Erech_Rechiv,NULL,0,tmp1.Erech_Rechiv) erech_rechiv, TO_DATE(tmp2.x,'dd/mm/yyyy') Taarich,
							 TO_NUMBER(TO_CHAR(DECODE(tmp1.Kod_Rechiv,NULL,0,tmp1.Kod_Rechiv))) Kod_Rechiv,
							 TO_NUMBER(TO_CHAR(TO_DATE(tmp2.x,'dd/mm/yyyy') ,'DD')) day_taarich ,
							  CASE  kod_rechiv
								     WHEN 76 THEN (SELECT y.erech_rechiv FROM TB_CHISHUV_CHODESH_OVDIM y
											WHERE y. Mispar_Ishi=p_mispar_ishi
											AND y.taarich between v_tar_me and last_day(v_tar_me)    --TO_CHAR(Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy')
											AND y.Erech_Rechiv IS NOT NULL
											AND y.Bakasha_ID=p_bakasha_id
											AND y.kod_rechiv=119)
								 WHEN 77 THEN (SELECT y.erech_rechiv FROM TB_CHISHUV_CHODESH_OVDIM y
											WHERE y. Mispar_Ishi=p_mispar_ishi
											AND y.taarich between v_tar_me and last_day(v_tar_me)    --	AND  TO_CHAR(Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy')
											AND y.Erech_Rechiv IS NOT NULL
											AND y.Bakasha_ID=p_bakasha_id
											AND y.kod_rechiv=120)
									 WHEN 78 THEN (SELECT y.erech_rechiv FROM TB_CHISHUV_CHODESH_OVDIM y
											WHERE y. Mispar_Ishi=p_mispar_ishi
											AND y.taarich between v_tar_me and last_day(v_tar_me)    --	AND  TO_CHAR(Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy')
											AND y.Erech_Rechiv IS NOT NULL
											AND y.Bakasha_ID=p_bakasha_id
											AND y.kod_rechiv=121 )
									 WHEN 1 THEN (SELECT y.erech_rechiv FROM TB_CHISHUV_CHODESH_OVDIM y
											WHERE y. Mispar_Ishi=p_mispar_ishi
											AND y.taarich between v_tar_me and last_day(v_tar_me)    --	AND  TO_CHAR(Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy')
											AND y.Erech_Rechiv IS NOT NULL
											AND y.Bakasha_ID=p_bakasha_id
											AND y.kod_rechiv=108 )
											ELSE NULL END  AS kizuz_meal_michsa
			  FROM
				(SELECT R.MIYUN_BESIKUM_CHODSHI,R.Teur_Rechiv, ' ' heara,C.Erech_Rechiv erech_rechiv,
						  c.Taarich,TO_NUMBER(TO_CHAR(R.Kod_Rechiv)) Kod_Rechiv,TO_NUMBER(TO_CHAR(c.Taarich,'DD')) day_taarich
						FROM CTB_RECHIVIM R, TB_TMP_CHISHUV_YOMI_OVDIM C
						WHERE R.Kod_Rechiv=C.Kod_Rechiv
						AND DECODE(p_tzuga,1,R.Letzuga_Besikum_Chodshi,r.LETZUGA_BETIHKUR_RASHEMET)=1
						AND C. Mispar_Ishi=p_mispar_ishi
						AND c.taarich between v_tar_me and last_day(v_tar_me)    --AND TO_CHAR(c.Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy')
						AND C.Erech_Rechiv IS NOT NULL
						AND C.Bakasha_ID=p_bakasha_id) tmp1,
			 			 TABLE(CAST(Convert_String_To_Table(str_dates ,  ',') AS mytabtype)) tmp2
			WHERE  TO_DATE(tmp2.x,'dd/mm/yyyy')=tmp1.Taarich(+))
		UNION ALL
			   (SELECT 900 MIYUN_BESIKUM_CHODSHI, 'הערה' TEUR_RECHIV,'מושעה' heara,0 erech_rechiv,
	   		    Y.TAARICH,TO_NUMBER(TO_CHAR(Y.KOD_RECHIV)) Kod_Rechiv,TO_NUMBER(TO_CHAR(y.Taarich,'DD'))  day_taarich,
	   			NULL kizuz_meal_michsa
	          FROM MATZAV_OVDIM p, TB_TMP_CHISHUV_YOMI_OVDIM y
				WHERE  p.Mispar_Ishi=p_mispar_ishi
			        AND y.taarich between v_tar_me and last_day(v_tar_me)    -- TO_CHAR(y.Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy')
					AND y.TAARICH BETWEEN p.taarich_hatchala AND p.taarich_siyum
				AND  p.Kod_matzav='33')
		UNION ALL
	   (SELECT 900 MIYUN_BESIKUM_CHODSHI, 'הערה' TEUR_RECHIV,MAX(H.HEARA) heara,0 erech_rechiv,
	   		    Y.TAARICH,-1 Kod_Rechiv,TO_NUMBER(TO_CHAR(y.Taarich,'DD'))  day_taarich,
	   			NULL kizuz_meal_michsa
	   FROM (SELECT   Mispar_Ishi,Taarich,SYS_CONNECT_BY_PATH (kod_rechiv,'.') KOD_RECHIV
       		            FROM ( SELECT C. Mispar_Ishi,  C.Kod_Rechiv, c.Taarich,ROW_NUMBER () OVER (PARTITION BY c.Taarich ORDER BY c.kod_rechiv ASC) RN
								          FROM TB_TMP_CHISHUV_YOMI_OVDIM C,CTB_RECHIVIM r
										  WHERE  C.Bakasha_ID=p_bakasha_id
										  AND C. Mispar_Ishi=p_mispar_ishi
										  AND c.erech_rechiv>0
										  AND r.yesh_heara=1
										  AND r.kod_rechiv=c.kod_rechiv
										 AND c.taarich between v_tar_me and last_day(v_tar_me) )   -- AND TO_CHAR(c.Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy'))
						    CONNECT BY Taarich = PRIOR Taarich AND RN = PRIOR RN + 1
						    START WITH RN = 1
						     ORDER BY Taarich) Y,
	 						 CTB_HEAROT_RECHIVIM H,
							  (select  o.taarich,p.erech  
                             from (select p_mispar_ishi   mispar_ishi ,TO_DATE(x,'dd/mm/yyyy') taarich 
                                      from ( SELECT X from    TABLE(CAST(Convert_String_To_Table(String_Dates_Of_Period(to_char( p_taarich ,'mm/yyyy')),',') AS mytabtype)))  )o,
                                      PIRTEY_OVDIM p  
                             where o.mispar_ISHI=  p.mispar_ISHI(+)
                               and  o.taarich between   p.Me_taarich(+) and p.Ad_taarich(+)  
                                and P.KOD_natun(+) =9 ) m
	         WHERE H.KOD_RECHIV=SUBSTR(Y.KOD_RECHIV,2,LENGTH(Y.KOD_RECHIV)-1)
	       AND ((H.MUTAM_BITACHON=1 AND M.ERECH IS NOT NULL) OR M.ERECH IS NULL)
           and  y.taarich =  m.taarich
		   		    GROUP BY Y.TAARICH)
		    ORDER BY MIYUN_BESIKUM_CHODSHI,Kod_Rechiv,Taarich ASC);

EXCEPTION
	 WHEN OTHERS THEN
            RAISE;
END  pro_get_rikuz_chodshi_tmp;

PROCEDURE pro_get_rec_pirtey_oved(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE, p_taarich IN DATE, p_cur OUT CurType)
IS
BEGIN
    OPEN p_cur FOR
		SELECT DISTINCT q1.erech yacha,q2.erech taarich_hatzava ,q3.erech ezor,q4.erech snif_av,q5.erech mikum,
		q6.erech isuk,q7.erech rishyon,q8.erech mutam,q9.erech mutam_bitachon,q10.erech erua,
		q11.erech dirug,q12.erech darga, q13.erech maamad, q14.erech gil, q15.erech tachanat_sachar,
		q17.erech misra,q18.erech revacha
FROM PIRTEY_OVDIM q1,  PIRTEY_OVDIM q2,  PIRTEY_OVDIM q3,  PIRTEY_OVDIM q4,  PIRTEY_OVDIM q5,  PIRTEY_OVDIM q6,
          PIRTEY_OVDIM q7,  PIRTEY_OVDIM q8, PIRTEY_OVDIM q9,  PIRTEY_OVDIM q10,PIRTEY_OVDIM q11,PIRTEY_OVDIM q12,
		  PIRTEY_OVDIM q13,  PIRTEY_OVDIM q14,  PIRTEY_OVDIM q15,   PIRTEY_OVDIM q17,  PIRTEY_OVDIM q18
WHERE q1.mispar_ishi(+)=p_mispar_ishi
AND  q1.kod_natun(+)=1
 AND q1.mispar_ishi(+)=q2.mispar_ishi
 AND p_taarich BETWEEN q1.me_taarich(+) AND q1.ad_taarich(+)
AND q2.mispar_ishi=p_mispar_ishi
AND  q2.kod_natun=2
 AND p_taarich BETWEEN q2.me_taarich(+) AND q2.ad_taarich(+)
AND q3.mispar_ishi(+)=p_mispar_ishi
AND  q3.kod_natun(+)=3
 AND q3.mispar_ishi(+)=q2.mispar_ishi
 AND p_taarich BETWEEN q3.me_taarich(+) AND q3.ad_taarich(+)
AND q4.mispar_ishi(+)=p_mispar_ishi
AND  q4.kod_natun(+)=4
 AND q4.mispar_ishi(+)=q2.mispar_ishi
 AND p_taarich BETWEEN q4.me_taarich(+) AND q4.ad_taarich(+)
AND q5.mispar_ishi(+)=p_mispar_ishi
AND  q5.kod_natun(+)=5
 AND q5.mispar_ishi(+)=q2.mispar_ishi
 AND p_taarich BETWEEN q5.me_taarich(+) AND q5.ad_taarich(+)
AND q6.mispar_ishi(+)=p_mispar_ishi
AND  q6.kod_natun(+)=6
 AND q6.mispar_ishi(+)=q2.mispar_ishi
 AND p_taarich BETWEEN q6.me_taarich(+) AND q6.ad_taarich(+)
AND q7.mispar_ishi(+)=p_mispar_ishi
AND  q7.kod_natun(+)=7
 AND q7.mispar_ishi(+)=q2.mispar_ishi
 AND p_taarich BETWEEN q7.me_taarich(+) AND q7.ad_taarich(+)
AND q8.mispar_ishi(+)=p_mispar_ishi
AND  q8.kod_natun(+)=8
 AND q8.mispar_ishi(+)=q2.mispar_ishi
 AND p_taarich BETWEEN q8.me_taarich(+) AND q8.ad_taarich(+)
AND q9.mispar_ishi(+)=p_mispar_ishi
AND  q9.kod_natun(+)=9
 AND q9.mispar_ishi(+)=q2.mispar_ishi
 AND p_taarich BETWEEN q9.me_taarich(+) AND q9.ad_taarich(+)
AND q10.mispar_ishi(+)=p_mispar_ishi
AND  q10.kod_natun(+)=10
 AND q10.mispar_ishi(+)=q2.mispar_ishi
 AND p_taarich BETWEEN q10.me_taarich(+) AND q10.ad_taarich(+)
AND q11.mispar_ishi(+)=p_mispar_ishi
AND  q11.kod_natun(+)=11
 AND q11.mispar_ishi(+)=q2.mispar_ishi
 AND p_taarich BETWEEN q11.me_taarich(+) AND q11.ad_taarich(+)
AND q12.mispar_ishi(+)=p_mispar_ishi
AND  q12.kod_natun(+)=12
 AND q12.mispar_ishi(+)=q2.mispar_ishi
 AND p_taarich BETWEEN q12.me_taarich(+) AND q12.ad_taarich(+)
AND q13.mispar_ishi(+)=p_mispar_ishi
AND  q13.kod_natun(+)=13
 AND q13.mispar_ishi(+)=q2.mispar_ishi
 AND p_taarich BETWEEN q13.me_taarich(+) AND q13.ad_taarich(+)
AND q14.mispar_ishi(+)=p_mispar_ishi
AND  q14.kod_natun(+)=14
 AND q14.mispar_ishi(+)=q2.mispar_ishi
 AND p_taarich BETWEEN q14.me_taarich(+) AND q14.ad_taarich(+)
AND q15.mispar_ishi(+)=p_mispar_ishi
AND  q15.kod_natun(+)=15
 AND q15.mispar_ishi(+)=q2.mispar_ishi
 AND p_taarich BETWEEN q15.me_taarich(+) AND q15.ad_taarich(+)
AND q17.mispar_ishi(+)=p_mispar_ishi
AND  q17.kod_natun(+)=17
 AND q17.mispar_ishi(+)=q2.mispar_ishi
 AND p_taarich BETWEEN q17.me_taarich(+) AND q17.ad_taarich(+)
AND q18.mispar_ishi(+)=p_mispar_ishi
AND  q18.kod_natun(+)=18
AND q18.mispar_ishi(+)=q2.mispar_ishi
 AND p_taarich BETWEEN q18.me_taarich(+) AND q18.ad_taarich(+);

EXCEPTION
       WHEN OTHERS THEN
            RAISE;

END  pro_get_rec_pirtey_oved;

PROCEDURE pro_upd_premia_details(p_kod_premia IN TRAIL_PREMYOT_YADANIYOT.SUG_PREMYA%TYPE,
		  									  	 								p_mispar_ishi IN TRAIL_PREMYOT_YADANIYOT.MISPAR_ISHI_TRAIL%TYPE,
																				p_taarich IN TRAIL_PREMYOT_YADANIYOT.TAARICH_IDKUN_TRAIL%TYPE,
																				p_dakot_premia IN TRAIL_PREMYOT_YADANIYOT.DAKOT_PREMYA%TYPE,
																				p_mispar_ishi_of_meadken IN TRAIL_PREMYOT_YADANIYOT.MISPAR_ISHI_TRAIL%TYPE)
IS
  meadken_acharon NUMBER;
  taarich_idkun_acharon DATE;
BEGIN

	 meadken_acharon:=0;
	  taarich_idkun_acharon:=NULL;

	SELECT COUNT(*)	 INTO meadken_acharon
	 FROM TB_PREMYOT_YADANIYOT
	 WHERE MISPAR_ISHI = p_mispar_ishi
	      AND TAARICH = p_taarich
		  AND SUG_PREMYA = p_kod_premia;

	IF meadken_acharon > 0 THEN
	 SELECT MEADKEN_ACHARON, TAARICH_IDKUN_ACHARON
	 		    INTO meadken_acharon, taarich_idkun_acharon
	 FROM TB_PREMYOT_YADANIYOT
	 WHERE MISPAR_ISHI = p_mispar_ishi
	     AND TAARICH = p_taarich
		 AND SUG_PREMYA = p_kod_premia;
    END IF;

	IF taarich_idkun_acharon IS NOT NULL THEN
			    INSERT INTO TRAIL_PREMYOT_YADANIYOT(MISPAR_ISHI,TAARICH,SUG_PREMYA,DAKOT_PREMYA,TAARICH_IDKUN_ACHARON,MEADKEN_ACHARON,MISPAR_ISHI_TRAIL,TAARICH_IDKUN_TRAIL,SUG_PEULA)
				SELECT MISPAR_ISHI,TAARICH,SUG_PREMYA,DAKOT_PREMYA,TAARICH_IDKUN_ACHARON,MEADKEN_ACHARON,p_mispar_ishi_of_meadken,SYSDATE,3 
			   FROM  TB_PREMYOT_YADANIYOT
			      WHERE MISPAR_ISHI = p_mispar_ishi
			     AND TAARICH = p_taarich
				 AND SUG_PREMYA = p_kod_premia;
		 
			   UPDATE TB_PREMYOT_YADANIYOT
			   SET DAKOT_PREMYA = p_dakot_premia,
			          MEADKEN_ACHARON = p_mispar_ishi_of_meadken,
					  TAARICH_IDKUN_ACHARON = SYSDATE
			   WHERE MISPAR_ISHI = p_mispar_ishi
			     AND TAARICH = p_taarich
				 AND SUG_PREMYA = p_kod_premia;

		ELSE
		INSERT INTO TB_PREMYOT_YADANIYOT(MISPAR_ISHI,TAARICH,SUG_PREMYA,DAKOT_PREMYA,TAARICH_IDKUN_ACHARON,MEADKEN_ACHARON)
		VALUES(p_mispar_ishi,p_taarich,p_kod_premia,p_dakot_premia,SYSDATE,p_mispar_ishi_of_meadken);

		--insert into meafyenim_ovdim(MISPAR_ISHI,KOD_MEAFYEN,ME_TAARICH,AD_TAARICH,ERECH_ISHI)
		--values(p_mispar_ishi,p_kod_premia,p_taarich,to_date('4712/12/31', 'yyyy/mm/dd'),1);
	END IF;

EXCEPTION
       WHEN OTHERS THEN
            RAISE;

END pro_upd_premia_details;


FUNCTION fun_get_BakashaId(p_misparIshi IN OVDIM.MISPAR_ISHI%TYPE ,
                                         p_taarich IN TB_CHISHUV_CHODESH_OVDIM.TAARICH%TYPE )
                                         RETURN TB_BAKASHOT.BAKASHA_ID%TYPE AS
        v_bakashaId TB_BAKASHOT.BAKASHA_ID%TYPE ; 
        v_sug_chishuv NUMBER ;
        v_taarich_chishuv DATE;
BEGIN
 v_bakashaId := 0 ;
 pro_get_sug_chishuv(p_misparIshi,p_taarich , v_bakashaId,v_sug_chishuv ,v_taarich_chishuv ) ;
RETURN v_bakashaId;  
END fun_get_BakashaId;
                                          
                                         
/*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0       25/05/2009     sari      1. פונקציה המחזירה מאפייני עובד
*/
FUNCTION fun_get_meafyen_oved(p_mispar_ishi IN MEAFYENIM_OVDIM.mispar_ishi%TYPE,
														p_kod_meafyen  IN MEAFYENIM_OVDIM.kod_meafyen%TYPE,
														p_taarich  IN MEAFYENIM_OVDIM.me_taarich%TYPE) RETURN MEAFYENIM_OVDIM.erech_ishi%TYPE AS
	v_erech MEAFYENIM_OVDIM.erech_ishi%TYPE;
BEGIN
	BEGIN

		SELECT DECODE(m.erech_ishi,NULL,m.ERECH_MECHDAL_PARTANY,m.erech_ishi) INTO v_erech
		   FROM PIVOT_MEAFYENIM_OVDIM m
		   WHERE TRUNC(p_taarich) BETWEEN m.ME_TAARICH AND NVL(m.AD_TAARICH,TO_DATE('01/01/9999','dd/mm/yyyy'))
		   AND m.MISPAR_ISHI=p_mispar_ishi
			AND m.kod_meafyen=p_kod_meafyen;

		 EXCEPTION
	       WHEN NO_DATA_FOUND THEN
		   		 v_erech:='-1';
	END;
	     IF  v_erech='-1' THEN
				       SELECT  m.erech  INTO v_erech
				   		FROM BREROT_MECHDAL_MEAFYENIM m
				   		WHERE TRUNC(p_taarich) BETWEEN m.ME_TAARICH AND NVL(m.AD_TAARICH,TO_DATE('01/01/9999','dd/mm/yyyy'))
						AND m.kod_meafyen=p_kod_meafyen;
		END IF;
     RETURN v_erech;
END   fun_get_meafyen_oved;

PROCEDURE pro_get_ovedim_mispar_ishi(p_prefix IN VARCHAR2, p_cur OUT CurType) IS
BEGIN
        OPEN p_cur FOR
        SELECT o.mispar_ishi
        FROM OVDIM o
        WHERE o.mispar_ishi LIKE p_prefix
        ORDER BY o.mispar_ishi;

    EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END pro_get_ovedim_mispar_ishi;

PROCEDURE pro_get_oved_snif_unit(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE, p_teur_snif OUT CTB_SNIF_AV.teur_snif_av%TYPE, p_teur_unit OUT CTB_YECHIDA.teur_yechida%TYPE) IS
    v_teur_unit CTB_YECHIDA.teur_yechida%TYPE;
    v_teur_snif CTB_SNIF_AV.teur_snif_av%TYPE;
BEGIN
     --Get unit and snif description for employee
     SELECT s.Teur_Snif_av, y.teur_yechida INTO  v_teur_snif,v_teur_unit

     FROM PIRTEY_OVDIM o1, PIRTEY_OVDIM o2,  CTB_SNIF_AV s ,CTB_YECHIDA y,OVDIM o,
            (SELECT tb_snif.mispar_ishi,
                   DECODE (tb_snif.kod_natun, 4, taarich_snif) taarich_snif,
                   DECODE (tb_unit.kod_natun, 1, taarich_unit) taarich_unit
             FROM (SELECT  MAX (o.me_taarich) taarich_unit, mispar_ishi  , kod_natun
                  FROM PIRTEY_OVDIM o
                  WHERE o.kod_natun = 1
                  GROUP BY mispar_ishi, kod_natun) tb_unit,

                  (SELECT MAX (o.me_taarich) taarich_snif, mispar_ishi, kod_natun
                   FROM PIRTEY_OVDIM o
                   WHERE o.kod_natun = 4
                   GROUP BY mispar_ishi, kod_natun) tb_snif
            WHERE tb_unit.mispar_ishi = tb_snif.mispar_ishi AND tb_unit.mispar_ishi=  p_mispar_ishi
            ) b

     WHERE o1.kod_natun = 1
          AND o1.mispar_ishi = b.mispar_ishi
          AND o1.me_taarich =  b.taarich_unit
          AND o2.mispar_ishi = b.mispar_ishi
          AND o2.me_taarich =  b.taarich_snif
          AND o2.kod_natun = 4
          AND s.kod_snif_av = o2.erech
          AND y.kod_yechida = o1.erech
          AND y.kod_hevra= s.kod_hevra
          AND o1.mispar_ishi = p_mispar_ishi
          AND o2.mispar_ishi=  p_mispar_ishi
          AND o1.mispar_ishi = o.mispar_ishi
          AND o2.mispar_ishi = o.mispar_ishi
          AND s.kod_hevra = o.kod_hevra
          AND y.kod_hevra = o.kod_hevra;

          p_teur_unit:=v_teur_unit;
          p_teur_snif:=v_teur_snif;
      EXCEPTION
        WHEN NO_DATA_FOUND THEN
            p_teur_unit:= '';
            p_teur_snif := '';
        WHEN OTHERS THEN
		RAISE;
END pro_get_oved_snif_unit;

PROCEDURE pro_get_oved_cards(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,p_month IN VARCHAR2,p_cur OUT CurType)
IS
 p_taarich date;
BEGIN
    p_taarich:=to_date('01/' || p_month,'dd/mm/yyyy');
    --get oved card for month
    OPEN p_cur FOR
    SELECT DISTINCT  TO_CHAR(y.taarich,'dd/mm/yyyy')  || ' ' || (CASE TO_CHAR(y.taarich,'d') WHEN '1' THEN 'א'
                                             WHEN '2' THEN 'ב'
                                             WHEN '3' THEN 'ג'
                                             WHEN '4' THEN 'ד'
                                             WHEN '5' THEN 'ה'
                                             WHEN '6' THEN 'ו'
                                             WHEN '7' THEN 'ש' END)  || ' ' || c.teur_yom taarich,
		-- DECODE (y.SHGIOT_LETEZUGA_LAOVED,1,'עדכן')     status,
          pkg_ovdim.fun_get_status_lekartis(y.SHGIOT_LETEZUGA_LAOVED,y.mispar_ishi,y.taarich) status,
           me.teur_measher_o_mistayeg measher_o_mistayeg_key,
          pkg_ovdim.fun_get_kartis_lelo_peilut(y.mispar_ishi,y.taarich)  kartis_without_peilut ,
            --DECODE(NVL(so.mispar_sidur,0),0,'+','') kartis_without_peilut ,
		     y.taarich sDate,
			DECODE (r.KOD_STATUS_ISHUR  ,0,'+','') status_tipul_key
    FROM TB_YAMEY_AVODA_OVDIM y,CTB_MEASHER_O_MISTAYEG me,
         TB_YAMIM_MEYUCHADIM b, CTB_SUGEY_YAMIM_MEYUCHADIM c,
         TB_SIDURIM_OVDIM so ,   (SELECT DISTINCT r.taarich,r.mispar_ishi,DECODE(r.KOD_STATUS_ISHUR,NULL,NULL,0) KOD_STATUS_ISHUR FROM   TB_ISHURIM r WHERE r.KOD_STATUS_ISHUR IN(0,3) ) r
    WHERE y.mispar_ishi=p_mispar_ishi
          AND so.mispar_ishi(+)=y.mispar_ishi
          AND so.taarich(+) = y.taarich
          AND y.taarich between p_taarich and last_day(p_taarich)   --TO_CHAR(y.taarich,'mm/yyyy') =p_month
          AND y.taarich= b.taarich(+)
          AND b.sug_yom= c.sug_yom(+)
		 -- and (r.KOD_STATUS_ISHUR  in (0,3) or r.KOD_STATUS_ISHUR  is null)
      --    AND y.status = k.kod_status_kartis(+)
          AND NVL(y.measher_o_mistayeg,2) = me.kod_measher_o_mistayeg(+)
         AND  y.mispar_ishi = r.mispar_ishi(+)
		AND y.taarich = r.taarich(+)
ORDER BY sDate DESC;


    EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END pro_get_oved_cards;

FUNCTION fun_get_status_lekartis(p_SHGIOT_LETEZUGA_LAOVED number,p_mispar_ishi number,p_taarich DATE) return varchar
IS 
    p_cur  CurType;
    simun varchar(5);
    icount number;
BEGIN
   simun:='';     
    if (p_SHGIOT_LETEZUGA_LAOVED =1) THEN

      icount:=   pkg_ovdim.func_is_card_last_updated(p_mispar_ishi,p_taarich);
     
     if (icount=0) then
            simun:='עדכן';
         end if;
   end if;
      return simun;
END fun_get_status_lekartis;
FUNCTION fun_get_kartis_lelo_peilut(p_mispar_ishi number,p_taarich DATE) return varchar
IS 
    cntSidurim number;
    simun varchar(1);
BEGIN
    simun:='';
    select count (*) into cntSidurim
     from tb_sidurim_ovdim s 
     where S.MISPAR_ISHI=p_mispar_ishi and S.TAARICH = p_taarich;
    
    if (cntSidurim =0) THEN
     simun :='+';
    else if (cntSidurim =1) THEN
         select count (*) into cntSidurim from tb_sidurim_ovdim s where S.MISPAR_ISHI=p_mispar_ishi and S.TAARICH = p_taarich and S.MISPAR_SIDUR=99200 ;
        if (cntSidurim =1) THEN
             simun :='+';
         end if;
      end if;
    end if;    
    
    return simun;
END fun_get_kartis_lelo_peilut;
PROCEDURE pro_get_oved_cards_in_tipul(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,p_cur OUT CurType)
IS
BEGIN
    --get oved cards that status tipul is not 1, still in tipul
    OPEN p_cur FOR
    SELECT DISTINCT  TO_CHAR(y.taarich,'dd/mm/yyyy')  || ' ' || (CASE TO_CHAR(y.taarich,'d') WHEN '1' THEN 'א'
                                             WHEN '2' THEN 'ב'
                                             WHEN '3' THEN 'ג'
                                             WHEN '4' THEN 'ד'
                                             WHEN '5' THEN 'ה'
                                             WHEN '6' THEN 'ו'
                                             WHEN '7' THEN 'ש' END)  || ' ' || c.teur_yom taarich,
        --   k.teur_status_kartis status,
          pkg_ovdim.fun_get_status_lekartis(y.SHGIOT_LETEZUGA_LAOVED,y.mispar_ishi,y.taarich) status,
       --   DECODE (y.SHGIOT_LETEZUGA_LAOVED,1,'עדכן')    status,
           me.teur_measher_o_mistayeg measher_o_mistayeg_key,
            pkg_ovdim.fun_get_kartis_lelo_peilut(y.mispar_ishi,y.taarich)  kartis_without_peilut ,
           -- DECODE(NVL(so.mispar_sidur,0),0,'+','') kartis_without_peilut,
		    DECODE (r.KOD_STATUS_ISHUR  ,0,'+','') status_tipul_key,
            y.taarich  sDate
    FROM TB_YAMEY_AVODA_OVDIM y,
         CTB_MEASHER_O_MISTAYEG me,TB_SIDURIM_OVDIM so,
         TB_YAMIM_MEYUCHADIM b, CTB_SUGEY_YAMIM_MEYUCHADIM c ,
		 TB_ISHURIM r
    WHERE y.mispar_ishi=p_mispar_ishi
          AND so.mispar_ishi(+)=y.mispar_ishi
          AND so.taarich(+) = y.taarich
          AND y.taarich= b.taarich(+)
          AND b.sug_yom= c.sug_yom(+)
       --   AND y.status = k.kod_status_kartis(+)
          AND NVL(y.measher_o_mistayeg,2) = me.kod_measher_o_mistayeg(+)
		  AND ( (pkg_ovdim.fun_get_status_lekartis(y.SHGIOT_LETEZUGA_LAOVED,y.mispar_ishi,y.taarich) = 'עדכן')
                 OR r.KOD_STATUS_ISHUR=0 
                 OR NVL(so.mispar_sidur,0)=0 
                 OR ( pkg_ovdim.fun_get_kartis_lelo_peilut(y.mispar_ishi,y.taarich) ='+'  ))
		   AND  y.mispar_ishi = r.mispar_ishi(+)
		  AND y.taarich = r.taarich(+)
    ORDER BY y.taarich DESC;


    EXCEPTION
        WHEN OTHERS THEN
		RAISE;
END pro_get_oved_cards_in_tipul;



/*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0       07/06/2009     sari      1. פונקציה המחזירה מאפייני עובד
*/
PROCEDURE pro_get_meafyeney_oved(p_mispar_ishi IN MEAFYENIM_OVDIM.mispar_ishi%TYPE,
		  									   	        p_taarich IN MEAFYENIM_OVDIM.me_taarich%TYPE,
														p_cur OUT CurType) IS
	BEGIN
	DBMS_APPLICATION_INFO.SET_MODULE('pkg_ovdim.pro_get_meafyeney_oved','get meafyenim leoved ');

	 OPEN p_cur FOR
	   		  SELECT  DECODE(m.kod_meafyen,NULL,to_char(b.kod_meafyen),to_char(m.kod_meafyen)) kod_meafyen,DECODE(m.Erech_Mechdal_partany,NULL,'',m.Erech_Mechdal_partany ||   ' (ב.מ.) ') Erech_Mechdal_partany,c.teur_MEAFYEN_BITZUA,
			                 DECODE(b.erech,NULL,'',b.erech  ||   ' (ב.מ. מערכת) ') Erech_Brirat_Mechdal,y.TEUR_YECHIDA_MEAFYEN YECHIDA,
			   		 		 DECODE(m.erech_ishi,NULL,DECODE(m.ERECH_MECHDAL_PARTANY,NULL, b.ME_TAARICH,m.ME_TAARICH),m.ME_TAARICH) ME_TAARICH,
			  				 DECODE(m.erech_ishi,NULL,DECODE(m.ERECH_MECHDAL_PARTANY,NULL, b.ad_TAARICH,m.ad_TAARICH),m.ad_TAARICH) AD_TAARICH,
			    			  DECODE(m.erech_ishi,NULL,DECODE(m.ERECH_MECHDAL_PARTANY,NULL, b.erech ||  ' (ב.מ. מערכת) ' ,m.ERECH_MECHDAL_PARTANY || ' (ב.מ.) ' ),m.erech_ishi || ' (אישי) '  ) Erech_ishi,
							  to_char(TO_NUMBER(DECODE(m.erech_ishi,NULL,DECODE(m.ERECH_MECHDAL_PARTANY,NULL, b.erech ,m.ERECH_MECHDAL_PARTANY ),m.erech_ishi)))  value_erech_ishi,
							  DECODE(m.kod_meafyen,NULL,'2','1') source_meafyen
				FROM PIVOT_MEAFYENIM_OVDIM m,CTB_MEAFYEN_BITZUA c, BREROT_MECHDAL_MEAFYENIM b,CTB_YECHIDAT_MEAFYEN Y
				WHERE TRUNC(p_taarich) BETWEEN m.ME_TAARICH(+) AND NVL(m.AD_TAARICH(+),TO_DATE('01/01/9999','dd/mm/yyyy'))
				 AND   m.MISPAR_ISHI(+)=p_mispar_ishi
				  AND  TRUNC(p_taarich) BETWEEN b.ME_TAARICH AND NVL(b.AD_TAARICH,TO_DATE('01/01/9999','dd/mm/yyyy'))
				   AND b.kod_meafyen=m.kod_meafyen(+)
				   AND c.KOD_MEAFYEN_BITZUA= b.kod_meafyen
				   AND c.YECHIDAT_MEAFYEN = Y.KOD_YECHIDA_MEAFYEN(+);
	
  EXCEPTION
       WHEN OTHERS THEN
				RAISE;
END   pro_get_meafyeney_oved;

PROCEDURE pro_get_meafyeney_oved_all(p_mispar_ishi IN MEAFYENIM_OVDIM.mispar_ishi%TYPE,
		  									   	        p_me_taarich IN MEAFYENIM_OVDIM.me_taarich%TYPE,
														 p_ad_taarich IN MEAFYENIM_OVDIM.me_taarich%TYPE,
														p_brerat_Mechadal  IN NUMBER,
														p_cur OUT CurType) IS
	BEGIN
	IF p_brerat_Mechadal =1 THEN
	 OPEN p_cur FOR
       WITH tbIshi AS
             ( 
             select h.kod_meafyen,h.ME_TAARICH,h.ad_TAARICH,
              h.TEUR_MEAFYEN_BITZUA, h.YECHIDA,
               h.value_erech_ishi,h.Erech_ishi, h.Erech_Mechdal_partany
               , nvl( LEAD (h.ME_TAARICH,1)
                  OVER(partition by h.kod_meafyen order by h.kod_meafyen,h.ME_TAARICH asc),  last_day(  h.taarich)+1 ) next_hour_me
                  , nvl( LAG (h.AD_TAARICH,1)
                OVER(partition by h.kod_meafyen order by h.kod_meafyen,h.ME_TAARICH asc) ,h.taarich-1  ) prev_hour_ad
              from
             (SELECT to_char(m.kod_meafyen) kod_meafyen, p_me_taarich taarich,
                          case when m.ME_TAARICH between p_me_taarich and  p_ad_taarich  then m.ME_TAARICH else   p_me_taarich  end ME_TAARICH,
                          case when NVL( m.ad_TAARICH,TO_DATE('31/12/4712' ,'dd/mm/yyyy'))  between p_me_taarich and  p_ad_taarich  then m.ad_TAARICH else p_ad_taarich   end ad_TAARICH,
                          C.TEUR_MEAFYEN_BITZUA,
                          y.TEUR_YECHIDA_MEAFYEN YECHIDA,
                          to_char(to_number(DECODE(m.erech_ishi,NULL,m.ERECH_MECHDAL_PARTANY,to_char(m.erech_ishi)))) value_erech_ishi,
                          DECODE(m.erech_ishi,NULL ,m.ERECH_MECHDAL_PARTANY || ' (ב.מ.) ',m.erech_ishi || ' (אישי) '  ) Erech_ishi,
                           DECODE(m.Erech_Mechdal_partany,NULL,'',m.Erech_Mechdal_partany ||   ' (ב.מ.) ') Erech_Mechdal_partany
               FROM   CTB_MEAFYEN_BITZUA c,   CTB_YECHIDAT_MEAFYEN Y, PIVOT_MEAFYENIM_OVDIM m
               WHERE   ( p_me_taarich   BETWEEN  m.ME_TAARICH  AND   NVL(m.ad_TAARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                              OR p_ad_taarich BETWEEN  m.ME_TAARICH  AND   NVL(m.ad_TAARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                              OR   m.ME_TAARICH>=p_me_taarich AND   NVL(m.ad_TAARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=p_ad_taarich   )
                            AND m.MISPAR_ISHI=p_mispar_ishi
                            AND c.KOD_MEAFYEN_BITZUA= m.kod_meafyen
                            AND c.YECHIDAT_MEAFYEN = Y.KOD_YECHIDA_MEAFYEN(+)
                         --   and s.mispar_ishi=75933
              order by  m.kod_meafyen,m.ME_TAARICH ) h
               order by  h.kod_meafyen,h.ME_TAARICH      )
   
   select  to_char(h.kod_meafyen) kod_meafyen, 
                      h.Erech_Mechdal_partany,
                      h.ME_TAARICH,h.AD_TAARICH,
                      h.TEUR_MEAFYEN_BITZUA, h.YECHIDA,
                      h.Erech_ishi,
                      h.value_erech_ishi,
                      h.Erech_Brirat_Mechdal,
                      h.source_meafyen
             from(
                    select  s.kod_meafyen, (s.AD_TAARICH+1) ME_TAARICH, (next_hour_me-1) AD_TAARICH, 
                      s.TEUR_MEAFYEN_BITZUA, s.YECHIDA,
                    '' Erech_Mechdal_partany,
                     m.erech ||  ' (ב.מ. מערכת) '  Erech_ishi,
                       to_char(m.erech) value_erech_ishi,
                         DECODE(m.erech,NULL,'',m.erech  ||   ' (ב.מ. מערכת) ') Erech_Brirat_Mechdal,
                            '1' source_meafyen
                    from  tbIshi s, brerot_mechdal_meafyenim m
                    where (s.AD_TAARICH+1)<next_hour_me
                    and s.kod_meafyen = m.kod_meafyen
                
                union 

                    select s.kod_meafyen, (s.prev_hour_ad+1) ME_TAARICH, (s.ME_TAARICH -1) AD_TAARICH,
                      s.TEUR_MEAFYEN_BITZUA, s.YECHIDA,
                    '' Erech_Mechdal_partany,
                     m.erech ||  ' (ב.מ. מערכת) '  Erech_ishi,
                      to_char(m.erech) value_erech_ishi,
                        DECODE(m.erech,NULL,'',m.erech  ||   ' (ב.מ. מערכת) ') Erech_Brirat_Mechdal,
                      '1' source_meafyen
                    from  tbIshi s, brerot_mechdal_meafyenim m
                    where (s.prev_hour_ad+1)<s.ME_TAARICH
                    and s.kod_meafyen = m.kod_meafyen

                union

                    select s.kod_meafyen,s.ME_TAARICH,s.AD_TAARICH,
                      s.TEUR_MEAFYEN_BITZUA, s.YECHIDA,
                    s.Erech_Mechdal_partany,
                    s.Erech_ishi,
                    s.value_erech_ishi,
                    DECODE(df.erech,NULL,'',df.erech  ||   ' (ב.מ. מערכת) ') Erech_Brirat_Mechdal,  
                     '2' source_meafyen
                    from  tbIshi s, brerot_mechdal_meafyenim df
                    where  s.kod_meafyen=df.kod_meafyen

                union

                    SELECT     to_char(df.KOD_MEAFYEN) KOD_MEAFYEN, p_me_taarich me_taarich ,p_ad_taarich ad_taarich,
                                   C.TEUR_MEAFYEN_BITZUA,y.TEUR_YECHIDA_MEAFYEN YECHIDA,
                                    '' Erech_Mechdal_partany,
                                    df.erech ||  ' (ב.מ. מערכת) '  Erech_ishi,
                                    to_char(df.erech) value_erech_ishi,
                                      DECODE(df.erech,NULL,'',df.erech  ||   ' (ב.מ. מערכת) ') Erech_Brirat_Mechdal,
                                     '1' source_meafyen
                    FROM  ctb_meafyen_bitzua c, brerot_mechdal_meafyenim df, CTB_YECHIDAT_MEAFYEN Y
                    where  df.kod_meafyen not in (select   sh.kod_meafyen 
                                                                from  tbIshi sh)
                            and df.kod_meafyen = c.kod_meafyen_bitzua 
                            and c.YECHIDAT_MEAFYEN = Y.KOD_YECHIDA_MEAFYEN(+)                                                                                                                                                                  
                 ) h
            order by to_number( h.kod_meafyen), h.ME_TAARICH ;

	   		 /* SELECT  DECODE(m.kod_meafyen,NULL,to_char(b.kod_meafyen),to_char(m.kod_meafyen)) kod_meafyen,DECODE(m.Erech_Mechdal_partany,NULL,'',m.Erech_Mechdal_partany ||   ' (ב.מ.) ') Erech_Mechdal_partany,c.teur_MEAFYEN_BITZUA,
			                 DECODE(b.erech,NULL,'',b.erech  ||   ' (ב.מ. מערכת) ') Erech_Brirat_Mechdal,y.TEUR_YECHIDA_MEAFYEN YECHIDA,
			   		 		 DECODE(m.erech_ishi,NULL,DECODE(m.ERECH_MECHDAL_PARTANY,NULL, b.ME_TAARICH,m.ME_TAARICH),m.ME_TAARICH) ME_TAARICH,
			  				NVL( DECODE(m.erech_ishi,NULL,DECODE(m.ERECH_MECHDAL_PARTANY,NULL, b.ad_TAARICH, m.ad_TAARICH ),m.ad_TAARICH),TO_DATE('31/12/4712' ,'dd/mm/yyyy')) AD_TAARICH,
			    			  DECODE(m.erech_ishi,NULL,DECODE(m.ERECH_MECHDAL_PARTANY,NULL, b.erech ||  ' (ב.מ. מערכת) ' ,m.ERECH_MECHDAL_PARTANY || ' (ב.מ.) ' ),m.erech_ishi || ' (אישי) '  ) Erech_ishi,
							 to_char( TO_NUMBER(DECODE(m.erech_ishi,NULL,DECODE(m.ERECH_MECHDAL_PARTANY,NULL, b.erech ,m.ERECH_MECHDAL_PARTANY ),m.erech_ishi)))  value_erech_ishi,
							  DECODE(m.kod_meafyen,NULL,'2','1') source_meafyen
				FROM PIVOT_MEAFYENIM_OVDIM m,CTB_MEAFYEN_BITZUA c, BREROT_MECHDAL_MEAFYENIM b,CTB_YECHIDAT_MEAFYEN Y
				WHERE  (TRUNC(p_me_taarich)  BETWEEN  m.ME_TAARICH(+)  AND   NVL(m.ad_TAARICH(+),TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
					  OR  TRUNC(p_ad_taarich) BETWEEN  m.ME_TAARICH(+)  AND   NVL(m.ad_TAARICH(+),TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
					  OR   m.ME_TAARICH(+)>= TRUNC(p_me_taarich)  AND   NVL(m.ad_TAARICH(+),TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=  TRUNC(p_ad_taarich) )
				 AND   m.MISPAR_ISHI(+)=p_mispar_ishi
				   AND  (TRUNC(p_me_taarich)  BETWEEN  b.ME_TAARICH  AND   NVL(b.ad_TAARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
					  OR  TRUNC(p_ad_taarich) BETWEEN  b.ME_TAARICH  AND   NVL(b.ad_TAARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
					  OR   b.ME_TAARICH>= TRUNC(p_me_taarich)  AND   NVL(b.ad_TAARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=  TRUNC(p_ad_taarich) )
				   AND b.kod_meafyen=m.kod_meafyen(+)
				   AND c.KOD_MEAFYEN_BITZUA= b.kod_meafyen
				   AND c.YECHIDAT_MEAFYEN = Y.KOD_YECHIDA_MEAFYEN(+);*/
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
				FROM PIVOT_MEAFYENIM_OVDIM m,CTB_MEAFYEN_BITZUA c,CTB_YECHIDAT_MEAFYEN Y
				 WHERE   (TRUNC(p_me_taarich)  BETWEEN  m.ME_TAARICH  AND   NVL(m.ad_TAARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
					  OR  TRUNC(p_ad_taarich) BETWEEN  m.ME_TAARICH  AND   NVL(m.ad_TAARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
					  OR   m.ME_TAARICH>= TRUNC(p_me_taarich)  AND   NVL(m.ad_TAARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=  TRUNC(p_ad_taarich) )
				AND   m.MISPAR_ISHI=p_mispar_ishi
				AND c.KOD_MEAFYEN_BITZUA=m.kod_meafyen
				   AND c.YECHIDAT_MEAFYEN =Y.KOD_YECHIDA_MEAFYEN(+);
	END IF;
  EXCEPTION
       WHEN OTHERS THEN
				RAISE;
END   pro_get_meafyeney_oved_all;
/*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0       10/06/2009     sari      1. פונקציה המחזירה היסוטוריה של מאפיין לעובד
*/
PROCEDURE pro_get_historiat_meafyen(p_mispar_ishi IN MEAFYENIM_OVDIM.mispar_ishi%TYPE,
														p_from_taarich IN MEAFYENIM_OVDIM.me_taarich%TYPE,
														p_Code_meafyen  IN MEAFYENIM_OVDIM.KOD_MEAFYEN%TYPE,
														p_cur OUT CurType) IS
	BEGIN
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
				FROM PIVOT_MEAFYENIM_OVDIM m,CTB_MEAFYEN_BITZUA c,CTB_YECHIDAT_MEAFYEN Y
				 WHERE m.AD_TAARICH<= TRUNC(p_from_taarich)
				AND   m.MISPAR_ISHI=p_mispar_ishi
				AND c.KOD_MEAFYEN_BITZUA=m.kod_meafyen
				AND m.KOD_MEAFYEN=p_Code_meafyen
				  AND c.YECHIDAT_MEAFYEN =Y.KOD_YECHIDA_MEAFYEN(+)
				ORDER BY me_taarich DESC;

  EXCEPTION
       WHEN OTHERS THEN
				RAISE;
END   pro_get_historiat_meafyen;

/*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0       07/06/2009     sari      1. םונקציה המחזירה 12 חודשי עבודה אחרונים לעובד
*/
PROCEDURE pro_get_month_year_to_oved(p_mispar_ishi IN TB_YAMEY_AVODA_OVDIM.mispar_ishi%TYPE,
														p_cur OUT CurType) IS
	BEGIN
	  OPEN p_cur FOR
	   	SELECT  TO_CHAR(month_year,'mm/yyyy') month_year FROM
		(SELECT  DISTINCT TO_DATE('01/' || TO_CHAR(taarich,'mm/yyyy'),'dd/mm/yyyy')  month_year
		FROM TB_YAMEY_AVODA_OVDIM
		WHERE mispar_ishi=p_mispar_ishi
		ORDER BY month_year  DESC)
		WHERE ROWNUM<15;
  EXCEPTION
       WHEN OTHERS THEN
				RAISE;
END   pro_get_month_year_to_oved;

/*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0       23/06/2009     sari      1. פונקציה המחזירה סטטוס לעובד
*/
PROCEDURE pro_get_status_oved(p_mispar_ishi IN MATZAV_OVDIM.mispar_ishi%TYPE,
														p_cur OUT CurType) IS
	BEGIN
	  OPEN p_cur FOR
	   	   SELECT m.KOD_MATZAV ,s.TEUR_STATUS, m.Taarich_hatchala,m.Taarich_siyum
			 FROM MATZAV_OVDIM  m,CTB_STATUS s,OVDIM o
			 WHERE m.mispar_ishi=p_mispar_ishi
			 AND m.mispar_ishi=o.mispar_ishi
			AND s.Kod_Status=m.Kod_matzav
			AND o. Kod_Hevra= s. Kod_Hevra
			ORDER BY m.Taarich_hatchala DESC;

  EXCEPTION
       WHEN OTHERS THEN
				RAISE;
END   pro_get_status_oved;

/*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        12/05/2009     sari      1. מביא פרטי עובד
*/
 PROCEDURE pro_get_pirtey_oved_all(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
 		   										 						    p_taarich IN DATE,
 		   									 							p_cur OUT CurType) IS
		v_date_from DATE;

BEGIN
	 v_date_from:=TO_DATE('01/' || TO_CHAR(p_taarich,'mm/yyyy'),'dd/mm/yyyy');

	 OPEN p_cur FOR
		 SELECT o.mispar_ishi,h.TEUR_NATUN,DECODE(K.kod ,NULL,p.ERECH,K.kod)kod_erech,k.teur teur_erech,
		 		p.ME_TAARICH ,p.ad_TAARICH ,k.kod_natun,TO_CHAR(p_taarich,'mm/yyyy') month_year
		 FROM   viw_kod_natun k,
						PIRTEY_OVDIM p,
						OVDIM o,
						CTB_NATUN_HR h
			WHERE   p.Mispar_Ishi=o.mispar_ishi
			AND p.KOD_NATUN=K.kod_natun
			AND p.Mispar_Ishi=p_mispar_ishi
			AND p.KOD_NATUN=p.kod_natun
			AND p.KOD_NATUN=h.KOD_NATUN
			 AND (v_date_from BETWEEN  p.ME_TAARICH  AND   NVL(p.ad_TAARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
			 OR  TRUNC(p_taarich) BETWEEN  p.ME_TAARICH  AND   NVL(p.ad_TAARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
			 OR   p.ME_TAARICH>=v_date_from  AND   NVL(p.ad_TAARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<= TRUNC(p_taarich))
			AND (p.ERECH=K.kod OR  K.kod IS NULL)
			AND  (o. Kod_Hevra= k.Kod_Hevra OR  k.Kod_Hevra IS NULL);

EXCEPTION
       WHEN OTHERS THEN
            RAISE;

END  pro_get_pirtey_oved_all;

/*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0       28/06/2009     sari      1. פונקציה המחזירה היסטוריית נתון
*/
	PROCEDURE pro_get_historiat_natun(p_mispar_ishi IN PIRTEY_OVDIM.mispar_ishi%TYPE,
														p_from_taarich IN PIRTEY_OVDIM.me_taarich%TYPE,
														p_Code_natun  IN PIRTEY_OVDIM.KOD_NATUN%TYPE,
														p_cur OUT CurType) IS
	BEGIN

		OPEN p_cur FOR
		SELECT p.mispar_ishi,DECODE(K.kod ,NULL,p.ERECH,K.kod)kod_erech,k.teur teur_erech,
		 		p.ME_TAARICH ,p.ad_TAARICH ,k.kod_natun
			FROM PIRTEY_OVDIM p,viw_kod_natun k,OVDIM o
				 WHERE p.AD_TAARICH<= TRUNC(p_from_taarich)
				AND   p.MISPAR_ISHI=p_mispar_ishi
				AND  p.Mispar_Ishi=o.mispar_ishi
				AND p.KOD_NATUN=k.KOD_NATUN
				AND p.KOD_NATUN=p_Code_natun
				AND (p.ERECH=k.kod OR  k.kod IS NULL)
				AND  (o. Kod_Hevra= k.Kod_Hevra OR k.Kod_Hevra IS NULL)
				ORDER BY me_taarich DESC;

  EXCEPTION
       WHEN OTHERS THEN
				RAISE;
END   pro_get_historiat_natun;

/*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        13/05/2009     sari      1. פונקציה המחזירה את ריצות החישוב לעובד
*/
 PROCEDURE pro_get_ritzot_chishuv(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
								 				p_taarich IN DATE,
												p_cur OUT CurType) IS
		v_first_bakasha NUMBER;
BEGIN
	 BEGIN
		 	 SELECT DISTINCT b.Bakasha_ID INTO v_first_bakasha
			FROM TB_CHISHUV_CHODESH_OVDIM  C, TB_BAKASHOT B
			WHERE C.Bakasha_ID=B.Bakasha_ID
			AND C.Mispar_Ishi =p_mispar_ishi
			AND C.Taarich =p_taarich
			AND B.Huavra_Lesachar=1
			AND B.Zman_Hatchala =(SELECT MIN (Zman_Hatchala)
														FROM TB_CHISHUV_CHODESH_OVDIM C2, TB_BAKASHOT B2
														WHERE C2.Bakasha_ID=B2.Bakasha_ID
														AND C2.Mispar_Ishi =p_mispar_ishi
														AND C2.Taarich =p_taarich
														AND B2.Huavra_Lesachar=1);

	EXCEPTION
	WHEN NO_DATA_FOUND THEN
		  v_first_bakasha:=0;
	  WHEN OTHERS THEN
            RAISE;
	END;

	OPEN p_cur FOR
		 SELECT DISTINCT TB.Bakasha_ID code_bakasha,TB.Zman_Hatchala,
		  TB.Bakasha_ID || ' - '||  SUBSTR(TB.Teur,0,15)  || '  ('||  TO_CHAR( TB.Zman_Hatchala,'dd.mm.yyyy')  || ') - ' ||  DECODE(TB.Bakasha_ID, v_first_bakasha,'רגיל','הפרשים')  Teur_bakasha,
		  TB.Bakasha_ID || ' - '||  TB.Teur  || '  ('||  TO_CHAR( TB.Zman_Hatchala,'dd.mm.yyyy')  || ') - ' ||  DECODE(TB.Bakasha_ID, v_first_bakasha,'רגיל','הפרשים')  Teur_bakasha_full
		FROM TB_BAKASHOT TB, TB_CHISHUV_CHODESH_OVDIM TC
		WHERE TB. Bakasha_ID=TC. Bakasha_ID
		AND TB.Huavra_Lesachar = 1
		AND TC.Mispar_Ishi =p_mispar_ishi
		AND TC.Taarich =p_taarich
		ORDER BY TB.Zman_Hatchala;

EXCEPTION
	  WHEN OTHERS THEN
            RAISE;

END  pro_get_ritzot_chishuv;

/*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        01/07/2009     sari      1. מביא  שעות מעל מכסה לעובד
*/
 PROCEDURE pro_get_shaot_meal_michsa(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
 		   										 						  p_taarich IN DATE,
																		  p_bakasha IN TB_CHISHUV_CHODESH_OVDIM.Bakasha_ID%TYPE,
 		   									 							p_cur OUT CurType) IS
BEGIN
	 OPEN p_cur FOR
		 SELECT  c.Kod_Rechiv,c.ERECH_RECHIV
		 		/* case  c.kod_rechiv
	  			     when 76 then (select sum(y.erech_rechiv) from  TB_Chishuv_chodesh_Ovdim  y
											Where y. Mispar_Ishi=p_mispar_ishi
											and  to_char(c.Taarich,'mm/yyyy') =to_char(p_taarich,'mm/yyyy')
											and y.Erech_Rechiv is not Null
											and y.Bakasha_ID= p_bakasha
											and y.kod_rechiv=119)
								 when 77 then (select sum(y.erech_rechiv) from TB_Chishuv_chodesh_Ovdim y
											Where y. Mispar_Ishi=p_mispar_ishi
											and  to_char(c.Taarich,'mm/yyyy') =to_char(p_taarich,'mm/yyyy')
											and y.Erech_Rechiv is not Null
											and y.Bakasha_ID= p_bakasha
											and y.kod_rechiv=120)
									when 79 then (select sum(y.erech_rechiv) from TB_Chishuv_chodesh_Ovdim  y
											Where y. Mispar_Ishi=p_mispar_ishi
											and  to_char(c.Taarich,'mm/yyyy') =to_char(p_taarich,'mm/yyyy')
											and y.Erech_Rechiv is not Null
											and y.Bakasha_ID= p_bakasha
											and y.kod_rechiv=122 ) end  as kizuz_meal_michsa*/
		FROM TB_CHISHUV_CHODESH_OVDIM C
						WHERE C.Mispar_Ishi(+)=p_mispar_ishi
						AND TO_CHAR(c.Taarich(+),'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy')
						AND c.kod_rechiv IN(119,120,121,143,147,160,161,163,253,254,255)
						AND C.Bakasha_ID(+)= p_bakasha;
EXCEPTION
       WHEN OTHERS THEN
            RAISE;

END  pro_get_shaot_meal_michsa;

/*Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        01/07/2009      sari       1.  הוספת רשומה לטבלת בקשות מחוץ למכסה
*/
  PROCEDURE pro_ins_bakasha_mechutz_lemich(p_mispar_ishi  IN TB_BAKASHOT_MICHUTZ_LAMICHSA.mispar_ishi%TYPE,
																			  p_taarich  IN TB_BAKASHOT_MICHUTZ_LAMICHSA.taarich%TYPE,
																			  p_shaot  IN TB_BAKASHOT_MICHUTZ_LAMICHSA.mevukash%TYPE,
																			  p_siba  IN TB_BAKASHOT_MICHUTZ_LAMICHSA.sibat_habakasha%TYPE,
																			  p_user_id   IN TB_BAKASHOT_MICHUTZ_LAMICHSA.mispar_ishi%TYPE) IS
  BEGIN
  	   		INSERT INTO TB_BAKASHOT_MICHUTZ_LAMICHSA
			                   ( MISPAR_ISHI ,TAARICH  ,MEVUKASH, SIBAT_HABAKASHA ,TAARICH_IDKUN_ACHARON , MEADKEN_ACHARON)
			VALUES (p_mispar_ishi,p_taarich,p_shaot,p_siba,SYSDATE,p_user_id);

      EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;
  END pro_ins_bakasha_mechutz_lemich;


PROCEDURE Pro_Get_Hour_Approval(p_TypeDemand INTEGER,
                                                             p_mispar_ishi IN OVDIM.mispar_ishi% TYPE,
                                                             p_Month VARCHAR2,
                                                             p_StatusIsuk INTEGER,
                                                             p_Filter IN VARCHAR2,
                                                             p_cur OUT CurType) IS
      strSql VARCHAR2(5000);
      TypeDemandCondition VARCHAR2(100);
      IsukCondition VARCHAR2(3000);
      strSqlEtsNiuli VARCHAR2(1000);
      EtsNiuliCondition VARCHAR2(2000);
      Status_Isuk INTEGER;
      strSqlAll VARCHAR2(15000);
       v_FirstInMonthDate DATE ;
      v_LastInMonthDate DATE ;
      countRec  NUMBER ;
  BEGIN
    IF (p_Month IS NULL ) THEN
      v_FirstInMonthDate := TO_DATE('30/12/1899','dd/mm/yyyy');
      v_LastInMonthDate := TO_DATE('30/12/4712','dd/mm/yyyy');
    ELSE
      v_FirstInMonthDate := TO_DATE('01/' || p_Month,'dd/mm/yyyy'); /* period= 05/2009=>  v_MinLimitDate = 01/05/2009 */
      v_LastInMonthDate := ADD_MONTHS(v_FirstInMonthDate,1) -1 ;    /* period= 05/2009=>  v_MaxLimitDate = 31/05/2009 */
    END IF ;
        IF (p_StatusIsuk= 4) THEN
            strSql  :=  strSql   || ' Select rama3.kod_status_ishur,rama3.Rama,' ;
      ELSE
            strSql  :=  strSql   || 'Select rama2.kod_status_ishur,rama2.Rama,' ;
      END IF ;
        strSql  :=  strSql   || 'rama1.mispar_ishi , 
            Shem,
            Yechida.TEUR_YECHIDA Agaf , 
            REPLACE(TEUR_SNIF_AV, ''סניף'','''')  snif_av,
            TEUR_MAAMAD_HR maamad,
            teur_isuk isuk ,
            rama2.TAARICH ,
            rama1.erech_mevukash Mevukash,
            rama1.siba,
            rama1.erech_meushar Meushar_MenahelYashir,
            rama2.erech_meushar Meushar_Agafit ,
            rama3.erech_mevukash UavarLeIshurVaad, 
            rama3.erech_meushar UsharAlVaad,
             Pkg_Ovdim.fun_get_BakashaId(  rama1.mispar_ishi,   rama2.TAARICH) Bakasha_ID, 
            rama2.KOD_ISHUR ,
            rama2.MISPAR_SIDUR,
            rama2.SHAT_HATCHALA,
            rama2.SHAT_YETZIA,
            rama2.MISPAR_KNISA,';
        IF (p_StatusIsuk= 4) THEN
            strSql  :=  strSql   || 'rama3.kod_status_ishur OriginalStatusIshur ' ;
      ELSE
            strSql  :=  strSql   || 'rama2.kod_status_ishur OriginalStatusIshur ' ;
      END IF ;
            
             strSql  :=  strSql   || ' FROM   (select  (shem_mish|| '' '' ||  shem_prat) as Shem,kod_hevra,mispar_ishi from  ovdim ) Ov,
            TB_ISHURIM rama1,           
            TB_ISHURIM rama2           ,
            TB_ISHURIM rama3           ,
            PIVOT_PIRTEY_OVDIM  Details, 
            CTB_MAAMAD Maamad,  
            CTB_SNIF_AV Snif,
            CTB_ISUK Isuk,
            CTB_YECHIDA Yechida  ,
            (SELECT po.mispar_ishi,MAX(po.ME_TARICH) me_taarich
                       FROM PIVOT_PIRTEY_OVDIM PO
                       WHERE po.isuk IS NOT NULL
                             AND (  ''' || v_FirstInMonthDate || '''  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE(''01/01/9999'' ,''dd/mm/yyyy''))
                               OR    ''' || v_LastInMonthDate || ''' BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE(''01/01/9999'' ,''dd/mm/yyyy''))
                                OR   po.ME_TARICH>=  ''' || v_FirstInMonthDate || ''' AND  NVL(po.ad_TARICH,TO_DATE(''01/01/9999'' ,''dd/mm/yyyy''))<=  ''' || v_LastInMonthDate || ''' )
                      GROUP BY po.mispar_ishi) RelevantDetails '  ||
           ' where  Ov.mispar_ishi = rama1.mispar_ishi
                AND Ov.kod_hevra = Yechida.kod_hevra 
                AND Ov.kod_hevra = MAAMAD.KOD_HEVRA 
                AND Yechida.kod_hevra = Isuk.kod_hevra
                AND Yechida.KOD_YECHIDA = Details.Agaf
                AND rama1.rama = 1 
                AND rama1.kod_ishur =35
                AND rama2.rama = 2 
                AND rama1.mispar_ishi = rama2.mispar_ishi
                AND rama1.taarich =rama2.taarich
                AND rama1.kod_ishur =rama2.kod_ishur
                AND rama1.SHAT_HATCHALA =rama2.SHAT_HATCHALA
                AND rama1.SHAT_YETZIA =rama2.SHAT_YETZIA
                AND rama1.mispar_knisa =rama2.mispar_knisa
                AND rama1.mispar_sidur =rama2.mispar_sidur
                AND rama1.mispar_ishi = rama3.mispar_ishi(+) 
                AND rama1.taarich =rama3.taarich(+)
                AND rama1.kod_ishur =rama3.kod_ishur(+)
                AND rama1.SHAT_HATCHALA =rama3.SHAT_HATCHALA(+)
                AND rama1.SHAT_YETZIA =rama3.SHAT_YETZIA(+)
                AND rama1.mispar_knisa =rama3.mispar_knisa(+)
                AND rama1.mispar_sidur =rama3.mispar_sidur(+)
                AND ( rama2.taarich BETWEEN ''' || v_FirstInMonthDate || ''' AND ''' || v_LastInMonthDate || ''')
                AND  Details.mispar_ishi = rama1.mispar_ishi 
                AND Details.isuk            = Isuk.kod_isuk
                AND Details.snif_av       = Snif.kod_snif_av   
                AND Details.maamad     = maamad.kod_maamad_hr  
                AND Details.mispar_ishi = RelevantDetails.mispar_ishi 
                AND  Details.ME_TARICH = RelevantDetails.me_taarich ' ;
                
        -- בחירת עובדים שנמצאים בעץ ניהולי של מנהל בעל מספר אישי p_mispar_ishi
                
        IF (p_StatusIsuk <> 4) THEN -- ועדת פנים
          TypeDemandCondition := CASE (p_TypeDemand) WHEN 0 THEN  '(rama2.kod_status_ishur= 0 or rama2.kod_status_ishur is null)' ELSE  '(rama2.kod_status_ishur<> 0)' END ;
            strSql  :=  strSql || ' and rama3.rama(+) = 3 and '  || TypeDemandCondition || ' and ( rama2.mispar_ishi  in (
            SELECT DISTINCT a.mispar_ishi                FROM (SELECT * FROM EZ_NIHULY e,
            (SELECT Mispar_Ishi,erech FROM PIRTEY_OVDIM WHERE  TRUNC(SYSDATE)>= Me_taarich AND TRUNC(SYSDATE)<= Ad_taarich AND  Kod_Natun=1) p
            WHERE p.erech=e.yechida_mekorit) a
            CONNECT BY a.YECHIDAT_ABA  = PRIOR a.yechida_mekorit  START WITH a.YECHIDAT_ABA  =(SELECT Erech FROM PIRTEY_OVDIM
            WHERE Mispar_Ishi=' || p_mispar_ishi ||  ' AND TRUNC(SYSDATE)>= Me_taarich AND TRUNC(SYSDATE)<= Ad_taarich AND  Kod_Natun=1))) '  ;
        ELSE 
            TypeDemandCondition := CASE (p_TypeDemand) WHEN 0 THEN  '(rama3.kod_status_ishur= 0 or rama3.kod_status_ishur is null)' ELSE  '(rama3.kod_status_ishur<> 0)' END ;
            strSql  :=  strSql || 'and rama3.rama = 3  and '  || TypeDemandCondition ;
        END IF ;
                
        IF (p_Filter IS NOT NULL ) THEN -- ועדת פנים
            strSql  :=  strSql ||REPLACE(p_Filter,'mispar_ishi','rama1.mispar_ishi') ||  ' order by  mispar_ishi asc ,TAARICH desc ';
        ELSE             
           strSql  :=  strSql ||  ' order by  Shem asc ,TAARICH desc ';
        END IF;

        strSqlAll  := strSql  ;
/*
         execute immediate 'select count(*)   from (' || strSqlAll || ')' into countRec ;
INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'countRec',countRec);
  */       
        OPEN p_Cur FOR  strSqlAll   ;

           DBMS_OUTPUT.PUT_LINE('strSqlAll = ' || strSqlAll);


       EXCEPTION
         WHEN OTHERS THEN
              RAISE;
  END Pro_Get_Hour_Approval;

PROCEDURE pro_getRelevantMonthOfApproval(p_mispar_ishi IN OVDIM.mispar_ishi% TYPE,
                                                             p_cur OUT CurType) IS
  BEGIN
        OPEN p_Cur FOR  SELECT      DISTINCT TO_CHAR(rama2.TAARICH,'MM/YYYY') RelevantMonths
                                 FROM   
                                        TB_ISHURIM rama1,           
                                        TB_ISHURIM rama2           ,
                                        TB_ISHURIM rama3           ,
                                        PIVOT_PIRTEY_OVDIM  Details
                            WHERE  Details.mispar_ishi = rama1.mispar_ishi
                                    AND rama1.rama = 1 
                                    AND rama1.kod_ishur =35
                                    AND rama2.rama = 2 
                                    AND rama1.mispar_ishi = rama2.mispar_ishi
                                    AND rama1.taarich =rama2.taarich
                                    AND rama1.kod_ishur =rama2.kod_ishur
                                    AND rama1.SHAT_HATCHALA =rama2.SHAT_HATCHALA
                                    AND rama1.SHAT_YETZIA =rama2.SHAT_YETZIA
                                    AND rama1.mispar_knisa =rama2.mispar_knisa
                                    AND rama1.mispar_sidur =rama2.mispar_sidur
                                    AND rama1.mispar_ishi = rama3.mispar_ishi(+) 
                                    AND rama1.taarich =rama3.taarich(+)
                                    AND rama1.kod_ishur =rama3.kod_ishur(+)
                                    AND rama1.SHAT_HATCHALA =rama3.SHAT_HATCHALA(+)
                                    AND rama1.SHAT_YETZIA =rama3.SHAT_YETZIA(+)
                                    AND rama1.mispar_knisa =rama3.mispar_knisa(+)
                                    AND rama1.mispar_sidur =rama3.mispar_sidur(+)
                                    AND  Details.mispar_ishi = rama1.mispar_ishi 
                                    AND rama3.rama(+) = 3 
                                    AND (rama2.kod_status_ishur= 0 OR rama2.kod_status_ishur IS NULL) 
                                    AND ( rama2.mispar_ishi  IN (
                                SELECT DISTINCT a.mispar_ishi                FROM (SELECT * FROM EZ_NIHULY e,
                                (SELECT Mispar_Ishi,erech FROM PIRTEY_OVDIM WHERE  TRUNC(SYSDATE)>= Me_taarich AND TRUNC(SYSDATE)<= Ad_taarich AND  Kod_Natun=1) p
                                WHERE p.erech=e.yechida_mekorit) a
                                CONNECT BY a.YECHIDAT_ABA  = PRIOR a.yechida_mekorit  START WITH a.YECHIDAT_ABA  =(SELECT Erech FROM PIRTEY_OVDIM
                                WHERE Mispar_Ishi=p_mispar_ishi AND TRUNC(SYSDATE)>= Me_taarich AND TRUNC(SYSDATE)<= Ad_taarich AND  Kod_Natun=1)))   ;


       EXCEPTION
         WHEN OTHERS THEN
              RAISE;
  END pro_getRelevantMonthOfApproval;



PROCEDURE pro_upd_Hour_Aproval(p_Bakasha_ID  IN NUMBER ,
                                                    p_kod_status_ishur IN NUMBER ,  p_MISPAR_ISHI IN  TB_ISHURIM.MISPAR_ISHI% TYPE, 
                                                    p_KOD_ISHUR IN  TB_ISHURIM.kod_ishur% TYPE,
                                                    p_TAARICH IN  TB_ISHURIM.TAARICH%TYPE, 
                                                    p_MISPAR_SIDUR IN  TB_ISHURIM.MISPAR_SIDUR%TYPE, 
                                                    p_SHAT_HATCHALA IN  TB_ISHURIM.SHAT_HATCHALA%TYPE, 
                                                    p_SHAT_YETZIA IN  TB_ISHURIM.SHAT_YETZIA%TYPE, 
                                                    p_MISPAR_KNISA IN  TB_ISHURIM.MISPAR_KNISA%TYPE, 
                                                    p_RAMA IN  TB_ISHURIM.RAMA%TYPE, 
                                                    p_ERECH_MEUSHAR IN  TB_ISHURIM.ERECH_MEUSHAR%TYPE, 
                                                    p_ERECH_MEVUKASH IN TB_ISHURIM.ERECH_MEVUKASH%TYPE ,
                                                    P_SIBA IN TB_ISHURIM.SIBA%TYPE ,
                                                    P_MAINFACTOR NUMBER, 
                                                    P_SECONDARYFACTOR NUMBER ,
                                                    p_Result OUT INTEGER) IS 
Old_Erech_Meushar NUMBER ;
Old_Erech_Mevukash NUMBER ;
Erech_Meushar_Agaf  NUMBER ; 
ApprovalDemandExist  NUMBER;
approve_value NUMBER;
v_FirstInMonthDate DATE ; 
v_LastInMonthDate DATE ; 
Erech_147 NUMBER ;
Erech_143 NUMBER ;
Erech_253 NUMBER ;
AllResult NUMBER ;
KDS_GW_EXCEPTION EXCEPTION ;
BEGIN
        v_FirstInMonthDate := TO_DATE('01/' || TO_CHAR(p_TAARICH,'mm/yyyy'),'DD/MM/YYYY'); /* period= 05/2009=>  v_MinLimitDate = 01/05/2009 */
        v_LastInMonthDate := ADD_MONTHS(v_FirstInMonthDate,1) -1 ;    /* period= 05/2009=>  v_MaxLimitDate = 31/05/2009 */
        
          SELECT I.ERECH_MEUSHAR INTO Erech_Meushar_Agaf 
          FROM TB_ISHURIM I WHERE 
          MISPAR_ISHI = P_MISPAR_ISHI AND 
          KOD_ISHUR = p_kod_ishur AND
          TAARICH = p_TAARICH AND 
          MISPAR_SIDUR = p_MISPAR_SIDUR AND  
          SHAT_HATCHALA = p_SHAT_HATCHALA AND
          SHAT_YETZIA = p_SHAT_YETZIA AND
          MISPAR_KNISA = p_MISPAR_KNISA AND
          RAMA =1;
          Erech_147 :=  Pkg_Utils.fun_GET_Rechiv_Value(p_mispar_ishi,147,v_FirstInMonthDate,v_LastInMonthDate,p_Bakasha_ID);
          Erech_143 := NVL(Pkg_Utils.fun_GET_Rechiv_Value(p_mispar_ishi,143,v_FirstInMonthDate,v_LastInMonthDate,p_Bakasha_ID),0);
          Erech_253 := NVL(Pkg_Utils.fun_GET_Rechiv_Value(p_mispar_ishi,253,v_FirstInMonthDate,v_LastInMonthDate,p_Bakasha_ID),0);

          SELECT I.ERECH_MEUSHAR , I.erech_mevukash  INTO Old_Erech_Meushar,Old_Erech_Mevukash
          FROM TB_ISHURIM I WHERE 
          MISPAR_ISHI = P_MISPAR_ISHI AND 
          KOD_ISHUR = p_kod_ishur AND
          TAARICH = p_TAARICH AND 
          MISPAR_SIDUR = p_MISPAR_SIDUR AND  
          SHAT_HATCHALA = p_SHAT_HATCHALA AND
          SHAT_YETZIA = p_SHAT_YETZIA AND
          MISPAR_KNISA = p_MISPAR_KNISA AND
          RAMA = p_RAMA;
           DBMS_OUTPUT.PUT_LINE('Old_Erech_Meushar = ' || Old_Erech_Meushar);


         SELECT COUNT (MISPAR_ISHI) INTO ApprovalDemandExist FROM TB_ISHURIM        WHERE 
          MISPAR_ISHI = P_MISPAR_ISHI AND 
          KOD_ISHUR = p_kod_ishur AND
          TAARICH = p_TAARICH AND 
          MISPAR_SIDUR = p_MISPAR_SIDUR AND  
          SHAT_HATCHALA = p_SHAT_HATCHALA AND
          SHAT_YETZIA = p_SHAT_YETZIA AND
          MISPAR_KNISA = p_MISPAR_KNISA AND
          RAMA = 3 ; 
           DBMS_OUTPUT.PUT_LINE('ApprovalDemandExist = ' || ApprovalDemandExist);
          IF (p_RAMA = 2) THEN 
              IF (p_kod_status_ishur =1 ) THEN --בקשה אושרה שלמה או חלקית
                     DBMS_OUTPUT.PUT_LINE('p_kod_status_ishur =1   ');
                       AllResult := 2000 ;    
                       IF ( (p_erech_meushar > Erech_147 ) AND ( Erech_147 > 0 )) THEN
                           AllResult := AllResult + 100 ; 
                           Egd_mafienim_New.Tipul_Mafienim@KDS_GW( approve_value,TO_CHAR(p_MISPAR_ISHI),TO_DATE(TO_CHAR(P_TAARICH,'mm/yyyy'),'mm/yyyy'),Erech_147 + Erech_143 ,14) ;
                           AllResult := AllResult + approve_value*10;
                           IF (approve_value<> 0) THEN 
                                RAISE KDS_GW_EXCEPTION ;
                            END IF;
                            Egd_mafienim_New.Tipul_Mafienim@KDS_GW( approve_value, TO_CHAR(p_MISPAR_ISHI),TO_DATE(TO_CHAR(P_TAARICH,'mm/yyyy'),'mm/yyyy'),Erech_253 + (p_erech_meushar- Erech_147) ,13) ;  
                           AllResult := AllResult + approve_value;
                           IF (approve_value<> 0) THEN 
                                RAISE KDS_GW_EXCEPTION ;
                            END IF;
                       ELSIF  (Erech_147 = 0 ) THEN 
                            AllResult := AllResult + 200 ;
                           Egd_mafienim_New.Tipul_Mafienim@KDS_GW( approve_value, TO_CHAR(p_MISPAR_ISHI),TO_DATE(TO_CHAR(P_TAARICH,'mm/yyyy'),'mm/yyyy'), (p_erech_meushar+ Erech_253) ,13) ;  
                           AllResult := AllResult + approve_value*10;
                           IF (approve_value<> 0) THEN 
                                RAISE KDS_GW_EXCEPTION ;
                            END IF;
                       ELSE
                            AllResult := AllResult + 300 ;
                            Egd_mafienim_New.Tipul_Mafienim@KDS_GW (approve_value,  TO_CHAR(p_MISPAR_ISHI),TO_DATE(TO_CHAR(P_TAARICH,'mm/yyyy'),'mm/yyyy'),p_erech_meushar + Erech_143 ,14) ;  
                            AllResult := AllResult + approve_value*10;
                           IF (approve_value<> 0) THEN 
                                RAISE KDS_GW_EXCEPTION ;
                            END IF;
                           Egd_mafienim_New.Tipul_Mafienim@KDS_GW (approve_value,  TO_CHAR(p_MISPAR_ISHI),TO_DATE(TO_CHAR(P_TAARICH,'mm/yyyy'),'mm/yyyy'),Erech_253,13) ;  
                           AllResult := AllResult + approve_value;
                          IF (approve_value<> 0) THEN 
                                RAISE KDS_GW_EXCEPTION ;
                            END IF;
                        END IF ;

                        UPDATE TB_ISHURIM I SET I.ERECH_MEUSHAR = p_ERECH_MEUSHAR, I.KOD_STATUS_ISHUR = 1 WHERE 
                        MISPAR_ISHI = P_MISPAR_ISHI AND KOD_ISHUR = p_kod_ishur AND TAARICH = p_TAARICH 
                        AND MISPAR_SIDUR = p_MISPAR_SIDUR AND  SHAT_HATCHALA = p_SHAT_HATCHALA AND SHAT_YETZIA = p_SHAT_YETZIA 
                        AND MISPAR_KNISA = p_MISPAR_KNISA AND RAMA = p_RAMA;
                       
                            IF  (ApprovalDemandExist > 0)  THEN--הייתה בקשה קודמת ומעדכנים אותה
                                 AllResult := AllResult + 1 ;
                                   DBMS_OUTPUT.PUT_LINE('(ApprovalDemandExist > 0 ) and ( p_kod_status_ishur =1) ');
                                      UPDATE TB_ISHURIM I SET  I.erech_mevukash = p_ERECH_MEVUKASH  WHERE 
                                      MISPAR_ISHI = P_MISPAR_ISHI AND KOD_ISHUR = p_kod_ishur AND TAARICH = p_TAARICH 
                                      AND MISPAR_SIDUR = p_MISPAR_SIDUR AND  SHAT_HATCHALA = p_SHAT_HATCHALA AND SHAT_YETZIA = p_SHAT_YETZIA 
                                      AND MISPAR_KNISA = p_MISPAR_KNISA AND RAMA = 3;
                          ELSIF  (p_ERECH_MEVUKASH >0 ) THEN  
                                    AllResult := AllResult + 2 ;
                                           DBMS_OUTPUT.PUT_LINE('יצירת רשומה חדשה  ');
                                      INSERT INTO TB_ISHURIM (mispar_ishi, kod_ishur, taarich, mispar_sidur, shat_hatchala, shat_yetzia, mispar_knisa, 
                                                        rama, siba, kod_status_ishur, erech_mevukash, taarich_bakashat_ishur, gorem_measher_rashsi, gorem_measher_mishni)  VALUES (
                                                        p_mispar_ishi,p_kod_ishur, p_taarich, p_mispar_sidur, p_shat_hatchala,p_shat_yetzia, p_mispar_knisa, 
                                                        3, p_siba, 0, p_ERECH_MEVUKASH,SYSDATE, 
                                                        CASE WHEN P_MAINFACTOR= 0 THEN NULL ELSE P_MAINFACTOR END  ,CASE WHEN P_SECONDARYFACTOR = 0 THEN NULL ELSE P_SECONDARYFACTOR END  ); 
                            END IF ;
                        --end if ; 
                        p_Result := AllResult ;
              ELSE -- ???? ?? ?????
                     DBMS_OUTPUT.PUT_LINE('????? ????? ??? ?????');
                     p_Result := 2;
                        UPDATE TB_ISHURIM I SET I.KOD_STATUS_ISHUR = 2 , I.ERECH_MEUSHAR = 0    WHERE 
                        MISPAR_ISHI = P_MISPAR_ISHI AND KOD_ISHUR = p_kod_ishur AND TAARICH = p_TAARICH 
                        AND MISPAR_SIDUR = p_MISPAR_SIDUR AND  SHAT_HATCHALA = p_SHAT_HATCHALA AND SHAT_YETZIA = p_SHAT_YETZIA 
                        AND MISPAR_KNISA = p_MISPAR_KNISA AND RAMA = p_RAMA;
              END IF ;
    
              IF ((ApprovalDemandExist > 0) AND 
                  ( (p_kod_status_ishur = 2  )  OR  ( p_ERECH_MEUSHAR = Old_Erech_Mevukash ) )) THEN  -- ???? ???? ???? ?????? ??? ?? ????? ?? ???? ??? 
                     p_Result := 3;
                     DBMS_OUTPUT.PUT_LINE('(ApprovalDemandExist > 0  ( Old_Kod_Status_Ishur =2 or ( p_ERECH_MEVUKASH = p_ERECH_MEUSHAR ))');
                        DELETE TB_ISHURIM I  WHERE 
                        MISPAR_ISHI = P_MISPAR_ISHI AND KOD_ISHUR = p_kod_ishur AND TAARICH = p_TAARICH 
                        AND MISPAR_SIDUR = p_MISPAR_SIDUR AND  SHAT_HATCHALA = p_SHAT_HATCHALA AND SHAT_YETZIA = p_SHAT_YETZIA 
                        AND MISPAR_KNISA = p_MISPAR_KNISA AND RAMA = 3;
              END IF ;
          ELSE -- עדכון מועדת פנים
                    AllResult:= 3000; 
                       IF ( ((p_erech_meushar + Old_Erech_Meushar) > Erech_147 ) AND ( Erech_147 > 0 )) THEN 
                           AllResult := AllResult + 100 ; 
                            Egd_mafienim_New.Tipul_Mafienim@KDS_GW (approve_value,  TO_CHAR(p_MISPAR_ISHI),TO_DATE(TO_CHAR(P_TAARICH,'mm/yyyy'),'mm/yyyy'),Erech_147 + Erech_143 ,14) ;
                           AllResult := AllResult + approve_value*10;
                          IF (approve_value<> 0) THEN 
                                RAISE KDS_GW_EXCEPTION ;
                            END IF;
                           Egd_mafienim_New.Tipul_Mafienim@KDS_GW (approve_value,  TO_CHAR(p_MISPAR_ISHI),TO_DATE(TO_CHAR(P_TAARICH,'mm/yyyy'),'mm/yyyy'),Erech_253 + ((p_erech_meushar+ Old_Erech_Meushar)- Erech_147) ,13) ;  
                           AllResult := AllResult + approve_value;
                          IF (approve_value<> 0) THEN 
                                RAISE KDS_GW_EXCEPTION ;
                            END IF;
                       ELSIF  (Erech_147 = 0 ) THEN 
                            AllResult := AllResult + 200 ;
                           Egd_mafienim_New.Tipul_Mafienim@KDS_GW( approve_value, TO_CHAR(p_MISPAR_ISHI),TO_DATE(TO_CHAR(P_TAARICH,'mm/yyyy'),'mm/yyyy'), (p_erech_meushar+ Erech_253) ,13) ;  
                           AllResult := AllResult + approve_value*10;
                          IF (approve_value<> 0) THEN 
                                RAISE KDS_GW_EXCEPTION ;
                            END IF;
                       ELSE
                            AllResult := AllResult + 300 ;
                           Egd_mafienim_New.Tipul_Mafienim@KDS_GW (approve_value,  TO_CHAR(p_MISPAR_ISHI),TO_DATE(TO_CHAR(P_TAARICH,'mm/yyyy'),'mm/yyyy'),p_erech_meushar + Old_Erech_Meushar + Erech_143 ,14) ;  
                           AllResult := AllResult + approve_value;
                          IF (approve_value<> 0) THEN 
                                RAISE KDS_GW_EXCEPTION ;
                            END IF;
                           Egd_mafienim_New.Tipul_Mafienim@KDS_GW (approve_value,  TO_CHAR(p_MISPAR_ISHI),TO_DATE(TO_CHAR(P_TAARICH,'mm/yyyy'),'mm/yyyy'),Erech_253,13) ;  
                           AllResult := AllResult + approve_value;
                          IF (approve_value<> 0) THEN 
                                RAISE KDS_GW_EXCEPTION ;
                            END IF;
                        END IF ;
                    -- table of update datas 
                    -- select * from egd_mafien_erechim_people_new@KDS_GW 
                     p_Result := AllResult;
                        UPDATE TB_ISHURIM I SET I.ERECH_MEUSHAR = p_ERECH_MEUSHAR, I.KOD_STATUS_ISHUR = p_kod_status_ishur WHERE 
                        MISPAR_ISHI = P_MISPAR_ISHI AND KOD_ISHUR = p_kod_ishur AND TAARICH = p_TAARICH 
                        AND MISPAR_SIDUR = p_MISPAR_SIDUR AND  SHAT_HATCHALA = p_SHAT_HATCHALA AND SHAT_YETZIA = p_SHAT_YETZIA 
                        AND MISPAR_KNISA = p_MISPAR_KNISA AND RAMA = p_RAMA;
          END IF;
          
         EXCEPTION 
             WHEN KDS_GW_EXCEPTION THEN 
                         p_Result := AllResult;
             WHEN OTHERS THEN 
                  RAISE;           
END pro_upd_Hour_Aproval;



    PROCEDURE pro_get_SharedMonthly_Quota(p_mispar_ishi IN OVDIM.mispar_ishi% TYPE,p_Period IN VARCHAR2,
    p_Quota OUT TB_MICHSA_AGAPIT .MICHSA_AGAPIT%TYPE,
    p_SharedQuota OUT INTEGER) IS
  v_MaxLimitDate DATE ;
  v_MinLimitDate DATE ;
  BEGIN
    IF (p_Period IS NULL ) THEN
      v_MinLimitDate  := TO_DATE('30-12-1899','dd/mm/yyyy');
      v_MaxLimitDate := TO_DATE('30-12-2299','dd/mm/yyyy');
    ELSE
      v_MinLimitDate := TO_DATE('01/' || p_Period,'dd/mm/yyyy'); /* period= 05/2009=>  v_MinLimitDate = 01/05/2009 */
      v_MaxLimitDate := ADD_MONTHS(v_MinLimitDate,1) -1 ;    /* period= 05/2009=>  v_MaxLimitDate = 31/05/2009 */
    END IF ;


         SELECT  MICHSA_AGAPIT   INTO p_Quota FROM TB_MICHSA_AGAPIT tb
          WHERE ((tb.Me_Taarich <=  v_MaxLimitDate ) AND (tb.Ad_Taarich >= v_MinLimitDate OR tb.Ad_Taarich IS NULL )) AND kod_agaf IN (
          SELECT DISTINCT yechida_irgunit FROM PIVOT_PIRTEY_OVDIM  o WHERE o.mispar_ishi = p_mispar_ishi
          AND
          (o.me_tarich <=  v_MaxLimitDate ) AND (o.ad_tarich >= v_MinLimitDate OR o.ad_tarich  IS NULL ) )
          AND ROWNUM =1
          ORDER BY Me_Taarich DESC ;
          SELECT NVL(SUM(Erech_Meushar),0) INTO p_SharedQuota
          FROM TB_ISHURIM
          WHERE Rama=2 AND Kod_Ishur =35
          AND TO_CHAR(taarich,'MM/YYYY') = p_period
          AND mispar_ishi IN
          (SELECT DISTINCT a.mispar_ishi                FROM (SELECT * FROM EZ_NIHULY e,
        (SELECT Mispar_Ishi,erech FROM PIRTEY_OVDIM WHERE  TRUNC(SYSDATE)>= Me_taarich AND TRUNC(SYSDATE)<= Ad_taarich AND  Kod_Natun=1) p
        WHERE p.erech=e.yechida_mekorit) a
        CONNECT BY a.YECHIDAT_ABA  = PRIOR a.yechida_mekorit  START WITH a.YECHIDAT_ABA  =(SELECT Erech FROM PIRTEY_OVDIM
        WHERE Mispar_Ishi= p_mispar_ishi  AND TRUNC(SYSDATE)>= Me_taarich AND TRUNC(SYSDATE)<= Ad_taarich AND  Kod_Natun=1))  ;

    EXCEPTION
	WHEN NO_DATA_FOUND THEN
  p_Quota := 0;
  p_SharedQuota := 0 ;
  WHEN OTHERS THEN
            RAISE;
  END pro_get_SharedMonthly_Quota;

    PROCEDURE pro_get_Status_Isuk(p_mispar_ishi IN OVDIM.mispar_ishi% TYPE,p_Form IN INTEGER, p_Month VARCHAR2,p_Result OUT INTEGER) IS
    v_MaxLimitDate DATE ;
  v_MinLimitDate DATE ;
  BEGIN
    IF (p_Month IS NULL ) THEN
      v_MinLimitDate :=SYSDATE;
    ELSE
      v_MinLimitDate := TO_DATE('01/' || p_Month,'dd/mm/yyyy'); /* period= 05/2009=>  v_MinLimitDate = 01/05/2009 */
    END IF ;
      v_MaxLimitDate := ADD_MONTHS(v_MinLimitDate,1) -1 ;    /* period= 05/2009=>  v_MaxLimitDate = 31/05/2009 */

SELECT CASE
          WHEN ((o.isuk IN (11 , 12 , 13 , 36)) AND (o.snif_av IN (72181 , 72184 , 78188 )) ) THEN 1 --אגף פנים/מנהל משרד אגף פנים
          WHEN ((o.isuk IN (11 , 12 , 13 , 36)) AND (o.snif_av NOT IN (72181 , 72184 , 78188 ))) THEN
              CASE WHEN  ( (o.isuk =36) AND (o.snif_av = 80143 ) AND (p_Form =2 ) ) THEN 4 --ועדת פנים
              ELSE 2-- -- מנהלי אגף/מנהלי משרד אגף בלבד
              END
          WHEN (o.isuk NOT IN (11 , 12 , 13 , 36)) THEN 3  --לא מנהלי אגף/מנהלי משרד אגף
          ELSE 0
          END INTO p_Result
FROM PIVOT_PIRTEY_OVDIM O INNER JOIN CTB_ISUK c ON c.kod_isuk= o.isuk
WHERE o.mispar_ishi=p_mispar_ishi
AND  ((o.me_tarich <= TO_DATE(v_MaxLimitDate,'dd/mm/yyyy') ) AND (o.ad_tarich >= TO_DATE(v_MinLimitDate,'dd/mm/yyyy') OR o.ad_tarich IS NULL ))
ORDER BY o.mispar_ishi, o.snif_av;

END pro_get_Status_Isuk;




PROCEDURE pro_get_tmp_pirtey_oved(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE, p_taarich IN DATE,
 		   									 							p_cur OUT CurType) IS
BEGIN
	 OPEN p_cur FOR
	   	SELECT po.*, i.KOD_SECTOR_ISUK
		  FROM PIVOT_PIRTEY_OVDIM PO,CTB_ISUK i,OVDIM o
		WHERE 	po.mispar_ishi=p_mispar_ishi
		AND po.mispar_ishi=o.mispar_ishi
		  AND  TRUNC(p_taarich) BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
		  AND i.Kod_Hevra = o.kod_hevra
		 AND  i.Kod_Isuk =po.isuk;

EXCEPTION
       WHEN OTHERS THEN
            RAISE;

END  pro_get_tmp_pirtey_oved;
FUNCTION fun_get_sug_yechida_oved(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
														p_taarich  IN PIVOT_PIRTEY_OVDIM.me_tarich%TYPE) RETURN CTB_YECHIDA.SUG_YECHIDA%TYPE AS
	v_sug_yechida CTB_YECHIDA.SUG_YECHIDA%TYPE;
	 v_date_from DATE;
BEGIN
	  DBMS_APPLICATION_INFO.SET_MODULE('PKG_OVDIM.fun_get_sug_yechida_oved','get sug yechida leoved  ');
  

	   v_date_from:=TO_DATE('01/' || TO_CHAR(p_taarich,'mm/yyyy'),'dd/mm/yyyy');
		SELECT y.SUG_YECHIDA INTO v_sug_yechida
		 FROM   (SELECT MAX( po.ME_TARICH) me_taarich
					   FROM PIVOT_PIRTEY_OVDIM PO
					     WHERE 	po.mispar_ishi=p_mispar_ishi
					 AND (v_date_from BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
		OR  TRUNC(p_taarich) BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
		OR   po.ME_TARICH>=v_date_from  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<= TRUNC(p_taarich))) po,
		 	  PIVOT_PIRTEY_OVDIM p,OVDIM o,CTB_YECHIDA y
		WHERE o.mispar_ishi=p_mispar_ishi
		AND o.mispar_ishi=p.mispar_ishi
		AND  po.ME_TAARICH= p.ME_TARICH
		AND y.KOD_HEVRA=o.KOD_HEVRA
		AND y.KOD_YECHIDA=p.YECHIDA_IRGUNIT;

		RETURN v_sug_yechida;

		 EXCEPTION
	       WHEN NO_DATA_FOUND THEN
		   		 RETURN '';

END  fun_get_sug_yechida_oved;

PROCEDURE pro_get_merkaz_erua_ByKod (p_mispar_ishi IN PIRTEY_OVDIM.mispar_ishi%TYPE,
                                   p_kod_natun IN PIRTEY_OVDIM.kod_natun%TYPE,
								   p_taarich IN PIRTEY_OVDIM.ME_TAARICH%TYPE,
                                   P_erech OUT PIRTEY_OVDIM.erech%TYPE)   IS

BEGIN

       SELECT e.KOD_MERKAZ_EROA_EZORI     INTO  P_erech
       FROM   PIRTEY_OVDIM p , CTB_MERKAZ_EROA e
		WHERE p.mispar_ishi =p_mispar_ishi AND
			  	     p.kod_natun =p_kod_natun AND
					 p_taarich BETWEEN  p.ME_TAARICH AND NVL(p.AD_TAARICH,TO_DATE('30/12/4712','dd/mm/yyyy')) AND
					 p.ERECH = e.KOD_MERKAZ_EROA ;

EXCEPTION
    WHEN NO_DATA_FOUND THEN
         P_erech := 0;
END pro_get_merkaz_erua_ByKod;

PROCEDURE pro_get_mikum_shaon_in_out(p_mispar_ishi IN TB_SIDURIM_OVDIM.mispar_ishi%TYPE,
                                     p_taarich IN DATE ,
                                     p_mikum_shaon_knisa  OUT INTEGER   ,
                                     p_mikum_shaon_yetzia OUT INTEGER)  IS
BEGIN

SELECT NVL((SELECT y.KOD_MIKUM_YECHIDA_EZORI
					FROM CTB_MIKUM_YECHIDA y,
								(SELECT DECODE (t.rk1,1,MIKUM_SHAON_knisa,NULL) knisa
								  FROM  (
										        SELECT   rank () OVER (ORDER BY SHAT_HATCHALA ASC) rk1 ,
										        rank () OVER (ORDER BY SHAT_HATCHALA DESC ) rk2,
										        MISPAR_ISHI ,MISPAR_SIDUR,SHAT_HATCHALA,SHAT_HATCHALA_LETASHLUM,SHAT_GMAR,SHAT_GMAR_LETASHLUM,MIKUM_SHAON_KNISA,MIKUM_SHAON_YETZIA,taarich
										        FROM TB_SIDURIM_OVDIM
										        WHERE MISPAR_ISHi = p_mispar_ishi
										        AND taarich =TRUNC(p_taarich) ) t
								 WHERE  t.rk1=1 )k
					WHERE SUBSTR(k.knisa,0,3) = y.KOD_MIKUM_YECHIDA),0) KNISA ,
		     NVL((SELECT y.KOD_MIKUM_YECHIDA_EZORI
					FROM  CTB_MIKUM_YECHIDA y,
							  (SELECT DECODE (t.rk2,1,MIKUM_SHAON_YETZIA,NULL) yezia
							   FROM (
									        SELECT   rank () OVER (ORDER BY SHAT_HATCHALA ASC) rk1 ,
									        rank () OVER (ORDER BY SHAT_HATCHALA DESC ) rk2,
									        MISPAR_ISHI ,MISPAR_SIDUR,SHAT_HATCHALA,SHAT_HATCHALA_LETASHLUM,SHAT_GMAR,SHAT_GMAR_LETASHLUM,MIKUM_SHAON_KNISA,MIKUM_SHAON_YETZIA,taarich
									        FROM TB_SIDURIM_OVDIM
									        WHERE MISPAR_ISHi = p_mispar_ishi
									        AND taarich = TRUNC(p_taarich)  ) t
									        WHERE  t.rk2=1)k
							    WHERE SUBSTR(k.yezia,0,3) = y.KOD_MIKUM_YECHIDA),0) YEZIA
INTO  p_mikum_shaon_knisa, p_mikum_shaon_yetzia
FROM dual;

EXCEPTION
    WHEN OTHERS         THEN
         p_mikum_shaon_knisa := 0;
         p_mikum_shaon_yetzia := 0;

END pro_get_mikum_shaon_in_out;

PROCEDURE pro_get_zman_nesiaa_mistane (p_merkaz_erua IN CTB_ZMAN_NSIAA_MISHTANE.MERKAZ_ERUA%TYPE,
                                       p_mikum_yaad  IN CTB_ZMAN_NSIAA_MISHTANE.MIKUM_YAAD%TYPE,
                                       p_taarich IN DATE ,
                                       p_dakot   OUT INTEGER)  IS

BEGIN
    SELECT DAKOT INTO p_dakot
        FROM CTB_ZMAN_NSIAA_MISHTANE
         WHERE MERKAZ_ERUA =  p_merkaz_erua AND
               MIKUM_YAAD  =  p_mikum_yaad  AND
               ME_TAARICH  <= p_taarich     AND
               AD_TAARICH  >= p_taarich;

EXCEPTION
    WHEN NO_DATA_FOUND THEN
         p_dakot := 0;

END pro_get_zman_nesiaa_mistane;

PROCEDURE pro_get_zman_nesiaa_ovdim (p_mispar_ishi IN TB_YAMEY_AVODA_OVDIM.mispar_ishi%TYPE,
                                     p_taarich IN DATE ,
                                     p_zman_nesia_haloch  OUT INTEGER   ,
                                     p_zman_nesia_hazor   OUT INTEGER)  IS

BEGIN
SELECT SUM(CASE ZMAN_NESIA_HALOCH  WHEN  NULL THEN 0 ELSE ZMAN_NESIA_HALOCH END) AS  ZMAN_NESIA_HALOCH  ,
       SUM(CASE ZMAN_NESIA_HAZOR   WHEN  NULL THEN 0 ELSE  ZMAN_NESIA_HAZOR END) AS ZMAN_NESIA_HAZOR
       INTO p_zman_nesia_haloch , p_zman_nesia_hazor
        FROM TB_YAMEY_AVODA_OVDIM
      WHERE MISPAR_ISHi = p_mispar_ishi  AND
            TAARICH   = p_taarich ;


END pro_get_zman_nesiaa_ovdim ;

PROCEDURE pro_upd_zman_nesiaa (p_mispar_ishi in TB_YAMEY_AVODA_OVDIM.mispar_ishi%type,
                                     p_taarich in date ,
                                     p_zman_nesia_haloch  in integer   ,
                                     p_zman_nesia_hazor   in integer,      
                                     p_meadken_acharon IN  number) is
BEGIN
 UPDATE TB_YAMEY_AVODA_OVDIM
   SET  ZMAN_NESIA_HALOCH = p_ZMAN_NESIA_HALOCH ,
        ZMAN_NESIA_HAZOR  = p_ZMAN_NESIA_HAZOR
     WHERE
            MISPAR_ISHi = p_MISPAR_ISHi AND
            taarich =  p_taarich ;


  if ((p_mispar_ishi <> p_meadken_acharon) and (p_meadken_acharon > 0) ) then
                          pkg_utils.pro_insert_meadken_acharon(p_mispar_ishi, p_taarich );
   end if;

END pro_upd_zman_nesiaa  ;


PROCEDURE pro_get_last_updator (p_mispar_ishi IN TB_YAMEY_AVODA_OVDIM.mispar_ishi%TYPE,
                                p_taarich IN DATE ,
                                p_cur OUT CurType )  IS
BEGIN

  OPEN  p_cur FOR
  		SELECT UpdateDate,DECODE(mispar_ishi,-1,'מערכת',FullName) FullName,
		DECODE(mispar_ishi,-1,'מערכת',mispar_ishi) IDNUM
 FROM (SELECT UpdateDate,O.SHEM_MISH || ' ' || O.SHEM_PRAT FullName , NVL(o.mispar_ishi,IdNum) mispar_ishi
  FROM
   ( SELECT UpdateDate , CASE WHEN (IdNum< 0) THEN -1 ELSE IdNum END IdNum
    FROM
    (
      SELECT a.TAARICH_IDKUN_ACHARON UpdateDate , a.MEADKEN_ACHARON IdNum
        FROM   TB_YAMEY_AVODA_OVDIM a
           WHERE a.MISPAR_ISHI = p_mispar_ishi AND   a.TAARICH = TRUNC(p_taarich)
    UNION
      SELECT b.TAARICH_IDKUN_ACHARON UpdateDate , b.MEADKEN_ACHARON  IdNum
       FROM   TB_SIDURIM_OVDIM b
           WHERE b.MISPAR_ISHI = p_mispar_ishi AND   b.TAARICH = TRUNC(p_taarich)
    UNION
      SELECT  c.TAARICH_IDKUN_ACHARON UpdateDate , c.MEADKEN_ACHARON  IdNum
       FROM   TB_PEILUT_OVDIM c
           WHERE c.MISPAR_ISHI = p_mispar_ishi AND   c.TAARICH = TRUNC(p_taarich)
    UNION
      SELECT d.TAARICH_IDKUN_ACHARON UpdateDate , d.MEADKEN_ACHARON  IdNum
       FROM   TRAIL_YAMEY_AVODA_OVDIM d
           WHERE d.MISPAR_ISHI = p_mispar_ishi AND   d.TAARICH =   TRUNC(p_taarich)
    UNION
      SELECT e.TAARICH_IDKUN_ACHARON UpdateDate , e.MEADKEN_ACHARON  IdNum
       FROM   TRAIL_SIDURIM_OVDIM e
           WHERE e.MISPAR_ISHI = p_mispar_ishi AND   e.TAARICH =  TRUNC(p_taarich)
    UNION
      SELECT  f.TAARICH_IDKUN_ACHARON UpdateDate , f.MEADKEN_ACHARON  IdNum
       FROM    TRAIL_PEILUT_OVDIM f
        WHERE f.MISPAR_ISHI = p_mispar_ishi AND   f.TAARICH =  TRUNC(p_taarich)
		    )) x,
		OVDIM o
		WHERE x.IdNum= o.mispar_ishi(+)
		)
ORDER BY  UpdateDate DESC;


END  pro_get_last_updator ;

PROCEDURE pro_get_RECHIVIM(   p_cur OUT CurType)
  IS
BEGIN
OPEN p_cur FOR

 SELECT   KOD_RECHIV ,
    /*    case
            when (KOD_RECHIV in (133 , 134 ))then
            'פרמיה רגילה + פרמית נמלק'
            else TEUR_RECHIV
        end         */      TEUR_RECHIV

    FROM  CTB_RECHIVIM
      WHERE   KOD_RECHIV IN (18,217,133,134) --23,52,105,133,134,217,214,215) -- ,216)
             ORDER BY KOD_RECHIV ;

END  pro_get_RECHIVIM ;

PROCEDURE pro_get_months_huavar_lesachar(p_mispar_ishi IN TB_YAMEY_AVODA_OVDIM.mispar_ishi%TYPE,
														p_cur OUT CurType) IS
    
    num NUMBER;
	BEGIN
    
      SELECT SM.ERECH_PARAM into num
      FROM TB_PARAMETRIM SM
      WHERE SM.KOD_PARAM = 100 and
      sysdate BETWEEN SM.ME_TAARICH AND SM.AD_TAARICH;
      
	  OPEN p_cur FOR
	  	SELECT  TO_CHAR(taarich,'mm/yyyy') month_year FROM
		(SELECT c.TAARICH
		FROM TB_BAKASHOT b,TB_CHISHUV_CHODESH_OVDIM c
		WHERE c.mispar_ishi=p_mispar_ishi
		AND  b.HUAVRA_LESACHAR=1
        AND b.ISHUR_HILAN=1
		AND b.BAKASHA_ID=c.bakasha_id
		GROUP BY c.TAARICH
		ORDER BY c.TAARICH DESC)
		WHERE ROWNUM<=num;

  EXCEPTION
       WHEN OTHERS THEN
				RAISE;
END  pro_get_months_huavar_lesachar;

PROCEDURE pro_save_employee_card(p_coll_yamey_avoda_ovdim IN coll_yamey_avoda_ovdim,
                                 p_coll_sidurim_ovdim_upd IN coll_sidurim_ovdim,
                                 p_coll_obj_peilut_ovdim  IN coll_obj_peilut_ovdim,
                                 p_coll_sidurim_ovdim_ins IN coll_sidurim_ovdim,
                                 p_coll_sidurim_ovdim_del IN coll_sidurim_ovdim,
                                 p_coll_peilut_ovdim_del  IN coll_obj_peilut_ovdim,
                                 p_coll_idkun_rashemet IN coll_idkun_rashemet,
                                 p_coll_peilut_ovdim_ins  IN coll_obj_peilut_ovdim) IS
BEGIN

    --Update tb_yamey_avoda_ovdim
    BEGIN
        Pkg_Errors.pro_upd_yamey_avoda_ovdim(p_coll_yamey_avoda_ovdim);
     EXCEPTION
		 WHEN OTHERS THEN
              RAISE_APPLICATION_ERROR(-20001,'An error was encountered in tb_yamey_avoda_ovdim - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;

    
    --delete peiluyot
    BEGIN
        Pkg_Errors.pro_del_peilut_ovdim(p_coll_peilut_ovdim_del);
       EXCEPTION
         WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20001,'An error was encountered in delete in tb_peiluyot_ovdim - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;
    
    --delete sidurim that was canceled    
    BEGIN
        Pkg_Errors.pro_del_sidurim_ovdim(p_coll_sidurim_ovdim_del,0);
       EXCEPTION
         WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20001,'An error was encountered in delete in tb_sidurim_ovdim - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;
    
    --Insert sidurim_ovdim
   BEGIN
        Pkg_Errors.pro_ins_sidurim_ovdim(p_coll_sidurim_ovdim_ins);
      EXCEPTION
         WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20001,'An error was encountered in insert to tb_sidurim_ovdim - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;
    
    --update peiluyot   
    BEGIN                 
          pro_upd_peilut_ovdim(p_coll_obj_peilut_ovdim);
             
      EXCEPTION
         WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20001,'An error was encountered in pkg_ovdim update to tb_peilut_ovdim - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;
   
    --delete peiluyot
   /* BEGIN
        Pkg_Errors.pro_del_peilut_ovdim(p_coll_peilut_ovdim_del);
       EXCEPTION
         WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20001,'An error was encountered in delete in tb_peiluyot_ovdim - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;*/
    
    --update siduriim ovdim
    BEGIN
       pro_upd_sidurim_ovdim(p_coll_sidurim_ovdim_upd);
      EXCEPTION
        WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20001,'An error was encountered in update to tb_sidurim_ovdim - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;

   
    --delete sidurim that the key was update
    BEGIN
        Pkg_Errors.pro_del_sidurim_ovdim(p_coll_sidurim_ovdim_del,1);
       EXCEPTION
         WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20001,'An error was encountered in delete in tb_sidurim_ovdim - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;
    
    BEGIN
        --Insert new peilyot
       Pkg_Errors.pro_ins_peilut_ovdim(p_coll_peilut_ovdim_ins);
     EXCEPTION
         WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20001,'An error was encountered in insert to tb_peilut_ovdim - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;    
    
    --delete sidurim from tb_idkun_rahemet (sidurim that was canceled)   
    BEGIN
        pro_del_idkuney_rashemet_sidur(p_coll_sidurim_ovdim_del,0);
       EXCEPTION
         WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20001,'An error was encountered in delete from tb_idkun_rashemet sidur - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;
    --delete peiluyot from tb_idkun_rahemet    
    BEGIN
        pro_del_idkuney_rashemet_peilt(p_coll_peilut_ovdim_del);
       EXCEPTION
         WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20001,'An error was encountered in delete from tb_idkun_rashemet peilut - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;
    
     --insert field that changed to tb_idkun_rashemet
    BEGIN
        pro_ins_idkuney_rashemet(p_coll_idkun_rashemet);
       EXCEPTION
        WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20001,'An error was encountered in insert to tb_idkun_rashemet - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;

    --delete sidurim from tb_idkun_rahemet (sidurim that the shat hatchala was chaneged)   
    BEGIN
        pro_del_idkuney_rashemet_sidur(p_coll_sidurim_ovdim_del,1);
       EXCEPTION
         WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20001,'An error was encountered in delete from tb_idkun_rashemet sidur - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;
    --delete knisot peilyout that not have peilut av
    BEGIN
        IF (p_coll_obj_peilut_ovdim IS NOT NULL) THEN
           pro_del_knisot_without_av(p_coll_obj_peilut_ovdim(1).mispar_ishi,p_coll_obj_peilut_ovdim(1).taarich, p_coll_obj_peilut_ovdim(1).meadken_acharon);
        END IF ;
       EXCEPTION
        WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20001,'An error was encountered in delete knisot in tb_peilut_ovdim - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;
    
    --delete sidurim from tb_idkun_rahemet  
    /* */ 
    BEGIN
        pro_del_idkuney_rashemet_sidur(p_coll_sidurim_ovdim_del,0);
       EXCEPTION
         WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20001,'An error was encountered in delete from tb_idkun_rashemet sidur - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;
    --delete peiluyot from tb_idkun_rahemet    
    BEGIN
        pro_del_idkuney_rashemet_peilt(p_coll_peilut_ovdim_del);
       EXCEPTION
         WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20001,'An error was encountered in delete from tb_idkun_rashemet peilut - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;/**/
    --Update tb_shgiot_meoharot
    BEGIN
       pro_upd_shgiot_meusharot(p_coll_sidurim_ovdim_ins,p_coll_obj_peilut_ovdim);
      EXCEPTION
        WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20001,'An error was encountered in insert to tb_shgiot_meusharot - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;
END  pro_save_employee_card;
PROCEDURE pro_upd_peilut_ovdim(p_coll_obj_peilut_ovdim IN coll_obj_peilut_ovdim) IS
    bDupWasFound boolean;
BEGIN
    
     IF (p_coll_obj_peilut_ovdim IS NOT NULL) THEN
         FOR i IN REVERSE 1..p_coll_obj_peilut_ovdim.COUNT LOOP
             IF (p_coll_obj_peilut_ovdim(i).update_object=1) THEN
                 pro_ins_peilut_ovdim_trail(p_coll_obj_peilut_ovdim(i),3);   
                 BEGIN
                       pro_upd_peilut_oved(p_coll_obj_peilut_ovdim(i));
                    EXCEPTION
                     WHEN DUP_VAL_ON_INDEX THEN
                        bDupWasFound:=true;
                 END;                                 
             END IF;             
          END LOOP;           
          
           IF (bDupWasFound) THEN
               FOR i IN  1..p_coll_obj_peilut_ovdim.COUNT LOOP
                     IF (p_coll_obj_peilut_ovdim(i).update_object=1) THEN
                          pro_ins_peilut_ovdim_trail(p_coll_obj_peilut_ovdim(i),3);                    
                          pro_upd_peilut_oved(p_coll_obj_peilut_ovdim(i));                                                                    
                     END IF;             
               END LOOP;           
           END IF;
      END IF;    
         

      EXCEPTION        
         WHEN OTHERS THEN
              RAISE;
END pro_upd_peilut_ovdim;
PROCEDURE pro_upd_peilut_oved(p_obj_peilut_ovdim IN obj_peilut_ovdim)
IS
BEGIN
     UPDATE TB_PEILUT_OVDIM
     SET kisuy_tor               = NVL( p_obj_peilut_ovdim.kisuy_tor,kisuy_tor),
         dakot_bafoal            = NVL(p_obj_peilut_ovdim.dakot_bafoal,dakot_bafoal),
         makat_nesia             = NVL( p_obj_peilut_ovdim.makat_nesia,makat_nesia),
         oto_no                  = NVL(p_obj_peilut_ovdim.oto_no,oto_no),
         shat_yetzia             = p_obj_peilut_ovdim.new_shat_yetzia,
         shat_hatchala_sidur     = p_obj_peilut_ovdim.new_shat_hatchala_sidur,
         --  mispar_sidur            = p_obj_peilut_ovdim.new_mispar_sidur,        
         --taarich_idkun_acharon   = p_obj_peilut_ovdim.taarich_idkun_acharon,
         taarich_idkun_acharon   = DECODE(TRUNC(p_obj_peilut_ovdim.taarich_idkun_acharon),TO_DATE('01/01/0001','dd/mm/yyyy'),taarich_idkun_acharon,p_obj_peilut_ovdim.taarich_idkun_acharon),
         meadken_acharon         =NVL( p_obj_peilut_ovdim.meadken_acharon,meadken_acharon),
         bitul_o_hosafa          = NVL(p_obj_peilut_ovdim.bitul_o_hosafa,bitul_o_hosafa ) 
		 --mispar_siduri_oto=  nvl(p_obj_peilut_ovdim.mispar_siduri_oto ,mispar_siduri_oto) ,
		 --kod_shinuy_premia= nvl(p_obj_peilut_ovdim.kod_shinuy_premia,kod_shinuy_premia),
		--km_visa= nvl(p_obj_peilut_ovdim. km_visa, km_visa),
		--mispar_visa= nvl(p_obj_peilut_ovdim.mispar_visa,mispar_visa)
		-- snif_tnua =nvl( p_obj_peilut_ovdim.snif_tnua,snif_tnua)
     WHERE mispar_ishi           = p_obj_peilut_ovdim.mispar_ishi AND
           taarich       = p_obj_peilut_ovdim.taarich AND
           mispar_sidur          = p_obj_peilut_ovdim.mispar_sidur AND
           shat_hatchala_sidur   = p_obj_peilut_ovdim.shat_hatchala_sidur AND
           shat_yetzia           = p_obj_peilut_ovdim.shat_yetzia AND
           mispar_knisa          = p_obj_peilut_ovdim.mispar_knisa;

   IF ((p_obj_peilut_ovdim.meadken_acharon<> p_obj_peilut_ovdim.mispar_ishi) and (p_obj_peilut_ovdim.meadken_acharon>0)) THEN
                    pkg_utils.pro_insert_meadken_acharon(p_obj_peilut_ovdim.mispar_ishi,p_obj_peilut_ovdim.taarich );
        END IF; 
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
                                   taarich_idkun_trail,sug_peula,heara,snif_tnua,teur_nesia,dakot_bafoal,
								  km_visa  )
    SELECT mispar_ishi,taarich,mispar_sidur,
           shat_hatchala_sidur,shat_yetzia,mispar_knisa,
           makat_nesia,oto_no,mispar_siduri_oto,kisuy_tor,
           bitul_o_hosafa,kod_shinuy_premia,mispar_visa,imut_netzer,
           shat_bhirat_nesia_netzer,oto_no_netzer,mispar_sidur_netzer,
           shat_yetzia_netzer,makat_netzer,shilut_netzer,
           mikum_bhirat_nesia_netzer,mispar_matala,
           taarich_idkun_acharon,meadken_acharon,p_obj_peilut_ovdim.meadken_acharon,SYSDATE, p_sug_peula,
           heara,snif_tnua,teur_nesia,dakot_bafoal ,km_visa

    FROM TB_PEILUT_OVDIM
    WHERE mispar_ishi           = p_obj_peilut_ovdim.mispar_ishi         AND
          taarich               = TRUNC(p_obj_peilut_ovdim.taarich)      AND
          mispar_sidur          = p_obj_peilut_ovdim.mispar_sidur        AND
          shat_hatchala_sidur   = p_obj_peilut_ovdim.shat_hatchala_sidur AND
          shat_yetzia           = p_obj_peilut_ovdim.shat_yetzia         AND
          mispar_knisa          = p_obj_peilut_ovdim.mispar_knisa;

    
EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END pro_ins_peilut_ovdim_trail;
PROCEDURE pro_upd_sidurim_ovdim(p_coll_sidurim_ovdim IN COLL_SIDURIM_OVDIM) IS
BEGIN

    IF (p_coll_sidurim_ovdim IS NOT NULL) THEN
        FOR i IN 1..p_coll_sidurim_ovdim.COUNT LOOP
            IF (p_coll_sidurim_ovdim(i).update_object=1) THEN
                pro_ins_sidurim_ovdim_trail(p_coll_sidurim_ovdim(i),3);
                pro_upd_tb_sidur_oved(p_coll_sidurim_ovdim(i));
            END IF;
          END LOOP;
    END IF;
      EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END pro_upd_sidurim_ovdim;

PROCEDURE pro_upd_tb_sidur_oved(p_obj_sidurim_ovdim IN obj_sidurim_ovdim) IS
BEGIN
    --Insert sidurim
    UPDATE TB_SIDURIM_OVDIM
    SET lo_letashlum                 = p_obj_sidurim_ovdim.lo_letashlum,
        hashlama                     = p_obj_sidurim_ovdim.Hashlama,
      --  hamarat_shabat               = p_obj_sidurim_ovdim.hamarat_shabat,
        shat_hatchala                = p_obj_sidurim_ovdim.new_shat_hatchala,
        shat_gmar                    = NVL(p_obj_sidurim_ovdim.shat_gmar,NULL),
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
        shayah_leyom_kodem           = p_obj_sidurim_ovdim.shayah_leyom_kodem,
        --yom_visa                     = p_obj_sidurim_ovdim.yom_visa,
        --achuz_knas_lepremyat_visa    = p_obj_sidurim_ovdim.achuz_knas_lepremyat_visa,
        --achuz_viza_besikun           = p_obj_sidurim_ovdim.achuz_viza_besikun,
        --mispar_musach_o_machsan      = p_obj_sidurim_ovdim.mispar_musach_o_machsan,
        --mispar_shiurey_nehiga        = p_obj_sidurim_ovdim.mispar_shiurey_nehiga,
        --sug_hazmanat_visa            = p_obj_sidurim_ovdim.sug_hazmanat_visa,
        --tafkid_visa                  = p_obj_sidurim_ovdim.tafkid_visa,
        --mivtza_visa                  = p_obj_sidurim_ovdim.mivtza_visa,
        heara                        = p_obj_sidurim_ovdim.heara,
        sug_hashlama                 = p_obj_sidurim_ovdim.sug_hashlama,
        mikum_shaon_knisa            = p_obj_sidurim_ovdim.mikum_shaon_knisa,
        mikum_shaon_yetzia           = p_obj_sidurim_ovdim.mikum_shaon_yetzia
    WHERE mispar_ishi  = p_obj_sidurim_ovdim.mispar_ishi AND
          mispar_sidur = p_obj_sidurim_ovdim.mispar_sidur AND
          taarich      = TRUNC(p_obj_sidurim_ovdim.taarich) AND
          shat_hatchala= p_obj_sidurim_ovdim.shat_hatchala;
          
          IF ((p_obj_sidurim_ovdim.meadken_acharon<> p_obj_sidurim_ovdim.mispar_ishi) and (p_obj_sidurim_ovdim.meadken_acharon>0)) THEN
                    pkg_utils.pro_insert_meadken_acharon(p_obj_sidurim_ovdim.mispar_ishi,p_obj_sidurim_ovdim.taarich );
        END IF;    
EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END pro_upd_tb_sidur_oved;
PROCEDURE pro_upd_sidur_oved(p_obj_sidurim_ovdim IN obj_sidurim_ovdim) IS
BEGIN
    --Insert sidurim
    UPDATE TB_SIDURIM_OVDIM
    SET lo_letashlum                 =NVL( p_obj_sidurim_ovdim.lo_letashlum,lo_letashlum),
        hashlama                     = NVL(p_obj_sidurim_ovdim.Hashlama,  hashlama),
      --  hamarat_shabat               = p_obj_sidurim_ovdim.hamarat_shabat,
        shat_hatchala                = NVL(p_obj_sidurim_ovdim.new_shat_hatchala, shat_hatchala  ),
        shat_gmar                    =NVL( p_obj_sidurim_ovdim.shat_gmar,shat_gmar  ),
        mispar_ishi                  = NVL(p_obj_sidurim_ovdim.mispar_ishi,mispar_ishi),
        mispar_sidur                 = NVL(p_obj_sidurim_ovdim.new_mispar_sidur,mispar_sidur),
        chariga                      =NVL( p_obj_sidurim_ovdim.chariga,chariga),
        out_michsa                   = NVL(p_obj_sidurim_ovdim.out_michsa, out_michsa  ),
        shat_hatchala_letashlum      =NVL( p_obj_sidurim_ovdim.shat_hatchala_letashlum,shat_hatchala_letashlum),
        shat_gmar_letashlum          = NVL(p_obj_sidurim_ovdim.shat_gmar_letashlum,shat_gmar_letashlum),
        mezake_nesiot                = NVL(p_obj_sidurim_ovdim.mezake_nesiot,mezake_nesiot  ),
        mezake_halbasha              =NVL( p_obj_sidurim_ovdim.mezake_halbasha,mezake_halbasha),
        taarich_idkun_acharon        = SYSDATE,
        meadken_acharon          =NVL( p_obj_sidurim_ovdim.meadken_acharon,meadken_acharon),
        kod_siba_ledivuch_yadani_out =NVL( p_obj_sidurim_ovdim.kod_siba_ledivuch_yadani_out,kod_siba_ledivuch_yadani_out),
        kod_siba_ledivuch_yadani_in  = NVL( p_obj_sidurim_ovdim.kod_siba_ledivuch_yadani_in,kod_siba_ledivuch_yadani_in ),
        pitzul_hafsaka               =NVL( p_obj_sidurim_ovdim.pitzul_hafsaka,pitzul_hafsaka),
        kod_siba_lo_letashlum        = NVL(p_obj_sidurim_ovdim.kod_siba_lo_letashlum,kod_siba_lo_letashlum ),
        bitul_o_hosafa               = NVL( p_obj_sidurim_ovdim.bitul_o_hosafa,bitul_o_hosafa),
        shat_hitiatzvut              =NVL( p_obj_sidurim_ovdim.shat_hitiatzvut,shat_hitiatzvut),
		yom_visa  =NVL( p_obj_sidurim_ovdim.yom_visa,yom_visa),
		achuz_knas_lepremyat_visa =NVL( p_obj_sidurim_ovdim.achuz_knas_lepremyat_visa,achuz_knas_lepremyat_visa),
		achuz_viza_besikun=NVL(p_obj_sidurim_ovdim.achuz_viza_besikun,achuz_viza_besikun),
        mispar_musach_o_machsan    =NVL(p_obj_sidurim_ovdim.mispar_musach_o_machsan,mispar_musach_o_machsan),
		mispar_shiurey_nehiga= NVL(p_obj_sidurim_ovdim.mispar_shiurey_nehiga,mispar_shiurey_nehiga),
		sug_hazmanat_visa=NVL(p_obj_sidurim_ovdim.sug_hazmanat_visa,sug_hazmanat_visa),
		tafkid_visa=NVL(p_obj_sidurim_ovdim.tafkid_visa,tafkid_visa),
		mivtza_visa=NVL(p_obj_sidurim_ovdim. mivtza_visa,mivtza_visa),
		heara = NVL(p_obj_sidurim_ovdim.heara,heara),
        sug_hashlama = p_obj_sidurim_ovdim.sug_hashlama
    WHERE mispar_ishi  = p_obj_sidurim_ovdim.mispar_ishi AND
          mispar_sidur = p_obj_sidurim_ovdim.mispar_sidur AND
          taarich      = TRUNC(p_obj_sidurim_ovdim.taarich) AND
          shat_hatchala= p_obj_sidurim_ovdim.shat_hatchala;
          
             IF ((p_obj_sidurim_ovdim.meadken_acharon<> p_obj_sidurim_ovdim.mispar_ishi) and (p_obj_sidurim_ovdim.meadken_acharon>0)) THEN
                    pkg_utils.pro_insert_meadken_acharon(p_obj_sidurim_ovdim.mispar_ishi,p_obj_sidurim_ovdim.taarich );
        END IF;   
EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END pro_upd_sidur_oved;
PROCEDURE pro_ins_sidurim_ovdim_trail(p_obj_sidurim_ovdim IN obj_sidurim_ovdim,p_sug_peula IN TRAIL_SIDURIM_OVDIM.sug_peula%TYPE) IS
BEGIN
    INSERT INTO TRAIL_SIDURIM_OVDIM (mispar_ishi,mispar_sidur,taarich,
                                    shat_hatchala,shat_gmar,shat_hatchala_letashlum,shat_gmar_letashlum,
                                    pitzul_hafsaka,chariga,tosefet_grira,hashlama,yom_visa,
                                    lo_letashlum,out_michsa,mikum_shaon_knisa,mikum_shaon_yetzia,
                                    achuz_knas_lepremyat_visa,achuz_viza_besikun,mispar_musach_o_machsan,
                                    kod_siba_lo_letashlum,kod_siba_ledivuch_yadani_in,kod_siba_ledivuch_yadani_out,
                                    meadken_acharon,taarich_idkun_acharon,heara,shayah_leyom_kodem,mispar_shiurey_nehiga,
                                    mispar_ishi_trail,taarich_idkun_trail,sug_peula,mezake_halbasha,mezake_nesiot,
									menahel_musach_meadken,sug_hazmanat_visa,tafkid_visa,mivtza_visa,nidreshet_hitiatzvut,
									ptor_mehitiatzvut,hachtama_beatar_lo_takin,hafhatat_nochechut_visa )
    SELECT mispar_ishi,mispar_sidur,taarich,
          shat_hatchala,shat_gmar,shat_hatchala_letashlum,shat_gmar_letashlum,
          pitzul_hafsaka,chariga,tosefet_grira,hashlama,yom_visa,
          lo_letashlum,out_michsa,mikum_shaon_knisa,mikum_shaon_yetzia,
          achuz_knas_lepremyat_visa,achuz_viza_besikun,mispar_musach_o_machsan,
          kod_siba_lo_letashlum,kod_siba_ledivuch_yadani_in,kod_siba_ledivuch_yadani_out,
          meadken_acharon,taarich_idkun_acharon,heara,shayah_leyom_kodem,mispar_shiurey_nehiga,
          p_obj_sidurim_ovdim.meadken_acharon,SYSDATE,p_sug_peula, mezake_halbasha,mezake_nesiot,
		  menahel_musach_meadken,sug_hazmanat_visa,tafkid_visa,mivtza_visa,nidreshet_hitiatzvut,
									ptor_mehitiatzvut,hachtama_beatar_lo_takin,hafhatat_nochechut_visa 
								
    FROM TB_SIDURIM_OVDIM
    WHERE mispar_ishi  = p_obj_sidurim_ovdim.mispar_ishi    AND
         mispar_sidur  = p_obj_sidurim_ovdim.mispar_sidur   AND
         taarich       = TRUNC(p_obj_sidurim_ovdim.taarich) AND
         shat_hatchala = p_obj_sidurim_ovdim.shat_hatchala;
         
       
EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END  pro_ins_sidurim_ovdim_trail;

PROCEDURE pro_ins_yemey_avoda_leoved(p_mispar_ishi IN TB_YAMEY_AVODA_OVDIM.mispar_ishi%TYPE,
																	p_taarich IN TB_YAMEY_AVODA_OVDIM.taarich%TYPE,
																	p_measher_mistayeg IN TB_YAMEY_AVODA_OVDIM.measher_o_mistayeg%TYPE,
																	p_status IN TB_YAMEY_AVODA_OVDIM.status_tipul%TYPE,
																	p_meadken IN TB_YAMEY_AVODA_OVDIM.meadken_acharon%TYPE) IS
v_count NUMBER;
BEGIN
	 		SELECT COUNT(*) INTO v_count
			FROM TB_YAMEY_AVODA_OVDIM
			WHERE mispar_ishi=p_mispar_ishi
			AND taarich=p_taarich;

	IF (v_count=0) THEN
  	     INSERT INTO TB_YAMEY_AVODA_OVDIM
		   (mispar_ishi,taarich, measher_o_mistayeg,status_tipul,meadken_acharon,taarich_idkun_acharon,ritzat_shgiot_acharona)
			  VALUES (p_mispar_ishi,p_taarich,p_measher_mistayeg,p_status,p_meadken,SYSDATE,TO_DATE('01/01/0001','dd/mm/yyyy') );
	ELSE
			  UPDATE  TB_YAMEY_AVODA_OVDIM
			  SET ritzat_shgiot_acharona=TO_DATE('01/01/0001','dd/mm/yyyy') 
			  WHERE mispar_ishi=p_mispar_ishi
			AND taarich=p_taarich;
	  END IF;

     IF ((p_meadken<> p_mispar_ishi) and (p_meadken>0)) THEN
                    pkg_utils.pro_insert_meadken_acharon(p_mispar_ishi,p_taarich );
        END IF; 
  EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END  pro_ins_yemey_avoda_leoved;

PROCEDURE pro_get_yemey_avoda(p_mispar_ishi IN  TB_YAMEY_AVODA_OVDIM.mispar_ishi%TYPE,
														p_taarich_me  IN  TB_YAMEY_AVODA_OVDIM.taarich%TYPE,
														p_taarich_ad  IN  TB_YAMEY_AVODA_OVDIM.taarich%TYPE,
                                                          p_cur out CurType)    IS
	--v_count NUMBER;
BEGIN
  OPEN p_cur FOR
	 		SELECT distinct y.taarich  --COUNT(*) INTO v_count
			FROM TB_YAMEY_AVODA_OVDIM y,TB_SIDURIM_OVDIM s
			WHERE y.mispar_ishi=p_mispar_ishi
			AND y.taarich BETWEEN p_taarich_me  AND p_taarich_ad
			AND y.mispar_ishi=s.mispar_ishi
			AND y.taarich=s.taarich
            AND s.mispar_sidur<>99200;
		--	AND (s.bitul_o_hosafa IS NULL OR s.bitul_o_hosafa<>1);

			--RETURN v_count;
  EXCEPTION
         WHEN OTHERS THEN
		 --     RETURN 0;
              RAISE;
END   pro_get_yemey_avoda;


  PROCEDURE  pro_get_MisparIshiByKodVeErech (p_kod_Natun IN INTEGER  ,
  			 					   				   			  	                p_Erech IN VARCHAR2,
																				p_Taarich IN DATE,
																				p_preFix IN VARCHAR2,
																				p_cur OUT CurType) IS
  BEGIN
      OPEN p_cur FOR

  		  SELECT DISTINCT P.MISPAR_ISHI   ---,   O.SHEM_MISH || ' ' ||  O.SHEM_PRAT  FULL_NAME
		   FROM PIRTEY_OVDIM P -- ,
		   			--	OVDIM O
		   WHERE
		   		 	--		 P.MISPAR_ISHI = O.MISPAR_ISHI AND
		   		 			P.KOD_NATUN = p_kod_Natun AND
							P.ERECH = p_Erech AND
							p_Taarich BETWEEN P.ME_TAARICH AND P.AD_TAARICH AND
							( p_preFix IS NULL OR P.MISPAR_ISHI LIKE p_preFix ||  '%'  )
		   ORDER BY P.MISPAR_ISHI;

	   EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;
  END pro_get_MisparIshiByKodVeErech;

PROCEDURE pro_get_peiluyot_le_oved(p_mispar_ishi IN  INTEGER,
		  										 	 		 		 		  	  p_taarich IN  TB_PEILUT_OVDIM.TAARICH%TYPE,
																					  p_mispar_sidur IN  INTEGER,
																					p_shat_hatchala IN  TB_PEILUT_OVDIM.SHAT_HATCHALA_SIDUR%TYPE,
																					p_cur OUT CurType) IS
BEGIN
   			OPEN p_cur FOR
				 SELECT t.*,v_element.hova_mispar_rechev bus_number_must
				 FROM TB_PEILUT_OVDIM t,PIVOT_MEAFYENEY_ELEMENTIM v_element
				 WHERE t.MISPAR_ISHI = p_mispar_ishi AND
				 	   		  t.MISPAR_SIDUR = p_mispar_sidur AND
							  t.TAARICH = TRUNC(p_taarich) AND
						--	  ( t.SHAT_HATCHALA_SIDUR =p_shat_hatchala  or t.SHAT_HATCHALA_SIDUR =p_shat_hatchala +1  ) and
							  t.SHAT_HATCHALA_SIDUR =p_shat_hatchala AND
							 TO_NUMBER(SUBSTR(t.makat_nesia,2,2)) = v_element.kod_element(+) AND
                             p_taarich BETWEEN v_element.me_tarich(+) AND v_element.ad_tarich(+)
				ORDER BY t.SHAT_YETZIA;

	   EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;
  END pro_get_peiluyot_le_oved;

PROCEDURE pro_upd_yamey_avoda_ovdim(p_coll_yamey_avoda_ovdim IN coll_yamey_avoda_ovdim) IS
BEGIN
      IF (p_coll_yamey_avoda_ovdim IS NOT NULL) THEN
          FOR i IN 1..p_coll_yamey_avoda_ovdim.COUNT LOOP
              IF (p_coll_yamey_avoda_ovdim(i).update_object=1) THEN
                --  pro_ins_yom_avoda_oved_trail(p_coll_yamey_avoda_ovdim(i));
                  pro_upd_yom_avoda_oved(p_coll_yamey_avoda_ovdim(i));
              END IF;
          END LOOP;
      END IF;
      EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;
END pro_upd_yamey_avoda_ovdim;

PROCEDURE pro_upd_sadot_nosafim(p_coll_yamey_avoda_ovdim IN coll_yamey_avoda_ovdim,
		  												 	p_coll_sidurim_ovdim IN coll_sidurim_ovdim,
															p_coll_obj_peilut_ovdim IN coll_obj_peilut_ovdim) IS
	 p_obj_yamey_avoda_ovdim obj_yamey_avoda_ovdim;
	p_obj_sidurim_ovdim obj_sidurim_ovdim;		
	p_obj_peilut_ovdim obj_peilut_ovdim;			
BEGIN
   IF (p_coll_yamey_avoda_ovdim IS NOT NULL) THEN
          FOR i IN 1..p_coll_yamey_avoda_ovdim.COUNT LOOP
              IF (p_coll_yamey_avoda_ovdim(i).update_object=1) THEN
                  Pkg_Errors.pro_ins_yom_avoda_oved_trail(p_coll_yamey_avoda_ovdim(i));
                  
				  p_obj_yamey_avoda_ovdim:=p_coll_yamey_avoda_ovdim(i);
				  UPDATE TB_YAMEY_AVODA_OVDIM
			     SET lina  = p_obj_yamey_avoda_ovdim.lina,
			         meadken_acharon        =p_obj_yamey_avoda_ovdim.meadken_acharon,
			         taarich_idkun_acharon = SYSDATE
			     WHERE mispar_ishi     = p_obj_yamey_avoda_ovdim.mispar_ishi AND
			           taarich         = TRUNC(p_obj_yamey_avoda_ovdim.taarich);
                       
                   IF ((p_obj_yamey_avoda_ovdim.meadken_acharon<> p_obj_yamey_avoda_ovdim.mispar_ishi) and (p_obj_yamey_avoda_ovdim.meadken_acharon>0)) THEN
                    pkg_utils.pro_insert_meadken_acharon(p_obj_yamey_avoda_ovdim.mispar_ishi,p_obj_yamey_avoda_ovdim.taarich );
                   END IF;      
              END IF;
          END LOOP;
      END IF;
	
	  IF (p_coll_sidurim_ovdim IS NOT NULL) THEN
        FOR i IN 1..p_coll_sidurim_ovdim.COUNT LOOP
            IF (p_coll_sidurim_ovdim(i).update_object=1) THEN
                pro_ins_sidurim_ovdim_trail(p_coll_sidurim_ovdim(i),3);
              
			  p_obj_sidurim_ovdim :=p_coll_sidurim_ovdim(i);
			  	 UPDATE TB_SIDURIM_OVDIM
			    SET meadken_acharon          =p_obj_sidurim_ovdim.meadken_acharon,
			        yom_visa  =p_obj_sidurim_ovdim.yom_visa,
					achuz_knas_lepremyat_visa =p_obj_sidurim_ovdim.achuz_knas_lepremyat_visa,
					achuz_viza_besikun=p_obj_sidurim_ovdim.achuz_viza_besikun,
			        mispar_musach_o_machsan    =p_obj_sidurim_ovdim.mispar_musach_o_machsan,
					mispar_shiurey_nehiga= p_obj_sidurim_ovdim.mispar_shiurey_nehiga,
					sug_hazmanat_visa=p_obj_sidurim_ovdim.sug_hazmanat_visa,
					tafkid_visa=p_obj_sidurim_ovdim.tafkid_visa,
					mivtza_visa=p_obj_sidurim_ovdim. mivtza_visa,
					  taarich_idkun_acharon        = SYSDATE
			    WHERE mispar_ishi  = p_obj_sidurim_ovdim.mispar_ishi AND
			          mispar_sidur = p_obj_sidurim_ovdim.mispar_sidur AND
			          taarich      = TRUNC(p_obj_sidurim_ovdim.taarich) AND
			          shat_hatchala= p_obj_sidurim_ovdim.shat_hatchala;
                      
                  IF ((p_obj_sidurim_ovdim.meadken_acharon<> p_obj_sidurim_ovdim.mispar_ishi) and (p_obj_sidurim_ovdim.meadken_acharon>0)) THEN
                    pkg_utils.pro_insert_meadken_acharon(p_obj_sidurim_ovdim.mispar_ishi,p_obj_sidurim_ovdim.taarich );
                   END IF;           
            END IF;
          END LOOP;
    END IF;

	 IF (p_coll_obj_peilut_ovdim IS NOT NULL) THEN
         FOR i IN 1..p_coll_obj_peilut_ovdim.COUNT LOOP
             IF (p_coll_obj_peilut_ovdim(i).update_object=1) THEN
                 pro_ins_peilut_ovdim_trail(p_coll_obj_peilut_ovdim(i),3);
               
			   p_obj_peilut_ovdim:=p_coll_obj_peilut_ovdim(i);
			     UPDATE TB_PEILUT_OVDIM
			     SET     taarich_idkun_acharon   = SYSDATE,
			         meadken_acharon         = p_obj_peilut_ovdim.meadken_acharon,
			        mispar_siduri_oto=  p_obj_peilut_ovdim.mispar_siduri_oto ,
					 kod_shinuy_premia= p_obj_peilut_ovdim.kod_shinuy_premia,
					km_visa= p_obj_peilut_ovdim. km_visa,
					mispar_visa= p_obj_peilut_ovdim.mispar_visa
				   WHERE mispar_ishi           = p_obj_peilut_ovdim.mispar_ishi AND
			           TRUNC(taarich)        = p_obj_peilut_ovdim.taarich AND
			           mispar_sidur          = p_obj_peilut_ovdim.mispar_sidur AND
			           shat_hatchala_sidur   = p_obj_peilut_ovdim.shat_hatchala_sidur AND
			           shat_yetzia           = p_obj_peilut_ovdim.shat_yetzia AND
			           mispar_knisa          = p_obj_peilut_ovdim.mispar_knisa;
                   
                   IF ((p_obj_peilut_ovdim.meadken_acharon<> p_obj_peilut_ovdim.mispar_ishi) and (p_obj_peilut_ovdim.meadken_acharon>0)) THEN
                    pkg_utils.pro_insert_meadken_acharon(p_obj_peilut_ovdim.mispar_ishi,p_obj_peilut_ovdim.taarich );
                   END IF;        
             END IF;

          END LOOP;
      END IF;

      EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;
END  pro_upd_sadot_nosafim;

PROCEDURE pro_upd_yom_avoda_oved(p_obj_yamey_avoda_ovdim IN obj_yamey_avoda_ovdim) IS
BEGIN
     UPDATE TB_YAMEY_AVODA_OVDIM
     SET tachograf             = NVL(p_obj_yamey_avoda_ovdim.tachograf, tachograf ),
         halbasha              = NVL(p_obj_yamey_avoda_ovdim.halbasha,halbasha),
         lina                  = NVL(p_obj_yamey_avoda_ovdim.lina, lina ),
         hashlama_leyom        = NVL(p_obj_yamey_avoda_ovdim.hashlama_leyom,hashlama_leyom   ),
         bitul_zman_nesiot     = NVL(p_obj_yamey_avoda_ovdim.bitul_zman_nesiot,bitul_zman_nesiot  ),
         meadken_acharon        =NVL( p_obj_yamey_avoda_ovdim.meadken_acharon,meadken_acharon),
         zman_nesia_haloch     = NVL(p_obj_yamey_avoda_ovdim.zman_nesia_haloch,zman_nesia_haloch),
         zman_nesia_hazor      = NVL(p_obj_yamey_avoda_ovdim.zman_nesia_hazor,zman_nesia_hazor),
         sibat_hashlama_leyom  = NVL(p_obj_yamey_avoda_ovdim.sibat_hashlama_leyom,sibat_hashlama_leyom),
         taarich_idkun_acharon = SYSDATE
     WHERE mispar_ishi     = p_obj_yamey_avoda_ovdim.mispar_ishi AND
           taarich         = TRUNC(p_obj_yamey_avoda_ovdim.taarich);
           
              IF ((p_obj_yamey_avoda_ovdim.meadken_acharon<> p_obj_yamey_avoda_ovdim.mispar_ishi) and (p_obj_yamey_avoda_ovdim.meadken_acharon>0)) THEN
                    pkg_utils.pro_insert_meadken_acharon(p_obj_yamey_avoda_ovdim.mispar_ishi,p_obj_yamey_avoda_ovdim.taarich );
                   END IF; 
EXCEPTION
		 WHEN OTHERS THEN
		      RAISE;
END pro_upd_yom_avoda_oved;

PROCEDURE pro_ins_idkuney_rashemet(p_coll_idkun_rashemet IN coll_idkun_rashemet) IS
 count_i number;
BEGIN

     IF (p_coll_idkun_rashemet IS NOT NULL) THEN
   -- count_i:=p_coll_idkun_rashemet.COUNT+1;
     
          FOR count_i IN 1..p_coll_idkun_rashemet.COUNT LOOP
               IF (p_coll_idkun_rashemet(count_i).NEW_SHAT_HATCHALA=p_coll_idkun_rashemet(count_i).SHAT_HATCHALA) THEN                  
                  IF (p_coll_idkun_rashemet(count_i).NEW_SHAT_YETZIA<>p_coll_idkun_rashemet(count_i).SHAT_YETZIA) THEN
                      pro_upd_idkun_rashemet_peilut(p_coll_idkun_rashemet(count_i));                     
                  END IF;
                  pro_ins_idkun_rashemet(p_coll_idkun_rashemet(count_i));
                 ELSE
                   pro_upd_idkun_rashemet(p_coll_idkun_rashemet(count_i)); 
                   IF (p_coll_idkun_rashemet(count_i).update_object=1) THEN
                        pro_ins_idkun_rashemet(p_coll_idkun_rashemet(count_i));
                   END IF;                 
               END IF;   
          END LOOP;
      END IF;
    EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END pro_ins_idkuney_rashemet;
PROCEDURE pro_ins_idkun_rashemet(p_obj_idkun_rashemet IN obj_idkun_rashemet) IS
BEGIN
     INSERT INTO TB_IDKUN_RASHEMET (mispar_ishi,
                                    taarich,
                                    mispar_sidur,
                                    shat_hatchala,
                                    shat_yetzia,
                                    mispar_knisa,
                                    taarich_idkun,
                                    pakad_id,
                                    gorem_meadken)
     VALUES (p_obj_idkun_rashemet.mispar_ishi,
             p_obj_idkun_rashemet.taarich,
             p_obj_idkun_rashemet.mispar_sidur,
             p_obj_idkun_rashemet.new_shat_hatchala,
             p_obj_idkun_rashemet.new_shat_yetzia,
             p_obj_idkun_rashemet.mispar_knisa,
             SYSDATE,
             p_obj_idkun_rashemet.pakad_id,
             p_obj_idkun_rashemet.gorem_meadken);
EXCEPTION
    WHEN DUP_VAL_ON_INDEX THEN
        --Update date
        UPDATE TB_IDKUN_RASHEMET 
        SET       
         taarich_idkun   = sysdate       
    WHERE
        mispar_ishi     = p_obj_idkun_rashemet.mispar_ishi AND 
        taarich         = p_obj_idkun_rashemet.taarich AND                                
        mispar_sidur    = p_obj_idkun_rashemet.mispar_sidur AND
        shat_hatchala   = p_obj_idkun_rashemet.shat_hatchala AND
        shat_yetzia     = p_obj_idkun_rashemet.shat_yetzia AND
        Mispar_knisa    = p_obj_idkun_rashemet.mispar_knisa AND
        pakad_id        = p_obj_idkun_rashemet.pakad_id; 
        RETURN;
    WHEN OTHERS THEN
            RAISE;  
        
END pro_ins_idkun_rashemet;
PROCEDURE pro_upd_idkun_rashemet(p_obj_idkun_rashemet IN obj_idkun_rashemet) IS
BEGIN
    UPDATE TB_IDKUN_RASHEMET 
    SET mispar_sidur    = p_obj_idkun_rashemet.mispar_sidur,
        shat_hatchala   = p_obj_idkun_rashemet.new_shat_hatchala,
       -- shat_yetzia     = p_obj_idkun_rashemet.new_shat_yetzia,
       -- mispar_knisa    = p_obj_idkun_rashemet.mispar_knisa--,
       taarich_idkun   = sysdate
       -- pakad_id        = p_obj_idkun_rashemet.pakad_id
    WHERE
        mispar_ishi     = p_obj_idkun_rashemet.mispar_ishi AND 
        taarich         = p_obj_idkun_rashemet.taarich AND                                
        mispar_sidur    = p_obj_idkun_rashemet.mispar_sidur AND
        shat_hatchala   = p_obj_idkun_rashemet.shat_hatchala; 
      --  shat_yetzia     = p_obj_idkun_rashemet.shat_yetzia AND
      --  mispar_knisa    = p_obj_idkun_rashemet.mispar_knisa;                                                               
                                    
    
EXCEPTION    
    WHEN OTHERS THEN
            RAISE;  
END pro_upd_idkun_rashemet;
PROCEDURE pro_upd_idkun_rashemet_peilut(p_obj_idkun_rashemet IN obj_idkun_rashemet) IS
BEGIN
    UPDATE TB_IDKUN_RASHEMET 
    SET shat_yetzia    = p_obj_idkun_rashemet.new_shat_yetzia ,
        taarich_idkun   = sysdate       
    WHERE
        mispar_ishi     = p_obj_idkun_rashemet.mispar_ishi AND 
        taarich         = p_obj_idkun_rashemet.taarich AND                     
        mispar_sidur    = p_obj_idkun_rashemet.mispar_sidur AND        
        shat_hatchala   = p_obj_idkun_rashemet.shat_hatchala AND
        shat_yetzia     = p_obj_idkun_rashemet.shat_yetzia; 
EXCEPTION    
    WHEN OTHERS THEN
            RAISE;    
END pro_upd_idkun_rashemet_peilut;
PROCEDURE pro_get_pirtey_hitkashrut_oved(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
 		   									 							p_cur OUT CurType) IS
BEGIN

	 OPEN p_cur FOR
		SELECT o.KTOVET,o.TELEFON_AVODA,o.TELEFON_BAIT,o.TELEFON_NAID,o.EMAIL
		FROM OVDIM o
		WHERE o.MISPAR_ISHI = p_mispar_ishi ;

EXCEPTION
       WHEN OTHERS THEN
            RAISE;
 END pro_get_pirtey_hitkashrut_oved;
PROCEDURE pro_get_idkuney_rashemet(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,p_taarich IN TB_YAMEY_AVODA_OVDIM.taarich%TYPE,p_cur OUT CurType) IS
BEGIN
    OPEN p_cur FOR
    SELECT * FROM TB_IDKUN_RASHEMET
    WHERE mispar_ishi = p_mispar_ishi
    AND taarich =  p_taarich;
EXCEPTION
       WHEN OTHERS THEN
            RAISE;
END  pro_get_idkuney_rashemet;
PROCEDURE pro_get_meadken_acharon(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,p_date IN TB_YAMEY_AVODA_OVDIM.taarich%TYPE,p_cur OUT CurType) IS
    i_count number;
BEGIN
DBMS_APPLICATION_INFO.SET_MODULE('pkg_ovdim.pro_get_meadken_acharon','get meadken acharon work card');

     --Get maximun table/trail  last update
     
     select count(*) into i_count
     from (
      SELECT a.TAARICH_IDKUN_ACHARON ,a.MEADKEN_ACHARON, 0 MISPAR_ISHI_TRAIL            
            FROM TB_YAMEY_AVODA_OVDIM a
              WHERE a.MISPAR_ISHI= p_mispar_ishi
                    AND a.TAARICH = p_date
                    AND MEADKEN_ACHARON > 0 AND MEADKEN_ACHARON <> p_mispar_ishi                    
   UNION
            SELECT a.TAARICH_IDKUN_ACHARON ,a.MEADKEN_ACHARON, 0 MISPAR_ISHI_TRAIL            
            FROM TB_SIDURIM_OVDIM a
              WHERE a.MISPAR_ISHI= p_mispar_ishi
                    AND a.TAARICH = p_date
                    AND MEADKEN_ACHARON > 0 AND MEADKEN_ACHARON <> p_mispar_ishi   
   UNION
            SELECT a.TAARICH_IDKUN_ACHARON ,a.MEADKEN_ACHARON, 0 MISPAR_ISHI_TRAIL          
            FROM TB_PEILUT_OVDIM a
              WHERE a.MISPAR_ISHI= p_mispar_ishi
                    AND a.TAARICH = p_date
                    AND MEADKEN_ACHARON > 0 AND MEADKEN_ACHARON <> p_mispar_ishi                    
    UNION
            SELECT a.TAARICH_IDKUN_TRAIL ,a.MEADKEN_ACHARON, a.MISPAR_ISHI_TRAIL           
            FROM TRAIL_YAMEY_AVODA_OVDIM a
              WHERE a.MISPAR_ISHI= p_mispar_ishi
                    AND a.TAARICH = p_date
                    AND ((MEADKEN_ACHARON > 0 AND MEADKEN_ACHARON <> p_mispar_ishi)
                      OR 
                       (MISPAR_ISHI_TRAIL > 0 AND MISPAR_ISHI_TRAIL <> p_mispar_ishi)) 
    UNION
            SELECT a.TAARICH_IDKUN_TRAIL ,a.MEADKEN_ACHARON, a.MISPAR_ISHI_TRAIL           
            FROM TRAIL_SIDURIM_OVDIM a
              WHERE a.MISPAR_ISHI= p_mispar_ishi
                    AND a.TAARICH = p_date
                    AND ((MEADKEN_ACHARON > 0 AND MEADKEN_ACHARON <> p_mispar_ishi)                                        
                     OR                   
                      (MISPAR_ISHI_TRAIL > 0 AND MISPAR_ISHI_TRAIL <> p_mispar_ishi)) 
     UNION
            SELECT a.TAARICH_IDKUN_TRAIL ,a.MEADKEN_ACHARON, a.MISPAR_ISHI_TRAIL           
            FROM TRAIL_PEILUT_OVDIM a
              WHERE a.MISPAR_ISHI= p_mispar_ishi
                    AND a.TAARICH = p_date
                    AND ((MEADKEN_ACHARON > 0 AND MEADKEN_ACHARON <> p_mispar_ishi)
                        OR 
                         (MISPAR_ISHI_TRAIL > 0 AND MISPAR_ISHI_TRAIL <> p_mispar_ishi))    );
     -- DBMS_OUTPUT.PUT_LINE('i_count = ' || i_count);                     
  if (i_count >0) then
  --DBMS_OUTPUT.PUT_LINE('i_count2 = ' || i_count);           
                OPEN p_cur FOR
                            
                        SELECT a.TAARICH_IDKUN_ACHARON ,a.MEADKEN_ACHARON, 0 MISPAR_ISHI_TRAIL            
                        FROM TB_YAMEY_AVODA_OVDIM a
                          WHERE a.MISPAR_ISHI= p_mispar_ishi
                                AND a.TAARICH = p_date
                                AND MEADKEN_ACHARON > 0 AND MEADKEN_ACHARON <> p_mispar_ishi                    
                        UNION

                        SELECT a.TAARICH_IDKUN_ACHARON ,a.MEADKEN_ACHARON, 0 MISPAR_ISHI_TRAIL            
                        FROM TB_SIDURIM_OVDIM a
                          WHERE a.MISPAR_ISHI= p_mispar_ishi
                                AND a.TAARICH = p_date
                                AND MEADKEN_ACHARON > 0 AND MEADKEN_ACHARON <> p_mispar_ishi
                                
                        UNION


                        SELECT a.TAARICH_IDKUN_ACHARON ,a.MEADKEN_ACHARON, 0 MISPAR_ISHI_TRAIL          
                        FROM TB_PEILUT_OVDIM a
                          WHERE a.MISPAR_ISHI= p_mispar_ishi
                                AND a.TAARICH = p_date
                                AND MEADKEN_ACHARON > 0 AND MEADKEN_ACHARON <> p_mispar_ishi                    
                                
                        UNION

                        SELECT a.TAARICH_IDKUN_TRAIL ,a.MEADKEN_ACHARON, a.MISPAR_ISHI_TRAIL           
                        FROM TRAIL_YAMEY_AVODA_OVDIM a
                          WHERE a.MISPAR_ISHI= p_mispar_ishi
                                AND a.TAARICH = p_date
                                AND ((MEADKEN_ACHARON > 0 AND MEADKEN_ACHARON <> p_mispar_ishi)
                                  OR 
                                   (MISPAR_ISHI_TRAIL > 0 AND MISPAR_ISHI_TRAIL <> p_mispar_ishi)) 
                                

                        UNION

                        SELECT a.TAARICH_IDKUN_TRAIL ,a.MEADKEN_ACHARON, a.MISPAR_ISHI_TRAIL           
                        FROM TRAIL_SIDURIM_OVDIM a
                          WHERE a.MISPAR_ISHI= p_mispar_ishi
                                AND a.TAARICH = p_date
                                AND ((MEADKEN_ACHARON > 0 AND MEADKEN_ACHARON <> p_mispar_ishi)                                        
                                 OR                   
                                  (MISPAR_ISHI_TRAIL > 0 AND MISPAR_ISHI_TRAIL <> p_mispar_ishi)) 
                        UNION

                        SELECT a.TAARICH_IDKUN_TRAIL ,a.MEADKEN_ACHARON, a.MISPAR_ISHI_TRAIL           
                        FROM TRAIL_PEILUT_OVDIM a
                          WHERE a.MISPAR_ISHI= p_mispar_ishi
                                AND a.TAARICH = p_date
                                AND ((MEADKEN_ACHARON > 0 AND MEADKEN_ACHARON <> p_mispar_ishi)
                                    OR 
                                     (MISPAR_ISHI_TRAIL > 0 AND MISPAR_ISHI_TRAIL <> p_mispar_ishi));
        end if;                        

EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END pro_get_meadken_acharon;

PROCEDURE pro_update_measher_o_mistayeg(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,p_date IN TB_YAMEY_AVODA_OVDIM.taarich%TYPE, p_status IN TB_YAMEY_AVODA_OVDIM.measher_o_mistayeg%TYPE) IS
BEGIN
    UPDATE TB_YAMEY_AVODA_OVDIM
    SET MEASHER_O_MISTAYEG = p_status
    WHERE MISPAR_ISHI = p_mispar_ishi
    AND TAARICH = p_date;
EXCEPTION
         WHEN OTHERS THEN
              RAISE;

END pro_update_measher_o_mistayeg;


PROCEDURE pro_get_oved_details_betkufa(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
		  											 						   		      p_start_date IN DATE,p_end_date  IN DATE, p_cur OUT CurType )
IS
BEGIN
        OPEN p_Cur FOR
		 SELECT DISTINCT  o.shem_mish || ' ' || o.shem_prat full_name,
		               s.teur_snif_av,s.kod_snif_av, ma.teur_maamad_hr,
		               ma.kod_maamad_hr, ez.kod_ezor, ez.teur_ezor,
		               ck.teur_isuk, ch.teur_hevra
		        FROM  OVDIM o,CTB_SNIF_AV s,CTB_MAAMAD ma,CTB_EZOR ez, PIVOT_PIRTEY_OVDIM  po,
		              CTB_ISUK ck, CTB_HEVRA ch          ,
					    (SELECT t.mispar_ishi,MAX(t.ME_TARICH) me_taarich
							   FROM PIVOT_PIRTEY_OVDIM t
							   WHERE t.isuk IS NOT NULL
								     AND (TRUNC(p_start_date) BETWEEN  t.ME_TARICH  AND   NVL(t.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
							 		  OR    TRUNC(p_end_date) BETWEEN t.ME_TARICH  AND   NVL(t.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
							  		  OR  t.ME_TARICH>= TRUNC(p_start_date) AND   NVL(t.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=  TRUNC(p_end_date) )
									  AND t.MISPAR_ISHI = p_mispar_ishi
							  GROUP BY t.mispar_ishi) p
		        WHERE po.mispar_ishi = p.mispar_ishi AND
		              po.me_tarich = p.me_taarich AND
				     o.mispar_ishi= p_mispar_ishi AND
		              po.mispar_ishi= o.mispar_ishi AND
		              s.kod_hevra = ez.kod_hevra AND
		              s.kod_snif_av = po.snif_av AND
		              ma.kod_maamad_hr = po.maamad AND
		              ez.kod_ezor = po.ezor AND
		              ck.KOD_HEVRA = o.KOD_HEVRA AND
		              ck.KOD_ISUK = po.ISUK AND
		              ch.KOD_HEVRA =o.KOD_HEVRA AND
					  o.mispar_ishi =p.mispar_ishi ;
EXCEPTION
         WHEN OTHERS THEN
              RAISE;

END pro_get_oved_details_betkufa;

PROCEDURE	pro_get_ovdim_to_period_ByCode(p_code IN  NUMBER,p_start_date IN DATE,
												  	  					  	 	  		  			  p_end_date  IN DATE, p_cur OUT CurType )IS
BEGIN
        OPEN p_Cur FOR
			 SELECT y.MISPAR_ISHI,y.TAARICH
			 FROM TB_YAMEY_AVODA_OVDIM y,
			           MEAFYENIM_OVDIM m
			 WHERE y.MISPAR_ISHI = m.MISPAR_ISHI
			 	   		AND  m.KOD_MEAFYEN = p_code
						AND y.TAARICH BETWEEN p_start_date AND p_end_date;

EXCEPTION
         WHEN OTHERS THEN
              RAISE;

END pro_get_ovdim_to_period_ByCode;
PROCEDURE pro_save_measher_O_mistayeg(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,p_date IN TB_YAMEY_AVODA_OVDIM.taarich%TYPE,p_status NUMBER) IS
BEGIN
    UPDATE TB_YAMEY_AVODA_OVDIM
    SET MEASHER_O_MISTAYEG = p_status
    WHERE MISPAR_ISHI = p_mispar_ishi
    AND TAARICH = p_date;
EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END  pro_save_measher_O_mistayeg;
PROCEDURE pro_get_pakad_id(p_masach_id IN TB_MASACH.masach_id%TYPE,  p_cur OUT CurType) IS
BEGIN
    OPEN p_cur FOR
    SELECT pakad_id, shem_db 
    FROM TB_MASACH M
    WHERE M.MASACH_ID = p_masach_id;
   
EXCEPTION
         WHEN OTHERS THEN
              RAISE;
     
END pro_get_pakad_id;
FUNCTION fun_is_card_exists_yemey_avoda(p_mispar_ishi IN  TB_YAMEY_AVODA_OVDIM.mispar_ishi%TYPE,
                                        p_taarich  IN  TB_YAMEY_AVODA_OVDIM.TAARICH%TYPE) RETURN NUMBER AS

v_count NUMBER;
BEGIN
        SELECT COUNT(*) INTO v_count
        FROM TB_YAMEY_AVODA_OVDIM y
        WHERE y.mispar_ishi=p_mispar_ishi
        AND y.taarich = TRUNC(p_taarich);
            
        RETURN v_count;
  EXCEPTION
         WHEN OTHERS THEN
              RETURN 0;
              RAISE;
END  fun_is_card_exists_yemey_avoda;           

FUNCTION fun_check_peilut_exist(p_mispar_sidur IN TB_PEILUT_OVDIM.mispar_sidur%TYPE,
		 									   p_mispar_ishi IN  TB_PEILUT_OVDIM.mispar_ishi%TYPE,
		 									  p_shat_hatchala IN  TB_PEILUT_OVDIM.shat_hatchala_sidur%TYPE,
											 p_shat_yezia IN  TB_PEILUT_OVDIM.shat_yetzia%TYPE ) RETURN NUMBER    AS

v_count NUMBER;
BEGIN
      
		SELECT COUNT(*)  INTO v_count
		FROM TB_PEILUT_OVDIM p
		WHERE p.MISPAR_ISHI =p_mispar_ishi 
		AND p.MISPAR_SIDUR = p_mispar_sidur
		--   and p.TAARICH = to_date('08/03/2010','dd/mm/yyyy')
		   AND p.SHAT_HATCHALA_SIDUR  =p_shat_hatchala --  to_date('08/03/2010 06:24:00 ','dd/mm/yyyy HH24:mi:ss')
		   AND p.SHAT_YETZIA =p_shat_yezia; -- to_date('08/03/2010 17:23:00 ','dd/mm/yyyy HH24:mi:ss')
            
        RETURN v_count;
  EXCEPTION
         WHEN OTHERS THEN
              RETURN 0;
              RAISE;
END fun_check_peilut_exist;
       
FUNCTION fun_check_sidur_exist(p_mispar_sidur IN TB_SIDURIM_OVDIM.mispar_sidur%TYPE,
		 									  	 					 p_mispar_ishi IN  TB_SIDURIM_OVDIM.mispar_ishi%TYPE,
		 									                         p_shat_hatchala IN  TB_SIDURIM_OVDIM.shat_hatchala%TYPE ) RETURN NUMBER    AS

v_count NUMBER;
BEGIN
      
		SELECT COUNT(*)  INTO v_count
		FROM TB_SIDURIM_OVDIM s
		WHERE s.MISPAR_ISHI =p_mispar_ishi 
		  AND s.MISPAR_SIDUR = p_mispar_sidur
		   AND s.SHAT_HATCHALA  =p_shat_hatchala ;
		
        RETURN v_count;
  EXCEPTION
         WHEN OTHERS THEN
              RETURN 0;
              RAISE;
END fun_check_sidur_exist;      

PROCEDURE pro_del_knisot_peilut( p_mispar_ishi IN  TB_PEILUT_OVDIM.mispar_ishi%TYPE,
		 									 p_mispar_sidur IN TB_PEILUT_OVDIM.mispar_sidur%TYPE,
											 p_taarich IN  TB_PEILUT_OVDIM.taarich%TYPE,
		 									   p_shat_hatchala IN  TB_PEILUT_OVDIM.shat_hatchala_sidur%TYPE,
											 p_shat_yezia IN  TB_PEILUT_OVDIM.shat_yetzia%TYPE,
											 p_makat_nesia IN  TB_PEILUT_OVDIM.makat_nesia%TYPE,
                                             p_meadken_acharon IN  TB_PEILUT_OVDIM.meadken_acharon%TYPE) IS
BEGIN
	  INSERT INTO TRAIL_PEILUT_OVDIM(mispar_ishi,taarich,mispar_sidur,
                                   shat_hatchala_sidur,shat_yetzia,mispar_knisa,
                                   makat_nesia,oto_no,mispar_siduri_oto,kisuy_tor,
                                   bitul_o_hosafa,kod_shinuy_premia,mispar_visa,imut_netzer,
                                   shat_bhirat_nesia_netzer,oto_no_netzer,mispar_sidur_netzer,
                                   shat_yetzia_netzer,makat_netzer,shilut_netzer,
                                   mikum_bhirat_nesia_netzer,mispar_matala,
                                   taarich_idkun_acharon,meadken_acharon,mispar_ishi_trail,
                                   taarich_idkun_trail,sug_peula,heara,snif_tnua,teur_nesia)
    SELECT mispar_ishi,taarich,mispar_sidur,
           shat_hatchala_sidur,shat_yetzia,mispar_knisa,
           makat_nesia,oto_no,mispar_siduri_oto,kisuy_tor,
           bitul_o_hosafa,kod_shinuy_premia,mispar_visa,imut_netzer,
           shat_bhirat_nesia_netzer,oto_no_netzer,mispar_sidur_netzer,
           shat_yetzia_netzer,makat_netzer,shilut_netzer,
           mikum_bhirat_nesia_netzer,mispar_matala,
           taarich_idkun_acharon,meadken_acharon,-2,SYSDATE,3,
           heara,snif_tnua,teur_nesia

    FROM TB_PEILUT_OVDIM
     WHERE    mispar_ishi =p_mispar_ishi 
	    AND taarich  = p_taarich
		AND mispar_sidur = p_mispar_sidur
		AND shat_hatchala_sidur = p_shat_hatchala
		AND shat_yetzia= p_shat_yezia
		AND  mispar_knisa >0
		 AND makat_nesia=p_makat_nesia;

        DELETE TB_PEILUT_OVDIM
        WHERE    mispar_ishi =p_mispar_ishi 
	    AND taarich  = p_taarich
		AND mispar_sidur = p_mispar_sidur
		AND shat_hatchala_sidur = p_shat_hatchala
		AND shat_yetzia= p_shat_yezia
		AND  mispar_knisa >0
		 AND makat_nesia=p_makat_nesia;
			   
        IF ((p_meadken_acharon<> p_mispar_ishi) and (p_meadken_acharon>0)) THEN
                    pkg_utils.pro_insert_meadken_acharon(p_mispar_ishi,p_taarich );
        END IF; 
        
		EXCEPTION
         WHEN OTHERS THEN
              RAISE;	   
END  pro_del_knisot_peilut;
           
PROCEDURE pro_del_hachanot_mechona( p_mispar_ishi IN  TB_PEILUT_OVDIM.mispar_ishi%TYPE,
		 									 p_mispar_sidur IN TB_PEILUT_OVDIM.mispar_sidur%TYPE,
											 p_taarich IN  TB_PEILUT_OVDIM.taarich%TYPE,
		 									   p_shat_hatchala IN  TB_PEILUT_OVDIM.shat_hatchala_sidur%TYPE) IS
BEGIN
	  DELETE TB_PEILUT_OVDIM
        WHERE    mispar_ishi =p_mispar_ishi 
	    AND taarich  = p_taarich
		AND mispar_sidur = p_mispar_sidur
		AND shat_hatchala_sidur = p_shat_hatchala
		AND  TO_NUMBER(SUBSTR(makat_nesia,0,3)) IN (701,711,712);
			   
		EXCEPTION
         WHEN OTHERS THEN
              RAISE;	   
END  pro_del_hachanot_mechona;  

FUNCTION func_get_mispar_tachana(p_mispar_ishi IN  TB_PEILUT_OVDIM.mispar_ishi%TYPE,
                                     p_mispar_sidur IN TB_PEILUT_OVDIM.mispar_sidur%TYPE,
                                     p_taarich IN  TB_PEILUT_OVDIM.taarich%TYPE,
                                     p_shat_hatchala IN  TB_PEILUT_OVDIM.shat_hatchala_sidur%TYPE) RETURN VARCHAR2 AS
 v_tachana VARCHAR2(10);
 v_snif_tnua NUMBER;
 v_oto_num NUMBER;
 v_branch2 VARCHAR2(10);
 BEGIN
    IF SUBSTR(LPAD(p_mispar_sidur,5,'0'),0,2)<>'99' THEN
        v_tachana:=to_char(to_number(SUBSTR(LPAD(p_mispar_sidur,5,'0'),0,2)));     
    ELSE
        BEGIN 
            SELECT SNIF_TNUA INTO v_snif_tnua
            FROM CTB_SNIF_AV sa,PIVOT_PIRTEY_OVDIM po , OVDIM o
            WHERE SA.KOD_SNIF_AV = PO.SNIF_AV
            AND sa.KOD_HEVRA = o.KOD_HEVRA
            AND PO.MISPAR_ISHI=p_mispar_ishi
            AND TRUNC(p_taarich,'MM') BETWEEN PO.ME_TARICH AND NVL(PO.AD_TARICH,p_taarich+1)  
            AND PO.MISPAR_ISHI=O.MISPAR_ISHI;  
        
            EXCEPTION
                WHEN NO_DATA_FOUND THEN  v_snif_tnua:=NULL;
        END;
       
        IF v_snif_tnua IS NOT NULL THEN 
            v_tachana:=TO_CHAR(v_snif_tnua);
        ELSE
        v_tachana:='00';
           /*BEGIN
              SELECT po.OTO_NO INTO v_oto_num
                FROM TB_PEILUT_OVDIM po
                WHERE PO.MISPAR_ISHI=p_mispar_ishi
                    AND PO.TAARICH=p_taarich
                    AND PO.MISPAR_SIDUR=p_mispar_sidur
                    AND p_shat_hatchala=p_shat_hatchala
                    AND PO.OTO_NO IS NOT NULL
                 ORDER BY PO.SHAT_YETZIA;
                 
                 SELECT Branch2 INTO v_branch2  
                 FROM VEHICLE_SPECIFICATIONS 
                 WHERE bus_number=v_oto_num;
                
                if v_branch2 is not null and trim(v_branch2)<>'' then
                   if to_number(v_branch2) between 1010 and 3999 then
                     v_tachana:=SUBSTR(v_branch2,2,2);
                   else
                      v_tachana:='43';
                   end if;
                else
                    v_tachana:='00';
                end if;
                EXCEPTION
                WHEN NO_DATA_FOUND THEN  v_tachana:='00';
            END; */
                                   
        END IF;  
       
    END IF;
 
    RETURN v_tachana;
END func_get_mispar_tachana;

PROCEDURE pro_get_arachim_by_misIshi( p_mispar_ishi IN  PIRTEY_OVDIM.mispar_ishi%TYPE,
																				        p_taarich IN PIRTEY_OVDIM.me_taarich%TYPE,
																	                    p_cur OUT CurType) IS
BEGIN
    OPEN p_cur FOR
    SELECT p.KOD_NATUN, p.ERECH
    FROM PIRTEY_OVDIM p
    WHERE p.MISPAR_ISHI = p_mispar_ishi AND
					p_taarich BETWEEN p.ME_TAARICH AND p.AD_TAARICH;
   
EXCEPTION
         WHEN OTHERS THEN
              RAISE;
     
END pro_get_arachim_by_misIshi;
	
FUNCTION func_get_erech_by_kod_natun(p_mispar_ishi IN  PIRTEY_OVDIM.mispar_ishi%TYPE,
										                                     p_kod_natun IN PIRTEY_OVDIM.kod_natun%TYPE,
										                                     p_taarich IN  PIRTEY_OVDIM.me_taarich%TYPE  ) RETURN VARCHAR2 IS
		 v_erech VARCHAR2(60);																	 
BEGIN
	 			SELECT p.erech
				INTO v_erech
				FROM PIRTEY_OVDIM p
				WHERE p.MISPAR_ISHI =	p_mispar_ishi
					  AND p.KOD_NATUN =  	p_kod_natun
					  AND  p_taarich  BETWEEN p.ME_TAARICH AND p.AD_TAARICH;		
					  
				RETURN v_erech;	  									 
EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END		func_get_erech_by_kod_natun;		


PROCEDURE pro_get_pirtey_oved_letkufot( p_mispar_ishi IN  PIVOT_PIRTEY_OVDIM.mispar_ishi%TYPE,
																				        p_start IN PIVOT_PIRTEY_OVDIM.me_tarich%TYPE,p_end IN PIVOT_PIRTEY_OVDIM.ad_tarich%TYPE,
																	                    p_cur OUT CurType) IS
BEGIN
    OPEN p_cur FOR
		 SELECT p.ME_TARICH,p.AD_TARICH,  i.kod_sector_isuk 
		  FROM CTB_ISUK i ,PIVOT_PIRTEY_OVDIM p WHERE
	              (   p_start  BETWEEN  p.ME_TARICH  AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
					  OR  p_end BETWEEN  p.ME_TARICH  AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
					  OR   p.ME_TARICH>=p_start  AND   NVL(p.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<= p_end )
			AND p.MISPAR_ISHI=p_mispar_ishi
			AND i.kod_isuk=p.isuk
			AND i.kod_sector_isuk IS NOT NULL;
	 
EXCEPTION
         WHEN OTHERS THEN
              RAISE; 
END pro_get_pirtey_oved_letkufot;


PROCEDURE pro_get_meafyen_oved_letkufot( p_mispar_ishi IN  PIVOT_MEAFYENIM_OVDIM.mispar_ishi%TYPE,
																				        p_start IN PIVOT_MEAFYENIM_OVDIM.me_taarich%TYPE,p_end IN PIVOT_MEAFYENIM_OVDIM.ad_taarich%TYPE,
																	                     p_cur OUT CurType) IS
BEGIN
    OPEN p_cur FOR
		 SELECT * FROM PIVOT_MEAFYENIM_OVDIM t
		 WHERE
		     (        p_start BETWEEN  t.ME_TAARICH  AND   NVL(t.ad_TAARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
						  OR  p_end BETWEEN  t.ME_TAARICH  AND   NVL(t.ad_TAARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
						  OR   t.ME_TAARICH>=p_start AND   NVL(t.ad_TAARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<= p_end )
				AND t.MISPAR_ISHI=p_mispar_ishi ;
	 
EXCEPTION
         WHEN OTHERS THEN
              RAISE; 
END pro_get_meafyen_oved_letkufot;

PROCEDURE pro_upd_shgiot_meusharot(p_coll_sidurim_ovdim IN coll_sidurim_ovdim,p_coll_peilut_ovdim  IN coll_obj_peilut_ovdim) IS
BEGIN
 IF (p_coll_peilut_ovdim IS NOT NULL) THEN
        FOR i IN 1..p_coll_peilut_ovdim.COUNT LOOP     
        BEGIN
            -- if shat yetiza changed then update tb_shgiot_meosharot
            --peilut level
            IF (p_coll_peilut_ovdim(i).shat_yetzia <>  p_coll_peilut_ovdim(i).new_shat_yetzia) THEN      
                Pkg_Errors.pro_upd_approval_errors(p_coll_peilut_ovdim(i).mispar_ishi,
                                                   p_coll_peilut_ovdim(i).taarich, 
                                                   p_coll_peilut_ovdim(i).mispar_sidur,
                                                   p_coll_peilut_ovdim(i).shat_hatchala_sidur,
                                                   p_coll_peilut_ovdim(i).shat_yetzia,
                                                   p_coll_peilut_ovdim(i).mispar_knisa,                                 
                                                   p_coll_peilut_ovdim(i).mispar_sidur,
                                                   p_coll_peilut_ovdim(i).new_shat_hatchala_sidur,
                                                   p_coll_peilut_ovdim(i).new_shat_yetzia);
                                               
            END IF;                                                               
        END;                                                      
        END LOOP;
    END IF;
     IF (p_coll_sidurim_ovdim IS NOT NULL) THEN
        FOR i IN 1..p_coll_sidurim_ovdim.COUNT LOOP     
        BEGIN
            -- if shat hatchala changed then update tb_shgiot_meosharot
            --sidur level
            IF (p_coll_sidurim_ovdim(i).shat_hatchala<>  p_coll_sidurim_ovdim(i).new_shat_hatchala) THEN      
                Pkg_Errors.pro_upd_approval_errors(p_coll_sidurim_ovdim(i).mispar_ishi,
                                                   p_coll_sidurim_ovdim(i).taarich, 
                                                   p_coll_sidurim_ovdim(i).mispar_sidur,
                                                   p_coll_sidurim_ovdim(i).new_shat_hatchala,                                  
                                                   p_coll_sidurim_ovdim(i).mispar_sidur,
                                                   p_coll_sidurim_ovdim(i).shat_hatchala);
            END IF;                                                               
        END;                                                      
        END LOOP;
    END IF;
    
   
    
      EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END pro_upd_shgiot_meusharot;
PROCEDURE pro_del_knisot_without_av(p_mispar_ishi IN TB_PEILUT_OVDIM.mispar_ishi%TYPE, p_taarich IN TB_PEILUT_OVDIM.taarich%TYPE,p_meadken_acharon IN TB_PEILUT_OVDIM.meadken_acharon%TYPE)
IS
BEGIN
    DELETE FROM TB_PEILUT_OVDIM o     
    WHERE o.mispar_ishi =  p_mispar_ishi
          AND o.taarich = p_taarich
          AND mispar_knisa>0
          AND NOT EXISTS(SELECT * FROM TB_PEILUT_OVDIM p     
                        WHERE p.mispar_ishi = o.mispar_ishi
                         AND p.taarich = o.taarich
                         AND p.mispar_knisa=0
                         AND p.mispar_sidur = o.mispar_sidur
                         AND p.shat_hatchala_sidur = o.shat_hatchala_sidur
                         AND p.shat_yetzia = o.shat_yetzia
                         AND o.makat_nesia = p.makat_nesia);
 
  IF ((p_meadken_acharon<> p_mispar_ishi) and (p_meadken_acharon>0)) THEN
                    pkg_utils.pro_insert_meadken_acharon(p_mispar_ishi,p_taarich );
        END IF; 
EXCEPTION
         WHEN OTHERS THEN
              RAISE;        
END pro_del_knisot_without_av;

PROCEDURE pro_del_idkuney_rashemet_sidur(p_coll_sidurim_ovdim IN COLL_SIDURIM_OVDIM, p_type_update in number) IS
BEGIN                                         
   IF (p_coll_sidurim_ovdim IS NOT NULL) THEN
       FOR i IN  1..p_coll_sidurim_ovdim.COUNT LOOP
         IF (p_coll_sidurim_ovdim(i).update_object=p_type_update) THEN 
             DELETE FROM TB_IDKUN_RASHEMET
             WHERE
                mispar_ishi     = p_coll_sidurim_ovdim(i).mispar_ishi AND 
                taarich         = p_coll_sidurim_ovdim(i).taarich AND                                
                mispar_sidur    = p_coll_sidurim_ovdim(i).mispar_sidur AND
                shat_hatchala   = p_coll_sidurim_ovdim(i).shat_hatchala;  
         END IF;       
       END LOOP;
   END IF;

EXCEPTION
         WHEN OTHERS THEN
              RAISE;     
END pro_del_idkuney_rashemet_sidur;

PROCEDURE pro_del_idkuney_rashemet_peilt(p_coll_peiluyot_ovdim IN coll_obj_peilut_ovdim) IS
BEGIN
   IF (p_coll_peiluyot_ovdim IS NOT NULL) THEN
       FOR i IN  1..p_coll_peiluyot_ovdim.COUNT LOOP
         DELETE FROM TB_IDKUN_RASHEMET
         WHERE
            mispar_ishi     = p_coll_peiluyot_ovdim(i).mispar_ishi   AND 
            taarich         = p_coll_peiluyot_ovdim(i).taarich       AND                                
            mispar_sidur    = p_coll_peiluyot_ovdim(i).mispar_sidur  AND
            shat_hatchala   = p_coll_peiluyot_ovdim(i).shat_hatchala_sidur AND
            shat_yetzia     = p_coll_peiluyot_ovdim(i).shat_yetzia;  
       END LOOP;
   END IF;
EXCEPTION
         WHEN OTHERS THEN
              RAISE;  
END pro_del_idkuney_rashemet_peilt;  

FUNCTION func_get_next_err_card(p_mispar_ishi in tb_yamey_avoda_ovdim.mispar_ishi%type, p_date in tb_yamey_avoda_ovdim.taarich%type) return varchar2
IS
    v_next_err_date tb_yamey_avoda_ovdim.taarich%type;
BEGIN
    select min(taarich) into v_next_err_date
    from tb_yamey_avoda_ovdim y 
    where y.mispar_ishi = p_mispar_ishi
    and y.taarich > p_date
    and y.status = 0;
    
    if (v_next_err_date is null) then
        return to_char(p_date,'dd/mm/yyyy');
    else
        return to_char(v_next_err_date,'dd/mm/yyyy');
    end if;
EXCEPTION
         WHEN NO_DATA_FOUND THEN
              return to_char(p_date,'dd/mm/yyyy');      
END;

PROCEDURE pro_get_rikuzey_avoda_leoved(p_mispar_ishi IN MATZAV_OVDIM.mispar_ishi%TYPE,p_taarich IN DATE,
                                                        p_cur OUT CurType) IS
    BEGIN
      OPEN p_cur FOR
              SELECT P.BAKASHA_ID, P.TAARICH,P.SUG_CHISHUV, 
                      DECODE(T.HUAVRA_LESACHAR ,1,'כן','')   HUAVRA_LESACHAR, T.TAARICH_HAAVARA_LESACHAR
             FROM TB_RIKUZ_PDF p, TB_BAKASHOT t
             WHERE p.mispar_ishi=p_mispar_ishi
             AND p.taarich=p_taarich
             and P.BAKASHA_ID = T.BAKASHA_ID
            ORDER BY T.TAARICH_HAAVARA_LESACHAR DESC;

  EXCEPTION
       WHEN OTHERS THEN
                RAISE;
END   pro_get_rikuzey_avoda_leoved;
FUNCTION func_is_card_last_updated(p_mispar_ishi in tb_peilut_ovdim.mispar_ishi%type, p_taarich in tb_peilut_ovdim.taarich%type) return number AS
  v_count  number;
BEGIN
    select count(mispar_ishi) into v_count
    from TB_MEADKEN_ACHARON a
    where a.mispar_ishi = p_mispar_ishi
              and a.taarich = p_taarich;
              
return v_count;              
EXCEPTION
       WHEN OTHERS THEN
                return 0;              
END  func_is_card_last_updated;

PROCEDURE get_ovdim_by_Rikuzim(p_preFix IN VARCHAR2,  p_cur OUT CurType) AS 
BEGIN 

OPEN p_cur FOR 
          SELECT DISTINCT p.MISPAR_ISHI
          FROM  TB_RIKUZ_PDF p              
          WHERE P.MISPAR_ISHI Like p_preFix ||  '%' 
          ORDER BY p.MISPAR_ISHI;            
  
        EXCEPTION 
         WHEN OTHERS THEN 
              RAISE;               
  END  get_ovdim_by_Rikuzim;   
  
FUNCTION fun_get_count_no_val_letashlum(p_tar_me IN DATE,p_tar_ad IN DATE,
                                                                    p_maamad IN NUMBER) return number AS
  v_count  number;
BEGIN
    select count(*) into v_count
    from (SELECT ya.mispar_ishi,ya.taarich
                 FROM TB_YAMEY_AVODA_OVDIM ya,
                 TB_SIDURIM_OVDIM so,
                 /* (SELECT  po.maamad,po.mispar_ishi,po.ME_TARICH,po.ad_tarich
               FROM*/ PIVOT_PIRTEY_OVDIM P/*O
                 WHERE  (p_tar_me BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
              OR  p_tar_ad  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
              OR   po.ME_TARICH>=p_tar_me AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<= p_tar_ad ))  p*/
                 WHERE  ya.status=1
             AND  ya.taarich BETWEEN p_tar_me AND p_tar_ad
              and ya.taarich BETWEEN p.ME_TARICH and p.ad_tarich
                 and ya.mispar_ishi=p.mispar_ishi
                  AND (SUBSTR(p.maamad,0,1)=p_maamad  OR p_maamad IS NULL)
                 AND   so.mispar_ishi=ya.mispar_ishi
                 AND so.taarich=ya.taarich
                  AND    so.mispar_sidur<>99200
                  and (SO.LO_LETASHLUM=0 or  SO.LO_LETASHLUM is null)
                   AND  (so.shat_hatchala_letashlum is null or so.shat_gmar_letashlum is null )
              group by ya.mispar_ishi,ya.taarich);
              
return v_count;              
EXCEPTION
       WHEN OTHERS THEN
                return 0;              
END  fun_get_count_no_val_letashlum;


PROCEDURE  pro_get_workcad_no_val_letash(p_tar_me IN DATE,p_tar_ad IN DATE,
                                                                    p_maamad IN NUMBER,  p_cur OUT CurType)  AS
BEGIN
    OPEN p_cur FOR
    select *
    from (SELECT ya.mispar_ishi,ya.taarich
                 FROM TB_YAMEY_AVODA_OVDIM ya,
                 TB_SIDURIM_OVDIM so,
                 /* (SELECT  po.maamad,po.mispar_ishi,po.ME_TARICH,po.ad_tarich
               FROM */PIVOT_PIRTEY_OVDIM P/*O
                 WHERE  (p_tar_me BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
              OR  p_tar_ad  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
              OR   po.ME_TARICH>=p_tar_me AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<= p_tar_ad ))  p*/
                 WHERE  ya.status=1
             AND  ya.taarich BETWEEN p_tar_me AND p_tar_ad
              and ya.taarich BETWEEN p.ME_TARICH and p.ad_tarich
                 and ya.mispar_ishi=p.mispar_ishi
                  AND (SUBSTR(p.maamad,0,1)=p_maamad  OR p_maamad IS NULL)
                 AND   so.mispar_ishi=ya.mispar_ishi
                 AND so.taarich=ya.taarich
                      AND    so.mispar_sidur<>99200
                  and (SO.LO_LETASHLUM=0 or  SO.LO_LETASHLUM is null)
                   AND  (so.shat_hatchala_letashlum is null or so.shat_gmar_letashlum is null )
              group by ya.mispar_ishi,ya.taarich);
              
            
EXCEPTION
       WHEN OTHERS THEN
              RAISE;       
END  pro_get_workcad_no_val_letash;
END Pkg_Ovdim;
/
