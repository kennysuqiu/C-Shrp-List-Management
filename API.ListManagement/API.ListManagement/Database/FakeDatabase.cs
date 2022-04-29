using ListManagement.models;

namespace API.ListManagement.database
{
    static public class FakeDatabase
    {

        public static List<Item> Appointments = new List<Item>
        {
            new Appointment{Name = "Appointment 1", Description="Appointment 1 Desc", Start=DateTime.Today, End=DateTime.Today, Id = 1, Attendees={ "Kenny", "Fernando", "Su"} },

        };

        public static List<Item> ToDos = new List<Item>
        {
            new ToDo{Name = "ToDo 1", Description="ToDo 1 Desc", IsCompleted=false, Id = 2}
        };
    }
}
