﻿/* 

Copyright © 2020 Eren "Haltroy" Kanat

Use of this source code is governed by MIT License that can be found in github.com/Haltroy/Korot/blob/master/LICENSE 

*/
using System;
using System.Runtime.InteropServices;

namespace Korot
{
    internal class WindowsMessageHelper
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int wMsg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll")]
        public static extern int RegisterWindowMessage(string msgName);

        public static int ClearHistoryArg;

        static WindowsMessageHelper()
        {
            ClearHistoryArg = WindowsMessageHelper.RegisterWindowMessage("Jumplist.demo.ClearHistoryArg");
        }

        public static int RegisterMessage(string msgName)
        {
            return RegisterWindowMessage(msgName);
        }

        public static void SendMessage(string windowTitle, int msgId)
        {
            SendMessage(windowTitle, msgId, IntPtr.Zero, IntPtr.Zero);
        }

        public static bool SendMessage(string windowTitle, int msgId, IntPtr wParam, IntPtr lParam)
        {
            IntPtr WindowToFind = FindWindow(null, windowTitle);
            if (WindowToFind == IntPtr.Zero)
            {
                return false;
            }

            long result = SendMessage(WindowToFind, msgId, wParam, lParam);

            if (result == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}