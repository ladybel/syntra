using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Syntra.Data.Models;
using WpfBudgetBeheer.ViewModel;
using System.Linq;
using System.IO;
using System.Windows.Media.Imaging;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using System.Windows;
using WpfBudgetBeheer.Tools;
using DevExpress.Office;
using System.DirectoryServices;

namespace WpfBudgetBeheer.ViewModel
{
	public class trViewM : INotifyPropertyChanged
	{
		#region Info
		/// Class 
		#endregion Info
		#region Definitions 
		#endregion Definitions
		#region Fields
		public event PropertyChangedEventHandler PropertyChanged;
		TransactiePosten _tpLijst = null;
		ObservableCollection<TransactiePost> _tpCollectie = null;

		

		//CategorieViewModel cvm = new CategorieViewModel();
		HoofdCategorieLijst _hcLijst = null;
		List<string> _hcLijstNaam = null;
		SubCategorieLijst _scLijst = null;
		SubCategorieLijst _currSCLijst = null;
		List<string> _scLijstNaam = null;
		List<string> _currSCLijstNaam = null;
		ObservableCollection<HoofdCategorie> _hcCollectie = null;
		ObservableCollection<SubCategorie> _scCollectie = null;
		ObservableCollection<SubCategorie> _currSCCollectie = null;

		ObservableCollection<string> _hcCollectieNaam = null;
		ObservableCollection<string> _scCollectieNaam = null;

		TransactiePost _currentTP = null;
		HoofdCategorie _currentHC = null;
		SubCategorie _currentSC = null;
		int _nieuwTrID;
		HoofdCategorie _nieuwTrHoofdCat = null;
		SubCategorie _nieuwTrSubCat = null;
		DateTime _nieuwTrDatum;
		string _nieuwTrOmschrijving = null;
		double _nieuwTrDebet;
		double _nieuwTrCredit;

		#endregion Fields
		#region Constructors
		#endregion Constructors
		#region Properties

		public int NieuwTrID
		{ get
			{
				_nieuwTrID = 1000 + TrPostLijst.Count + 1;
				return _nieuwTrID;
			}
			set
			{
				_nieuwTrID = value;
				RaisePropertyChanged("NieuwTrID");
			}
		}
		public HoofdCategorie NieuwTrHoofdCat
		{
			get => _nieuwTrHoofdCat;

			set
			{
				_nieuwTrHoofdCat = value;
				RaisePropertyChanged("NieuwTrHoofdCat");
			}
		}
		public SubCategorie NieuwTrSubCat
		{
			get => _nieuwTrSubCat;

			set
			{
				_nieuwTrSubCat = value;
				RaisePropertyChanged("NieuwTrSubCat");
			}
		}
		public string NieuwTrOmschrijving
		{
			get => _nieuwTrOmschrijving;

			set
			{
				_nieuwTrOmschrijving = value;
				RaisePropertyChanged("NieuwTrOmschrijving");
			}
		}
		public DateTime NieuwTrDatum
		{
			get => _nieuwTrDatum;

			set
			{
				_nieuwTrDatum = value;
				RaisePropertyChanged("NieuwTrDatum");
			}
		}
		public double NieuwTrDebet
		{
			get => _nieuwTrDebet;

			set
			{
				_nieuwTrDebet = value;
				RaisePropertyChanged("NieuwTrDebet");
			}
		}
		public double NieuwTrCredit
		{
			get => _nieuwTrCredit;

			set
			{
				_nieuwTrCredit = value;
				RaisePropertyChanged("NieuwTrCredit");
			}
		}

		public TransactiePosten TrPostLijst 
		{ get 
			{
				_tpLijst ??= new TransactiePosten(); 
				return _tpLijst; 
			}
			set
			{
				_tpLijst = value;
				RaisePropertyChanged("TrPostLijst");
			}
		}

		public ObservableCollection<TransactiePost> TPCollectie
		{
			get
			{
				_tpCollectie ??= new ObservableCollection<TransactiePost>(TrPostLijst.Members); return _tpCollectie;
			}
			set => TrPostLijst.Members = value.Cast<TransactiePost>().ToList();
		}
		public TransactiePost CurrentTP 
		{
			get 
			{
				return _currentTP;
			}
			set 
			{ 
				_currentTP = value; 
				RaisePropertyChanged("CurrentTP");
				
			} 
		}

