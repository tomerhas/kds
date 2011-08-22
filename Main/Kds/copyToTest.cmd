@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
@echo 			INSTALLATION OF KDS PROGRAMS TO TEST
@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
pause 
@echo KdsCalcul install in progress ....
@echo copy backup KdsCalcul to old folder ....
@echo off 
copy \\kdstst02\KdsCalcul\*.exe \\kdstst02\KdsCalcul\old\
copy \\kdstst02\KdsCalcul\*.dll \\kdstst02\KdsCalcul\old\
copy \\kdstst02\KdsCalcul\*.config \\kdstst02\KdsCalcul\old\
@echo install KdsCalcul files to kdstst02 ....
@echo off 
copy C:\dev\kds\Main\Kds\KdsCalcul\bin\Release\*.exe  \\kdstst02\KdsCalcul\
copy C:\dev\kds\Main\Kds\KdsCalcul\bin\Release\*.dll  \\kdstst02\KdsCalcul\
@echo KdsCalcul was installed in kdstst02 !
@echo %%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%%
@echo KdsService install in progress ....
@echo copy backup KdsService to old folder ....
@echo off 
copy \\kdstst02\KdsService\*.exe \\kdstst02\KdsService\old\
copy \\kdstst02\KdsService\*.dll \\kdstst02\KdsService\old\
copy \\kdstst02\KdsService\*.config \\kdstst02\KdsService\old\
copy \\kdstst02\KdsService\*.xml \\kdstst02\KdsService\old\
@echo install KdsService to kdstst02 ....
copy C:\dev\kds\Main\Kds\KdsService\bin\Release\*.exe  \\kdstst02\KdsService\
copy C:\dev\kds\Main\Kds\KdsService\bin\Release\*.dll  \\kdstst02\KdsService\
@echo KdsService was installed in kdstst02 !
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
