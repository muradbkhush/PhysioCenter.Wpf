using PhysioCenter.Wpf.Domain;
using System;

namespace PhysioCenter.Wpf.Infrastructure
{
    public static class AuditHelper
    {
        public static void Log(string username, string action)
        {
            try
            {
                using var db = new AppDbContext();
                db.AuditLogs.Add(new AuditLog
                {
                    Username = username,
                    Action = action,
                    Date = DateTime.Now
                });
                db.SaveChanges();
            }
            catch
            {
                // نتجاهل أي خطأ هنا عشان ما يطيّح البرنامج بسجل العمليات
            }
        }
    }
}
