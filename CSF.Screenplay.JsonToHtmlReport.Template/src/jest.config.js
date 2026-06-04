const {defineConfig} = require('jest');

module.exports = defineConfig({
  clearMocks: true,
  collectCoverage: true,
  coverageDirectory: "TestResults",
  testEnvironment: 'jsdom',
  testEnvironmentOptions: {
    url: 'http://localhost/',
  },
  transform: {
    '\\.html$': 'jest-html-loader',
    '\\.js$': 'babel-jest'
  }
});
