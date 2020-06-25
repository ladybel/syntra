﻿using System;
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
using WpfDataBindingDemo.Tools;
using DevExpress.Office;
using System.DirectoryServices;

namespace WpfDataBindingDemo.ViewModel {

	public class CountryViewModel :INotifyPropertyChanged {
		#region Info
		/// Class : CountryViewModel
		/// ViewModel is de tussenschakel tussen de data en de grafische user interface (GUI).
		/// Door de PropertyChangedEventhandler kan de ViewModel de GUI verwittigen als er een verandering is 
		/// Door gebruik te maken van ObservableCollection list kunnen we de list objecten in de GUI up to date houen
		#endregion Info
		#region Definitions 
		#endregion Definitions
		#region Fields
		public event PropertyChangedEventHandler PropertyChanged;
		CountryRepository _repo = null;
		ObservableCollection<WpfCountry> _countries = null;
		Country _currentCountry = null;
		#endregion Fields
		#region Constructors
		#endregion Constructors
		#region Properties
		public string SearchText { get; set; } = "";
		public bool UseCase { get; set; } = true;
		public bool UseReverse { get; set; } = false;
		public int MaxResults { get; set; } = 0;
		public CountryRepository Repository { get { _repo ??= new CountryRepository(); return _repo; } set => _repo = value; }
		public ObservableCollection<WpfCountry> Countries {
			get {
				_countries ??= new ObservableCollection<WpfCountry>(Repository.Members.Select(s => new WpfCountry(s)).ToList()); return _countries;
			}
			set => Repository.Members = value.Cast<Country>().ToList();
		}
		public Country CurrentCountry { get => _currentCountry; set { _currentCountry = value; RaisePropertyChanged("CurrentCountry"); } }

		public Visibility FillButtonVisible { get => Countries?.Count > 0 ? Visibility.Hidden : Visibility.Visible; set { } }

		public BitmapImage SelectedImage { get => selectedImage; set { selectedImage = value; RaisePropertyChanged(); } }
    #endregion Properties
    #region Methods
    public void ResetCountryList() {
			Countries.Clear();
			foreach (var country in Repository.Members) { Countries.Add(new WpfCountry(country)); }		
		}
		public bool UpdateCountryList() {
			if (Countries?.Count > 0) {
				Repository.Members = new List<Country>(Countries);
				return true;
			}
			return false;
		}
		public int StandardFontSize { get; set; } = 12;
    internal void FilterResult() {
			Countries.Clear();
			List<WpfCountry> result = Repository.FindCapital(SearchText, UseCase, UseReverse, MaxResults).Select(s => new WpfCountry(s)).ToList();
			foreach(var c in result) {
				Countries.Add(c);
			}			
		}

    internal void FillWithInitialData() {
			Repository.FillWithInitialData();
			ResetCountryList();
			RaisePropertyChanged("FillButtonVisible");
    }

		protected void RaisePropertyChanged([CallerMemberName] string name = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
		}

		//protected void RaisePropertyChanged([CallerMemberName] string property="") => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
		public  bool Import(string json) {
			var ok=Repository.Import(json);
			ResetCountryList();
			return ok;
		}
		public string Export() {
			UpdateCountryList();
			return Repository?.Export();
		}
		public bool ImportFromFile(string path) {
			if (File.Exists(path)) {
				return Import(File.ReadAllText(path))==true;
			}
			return false;
		}
		public bool ExportToFile(string path) {
			try {
				File.WriteAllText(path, Export());
				return File.Exists(path);
			} catch { }
			return false;
		}

    private BitmapImage selectedImage = null;

    public BitmapImage TryLoadImage() {
			if (CurrentCountry.FlagData?.Length > 0) {
				MemoryStream stream = new MemoryStream(System.Convert.FromBase64String(CurrentCountry.FlagData));
				BitmapImage image = new BitmapImage();
				image.BeginInit();
				image.StreamSource = stream;
				image.EndInit();
				return image;
			}
			return null;
		}
		public string SaveImageAsB64(BitmapImage src, BitmapEncoder encoder = null) => src != null ? System.Convert.ToBase64String(SaveImage(src, encoder)) : null;
		public byte[] SaveImage(BitmapImage src, BitmapEncoder encoder=null) {
			if (src != null) {
				encoder ??= new PngBitmapEncoder();
				using MemoryStream ms = new MemoryStream();
				encoder.Frames.Add(BitmapFrame.Create(src));
				encoder.Save(ms);
				return ms.ToArray();
			}
			return null;
		}
		public Stretch ImageStrechMode { get=>Enum.Parse<Stretch>(SelectedImageStrechMode); set { SelectedImageStrechMode = value.ToString(); } }

    public string SelectedImageStrechMode { get => _selectedImageStrechMode; set { _selectedImageStrechMode = value; RaisePropertyChanged(); RaisePropertyChanged("ImageStrechMode"); } }

    private string _selectedImageStrechMode = Stretch.Uniform.ToString();
    #endregion Methods      
  }
}
