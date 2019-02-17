using System;
using System.Runtime.InteropServices;

namespace WinToolkit
{
    public class WimApi
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct _WIM_INFO
        {
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string WimPath;
            public Guid Guid;
            public Int32 ImageCount;
            public Int32 CompressionType;
            public Int16 PartNumber;
            public Int16 TotalParts;
            public Int32 BootIndex;
            public Int32 WimAttributes;
            public Int32 WimFlagsAndAttr;
        }

        public struct _WIM_MOUNT_LIST
        {
            public string WimPath;
            public string MountPath;
            public Int64 ImageIndex;
            public Int64 MountedForRW;
        }

        public const uint WIM_GENERIC_READ = 0X80000000;
        public const uint WIM_GENERIC_WRITE = 0X40000000;
        public const int WIM_CREATE_NEW = 1;
        public const int WIM_CREATE_ALWAYS = 2;
        public const int WIM_OPEN_EXISTING = 3;
        public const int WIM_OPEN_ALWAYS = 4;
        public const int WIM_COMPRESS_NONE = 0;
        public const int WIM_COMPRESS_XPRESS = 1;
        public const int WIM_COMPRESS_LZX = 2;
        public const int WIM_CREATED_NEW = 0;
        public const int WIM_OPENED_EXISTING = 1;

        public const int WIM_FLAG_RESERVED = 0x00000001;
        public const int WIM_FLAG_VERIFY = 0x00000002;
        public const int WIM_FLAG_INDEX = 0x00000004;
        public const int WIM_FLAG_NO_APPLY = 0x00000008;
        public const int WIM_FLAG_NO_DIRACL = 0x00000010;
        public const int WIM_FLAG_NO_FILEACL = 0x00000020;
        public const int WIM_FLAG_SHARE_WRITE = 0x00000040;
        public const int WIM_FLAG_FILEINFO = 0x00000080;
        public const int WIM_FLAG_NO_RP_FIX = 0x00000100;

        public const int WIM_REFERENCE_APPEND = 0x00010000;
        public const int WIM_REFERENCE_REPLACE = 0x00020000;

        // WIMExportImage

        public const int WIM_EXPORT_ALLOW_DUPLICATES = 0x00000001;
        public const int WIM_EXPORT_ONLY_RESOURCES = 0x00000002;
        public const int WIM_EXPORT_ONLY_METADATA = 0x00000004;

        // WIMRegisterMessageCallback:

        public const uint INVALID_CALLBACK_VALUE = 0xFFFFFFFF;

        public const int WIM_COPY_FILE_RETRY = 0x01000000;
        public const int COPY_FILE_FAIL_IF_EXISTS = 0x00000001;

        public const int WM_APP = 0x8000;

        public const int WIM_MSG = WM_APP + 0x1476;
        public const int WIM_MSG_TEXT = WM_APP + 0x1477;
        public const int WIM_MSG_PROGRESS = WM_APP + 0x1478;
        public const int WIM_MSG_PROCESS = WM_APP + 0x1479;
        public const int WIM_MSG_SCANNING = WM_APP + 0x147A;
        public const int WIM_MSG_SETRANGE = WM_APP + 0x147b;
        public const int WIM_MSG_SETPOS = WM_APP + 0x147c;
        public const int WIM_MSG_STEPIT = WM_APP + 0x147d;
        public const int WIM_MSG_COMPRESS = WM_APP + 0x147e;
        public const int WIM_MSG_ERROR = WM_APP + 0x147f;
        public const int WIM_MSG_ALIGNMENT = WM_APP + 1480;
        public const int WIM_MSG_RETRY = WM_APP + 1481;
        public const int WIM_MSG_SPLIT = WM_APP + 1482;
        public const int WIM_MSG_FILEINFO = WM_APP + 1483;
        public const int WIM_MSG_INFO = WM_APP + 1483;
        public const int WIM_MSG_WARNING = WM_APP + 1484;
        public const int WIM_MSG_CHK_PROCESS = WM_APP + 1485;

        public const int WIM_MSG_SUCCESS = 0x0;
        public const uint WIM_MSG_DONE = 0xFFFFFFF0;
        public const uint WIM_MSG_SKIP_ERROR = 0xFFFFFFFE;
        public const uint WIM_MSG_ABORT_IMAGE = 0xFFFFFFFF;

