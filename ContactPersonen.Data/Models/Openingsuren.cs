using System;
using System.Collections.Generic;
using System.Text;

namespace Syntra.Data.Models
{   
    public class Openingsuren
    {
        public string Omschrijving { get; set; } = null;
        

        public Openingsuren(string oms)
        {
            Omschrijving = oms;
         
        }
    }
}
