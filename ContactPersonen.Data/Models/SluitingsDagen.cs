using System;
using System.Collections.Generic;
using System.Text;

namespace Syntra.Data.Models
{
    public class SluitingsDagen
    {
        public string Omschrijving { get; set; } = null;


        public SluitingsDagen(string oms)
        {
            Omschrijving = oms;

        }
    }
}