		public HoofdCategorieLijst HoofdCatLijst { get { _hcLijst ??= new HoofdCategorieLijst(); return _hcLijst; } set { _hcLijst = value; RaisePropertyChanged("HoofdCatLijst"); } }
		public SubCategorieLijst SubCatLijst { get { _scLijst ??= new SubCategorieLijst(); return _scLijst; } set { _scLijst = value; RaisePropertyChanged("SubCatLijst"); } }
		public SubCategorieLijst CurrSubCatLijst 
		{ 
			get 
			{
				if (CurrentTP != null)
				{
					_currSCLijst = new SubCategorieLijst();
					//_currSCLijst.Members.Add(CurrentTP.SubCat);
					_currSCLijst.Members.AddRange(SubCatLijst.Members.Where(t => ((t.ID) / 100) * 100 == CurrentTP.HoofdCat.ID));
				}
				if (CurrentHC != null)
				{
					_currSCLijst = new SubCategorieLijst();
					_currSCLijst.Members.AddRange(SubCatLijst.Members.Where(t => ((t.ID) / 100) * 100 == CurrentHC.ID));


				}
				if (NieuwTrHoofdCat != null)
				{
					_currSCLijst = new SubCategorieLijst();
					_currSCLijst.Members.AddRange(SubCatLijst.Members.Where(t => ((t.ID) / 100) * 100 == NieuwTrHoofdCat.ID));

				}

				return _currSCLijst; 
			}
			set
			{
				_currSCLijst = value;
				RaisePropertyChanged("CurrSubCatLijst");
			}
		}
/*
		public List<string> CurrSubCatLijstNaam 
		{ 
			get
			{   
				_currSCLijstNaam ??= new List<string>();

				if (_currSCLijstNaam.Count == 0) { 
					foreach (var item in CurrSubCatLijst.Members)
					{
						_currSCLijstNaam.Add(item.Naam);
					}
				}

				
				return _currSCLijstNaam;
			}
			set
			{
				_currSCLijstNaam = value;
				RaisePropertyChanged("CurrSCLijstNaam");
			}
		}
		public List<string> HCLijstNaam
		{
			get
			{

					_hcLijstNaam ??= new List<string>();
				if (_hcLijstNaam.Count == 0) { 
					foreach (var item in HoofdCatLijst.Members)
					{
						_hcLijstNaam.Add(item.Naam);
					}
				}

				return _hcLijstNaam;
			}
			set
			{
				_hcLijstNaam = value; RaisePropertyChanged("HCLijstNaam");
			}
		}
		public List<string> SCLijstNaam
		{
			get
			{

				_scLijstNaam ??= new List<string>();
				if (_scLijstNaam.Count == 0)
				{
					foreach (var item in SubCatLijst.Members)
					{
						_scLijstNaam.Add(item.Naam);
					}
				}

				return _scLijstNaam;
			}
			set
			{
				_scLijstNaam = value;
				RaisePropertyChanged("SCLijstNaam");
			}
		}*/
		public ObservableCollection<SubCategorie> SCCollectie
		{
			get
			{

				_scCollectie = new ObservableCollection<SubCategorie>(SubCatLijst.Members);


				return _scCollectie;
			}

				set =>SubCatLijst.Members = value.Cast<SubCategorie>().ToList(); 

		}
		public ObservableCollection<SubCategorie> CurrSCCollectie
		{
			get
			{
				if (CurrentTP != null|| CurrentSC != null||NieuwTrHoofdCat!=null)
				{
					_currSCCollectie = new ObservableCollection<SubCategorie>(CurrSubCatLijst.Members);
				}

				return _currSCCollectie;
			}

			set => CurrSubCatLijst.Members = value.Cast<SubCategorie>().ToList();
		}

