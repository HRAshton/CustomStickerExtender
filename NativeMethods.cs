using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace HRAshton.CustomStickerExtender
{
    internal static class NativeMethods
    {
        // Get a handle to an application window.
        [DllImport("user32.dll", SetLastError = true)]
        internal static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        // Find window by Caption only. Note you must pass IntPtr.Zero as the first parameter.
        [DllImport("user32.dll", EntryPoint = "FindWindow", SetLastError = true)]
        internal static extern IntPtr FindWindowByCaption(IntPtr ZeroOnly, string lpWindowName);

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32.dll")]
        internal static extern bool GetTitleBarInfo(IntPtr hwnd, ref TITLEBARINFO pti);

        [DllImport("user32.dll")]
        internal static extern IntPtr GetForegroundWindow();
        
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        internal static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        internal static extern int GetWindowTextLength(IntPtr hWnd);
        
        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, WinApiHelpers.GWLParameter nIndex);

        //GetLastError- retrieves the last system error.

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);
        
        [StructLayout(LayoutKind.Sequential)]
        internal struct TITLEBARINFO
        {
            public uint cbSize; //Specifies the size, in bytes, of the structure. 
            //The caller must set this to sizeof(TITLEBARINFO).

            public RECT rcTitleBar; //Pointer to a RECT structure that receives the 
            //coordinates of the title bar. These coordinates include all title-bar elements
            //except the window menu.

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]

            //Add reference for System.Windows.Forms
            public AccessibleStates[] rgstate;
            //0	The title bar itself.
            //1	Reserved.
            //2	Minimize button.
            //3	Maximize button.
            //4	Help button.
            //5	Close button.
        }

        [StructLayout(LayoutKind.Sequential)]
        internal struct RECT
        {
            internal int left;
            internal int top;
            internal int right;
            internal int bottom;
        }
        ///The SetWindowLongPtr function changes an attribute of the specified window

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        internal static extern int SetWindowLong32(HandleRef hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        internal static extern int SetWindowLong32(IntPtr windowHandle, WinApiHelpers.GWLParameter nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        internal static extern IntPtr SetWindowLongPtr64(IntPtr windowHandle, WinApiHelpers.GWLParameter nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        internal static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, IntPtr dwNewLong);
    }
}
