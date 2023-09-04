using Gobi.Test.Jr.Application;
using Gobi.Test.Jr.Domain;
using Microsoft.AspNetCore.Mvc;

namespace Gobi.Tests.Jr.WebApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoItemService _todoItemService;

        public TodoController(TodoItemService todoItemService)
        {
            _todoItemService = todoItemService;
        }

        public IActionResult Index()
        {
            var items = _todoItemService.GetAll();

            items.ForEach(t =>
            {
                var status = t.Completed == 0 ? t.status = "Em andamento" : (t.Completed == 1 ? t.status = "Finalizado" : t.status = "Atrasado");

            });

            return View(items);
        }

        public ActionResult Details(int tarefaId)
        {
            var items = _todoItemService.GetAll();
            var tarefa = items.FirstOrDefault(t => t.Id == tarefaId);

            return Json(tarefa);    
        }
        public IActionResult Add(TodoItem dados)
        {
            var add = _todoItemService.Add(dados);

            return Json(add);   
        }

        public IActionResult Edit(TodoItem dados)
        {
            var att = _todoItemService.Edit(dados);
            return Json(att);

        }
        public void Delete(int tarefaId)
        {
            _todoItemService.Delete(tarefaId);

        }
    }
}