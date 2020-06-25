/*using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using Syntra.Data.Models;
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
   public class CategorieViewModel: INotifyPropertyChanged
	{
		#region Info
		/// Class : CategorieViewModel
		#endregion Info
		#region Definitions 
		#endregion Definitions
		#region Fields
		public event PropertyChangedEventHandler PropertyChanged;
		HoofdCategorieLijst _hcLijst = null;
		ObservableCollection<HoofdCategorie> _hcCollectie = null;

		HoofdCategorie _currentHC = null;
		SubCategorie _currentSC = null;

		#endregion Fields
		#region Constructors


		#endregion Constructors
		#region Properties
		public HoofdCategorieLijst HoofdCatLijst { get { _hcLijst ??= new HoofdCategorieLijst(); return _hcLijst; } set => _hcLijst = value; }
		public ObservableCollection<HoofdCategorie> HCCollectie
		{
			get
			{
				_hcCollectie ??= new ObservableCollection<HoofdCategorie>(HoofdCatLijst.Members); return _hcCollectie;
			}
			set => HoofdCatLijst.Members = value.ToList();
		}
		public HoofdCategorie CurrentHC { get => _currentHC; set { _currentHC = value; RaisePropertyChanged("CurrentHC"); } }
		public SubCategorie CurrentSC { get => _currentSC; set { _currentSC = value; RaisePropertyChanged("CurrentSC"); } }
		#endregion Properties
		#region Methods
		
		public bool addHoofdCategorie(string naam)
		{
			HoofdCatLijst.addHoofdCategorie(naam);
			ResetHCList();
			return true;

		}
		public bool addHoofdCategorie(string hc_naam, string sc_naam)
		{
			HoofdCatLijst.addHoofdCategorie(hc_naam,sc_naam);
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
			HoofdCatLijst.renameHoofdCategorie(hc,naam);
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
			var hc=HCCollectie.Where(t => t.Naam == hc_naam).FirstOrDefault();
			if (hc != null)
			{
				
				hc.addSubCategorie(sc_naam);
				ResetHCList();
				return true;
			}
			else return false;

		}
		public bool addSubCategorie(HoofdCategorie hc, string sc_naam)
		{
				hc.addSubCategorie(sc_naam);
				ResetHCList();
				return true;
		}

		public bool addSubCategorie(HoofdCategorie hc, SubCategorie sc)
		{
			hc.addSubCategorie(sc);
			ResetHCList();
			return true;
		}
		public bool addSubCategorie(string hc_naam, SubCategorie sc)
		{
			var hc = HCCollectie.Where(t => t.Naam == hc_naam).FirstOrDefault();
			if (hc != null)
			{

				hc.addSubCategorie(sc);
				ResetHCList();
				return true;
			}
			else return false;
		}
		public bool deleteSubCategorie(HoofdCategorie hc, SubCategorie sc)
		{
			hc.deleteSubCategorie(sc);
			ResetHCList();
			return true;
		}
		public bool deleteSubCategorie(string hc_naam, string sc_naam)
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
			hc.renameSubCategorie(sc,nieuw);
			ResetHCList();
			return true;
		}
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

		public void ResetHCList()
		{
			CurrentHC = null;
			HCCollectie.Clear();
			foreach (var hc in HoofdCatLijst.Members.Where(t=>t.Deleted==false)) { HCCollectie.Add(hc); }
			//foreach (var hc in HoofdCatLijst.Members) { HCCollectie.Add(hc); }

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
		internal void VulData()
		{
			HoofdCatLijst.VulData();
			ResetHCList();

		}
		protected void RaisePropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
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
		}
		#endregion Methods      

	}
}*/