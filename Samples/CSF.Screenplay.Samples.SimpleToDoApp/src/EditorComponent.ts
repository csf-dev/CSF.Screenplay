import type { ListOfItems } from "./items";
import { allPriorities, type Priority } from "./items/TodoItem";

const disabledAttrib = 'disabled';
const clickEvent = 'click';
const changeEvent = 'change';

export class EditorComponent {
    #deleteButton = document.getElementById('DeleteItem') as HTMLButtonElement;
    #newButton = document.getElementById('NewItem') as HTMLButtonElement;
    #saveButton = document.getElementById('SaveItem') as HTMLButtonElement;
    #nameElement = document.getElementById('ItemName') as HTMLInputElement;
    #detailElement = document.getElementById('ItemDetail') as HTMLTextAreaElement;
    #priorityElement = document.getElementById('ItemPriority') as HTMLSelectElement;
    #completedElement = document.getElementById('ItemCompleted') as HTMLInputElement;

    setupSubscriptions() {
        this.items.hasSelectedItem.subscribe(val => {
            const enabled = !val;
            this.#deleteButton.toggleAttribute(disabledAttrib, enabled);
            this.#saveButton.toggleAttribute(disabledAttrib, enabled);
            this.#nameElement.toggleAttribute(disabledAttrib, enabled);
            this.#detailElement.toggleAttribute(disabledAttrib, enabled);
            this.#priorityElement.toggleAttribute(disabledAttrib, enabled);
            this.#completedElement.toggleAttribute(disabledAttrib, enabled);
        });
        this.#newButton.addEventListener(clickEvent, () => this.items.newItem());
        this.#saveButton.addEventListener(clickEvent, () => this.items.saveSelectedItem());
        this.#nameElement.addEventListener(changeEvent, ev => this.items.updateSelectedItem({Name: this.#nameElement.value}));
        this.#detailElement.addEventListener(changeEvent, ev => this.items.updateSelectedItem({Detail: this.#detailElement.value}));
        this.#priorityElement.addEventListener(changeEvent, ev => this.items.updateSelectedItem({Priority: this.#priorityElement.value as Priority}));
        this.#completedElement.addEventListener(changeEvent, ev => this.items.updateSelectedItem({Completed: new Date(Date.parse(this.#completedElement.value))}));

        for(const p of allPriorities) {
            this.#priorityElement.innerHTML += `<option>${p}</option>`;
        }
    }

    constructor(private readonly items : ListOfItems) {}
}