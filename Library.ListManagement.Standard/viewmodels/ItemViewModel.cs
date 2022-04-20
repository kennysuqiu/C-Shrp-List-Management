using ListManagement.Standard.models;

namespace ListManagement.Standard.ViewModels
{
    public class ItemViewModel
    {
        public string Name
        {
            get
            {
                return BoundItem?.Name ?? string.Empty;
            }
        }

        public string Description
        {
            get
            {
                return IsTodo
                    ? BoundToDo?.Description ?? string.Empty
                    : BoundAppointment?.Description ?? string.Empty;
            }
        }

        public string AttendeesString
        {
            get
            {
                return IsAppointment
                    ? BoundAppointment?.AttendeesString ?? string.Empty
                    : BoundAppointment?.AttendeesString ?? string.Empty;
            }
        }


        public Item BoundItem
        {
            get
            {
                if (IsTodo)
                {
                    return BoundToDo;
                }

                return BoundAppointment;
            }
        }

        public int Id
        {
            get
            {
                return BoundItem.Id;
            }
        }
        public bool IsCompleted
        {
            get
            {
                if (IsTodo)
                {
                    return BoundToDo.IsCompleted;
                }

                return false;
            }

            set
            {
                if (IsTodo)
                {
                    BoundToDo.IsCompleted = value;
                }
            }
        }

        public bool IsAppointment
        {
            get
            {
                return BoundAppointment != null;
            }
        }


        public bool IsTodo
        {
            get
            {
                return BoundToDo != null;
            }
        }

        public ToDo BoundToDo { get; set; }

        public Appointment BoundAppointment { get; set; }
        public object Priority { get; set; }

        public ItemViewModel(Item item)
        {
            if (item is Appointment)
            {
                BoundAppointment = item as Appointment;
                IsCompleted = false;
                BoundToDo = null;
            }
            else if (item is ToDo)
            {
                BoundToDo = item as ToDo;
                BoundAppointment = null;
                IsCompleted = (item as ToDo).IsCompleted;
            }
            else
            {
                
            }
        }
    }
}