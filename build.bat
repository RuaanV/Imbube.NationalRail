
@echo off

set Path.MSBuild="%windir%\Microsoft.NET\Framework\v4.0.30319\msbuild.exe"

:BUILDNATIONALRAILSOLUTION
echo.
echo.......
echo ========================================================================
echo BUILDING IMBUBE NATIONALRAIL APP SOLUTION - USING DEFAULT CONFIGURATION
echo ========================================================================
echo.....................

%Path.MSBuild% Imbube.NationalRail.sln /t:rebuild /m
echo.
if %ERRORLEVEL%==0 goto END
if not %ERRORLEVEL%==0 goto FAIL

:FAIL
echo.
echo ==========================================
echo BUILD FAILED.  See log files for details.
echo ==========================================
echo.
timeout 5
EXIT /B

:END
timeout 5
EXIT /B
