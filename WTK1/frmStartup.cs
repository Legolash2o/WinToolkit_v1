using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using WinToolkit.Classes;
using WinToolkit.Classes.Helpers;
using WinToolkit.Properties;
namespace WinToolkit {
	public partial class frmStartup : Form {
		private cMain.UpdateAvailable bUpdateAvailable;
		private string RT = "";

		public frmStartup() {
			InitializeComponent();
			CheckForIllegalCrossThreadCalls = false;


		}

		private void PB() {
			try {
				if (progressBar1.Value + 2 < progressBar1.Maximum) {
					progressBar1.Value = progressBar1.Value + 2;
					progressBar1.Value = progressBar1.Value--;
				}
			}
			catch { }
			Application.DoEvents();
			cMain.FreeRAM();
		}

	

		//
		private void frmStartup_FormClosing(object sender, FormClosingEventArgs e) {
		}

		private void frmStartup_Load(object sender, EventArgs e) {
			label1.Text = "Win Toolkit v" + cMain.WinToolkitVersion();
			cMain.DefaultLang = Thread.CurrentThread.CurrentUICulture.ToString();
            
            //Setup NotifyIcon
            cNotify.Setup();

			//Check if vLite or RT7Lite are running.
			if (cMain.IsApplicationAlreadyRunning("RTWin7Lite") || cMain.IsApplicationAlreadyRunning("vLite")) {
				MessageBox.Show("It is not recommended to run Win Toolkit at the same time as RT7Lite or vLite, this will cause errors when working with images!", "Warning");
			}

			Text += " v" + cMain.WinToolkitVersion() + " - Wincert.net";
			if (cOptions.SkipSettings == false && File.Exists(cMain.Root + "Settings.txt")) {
				cMain.UpdateText(lblStatus, "Checking Win Toolkit Settings...");

				cOptions.GetSettings();
			}
			PB();
		}

		private void frmStartup_Shown(object sender, EventArgs e) {
			cMain.UpdateText(lblStatus, "Checking OS Architecture...");
			Application.DoEvents();
			cMain.Arc64 = Directory.Exists(Environment.GetEnvironmentVariable("SystemRoot") + "\\SysWOW64");
			cMain.FreeRAM();
			PB();

			if (cOptions.SkipOSCheck == false) {
				cMain.UpdateText(lblStatus, "Looking for DISM...");
				Application.DoEvents();
			   
				if (DISM.Load() == 0) {
                                
					var DR = MessageBox.Show("You need to have DISM installed to use Win Toolkit!\n\nDownload now?", "Aborting", MessageBoxButtons.YesNo);
					if (DR == DialogResult.Yes)
					{
						cMain.OpenLink("http://dism.wintoolkit.co.uk/");
					}
					Environment.Exit(0);
				}
			}
			PB();

			BWLoad.RunWorkerAsync();
			if (cOptions.CheckForUpdates && cOptions.SkipUpdate == false) {
				BWUpdate.RunWorkerAsync();
			}
			cMain.FreeRAM();

			cMain.FormIcon(this);


			PB();

			cMain.UpdateText(lblStatus, "Setting Win Toolkit Priority...");
			Application.DoEvents();

			switch (cOptions.WinToolkitPri) {
				case "RealTime":
					Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;
					break;
				case "High":
					Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.High;
					break;
				case "AboveNormal":
					Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.AboveNormal;
					break;
				case "Normal":
					Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.Normal;
					break;
				case "BelowNormal":
					Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.BelowNormal;
					break;
				case "Idle":
					Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.Idle;
					break;
			}
			PB();

		}

