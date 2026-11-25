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

## Cloudflare Workers Builds settings

In **Workers & Pages → boostpilot → Settings → Builds**, use:

| Setting | Value |
|---|---|
| Production branch | `main` |
| Root directory | `website` |
| Build command | `None` / empty |
| Deploy command | `npx wrangler deploy` |

The `wrangler.jsonc` file is in this directory and points Workers Static Assets at `./public`. The website does not require a frontend build step.

The Downloads page reads public GitHub Releases, sorts non-draft releases newest to oldest, and links attached installer assets directly. Source archives are labelled separately from installers.

No paid product or checkout is configured yet, so discount codes are not active.
