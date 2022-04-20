using Library.ListManagement.helpers;
using ListManagement.Standard.models;
using ListManagement.Standard.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace ListManagement.Standard.services
{
    public class ItemService
    {
        private ObservableCollection<ItemViewModel> items;
        private ListNavigator<ItemViewModel> listNav;
        private string persistencePath;
        private JsonSerializerSettings serializerSettings
            = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        static private ItemService instance;

        public bool ShowComplete { get; set; }
        public ObservableCollection<ItemViewModel> Items
        {
            get
            {
                return items;
            }
        }

        private void LoadFromDisk()
        {

            if (File.Exists(persistencePath))
            {
                try
                {
                    var state = File.ReadAllText(persistencePath);
                    if (state != null)
                    {
                        items = JsonConvert
                        .DeserializeObject<ObservableCollection<ItemViewModel>>(state, serializerSettings) ?? new ObservableCollection<ItemViewModel>();
                    }
                }
                catch (Exception e)
                {
                    File.Delete(persistencePath);
                    items = new ObservableCollection<ItemViewModel>();
                }
            }
        }

        public string Query { get; set; }

        public IEnumerable<ItemViewModel> FilteredItems
        {
            get
            {
                var incompleteItems = Items.Where(i =>
                (!ShowComplete && !((i.BoundToDo)?.IsCompleted ?? true)) //incomplete only
                || ShowComplete);
                //show complete (all)

                var searchResults = incompleteItems.Where(i => string.IsNullOrWhiteSpace(Query)
                //there is no query
                || (i?.Name?.ToUpper()?.Contains(Query.ToUpper()) ?? false)
                //i is any item and its name contains the query
                || (i?.Description?.ToUpper()?.Contains(Query.ToUpper()) ?? false)
                //or i is any item and its description contains the query
                || ((i.BoundAppointment)?.Attendees?.Select(t => t.ToUpper())?.Contains(Query.ToUpper()) ?? false));
                //or i is an appointment and has the query in the attendees list
                return searchResults;
            }
        }

        public static ItemService Current
        {
            get
            {
                if (instance == null)
                {
                    instance = new ItemService();
                }
                return instance;
            }
        }

        private ItemService()
        {
            items = new ObservableCollection<ItemViewModel>();

            persistencePath = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\SaveData.json";
            LoadFromDisk();

            listNav = new ListNavigator<ItemViewModel>(FilteredItems, 5);
        }

        public void Add(Item i)
        {
            if (i.Id <= 0)
            {
                i.Id = nextId;
            }
            items.Add(new ItemViewModel(i));
        }

        public void Remove(Item i)
        {
            //items.Remove(i);
        }

        public void Save()
        {
            var listJson = JsonConvert.SerializeObject(Items, serializerSettings);
            if (File.Exists(persistencePath))
            {
                File.Delete(persistencePath);
            }
            File.WriteAllText(persistencePath, listJson);
        }

        public Dictionary<object, ItemViewModel> GetPage()
        {
            var page = listNav.GetCurrentPage();
            if (listNav.HasNextPage)
            {
                //page.Add("N", new Item { Name = "Next" });
            }
            if (listNav.HasPreviousPage)
            {
                //page.Add("P", new Item { Name = "Previous" });
            }
            return page;
        }

        public Dictionary<object, ItemViewModel> NextPage()
        {
            return listNav.GoForward();
        }

        public Dictionary<object, ItemViewModel> PreviousPage()
        {
            return listNav.GoBackward();
        }

        private int nextId
        {
            get
            {
                if (Items.Any())
                {
                    return Items.Select(i => i.Id).Max() + 1;
                }
                return 1;
            }
        }
    }
}