		private void BWLoad_DoWork(object sender, DoWorkEventArgs e) {
			cMain.FreeRAM();
			try {
				cMain.UpdateText(lblStatus, "Checking 'NTFSDisableCompression'...");
				Application.DoEvents();
				if (string.IsNullOrEmpty(cReg.GetValue(Registry.LocalMachine, "SYSTEM\\CurrentControlSet\\Control\\FileSystem", "NtfsDisableCompression"))) {
					cReg.WriteValue(Registry.LocalMachine, "SYSTEM\\CurrentControlSet\\Control\\FileSystem", "NtfsDisableCompression", 0, RegistryValueKind.DWord);
				}

				if (cReg.GetValue(Registry.LocalMachine, "SYSTEM\\CurrentControlSet\\Control\\FileSystem", "NtfsDisableCompression") != "0") {
					cReg.WriteValue(Registry.LocalMachine, "SYSTEM\\CurrentControlSet\\Control\\FileSystem", "NtfsDisableCompression", 0, RegistryValueKind.DWord);
					RT += "[NTFSDisableCompression]";
				}
			}
			catch (Exception Ex) { }

			PB();
			cMain.FreeRAM();
			try {
				cMain.UpdateText(lblStatus, "Checking for WimFltr..");
				Application.DoEvents();

				if (cOptions.SkipWimFltr == false && (cReg.RegCheckWIMFltr() || File.Exists(cMain.SysFolder + "\\driver\\wimfltr.sys"))) {
					cMain.UpdateText(lblStatus, "Removing WimFltr..");
					Application.DoEvents();
					cMain.WriteResource(Resources.wimfltr, cMain.UserTempPath + "\\wimfltr.inf", this);
					cMain.OpenProgram("\"" + cMain.SysFolder + "\\rundll32.exe\"", "setupapi,InstallHinfSection DefaultUnInstall 132 " + cMain.UserTempPath + "\\wimfltr.inf", true, ProcessWindowStyle.Hidden);
					if (cOptions.SkipWimFltrRestart == false) { RT += "[WimFltr Removed]"; }
				}

			}
			catch (Exception Ex) { }
			PB();

			cMain.UpdateText(lblStatus, "Checking WinToolkitTemp Folder...");
			Application.DoEvents();
			string WTD = Environment.GetEnvironmentVariable("SystemDrive");

			try {
				if (string.IsNullOrEmpty(cOptions.WinToolkitTemp) || string.IsNullOrEmpty(cOptions.MountTemp)) {
					var mDI = new DriveInfo(cMain.SysDrive);
					if (mDI.IsReady && mDI.AvailableFreeSpace > 26843545600 && mDI.DriveFormat.EqualsIgnoreCase("NTFS")) {
						WTD = mDI.Name.ReplaceIgnoreCase( "\\", "");
					}
					else {
						cMain.UpdateText(lblStatus, "Detecting Largest Drive...");
						WTD = cMain.DetectLargestDrive(mDI.Name, 26843545600);
					}

					if (!WTD.EndsWithIgnoreCase("\\")) { WTD += "\\"; }

					if (string.IsNullOrEmpty(cOptions.WinToolkitTemp)) { cOptions.WinToolkitTemp = WTD + "WinToolkit_Temp"; Prompts.frmAntiVirus.TempChange = true; }
					if (string.IsNullOrEmpty(cOptions.MountTemp)) { cOptions.MountTemp = WTD + "WinToolkit_Mount"; Prompts.frmAntiVirus.TempChange = true; }
				}

				if (cOptions.WinToolkitTemp.EndsWithIgnoreCase("\\")) {
					cOptions.WinToolkitTemp = cOptions.WinToolkitTemp.Substring(0, cOptions.WinToolkitTemp.Length - 1);
				}

				if (cOptions.WinToolkitTemp.StartsWithIgnoreCase(cOptions.MountTemp)) {
					cOptions.MountTemp = WTD + "WinToolkit_Mount";
					cOptions.WinToolkitTemp = WTD + "WinToolkit_Temp";
				}
			}
			catch (Exception Ex) {
				cOptions.MountTemp = WTD + "WinToolkit_Mount";
				cOptions.WinToolkitTemp = WTD + "WinToolkit_Temp";
			}

			if (cOptions.MountTemp.Substring(1, 1).EqualsIgnoreCase("\\")) {
				cOptions.MountTemp = cOptions.MountTemp.Substring(0, 1) + ":" + cOptions.MountTemp.Substring(2);
			}

            if (cOptions.WinToolkitTemp.Substring(1, 1).EqualsIgnoreCase("\\"))
            {
				cOptions.WinToolkitTemp = cOptions.WinToolkitTemp.Substring(0, 1) + ":" + cOptions.WinToolkitTemp.Substring(2);
			}

			while (cOptions.WinToolkitTemp.ContainsIgnoreCase("\\\\")) { cOptions.WinToolkitTemp = cOptions.WinToolkitTemp.ReplaceIgnoreCase("\\\\", "\\"); }
			while (cOptions.MountTemp.ContainsIgnoreCase("\\\\")) { cOptions.MountTemp = cOptions.MountTemp.ReplaceIgnoreCase("\\\\", "\\"); }
			while (cOptions.SolDownload.ContainsIgnoreCase("\\\\")) { cOptions.SolDownload = cOptions.SolDownload.ReplaceIgnoreCase("\\\\", "\\"); }

			PB();
			cMain.UpdateText(lblStatus, "Setting PreventSleep...");
			Application.DoEvents();

			cMain.PreventSleep(cOptions.PreventSleep);

			PB();

			if (!File.Exists(cMain.SysFolder + "\\WA.ico")) {
				cMain.UpdateText(lblStatus, "Setting *.WA files...");
				Application.DoEvents();
				cMain.WriteResource(Resources.WA, cMain.SysFolder + "\\WA.ico", this);
				if (File.Exists(cMain.SysFolder + "\\WA.ico")) {
					cReg.WriteValue(Registry.ClassesRoot, ".wa\\DefaultIcon", "", cMain.SysFolder + "\\WA.ico,0");
				}
			}

			PB();
            cMain.UpdateText(lblStatus, "Loading Update Cache...");
		    UpdateCache.Load();

            PB();
			cMain.FreeRAM();
		}

	

