using PhysioCenter.Wpf.Domain;
using PhysioCenter.Wpf.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PhysioCenter.Wpf.Views
{
    public partial class AppointmentForm : Window
    {
        private int? _appointmentId;

        public AppointmentForm(int? appointmentId = null)
        {
            InitializeComponent();
            _appointmentId = appointmentId;

            LoadPatients();
            LoadTherapists();

            if (appointmentId != null)
                LoadAppointment();
        }

        private void LoadPatients()
        {
            using var db = new AppDbContext();
            PatientCombo.ItemsSource = db.Patients.ToList();
            PatientCombo.DisplayMemberPath = "FullName";
            PatientCombo.SelectedValuePath = "Id";
        }

        private void LoadTherapists()
        {
            using var db = new AppDbContext();
            TherapistCombo.ItemsSource = db.Therapists.ToList();
            TherapistCombo.DisplayMemberPath = "FullName";
            TherapistCombo.SelectedValuePath = "Id";
        }

        private void LoadAppointment()
        {
            using var db = new AppDbContext();
            var ap = db.Appointments.First(a => a.Id == _appointmentId);

            DatePicker.SelectedDate = ap.Date;
            StatusCombo.Text = ap.Status;

            PatientCombo.SelectedValue = ap.PatientId;
            TherapistCombo.SelectedValue = ap.TherapistId;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();

            if (_appointmentId == null)
            {
                db.Appointments.Add(new Appointment
                {
                    Date = DatePicker.SelectedDate!.Value,
                    Status = (StatusCombo.SelectedItem as ComboBoxItem)!.Content.ToString(),
                    PatientId = (int)PatientCombo.SelectedValue!,
                    TherapistId = (int)TherapistCombo.SelectedValue!
                });
            }
            else
            {
                var ap = db.Appointments.First(a => a.Id == _appointmentId);

                ap.Date = DatePicker.SelectedDate!.Value;
                ap.Status = (StatusCombo.SelectedItem as ComboBoxItem)!.Content.ToString();
                ap.PatientId = (int)PatientCombo.SelectedValue!;
                ap.TherapistId = (int)TherapistCombo.SelectedValue!;
            }

            db.SaveChanges();
            Close();
        }
    }
}
