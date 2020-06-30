using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Syntra.Data.Models
{
    public class FysiekeContactpersoon:Contactpersoon
    {

        /*[JsonPropertyName("naam")]
        public string Naam { get; set; }
        public string Telefoon { get; set; }
        [JsonPropertyName("adres")]
        public string Adres { get; set; }
/*
        [JsonPropertyName("categorie")]
        public int Categorie { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }*/

        [JsonPropertyName("voornaam")]
        public string Voornaam { get; set; }
       /*
        [JsonPropertyName("afbeelding")]
        public string Afbeelding{ get; set; }
        */

        public FysiekeContactpersoon() { }
         public FysiekeContactpersoon(int id,string naam, string voornaam,string telefoon, string adres, string categorie, string comment)
         {
             ID = id;
             Naam = naam;
             Voornaam = voornaam;
             Telefoon = telefoon;
             Adres = adres;
             Categorie = categorie;
             Comment = comment;
         }
        
        //public override string ToString() => Naam;

    }

}