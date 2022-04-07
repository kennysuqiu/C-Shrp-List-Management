using System;
using UWPListManagement.Dialogs;
using UWPListManagement.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UWPListManagement
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            DataContext = new MainViewModel();
        }

        private bool isCheckBoxChecked;
        public bool IsCheckBoxChecked
        {
            get
            {
                return isCheckBoxChecked;
            }
            set
            {
                isCheckBoxChecked = value;
            }
        }

        private async void AddToDoClick(object sender, RoutedEventArgs e)
        {
            var dialog = new ToDoDialog();
            await dialog.ShowAsync();
            }

        private async void EditToDoClick(object sender, RoutedEventArgs e)
        {
            var dialog = new ToDoDialog((DataContext as MainViewModel).SelectedItem);
            await dialog.ShowAsync();
        }

        private async void AddAptClick(object sender, RoutedEventArgs e)
        {
            var dialog = new AppointmentDialog();
            await dialog.ShowAsync();
        }

        private async void EditAptClick(object sender, RoutedEventArgs e)
        {
            var dialog = new AppointmentDialog((DataContext as MainViewModel).SelectedItem);
            await dialog.ShowAsync();
        }

        private void DeleteClick(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).RemoveItem();
        }

        private void SaveClick(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).SaveState();
        }

        private void SortClick(object sender, RoutedEventArgs e)
        {
            (DataContext as MainViewModel).PrioritySort();

        }
    }
}