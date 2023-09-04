using Gobi.Test.Jr.Domain;
using Gobi.Test.Jr.Domain.Interfaces;

namespace Gobi.Test.Jr.Application
{
    public class TodoItemService
    {
        private readonly ITodoItemRepository _todoItemRepository;

        public TodoItemService(ITodoItemRepository todoItemRepository)
        {
            _todoItemRepository = todoItemRepository;
        }

        public List<TodoItem> GetAll()
        {
            return _todoItemRepository.GetAll();
        }

        public bool Add(TodoItem dados) 
        { 
           
            return _todoItemRepository.Add(dados);  
        
        }
        public bool Edit(TodoItem dados)
        {
            return _todoItemRepository.Edit(dados);
        }
        public void Delete(int id)
        {
            _todoItemRepository.Delete(id);
        }
    }
}