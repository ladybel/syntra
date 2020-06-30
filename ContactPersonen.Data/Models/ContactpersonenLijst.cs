using System;
using System.Collections.Generic;
using System.Linq;
using SyntraAB.Tools.Extensions;
using System.IO;
using System.Text.Json;
using System.Security.Cryptography;
//using Json.Net;

//using Newtonsoft.Json;

namespace Syntra.Data.Models
{
    public class ContactpersonenLijst
    {
		public const string DataFile = "Contactpersonen.dat";
		public string LastError { get; protected set; } = "";
		public List<Contactpersoon> Members { get; set; } = new List<Contactpersoon>();
		public int Count { get => Members?.Count > 0 ? Members.Count : 0; }
		//public string trans { get; set; }


		public ContactpersonenLijst()
		{

		}
		public string StandardAppDir
		{
			get
			{
				//aangepast om te testen, het mag terug naar de oorspronkelijke
				string dir = $@"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData).TrimEnd('\\')}\Syntra Eindwerk";
				if (!Directory.Exists(dir))
				{
					Directory.CreateDirectory(dir);
				}
				string dr = "C:\\Users\\maria\\Downloads\\Documents\\syntra\\ExamenOefening\\ContactPersonen.Data\\Data";
				return dr;
			}
		}
		public bool SaveData()
		{
			string json = Export();
			if (json?.Length > 0)
			{
				try
				{
					File.WriteAllText(DataFilePath, json);
					return true;
				}
				catch { }
			}
			return false;
		}
		public string DataFilePath => $@"{StandardAppDir}\{DataFile}";

		public bool Import()
        {
			//string data = GetType().GetEmbeddedResource("contactpersonen.json");
			string data = GetType().GetEmbeddedResource("contactpersonen.dat");
			ImportFysiekePersonen(data);
			ImportWinkelsEnBedrijven(data);
			return true;
		}
		public bool ImportFysiekePersonen(string data)
		{
			if (data.NotEmpty())
			{
				try
				{				
					var jsonData = JsonSerializer.Deserialize<List<FysiekeContactpersoon>>(data);
					if (jsonData != null)
					{
						jsonData.RemoveAll(cp => cp.Voornaam == null);
						if (Count > 0)
						{
							foreach (var cp in jsonData)
							{
								FysiekeContactpersoon fcp = (FysiekeContactpersoon)cp;
								FysiekeContactpersoon item = (FysiekeContactpersoon)Members.Where(t => t.ID == fcp.ID).FirstOrDefault();
								if (item != null)
								{
									item.Naam = fcp.Naam?.Length > 0 ? item.Naam : fcp.Naam;
									item.Voornaam = fcp.Voornaam?.Length > 0 ? item.Voornaam : fcp.Voornaam;
									item.Adres = fcp.Adres?.Length > 0 ? item.Adres : fcp.Adres;
									item.Telefoon = fcp.Telefoon?.Length > 0 ? item.Telefoon : fcp.Telefoon;
									item.Comment = fcp.Comment?.Length > 0 ? item.Comment : fcp.Comment;
								}

								else Members.Add(fcp);
							}
						}
						else Members.AddRange(jsonData);
						return Members?.Count > 0;
					}
				}
				catch (Exception ex) { LastError = ex.ToString(); }
				return false;
			}
			return false;
		}
		public bool ImportWinkelsEnBedrijven(string data)
		{

			if (data.NotEmpty())
			{
				try
				{
					var jsonData = JsonSerializer.Deserialize<List<WinkelOfBedrijf>>(data);
					if (jsonData != null)
					{
						jsonData.RemoveAll(cp => cp.Openingsuren == null);
						if (Count > 0)
						{
							foreach (var cp in jsonData)
							{
								WinkelOfBedrijf wbcp = (WinkelOfBedrijf)cp;
								WinkelOfBedrijf item = (WinkelOfBedrijf)Members.Where(t => t.ID == wbcp.ID).FirstOrDefault();
								if (item != null)
								{
									item.Naam = wbcp.Naam?.Length > 0 ? item.Naam : wbcp.Naam;									
									item.Adres = wbcp.Adres?.Length > 0 ? item.Adres : wbcp.Adres;
									item.Telefoon = wbcp.Telefoon?.Length > 0 ? item.Telefoon : wbcp.Telefoon;
									item.Comment = wbcp.Comment?.Length > 0 ? item.Comment : wbcp.Comment;
									item.Openingsuren = wbcp.Openingsuren?.Count > 0 ? item.Openingsuren : wbcp.Openingsuren;
									item.SluitingsDagen = wbcp.SluitingsDagen?.Count > 0 ? item.SluitingsDagen : wbcp.SluitingsDagen;
								}

								else Members.Add(wbcp);
							}
						}
						else Members.AddRange(jsonData);
						return Members?.Count > 0;
					}
				}
				catch (Exception ex) { LastError = ex.ToString(); }
				return false;
			}
			return false;
		}
		private string getJsonString()
		{
			string jsonstr = "[";
			foreach(var item in Members)
			{
				if(item.Categorie=="Fysieke contactpersoon") { 
				jsonstr+= JsonSerializer.Serialize((FysiekeContactpersoon)item, new JsonSerializerOptions() { WriteIndented = true, IgnoreNullValues = true });
				}
				else if (item.Categorie == "Winkel of bedrijf") {
					 jsonstr += JsonSerializer.Serialize((WinkelOfBedrijf)item, new JsonSerializerOptions() { WriteIndented = true, IgnoreNullValues = true });
				}
				jsonstr += ",";
			}
			jsonstr=jsonstr.Remove(jsonstr.Length - 1);
			jsonstr += "]";
			return jsonstr;	
		}


		public string Export() => getJsonString();
	}
}
