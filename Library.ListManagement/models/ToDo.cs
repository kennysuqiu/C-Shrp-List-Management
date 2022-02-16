using System;
namespace ListManagement.models
{
	public class ToDo: Item	//ToDo is a child of Item or Item is the Parent of ToDo
	{
		public DateTime Deadline { get; set; }	// <- this is a property
		public bool IsCompleted { get; set; }   // <- this is a property

        public override string ToString()
        {
            return $"{Id} Task Name: {Name} | Description: {Description} | Completed: {IsCompleted} | Deadline: {Deadline.Month}/{Deadline.Day}/{Deadline.Year}";
        }

    }
}

