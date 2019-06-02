using System;
using System.Collections.Generic;
using System.Linq;
using MisProjekat.Model;

namespace MisProjekat.Control
{
    public class RezervacijaControl
    {
        private Database _database;

        public RezervacijaControl(Database database)
        {
            _database = database;
        }

        public void Dodaj(Rezervacija r)
        {
            _database.Rezervacije.Add(r);
        }

        public IEnumerable<Rezervacija> NadjiRezervacije(Korisnik user)
        {
            return _database.Rezervacije.Where(r => r.Korisnik == user && r.Karta == null);
        }
    }
}