﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace LauncherGUI.Helpers
{
    internal class DesktopResolutionHelper
    {
        [DllImport("user32.dll", BestFitMapping = false, CharSet = CharSet.Ansi, ThrowOnUnmappableChar = true)]
        public static extern bool EnumDisplaySettings(string? deviceName, int modeNum, ref DEVMODE devMode);

        [StructLayout(LayoutKind.Sequential)]
        public struct DEVMODE
        {
            private const int CCHDEVICENAME = 0x20;
            private const int CCHFORMNAME = 0x20;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmDeviceName;
            public short dmSpecVersion;
            public short dmDriverVersion;
            public short dmSize;
            public short dmDriverExtra;
            public int dmFields;
            public int dmPositionX;
            public int dmPositionY;
            public int dmDisplayOrientation;
            public int dmDisplayFixedOutput;
            public short dmColor;
            public short dmDuplex;
            public short dmYResolution;
            public short dmTTOption;
            public short dmCollate;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 0x20)]
            public string dmFormName;
            public short dmLogPixels;
            public int dmBitsPerPel;
            public int dmPelsWidth;
            public int dmPelsHeight;
            public int dmDisplayFlags;
            public int dmDisplayFrequency;
        }

        internal static List<string> GetAllSupportedResolutions()
        {
            List<string> allResolutions = [];

            DEVMODE vDevMode = new();
            int i = 0;
            while (EnumDisplaySettings(null, i, ref vDevMode))
            {
                if (vDevMode.dmDisplayFrequency == 60 && vDevMode.dmBitsPerPel == 32 && vDevMode.dmDisplayFixedOutput == 0)
                    allResolutions.Add(vDevMode.dmPelsWidth.ToString() + "x" + vDevMode.dmPelsHeight.ToString());

                i++;
            }

            allResolutions.RemoveRange(0, Math.Min(3, allResolutions.Count));
            allResolutions[^1] = allResolutions[^1];

            return allResolutions;
        }
    }
}