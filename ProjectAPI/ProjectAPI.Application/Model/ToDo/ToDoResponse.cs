using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectAPI.Application.Model.ToDo
{
    public class ToDoResponse
    {
        public string? Name { get; set; }
        public bool IsComplete { get; set; }
    }
}
