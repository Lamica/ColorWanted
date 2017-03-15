﻿using System;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using ColorWanted.enums;
using Microsoft.Win32;

// ReSharper disable EmptyGeneralCatchClause
// 防止权限问题导致的失败
// 即使失败，也不应该影响功能
// 所有文件操作都加个  ttry catch

namespace ColorWanted
{
    class Settings
    {
        /// <summary>
        /// 配置文件名
        /// </summary>
        public static readonly string FileName;
        private const string section = "colorwanted";

        static Settings()
        {
            var path = Path.Combine(Environment
                    .GetFolderPath(Environment.SpecialFolder.ApplicationData),
                Application.ProductName);
            FileName = Path.Combine(path, "option.ini");

            if (Directory.Exists(path)) return;

            try
            {
                Directory.CreateDirectory(path);
            }
            catch
            {
            }
        }

        private static void Set(string key, string value)
        {
            try
            {
                NativeMethods.WriteIni(section, key, value, FileName);
            }
            catch { }
        }

        private static string Get(string key)
        {
            try
            {
                var buf = new StringBuilder(512);
                NativeMethods.ReadIni(section, key, "", buf, 512, FileName);
                return buf.ToString();
            }
            catch { return ""; }
        }

        public static bool AutoPin
        {
            get
            {
                var v = Get("autopin");
                return v == "" || v == "1";
            }
            set
            {
                Set("autopin", value ? "1" : "0");
            }
        }

        public static bool PreviewVisible
        {
            get
            {
                var v = Get("previewvisible");
                return v == "" || v == "1";
            }
            set
            {
                Set("previewvisible", value ? "1" : "0");
            }
        }

        public static bool ShowRgb
        {
            get
            {
                var v = Get("showrgb");
                return v != "" && v == "1";
            }
            set
            {
                Set("showrgb", value ? "1" : "0");
            }
        }

        public static DisplayMode Mode
        {
            get
            {
                var v = Get("mode");
                DisplayMode mode;
                if (!Enum.TryParse(v, out mode))
                {
                    mode = DisplayMode.Fixed;
                }
                return mode;
            }
            set
            {
                Set("mode", value.ToString());
            }
        }


        public static Point Location
        {
            get
            {
                return ParsePoint(Get("location"));
            }
            set
            {
                Set("location", string.Format("{0},{1}", value.X, value.Y));
            }
        }

        public static bool Autostart
        {
            get
            {
                try
                {
                    using (var reg = Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run"))
                    {
                        if (reg != null)
                        {
                            var path = reg.GetValue(Application.ProductName);
                            if (path != null)
                            {
                                if (string.Equals(path.ToString(), "\"" + Application.ExecutablePath + "\"", StringComparison.OrdinalIgnoreCase))
                                {
                                    return true;
                                }
                            }
                        }
                    }
                }
                catch { }

                return false;
            }

            set
            {
                try
                {
                    using (var reg = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Run"))
                    {
                        if (reg == null) return;
                        if (value)
                        {
                            reg.SetValue(Application.ProductName, "\"" + Application.ExecutablePath + "\"");
                        }
                        else
                        {
                            reg.DeleteValue(Application.ProductName);
                        }
                    }
                }
                catch
                {
                }
            }
        }

        public static Point PreviewLocation
        {
            get
            {
                return ParsePoint(Get("previewLocation"));
            }
            set
            {
                Set("previewLocation", string.Format("{0},{1}", value.X, value.Y));
            }
        }

        private static Point ParsePoint(string loc)
        {
            Point point = Point.Empty;

            if (string.IsNullOrWhiteSpace(loc))
            {
                return point;
            }

            var arr = loc.Split(',');
            if (arr.Length != 2)
            {
                return point;
            }
            int x, y;
            if (int.TryParse(arr[0], out x))
            {
                point.X = x;
            }
            if (int.TryParse(arr[1], out y))
            {
                point.Y = y;
            }

            return point;
        }

        public static bool IsFirstRun
        {
            get { return Get("firstrun") != "0"; }
            set
            {
                Set("firstrun", value ? "1" : "0");
            }
        }
    }
}
