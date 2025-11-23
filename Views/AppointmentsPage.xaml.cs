using PhysioCenter.Wpf.Infrastructure;
using PhysioCenter.Wpf.Domain;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PhysioCenter.Wpf.Views
{
    public partial class AppointmentsPage : UserControl
    {
        public AppointmentsPage()
        {
            InitializeComponent();
            LoadAppointments(DateTime.Today);
        }

        private void LoadAppointments(DateTime date)
        {
            using var db = new AppDbContext();

            var data = db.Appointments
                .Where(a => a.Date.Date == date.Date)
                .Select(a => new
                {
                    a.Id,
                    a.Date,
                    a.Status,
                    Patient = a.Patient,
                    Therapist = a.Therapist
                })
                .ToList();

            AppointmentsGrid.ItemsSource = data;
        }

        private void AppointmentsCalendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            if (AppointmentsCalendar.SelectedDate.HasValue)
            {
                LoadAppointments(AppointmentsCalendar.SelectedDate.Value);
            }
        }

        private void AddAppointment_Click(object sender, RoutedEventArgs e)
        {
            var win = new AppointmentForm();
            win.ShowDialog();

            if (AppointmentsCalendar.SelectedDate.HasValue)
                LoadAppointments(AppointmentsCalendar.SelectedDate.Value);
            else
                LoadAppointments(DateTime.Today);
        }
    }
}
