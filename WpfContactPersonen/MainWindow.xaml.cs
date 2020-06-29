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

namespace WpfContactPersonen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ContactPersoonViewModel _viewmod = null;
        //public Contactpersoon CurrentCP;
        public ContactPersoonViewModel ViewMod
        {
            get { _viewmod ??= new ContactPersoonViewModel(); return _viewmod; }
            set => _viewmod = value;
        }
        public MainWindow()
        {
            InitializeComponent();  
            ViewMod.Import();
            DataContext = ViewMod;

        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            //List<Contactpersoon> SelectedForDeleting = new List<Contactpersoon>();

            foreach (Contactpersoon item in ContactpersonenGrid.ItemsSource)
            {
                if (((CheckBox)CheckForDelete.GetCellContent(item)).IsChecked == true)
                {
                    ViewMod.SelectedForDeleting.Add(item);
                }
            }
            if (ViewMod.SelectedForDeleting.Count > 0) { 
            Delete del = new Delete(ViewMod) { Owner = this };
            if (del.ShowDialog() == true) { }}

        }

        private void InfoButton_Click(object sender, RoutedEventArgs e)
        {
            DialogInfo dlg = new DialogInfo(ViewMod) { Owner = this };
            if (dlg.ShowDialog() == true) { }
        }

        private void NieuwButton_Click(object sender, RoutedEventArgs e)
        {
            ViewMod.NieuwCP = new Contactpersoon();
            DialogAdd add = new DialogAdd(ViewMod) { Owner = this };
            if (add.ShowDialog() == true) { }
        }

        private void Zoek(object sender, RoutedEventArgs e)
        {
            var checkedValue = panel.Children.OfType<RadioButton>().Where(r => r.IsChecked==true).FirstOrDefault();
                
            if (checkedValue != null)
                {
                switch (checkedValue.Name)
                {
                    case "ToonAllesRadioButton":
                        ViewMod.ToonAlleContactPersonen();
                        break;
                    case "CategorieKeuzeRadioButton":
                        ViewMod.ToonPerCategorie();
                        break;
                    case "ZoekRadioButton":
                        ViewMod.ToonZoekRes();  
                        break;
                }
            }
        }

        private void Export(object sender, RoutedEventArgs e)
        {
            ViewMod.CPLijst.SaveData();
        }
    }
}
