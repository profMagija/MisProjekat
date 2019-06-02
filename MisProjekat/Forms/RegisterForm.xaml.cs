using System.Windows;
using MisProjekat.Control;
using MisProjekat.Model;

namespace MisProjekat.Forms
{
    /// <summary>
    /// Interaction logic for RegisterForm.xaml
    /// </summary>
    public partial class RegisterForm
    {
        private readonly bool _blagajnik;
        private readonly KorisnikControl _korisnikControl;
        private readonly Database _database;

        public RegisterForm(Database database, bool blagajnik=false)
        {
            InitializeComponent();
            _database = database;
            _korisnikControl = new KorisnikControl(database);
            _blagajnik = blagajnik;
        }

        private void ConfirmClick(object sender, RoutedEventArgs e)
        {
            var user = UsernameText.Text;
            var pass = PasswordText.Password;

            if (string.IsNullOrWhiteSpace(user))
            {
                MessageBox.Show("Morate uneti korisničko ime", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_korisnikControl.PostojiKorisnik(user))
            {
                MessageBox.Show("Korisničko ime već postoji", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (string.IsNullOrWhiteSpace(pass))
            {
                MessageBox.Show("Morate uneti lozinku", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (pass != PasswordRepeatText.Password)
            {
                MessageBox.Show("Unete lozinke se ne poklapaju", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            if (_blagajnik)
            {
                _korisnikControl.DodajBlagajnika(new Blagajnik
                {
                    Ime = FirstNameText.Text,
                    Prezime = LastNameText.Text,
                    KorisnickoIme = user,
                    Lozinka = pass
                });
            }
            else
            {
                _korisnikControl.DodajKorisnika(new Korisnik
                {
                    Ime = FirstNameText.Text,
                    Prezime = LastNameText.Text,
                    KorisnickoIme = user,
                    Lozinka = pass
                });
            }


            MessageBox.Show("Uspešna registracija!", "Uspeh", MessageBoxButton.OK, MessageBoxImage.Information);

            Close();
        }
    }
}
