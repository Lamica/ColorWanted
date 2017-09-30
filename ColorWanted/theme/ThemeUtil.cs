﻿using ColorWanted.setting;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace ColorWanted.theme
{
    public static class ThemeUtil
    {
        private static readonly Dictionary<ThemeType, ThemeDescription> Themes;

        static ThemeUtil()
        {
            Themes = new Dictionary<ThemeType, ThemeDescription>
            {
                {
                    ThemeType.Dark, new ThemeDescription
                    {
                        ForeColor = Color.FromArgb(0, 192, 0),
                        BackColor = Color.FromArgb(0, 0, 0)
                    }
                },{
                    ThemeType.Light, new ThemeDescription
                    {
                        ForeColor = Color.FromArgb(0, 0, 0),
                        BackColor = Color.FromArgb(255, 255, 255)
                    }
                },{
                    ThemeType.Custom, new ThemeDescription
                    {
                        ForeColor = Settings.Theme.CustomForeColor,
                        BackColor = Settings.Theme.CustomBackColor
                    }
                }
            };
        }

        public static ThemeDescription GetCurrent()
        {
            return Get(Settings.Theme.Type);
        }

        public static ThemeDescription Get(ThemeType type)
        {
            return Themes[type];
        }

        public static void SetOpacity(int opacity)
        {
            var value = opacity / 100.0;
            foreach (Form form in Application.OpenForms)
            {
                if (form.Name != "MainForm" && form.Name != "PreviewForm")
                {
                    form.Opacity = value;
                }
            }
        }

        public static void SetTheme(ThemeDescription theme, Form theForm = null)
        {
            foreach (var form in theForm == null ? Application.OpenForms.Cast<Form>() : new[] { theForm })
            {
                form.BackColor = theme.BackColor;
                form.ForeColor = theme.ForeColor;
                foreach (Control control in form.Controls)
                {
                    if (!(control is LinkLabel)) continue;

                    var link = control as LinkLabel;
                    link.ForeColor = theme.ForeColor;
                    link.LinkColor = theme.ForeColor;
                }
            }
        }

        /// <summary>
        /// 将主题应用到窗体上
        /// </summary>
        /// <param name="form"></param>
        public static void Apply(Form form)
        {
            SetOpacity(Settings.Theme.Opacity);

            var theme = GetCurrent();
            if (theme.IsEmpty)
            {
                theme = Get(ThemeType.Dark);
            }

            SetTheme(theme, form);
        }
    }
}
