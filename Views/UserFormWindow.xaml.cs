using PhysioCenter.Wpf.Domain;
using PhysioCenter.Wpf.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PhysioCenter.Wpf.Views
{
    public partial class UserFormWindow : Window
    {
        private int? _userId;

        public UserFormWindow(int? userId = null)
        {
            InitializeComponent();
            _userId = userId;

            if (userId != null)
                LoadUser();
        }

        private void LoadUser()
        {
            using var db = new AppDbContext();
            var user = db.Users.First(u => u.Id == _userId);

            TitleText.Text = "تعديل مستخدم";
            UsernameBox.Text = user.Username;
            RoleCombo.Text = user.Role;
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();

            if (_userId == null)
            {
                db.Users.Add(new User
                {
                    Username = UsernameBox.Text.Trim(),
                    PasswordHash = PasswordBox.Password.Trim(),
                    Role = (RoleCombo.SelectedItem as ComboBoxItem)!.Content.ToString()
                });
            }
            else
            {
                var user = db.Users.First(u => u.Id == _userId);
                user.Username = UsernameBox.Text.Trim();

                if (!string.IsNullOrWhiteSpace(PasswordBox.Password))
                    user.PasswordHash = PasswordBox.Password.Trim();

                user.Role = (RoleCombo.SelectedItem as ComboBoxItem)!.Content.ToString();
            }

            db.SaveChanges();
            Close();
        }
    }
}
