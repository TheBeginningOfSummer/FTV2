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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImportForm));
            this.RTB数据 = new System.Windows.Forms.RichTextBox();
            this.CMS导入数据 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TMI清除导入数据 = new System.Windows.Forms.ToolStripMenuItem();
            this.CMS设置右键 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.TMI清除 = new System.Windows.Forms.ToolStripMenuItem();
            this.TMI选择 = new System.Windows.Forms.ToolStripMenuItem();
            this.TMI添加 = new System.Windows.Forms.ToolStripMenuItem();
            this.TMI添加按钮 = new System.Windows.Forms.ToolStripMenuItem();
            this.TMI添加标签 = new System.Windows.Forms.ToolStripMenuItem();
            this.TMI添加文本 = new System.Windows.Forms.ToolStripMenuItem();
            this.TMI添加分组 = new System.Windows.Forms.ToolStripMenuItem();
            this.TMI设置 = new System.Windows.Forms.ToolStripMenuItem();
            this.TMI宽高 = new System.Windows.Forms.ToolStripMenuItem();
            this.TMI文本 = new System.Windows.Forms.ToolStripMenuItem();
            this.TMItag = new System.Windows.Forms.ToolStripMenuItem();
            this.TMI字体大小 = new System.Windows.Forms.ToolStripMenuItem();
            this.ATP控件设置 = new ReaLTaiizor.Controls.AirTabPage();
            this.TP控件信息导入 = new System.Windows.Forms.TabPage();
            this.TP控件位置设置 = new System.Windows.Forms.TabPage();
            this.PN控件预览 = new System.Windows.Forms.Panel();
            this.OFD打开 = new System.Windows.Forms.OpenFileDialog();
            this.FSB状态 = new ReaLTaiizor.Controls.ForeverStatusBar();
            this.CTS菜单 = new ReaLTaiizor.Controls.CrownToolStrip();
            this.TTB文件名 = new System.Windows.Forms.ToolStripTextBox();
            this.TSB保存 = new System.Windows.Forms.ToolStripButton();
            this.TSB加载 = new System.Windows.Forms.ToolStripButton();
            this.TSL初始位置 = new System.Windows.Forms.ToolStripLabel();
            this.TTB初始位置 = new System.Windows.Forms.ToolStripTextBox();
            this.TSL偏移量 = new System.Windows.Forms.ToolStripLabel();
            this.TTB偏移量 = new System.Windows.Forms.ToolStripTextBox();
            this.TSL宽高 = new System.Windows.Forms.ToolStripLabel();
            this.TCB宽高 = new System.Windows.Forms.ToolStripComboBox();
            this.TSL行数 = new System.Windows.Forms.ToolStripLabel();
            this.TTB行数 = new System.Windows.Forms.ToolStripTextBox();
            this.TSB导入 = new System.Windows.Forms.ToolStripButton();
            this.CMS导入数据.SuspendLayout();
            this.CMS设置右键.SuspendLayout();
            this.ATP控件设置.SuspendLayout();
            this.TP控件信息导入.SuspendLayout();
            this.TP控件位置设置.SuspendLayout();
            this.CTS菜单.SuspendLayout();
            this.SuspendLayout();
            // 
            // RTB数据
            // 
            this.RTB数据.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RTB数据.ContextMenuStrip = this.CMS导入数据;
            this.RTB数据.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RTB数据.Location = new System.Drawing.Point(3, 3);
            this.RTB数据.Margin = new System.Windows.Forms.Padding(0);
            this.RTB数据.Name = "RTB数据";
            this.RTB数据.Size = new System.Drawing.Size(1284, 681);
            this.RTB数据.TabIndex = 2;
            this.RTB数据.Text = "";
            // 
            // CMS导入数据
            // 
            this.CMS导入数据.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TMI清除导入数据});
            this.CMS导入数据.Name = "CMS导入数据";
            this.CMS导入数据.Size = new System.Drawing.Size(101, 26);
            // 
            // TMI清除导入数据
            // 
            this.TMI清除导入数据.Name = "TMI清除导入数据";
            this.TMI清除导入数据.Size = new System.Drawing.Size(100, 22);
            this.TMI清除导入数据.Text = "清除";
            this.TMI清除导入数据.Click += new System.EventHandler(this.TMI清除导入数据_Click);
            // 
            // CMS设置右键
            // 
            this.CMS设置右键.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TMI清除,
            this.TMI选择,
            this.TMI添加,
            this.TMI设置});
            this.CMS设置右键.Name = "CMS右键";
            this.CMS设置右键.Size = new System.Drawing.Size(101, 92);
            // 
            // TMI清除
            // 
            this.TMI清除.Name = "TMI清除";
            this.TMI清除.Size = new System.Drawing.Size(180, 22);
            this.TMI清除.Text = "清除";
            this.TMI清除.Click += new System.EventHandler(this.TMI清除_Click);
            // 
            // TMI选择
            // 
            this.TMI选择.Name = "TMI选择";
            this.TMI选择.Size = new System.Drawing.Size(180, 22);
            this.TMI选择.Text = "选择";
            this.TMI选择.Click += new System.EventHandler(this.TMI选择_Click);
            // 
            // TMI添加
            // 
            this.TMI添加.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TMI添加按钮,
            this.TMI添加标签,
            this.TMI添加文本,
            this.TMI添加分组});
            this.TMI添加.Name = "TMI添加";
            this.TMI添加.Size = new System.Drawing.Size(180, 22);
            this.TMI添加.Tag = "all";
            this.TMI添加.Text = "添加";
            // 
            // TMI添加按钮
            // 
            this.TMI添加按钮.Name = "TMI添加按钮";
            this.TMI添加按钮.Size = new System.Drawing.Size(100, 22);
            this.TMI添加按钮.Tag = "button";
            this.TMI添加按钮.Text = "按钮";
            this.TMI添加按钮.Click += new System.EventHandler(this.TMI添加_Click);
            // 
            // TMI添加标签
            // 
            this.TMI添加标签.Name = "TMI添加标签";
            this.TMI添加标签.Size = new System.Drawing.Size(100, 22);
            this.TMI添加标签.Tag = "label";
            this.TMI添加标签.Text = "标签";
            this.TMI添加标签.Click += new System.EventHandler(this.TMI添加_Click);
            // 
            // TMI添加文本
            // 
            this.TMI添加文本.Name = "TMI添加文本";
            this.TMI添加文本.Size = new System.Drawing.Size(100, 22);
            this.TMI添加文本.Tag = "textbox";
            this.TMI添加文本.Text = "文本";
            this.TMI添加文本.Click += new System.EventHandler(this.TMI添加_Click);
            // 
            // TMI添加分组
            // 
            this.TMI添加分组.Name = "TMI添加分组";
            this.TMI添加分组.Size = new System.Drawing.Size(100, 22);
            this.TMI添加分组.Tag = "group";
            this.TMI添加分组.Text = "分组";
            this.TMI添加分组.Click += new System.EventHandler(this.TMI添加_Click);
            // 
            // TMI设置
            // 
            this.TMI设置.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TMI宽高,
            this.TMI文本,
            this.TMItag,
            this.TMI字体大小});
            this.TMI设置.Name = "TMI设置";
            this.TMI设置.Size = new System.Drawing.Size(180, 22);
            this.TMI设置.Text = "设置";
            // 
            // TMI宽高
            // 
            this.TMI宽高.Name = "TMI宽高";
            this.TMI宽高.Size = new System.Drawing.Size(180, 22);
            this.TMI宽高.Tag = "size";
            this.TMI宽高.Text = "宽高";
            this.TMI宽高.Click += new System.EventHandler(this.TMI设置_Click);
            // 
            // TMI文本
            // 
            this.TMI文本.Name = "TMI文本";
            this.TMI文本.Size = new System.Drawing.Size(180, 22);
            this.TMI文本.Tag = "text";
            this.TMI文本.Text = "文本";
            this.TMI文本.Click += new System.EventHandler(this.TMI设置_Click);
            // 
            // TMItag
            // 
            this.TMItag.Name = "TMItag";
            this.TMItag.Size = new System.Drawing.Size(180, 22);
            this.TMItag.Tag = "tag";
            this.TMItag.Text = "Tag";
            this.TMItag.Click += new System.EventHandler(this.TMI设置_Click);
            // 
            // TMI字体大小
            // 
            this.TMI字体大小.Name = "TMI字体大小";
            this.TMI字体大小.Size = new System.Drawing.Size(180, 22);
            this.TMI字体大小.Tag = "fontsize";
            this.TMI字体大小.Text = "字体大小";
            this.TMI字体大小.Click += new System.EventHandler(this.TMI设置_Click);
            // 
            // ATP控件设置
            // 
            this.ATP控件设置.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.ATP控件设置.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ATP控件设置.BaseColor = System.Drawing.Color.WhiteSmoke;
            this.ATP控件设置.Controls.Add(this.TP控件信息导入);
            this.ATP控件设置.Controls.Add(this.TP控件位置设置);
            this.ATP控件设置.Cursor = System.Windows.Forms.Cursors.Hand;
            this.ATP控件设置.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ATP控件设置.ItemSize = new System.Drawing.Size(30, 115);
            this.ATP控件设置.Location = new System.Drawing.Point(0, 28);
            this.ATP控件设置.Margin = new System.Windows.Forms.Padding(0);
            this.ATP控件设置.Multiline = true;
            this.ATP控件设置.Name = "ATP控件设置";
            this.ATP控件设置.NormalTextColor = System.Drawing.Color.Silver;
            this.ATP控件设置.SelectedIndex = 0;
            this.ATP控件设置.SelectedTabBackColor = System.Drawing.Color.White;
            this.ATP控件设置.SelectedTextColor = System.Drawing.Color.DodgerBlue;
            this.ATP控件设置.ShowOuterBorders = false;
            this.ATP控件设置.Size = new System.Drawing.Size(1413, 695);
            this.ATP控件设置.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.ATP控件设置.SquareColor = System.Drawing.Color.DodgerBlue;
            this.ATP控件设置.TabCursor = System.Windows.Forms.Cursors.Hand;
            this.ATP控件设置.TabIndex = 7;
            // 
            // TP控件信息导入
            // 
            this.TP控件信息导入.BackColor = System.Drawing.Color.White;
            this.TP控件信息导入.Controls.Add(this.RTB数据);
            this.TP控件信息导入.Cursor = System.Windows.Forms.Cursors.Default;
            this.TP控件信息导入.Location = new System.Drawing.Point(119, 4);
            this.TP控件信息导入.Name = "TP控件信息导入";
            this.TP控件信息导入.Padding = new System.Windows.Forms.Padding(3);
            this.TP控件信息导入.Size = new System.Drawing.Size(1290, 687);
            this.TP控件信息导入.TabIndex = 0;
            this.TP控件信息导入.Text = "控件信息导入";
            // 
            // TP控件位置设置
            // 
            this.TP控件位置设置.BackColor = System.Drawing.Color.White;
            this.TP控件位置设置.Controls.Add(this.PN控件预览);
            this.TP控件位置设置.Cursor = System.Windows.Forms.Cursors.Default;
            this.TP控件位置设置.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TP控件位置设置.Location = new System.Drawing.Point(119, 4);
            this.TP控件位置设置.Name = "TP控件位置设置";
            this.TP控件位置设置.Padding = new System.Windows.Forms.Padding(3);
            this.TP控件位置设置.Size = new System.Drawing.Size(1290, 687);
            this.TP控件位置设置.TabIndex = 1;
            this.TP控件位置设置.Text = "控件位置设置";
            // 
            // PN控件预览
            // 
            this.PN控件预览.AutoScroll = true;
            this.PN控件预览.ContextMenuStrip = this.CMS设置右键;
            this.PN控件预览.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PN控件预览.Location = new System.Drawing.Point(3, 3);
            this.PN控件预览.Margin = new System.Windows.Forms.Padding(0);
            this.PN控件预览.Name = "PN控件预览";
            this.PN控件预览.Size = new System.Drawing.Size(1284, 681);
            this.PN控件预览.TabIndex = 6;
            // 
            // OFD打开
            // 
            this.OFD打开.FileName = "openFileDialog1";
            // 
            // FSB状态
            // 
            this.FSB状态.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(45)))), ((int)(((byte)(47)))), ((int)(((byte)(49)))));
            this.FSB状态.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.FSB状态.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.FSB状态.ForeColor = System.Drawing.Color.White;
            this.FSB状态.Location = new System.Drawing.Point(0, 726);
            this.FSB状态.Name = "FSB状态";
            this.FSB状态.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(35)))), ((int)(((byte)(168)))), ((int)(((byte)(109)))));
            this.FSB状态.ShowTimeDate = false;
            this.FSB状态.Size = new System.Drawing.Size(1413, 23);
            this.FSB状态.TabIndex = 9;
            this.FSB状态.Text = "状态";
            this.FSB状态.TextColor = System.Drawing.Color.White;
            this.FSB状态.TimeColor = System.Drawing.Color.White;
            this.FSB状态.TimeFormat = "dd.MM.yyyy - HH:mm:ss";
            // 
            // CTS菜单
            // 
            this.CTS菜单.AutoSize = false;
            this.CTS菜单.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.CTS菜单.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.CTS菜单.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TTB文件名,
            this.TSB保存,
            this.TSB加载,
            this.TSL初始位置,
            this.TTB初始位置,
            this.TSL偏移量,
            this.TTB偏移量,
            this.TSL宽高,
            this.TCB宽高,
            this.TSL行数,
            this.TTB行数,
            this.TSB导入});
            this.CTS菜单.Location = new System.Drawing.Point(0, 0);
            this.CTS菜单.Name = "CTS菜单";
            this.CTS菜单.Padding = new System.Windows.Forms.Padding(5, 0, 1, 0);
            this.CTS菜单.Size = new System.Drawing.Size(1413, 28);
            this.CTS菜单.TabIndex = 10;
            this.CTS菜单.Text = "CTS菜单";
            // 
            // TTB文件名
            // 
            this.TTB文件名.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TTB文件名.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.TTB文件名.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.TTB文件名.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.TTB文件名.Name = "TTB文件名";
            this.TTB文件名.Size = new System.Drawing.Size(100, 28);
            this.TTB文件名.Text = "文件名";
            // 
            // TSB保存
            // 
            this.TSB保存.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TSB保存.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.TSB保存.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TSB保存.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.TSB保存.Image = ((System.Drawing.Image)(resources.GetObject("TSB保存.Image")));
            this.TSB保存.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB保存.Name = "TSB保存";
            this.TSB保存.Size = new System.Drawing.Size(36, 25);
            this.TSB保存.Text = "保存";
            this.TSB保存.Click += new System.EventHandler(this.BTN保存_Click);
            // 
            // TSB加载
            // 
            this.TSB加载.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.TSB加载.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.TSB加载.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TSB加载.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.TSB加载.Image = ((System.Drawing.Image)(resources.GetObject("TSB加载.Image")));
            this.TSB加载.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB加载.Name = "TSB加载";
            this.TSB加载.Size = new System.Drawing.Size(36, 25);
            this.TSB加载.Text = "加载";
            this.TSB加载.Click += new System.EventHandler(this.BTN加载_Click);
            // 
            // TSL初始位置
            // 
            this.TSL初始位置.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.TSL初始位置.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.TSL初始位置.Name = "TSL初始位置";
            this.TSL初始位置.Size = new System.Drawing.Size(56, 25);
            this.TSL初始位置.Text = "初始位置";
            // 
            // TTB初始位置
            // 
            this.TTB初始位置.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.TTB初始位置.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.TTB初始位置.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.TTB初始位置.Name = "TTB初始位置";
            this.TTB初始位置.Size = new System.Drawing.Size(80, 28);
            this.TTB初始位置.Text = "15:20";
            // 
            // TSL偏移量
            // 
            this.TSL偏移量.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.TSL偏移量.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.TSL偏移量.Name = "TSL偏移量";
            this.TSL偏移量.Size = new System.Drawing.Size(44, 25);
            this.TSL偏移量.Text = "偏移量";
            // 
            // TTB偏移量
            // 
            this.TTB偏移量.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.TTB偏移量.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.TTB偏移量.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.TTB偏移量.Name = "TTB偏移量";
            this.TTB偏移量.Size = new System.Drawing.Size(80, 28);
            this.TTB偏移量.Text = "150:30";
            // 
            // TSL宽高
            // 
            this.TSL宽高.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.TSL宽高.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.TSL宽高.Name = "TSL宽高";
            this.TSL宽高.Size = new System.Drawing.Size(32, 25);
            this.TSL宽高.Text = "宽高";
            // 
            // TCB宽高
            // 
            this.TCB宽高.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.TCB宽高.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.TCB宽高.Items.AddRange(new object[] {
            "40:24",
            "100:24",
            "230:220",
            "230:330"});
            this.TCB宽高.Name = "TCB宽高";
            this.TCB宽高.Size = new System.Drawing.Size(100, 28);
            this.TCB宽高.Text = "40:24";
            // 
            // TSL行数
            // 
            this.TSL行数.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.TSL行数.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.TSL行数.Name = "TSL行数";
            this.TSL行数.Size = new System.Drawing.Size(32, 25);
            this.TSL行数.Text = "行数";
            // 
            // TTB行数
            // 
            this.TTB行数.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.TTB行数.Font = new System.Drawing.Font("Microsoft YaHei UI", 9F);
            this.TTB行数.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.TTB行数.Name = "TTB行数";
            this.TTB行数.Size = new System.Drawing.Size(50, 28);
            this.TTB行数.Text = "9";
            // 
            // TSB导入
            // 
            this.TSB导入.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(63)))), ((int)(((byte)(65)))));
            this.TSB导入.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TSB导入.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(220)))), ((int)(((byte)(220)))));
            this.TSB导入.Image = ((System.Drawing.Image)(resources.GetObject("TSB导入.Image")));
            this.TSB导入.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.TSB导入.Name = "TSB导入";
            this.TSB导入.Size = new System.Drawing.Size(36, 25);
            this.TSB导入.Text = "导入";
            this.TSB导入.Click += new System.EventHandler(this.BTN导入_Click);
            // 
            // ImportForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1413, 749);
            this.Controls.Add(this.CTS菜单);
            this.Controls.Add(this.FSB状态);
            this.Controls.Add(this.ATP控件设置);
            this.Name = "ImportForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ImportForm";
            this.CMS导入数据.ResumeLayout(false);
            this.CMS设置右键.ResumeLayout(false);
            this.ATP控件设置.ResumeLayout(false);
            this.TP控件信息导入.ResumeLayout(false);
            this.TP控件位置设置.ResumeLayout(false);
            this.CTS菜单.ResumeLayout(false);
            this.CTS菜单.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.RichTextBox RTB数据;
        private System.Windows.Forms.ContextMenuStrip CMS设置右键;
        private System.Windows.Forms.ToolStripMenuItem TMI清除;
        private ReaLTaiizor.Controls.AirTabPage ATP控件设置;
        private System.Windows.Forms.TabPage TP控件信息导入;
        private System.Windows.Forms.TabPage TP控件位置设置;
        private System.Windows.Forms.OpenFileDialog OFD打开;
        private System.Windows.Forms.Panel PN控件预览;
        private System.Windows.Forms.ToolStripMenuItem TMI添加;
        private System.Windows.Forms.ToolStripMenuItem TMI添加按钮;
        private System.Windows.Forms.ToolStripMenuItem TMI添加标签;
        private System.Windows.Forms.ToolStripMenuItem TMI添加文本;
        private System.Windows.Forms.ToolStripMenuItem TMI添加分组;
        private System.Windows.Forms.ToolStripMenuItem TMI选择;
        private System.Windows.Forms.ToolStripMenuItem TMI设置;
        private System.Windows.Forms.ToolStripMenuItem TMI宽高;
        private System.Windows.Forms.ToolStripMenuItem TMI文本;
        private System.Windows.Forms.ToolStripMenuItem TMItag;
        private ReaLTaiizor.Controls.ForeverStatusBar FSB状态;
        private ReaLTaiizor.Controls.CrownToolStrip CTS菜单;
        private System.Windows.Forms.ToolStripTextBox TTB文件名;
        private System.Windows.Forms.ToolStripButton TSB保存;
        private System.Windows.Forms.ToolStripButton TSB加载;
        private System.Windows.Forms.ToolStripButton TSB导入;
        private System.Windows.Forms.ToolStripLabel TSL初始位置;
        private System.Windows.Forms.ToolStripTextBox TTB初始位置;
        private System.Windows.Forms.ToolStripLabel TSL偏移量;
        private System.Windows.Forms.ToolStripTextBox TTB偏移量;
        private System.Windows.Forms.ToolStripLabel TSL宽高;
        private System.Windows.Forms.ToolStripLabel TSL行数;
        private System.Windows.Forms.ToolStripTextBox TTB行数;
        private System.Windows.Forms.ToolStripMenuItem TMI字体大小;
        private System.Windows.Forms.ToolStripComboBox TCB宽高;
        private System.Windows.Forms.ContextMenuStrip CMS导入数据;
        private System.Windows.Forms.ToolStripMenuItem TMI清除导入数据;
    }
}