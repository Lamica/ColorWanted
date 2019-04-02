﻿namespace ColorWanted
{
    partial class PreviewForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer componentsSet = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (componentsSet != null))
            {
                componentsSet.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void componentsLayout()
        {
            i18n.I18nManager resources = new i18n.I18nManager(typeof(PreviewForm));
            this.picPreview = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // picPreview
            // 
            this.picPreview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.picPreview.Location = new System.Drawing.Point(0, 0);
            this.picPreview.Margin = new System.Windows.Forms.Padding(0);
            resources.ApplyResources(this.picPreview, "picPreview");
            this.picPreview.Name = "picPreview";
            this.picPreview.Size = new System.Drawing.Size(121, 121);
            this.picPreview.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPreview.TabIndex = 0;
            this.picPreview.TabStop = false;
            this.picPreview.MouseClick += new System.Windows.Forms.MouseEventHandler(this.picPreview_MouseClick);
            this.picPreview.MouseDown += new System.Windows.Forms.MouseEventHandler(this.picPreview_MouseDown);
            this.picPreview.MouseEnter += new System.EventHandler(this.PreviewForm_MouseEnter);
            this.picPreview.MouseLeave += new System.EventHandler(this.PreviewForm_MouseLeave);
            // 
            // PreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(121, 121);
            this.ControlBox = false;
            this.Controls.Add(this.picPreview);
            this.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximumSize = new System.Drawing.Size(429, 429);
            this.MinimumSize = new System.Drawing.Size(77, 77);
            this.Name = "PreviewForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.PreviewForm_Load);
            this.LocationChanged += new System.EventHandler(this.PreviewForm_LocationChanged);
            ((System.ComponentModel.ISupportInitialize)(this.picPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.PictureBox picPreview;
    }
}