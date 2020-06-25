
using System;
using System.Collections.Generic;
using System.Linq;
using SyntraAB.Tools.Extensions;
using System.IO;
using System.Text.Json;

namespace Syntra.Data.Models
{
	public class HoofdCategorieLijst
	{
		public const string DataFile = "HoofdCategorie.dat";
		public string LastError { get; protected set; } = "";
		public List<HoofdCategorie> Members { get; set; } = new List<HoofdCategorie>();
		public int Count { get => Members?.Count > 0 ? Members.Count : 0; }
		public string categ { get; set; }
		
		public HoofdCategorieLijst()
		{
			
		}
		public bool Import()
		{
			//string data = GetType().GetEmbeddedResource("HoofdCategories.json");
			string data = GetType().GetEmbeddedResource("newHoofdCateg.json");
			
			if (data.NotEmpty())
			{
				try
				{
					
					var jsonData = JsonSerializer.Deserialize<List<HoofdCategorie>>(data);
					if (jsonData != null)
					{
						if (Count > 0)
						{
							foreach (var hc in jsonData)
							{
								var item = Members.Where(t => t.ID == hc.ID).FirstOrDefault();
								if (item != null)
								{
									item.Naam = hc.Naam?.Length > 0 ? hc.Naam : item.Naam;


								}

								else
								{
									Members.Add(hc);

								}

							}
						}
						else Members.AddRange(jsonData);

					}
					return true;
				}
				catch (Exception ex) { LastError = ex.ToString(); }
				return false;
			}
			return false;
		}

		public bool Import(string json)
		{
			try
			{
				//var data = JsonConvert.DeserializeObject<HoofdCategorieWrapper>(json);
				var data = JsonSerializer.Deserialize<List<HoofdCategorie>>(json);
				if (data != null)
				{
					if (Count > 0)
					{
						foreach (var hc in data)
						{
							var item = Members.Where(t => t.ID == hc.ID).FirstOrDefault();
							if (item != null)
							{
								item.Naam = hc.Naam?.Length > 0 ? hc.Naam : item.Naam;
								

							}

							else { Members.Add(hc); 
							
							}

						}
					}
					else Members.AddRange(data);

				}
				
			}
			catch (Exception ex) { LastError = ex.ToString(); }
			return false;
		}



		public bool addHoofdCategorie(string hc_naam, string sc_naam )
		{
			if ((Members.Where(t => t.Naam == hc_naam).FirstOrDefault())==null)
			{
				var index = (Count + 1)*100;
				var subcats = new List<SubCategorie>();
				var sc_index = index + 1;
				SubCategorie sc = new SubCategorie(sc_index,sc_naam);
				subcats.Add(sc);

				HoofdCategorie hc = new HoofdCategorie(index, hc_naam);
				Members.Add(hc);
				return true;
			}
			else return false;

		}
		public bool addHoofdCategorie(string hc_naam)
		{
			if ((Members.Where(t => t.Naam == hc_naam).FirstOrDefault()) == null)
			{
				var index = (Count + 1) * 100;
				HoofdCategorie hc = new HoofdCategorie(index, hc_naam);
				Members.Add(hc);
				return true;
			}
			else return false;

		}

		public bool addHoofdCategorie(HoofdCategorie hc)
		{
			if (!Members.Contains(hc))
			{

				Members.Add(hc);
				return true;
			}
			else return false;

		}
		public bool renameHoofdCategorie(HoofdCategorie hc, string naam)
		{
			if (Members.Contains(hc)&&hc.Deleted==false)
			{

				return(hc.Rename(naam));
				
			}
			else return false;

		}
		public bool renameHoofdCategorie(string oud, string nieuw)
		{
			HoofdCategorie hc=Members.Where(t => t.Naam == oud).FirstOrDefault();
			if (hc!=null && hc.Deleted == false)
			{

				return (hc.Rename(nieuw));

			}
			else return false;

		}
		public bool deleteHoofdCategorie(HoofdCategorie hc)
		{
				return(hc.Deactivate());
				//return (Members.Remove(hc));

		}

		public bool deleteHoofdCategorie(string naam)
		{
			HoofdCategorie hc = Members.Where(t => t.Naam == naam).FirstOrDefault();
			if (hc != null)
			{
				return (hc.Deactivate());
				//return (Members.Remove(hc));

			}
			else return false;

		}
		public HoofdCategorie getHoofdCategorie(SubCategorie sc)
		{
			
			HoofdCategorie hc = Members.Where(t => t.ID==(sc.ID%100)*100).FirstOrDefault();
			return hc;
		}
		public HoofdCategorie getHoofdCategorie(string hc_naam)
		{
			HoofdCategorie hc = Members.Where(t => t.Naam == hc_naam).FirstOrDefault();
				
			return hc;
		}
		public HoofdCategorie getHoofdCategorie(int hc_id)
		{
			HoofdCategorie hc = Members.Where(t => t.ID == hc_id).FirstOrDefault();

			return hc;
		}


		/*public SubCategorie getSubCategorie(string sc_naam)
		{
			foreach (HoofdCategorie item in Members)
			{
				SubCategorie sc = item.SubCats.Where(t => t.Naam == sc_naam).FirstOrDefault();
				if (sc != null)
				{
					return item.SubCats.Where(t => t.Naam == sc_naam).FirstOrDefault();
				}
				
			}return null;
		}
		public bool behoortTot(SubCategorie sc, HoofdCategorie hc)
		{

			return hc.SubCats.Where(t => t.Naam == sc.Naam).FirstOrDefault() != null;
			
		}
*/
		public string Export() => JsonSerializer.Serialize(Members, new JsonSerializerOptions() { WriteIndented = true, IgnoreNullValues = true });
		//public string Export() => JsonConvert.SerializeObject(Members);

	}
}

