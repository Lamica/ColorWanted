﻿using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
namespace ColorWanted.screenshot
{
    partial class ScreenForm
    {
        private ToolStripButton activeToolShapeType;
        private ToolStripButton activeToolColor;
        private ToolStripButton activeToolLineStyle;

        public DrawRecord current => editor.GetCurrentRecord();

        public void Show(Bitmap img)
        {
            editor.SetImage(img);

            editor.AreaSelected += Editor_AreaSelected;
            editor.AreaCleared += Editor_AreaCleared;

            new Thread(InitEditorToolbar) { IsBackground = true }.Start();

            Refresh();
            Show();
            BringToFront();
            TopMost = false;
        }

        private void Editor_AreaCleared(object sender, EventArgs e)
        {
            // 选区被清除时，隐藏工具条
            toolbarMask.Hide();
        }

        private void Editor_AreaSelected(object sender, events.AreaSelectedEventArgs e)
        {
            // 这个事件会在创建选区或移动选区时触发
            if (toolPanel.Visible)
            {
                toolPanel.Hide();
            }

            toolbarMask.Left = e.Rect.X;
            toolbarMask.Top = e.Rect.Y + e.Rect.Height;

            if (!toolbarMask.Visible)
            {
                toolbarMask.Show();
            }
        }

        /// <summary>
        /// 初始化编辑器的工具条
        /// </summary>
        private void InitEditorToolbar()
        {
            activeToolShapeType = toolRectangle;
            activeToolColor = toolColorRed;
            activeToolLineStyle = toolStyleSolid;

            // 字体
            toolTextStyle.ForeColor = activeToolColor.BackColor;
        }

        private void ScreenForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                CloseForm();
            }
        }

        private void CloseForm()
        {
            Close();
        }

        private void ScreenForm_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void ToolMaskEdit_Click(object sender, EventArgs e)
        {
            toolbarMask.Hide();

            toolPanel.Location = toolbarMask.Location;
            toolPanel.Show();
            toolPanel.BringToFront();
        }

        private void HideEdit()
        {
            current.Reset();
            current.Shape = DrawShapes.Rectangle;

            toolPanel.Hide();

            toolbar.Hide();

            toolbarMask.Show();
            toolbarMask.BringToFront();
        }

        private void Toolbar_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (!(e.ClickedItem is ToolStripButton item))
            {
                return;
            }
            var type = item.Tag;
            if (type == null)
            {
                // 为空表示绘制类型按钮
                return;
            }
            if (activeToolShapeType != null)
            {
                activeToolShapeType.Checked = false;
            }
            activeToolShapeType = item;
            item.Checked = true;
            current.Shape = (DrawShapes)Enum.Parse(typeof(DrawShapes), type.ToString());
            if (current.Shape == DrawShapes.Text)
            {
                toolbarLineType.Hide();
                toolLineWidth.Hide();
                toolbarTextStyle.Show();
            }
            else
            {
                toolbarTextStyle.Hide();
                toolbarLineType.Show();
                toolLineWidth.Show();
            }
        }

        private Bitmap GetResult()
        {
            return editor.GetResult();
        }

        private void ToolOK_Click(object sender, System.EventArgs e)
        {
            Clipboard.SetImage(GetResult());
            CloseForm();
        }

        private void ToolSave_Click(object sender, System.EventArgs e)
        {
            using (var result = new SaveFileDialog
            {
                AddExtension = true,
                DefaultExt = ".png"
            })
            {
                if (result.ShowDialog() != DialogResult.OK)
                {
                    return;
                }
                var img = GetResult();
                img.Save(result.FileName);
            }
            CloseForm();
        }

        private void ToolCancel_Click(object sender, System.EventArgs e)
        {
            CloseForm();
        }

        private void ToolbarColor_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var item = (ToolStripButton)e.ClickedItem;

            if (activeToolColor != null)
            {
                activeToolColor.Checked = false;
            }
            activeToolColor = item;
            item.Checked = true;
            if (item.Tag == null)
            {
                //current.Color = item.BackColor;
            }
            else
            {
                // 选择颜色
                var dialog = new ColorDialog
                {
                    Color = item.BackColor,
                    AllowFullOpen = true
                };

                if (dialog.ShowDialog(this) == DialogResult.OK)
                {
                    //current.Color = dialog.Color;
                }
                dialog.Dispose();
            }
            //toolTextStyle.ForeColor = current.Color;
        }

        private void ToolLineWidth_Scroll(object sender, System.EventArgs e)
        {
            current.Width = toolLineWidth.Value;
        }

        private void ToolbarLineType_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var item = (ToolStripButton)e.ClickedItem;

            if (activeToolLineStyle != null)
            {
                activeToolLineStyle.Checked = false;
            }
            activeToolLineStyle = item;
            item.Checked = true;
            current.LineStyle = (LineStyles)Enum.Parse(typeof(LineStyles), item.Tag.ToString());
        }

        /// <summary>
        /// 设置文字样式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ToolTextStyle_Click(object sender, System.EventArgs e)
        {
            var dialog = new FontDialog
            {
                MinSize = 8,
                MaxSize = 16,
                ShowApply = false,
                ShowColor = false,
                ShowEffects = true,
                FontMustExist = true
            };
            if (dialog.ShowDialog(this) != DialogResult.OK)
            {
                return;
            }

            current.TextFont = toolTextStyle.Font = dialog.Font;
        }
    }
}
