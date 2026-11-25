# BoostPilot Claude Edition

This is a separate WPF app supplied as a Claude-built project. It lives in this repository beside the original BoostPilot project; it does not replace the OG app.

## Isolation from OG BoostPilot

- Separate executable: `BoostPilotClaude.exe`
- Separate install folder: `%LOCALAPPDATA%\\Programs\\BoostPilot Claude Edition`
- Separate restore database: `%LOCALAPPDATA%\\BoostPilot-Claude-Edition\\boostpilot.db`
- Separate Inno Setup AppId and Start Menu/Desktop shortcuts
- Separate GitHub Actions workflow and installer artifact

**Important:** Claude Edition is not published as the repository's latest GitHub Release. The OG BoostPilot updater checks the latest release endpoint, so publishing Claude Edition as a normal release could interfere with the OG update channel.

## Build and download

1. Open this repository's **Actions** tab.
2. Choose **Build BoostPilot Claude Edition**.
3. Run the workflow manually, or push a change under `ClaudeEdition/`.
4. When it succeeds, download the `BoostPilot-Claude-Edition-Installer` artifact from that run.

The workflow builds on a Windows runner using .NET 10 and packages the result with Inno Setup. The first Actions run is the compilation and installer packaging check. This workspace cannot run a WPF app or test it on a physical Windows PC.

## Features in the supplied source

- Live CPU, GPU, RAM, disk, network and uptime readings
- CPU usage graph and top memory processes
- First-run setup wizard and goal-based optimisation plan
- Gaming, balanced and power-saving Windows power plan modes
- Per-user startup application controls
- Temporary-file scan and cleanup
- SQLite-backed Restore Centre for reversible changes

The project is based on the uploaded Claude ZIP. Feature behaviour should be verified on your PC before relying on optimisation changes.
