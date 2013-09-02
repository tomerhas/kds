@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
@echo 			INSTALLATION OF KDS PROGRAMS TO TEST
@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
pause 
@echo KdsCalcul install in progress ....
@echo copy backup KdsCalcul to old folder ....
@echo on
copy \\kdstst\KdsCalculTest\*.exe \\kdstst\KdsCalculTest\old\
copy \\kdstst\KdsCalculTest\*.dll \\kdstst\KdsCalculTest\old\
copy \\kdstst\KdsCalculTest\*.config \\kdstst\KdsCalculTest\old\
@echo install KdsCalculTest files to kdstst ....
@echo off 
copy C:\dev\kds\Main\Kds\KdsCalcul\bin\Release\*.exe  \\kdstst\KdsCalculTest\
copy C:\dev\kds\Main\Kds\KdsCalcul\bin\Release\*.dll  \\kdstst\KdsCalculTest\
del \\kdstst\KdsCalculTest\KdsCalculTest.exe
Rename \\kdstst\KdsCalculTest\KdsCalcul.exe  KdsCalculTest.exe 

copy \\kdstst\KdsCalculTest\KdsCalculTest.exe  \\kdstst\KdsCalculTest\KdsRikuzimsTest.exe 

@echo KdsCalcul was installed in kdstst !
@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
@echo KdsService install in progress ....
@echo copy backup KdsService to old folder ....
@echo off 
copy \\kdstst\KdsServiceTest\*.exe \\kdstst\KdsServiceTest\old\
copy \\kdstst\KdsServiceTest\*.dll \\kdstst\KdsServiceTest\old\
copy \\kdstst\KdsServiceTest\*.config \\kdstst\KdsServiceTest\old\
copy \\kdstst\KdsServiceTest\*.xml \\kdstst\KdsServiceTest\old\
@echo install KdsService to kdstst ....
copy C:\dev\kds\Main\Kds\KdsService\bin\Release\*.exe  \\kdstst\KdsServiceTest\
copy C:\dev\kds\Main\Kds\KdsService\bin\Release\*.dll  \\kdstst\KdsServiceTest\
@echo KdsService was installed in kdstst !
@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%

@echo KdsService install in progress inds kdsTest....
@echo copy backup KdsService to old folder ....
@echo off 
copy \\kdstest\KdsService\*.exe \\kdstest\KdsService\old\
copy \kdstest\KdsService\*.dll \\kdstest\KdsService\old\
copy \\kdstest\KdsService\*.config \\kdstest\KdsService\old\
copy \\kdstest\KdsService\*.xml \\kdstest\KdsService\old\
copy C:\dev\kds\Main\Kds\KdsService\bin\Release\*.exe   \\kdstest\KdsService
copy C:\dev\kds\Main\Kds\KdsService\bin\Release\*.dll  \\kdstest\KdsService

@echo KdsService was installed in KdsTest02 !
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
