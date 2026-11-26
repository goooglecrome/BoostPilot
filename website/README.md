# BoostPilot website

Static multi-page website for BoostPilot.

Cloudflare publishes only the dedicated `public/` directory. This keeps Wrangler dependencies, package files, source archives and project configuration out of public website assets.

## Website pages

- `public/index.html` — home page
- `public/downloads.html` — all public GitHub Releases, newest to oldest
- `public/faq.html` — FAQ
- `public/discounts.html` — discounts information
- `public/styles.css`, `public/app.js`, `public/downloads.js` — shared presentation and page behaviour
- `public/favicon.svg` — browser icon
- `public/_headers` — security headers

## Published versions

- OG stable: v1.3.0 (`BoostPilot-Setup.exe`)
- Beta pre-release: v0.2.0-beta.1 (`BoostPilot-Beta-Setup.exe`), built from the `beta` branch. Beta installs separately from OG.
- The Downloads page reads public GitHub Releases, sorts non-draft releases newest to oldest, and links attached installer assets directly. Beta pre-releases are included.

## Cloudflare Workers Builds settings

In **Workers & Pages → boostpilot → Settings → Builds**, use:

| Setting | Value |
|---|---|
| Production branch | `main` |
| Root directory | `website` |
| Build command | `None` / empty |
| Deploy command | `npx wrangler deploy --config ./wrangler.jsonc --assets ./public` |

The `wrangler.jsonc` file is in this directory and points Workers Static Assets at `./public`.

No paid product or checkout is configured yet, so discount codes are not active.
