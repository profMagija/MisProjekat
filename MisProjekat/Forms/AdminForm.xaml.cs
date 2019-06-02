using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using MisProjekat.Control;
using MisProjekat.Model;

namespace MisProjekat.Forms
{
    /// <summary>
    /// Interaction logic for AdminForm.xaml
    /// </summary>
    public partial class AdminForm
    {
        private readonly PredstavaControl _predstavaControl;
        private readonly Database _database;

        public AdminForm(Database database)
        {
            InitializeComponent();
            _database = database;
            _predstavaControl = new PredstavaControl(database);
            PredstaveListView.ItemsSource = _predstavaControl.NadjiSvePredstave().ToList();
        }

        private void NapraviBlagajnikaClick(object sender, RoutedEventArgs e)
        {
            var reg = new RegisterForm(_database, true);
            reg.ShowDialog();
        }

        private void NapraviPredstavuClick(object sender, RoutedEventArgs e)
        {
            _predstavaControl.Sacuvaj(new Predstava
            {
                Izvodjenja = new List<Izvodjenje>(),
                Naziv = "",
                Opis = ""
            });

            PredstaveListView.ItemsSource = _predstavaControl.NadjiSvePredstave().ToList();
        }

        private void DataGrid_OnAddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            var izvodjenje = new Izvodjenje {Predstava = (Predstava) ((DataGrid) sender).DataContext, CenaKarte = 0, Vreme = DateTime.Today};
            e.NewItem = izvodjenje;
            _predstavaControl.Sacuvaj(izvodjenje);
        }

        private void LogoutClick(object sender, RoutedEventArgs e)
        {
            var login = new LoginForm(_database);
            login.Show();
            Application.Current.MainWindow = login;
            Close(); 
        }
    }
}