        public const int WIM_ATTRIBUTE_NORMAL = 0x00000000;
        public const int WIM_ATTRIBUTE_RESOURCE_ONLY = 0x00000001;
        public const int WIM_ATTRIBUTE_METADATA_ONLY = 0x00000002;
        public const int WIM_ATTRIBUTE_VERIFY_DATA = 0x00000004;
        public const int WIM_ATTRIBUTE_RP_FIX = 0x00000008;
        public const int WIM_ATTRIBUTE_SPANNED = 0x00000010;
        public const int WIM_ATTRIBUTE_READONLY = 0x00000020;

        public const string WIM_XML_NODE_NAME = "NAME";
        public const string WIM_XML_NODE_WIM = "WIM";
        public const string WIM_XML_NODE_WINDOWS = "WINDOWS";
        public const string WIM_XML_NODE_VERSION = "VERSION";
        public const string WIM_XML_NODE_MAJOR = "MAJOR";
        public const string WIM_XML_NODE_MINOR = "MINOR";
        public const string WIM_XML_NODE_BUILD = "BUILD";
        public const string WIM_XML_NODE_SPBUILD = "SPBUILD";

        // WIMCreateFile  - Makes a new image file or opens an existing image file.

        [DllImport("wimgapi.dll", EntryPoint = "WIMCreateFile", SetLastError = true)]
        public static extern IntPtr WIMCreateFile(
        [MarshalAs(UnmanagedType.LPWStr)] string lpszWimPath,
        UInt32 dwDesiredAccess,
        Int32 dwCreationDisposition,
        Int32 dwFlagsAndAttributes,
        Int32 dwCompressionType,
            Int32 lpdwCreationResult
        );

        // WIMApplyImage  - Applies an image to a directory path from a Windows image (.wim) file.

        [DllImport("wimgapi.dll", EntryPoint = "WIMApplyImage", SetLastError = true)]
        public static extern int WIMApplyImage(
        IntPtr hImage,
        [MarshalAs(UnmanagedType.LPWStr)]  string lpszPath,
        Int32 dwApplyFlags
        );

        // WIMCaptureImage - Captures an image from a directory path and stores it in an image file.

        [DllImport("wimgapi.dll", EntryPoint = "WIMCaptureImage", SetLastError = true)]
        public static extern IntPtr WIMCaptureImage(
        IntPtr hWim,
        [MarshalAs(UnmanagedType.LPWStr)] string lpszPath,
        Int32 dwApplyFlags
        );

        // WIMCloseHandle - Closes an open Windows Imaging (.wim) file or image handle.

        [DllImport("wimgapi.dll", EntryPoint = "WIMCloseHandle", SetLastError = true)]
        public static extern int WIMCloseHandle(
        IntPtr hObject
        );

        // WIMDeleteImage - Removes an image from within a .wim (Windows image) file 
        //                  so it cannot be accessed. However, the file resources 
        //                  are still available for use by the WIMSetReferenceFile function.

        [DllImport("Wimgapi.dll",
                       ExactSpelling = true,
                       EntryPoint = "WIMDeleteImage",
                       CallingConvention = CallingConvention.StdCall,
                       SetLastError = true)]
        public static extern
        bool
        WIMDeleteImage(
            IntPtr hWim,
            Int32 index
        );

        // WIMExportImage - Transfers the data of an image from one Windows image (.wim) 
        //                  file to another.

        [DllImport("wimgapi.dll", EntryPoint = "WIMExportImage", SetLastError = true)]
        public static extern int WIMExportImage(
            IntPtr hImage,
            IntPtr hWim,
            Int32 dwApplyFlags
        );

        // WIMGetImageCount - Returns the number of volume images stored in a Windows image (.wim) file.

        [DllImport("wimgapi.dll", EntryPoint = "WIMGetImageCount", SetLastError = true)]
        public static extern Int32 WIMGetImageCount(
        IntPtr hwim
        );

        // WIMGetMessageCallbackCount - Returns the count of callback routines currently registered by the imaging library.

