using Library.ListManagement.Standard.DTO;
using ListManagement.models;
using ListManagement.ViewModels;
using UWPListManagement.ViewModels;
using Windows.UI.Xaml.Controls;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPListManagement.Dialogs
{
    public sealed partial class AppointmentDialog : ContentDialog
    {
        private MainViewModel _mvm;
        public AppointmentDialog(MainViewModel mvm)
        {
            this.InitializeComponent();
            _mvm = mvm;

            if (mvm != null && _mvm.SelectedItem != null)
            {
                DataContext = mvm.SelectedItem;
            }
            else
            {
                DataContext = new AppointmentDTO(new Appointment());
            }

        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var item = new ItemViewModel(DataContext as AppointmentDTO);

            _mvm.AddApt(item);
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
