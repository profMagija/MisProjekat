using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using Accessibility;
using MisProjekat.Model;

namespace MisProjekat.Control
{
    public class PredstavaControl
    {
        private readonly Database _database;

        public PredstavaControl(Database database)
        {
            _database = database;
        }

        public void Sacuvaj(Predstava p)
        {
            if (!_database.Predstave.Contains(p))
                _database.Predstave.Add(p);

        }

        public void Sacuvaj(Izvodjenje i)
        {
            if (!i.Predstava.Izvodjenja.Contains(i))
                i.Predstava.Izvodjenja.Add(i);
        }

        public IEnumerable<string> NadjiSlobodnaMesta(Izvodjenje i)
        {
            return _database.Mesta.Except(_database.Rezervacije.Where(rez => rez.Izvodjenje == i).Select(rez => rez.Sediste));
        }

        public IEnumerable<Izvodjenje> NadjiSvaIzvodjenja()
        {
            return _database.Predstave.SelectMany(p => p.Izvodjenja).OrderBy(i => i.Vreme);
        }

        public IEnumerable<Predstava> NadjiSvePredstave()
        {
            return _database.Predstave.OrderBy(p => p.Naziv);
        }
    }
}
