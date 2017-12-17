using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Zadatak1
{
    public class TodoSqlRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;

        public TodoSqlRepository(TodoDbContext context)
        {
            _context = context;
        }

        public TodoItem Get(Guid todoId, Guid userId)
        {
            TodoItem item = _context.TodoItems.FirstOrDefault(i=>i.Id == todoId);
            if (item.UserId != userId)
            {
                throw new TodoAccessDeniedException("Access to item denied. User is not owner of item.");
            }
            return item;
        }

        public void Add(TodoItem todoItem)
        {
            TodoItem item = _context.TodoItems.FirstOrDefault(i => i.Id == todoItem.Id);
            if (item != null)
            {
                throw new DuplicateTodoItemException("duplicate id: {id}");
            }
            _context.TodoItems.Add(todoItem);
            _context.SaveChanges();
        }

        public bool Remove(Guid todoId, Guid userId)
        {
            TodoItem itemToRemove = _context.TodoItems.FirstOrDefault(i => i.Id == todoId);
            if (itemToRemove == null)
            {
                return false;
            }

            if (itemToRemove.UserId != userId)
            {
                throw new TodoAccessDeniedException("Can't remove item. User is not owner of item.");
            }

            _context.TodoItems.Remove(itemToRemove);
            _context.SaveChanges();
            return true;
            
        }

        public void Update(TodoItem todoItem, Guid userId)
        {
            if (todoItem.UserId != userId)
            {
                throw new TodoAccessDeniedException("Can't update item. User is not owner of item.");
            }
            _context.Entry(todoItem).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public bool MarkAsCompleted(Guid todoId, Guid userId)
        {
            TodoItem item = _context.TodoItems.FirstOrDefault(i => i.Id == todoId);

            if (item.UserId != userId)
            {
                throw new TodoAccessDeniedException("Can't mark item as completed. User is not owner of item.");
            }
            bool succeeded = item.MarkAsCompleted();

            if (succeeded)
            {
                Update(item, userId);
            }
            return succeeded;
            
        }

        public List<TodoItem> GetAll(Guid userId)
        {
            List<TodoItem> todoItems = _context.TodoItems.Where(i => i.UserId == userId).OrderByDescending(i => i.DateCreated).ToList();
            return todoItems;

        }

        public List<TodoItem> GetActive(Guid userId)
        {
            List<TodoItem> activeTodoItems = _context.TodoItems.Where(i => (i.UserId == userId) && (i.DateCompleted == null)).ToList();
            return activeTodoItems;
        }

        public List<TodoItem> GetCompleted(Guid userId)
        {
            List<TodoItem> completedTodoItems = _context.TodoItems.Where(i => i.UserId == userId && i.DateCompleted != null).ToList();
            return completedTodoItems;
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId)
        {
            List<TodoItem> filteredTodoItems = _context.TodoItems.Where(i => i.UserId == userId && filterFunction(i)).ToList();
            return filteredTodoItems;
        }
    }
}
