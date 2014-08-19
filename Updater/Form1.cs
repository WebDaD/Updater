using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebDaD.Toolkit.Update;

namespace Updater
{
    public partial class Form1 : Form
    {
        private bool autostart;
        private Update u;
        private string tempPath;
        private string startExe;

        public Form1(bool autostart, Update u, string tempPath, string startExe)
        {
            this.autostart = autostart;
            this.u = u;
            this.tempPath = tempPath;
            this.startExe = startExe;

            InitializeComponent();
            if (!autostart)
            {
                if (MessageBox.Show("Starte Update für " + u.AppName + "?", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    Application.Exit();
                }
            }
            lb_status.Text = "Starte Update";
            pb_status.Minimum = 0;
            pb_status.Maximum = 100;
            pb_status.Value = 0;
            this.u.UpdateAction += u_UpdateAction;
            this.u.PerformUpdate(this.tempPath);
        }

        void u_UpdateAction(object sender, UpdateActionEventArgs e)
        {
            lb_status.Text = e.Message;
            pb_status.Value = e.Percent;
            if (e.Percent == 100)
            {
                MessageBox.Show("Update beendet", "OK", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                ProcessStartInfo startInfo = new ProcessStartInfo();
                startInfo.FileName = startExe;
                Process.Start(startInfo);
                Application.Exit();
            }
        }
    }
}
