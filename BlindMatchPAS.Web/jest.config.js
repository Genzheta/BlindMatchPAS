module.exports = {
  testEnvironment: 'jsdom',
  testMatch: ['**/Tests/**/*.test.js'],
  verbose: true,
  collectCoverage: true,
  coverageDirectory: 'coverage',
  coverageReporters: ['text', 'lcov']
};
