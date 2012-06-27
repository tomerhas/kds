CREATE OR REPLACE PACKAGE          PKG_BATCH AS
TYPE    CurType      IS    REF  CURSOR;
/******************************************************************************
   NAME:       PKG_BATCH
   PURPOSE:

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        26/04/2009             1. Created this package.
******************************************************************************/

PROCEDURE pro_ins_log_bakasha(p_bakasha_id  IN tb_log_bakashot.bakasha_id%type,
                   p_taarich_idkun  IN tb_log_bakashot.taarich_idkun_acharon%type,
                 p_sug_hodaa  IN tb_log_bakashot.sug_hodaa%type,
                 p_kod_tahalich IN tb_log_bakashot.kod_tahalich%type,
                 p_kod_yeshut IN tb_log_bakashot.kod_yeshut%type,
              p_mispar_ishi IN tb_log_bakashot.mispar_ishi%type,
              p_taarich IN tb_log_bakashot.taarich%type,
              p_mispar_sidur IN tb_log_bakashot.mispar_sidur%type,
              p_shat_hatchala_sidur IN tb_log_bakashot.shat_hatchala_sidur%type,
              p_shat_yetzia IN tb_log_bakashot.shat_yetzia%type,
              p_mispar_knisa IN tb_log_bakashot.mispar_knisa%type,
              p_teur_hodaa IN tb_log_bakashot.teur_hodaa%type,
              p_kod_hodaa IN tb_log_bakashot.kod_hodaa%type,
              p_mispar_siduri OUT tb_log_bakashot.mispar_siduri%type);

PROCEDURE pro_ins_bakasha(p_sug_bakasha  IN tb_bakashot.sug_bakasha%type,
                   p_teur  IN tb_bakashot.teur%type,
                 p_status   IN tb_bakashot.status%type,
                 p_user_id IN tb_bakashot.mishtamesh_id%type,
              p_bakasha_id OUT tb_bakashot.bakasha_id%type);

  PROCEDURE pro_upd_bakasha(p_bakasha_id IN tb_bakashot.bakasha_id%type,
                   p_status   IN tb_bakashot.status%type,
              p_huavra_lesachar in  tb_bakashot.huavra_lesachar%type,
              p_zman_siyum   in  tb_bakashot.zman_siyum%type,
              p_tar_haavara_lesachar   in  tb_bakashot.taarich_haavara_lesachar%type) ;

 PROCEDURE  pro_upd_bakasha_all_fields(p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                                              p_zman_siyum   IN  TB_BAKASHOT.zman_siyum%TYPE,
                                                              p_status   IN TB_BAKASHOT.status%TYPE,
                                                              p_huavra_lesachar IN  TB_BAKASHOT.huavra_lesachar%TYPE,
                                                              p_tar_haavara_lesachar   IN  TB_BAKASHOT.taarich_haavara_lesachar%TYPE,
                                                              p_ishur_hilan   IN TB_BAKASHOT.ISHUR_HILAN%TYPE );
 PROCEDURE pro_ins_bakasha_param(p_bakasha_id  IN tb_bakashot_params.bakasha_id%type,
                   p_param_id  IN  tb_bakashot_params.param_id%type,
                 p_erech   IN  tb_bakashot_params.erech%type);

 PROCEDURE pro_get_pirtey_ritzot(p_taarich_me in tb_bakashot.zman_hatchala%type,
             p_taarich_ad in tb_bakashot.zman_hatchala%type,
            p_get_all number,
            p_cur out CurType);
FUNCTION fun_get_status_sachar(p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE) return number;
FUNCTION fun_get_status_bakasha(p_sug_bakasha number,p_Param_id number,p_erech varchar) return number;
function fun_get_rizot_zehot_lesachar(p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE) return varchar;
  PROCEDURE pro_get_ovdim_lechishuv(p_chodesh in date,
             p_maamad in number,
            p_ritza_gorefet in number,
            p_cur out CurType) ;

  PROCEDURE pro_get_ovdim_to_transfer(p_request_id in  tb_bakashot.bakasha_id%type,
                 p_cur_list out CurType,
            p_cur out CurType);

   PROCEDURE pro_get_chishuv_yomi(p_request_id in  tb_bakashot.bakasha_id%type,
                   p_mispar_ishi IN  tb_chishuv_yomi_ovdim.MISPAR_ISHI%TYPE,
                    p_taarich IN TB_CHISHUV_YOMI_OVDIM.TAARICH%TYPE,
              p_cur out CurType);

    ----------------
PROCEDURE pro_del_chishuv_after_transfer(p_request_id in  tb_bakashot.bakasha_id%type);
----------------
PROCEDURE pro_upd_status_yamey_avoda(p_request_id in  tb_bakashot.bakasha_id%type); 
PROCEDURE pro_ins_yamey_avoda_ovdim ;
     PROCEDURE pro_ins_yamey_avoda_ovdim(pDt varchar);
        PROCEDURE pro_get_premiot_ovdim(pYM varchar,p_Cur OUT CurType);
     PROCEDURE pro_upd_yamey_avoda_ovdim(pDt varchar);
 --         PROCEDURE pro_upd_yamey_avoda_1oved(pDt varchar,pIshi varchar);
      PROCEDURE pro_get_my_attendance(pIshi varchar,pDt varchar,p_Cur OUT CurType);
      PROCEDURE pro_pivot(p_ishi varchar,p_Dt_from varchar,p_Dt_to varchar,p_Cur OUT CurType);
           PROCEDURE pro_pivot_1Day(p_ishi varchar,p_Dt varchar,p_Cur OUT CurType);
     PROCEDURE pro_ins_log_tahalich(p_KodTahalich  number  ,p_KodPeilut number ,  p_KodStatus  number ,  p_TeurTech varchar  ) ;
      PROCEDURE pro_ins_log_tahalich_takala(p_KodTahalich  number  ,p_KodPeilut number,  p_KodStatus  number ,  p_TeurTech varchar  ,p_KodTakala number) ;
	  PROCEDURE pro_GetListDs(pDt varchar, pIshi varchar ,psidur varchar,p_cur out CurType) ;
         PROCEDURE pro_GetRowDt(pDt varchar, p_cur out CurType) ;
      PROCEDURE pro_GetRowDtLong(pDt varchar, p_cur out CurType);
        PROCEDURE pro_GetRowDtVeryLong(pDt varchar, p_cur out CurType) ;
        PROCEDURE pro_GetRowDtVeryLong2(pDt varchar, phatchala varchar,pIshi varchar ,psidur varchar,p_cur out CurType)  ;
        PROCEDURE pro_GetRowDtVeryLongPundakim2(pDt varchar, phatchala varchar,pIshi varchar ,p_cur out CurType)  ; 
	   PROCEDURE pro_RefreshMv(shem_mvew varchar)  ;
         PROCEDURE InsIntoTrailKnisa(pDt varchar, pDt_N_KNISA varchar,SRV_D_ISHI number,calc_D_new_sidur number,P24 number)  ;
     PROCEDURE InsIntoTrailYetzia(pDt varchar, pDt_N_YETZIA varchar,SRV_D_ISHI number,calc_D_new_sidur number,P24 number)  ;
    PROCEDURE pro_get_yamei_avoda_meshek( p_date DATE, p_bakasha_id number, p_Cur OUT CurType);
      PROCEDURE pro_get_all_yamei_avoda(  p_Cur OUT CurType);
   PROCEDURE pro_new_rec(  SRV_D_ISHI varchar,  SRV_D_TAARICH varchar,  calc_D_new_sidur varchar,
      SRV_D_KNISA_X varchar,  SRV_D_MIKUM_KNISA varchar,  SRV_D_SIBAT_DIVUACH_KNISA varchar,
      SRV_D_YETZIA_X varchar,  SRV_D_MIKUM_YETZIA varchar,  SRV_D_SIBAT_DIVUACH_YETZIA varchar,
      SRV_D_ISHI_MEADKEN varchar,
   SRV_D_KOD_BITUL_ZMAN_NESIA_X varchar,
      SRV_D_KOD_CHARIGA_X varchar,  SRV_D_KOD_HALBASHA_X varchar,  SRV_D_KOD_HAZMANA_X varchar,
      TAARICH_knisa_p24 number,  TAARICH_yetzia_p24 number,  DatEfes varchar,
      TAARICH_knisa_letashlum_p24 number,  SRV_D_KNISA_letashlum_X varchar,
      TAARICH_yetzia_letashlum_p24 number,  SRV_D_YETZIA_letashlum_X varchar) ;
   PROCEDURE pro_get_yamei_avoda_shinui_hr(p_date DATE, p_bakasha_id number, p_Cur OUT CurType);
       PROCEDURE pro_measher_o_mistayeg(  SRV_D_ISHI varchar,  SRV_D_TAARICH varchar,  TAARICH_knisa_p24 number );
          PROCEDURE pro_lo_letashlum(  SRV_D_ISHI varchar,  SRV_D_TAARICH varchar,  TAARICH_knisa_p24 number ) ;
       procedure pro_upd_out_blank(  SRV_D_ISHI varchar,  SRV_D_TAARICH varchar,  calc_D_new_sidur varchar,
      SRV_D_KNISA_X varchar,  SRV_D_MIKUM_KNISA varchar,  SRV_D_SIBAT_DIVUACH_KNISA varchar,
      SRV_D_YETZIA_X varchar,  SRV_D_MIKUM_YETZIA varchar,  SRV_D_SIBAT_DIVUACH_YETZIA varchar,
      SRV_D_ISHI_MEADKEN varchar,
   SRV_D_KOD_BITUL_ZMAN_NESIA_X varchar,
      SRV_D_KOD_CHARIGA_X varchar,  SRV_D_KOD_HALBASHA_X varchar,  SRV_D_KOD_HAZMANA_X varchar,
      TAARICH_knisa_p24 number , TAARICH_yetzia_p24 number,  DatEfes varchar,
      TAARICH_knisa_letashlum_p24 number,  SRV_D_KNISA_letashlum_X varchar,
      TAARICH_yetzia_letashlum_p24 number,  SRV_D_YETZIA_letashlum_X varchar) ;
   procedure pro_upd_in_blank(  SRV_D_ISHI varchar,  SRV_D_TAARICH varchar,  calc_D_new_sidur varchar,
      SRV_D_KNISA_X varchar,  SRV_D_MIKUM_KNISA varchar,  SRV_D_SIBAT_DIVUACH_KNISA varchar,
      SRV_D_YETZIA_X varchar,  SRV_D_MIKUM_YETZIA varchar,  SRV_D_SIBAT_DIVUACH_YETZIA varchar,
      SRV_D_ISHI_MEADKEN varchar,
   SRV_D_KOD_BITUL_ZMAN_NESIA_X varchar,
      SRV_D_KOD_CHARIGA_X varchar,  SRV_D_KOD_HALBASHA_X varchar,  SRV_D_KOD_HAZMANA_X varchar,
      TAARICH_knisa_p24 number , TAARICH_yetzia_p24 number,  DatEfes varchar,
      TAARICH_knisa_letashlum_p24 number,  SRV_D_KNISA_letashlum_X varchar,
      TAARICH_yetzia_letashlum_p24 number,  SRV_D_YETZIA_letashlum_X varchar) ;
     procedure pro_upd_in_out_letashlum(  SRV_D_ISHI varchar,  SRV_D_TAARICH varchar,  calc_D_new_sidur varchar,
      SRV_D_KNISA_X varchar,
   --SRV_D_MIKUM_KNISA varchar,  SRV_D_SIBAT_DIVUACH_KNISA varchar,
      SRV_D_YETZIA_X varchar,
   --SRV_D_MIKUM_YETZIA varchar,  SRV_D_SIBAT_DIVUACH_YETZIA varchar,
      SRV_D_ISHI_MEADKEN varchar,
   SRV_D_KOD_BITUL_ZMAN_NESIA_X varchar,
      --SRV_D_KOD_CHARIGA_X varchar,
   SRV_D_KOD_HALBASHA_X varchar,
   --SRV_D_KOD_HAZMANA_X varchar,
      TAARICH_knisa_p24 number , TAARICH_yetzia_p24 number,  DatEfes varchar,
      TAARICH_knisa_letashlum_p24 number,  SRV_D_KNISA_letashlum_X varchar,
      TAARICH_yetzia_letashlum_p24 number,  SRV_D_YETZIA_letashlum_X varchar) ;
    procedure  pro_ins_yamey_avoda_1oved(SRV_D_ISHI number,  SRV_D_TAARICH varchar) ;
  PROCEDURE pro_new_rec_pundakim(  SRV_D_ISHI varchar,  SRV_D_TAARICH varchar,
                                     SRV_D_KNISA_X varchar,  SRV_D_MIKUM_KNISA varchar,   TAARICH_knisa_p24 number) ;
  PROCEDURE pro_GetListDsPundakim(pDt varchar, pIshi varchar ,p_cur out CurType) ;
 	 PROCEDURE pro_insert_debug_maatefet(p_mispar_ishi NUMBER, p_taarich date, p_taarich_ritza date,
                                    p_bakasha_id number, p_sug_bakasha number, 
                                    p_comments test_maatefet.comments%type default null);
	 procedure pro_IfSdrnManas(pDt varchar,pIshi varchar ,p_cur out CurType) ;
procedure pro_get_shinuy_matsav_ovdim(p_Cur out CurType);
procedure pro_get_Shinuy_meafyeney_bizua(p_Cur out CurType) ;
procedure pro_get_shinuy_pirey_oved( p_Cur out CurType);
procedure pro_get_shinuy_brerot_mechdal( p_Cur out CurType);
procedure pro_ins_ovdim_im_shinuy_hr(p_coll_obj_ovdim_im_shinuy_hr in coll_ovdim_im_shinuy_hr);
procedure pro_ins_defaults_hr(p_coll_obj_brerot_mechdal_hr in coll_brerot_mechdal_meafyenim);

procedure inset_oved_im_shinuy_hr(p_mispar_ishi in number,
		  											   	   	   			  		 p_taarich in Date,p_tavla in varchar)	;
procedure update_oved_im_shinuy_hr(p_mispar_ishi in number,
		  											   	   	   			  		 p_taarich in Date,
																				 p_tavla in varchar );

PROCEDURE MoveNewMatzavOvdimToOld ;
PROCEDURE MoveNewPirteyOvedToOld ;
PROCEDURE MoveNewMeafyenimOvdimToOld ;
PROCEDURE MoveNewBrerotMechdalToOld ;																			 
PROCEDURE pro_get_ovdim4rerunsdrn( pDt varchar,p_cur OUT CurType) ;
 PROCEDURE pro_sof_meafyenim( pDt varchar,p_Cur OUT CurType);
procedure pro_get_premia_input(p_taarich date, p_bakasha_id tb_bakashot.bakasha_id%type,p_Cur OUT CurType);
procedure pro_update_calc_premia(p_taarich date, p_bakasha_id tb_bakashot.bakasha_id%type, p_mispar_ishi number,
            p_kod_rechiv number, p_erech_rechiv number);
procedure pro_get_premia_bakashot(p_taarich date,p_Cur OUT CurType);
 PROCEDURE pro_if_start(p_Cur OUT CurType);
  PROCEDURE pro_if_GalreadyRun(p_Cur OUT CurType) ;
procedure pro_get_ovdim_lehishuv_premiot(p_Cur OUT CurType);
/*procedure pro_update_chishuv_premia(p_bakasha_id tb_bakashot.bakasha_id%type,
            p_mispar_ishi OVDIM_LECHISHUV_PREMYOT.MISPAR_ISHI%type,
            p_chodesh ovdim_lechishuv_premyot.chodesh%type);*/
PROCEDURE pro_update_chishuv_premia(p_bakasha_id TB_BAKASHOT.bakasha_id%TYPE);
 procedure pro_ins_ovdim_lehishuv_premiot ;
FUNCTION fun_get_num_changes_to_shguim RETURN number;
FUNCTION pro_ins_log_tahalich_rec(p_KodTahalich  number  ,p_KodPeilut number,  
		  										  		  			   			p_KodStatus  number ,  p_TeurTech varchar ,p_KodTakala number ) RETURN number ;
	 procedure pro_upd_log_tahalich_rec(p_seqTahalich number ,p_KodStatus number,  p_TeurTech varchar ,p_KodTakala number ) ;	
 procedure pro_delete_log_tahalich_rcds;		
 procedure  pro_upd_yamimOfSdrn   ;				
  PROCEDURE pro_get_meafyenim_gap(p_num_process number,   p_cur OUT CurType);
   PROCEDURE pro_get_meafyenim_manygaps(p_num_process number,   p_cur OUT CurType) ;

	 procedure Pro_get_pirtey_ovdim_leRikuzim(p_bakasha_id number, p_cur OUT CurType);
       procedure Pro_get_Email_Ovdim_LeRikuzim(p_bakasha_id number, p_cur OUT CurType);
       
   FUNCTION  pro_get_sug_chishuv(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                                    p_taarich IN DATE,
                                                    p_bakasha_id OUT TB_BAKASHOT.bakasha_id%TYPE ) RETURN NUMBER;  
                                                    
PROCEDURE pro_ins_misparishi_sug_chishuv(p_bakasha_id NUMBER,p_coll_chishuv_sug_sidur IN COLL_MISPAR_ISHI_SUG_CHISHUV);		
PROCEDURE Prepare_yamei_avoda_meshek(p_date IN DATE, p_type in number,p_num_process IN NUMBER, p_bakasha_id tb_bakashot.bakasha_id%type) ;		
PROCEDURE Prepare_yamei_avoda_shinui_hr(p_type in number,p_num_process IN NUMBER, p_bakasha_id tb_bakashot.bakasha_id%type);
PROCEDURE pro_divide_packets( p_num_process IN  NUMBER,p_bakasha_id tb_bakashot.bakasha_id%type );	
PROCEDURE pro_get_netunim_for_process( p_num_process IN  NUMBER ,  p_bakasha_id tb_bakashot.bakasha_id%type, p_cur OUT CurType);	
PROCEDURE pro_delete_tb_shguim_batch(	p_num_process IN  NUMBER ,  p_bakasha_id tb_bakashot.bakasha_id%type);		
PROCEDURE Prepare_premiot_shguim_batch(p_type in number,p_num_process IN NUMBER, p_bakasha_id tb_bakashot.bakasha_id%type);
PROCEDURE Pro_Save_Rikuz_Pdf( p_BakashatId TB_RIKUZ_PDF.bakasha_id%type,p_coll_rikuz_pdf IN COLL_RIKUZ_PDF);
PROCEDURE Pro_Get_Rikuz_Pdf(p_mispar_ishi IN NUMBER,p_taarich IN DATE,p_BakashatId IN NUMBER, p_cur OUT CurType); --p_rikuz OUT BLOB);
PROCEDURE Pro_ins_baam;
FUNCTION pro_check_view_empty(p_TableName VARCHAR2) RETURN NUMBER;
PROCEDURE  chk_creation_date_meafyenim(shem_mvew VARCHAR) ;
PROCEDURE DeleteBakashotYeziratRikuzim(p_BakashatId tb_bakashot.bakasha_id%type);
    PROCEDURE pro_Get_Makatim_LeTkinut(p_date IN DATE, p_cur OUT CurType);    
END PKG_BATCH;
/


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
                                            P_MisparIshi  IN VARCHAR2,
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
PROCEDURE Pro_get_num_rechivim(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                                                       p_taarich IN DATE,
                                                                                             p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                                                                         p_cur OUT CurType);        
                                                                                         
                                         

  PROCEDURE pro_get_rikuz_chodshi_temp(    p_Cur_Rechivim_Yomi OUT CurType ,
                                                               p_Cur_Rechivim_Chodshi OUT CurType,
                                                                p_Cur_Rechivey_Headrut OUT CurType,
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
PROCEDURE Pro_get_num_rechivim_tmp(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                                             p_taarich IN DATE,
                                                                                             p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                                                                         p_cur OUT CurType);                                                                                                                                            
END PKG_RIKUZ_AVODA;
/


CREATE OR REPLACE PACKAGE          Pkg_Sdrn AS
TYPE    CurType      IS    REF  CURSOR;
/******************************************************************************
   NAME:       PKG_sdrn
   PURPOSE:

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        09/03/2010             1. Created this package.
******************************************************************************/

     PROCEDURE pro_ins_yamim_4_sidurim(pDt VARCHAR) ;
     PROCEDURE pro_ins_sidurim_4_sidurim(pDt VARCHAR) ;
     PROCEDURE pro_ins_peilut_4_sidurim(pDt VARCHAR) ;
         PROCEDURE pro_get_sdrm_control(pDt VARCHAR,p_Cur OUT CurType) ;
      PROCEDURE pro_upd_sdrm_control(pDt VARCHAR);
    PROCEDURE pro_GetStatusSdrn(pDt VARCHAR, p_cur OUT CurType);
	 PROCEDURE pro_GetStatus2Sdrn(pAr VARCHAR, p_cur OUT CurType) ;
	  PROCEDURE pro_GetDtReRunSdrn(pDt VARCHAR, p_cur OUT CurType) ;
	  PROCEDURE pro_upd_sdrnRerun_control(pDt VARCHAR) ;
 	  PROCEDURE pro_TrailNDel_peilut_4retrSdrn(pDt VARCHAR);
 	  PROCEDURE pro_TrailNDel_sidurim_4reSdrn(pDt VARCHAR);
	   	PROCEDURE pro_ins_sidurim_retroSdrn(pDt VARCHAR) ;
		PROCEDURE pro_ins_peilut_retroSdrn(pDt VARCHAR) ;
		PROCEDURE pro_ins_YamimOfSdrn;
		PROCEDURE pro_ins_SidurimOfSdrn ;
		PROCEDURE pro_ins_PeilutOfSdrn ;
		PROCEDURE pro_upd_CtrlOfSdrn ;
		PROCEDURE pro_chk_Dt4_rerun ;
		PROCEDURE pro_upd_sdrntstRerun_control(pDt VARCHAR) ;
		--PROCEDURE pro_stam;
		PROCEDURE pro_stam2;
		PROCEDURE pro_stam3;
        PROCEDURE pro_stam4(pDt VARCHAR) ;
	    PROCEDURE pro_retro1(pDt VARCHAR) ;
  		--PROCEDURE pro_ins_yamim_from_sidurim(pDt VARCHAR);
		PROCEDURE pro_ins_sidurim_from_sdrm(pDt VARCHAR) ; 
		PROCEDURE pro_ins_peilut_from_sdrm(pDt VARCHAR);
		PROCEDURE pro_change_Knisot_sdrm(pDt VARCHAR,p_Cur OUT CurType);
		PROCEDURE pro_Ins_Knisot_sdrm(pDt VARCHAR,p_makat_nesia NUMBER,pMisparKnisa NUMBER,PSugKnisa NUMBER, PTeurNesia VARCHAR);
        PROCEDURE pro_get_Knisot_sdrm(pDt DATE,p_Cur OUT CurType);
        PROCEDURE pro_insert_knisot(p_coll_obj_peilut_ovdim IN coll_obj_peilut_ovdim);
END Pkg_Sdrn;
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
																						
	PROCEDURE pro_insert_barkod_Tachograf(p_mispar_ishi IN  INTEGER,p_taarich IN TB_TACHOGRAF_LE_KARTIS.TAARICH%TYPE,p_Barkod IN  NUMBER	);		
    PROCEDURE fun_get_barkod_Tachograf(p_mispar_ishi IN  INTEGER,p_taarich IN TB_TACHOGRAF_LE_KARTIS.TAARICH%TYPE, p_cur OUT CurType  ) ;
	PROCEDURE pro_get_tavlaot_to_refresh(p_cur OUT CurType);	
	PROCEDURE pro_get_snif_tnua_by_kod(p_kod_snif IN NUMBER,p_cur OUT CurType);		
    PROCEDURE pro_insert_meadken_acharon(p_mispar_ishi IN NUMBER,p_taarich DATE);	
        PROCEDURE pro_get_ovdim_by_bakasha(p_bakasha_id IN NUMBER,p_cur OUT CurType);										
END Pkg_Utils;
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
   1.0        27/04/2009      sari       1. ����� ����� ����� �����
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
   1.0        27/04/2009      sari       1. �����   ����� ����� ����� �����
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
   1.0        02/07/2009     sari      1. ������� �������  ����� ����� ��� �������
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
             (SELECT  DECODE(bp.erech,NULL,'',1,'�����',2,'������','����� �������') FROM TB_BAKASHOT_PARAMS bp WHERE bp.bakasha_id=b.bakasha_id AND  bp.param_id=1)   auchlusia,
               (SELECT  bp.erech FROM TB_BAKASHOT_PARAMS bp WHERE bp.bakasha_id=b.bakasha_id AND bp.param_id=2) tkufa,
               TO_DATE('01/' ||  (SELECT  bp.erech FROM TB_BAKASHOT_PARAMS bp WHERE bp.bakasha_id=b.bakasha_id AND bp.param_id=2) ,'dd/mm/yyyy') tkufa_date,
          (SELECT  DECODE(bp.erech,1,'��','��') FROM TB_BAKASHOT_PARAMS bp WHERE bp.bakasha_id=b.bakasha_id AND bp.param_id=3) ritza_gorfet,
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
                (SELECT  DECODE(bp.erech,NULL,'',1,'�����',2,'������','����� �������') FROM TB_BAKASHOT_PARAMS bp WHERE bp.bakasha_id=b.bakasha_id AND  bp.param_id=1)   auchlusia,
             (SELECT  bp.erech FROM TB_BAKASHOT_PARAMS bp WHERE bp.bakasha_id=b.bakasha_id AND bp.param_id=2) tkufa,
              TO_DATE('01/' ||  (SELECT  bp.erech FROM TB_BAKASHOT_PARAMS bp WHERE bp.bakasha_id=b.bakasha_id AND bp.param_id=2),'dd/mm/yyyy' ) tkufa_date,
             (SELECT  DECODE(bp.erech,1,'��','��') FROM TB_BAKASHOT_PARAMS bp WHERE bp.bakasha_id=b.bakasha_id AND bp.param_id=3) ritza_gorfet,
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
            p_cur OUT CurType) IS
 BEGIN
     OPEN p_cur_list FOR
			     SELECT c.taarich,c.mispar_ishi ,DECODE(SUBSTR(p.maamad,0,1),1,'0013', DECODE(SUBSTR(p.maamad,2,2),'23','2626',  '0026')) mifal,
                 SUBSTR(p.maamad,2,2) maamad,p.gil,SUBSTR(p.maamad,0,1) maamad_rashi, c.chodesh_ibud,
						o.SIFRAT_BIKORET_MI sifrat_bikoret,o.SHEM_MISH,o.SHEM_PRAT,p.dirug,p.darga,o.TEUDAT_ZEHUT,p.Isuk,
                      Pkg_Ovdim.fun_get_meafyen_oved(c.mispar_ishi,53,c.taarich) meafyen53,
                      Pkg_Ovdim.fun_get_meafyen_oved(c.mispar_ishi,83,c.taarich) meafyen83,
			                DECODE((SELECT  1 FROM MATZAV_OVDIM m
									            WHERE m.mispar_ishi=c.mispar_ishi
									           AND c.TAARICH BETWEEN m.taarich_hatchala AND m.taarich_siyum
									          AND  m.Kod_matzav='33'),NULL,0,1) mushhe
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
              -- and c.mispar_ishi =28466
			   AND o.mispar_ishi=c.mispar_ishi
			    AND p.ME_TARICH=(SELECT MAX(po.me_tarich)
													          FROM   PIVOT_PIRTEY_OVDIM po
													          WHERE (c.taarich BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
													          OR   (ADD_MONTHS(c.taarich,1)-1)  BETWEEN  po.ME_TARICH  AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))
													          OR   po.ME_TARICH>=c.taarich AND   NVL(po.ad_TARICH,TO_DATE('01/01/9999' ,'dd/mm/yyyy'))<=    (ADD_MONTHS(c.taarich,1)-1))
													  AND po.ISUK IS NOT NULL
													   AND po.mispar_ishi=c.mispar_ishi)
			   ORDER BY p.mispar_ishi asc,c.taarich desc;

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


 BEGIN
-- 2010/06/29 measher_o_mistayeg
   UPDATE  TB_YAMEY_AVODA_OVDIM yao
 SET   measher_o_mistayeg =1
WHERE yao.taarich =  TO_DATE(pDt,'yyyymmdd')
AND measher_o_mistayeg IS NULL
AND NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM so
           WHERE so.meadken_acharon=-12
        AND so.mispar_ishi=yao.mispar_ishi
        AND  so.taarich=yao.taarich
      AND so.taarich =  TO_DATE(pDt,'yyyymmdd')
        AND so.mispar_sidur<>99200);
       EXCEPTION
   WHEN OTHERS THEN
   INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,58,SYSDATE,'',10,'',SUBSTR('update_yamim '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
	END;
COMMIT; 

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
                            AND a.kod_meafyen in(3,4,5,6,7,8,23,24,25,26,27,28,41,42,44,51,56,61,63,64)
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
                            AND a.kod_meafyen in(3,4,5,6,7,8,23,24,25,26,27,28,41,42,44,51,56,61,63,64);
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
                            and a.kod_natun in(6,7,8,10,11,13,20,21)
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
                             and a.kod_natun in(6,7,8,10,11,13,20,21);
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
        p_tar DATE;
BEGIN
        p_tar:= TO_DATE('01/' || TO_CHAR(p_taarich,'mm/yyyy'));
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
        AND cs.taarich BETWEEN p_tar AND LAST_DAY(p_tar) -- to_char(cs.taarich,'mm/yyyy')=to_char(p_taarich,'mm/yyyy')
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
  PROCEDURE pro_ins_ovdim_lehishuv_premiot   IS
  -- todo: chainge this procedure to look at view in wr1 & baam, and to ommit the parameters
  --also split this to 4  parts so that if one part 's dblink is not available the rest will still be ok
 BEGIN
 EXECUTE IMMEDIATE  'truncate table   OVDIM_LECHISHUV_PREMYOT';
  INSERT INTO  OVDIM_LECHISHUV_PREMYOT( mispar_ishi,chodesh)
--SELECT DISTINCT oved,tkufa FROM (SELECT DISTINCT wr_premia_empl  oved,TO_DATE(wr_premia_year_month*100+01,'yyyymmdd') tkufa
--FROM  wr_premia_4_kds@KDS2wr1
-- WHERE wr_premia_year_month  BETWEEN pYMf AND pYM2
-- UNION ALL
--    SELECT DISTINCT baa_premia_empl  ,TO_DATE(baa_premia_year_month*100+01,'yyyymmdd') 
--FROM  baam_premia_4_kds@KDS2baam
-- WHERE baa_premia_year_month BETWEEN  pYMf AND pYM2
-- UNION ALL 
-- SELECT DISTINCT mispar_ishi,TO_DATE(TO_CHAR(taarich,'yyyymm')*100+01,'yyyymmdd') chodesh
-- FROM TB_SIDURIM_OVDIM
--WHERE taarich BETWEEN TO_DATE(pYMf*100+01,'yyyymmdd') AND  LAST_DAY(TO_DATE(pYM2*100+01,'yyyymmdd'))
--AND mispar_sidur BETWEEN 99220 AND 99222);
SELECT DISTINCT mispar_ishi,chodesh FROM (SELECT *
 FROM  wr1.wr_OVDIM_LECHISHUV_PREMIA@KDS2wr1
 UNION ALL
 SELECT *
FROM  baam.baam_OVDIM_LECHISHUV_PREMIA@KDS2baam
UNION ALL
 SELECT DISTINCT mispar_ishi,TO_DATE(TO_CHAR(taarich,'yyyymm')*100+01,'yyyymmdd')
 FROM TB_SIDURIM_OVDIM
WHERE mispar_sidur BETWEEN 99220 AND 99222
AND taarich BETWEEN (SELECT  TO_DATE(MIN(baa_run_year_month)*100+1,'yyyymmdd')  
   FROM  baam.tb_baa_run_param@KDS2baam
  WHERE baa_run_status='�')
AND   (  SELECT  TO_DATE(MAX(baa_run_year_month)*100+1,'yyyymmdd') 
   FROM  baam.tb_baa_run_param@KDS2baam
  WHERE baa_run_status='�'  )
  -- test 4 premiot nihul pkidim
       UNION ALL
  SELECT 12666,TO_DATE('01/04/2012','dd/mm/yyyy') 
  FROM dual
  UNION ALL
  SELECT 75242,TO_DATE('01/04/2012','dd/mm/yyyy') 
  FROM dual  
  -- test 4 premiot 201201
    UNION ALL
  SELECT 48441,TO_DATE('01/01/2012','dd/mm/yyyy') 
  FROM dual
  UNION ALL
  SELECT 71717,TO_DATE('01/01/2012','dd/mm/yyyy') 
  FROM dual  
    -- end test 4 premiot 201201
  );
      EXCEPTION
   WHEN OTHERS THEN
        RAISE;
 END pro_ins_ovdim_lehishuv_premiot; 
 
 
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
    RAISE_APPLICATION_ERROR(-20005, SUBSTR(err_str,1,100), TRUE);
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
                 B.ZMAN_HATCHALA,c.SUG_CHISHUV   
        FROM TB_CHISHUV_CHODESH_OVDIM s ,TB_BAKASHOT b ,
                BREROT_MECHDAL_MEAFYENIM r,TB_MISPAR_ISHI_SUG_CHISHUV c,
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
                     AND EXISTS(SELECT 1 FROM TB_SIDURIM_OVDIM so WHERE so.mispar_ishi=ya.mispar_ishi
                                                                                                               AND so.taarich=ya.taarich AND so.meadken_acharon<>-12)
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


PROCEDURE Pro_ins_baam IS

BEGIN

	--when active:  Wr1.pkg_Calc.DelAttend@KDS2wr1;
	 Wr1.Pkg_Calc.DelPersonnel@KDS2wr1;
	--todo: Wr1.pkg_Calc.InsPersonnel@KDS2wr1;
	--todo: Wr1.pkg_Calc.InsAttend@KDS2wr1;

    baam.Tmp.DelPersonnel@KDS2baam;

	
INSERT INTO TB_LOG_TAHALICH
	VALUES (13,5,1,SYSDATE,'','','','Pro_ins_baam');

      EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END  Pro_ins_baam;

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
        select distinct P.MAKAT_NESIA,P.TAARICH
        from tb_peilut_ovdim p
        where  P.TAARICH between p_date and last_day(p_date) 
      --  P.TAARICH =  to_date('01/04/2011','dd/mm/yyyy') 
        -- and  substr(P.MAKAT_NESIA,1,1)='1' 
        and P.MAKAT_NESIA>0
        and not((substr(P.MAKAT_NESIA,1,1)='7' or substr(P.MAKAT_NESIA,1,1)='5' ) and length(P.MAKAT_NESIA)=8);
        
        
        -- P.TAARICH between p_date and last_day(p_date);
        
END pro_Get_Makatim_LeTkinut;
END Pkg_Batch;
/


CREATE OR REPLACE PACKAGE BODY          Pkg_Errors AS

PROCEDURE pro_get_oved_sidurim_peilut(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                               p_date IN DATE, p_cur OUT CurType)
IS
BEGIN
   DBMS_APPLICATION_INFO.SET_MODULE('pkg_errors.pro_get_oved_sidurim_peilut','get sidurim and peilut leoved leyom');
	
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
           v_element.peula_o_yedia_bilvad element_for_yedia,
           v_element.hova_mispar_rechev bus_number_must,
           v_element.divuach_besidur_visa divuch_in_sidur_visa,
           v_element.kod_lechishuv_premia,  v_element.lo_nizbar_leshat_gmar,
           v_element.sector_zvira_zman_haelement element_zvira_zman,
           v_element.erech_element element_in_minutes ,
           v_element.mispar_sidur_matalot_tnua,v_element.divuach_besidur_meyuchad,
           v_element.bitul_biglal_ichur_lasidur,v_element.nesia_reika,
		   v_element.lehitalem_hafifa_bein_nesiot, v_element.Hamtana element_hamtana,
		   v_element.Lershut ,v_sidurm.hovat_hityazvut  ,
		   v_element.erech_element,v_element.peilut_mashmautit,
           v_element.lehitalem_beitur_reyka,           
		   DECODE(o.bitul_o_hosafa,1,1,3,1,0) sidur_mevutal ,
		   DECODE(po.bitul_o_hosafa,1,1,3,1,0) peilut_mevutelet,
           sm.teur_sidur_meychad,sl.LEBDIKAT_SHGUIM,
           nvl(O.SUG_SIDUR,0) SUG_SIDUR
    FROM TB_SIDURIM_OVDIM o,TB_PEILUT_OVDIM po,
         TB_YAMIM_MEYUCHADIM ym,CTB_SUGEY_YAMIM_MEYUCHADIM sug_yom,
         pivot_sidurim_meyuchadim v_sidurm,
         pivot_MEAFYENEY_ELEMENTIM v_element,
         CTB_SIDURIM_MEYUCHADIM  sm,
		 CTB_SIBOT_LOLETASHLUM sl
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
		  AND (sl.PAIL=1 OR sl.PAIL IS NULL) )
    ORDER BY  sidur_mevutal ,shat_hatchala,mispar_sidur  ,peilut_mevutelet,shat_yetzia,mispar_knisa ;

EXCEPTION
        WHEN OTHERS THEN
		RAISE;

END pro_get_oved_sidurim_peilut;

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
            NVL(status_card.teur_status_kartis,'����')  teur_status_kartis,
            DECODE(s.snif_tnua,NULL,v_pirty_oved.hatzava_lekav,s.snif_tnua) snif_tnua,
            BECHISHUV_SACHAR

     FROM TB_YAMEY_AVODA_OVDIM ymao,
          pivot_pirtey_ovdim v_pirty_oved,
          OVDIM ov,
          TB_YAMIM_MEYUCHADIM ym,
          CTB_SUGEY_YAMIM_MEYUCHADIM sug_yom,
          CTB_STATUS_KARTIS status_card,
          CTB_ISUK i,
		  CTB_SNIF_AV s
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
		   AND s.kod_snif_av(+)=v_pirty_oved.snif_av;

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
    SELECT kod_matzav
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
		   zakay_lepizul, lo_nidreshet_hityazvut,S.TEUR_SIDUR_AVODA
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
                           sug_sidur
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
                      p_coll_sidurim_ovdim(i).sug_sidur);
                      
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
                                   taarich_idkun_trail,sug_peula,heara,snif_tnua,teur_nesia,dakot_bafoal ,km_visa)
    SELECT mispar_ishi,taarich,mispar_sidur,
           shat_hatchala_sidur,shat_yetzia,mispar_knisa,
           makat_nesia,oto_no,mispar_siduri_oto,kisuy_tor,
           p_obj_peilut_ovdim.bitul_o_hosafa,kod_shinuy_premia,mispar_visa,imut_netzer,
           shat_bhirat_nesia_netzer,oto_no_netzer,mispar_sidur_netzer,
           shat_yetzia_netzer,makat_netzer,shilut_netzer,
           mikum_bhirat_nesia_netzer,mispar_matala,
           taarich_idkun_acharon,meadken_acharon,p_obj_peilut_ovdim.meadken_acharon,
		   SYSDATE, p_sug_peula,
           heara,snif_tnua,teur_nesia,dakot_bafoal ,km_visa

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
    --����� ��� �����
   BEGIN
        pro_upd_yamey_avoda_ovdim(p_coll_yamey_avoda_ovdim_upd);
   EXCEPTION
		 WHEN OTHERS THEN
              RAISE_APPLICATION_ERROR(-20001,'An error was encountered in tb_yamey_avoda_ovdim - '||SQLCODE||' -ERROR- '||SQLERRM);

   END;

--����� �������
    BEGIN
      pro_del_sidurim_ovdim(p_coll_sidurim_ovdim_del,2);
      -- pro_del_sidurim_ovdim(p_coll_sidurim_ovdim_del,p_coll_sidurim_ovdim_ins,2);
      EXCEPTION
		 WHEN OTHERS THEN
           RAISE_APPLICATION_ERROR(-20001,'An error was encountered in delete to tb_sidurim_ovdim - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;
 
 --����� ������� �����
   BEGIN
        pro_ins_sidurim_ovdim(p_coll_sidurim_ovdim_ins);
      EXCEPTION
		 WHEN OTHERS THEN
		       RAISE_APPLICATION_ERROR(-20001,'An error was encountered in insert to tb_sidurim_ovdim - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;

--����� ��������
    BEGIN

        pro_del_peilut_ovdim(p_coll_obj_peilut_ovdim_del);
     EXCEPTION
		 WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20001,'An error was encountered in delete to tb_peiluyot_ovdim - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;
	
	 --����� ��������
    BEGIN
        pro_upd_peilut_ovdim(p_coll_obj_peilut_ovdim_upd);
       EXCEPTION
		 WHEN OTHERS THEN
		      RAISE_APPLICATION_ERROR(-20001,'An error was encountered in update to tb_peiluyot_ovdim - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;
	
	 
    --����� �������
    BEGIN
        pro_upd_sidurim_ovdim(p_coll_sidurim_ovdim_upd);
      EXCEPTION
		 WHEN OTHERS THEN
            RAISE_APPLICATION_ERROR(-20001,'An error was encountered in update to tb_sidurim_ovdim - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;
   --����� �������
   BEGIN
        pro_del_sidurim_ovdim(p_coll_sidurim_ovdim_del,0);
      EXCEPTION
		 WHEN OTHERS THEN
           RAISE_APPLICATION_ERROR(-20001,'An error was encountered in delete to tb_sidurim_ovdim - '||SQLCODE||' -ERROR- '||SQLERRM);
    END;

--����� ��������
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
--Get maximun table/trail  last update
    OPEN p_cur FOR
    SELECT TRUNC(max_date) last_date, NVL(OVDIM.SHEM_MISH || ' ' ||  OVDIM.SHEM_PRAT ,'�����') full_name,MEADKEN_ACHARON
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
           o.heara, o.taarich_idkun_acharon, o.meadken_acharon,
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

    DELETE TB_SHGIOT T 
    WHERE T.MISPAR_ISHI =p_mispar_ishi
          and T.taarich =  p_date;

EXCEPTION
         WHEN OTHERS THEN
              RAISE;
END pro_Delete_Errors ;


END Pkg_Errors;
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
								   AND measher_o_mistayeg IS NOT NULL
								    AND measher_o_mistayeg <>2
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
           AND  ((p_kod_snif IS NULL )OR (v.SNIF_AV =p_kod_snif) OR (p_kod_snif=101 and S.TEUR_SNIF_AV like '%����%' ) 
                                                                                             OR  (p_kod_snif=100 and S.TEUR_SNIF_AV like '%���%'))
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
       --open p_cur for  select 75290 mispar_ishi ,'75290 ( ��� ��� �����)' FullName from dual ;
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
                TO_CHAR(aa.Taarich,'dd/mm/yyyy') || ' ' || (CASE TO_CHAR(aa.taarich,'d') WHEN '1' THEN '�'
                                             WHEN '2' THEN '�'
                                             WHEN '3' THEN '�'
                                             WHEN '4' THEN '�'
                                             WHEN '5' THEN '�'
                                             WHEN '6' THEN '�'
                                             WHEN '7' THEN '�' END)  || ' ' || c.teur_yom teur_yom

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
                to_char(aa.Taarich,'dd/mm/yyyy') || ' ' || (case to_char(aa.taarich,'d') when '1' then '�'
                                             when '2' then '�'
                                             when '3' then '�'
                                             when '4' then '�'
                                             when '5' then '�'
                                             when '6' then '�'
                                             when '7' then '�' end)  || ' ' || c.teur_yom teur_yom

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
   1.0        12/05/2009     sari      1. ���� ���� ����
*/
 PROCEDURE pro_get_pirtey_oved(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,p_taarich IN DATE,
 		   									 							p_cur OUT CurType) IS
	v_date_from DATE;
 --   p_taarich date;
BEGIN
--p_taarich:= to_date('01/08/2010','dd/mm/yyyy');
	 v_date_from:=TO_DATE('01/' || TO_CHAR(p_taarich,'mm/yyyy'),'dd/mm/yyyy');
             
   -- INSERT INTO TB_LOG_TEST(ID,PARAM,VALUE) VALUES ( KDSADMIN.TB_LOG_TEST_SEQ.NEXTVAL , 'p_taarich:',p_taarich);
    OPEN p_cur FOR
	      SELECT o.mispar_ishi,o.shem_MISH,o.shem_prat, TO_CHAR(p_taarich,'mm/yyyy') month_year,o. Kod_Hevra,
          substr(p.maamad,2,2) kod_maamad,
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
   1.0        13/05/2009     sari      1. ������� ������� �� ��� �����  ��� ���� ���� ������ ����
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
   1.0        17/05/2009     sari      1. ������� ������� �� ��� ������ ��������� �� �����
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
   1.0        17/05/2009     sari      1. ������� ������� ������ �������
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
   1.0        18/05/2009     sari      1. ������� ������� ����� ����� �����
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
			         --����� ������� �� �� �������� �����
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
			   (SELECT 900 MIYUN_BESIKUM_CHODSHI, '����' TEUR_RECHIV,'�����' heara,0 erech_rechiv,
	   		    Y.TAARICH,TO_NUMBER(TO_CHAR(Y.KOD_RECHIV)) Kod_Rechiv,TO_NUMBER(TO_CHAR(y.Taarich,'DD'))  day_taarich,
	   			NULL kizuz_meal_michsa
	          FROM MATZAV_OVDIM p, TB_CHISHUV_YOMI_OVDIM y
				WHERE  p.Mispar_Ishi=p_mispar_ishi
			   AND y.taarich between v_tar_me and last_day(v_tar_me) --AND TO_CHAR(y.Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy')
					AND y.TAARICH BETWEEN p.taarich_hatchala AND p.taarich_siyum
				AND  p.Kod_matzav='33')
		UNION ALL
	   (SELECT 900 MIYUN_BESIKUM_CHODSHI, '����' TEUR_RECHIV,MAX(H.HEARA) heara,0 erech_rechiv,
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
			         --����� ������� �� �� �������� �����
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
			   (SELECT 900 MIYUN_BESIKUM_CHODSHI, '����' TEUR_RECHIV,'�����' heara,0 erech_rechiv,
	   		    Y.TAARICH,TO_NUMBER(TO_CHAR(Y.KOD_RECHIV)) Kod_Rechiv,TO_NUMBER(TO_CHAR(y.Taarich,'DD'))  day_taarich,
	   			NULL kizuz_meal_michsa
	          FROM MATZAV_OVDIM p, TB_TMP_CHISHUV_YOMI_OVDIM y
				WHERE  p.Mispar_Ishi=p_mispar_ishi
			        AND y.taarich between v_tar_me and last_day(v_tar_me)    -- TO_CHAR(y.Taarich,'mm/yyyy') =TO_CHAR(p_taarich,'mm/yyyy')
					AND y.TAARICH BETWEEN p.taarich_hatchala AND p.taarich_siyum
				AND  p.Kod_matzav='33')
		UNION ALL
	   (SELECT 900 MIYUN_BESIKUM_CHODSHI, '����' TEUR_RECHIV,MAX(H.HEARA) heara,0 erech_rechiv,
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
   1.0       25/05/2009     sari      1. ������� ������� ������� ����
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
    SELECT DISTINCT  TO_CHAR(y.taarich,'dd/mm/yyyy')  || ' ' || (CASE TO_CHAR(y.taarich,'d') WHEN '1' THEN '�'
                                             WHEN '2' THEN '�'
                                             WHEN '3' THEN '�'
                                             WHEN '4' THEN '�'
                                             WHEN '5' THEN '�'
                                             WHEN '6' THEN '�'
                                             WHEN '7' THEN '�' END)  || ' ' || c.teur_yom taarich,
		-- DECODE (y.SHGIOT_LETEZUGA_LAOVED,1,'����')     status,
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
            simun:='����';
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
    SELECT DISTINCT  TO_CHAR(y.taarich,'dd/mm/yyyy')  || ' ' || (CASE TO_CHAR(y.taarich,'d') WHEN '1' THEN '�'
                                             WHEN '2' THEN '�'
                                             WHEN '3' THEN '�'
                                             WHEN '4' THEN '�'
                                             WHEN '5' THEN '�'
                                             WHEN '6' THEN '�'
                                             WHEN '7' THEN '�' END)  || ' ' || c.teur_yom taarich,
        --   k.teur_status_kartis status,
          pkg_ovdim.fun_get_status_lekartis(y.SHGIOT_LETEZUGA_LAOVED,y.mispar_ishi,y.taarich) status,
       --   DECODE (y.SHGIOT_LETEZUGA_LAOVED,1,'����')    status,
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
		  AND ( (pkg_ovdim.fun_get_status_lekartis(y.SHGIOT_LETEZUGA_LAOVED,y.mispar_ishi,y.taarich) = '����')
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
   1.0       07/06/2009     sari      1. ������� ������� ������� ����
*/
PROCEDURE pro_get_meafyeney_oved(p_mispar_ishi IN MEAFYENIM_OVDIM.mispar_ishi%TYPE,
		  									   	        p_taarich IN MEAFYENIM_OVDIM.me_taarich%TYPE,
														p_cur OUT CurType) IS
	BEGIN
	DBMS_APPLICATION_INFO.SET_MODULE('pkg_ovdim.pro_get_meafyeney_oved','get meafyenim leoved ');

	 OPEN p_cur FOR
	   		  SELECT  DECODE(m.kod_meafyen,NULL,to_char(b.kod_meafyen),to_char(m.kod_meafyen)) kod_meafyen,DECODE(m.Erech_Mechdal_partany,NULL,'',m.Erech_Mechdal_partany ||   ' (�.�.) ') Erech_Mechdal_partany,c.teur_MEAFYEN_BITZUA,
			                 DECODE(b.erech,NULL,'',b.erech  ||   ' (�.�. �����) ') Erech_Brirat_Mechdal,y.TEUR_YECHIDA_MEAFYEN YECHIDA,
			   		 		 DECODE(m.erech_ishi,NULL,DECODE(m.ERECH_MECHDAL_PARTANY,NULL, b.ME_TAARICH,m.ME_TAARICH),m.ME_TAARICH) ME_TAARICH,
			  				 DECODE(m.erech_ishi,NULL,DECODE(m.ERECH_MECHDAL_PARTANY,NULL, b.ad_TAARICH,m.ad_TAARICH),m.ad_TAARICH) AD_TAARICH,
			    			  DECODE(m.erech_ishi,NULL,DECODE(m.ERECH_MECHDAL_PARTANY,NULL, b.erech ||  ' (�.�. �����) ' ,m.ERECH_MECHDAL_PARTANY || ' (�.�.) ' ),m.erech_ishi || ' (����) '  ) Erech_ishi,
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
                          DECODE(m.erech_ishi,NULL ,m.ERECH_MECHDAL_PARTANY || ' (�.�.) ',m.erech_ishi || ' (����) '  ) Erech_ishi,
                           DECODE(m.Erech_Mechdal_partany,NULL,'',m.Erech_Mechdal_partany ||   ' (�.�.) ') Erech_Mechdal_partany
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
                     m.erech ||  ' (�.�. �����) '  Erech_ishi,
                       to_char(m.erech) value_erech_ishi,
                         DECODE(m.erech,NULL,'',m.erech  ||   ' (�.�. �����) ') Erech_Brirat_Mechdal,
                            '1' source_meafyen
                    from  tbIshi s, brerot_mechdal_meafyenim m
                    where (s.AD_TAARICH+1)<next_hour_me
                    and s.kod_meafyen = m.kod_meafyen
                
                union 

                    select s.kod_meafyen, (s.prev_hour_ad+1) ME_TAARICH, (s.ME_TAARICH -1) AD_TAARICH,
                      s.TEUR_MEAFYEN_BITZUA, s.YECHIDA,
                    '' Erech_Mechdal_partany,
                     m.erech ||  ' (�.�. �����) '  Erech_ishi,
                      to_char(m.erech) value_erech_ishi,
                        DECODE(m.erech,NULL,'',m.erech  ||   ' (�.�. �����) ') Erech_Brirat_Mechdal,
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
                    DECODE(df.erech,NULL,'',df.erech  ||   ' (�.�. �����) ') Erech_Brirat_Mechdal,  
                     '2' source_meafyen
                    from  tbIshi s, brerot_mechdal_meafyenim df
                    where  s.kod_meafyen=df.kod_meafyen

                union

                    SELECT     to_char(df.KOD_MEAFYEN) KOD_MEAFYEN, p_me_taarich me_taarich ,p_ad_taarich ad_taarich,
                                   C.TEUR_MEAFYEN_BITZUA,y.TEUR_YECHIDA_MEAFYEN YECHIDA,
                                    '' Erech_Mechdal_partany,
                                    df.erech ||  ' (�.�. �����) '  Erech_ishi,
                                    to_char(df.erech) value_erech_ishi,
                                      DECODE(df.erech,NULL,'',df.erech  ||   ' (�.�. �����) ') Erech_Brirat_Mechdal,
                                     '1' source_meafyen
                    FROM  ctb_meafyen_bitzua c, brerot_mechdal_meafyenim df, CTB_YECHIDAT_MEAFYEN Y
                    where  df.kod_meafyen not in (select   sh.kod_meafyen 
                                                                from  tbIshi sh)
                            and df.kod_meafyen = c.kod_meafyen_bitzua 
                            and c.YECHIDAT_MEAFYEN = Y.KOD_YECHIDA_MEAFYEN(+)                                                                                                                                                                  
                 ) h
            order by to_number( h.kod_meafyen), h.ME_TAARICH ;

	   		 /* SELECT  DECODE(m.kod_meafyen,NULL,to_char(b.kod_meafyen),to_char(m.kod_meafyen)) kod_meafyen,DECODE(m.Erech_Mechdal_partany,NULL,'',m.Erech_Mechdal_partany ||   ' (�.�.) ') Erech_Mechdal_partany,c.teur_MEAFYEN_BITZUA,
			                 DECODE(b.erech,NULL,'',b.erech  ||   ' (�.�. �����) ') Erech_Brirat_Mechdal,y.TEUR_YECHIDA_MEAFYEN YECHIDA,
			   		 		 DECODE(m.erech_ishi,NULL,DECODE(m.ERECH_MECHDAL_PARTANY,NULL, b.ME_TAARICH,m.ME_TAARICH),m.ME_TAARICH) ME_TAARICH,
			  				NVL( DECODE(m.erech_ishi,NULL,DECODE(m.ERECH_MECHDAL_PARTANY,NULL, b.ad_TAARICH, m.ad_TAARICH ),m.ad_TAARICH),TO_DATE('31/12/4712' ,'dd/mm/yyyy')) AD_TAARICH,
			    			  DECODE(m.erech_ishi,NULL,DECODE(m.ERECH_MECHDAL_PARTANY,NULL, b.erech ||  ' (�.�. �����) ' ,m.ERECH_MECHDAL_PARTANY || ' (�.�.) ' ),m.erech_ishi || ' (����) '  ) Erech_ishi,
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
	   		    SELECT m.kod_meafyen,DECODE(m.Erech_Mechdal_partany,NULL,'',m.Erech_Mechdal_partany ||   ' (�.�.) ') Erech_Mechdal_partany,
				 	   		 c.teur_MEAFYEN_BITZUA,( SELECT  b.erech  ||   ' (�.�. �����) '
																   		FROM BREROT_MECHDAL_MEAFYENIM b
																   		WHERE TRUNC(SYSDATE) BETWEEN b.ME_TAARICH AND NVL(b.AD_TAARICH,TO_DATE('01/01/9999','dd/mm/yyyy'))
																		AND b.kod_meafyen=m.kod_meafyen
																		AND b.erech  IS NOT NULL)  Erech_Brirat_Mechdal,
				 			m.ME_TAARICH,m.ad_TAARICH,y.TEUR_YECHIDA_MEAFYEN YECHIDA,
				           DECODE(m.erech_ishi,NULL,DECODE(m.ERECH_MECHDAL_PARTANY,NULL, NULL,m.Erech_Mechdal_partany || ' (�.�.) '),m.erech_ishi || ' (����) ' ) Erech_ishi,
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
   1.0       10/06/2009     sari      1. ������� ������� ��������� �� ������ �����
*/
PROCEDURE pro_get_historiat_meafyen(p_mispar_ishi IN MEAFYENIM_OVDIM.mispar_ishi%TYPE,
														p_from_taarich IN MEAFYENIM_OVDIM.me_taarich%TYPE,
														p_Code_meafyen  IN MEAFYENIM_OVDIM.KOD_MEAFYEN%TYPE,
														p_cur OUT CurType) IS
	BEGIN
	OPEN p_cur FOR
	   		    SELECT m.kod_meafyen,DECODE(m.Erech_Mechdal_partany,NULL,'',m.Erech_Mechdal_partany ||   ' (�.�.) ') Erech_Mechdal_partany,
				 	   		 c.teur_MEAFYEN_BITZUA,( SELECT  b.erech  ||   ' (�.�. �����) '
																   		FROM BREROT_MECHDAL_MEAFYENIM b
																   		WHERE TRUNC(SYSDATE) BETWEEN b.ME_TAARICH AND NVL(b.AD_TAARICH,TO_DATE('01/01/9999','dd/mm/yyyy'))
																		AND b.kod_meafyen=m.kod_meafyen
																		AND b.erech  IS NOT NULL)  Erech_Brirat_Mechdal,
				 			m.ME_TAARICH,m.ad_TAARICH,y.TEUR_YECHIDA_MEAFYEN YECHIDA,
				           DECODE(m.erech_ishi,NULL,DECODE(m.ERECH_MECHDAL_PARTANY,NULL, NULL,m.Erech_Mechdal_partany || ' (�.�.) '),m.erech_ishi || ' (����) ' ) Erech_ishi,
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
   1.0       07/06/2009     sari      1. ������� ������� 12 ����� ����� ������� �����
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
   1.0       23/06/2009     sari      1. ������� ������� ����� �����
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
   1.0        12/05/2009     sari      1. ���� ���� ����
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
   1.0       28/06/2009     sari      1. ������� ������� ��������� ����
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
   1.0        13/05/2009     sari      1. ������� ������� �� ����� ������ �����
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
		  TB.Bakasha_ID || ' - '||  SUBSTR(TB.Teur,0,15)  || '  ('||  TO_CHAR( TB.Zman_Hatchala,'dd.mm.yyyy')  || ') - ' ||  DECODE(TB.Bakasha_ID, v_first_bakasha,'����','������')  Teur_bakasha,
		  TB.Bakasha_ID || ' - '||  TB.Teur  || '  ('||  TO_CHAR( TB.Zman_Hatchala,'dd.mm.yyyy')  || ') - ' ||  DECODE(TB.Bakasha_ID, v_first_bakasha,'����','������')  Teur_bakasha_full
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
   1.0        01/07/2009     sari      1. ����  ���� ��� ���� �����
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
   1.0        01/07/2009      sari       1.  ����� ����� ����� ����� ���� �����
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
            REPLACE(TEUR_SNIF_AV, ''����'','''')  snif_av,
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
                
        -- ����� ������ ������� ��� ������ �� ���� ��� ���� ���� p_mispar_ishi
                
        IF (p_StatusIsuk <> 4) THEN -- ���� ����
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
                
        IF (p_Filter IS NOT NULL ) THEN -- ���� ����
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
              IF (p_kod_status_ishur =1 ) THEN --���� ����� ���� �� �����
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
                       
                            IF  (ApprovalDemandExist > 0)  THEN--����� ���� ����� �������� ����
                                 AllResult := AllResult + 1 ;
                                   DBMS_OUTPUT.PUT_LINE('(ApprovalDemandExist > 0 ) and ( p_kod_status_ishur =1) ');
                                      UPDATE TB_ISHURIM I SET  I.erech_mevukash = p_ERECH_MEVUKASH  WHERE 
                                      MISPAR_ISHI = P_MISPAR_ISHI AND KOD_ISHUR = p_kod_ishur AND TAARICH = p_TAARICH 
                                      AND MISPAR_SIDUR = p_MISPAR_SIDUR AND  SHAT_HATCHALA = p_SHAT_HATCHALA AND SHAT_YETZIA = p_SHAT_YETZIA 
                                      AND MISPAR_KNISA = p_MISPAR_KNISA AND RAMA = 3;
                          ELSIF  (p_ERECH_MEVUKASH >0 ) THEN  
                                    AllResult := AllResult + 2 ;
                                           DBMS_OUTPUT.PUT_LINE('����� ����� ����  ');
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
          ELSE -- ����� ����� ����
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
          WHEN ((o.isuk IN (11 , 12 , 13 , 36)) AND (o.snif_av IN (72181 , 72184 , 78188 )) ) THEN 1 --��� ����/���� ���� ��� ����
          WHEN ((o.isuk IN (11 , 12 , 13 , 36)) AND (o.snif_av NOT IN (72181 , 72184 , 78188 ))) THEN
              CASE WHEN  ( (o.isuk =36) AND (o.snif_av = 80143 ) AND (p_Form =2 ) ) THEN 4 --���� ����
              ELSE 2-- -- ����� ���/����� ���� ��� ����
              END
          WHEN (o.isuk NOT IN (11 , 12 , 13 , 36)) THEN 3  --�� ����� ���/����� ���� ���
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
  		SELECT UpdateDate,DECODE(mispar_ishi,-1,'�����',FullName) FullName,
		DECODE(mispar_ishi,-1,'�����',mispar_ishi) IDNUM
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
            '����� ����� + ����� ����'
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
BEGIN

     IF (p_coll_obj_peilut_ovdim IS NOT NULL) THEN
         FOR i IN REVERSE 1..p_coll_obj_peilut_ovdim.COUNT LOOP
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
BEGIN
      IF (p_coll_idkun_rashemet IS NOT NULL) THEN
          FOR i IN 1..p_coll_idkun_rashemet.COUNT LOOP
               IF (p_coll_idkun_rashemet(i).NEW_SHAT_HATCHALA=p_coll_idkun_rashemet(i).SHAT_HATCHALA) THEN                  
                  IF (p_coll_idkun_rashemet(i).NEW_SHAT_YETZIA<>p_coll_idkun_rashemet(i).SHAT_YETZIA) THEN
                      pro_upd_idkun_rashemet_peilut(p_coll_idkun_rashemet(i));                     
                  END IF;
                  pro_ins_idkun_rashemet(p_coll_idkun_rashemet(i));
                 ELSE
                   pro_upd_idkun_rashemet(p_coll_idkun_rashemet(i)); 
                   IF (p_coll_idkun_rashemet(i).update_object=1) THEN
                        pro_ins_idkun_rashemet(p_coll_idkun_rashemet(i));
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
                      DECODE(T.HUAVRA_LESACHAR ,1,'��','')   HUAVRA_LESACHAR, T.TAARICH_HAAVARA_LESACHAR
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
      SELECT  0 KOD_PROFIL ,'����'  TEUR_PROFIL_HEB , 1 ord FROM dual 
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
  SELECT  -1 kod_ezor ,'���'  teur_ezor , 1 ord FROM dual 
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
  SELECT Teur_Hevra,Kod_Hevra FROM 
  (SELECT '���' Teur_Hevra, -1 Kod_Hevra , 1 ord FROM dual
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
-- COMMIT ; 
DBMS_OUTPUT.PUT_LINE (QryMakatDate);
GeneralQry := 'Select substr(Details.EZOR,0,1) || substr(Details.MAAMAD,0,1) || ''-'' ||  Details.MISPAR_ISHI Misparishi , activity.makat_nesia, activity.shat_yetzia, activity.mispar_knisa, 
activity.oto_no,activity.Snif_Tnua,snif.teur_snif_av,Ov.shem_mish|| '' '' ||  shem_prat full_name,So.chariga,So.hashlama,
So.out_michsa, So.meadken_acharon, so.mispar_sidur,so.shat_hatchala, so.shat_gmar,so.taarich,Dayofweek(so.taarich) Dayofweek  , 
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
    ParamQry := ParamQry ||  '  AND dayofweek(so.taarich) =''�''  ';
END IF ;  
IF (( P_SNIF IS NOT  NULL ) OR ( P_SNIF <> '' )) THEN 
    ParamQry := ParamQry ||  '  AND Snif.kod_snif_av in (' || P_SNIF || ') '; 
END IF ; 
IF (( P_WORKERID IS NOT  NULL ) OR ( P_WORKERID <> '' )) THEN 
    ParamQry := ParamQry || '  AND so.mispar_ishi in (' || P_WORKERID || ' ';
END IF ; 
IF (( P_Makat IS NOT  NULL ) OR ( P_Makat <> '' )) THEN 
    ParamQry := ParamQry ||  ' AND  Activity.makat_nesia like  ''%' || P_Makat || '%'' ';
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
         S.TAARICH,S.MISPAR_SIDUR,S.SHAT_HATCHALA,S.SHAT_GMAR,S.MEADKEN_ACHARON,
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
        SELECT '��� ' description  , -1 kod FROM dual
    UNION  
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
                                            P_MisparIshi  IN VARCHAR2,
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
                          to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))>5 
                         -- T.MIKUM_SHAON='9601'
            union all
            SELECT T.MISPAR_ISHI  , (t.taarich-1) taarich,t.SHAT_HITYAZVUT,t.MIKUM_SHAON
            FROM  TB_HITYAZVUT_PUNDAKIM t,ovdim v
            WHERE   T.MISPAR_ISHI = V.MISPAR_ISHI and
                          t.TAARICH BETWEEN P_STARTDATE AND ( P_ENDDATE+1)  AND
                           to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))>=0 and to_number(to_char(t.SHAT_HITYAZVUT,'HH24'))<=5 
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
BEGIN
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
                     -- PKG_REPORTS.IsEilatTrip(nvl(c.KM,0),trunc( P_TAARICH),nvl(c.EILAT_TRIP,0)) isEilatTrip ,
                     --e.KOD_MEAFYEN MEAFYEN_NOSEA,el.TEUR_ELEMENT
                      c.EILAT_TRIP  isEilatTrip,t.teur_nekudat_tiful,trim(p.teur_nesia) teur,
                      row_number() OVER ( ORDER BY s.SHAT_HATCHALA,p.SHAT_YETZIA,p.mispar_knisa asc ) num_row,
                      case  when (p.MAKAT_NESIA >= 70000000 and  p.MAKAT_NESIA < 80000000) then pkg_elements.fun_get_description_by_kod( to_number(SUBSTR( p.MAKAT_NESIA,2,2))) else null end  TEUR_ELEMENT,
                      case  when (p.MAKAT_NESIA >= 70000000 and  p.MAKAT_NESIA < 80000000) then pkg_sidurim.fun_check_meafyen_exist( to_number(SUBSTR( p.MAKAT_NESIA,2,2)),19) else null end  MEAFYEN_NOSEA,
                      case  when (p.MAKAT_NESIA >= 70000000 and  p.MAKAT_NESIA < 80000000) then pkg_sidurim.fun_check_meafyen_exist( to_number(SUBSTR( p.MAKAT_NESIA,2,2)),11) else null end  meafyen_11

        FROM TB_SIDURIM_OVDIM s,
                  TB_PEILUT_OVDIM p  ,
                 TMP_CATALOG c,
            --      TB_MEAFYENEY_ELEMENTIM e,
            --     CTB_ELEMENTIM el,
                  CTB_NKUDUT_TIFAUL t
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
          --    AND SUBSTR( p.MAKAT_NESIA,2,2) = e.KOD_ELEMENT(+) 
         --     AND SUBSTR( p.MAKAT_NESIA,2,2)=el.KOD_ELEMENT(+)
            AND SUBSTR(p.MAKAT_NESIA,4,3)= SUBSTR(t.kod_nekudat_tiful(+),2,3)
           --   AND (el.PAIL=1 OR el.PAIL IS NULL)
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
           SELECT -1 KOD_SNIF_TNUAA,'���' teur FROM dual 
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
       (select mushee.taarich,decode(mushee.KOD_MATZAV,null,hearot_tb.heara,'�����') heara
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
                       and z.KOD_MATZAV(+) =33 ) mushee
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
   
   --1 ��� ������
   --2 �����
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
                                                                p_Cur_Num_Rechivim OUT CurType,
                                                                p_mispar_ishi IN NUMBER,
                                                                p_taarich IN DATE,
                                                                p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE) IS
 
 BEGIN

     PKG_RIKUZ_AVODA.pro_get_rechivim_lerikuz_tmp(p_mispar_ishi,  p_taarich,p_bakasha_id,p_Cur_Rechivim_Yomi);
      
     PKG_RIKUZ_AVODA.pro_rechivim_chodshiim_tmp(p_mispar_ishi,p_taarich,p_bakasha_id,p_Cur_Rechivim_Chodshi);
            
     PKG_RIKUZ_AVODA.pro_rechivey_headrut_tmp(p_mispar_ishi ,  p_taarich ,  p_bakasha_id  ,   p_Cur_Rechivey_Headrut);     
     
      --PKG_RIKUZ_AVODA.pro_rechivey_shonot_tmp(p_mispar_ishi ,  p_taarich ,  p_bakasha_id  ,   p_Cur_Rechivey_Headrut);     
      
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
--p_taarich:=to_date('31/08/2010','dd/mm/yyyy');
   tar_me:= to_date('01/' ||  to_char(p_taarich,'mm/yyyy'),'dd/mm/yyyy');
   list_rechivim:= '1,22,26,28,29,30,47,48,49,50,53,55,66,67,76,77,78,91,92,125,126,131,202,203,220,221';    
 open  p_cur for                                                                                     
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
        order  BY p.mispar_ishi, p.taarich;

end pro_get_rechivim_lerikuz_tmp;

PROCEDURE pro_rechivim_chodshiim_tmp(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                                              p_taarich IN DATE,
                                                              p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                                             p_cur OUT CurType) IS
    tar_me date;       
    list_rechivim nvarchar2(200);        
  --  p_taarich  DATE;                                                             
 BEGIN  
 --  p_taarich:=to_date('31/08/2010','dd/mm/yyyy');
   tar_me:= to_date('01/' ||  to_char(p_taarich,'mm/yyyy'),'dd/mm/yyyy');
   list_rechivim:= '1,5,10,11,22,24,26,28,29,30,39,41,43,47,48,49,50,53,55,66,67,76,77,78,91,92,95,100,101,102,103,108,119,120,121,122,125,126,131,146,202,203,219,220,221';    
         
   open  p_cur for                  
    select   
          max(case to_number(kod_rechiv) when 1 then  erech_rechiv end ) r1 ,
          max(case to_number(kod_rechiv) when 5 then  erech_rechiv end ) r5 ,
          max(case to_number(kod_rechiv) when 10 then  erech_rechiv end ) r10 ,
          max(case to_number(kod_rechiv) when 11 then  erech_rechiv end ) r11 ,
          max(case to_number(kod_rechiv) when 22 then  erech_rechiv end ) r22 ,
          max(case to_number(kod_rechiv) when 24 then  erech_rechiv end ) r24,
          max(case to_number(kod_rechiv) when 26 then  erech_rechiv end ) r26 ,
          max(case to_number(kod_rechiv) when 28 then  erech_rechiv end ) r28 ,
          max(case to_number(kod_rechiv) when 29 then  erech_rechiv end ) r29 ,
          max(case to_number(kod_rechiv) when 30 then erech_rechiv end ) r30 ,
          max(case to_number(kod_rechiv) when 39 then  erech_rechiv end ) r39 ,
          max(case to_number(kod_rechiv) when 41 then  erech_rechiv end ) r41,
          max(case to_number(kod_rechiv) when 43 then  erech_rechiv end ) r43,
          max(case to_number(kod_rechiv) when 47 then  erech_rechiv end ) r47,
          max(case to_number(kod_rechiv) when 48 then  erech_rechiv end ) r48,
          max(case to_number(kod_rechiv) when 49 then  erech_rechiv end ) r49,
          max(case to_number(kod_rechiv) when 50 then  erech_rechiv end ) r50,
          max(case to_number(kod_rechiv) when 53 then  erech_rechiv end ) r53 ,
          max(case to_number(kod_rechiv) when 55 then  erech_rechiv end ) r55,
          max(case to_number(kod_rechiv) when 66 then  erech_rechiv end ) r66,
          max(case to_number(kod_rechiv) when 67 then  erech_rechiv end ) r67,
          max(case to_number(kod_rechiv) when 76 then  erech_rechiv end ) r76,
          max(case to_number(kod_rechiv) when 77 then  erech_rechiv end ) r77 ,
          max(case to_number(kod_rechiv) when 78 then erech_rechiv end ) r78 ,
          max(case to_number(kod_rechiv) when 91 then  erech_rechiv end ) r91 ,
          max(case to_number(kod_rechiv) when 92 then  erech_rechiv end ) r92 ,
          max(case to_number(kod_rechiv) when 95 then  erech_rechiv end ) r95 ,
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
          max(case to_number(kod_rechiv) when 22 then  erech_rechiv end ) r220 ,
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
--   p_taarich:=to_date('31/08/2010','dd/mm/yyyy');
   tar_me:= to_date('01/' ||  to_char(p_taarich,'mm/yyyy'),'dd/mm/yyyy');
   list_rechivim:= '53,56,57,60,61,62,64,65,66,67,68,69,70,71,72,266';    
   
   open  p_cur for                  
    select   
          max(case to_number(kod_rechiv) when 53 then  erech_rechiv end ) r53,
          max(case to_number(kod_rechiv) when 56 then  erech_rechiv end ) r56 ,
          max(case to_number(kod_rechiv) when 57 then  erech_rechiv end ) r57 ,
          max(case to_number(kod_rechiv) when 60 then  erech_rechiv end ) r60 ,
          max(case to_number(kod_rechiv) when 61 then  erech_rechiv end ) r61 ,
          max(case to_number(kod_rechiv) when 62 then  erech_rechiv end ) r62,
      --    max(case to_number(kod_rechiv) when 63 then  erech_rechiv end ) r63,
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
  -- p_taarich  DATE;                                                             
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
          max(case to_number(kod_rechiv) when 205 then  erech_rechiv end ) r205
   from(     
   select  C.KOD_RECHIV,C.ERECH_RECHIV
   from TB_TMP_CHISHUV_CHODESH_OVDIM C
   where c.Mispar_Ishi=p_mispar_ishi 
              and C.Bakasha_ID=p_bakasha_id
              AND c.taarich between tar_me and last_day(tar_me) 
              and  c.Kod_Rechiv in ( SELECT X FROM TABLE(CAST(Convert_String_To_Table( list_rechivim,  ',') AS MYTABTYPE))) ) p ;
              
end pro_rechivey_shonot_tmp;


PROCEDURE Pro_get_num_rechivim_tmp(p_mispar_ishi IN OVDIM.mispar_ishi%TYPE,
                                                            p_taarich IN DATE,
                                                                                             p_bakasha_id IN TB_BAKASHOT.bakasha_id%TYPE,
                                                                                         p_cur OUT CurType) IS
   tar_me date;       
    list_RechiveyHeadrut nvarchar2(100);        
    list_RechiveyShonot nvarchar2(100);        
   --p_taarich  DATE;                                                             
 BEGIN  
 --  p_taarich:=to_date('31/08/2010','dd/mm/yyyy');
   tar_me:= to_date('01/' ||  to_char(p_taarich,'mm/yyyy'),'dd/mm/yyyy');
   list_RechiveyHeadrut:= '56,57,64,68,69,70,71,72,65';    
   list_RechiveyShonot:= '95,10,11,126,39,43,41';    
   
   --1 ��� ������
   --2 �����
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


CREATE OR REPLACE PACKAGE BODY          Pkg_Sdrn AS
/******************************************************************************
   NAME:       PKG_BATCH
   PURPOSE:

   REVISIONS:
   Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0        26/04/2009             1. Created this package body.
******************************************************************************/


  PROCEDURE pro_ins_yamim_4_sidurim(pDt VARCHAR) IS
--err_str  varchar2(1000);
 
   CURSOR Yamim IS
  SELECT   DISTINCT driver_id,start_dt,-12 meadken--,0,sysdate,-12
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
WHERE start_dt= TO_DATE(pDt,'yyyymmdd')
AND  NOT  EXISTS(SELECT * FROM TB_YAMEY_AVODA_OVDIM
WHERE  mispar_ishi=driver_id
AND  taarich=start_dt);

BEGIN
--  err_str:='';
  
FOR  Yamim_rec IN  Yamim LOOP
BEGIN
 INSERT INTO  TB_YAMEY_AVODA_OVDIM (mispar_ishi,taarich,lina,taarich_idkun_acharon,meadken_acharon)
VALUES (Yamim_rec.driver_id,Yamim_rec.start_dt,  0,SYSDATE,Yamim_rec.meadken );

EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,1) '||to_char(Yamim_rec.driver_id)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
 	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,1,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Yamim_rec.driver_id)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
END;
END LOOP;
COMMIT;

-- if  not err_str=''  then 
--  raise_application_error(1, SUBSTR     (err_str,1,1000), TRUE);
--  end if;

  
END pro_ins_yamim_4_sidurim;


  PROCEDURE pro_ins_sidurim_4_sidurim(pDt VARCHAR) IS
-- err_str  varchar2(1000);
  
  CURSOR Sidurim1 IS
  SELECT   DISTINCT k1.driver_id,k1.start_dt,k1.schedule_num,
k1.start_dt + SUBSTR(LPAD(k1.start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(k1.start_schedule,4,0),3,2)/1440 hatchala ,
k1.start_dt + SUBSTR(LPAD(k1.end_schedule,4,0),1,2)/24 + SUBSTR(LPAD(k1.end_schedule,4,0),3,2)/1440 gmar,
DECODE(sug_visa,' ','',sug_visa) sug_visa,-12 meadken,sug_sidur
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1
WHERE k1.start_dt=  TO_DATE(pDt,'yyyymmdd')
AND NOT EXISTS (SELECT *  FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k2
WHERE k2.start_dt=  TO_DATE(pDt,'yyyymmdd')
AND k1.driver_id=k2.driver_id
AND k1.start_dt=k2.start_dt
AND k1.schedule_num=k2.schedule_num
AND k1.start_schedule=k2.start_schedule
AND k1.end_schedule<>k2.end_schedule);
--sidurim9 is update not insert!!
--and not (k1.start_schedule<2400
--and k1.end_schedule<2400
--and decode(sug_visa,' ','',sug_visa) in (0,1)
--and  k1.start_schedule>k1.end_schedule);

 
 CURSOR Trail_Sidurim1 IS
 SELECT   DISTINCT driver_id,start_dt,schedule_num,
TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')  hatchala,
TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(end_schedule-2400,4,0),1,2)||':'||SUBSTR(LPAD(end_schedule,4,0),3,2),'yyyymmdd hh24:mi')  gmar,
DECODE(sug_visa,' ','',sug_visa) sug_visa,-12 meadken,sug_sidur
FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
WHERE start_dt =  TO_DATE(pDt,'yyyymmdd')
AND start_schedule<2400
AND end_schedule>2359
 AND  EXISTS(SELECT * FROM TB_YAMEY_AVODA_OVDIM
WHERE  mispar_ishi=driver_id
AND  taarich=start_dt)
AND  EXISTS (SELECT * FROM  TB_SIDURIM_OVDIM s2
WHERE s2.mispar_ishi=driver_id
AND  s2.taarich= start_dt
AND s2.mispar_sidur =schedule_num
AND  s2.shat_hatchala =TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')
AND s2.shat_gmar<start_dt+1);
 
 CURSOR Sidurim6 IS
  SELECT   DISTINCT k1.driver_id,k1.start_dt,k1.schedule_num,
TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_schedule,4,0),3,2),'yyyymmdd hh24:mi') hatchala,
TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.end_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.end_schedule,4,0),3,2),'yyyymmdd hh24:mi') gmar,
DECODE(sug_visa,' ','',sug_visa) sug_visa, -12 meadken,sug_sidur
FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1
WHERE k1.start_dt =  TO_DATE(pDt,'yyyymmdd')
AND k1.start_schedule<2400
AND k1.end_schedule<2400
AND k1.end_schedule=(SELECT MIN(k3.end_schedule) FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k3
WHERE k3.start_dt =  TO_DATE(pDt,'yyyymmdd')
AND k3.start_schedule<2400
AND k3.end_schedule<2400
AND k1.driver_id=k3.driver_id
AND k1.start_dt=k3.start_dt
AND k1.schedule_num=k3.schedule_num
AND k1.start_schedule=k3.start_schedule)
AND  EXISTS (SELECT *  FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k2
WHERE k2.start_dt =  TO_DATE(pDt,'yyyymmdd')
AND k2.start_schedule<2400
AND k2.end_schedule<2400
AND k1.driver_id=k2.driver_id
AND k1.start_dt=k2.start_dt
AND k1.schedule_num=k2.schedule_num
AND k1.start_schedule=k2.start_schedule
AND k1.end_schedule<>k2.end_schedule);

CURSOR Sidurim7 IS
SELECT   DISTINCT k1.driver_id,k1.start_dt,k1.schedule_num,
TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_schedule,4,0),3,2),'yyyymmdd hh24:mi') +1/1440 hatchala,
TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.end_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.end_schedule,4,0),3,2),'yyyymmdd hh24:mi') gmar,
DECODE(sug_visa,' ','',sug_visa) sug_visa , -12 meadken,sug_sidur
FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1
WHERE k1.start_dt =  TO_DATE(pDt,'yyyymmdd')
AND k1.start_schedule<2400
AND k1.end_schedule<2400
AND k1.end_schedule=(SELECT MAX(k3.end_schedule) FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k3
WHERE k3.start_dt =  TO_DATE(pDt,'yyyymmdd')
AND k3.start_schedule<2400
AND k3.end_schedule<2400
AND k1.driver_id=k3.driver_id
AND k1.start_dt=k3.start_dt
AND k1.schedule_num=k3.schedule_num
AND k1.start_schedule=k3.start_schedule)
AND  EXISTS (SELECT *  FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k2
WHERE k2.start_dt =  TO_DATE(pDt,'yyyymmdd')
AND k2.start_schedule<2400
AND k2.end_schedule<2400
AND k1.driver_id=k2.driver_id
AND k1.start_dt=k2.start_dt
AND k1.schedule_num=k2.schedule_num
AND k1.start_schedule=k2.start_schedule
AND k1.end_schedule<>k2.end_schedule);

CURSOR Sidurim8  IS
  SELECT  DISTINCT driver_id,start_dt,schedule_num,
TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi') hatchala,
TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(end_schedule-2400,4,0),1,2)||':'||SUBSTR(LPAD(end_schedule,4,0),3,2),'yyyymmdd hh24:mi') gmar,
DECODE(sug_visa,' ','',sug_visa) sug_visa, -12 meadken,sug_sidur
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1
WHERE k1.start_dt = TO_DATE(pDt,'yyyymmdd') 
AND k1.start_schedule<2400
AND k1.end_schedule>2359
AND    EXISTS ( SELECT * FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k2
WHERE k1.start_dt = TO_DATE(pDt,'yyyymmdd') 
AND k2.start_schedule<2400
AND k2.end_schedule>2359
AND k1.start_dt= k2.start_dt
AND   k2.driver_id =k1.driver_id
AND k2.schedule_num=k1.schedule_num
AND k2.start_schedule=k1.start_schedule
AND k2.end_schedule<>k1.end_schedule)
ORDER BY driver_id;

  CURSOR Trail_Sidurim2 IS
SELECT   DISTINCT k1.driver_id,k1.start_dt,k1.schedule_num,
TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_schedule,4,0),3,2),'yyyymmdd hh24:mi')  hatchala,
TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.end_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.end_schedule,4,0),3,2),'yyyymmdd hh24:mi')  gmar,
DECODE(sug_visa,' ','',sug_visa) sug_visa, -12 meadken,sug_sidur
FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1
WHERE k1.start_dt =  TO_DATE(pDt,'yyyymmdd')
AND k1.start_schedule<2400
AND k1.end_schedule<2400
AND k1.end_schedule=(SELECT MAX(k3.end_schedule) FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k3
WHERE k3.start_dt =  TO_DATE(pDt,'yyyymmdd')
AND k3.start_schedule<2400
AND k3.end_schedule<2400
AND k1.driver_id=k3.driver_id
AND k1.start_dt=k3.start_dt
AND k1.schedule_num=k3.schedule_num
AND k1.start_schedule=k3.start_schedule)
AND  EXISTS (SELECT *  FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k2
WHERE k2.start_dt =  TO_DATE(pDt,'yyyymmdd')
AND k2.start_schedule<2400
AND k2.end_schedule<2400
AND k1.driver_id=k2.driver_id
AND k1.start_dt=k2.start_dt
AND k1.schedule_num=k2.schedule_num
AND k1.start_schedule=k2.start_schedule
AND k1.end_schedule<>k2.end_schedule);

-- problem? if start_dt+1 is today and is in the future??
-- problem: do not write it on cursor1
CURSOR Sidurim9  IS
SELECT   DISTINCT k1.driver_id,k1.start_dt,k1.schedule_num,
TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_schedule,4,0),3,2),'yyyymmdd hh24:mi') hatchala,
TO_DATE(TO_CHAR(k1.start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(k1.end_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.end_schedule,4,0),3,2),'yyyymmdd hh24:mi') gmar
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1
WHERE start_dt= TO_DATE(pDt,'yyyymmdd')
AND k1.start_schedule<2400
AND k1.end_schedule<2400
AND DECODE(sug_visa,' ','',sug_visa) IN (0,1)
AND  k1.start_schedule>k1.end_schedule;


   BEGIN
--  err_str:='';
     
   FOR  Sidurim1_rec IN  Sidurim1 LOOP
BEGIN
  INSERT INTO TB_SIDURIM_OVDIM s ( mispar_ishi  , taarich  , mispar_sidur  , shat_hatchala  , shat_gmar,sector_visa,meadken_acharon   ,sug_sidur  )
VALUES (Sidurim1_rec.driver_id,Sidurim1_rec.start_dt,  Sidurim1_rec.schedule_num,  
Sidurim1_rec.hatchala,Sidurim1_rec.gmar,  Sidurim1_rec.sug_visa,   Sidurim1_rec.meadken, Sidurim1_rec.sug_sidur );

EXCEPTION
   WHEN OTHERS THEN
 --  err_str:=err_str||' (4,20) '||to_char(Sidurim1_rec.driver_id) ||' '||to_char(Sidurim1_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,20,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Sidurim1_rec.driver_id) ||' '||TO_CHAR(Sidurim1_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
END;
END LOOP;
COMMIT;

 --used to be in Sidurim3:&  Sidurim5:
 BEGIN
 UPDATE  TB_SIDURIM_OVDIM 
 SET   Shayah_LeYom_Kodem=1   
WHERE  taarich = TO_DATE(pDt,'yyyymmdd')
AND EXISTS (SELECT *   FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1
WHERE k1.start_dt= TO_DATE(pDt,'yyyymmdd')
AND k1.start_schedule>2359
--and k1.end_schedule>2359
AND    taarich = k1.start_dt
AND mispar_ishi=k1.driver_id
AND  mispar_sidur  =k1.schedule_num
AND  shat_hatchala  =k1.start_dt + SUBSTR(LPAD(k1.start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(k1.start_schedule,4,0),3,2)/1440
AND  shat_gmar=k1.start_dt + SUBSTR(LPAD(k1.end_schedule,4,0),1,2)/24 + SUBSTR(LPAD(k1.end_schedule,4,0),3,2)/1440);

EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,22) '||to_char(Sidurim3_rec.driver_id) ||' '||to_char(Sidurim3_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
    INSERT INTO TB_LOG_TAHALICH
    VALUES (4,1,22,SYSDATE,'',10,'',SUBSTR(pDt||' Shayah_LeYom_Kodem '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));     
END;
COMMIT;
 

FOR  Trail_Sidurim1_rec IN  Trail_Sidurim1 LOOP
BEGIN
 INSERT INTO TRAIL_SIDURIM_OVDIM s ( mispar_ishi  , taarich  , mispar_sidur  , shat_hatchala  , shat_gmar,sector_visa,meadken_acharon  ,sug_sidur   )
 VALUES (Trail_Sidurim1_rec.driver_id,Trail_Sidurim1_rec.start_dt,  Trail_Sidurim1_rec.schedule_num,  
Trail_Sidurim1_rec.hatchala,Trail_Sidurim1_rec.gmar,  Trail_Sidurim1_rec.sug_visa,   Trail_Sidurim1_rec.meadken, Trail_Sidurim1_rec.sug_sidur );


EXCEPTION
   WHEN OTHERS THEN
 --  err_str:=err_str||' (4,25) '||to_char(Trail_Sidurim1_rec.driver_id) ||' '||to_char(Trail_Sidurim1_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,25,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Trail_Sidurim1_rec.driver_id) ||' '||TO_CHAR(Trail_Sidurim1_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));   
   
END;
END LOOP;
COMMIT;


FOR  Sidurim6_rec IN  Sidurim6 LOOP
BEGIN
INSERT INTO TB_SIDURIM_OVDIM ( mispar_ishi  , taarich  , mispar_sidur  , shat_hatchala  ,
shat_gmar,sector_visa,meadken_acharon  ,sug_sidur   )
VALUES (Sidurim6_rec.driver_id,Sidurim6_rec.start_dt,  Sidurim6_rec.schedule_num,  Sidurim6_rec.hatchala,
Sidurim6_rec.gmar,  Sidurim6_rec.sug_visa,   Sidurim6_rec.meadken ,Sidurim6_rec.sug_sidur   );
EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,26) '||to_char(Sidurim6_rec.driver_id) ||' '||to_char(Sidurim6_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,26,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Sidurim6_rec.driver_id) ||' '||TO_CHAR(Sidurim6_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));   
   
END;
END LOOP;
COMMIT;


 FOR  Sidurim7_rec IN  Sidurim7 LOOP
BEGIN
INSERT INTO TB_SIDURIM_OVDIM ( mispar_ishi  , taarich  , mispar_sidur  , shat_hatchala  ,
shat_gmar,sector_visa,meadken_acharon  ,sug_sidur   )
VALUES (Sidurim7_rec.driver_id,Sidurim7_rec.start_dt,  Sidurim7_rec.schedule_num,  Sidurim7_rec.hatchala,
Sidurim7_rec.gmar,  Sidurim7_rec.sug_visa,   Sidurim7_rec.meadken ,Sidurim7_rec.sug_sidur    );
EXCEPTION
   WHEN OTHERS THEN
 --  err_str:=err_str||' (4,27) '|| to_char(Sidurim7_rec.driver_id) ||' '||to_char(Sidurim7_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,27,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Sidurim7_rec.driver_id) ||' '||TO_CHAR(Sidurim7_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));   
  
END;
END LOOP;
COMMIT;


FOR  Sidurim8_rec IN  Sidurim8 LOOP
BEGIN
INSERT INTO TB_SIDURIM_OVDIM s1 ( mispar_ishi  , taarich  , mispar_sidur  , shat_hatchala  , shat_gmar,sector_visa,meadken_acharon   ,sug_sidur  )
VALUES (Sidurim8_rec.driver_id,Sidurim8_rec.start_dt,  Sidurim8_rec.schedule_num,  
Sidurim8_rec.hatchala,Sidurim8_rec.gmar,  Sidurim8_rec.sug_visa,   Sidurim8_rec.meadken  ,Sidurim8_rec.sug_sidur );


EXCEPTION
   WHEN OTHERS THEN
 --  err_str:=err_str||' (4,28) '|| to_char(Sidurim8_rec.driver_id) ||' '||to_char(Sidurim8_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,28,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Sidurim8_rec.driver_id) ||' '||TO_CHAR(Sidurim8_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));   

END;
END LOOP;
COMMIT;


FOR  Trail_Sidurim2_rec IN  Trail_Sidurim2 LOOP
BEGIN
 INSERT INTO TRAIL_SIDURIM_OVDIM s ( mispar_ishi  , taarich  , mispar_sidur  , shat_hatchala  , shat_gmar,sector_visa,meadken_acharon  ,sug_sidur  ) 
 VALUES (Trail_Sidurim2_rec.driver_id,Trail_Sidurim2_rec.start_dt,  Trail_Sidurim2_rec.schedule_num,  
Trail_Sidurim2_rec.hatchala,Trail_Sidurim2_rec.gmar,  Trail_Sidurim2_rec.sug_visa,   Trail_Sidurim2_rec.meadken  ,Trail_Sidurim2_rec.sug_sidur );


EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,29) '|| to_char(Trail_Sidurim2_rec.driver_id) ||' '||to_char(Trail_Sidurim2_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,29,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Trail_Sidurim2_rec.driver_id) ||' '||TO_CHAR(Trail_Sidurim2_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));   

END;
END LOOP;
COMMIT;

  
BEGIN
 UPDATE TB_SIDURIM_OVDIM 
SET   Lo_letashlum=1 ,Kod_Siba_Lo_Letashlum=16
 WHERE taarich= TO_DATE(pDt,'yyyymmdd')
AND meadken_acharon=-12;

  EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,2) '||'update_sidurim '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,2,SYSDATE,'',10,'',SUBSTR('update_sidurim '||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));   

END;
COMMIT;

--if not  err_str=''  then 
--  raise_application_error(2, SUBSTR     (err_str,1,1000), TRUE);
--  end if;

FOR  Sidurim9_rec IN  Sidurim9 LOOP
BEGIN
UPDATE TB_SIDURIM_OVDIM 
SET shat_gmar=Sidurim9_rec.gmar
WHERE mispar_ishi=Sidurim9_rec.driver_id
AND taarich=Sidurim9_rec.start_dt
AND mispar_sidur=Sidurim9_rec.schedule_num
AND shat_hatchala=Sidurim9_rec.hatchala
AND sector_visa IN (0,1);

EXCEPTION
   WHEN OTHERS THEN
 --  err_str:=err_str||' (4,28) '|| to_char(Sidurim8_rec.driver_id) ||' '||to_char(Sidurim8_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,2,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Sidurim9_rec.driver_id) ||' '||TO_CHAR(Sidurim9_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));   

END;
END LOOP;
COMMIT;

END pro_ins_sidurim_4_sidurim;


 PROCEDURE pro_ins_peilut_4_sidurim(pDt VARCHAR) IS
-- err_str  varchar2(1000);
 
   CURSOR Peilut1 IS
   SELECT    driver_id,start_dt,schedule_num,branch,
-- to_date(to_char(start_dt,'yyyymmdd')||' '||substr(lpad(start_schedule,4,0),1,2)||':'||substr(lpad(start_schedule,4,0),3,2),'yyyymmdd hh24:mi') hatchala,
-- to_date(to_char(start_dt,'yyyymmdd')||' '||substr(lpad(start_time,4,0),1,2)||':'||substr(lpad(start_time,4,0),3,2),'yyyymmdd hh24:mi') gmar,ride_id,
start_dt + SUBSTR(LPAD(start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(start_schedule,4,0),3,2)/1440 hatchala ,
start_dt + SUBSTR(LPAD(start_time,4,0),1,2)/24 + SUBSTR(LPAD(start_time,4,0),3,2)/1440 gmar,ride_id,
 makat_line,bus_number,bus_sequence,waiting_time,
 spm_time,spm_bus_number,spm_schedule_num,spm_start_time,spm_makat_line,spm_line_sign,spm_location,-12 meadken
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
WHERE  start_dt= TO_DATE(pDt,'yyyymmdd')
AND start_schedule<2400
AND start_time<2400
AND NOT (start_schedule<2400
AND end_schedule<2400
AND DECODE(sug_visa,' ','',sug_visa) IN (0,1)
AND  start_schedule>end_schedule);
-- and schedule_num<100000;

   CURSOR Peilut2 IS
  SELECT    driver_id,start_dt,schedule_num,branch,
-- to_date(to_char(start_dt,'yyyymmdd')||' '||substr(lpad(start_schedule,4,0),1,2)||':'||substr(lpad(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')  hatchala,
-- to_date(to_char(start_dt+1,'yyyymmdd')||' '||substr(lpad(start_time-2400,4,0),1,2)||':'||substr(lpad(start_time,4,0),3,2),'yyyymmdd hh24:mi') gmar,ride_id,
start_dt + SUBSTR(LPAD(start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(start_schedule,4,0),3,2)/1440 hatchala ,
start_dt + SUBSTR(LPAD(start_time,4,0),1,2)/24 + SUBSTR(LPAD(start_time,4,0),3,2)/1440 gmar,ride_id,
 makat_line,bus_number,bus_sequence,waiting_time,
 spm_time,spm_bus_number,spm_schedule_num,spm_start_time,spm_makat_line,spm_line_sign,spm_location,-12 meadken
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
WHERE  start_dt= TO_DATE(pDt,'yyyymmdd')
AND start_schedule<2400
AND start_time>2359
 --and schedule_num<100000;
 AND NOT EXISTS (SELECT * FROM  TB_SIDURIM_OVDIM s2
WHERE s2.mispar_ishi=driver_id
AND  s2.taarich= start_dt
AND s2.mispar_sidur =schedule_num
AND  s2.shat_hatchala =TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')
AND s2.shat_gmar<start_dt+1);

   CURSOR Peilut3 IS
   SELECT    driver_id,start_dt,schedule_num,branch,
-- to_date(to_char(start_dt+1,'yyyymmdd')||' '||substr(lpad(start_schedule-2400,4,0),1,2)||':'||substr(lpad(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')  hatchala,
-- to_date(to_char(start_dt+1,'yyyymmdd')||' '||substr(lpad(start_time-2400,4,0),1,2)||':'||substr(lpad(start_time,4,0),3,2),'yyyymmdd hh24:mi') gmar,ride_id,
start_dt + SUBSTR(LPAD(start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(start_schedule,4,0),3,2)/1440 hatchala ,
start_dt + SUBSTR(LPAD(start_time,4,0),1,2)/24 + SUBSTR(LPAD(start_time,4,0),3,2)/1440 gmar,ride_id,
 makat_line,bus_number,bus_sequence,waiting_time,
 spm_time,spm_bus_number,spm_schedule_num,spm_start_time,spm_makat_line,spm_line_sign,spm_location,-12 meadken
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
WHERE  start_dt= TO_DATE(pDt,'yyyymmdd')
AND start_schedule>2359;
--and start_time>2359;
-- and schedule_num<100000;

   CURSOR Peilut4 IS
SELECT    driver_id,start_dt,schedule_num,branch,
-- to_date(to_char(start_dt,'yyyymmdd')||' '||substr(lpad(start_schedule,4,0),1,2)||':'||substr(lpad(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')+1/1440  hatchala,
-- to_date(to_char(start_dt+1,'yyyymmdd')||' '||substr(lpad(start_time-2400,4,0),1,2)||':'||substr(lpad(start_time,4,0),3,2),'yyyymmdd hh24:mi') gmar,ride_id,
start_dt + SUBSTR(LPAD(start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(start_schedule,4,0),3,2)/1440 hatchala ,
start_dt + SUBSTR(LPAD(start_time,4,0),1,2)/24 + SUBSTR(LPAD(start_time,4,0),3,2)/1440 gmar,ride_id,
 makat_line,bus_number,bus_sequence,waiting_time,
 spm_time,spm_bus_number,spm_schedule_num,spm_start_time,spm_makat_line,spm_line_sign,spm_location,-12 meadken
FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
WHERE  start_dt= TO_DATE(pDt,'yyyymmdd')
AND start_schedule<2400
AND start_time>2359
 --and schedule_num<100000;
 AND EXISTS (SELECT * FROM  TB_SIDURIM_OVDIM s2
WHERE s2.mispar_ishi=driver_id
AND  s2.taarich= start_dt
AND s2.mispar_sidur =schedule_num
AND  s2.shat_hatchala =TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')
AND s2.shat_gmar<start_dt+1);

-- included in cursor3
--   CURSOR Peilut5 IS
--  select    driver_id,start_dt,schedule_num,branch,
-- to_date(to_char(start_dt+1,'yyyymmdd')||' '||substr(lpad(start_schedule-2400,4,0),1,2)||':'||substr(lpad(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')  hatchala,
-- to_date(to_char(start_dt,'yyyymmdd')||' '||substr(lpad(start_time,4,0),1,2)||':'||substr(lpad(start_time,4,0),3,2),'yyyymmdd hh24:mi') gmar,ride_id,
-- makat_line,bus_number,bus_sequence,waiting_time,
-- spm_time,spm_bus_number,spm_schedule_num,spm_start_time,spm_makat_line,spm_line_sign,spm_location,-12 meadken
--from kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
--where  start_dt= to_date(pDt,'yyyymmdd')
--and start_schedule>2359
--and start_time<2400;

   CURSOR Peilut6 IS
 SELECT    k1.driver_id,k1.start_dt,k1.schedule_num,branch,
 TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_schedule,4,0),3,2),'yyyymmdd hh24:mi')  hatchala,
 TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_time,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_time,4,0),3,2),'yyyymmdd hh24:mi')  gmar,k1.ride_id,
 k1.makat_line,bus_number,k1.bus_sequence,k1.waiting_time,
 k1.spm_time,k1.spm_bus_number,k1.spm_schedule_num,k1.spm_start_time,k1.spm_makat_line,
 k1.spm_line_sign,k1.spm_location,-12 meadken
FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1
WHERE  k1.start_dt= TO_DATE(pDt,'yyyymmdd')
AND k1.start_schedule<2400
AND k1.start_time<2400
 --and k1.schedule_num<100000
AND k1.end_schedule=(SELECT MIN(k3.end_schedule) FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k3
WHERE k3.start_dt =  TO_DATE(pDt,'yyyymmdd')
AND k3.start_schedule<2400
AND k3.end_schedule<2400
AND k1.driver_id=k3.driver_id
AND k1.start_dt=k3.start_dt
AND k1.schedule_num=k3.schedule_num
AND k1.start_schedule=k3.start_schedule)
 AND EXISTS (SELECT *  FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k2
WHERE k2.start_dt=  TO_DATE(pDt,'yyyymmdd')
AND k2.start_schedule<2400
AND k2.end_schedule<2400
AND k1.driver_id=k2.driver_id
AND k1.start_dt=k2.start_dt
AND k1.schedule_num=k2.schedule_num
AND k1.start_schedule=k2.start_schedule
AND k1.end_schedule<>k2.end_schedule)
AND NOT EXISTS (SELECT * FROM  TB_PEILUT_OVDIM s2
WHERE s2.mispar_ishi= k1.driver_id
AND  s2.taarich= k1.start_dt
AND  s2.taarich=  TO_DATE(pDt,'yyyymmdd')
AND s2.mispar_sidur =k1.schedule_num
AND  s2.shat_hatchala_sidur =TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_schedule,4,0),3,2),'yyyymmdd hh24:mi')
AND s2.SHAT_YETZIA=TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_time,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_time,4,0),3,2),'yyyymmdd hh24:mi')
AND s2.MISPAR_KNISA=k1.ride_id);

   CURSOR Peilut7 IS
 SELECT    k1.driver_id,k1.start_dt,k1.schedule_num,branch,
 TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_schedule,4,0),3,2),'yyyymmdd hh24:mi')+1/1440  hatchala,
 TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_time,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_time,4,0),3,2),'yyyymmdd hh24:mi')  gmar,k1.ride_id,
 k1.makat_line,bus_number,k1.bus_sequence,k1.waiting_time,
 k1.spm_time,k1.spm_bus_number,k1.spm_schedule_num,k1.spm_start_time,k1.spm_makat_line,
 k1.spm_line_sign,k1.spm_location,-12 meadken
FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1
WHERE  k1.start_dt= TO_DATE(pDt,'yyyymmdd')
AND k1.start_schedule<2400
AND k1.start_time<2400
 --and k1.schedule_num<100000
 AND k1.end_schedule=(SELECT MAX(k3.end_schedule) FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k3
WHERE k3.start_dt =  TO_DATE(pDt,'yyyymmdd')
AND k3.start_schedule<2400
AND k3.end_schedule<2400
AND k1.driver_id=k3.driver_id
AND k1.start_dt=k3.start_dt
AND k1.schedule_num=k3.schedule_num
AND k1.start_schedule=k3.start_schedule)
AND EXISTS (SELECT *  FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k2
WHERE k2.start_dt=  TO_DATE(pDt,'yyyymmdd')
AND k2.start_schedule<2400
AND k2.end_schedule<2400
AND k1.driver_id=k2.driver_id
AND k1.start_dt=k2.start_dt
AND k1.schedule_num=k2.schedule_num
AND k1.start_schedule=k2.start_schedule
AND k1.end_schedule<>k2.end_schedule);

 CURSOR Peilut8  IS
  SELECT   DISTINCT    start_dt, driver_id,schedule_num,
                 TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi') hatchala,
				 TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_time,4,0),1,2)||':'||SUBSTR(LPAD(start_time,4,0),3,2),'yyyymmdd hh24:mi') moed,
				 makat_line,ride_id,  line_description
 FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
						   	WHERE  start_dt=  TO_DATE(pDt,'yyyymmdd')
							AND start_schedule<2400
							AND start_time<2400
							AND line_description IS NOT NULL
							AND ride_id>0
							AND makat_line<50000000;
							
--like sidurim9, for visa when end<start
-- problem: do not write it on cursor1
CURSOR Peilut9  IS
  SELECT   DISTINCT      driver_id,start_dt,schedule_num,branch,
 TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi') hatchala,
 TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_time,4,0),1,2)||':'||SUBSTR(LPAD(start_time,4,0),3,2),'yyyymmdd hh24:mi') gmar,ride_id,
 makat_line,bus_number,bus_sequence,waiting_time,
 spm_time,spm_bus_number,spm_schedule_num,spm_start_time,spm_makat_line,spm_line_sign,spm_location,-12 meadken
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1
WHERE  start_dt=  TO_DATE(pDt,'yyyymmdd')
AND k1.start_schedule<2400
AND k1.end_schedule<2400
AND DECODE(sug_visa,' ','',sug_visa) IN (0,1)
AND  k1.start_schedule>k1.end_schedule;
  
   BEGIN
--  err_str:='';
     
   FOR  Peilut1_rec IN  Peilut1 LOOP
BEGIN
INSERT INTO TB_PEILUT_OVDIM (mispar_ishi,taarich,mispar_sidur,  snif_tnua,shat_hatchala_sidur,shat_yetzia,mispar_knisa,
makat_nesia,oto_no,mispar_siduri_oto,kisuy_tor,
 Shat_Bhirat_Nesia_Netzer , Oto_No_Netzer, Mispar_Sidur_Netzer,  Shat_yetzia_Netzer,  Makat_Netzer, Shilut_Netzer ,
 Mikum_Bhirat_Nesia_Netzer ,meadken_acharon  )
  VALUES (Peilut1_rec.driver_id,Peilut1_rec.start_dt,  Peilut1_rec.schedule_num,  Peilut1_rec.branch,
Peilut1_rec.hatchala,Peilut1_rec.gmar,   Peilut1_rec.ride_id, Peilut1_rec.makat_line,Peilut1_rec.bus_number,Peilut1_rec.bus_sequence,
Peilut1_rec.waiting_time, Peilut1_rec.spm_time,Peilut1_rec.spm_bus_number,Peilut1_rec.spm_schedule_num,Peilut1_rec.spm_start_time,
Peilut1_rec.spm_makat_line, Peilut1_rec.spm_line_sign,Peilut1_rec.spm_location, Peilut1_rec.meadken );

EXCEPTION
   WHEN OTHERS THEN
  -- err_str:=err_str||' (4,30) '||to_char(Peilut1_rec.driver_id) ||' '||to_char(Peilut1_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
  	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,30,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Peilut1_rec.driver_id) ||' '||TO_CHAR(Peilut1_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
   END;
END LOOP;
COMMIT;

  
FOR  Peilut2_rec IN  Peilut2 LOOP
BEGIN
  INSERT INTO TB_PEILUT_OVDIM (mispar_ishi,taarich,mispar_sidur,  snif_tnua,shat_hatchala_sidur,shat_yetzia,mispar_knisa,
makat_nesia,oto_no,mispar_siduri_oto,kisuy_tor,
 Shat_Bhirat_Nesia_Netzer , Oto_No_Netzer, Mispar_Sidur_Netzer,  Shat_yetzia_Netzer,  Makat_Netzer, Shilut_Netzer ,
 Mikum_Bhirat_Nesia_Netzer ,meadken_acharon  )
  VALUES (Peilut2_rec.driver_id,Peilut2_rec.start_dt,  Peilut2_rec.schedule_num,  Peilut2_rec.branch,
Peilut2_rec.hatchala,Peilut2_rec.gmar,   Peilut2_rec.ride_id, Peilut2_rec.makat_line,Peilut2_rec.bus_number,Peilut2_rec.bus_sequence,
Peilut2_rec.waiting_time, Peilut2_rec.spm_time,Peilut2_rec.spm_bus_number,Peilut2_rec.spm_schedule_num,Peilut2_rec.spm_start_time,
Peilut2_rec.spm_makat_line, Peilut2_rec.spm_line_sign,Peilut2_rec.spm_location, Peilut2_rec.meadken );

EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,31) '||to_char(Peilut2_rec.driver_id) ||' '||to_char(Peilut2_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
  	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,31,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Peilut2_rec.driver_id) ||' '||TO_CHAR(Peilut2_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
 END;
END LOOP;
COMMIT;


-- 14/12/2010  no more tomorrow 4 Shayah_LeYom_Kodem
FOR  Peilut3_rec IN  Peilut3 LOOP
BEGIN
  INSERT INTO TB_PEILUT_OVDIM (mispar_ishi,taarich,mispar_sidur,  snif_tnua,shat_hatchala_sidur,shat_yetzia,mispar_knisa,
makat_nesia,oto_no,mispar_siduri_oto,kisuy_tor,
Shat_Bhirat_Nesia_Netzer , Oto_No_Netzer, Mispar_Sidur_Netzer,  Shat_yetzia_Netzer,  Makat_Netzer, Shilut_Netzer ,
 Mikum_Bhirat_Nesia_Netzer ,meadken_acharon  )
 VALUES (Peilut3_rec.driver_id,Peilut3_rec.start_dt,  Peilut3_rec.schedule_num,  Peilut3_rec.branch,
Peilut3_rec.hatchala,Peilut3_rec.gmar,   Peilut3_rec.ride_id, Peilut3_rec.makat_line,Peilut3_rec.bus_number,Peilut3_rec.bus_sequence,
Peilut3_rec.waiting_time, Peilut3_rec.spm_time,Peilut3_rec.spm_bus_number,Peilut3_rec.spm_schedule_num,Peilut3_rec.spm_start_time,
Peilut3_rec.spm_makat_line, Peilut3_rec.spm_line_sign,Peilut3_rec.spm_location, Peilut3_rec.meadken );

EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,32) '|| to_char(Peilut3_rec.driver_id) ||' '||to_char(Peilut3_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
  	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,32,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Peilut3_rec.driver_id) ||' '||TO_CHAR(Peilut3_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
  END;
END LOOP;
COMMIT;
   

FOR  Peilut4_rec IN  Peilut4 LOOP
BEGIN
  INSERT INTO TB_PEILUT_OVDIM (mispar_ishi,taarich,mispar_sidur,  snif_tnua,shat_hatchala_sidur,shat_yetzia,mispar_knisa,
makat_nesia,oto_no,mispar_siduri_oto,kisuy_tor,
 Shat_Bhirat_Nesia_Netzer , Oto_No_Netzer, Mispar_Sidur_Netzer,  Shat_yetzia_Netzer,  Makat_Netzer, Shilut_Netzer ,
 Mikum_Bhirat_Nesia_Netzer  ,meadken_acharon )
  VALUES (Peilut4_rec.driver_id,Peilut4_rec.start_dt,  Peilut4_rec.schedule_num,  Peilut4_rec.branch,
Peilut4_rec.hatchala,Peilut4_rec.gmar,   Peilut4_rec.ride_id, Peilut4_rec.makat_line,Peilut4_rec.bus_number,Peilut4_rec.bus_sequence,
Peilut4_rec.waiting_time, Peilut4_rec.spm_time,Peilut4_rec.spm_bus_number,Peilut4_rec.spm_schedule_num,Peilut4_rec.spm_start_time,
Peilut4_rec.spm_makat_line, Peilut4_rec.spm_line_sign,Peilut4_rec.spm_location, Peilut4_rec.meadken );

EXCEPTION
   WHEN OTHERS THEN
 --  err_str:=err_str||' (4,33) '|| to_char(Peilut4_rec.driver_id) ||' '||to_char(Peilut4_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
   	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,33,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Peilut4_rec.driver_id) ||' '||TO_CHAR(Peilut4_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
  END;
END LOOP;
COMMIT;
  


FOR  Peilut6_rec IN  Peilut6 LOOP
BEGIN
 INSERT INTO TB_PEILUT_OVDIM (mispar_ishi,taarich,mispar_sidur,  snif_tnua,shat_hatchala_sidur,shat_yetzia,mispar_knisa,
makat_nesia,oto_no,mispar_siduri_oto,kisuy_tor,
 Shat_Bhirat_Nesia_Netzer , Oto_No_Netzer, Mispar_Sidur_Netzer,  Shat_yetzia_Netzer,  Makat_Netzer, Shilut_Netzer ,
 Mikum_Bhirat_Nesia_Netzer ,meadken_acharon  )
  VALUES (Peilut6_rec.driver_id,Peilut6_rec.start_dt,  Peilut6_rec.schedule_num,  Peilut6_rec.branch,
Peilut6_rec.hatchala,Peilut6_rec.gmar,   Peilut6_rec.ride_id, Peilut6_rec.makat_line,Peilut6_rec.bus_number,Peilut6_rec.bus_sequence,
Peilut6_rec.waiting_time, Peilut6_rec.spm_time,Peilut6_rec.spm_bus_number,Peilut6_rec.spm_schedule_num,Peilut6_rec.spm_start_time,
Peilut6_rec.spm_makat_line, Peilut6_rec.spm_line_sign,Peilut6_rec.spm_location, Peilut6_rec.meadken );

EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,35) '|| to_char(Peilut6_rec.driver_id) ||' '||to_char(Peilut6_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
  	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,35,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Peilut6_rec.driver_id) ||' '||TO_CHAR(Peilut6_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
   END;
END LOOP;
COMMIT;

FOR  Peilut7_rec IN  Peilut7 LOOP
BEGIN
 INSERT INTO TB_PEILUT_OVDIM (mispar_ishi,taarich,mispar_sidur, snif_tnua, shat_hatchala_sidur,shat_yetzia,mispar_knisa,
makat_nesia,oto_no,mispar_siduri_oto,kisuy_tor,
 Shat_Bhirat_Nesia_Netzer , Oto_No_Netzer, Mispar_Sidur_Netzer,  Shat_yetzia_Netzer,  Makat_Netzer, Shilut_Netzer ,
 Mikum_Bhirat_Nesia_Netzer ,meadken_acharon  )
 VALUES (Peilut7_rec.driver_id,Peilut7_rec.start_dt,  Peilut7_rec.schedule_num,  Peilut7_rec.branch,
Peilut7_rec.hatchala,Peilut7_rec.gmar,   Peilut7_rec.ride_id, Peilut7_rec.makat_line,Peilut7_rec.bus_number,Peilut7_rec.bus_sequence,
Peilut7_rec.waiting_time, Peilut7_rec.spm_time,Peilut7_rec.spm_bus_number,Peilut7_rec.spm_schedule_num,Peilut7_rec.spm_start_time,
Peilut7_rec.spm_makat_line, Peilut7_rec.spm_line_sign,Peilut7_rec.spm_location, Peilut7_rec.meadken );

EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,36) '|| to_char(Peilut7_rec.driver_id) ||' '||to_char(Peilut7_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
  	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,36,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Peilut7_rec.driver_id) ||' '||TO_CHAR(Peilut7_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
   END;
END LOOP;
COMMIT;

BEGIN
 UPDATE TB_PEILUT_OVDIM
 SET imut_netzer=1
 WHERE taarich= TO_DATE(pDt,'yyyymmdd')
  AND Oto_No_Netzer>0 ;
 --and (Oto_No_Netzer>0 or Mispar_Sidur_Netzer>0  );
EXCEPTION
   WHEN OTHERS THEN
--      err_str:=err_str||' (4,37)  update imut_peilut  '||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
  	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,37,SYSDATE,'',10,'',SUBSTR('update imut peilut  '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
END;
COMMIT;

BEGIN
 UPDATE TB_PEILUT_OVDIM
SET mispar_matala=mispar_sidur
 WHERE taarich= TO_DATE(pDt,'yyyymmdd')
 AND mispar_sidur< 1000;
 EXCEPTION
   WHEN OTHERS THEN
--      err_str:=err_str||' (4,38)  update matala_peilut  '||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
  	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,38,SYSDATE,'',10,'',SUBSTR('update matala peilut  '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
END;
COMMIT;
 
-- 14/12/2010  no more tomorrow 4 Shayah_LeYom_Kodem update tb_peilut_ovdim
 --todo: only if exists in sidurim shaya_leyom_kodem
-- 14/12/2010  set mispar_matala=mispar_sidur
-- 14/12/2010   where taarich= to_date(pDt,'yyyymmdd')+1
-- 14/12/2010   and mispar_sidur< 1000;
 
FOR  Peilut8_rec IN  Peilut8 LOOP
BEGIN
UPDATE TB_PEILUT_OVDIM
SET teur_nesia=    Peilut8_rec.line_description
WHERE taarich= TO_DATE(pDt,'yyyymmdd')
AND makat_nesia<50000000
AND mispar_knisa>0
AND TRUNC(shat_hatchala_sidur)=taarich
AND TRUNC(shat_yetzia)=taarich
							AND Peilut8_rec.driver_id=mispar_ishi
							AND Peilut8_rec.schedule_num=mispar_sidur
							AND Peilut8_rec.hatchala =shat_hatchala_sidur
							AND Peilut8_rec.moed =shat_yetzia
							AND Peilut8_rec.makat_line=makat_nesia
							AND Peilut8_rec.ride_id=mispar_knisa;

EXCEPTION
   WHEN OTHERS THEN
--    err_str:=err_str||' (4,39)  update_teur_peilut  '||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
  	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,39,SYSDATE,'',10,'',SUBSTR('update teur peilut  '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
END;
END LOOP;
COMMIT;

FOR  Peilut9_rec IN  Peilut9 LOOP
BEGIN
INSERT INTO TB_PEILUT_OVDIM (mispar_ishi,taarich,mispar_sidur,  snif_tnua,shat_hatchala_sidur,shat_yetzia,mispar_knisa,
makat_nesia,oto_no,mispar_siduri_oto,kisuy_tor,
 Shat_Bhirat_Nesia_Netzer , Oto_No_Netzer, Mispar_Sidur_Netzer,  Shat_yetzia_Netzer,  Makat_Netzer, Shilut_Netzer ,
 Mikum_Bhirat_Nesia_Netzer ,meadken_acharon  )
  VALUES (Peilut9_rec.driver_id,Peilut9_rec.start_dt,  Peilut9_rec.schedule_num,  Peilut9_rec.branch,
Peilut9_rec.hatchala,Peilut9_rec.gmar,   Peilut9_rec.ride_id, Peilut9_rec.makat_line,Peilut9_rec.bus_number,Peilut9_rec.bus_sequence,
Peilut9_rec.waiting_time, Peilut9_rec.spm_time,Peilut9_rec.spm_bus_number,Peilut9_rec.spm_schedule_num,Peilut9_rec.spm_start_time,
Peilut9_rec.spm_makat_line, Peilut9_rec.spm_line_sign,Peilut9_rec.spm_location, Peilut9_rec.meadken );

EXCEPTION
   WHEN OTHERS THEN
  -- err_str:=err_str||' (4,30) '||to_char(Peilut1_rec.driver_id) ||' '||to_char(Peilut1_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
  	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,45,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Peilut9_rec.driver_id) ||' '||TO_CHAR(Peilut9_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
   END;
END LOOP;
COMMIT;
 
BEGIN
UPDATE TB_PEILUT_OVDIM
SET teur_nesia=( SELECT    line_description
				 		   FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
						   	WHERE  start_dt= taarich
							AND driver_id=mispar_ishi
							AND schedule_num=mispar_sidur
							AND TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')=shat_hatchala_sidur
							 AND TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(start_time-2400,4,0),1,2)||':'||SUBSTR(LPAD(start_time,4,0),3,2),'yyyymmdd hh24:mi')=shat_yetzia
							AND makat_line=makat_nesia
							AND ride_id=mispar_knisa
							AND start_schedule<2400
							AND start_time>2359
							AND line_description IS NOT NULL
				   			AND ride_id>0
							AND makat_line<50000000)
WHERE taarich= TO_DATE(pDt,'yyyymmdd')
AND makat_nesia<50000000
AND mispar_knisa>0
AND TRUNC(shat_hatchala_sidur)=taarich
AND TRUNC(shat_yetzia)=taarich+1;
EXCEPTION
   WHEN OTHERS THEN
--    err_str:=err_str||' (4,40)  update_peilut  '||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
  	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,40,SYSDATE,'',10,'',SUBSTR('update peilut  '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
END;
COMMIT;
 
BEGIN
UPDATE TB_PEILUT_OVDIM
SET teur_nesia=( SELECT    line_description
				 		   FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
						   	WHERE  start_dt= taarich
							AND driver_id=mispar_ishi
							AND schedule_num=mispar_sidur
							AND TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule-2400,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')=shat_hatchala_sidur
							 AND TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(start_time-2400,4,0),1,2)||':'||SUBSTR(LPAD(start_time,4,0),3,2),'yyyymmdd hh24:mi')=shat_yetzia
							AND makat_line=makat_nesia
							AND ride_id=mispar_knisa
							AND start_schedule>2359
							AND start_time>2359
							AND line_description IS NOT NULL
				   			AND ride_id>0
							AND makat_line<50000000)
WHERE taarich= TO_DATE(pDt,'yyyymmdd')
AND makat_nesia<50000000
AND mispar_knisa>0
AND TRUNC(shat_hatchala_sidur)=taarich+1
AND TRUNC(shat_yetzia)=taarich+1;
EXCEPTION
   WHEN OTHERS THEN
--    err_str:=err_str||' (4,41)  update_peilut  '||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
  	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,41,SYSDATE,'',10,'',SUBSTR('update peilut  '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
END;
COMMIT;
 
BEGIN
UPDATE TB_PEILUT_OVDIM
SET teur_nesia=( SELECT    line_description
				 		   FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
						   	WHERE  start_dt= taarich
							AND driver_id=mispar_ishi
							AND schedule_num=mispar_sidur
							AND TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')=shat_hatchala_sidur
							AND TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_time,4,0),1,2)||':'||SUBSTR(LPAD(start_time,4,0),3,2),'yyyymmdd hh24:mi')=shat_yetzia
							AND makat_line=makat_nesia
							AND ride_id=mispar_knisa
							AND start_schedule<2400
							AND start_time<2400
							AND line_description IS NOT NULL
							AND NVL(makat_line,0)=0
							AND schedule_num<1000)
WHERE taarich= TO_DATE(pDt,'yyyymmdd')
AND NVL(makat_nesia,0)=0
AND mispar_sidur<1000
AND TRUNC(shat_hatchala_sidur)=taarich
AND TRUNC(shat_yetzia)=taarich;
EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,42)  update_peilut  '||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
  	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,42,SYSDATE,'',10,'',SUBSTR('update peilut  '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
END;
COMMIT;
 
BEGIN
UPDATE TB_PEILUT_OVDIM
SET teur_nesia=( SELECT    line_description
				 		   FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
						   	WHERE  start_dt= taarich
							AND driver_id=mispar_ishi
							AND schedule_num=mispar_sidur
							AND TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')=shat_hatchala_sidur
							 AND TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(start_time-2400,4,0),1,2)||':'||SUBSTR(LPAD(start_time,4,0),3,2),'yyyymmdd hh24:mi')=shat_yetzia
							AND makat_line=makat_nesia
							AND ride_id=mispar_knisa
							AND start_schedule<2400
							AND start_time>2359
							AND line_description IS NOT NULL
							AND NVL(makat_line,0)=0
							AND schedule_num<1000)
WHERE taarich= TO_DATE(pDt,'yyyymmdd')
AND NVL(makat_nesia,0)=0
AND mispar_sidur<1000
AND TRUNC(shat_hatchala_sidur)=taarich
AND TRUNC(shat_yetzia)=taarich+1;
EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,43)  update_peilut  '||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
  	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,43,SYSDATE,'',10,'',SUBSTR('update peilut  '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
END;
COMMIT;
 
BEGIN
UPDATE TB_PEILUT_OVDIM
SET teur_nesia=( SELECT    line_description
				 		   FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
						   	WHERE  start_dt= taarich
							AND driver_id=mispar_ishi
							AND schedule_num=mispar_sidur
							AND TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule-2400,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')=shat_hatchala_sidur
							 AND TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(start_time-2400,4,0),1,2)||':'||SUBSTR(LPAD(start_time,4,0),3,2),'yyyymmdd hh24:mi')=shat_yetzia
							AND makat_line=makat_nesia
							AND ride_id=mispar_knisa
							AND start_schedule>2359
							AND start_time>2359
							AND line_description IS NOT NULL
							AND NVL(makat_line,0)=0
							AND schedule_num<1000)
WHERE taarich= TO_DATE(pDt,'yyyymmdd')
AND NVL(makat_nesia,0)=0
AND mispar_sidur<1000
AND TRUNC(shat_hatchala_sidur)=taarich+1
AND TRUNC(shat_yetzia)=taarich+1;
EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,22) '||'update_peilut  '||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
  	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,44,SYSDATE,'',10,'',SUBSTR('update peilut  '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
END;
COMMIT;

 
-- if not  err_str=''  then 
--  raise_application_error(3, SUBSTR     (err_str,1,1000), TRUE);
--  end if;
  
END pro_ins_peilut_4_sidurim;


   PROCEDURE pro_get_sdrm_control(pDt VARCHAR,p_Cur OUT CurType) IS
  BEGIN
  OPEN p_Cur FOR
    SELECT  *
  FROM
 kds.kds_control_driver_activities@kds2sdrm;
  EXCEPTION
   WHEN OTHERS THEN
        RAISE;
      END pro_get_sdrm_control;


   PROCEDURE pro_upd_sdrm_control(pDt VARCHAR) IS
     BEGIN
      UPDATE  kds.kds_control_driver_activities@kds2sdrm
     SET status=9
      WHERE status=1
      AND  start_dt = TO_DATE(pDt,'yyyymmdd') ;
        EXCEPTION
   WHEN OTHERS THEN
INSERT INTO TB_LOG_TAHALICH
	VALUES (4,6,1,SYSDATE,'',10,'',SUBSTR('upd_sdrm '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));   
COMMIT;

 END pro_upd_sdrm_control;
 


  PROCEDURE pro_GetStatusSdrn(pDt VARCHAR, p_cur OUT CurType)  IS
     BEGIN
 OPEN p_cur FOR
 SELECT status
    FROM   kds.kds_control_driver_activities@kds2sdrm
    WHERE start_dt=TO_DATE(pDt,'yyyymmdd');
	 EXCEPTION
   WHEN OTHERS THEN
        RAISE;
 END pro_GetStatusSdrn;



PROCEDURE pro_GetStatus2Sdrn( pAr VARCHAR, p_cur OUT CurType)  IS
     BEGIN
 OPEN p_cur FOR
 SELECT TO_CHAR(start_dt,'yyyymmdd') start_dt
    FROM   kds.kds_control_driver_activities@kds2sdrm
       WHERE start_dt>=SYSDATE-(SELECT erech_param FROM TB_PARAMETRIM WHERE kod_param=100)
	AND status=2
	ORDER BY start_dt;
	EXCEPTION
   WHEN OTHERS THEN
        RAISE;
 END pro_GetStatus2Sdrn;
 
 PROCEDURE pro_GetDtReRunSdrn(pDt VARCHAR, p_cur OUT CurType)  IS
     BEGIN
 OPEN p_cur FOR
 SELECT MAX(taarich),kod_peilut_tahalich
  FROM TB_LOG_TAHALICH
WHERE kod_tahalich=4
AND kod_peilut_tahalich>=8
AND status=1
AND NVL(taarich_sgira,TRUNC(SYSDATE))=TO_DATE(pDt,'yyyymmdd')
GROUP BY kod_peilut_tahalich
ORDER BY MAX(taarich) DESC;
	EXCEPTION
   WHEN OTHERS THEN
        RAISE;
 END pro_GetDtReRunSdrn;
 
 PROCEDURE pro_upd_sdrnRerun_control(pDt VARCHAR) IS
     BEGIN
      UPDATE  kds.kds_control_driver_activities@kds2sdrm
     SET status=9
      WHERE status=2
      AND  start_dt = TO_DATE(pDt,'yyyymmdd') ;
        EXCEPTION
   WHEN OTHERS THEN
        RAISE;
 END pro_upd_sdrnRerun_control;
 
 
 PROCEDURE pro_TrailNDel_peilut_4retrSdrn(pDt VARCHAR) IS
 
   BEGIN
   
   BEGIN
INSERT INTO TRAIL_PEILUT_OVDIM t
(t.MISPAR_ISHI   ,  t.TAARICH  ,  t.MISPAR_sidur  ,  t.Shat_hatchala_sidur	,
  t.Shat_yetzia		,  t.Mispar_knisa		,  t.Makat_nesia	,  t.Oto_no	,  t.Mispar_siduri_oto	 ,  t.Kisuy_tor		,  t.Bitul_O_Hosafa	,  t.Kod_shinuy_premia	  ,
  t.Snif_tnua	   ,  t.Mispar_visa		,  t.Imut_netzer		,  t.Shat_Bhirat_Nesia_Netzer  ,  t.Oto_No_Netzer		,  t.Mispar_Sidur_Netzer	 ,  t.Shat_yetzia_Netzer	,
  t.Makat_Netzer		,  t.Shilut_Netzer		,  t.Mispar_matala	 ,  t.Dakot_bafoal		,  t.Km_visa 	,  t.TAARICH_IDKUN_ACHARON  ,  t.MEADKEN_ACHARON 	,
  t.MISPAR_ISHI_trail   ,  t.TAARICH_IDKUN_trail      ,  t.Sug_peula  	,  
  t.Mikum_Bhirat_Nesia_Netzer ,  t.heara			,  t.Teur_Nesia )
SELECT DISTINCT p.MISPAR_ISHI   ,  p.TAARICH  ,  p.MISPAR_sidur  ,  p.Shat_hatchala_sidur	,
  p.Shat_yetzia		,  p.Mispar_knisa		,  p.Makat_nesia	,  p.Oto_no	,  p.Mispar_siduri_oto	 ,  p.Kisuy_tor		,  p.Bitul_O_Hosafa	,  p.Kod_shinuy_premia	  ,
  p.Snif_tnua	   ,  p.Mispar_visa		,  p.Imut_netzer		,  p.Shat_Bhirat_Nesia_Netzer  ,  p.Oto_No_Netzer		,  p.Mispar_Sidur_Netzer	 ,  p.Shat_yetzia_Netzer	,
  p.Makat_Netzer		,  p.Shilut_Netzer		,  p.Mispar_matala	 ,  p.Dakot_bafoal		,  p.Km_visa 	,  p.TAARICH_IDKUN_ACHARON  ,  p.MEADKEN_ACHARON 	,
 77690  ,  SYSDATE      ,  8 	,  
  p.Mikum_Bhirat_Nesia_Netzer ,  p.heara			,  p.Teur_Nesia  
FROM TB_PEILUT_OVDIM p, TB_YAMEY_AVODA_OVDIM y
WHERE p.taarich =   TO_DATE(pDt,'yyyymmdd')  
 AND (p.mispar_sidur < 99000 OR p.mispar_sidur> 99999)
 AND  y.taarich =   TO_DATE(pDt,'yyyymmdd')  
AND y.taarich  = p.taarich
AND y.mispar_ishi=p.mispar_ishi
AND y.measher_o_mistayeg IS NULL
AND  NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s
 	 				  	   WHERE s.taarich =   TO_DATE(pDt,'yyyymmdd')  
						   AND s.taarich  = p.taarich
						   AND s.mispar_ishi=p.mispar_ishi
						   AND s.mispar_sidur=p.mispar_sidur
						   AND p.shat_hatchala_sidur=s.shat_hatchala
						   AND s.shayah_leyom_kodem=1)    ;
						   EXCEPTION
   WHEN OTHERS THEN
 	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,1,SYSDATE,'',10,'',SUBSTR('trail_peilut '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
END;
COMMIT;

BEGIN						   
 INSERT INTO TRAIL_PEILUT_OVDIM t
(t.MISPAR_ISHI   ,  t.TAARICH  ,  t.MISPAR_sidur  ,  t.Shat_hatchala_sidur	,
  t.Shat_yetzia		,  t.Mispar_knisa		,  t.Makat_nesia	,  t.Oto_no	,  t.Mispar_siduri_oto	 ,  t.Kisuy_tor		,  t.Bitul_O_Hosafa	,  t.Kod_shinuy_premia	  ,
  t.Snif_tnua	   ,  t.Mispar_visa		,  t.Imut_netzer		,  t.Shat_Bhirat_Nesia_Netzer  ,  t.Oto_No_Netzer		,  t.Mispar_Sidur_Netzer	 ,  t.Shat_yetzia_Netzer	,
  t.Makat_Netzer		,  t.Shilut_Netzer		,  t.Mispar_matala	 ,  t.Dakot_bafoal		,  t.Km_visa 	,  t.TAARICH_IDKUN_ACHARON  ,  t.MEADKEN_ACHARON 	,
  t.MISPAR_ISHI_trail   ,  t.TAARICH_IDKUN_trail      ,  t.Sug_peula  	,  
  t.Mikum_Bhirat_Nesia_Netzer ,  t.heara			,  t.Teur_Nesia )
SELECT DISTINCT p.MISPAR_ISHI   ,  p.TAARICH  ,  p.MISPAR_sidur  ,  p.Shat_hatchala_sidur	,
  p.Shat_yetzia		,  p.Mispar_knisa		,  p.Makat_nesia	,  p.Oto_no	,  p.Mispar_siduri_oto	 ,  p.Kisuy_tor		,  p.Bitul_O_Hosafa	,  p.Kod_shinuy_premia	  ,
  p.Snif_tnua	   ,  p.Mispar_visa		,  p.Imut_netzer		,  p.Shat_Bhirat_Nesia_Netzer  ,  p.Oto_No_Netzer		,  p.Mispar_Sidur_Netzer	 ,  p.Shat_yetzia_Netzer	,
  p.Makat_Netzer		,  p.Shilut_Netzer		,  p.Mispar_matala	 ,  p.Dakot_bafoal		,  p.Km_visa 	,  p.TAARICH_IDKUN_ACHARON  ,  p.MEADKEN_ACHARON 	,
 77690  ,  SYSDATE      ,  8 	,  
  p.Mikum_Bhirat_Nesia_Netzer ,  p.heara			,  p.Teur_Nesia  
FROM TB_PEILUT_OVDIM p, TB_YAMEY_AVODA_OVDIM y
WHERE p.taarich =   TO_DATE(pDt,'yyyymmdd')  
 AND (p.mispar_sidur < 99000 OR p.mispar_sidur> 99999)
 AND  y.taarich =  TO_DATE(pDt,'yyyymmdd')    
AND y.taarich  = p.taarich
AND y.mispar_ishi=p.mispar_ishi
AND y.measher_o_mistayeg =1
AND  NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s
 	 				  	   WHERE s.taarich =   TO_DATE(pDt,'yyyymmdd')    
						   AND s.taarich  = p.taarich
						   AND s.mispar_ishi=p.mispar_ishi
						   AND s.mispar_sidur=p.mispar_sidur
						   AND s.shayah_leyom_kodem=1
						   AND p.shat_hatchala_sidur=s.shat_hatchala
						   AND (s.mispar_sidur < 99000 OR s.mispar_sidur> 99999));
						   EXCEPTION
   WHEN OTHERS THEN
 	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,2,SYSDATE,'',10,'',SUBSTR('trail_peilut '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
END;
COMMIT;
						   
BEGIN
 INSERT INTO TRAIL_PEILUT_OVDIM t
(t.MISPAR_ISHI   ,  t.TAARICH  ,  t.MISPAR_sidur  ,  t.Shat_hatchala_sidur	,
  t.Shat_yetzia		,  t.Mispar_knisa		,  t.Makat_nesia	,  t.Oto_no	,  t.Mispar_siduri_oto	 ,  t.Kisuy_tor		,  t.Bitul_O_Hosafa	,  t.Kod_shinuy_premia	  ,
  t.Snif_tnua	   ,  t.Mispar_visa		,  t.Imut_netzer		,  t.Shat_Bhirat_Nesia_Netzer  ,  t.Oto_No_Netzer		,  t.Mispar_Sidur_Netzer	 ,  t.Shat_yetzia_Netzer	,
  t.Makat_Netzer		,  t.Shilut_Netzer		,  t.Mispar_matala	 ,  t.Dakot_bafoal		,  t.Km_visa 	,  t.TAARICH_IDKUN_ACHARON  ,  t.MEADKEN_ACHARON 	,
  t.MISPAR_ISHI_trail   ,  t.TAARICH_IDKUN_trail      ,  t.Sug_peula  	,  
  t.Mikum_Bhirat_Nesia_Netzer ,  t.heara			,  t.Teur_Nesia )
SELECT DISTINCT p.MISPAR_ISHI   ,  p.TAARICH  ,  p.MISPAR_sidur  ,  p.Shat_hatchala_sidur	,
  p.Shat_yetzia		,  p.Mispar_knisa		,  p.Makat_nesia	,  p.Oto_no	,  p.Mispar_siduri_oto	 ,  p.Kisuy_tor		,  p.Bitul_O_Hosafa	,  p.Kod_shinuy_premia	  ,
  p.Snif_tnua	   ,  p.Mispar_visa		,  p.Imut_netzer		,  p.Shat_Bhirat_Nesia_Netzer  ,  p.Oto_No_Netzer		,  p.Mispar_Sidur_Netzer	 ,  p.Shat_yetzia_Netzer	,
  p.Makat_Netzer		,  p.Shilut_Netzer		,  p.Mispar_matala	 ,  p.Dakot_bafoal		,  p.Km_visa 	,  p.TAARICH_IDKUN_ACHARON  ,  p.MEADKEN_ACHARON 	,
 77690  ,  SYSDATE      ,  8 	,  
  p.Mikum_Bhirat_Nesia_Netzer ,  p.heara			,  p.Teur_Nesia  
FROM TB_PEILUT_OVDIM p, TB_YAMEY_AVODA_OVDIM y,TB_SIDURIM_OVDIM s
WHERE p.taarich =   TO_DATE(pDt,'yyyymmdd')  +1
 AND (p.mispar_sidur < 99000 OR p.mispar_sidur> 99999)
 AND  y.taarich =   TO_DATE(pDt,'yyyymmdd')  +1
AND y.taarich  = p.taarich
AND y.mispar_ishi=p.mispar_ishi
AND y.measher_o_mistayeg IS NULL
AND s.taarich =   TO_DATE(pDt,'yyyymmdd')  +1
AND s.taarich  = p.taarich
AND s.mispar_ishi=p.mispar_ishi
AND s.mispar_sidur=p.mispar_sidur
AND p.shat_hatchala_sidur=s.shat_hatchala
AND s.shayah_leyom_kodem=1;
						   EXCEPTION
   WHEN OTHERS THEN
 	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,3,SYSDATE,'',10,'',SUBSTR('trail_peilut '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
END;
COMMIT;

BEGIN
 INSERT INTO TRAIL_PEILUT_OVDIM t
(t.MISPAR_ISHI   ,  t.TAARICH  ,  t.MISPAR_sidur  ,  t.Shat_hatchala_sidur	,
  t.Shat_yetzia		,  t.Mispar_knisa		,  t.Makat_nesia	,  t.Oto_no	,  t.Mispar_siduri_oto	 ,  t.Kisuy_tor		,  t.Bitul_O_Hosafa	,  t.Kod_shinuy_premia	  ,
  t.Snif_tnua	   ,  t.Mispar_visa		,  t.Imut_netzer		,  t.Shat_Bhirat_Nesia_Netzer  ,  t.Oto_No_Netzer		,  t.Mispar_Sidur_Netzer	 ,  t.Shat_yetzia_Netzer	,
  t.Makat_Netzer		,  t.Shilut_Netzer		,  t.Mispar_matala	 ,  t.Dakot_bafoal		,  t.Km_visa 	,  t.TAARICH_IDKUN_ACHARON  ,  t.MEADKEN_ACHARON 	,
  t.MISPAR_ISHI_trail   ,  t.TAARICH_IDKUN_trail      ,  t.Sug_peula  	,  
  t.Mikum_Bhirat_Nesia_Netzer ,  t.heara			,  t.Teur_Nesia )
SELECT DISTINCT p.MISPAR_ISHI   ,  p.TAARICH  ,  p.MISPAR_sidur  ,  p.Shat_hatchala_sidur	,
  p.Shat_yetzia		,  p.Mispar_knisa		,  p.Makat_nesia	,  p.Oto_no	,  p.Mispar_siduri_oto	 ,  p.Kisuy_tor		,  p.Bitul_O_Hosafa	,  p.Kod_shinuy_premia	  ,
  p.Snif_tnua	   ,  p.Mispar_visa		,  p.Imut_netzer		,  p.Shat_Bhirat_Nesia_Netzer  ,  p.Oto_No_Netzer		,  p.Mispar_Sidur_Netzer	 ,  p.Shat_yetzia_Netzer	,
  p.Makat_Netzer		,  p.Shilut_Netzer		,  p.Mispar_matala	 ,  p.Dakot_bafoal		,  p.Km_visa 	,  p.TAARICH_IDKUN_ACHARON  ,  p.MEADKEN_ACHARON 	,
 77690  ,  SYSDATE      ,  8 	,  
  p.Mikum_Bhirat_Nesia_Netzer ,  p.heara			,  p.Teur_Nesia  
FROM TB_PEILUT_OVDIM p, TB_YAMEY_AVODA_OVDIM y, TB_SIDURIM_OVDIM s
WHERE p.taarich =   TO_DATE(pDt,'yyyymmdd')  +1 
 AND (p.mispar_sidur < 99000 OR p.mispar_sidur> 99999)
 AND  y.taarich =  TO_DATE(pDt,'yyyymmdd')  +1   
AND y.taarich  = p.taarich
AND y.mispar_ishi=p.mispar_ishi
AND y.measher_o_mistayeg =1
AND  s.taarich =   TO_DATE(pDt,'yyyymmdd')  +1   
AND s.taarich  = p.taarich
AND s.mispar_ishi=p.mispar_ishi
AND s.mispar_sidur=p.mispar_sidur
AND p.shat_hatchala_sidur=s.shat_hatchala
AND s.shayah_leyom_kodem=1
AND  NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s2
 	 				  	   WHERE s2.taarich =   TO_DATE(pDt,'yyyymmdd')  +1    
						   AND s2.taarich  = p.taarich
						   AND s2.mispar_ishi=p.mispar_ishi
						   AND s2.mispar_sidur=p.mispar_sidur
						    AND (s2.mispar_sidur < 99000 OR s2.mispar_sidur> 99999)
							AND p.shat_hatchala_sidur=s2.shat_hatchala
						   AND s2.shayah_leyom_kodem=1);
						   EXCEPTION
   WHEN OTHERS THEN
 	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,4,SYSDATE,'',10,'',SUBSTR('trail_peilut '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
END;
COMMIT;

BEGIN						   
DELETE FROM TB_PEILUT_OVDIM p 
WHERE p.taarich =   TO_DATE(pDt,'yyyymmdd')  
 AND (p.mispar_sidur < 99000 OR p.mispar_sidur> 99999)
 AND EXISTS (SELECT * FROM  TB_YAMEY_AVODA_OVDIM y
 WHERE  y.taarich =   TO_DATE(pDt,'yyyymmdd')  
AND y.taarich  = p.taarich
AND y.mispar_ishi=p.mispar_ishi
AND y.measher_o_mistayeg IS NULL)
AND  NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s
 	 				  	   WHERE s.taarich =   TO_DATE(pDt,'yyyymmdd')  
						   AND s.taarich  = p.taarich
						   AND s.mispar_ishi=p.mispar_ishi
						   AND s.mispar_sidur=p.mispar_sidur
						   AND p.shat_hatchala_sidur=s.shat_hatchala
						   AND s.shayah_leyom_kodem=1)  ;
						   EXCEPTION
   WHEN OTHERS THEN
 	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,5,SYSDATE,'',10,'',SUBSTR('trail_peilut '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
END;
COMMIT;

BEGIN						   
DELETE FROM TB_PEILUT_OVDIM p 
WHERE p.taarich =   TO_DATE(pDt,'yyyymmdd')  
 AND (p.mispar_sidur < 99000 OR p.mispar_sidur> 99999)
 AND EXISTS (SELECT * FROM   TB_YAMEY_AVODA_OVDIM y
WHERE   y.taarich =  TO_DATE(pDt,'yyyymmdd')    
AND y.taarich  = p.taarich
AND y.mispar_ishi=p.mispar_ishi
AND y.measher_o_mistayeg =1)
AND  NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s
 	 				  	   WHERE s.taarich =   TO_DATE(pDt,'yyyymmdd')    
						   AND s.taarich  = p.taarich
						   AND s.mispar_ishi=p.mispar_ishi
						   AND s.mispar_sidur=p.mispar_sidur
						   AND s.shayah_leyom_kodem=1
						   AND p.shat_hatchala_sidur=s.shat_hatchala
						   AND (s.mispar_sidur < 99000 OR s.mispar_sidur> 99999));
						   EXCEPTION
   WHEN OTHERS THEN
 	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,6,SYSDATE,'',10,'',SUBSTR('trail_peilut '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
END;
COMMIT;

BEGIN						   
DELETE FROM TB_PEILUT_OVDIM p 
WHERE p.taarich =   TO_DATE(pDt,'yyyymmdd')  +1
 AND (p.mispar_sidur < 99000 OR p.mispar_sidur> 99999)
 AND EXISTS (SELECT * FROM  TB_YAMEY_AVODA_OVDIM y  
WHERE   y.taarich =   TO_DATE(pDt,'yyyymmdd')  +1
AND y.taarich  = p.taarich
AND y.mispar_ishi=p.mispar_ishi
AND y.measher_o_mistayeg IS NULL)
AND EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s
WHERE  s.taarich =   TO_DATE(pDt,'yyyymmdd')  +1
AND s.taarich  = p.taarich
AND s.mispar_ishi=p.mispar_ishi
AND s.mispar_sidur=p.mispar_sidur
AND p.shat_hatchala_sidur=s.shat_hatchala
AND s.shayah_leyom_kodem=1);
						   EXCEPTION
   WHEN OTHERS THEN
 	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,7,SYSDATE,'',10,'',SUBSTR('trail_peilut '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
END;
COMMIT;

BEGIN
DELETE FROM TB_PEILUT_OVDIM p
WHERE p.taarich =   TO_DATE(pDt,'yyyymmdd')  +1 
 AND (p.mispar_sidur < 99000 OR p.mispar_sidur> 99999)
 AND EXISTS (SELECT * FROM TB_YAMEY_AVODA_OVDIM y 
WHERE  y.taarich =  TO_DATE(pDt,'yyyymmdd')  +1   
AND y.taarich  = p.taarich
AND y.mispar_ishi=p.mispar_ishi
AND y.measher_o_mistayeg =1)
AND EXISTS (SELECT * FROM  TB_SIDURIM_OVDIM s
WHERE   s.taarich =   TO_DATE(pDt,'yyyymmdd')  +1   
AND s.taarich  = p.taarich
AND s.mispar_ishi=p.mispar_ishi
AND s.mispar_sidur=p.mispar_sidur
AND p.shat_hatchala_sidur=s.shat_hatchala
AND s.shayah_leyom_kodem=1)
AND  NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s2
 	 				  	   WHERE s2.taarich =   TO_DATE(pDt,'yyyymmdd')  +1    
						   AND s2.taarich  = p.taarich
						   AND s2.mispar_ishi=p.mispar_ishi
						   AND s2.mispar_sidur=p.mispar_sidur
						    AND (s2.mispar_sidur < 99000 OR s2.mispar_sidur> 99999)
							AND p.shat_hatchala_sidur=s2.shat_hatchala
						   AND s2.shayah_leyom_kodem=1);
							   EXCEPTION
   WHEN OTHERS THEN
 	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,8,SYSDATE,'',10,'',SUBSTR('trail_peilut '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
END;
COMMIT;

		END  pro_TrailNDel_peilut_4retrSdrn;

		
	PROCEDURE pro_TrailNDel_sidurim_4reSdrn(pDt VARCHAR) IS

   BEGIN
   
   BEGIN
   	 INSERT INTO TRAIL_SIDURIM_OVDIM t
 ( MISPAR_ISHI  ,  MISPAR_sidur    ,  TAARICH ,  Shat_hatchala		,  
 Shat_gmar		,  Shat_hatchala_letashlum ,  Shat_gmar_letashlum	,  Pitzul_hafsaka	,  Chariga	,  Tosefet_Grira	,  Hashlama,  sug_hashlama 		,
  Yom_Visa		,  Lo_letashlum	,  Out_michsa	,  Mikum_shaon_knisa	 ,  Mikum_shaon_yetzia	,  Achuz_Knas_LePremyat_Visa	,  Achuz_Viza_Besikun	,
  Mispar_Musach_O_Machsan	,  Kod_siba_lo_letashlum		,  Kod_siba_ledivuch_yadani_in	,  Kod_siba_ledivuch_yadani_out	,  Menahel_Musach_Meadken	,
  Shayah_LeYom_Kodem 	,  Mispar_shiurey_nehiga	,  Mezake_Halbasha 	,  Mezake_nesiot		,  Sug_Hazmanat_Visa ,  Bitul_O_Hosafa	,
  tafkid_visa 		,  mivtza_visa 		,  Nidreshet_hitiatzvut 		,  Shat_hitiatzvut	,  Ptor_Mehitiatzvut 	,  Hachtama_Beatar_Lo_Takin  ,  Hafhatat_Nochechut_Visa	,
  Sector_Visa 		,  MEADKEN_ACHARON 	 ,  TAARICH_IDKUN_ACHARON   ,  MISPAR_ISHI_trail     ,  TAARICH_IDKUN_trail   , Sug_peula , heara	,sug_sidur	 )
SELECT DISTINCT p.MISPAR_ISHI  ,  MISPAR_sidur    ,  p.TAARICH ,  p.Shat_hatchala		,  
  Shat_gmar		,  Shat_hatchala_letashlum ,  Shat_gmar_letashlum	,  Pitzul_hafsaka	,  Chariga	,  Tosefet_Grira	,  Hashlama,  sug_hashlama 		,
  Yom_Visa		,  Lo_letashlum	,  Out_michsa	,  Mikum_shaon_knisa	 ,  Mikum_shaon_yetzia	,  Achuz_Knas_LePremyat_Visa	,  Achuz_Viza_Besikun	,
  Mispar_Musach_O_Machsan	,  Kod_siba_lo_letashlum		,  Kod_siba_ledivuch_yadani_in	,  Kod_siba_ledivuch_yadani_out	,  Menahel_Musach_Meadken	,
  Shayah_LeYom_Kodem 	,  Mispar_shiurey_nehiga	,  Mezake_Halbasha 	,  Mezake_nesiot		,    Sug_Hazmanat_Visa ,  Bitul_O_Hosafa	,
  tafkid_visa 		,  mivtza_visa 		,  Nidreshet_hitiatzvut 		,  Shat_hitiatzvut	,  Ptor_Mehitiatzvut 	,  Hachtama_Beatar_Lo_Takin  ,  Hafhatat_Nochechut_Visa	,
  Sector_Visa 		,  p.MEADKEN_ACHARON 	 ,  p.TAARICH_IDKUN_ACHARON  ,   77690  ,  SYSDATE  ,8    ,  p.heara 	,p.sug_sidur
FROM TB_SIDURIM_OVDIM p, TB_YAMEY_AVODA_OVDIM y
WHERE p.taarich =   TO_DATE(pDt,'yyyymmdd')  
 AND (p.mispar_sidur < 99000 OR p.mispar_sidur> 99999)
 AND  y.taarich =   TO_DATE(pDt,'yyyymmdd')  
AND y.taarich  = p.taarich
AND y.mispar_ishi=p.mispar_ishi
AND y.measher_o_mistayeg IS NULL
AND NVL(p.shayah_leyom_kodem,0)=0 ;
						   EXCEPTION
   WHEN OTHERS THEN
 	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,10,SYSDATE,'',10,'',SUBSTR('trail_sidurim '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
END;
COMMIT;

BEGIN
  	 INSERT INTO TRAIL_SIDURIM_OVDIM t
 ( MISPAR_ISHI  ,  MISPAR_sidur    ,  TAARICH ,  Shat_hatchala		,  
 Shat_gmar		,  Shat_hatchala_letashlum ,  Shat_gmar_letashlum	,  Pitzul_hafsaka	,  Chariga	,  Tosefet_Grira	,  Hashlama,  sug_hashlama 		,
  Yom_Visa		,  Lo_letashlum	,  Out_michsa	,  Mikum_shaon_knisa	 ,  Mikum_shaon_yetzia	,  Achuz_Knas_LePremyat_Visa	,  Achuz_Viza_Besikun	,
  Mispar_Musach_O_Machsan	,  Kod_siba_lo_letashlum		,  Kod_siba_ledivuch_yadani_in	,  Kod_siba_ledivuch_yadani_out	,  Menahel_Musach_Meadken	,
  Shayah_LeYom_Kodem 	,  Mispar_shiurey_nehiga	,  Mezake_Halbasha 	,  Mezake_nesiot		,  Sug_Hazmanat_Visa ,  Bitul_O_Hosafa	,
  tafkid_visa 		,  mivtza_visa 		,  Nidreshet_hitiatzvut 		,  Shat_hitiatzvut	,  Ptor_Mehitiatzvut 	,  Hachtama_Beatar_Lo_Takin  ,  Hafhatat_Nochechut_Visa	,
  Sector_Visa 		,  MEADKEN_ACHARON 	 ,  TAARICH_IDKUN_ACHARON   ,  MISPAR_ISHI_trail     ,  TAARICH_IDKUN_trail   , Sug_peula , heara		,sug_sidur	 )
SELECT DISTINCT p.MISPAR_ISHI  ,  MISPAR_sidur    ,  p.TAARICH ,  p.Shat_hatchala		,  
  Shat_gmar		,  Shat_hatchala_letashlum ,  Shat_gmar_letashlum	,  Pitzul_hafsaka	,  Chariga	,  Tosefet_Grira	,  Hashlama,  sug_hashlama 		,
  Yom_Visa		,  Lo_letashlum	,  Out_michsa	,  Mikum_shaon_knisa	 ,  Mikum_shaon_yetzia	,  Achuz_Knas_LePremyat_Visa	,  Achuz_Viza_Besikun	,
  Mispar_Musach_O_Machsan	,  Kod_siba_lo_letashlum		,  Kod_siba_ledivuch_yadani_in	,  Kod_siba_ledivuch_yadani_out	,  Menahel_Musach_Meadken	,
  Shayah_LeYom_Kodem 	,  Mispar_shiurey_nehiga	,  Mezake_Halbasha 	,  Mezake_nesiot		,    Sug_Hazmanat_Visa ,  Bitul_O_Hosafa	,
  tafkid_visa 		,  mivtza_visa 		,  Nidreshet_hitiatzvut 		,  Shat_hitiatzvut	,  Ptor_Mehitiatzvut 	,  Hachtama_Beatar_Lo_Takin  ,  Hafhatat_Nochechut_Visa	,
  Sector_Visa 		,  p.MEADKEN_ACHARON 	 ,  p.TAARICH_IDKUN_ACHARON  ,   77690  ,  SYSDATE  ,8    ,  p.heara,p.sug_sidur
FROM TB_SIDURIM_OVDIM p, TB_YAMEY_AVODA_OVDIM y
WHERE p.taarich =   TO_DATE(pDt,'yyyymmdd')  
 AND (p.mispar_sidur < 99000 OR p.mispar_sidur> 99999)
 AND  y.taarich =  TO_DATE(pDt,'yyyymmdd')    
AND y.taarich  = p.taarich
AND y.mispar_ishi=p.mispar_ishi
AND y.measher_o_mistayeg =1
AND   NVL(p.shayah_leyom_kodem,0)=0
AND  NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s2
 	 				  	   WHERE s2.taarich =   TO_DATE(pDt,'yyyymmdd')    
						   AND s2.taarich  = p.taarich
						   AND s2.mispar_ishi=p.mispar_ishi
						   AND s2.mispar_sidur=p.mispar_sidur
						    AND (s2.mispar_sidur < 99000 OR s2.mispar_sidur> 99999)
						   AND NVL(s2.shayah_leyom_kodem,0)=0);
						   EXCEPTION
   WHEN OTHERS THEN
 	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,11,SYSDATE,'',10,'',SUBSTR('trail_sidurim '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
END;
COMMIT;

 BEGIN
  	 INSERT INTO TRAIL_SIDURIM_OVDIM t
 ( MISPAR_ISHI  ,  MISPAR_sidur    ,  TAARICH ,  Shat_hatchala		,  
 Shat_gmar		,  Shat_hatchala_letashlum ,  Shat_gmar_letashlum	,  Pitzul_hafsaka	,  Chariga	,  Tosefet_Grira	,  Hashlama,  sug_hashlama 		,
  Yom_Visa		,  Lo_letashlum	,  Out_michsa	,  Mikum_shaon_knisa	 ,  Mikum_shaon_yetzia	,  Achuz_Knas_LePremyat_Visa	,  Achuz_Viza_Besikun	,
  Mispar_Musach_O_Machsan	,  Kod_siba_lo_letashlum		,  Kod_siba_ledivuch_yadani_in	,  Kod_siba_ledivuch_yadani_out	,  Menahel_Musach_Meadken	,
  Shayah_LeYom_Kodem 	,  Mispar_shiurey_nehiga	,  Mezake_Halbasha 	,  Mezake_nesiot		,  Sug_Hazmanat_Visa ,  Bitul_O_Hosafa	,
  tafkid_visa 		,  mivtza_visa 		,  Nidreshet_hitiatzvut 		,  Shat_hitiatzvut	,  Ptor_Mehitiatzvut 	,  Hachtama_Beatar_Lo_Takin  ,  Hafhatat_Nochechut_Visa	,
  Sector_Visa 		,  MEADKEN_ACHARON 	 ,  TAARICH_IDKUN_ACHARON   ,  MISPAR_ISHI_trail     ,  TAARICH_IDKUN_trail   , Sug_peula , heara	,sug_sidur	 )
SELECT DISTINCT p.MISPAR_ISHI  ,  MISPAR_sidur    ,  p.TAARICH ,  p.Shat_hatchala		,  
  Shat_gmar		,  Shat_hatchala_letashlum ,  Shat_gmar_letashlum	,  Pitzul_hafsaka	,  Chariga	,  Tosefet_Grira	,  Hashlama,  sug_hashlama 		,
  Yom_Visa		,  Lo_letashlum	,  Out_michsa	,  Mikum_shaon_knisa	 ,  Mikum_shaon_yetzia	,  Achuz_Knas_LePremyat_Visa	,  Achuz_Viza_Besikun	,
  Mispar_Musach_O_Machsan	,  Kod_siba_lo_letashlum		,  Kod_siba_ledivuch_yadani_in	,  Kod_siba_ledivuch_yadani_out	,  Menahel_Musach_Meadken	,
  Shayah_LeYom_Kodem 	,  Mispar_shiurey_nehiga	,  Mezake_Halbasha 	,  Mezake_nesiot		,    Sug_Hazmanat_Visa ,  Bitul_O_Hosafa	,
  tafkid_visa 		,  mivtza_visa 		,  Nidreshet_hitiatzvut 		,  Shat_hitiatzvut	,  Ptor_Mehitiatzvut 	,  Hachtama_Beatar_Lo_Takin  ,  Hafhatat_Nochechut_Visa	,
  Sector_Visa 		,  p.MEADKEN_ACHARON 	 ,  p.TAARICH_IDKUN_ACHARON  ,   77690  ,  SYSDATE  ,8    ,  p.heara, p.sug_sidur
FROM TB_SIDURIM_OVDIM p, TB_YAMEY_AVODA_OVDIM y
WHERE p.taarich =   TO_DATE(pDt,'yyyymmdd')  +1
 AND (p.mispar_sidur < 99000 OR p.mispar_sidur> 99999)
 AND  y.taarich =   TO_DATE(pDt,'yyyymmdd') +1 
AND y.taarich  = p.taarich
AND y.mispar_ishi=p.mispar_ishi
AND y.measher_o_mistayeg IS NULL
AND p.shayah_leyom_kodem=1 ;
						   EXCEPTION
   WHEN OTHERS THEN
 	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,12,SYSDATE,'',10,'',SUBSTR('trail_sidurim '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
END;
COMMIT;

 BEGIN
  	 INSERT INTO TRAIL_SIDURIM_OVDIM t
 ( MISPAR_ISHI  ,  MISPAR_sidur    ,  TAARICH ,  Shat_hatchala		,  
 Shat_gmar		,  Shat_hatchala_letashlum ,  Shat_gmar_letashlum	,  Pitzul_hafsaka	,  Chariga	,  Tosefet_Grira	,  Hashlama,  sug_hashlama 		,
  Yom_Visa		,  Lo_letashlum	,  Out_michsa	,  Mikum_shaon_knisa	 ,  Mikum_shaon_yetzia	,  Achuz_Knas_LePremyat_Visa	,  Achuz_Viza_Besikun	,
  Mispar_Musach_O_Machsan	,  Kod_siba_lo_letashlum		,  Kod_siba_ledivuch_yadani_in	,  Kod_siba_ledivuch_yadani_out	,  Menahel_Musach_Meadken	,
  Shayah_LeYom_Kodem 	,  Mispar_shiurey_nehiga	,  Mezake_Halbasha 	,  Mezake_nesiot		,  Sug_Hazmanat_Visa ,  Bitul_O_Hosafa	,
  tafkid_visa 		,  mivtza_visa 		,  Nidreshet_hitiatzvut 		,  Shat_hitiatzvut	,  Ptor_Mehitiatzvut 	,  Hachtama_Beatar_Lo_Takin  ,  Hafhatat_Nochechut_Visa	,
  Sector_Visa 		,  MEADKEN_ACHARON 	 ,  TAARICH_IDKUN_ACHARON   ,  MISPAR_ISHI_trail     ,  TAARICH_IDKUN_trail   , Sug_peula , heara		,sug_sidur	 )
SELECT DISTINCT p.MISPAR_ISHI  ,  MISPAR_sidur    ,  p.TAARICH ,  p.Shat_hatchala		,  
  Shat_gmar		,  Shat_hatchala_letashlum ,  Shat_gmar_letashlum	,  Pitzul_hafsaka	,  Chariga	,  Tosefet_Grira	,  Hashlama,  sug_hashlama 		,
  Yom_Visa		,  Lo_letashlum	,  Out_michsa	,  Mikum_shaon_knisa	 ,  Mikum_shaon_yetzia	,  Achuz_Knas_LePremyat_Visa	,  Achuz_Viza_Besikun	,
  Mispar_Musach_O_Machsan	,  Kod_siba_lo_letashlum		,  Kod_siba_ledivuch_yadani_in	,  Kod_siba_ledivuch_yadani_out	,  Menahel_Musach_Meadken	,
  Shayah_LeYom_Kodem 	,  Mispar_shiurey_nehiga	,  Mezake_Halbasha 	,  Mezake_nesiot		,    Sug_Hazmanat_Visa ,  Bitul_O_Hosafa	,
  tafkid_visa 		,  mivtza_visa 		,  Nidreshet_hitiatzvut 		,  Shat_hitiatzvut	,  Ptor_Mehitiatzvut 	,  Hachtama_Beatar_Lo_Takin  ,  Hafhatat_Nochechut_Visa	,
  Sector_Visa 		,  p.MEADKEN_ACHARON 	 ,  p.TAARICH_IDKUN_ACHARON  ,   77690  ,  SYSDATE  ,8    ,  p.heara	,p.sug_sidur
FROM TB_SIDURIM_OVDIM p, TB_YAMEY_AVODA_OVDIM y
WHERE p.taarich =   TO_DATE(pDt,'yyyymmdd')  +1
 AND (p.mispar_sidur < 99000 OR p.mispar_sidur> 99999)
 AND  y.taarich =  TO_DATE(pDt,'yyyymmdd')    +1
AND y.taarich  = p.taarich
AND y.mispar_ishi=p.mispar_ishi
AND y.measher_o_mistayeg =1
AND    p.shayah_leyom_kodem=1
AND  NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s2
 	 				  	   WHERE s2.taarich =   TO_DATE(pDt,'yyyymmdd')    +1
						   AND s2.taarich  = p.taarich
						   AND s2.mispar_ishi=p.mispar_ishi
						   AND s2.mispar_sidur=p.mispar_sidur
						    AND (s2.mispar_sidur < 99000 OR s2.mispar_sidur> 99999)
						   AND s2.shayah_leyom_kodem=1);
						   EXCEPTION
   WHEN OTHERS THEN
 	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,13,SYSDATE,'',10,'',SUBSTR('trail_sidurim '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
END;
COMMIT;
						   
BEGIN
DELETE FROM  TB_SIDURIM_OVDIM p 
WHERE p.taarich =   TO_DATE(pDt,'yyyymmdd')  
 AND (p.mispar_sidur < 99000 OR p.mispar_sidur> 99999)
 AND NVL(p.shayah_leyom_kodem,0)=0
 AND  EXISTS (SELECT * FROM  TB_YAMEY_AVODA_OVDIM y
 WHERE y.taarich =   TO_DATE(pDt,'yyyymmdd')  
AND y.taarich  = p.taarich
AND y.mispar_ishi=p.mispar_ishi
AND y.measher_o_mistayeg IS NULL );
						   EXCEPTION
   WHEN OTHERS THEN
 	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,14,SYSDATE,'',10,'',SUBSTR('trail_sidurim '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
END;
COMMIT;

BEGIN
 DELETE  FROM TB_SIDURIM_OVDIM p 
WHERE p.taarich =   TO_DATE(pDt,'yyyymmdd')  
 AND (p.mispar_sidur < 99000 OR p.mispar_sidur> 99999)
 AND   NVL(p.shayah_leyom_kodem,0)=0
 AND  EXISTS (SELECT * FROM  TB_YAMEY_AVODA_OVDIM y
 WHERE y.taarich =  TO_DATE(pDt,'yyyymmdd')    
AND y.taarich  = p.taarich
AND y.mispar_ishi=p.mispar_ishi
AND y.measher_o_mistayeg =1)
AND  NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s2
 	 				  	   WHERE s2.taarich =   TO_DATE(pDt,'yyyymmdd')    
						   AND s2.taarich  = p.taarich
						   AND s2.mispar_ishi=p.mispar_ishi
						   AND s2.mispar_sidur=p.mispar_sidur
						    AND (s2.mispar_sidur < 99000 OR s2.mispar_sidur> 99999)
						   AND NVL(s2.shayah_leyom_kodem,0)=0);
						   EXCEPTION
   WHEN OTHERS THEN
 	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,15,SYSDATE,'',10,'',SUBSTR('trail_sidurim '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
END;
COMMIT;
						   
BEGIN
DELETE  FROM TB_SIDURIM_OVDIM p 
WHERE p.taarich =   TO_DATE(pDt,'yyyymmdd')  +1
 AND (p.mispar_sidur < 99000 OR p.mispar_sidur> 99999)
 AND p.shayah_leyom_kodem=1
 AND  EXISTS (SELECT * FROM  TB_YAMEY_AVODA_OVDIM y
 WHERE y.taarich =   TO_DATE(pDt,'yyyymmdd') +1 
AND y.taarich  = p.taarich
AND y.mispar_ishi=p.mispar_ishi
AND y.measher_o_mistayeg IS NULL );
						   EXCEPTION
   WHEN OTHERS THEN
 	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,16,SYSDATE,'',10,'',SUBSTR('trail_sidurim '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
END;
COMMIT;

BEGIN
DELETE FROM TB_SIDURIM_OVDIM p 
WHERE p.taarich =   TO_DATE(pDt,'yyyymmdd')  +1
 AND (p.mispar_sidur < 99000 OR p.mispar_sidur> 99999)
 AND    p.shayah_leyom_kodem=1
 AND  EXISTS (SELECT * FROM  TB_YAMEY_AVODA_OVDIM y
 WHERE  y.taarich =  TO_DATE(pDt,'yyyymmdd')    +1
AND y.taarich  = p.taarich
AND y.mispar_ishi=p.mispar_ishi
AND y.measher_o_mistayeg =1)
AND  NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s2
 	 				  	   WHERE s2.taarich =   TO_DATE(pDt,'yyyymmdd')    +1
						   AND s2.taarich  = p.taarich
						   AND s2.mispar_ishi=p.mispar_ishi
						   AND s2.mispar_sidur=p.mispar_sidur
						    AND (s2.mispar_sidur < 99000 OR s2.mispar_sidur> 99999)
						   AND s2.shayah_leyom_kodem=1);
						   EXCEPTION
   WHEN OTHERS THEN
 	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,17,SYSDATE,'',10,'',SUBSTR('trail_sidurim '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
END;
COMMIT;
						   
		END  pro_TrailNDel_sidurim_4reSdrn;   
		
	PROCEDURE pro_ins_sidurim_retroSdrn(pDt VARCHAR) IS
--	err_str  varchar2(1000);
	
	  CURSOR Sidurim_Retro1 IS
	  SELECT   DISTINCT k1.driver_id,k1.start_dt,k1.schedule_num,
TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_schedule,4,0),3,2),'yyyymmdd hh24:mi') hatchala,
TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.end_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.end_schedule,4,0),3,2),'yyyymmdd hh24:mi') gmar,
DECODE(sug_visa,' ','',sug_visa) sug_visa,-12 meadken	,sug_sidur
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1
WHERE k1.start_dt=  TO_DATE(pDt,'yyyymmdd')
AND k1.start_schedule<2400
AND k1.end_schedule<2400
AND NOT EXISTS (SELECT *  FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k2
WHERE k2.start_dt=  TO_DATE(pDt,'yyyymmdd')
AND k2.start_schedule<2400
AND k2.end_schedule<2400
AND k1.driver_id=k2.driver_id
AND k1.start_dt=k2.start_dt
AND k1.schedule_num=k2.schedule_num
AND k1.start_schedule=k2.start_schedule
AND k1.end_schedule<>k2.end_schedule)
AND NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s2
WHERE   s2.taarich = TO_DATE(pDt,'yyyymmdd')
AND s2.taarich =  k1.start_dt
AND  s2.mispar_ishi =k1.driver_id
AND  s2.mispar_sidur =k1.schedule_num
AND  s2.shat_hatchala =  TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_schedule,4,0),3,2),'yyyymmdd hh24:mi'));


	  CURSOR Sidurim_Retro2 IS
  SELECT   DISTINCT driver_id,start_dt,schedule_num,
TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi') hatchala,
TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(end_schedule-2400,4,0),1,2)||':'||SUBSTR(LPAD(end_schedule,4,0),3,2),'yyyymmdd hh24:mi') gmar,
DECODE(sug_visa,' ','',sug_visa) sug_visa,-12 meadken	,sug_sidur
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1
WHERE k1.start_dt= TO_DATE(pDt,'yyyymmdd')
AND k1.start_schedule<2400
AND k1.end_schedule>2359
 --and schedule_num<100000
 AND  EXISTS(SELECT * FROM TB_YAMEY_AVODA_OVDIM
WHERE  mispar_ishi=k1.driver_id
AND  taarich=k1.start_dt)
AND NOT EXISTS (SELECT * FROM  TB_SIDURIM_OVDIM s2
WHERE s2.mispar_ishi=k1.driver_id
AND  s2.taarich= k1.start_dt
AND s2.mispar_sidur =k1.schedule_num
AND  s2.shat_hatchala =TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_schedule,4,0),3,2),'yyyymmdd hh24:mi')
AND s2.shat_gmar<k1.start_dt+1)
AND  NOT  EXISTS ( SELECT * FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k2
WHERE k1.start_dt=  TO_DATE(pDt,'yyyymmdd')
AND k2.schedule_num=k1.schedule_num
AND k2.start_schedule<2400
AND k2.end_schedule>2359
AND k1.start_dt= k2.start_dt
AND   k2.driver_id =k1.driver_id
AND k2.start_schedule=k1.start_schedule
AND k2.end_schedule<>k1.end_schedule)
AND NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s3
WHERE   s3.taarich = TO_DATE(pDt,'yyyymmdd')
AND s3.taarich =  k1.start_dt
AND  s3.mispar_ishi =k1.driver_id
AND  s3.mispar_sidur =k1.schedule_num
AND  s3.shat_hatchala =  TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_schedule,4,0),3,2),'yyyymmdd hh24:mi'));
	  
	  	  CURSOR Sidurim_Retro3 IS
SELECT   DISTINCT driver_id,start_dt+1start_dt,schedule_num,
TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule-2400,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi') hatchala,
TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(end_schedule-2400,4,0),1,2)||':'||SUBSTR(LPAD(end_schedule,4,0),3,2),'yyyymmdd hh24:mi') gmar,
DECODE(sug_visa,' ','',sug_visa) sug_visa ,-12 meadken,1 Shayah	,sug_sidur
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1
WHERE start_dt= TO_DATE(pDt,'yyyymmdd')
AND start_schedule>2359
AND end_schedule>2359
 --and schedule_num<100000
 AND  EXISTS(SELECT * FROM TB_YAMEY_AVODA_OVDIM
WHERE  mispar_ishi=driver_id
AND  taarich=start_dt+1)
AND NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s2
WHERE   s2.taarich = TO_DATE(pDt,'yyyymmdd')+1
AND s2.taarich =  k1.start_dt
AND  s2.mispar_ishi =k1.driver_id
AND  s2.mispar_sidur =k1.schedule_num
AND  s2.shat_hatchala =  TO_DATE(TO_CHAR(k1.start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_schedule-2400,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_schedule,4,0),3,2),'yyyymmdd hh24:mi'));

CURSOR Sidurim_Retro4 IS
  SELECT   DISTINCT driver_id,start_dt,schedule_num,
TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi') +1/1440 hatchala,
TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(end_schedule-2400,4,0),1,2)||':'||SUBSTR(LPAD(end_schedule,4,0),3,2),'yyyymmdd hh24:mi') gmar,
DECODE(sug_visa,' ','',sug_visa) sug_visa, -12 meadken	,sug_sidur
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
WHERE start_dt= TO_DATE(pDt,'yyyymmdd')
AND start_schedule<2400
AND end_schedule>2359
 --and schedule_num<100000
 AND  EXISTS(SELECT * FROM TB_YAMEY_AVODA_OVDIM
WHERE  mispar_ishi=driver_id
AND  taarich=start_dt)
AND  EXISTS (SELECT * FROM  TB_SIDURIM_OVDIM s2
WHERE s2.mispar_ishi=driver_id
AND  s2.taarich= start_dt
AND s2.mispar_sidur =schedule_num
AND  s2.shat_hatchala =TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')
AND s2.shat_gmar<start_dt+1)
AND  NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s3
WHERE   s3.taarich = TO_DATE(pDt,'yyyymmdd')+1
AND s3.taarich =  start_dt
AND  s3.mispar_ishi =driver_id
AND  s3.mispar_sidur =schedule_num
AND  s3.shat_hatchala =  TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule-2400,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi'));

		 CURSOR Sidurim_Retro5 IS
SELECT   DISTINCT driver_id,start_dt+1 start_dt,schedule_num,
TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule-2400,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi') hatchala,
TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(end_schedule,4,0),1,2)||':'||SUBSTR(LPAD(end_schedule,4,0),3,2),'yyyymmdd hh24:mi') gmar,
DECODE(sug_visa,' ','',sug_visa) sug_visa, -12 meadken,1 Shayah	,sug_sidur
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1
WHERE start_dt= TO_DATE(pDt,'yyyymmdd')
AND start_schedule>2359
AND end_schedule<2400
 --and schedule_num<100000
 AND  EXISTS(SELECT * FROM TB_YAMEY_AVODA_OVDIM
WHERE  mispar_ishi=driver_id
AND  taarich=start_dt)
AND NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s2
WHERE   s2.taarich = TO_DATE(pDt,'yyyymmdd')
AND s2.taarich =  k1.start_dt
AND  s2.mispar_ishi =k1.driver_id
AND  s2.mispar_sidur =k1.schedule_num
AND  s2.shat_hatchala =  TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_schedule,4,0),3,2),'yyyymmdd hh24:mi'));

	  CURSOR Trail_Sidurim_Retro1 IS
SELECT   DISTINCT driver_id,start_dt,schedule_num,
TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi') hatchala,
TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(end_schedule-2400,4,0),1,2)||':'||SUBSTR(LPAD(end_schedule,4,0),3,2),'yyyymmdd hh24:mi') gmar,
DECODE(sug_visa,' ','',sug_visa) sug_visa, -12 meadken	,sug_sidur
FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
WHERE start_dt =  TO_DATE(pDt,'yyyymmdd')
AND start_schedule<2400
AND end_schedule>2359
 AND  EXISTS(SELECT * FROM TB_YAMEY_AVODA_OVDIM
WHERE  mispar_ishi=driver_id
AND  taarich=start_dt)
AND  EXISTS (SELECT * FROM  TB_SIDURIM_OVDIM s2
WHERE s2.mispar_ishi=driver_id
AND  s2.taarich= start_dt
AND s2.mispar_sidur =schedule_num
AND  s2.shat_hatchala =TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')
AND s2.shat_gmar<start_dt+1);

	  CURSOR Trail_Sidurim_Retro2 IS
 SELECT   DISTINCT k1.driver_id,k1.start_dt,k1.schedule_num,
TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_schedule,4,0),3,2),'yyyymmdd hh24:mi') hatchala,
TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.end_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.end_schedule,4,0),3,2),'yyyymmdd hh24:mi') gmar,
DECODE(sug_visa,' ','',sug_visa) sug_visa, -12 meadken	,sug_sidur
FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1
WHERE k1.start_dt =  TO_DATE(pDt,'yyyymmdd')
AND k1.start_schedule<2400
AND k1.end_schedule<2400
AND k1.end_schedule=(SELECT MAX(k3.end_schedule) FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k3
WHERE k3.start_dt =  TO_DATE(pDt,'yyyymmdd')
AND k3.start_schedule<2400
AND k3.end_schedule<2400
AND k1.driver_id=k3.driver_id
AND k1.start_dt=k3.start_dt
AND k1.schedule_num=k3.schedule_num
AND k1.start_schedule=k3.start_schedule)
AND  EXISTS (SELECT *  FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k2
WHERE k2.start_dt =  TO_DATE(pDt,'yyyymmdd')
AND k2.start_schedule<2400
AND k2.end_schedule<2400
AND k1.driver_id=k2.driver_id
AND k1.start_dt=k2.start_dt
AND k1.schedule_num=k2.schedule_num
AND k1.start_schedule=k2.start_schedule
AND k1.end_schedule<>k2.end_schedule)
AND NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s2
WHERE   s2.taarich = TO_DATE(pDt,'yyyymmdd')
AND s2.taarich =  k1.start_dt
AND  s2.mispar_ishi =k1.driver_id
AND  s2.mispar_sidur =k1.schedule_num
AND  s2.shat_hatchala =  TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_schedule,4,0),3,2),'yyyymmdd hh24:mi'));

CURSOR Sidurim_Retro6 IS
SELECT   DISTINCT k1.driver_id,k1.start_dt,k1.schedule_num,
TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_schedule,4,0),3,2),'yyyymmdd hh24:mi') hatchala,
TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.end_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.end_schedule,4,0),3,2),'yyyymmdd hh24:mi') gmar,
DECODE(sug_visa,' ','',sug_visa) sug_visa, -12 meadken	,sug_sidur
FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1
WHERE k1.start_dt =  TO_DATE(pDt,'yyyymmdd')
AND k1.start_schedule<2400
AND k1.end_schedule<2400
AND k1.end_schedule=(SELECT MIN(k3.end_schedule) FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k3
WHERE k3.start_dt =  TO_DATE(pDt,'yyyymmdd')
AND k3.start_schedule<2400
AND k3.end_schedule<2400
AND k1.driver_id=k3.driver_id
AND k1.start_dt=k3.start_dt
AND k1.schedule_num=k3.schedule_num
AND k1.start_schedule=k3.start_schedule)
AND  EXISTS (SELECT *  FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k2
WHERE k2.start_dt =  TO_DATE(pDt,'yyyymmdd')
AND k2.start_schedule<2400
AND k2.end_schedule<2400
AND k1.driver_id=k2.driver_id
AND k1.start_dt=k2.start_dt
AND k1.schedule_num=k2.schedule_num
AND k1.start_schedule=k2.start_schedule
AND k1.end_schedule<>k2.end_schedule)
AND  NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s2
WHERE   s2.taarich = TO_DATE(pDt,'yyyymmdd')
AND s2.taarich =  k1.start_dt
AND  s2.mispar_ishi =k1.driver_id
AND  s2.mispar_sidur =k1.schedule_num
AND  s2.shat_hatchala =  TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_schedule,4,0),3,2),'yyyymmdd hh24:mi'));

CURSOR Sidurim_Retro7 IS
SELECT   DISTINCT k1.driver_id,k1.start_dt,k1.schedule_num,
TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_schedule,4,0),3,2),'yyyymmdd hh24:mi') +1/1440 hatchala,
TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.end_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.end_schedule,4,0),3,2),'yyyymmdd hh24:mi') gmar,
DECODE(sug_visa,' ','',sug_visa) sug_visa , -12 meadken	,sug_sidur
FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1
WHERE k1.start_dt =  TO_DATE(pDt,'yyyymmdd')
AND k1.start_schedule<2400
AND k1.end_schedule<2400
AND k1.end_schedule=(SELECT MAX(k3.end_schedule) FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k3
WHERE k3.start_dt =  TO_DATE(pDt,'yyyymmdd')
AND k3.start_schedule<2400
AND k3.end_schedule<2400
AND k1.driver_id=k3.driver_id
AND k1.start_dt=k3.start_dt
AND k1.schedule_num=k3.schedule_num
AND k1.start_schedule=k3.start_schedule)
AND  EXISTS (SELECT *  FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k2
WHERE k2.start_dt =  TO_DATE(pDt,'yyyymmdd')
AND k2.start_schedule<2400
AND k2.end_schedule<2400
AND k1.driver_id=k2.driver_id
AND k1.start_dt=k2.start_dt
AND k1.schedule_num=k2.schedule_num
AND k1.start_schedule=k2.start_schedule
AND k1.end_schedule<>k2.end_schedule)
AND  NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s2
WHERE   s2.taarich = TO_DATE(pDt,'yyyymmdd')
AND s2.taarich =  k1.start_dt
AND  s2.mispar_ishi =k1.driver_id
AND  s2.mispar_sidur =k1.schedule_num
AND  s2.shat_hatchala =  TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_schedule,4,0),3,2),'yyyymmdd hh24:mi'));

CURSOR Sidurim_Retro8 IS
SELECT  DISTINCT driver_id,start_dt,schedule_num,
TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi') hatchala,
TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(end_schedule-2400,4,0),1,2)||':'||SUBSTR(LPAD(end_schedule,4,0),3,2),'yyyymmdd hh24:mi') gmar,
DECODE(sug_visa,' ','',sug_visa) sug_visa, -12 meadken	,sug_sidur
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1
WHERE k1.start_dt = TO_DATE(pDt,'yyyymmdd') 
AND k1.start_schedule<2400
AND k1.end_schedule>2359
AND    EXISTS ( SELECT * FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k2
WHERE k1.start_dt = TO_DATE(pDt,'yyyymmdd') 
AND k2.start_schedule<2400
AND k2.end_schedule>2359
AND k1.start_dt= k2.start_dt
AND   k2.driver_id =k1.driver_id
AND k2.schedule_num=k1.schedule_num
AND k2.start_schedule=k1.start_schedule
AND k2.end_schedule<>k1.end_schedule)
AND  NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s2
WHERE   s2.taarich = TO_DATE(pDt,'yyyymmdd')
AND s2.taarich =  k1.start_dt
AND  s2.mispar_ishi =k1.driver_id
AND  s2.mispar_sidur =k1.schedule_num
AND  s2.shat_hatchala =  TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_schedule,4,0),3,2),'yyyymmdd hh24:mi'))
ORDER BY driver_id;
			  
   BEGIN
--     err_str:='';
	 
  FOR  Sidurim_Retro1_rec IN  Sidurim_Retro1 LOOP
BEGIN
 		 INSERT INTO TB_SIDURIM_OVDIM s ( mispar_ishi  , taarich  , mispar_sidur  , shat_hatchala  , shat_gmar,sector_visa,meadken_acharon 	,sug_sidur  )
VALUES (Sidurim_Retro1_rec.driver_id,Sidurim_Retro1_rec.start_dt,  Sidurim_Retro1_rec.schedule_num,  
Sidurim_Retro1_rec.hatchala,Sidurim_Retro1_rec.gmar,  Sidurim_Retro1_rec.sug_visa,   Sidurim_Retro1_rec.meadken ,Sidurim_Retro1_rec.sug_sidur);

EXCEPTION
   WHEN OTHERS THEN
 --  err_str:=err_str||' (4,60) '|| to_char(Sidurim_Retro1_rec.driver_id) ||' '||to_char(Sidurim_Retro1_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
 	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,20,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Sidurim_Retro1_rec.driver_id) ||' '||TO_CHAR(Sidurim_Retro1_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));   
 END;
END LOOP;
COMMIT;
  
		 
FOR  Sidurim_Retro2_rec IN  Sidurim_Retro2 LOOP
BEGIN
INSERT INTO TB_SIDURIM_OVDIM s ( mispar_ishi  , taarich  , mispar_sidur  , shat_hatchala  , shat_gmar,sector_visa,meadken_acharon 	,sug_sidur  )
VALUES (Sidurim_Retro2_rec.driver_id,Sidurim_Retro2_rec.start_dt,  Sidurim_Retro2_rec.schedule_num,  
Sidurim_Retro2_rec.hatchala,Sidurim_Retro2_rec.gmar,  Sidurim_Retro2_rec.sug_visa,   Sidurim_Retro2_rec.meadken ,Sidurim_Retro2_rec.sug_sidur);

EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,61) '||to_char(Sidurim_Retro2_rec.driver_id) ||' '||to_char(Sidurim_Retro2_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
 	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,21,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Sidurim_Retro2_rec.driver_id) ||' '||TO_CHAR(Sidurim_Retro2_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
  END;
END LOOP;
COMMIT;
  
FOR  Sidurim_Retro3_rec IN  Sidurim_Retro3 LOOP
BEGIN
INSERT INTO TB_SIDURIM_OVDIM s ( mispar_ishi  , taarich  , mispar_sidur  , shat_hatchala  , shat_gmar,sector_visa,
 meadken_acharon,Shayah_LeYom_Kodem 	,sug_sidur  )
VALUES (Sidurim_Retro3_rec.driver_id,Sidurim_Retro3_rec.start_dt,  Sidurim_Retro3_rec.schedule_num,  
Sidurim_Retro3_rec.hatchala,Sidurim_Retro3_rec.gmar,  Sidurim_Retro3_rec.sug_visa,   Sidurim_Retro3_rec.meadken ,Sidurim_Retro3_rec.Shayah,Sidurim_Retro3_rec.sug_sidur);

EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,62) '||to_char(Sidurim_Retro3_rec.driver_id) ||' '||to_char(Sidurim_Retro3_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
 	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,22,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Sidurim_Retro3_rec.driver_id) ||' '||TO_CHAR(Sidurim_Retro3_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
  END;
END LOOP;
COMMIT;
 
 
FOR  Sidurim_Retro4_rec IN  Sidurim_Retro4 LOOP
BEGIN
INSERT INTO TB_SIDURIM_OVDIM ( mispar_ishi  , taarich  , mispar_sidur  , shat_hatchala  ,
shat_gmar,sector_visa,meadken_acharon  	,sug_sidur )
VALUES (Sidurim_Retro4_rec.driver_id,Sidurim_Retro4_rec.start_dt,  Sidurim_Retro4_rec.schedule_num,  Sidurim_Retro4_rec.hatchala,
Sidurim_Retro4_rec.gmar,  Sidurim_Retro4_rec.sug_visa,   Sidurim_Retro4_rec.meadken  ,Sidurim_Retro4_rec.sug_sidur );

EXCEPTION
   WHEN OTHERS THEN
 --  err_str:=err_str||' (4,63) '|| to_char(Sidurim_Retro4_rec.driver_id) ||' '||to_char(Sidurim_Retro4_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
 	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,23,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Sidurim_Retro4_rec.driver_id) ||' '||TO_CHAR(Sidurim_Retro4_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
  END;
END LOOP;
COMMIT;

FOR  Sidurim_Retro5_rec IN  Sidurim_Retro5 LOOP
BEGIN
 INSERT INTO TB_SIDURIM_OVDIM s ( mispar_ishi  , taarich  , mispar_sidur  , shat_hatchala  , shat_gmar,sector_visa,
 meadken_acharon,Shayah_LeYom_Kodem 	,sug_sidur  )
 VALUES (Sidurim_Retro5_rec.driver_id,Sidurim_Retro5_rec.start_dt,  Sidurim_Retro5_rec.schedule_num,  
Sidurim_Retro5_rec.hatchala,Sidurim_Retro5_rec.gmar,  Sidurim_Retro5_rec.sug_visa,   Sidurim_Retro5_rec.meadken ,Sidurim_Retro5_rec.Shayah,Sidurim_Retro5_rec.sug_sidur);

EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,64) '||to_char(Sidurim_Retro5_rec.driver_id) ||' '||to_char(Sidurim_Retro5_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
 	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,24,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Sidurim_Retro5_rec.driver_id) ||' '||TO_CHAR(Sidurim_Retro5_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
 END;
END LOOP;
COMMIT;
 
 
FOR  Trail_Sidurim_Retro1_rec IN  Trail_Sidurim_Retro1 LOOP
BEGIN
INSERT INTO TRAIL_SIDURIM_OVDIM s ( mispar_ishi  , taarich  , mispar_sidur  , shat_hatchala  , shat_gmar,sector_visa,meadken_acharon  	,sug_sidur )
 VALUES (Trail_Sidurim_Retro1_rec.driver_id,Trail_Sidurim_Retro1_rec.start_dt,  Trail_Sidurim_Retro1_rec.schedule_num,  
Trail_Sidurim_Retro1_rec.hatchala,Trail_Sidurim_Retro1_rec.gmar,  Trail_Sidurim_Retro1_rec.sug_visa,   Trail_Sidurim_Retro1_rec.meadken ,Trail_Sidurim_Retro1_rec.sug_sidur);

EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,65) '||to_char(Trail_Sidurim_Retro1_rec.driver_id) ||' '||to_char(Trail_Sidurim_Retro1_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
 	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,25,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Trail_Sidurim_Retro1_rec.driver_id) ||' '||TO_CHAR(Trail_Sidurim_Retro1_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
   END;
END LOOP; 
COMMIT;
 
FOR  Trail_Sidurim_Retro2_rec IN  Trail_Sidurim_Retro2 LOOP
BEGIN
 INSERT INTO TRAIL_SIDURIM_OVDIM s ( mispar_ishi  , taarich  , mispar_sidur  , shat_hatchala  , shat_gmar,sector_visa,meadken_acharon 	,sug_sidur  )
 VALUES (Trail_Sidurim_Retro2_rec.driver_id,Trail_Sidurim_Retro2_rec.start_dt,  Trail_Sidurim_Retro2_rec.schedule_num,  
Trail_Sidurim_Retro2_rec.hatchala,Trail_Sidurim_Retro2_rec.gmar,  Trail_Sidurim_Retro2_rec.sug_visa,   Trail_Sidurim_Retro2_rec.meadken ,Trail_Sidurim_Retro2_rec.sug_sidur);

EXCEPTION
   WHEN OTHERS THEN
 --  err_str:=err_str||' (4,66) '|| to_char(Trail_Sidurim_Retro2_rec.driver_id) ||' '||to_char(Trail_Sidurim_Retro2_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,26,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Trail_Sidurim_Retro2_rec.driver_id) ||' '||TO_CHAR(Trail_Sidurim_Retro2_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
   END;
END LOOP;
COMMIT;
  
	
FOR  Sidurim_Retro6_rec IN  Sidurim_Retro6 LOOP
BEGIN
 INSERT INTO TB_SIDURIM_OVDIM s ( mispar_ishi  , taarich  , mispar_sidur  , shat_hatchala  , shat_gmar,sector_visa, meadken_acharon 	,sug_sidur  )
 VALUES (Sidurim_Retro6_rec.driver_id,Sidurim_Retro6_rec.start_dt,  Sidurim_Retro6_rec.schedule_num,  
Sidurim_Retro6_rec.hatchala,Sidurim_Retro6_rec.gmar,  Sidurim_Retro6_rec.sug_visa,   Sidurim_Retro6_rec.meadken ,Sidurim_Retro6_rec.sug_sidur);

EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,67) '||to_char(Sidurim_Retro6_rec.driver_id) ||' '||to_char(Sidurim_Retro6_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,27,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Sidurim_Retro6_rec.driver_id) ||' '||TO_CHAR(Sidurim_Retro6_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
   END;
END LOOP;
COMMIT;

FOR  Sidurim_Retro7_rec IN  Sidurim_Retro7 LOOP
BEGIN
 INSERT INTO TB_SIDURIM_OVDIM s ( mispar_ishi  , taarich  , mispar_sidur  , shat_hatchala  , shat_gmar,sector_visa, meadken_acharon  	,sug_sidur )
 VALUES (Sidurim_Retro7_rec.driver_id,Sidurim_Retro7_rec.start_dt,  Sidurim_Retro7_rec.schedule_num,  
Sidurim_Retro7_rec.hatchala,Sidurim_Retro7_rec.gmar,  Sidurim_Retro7_rec.sug_visa,   Sidurim_Retro7_rec.meadken,Sidurim_Retro7_rec.sug_sidur );

EXCEPTION
   WHEN OTHERS THEN
 --  err_str:=err_str||' (4,68) '||to_char(Sidurim_Retro7_rec.driver_id) ||' '||to_char(Sidurim_Retro7_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,28,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Sidurim_Retro7_rec.driver_id) ||' '||TO_CHAR(Sidurim_Retro7_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
     END;
END LOOP;
COMMIT;

FOR  Sidurim_Retro8_rec IN  Sidurim_Retro8 LOOP
BEGIN
 INSERT INTO TB_SIDURIM_OVDIM s ( mispar_ishi  , taarich  , mispar_sidur  , shat_hatchala  , shat_gmar,sector_visa, meadken_acharon 	,sug_sidur  )
 VALUES (Sidurim_Retro8_rec.driver_id,Sidurim_Retro8_rec.start_dt,  Sidurim_Retro8_rec.schedule_num,  
Sidurim_Retro8_rec.hatchala,Sidurim_Retro8_rec.gmar,  Sidurim_Retro8_rec.sug_visa,   Sidurim_Retro8_rec.meadken  ,Sidurim_Retro8_rec.sug_sidur);

EXCEPTION
   WHEN OTHERS THEN
 --  err_str:=err_str||' (4,69) '||to_char(Sidurim_Retro8_rec.driver_id) ||' '||to_char(Sidurim_Retro8_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,29,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Sidurim_Retro8_rec.driver_id) ||' '||TO_CHAR(Sidurim_Retro8_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
   END;
END LOOP;
COMMIT;

		BEGIN
 UPDATE TB_SIDURIM_OVDIM 
SET   Lo_letashlum=1 ,Kod_Siba_Lo_Letashlum=16
 WHERE taarich= TO_DATE(pDt,'yyyymmdd')
AND meadken_acharon=-12
AND Lo_letashlum IS NULL
 AND Kod_Siba_Lo_Letashlum IS NULL;

  EXCEPTION
   WHEN OTHERS THEN
 --  err_str:=err_str||' (4,6) '||'update_sidurim_retro '||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,30,SYSDATE,'',10,'',SUBSTR('update_sidurim_retro '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
   END;
COMMIT;
	
		END  pro_ins_sidurim_retroSdrn;   
	


PROCEDURE pro_ins_peilut_retroSdrn(pDt VARCHAR) IS
--err_str  varchar2(1000);

		 CURSOR Peilut_Retro1 IS
SELECT    driver_id,start_dt,schedule_num,branch,
 TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi') hatchala,
 TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_time,4,0),1,2)||':'||SUBSTR(LPAD(start_time,4,0),3,2),'yyyymmdd hh24:mi') gmar,ride_id,
 makat_line,bus_number,bus_sequence,waiting_time,
 spm_time,spm_bus_number,spm_schedule_num,spm_start_time,spm_makat_line,spm_line_sign,spm_location,-12 meadken
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm,TB_YAMEY_AVODA_OVDIM y
WHERE  start_dt= TO_DATE(pDt,'yyyymmdd')
AND start_schedule<2400
AND start_time<2400
AND   y.taarich =   TO_DATE(pDt,'yyyymmdd')  
AND y.taarich  = start_dt
AND y.mispar_ishi=driver_id
AND y.measher_o_mistayeg IS NULL
UNION ALL
SELECT    driver_id,start_dt,schedule_num,branch,
 TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi') hatchala,
 TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_time,4,0),1,2)||':'||SUBSTR(LPAD(start_time,4,0),3,2),'yyyymmdd hh24:mi') gmar,ride_id,
 makat_line,bus_number,bus_sequence,waiting_time,
 spm_time,spm_bus_number,spm_schedule_num,spm_start_time,spm_makat_line,spm_line_sign,spm_location,-12 meadken
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm,TB_YAMEY_AVODA_OVDIM y
WHERE  start_dt= TO_DATE(pDt,'yyyymmdd')
AND start_schedule<2400
AND start_time<2400
AND   y.taarich =   TO_DATE(pDt,'yyyymmdd')  
AND y.taarich  = start_dt
AND y.mispar_ishi=driver_id
AND y.measher_o_mistayeg =1
AND  NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s
 	 				  	   WHERE s.taarich =   TO_DATE(pDt,'yyyymmdd')  
						   AND s.taarich  = start_dt
						   AND s.mispar_ishi=driver_id
						   AND s.mispar_sidur=schedule_num
						   AND s.shat_hatchala= TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')
						   AND s.shayah_leyom_kodem=1) ;
						   
 		 CURSOR Peilut_Retro2 IS
		 SELECT    driver_id,start_dt,schedule_num,branch,
 TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')  hatchala,
 TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(start_time-2400,4,0),1,2)||':'||SUBSTR(LPAD(start_time,4,0),3,2),'yyyymmdd hh24:mi') gmar,ride_id,
 makat_line,bus_number,bus_sequence,waiting_time,
 spm_time,spm_bus_number,spm_schedule_num,spm_start_time,spm_makat_line,spm_line_sign,spm_location,-12 meadken
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm,TB_YAMEY_AVODA_OVDIM y
WHERE  start_dt= TO_DATE(pDt,'yyyymmdd')
AND start_schedule<2400
AND start_time>2359
 --and schedule_num<100000;
 AND NOT EXISTS (SELECT * FROM  TB_SIDURIM_OVDIM s2
WHERE s2.mispar_ishi=driver_id
AND  s2.taarich= start_dt
AND s2.mispar_sidur =schedule_num
AND  s2.shat_hatchala =TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')
AND s2.shat_gmar<start_dt+1)
AND   y.taarich =   TO_DATE(pDt,'yyyymmdd')  
AND y.taarich  = start_dt
AND y.mispar_ishi=driver_id
AND y.measher_o_mistayeg IS NULL
UNION ALL
 SELECT    driver_id,start_dt,schedule_num,branch,
 TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')  hatchala,
 TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(start_time-2400,4,0),1,2)||':'||SUBSTR(LPAD(start_time,4,0),3,2),'yyyymmdd hh24:mi') gmar,ride_id,
 makat_line,bus_number,bus_sequence,waiting_time,
 spm_time,spm_bus_number,spm_schedule_num,spm_start_time,spm_makat_line,spm_line_sign,spm_location,-12 meadken
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm,TB_YAMEY_AVODA_OVDIM y
WHERE  start_dt= TO_DATE(pDt,'yyyymmdd')
AND start_schedule<2400
AND start_time>2359
 --and schedule_num<100000;
 AND NOT EXISTS (SELECT * FROM  TB_SIDURIM_OVDIM s2
WHERE s2.mispar_ishi=driver_id
AND  s2.taarich= start_dt
AND s2.mispar_sidur =schedule_num
AND  s2.shat_hatchala =TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')
AND s2.shat_gmar<start_dt+1)
AND   y.taarich =   TO_DATE(pDt,'yyyymmdd')  
AND y.taarich  = start_dt
AND y.mispar_ishi=driver_id
AND y.measher_o_mistayeg =1
AND  NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s
 	 				  	   WHERE s.taarich =   TO_DATE(pDt,'yyyymmdd')  
						   AND s.taarich  = start_dt
						   AND s.mispar_ishi=driver_id
						   AND s.mispar_sidur=schedule_num
						   AND s.shat_hatchala= TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')
						   AND s.shayah_leyom_kodem=1) ;		
						   
		 CURSOR Peilut_Retro3 IS
		 SELECT    driver_id,start_dt,schedule_num,branch,
 TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule-2400,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')  hatchala,
 TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(start_time-2400,4,0),1,2)||':'||SUBSTR(LPAD(start_time,4,0),3,2),'yyyymmdd hh24:mi') gmar,ride_id,
 makat_line,bus_number,bus_sequence,waiting_time,
 spm_time,spm_bus_number,spm_schedule_num,spm_start_time,spm_makat_line,spm_line_sign,spm_location,-12 meadken
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm,TB_YAMEY_AVODA_OVDIM y
WHERE  start_dt= TO_DATE(pDt,'yyyymmdd')
AND start_schedule>2359
AND start_time>2359
AND   y.taarich =   TO_DATE(pDt,'yyyymmdd')  
AND y.taarich  = start_dt
AND y.mispar_ishi=driver_id
AND y.measher_o_mistayeg IS NULL
UNION ALL
SELECT    driver_id,start_dt,schedule_num,branch,
 TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule-2400,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')  hatchala,
 TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(start_time-2400,4,0),1,2)||':'||SUBSTR(LPAD(start_time,4,0),3,2),'yyyymmdd hh24:mi') gmar,ride_id,
 makat_line,bus_number,bus_sequence,waiting_time,
 spm_time,spm_bus_number,spm_schedule_num,spm_start_time,spm_makat_line,spm_line_sign,spm_location,-12 meadken
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm,TB_YAMEY_AVODA_OVDIM y
WHERE  start_dt= TO_DATE(pDt,'yyyymmdd')
AND start_schedule>2359
AND start_time>2359
AND   y.taarich =   TO_DATE(pDt,'yyyymmdd')  
AND y.taarich  = start_dt
AND y.mispar_ishi=driver_id
AND y.measher_o_mistayeg =1
AND  NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s
 	 				  	   WHERE s.taarich =   TO_DATE(pDt,'yyyymmdd')  
						   AND s.taarich  = start_dt
						   AND s.mispar_ishi=driver_id
						   AND s.mispar_sidur=schedule_num
						   AND s.shat_hatchala= TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')
						   AND s.shayah_leyom_kodem=1) ;
						   
		 CURSOR Peilut_Retro4 IS
		 SELECT    driver_id,start_dt,schedule_num,branch,
 TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')+1/1440  hatchala,
 TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(start_time-2400,4,0),1,2)||':'||SUBSTR(LPAD(start_time,4,0),3,2),'yyyymmdd hh24:mi') gmar,ride_id,
 makat_line,bus_number,bus_sequence,waiting_time,
 spm_time,spm_bus_number,spm_schedule_num,spm_start_time,spm_makat_line,spm_line_sign,spm_location,-12 meadken
FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm,TB_YAMEY_AVODA_OVDIM y
WHERE  start_dt= TO_DATE(pDt,'yyyymmdd')
AND start_schedule<2400
AND start_time>2359
 --and schedule_num<100000;
 AND EXISTS (SELECT * FROM  TB_SIDURIM_OVDIM s2
WHERE s2.mispar_ishi=driver_id
AND  s2.taarich= start_dt
AND s2.mispar_sidur =schedule_num
AND  s2.shat_hatchala =TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')
AND s2.shat_gmar<start_dt+1)
AND   y.taarich =   TO_DATE(pDt,'yyyymmdd')  
AND y.taarich  = start_dt
AND y.mispar_ishi=driver_id
AND y.measher_o_mistayeg IS NULL
UNION ALL
SELECT    driver_id,start_dt,schedule_num,branch,
 TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')+1/1440  hatchala,
 TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(start_time-2400,4,0),1,2)||':'||SUBSTR(LPAD(start_time,4,0),3,2),'yyyymmdd hh24:mi') gmar,ride_id,
 makat_line,bus_number,bus_sequence,waiting_time,
 spm_time,spm_bus_number,spm_schedule_num,spm_start_time,spm_makat_line,spm_line_sign,spm_location,-12 meadken
FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm,TB_YAMEY_AVODA_OVDIM y
WHERE  start_dt= TO_DATE(pDt,'yyyymmdd')
AND start_schedule<2400
AND start_time>2359
 --and schedule_num<100000;
 AND EXISTS (SELECT * FROM  TB_SIDURIM_OVDIM s2
WHERE s2.mispar_ishi=driver_id
AND  s2.taarich= start_dt
AND s2.mispar_sidur =schedule_num
AND  s2.shat_hatchala =TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')
AND s2.shat_gmar<start_dt+1)
AND   y.taarich =   TO_DATE(pDt,'yyyymmdd')  
AND y.taarich  = start_dt
AND y.mispar_ishi=driver_id
AND y.measher_o_mistayeg =1
AND  NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s
 	 				  	   WHERE s.taarich =   TO_DATE(pDt,'yyyymmdd')  
						   AND s.taarich  = start_dt
						   AND s.mispar_ishi=driver_id
						   AND s.mispar_sidur=schedule_num
						   AND s.shat_hatchala= TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')
						   AND s.shayah_leyom_kodem=1) ;
						   
		 CURSOR Peilut_Retro5 IS
		 SELECT    driver_id,start_dt,schedule_num,branch,
 TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule-2400,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')  hatchala,
 TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_time,4,0),1,2)||':'||SUBSTR(LPAD(start_time,4,0),3,2),'yyyymmdd hh24:mi') gmar,ride_id,
 makat_line,bus_number,bus_sequence,waiting_time,
 spm_time,spm_bus_number,spm_schedule_num,spm_start_time,spm_makat_line,spm_line_sign,spm_location,-12 meadken
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm,TB_YAMEY_AVODA_OVDIM y
WHERE  start_dt= TO_DATE(pDt,'yyyymmdd')
AND start_schedule>2359
AND start_time<2400
AND   y.taarich =   TO_DATE(pDt,'yyyymmdd')  
AND y.taarich  = start_dt
AND y.mispar_ishi=driver_id
AND y.measher_o_mistayeg IS NULL
UNION ALL
SELECT    driver_id,start_dt,schedule_num,branch,
 TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule-2400,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')  hatchala,
 TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_time,4,0),1,2)||':'||SUBSTR(LPAD(start_time,4,0),3,2),'yyyymmdd hh24:mi') gmar,ride_id,
 makat_line,bus_number,bus_sequence,waiting_time,
 spm_time,spm_bus_number,spm_schedule_num,spm_start_time,spm_makat_line,spm_line_sign,spm_location,-12 meadken
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm,TB_YAMEY_AVODA_OVDIM y
WHERE  start_dt= TO_DATE(pDt,'yyyymmdd')
AND start_schedule>2359
AND start_time<2400
AND   y.taarich =   TO_DATE(pDt,'yyyymmdd')  
AND y.taarich  = start_dt
AND y.mispar_ishi=driver_id
AND y.measher_o_mistayeg =1
AND  NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s
 	 				  	   WHERE s.taarich =   TO_DATE(pDt,'yyyymmdd')  
						   AND s.taarich  = start_dt
						   AND s.mispar_ishi=driver_id
						   AND s.mispar_sidur=schedule_num
						   AND s.shat_hatchala= TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')
						   AND s.shayah_leyom_kodem=1) ;
						   
		 CURSOR Peilut_Retro6 IS
		 SELECT    k1.driver_id,k1.start_dt,k1.schedule_num,branch,
 TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_schedule,4,0),3,2),'yyyymmdd hh24:mi')  hatchala,
 TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_time,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_time,4,0),3,2),'yyyymmdd hh24:mi')  gmar,k1.ride_id,
 k1.makat_line,bus_number,k1.bus_sequence,k1.waiting_time,
 k1.spm_time,k1.spm_bus_number,k1.spm_schedule_num,k1.spm_start_time,k1.spm_makat_line,
 k1.spm_line_sign,k1.spm_location,-12 meadken
FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1,TB_YAMEY_AVODA_OVDIM y
WHERE  k1.start_dt= TO_DATE(pDt,'yyyymmdd')
AND k1.start_schedule<2400
AND k1.start_time<2400
 --and k1.schedule_num<100000
AND k1.end_schedule=(SELECT MIN(k3.end_schedule) FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k3
WHERE k3.start_dt =  TO_DATE(pDt,'yyyymmdd')
AND k3.start_schedule<2400
AND k3.end_schedule<2400
AND k1.driver_id=k3.driver_id
AND k1.start_dt=k3.start_dt
AND k1.schedule_num=k3.schedule_num
AND k1.start_schedule=k3.start_schedule)
 AND EXISTS (SELECT *  FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k2
WHERE k2.start_dt=  TO_DATE(pDt,'yyyymmdd')
AND k2.start_schedule<2400
AND k2.end_schedule<2400
AND k1.driver_id=k2.driver_id
AND k1.start_dt=k2.start_dt
AND k1.schedule_num=k2.schedule_num
AND k1.start_schedule=k2.start_schedule
AND k1.end_schedule<>k2.end_schedule)
AND NOT EXISTS (SELECT * FROM  TB_PEILUT_OVDIM s2
WHERE s2.mispar_ishi= k1.driver_id
AND  s2.taarich= k1.start_dt
AND  s2.taarich=  TO_DATE(pDt,'yyyymmdd')
AND s2.mispar_sidur =k1.schedule_num
AND  s2.shat_hatchala_sidur =TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_schedule,4,0),3,2),'yyyymmdd hh24:mi')
AND s2.SHAT_YETZIA=TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_time,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_time,4,0),3,2),'yyyymmdd hh24:mi')
AND s2.MISPAR_KNISA=k1.ride_id)
AND   y.taarich =   TO_DATE(pDt,'yyyymmdd')  
AND y.taarich  = start_dt
AND y.mispar_ishi=driver_id
AND y.measher_o_mistayeg IS NULL
UNION ALL
SELECT    k1.driver_id,k1.start_dt,k1.schedule_num,branch,
 TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_schedule,4,0),3,2),'yyyymmdd hh24:mi')  hatchala,
 TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_time,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_time,4,0),3,2),'yyyymmdd hh24:mi')  gmar,k1.ride_id,
 k1.makat_line,bus_number,k1.bus_sequence,k1.waiting_time,
 k1.spm_time,k1.spm_bus_number,k1.spm_schedule_num,k1.spm_start_time,k1.spm_makat_line,
 k1.spm_line_sign,k1.spm_location,-12 meadken
FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1,TB_YAMEY_AVODA_OVDIM y
WHERE  k1.start_dt= TO_DATE(pDt,'yyyymmdd')
AND k1.start_schedule<2400
AND k1.start_time<2400
 --and k1.schedule_num<100000
AND k1.end_schedule=(SELECT MIN(k3.end_schedule) FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k3
WHERE k3.start_dt =  TO_DATE(pDt,'yyyymmdd')
AND k3.start_schedule<2400
AND k3.end_schedule<2400
AND k1.driver_id=k3.driver_id
AND k1.start_dt=k3.start_dt
AND k1.schedule_num=k3.schedule_num
AND k1.start_schedule=k3.start_schedule)
 AND EXISTS (SELECT *  FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k2
WHERE k2.start_dt=  TO_DATE(pDt,'yyyymmdd')
AND k2.start_schedule<2400
AND k2.end_schedule<2400
AND k1.driver_id=k2.driver_id
AND k1.start_dt=k2.start_dt
AND k1.schedule_num=k2.schedule_num
AND k1.start_schedule=k2.start_schedule
AND k1.end_schedule<>k2.end_schedule)
AND NOT EXISTS (SELECT * FROM  TB_PEILUT_OVDIM s2
WHERE s2.mispar_ishi= k1.driver_id
AND  s2.taarich= k1.start_dt
AND  s2.taarich=  TO_DATE(pDt,'yyyymmdd')
AND s2.mispar_sidur =k1.schedule_num
AND  s2.shat_hatchala_sidur =TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_schedule,4,0),3,2),'yyyymmdd hh24:mi')
AND s2.SHAT_YETZIA=TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_time,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_time,4,0),3,2),'yyyymmdd hh24:mi')
AND s2.MISPAR_KNISA=k1.ride_id)
AND   y.taarich =   TO_DATE(pDt,'yyyymmdd')  
AND y.taarich  = start_dt
AND y.mispar_ishi=driver_id
AND y.measher_o_mistayeg =1
AND  NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s
 	 				  	   WHERE s.taarich =   TO_DATE(pDt,'yyyymmdd')  
						   AND s.taarich  = start_dt
						   AND s.mispar_ishi=driver_id
						   AND s.mispar_sidur=schedule_num
						   AND s.shat_hatchala= TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')
						   AND s.shayah_leyom_kodem=1) ;
						   
		 CURSOR Peilut_Retro7 IS
		  SELECT    k1.driver_id,k1.start_dt,k1.schedule_num,branch,
 TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_schedule,4,0),3,2),'yyyymmdd hh24:mi')+1/1440  hatchala,
 TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_time,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_time,4,0),3,2),'yyyymmdd hh24:mi')  gmar,k1.ride_id,
 k1.makat_line,bus_number,k1.bus_sequence,k1.waiting_time,
 k1.spm_time,k1.spm_bus_number,k1.spm_schedule_num,k1.spm_start_time,k1.spm_makat_line,
 k1.spm_line_sign,k1.spm_location,-12 meadken
FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1,TB_YAMEY_AVODA_OVDIM y
WHERE  k1.start_dt= TO_DATE(pDt,'yyyymmdd')
AND k1.start_schedule<2400
AND k1.start_time<2400
 --and k1.schedule_num<100000
 AND k1.end_schedule=(SELECT MAX(k3.end_schedule) FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k3
WHERE k3.start_dt =  TO_DATE(pDt,'yyyymmdd')
AND k3.start_schedule<2400
AND k3.end_schedule<2400
AND k1.driver_id=k3.driver_id
AND k1.start_dt=k3.start_dt
AND k1.schedule_num=k3.schedule_num
AND k1.start_schedule=k3.start_schedule)
AND EXISTS (SELECT *  FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k2
WHERE k2.start_dt=  TO_DATE(pDt,'yyyymmdd')
AND k2.start_schedule<2400
AND k2.end_schedule<2400
AND k1.driver_id=k2.driver_id
AND k1.start_dt=k2.start_dt
AND k1.schedule_num=k2.schedule_num
AND k1.start_schedule=k2.start_schedule
AND k1.end_schedule<>k2.end_schedule)
AND   y.taarich =   TO_DATE(pDt,'yyyymmdd')  
AND y.taarich  = start_dt
AND y.mispar_ishi=driver_id
AND y.measher_o_mistayeg IS NULL
UNION ALL
 SELECT    k1.driver_id,k1.start_dt,k1.schedule_num,branch,
 TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_schedule,4,0),3,2),'yyyymmdd hh24:mi')+1/1440  hatchala,
 TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_time,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_time,4,0),3,2),'yyyymmdd hh24:mi')  gmar,k1.ride_id,
 k1.makat_line,bus_number,k1.bus_sequence,k1.waiting_time,
 k1.spm_time,k1.spm_bus_number,k1.spm_schedule_num,k1.spm_start_time,k1.spm_makat_line,
 k1.spm_line_sign,k1.spm_location,-12 meadken
FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1,TB_YAMEY_AVODA_OVDIM y
WHERE  k1.start_dt= TO_DATE(pDt,'yyyymmdd')
AND k1.start_schedule<2400
AND k1.start_time<2400
 --and k1.schedule_num<100000
 AND k1.end_schedule=(SELECT MAX(k3.end_schedule) FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k3
WHERE k3.start_dt =  TO_DATE(pDt,'yyyymmdd')
AND k3.start_schedule<2400
AND k3.end_schedule<2400
AND k1.driver_id=k3.driver_id
AND k1.start_dt=k3.start_dt
AND k1.schedule_num=k3.schedule_num
AND k1.start_schedule=k3.start_schedule)
AND EXISTS (SELECT *  FROM  kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k2
WHERE k2.start_dt=  TO_DATE(pDt,'yyyymmdd')
AND k2.start_schedule<2400
AND k2.end_schedule<2400
AND k1.driver_id=k2.driver_id
AND k1.start_dt=k2.start_dt
AND k1.schedule_num=k2.schedule_num
AND k1.start_schedule=k2.start_schedule
AND k1.end_schedule<>k2.end_schedule)
AND   y.taarich =   TO_DATE(pDt,'yyyymmdd')  
AND y.taarich  = start_dt
AND y.mispar_ishi=driver_id
AND y.measher_o_mistayeg =1
AND  NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s
 	 				  	   WHERE s.taarich =   TO_DATE(pDt,'yyyymmdd')  
						   AND s.taarich  = start_dt
						   AND s.mispar_ishi=driver_id
						   AND s.mispar_sidur=schedule_num
						   AND s.shat_hatchala= TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')
						   AND s.shayah_leyom_kodem=1) ;
						   
 		 CURSOR Peilut_Retro8 IS
  SELECT   DISTINCT    start_dt, driver_id,schedule_num,
                 TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi') hatchala,
				 TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_time,4,0),1,2)||':'||SUBSTR(LPAD(start_time,4,0),3,2),'yyyymmdd hh24:mi') moed,
				 makat_line,ride_id,  line_description
 FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
						   	WHERE  start_dt=  TO_DATE(pDt,'yyyymmdd')
							AND start_schedule<2400
							AND start_time<2400
							AND line_description IS NOT NULL
							AND ride_id>0
							AND makat_line<50000000;
																	  		 				   
						   
   BEGIN
--     err_str:='';
	 
   FOR  Peilut_Retro1_rec IN  Peilut_Retro1 LOOP
BEGIN
  INSERT INTO TB_PEILUT_OVDIM (mispar_ishi,taarich,mispar_sidur,  snif_tnua,shat_hatchala_sidur,shat_yetzia,mispar_knisa,
makat_nesia,oto_no,mispar_siduri_oto,kisuy_tor,
 Shat_Bhirat_Nesia_Netzer , Oto_No_Netzer, Mispar_Sidur_Netzer,  Shat_yetzia_Netzer,  Makat_Netzer, Shilut_Netzer ,
 Mikum_Bhirat_Nesia_Netzer ,meadken_acharon  )
  VALUES (Peilut_Retro1_rec.driver_id,Peilut_Retro1_rec.start_dt,  Peilut_Retro1_rec.schedule_num,  Peilut_Retro1_rec.branch,
Peilut_Retro1_rec.hatchala,Peilut_Retro1_rec.gmar,   Peilut_Retro1_rec.ride_id, Peilut_Retro1_rec.makat_line,Peilut_Retro1_rec.bus_number,Peilut_Retro1_rec.bus_sequence,
Peilut_Retro1_rec.waiting_time, Peilut_Retro1_rec.spm_time,Peilut_Retro1_rec.spm_bus_number,Peilut_Retro1_rec.spm_schedule_num,Peilut_Retro1_rec.spm_start_time,
Peilut_Retro1_rec.spm_makat_line, Peilut_Retro1_rec.spm_line_sign,Peilut_Retro1_rec.spm_location, Peilut_Retro1_rec.meadken );

EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,70) '||to_char(Peilut_Retro1_rec.driver_id) ||' '||to_char(Peilut_Retro1_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,40,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Peilut_Retro1_rec.driver_id) ||' '||TO_CHAR(Peilut_Retro1_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
 END;
END LOOP;
COMMIT;


FOR  Peilut_Retro2_rec IN  Peilut_Retro2 LOOP
BEGIN
  INSERT INTO TB_PEILUT_OVDIM (mispar_ishi,taarich,mispar_sidur,  snif_tnua,shat_hatchala_sidur,shat_yetzia,mispar_knisa,
makat_nesia,oto_no,mispar_siduri_oto,kisuy_tor,
 Shat_Bhirat_Nesia_Netzer , Oto_No_Netzer, Mispar_Sidur_Netzer,  Shat_yetzia_Netzer,  Makat_Netzer, Shilut_Netzer ,
 Mikum_Bhirat_Nesia_Netzer ,meadken_acharon  )
  VALUES (Peilut_Retro2_rec.driver_id,Peilut_Retro2_rec.start_dt,  Peilut_Retro2_rec.schedule_num,  Peilut_Retro2_rec.branch,
Peilut_Retro2_rec.hatchala,Peilut_Retro2_rec.gmar,   Peilut_Retro2_rec.ride_id, Peilut_Retro2_rec.makat_line,Peilut_Retro2_rec.bus_number,
Peilut_Retro2_rec.bus_sequence,Peilut_Retro2_rec.waiting_time, Peilut_Retro2_rec.spm_time,Peilut_Retro2_rec.spm_bus_number,
Peilut_Retro2_rec.spm_schedule_num,Peilut_Retro2_rec.spm_start_time,
Peilut_Retro2_rec.spm_makat_line, Peilut_Retro2_rec.spm_line_sign,Peilut_Retro2_rec.spm_location, Peilut_Retro2_rec.meadken );

EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,71) '|| to_char(Peilut_Retro2_rec.driver_id) ||' '||to_char(Peilut_Retro2_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,41,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Peilut_Retro2_rec.driver_id) ||' '||TO_CHAR(Peilut_Retro2_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
   END;
END LOOP;
COMMIT;

FOR  Peilut_Retro3_rec IN  Peilut_Retro3 LOOP
BEGIN
  INSERT INTO TB_PEILUT_OVDIM (mispar_ishi,taarich,mispar_sidur,  snif_tnua,shat_hatchala_sidur,shat_yetzia,mispar_knisa,
makat_nesia,oto_no,mispar_siduri_oto,kisuy_tor,
Shat_Bhirat_Nesia_Netzer , Oto_No_Netzer, Mispar_Sidur_Netzer,  Shat_yetzia_Netzer,  Makat_Netzer, Shilut_Netzer ,
 Mikum_Bhirat_Nesia_Netzer ,meadken_acharon  )
 VALUES (Peilut_Retro3_rec.driver_id,Peilut_Retro3_rec.start_dt,  Peilut_Retro3_rec.schedule_num,  Peilut_Retro3_rec.branch,
Peilut_Retro3_rec.hatchala,Peilut_Retro3_rec.gmar,   Peilut_Retro3_rec.ride_id, Peilut_Retro3_rec.makat_line,Peilut_Retro3_rec.bus_number,Peilut_Retro3_rec.bus_sequence,
Peilut_Retro3_rec.waiting_time, Peilut_Retro3_rec.spm_time,Peilut_Retro3_rec.spm_bus_number,Peilut_Retro3_rec.spm_schedule_num,Peilut_Retro3_rec.spm_start_time,
Peilut_Retro3_rec.spm_makat_line, Peilut_Retro3_rec.spm_line_sign,Peilut_Retro3_rec.spm_location, Peilut_Retro3_rec.meadken );

EXCEPTION
   WHEN OTHERS THEN
 --  err_str:=err_str||' (4,72) '||to_char(Peilut_Retro3_rec.driver_id) ||' '||to_char(Peilut_Retro3_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,42,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Peilut_Retro3_rec.driver_id) ||' '||TO_CHAR(Peilut_Retro3_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
 END;
END LOOP;
COMMIT;
   

FOR  Peilut_Retro4_rec IN  Peilut_Retro4 LOOP
BEGIN
  INSERT INTO TB_PEILUT_OVDIM (mispar_ishi,taarich,mispar_sidur,  snif_tnua,shat_hatchala_sidur,shat_yetzia,mispar_knisa,
makat_nesia,oto_no,mispar_siduri_oto,kisuy_tor,
 Shat_Bhirat_Nesia_Netzer , Oto_No_Netzer, Mispar_Sidur_Netzer,  Shat_yetzia_Netzer,  Makat_Netzer, Shilut_Netzer ,
 Mikum_Bhirat_Nesia_Netzer  ,meadken_acharon )
  VALUES (Peilut_Retro4_rec.driver_id,Peilut_Retro4_rec.start_dt,  Peilut_Retro4_rec.schedule_num,  Peilut_Retro4_rec.branch,
Peilut_Retro4_rec.hatchala,Peilut_Retro4_rec.gmar,   Peilut_Retro4_rec.ride_id, Peilut_Retro4_rec.makat_line,Peilut_Retro4_rec.bus_number,Peilut_Retro4_rec.bus_sequence,
Peilut_Retro4_rec.waiting_time, Peilut_Retro4_rec.spm_time,Peilut_Retro4_rec.spm_bus_number,Peilut_Retro4_rec.spm_schedule_num,Peilut_Retro4_rec.spm_start_time,
Peilut_Retro4_rec.spm_makat_line, Peilut_Retro4_rec.spm_line_sign,Peilut_Retro4_rec.spm_location, Peilut_Retro4_rec.meadken );

EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,73) '|| to_char(Peilut_Retro4_rec.driver_id) ||' '||to_char(Peilut_Retro4_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,43,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Peilut_Retro4_rec.driver_id) ||' '||TO_CHAR(Peilut_Retro4_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
  END;
END LOOP;
COMMIT;

FOR  Peilut_Retro5_rec IN  Peilut_Retro5 LOOP
BEGIN
  INSERT INTO TB_PEILUT_OVDIM (mispar_ishi,taarich,mispar_sidur,  snif_tnua,shat_hatchala_sidur,shat_yetzia,mispar_knisa,
makat_nesia,oto_no,mispar_siduri_oto,kisuy_tor,
Shat_Bhirat_Nesia_Netzer , Oto_No_Netzer, Mispar_Sidur_Netzer,  Shat_yetzia_Netzer,  Makat_Netzer, Shilut_Netzer ,
 Mikum_Bhirat_Nesia_Netzer ,meadken_acharon  )
 VALUES (Peilut_Retro5_rec.driver_id,Peilut_Retro5_rec.start_dt,  Peilut_Retro5_rec.schedule_num,  Peilut_Retro5_rec.branch,
Peilut_Retro5_rec.hatchala,Peilut_Retro5_rec.gmar,   Peilut_Retro5_rec.ride_id, Peilut_Retro5_rec.makat_line,Peilut_Retro5_rec.bus_number,Peilut_Retro5_rec.bus_sequence,
Peilut_Retro5_rec.waiting_time, Peilut_Retro5_rec.spm_time,Peilut_Retro5_rec.spm_bus_number,Peilut_Retro5_rec.spm_schedule_num,Peilut_Retro5_rec.spm_start_time,
Peilut_Retro5_rec.spm_makat_line, Peilut_Retro5_rec.spm_line_sign,Peilut_Retro5_rec.spm_location, Peilut_Retro5_rec.meadken );

EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,74) '||to_char(Peilut_Retro5_rec.driver_id) ||' '||to_char(Peilut_Retro5_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,44,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Peilut_Retro5_rec.driver_id) ||' '||TO_CHAR(Peilut_Retro5_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
   END;
END LOOP;
COMMIT;

FOR  Peilut_Retro6_rec IN  Peilut_Retro6 LOOP
BEGIN
 INSERT INTO TB_PEILUT_OVDIM (mispar_ishi,taarich,mispar_sidur,  snif_tnua,shat_hatchala_sidur,shat_yetzia,mispar_knisa,
makat_nesia,oto_no,mispar_siduri_oto,kisuy_tor,
 Shat_Bhirat_Nesia_Netzer , Oto_No_Netzer, Mispar_Sidur_Netzer,  Shat_yetzia_Netzer,  Makat_Netzer, Shilut_Netzer ,
 Mikum_Bhirat_Nesia_Netzer ,meadken_acharon  )
  VALUES (Peilut_Retro6_rec.driver_id,Peilut_Retro6_rec.start_dt,  Peilut_Retro6_rec.schedule_num,  Peilut_Retro6_rec.branch,
Peilut_Retro6_rec.hatchala,Peilut_Retro6_rec.gmar,   Peilut_Retro6_rec.ride_id, Peilut_Retro6_rec.makat_line,Peilut_Retro6_rec.bus_number,Peilut_Retro6_rec.bus_sequence,
Peilut_Retro6_rec.waiting_time, Peilut_Retro6_rec.spm_time,Peilut_Retro6_rec.spm_bus_number,Peilut_Retro6_rec.spm_schedule_num,Peilut_Retro6_rec.spm_start_time,
Peilut_Retro6_rec.spm_makat_line, Peilut_Retro6_rec.spm_line_sign,Peilut_Retro6_rec.spm_location, Peilut_Retro6_rec.meadken );

EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,75) '|| to_char(Peilut_Retro6_rec.driver_id) ||' '||to_char(Peilut_Retro6_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,45,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Peilut_Retro6_rec.driver_id) ||' '||TO_CHAR(Peilut_Retro6_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
    END;
END LOOP;
COMMIT;

FOR  Peilut_Retro7_rec IN  Peilut_Retro7 LOOP
BEGIN
 INSERT INTO TB_PEILUT_OVDIM (mispar_ishi,taarich,mispar_sidur, snif_tnua, shat_hatchala_sidur,shat_yetzia,mispar_knisa,
makat_nesia,oto_no,mispar_siduri_oto,kisuy_tor,
 Shat_Bhirat_Nesia_Netzer , Oto_No_Netzer, Mispar_Sidur_Netzer,  Shat_yetzia_Netzer,  Makat_Netzer, Shilut_Netzer ,
 Mikum_Bhirat_Nesia_Netzer ,meadken_acharon  )
 VALUES (Peilut_Retro7_rec.driver_id,Peilut_Retro7_rec.start_dt,  Peilut_Retro7_rec.schedule_num,  Peilut_Retro7_rec.branch,
Peilut_Retro7_rec.hatchala,Peilut_Retro7_rec.gmar,   Peilut_Retro7_rec.ride_id, Peilut_Retro7_rec.makat_line,Peilut_Retro7_rec.bus_number,Peilut_Retro7_rec.bus_sequence,
Peilut_Retro7_rec.waiting_time, Peilut_Retro7_rec.spm_time,Peilut_Retro7_rec.spm_bus_number,Peilut_Retro7_rec.spm_schedule_num,Peilut_Retro7_rec.spm_start_time,
Peilut_Retro7_rec.spm_makat_line, Peilut_Retro7_rec.spm_line_sign,Peilut_Retro7_rec.spm_location, Peilut_Retro7_rec.meadken );

EXCEPTION
   WHEN OTHERS THEN
 --  err_str:=err_str||' (4,76) '||to_char(Peilut_Retro7_rec.driver_id) ||' '||to_char(Peilut_Retro7_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,46,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Peilut_Retro7_rec.driver_id) ||' '||TO_CHAR(Peilut_Retro7_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
    END;
END LOOP;
COMMIT;

	
BEGIN
 UPDATE TB_PEILUT_OVDIM
 SET imut_netzer=1
 WHERE taarich= TO_DATE(pDt,'yyyymmdd')
  AND Oto_No_Netzer>0 ;
 --and (Oto_No_Netzer>0 or Mispar_Sidur_Netzer>0  );
EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,77) '|| 'update_imut_peilut_retro  '||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,47,SYSDATE,'',10,'',SUBSTR('update_imut_peilut_retro '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
END;
COMMIT;


 BEGIN
 UPDATE TB_PEILUT_OVDIM
SET mispar_matala=mispar_sidur
 WHERE taarich= TO_DATE(pDt,'yyyymmdd')
 AND mispar_sidur< 1000;
 EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,78) '|| 'update_matala_peilut_retro  '||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,48,SYSDATE,'',10,'',SUBSTR('update_matala_peilut_retro '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
   END;
   COMMIT;

FOR  Peilut_Retro8_rec IN  Peilut_Retro8 LOOP
BEGIN
UPDATE TB_PEILUT_OVDIM
SET teur_nesia=    Peilut_Retro8_rec.line_description
WHERE taarich= TO_DATE(pDt,'yyyymmdd')
AND makat_nesia<50000000
AND mispar_knisa>0
AND TRUNC(shat_hatchala_sidur)=taarich
AND TRUNC(shat_yetzia)=taarich
							AND Peilut_Retro8_rec.driver_id=mispar_ishi
							AND Peilut_Retro8_rec.schedule_num=mispar_sidur
							AND Peilut_Retro8_rec.hatchala =shat_hatchala_sidur
							AND Peilut_Retro8_rec.moed =shat_yetzia
							AND Peilut_Retro8_rec.makat_line=makat_nesia
							AND Peilut_Retro8_rec.ride_id=mispar_knisa;

EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,79) '|| to_char(Peilut_Retro8_rec.driver_id) ||' '||to_char(Peilut_Retro8_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,49,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Peilut_Retro8_rec.driver_id) ||' '||TO_CHAR(Peilut_Retro8_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
    END;
END LOOP;
COMMIT;
  
  BEGIN
UPDATE TB_PEILUT_OVDIM
SET teur_nesia=( SELECT    line_description
				 		   FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
						   	WHERE  start_dt= taarich
							AND driver_id=mispar_ishi
							AND schedule_num=mispar_sidur
							AND TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')=shat_hatchala_sidur
							 AND TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(start_time-2400,4,0),1,2)||':'||SUBSTR(LPAD(start_time,4,0),3,2),'yyyymmdd hh24:mi')=shat_yetzia
							AND makat_line=makat_nesia
							AND ride_id=mispar_knisa
							AND start_schedule<2400
							AND start_time>2359
							AND line_description IS NOT NULL
				   			AND ride_id>0
							AND makat_line<50000000)
WHERE taarich= TO_DATE(pDt,'yyyymmdd')
AND makat_nesia<50000000
AND mispar_knisa>0
AND TRUNC(shat_hatchala_sidur)=taarich
AND TRUNC(shat_yetzia)=taarich+1;
EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,80) '||'update_teur_peilut_retro  '||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,50,SYSDATE,'',10,'',SUBSTR('update_teur_peilut_retro '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
END;
COMMIT;

 
  BEGIN
  UPDATE TB_PEILUT_OVDIM
SET teur_nesia=( SELECT    line_description
				 		   FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
						   	WHERE  start_dt= taarich
							AND driver_id=mispar_ishi
							AND schedule_num=mispar_sidur
							AND TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule-2400,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')=shat_hatchala_sidur
							 AND TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(start_time-2400,4,0),1,2)||':'||SUBSTR(LPAD(start_time,4,0),3,2),'yyyymmdd hh24:mi')=shat_yetzia
							AND makat_line=makat_nesia
							AND ride_id=mispar_knisa
							AND start_schedule>2359
							AND start_time>2359
							AND line_description IS NOT NULL
				   			AND ride_id>0
							AND makat_line<50000000)
WHERE taarich= TO_DATE(pDt,'yyyymmdd')
AND makat_nesia<50000000
AND mispar_knisa>0
AND TRUNC(shat_hatchala_sidur)=taarich+1
AND TRUNC(shat_yetzia)=taarich+1;
EXCEPTION
   WHEN OTHERS THEN
--    err_str:=err_str||' (4,81) '||'update_teur_peilut_retro  '||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,51,SYSDATE,'',10,'',SUBSTR('update_teur_peilut_retro '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
END;
COMMIT;

   BEGIN
UPDATE TB_PEILUT_OVDIM
SET teur_nesia=( SELECT    line_description
				 		   FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
						   	WHERE  start_dt= taarich
							AND driver_id=mispar_ishi
							AND schedule_num=mispar_sidur
							AND TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')=shat_hatchala_sidur
							AND TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_time,4,0),1,2)||':'||SUBSTR(LPAD(start_time,4,0),3,2),'yyyymmdd hh24:mi')=shat_yetzia
							AND makat_line=makat_nesia
							AND ride_id=mispar_knisa
							AND start_schedule<2400
							AND start_time<2400
							AND line_description IS NOT NULL
							AND NVL(makat_line,0)=0
							AND schedule_num<1000)
WHERE taarich= TO_DATE(pDt,'yyyymmdd')
AND NVL(makat_nesia,0)=0
AND mispar_sidur<1000
AND TRUNC(shat_hatchala_sidur)=taarich
AND TRUNC(shat_yetzia)=taarich;
EXCEPTION
   WHEN OTHERS THEN
 --   err_str:=err_str||' (4,82) '||'update_teur_peilut_retro  '||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,52,SYSDATE,'',10,'',SUBSTR('update_teur_peilut_retro '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
END;
COMMIT;

 
  BEGIN
  UPDATE TB_PEILUT_OVDIM
SET teur_nesia=( SELECT    line_description
				 		   FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
						   	WHERE  start_dt= taarich
							AND driver_id=mispar_ishi
							AND schedule_num=mispar_sidur
							AND TO_DATE(TO_CHAR(start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')=shat_hatchala_sidur
							 AND TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(start_time-2400,4,0),1,2)||':'||SUBSTR(LPAD(start_time,4,0),3,2),'yyyymmdd hh24:mi')=shat_yetzia
							AND makat_line=makat_nesia
							AND ride_id=mispar_knisa
							AND start_schedule<2400
							AND start_time>2359
							AND line_description IS NOT NULL
							AND NVL(makat_line,0)=0
							AND schedule_num<1000)
WHERE taarich= TO_DATE(pDt,'yyyymmdd')
AND NVL(makat_nesia,0)=0
AND mispar_sidur<1000
AND TRUNC(shat_hatchala_sidur)=taarich
AND TRUNC(shat_yetzia)=taarich+1;
EXCEPTION
   WHEN OTHERS THEN
 --  err_str:=err_str||' (4,83) '||'update_teur_peilut_retro  '||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,53,SYSDATE,'',10,'',SUBSTR('update_teur_peilut_retro '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
  END;
  COMMIT;

  BEGIN 
UPDATE TB_PEILUT_OVDIM
SET teur_nesia=( SELECT    line_description
				 		   FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
						   	WHERE  start_dt= taarich
							AND driver_id=mispar_ishi
							AND schedule_num=mispar_sidur
							AND TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(start_schedule-2400,4,0),1,2)||':'||SUBSTR(LPAD(start_schedule,4,0),3,2),'yyyymmdd hh24:mi')=shat_hatchala_sidur
							 AND TO_DATE(TO_CHAR(start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(start_time-2400,4,0),1,2)||':'||SUBSTR(LPAD(start_time,4,0),3,2),'yyyymmdd hh24:mi')=shat_yetzia
							AND makat_line=makat_nesia
							AND ride_id=mispar_knisa
							AND start_schedule>2359
							AND start_time>2359
							AND line_description IS NOT NULL
							AND NVL(makat_line,0)=0
							AND schedule_num<1000)
WHERE taarich= TO_DATE(pDt,'yyyymmdd')
AND NVL(makat_nesia,0)=0
AND mispar_sidur<1000
AND TRUNC(shat_hatchala_sidur)=taarich+1
AND TRUNC(shat_yetzia)=taarich+1;
EXCEPTION
   WHEN OTHERS THEN
--   err_str:=err_str||' (4,84) '|| 'update_teur_peilut_retro  '||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,54,SYSDATE,'',10,'',SUBSTR('update_teur_peilut_retro '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
 END;
 COMMIT;

	
END pro_ins_peilut_retroSdrn;


PROCEDURE pro_ins_YamimOfSdrn  IS
idNumber NUMBER;
err_str VARCHAR(1000);
		
CURSOR  stam1 IS
SELECT teur_tech
 FROM TB_LOG_TAHALICH
WHERE   taarich>SYSDATE-0.01
AND kod_tahalich=4
AND kod_peilut_tahalich=1
AND seq=1
ORDER BY taarich;

 BEGIN
 idNumber:=0;
err_str:='';

   pro_ins_yamim_4_sidurim(TO_CHAR(SYSDATE-1,'yyyymmdd'));
   --test new procedures:
   --pro_ins_yamim_from_sidurim(TO_CHAR(SYSDATE-1,'yyyymmdd'));

FOR  stam1_rec IN  stam1  LOOP
BEGIN
err_str:=err_str ||trim(stam1_rec.teur_tech);
END;
END LOOP;
COMMIT;

SELECT COUNT(*)
 INTO idNumber
 FROM TB_LOG_TAHALICH
WHERE   taarich>SYSDATE-0.01  
AND kod_tahalich=4
AND seq=1
AND kod_peilut_tahalich=1;

	IF (idNumber >0) THEN
--	insert into tb_log_tahalich
--		values (4,2,2,sysdate,'',10,'',substr(to_char(77690)||' '||err_str,1,100));
    RAISE_APPLICATION_ERROR(-20001, SUBSTR(err_str,1,100), TRUE);
 END IF;
 
  EXCEPTION
     WHEN NO_DATA_FOUND  THEN
        						 idNumber:=0;
   WHEN OTHERS THEN
        RAISE;

END pro_ins_YamimOfSdrn; 

PROCEDURE pro_ins_SidurimOfSdrn   IS
idNumber NUMBER;
err_str VARCHAR(1000);
		
CURSOR  stam2 IS
SELECT teur_tech
 FROM TB_LOG_TAHALICH
WHERE   taarich>SYSDATE-0.01
AND kod_tahalich=4
AND kod_peilut_tahalich=1
AND seq BETWEEN 21 AND 29
UNION ALL
SELECT teur_tech
 FROM TB_LOG_TAHALICH
WHERE   taarich>SYSDATE-0.01
AND kod_tahalich=4
AND kod_peilut_tahalich=1
AND seq =2;

 BEGIN
 idNumber:=0;
err_str:='';

   --pro_ins_sidurim_4_sidurim(TO_CHAR(SYSDATE-1,'yyyymmdd'));
      -- new procedures:
   pro_ins_sidurim_from_sdrm(TO_CHAR(SYSDATE-1,'yyyymmdd'));


FOR  stam2_rec IN  stam2  LOOP
BEGIN
err_str:=err_str ||trim(stam2_rec.teur_tech);
END;
END LOOP;
COMMIT;

SELECT COUNT(*)
 INTO idNumber
 FROM TB_LOG_TAHALICH
WHERE   taarich>SYSDATE-0.01  
AND kod_tahalich=4
AND kod_peilut_tahalich=1
AND (seq=2
OR seq BETWEEN 21 AND 29);

	IF (idNumber >0) THEN
--	insert into tb_log_tahalich
--		values (4,2,2,sysdate,'',10,'',substr(to_char(77690)||' '||err_str,1,100));
    RAISE_APPLICATION_ERROR(-20002, SUBSTR(err_str,1,100), TRUE);
 END IF;
 
  EXCEPTION
     WHEN NO_DATA_FOUND  THEN
        						 idNumber:=0;
   WHEN OTHERS THEN
        RAISE;

END pro_ins_SidurimOfSdrn; 

PROCEDURE pro_ins_PeilutOfSdrn   IS
idNumber NUMBER;
err_str VARCHAR(1000);
		
CURSOR  stam3 IS
SELECT teur_tech
 FROM TB_LOG_TAHALICH
WHERE   taarich>SYSDATE-0.01
AND kod_tahalich=4
AND kod_peilut_tahalich=1
AND seq BETWEEN 30 AND 45;

 BEGIN
 idNumber:=0;
err_str:='';

   --pro_ins_peilut_4_sidurim(TO_CHAR(SYSDATE-1,'yyyymmdd'));
      -- new procedures:
   pro_ins_peilut_from_sdrm(TO_CHAR(SYSDATE-1,'yyyymmdd'));


FOR  stam3_rec IN  stam3  LOOP
BEGIN
err_str:=err_str ||trim(stam3_rec.teur_tech);
END;
END LOOP;
COMMIT;

SELECT COUNT(*)
 INTO idNumber
 FROM TB_LOG_TAHALICH
WHERE   taarich>SYSDATE-0.01  
AND kod_tahalich=4
AND kod_peilut_tahalich=1
AND seq BETWEEN 30 AND 45;

	IF (idNumber >0) THEN
--	insert into tb_log_tahalich
--		values (4,2,2,sysdate,'',10,'',substr(to_char(77690)||' '||err_str,1,100));
    RAISE_APPLICATION_ERROR(-20003, SUBSTR(err_str,1,100), TRUE);
 END IF;
 
  EXCEPTION
     WHEN NO_DATA_FOUND  THEN
        						 idNumber:=0;
   WHEN OTHERS THEN
        RAISE;

END pro_ins_PeilutOfSdrn; 

PROCEDURE pro_upd_CtrlOfSdrn   IS
idNumber NUMBER;
err_str VARCHAR(1000);
		
CURSOR  stam7 IS
SELECT teur_tech
 FROM TB_LOG_TAHALICH
WHERE   taarich>SYSDATE-0.01
AND kod_tahalich=4
AND kod_peilut_tahalich=6;

 BEGIN
 idNumber:=0;
err_str:='';

   pro_upd_sdrm_control(TO_CHAR(SYSDATE-1,'yyyymmdd'));
  FOR  stam7_rec IN  stam7  LOOP
BEGIN
err_str:=err_str ||trim(stam7_rec.teur_tech);
END;
END LOOP;
COMMIT;

SELECT COUNT(*)
 INTO idNumber
 FROM TB_LOG_TAHALICH
WHERE   taarich>SYSDATE-0.01  
AND kod_tahalich=4
AND kod_peilut_tahalich=6;

	IF (idNumber >0) THEN
    RAISE_APPLICATION_ERROR(-20007, SUBSTR(err_str,1,100), TRUE);
 END IF;
 
  EXCEPTION
     WHEN NO_DATA_FOUND  THEN
        						 idNumber:=0;
   WHEN OTHERS THEN
        RAISE;

END pro_upd_CtrlOfSdrn; 
  
   PROCEDURE pro_chk_Dt4_rerun IS
--err_str  varchar2(1000);
 idNumber NUMBER;
err_str VARCHAR(1000);
		
CURSOR  stam4 IS
SELECT teur_tech
 FROM TB_LOG_TAHALICH
WHERE   taarich>SYSDATE-0.01
AND kod_tahalich=15
AND kod_peilut_tahalich=1
--and seq=1
UNION ALL
SELECT teur_tech
 FROM TB_LOG_TAHALICH
WHERE   taarich>SYSDATE-0.01
AND kod_tahalich=4
AND kod_peilut_tahalich=1;
--and seq=1
--order by taarich;

   CURSOR Dt4_rerun IS
       SELECT TO_CHAR(start_dt,'yyyymmdd') start_dt
    FROM   kds.kds_control_driver_activities@kds2sdrm
       WHERE start_dt>=SYSDATE-(SELECT erech_param FROM TB_PARAMETRIM WHERE kod_param=100)
	AND status=2  --4
--	 AND EXISTS (SELECT y.mispar_ishi
--  FROM TB_YAMEY_AVODA_OVDIM y
--WHERE  y.taarich= start_dt 
--AND y.taarich>=SYSDATE-(SELECT erech_param FROM TB_PARAMETRIM WHERE kod_param=100)
-- AND measher_o_mistayeg IS NULL
-- UNION ALL
-- SELECT y.mispar_ishi
--  FROM TB_YAMEY_AVODA_OVDIM y
--WHERE  y.taarich= start_dt 
--AND y.taarich>=SYSDATE-(SELECT erech_param FROM TB_PARAMETRIM WHERE kod_param=100)
-- AND measher_o_mistayeg =1
--AND  NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s
--  	 	 				  	   WHERE  s.taarich= start_dt 
--							   AND s.taarich=y.taarich
--							   AND s.taarich>=SYSDATE-(SELECT erech_param FROM TB_PARAMETRIM WHERE kod_param=100)
--							   AND s.mispar_ishi=y.mispar_ishi
--						   AND (s.mispar_sidur<99000 OR s.mispar_sidur>99999)))  
	ORDER BY start_dt;

BEGIN
  --err_str:='';
 idNumber:=0;
err_str:='';

  
FOR  Dt4_rerun_rec IN  Dt4_rerun LOOP
BEGIN
Pkg_Sdrn.pro_retro1(Dt4_rerun_rec.start_dt);
Pkg_Batch.pro_upd_yamey_avoda_ovdim(Dt4_rerun_rec.start_dt);
Pkg_Sdrn.pro_upd_sdrnRerun_control(Dt4_rerun_rec.start_dt);

---- insert into  tb_log_tahalich
----values (15,1,1,to_date(Dt4_rerun_rec.start_dt,'yyyymmdd'),0,0,sysdate,'');
--Pkg_Sdrn.pro_ins_yamim_4_sidurim(Dt4_rerun_rec.start_dt);
---- insert into  tb_log_tahalich
----values (15,1,2,to_date(Dt4_rerun_rec.start_dt,'yyyymmdd'),0,0,sysdate,'');
--Pkg_Sdrn.pro_TrailNDel_peilut_4retrSdrn(Dt4_rerun_rec.start_dt);
---- insert into  tb_log_tahalich
----values (15,1,3,to_date(Dt4_rerun_rec.start_dt,'yyyymmdd'),0,0,sysdate,'');
--Pkg_Sdrn.pro_TrailNDel_sidurim_4reSdrn(Dt4_rerun_rec.start_dt);
---- insert into  tb_log_tahalich
----values (15,1,4,to_date(Dt4_rerun_rec.start_dt,'yyyymmdd'),0,0,sysdate,'');
--Pkg_Sdrn.pro_ins_sidurim_retroSdrn(Dt4_rerun_rec.start_dt);
---- insert into  tb_log_tahalich
----values (15,1,5,to_date(Dt4_rerun_rec.start_dt,'yyyymmdd'),0,0,sysdate,'');
--Pkg_Sdrn.pro_ins_peilut_retroSdrn(Dt4_rerun_rec.start_dt);
---- insert into  tb_log_tahalich
----values (15,1,6,to_date(Dt4_rerun_rec.start_dt,'yyyymmdd'),0,0,sysdate,'');
--Pkg_Batch.pro_upd_yamey_avoda_ovdim(Dt4_rerun_rec.start_dt);
---- insert into  tb_log_tahalich
----values (15,1,7,to_date(Dt4_rerun_rec.start_dt,'yyyymmdd'),0,0,sysdate,'');
--	  -- todo: find a way to differ tst & prd using this procedure  pro_upd_sdrntstRerun_control
---- prd: 
----PKG_sdrn.pro_upd_sdrnRerun_control(Dt4_rerun_rec.start_dt);
---- tst:
--Pkg_Sdrn.pro_upd_sdrntstRerun_control(Dt4_rerun_rec.start_dt);
---- insert into  tb_log_tahalich
----values (15,1,8,to_date(Dt4_rerun_rec.start_dt,'yyyymmdd'),0,0,sysdate,'');
END;
END LOOP;
COMMIT;


FOR  stam4_rec IN  stam4  LOOP
BEGIN
err_str:=err_str ||trim(stam4_rec.teur_tech);
END;
END LOOP;
COMMIT;

SELECT COUNT(*)
 INTO idNumber
 FROM TB_LOG_TAHALICH
WHERE   taarich>SYSDATE-0.01  
AND (kod_tahalich=15 OR   kod_tahalich=4)
--and seq=1
AND kod_peilut_tahalich=1;

	IF (idNumber >0) THEN
    RAISE_APPLICATION_ERROR(-20004, SUBSTR(err_str,1,100), TRUE);
 END IF;
 
  EXCEPTION
     WHEN NO_DATA_FOUND  THEN
        						 idNumber:=0;
   WHEN OTHERS THEN
        RAISE;
  
END pro_chk_Dt4_rerun;

PROCEDURE pro_upd_sdrntstRerun_control(pDt VARCHAR) IS
     BEGIN
      UPDATE  kds.kds_control_driver_activities@kds2sdrm
     SET status=9
      WHERE status=4
      AND  start_dt = TO_DATE(pDt,'yyyymmdd') ;
        EXCEPTION
   WHEN OTHERS THEN
   INSERT INTO TB_LOG_TAHALICH
	VALUES (15,1,55,SYSDATE,'',10,'',SUBSTR('update_teur_peilut_retro '||pDt||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
COMMIT; 
--        RAISE;
 END pro_upd_sdrntstRerun_control;

 
-- PROCEDURE pro_stam    IS
-- err_str  varchar2(1000);
 
-- cursor stam_dt is
-- select to_char(sysdate,'dd') kk from dual
--union all
--select to_char(sysdate+1,'dd') kk from dual;

--BEGIN
--  err_str:='';

--FOR  stam_dt_rec IN  stam_dt LOOP
--begin
--if    stam_dt_rec.kk='27' then
--       insert into  y_miri_stam
--values (55,'20110230',sysdate,sysdate,2);

--end if;
--if    stam_dt_rec.kk='28' then
--      insert into  y_miri_stam
--values (555555,sysdate,sysdate,sysdate,2);
--end if;
 
--EXCEPTION
--   WHEN OTHERS THEN
-- -- err_str:=err_str||' (15,1) '||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
-- 	insert into tb_log_tahalich
--	values (4,1,1,sysdate,'',10,'',substr(to_char(77690)||' '||DBMS_UTILITY.format_error_stack,1,100));
--end;
--END LOOP;
--commit;
   
   
-- --  if  not err_str=''  then 
------  raise_application_error(15, SUBSTR     (err_str,1,1000), TRUE);
---- insert into tb_log_tahalich
----	 values (5,5,5,sysdate,'',10,sysdate,substr(err_str,1,100));
----  end if;
----  commit;

   
--END pro_stam;

PROCEDURE pro_stam2   IS
idNumber NUMBER;
  BEGIN
  	 idNumber:=0;

--	 pro_stam;

 SELECT COUNT(*)
 INTO idNumber
 FROM TB_LOG_TAHALICH
WHERE   taarich>SYSDATE-0.01  
AND kod_tahalich=4
AND kod_peilut_tahalich=1;

	IF (idNumber >0) THEN
	 RAISE_APPLICATION_ERROR(-2005, 'log_tahalich', TRUE);
 END IF;
 
  EXCEPTION
     WHEN NO_DATA_FOUND  THEN
        						 idNumber:=0;
   WHEN OTHERS THEN
        RAISE;
		
END pro_stam2;


PROCEDURE pro_stam3   IS
err_str VARCHAR(1000);
idNumber NUMBER;

CURSOR  stam1 IS
SELECT teur_tech
 FROM TB_LOG_TAHALICH
WHERE   taarich>SYSDATE-1
AND kod_tahalich=4
AND kod_peilut_tahalich=1
ORDER BY taarich;

BEGIN
err_str:='';
idNumber:=0;

FOR  stam1_rec IN  stam1  LOOP
BEGIN
err_str:=err_str ||trim(stam1_rec.teur_tech);
END;
END LOOP;
COMMIT;

SELECT COUNT(*)
 INTO idNumber
 FROM TB_LOG_TAHALICH
WHERE   taarich>SYSDATE-1  
AND kod_tahalich=4
AND kod_peilut_tahalich=1;

	IF (idNumber >0) THEN
--	insert into tb_log_tahalich
--		values (4,2,2,sysdate,'',10,'',substr(to_char(77690)||' '||err_str,1,100));
  --   ERR_MESSAGE:=substr(err_str,1,100);
    RAISE_APPLICATION_ERROR(-20001, SUBSTR(err_str,1,100), TRUE);
 END IF;
 
  EXCEPTION
     WHEN NO_DATA_FOUND  THEN
        						 idNumber:=0;
   WHEN OTHERS THEN
        RAISE;
END pro_stam3;
		
      PROCEDURE pro_stam4(pDt VARCHAR)  IS
-- problem? if start_dt+1 is today and is in the future??
	  CURSOR Sidurim9  IS
SELECT   DISTINCT k1.driver_id,k1.start_dt,k1.schedule_num,
TO_DATE(TO_CHAR(k1.start_dt,'yyyymmdd')||' '||SUBSTR(LPAD(k1.start_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.start_schedule,4,0),3,2),'yyyymmdd hh24:mi') hatchala,
TO_DATE(TO_CHAR(k1.start_dt+1,'yyyymmdd')||' '||SUBSTR(LPAD(k1.end_schedule,4,0),1,2)||':'||SUBSTR(LPAD(k1.end_schedule,4,0),3,2),'yyyymmdd hh24:mi') gmar
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1
WHERE start_dt= TO_DATE(pDt,'yyyymmdd')
AND k1.start_schedule<2400
AND k1.end_schedule<2400
AND DECODE(sug_visa,' ','',sug_visa) IN (0,1)
AND  k1.start_schedule>k1.end_schedule;

BEGIN

FOR  Sidurim9_rec IN  Sidurim9 LOOP
BEGIN
UPDATE TB_SIDURIM_OVDIM 
SET shat_gmar=Sidurim9_rec.gmar
WHERE mispar_ishi=Sidurim9_rec.driver_id
AND taarich=Sidurim9_rec.start_dt
AND mispar_sidur=Sidurim9_rec.schedule_num
AND shat_hatchala=Sidurim9_rec.hatchala
AND sector_visa IN (0,1);

EXCEPTION
   WHEN OTHERS THEN
 --  err_str:=err_str||' (4,28) '|| to_char(Sidurim8_rec.driver_id) ||' '||to_char(Sidurim8_rec.schedule_num)||' '||DBMS_UTILITY.format_error_stack||chr(10)||chr(13);
	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,2,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Sidurim9_rec.driver_id) ||' '||TO_CHAR(Sidurim9_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));   

END;
END LOOP;
COMMIT;

END pro_stam4;


     PROCEDURE pro_retro1(pDt VARCHAR)  IS
idNumber NUMBER;

		 --cursor1 & cursor2 are for delete from peiluyot and sidurim
		 --sidurim_cursor1 & 3 are to insert into sidurim where count=1
		 --sidurim_cursor2 & 4 are to insert into sidurim where count>1
	 	 --peiluyot1: insert into  peiluyot	
	 --cursor5 & 8: update teur_nesia in  peiluyot


	 --cursor1: measher_mistayeg null, 
	 --			if sidur is not shaonim 99000-99999 then delete
	 --			if sidur is visa/matala 99000-99999 and sector_visa (0,1) then delete
	 CURSOR retro1 IS
	 SELECT s.mispar_sidur,s.mispar_ishi,s.taarich,s.shat_hatchala
FROM TB_SIDURIM_OVDIM s,TB_YAMEY_AVODA_OVDIM y  
WHERE s.taarich=TO_DATE(pDt,'yyyymmdd')
AND s.mispar_sidur NOT BETWEEN 99000 AND 99999
AND  y.taarich =TO_DATE(pDt,'yyyymmdd')
AND y.measher_o_mistayeg IS NULL
AND y.mispar_ishi=s.mispar_ishi
AND y.taarich=s.taarich
UNION ALL
SELECT s.mispar_sidur,s.mispar_ishi,s.taarich,s.shat_hatchala
FROM TB_SIDURIM_OVDIM s,TB_YAMEY_AVODA_OVDIM y  
WHERE s.taarich =TO_DATE(pDt,'yyyymmdd')
AND s.mispar_sidur BETWEEN 99000 AND 99999
AND s.sector_visa BETWEEN 0 AND 1
AND  y.taarich =TO_DATE(pDt,'yyyymmdd')
AND y.measher_o_mistayeg IS NULL
AND y.mispar_ishi=s.mispar_ishi
AND y.taarich=s.taarich; 
	 
 	 --cursor2: measher_mistayeg =1 and no sdrn at all
	 --			no sdrn is not any sidur<99000 & not any sidur>90999
	 --			and not any sidur between 99000 and 99999 and sector_visa (0,1)
	 CURSOR retro2 IS
	 	 SELECT y.mispar_ishi,y.taarich
FROM TB_YAMEY_AVODA_OVDIM y  
WHERE y.taarich =TO_DATE(pDt,'yyyymmdd')
AND y.measher_o_mistayeg =1
AND NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM s1
WHERE s1.taarich =TO_DATE(pDt,'yyyymmdd')
AND  s1.mispar_sidur NOT BETWEEN 99000 AND 99999
AND y.mispar_ishi=s1.mispar_ishi
AND y.taarich=s1.taarich
UNION ALL
SELECT * FROM TB_SIDURIM_OVDIM s2
WHERE  s2.taarich =TO_DATE(pDt,'yyyymmdd') 
AND s2.mispar_sidur BETWEEN 99000 AND 99999
AND s2.sector_visa BETWEEN 0 AND 1
AND y.mispar_ishi=s2.mispar_ishi
AND y.taarich=s2.taarich);

	 --cursor3: insert into sidurim	 
CURSOR Sidurim_retro1 IS
SELECT empl,taarich,sidur,hatchala,COUNT(*) FROM
(SELECT   DISTINCT k1.driver_id empl,k1.start_dt taarich,k1.schedule_num sidur,
k1.start_schedule  hatchala ,k1.end_schedule   gmar
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1 
WHERE k1.start_dt =TO_DATE(pDt,'yyyymmdd')
AND NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM
WHERE k1.driver_id=mispar_ishi 
--test:+100, 13/5 oved=971
--AND k1.start_dt+100= taarich
AND k1.start_dt= taarich
AND k1.schedule_num=mispar_sidur
AND k1.start_dt + SUBSTR(LPAD(k1.start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(k1.start_schedule,4,0),3,2)/1440 =shat_hatchala 
AND k1.start_dt + SUBSTR(LPAD(k1.end_schedule,4,0),1,2)/24 + SUBSTR(LPAD(k1.end_schedule,4,0),3,2)/1440 =shat_gmar))
GROUP BY empl,taarich,sidur,hatchala
HAVING COUNT(*)=1;

CURSOR Sidurim_retro2 IS
SELECT empl,taarich,sidur,hatchala,COUNT(*) FROM
(SELECT   DISTINCT k1.driver_id empl,k1.start_dt taarich,k1.schedule_num sidur,
k1.start_schedule  hatchala ,k1.end_schedule   gmar
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1 
WHERE k1.start_dt =TO_DATE(pDt,'yyyymmdd')
AND NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM
WHERE k1.driver_id=mispar_ishi 
--test:+100, 14/5 oved=29753
--AND k1.start_dt+100= taarich
AND k1.start_dt= taarich
AND k1.schedule_num=mispar_sidur
AND k1.start_dt + SUBSTR(LPAD(k1.start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(k1.start_schedule,4,0),3,2)/1440 =shat_hatchala 
AND k1.start_dt + SUBSTR(LPAD(k1.end_schedule,4,0),1,2)/24 + SUBSTR(LPAD(k1.end_schedule,4,0),3,2)/1440 =shat_gmar))
GROUP BY empl,taarich,sidur,hatchala
HAVING COUNT(*)>1;

-- this is based on sidurim_retro1 :bulk is most of the sidurim as is
  CURSOR Sidurim_retro3(p_empl NUMBER,p_taarich DATE,p_sidur NUMBER,p_hatchala NUMBER) IS
  SELECT   DISTINCT k1.driver_id,k1.start_dt,k1.schedule_num,
k1.start_dt + SUBSTR(LPAD(k1.start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(k1.start_schedule,4,0),3,2)/1440 hatchala ,
k1.start_dt + SUBSTR(LPAD(k1.end_schedule,4,0),1,2)/24 + SUBSTR(LPAD(k1.end_schedule,4,0),3,2)/1440 gmar,
DECODE(sug_visa,' ','',sug_visa) sug_visa,-12 meadken,sug_sidur
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1
WHERE k1.start_dt=  TO_DATE(pDt,'yyyymmdd') 
AND k1.driver_id=p_empl
AND k1.start_dt=p_taarich
AND k1.schedule_num=p_sidur
AND k1.start_schedule=p_hatchala;

-- this will be based on sidurim_retro2 :the duplicates
  CURSOR Sidurim_retro4(p_empl NUMBER,p_taarich DATE,p_sidur NUMBER,p_hatchala NUMBER) IS
SELECT DISTINCT k1.driver_id,k1.start_dt,k1.schedule_num,
k1.start_dt + SUBSTR(LPAD(k1.start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(k1.start_schedule,4,0),3,2)/1440 hatchala ,
k1.start_dt + SUBSTR(LPAD(k1.end_schedule,4,0),1,2)/24 + SUBSTR(LPAD(k1.end_schedule,4,0),3,2)/1440 gmar,
DECODE(sug_visa,' ','',sug_visa) sug_visa,-12 meadken,sug_sidur
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1
WHERE k1.start_dt=  TO_DATE(pDt,'yyyymmdd')  
AND k1.driver_id=p_empl
AND k1.start_dt=p_taarich
AND k1.schedule_num=p_sidur
AND k1.start_schedule=p_hatchala;

	 --cursor4: insert into  peiluyot	
	    CURSOR Peilut_retro1 IS
   SELECT    driver_id,start_dt,schedule_num,branch,
start_dt + SUBSTR(LPAD(start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(start_schedule,4,0),3,2)/1440 hatchala ,
start_dt + SUBSTR(LPAD(start_time,4,0),1,2)/24 + SUBSTR(LPAD(start_time,4,0),3,2)/1440 gmar,ride_id,
 makat_line,bus_number,bus_sequence,waiting_time,
 spm_time,spm_bus_number,spm_schedule_num,spm_start_time,spm_makat_line,spm_line_sign,spm_location,-12 meadken
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
WHERE  start_dt= TO_DATE(pDt,'yyyymmdd')
AND NOT EXISTS (SELECT * FROM TB_PEILUT_OVDIM WHERE taarich= TO_DATE(pDt,'yyyymmdd')
			   AND driver_id=mispar_ishi
 			   AND start_dt=taarich
			   AND schedule_num=mispar_sidur
			   --AND branch=snif_tnua
			   AND shat_hatchala_sidur=start_dt + SUBSTR(LPAD(start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(start_schedule,4,0),3,2)/1440  
			   AND shat_yetzia=start_dt + SUBSTR(LPAD(start_time,4,0),1,2)/24 + SUBSTR(LPAD(start_time,4,0),3,2)/1440  
			   AND mispar_knisa=ride_id);

	 --cursor5 & 8: update teur_nesia in  peiluyot	
CURSOR Peilut5  IS
  SELECT   DISTINCT    start_dt, driver_id,schedule_num,
start_dt + SUBSTR(LPAD(start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(start_schedule,4,0),3,2)/1440 hatchala,
start_dt + SUBSTR(LPAD(start_time,4,0),1,2)/24 + SUBSTR(LPAD(start_time,4,0),3,2)/1440 moed,
				 makat_line,ride_id,  line_description
 FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
						   	WHERE  start_dt=  TO_DATE(pDt,'yyyymmdd')
							AND line_description IS NOT NULL
							AND NVL(makat_line,0)=0
							AND schedule_num<1000;
  
CURSOR Peilut8  IS
  SELECT   DISTINCT    start_dt, driver_id,schedule_num,
start_dt + SUBSTR(LPAD(start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(start_schedule,4,0),3,2)/1440 hatchala,
start_dt + SUBSTR(LPAD(start_time,4,0),1,2)/24 + SUBSTR(LPAD(start_time,4,0),3,2)/1440 moed,
				 makat_line,ride_id,  line_description
 FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
						   	WHERE  start_dt=  TO_DATE(pDt,'yyyymmdd')
							AND line_description IS NOT NULL
							AND ride_id>0
							AND makat_line<50000000;
	 
  BEGIN
  
FOR  retro1_rec IN  retro1 LOOP
BEGIN
 INSERT INTO TRAIL_SIDURIM_OVDIM t
 ( MISPAR_ISHI  ,  MISPAR_sidur    ,  TAARICH ,  Shat_hatchala		,  
 Shat_gmar		,  Shat_hatchala_letashlum ,  Shat_gmar_letashlum	,  Pitzul_hafsaka	,  Chariga	,  Tosefet_Grira	,  Hashlama,  sug_hashlama 		,
  Yom_Visa		,  Lo_letashlum	,  Out_michsa	,  Mikum_shaon_knisa	 ,  Mikum_shaon_yetzia	,  Achuz_Knas_LePremyat_Visa	,  Achuz_Viza_Besikun	,
  Mispar_Musach_O_Machsan	,  Kod_siba_lo_letashlum		,  Kod_siba_ledivuch_yadani_in	,  Kod_siba_ledivuch_yadani_out	,  Menahel_Musach_Meadken	,
  Shayah_LeYom_Kodem 	,  Mispar_shiurey_nehiga	,  Mezake_Halbasha 	,  Mezake_nesiot		,  Sug_Hazmanat_Visa ,  Bitul_O_Hosafa	,
  tafkid_visa 		,  mivtza_visa 		,  Nidreshet_hitiatzvut 		,  Shat_hitiatzvut	,  Ptor_Mehitiatzvut 	,  Hachtama_Beatar_Lo_Takin  ,  Hafhatat_Nochechut_Visa	,
  Sector_Visa 		,  MEADKEN_ACHARON 	 ,  TAARICH_IDKUN_ACHARON   ,  MISPAR_ISHI_trail     ,  TAARICH_IDKUN_trail   , Sug_peula , heara	,sug_sidur	 )
SELECT DISTINCT p.MISPAR_ISHI  ,  MISPAR_sidur    ,  p.TAARICH ,  p.Shat_hatchala		,  
  Shat_gmar		,  Shat_hatchala_letashlum ,  Shat_gmar_letashlum	,  Pitzul_hafsaka	,  Chariga	,  Tosefet_Grira	,  Hashlama,  sug_hashlama 		,
  Yom_Visa		,  Lo_letashlum	,  Out_michsa	,  Mikum_shaon_knisa	 ,  Mikum_shaon_yetzia	,  Achuz_Knas_LePremyat_Visa	,  Achuz_Viza_Besikun	,
  Mispar_Musach_O_Machsan	,  Kod_siba_lo_letashlum		,  Kod_siba_ledivuch_yadani_in	,  Kod_siba_ledivuch_yadani_out	,  Menahel_Musach_Meadken	,
  Shayah_LeYom_Kodem 	,  Mispar_shiurey_nehiga	,  Mezake_Halbasha 	,  Mezake_nesiot		,    Sug_Hazmanat_Visa ,  Bitul_O_Hosafa	,
  tafkid_visa 		,  mivtza_visa 		,  Nidreshet_hitiatzvut 		,  Shat_hitiatzvut	,  Ptor_Mehitiatzvut 	,  Hachtama_Beatar_Lo_Takin  ,  Hafhatat_Nochechut_Visa	,
  Sector_Visa 		,  p.MEADKEN_ACHARON 	 ,  p.TAARICH_IDKUN_ACHARON  ,   77690  ,  SYSDATE  ,8    ,  p.heara 	,p.sug_sidur
FROM TB_SIDURIM_OVDIM p
WHERE p.taarich =   TO_DATE(pDt,'yyyymmdd')  
 AND  p.taarich = retro1_rec.taarich
 AND  p.mispar_sidur = retro1_rec.mispar_sidur
 AND  p.mispar_ishi = retro1_rec.mispar_ishi
 AND  p.shat_hatchala = retro1_rec.shat_hatchala;
 
 INSERT INTO TRAIL_PEILUT_OVDIM t
(t.MISPAR_ISHI   ,  t.TAARICH  ,  t.MISPAR_sidur  ,  t.Shat_hatchala_sidur	,
  t.Shat_yetzia		,  t.Mispar_knisa		,  t.Makat_nesia	,  t.Oto_no	,  t.Mispar_siduri_oto	 ,  t.Kisuy_tor		,  t.Bitul_O_Hosafa	,  t.Kod_shinuy_premia	  ,
  t.Snif_tnua	   ,  t.Mispar_visa		,  t.Imut_netzer		,  t.Shat_Bhirat_Nesia_Netzer  ,  t.Oto_No_Netzer		,  t.Mispar_Sidur_Netzer	 ,  t.Shat_yetzia_Netzer	,
  t.Makat_Netzer		,  t.Shilut_Netzer		,  t.Mispar_matala	 ,  t.Dakot_bafoal		,  t.Km_visa 	,  t.TAARICH_IDKUN_ACHARON  ,  t.MEADKEN_ACHARON 	,
  t.MISPAR_ISHI_trail   ,  t.TAARICH_IDKUN_trail      ,  t.Sug_peula  	,  
  t.Mikum_Bhirat_Nesia_Netzer ,  t.heara			,  t.Teur_Nesia )
SELECT DISTINCT p.MISPAR_ISHI   ,  p.TAARICH  ,  p.MISPAR_sidur  ,  p.Shat_hatchala_sidur	,
  p.Shat_yetzia		,  p.Mispar_knisa		,  p.Makat_nesia	,  p.Oto_no	,  p.Mispar_siduri_oto	 ,  p.Kisuy_tor		,  p.Bitul_O_Hosafa	,  p.Kod_shinuy_premia	  ,
  p.Snif_tnua	   ,  p.Mispar_visa		,  p.Imut_netzer		,  p.Shat_Bhirat_Nesia_Netzer  ,  p.Oto_No_Netzer		,  p.Mispar_Sidur_Netzer	 ,  p.Shat_yetzia_Netzer	,
  p.Makat_Netzer		,  p.Shilut_Netzer		,  p.Mispar_matala	 ,  p.Dakot_bafoal		,  p.Km_visa 	,  p.TAARICH_IDKUN_ACHARON  ,  p.MEADKEN_ACHARON 	,
 77690  ,  SYSDATE      ,  8 	,  
  p.Mikum_Bhirat_Nesia_Netzer ,  p.heara			,  p.Teur_Nesia  
FROM TB_PEILUT_OVDIM p 
WHERE p.taarich =   TO_DATE(pDt,'yyyymmdd')  
 AND  p.taarich = retro1_rec.taarich
 AND  p.mispar_sidur = retro1_rec.mispar_sidur
 AND  p.mispar_ishi = retro1_rec.mispar_ishi
 AND  p.shat_hatchala_sidur = retro1_rec.shat_hatchala;

 DELETE FROM TB_PEILUT_OVDIM p 
WHERE p.taarich =   TO_DATE(pDt,'yyyymmdd')  
 AND  p.taarich = retro1_rec.taarich
 AND  p.mispar_sidur = retro1_rec.mispar_sidur
 AND  p.mispar_ishi = retro1_rec.mispar_ishi
 AND  p.shat_hatchala_sidur = retro1_rec.shat_hatchala;
 
 DELETE FROM TB_SIDURIM_OVDIM p
WHERE p.taarich =   TO_DATE(pDt,'yyyymmdd')  
 AND  p.taarich = retro1_rec.taarich
 AND  p.mispar_sidur = retro1_rec.mispar_sidur
 AND  p.mispar_ishi = retro1_rec.mispar_ishi
 AND  p.shat_hatchala = retro1_rec.shat_hatchala;
 
  EXCEPTION
    WHEN OTHERS THEN
 	INSERT INTO TB_LOG_TAHALICH
 	VALUES (15,1,1,SYSDATE,'',10,'',SUBSTR(TO_CHAR(retro1_rec.mispar_ishi) ||' '||TO_CHAR(retro1_rec.mispar_sidur) ||' '||TO_CHAR(retro1_rec.taarich)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));   

END;
END LOOP;
COMMIT;

FOR  retro2_rec IN  retro2 LOOP
BEGIN
 INSERT INTO TRAIL_SIDURIM_OVDIM t
 ( MISPAR_ISHI  ,  MISPAR_sidur    ,  TAARICH ,  Shat_hatchala		,  
 Shat_gmar		,  Shat_hatchala_letashlum ,  Shat_gmar_letashlum	,  Pitzul_hafsaka	,  Chariga	,  Tosefet_Grira	,  Hashlama,  sug_hashlama 		,
  Yom_Visa		,  Lo_letashlum	,  Out_michsa	,  Mikum_shaon_knisa	 ,  Mikum_shaon_yetzia	,  Achuz_Knas_LePremyat_Visa	,  Achuz_Viza_Besikun	,
  Mispar_Musach_O_Machsan	,  Kod_siba_lo_letashlum		,  Kod_siba_ledivuch_yadani_in	,  Kod_siba_ledivuch_yadani_out	,  Menahel_Musach_Meadken	,
  Shayah_LeYom_Kodem 	,  Mispar_shiurey_nehiga	,  Mezake_Halbasha 	,  Mezake_nesiot		,  Sug_Hazmanat_Visa ,  Bitul_O_Hosafa	,
  tafkid_visa 		,  mivtza_visa 		,  Nidreshet_hitiatzvut 		,  Shat_hitiatzvut	,  Ptor_Mehitiatzvut 	,  Hachtama_Beatar_Lo_Takin  ,  Hafhatat_Nochechut_Visa	,
  Sector_Visa 		,  MEADKEN_ACHARON 	 ,  TAARICH_IDKUN_ACHARON   ,  MISPAR_ISHI_trail     ,  TAARICH_IDKUN_trail   , Sug_peula , heara	,sug_sidur	 )
SELECT DISTINCT p.MISPAR_ISHI  ,  MISPAR_sidur    ,  p.TAARICH ,  p.Shat_hatchala		,  
  Shat_gmar		,  Shat_hatchala_letashlum ,  Shat_gmar_letashlum	,  Pitzul_hafsaka	,  Chariga	,  Tosefet_Grira	,  Hashlama,  sug_hashlama 		,
  Yom_Visa		,  Lo_letashlum	,  Out_michsa	,  Mikum_shaon_knisa	 ,  Mikum_shaon_yetzia	,  Achuz_Knas_LePremyat_Visa	,  Achuz_Viza_Besikun	,
  Mispar_Musach_O_Machsan	,  Kod_siba_lo_letashlum		,  Kod_siba_ledivuch_yadani_in	,  Kod_siba_ledivuch_yadani_out	,  Menahel_Musach_Meadken	,
  Shayah_LeYom_Kodem 	,  Mispar_shiurey_nehiga	,  Mezake_Halbasha 	,  Mezake_nesiot		,    Sug_Hazmanat_Visa ,  Bitul_O_Hosafa	,
  tafkid_visa 		,  mivtza_visa 		,  Nidreshet_hitiatzvut 		,  Shat_hitiatzvut	,  Ptor_Mehitiatzvut 	,  Hachtama_Beatar_Lo_Takin  ,  Hafhatat_Nochechut_Visa	,
  Sector_Visa 		,  p.MEADKEN_ACHARON 	 ,  p.TAARICH_IDKUN_ACHARON  ,   77690  ,  SYSDATE  ,8    ,  p.heara 	,p.sug_sidur
FROM TB_SIDURIM_OVDIM p
WHERE p.taarich =   TO_DATE(pDt,'yyyymmdd')  
 AND  p.taarich = retro2_rec.taarich
-- AND  p.mispar_sidur = retro2_rec.mispar_sidur
 AND  p.mispar_ishi = retro2_rec.mispar_ishi;
-- AND  p.shat_hatchala = retro2_rec.shat_hatchala;
 
 INSERT INTO TRAIL_PEILUT_OVDIM t
(t.MISPAR_ISHI   ,  t.TAARICH  ,  t.MISPAR_sidur  ,  t.Shat_hatchala_sidur	,
  t.Shat_yetzia		,  t.Mispar_knisa		,  t.Makat_nesia	,  t.Oto_no	,  t.Mispar_siduri_oto	 ,  t.Kisuy_tor		,  t.Bitul_O_Hosafa	,  t.Kod_shinuy_premia	  ,
  t.Snif_tnua	   ,  t.Mispar_visa		,  t.Imut_netzer		,  t.Shat_Bhirat_Nesia_Netzer  ,  t.Oto_No_Netzer		,  t.Mispar_Sidur_Netzer	 ,  t.Shat_yetzia_Netzer	,
  t.Makat_Netzer		,  t.Shilut_Netzer		,  t.Mispar_matala	 ,  t.Dakot_bafoal		,  t.Km_visa 	,  t.TAARICH_IDKUN_ACHARON  ,  t.MEADKEN_ACHARON 	,
  t.MISPAR_ISHI_trail   ,  t.TAARICH_IDKUN_trail      ,  t.Sug_peula  	,  
  t.Mikum_Bhirat_Nesia_Netzer ,  t.heara			,  t.Teur_Nesia )
SELECT DISTINCT p.MISPAR_ISHI   ,  p.TAARICH  ,  p.MISPAR_sidur  ,  p.Shat_hatchala_sidur	,
  p.Shat_yetzia		,  p.Mispar_knisa		,  p.Makat_nesia	,  p.Oto_no	,  p.Mispar_siduri_oto	 ,  p.Kisuy_tor		,  p.Bitul_O_Hosafa	,  p.Kod_shinuy_premia	  ,
  p.Snif_tnua	   ,  p.Mispar_visa		,  p.Imut_netzer		,  p.Shat_Bhirat_Nesia_Netzer  ,  p.Oto_No_Netzer		,  p.Mispar_Sidur_Netzer	 ,  p.Shat_yetzia_Netzer	,
  p.Makat_Netzer		,  p.Shilut_Netzer		,  p.Mispar_matala	 ,  p.Dakot_bafoal		,  p.Km_visa 	,  p.TAARICH_IDKUN_ACHARON  ,  p.MEADKEN_ACHARON 	,
 77690  ,  SYSDATE      ,  8 	,  
  p.Mikum_Bhirat_Nesia_Netzer ,  p.heara			,  p.Teur_Nesia  
FROM TB_PEILUT_OVDIM p 
WHERE p.taarich =   TO_DATE(pDt,'yyyymmdd')  
 AND  p.taarich = retro2_rec.taarich
-- AND  p.mispar_sidur = retro2_rec.mispar_sidur
 AND  p.mispar_ishi = retro2_rec.mispar_ishi;
-- AND  p.shat_hatchala_sidur = retro2_rec.shat_hatchala;

 DELETE FROM TB_PEILUT_OVDIM p 
WHERE p.taarich =   TO_DATE(pDt,'yyyymmdd')  
 AND  p.taarich = retro2_rec.taarich
-- AND  p.mispar_sidur = retro2_rec.mispar_sidur
 AND  p.mispar_ishi = retro2_rec.mispar_ishi;
-- AND  p.shat_hatchala_sidur = retro2_rec.shat_hatchala;
 
 DELETE FROM TB_SIDURIM_OVDIM p
WHERE p.taarich =   TO_DATE(pDt,'yyyymmdd')  
 AND  p.taarich = retro2_rec.taarich
-- AND  p.mispar_sidur = retro2_rec.mispar_sidur
 AND  p.mispar_ishi = retro2_rec.mispar_ishi;
-- AND  p.shat_hatchala = retro2_rec.shat_hatchala;
 
  EXCEPTION
    WHEN OTHERS THEN
 	INSERT INTO TB_LOG_TAHALICH
 	VALUES (15,2,1,SYSDATE,'',10,'',SUBSTR(TO_CHAR(retro2_rec.mispar_ishi) ||' '||TO_CHAR(retro2_rec.taarich)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));   

END;
END LOOP;
COMMIT;

-- this is equal to the normal procedure
   Pkg_Sdrn.pro_ins_yamim_4_sidurim(pDt);
   
-- insert sidurim:
   FOR  Sidurim_retro1_rec IN  Sidurim_retro1 LOOP
 
   		FOR  sidurim_retro3_rec IN  sidurim_retro3(sidurim_retro1_rec.empl,sidurim_retro1_rec.taarich,sidurim_retro1_rec.sidur,sidurim_retro1_rec.hatchala)  LOOP
 		BEGIN
  INSERT INTO TB_SIDURIM_OVDIM s ( mispar_ishi  , taarich  , mispar_sidur  , shat_hatchala  , shat_gmar,sector_visa,meadken_acharon   ,sug_sidur  )
--VALUES (sidurim_retro3_rec.driver_id,sidurim_retro3_rec.start_dt+100,  sidurim_retro3_rec.schedule_num,  
VALUES (sidurim_retro3_rec.driver_id,sidurim_retro3_rec.start_dt,  sidurim_retro3_rec.schedule_num,  
sidurim_retro3_rec.hatchala,sidurim_retro3_rec.gmar,  sidurim_retro3_rec.sug_visa,   sidurim_retro3_rec.meadken, sidurim_retro3_rec.sug_sidur );

EXCEPTION
   WHEN OTHERS THEN
	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,3,20,SYSDATE,'',10,'',SUBSTR(TO_CHAR(sidurim_retro3_rec.driver_id) ||' retro '||TO_CHAR(sidurim_retro3_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
	  END;
 		   END LOOP;
	 
END LOOP;
COMMIT;

   FOR  sidurim_retro2_rec IN  sidurim_retro2 LOOP
 idNumber:=0; 
   		FOR  sidurim_retro4_rec IN  sidurim_retro4(sidurim_retro2_rec.empl,sidurim_retro2_rec.taarich,sidurim_retro2_rec.sidur,sidurim_retro2_rec.hatchala)  LOOP
 		BEGIN
  INSERT INTO TB_SIDURIM_OVDIM s ( mispar_ishi  , taarich  , mispar_sidur  , shat_hatchala  , shat_gmar,sector_visa,meadken_acharon   ,sug_sidur  )
--VALUES (sidurim_retro4_rec.driver_id,sidurim_retro4_rec.start_dt+100,  sidurim_retro4_rec.schedule_num,  
VALUES (sidurim_retro4_rec.driver_id,sidurim_retro4_rec.start_dt,  sidurim_retro4_rec.schedule_num,  
sidurim_retro4_rec.hatchala+idNumber/1440,sidurim_retro4_rec.gmar,  sidurim_retro4_rec.sug_visa,   sidurim_retro4_rec.meadken, sidurim_retro4_rec.sug_sidur );
idNumber:=idNumber+1;
EXCEPTION
   WHEN OTHERS THEN
	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,4,21,SYSDATE,'',10,'',SUBSTR(TO_CHAR(sidurim_retro4_rec.driver_id) ||' new '||TO_CHAR(sidurim_retro4_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
	  END;
 		   END LOOP;
	 
END LOOP;
COMMIT;

 BEGIN
 UPDATE  TB_SIDURIM_OVDIM 
 SET   Shayah_LeYom_Kodem=1   
WHERE  taarich = TO_DATE(pDt,'yyyymmdd')--+100
AND meadken_acharon=-12 AND taarich_idkun_acharon>SYSDATE-0.5
AND EXISTS (SELECT *   FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1
WHERE k1.start_dt= TO_DATE(pDt,'yyyymmdd')
AND k1.start_schedule>2359
AND    taarich = k1.start_dt--+100
AND mispar_ishi=k1.driver_id
AND  mispar_sidur  =k1.schedule_num
AND  shat_hatchala  =k1.start_dt + SUBSTR(LPAD(k1.start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(k1.start_schedule,4,0),3,2)/1440
AND  shat_gmar=k1.start_dt + SUBSTR(LPAD(k1.end_schedule,4,0),1,2)/24 + SUBSTR(LPAD(k1.end_schedule,4,0),3,2)/1440);

EXCEPTION
   WHEN OTHERS THEN
    INSERT INTO TB_LOG_TAHALICH
    VALUES (15,5,22,SYSDATE,'',10,'',SUBSTR(pDt||' Shayah_LeYom_Kodem retro '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));     
END;
COMMIT;

BEGIN
 UPDATE TB_SIDURIM_OVDIM 
SET   Lo_letashlum=1 ,Kod_Siba_Lo_Letashlum=16
 WHERE taarich= TO_DATE(pDt,'yyyymmdd')--+100
AND meadken_acharon=-12 AND taarich_idkun_acharon>SYSDATE-0.5;

  EXCEPTION
   WHEN OTHERS THEN
	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,6,2,SYSDATE,'',10,'',SUBSTR('update_sidurim retro'||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));   

END;
COMMIT;

-- insert peiluyot   
   FOR  Peilut_retro1_rec IN  Peilut_retro1 LOOP
BEGIN
INSERT INTO TB_PEILUT_OVDIM (mispar_ishi,taarich,mispar_sidur,  snif_tnua,shat_hatchala_sidur,shat_yetzia,mispar_knisa,
makat_nesia,oto_no,mispar_siduri_oto,kisuy_tor,
 Shat_Bhirat_Nesia_Netzer , Oto_No_Netzer, Mispar_Sidur_Netzer,  Shat_yetzia_Netzer,  Makat_Netzer, Shilut_Netzer ,
 Mikum_Bhirat_Nesia_Netzer ,meadken_acharon  )
--  VALUES (Peilut1_rec.driver_id,Peilut1_rec.start_dt+100,  Peilut1_rec.schedule_num,  Peilut1_rec.branch,
  VALUES (Peilut_retro1_rec.driver_id,Peilut_retro1_rec.start_dt,  Peilut_retro1_rec.schedule_num,  
  Peilut_retro1_rec.branch,Peilut_retro1_rec.hatchala,Peilut_retro1_rec.gmar,Peilut_retro1_rec.ride_id, 
  Peilut_retro1_rec.makat_line,Peilut_retro1_rec.bus_number,Peilut_retro1_rec.bus_sequence,
Peilut_retro1_rec.waiting_time, Peilut_retro1_rec.spm_time,Peilut_retro1_rec.spm_bus_number,
Peilut_retro1_rec.spm_schedule_num,Peilut_retro1_rec.spm_start_time,Peilut_retro1_rec.spm_makat_line, 
Peilut_retro1_rec.spm_line_sign,Peilut_retro1_rec.spm_location, Peilut_retro1_rec.meadken );

EXCEPTION
   WHEN OTHERS THEN
  	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,7,30,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Peilut_retro1_rec.driver_id) ||'update_peilut retro'||TO_CHAR(Peilut_retro1_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
   END;
END LOOP;
COMMIT;

--update teur_nesia cursor 5 & 8
 FOR  Peilut5_rec IN  Peilut5 LOOP
BEGIN
UPDATE TB_PEILUT_OVDIM
SET teur_nesia=    Peilut5_rec.line_description
WHERE taarich= TO_DATE(pDt,'yyyymmdd')--+100
AND NVL(makat_nesia,0)=0
AND mispar_sidur<1000
							AND Peilut5_rec.driver_id=mispar_ishi
							AND Peilut5_rec.schedule_num=mispar_sidur
							AND Peilut5_rec.hatchala =shat_hatchala_sidur
							AND Peilut5_rec.moed =shat_yetzia
							AND Peilut5_rec.makat_line=makat_nesia
							AND Peilut5_rec.ride_id=mispar_knisa;

EXCEPTION
   WHEN OTHERS THEN
  	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,8,42,SYSDATE,'',10,'',SUBSTR('update teur peilut retro '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
END;
END LOOP;
COMMIT;

FOR  Peilut8_rec IN  Peilut8 LOOP
BEGIN
UPDATE TB_PEILUT_OVDIM
SET teur_nesia=    Peilut8_rec.line_description
WHERE taarich= TO_DATE(pDt,'yyyymmdd')--+100
AND makat_nesia<50000000
AND mispar_knisa>0
							AND Peilut8_rec.driver_id=mispar_ishi
							AND Peilut8_rec.schedule_num=mispar_sidur
							AND Peilut8_rec.hatchala =shat_hatchala_sidur
							AND Peilut8_rec.moed =shat_yetzia
							AND Peilut8_rec.makat_line=makat_nesia
							AND Peilut8_rec.ride_id=mispar_knisa;

EXCEPTION
   WHEN OTHERS THEN
  	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,9,39,SYSDATE,'',10,'',SUBSTR('update teur peilut retro '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
END;
END LOOP;
COMMIT;

 END pro_retro1;

--  PROCEDURE pro_ins_yamim_from_sidurim(pDt VARCHAR) IS
 
--   CURSOR Yamim IS
--  SELECT   DISTINCT driver_id,start_dt,-12 meadken--,0,sysdate,-12
--FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
--WHERE start_dt= TO_DATE(pDt,'yyyymmdd')
--AND  NOT  EXISTS(SELECT * FROM TB_YAMEY_AVODA_OVDIM
--WHERE  mispar_ishi=driver_id
----AND  taarich=start_dt+100);
--AND  taarich=start_dt);

--BEGIN
  
--FOR  Yamim_rec IN  Yamim LOOP
--BEGIN
-- INSERT INTO  TB_YAMEY_AVODA_OVDIM (mispar_ishi,taarich,lina,taarich_idkun_acharon,meadken_acharon)
----VALUES (Yamim_rec.driver_id,Yamim_rec.start_dt+100,  0,SYSDATE,Yamim_rec.meadken );
--VALUES (Yamim_rec.driver_id,Yamim_rec.start_dt,  0,SYSDATE,Yamim_rec.meadken );

--EXCEPTION
--   WHEN OTHERS THEN
-- 	INSERT INTO TB_LOG_TAHALICH
--	VALUES (4,1,1,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Yamim_rec.driver_id)||' new '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
--END;
--END LOOP;
--COMMIT;
  
--END pro_ins_yamim_from_sidurim;
  
PROCEDURE pro_ins_sidurim_from_sdrm(pDt VARCHAR) IS
idNumber NUMBER;

-- this bulk is most of the sidurim
CURSOR Sidurim1 IS
SELECT empl,taarich,sidur,hatchala,COUNT(*) FROM
(SELECT   DISTINCT k1.driver_id empl,k1.start_dt taarich,k1.schedule_num sidur,
k1.start_schedule  hatchala ,k1.end_schedule   gmar
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1 
WHERE k1.start_dt = TO_DATE(pDt,'yyyymmdd'))
GROUP BY empl,taarich,sidur,hatchala
HAVING COUNT(*)=1;

-- these r the duplicates, such as ���� ����� or ������ ����� etc
CURSOR Sidurim2 IS
SELECT empl,taarich,sidur,hatchala,COUNT(*) FROM
(SELECT   DISTINCT k1.driver_id empl,k1.start_dt taarich,k1.schedule_num sidur,
k1.start_schedule  hatchala ,k1.end_schedule   gmar
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1 
WHERE k1.start_dt = TO_DATE(pDt,'yyyymmdd')) 
GROUP BY empl,taarich,sidur,hatchala
HAVING COUNT(*)>1;


-- this is based on cursor1 :bulk is most of the sidurim
  CURSOR Sidurim3(p_empl NUMBER,p_taarich DATE,p_sidur NUMBER,p_hatchala NUMBER) IS
  SELECT   DISTINCT k1.driver_id,k1.start_dt,k1.schedule_num,
k1.start_dt + SUBSTR(LPAD(k1.start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(k1.start_schedule,4,0),3,2)/1440 hatchala ,
k1.start_dt + SUBSTR(LPAD(k1.end_schedule,4,0),1,2)/24 + SUBSTR(LPAD(k1.end_schedule,4,0),3,2)/1440 gmar,
DECODE(sug_visa,' ','',sug_visa) sug_visa,-12 meadken,sug_sidur
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1
WHERE k1.start_dt=  TO_DATE(pDt,'yyyymmdd') 
AND k1.driver_id=p_empl
AND k1.start_dt=p_taarich
AND k1.schedule_num=p_sidur
AND k1.start_schedule=p_hatchala;

-- this will be based on cursor2 :the duplicates
  CURSOR Sidurim4(p_empl NUMBER,p_taarich DATE,p_sidur NUMBER,p_hatchala NUMBER) IS
SELECT DISTINCT k1.driver_id,k1.start_dt,k1.schedule_num,
k1.start_dt + SUBSTR(LPAD(k1.start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(k1.start_schedule,4,0),3,2)/1440 hatchala ,
k1.start_dt + SUBSTR(LPAD(k1.end_schedule,4,0),1,2)/24 + SUBSTR(LPAD(k1.end_schedule,4,0),3,2)/1440 gmar,
DECODE(sug_visa,' ','',sug_visa) sug_visa,-12 meadken,sug_sidur
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1
WHERE k1.start_dt=  TO_DATE(pDt,'yyyymmdd')  
AND k1.driver_id=p_empl
AND k1.start_dt=p_taarich
AND k1.schedule_num=p_sidur
AND k1.start_schedule=p_hatchala;


 BEGIN
     
   FOR  Sidurim1_rec IN  Sidurim1 LOOP
 
   		FOR  Sidurim3_rec IN  Sidurim3(Sidurim1_rec.empl,Sidurim1_rec.taarich,Sidurim1_rec.sidur,Sidurim1_rec.hatchala)  LOOP
 		BEGIN
  INSERT INTO TB_SIDURIM_OVDIM s ( mispar_ishi  , taarich  , mispar_sidur  , shat_hatchala  , shat_gmar,sector_visa,meadken_acharon   ,sug_sidur  )
--VALUES (Sidurim3_rec.driver_id,Sidurim3_rec.start_dt+100,  Sidurim3_rec.schedule_num,  
VALUES (Sidurim3_rec.driver_id,Sidurim3_rec.start_dt,  Sidurim3_rec.schedule_num,  
Sidurim3_rec.hatchala,Sidurim3_rec.gmar,  Sidurim3_rec.sug_visa,   Sidurim3_rec.meadken, Sidurim3_rec.sug_sidur );

EXCEPTION
   WHEN OTHERS THEN
	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,20,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Sidurim3_rec.driver_id) ||' new '||TO_CHAR(Sidurim3_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
	  END;
 		   END LOOP;
	 
END LOOP;
COMMIT;

   FOR  Sidurim2_rec IN  Sidurim2 LOOP
 idNumber:=0; 
   		FOR  Sidurim4_rec IN  Sidurim4(Sidurim2_rec.empl,Sidurim2_rec.taarich,Sidurim2_rec.sidur,Sidurim2_rec.hatchala)  LOOP
 		BEGIN
  INSERT INTO TB_SIDURIM_OVDIM s ( mispar_ishi  , taarich  , mispar_sidur  , shat_hatchala  , shat_gmar,sector_visa,meadken_acharon   ,sug_sidur  )
--VALUES (Sidurim4_rec.driver_id,Sidurim4_rec.start_dt+100,  Sidurim4_rec.schedule_num,  
VALUES (Sidurim4_rec.driver_id,Sidurim4_rec.start_dt,  Sidurim4_rec.schedule_num,  
Sidurim4_rec.hatchala+idNumber/1440,Sidurim4_rec.gmar,  Sidurim4_rec.sug_visa,   Sidurim4_rec.meadken, Sidurim4_rec.sug_sidur );
idNumber:=idNumber+1;
EXCEPTION
   WHEN OTHERS THEN
	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,21,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Sidurim4_rec.driver_id) ||' new '||TO_CHAR(Sidurim4_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
	  END;
 		   END LOOP;
	 
END LOOP;
COMMIT;


 BEGIN
 UPDATE  TB_SIDURIM_OVDIM 
 SET   Shayah_LeYom_Kodem=1   
WHERE  taarich = TO_DATE(pDt,'yyyymmdd')--+100
AND EXISTS (SELECT *   FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1
WHERE k1.start_dt= TO_DATE(pDt,'yyyymmdd')
AND k1.start_schedule>2359
AND    taarich = k1.start_dt--+100
AND mispar_ishi=k1.driver_id
AND  mispar_sidur  =k1.schedule_num
AND  shat_hatchala  =k1.start_dt + SUBSTR(LPAD(k1.start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(k1.start_schedule,4,0),3,2)/1440
AND  shat_gmar=k1.start_dt + SUBSTR(LPAD(k1.end_schedule,4,0),1,2)/24 + SUBSTR(LPAD(k1.end_schedule,4,0),3,2)/1440);

EXCEPTION
   WHEN OTHERS THEN
    INSERT INTO TB_LOG_TAHALICH
    VALUES (4,1,22,SYSDATE,'',10,'',SUBSTR(pDt||' Shayah_LeYom_Kodem new '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));     
END;
COMMIT;

BEGIN
 UPDATE TB_SIDURIM_OVDIM 
SET   Lo_letashlum=1 ,Kod_Siba_Lo_Letashlum=16
 WHERE taarich= TO_DATE(pDt,'yyyymmdd')--+100
AND meadken_acharon=-12;

  EXCEPTION
   WHEN OTHERS THEN
	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,2,SYSDATE,'',10,'',SUBSTR('update_sidurim new'||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));   

END;
COMMIT;

END pro_ins_sidurim_from_sdrm;

PROCEDURE pro_ins_peilut_from_sdrm(pDt VARCHAR) IS

---- this bulk is most of the peiluyot, except continuous visa
-- this bulk is all the peiluyot, 2 more cursors for upd teur_nesia 
   CURSOR Peilut1 IS
   SELECT    driver_id,start_dt,schedule_num,branch,
start_dt + SUBSTR(LPAD(start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(start_schedule,4,0),3,2)/1440 hatchala ,
start_dt + SUBSTR(LPAD(start_time,4,0),1,2)/24 + SUBSTR(LPAD(start_time,4,0),3,2)/1440 gmar,ride_id,
 makat_line,bus_number,bus_sequence,waiting_time,
 spm_time,spm_bus_number,spm_schedule_num,spm_start_time,spm_makat_line,spm_line_sign,spm_location,-12 meadken
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
WHERE  start_dt= TO_DATE(pDt,'yyyymmdd')
--AND NOT (start_schedule<2400
--AND end_schedule<2400
--AND DECODE(sug_visa,' ','',sug_visa) IN (0,1)
--AND  start_schedule>end_schedule)
;
							
----for visa when end<start ommited cursor1
--CURSOR Peilut9  IS
--  SELECT   DISTINCT      driver_id,start_dt,schedule_num,branch,
--start_dt + SUBSTR(LPAD(start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(start_schedule,4,0),3,2)/1440 hatchala ,
--start_dt + SUBSTR(LPAD(start_time,4,0),1,2)/24 + SUBSTR(LPAD(start_time,4,0),3,2)/1440 gmar,ride_id,
-- makat_line,bus_number,bus_sequence,waiting_time,
-- spm_time,spm_bus_number,spm_schedule_num,spm_start_time,spm_makat_line,spm_line_sign,spm_location,-12 meadken
--FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1
--WHERE  start_dt=  TO_DATE(pDt,'yyyymmdd')
--AND k1.start_schedule<2400
--AND k1.end_schedule<2400
--AND DECODE(sug_visa,' ','',sug_visa) IN (0,1)
--AND  k1.start_schedule>k1.end_schedule;

----for not a visa and ommited in cursor1
--CURSOR Peilut3  IS
--  SELECT   DISTINCT      driver_id,start_dt,schedule_num,branch,
--start_dt + SUBSTR(LPAD(start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(start_schedule,4,0),3,2)/1440 hatchala ,
--start_dt + SUBSTR(LPAD(start_time,4,0),1,2)/24 + SUBSTR(LPAD(start_time,4,0),3,2)/1440 gmar,ride_id,
-- makat_line,bus_number,bus_sequence,waiting_time,
-- spm_time,spm_bus_number,spm_schedule_num,spm_start_time,spm_makat_line,spm_line_sign,spm_location,-12 meadken
--FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1
--WHERE  start_dt=  TO_DATE(pDt,'yyyymmdd')
--AND NOT EXISTS (SELECT * FROM   TB_PEILUT_OVDIM
--WHERE taarich=TO_DATE(pDt,'yyyymmdd')--+100
--AND driver_id=mispar_ishi
---- AND start_dt+100=taarich 
-- AND start_dt=taarich
--AND schedule_num=mispar_sidur
--AND branch=snif_tnua
--AND shat_hatchala_sidur=start_dt + SUBSTR(LPAD(start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(start_schedule,4,0),3,2)/1440  
--AND shat_yetzia=start_dt + SUBSTR(LPAD(start_time,4,0),1,2)/24 + SUBSTR(LPAD(start_time,4,0),3,2)/1440  
--AND mispar_knisa=ride_id) ;


CURSOR Peilut5  IS
  SELECT   DISTINCT    start_dt, driver_id,schedule_num,
start_dt + SUBSTR(LPAD(start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(start_schedule,4,0),3,2)/1440 hatchala,
start_dt + SUBSTR(LPAD(start_time,4,0),1,2)/24 + SUBSTR(LPAD(start_time,4,0),3,2)/1440 moed,
				 makat_line,ride_id,  line_description
 FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
						   	WHERE  start_dt=  TO_DATE(pDt,'yyyymmdd')
							AND line_description IS NOT NULL
							AND NVL(makat_line,0)=0
							AND schedule_num<1000;
  
CURSOR Peilut8  IS
  SELECT   DISTINCT    start_dt, driver_id,schedule_num,
start_dt + SUBSTR(LPAD(start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(start_schedule,4,0),3,2)/1440 hatchala,
start_dt + SUBSTR(LPAD(start_time,4,0),1,2)/24 + SUBSTR(LPAD(start_time,4,0),3,2)/1440 moed,
				 makat_line,ride_id,  line_description
 FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
						   	WHERE  start_dt=  TO_DATE(pDt,'yyyymmdd')
							AND line_description IS NOT NULL
							AND ride_id>0
							AND makat_line<50000000;

     BEGIN
     
   FOR  Peilut1_rec IN  Peilut1 LOOP
BEGIN
INSERT INTO TB_PEILUT_OVDIM (mispar_ishi,taarich,mispar_sidur,  snif_tnua,shat_hatchala_sidur,shat_yetzia,mispar_knisa,
makat_nesia,oto_no,mispar_siduri_oto,kisuy_tor,
 Shat_Bhirat_Nesia_Netzer , Oto_No_Netzer, Mispar_Sidur_Netzer,  Shat_yetzia_Netzer,  Makat_Netzer, Shilut_Netzer ,
 Mikum_Bhirat_Nesia_Netzer ,meadken_acharon  )
--  VALUES (Peilut1_rec.driver_id,Peilut1_rec.start_dt+100,  Peilut1_rec.schedule_num,  Peilut1_rec.branch,
  VALUES (Peilut1_rec.driver_id,Peilut1_rec.start_dt,  Peilut1_rec.schedule_num,  Peilut1_rec.branch,
Peilut1_rec.hatchala,Peilut1_rec.gmar,   Peilut1_rec.ride_id, Peilut1_rec.makat_line,Peilut1_rec.bus_number,Peilut1_rec.bus_sequence,
Peilut1_rec.waiting_time, Peilut1_rec.spm_time,Peilut1_rec.spm_bus_number,Peilut1_rec.spm_schedule_num,Peilut1_rec.spm_start_time,
Peilut1_rec.spm_makat_line, Peilut1_rec.spm_line_sign,Peilut1_rec.spm_location, Peilut1_rec.meadken );

EXCEPTION
   WHEN OTHERS THEN
  	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,30,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Peilut1_rec.driver_id) ||' new '||TO_CHAR(Peilut1_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
   END;
END LOOP;
COMMIT;

--FOR  Peilut9_rec IN  Peilut9 LOOP
--BEGIN
--INSERT INTO TB_PEILUT_OVDIM (mispar_ishi,taarich,mispar_sidur,  snif_tnua,shat_hatchala_sidur,shat_yetzia,mispar_knisa,
--makat_nesia,oto_no,mispar_siduri_oto,kisuy_tor,
-- Shat_Bhirat_Nesia_Netzer , Oto_No_Netzer, Mispar_Sidur_Netzer,  Shat_yetzia_Netzer,  Makat_Netzer, Shilut_Netzer ,
-- Mikum_Bhirat_Nesia_Netzer ,meadken_acharon  )
----  VALUES (Peilut9_rec.driver_id,Peilut9_rec.start_dt+100,  Peilut9_rec.schedule_num,  Peilut9_rec.branch,
--  VALUES (Peilut9_rec.driver_id,Peilut9_rec.start_dt,  Peilut9_rec.schedule_num,  Peilut9_rec.branch,
--Peilut9_rec.hatchala,Peilut9_rec.gmar,   Peilut9_rec.ride_id, Peilut9_rec.makat_line,Peilut9_rec.bus_number,Peilut9_rec.bus_sequence,
--Peilut9_rec.waiting_time, Peilut9_rec.spm_time,Peilut9_rec.spm_bus_number,Peilut9_rec.spm_schedule_num,Peilut9_rec.spm_start_time,
--Peilut9_rec.spm_makat_line, Peilut9_rec.spm_line_sign,Peilut9_rec.spm_location, Peilut9_rec.meadken );

--EXCEPTION
--   WHEN OTHERS THEN
--  	INSERT INTO TB_LOG_TAHALICH
--	VALUES (4,1,45,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Peilut9_rec.driver_id) ||' new '||TO_CHAR(Peilut9_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
--   END;
--END LOOP;
--COMMIT;

--FOR  Peilut3_rec IN  Peilut3 LOOP
--BEGIN
--INSERT INTO TB_PEILUT_OVDIM (mispar_ishi,taarich,mispar_sidur,  snif_tnua,shat_hatchala_sidur,shat_yetzia,mispar_knisa,
--makat_nesia,oto_no,mispar_siduri_oto,kisuy_tor,
-- Shat_Bhirat_Nesia_Netzer , Oto_No_Netzer, Mispar_Sidur_Netzer,  Shat_yetzia_Netzer,  Makat_Netzer, Shilut_Netzer ,
-- Mikum_Bhirat_Nesia_Netzer ,meadken_acharon  )
----  VALUES (Peilut3_rec.driver_id,Peilut3_rec.start_dt+100,  Peilut3_rec.schedule_num,  Peilut3_rec.branch,
--  VALUES (Peilut3_rec.driver_id,Peilut3_rec.start_dt,  Peilut3_rec.schedule_num,  Peilut3_rec.branch,
--Peilut3_rec.hatchala,Peilut3_rec.gmar,   Peilut3_rec.ride_id, Peilut3_rec.makat_line,Peilut3_rec.bus_number,Peilut3_rec.bus_sequence,
--Peilut3_rec.waiting_time, Peilut3_rec.spm_time,Peilut3_rec.spm_bus_number,Peilut3_rec.spm_schedule_num,Peilut3_rec.spm_start_time,
--Peilut3_rec.spm_makat_line, Peilut3_rec.spm_line_sign,Peilut3_rec.spm_location, Peilut3_rec.meadken );


--EXCEPTION
--   WHEN OTHERS THEN
--  	INSERT INTO TB_LOG_TAHALICH
--	VALUES (4,1,43,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Peilut3_rec.driver_id) ||' new '||TO_CHAR(Peilut3_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
--   END;
--END LOOP;
--COMMIT;

 
 FOR  Peilut5_rec IN  Peilut5 LOOP
BEGIN
UPDATE TB_PEILUT_OVDIM
SET teur_nesia=    Peilut5_rec.line_description
WHERE taarich= TO_DATE(pDt,'yyyymmdd')--+100
AND NVL(makat_nesia,0)=0
AND mispar_sidur<1000
							AND Peilut5_rec.driver_id=mispar_ishi
							AND Peilut5_rec.schedule_num=mispar_sidur
							AND Peilut5_rec.hatchala =shat_hatchala_sidur
							AND Peilut5_rec.moed =shat_yetzia
							AND Peilut5_rec.makat_line=makat_nesia
							AND Peilut5_rec.ride_id=mispar_knisa;

EXCEPTION
   WHEN OTHERS THEN
  	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,42,SYSDATE,'',10,'',SUBSTR('update teur peilut new '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
END;
END LOOP;
COMMIT;

FOR  Peilut8_rec IN  Peilut8 LOOP
BEGIN
UPDATE TB_PEILUT_OVDIM
SET teur_nesia=    Peilut8_rec.line_description
WHERE taarich= TO_DATE(pDt,'yyyymmdd')--+100
AND makat_nesia<50000000
AND mispar_knisa>0
							AND Peilut8_rec.driver_id=mispar_ishi
							AND Peilut8_rec.schedule_num=mispar_sidur
							AND Peilut8_rec.hatchala =shat_hatchala_sidur
							AND Peilut8_rec.moed =shat_yetzia
							AND Peilut8_rec.makat_line=makat_nesia
							AND Peilut8_rec.ride_id=mispar_knisa;

EXCEPTION
   WHEN OTHERS THEN
  	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,39,SYSDATE,'',10,'',SUBSTR('update teur peilut new '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));
END;
END LOOP;
COMMIT;

END pro_ins_peilut_from_sdrm;

PROCEDURE pro_change_Knisot_sdrm(pDt VARCHAR,p_Cur OUT CurType) IS

--  headers to use in GetKavDetails
     BEGIN
  OPEN p_Cur FOR
    SELECT  DISTINCT makat_nesia
FROM TB_PEILUT_OVDIM p0
WHERE p0.taarich= TO_DATE(pDt,'yyyymmdd')
AND p0.mispar_knisa=0
-- test:
--AND p0.makat_nesia=45712235
AND EXISTS (SELECT * FROM TB_PEILUT_OVDIM p
		   WHERE p.taarich= TO_DATE(pDt,'yyyymmdd')
		   AND p.mispar_knisa>0
		   AND p0.mispar_ishi=p.mispar_ishi
		   AND p0.taarich=p.taarich
		   AND p0.mispar_sidur=p.mispar_sidur
		   AND p0.makat_nesia=p.makat_nesia);
 

INSERT INTO TRAIL_PEILUT_OVDIM t
(t.MISPAR_ISHI   ,  t.TAARICH  ,  t.MISPAR_sidur  ,  t.Shat_hatchala_sidur	,
  t.Shat_yetzia		,  t.Mispar_knisa		,  t.Makat_nesia	,  t.Oto_no	,  t.Mispar_siduri_oto	 ,  t.Kisuy_tor		,  t.Bitul_O_Hosafa	,  t.Kod_shinuy_premia	  ,
  t.Snif_tnua	   ,  t.Mispar_visa		,  t.Imut_netzer		,  t.Shat_Bhirat_Nesia_Netzer  ,  t.Oto_No_Netzer		,  t.Mispar_Sidur_Netzer	 ,  t.Shat_yetzia_Netzer	,
  t.Makat_Netzer		,  t.Shilut_Netzer		,  t.Mispar_matala	 ,  t.Dakot_bafoal		,  t.Km_visa 	,  t.TAARICH_IDKUN_ACHARON  ,  t.MEADKEN_ACHARON 	,
  t.MISPAR_ISHI_trail   ,  t.TAARICH_IDKUN_trail      ,  t.Sug_peula  	,  
  t.Mikum_Bhirat_Nesia_Netzer ,  t.heara			,  t.Teur_Nesia ,t.SUG_KNISA)
SELECT DISTINCT p0.MISPAR_ISHI   ,  p0.TAARICH  ,  p0.MISPAR_sidur  ,  p0.Shat_hatchala_sidur	,
  p0.Shat_yetzia		,  p0.Mispar_knisa		,  p0.Makat_nesia	,  p0.Oto_no	,  p0.Mispar_siduri_oto	 ,  p0.Kisuy_tor		,  p0.Bitul_O_Hosafa	,  p0.Kod_shinuy_premia	  ,
  p0.Snif_tnua	   ,  p0.Mispar_visa		,  p0.Imut_netzer		,  p0.Shat_Bhirat_Nesia_Netzer  ,  p0.Oto_No_Netzer		,  p0.Mispar_Sidur_Netzer	 ,  p0.Shat_yetzia_Netzer	,
  p0.Makat_Netzer		,  p0.Shilut_Netzer		,  p0.Mispar_matala	 ,  p0.Dakot_bafoal		,  p0.Km_visa 	,  p0.TAARICH_IDKUN_ACHARON  ,  p0.MEADKEN_ACHARON 	,
 77690  ,  SYSDATE      ,  7 	,  
  p0.Mikum_Bhirat_Nesia_Netzer ,  p0.heara			,  p0.Teur_Nesia  ,p0.SUG_KNISA
FROM TB_PEILUT_OVDIM p0 
WHERE p0.taarich =TO_DATE(pDt,'yyyymmdd')
AND p0.mispar_knisa>0
-- test:
--AND p0.makat_nesia=45712235
AND EXISTS (SELECT * FROM TB_PEILUT_OVDIM p
		   WHERE p.taarich=TO_DATE(pDt,'yyyymmdd')
		   AND p.mispar_knisa=0
		   AND p0.mispar_ishi=p.mispar_ishi
		   AND p0.taarich=p.taarich
		   AND p0.mispar_sidur=p.mispar_sidur
		   AND p0.makat_nesia=p.makat_nesia);

   DELETE FROM TB_PEILUT_OVDIM p0
  WHERE p0.taarich =   TO_DATE(pDt,'yyyymmdd')  
  AND p0.mispar_knisa>0
  -- test:
  --AND p0.makat_nesia=45712235
   AND EXISTS (SELECT * FROM TB_PEILUT_OVDIM p
		   WHERE p.taarich= TO_DATE(pDt,'yyyymmdd')
		   AND p.mispar_knisa=0
		   AND p0.mispar_ishi=p.mispar_ishi
		   AND p0.taarich=p.taarich
		   AND p0.mispar_sidur=p.mispar_sidur
		   AND p0.makat_nesia=p.makat_nesia);

EXCEPTION
   WHEN OTHERS THEN
  	INSERT INTO TB_LOG_TAHALICH
	VALUES (44,1,44,SYSDATE,'',10,'',SUBSTR('ins kniot to trail'||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));

 --in clKDS call getkavdetails using p_Cur
  
END pro_change_Knisot_sdrm;

PROCEDURE pro_Ins_Knisot_sdrm(pDt VARCHAR,p_makat_nesia NUMBER,pMisparKnisa NUMBER,PSugKnisa NUMBER, PTeurNesia VARCHAR) IS
 BEGIN
INSERT INTO TB_PEILUT_OVDIM t
(t.MISPAR_ISHI   ,  t.TAARICH  ,  t.MISPAR_sidur  ,  t.Shat_hatchala_sidur	,  t.Shat_yetzia	,  t.Mispar_knisa	,
  t.Makat_nesia	,  t.Oto_no	,  t.Mispar_siduri_oto	 ,  t.Kisuy_tor,  t.Bitul_O_Hosafa	,  t.Kod_shinuy_premia	 ,
  t.Snif_tnua	   ,  t.Mispar_visa		,  t.Imut_netzer		,  t.Shat_Bhirat_Nesia_Netzer  ,  t.Oto_No_Netzer	,
  t.Mispar_Sidur_Netzer	 ,  t.Shat_yetzia_Netzer	,  t.Makat_Netzer	,  t.Shilut_Netzer	,  t.Mispar_matala	 ,
  t.Dakot_bafoal		,  t.Km_visa 	,  t.TAARICH_IDKUN_ACHARON  ,  t.MEADKEN_ACHARON 	,
  t.Mikum_Bhirat_Nesia_Netzer ,  t.heara			,  t.Teur_Nesia ,	t.SUG_KNISA)
SELECT DISTINCT p.MISPAR_ISHI   ,  p.TAARICH  ,  p.MISPAR_sidur  ,  p.Shat_hatchala_sidur	,  p.Shat_yetzia		,
  pMisparKnisa		,  
  p.Makat_nesia	,  p.Oto_no	,  p.Mispar_siduri_oto	 ,  p.Kisuy_tor		,  p.Bitul_O_Hosafa	,  p.Kod_shinuy_premia	  ,
  p.Snif_tnua	   ,  p.Mispar_visa		,  p.Imut_netzer		,  p.Shat_Bhirat_Nesia_Netzer  ,  p.Oto_No_Netzer	,
  p.Mispar_Sidur_Netzer	 ,  p.Shat_yetzia_Netzer	,  p.Makat_Netzer		,  p.Shilut_Netzer	,  p.Mispar_matala	 ,
  p.Dakot_bafoal		,  p.Km_visa 	,  p.TAARICH_IDKUN_ACHARON  ,  p.MEADKEN_ACHARON 	,
  p.Mikum_Bhirat_Nesia_Netzer ,  p.heara			,  PTeurNesia  , PSugKnisa
FROM TB_PEILUT_OVDIM p
WHERE  p.taarich= TO_DATE(pDt,'yyyymmdd')
  AND p.makat_nesia=p_makat_nesia
  AND p.mispar_knisa=0;
  
  EXCEPTION
   WHEN OTHERS THEN
  	INSERT INTO TB_LOG_TAHALICH
	VALUES (48,1,48,SYSDATE,'',10,'',SUBSTR('Ins kniot'||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));

END pro_Ins_Knisot_sdrm;



PROCEDURE pro_get_Knisot_sdrm(pDt DATE,p_Cur  OUT CurType) IS

--  headers to use in GetKavDetails
     BEGIN
  OPEN p_Cur FOR
  SELECT P0.MAKAT_NESIA, P0.MISPAR_ISHI,p0.MISPAR_SIDUR,P0.TAARICH,P0.SHAT_HATCHALA_SIDUR,P0.SHAT_YETZIA,P0.OTO_NO,P0.MISPAR_SIDURI_OTO,P0.SNIF_TNUA,
                 RANK() OVER(PARTITION BY P0.MAKAT_NESIA   ORDER BY  P0.mispar_ishi  asc  ) AS num                                                    
        FROM TB_PEILUT_OVDIM p0
        WHERE p0.taarich= trunc(pDt)
        AND p0.mispar_knisa=0
        -- test:
    --   AND p0.makat_nesia=46005132
   --    and p0.mispar_ishi=65562; --402131
       AND EXISTS (SELECT * FROM TB_PEILUT_OVDIM p
                   WHERE p.taarich= trunc(pDt)
                   AND p.mispar_knisa>0
                   AND p0.mispar_ishi=p.mispar_ishi
                   AND p0.taarich=p.taarich
                   AND p0.mispar_sidur=p.mispar_sidur
                   AND p0.makat_nesia=p.makat_nesia)
           order by   P0.MAKAT_NESIA, P0.MISPAR_ISHI,num ;  

   /*select distinct MAKAT_NESIA,mispar_ishi,mispar_sidur,taarich,shat_hatchala_sidur,shat_yetzia ,OTO_NO,MISPAR_SIDURI_OTO,SNIF_TNUA,1 num
 FROM TRAIL_PEILUT_OVDIM p0
  WHERE p0.taarich = to_date('20/06/2012','dd/mm/yyyy')
  and trunc(taarich_idkun_trail)= to_date('21/06/2012','dd/mm/yyyy')
  and P0.SUG_PEULA=7;*/

INSERT INTO TRAIL_PEILUT_OVDIM t
(t.MISPAR_ISHI   ,  t.TAARICH  ,  t.MISPAR_sidur  ,  t.Shat_hatchala_sidur    ,
  t.Shat_yetzia        ,  t.Mispar_knisa        ,  t.Makat_nesia    ,  t.Oto_no    ,  t.Mispar_siduri_oto     ,  t.Kisuy_tor        ,  t.Bitul_O_Hosafa    ,  t.Kod_shinuy_premia      ,
  t.Snif_tnua       ,  t.Mispar_visa        ,  t.Imut_netzer        ,  t.Shat_Bhirat_Nesia_Netzer  ,  t.Oto_No_Netzer        ,  t.Mispar_Sidur_Netzer     ,  t.Shat_yetzia_Netzer    ,
  t.Makat_Netzer        ,  t.Shilut_Netzer        ,  t.Mispar_matala     ,  t.Dakot_bafoal        ,  t.Km_visa     ,  t.TAARICH_IDKUN_ACHARON  ,  t.MEADKEN_ACHARON     ,
  t.MISPAR_ISHI_trail   ,  t.TAARICH_IDKUN_trail      ,  t.Sug_peula      ,  
  t.Mikum_Bhirat_Nesia_Netzer ,  t.heara            ,  t.Teur_Nesia ,t.SUG_KNISA)
SELECT DISTINCT p0.MISPAR_ISHI   ,  p0.TAARICH  ,  p0.MISPAR_sidur  ,  p0.Shat_hatchala_sidur    ,
  p0.Shat_yetzia        ,  p0.Mispar_knisa        ,  p0.Makat_nesia    ,  p0.Oto_no    ,  p0.Mispar_siduri_oto     ,  p0.Kisuy_tor        ,  p0.Bitul_O_Hosafa    ,  p0.Kod_shinuy_premia      ,
  p0.Snif_tnua       ,  p0.Mispar_visa        ,  p0.Imut_netzer        ,  p0.Shat_Bhirat_Nesia_Netzer  ,  p0.Oto_No_Netzer        ,  p0.Mispar_Sidur_Netzer     ,  p0.Shat_yetzia_Netzer    ,
  p0.Makat_Netzer        ,  p0.Shilut_Netzer        ,  p0.Mispar_matala     ,  p0.Dakot_bafoal        ,  p0.Km_visa     ,  p0.TAARICH_IDKUN_ACHARON  ,  p0.MEADKEN_ACHARON     ,
 77690  ,  SYSDATE      ,  7     ,  
  p0.Mikum_Bhirat_Nesia_Netzer ,  p0.heara            ,  p0.Teur_Nesia  ,p0.SUG_KNISA
FROM TB_PEILUT_OVDIM p0 
WHERE p0.taarich = trunc(pDt)
AND p0.mispar_knisa>0
-- test:
--AND p0.makat_nesia=402131
AND EXISTS (SELECT * FROM TB_PEILUT_OVDIM p
           WHERE p.taarich=trunc(pDt)
           AND p.mispar_knisa=0
           AND p0.mispar_ishi=p.mispar_ishi
           AND p0.taarich=p.taarich
           AND p0.mispar_sidur=p.mispar_sidur
           AND p0.makat_nesia=p.makat_nesia);

   DELETE FROM TB_PEILUT_OVDIM p0
  WHERE p0.taarich =  trunc(pDt)
  AND p0.mispar_knisa>0
  -- test:
  --AND p0.makat_nesia=402131
   AND EXISTS (SELECT * FROM TB_PEILUT_OVDIM p
           WHERE p.taarich= trunc(pDt)
           AND p.mispar_knisa=0
           AND p0.mispar_ishi=p.mispar_ishi
           AND p0.taarich=p.taarich
           AND p0.mispar_sidur=p.mispar_sidur
           AND p0.makat_nesia=p.makat_nesia);

EXCEPTION
   WHEN OTHERS THEN
      INSERT INTO TB_LOG_TAHALICH
    VALUES (44,1,44,SYSDATE,'',10,'',SUBSTR('ins kniot to trail'||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));

 --in clKDS call getkavdetails using p_Cur
  
END pro_get_Knisot_sdrm;


PROCEDURE pro_insert_knisot(p_coll_obj_peilut_ovdim IN coll_obj_peilut_ovdim) IS
 BEGIN
 
 IF (p_coll_obj_peilut_ovdim IS NOT NULL) THEN
          FOR i IN 1..p_coll_obj_peilut_ovdim.COUNT LOOP
               
                 INSERT INTO TB_PEILUT_OVDIM t
                            (t.MISPAR_ISHI   ,  t.TAARICH  ,  t.MISPAR_sidur  ,  t.Shat_hatchala_sidur    ,  t.Shat_yetzia    ,  t.Mispar_knisa    ,
                              t.Makat_nesia    ,  t.Oto_no    ,  t.Mispar_siduri_oto   ,
                              t.Snif_tnua       , t.TAARICH_IDKUN_ACHARON  ,  t.MEADKEN_ACHARON     , t.Teur_Nesia ,    t.SUG_KNISA)
                values (p_coll_obj_peilut_ovdim(i).mispar_ishi,
                           p_coll_obj_peilut_ovdim(i).taarich ,
                           p_coll_obj_peilut_ovdim(i).mispar_sidur ,       
                           p_coll_obj_peilut_ovdim(i).Shat_hatchala_sidur ,     
                           p_coll_obj_peilut_ovdim(i).Shat_yetzia ,      
                           p_coll_obj_peilut_ovdim(i).Mispar_knisa ,      
                           p_coll_obj_peilut_ovdim(i).Makat_nesia ,      
                           p_coll_obj_peilut_ovdim(i).Oto_no ,  
                           p_coll_obj_peilut_ovdim(i).Mispar_siduri_oto ,       
                           p_coll_obj_peilut_ovdim(i).Snif_tnua ,  
                           sysdate,-12,     
                           p_coll_obj_peilut_ovdim(i).teur_nesia  ,p_coll_obj_peilut_ovdim(i).sug_knisa);                            
       END LOOP;                    
            END IF;
  EXCEPTION
   WHEN OTHERS THEN
      INSERT INTO TB_LOG_TAHALICH
    VALUES (48,1,48,SYSDATE,'',10,'',SUBSTR('Ins kniot'||DBMS_UTILITY.FORMAT_ERROR_STACK,1,1000));

END pro_insert_knisot;


END Pkg_Sdrn;
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

  SELECT SM.ERECH_PARAM INTO param
  FROM TB_PARAMETRIM SM
  WHERE SM.KOD_PARAM =100   AND
	   SYSDATE BETWEEN SM.ME_TAARICH AND SM.AD_TAARICH;
	   
  SELECT ADD_MONTHS(TRUNC(SYSDATE, 'MM'),param*-1) INTO fromDate FROM dual;
   SELECT  LAST_DAY(fromDate) INTO toDate FROM dual;


		INSERT  INTO HISTORY_YAMEY_AVODA_OVDIM(MISPAR_ISHI,TAARICH,SHAT_HATCHALA,SHAT_SIYUM,TACHOGRAF,BITUL_ZMAN_NESIOT,ZMAN_NESIA_HALOCH,ZMAN_NESIA_HAZOR,HALBASHA,LINA,HASHLAMA_LEYOM,SIBAT_HASHLAMA_LEYOM,STATUS,KOD_HISTAYGUT_AUTO,MEASHER_O_MISTAYEG,STATUS_TIPUL,MEADKEN_ACHARON,TAARICH_IDKUN_ACHARON,HEARA,HAMARAT_SHABAT)
		 (SELECT MISPAR_ISHI,TAARICH,SHAT_HATCHALA,SHAT_SIYUM,TACHOGRAF,BITUL_ZMAN_NESIOT,ZMAN_NESIA_HALOCH,ZMAN_NESIA_HAZOR,HALBASHA,LINA,HASHLAMA_LEYOM,SIBAT_HASHLAMA_LEYOM,STATUS,KOD_HISTAYGUT_AUTO,MEASHER_O_MISTAYEG,STATUS_TIPUL,MEADKEN_ACHARON,TAARICH_IDKUN_ACHARON,HEARA,HAMARAT_SHABAT
		  FROM TB_YAMEY_AVODA_OVDIM
		  WHERE Taarich BETWEEN fromDate AND toDate);
  
  
		INSERT INTO HISTORY_SIDURIM_OVDIM(MISPAR_ISHI,MISPAR_SIDUR,TAARICH,SHAT_HATCHALA,SHAT_GMAR,SHAT_HATCHALA_LETASHLUM,SHAT_GMAR_LETASHLUM,PITZUL_HAFSAKA,CHARIGA,TOSEFET_GRIRA,HASHLAMA,YOM_VISA,LO_LETASHLUM,OUT_MICHSA,MIKUM_SHAON_KNISA,MIKUM_SHAON_YETZIA,BITUL_O_HOSAFA,ACHUZ_KNAS_LEPREMYAT_VISA,ACHUZ_VIZA_BESIKUN,tafkid_visa,MISPAR_MUSACH_O_MACHSAN,KOD_SIBA_LO_LETASHLUM,KOD_SIBA_LEDIVUCH_YADANI_IN,KOD_SIBA_LEDIVUCH_YADANI_OUT,SHAYAH_LEYOM_KODEM,MEADKEN_ACHARON,TAARICH_IDKUN_ACHARON,HEARA,MISPAR_SHIUREY_NEHIGA,MEZAKE_HALBASHA,MEZAKE_NESIOT,MIVTZA_VISA,SUG_HAZMANAT_VISA,NIDRESHET_HITIATZVUT,SHAT_HITIATZVUT,PTOR_MEHITIATZVUT,MENAHEL_MUSACH_MEADKEN)
		 (SELECT MISPAR_ISHI,MISPAR_SIDUR,TAARICH,SHAT_HATCHALA,SHAT_GMAR,SHAT_HATCHALA_LETASHLUM,SHAT_GMAR_LETASHLUM,PITZUL_HAFSAKA,CHARIGA,TOSEFET_GRIRA,HASHLAMA,YOM_VISA,LO_LETASHLUM,OUT_MICHSA,MIKUM_SHAON_KNISA,MIKUM_SHAON_YETZIA,BITUL_O_HOSAFA,ACHUZ_KNAS_LEPREMYAT_VISA,ACHUZ_VIZA_BESIKUN,tafkid_visa,MISPAR_MUSACH_O_MACHSAN,KOD_SIBA_LO_LETASHLUM,KOD_SIBA_LEDIVUCH_YADANI_IN,KOD_SIBA_LEDIVUCH_YADANI_OUT,SHAYAH_LEYOM_KODEM,MEADKEN_ACHARON,TAARICH_IDKUN_ACHARON,HEARA,MISPAR_SHIUREY_NEHIGA,MEZAKE_HALBASHA,MEZAKE_NESIOT,MIVTZA_VISA,SUG_HAZMANAT_VISA,NIDRESHET_HITIATZVUT,SHAT_HITIATZVUT,PTOR_MEHITIATZVUT,MENAHEL_MUSACH_MEADKEN
		  FROM TB_SIDURIM_OVDIM
		  WHERE Taarich BETWEEN fromDate AND toDate);
  
		   INSERT INTO HISTORY_PEILUT_OVDIM(MISPAR_ISHI,TAARICH,MISPAR_SIDUR,SHAT_HATCHALA_SIDUR,SHAT_YETZIA,MISPAR_KNISA,MAKAT_NESIA,OTO_NO,MISPAR_SIDURI_OTO,KISUY_TOR,BITUL_O_HOSAFA,KOD_SHINUY_PREMIA,SNIF_TNUA,MISPAR_VISA,IMUT_NETZER,SHAT_BHIRAT_NESIA_NETZER,OTO_NO_NETZER,MISPAR_SIDUR_NETZER,SHAT_YETZIA_NETZER,MAKAT_NETZER,SHILUT_NETZER,MIKUM_BHIRAT_NESIA_NETZER,MISPAR_MATALA,TAARICH_IDKUN_ACHARON,MEADKEN_ACHARON,HEARA,DAKOT_BAFOAL,KM_VISA)
		 (SELECT MISPAR_ISHI,TAARICH,MISPAR_SIDUR,SHAT_HATCHALA_SIDUR,SHAT_YETZIA,MISPAR_KNISA,MAKAT_NESIA,OTO_NO,MISPAR_SIDURI_OTO,KISUY_TOR,BITUL_O_HOSAFA,KOD_SHINUY_PREMIA,SNIF_TNUA,MISPAR_VISA,IMUT_NETZER,SHAT_BHIRAT_NESIA_NETZER,OTO_NO_NETZER,MISPAR_SIDUR_NETZER,SHAT_YETZIA_NETZER,MAKAT_NETZER,SHILUT_NETZER,MIKUM_BHIRAT_NESIA_NETZER,MISPAR_MATALA,TAARICH_IDKUN_ACHARON,MEADKEN_ACHARON,HEARA,DAKOT_BAFOAL,KM_VISA
		  FROM TB_PEILUT_OVDIM
		  WHERE Taarich BETWEEN fromDate AND toDate);
  

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
       OPEN p_Cur FOR
            WITH NIHULY AS (SELECT
                                     EN.YECHIDAT_ABA, EN.YECHIDA_MEKORIT
                            FROM         KDSADMIN.EZ_NIHULY EN
                            CONNECT BY     EN.YECHIDAT_ABA = PRIOR EN.YECHIDA_MEKORIT
                            START WITH     EN.yechida_mekorit = p_yechida ) --    97709
           SELECT   /*+ use_nl (e po o) */
                      E.YECHIDAT_ABA, E.YECHIDA_MEKORIT,
                       O.SHEM_MISH || ' ' || O.SHEM_PRAT || ' (' || O.MISPAR_ISHI || ')'
                           OVED_NAME, O.MISPAR_ISHI
            FROM       KDSADMIN.PIRTEY_OVDIM PO, NIHULY E, KDSADMIN.OVDIM O
            WHERE           TRUNC ( SYSDATE ) >= PO.ME_TAARICH
                       AND TRUNC ( SYSDATE ) <= PO.AD_TAARICH
                       AND PO.KOD_NATUN = 1
                       AND PO.ERECH = E.YECHIDA_MEKORIT
                       AND O.MISPAR_ISHI = PO.MISPAR_ISHI
                       AND po.MISPAR_ISHI LIKE p_prefix
            ORDER BY   mispar_ishi;  




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
BEGIN 
INSERT INTO TMP_MANAGE_TREE 
          SELECT DISTINCT a.mispar_ishi
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

 

END pro_ins_Manage_Tree; 

/*
Ver        Date        Author           Description
   ---------  ----------  ---------------  ------------------------------------
   1.0       02/06/2009      sari       1. ����� ������ ������ �����
*/
PROCEDURE pro_get_etz_nihuly_by_name(p_prefix IN VARCHAR2, p_mispar_ishi IN NUMBER, p_cur OUT CurType) IS
BEGIN
   OPEN p_Cur FOR
          SELECT DISTINCT a.yechidat_aba,a.yechida_mekorit,o.shem_mish || ' ' ||  o.shem_prat  || ' (' || o.mispar_ishi || ')' Oved_Name,a.mispar_ishi
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
				ORDER BY mispar_ishi;


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

if (p_kod_premia <> 103) then

	 OPEN p_cur FOR
            SELECT *
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
                ORDER BY t.MISPAR_ISHI ASC;
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
	 	  SELECT Kod_Premia, Teur_Premia
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
  
 PROCEDURE pro_insert_meadken_acharon(p_mispar_ishi IN NUMBER,p_taarich DATE)
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
                insert into tb_meadken_acharon(MISPAR_ISHI,TAARICH)
                VALUES (p_mispar_ishi,p_taarich);
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
  							
END Pkg_Utils;
/
