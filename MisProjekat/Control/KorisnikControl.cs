using System;
using System.Linq;
using MisProjekat.Model;

namespace MisProjekat.Control
{
    public class KorisnikControl
    {
        private Database _database;

        public KorisnikControl(Database database)
        {
            _database = database;
        }

        public void DodajKorisnika(Korisnik k)
        {
            _database.Korisnici.Add(k);
        }

        public void DodajBlagajnika(Blagajnik b)
        {
            _database.Blagajnici.Add(b);
        }

        public Korisnik Nadji(string ime)
        {
            return _database.Korisnici.FirstOrDefault(k => k.KorisnickoIme == ime);
        }

        public Korisnik Uloguj(string user, string pass)
        {
            return _database.Korisnici.FirstOrDefault(k => k.KorisnickoIme == user && k.Lozinka == pass);
        }

        public Blagajnik UlogujBlagajnk(string user, string pass)
        {
            return _database.Blagajnici.FirstOrDefault(k => k.KorisnickoIme == user && k.Lozinka == pass);
        }

        public bool PostojiKorisnik(string user)
        {
            return _database.Korisnici.Exists(u => u.KorisnickoIme == user) || _database.Blagajnici.Exists(u => u.KorisnickoIme == user);
        }
    }
}