using Library.ListManagement.Standard.DTO;
using ListManagement.models;
using ListManagement.services;
using ListManagement.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using UWPListManagement.services;

namespace UWPListManagement.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private ItemServiceProxy itemService = new ItemServiceProxy();
        private void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
        }

        public ObservableCollection<ItemViewModel> Items
        {
            get
            {
                return itemService.Items;
            }
        }

        public ItemViewModel SelectedItem
        {
            get; 
            set;
        }

        public async void Add(ItemViewModel item)
        {
            await itemService.Add(item);

            Refresh();
        }
        public async void AddApt(ItemViewModel item)
        {
            await itemService.AddApt(item);

            Refresh();
        }

        public void Refresh()
        {
            NotifyPropertyChanged("Items");
        }

        public async void DeleteToDo()
        {
            await itemService.DeleteToDo(this.SelectedItem.Id);
            Refresh();
        }

        public async void DeleteApt()
        {
            await itemService.DeleteApt(this.SelectedItem.Id);
            Refresh();
        }


        private void Load(string path)
        {
            MainViewModel mvm;
            if (File.Exists(path))
            {
                try
                {
                    mvm = JsonConvert
                    .DeserializeObject<MainViewModel>(File.ReadAllText(path));

                    SelectedItem = mvm.SelectedItem;

                }
                catch (Exception)
                {
                    File.Delete(path);
                }

            }
        }

        public void Save()
        {
            //itemService.Save();
        }
    }
}
