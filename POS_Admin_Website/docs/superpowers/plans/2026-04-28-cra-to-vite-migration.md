# CRA → Vite Migration Implementation Plan

> **For agentic workers:** REQUIRED SUB-SKILL: Use superpowers:subagent-driven-development (recommended) or superpowers:executing-plans to implement this plan task-by-task. Steps use checkbox (`- [ ]`) syntax for tracking.

**Goal:** Migrate `POS_Admin_Website` from Create React App (`react-scripts`) to Vite, introducing dev/prod environment files for API base URLs and base path. Project must boot via `npm run dev` and produce a working build via `npm run build && npm run preview`.

**Architecture:** Approach A from the design spec — minimal-diff migration with esbuild loader for JSX-in-`.js` to avoid renaming 123 files. New env layer reads via `import.meta.env.VITE_*`. HashRouter retained.

**Tech Stack:** Vite 5, `@vitejs/plugin-react`, React 18 (existing), Redux Toolkit (existing), axios (existing), HashRouter (existing).

**Spec:** `POS_Admin_Website/docs/superpowers/specs/2026-04-28-cra-to-vite-migration-design.md`

**Working directory for all commands:** `c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website` (unless explicitly otherwise).

**Note on testing:** This codebase has no tests today. Verification in this plan is *functional* (does the dev server boot? does the route render? does the network panel show the right base URL?) — not unit-test based.

**Note on git hygiene:** The working tree has pre-existing unstaged `D` (deletion) entries from a prior project restructure. **Do not stage `.` or `-A`** in any commit — always pass explicit paths. The migration's commits should be clean and self-contained.

---

## File Structure

### Files created

| Path | Responsibility |
|---|---|
| `vite.config.js` | Vite config: React plugin, dev port 3000, esbuild loader allowing JSX in `.js` |
| `index.html` | Root entry HTML, replaces the role of `public/index.html` |
| `.env` | Shared committed defaults (`VITE_BASE_PATH`, `VITE_PORTAL_API_URL`) |
| `.env.development` | Dev override (`VITE_POS_API_URL` → localhost) |
| `.env.production` | Prod override (`VITE_POS_API_URL` → tmis.co.za) |
| `.env.example` | Reference template documenting every var |
| `jsconfig.json` | Editor hint for path resolution |

### Files modified

| Path | Change |
|---|---|
| `package.json` | Scripts swap, deps swap (add Vite, drop `react-scripts`), remove CRA-only top-level fields |
| `src/services/api.js` | `baseURL` reads from `import.meta.env.VITE_PORTAL_API_URL` |
| `src/services/posAPI.js` | `baseURL` reads from `import.meta.env.VITE_POS_API_URL`; commented URL alternates removed |
| `src/environment.jsx` | `base_path` and `image_path` read from `import.meta.env.VITE_BASE_PATH` |
| `src/index.js` → `src/index.jsx` | Rename only (Vite entry must be `.jsx`) |
| `.gitignore` | Append `.env.local`, `.env.*.local`, `dist/` |

### Files deleted

| Path | Reason |
|---|---|
| `public/index.html` | Replaced by root `index.html` |
| `.babelrc` | Vite + `@vitejs/plugin-react` handle transforms |

---

## Task 1: Pre-migration audit

**Files:** none modified — this task gathers information used by Task 5.

**Goal:** Confirm there are no surprise `process.env.*` consumers in `src/` that the migration must rewrite, and confirm baseline state is clean enough to proceed.

- [ ] **Step 1: Confirm git working state and branch**

```bash
git status --short
git rev-parse --abbrev-ref HEAD
```

Expected: branch is `main`. Pre-existing unstaged `D` entries are present (from the earlier project restructure) — that is OK and expected. Do not stage them.

- [ ] **Step 2: Confirm CRA still boots before we start (sanity baseline)**

Skip this only if you already know the current state. Otherwise:

```bash
cd c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website
npm install
npm start &
```

Expected: dev server starts on `http://localhost:3000`. Manually confirm the app loads, then stop the dev server. If CRA already does not start cleanly, **stop and surface this to the user** before continuing — the migration cannot fix a broken baseline.

- [ ] **Step 3: Audit `process.env.*` usage in `src/`**

