using PhysioCenter.Wpf.Domain;
using PhysioCenter.Wpf.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace PhysioCenter.Wpf.Views
{
    public partial class InvoiceForm : Window
    {
        private readonly AppDbContext _db = new AppDbContext();
        private readonly List<InvoiceItem> _items = new List<InvoiceItem>();

        public InvoiceForm()
        {
            InitializeComponent();
            LoadPatients();
            LoadServices();
            RefreshItemsGrid();
        }

        private void LoadPatients()
        {
            PatientBox.ItemsSource = _db.Patients
                .Select(p => new { p.Id, p.FullName })
                .ToList();

            PatientBox.DisplayMemberPath = "FullName";
            PatientBox.SelectedValuePath = "Id";
        }

        private void LoadServices()
        {
            var services = _db.Services
                .Select(s => new { s.Id, s.Name, s.Price })
                .ToList();

            ServiceBox.ItemsSource = services;
            ServiceBox.DisplayMemberPath = "Name";
            ServiceBox.SelectedValuePath = "Id";

            ServiceBox.SelectionChanged += (_, __) =>
            {
                if (ServiceBox.SelectedItem is not null)
                {
                    dynamic selected = ServiceBox.SelectedItem;
                    ServicePriceBox.Text = selected.Price.ToString("0.00");
                }
            };
        }

        private void AddServiceItem_Click(object sender, RoutedEventArgs e)
        {
            if (ServiceBox.SelectedValue is null)
            {
                MessageBox.Show("يرجى اختيار الخدمة.");
                return;
            }

            int serviceId = (int)ServiceBox.SelectedValue;
            var service = _db.Services.First(s => s.Id == serviceId);

            _items.Add(new InvoiceItem
            {
                ServiceId = service.Id,
                Service = service,
                Price = service.Price
            });

            RefreshItemsGrid();
        }

        private void RefreshItemsGrid()
        {
            ItemsGrid.ItemsSource = null;

            ItemsGrid.ItemsSource = _items.Select(i => new
            {
                Name = i.Service.Name,
                Price = i.Price
            }).ToList();

            TotalBox.Text = _items.Sum(i => i.Price).ToString("0.00");
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (PatientBox.SelectedValue is null)
            {
                MessageBox.Show("يرجى اختيار المراجع.");
                return;
            }

            if (!_items.Any())
            {
                MessageBox.Show("لا يوجد عناصر في الفاتورة.");
                return;
            }

            var invoice = new Invoice
            {
                Date = DateTime.Now,
                TotalAmount = _items.Sum(i => i.Price),
                Items = _items
            };

            _db.Invoices.Add(invoice);
            _db.SaveChanges();

            MessageBox.Show("تم حفظ الفاتورة بنجاح!");
            this.Close();
        }
    }
}
