using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Syntra.Data.Models
{
   public class TransactiePost
    {

        [JsonPropertyName("tr_id")]
        public int ID { get; set; }
        [JsonPropertyName("tr_datum")]
        public DateTime Datum { get; set; }
        
        public HoofdCategorie HoofdCat { get; set; }
        public SubCategorie SubCat { get; set; }
       
        public string Comment { get; set; }
        //[JsonPropertyName("tr_debet")]
        public double Debet { get; set; }
        //[JsonPropertyName("tr_credit")]
        public double Credit { get; set; }

        public string DatumString { get; set; }



        public TransactiePost() { }
        

        public TransactiePost(int id, DateTime datum, HoofdCategorie hoofdcat, SubCategorie subcat, string comment, double debet, double credit)
            {
            string ds= datum.ToShortDateString();
            
            ID = id;
            Datum = datum;
            HoofdCat = hoofdcat;
            SubCat = subcat;
            Comment = comment;
            Debet = debet;
            Credit = credit;
            DatumString = ds;
        }
        /*public TransactiePost(int id, DateTime datum, string subcat,string hoofdcat, string comment, double debet, double credit)
        {
            string ds = datum.ToShortDateString();
            ID = id;
            Datum = datum;
             HoofdCat= hoofdcat;
            SubCat = subcat;
            Comment = comment;
            Debet = debet;
            Credit = credit;
            DatumString = ds;
        }

    */

    }
}
