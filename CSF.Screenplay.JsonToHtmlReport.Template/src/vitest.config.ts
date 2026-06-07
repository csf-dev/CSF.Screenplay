import { defineConfig } from "vitest/config";
import stringPlugin from "vite-plugin-string";

export default defineConfig({
    plugins: [stringPlugin({include: ['**/*.html']})],
    test: {
        environment: 'jsdom',
        coverage: { provider: 'v8', reportsDirectory: 'TestResults', reporter: ['lcovonly', "text"] },
        reporters: ['json', 'default'],
        outputFile: 'TestResults/test-results.json'
    },
});