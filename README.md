# BoostPilot

**A Windows gaming and PC optimisation control centre.**

This repository is the home of the BoostPilot desktop application, Windows build scripts, installer packaging, and release workflow.

## Current development milestone

The first native Windows rebuild focuses on a reliable WPF desktop shell, a first-run optimisation wizard, reversible Windows gaming settings, hardware/network diagnostics, and a transparent results report.

## Planned modules

- Modern Windows desktop dashboard with an animated collapsible sidebar.
- First-run setup wizard for PC type, gaming goals, games, network, and automation preferences.
- Review-before-apply optimisation with real task progress and a completion report.
- Master Optimizer ON/OFF control and restore workflow.
- CPU, GPU, memory, storage, and network diagnostics.
- Gaming/FPS settings, including Windows per-game GPU preferences.
- Ping and packet-loss diagnostics. No promise that local software can control ISP routing or game-server latency.
- Hardware-specific fan and memory controls only through compatible vendor or firmware interfaces.
- Windows installer and GitHub Releases update checks.

## Safety principles

- Never silently apply risky or irreversible settings.
- Back up settings that BoostPilot changes and provide a restore path.
- Do not disable Windows Defender, Windows Update, or essential system services.
- Do not expose generic voltage, memory-timing, or fan-control sliders without hardware-specific validation.
- Report each task as complete, skipped, or failed based on its actual result.
- Do not claim an FPS or ping improvement unless it is measured.

## Build and release

The application targets Windows 10/11 x64 and .NET 8 WPF. GitHub Actions builds a self-contained app and packages it with Inno Setup. Tagged releases publish the installer asset used by the in-app update checker.

See `docs/DEVELOPMENT.md` for build instructions and known limitations.
