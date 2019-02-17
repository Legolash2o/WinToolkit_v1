// Copyright (c) Microsoft Corporation.  All rights reserved.

using System;
using System.Runtime.InteropServices;

namespace WinToolkit {
	public static class Windows7Taskbar {
		private static ITaskbarList3 _taskbarList;

		private static ITaskbarList3 TaskbarList {
			get {
				if (_taskbarList == null) {
					lock (typeof(Windows7Taskbar)) {
						if (_taskbarList == null) {
							_taskbarList = (ITaskbarList3)new CTaskbarList();
							_taskbarList.HrInit();
						}
					}
				}
				return _taskbarList;
			}
		}

		static readonly OperatingSystem osInfo = Environment.OSVersion;

		internal static bool Windows7OrGreater {
			get {
				return (osInfo.Version.Major == 6 && osInfo.Version.Minor >= 1) || (osInfo.Version.Major > 6);
			}
		}
		/// <summary>
		/// Sets the progress state of the specified window's
		/// taskbar button.
		/// </summary>
		/// <param name="hwnd">The window handle.</param>
		/// <param name="state">The progress state.</param>
		public static void SetProgressState(IntPtr hwnd, ThumbnailProgressState state) {
			try {
				if (Windows7OrGreater && hwnd != null) {
					TaskbarList.SetProgressState(hwnd, state);
				}
			}
			catch (Exception Ex) {
				new SmallError("Error setting taskbar state.", Ex).Upload();
			}
		}
		/// <summary>
		/// Sets the progress value of the specified window's
		/// taskbar button.
		/// </summary>
		/// <param name="hwnd">The window handle.</param>
		/// <param name="current">The current value.</param>
		/// <param name="maximum">The maximum value.</param>
		public static void SetProgressValue(IntPtr hwnd, ulong current, ulong maximum) {
			try {
				if (Windows7OrGreater && hwnd != null && current < maximum) {
					TaskbarList.SetProgressValue(hwnd, current, maximum);
				}
			}
			catch (Exception Ex) {
				new SmallError("Error setting taskbar value.", Ex, current.ToString() + "/" + maximum.ToString()).Upload();
			}
		}

		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		internal static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
	}
}