using PhysioCenter.Wpf.Infrastructure;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PhysioCenter.Wpf.Views
{
    public partial class InvoicesPage : UserControl
    {
        public InvoicesPage()
        {
            InitializeComponent();
            LoadInvoices();
        }

        private void LoadInvoices()
        {
            using var db = new AppDbContext();

            var data = db.Invoices
                .Select(i => new
                {
                    i.Id,
                    i.Date,
                    PatientName = db.Patients
                        .Where(p => p.Id == i.Items.First().ServiceId)
                        .Select(p => p.FullName)
                        .FirstOrDefault(),
                    i.TotalAmount
                })
                .OrderByDescending(i => i.Id)
                .ToList();

            InvoicesGrid.ItemsSource = data;
        }

        private void AddInvoice_Click(object sender, RoutedEventArgs e)
        {
            var win = new InvoiceForm();
            win.ShowDialog();
            LoadInvoices();
        }
    }
}
