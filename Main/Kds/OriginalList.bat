set PathSource=\\kdstst02\wwwroot\KdsKiosk   
set PathDestination= \\KIOSK01\inetpub\wwwroot\Kiosk\
set DestinationFolder=KdsKiosk
@echo on
cls 
set FullDestinationPath=%PathDestination%%DestinationFolder%
set TheDate=%date:~4,2%%date:~7,2%%date:~12,2%
set TheTime=%time:~0,2%%time:~3,2%%time:~6,2%
cd %PathDestination% 
@echo ==================================================================
@echo 	         BACKUP OF THE ACTUAL FOLDER WEBSITE 
@echo ==================================================================
@echo create backup of %FullDestinationPath%
xcopy  %FullDestinationPath%  %FullDestinationPath%_%TheDate%_%TheTime% /i /s
@echo %DestinationFolder%  was copied succefully to %DestinationFolder%_%TheDate%_%TheTime%
@ECHO OFF
@echo ==================================================================
@echo             	              COPY OF THE NEW VERSION 
@echo ==================================================================
SET /P ANSWER=Do you want to copy the new version to %FullDestinationPath%(y/n)?
If "%answer%"=="y" goto continue_line
If "%answer%"=="n" goto end_line 
:continue_line
@ECHO off
@echo ==================================================================
@echo copy the new version to the folder site 
@echo ==================================================================
mkdir  %FullDestinationPath%
cd %FullDestinationPath%
mkdir  %FullDestinationPath%\bin
mkdir  %FullDestinationPath%\Css
mkdir   %FullDestinationPath%\ExternalDlls
mkdir  %FullDestinationPath%\Modules\Ovdim
mkdir  %FullDestinationPath%\Images
mkdir  %FullDestinationPath%\Js
mkdir  %FullDestinationPath%\Modules
mkdir  %FullDestinationPath%\Xml
mkdir  %FullDestinationPath%\EggedFramework
mkdir  %FullDestinationPath%\EggedFramework\Images
mkdir  %FullDestinationPath%\EggedFramework\Resources
mkdir  %FullDestinationPath%\UserControls
mkdir  %FullDestinationPath%\Modules\Reports
mkdir  %FullDestinationPath%\Modules\UserControl
mkdir  %FullDestinationPath%\Modules\WebServices
mkdir  %FullDestinationPath%\Modules\Xslt

