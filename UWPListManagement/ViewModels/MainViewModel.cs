using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Library.ListManagement.services;
using ListManagement.models;

namespace UWPListManagement.ViewModels
{
    public class MainViewModel: INotifyPropertyChanged
    {
        private ItemService itemService = ItemService.Current;
        public ObservableCollection<Item> Items 
        {
            get
            {
                return itemService.Items;
                // items.Clear();
                // itemService.Items.ForEach(items.Add);
                // return items;
                // return new ObservableCollection<Item>(itemService);
            }
        }

        private Item selectedItem;

        public event PropertyChangedEventHandler PropertyChanged;

        public Item SelectedItem
        {
            get; set;
        }

        public void Add(Item item)
        {
            itemService.Add(item);
        }
    }
}
