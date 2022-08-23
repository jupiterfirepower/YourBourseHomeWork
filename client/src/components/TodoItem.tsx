import React from 'react'
import { ToDoItem } from '../references/codegen/index'

type Props = TodoProps & {
    updateTodo: (todo: ToDoItem) => void
    deleteTodo: (_id: string) => void
}

const Todo: React.FC<Props> = ({ todo, updateTodo, deleteTodo }) => {
  const checkTodo: string = todo.isComplete ? `line-through` : ''
  return (
    <div className='Card'>
      <div className='Card--text'>
        <h1 className={checkTodo}>{todo.id}</h1>
        <span className={checkTodo}>{todo.description}</span>
        <span>&emsp;</span>
        <span className={checkTodo}>{todo.createdOnUtc}</span>
      </div>
      <div className='Card--button'>
        <button
          onClick={() => updateTodo(todo)}
          className={todo.isComplete ? `hide-button` : 'Card--button__done'}
        >
          Complete
        </button>
        <button
          onClick={() => deleteTodo(todo.id.toString())}
          className='Card--button__delete'
        >
          Delete
        </button>
      </div>
    </div>
  )
}

export default Todo
