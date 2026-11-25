# BoostPilot

**A Windows gaming and PC optimisation control centre.**

## Get the first Windows build

The source project is packaged as `BoostPilot_SourceOnly_v1.2.0.zip`. To start the Windows build:

1. Download the source ZIP from the latest ChatGPT hand-off.
2. Open this repository on GitHub.
3. Choose **Add file → Upload files**, upload `BoostPilot_SourceOnly_v1.2.0.zip` to the repository root, and commit it to `main`.
4. Open **Actions → Build BoostPilot for Windows** and run the workflow.
5. When it finishes successfully, download the `BoostPilot-Windows-Installer` artifact from that workflow run.

The workflow compiles on a Windows runner, publishes a self-contained x64 desktop app, and packages it with Inno Setup. It will fail clearly if the source archive or expected project files are missing.

## Planned modules

- Modern Windows desktop dashboard with an animated, collapsible sidebar.
- First-run setup wizard for PC type, gaming goals, games, network, and automation preferences.
- Review-before-apply optimisation with real task progress and a completion report.
- Master Optimizer ON/OFF control and restore workflow.
- CPU, GPU, memory, storage, and network diagnostics.
- Gaming/FPS settings, including Windows per-game GPU preferences.
- Ping and packet-loss diagnostics. Local software cannot control ISP routing or game-server latency.
- Hardware-specific fan and memory controls only through compatible vendor or firmware interfaces.
- Windows installer and GitHub Releases update checks.

## Safety principles

- Never silently apply risky or irreversible settings.
- Back up settings that BoostPilot changes and provide a restore path.
- Do not disable Windows Defender, Windows Update, firewall protections, or essential services.
- Do not expose generic voltage, memory-timing, or fan-control sliders without hardware-specific validation.
- Report each task as complete, skipped, or failed based on its actual result.
- Do not claim an FPS or ping improvement unless it is measured.

## Development status

The first native Windows rebuild targets Windows 10/11 x64 and .NET 8 WPF. It has not yet been compiled on a Windows machine or tested on physical hardware. The installer is not code-signed. The first GitHub Actions run is the Windows compilation and packaging check; treat its result as authoritative before installing.

See `docs/DEVELOPMENT.md` in the source archive for build instructions and current limitations.
