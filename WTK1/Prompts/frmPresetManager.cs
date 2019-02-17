using System;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WinToolkit.Classes;
using WinToolkit.Classes.FileHandling;
using WinToolkit.Classes.Helpers;

namespace WinToolkit {
	public partial class frmAIOPresetManager : Form {
		public frmAIOPresetManager() {
			InitializeComponent();
		}

		public string sSelectedFile = "";
		public string sSelectedFilter = "";

		//HIDE TREENODE CHILD CHECKBOX

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

		private const int TVIF_STATE = 0x8;
		private const int TVIS_STATEIMAGEMASK = 0xF000;
		private const int TV_FIRST = 0x1100;
		private const int TVM_SETITEM = TV_FIRST + 63;

		#region Nested type: TVITEM

		[StructLayout(LayoutKind.Sequential, Pack = 8, CharSet = CharSet.Auto)]
		private struct TVITEM {
			public int mask;
			public IntPtr hItem;
			public int state;
			public int stateMask;
			[MarshalAs(UnmanagedType.LPTStr)]
			private readonly string lpszText;
			private readonly int cchTextMax;
			private readonly int iImage;
			private readonly int iSelectedImage;
			private readonly int cChildren;
			private readonly IntPtr lParam;
		}

		#endregion
		private void HideCheckBox(TreeView tvw, TreeNode node) {
			try {
				var tvi = new TVITEM();
				tvi.hItem = node.Handle;
				tvi.mask = TVIF_STATE;
				tvi.stateMask = TVIS_STATEIMAGEMASK;
				tvi.state = 0;
				IntPtr lparam = Marshal.AllocHGlobal(Marshal.SizeOf(tvi));
				Marshal.StructureToPtr(tvi, lparam, false);

				SendMessage(tvw.Handle, TVM_SETITEM, IntPtr.Zero, lparam);
			}
			catch {
			}
		}

		private void frmPreset_Load(object sender, EventArgs e) {
			cMain.FormIcon(this);
		    splitContainer5.Scale4K(_4KHelper.Panel.Pan1);
		    splitContainer4.Scale4K(_4KHelper.Panel.Pan2);
            chkPresetMan.Checked = cOptions.PresetManager;

			LoadPresetList();
            cMain.FreeRAM();
		 
        }

	    private void frmPreset_Shown(object sender, EventArgs e)
	    {
	        this.Height -= 1;
	    }

	    private string CheckSelectedPreset(bool multi = false) {
			string Sel = "";
			int C = 0;
			foreach (TreeNode TN in tvPreset.Nodes) {
				if (TN.Checked) {
					Sel = TN.Tag.ToString();
					C += 1;
				}
			}

			if (C == 0) {
				MessageBox.Show("You need to tick 1 preset!", "No Preset Ticked");
				Sel = "";
			}

			if (C > 1 && multi == false) {
				MessageBox.Show("You can only tick 1 preset", C + " Presets Ticked");
				Sel = "";
			}

			cMain.FreeRAM();
			return Sel;
		}

		private void LoadPresetList() {
			tvPreset.Nodes.Clear();
			Application.DoEvents();
			if (Directory.Exists(cMain.Root + "Last Sessions")) {
				foreach (string P in Directory.GetFiles(cMain.Root + "Last Sessions", "*.ini", SearchOption.AllDirectories)) {
					try {
						var TNP = new TreeNode();

						string T = P;
						while (T.ContainsIgnoreCase("\\")) {
							T = T.Substring(1);
						}

						T = T.ReplaceIgnoreCase(".ini", "");
                        
						TNP.Text = T;

						TNP.Tag = P;
						string Ps = "";
						using (var SR = new StreamReader(P)) {
							Ps = SR.ReadToEnd();
						}
						string Cat = "";
						if (!Ps.Substring(0, 4).StartsWithIgnoreCase("*AIO")) { continue; }

						foreach (string S in Ps.Split(Environment.NewLine.ToCharArray())) {
							if (S.StartsWithIgnoreCase("*") && S.ContainsIgnoreCase("|")) {
								TNP.Text = TNP.Text.PadRight(80, ' ');
								string Info = S.Substring(5);
								foreach (string data in Info.Split('|')) {
									TNP.Text += " |   " + data;
								}

							}//&& S.ContainsIgnoreCase(".")) {  }
							try {
								if (!string.IsNullOrEmpty(S) && !S.StartsWithIgnoreCase("*")) {
									if (S.StartsWithIgnoreCase("#")) {
										Cat = S.Substring(1);
										TNP.Nodes.Add(Cat);
									}
									else {
										foreach (TreeNode TN in TNP.Nodes) {
											if (TN.Text == Cat) {
												TN.Nodes.Add(S);

												break;
											}
										}
									}
								}
							}
							catch {
							}
						}
						tvPreset.Nodes.Insert(0, TNP);

						TNP.Expand();
						foreach (TreeNode TP in TNP.Nodes) {
							if (TP.Nodes.Count == 0) {
								TP.Remove();
							}
							else {
								foreach (TreeNode TP2 in TP.Nodes) {
									TP2.Tag = "C";
									HideCheckBox(tvPreset, TP2);
								}
							}
						}
					}
					catch { }
				}
			}

			if (tvPreset.Nodes.Count > 0) {
				cmdLoadPreset.Visible = true;
				cmdExportPreset.Visible = true;
				cmdDeletePreset.Visible = true;
				cmdRenamePreset.Visible = true;
				cmdSkip.Text = "Skip (No Preset)";
			}
			else {
				cmdLoadPreset.Visible = false;
				cmdExportPreset.Visible = false;
				cmdDeletePreset.Visible = false;
				cmdRenamePreset.Visible = false;
				cmdSkip.Text = "Continue (No Preset)";
			}
		}

