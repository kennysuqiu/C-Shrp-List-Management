using Library.ListManagement.helpers;
using ListManagement.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.ListManagement.services
{
    public class ItemService
    {
        private List<Item> items;
        private ListNavigator<Item> listNav;
        private JsonSerializerSettings serializerSettings
            = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        static private ItemService instance;

        static private string peristencePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
        static private string filePath = Path.Combine(peristencePath, "taskData.json");

        public bool ShowComplete { get; set; }
        public string Query { get; set; }
        public List<Item> Items
        {
            get
            {
                return items;
            }
        }

        public IEnumerable<Item> FilteredItems
        {
            get
            {
                var incompleteItems = Items.Where(i =>
                (!ShowComplete && !((i as ToDo)?.IsCompleted ?? true)) //incomplete only
                || ShowComplete);
                var searchResults = incompleteItems.Where(i => string.IsNullOrWhiteSpace(Query)
                || (i?.Name?.ToUpper().Contains(Query) ?? false)
                || (i?.Description?.ToUpper()?.Contains(Query) ?? false)
                || ((i as Appointment)?.Attendees?.Select(t => t.ToUpper())?.Contains(Query) ?? false));
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
            items = new List<Item>();

            if (File.Exists(filePath))
            {
                try
                {
                    var state = File.ReadAllText(filePath);
                    if (state.Length > 0)
                    {
                        items = JsonConvert.DeserializeObject<List<Item>>(state, serializerSettings) ?? new List<Item>();
                    }
                }
                catch (Exception e)
                {
                    File.Delete(filePath);
                    items = new List<Item>();
                }
            }

            listNav = new ListNavigator<Item>(FilteredItems, 5);
        }

        public void Add(Item i)
        {
            items.Add(i);
            if (i.Id <= 0)
            {
                i.Id = nextId;
            }
            
        }

        public void Remove(Item i)
        {
            items.Remove(i);
        }

        public void Save()
        {

            var listJson = JsonConvert.SerializeObject(Items, serializerSettings);
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            File.WriteAllText(filePath, listJson);
        }

        public Dictionary<object, Item> GetPage()
        {
            var page = listNav.GetCurrentPage();
            if (listNav.HasNextPage)
            {
                page.Add("N", new Item { Name = "Next" });
            }
            if (listNav.HasPreviousPage)
            {
                page.Add("P", new Item { Name = "Previous" });
            }
            return page;
        }

        public Dictionary<object, Item> NextPage()
        {
            return listNav.GoForward();
        }

        public Dictionary<object, Item> PreviousPage()
        {
            return listNav.GoBackward();
        }

        private int nextId
        {
            get
            {
                return Items.Select(i => i.Id).Max() + 1;
            }
        }
    }
}