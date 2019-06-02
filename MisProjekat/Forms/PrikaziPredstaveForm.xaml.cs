using System.Windows;
using MisProjekat.Control;
using MisProjekat.Model;

namespace MisProjekat.Forms
{
    /// <summary>
    /// Interaction logic for PrikaziPredstaveForm.xaml
    /// </summary>
    public partial class PrikaziPredstaveForm
    {
        private PredstavaControl _predstavaControl;
        private Database _database;
        public Korisnik Korisnik { get; }

        public PrikaziPredstaveForm(Database database, Korisnik korisnik)
        {
            InitializeComponent();

            _database = database;
            Korisnik = korisnik;
            _predstavaControl = new PredstavaControl(database);

            foreach (var predstava in _predstavaControl.NadjiSvePredstave())
            {
                RepertoarList.Items.Add(predstava);
            }
        }

        private void RezervisiClick(object sender, RoutedEventArgs e)
        {
            if (sender is System.Windows.Controls.Control ctrl && ctrl.DataContext is Izvodjenje izv)
            {
                var rf = new RezervacijaForm(_database, izv, Korisnik);
                rf.Show();
            }

        }
    }
}
