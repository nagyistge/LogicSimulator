@echo off

set PATH=C:\WINDOWS\Microsoft.NET\Framework\v3.5;%PATH%

MSBuild Logic.sln /m /t:Rebuild /p:Configuration=Debug;TargetFrameworkVersion=v3.5
MSBuild Logic.sln /m /t:Rebuild /p:Configuration=Release;TargetFrameworkVersion=v3.5

pause