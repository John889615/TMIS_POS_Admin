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
