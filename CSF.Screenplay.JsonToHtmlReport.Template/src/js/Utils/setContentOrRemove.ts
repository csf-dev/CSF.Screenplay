import { querySelectorNotNull } from "./html";

/**
 * If the {@link predicate} function, executed upon the {@link dataContext} evaluates to `true`, then this function
 * sets the `textContent` of the HTML element exposed by the {@link elementSelector} (from the perspective of the
 * {@link baseElement}) to a string which is created by executing {@link contentSelector} upon the data context.
 * 
 * If the predicate function returns `false`, then the element revealed by the element selector is removed from the DOM instead.
 * 
 * @param baseElement The element from which to execute the {@link elementSelector}
 * @param dataContext The a current data context, upon which to execute the {@link predicate} and {@link contentSelector}
 * @param elementSelector A string selector, used like `baseElement.querySelector(elementSelector)`
 * @param predicate A predicate function to determine whether to write content or remove the element
 * @param contentSelector A content selector function
 */
export function setContentOrRemove<TContext extends {}>(baseElement : ParentNode,
                                                        dataContext : TContext,
                                                        elementSelector : string,
                                                        predicate : (c : TContext) => boolean,
                                                        contentSelector : (c : TContext) => string) {
    const element = querySelectorNotNull(elementSelector, baseElement);
    if(predicate(dataContext)) element.textContent = contentSelector(dataContext);
    else element.remove();
}