import { resolve } from "node:path";
import { defineConfig } from "vite";
import { viteSingleFile } from "vite-plugin-singlefile";

export default defineConfig({
    plugins: [viteSingleFile()],
    build: {
        assetsInlineLimit: 1000000,
        rolldownOptions: {
            input: { main: resolve(import.meta.dirname, 'template.html'), }
        },
        outDir: 'output'
    }
})