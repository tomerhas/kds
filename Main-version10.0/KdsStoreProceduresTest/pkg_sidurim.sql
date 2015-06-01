
  CREATE OR REPLACE PACKAGE BODY "KDSADMIN"."PKG_SIDURIM" AS

  PROCEDURE Get_Sidurim_Meyuchadim(p_Kod IN VARCHAR2,p_Period in VARCHAR2,p_Cur OUT CurType) IS
  v_tar_me DATE ; 
  BEGIN
  	v_tar_me:=to_date('01/' || p_Period,'dd/mm/yyyy'); 
          OPEN p_Cur FOR
          SELECT tb.MISPAR_SIDUR, ctb.Kod_Meafyen,(ctb.Kod_Meafyen||','|| ctb.shem_meafyen || '(' || ctb.SUG_NATUN || ')')DetailsMeafyen ,tb.Me_Taarich,tb.Ad_Taarich,Erech, ctb.sug_natun,tb.taarich_idkun_acharon LastUpdate
          FROM TB_SIDURIM_MEYUCHADIM tb      INNER JOIN ctb_meafyeney_sidurim ctb 
          ON ctb.kod_meafyen= tb.kod_meafyen
          WHERE tb.Me_Taarich <v_tar_me  AND tb.MISPAR_SIDUR = p_Kod AND ctb.PAIL = 1 
          ORDER BY   ctb.Kod_Meafyen ;
    EXCEPTION
        WHEN OTHERS THEN
            RAISE;      
  END Get_Sidurim_Meyuchadim;

  PROCEDURE GetMatching_Sidur_Desc(p_Prefix in VARCHAR2,  p_Cur OUT CurType) AS
  BEGIN
  OPEN p_Cur for 
      SELECT TEUR_SIDUR_MEYCHAD from CTB_SIDURIM_MEYUCHADIM SM 
      WHERE SM.TEUR_SIDUR_MEYCHAD   LIKE  p_Prefix || '%' and pail =1 ;
    NULL;
  END GetMatching_Sidur_Desc;
  
  PROCEDURE Get_AllKodOfSidur(  p_Cur OUT CurType) AS
  BEGIN
  OPEN p_Cur for 
      SELECT KOD_SIDUR_MEYUCHAD MISPAR_SIDUR
      FROM CTB_SIDURIM_MEYUCHADIM SM 
      WHERE PAIL = 1 ;

    END Get_AllKodOfSidur;

  PROCEDURE GetMatching_Sidur_Kod(p_Prefix in VARCHAR2,  p_Cur OUT CurType) AS
  BEGIN
  OPEN p_Cur for 
      SELECT KOD_SIDUR_MEYUCHAD 
      FROM CTB_SIDURIM_MEYUCHADIM SM 
      WHERE SM.KOD_SIDUR_MEYUCHAD   LIKE  p_Prefix || '%' and pail =1 
      ORDER BY KOD_SIDUR_MEYUCHAD ASC ;
    END GetMatching_Sidur_Kod;

PROCEDURE Get_History_Of_Sidur(p_Kod in VARCHAR2, p_Cur out Curtype) as 
BEGIN 
  OPEN p_Cur for 
      SELECT SM.ME_TAARICH,SM.ad_taarich,SM.erech   
      FROM TB_MEAFYENEY_SUG_SIDUR SM 
      WHERE SM.kod_meafyen =p_Kod
      ORDER BY SM.ME_TAARICH DESC ; 
      
END Get_History_Of_Sidur;

