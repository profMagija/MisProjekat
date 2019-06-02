using System.Linq;
using System.Windows;
using MisProjekat.Control;
using MisProjekat.Model;

namespace MisProjekat.Forms
{
    /// <summary>
    /// Interaction logic for RezervacijaForm.xaml
    /// </summary>
    public partial class RezervacijaForm : Window
    {
        private PredstavaControl _predstavaControl;
        private RezervacijaControl _rezervacijaControl;
        public Izvodjenje Izvodjenje { get; }
        public Korisnik Korisnik { get; }

        public RezervacijaForm(Database database, Izvodjenje i, Korisnik korisnik)
        {
            InitializeComponent();
            DataContext = this;
            Izvodjenje = i;
            Korisnik = korisnik;

            _predstavaControl = new PredstavaControl(database);
            _rezervacijaControl = new RezervacijaControl(database);

            var mesta = _predstavaControl.NadjiSlobodnaMesta(i).ToList();
            if (mesta.Any())
            {
                MestaBox.ItemsSource = mesta;
            }
            else
            {
                MestaBox.Visibility = Visibility.Collapsed;
                PotvrdiButton.Visibility = Visibility.Collapsed;
                NemaMesta.Visibility = Visibility.Visible;
            }
        }

        private void PotvrdiClick(object sender, RoutedEventArgs e)
        {

            if (MestaBox.SelectedItems.Count == 0)
            {
                MessageBox.Show("Morate izabrati barem jedno sedište", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            foreach (string sediste in MestaBox.SelectedItems)
            {
                _rezervacijaControl.Dodaj(new Rezervacija
                {
                    Izvodjenje = Izvodjenje,
                    Karta = null,
                    Korisnik = Korisnik,
                    Sediste = sediste
                });
            }

            MessageBox.Show("Uspešna rezervacija", "Uspeh", MessageBoxButton.OK, MessageBoxImage.Information);

            Close();
        }
    }
}