        [DllImport("wimgapi.dll", EntryPoint = "WIMGetMessagecallbackCount", SetLastError = true)]
        public static extern Int32 WIMGetMessagecallbackCount(
        IntPtr hwim
        );

        // WIMLoadImage - Loads a volume image from a Windows image (.wim) file.

        [DllImport("wimgapi.dll", EntryPoint = "WIMLoadImage", SetLastError = true)]
        public static extern IntPtr WIMLoadImage(
        IntPtr hwim,
        Int32 dwImageIndex
        );

        // WIMSetBootImage - Marks the image with the given image index as bootable.

        [DllImport("wimgapi.dll", EntryPoint = "WIMSetBootImage", SetLastError = true)]
        public static extern int WIMSetBootImage(
        IntPtr hwim,
        Int32 dwImageIndex
        );

        // WIMSetReferenceFile - Enables the WIMApplyImage and WIMCaptureImage 
        //                       functions to use alternate .wim files for file 
        //                       resources. This can enable optimization of storage
        //                       when multiple images are captured with similar data.

        [DllImport("wimgapi.dll", EntryPoint = "WIMSetReferenceFile", SetLastError = true)]
        public static extern int WIMSetReferenceFile(
        IntPtr hwim,
        [MarshalAs(UnmanagedType.LPWStr)]  string lpszPath,
        Int32 dwFlags
        );

        // WIMSplitFile - Enables a large Windows image (.wim) file to be split
        //                into smaller parts for replication or storage on smaller forms of media.

        [DllImport("wimgapi.dll", EntryPoint = "WIMSplitFile", SetLastError = true)]
        public static extern int WIMSplitFile(
            IntPtr hwim,
            [MarshalAs(UnmanagedType.LPWStr)]  string lpszPartPath,
            out Int64 pliPartSize,
            Int32 dwFlags
            );

        // WIMMountImage - Mounts an image in a Windows image (.wim) file to the specified directory.
              
        [DllImport("wimgapi.dll", EntryPoint = "WIMMountImage", SetLastError = true)]
        public static extern int WIMMountImage(
            [MarshalAs(UnmanagedType.LPWStr)] string lpszMountPath,
            [MarshalAs(UnmanagedType.LPWStr)]  string lpszWimFileName,
            Int32 dwImageIndex,
            [MarshalAs(UnmanagedType.LPWStr)]  string lpszTempPath
            );

        // WIMUnmountImage - Unmounts a mounted image in a Windows image (.wim) 
        //                   file from the specified directory.

        [DllImport("wimgapi.dll", EntryPoint = "WIMUnmountImage", SetLastError = true)]
        public static extern int WIMUnmountImage(
        [MarshalAs(UnmanagedType.LPWStr)]  string lpszMountPath,
        [MarshalAs(UnmanagedType.LPWStr)]  string lpszWimFileName,
        Int32 dwImageIndex,
        int bCommitChanges
        );

        // WIMSetTemporaryPath - Sets the location where temporary imaging files 
        //                       are to be stored.

        [DllImport("Wimgapi.dll",
                       ExactSpelling = true,
                       EntryPoint = "WIMSetTemporaryPath",
                       CallingConvention = CallingConvention.StdCall,
                       SetLastError = true)]
        public static extern
        bool
        WimSetTemporaryPath(
            IntPtr Handle,
            [MarshalAs(UnmanagedType.LPWStr)] string TemporaryPath
        );

        //WIMMountIMageHandle

        [DllImport("Wimgapi.dll",
                       ExactSpelling = true,
                       EntryPoint = "WIMMountImageHandle",
                       CallingConvention = CallingConvention.StdCall,
                       SetLastError = true)]
        public static extern
        bool
        WimMountImageHandle(
            IntPtr Handle,
            [MarshalAs(UnmanagedType.LPWStr)] string MountPath,
            Int32 MountFlags
        );

        // WIMCopyFile - Copies an existing file to a new file. Notifies the 
        //               application of its progress through a callback function. 
        //               If the source file has verification data, the contents of 
        //               the file are verified during the copy operation.

