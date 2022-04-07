using System;
namespace ListManagement.models
{
	public class ToDo: Item	//ToDo is a child of Item or Item is the Parent of ToDo
	{

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

		public ToDo()
        {
			BoundDeadline = DateTime.Today;

		}
		public DateTime Deadline { get; set; }	// <- this is a property
		public bool IsCompleted { get; set; }   // <- this is a property

        public override string ToString()
        {
            return $"Task Name: {Name} | Description: {Description}";
        }

    }
}

