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
        <h1 className={checkTodo}>{todo.id}<span>&emsp;</span><span className={checkTodo}>{todo.description}</span></h1>
        <label className={checkTodo}>Created:</label>
        <span>&emsp;</span>
        <span className={checkTodo}>{todo.createdOnUtc}</span>
        <span>&emsp;</span>
        <label className={checkTodo}>Modified:</label>
        <span>&emsp;</span>
        <span className={checkTodo}>{todo.lastModifiedOnUtc}</span>
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
