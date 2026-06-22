import { TodoListApp } from "./TodoListApp";

const readyEvent = "DOMContentLoaded";

const initPage = (ev : Event) => {
    document.removeEventListener(readyEvent, initPage);
    const app = new TodoListApp();
    app.run();
}

document.addEventListener(readyEvent, initPage);
