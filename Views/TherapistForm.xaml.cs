using PhysioCenter.Wpf.Domain;
using PhysioCenter.Wpf.Infrastructure;
using System.Windows;

namespace PhysioCenter.Wpf.Views
{
    public partial class TherapistForm : Window
    {
        public TherapistForm()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();

            var therapist = new Therapist
            {
                FullName = NameBox.Text.Trim(),
                Specialty = SpecialtyBox.Text.Trim()
            };

            db.Therapists.Add(therapist);
            db.SaveChanges();

            MessageBox.Show("تم حفظ المعالج بنجاح.", "نجاح");
            this.Close();
        }
    }
}
