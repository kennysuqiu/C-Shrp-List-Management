using Library.ListManagement.Standard.DTO;
using ListManagement.interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListManagement.models
{
    public class ToDo: Item
    {
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; }
        private DateTimeOffset _boundDeadline;
		public DateTimeOffset BoundDeadline
		{
			get { return _boundDeadline; }
			set
			{
				_boundDeadline = value;
				Deadline = _boundDeadline.DateTime;

			}
		}

        public override string ToString()
        {
            return $"{Name} {Description} Completed: {IsCompleted}";
        }

        public ToDo()
        {

        }

        public ToDo(ToDoDTO dto)
        {
            BoundDeadline = dto.Deadline;
            IsCompleted = dto.IsCompleted;

            Name = dto.Name;
            Description = dto.Description;

            Id = dto.Id;
        }
    }
}
