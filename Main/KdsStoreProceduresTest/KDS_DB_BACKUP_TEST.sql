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
		PROCEDURE pro_UpdDtKnisotRetro;
        PROCEDURE pro_chk_Dt4_retrospect ;
        PROCEDURE pro_UpdDtKnisotRetrospect ;

END Pkg_Sdrn;
/


CREATE OR REPLACE PACKAGE          PKG_SIDURIM  AS

TYPE	CurType	  IS	REF  CURSOR;
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
END PKG_SIDURIM;
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
      
--    v_me_taarich:=to_date('01/09/2010','dd/mm/yyyy');
 --  v_ad_taarich:=to_date('30/11/2010','dd/mm/yyyy');
      
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
              ,     TB_MISPAR_ISHI_CHISHUV_BAK t
               WHERE o.status=1
           --   AND  o.taarich BETWEEN v_me_taarich AND v_ad_taarich
                --and o.MISPAR_ISHI =44965
           AND T.NUM_PACK=102--
              AND t.mispar_ishi=o.mispar_ishi--
            AND  o.taarich BETWEEN T.TAARICH AND LAST_DAY( T.TAARICH)  --
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
             TB_MISPAR_ISHI_CHISHUV_BAK t,
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
        --AND p.taarich BETWEEN v_me_taarich AND v_ad_taarich
          AND  o.status=2
     AND T.NUM_PACK=102--
      AND t.mispar_ishi=o.mispar_ishi--
      AND  p.taarich BETWEEN T.TAARICH AND LAST_DAY( T.TAARICH)  --
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
       WHERE start_dt>=TRUNC(SYSDATE-(SELECT erech_param FROM TB_PARAMETRIM WHERE kod_param=252))
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
       SELECT TO_CHAR(MIN(start_dt),'yyyymmdd') start_dt
    FROM   kds.kds_control_driver_activities@kds2sdrm
       WHERE start_dt>=TRUNC(SYSDATE-(SELECT erech_param FROM TB_PARAMETRIM WHERE kod_param=252))
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

UPDATE  TB_PARAMETRS_PEILUYOT
 SET ERECH_PARAMETR=Dt4_rerun_rec.start_dt 
WHERE  KOD_KVUZA    =15
AND  KOD_PEILUT_BEKVUZA  =2
AND  SHEM_PARAMETR   ='p_date';

 INSERT INTO  TB_LOG_TAHALICH
VALUES (15,1,1,TO_DATE(Dt4_rerun_rec.start_dt,'yyyymmdd'),0,0,SYSDATE,'retro');
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
WHERE    taarich_sgira>SYSDATE-0.01  
AND kod_tahalich=15 --OR   kod_peilut_tahalich=4)
--and seq=1
AND kod_peilut_tahalich=1;

	IF (idNumber >0) THEN
--todo: DO NOT send error but notice USING SendNotice(INT GroupId, INT ActionId, string Message)
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

		 --cursor1 is for delete from peiluyot and sidurim
		 --sidurim_cursor1 & 3 are to insert into sidurim where count=1
		 --sidurim_cursor2 & 4 are to insert into sidurim where count>1
         -- 02092012: due to shinuy_etc the start is changed, so check exists without time
         -- 06092012: only measher_mistayeg null is treated!!
	 	 --peiluyot1: insert into  peiluyot	
	 --cursor5 & 8: update teur_nesia in  peiluyot


	 --cursor1: measher_mistayeg null, 
	 --			if sidur is not shaonim 99000-99999 then delete
	 --			if sidur is visa  99000-99999 and sector_visa (0,1) then delete
	 --			if sidur is matala 99000-99999 and  MISPAR_MATALA>0 then delete
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
AND y.taarich=s.taarich
UNION ALL
SELECT DISTINCT p.mispar_sidur,p.mispar_ishi,p.taarich,p.shat_hatchala_sidur
FROM TB_PEILUT_OVDIM p,TB_YAMEY_AVODA_OVDIM y  
WHERE p.taarich =TO_DATE(pDt,'yyyymmdd')
AND p.mispar_sidur BETWEEN 99000 AND 99999
AND p.MISPAR_MATALA>0
AND  y.taarich =TO_DATE(pDt,'yyyymmdd')
AND y.measher_o_mistayeg IS NULL
AND y.mispar_ishi=p.mispar_ishi
AND y.taarich=p.taarich; 
	 

	 --cursor3: insert into sidurim	  (while checking measher_mistayeg is null or = 1
CURSOR Sidurim_retro1 IS
SELECT empl,y.taarich,sidur,hatchala,COUNT(*) FROM TB_YAMEY_AVODA_OVDIM y,
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
-- 13092012 
AND k1.start_dt + SUBSTR(LPAD(k1.start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(k1.start_schedule,4,0),3,2)/1440 =shat_hatchala 
AND k1.start_dt + SUBSTR(LPAD(k1.end_schedule,4,0),1,2)/24 + SUBSTR(LPAD(k1.end_schedule,4,0),3,2)/1440 =shat_gmar
-- 13092012 
))
WHERE y.taarich=TO_DATE(pDt,'yyyymmdd')
AND y.mispar_ishi=empl
AND   y.measher_o_mistayeg is null
GROUP BY empl,y.taarich,sidur,hatchala
HAVING COUNT(*)=1;

CURSOR Sidurim_retro2 IS
SELECT empl,y.taarich,sidur,hatchala,COUNT(*) FROM TB_YAMEY_AVODA_OVDIM y,
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
-- 13092012 
AND k1.start_dt + SUBSTR(LPAD(k1.start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(k1.start_schedule,4,0),3,2)/1440 =shat_hatchala 
AND k1.start_dt + SUBSTR(LPAD(k1.end_schedule,4,0),1,2)/24 + SUBSTR(LPAD(k1.end_schedule,4,0),3,2)/1440 =shat_gmar
-- 13092012 
))
WHERE y.taarich=TO_DATE(pDt,'yyyymmdd')
AND y.mispar_ishi=empl
AND  y.measher_o_mistayeg is null
GROUP BY empl,y.taarich,sidur,hatchala
HAVING COUNT(*)>1;

-- this is based on sidurim_retro1 :bulk is most of the sidurim as is. measher_mistayeg is checked at sidurim_retro1
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
AND k1.start_schedule=p_hatchala
AND NOT ( SUBSTR(makat_line,1,3)=756 AND line_description='רציפות נהיגה');

-- this will be based on sidurim_retro2 :the duplicates. measher_mistayeg is checked at sidurim_retro2
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
AND k1.start_schedule=p_hatchala
AND NOT ( SUBSTR(makat_line,1,3)=756 AND line_description='רציפות נהיגה');

	 --cursor4: insert into  peiluyot	, element 756 is added separately
	    CURSOR Peilut_retro1 IS
   SELECT    driver_id,start_dt,schedule_num,branch,
