﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Diagnostics;
using System.Data;
using KdsLibrary.Security;
using KdsLibrary.Controls;
using System.Configuration;
using System.Text.RegularExpressions;
using KDSCommon.UDT;
using DalOraInfra.DAL;
using KDSCommon.Helpers;
using KDSCommon.Enums;
using Saplin.Controls;

namespace KdsLibrary
{
    public static class clGeneral
    {
        private const string KdsEventLogSource = "Kds";

        public const string cSchema = "KDSADMIN";
        public const string cKds_Premia = "kds_premia";
        public const string cCtbZmaneyNesiaa = "ctb_zmaney_nesiaa";
        public const string cCtbZmaneyHalbasha = "ctb_zmaney_halbasha";
        public const string cCtbLina = "ctb_lina";
        public const string cCtbSibotHashlamaLeyom = "ctb_sibot_hashlama_leyom";
        public const string cCtbDivuchHarigaMeshaot = "ctb_divuch_hariga_meshaot";
        public const string cCtbPitzulaHafsaka = "ctb_pitzul_hafsaka";

        #region pkg_rikuz_avoda constants
        public const string cProGetRikuzAvodaChodshiTemp = "PKG_RIKUZ_AVODA.pro_get_rikuz_chodshi_temp";
        #endregion
        
        #region pkg_batch constants
        public const string cProInsBakasha = "pkg_batch.pro_ins_bakasha";
        public const string cProInsLogBakasha = "pkg_batch.pro_ins_log_bakasha";
        public const string cProUpdBakasha = "pkg_batch.pro_upd_bakasha";
        public const string cProInsBakashaParam = "pkg_batch.pro_ins_bakasha_param";
        public const string cProGetPirteyRitzotChishuv = "pkg_batch.pro_get_pirtey_ritzot";
        public const string cProGetYameiAvodaMeshek = "pkg_batch.pro_get_yamei_avoda_meshek";
        public const string cProGetAllYameiAvoda = "pkg_batch.pro_get_all_yamei_avoda";
        public const string cProGetYameiAvodaShinuiHR = "pkg_batch.pro_get_yamei_avoda_shinui_hr";
        public const string cProInsertDebugBathcPopulation = "pkg_batch.pro_insert_debug_maatefet";
        public const string cProInsertLogMaatefet = "pkg_batch.pro_insert_log_maatefet";
        public const string cProGetShinuyMatsavOvdim = "pkg_batch.pro_get_shinuy_matsav_ovdim";
        public const string cProGetShinuyMeafyeneyBizua = "pkg_batch.pro_get_Shinuy_meafyeney_bizua";
        public const string cProGetShinuyPireyOved = "pkg_batch.pro_get_shinuy_pirey_oved";
        public const string cProGetShinuyBrerotMechdal = "pkg_batch.pro_get_shinuy_brerot_mechdal";
        public const string cProInsOvdimImShinuyHR = "pkg_batch.pro_ins_ovdim_im_shinuy_hr";
        public const string cProInsDefaultsHR = "pkg_batch.pro_ins_defaults_hr";
        public const string cFunGetNumChangesHrToShguim = "pkg_batch.fun_get_num_changes_to_shguim";
        public const string cFunInsertLogTahalichRecord = "PKG_BATCH.pro_ins_log_tahalich_rec";
        public const string cProUpdateLogTahalichRecord = "PKG_BATCH.pro_upd_log_tahalich_rec";
        public const string cProCheckViewEmpty = "PKG_BATCH.pro_check_view_empty";

        public const string cProMoveNewMatzavOvdimToOld = "pkg_batch.MoveNewMatzavOvdimToOld";
        public const string cProMoveNewPirteyOvedToOld = "pkg_batch.MoveNewPirteyOvedToOld";
        public const string cProMoveNewMeafyenimOvdimToOld = "pkg_batch.MoveNewMeafyenimOvdimToOld";
        public const string cProMoveNewBrerotMechdalToOld = "pkg_batch.MoveNewBrerotMechdalToOld";
        public const string cProDeleteLogTahalichRecords = "pkg_batch.pro_delete_log_tahalich_rcds";
        public const string cProGetDetailsOvdimLeRikuzim = "pkg_batch.Pro_get_pirtey_ovdim_leRikuzim";
        public const string cProPrepareOvdimRikuzim = "pkg_batch.Pro_PrepareOvdimRikuzim";
        public const string cProGetEmailOvdimLeRikuzim = "pkg_batch.Pro_get_Email_Ovdim_LeRikuzim";
        public const string cProDeleteRikuzimPdf = "pkg_batch.Pro_Delete_Rikuzim_Pdf";


        public const string cProPrepareYameiAvodaMeshek = "pkg_batch.Prepare_yamei_avoda_meshek";
        public const string cProPrepareYameiAvodaShinuiHr = "pkg_batch.Prepare_yamei_avoda_shinui_hr";
        public const string cProproGetNetunimForProcess = "pkg_batch.pro_get_netunim_for_process";
        public const string cProPrepareYameiAvodaPremiot = "pkg_batch.Prepare_premiot_shguim_batch";

        public const string cProSaveRikuzPdf = "pkg_batch.Pro_Save_Rikuz_Pdf";
        public const string cProGetRikuzPdf = "pkg_batch.Pro_Get_Rikuz_Pdf";
        public const string cProDeleteBakashotYeziratRikuzim = "pkg_batch.DeleteBakashotYeziratRikuzim";
        public const string cProRefreshTable = "PKG_BATCH.pro_RefreshMv";
        public const string cProGetMakatimLeTkinut = "PKG_BATCH.pro_Get_Makatim_LeTkinut";
        public const string cProInsMachalotLoMeturgamot = "PKG_BATCH.pro_ins_machalot_lo_meturgamot";

