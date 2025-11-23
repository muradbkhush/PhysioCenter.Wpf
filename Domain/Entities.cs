using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhysioCenter.Wpf.Domain
{
    public class User
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Username { get; set; } = "";

        [MaxLength(250)]
        public string PasswordHash { get; set; } = "";

        [MaxLength(20)]
        public string Role { get; set; } = "User";   // Admin / Therapist / Receptionist
    }

    public class Patient
    {
        public int Id { get; set; }

        [MaxLength(150)]
        public string FullName { get; set; } = "";

        [MaxLength(50)]
        public string Phone { get; set; } = "";

        public DateTime? BirthDate { get; set; }

        public string Notes { get; set; } = "";

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }

    public class Therapist
    {
        public int Id { get; set; }

        [MaxLength(150)]
        public string FullName { get; set; } = "";

        [MaxLength(50)]
        public string Specialty { get; set; } = "";
    }

    public class Appointment
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Scheduled";
        // Scheduled / Completed / Cancelled / NoShow

        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        public int TherapistId { get; set; }
        public Therapist? Therapist { get; set; }
    }

    public class Session
    {
        public int Id { get; set; }

        public DateTime SessionDate { get; set; }

        public string Evaluation { get; set; } = "";
        public string TreatmentPlan { get; set; } = "";
        public int ImprovementLevel { get; set; }  // 0–100%

        public int PatientId { get; set; }
        public Patient? Patient { get; set; }

        public int AppointmentId { get; set; }
        public Appointment? Appointment { get; set; }
    }

    public class Service
    {
        public int Id { get; set; }

        [MaxLength(150)]
        public string Name { get; set; } = "";

        public decimal Price { get; set; }
    }

    public class Invoice
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public decimal TotalAmount { get; set; }

        public ICollection<InvoiceItem> Items { get; set; } = new List<InvoiceItem>();
    }

    public class InvoiceItem
    {
        public int Id { get; set; }

        public int InvoiceId { get; set; }
        public Invoice? Invoice { get; set; }

        public int ServiceId { get; set; }
        public Service? Service { get; set; }

        public decimal Price { get; set; }
    }

    public class Expense
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }
        public string Description { get; set; } = "";
        public decimal Amount { get; set; }
    }

    public class AuditLog
    {
        public int Id { get; set; }

        public DateTime Date { get; set; } = DateTime.Now;
        public string Action { get; set; } = "";
        public string Username { get; set; } = "";
    }

    public class ClinicSettings
    {
        public int Id { get; set; }
        public string ClinicName { get; set; } = "";
        public string Address { get; set; } = "";
        public string Phone { get; set; } = "";
        public string? LogoPath { get; set; }
    }

}
