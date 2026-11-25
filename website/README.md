# BoostPilot website

Static marketing and download website for BoostPilot.

## Cloudflare Pages (free plan) deployment

1. Sign in to Cloudflare and open **Workers & Pages**.
2. Choose **Create application → Pages → Connect to Git**.
3. Authorise GitHub and select `goooglecrome/BoostPilot`.
4. Set the production branch to `main`.
5. Set the **Root directory** to `website`.
6. Choose **None** as the framework preset.
7. Leave the build command empty and set the build output directory to `.`.
8. Save and deploy.

The website is plain HTML, CSS and JavaScript; no npm install or build step is required. Cloudflare will redeploy when files in `website/` change on `main`.

## Download links

- OG stable v1.2.1 installer: the official GitHub Release asset.
- Claude Edition: currently shown as in development because its Windows build has not yet completed successfully. Add its download button only after a successful installer artifact or release is available.

The preview dashboard is illustrative interface artwork, not a live screenshot or live hardware readings.
