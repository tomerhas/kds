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
@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
@echo KdsTaskManager install in progress ....
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
@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
@echo 			END OF INSTALL !
@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
pause 
