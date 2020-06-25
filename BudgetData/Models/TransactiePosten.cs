using System;
using System.Collections.Generic;
using System.Linq;
using SyntraAB.Tools.Extensions;
using System.IO;
using System.Text.Json;

namespace Syntra.Data.Models
{
	public class TransactiePosten
	{
		public const string DataFile = "TransactiePosten.dat";
		public string LastError { get; protected set; } = "";
		public List<TransactiePost> Members { get; set; } = new List<TransactiePost>();
		public int Count { get => Members?.Count > 0 ? Members.Count : 0; }
		public string trans { get; set; }

		public TransactiePosten()
		{

		}
		public bool Import()
		{

			string data = GetType().GetEmbeddedResource("newTrMetHoofdcat.json");

			if (data.NotEmpty())
			{
				try
				{
					var jsonData = JsonSerializer.Deserialize<List<TransactiePost>>(data);
					if (jsonData != null)
					{
						if (Count > 0)
						{
							/*foreach (var tp in data)
							{
								var item = Members.Where(t => t.ID == tp.ID).FirstOrDefault();
								if (item != null)
								{
									item.Naam = hc.Naam?.Length > 0 ? hc.Naam : item.Naam;
									//item.load_subcategories();

								}

								else
								{
									Members.Add(hc);
									//hc.load_subcategories();
								}

							}*/
							Members.Clear();
						}
						Members.AddRange(jsonData);

					}
					return true;
				}
				catch (Exception ex) { LastError = ex.ToString(); }
				return false;
			}
			return false;
		}

		/*public bool Import(string json)
		{
			try
			{
				var data = JsonSerializer.Deserialize<List<TransactiePost>>(json);
				if (data != null)
				{
					if (Count > 0)
					{
						foreach (var tp in data)
						{
							var item = Members.Where(t => t.ID == tp.ID).FirstOrDefault();
							if (item != null)
							{
								item.Naam = hc.Naam?.Length > 0 ? hc.Naam : item.Naam;
								//item.load_subcategories();

							}

							else
							{
								Members.Add(hc);
								//hc.load_subcategories();
							}

						}
						Members.Clear();
					}
					Members.AddRange(data);

				}

			}
			catch (Exception ex) { LastError = ex.ToString(); }
			return false;
		}
			*/


		public bool addTransactiePost(DateTime datum, HoofdCategorie hc,SubCategorie sc, string comment, double debet, double credit )
		//public bool addTransactiePost(DateTime datum, SubCategorie sc, string comment, double debet, double credit)
		{

			var index =  1001+Count;
				TransactiePost tp = new TransactiePost(index, datum,hc, sc, comment,debet, credit);
				// TransactiePost tp = new TransactiePost(index, datum, sc, comment, debet, credit);
				Members.Add(tp);
				return true;

		}
		/*public bool addTransactiePost(DateTime datum, string sc_naam, string comment, decimal debet, decimal credit)
		{

			var index = 1001 + Count;
			HoofdCategorieLijst HoofdCatLijst = 
			TransactiePost tp = new TransactiePost(index, datum, sc, comment, debet, credit);
			Members.Add(tp);
			return true;

		}*/

		public bool deleteTransactiePost(TransactiePost tp)
		{

			return (Members.Remove(tp));
			

		}


		public string Export() => JsonSerializer.Serialize(Members, new JsonSerializerOptions() { WriteIndented = true, IgnoreNullValues = true });
		//public string Export() => JsonConvert.SerializeObject(Members);

	}
}
