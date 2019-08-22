ECHO OFF
echo Please wait...
'sets the name of your app to display
reg add "HKLM\Software\Microsoft\Windows\CurrentVersion\Uninstall\Yourapp" /v "DisplayName" /d "[APPNAME]" /f 
'sets your app version to display
REG ADD "HKLM\Software\Microsoft\Windows\CurrentVersion\Uninstall\Yourapp" /V "DisplayVersion" /D "[APPVERSION]"
'sets your app icon to display location
REG ADD "HKLM\Software\Microsoft\Windows\CurrentVersion\Uninstall\Yourapp" /V "DisplayIcon" /D "[APPPATH]"
'sets your name to display as publisher
REG ADD "HKLM\Software\Microsoft\Windows\CurrentVersion\Uninstall\Yourapp" /V "Publisher" /D "[PUBLISHER]"
'sets the uninstall path
REG ADD "HKLM\Software\Microsoft\Windows\CurrentVersion\Uninstall\Yourapp" /V "UninstallString" /D "[INSTALLER]"
REG ADD "HKLM\Software\Microsoft\Windows\CurrentVersion\Uninstall\Yourapp" /V "UninstallPath" /D "[INSTALLER]"
' sets the install directory
REG ADD "HKLM\Software\Microsoft\Windows\CurrentVersion\Uninstall\Yourapp" /V "InstallLocation" /D "[APPFOLDER]"