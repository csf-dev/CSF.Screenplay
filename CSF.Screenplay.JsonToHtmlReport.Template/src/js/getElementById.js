// @flow

/**
 * Optimisation for `document.getElementById`.  This function call may be minified and reduced to a single character. The original function cannot.
 */
export function getElementById(id : string) : HTMLElement | null {
    return document.getElementById(id);
}