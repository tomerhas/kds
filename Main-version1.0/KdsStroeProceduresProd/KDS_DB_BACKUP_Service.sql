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
PROCEDURE pro_get_Ovdim_ShePutru(p_Cur_Piturim OUT CurType,p_num_process IN NUMBER);
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
  p_Cur_Piturim  OUT CurType, 
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
 p_Cur_Piturim  OUT CurType, 
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
 
PROCEDURE pro_get_sug_yechida( p_cur OUT CurType);

PROCEDURE pro_get_yemey_avoda ( p_status_tipul  IN  TB_YAMEY_AVODA_OVDIM.status_tipul%TYPE,  
                                                      p_cur OUT CurType,p_tar_me IN DATE,p_tar_ad IN DATE);

PROCEDURE pro_get_pirtey_ovdim(p_cur OUT CurType);     

 PROCEDURE pro_get_peiluyot_ovdim( p_Cur OUT CurType,p_tar_me IN DATE,p_tar_ad IN DATE);
                       
 PROCEDURE pro_get_Ovdim_ShePutru(p_Cur_Piturim OUT CurType);
 
 PROCEDURE pro_get_buses_details( p_Cur OUT CurType,p_tar_me IN DATE,p_tar_ad IN DATE);
 
 PROCEDURE pro_get_kavim_details(p_cur OUT CurType,p_tar_me IN DATE,p_tar_ad IN DATE);
                            
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
 p_Cur_Piturim  OUT CurType, 
  p_Cur_Buses_Details OUT CurType, 
 p_Cur_Kavim_Details OUT CurType, 
 p_tar_me IN DATE,p_tar_ad IN DATE, 
 p_status_tipul  IN  TB_YAMEY_AVODA_OVDIM.status_tipul%TYPE,
 p_brerat_Mechadal  IN NUMBER, 
 p_Mis_Ishi IN NUMBER,
  p_num_process IN NUMBER  );
	END Pkg_Calc_worker;
/


CREATE OR REPLACE PACKAGE          PKG_FILES AS
/******************************************************************************
   NAME:       PKG_FILES
   PURPOSE:

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        4/29/2012      SaraC       1. Created this package.
******************************************************************************/
   procedure create_egged_taavura(p_tar_me in date , p_tar_ad in date, P_BAKAHA_ID in number); 
   
    PROCEDURE  create_file_visot(p_tar_me IN Date,p_tar_ad IN date,P_BAKAHA_ID in number);
    
    procedure create_WorkHours(p_BakashaId number ,p_tar_me in date , p_tar_ad in date);
    procedure create_Calcalit(p_BakashaId number ,p_tar_me in date , p_tar_ad in date);
    
   PROCEDURE create_file_egged_taavura(p_tar_me IN Date,p_tar_ad IN date,P_BAKAHA_ID IN NUMBER);
   
 FUNCTION fn_get_first_namak_sherut(p_mispar_ishi IN TB_SIDURIM_OVDIM.mispar_ishi%TYPE,
                                                            p_mispar_sidur IN TB_SIDURIM_OVDIM.mispar_sidur%TYPE,
                                                            p_taarich IN TB_SIDURIM_OVDIM.taarich%TYPE,
                                                                p_shat_hatchala IN TB_SIDURIM_OVDIM.shat_hatchala%TYPE) return varchar2;
                                                                
    PROCEDURE create_file_meshek (p_tar_me IN DATE, p_tar_ad IN DATE,P_BAKAHA_ID IN NUMBER,
                                                P_EZOR IN VARCHAR2 DEFAULT NULL, 
                                                P_MIKUM_YECHIDA IN VARCHAR2 DEFAULT NULL,
                                                 P_PREFIX_FILE_NAME IN VARCHAR2);           
    
        PROCEDURE create_file_et_namak(p_tar_me IN DATE, p_tar_ad IN DATE,P_BAKAHA_ID IN NUMBER);
        
        PROCEDURE create_file_mushaley_egged(p_tar_me IN DATE, p_tar_ad IN DATE,P_BAKAHA_ID IN NUMBER );   
       
      PROCEDURE create_file_et_sherut(p_tar_me IN DATE, p_tar_ad IN DATE,P_BAKAHA_ID IN NUMBER ) ;
                                                                                                                                                                 
   PROCEDURE ftp_file(p_from_dir varchar2,p_from_file varchar2,p_to_file_name varchar2);
END PKG_FILES;
/


CREATE OR REPLACE PACKAGE          PKG_RIKUZ_AVODA AS
/******************************************************************************
   NAME:       PKG_RIKUZ_AVODA
   PURPOSE:

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        24/05/2012      meravn       1. Created this package.
******************************************************************************/
 TYPE    CurType      IS    REF  CURSOR;
PROCEDURE pro_get_rechivim_lerikuz(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                                                                        p_taarich IN DATE,
                                                                                             p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                                                                         p_cur OUT CurType);    
 
PROCEDURE pro_rechivim_chodshiim_lerikuz(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                                                               p_taarich IN DATE,
                                                                                             p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                                                                         p_cur OUT CurType);        
                                                                                         
PROCEDURE pro_rechivey_headrut_lerikuz(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                                                             p_taarich IN DATE,
                                                                                             p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                                                                         p_cur OUT CurType);                                                                                               
 PROCEDURE pro_rechivey_shonot_lerikuz(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                                          p_taarich IN DATE,
                                                          p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                                          p_cur OUT CurType);   
                                                          
 function getNochechutChodshit(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                        p_taarich IN DATE,
                                        p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE) return number;                                                         
PROCEDURE Pro_get_num_rechivim(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                                                       p_taarich IN DATE,
                                                                                             p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                                                                         p_cur OUT CurType);        
                                                                                         
                                         

  PROCEDURE pro_get_rikuz_chodshi_temp(    p_Cur_Rechivim_Yomi OUT CurType ,
                                                               p_Cur_Rechivim_Chodshi OUT CurType,
                                                                p_Cur_Rechivey_Headrut OUT CurType,
                                                                 p_Cur_Rechivey_Shonot OUT CurType,
                                                                p_Cur_Num_Rechivim OUT CurType,
                                                                p_mispar_ishi IN NUMBER,
                                                               p_taarich IN DATE,
                                                                p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE );


                                                
 PROCEDURE pro_get_rechivim_lerikuz_tmp(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                                                p_taarich IN DATE,
                                                                p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                                                p_cur OUT CurType);
                                                                                                                                                   
PROCEDURE pro_rechivim_chodshiim_tmp(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                                              p_taarich IN DATE,
                                                              p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                                             p_cur OUT CurType);
 
PROCEDURE pro_rechivey_headrut_tmp(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                                           p_taarich IN DATE,
                                                           p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                                            p_cur OUT CurType)   ;      
 
PROCEDURE pro_rechivey_shonot_tmp(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                                          p_taarich IN DATE,
                                                          p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                                          p_cur OUT CurType);     
                                                          
function getNochechutChodshitTemp(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                        p_taarich IN DATE,
                                        p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE) return number;                                                                                                                                                                                           
PROCEDURE Pro_get_num_rechivim_tmp(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                                             p_taarich IN DATE,
                                                                                             p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                                                                         p_cur OUT CurType);                                                                                                                                            
