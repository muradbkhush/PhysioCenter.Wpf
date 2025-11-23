using PhysioCenter.Wpf.Domain;
using PhysioCenter.Wpf.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PhysioCenter.Wpf.Views
{
    public partial class ServicesPage : UserControl
    {
        public ServicesPage()
        {
            InitializeComponent();
            LoadServices();
        }

        private void LoadServices()
        {
            using var db = new AppDbContext();
            var services = db.Services
                             .OrderBy(s => s.Name)
                             .ToList();

            ServicesGrid.ItemsSource = services;
        }

        private void AddService_Click(object sender, RoutedEventArgs e)
        {
            var win = new ServiceForm();
            win.ShowDialog();
            LoadServices();
        }
    }
}
