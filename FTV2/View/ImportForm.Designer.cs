namespace FTV2.View
{
    partial class ImportForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.BTN保存 = new System.Windows.Forms.Button();
            this.RTB数据 = new System.Windows.Forms.RichTextBox();
            this.CMS右键 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TMI清除 = new System.Windows.Forms.ToolStripMenuItem();
            this.CCB类型 = new ReaLTaiizor.Controls.CrownComboBox();
            this.BTN加载 = new System.Windows.Forms.Button();
            this.TB文件名 = new System.Windows.Forms.TextBox();
            this.ATP控件设置 = new ReaLTaiizor.Controls.AirTabPage();
            this.TP控件信息导入 = new System.Windows.Forms.TabPage();
            this.TP控件位置设置 = new System.Windows.Forms.TabPage();
            this.BTN添加 = new System.Windows.Forms.Button();
            this.LB信息 = new System.Windows.Forms.Label();
            this.PN控件预览 = new System.Windows.Forms.Panel();
            this.BTN位置保存 = new System.Windows.Forms.Button();
            this.PFM菜单 = new ReaLTaiizor.Controls.ParrotFlatMenuStrip();
            this.OFD打开 = new System.Windows.Forms.OpenFileDialog();
            this.CMS右键.SuspendLayout();
            this.ATP控件设置.SuspendLayout();
            this.TP控件信息导入.SuspendLayout();
            this.TP控件位置设置.SuspendLayout();
            this.SuspendLayout();
            // 
            // BTN保存
            // 
            this.BTN保存.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN保存.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BTN保存.Location = new System.Drawing.Point(820, 521);
            this.BTN保存.Name = "BTN保存";
            this.BTN保存.Size = new System.Drawing.Size(75, 22);
            this.BTN保存.TabIndex = 1;
            this.BTN保存.Text = "保存";
            this.BTN保存.UseVisualStyleBackColor = true;
            this.BTN保存.Click += new System.EventHandler(this.BTN保存_Click);
            // 
            // RTB数据
            // 
            this.RTB数据.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.RTB数据.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RTB数据.ContextMenuStrip = this.CMS右键;
            this.RTB数据.Location = new System.Drawing.Point(3, 3);
            this.RTB数据.Margin = new System.Windows.Forms.Padding(0);
            this.RTB数据.Name = "RTB数据";
            this.RTB数据.Size = new System.Drawing.Size(897, 515);
            this.RTB数据.TabIndex = 2;
            this.RTB数据.Text = "";
            // 
            // CMS右键
            // 
            this.CMS右键.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TMI清除});
            this.CMS右键.Name = "CMS右键";
            this.CMS右键.Size = new System.Drawing.Size(101, 26);
            // 
            // TMI清除
            // 
            this.TMI清除.Name = "TMI清除";
            this.TMI清除.Size = new System.Drawing.Size(100, 22);
            this.TMI清除.Text = "清除";
            this.TMI清除.Click += new System.EventHandler(this.TMI清除_Click);
            // 
            // CCB类型
            // 
            this.CCB类型.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CCB类型.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
            this.CCB类型.FormattingEnabled = true;
            this.CCB类型.Items.AddRange(new object[] {
            "Button",
            "TextBox"});
            this.CCB类型.Location = new System.Drawing.Point(693, 521);
            this.CCB类型.Name = "CCB类型";
            this.CCB类型.Size = new System.Drawing.Size(121, 22);
            this.CCB类型.TabIndex = 3;
            // 
            // BTN加载
            // 
            this.BTN加载.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN加载.Location = new System.Drawing.Point(739, 521);
            this.BTN加载.Name = "BTN加载";
            this.BTN加载.Size = new System.Drawing.Size(75, 22);
            this.BTN加载.TabIndex = 4;
            this.BTN加载.Text = "加载";
            this.BTN加载.UseVisualStyleBackColor = true;
            this.BTN加载.Click += new System.EventHandler(this.BTN加载_Click);
            // 
            // TB文件名
            // 
            this.TB文件名.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TB文件名.ForeColor = System.Drawing.Color.Gray;
            this.TB文件名.Location = new System.Drawing.Point(902, 3);
            this.TB文件名.Name = "TB文件名";
            this.TB文件名.Size = new System.Drawing.Size(120, 21);
            this.TB文件名.TabIndex = 5;
            this.TB文件名.Text = "文件名";
            // 
            // ATP控件设置
            // 
            this.ATP控件设置.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.ATP控件设置.BaseColor = System.Drawing.Color.WhiteSmoke;
            this.ATP控件设置.Controls.Add(this.TP控件信息导入);
            this.ATP控件设置.Controls.Add(this.TP控件位置设置);
            this.ATP控件设置.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ATP控件设置.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ATP控件设置.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ATP控件设置.ItemSize = new System.Drawing.Size(30, 115);
            this.ATP控件设置.Location = new System.Drawing.Point(0, 24);
            this.ATP控件设置.Multiline = true;
            this.ATP控件设置.Name = "ATP控件设置";
            this.ATP控件设置.NormalTextColor = System.Drawing.Color.Silver;
            this.ATP控件设置.SelectedIndex = 0;
            this.ATP控件设置.SelectedTabBackColor = System.Drawing.Color.White;
            this.ATP控件设置.SelectedTextColor = System.Drawing.Color.DodgerBlue;
            this.ATP控件设置.ShowOuterBorders = false;
            this.ATP控件设置.Size = new System.Drawing.Size(1026, 556);
            this.ATP控件设置.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.ATP控件设置.SquareColor = System.Drawing.Color.DodgerBlue;
            this.ATP控件设置.TabCursor = System.Windows.Forms.Cursors.Hand;
            this.ATP控件设置.TabIndex = 7;
            // 
            // TP控件信息导入
            // 
            this.TP控件信息导入.BackColor = System.Drawing.Color.White;
            this.TP控件信息导入.Controls.Add(this.RTB数据);
            this.TP控件信息导入.Controls.Add(this.BTN保存);
            this.TP控件信息导入.Controls.Add(this.CCB类型);
            this.TP控件信息导入.Cursor = System.Windows.Forms.Cursors.Default;
            this.TP控件信息导入.Location = new System.Drawing.Point(119, 4);
            this.TP控件信息导入.Name = "TP控件信息导入";
            this.TP控件信息导入.Padding = new System.Windows.Forms.Padding(3);
            this.TP控件信息导入.Size = new System.Drawing.Size(903, 548);
            this.TP控件信息导入.TabIndex = 0;
            this.TP控件信息导入.Text = "控件信息导入";
            // 
            // TP控件位置设置
            // 
            this.TP控件位置设置.BackColor = System.Drawing.Color.White;
            this.TP控件位置设置.Controls.Add(this.BTN添加);
            this.TP控件位置设置.Controls.Add(this.LB信息);
            this.TP控件位置设置.Controls.Add(this.PN控件预览);
            this.TP控件位置设置.Controls.Add(this.BTN加载);
            this.TP控件位置设置.Controls.Add(this.BTN位置保存);
            this.TP控件位置设置.Cursor = System.Windows.Forms.Cursors.Default;
            this.TP控件位置设置.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TP控件位置设置.Location = new System.Drawing.Point(119, 4);
            this.TP控件位置设置.Name = "TP控件位置设置";
            this.TP控件位置设置.Padding = new System.Windows.Forms.Padding(3);
            this.TP控件位置设置.Size = new System.Drawing.Size(903, 548);
            this.TP控件位置设置.TabIndex = 1;
            this.TP控件位置设置.Text = "控件位置设置";
            // 
            // BTN添加
            // 
            this.BTN添加.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN添加.Location = new System.Drawing.Point(658, 521);
            this.BTN添加.Name = "BTN添加";
            this.BTN添加.Size = new System.Drawing.Size(75, 22);
            this.BTN添加.TabIndex = 8;
            this.BTN添加.Text = "添加";
            this.BTN添加.UseVisualStyleBackColor = true;
            this.BTN添加.Click += new System.EventHandler(this.BTN添加_Click);
            // 
            // LB信息
            // 
            this.LB信息.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.LB信息.AutoSize = true;
            this.LB信息.Location = new System.Drawing.Point(6, 526);
            this.LB信息.Name = "LB信息";
            this.LB信息.Size = new System.Drawing.Size(29, 12);
            this.LB信息.TabIndex = 7;
            this.LB信息.Text = "信息";
            // 
            // PN控件预览
            // 
            this.PN控件预览.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PN控件预览.AutoScroll = true;
            this.PN控件预览.Location = new System.Drawing.Point(4, 4);
            this.PN控件预览.Margin = new System.Windows.Forms.Padding(1);
            this.PN控件预览.Name = "PN控件预览";
            this.PN控件预览.Size = new System.Drawing.Size(895, 513);
            this.PN控件预览.TabIndex = 6;
            // 
            // BTN位置保存
            // 
            this.BTN位置保存.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.BTN位置保存.Location = new System.Drawing.Point(820, 521);
            this.BTN位置保存.Name = "BTN位置保存";
            this.BTN位置保存.Size = new System.Drawing.Size(75, 22);
            this.BTN位置保存.TabIndex = 5;
            this.BTN位置保存.Text = "保存";
            this.BTN位置保存.UseVisualStyleBackColor = true;
            this.BTN位置保存.Click += new System.EventHandler(this.BTN位置保存_Click);
            // 
            // PFM菜单
            // 
            this.PFM菜单.BackColor = System.Drawing.Color.WhiteSmoke;
            this.PFM菜单.HoverBackColor = System.Drawing.Color.RoyalBlue;
            this.PFM菜单.HoverTextColor = System.Drawing.Color.White;
            this.PFM菜单.ItemBackColor = System.Drawing.Color.DodgerBlue;
            this.PFM菜单.Location = new System.Drawing.Point(0, 0);
            this.PFM菜单.Name = "PFM菜单";
            this.PFM菜单.SelectedBackColor = System.Drawing.Color.DarkOrchid;
            this.PFM菜单.SelectedTextColor = System.Drawing.Color.White;
            this.PFM菜单.SeparatorColor = System.Drawing.Color.White;
            this.PFM菜单.Size = new System.Drawing.Size(1026, 24);
            this.PFM菜单.TabIndex = 8;
            this.PFM菜单.Text = "parrotFlatMenuStrip1";
            this.PFM菜单.TextColor = System.Drawing.Color.White;
            // 
            // OFD打开
            // 
            this.OFD打开.FileName = "openFileDialog1";
            // 
            // ImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1026, 580);
            this.Controls.Add(this.ATP控件设置);
            this.Controls.Add(this.TB文件名);
            this.Controls.Add(this.PFM菜单);
            this.Name = "ImportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ImportForm";
            this.CMS右键.ResumeLayout(false);
            this.ATP控件设置.ResumeLayout(false);
            this.TP控件信息导入.ResumeLayout(false);
            this.TP控件位置设置.ResumeLayout(false);
            this.TP控件位置设置.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button BTN保存;
        private System.Windows.Forms.RichTextBox RTB数据;
        private ReaLTaiizor.Controls.CrownComboBox CCB类型;
        private System.Windows.Forms.Button BTN加载;
        private System.Windows.Forms.TextBox TB文件名;
        private System.Windows.Forms.ContextMenuStrip CMS右键;
        private System.Windows.Forms.ToolStripMenuItem TMI清除;
        private ReaLTaiizor.Controls.AirTabPage ATP控件设置;
        private System.Windows.Forms.TabPage TP控件信息导入;
        private System.Windows.Forms.TabPage TP控件位置设置;
        private System.Windows.Forms.Button BTN位置保存;
        private ReaLTaiizor.Controls.ParrotFlatMenuStrip PFM菜单;
        private System.Windows.Forms.OpenFileDialog OFD打开;
        private System.Windows.Forms.Panel PN控件预览;
        private System.Windows.Forms.Label LB信息;
        private System.Windows.Forms.Button BTN添加;
    }
}