# BoostPilot Beta 0.2.0-beta.1

This is a separate beta build made from the recently uploaded BoostPilot WPF source project.

## Separate from the original stable app

- Beta executable: `BoostPilotBeta.exe`
- Beta install folder: `%LOCALAPPDATA%\\Programs\\BoostPilot Beta`
- Beta Restore Centre database: `%LOCALAPPDATA%\\BoostPilot-Beta\\boostpilot.db`
- Separate installer AppId and Start Menu/Desktop shortcuts
- Separate Git branch: `beta`
- Published as a GitHub pre-release with `make_latest: false`

The OG app's stable installer and updater remain on the regular `v1.2.1` release.

## Build and release

The `Build BoostPilot Beta` workflow runs on pushes to this branch. It checks the source archive SHA-256, restores .NET 10 packages, fails on high/critical NuGet advisories, publishes a self-contained Windows x64 WPF app, builds an Inno Setup installer, uploads an Actions artifact and publishes the beta as a pre-release.

## Changes

- Updates .NET 10 dependencies to 10.0.15.
- Uses LibreHardwareMonitorLib 0.9.6.
- Replaces the missing Visual Basic ComputerInfo dependency with the Windows GlobalMemoryStatusEx API.
- Adds explicit System.IO imports for file and drive operations.
- Gives the beta its own SQLite restore database.
- Stores and restores the exact StartupApproved registry bytes for startup app changes.
- Rolls back supported changes if the restore record cannot be saved.
- Keeps startup disabling and non-reversible temp cleanup unselected by default.

Beta builds need testing on actual Windows hardware. A successful GitHub Actions build confirms compilation and installer creation, not that every sensor or Windows setting works across all PCs.
