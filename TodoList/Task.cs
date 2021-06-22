using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList
{
    class Task
    {
        public string Name { get; set; }
        public string Desc { get; set; }
        public string Status { get; set; }
        public DateTime Date { get; set; }
        public Task(string name, string desc, DateTime date, string status)
        {
            Name = name;
            Desc = desc;
            Date = date;
            Status = status;
        }


    }
}
