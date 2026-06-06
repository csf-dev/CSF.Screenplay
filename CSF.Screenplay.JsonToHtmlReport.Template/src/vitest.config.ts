import { defineConfig } from "vitest/config";
import stringPlugin from "vite-plugin-string";

export default defineConfig({
    plugins: [stringPlugin({include: ['**/*.html']})],
    test: { environment: 'jsdom' },
});