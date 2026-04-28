import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

// JSX-in-.js: 123 files in src/ end with .js but contain JSX.
// The esbuild + optimizeDeps blocks below tell Vite to compile JSX in .js
// files (both source and pre-bundled deps) so we don't have to rename them.
// See migration spec section "JSX in .js files".

export default defineConfig({
  plugins: [react()],
  // Shim Node's `global` to the browser's `globalThis`. Some CommonJS deps
  // (e.g. dragula via crossvent) reference `global` at module-init time,
  // which is undefined in browser ES modules. Webpack provided this shim
  // implicitly; Vite needs it spelled out.
  define: {
    global: 'globalThis',
  },
  server: {
    port: 3000,
    host: true,
    open: true,
    // Dev-only proxies. The frontend reads relative paths from
    // .env.development (VITE_PORTAL_API_URL=/portal-api,
    // VITE_POS_API_URL=/pos-api). Vite forwards them server-side to the
    // real upstreams, so the browser never makes a cross-origin request
    // and CORS / self-signed cert issues do not apply during local dev.
    // Production uses absolute URLs from .env.production (no proxy).
    proxy: {
      '/portal-api': {
        target: 'https://tmis.co.za/TMIS_Portal/Portal_Api/api',
        changeOrigin: true,
        rewrite: (path) => path.replace(/^\/portal-api/, ''),
      },
      '/pos-api': {
        target: 'https://localhost:44392/api',
        changeOrigin: true,
        secure: false, // POS API uses a self-signed cert in dev
        rewrite: (path) => path.replace(/^\/pos-api/, ''),
      },
    },
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
