using System.Threading.Tasks;

namespace YB.Todo.Contracts
{
    public interface IUnitOfWork
    {
        IToDoRepository ToDoRepository { get; }

        Task CompleteAsync();

        /// <summary>
        /// Discards all changes that has not been commited
        /// </summary>
        void RejectChanges();
    }
}
