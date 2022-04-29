using Api.ToDoApplication.Persistence;
using API.ListManagement.database;
using Library.ListManagement.Standard.DTO;
using ListManagement.models;
using ListManagement.services;

namespace API.ListManagement.EC
{
    public class AppointmentEC
    {
        public IEnumerable<AppointmentDTO> Get()
        {
            return FakeDatabase.Appointments.Select(t => new AppointmentDTO(t));
            //return Filebase.Current.Appointments.Select(t => new AppointmentDTO(t
        }

        public AppointmentDTO AddOrUpdate(AppointmentDTO apt)
        {
            if (apt.Id <= 0)
            {
                //CREATE
                apt.Id = ItemService.Current.NextId;
                //FakeDatabase.Appointments.Add(new Appointment(apt));
                Filebase.Current.AddOrUpdate(new Appointment(apt));
            }
            else
            {

            }
            {
                //UPDATE
                var itemToUpdate = Filebase.Current.Appointments.FirstOrDefault(i => i.Id == apt.Id);
                if (itemToUpdate != null)
                {
                    var index = Filebase.Current.Appointments.IndexOf(itemToUpdate);
                    Filebase.Current.Appointments.Remove(itemToUpdate);
                    Filebase.Current.Appointments.Insert(index, new Appointment(apt));
                }
                else
                {
                    //CREATE -- Fall-Back
                    Filebase.Current.Appointments.Add(new Appointment(apt));
                }
            }

            return apt;
        }
        public AppointmentDTO Delete(int id)
        {
            var aptToDelete = FakeDatabase.Appointments.FirstOrDefault(i => i.Id == id);
            if (aptToDelete != null)
            {
                FakeDatabase.Appointments.Remove(aptToDelete);
            }

            return new AppointmentDTO(aptToDelete);
        }
    }
}
