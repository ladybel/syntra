﻿using System.Text.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using SyntraAB.Tools.Extensions;
using System.IO;

namespace Syntra.Data.Models {

	public class CountryRepository {
		public const string DataFile = "Country.dat";
		public string LastError { get; protected set; } = "";
		public List<Country> Members { get; set; } = new List<Country>();	
		public int Count { get => Members?.Count > 0 ? Members.Count : 0; }
		public bool FillWithInitialData() {
			string data = GetType().GetEmbeddedResource("Countries.json");
			if (data.NotEmpty()) {
				return Import(data);
			}
			return false;
		}
		public CountryRepository() {
			LoadData();
		}

		public string StandardAppDir {
			get {
				string dir = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).TrimEnd('\\')}\SyntraAB";
				if (!Directory.Exists(dir)) {
					Directory.CreateDirectory(dir);
				}
				return dir;
			}
    }
		public string DataFilePath => $@"{StandardAppDir}\{DataFile}";
		public void LoadData() {
			if (File.Exists(DataFilePath)) {
				Import(File.ReadAllText(DataFilePath));
			}
		}

		public bool SaveData() {
			string json = Export();
			if (json?.Length > 0) {
				try {
					File.WriteAllText(DataFilePath, json);
					return true;
				} catch { }
			}
			return false;
		}

		public bool Import(string json) {
			try {
				var data = JsonSerializer.Deserialize<List<Country>>(json);
				if (data != null) {
					if (Count > 0) {
						foreach (var ctry in data) {
							var item = Members.Where(t => t.Name == ctry.Name).FirstOrDefault();
							if (item != null) {
								item.Capital = ctry.Capital?.Length > 0 ? ctry.Capital : item.Capital;
								item.Continent = ctry.Continent?.Length > 0 ? ctry.Continent : item.Continent;
								item.TelephoneCode = ctry.TelephoneCode > 0 ? ctry.TelephoneCode : item.TelephoneCode;
								item.LandArea = ctry.LandArea > 0 ? ctry.LandArea : item.LandArea;
								item.IsoCode = ctry.IsoCode > 0 ? ctry.IsoCode : item.IsoCode;
								item.NationalDish = ctry.NationalDish?.Length > 0 ? ctry.NationalDish : item.NationalDish;
								item.LifeExpectancy = ctry.LifeExpectancy > 0 ? ctry.LifeExpectancy : item.LifeExpectancy;
								item.Population = ctry.Population > 0 ? ctry.Population : item.Population;
								item.GovernmentType = ctry.GovernmentType?.Length > 0 ? ctry.GovernmentType : item.GovernmentType;
								item.CurrencyCode = ctry.CurrencyCode?.Length > 0 ? ctry.CurrencyCode : item.CurrencyCode;
								item.Currency = ctry.Currency?.Length > 0 ? ctry.Currency : item.Currency;
								item.Languages = ctry.Languages?.Count > 0 ? ctry.Languages.ToList() : item.Languages;
								item.AvarageTemperature = ctry.AvarageTemperature > 0 ? ctry.AvarageTemperature : item.AvarageTemperature;
								item.Abbreviation = ctry.Abbreviation?.Length > 0 ? ctry.Abbreviation : item.Abbreviation;
								item.FlagData = ctry.FlagData?.Length > 0 ? ctry.FlagData : item.FlagData;
							} else {
								Members.Add(ctry);
							}
						}
					} else Members.AddRange(data);
					return Members?.Count > 0;
				}
			} catch (Exception ex) { LastError = ex.ToString(); }
			return false;
		}

		public List<Country> FindCapital(string searchText, bool useCase = false, bool useReverse = false, int maxResults = 0) {
			if (searchText?.Length > 0 && Members?.Count > 0) {
				searchText = useCase ? searchText : searchText.ToLower();
				var query = Members.Where(t => useCase ? t.Capital?.StartsWith(searchText) == true : t.Capital?.ToLower().StartsWith(searchText) == true);
				query = query.OrderBy(o => o.Capital);
				if(maxResults>0 && query.Count() > maxResults) {
					query = query.Take(maxResults);
        }
				return useReverse ? query.Reverse().ToList() : query.ToList();
			}
			return new List<Country>();
		}

    public string Export() => JsonSerializer.Serialize(Members, new JsonSerializerOptions() { WriteIndented = true, IgnoreNullValues = true });
	}
}