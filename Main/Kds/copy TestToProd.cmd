@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
@echo 			INSTALLATION OF KDS PROGRAMS TO PRODUCTION
@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
pause 
@echo KdsCalcul install in progress ....
@echo copy backup KdsCalcul to old folder ....
@echo off 
copy \\kdscalc01\KdsCalculProd\*.exe \\kdscalc01\KdsCalculProd\old\
copy \\kdscalc01\KdsCalculProd\*.dll \\kdscalc01\KdsCalculProd\old\
copy \\kdscalc01\KdsCalculProd\*.config \\kdscalc01\KdsCalculProd\old\
@echo install KdsCalcul files to kdscalc01 ....
@echo off 
copy \\kdscalc01\KdsCalculTest\*.exe  \\kdscalc01\KdsCalculProd\
copy \\kdscalc01\KdsCalculTest\*.dll  \\kdscalc01\KdsCalculProd\

rename \\KdsCalc01\KdsCalculProd\KdsCalculTest.exe  KdsCalculProd.exe 
rename \\KdsCalc01\KdsCalculProd\KdsCalculTest.exe.config  KdsCalculProd.exe.config 
@echo KdsCalcul was installed in kdscalc01 !
@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
@echo KdsService install in progress ....
@echo copy backup KdsService to old folder ....
@echo off 
copy \\kdscalc01\KdsServiceProd\*.exe \\kdscalc01\KdsServiceProd\old\
copy \\kdscalc01\KdsServiceProd\*.dll \\kdscalc01\KdsServiceProd\old\
copy \\kdscalc01\KdsServiceProd\*.config \\kdscalc01\KdsServiceProd\old\
copy \\kdscalc01\KdsServiceProd\*.xml \\kdscalc01\KdsServiceProd\old\
@echo install KdsService to kdscalc01 ....
copy \\kdscalc01\KdsServiceTest\*.exe  \\kdscalc01\KdsServiceProd\
copy \\kdscalc01\KdsServiceTest\*.dll  \\kdscalc01\KdsServiceProd\
@echo KdsService was installed in kdscalc01 !

@echo backup Local KdsService of kdsappl02 ....
copy \\kdsappl02\KdsService\*.exe  \\kdsappl02\KdsService\old\
copy \\kdsappl02\KdsService\*.dll  \\kdsappl02\KdsService\old\
@echo install Local KdsService to kdsappl02 ....
copy \\kdscalc01\KdsServiceProd\*.exe  \\kdsappl02\KdsService\
copy \\kdscalc01\KdsServiceProd\*.dll  \\kdsappl02\KdsService\
@echo Local KdsService was installed in kdsappl02 !

@echo backup Local KdsService of kdsappl01 ....
copy \\kdsappl01\KdsService\*.exe  \\kdsappl01\KdsService\old\
copy \\kdsappl01\KdsService\*.dll  \\kdsappl01\KdsService\old\
@echo install Local KdsService to kdsappl01 ....
copy \\kdscalc01\KdsServiceProd\*.exe  \\kdsappl01\KdsService\
copy \\kdscalc01\KdsServiceProd\*.dll  \\kdsappl01\KdsService\
@echo Local KdsService was installed in kdsappl01 !
@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
@echo KdsTaskManager install in kdsappl02 progress ....
@echo copy backup KdsTaskManager to old folder ....
@echo off 
copy \\kdsappl02\KdsTaskManager\*.exe \\kdsappl02\KdsTaskManager\old\
copy \\kdsappl02\KdsTaskManager\*.dll \\kdsappl02\KdsTaskManager\old\
copy \\kdsappl02\KdsTaskManager\*.config \\kdsappl02\KdsTaskManager\old\
copy \\kdsappl02\KdsTaskManager\*.xml \\kdsappl02\KdsTaskManager\old\
@echo install KdsTaskManager to kdsappl02 ....
@echo off 
copy \\kdstst02\KdsTaskManager\*.exe  \\kdsappl02\KdsTaskManager\
copy \\kdstst02\KdsTaskManager\*.dll  \\kdsappl02\KdsTaskManager\
@echo KdsTaskManager was installed in kdsappl02 !



@echo KdsTaskManager install in kdsappl01 progress ....
@echo copy backup KdsTaskManager to old folder ....
@echo off 
copy \\kdsappl01\KdsTaskManager\*.exe \\kdsappl01\KdsTaskManager\old\
copy \\kdsappl01\KdsTaskManager\*.dll \\kdsappl01\KdsTaskManager\old\
copy \\kdsappl01\KdsTaskManager\*.config \\kdsappl01\KdsTaskManager\old\
copy \\kdsappl01\KdsTaskManager\*.xml \\kdsappl01\KdsTaskManager\old\
@echo install KdsTaskManager to kdsappl02 ....
@echo off 
copy \\kdstst02\KdsTaskManager\*.exe  \\kdsappl01\KdsTaskManager\
copy \\kdstst02\KdsTaskManager\*.dll  \\kdsappl01\KdsTaskManager\
@echo KdsTaskManager was installed in kdsappl01 !
@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
@echo 			END OF INSTALL !
@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
pause 
