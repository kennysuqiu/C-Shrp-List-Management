using ListManagement.models;
using ListManagement.services;
using ListManagement.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;

namespace UWPListManagement.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private string persistencePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\SaveData.json";
        private JsonSerializerSettings serializerSettings
            = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        private ItemService itemService = ItemService.Current;
        private bool isSortByPriority;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<ItemViewModel> Items
        {
            get
            {
                return itemService.Items;
            }
        }

        public ItemViewModel SelectedItem
        {
            get; set;
        }

        public void Add(Item item)
        {
            itemService.Add(item);
        }
        public void RemoveItem()
        {
            if (SelectedItem != null)
            {
                //make a web call to delete this same item on the server
                Items.Remove(SelectedItem);

            }
        }

        public void SaveState()
        {
            File.WriteAllText(persistencePath, JsonConvert.SerializeObject(this, serializerSettings));
        }

        public void PrioritySort()
        {
            isSortByPriority = !isSortByPriority;
            //NotifyPropertyChanged("Items");
        }
    }
}