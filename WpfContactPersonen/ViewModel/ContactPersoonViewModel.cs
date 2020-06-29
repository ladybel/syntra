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
        Contactpersoon _nieuwCP = null;
        
        string cat1 = "Fysieke contactpersoon";
        string cat2="Winkel of bedrijf";
        public string CurrentOU = null;
        public string CurrentSD = null;
        List<string> _categories = new List<string>();
        public List<Contactpersoon> SelectedForDeleting = new List<Contactpersoon>();
       


        public  List<string> ContactOpeningsuren = new List<string>();
         public List<string> ContactSluitingsDagen = new List<string>();
        ObservableCollection<string> _categoriesColl = null;
        ObservableCollection<string> _openingsurenColl = null;
        ObservableCollection<string> _sluitingsdagenColl = null;
        ObservableCollection<Contactpersoon> _sfdColl = null;



        #endregion Fields
        #region Properties

        public BitmapImage SelectedImage { get => selectedImage; set { selectedImage = value; RaisePropertyChanged(); } }
        public List<string> Categories
        {
            get
            {
                if(_categories.Count == 0)
                {
                    _categories.Add(cat1);
                    _categories.Add(cat2);
                }
                return _categories;
            }
            set
            {
                _categories = value;
                RaisePropertyChanged("Categories");
            }
        }
        public string CurrentCategorie { get; set; } ="";
        public string ZoekText { get; set; } = "";
        public Contactpersoon CurrentCP
        {
            get => _currentCP;

            set
            {
                _currentCP = value;
                RaisePropertyChanged("Contactpersoon");
            }
        }
        public Contactpersoon NieuwCP
        {
            get => _nieuwCP;

            set
            {
                _nieuwCP = value;
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
        public ObservableCollection<string> CategoriesColl
        {
            get
            {
                _categoriesColl = new ObservableCollection<string>(Categories);
                return _categoriesColl;
            }
            set => _categoriesColl = value;
        }
        public ObservableCollection<Contactpersoon> SelectedForDelColl
        {
            get
            {
                _sfdColl = new ObservableCollection<Contactpersoon>(SelectedForDeleting);
                return _sfdColl;
            }
            set => _sfdColl = value;
        }
        public ObservableCollection<string> OpeningsurenColl
        {
            get
            {
                _openingsurenColl = new ObservableCollection<string>(ContactOpeningsuren);
                return _openingsurenColl;
            }
            set => _openingsurenColl = value;
        }
        public ObservableCollection<string> SluitingsDagenColl
        {
            get
            {
                _sluitingsdagenColl = new ObservableCollection<string>(ContactSluitingsDagen);
                return _sluitingsdagenColl;
            }
            set => _sluitingsdagenColl = value;
        }
        public ObservableCollection<Contactpersoon> CPCollectie
        {
            get
            {
                _cpCollectie ??= new ObservableCollection<Contactpersoon>(CPLijst.Members); return _cpCollectie;
            }
            set => CPLijst.Members = value.Cast<Contactpersoon>().ToList();
        }
        private BitmapImage selectedImage = null;

        #endregion Properties
        #region Methods
        public int GetNextIndex()
        {
            int id= CPLijst.Members.Max(t=>t.ID)+1;
            return id;
        }
        internal void ToonAlleContactPersonen()
        {
            ResetCPColl();
        }


        internal void ToonPerCategorie()
        {
            if (CurrentCategorie != null &CurrentCategorie != "")
            {

                List<Contactpersoon> result = CPLijst.Members.FindAll(t => t.Categorie == CurrentCategorie);

                if (result.Count > 0)
                {
                    CPCollectie.Clear();
                    foreach (var cp in result)
                    {
                        CPCollectie.Add(cp);
                    }
                }
            }

            else ResetCPColl();
        }
        internal void ToonZoekRes()
        {
            if (ZoekText != null)
            {

                List<Contactpersoon> result = CPLijst.Members.FindAll(t => (t.Naam.ToLower().Contains(ZoekText.ToLower()) || t.Categorie.Contains(ZoekText) || t.Telefoon.Contains(ZoekText))).ToList();

                if (result.Count > 0)
                {
                    CPCollectie.Clear();
                    foreach (var c in result)
                    {
                       CPCollectie.Add(c);
                    }
                }
            }

        }

        public void ResetOUColl()
        {
            OpeningsurenColl.Clear();
            foreach (var Item in ContactOpeningsuren)
            {
                OpeningsurenColl.Add(Item);
            }
        }

        public void ResetSDColl()
        {SluitingsDagenColl.Clear();
            foreach (var Item in ContactSluitingsDagen)
            {
                SluitingsDagenColl.Add(Item);
            }
        }

        public void ResetCPColl()
        {
            CPCollectie.Clear();
            foreach(var Item in CPLijst.Members)
            {
               CPCollectie.Add(Item);
            }
        }
        protected void RaisePropertyChanged([CallerMemberName] string property = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        internal void Import()
        {

            CPLijst.Import();
        
        }
        public void UpdateGui() => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(""));

        
        #endregion Methods

    }
}
