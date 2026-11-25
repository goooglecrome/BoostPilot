# BoostPilot website

Static product and downloads website for BoostPilot.

## Cloudflare Workers Builds setup

The repository is connected to a Cloudflare Worker named `boostpilot`.

In the Cloudflare dashboard, open **Workers & Pages → boostpilot → Settings → Builds** (or the build configuration section) and set:

| Setting | Value |
|---|---|
| Root directory | `website` |
| Build command | `None` / empty |
| Deploy command | `npx wrangler deploy` |

Save the settings and trigger a new deployment. The root directory must be `website`, so Wrangler reads `wrangler.jsonc` here and publishes this folder's static HTML, CSS, JavaScript and image assets.

The configuration uses Workers Static Assets. Wrangler automatically uploads the files in the configured assets directory and serves them to website visitors.

## Download links

- OG stable v1.2.1 installer: official GitHub Release asset.
- Claude Edition: shown as in development until its Windows build successfully creates an installer artifact.

The website is plain HTML, CSS and JavaScript; no frontend build command is required.

The preview dashboard is illustrative artwork, not a live screenshot or live hardware readings.
