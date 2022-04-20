using System;
using System.Collections.Generic;
using System.Linq;

namespace ListManagement.Standard.models
{
	public class Appointment : Item
	{
		public DateTime Start { get; set; }
		public DateTime End { get; set; }

		private DateTimeOffset _boundStart;
		public DateTimeOffset BoundStart
        {
			get { return _boundStart; }
			set
			{
				_boundStart = value;
				Start = _boundStart.DateTime;

			}
        }

		private DateTimeOffset _boundEnd;
		public DateTimeOffset BoundEnd
		{
			get { return _boundEnd; }
			set
			{
				_boundEnd = value;
				End = _boundEnd.DateTime;

			}
		}
		public List<string> Attendees 
		{ 
			get
            {
				if (!string.IsNullOrWhiteSpace(AttendeesString))
                {
					return AttendeesString.Split(new char[] { ',' }).ToList();
                }
				return new List<string>();
            }
		}

		public string AttendeesString { get; set; }

		public Appointment()
        {
			//Attendees = new List<string>(); // Initialize the list to a blank list, prevents it from being null
			BoundStart = DateTime.Today;
			BoundEnd = DateTime.Today.AddDays(1);
        }


		public override string ToString()
		{
			string listAttendees = "";
			foreach (var val in Attendees)
            {
				listAttendees += val.ToString() + " ";
            }
			return $"Appointment: {Name} | Description: {Description} | From: {Start} | To: {End} | Attendees: {listAttendees}";
		}
	}
}

