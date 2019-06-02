using System;
using System.Collections.Generic;
using System.Linq;
using MisProjekat.Model;

namespace MisProjekat.Control
{
    public class Database
    {
        public List<Predstava> Predstave { get; } = new List<Predstava>();
        public List<Karta> Karte { get; } = new List<Karta>();
        public List<Rezervacija> Rezervacije { get; } = new List<Rezervacija>();
        public List<string> Mesta { get; } = new List<string> {"1-1", "1-2", "1-3"};
        public List<Korisnik> Korisnici { get; } = new List<Korisnik>();
        public List<Blagajnik> Blagajnici { get; } = new List<Blagajnik>();

        internal void FillWithData()
        {
            Korisnici.Add(new Korisnik
            {
                Ime = "Petar",
                Prezime = "Petrovic",
                KorisnickoIme = "petar",
                Lozinka = "a",
            });

            Blagajnici.Add(new Blagajnik
            {
                Ime = "Marko",
                Prezime = "Markovic",
                KorisnickoIme = "marko",
                Lozinka = "a"
            });

            FillPredstava("РОМЕО И ЈУЛИЈА", @"балет у два чина

Либрето: Л. Лавровски, С. Е. Радлов, по истоименој трaгедији В. Шекспира
Кореографија и режија: Константин Костјуков
Диригент: Микица Јевтић

Премијера: 17. април 2014, сцена „Јован Ђорђевић“

Представа траје два сата и десет минута, с једном паузом.", DateTime.Today.AddHours(19), DateTime.Today.AddHours(19 + 24*2));

            FillPredstava("ЋЕЛАВА ПЕВАЧИЦА", @"Ежен Јонеско
ЋЕЛАВА ПЕВАЧИЦА

Копродукција СНП-а и Агенције „БЕБА 021″

Режија: Беба Балашевић

 

Премијера: сцена „Пера Добриновић“, 10. децембар 2009.", DateTime.Today.AddHours(19+24), DateTime.Today.AddHours(19 + 24*3));

            Rezervacije.Add(new Rezervacija
            {
                Izvodjenje = Predstave[0].Izvodjenja[0],
                Karta = null,
                Korisnik =  Korisnici[0],
                Sediste = Mesta[0]
            });
        }

        private void FillPredstava(string naziv, string opis, params DateTime[] izs)
        {
            var p = new Predstava {Naziv = naziv, Opis = opis};
            p.Izvodjenja = izs.Select(i => new Izvodjenje { Predstava = p, Vreme = i, CenaKarte = 450M }).ToList();
            Predstave.Add(p);
        }
    }
}