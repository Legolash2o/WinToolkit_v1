using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Management;
using System.Windows.Forms;

namespace RunOnce {

	//http://social.msdn.microsoft.com/Forums/vstudio/en-US/a3d4dbc7-c63a-46d5-a191-c73a4fca233a/usb-drive-detection-flaws-in-systemiodriveinfogetdrives?forum=netfxbcl
	//07th September 2014
	//Malcolm McCaffery (Fujitsu Partner)
	//Monday, January 17, 2011 12:44 PM

	class DiskDrive {
		public string MediaType;
		public string DriveLetter;
		public string VolumeName;
		public bool InitialFind;
		public ulong Size;
		public ulong Freespace;
	}

	static class DriveDetection {
		public static List<DiskDrive> GetAvailableDisks() {
			var DiskDrives = new List<DiskDrive>();

			foreach (DriveInfo drive in DriveInfo.GetDrives().Where(d => d.IsReady)) {

				var disk = new DiskDrive();

				disk.MediaType = drive.DriveType.ToString();
				disk.DriveLetter = drive.Name;
				disk.Freespace = (ulong)drive.AvailableFreeSpace;
				disk.Size = (ulong)drive.TotalSize;
				disk.VolumeName = drive.VolumeLabel;
				disk.InitialFind = true;
				DiskDrives.Add(disk);
			}

			try {

				// browse all USB WMI physical disks
				foreach (var o in new ManagementObjectSearcher("select DeviceID, MediaType from Win32_DiskDrive").Get()) {
				    var drive = (ManagementObject) o;
				    // associate physical disks with partitions
					var partitionCollection = new ManagementObjectSearcher(String.Format(
					 "associators of {{Win32_DiskDrive.DeviceID='{0}'}} " +
						"where AssocClass = Win32_DiskDriveToDiskPartition",
					 drive["DeviceID"])).Get();

					foreach (var managementBaseObject in partitionCollection)
					{
					    var partition = (ManagementObject) managementBaseObject;
					    if (partition != null) {
							// associate partitions with logical disks (drive letter volumes)
							var logicalCollection = new ManagementObjectSearcher(String.Format(
							 "associators of {{Win32_DiskPartition.DeviceID='{0}'}} " +
							  "where AssocClass= Win32_LogicalDiskToPartition",
							 partition["DeviceID"])).Get();

							foreach (ManagementObject logical in logicalCollection) {
							    if (logical == null) continue;
							    // finally find the logical disk entry
							    ManagementObjectCollection.ManagementObjectEnumerator volumeEnumerator = new ManagementObjectSearcher(String.Format(
							        "select * from Win32_LogicalDisk " + "where Name='{0}'",
							        logical["Name"])).Get().GetEnumerator();

							    volumeEnumerator.MoveNext();

							    var volume = (ManagementObject)volumeEnumerator.Current;

							    var disk = new DiskDrive();
							    string driveLetter = volume["DeviceID"] + "\\";
							    if (DiskDrives.Any(d => d.DriveLetter == driveLetter)) continue;
							    disk.MediaType = drive["MediaType"].ToString();
							    disk.DriveLetter = driveLetter;
							    disk.Freespace = (ulong)volume["Freespace"];
							    disk.Size = (ulong)volume["Size"];
							    disk.VolumeName = volume["VolumeName"].ToString();
							    disk.InitialFind = false;
							    DiskDrives.Add(disk);
							}
						}
					}
				}

			}
			catch (Exception) { }

			return DiskDrives;
		}

	}
}