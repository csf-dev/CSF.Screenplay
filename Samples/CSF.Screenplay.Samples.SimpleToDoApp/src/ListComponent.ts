import { map } from "rxjs";
import type { ListOfItems } from "./items";
import type { TodoItem } from "./items/TodoItem";

export class ListComponent {
    #listElement = document.getElementById('items_list') as HTMLOListElement;
    #itemTemplate = document.getElementById('item_template') as HTMLTemplateElement;

    #getItemElements(items : TodoItem[]) : HTMLLIElement[] {
        return items.map(x => this.#getItemElement(x));
    }

    #getItemElement(item : TodoItem) : HTMLLIElement {
        const templateContent = document.importNode(this.#itemTemplate.content, true);
        const title = templateContent.querySelector('h3') as HTMLHeadingElement;
        title.textContent = item.Name || 'Unnamed item';
        const detail = templateContent.querySelector('.detail') as HTMLDivElement;
        detail.textContent = item.Detail;
        const priority = templateContent.querySelector('.priority') as HTMLLabelElement;
        priority.textContent = `${item.Priority} priority`;
        const completed = templateContent.querySelector('.completed') as HTMLLabelElement;
        if(item.Completed) {
            completed.textContent = `Completed on ${item.Completed.getFullYear()}-${item.Completed.getMonth() + 1}-${item.Completed.getDate()}`;
        }
        else {
            completed.textContent = '';
        }
        
        return templateContent.firstElementChild! as HTMLLIElement;
    }

    setupSubscriptions() {
        this.items.list.pipe(map(x => this.#getItemElements(x))).subscribe(items => this.#listElement.replaceChildren(...items));
    }

    constructor(private readonly items : ListOfItems) {}
}