using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Nancy.Extensions;
using Nancy.IO;

namespace NancyTodo.Modules
{
    public class TodoItemsModule : Nancy.NancyModule
    {
        private readonly Core.ITodoItemRepository _repository;

        public TodoItemsModule(Core.ITodoItemRepository repository)
            : base("todo")
        {
            this._repository = repository;

            this.Get("/", x => GetAll());
            
            this.Get("/active", x => GetActive());
            
            this.Get("/completed", x => GetCompleted());
            
            this.Get("/{id}", x =>  GetById(x.id) ?? Nancy.HttpStatusCode.NotFound);

            this.Post("/", 
                x =>
                {
                    var itemTitle = RequestStream.FromStream(Request.Body).AsString();
                    var item = this.Create(itemTitle);

                    if (item == null)
                        return Nancy.HttpStatusCode.BadRequest;
                     
                    var response = (Nancy.Response)item.Id;
                    response.StatusCode = Nancy.HttpStatusCode.Created;

                    return response;
                });

            this.Put("/{id}/complete", x => this.Complete(x.id) ?? Nancy.HttpStatusCode.NotFound);
        }

        private IList<Core.Entities.TodoItem> GetAll() => this._repository.GetAll();
        
        private IList<Core.Entities.TodoItem> GetActive() => this._repository.GetByStatus(false);
        
        private IList<Core.Entities.TodoItem> GetCompleted() => this._repository.GetByStatus(true);
        
        private Core.Entities.TodoItem GetById(string id) => this._repository.Get(id);
        
        private Core.Entities.TodoItem Create(string title)
        {
            if (string.IsNullOrWhiteSpace(title))
                return null;

            return this._repository.Add(new Core.Entities.TodoItem { Title = title });
        }

        private Core.Entities.TodoItem Complete(string id) => this._repository.Complete(id);
    }
}
