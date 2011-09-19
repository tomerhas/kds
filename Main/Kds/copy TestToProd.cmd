@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
@echo 			INSTALLATION OF KDS PROGRAMS TO PRODUCTION
@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
pause 
@echo KdsCalcul install in progress ....
@echo copy backup KdsCalcul to old folder ....
@echo off 
copy \\kdscalc01\KdsCalcul\*.exe \\kdscalc01\KdsCalcul\old\
copy \\kdscalc01\KdsCalcul\*.dll \\kdscalc01\KdsCalcul\old\
copy \\kdscalc01\KdsCalcul\*.config \\kdscalc01\KdsCalcul\old\
@echo install KdsCalcul files to kdscalc01 ....
@echo off 
copy \\kdstst02\KdsCalcul\*.exe  \\kdscalc01\KdsCalcul\
copy \\kdstst02\KdsCalcul\*.dll  \\kdscalc01\KdsCalcul\
@echo KdsCalcul was installed in kdscalc01 !
@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
@echo KdsService install in progress ....
@echo copy backup KdsService to old folder ....
@echo off 
copy \\kdscalc01\KdsService\*.exe \\kdscalc01\KdsService\old\
copy \\kdscalc01\KdsService\*.dll \\kdscalc01\KdsService\old\
copy \\kdscalc01\KdsService\*.config \\kdscalc01\KdsService\old\
copy \\kdscalc01\KdsService\*.xml \\kdscalc01\KdsService\old\
@echo install KdsService to kdscalc01 ....
copy \\kdstst02\KdsService\*.exe  \\kdscalc01\KdsService\
copy \\kdstst02\KdsService\*.dll  \\kdscalc01\KdsService\
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
