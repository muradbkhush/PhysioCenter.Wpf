using System;
using System.Windows;

namespace PhysioCenter.Wpf
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // نخلي التطبيق ما يسكر إلا لما آخر نافذة تتسكر
            this.ShutdownMode = ShutdownMode.OnLastWindowClose;

            try
            {
                var login = new Views.LoginWindow();
                login.Show();
            }
            catch (Exception ex)
            {
                // أهم خطوة الآن: نعرض أي مشكلة تمنع النافذة من الظهور
                MessageBox.Show(
                    "❌ حدث خطأ أثناء تشغيل التطبيق:\n\n" +
                    ex.Message + "\n\n" +
                    ex.StackTrace,
                    "Startup Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                );

                // نغلق التطبيق بعد إظهار الخطأ
                Shutdown();
            }
        }
    }
}
