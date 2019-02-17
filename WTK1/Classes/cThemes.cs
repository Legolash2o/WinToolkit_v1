using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using WinToolkit.Classes;

namespace WinToolkit {
	class cThemes {

		public static bool PatchFiles(string WimPath) {
			string sysFol = WimPath + "\\Windows\\System32\\";
			bool x64Mode = Directory.Exists(WimPath + "\\Windows\\SysWOW64\\");

			try {
				PatchUXTHEME_DLL(sysFol + "uxtheme.dll", x64Mode);
				PatchThemeService_DLL(sysFol + "themeservice.dll", x64Mode);
				PatchSHSVCS_DLL(sysFol + "shsvcs.dll", x64Mode);
				PatchThemeUI_DLL(sysFol + "themeui.dll", x64Mode);
				PatchUXINIT_DLL(sysFol + "uxinit.dll", x64Mode);
			}
			catch (Exception Ex) {
				new SmallError("Error patching theme", Ex, WimPath).Upload();
				return false;
			}
			return true;
		}
		
		#region THEMEUI

		public static bool PatchThemeUI_DLL(string filePath, bool x64) {
			if (!File.Exists(filePath)) { return false; }
			cMain.TakeOwnership(filePath);

			string dllVersion = FileVersionInfo.GetVersionInfo(filePath).FileVersion;

			byte[] oldValues = null;

			var toWrite = new Dictionary<long, byte>();
			long startPos = 0;
			long endPos = 0;

			if (x64) {
				if (dllVersion.StartsWithIgnoreCase("6.0") || dllVersion.StartsWithIgnoreCase("6.1")) {
					if (dllVersion.StartsWithIgnoreCase("6.0")) { startPos = 265000; }
					if (dllVersion.StartsWithIgnoreCase("6.1")) { startPos = 329000; }
					oldValues = new byte[] { 0x48, 0x81, 0xEC, 0xC0, 0x00, 0x00, 0x00, 0x48 };
					toWrite.Add(0, 0x33); toWrite.Add(1, 0xDB); toWrite.Add(2, 0x8B);
					toWrite.Add(3, 0xC3); toWrite.Add(4, 0x5F); toWrite.Add(5, 0x5E);
					toWrite.Add(6, 0x5D); toWrite.Add(7, 0xC3);
				}
				else if (dllVersion.StartsWithIgnoreCase("6.2") || dllVersion.StartsWithIgnoreCase("6.3")) {
					if (dllVersion.StartsWithIgnoreCase("6.2")) { startPos = 263000; }
					if (dllVersion.StartsWithIgnoreCase("6.3")) { startPos = 280000; }
					oldValues = new byte[] { 0xC0, 0x78, 0x46 };
					toWrite.Add(1, 0x90); toWrite.Add(2, 0x90);
					toWrite.Add(47, 0x90); toWrite.Add(48, 0x90);
				}
			}
			else {
				if (dllVersion.StartsWithIgnoreCase("6.0") || dllVersion.StartsWithIgnoreCase("6.1")) {
					if (dllVersion.StartsWithIgnoreCase("6.0")) { startPos = 213000; }
					if (dllVersion.StartsWithIgnoreCase("6.1")) { startPos = 268000; }
					oldValues = new byte[] { 0x81, 0xEC, 0x84, 0x00, 0x00 };
					toWrite.Add(0, 0x33); toWrite.Add(1, 0xC0); toWrite.Add(2, 0xC9);
					toWrite.Add(3, 0xC2); toWrite.Add(4, 0x04);
				}
				else if (dllVersion.StartsWithIgnoreCase("6.2") || dllVersion.StartsWithIgnoreCase("6.3")) {
					if (dllVersion.StartsWithIgnoreCase("6.2")) { startPos = 228000; }
					if (dllVersion.StartsWithIgnoreCase("6.3")) { startPos = 248000; }
					oldValues = new byte[] { 0xC0, 0x78, 0x3D, 0x6A };
					toWrite.Add(1, 0x90); toWrite.Add(2, 0x90);
					toWrite.Add(36, 0x90); toWrite.Add(37, 0x90);
				}
			}

			if (oldValues == null || toWrite.Count == 0) { return false; }

			if (startPos > 0 && endPos == 0) { endPos = startPos + 10000; }

			long lPosition = Hex.FindBytes(filePath, oldValues, startPos, endPos);

			if (lPosition > 0) {
				Hex.WriteBytes(filePath, lPosition, toWrite);
				return true;
			}
			return false;
		}
		#endregion

