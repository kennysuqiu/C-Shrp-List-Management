using ListManagement.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Library.ListManagement.Standard.DTO
{
    public class AppointmentDTO: ItemDTO
    {
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset End { get; set; }

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

        public AppointmentDTO(Item i) : base(i)
        {
            var a = i as Appointment;
            if(a != null)
            {
                Start = a.Start;
                End = a.End;

                AttendeesString = a.AttendeesString;
            }
        }

        public AppointmentDTO()
        {

        }
    }
}
