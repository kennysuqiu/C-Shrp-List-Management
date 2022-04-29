using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.ListManagement.Standard.DTO;

namespace ListManagement.models
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
            BoundStart = DateTime.Today;
            BoundEnd = DateTime.Today.AddDays(1);
        }



        public Appointment(AppointmentDTO dto)
        {
            BoundStart = dto.Start;
            BoundEnd = dto.End;

            Name = dto.Name;
            Description = dto.Description;

            Id = dto.Id;

            AttendeesString = dto.AttendeesString;
        }

        public override string ToString()
        {
            return $"{Name} {Description} From {Start} to {End}";
        }
    }
}
