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
       /* [JsonPropertyName("extra")]
        public ExtraInfo Extra { get; set; }*/
        /*[JsonPropertyName("categorie")]
        public int Categorie { get; set; }*/

        [JsonPropertyName("comment")]
        public string Comment { get; set; }

        public Contactpersoon() { }
         public Contactpersoon(string naam, string telefoon, string adres, string comment)
         {
             Naam = naam;
             Telefoon = telefoon;
             Adres = adres;
             Comment = comment;
         }
        
        public override string ToString() => Naam;

    }

}
