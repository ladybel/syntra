
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
    /// Логика взаимодействия для Bookingen.xaml
    /// </summary>
    public partial class BookingPage : Page
    {
       
        MainViewModel _viewM = null;

         public MainViewModel ViewM
         {
             get { _viewM ??= new MainViewModel(); return _viewM; }
             set => _viewM = value;
         }
         
       
        public BookingPage()    
        {
          
            //InitializeComponent();
            //MasterVM.ViewM.KlantenLijst.Import();
            //MasterVM.BookViewM.BookingLijst.Import();

            DataContext = ViewM;
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            var checkedValue = panel.Children.OfType<RadioButton>()
                             .FirstOrDefault(r => r.IsChecked.HasValue && r.IsChecked.Value);
            if (checkedValue != null)
            {
                switch (checkedValue.Name)
                {
                    case "ToonAlleBookingen":
                        ViewM.ToonAlleBookingen();
                        break;
                    case "ToonBeschikbareTafels":
                        ViewM.ToonAantalBeschikbareTafels();
                        break;
                    case "PerTafel":
                        ViewM.ToonBookingenPerTafel();
                        break;

                }
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            ViewM.deleteBooking(ViewM.CurrentBooking);
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            string ContextBL = ViewM.BookingLijst?.Export();

            SaveFileDialog saveDlg = new SaveFileDialog()
            {
                Filter = "Json files (*.json)|*.json|All files (*.*)|*.*"
            };
            if (saveDlg.ShowDialog() == true)
            {
                ViewM.ExportToFile(saveDlg.FileName, ContextBL);
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            if  (ViewM.KlantenLijst.Members.Where(t => t.ID == ViewM.CurrentKlantID) != null)
            {

                if (ViewM.CurrentDatum >= DateTime.Today)
                {
                    if (isTafelVrij(ViewM.CurrentTafel, ViewM.CurrentDatum) == true)
                    {
                        {
                            int index = ViewM.getVolgendeBookingID();
                            Booking NewBooking = new Booking(index, ViewM.CurrentKlantID, ViewM.CurrentTafel, ViewM.CurrentDatum);
                            ViewM.addBooking(NewBooking);
                            ViewM.UpdateBookingColl();
                            ViewM.CurrentKlantID = 0;
                            //NieuweBookingDatum.SelectedDate = BookViewM.CurrentDatum.AddDays(1); 
                            NieuweBookingDatum.SelectedDate = null;
                            ViewM.CurrentTafel = 0;


                        }
                    }
                }
            }




        }

        public bool isTafelVrij(int tafel, DateTime datum)
        {

            var b = ViewM.BookingLijst.Members.FindAll(t => t.Tafel == tafel && t.Datum == datum);
            return (b.Count() == 0);
        }


        private void dpick_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ViewM.BeschikbareTafels = ViewM.getBeschikbareTafels(ViewM.CurrentDatum);
            ViewM.BeschikbareTafelsColl.Clear();
            foreach (var item in ViewM.BeschikbareTafels)
            {
                ViewM.BeschikbareTafelsColl.Add(item);
            }

            //BookViewM.BeschikbareTafelsColl = new ObservableCollection<int>(BookViewM.BeschikbareTafels);
        }
    }
}
