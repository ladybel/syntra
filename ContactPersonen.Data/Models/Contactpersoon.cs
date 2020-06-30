using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
namespace Syntra.Data.Models
{
    public class Contactpersoon
    {
        [JsonPropertyName("id")]
        public int ID { get; set; }
        [JsonPropertyName("naam")]
        public string Naam { get; set; }
        [JsonPropertyName("telefoon")]
        public string Telefoon { get; set; }
        [JsonPropertyName("adres")]
        public string Adres { get; set; }
      
        [JsonPropertyName("categorie")]
        public string Categorie { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }

        public Contactpersoon() { }
         public Contactpersoon(int id, string naam, string telefoon, string adres, string categorie, string comment)
         {
             ID = ID;
             Naam = naam;
             Telefoon = telefoon;
             Adres = adres;
             Categorie = categorie;
             Comment = comment;
         }
        
        public override string ToString() => Naam;

    }

}
