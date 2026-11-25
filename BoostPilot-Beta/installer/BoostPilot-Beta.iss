#define AppVersion "0.2.0-beta.1"
#ifndef PublishDir
  #define PublishDir "build\\artifacts\\publish"
#endif
#ifndef OutputDir
  #define OutputDir "build\\artifacts"
#endif

[Setup]
AppId={{8d438f8b-6d21-4a5e-9c4f-2fbb0ab6e1d0}
AppName=BoostPilot Beta
AppVersion={#AppVersion}
AppPublisher=BoostPilot
AppPublisherURL=https://github.com/goooglecrome/BoostPilot
DefaultDirName={localappdata}\\Programs\\BoostPilot Beta
DefaultGroupName=BoostPilot Beta
UninstallDisplayName=BoostPilot Beta {#AppVersion}
UninstallDisplayIcon={app}\\BoostPilotBeta.exe
PrivilegesRequired=lowest
Uninstallable=yes
OutputDir={#OutputDir}
OutputBaseFilename=BoostPilot-Beta-Setup
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
Name: "{autoprograms}\\BoostPilot Beta"; Filename: "{app}\\BoostPilotBeta.exe"
Name: "{autodesktop}\\BoostPilot Beta"; Filename: "{app}\\BoostPilotBeta.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\\BoostPilotBeta.exe"; Description: "Launch BoostPilot Beta"; Flags: nowait postinstall skipifsilent