		private void BWLoad_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			progressBar1.Value = progressBar1.Maximum;
			if (!string.IsNullOrEmpty(RT)) {
				cMain.UpdateText(lblStatus, "Waiting for user input...");
				Application.DoEvents();
				DialogResult SR = MessageBox.Show("The following had to be changed in order for Win Toolkit to work properly:" + Environment.NewLine + Environment.NewLine + RT + Environment.NewLine + Environment.NewLine + "Would you like to reboot now?", "Restart Required!", MessageBoxButtons.YesNo);
				if (SR == DialogResult.Yes) {
					cMain.OpenProgram("\"" + cMain.SysFolder + "\\shutdown.exe\"", "-r -t 00", true, ProcessWindowStyle.Hidden);
				}
				Environment.Exit(0);
			}

			cMain.UpdateText(lblStatus, "Loading Tools Manager...");
			Application.DoEvents();
			cMain.FreeRAM();

			try {
				pictureBox1.Image = null;
				pictureBox1.Dispose();
				lblStatus.Dispose();
				label1.Dispose();
				label2.Dispose();
				progressBar1.Dispose();
			}
			catch { }

			 cMain.WriteResource(Resources.imagex, cMain.UserTempPath + "\\Files\\Imagex.exe", this);

			Opacity = 0;
			FormBorderStyle = FormBorderStyle.SizableToolWindow;
			Visible = false;
			Hide();
			new frmToolsManager().Show();
			Enabled = false;

			cMain.FreeRAM();
		}


		private void BWUpdate_DoWork(object sender, DoWorkEventArgs e) {
			bUpdateAvailable = cMain.CheckForUpdates();
		}

		private void BWUpdate_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			if (bUpdateAvailable == cMain.UpdateAvailable.Yes) {
				cNotify.ShowNotification("v" + cOptions.NV + " Available", "Click this balloon to download");
			}
		}
	}
}