Use the Grep tool with a tight, scoped pattern (the earlier wide grep timed out on this codebase):

```
pattern: process\.env\.[A-Z_]+
path: c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website/src
output_mode: content
-n: true
```

Record every hit with `file:line` in a scratch list. Each hit will be rewritten in Task 5.

Expected typical findings: zero or one or two hits. The current `.env` only set CRA-internal flags (`DISABLE_ESLINT_PLUGIN`, `BROWSER`) which application code does not consume.

- [ ] **Step 4: Audit `PUBLIC_URL` usage**

```
pattern: PUBLIC_URL
path: c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website/src
output_mode: content
-n: true
```

If hits exist, note them — they need rewriting to `import.meta.env.BASE_URL` in Task 5. Most likely zero hits (the project uses `HashRouter` and a hardcoded `base_path`).

- [ ] **Step 5: No commit**

This task produces no file changes. Move to Task 2.

---

## Task 2: Create env files and update .gitignore

**Files:**
- Create: `c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website/.env`
- Create: `c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website/.env.development`
- Create: `c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website/.env.production`
- Create: `c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website/.env.example`
- Modify: `c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website/.gitignore`

These files are *inert* until Task 5 — Vite is not yet installed, and the source code does not yet read `import.meta.env`. CRA continues to work normally.

- [ ] **Step 1: Overwrite `.env` with shared defaults**

Use the Write tool. Path: `c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website/.env`

Content (replace the current CRA-flag content entirely):

```
# Shared environment defaults (committed)
# Vite picks these up automatically; values prefixed VITE_ are exposed to client code.

VITE_BASE_PATH=/
VITE_PORTAL_API_URL=https://tmis.co.za/TMIS_Portal/Portal_Api/api
```

- [ ] **Step 2: Create `.env.development`**

Use the Write tool. Path: `c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website/.env.development`

Content:

```
# Development overrides (committed). Loaded by Vite when running `npm run dev`.

VITE_POS_API_URL=https://localhost:44392/api
```

- [ ] **Step 3: Create `.env.production`**

Use the Write tool. Path: `c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website/.env.production`

Content:

```
# Production overrides (committed). Loaded by Vite when running `npm run build`.

VITE_POS_API_URL=https://tmis.co.za/pos_bs/api
```

- [ ] **Step 4: Create `.env.example`**

Use the Write tool. Path: `c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website/.env.example`

Content:

```
# Reference template — documents every env var the app understands.
# Copy to .env.local and adjust if you need personal overrides.
# Variables MUST be prefixed VITE_ to be visible to client code.

VITE_BASE_PATH=/
VITE_PORTAL_API_URL=https://tmis.co.za/TMIS_Portal/Portal_Api/api

# POS API base URL. Defaults: dev → localhost:44392, prod → tmis.co.za/pos_bs.
VITE_POS_API_URL=https://localhost:44392/api
```

- [ ] **Step 5: Update `.gitignore`**

Use the Read tool first to see the current `.gitignore` head. Then use the Edit tool to append (do not overwrite — `.gitignore` is large at 545KB-ish per the earlier listing; appending is safe):

The append block (add at the end of the file):

```

# Vite — local env overrides
.env.local
.env.*.local

# Vite — build output
dist/
```

Verify the appended block shows up at the tail of the file.

- [ ] **Step 6: Commit env files and gitignore**

Stage explicitly — never use `.` or `-A`:

```bash
git add POS_Admin_Website/.env POS_Admin_Website/.env.development POS_Admin_Website/.env.production POS_Admin_Website/.env.example POS_Admin_Website/.gitignore
git status --short
```

Expected: only the five paths above are staged with `A ` or `M ` prefixes.

```bash
git commit -m "$(cat <<'EOF'
chore: add Vite env files (dev, prod, example) and gitignore entries

Adds .env, .env.development, .env.production, .env.example with
VITE_BASE_PATH, VITE_PORTAL_API_URL, VITE_POS_API_URL. Inert until
the build tool swap in a follow-up commit.

Co-Authored-By: Claude Opus 4.7 (1M context) <noreply@anthropic.com>
EOF
)"
```

Expected: one commit, 5 files changed.

---

## Task 3: Create Vite config files

