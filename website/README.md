# BoostPilot website

Multi-page static website for BoostPilot.

## Pages

- `index.html` — home page and feature overview
- `downloads.html` — loads all public GitHub Releases newest-to-oldest, including downloadable release assets
- `faq.html` — standalone FAQ page
- `discounts.html` — official promotions information (awaiting a paid product and checkout before redeemable coupons can be enabled)

The Downloads page calls the public GitHub Releases API and paginates through the release history. The currently published history contains one installer release, v1.2.1. The v1.2.0 source ZIP in the repository is separately labelled as source code, not an installer. The Claude Edition is not shown as downloadable until its Windows installer has been successfully built and published.

## Cloudflare Workers Builds setup

The repository is connected to a Cloudflare Worker named `boostpilot`.

In **Workers & Pages → boostpilot → Settings → Builds**, use:

| Setting | Value |
|---|---|
| Production branch | `main` |
| Root directory | `website` |
| Build command | `None` / empty |
| Deploy command | `npx wrangler deploy` |

The `wrangler.jsonc` file configures Workers Static Assets to serve this directory. No frontend build command is required.

## Discount codes

No checkout or paid product is currently configured. The Discounts page explains this honestly rather than offering inactive codes. Add secure, server-side coupon validation only after the product, price and checkout provider have been chosen. Never implement discount validation solely in browser JavaScript.

The dashboard artwork is illustrative, not live hardware readings.
