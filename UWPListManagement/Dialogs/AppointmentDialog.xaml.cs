using System.Collections.ObjectModel;
using System.Linq;
using ListManagement.models;
using ListManagement.services;
using ListManagement.ViewModels;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPListManagement.Dialogs
{
    public sealed partial class AppointmentDialog : ContentDialog {
        private ObservableCollection<ItemViewModel> _toDoCollection;


        public AppointmentDialog()
        {
            this.InitializeComponent();
            _toDoCollection = ItemService.Current.Items;

            DataContext = new Appointment();
        }

        public AppointmentDialog(ItemViewModel item)
        {
            this.InitializeComponent();
            _toDoCollection = ItemService.Current.Items;
            DataContext = item;
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var item = new ItemViewModel(DataContext as Appointment);
            if (_toDoCollection.Any(i => i.Id == item.Id))
            {
                var itemToUpdate = _toDoCollection.FirstOrDefault(i => i.Id == item.Id);
                var index = _toDoCollection.IndexOf(itemToUpdate);
                _toDoCollection.RemoveAt(index);
                _toDoCollection.Insert(index, item);
            }
            else
            {
                ItemService.Current.Add(DataContext as Appointment);
            }
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
