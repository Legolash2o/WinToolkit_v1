using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Cache;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
using WinToolkit.Classes.FileHandling;

namespace WinToolkit.Classes
{
    //Backported from v2.x
    public static class UpdateCache
    {
        //    static readonly string CACHE_PATH = cMain.Root + "\\updateCache.db";

        //    private static readonly List<UpdateCacheItem> _updateCache = new List<UpdateCacheItem>();

        //    public static UpdateCacheItem Find(string MD5)
        //    {
        //        return _updateCache.FirstOrDefault(u => u.MD5 == MD5);
        //    }


        //    public static UpdateCacheItem Find(string fileName, string size)
        //    {
        //        return _updateCache.FirstOrDefault(u => u.FileName.StartsWithIgnoreCase(fileName.ToUpper()) && u.Size == size);
        //    }

        public static void Update()
        {
            try
            {
                string DBFILE = cMain.Root + "updateCache.xml";
                const string DBURL = "http://update.wintoolkit.co.uk/updateCache.xml";
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(DBURL);
                request.Accept = "text/plain";
                request.Proxy = WebRequest.DefaultWebProxy;
                request.Timeout = 10000;
                request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.NoCacheNoStore);
                request.AuthenticationLevel = System.Net.Security.AuthenticationLevel.None;
                HttpWebResponse gameFileResponse = (HttpWebResponse)request.GetResponse();

                DateTime localFileModifiedTime = DateTime.Now;
                if (File.Exists(DBFILE))
                {
                    localFileModifiedTime = File.GetLastWriteTime(DBFILE);
                }

                DateTime onlineFileModifiedTime = gameFileResponse.LastModified;

                if (!File.Exists(DBFILE) || localFileModifiedTime <= onlineFileModifiedTime)
                {
                    if (File.Exists(DBFILE))
                    {
                        DialogResult DR =
                            MessageBox.Show("The updateCache.db file is out of date. Would you like to update it now?",
                                "UpdateCache", MessageBoxButtons.YesNo);

                        if (DR != DialogResult.Yes)
                        {
                            return;
                        }
                    }
                    try
                    {
                        Files.DeleteFile(DBFILE + ".bak");
                        if (File.Exists(DBFILE))
                        {
                            File.Move(DBFILE, DBFILE + ".bak");
                        }

                        WebClient WC = new WebClient();
                        WC.Proxy = null;
                        WC.DownloadFile(DBURL, DBFILE);

                        Files.DeleteFile(DBFILE + ".bak");


                    }
                    catch (Exception)
                    {
                        if (File.Exists(DBFILE + ".bak"))
                        {
                            File.Move(DBFILE + ".bak", DBFILE);
                        }
                    }


                }
            }
            catch (Exception ex)
            {
                if (ex is WebException)
                {
                    WebException we = (WebException)ex;
                    switch (we.Status)
                    {
                        case WebExceptionStatus.NameResolutionFailure:
                        case WebExceptionStatus.Timeout:
                            new LargeError("Update Cache Error", "Could not connect to server.", ex).ShowDialog();
                            return;
                    }
                }

                new SmallError("Cache Update Check", ex).Upload();
            }

        }



        //    public static void Load()
        //    {
        //        if (File.Exists(CACHE_PATH))
        //        {
        //            _updateCache.Clear();
        //            string cacheFile = "";
        //            using (var SR = new StreamReader(CACHE_PATH))
        //            {
        //                cacheFile = SR.ReadToEnd();
        //            }

        //            int errorCount = 0;
        //            string error = "";
        //            foreach (string line in cacheFile.Split(Environment.NewLine.ToCharArray()))
        //            {
        //                if (string.IsNullOrEmpty(line)) { continue; }
        //                try
        //                {
        //                    var newCache = new UpdateCacheItem
        //                    {
        //                        FileName = line.Split('|')[0],
        //                        Size = line.Split('|')[1],
        //                        PackageName = line.Split('|')[2],
        //                        PackageVersion = line.Split('|')[3],
        //                        Type = (UpdateType)Enum.Parse(typeof(UpdateType), line.Split('|')[4]),
        //                        Architecture = line.Split('|')[5],

        //                        MD5 = line.Split('|')[6],
        //                        PackageDescription = line.Split('|')[7],
        //                        Language = line.Split('|')[8],
        //                        Support = line.Split('|')[10],
        //                        CreatedDate = cMain.ParseDate(line.Split('|')[11], "DD/MM/YYYY")
        //                    };
        //                    _updateCache.Add(newCache);
        //                }
        //                catch (Exception ex)
        //                {

        //                    if (errorCount++ < 10)
        //                    {
        //                        error += string.Format(ex.Message + "\r\n" + line) + "\r\n";
        //                    }
        //                }

        //                if (!string.IsNullOrEmpty(error))
        //                {
        //                    new SmallError("Update Cache item error", null, "Items: " + errorCount + "\r\n" + error).Upload();
        //                }

        //            }
        //        }

        //    }

        public class UpdateCacheItem
        {
            public string MD5;
            public long Size;
            public string FileName;
            public string PackageName;
            public string PackageVersion;
            public string PackageDescription;
            public string Language;
            public string Support;
            public DateTime CreatedDate;
            public string Architecture;
            public bool AllowedOffline;
            public decimal AppliesTo;
            public UpdateType Type;