**Files:**
- Create: `c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website/vite.config.js`
- Create: `c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website/index.html`
- Create: `c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website/jsconfig.json`

These files are *inert* until Task 5 swaps `package.json` to use Vite. CRA continues to work because it ignores root-level `index.html` and `vite.config.js`.

- [ ] **Step 1: Create `vite.config.js`**

Use the Write tool. Path: `c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website/vite.config.js`

Content:

```js
import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

// JSX-in-.js: 123 files in src/ end with .js but contain JSX.
// The esbuild + optimizeDeps blocks below tell Vite to compile JSX in .js
// files (both source and pre-bundled deps) so we don't have to rename them.
// See migration spec section "JSX in .js files".

export default defineConfig({
  plugins: [react()],
  server: {
    port: 3000,
    host: true,
    open: true,
  },
  build: {
    outDir: 'dist',
    sourcemap: true,
  },
  esbuild: {
    loader: 'jsx',
    include: /src\/.*\.jsx?$/,
    exclude: [],
  },
  optimizeDeps: {
    esbuildOptions: {
      loader: { '.js': 'jsx' },
    },
  },
});
```

- [ ] **Step 2: Create root `index.html`**

Use the Write tool. Path: `c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website/index.html`

Content (note: references `/src/index.jsx` — that file is created by the rename in Task 4; until Task 5 lands, this `index.html` is unused):

```html
<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="UTF-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0" />
    <meta http-equiv="X-UA-Compatible" content="ie=edge" />
    <link rel="icon" href="/assets/img/logo-small.png" />
    <title>TMIS POS Admin</title>
  </head>
  <body>
    <div id="root"></div>
    <script type="module" src="/src/index.jsx"></script>
  </body>
</html>
```

- [ ] **Step 3: Create `jsconfig.json`**

Use the Write tool. Path: `c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website/jsconfig.json`

Content:

```json
{
  "compilerOptions": {
    "baseUrl": ".",
    "jsx": "preserve",
    "module": "ESNext",
    "moduleResolution": "Bundler",
    "target": "ESNext",
    "allowSyntheticDefaultImports": true,
    "resolveJsonModule": true
  },
  "include": ["src", "vite.config.js"],
  "exclude": ["node_modules", "dist", "build"]
}
```

- [ ] **Step 4: Commit Vite config files**

```bash
git add POS_Admin_Website/vite.config.js POS_Admin_Website/index.html POS_Admin_Website/jsconfig.json
git status --short
```

Expected: only the three paths above are staged.

```bash
git commit -m "$(cat <<'EOF'
chore: add Vite config, root index.html, jsconfig

Inert until package.json swap. CRA continues to ignore these files.

Co-Authored-By: Claude Opus 4.7 (1M context) <noreply@anthropic.com>
EOF
)"
```

Expected: one commit, 3 files changed.

---

## Task 4: Rename src/index.js → src/index.jsx

**Files:**
- Rename: `c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website/src/index.js` → `c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website/src/index.jsx`

CRA accepts `.jsx` for the entry. This is the only file rename in the migration; the 123 other `.js`-with-JSX files are handled by the Vite esbuild loader override.

- [ ] **Step 1: Rename via git so history is preserved**

```bash
git mv POS_Admin_Website/src/index.js POS_Admin_Website/src/index.jsx
git status --short
```

Expected: a single line `R  POS_Admin_Website/src/index.js -> POS_Admin_Website/src/index.jsx` (rename detected).

- [ ] **Step 2: Verify file content unchanged**

Read the new `src/index.jsx`. Confirm the imports and `createRoot` call are identical to the original — this should be a pure rename.

- [ ] **Step 3: Commit rename**

```bash
git commit -m "$(cat <<'EOF'
chore: rename src/index.js to src/index.jsx

Vite requires the entry module to have a JSX-friendly extension. Pure
rename, no content change.

Co-Authored-By: Claude Opus 4.7 (1M context) <noreply@anthropic.com>
EOF
)"
```

Expected: one commit, 1 file renamed.

---

## Task 5: Migration switch — env consumers, package.json, deletions

This is the atomic step that retires CRA and turns Vite on. After this commit, `npm start` no longer exists; `npm run dev` is the new entry point. Every change in this task lands in **one commit** so the working tree never sits in a half-migrated state.

