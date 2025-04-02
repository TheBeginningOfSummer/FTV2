using System;
using System.Threading;
using System.Windows.Forms;

namespace FTV2
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.ThreadException += Application_ThreadException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception exception = e.Exception;
            MessageBox.Show($"捕获到的异常：{exception.GetType()}{Environment.NewLine}异常信息：{exception.Message}{Environment.NewLine}异常堆栈：{exception.StackTrace}", "异常",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            //bool isStop = e.IsTerminating;//程序是否崩溃
            if (!(e.ExceptionObject is Exception exception)) return;
            MessageBox.Show($"捕获到的异常：{exception.GetType()}{Environment.NewLine}异常信息：{exception.Message}{Environment.NewLine}异常堆栈：{exception.StackTrace}", "线程异常",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
