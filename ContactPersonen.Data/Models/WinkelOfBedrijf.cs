using Syntra.Data.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Syntra.Data.Models
{
   public class WinkelOfBedrijf : Contactpersoon
    {

        [JsonPropertyName("openingsuren")]
        public List<string> Openingsuren { get; set; }

        [JsonPropertyName("sluitingsdagen")]
        public List<string> SluitingsDagen { get; set; }

        public WinkelOfBedrijf() { }
        public WinkelOfBedrijf(int id, string naam, string categorie, string telefoon, string adres, List<string> openingsuren, List<string> sluitingsdagen, string comment)
        {
            ID=id;
            Naam = naam;
            Categorie = categorie;
            Openingsuren = openingsuren;
            SluitingsDagen = sluitingsdagen;
            Telefoon = telefoon;
            Adres = adres;
            Comment = comment;
        }

    }
}