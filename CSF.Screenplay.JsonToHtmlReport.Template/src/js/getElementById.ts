/**
 * Optimisation for `document.getElementById`.  This function call may be minified and reduced to a single character. The original function cannot.
 */
export function getElementById(id : string) {
    return document.getElementById(id);
}