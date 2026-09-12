export interface TodoItem {
    Name : string,
    Detail : string,
    Priority : Priority,
    Completed : Date,
}

export const LowPriority = 'Low', MediumPriority = 'Medium', HighPriority = 'High';
export type Priority = typeof LowPriority | typeof MediumPriority | typeof HighPriority;