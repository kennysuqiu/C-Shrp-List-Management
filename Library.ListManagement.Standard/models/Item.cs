using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.ListManagement.Standard.utilities;
using Newtonsoft.Json;

namespace ListManagement.models
{
    public class Item
    {
        [JsonConverter(typeof(ItemJsonConverter))]
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }

        }
        public double Priority { get; set; } 
        public string Description { get; set; }

        public int Id { get; set; }
        public override string ToString()
        {
            return $"{Name} {Description}";
        }
    }
}