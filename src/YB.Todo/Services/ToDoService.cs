using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using YB.Todo.Contracts;
using YB.Todo.Entities;
using System.Linq;
using AutoMapper;
using YB.Todo.DtoModels;

namespace YB.Todo.Services
{
    public class ToDoService : IToDoService
    {
        private readonly IMapper _mapper;
        private IUnitOfWork _context;

        public ToDoService(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IList<ToDoItem>> GetListAsync()
        {
            var data =  await _context.ToDoRepository.GetAll();
            var result = _mapper.Map<IList<ToDoItem>>(data);

            return result.OrderBy(item => item.Id).ToList();
        }

        public async Task<ToDoItem> GetAsync(int id)
        {
            var item = await _context.ToDoRepository.GetByIdAsync(id);

            return item != null ? _mapper.Map<ToDoItem>(item) : null;
        }

        public async Task<int> AddAsync(ToDoItem todoItem)
        {
            var entity = _mapper.Map<ToDoEntity>(todoItem);

            var entityEntry = await _context.ToDoRepository.AddAsync(entity);
            await _context.CompleteAsync();

            return entityEntry.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var entity = await _context.ToDoRepository.GetByIdAsync(id);

            if (entity == null)
            {
                return false;
            }

            try
            {
                await _context.ToDoRepository.DeleteAsync(entity);
                await _context.CompleteAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return false;
            }

            return true;
        }
    }
}