PROCEDURE Get_SidurDesc_by_Kod(p_Kod in VARCHAR2,p_Desc out VARCHAR2) AS
  BEGIN
      SELECT  SM.teur_sidur_meychad INTO p_desc  
      FROM CTB_SIDURIM_MEYUCHADIM SM 
      WHERE SM.KOD_SIDUR_MEYUCHAD = p_Kod;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
        p_Desc := '';
    NULL;
  END Get_SidurDesc_by_Kod;

  PROCEDURE Get_Kod_By_SidurDesc(p_Desc in CTB_SIDURIM_MEYUCHADIM.TEUR_SIDUR_MEYCHAD%type  ,p_Kod out CTB_SIDURIM_MEYUCHADIM.KOD_SIDUR_MEYUCHAD%type) AS
  BEGIN
      SELECT  SM.KOD_SIDUR_MEYUCHAD INTO p_Kod  
      from CTB_SIDURIM_MEYUCHADIM SM 
      WHERE SM.teur_sidur_meychad = p_desc;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
        p_Kod := '';
    NULL;
  END Get_Kod_By_SidurDesc;

PROCEDURE Get_DetailsKodOfSidur(p_Cur OUT CurType ) as 
BEGIN
  OPEN p_Cur for 
      SELECT  SM.KOD_MEAFYEN Kod_Meafyen , (SM.KOD_MEAFYEN  ||',' || SM.SHEM_MEAFYEN || '(' || sm.SUG_NATUN || ')' ) DetailsMeafyen
      FROM ctb_meafyeney_sidurim SM 
      WHERE SM.PAIL = 1 ;
END Get_DetailsKodOfSidur;

  PROCEDURE Update_Sidurim_Meyuchadim(p_KodMeafyen number,p_MisparSidur number,p_MeTaarich date,p_AdTaarich date,p_Erech VARCHAR2 ,p_SugNatun VARCHAR2, p_taarich_idkun_acharon DATE,p_meadken_acharon NUMBER) AS
  BEGIN
    update TB_SIDURIM_MEYUCHADIM SM
    set SM.ERECH = p_Erech ,
        SM.AD_TAARICH = p_AdTaarich , 
          SM.TAARICH_IDKUN_ACHARON = p_taarich_idkun_acharon,
          SM.MEADKEN_ACHARON = p_meadken_acharon
    where MISPAR_SIDUR = p_MisparSidur AND 
    ME_TAARICH = p_MeTaarich AND 
    KOD_MEAFYEN = p_KodMeafyen;
  END Update_Sidurim_Meyuchadim;