copy %PathSource%\ErrorPage.aspx %FullDestinationPath%\ErrorPage.aspx
copy %PathSource%\ErrorPage.aspx.cs %FullDestinationPath%\ErrorPage.aspx.cs
copy %PathSource%\kds.lnk %FullDestinationPath%\kds.lnk
copy %PathSource%\MasterPage.master %FullDestinationPath%\MasterPage.master
copy %PathSource%\MasterPage.master.cs %FullDestinationPath%\MasterPage.master.cs
copy %PathSource%\NotAuthorizedLogin.aspx %FullDestinationPath%\NotAuthorizedLogin.aspx
copy %PathSource%\NotAuthorizedLogin.aspx.cs %FullDestinationPath%\NotAuthorizedLogin.aspx.cs
copy %PathSource%\StyleSheet.css %FullDestinationPath%\StyleSheet.css
copy %PathSource%\Web.sitemap %FullDestinationPath%\Web.sitemap
copy %PathSource%\bin\AjaxControlToolkit.DLL %FullDestinationPath%\bin\AjaxControlToolkit.DLL
copy %PathSource%\bin\App_Code.compiled %FullDestinationPath%\bin\App_Code.compiled
copy %PathSource%\bin\App_Code.dll %FullDestinationPath%\bin\App_Code.dll
copy %PathSource%\bin\App_WebReferences.compiled %FullDestinationPath%\bin\App_WebReferences.compiled
copy %PathSource%\bin\App_WebReferences.dll %FullDestinationPath%\bin\App_WebReferences.dll
copy %PathSource%\bin\KdsBatch.dll %FullDestinationPath%\bin\KdsBatch.dll
copy %PathSource%\bin\KdsBatch.pdb %FullDestinationPath%\bin\KdsBatch.pdb
copy %PathSource%\bin\KdsDataImport.dll %FullDestinationPath%\bin\KdsDataImport.dll
copy %PathSource%\bin\KdsDataImport.pdb %FullDestinationPath%\bin\KdsDataImport.pdb
copy %PathSource%\bin\KdsDataImport.xml %FullDestinationPath%\bin\KdsDataImport.xml
copy %PathSource%\bin\KdsLibrary.dll %FullDestinationPath%\bin\KdsLibrary.dll
copy %PathSource%\bin\KdsLibrary.pdb %FullDestinationPath%\bin\KdsLibrary.pdb
copy %PathSource%\bin\KdsWorkFlow.dll %FullDestinationPath%\bin\KdsWorkFlow.dll
copy %PathSource%\bin\KdsWorkFlow.pdb %FullDestinationPath%\bin\KdsWorkFlow.pdb
copy %PathSource%\bin\Oracle.DataAccess.dll %FullDestinationPath%\bin\Oracle.DataAccess.dll
copy %PathSource%\Css\basic.css %FullDestinationPath%\Css\basic.css
copy %PathSource%\EggedFramework\Egged.WebCustomControls.dll %FullDestinationPath%\EggedFramework\Egged.WebCustomControls.dll
copy %PathSource%\EggedFramework\Images\calendar.gif %FullDestinationPath%\EggedFramework\Images\calendar.gif
copy %PathSource%\EggedFramework\Images\calendar_disabled.gif %FullDestinationPath%\EggedFramework\Images\calendar_disabled.gif
copy %PathSource%\EggedFramework\Resources\Calendar.resources %FullDestinationPath%\EggedFramework\Resources\Calendar.resources
copy %PathSource%\EggedFramework\Resources\TopMenu.resources %FullDestinationPath%\EggedFramework\Resources\TopMenu.resources
copy %PathSource%\ExternalDlls\AjaxControlToolkit.DLL %FullDestinationPath%\ExternalDlls\AjaxControlToolkit.DLL
copy %PathSource%\ExternalDlls\AjaxControlToolkit.dll.old %FullDestinationPath%\ExternalDlls\AjaxControlToolkit.dll.old
copy %PathSource%\ExternalDlls\AjaxControlToolkit.dll.refresh %FullDestinationPath%\ExternalDlls\AjaxControlToolkit.dll.refresh
copy %PathSource%\ExternalDlls\Egged.WebCustomControls.dll %FullDestinationPath%\ExternalDlls\Egged.WebCustomControls.dll
copy %PathSource%\ExternalDlls\Interop.Excel.dll %FullDestinationPath%\ExternalDlls\Interop.Excel.dll
copy %PathSource%\ExternalDlls\Microsoft.ReportViewer.Common.dll %FullDestinationPath%\ExternalDlls\Microsoft.ReportViewer.Common.dll
copy %PathSource%\ExternalDlls\Microsoft.ReportViewer.WebForms.dll %FullDestinationPath%\ExternalDlls\Microsoft.ReportViewer.WebForms.dll
copy %PathSource%\ExternalDlls\Oracle.DataAccess.dll %FullDestinationPath%\ExternalDlls\Oracle.DataAccess.dll
copy %PathSource%\Images\!.png %FullDestinationPath%\Images\!.png
copy %PathSource%\Images\1-10.jpg %FullDestinationPath%\Images\1-10.jpg
copy %PathSource%\Images\11-20.jpg %FullDestinationPath%\Images\11-20.jpg
copy %PathSource%\Images\21-31.jpg %FullDestinationPath%\Images\21-31.jpg
copy %PathSource%\Images\activeV.png %FullDestinationPath%\Images\activeV.png
copy %PathSource%\Images\activeV_dis.png %FullDestinationPath%\Images\activeV_dis.png
copy %PathSource%\Images\activeX.png %FullDestinationPath%\Images\activeX.png
copy %PathSource%\Images\activeX_Dis.png %FullDestinationPath%\Images\activeX_Dis.png
copy %PathSource%\Images\allscreens-cancle-btn.jpg %FullDestinationPath%\Images\allscreens-cancle-btn.jpg
copy %PathSource%\Images\allscreens-cancle-disable.jpg %FullDestinationPath%\Images\allscreens-cancle-disable.jpg
copy %PathSource%\Images\allscreens-checkbox-big-empty.jpg %FullDestinationPath%\Images\allscreens-checkbox-big-empty.jpg
copy %PathSource%\Images\allscreens-checkbox-big.jpg %FullDestinationPath%\Images\allscreens-checkbox-big.jpg
copy %PathSource%\Images\allscreens-checkbox-empty.jpg %FullDestinationPath%\Images\allscreens-checkbox-empty.jpg
copy %PathSource%\Images\allscreens-checkbox.jpg %FullDestinationPath%\Images\allscreens-checkbox.jpg
copy %PathSource%\Images\allscreens-combo-aroow.jpg %FullDestinationPath%\Images\allscreens-combo-aroow.jpg
copy %PathSource%\Images\allscreens-combo-table-arrow.jpg %FullDestinationPath%\Images\allscreens-combo-table-arrow.jpg
copy %PathSource%\Images\allscreens-disable.jpg %FullDestinationPath%\Images\allscreens-disable.jpg
copy "%PathSource%\Images\allscreens-minus btn.jpg" "%FullDestinationPath%\Images\allscreens-minus btn.jpg"
copy %PathSource%\Images\allscreens-minusbtn.jpg %FullDestinationPath%\Images\allscreens-minusbtn.jpg
copy "%PathSource%\Images\allscreens-plus btn.jpg" "%FullDestinationPath%\Images\allscreens-plus btn.jpg"
copy %PathSource%\Images\allscreens-plusbtn.jpg %FullDestinationPath%\Images\allscreens-plusbtn.jpg
copy "%PathSource%\Images\allscreens-questionmark btn.jpg" "%FullDestinationPath%\Images\allscreens-questionmark btn.jpg"
copy %PathSource%\Images\Approval-btn.jpg %FullDestinationPath%\Images\Approval-btn.jpg
copy %PathSource%\Images\ApprovalSign.jpg %FullDestinationPath%\Images\ApprovalSign.jpg
copy %PathSource%\Images\ApprovalSign_big.jpg %FullDestinationPath%\Images\ApprovalSign_big.jpg
copy %PathSource%\Images\ArikooeWait.gif %FullDestinationPath%\Images\ArikooeWait.gif
copy %PathSource%\Images\ArikooeWaitBig.gif %FullDestinationPath%\Images\ArikooeWaitBig.gif
copy %PathSource%\Images\AscSort.gif %FullDestinationPath%\Images\AscSort.gif
copy %PathSource%\Images\btn-approval-checked.jpg %FullDestinationPath%\Images\btn-approval-checked.jpg
copy %PathSource%\Images\btn-approval-regular.jpg %FullDestinationPath%\Images\btn-approval-regular.jpg
copy %PathSource%\Images\btn-close.jpg %FullDestinationPath%\Images\btn-close.jpg
copy %PathSource%\Images\btn-confirm.jpg %FullDestinationPath%\Images\btn-confirm.jpg
copy %PathSource%\Images\btn-end-update.jpg %FullDestinationPath%\Images\btn-end-update.jpg
copy %PathSource%\Images\btn-error.jpg %FullDestinationPath%\Images\btn-error.jpg
copy %PathSource%\Images\btn-make-empty.jpg %FullDestinationPath%\Images\btn-make-empty.jpg
copy %PathSource%\Images\btn-make-empty_disabled.jpg %FullDestinationPath%\Images\btn-make-empty_disabled.jpg
copy %PathSource%\Images\btn-make.jpg %FullDestinationPath%\Images\btn-make.jpg
copy %PathSource%\Images\btn-mistayeg.jpg %FullDestinationPath%\Images\btn-mistayeg.jpg
copy %PathSource%\Images\btn-ok.jpg %FullDestinationPath%\Images\btn-ok.jpg
copy %PathSource%\Images\btn-oved-next.png %FullDestinationPath%\Images\btn-oved-next.png
copy %PathSource%\Images\btn-oved-next2.jpg %FullDestinationPath%\Images\btn-oved-next2.jpg
copy %PathSource%\Images\btn-oved-prev-empty-disabled.jpg %FullDestinationPath%\Images\btn-oved-prev-empty-disabled.jpg
copy %PathSource%\Images\btn-oved-prev-empty.jpg %FullDestinationPath%\Images\btn-oved-prev-empty.jpg
copy %PathSource%\Images\btn-oved-prev.jpg %FullDestinationPath%\Images\btn-oved-prev.jpg
copy %PathSource%\Images\btn-run-empty-disable.jpg %FullDestinationPath%\Images\btn-run-empty-disable.jpg
copy %PathSource%\Images\btn-run-empty.jpg %FullDestinationPath%\Images\btn-run-empty.jpg
copy %PathSource%\Images\btn-run.jpg %FullDestinationPath%\Images\btn-run.jpg
copy %PathSource%\Images\btn-runall-empty-disable.jpg %FullDestinationPath%\Images\btn-runall-empty-disable.jpg
copy %PathSource%\Images\btn-runall-empty.jpg %FullDestinationPath%\Images\btn-runall-empty.jpg
copy %PathSource%\Images\btn-runall.jpg %FullDestinationPath%\Images\btn-runall.jpg
copy %PathSource%\Images\btn-search-empty.jpg %FullDestinationPath%\Images\btn-search-empty.jpg
copy %PathSource%\Images\btn-search-empty_disabled.jpg %FullDestinationPath%\Images\btn-search-empty_disabled.jpg
copy %PathSource%\Images\btn-search.jpg %FullDestinationPath%\Images\btn-search.jpg
copy %PathSource%\Images\btn-update.jpg %FullDestinationPath%\Images\btn-update.jpg
copy %PathSource%\Images\btn-valid.jpg %FullDestinationPath%\Images\btn-valid.jpg
copy %PathSource%\Images\btn_error.jpg %FullDestinationPath%\Images\btn_error.jpg
copy %PathSource%\Images\B_AddMap_reg.png %FullDestinationPath%\Images\B_AddMap_reg.png
copy %PathSource%\Images\B_AddSpecial_reg.png %FullDestinationPath%\Images\B_AddSpecial_reg.png
copy %PathSource%\Images\B_approve.png %FullDestinationPath%\Images\B_approve.png
copy %PathSource%\Images\B_approve_Dis.png %FullDestinationPath%\Images\B_approve_Dis.png
copy %PathSource%\Images\B_approve_press.png %FullDestinationPath%\Images\B_approve_press.png
copy %PathSource%\Images\B_calander.png %FullDestinationPath%\Images\B_calander.png
copy %PathSource%\Images\B_calendar_press.png %FullDestinationPath%\Images\B_calendar_press.png
copy %PathSource%\Images\B_callculated_Dis.png %FullDestinationPath%\Images\B_callculated_Dis.png
copy %PathSource%\Images\B_callculated_reg.png %FullDestinationPath%\Images\B_callculated_reg.png
copy %PathSource%\Images\B_cancelError_Dis.png %FullDestinationPath%\Images\B_cancelError_Dis.png
copy %PathSource%\Images\B_cancelError_press.png %FullDestinationPath%\Images\B_cancelError_press.png
copy %PathSource%\Images\B_cancelError_reg.png %FullDestinationPath%\Images\B_cancelError_reg.png
copy %PathSource%\Images\B_clocks_reg.png %FullDestinationPath%\Images\B_clocks_reg.png
copy %PathSource%\Images\B_closeWin_Dis.png %FullDestinationPath%\Images\B_closeWin_Dis.png
copy %PathSource%\Images\B_closeWin_press.png %FullDestinationPath%\Images\B_closeWin_press.png
copy %PathSource%\Images\B_closeWin_reg.png %FullDestinationPath%\Images\B_closeWin_reg.png
copy %PathSource%\Images\B_close_reg.png %FullDestinationPath%\Images\B_close_reg.png
copy %PathSource%\Images\B_Disapprove.png %FullDestinationPath%\Images\B_Disapprove.png
copy %PathSource%\Images\B_Disapprove_Dis.png %FullDestinationPath%\Images\B_Disapprove_Dis.png
copy %PathSource%\Images\B_Disapprove_Press.png %FullDestinationPath%\Images\B_Disapprove_Press.png
copy %PathSource%\Images\B_left.png %FullDestinationPath%\Images\B_left.png
copy %PathSource%\Images\B_Mid.png %FullDestinationPath%\Images\B_Mid.png
copy %PathSource%\Images\B_Move2Approve_Dis.png %FullDestinationPath%\Images\B_Move2Approve_Dis.png
copy %PathSource%\Images\B_Move2Approve_press.png %FullDestinationPath%\Images\B_Move2Approve_press.png
copy %PathSource%\Images\B_Move2Approve_reg.png %FullDestinationPath%\Images\B_Move2Approve_reg.png
copy %PathSource%\Images\B_nextDay.png %FullDestinationPath%\Images\B_nextDay.png
copy %PathSource%\Images\B_nextDay_Dis.png %FullDestinationPath%\Images\B_nextDay_Dis.png
copy %PathSource%\Images\B_next_error.png %FullDestinationPath%\Images\B_next_error.png
copy %PathSource%\Images\B_next_error_Dis.png %FullDestinationPath%\Images\B_next_error_Dis.png
copy %PathSource%\Images\B_prevDay.png %FullDestinationPath%\Images\B_prevDay.png
copy %PathSource%\Images\B_prevDay_Dis.png %FullDestinationPath%\Images\B_prevDay_Dis.png
copy %PathSource%\Images\B_print.png %FullDestinationPath%\Images\B_print.png
copy %PathSource%\Images\B_printDis.png %FullDestinationPath%\Images\B_printDis.png
copy %PathSource%\Images\B_right.png %FullDestinationPath%\Images\B_right.png
copy %PathSource%\Images\B_update.png %FullDestinationPath%\Images\B_update.png
copy %PathSource%\Images\B_updateLong_Dis.png %FullDestinationPath%\Images\B_updateLong_Dis.png
copy %PathSource%\Images\B_updateLong_press.png %FullDestinationPath%\Images\B_updateLong_press.png
copy %PathSource%\Images\B_updateLong_reg.png %FullDestinationPath%\Images\B_updateLong_reg.png
copy %PathSource%\Images\B_update_Dis.png %FullDestinationPath%\Images\B_update_Dis.png
copy %PathSource%\Images\calendar.gif %FullDestinationPath%\Images\calendar.gif
copy %PathSource%\Images\calendar_disabled.gif %FullDestinationPath%\Images\calendar_disabled.gif
copy %PathSource%\Images\checkbox.png %FullDestinationPath%\Images\checkbox.png
copy %PathSource%\Images\closeArrow.png %FullDestinationPath%\Images\closeArrow.png
copy %PathSource%\Images\closeArrow_red.png %FullDestinationPath%\Images\closeArrow_red.png
copy %PathSource%\Images\collapse_blue.jpg %FullDestinationPath%\Images\collapse_blue.jpg
copy %PathSource%\Images\collapse_blue_big.jpg %FullDestinationPath%\Images\collapse_blue_big.jpg
copy %PathSource%\Images\collapse_blue_small.jpg %FullDestinationPath%\Images\collapse_blue_small.jpg
copy %PathSource%\Images\date.jpg %FullDestinationPath%\Images\date.jpg
copy %PathSource%\Images\delete.png %FullDestinationPath%\Images\delete.png
copy %PathSource%\Images\DescSort.gif %FullDestinationPath%\Images\DescSort.gif
copy %PathSource%\Images\disabled.jpg %FullDestinationPath%\Images\disabled.jpg
copy %PathSource%\Images\dividingLine.jpg %FullDestinationPath%\Images\dividingLine.jpg
copy %PathSource%\Images\down.png %FullDestinationPath%\Images\down.png
copy %PathSource%\Images\down_Dis.png %FullDestinationPath%\Images\down_Dis.png
copy %PathSource%\Images\Eggedprogress.gif %FullDestinationPath%\Images\Eggedprogress.gif
copy %PathSource%\Images\enabled.jpg %FullDestinationPath%\Images\enabled.jpg
copy %PathSource%\Images\error.JPG %FullDestinationPath%\Images\error.JPG
copy "%PathSource%\Images\ErrorSign - Copy.jpg" "%FullDestinationPath%\Images\ErrorSign - Copy.jpg"
copy %PathSource%\Images\ErrorSign.jpg %FullDestinationPath%\Images\ErrorSign.jpg
copy %PathSource%\Images\ErrorSign_big.jpg %FullDestinationPath%\Images\ErrorSign_big.jpg
copy %PathSource%\Images\expand_blue.jpg %FullDestinationPath%\Images\expand_blue.jpg
copy %PathSource%\Images\expand_blue_big.jpg %FullDestinationPath%\Images\expand_blue_big.jpg
copy %PathSource%\Images\green_down_2.jpg %FullDestinationPath%\Images\green_down_2.jpg
copy %PathSource%\Images\green_down_2_big.jpg %FullDestinationPath%\Images\green_down_2_big.jpg
copy %PathSource%\Images\green_down_old.jpg %FullDestinationPath%\Images\green_down_old.jpg
copy %PathSource%\Images\green_up_2.jpg %FullDestinationPath%\Images\green_up_2.jpg
copy %PathSource%\Images\green_up_2_big.jpg %FullDestinationPath%\Images\green_up_2_big.jpg
copy %PathSource%\Images\green_up_old.jpg %FullDestinationPath%\Images\green_up_old.jpg
copy %PathSource%\Images\grey.jpg %FullDestinationPath%\Images\grey.jpg
copy %PathSource%\Images\head.jpg %FullDestinationPath%\Images\head.jpg
copy %PathSource%\Images\headline-bar.jpg %FullDestinationPath%\Images\headline-bar.jpg
copy %PathSource%\Images\icon-excel.jpg %FullDestinationPath%\Images\icon-excel.jpg
copy %PathSource%\Images\icon-exit.jpg %FullDestinationPath%\Images\icon-exit.jpg
copy %PathSource%\Images\icon-help.jpg %FullDestinationPath%\Images\icon-help.jpg
copy %PathSource%\Images\icon-home.jpg %FullDestinationPath%\Images\icon-home.jpg
copy %PathSource%\Images\icon-pdf.jpg %FullDestinationPath%\Images\icon-pdf.jpg
copy %PathSource%\Images\icon-print.jpg %FullDestinationPath%\Images\icon-print.jpg
copy %PathSource%\Images\kds.ico %FullDestinationPath%\Images\kds.ico
copy %PathSource%\Images\KnisotS.png %FullDestinationPath%\Images\KnisotS.png
copy %PathSource%\Images\KnisotS_Dis.png %FullDestinationPath%\Images\KnisotS_Dis.png
copy %PathSource%\Images\menu-bar.jpg %FullDestinationPath%\Images\menu-bar.jpg
copy %PathSource%\Images\menu-btn-off.jpg %FullDestinationPath%\Images\menu-btn-off.jpg
copy %PathSource%\Images\menu-btn-on.jpg %FullDestinationPath%\Images\menu-btn-on.jpg
copy %PathSource%\Images\openArrow.png %FullDestinationPath%\Images\openArrow.png
copy %PathSource%\Images\openArrow_red.png %FullDestinationPath%\Images\openArrow_red.png
copy %PathSource%\Images\opening-btn.png %FullDestinationPath%\Images\opening-btn.png
copy %PathSource%\Images\opening-date.jpg %FullDestinationPath%\Images\opening-date.jpg
copy %PathSource%\Images\opening-head.jpg %FullDestinationPath%\Images\opening-head.jpg
copy %PathSource%\Images\opening-quickmenu.jpg %FullDestinationPath%\Images\opening-quickmenu.jpg
copy %PathSource%\Images\opening-system-mes.jpg %FullDestinationPath%\Images\opening-system-mes.jpg
copy %PathSource%\Images\oved-next.jpg %FullDestinationPath%\Images\oved-next.jpg
copy %PathSource%\Images\oved-next.png %FullDestinationPath%\Images\oved-next.png
copy %PathSource%\Images\oved-prev.jpg %FullDestinationPath%\Images\oved-prev.jpg
copy "%PathSource%\Images\plus - Copy.jpg" "%FullDestinationPath%\Images\plus - Copy.jpg"
copy %PathSource%\Images\plus-disable.jpg %FullDestinationPath%\Images\plus-disable.jpg
copy %PathSource%\Images\plus.jpg %FullDestinationPath%\Images\plus.jpg
copy %PathSource%\Images\Plus.png %FullDestinationPath%\Images\Plus.png
copy %PathSource%\Images\Plus_Dis.png %FullDestinationPath%\Images\Plus_Dis.png
copy %PathSource%\Images\pressed.jpg %FullDestinationPath%\Images\pressed.jpg
copy %PathSource%\Images\progress.gif %FullDestinationPath%\Images\progress.gif
copy %PathSource%\Images\Qmark.png %FullDestinationPath%\Images\Qmark.png
copy %PathSource%\Images\Qmark_dis.png %FullDestinationPath%\Images\Qmark_dis.png
copy %PathSource%\Images\questionbtn.jpg %FullDestinationPath%\Images\questionbtn.jpg
copy %PathSource%\Images\red_down_2.jpg %FullDestinationPath%\Images\red_down_2.jpg
copy %PathSource%\Images\red_down_2_big.jpg %FullDestinationPath%\Images\red_down_2_big.jpg
copy %PathSource%\Images\red_up_2.jpg %FullDestinationPath%\Images\red_up_2.jpg
copy %PathSource%\Images\red_up_2_big.jpg %FullDestinationPath%\Images\red_up_2_big.jpg
copy %PathSource%\Images\sergeiwait.gif %FullDestinationPath%\Images\sergeiwait.gif
copy %PathSource%\Images\showB.png %FullDestinationPath%\Images\showB.png
copy %PathSource%\Images\showB.png.old %FullDestinationPath%\Images\showB.png.old
copy %PathSource%\Images\showB_Dis.png %FullDestinationPath%\Images\showB_Dis.png
copy %PathSource%\Images\showB_Dis.png.old %FullDestinationPath%\Images\showB_Dis.png.old
copy %PathSource%\Images\space-menu.png %FullDestinationPath%\Images\space-menu.png
copy %PathSource%\Images\spinner.gif %FullDestinationPath%\Images\spinner.gif
copy %PathSource%\Images\spinnerSmall.gif %FullDestinationPath%\Images\spinnerSmall.gif
copy %PathSource%\Images\tabMinus.png %FullDestinationPath%\Images\tabMinus.png
copy %PathSource%\Images\tabPlus.png %FullDestinationPath%\Images\tabPlus.png
copy %PathSource%\Images\Title_activeV.png %FullDestinationPath%\Images\Title_activeV.png
copy %PathSource%\Images\Title_activeV_Dis.png %FullDestinationPath%\Images\Title_activeV_Dis.png
copy %PathSource%\Images\unpressed.jpg %FullDestinationPath%\Images\unpressed.jpg
copy %PathSource%\Images\up.png %FullDestinationPath%\Images\up.png
copy %PathSource%\Images\Up_Dis.png %FullDestinationPath%\Images\Up_Dis.png
copy %PathSource%\Images\white.jpg %FullDestinationPath%\Images\white.jpg
copy %PathSource%\Images\x.png %FullDestinationPath%\Images\x.png
copy %PathSource%\Js\basic.js %FullDestinationPath%\Js\basic.js
copy %PathSource%\Js\GeneralFunction.js %FullDestinationPath%\Js\GeneralFunction.js
copy %PathSource%\Js\HosafatPeiluyot.js %FullDestinationPath%\Js\HosafatPeiluyot.js
copy %PathSource%\Js\jquery.fixedheader.js %FullDestinationPath%\Js\jquery.fixedheader.js
copy %PathSource%\Js\jquery.js %FullDestinationPath%\Js\jquery.js
copy %PathSource%\Js\jquery.simplemodal.js %FullDestinationPath%\Js\jquery.simplemodal.js
copy %PathSource%\Js\jqueryheader.js %FullDestinationPath%\Js\jqueryheader.js
copy %PathSource%\Js\jqueryNew.js %FullDestinationPath%\Js\jqueryNew.js
copy %PathSource%\Js\ListBoxExtended.js %FullDestinationPath%\Js\ListBoxExtended.js
copy %PathSource%\Js\NetuneyOved.js %FullDestinationPath%\Js\NetuneyOved.js
copy %PathSource%\Js\Sidurim.js %FullDestinationPath%\Js\Sidurim.js
copy %PathSource%\Js\String.js %FullDestinationPath%\Js\String.js
copy %PathSource%\Js\SystemManager.js %FullDestinationPath%\Js\SystemManager.js
copy %PathSource%\Js\WorkCard.js %FullDestinationPath%\Js\WorkCard.js

