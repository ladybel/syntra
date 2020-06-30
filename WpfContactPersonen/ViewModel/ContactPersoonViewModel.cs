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
        private List<Openingsuren> _contactOU;
        private List<SluitingsDagen> _contactSD;
       


        ObservableCollection<string> _categoriesColl = null;
        ObservableCollection<Openingsuren> _openingsurenColl = null;
        ObservableCollection<SluitingsDagen> _sluitingsdagenColl = null;
        ObservableCollection<Contactpersoon> _sfdColl = null;



        #endregion Fields
        #region Properties
        public List<Openingsuren> ContactOpeningsuren
        {
            get
            {
                _contactOU ??= new List<Openingsuren>();
                return _contactOU;
            }
            set
            {   _contactOU = value;
                RaisePropertyChanged("ContactOpeningsuren");
            }
        }
        public List<SluitingsDagen> ContactSluitingsDagen
        {
            get
            {
                _contactSD ??= new List<SluitingsDagen>();
                return _contactSD;
            }
            set
            {
                _contactSD = value;
                RaisePropertyChanged("ContactSluitingsDagen");
            }
        }
        public BitmapImage SelectedImage { get => selectedImage; set { selectedImage = value; RaisePropertyChanged(); } }
        public BitmapImage CurrentImage { get => currentImage; set { currentImage = value; RaisePropertyChanged(); } }

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
        public Openingsuren CurrOU { get; set; }=null;
        public SluitingsDagen CurrSD { get; set; } = null;
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
        public ObservableCollection<Openingsuren> OpeningsurenColl
        {
            get
            {
                if (CurrentCP != null&& CurrentCP.Categorie=="Winkel of bedrijf") { 
                    //WinkelOfBedrijf currCP = (WinkelOfBedrijf)CurrentCP;
                    _openingsurenColl = new ObservableCollection<Openingsuren>(ContactOpeningsuren);
                    /*foreach(var ou in currCP.Openingsuren) 
                    {
                    _openingsurenColl.Add(new Openingsuren(ou));
                    }    */
                }
                return _openingsurenColl;
            }
            set => _openingsurenColl = value;
        }
        public ObservableCollection<SluitingsDagen> SluitingsDagenColl
        {
            get
            {
                if (CurrentCP != null&& CurrentCP.Categorie == "Winkel of bedrijf")
                {
                   // WinkelOfBedrijf currCP = (WinkelOfBedrijf)CurrentCP;
                    _sluitingsdagenColl = new ObservableCollection<SluitingsDagen>(ContactSluitingsDagen);
                    /*foreach (var sd in currCP.SluitingsDagen)
                    {
                        _sluitingsdagenColl.Add(sd);
                    }*/
                }
               
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
        private BitmapImage currentImage = null;


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
            /*if(OpeningsurenColl!=null) {
            OpeningsurenColl.Clear(); }
            foreach (var Item in ContactOpeningsuren)
            {
                OpeningsurenColl.Add(Item);
            }*/
        }

        public void ResetSDColl()
        {/*
            if (SluitingsDagenColl != null) { 
            SluitingsDagenColl.Clear();}
            foreach (var Item in ContactSluitingsDagen)
            {
                SluitingsDagenColl.Add(Item);
            }*/
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
        //private BitmapImage selectedImage = null;

        public BitmapImage TryLoadImage()
        {
            if(CurrentCP.Categorie=="Fysieke contactpersoon") 
            {
                var cp = (FysiekeContactpersoon)CurrentCP;
                if (cp.Foto?.Length > 0)
                {
                    MemoryStream stream = new MemoryStream(System.Convert.FromBase64String(cp.Foto));
                    BitmapImage image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.EndInit();
                    return image;
                }
            }
            return null;
        }
        public string SaveImageAsB64(BitmapImage src, BitmapEncoder encoder = null) => src != null ? System.Convert.ToBase64String(SaveImage(src, encoder)) : null;
        public byte[] SaveImage(BitmapImage src, BitmapEncoder encoder = null)
        {
            if (src != null)
            {
                encoder ??= new PngBitmapEncoder();
                using MemoryStream ms = new MemoryStream();
                encoder.Frames.Add(BitmapFrame.Create(src));
                encoder.Save(ms);
                return ms.ToArray();
            }
            return null;
        }
        public void UpdateFoto(BitmapImage img)
        {
            if (SelectedImage != null&&CurrentCP!=null)
            {
                string imgstr = SaveImageAsB64(img);
                FysiekeContactpersoon fcp = (FysiekeContactpersoon)CurrentCP;
                fcp.Foto = imgstr;
                CurrentCP = fcp;
                UpdateGui();
            }
        }
        public string SaveFotoAsString()
        {
            if (SelectedImage != null )
            {
                string imgstr = SaveImageAsB64(SelectedImage);
                return imgstr;
            }
            return "";
        }

        #endregion Methods

    }
}
