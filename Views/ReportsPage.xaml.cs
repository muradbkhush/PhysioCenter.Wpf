using PhysioCenter.Wpf.Infrastructure;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace PhysioCenter.Wpf.Views
{
    public partial class ReportsPage : UserControl
    {
        public ReportsPage()
        {
            InitializeComponent();

            // تاريخ اليوم كنطاق افتراضي
            FromDate.SelectedDate = DateTime.Today.AddMonths(-1);
            ToDate.SelectedDate = DateTime.Today;

            LoadReport();
        }

        private void LoadReport_Click(object sender, RoutedEventArgs e)
        {
            LoadReport();
        }

        private void LoadReport()
        {
            using var db = new AppDbContext();

            var from = FromDate.SelectedDate ?? DateTime.Today.AddMonths(-1);
            var to = ToDate.SelectedDate ?? DateTime.Today;

            // الإيرادات — من الفواتير
            var revenue = db.Invoices
                .Where(i => i.Date >= from && i.Date <= to)
                .Sum(i => (decimal?)i.TotalAmount) ?? 0;

            // المصاريف
            var expenses = db.Expenses
                .Where(e => e.Date >= from && e.Date <= to)
                .Sum(e => (decimal?)e.Amount) ?? 0;

            var profit = revenue - expenses;

            RevenueText.Text = $"{revenue:0.00} JD";
            ExpensesText.Text = $"{expenses:0.00} JD";
            ProfitText.Text = $"{profit:0.00} JD";
        }
    }
}
