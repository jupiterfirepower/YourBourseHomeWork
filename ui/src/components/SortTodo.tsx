import React from 'react'

type Props = { 
  sortTodo: (e: React.FormEvent, formData: SortTodoProperty | any) => void 
  sortCreatedTodo: (e: React.FormEvent) => void
  refreshTodo: (e: React.FormEvent) => void
}

const SortTodo: React.FC<Props> = ({ sortTodo, refreshTodo, sortCreatedTodo }) => {
  let sortProperty = { sortDescription : true, sortcreatedOnUtc : false}
  return (
    <form className='Form' onSubmit={(e) => sortTodo(e, sortProperty)}>
      <div>
      <button disabled={false}>Sort By Description</button>
      <span>&ensp;</span>
      <button disabled={false} onClick={(e) => sortCreatedTodo(e)}>Sort By Created</button>
      </div>
      <button disabled={false} onClick={(e) => refreshTodo(e)}>Refresh</button>
    </form>
  )
}

export default SortTodo
