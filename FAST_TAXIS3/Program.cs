using System;
using System.Windows.Forms;
using FAST_TAXIS3.Forms;

namespace FAST_TAXIS3
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // ✅ 1. شاشة البداية
            SplashScreenForm splash = new SplashScreenForm();
            splash.ShowDialog();

            // ✅ 2. شاشة تسجيل الدخول
            LoginForm login = new LoginForm();
            DialogResult result = login.ShowDialog();

            // ✅ 3. إذا سجل دخول ناجح → شغل MainForm
            if (result == DialogResult.OK)
            {
                Application.Run(new MainForm());
            }
            else
            {
                // ✅ إذا ألغى أو قفل → يخرج
                Application.Exit();
            }
        }
    }
}