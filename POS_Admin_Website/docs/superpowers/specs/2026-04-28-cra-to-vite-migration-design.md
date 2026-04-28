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

## Deviations during implementation

Recorded after the migration was executed. The following items diverged from or extended the plan:

- **Repo state surprise.** Almost the entire `POS_Admin_Website/` source tree was untracked at the start of execution (only 11 files were tracked: the env files, Vite config files, `src/index.jsx`, and the docs). A prior parent-level project restructure was sitting unstaged. As a result, every Task 5 file edit committed as `A` (add), not `M` (modify), and `git rm` was unnecessary for `.babelrc` / `public/index.html` (a plain `rm` was correct because they were never tracked at the new paths). This did not change the migration outcome — the files end up at the correct paths with correct content — but the commit history records the migration as a sequence of additions on top of the pending restructure.
- **Audit finding (Task 1):** one `process.env.*` consumer was found and rewritten — `src/utils/getBranding.js:4` read `process.env.REACT_APP_TENANT`. It now reads `import.meta.env.VITE_TENANT`. A `VITE_TENANT=default` example entry was added to `.env.example` in a follow-up commit (`81f9444bd`).
- **Pre-existing bugs surfaced by Vite (Task 6):** Vite's stricter pipeline exposed four bugs that CRA's `react-scripts` had been masking. The first three were fixed in commit `a23d54ca7` during dev-boot verification; the fourth was surfaced in the browser by the user during smoke-testing the production build and fixed in commit `1f76b164e`:
  1. `src/core/img/imagewithbasebath.jsx` used TypeScript `interface` syntax in a `.jsx` file. CRA's `babel-preset-typescript` had been silently stripping it. The interface and the corresponding `: Image` parameter annotation were removed; runtime behaviour is unchanged.
  2. `src/feature-module/settings/financialsettings/banksettinggrid.jsx` imported `{ allSettled }` from `"q"` (a package never installed) and used the value as `route.banksettingslist`, which would always have been `undefined` at runtime. The import was replaced with `import { all_routes } from "../../../Router/all_routes"`, matching the pattern used by sibling files in the same module.
  3. `src/feature-module/Application/calendar.jsx` declared `defaultEvents` as part of a multi-`const` chain at the top of `Calendar()`, then reassigned it at line 124. esbuild rejected this. The chain was split so `defaultEvents` is declared with `let` separately.
  4. `src/core/redux/initial.value.jsx:33` used CommonJS `require("../json/productlistdata")` in the middle of an ES module. CRA's webpack provided `require` as a global as part of CommonJS interop; Vite's Rollup pipeline does not, so the production build threw `ReferenceError: require is not defined` at runtime. Replaced with the equivalent named ES import.
- **Smoke-route verification (Task 7) deferred to user.** The plan called for navigating to `/dashboard`, `/products`, `/sales`, `/inventory/productlist` in a real browser with DevTools open. The implementation session could not drive a real browser, so this verification step was handed back to the user. The dev server starts cleanly, the entry module transforms cleanly, the production build succeeds, and the production URL is correctly baked into the prod bundle (verified via grep of `dist/assets/index-*.js`).
- **Vite version pinned to `^5.4.0`** and `@vitejs/plugin-react` to `^4.3.0`. Both confirmed installed and operational under Vite v5.4.21 / Node bundled with the dev environment.
- **Build output observation:** the production bundle is large (~9.7MB unminified, ~2MB gzipped main JS chunk). Vite emitted a chunk-size warning. This is not a regression — the kitchen-sink dependency surface (3 UI kits, multiple chart/DnD/date libs) means the bundle was always going to be large. Code-splitting is a candidate for the dependency-audit follow-up.