        public const string cProInsYameyAvodaHistory = "PKG_MF_HISTORY.Ins_netuney_historiya_Yamim";
        public const string cProInsSidurimOvdimHistory = "PKG_MF_HISTORY.Ins_netuney_historiya_Sidurim";
        public const string cProInsPeilutOvdimHistory = "PKG_MF_HISTORY.Ins_netuney_historiya_Peilut";
        public const string cProInsNetuneyHistoryHodshi = "PKG_MF_HISTORY.Pro_Ins_historiya_chodshi";
        public const string cProInsNetuneyHistoryYomi = "PKG_MF_HISTORY.Pro_Ins_historiya_yomi";
        public const string cProInsNetuneyHistoryCatalog = "PKG_MF_HISTORY.Ins_netuney_historiya_Catalog";
      
#endregion 
        #region pkg_sdrn constants
        public const string cProGetKnisotToRefresh = "PKG_sdrn.pro_get_Knisot_sdrm";
        public const string cProInsertKnisot = "PKG_sdrn.pro_insert_knisot";
        public const string cProGetStatusSdrn = "PKG_sdrn.pro_GetStatusSdrn";
        public const string cProRunSdrnWithDate = "KDSADMIN.RunSdrnWithDate";
        public const string cProRunRetrospectSdrn = "PKG_sdrn.pro_runRetrospectSdrn";
        #endregion
        #region pkg_calc constants
        public const string cProCheckOvedPutar = "pkg_calc.pro_get_oved_putar";
        public const string cProGetPeiluyotLeoved = "pkg_calc.pro_get_peiluyot_leoved";
        public const string cProGetPirteyOvedForMonth = "pkg_calc.pro_get_pirtey_oved_ForMonth";
#endregion

#region kds_catalog_pack constants
        public const string cGetKavDetails = "kds_catalog_pack.GetKavDetails";
        public const string cGetRekaDetails = "kds_catalog_pack.GetRekaDetails";
        public const string cGetNamakDetails = "kds_catalog_pack.GetNamakDetails";
        public const string cGetRekaDetailsByXY = "kds_catalog_pack.GetRekaDetailsByXY";
#endregion 

#region PKG_COMPONENT_SIDURIM constant
        public const string cProGetMatchingComponentSidurDesc = "PKG_COMPONENT_SIDURIM.pro_get_matching_description";
        public const string cProGetMatchingComponentSidurKod = "PKG_COMPONENT_SIDURIM.pro_get_matching_kod";
        public const string cProGetComponentSidurDescByKod = "PKG_COMPONENT_SIDURIM.pro_get_description_by_kod";
        public const string cProGetKodByComponentSidurDesc = "PKG_COMPONENT_SIDURIM.pro_get_kod_by_description";
        public const string cProGetHistoryOfComponentSidur = "PKG_COMPONENT_SIDURIM.pro_get_history";
#endregion 

#region PKG_COMPONENT_SUG_SIDUR constant
        public const string cProGetMatchingComponentTypeSidurDesc = "PKG_COMPONENT_SUG_SIDUR.pro_get_matching_description";
        public const string cProGetMatchingComponentTypeSidurKod = "PKG_COMPONENT_SUG_SIDUR.pro_get_matching_kod";
        public const string cProGetComponentTypeSidurDescByKod = "PKG_COMPONENT_SUG_SIDUR.pro_get_description_by_kod";
        public const string cProGetKodByComponentTypeSidurDesc = "PKG_COMPONENT_SUG_SIDUR.pro_get_kod_by_description";
        public const string cProGetHistoryOfComponentTypeSidur = "PKG_COMPONENT_SUG_SIDUR.pro_get_history";
#endregion 
        
#region PKG_ELEMENTS constants
        public const string cProGetDataByKodElement = "PKG_ELEMENTS.pro_get_data_by_kod_element";
        public const string cProGetElementsVeMeafyenim = "PKG_ELEMENTS.pro_get_elements_vemeafyenim";
        public const string cProGetTeurElements = "PKG_ELEMENTS.pro_get_teur_elements";
        public const string cProGetMatchingElementDesc = "PKG_ELEMENTS.pro_get_matching_description";
        public const string cProGetMatchingElementKod = "PKG_ELEMENTS.pro_get_matching_kod";
        public const string cProGetElementDescByKod = "PKG_ELEMENTS.pro_get_description_by_kod";
        public const string cProGetKodByElementDesc = "PKG_ELEMENTS.pro_get_kod_by_description";
        public const string cProGetHistoryOfElement = "PKG_ELEMENTS.pro_get_history";
        public const string cProRefreshMeafyeneyElement = "PKG_ELEMENTS.calling_Pivot_Meafyeney_e";
        public const string cProGetAllElementsKod = "PKG_ELEMENTS.pro_get_all_elements_kod";
        public const string cProGetElementDetails = "PKG_ELEMENTS.pro_get_element_details";
        public const string cProGetVisutDetails = "PKG_ELEMENTS.pro_get_visut_details";
#endregion

#region PKG_ERRORS constants
        public const string cFnIsOtoNumberExists = "pkg_errors.fn_is_oto_number_exists";
        public const string cProGetOvedSidurimPeilut = "pkg_errors.pro_get_oved_sidurim_peilut";
        public const string cProGetLastUpdateData = "PKG_ERRORS.pro_oved_update_fields";
        public const string cProGetOvedYomAvodaUDT = "PKG_ERRORS.pro_get_oved_yom_avoda_UDT";
        public const string cProGetLookUpTables = "pkg_errors.pro_get_lookup_tables";
        public const string cProGetCtbShgiot = "pkg_errors.pro_get_ctb_shgiot";
        public const string cProGetErrorsForField = "pkg_errors.pro_get_errors_for_field";
        public const string cProInsApprovalErrors = "PKG_ERRORS.pro_ins_approval_errors";
        public const string cFnIsApprovalErrorExists = "PKG_ERRORS.fn_is_approval_errors_exists";
        public const string cProInsPeilutOvdim = "pkg_errors.pro_ins_peilut_ovdim";
        public const string cProGetAllErrorsAndFields = "pkg_errors.pro_get_all_shgiot";
        public const string cProGetApprovalErrors = "pkg_errors.get_approval_errors";
        public const string cProGetShgiotNoActive = "Pkg_Errors.pro_get_shgiot_active";
        public const string cProSaveCardIsUsed = "Pkg_Errors.fun_save_card_is_used";
#endregion

#region pkg_ovdim constants
        public const string cProGetHistoriatMeafyenLeoved = "pkg_ovdim.pro_get_historiat_meafyen";
        public const string cProGetMonthWorkToOved = "pkg_ovdim.pro_get_month_year_to_oved";
        public const string cProGetMonthHuavarLesacharToOved = "pkg_ovdim.pro_get_months_huavar_lesachar";
        public const string cProGetStatusToOved = "pkg_ovdim.pro_get_status_oved";
        public const string cProGetPirteyOvedAll = "pkg_ovdim.pro_get_pirtey_oved_all";
        public const string cProGetHistoriatNatunLeoved = "pkg_ovdim.pro_get_historiat_natun";
        public const string cProGetRitzotChishuv = "pkg_ovdim.pro_get_ritzot_chishuv";
        public const string cProGetShaotMealMichsa = "pkg_ovdim.pro_get_shaot_meal_michsa";
        public const string cProInsBakashMechutzLemichsa = "pkg_ovdim.pro_ins_bakasha_mechutz_lemich";
        public const string cProGetSugYechidaOved = "pkg_ovdim.fun_get_sug_yechida_oved";
        public const string cProGetMispareyRashamot = "pkg_ovdim.pro_get_MisparIshiByKodVeErech";
        public const string cproGetMerkazEruaByKod = "pkg_ovdim.pro_get_merkaz_erua_ByKod";
        public const string cProGetMikumShaonInOut = "pkg_ovdim.pro_get_mikum_shaon_in_out";
        public const string cproGetZmanNesiaaMistane = "pkg_ovdim.pro_get_zman_nesiaa_mistane";
        public const string cproGetZmanNesiaaOvdim = "pkg_ovdim.pro_get_zman_nesiaa_ovdim";
        public const string cproProUpdZmanNesiaa = "pkg_ovdim.pro_upd_zman_nesiaa";
        public const string cProGetLastUpdator = "pkg_ovdim.pro_get_last_updator";
        public const string cproGetRechivim = "pkg_ovdim.pro_get_RECHIVIM";
        public const string cProSaveEmployeeCard = "pkg_ovdim.pro_save_employee_card";
        public const string cProInsYemeyAvodaLeoved = "pkg_ovdim.pro_ins_yemey_avoda_leoved";
        public const string cFunIsCardExistsYemeyAvoda = "pkg_ovdim.fun_is_card_exists_yemey_avoda";
        public const string cProGetYemeyAvodaLeoved = "pkg_ovdim.pro_get_yemey_avoda";
        public const string cProGetErrorOvdim = "pkg_ovdim.pro_get_error_ovdim";
        public const string cProGetOvedFullName = "pkg_ovdim.pro_get_oved_full_name";
        public const string cProGetOvedimMisparIshi = "pkg_ovdim.pro_get_ovedim_mispar_ishi";
        public const string cProGetActiveWorkers = "pkg_ovdim.pro_get_Active_Workers";
        public const string cProGetOvedErrorsCards = "pkg_ovdim.pro_get_oved_errors_cards";
        public const string cProGetOvedDetails = "pkg_ovdim.pro_get_oved_details";
        public const string cProGetOvedimByName = "pkg_ovdim.pro_get_ovedim_by_name";
        public const string cProGetOvedMisparIshi = "pkg_ovdim.pro_get_oved_mispar_ishi";
        public const string cProGetPirteyOved = "pkg_ovdim.pro_get_pirtey_oved";
        public const string cProGetSugChishuv = "pkg_ovdim.pro_get_sug_chishuv";
        public const string cProGetHeadruyot = "pkg_ovdim.pro_get_headruyot";
        public const string cProGetPirteyHeadrut = "pkg_ovdim.pro_get_pirtey_headrut";
        public const string cProGetPirteyHeadrutTemp = "pkg_ovdim.pro_get_pirtey_headrut_tmp";
        public const string cProGetRechivimChodshiyim = "pkg_ovdim.pro_get_rechivim_codshiyim";
        public const string cProGetRechivimChodshiyimTemp = "pkg_ovdim.pro_get_rechivim_codshiyim_tmp";
        public const string cProGetRikuzChodshi = "pkg_ovdim.pro_get_rikuz_chodshi";
        public const string cProGetRikuzChodshiTemp = "pkg_ovdim.pro_get_rikuz_chodshi_tmp";
        public const string cFunGetMeafyenLeOved = "pkg_ovdim.fun_get_meafyen_oved";
        public const string cProGetOvedSnifUnit = "pkg_ovdim.pro_get_oved_snif_unit";
        public const string cProGetOvedCards = "pkg_ovdim.pro_get_oved_cards";
        public const string cProGetOvedCardsTipul = "pkg_ovdim.pro_get_oved_cards_in_tipul";
        public const string cProGetSharedAndMonthlyQuota = "pkg_ovdim.pro_get_SharedMonthly_Quota";
        public const string cProGetHourApproval = "pkg_ovdim.pro_get_Hour_Approval";
        public const string cProGetRelevantMonthOfApproval = "pkg_ovdim.pro_getRelevantMonthOfApproval";
        public const string cProUptPremia = "pkg_ovdim.pro_upd_premia_details";
        public const string cProGetStatusIsuk = "pkg_ovdim.pro_get_Status_Isuk";
        public const string cProGetMeafyeneyBituaLeoved = "pkg_ovdim.pro_get_meafyeney_oved";
        public const string cProGetMeafyeneyBituaLeovedAll = "pkg_ovdim.pro_get_meafyeney_oved_all";
        public const string cProUptHourApproval = "PKG_OVDIM.pro_upd_Hour_Aproval";
        public const string cProGetPeiluyotLeOved = "pkg_ovdim.pro_get_peiluyot_le_oved";
        public const string cProUpdSidurimLeOved = "pkg_ovdim.pro_upd_sidurim_ovdim";
        public const string cProUpdYameyAvodaOvdim = "pkg_ovdim.pro_upd_yamey_avoda_ovdim";
        public const string cProUpdPeilutOvdim = "pkg_ovdim.pro_upd_peilut_ovdim";
        public const string cProGetIdkuneyRashemet = "pkg_ovdim.pro_get_idkuney_rashemet";
        public const string cProGetMeadkenAcharon = "pkg_ovdim.pro_get_meadken_acharon";
        public const string cProGetOvedDetailsBeTkufa = "pkg_ovdim.pro_get_oved_details_betkufa";
        public const string cProGetOvdimToPeriodByCode = "pkg_ovdim.pro_get_ovdim_to_period_ByCode";
        public const string cProSaveMeasherOmistayeg = "pkg_ovdim.pro_save_measher_O_mistayeg";
        public const string cProGetPakadId = "pkg_ovdim.pro_get_pakad_id";
        public const string cFunCheckPeilutExist = "pkg_ovdim.fun_check_peilut_exist";
        public const string cFunCheckSidurExist = "pkg_ovdim.fun_check_sidur_exist";       
        public const string cFunIsCardLastUpdate = "pkg_ovdim.func_is_card_last_updated";
        public const string cProUpdSadotNosafim = "pkg_ovdim.pro_upd_sadot_nosafim";
        public const string cProDelKnisotPeilut = "pkg_ovdim.pro_del_knisot_peilut";
        public const string cProDelHachanotMechona = "pkg_ovdim.pro_del_hachanot_mechona";
        public const string cProGetArachimLeoved = "pkg_ovdim.pro_get_arachim_by_misIshi";
        public const string cProUpdIdkunRashemet = "pkg_ovdim.pro_ins_idkuney_rashemet";
        public const string cProDeleteIdkunRashemet = "pkg_ovdim.pro_delete_idkuney_rashemet";
        public const string cProSaveToBarcodeTable = "pkg_ovdim.pro_save_barcode_table";

