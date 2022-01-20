using System;
namespace ListManagement.models
{
	public class Item
	{
		public string Name { get; set; }
		public string Description { get; set; } // <- this is a property

		public override string ToString()
		{
			return $"Task Name: {Name} | Description: {Description}";
		}
	}
}