		public ObservableCollection<HoofdCategorie> HCCollectie
		{
			get
			{
				
				_hcCollectie ??= new ObservableCollection<HoofdCategorie>(HoofdCatLijst.Members); 
				return _hcCollectie;
			}
			set => HoofdCatLijst.Members = value.Cast<HoofdCategorie>().ToList();
		}
		/*
		public ObservableCollection<string> HCCollectieNaam
		{
			get
			{

				_hcCollectieNaam ??= new ObservableCollection<string>(HCLijstNaam);
				
				return _hcCollectieNaam;
			}
		
				set => _hcCollectieNaam = value;
			

		}
		
		public ObservableCollection<string> SCCollectieNaam 
		{ 
			get
			{
				if (CurrentTP != null) 
				{ 
				_scCollectieNaam= new ObservableCollection<string>(CurrSubCatLijstNaam); 
				}
				return _scCollectieNaam;
			}
			set
			{
				_scCollectieNaam = value;
				RaisePropertyChanged("CSCollectieNaam");
			}
		} */
		public HoofdCategorie CurrentHC 
			{
			get 
			{
				/*if(CurrentSC!=null) { 
				_currentHC = HoofdCatLijst.Members.Where(t => t.ID == (_currentSC.ID % 100) * 100).FirstOrDefault();
				}*/
				return _currentHC; 
			}
			set 
			{ 
				_currentHC = value; 
				RaisePropertyChanged("CurrentHC"); 
			} 
		}
		public SubCategorie CurrentSC 
		{
			get
			{
				/*if (CurrentTP != null) 
				{ 
					_currentSC = _currentTP.SubCat;
				}*/
				return _currentSC;
			}
			set 
			{ 
				_currentSC = value; 
				RaisePropertyChanged("CurrentSC"); 
			} 
		}
		#endregion Properties
		#region Methods
		protected void RaisePropertyChanged([CallerMemberName] string property = "") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));


		public bool addTransactiePost(DateTime datum, HoofdCategorie hc, SubCategorie sc, string comment, double debet, double credit)
		{
			//HoofdCategorie hc = HoofdCatLijst.getHoofdCategorie(sc);
			TrPostLijst.addTransactiePost(datum, hc, sc, comment, debet, credit);
			//TrPostLijst.addTransactiePost(datum, sc, comment, debet, credit);
			ResetTPList();
			return true;
		}
		public bool addTransactiePost(DateTime datum, string sc_naam, string comment, double debet, double credit)
		{

			SubCategorie sc = SubCatLijst.GetSubCategorie(sc_naam);

			HoofdCategorie hc = HoofdCatLijst.Members.Where(t=>t.ID==(sc.ID%100)*100).FirstOrDefault();
			TrPostLijst.addTransactiePost(datum, hc, sc, comment, debet, credit);
			//TrPostLijst.addTransactiePost(datum, sc, comment, debet, credit);
			ResetTPList();
			return true;
		}
		
		/*public HoofdCategorie getHoofdCategorie(SubCategorie sc)
		{
			return cvm.HoofdCatLijst.getHoofdCategorie(sc);
		}*/
		public bool deleteTransactiePost(TransactiePost tp)
		{
			TrPostLijst.deleteTransactiePost(tp);
			ResetTPList();
			return true;
		}
		

		public void ResetTPList()
		{
			//CurrentTP = null;
			
			TPCollectie.Clear();
			foreach (var tp in TrPostLijst.Members) { TPCollectie.Add(tp); }


		}
		
		public bool UpdateTPList()
		{
			if (TPCollectie?.Count > 0)
			{
				TrPostLijst.Members = new List<TransactiePost>(TPCollectie);
				return true;
			}
			return false;
		}
		internal void Import()
		{

			HoofdCatLijst.Import();
			SubCatLijst.Import();
			TrPostLijst.Import();



		}
		/*
		protected void RaisePropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		/*public bool Import(string json)
		{
			var ok = TrPostLijst.Import(json);
			ResetTPList();
			return ok;
		}*/
		/*public string Export()
		{
			
			TrPostLijst?.Export();
			
			
		}*/
		/*public bool ImportFromFile(string path)
		{
			if (File.Exists(path))
			{
				return Import(File.ReadAllText(path)) == true;
			}
			return false;
		}*/
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
		public bool addHoofdCategorie(string naam)
		{
			HoofdCatLijst.addHoofdCategorie(naam);
			ResetHCList();
			return true;

		}
		public bool addHoofdCategorie(string hc_naam, string sc_naam)
		{
			HoofdCatLijst.addHoofdCategorie(hc_naam, sc_naam);
			ResetHCList();
			return true;


		}
		public bool addHoofdCategorie(HoofdCategorie hc)
		{
			HoofdCatLijst.addHoofdCategorie(hc);
			ResetHCList();
			return true;
		}
		public bool deleteHoofdCategorie(HoofdCategorie hc)
		{
			HoofdCatLijst.deleteHoofdCategorie(hc);
			ResetHCList();
			return true;
		}
		public bool deleteHoofdCategorie(string naam)
		{
			var hc = HCCollectie.Where(t => t.Naam == naam).FirstOrDefault();
			if (hc != null)
			{

				HoofdCatLijst.deleteHoofdCategorie(hc);
				ResetHCList();
				return true;
			}
			else return false;
		}
		public bool renameHoofdCategorie(HoofdCategorie hc, string naam)
		{
			HoofdCatLijst.renameHoofdCategorie(hc, naam);
			ResetHCList();
			return true;
		}
		public bool renameHoofdCategorie(string oud, string nieuw)
		{
			var hc = HCCollectie.Where(t => t.Naam == oud).FirstOrDefault();
			if (hc != null)
			{

				HoofdCatLijst.renameHoofdCategorie(hc, nieuw);
				ResetHCList();
				return true;
			}
			else return false;
		}
		public bool addSubCategorie(string hc_naam, string sc_naam)
		{
			var hc = HCCollectie.Where(t => t.Naam == hc_naam).FirstOrDefault();
			if (hc != null)
			{

				SubCatLijst.AddSubCategorie(sc_naam, hc.ID);
				ResetSCList();
				return true;
			}
			else return false;

		}
		public bool addSubCategorie(HoofdCategorie hc, string sc_naam)
		{
			SubCatLijst.AddSubCategorie(sc_naam,hc.ID);
			ResetHCList();
			return true;
		}

		public bool addSubCategorie(HoofdCategorie hc, SubCategorie sc)
		{
			SubCatLijst.AddSubCategorie(sc.Naam,hc.ID);
			ResetHCList();
			return true;
		}
		public bool addSubCategorie(string hc_naam, SubCategorie sc)
		{
			var hc = HCCollectie.Where(t => t.Naam == hc_naam).FirstOrDefault();
			if (hc != null)
			{

				SubCatLijst.AddSubCategorie(sc.Naam, hc.ID);
				//ResetHCList();
				return true;
			}
			else return false;
		}
		public bool deleteSubCategorie(HoofdCategorie hc, SubCategorie sc)
		{
			SubCatLijst.DeleteSubCategorie(sc);
			ResetHCList();
			return true;
		}
		/*public bool deleteSubCategorie(string hc_naam, string sc_naam)
		{
			var hc = HCCollectie.Where(t => t.Naam == hc_naam).FirstOrDefault();
			if (hc != null)
			{

				hc.deleteSubCategorie(sc_naam);
				ResetHCList();
				return true;
			}
			else return false;

		}
		public bool renameSubCategorie(HoofdCategorie hc, SubCategorie sc, string nieuw)
		{
			hc.renameSubCategorie(sc, nieuw);
			ResetHCList();
			return true;
		}*/
		/*public HoofdCategorie getHoofdCategorie(string sc_naam)
		{
			return HoofdCatLijst.getHoofdCategorie(sc_naam);
		}
		public HoofdCategorie getHoofdCategorie(SubCategorie sc)
		{
			return HoofdCatLijst.getHoofdCategorie(sc);
		}
		public SubCategorie getSubCategorie(string sc_naam)
		{

			return HoofdCatLijst.getSubCategorie(sc_naam);
		}
		*/
		public void ResetHCList()
		{
			CurrentHC = null;
			HCCollectie.Clear();
			foreach (var hc in HoofdCatLijst.Members.Where(t => t.Deleted == false)) { HCCollectie.Add(hc); }
			//foreach (var hc in HoofdCatLijst.Members) { HCCollectie.Add(hc); }

		}
		public void ResetSCList()
		{
			CurrentHC = null;
			SCCollectie.Clear();
			foreach (var hc in SubCatLijst.Members.Where(t => t.Deleted == false)) { SCCollectie.Add(hc); }
		}
		public bool UpdateHCList()
		{
			if (HCCollectie?.Count > 0)
			{
				HoofdCatLijst.Members = new List<HoofdCategorie>(HCCollectie);
				return true;
			}
			return false;
		}

		/*internal void VulData()
		{
			HoofdCatLijst.VulData();
			ResetHCList();

		}
		public bool Import(string json)
		{
			var ok = HoofdCatLijst.Import(json);
			ResetHCList();
			return ok;
		}
		public string Export()
		{
			UpdateHCList();
			return HoofdCatLijst?.Export();
		}
		public bool ImportFromFile(string path)
		{
			if (File.Exists(path))
			{
				return Import(File.ReadAllText(path)) == true;
			}
			return false;
		}
		public bool ExportToFile(string path)
		{

			try
			{
				File.WriteAllText(path, Export());
				return File.Exists(path);
			}
			catch { }
			return false;
		}*/
		#endregion Methods      



	}
}