**Files:**
- Modify: `c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website/src/services/api.js`
- Modify: `c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website/src/services/posAPI.js`
- Modify: `c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website/src/environment.jsx`
- Modify: any files flagged in Task 1 Step 3 / Step 4 (`process.env.*` / `PUBLIC_URL` consumers)
- Modify: `c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website/package.json`
- Delete: `c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website/.babelrc`
- Delete: `c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website/public/index.html`

- [ ] **Step 1: Update `src/services/api.js`**

Use Read first, then Edit.

Replace:

```js
  baseURL: "https://tmis.co.za/TMIS_Portal/Portal_Api/api",
```

With:

```js
  baseURL: import.meta.env.VITE_PORTAL_API_URL,
```

- [ ] **Step 2: Update `src/services/posAPI.js`**

Use Read first, then Edit. Remove the three commented baseURL lines and replace the active line. The block:

```js
const api = axios.create({
  //baseURL: "https://tmis.co.za/tmis_pos/pos_api/api",
   baseURL: "https://tmis.co.za/pos_bs/api",
  //baseURL: "https://localhost:44392/api",
  
  timeout: 50000,
```

Becomes:

```js
const api = axios.create({
  baseURL: import.meta.env.VITE_POS_API_URL,
  timeout: 50000,
```

- [ ] **Step 3: Update `src/environment.jsx`**

Use Read first, then Edit. Replace the entire file content (it is only 2 exports) with:

```jsx
const basePath = import.meta.env.VITE_BASE_PATH ?? "/";

export const base_path = basePath;
export const image_path = basePath;
```

- [ ] **Step 4: Rewrite any `process.env.*` / `PUBLIC_URL` consumers found in Task 1**

For each `file:line` recorded in Task 1, use the Edit tool to rewrite:
- `process.env.PUBLIC_URL` → `import.meta.env.BASE_URL`
- `process.env.NODE_ENV` → `import.meta.env.MODE` (note: Vite uses `'development'` / `'production'` strings, same as CRA)
- `process.env.VITE_X` → `import.meta.env.VITE_X` (unlikely to exist, but covers the case)
- `process.env.X` (any other) → if X is a CRA-only value like `REACT_APP_*`, define a corresponding `VITE_*` env var in `.env`/`.env.development`/`.env.production` and rewrite

If Task 1 found zero hits, skip this step.

- [ ] **Step 5: Update `package.json`**

Use Read first, then Edit. Apply each of the following changes.

**5a — scripts** — replace:

```json
  "scripts": {
    "start": "react-scripts start",
    "build": "react-scripts build",
    "test": "react-scripts test",
    "eject": "react-scripts eject"
  },
```

With:

```json
  "scripts": {
    "dev": "vite",
    "build": "vite build",
    "preview": "vite preview"
  },
```

(The `test` and `eject` scripts go away — there are no tests in this project; reintroducing a test runner is a separate scope.)

**5b — top-level `homepage`** — remove the line:

```json
  "homepage": "/",
```

**5c — top-level `browserslist`** — remove the entire block:

```json
  "browserslist": {
    "production": [
      ">0.2%",
      "not dead",
      "not op_mini all"
    ],
    "development": [
      "last 1 chrome version",
      "last 1 firefox version",
      "last 1 safari version"
    ]
  },
```

**5d — top-level `eslintConfig`** — if present (it was not visible in the original Read but check now), remove the block.

**5e — drop `react-scripts` from `dependencies`** — remove the line:

```json
    "react-scripts": "^5.0.1",
```

(Make sure to also remove the dangling comma if it was the last entry in `dependencies` — it was not in the original; `yet-another-react-lightbox` is the last entry.)

**5f — drop `@babel/plugin-proposal-private-property-in-object` from `devDependencies`** — it was a CRA peer-dep workaround. Remove the line:

```json
    "@babel/plugin-proposal-private-property-in-object": "^7.21.11",
```

**5g — add Vite to `devDependencies`** — add these two entries (alphabetically, before `eslint`):

```json
    "@vitejs/plugin-react": "^4.3.0",
    "vite": "^5.4.0",
```

