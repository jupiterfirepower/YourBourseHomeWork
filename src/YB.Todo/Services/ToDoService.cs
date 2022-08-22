using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using YB.Todo.Contracts;
using YB.Todo.Entities;
using System.Linq;
using AutoMapper;
using YB.Todo.DtoModels;
using System;

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

        public async Task<int> AddAsync(AddToDoItem todoItem)
        {
            var entity = _mapper.Map<ToDoEntity>(todoItem);

            entity.CreatedOnUtc = DateTime.UtcNow;
            entity.LastModifiedOnUtc = null;

            var entityEntry = await _context.ToDoRepository.AddAsync(entity);
            await _context.CompleteAsync();

            return entityEntry.Id;
        }

        public async Task<int> UpdateAsync(UpdateToDoItem todoItem)
        {
            var entity = await _context.ToDoRepository.GetByIdAsync(todoItem.Id);

            entity.Description = todoItem.Description;
            entity.IsComplete = todoItem.IsComplete;
            entity.LastModifiedOnUtc = DateTime.UtcNow;

            await _context.ToDoRepository.UpdateAsync(entity);
            await _context.CompleteAsync();

            return entity.Id;
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
