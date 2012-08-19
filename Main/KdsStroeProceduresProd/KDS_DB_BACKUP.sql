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
TYPE    CurType      IS    REF  CURSOR;
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
   procedure create_file_rechvey_nochechot(p_from_date in date, p_to_date in date, p_maamad in pivot_pirtey_ovdim.maamad%type,  p_cur OUT CurType);
END PKG_FILES;
/


CREATE OR REPLACE PACKAGE PKG_HR AS
/******************************************************************************
   NAME:       PKG_HR
   PURPOSE:

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        7/8/2012      SaraC       1. Created this package.
******************************************************************************/

  PROCEDURE  chk_creation_date_meafyenim(shem_mvew VARCHAR);

END PKG_HR;
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
 
                                                                                                                 																 																	 
END PKG_OVDIM;
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
END Pkg_Reports;
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
                                        
function getMaxRechivYomi(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                        p_taarich IN DATE,
                                        p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                        p_kod_rechiv IN tb_chishuv_yomi_ovdim.kod_rechiv%TYPE) return number;
                                                                                                                                  
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
                                        
 function getMaxRechivYomiTemp(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                        p_taarich IN DATE,
                                        p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                        p_kod_rechiv IN tb_chishuv_yomi_ovdim.kod_rechiv%TYPE) return number;
                                                                                                                                                                                                                                                                      
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
		--	   ORDER BY p.mispar_ishi asc,c.taarich desc
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
					--			decode( instr((sysdate-1)-b.ad_taarich,'-'),1,trunc(sysdate-1), b.ad_taarich) date_b,
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
					--	and  a.mispar_ishi=67761
	UNION
			   			SELECT  a.mispar_ishi, a.me_taarich taarich_hatchala,
						   		DECODE(a.ad_taarich,NULL,TRUNC(SYSDATE-1),DECODE( INSTR((SYSDATE-1)-a.ad_taarich,'-'),1,TRUNC(SYSDATE-1), a.ad_taarich)) date_a,
						--		decode( instr((sysdate-1)-b.ad_taarich,'-'),1,trunc(sysdate-1), b.ad_taarich) date_b,
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
				--	)
			--	where mispar_ishi=224;


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
                  --    AND ya.measher_o_mistayeg IS NOT NULL
                      AND NVL(ya.status,-1)<>0
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
              --   ,     TB_MISPAR_ISHI_CHISHUV_BAK t
               WHERE o.status=1
            AND  o.taarich BETWEEN v_me_taarich AND v_ad_taarich
                --and o.MISPAR_ISHI =44965
          --  AND T.NUM_PACK=101--
            --   AND t.mispar_ishi=o.mispar_ishi--
          --   AND  o.taarich BETWEEN T.TAARICH AND LAST_DAY( T.TAARICH)  --
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
          --   TB_MISPAR_ISHI_CHISHUV_BAK t,
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
       --   AND p.taarich BETWEEN v_me_taarich AND v_ad_taarich
          AND  o.status=2
     -- AND T.NUM_PACK=101--
     --  AND t.mispar_ishi=o.mispar_ishi--
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
                     AND a.taarich_source = p.taarich
                      AND a.mispar_sidur= p.mispar_sidur
                      AND a.shat_hatchala = p.shat_hatchala_sidur) sum_km,
                                     (select c.snif from   tmp_catalog c ,tb_peilut_ovdim p
                      where p.MAKAT_NESIA  = C.MAKAT8(+)   
                    and p.TAARICH = C.ACTIVITY_DATE(+) 
                     AND a.taarich_source = p.taarich
                      AND a.mispar_sidur= p.mispar_sidur
                      AND a.shat_hatchala = p.shat_hatchala_sidur
                    and p.makat_nesia = first_makat
                    and rownum=1) snif_metugbar from
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
                nvl(DECODE (sm.headrut_type_kod,
                        NULL, (s.shat_gmar - s.shat_hatchala)*60*24,
                      0),0)
                   dakot_nochehut,
                nvl(DECODE (sm.headrut_type_kod,
                        NULL, 0,
                        (s.shat_gmar - s.shat_hatchala)*60*24),0)
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
                         FROM PIVOT_PIRTEY_OVDIM PO
                 WHERE  (p_tar_me BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
              OR   p_tar_ad  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
              OR   po.ME_TARICH>=p_tar_me AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=  p_tar_ad )) ppo,
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
                  and o.kod_hevra=580
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
                AND i.kod_isuk = PPO.ISUK) a
                order by  a.TAARICH;


      v_rec         p_cur%ROWTYPE;
      output_file   UTL_FILE.FILE_TYPE;
      v_line        VARCHAR (340);
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
       if (v_rec.dakot_nochehut=0) then v_line := v_line ||'0000'; else v_line := v_line || to_char(to_number(v_rec.dakot_nochehut),'0000'); end if;
          v_line := v_line || ';';
         if (v_rec.dakot_nochehut_headrut=0) then v_line := v_line ||'0000'; else v_line := v_line || to_char(to_number(v_rec.dakot_nochehut_headrut),'0000'); end if;
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

