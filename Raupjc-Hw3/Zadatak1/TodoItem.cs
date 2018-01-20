using System;
using System.Collections.Generic;

namespace Zadatak1
{
    public class TodoItem
    {

        public Guid Id { get; set; }
        public string Text { get; set; }

        public bool IsCompleted() { return DateCompleted.HasValue; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateCompleted { get; set; }

        public Guid UserId { get; set; }
        public List<TodoLabel> Labels { get; set; }
        public DateTime? DateDue { get; set; }



        public TodoItem(string text, Guid userId)
        {
            Id = Guid.NewGuid();
            Text = text;
            DateCreated = DateTime.UtcNow;
            UserId = userId;
            Labels = new List<TodoLabel>();
        }

        public TodoItem()
        {
            // entity framework needs this one
            // not for use :)
        }

        protected bool Equals(TodoItem other)
        {
            return Id.Equals(other.Id);
            
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((TodoItem)obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public bool MarkAsCompleted()
        {
            if (!IsCompleted())
            {
                DateCompleted = DateTime.Now;
                return true;
            }
            return false;
        }

    }
}
