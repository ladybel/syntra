using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using Syntra.Data;


namespace Syntra.Data.Models
{
  //  public enum SCType { id, naam }
    public class SubCategorie2
    {
              
        public int ID { get; set; }
        public string Naam { get; set; }
        
        public bool Deleted;
       

        public SubCategorie2()
        {

            
        }

        public SubCategorie2(int id,string naam)
        {
            ID = id;
            Naam = naam;
            Deleted = false;
            

        }
        public int hc_id ()
        {
            return ID % 100;
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


    }
}
