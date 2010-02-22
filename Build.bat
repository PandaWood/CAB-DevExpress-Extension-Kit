
echo off
echo.
echo =========================================================
echo   Build                                    
echo      Builds the Application
echo =========================================================
echo.

set msBuildDir=%WINDIR%\Microsoft.NET\Framework\v3.5
set solutionDir="."
set buildType=Debug
set returnErrorCode=true
set pause=true
set SolutionName="CABDevExpress.ExtensionKit.sln"


if "%1"=="/?" goto HELP

if not Exist %solutionDir%\%SolutionName% goto HELP

@REM  ----------------------------------------------------
@REM  If the first parameter is /q, do not pause
@REM  at the end of execution.
@REM  ----------------------------------------------------

if /i "%1"=="/q" (
 set pause=false
 SHIFT
)

@REM  ----------------------------------------------------
@REM  If the first or second parameter is /i, do not 
@REM  return an error code on failure.
@REM  ----------------------------------------------------

if /i "%1"=="/i" (
 set returnErrorCode=false
 SHIFT
)

@REM  ----------------------------------------------------
@REM  User can override default build type by specifiying
@REM  a parameter to batch file (e.g. BuildLibrary Debug).
@REM  ----------------------------------------------------

if not "%1"=="" set buildType=%1
set OutputDir=..\Bin\%buildType%\

@REM  ------------------------------------------------
@REM  Shorten the command prompt for making the output
@REM  easier to read.
@REM  ------------------------------------------------
set savedPrompt=%prompt%
set prompt=*$g

@ECHO ----------------------------------------
@ECHO BuildLibrary.bat Started
@ECHO ----------------------------------------
@ECHO.

@REM -------------------------------------------------------
@REM Change to the directory where the solution file resides
@REM -------------------------------------------------------

pushd %solutionDir%

@REM -------------------------------------------------------
@REM Change to the directory where the solution file resides
@REM -------------------------------------------------------

if "%DevEnvDir%"=="" (
	@ECHO ------------------------------------------
	@ECHO Setting build environment
	@ECHO ------------------------------------------
	@CALL "%VS80COMNTOOLS%\vsvars32.bat" > NUL 
)

@ECHO.
@ECHO -------------------------------------------
@ECHO Building the Enterprise Library assemblies
@ECHO -------------------------------------------

call %msBuildDir%\msbuild %SolutionName% /t:Rebuild /p:Configuration=%buildType% /property:OutDir=%OutputDir%
@if errorlevel 1 goto :error

@ECHO.
@ECHO ----------------------------------------
@ECHO BuildLibrary.bat Completed
@ECHO ----------------------------------------
@ECHO.

@REM  ----------------------------------------
@REM  Restore the command prompt and exit
@REM  ----------------------------------------
@goto :exit

@REM  -------------------------------------------
@REM  Handle errors
@REM
@REM  Use the following after any call to exit
@REM  and return an error code when errors occur
@REM
@REM  if errorlevel 1 goto :error	
@REM  -------------------------------------------
:error
if %returnErrorCode%==false goto exit

@ECHO An error occured in Build.bat - %errorLevel%
if %pause%==true PAUSE
@exit errorLevel

:HELP
echo Usage: Build [/q] [/i] [build type] 
echo.
echo BuildLibrary is to be executed in the directory where EntLibContrib.sln resides
echo The default build type is Debug.
echos. 
echo    If the first or second parameter is /i, do not  return an error code on failure.
echo    If the first parameter is /q, do not pause at the end of execution.
echo. 
echo Examples:
echo.
echo    "Build" - builds a Debug build      
echo    "Build Release" - builds a Release build
echo.

@REM  ----------------------------------------
@REM  The exit label
@REM  ----------------------------------------
:exit
if %pause%==true PAUSE

popd
set pause=
set solutionDir=
set buildType=
set returnErrorCode=
set prompt=%savedPrompt%
set savedPrompt=
set SolutionName=
echo on