        [DllImport("wimgapi.dll", EntryPoint = "WIMCopyFile", SetLastError = true)]
        public static extern int WIMCopyFile(
        [MarshalAs(UnmanagedType.LPWStr)] string lpszExistingFileName,
        [MarshalAs(UnmanagedType.LPWStr)]  string lpszNewFileName,
        CopyProgressRoutine lpProgressRoutine,
        IntPtr lpvData,
        out Int32 pbCancel,
        Int32 dwCopyFlags
        );

        // WIMRegisterMessageCallback - Registers a function to be called with imaging-specific data. 
        //                              For information about the messages that can be handled, 
        //                              see Messages

        [DllImport("wimgapi.dll", EntryPoint = "WIMRegisterMessageCallback", SetLastError = true)]
        public static extern Int32 WIMRegisterMessageCallback(
        IntPtr hwim,
        WIMMessageCallback fpMessageProc,
        IntPtr lpvUserData
        );

        // WIMUnregisterMessageCallback - Unregister a function from being called with imaging-specific data.

        [DllImport("wimgapi.dll", EntryPoint = "WIMUnregisterMessageCallback", SetLastError = true)]
        public static extern long WIMUnregisterMessageCallback(
        IntPtr hwim,
        WIMMessageCallback fpMessageProc
        );

        //WIMGetAttributes - Returns the information about the .wim file.

        [DllImport("wimgapi.dll", EntryPoint = "WIMGetAttributes", SetLastError = true)]
        public static extern int WIMGetAttributes(
            IntPtr hWim,
            out _WIM_INFO lpWimInfo,
            Int32 cbWimInfo
        );

        // WIMGetImageInformation - Returns information about an image within the .wim (Windows image) file.

        [DllImport("Wimgapi.dll",
                       ExactSpelling = true,
                       EntryPoint = "WIMGetImageInformation",
                       CallingConvention = CallingConvention.StdCall,
                       SetLastError = true)]
        public static extern bool WimGetImageInformation(
            IntPtr Handle,
            out IntPtr ImageInfo,
            out IntPtr SizeOfImageInfo
        );

        [DllImport("kernel32.dll", EntryPoint = "LocalFree", SetLastError = true)]
        public static extern long LocalFree(
        IntPtr hMem
        );

        // WIMSetImageInformation - Stores information about an image in the Windows image (.wim) file.

        [DllImport("Wimgapi.dll",
                       ExactSpelling = true,
                       EntryPoint = "WIMSetImageInformation",
                       CallingConvention = CallingConvention.StdCall,
                       SetLastError = true)]
        public static extern
        bool
        WimSetImageInformation(
            IntPtr Handle,
            IntPtr ImageInfo,
            uint SizeOfImageInfo
        );

        public delegate long CopyProgressRoutine(
            long TotalFileSize,
            long TotalBytesTransferred,
            long StreamSize,
            long StreamBytesTransferred,
            Int32 dwStreamNumber,
            Int32 dwCallbackReason,
            int hSourceFile,
            int hDestinationFile,
            int lpData);

        // WIMMessageCallback - An application-defined function used with the 
        //                      WIMRegisterMessageCallback or WIMUnregisterMessageCallback functions.

        public delegate Int32 WIMMessageCallback(
            Int32 dwMessageId,
            Int32 wParam,
            Int32 lParam,
            Int32 lpvUserData
            );

        // Get the Name for the given Image in the WIM file 
        // if image not found, returns blank string 

    }
    public class WindowsImage
    {
        private string m_WimPath;

        public string WimPath
        {
            get
            {
                return m_WimPath;
            }
        }
        private int m_ImageID;
        public int ImageID
        {
            get
            {
                return m_ImageID;
            }
        }
        private string m_ImageName;
        public string ImageName
        {
            get
            {
                return m_ImageName;
            }
        }
        private Int32 m_Major;
        public Int32 Major
        {
            get
            {
                return m_Major;
            }
        }
        private Int32 m_Minor;
        public Int32 Minor
        {
            get
            {
                return m_Minor;
            }
        }
        private Int32 m_Build;
        public Int32 Build
        {
            get
            {
                return m_Build;
            }
        }
        private Int32 m_SPBuild;
        public Int32 SPBuild
        {
            get
            {
                return m_SPBuild;
            }
        }
        private bool m_Success;
        public bool Success
        {
            get
            {
                return m_Success;
            }
        }

    }

}
