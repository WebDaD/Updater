using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebDaD.Toolkit.Helper;
using WebDaD.Toolkit.Update;

namespace Updater
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Args a = new Args(args);
            string temppath=Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + Path.DirectorySeparatorChar + "temp";
            if (!Directory.Exists(temppath)) Directory.CreateDirectory(temppath);
            try
            {
                Update u = new Update(a.Arguments["apppath"], a.Arguments["appname"], Double.Parse(a.Arguments["version"]), a.Database, a.Arguments["updatePath"]);
                
                if (a.Silent)
                {
                    if (u.PerformUpdate(temppath))
                    {
                        ProcessStartInfo startInfo = new ProcessStartInfo();
                        startInfo.FileName = a.Arguments["startExe"];
                        Process.Start(startInfo);
                        Application.Exit();
                    }

                }
                else
                {
                    Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new Form1(a.AutoStart, u, temppath, a.Arguments["startExe"]));
                }
            }
            catch (Exception e)
            {
                Application.EnableVisualStyles();
                    Application.SetCompatibleTextRenderingDefault(false);
                    Application.Run(new ChooseUpdate());
            }
            
        }
    }
}
