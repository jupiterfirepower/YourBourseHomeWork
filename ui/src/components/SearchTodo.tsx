import React, { useState } from 'react'

type Props = { 
  searchTodo: (e: React.FormEvent, formData: ISearchTodoProperty | any) => void 
}

const SearchTodo: React.FC<Props> = ({ searchTodo }) => {
  const [formData, setFormData] = useState<ISearchTodoProperty | {}>()

  const handleForm = (e: React.FormEvent<HTMLInputElement>): void => {
    setFormData({
      ...formData,
      [e.currentTarget.id]: e.currentTarget.value,
    })
  }

  return (
    <form className='Form' onSubmit={(e) => searchTodo(e, formData)}>
      <div>
        <div>
          <label htmlFor='description'>Description</label>
          <input onChange={handleForm} type='text' id='description' />
        </div>
      </div>
      <button disabled={false} >Search on Server</button>
    </form>
  )
}

export default SearchTodo
