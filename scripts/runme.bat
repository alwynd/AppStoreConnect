cd ..

call TestCore\bin\Debug\net6.0\TestCore.exe NXXXXXXXX xxxxxxxx-xxxx-xxxx-xxxx-xxxxxxxxxxxx "C:\Users\alwynd\Downloads\AuthKey_NXXXXXXXX.p8" > scripts\output.log
if %ERRORLEVEL% neq 0 pause
