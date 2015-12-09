using Squirrel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

           Task task = Task.Run(async () =>
            {

                try
                {
                    File.AppendAllText("C:\\temp\\log.txt", DateTime.Now.ToShortTimeString() + " Starting update...\r\n");

                    await UpdateManager.GitHubUpdateManager("https://github.com/kenbailey/MyAppGitHub");
                    File.AppendAllText("C:\\temp\\log.txt", DateTime.Now.ToShortTimeString() + " Finished Update\r\n");
                }
                catch (AggregateException aggEx)
                {
                    File.AppendAllText("C:\\temp\\log.txt", DateTime.Now.ToShortTimeString() + " Exception: " + aggEx.Message + "\r\n");
                    foreach (Exception ex in aggEx.Flatten().InnerExceptions)
                   { 
                        File.AppendAllText("C:\\temp\\log.txt", DateTime.Now.ToShortTimeString() + " - " + ex.Message + "\r\n" + ex.StackTrace + "\r\n\r\n");
                   }
                }
               
            });


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
