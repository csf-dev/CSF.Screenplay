export function setContentOrRemove(baseElement, dataContext, elementEelector, predicate, contentSelector) {
    const element = baseElement.querySelector(elementEelector);
    if(predicate(dataContext)) element.textContent = contentSelector(dataContext);
    else element.remove();
}