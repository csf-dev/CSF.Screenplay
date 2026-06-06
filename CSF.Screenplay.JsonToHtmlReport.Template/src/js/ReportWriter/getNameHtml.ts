import { IdentifierAndNameModel } from "../Models";

const dotnetNamespaceQualifiedNameMatcher = /^\w+(?:\.\w+)+$/i;
const noName = 'No name';

export const getNameHtml : (f : Partial<IdentifierAndNameModel> | null) => Text = (featureOrScenario) => {
    const name = featureOrScenario ? (featureOrScenario.Name || noName) : noName;
    if(!looksLikeDotnetNamespaceQualifiedName(name))
        return document.createTextNode(name);
    const nameParts = name.split('.');
    return document.createTextNode(nameParts.reduce((acc, next, idx) => idx == 0 ? acc + next : acc + '.\u00ad' + next, ''));
}

const looksLikeDotnetNamespaceQualifiedName = (name : string) => dotnetNamespaceQualifiedNameMatcher.test(name);
