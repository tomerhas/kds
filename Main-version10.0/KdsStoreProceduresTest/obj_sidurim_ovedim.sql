
CREATE OR REPLACE TYPE KDSADMIN.OBJ_SIDURIM_OVDIM as object 
    (mispar_ishi number (9),mispar_sidur number (6), taarich date, shat_hatchala date, shat_gmar date,
    lo_letashlum number(1), hashlama number(1), hamarat_shabat number(1),chariga number,
    visa number(1),out_michsa number(1),pitzul_hafsaka number(1), new_mispar_sidur number (6), mezake_nesiot number(1),
    mezake_halbasha number(1),
    shat_hatchala_letashlum date, 
    shat_gmar_letashlum date,    
    tosefet_grira number(1),
    mikum_shaon_knisa             number(3),
    mikum_shaon_yetzia            number(3),    
    achuz_knas_lepremyat_visa     number(3),
    achuz_viza_besikun            number(3),   
    mispar_musach_o_machsan       number(4),
    kod_siba_lo_letashlum         number(2),
    kod_siba_ledivuch_yadani_in   number(2),
    kod_siba_ledivuch_yadani_out  number(2),
    shayah_leyom_kodem            number(1),
    heara                         varchar2(100 byte),
    mispar_shiurey_nehiga         number(2),    
    sug_hazmanat_visa             number(2),
    meadken_acharon               number(9),
    taarich_idkun_acharon         date,
    update_object number(1),
    bitul_o_hosafa number(1),
    new_shat_hatchala date )
/