procedure create_file_rechvey_nochechot(p_from_date in date, p_to_date in date, p_maamad in pivot_pirtey_ovdim.maamad%type,p_cur OUT CurType) 
 IS
BEGIN
OPEN  p_cur FOR
select distinct a.mispar_ishi,O.SHEM_PRAT, O.SHEM_MISH, a.maamad,to_char(Dakot_bafoal/60,'9,999.99') shaot_bafoal,to_char(Dakot_letashleum/60,'9,999.99') shaot_letashlum,r100,  r150,r200,yemay_ovoda,hofesh,machala
from pivot_pirtey_ovdim a, ovdim o,
        (select  mispar_ishi, bakasha_id,Taarich,
        max(CASE kod_rechiv  WHEN 18 THEN ERECH_RECHIV else 0 end ) Dakot_bafoal,
        max(CASE kod_rechiv  WHEN 1 THEN ERECH_RECHIV else 0 end ) Dakot_letashleum,        
        max(CASE kod_rechiv  WHEN 100 THEN ERECH_RECHIV else 0 end ) r100,
         max(CASE kod_rechiv  WHEN 76 THEN ERECH_RECHIV else 0 end ) r125,
        max(CASE kod_rechiv  WHEN 77 THEN ERECH_RECHIV else 0 end ) r150,
        max(CASE kod_rechiv  WHEN 78 THEN ERECH_RECHIV else 0  end ) r200,
        max(CASE kod_rechiv  WHEN 109 THEN ERECH_RECHIV else 0  end ) yemay_ovoda,
        max(CASE kod_rechiv  WHEN 270 THEN ERECH_RECHIV else 0  end ) hofesh,
        max(CASE kod_rechiv  WHEN 60 THEN ERECH_RECHIV else 0 end ) machala 
        from
        (select ch.mispar_ishi, ch.bakasha_id,ch.Taarich,ch.kod_rechiv,ch.ERECH_RECHIV
         from TB_CHISHUV_CHODESH_OVDIM ch where ch.TAARICH between p_from_date and p_to_date
         and ch.bakasha_id = (select max(bakasha_id) from TB_CHISHUV_CHODESH_OVDIM c where c.MISPAR_ISHI =  ch.mispar_ishi  and TAARICH between p_from_date and p_to_date))
        group by mispar_ishi, bakasha_id,Taarich
        ) Chishuv

where p_from_date > A.me_TARICH
and a.mispar_ishi = o.mispar_ishi
and substr(maamad,2,2) = p_maamad
and Chishuv.mispar_ishi = A.mispar_ishi
and Chishuv.taarich between p_from_date and p_to_date;
END create_file_rechvey_nochechot;
END PKG_FILES;
/


CREATE OR REPLACE PACKAGE BODY PKG_HR AS
/******************************************************************************
   NAME:       PKG_HR
   PURPOSE:

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        7/8/2012      SaraC       1. Created this package body.
******************************************************************************/

 
PROCEDURE  chk_creation_date_meafyenim(shem_mvew VARCHAR)  IS
CreationDt DATE;

