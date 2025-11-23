using Microsoft.EntityFrameworkCore;
using PhysioCenter.Wpf.Domain;
using PhysioCenter.Wpf.Infrastructure;
using System;
using System.Linq;
using System.Windows;

namespace PhysioCenter.Wpf.Views
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            ErrorTextBlock.Text = "";

            var username = UsernameBox.Text.Trim();
            var password = PasswordBox.Password.Trim();

            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ErrorTextBlock.Text = "رجاءً أدخل اسم المستخدم وكلمة المرور.";
                return;
            }

            using var db = new AppDbContext();

            // إنشاء مستخدم أول مرة فقط
            if (!db.Users.Any())
            {
                db.Users.Add(new User
                {
                    Username = "admin",
                    PasswordHash = "1234",
                    Role = "Admin"
                });
                db.SaveChanges();
            }

            // التحقق
            var user = db.Users
                .AsNoTracking()
                .FirstOrDefault(u => u.Username == username && u.PasswordHash == password);

            if (user is null)
            {
                ErrorTextBlock.Text = "بيانات الدخول غير صحيحة.";
                return;
            }

            try
            {
                var main = new MainWindow(user);
                main.Show();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"❌ حدث خطأ أثناء فتح البرنامج:\n\n{ex.Message}\n\n{ex.StackTrace}",
                    "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );
            }
        }
    }
}
