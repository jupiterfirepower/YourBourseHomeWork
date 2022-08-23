/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */

export type ToDoItem = {
    id: number;
    description: string;
    isComplete: boolean;
    createdOnUtc: string;
    lastModifiedOnUtc?: string | null;
};
