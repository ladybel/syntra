using Syntra.Data.Models;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Syntra.Data.Models
{
    class WinkelOfBedrijf : Contactpersoon
    {

        [JsonPropertyName("openingsuren")]
        public List<string> Openingsuren { get; set; }

        [JsonPropertyName("sluitingsdagen")]
        public List<string> SluitingsDagen { get; set; }

        public WinkelOfBedrijf() { }
        public WinkelOfBedrijf(string naam, List<string> openingsuren, List<string> sluitingsdagen, string telefoon, string adres, string comment)
        {
            Naam = naam;
            Openingsuren = openingsuren;
            SluitingsDagen = sluitingsdagen;
            Telefoon = telefoon;
            Adres = adres;
            Comment = comment;
        }

    }
}