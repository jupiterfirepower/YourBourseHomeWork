import React, { useEffect, useState } from 'react'
import TodoItem from './components/TodoItem'
import AddTodo from './components/AddTodo'
import { getTodos, addTodo, updateTodo, deleteTodo } from './API'
import { ToDoItem, TodoItemResult } from './references/codegen/index'

const App: React.FC = () => {
  const [todos, setTodos] = useState<ITodo[]>([])

  useEffect(() => {
    fetchTodos()
  }, [])

  const fetchTodos = (): void => {
    getTodos()
    .then((data : ToDoItem[] | any) => {
      console.log(data)
      setTodos(data)
    })
    .catch((err: Error) => console.log(err))
  }

 const handleSaveTodo = (e: React.FormEvent, formData: ToDoItem): void => {
   e.preventDefault()
   addTodo(formData)
   .then(({ id }) : TodoItemResult | any => { 
    if (id === 0) {
      throw new Error('Error! Todo not updated')
    } 
   getTodos()
   .then((data : ToDoItem[] | any) => setTodos(data))
  })
  .catch((err) => console.log(err))
}

  const handleUpdateTodo = (todo: ToDoItem): void => {
    updateTodo(todo)
    .then(({ id }) : TodoItemResult | any => {
      if (id === 0) {
          throw new Error('Error! Todo not updated')
        }
        getTodos()
        .then((data : ToDoItem[] | any) => setTodos(data))
      })
      .catch((err) => console.log(err))
  }

  const handleDeleteTodo = (_id: string): void => {
    deleteTodo(+_id)
    .then(({ id }) => {
      if (id === 0) {
          throw new Error('Error! Todo not deleted')
        }
        getTodos()
        .then((data : ToDoItem[] | any) => setTodos(data))
      })
      .catch((err) => console.log(err))
  }

  return (
    <main className='App'>
      <h1>Your Bourse Todo List</h1>
      <AddTodo saveTodo={handleSaveTodo} />
      {todos.map((todo: ITodo) => (
        <TodoItem
          key={todo.id}
          updateTodo={handleUpdateTodo}
          deleteTodo={handleDeleteTodo}
          todo={todo}
        />
      ))}
    </main>
  )
}

export default App
