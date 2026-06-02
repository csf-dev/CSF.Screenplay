const dotnetNamespaceQualifiedNameMatcher = /^\w+(?:\.\w+)+$/i;

export const getNameHtml = (featureOrScenario) => {
    const name = featureOrScenario ? featureOrScenario.Name : 'No name';
    if(!looksLikeDotnetNamespaceQualifiedName(name))
        return document.createTextNode(name);
    const nameParts = name.split('.');
    return document.createTextNode(nameParts.reduce((acc, next, idx) => idx == 0 ? acc + next : acc + '.\u00ad' + next, ''));
}

const looksLikeDotnetNamespaceQualifiedName = (name) => dotnetNamespaceQualifiedNameMatcher.test(name);
