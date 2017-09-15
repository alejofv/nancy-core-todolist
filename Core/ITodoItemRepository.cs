using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NancyTodo.Core
{
    public interface ITodoItemRepository
    {
        IList<Entities.TodoItem> GetAll();

        IList<Entities.TodoItem> GetByStatus(bool completedStatus);

        Entities.TodoItem Get(string id);

        Entities.TodoItem Add(Entities.TodoItem item);

        Entities.TodoItem Complete(string id);
    }
}
