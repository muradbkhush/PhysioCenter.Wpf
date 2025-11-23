using PhysioCenter.Wpf.Domain;
using PhysioCenter.Wpf.Infrastructure;
using System;
using System.Linq;
using System.Windows;

namespace PhysioCenter.Wpf.Views
{
    public partial class SessionForm : Window
    {
        public SessionForm()
        {
            InitializeComponent();
            LoadPatients();
            DateBox.SelectedDate = DateTime.Today;
        }

        private void LoadPatients()
        {
            using var db = new AppDbContext();
            var patients = db.Patients
                             .OrderBy(p => p.FullName)
                             .ToList();

            PatientBox.ItemsSource = patients;
            PatientBox.DisplayMemberPath = "FullName";
        }

        private void LoadAppointmentsForPatient(int patientId)
        {
            using var db = new AppDbContext();
            var appointments = db.Appointments
                                 .Where(a => a.PatientId == patientId)
                                 .OrderByDescending(a => a.Date)
                                 .ToList();

            AppointmentBox.ItemsSource = appointments;
            AppointmentBox.DisplayMemberPath = "Date"; // سنعرضه كـ نص أدناه
            AppointmentBox.ItemTemplate = null; // نبقيها بسيطة الآن
        }

        private void PatientBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            AppointmentBox.ItemsSource = null;

            if (PatientBox.SelectedItem is Patient p)
            {
                LoadAppointmentsForPatient(p.Id);
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (PatientBox.SelectedItem is not Patient patient)
            {
                MessageBox.Show("الرجاء اختيار المراجع.");
                return;
            }

            if (AppointmentBox.SelectedItem is not Appointment appointment)
            {
                MessageBox.Show("الرجاء اختيار الموعد المرتبط.");
                return;
            }

            if (!DateBox.SelectedDate.HasValue)
            {
                MessageBox.Show("الرجاء اختيار تاريخ الجلسة.");
                return;
            }

            if (!TimeSpan.TryParse(TimeBox.Text.Trim(), out var time))
            {
                MessageBox.Show("صيغة الوقت غير صحيحة. مثال: 15:00");
                return;
            }

            if (!int.TryParse(ImprovementBox.Text.Trim(), out var level) ||
                level < 0 || level > 100)
            {
                MessageBox.Show("مستوى التحسن يجب أن يكون رقماً بين 0 و 100.");
                return;
            }

            using var db = new AppDbContext();

            var session = new Session
            {
                PatientId = patient.Id,
                AppointmentId = appointment.Id,
                SessionDate = DateBox.SelectedDate.Value.Date + time,
                ImprovementLevel = level,
                Evaluation = EvaluationBox.Text.Trim(),
                TreatmentPlan = PlanBox.Text.Trim()
            };

            db.Sessions.Add(session);
            db.SaveChanges();

            MessageBox.Show("تم حفظ الجلسة بنجاح.", "نجاح");
            this.Close();
        }
    }
}
