/**
 * Optimisation for `document.getElementById`.  This function call may be minified and reduced to a single character. The original function cannot.
 * 
 * @param {string} id 
 * @returns HTMLElement
 */
export function getElementById(id) {
    return document.getElementById(id);
}