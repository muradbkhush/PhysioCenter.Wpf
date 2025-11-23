using PhysioCenter.Wpf.Domain;
using System.Windows;

namespace PhysioCenter.Wpf.Views
{
    public partial class MainWindow : Window
    {
        private readonly User _currentUser;

        public MainWindow(User currentUser)
        {
            InitializeComponent();
            _currentUser = currentUser;
            LoadUserInfo();

            // افتراضياً: عرض الداشبورد
            ContentHost.Children.Clear();
            ContentHost.Children.Add(new DashboardPage());
        }

        private void LoadUserInfo()
        {
            CurrentUserText.Text = $"المستخدم: {_currentUser.Username} ({_currentUser.Role})";
        }

        private void DashboardButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Children.Clear();
            ContentHost.Children.Add(new DashboardPage());
        }

        private void PatientsButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Children.Clear();
            ContentHost.Children.Add(new PatientsPage());
        }

        private void TherapistsButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Children.Clear();
            ContentHost.Children.Add(new TherapistsPage());
        }

        private void AppointmentsButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Children.Clear();
            ContentHost.Children.Add(new AppointmentsPage());
        }

        private void SessionsButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Children.Clear();
            ContentHost.Children.Add(new SessionsPage());
        }

        private void FinanceButton_Click(object sender, RoutedEventArgs e)
        {
            // اخترت حالياً عرض صفحة الفواتير كمدخل مالي
            ContentHost.Children.Clear();
            ContentHost.Children.Add(new InvoicesPage());
        }

        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Children.Clear();
            ContentHost.Children.Add(new UsersPage());
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Children.Clear();
            ContentHost.Children.Add(new SettingsPage());
        }

        private void AuditLogButton_Click(object sender, RoutedEventArgs e)
        {
            ContentHost.Children.Clear();
            ContentHost.Children.Add(new AuditLogPage());
        }
    }
}
