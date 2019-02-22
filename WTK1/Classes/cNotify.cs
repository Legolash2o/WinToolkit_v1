using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WinToolkit.Classes;

namespace WinToolkit {
    class cNotify {
        public static NotifyIcon Notify;

        public static void ShowNotification(string Title, string Text, ToolTipIcon TTI = ToolTipIcon.Info, string Path = "") {
            //Thread guiThread = new Thread(new ThreadStart((Action)delegate() {
            Notify.BalloonTipTitle = Title;
            Notify.BalloonTipText = Text;
            Notify.BalloonTipIcon = TTI;
            Notify.Tag = Path;
            Notify.ShowBalloonTip(3000);
            //}));
            //guiThread.Start();
        }

        public static void Setup() {
            cNotify.Notify = new NotifyIcon() { Icon = Properties.Resources.W7T_128, Visible = true, BalloonTipIcon = ToolTipIcon.Info };
            cNotify.Notify.BalloonTipClicked += new EventHandler(cNotify.Notify_BalloonTipClicked);
            cNotify.Notify.Text = "Win Toolkit v" + cMain.WinToolkitVersion();
        }
        public static void Notify_BalloonTipClicked(object sender, EventArgs e) {
            string Title = Notify.BalloonTipTitle;

            if (Title.StartsWithIgnoreCase("v") && Title.EndsWithIgnoreCase(" Available")) { Title = "Update"; }

            switch (Title) {
                case "Update":
                    var TS = new frmWTUpdate();
                    TS.StartPosition = FormStartPosition.CenterScreen;
                    TS.ShowDialog();
                    break;
                case "Log Uploaded":
                  case "Log Saved":
                 
                    cMain.OpenLink(Notify.Tag.ToString());
                    break;

            }
        }

      
    }
}