After all edits, the relevant sections should look like:

```json
  "scripts": {
    "dev": "vite",
    "build": "vite build",
    "preview": "vite preview"
  },
  ...
  "devDependencies": {
    "@vitejs/plugin-react": "^4.3.0",
    "eslint": "^8.44.0",
    "vite": "^5.4.0"
  }
```

(Sort `devDependencies` alphabetically.)

- [ ] **Step 6: Delete `.babelrc`**

```bash
git rm POS_Admin_Website/.babelrc
```

- [ ] **Step 7: Delete `public/index.html`**

```bash
git rm POS_Admin_Website/public/index.html
```

- [ ] **Step 8: Reinstall dependencies**

```bash
cd c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website
npm install
```

Expected: `react-scripts` is uninstalled, `vite` and `@vitejs/plugin-react` are installed. The `package-lock.json` will change substantially — that is expected and goes in this commit.

If `npm install` errors with peer-dep complaints, re-run with `--legacy-peer-deps`. The kitchen-sink `dependencies` list has known peer-dep tension; this is acceptable.

- [ ] **Step 9: Stage and commit migration switch**

```bash
git add \
  POS_Admin_Website/src/services/api.js \
  POS_Admin_Website/src/services/posAPI.js \
  POS_Admin_Website/src/environment.jsx \
  POS_Admin_Website/package.json \
  POS_Admin_Website/package-lock.json
git status --short
```

If Task 1 found additional `process.env.*` / `PUBLIC_URL` consumers, add those file paths to the `git add` line above as well. If `git rm` from Steps 6–7 has already staged the deletes, they will appear in the status output.

Expected staged files:
- `M  POS_Admin_Website/src/services/api.js`
- `M  POS_Admin_Website/src/services/posAPI.js`
- `M  POS_Admin_Website/src/environment.jsx`
- `M  POS_Admin_Website/package.json`
- `M  POS_Admin_Website/package-lock.json`
- `D  POS_Admin_Website/.babelrc`
- `D  POS_Admin_Website/public/index.html`
- (plus any extras from Task 1)

```bash
git commit -m "$(cat <<'EOF'
feat: switch build tool from CRA to Vite

- Replace react-scripts with vite + @vitejs/plugin-react
- Wire api.js, posAPI.js, environment.jsx to import.meta.env.VITE_*
- Drop CRA artefacts: .babelrc, public/index.html, homepage, browserslist
- Scripts: start/build/test/eject -> dev/build/preview

Co-Authored-By: Claude Opus 4.7 (1M context) <noreply@anthropic.com>
EOF
)"
```

Expected: one commit covering the migration switch.

---

## Task 6: Verify dev boot

**Files:** none modified unless an error needs a fix.

**Goal:** Confirm `npm run dev` boots cleanly and the sign-in page renders. This is the first real test that the migration succeeded.

- [ ] **Step 1: Start dev server**

```bash
cd c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website
npm run dev
```

Expected: Vite prints something like:

```
  VITE v5.x.x  ready in NNN ms

  ➜  Local:   http://localhost:3000/
  ➜  Network: http://<lan-ip>:3000/
```

A browser tab opens to `http://localhost:3000/`. The sign-in page renders.

- [ ] **Step 2: Check the browser console**

Open DevTools console. Expected: no red errors that prevent rendering. Yellow warnings (e.g. about React Router future flags, deprecated lifecycles in legacy admin-template components) are acceptable noise.

- [ ] **Step 3: Address common startup failures (if any)**

Three common failure shapes for this codebase, and the fix for each:

1. **`Failed to resolve import` on a CommonJS-only dependency** (e.g. `jquery`, `dragula`, `boxicons`):
   Edit `vite.config.js` and add the offending package name to `optimizeDeps.include`:
   ```js
   optimizeDeps: {
     include: ['jquery', 'dragula'],
     esbuildOptions: { loader: { '.js': 'jsx' } },
   },
   ```
   Restart dev server.

2. **`process is not defined`** at runtime:
   This means a stray `process.env.X` reference was missed in Task 5. Grep the console error's stack frame, locate the file, rewrite to `import.meta.env.X`, save, HMR will reload.

