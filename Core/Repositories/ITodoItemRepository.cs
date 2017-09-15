using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NancyTodo.Core.Repositories
{
    public interface ITodoItemRepository
    {
        IList<Models.TodoItem> GetAll();

        IList<Models.TodoItem> GetByStatus(bool completedStatus);

        Models.TodoItem Get(string id);

        Models.TodoItem Add(Models.TodoItem item);

        Models.TodoItem Complete(string id);
    }
}
