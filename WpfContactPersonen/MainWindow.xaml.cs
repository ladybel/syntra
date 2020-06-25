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
using WpfContactPersonen.ViewModel;

namespace WpfContactPersonen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ContactPersoonViewModel _viewmod = null;
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

        }
    }
}
