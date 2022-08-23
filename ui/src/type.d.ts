interface ITodo {
    id: number;
    description: string;
    isComplete: boolean;
    createdOnUtc: string;
    lastModifiedOnUtc?: string | null;
}

type TodoProps = {
    todo: ITodo
}

interface SortTodoProperty {
    sortDescription: boolean;
    sortcreatedOnUtc: boolean;
    refresh: boolean;
}