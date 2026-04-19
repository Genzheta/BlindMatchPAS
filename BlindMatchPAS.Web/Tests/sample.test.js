const { validateProposalTitle, validateTechnicalStack } = require('../wwwroot/js/sample');

describe('Proposal Validation Tests', () => {
    
    test('validateProposalTitle should return true for valid titles', () => {
        expect(validateProposalTitle('Machine Learning Research')).toBe(true);
        expect(validateProposalTitle('AI in Healthcare')).toBe(true);
    });

    test('validateProposalTitle should return false for too short titles', () => {
        expect(validateProposalTitle('AI')).toBe(false);
    });

    test('validateProposalTitle should return false for too long titles', () => {
        const longTitle = 'a'.repeat(101);
        expect(validateProposalTitle(longTitle)).toBe(false);
    });

    test('validateProposalTitle should return false for empty or null titles', () => {
        expect(validateProposalTitle('')).toBe(false);
        expect(validateProposalTitle(null)).toBe(false);
        expect(validateProposalTitle(undefined)).toBe(false);
    });

    test('validateTechnicalStack should return true for valid comma-separated list', () => {
        expect(validateTechnicalStack('Python, TensorFlow, Keras')).toBe(true);
        expect(validateTechnicalStack('React, Node.js')).toBe(true);
    });

    test('validateTechnicalStack should return true for a single technology', () => {
        expect(validateTechnicalStack('C#')).toBe(true);
    });

    test('validateTechnicalStack should return false for empty or whitespace-only strings', () => {
        expect(validateTechnicalStack('')).toBe(false);
        expect(validateTechnicalStack('   ')).toBe(false);
        expect(validateTechnicalStack(',,,')).toBe(false);
    });
});
