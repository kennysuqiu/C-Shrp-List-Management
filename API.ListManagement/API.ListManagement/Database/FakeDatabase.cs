using ListManagement.models;

namespace API.ListManagement.Database
{
    public static class FakeDatabase
    {
        public static List<int> ints = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        public static List<double> doubles = new List<double> { 3.14, 2.80, 9.23 };

        public static List<Item> Items = new List<Item>
        {
            new Appointment{Name = "Appointment 1", Description="Appointment 1 Description"},
            new ToDo{Name = "ToDo 1", Description="ToDo 1 Description"}
        };
    }
}
