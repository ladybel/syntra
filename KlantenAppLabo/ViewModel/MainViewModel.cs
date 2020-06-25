using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Syntra.Data.Models;
using KlantenAppWPF.ViewModel;
using System.Linq;
using System.IO;
using System.Windows.Media.Imaging;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows;
using KlantenAppWPF.Tools;
using DevExpress.Office;
using System.DirectoryServices;


namespace KlantenAppWPF.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Fields 

        public event PropertyChangedEventHandler PropertyChanged;
        public const int AantalTafels = 5;
        KlantenLijst _klantenLijst = null;
        BookingLijst _bookingLijst = null;
       
        public ObservableCollection<Klant> _klantenColl = null;
        public ObservableCollection<Booking> _bookingColl = null;      
        public ObservableCollection<int> _tafelsColl = null;
        public Klant _currentKlant = null;
        public string _currentNaam = "";
        public string _currentAdres = "";
        public DateTime _currentGebDatum;
        public string ZoekText { get; set; } = "";
        public int TafelNr { get; set; } =0;
        public Booking _currentBooking = null;
       
        public List<int> _tafels;
        public List<int> _beschikbareTafels;
        public List<int> _geboekteTafels;
     
        public ObservableCollection<int> _beschikbareTafelsColl = null;
        public ObservableCollection<int> _geboekteTafelsColl = null;
        public int _aantalBeschikbareTafels;              
        int _currentKlantID;
        DateTime _currentDatum;
        int _currentTafel;


        public string LastError { get; protected set; } = "";

        #endregion Fields
        #region Properties

        public int AantalBeschikbareTafels
        {
            get
            {
                _aantalBeschikbareTafels = GetNrBeschikbareTafels(DateTime.Today);
                return _aantalBeschikbareTafels;
            }
            set => _aantalBeschikbareTafels = value;
        }
        public KlantenLijst KlantenLijst 
        
        { 
            get 
            {   _klantenLijst ??= new KlantenLijst(); 
                return _klantenLijst; 
            } set
            {
                _klantenLijst = value;
                RaisePropertyChanged("KlantenLijst");
            }
        }
        public BookingLijst BookingLijst 
        { get 
            { 
                _bookingLijst ??= new BookingLijst(); 
                return _bookingLijst; 
            }
            set
            {
                _bookingLijst = value;
                RaisePropertyChanged("BookingLijst");
            }
        }
       
        public List<int> Tafels
        {
            get
            {
                _tafels = Enumerable.Range(1, AantalTafels).ToList();
                return _tafels;
            }
            set
            {
                _tafels = value;
                RaisePropertyChanged("Tafels");
            }
        }
        public List<int> BeschikbareTafels
        {
            get
            {

                _beschikbareTafels ??= new List<int>();
                return _beschikbareTafels;
            }
            set
            {
                _beschikbareTafels = value;
                RaisePropertyChanged("BeschikbareTafels");
            }
        }
        public List<int> GeboekteTafels
        {
            get
            {

                return _geboekteTafels;
            }
            set
            {
                _beschikbareTafels = value;
                RaisePropertyChanged("GeboekteTafels");
            }
        }



        public Booking CurrentBooking
        {
            get
            {
                /*if (_currentBooking != null)
                {

                }*/
                return _currentBooking;
            }
            set
            {
                _currentBooking = value;
                RaisePropertyChanged("CurrentBooking");

            }
        }
        public int CurrentKlantID { get { return _currentKlantID; } set { _currentKlantID = value; RaisePropertyChanged("CurrentKlantID"); } }
        public int CurrentTafel { get { return _currentTafel; } set { _currentTafel = value; RaisePropertyChanged("CurrentTafel"); } }
        public DateTime CurrentDatum { get { return _currentDatum; } set { _currentDatum = value; RaisePropertyChanged("CurrentDatum"); } }

        public ObservableCollection<Booking> BookingColl
        {
            get
            {
                _bookingColl ??= new ObservableCollection<Booking>(BookingLijst.Members);
                return _bookingColl;
            }
            set => BookingLijst.Members = value.ToList();
        }
        public ObservableCollection<int> TafelsColl
        {
            get
            {
                _tafelsColl = new ObservableCollection<int>(Tafels);
                return _tafelsColl;
            }
            set => _tafelsColl = value;
        }
        public ObservableCollection<int> BeschikbareTafelsColl
        {
            get
            {
                _beschikbareTafelsColl ??= new ObservableCollection<int>(BeschikbareTafels);
                return _beschikbareTafelsColl;
            }
            set => BeschikbareTafels = value.ToList();
        }
        public ObservableCollection<int> GeboekteTafelsColl
        {
            get
            {
                _geboekteTafelsColl = new ObservableCollection<int>(GeboekteTafels);
                return _geboekteTafelsColl;
            }
            set
            {
                _geboekteTafelsColl = value;
                RaisePropertyChanged("GeboekteTafels");
            }
        }


        

        public Klant CurrentKlant {
            get
            {
                if (_currentKlant != null)
                {
                    
                }
                return _currentKlant;
            }
            set{
                _currentKlant = value; 
                RaisePropertyChanged("CurrentKlant"); 
                
            } 
        }
        public string CurrentNaam { get { return _currentNaam; } set { _currentNaam = value; RaisePropertyChanged("CurrentNaam"); } }
        public string CurrentAdres { get { return _currentAdres; } set { _currentAdres = value; RaisePropertyChanged("CurrentAdres"); } }
        public DateTime CurrentGebDatum { get { return _currentGebDatum; } set { _currentGebDatum = value; RaisePropertyChanged("CurrentGebDatum"); } }
       

    

    public ObservableCollection<Klant> KlantenColl
        { get
            {
                _klantenColl ??= new ObservableCollection<Klant>(KlantenLijst.Members);
               
                return _klantenColl;
            }
            set
            {
                KlantenLijst.Members = value.ToList();
                RaisePropertyChanged("KlantenColl");
            }
        }



        #endregion Properties
        #region Methods
        internal void VulData()
        {
            //KlantenLijst.Import();
            //BookingLijst.Import();

        }
            
        protected void RaisePropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));

        public bool ExportToFile(string path, string context)
        {
            try
            {
                File.WriteAllText(path, context);
                return File.Exists(path);
            }
            catch { }
            return false;
        }

        public bool addKlant(Klant klant)
        {
            KlantenLijst.AddKlant(klant);
            UpdateKlantenColl();
            return true;

        }
        public bool deleteKlant(Klant klant)
        {
            KlantenLijst.DeleteKlant(klant);
            UpdateKlantenColl();
            return true;
        }
       
        public int getVolgendeKlantID() 
        {
            return (KlantenLijst.Members.Max(t => t.ID)+1);
        }
        public bool UpdateKlantenColl()
        {

            CurrentKlant = null;
            KlantenColl.Clear();
            foreach (var tp in KlantenLijst.Members) 
            { 
                KlantenColl.Add(tp); 
            }
            return true;
        }
        internal void ToonKlantZoekRes()
        {
            if (ZoekText != null)
            {
                string s = "*******************************************";
                if(DateTime.TryParse(ZoekText, out var d))
                {
                   s=d.ToShortDateString();
                }
                
                List<Klant> result = KlantenLijst.Members.FindAll(t => (t.Naam.ToLower().Contains(ZoekText.ToLower()) || t.GebDatum.ToShortDateString().Contains(ZoekText) || t.GebDatum.ToShortDateString().Contains(s) || t.Adres.ToLower().Contains(ZoekText.ToLower()))).ToList();
                
                if (result.Count > 0)
                {
                    KlantenColl.Clear();
                    foreach (var c in result)
                    {
                        KlantenColl.Add(c);
                    }
                }
            }




        }
        public bool addBooking(Booking booking)
        {

            BookingLijst.AddBooking(booking);

            return true;

        }
        public bool deleteBooking(Booking booking)
        {
            BookingLijst.DeleteBooking(booking);
            UpdateBookingColl();
            return true;
        }

        public int getVolgendeBookingID()
        {
            return (BookingLijst.Members.Max(t => t.ID) + 1);
        }
        public int GetNrBeschikbareTafels(DateTime datum)
        {
            return AantalTafels - BookingLijst.Members.Where(t => t.Datum == datum).Count();
        }

        public List<int> getBeschikbareTafels(DateTime datum)
        {
            List<Booking> bl = BookingLijst.Members.Where(b => b.Datum.ToShortDateString() == datum.ToShortDateString()).ToList();
            if (bl.Count > 0)
            {
                var bschtf = Tafels;
                foreach (var item in bl)
                {
                    bschtf.Remove(Tafels.Where(t => t == item.Tafel).FirstOrDefault());
                }
                return bschtf;
            }
            else return Tafels;
        }

        public bool UpdateBookingColl()
        {

            CurrentBooking = null;
            BookingColl.Clear();
            foreach (var b in BookingLijst.Members)
            {
                BookingColl.Add(b);
            }
            return true;
        }
        internal void ToonBookZoekRes()
        {
            if (ZoekText != null)
            {
                string s = "*******************************************";
                if (DateTime.TryParse(ZoekText, out var d))
                {
                    s = d.ToShortDateString();
                }

                List<Booking> result = BookingLijst.Members.FindAll(t => (t.Klant_ID == Int32.Parse(ZoekText) || t.Datum.ToShortDateString().Contains(ZoekText) || t.Datum.ToShortDateString().Contains(s))).ToList();

                if (result.Count > 0)
                {
                    BookingColl.Clear();
                    foreach (var c in result)
                    {
                        BookingColl.Add(c);
                    }
                }
            }




        }

        internal void ToonAantalBeschikbareTafels()
        {
            AantalBeschikbareTafels = getBeschikbareTafels(DateTime.Today).Count();
        }
        internal void ToonBookingenPerTafel()
        {
            if (TafelNr > 0)
            {



                List<Booking> result = BookingLijst.Members.FindAll(t => t.Tafel == TafelNr);

                if (result.Count > 0)
                {
                    BookingColl.Clear();
                    foreach (var c in result)
                    {
                        BookingColl.Add(c);
                    }
                }
            }




        }


        internal void ToonAlleBookingen()
        {
            BookingColl.Clear();
            foreach (var c in BookingLijst.Members)
            {
                BookingColl.Add(c);
            }
        }


        internal void ToonKlantenPerTafel()
        {
            if (TafelNr>0)
            {

                List<Klant> result = KlantenLijst.Members.FindAll(t => (BookingLijst.Members.Where(s=>s.Klant_ID==t.ID&&s.Tafel==TafelNr)!=null));

                if (result.Count > 0)
                {
                    KlantenColl.Clear();
                    foreach (var c in result)
                    {
                        KlantenColl.Add(c);
                    }
                }
            }

            

        }

        internal void ToonAlleKlanten()
        {
            KlantenColl.Clear();
            foreach (var c in KlantenLijst.Members)
            {
                KlantenColl.Add(c);
            }
        }
        #endregion Methods

    }
}
 