PROCEDURE Insert_Sidurim_Meyuchadim(p_KodMeafyen number,p_MisparSidur number,p_MeTaarich date,p_AdTaarich date,p_Erech VARCHAR2 ,p_SugNatun VARCHAR2, p_taarich_idkun_acharon DATE,p_meadken_acharon NUMBER) AS
  BEGIN
  insert into TB_SIDURIM_MEYUCHADIM
    (ad_taarich, erech, kod_meafyen, me_taarich, meadken_acharon, mispar_sidur) 
    VALUES (p_adtaarich, p_erech, p_kodmeafyen, p_metaarich, p_meadken_acharon, p_misparsidur);
  END Insert_Sidurim_Meyuchadim;
  
  PROCEDURE Get_Meafyeney_Sug_Sidur(p_Kod IN VARCHAR2,p_Period in VARCHAR2,p_Cur OUT CurType) IS
  v_tar_me DATE ; 
  BEGIN
    v_tar_me:=to_date('01/' || p_Period,'dd/mm/yyyy'); 
       OPEN p_Cur FOR
        SELECT tb.SUG_SIDUR , ctb.Kod_Meafyen,(ctb.Kod_Meafyen||','|| ctb.shem_meafyen  || '(' || ctb.SUG_NATUN || ')') DetailsMeafyen , tb.me_taarich, tb.ad_taarich, tb.erech, ctb.sug_natun,tb.Taarich_Idkun_Acharon  LastUpdate
      FROM TB_Meafyeney_Sug_Sidur tb
      INNER JOIN ctb_meafyeney_sidurim ctb 
      ON ctb.kod_meafyen= tb.kod_meafyen
        WHERE tb.Me_Taarich <v_tar_me  AND tb.SUG_SIDUR = p_Kod AND ctb.PAIL = 1 
          ORDER BY   ctb.Kod_Meafyen ;

    EXCEPTION
        WHEN OTHERS THEN
            RAISE;    
    NULL;
  END Get_Meafyeney_Sug_Sidur;
   
   PROCEDURE GetMatching_TypeSidur_Desc(p_Prefix in VARCHAR2,  p_Cur OUT CurType) AS
  BEGIN
  OPEN p_Cur for 
      SELECT SHEM_MEAFYEN from CTB_MEAFYENEY_SIDURIM MS 
      WHERE MS.SHEM_MEAFYEN   LIKE  p_Prefix || '%' and pail =1 ; 
    NULL;
  END GetMatching_TypeSidur_Desc;
      
    PROCEDURE Get_AllKodOfTypeSidur(  p_Cur OUT CurType) AS
  BEGIN
  OPEN p_Cur for 
      SELECT KOD_MEAFYEN SUG_SIDUR
      FROM CTB_MEAFYENEY_SIDURIM 
      WHERE PAIL = 1 ;
    END Get_AllKodOfTypeSidur;
  
  PROCEDURE GetMatching_TypeSidur_Kod(p_Prefix in VARCHAR2,  p_Cur OUT CurType) AS
  BEGIN
      OPEN p_Cur for 
          SELECT KOD_MEAFYEN 
          FROM CTB_MEAFYENEY_SIDURIM MS
          WHERE MS.KOD_MEAFYEN   LIKE  p_Prefix || '%' and pail =1 
          ORDER BY KOD_MEAFYEN ASC; 
  END GetMatching_TypeSidur_Kod;
     
  PROCEDURE Get_DetailsKodOfTypeSidur(p_Cur OUT CurType ) as 
  BEGIN
      OPEN p_Cur for 
      SELECT  SM.KOD_MEAFYEN Kod_Meafyen , (SM.KOD_MEAFYEN  ||',' || SM.SHEM_MEAFYEN || '(' || sm.SUG_NATUN || ')' ) DetailsMeafyen 
      FROM CTB_MEAFYENEY_SIDURIM SM 
      WHERE SM.PAIL = 1 ;
  END Get_DetailsKodOfTypeSidur;
  
  PROCEDURE Get_TypeSidurDesc_by_Kod(p_Kod in VARCHAR2,p_Desc out VARCHAR2) AS
  BEGIN
      SELECT  SM.SHEM_MEAFYEN INTO p_desc  
      FROM CTB_MEAFYENEY_SIDURIM SM 
      WHERE SM.KOD_MEAFYEN = p_Kod;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
        p_Desc := '';
    NULL;
  END Get_TypeSidurDesc_by_Kod;
  
  PROCEDURE Get_Kod_By_TypeSidurDesc(p_Desc in VARCHAR2,p_Kod out VARCHAR2) AS
  BEGIN
      SELECT  SM.KOD_MEAFYEN INTO p_Kod  
      from CTB_MEAFYENEY_SIDURIM SM 
      WHERE SM.SHEM_MEAFYEN = p_desc;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
        p_Kod := '';
  END Get_Kod_By_TypeSidurDesc;
  
PROCEDURE Get_History_Of_TypeSidur(p_Kod in VARCHAR2, p_Cur out Curtype)
AS
  BEGIN
  OPEN p_Cur for 
      SELECT SM.ME_TAARICH,SM.ad_taarich,SM.erech   
      FROM TB_Meafyeney_Sug_Sidur SM 
      WHERE SM.kod_meafyen =p_Kod
      ORDER BY SM.ME_TAARICH DESC ; 
  END Get_History_Of_TypeSidur;
  
  PROCEDURE Update_Meafyeney_Sug_Sidur(p_KodMeafyen number,p_SugSidur number,p_MeTaarich date,p_AdTaarich date,p_Erech VARCHAR2 ,p_SugNatun VARCHAR2, p_taarich_idkun_acharon DATE,p_meadken_acharon NUMBER) AS
  BEGIN