3. **`Uncaught ReferenceError: global is not defined`**:
   Some old CommonJS deps assume Node globals. Add to `vite.config.js`:
   ```js
   define: {
     global: 'globalThis',
   },
   ```
   Restart dev server.

Apply only fixes that are actually triggered. Do not pre-emptively add config you don't need.

- [ ] **Step 4: Commit any fixes (only if Step 3 needed changes)**

```bash
git add POS_Admin_Website/vite.config.js
# plus any source files touched
git status --short
git commit -m "$(cat <<'EOF'
fix: resolve Vite dev-server startup errors

<list the specific errors fixed>

Co-Authored-By: Claude Opus 4.7 (1M context) <noreply@anthropic.com>
EOF
)"
```

If Step 3 needed no changes, skip this step.

- [ ] **Step 5: Stop dev server**

Stop the dev server (Ctrl+C in its terminal, or kill the background process).

---

## Task 7: Verify smoke routes

**Files:** none modified unless an error needs a fix.

**Goal:** Confirm key routes render under Vite dev and the network panel shows the correct (dev) `VITE_POS_API_URL`.

- [ ] **Step 1: Start dev server**

```bash
npm run dev
```

- [ ] **Step 2: Sign-in flow**

Manually attempt sign-in. If `https://localhost:44392/api` is reachable from your machine, the auth flow should complete and place a JWT in `localStorage`. If the API is not reachable (port 44392 not running locally), document that and proceed to Step 3 — we cannot smoke-test routes that require auth, but routes that render before auth (sign-in itself) still validate the migration.

If `RequireAuth` blocks unauthenticated route access, briefly bypass for smoke purposes:
- Open the dev server URL with a fragment that targets a public route, OR
- Set a fake JWT in `localStorage` to satisfy `AuthContext` (only if the auth check is purely existence-based).

Do not commit any auth bypass — this is a runtime-only diagnostic.

- [ ] **Step 3: Visit smoke routes**

Navigate to (HashRouter, so URLs use `#/`):
- `http://localhost:3000/#/dashboard`
- `http://localhost:3000/#/product-list` (or whichever route renders the productlist component)
- `http://localhost:3000/#/sales-list`
- `http://localhost:3000/#/inventory/productlist`

(If the exact path differs from the above, consult `src/Router/router.link.jsx` for the canonical paths in this project.)

Each page must render its main content without console-blocking errors.

- [ ] **Step 4: Confirm network panel base URLs**

In DevTools Network panel, confirm:
- POS-domain XHR requests go to `https://localhost:44392/api/...` (dev value of `VITE_POS_API_URL`).
- Portal-domain XHR requests go to `https://tmis.co.za/TMIS_Portal/Portal_Api/api/...` (`VITE_PORTAL_API_URL`).

If either is wrong (e.g. requests go to `undefined/...`), the env var was not picked up. Diagnostic:
- Restart the dev server (Vite reads `.env*` once at start).
- Confirm the env file path matches: `c:/Sandbox/TMIS_POS_Admin/POS_Admin_Website/.env`, not the parent directory.
- Confirm the var name is exactly `VITE_POS_API_URL` (typos in the file or the `import.meta.env` call cause silent `undefined`).

- [ ] **Step 5: Commit any fixes**

If Step 4 surfaced a bug requiring code changes, commit with explicit paths and a message describing what was fixed.

If everything worked, no commit.

- [ ] **Step 6: Stop dev server**

---

## Task 8: Verify build & preview

**Files:** none modified unless an error needs a fix.

**Goal:** Confirm `npm run build` produces a working build and `npm run preview` serves it with the production env values.

- [ ] **Step 1: Build**

```bash
npm run build
```

Expected: Vite prints a build report listing chunk sizes and writes to `dist/`. No errors.

If errors fire, the most common are:
- **Lib that worked in dev fails in build** — Vite uses Rollup for build, which is stricter than esbuild dev. Fix by adding to `vite.config.js` `build.commonjsOptions.include` or `optimizeDeps.include`.
- **Asset import fails** — large image/asset paths that worked in CRA may need updating. Vite supports `import imgUrl from './foo.png'` natively.

Address only failures that actually fire. Commit any fix with explicit paths.

- [ ] **Step 2: Preview**

