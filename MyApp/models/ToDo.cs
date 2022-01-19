using System;
namespace ListManagement.models
{
	public class ToDo
	{
		private string name;	// <- this is a field
		// C# - Style of getter and setter
		public string Name		// <- this is a property
		{
			get
			{
				return this.name;
			}
			set
			{
				if (this.name != value)
                {
					this.name = value;
				}
			}
		}
		public string Description { get; set; } // <- this is a property
		public DateTime Deadline { get; set; }	// <- this is a property
		public bool IsCompleted { get; set; }   // <- this is a property

        public override string ToString()
        {
            return $"Task Name: {Name} | Completed: {IsCompleted} | Deadline: {Deadline.Month}/{Deadline.Day}/{Deadline.Year}";
        }

    }
}

