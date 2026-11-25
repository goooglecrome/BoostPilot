#define AppName "BoostPilot Claude Edition"
#ifndef AppVersion
  #define AppVersion "0.1.0"
#endif
#ifndef PublishDir
  #define PublishDir "build\\artifacts\\publish"
#endif
#ifndef OutputDir
  #define OutputDir "build\\artifacts"
#endif

[Setup]
AppId={{7a6d9dbb-3dc7-4545-bc71-00f4e93d4f3e}
AppName={#AppName}
AppVersion={#AppVersion}
AppPublisher=BoostPilot
DefaultDirName={localappdata}\\Programs\\BoostPilot Claude Edition
DefaultGroupName=BoostPilot Claude Edition
PrivilegesRequired=lowest
Uninstallable=yes
OutputDir={#OutputDir}
OutputBaseFilename=BoostPilot-Claude-Setup
ArchitecturesAllowed=x64
ArchitecturesInstallIn64BitMode=x64
WizardStyle=modern
Compression=lzma2
SolidCompression=yes
SetupLogging=yes
CloseApplications=yes

[Tasks]
Name: "desktopicon"; Description: "Create a desktop shortcut"; GroupDescription: "Additional icons:"

[Files]
Source: "{#PublishDir}\\*"; DestDir: "{app}"; Flags: ignoreversion recursesubdirs createallsubdirs

[Icons]
Name: "{autoprograms}\\BoostPilot Claude Edition"; Filename: "{app}\\BoostPilotClaude.exe"
Name: "{autodesktop}\\BoostPilot Claude Edition"; Filename: "{app}\\BoostPilotClaude.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\\BoostPilotClaude.exe"; Description: "Launch BoostPilot Claude Edition"; Flags: nowait postinstall skipifsilent