		#region UXTHEME
		public static bool PatchUXTHEME_DLL(string filePath, bool x64) {
			if (!File.Exists(filePath)) { return false; }
			string dllVersion = FileVersionInfo.GetVersionInfo(filePath).FileVersion;

			byte[] oldValues = null;
			long startPos = 0;
			long endPos = 0;
			var toWrite = new Dictionary<long, byte>();

			if (x64) {
				if (dllVersion.StartsWithIgnoreCase("6.0") || dllVersion.StartsWithIgnoreCase("6.1")) {
					if (dllVersion.StartsWithIgnoreCase("6.0")) { startPos = 110000; }
					if (dllVersion.StartsWithIgnoreCase("6.1")) { startPos = 115000; }
					oldValues = new byte[] { 0x48, 0x81, 0xEC, 0xC0, 0x00, 0x00, 0x00, 0x48 };
					toWrite.Add(0, 0x33); toWrite.Add(1, 0xDB); toWrite.Add(2, 0x8B);
					toWrite.Add(3, 0xC3); toWrite.Add(4, 0x5F); toWrite.Add(5, 0x5E);
					toWrite.Add(6, 0x5D); toWrite.Add(7, 0xC3);
				}
				else if (dllVersion.StartsWithIgnoreCase("6.2") || dllVersion.StartsWithIgnoreCase("6.3")) {
					if (dllVersion.StartsWithIgnoreCase("6.2")) { startPos = 592000; }
					if (dllVersion.StartsWithIgnoreCase("6.3.9600.16")) { startPos = 914000; }
					if (dllVersion.StartsWithIgnoreCase("6.3.9600.17")) { startPos = 709000; }
					oldValues = new byte[] { 0xC0, 0x78, 0x46, 0x4C };
					toWrite.Add(1, 0x90); toWrite.Add(2, 0x90);
					toWrite.Add(47, 0x90); toWrite.Add(48, 0x90);
				}
			}
			else {
				if (dllVersion.StartsWithIgnoreCase("6.0") || dllVersion.StartsWithIgnoreCase("6.1")) {
					if (dllVersion.StartsWithIgnoreCase("6.0")) { startPos = 102000; }
					if (dllVersion.StartsWithIgnoreCase("6.1")) { startPos = 111000; }
					oldValues = new byte[] { 0x81, 0xEC, 0x84, 0x00, 0x00 };
					toWrite.Add(0, 0x33); toWrite.Add(1, 0xC0); toWrite.Add(2, 0xC9);
					toWrite.Add(3, 0xC2); toWrite.Add(4, 0x04);
				}
				else if (dllVersion.StartsWithIgnoreCase("6.2") || dllVersion.StartsWithIgnoreCase("6.3")) {
					if (dllVersion.StartsWithIgnoreCase("6.2")) { startPos = 550000; }
					if (dllVersion.StartsWithIgnoreCase("6.3.9600.16")) { startPos = 781000; }
					if (dllVersion.StartsWithIgnoreCase("6.3.9600.17")) { startPos = 780000; }
					oldValues = new byte[] { 0xC0, 0x78, 0x3D, 0x6A };
					toWrite.Add(1, 0x90); toWrite.Add(2, 0x90);
					toWrite.Add(34, 0x90); toWrite.Add(35, 0x90);
				}
			}

			if (oldValues == null || toWrite.Count == 0) { return false; }

			if (startPos > 0 && endPos == 0) { endPos = startPos + 10000; }

			long lPosition = Hex.FindBytes(filePath, oldValues, startPos, endPos);

			if (lPosition > 0) {
				Hex.WriteBytes(filePath, lPosition, toWrite);
				return true;
			}
			return false;
		}
		#endregion

