using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Syntra.Data.Models;
using WpfContactPersonen.ViewModel;

namespace WpfContactPersonen.Dialogs
{
    /// <summary>
    /// Interaction logic for Delete.xaml
    /// </summary>
    public partial class Delete : Window
    {
        ContactPersoonViewModel _viewModel = null;

        public ContactPersoonViewModel ViewModel { get => _viewModel; set => _viewModel = value; }
        public Delete(ContactPersoonViewModel vm)
        {
            ViewModel = vm;
            InitializeComponent();
            // DataContext = vm.CurrentCP;
            DataContext = vm;
        }

        private void DeleteAccepted(object sender, RoutedEventArgs e)
        {
            foreach(var item in ViewModel.SelectedForDeleting) 
             {
                Contactpersoon cp =
                ViewModel.CPLijst.Members.Where(t => t.ID == item.ID).FirstOrDefault();
                ViewModel.CPLijst.Members.Remove(cp);
             }
            ViewModel.SelectedForDeleting.Clear();
            ViewModel.SelectedForDelColl.Clear();
            ViewModel.ResetCPColl();
            this.Close();
        }

        private void DeleteRejected(object sender, RoutedEventArgs e)
        {
            ViewModel.SelectedForDeleting.Clear();
            ViewModel.SelectedForDelColl.Clear();
            this.Close();
        }
    }
}
