using Microsoft.AspNetCore.Http;
using Nancy.Extensions;
using Nancy.IO;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace NancyTodo.Modules
{
    public class TodoItemsModule : Nancy.NancyModule
    {
        private readonly Core.Repositories.ITodoItemRepository _repository;

        public TodoItemsModule(Core.Repositories.ITodoItemRepository repository)
            : base("todo")
        {
            this._repository = repository;

            this.Get("/",
                x => this._repository.GetActive());

            this.Get("/all",
                x => this._repository.GetAll());

            this.Get("/{id}",
                x => this._repository.Get(x.id));

            this.Post("/",
                x =>
                {
                    var itemTitle = RequestStream.FromStream(Request.Body).AsString();
                    if (string.IsNullOrWhiteSpace(itemTitle))
                        return HttpStatusCode.BadRequest;

                    var item = this._repository.Add(new Core.Models.TodoItem { Title = itemTitle });

                    var response = (Nancy.Response)item.Id;
                    response.StatusCode = Nancy.HttpStatusCode.Created;

                    return response;
                });

            this.Put("/{id}/complete",
                x => this._repository.Complete(x.id));
        }
    }
}
