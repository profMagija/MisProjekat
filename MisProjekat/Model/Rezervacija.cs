namespace MisProjekat.Model
{
    public class Rezervacija
    {
        public Karta Karta { get; set; }
        public Korisnik Korisnik { get; set; }
        public Izvodjenje Izvodjenje { get; set; }
        public string Sediste { get; set; }
    }
}