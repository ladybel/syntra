
using System;
using System.Collections.Generic;
using System.Linq;
using SyntraAB.Tools.Extensions;
using System.IO;
using System.Text.Json;

namespace Syntra.Data.Models
{
	public class SubCategorieLijst
	{
		public const string DataFile = "SubCategorie.dat";
		public string LastError { get; protected set; } = "";
		public List<SubCategorie> Members { get; set; } = new List<SubCategorie>();
		public int Count { get => Members?.Count > 0 ? Members.Count : 0; }


		public SubCategorieLijst()
		{
		}
		public bool Import()
		{ 
			string data = GetType().GetEmbeddedResource("newSubCateg.json");
			if (data.NotEmpty())
			{
				try
				{

					var jsonData = JsonSerializer.Deserialize<List<SubCategorie>>(data);
					if (jsonData != null)
					{
						if (Count > 0)
						{
							foreach (SubCategorie sc in jsonData)
							{
								var item = Members.Where(t => t.ID == sc.ID).FirstOrDefault();
								if (item != null)
								{
									item.Naam = sc.Naam?.Length > 0 ? sc.Naam : item.Naam;


								}

								else
								{
									Members.Add(sc);

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
		public bool AddSubCategorie(string sc_naam, int hoofd_id)
		{
			int index = 1;
			while(Members.Find(t => t.ID == hoofd_id + index) != null)
			{
				index++;
				if (index == 99)
				{
					return false;
				}

			}
			SubCategorie sc = new SubCategorie(hoofd_id+index, sc_naam);
			Members.Add(sc);
			return true;
		}
		public bool DeleteSubCategorie(SubCategorie sc)
		{
			return(Members.Remove(sc));
		}
		public bool RenameSubCategorie(SubCategorie sc, string sc_nieuwenaam)
		{
			return sc.Rename(sc_nieuwenaam);
		}
		public SubCategorie GetSubCategorie(string sc_naam)
		{
			SubCategorie sc =  Members.Where(t => t.Naam == sc_naam).FirstOrDefault();

			return sc;
		}
		public SubCategorie GetSubCategorie(int sc_id)
		{
			SubCategorie sc = Members.Where(t => t.ID == sc_id).FirstOrDefault();

			return sc;
		}
		public string Export() => JsonSerializer.Serialize(Members, new JsonSerializerOptions() { WriteIndented = true, IgnoreNullValues = true });

	}
}


	
