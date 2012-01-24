@echo off
echo Running Build for Imbube National Rail Windows Phone 7 Application

powershell -ExecutionPolicy RemoteSigned -noLogo -NonInteractive -File .\build.ps1

PAUSE 