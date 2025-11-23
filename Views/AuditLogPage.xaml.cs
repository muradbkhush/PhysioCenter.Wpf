using PhysioCenter.Wpf.Infrastructure;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PhysioCenter.Wpf.Views
{
    public partial class AuditLogPage : UserControl
    {
        public AuditLogPage()
        {
            InitializeComponent();

            FromDate.SelectedDate = DateTime.Today.AddDays(-7);
            ToDate.SelectedDate = DateTime.Today;
            LoadLogs();
        }

        private void LoadLogs()
        {
            using var db = new AppDbContext();

            var from = FromDate.SelectedDate ?? DateTime.Today.AddDays(-7);
            var to = ToDate.SelectedDate?.AddDays(1).AddTicks(-1) ?? DateTime.Today.AddDays(1).AddTicks(-1);
            var usernameFilter = UsernameFilterBox.Text?.Trim();

            var query = db.AuditLogs.AsQueryable();

            query = query.Where(l => l.Date >= from && l.Date <= to);

            if (!string.IsNullOrWhiteSpace(usernameFilter))
            {
                query = query.Where(l => l.Username.Contains(usernameFilter));
            }

            var data = query
                .OrderByDescending(l => l.Date)
                .Take(500)
                .ToList();

            AuditGrid.ItemsSource = data;
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            LoadLogs();
        }
    }
}