		//Windows Vista Only 6.0
		#region SHSVCS
		public static bool PatchSHSVCS_DLL(string filePath, bool x64) {
			if (!File.Exists(filePath)) { return false; }

			string dllVersion = FileVersionInfo.GetVersionInfo(filePath).FileVersion;
			if (!dllVersion.StartsWithIgnoreCase("6.0")) { return false; }

			byte[] oldValues = null;
			long startPos = 0, endPos = 0;
			var toWrite = new Dictionary<long, byte>();

			if (x64) {
				oldValues = new byte[] { 0x48, 0x81, 0xEC, 0xC0, 0x00, 0x00, 0x00, 0x48 };
				toWrite.Add(0, 0x33); toWrite.Add(1, 0xDB); toWrite.Add(2, 0x8B);
				toWrite.Add(3, 0xC3); toWrite.Add(4, 0x5F); toWrite.Add(5, 0x5E);
				toWrite.Add(6, 0x5D); toWrite.Add(7, 0xC3);
				startPos = 45000;
			}
			else {
				oldValues = new byte[] { 0x81, 0xEC, 0x84, 0x00, 0x00 };
				toWrite.Add(0, 0x33); toWrite.Add(1, 0xC0); toWrite.Add(2, 0xC9);
				toWrite.Add(3, 0xC2); toWrite.Add(4, 0x04);
				startPos = 55000;
			}

			if (oldValues == null || toWrite.Count == 0) { return false; }
			if (startPos > 0 && endPos == 0) { endPos = startPos + 10000; }

			long lPosition = Hex.FindBytes(filePath, oldValues, startPos, endPos);

			if (lPosition > 0) {
				Hex.WriteBytes(filePath, lPosition, toWrite);
				return true;
			}
			return false;
		}
		#endregion

		//Windows 7 Only 6.1
		#region ThemeService
		public static bool PatchThemeService_DLL(string filePath, bool x64) {
			if (!File.Exists(filePath)) { return false; }

			string dllVersion = FileVersionInfo.GetVersionInfo(filePath).FileVersion;
			if (!dllVersion.StartsWithIgnoreCase("6.1")) { return false; }

			byte[] oldValues = null;
			long startPos = 0, endPos = 0;
			var toWrite = new Dictionary<long, byte>();

			if (x64) {
				oldValues = new byte[] { 0x48, 0x81, 0xEC, 0xC0, 0x00, 0x00, 0x00, 0x48 };
				toWrite.Add(0, 0x33); toWrite.Add(1, 0xDB); toWrite.Add(2, 0x8B);
				toWrite.Add(3, 0xC3); toWrite.Add(4, 0x5F); toWrite.Add(5, 0x5E);
				toWrite.Add(6, 0x5D); toWrite.Add(7, 0xC3);
				startPos = 17000;
			}
			else {
				oldValues = new byte[] { 0x81, 0xEC, 0x84, 0x00, 0x00 };
				toWrite.Add(0, 0x33); toWrite.Add(1, 0xC0); toWrite.Add(2, 0xC9);
				toWrite.Add(3, 0xC2); toWrite.Add(4, 0x04);
				startPos = 12000;
			}

			if (oldValues == null || toWrite.Count == 0) { return false; }
			if (startPos > 0 && endPos == 0) { endPos = startPos + 10000; }

			long lPosition = Hex.FindBytes(filePath, oldValues, startPos, endPos);

			if (lPosition > 0) {
				Hex.WriteBytes(filePath, lPosition, toWrite);
				return true;
			}
			return false;
		}
		#endregion

		//Windows 8 Only 6.2 / 6.3
		#region UXINIT

