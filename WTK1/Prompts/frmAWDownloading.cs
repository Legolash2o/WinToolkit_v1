using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;
using WinToolkit.Classes.FileHandling;

namespace WinToolkit.Prompts
{
    public partial class frmAWDownloading : Form
    {
        public bool bRefresh = false;

        public frmAWDownloading()
        {
            InitializeComponent();
        }

        List<UpdateFile> UFList = new List<UpdateFile>();
        private class UpdateFile
        {
            public string Name;
            public string URL;

            public bool Download
            {
                get
                {
                    if (File.Exists(cMain.Root + "UpdateLists\\" + Name + ".wh"))
                    {
                        DateTime d1 = new FileInfo(cMain.Root + "UpdateLists\\" + Name + ".wh").CreationTime;
                        DateTime d2 = DateTime.Now;
                        if ((d2 - d1).TotalHours < 1)
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }
        }



        private void frmUCDownloading_Shown(object sender, EventArgs e)
        {
           

            try
            {
                using (WebClient Client = new WebClient())
                {
                    UpdateFile[] UFDownloader;
                    if (bRefresh)
                    {
                        UFDownloader = UFList.ToArray();
                    }
                    else
                    {
                        UFDownloader = UFList.Where(U => U.Download).ToArray();
                    }
                    progressBar1.Value = 0;
                    progressBar1.Maximum = UFDownloader.Count() * 2;

                    Client.Proxy = null;

                    foreach (UpdateFile UF in UFDownloader)
                    {
                        try
                        {
                            lblStatus.Text = "Downloading: " + UF.Name;
                            Application.DoEvents();

                            string sDownloadPath = cMain.UserTempPath + "\\" + UF.Name + ".7z";
                            Client.Headers.Add("Content-Type", "application/x-7z-compressed");
                            Client.DownloadFile(UF.URL, sDownloadPath);
                            progressBar1.Value++;

                            lblStatus.Text = "Extracting: " + UF.Name;
                            Application.DoEvents();
                            string sDownloadDir = Path.GetDirectoryName(sDownloadPath) + "\\";
                            if (File.Exists(sDownloadPath)) { cMain.ExtractFiles(sDownloadPath, sDownloadDir, this, "*.*", false); }
                            if (File.Exists(sDownloadDir + UF.Name + ".wh"))
                            {
                                Files.DeleteFile(cMain.Root + "UpdateLists\\" + UF.Name + ".wh");
                                File.Move(sDownloadDir + UF.Name + ".wh", cMain.Root + "UpdateLists\\" + UF.Name + ".wh");
                                File.SetCreationTime(cMain.Root + "UpdateLists\\" + UF.Name + ".wh", DateTime.Now);
                            }
                            progressBar1.Value++;
                        }
                        catch (Exception Ex)
                        {
                            if (Ex is WebException)
                            {
                                WebException we = (WebException)Ex;
                                switch (we.Status)
                                {
                                    case WebExceptionStatus.NameResolutionFailure:
                                    case WebExceptionStatus.Timeout:
                                        new LargeError("UC List Download Error", "Could not connect to server.", Ex).ShowDialog();
                                        return;
                                }
                            }
                            new SmallError("UC List Download Error", Ex).Upload();
                        }
                    }
                }
            }
               
              
            catch (Exception Ex)
            {
                new SmallError("UC List Download Error [Main]", Ex).Upload();
            }

            DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void frmAWDownloading_Load(object sender, EventArgs e)
        {
            if (!Directory.Exists(cMain.Root + "UpdateLists\\")) { cMain.CreateDirectory(cMain.Root + "UpdateLists\\"); }
            Application.DoEvents();

            UFList.Clear();
            UpdateFile UF1 = new UpdateFile() { Name = "Windows7-x64", URL = "https://dl.dropboxusercontent.com/s/l8sqopd2kwsob8g/Windows7-x64.7z" };
            UpdateFile UF2 = new UpdateFile() { Name = "Windows7-x86", URL = "https://dl.dropboxusercontent.com/s/qizw50fnkcmvpol/Windows7-x86.7z" };

            UpdateFile UF3 = new UpdateFile() { Name = "Windows8.1-x64", URL = "https://dl.dropboxusercontent.com/s/xbb25ns1y25f03r/Windows8.1-x64.7z" };
            UpdateFile UF4 = new UpdateFile() { Name = "Windows8.1-x86", URL = "https://dl.dropboxusercontent.com/s/jymdkcqszxxz7ms/Windows8.1-x86.7z" };

            UpdateFile UF5 = new UpdateFile() { Name = "Office2013-x64", URL = "https://dl.dropboxusercontent.com/s/yk87gu4on1h1vj5/Office2013-x64.7z" };
            UpdateFile UF6 = new UpdateFile() { Name = "Office2013-x86", URL = "https://dl.dropboxusercontent.com/s/wri6xw9adg80hat/Office2013-x86.7z" };

            UpdateFile UF7 = new UpdateFile() { Name = "Office2010-x64", URL = "https://dl.dropboxusercontent.com/s/xdaps6k0l7f33q2/Office2010-x64.7z" };
            UpdateFile UF8 = new UpdateFile() { Name = "Office2010-x86", URL = "https://dl.dropboxusercontent.com/s/fdha92fk7z8ry35/Office2010-x86.7z" };

            UFList.AddRange(new UpdateFile[] {
				UF1, UF2, UF3, UF4, UF5, UF6, UF7, UF8});
        }


    }
}
