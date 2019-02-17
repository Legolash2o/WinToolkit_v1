using System;
using System.Globalization;
using System.IO;
using System.Windows.Forms;
using Microsoft.Win32;
using WinToolkit.Classes;
using WinToolkit.Classes.FileHandling;

namespace WinToolkit.Integration
{
    static class SilentInstaller
    {

        public static void Integrate(ListViewItem silentItem, ToolStripLabel lblStatus, ListView lvList, string dvd)
        {
            string IF = silentItem.SubItems[4].Text;
            string sName = silentItem.Text;

            if (string.IsNullOrEmpty(sName))
            {
                sName = Path.GetFileNameWithoutExtension(IF);
            }

            cMain.UpdateToolStripLabel(lblStatus,
                "Installing Silent #1: " + (silentItem.Index + 1) + " of " + lvList.Items.Count + " (" + sName + ")");
            if (File.Exists(IF))
            {

                string CopyTo = dvd + "WinToolkit_Apps\\" + sName.ReplaceIgnoreCase(" ", "_");
                string DEST = Path.GetFileName(IF);

                if (IF.ContainsForeignCharacters())
                {
                    CopyTo = dvd + "WinToolkit_Apps\\" + silentItem.Index.ToString(CultureInfo.InvariantCulture);
                    DEST = silentItem.Index.ToString(CultureInfo.InvariantCulture) + Path.GetExtension(IF);
                }

                if ((CopyTo.Length + 1) + DEST.Length >= 260)
                {
                    DEST = silentItem.Index.ToString(CultureInfo.InvariantCulture) + Path.GetExtension(IF);
                    if ((CopyTo.Length + 1) + DEST.Length >= 260)
                    {
                        CopyTo = dvd + "WinToolkit_Apps\\" + silentItem.Index.ToString(CultureInfo.InvariantCulture);
                    }
                }

                cMain.UpdateToolStripLabel(lblStatus,
                    "Installing Silent #2: " + (silentItem.Index + 1) + " of " + lvList.Items.Count + " (" + sName + ")");
                if (!Directory.Exists(CopyTo))
                {
                    cMain.CreateDirectory(CopyTo);
                }

                cMain.UpdateToolStripLabel(lblStatus,
                    "Installing Silent #3: " + (silentItem.Index + 1) + " of " + lvList.Items.Count + " (" + sName + ")");

                if (silentItem.SubItems[2].Text.EqualsIgnoreCase("YES"))
                {
                    string dirToCopy = Path.GetDirectoryName(IF);
                    //while (!dirToCopy.EndsWithIgnoreCase("\\"))
                    //{
                    //    dirToCopy = dirToCopy.Substring(0, dirToCopy.Length - 1);
                    //}
                    ////SRC = SRC.Substring(0, SRC.Length - 1);

                    if (Directory.Exists(dirToCopy))
                    {
                        cMain.CopyDirectory(dirToCopy, CopyTo, false, true);
                    }

                    if (File.Exists(CopyTo + "\\" + DEST) && DEST.ContainsIgnoreCase(" "))
                    {
                        string newDest = DEST.ReplaceIgnoreCase(" ", "_");
                        if (!String.Equals(newDest, DEST, StringComparison.OrdinalIgnoreCase))
                        {
                            Files.DeleteFile(CopyTo + "\\" + newDest);
                            File.Move(CopyTo + "\\" + DEST, CopyTo + "\\" + newDest);
                            DEST = newDest;
                        }
                    }
                }
                else
                {
                    string CT = CopyTo + "\\" + DEST;

                    if (!File.Exists(CT))
                    {
                        File.Copy(IF, CT, false);
                    }
                    else
                    {
                        cMain.UpdateToolStripLabel(lblStatus,
                            "Calculating MD5: " + (silentItem.Index + 1) + " of " + lvList.Items.Count + " (" + sName + ")");

                        string mIF = cMain.MD5CalcFile(IF);
                        string mCT = cMain.MD5CalcFile(CT);

                        if (mIF != mCT)
                        {
                            Files.DeleteFile(CT);
                            File.Copy(IF, CT, true);
                        }
                    }
                }

                cMain.UpdateToolStripLabel(lblStatus,
                    "Installing Silent #4: " + (silentItem.Index + 1) + " of " + lvList.Items.Count + " (" + sName + ")");
                Application.DoEvents();

                string Install = CopyTo + "\\" + DEST + "*";

                if (IF.ToUpper().EndsWithIgnoreCase(".MSU"))
                {
                    Install += "/quiet /norestart";
                }
                else if (IF.ToUpper().EndsWithIgnoreCase(".INF"))
                {
                    silentItem.SubItems[1].Text = silentItem.SubItems[1].Text.ReplaceIgnoreCase( "%1", "\"" + CopyTo + "\\" + DEST + "\"");
                    silentItem.SubItems[1].Text = silentItem.SubItems[1].Text.ReplaceIgnoreCase( "\"\"", "\"");
                    Install = "%SystemRoot%\\System32\\rundll32.exe*" + silentItem.SubItems[1].Text;
                }
                else
                {
                    if (!string.IsNullOrEmpty(silentItem.SubItems[1].Text))
                    {
                        Install += silentItem.SubItems[1].Text;
                    }
                }

                cMain.UpdateToolStripLabel(lblStatus,
                    "Installing Silent #5: " + (silentItem.Index + 1) + " of " + lvList.Items.Count + " (" + sName + ")");
                //%DVD%:\\Apps\\"
                while (Install.ContainsIgnoreCase("\\WinToolkit_Apps\\"))
                {
                    Install = Install.Substring(1);
                }
                Install = "%DVD%:\\" + Install;

                cMain.UpdateToolStripLabel(lblStatus,
                    "Installing Silent #6: " + (silentItem.Index + 1) + " of " + lvList.Items.Count + " (" + sName + ")");
                if (silentItem.Group.Header == null)
                {
                    Install = "|" + Install;
                }
                else
                {
                    if (silentItem.Group.Header.EqualsIgnoreCase("Always Installed"))
                    {
                        Install = "|" + Install;
                    }
                }

                cMain.UpdateToolStripLabel(lblStatus,
                    "Installing Silent #7: " + (silentItem.Index + 1) + " of " + lvList.Items.Count + " (" + sName + ")");

                bool bAdd = CheckRegExists(silentItem, Install);

                if (!bAdd)
                {
                    cReg.WriteValue(Registry.LocalMachine, "WIM_Software\\WinToolkit",
                        String.Format("{0:00000}", silentItem.Index) + "|" + sName, Install);
                }
                
            }

        }

        private static bool CheckRegExists(ListViewItem silentItem, string install)
        {
            try
            {
                if (cReg.RegCheckMounted("WIM_Software\\WinToolkit"))
                {
                    foreach (string entry in cReg.GetAllValues(Registry.LocalMachine, "WIM_Software\\WinToolkit"))
                    {
                        if (cReg.GetValue(Registry.LocalMachine, "WIM_Software\\WinToolkit", entry) == install)
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                new SmallError("Error Checking Existing Silents", ex, silentItem).Upload();
            }
            return false;
        }
    }
}