        public const string cProGetPirteyOvedLetkufot = "pkg_ovdim.pro_get_pirtey_oved_letkufot";
        public const string cProGetMeafyenOvedLetkufot = "pkg_ovdim.pro_get_meafyen_oved_letkufot";
        public const string cProGetRikuzeyAvodaLeoved = "pkg_ovdim.pro_get_rikuzey_avoda_leoved";
        public const string cProGetOvdimLefiRikuzim = "pkg_ovdim.get_ovdim_by_Rikuzim";
        public const string cProGetCountWorkCardNoShaotLetahlum = "pkg_ovdim.fun_get_count_no_val_letashlum";
        public const string GetCountWCLoLetashlumWithMeafyenim = "pkg_ovdim.fun_count_lo_letashlum_meafyen";
        public const string cProGetWorkCardNoShaotLetahlum = "pkg_ovdim.pro_get_workcad_no_val_letash";
        public const string cProGetNochechutMerukezet = "pkg_ovdim.pro_get_nochechut_merukezet";
        
#endregion

#region PKG_PARAMETERS constants
        public const string cProGetErechParameterByKod = "PKG_PARAMETERS.pro_get_erech_by_kod";
        public const string cProGetMatchingParametersDesc = "PKG_PARAMETERS.pro_get_matching_description";
        public const string cProGetMatchingParametersKod = "PKG_PARAMETERS.pro_get_matching_kod";
        public const string cProGetParameterDescByKod = "PKG_PARAMETERS.pro_get_description_by_kod";
        public const string cProGetKodByParametersDesc = "PKG_PARAMETERS.pro_get_kod_by_description";
        public const string cProGetHistoryOfParameters = "PKG_PARAMETERS.pro_get_history";
#endregion

#region PKG_REQUEST constants
        public const string cProGetRequests = "PKG_REQUEST.pro_get_requests";
        public const string cProGetRequestsList = "PKG_REQUEST.pro_get_requests_list";
        public const string cProGetStatusRequest = "PKG_REQUEST.pro_get_status_request";
        public const string cProGetTypeRequest = "PKG_REQUEST.pro_get_type_request";
        public const string cProGetMonthsFromRequest = "PKG_REQUEST.pro_get_month_requests";
        public const string cProGetLogBakashot = "PKG_REQUEST.pro_get_log_bakashot";
        public const string cProGetListOvdimFromLogBakashot = "PKG_REQUEST.pro_get_ovdim_log_bakashot";
        public const string cProGetTahalichKlita = "PKG_REQUEST.pro_get_tahalich_klita";
        public const string cProGetLogTahalich = "PKG_REQUEST.pro_get_log_tahalich";
        public const string cFunCheckTahalichEnd = "PKG_REQUEST.fun_check_tahalich_beEnd";
        public const string cFunGetMaxBakasaBySug = "PKG_REQUEST.fun_get_max_bakasa_by_sug";
#endregion

#region PKG_REPORTS constants
        public const string cProGetIdOfYameyAvoda = "PKG_REPORTS.pro_get_Id_of_Yamey_Avoda";
        public const string cProGetRizotChishuvLechodeshSucceeded = "PKG_REPORTS.pro_get_rizot_chishuv_lehodesh";
        public const string cProGetListReports = "PKG_REPORTS.pro_get_reports_list";
        public const string cProGetRizotChishuvSucceeded = "PKG_REPORTS.pro_get_rizot_chishuv_succeed";
        public const string cProGetLicenceNumber = "PKG_REPORTS.pro_get_LicenseNumber";
        public const string cProGetOvdim = "PKG_REPORTS.pro_get_Id_of_Ovdim";
        public const string cProGetWorkStation = "PKG_REPORTS.pro_get_WorkStation";
        public const string cProGetMakatOfActivity = "PKG_REPORTS.pro_get_MakatOfActivity";
        public const string cProGetSidurimOvdim = "PKG_REPORTS.pro_get_SidurimOvdim";
        public const string cProGetDefinitionReports = "PKG_REPORTS.pro_get_Definition_Reports";
        public const string cProGetDetailsReport = "PKG_REPORTS.pro_get_ReportDetails";
        public const string cProGetDetailsReports = "PKG_REPORTS.pro_get_Details_Reports";
        public const string cProGetDestinationsReports = "PKG_REPORTS.pro_get_Destinations_Reports";
        public const string cProInsertHeavyReports = "PKG_REPORTS.pro_ins_Heavy_Reports";
        public const string cProGetPrepareReports = "PKG_REPORTS.pro_getPrepareReports";
        public const string cProUpdatePrepareReports = "PKG_REPORTS.pro_updatePrepareReports";
        public const string cProGetHeavyReportsToDelete = "PKG_REPORTS.pro_get_HeavyReportsToDelete";
        public const string cProGetProfilToDisplay = "PKG_REPORTS.pro_get_ProfilToDisplay";
        public const string cProGetSnifeyTnuaByEzor = "PKG_REPORTS.pro_GetSnifeyTnuaByEzor";
        public const string cProGetSnifeyTnua = "PKG_REPORTS.proGetSnifeyTnuaByEzor";
        public const string cProGetReportDetails = "PKG_REPORTS.pro_get_Report_Details";
#endregion

#region PKG_SIDURIM constants
        public const string cProGetSidurimLeoved = "PKG_SIDURIM.pro_get_sidurim_leoved";
        public const string cProInsUpdSidurimLeoved = "pkg_sidurim.pro_ins_upd_sidurim_ovdim";
        public const string cProGetMeafyeneySidur = "PKG_SIDURIM.pro_get_meafyen_sidur_by_kod";
        public const string cProTeurSidurWhithOutList = "PKG_SIDURIM.pro_get_teur_whithoutlist";
        public const string cProGetKodElement = "PKG_SIDURIM.pro_get_kod_element";
        public const string cProKodSidurWhithOutList = "PKG_SIDURIM.pro_get_kod_whithoutlist"; 
        public const string cProGetCtbSidurimWithMeafyen = "PKG_SIDURIM.pro_get_ctb_sidurim_meafyen";
        public const string cProGetMatchingSidurDesc = "PKG_SIDURIM.pro_get_matching_description";
        public const string cProGetMatchingSidurKod = "PKG_SIDURIM.pro_get_matching_kod";
        public const string cProGetSidurDescbyKod = "PKG_SIDURIM.pro_get_description_by_kod";
        public const string cProGetKodBySidurDesc = "PKG_SIDURIM.pro_get_kod_by_description";
        public const string cProGetHistoryOfSidur = "PKG_SIDURIM.pro_get_history";
        public const string cProGetSidur = "PKG_SIDURIM.get_sidur";
        public const string cProRefreshSidurim = "PKG_SIDURIM.calling_Pivot_Sidurim_M";
        public const string cProGetMusach_O_Machsan = "PKG_SIDURIM.ProGetMusach_O_Machsan";
        public const string cGetTbSadotNosafimLesidur = "PKG_SIDURIM.get_tb_sadot_nosafim_lesidur";
        public const string cProGetMeafyeneySidurim = "pkg_sidurim.pro_get_meafyeney_sidur";
        public const string cProGetMeafyenySidurById = "pkg_sidurim.pro_get_meafyeny_sidur_by_id";
        public const string cProGetSibaLoLetashlum = "pkg_sidurim.pro_get_siba_lo_letashlum";
        public const string cProGetSidurimMeyuchadim = "pkg_sidurim.get_sidurim_meyuchadim_all";
        public const string cFunGetMeshechSidurForPeriod = "pkg_sidurim.fun_get_meshech_sidur_Toperiod";
        public const string cFuncGetNextErrCard = "pkg_ovdim.func_get_next_err_card";
      //  public const string cProGetMeafyenimLesidurRagil = "PKG_SIDURIM.pro_get_meafyen_sidur_ragil";
#endregion

#region PKG_SUG_SIDUR constant
        public const string cProGetMeafyeneySugSidur = "PKG_SUG_SIDUR.get_meafyeney_sug_sidur_all";
        public const string cProGetMatchingTypeSidurDesc = "PKG_SUG_SIDUR.pro_get_matching_description";
        public const string cProGetMatchingTypeSidurKod = "PKG_SUG_SIDUR.pro_get_matching_kod";
        public const string cProGetTypeSidurDescByKod = "PKG_SUG_SIDUR.pro_get_description_by_kod";
        public const string cProGetKodByTypeSidurDesc = "PKG_SUG_SIDUR.pro_get_kod_by_description";
        public const string cProGetHistoryOfTypeSidur = "PKG_SUG_SIDUR.pro_get_history";
        public const string cProRefreshSugSidur = "PKG_SUG_SIDUR.calling_Pivot_Meafyeney_S";
        public const string cProGetSugeySidurFromTnua = "PKG_SUG_SIDUR.pro_get_sugey_sidur_tnua";
        public const string cFunGetSugSidur = "PKG_SUG_SIDUR.fun_get_sug_sidur";
#endregion 


public const string cProGetYamimMeyuchadim = "pkg_utils.pro_get_yamim_meyuchadim";
public const string cProGetSugeyYamimMeyuchadim = "pkg_utils.pro_get_sugey_yamim_meyuchadim";
     


#region KDS_SIDUR_AVODA_PACK constants 
        public const string cGetSidurDetails = "KDS_SIDUR_AVODA_PACK.GetSidurDetails";
#endregion
       
#region pkg_tnua constants
        public const string cGetKatalogKavim = "pkg_tnua.pro_get_kavim_details";        
        public const string cProGetMasharBusLicenseNum = "pkg_tnua.pro_get_mashar_bus_license_num";
        public const string cProGetMasharData = "PKG_TNUA.pro_get_mashar_data";
        public const string cProGetBusesDetails = "PKG_TNUA.pro_get_buses_details";
       
#endregion


#region tnua function constants
        public const string cCheckHityatzvutNehag = "kds.KdsVerifyDriverCheckIn";
        public const string cProInsTekenDriversToTnua = "KDSADMIN.pro_ins_amount_of_drivers_tnua";          
#endregion
        public const string cProBdikatChufshaRezifa = "KDSADMIN.pro_bdikat_chufsha_rezifa";
        public const string cProIdkunMachalotOvdim = "KDSADMIN.pro_idkun_machalot_ovdim";
        public const string cProBdikatYemeyMachala = "KDSADMIN.pro_bdikat_machalot_dor_B";
        #region pkg_task_Manager 
        public const string cGetStuckGroup = "pkg_task_Manager.GetStuckGroup";
        public const string cGetLogKvuzotByKod = "pkg_task_Manager.GetLogKvuzotByKod";
        #endregion 
        #region pkg_utils constants
        public const string cProGetPremiaYadanit = "pkg_utils.pro_get_premia_yadanit";
        public const string cProGetOvdimLeritza = "pkg_utils.pro_get_ovdim_leRitza";
        public const string cProGetEzorim = "pkg_utils.pro_get_ezorim";
        public const string cProGetSnifAv = "pkg_utils.pro_get_snif_av";
        public const string cProGetProfil = "pkg_utils.pro_get_profil";
        public const string cProGetHarshaotToProfil = "pkg_utils.pro_get_harshaot_to_profil";
        public const string cProGetMaamad = "pkg_utils.pro_get_maamad";
        public const string cProGetCardStatuses = "pkg_utils.pro_get_status_card";
        public const string cProGetHodaotToProfil = "pkg_utils.pro_get_hodaot_to_profil";
        public const string cProGetStatusIshurMaxLevel = "pkg_utils.pro_get_status_ishur_max_level";
        public const string cProCheckIshur = "pkg_utils.pro_check_ishur";
        public const string cProGetMutamut = "pkg_utils.pro_get_ctb_mutamut";
        public const string cProGetLogTahalichim = "pkg_utils.pro_get_log_tahalich";
        public const string cProGetOvdimToUser = "pkg_utils.pro_get_etz_nihuly_by_user";
        public const string cProGetOvdimForPremia = "pkg_utils.pro_get_ovdim_for_premia";
        public const string cProGetOvdimForPremiot = "pkg_utils.pro_get_ovdim_for_premiot";
        public const string cProGetPremiotDetails = "pkg_utils.pro_get_premyot_details";
        public const string cProGetOvdimByUserName = "pkg_utils.pro_get_etz_nihuly_by_name";
        public const string cProGetSibotLedivuchYadani = "pkg_utils.pro_get_sibot_ledivuch_yadani";
        public const string cProGetMeafyeneyBitsua = "pkg_utils.pro_get_meafyeney_bitua";
        public const string cProGetKodNatun = "pkg_utils.pro_get_kod_natun";
        public const string cProGetCtbElementim = "PKG_UTILS.pro_get_ctb_elementim";
        public const string cProGetParameters = "PKG_UTILS.pro_get_parameters_table";
        public const string cProGetZmanNesia = "PKG_UTILS.pro_get_zman_nesia";
        public const string cproProGetTbParametrim = "PKG_UTILS.Pro_Get_Value_From_Parametrim";
        public const string cProGetSadotNosafomLeSidur = "PKG_UTILS.get_sadot_nosafim_lesidur";
        public const string cProGetSadotNosafomKayamim = "PKG_UTILS.get_sadot_nosafim_kayamim";
        public const string cProGetSadotNosafomLePeilut = "PKG_UTILS.get_sadot_nosafim_lePeilut";
        public const string cProMoveRecordsToHistory = "PKG_UTILS.MoveRecordsToHistory";
        public const string cProGetTavlaotToRefresh = "PKG_UTILS.pro_get_tavlaot_to_refresh";
        public const string cProGetOvdimLeBakashatChishuv = "PKG_UTILS.pro_get_ovdim_by_bakasha";

        
     
#endregion
        #region PKG_APPROVALS constants
        public const string cFnIsOvedMusach = "PKG_APPROVALS.fn_is_oved_musach";       
        #endregion


#region PKG_DEMANDS
        public const string cProGetMachalaDays = "PKG_DEMANDS.pro_get_machala_days_rivon";
#endregion 
        public static string[] arrCalcType = new string[] { "רגיל", "הפרשים", "חודש פתוח" };
        public static string[] arrDays = new string[] { "א", "ב", "ג", "ד", "ה", "ו", "ש" };