            public XElement XML
            {
                get
                {
                    var xUpdate = new XElement("Update");
                    xUpdate.SetAttributeValue("Name", PackageName);
                    xUpdate.SetAttributeValue("Size", Size);


                    xUpdate.SetAttributeValue("Version", PackageVersion);
                    xUpdate.SetAttributeValue("Filename", FileName);
                    xUpdate.SetAttributeValue("Arc", Architecture.ToString());
                    xUpdate.SetAttributeValue("MD5", MD5);
                    xUpdate.SetAttributeValue("Desc", PackageDescription);

                    xUpdate.SetAttributeValue("AppliesTo", AppliesTo);
                    xUpdate.SetAttributeValue("Support", Support);
                    xUpdate.SetAttributeValue("CreatedDate", CreatedDate);

                    if (AllowedOffline)
                    {
                        xUpdate.SetAttributeValue("Offline", AllowedOffline);
                    }
                    if (Type == UpdateType.LDR)
                    {
                        xUpdate.SetAttributeValue("Type", Type);
                    }
                    if (!Language.EqualsIgnoreCase("NEUTRAL"))
                    {
                        xUpdate.SetAttributeValue("Lang", Language);
                    }

                    return xUpdate;
                }
            }

        }

        public enum UpdateType
        {
            /// <summary>
            /// Normal Windows update.
            /// </summary>
            GDR,
            /// <summary>
            /// LDR Windows update.
            /// </summary>
            LDR,
            /// <summary>
            /// Not know whether or not it is 
            /// LDR or GDR yet.
            /// </summary>
            Unknown
        }


        public static readonly string CACHE_PATH_XML = cMain.Root + "\\updateCache.xml";
        public static readonly string CACHE_ERROR_XML = cMain.Root + "\\updateCacheErrors.xml";
        private static readonly XElement xError = new XElement("UpdatesError");
        private static readonly XElement xUpdates = new XElement("Updates");
        private static readonly List<UpdateCacheItem> _updateCache = new List<UpdateCacheItem>();

        public static int Count
        {
            get { return _updateCache.Count; }
        }

        public static int LDRCount
        {
            get { return _updateCache.Count(u => u.Type == UpdateType.LDR); }
        }

        public static int GDRCount
        {
            get { return _updateCache.Count(u => u.Type == UpdateType.GDR); }
        }

        public static int UnknownCount
        {
            get { return _updateCache.Count(u => u.Type == UpdateType.Unknown); }
        }

        public static UpdateCacheItem Find(string MD5)
        {
            return _updateCache.FirstOrDefault(u => u.MD5.EqualsIgnoreCase(MD5));
        }

        public static UpdateCacheItem Find(string fileName, string size)
        {
            if (fileName == null)
            {
                return null;
            }
            long _size = 0;

            long.TryParse(size, out _size);

            return Find(fileName, _size); 
        }

        public static UpdateCacheItem Find(string fileName, long size)
        {
            if (fileName == null)
            {
                return null;
            }
            return _updateCache.FirstOrDefault(u => u.FileName.StartsWithIgnoreCase(fileName) && u.Size == size);
        }

      

        public static void Load()
        {
            if (File.Exists(CACHE_PATH_XML))
            {
                var xDoc = new XmlDocument();
                xDoc.Load(CACHE_PATH_XML);

                var xParent = (XmlElement)xDoc.LastChild;

                foreach (XmlElement E in xParent.ChildNodes)
                {
                    var newCache = new UpdateCacheItem
                    {
                        FileName = E.Attributes["Filename"].InnerText,
                        Size = long.Parse(E.Attributes["Size"].InnerText),
                        PackageName = E.Attributes["Name"].InnerText,
                        PackageVersion = E.Attributes["Version"].InnerText,
                        Architecture = E.Attributes["Arc"].InnerText,
                        MD5 = E.Attributes["MD5"].InnerText,
                        PackageDescription = E.Attributes["Desc"].InnerText,
                      
                       
                        Support = E.Attributes["Support"].InnerText,
                        CreatedDate = DateTime.Parse(E.Attributes["CreatedDate"].InnerText)
                    };

                    decimal.TryParse(E.Attributes["AppliesTo"].InnerText, out newCache.AppliesTo);

                    if (E.HasAttribute("Offline"))
                    {
                        newCache.AllowedOffline = bool.Parse(E.Attributes["Offline"].InnerText);
                    }

                    if (E.HasAttribute("Type"))
                    {
                        newCache.Type = (UpdateType)Enum.Parse(typeof(UpdateType), E.Attributes["Type"].InnerText);
                    }
                    if (E.HasAttribute("Lang"))
                    {
                        newCache.Language = E.Attributes["Lang"].InnerText;
                    }

                    _updateCache.Add(newCache);
                }
            }
        }

        //public static void Save()
        //{
        //    var stopwatch = new Stopwatch();
        //    if (Debugger.IsAttached)
        //        stopwatch.Start();

        //    Console.ForegroundColor = ConsoleColor.DarkYellow;
        //    Console.WriteLine("Saving...");
        //    Console.ResetColor();

        //    xUpdates.SetAttributeValue("Count", xUpdates.Nodes().Count());
        //    xUpdates.SetAttributeValue("GDR", GDRCount);
        //    xUpdates.SetAttributeValue("LDR", LDRCount);
        //    xUpdates.SetAttributeValue("Date", DateTime.UtcNow.ToString("dd MMMM yyyy H:mm"));
        //    xError.SetAttributeValue("Count", xError.Nodes().Count());

        //    if (xUpdates.HasElements)
        //    {
        //        xUpdates.Save(CACHE_PATH_XML);
        //    }

        //    if (xError.HasElements)
        //    {
        //        xError.Save(CACHE_ERROR_XML);
        //    }


        //    if (Debugger.IsAttached)
        //    {
        //        stopwatch.Stop();
        //        Console.ForegroundColor = ConsoleColor.Red;
        //        Console.WriteLine(stopwatch.ElapsedMilliseconds);
        //        Console.ResetColor();
        //    }
        //}

     
    }


}