BEGIN
CreationDt:=SYSDATE+1;

  SELECT MAX( creation_date)
 INTO   CreationDt
FROM apps.EGD_MKS_MeAFYENim_ovdim@kds_gw
WHERE     mispar_ishi='77690';

  IF (TRUNC( CreationDt) = TRUNC(SYSDATE-1)) THEN
     dbms_mview.REFRESH(shem_mvew,'c');
   ELSE IF (TRUNC( CreationDt) < TRUNC(SYSDATE)) THEN
       INSERT INTO TB_LOG_TAHALICH
    VALUES (16,1,1,SYSDATE,'',10,'','creationdate= '||TO_CHAR(CreationDt,'dd/mm/yyyy hh24:mi:ssss'));
                     RAISE_APPLICATION_ERROR(-2005, 'log_tahalich', TRUE);
         ELSE IF (TRUNC( CreationDt) = TRUNC(SYSDATE)) THEN
                  dbms_mview.REFRESH(shem_mvew,'c');
                ELSE 
       INSERT INTO TB_LOG_TAHALICH
    VALUES (16,1,1,SYSDATE,'',10,'','creationdate= '||TO_CHAR(CreationDt,'dd/mm/yyyy hh24:mi:ssss'));
                              RAISE_APPLICATION_ERROR(-2005, 'log_tahalich', TRUE);
                END IF;
         END IF;
   END IF;
     
    EXCEPTION
            WHEN OTHERS THEN
                          RAISE; 
END chk_creation_date_meafyenim;

