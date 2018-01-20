using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zadatak1
{
    /// <summary >
    /// Label describing the TodoItem .
    /// e.g. Critical , Personal , Work ...
    /// </ summary >
    public class TodoLabel
    {
        public Guid Id { get; set; }
        public string Value { get; set; }

        /// <summary >
        /// All TodoItems that are associated with this label
        /// </ summary >
        public List<TodoItem> LabelTodoItems { get; set; }

        public TodoLabel(string value)
        {
            Id = Guid.NewGuid();
            Value = value;
            LabelTodoItems = new List<TodoItem>();
        }

        public TodoLabel()
        {
            // entity framework needs this one
            // not for use :)
        }

        protected bool Equals(TodoLabel other)
        {
            return Id.Equals(other.Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TodoLabel) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }
    }
}
