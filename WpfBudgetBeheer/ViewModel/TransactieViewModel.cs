/*using System;
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
    public class TransactieViewModel
    {
		#region Info
		/// Class : TransactieViewModel
		#endregion Info
		#region Definitions 
		#endregion Definitions
		#region Fields
		public event PropertyChangedEventHandler PropertyChanged;
		TransactiePosten _tpLijst = null;
		ObservableCollection<TransactiePost> _tpCollectie = null;

		TransactiePost _currentTP = null;
		CategorieViewModel cvm = new CategorieViewModel();

		#endregion Fields
		#region Constructors
		#endregion Constructors
		#region Properties
		public TransactiePosten TrPostLijst { get { _tpLijst ??= new TransactiePosten(); return _tpLijst; } set => _tpLijst = value; }
		
		public ObservableCollection<TransactiePost> TPCollectie
		{
			get
			{
				_tpCollectie ??= new ObservableCollection<TransactiePost>(TrPostLijst.Members); return _tpCollectie;
			}
			set => TrPostLijst.Members = value.ToList();
		}
		public TransactiePost CurrentTP { get => _currentTP; set { _currentTP = value; RaisePropertyChanged("CurrentHC"); } }
		

		#endregion Properties
		#region Methods


		public bool addTransactiePost(DateTime datum, SubCategorie sc, string comment, double debet, double credit)
		{
			HoofdCategorie hc = cvm.HoofdCatLijst.getHoofdCategorie(sc);
			//TrPostLijst.addTransactiePost(datum, hc,sc,comment,debet, credit);
			TrPostLijst.addTransactiePost(datum,  sc, comment, debet, credit);
			ResetTPList();
			return true;
		}
		public bool addTransactiePost(DateTime datum, string sc_naam, string comment, double debet, double credit)
		{
			SubCategorie sc= cvm.HoofdCatLijst.getSubCategorie(sc_naam);
			HoofdCategorie hc= cvm.HoofdCatLijst.getHoofdCategorie(sc_naam);
			//TrPostLijst.addTransactiePost(datum, hc, sc, comment, debet, credit);
			TrPostLijst.addTransactiePost(datum,  sc, comment, debet, credit); 
			ResetTPList();
			return true;
		}

		public HoofdCategorie getHoofdCategorie(SubCategorie sc)
		{
			return cvm.HoofdCatLijst.getHoofdCategorie(sc);
		}
		public bool deleteTransactiePost(TransactiePost tp)
		{
			TrPostLijst.deleteTransactiePost(tp);
			ResetTPList();
			return true;
		}
		

		public void ResetTPList()
		{
			CurrentTP = null;
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
		internal void VulData()
		{
			TrPostLijst.VulData();
			ResetTPList();


		}

		protected void RaisePropertyChanged(string property) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		public bool Import(string json)
		{
			var ok = TrPostLijst.Import(json);
			ResetTPList();
			return ok;
		}
		public string Export()
		{
			UpdateTPList();
			return TrPostLijst?.Export();
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
}
*/