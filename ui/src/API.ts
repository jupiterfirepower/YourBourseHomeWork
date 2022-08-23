// Import our client
import { OpenAPI, ToDoItem, AddToDoItem, UpdateToDoItem, TodoItemResult, TodoService } from './references/codegen/index'
OpenAPI.BASE = "http://localhost:5178"; // Set this to match your local API endpoint.

export const getTodos = async (): Promise<ToDoItem[]> => {
  try {
    const todos: ToDoItem[] = await TodoService.getTodo()
    return todos
  } 
  catch(error){
    let result = (error as Error).message;
    throw new Error(result)
  }
}

export const addTodo = async (
  formData: ToDoItem
): Promise<TodoItemResult> => {
  try {
    const todo: Omit<AddToDoItem, 'id'> = {
      description: formData.description,
      isComplete: false
    }
    const saveTodo: TodoItemResult = await TodoService.postTodo({ requestBody:todo })
    return saveTodo
  } catch(error){
    let result = (error as Error).message;
    throw new Error(result)
  }
}

export const updateTodo = async (
  todo: ToDoItem
): Promise<TodoItemResult> => {
  try {
    const todoUpdate: UpdateToDoItem = {
      id: todo.id,
      description: todo.description,
      isComplete: true,
    }
    const updatedTodo: TodoItemResult = await TodoService.putTodo({ requestBody:todoUpdate })
    return updatedTodo
  } catch(error){
    let result = (error as Error).message;
    throw new Error(result)
  }
}

export const deleteTodo = async (
  id: number
): Promise<TodoItemResult> => {
  try {
    const deletedTodo: TodoItemResult = await TodoService.deleteTodo({ id })
    return deletedTodo
  } catch(error){
    let result = (error as Error).message;
    throw new Error(result)
  }
}
