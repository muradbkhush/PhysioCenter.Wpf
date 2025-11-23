using PhysioCenter.Wpf.Domain;
using PhysioCenter.Wpf.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PhysioCenter.Wpf.Views
{
    public partial class PatientsPage : UserControl
    {
        public PatientsPage()
        {
            InitializeComponent();
            LoadPatients();
        }

        private void LoadPatients()
        {
            using var db = new AppDbContext();
            var patients = db.Patients
                             .OrderByDescending(p => p.Id)
                             .ToList();

            PatientsGrid.ItemsSource = patients;
        }

        private void AddPatient_Click(object sender, RoutedEventArgs e)
        {
            var win = new PatientForm();
            win.ShowDialog();
            LoadPatients();
        }
    }
}
