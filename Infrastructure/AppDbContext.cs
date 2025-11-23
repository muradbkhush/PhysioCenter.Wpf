using Microsoft.EntityFrameworkCore;
using PhysioCenter.Wpf.Domain;

namespace PhysioCenter.Wpf.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Therapist> Therapists { get; set; }
        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Session> Sessions { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceItem> InvoiceItems { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }
        public DbSet<ClinicSettings> ClinicSettings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=physio.db");
        }
    }
}
