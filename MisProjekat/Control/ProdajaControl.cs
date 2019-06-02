using System;
using MisProjekat.Model;

namespace MisProjekat.Control
{
    public class ProdajaControl
    {
        private Database _database;

        public ProdajaControl(Database database)
        {
            _database = database;
        }

        public void Prodaj(Izvodjenje i, string sediste, decimal popust)
        {
            var k = new Karta
            {
                Cena = i.CenaKarte * (1 - popust),
                Popust = popust
            };

            var r = new Rezervacija
            {
                Izvodjenje = i,
                Karta = k,
                Korisnik = null,
                Sediste = sediste
            };
            ProdajPoRez(k, r);
        }

        public void ProdajPoRez(Karta k, Rezervacija r)
        {
            _database.Rezervacije.Add(r);
            _database.Karte.Add(k);
        }
    }
}