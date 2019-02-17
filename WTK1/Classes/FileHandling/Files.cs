using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace WinToolkit.Classes.FileHandling
{
    public static class Files
    {
        public static bool MoveFile(string original, string saveTo, bool replaceFile)
        {
            if (File.Exists(saveTo) && !replaceFile)
                return false;

            try
            {
                if (File.Exists(saveTo))
                {
                    cMain.TakeOwnership(saveTo);
                    cMain.ClearAttributeFile(saveTo);

                    if (File.Exists(saveTo + ".bak"))
                        DeleteFile(saveTo + ".bak");

                    File.Move(saveTo, saveTo + ".bak");
                }

                File.Copy(original, saveTo, replaceFile);

                DeleteFile(saveTo + ".bak");
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Deletes a file.
        /// </summary>
        /// <param name="filePath">The file to delete</param>
        /// <returns>Returns true if deleted.</returns>
        public static bool DeleteFile(string filePath)
        {
            if (File.Exists(filePath))
            {
                try
                {
                    File.Delete(filePath);
                    return true;
                }
                catch
                {
                    try
                    {
                        cMain.TakeOwnership(filePath);
                        cMain.ClearAttributeFile(filePath);
                        File.Delete(filePath);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                }


            }

            return !File.Exists(filePath);
        }


        /// <summary>
        /// Deletes a folder.
        /// </summary>
        /// <param name="folderPath">Path to the folder</param>
        /// <param name="reCreate">Should the folder be re-created after.</param>
        /// <returns>Returns true if folder was deleted.</returns>
        public static bool DeleteFolder(string folderPath, bool reCreate)
        {
            if (folderPath.Length <= 3)
            {
                //Can't delete entire drive.
                return false;
            }

            //Normal Attempt
            if (Directory.Exists(folderPath))
            {
                try
                {
                    Directory.Delete(folderPath, true);
                }
                catch (Exception) { }
            }

            //Attempt 2
            if (Directory.Exists(folderPath))
            {
                try
                {
                    cMain.ClearAttributeFile(folderPath);
                    cMain.TakeOwnership(folderPath);
                    Directory.Delete(folderPath, true);
                }
                catch (Exception ex)
                {
                    return false;
                }
            }

            if (reCreate)
            {
                try
                {
                    cMain.CreateDirectory(folderPath);
                    return true;
                }
                catch
                {
                    return false;
                }
            }

            return true;
        }
    }
}
