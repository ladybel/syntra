using Syntra.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media.Imaging;
using WpfContactPersonen.Tools;
using SyntraAB.Tools.Extensions;
namespace WpfContactPersonen.ViewModel
{
	public class WpfContactPersoon: FysiekeContactpersoon
	{

		public WpfContactPersoon() { }
		public WpfContactPersoon(FysiekeContactpersoon src)
		{
			CopyFrom(src);

		}
		private void CopyFrom(FysiekeContactpersoon src)
		{
			ID = src.ID;
			Naam = src.Naam;
			Voornaam = src.Voornaam;
			Categorie = src.Categorie;
			Adres = src.Adres;
			Telefoon = src.Telefoon;
			Foto = src.Foto;
			Comment = src.Comment;
			
		}
		

		public BitmapImage Afbeelding
		{
			get
			{
				return Foto.ToBitmap();
			}
			set
			{
				Foto = value.ToB64();
			}
		}

		public object Clone()
		{
			return MemberwiseClone();
		}
	}
}