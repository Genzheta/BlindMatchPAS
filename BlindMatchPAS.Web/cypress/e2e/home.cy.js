Cypress.on('uncaught:exception', (err, runnable) => {
  // returning false here prevents Cypress from failing the test
  return false;
});

describe('BlindMatch PAS Home Page', () => {
  it('should load the home page correctly', () => {
    cy.visit('/');
    cy.contains('Smart Matching for Research Excellence').should('be.visible');
    cy.contains('Get Started').should('be.visible');
  });

  it('should navigate to login page', () => {
    cy.visit('/');
    cy.contains('Sign In').click();
    cy.url().should('include', '/Account/Login');
    cy.contains('Welcome Back').should('be.visible');
  });

  it('should navigate to register page', () => {
    cy.visit('/');
    cy.contains('Get Started').click();
    cy.url().should('include', '/Account/Register');
    cy.contains('Join BlindMatch').should('be.visible');
  });
});
