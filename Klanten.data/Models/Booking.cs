using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Syntra.Data;

namespace Syntra.Data.Models
{
   public class Booking
    {
        [JsonPropertyName("booking_id")]
        public int ID { get; set; }
        [JsonPropertyName("kl_id")]
        public int Klant_ID  { get; set; }
        [JsonPropertyName("tafel")]
        public int Tafel { get; set; }
        [JsonPropertyName("datum")]
        public DateTime Datum { get; set; }
        public string DatumStr { get; set; }

        public Booking()
        {


        }

        public Booking(int id, int kl_id, int tafel, DateTime datum)
        {
            string ds = datum.ToShortDateString();
            ID = id;
            Klant_ID = kl_id;
            Tafel =tafel;
            Datum =datum;
            DatumStr = ds;


        }
    }
}
