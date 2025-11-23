using PhysioCenter.Wpf.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PhysioCenter.Wpf.Views
{
    public partial class TherapistsPage : UserControl
    {
        public TherapistsPage()
        {
            InitializeComponent();
            LoadTherapists();
        }

        private void LoadTherapists()
        {
            using var db = new AppDbContext();
            var therapists = db.Therapists
                               .OrderByDescending(t => t.Id)
                               .ToList();

            TherapistsGrid.ItemsSource = therapists;
        }

        private void AddTherapist_Click(object sender, RoutedEventArgs e)
        {
            var win = new TherapistForm();
            win.ShowDialog();
            LoadTherapists();
        }
    }
}
