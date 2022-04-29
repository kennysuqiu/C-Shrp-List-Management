using ListManagement.interfaces;
using Library.ListManagement.Standard.utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ListManagement.models
{
    [JsonConverter(typeof(ItemJsonConverter))]
    public class Item : INotifyPropertyChanged
    {
        private string _name;
        public string Name { 
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("Name");
                NotifyPropertyChanged("Description");
            }
        
        }
        private string _description;
        public string Description 
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
                NotifyPropertyChanged();
                NotifyPropertyChanged("Name");
                NotifyPropertyChanged("Description");
            }

        }

        internal void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public int Id { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public override string ToString()
        {
            return $"{Id} {Name} {Description}";
        }
    }
}
