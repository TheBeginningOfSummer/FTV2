namespace FTV2
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
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.HTP主界面 = new ReaLTaiizor.Controls.HopeTabPage();
            this.TP主界面 = new System.Windows.Forms.TabPage();
            this.RTB信息 = new System.Windows.Forms.RichTextBox();
            this.CTS菜单 = new ReaLTaiizor.Controls.CrownToolStrip();
            this.TSB导入 = new System.Windows.Forms.ToolStripButton();
            this.TSB测试 = new System.Windows.Forms.ToolStripButton();
            this.TP上料 = new System.Windows.Forms.TabPage();
            this.TP测试 = new System.Windows.Forms.TabPage();
            this.TPIO信息 = new System.Windows.Forms.TabPage();
            this.TP示教 = new System.Windows.Forms.TabPage();
            this.FSB状态 = new ReaLTaiizor.Controls.ForeverStatusBar();
            this.HTP主界面.SuspendLayout();
            this.TP主界面.SuspendLayout();
            this.CTS菜单.SuspendLayout();
            this.SuspendLayout();
            // 
            // HTP主界面
            // 
            this.HTP主界面.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(55)))), ((int)(((byte)(66)))));
            this.HTP主界面.Controls.Add(this.TP主界面);
            this.HTP主界面.Controls.Add(this.TP上料);
            this.HTP主界面.Controls.Add(this.TP测试);
            this.HTP主界面.Controls.Add(this.TPIO信息);
            this.HTP主界面.Controls.Add(this.TP示教);
            this.HTP主界面.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HTP主界面.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.HTP主界面.ForeColorA = System.Drawing.Color.Silver;
            this.HTP主界面.ForeColorB = System.Drawing.Color.Gray;
            this.HTP主界面.ForeColorC = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.HTP主界面.ItemSize = new System.Drawing.Size(120, 40);
            this.HTP主界面.Location = new System.Drawing.Point(0, 0);
            this.HTP主界面.Name = "HTP主界面";
            this.HTP主界面.PixelOffsetType = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
            this.HTP主界面.SelectedIndex = 0;
            this.HTP主界面.Size = new System.Drawing.Size(1344, 681);
            this.HTP主界面.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.HTP主界面.SmoothingType = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            this.HTP主界面.TabIndex = 0;
            this.HTP主界面.TextRenderingType = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            this.HTP主界面.ThemeColorA = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.HTP主界面.ThemeColorB = System.Drawing.Color.FromArgb(((int)(((byte)(150)))), ((int)(((byte)(64)))), ((int)(((byte)(158)))), ((int)(((byte)(255)))));
            this.HTP主界面.TitleTextState = ReaLTaiizor.Controls.HopeTabPage.TextState.Normal;
            // 
            // TP主界面
            // 
            this.TP主界面.Controls.Add(this.RTB信息);
            this.TP主界面.Controls.Add(this.CTS菜单);
            this.TP主界面.Location = new System.Drawing.Point(0, 40);
            this.TP主界面.Name = "TP主界面";
            this.TP主界面.Padding = new System.Windows.Forms.Padding(3);
            this.TP主界面.Size = new System.Drawing.Size(1344, 641);
            this.TP主界面.TabIndex = 0;
            this.TP主界面.Text = "主界面";
            this.TP主界面.UseVisualStyleBackColor = true;
            // 
            // RTB信息
            // 
            this.RTB信息.BackColor = System.Drawing.Color.Gainsboro;
            this.RTB信息.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RTB信息.Location = new System.Drawing.Point(989, 49);
            this.RTB信息.Name = "RTB信息";
            this.RTB信息.Size = new System.Drawing.Size(329, 98);
            this.RTB信息.TabIndex = 1;
            this.RTB信息.Text = "";
            // 
            // CTS菜单
            // 
            this.CTS菜单.AutoSize = false;
            this.CTS菜单.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.CTS菜单.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.CTS菜单.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TSB导入,
            this.TSB测试});
            this.CTS菜单.Location = new System.Drawing.Point(3, 3);
            this.CTS菜单.Name = "CTS菜单";
            this.CTS菜单.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
            this.CTS菜单.Size = new System.Drawing.Size(1338, 28);
            this.CTS菜单.TabIndex = 0;
            this.CTS菜单.Text = "菜单";
            // 
            // TSB导入
            // 
            this.TSB导入.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TSB导入.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.TSB导入.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TSB导入.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.TSB导入.Image = ((System.Drawing.Image)(resources.GetObject("TSB导入.Image")));
            this.TSB导入.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB导入.Name = "TSB导入";
            this.TSB导入.Size = new System.Drawing.Size(36, 25);
            this.TSB导入.Text = "导入";
            this.TSB导入.Click += new System.EventHandler(this.TSB导入_Click);
            // 
            // TSB测试
            // 
            this.TSB测试.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.TSB测试.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TSB测试.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.TSB测试.Image = ((System.Drawing.Image)(resources.GetObject("TSB测试.Image")));
            this.TSB测试.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB测试.Name = "TSB测试";
            this.TSB测试.Size = new System.Drawing.Size(36, 25);
            this.TSB测试.Text = "测试";
            this.TSB测试.Click += new System.EventHandler(this.TSB测试_Click);
            // 
            // TP上料
            // 
            this.TP上料.Location = new System.Drawing.Point(0, 40);
            this.TP上料.Name = "TP上料";
            this.TP上料.Padding = new System.Windows.Forms.Padding(3);
            this.TP上料.Size = new System.Drawing.Size(1344, 641);
            this.TP上料.TabIndex = 1;
            this.TP上料.Text = "上料";
            this.TP上料.UseVisualStyleBackColor = true;
            // 
            // TP测试
            // 
            this.TP测试.Location = new System.Drawing.Point(0, 40);
            this.TP测试.Name = "TP测试";
            this.TP测试.Size = new System.Drawing.Size(1344, 641);
            this.TP测试.TabIndex = 2;
            this.TP测试.Text = "测试";
            this.TP测试.UseVisualStyleBackColor = true;
            // 
            // TPIO信息
            // 
            this.TPIO信息.Location = new System.Drawing.Point(0, 40);
            this.TPIO信息.Name = "TPIO信息";
            this.TPIO信息.Size = new System.Drawing.Size(1344, 641);
            this.TPIO信息.TabIndex = 3;
            this.TPIO信息.Text = "I/O信息";
            this.TPIO信息.UseVisualStyleBackColor = true;
            // 
            // TP示教
            // 
            this.TP示教.Location = new System.Drawing.Point(0, 40);
            this.TP示教.Name = "TP示教";
            this.TP示教.Size = new System.Drawing.Size(1344, 641);
            this.TP示教.TabIndex = 4;
            this.TP示教.Text = "示教";
            this.TP示教.UseVisualStyleBackColor = true;
            // 
            // FSB状态
            // 
            this.FSB状态.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.FSB状态.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FSB状态.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.FSB状态.ForeColor = System.Drawing.Color.White;
            this.FSB状态.Location = new System.Drawing.Point(0, 658);
            this.FSB状态.Name = "FSB状态";
            this.FSB状态.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.FSB状态.ShowTimeDate = true;
            this.FSB状态.Size = new System.Drawing.Size(1344, 23);
            this.FSB状态.TabIndex = 1;
            this.FSB状态.Text = "状态";
            this.FSB状态.TextColor = System.Drawing.Color.White;
            this.FSB状态.TimeColor = System.Drawing.Color.White;
            this.FSB状态.TimeFormat = "yyyy.MM.dd - HH:mm:ss";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1344, 681);
            this.Controls.Add(this.FSB状态);
            this.Controls.Add(this.HTP主界面);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FT";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.HTP主界面.ResumeLayout(false);
            this.TP主界面.ResumeLayout(false);
            this.CTS菜单.ResumeLayout(false);
            this.CTS菜单.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private ReaLTaiizor.Controls.HopeTabPage HTP主界面;
        private System.Windows.Forms.TabPage TP主界面;
        private System.Windows.Forms.TabPage TP上料;
        private ReaLTaiizor.Controls.CrownToolStrip CTS菜单;
        private System.Windows.Forms.ToolStripButton TSB导入;
        private System.Windows.Forms.ToolStripButton TSB测试;
        private System.Windows.Forms.TabPage TP测试;
        private System.Windows.Forms.TabPage TPIO信息;
        private System.Windows.Forms.TabPage TP示教;
        private System.Windows.Forms.RichTextBox RTB信息;
        private ReaLTaiizor.Controls.ForeverStatusBar FSB状态;
    }
}

