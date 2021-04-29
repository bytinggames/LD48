dotnet publish -c Release -r win-x64 /p:PublishReadyToRun=false /p:TieredCompilation=false --self-contained true -p:PublishSingleFile=true /p:DefineConstants=WINDOWS
REM pause