END PKG_HR;
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
                               --    AND measher_o_mistayeg IS NOT NULL
                                --    AND measher_o_mistayeg <>2
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
                           OR ((p_kod_maamad IN (1,2)) AND (v.MAAMAD LIKE p_kod_maamad ||'%' ))        )
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
          substr(p.maamad,2,2) kod_maamad,p.DIRUG,p.darga,
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
                         WHERE     mispar_ishi=p_mispar_ishi
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
                                            AND y.taarich between v_tar_me and last_day(v_tar_me) --    AND  TO_CHAR(Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy')
                                            AND y.Erech_Rechiv IS NOT NULL
                                            AND y.Bakasha_ID=p_bakasha_id
                                            AND y.kod_rechiv=120)
                                     WHEN 78 THEN (SELECT y.erech_rechiv FROM TB_CHISHUV_CHODESH_OVDIM y
                                            WHERE y. Mispar_Ishi=p_mispar_ishi
                                            AND y.taarich between v_tar_me and last_day(v_tar_me) --    AND  TO_CHAR(Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy')
                                            AND y.Erech_Rechiv IS NOT NULL
                                            AND y.Bakasha_ID=p_bakasha_id
                                            AND y.kod_rechiv=121 )
                                     WHEN 1 THEN (SELECT y.erech_rechiv FROM TB_CHISHUV_CHODESH_OVDIM y
                                            WHERE y. Mispar_Ishi=p_mispar_ishi
                                            AND y.taarich between v_tar_me and last_day(v_tar_me) --    AND  TO_CHAR(Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy')
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
                             (SELECT COUNT(Erech) Mutam_Bitachon
                            FROM PIRTEY_OVDIM
                            WHERE  Mispar_Ishi=p_mispar_ishi
                            AND Me_taarich <=p_taarich
                            AND Ad_taarich>=p_taarich
                            AND Kod_Natun=9) M
            WHERE H.KOD_RECHIV=SUBSTR(Y.KOD_RECHIV,2,LENGTH(Y.KOD_RECHIV)-1)
           AND (M.MUTAM_BITACHON=H.MUTAM_BITACHON OR H.MUTAM_BITACHON IS NULL)
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
                                            AND y.taarich between v_tar_me and last_day(v_tar_me)    --    AND  TO_CHAR(Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy')
                                            AND y.Erech_Rechiv IS NOT NULL
                                            AND y.Bakasha_ID=p_bakasha_id
                                            AND y.kod_rechiv=120)
                                     WHEN 78 THEN (SELECT y.erech_rechiv FROM TB_CHISHUV_CHODESH_OVDIM y
                                            WHERE y. Mispar_Ishi=p_mispar_ishi
                                            AND y.taarich between v_tar_me and last_day(v_tar_me)    --    AND  TO_CHAR(Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy')
                                            AND y.Erech_Rechiv IS NOT NULL
                                            AND y.Bakasha_ID=p_bakasha_id
                                            AND y.kod_rechiv=121 )
                                     WHEN 1 THEN (SELECT y.erech_rechiv FROM TB_CHISHUV_CHODESH_OVDIM y
                                            WHERE y. Mispar_Ishi=p_mispar_ishi
                                            AND y.taarich between v_tar_me and last_day(v_tar_me)    --    AND  TO_CHAR(Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy')
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
                             (SELECT COUNT(Erech) Mutam_Bitachon
                            FROM PIRTEY_OVDIM
                            WHERE  Mispar_Ishi=p_mispar_ishi
                            AND Me_taarich <=p_taarich
                            AND Ad_taarich>=p_taarich
                            AND Kod_Natun=9) M
             WHERE H.KOD_RECHIV=SUBSTR(Y.KOD_RECHIV,2,LENGTH(Y.KOD_RECHIV)-1)
           AND (M.MUTAM_BITACHON=H.MUTAM_BITACHON OR H.MUTAM_BITACHON IS NULL)
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

    SELECT COUNT(*)     INTO meadken_acharon
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
        WHERE     po.mispar_ishi=p_mispar_ishi
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
                         WHERE     po.mispar_ishi=p_mispar_ishi
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
           TRUNC(taarich)        = p_obj_peilut_ovdim.taarich AND
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
        --    AND (s.bitul_o_hosafa IS NULL OR s.bitul_o_hosafa<>1);

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
                       --    OVDIM O
           WHERE
                        --         P.MISPAR_ISHI = O.MISPAR_ISHI AND
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
                        --      ( t.SHAT_HATCHALA_SIDUR =p_shat_hatchala  or t.SHAT_HATCHALA_SIDUR =p_shat_hatchala +1  ) and
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

PROCEDURE    pro_get_ovdim_to_period_ByCode(p_code IN  NUMBER,p_start_date IN DATE,
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
                WHERE p.MISPAR_ISHI =    p_mispar_ishi
                      AND p.KOD_NATUN =      p_kod_natun
                      AND  p_taarich  BETWEEN p.ME_TAARICH AND p.AD_TAARICH;        
                      
                RETURN v_erech;                                           
EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END        func_get_erech_by_kod_natun;        


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
  


END Pkg_Ovdim;
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
    ParamQry := ParamQry || ' Activity.makat_nesia like  ''%' || P_Makat || '%'' AND ';
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
DBMS_OUTPUT.PUT_LINE ('5:CountRows'  || CountRows);
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
                    select h.*,Ov.kod_hevra,Ov.shem_mish|| '' '' ||  Ov.shem_prat full_name ,
                                         (Pkg_Ovdim.fun_get_meafyen_oved(h.mispar_ishi, 3,''' ||  P_ENDDATE  || ''' )) START_TIME_ALLOWED ,
                                         (Pkg_Ovdim.fun_get_meafyen_oved(h.mispar_ishi, 4,''' ||  P_ENDDATE  || ''')) END_TIME_ALLOWED  ,
                                isuk.teur_isuk ,Ezor.Teur_ezor,Maamad.teur_maamad_hr,snif.teur_snif_av 

                       from  OVDIM Ov  ,
                                 CTB_EZOR Ezor ,
                                 CTB_MAAMAD Maamad, 
                                 CTB_ISUK Isuk,
                                 CTB_SNIF_AV Snif,    
                                 ( SELECT * 
                                                                        FROM ( SELECT   t.mispar_ishi ,  min( t.ME_TARICH) OVER (PARTITION BY t.mispar_ishi) me_tarich,
                                                                                                 ad_tarich,  t.SNIF_AV,t.EZOR,MAAMAD,t.ISUK, 
                                                                                                 row_number() OVER (PARTITION BY t.mispar_ishi ORDER BY me_tarich desc) seq
                                                                                    FROM  PIVOT_PIRTEY_OVDIM t
                                                                                    WHERE t.isuk IS NOT NULL 
                                                                                        AND ( ''' || P_STARTDATE  || ''' BETWEEN ME_TARICH  AND NVL(t.ad_TARICH,TO_DATE( ''01/01/9999'' , ''dd/mm/yyyy'' ))
                                                                                             OR ''' || P_ENDDATE  || '''  BETWEEN ME_TARICH AND NVL(t.ad_TARICH,TO_DATE( ''01/01/9999'' , ''dd/mm/yyyy'' )) 
                                                                                             OR t.ME_TARICH>= ''' || P_STARTDATE  || ''' AND NVL(t.ad_TARICH,TO_DATE( ''01/01/9999'' ,''dd/mm/yyyy'' ))<= ''' || P_ENDDATE  || ''' )';
                                                            GeneralQry := GeneralQry || ParamQry2; 
                                                            GeneralQry := GeneralQry || '  ORDER BY mispar_ishi) a
                                                                        WHERE a.seq=1 ) Details,
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
                                           
        where   ov.MISPAR_ISHI = Details.MISPAR_ISHI
                  and  Details.MISPAR_ISHI =  h.MISPAR_ISHI
                  AND (h.taarich BETWEEN Details.me_tarich AND Details.ad_tarich)
                  AND Ezor.kod_ezor(+)  = Details.ezor
                  AND EZOR.KOD_HEVRA = OV.KOD_HEVRA
                  AND Isuk.kod_isuk(+) = details.isuk
                  AND Isuk.KOD_HEVRA = OV.KOD_HEVRA
                  AND Snif.kod_snif_av(+) = details.snif_av
                  AND Snif.KOD_HEVRA = OV.KOD_HEVRA
                  AND maamad.kod_maamad_hr(+)  =  details.maamad 
                  AND MAAMAD.KOD_HEVRA = OV.KOD_HEVRA                                          
                                                   
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
                  SELECT    Y. MISPAR_ISHI,Y.MISPAR_SIDUR, TO_CHAR( Y.TAARICH_IDKUN,'dd/mm/yyyy HH24:ss:mi') TAARICH_IDKUN,TO_CHAR(Y.TAARICH,'dd/mm/yyyy') TAARICH
                  FROM TB_IDKUN_RASHEMET  Y
                  WHERE Y.GOREM_MEADKEN=TO_NUMBER(P_MIS_RASHEMET) AND
                        Y.TAARICH_IDKUN >= TaarichIdkun and  Y.TAARICH_IDKUN < (TO_DATE(P_TAARICH_CA,'dd/mm/yyyy')+1)
                GROUP BY Y.TAARICH_IDKUN ,Y.TAARICH,Y.MISPAR_ISHI ,Y.MISPAR_SIDUR  
                  ORDER BY Y.TAARICH_IDKUN ,Y.TAARICH,MISPAR_ISHI ,Y.MISPAR_SIDUR;                                        
                  
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
  CDate TB_CHISHUV_YOMI_OVDIM.Taarich%TYPE; 
  CMispar_Ishi TB_CHISHUV_YOMI_OVDIM.Mispar_ishi%TYPE; 
  CKod_Rechiv TB_CHISHUV_YOMI_OVDIM.kod_rechiv%TYPE;
  CURSOR cursor_GroupBy(p_Month VARCHAR)  IS  
                                    SELECT  DayCalc.Mispar_ishi, DayCalc.kod_rechiv 
                                    FROM TB_CHISHUV_YOMI_OVDIM DayCalc 
                                    WHERE TO_CHAR(Taarich,'MM/YYYY') =  p_Month 
                                            AND DayCalc.BAKASHA_ID = (
                                                        SELECT   MAX(YOMI.BAKASHA_ID) LastBakasha
                                                        FROM  TB_CHISHUV_YOMI_OVDIM Yomi
                                                        WHERE TO_CHAR(Taarich,'MM/YYYY') = CurrentMonth
                                                    )
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
                                    AND TO_CHAR(Taarich,'MM/YYYY') =  p_Month 
                                    AND DayCalc.BAKASHA_ID = (
                                                SELECT   MAX(YOMI.BAKASHA_ID) LastBakasha
                                                FROM  TB_CHISHUV_YOMI_OVDIM Yomi
                                                WHERE TO_CHAR(Taarich,'MM/YYYY') = CurrentMonth
                                            )                                    
                                    ORDER BY TO_CHAR(Taarich,'DD');                                    
                                    

BEGIN
  --CurrentMonth :=  '11/2009'; 
 

OPEN cursor_GroupBy(CurrentMonth) ;

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
    --    P_TAARICH        date;                                                                        
BEGIN
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
                    g.F5,g.NOT_ON_TIME,g.PUBLIC_COMPLAINTS --,
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
              s.SHAT_HATCHALA SHAT_SIDUR, s.TAARICH,s.MISPAR_ISHI --,
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
                          to_char(R.TAARICH_IDKUN,'dd/mm/yyyy HH24:ss') TAARICH_IDKUN,R.TAARICH_IDKUN TAARICH_IDKUN_DATE,
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
        WHERE Ch.Mispar_Ishi =p_mispar_ishi
        and CH.KOD_RECHIV=R.KOD_RECHIV
      -- and  R.LETZUGA_BESIKUM_CHODSHI is not  null
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
        and o.mispar_sidur<>99200;
end  get_query4;                                                  
END Pkg_Reports;
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
       --   max(case to_number(kod_rechiv) when 126 then  erech_rechiv end ) r126,
       pkg_rikuz_avoda.getMaxRechivYomi(p_mispar_ishi , tar_me, p_bakasha_id,126) r126,
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

function getMaxRechivYomi(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                        p_taarich IN DATE,
                                        p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                        p_kod_rechiv IN tb_chishuv_yomi_ovdim.kod_rechiv%TYPE) return number is
     v_erech_rechiv number;                                   
begin
     select nvl(max(y.erech_rechiv),0) into v_erech_rechiv
        from tb_chishuv_yomi_ovdim y
        where y.mispar_ishi =p_mispar_ishi 
            and y.bakasha_id =p_bakasha_id
            and y.taarich   between  p_taarich and last_day(p_taarich) 
            and y.kod_rechiv=p_kod_rechiv;
 
    return v_erech_rechiv;

  EXCEPTION
    WHEN NO_DATA_FOUND THEN
        return 0;
    WHEN OTHERS THEN
              RAISE;         
end getMaxRechivYomi;
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
      --    max(case to_number(kod_rechiv) when 126 then  erech_rechiv end ) r126,
      pkg_rikuz_avoda.getMaxRechivYomiTemp(p_mispar_ishi , tar_me, p_bakasha_id,126) r126,
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

function getMaxRechivYomiTemp(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                        p_taarich IN DATE,
                                        p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                        p_kod_rechiv IN tb_chishuv_yomi_ovdim.kod_rechiv%TYPE) return number is
     v_erech_rechiv number;                                   
begin
     select nvl(max(y.erech_rechiv),0) into v_erech_rechiv
        from tb_tmp_chishuv_yomi_ovdim y
        where y.mispar_ishi =p_mispar_ishi 
            and y.bakasha_id =p_bakasha_id
            and y.taarich   between  p_taarich and last_day(p_taarich) 
            and y.kod_rechiv=p_kod_rechiv;
 
    return v_erech_rechiv;

  EXCEPTION
    WHEN NO_DATA_FOUND THEN
        return 0;
    WHEN OTHERS THEN
              RAISE;         
end getMaxRechivYomiTemp;
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
