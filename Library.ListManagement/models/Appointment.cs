using System;
namespace ListManagement.models
{
	public class Appointment : Item
	{
		public DateTime Start { get; set; }
		public DateTime End { get; set; }
		public List<string> Attendees { get; set; }

		public Appointment()
        {
			Attendees = new List<string>();	// Initialize the list to a blank list, prevents it from being null

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

