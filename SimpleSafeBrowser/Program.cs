using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.IO;

namespace SimpleSafeBrowser
{
    static class Program
    {

        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Ambiesoft.CppUtils.AmbSetProcessDPIAware();

            int val;
            string inipath = Application.ExecutablePath + ".ini";
            Ambiesoft.Profile.GetInt("Option", "emulation", -1, out val, inipath);
            if(val>0 )
                Ambiesoft.AmbLib.setRegMaxIE(val);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormMain());

            GC.WaitForPendingFinalizers();
            libwaitdown.FormWD.WaitDownloadWindow();
        }

        public static void Reboot()
        {
            System.Diagnostics.Process.Start(Application.ProductName);
        }
    }
}
