import {BehaviorSubject, map, Observable, share} from "rxjs";
import { MediumPriority } from "./TodoItem";
import type { TodoItem } from "./TodoItem";

export interface ListOfItems {
    get list() : Observable<TodoItem[]>;

    get selectedItem() : Observable<TodoItem | null>;

    get hasSelectedItem() : Observable<boolean>;

    deleteSelectedItem() : void;

    newItem() : void;

    saveSelectedItem() : void;

    updateSelectedItem(item : Partial<TodoItem>) : void;
}

export class ItemList implements ListOfItems {
    #list = new BehaviorSubject<TodoItem[]>([]);
    #selectedItem = new BehaviorSubject<TodoItem | null>(null);
    #nextId = 1;

    get list() { return this.#list.asObservable(); }

    get selectedItem() { return this.#selectedItem.asObservable(); }

    get hasSelectedItem() { return this.#selectedItem.pipe(map(x => x != null), share()); }

    #getItems() { return [...this.#list.getValue()]; }

    deleteSelectedItem() {
        const currentlySelected = this.#selectedItem.getValue();
        if(!currentlySelected) return;

        const items = this.#getItems();
        this.#list.next(items.filter(x => x.Id != currentlySelected.Id));
    }

    newItem() {
        const item : TodoItem = {
            Id: this.#nextId++,
            Name: '',
            Detail: '',
            Priority: MediumPriority,
            Completed: null
        };
        const items = this.#getItems();
        items.push(item);
        this.#list.next(items);

        const selectedItem = {...item};
        this.#selectedItem.next(selectedItem);
    }

    saveSelectedItem() {
        const currentlySelected = this.#selectedItem.getValue();
        if(!currentlySelected) return;

        const items = this.#getItems();
        const selectedIndex = items.findIndex(x => x.Id == currentlySelected.Id);
        items.splice(selectedIndex, 1, {...currentlySelected});
        this.#list.next(items);
    }

    updateSelectedItem(item : Partial<TodoItem>) {
        const selectedItem = this.#selectedItem.getValue();
        if(!selectedItem) return;
        for(const prop of Object.getOwnPropertyNames(selectedItem) as [k : keyof TodoItem]) {
            if(Object.hasOwn(item, prop)) {
                const replacementVal = item[prop];
                (selectedItem as any)[prop] = replacementVal;
            }
        }
    }
}