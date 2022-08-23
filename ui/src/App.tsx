import React, { useEffect, useState } from 'react'
import TodoItem from './components/TodoItem'
import AddTodo from './components/AddTodo'
import SortTodo from './components/SortTodo'
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

  const handleSortTodo = (e: React.FormEvent, sortProp : SortTodoProperty): void => {
    e.preventDefault()
    if(sortProp.sortDescription)
    {
      getTodos()
      .then((data : ToDoItem[] | any) => {
        data.sort((a: ToDoItem, b: ToDoItem) => (a.description > b.description) ? 1 : -1)
        setTodos(data)})
      .catch((err) => console.log(err))
    }
   }

  const handleRefreshTodo = (e: React.FormEvent): void => {
    e.preventDefault()
    getTodos()
    .then((data : ToDoItem[] | any) => setTodos(data))
    .catch((err) => console.log(err))
  }

  const handleSortCreatedTodo = (e: React.FormEvent): void => {
    e.preventDefault()
    getTodos()
    .then((data : ToDoItem[] | any) =>{
      data.sort((x:ToDoItem, y:ToDoItem) => +new Date(x.createdOnUtc) - +new Date(y.createdOnUtc))
      setTodos(data)
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
      <SortTodo sortTodo={handleSortTodo} refreshTodo={handleRefreshTodo} sortCreatedTodo={handleSortCreatedTodo}/>
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
