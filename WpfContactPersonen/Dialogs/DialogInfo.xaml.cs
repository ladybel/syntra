using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Syntra.Data.Models;
using System.Windows.Shapes;
using WpfContactPersonen.ViewModel;
using Microsoft.Win32;

namespace WpfContactPersonen.Dialogs
{
    /// <summary>
    /// Логика взаимодействия для DialogInfo.xaml
    /// </summary>
        public partial class DialogInfo : Window
        {
            ContactPersoonViewModel _viewModel = null;
            public ContactPersoonViewModel ViewModel { get => _viewModel; set => _viewModel = value; }



        public DialogInfo(ContactPersoonViewModel vm)
        {
            ViewModel = vm;
            InitializeComponent();
            DataContext = vm.CurrentCP;
            if (ViewModel.CurrentCP.Categorie == "Fysieke contactpersoon")
            {
                voornaam.Visibility = Visibility.Visible;
                openingsuren.Visibility = Visibility.Collapsed;
                sluitingsdagen.Visibility = Visibility.Collapsed;
            }
            else if (ViewModel.CurrentCP.Categorie == "Winkel of bedrijf")
            {
                voornaam.Visibility = Visibility.Collapsed;
                openingsuren.Visibility = Visibility.Visible;
                sluitingsdagen.Visibility = Visibility.Visible;
            }
        }
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            ViewModel.UpdateGui();
            DialogResult = true;
        }



        /*private void LoadImgButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog()
            {
                Filter = "png files (*.png)|*.png|jpg files (*.jpg)|*.jpg|All files (*.*)|*.*"
            };
            if (dlg.ShowDialog() == true)
            {
                ViewModel.SelectedImage = new BitmapImage(new Uri(dlg.FileName));
            }
        }*/
    }
}

