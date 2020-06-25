using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Syntra.Data;


namespace Syntra.Data.Models
{
   
    public class SubCategorie
    {
        [JsonPropertyName("sc_id")]
        public int ID { get; set; }

        [JsonPropertyName("sc_naam")]
        public string Naam { get; set; }

        public bool Deleted;


        public SubCategorie()
        {


        }
        public override bool Equals(object obj)
        {
            
            if (obj is SubCategorie sb)
            {
                return sb.ID == ID;
            }
            return base.Equals(obj);
        }
        public SubCategorie(int id, string naam)
        {
            ID = id;
            Naam = naam;
            Deleted = false;


        }
        public int hc_id()
        {
            return (ID % 100)*100;
        }

        public bool Rename(string naam)
        {
            Naam = naam;
            return true;
        }
        public bool Deactivate()
        {
            return Deleted = true;

        }
       public override string ToString() => Naam;

    }
}
