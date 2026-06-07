/**
 * A simple optimisation for `document.getElementById`.
 * This function call may be minified and reduced to a single character.
 * The original function cannot.
 */
export function getElementById<TElement extends HTMLElement = HTMLElement>(id : string) : TElement {
    return document.getElementById(id) as TElement;
}

/**
 * Like {@link getElementById} but will throw if the element does not exist.
 */
export function getElementByIdNotNull<TElement extends HTMLElement = HTMLElement>(id : string) : TElement {
    const element = getElementById<TElement>(id);
    if(!element) throw new Error(`The HTML element with id '${id}' must exist`);
    return element;
}

/**
 * A simple optimisation for `ParentNode.querySelector`.
 * This function call may be minified and reduced to a single character.
 * The original function cannot.
 */
export function querySelector<TElement extends Element = Element>(query : string, parent? : ParentNode) : TElement {
    const p = parent || document;
    return p.querySelector(query) as TElement;
}

/**
 * Like {@link querySelector} but will throw if no element is found matching the query.
 */
export function querySelectorNotNull<TElement extends Element = Element>(query : string, parent? : ParentNode) : TElement {
    const element = querySelector<TElement>(query, parent);
    if(!element) throw new Error(`The HTML element matching query '${query}' must exist`);
    return element;
}