﻿using ColorWanted.colors;
using ColorWanted.ext;
using ColorWanted.hotkey;
using ColorWanted.mode;
using ColorWanted.setting;
using ColorWanted.util;
using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace ColorWanted
{
    internal partial class MainForm
    {
        /// <summary>
        /// 执行初始化操作
        /// </summary>
        private void Init()
        {
            trayMenuHsiAlgorithmGeometry.Tag = HsiAlgorithm.Geometry;
            trayMenuHsiAlgorithmAxis.Tag = HsiAlgorithm.Axis;
            trayMenuHsiAlgorithmSegment.Tag = HsiAlgorithm.Segment;
            trayMenuHsiAlgorithmBajon.Tag = HsiAlgorithm.Bajon;
            trayMenuHsiAlgorithmStandard.Tag = HsiAlgorithm.Standard;

            previewForm = new PreviewForm();
            previewForm.LocationChanged += previewForm_LocationChanged;

            currentDisplayMode = DisplayMode.Fixed;
            SwitchHsiAlgorithm(Settings.Main.HsiAlgorithm);

            colorBuffer = new StringBuilder(8, 64);


            if (Settings.Preview.Visible)
            {
                TogglePreview();
            }

            if (trayMenuFixed.Checked)
            {
                FixedPosition();
            }

            SwitchFormatMode(Settings.Main.Format);

            var now = DateTime.Now;

            lastPressTime = Util.Enum<HotKeyType>()
                .ToDictionary(item => item, item => now);

            HotKey.Bind(Handle);

            trayMenuCopyPolicyHexValueOnly.Checked = Settings.Base.HexValueOnly;
            trayMenuCopyPolicyRgbValueOnly.Checked = Settings.Base.RgbValueOnly;
            trayMenuCopyPolicyUpperCase.Checked = Settings.Base.CopyUpperCase;

            new Thread(() =>
            {
                UpdateTooltip();

                // 读取开机启动的注册表
                this.InvokeMethod(() =>
                {
                    trayMenuAutoStart.Checked = Settings.Base.Autostart;
                    trayMenuAutoPin.Checked = Settings.Base.AutoPin;

                    trayMenuPixelScale.Checked = Settings.Preview.PixelScale;
                });
            })
            {
                IsBackground = true
            }.Start();

            caretTimer = new Timer { Interval = caretInterval };
            caretTimer.Tick += carettimer_Tick;
            caretTimer.Start();

            colorTimer = new Timer { Interval = colorInterval };
            colorTimer.Tick += colortimer_Tick;
            colorTimer.Start();

            // 检查是否是首次运行
            if (Settings.Base.IsFirstRun)
            {

                Settings.Base.IsFirstRun = false;

                // 首次运行时，打开帮助窗口
                trayMenuShowAbout_Click(null, null);
                if (!IsDisposed)
                {
                    // 然后打开快捷键设置窗口
                    trayMenuHotkey_Click(null, null);
                }
            }

            // 是否监听剪贴板
            trayMenuEnableClipboard.Checked = Settings.Clipboard.Enabled;
            if (trayMenuEnableClipboard.Checked)
            {
                NativeMethods.AddClipboardFormatListener(Handle);
            }

            // 加载语言并选中使用的项
            new Thread(() =>
            {
                // 当前显示的语言
                var locale = (Settings.I18n.Lang ?? System.Globalization.CultureInfo.InstalledUICulture.Name).ToLower();
                // 加载自定义语言
                var langs = i18n.I18nManager.GetLocalLangs();
                if (langs.Any())
                {
                    // 都放到其它语言菜单项下
                    var others = new ToolStripMenuItem();
                    resources.ApplyResources(others, "trayMenuLanguageOther");
                    others.Name = "trayMenuLanguageOther";

                    // 存放语言 tooltip 的临时量
                    var temp = new StringBuilder();

                    var subs = langs.Select(language =>
                    {
                        var item = new ToolStripMenuItem();
                        item.Name = $"customize-lang--{language.Locale}";
                        item.Text = language.Name;

                        // 选中项
                        var l = language.Locale.ToLower();
                        item.Checked = locale == l || locale.StartsWith(l) || l.StartsWith(locale);

                        // 提示信息中显示语言的版本以及作者
                        temp.Append($"{language.Version}\n");
                        if (language.Authors != null && language.Authors.Any())
                        {
                            temp.Append("------------\n");
                            foreach (var author in language.Authors)
                            {
                                temp.AppendFormat("{0}/{1}\n", author.Name, author.Mail);
                                if (string.IsNullOrEmpty(author.HomePage))
                                {
                                    temp.Append(author.HomePage);
                                }
                            }
                        }
                        item.ToolTipText = temp.ToString();
                        temp.Clear();
                        return item;
                    });

                    // 添加菜单项
                    others.DropDownItems.AddRange(subs.ToArray());
                    this.InvokeMethod(() =>
                    {
                        trayMenuLanguage.DropDownItems.Add(others);
                    });
                }
                if (locale.StartsWith("zh"))
                {
                    trayMenuLanguageZH.Checked = true;
                }
                else if (!langs.Any() || locale.StartsWith("en"))
                {
                    // 没有其它语言或设置为英语时
                    trayMenuLanguageEN.Checked = true;
                }
            })
            {
                IsBackground = true
            }.Start();

            // 启动时检查更新
            trayMenuCheckUpdateOnStartup.Checked = Settings.Update.CheckOnStartup;

            // 自动检查更新
            if (trayMenuCheckUpdateOnStartup.Checked &&
                (DateTime.Now.Date - Settings.Update.LastUpdate).TotalDays >= Settings.Update.Span)
            {
                update.UpdateForm.ShowWindow(true);
            }
        }
    }
}