        public const int cSuccessBatchRecordsParameterCode = 204;


      

        public enum enProfile
        {
            enDefault = 0,
            enSystemAdmin = 1,
            enRashemet = 2,
            enRashemetAll = 3,
            enMenahelImKfufim = 5,
            enVaadatPikuah = 6,
            enMenahelBankMeshek = 9
        }
        public enum enOnatiut
        {
            enOnatiut = 71
        }
        public enum enPtorHityazvut
        {
            PtorHityazvut = 1
        }
        public enum enHityazvutErrorInSite
        {
            enHityazvutErrorInSite = 1          
        }
        public enum enHityazvut
        {
            enFirstHityatzvut = 1,
            enSecondHityatzvut = 2
        }
        public enum enSugHashlama
        {
            enHashlama = 2, enNoHashlama = 0
        }
        public enum enShowOvedSibatHashlamaLeyom
        {
            enOvedKod5 = 5,
            enBitulHashlama = 0
        }
        public enum enShowPitzul
        {
            enLoZakaiLepitzul = 0,
            enOvedHafsaka = 3
        }
       
        public enum enMaamad
        {
            Friends = 1, Salarieds = 2
        }

        public enum enSugChishiuv
        {
            Ragil = 0, Hefreshim = 1, Patuch = 2
        }

       
        public enum enBitulOHosafa
        {
            BitulByUser = 1,
            AddByUser = 2,
            BitulAutomat = 3,
            AddAutomat = 4
        }
        public enum enStatusRequest
        {
            InProcess = 1, ToBeEnded = 2, Failure = 3, PartEnded = 4
        }
        public enum enLoLetashlum
        {
            WorkAtFridayWithoutPremission = 5, WorkAtSaturdayWithoutPremission = 4, SidurInNonePremissionHours = 10, WorkWitoutPremmision = 17, WorkCardWithoutHityachasut = 16 //17 -  עבודה ביום חול ללא הרשאה
        }
        public enum enNetuneyOved
        {
            Ezor = 3, Isuk = 6, Maamad = 13, Gil = 14, TachanatSachar = 15, SnifAv = 4, TarTchilatAvoda = 19
        }

        public enum enMeafyeneyOved
        {
            YemeyAvoda = 56, MichsaLeShaotNosafot = 12
        }
    
        public enum enRishyonAutobus
        {
            //לעובד אין רישיון אוטובוס
            enRishyon6 = 6, enRishyon10 = 10, enRishyon11 = 11
        }
        public enum enMeafyen32
        {
            //מאפיין 32 - זכאות להמרה
            enZakai = 1,  //זכאי
            enLoZakai = 2 //לא זכאי
        }
        public enum enMeafyen14
        {
            //מאפיין 14 - זכאות לזמן נסיעה
            enZakai = 1
        }
        public enum enMeafyen15
        {
            //מאפיין 15 - זכאות להלבשה
            enZakai = 1,  //זכאי
            enLoZakai = 2 //לא זכאי
        }
        public enum enMutaam
        {
            //עובד מותאם - אסור לו לנהוג לפי ערכים 4, 5 בקוד נתון 8 (קוד עובד מותאם)
            enMutaam1 = 1,
            enMutaam2 = 2,
            enMutaam3 = 3,
            enMutaam4 = 4,
            enMutaam5 = 5,
            enMutaam6 = 6,
            enMutaam7 = 7
        }

        public enum enOvedBShlila
        {
            //עובד בשלילה
            enBShlila = 1
        }
        //public enum enKnisaType
        //{
        //    KnisaByDemand = 3
        //}

        //public enum enKodGil
        //{
        //    enTzair = 0,
        //    enKashish = 1,
        //    enKshishon = 2
        //}

      
        public enum enMeafyenSidur46
        {
            //אסור לדווח פעילות
            enAddPeilutNotAllowed = 1
        }
      
        
        public enum enMeafyenSidur54
        {
            //מאפיין 54 - שעון נוכחות
            enAdministrator = 1 //מנהל
        }
        public enum enDynamicFormType
        {
            SpecialSidur = 1, SugSidur = 2, Elements = 3, Parameters = 4, ComponentSidurim = 5, ComponentTypeSidur = 6
        }
        public enum enCharigaValue
        {
            CharigaKnisa = 1,
            CharigaYetiza = 2,
            CharigaKnisaYetiza = 3,
            CharigaAvodaWithoutPremmision = 4
        }

        public enum BatchRequestSource
        {
            ImportProcess = 1,
            ErrorExecutionFromUI = 2,
            ImportProcessForChangesInHR = 3,
            ImportProcessForPremiot = 4
        }

        public enum enSugeyMeadkenShinuyim
        {
            WorkCard = -2,
            MasachChishuvBatch = -3,
            MasachTichkurChishuvOnLine = -4,
            ShnuyimShguimBatchPrem = -5,
            ShnuyimShguimBatchHR = -6,
            ShnuyimShguimBatchSdrn = -7,
            Default=-9
        }
        public enum enVehicleType
        {
            NoTachograph = 1064
        }



