@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
@echo 			INSTALLATION OF KDS PROGRAMS TO TEST
@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
pause 
@echo KdsCalcul install in progress ....
@echo copy backup KdsCalcul to old folder ....
@echo off 
copy \\KdsCalc01\KdsCalculTest\*.exe \\KdsCalc01\KdsCalculTest\old\
copy \\KdsCalc01\KdsCalculTest\*.dll \\KdsCalc01\KdsCalculTest\old\
copy \\KdsCalc01\KdsCalculTest\*.config \\KdsCalc01\KdsCalculTest\old\
@echo install KdsCalculTest files to KdsCalc01 ....
@echo off 
copy C:\dev\kds\Main\Kds\KdsCalcul\bin\Release\*.exe  \\KdsCalc01\KdsCalculTest\
copy C:\dev\kds\Main\Kds\KdsCalcul\bin\Release\*.dll  \\KdsCalc01\KdsCalculTest\
@echo KdsCalcul was installed in KdsCalc01 !
@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
@echo KdsService install in progress ....
@echo copy backup KdsService to old folder ....
@echo off 
copy \\KdsCalc01\KdsServiceTest\*.exe \\KdsCalc01\KdsServiceTest\old\
copy \\KdsCalc01\KdsServiceTest\*.dll \\KdsCalc01\KdsServiceTest\old\
copy \\KdsCalc01\KdsServiceTest\*.config \\KdsCalc01\KdsServiceTest\old\
copy \\KdsCalc01\KdsServiceTest\*.xml \\KdsCalc01\KdsServiceTest\old\
@echo install KdsService to KdsCalc01 ....
copy C:\dev\kds\Main\Kds\KdsService\bin\Release\*.exe  \\KdsCalc01\KdsServiceTest\
copy C:\dev\kds\Main\Kds\KdsService\bin\Release\*.dll  \\KdsCalc01\KdsServiceTest\
@echo KdsService was installed in KdsCalc01 !
@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
@echo KdsTaskManager install in progress ....
@echo copy backup KdsTaskManager to old folder ....
@echo off 
copy \\kdstst02\KdsTaskManager\*.exe \\kdstst02\KdsTaskManager\old\
copy \\kdstst02\KdsTaskManager\*.dll \\kdstst02\KdsTaskManager\old\
copy \\kdstst02\KdsTaskManager\*.config \\kdstst02\KdsTaskManager\old\
copy \\kdstst02\KdsTaskManager\*.xml \\kdstst02\KdsTaskManager\old\
@echo install KdsTaskManager to kdstst02 ....
@echo off 
copy C:\dev\kds\Main\Kds\KdsTaskManager\bin\Release\*.exe  \\kdstst02\KdsTaskManager\
copy C:\dev\kds\Main\Kds\KdsTaskManager\bin\Release\*.dll  \\kdstst02\KdsTaskManager\
@echo KdsTaskManager was installed in kdstst02 !
@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
@echo 			END OF INSTALL !
@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
pause 
