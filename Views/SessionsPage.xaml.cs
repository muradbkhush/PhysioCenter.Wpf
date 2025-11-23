using Microsoft.EntityFrameworkCore;
using PhysioCenter.Wpf.Infrastructure;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PhysioCenter.Wpf.Views
{
    public partial class SessionsPage : UserControl
    {
        public SessionsPage()
        {
            InitializeComponent();
            LoadSessions();
        }

        private void LoadSessions()
        {
            using var db = new AppDbContext();

            var data = db.Sessions
                         .Include(s => s.Patient)
                         .Include(s => s.Appointment)
                         .ThenInclude(a => a.Therapist)
                         .OrderByDescending(s => s.SessionDate)
                         .Take(100)
                         .AsEnumerable()
                         .Select(s => new
                         {
                             s.SessionDate,
                             s.ImprovementLevel,
                             Patient = s.Patient,
                             TherapistName = s.Appointment?.Therapist?.FullName ?? "",
                             ShortEvaluation = string.IsNullOrWhiteSpace(s.Evaluation)
                                ? ""
                                : (s.Evaluation.Length > 40
                                    ? s.Evaluation.Substring(0, 40) + "..."
                                    : s.Evaluation)
                         })
                         .ToList();

            SessionsGrid.ItemsSource = data;
        }

        private void AddSession_Click(object sender, RoutedEventArgs e)
        {
            var win = new SessionForm();
            win.ShowDialog();
            LoadSessions();
        }
    }
}
