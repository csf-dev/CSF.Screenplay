import { getNameHtml } from "./getNameHtml";

test('getNameHtml should return a node containing No name if the feature or scenario is null', () => {
    expect(getNameHtml(null).textContent).toBe('No name');
});

test('getNameHtml should return a node containing No name if the feature or scenario has no name', () => {
    expect(getNameHtml({}).textContent).toBe('No name');
});

test('getNameHtml should return a node containing the name if the feature or scenario has a name which does not look like a .NET namespace qualified name', () => {
    expect(getNameHtml({Name: 'Sample name'}).textContent).toBe('Sample name');
});

test('getNameHtml should return a node containing inserted soft hyphens if the feature or scenario has a name which looks like a .NET namespace qualified name', () => {
    expect(getNameHtml({Name: 'This.Is.A.Namespace.ClassName.MethodName'}).textContent).toBe('This.\u00adIs.\u00adA.\u00adNamespace.\u00adClassName.\u00adMethodName');
});