using System.Collections.Generic;
using System.Windows.Documents;

namespace MisProjekat.Model
{
    public class Predstava
    {
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public List<Izvodjenje> Izvodjenja { get; set; }
    }
}