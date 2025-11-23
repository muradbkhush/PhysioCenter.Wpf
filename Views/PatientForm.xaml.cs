using PhysioCenter.Wpf.Domain;
using PhysioCenter.Wpf.Infrastructure;
using System;
using System.Windows;

namespace PhysioCenter.Wpf.Views
{
    public partial class PatientForm : Window
    {
        public PatientForm()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();

            var patient = new Patient
            {
                FullName = NameBox.Text.Trim(),
                Phone = PhoneBox.Text.Trim(),
                BirthDate = BirthBox.SelectedDate,
                Notes = NotesBox.Text.Trim()
            };

            db.Patients.Add(patient);
            db.SaveChanges();

            MessageBox.Show("تم حفظ المراجع بنجاح.", "نجاح");
            this.Close();
        }
    }
}
