namespace Gobi.Test.Jr.Domain.Interfaces
{
    public interface ITodoItemRepository
    {
        List<TodoItem> GetAll();
        bool Add(TodoItem dados);
        bool Edit(TodoItem dados);
        void Delete(int id);
    }
}
