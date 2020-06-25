
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
    public partial class KlantenPage : Page
    {


       /* MainViewModel ViewM = MainWindow.ViewM;

        public MainViewModel ViewM
        {
            get { _viewM ??= new MainViewModel(); return _viewM; }
            set => _viewM = value;
        }*/
        
        
        public KlantenPage()
        {
            InitializeComponent();
            

           //DataContext = ViewM;


        }

        
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
           /* ViewM.deleteKlant(ViewM.CurrentKlant);

            KlantenGrid.Items.Refresh();*/


        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {

            //tring ContextKL = ViewM.KlantenLijst?.Export();



            SaveFileDialog saveDlg = new SaveFileDialog()
            {
                Filter = "Json files (*.json)|*.json|All files (*.*)|*.*"
            };
            if (saveDlg.ShowDialog() == true)
            {
                //ViewM.ExportToFile(saveDlg.FileName, ContextKL);
            }



        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            /*int index = ViewM.getVolgendeKlantID();
            Klant NewKlant = new Klant(index, ViewM.CurrentNaam, ViewM.CurrentAdres, ViewM.CurrentGebDatum);
            ViewM.addKlant(NewKlant);
            ViewM.UpdateKlantenColl();
            if (KlantenGrid.Items.SourceCollection == null)
            {

            }*/
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {

            var checkedValue = panel.Children.OfType<RadioButton>()
                 .FirstOrDefault(r => r.IsChecked.HasValue && r.IsChecked.Value);
            if (checkedValue != null)
            {
                /*switch (checkedValue.Name)
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
                }*/
            }


        }

        
    }
}




