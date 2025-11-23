using PhysioCenter.Wpf.Domain;
using PhysioCenter.Wpf.Infrastructure;
using System;
using System.Windows;

namespace PhysioCenter.Wpf.Views
{
    public partial class ServiceForm : Window
    {
        public ServiceForm()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameBox.Text))
            {
                MessageBox.Show("اسم الخدمة مطلوب.");
                return;
            }

            if (!decimal.TryParse(PriceBox.Text.Trim(), out decimal price))
            {
                MessageBox.Show("السعر يجب أن يكون رقم.");
                return;
            }

            using var db = new AppDbContext();

            var service = new Service
            {
                Name = NameBox.Text.Trim(),
                Price = price
            };

            db.Services.Add(service);
            db.SaveChanges();

            MessageBox.Show("تم حفظ الخدمة بنجاح.");
            this.Close();
        }
    }
}
