/**
 * Validates a project proposal title.
 * @param {string} title - The title to validate.
 * @returns {boolean} - True if valid, false otherwise.
 */
function validateProposalTitle(title) {
    if (!title || typeof title !== 'string') return false;
    const trimmedTitle = title.trim();
    return trimmedTitle.length >= 5 && trimmedTitle.length <= 100;
}

/**
 * Validates the technical stack string.
 * @param {string} stack - The comma-separated stack.
 * @returns {boolean} - True if at least one technology is listed.
 */
function validateTechnicalStack(stack) {
    if (!stack || typeof stack !== 'string') return false;
    return stack.split(',').map(s => s.trim()).filter(s => s.length > 0).length > 0;
}

if (typeof module !== 'undefined' && module.exports) {
    module.exports = { validateProposalTitle, validateTechnicalStack };
}
