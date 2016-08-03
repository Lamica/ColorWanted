﻿namespace ColorWanted
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.pnProxy = new System.Windows.Forms.Panel();
            this.btnExit = new System.Windows.Forms.Label();
            this.lbHex = new System.Windows.Forms.Label();
            this.lbRgb = new System.Windows.Forms.Label();
            this.tray = new System.Windows.Forms.NotifyIcon(this.components);
            this.trayMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.visibleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pnProxy.SuspendLayout();
            this.trayMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnProxy
            // 
            this.pnProxy.BackColor = System.Drawing.Color.White;
            this.pnProxy.Controls.Add(this.btnExit);
            this.pnProxy.Controls.Add(this.lbHex);
            this.pnProxy.Controls.Add(this.lbRgb);
            this.pnProxy.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnProxy.Location = new System.Drawing.Point(0, 0);
            this.pnProxy.Name = "pnProxy";
            this.pnProxy.Size = new System.Drawing.Size(283, 36);
            this.pnProxy.TabIndex = 0;
            this.pnProxy.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDownEventHandler);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExit.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnExit.ForeColor = System.Drawing.Color.White;
            this.btnExit.Location = new System.Drawing.Point(260, 7);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(20, 20);
            this.btnExit.TabIndex = 1;
            this.btnExit.Text = "×";
            this.btnExit.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lbHex
            // 
            this.lbHex.AutoSize = true;
            this.lbHex.BackColor = System.Drawing.Color.Black;
            this.lbHex.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbHex.ForeColor = System.Drawing.Color.Lime;
            this.lbHex.Location = new System.Drawing.Point(12, 7);
            this.lbHex.Name = "lbHex";
            this.lbHex.Padding = new System.Windows.Forms.Padding(2);
            this.lbHex.Size = new System.Drawing.Size(68, 20);
            this.lbHex.TabIndex = 0;
            this.lbHex.Text = "#FFFFFF";
            this.lbHex.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbHex.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDownEventHandler);
            // 
            // lbRgb
            // 
            this.lbRgb.AutoSize = true;
            this.lbRgb.BackColor = System.Drawing.Color.Black;
            this.lbRgb.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRgb.ForeColor = System.Drawing.Color.Lime;
            this.lbRgb.Location = new System.Drawing.Point(98, 7);
            this.lbRgb.Name = "lbRgb";
            this.lbRgb.Padding = new System.Windows.Forms.Padding(2);
            this.lbRgb.Size = new System.Drawing.Size(140, 20);
            this.lbRgb.TabIndex = 0;
            this.lbRgb.Text = "RGB(255,255,255)";
            this.lbRgb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbRgb.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDownEventHandler);
            // 
            // tray
            // 
            this.tray.BalloonTipText = "屏幕取色器正在工作";
            this.tray.BalloonTipTitle = "赏色";
            this.tray.ContextMenuStrip = this.trayMenu;
            this.tray.Icon = ((System.Drawing.Icon)(resources.GetObject("tray.Icon")));
            this.tray.Text = "赏色-取色器";
            this.tray.Visible = true;
            // 
            // trayMenu
            // 
            this.trayMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.visibleToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.trayMenu.Name = "trayMenu";
            this.trayMenu.Size = new System.Drawing.Size(115, 76);
            // 
            // visibleToolStripMenuItem
            // 
            this.visibleToolStripMenuItem.Checked = true;
            this.visibleToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.visibleToolStripMenuItem.Name = "visibleToolStripMenuItem";
            this.visibleToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.visibleToolStripMenuItem.Text = "Visible";
            this.visibleToolStripMenuItem.CheckedChanged += new System.EventHandler(this.visibleToolStripMenuItem_CheckedChanged);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(111, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(114, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(283, 36);
            this.ControlBox = false;
            this.Controls.Add(this.pnProxy);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Opacity = 0.8D;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "赏色";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.MouseDownEventHandler);
            this.pnProxy.ResumeLayout(false);
            this.pnProxy.PerformLayout();
            this.trayMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnProxy;
        private System.Windows.Forms.Label lbRgb;
        private System.Windows.Forms.Label lbHex;
        private System.Windows.Forms.Label btnExit;
        private System.Windows.Forms.NotifyIcon tray;
        private System.Windows.Forms.ContextMenuStrip trayMenu;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem visibleToolStripMenuItem;
    }
}

