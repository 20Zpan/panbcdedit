@echo off
chcp 65001 >nul
echo ========================================
echo   panbcdedit 编译脚本
echo   作者: zpan  版本: 1.0
echo ========================================
echo.

set CSC=C:\Windows\Microsoft.NET\Framework64\v4.0.30319\csc.exe
set OUT_DIR=bin\Debug
set OUT_EXE=%OUT_DIR%\panbcdedit.exe
set MANIFEST=app.manifest

if not exist "%CSC%" (
    echo [错误] 未找到 csc.exe 编译器: %CSC%
    echo 请确认已安装 .NET Framework 4.8
    pause
    exit /b 1
)

echo [信息] 编译器: %CSC%
echo [信息] 输出路径: %OUT_EXE%
echo.

if not exist "%OUT_DIR%" mkdir "%OUT_DIR%"

echo 正在编译...

"%CSC%" /target:winexe /out:"%OUT_EXE%" /win32manifest:"%MANIFEST%" /win32icon:Resources\bcd.ico /reference:System.dll /reference:System.Core.dll /reference:System.Data.dll /reference:System.Drawing.dll /reference:System.Windows.Forms.dll /reference:System.Xml.dll /resource:Resources\efiloader.efi,panbcdedit.Resources.efiloader.efi /recurse:*.cs

if %ERRORLEVEL% equ 0 (
    echo.
    echo ========================================
    echo   编译成功！
    echo   输出: %OUT_EXE%
    echo ========================================
) else (
    echo.
    echo ========================================
    echo   编译失败！请检查上方错误信息。
    echo ========================================
)

pause
