@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
@echo 			INSTALLATION OF KDS PROGRAMS TO PRODUCTION
@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
pause 
@echo KdsCalcul install in progress ....
@echo copy backup KdsCalcul to old folder ....
@echo off 
copy \\kdscalc\KdsCalculProd\*.exe \\kdscalc\KdsCalculProd\old\
copy \\kdscalc\KdsCalculProd\*.dll \\kdscalc\KdsCalculProd\old\
copy \\kdscalc\KdsCalculProd\*.config \\kdscalc\KdsCalculProd\old\
@echo install KdsCalcul files to kdstst ....
@echo off 
copy \\kdscalc\KdsCalculTest\*.exe  \\kdscalc\KdsCalculProd\
copy \\kdscalc\KdsCalculTest\*.dll  \\kdscalc\KdsCalculProd\

del \\kdscalc\KdsCalculProd\KdsCalculProd.exe

rename \\kdscalc\KdsCalculProd\KdsCalculTest.exe  KdsCalculProd.exe 
rename \\kdscalc\KdsCalculProd\KdsCalculTest.exe.config  KdsCalculProd.exe.config 
@echo KdsCalcul was installed in kdstst !
@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
@echo KdsService install in progress ....
@echo copy backup KdsService to old folder ....
@echo off 
copy \\kdscalc\KdsServiceProd\*.exe \\kdscalc\KdsServiceProd\old\
copy \\kdscalc\KdsServiceProd\*.dll \\kdscalc\KdsServiceProd\old\
copy \\kdscalc\KdsServiceProd\*.config \\kdscalc\KdsServiceProd\old\
copy \\kdscalc\KdsServiceProd\*.xml \\kdscalc\KdsServiceProd\old\
@echo install KdsService to kdstst ....
copy \\kdscalc\KdsServiceTest\*.exe  \\kdscalc\KdsServiceProd\
copy \\kdscalc\KdsServiceTest\*.dll  \\kdscalc\KdsServiceProd\
@echo KdsService was installed in kdstst !

@echo backup Local KdsService of kdsappl02 ....
copy \\kdsappl02\KdsService\*.exe  \\kdsappl02\KdsService\old\
copy \\kdsappl02\KdsService\*.dll  \\kdsappl02\KdsService\old\
@echo install Local KdsService to kdsappl02 ....
copy \\kdscalc\KdsServiceProd\*.exe  \\kdsappl02\KdsService\
copy \\kdscalc\KdsServiceProd\*.dll  \\kdsappl02\KdsService\
@echo Local KdsService was installed in kdsappl02 !

@echo backup Local KdsService of kdsappl01 ....
copy \\kdsappl01\KdsService\*.exe  \\kdsappl01\KdsService\old\
copy \\kdsappl01\KdsService\*.dll  \\kdsappl01\KdsService\old\
@echo install Local KdsService to kdsappl01 ....
copy \\kdscalc\KdsServiceProd\*.exe  \\kdsappl01\KdsService\
copy \\kdscalc\KdsServiceProd\*.dll  \\kdsappl01\KdsService\
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