        public enum enRechivim
        {
            DakotNochehutLetashlum = 1,
            DakotNehigaChol = 2,
            DakotNihulTnuaChol = 3,
            DakotTafkidChol = 4,
            ShaotHeadrut = 5,
            DakotZikuyChofesh = 7,
            KizuzZchutChofesh = 8,
            DakotTamritzTafkid = 10,
            DakotTamritzNahagut = 11,
            DakotTosefetMeshek = 12,
            DakotMachlifMeshaleach = 13,
            DakotMachlifSadran = 14,
            DakotMachlifPakach = 15,
            DakotMachlifKupai = 16,
            DakotMachlifRakaz = 17,
            DakotNochehutBefoal = 18,
            DakotNosafotNahagut = 19,
            DakotNosafotNihul = 20,
            DakotNosafotTafkid = 21,
            KamutGmulChisachon = 22,
            DakotSikun = 23,
            DakotPremiaShabat = 26,
            DakotPremiaBetochMichsa = 27,
            DakotPremiaVisa = 28,
            DakotPremiaVisaShabat = 29,
            DakotPremiaYomit = 30,
            DakotRegilot = 32,
            DakotShabat = 33,
            DakotNehigaShabat = 35,
            DakotNihulTnuaShabat = 36,
            DakotTafkidShabat = 37,
            SachEshelBoker = 38,
            SachEshelBokerMevkrim = 39,
            SachEshelErev = 40,
            SachEshelErevMevkrim = 41,
            SachEshelTzaharayim = 42,
            SachEshelTzaharayimMevakrim = 43,
            KamutGmulChisachonNosafot = 44,
            SachGmulChisachon = 45,
            SachLina = 47,
            SachLinaKfula = 48,
            SachPitzul = 49,
            SachPitzulKaful = 50,
            SachDakotLepremia = 52,
            ZmanHamaratShaotShabat = 53,
            ZmanLailaEgged = 54,
            ZmanLailaChok = 55,
            YomEvel = 56,
            YomHadracha = 57,
            YomLeloDivuach = 59,
            YomMachla = 60,
            YomMachalaBoded = 61,
            YomMiluim = 62,
            YomAvodaBechul = 63,
            YomTeuna = 64,
            YomShmiratHerayon = 65,
            YomHeadrut = 66,
            YomChofesh = 67,
            YomTipatChalav = 68,
            YomMachalatBenZug = 69,
            YomMachalatHorim = 70,
            YomMachalaYeled = 71,
            YomKursHasavaLekav = 72,
            YomShlichutBeChul = 73,
            YemeyNochehutLeoved = 75,
            Nosafot125 = 76,
            Nosafot150 = 77,
            NosafotShabat = 78,
            DakotMichutzTafkidChol = 80,
            DakotMichutzLamichsaShabat = 81,
            SachNosafotChol = 82,
            SachKizuzim = 83,
            SachTosefetRetzifut = 85,
            KizuzDakotHatchalaGmar = 86,
            //KizuzAruchatBoker = 87,
            ZmanAruchatTzaraim = 88,
            KizuzBevisa = 89,
            KizuzOvedMutam = 90,
            Shaot25 = 91,
            Shaot50 = 92,
            ZmanHalbasha = 93,
            ZmanHashlama = 94,
            ZmanNesia = 95,
            ZmanRetzifutNehiga = 96,
            ZmanRetzifutTafkid = 97,
            Shaot100Letashlum = 100,
            Shaot125Letashlum = 101,
            Shaot150Letashlum = 102,
            Shaot200Letashlum = 103,
            SachShaotNosafot = 105,
            SachKizuzShaotNosafot = 108,
            YemeyAvoda = 109,
            YemeyAvodaLeloMeyuchadim = 110,
            //MichsaChodshit = 111,
            PremiaGrira = 112,
            PremiaMachsenaim = 113,
            PremiaManasim = 114,
            PremiaMeshek = 115,
            PremiaSadranim = 116,
            PremiaPakachim = 117,
            PremiaRakazim = 118,
            Kizuz125 = 119,
            Kizuz150 = 120,
            Kizuz200 = 121,
            Kizuz100 = 122,
            MishmeretShniaBameshek = 125,
            MichsaYomitMechushevet = 126,
            ZmanGrirot = 128,
            DakotMachlifTnua = 129,
            KizuzMachlifTnua = 130,
            ShaotShabat100 = 131,
            ChofeshZchut = 132,
            PremyaRegila = 133,
            PremyaNamlak = 134,
            KizuzKaytanaTafkid = 135,
            KizuzKaytanaShivuk = 136,
            KizuzKaytanaBniaPeruk = 137,
            KizuzKaytanaYeshivatTzevet = 138,
            KizuzKaytanaYamimArukim = 139,
            KizuzKaytanaMeavteach = 140,
            KizuzKaytanaMatzil = 141,
            KizuzNochehut = 142,
            MichsatShaotNosafotTafkidChol = 143,
            //NochehutChodshit = 144,
            KizuzVaadOvdim = 145,
            Nosafot100 = 146,
            KizuzNosafotTafkidChol = 147,
            //KizuzNosafotNahagutChol = 148,
            HashlamaAlCheshbonPremia = 149,
            //DakotNosafotPakach = 150,
            //DakotNosafotSadran = 151,
            //DakotNosafotRakaz = 152,
            //DakotNosafotMeshaleach = 153,
            //DakotNosafotKupai = 154,
            //DakotNosafotMachlifPakach = 155,
            //DakotNosafotMachlifSadran = 156,
            //DakotNosafotMachlifRakaz = 157,
            //DakotNosafotMachlifMeshaleach = 158,
            //DakotNosafotMachlifKupai = 159,
            KizuzNosafotTnuaChol = 160,
            //KizuzNosafotNahgutShabat = 162,
            KizuzNosafotTafkidShabat = 161,
            KizuzNosafotTnuaShabat = 163,
            //DakotMichutzLemichsaPakach = 164,
            //DakotMichutzLemichsaSadsran = 165,
            //DakotMichutzLemichsaRakaz = 166,
            //DakotMichutzLemichsaMeshaleach = 167,
            //DakotMichutzLemichsaKupai = 168,
            //KizuzNosafotPakach = 169,
            //KizuzNosafotSadran = 170,
            //KizuzNosafotRakaz = 171,
            //KizuzNosafotMeshaleach = 172,
            KizuzMachlifPakach = 174,
            KizuzMachlifSadran = 175,
            KizuzMachlifRakaz = 176,
            KizuzMachlifMeshaleach = 177,
            KizuzMachlifKupai = 178,
            SachDakotPakach = 179,
            SachDakotSadran = 180,
            SachDakotRakaz = 181,
            SachDakotMeshalech = 182,
            SachDakotKupai = 183,
            DakotMichutzLamichsaNihulTnua = 184,
            KizuzMishpatChaverim = 185,
            MichutzLamichsaTafkidShabat = 186,
            MichutzLamichsaNihulShabat = 187,
            SachDakotNahagut = 188,
            SachDakotNehigaShishi = 189,
            SachDakotNihulTnua = 190,
            SachDakotNihulShishi = 191,
            SachDakotTafkid = 192,
            SachDakotTafkidShishi = 193,
            NochehutChol = 194,
            NochehutBeshishi = 195,
            NochehutShabat = 196,
            NosafotShishi = 198,
            MichutzLamichsaChol = 200,
            MichutzLamichsaShishi = 201,
            DakotPremiaBeShishi = 202,
            DakotPremiaVisaShishi = 203,
            PremiaLariushum = 204,
            PremiaTichnunTnua = 205,
            PremiaMachsanKatisim = 206,
            MichutzLamichsaTafkidShishi = 207,
            MichutzLamichsaTnuaShishi = 208,
            NochehutPremiaLerishum = 209,
            NochehutPremiaMevakrim = 210,
            NochehutPremiaMeshekMusachim = 211,
            NochehutPremiaMeshekAchsana = 212,
            SachNesiot = 213,
            DakotHistaglut = 214,
            SachKM = 215,
            SachKMVisaLepremia = 216,
            DakotHagdara = 217,
            DakotKisuiTor = 218,
            ShaotChofesh = 219,
            DakotHeadrut = 220,
            DakotChofesh = 221,
            NochehutLepremiaMachsanKartisim = 223,
            PremyatMevakrimBadrachim = 224,
            PremyatMifalYetzur = 225,
            PremyatNehageyTovala = 226,
            PremyatNehageyTenderim = 227,
            PremyatDfus = 228,
            PremyatMisgarot = 229,
            PremyatGifur = 230,
            PremyatMusachRishonLetzyon = 231,
            PremyatTechnayYetzur = 232,
            NochehutLePremyatMifalYetzur = 233,
            NochehutLePremyatNehageyTovala = 234,
            NochehutLePremyatNehageyTenderim = 235,
            NochehutLePremyatDfus = 236,
            NochehutLePremyatAchzaka = 237,
            NochehutLePremyatGifur = 238,
            NochehutLePremyaRishonLetzyon = 239,
            NochehutLePremyaTechnayYetzur = 240,
            NochehutLePremyatPerukVeshiputz = 241,
            PremiyatReshetBitachon = 242,
            PremiyatPeirukVeshiputz = 243,
            DmeyNesiaLeEggedTaavura = 244,
            EshelLeEggedTaavura = 245,
            NochechutLepremyaMenahel = 246,
            PremyaMenahel = 247,
            YomChofeshNoDivuach = 248,
            YomHeadrutNoDivuach = 249,
            SachNosafotTafkidCholVeshishi = 250,
            SachNosafotTnuaCholVeshishi = 251,
            SachNosafotNahagutCholVeshishi = 252,
            MichsatShaotNosafotNihul = 253,
            MichsatShaotNosafotTafkidShabat = 254,
            MichsatShaotNoafotNihulShabat = 255,
            NochehutLepremiaManasim = 256,
            NochehutLepremiaMetachnenTnua = 257,
            NochehutLepremiaSadran = 258,
            NochehutLepremiaRakaz = 259,
            NochehutLepremiaPakach = 260,
            MachalaYomMale = 261,
            MachalaYomChelki = 262,
            HashlamaBenahagut = 263,
            HashlamaBenihulTnua = 264,
            HashlamaBetafkid = 265,
            YomMiluimChelki = 266,
            DakotElementim = 267,
            DakotNesiaLepremia = 268,
            DakotChofeshHeadrut = 269,
            YemeyChofeshHeadrut = 270,
            ZmanLilaSidureyBoker = 271,
            ZmanRetzifutLaylaEgged = 272,
            ZmanRetzifutLaylaChok = 273,
            ZmanRetzifutBoker = 274,
            ZmanRetzifutShabat = 275,
            NochechutLePremiyaMeshekGrira = 276,
            NochechutLePremiyaMeshekKonenutGrira = 277,
            ETPaarBetweenMichsaRegilaAndMuktenet = 278,
            ETMichsaMekoritBeforeHafchata = 279,
            YomMachalatYeledHadHori=280,
            PremyatGriraYadanit=282,
            PremyatMachsanimYadanit = 283,
            PremyatManasYadani = 284,
            PremyatMeshekYadani = 285,
            PremyatSadranYadani = 286,
            PremyatPakachYadani = 287,
            PremyatRakazYadani = 288,
            MachalatYeledImMugbalutOved=289,
            MachalatYeledImMugbalutMaavid = 290,
            NochehutByameyMiluim = 291,
            YomMachalaMisradHaBitachon = 292,
            YemeyNochechut =293,
            NesiaBerechevMuganET = 295,
            ZmanElementZarNahagut = 296,
            NosafotBeNeemanut=281,
            HalbashaTchilatYom=931,
            HalbashaSofYom=932,
            TosefetGririoTchilatSidur=1281,
            TosefetGrirotSofSidur=1282
        }
        public enum TypeCalc
        { Batch = 1, OnLine = 2, Test = 3, Premiya = 4 }


        public enum enMeafyenim
        {
            Meafyen25 = 25,
            HamaratShaot = 32,
            Meafyen40 = 40,
            Meafyen45 = 45,
            Meafyen46 = 46,
            SugHeadrut = 53,
            Meafyen54 = 54,
            EinLeshalemTosLila = 61
        }
        public enum enSugSidur
        {
            //סוג סידור
           SugSidur73 = 73
        }


        public enum enKodIshur
        {
            HashlamatShaotLemutamut = 32,
            TashlumShaotNosafotOvdeyMusac = 34,
            TashlumShaotNosafot = 35,
            HashlamaLeshaot = 38,
            HashlamaLeyom = 39,
            TashlumShaotNosafotShabatOvdeyMusac = 44,
            TashlumShaotNosafotShabat = 45,
            OutMichsa = 48
        }

        public enum enStatusTipul
        {
            HistayemTipul = 1,
            Betipul = 0
        }

       

        public enum enMeafyen
        {
            SectorAvoda = 3,
            SugAvoda = 52
        }



        public enum enDemandType
        {
            InTreatment = 0,
            Treated = 1
        }


        public enum enYechidaIrgunit
        {
            RishumArtzi = 63321,
            RishumBameshek = 98608,
            MachsanKartisimDarom = 80457,
            MachsanKartisimJerusalem = 80556,
            RishumZafonHaifa = 81109
        }

        public enum enIsukOved
        {
            SganMenahel = 15,
            MenahelMachlaka = 23,
            ManasSadran = 420,
            ManasPakch = 422,
            Rasham = 133,
            Poked = 188,
            MenahelMoreNehiga = 194,
            Mafil = 122,
            AchraaiMishmeretMachshev = 123,
            MetaemTikshoret = 124
        }



       

        public enum enKodMaamad
        {
            ChaverSofi = 11,
            MoamadB=12,
            MoamadA=13,
            GimlayTakzivi=15,
            ChozeMeyuchad = 23,
            SachirKavua = 22,
            Sachir12 = 21,
            SachirZmani = 24,
            OvedBechoze = 25,
            OvedChadshKavua = 26,
            Aray = 27,
            GimlaiBechoze = 28,
            Chanich = 29,
            PensyonerBechoze = 38,
            GimlayTaktziviBechoze = 48,
            PensyonerTakziviBechoze = 58,
            Shtachim = 86
        }
        public enum enHrMaamad
        {            
            SalariedEmployee12 = 221,
            PermanentSalariedEmployee = 222            
        }
        public enum enEzor
        {
            Yerushalim = 3
        }

