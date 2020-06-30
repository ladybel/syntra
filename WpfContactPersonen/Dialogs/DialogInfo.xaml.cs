using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfContactPersonen.Dialogs;
using WpfContactPersonen.ViewModel;
using Syntra.Data.Models;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
            DataContext = vm;

            if (ViewModel.CurrentCP.Categorie == "Fysieke contactpersoon")
            {
                voornaam.Visibility = Visibility.Visible;
                openingsuren.Visibility = Visibility.Collapsed;
                sluitingsdagen.Visibility = Visibility.Collapsed;
                FotoPanel.Visibility = Visibility.Visible;
            }
            else if (ViewModel.CurrentCP.Categorie == "Winkel of bedrijf")
            {
                voornaam.Visibility = Visibility.Collapsed;
                openingsuren.Visibility = Visibility.Visible;
                sluitingsdagen.Visibility = Visibility.Visible;
                FotoPanel.Visibility = Visibility.Collapsed;
            }
        }
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {

            PasAan();
            ViewModel.UpdateGui();
            DialogResult = true;
        }

        public void PasAan() { 
        
            if(ViewModel.CurrentCP.Categorie=="Winkel of bedrijf")
            {
                
                var cp = (WinkelOfBedrijf)ViewModel.CurrentCP;
                cp.Openingsuren.Clear();
                cp.SluitingsDagen.Clear();

                foreach (var item in ViewModel.ContactOpeningsuren) {
                    cp.Openingsuren.Add(item.Omschrijving);
                }
                foreach (var item in ViewModel.ContactSluitingsDagen)
                {
                    cp.SluitingsDagen.Add(item.Omschrijving);
                }
                ViewModel.CurrentCP = cp;
            }
            if(ViewModel.CurrentCP.Categorie == "Fysieke Contactpersoon")
            {
                var cp = (FysiekeContactpersoon)ViewModel.CurrentCP;
                cp.Voornaam = ContactVoornaam.Text;
                ViewModel.CurrentCP = cp;
            }
            
        }

        private void DeleteOpeningsuren_ButtonClick(object sender, RoutedEventArgs e)
            {
            if (ViewModel.CurrentCP.Categorie == "Winkel of bedrijf")
            {

                var currou = ViewModel.ContactOpeningsuren.Where(t => t== ViewModel.CurrOU).FirstOrDefault();                
                ViewModel.ContactOpeningsuren.Remove(currou);
                ViewModel.ResetOUColl();
                ViewModel.UpdateGui();

            }
        }

        private void DeleteSluitingsDagen_ButtonClick(object sender, RoutedEventArgs e)
        {
            if (ViewModel.CurrentCP.Categorie == "Winkel of bedrijf")
            {

                var currsd = ViewModel.ContactSluitingsDagen.Where(t => t== ViewModel.CurrSD).FirstOrDefault();
                ViewModel.ContactSluitingsDagen.Remove(currsd);
                ViewModel.ResetSDColl();
                ViewModel.UpdateGui();
            }
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