copy %PathSource%\Modules\Ovdim\EmployeeCards.aspx %FullDestinationPath%\Modules\Ovdim\EmployeeCards.aspx
copy %PathSource%\Modules\Ovdim\EmployeeCards.aspx.cs %FullDestinationPath%\Modules\Ovdim\EmployeeCards.aspx.cs
copy %PathSource%\Modules\Ovdim\WorkCard.aspx %FullDestinationPath%\Modules\Ovdim\WorkCard.aspx
copy "%PathSource%\Modules\Ovdim\WorkCard.aspx - Copy.cs" "%FullDestinationPath%\Modules\Ovdim\WorkCard.aspx - Copy.cs"
copy %PathSource%\Modules\Ovdim\WorkCard.aspx.cs %FullDestinationPath%\Modules\Ovdim\WorkCard.aspx.cs
copy %PathSource%\Modules\Ovdim\WorkCardErrors.aspx %FullDestinationPath%\Modules\Ovdim\WorkCardErrors.aspx
copy %PathSource%\Modules\Ovdim\WorkCardErrors.aspx.cs %FullDestinationPath%\Modules\Ovdim\WorkCardErrors.aspx.cs
copy %PathSource%\Modules\Reports\ShowReport.aspx %FullDestinationPath%\Modules\Reports\ShowReport.aspx
copy %PathSource%\Modules\Reports\ShowReport.aspx.cs %FullDestinationPath%\Modules\Reports\ShowReport.aspx.cs
copy %PathSource%\Modules\UserControl\ucSidurim.ascx %FullDestinationPath%\Modules\UserControl\ucSidurim.ascx
copy %PathSource%\Modules\UserControl\ucSidurim.ascx.cs %FullDestinationPath%\Modules\UserControl\ucSidurim.ascx.cs
copy %PathSource%\Modules\WebServices\wsBatch.asmx %FullDestinationPath%\Modules\WebServices\wsBatch.asmx
copy %PathSource%\Modules\WebServices\wsGeneral.asmx %FullDestinationPath%\Modules\WebServices\wsGeneral.asmx
copy %PathSource%\Modules\Xslt\XSLErrors.xslt %FullDestinationPath%\Modules\Xslt\XSLErrors.xslt
copy %PathSource%\UserControls\GridViewPager.ascx %FullDestinationPath%\UserControls\GridViewPager.ascx
copy %PathSource%\UserControls\GridViewPager.ascx.cs %FullDestinationPath%\UserControls\GridViewPager.ascx.cs
copy %PathSource%\Xml\DynamicsReports.xml %FullDestinationPath%\Xml\DynamicsReports.xml
copy %PathSource%\Xml\Sysman.xml %FullDestinationPath%\Xml\Sysman.xml
pause
:end_line 
exit
