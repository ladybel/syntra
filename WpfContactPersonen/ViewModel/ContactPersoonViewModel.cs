using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Syntra.Data.Models;
using WpfContactPersonen.ViewModel;
using System.Linq;
using System.IO;
using System.Windows.Media.Imaging;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows;
using WpfContactPersonen.Tools;
using DevExpress.Office;
using System.DirectoryServices;

namespace WpfContactPersonen.ViewModel
{
   public class ContactPersoonViewModel : INotifyPropertyChanged
    {
        #region Fields
        public event PropertyChangedEventHandler PropertyChanged;
        public ContactpersonenLijst _cpLijst = null;
        ObservableCollection<Contactpersoon> _cpCollectie = null;
        Contactpersoon _currentCP = null;


        #endregion Fields
        #region Properties

        public Contactpersoon CurrentCP
        {
            get => _currentCP;

            set
            {
                _currentCP = value;
                RaisePropertyChanged("Contactpersoon");
            }
        }
        public ContactpersonenLijst CPLijst
        {
            get
            {
                _cpLijst ??= new ContactpersonenLijst();
                return _cpLijst;
            }
            set
            {
                _cpLijst = value;
                RaisePropertyChanged("CPLijst");
            }
        }
        public ObservableCollection<Contactpersoon> CPCollectie
        {
            get
            {
                _cpCollectie ??= new ObservableCollection<Contactpersoon>(CPLijst.Members); return _cpCollectie;
            }
            set => CPLijst.Members = value.Cast<Contactpersoon>().ToList();
        }


        #endregion Properties
        #region Methods

        protected void RaisePropertyChanged([CallerMemberName] string property = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        internal void Import()
        {

            CPLijst.Import();
        
        }

        #endregion Methods

    }
}
