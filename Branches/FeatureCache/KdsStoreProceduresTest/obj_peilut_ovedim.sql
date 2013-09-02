CREATE OR REPLACE TYPE KDSADMIN.obj_peilut_ovdim as object

    (mispar_ishi number (9),

    taarich date,

    mispar_sidur number(6),

    shat_hatchala_sidur date,

    shat_yetzia date,

    mispar_knisa number,

    makat_nesia number(8),

    oto_no number(7),

    mispar_siduri_oto number,

    kisuy_tor number,

    bitul_o_hosafa number(1),

    kod_shinuy_premia number(1),

    mispar_visa number(10),

    imut_netzer number(1),

    shat_bhirat_nesia_netzer  date,

    oto_no_netzer number(7),

    mispar_sidur_netzer  number(6),

    shat_yetzia_netzer date,

    makat_netzer number(8),

    shilut_netzer varchar2(5 byte),

    mikum_bhirat_nesia_netzer  number(3),

    mispar_matala  number(6),

    taarich_idkun_acharon  date,

    meadken_acharon  number(9),

    heara  varchar2(100 byte),

    ishur_kfula   number(1),
    
    update_object number(1),
    
    new_mispar_sidur number (6),
    
    dakot_bafoal number(3),
    
    new_shat_yetzia date,
    
    new_shat_hatchala_sidur date


    )
/

