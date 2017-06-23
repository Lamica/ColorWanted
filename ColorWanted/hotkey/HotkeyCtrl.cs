﻿using System.Text;
using System.Windows.Forms;

namespace ColorWanted.hotkey
{
    internal partial class HotkeyCtrl : UserControl
    {
        private StringBuilder buffer;
        private readonly HotKey hotkey;
        /// <summary>
        /// 设置快捷键成功后的事件回调
        /// </summary>
        public event HotkeyChangeHandler OnHotkeyChanged;

        public HotkeyCtrl()
        {
            InitializeComponent();
        }

        public HotkeyCtrl(HotKey hotkey)
        {
            InitializeComponent();

            this.hotkey = hotkey;
            lbTypeName.Text = hotkey.Name;
            tbInput.Text = hotkey.ToString();
        }

        public void Reset()
        {
            hotkey.Reset();
            tbInput.Text = hotkey.ToString();
            lkOK_LinkClicked(null, null);
        }

        private void tbInput_KeyDown(object sender, KeyEventArgs e)
        {
            // 删除键，清空快捷键
            if (e.KeyValue == 8)
            {
                tbInput.Clear();

                hotkey.Modifiers = KeyModifier.None;
                hotkey.Key = Keys.None;
                return;
            }

            if (buffer == null)
            {
                buffer = new StringBuilder(32);
            }
            else
            {
                buffer.Clear();
            }
            hotkey.Modifiers = KeyModifier.None;
            if (e.Control)
            {
                buffer.Append("Ctrl + ");
                hotkey.Modifiers |= KeyModifier.Ctrl;
            }
            if (e.Alt)
            {
                buffer.Append("Alt + ");
                hotkey.Modifiers |= KeyModifier.Alt;
            }
            if (e.Shift)
            {
                buffer.Append("Shift + ");
                hotkey.Modifiers |= KeyModifier.Shift;
            }

            if (e.KeyValue >= 33 && e.KeyValue <= 40 ||
                e.KeyValue >= 65 && e.KeyValue <= 90 ||   //a-z/A-Z
                e.KeyValue >= 112 && e.KeyValue <= 123)   //F1-F12
            {
                buffer.Append(e.KeyCode);
            }
            else if (e.KeyValue >= 48 && e.KeyValue <= 57)    //0-9
            {
                buffer.Append(e.KeyCode.ToString().Substring(1));
            }
            else if (e.KeyCode == Keys.Oemtilde)
            {
                buffer.Append("`");
            }
            else if (e.KeyCode == Keys.Enter)
            {
                buffer.Append("Enter");
            }

            tbInput.Text = buffer.ToString();

            hotkey.Key = e.KeyCode;
        }

        private void tbInput_KeyUp(object sender, KeyEventArgs e)
        {
            var str = tbInput.Text.TrimEnd();

            if (!str.EndsWith("+")) return;

            tbInput.Text = hotkey.ToString();
        }

        private void lkOK_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HotKey.Set(hotkey);

            if (OnHotkeyChanged == null)
            {
                return;
            }

            OnHotkeyChanged.Invoke(hotkey);
        }

        private void lkReset_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Reset();
        }
    }
}
