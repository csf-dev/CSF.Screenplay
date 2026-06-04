module.exports = {
    presets: [
        ['@babel/preset-flow', {targets: {node: 'current'}}],
        ['@babel/preset-env', {targets: {node: 'current'}}],
    ],
    plugins: [
        ["babel-plugin-syntax-hermes-parser"],
    ],
};