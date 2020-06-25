using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using KlantenAppWPF.ViewModel;
using Syntra.Data.Models;

namespace KlantenAppWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
          
          MainViewModel _viewM = null;
         

          public MainViewModel ViewM
          {
              get { _viewM ??= new MainViewModel(); return _viewM; }
              set => _viewM = value;
          }
        
      
        public MainWindow()
        {
            InitializeComponent();
            ViewM.KlantenLijst.Import();
            ViewM.BookingLijst.Import();
            DataContext = ViewM;
         


        }

        protected override void OnClosing(CancelEventArgs e)
        {
            
            ViewM?.BookingLijst?.SaveData();
            ViewM?.KlantenLijst?.SaveData();
            base.OnClosing(e);
        }

        private void NavigateToBookingen(object sender, RoutedEventArgs e)
        {   ViewM?.KlantenLijst?.SaveData();
            MainFrame.Navigate(new Uri("BookingPage.xaml", UriKind.RelativeOrAbsolute));            
            Bookingen.IsEnabled = false;
            Klanten.IsEnabled = true;


        }

        private void NavigateToKlanten(object sender, RoutedEventArgs e)
        {   
            ViewM?.BookingLijst?.SaveData();
            MainFrame.Navigate(new Uri("KlantenPage.xaml", UriKind.RelativeOrAbsolute));
            
            Bookingen.IsEnabled = true;
            Klanten.IsEnabled = false;
        }

/*
        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {

            string ContextKL = ViewM.KlantenLijst?.Export();
            
            

            SaveFileDialog saveDlg = new SaveFileDialog()
            {
                Filter = "Json files (*.json)|*.json|All files (*.*)|*.*"
            };
            if (saveDlg.ShowDialog() == true)
            {
                ViewM.ExportToFile(saveDlg.FileName, ContextKL);
            }

            
        
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            int index = ViewM.getVolgendeKlantID();
            Klant NewKlant = new Klant(index, ViewM.CurrentNaam, ViewM.CurrentAdres, ViewM.CurrentGebDatum);
            ViewM.addKlant(NewKlant);
            ViewM.UpdateKlantenColl();
           
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

            var checkedValue = panel.Children.OfType<RadioButton>()
                 .FirstOrDefault(r => r.IsChecked.HasValue && r.IsChecked.Value);
            if (checkedValue != null)
            {
                switch(checkedValue.Name)    
                   {
                    case "AlleKlanten":
                        ViewM.ToonAlleKlanten();
                        break;
                    case "PerTafel":
                        ViewM.ToonKlantenPerTafel();
                        break;
                    case "ZoekKlanten":
                        ViewM.ToonKlantZoekRes();
                        break;
                }
            }
         

        }

       */
    }
}