update TB_Meafyeney_Sug_Sidur SM
    set SM.ERECH = p_Erech ,
        SM.AD_TAARICH = p_AdTaarich , 
          SM.TAARICH_IDKUN_ACHARON = p_taarich_idkun_acharon,
          SM.MEADKEN_ACHARON = p_meadken_acharon
    where SUG_SIDUR = p_SugSidur AND 
    ME_TAARICH = p_MeTaarich AND 
    KOD_MEAFYEN = p_KodMeafyen;
    
  END Update_Meafyeney_Sug_Sidur;
  
  PROCEDURE Insert_Meafyeney_Sug_Sidur(p_KodMeafyen number,p_SugSidur number,p_MeTaarich date,p_AdTaarich date,p_Erech VARCHAR2 ,p_SugNatun VARCHAR2, p_taarich_idkun_acharon DATE,p_meadken_acharon NUMBER) AS
  BEGIN 
      insert into TB_Meafyeney_Sug_Sidur
    (ad_taarich, erech, kod_meafyen, me_taarich, meadken_acharon, SUG_SIDUR) 
    VALUES (p_adtaarich, p_erech, p_kodmeafyen, p_metaarich, p_meadken_acharon, p_SugSidur);
  END Insert_Meafyeney_Sug_Sidur;


  PROCEDURE  Get_Meafyeney_Elementim(p_Kod IN VARCHAR2,p_Period in VARCHAR2,p_Cur OUT CurType)  IS
  v_tar_me DATE ; 
  BEGIN
      v_tar_me:=to_date('01/' || p_Period,'dd/mm/yyyy'); 
       OPEN p_Cur FOR
            SELECT tb.KOD_ELEMENT, tb.Kod_Meafyen,(ctb.KOD_MEAFYEN || ',' || ctb.SHEM_MEAFYEN || '(' || ctb.SUG_NATUN || ')') DetailsElement  ,Me_Taarich,Ad_Taarich,Erech, ctb.sug_natun,tb.Taarich_Idkun_Acharon LastUpdate
      FROM TB_Meafyeney_Elementim  tb
      INNER JOIN CTB_MEAFYENEY_Elementim ctb 
      ON ctb.kod_meafyen= tb.kod_meafyen
              WHERE tb.Me_Taarich <v_tar_me  AND tb.KOD_ELEMENT = p_Kod AND ctb.PAIL = 1 
          ORDER BY   ctb.Kod_Meafyen ;
    EXCEPTION
        WHEN OTHERS THEN
            RAISE;    
    NULL;
  END Get_Meafyeney_Elementim;
  
  PROCEDURE GetMatching_Element_Desc(p_Prefix in VARCHAR2,  p_Cur OUT CurType) AS
  BEGIN
      OPEN p_Cur for 
      SELECT SHEM_MEAFYEN from CTB_MEAFYENEY_ELEMENTIM ME
      WHERE ME.SHEM_MEAFYEN   LIKE  p_Prefix || '%' and pail =1 ; 
      
    NULL;
  END GetMatching_Element_Desc;
  
  PROCEDURE Get_AllKodOfElements(  p_Cur OUT CurType) AS
  BEGIN
  OPEN p_Cur for 
      SELECT KOD_MEAFYEN KOD_ELEMENT 
      FROM CTB_MEAFYENEY_ELEMENTIM   
      WHERE PAIL = 1 ;
    END Get_AllKodOfElements;

  PROCEDURE GetMatching_Element_Kod(p_Prefix in VARCHAR2,  p_Cur OUT CurType) AS
  BEGIN
      OPEN p_Cur for 
          SELECT KOD_MEAFYEN 
          FROM CTB_MEAFYENEY_ELEMENTIM ME
          WHERE ME.KOD_MEAFYEN   LIKE  p_Prefix || '%' and pail =1 
          ORDER BY KOD_MEAFYEN ASC ;
    NULL;
  END GetMatching_Element_Kod;
  
  PROCEDURE Get_DetailsKodOfElement(p_Cur OUT CurType ) AS 
  BEGIN
        OPEN p_Cur for 
      SELECT  SM.KOD_MEAFYEN Kod_Meafyen , (SM.KOD_MEAFYEN  ||',' || SM.SHEM_MEAFYEN || '(' || sm.SUG_NATUN || ')' ) DetailsElement 
      FROM CTB_MEAFYENEY_ELEMENTIM SM 
      WHERE SM.PAIL = 1 ;
      END Get_DetailsKodOfElement; 

  PROCEDURE Get_ElementDesc_by_Kod(p_Kod in VARCHAR2,p_Desc out VARCHAR2) AS
  BEGIN
      SELECT  SM.SHEM_MEAFYEN INTO p_desc  
      FROM CTB_MEAFYENEY_ELEMENTIM SM 
      WHERE SM.KOD_MEAFYEN = p_Kod;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
        p_Desc := '';
  END Get_ElementDesc_by_Kod;

  PROCEDURE Get_Kod_By_ElementDesc(p_Desc in VARCHAR2,p_Kod out VARCHAR2) AS
  BEGIN
      SELECT  SM.KOD_MEAFYEN INTO p_Kod  
      from CTB_MEAFYENEY_ELEMENTIM SM 
      WHERE SM.SHEM_MEAFYEN = p_desc;
    EXCEPTION
        WHEN NO_DATA_FOUND THEN
        p_Kod := '';
  END Get_Kod_By_ElementDesc;
  
  PROCEDURE Get_History_Of_Element(p_Kod in VARCHAR2, p_Cur out Curtype) as 
  BEGIN
  OPEN p_Cur for 
      SELECT SM.ME_TAARICH,SM.ad_taarich,SM.erech   
      FROM TB_MEAFYENEY_ELEMENTIM SM 
      WHERE SM.kod_meafyen =p_Kod
      ORDER BY SM.ME_TAARICH DESC ; 
  END Get_History_Of_Element;

 PROCEDURE Update_Meafyeney_Elementim(p_KodMeafyen number,p_KodElement number,p_MeTaarich date,p_AdTaarich date,p_Erech VARCHAR2 ,p_SugNatun VARCHAR2, p_taarich_idkun_acharon DATE,p_meadken_acharon NUMBER)  AS
  BEGIN
