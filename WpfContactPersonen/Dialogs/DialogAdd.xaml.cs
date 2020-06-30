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
using WpfContactPersonen.ViewModel;
using Syntra.Data.Models;

    

namespace WpfContactPersonen.Dialogs
{
    /// <summary>
    /// Interaction logic for addNew.xaml
    /// </summary>
    public partial class DialogAdd : Window
    {
        ContactPersoonViewModel _viewModel = null;
        
        public ContactPersoonViewModel ViewModel { get => _viewModel; set => _viewModel = value; }
        
        public string categ;
        public DialogAdd(ContactPersoonViewModel vm)
        {
            ViewModel = vm;
            InitializeComponent();
            DataContext = vm;
            
        }
        
        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            
           
            int ID = ViewModel.GetNextIndex();
            switch (categ)
            {
                case "Fysieke contactpersoon":
                    {
                        
                        string Naam = ContactNaam.Text;                        
                        string Voornaam = ContactVoornaam.Text;
                        string Telefoon = ContactTelefoon.Text;
                        string Adres = ContactAdres.Text;
                        string Comment = ContactOmschrijving.Text;
                        FysiekeContactpersoon fp = new FysiekeContactpersoon(ID, Naam, Voornaam, Telefoon, Adres, categ, Comment);
                        ViewModel.CPLijst.Members.Add(fp);
                        ViewModel.ResetCPColl();
                        break;
                    }
                case "Winkel of bedrijf":
                    {
                        string Naam = ContactNaam.Text;                      
                        string Telefoon = ContactTelefoon.Text;
                        string Adres = ContactAdres.Text;
                        string Comment= ContactOmschrijving.Text;
                        List<string> Openingsuren=new List<string>();
                        List<string> SluitingsDagen = new List<string>();
                        foreach (string item in ContactOpeningsuren.Items)
                        {
                            Openingsuren.Add(item);
                        }
                        foreach (string item in ContactSluitingsdagen.Items)
                        {
                            SluitingsDagen.Add(item);
                        }
                        WinkelOfBedrijf wb = new WinkelOfBedrijf(ID, Naam, categ, Telefoon, Adres, Openingsuren, SluitingsDagen, Comment);
                        ViewModel.CPLijst.Members.Add(wb);
                        ViewModel.ResetCPColl();
                        break;
                    }
              
            }
            ViewModel.UpdateGui();
            DialogResult = true;
        }

        private void KeuzeFP(object sender, RoutedEventArgs e)
        {
            voornaam.Visibility = Visibility.Visible;
            openingsuren.Visibility = Visibility.Collapsed;
            sluitingsdagen.Visibility = Visibility.Collapsed;
            ImagePanel.Visibility= Visibility.Visible;
            categ = "Fysieke contactpersoon";
        }

        private void KeuzeWB(object sender, RoutedEventArgs e)
        {
            voornaam.Visibility = Visibility.Collapsed;
            openingsuren.Visibility = Visibility.Visible;
            sluitingsdagen.Visibility = Visibility.Visible;
            ImagePanel.Visibility = Visibility.Collapsed;
            categ = "Winkel of bedrijf";
        }

       /* private void AddOpeningsuren(object sender, RoutedEventArgs e)
        {
           
        }*/

        private void addou(object sender, RoutedEventArgs e)
        {
            string Currentou = CurrentOU.Text;
            ViewModel.ContactOpeningsuren.Add(new Openingsuren(Currentou));
            ViewModel.ResetOUColl();
            ContactOpeningsuren.Items.Add(new Openingsuren(Currentou));
            CurrentOU.Text = "";
        }

        private void addsd(object sender, RoutedEventArgs e)
        {
            string Currentsd = CurrentSD.Text;
            ViewModel.ContactSluitingsDagen.Add(new SluitingsDagen(Currentsd));
            ViewModel.ResetSDColl();
            ContactSluitingsdagen.Items.Add(new SluitingsDagen(Currentsd));
            CurrentSD.Text = "";
        }

        private void CurrentOU_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


    }
}
