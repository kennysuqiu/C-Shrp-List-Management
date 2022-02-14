using System;
using ListManagement.models;

namespace ListManagement
{
	public class ItemService
	{
		static private List<Item> items;

		static private ItemService instance;

		public List<Item> Items
		{ 
			get
			{
				return items;
			} 
		}


		static public ItemService Current
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
        }

		public void Add(Item i)
        {
			items.Add(i);
        }

		public void Remove(Item i)
        {
			items.Remove(i);
        }
	}
}

