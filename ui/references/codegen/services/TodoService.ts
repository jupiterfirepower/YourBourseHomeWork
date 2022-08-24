/* istanbul ignore file */
/* tslint:disable */
/* eslint-disable */
import type { AddToDoItem } from '../models/AddToDoItem';
import type { CombineType } from '../models/CombineType';
import type { Sorting } from '../models/Sorting';
import type { ToDoItem } from '../models/ToDoItem';
import type { TodoItemResult } from '../models/TodoItemResult';
import type { UpdateToDoItem } from '../models/UpdateToDoItem';

import type { CancelablePromise } from '../core/CancelablePromise';
import { OpenAPI } from '../core/OpenAPI';
import { request as __request } from '../core/request';

export class TodoService {

    /**
     * @returns ToDoItem Success
     * @throws ApiError
     */
    public static getTodo(): CancelablePromise<Array<ToDoItem>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/Todo',
        });
    }

    /**
     * @returns TodoItemResult Success
     * @throws ApiError
     */
    public static postTodo({
requestBody,
}: {
requestBody: AddToDoItem,
}): CancelablePromise<TodoItemResult> {
        return __request(OpenAPI, {
            method: 'POST',
            url: '/Todo',
            body: requestBody,
            mediaType: 'application/json',
        });
    }

    /**
     * @returns TodoItemResult Success
     * @throws ApiError
     */
    public static putTodo({
requestBody,
}: {
requestBody: UpdateToDoItem,
}): CancelablePromise<TodoItemResult> {
        return __request(OpenAPI, {
            method: 'PUT',
            url: '/Todo',
            body: requestBody,
            mediaType: 'application/json',
            errors: {
                404: `Not Found`,
            },
        });
    }

    /**
     * @returns ToDoItem Success
     * @throws ApiError
     */
    public static getTodoGetFiltered({
idMin,
idMax,
description,
isComplete,
createdOnUtcMin,
createdOnUtcMax,
lastModifiedOnUtcMin,
lastModifiedOnUtcMax,
page,
perPage,
sortBy,
sort,
combineWith,
}: {
idMin?: number,
idMax?: number,
description?: string,
isComplete?: boolean,
createdOnUtcMin?: string,
createdOnUtcMax?: string,
lastModifiedOnUtcMin?: string,
lastModifiedOnUtcMax?: string,
page?: number,
perPage?: number,
sortBy?: Sorting,
sort?: string,
combineWith?: CombineType,
}): CancelablePromise<Array<ToDoItem>> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/Todo/GetFiltered',
            query: {
                'Id.Min': idMin,
                'Id.Max': idMax,
                'Description': description,
                'IsComplete': isComplete,
                'CreatedOnUtc.Min': createdOnUtcMin,
                'CreatedOnUtc.Max': createdOnUtcMax,
                'LastModifiedOnUtc.Min': lastModifiedOnUtcMin,
                'LastModifiedOnUtc.Max': lastModifiedOnUtcMax,
                'Page': page,
                'PerPage': perPage,
                'SortBy': sortBy,
                'Sort': sort,
                'CombineWith': combineWith,
            },
        });
    }

    /**
     * @returns ToDoItem Success
     * @throws ApiError
     */
    public static getTodo1({
id,
}: {
id: number,
}): CancelablePromise<ToDoItem> {
        return __request(OpenAPI, {
            method: 'GET',
            url: '/Todo/{id}',
            path: {
                'id': id,
            },
            errors: {
                404: `Not Found`,
            },
        });
    }

    /**
     * @returns TodoItemResult Success
     * @throws ApiError
     */
    public static deleteTodo({
id,
}: {
id: number,
}): CancelablePromise<TodoItemResult> {
        return __request(OpenAPI, {
            method: 'DELETE',
            url: '/Todo/{id}',
            path: {
                'id': id,
            },
            errors: {
                404: `Not Found`,
            },
        });
    }

}