start_dt + SUBSTR(LPAD(start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(start_schedule,4,0),3,2)/1440 hatchala ,
start_dt + SUBSTR(LPAD(start_time,4,0),1,2)/24 + SUBSTR(LPAD(start_time,4,0),3,2)/1440 gmar,ride_id,
 makat_line,bus_number,bus_sequence,waiting_time,
 spm_time,spm_bus_number,spm_schedule_num,spm_start_time,spm_makat_line,spm_line_sign,spm_location,-12 meadken
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm,TB_YAMEY_AVODA_OVDIM y
WHERE  start_dt= TO_DATE(pDt,'yyyymmdd')
AND NOT (   ride_id=-1 AND SUBSTR(makat_line,1,3)=756 AND line_description='רציפות נהיגה')
AND NOT EXISTS (SELECT * FROM TB_PEILUT_OVDIM p WHERE p.taarich= TO_DATE(pDt,'yyyymmdd')
			   AND driver_id=p.mispar_ishi
 			   AND start_dt=p.taarich
			   AND schedule_num=p.mispar_sidur)
			   --AND branch=snif_tnua
			   -- 02092012 AND shat_hatchala_sidur=start_dt + SUBSTR(LPAD(start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(start_schedule,4,0),3,2)/1440  
			   -- 02092012 AND shat_yetzia=start_dt + SUBSTR(LPAD(start_time,4,0),1,2)/24 + SUBSTR(LPAD(start_time,4,0),3,2)/1440  
			   -- 02092012 AND mispar_knisa=ride_id
               --20120910)
and y.taarich= TO_DATE(pDt,'yyyymmdd')
AND start_dt=y.taarich
AND y.mispar_ishi=driver_id
AND  y.measher_o_mistayeg is null;


	 --cursor5 & 8: update teur_nesia in  peiluyot	
CURSOR Peilut5  IS
  SELECT   DISTINCT    start_dt, driver_id,schedule_num,
start_dt + SUBSTR(LPAD(start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(start_schedule,4,0),3,2)/1440 hatchala,
start_dt + SUBSTR(LPAD(start_time,4,0),1,2)/24 + SUBSTR(LPAD(start_time,4,0),3,2)/1440 moed,
				 makat_line,ride_id,  line_description
 FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm,tb_yamey_avoda_ovdim y
						   	WHERE  start_dt=  TO_DATE(pDt,'yyyymmdd')
							AND line_description IS NOT NULL
							AND NVL(makat_line,0)=0
							AND schedule_num<1000
							AND NOT (   ride_id=-1 AND SUBSTR(makat_line,1,3)=756 AND line_description='רציפות נהיגה')
                            and y.taarich=  TO_DATE(pDt,'yyyymmdd')
                            and y.taarich=start_dt
                            and y.mispar_ishi=driver_id
                            and y.measher_o_mistayeg is null;
  
CURSOR Peilut8  IS
  SELECT   DISTINCT    start_dt, driver_id,schedule_num,
start_dt + SUBSTR(LPAD(start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(start_schedule,4,0),3,2)/1440 hatchala,
start_dt + SUBSTR(LPAD(start_time,4,0),1,2)/24 + SUBSTR(LPAD(start_time,4,0),3,2)/1440 moed,
				 makat_line,ride_id,  line_description
 FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm,tb_yamey_avoda_ovdim y
						   	WHERE  start_dt=  TO_DATE(pDt,'yyyymmdd')
							AND line_description IS NOT NULL
							AND ride_id>0
							AND makat_line<50000000
 and y.taarich=  TO_DATE(pDt,'yyyymmdd')
                            and y.taarich=start_dt
                            and y.mispar_ishi=driver_id
                            and y.measher_o_mistayeg is null;
	 
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
  Sector_Visa 		,  MEADKEN_ACHARON 	 ,  TAARICH_IDKUN_ACHARON   ,  MISPAR_ISHI_trail     ,  TAARICH_IDKUN_trail   , Sug_peula , heara	,sug_sidur,
   shat_hatchala_letashlum_musach, shat_gmar_letashlum_musach	 )
SELECT  p.MISPAR_ISHI  ,  MISPAR_sidur    ,  p.TAARICH ,  p.Shat_hatchala		,
-- 20120905: distinct?  
  Shat_gmar		,  Shat_hatchala_letashlum ,  Shat_gmar_letashlum	,  Pitzul_hafsaka	,  Chariga	,  Tosefet_Grira	,  Hashlama,  sug_hashlama 		,
  Yom_Visa		,  Lo_letashlum	,  Out_michsa	,  Mikum_shaon_knisa	 ,  Mikum_shaon_yetzia	,  Achuz_Knas_LePremyat_Visa	,  Achuz_Viza_Besikun	,
  Mispar_Musach_O_Machsan	,  Kod_siba_lo_letashlum		,  Kod_siba_ledivuch_yadani_in	,  Kod_siba_ledivuch_yadani_out	,  Menahel_Musach_Meadken	,
  Shayah_LeYom_Kodem 	,  Mispar_shiurey_nehiga	,  Mezake_Halbasha 	,  Mezake_nesiot		,    Sug_Hazmanat_Visa ,  Bitul_O_Hosafa	,
  tafkid_visa 		,  mivtza_visa 		,  Nidreshet_hitiatzvut 		,  Shat_hitiatzvut	,  Ptor_Mehitiatzvut 	,  Hachtama_Beatar_Lo_Takin  ,  Hafhatat_Nochechut_Visa	,
  Sector_Visa 		,  p.MEADKEN_ACHARON 	 ,  p.TAARICH_IDKUN_ACHARON  ,   77690  ,  SYSDATE  ,8    ,  p.heara 	,p.sug_sidur,
  p.shat_hatchala_letashlum_musach, p.shat_gmar_letashlum_musach
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
  t.Mikum_Bhirat_Nesia_Netzer ,  t.heara			,  t.Teur_Nesia, t.sug_knisa )
SELECT   p.MISPAR_ISHI   ,  p.TAARICH  ,  p.MISPAR_sidur  ,  p.Shat_hatchala_sidur	,
-- 20120905: distinct?  
  p.Shat_yetzia		,  p.Mispar_knisa		,  p.Makat_nesia	,  p.Oto_no	,  p.Mispar_siduri_oto	 ,  p.Kisuy_tor		,  p.Bitul_O_Hosafa	,  p.Kod_shinuy_premia	  ,
  p.Snif_tnua	   ,  p.Mispar_visa		,  p.Imut_netzer		,  p.Shat_Bhirat_Nesia_Netzer  ,  p.Oto_No_Netzer		,  p.Mispar_Sidur_Netzer	 ,  p.Shat_yetzia_Netzer	,
  p.Makat_Netzer		,  p.Shilut_Netzer		,  p.Mispar_matala	 ,  p.Dakot_bafoal		,  p.Km_visa 	,  p.TAARICH_IDKUN_ACHARON  ,  p.MEADKEN_ACHARON 	,
 77690  ,  SYSDATE      ,  8 	,  
  p.Mikum_Bhirat_Nesia_Netzer ,  p.heara			,  p.Teur_Nesia  ,p.sug_knisa
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
 	VALUES (15,1,1,SYSDATE,'',10,'',SUBSTR(TO_CHAR(retro1_rec.mispar_ishi) ||' '||TO_CHAR(retro1_rec.mispar_sidur) ||' '||TO_CHAR(retro1_rec.taarich)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));   

END;
END LOOP;
COMMIT;


-- this is equal to the normal procedure
   Pkg_Sdrn.pro_ins_yamim_4_sidurim(pDt);
   
-- insert sidurim:
   FOR  Sidurim_retro1_rec IN  Sidurim_retro1 LOOP
 
   		FOR  sidurim_retro3_rec IN  sidurim_retro3(sidurim_retro1_rec.empl,sidurim_retro1_rec.taarich,sidurim_retro1_rec.sidur,sidurim_retro1_rec.hatchala)  LOOP
 		BEGIN
  INSERT INTO TB_SIDURIM_OVDIM s ( mispar_ishi  , taarich  , mispar_sidur  , shat_hatchala  , shat_gmar,sector_visa,meadken_acharon   ,sug_sidur ,
 Lo_letashlum ,Kod_Siba_Lo_Letashlum     )
--VALUES (sidurim_retro3_rec.driver_id,sidurim_retro3_rec.start_dt+100,  sidurim_retro3_rec.schedule_num,  
VALUES (sidurim_retro3_rec.driver_id,sidurim_retro3_rec.start_dt,  sidurim_retro3_rec.schedule_num,  
sidurim_retro3_rec.hatchala,sidurim_retro3_rec.gmar,  sidurim_retro3_rec.sug_visa,   sidurim_retro3_rec.meadken, sidurim_retro3_rec.sug_sidur ,
 1 ,16  );
-- 20120712
--20120906:   UPDATE  TB_YAMEY_AVODA_OVDIM yao
--20120906:   SET   measher_o_mistayeg =NULL
--20120906:   WHERE yao.taarich =  sidurim_retro3_rec.start_dt
--20120906:   AND yao.mispar_ishi= sidurim_retro3_rec.driver_id
--20120906:   AND measher_o_mistayeg =1
--20120906:   AND NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM so
--20120906:           WHERE so.meadken_acharon=-12
--20120906:           AND so.mispar_ishi=yao.mispar_ishi
--20120906:           AND  so.taarich=yao.taarich
--20120906:      	   AND so.taarich =  sidurim_retro3_rec.start_dt
--20120906:   		   AND so.mispar_ishi= sidurim_retro3_rec.driver_id
--20120906:           AND so.mispar_sidur<>99200);

EXCEPTION
   WHEN OTHERS THEN
	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,3,20,SYSDATE,'',10,'',SUBSTR(TO_CHAR(sidurim_retro3_rec.driver_id) ||' retro '||TO_CHAR(sidurim_retro3_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
	  END;
 		   END LOOP;
	 
END LOOP;
COMMIT;

   FOR  sidurim_retro2_rec IN  sidurim_retro2 LOOP
 idNumber:=0; 
   		FOR  sidurim_retro4_rec IN  sidurim_retro4(sidurim_retro2_rec.empl,sidurim_retro2_rec.taarich,sidurim_retro2_rec.sidur,sidurim_retro2_rec.hatchala)  LOOP
 		BEGIN
  INSERT INTO TB_SIDURIM_OVDIM s ( mispar_ishi  , taarich  , mispar_sidur  , shat_hatchala  , shat_gmar,sector_visa,meadken_acharon   ,sug_sidur ,
 Lo_letashlum ,Kod_Siba_Lo_Letashlum     )
--VALUES (sidurim_retro4_rec.driver_id,sidurim_retro4_rec.start_dt+100,  sidurim_retro4_rec.schedule_num,  
VALUES (sidurim_retro4_rec.driver_id,sidurim_retro4_rec.start_dt,  sidurim_retro4_rec.schedule_num,  
sidurim_retro4_rec.hatchala+idNumber/1440,sidurim_retro4_rec.gmar,  sidurim_retro4_rec.sug_visa,   sidurim_retro4_rec.meadken, sidurim_retro4_rec.sug_sidur,
1,16 );
idNumber:=idNumber+1;
-- 20120712
--20120906:   UPDATE  TB_YAMEY_AVODA_OVDIM yao
--20120906:   SET   measher_o_mistayeg =NULL
--20120906:   WHERE yao.taarich =  sidurim_retro4_rec.start_dt
--20120906:   AND yao.mispar_ishi= sidurim_retro4_rec.driver_id
--20120906:   AND measher_o_mistayeg =1
--20120906:   AND NOT EXISTS (SELECT * FROM TB_SIDURIM_OVDIM so
--20120906:           WHERE so.meadken_acharon=-12
--20120906:           AND so.mispar_ishi=yao.mispar_ishi
--20120906:           AND  so.taarich=yao.taarich
--20120906:      	   AND so.taarich =  sidurim_retro4_rec.start_dt
--20120906:   		   AND so.mispar_ishi= sidurim_retro4_rec.driver_id
--20120906:           AND so.mispar_sidur<>99200);

EXCEPTION
   WHEN OTHERS THEN
	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,4,21,SYSDATE,'',10,'',SUBSTR(TO_CHAR(sidurim_retro4_rec.driver_id) ||' new '||TO_CHAR(sidurim_retro4_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
	  END;
 		   END LOOP;
	 
END LOOP;
COMMIT;

 BEGIN
 UPDATE  TB_SIDURIM_OVDIM 
 SET   Shayah_LeYom_Kodem=1   
WHERE  taarich = TO_DATE(pDt,'yyyymmdd')--+100
AND meadken_acharon=-12 AND taarich_idkun_acharon>SYSDATE-0.2
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
    VALUES (15,5,22,SYSDATE,'',10,'',SUBSTR(pDt||' Shayah_LeYom_Kodem retro '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));     
END;
COMMIT;

--20120906:BEGIN
 --20120906:UPDATE TB_SIDURIM_OVDIM 
--20120906:SET   Lo_letashlum=1 ,Kod_Siba_Lo_Letashlum=16
 --20120906:WHERE taarich= TO_DATE(pDt,'yyyymmdd')--+100
--20120906:AND meadken_acharon=-12 AND taarich_idkun_acharon>SYSDATE-0.1;

  --20120906:EXCEPTION
   --20120906:WHEN OTHERS THEN
	--20120906:INSERT INTO TB_LOG_TAHALICH
	--20120906:VALUES (15,6,2,SYSDATE,'',10,'',SUBSTR('update_sidurim retro'||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));   

--20120906:END;
--20120906:COMMIT;

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
	VALUES (15,7,30,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Peilut_retro1_rec.driver_id) ||'update_peilut retro'||TO_CHAR(Peilut_retro1_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
   END;
END LOOP;
COMMIT;

BEGIN
--in case of duplicate key in element 756, add 1 to shat_yetzia and update ride_id (knisot) to 0
 INSERT INTO TB_PEILUT_OVDIM (mispar_ishi,taarich,mispar_sidur,  snif_tnua,shat_hatchala_sidur,shat_yetzia,mispar_knisa,
 makat_nesia,oto_no,mispar_siduri_oto,kisuy_tor,
  Shat_Bhirat_Nesia_Netzer , Oto_No_Netzer, Mispar_Sidur_Netzer,  Shat_yetzia_Netzer,  Makat_Netzer, Shilut_Netzer ,
  Mikum_Bhirat_Nesia_Netzer ,meadken_acharon ,teur_nesia )
   SELECT    driver_id,start_dt,schedule_num,branch,
start_dt + SUBSTR(LPAD(start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(start_schedule,4,0),3,2) /1440   ,
start_dt + SUBSTR(LPAD(start_time,4,0),1,2)/24 + (SUBSTR(LPAD(start_time,4,0),3,2)+1)/1440  ,0  ,
 makat_line,bus_number,bus_sequence,waiting_time,
 spm_time,spm_bus_number,spm_schedule_num,spm_start_time,spm_makat_line,spm_line_sign,spm_location,-12,line_description 
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm,tb_yamey_avoda_ovdim y
WHERE  start_dt= TO_DATE(pDt,'yyyymmdd')
AND  (   ride_id=-1 AND SUBSTR(makat_line,1,3)=756 AND line_description='רציפות נהיגה')
AND NOT EXISTS (SELECT * FROM TB_PEILUT_OVDIM p WHERE p.taarich= TO_DATE(pDt,'yyyymmdd')
			   AND driver_id=p.mispar_ishi
 			   AND start_dt=p.taarich
			   AND schedule_num=p.mispar_sidur
			   --AND branch=snif_tnua
			   AND shat_hatchala_sidur=start_dt + SUBSTR(LPAD(start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(start_schedule,4,0),3,2)/1440  
			   AND shat_yetzia=start_dt + SUBSTR(LPAD(start_time,4,0),1,2)/24 + SUBSTR(LPAD(start_time,4,0),3,2)/1440  
			   AND mispar_knisa=ride_id)
and y.taarich= TO_DATE(pDt,'yyyymmdd')
and y.taarich=start_dt
and y.mispar_ishi=driver_id
and y.measher_o_mistayeg is null               ;
EXCEPTION
   WHEN OTHERS THEN
  	INSERT INTO TB_LOG_TAHALICH
	VALUES (15,7,31,SYSDATE,'',10,'',SUBSTR(' retro 756 רציפות '||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
   END;
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
	VALUES (15,8,42,SYSDATE,'',10,'',SUBSTR('update teur peilut retro '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
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
	VALUES (15,9,39,SYSDATE,'',10,'',SUBSTR('update teur peilut retro '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
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

-- these r the duplicates, such as הכנת מכונה or רציפות נהיגה etc
CURSOR Sidurim2 IS
SELECT empl,taarich,sidur,hatchala,COUNT(*) FROM
(SELECT   DISTINCT k1.driver_id empl,k1.start_dt taarich,k1.schedule_num sidur,
k1.start_schedule  hatchala ,k1.end_schedule   gmar
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm k1 
WHERE k1.start_dt = TO_DATE(pDt,'yyyymmdd')) 
GROUP BY empl,taarich,sidur,hatchala
HAVING COUNT(*)>1;


-- this is based on cursor1 :bulk is most of the sidurim, ommiting 756
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
AND k1.start_schedule=p_hatchala
AND NOT ( SUBSTR(makat_line,1,3)=756 AND line_description='רציפות נהיגה');

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
AND k1.start_schedule=p_hatchala
AND NOT ( SUBSTR(makat_line,1,3)=756 AND line_description='רציפות נהיגה');


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
	VALUES (4,1,20,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Sidurim3_rec.driver_id) ||' new '||TO_CHAR(Sidurim3_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
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
	VALUES (4,1,21,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Sidurim4_rec.driver_id) ||' new '||TO_CHAR(Sidurim4_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
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
AND NOT (   ride_id=-1 AND SUBSTR(makat_line,1,3)=756 AND line_description='רציפות נהיגה');


CURSOR Peilut5  IS
  SELECT   DISTINCT    start_dt, driver_id,schedule_num,
start_dt + SUBSTR(LPAD(start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(start_schedule,4,0),3,2)/1440 hatchala,
start_dt + SUBSTR(LPAD(start_time,4,0),1,2)/24 + SUBSTR(LPAD(start_time,4,0),3,2)/1440 moed,
				 makat_line,ride_id,  line_description
 FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
						   	WHERE  start_dt=  TO_DATE(pDt,'yyyymmdd')
							AND line_description IS NOT NULL
							AND NVL(makat_line,0)=0
							AND schedule_num<1000
							AND NOT (   ride_id=-1 AND SUBSTR(makat_line,1,3)=756 AND line_description='רציפות נהיגה');
  
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
	VALUES (4,1,30,SYSDATE,'',10,'',SUBSTR(TO_CHAR(Peilut1_rec.driver_id) ||' new '||TO_CHAR(Peilut1_rec.schedule_num)||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
   END;
END LOOP;
COMMIT;

BEGIN
--in case of duplicate key in element 756, add 1 to shat_yetzia and update ride_id (knisot) to 0
 INSERT INTO TB_PEILUT_OVDIM (mispar_ishi,taarich,mispar_sidur,  snif_tnua,shat_hatchala_sidur,shat_yetzia,mispar_knisa,
 makat_nesia,oto_no,mispar_siduri_oto,kisuy_tor,
  Shat_Bhirat_Nesia_Netzer , Oto_No_Netzer, Mispar_Sidur_Netzer,  Shat_yetzia_Netzer,  Makat_Netzer, Shilut_Netzer ,
  Mikum_Bhirat_Nesia_Netzer ,meadken_acharon ,teur_nesia )
   SELECT    driver_id,start_dt,schedule_num,branch,
start_dt + SUBSTR(LPAD(start_schedule,4,0),1,2)/24 + SUBSTR(LPAD(start_schedule,4,0),3,2) /1440   ,
start_dt + SUBSTR(LPAD(start_time,4,0),1,2)/24 + (SUBSTR(LPAD(start_time,4,0),3,2)+1)/1440  ,0  ,
 makat_line,bus_number,bus_sequence,waiting_time,
 spm_time,spm_bus_number,spm_schedule_num,spm_start_time,spm_makat_line,spm_line_sign,spm_location,-12,line_description 
FROM kds.KDS_DRIVER_ACTIVITIES@kds2sdrm
WHERE  start_dt= TO_DATE(pDt,'yyyymmdd')
AND  (   ride_id=-1 AND SUBSTR(makat_line,1,3)=756 AND line_description='רציפות נהיגה') ;
EXCEPTION
   WHEN OTHERS THEN
  	INSERT INTO TB_LOG_TAHALICH
	VALUES (4,1,3,SYSDATE,'',10,'',SUBSTR(' 756 רציפות '||' '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
   END;
COMMIT;

 
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
	VALUES (4,1,42,SYSDATE,'',10,'',SUBSTR('update teur peilut new '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
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
	VALUES (4,1,39,SYSDATE,'',10,'',SUBSTR('update teur peilut new '||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));
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
	VALUES (44,1,44,SYSDATE,'',10,'',SUBSTR('ins kniot to trail'||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));

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
	VALUES (48,1,48,SYSDATE,'',10,'',SUBSTR('Ins kniot'||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));

END pro_Ins_Knisot_sdrm;



PROCEDURE pro_get_Knisot_sdrm(pDt DATE,p_Cur  OUT CurType) IS

--  headers to use in GetKavDetails
     BEGIN
  OPEN p_Cur FOR
  SELECT P0.MAKAT_NESIA, P0.MISPAR_ISHI,p0.MISPAR_SIDUR,P0.TAARICH,P0.SHAT_HATCHALA_SIDUR,P0.SHAT_YETZIA,P0.OTO_NO,P0.MISPAR_SIDURI_OTO,P0.SNIF_TNUA,
                 RANK() OVER(PARTITION BY P0.MAKAT_NESIA   ORDER BY  P0.mispar_ishi  ASC  ) AS num                                                    
        FROM TB_PEILUT_OVDIM p0
        WHERE p0.taarich= TRUNC(pDt)
        AND p0.mispar_knisa=0
        -- test:
    --   AND p0.makat_nesia=46005132
   --    and p0.mispar_ishi=65562; --402131
       AND EXISTS (SELECT * FROM TB_PEILUT_OVDIM p
                   WHERE p.taarich= TRUNC(pDt)
                   AND p.mispar_knisa>0
                   AND p0.mispar_ishi=p.mispar_ishi
                   AND p0.taarich=p.taarich
                   AND p0.mispar_sidur=p.mispar_sidur
                   AND p0.makat_nesia=p.makat_nesia)
           ORDER BY   P0.MAKAT_NESIA, P0.MISPAR_ISHI,num ;  

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
WHERE p0.taarich = TRUNC(pDt)
AND p0.mispar_knisa>0
-- test:
--AND p0.makat_nesia=402131
AND EXISTS (SELECT * FROM TB_PEILUT_OVDIM p
           WHERE p.taarich=TRUNC(pDt)
           AND p.mispar_knisa=0
           AND p0.mispar_ishi=p.mispar_ishi
           AND p0.taarich=p.taarich
           AND p0.mispar_sidur=p.mispar_sidur
           AND p0.makat_nesia=p.makat_nesia);

   DELETE FROM TB_PEILUT_OVDIM p0
  WHERE p0.taarich =  TRUNC(pDt)
  AND p0.mispar_knisa>0
  -- test:
  --AND p0.makat_nesia=402131
   AND EXISTS (SELECT * FROM TB_PEILUT_OVDIM p
           WHERE p.taarich= TRUNC(pDt)
           AND p.mispar_knisa=0
           AND p0.mispar_ishi=p.mispar_ishi
           AND p0.taarich=p.taarich
           AND p0.mispar_sidur=p.mispar_sidur
           AND p0.makat_nesia=p.makat_nesia);

EXCEPTION
   WHEN OTHERS THEN
      INSERT INTO TB_LOG_TAHALICH
    VALUES (44,1,44,SYSDATE,'',10,'',SUBSTR('ins kniot to trail'||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));

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
                VALUES (p_coll_obj_peilut_ovdim(i).mispar_ishi,
                           p_coll_obj_peilut_ovdim(i).taarich ,
                           p_coll_obj_peilut_ovdim(i).mispar_sidur ,       
                           p_coll_obj_peilut_ovdim(i).Shat_hatchala_sidur ,     
                           p_coll_obj_peilut_ovdim(i).Shat_yetzia ,      
                           p_coll_obj_peilut_ovdim(i).Mispar_knisa ,      
                           p_coll_obj_peilut_ovdim(i).Makat_nesia ,      
                           p_coll_obj_peilut_ovdim(i).Oto_no ,  
                           p_coll_obj_peilut_ovdim(i).Mispar_siduri_oto ,       
                           p_coll_obj_peilut_ovdim(i).Snif_tnua ,  
                           SYSDATE,-12,     
                           p_coll_obj_peilut_ovdim(i).teur_nesia  ,p_coll_obj_peilut_ovdim(i).sug_knisa);                            
       END LOOP;                    
            END IF;
  EXCEPTION
   WHEN OTHERS THEN
      INSERT INTO TB_LOG_TAHALICH
    VALUES (48,1,48,SYSDATE,'',10,'',SUBSTR('Ins kniot'||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));

END pro_insert_knisot;

PROCEDURE pro_UpdDtKnisotRetro  IS
 BEGIN

UPDATE  TB_PARAMETRS_PEILUYOT
 SET ERECH_PARAMETR='01/01/2000'
WHERE  KOD_KVUZA    =15
AND  KOD_PEILUT_BEKVUZA  =2
AND  SHEM_PARAMETR   ='p_date';
  EXCEPTION
   WHEN OTHERS THEN
      INSERT INTO TB_LOG_TAHALICH
    VALUES (15,3,1,SYSDATE,'',10,'',SUBSTR('Ins kniot retro'||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));

END pro_UpdDtKnisotRetro;


   PROCEDURE pro_chk_Dt4_retrospect IS

 idNumber NUMBER;
err_str VARCHAR(1000);
        
CURSOR  stam4 IS
SELECT teur_tech
 FROM TB_LOG_TAHALICH
WHERE   taarich>SYSDATE-0.01
AND kod_tahalich=19
AND kod_peilut_tahalich=1
--and seq=1
UNION ALL
SELECT teur_tech
 FROM TB_LOG_TAHALICH
WHERE   taarich>SYSDATE-0.01
AND kod_tahalich=4
AND kod_peilut_tahalich=11;
--and seq=1
--order by taarich;

   CURSOR Dt4_retrospect IS
       SELECT TO_CHAR(MIN(start_dt),'yyyymmdd') start_dt
    FROM   kds.kds_control_driver_activities@kds2sdrm
       WHERE start_dt>=TRUNC(SYSDATE-(SELECT erech_param FROM TB_PARAMETRIM WHERE kod_param=252))
    AND status=1
    ORDER BY start_dt;

BEGIN
  --err_str:='';
 idNumber:=0;
err_str:='';

  
FOR  Dt4_retrospect_rec IN  Dt4_retrospect LOOP
BEGIN

   Pkg_Sdrn.pro_ins_yamim_4_sidurim( Dt4_retrospect_rec.start_dt); --,'yyyymmdd'
   Pkg_Sdrn.pro_ins_sidurim_from_sdrm(Dt4_retrospect_rec.start_dt);
   Pkg_Sdrn.pro_ins_peilut_from_sdrm(Dt4_retrospect_rec.start_dt);
   Pkg_Sdrn.pro_upd_sdrm_control(Dt4_retrospect_rec.start_dt);
   Pkg_Batch.pro_upd_yamey_avoda_ovdim(Dt4_retrospect_rec.start_dt);

UPDATE  TB_PARAMETRS_PEILUYOT
 SET ERECH_PARAMETR=Dt4_retrospect_rec.start_dt 
WHERE  KOD_KVUZA    =19
AND  KOD_PEILUT_BEKVUZA  =2
AND  SHEM_PARAMETR   ='p_date';

 INSERT INTO  TB_LOG_TAHALICH
VALUES (19,1,1,TO_DATE(Dt4_retrospect_rec.start_dt,'yyyymmdd'),0,0,SYSDATE,'retrospect');
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
WHERE   taarich_sgira>SYSDATE-0.01  
AND kod_tahalich=19 --OR   kod_peilut_tahalich=4)
--and seq=1
AND kod_peilut_tahalich=1;

    IF (idNumber >0) THEN
--todo: DO NOT send error but notice USING SendNotice(INT GroupId, INT ActionId, string Message)
    RAISE_APPLICATION_ERROR(-20004, SUBSTR(err_str,1,100), TRUE);
 END IF;
 
  EXCEPTION
     WHEN NO_DATA_FOUND  THEN
                                 idNumber:=0;
   WHEN OTHERS THEN
        RAISE;
  
END pro_chk_Dt4_retrospect;
PROCEDURE pro_UpdDtKnisotRetrospect  IS
 BEGIN

UPDATE  TB_PARAMETRS_PEILUYOT
 SET ERECH_PARAMETR='01/01/2000'
WHERE  KOD_KVUZA    =19
AND  KOD_PEILUT_BEKVUZA  =2
AND  SHEM_PARAMETR   ='p_date';
  EXCEPTION
   WHEN OTHERS THEN
      INSERT INTO TB_LOG_TAHALICH
    VALUES (19,3,1,SYSDATE,'',10,'',SUBSTR('Ins kniot retrospect'||DBMS_UTILITY.FORMAT_ERROR_STACK,1,100));

END pro_UpdDtKnisotRetrospect;

END Pkg_Sdrn;
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
	 	  IF p_sidur_position != NULL THEN

		  		  	   IF p_sidur_position = 'first' THEN

							   SELECT MIN(tbs.SHAT_HATCHALA)  INTO shat_hatchala
							   FROM TB_SIDURIM_OVDIM tbs
					   		   WHERE  tbs.TAARICH = TO_DATE(TO_CHAR(p_taarich, 'dd/mm/yyyy'), 'dd/mm/yyyy')
                               AND tbs.mispar_sidur<>99200
								          AND tbs.MISPAR_ISHI = p_mispar_ishi
                                          AND tbs.lo_letashlum=0;
										--  AND (tbs.BITUL_O_HOSAFA=2 OR   tbs.BITUL_O_HOSAFA=4);

						   		OPEN p_cur FOR
						   		SELECT *
					   			FROM TB_SIDURIM_OVDIM tb
					   			WHERE  tb.TAARICH = TO_DATE(TO_CHAR(p_taarich, 'dd/mm/yyyy'), 'dd/mm/yyyy')
									      AND tb.MISPAR_ISHI = p_mispar_ishi
                                         AND tb.mispar_sidur<>99200
                                         AND tb.SHAT_HATCHALA = shat_hatchala
                                          AND tb.lo_letashlum=0;
									--	  AND (tb.BITUL_O_HOSAFA=2 OR   tb.BITUL_O_HOSAFA=4);

					   ELSIF p_sidur_position = 'last' THEN

							   SELECT MAX(tbs.SHAT_HATCHALA)  INTO shat_hatchala
							   FROM TB_SIDURIM_OVDIM tbs
					   		   WHERE  tbs.TAARICH = TO_DATE(TO_CHAR(p_taarich, 'dd/mm/yyyy'), 'dd/mm/yyyy')
                               AND tbs.mispar_sidur<>99200
								          AND tbs.MISPAR_ISHI = p_mispar_ishi
                                      AND tbs.lo_letashlum=0;
									--	  AND (tbs.BITUL_O_HOSAFA=2 OR   tbs.BITUL_O_HOSAFA=4);

								OPEN p_cur FOR
								SELECT *
					   			FROM TB_SIDURIM_OVDIM tb
					   			WHERE  tb.TAARICH = TO_DATE(TO_CHAR(p_taarich, 'dd/mm/yyyy'), 'dd/mm/yyyy')
                                AND tb.mispar_sidur<>99200
									      AND tb.MISPAR_ISHI = p_mispar_ishi
									      AND tb.SHAT_HATCHALA = shat_hatchala
                                          AND tb.lo_letashlum=0;
									--	 AND (tb.BITUL_O_HOSAFA=2 OR   tb.BITUL_O_HOSAFA=4) ;
					 END IF;

		  ELSE

				  	   OPEN p_cur FOR
				  	   SELECT *
					   FROM TB_SIDURIM_OVDIM tb
					   WHERE  tb.Taarich = TO_DATE(TO_CHAR(p_taarich, 'dd/mm/yyyy'), 'dd/mm/yyyy')
                       AND tb.mispar_sidur<>99200
				                  AND tb.MISPAR_ISHI = p_mispar_ishi
                                  AND tb.lo_letashlum=0;
							--	  AND (tb.BITUL_O_HOSAFA=2 OR   tb.BITUL_O_HOSAFA=4);
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
			 FROM 	 TB_SIDURIM_OVDIM
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
							--	  SM.KOD_SIDUR_MEYUCHAD not in(whithOutListMisSidur) AND
	  		  		  	          SM.KOD_SIDUR_MEYUCHAD NOT  IN(
												  	  	   	  	   SELECT DISTINCT( t.MISPAR_SIDUR)
																   FROM TB_SIDURIM_MEYUCHADIM t
																   WHERE t.KOD_MEAFYEN IN( SELECT x FROM TABLE(CAST(Convert_String_To_Table(whithOutList ,  ',') AS mytabtype)) ))
					ORDER BY 	SM.KOD_SIDUR_MEYUCHAD;

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
   	--  	   		   		  		 and   p2.ad_taarich+1=p3.me_taarich
	--							 and  p2.mispar_sidur= p3.mispar_sidur);
	)
	ORDER BY me_tarich;


CURSOR Me_Ad_tmp(par_sidur NUMBER) IS
SELECT me_tarich,NVL(ad_tarich,TO_DATE('31/12/4712','dd/mm/yyyy')) ad_tarich
FROM    PIVOT_SIDURIM_MEYUCHADIM
WHERE mispar_sidur=par_sidur;

CURSOR sidurim_meyuchadim_tmp(par_sidur NUMBER,p_Dt_from DATE, p_Dt_to DATE) IS
SELECT
  mispar_sidur,
  teur, Sug_sidur	 ,sector_avoda	 ,sug_premia, mofia_bdoch_nochachut_minhal_u, headrut_hova_ledaveach_shaot,
  shat_hatchala_muteret,
  shat_gmar_muteret,
  hukiyut_beshabaton ,hokiyut_beshishi ,
  sug_hashaot_beyom_shishi,	 sug_hashaot_beyom_shabaton, hova_ledaveach_peilut,
  zakay_leaman_nesia,zakay_lezman_halbasha,  max_shaot_byom_hol	 ,
  sug_shaot_byom_hol_if_migbala,	 max_shaot_byom_shishi	 ,max_shaot_beshabaton	 ,
  asur_2_peiluyot_meoto_sug,hashlama_lemin_shaot_imahu,
  mutar_lebitzua_rak_lemeafyen_x, mutar_lebitzua_rak_lerishyon,avar_hadracha_meyuchedet	,
  zakay_michutz_lamichsa,  zakay_lepizul 	 ,
  nitan_ledaveach_ad_taarich,
  kod_tipul_meyuchad_Baklita,	 kod_tipul_meyuchad_Bashguim,	 kod_tipul_meyuchad_Bachishuv,
  asur_ledivuach_lechevrot_bat	, zakay_lehamara		 ,kod_headrut		 ,zakay_lelina	 ,
  zakay_lechariga,
  hova_ledaveach_mispar_machsan,hova_ledaveach_mispar_musach,
  hova_yom_visa_namlak ,
  asur_divuach_simun_tachograf,	 zakay_lehashlama_avur_sidur ,zakay_lechishuv_retzifut	 ,
  metagber_benihul_tnua,asur_ledaveach_mispar_rechev,
  zarich_ledaveach_km,sidur_namlak_visa,sidur_asur_ledaveach_peilut,
  avodat_musach	,zakaut_lepizul_nehagut	,zakaut_legmul_chisachon,
  sug_avoda,sidur_misug_headrut, shaon_nochachut,ein_leshalem_tos_lila,max_dakot_boded,
   dakot_n_letashlum_hol,sidur_kaytanot_veruey_kayiz,sidur_lo_nivdak_sofash,
  lo_letashlum_automati,kizuz_al_pi_hatchala_gmar,Michsat_shaot_chodshit,
       zakaut_lenosafot	,   lo_pogea_bemichsat_nosafot,   rechiv_lezvira_yomit,
   rechiv_lezvira_chodshit,   max_dakot_nosafot_bechodesh,   zakaut_tamritz_nosafot_minhal,
     michsat_shishi_lebaley_x,  shabaton_tashlum_kavua,	   max_yemey_avoda_bechodesh,
	 kod_ishur_WF	,	   sidur_mishtatel_bechafifa,  tashlum_kavua_beshishi,	   lo_zakay_legmul_beshishi,
	  lo_zakay_legmul_beshabaton,		   sug_shaot_nosafot	,  musachey_mishmeret2,
	  asur_leshanot_zman_peilut,   lo_nidreshet_hityazvut,   zakaut_lerezifut_benahagut,
	  zakaut_letamriz_nos_nehagut,	   tokef_hatchala	,	   tokef_siyum		,   hovat_hityazvut	,   
	  lidrosh_kod_mivtza ,mofia_bedoch_noch_prem	,avodat_meshek,
	  tashlum_kavua_bchol	,shat_hatchala_auto,shat_gmar_auto, sidur_lo_chosem,realy_veod_yom,sidur_rak_lechevrot_banot ,
	  element1_hova	,	  element2_hova	,   element3_hova	,   bdikat_hityazvut_letachanat_s,     
	  nitan_ledaveach_bmachala_aruc, nizbar_yomit_lehalbasha_nesio,rashai_ledaveach,heara_letiud,
      matala_klalit_lelo_rechev
FROM (
  SELECT
    mispar_sidur,
    MAX(CASE WHEN kod_meafyen=1 THEN NVL(erech,'-1')   ELSE '' END)    teur,
    MAX(CASE WHEN  kod_meafyen=2 THEN NVL(erech,'-1')   ELSE '' END)   Sug_sidur	 ,
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
    MAX(CASE WHEN  kod_meafyen=14 THEN NVL(erech,'-1')   ELSE '' END) zakay_leaman_nesia	 ,
    MAX(CASE WHEN  kod_meafyen=15  THEN NVL(erech,'-1')   ELSE '' END) zakay_lezman_halbasha,
    MAX(CASE WHEN  kod_meafyen=16 THEN NVL(erech,'-1')   ELSE '' END)  max_shaot_byom_hol	 ,
    MAX(CASE WHEN  kod_meafyen=17 THEN NVL(erech,'-1')   ELSE '' END)  sug_shaot_byom_hol_if_migbala,
    MAX(CASE WHEN  kod_meafyen=18 THEN NVL(erech,'-1')   ELSE '' END)  max_shaot_byom_shishi	 ,
    MAX(CASE WHEN  kod_meafyen=19 THEN NVL(erech,'-1')   ELSE '' END)  max_shaot_beshabaton	 ,
    MAX(CASE WHEN  kod_meafyen=20 THEN NVL(erech,'-1')   ELSE '' END)  asur_2_peiluyot_meoto_sug,
    MAX(CASE WHEN  kod_meafyen=21 THEN NVL(erech,'-1')   ELSE '' END)  hashlama_lemin_shaot_imahu,
    MAX(CASE WHEN  kod_meafyen=22 THEN NVL(erech,'-1')   ELSE '' END)  mutar_lebitzua_rak_lemeafyen_x,
    MAX(CASE WHEN  kod_meafyen=23 THEN NVL(erech,'-1')   ELSE '' END)  mutar_lebitzua_rak_lerishyon,
    MAX(CASE WHEN  kod_meafyen=24 THEN NVL(erech,'-1')   ELSE '' END)  avar_hadracha_meyuchedet	,
    MAX(CASE WHEN  kod_meafyen=25 THEN NVL(erech,'-1')   ELSE '' END)  zakay_michutz_lamichsa,
    MAX(CASE WHEN  kod_meafyen=26 THEN NVL(erech,'-1')   ELSE '' END)  zakay_lepizul 	 ,
    MAX(CASE WHEN  kod_meafyen=27 THEN NVL(erech,'-1')   ELSE '' END)  nitan_ledaveach_ad_taarich,
    MAX(CASE WHEN  kod_meafyen=28 THEN NVL(erech,'-1')   ELSE '' END)  kod_tipul_meyuchad_Baklita,
    MAX(CASE WHEN  kod_meafyen=29 THEN NVL(erech,'-1')   ELSE '' END)  kod_tipul_meyuchad_Bashguim,
    MAX(CASE WHEN  kod_meafyen=30 THEN NVL(erech,'-1')   ELSE '' END)  kod_tipul_meyuchad_Bachishuv,
    MAX(CASE WHEN  kod_meafyen=31 THEN NVL(erech,'-1')   ELSE '' END)  asur_ledivuach_lechevrot_bat	,
    MAX(CASE WHEN  kod_meafyen=32 THEN NVL(erech,'-1')   ELSE '' END)  zakay_lehamara		 ,
    MAX(CASE WHEN  kod_meafyen=33 THEN NVL(erech,'-1')   ELSE '' END)  kod_headrut		 ,
    MAX(CASE WHEN  kod_meafyen=34 THEN NVL(erech,'-1')   ELSE '' END)  zakay_lelina	 ,
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
	MAX(CASE WHEN  kod_meafyen=47 THEN NVL(erech,'-1')   ELSE '' END)  avodat_musach	,
	MAX(CASE WHEN  kod_meafyen=48 THEN NVL(erech,'-1')   ELSE '' END)  zakaut_lepizul_nehagut	,
	MAX(CASE WHEN  kod_meafyen=49 THEN NVL(erech,'-1')   ELSE '' END)  zakaut_legmul_chisachon,
	MAX(CASE WHEN  kod_meafyen=50 THEN NVL(erech,'-1')   ELSE '' END)      zakaut_lenosafot	,
    MAX(CASE WHEN  kod_meafyen=51 THEN NVL(erech,'-1')   ELSE '' END)	  lo_pogea_bemichsat_nosafot,
    MAX(CASE WHEN  kod_meafyen=52 THEN NVL(erech,'-1')   ELSE '' END)  sug_avoda,
    MAX(CASE WHEN  kod_meafyen=53 THEN NVL(erech,'-1')   ELSE '' END)  sidur_misug_headrut,
    MAX(CASE WHEN  kod_meafyen=54 THEN NVL(erech,'-1')   ELSE '' END)  shaon_nochachut,
    MAX(CASE WHEN  kod_meafyen=55 THEN NVL(erech,'-1')   ELSE '' END)	   rechiv_lezvira_yomit,
    MAX(CASE WHEN  kod_meafyen=56 THEN NVL(erech,'-1')   ELSE '' END)	   rechiv_lezvira_chodshit,
	MAX(CASE WHEN  kod_meafyen=57 THEN NVL(erech,'-1')   ELSE '' END)  Michsat_shaot_chodshit,
    MAX(CASE WHEN  kod_meafyen=58 THEN NVL(erech,'-1')   ELSE '' END)	   max_dakot_nosafot_bechodesh,
    MAX(CASE WHEN  kod_meafyen=59 THEN NVL(erech,'-1')   ELSE '' END)	   zakaut_tamritz_nosafot_minhal,
    MAX(CASE WHEN  kod_meafyen=60 THEN NVL(erech,'-1')   ELSE '' END)  max_dakot_boded,
    MAX(CASE WHEN  kod_meafyen=61 THEN NVL(erech,'-1')   ELSE '' END)  ein_leshalem_tos_lila,
	MAX(CASE WHEN  kod_meafyen=62 THEN NVL(erech,'-1')   ELSE '' END)  dakot_n_letashlum_hol,
    MAX(CASE WHEN  kod_meafyen=63 THEN NVL(erech,'-1')   ELSE '' END)	   michsat_shishi_lebaley_x,
    MAX(CASE WHEN  kod_meafyen=64 THEN NVL(erech,'-1')   ELSE '' END)       shabaton_tashlum_kavua,
    MAX(CASE WHEN  kod_meafyen=65 THEN NVL(erech,'-1')   ELSE '' END)  	   max_yemey_avoda_bechodesh,
    MAX(CASE WHEN  kod_meafyen=66 THEN NVL(erech,'-1')   ELSE '' END)  	   kod_ishur_WF	,
    MAX(CASE WHEN  kod_meafyen=67 THEN NVL(erech,'-1')   ELSE '' END)  	   sidur_mishtatel_bechafifa,
    MAX(CASE WHEN  kod_meafyen=68 THEN NVL(erech,'-1')   ELSE '' END)  	   tashlum_kavua_beshishi,
    MAX(CASE WHEN  kod_meafyen=69 THEN NVL(erech,'-1')   ELSE '' END)  	   lo_zakay_legmul_beshishi,
    MAX(CASE WHEN  kod_meafyen=70 THEN NVL(erech,'-1')   ELSE '' END)  	   lo_zakay_legmul_beshabaton,
    MAX(CASE WHEN  kod_meafyen=71 THEN NVL(erech,'-1')   ELSE '' END)  	   sug_shaot_nosafot	,
	MAX(CASE WHEN  kod_meafyen=4 THEN NVL(erech,'-1')   ELSE '' END)        sidur_lo_nivdak_sofash,
    MAX(CASE WHEN  kod_meafyen=73 THEN NVL(erech,'-1')   ELSE '' END)      sidur_kaytanot_veruey_kayiz,
    MAX(CASE WHEN  kod_meafyen=74 THEN NVL(erech,'-1')   ELSE '' END)  	   musachey_mishmeret2,
    MAX(CASE WHEN  kod_meafyen=75 THEN NVL(erech,'-1')   ELSE '' END)	   asur_leshanot_zman_peilut,
    MAX(CASE WHEN  kod_meafyen=76 THEN NVL(erech,'-1')   ELSE '' END)	   lo_nidreshet_hityazvut,
    MAX(CASE WHEN  kod_meafyen=77 THEN NVL(erech,'-1')   ELSE '' END)	   zakaut_lerezifut_benahagut,
	MAX(CASE WHEN  kod_meafyen=78 THEN NVL(erech,'-1')   ELSE '' END)      kizuz_al_pi_hatchala_gmar,
	MAX(CASE WHEN  kod_meafyen=79 THEN NVL(erech,'-1')   ELSE '' END)      lo_letashlum_automati,
    MAX(CASE WHEN  kod_meafyen=80 THEN NVL(erech,'-1')   ELSE '' END)	   zakaut_letamriz_nos_nehagut,
	MAX(CASE WHEN  kod_meafyen=81 THEN erech  ELSE  NULL  END)   tokef_hatchala	 ,
	MAX(CASE WHEN  kod_meafyen=82 THEN erech  ELSE  NULL  END)   tokef_siyum	 ,
    MAX(CASE WHEN  kod_meafyen=83 THEN NVL(erech,'-1')   ELSE '' END)	   hovat_hityazvut	,
	MAX(CASE WHEN  kod_meafyen=84 THEN NVL(erech,'-1')   ELSE '' END)	   lidrosh_kod_mivtza,
	MAX(CASE WHEN  kod_meafyen=85 THEN NVL(erech,'-1')   ELSE '' END)      mofia_bedoch_noch_prem	,
	MAX(CASE WHEN  kod_meafyen=86 THEN NVL(erech,'-1')   ELSE '' END)      avodat_meshek,
    MAX(CASE WHEN  kod_meafyen=87 THEN NVL(erech,'-1')   ELSE '' END) 	   tashlum_kavua_bchol	,
    MAX(CASE WHEN  kod_meafyen=88 THEN NVL(erech,'-1')   ELSE '' END) 	   shat_hatchala_auto,
    MAX(CASE WHEN  kod_meafyen=89 THEN NVL(erech,'-1')   ELSE '' END)      shat_gmar_auto,
	MAX(CASE WHEN  kod_meafyen=90 THEN NVL(erech,'-1')   ELSE '' END)      sidur_lo_chosem,
	MAX(CASE WHEN  kod_meafyen=91 THEN NVL(erech,'-1')   ELSE '' END)      realy_veod_yom,
    MAX(CASE WHEN  kod_meafyen=92 THEN NVL(erech,'-1')   ELSE '' END)      sidur_rak_lechevrot_banot,
    MAX(CASE WHEN  kod_meafyen=93 THEN NVL(erech,'-1')   ELSE '' END)	   element1_hova	,
    MAX(CASE WHEN  kod_meafyen=94 THEN NVL(erech,'-1')   ELSE '' END)	   element2_hova	,
    MAX(CASE WHEN  kod_meafyen=95 THEN NVL(erech,'-1')   ELSE '' END)	   element3_hova	,
    MAX(CASE WHEN  kod_meafyen=96 THEN NVL(erech,'-1')   ELSE '' END)	   bdikat_hityazvut_letachanat_s,
	MAX(CASE WHEN  kod_meafyen=97 THEN NVL(erech,'-1')   ELSE '' END)	   nitan_ledaveach_bmachala_aruc,
	MAX(CASE WHEN  kod_meafyen=98 THEN NVL(erech,'-1')   ELSE '' END)	   nizbar_yomit_lehalbasha_nesio,
	MAX(CASE WHEN  kod_meafyen=99 THEN NVL(erech,'-1')   ELSE '' END)	   rashai_ledaveach,
    MAX(CASE WHEN  kod_meafyen=100 THEN NVL(erech,'-1')   ELSE '' END)	   heara_letiud,
    MAX(CASE WHEN  kod_meafyen=101 THEN NVL(erech,'-1')   ELSE '' END)      matala_klalit_lelo_rechev

  FROM
    TB_SIDURIM_MEYUCHADIM
	WHERE mispar_sidur =par_sidur
			AND ((p_Dt_from <= me_taarich  AND  p_Dt_to >= me_taarich  AND p_Dt_to <= NVL(ad_taarich,TO_DATE('31/12/4712','dd/mm/yyyy')))
	 OR  (p_Dt_from <= me_taarich  AND p_Dt_to >= NVL(ad_taarich,TO_DATE('31/12/4712','dd/mm/yyyy')))
     OR (p_Dt_from >= me_taarich  AND  p_Dt_from <= NVL(ad_taarich,TO_DATE('31/12/4712','dd/mm/yyyy')) AND p_Dt_to >= NVL(ad_taarich,TO_DATE('31/12/4712','dd/mm/yyyy')))
	 OR (p_Dt_from >= me_taarich   AND  p_Dt_to <= NVL(ad_taarich,TO_DATE('31/12/4712','dd/mm/yyyy'))))
--	and ((to_date(p_Dt_from,'dd/mm/yyyy') <= me_taarich  and to_date(p_Dt_to,'dd/mm/yyyy') >= me_taarich  and to_date(p_Dt_to,'dd/mm/yyyy') <= nvl(ad_taarich,to_date('31/12/4712','dd/mm/yyyy')))
--	 or  (to_date(p_Dt_from,'dd/mm/yyyy') <= me_taarich  and to_date(p_Dt_to,'dd/mm/yyyy') >= nvl(ad_taarich,to_date('31/12/4712','dd/mm/yyyy')))
 --    or (to_date(p_Dt_from,'dd/mm/yyyy') >= me_taarich  and to_date(p_Dt_from,'dd/mm/yyyy') <= nvl(ad_taarich,to_date('31/12/4712','dd/mm/yyyy')) and to_date(p_Dt_to,'dd/mm/yyyy') >= nvl(ad_taarich,to_date('31/12/4712','dd/mm/yyyy')))
--	 or (to_date(p_Dt_from,'dd/mm/yyyy') >= me_taarich   and to_date(p_Dt_to,'dd/mm/yyyy') <= nvl(ad_taarich,to_date('31/12/4712','dd/mm/yyyy'))))
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
 	  mutar_lebitzua_rak_lmeafyen_x	=sidurim_meyuchadim_tmp_rec.mutar_lebitzua_rak_lemeafyen_x,
      mutar_lebitzua_rak_lerishyon=sidurim_meyuchadim_tmp_rec.mutar_lebitzua_rak_lerishyon,
      zakay_michutz_lamichsa=sidurim_meyuchadim_tmp_rec.zakay_michutz_lamichsa,
	  nitan_ledaveach_ad_taarich=sidurim_meyuchadim_tmp_rec.nitan_ledaveach_ad_taarich,
	  zakay_lechariga=sidurim_meyuchadim_tmp_rec.zakay_lechariga,
	  hova_ledaveach_mispar_machsan=sidurim_meyuchadim_tmp_rec.hova_ledaveach_mispar_machsan,
	  hova_ledaveach_mispar_musach=sidurim_meyuchadim_tmp_rec.hova_ledaveach_mispar_musach,
	  hova_yom_visa_namlak=sidurim_meyuchadim_tmp_rec.hova_yom_visa_namlak,
      Sug_sidur	=sidurim_meyuchadim_tmp_rec.sug_sidur,
      sector_avoda	=sidurim_meyuchadim_tmp_rec.sector_avoda,
      headrut_hova_ledaveach_shaot=sidurim_meyuchadim_tmp_rec.headrut_hova_ledaveach_shaot,
      mofia_bdoch_nochachut_minhal_u=sidurim_meyuchadim_tmp_rec.mofia_bdoch_nochachut_minhal_u,
      hukiyut_beshabaton=sidurim_meyuchadim_tmp_rec.hukiyut_beshabaton,
      hokiyut_beshishi=sidurim_meyuchadim_tmp_rec.hokiyut_beshishi,
      sug_hashaot_beyom_shishi	=sidurim_meyuchadim_tmp_rec.sug_hashaot_beyom_shishi	,
      sug_hashaot_beyom_shabaton =sidurim_meyuchadim_tmp_rec.sug_hashaot_beyom_shabaton ,
      zakay_leaman_nesia	=sidurim_meyuchadim_tmp_rec.zakay_leaman_nesia,
      max_shaot_byom_hol	=sidurim_meyuchadim_tmp_rec.max_shaot_byom_hol,
      sug_shaot_byom_hol_if_migbala	=sidurim_meyuchadim_tmp_rec.sug_shaot_byom_hol_if_migbala	,
      max_shaot_byom_shishi	=sidurim_meyuchadim_tmp_rec.max_shaot_byom_shishi	,
      max_shaot_beshabaton	=sidurim_meyuchadim_tmp_rec.max_shaot_beshabaton	,
      hashlama_lemin_shaot_imahu	=sidurim_meyuchadim_tmp_rec.hashlama_lemin_shaot_imahu,
      avar_hadracha_meyuchedet	=sidurim_meyuchadim_tmp_rec.avar_hadracha_meyuchedet	,
      zakay_lepizul 	=sidurim_meyuchadim_tmp_rec.zakay_lepizul ,
      kod_tipul_meyuchad_Baklita	=sidurim_meyuchadim_tmp_rec.kod_tipul_meyuchad_Baklita,
      kod_tipul_meyuchad_Bashguim	=sidurim_meyuchadim_tmp_rec.kod_tipul_meyuchad_Bashguim	,
      kod_tipul_meyuchad_Bachishuv	=sidurim_meyuchadim_tmp_rec.kod_tipul_meyuchad_Bachishuv,
      asur_ledivuach_lechevrot_bat	=sidurim_meyuchadim_tmp_rec.asur_ledivuach_lechevrot_bat,
      zakay_lehamara		=sidurim_meyuchadim_tmp_rec.zakay_lehamara,
      kod_headrut		=sidurim_meyuchadim_tmp_rec.kod_headrut	,
      zakay_lelina	=sidurim_meyuchadim_tmp_rec.zakay_lelina,
      asur_divuach_simun_tachograf	=sidurim_meyuchadim_tmp_rec.asur_divuach_simun_tachograf,
      zakay_lehashlama_avur_sidur=sidurim_meyuchadim_tmp_rec.zakay_lehashlama_avur_sidur,
      zakay_lechishuv_retzifut	=sidurim_meyuchadim_tmp_rec.zakay_lechishuv_retzifut,
	  metagber_benihul_tnua=sidurim_meyuchadim_tmp_rec.metagber_benihul_tnua,
	  zarich_ledaveach_km=sidurim_meyuchadim_tmp_rec.zarich_ledaveach_km,
      asur_ledaveach_mispar_rechev =sidurim_meyuchadim_tmp_rec.asur_ledaveach_mispar_rechev,
      sidur_namlak_visa  =sidurim_meyuchadim_tmp_rec.sidur_namlak_visa,
      sidur_asur_ledaveach_peilut=sidurim_meyuchadim_tmp_rec.sidur_asur_ledaveach_peilut,
	  avodat_musach	=sidurim_meyuchadim_tmp_rec.avodat_musach,
	  zakaut_lepizul_nehagut	=sidurim_meyuchadim_tmp_rec.zakaut_lepizul_nehagut,
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
      zakaut_lenosafot	=sidurim_meyuchadim_tmp_rec.zakaut_lenosafot,
	  lo_pogea_bemichsat_nosafot	=sidurim_meyuchadim_tmp_rec.lo_pogea_bemichsat_nosafot,
	   rechiv_lezvira_yomit	=sidurim_meyuchadim_tmp_rec.rechiv_lezvira_yomit,
	   rechiv_lezvira_chodshit	=sidurim_meyuchadim_tmp_rec.rechiv_lezvira_chodshit	,
	   max_dakot_nosafot_bechodesh	=sidurim_meyuchadim_tmp_rec.max_dakot_nosafot_bechodesh,
	   zakaut_tamritz_nosafot_minhal=sidurim_meyuchadim_tmp_rec.zakaut_tamritz_nosafot_minhal,
	   michsat_shishi_lebaley_x=sidurim_meyuchadim_tmp_rec.michsat_shishi_lebaley_x,
       shabaton_tashlum_kavua	=sidurim_meyuchadim_tmp_rec.shabaton_tashlum_kavua,
  	   max_yemey_avoda_bechodesh	=sidurim_meyuchadim_tmp_rec.max_yemey_avoda_bechodesh,
  	   kod_ishur_WF=sidurim_meyuchadim_tmp_rec. kod_ishur_WF,
  	   sidur_mishtatel_bechafifa=sidurim_meyuchadim_tmp_rec.sidur_mishtatel_bechafifa,
  	   tashlum_kavua_beshishi	=sidurim_meyuchadim_tmp_rec. tashlum_kavua_beshishi,
  	   lo_zakay_legmul_beshishi=sidurim_meyuchadim_tmp_rec.lo_zakay_legmul_beshishi,
  	   lo_zakay_legmul_beshabaton	=sidurim_meyuchadim_tmp_rec. lo_zakay_legmul_beshabaton	,
  	   sug_shaot_nosafot	=sidurim_meyuchadim_tmp_rec.sug_shaot_nosafot,
  	   musachey_mishmeret2	=sidurim_meyuchadim_tmp_rec.musachey_mishmeret2,
	   asur_leshanot_zman_peilut	=sidurim_meyuchadim_tmp_rec. asur_leshanot_zman_peilut	,
	   lo_nidreshet_hityazvut	=sidurim_meyuchadim_tmp_rec.lo_nidreshet_hityazvut	,
	   zakaut_lerezifut_benahagut=sidurim_meyuchadim_tmp_rec.zakaut_lerezifut_benahagut,
	   zakaut_letamriz_nos_nehagut=sidurim_meyuchadim_tmp_rec.zakaut_letamriz_nos_nehagut,
	   hovat_hityazvut	=sidurim_meyuchadim_tmp_rec.hovat_hityazvut	,
	   lidrosh_kod_mivtza    =sidurim_meyuchadim_tmp_rec.lidrosh_kod_mivtza,
	   mofia_bedoch_noch_prem	   =sidurim_meyuchadim_tmp_rec.mofia_bedoch_noch_prem	,
	   avodat_meshek	=sidurim_meyuchadim_tmp_rec.avodat_meshek,
	   tashlum_kavua_bchol	=sidurim_meyuchadim_tmp_rec.tashlum_kavua_bchol,
	   shat_hatchala_auto  =sidurim_meyuchadim_tmp_rec.shat_hatchala_auto,
	   shat_gmar_auto   =sidurim_meyuchadim_tmp_rec.shat_gmar_auto ,
	   sidur_lo_chosem = sidurim_meyuchadim_tmp_rec.sidur_lo_chosem,
	   realy_veod_yom  = sidurim_meyuchadim_tmp_rec.realy_veod_yom,
       sidur_rak_lechevrot_banot  =sidurim_meyuchadim_tmp_rec.sidur_rak_lechevrot_banot ,
	   element1_hova	=sidurim_meyuchadim_tmp_rec.element1_hova,
	   element2_hova	=sidurim_meyuchadim_tmp_rec.element2_hova,
	   element3_hova	=sidurim_meyuchadim_tmp_rec.element3_hova	,
	   bdikat_hityazvut_letachanat_s=sidurim_meyuchadim_tmp_rec.bdikat_hityazvut_letachanat_s,
	   nitan_ledaveach_bmachala_aruc=  sidurim_meyuchadim_tmp_rec.nitan_ledaveach_bmachala_aruc,
	   nizbar_yomit_lehalbasha_nesio     =  sidurim_meyuchadim_tmp_rec.nizbar_yomit_lehalbasha_nesio,
	   tokef_hatchala= TO_DATE(sidurim_meyuchadim_tmp_rec.tokef_hatchala,'dd/mm/yyyy'),
	   tokef_siyum	= TO_DATE(sidurim_meyuchadim_tmp_rec.tokef_siyum,'dd/mm/yyyy'),
	   rashai_ledaveach =  sidurim_meyuchadim_tmp_rec.rashai_ledaveach,
	   heara_letiud		=sidurim_meyuchadim_tmp_rec.heara_letiud ,
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

PROCEDURE	pro_get_sug_hazmanat_viza( p_Cur OUT CurType) IS
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

PROCEDURE	pro_get_kod_mivza_viza( p_Cur OUT CurType) IS
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

PROCEDURE	pro_get_lina( p_Cur OUT CurType) IS
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
   	--  	   		   		  		 and   p2.ad_taarich+1=p3.me_taarich
	--							 and  p2.mispar_sidur= p3.mispar_sidur);
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

END Pkg_Sidurim;
/