        public enum enSugPremia
        {
            Meshek = 1,
            Machsenaim = 5,
            Grira = 8,
            Manas = 10,
            Rakaz = 30,
            Sadran = 40,
            Pakach = 50,
            MevakrimBadrachim = 100,
            MifalYetzur = 101,
            NehageyTovala = 102,
            NehageyTenderim = 103,
            Dfus = 104,
            MisgarotAchzaka = 105,
            Gifur = 106,
            MusachRishonLetzyon = 107,
            TechnayYetzur = 108,
            ReshetBitachon = 109,
            PerukVeshiputz = 110,
            GriraYadani = 112,
            MachsenaiYdani = 113,
            ManasYadani = 114,
            MeshekYadani = 115,
            SadranYadani = 116,
            PakachYadani = 117,
            RakazYadani = 118
        }

        public enum enHachtamatShaon
        {
            Knisa = 1,
            Yetzia = 2,
            KnisaAndYetzia = 3
        }

        public enum enStatusIsuk
        {
            NotDefined = 0, // משתמשים לא מורדים באגד
            UnitOfficeManagerAndInternal = 1,//מנהלי אגף/מנהלי משרד אגף פנים
            OnlyUnitOfficeManager = 2, //מנהלי אגף/מנהלי משרד אגף בלבד  
            NotManagerUnit = 3,  // לא מנהלי אגף/מנהלי משרד אגף 
            VaadatPnim = 4 // ועדת פנים 
        }

        public enum enSectorZviratZmanForElement
        {
            ElementZviratZman = 5
        }

        public enum enWorkerType
        {
            Garage = 0, //עובדי מוסך
            InternalUnit = 1 // עובדי אגף פנים 
        }
        public enum enMonthlyQuotaForm
        {
            Agafit = 1,
            VaadatPnim = 4
        }



        public enum enGeneralBatchType
        {
            Calculation = 1,
            InputDataAndErrorsFromUI = 2,
            TransferToPayment = 3,
            Approvals = 4,
            InputDataAndErrorsFromInputProcess = 5,
            CreateConstantReprts = 6,
            CreateHeavyReports = 7,
            CreatePremiaExcelInput = 8,
            ExecutePremiaCalculationMacro = 9,
            StorePremiaCalculationOutput = 10,
            DataComparisonImport = 11,
            CalculationForPremiaPopulation = 12,
            YeziratRikuzim = 13,
            SendRikuzimMail = 14,
            RifreshKnisot = 15,
            TransferTekenNehagim = 16,
            HasavatNetuniToOracle = 17,
            RetroSpectSDRN =18,
            LoadNetuneyMeshekForPremyot=19,
            BdikatChufshaRezifa=20,
            ShguimOfPremiyotMusachimNihulTnuaYadaniyot=21,
            ShguimOfHR=22,
            Sleep=23,
            ShguimOfRetroSpaectSdrn = 24,
            IdkunMachalotOvdim=25,
            BdikatYemeyMachala=26,
            ChishuvBankShaotMeshek=27
        }
        public enum enBechishuvSachar
        {
            bsActive = 1
        }
        public enum enBatchExecutionStatus
        {
            Running = 1,
            Succeeded = 2,
            Failed = 3,
            PartialyFinished = 4
        }
        public enum enMeafyenElementim
        {
            Meafyen4 = 4,
            Meafyen23 = 23,
            Meafyen35 = 35
        }
        public enum enMeafyenElementim4
        {
            ElementTime = 1
        }
        public enum enMeafyenElementim23
        {
            ElementTimeNesiaReka = 1            
        }
        public enum enMeafyenElementim35
        {
            ElementTimeNesiaMelea = 1
        }
        public enum enMeasherOMistayeg
        {
            Measher = 1,
            Mistayeg = 0,
            ValueNull = -1
        }

   
        public enum enNetuneyPeilut
        {
            SugPeilut = 0, //0-element, 1-nesiaa
            KisuyTorShaa = 1,
            ShatYezia = 2,
            Teur = 3,
            Kav = 4,
            Sug = 5,
            MisparRechev = 6,
            MisparRishuy = 7,
            Makat = 8,
            DakotHagdara = 9,
            DakotBafoal = 10,
            MisparKnisa = 11,
            Bitul_O_Hosafa = 12,
            KisuyTorDakot = 13,
            ShatYeziaDate = 14
        }
        public enum WorkerViewLevel
        {
            HimSelf,
            UnderManager,
            All 
        }

        public enum enReportType
        {
            ConstantReport = 0,
            HeavyReport = 1,
            Rikuz = 2
        }

        public enum enMouthlyMailsType
        {
            Rikuzim = 0,
            NochechutEmzaChodesh = 1,
            NochechutSofChodesh = 2
        }

        public static void BuildError(Page pPage, string sErr, bool DisplayFunctionName)
        {
            string OriginFunction;
            string StrError = sErr;
            if (DisplayFunctionName)
            {
                StackTrace st = new StackTrace();
                StackFrame sf = st.GetFrame(1);
                OriginFunction = sf.GetMethod().Name;
                StrError = OriginFunction + " :: " + StrError;
            }
            BuildError(pPage, StrError);
        }

        public static void BuildError(Page pPage, string sErr)
        {
            CustomValidator vldCustom = new CustomValidator();
            vldCustom.ErrorMessage = sErr;
            vldCustom.IsValid = false;
            vldCustom.ValidationGroup = "ErrGeneral";
            pPage.Validators.Add(vldCustom);
            if(pPage.Master.FindControl("vldGeneral") != null)
                ((ValidationSummary)pPage.Master.FindControl("vldGeneral")).Visible = true;
            EventLog.WriteEntry(KdsEventLogSource, pPage.Title + ": " + sErr, EventLogEntryType.Error);
        }

        public static void InsertNotSelectedOption(ref DataTable dt,
                                                   int iValue,
                                                   string sText)
        {
            DataRow dr;

            dr = dt.NewRow();
            dr["Description"] = sText;
            dr["CODE"] = iValue;
            dt.Rows.InsertAt(dr, 0);
        }

        public static bool IsNumeric(object Expression)
        {
            bool isNum;
            double retNum;
            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }

