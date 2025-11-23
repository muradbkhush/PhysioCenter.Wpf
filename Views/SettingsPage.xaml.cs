using Microsoft.Win32;
using PhysioCenter.Wpf.Domain;
using PhysioCenter.Wpf.Infrastructure;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PhysioCenter.Wpf.Views
{
    public partial class SettingsPage : UserControl
    {
        private string? _logoPathTemp = null;

        public SettingsPage()
        {
            InitializeComponent();
            LoadSettings();
        }

        private void LoadSettings()
        {
            using var db = new AppDbContext();

            var settings = db.ClinicSettings.FirstOrDefault();
            if (settings is null)
            {
                return;
            }

            ClinicNameBox.Text = settings.ClinicName;
            AddressBox.Text = settings.Address;
            PhoneBox.Text = settings.Phone;

            if (!string.IsNullOrWhiteSpace(settings.LogoPath) && File.Exists(settings.LogoPath))
            {
                LogoPreview.Source = new BitmapImage(new System.Uri(settings.LogoPath));
            }
        }

        private void SelectLogo_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog();
            dialog.Filter = "Image Files|*.png;*.jpg;*.jpeg;*.bmp";

            if (dialog.ShowDialog() == true)
            {
                _logoPathTemp = dialog.FileName;
                LogoPreview.Source = new BitmapImage(new System.Uri(_logoPathTemp));
            }
        }

        private void SaveSettings_Click(object sender, RoutedEventArgs e)
        {
            using var db = new AppDbContext();

            var settings = db.ClinicSettings.FirstOrDefault();
            if (settings is null)
            {
                settings = new ClinicSettings();
                db.ClinicSettings.Add(settings);
            }

            settings.ClinicName = ClinicNameBox.Text.Trim();
            settings.Address = AddressBox.Text.Trim();
            settings.Phone = PhoneBox.Text.Trim();

            if (_logoPathTemp != null)
            {
                string destPath = System.IO.Path.Combine(
                    Directory.GetCurrentDirectory(),
                    "clinic_logo" + System.IO.Path.GetExtension(_logoPathTemp));

                File.Copy(_logoPathTemp, destPath, overwrite: true);
                settings.LogoPath = destPath;
            }

            db.SaveChanges();

            MessageBox.Show("تم حفظ الإعدادات بنجاح", "Success");
        }
    }
}
