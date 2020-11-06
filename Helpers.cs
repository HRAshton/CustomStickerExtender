﻿using System;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows;
using System.Windows.Interop;

namespace HRAshton.CustomStickerExtender
{
    public class Helpers
    {
        public static IntPtr Find(string ModuleName, string MainWindowTitle)
        {
            //Search the window using Module and Title
            IntPtr WndToFind = NativeMethods.FindWindow(ModuleName, MainWindowTitle);
            if (WndToFind.Equals(IntPtr.Zero))
            {
                if (!string.IsNullOrEmpty(MainWindowTitle))
                {
                    //Search window using TItle only.
                    WndToFind = NativeMethods.FindWindowByCaption(WndToFind, MainWindowTitle);
                    if (WndToFind.Equals(IntPtr.Zero))
                        return new IntPtr(0);
                }
            }
            return WndToFind;
        }

        public static IntPtr GetForegroundWindow()
        {
            IntPtr WndToFind = NativeMethods.GetForegroundWindow();
            
            return WndToFind;
        }

        public static string GetWindowText(IntPtr hWnd)
        {
            var length = NativeMethods.GetWindowTextLength(hWnd) + 1;
            var title = new StringBuilder(length);
            NativeMethods.GetWindowText(hWnd, title, length);
            return title.ToString();
        }

        public static void SetNoActivate(Window currentWindow)
        {
            int WS_EX_NOACTIVATE = 0x08000000;
            
            var helper = new WindowInteropHelper(currentWindow);
            NativeMethods.SetWindowLong32(helper.Handle, GWLParameter.GWL_EXSTYLE,
                NativeMethods.GetWindowLong(helper.Handle, GWLParameter.GWL_EXSTYLE) | WS_EX_NOACTIVATE);
        }

        public static WinPosition GetWindowPosition(IntPtr wnd)
        {
            NativeMethods.TITLEBARINFO pti = new NativeMethods.TITLEBARINFO();

            pti.cbSize = (uint)Marshal.SizeOf(pti);//Specifies the size, in bytes, of the structure. 
            //The caller must set this to sizeof(TITLEBARINFO).

            bool result = NativeMethods.GetTitleBarInfo(wnd, ref pti);
            NativeMethods.GetWindowRect(wnd, out NativeMethods.RECT sizes);

            var winpos = result ? new WinPosition(pti) : new WinPosition();
            winpos.Bottom = sizes.bottom - 8;
            winpos.Right = sizes.right - 8;

            return winpos;
        }

        public static IntPtr SetWindowLongPtr(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 8) //Check if this window is 64bit
            {
                return NativeMethods.SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            }
            return new IntPtr(NativeMethods.SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
        }

        //Specifies the zero-based offset to the value to be set.
        //Valid values are in the range zero through the number of bytes of extra window memory, minus the size of an integer.
        public enum GWLParameter
        {
            GWL_EXSTYLE = -20, //Sets a new extended window style
            GWL_HINSTANCE = -6, //Sets a new application instance handle.
            GWL_HWNDPARENT = -8, //Set window handle as parent
            GWL_ID = -12, //Sets a new identifier of the window.
            GWL_STYLE = -16, // Set new window style
            GWL_USERDATA = -21, //Sets the user data associated with the window. This data is intended for use by the application that created the window. Its value is initially zero.
            GWL_WNDPROC = -4 //Sets a new address for the window procedure.
        }

        public static int SetWindowLong(IntPtr windowHandle, GWLParameter nIndex, int dwNewLong)
        {
            if (IntPtr.Size == 8) //Check if this window is 64bit
            {
                return (int)NativeMethods.SetWindowLongPtr64(windowHandle, nIndex, new IntPtr(dwNewLong));
            }
            return NativeMethods.SetWindowLong32(windowHandle, nIndex, dwNewLong);
        }
    }
}