        /// <summary>
        /// Fill 'ddl' with a list of 'iMonth' previous months at the month in 'StartDate', in format MM/YYYY 
        /// </summary>
        /// <param name="ddl">the DropDownList field in the page</param>
        /// <param name="iMonth">number of previous month </param>
        public static void LoadDateCombo(DropDownList ddl, int iMonth, DateTime StartDate)
        {
            FillDateCombo(ddl, iMonth, StartDate);
        }
        /// <summary>
        /// Fill 'ddl' with a list of 'iMonth' previous months at the current month , in format MM/YYYY 
        /// </summary>
        /// <param name="ddl">the DropDownList field in the page</param>
        /// <param name="iMonth">number of previous month </param>
        public static void LoadDateCombo(DropDownList ddl, int iMonth)
        {
            FillDateCombo(ddl, iMonth, DateTime.Now);
        }
        private static void FillDateCombo(DropDownList ddl, int iMonth, DateTime StartDate)
        {
            DataTable Dt = new DataTable();
            try
            {
                Dt = FillDateInDataTable(iMonth, StartDate, false);
                ddl.DataSource = Dt;
                ddl.DataTextField = Dt.Columns[0].ToString();
                ddl.DataValueField = Dt.Columns[0].ToString();
                ddl.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable FillDateInDataTable(int NumOfPreviousMonth, DateTime StartDate, bool DisplayAll)
        {
            if (DisplayAll)
                return FillDateInDataTable(NumOfPreviousMonth, StartDate, DisplayAll, "הכל");
            else
                return FillDateInDataTable(NumOfPreviousMonth, StartDate, DisplayAll, "");
        }
        public static DataTable FillDateInDataTable(int NumOfPreviousMonth, DateTime StartDate, bool DisplayAll, string firstText)
        {
            DateTime dCurrentMonth = StartDate;
            DataTable Dt = new DataTable();
            DataTable DtNew = new DataTable();
            DataRow Dr, Drnew;
            String Erech = "";
            try
            {
                Dt.Columns.Add("Month", typeof(String));
                Dt.Columns.Add("Value", typeof(String));
                for (int i = 0; i < NumOfPreviousMonth; i++)
                {
                    Dr = Dt.NewRow();
                    Erech = (dCurrentMonth.Month.ToString() + "/" + dCurrentMonth.Year.ToString()).PadLeft(7, (char)48);
                    Dr["Month"] = Erech;
                    Dr["Value"] = Erech;
                    dCurrentMonth = dCurrentMonth.AddMonths(-1);
                    Dt.Rows.Add(Dr);
                }
                if (DisplayAll)
                {
                    DtNew.Columns.Add("Month", typeof(String));
                    DtNew.Columns.Add("Value", typeof(String));
                    Drnew = DtNew.NewRow();
                    Drnew["Month"] = firstText;

                    //  Drnew["Month"] = "הכל";
                    Drnew["Value"] = "-1";
                    DtNew.Rows.Add(Drnew);
                    foreach (DataRow DrOrigin in Dt.Rows)
                        DtNew.ImportRow(DrOrigin);
                    return DtNew;
                }
                else return Dt;
            }
            catch
            {
                throw;
            }
        }
        /// <summary>
        /// Reallocates an array with a new size, and copies the contents of the old array to the new array.
        /// </summary>
        /// <param name="oldArray">the old array, to be reallocated</param>
        /// <param name="newSize">the new array size</param>
        /// <returns> A new array with the same contents</returns>
        public static System.Array ResizeArray(System.Array oldArray, long newSize)
        {
            long oldSize = oldArray.Length;
            System.Type elementType = oldArray.GetType().GetElementType();
            System.Array newArray = System.Array.CreateInstance(elementType, newSize);
            long preserveLength = System.Math.Min(oldSize, newSize);
            if (preserveLength > 0)
                System.Array.Copy(oldArray, newArray, preserveLength);
            return newArray;
        }
        /// <summary>
        /// Reallocates an array with a new size, and copies the contents of the old array to the new array.
        /// </summary>
        /// <param name="oldArray">the old array, to be reallocated</param>
        /// <param name="newSize">the new array size</param>
        /// <returns> A new array with the same contents</returns>
        public static System.Array ResizeArray(System.Array oldArray, int newSize)
        {
            int oldSize = oldArray.Length;
            System.Type elementType = oldArray.GetType().GetElementType();
            System.Array newArray = System.Array.CreateInstance(elementType, newSize);
            int preserveLength = System.Math.Min(oldSize, newSize);
            if (preserveLength > 0)
                System.Array.Copy(oldArray, newArray, preserveLength);
            return newArray;
        }
        /// <summary>
        /// Convert a column of a datatable to string array .
        /// This function used in Autocomplete action which requires string[] 
        /// </summary>
        public static string[] ConvertDatatableColumnToStringArray(DataTable dt, string ColumnName)
        {
            try
            {
                string[] items = new string[dt.Rows.Count];
                int i = 0;
                foreach (DataRow dr in dt.Rows)
                {
                    items.SetValue(dr[ColumnName].ToString(), i);
                    i++;
                }
                return items;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void CreateUdt()
        {
            COLL_CHISHUV_CHODESH col = new COLL_CHISHUV_CHODESH();
            OBJ_CHISHUV_CHODESH obj = new OBJ_CHISHUV_CHODESH();
            obj.BAKASHA_ID = 43;
            obj.ERECH_RECHIV = 434;
            obj.KOD_RECHIV = 543;
            obj.MISPAR_ISHI = 534534;
            obj.TAARICH = DateTime.Now;
            col.Add(obj);
            col.Add(obj);

        }

        public static int GetIntegerValue(string StringValue)
        {
            int Result = 0;
            Int32.TryParse(StringValue, out Result);
            return Result;
        }

        public static object GetControlValue(Control ctl)
        {
            string StrListValues = string.Empty;
            switch (ctl.GetType().ToString())
            {
                case "System.Web.UI.WebControls.TextBox":
                    return ((TextBox)ctl).Text;
                case "System.Web.UI.WebControls.DropDownList":
                    return ((DropDownList)ctl).SelectedValue;
                case "System.Web.UI.WebControls.CheckBox":
                    return ((CheckBox)ctl).Checked;
                case "System.Web.UI.WebControls.Label":
                    return ((Label)ctl).Text;
                case "KdsLibrary.Controls.KdsCalendar":
                    return ((KdsCalendar)ctl).Text.ToString();
                case "System.Web.UI.WebControls.ListBox":
                    foreach (ListItem Item in ((ListBox)ctl).Items)
                        if (Item.Selected)
                            StrListValues += Item.Value.ToString() + ",";
                    return (StrListValues.Length > 0) ? StrListValues.Remove(StrListValues.Length - 1) : "";
                case "System.Web.UI.WebControls.CheckBoxList":
                    foreach (ListItem Item in ((CheckBoxList)ctl).Items)
                        if (Item.Selected)
                            StrListValues += Item.Value.ToString() + ",";
                    return (StrListValues.Length > 0) ? StrListValues.Remove(StrListValues.Length - 1) : "";
                case "KdsLibrary.Controls.ListBoxExtended":
                    return ((ListBoxExtended)ctl).ListOfValues; 
                case "System.Web.UI.WebControls.RadioButtonList":
                    foreach (ListItem Item in ((RadioButtonList)ctl).Items)
                        if (Item.Selected)
                            StrListValues = Item.Value;
                    return StrListValues;
                case "Saplin.Controls.DropDownCheckBoxes":
                    if (((DropDownCheckBoxes)ctl).SelectAllIsChecked())
                        return "";
                    else
                    {
                        foreach (ListItem Item in ((DropDownCheckBoxes)ctl).Items)
                            if (Item.Selected)
                                StrListValues += Item.Value.ToString() + ",";
                        return (StrListValues.Length > 0) ? StrListValues.Remove(StrListValues.Length - 1) : "";
                    }
                default: return null;
            }

        }
        /// <summary>
        /// Get the value , by reflection of a property of a class defined in Property
        /// when Property contain the full path ( included his namespace ) of his definition 
        /// </summary>
        /// <param name="Property">a string containing the property by reference(for example: DateTime.Now) </param>
        /// <returns></returns>
        public static object GetValueOfProperty(string PropertyName)
        {
            object obj = new object();
            string strType, strProperty;
            int PositionOfLibrary = PropertyName.LastIndexOf(".");
            strType = (PositionOfLibrary > 0) ? PropertyName.Substring(0, PositionOfLibrary) : "System";
            strProperty = (PositionOfLibrary > 0) ? PropertyName.Substring(PositionOfLibrary + 1) : PropertyName;
            Type Tp = Type.GetType(strType);
            if (Tp != null)
            {
                var pi = Tp.GetProperty(strProperty, BindingFlags.Static | BindingFlags.Public);
                if (pi != null)
                {
                    obj = pi.GetValue(Tp, null);
                }
            }
            return obj;
        }
        /// <summary>
        /// Get the value , by reflection of a function of a class defined in function
        /// when function contain the full path ( included his namespace ) of his definition 
        /// </summary>
        /// <param name="MethodName">a string containing the function by reference(for example: DateTime.Now) </param>
        /// <returns></returns>
        public static object GetValueOfMethod(string MethodName)
        {
            string strType, strMethod;
            string strResult;
            int PositionOfLibrary = MethodName.LastIndexOf(".");
            strType = (PositionOfLibrary > 0) ? MethodName.Substring(0, PositionOfLibrary) : "System";
            strMethod = (PositionOfLibrary > 0) ? MethodName.Substring(PositionOfLibrary + 1) : MethodName;
            Type Tp = Type.GetType(strType);
            MethodInfo theMethod = Tp.GetMethod(strMethod);
            strResult = (string)theMethod.Invoke(null, null);
            return strResult;
        }
        public static void LogBakasha(long btchRequest, int iMisparIshi, string sug_hodaa, int kod_yeshut, string teur_hodaa)
        {
            long mispar_siduri = 0;
            LogBakasha(btchRequest, null, null, sug_hodaa, null, kod_yeshut, iMisparIshi, null, null, null, null, null, teur_hodaa, null, out mispar_siduri);
        }

        public static void LogBakasha(long btchRequest, string sug_hodaa, int kod_yeshut, string teur_hodaa)
        {
            long mispar_siduri = 0;
            LogBakasha(btchRequest, null, null, sug_hodaa, null, kod_yeshut, null, null, null, null, null, null, teur_hodaa, null, out mispar_siduri);
        }

        public static void LogBakasha(long btchRequest, int? mispar_siduri, DateTime? taarich_idkun, string sug_hodaa, int? kod_tahalich, int kod_yeshut, int? mispar_ishi, DateTime? taarich, int? mispar_sidur, DateTime? shat_hatchala_sidur, DateTime? shat_yetzia, int? mispar_knisa, string teur_hodaa, int? kod_hodaa, out long final_mispar_siduri)
        {
            final_mispar_siduri = 0;

            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, btchRequest, ParameterDir.pdInput);
                dal.AddParameter("p_taarich_idkun", ParameterType.ntOracleDate, taarich_idkun.HasValue ? taarich_idkun.Value : DateTime.Today, ParameterDir.pdInput);
                dal.AddParameter("p_sug_hodaa", ParameterType.ntOracleChar, sug_hodaa, ParameterDir.pdInput, 100);
                dal.AddParameter("p_kod_tahalich", ParameterType.ntOracleInteger, kod_tahalich.HasValue ? kod_tahalich.Value : -1, ParameterDir.pdInput);
                dal.AddParameter("p_kod_yeshut", ParameterType.ntOracleInteger, kod_yeshut, ParameterDir.pdInput);
                dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, mispar_ishi.HasValue ? mispar_ishi.Value : -1, ParameterDir.pdInput);
                dal.AddParameter("p_taarich", ParameterType.ntOracleDate, taarich.HasValue ? taarich.Value : DateTime.MinValue, ParameterDir.pdInput);
                dal.AddParameter("p_mispar_sidur", ParameterType.ntOracleInteger, mispar_sidur.HasValue ? mispar_sidur.Value : -1, ParameterDir.pdInput);
                dal.AddParameter("p_shat_hatchala_sidur", ParameterType.ntOracleInteger, shat_hatchala_sidur.HasValue ? shat_hatchala_sidur.Value : DateTime.MinValue, ParameterDir.pdInput);
                dal.AddParameter("p_shat_yetzia", ParameterType.ntOracleInteger, shat_yetzia.HasValue ? shat_yetzia.Value : DateTime.MinValue, ParameterDir.pdInput);
                dal.AddParameter("p_mispar_knisa", ParameterType.ntOracleInteger, mispar_knisa.HasValue ? mispar_knisa.Value : -1, ParameterDir.pdInput);
                dal.AddParameter("p_teur_hodaa", ParameterType.ntOracleInteger, teur_hodaa, ParameterDir.pdInput);
                dal.AddParameter("p_kod_hodaa", ParameterType.ntOracleInteger, kod_hodaa.HasValue ? kod_hodaa.Value : -1, ParameterDir.pdInput);
                dal.AddParameter("p_mispar_siduri", ParameterType.ntOracleInteger, mispar_siduri.HasValue ? mispar_siduri.Value : -1, ParameterDir.pdOutput);
                dal.ExecuteSP(KdsLibrary.clGeneral.cProInsLogBakasha);

                final_mispar_siduri = long.Parse(dal.GetValParam("p_mispar_siduri"));
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }



        public static bool UpdatePremiaForOved(string premiaType, int mispar_ishi, DateTime taarich, float dakotPremia, int mispar_ishi_of_meadken)
        {
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_kod_premia", ParameterType.ntOracleInteger, premiaType, ParameterDir.pdInput);
                dal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, mispar_ishi, ParameterDir.pdInput);
                dal.AddParameter("p_taarich", ParameterType.ntOracleDate, taarich, ParameterDir.pdInput);
                dal.AddParameter("p_dakot_premia", ParameterType.ntOracleDecimal, dakotPremia, ParameterDir.pdInput);
                dal.AddParameter("p_mispar_ishi_of_meadken", ParameterType.ntOracleInteger, mispar_ishi_of_meadken, ParameterDir.pdInput);
                dal.ExecuteSP(KdsLibrary.clGeneral.cProUptPremia);

                return true;
            }
            catch (Exception ex)
            {
                LogError(ex);
            }

            return false;
        }


