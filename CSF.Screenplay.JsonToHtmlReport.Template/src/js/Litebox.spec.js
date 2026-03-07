/**
 * @jest-environment jsdom
*/

import { Litebox } from "./Litebox";

test('open should show the mask', () => {
    document.body.innerHTML = htmlContent;
    const sut = new Litebox();

    sut.open('foo/bar.png', 'bar.png', 'test image', false);

    const hasOpenClass = document.querySelector('#liteboxMask').classList.contains('open');
    expect(hasOpenClass).toBe(true);
});

test('open should set the summary using the image summary', () => {
    document.body.innerHTML = htmlContent;
    const sut = new Litebox();

    sut.open('foo/bar.png', 'bar.png', 'test image', false);

    const summaryText = document.querySelector('#liteboxMask .summary').textContent;
    expect(summaryText).toBe('test image');
});

test('open should set the image src and title', () => {
    document.body.innerHTML = htmlContent;
    const sut = new Litebox();

    sut.open('foo/bar.png', 'bar.png', 'test image', false);

    const src = document.querySelector('#liteboxMask .content').src;
    expect(src).toMatch(/foo\/bar.png$/);

    const title = document.querySelector('#liteboxMask .content').title;
    expect(title).toBe('bar.png');
});

test('close should remove the open class', () => {
    document.body.innerHTML = htmlContent;
    document.querySelector('#liteboxMask').classList.add('open');
    const sut = new Litebox();

    sut.open('foo/bar.png', 'bar.png', 'test image', false);
    sut.close();

    const hasOpenClass = document.querySelector('#liteboxMask').classList.contains('open');
    expect(hasOpenClass).toBe(false);
});

const htmlContent = `<div id="liteboxMask">
    <div id="litebox">
        <fieldset class="controls">
            <button class="download" title="Download"><span>Download</span></button>
            <button class="close" title="Close"><span>Close</span></button>
        </fieldset>
        <img alt="" class="content">
        <p class="summary">Asset summary</p>
    </div>
</div>`;