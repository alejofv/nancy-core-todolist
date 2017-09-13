using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NancyTodo.Core.Models
{
    public class TodoItem
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public bool IsCompleted { get; set; }
    }
}