        public static long OpenBatchRequest(enGeneralBatchType batchType,
           string batchDescription, int userID)
        {
            long btchRequest = 0;
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_sug_bakasha", ParameterType.ntOracleInteger, (int)batchType, ParameterDir.pdInput);
                dal.AddParameter("p_teur", ParameterType.ntOracleVarchar, batchDescription, ParameterDir.pdInput, 100);
                dal.AddParameter("p_status", ParameterType.ntOracleInteger, 1, ParameterDir.pdInput);
                dal.AddParameter("p_user_id", ParameterType.ntOracleInteger, userID, ParameterDir.pdInput);
                dal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, "", ParameterDir.pdOutput);
                dal.ExecuteSP(KdsLibrary.clGeneral.cProInsBakasha);
                btchRequest = long.Parse(dal.GetValParam("p_bakasha_id"));
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
            return btchRequest;
        }

        public static void CloseBatchRequest(long btchRequest, KdsLibrary.clGeneral.enBatchExecutionStatus btchStatus)
        {
            try
            {
                clDal dal = new clDal();
                dal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, btchRequest, ParameterDir.pdInput);
                dal.AddParameter("p_status", ParameterType.ntOracleInteger, (int)btchStatus, ParameterDir.pdInput);
                dal.AddParameter("p_huavra_lesachar", ParameterType.ntOracleChar, null, ParameterDir.pdInput);
                dal.AddParameter("p_zman_siyum", ParameterType.ntOracleDate, DateTime.Now, ParameterDir.pdInput, 100);
                dal.AddParameter("p_tar_haavara_lesachar", ParameterType.ntOracleDate, null, ParameterDir.pdInput);
                dal.ExecuteSP(KdsLibrary.clGeneral.cProUpdBakasha);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        public static void InsertBakashaParam(long iRequestId, int iParam, string sErech)
        {
            clDal dal = new clDal();
            try
            {
                dal.AddParameter("p_bakasha_id", ParameterType.ntOracleInt64, iRequestId, ParameterDir.pdInput);
                dal.AddParameter("p_param_id", ParameterType.ntOracleInteger, iParam, ParameterDir.pdInput);
                dal.AddParameter("p_erech", ParameterType.ntOracleVarchar, sErech, ParameterDir.pdInput, 50);

                dal.ExecuteSP(clGeneral.cProInsBakashaParam);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //public static void LogBatchPopulation(int employeeID, DateTime date, DateTime btchExecutionDate,
        // long btchRequest,  clGeneral.enSugeyMeadkenShinuyim enMeadken)
        //{
        //    LogBatchPopulation(employeeID, date, btchExecutionDate, btchRequest,null ,null, enMeadken);
        //}

        public static void LogBatchPopulation(int employeeID, DateTime date, DateTime btchExecutionDate,
           long btchRequest, enGeneralBatchType batchType)
        {
            LogBatchPopulation(employeeID, date, btchExecutionDate, btchRequest, batchType, null);
        }
        public static void LogBatchPopulation(int employeeID, DateTime date, DateTime btchExecutionDate,
            long btchRequest, enGeneralBatchType batchType, string comments)
        {
            bool isLogEnabled = false;
            bool.TryParse(ConfigurationManager.AppSettings["DebugBatchPopulation"],
                out isLogEnabled);
            if (!isLogEnabled) return;
            clDal oDal = new clDal();


            try
            {
                oDal.AddParameter("p_mispar_ishi", ParameterType.ntOracleInteger, employeeID,
                    ParameterDir.pdInput);
                oDal.AddParameter("p_taarich", ParameterType.ntOracleDate, date,
                    ParameterDir.pdInput);
                oDal.AddParameter("p_taarich_ritza", ParameterType.ntOracleDate, btchExecutionDate,
                    ParameterDir.pdInput);
                oDal.AddParameter("p_bakasha_id", ParameterType.ntOracleInteger, btchRequest,
                    ParameterDir.pdInput);
                oDal.AddParameter("p_sug_bakasha", ParameterType.ntOracleInteger, (int)batchType,
                    ParameterDir.pdInput);
                oDal.AddParameter("p_comments", ParameterType.ntOracleVarchar, comments,
                    ParameterDir.pdInput, 50);
                oDal.ExecuteSP(clGeneral.cProInsertDebugBathcPopulation);
            }
            catch (Exception ex)
            {
                LogError(ex);
            }
        }

        public static int GetEmployeeNumberByContext()
        {
            if (System.Web.HttpContext.Current != null)
                return int.Parse(
                    KdsLibrary.Security.LoginUser.GetLoginUser().UserInfo.EmployeeNumber);
            else return -1;
        }

        public static string GetProfilesOfUser()
        {
            string Result = string.Empty;

            if (System.Web.HttpContext.Current != null)
                foreach (UserProfile Profile in LoginUser.GetLoginUser().UserProfiles)
                        Result += Profile.ProfileGroup + ",";
            return (Result == string.Empty) ? string.Empty : Result.Substring(0, Result.Length - 1);
        }

        public static string GetPremiotOfUser()
        {
            string Result = string.Empty;

            if (System.Web.HttpContext.Current != null)
                foreach (UserProfile Profile in LoginUser.GetLoginUser().UserProfiles)
                    if (Profile.ProfileGroup.IndexOf(cKds_Premia) == 0)
                        Result += Profile.ProfileGroup.Substring(cKds_Premia.Length) + ",";
            return (Result == string.Empty) ? string.Empty : Result.Substring(0, Result.Length - 1);
        }

        public static void LogError(Exception exc)
        {
            LogError(exc.ToString());
        }

        public static void LogError(string message)
        {
            LogMessage(message, EventLogEntryType.Error);
        }
        public static void LogMessage(string message, EventLogEntryType entryType, bool DisplayFunctionName)
        {
            string StrError = message;
            if (DisplayFunctionName)
            {
                StackTrace st = new StackTrace();
                StackFrame sf = st.GetFrame(1);
                StrError = sf.GetMethod().Name + " :: " + StrError;
            }
            LogMessage(StrError, entryType);
        }

        public static void LogMessage(string message, EventLogEntryType entryType)
        {
            try
            {
                if (!EventLog.SourceExists(KdsEventLogSource))
                {
                    EventLog.CreateEventSource(KdsEventLogSource, KdsEventLogSource);
                }
                // Create an EventLog instance and assign its source.
                EventLog kdsLog = new EventLog();
                kdsLog.Source = KdsEventLogSource;

                // Write an informational entry to the event log.    
                kdsLog.WriteEntry(message, entryType);
            }
            catch (Exception) { }
        }

        public static void LogMessage(string message, EventLogEntryType entryType, int iEventId)
        {
            try
            {
                if (!EventLog.SourceExists(KdsEventLogSource))
                {
                    EventLog.CreateEventSource(KdsEventLogSource, KdsEventLogSource);
                }
                // Create an EventLog instance and assign its source.
                EventLog kdsLog = new EventLog();
                kdsLog.Source = KdsEventLogSource;

                // Write an informational entry to the event log.    
                kdsLog.WriteEntry(message, entryType, iEventId);
            }
            catch (Exception) { }
        }

        public static DateTime GetDateTimeFromStringHour(string sShaa, DateTime dDate)
        {
            DateTime dTemp;
            string[] arrTime;
            try
            {
                dDate = dDate.Date;
                arrTime = sShaa.Split(char.Parse(":"));
                if (arrTime.Length > 1)
                {                    
                    dTemp = dDate.AddHours(double.Parse(arrTime[0])).AddMinutes(double.Parse(arrTime[1]));
                    if (arrTime.Length > 2)
                    {
                        dTemp = dTemp.AddSeconds(double.Parse(arrTime[2]));
                    }
                }
                else
                {
                    sShaa = sShaa.PadLeft(4, (char)48);
                    dTemp = dDate.AddHours(double.Parse(sShaa.Substring(0, 2))).AddMinutes(double.Parse(sShaa.Substring(2, 2)));
                 }

                return dTemp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DateTime GetEndMonthFromStringMonthYear(int iDay, string sMonthYear)
        {
            DateTime dTemp;
            string[] arrDate;
            try
            {
                arrDate = sMonthYear.Split(char.Parse("/"));
                dTemp = new DateTime(int.Parse(arrDate[1].ToString()), int.Parse(arrDate[0].ToString()), iDay).AddMonths(1).AddDays(-1);
                return dTemp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DateTime GetDateTimeFromStringMonthYear(int iDay, string sMonthYear)
        {
            DateTime dTemp;
            string[] arrDate;
            try
            {
                arrDate = sMonthYear.Split(char.Parse("/"));
                dTemp = new DateTime(int.Parse(arrDate[1].ToString()), int.Parse(arrDate[0].ToString()), iDay);
                return dTemp;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void SetServiceReference(Page CurrentPage, string WebServicePath)
        {
            MasterPage mp = (MasterPage)CurrentPage.Master;
            ScriptManager objScriptManager = new ScriptManager();
            ServiceReference objRefService = new ServiceReference(WebServicePath);
            if (mp != null)
            {
                objScriptManager = (ScriptManager)mp.FindControl("ScriptManagerKds");
            }
            objScriptManager.Services.Add(objRefService);
        }

        public static double TrimDoubleToXDigits(double initialValue, int numDigits)
        {
            if (numDigits < 0)
            {
                throw new Exception("should not be negative");
            }

            double divisor = Math.Pow(10, numDigits);
            // multiply by 100 to get all keeper values left of the decimal.
            double result = initialValue * divisor;
            string testValNoDec = result.ToString();
            // truncate everything to the right of the decimal.
            if (testValNoDec.IndexOf(".") != -1)
            {
                // remove the decimal and everything else.
                testValNoDec = testValNoDec.Substring(0, testValNoDec.IndexOf("."));
            }

            result = Convert.ToDouble(testValNoDec) / divisor;
            return result;
        }



        public static DataTable FilterDataTable(DataTable dtTable, string sFilter)
        {
            DataRow[] dr;
            DataTable dtTableNew = new DataTable();

            dr = dtTable.Select(sFilter);
            dtTableNew = new DataTable();
            dtTableNew = dtTable.Clone();
            foreach (DataRow dRow in dr)
                dtTableNew.ImportRow(dRow);
            
            return dtTableNew;
        }



        public static DataTable GetYamimMeyuchadim()
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();

            try
            {
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(cProGetYamimMeyuchadim, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static DataTable GetSugeyYamimMeyuchadim()
        {
            DataTable dt = new DataTable();
            clDal oDal = new clDal();

            try
            {
                oDal.AddParameter("p_Cur", ParameterType.ntOracleRefCursor, null, ParameterDir.pdOutput);
                oDal.ExecuteSP(cProGetSugeyYamimMeyuchadim, ref dt);
                return dt;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


                   
        public static string AsDomain(string url)
        {
            if (string.IsNullOrEmpty(url))
                return url;

            var match = Regex.Match(url, @"^http[s]?[:/]+[^/]+");
            if (match.Success)
                return match.Captures[0].Value;
            else
                return url;
        }

        
    }
}