using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MisProjekat.Control;
using MisProjekat.Model;

namespace MisProjekat.Forms
{
    /// <summary>
    /// Interaction logic for BlagajnikForm.xaml
    /// </summary>
    public partial class BlagajnikForm
    {
        private readonly ProdajaControl _prodajaControl;
        private readonly KorisnikControl _korisnikControl;
        private readonly PredstavaControl _predstavaControl;
        private RezervacijaControl _rezervacijaControl;
        public Blagajnik Blagajnik { get; }

        public BlagajnikForm(Database database, Blagajnik blagajnik)
        {
            InitializeComponent();
            Blagajnik = blagajnik;

            _prodajaControl = new ProdajaControl(database);
            _korisnikControl = new KorisnikControl(database);
            _predstavaControl = new PredstavaControl(database);
            _rezervacijaControl = new RezervacijaControl(database);

            PopustKartaComboBox.ItemsSource = PopustComboBox.ItemsSource = new List<Popust>
            {
                new Popust {Naziv = "Ništa", Vrednost = 0M},
                new Popust {Naziv = "Studentski 30%", Vrednost = 0.3M},
                new Popust {Naziv = "Penzionerski 30%", Vrednost = 0.3M}
            };
            PopustComboBox.SelectedIndex = 0;
            PopustKartaComboBox.SelectedIndex = 0;

            IzvodjenjaListView.ItemsSource = _predstavaControl.NadjiSvaIzvodjenja();
        }

        private void NadjiKorisnikaClick(object sender, RoutedEventArgs e)
        {
            RezervacijeListView.ItemsSource = Enumerable.Empty<Rezervacija>();

            if (UsernameText.Text == "")
                return;


            var user = _korisnikControl.Nadji(UsernameText.Text);

            if (user == null)
            {
                MessageBox.Show("Ne postoji takav korisnik", "Greška", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                return;
            }

            var rezs = _rezervacijaControl.NadjiRezervacije(user);

            RezervacijeListView.ItemsSource = rezs.ToList();
        }

        private void StampajClick(object sender, RoutedEventArgs e)
        {
            var popust = (Popust) PopustComboBox.SelectionBoxItem;

            var total = RezervacijeListView.SelectedItems.OfType<Rezervacija>().Select(x => x.Izvodjenje.CenaKarte * (1 - popust.Vrednost)).Sum();

            MessageBox.Show("Štampam račun, ukupno: " + total.ToString("F2"), "Štampa", MessageBoxButton.OK, MessageBoxImage.Information);

            foreach (var item in RezervacijeListView.SelectedItems.OfType<Rezervacija>())
            {
                item.Karta = new Karta {Cena = item.Izvodjenje.CenaKarte * (1 - popust.Vrednost), Popust = popust.Vrednost};
                _prodajaControl.ProdajPoRez(item.Karta, item);
                MessageBox.Show("Štampam kartu " + item.Sediste, "Štampa", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            UsernameText.Text = "";
            NadjiKorisnikaClick(null, null); // refresh the page
        }

        private void RekalkulisiCenu(object sender, SelectionChangedEventArgs e)
        {
            var popust = PopustComboBox.SelectedItem as Popust;
            if (popust == null)
                return;
            var rezs = RezervacijeListView.SelectedItems.OfType<Rezervacija>().Select(x => x.Izvodjenje.CenaKarte * (1 - popust.Vrednost)).Sum();

            UkupnoTextBlock.Text = "Ukupno: " + rezs.ToString("F2");
        }

        private void IzvodjenjaListView_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var izv = IzvodjenjaListView.SelectedItem as Izvodjenje;

            SedistaListView.ItemsSource = new List<string>();

            if (izv == null)
                return;

            SedistaListView.ItemsSource = _predstavaControl.NadjiSlobodnaMesta(izv);
            SedistaListView.SelectedIndex = -1;
        }

        private void RekalkulisiKartaCenu(object sender, SelectionChangedEventArgs e)
        {
            var izv = IzvodjenjaListView.SelectedItem as Izvodjenje;

            var popust = PopustKartaComboBox.SelectedItem as Popust;

            if (popust == null || izv == null)
                return;

            var rezs = izv.CenaKarte * SedistaListView.SelectedItems.Count * (1 - popust.Vrednost);

            UkupnoKartaTextBlock.Text = "Ukupno: " + rezs.ToString("F2");
        }

        private void StampajKartaClick(object sender, RoutedEventArgs e)
        {
            var izv = IzvodjenjaListView.SelectedItem as Izvodjenje;
            var popust = PopustKartaComboBox.SelectedItem as Popust;
            if (popust == null || izv == null)
                return;

            var total = izv.CenaKarte * SedistaListView.SelectedItems.Count * (1 - popust.Vrednost);

            MessageBox.Show("Štampam račun, ukupno: " + total.ToString("F2"), "Štampa", MessageBoxButton.OK, MessageBoxImage.Information);

            foreach (var item in SedistaListView.SelectedItems.OfType<string>())
            {
                _prodajaControl.Prodaj(izv, item, popust.Vrednost);
                MessageBox.Show("Štampam kartu " + item, "Štampa", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            UsernameText.Text = "";
            NadjiKorisnikaClick(sender, null); // refresh the page
            IzvodjenjaListView.SelectedIndex = -1;
            PopustKartaComboBox.SelectedIndex = 0;
        }

        private void UsernameText_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                NadjiKorisnikaClick(sender, null);
            }
        }
    }
}