```bash
npm run preview
```

Expected: Vite serves `dist/` on `http://localhost:4173/` (default preview port). A browser does NOT auto-open in preview; navigate manually.

- [ ] **Step 3: Smoke test preview**

Repeat Task 7 Steps 3–4 against the preview URL. Confirm:
- Routes render.
- POS-domain XHRs go to `https://tmis.co.za/pos_bs/api/...` (the **production** value of `VITE_POS_API_URL`).
- Portal-domain XHRs unchanged.

This validates that `.env.production` actually overrides `.env.development` during build.

- [ ] **Step 4: Stop preview server**

- [ ] **Step 5: Commit any fixes**

If Steps 1 or 3 surfaced bugs that required edits, commit with explicit paths and a message describing what was fixed.

---

## Task 9: Final cleanup

**Files:**
- Possibly modify: `POS_Admin_Website/docs/superpowers/specs/2026-04-28-cra-to-vite-migration-design.md` (only if Tasks 6–8 forced design deviations worth recording)
- Possibly modify: `POS_Admin_Website/vite.config.js` (only if its inline comments now reference resolved issues that should be tightened)

- [ ] **Step 1: Diff against the spec**

Compare what was actually changed against the "Files added/modified/deleted" lists in the design spec. Note any deviation:
- Did `optimizeDeps.include` end up containing extra packages?
- Did any extra `process.env.*` consumer turn up that the spec did not anticipate?
- Did `define: { global: 'globalThis' }` get added?

- [ ] **Step 2: Update the spec if deviations are material**

If real deviations exist, append a "Deviations during implementation" section to the spec describing each one and why. Do not rewrite the body of the spec — append only.

- [ ] **Step 3: Confirm git working tree is clean (in scope)**

```bash
git status --short
```

The pre-existing unstaged `D` entries from before the migration should still be present and untouched. The migration's own additions and deletions should all be committed.

- [ ] **Step 4: Final smoke test**

```bash
npm run dev
```

Expected: dev server boots clean, sign-in page renders. Stop the server.

```bash
npm run build
npm run preview
```

Expected: build produces `dist/`, preview serves it cleanly. Stop the preview.

- [ ] **Step 5: Commit final spec update (if any) and announce completion**

```bash
git add POS_Admin_Website/docs/superpowers/specs/2026-04-28-cra-to-vite-migration-design.md
git commit -m "$(cat <<'EOF'
docs: record migration deviations in spec

<short list of what diverged>

Co-Authored-By: Claude Opus 4.7 (1M context) <noreply@anthropic.com>
EOF
)"
```

Skip if no deviations.

Migration is complete. The codebase now boots via Vite on port 3000, builds via `npm run build`, and uses environment files for per-environment configuration. CRA artefacts have been removed.

---

## Spec coverage check (self-review)

| Spec requirement | Implementing task |
|---|---|
| Add `vite.config.js` | Task 3 Step 1 |
| Add root `index.html` | Task 3 Step 2 |
| Add `.env`, `.env.development`, `.env.production`, `.env.example` | Task 2 Steps 1–4 |
| Add `jsconfig.json` | Task 3 Step 3 |
| Modify `package.json` (scripts/deps) | Task 5 Step 5 |
| Rename `src/index.js` → `src/index.jsx` | Task 4 |
| Modify `src/services/api.js` for `VITE_PORTAL_API_URL` | Task 5 Step 1 |
| Modify `src/services/posAPI.js` for `VITE_POS_API_URL` | Task 5 Step 2 |
| Modify `src/environment.jsx` for `VITE_BASE_PATH` | Task 5 Step 3 |
| Delete `public/index.html` | Task 5 Step 7 |
| Delete `.babelrc` | Task 5 Step 6 |
| Update `.gitignore` | Task 2 Step 5 |
| `process.env.*` audit + rewrite | Task 1 Step 3, Task 5 Step 4 |
| `PUBLIC_URL` audit + rewrite | Task 1 Step 4, Task 5 Step 4 |
| Verification: dev boot | Task 6 |
| Verification: smoke routes + dev API URL | Task 7 |
| Verification: build + preview + prod API URL | Task 8 |
| HashRouter retained, Web.config untouched | (no task — by omission) |
