using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Syntra.Data;


namespace Syntra.Data.Models
{

    public class Klant
    {
        [JsonPropertyName("kl_id")]
        public int ID { get; set; }
        [JsonPropertyName("kl_naam")]
        public string Naam { get; set; }
        [JsonPropertyName("kl_adres")]
        public string Adres { get; set; }
        [JsonPropertyName("kl_gebdatum")]
        public DateTime GebDatum { get; set; }
        public string GebDatumStr { get; set; }


        public Klant()
        {


        }
       
        public Klant(int id, string naam, string adres, DateTime gebdatum)
        {
            string gds = gebdatum.ToShortDateString();
            ID = id;
            Naam = naam;
            Adres = adres;
            GebDatum = gebdatum;
            GebDatumStr = gds;

        }
        

    }
}

