using Library.ListManagement.Standard.DTO;
using ListManagement.models;
using ListManagement.services;
using ListManagement.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using UWPListManagement.ViewModels;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Content Dialog item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace UWPListManagement.Dialogs
{
    public sealed partial class ToDoDialog : ContentDialog
    {
        private MainViewModel _mvm;
        public ToDoDialog(MainViewModel mvm)
        {
            this.InitializeComponent();
            _mvm = mvm;

            if(mvm != null && _mvm.SelectedItem != null)
            {
                DataContext = mvm.SelectedItem;
            } else
            {
                DataContext = new ToDoDTO(new ToDo());
            }

        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            var item = new ItemViewModel(DataContext as ToDoDTO);

            _mvm.Add(item);

        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }
    }
}
