using System.Linq;
using System.Windows;
using MisProjekat.Control;

namespace MisProjekat.Forms
{
    /// <summary>
    /// Interaction logic for LoginForm.xaml
    /// </summary>
    public partial class LoginForm
    {
        private Database _database;
        private KorisnikControl _korisnikControl;

        public LoginForm() : this(new Database())
        {
            _database.FillWithData();
        }

        public LoginForm(Database database)
        {
            InitializeComponent();
            _database = database;
            _korisnikControl = new KorisnikControl(database);
        }

        private void LoginClick(object sender, RoutedEventArgs e)
        {
            var username = UsernameBox.Text;
            var password = PasswordBox.Password;
            var user = _korisnikControl.Uloguj(username, password);
            if (user != null)
            {
                var ppf = new PrikaziPredstaveForm(_database, user);
                Application.Current.MainWindow = ppf;
                ppf.Show();
                Close();
                return;
            }

            var blagajnik = _korisnikControl.UlogujBlagajnk(username, password);
            if (blagajnik != null)
            {
                var ppf = new BlagajnikForm(_database, blagajnik);
                Application.Current.MainWindow = ppf;
                ppf.Show();
                Close();
                return;
            }

            if (username == "admin" && password == "admin")
            {
                var ppf = new AdminForm(_database);
                Application.Current.MainWindow = ppf;
                ppf.Show();
                Close();
                return;
            }

            MessageBox.Show("Nepostojeći korisnik!");
        }

        private void NoLoginClick(object sender, RoutedEventArgs e)
        {
        }

        private void RegisterClick(object sender, RoutedEventArgs e)
        {
            var ppf = new RegisterForm(_database);
            ppf.ShowDialog();
        }
    }
}
