using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;
using System.Collections.ObjectModel;

namespace Syntra.Data.Models
{
    
    public class HoofdCategorie
    {
       
        [JsonPropertyName("hc_naam")]
        public string Naam { get; set; }
        [JsonPropertyName("hc_id")]
        public int ID { get; set; }

        //public List<SubCategorie> SubCats { get; set; } = new List<SubCategorie>();
        public bool Deleted;


        public HoofdCategorie() { }
        public override bool Equals(object obj)
        {

            if (obj is HoofdCategorie hd)
            {
                return hd.ID == ID;
            }
            return base.Equals(obj);
        }

        public HoofdCategorie(int id, string naam)
        {
            ID = id;
            Naam = naam;
            //SubCats = subs;
            Deleted = false;

        }
         /*public bool addSubCategorie(string naam)
         {
             if (SubCats.Find(t => t.Naam ==naam)!=null){ 
            return false;
             }
             var index = ID+SubCats.Count + 1;
            SubCategorie sc = new SubCategorie(index,naam);
             SubCats.Add(sc);
             return true;
         }
        
        public bool addSubCategorie(SubCategorie sc)
        {
            if (SubCats.Contains(sc))
            {
                return false;
            }
           
            SubCats.Add(sc);
            return true;
        }
        public bool deleteSubCategorie(string naam)
        {
            SubCategorie sc = SubCats.Find(t => t.Naam == naam);
            if (sc== null)
            {
                return false;
            }
            sc.Deactivate();
            //SubCats.Remove(sc);
            return true;
        }

        public bool deleteSubCategorie(SubCategorie sc)
        {
            if (!SubCats.Contains(sc))
            {
                return false;
            }
            sc.Deactivate();
            //SubCats.Remove(sc);
            return true;
        }
        public bool renameSubCategorie(SubCategorie sc,string naam)
        {
            if (!SubCats.Contains(sc) && sc.Deleted == false)
            {
                return false;
            }

            sc.Rename(naam);
            return true;
        }
        public bool renameSubCategorie(string oud, string nieuw)
        {
            SubCategorie sc = SubCats.Find(t => t.Naam == oud);
            if (sc== null||sc.Deleted == true)
            {
                return false;
            }

            sc.Rename(nieuw);
            return true;
        }*/

        public bool Rename(string nieuw)
        {
            if (Deleted == false)
            {
                Naam = nieuw; return true;
            }
            else return false;
        }
        public bool Deactivate()
        {
            /*foreach(SubCategorie sc in SubCats) 
            {
                sc.Deleted = true;
            }*/
            Deleted = true;
            return true;
        }
        public override string ToString() => Naam;

    }
    
}