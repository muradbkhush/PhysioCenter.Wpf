using PhysioCenter.Wpf.Infrastructure;
using System;
using System.Linq;
using System.Windows.Controls;

namespace PhysioCenter.Wpf.Views
{
    public partial class DashboardPage : UserControl
    {
        public DashboardPage()
        {
            InitializeComponent();

            try
            {
                LoadStats();
            }
            catch (Exception ex)
            {
                // لو صار أي خطأ غير متوقع، ما نطيح البرنامج
                System.Diagnostics.Debug.WriteLine("Dashboard error: " + ex);
                SafeFallback();
            }
        }

        private void LoadStats()
        {
            try
            {
                using var db = new AppDbContext();

                // البطاقات
                PatientsCountText.Text = db.Patients.Count().ToString();
                TherapistsCountText.Text = db.Therapists.Count().ToString();

                TodayAppointmentsText.Text =
                    db.Appointments.Count(a => a.Date.Date == DateTime.Now.Date).ToString();

                CompletedSessionsText.Text =
                    db.Sessions.Count(s => s.ImprovementLevel > 0).ToString();

                // أقرب 5 مواعيد (مع حماية من الـ NULL)
                var upcoming = db.Appointments
                    .OrderBy(a => a.Date)
                    .Take(5)
                    .Select(a => new
                    {
                        PatientName = a.Patient != null ? a.Patient.FullName : "—",
                        TherapistName = a.Therapist != null ? a.Therapist.FullName : "—",
                        Date = a.Date.ToString("yyyy/MM/dd HH:mm"),
                        Status = a.Status
                    })
                    .ToList();

                UpcomingAppointmentsGrid.ItemsSource = upcoming;
            }
            catch (Exception ex)
            {
                // أي خطأ في الداتا → نطبع في الـ Debug ونستخدم قيم افتراضية
                System.Diagnostics.Debug.WriteLine("LoadStats error: " + ex);
                SafeFallback();
            }
        }

        private void SafeFallback()
        {
            // قيم افتراضية آمنة تمنع الكرش
            PatientsCountText.Text = "0";
            TherapistsCountText.Text = "0";
            TodayAppointmentsText.Text = "0";
            CompletedSessionsText.Text = "0";
            UpcomingAppointmentsGrid.ItemsSource = Array.Empty<object>();
        }
    }
}
