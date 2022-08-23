import React, { useState } from 'react'
import { ToDoItem } from '../references/codegen/index'

type Props = { 
  saveTodo: (e: React.FormEvent, formData: ToDoItem | any) => void 
}

const AddTodo: React.FC<Props> = ({ saveTodo }) => {
  const [formData, setFormData] = useState<ToDoItem | {}>()

  const handleForm = (e: React.FormEvent<HTMLInputElement>): void => {
    setFormData({
      ...formData,
      [e.currentTarget.id]: e.currentTarget.value,
    })
  }

  return (
    <form className='Form' onSubmit={(e) => saveTodo(e, formData)}>
      <div>
        <div>
          <label htmlFor='description'>Description</label>
          <input onChange={handleForm} type='text' id='description' />
        </div>
      </div>
      <button disabled={formData === undefined ? true: false} >Add Todo</button>
    </form>
  )
}

export default AddTodo