END PKG_RIKUZ_AVODA;
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

 ELSE
        bakasha_id_prem:= NULL;
        bakasha_id_nihul_prem:=NULL;
        taarich_prem:=NULL;
 END IF;
 
     OPEN p_cur_list FOR
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
                 FROM  (SELECT  DISTINCT taarich,mispar_ishi,
                 (SELECT erech FROM TB_BAKASHOT_PARAMS 
                            WHERE bakasha_id=p_request_id
                            AND param_id=2) chodesh_ibud
                                  FROM TB_CHISHUV_CHODESH_OVDIM c
                                WHERE Bakasha_ID=p_request_id) c,
                                   OVDIM o,
                                (SELECT po.me_tarich,po.ad_tarich ,po.mispar_ishi,po.maamad,po.dirug,po. darga,po.Isuk,po.gil
                               FROM PIVOT_PIRTEY_OVDIM PO)p
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
        --       ORDER BY p.mispar_ishi asc,c.taarich desc
          UNION       
               SELECT DISTINCT c.taarich, c.mispar_ishi,
                         DECODE(SUBSTR(p.maamad,0,1),1,'0013', DECODE(SUBSTR(p.maamad,2,2),'23','2626',  '0026')) mifal,
                         SUBSTR(p.maamad,2,2) maamad,p.gil,SUBSTR(p.maamad,0,1) maamad_rashi , TO_CHAR( TO_DATE('01/06/2012','dd/mm/yyyy'),'mm/yyyy') chodesh_ibud,
                          o.SIFRAT_BIKORET_MI sifrat_bikoret,o.SHEM_MISH,o.SHEM_PRAT, p.dirug,p.darga,o.TEUDAT_ZEHUT,p.Isuk,
                          Pkg_Ovdim.fun_get_meafyen_oved(c.mispar_ishi,53,c.taarich) meafyen53,
                          Pkg_Ovdim.fun_get_meafyen_oved(c.mispar_ishi,83,c.taarich) meafyen83 ,
                            DECODE((SELECT  1 FROM MATZAV_OVDIM m
                                                WHERE m.mispar_ishi=c.mispar_ishi
                                               AND c.TAARICH BETWEEN m.taarich_hatchala AND m.taarich_siyum
                                              AND  m.Kod_matzav='33'),NULL,0,1) mushhe, 2 makor
               FROM TB_CHISHUV_CHODESH_OVDIM c,OVDIM o,
                 (SELECT po.me_tarich,po.ad_tarich ,po.mispar_ishi,po.maamad,po.dirug,po. darga,po.Isuk,po.gil
                               FROM PIVOT_PIRTEY_OVDIM PO)p
               WHERE (  (c.Bakasha_ID=bakasha_id_prem  AND c.KOD_RECHIV IN(115,113,112) )
                           OR   (c.Bakasha_ID=bakasha_id_nihul_prem  AND c.KOD_RECHIV IN(114,117,116,118) ) )
                   AND c.taarich= taarich_prem
                   AND c.mispar_ishi=o.mispar_ishi
                   AND c.mispar_ishi=p.mispar_ishi
                   AND  SUBSTR(p.maamad,0,1) = 2
              --     and c.mispar_ishi =46629
                   AND c.taarich BETWEEN p.me_tarich AND p.ad_tarich
                 AND c.mispar_ishi NOT IN(     SELECT x.mispar_ishi --,c.taarich,c.BAKASHA_ID,c.kod_rechiv,c.erech_rechiv
                        FROM TB_CHISHUV_CHODESH_OVDIM x
                        WHERE x.Bakasha_ID=p_request_id  )  )     
     ORDER BY mispar_ishi ASC,taarich DESC;          

   OPEN p_cur FOR
   SELECT c.mispar_ishi, c.taarich,c.kod_rechiv,c.b1 bakasha_id_1,c.b2 bakasha_id_2,c.erech_rechiv_a,c.erech_rechiv_b,c.erech_rechiv
    FROM
            ( SELECT  NVL(a.taarich,b.taarich) taarich,NVL(a.mispar_ishi,b.mispar_ishi)mispar_ishi,a.BAKASHA_ID b1,NVL(a.erech_rechiv,0) erech_rechiv_a,
                NVL(b.BAKASHA_ID,bakasha_id_last) b2, NVL(b.erech_rechiv,0) erech_rechiv_b,NVL(a.kod_rechiv,b.kod_rechiv) kod_rechiv,(NVL(a.erech_rechiv,0)-NVL(b.erech_rechiv,0))  erech_rechiv
               FROM
                       (
                        SELECT  c1.mispar_ishi,c1.taarich,c1.BAKASHA_ID,c1.kod_rechiv,c1.erech_rechiv,c2.bakasha_id bakasha_id_last
                        FROM    (SELECT c.mispar_ishi,c.taarich,c.BAKASHA_ID,c.kod_rechiv,c.erech_rechiv
                                     FROM TB_CHISHUV_CHODESH_OVDIM c
                                     WHERE c.Bakasha_ID=p_request_id
                                  --     and c.mispar_ishi=28466
                                     ORDER BY c.mispar_ishi,c.kod_rechiv) c1,                        
                                   ( SELECT DISTINCT   a.mispar_ishi, a.Taarich, a.bakasha_id,a.th2 TAARICH_HAAVARA_LESACHAR
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
                                                  WHERE a.th1=a.th2
                                                ) c2
                          WHERE   c1.mispar_ishi= c2.mispar_ishi(+)
                                AND c1.taarich = c2.taarich(+)                    
                         )a
                    FULL JOIN
                        (SELECT c1.mispar_ishi,c1.bakasha_id,c1.Taarich,c1.kod_rechiv,c1.erech_rechiv
                          FROM TB_CHISHUV_CHODESH_OVDIM c1 ,
                                    TB_BAKASHOT b,
                                    (SELECT  DISTINCT taarich,mispar_ishi
                                      FROM TB_CHISHUV_CHODESH_OVDIM c
                                      WHERE Bakasha_ID=p_request_id)c2  ,
                                     ( SELECT DISTINCT   a.mispar_ishi, a.Taarich, a.bakasha_id,a.th2 TAARICH_HAAVARA_LESACHAR
                                        FROM( SELECT  co.mispar_ishi, co.Taarich, co.bakasha_id,b.TAARICH_HAAVARA_LESACHAR th1,
                                                              MAX(B.TAARICH_HAAVARA_LESACHAR) OVER (PARTITION BY co.mispar_ishi,co.Taarich )  th2
                                                 FROM TB_CHISHUV_CHODESH_OVDIM co,TB_BAKASHOT b
                                                 WHERE co.Bakasha_ID<>p_request_id
                                                    AND co.Bakasha_ID<p_request_id
                                                       AND B.BAKASHA_ID = CO.BAKASHA_ID
                                                       AND b.Huavra_Lesachar=1
                                           --      and co.mispar_ishi=28466
                                                  ) a
                                          WHERE a.th1=a.th2
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
  BEGIN
  --icount:=0;
      FOR   v_cur_rec IN  v_cur(p_request_id)
   LOOP
   
  DELETE FROM TB_CHISHUV_SIDUR_OVDIM
    WHERE bakasha_id=v_cur_rec.bakasha_id;

    DELETE FROM TB_CHISHUV_YOMI_OVDIM
    WHERE bakasha_id=v_cur_rec.bakasha_id;

    DELETE FROM TB_CHISHUV_CHODESH_OVDIM
    WHERE bakasha_id=v_cur_rec.bakasha_id;
    
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
-- 20120712    VALUES (15,1,58,SYSDATE,'',10,'',SUBSTR('update_yamim '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
-- 20120712    END;
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
                                    p_comments TEST_MAATEFET.comments%TYPE DEFAULT NULL)                     IS
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
                                --    decode( instr((sysdate-1)-b.taarich_siyum,'-'),1,trunc(sysdate-1), b.taarich_siyum) date_b,
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
                                --    decode( instr((sysdate-1)-b.taarich_siyum,'-'),1,trunc(sysdate-1), b.taarich_siyum) date_b,
                                    a.kod_matzav erech_a,b.kod_matzav erech_b,0 kod
                     FROM
                                    (SELECT * FROM NEW_MATZAV_OVDIM MINUS SELECT * FROM MATZAV_OVDIM) a,
                                    MATZAV_OVDIM b,
                                    OVDIM o
                        WHERE a.mispar_ishi=b.mispar_ishi(+)
                                AND a.mispar_ishi=o.mispar_ishi
                              AND a.taarich_hatchala=b.taarich_hatchala(+)
                              AND a.taarich_hatchala <= TRUNC(SYSDATE);
                        --      and a.mispar_ishi=47906;
                --          )
                --    where mispar_ishi =763;
                --    where RowNum<6;


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
                            AND a.kod_meafyen IN(3,4,5,6,7,8,23,24,25,26,27,28,41,42,44,51,56,61,63,64)
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
                            AND a.kod_meafyen IN(3,4,5,6,7,8,23,24,25,26,27,28,41,42,44,51,56,61,63,64);
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
                            --    decode( instr((sysdate-1)-b.ad_taarich,'-'),1,trunc(sysdate-1), b.ad_taarich) date_b,
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
                        --    and a.mispar_ishi=21470
        UNION
                     SELECT   a.mispar_ishi, a.me_taarich taarich_hatchala,
                                 DECODE(a.ad_taarich,NULL,TRUNC(SYSDATE-1),DECODE( INSTR((SYSDATE-1)-a.ad_taarich,'-'),1,TRUNC(SYSDATE-1), a.ad_taarich)) date_a,
                            --    decode( instr((sysdate-1)-b.ad_taarich,'-'),1,trunc(sysdate-1), b.ad_taarich) date_b,
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
                        --    and a.mispar_ishi=21470;


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
                                    --         decode( instr((sysdate-1)-b.ad_taarich,'-'),1,trunc(sysdate-1), b.ad_taarich) date_b,
                                    DECODE( INSTR((SYSDATE-1)-b.ad_taarich,'-'),1,TRUNC(SYSDATE-1),DECODE(b.ad_taarich,NULL,DECODE(b.erech ,NULL,NULL,TRUNC(SYSDATE-1)),b.ad_taarich) )date_b,
                                                a.erech erech_a,b.erech  erech_b,0 mispar_ishi
                                FROM
                                           (SELECT B.KOD_MEAFYEN,B.ME_TAARICH,B.AD_TAARICH,B.ERECH FROM BREROT_MECHDAL_MEAFYENIM b
                                              MINUS SELECT A.KOD_MEAFYEN,A.ME_TAARICH,A.AD_TAARICH,A.ERECH FROM NEW_BREROT_MECHDAL_MEAFYENIM a) a,
                                            NEW_BREROT_MECHDAL_MEAFYENIM b
                                WHERE a.kod_meafyen=b.kod_meafyen(+)
                                       AND a.me_taarich=b.me_taarich(+)
                                       AND a.me_taarich <= TRUNC(SYSDATE);
                                --       and a.kod_meafyen=5;


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
                                Pkg_Batch.update_oved_im_shinuy_hr    ( p_coll_obj_ovdim_im_shinuy_hr(i).mispar_ishi,
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
                                            Pkg_Batch.update_oved_im_shinuy_hr    ( v_rec.mispar_ishi, TRUNC(v_rec.taarich),'brerot_mechdal');
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
          WHERE     o.mispar_ishi = p_mispar_ishi
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
    --          and ((p1.ad_taarich+1 between b.me_taarich and nvl(b.ad_taarich,sysdate+200)) or
    --                    ( nvl(least(p3.me_taarich-1,last_day(s.taarich)),last_day(s.taarich))  between b.me_taarich and nvl(b.ad_taarich,sysdate+200)))
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
        AND         p4.me_taarich-1 BETWEEN s.taarich AND  LAST_DAY(s.taarich)
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
   --stepc :      add to a whole month where the kod does not exist
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
   --todo  stepd :      pro_get_meafyenim_manygaps use cursors where there is more than one gap 
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
        AND         p4.me_taarich-1 BETWEEN s.taarich AND  LAST_DAY(s.taarich)
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
       SELECT MISPAR_ISHI   ,  Kod_meafyen    ,  Me_taarich  ,  ad_taarich   
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
                (select *  FROM   PIVOT_PIRTEY_OVDIM ppo
                 WHERE      
                       ((ppo.me_tarich <=to_date('01/05/2012','dd/mm/yyyy')    AND ppo.ad_tarich<= LAST_DAY(to_date('01/05/2012','dd/mm/yyyy') )  AND ppo.ad_tarich>=  to_date('01/05/2012','dd/mm/yyyy')    ) OR
                         (ppo.me_tarich>=  to_date('01/05/2012','dd/mm/yyyy')    AND ppo.ad_tarich<= LAST_DAY( to_date('01/05/2012','dd/mm/yyyy')  )  ) OR
                         (ppo.me_tarich>=to_date('01/05/2012','dd/mm/yyyy')    AND ppo.ad_tarich>=  LAST_DAY(to_date('01/05/2012','dd/mm/yyyy') )  AND ppo.me_tarich<=  LAST_DAY( to_date('01/05/2012','dd/mm/yyyy')  )   ) OR
                         (ppo.me_tarich<= to_date('01/05/2012','dd/mm/yyyy')    AND ppo.ad_tarich>= LAST_DAY( to_date('01/05/2012','dd/mm/yyyy')  )  )) ) ppo,
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
                      AND ya.measher_o_mistayeg IS NOT NULL
                      AND NVL(ya.status,-1)<>0
                  /* bet */
     UNION  SELECT DISTINCT   ya.mispar_ishi,ya.taarich
                 FROM TB_YAMEY_AVODA_OVDIM ya,OVDIM o
                 WHERE ya.taarich=TRUNC(p_date)
                     AND  o.mispar_ishi=ya.mispar_ishi
                     AND NOT EXISTS(SELECT 1 FROM TB_SIDURIM_OVDIM so WHERE so.mispar_ishi=ya.mispar_ishi
                                                                                                               AND so.taarich=ya.taarich AND so.meadken_acharon=-12)
                   /*  AND EXISTS(SELECT 1 FROM TB_SIDURIM_OVDIM so WHERE so.mispar_ishi=ya.mispar_ishi
                                                                                                               AND so.taarich=ya.taarich AND so.meadken_acharon<>-12)*/
      /* UNION  SELECT DISTINCT   ya.mispar_ishi,ya.taarich
                 FROM TB_YAMEY_AVODA_OVDIM ya,OVDIM o
                 WHERE ya.taarich between TRUNC(to_date('01/07/2012','dd/mm/yyyy')) and  TRUNC(to_date('25/07/2012','dd/mm/yyyy'))
                     AND  o.mispar_ishi=ya.mispar_ishi
                     AND NOT EXISTS(SELECT 1 FROM TB_SIDURIM_OVDIM so WHERE so.mispar_ishi=ya.mispar_ishi
                                                                                                               AND so.taarich=ya.taarich)
                    and YA.STATUS is null       */                                                                                         
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
          AND y. Measher_O_Mistayeg IS NOT NULL
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
 
 CURSOR Yamim_r IS
  SELECT DISTINCT taarich dt 
FROM  TB_YAMEY_AVODA_OVDIM 
WHERE    taarich BETWEEN GREATEST(TO_DATE('01/07/2012','dd/mm/yyyy'),SYSDATE-(SELECT erech_param FROM TB_PARAMETRIM WHERE kod_param=262)) AND SYSDATE-1;

   BEGIN
FOR  Yamim_r_rec IN  Yamim_r LOOP
BEGIN
   INSERT INTO TB_YAMEY_AVODA_OVDIM
   (mispar_ishi,taarich,lina,measher_o_mistayeg ,taarich_idkun_acharon,meadken_acharon)
   SELECT mispar_ishi,Yamim_r_rec.dt,0,1,SYSDATE,-11
  FROM  NEW_MATZAV_OVDIM  o
   WHERE   kod_matzav IN ('01','03','04','05','06','07','08')
   AND Yamim_r_rec.dt BETWEEN taarich_hatchala AND NVL(taarich_siyum,Yamim_r_rec.dt+1)
   AND NOT EXISTS (SELECT * FROM  TB_YAMEY_AVODA_OVDIM yao
  WHERE yao.taarich=Yamim_r_rec.dt
  AND yao.MISPAR_ISHI=o.mispar_ishi);
EXCEPTION
   WHEN OTHERS THEN
     INSERT INTO TB_LOG_TAHALICH
    VALUES (7,12,1,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Yamim_r_rec.dt) ||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
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
      
    -- v_me_taarich:=to_date('01/06/2012','dd/mm/yyyy');
    -- v_ad_taarich:=to_date('30/06/2012','dd/mm/yyyy');
      
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
             --    ,     TB_MISPAR_ISHI_CHISHUV_BAK t
               WHERE o.status=1
               AND  o.taarich BETWEEN v_me_taarich AND v_ad_taarich
                --and o.MISPAR_ISHI =44965
            -- AND T.NUM_PACK=101--
      --         AND t.mispar_ishi=o.mispar_ishi--
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
       and not( p.dirug=13 and (p.darga=64 or p.darga=70 or p.darga=2))
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
      --       TB_MISPAR_ISHI_CHISHUV_BAK t,
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
    --  AND T.NUM_PACK=101--
      -- AND t.mispar_ishi=o.mispar_ishi--
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
         and not( p.dirug=13 and (p.darga=64 or p.darga=70 or p.darga=2))
       and not( p.dirug=12 and ((p.darga>=21 and p.darga<=30 ) or (p.darga>=62 and p.darga<=72 )))
      AND o.mispar_ishi=y.mispar_ishi ;
  END IF;
  Pkg_Calculation.pro_divide_packets(p_num_processe);
  
  Pkg_Calculation.pro_set_kavim_details_chishuv(p_tar_me,p_tar_ad);
  Pkg_Calculation.pro_upd_yemey_avoda_bechishuv(p_tar_me,p_tar_ad);
  pro_InsertYamimLeTavla(p_tar_me,p_tar_ad,p_num_processe );
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
        --         RAISE;
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
           FROM      OVDIM o,CTB_YECHIDA y, 
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
                                                    p_num_process IN NUMBER,    p_cur OUT CurType,p_tar_me IN DATE,p_tar_ad IN DATE) IS
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
                           KOD_SIBA_LEDIVUCH_YADANI_OUT,
                       NVL ( S.ACHUZ_KNAS_LEPREMYAT_VISA, 0 ) ACHUZ_KNAS_LEPREMYAT_VISA,
                       NVL ( S.ACHUZ_VIZA_BESIKUN, 0 ) ACHUZ_VIZA_BESIKUN,
                       S.MIKUM_SHAON_KNISA, S.MIKUM_SHAON_YETZIA,
                       V_SIDURIM.ZAKAY_LECHISHUV_RETZIFUT, NVL ( S.SUG_SIDUR, 0 ) SUG_SIDUR
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
         WITH all_dates AS 
            (SELECT /*+ materialize */   DISTINCT taarich FROM  kdsadmin.TB_MISPAR_ISHI_CHISHUV 
             WHERE  NUM_PACK = p_num_process)
            SELECT     S.MISPAR_ISHI, C.*
            FROM     kdsadmin.TB_CATALOG_CHISHUV C, 
                     kdsadmin.TB_PEILUT_OVDIM PO, 
                     kdsadmin.TB_MISPAR_ISHI_CHISHUV S
            WHERE         PO.MISPAR_ISHI = S.MISPAR_ISHI
                     AND S.TAARICH IN (SELECT taarich FROM all_dates)
                     AND PO.TAARICH BETWEEN S.TAARICH AND LAST_DAY ( S.TAARICH )
                     AND S.NUM_PACK =p_num_process
                     AND PO.MAKAT_NESIA = C.MAKAT8
                     AND PO.TAARICH = C.ACTIVITY_DATE;
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
        WHERE     po.mispar_ishi=s.MISPAR_ISHI
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

PROCEDURE pro_get_Ovdim_ShePutru(p_Cur_Piturim OUT CurType,p_num_process IN NUMBER) IS
BEGIN
     OPEN p_Cur_Piturim FOR
        SELECT  s.MISPAR_ISHI, s.taarich TAARICH_ME, LAST_DAY( s.taarich) TAARICH_AD
        FROM MATZAV_OVDIM m ,TB_MISPAR_ISHI_CHISHUV s
        WHERE m.Kod_Matzav ='P'
        AND m.mispar_ishi=s.mispar_ishi
         AND S.NUM_PACK= p_num_process
        AND m.Taarich_hatchala  BETWEEN  s.taarich AND LAST_DAY(s.taarich) ;
        
          EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END pro_get_Ovdim_ShePutru;
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
 p_Cur_Piturim  OUT CurType, 
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
 p_Cur_Piturim  OUT CurType, 
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
     
     Pkg_Calculation.pro_get_sug_yechida( p_Cur_Sug_Yechida,p_num_process); 
     
    Pkg_Calculation.pro_get_yemey_avoda (p_status_tipul,p_num_process,p_Cur_Yemey_Avoda,p_tarich_me,p_tarich_ad);                            
     
     Pkg_Calculation.pro_get_pirtey_ovdim(p_Cur_Pirtey_Ovdim,p_num_process); 
     
     Pkg_Calculation.pro_get_meafyeney_ovdim(p_brerat_Mechadal,p_num_process,p_tarich_me,p_tarich_ad,p_Cur_Meafyeney_Ovdim);
     
     Pkg_Calculation.pro_get_peiluyot_ovdim(p_Cur_Peiluyot_Ovdim,p_num_process,p_tarich_me,p_tarich_ad);
 
      Pkg_Utils.pro_get_ctb_mutamut(p_Cur_Mutamut);  
   
    Pkg_Calculation.pro_get_Ovdim_ShePutru(p_Cur_Piturim,p_num_process);
     
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
                           KOD_SIBA_LEDIVUCH_YADANI_OUT,
                       NVL ( S.ACHUZ_KNAS_LEPREMYAT_VISA, 0 ) ACHUZ_KNAS_LEPREMYAT_VISA,
                       NVL ( S.ACHUZ_VIZA_BESIKUN, 0 ) ACHUZ_VIZA_BESIKUN,
                       S.MIKUM_SHAON_KNISA, S.MIKUM_SHAON_YETZIA,
                       V_SIDURIM.ZAKAY_LECHISHUV_RETZIFUT, NVL ( S.SUG_SIDUR, 0 ) SUG_SIDUR
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

PROCEDURE pro_get_Ovdim_ShePutru(p_Cur_Piturim OUT CurType) IS
BEGIN
     OPEN p_Cur_Piturim FOR
        SELECT  s.MISPAR_ISHI, s.taarich TAARICH_ME, last_day( s.taarich) TAARICH_AD
        FROM MATZAV_OVDIM m ,TB_TMP_OVDIM_LECHISHUV s
        WHERE m.Kod_Matzav ='P'
        AND m.mispar_ishi=s.mispar_ishi
        AND m.Taarich_hatchala  between  s.taarich AND LAST_DAY(s.taarich) ;
        
          EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END pro_get_Ovdim_ShePutru;

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
 p_Cur_Sug_Yechida OUT CurType, 
 p_Cur_Yemey_Avoda OUT CurType,
 p_Cur_Pirtey_Ovdim OUT CurType,
 p_Cur_Meafyeney_Ovdim OUT CurType,
 p_Cur_Peiluyot_Ovdim OUT CurType,   
 p_Cur_Mutamut OUT CurType, 
 p_Cur_Piturim  OUT CurType, 
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
     
     Pkg_Calc_Worker.pro_get_sug_yechida( p_Cur_Sug_Yechida);
     
     Pkg_Calc_Worker.pro_get_yemey_avoda (p_status_tipul,p_Cur_Yemey_Avoda,p_tarich_me , p_tarich_ad);                                 
     
     Pkg_Calc_Worker.pro_get_pirtey_ovdim(p_Cur_Pirtey_Ovdim);
     
     Pkg_Ovdim.pro_get_meafyeney_oved_all(p_Mis_Ishi,p_tarich_me ,p_tarich_ad ,  p_brerat_Mechadal,p_Cur_Meafyeney_Ovdim);
     
     Pkg_Calc_Worker.pro_get_peiluyot_ovdim(p_Cur_Peiluyot_Ovdim,p_tarich_me , p_tarich_ad); 
 
     Pkg_Utils.pro_get_ctb_mutamut(p_Cur_Mutamut);  
   
     Pkg_Calc_Worker.pro_get_Ovdim_ShePutru(p_Cur_Piturim);
     
     Pkg_Calc_Worker.pro_get_buses_details(p_Cur_Buses_Details,p_tarich_me , p_tarich_ad);  
 
    Pkg_Calc_Worker.pro_get_kavim_details(p_Cur_Kavim_Details,p_tarich_me , p_tarich_ad); 
     
 
       EXCEPTION
         WHEN OTHERS THEN
              RAISE;   
 END pro_get_netunim_lechishuv;
END Pkg_Calc_worker;
/


CREATE OR REPLACE PACKAGE BODY          PKG_FILES
AS
    FUNCTION fct_MakatDateSql    (  P_STARTDATE IN DATE,P_ENDDATE IN DATE  
 ) RETURN VARCHAR AS
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
RETURN GeneralQry ; 

EXCEPTION 
WHEN OTHERS THEN 
  RAISE;               
  END  fct_MakatDateSql;   



procedure create_egged_taavura(p_tar_me in date , p_tar_ad in date, P_BAKAHA_ID in number)
is
      CURSOR p_cur (
         p_tar_me    tb_sidurim_ovdim.TAARICH%TYPE,
         p_tar_ad    tb_sidurim_ovdim.TAARICH%TYPE)
      IS
select  
        sp.mispar_ishi,  
        sp.shem_mish,  
        sp.shem_prat ,
        s.teur_snif_av,
        S.KOD_SNIF_AV,
        to_char (sp.taarich, 'yyyymmdd') taarich,
        sp.dayofweek,
        sp.mispar_sidur,
        decode(substr(sp.mispar_sidur,0,2),'99',csm.teur_sidur_meychad,css.teur_sidur_avoda) sidur_description,
        decode(PSM.SIDUR_NAMLAK_VISA,null,0,0,0,1) SIDUR_NAMLAK_VISA,
        sp.shat_hatchala,
        sp.SHAT_GMAR,
        sp.sidur_period,
        sp.sum_km,
        sp.SHAT_YETZIA, 
        sp.MAKAT_NESIA,
        C.DESCRIPTION,  
        C.SHILUT, 
        C.MAZAN_TASHLUM,
        C.MAZAN_TICHNUN, 
        C.KM,  
        sp.OTO_NO,  
        V.LICENSE_NUMBER,
        V.BRANCH2,
        sp.SNIF_TNUA,
        C.snif 
from ovdim o,
        tmp_catalog c ,  
        ctb_snif_av s,
        pivot_pirtey_ovdim po,  
        ctb_sug_sidur css,
        ctb_sidurim_meyuchadim csm   ,
        VEHICLE_SPECIFICATIONS v,
        pivot_sidurim_meyuchadim psm,
        (     
        select   
            so.mispar_ishi,so.taarich, so.mispar_sidur,so.shat_hatchala,SO.SHAT_GMAR ,so.sug_sidur,
            (so.shat_gmar - so.shat_hatchala)*1440 sidur_period,to_char (so.taarich, 'D') dayofweek,
            ACTIVITY.SHAT_YETZIA, ACTIVITY.MAKAT_NESIA, ACTIVITY.OTO_NO,  ACTIVITY.SNIF_TNUA,
            sum(km_visa)OVER (partition by  activity.mispar_ishi,activity.MISPAR_SIDUR,activity.taarich,activity.shat_hatchala_sidur) sum_km,
             O.KOD_HEVRA, o.shem_mish,  o.shem_prat 
        from ovdim o,  tb_sidurim_ovdim so,  tb_peilut_ovdim activity ,tb_yamey_avoda_ovdim yao
        where 
                YAO.STATUS <> 0 and 
                YAO.MEASHER_O_MISTAYEG is not NULL AND 
                YAO.MISPAR_ISHI = SO.MISPAR_ISHI and 
                YAO.TAARICH =SO.TAARICH and  
                SO.MISPAR_ISHI= O.MISPAR_ISHI and
                O.KOD_HEVRA = 580     and 
                SO.TAARICH  BETWEEN p_tar_me AND p_tar_ad and 
                --SO.TAARICH between to_date('01/01/2012','dd/mm/yyyy') and to_date('01/02/2012','dd/mm/yyyy')  and 
                SO.MISPAR_ISHI= activity.MISPAR_ISHI (+) and 
                SO.TAARICH = ACTIVITY.TAARICH (+) and 
                SO.MISPAR_SIDUR= ACTIVITY.MISPAR_SIDUR(+) and  
                SO.SHAT_HATCHALA = ACTIVITY.SHAT_HATCHALA_SIDUR(+) and 
                not (ACTIVITY.MAKAT_NESIA like '700%' and length(ACTIVITY.MAKAT_NESIA)=8 ) and -- not vissout     
                ACTIVITY.MISPAR_KNISA = 0   
        ) sp
where
        PO.DIRUG <> 85 and 
        PO.KOD_HEVRA_HASHALA = 4895 and
        o.mispar_ishi= sp.mispar_ishi and
        sp.mispar_ishi = po.mispar_ishi and
        sp.taarich between po.me_tarich and po.ad_tarich   and
        sp.taarich between PSM.me_tarich(+) and PSM.ad_tarich(+)   and
        sp.mispar_sidur = PSM.MISPAR_SIDUR(+) and 
        s.kod_snif_av = po.snif_av and
        S.KOD_HEVRA = O.KOD_HEVRA and 
        css.kod_sidur_avoda(+) = sp.sug_sidur and
        csm.kod_sidur_meyuchad(+) = sp.mispar_sidur and 
        sp.MAKAT_NESIA  = C.MAKAT8(+)  and 
        sp.TAARICH = C.ACTIVITY_DATE(+) and
        nvl(sp.OTO_NO,0) = v.BUS_NUMBER(+) and 
        sp.mispar_sidur =   PSM.MISPAR_SIDUR (+);  


    v_rec         p_cur%ROWTYPE;
    output_file   UTL_FILE.FILE_TYPE;
    v_line        VARCHAR (400);
    v_km          NUMBER;
    v_file_name       VARCHAR(30);
    QryMakatDate VARCHAR2(3500);
   BEGIN
      DBMS_OUTPUT.put_line('start');
      QryMakatDate :=  fct_MakatDateSql(p_tar_me ,p_tar_ad );
      DBMS_OUTPUT.put_line('QryMakatDate:');
      DBMS_OUTPUT.put_line(QryMakatDate);
      Pkg_Reports.pro_Prepare_Catalog_Details(QryMakatDate);
      DBMS_OUTPUT.put_line('after pro_Prepare_Catalog_Details');
     v_file_name:=   'musi_' || lpad(P_BAKAHA_ID,6,0) || TO_CHAR (p_tar_me, 'ddmmyyyy') || '.csv';
      output_file := UTL_FILE.fopen ('KDS_FILES',v_file_name, 'W');
              DBMS_OUTPUT.put_line('start loop');
      FOR v_rec IN p_cur (p_tar_me, p_tar_ad)
      LOOP
            v_line:=';';
        v_line := v_line || v_rec.mispar_ishi || ';' ;
        v_line := v_line || v_rec.shem_mish || ';' ;
        v_line := v_line || v_rec.shem_prat || ';' ;
        v_line := v_line || v_rec.teur_snif_av || ';' ;
        v_line := v_line || v_rec.KOD_SNIF_AV || ';' ;
        v_line := v_line || v_rec.taarich || ';' ;
        v_line := v_line || v_rec.dayofweek || ';' ;
        v_line := v_line || v_rec.mispar_sidur || ';' ;
        v_line := v_line || v_rec.sidur_description || ';' ;
        v_line := v_line || v_rec.SIDUR_NAMLAK_VISA  || ';' ;
        v_line := v_line || to_char(v_rec.shat_hatchala,'dd/mm/yyyy hh24:mi:ss') || ';';
        v_line := v_line || to_char(v_rec.SHAT_GMAR,'dd/mm/yyyy hh24:mi:ss') || ';';
        v_line := v_line || v_rec.sidur_period || ';' ;
        v_line := v_line || v_rec.sum_km || ';' ;
        v_line := v_line || to_char(v_rec.SHAT_YETZIA,'dd/mm/yyyy hh24:mi:ss') || ';';
        v_line := v_line || v_rec.MAKAT_NESIA || ';' ;
        v_line := v_line || v_rec.DESCRIPTION || ';' ;
        v_line := v_line || v_rec.SHILUT || ';' ;
        v_line := v_line || v_rec.MAZAN_TASHLUM || ';' ;
        v_line := v_line || v_rec.MAZAN_TICHNUN || ';' ; 
        v_line := v_line || v_rec.KM || ';' ;
        v_line := v_line || v_rec.OTO_NO || ';' ;
        v_line := v_line || v_rec.LICENSE_NUMBER || ';' ;
        v_line := v_line || v_rec.BRANCH2 || ';' ;
        v_line := v_line || v_rec.SNIF_TNUA|| ';' ;
        v_line := v_line || v_rec.snif || ';' ;

              DBMS_OUTPUT.put_line('v_line:' || v_line);
            UTL_FILE.put_line (output_file, v_line);
      END LOOP;
              DBMS_OUTPUT.put_line('end loop');

      UTL_FILE.fclose (output_file);
      commit;
    ftp_file('KDS_FILES',v_file_name,'filereports/' || v_file_name);
      
   EXCEPTION
      WHEN OTHERS
      THEN
                    DBMS_OUTPUT.put_line('On error , v_line:' || v_line);

         UTL_FILE.put_line (output_file,
                            'Error: ' || SUBSTR (SQLERRM, 1, 100));

         IF UTL_FILE.is_open (output_file)
         THEN
            UTL_FILE.fclose (output_file);
         END IF;

         RAISE;
   END create_egged_taavura;




 
procedure create_file_visot(p_tar_me in date , p_tar_ad in date, P_BAKAHA_ID in number)
is 
      CURSOR p_cur (
         p_tar_me    tb_sidurim_ovdim.TAARICH%TYPE,
         p_tar_ad    tb_sidurim_ovdim.TAARICH%TYPE)
      IS
      select  
            h.mispar_visa,
           TO_CHAR (h.TAARICH, 'yyyymmdd') taarich,
            h.SHAT_HATCHALA,
            h.SHAT_YETZIA,
            (h.SHAT_GMAR - h.SHAT_HATCHALA)*1440 Sidur_Period,
             (h.next_hour -   h.SHAT_YETZIA)*1440  meshech_visa ,
            to_number(h.sum_km, '9999.9') sum_km, 
            h.mispar_ishi --,
            --h.MISPAR_SIDUR,
            --h.MAKAT_NESIA ,
            --h.shat_hatchala_sidur,
            --h.SHAT_GMAR ,
            --h.SHAYAH_LEYOM_KODEM,
            --h.next_hour,
 from ( 
   SELECT   p.mispar_ishi,
            P.MISPAR_SIDUR,
            P.MAKAT_NESIA ,
            p.taarich,
            p.shat_hatchala_sidur,
            p.mispar_visa,
            S.SHAT_HATCHALA,
            S.SHAT_GMAR ,
            P.SHAT_YETZIA,
            S.SHAT_GMAR - S.SHAT_HATCHALA Sidur_Period,
            S.SHAYAH_LEYOM_KODEM,
             sum(km_visa)OVER (partition by  p.mispar_ishi,P.MISPAR_SIDUR,p.taarich,p.shat_hatchala_sidur) sum_km,
             nvl(LEAD (P.SHAT_YETZIA) OVER(partition by  p.mispar_ishi,P.MISPAR_SIDUR,p.taarich,p.shat_hatchala_sidur order by P.SHAT_YETZIA ) , S.SHAT_GMAR )  next_hour
                  
     FROM   tb_sidurim_ovdim s, tb_peilut_ovdim p , pivot_sidurim_meyuchadim psm ,tb_yamey_avoda_ovdim yao
    WHERE      
                YAO.STATUS <> 0 and 
                YAO.MEASHER_O_MISTAYEG is not NULL AND 
                YAO.MISPAR_ISHI = S.MISPAR_ISHI and 
                YAO.TAARICH =S.TAARICH and  
                 S.MISPAR_ISHI = p.MISPAR_ISHI
            AND S.MISPAR_SIDUR = p.MISPAR_SIDUR
            AND s.TAARICH = p.taarich
            AND S.SHAT_HATCHALA = p.shat_hatchala_sidur
            and S.LO_LETASHLUM <> 1 
            and PSM.MISPAR_SIDUR = P.MISPAR_SIDUR
            and PSM.SIDUR_NAMLAK_VISA is not  null
            AND s.TAARICH  between PSM.ME_TARICH and PSM.AD_TARICH
            and S.TAARICH BETWEEN p_tar_me AND p_tar_ad
            --and P.MISPAR_SIDUR = 99110 --for debug 
            --and  S.MISPAR_ISHI = 87744 -- for debug
            --and S.TAARICH = to_date('01/08/2010','dd/mm/yyyy') -- for debug
            order by  P.MISPAR_SIDUR, p.taarich,p.shat_hatchala_sidur, P.MAKAT_NESIA 
 ) h
 where  to_char(h.MAKAT_NESIA) like '50%' ;

      v_rec         p_cur%ROWTYPE;
      output_file   UTL_FILE.FILE_TYPE;
      v_line        VARCHAR (240);
      v_km          NUMBER;
      v_file_name       VARCHAR(30);
   BEGIN
      DBMS_OUTPUT.put_line('start');
     v_file_name:=   'egged_visot_' || lpad(P_BAKAHA_ID,6,0)  || TO_CHAR (p_tar_me, 'ddmmyyyy') || '.csv';
      output_file := UTL_FILE.fopen ('KDS_FILES',v_file_name, 'W');
      FOR v_rec IN p_cur (p_tar_me, p_tar_ad)
      LOOP
              DBMS_OUTPUT.put_line('start loop');
            v_line:=';';
            v_line := v_line || v_rec.mispar_visa || ';' ;
            v_line := v_line || v_rec.taarich        || ';' ;
            v_line := v_line || to_char(v_rec.SHAT_HATCHALA,'dd/mm/yyyy hh24:mi:ss') || ';';
            v_line := v_line || to_char(v_rec.SHAT_YETZIA,'dd/mm/yyyy hh24:mi:ss') || ';';
            v_line := v_line || v_rec.Sidur_Period || ';' ;
            v_line := v_line || v_rec.meshech_visa  || ';' ;
            v_line := v_line || v_rec.sum_km  || ';' ;
            v_line := v_line || v_rec.mispar_ishi  || ';' ;
              DBMS_OUTPUT.put_line('v_line:' || v_line);
            UTL_FILE.put_line (output_file, v_line);
              DBMS_OUTPUT.put_line('end loop');
      END LOOP;

      UTL_FILE.fclose (output_file);
      commit;
    ftp_file('KDS_FILES',v_file_name,'filereports/' || v_file_name);
      
   EXCEPTION
      WHEN OTHERS
      THEN
         UTL_FILE.put_line (output_file,
                            'Error: ' || SUBSTR (SQLERRM, 1, 100));

         IF UTL_FILE.is_open (output_file)
         THEN
            UTL_FILE.fclose (output_file);
         END IF;

         RAISE;
   END create_file_visot;



procedure create_WorkHours(p_BakashaId number ,p_tar_me in date , p_tar_ad in date)
is 
      CURSOR p_cur (
         p_BakashaId    TB_CHISHUV_CHODESH_OVDIM.BAKASHA_ID%TYPE,
         p_tar_me    tb_sidurim_ovdim.TAARICH%TYPE,
         p_tar_ad    tb_sidurim_ovdim.TAARICH%TYPE)
      IS
select 
        O.MISPAR_ISHI,
        O.MIN_OVED,
        PO.MAAMAD,
        PO.GIL,
        PO.EZOR,
        PO.SNIF_AV, 
        CS.TEUR_SNIF_AV,
        PO.ISUK,
        ISUK.TEUR_ISUK,
        TO_CHAR(PO.TCHILAT_AVODA,'yyyymm') StartDate ,
        trim(TO_CHAR(co.R75 ,'99')) R75,
        trim(TO_CHAR(co.R12 ,'9999')) R12,
        trim(TO_CHAR(co.R18/60 ,'999.9')) R18,
        trim(TO_CHAR(co.R32/60 ,'999.9')) R32,
        trim(TO_CHAR(co.R105 ,'999.9')) R105,
        trim(TO_CHAR((co.R192 - co.R250)/60  ,'999.9')) R192_250,
        trim(TO_CHAR(co.R250/60  ,'999.9')) R250,
        trim(TO_CHAR((co.R2 + co.R189 + co.R35 +co.R96 - co.R252)/60 ,'999.99')) R252_REG,
        trim(TO_CHAR((co.R252)/60 ,'999.99')) R252,
        trim(TO_CHAR((co.R251 - co.R190)/60 ,'999.99')) R251_190,
        trim(TO_CHAR((co.R251)/60 ,'999.99')) R251,
        trim(TO_CHAR((co.R2 + co.R189 + co.R35 +co.R96 - (co.R189 + co.R35))/60 ,'999.99')) WEEKTRIP,
        trim(TO_CHAR(co.R189/60 ,'999.99')) R189,
        trim(TO_CHAR(co.R35 ,'999.99')) R35,
        trim(TO_CHAR(co.R36 ,'999.99')) R36,
        trim(TO_CHAR(co.R37 ,'9999.99')) R37,
        trim(TO_CHAR(co.R66 + co.R67 ,'999.99')) R66_67,
        trim(TO_CHAR(co.R202 + co.R30 +co.R26 +co.R203 +co.R28 +co.R29 ,'999.99')) DRIVER_PREMIA,
        trim(TO_CHAR(co.R116 + co.R117 +co.R118 +co.R205 ,'999.99')) TNUA_PREMIA,
        trim(TO_CHAR(co.R96/60 ,'999.99')) R96,
        trim(TO_CHAR(co.R95/60 ,'999.99')) R95,
        trim(TO_CHAR(co.R93/60 ,'999.99')) R93,
        trim(TO_CHAR(co.R94/60 ,'999.99')) R94,
        trim(TO_CHAR(co.R146 + ((co.R131 - co.R53) +co.R32)/60 ,'9999.99')) HOURSPEAR ,
        trim(TO_CHAR(co.R76/60 ,'9999.99')) R76 ,
        trim(TO_CHAR(co.R77/60 ,'9999.99')) R77 ,
        trim(TO_CHAR(co.R78/60 ,'9999.99')) R78 ,
        trim(TO_CHAR(co.R53/60 ,'9999.99')) R53 ,
        trim(TO_CHAR(co.R91 ,'9999.99')) R91 ,
        trim(TO_CHAR(co.R92 ,'9999.99')) R92 ,
        trim(TO_CHAR(co.R55/60 ,'9999.99')) R55 ,
        trim(TO_CHAR(co.R22 ,'99')) R22
from
PIVOT_PIRTEY_OVDIM  Po  ,
(SELECT po.mispar_ishi,MAX(po.ME_TARICH) me_taarich
                       FROM PIVOT_PIRTEY_OVDIM PO
                       WHERE po.isuk IS NOT NULL
                             AND (p_tar_me BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                               OR  p_tar_ad  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                                OR   po.ME_TARICH>= p_tar_me  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<= p_tar_ad  )
                      GROUP BY po.mispar_ishi) RelevantDetails,
        ovdim o, 
        ctb_snif_av cs,
        CTB_ISUK Isuk,
 (
SELECT   cco.MISPAR_ISHI, 
        sum(cco.R2) R2,           sum(cco.R12) R12,         sum(cco.R18) R18,        sum(cco.R22) R22,        sum(cco.R26) R26,        
        sum(cco.R27) R27,        sum(cco.R28) R28,        sum(cco.R29) R29,        sum(cco.R30) R30,        sum(cco.R32) R32,        
        sum(cco.R35) R35,        sum(cco.R36) R36,        sum(cco.R37) R37,        sum(cco.R53) R53,        sum(cco.R55) R55,        
        sum(cco.R60) R60,        sum(cco.R66) R66,        sum(cco.R67) R67,        sum(cco.R75) R75,        sum(cco.R76) R76,        
        sum(cco.R77) R77,        sum(cco.R78) R78,        sum(cco.R91) R91,        sum(cco.R92) R92,        sum(cco.R93) R93,        
        sum(cco.R94) R94,        sum(cco.R95) R95,        sum(cco.R96) R96,        sum(cco.R105) R105,    sum(cco.R112) R112,        
        sum(cco.R113) R113,    sum(cco.R114) R114,     sum(cco.R115) R115,     sum(cco.R116) R116,    sum(cco.R118) R118,        
        sum(cco.R117) R117,        sum(cco.R131) R131,        sum(cco.R146) R146 ,sum(cco.R189) R189,        sum(cco.R190) R190,        
        sum(cco.R192) R192,        sum(cco.R202) R202,        sum(cco.R203) R203,        sum(cco.R204) R204 ,        sum(cco.R205) R205,        
        sum(cco.R250) R250,        sum(cco.R251) R251,        sum(cco.R252) R252
        FROM     ( SELECT   CH.MISPAR_ISHI, 
                    CASE kod_rechiv WHEN 2 THEN Erech_Rechiv ELSE NULL END R2,
                    CASE kod_rechiv WHEN 12 THEN Erech_Rechiv ELSE NULL END R12,
                    CASE kod_rechiv WHEN 18 THEN Erech_Rechiv ELSE NULL END R18,
                    CASE kod_rechiv WHEN 22 THEN Erech_Rechiv ELSE NULL END R22,
                    CASE kod_rechiv WHEN 26 THEN Erech_Rechiv ELSE NULL END R26,
                    CASE kod_rechiv WHEN 27 THEN Erech_Rechiv ELSE NULL END R27,
                    CASE kod_rechiv WHEN 28 THEN Erech_Rechiv ELSE NULL END R28,
                    CASE kod_rechiv WHEN 29 THEN Erech_Rechiv ELSE NULL END R29,
                    CASE kod_rechiv WHEN 30 THEN Erech_Rechiv ELSE NULL END R30,
                    CASE kod_rechiv WHEN 32 THEN Erech_Rechiv ELSE NULL END R32,
                    CASE kod_rechiv WHEN 35 THEN Erech_Rechiv ELSE NULL END R35,
                    CASE kod_rechiv WHEN 36 THEN Erech_Rechiv ELSE NULL END R36,
                    CASE kod_rechiv WHEN 37 THEN Erech_Rechiv ELSE NULL END R37,
                    CASE kod_rechiv WHEN 53 THEN Erech_Rechiv ELSE NULL END R53,
                    CASE kod_rechiv WHEN 55 THEN Erech_Rechiv ELSE NULL END R55,
                    CASE kod_rechiv WHEN 60 THEN Erech_Rechiv ELSE NULL END R60,
                    CASE kod_rechiv WHEN 66 THEN Erech_Rechiv ELSE NULL END R66,
                    CASE kod_rechiv WHEN 67 THEN Erech_Rechiv ELSE NULL END R67,
                    CASE kod_rechiv WHEN 75 THEN Erech_Rechiv ELSE NULL END R75,
                    CASE kod_rechiv WHEN 76 THEN Erech_Rechiv ELSE NULL END R76,
                    CASE kod_rechiv WHEN 77 THEN Erech_Rechiv ELSE NULL END R77,
                    CASE kod_rechiv WHEN 78 THEN Erech_Rechiv ELSE NULL END R78,
                    CASE kod_rechiv WHEN 91 THEN Erech_Rechiv ELSE NULL END R91,
                    CASE kod_rechiv WHEN 92 THEN Erech_Rechiv ELSE NULL END R92,
                    CASE kod_rechiv WHEN 93 THEN Erech_Rechiv ELSE NULL END R93,
                    CASE kod_rechiv WHEN 94 THEN Erech_Rechiv ELSE NULL END R94,
                    CASE kod_rechiv WHEN 95 THEN Erech_Rechiv ELSE NULL END R95,
                    CASE kod_rechiv WHEN 96 THEN Erech_Rechiv ELSE NULL END R96,
                    CASE kod_rechiv WHEN 105 THEN Erech_Rechiv ELSE NULL END R105,
                    CASE kod_rechiv WHEN 112 THEN Erech_Rechiv ELSE NULL END R112,
                    CASE kod_rechiv WHEN 113 THEN Erech_Rechiv ELSE NULL END R113,
                    CASE kod_rechiv WHEN 114 THEN Erech_Rechiv ELSE NULL END R114,
                    CASE kod_rechiv WHEN 115 THEN Erech_Rechiv ELSE NULL END R115,
                    CASE kod_rechiv WHEN 116 THEN Erech_Rechiv ELSE NULL END R116,
                    CASE kod_rechiv WHEN 118 THEN Erech_Rechiv ELSE NULL END R118,
                    CASE kod_rechiv WHEN 117 THEN Erech_Rechiv ELSE NULL END R117,
                    CASE kod_rechiv WHEN 131 THEN Erech_Rechiv ELSE NULL END R131,
                    CASE kod_rechiv WHEN 146 THEN Erech_Rechiv ELSE NULL END R146,
                    CASE kod_rechiv WHEN 189 THEN Erech_Rechiv ELSE NULL END R189,
                    CASE kod_rechiv WHEN 190 THEN Erech_Rechiv ELSE NULL END R190,
                    CASE kod_rechiv WHEN 192 THEN Erech_Rechiv ELSE NULL END R192,
                    CASE kod_rechiv WHEN 202 THEN Erech_Rechiv ELSE NULL END R202,
                    CASE kod_rechiv WHEN 203 THEN Erech_Rechiv ELSE NULL END R203,
                    CASE kod_rechiv WHEN 204 THEN Erech_Rechiv ELSE NULL END R204,
                    CASE kod_rechiv WHEN 205 THEN Erech_Rechiv ELSE NULL END R205,
                    CASE kod_rechiv WHEN 250 THEN Erech_Rechiv ELSE NULL END R250,
                    CASE kod_rechiv WHEN 251 THEN Erech_Rechiv ELSE NULL END R251,
                    CASE kod_rechiv WHEN 252 THEN Erech_Rechiv ELSE NULL END R252
        FROM TB_CHISHUV_CHODESH_OVDIM Ch
        WHERE
                    --CH.TAARICH between to_date('01/05/2012','dd/MM/yyyy') and to_date('30/05/2012','dd/MM/yyyy') 
                    Ch.Taarich BETWEEN p_tar_me  AND p_tar_ad 
                    --AND Ch.Bakasha_ID = 7643
                    AND Ch.Bakasha_ID = p_BakashaId
                    ) cco
 group by cco.MISPAR_ISHI
) co
where 
        Po.mispar_ishi = o.mispar_ishi   and 
        Po.mispar_ishi = RelevantDetails.mispar_ishi and  
        Po.ME_TARICH = RelevantDetails.me_taarich and 
        CS.EZOR = PO.EZOR and 
        CS.KOD_HEVRA = O.KOD_HEVRA and
        CS.KOD_SNIF_AV = PO.SNIF_AV and
        PO.MISPAR_ISHI = O.MISPAR_ISHI and
        CO.MISPAR_ISHI = PO.MISPAR_ISHI and 
        ISUK.KOD_HEVRA = CS.KOD_HEVRA and 
        ISUK.KOD_ISUK = PO.ISUK ;

      v_rec         p_cur%ROWTYPE;
      output_file   UTL_FILE.FILE_TYPE;
      v_line        VARCHAR (1000);
      v_km          NUMBER;
      v_file_name       VARCHAR(30);
   BEGIN
      DBMS_OUTPUT.put_line('start');
     v_file_name:=   'oved_' || lpad(p_BakashaId,6,0)  || '_'  || TO_CHAR (p_tar_me, 'ddmmyyyy') || '.csv';
      output_file := UTL_FILE.fopen ('KDS_FILES',v_file_name, 'W');
      FOR v_rec IN p_cur (p_BakashaId,p_tar_me, p_tar_ad)
      LOOP
      v_line := '';
              DBMS_OUTPUT.put_line('start loop');
        v_line := v_line || v_rec.MISPAR_ISHI || ';' ;
    v_line := v_line || v_rec.MIN_OVED || ';' ;
        v_line := v_line || v_rec.MAAMAD || ';' ;
        v_line := v_line || v_rec.GIL || ';' ;
        v_line := v_line || v_rec.EZOR || ';' ;
        v_line := v_line || v_rec.SNIF_AV || ';' ;
        v_line := v_line || v_rec.TEUR_SNIF_AV || ';' ;
        v_line := v_line || v_rec.ISUK || ';' ;
        v_line := v_line || v_rec.TEUR_ISUK || ';' ;
        v_line := v_line || v_rec.StartDate || ';' ;
        v_line := v_line || v_rec.R75 || ';' ;
        v_line := v_line || v_rec.R12 || ';' ;
        v_line := v_line || v_rec.R18 || ';' ;
        v_line := v_line || v_rec.R32 || ';' ;
        v_line := v_line || v_rec.R105 || ';' ;
        v_line := v_line || v_rec.R192_250 || ';' ;
        v_line := v_line || v_rec.R250 || ';' ;
        v_line := v_line || v_rec.R252_REG || ';' ;
        v_line := v_line || v_rec.R252 || ';' ;
        v_line := v_line || v_rec.R251_190 || ';' ;
        v_line := v_line || v_rec.R251 || ';' ;
        v_line := v_line || v_rec.WEEKTRIP || ';' ;
        v_line := v_line || v_rec.R189 || ';' ;
        v_line := v_line || v_rec.R35 || ';' ;
        v_line := v_line || v_rec.R36 || ';' ;
        v_line := v_line || v_rec.R37 || ';' ;
        v_line := v_line || v_rec.R66_67|| ';' ;
        v_line := v_line || v_rec.DRIVER_PREMIA || ';' ;
        v_line := v_line || v_rec.TNUA_PREMIA || ';' ;
        v_line := v_line || v_rec.R96 || ';' ;
        v_line := v_line || v_rec.R95 || ';' ;
        v_line := v_line || v_rec.R93 || ';' ;
        v_line := v_line || v_rec.R94|| ';' ;
        v_line := v_line || v_rec.HOURSPEAR || ';' ;
        v_line := v_line || v_rec.R76 || ';' ;
        v_line := v_line || v_rec.R77 || ';' ;
        v_line := v_line || v_rec.R78 || ';' ;
        v_line := v_line || v_rec.R53 || ';' ;
        v_line := v_line || v_rec.R91 || ';' ;
        v_line := v_line || v_rec.R92 || ';' ;
        v_line := v_line || v_rec.R55 || ';' ;
        v_line := v_line || v_rec.R22|| ';' ;
              DBMS_OUTPUT.put_line('v_line:' || v_line);
            UTL_FILE.put_line (output_file, v_line);
              DBMS_OUTPUT.put_line('end loop');
      END LOOP;

      UTL_FILE.fclose (output_file);
      commit;
    ftp_file('KDS_FILES',v_file_name,'filereports/' || v_file_name);
      
   EXCEPTION
      WHEN OTHERS
      THEN
         UTL_FILE.put_line (output_file,                            'Error: ' || SUBSTR (SQLERRM, 1, 100));

         IF UTL_FILE.is_open (output_file)
        THEN
            UTL_FILE.fclose (output_file);
        END IF;

         RAISE;
   END create_WorkHours;

 


procedure create_Calcalit(p_BakashaId number ,p_tar_me in date , p_tar_ad in date)
is 
      CURSOR p_cur (
         p_BakashaId    TB_CHISHUV_CHODESH_OVDIM.BAKASHA_ID%TYPE,
         p_tar_me    tb_sidurim_ovdim.TAARICH%TYPE,
         p_tar_ad    tb_sidurim_ovdim.TAARICH%TYPE)
      IS
select 
        O.MISPAR_ISHI,
        O.SHEM_MISH,
        O.SHEM_PRAT,
        PO.MAAMAD,
        PO.GIL,
        PO.EZOR,
        PO.SNIF_AV, 
        CS.TEUR_SNIF_AV,
        PO.ISUK,
        ISUK.TEUR_ISUK,
        PO.DIRUG,
        TO_CHAR(p_tar_me,'yyyymm') MonthData ,
        trim(TO_CHAR(co.R75 ,'99')) R75,
        trim(TO_CHAR(co.R18/60 ,'999.9')) R18,
        trim(TO_CHAR(co.R32/60 ,'999.9')) R32,
        trim(TO_CHAR(co.R105 ,'999.9')) R105,
        trim(TO_CHAR((co.R192 - co.R250)/60  ,'999.9')) R192_250,
        trim(TO_CHAR(co.R250/60  ,'999.9')) R250,
        trim(TO_CHAR((co.R2 + co.R189 + co.R35 +co.R96 - co.R252)/60 ,'999.99')) R252_REG,
        trim(TO_CHAR((co.R252)/60 ,'999.99')) R252,
        trim(TO_CHAR((co.R251 - co.R190)/60 ,'999.99')) R251_190,
        trim(TO_CHAR((co.R251)/60 ,'999.99')) R251,
        trim(TO_CHAR(co.R202 + co.R30 + co.R203 +co.R28 + co.R29 ,'999.99')) premia,
        trim(TO_CHAR(co.R116 + co.R118 + co.R117 +co.R205 ,'999.99')) premiaMihul,
        trim(TO_CHAR(co.R146 + ((co.R131 - co.R53) +co.R32)/60 ,'9999.99')) HOURSPEAR ,
        trim(TO_CHAR(co.R76/60 ,'9999.99')) R76 ,
        trim(TO_CHAR(co.R77/60 ,'9999.99')) R77 ,
        trim(TO_CHAR(co.R78/60 ,'9999.99')) R78 ,
        trim(TO_CHAR(co.R55/60 ,'9999.99')) R55 ,
        trim(TO_CHAR(case when( PO.ISUK > 500 and PO.ISUK <600) then co.R49 else null end ,'9999')) R49 ,
        trim(TO_CHAR(co.R94/60 ,'999.99')) R94,
        trim(TO_CHAR(co.R96/60 ,'9999')) R96,
        trim(TO_CHAR(co.R91 ,'9999.99')) R91 ,
        trim(TO_CHAR(co.R92 ,'9999.99')) R92 
from
PIVOT_PIRTEY_OVDIM  Po  ,
(SELECT po.mispar_ishi,MAX(po.ME_TARICH) me_taarich
                       FROM PIVOT_PIRTEY_OVDIM PO
                       WHERE po.isuk IS NOT NULL
                             AND (to_date('01/09/2010','dd/MM/yyyy')  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                               OR  to_date('30/09/2010','dd/MM/yyyy')  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
                                OR   po.ME_TARICH>= to_date('01/09/2010','dd/MM/yyyy')  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<= to_date('30/09/2010','dd/MM/yyyy')  )
                      GROUP BY po.mispar_ishi) RelevantDetails,
        ovdim o, 
        ctb_snif_av cs,
        CTB_ISUK Isuk,
 (
SELECT   cco.MISPAR_ISHI, 
        sum(cco.R2) R2,           sum(cco.R18) R18,        sum(cco.R26) R26,        sum(cco.R28) R28,        
        sum(cco.R29) R29,        sum(cco.R30) R30,        sum(cco.R32) R32,       sum(cco.R35) R35,        
        sum(cco.R49) R49,        sum(cco.R53) R53,        sum(cco.R55) R55,       sum(cco.R75) R75,        
        sum(cco.R76) R76,        sum(cco.R77) R77,        sum(cco.R78) R78,        sum(cco.R91) R91,        
        sum(cco.R92) R92,       sum(cco.R94) R94,        sum(cco.R96) R96,        sum(cco.R105) R105,        
        sum(cco.R116) R116,                     sum(cco.R117) R117,                     sum(cco.R118) R118,
        sum(cco.R131) R131,        sum(cco.R146) R146 ,sum(cco.R189) R189,        sum(cco.R190) R190,        
        sum(cco.R192) R192,        sum(cco.R202) R202,        sum(cco.R203) R203,                sum(cco.R205) R205,
        sum(cco.R250) R250,        sum(cco.R251) R251,        sum(cco.R252) R252
        FROM     ( SELECT   CH.MISPAR_ISHI, 
                    CASE kod_rechiv WHEN 2 THEN Erech_Rechiv ELSE NULL END R2,
                    CASE kod_rechiv WHEN 18 THEN Erech_Rechiv ELSE NULL END R18,
                    CASE kod_rechiv WHEN 26 THEN Erech_Rechiv ELSE NULL END R26,
                    CASE kod_rechiv WHEN 28 THEN Erech_Rechiv ELSE NULL END R28,
                    CASE kod_rechiv WHEN 29 THEN Erech_Rechiv ELSE NULL END R29,
                    CASE kod_rechiv WHEN 30 THEN Erech_Rechiv ELSE NULL END R30,
                    CASE kod_rechiv WHEN 32 THEN Erech_Rechiv ELSE NULL END R32,
                    CASE kod_rechiv WHEN 35 THEN Erech_Rechiv ELSE NULL END R35,
                    CASE kod_rechiv WHEN 49 THEN Erech_Rechiv ELSE NULL END R49,
                    CASE kod_rechiv WHEN 53 THEN Erech_Rechiv ELSE NULL END R53,
                    CASE kod_rechiv WHEN 55 THEN Erech_Rechiv ELSE NULL END R55,
                    CASE kod_rechiv WHEN 75 THEN Erech_Rechiv ELSE NULL END R75,
                    CASE kod_rechiv WHEN 76 THEN Erech_Rechiv ELSE NULL END R76,
                    CASE kod_rechiv WHEN 77 THEN Erech_Rechiv ELSE NULL END R77,
                    CASE kod_rechiv WHEN 78 THEN Erech_Rechiv ELSE NULL END R78,
                    CASE kod_rechiv WHEN 91 THEN Erech_Rechiv ELSE NULL END R91,
                    CASE kod_rechiv WHEN 92 THEN Erech_Rechiv ELSE NULL END R92,
                    CASE kod_rechiv WHEN 94 THEN Erech_Rechiv ELSE NULL END R94,
                    CASE kod_rechiv WHEN 96 THEN Erech_Rechiv ELSE NULL END R96,
                    CASE kod_rechiv WHEN 105 THEN Erech_Rechiv ELSE NULL END R105,
                    CASE kod_rechiv WHEN 116 THEN Erech_Rechiv ELSE NULL END R116,
                    CASE kod_rechiv WHEN 118 THEN Erech_Rechiv ELSE NULL END R118,
                    CASE kod_rechiv WHEN 117 THEN Erech_Rechiv ELSE NULL END R117,
                    CASE kod_rechiv WHEN 131 THEN Erech_Rechiv ELSE NULL END R131,
                    CASE kod_rechiv WHEN 146 THEN Erech_Rechiv ELSE NULL END R146,
                    CASE kod_rechiv WHEN 189 THEN Erech_Rechiv ELSE NULL END R189,
                    CASE kod_rechiv WHEN 190 THEN Erech_Rechiv ELSE NULL END R190,
                    CASE kod_rechiv WHEN 192 THEN Erech_Rechiv ELSE NULL END R192,
                    CASE kod_rechiv WHEN 202 THEN Erech_Rechiv ELSE NULL END R202,
                    CASE kod_rechiv WHEN 203 THEN Erech_Rechiv ELSE NULL END R203,
                    CASE kod_rechiv WHEN 204 THEN Erech_Rechiv ELSE NULL END R204,
                    CASE kod_rechiv WHEN 205 THEN Erech_Rechiv ELSE NULL END R205,
                    CASE kod_rechiv WHEN 250 THEN Erech_Rechiv ELSE NULL END R250,
                    CASE kod_rechiv WHEN 251 THEN Erech_Rechiv ELSE NULL END R251,
                    CASE kod_rechiv WHEN 252 THEN Erech_Rechiv ELSE NULL END R252
        FROM TB_CHISHUV_CHODESH_OVDIM Ch
        WHERE
                    --CH.TAARICH between to_date('01/09/2010','dd/MM/yyyy') and to_date('30/09/2010','dd/MM/yyyy') 
                    Ch.Taarich BETWEEN p_tar_me  AND p_tar_ad 
                    --AND Ch.Bakasha_ID = 7128
                    AND Ch.Bakasha_ID = p_BakashaId
                    ) cco
 group by cco.MISPAR_ISHI
) co
where 
        Po.mispar_ishi = o.mispar_ishi   and 
        Po.mispar_ishi = RelevantDetails.mispar_ishi and  
        Po.ME_TARICH = RelevantDetails.me_taarich and 
        CS.EZOR = PO.EZOR and 
        CS.KOD_HEVRA = O.KOD_HEVRA and
        CS.KOD_SNIF_AV = PO.SNIF_AV and
        CS.KOD_HEVRA = 4895 and 
        PO.MISPAR_ISHI = O.MISPAR_ISHI and
        CO.MISPAR_ISHI = PO.MISPAR_ISHI and 
        ISUK.KOD_HEVRA = CS.KOD_HEVRA and 
        ISUK.KOD_ISUK = PO.ISUK ;
 

      v_rec         p_cur%ROWTYPE;
      output_file   UTL_FILE.FILE_TYPE;
      v_line        VARCHAR (1000);
      v_km          NUMBER;
      v_file_name       VARCHAR(30);
   BEGIN
      DBMS_OUTPUT.put_line('start');
     v_file_name:=   'tast_' || lpad(p_BakashaId,6,0)  || '_'  || TO_CHAR (p_tar_me, 'ddmmyyyy') || '.csv';
      output_file := UTL_FILE.fopen ('KDS_FILES',v_file_name, 'W');
      FOR v_rec IN p_cur (p_BakashaId,p_tar_me, p_tar_ad)
      LOOP
      v_line := '';
              DBMS_OUTPUT.put_line('start loop');
              
        v_line := v_line || v_rec.MISPAR_ISHI || ';' ;
        v_line := v_line || v_rec.SHEM_MISH || ';' ;
        v_line := v_line || v_rec.SHEM_PRAT || ';' ;
        v_line := v_line || v_rec.MAAMAD || ';' ;
        v_line := v_line || v_rec.GIL || ';' ;
        v_line := v_line || v_rec.EZOR || ';' ;
        v_line := v_line || v_rec.SNIF_AV || ';' ;
        v_line := v_line || v_rec.TEUR_SNIF_AV || ';' ;
        v_line := v_line || v_rec.ISUK || ';' ;
        v_line := v_line || v_rec.TEUR_ISUK || ';' ;
        v_line := v_line || v_rec.DIRUG || ';' ;
        v_line := v_line || v_rec.MonthData || ';' ;
        v_line := v_line || v_rec.R75 || ';' ;
        v_line := v_line || v_rec.R18 || ';' ;
        v_line := v_line || v_rec.R32 || ';' ;
        v_line := v_line || v_rec.R105 || ';' ;
        v_line := v_line || v_rec.R192_250 || ';' ;
        v_line := v_line || v_rec.R250 || ';' ;
        v_line := v_line || v_rec.R252_REG || ';' ;
        v_line := v_line || v_rec.R252|| ';' ;
        v_line := v_line || v_rec.R251_190 || ';' ;
        v_line := v_line || v_rec.R251 || ';' ;
        v_line := v_line || v_rec.premia|| ';' ;
        v_line := v_line || v_rec.premiaMihul || ';' ;
        v_line := v_line || v_rec.HOURSPEAR || ';' ;
        v_line := v_line || v_rec.R76 || ';' ;
        v_line := v_line || v_rec.R77 || ';' ;
        v_line := v_line || v_rec.R78 || ';' ;
        v_line := v_line || v_rec.R55 || ';' ;
        v_line := v_line || v_rec.R49 || ';' ;
        v_line := v_line || v_rec.R94 || ';' ;
        v_line := v_line || v_rec.R96 || ';' ;
        v_line := v_line || v_rec.R91 || ';' ;
        v_line := v_line || v_rec.R92 || ';' ;

              
              
              
              DBMS_OUTPUT.put_line('v_line:' || v_line);
            UTL_FILE.put_line (output_file, v_line);
              DBMS_OUTPUT.put_line('end loop');
      END LOOP;

      UTL_FILE.fclose (output_file);
      commit;
    ftp_file('KDS_FILES',v_file_name,'filereports/' || v_file_name);
      
   EXCEPTION
      WHEN OTHERS
      THEN
         UTL_FILE.put_line (output_file,                            'Error: ' || SUBSTR (SQLERRM, 1, 100));

         IF UTL_FILE.is_open (output_file)
        THEN
            UTL_FILE.fclose (output_file);
        END IF;

         RAISE;
   END create_Calcalit;



 
 
   /******************************************************************************
      NAME:       PKG_FILES
      PURPOSE:

      REVISIONS:
      Ver        Date        Author           Description
      ---------  ----------  ---------------  ------------------------------------
      1.0        4/29/2012      SaraC       1. Created this package body.
   ******************************************************************************/

   PROCEDURE create_file_egged_taavura (p_tar_me IN DATE, p_tar_ad IN DATE,P_BAKAHA_ID IN NUMBER)
   IS
      CURSOR p_cur (
         p_tar_me    tb_sidurim_ovdim.TAARICH%TYPE,
         p_tar_ad    tb_sidurim_ovdim.TAARICH%TYPE)
      IS
        select a.*,    (select sum(c.km) from   tmp_catalog c ,tb_peilut_ovdim p
                      where p.MAKAT_NESIA  = C.MAKAT8(+)   
                    and p.TAARICH = C.ACTIVITY_DATE(+) 
                     AND a.taarich = p.taarich
                      AND a.mispar_sidur= p.mispar_sidur
                      AND a.shat_hatchala = p.shat_hatchala_sidur) sum_km,
                                     (select c.snif from   tmp_catalog c ,tb_peilut_ovdim p
                      where p.MAKAT_NESIA  = C.MAKAT8(+)   
                    and p.TAARICH = C.ACTIVITY_DATE(+) 
                     AND a.taarich = p.taarich
                      AND a.mispar_sidur= p.mispar_sidur
                      AND a.shat_hatchala = p.shat_hatchala_sidur
                    and p.makat_nesia = first_makat) snif_metugbar from
        ( SELECT S.TAARICH taarich_source,
                s.mispar_ishi,
                s.mispar_sidur,
                S.SHAT_HATCHALA,
                S.SHAT_GMAR,
                S.SECTOR_VISA,
                S.HASHLAMA,
                S.CHARIGA,
                mp.snif_av,
                SUBSTR (T.BRANCH2, 2, 2) snif_mashar,
                t.oto_num,
                PPO.Mikum_yechida,
                t.LICENSE_NUMBER,
                substr(mp.TEUR_SNIF_AV,0,20) TEUR_SNIF_AV,
                DECODE (sm.headrut_type_kod,
                        NULL, (s.shat_gmar - s.shat_hatchala)*60*24,
                      0)
                   dakot_nochehut,
                DECODE (sm.headrut_type_kod,
                        NULL, 0,
                        (s.shat_gmar - s.shat_hatchala)*60*24)
                   dakot_nochehut_headrut,
                DECODE (SUBSTR (s.mispar_sidur, 0, 2),
                        99, NVL (mp.snif_tnua, t.snif_tnua),
                        mp.snif_tnua)
                   snif_tnua,
                 substr(O.SHEM_MISH,0,8) SHEM_MISH,
                substr(O.SHEM_PRAT,0,6) SHEM_PRAT,
                PPO.GIL,
                PPO.MAAMAD,
                PPO.EZOR,
                PPO.ISUK,
                TO_CHAR (S.TAARICH, 'yyyymmdd') taarich,
                substr(DECODE (SUBSTR (s.mispar_sidur, 0, 2),
                        99, (SELECT m.TEUR_SIDUR_MEYCHAD
                               FROM CTB_SIDURIM_MEYUCHADIM m
                              WHERE m.KOD_SIDUR_MEYUCHAD = s.mispar_sidur),
                        (SELECT d.TEUR_SIDUR_AVODA
                           FROM CTB_SUG_SIDUR d
                          WHERE d.KOD_SIDUR_AVODA = s.sug_sidur)),0,20)
                   sidur_name,
                TO_CHAR (s.taarich, 'D') dayOfWeek,
                substr(i.teur_isuk,0,20) teur_isuk,
                m.mispar_visa,
                      pkg_files.fn_get_first_namak_sherut(s.mispar_ishi, s.mispar_sidur,s.TAARICH, S.SHAT_HATCHALA) first_makat
           FROM tb_sidurim_ovdim s,
              tb_yamey_avoda_ovdim y,
                ( (SELECT * FROM VIW_ET_SNIF_SIDUR_MEYUCHAD)
                 UNION ALL
                 (SELECT * FROM VIW_ET_SNIF_SIDUREY_MAPA)) mp,
                (SELECT *
                   FROM pivot_pirtey_ovdim
                  WHERE ME_TARICH BETWEEN p_tar_me AND p_tar_ad) ppo,
                ovdim o,
                ctb_isuk i,
                VIW_SNIF_TNUA_FROM_TNUA t,
                VIW_MISPAR_VISA_IN_SIDUR_VISA m,
                VIW_SIDURIM_MEYUCHADIM sm
          WHERE     y.TAARICH BETWEEN p_tar_me AND p_tar_ad
                   AND y.mispar_ishi=s.mispar_ishi
                AND y.taarich=s.taarich
                AND y.status<>0
               AND not y.MEASHER_O_MISTAYEG is null
                AND o.mispar_ishi = s.mispar_ishi
                AND s.mispar_ishi = mp.mispar_ishi
                AND s.taarich = mp.taarich
                AND s.mispar_sidur = MP.MISPAR_SIDUR
                AND o.mispar_ishi = ppo.mispar_ishi
                AND s.TAARICH BETWEEN PPO.ME_TARICH AND ppo.ad_tarich
                AND S.MISPAR_ISHI = T.MISPAR_ISHI(+)
                AND S.MISPAR_SIDUR = T.MISPAR_SIDUR(+)
                AND s.TAARICH = t.taarich(+)
                AND S.SHAT_HATCHALA = T.SHAT_HATCHALA(+)
                AND S.MISPAR_ISHI = m.MISPAR_ISHI(+)
                AND S.MISPAR_SIDUR = m.MISPAR_SIDUR(+)
                AND s.TAARICH = m.taarich(+)
                AND S.SHAT_HATCHALA = m.SHAT_HATCHALA_SIDUR(+)
                AND S.MISPAR_SIDUR = sm.MISPAR_SIDUR(+)
                AND s.TAARICH BETWEEN sm.me_taarich(+) AND sm.ad_taarich(+)
                AND i.kod_isuk = PPO.ISUK
                AND i.kod_hevra = 4895) a
                order by  a.TAARICH;


      v_rec         p_cur%ROWTYPE;
      output_file   UTL_FILE.FILE_TYPE;
      v_line        VARCHAR (240);
    v_file_name       VARCHAR(30);
     QryMakatDate VARCHAR2(3500);
   BEGIN
    QryMakatDate :=   'Select   distinct p.makat_nesia,p.TAARICH  
                                  FROM TB_PEILUT_OVDIM p
                                   WHERE  p.taarich BETWEEN  ''' || p_tar_me  || ''' AND ''' ||  p_tar_ad  || '''';
       Pkg_Reports.pro_Prepare_Catalog_Details(QryMakatDate);
       
     v_file_name:=   'S38N' || lpad(P_BAKAHA_ID,6,0)  || TO_CHAR (p_tar_me, 'ddmmyyyy') ||  '.csv';
      output_file :=
         UTL_FILE.fopen ('KDS_FILES',
                      v_file_name,
                         'W');
      --DBMS_OUTPUT.put_line('start');
      FOR v_rec IN p_cur (p_tar_me, p_tar_ad)
      LOOP
   --   DBMS_OUTPUT.put_line('start loop');
         v_line:='';
         v_line := v_line || v_rec.mispar_ishi || ';';
         v_line := v_line || v_rec.SHEM_MISH || ';';
         v_line := v_line || v_rec.SHEM_prat || ';';
         v_line := v_line || v_rec.MAAMAD || ';';
         v_line := v_line || v_rec.gil || ';';
         v_line := v_line || v_rec.ezor || ';';
         v_line := v_line || v_rec.snif_av || ';';
         v_line := v_line || v_rec.ISUK || ';';
         v_line := v_line || v_rec.taarich || ';';
         v_line := v_line || v_rec.dayOfWeek || ';';

         v_line := v_line || v_rec.mispar_sidur || ';';
         v_line := v_line || to_char(v_rec.shat_hatchala,'dd/mm/yyyy hh24:mi:ss') || ';';
         v_line := v_line || to_char(v_rec.shat_gmar,'dd/mm/yyyy hh24:mi:ss') || ';';
         
       --  DBMS_OUTPUT.put_line('start dakot');
       if (v_rec.dakot_nochehut=0) then v_line := v_line ||'0000'; else v_line := v_line || v_rec.dakot_nochehut; end if;
          v_line := v_line || ';';
         if (v_rec.dakot_nochehut_headrut=0) then v_line := v_line ||'0000'; else v_line := v_line || v_rec.dakot_nochehut_headrut; end if;
            v_line := v_line || ';';
         v_line := v_line || v_rec.TEUR_SNIF_AV || ';';
         v_line := v_line || v_rec.teur_isuk || ';';
        
          v_line := v_line || v_rec.sum_km || ';';
         v_line := v_line || v_rec.chariga || ';';
         v_line := v_line || v_rec.hashlama || ';';
         v_line := v_line || v_rec.sidur_name || ';';
         v_line := v_line || v_rec.taarich_source || ';';
         v_line := v_line || v_rec.mikum_yechida || ';';
         v_line := v_line || v_rec.snif_tnua || ';';
         v_line := v_line || v_rec.oto_num || ';';
         v_line := v_line || v_rec.LICENSE_NUMBER || ';';
         v_line := v_line || v_rec.snif_mashar || ';';
         if  SUBSTR (v_rec.mispar_sidur, 0, 2)='99' then
           v_line := v_line || v_rec.snif_metugbar || ';';
         else
          v_line := v_line || 0 || ';';
          end if;
         v_line := v_line || v_rec.mispar_visa || ';';
         v_line := v_line || v_rec.sector_visa || ';';
             
         --   DBMS_OUTPUT.put_line(v_line);
         UTL_FILE.put_line (output_file, v_line);
      --    DBMS_OUTPUT.put_line('end loop');
      END LOOP;

      UTL_FILE.fclose (output_file);
      commit;
    ftp_file('KDS_FILES',v_file_name,'filereports/' || v_file_name);
      
   EXCEPTION
      WHEN OTHERS
      THEN
         UTL_FILE.put_line (output_file,
                            'Error: ' || SUBSTR (SQLERRM, 1, 100));

         IF UTL_FILE.is_open (output_file)
         THEN
            UTL_FILE.fclose (output_file);
         END IF;

         RAISE;
   END create_file_egged_taavura;

 FUNCTION fn_get_first_namak_sherut(p_mispar_ishi IN TB_SIDURIM_OVDIM.mispar_ishi%TYPE,
                                                            p_mispar_sidur IN TB_SIDURIM_OVDIM.mispar_sidur%TYPE,
                                                            p_taarich IN TB_SIDURIM_OVDIM.taarich%TYPE,
                                                                p_shat_hatchala IN TB_SIDURIM_OVDIM.shat_hatchala%TYPE) return varchar2 as
v_makat varchar2(8);
BEGIN
select   makat_nesia into v_makat
          from(
   select  p.MISPAR_ISHI, p.taarich,
          p.MISPAR_SIDUR,
           p.shat_hatchala_sidur,p.makat_nesia,
          pkg_tnua.fn_get_makat_type(p.makat_nesia) makat_type
   from tb_peilut_ovdim p
  where  p.taarich = p_taarich
          AND p.mispar_ishi=p_mispar_ishi
          and p.mispar_sidur=p_mispar_sidur
            AND p.shat_hatchala_sidur = p_shat_hatchala
   order by SHAT_YETZIA,MISPAR_KNISA )
   where (makat_type=1 or makat_type=3)
 and  rownum=1;
   
   return v_makat;
   
     EXCEPTION
              WHEN NO_DATA_FOUND THEN
                   return 0;
                   
END fn_get_first_namak_sherut; 
   
 PROCEDURE create_file_meshek(p_tar_me IN DATE, p_tar_ad IN DATE,P_BAKAHA_ID IN NUMBER,
                                                P_EZOR IN VARCHAR2 DEFAULT NULL, 
                                                P_MIKUM_YECHIDA IN VARCHAR2 DEFAULT NULL,
                                                P_PREFIX_FILE_NAME IN VARCHAR2)
   IS
      CURSOR p_cur (
         p_tar_me    tb_sidurim_ovdim.TAARICH%TYPE,
         p_tar_ad    tb_sidurim_ovdim.TAARICH%TYPE,
          P_EZOR IN VARCHAR2, 
          P_MIKUM_YECHIDA IN VARCHAR2)
      IS
               select o.mispar_ishi,substr(O.SHEM_MISH,0,8) SHEM_MISH,
                substr(O.SHEM_PRAT,0,6) SHEM_PRAT,substr(m.teur_mikum_yechida,0,20) teur_mikum_yechida,sm.avodat_meshek,ppo.snif_av,
                ppo.isuk, substr(i.teur_isuk,0,20) teur_isuk,to_char(s.taarich,'yyyymmdd') taarich,
        s.mispar_sidur,s.hashlama,s.chariga,to_char(s.shat_gmar,'hh24mi') shat_gmar,to_char(s.shat_hatchala,'hh24mi') shat_hatchala,GET_SUG_YOM(s.taarich) sug_yom,
        pkg_ovdim.fun_get_meafyen_oved(s.mispar_ishi,3,s.taarich) shat_hatchala_muteret, substr(SA.TEUR_SNIF_AV,0,20) TEUR_SNIF_AV,ppo.MAAMAD,
        pkg_ovdim.fun_get_meafyen_oved(s.mispar_ishi,4,s.taarich) shat_gmar_muteret,substr(cm.teur_sidur_meychad,0,20) teur_sidur_meychad
        from
        tb_yamey_avoda_ovdim  y,
        tb_sidurim_ovdim  s,
        Ctb_snif_av sa,
         (select *
           from pivot_pirtey_ovdim
           where me_tarich between  p_tar_me and p_tar_ad) ppo,
        ovdim o,
        ctb_mikum_yechida  m,
        pivot_sidurim_meyuchadim  sm    ,
        ctb_isuk i,ctb_sidurim_meyuchadim cm
        where y.status<>0
        and not y.measher_o_mistayeg is null
        and s.mispar_ishi=y.mispar_ishi
        and s.taarich=y.taarich
        and s.taarich between p_tar_me and p_tar_ad
        and o.mispar_ishi=s.mispar_ishi
         and s.mispar_ishi = ppo.mispar_ishi
        and s.taarich between ppo.me_tarich and ppo.ad_tarich
        and ppo.mikum_yechida=m.kod_mikum_yechida
        and s.mispar_sidur=sm.mispar_sidur
        and s.taarich between sm.me_tarich and sm.ad_tarich
        and ppo.isuk=i.kod_isuk
        and o.kod_hevra=i.kod_hevra
        and not sm.avodat_meshek is null
        and cm.kod_sidur_meyuchad=s.mispar_sidur
          and sa.kod_snif_av = ppo.snif_av
        and sa.kod_hevra =o.kod_hevra
        and substr(ppo.isuk,0,1) in (6,7,8)
        and (p_ezor is null or ppo.EZOR  IN (SELECT x FROM TABLE(CAST(Convert_String_To_Table(p_ezor ,  ',') AS mytabtype))))
        and (p_mikum_yechida is null or ppo.MIKUM_YECHIDA  IN (SELECT x FROM TABLE(CAST(Convert_String_To_Table(p_mikum_yechida ,  ',') AS mytabtype))));

      v_rec         p_cur%ROWTYPE;
      output_file   UTL_FILE.FILE_TYPE;
      v_line        VARCHAR (240);
      v_file_name       VARCHAR(30);
   BEGIN
     v_file_name:=  P_PREFIX_FILE_NAME || lpad(P_BAKAHA_ID,6,0)  || TO_CHAR (p_tar_me, 'ddmmyy') ||  '.csv';
      output_file :=
         UTL_FILE.fopen ('KDS_FILES',
                      v_file_name,
                         'W');
      --DBMS_OUTPUT.put_line('start');
      FOR v_rec IN p_cur (p_tar_me, p_tar_ad,p_ezor,p_mikum_yechida)
      LOOP
   --   DBMS_OUTPUT.put_line('start loop');
         v_line:='';
         v_line := v_line || v_rec.mispar_ishi || ';';
         v_line := v_line || v_rec.shem_mish || ';';
             v_line := v_line || v_rec.shem_prat || ';';
            v_line := v_line || v_rec.maamad || ';';
         v_line := v_line || v_rec.teur_mikum_yechida || ';';
          v_line := v_line || v_rec.teur_snif_av || ';';
         v_line := v_line || v_rec.isuk || ';';
             v_line := v_line || v_rec.teur_isuk || ';';
         v_line := v_line || v_rec.taarich || ';';
         v_line := v_line || v_rec.sug_yom || ';';
          v_line := v_line || v_rec.mispar_sidur || ';';
         v_line := v_line || v_rec.teur_sidur_meychad || ';';
             v_line := v_line || v_rec.shat_hatchala || ';';
         v_line := v_line || v_rec.shat_gmar || ';';
            v_line := v_line || v_rec.chariga || ';';
         v_line := v_line || v_rec.hashlama || ';';
          v_line := v_line || v_rec.shat_hatchala_muteret || ';';
         v_line := v_line || v_rec.shat_gmar_muteret || ';';
         --   DBMS_OUTPUT.put_line(v_line);
         UTL_FILE.put_line (output_file, v_line);
      --    DBMS_OUTPUT.put_line('end loop');
      END LOOP;

      UTL_FILE.fclose (output_file);
      commit;
    ftp_file('KDS_FILES',v_file_name,'filereports/' || v_file_name);
      
   EXCEPTION
      WHEN OTHERS
      THEN
         UTL_FILE.put_line (output_file,
                            'Error: ' || SUBSTR (SQLERRM, 1, 100));

         IF UTL_FILE.is_open (output_file)
         THEN
            UTL_FILE.fclose (output_file);
         END IF;

         RAISE;
   END create_file_meshek;

PROCEDURE create_file_et_namak(p_tar_me IN DATE, p_tar_ad IN DATE,P_BAKAHA_ID IN NUMBER )
   IS
      CURSOR p_cur (
         p_tar_me    tb_peilut_ovdim.TAARICH%TYPE,
         p_tar_ad    tb_peilut_ovdim.TAARICH%TYPE)
      IS
         select * from
            (select p.mispar_ishi,to_char(p.taarich,'yyyymmdd') taarich,to_char(p.taarich,'D') yom,p.oto_no,p.mispar_sidur,
            p.taarich taarich_source,p.shat_hatchala_sidur,P.MAKAT_NESIA,
            pkg_tnua.fn_get_makat_type(P.MAKAT_NESIA) makat_type,to_char(P.SHAT_YETZIA,'hh24mi') SHAT_YETZIA,
            p.snif_tnua,v.license_number,C.DESCRIPTION,C.KM,C.SNIF,C.MAZAN_TASHLUM
            from
            tb_yamey_avoda_ovdim  y,
            tb_peilut_ovdim p,
            vehicle_specifications v,
            Ctb_snif_av sa,
                tmp_catalog c ,
             (select *
               from PIVOT_PIRTEY_OVDIM
               where me_tarich between  p_tar_me and p_tar_ad) ppo
            where y.status<>0
            and not y.measher_o_mistayeg is null
            and p.mispar_ishi=y.mispar_ishi
            and p.taarich=y.taarich
            and p.taarich between p_tar_me and p_tar_ad
             and p.mispar_ishi = ppo.mispar_ishi
            and p.taarich between ppo.me_tarich and ppo.ad_tarich
            and p.oto_no = v.bus_number
            and sa.kod_snif_av = ppo.snif_av
            and sa.kod_hevra =4895
            and  p.MAKAT_NESIA  = C.MAKAT8(+)   
            and p.TAARICH = C.ACTIVITY_DATE(+) )
           where makat_type in (3,6)
            order by  taarich_source;
     
      v_rec         p_cur%ROWTYPE;
      output_file   UTL_FILE.FILE_TYPE;
      v_line        VARCHAR (240);
    QryMakatDate VARCHAR2(3500);
      v_file_name       VARCHAR(20);
   BEGIN
    QryMakatDate :=   'Select   distinct p.makat_nesia,p.TAARICH  
                                  FROM TB_PEILUT_OVDIM p
                                   WHERE  p.taarich BETWEEN  ''' || p_tar_me  || ''' AND ''' ||  p_tar_ad  || '''';
       Pkg_Reports.pro_Prepare_Catalog_Details(QryMakatDate);
       
     v_file_name:=  'TNMK' || P_BAKAHA_ID || TO_CHAR (p_tar_me, 'ddmmyyyy') || '.csv';
      output_file :=
         UTL_FILE.fopen ('KDS_FILES',
                      v_file_name,
                         'W');
      --DBMS_OUTPUT.put_line('start');
      FOR v_rec IN p_cur (p_tar_me, p_tar_ad)
      LOOP
   --   DBMS_OUTPUT.put_line('start loop');
         v_line:='';
          v_line := v_line || v_rec.taarich || ';';
         v_line := v_line || v_rec.yom || ';';
          v_line := v_line || v_rec.shat_yetzia || ';';
          v_line := v_line || v_rec.makat_nesia || ';';
                                                           
          v_line := v_line || substr(RPAD(v_rec.description,50,' '),0,32) || ';';
          v_line := v_line || v_rec.mazan_tashlum || ';';
          v_line := v_line || v_rec.km || ';';
          v_line := v_line || v_rec.oto_no || ';';         
          v_line := v_line || v_rec.LICENSE_NUMBER || ';';   
          v_line := v_line || v_rec.snif || ';';    
          v_line := v_line || v_rec.snif_tnua || ';';                                   
         --   DBMS_OUTPUT.put_line(v_line);
         UTL_FILE.put_line (output_file, v_line);
      --    DBMS_OUTPUT.put_line('end loop');
      END LOOP;

      UTL_FILE.fclose (output_file);
      commit;
    ftp_file('KDS_FILES',v_file_name,'filereports/' || v_file_name);
      
   EXCEPTION
      WHEN OTHERS
      THEN
         UTL_FILE.put_line (output_file,
                            'Error: ' || SUBSTR (SQLERRM, 1, 100));

         IF UTL_FILE.is_open (output_file)
         THEN
            UTL_FILE.fclose (output_file);
         END IF;

         RAISE;
   END create_file_et_namak;
  
PROCEDURE create_file_mushaley_egged(p_tar_me IN DATE, p_tar_ad IN DATE,P_BAKAHA_ID IN NUMBER )
   IS
      CURSOR p_cur (
         p_tar_me    tb_peilut_ovdim.TAARICH%TYPE,
         p_tar_ad    tb_peilut_ovdim.TAARICH%TYPE)
      IS
             select snif_mashar,LICENSE_NUMBER,sum(count_travels) count_travels,sum(sum_km_sherut) sum_km_sherut,
             sum(sum_km_namak) sum_km_namak,sum(sum_km_empty) sum_km_empty
  from
  ( select snif_mashar,LICENSE_NUMBER,makat_type,  case makat_type when 1 then count(*) else null end count_travels,
  case makat_type when 1 then sum(km) else null end sum_km_sherut,
  case makat_type when 3 then sum(km) when  6 then sum(km)  else null end sum_km_namak,
  case makat_type when 2   then sum(km) when 5 then sum(km)  else null end sum_km_empty from
            (select  p.TAARICH ,p.MAKAT_NESIA,pkg_tnua.fn_get_makat_type(P.MAKAT_NESIA) makat_type, substr(v.branch2,2,2) snif_mashar ,ME.HOVA_MISPAR_RECHEV,
            V.LICENSE_NUMBER,C.KM
            from
            tb_yamey_avoda_ovdim  y,
            tb_peilut_ovdim p,
           pivot_MEAFYENEY_ELEMENTIM me,
            vehicle_specifications v,
              tmp_catalog c 
          where y.status<>0
            and not y.measher_o_mistayeg is null
            and p.mispar_ishi=y.mispar_ishi
            and p.taarich=y.taarich
            and p.taarich between  p_tar_me and  p_tar_ad
            and v.own_firm_code=24
            and p.oto_no = v.bus_number
            and ME.KOD_ELEMENT(+)=to_number(substr(P.MAKAT_NESIA,2,2))
            and P.TAARICH  between me.me_tarich(+) and ME.AD_TARICH(+)
            and  p.MAKAT_NESIA  = C.MAKAT8(+)   
            and p.TAARICH = C.ACTIVITY_DATE(+) 
          )
            where (makat_type in (1,2,3,6)  or (makat_type=5 and not HOVA_MISPAR_RECHEV is null))
          group by snif_mashar,LICENSE_NUMBER,makat_type)
          group by snif_mashar,LICENSE_NUMBER;
     
      v_rec         p_cur%ROWTYPE;
      output_file   UTL_FILE.FILE_TYPE;
      v_line        VARCHAR (240);
        v_file_name       VARCHAR(30);
   QryMakatDate VARCHAR2(3500);
   BEGIN
      QryMakatDate :=   'Select   distinct p.makat_nesia,p.TAARICH  
                                  FROM TB_PEILUT_OVDIM p
                                   WHERE  p.taarich BETWEEN  ''' || p_tar_me  || ''' AND ''' ||  p_tar_ad  || '''';
       Pkg_Reports.pro_Prepare_Catalog_Details(QryMakatDate);
     
     v_file_name:=  'BUS' || lpad(P_BAKAHA_ID,6,0)  || TO_CHAR (p_tar_me, 'ddmmyyyy') || '.csv';
      output_file :=
         UTL_FILE.fopen ('KDS_FILES',
                      v_file_name,
                         'W');
      --DBMS_OUTPUT.put_line('start');
      FOR v_rec IN p_cur (p_tar_me, p_tar_ad)
      LOOP
   --   DBMS_OUTPUT.put_line('start loop');
         v_line:='';
          v_line := v_line || v_rec.snif_mashar || ';';
         v_line := v_line || v_rec.license_number || ';';
         v_line := v_line || v_rec.sum_km_sherut || ';';
         v_line := v_line || v_rec.sum_km_empty || ';';
         v_line := v_line || v_rec.sum_km_namak || ';';
          v_line := v_line || v_rec.count_travels || ';';
                                    
         --   DBMS_OUTPUT.put_line(v_line);
         UTL_FILE.put_line (output_file, v_line);
      --    DBMS_OUTPUT.put_line('end loop');
      END LOOP;

      UTL_FILE.fclose (output_file);
      commit;
    ftp_file('KDS_FILES',v_file_name,'filereports/' || v_file_name);
      
   EXCEPTION
      WHEN OTHERS
      THEN
         UTL_FILE.put_line (output_file,
                            'Error: ' || SUBSTR (SQLERRM, 1, 100));

         IF UTL_FILE.is_open (output_file)
         THEN
            UTL_FILE.fclose (output_file);
         END IF;

         RAISE;
   END create_file_mushaley_egged;
   
   
PROCEDURE create_file_et_sherut(p_tar_me IN DATE, p_tar_ad IN DATE,P_BAKAHA_ID IN NUMBER )
   IS
      CURSOR p_cur (
         p_tar_me    tb_peilut_ovdim.TAARICH%TYPE,
         p_tar_ad    tb_peilut_ovdim.TAARICH%TYPE)
      IS
        select * from
            (select p.mispar_ishi,to_char(p.taarich,'yyyymmdd') taarich,
            p.taarich taarich_source,P.MAKAT_NESIA,
            pkg_tnua.fn_get_makat_type(P.MAKAT_NESIA) makat_type,to_char(P.SHAT_YETZIA,'hh24mi') SHAT_YETZIA,v.license_number
            from
            tb_yamey_avoda_ovdim  y,
            tb_peilut_ovdim p,
            vehicle_specifications v,
            tmp_catalog c 
            where y.status<>0
            and not y.measher_o_mistayeg is null
            and p.mispar_ishi=y.mispar_ishi
            and p.taarich=y.taarich
            and p.taarich between p_tar_me and p_tar_ad
            and p.mispar_knisa=0
            and p.oto_no = v.bus_number
          and  p.MAKAT_NESIA  = C.MAKAT8(+)   
            and p.TAARICH = C.ACTIVITY_DATE(+) )
           where makat_type=1
            order by  taarich_source;
     
      v_rec         p_cur%ROWTYPE;
      output_file   UTL_FILE.FILE_TYPE;
      v_line        VARCHAR (240);
    QryMakatDate VARCHAR2(3500);
      v_file_name       VARCHAR(20);
   BEGIN
    QryMakatDate :=   'Select   distinct p.makat_nesia,p.TAARICH  
                                  FROM TB_PEILUT_OVDIM p
                                   WHERE  p.taarich BETWEEN  ''' || p_tar_me  || ''' AND ''' ||  p_tar_ad  || '''';
       Pkg_Reports.pro_Prepare_Catalog_Details(QryMakatDate);
       
     v_file_name:=  'TA' || P_BAKAHA_ID || TO_CHAR (p_tar_me, 'ddmmyyyy') || '.csv';
      output_file :=
         UTL_FILE.fopen ('KDS_FILES',
                      v_file_name,
                         'W');
      --DBMS_OUTPUT.put_line('start');
      FOR v_rec IN p_cur (p_tar_me, p_tar_ad)
      LOOP
   --   DBMS_OUTPUT.put_line('start loop');
         v_line:='';
           v_line := v_line || v_rec.mispar_ishi || ';';
          v_line := v_line || v_rec.taarich || ';';
          v_line := v_line || v_rec.shat_yetzia || ';';
          v_line := v_line || v_rec.makat_nesia || ';';
           v_line := v_line || v_rec.LICENSE_NUMBER || ';';   
                                
         --   DBMS_OUTPUT.put_line(v_line);
         UTL_FILE.put_line (output_file, v_line);
      --    DBMS_OUTPUT.put_line('end loop');
      END LOOP;

      UTL_FILE.fclose (output_file);
      commit;
    ftp_file('KDS_FILES',v_file_name,'filereports/' || v_file_name);
      
   EXCEPTION
      WHEN OTHERS
      THEN
         UTL_FILE.put_line (output_file,
                            'Error: ' || SUBSTR (SQLERRM, 1, 100));

         IF UTL_FILE.is_open (output_file)
         THEN
            UTL_FILE.fclose (output_file);
         END IF;

         RAISE;
   END create_file_et_sherut;
   
PROCEDURE ftp_file(p_from_dir varchar2,p_from_file varchar2,p_to_file_name varchar2)
   IS
    l_conn  UTL_TCP.connection;
BEGIN
  l_conn := ftp.login('kdstst02.egged.intra', '21', lower('runbatch'), lower('runbatch'),60);
 ftp.binary(p_conn => l_conn);
ftp.put(p_conn      => l_conn,
          p_from_dir  => p_from_dir,
          p_from_file => p_from_file,
          p_to_file   => p_to_file_name);
  ftp.logout(l_conn);
END ftp_file;

END PKG_FILES;
/


CREATE OR REPLACE PACKAGE BODY          PKG_RIKUZ_AVODA AS
/******************************************************************************
   NAME:       PKG_RIKUZ_AVODA
   PURPOSE:

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        24/05/2012      meravn       1. Created this package body.
******************************************************************************/

PROCEDURE pro_get_rechivim_lerikuz(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                                 p_taarich IN DATE,
                                                         p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                                      p_cur OUT CurType) IS
    tar_me date;       
    list_rechivim nvarchar2(100);        
  --p_taarich  DATE;                                                             
 BEGIN  
-- p_taarich:=to_date('31/08/2010','dd/mm/yyyy');
   tar_me:= to_date('01/' ||  to_char(p_taarich,'mm/yyyy'),'dd/mm/yyyy');
   list_rechivim:= '1,22,26,28,29,30,47,48,49,50,53,55,66,67,76,77,78,91,92,125,126,131,202,203,220,221';    
 open  p_cur for 
 
 
  select rechivim.*, hearot.heara
 from
 (                                                                                    
        select   TO_NUMBER(TO_CHAR(p.taarich ,'DD')) day_num, DayOfWeek(p.taarich) day_ot , 
          p.mispar_ishi, p.taarich,
          max(case  to_number(kod_rechiv)  when 1 then erech_rechiv end ) r1,
          max(case  to_number(kod_rechiv)  when 22 then erech_rechiv end )r22,
          max(case  to_number(kod_rechiv)  when 26 then erech_rechiv end )r26,
          max(case  to_number(kod_rechiv)  when 28 then erech_rechiv end )r28,
          max(case  to_number(kod_rechiv)  when 29 then erech_rechiv end )r29,
          max(case  to_number(kod_rechiv)  when 30 then erech_rechiv end )r30,
          max(case  to_number(kod_rechiv)  when 47 then erech_rechiv end )r47,
          max(case  to_number(kod_rechiv)  when 48 then erech_rechiv end )r48,
          max(case  to_number(kod_rechiv)  when 49 then erech_rechiv end )r49,
          max(case  to_number(kod_rechiv)  when 50 then erech_rechiv end )r50,
          max(case  to_number(kod_rechiv)  when 53 then erech_rechiv end )r53,
          max(case  to_number(kod_rechiv)  when 55 then erech_rechiv end )r55,
          max(case  to_number(kod_rechiv)  when 66 then erech_rechiv end )r66,
          max(case  to_number(kod_rechiv)  when 67 then erech_rechiv end )r67,
          max(case  to_number(kod_rechiv)  when 76 then erech_rechiv end )r76,
          max(case  to_number(kod_rechiv)  when 77 then erech_rechiv end )r77,
          max(case  to_number(kod_rechiv)  when 78 then erech_rechiv end )r78,
          max(case  to_number(kod_rechiv)  when 91 then erech_rechiv end )r91,
          max(case  to_number(kod_rechiv)  when 92 then erech_rechiv end )r92,
          max(case to_number(kod_rechiv) when 125 then erech_rechiv end ) r125 ,
          max(case to_number(kod_rechiv) when 126 then erech_rechiv end ) r126 ,
          max(case  to_number(kod_rechiv)  when 131 then erech_rechiv end )r131,
          max(case  to_number(kod_rechiv)  when 202 then erech_rechiv end )r202,
          max(case  to_number(kod_rechiv)  when 203 then erech_rechiv end )r203,
          max(case  to_number(kod_rechiv)  when 220 then erech_rechiv end )r220,
          max(case  to_number(kod_rechiv)  when 221 then erech_rechiv end )r221       
        from (
        SELECT h.mispar_ishi,  h.Kod_Rechiv, h.TAARICH ,y.Erech_Rechiv erech_rechiv
         FROM 
        ( select c.mispar_ishi,c.taarich,c.kod_rechiv,c.erech_rechiv
         from  TB_CHISHUV_YOMI_OVDIM C
         where c.Mispar_Ishi=p_mispar_ishi 
              and C.Bakasha_ID=p_bakasha_id
              AND c.taarich between tar_me and last_day(tar_me) 
              and  c.Kod_Rechiv in ( SELECT X FROM TABLE(CAST(Convert_String_To_Table( list_rechivim,  ',') AS MYTABTYPE)))   ) y ,
        (select D.mispar_ishi,D.taarich,R.kod_rechiv
        from
         (   select p_mispar_ishi mispar_ishi ,TO_DATE(x,'dd/mm/yyyy') taarich 
             from ( SELECT X from    TABLE(CAST(Convert_String_To_Table(String_Dates_Of_Period(to_char(p_taarich,'mm/yyyy')),',') AS mytabtype)))   ) D
        full join     
            (select p_mispar_ishi mispar_ishi ,x kod_rechiv
             from ( SELECT X FROM TABLE(CAST(Convert_String_To_Table(list_rechivim,  ',') AS MYTABTYPE))) )R
        on D.mispar_ishi = R.mispar_ishi   ) h
        where  h.MISPAR_ISHI = y.MISPAR_ISHI(+)
                and h.TAARICH= y.TAARICH(+)
                and h.KOD_RECHIV= y.Kod_Rechiv(+)  ) p
        GROUP BY p.mispar_ishi, p.taarich
        order  BY p.mispar_ishi, p.taarich ) rechivim,
       (select mushee.taarich,decode(mushee.KOD_MATZAV,null,hearot_tb.heara,'מושעה') heara
        from (select y.taarich,max(h.heara) heara
                 from (SELECT   Mispar_Ishi,Taarich,max(SYS_CONNECT_BY_PATH (kod_rechiv,'.'))KOD_RECHIV
                          FROM ( SELECT C. Mispar_Ishi,  C.Kod_Rechiv, c.Taarich,ROW_NUMBER () OVER (PARTITION BY c.Taarich ORDER BY c.kod_rechiv ASC) RN
                                      FROM TB_CHISHUV_YOMI_OVDIM C,CTB_RECHIVIM r
                                      WHERE  C.Bakasha_ID= p_bakasha_id --7106 
                                              AND C. Mispar_Ishi= p_mispar_ishi --19485  
                                              AND c.erech_rechiv>0
                                              AND r.yesh_heara=1
                                              AND r.kod_rechiv=c.kod_rechiv
                                              AND c.taarich between  tar_me and last_day(tar_me)  ) --AND TO_CHAR(c.Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy') )
                             CONNECT BY Taarich = PRIOR Taarich AND RN = PRIOR RN + 1
                             START WITH RN = 1
                             group by Mispar_Ishi,Taarich
                            ORDER BY Taarich) y,
                            CTB_HEAROT_RECHIVIM H,
                            (select  o.taarich,p.erech  
                             from (select p_mispar_ishi   mispar_ishi ,TO_DATE(x,'dd/mm/yyyy') taarich 
                                      from ( SELECT X from    TABLE(CAST(Convert_String_To_Table(String_Dates_Of_Period(to_char( p_taarich ,'mm/yyyy')),',') AS mytabtype)))  )o,
                                      PIRTEY_OVDIM p  
                             where o.mispar_ISHI=  p.mispar_ISHI(+)
                               and  o.taarich between   p.Me_taarich(+) and p.Ad_taarich(+)  
                                and P.KOD_natun(+) =9 ) m
                 WHERE   H.KOD_RECHIV=SUBSTR(Y.KOD_RECHIV,2,LENGTH(Y.KOD_RECHIV)-1)   
                      and ( (H.MUTAM_BITACHON=1 and m.erech is not null) or  m.erech is null) 
                      and  y.taarich =  m.taarich
                 GROUP BY y.TAARICH          ) hearot_tb,    
                (select  o.taarich , z.KOD_MATZAV 
                 from (select p_mispar_ishi   mispar_ishi ,TO_DATE(x,'dd/mm/yyyy') taarich 
                          from ( SELECT X from    TABLE(CAST(Convert_String_To_Table(String_Dates_Of_Period(to_char( p_taarich ,'mm/yyyy')),',') AS mytabtype)))  )o,
                          matzav_ovdim z
                  where  o.mispar_ISHI=  z.mispar_ISHI(+)
                      and  o.taarich between  z.TAARICH_HATCHALA(+) and z.TAARICH_SIYUM(+)  
                       and z.KOD_MATZAV(+) ='33' ) mushee
            where   mushee.taarich = hearot_tb.taarich(+) ) hearot
 where rechivim.taarich = hearot.taarich(+);

end pro_get_rechivim_lerikuz;

PROCEDURE pro_rechivim_chodshiim_lerikuz(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                                              p_taarich IN DATE,
                                                                  p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                                                 p_cur OUT CurType) IS
    tar_me date;       
    list_rechivim nvarchar2(200);        
  -- p_taarich  DATE;                                                             
 BEGIN  
 --p_taarich:=to_date('31/08/2010','dd/mm/yyyy');
   tar_me:= to_date('01/' ||  to_char(p_taarich,'mm/yyyy'),'dd/mm/yyyy');
   list_rechivim:= '1,5,22,26,28,29,30,44,47,48,49,50,53,55,66,67,76,77,78,91,92,100,101,102,103,108,119,120,121,122,125,126,131,146,202,203,219,220,221';    
   open  p_cur for                  
    select   
          max(case to_number(kod_rechiv) when 1 then  erech_rechiv end ) r1 ,
          max(case to_number(kod_rechiv) when 5 then  erech_rechiv end ) r5 ,
       --   max(case to_number(kod_rechiv) when 10 then  erech_rechiv end ) r10 ,
        -- max(case to_number(kod_rechiv) when 11 then  erech_rechiv end ) r11 ,
          max(case to_number(kod_rechiv) when 22 then  erech_rechiv end ) r22 ,
          max(case to_number(kod_rechiv) when 44 then  erech_rechiv end ) r44,
          max(case to_number(kod_rechiv) when 26 then  erech_rechiv end ) r26 ,
          max(case to_number(kod_rechiv) when 28 then  erech_rechiv end ) r28 ,
          max(case to_number(kod_rechiv) when 29 then  erech_rechiv end ) r29 ,
          max(case to_number(kod_rechiv) when 30 then erech_rechiv end ) r30 ,
     --     max(case to_number(kod_rechiv) when 39 then  erech_rechiv end ) r39 ,
     --     max(case to_number(kod_rechiv) when 41 then  erech_rechiv end ) r41,
      --    max(case to_number(kod_rechiv) when 43 then  erech_rechiv end ) r43,
          max(case to_number(kod_rechiv) when 47 then  erech_rechiv end ) r47,
          max(case to_number(kod_rechiv) when 48 then  erech_rechiv end ) r48,
          max(case to_number(kod_rechiv) when 49 then  erech_rechiv end ) r49,
          max(case to_number(kod_rechiv) when 50 then  erech_rechiv end ) r50,
          max(case to_number(kod_rechiv) when 53 then  erech_rechiv end ) r53,
          max(case to_number(kod_rechiv) when 55 then  erech_rechiv end ) r55,
          max(case to_number(kod_rechiv) when 66 then  erech_rechiv end ) r66,
          max(case to_number(kod_rechiv) when 67 then  erech_rechiv end ) r67,
          max(case to_number(kod_rechiv) when 76 then  erech_rechiv end ) r76,
          max(case to_number(kod_rechiv) when 77 then  erech_rechiv end ) r77 ,
          max(case to_number(kod_rechiv) when 78 then erech_rechiv end ) r78 ,
          max(case to_number(kod_rechiv) when 91 then  erech_rechiv end ) r91 ,
          max(case to_number(kod_rechiv) when 92 then  erech_rechiv end ) r92 ,
   --      max(case to_number(kod_rechiv) when 95 then  erech_rechiv end ) r95 ,
          max(case to_number(kod_rechiv) when 100 then  erech_rechiv end ) r100,
          max(case to_number(kod_rechiv) when 101 then  erech_rechiv end ) r101 ,
          max(case to_number(kod_rechiv) when 102 then  erech_rechiv end ) r102 ,
          max(case to_number(kod_rechiv) when 103 then  erech_rechiv end ) r103 ,
          max(case to_number(kod_rechiv) when 108 then  erech_rechiv end ) r108 ,
          max(case to_number(kod_rechiv) when 119 then  erech_rechiv end ) r119 ,
          max(case to_number(kod_rechiv) when 120 then  erech_rechiv end ) r120 ,
          max(case to_number(kod_rechiv) when 121 then  erech_rechiv end ) r121 ,
          max(case to_number(kod_rechiv) when 122 then  erech_rechiv end ) r122 ,
          max(case to_number(kod_rechiv) when 125 then  erech_rechiv end ) r125 ,
          max(case to_number(kod_rechiv) when 126 then  erech_rechiv end ) r126,
          max(case to_number(kod_rechiv) when 131 then  erech_rechiv end ) r131 ,
          max(case to_number(kod_rechiv) when 146 then  erech_rechiv end ) r146 ,
          max(case to_number(kod_rechiv) when 202 then  erech_rechiv end ) r202 ,
          max(case to_number(kod_rechiv) when 203 then  erech_rechiv end ) r203,
          max(case to_number(kod_rechiv) when 219 then  erech_rechiv end ) r219 ,     
          max(case to_number(kod_rechiv) when 220 then  erech_rechiv end ) r220 ,
          max(case to_number(kod_rechiv) when 221 then  erech_rechiv end ) r221 
          
   from(     
   select  C.KOD_RECHIV,C.ERECH_RECHIV
   from TB_CHISHUV_CHODESH_OVDIM C
   where c.Mispar_Ishi=p_mispar_ishi 
              and C.Bakasha_ID=p_bakasha_id
              AND c.taarich between tar_me and last_day(tar_me) 
              and  c.Kod_Rechiv in ( SELECT X FROM TABLE(CAST(Convert_String_To_Table( list_rechivim,  ',') AS MYTABTYPE))) ) p ;
              
end pro_rechivim_chodshiim_lerikuz;


PROCEDURE pro_rechivey_headrut_lerikuz(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                                  p_taarich IN DATE,
                                                              p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                                              p_cur OUT CurType) IS
    tar_me date;       
    list_rechivim nvarchar2(100);        
 --  p_taarich  DATE;                                                             
 BEGIN  
 --p_taarich:=to_date('31/08/2010','dd/mm/yyyy');
   tar_me:= to_date('01/' ||  to_char(p_taarich,'mm/yyyy'),'dd/mm/yyyy');
   list_rechivim:= '53,56,57,60,61,62,64,65,66,67,68,69,70,71,72,266';    
   
   open  p_cur for                  
    select   
          max(case to_number(kod_rechiv) when 53 then  erech_rechiv end ) r53 ,
          max(case to_number(kod_rechiv) when 56 then  erech_rechiv end ) r56 ,
          max(case to_number(kod_rechiv) when 57 then  erech_rechiv end ) r57 ,
          max(case to_number(kod_rechiv) when 60 then  erech_rechiv end ) r60 ,
          max(case to_number(kod_rechiv) when 61 then  erech_rechiv end ) r61 ,
          max(case to_number(kod_rechiv) when 62 then  erech_rechiv end ) r62,
     --     max(case to_number(kod_rechiv) when 63 then  erech_rechiv end ) r63,
          max(case to_number(kod_rechiv) when 64 then  erech_rechiv end ) r64,
          max(case to_number(kod_rechiv) when 65 then  erech_rechiv end ) r65,
          max(case to_number(kod_rechiv) when 66 then  erech_rechiv end ) r66,
          max(case to_number(kod_rechiv) when 67 then  erech_rechiv end ) r67,
          max(case to_number(kod_rechiv) when 68 then  erech_rechiv end ) r68,
          max(case to_number(kod_rechiv) when 69 then  erech_rechiv end ) r69 ,
          max(case to_number(kod_rechiv) when 70 then  erech_rechiv end ) r70,
          max(case to_number(kod_rechiv) when 71 then  erech_rechiv end ) r71 ,
          max(case to_number(kod_rechiv) when 72 then  erech_rechiv end ) r72,
          max(case to_number(kod_rechiv) when 266 then  erech_rechiv end ) r266
   from(     
   select  C.KOD_RECHIV,C.ERECH_RECHIV
   from TB_CHISHUV_CHODESH_OVDIM C
   where c.Mispar_Ishi=p_mispar_ishi 
              and C.Bakasha_ID=p_bakasha_id
              AND c.taarich between tar_me and last_day(tar_me) 
              and  c.Kod_Rechiv in ( SELECT X FROM TABLE(CAST(Convert_String_To_Table( list_rechivim,  ',') AS MYTABTYPE))) ) p ;
              
end pro_rechivey_headrut_lerikuz;


PROCEDURE pro_rechivey_shonot_lerikuz(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                                   p_taarich IN DATE,
                                                              p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                                              p_cur OUT CurType) IS
    tar_me date;       
    list_rechivim nvarchar2(100);        
   --p_taarich  DATE;                                                             
 BEGIN  
 --p_taarich:=to_date('31/08/2010','dd/mm/yyyy');
   tar_me:= to_date('01/' ||  to_char(p_taarich,'mm/yyyy'),'dd/mm/yyyy');
   list_rechivim:= '1,10,11,12,39,41,43,75,95,112,113,114,115,116,117,118,126,204,205';    
   
   open  p_cur for                  
    select   
          max(case to_number(kod_rechiv) when 1 then  erech_rechiv end ) r1,
          max(case to_number(kod_rechiv) when 10 then  erech_rechiv end ) r10 ,
          max(case to_number(kod_rechiv) when 11 then  erech_rechiv end ) r11 ,
          max(case to_number(kod_rechiv) when 12 then  erech_rechiv end ) r12 ,
          max(case to_number(kod_rechiv) when 39 then  erech_rechiv end ) r39,
          max(case to_number(kod_rechiv) when 41 then  erech_rechiv end ) r41,
          max(case to_number(kod_rechiv) when 43 then  erech_rechiv end ) r43,
          max(case to_number(kod_rechiv) when 75 then  erech_rechiv end ) r75,
          max(case to_number(kod_rechiv) when 95 then  erech_rechiv end ) r95,
          max(case to_number(kod_rechiv) when 112 then  erech_rechiv end ) r112,
          max(case to_number(kod_rechiv) when 113 then  erech_rechiv end ) r113,
          max(case to_number(kod_rechiv) when 114 then  erech_rechiv end ) r114,
          max(case to_number(kod_rechiv) when 115 then  erech_rechiv end ) r115,
          max(case to_number(kod_rechiv) when 116 then  erech_rechiv end ) r116,
          max(case to_number(kod_rechiv) when 117 then  erech_rechiv end ) r117,
          max(case to_number(kod_rechiv) when 118 then  erech_rechiv end ) r118,
          max(case to_number(kod_rechiv) when 126 then  erech_rechiv end ) r126,
          max(case to_number(kod_rechiv) when 204 then  erech_rechiv end ) r204,
          max(case to_number(kod_rechiv) when 205 then  erech_rechiv end ) r205,
          pkg_rikuz_avoda.getNochechutChodshit(p_mispar_ishi , tar_me, p_bakasha_id) r1b
   from(     
   select  C.KOD_RECHIV,C.ERECH_RECHIV
   from TB_CHISHUV_CHODESH_OVDIM C
   where c.Mispar_Ishi=p_mispar_ishi 
              and C.Bakasha_ID=p_bakasha_id
              AND c.taarich between tar_me and last_day(tar_me) 
              and  c.Kod_Rechiv in ( SELECT X FROM TABLE(CAST(Convert_String_To_Table( list_rechivim,  ',') AS MYTABTYPE))) ) p ;
              
end pro_rechivey_shonot_lerikuz;

function getNochechutChodshit(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                        p_taarich IN DATE,
                                        p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE) return number is
 sum_nochechut number;
begin
    sum_nochechut:=0;
    select sum(h.r1)  into sum_nochechut
    from
    ( select   taarich,
          max(case to_number(kod_rechiv) when 1 then  erech_rechiv end ) r1,
          max(case to_number(kod_rechiv) when 75 then  erech_rechiv end ) r75 
     from     
       ( select y.kod_rechiv,y.erech_rechiv,y.taarich
        from tb_chishuv_yomi_ovdim y
        where y.mispar_ishi = p_mispar_ishi
            and y.bakasha_id = p_bakasha_id
            and y.taarich between p_taarich and last_day(p_taarich) 
            and y.kod_rechiv in(1,75)) 
         group by    taarich
                ) h
     where   h.r75>0;    
    
    return sum_nochechut;
end getNochechutChodshit;

PROCEDURE Pro_get_num_rechivim(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                               p_taarich IN DATE,
                                                     p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                                     p_cur OUT CurType) IS
   tar_me date;       
    list_RechiveyHeadrut nvarchar2(100);        
    list_RechiveyShonot nvarchar2(100);        
   --p_taarich  DATE;                                                             
 BEGIN  
  -- p_taarich:=to_date('31/08/2010','dd/mm/yyyy');
   tar_me:= to_date('01/' ||  to_char(p_taarich,'mm/yyyy'),'dd/mm/yyyy');
   list_RechiveyHeadrut:=  '56,57,64,68,69,70,71,72,65';   
   list_RechiveyShonot:= '1,10,11,39,41,43,75,95,112,113,114,115,116,117,118,126,204,205';    
   
   --1 סוג העדרות
   --2 שונות
   open  p_cur for                  

 select   
          max(case to_number(p.sug) when 1 then  p.cnt end ) cnt_headrut ,
          max(case to_number(p.sug) when 2 then  p.cnt end ) cnt_shonot 
 from(
   select 1 sug, count(*) cnt
   from TB_CHISHUV_CHODESH_OVDIM C
   where c.Mispar_Ishi=p_mispar_ishi 
              and C.Bakasha_ID=p_bakasha_id
              AND c.taarich between tar_me and last_day(tar_me) 
              and  c.Kod_Rechiv in ( SELECT X FROM TABLE(CAST(Convert_String_To_Table( list_RechiveyHeadrut,  ',') AS MYTABTYPE))) 
   union
   
    select 2 sug, count(*) cnt
   from TB_CHISHUV_CHODESH_OVDIM C
   where c.Mispar_Ishi=p_mispar_ishi 
              and C.Bakasha_ID=p_bakasha_id
              AND c.taarich between tar_me and last_day(tar_me) 
              and  c.Kod_Rechiv in ( SELECT X FROM TABLE(CAST(Convert_String_To_Table( list_RechiveyShonot,  ',') AS MYTABTYPE)))  ) p;
    
END Pro_get_num_rechivim;




  PROCEDURE pro_get_rikuz_chodshi_temp(  p_Cur_Rechivim_Yomi OUT CurType ,
                                                                p_Cur_Rechivim_Chodshi OUT CurType,
                                                                p_Cur_Rechivey_Headrut OUT CurType,
                                                                p_Cur_Rechivey_Shonot OUT CurType,
                                                                p_Cur_Num_Rechivim OUT CurType,
                                                                p_mispar_ishi IN NUMBER,
                                                                p_taarich IN DATE,
                                                                p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE) IS
 
 BEGIN

     PKG_RIKUZ_AVODA.pro_get_rechivim_lerikuz_tmp(p_mispar_ishi,  p_taarich,p_bakasha_id,p_Cur_Rechivim_Yomi);
      
     PKG_RIKUZ_AVODA.pro_rechivim_chodshiim_tmp(p_mispar_ishi,p_taarich,p_bakasha_id,p_Cur_Rechivim_Chodshi);
            
     PKG_RIKUZ_AVODA.pro_rechivey_headrut_tmp(p_mispar_ishi ,  p_taarich ,  p_bakasha_id  ,   p_Cur_Rechivey_Headrut);     
     
     PKG_RIKUZ_AVODA.pro_rechivey_shonot_tmp(p_mispar_ishi ,  p_taarich ,  p_bakasha_id  ,   p_Cur_Rechivey_Shonot);     
      
     PKG_RIKUZ_AVODA.Pro_get_num_rechivim_tmp(p_mispar_ishi,p_taarich,p_bakasha_id,p_Cur_Num_Rechivim);
                                      
    
       EXCEPTION
         WHEN OTHERS THEN
              RAISE;   
 END pro_get_rikuz_chodshi_temp;



PROCEDURE pro_get_rechivim_lerikuz_tmp(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                                                p_taarich IN DATE,
                                                                p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                                                 p_cur OUT CurType) IS
    tar_me date;       
    list_rechivim nvarchar2(100);        
   -- p_taarich  DATE;                                                             
 BEGIN  
-- p_taarich:=to_date('31/08/2010','dd/mm/yyyy');
   tar_me:= to_date('01/' ||  to_char(p_taarich,'mm/yyyy'),'dd/mm/yyyy');
   list_rechivim:= '1,22,26,28,29,30,47,48,49,50,53,55,66,67,76,77,78,91,92,125,126,131,202,203,220,221';    
 open  p_cur for 
 
 
  select rechivim.*, hearot.heara
 from
 (                                                                                    
        select   TO_NUMBER(TO_CHAR(p.taarich ,'DD')) day_num, DayOfWeek(p.taarich) day_ot , 
          p.mispar_ishi, p.taarich,
          max(case  to_number(kod_rechiv)  when 1 then erech_rechiv end ) r1,
          max(case  to_number(kod_rechiv)  when 22 then erech_rechiv end )r22,
          max(case  to_number(kod_rechiv)  when 26 then erech_rechiv end )r26,
          max(case  to_number(kod_rechiv)  when 28 then erech_rechiv end )r28,
          max(case  to_number(kod_rechiv)  when 29 then erech_rechiv end )r29,
          max(case  to_number(kod_rechiv)  when 30 then erech_rechiv end )r30,
          max(case  to_number(kod_rechiv)  when 47 then erech_rechiv end )r47,
          max(case  to_number(kod_rechiv)  when 48 then erech_rechiv end )r48,
          max(case  to_number(kod_rechiv)  when 49 then erech_rechiv end )r49,
          max(case  to_number(kod_rechiv)  when 50 then erech_rechiv end )r50,
          max(case  to_number(kod_rechiv)  when 53 then erech_rechiv end )r53,
          max(case  to_number(kod_rechiv)  when 55 then erech_rechiv end )r55,
          max(case  to_number(kod_rechiv)  when 66 then erech_rechiv end )r66,
          max(case  to_number(kod_rechiv)  when 67 then erech_rechiv end )r67,
          max(case  to_number(kod_rechiv)  when 76 then erech_rechiv end )r76,
          max(case  to_number(kod_rechiv)  when 77 then erech_rechiv end )r77,
          max(case  to_number(kod_rechiv)  when 78 then erech_rechiv end )r78,
          max(case  to_number(kod_rechiv)  when 91 then erech_rechiv end )r91,
          max(case  to_number(kod_rechiv)  when 92 then erech_rechiv end )r92,
          max(case to_number(kod_rechiv) when 125 then erech_rechiv end ) r125 ,
          max(case to_number(kod_rechiv) when 126 then erech_rechiv end ) r126 ,
          max(case  to_number(kod_rechiv)  when 131 then erech_rechiv end )r131,
          max(case  to_number(kod_rechiv)  when 202 then erech_rechiv end )r202,
          max(case  to_number(kod_rechiv)  when 203 then erech_rechiv end )r203,
          max(case  to_number(kod_rechiv)  when 220 then erech_rechiv end )r220,
          max(case  to_number(kod_rechiv)  when 221 then erech_rechiv end )r221       
        from (
        SELECT h.mispar_ishi,  h.Kod_Rechiv, h.TAARICH ,y.Erech_Rechiv erech_rechiv
         FROM 
        ( select c.mispar_ishi,c.taarich,c.kod_rechiv,c.erech_rechiv
         from  TB_TMP_CHISHUV_YOMI_OVDIM C
         where c.Mispar_Ishi=p_mispar_ishi 
              and C.Bakasha_ID=p_bakasha_id
              AND c.taarich between tar_me and last_day(tar_me) 
              and  c.Kod_Rechiv in ( SELECT X FROM TABLE(CAST(Convert_String_To_Table( list_rechivim,  ',') AS MYTABTYPE)))   ) y ,
        (select D.mispar_ishi,D.taarich,R.kod_rechiv
        from
         (   select p_mispar_ishi mispar_ishi ,TO_DATE(x,'dd/mm/yyyy') taarich 
             from ( SELECT X from    TABLE(CAST(Convert_String_To_Table(String_Dates_Of_Period(to_char(p_taarich,'mm/yyyy')),',') AS mytabtype)))   ) D
        full join     
            (select p_mispar_ishi mispar_ishi ,x kod_rechiv
             from ( SELECT X FROM TABLE(CAST(Convert_String_To_Table(list_rechivim,  ',') AS MYTABTYPE))) )R
        on D.mispar_ishi = R.mispar_ishi   ) h
        where  h.MISPAR_ISHI = y.MISPAR_ISHI(+)
                and h.TAARICH= y.TAARICH(+)
                and h.KOD_RECHIV= y.Kod_Rechiv(+)  ) p
        GROUP BY p.mispar_ishi, p.taarich
        order  BY p.mispar_ishi, p.taarich ) rechivim,
       (select mushee.taarich,decode(mushee.KOD_MATZAV,null,hearot_tb.heara,'מושעה') heara
        from (select y.taarich,max(h.heara) heara
                 from (SELECT   Mispar_Ishi,Taarich,max(SYS_CONNECT_BY_PATH (kod_rechiv,'.'))KOD_RECHIV
                          FROM ( SELECT C. Mispar_Ishi,  C.Kod_Rechiv, c.Taarich,ROW_NUMBER () OVER (PARTITION BY c.Taarich ORDER BY c.kod_rechiv ASC) RN
                                      FROM TB_TMP_CHISHUV_YOMI_OVDIM C,CTB_RECHIVIM r
                                      WHERE  C.Bakasha_ID= p_bakasha_id --7106 
                                              AND C. Mispar_Ishi= p_mispar_ishi --19485  
                                              AND c.erech_rechiv>0
                                              AND r.yesh_heara=1
                                              AND r.kod_rechiv=c.kod_rechiv
                                              AND c.taarich between  tar_me and last_day(tar_me)  ) --AND TO_CHAR(c.Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy') )
                             CONNECT BY Taarich = PRIOR Taarich AND RN = PRIOR RN + 1
                             START WITH RN = 1
                             group by Mispar_Ishi,Taarich
                            ORDER BY Taarich) y,
                            CTB_HEAROT_RECHIVIM H,
                            (select  o.taarich,p.erech  
                             from (select p_mispar_ishi   mispar_ishi ,TO_DATE(x,'dd/mm/yyyy') taarich 
                                      from ( SELECT X from    TABLE(CAST(Convert_String_To_Table(String_Dates_Of_Period(to_char( p_taarich ,'mm/yyyy')),',') AS mytabtype)))  )o,
                                      PIRTEY_OVDIM p  
                             where o.mispar_ISHI=  p.mispar_ISHI(+)
                               and  o.taarich between   p.Me_taarich(+) and p.Ad_taarich(+)  
                                and P.KOD_natun(+) =9 ) m
                 WHERE   H.KOD_RECHIV=SUBSTR(Y.KOD_RECHIV,2,LENGTH(Y.KOD_RECHIV)-1)   
                      and ( (H.MUTAM_BITACHON=1 and m.erech is not null) or  m.erech is null) 
                      and  y.taarich =  m.taarich
                 GROUP BY y.TAARICH          ) hearot_tb,    
                (select  o.taarich , z.KOD_MATZAV 
                 from (select p_mispar_ishi   mispar_ishi ,TO_DATE(x,'dd/mm/yyyy') taarich 
                          from ( SELECT X from    TABLE(CAST(Convert_String_To_Table(String_Dates_Of_Period(to_char( p_taarich ,'mm/yyyy')),',') AS mytabtype)))  )o,
                          matzav_ovdim z
                  where  o.mispar_ISHI=  z.mispar_ISHI(+)
                      and  o.taarich between  z.TAARICH_HATCHALA(+) and z.TAARICH_SIYUM(+)  
                       and z.KOD_MATZAV(+) ='33' ) mushee
            where   mushee.taarich = hearot_tb.taarich(+) ) hearot
 where rechivim.taarich = hearot.taarich(+);
   
end pro_get_rechivim_lerikuz_tmp;

PROCEDURE pro_rechivim_chodshiim_tmp(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                                              p_taarich IN DATE,
                                                              p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                                             p_cur OUT CurType) IS
    tar_me date;       
    list_rechivim nvarchar2(200);        
  --  p_taarich  DATE;                                                             
 BEGIN  
 --p_taarich:=to_date('31/08/2010','dd/mm/yyyy');
   tar_me:= to_date('01/' ||  to_char(p_taarich,'mm/yyyy'),'dd/mm/yyyy');
   list_rechivim:= '1,5,22,26,28,29,30,44,47,48,49,50,53,55,66,67,76,77,78,91,92,100,101,102,103,108,119,120,121,122,125,126,131,146,202,203,219,220,221';    
   open  p_cur for                  
    select   
          max(case to_number(kod_rechiv) when 1 then  erech_rechiv end ) r1 ,
          max(case to_number(kod_rechiv) when 5 then  erech_rechiv end ) r5 ,
       --   max(case to_number(kod_rechiv) when 10 then  erech_rechiv end ) r10 ,
        -- max(case to_number(kod_rechiv) when 11 then  erech_rechiv end ) r11 ,
          max(case to_number(kod_rechiv) when 22 then  erech_rechiv end ) r22 ,
          max(case to_number(kod_rechiv) when 44 then  erech_rechiv end ) r44,
          max(case to_number(kod_rechiv) when 26 then  erech_rechiv end ) r26 ,
          max(case to_number(kod_rechiv) when 28 then  erech_rechiv end ) r28 ,
          max(case to_number(kod_rechiv) when 29 then  erech_rechiv end ) r29 ,
          max(case to_number(kod_rechiv) when 30 then erech_rechiv end ) r30 ,
     --     max(case to_number(kod_rechiv) when 39 then  erech_rechiv end ) r39 ,
     --     max(case to_number(kod_rechiv) when 41 then  erech_rechiv end ) r41,
      --    max(case to_number(kod_rechiv) when 43 then  erech_rechiv end ) r43,
          max(case to_number(kod_rechiv) when 47 then  erech_rechiv end ) r47,
          max(case to_number(kod_rechiv) when 48 then  erech_rechiv end ) r48,
          max(case to_number(kod_rechiv) when 49 then  erech_rechiv end ) r49,
          max(case to_number(kod_rechiv) when 50 then  erech_rechiv end ) r50,
          max(case to_number(kod_rechiv) when 53 then  erech_rechiv end ) r53,
          max(case to_number(kod_rechiv) when 55 then  erech_rechiv end ) r55,
          max(case to_number(kod_rechiv) when 66 then  erech_rechiv end ) r66,
          max(case to_number(kod_rechiv) when 67 then  erech_rechiv end ) r67,
          max(case to_number(kod_rechiv) when 76 then  erech_rechiv end ) r76,
          max(case to_number(kod_rechiv) when 77 then  erech_rechiv end ) r77 ,
          max(case to_number(kod_rechiv) when 78 then erech_rechiv end ) r78 ,
          max(case to_number(kod_rechiv) when 91 then  erech_rechiv end ) r91 ,
          max(case to_number(kod_rechiv) when 92 then  erech_rechiv end ) r92 ,
   --      max(case to_number(kod_rechiv) when 95 then  erech_rechiv end ) r95 ,
          max(case to_number(kod_rechiv) when 100 then  erech_rechiv end ) r100,
          max(case to_number(kod_rechiv) when 101 then  erech_rechiv end ) r101 ,
          max(case to_number(kod_rechiv) when 102 then  erech_rechiv end ) r102 ,
          max(case to_number(kod_rechiv) when 103 then  erech_rechiv end ) r103 ,
          max(case to_number(kod_rechiv) when 108 then  erech_rechiv end ) r108 ,
          max(case to_number(kod_rechiv) when 119 then  erech_rechiv end ) r119 ,
          max(case to_number(kod_rechiv) when 120 then  erech_rechiv end ) r120 ,
          max(case to_number(kod_rechiv) when 121 then  erech_rechiv end ) r121 ,
          max(case to_number(kod_rechiv) when 122 then  erech_rechiv end ) r122 ,
          max(case to_number(kod_rechiv) when 125 then  erech_rechiv end ) r125 ,
          max(case to_number(kod_rechiv) when 126 then  erech_rechiv end ) r126,
          max(case to_number(kod_rechiv) when 131 then  erech_rechiv end ) r131 ,
          max(case to_number(kod_rechiv) when 146 then  erech_rechiv end ) r146 ,
          max(case to_number(kod_rechiv) when 202 then  erech_rechiv end ) r202 ,
          max(case to_number(kod_rechiv) when 203 then  erech_rechiv end ) r203,
          max(case to_number(kod_rechiv) when 219 then  erech_rechiv end ) r219 ,     
          max(case to_number(kod_rechiv) when 220 then  erech_rechiv end ) r220 ,
          max(case to_number(kod_rechiv) when 221 then  erech_rechiv end ) r221 
          
   from(     
   select  C.KOD_RECHIV,C.ERECH_RECHIV
   from TB_TMP_CHISHUV_CHODESH_OVDIM C
   where c.Mispar_Ishi=p_mispar_ishi 
              and C.Bakasha_ID=p_bakasha_id
              AND c.taarich between tar_me and last_day(tar_me) 
              and  c.Kod_Rechiv in ( SELECT X FROM TABLE(CAST(Convert_String_To_Table( list_rechivim,  ',') AS MYTABTYPE))) ) p ;
                  
end pro_rechivim_chodshiim_tmp;

PROCEDURE pro_rechivey_headrut_tmp(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                                           p_taarich IN DATE,
                                                                                             p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                                                                         p_cur OUT CurType) IS
    tar_me date;       
    list_rechivim nvarchar2(100);        
 --   p_taarich  DATE;                                                             
 BEGIN  
 --p_taarich:=to_date('31/08/2010','dd/mm/yyyy');
   tar_me:= to_date('01/' ||  to_char(p_taarich,'mm/yyyy'),'dd/mm/yyyy');
   list_rechivim:= '53,56,57,60,61,62,64,65,66,67,68,69,70,71,72,266';    
   
   open  p_cur for                  
    select   
          max(case to_number(kod_rechiv) when 53 then  erech_rechiv end ) r53 ,
          max(case to_number(kod_rechiv) when 56 then  erech_rechiv end ) r56 ,
          max(case to_number(kod_rechiv) when 57 then  erech_rechiv end ) r57 ,
          max(case to_number(kod_rechiv) when 60 then  erech_rechiv end ) r60 ,
          max(case to_number(kod_rechiv) when 61 then  erech_rechiv end ) r61 ,
          max(case to_number(kod_rechiv) when 62 then  erech_rechiv end ) r62,
     --     max(case to_number(kod_rechiv) when 63 then  erech_rechiv end ) r63,
          max(case to_number(kod_rechiv) when 64 then  erech_rechiv end ) r64,
          max(case to_number(kod_rechiv) when 65 then  erech_rechiv end ) r65,
          max(case to_number(kod_rechiv) when 66 then  erech_rechiv end ) r66,
          max(case to_number(kod_rechiv) when 67 then  erech_rechiv end ) r67,
          max(case to_number(kod_rechiv) when 68 then  erech_rechiv end ) r68,
          max(case to_number(kod_rechiv) when 69 then  erech_rechiv end ) r69 ,
          max(case to_number(kod_rechiv) when 70 then  erech_rechiv end ) r70,
          max(case to_number(kod_rechiv) when 71 then  erech_rechiv end ) r71 ,
          max(case to_number(kod_rechiv) when 72 then  erech_rechiv end ) r72,
          max(case to_number(kod_rechiv) when 266 then  erech_rechiv end ) r266
   from(     
   select  C.KOD_RECHIV,C.ERECH_RECHIV
   from TB_TMP_CHISHUV_CHODESH_OVDIM C
   where c.Mispar_Ishi=p_mispar_ishi 
              and C.Bakasha_ID=p_bakasha_id
              AND c.taarich between tar_me and last_day(tar_me) 
              and  c.Kod_Rechiv in ( SELECT X FROM TABLE(CAST(Convert_String_To_Table( list_rechivim,  ',') AS MYTABTYPE))) ) p ;
         
end pro_rechivey_headrut_tmp;


PROCEDURE pro_rechivey_shonot_tmp(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                                   p_taarich IN DATE,
                                                              p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                                              p_cur OUT CurType) IS
    tar_me date;       
    list_rechivim nvarchar2(100);        
   --p_taarich  DATE;                                                             
 BEGIN  
 --p_taarich:=to_date('31/08/2010','dd/mm/yyyy');
   tar_me:= to_date('01/' ||  to_char(p_taarich,'mm/yyyy'),'dd/mm/yyyy');
   list_rechivim:= '1,10,11,12,39,41,43,75,95,112,113,114,115,116,117,118,126,204,205';    
   
   open  p_cur for                  
    select   
          max(case to_number(kod_rechiv) when 1 then  erech_rechiv end ) r1,
          max(case to_number(kod_rechiv) when 10 then  erech_rechiv end ) r10 ,
          max(case to_number(kod_rechiv) when 11 then  erech_rechiv end ) r11 ,
          max(case to_number(kod_rechiv) when 12 then  erech_rechiv end ) r12 ,
          max(case to_number(kod_rechiv) when 39 then  erech_rechiv end ) r39,
          max(case to_number(kod_rechiv) when 41 then  erech_rechiv end ) r41,
          max(case to_number(kod_rechiv) when 43 then  erech_rechiv end ) r43,
          max(case to_number(kod_rechiv) when 75 then  erech_rechiv end ) r75,
          max(case to_number(kod_rechiv) when 95 then  erech_rechiv end ) r95,
          max(case to_number(kod_rechiv) when 112 then  erech_rechiv end ) r112,
          max(case to_number(kod_rechiv) when 113 then  erech_rechiv end ) r113,
          max(case to_number(kod_rechiv) when 114 then  erech_rechiv end ) r114,
          max(case to_number(kod_rechiv) when 115 then  erech_rechiv end ) r115,
          max(case to_number(kod_rechiv) when 116 then  erech_rechiv end ) r116,
          max(case to_number(kod_rechiv) when 117 then  erech_rechiv end ) r117,
          max(case to_number(kod_rechiv) when 118 then  erech_rechiv end ) r118,
          max(case to_number(kod_rechiv) when 126 then  erech_rechiv end ) r126,
          max(case to_number(kod_rechiv) when 204 then  erech_rechiv end ) r204,
          max(case to_number(kod_rechiv) when 205 then  erech_rechiv end ) r205,
          pkg_rikuz_avoda.getNochechutChodshitTemp(p_mispar_ishi , tar_me, p_bakasha_id) r1b
   from(     
   select  C.KOD_RECHIV,C.ERECH_RECHIV
   from TB_TMP_CHISHUV_CHODESH_OVDIM C
   where c.Mispar_Ishi=p_mispar_ishi 
              and C.Bakasha_ID=p_bakasha_id
              AND c.taarich between tar_me and last_day(tar_me) 
              and  c.Kod_Rechiv in ( SELECT X FROM TABLE(CAST(Convert_String_To_Table( list_rechivim,  ',') AS MYTABTYPE))) ) p ;
              
end pro_rechivey_shonot_tmp;

function getNochechutChodshitTemp(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                        p_taarich IN DATE,
                                        p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE) return number is
 sum_nochechut number;
begin
    sum_nochechut:=0;
    select sum(h.r1)  into sum_nochechut
    from
    ( select   taarich,
          max(case to_number(kod_rechiv) when 1 then  erech_rechiv end ) r1,
          max(case to_number(kod_rechiv) when 75 then  erech_rechiv end ) r75 
     from     
       ( select y.kod_rechiv,y.erech_rechiv,y.taarich
        from tb_tmp_chishuv_yomi_ovdim y
        where y.mispar_ishi = p_mispar_ishi
            and y.bakasha_id = p_bakasha_id
            and y.taarich between p_taarich and last_day(p_taarich) 
            and y.kod_rechiv in(1,75)) 
         group by    taarich
                ) h
     where   h.r75>0;    
    
    return sum_nochechut;
end getNochechutChodshitTemp;
PROCEDURE Pro_get_num_rechivim_tmp(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                                            p_taarich IN DATE,
                                                                                             p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                                                                         p_cur OUT CurType) IS
   tar_me date;       
    list_RechiveyHeadrut nvarchar2(100);        
    list_RechiveyShonot nvarchar2(100);        
   --p_taarich  DATE;                                                             
 BEGIN
  -- p_taarich:=to_date('31/08/2010','dd/mm/yyyy');
   tar_me:= to_date('01/' ||  to_char(p_taarich,'mm/yyyy'),'dd/mm/yyyy');
   list_RechiveyHeadrut:=  '56,57,64,68,69,70,71,72,65';   
   list_RechiveyShonot:= '1,10,11,39,41,43,75,95,112,113,114,115,116,117,118,126,204,205';    
   
   --1 סוג העדרות
   --2 שונות
   open  p_cur for                  

 select   
          max(case to_number(p.sug) when 1 then  p.cnt end ) cnt_headrut ,
          max(case to_number(p.sug) when 2 then  p.cnt end ) cnt_shonot 
 from(
   select 1 sug, count(*) cnt
   from TB_TMP_CHISHUV_CHODESH_OVDIM C
   where c.Mispar_Ishi=p_mispar_ishi 
              and C.Bakasha_ID=p_bakasha_id
              AND c.taarich between tar_me and last_day(tar_me) 
              and  c.Kod_Rechiv in ( SELECT X FROM TABLE(CAST(Convert_String_To_Table( list_RechiveyHeadrut,  ',') AS MYTABTYPE))) 
   union
   
    select 2 sug, count(*) cnt
   from TB_TMP_CHISHUV_CHODESH_OVDIM C
   where c.Mispar_Ishi=p_mispar_ishi 
              and C.Bakasha_ID=p_bakasha_id
              AND c.taarich between tar_me and last_day(tar_me) 
              and  c.Kod_Rechiv in ( SELECT X FROM TABLE(CAST(Convert_String_To_Table( list_RechiveyShonot,  ',') AS MYTABTYPE)))  ) p;
END Pro_get_num_rechivim_tmp;





END PKG_RIKUZ_AVODA;
/