		public static bool PatchUXINIT_DLL(string filePath, bool x64) {
			if (!File.Exists(filePath)) { return false; }

			string dllVersion = FileVersionInfo.GetVersionInfo(filePath).FileVersion;
			if (!dllVersion.StartsWithIgnoreCase("6.2") && !dllVersion.StartsWithIgnoreCase("6.3")) { return false; }

			byte[] oldValues = null;
			long startPos = 0, endPos = 0;
			var toWrite = new Dictionary<long, byte>();

			if (x64) {
				if (dllVersion.StartsWithIgnoreCase("6.2")) { startPos = 33000; }
				if (dllVersion.StartsWithIgnoreCase("6.3")) { startPos = 45000; }
				oldValues = new byte[] { 0xC0, 0x78, 0x46, 0x4C };
				toWrite.Add(1, 0x90); toWrite.Add(2, 0x90);
				toWrite.Add(47, 0x90); toWrite.Add(48, 0x90);

			}
			else {
				if (dllVersion.StartsWithIgnoreCase("6.2")) { startPos = 28000; }
				if (dllVersion.StartsWithIgnoreCase("6.3")) { startPos = 35000; }
				oldValues = new byte[] { 0xC0, 0x78, 0x3D, 0x6A };
				toWrite.Add(1, 0x90); toWrite.Add(2, 0x90);
				toWrite.Add(36, 0x90); toWrite.Add(37, 0x90);
			}

			if (oldValues == null || toWrite.Count == 0) { return false; }
			if (startPos > 0 && endPos == 0) { endPos = startPos + 10000; }

			long lPosition = Hex.FindBytes(filePath, oldValues, startPos, endPos);

			if (lPosition > 0) {
				Hex.WriteBytes(filePath, lPosition, toWrite);
				return true;
			}
			return false;
		}
		#endregion
	}

	public static class Hex {

		/// <summary>
		/// Find an array of bytes within a file. Useful for hex editing.
		/// </summary>
		/// <param name="fileName">The file you wish to search.</param>
		/// <param name="bytes">An array of bytes</param>
		/// <returns></returns>
		public static long FindBytes(string fileName, byte[] bytes) {
			return FindBytes(fileName, bytes, 0, 0);
		}

		/// <summary>
		/// Find an array of bytes within a file. Useful for hex editing.
		/// </summary>
		/// <param name="fileName">The file you wish to search.</param>
		/// <param name="bytes">An array of bytes</param>
		/// <param name="startPos">For efficiency purposes (optional). Narrow the search down.</param>
		/// <returns></returns>
		public static long FindBytes(string fileName, byte[] bytes, long startPos) {
			return FindBytes(fileName, bytes, startPos, 0);
		}

		/// <summary>
		/// Find an array of bytes within a file. Useful for hex editing.
		/// </summary>
		/// <param name="fileName">The file you wish to search.</param>
		/// <param name="bytes">An array of bytes</param>
		/// <param name="startPos">For efficiency purposes (optional). Narrow the search down.</param>
		/// <param name="endPos">For efficiency purposes (optional). Narrow the search down.</param>
		/// <returns>The position of the first item in the search array.</returns>
		public static long FindBytes(string fileName, byte[] bytes, long startPos, long endPos) {

			if (startPos < 1) { startPos = 0; }
			using (var fs = File.OpenRead(fileName)) {

				for (long i = startPos; i < fs.Length - bytes.Length; i++) {
					fs.Seek(i, SeekOrigin.Begin);
					long j;
					for (j = 0; j < bytes.Length; j++)
						if (fs.ReadByte() != bytes[j]) { break; }
					if (endPos > startPos && i >= endPos) { return -1; }
					if (j != bytes.Length) continue;
					return i;
				}
			}
			return -1;
		}

		/// <summary>
		/// Writes specified hex values in the required position.
		/// </summary>
		/// <param name="fileName">The file you're editing.</param>
		/// <param name="toWrite">A dictionary of locations and bytes that require writing.</param>
		/// <returns></returns>
		public static bool WriteBytes(string fileName, Dictionary<long, byte> toWrite) {
			return WriteBytes(fileName, 0, toWrite);
		}


		/// <summary>
		/// Writes specified hex values in the required position.
		/// </summary>
		/// <param name="fileName">The file you're editing.</param>
		/// <param name="position">The starting point. Usually retried with the FindBytes method.</param>
		/// <param name="toWrite">A dictionary of locations and bytes that require writing.</param>
		/// <returns></returns>
		public static bool WriteBytes(string fileName, long position, Dictionary<long, byte> toWrite) {
			cMain.TakeOwnership(fileName);
			using (var stream = new FileStream(fileName, FileMode.Open, FileAccess.ReadWrite)) {
				foreach (var values in toWrite) {
					stream.Position = position + values.Key;
					stream.WriteByte(values.Value);
				}
			}
			return true;
		}


	}
}