update TB_MEAFYENEY_ELEMENTIM SM
    set SM.ERECH = p_Erech ,
        SM.AD_TAARICH = p_AdTaarich , 
          SM.TAARICH_IDKUN_ACHARON = p_taarich_idkun_acharon,
          SM.MEADKEN_ACHARON = p_meadken_acharon
    where KOD_ELEMENT = p_KodElement AND 
    ME_TAARICH = p_MeTaarich AND 
    KOD_MEAFYEN = p_KodMeafyen;

    
  END Update_Meafyeney_Elementim;
 

  PROCEDURE Insert_Meafyeney_Elementim(p_KodMeafyen number,p_KodElement number,p_MeTaarich date,p_AdTaarich date,p_Erech VARCHAR2 ,p_SugNatun VARCHAR2, p_taarich_idkun_acharon DATE,p_meadken_acharon NUMBER)  AS
  BEGIN
      insert into TB_MEAFYENEY_ELEMENTIM
    (ad_taarich, erech, kod_meafyen, me_taarich, meadken_acharon, KOD_ELEMENT) 
    VALUES (p_adtaarich, p_erech, p_kodmeafyen, p_metaarich, p_meadken_acharon, p_KodElement);
  END Insert_Meafyeney_Elementim;

END PKG_SIDURIM;
/
 
