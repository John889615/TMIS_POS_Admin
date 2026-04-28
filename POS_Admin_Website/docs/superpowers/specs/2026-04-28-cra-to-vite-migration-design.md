# CRA → Vite Migration with Proper Environment Files

**Date:** 2026-04-28
**Project:** TMIS POS Admin Website (`POS_Admin_Website`)
**Scope:** Frontend only. Light cleanup approach (knowledge layer + targeted structural change).

## Goal

Move the TMIS POS Admin React frontend from Create React App (`react-scripts`) to Vite, and replace hardcoded configuration (API base URLs, base path) with proper development/production environment files. The project must run cleanly in both `dev` and `preview` modes after the change.

## Why

- CRA is deprecated and unmaintained. Vite gives ~10–100× faster dev startup, better HMR, and active upstream support.
- The current codebase has hardcoded API URLs in two axios clients with three different URLs commented in/out of `posAPI.js` — this is exactly the smell that env files exist to remove.
- The current `.env` only holds CRA-specific flags (`DISABLE_ESLINT_PLUGIN=true`, `BROWSER=none`) — no real configuration is yet expressed via env. Migrating to Vite is a natural moment to introduce real env discipline.

## Current state (baseline)

| Item | Value |
|---|---|
| Build tool | `react-scripts` 5.0.1 (CRA) |
| Entry | `src/index.js` (contains JSX) |
| HTML template | `public/index.html` |
| Babel config | `.babelrc` (preset-env, preset-react, class-properties) |
| Env file | `.env` (40 bytes, CRA-only flags) |
| Routing | `HashRouter`, `base_path = "/"` from `src/environment.jsx` |
| File counts | 436 `.jsx` + 123 `.js` in `src/` (~90k LOC total) |
| API clients | `src/services/api.js` → `https://tmis.co.za/TMIS_Portal/Portal_Api/api`<br>`src/services/posAPI.js` → `https://tmis.co.za/pos_bs/api` (with 2 commented alternates) |
| Auth | `AuthContext` + `RequireAuth` + JWT in localStorage |
| Hosting hint | `Web.config` (generic .NET 4.8 stub, no URL rewrite — IIS leftover) |

## Target architecture

- **Build tool:** Vite 5 with `@vitejs/plugin-react`.
- **Dev server:** port `3000` to match CRA's default; `host: true` so it's accessible on the LAN.
- **Entry HTML:** `index.html` at the project root (Vite convention), referencing `/src/index.jsx` via `<script type="module">`.
- **Env layer:** Vite env vars (`VITE_*` prefix) consumed via `import.meta.env`. Three committed env files plus a gitignored local override and a committed example.
- **Routing:** unchanged. `HashRouter` is retained, so no SPA fallback rewrite is required for static hosting.
- **Styles & assets:** Vite natively handles SCSS (because `sass` is already installed), CSS imports from `node_modules`, and `public/` as static asset root.

## Files added

| Path | Purpose |
|---|---|
| `vite.config.js` | Vite config: React plugin, port 3000, `define` shim if needed, esbuild loader rule allowing JSX in `.js` files |
| `index.html` | Project-root HTML, replaces `public/index.html`'s role |
| `.env` | Shared defaults: `VITE_BASE_PATH`, `VITE_PORTAL_API_URL` |
| `.env.development` | Dev overrides: `VITE_POS_API_URL=https://localhost:44392/api` |
| `.env.production` | Prod overrides: `VITE_POS_API_URL=https://tmis.co.za/pos_bs/api` |
| `.env.example` | Committed reference template documenting every variable |
| `jsconfig.json` (optional) | Editor hint — `compilerOptions.baseUrl: "."` so VS Code path resolution stays sane |

## Files modified

### `package.json`
- **Scripts**
  - `start` → remove (replaced by `dev`)
  - `dev` → `vite`
  - `build` → `vite build`
  - `preview` → `vite preview`
  - `test` → keep (or stub; CRA test runner removed with `react-scripts`)
- **Dependencies**
  - Add `vite` (^5.x), `@vitejs/plugin-react` (^4.x)
  - Remove `react-scripts`
- **Top-level fields**
  - Remove `homepage` (CRA-specific)
  - Remove `browserslist` (CRA-specific; Vite uses its own targets)
  - Remove `eslintConfig` if present (CRA-specific)
  - Keep `overrides`
- **devDependencies**
  - Keep `eslint` for now (manual ESLint setup outside Vite)
  - Drop `@babel/plugin-proposal-private-property-in-object` (it was a CRA peer-dep workaround)

### `src/index.js` → `src/index.jsx`
- Rename only. The Vite entry must end in `.jsx` because it contains JSX. No code change.

### `src/services/api.js`
```diff
- baseURL: "https://tmis.co.za/TMIS_Portal/Portal_Api/api",
+ baseURL: import.meta.env.VITE_PORTAL_API_URL,
```

### `src/services/posAPI.js`
```diff
- //baseURL: "https://tmis.co.za/tmis_pos/pos_api/api",
-  baseURL: "https://tmis.co.za/pos_bs/api",
- //baseURL: "https://localhost:44392/api",
+ baseURL: import.meta.env.VITE_POS_API_URL,
```

### `src/environment.jsx`
```diff
- export const base_path = "/";
- export const image_path ='/'
+ export const base_path = import.meta.env.VITE_BASE_PATH ?? "/";
+ export const image_path = import.meta.env.VITE_BASE_PATH ?? "/";
```

