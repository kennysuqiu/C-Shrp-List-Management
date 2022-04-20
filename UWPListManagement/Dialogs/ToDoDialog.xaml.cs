using ListManagement.Standard.models;
using ListManagement.Standard.services;
using ListManagement.Standard.ViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPListManagement.Dialogs
{
    public sealed partial class ToDoDialog : ContentDialog
    {
        private ObservableCollection<ItemViewModel> _toDoCollection;
        public ToDoDialog()
        {
            this.InitializeComponent();
            _toDoCollection = ItemService.Current.Items;

            DataContext = new ToDo();
        }

        public ToDoDialog(ItemViewModel item)
        {
            this.InitializeComponent();
            _toDoCollection = ItemService.Current.Items;
            DataContext = item;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var item = new ItemViewModel(DataContext as ToDo);
            if (_toDoCollection.Any(i => i.Id == item.Id))
            {
                var itemToUpdate = _toDoCollection.FirstOrDefault(i => i.Id == item.Id);
                var index = _toDoCollection.IndexOf(itemToUpdate);
                _toDoCollection.RemoveAt(index);
                _toDoCollection.Insert(index, item);
            }
            else
            {
                ItemService.Current.Add(DataContext as ToDo);
            }

        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}