		private void cmdLoadPreset_Click(object sender, EventArgs e) {
			try {
				sSelectedFile = CheckSelectedPreset();

				if (String.IsNullOrEmpty(sSelectedFile)) {
					return;
				}

				sSelectedFilter = "";
				foreach (TreeNode TP in tvPreset.Nodes) {
					if (TP.Checked) {
						sSelectedFilter = TP.Nodes.Cast<TreeNode>().Where(TC => TC.Checked).Aggregate(sSelectedFilter, (current, TC) => current + ("#" + TC.Text));
						break;
					}
				}

				toolStrip3.Enabled = false;
				chkPresetMan.Enabled = false;

			}
			catch { }

			this.DialogResult = System.Windows.Forms.DialogResult.OK;
		}

		private void cmdSkip_Click(object sender, EventArgs e) {
			DialogResult = System.Windows.Forms.DialogResult.Cancel;
			Close();
		}

		private void cmdRenamePreset_Click(object sender, EventArgs e) {
			try {
				string S = CheckSelectedPreset();
				if (string.IsNullOrEmpty(S)) {
					return;
				}

				string N = cMain.InputBox("Please enter a new name for this preset", "New Name", S);
				S = cMain.Root + "Last Sessions\\" + S + ".ini";

				if (string.IsNullOrEmpty(N) || N == S) {
					return;
				}
				N = cMain.Root + "Last Sessions\\" + N + ".ini";
				if (!File.Exists(N)) {
					File.Move(S, N);
				}
				else {
					MessageBox.Show("This file already exists!", "Aborted");
				}
				LoadPresetList();
			}
			catch (Exception Ex) {
				MessageBox.Show(Ex.Message, "Error");
			}
		}

		private void cmdDeletePreset_Click(object sender, EventArgs e) {
			DialogResult DR = MessageBox.Show("Are you sure you wish to delete the selected file(s)?", "Are you sure?",
														 MessageBoxButtons.YesNo);

			if (DR != DialogResult.Yes) {
				return;
			}

			foreach (TreeNode TN in tvPreset.Nodes) {
				try {
					if (TN.Checked) {
						Files.DeleteFile(TN.Tag.ToString());
					}
				}
				catch { }
			}

			tvPreset.Nodes.Clear();
			LoadPresetList();
		}

		private void cmdImportPreset_Click(object sender, EventArgs e) {
			try {
				var OFD = new OpenFileDialog();
				OFD.Title = "Select Preset";
				OFD.Filter = "Win Toolkit Preset *.ini|*.ini";
				OFD.Multiselect = true;

				if (OFD.ShowDialog() != DialogResult.OK) {
					return;
				}

				if (!Directory.Exists(cMain.Root + "Last Sessions")) {
					cMain.CreateDirectory(cMain.Root + "Last Sessions");
				}

				foreach (string S in OFD.FileNames) {
					try {
						string N = S;
						while (N.ContainsIgnoreCase("\\")) {
							N = N.Substring(1);
						}

						if (File.Exists(cMain.Root + "Last Sessions\\" + N)) {
							MessageBox.Show("This file already exists, try renaming it first", "Already exists");
						}
						else {
							File.Copy(S, cMain.Root + "Last Sessions\\" + N);
						}
					}
					catch (Exception Ex) {
						MessageBox.Show(Ex.Message, "Error");
					}
				}
				LoadPresetList();
			}
			catch {
			}
		}

		private void cmdExportPreset_Click(object sender, EventArgs e) {
			try {
				string S = CheckSelectedPreset();
				var SFD = new SaveFileDialog();
				SFD.Title = "Select Preset";
				SFD.Filter = "Win Toolkit Preset *.ini|*.ini";
				if (String.IsNullOrEmpty(S)) {
					return;
				}

				if (SFD.ShowDialog() != DialogResult.OK) {
					return;
				}

				if (!Directory.Exists(cMain.Root + "Last Sessions")) {
					cMain.CreateDirectory(cMain.Root + "Last Sessions");
				}

				File.Copy(S, SFD.FileName, true);

				LoadPresetList();
			}
			catch { }
		}

		bool lAIO = false;
		private void tvPreset_AfterCheck(object sender, TreeViewEventArgs e) {
			if (lAIO == false) {
				lAIO = true;
				if (e.Node.Parent == null) {
					foreach (TreeNode TN in e.Node.Nodes) {
						TN.Checked = e.Node.Checked;
					}
				}
				else {
					bool C = false;
					foreach (TreeNode TN in e.Node.Parent.Nodes) {
						if (TN.Checked) { C = true; break; }
					}
					e.Node.Parent.Checked = C;
				}
				lAIO = false;
			}
		}

		private void tvPreset_AfterSelect(object sender, TreeViewEventArgs e) {
		}

		private void chkPresetMan_CheckedChanged(object sender, EventArgs e) {
			if (!lAIO) {
				cOptions.PresetManager = chkPresetMan.Checked;
				cOptions.SaveSettings();
			}
		}
	}
}
