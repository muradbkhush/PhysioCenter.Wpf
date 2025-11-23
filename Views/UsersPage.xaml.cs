using PhysioCenter.Wpf.Domain;
using PhysioCenter.Wpf.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PhysioCenter.Wpf.Views
{
    public partial class UsersPage : UserControl
    {
        public UsersPage()
        {
            InitializeComponent();
            LoadUsers();
        }

        private void LoadUsers()
        {
            using var db = new AppDbContext();
            UsersGrid.ItemsSource = db.Users
                .OrderBy(u => u.Username)
                .ToList();
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            var win = new UserFormWindow();
            win.ShowDialog();
            LoadUsers();
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersGrid.SelectedItem is not User user)
                return;

            var win = new UserFormWindow(user.Id);
            win.ShowDialog();
            LoadUsers();
        }

        private void DeleteUser_Click(object sender, RoutedEventArgs e)
        {
            if (UsersGrid.SelectedItem is not User user)
                return;

            if (user.Role == "Admin")
            {
                MessageBox.Show("لا يمكن حذف حساب الأدمن.", "تحذير");
                return;
            }

            var confirm = MessageBox.Show(
                $"هل تريد حذف المستخدم {user.Username}؟",
                "تأكيد الحذف",
                MessageBoxButton.YesNo);

            if (confirm != MessageBoxResult.Yes)
                return;

            using var db = new AppDbContext();
            var u = db.Users.First(x => x.Id == user.Id);
            db.Users.Remove(u);
            db.SaveChanges();

            LoadUsers();
        }
    }
}
