export interface TodoItem {
    Id : number;
    Name : string,
    Detail : string,
    Priority : Priority,
    Completed : Date | null,
}

export const LowPriority = 'Low', MediumPriority = 'Medium', HighPriority = 'High';
export type Priority = typeof LowPriority | typeof MediumPriority | typeof HighPriority;
export const allPriorities = [LowPriority, MediumPriority, HighPriority];