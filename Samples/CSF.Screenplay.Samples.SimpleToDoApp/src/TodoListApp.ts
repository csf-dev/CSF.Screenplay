import { EditorComponent } from "./EditorComponent";
import { ListComponent } from "./ListComponent";
import { ItemList } from "./items";
import type { ListOfItems } from "./items";

export class TodoListApp {
    #items : ListOfItems = new ItemList();
    #editor = new EditorComponent(this.#items);
    #list = new ListComponent(this.#items);

    run() {
        this.#editor.setupSubscriptions();
        this.#list.setupSubscriptions();
    }
}