### `public/index.html`
- **Delete.** The root `index.html` replaces it. The `public/` folder remains and continues to host static assets (`/assets/img/...`).

### `.babelrc`
- **Delete.** Vite + `@vitejs/plugin-react` handle JSX/transforms; the existing presets are not needed.

### `.gitignore`
- Append:
  ```
  # local env overrides
  .env.local
  .env.*.local

  # Vite build output
  dist/
  ```

## Env values

Committed defaults:

```
# .env (shared)
VITE_BASE_PATH=/
VITE_PORTAL_API_URL=https://tmis.co.za/TMIS_Portal/Portal_Api/api

# .env.development
VITE_POS_API_URL=https://localhost:44392/api

# .env.production
VITE_POS_API_URL=https://tmis.co.za/pos_bs/api
```

`.env.example` mirrors the union of all keys above with placeholder values, so a new developer cloning the repo can copy it to `.env.local` and edit.

## Vite config sketch

```js
// vite.config.js
import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

export default defineConfig({
  plugins: [react()],
  server: { port: 3000, host: true, open: true },
  build: { outDir: 'dist', sourcemap: true },
  esbuild: {
    loader: 'jsx',
    include: [/src\/.*\.jsx?$/],
    exclude: [],
  },
  optimizeDeps: {
    esbuildOptions: {
      loader: { '.js': 'jsx' },
    },
  },
});
```

The `esbuild` + `optimizeDeps` blocks are what allow JSX inside `.js` files to compile without renaming 123 files.

## Key decisions and trade-offs

### 1. JSX in `.js` files — keep, don't rename
There are 123 `.js` files in `src/`, many containing JSX. Vite's defaults reject this. Two options:
- (a) Rename all `.js`-with-JSX files to `.jsx`. Cleaner final state, but a large mechanical diff that obscures the migration.
- (b) Configure esbuild to compile JSX in `.js`. Smaller diff, non-standard config.

**Decision: (b).** Rename pass is a follow-up once the migration is verified stable. The non-standard config is documented in `vite.config.js`.

### 2. `process.env.*` references
The CRA-specific values in `.env` (`DISABLE_ESLINT_PLUGIN`, `BROWSER`) do not need to survive — both are CRA-internal behaviour with no Vite equivalent or need. Any other `process.env.*` references in `src/` are rewritten to `import.meta.env.VITE_*`. (An earlier scoped grep timed out; the execution plan covers running a tighter scoped grep file-by-file.)

### 3. `PUBLIC_URL`
If grep finds usages, replace with `import.meta.env.BASE_URL`. `HashRouter` and the existing `environment.jsx` `base_path` are independent of `PUBLIC_URL`, so this is unlikely to apply.

### 4. CommonJS deps
`jquery`, `dragula`, `boxicons`, `react-jvectormap` and similar can occasionally trip Vite's ESM-first resolver. **Mitigation:** if startup throws on one of these, add to `optimizeDeps.include`. This is a known risk, addressed reactively rather than pre-emptively.

### 5. `Web.config`
Untouched. It's a generic ASP.NET 4.8 stub with no URL rewrite rules and is not actively involved in serving the React build. Future IIS-hosting work can revisit it.

### 6. HashRouter retained
Avoids any need for IIS/static-host SPA fallback rewrites. Switching to BrowserRouter is a separate, optional follow-up.

## Verification plan

Each step must pass before the migration is declared done.

1. **Install** — `npm install` completes with no peer-dep errors.
2. **Dev boot** — `npm run dev` starts on `http://localhost:3000`; browser opens; sign-in page renders with no blocking console errors.
3. **Auth flow** — sign in succeeds (depends on `localhost:44392` POS API being reachable; if not, document and continue with mocked path); JWT lands in `localStorage`; `RequireAuth` allows navigation.
4. **Smoke routes** — visit `/dashboard`, `/products` (or `/feature-module/Products/product`), `/sales`, `/inventory/productlist`. Each page renders.
5. **Network panel** — confirm requests go to the URL configured by `VITE_POS_API_URL` (dev value), and Portal API requests go to `VITE_PORTAL_API_URL`.
6. **Build** — `npm run build` produces `dist/` with no errors. Build size logged for future reference.
7. **Preview** — `npm run preview` serves `dist/`; same smoke routes render; network panel confirms **prod** API URL is now in effect.

If any step fails, stop and document the failure in the implementation plan rather than papering over it.

## Out of scope (deliberate)

- Dependency pruning. The kitchen-sink dependency surface (3 UI kits, multiple chart/DnD/date libs) stays untouched.
- Folder restructure or casing normalisation in `src/feature-module/`.
- Test setup. The project has no tests; introducing them is a separate scope.
- Switch to `BrowserRouter`.
- Full `CLAUDE.md` / memory layer.

## Recommended follow-ups (separate brainstorms)

1. **CLAUDE.md** at the project root capturing: folder map, two-API-client rule, env var list, "use `posAPI` for POS, `api` for Portal", and the kitchen-sink UI-kit warning. High value — would have shortcut the question loop in this brainstorm.
2. **File-rename pass** — `.js` → `.jsx` where JSX is present, then drop the esbuild loader override.
3. **Dependency audit** — pick one UI kit, prune the others.
4. **Test scaffolding** — Vitest + React Testing Library, a minimal smoke test per top-level route.
